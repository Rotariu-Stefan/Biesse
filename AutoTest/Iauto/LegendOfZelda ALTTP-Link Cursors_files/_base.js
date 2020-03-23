
function toggle_login_form()
{
    var loginForm = document.getElementById('loginform');
    if (loginForm.innerHTML == '')
    {
        loginForm.innerHTML = '<form action="'+location.href+'" method="post"><a href="#" onclick="return toggle_login_form()" class="cmddelete">Close</a><strong>Log-in to your account:</strong><hr /><label for="login_email">Email:</label><input name="login_email" id="login_email" /><br /><label for="login_email">Password:</label><input type="password" name="login_pass" id="login_pass" /><input type="submit" value="Login" id="login_submit" /><input type="hidden" name="action" value="inlinelogin" /></form>';
        loginForm.className = 'loginactive';
        document.getElementById('login_email').focus();
    }
    else
    {
        loginForm.innerHTML = '';
        loginForm.className = 'loginhidden';
    }
    return false;
}

function toggle_subscription(approot, type, itemid, control)
{
    var httpRequest = window.XMLHttpRequest ? new XMLHttpRequest() : new ActiveXObject("Microsoft.XMLHTTP");
    httpRequest.onreadystatechange = function()
    {
        if (httpRequest.readyState == 4 && httpRequest.status == 200)
        {
            if (httpRequest.responseText == 'subscribed')
                control.innerHTML = 'Unsubscribe';
            else if (httpRequest.responseText == 'unsubscribed')
                control.innerHTML = 'Subscribe';
        }
    };
    httpRequest.open('POST', approot+'watchdog-ajax.php', true);
    var params =
        "action=togglesubscription"+
        "&type="+type+"&itemid="+itemid;
    httpRequest.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    httpRequest.send(params);
    return false;
}
