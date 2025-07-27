# System Authentication Design (JWT with Refresh Token)

This document outlines the authentication mechanism for the `account-web` application. The system has been updated from a traditional session-based approach to a modern, stateless JWT (JSON Web Token) authentication flow that includes a Refresh Token strategy for improved security and user experience.

## 1. Authentication Flow Overview

The authentication process is as follows:

1.  **Login**: The user submits their credentials (`UserId` and `Password`) to the `/Auth/Login` endpoint.
2.  **Token Issuance**: Upon successful validation, the server generates two tokens:
    *   **Access Token**: A short-lived JWT (expires in 15 minutes) containing user claims (ID, name, etc.). This token is used to authorize access to protected API endpoints.
    *   **Refresh Token**: A long-lived, random string (expires in 7 days). Its sole purpose is to obtain a new Access Token.
3.  **Token Storage**: The client-side application stores both the Access Token and the Refresh Token in the browser's `localStorage`.
4.  **API Requests**: For every request to a protected API, the client sends the Access Token in the `Authorization` header (`Bearer <token>`).
5.  **Token Expiration & Refresh**: 
    *   When the Access Token expires, the API will return a `401 Unauthorized` status.
    *   A global `fetch` interceptor on the client-side catches this 401 error.
    *   The interceptor automatically sends the Refresh Token to the `/Auth/Refresh` endpoint.
    *   The server validates the Refresh Token, and if valid, issues a new pair of Access and Refresh Tokens.
    *   The client updates its stored tokens and retries the original, failed API request with the new Access Token.
6.  **Logout**: Logout is handled client-side by simply removing the tokens from `localStorage`.

## 2. Technical Implementation Details

### Backend (`ASP.NET Core`)

*   **JWT Packages**: `Microsoft.AspNetCore.Authentication.JwtBearer` is used for handling JWT validation.
*   **Configuration (`appsettings.json`)**: JWT parameters are configured in the `Jwt` section:
    *   `Issuer`: The token issuer.
    *   `Audience`: The token audience.
    *   `Key`: A secure, secret key for signing the tokens. **This must be managed securely and not hardcoded in production.**
*   **`Program.cs`**: Configures the authentication services and middleware. It sets up the JWT Bearer validation parameters.
*   **`Models/User.cs`**: The `User` model is extended with two nullable fields to support the Refresh Token mechanism:
    *   `RefreshToken` (string): Stores the currently active Refresh Token.
    *   `RefreshTokenExpiryTime` (DateTime): Stores the expiration date of the Refresh Token.
*   **`Controllers/AuthController.cs`**: This controller manages the authentication logic:
    *   `POST /Auth/Login`: Validates credentials, generates both tokens, stores the Refresh Token in the database, and returns the token pair to the client.
    *   `POST /Auth/Refresh`: Accepts an expired Access Token and a valid Refresh Token. It validates the tokens, and if successful, issues a new pair.
    *   `GenerateJwtToken()`: A private method to create and sign the short-lived Access Token.
    *   `GenerateRefreshToken()`: A private method to create a cryptographically secure random string for the Refresh Token.

### Frontend (JavaScript)

*   **`Views/Auth/Login.cshtml`**: The login form submission is handled via an AJAX `fetch` call. On success, it retrieves the `token` and `refreshToken` from the JSON response and stores them in `localStorage`.
*   **`wwwroot/js/site.js`**: Contains a global `fetch` interceptor.
    *   This interceptor automatically adds the `Authorization` header to all outgoing requests.
    *   It includes logic to detect a `401 Unauthorized` response, trigger the `refreshTokenFlow`, and retry the original request upon success.
    *   If the refresh mechanism fails, it clears the tokens and redirects the user to the login page to prevent an infinite loop.

## 3. Security Considerations

*   **Secret Key**: The `Jwt:Key` must be a long, complex, and randomly generated string. It should be stored outside of version control using a secure method like .NET User Secrets, Azure Key Vault, or environment variables.
*   **XSS (Cross-Site Scripting)**: Since tokens are stored in `localStorage`, the application must be protected against XSS attacks, as a successful attack could lead to token theft. Implement proper input validation and output encoding.
*   **HTTPS**: Always use HTTPS in production to ensure that tokens are encrypted in transit.
*   **Token Revocation**: The current implementation revokes a Refresh Token by replacing it in the database when a new one is issued. For immediate, forced logout (e.g., if a user changes their password), you would need to implement a mechanism to invalidate the stored Refresh Token, for example, by setting its expiry time to a past date.
