// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

(function() {
    const originalFetch = window.fetch;

    window.fetch = async (url, options) => {
        let accessToken = localStorage.getItem('jwt_token');

        if (accessToken) {
            options = options || {};
            options.headers = options.headers || {};
            options.headers['Authorization'] = 'Bearer ' + accessToken;
        }

        let response = await originalFetch(url, options);

        // If response is 401, try to refresh the token
        if (response.status === 401) {
            const refreshToken = localStorage.getItem('refresh_token');
            if (!refreshToken) {
                // No refresh token, redirect to login
                redirectToLogin();
                return response; // Return original 401 response
            }

            try {
                const newTokens = await refreshTokenFlow(accessToken, refreshToken);
                if (newTokens) {
                    localStorage.setItem('jwt_token', newTokens.token);
                    localStorage.setItem('refresh_token', newTokens.refreshToken);

                    // Retry the original request with the new token
                    options.headers['Authorization'] = 'Bearer ' + newTokens.token;
                    response = await originalFetch(url, options);
                }
            } catch (error) {
                console.error('Token refresh failed.', error);
                redirectToLogin();
                return response; // Return original 401 response
            }
        }

        return response;
    };

    async function refreshTokenFlow(expiredToken, refreshToken) {
        const response = await originalFetch('/Auth/Refresh', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                accessToken: expiredToken,
                refreshToken: refreshToken
            })
        });

        if (!response.ok) {
            throw new Error('Failed to refresh token');
        }

        return await response.json();
    }

    function redirectToLogin() {
        localStorage.removeItem('jwt_token');
        localStorage.removeItem('refresh_token');
        window.location.href = '/Auth/Login';
    }
})();
