(function () {
    $(function () {
        $("#input_apiKey").hide();
        if ($("#input_username").length < 1) {
            var basicAuthUI =
                '<div class="input"><input placeholder="Username" id="input_username" name="username" type="text" size="15"/></div>' +
                '<div class="input"><input placeholder="Password" id="input_password" name="password" type="password" size="10"/></div>';
            $(basicAuthUI).insertBefore('#api_selector div.input:last-child');
        } 

        $("#explore").unbind().click( function () {
            $.ajax({
                url: "http://localhost:15360/token",
                type: "post",
                contenttype: 'x-www-form-urlencoded',
                data: "grant_type=password&username=" + $('#input_username').val() + "&password=" + $('#input_password').val(),
                success: function (response) {
                    var bearerToken = "Bearer " + response.access_token;
                    console.log("Access Token Key is:   " + bearerToken);
                    window.swaggerUi.api.clientAuthorizations.remove('api_key');
                    var apiKeyAuth = new SwaggerClient.ApiKeyAuthorization("Authorization", bearerToken, "header");
                    window.swaggerUi.api.clientAuthorizations.add('oauth2', apiKeyAuth);
                    alert("Login successful! Check console (F12) for Token Key.");
                },
                error: function (xhr, ajaxoptions, thrownerror) {
                    alert("Login failed! Maybe wrong Password or UserName");
                }
            });
        });
    });

     
})();