// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

(function() {
    const originalFetch = window.fetch;
    window.fetch = function(url, options) {
        const token = localStorage.getItem('jwt_token');
        if (token) {
            options = options || {};
            options.headers = options.headers || {};
            options.headers['Authorization'] = 'Bearer ' + token;
        }
        return originalFetch(url, options);
    };
})();
