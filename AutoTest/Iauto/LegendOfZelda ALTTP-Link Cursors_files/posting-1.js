
// required global variables: app_root, checkcode, newpost_params

function insertAtCaret(areaId, text)
{
    var txtarea = document.getElementById(areaId);
    var scrollPos = txtarea.scrollTop;
    var strPos = 0;
    var br = ((txtarea.selectionStart || txtarea.selectionStart == '0') ? "ff" : (document.selection ? "ie" : false ) );
    if (br == "ie")
    {
        txtarea.focus();
        var range = document.selection.createRange();
        range.moveStart ('character', -txtarea.value.length);
        strPos = range.text.length;
    }
    else if (br == "ff")
        strPos = txtarea.selectionStart;

    var front = (txtarea.value).substring(0,strPos);
    var back = (txtarea.value).substring(strPos,txtarea.value.length);
    var before = front.length > 0 && front[front.length-1] != ' ' && front[front.length-1] != '\n' ? ' ' : '';
    var after = back.length > 0 && back[0] != ' ' && back[0] != '\n' ? ' ' : '';
    txtarea.value=front+before+text+after+back;
    strPos = strPos + text.length + before.length + after.length;
    if (br == "ie")
    {
        txtarea.focus();
        var range = document.selection.createRange();
        range.moveStart ('character', -txtarea.value.length);
        range.moveStart ('character', strPos);
        range.moveEnd ('character', 0);
        range.select();
    }
    else if (br == "ff")
    {
        txtarea.selectionStart = strPos;
        txtarea.selectionEnd = strPos;
        txtarea.focus();
    }
    txtarea.scrollTop = scrollPos;
} 

function insert_smiley(what) { insertAtCaret('postarea', what); }
function insert_attachment(id, mime) { insertAtCaret('postarea', '[[image:'+id+']]'); }

function deactivate_postform()
{
    var rtng = document.getElementById('postrtng');
	if (rtng) rtng.innerHTML = '';
    document.getElementById('postctrl').innerHTML = '';
    document.getElementById('postarea').value = '';
    document.getElementById('postarea').style.height = '';
}

var smilies = ['wink.gif', ';-)', '001_smile.gif', ':-)', 'laugh.gif' , ':-D', 'sad.gif', ':-(', 'ohmy.gif', ':-o', 'cool.gif', '8-)', 'sleep.gif', '|-)'];

function activate_postform(extended)
{
    var rtng = document.getElementById('postrtng');
    var ctrl = document.getElementById('postctrl');
    var area = document.getElementById('postarea');
    if (ctrl.innerHTML == '')
    {
        var v = '<input id="postno" class="postbtn" type="button" value="Cancel" onclick="deactivate_postform()" /><input id="postyes" class="postbtn" type="button" value="Publish" onclick="post_comment(\'newcomment\')" />';
        for (i = 0; i < smilies.length; i += 2)
            v += '<img src="'+app_root+'sml/'+smilies[i]+'" alt="'+smilies[i+1]+'" onclick="insert_smiley(this.alt)" />';
        if (typeof quickcodes == "object")
        	for (i = 0; i < quickcodes.length; i += 2)
                v += '<img src="'+app_root+quickcodes[i]+'" alt="'+quickcodes[i+1]+'" onclick="insert_smiley(this.alt)" />';
        ctrl.innerHTML = v+' &nbsp; <span style="display:inline-block;vertical-align:top;line-height:24px;"><a href="'+app_root+'forum/1507" rel="nofollow" target="_blank">More about formatting.</a></span>';
        area.style.height = '180px';
		if (rtng)
		{
			var selectcode = 'Your rating: <select id="poststar">';
			var selectoptions = ['&lt;not set&gt;', '0.5 stars - worst', '1 star', '1.5 stars', '2 stars', '2.5 stars', '3 stars', '3.5 stars', '4 stars', '4.5 stars', '5 stars - best'];
			for (var i = -1; i < 10; ++i)
				selectcode += '<option value="'+(i+1)+'"'+(i == current_rating ? ' selected="selected"' : '')+'>'+selectoptions[i+1]+'</option>';
			rtng.innerHTML = selectcode+'</select>';
		}
        area.scrollIntoView();
    }
}

function post_comment(action)
{
    var rtng = document.getElementById('poststar');
    var area = document.getElementById('postarea');
    var msg = area.value.replace(/^\s+|\s+$/g,"");
    if (msg == '')
    {
        deactivate_postform('');
        return false;
    }
    var btnyes = document.getElementById('postyes');
    var btnno = document.getElementById('postno');
    var prevtext = btnyes.value;
    btnyes.value = 'Working';
    btnyes.disabled = true;
    btnno.disabled = true;
    area.disabled = true;
    if (rtng) rtng.disabled = true;
    var httpRequest = window.XMLHttpRequest ? new XMLHttpRequest() : new ActiveXObject("Microsoft.XMLHTTP");
    httpRequest.onreadystatechange = function()
    {
        if (httpRequest.readyState == 4)
        {
            if (httpRequest.status == 200 && httpRequest.responseText != '')
            {
                area.disabled = false;
                document.getElementById('postbodies').innerHTML = httpRequest.responseText;
                deactivate_postform('');
            }
            else
            {
                btnyes.value = prevtext;
                btnyes.disabled = false;
                btnno.disabled = false;
                area.disabled = false;
				if (rtng) rtng.disabled = false;
                alert('Failed to publish your post. Please try again later.');
            }
        }
    };
	if (rtng) current_rating = rtng.value-1;
    httpRequest.open('POST', app_root+'comments_ajax.php', true);
	var rating = rtng ? "&rating="+rtng.value : '';
    var params = "action="+action+rating+
        "&message="+encodeURIComponent(msg)+
        newpost_params+"&checkcode="+checkcode;
    httpRequest.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    httpRequest.send(params);
}

function delete_post(commid, forum)
{
    var httpRequest = window.XMLHttpRequest ? new XMLHttpRequest() : new ActiveXObject("Microsoft.XMLHTTP");
    httpRequest.onreadystatechange = function()
    {
        if (httpRequest.readyState == 4)
        {
            if (httpRequest.status == 200)
            {
                if (httpRequest.responseText == 'OK')
                {
                    var item = document.getElementById('post'+commid);
                    if (item != null)
                        item.parentNode.removeChild(item);
                }
                else
                    alert(httpRequest.responseText);
            }
        }
    };
    httpRequest.open('POST', app_root+(forum ? 'forum_ajax.php' : 'comments_ajax.php'), true);
    var params = (forum ? "action=deletereply&postid=" : "deleteid=")+commid;
    httpRequest.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    httpRequest.send(params);
    return false;
}

var saved_posts = new Array();

function modify_comment_cancel(commid)
{
    var div = document.getElementById('content'+commid);
    var saved = saved_posts[commid];
    if (saved != null)
    {
        div.className = saved[0];
        div.innerHTML = saved[1];
        saved_posts[commid] = null;
    }
}

function modify_comment_apply(commid, forum)
{
    var area = document.getElementById('postarea'+commid);
    if (area == null)
        return false;
    var msg = area.value.replace(/^\s+|\s+$/g,"");
    if (msg == '')
    {
        modify_comment_cancel(commid);
        return false;
    }
    var btnyes = document.getElementById('postyes'+commid);
    var btnno = document.getElementById('postno'+commid);
    var prevtext = btnyes.value;
    btnyes.value = 'Working';
    btnyes.disabled = true;
    btnno.disabled = true;
    area.disabled = true;
    var httpRequest = window.XMLHttpRequest ? new XMLHttpRequest() : new ActiveXObject("Microsoft.XMLHTTP");
    httpRequest.onreadystatechange = function()
    {
        if (httpRequest.readyState == 4)
        {
            if (httpRequest.status == 200 && httpRequest.responseText != '')
            {
                area.disabled = false;
                var frag = document.createElement('div');
                frag.innerHTML = httpRequest.responseText;
                var orig = document.getElementById('post'+commid);
                var parent = orig.parentNode;
                var before = orig.nextSibling;
                parent.removeChild(orig);
                parent.insertBefore(frag.firstChild, before);
            }
            else
            {
                btnyes.value = prevtext;
                btnyes.disabled = false;
                btnno.disabled = false;
                area.disabled = false;
                alert('Failed to update the post. Please try again later.');
            }
        }
    };
    httpRequest.open('POST', app_root+(forum ? 'forum_ajax.php' : 'comments_ajax.php'), true);
	var saved = forum ? null : saved_posts[commid];
    var withrating = saved != null && saved[2] ? '&showrating=1' : '';
    var params = "action="+(forum ? 'modifyreply' : 'modifycomment')+
        "&message="+encodeURIComponent(msg)+withrating+
        "&checkcode="+checkcode+
        "&postid="+commid;
    httpRequest.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    httpRequest.send(params);
}

function modify_post(commid, forum)
{
    if (saved_posts[commid] != null)
        modify_comment_cancel(commid);
    var httpRequest = window.XMLHttpRequest ? new XMLHttpRequest() : new ActiveXObject("Microsoft.XMLHTTP");
    httpRequest.onreadystatechange = function()
    {
        if (httpRequest.readyState == 4)
        {
            if (httpRequest.status == 200)
            {
                var div = document.getElementById('content'+commid);
                var rat = document.getElementById('rating'+commid);
                saved_posts[commid] = new Array(div.className, div.innerHTML, rat != null);
                div.className = 'content full';
                var height = Math.round(httpRequest.responseText.length/50)*30;
                if (height < 60) height = 60; else if (height > 240) height = 240;
                div.innerHTML = '<textarea id="postarea'+commid+'" rows="10" cols="50" style="height:'+height+'px">'+httpRequest.responseText+'<\/textarea><div class="postctrl" id="postctrl'+commid+'"><input id="postno'+commid+'" class="postbtn" type="button" value="Cancel" onclick="modify_comment_cancel('+commid+')" /><input id="postyes'+commid+'" class="postbtn" style="font-weight:bold" type="button" value="Publish" onclick="modify_comment_apply('+commid+', '+forum+')" /></div>';
            }
        }
    };
    httpRequest.open('POST', app_root+(forum ? 'forum_ajax.php' : 'comments_ajax.php'), true);
    var params = "action="+(forum ? 'getpost' : 'getcomment')+"&postid="+commid;
    httpRequest.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    httpRequest.send(params);
    return false;
}

// forum

function delete_comment(commid)
{
    return delete_post(commid, false);
}

function modify_comment(commid)
{
    return modify_post(commid, false);
}






var postingcancel = '';

function cancel_form()
{
    if (postingcancel != '')
    {
        document.getElementById('postform').innerHTML = postingcancel;
        postingcancel = '';
    }
}

function show_postform(formtype, extraparam)
{
    var httpRequest = window.XMLHttpRequest ? new XMLHttpRequest() : new ActiveXObject("Microsoft.XMLHTTP");
    httpRequest.onreadystatechange = function()
    {
        if (httpRequest.readyState == 4 && httpRequest.status == 200)
        {
            postingcancel = document.getElementById('postform').innerHTML;
            document.getElementById('postform').innerHTML = httpRequest.responseText;
            document.getElementById('topicbody').focus();
        }
    };
    httpRequest.open('POST', app_root+'forum_ajax.php', true);
    var params = "action=get"+formtype;
    if (extraparam != null) params += "&extraparam="+extraparam;
    httpRequest.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    httpRequest.send(params);
    return false;
}

function post_reply(topicid, elementid, statusid)
{
    var httpRequest = window.XMLHttpRequest ? new XMLHttpRequest() : new ActiveXObject("Microsoft.XMLHTTP");
    httpRequest.onreadystatechange = function()
    {
        if (httpRequest.readyState == 4 && httpRequest.status == 200)
        {
            if (httpRequest.responseText != 'error')
            {
                cancel_form();
                if (refreshtimerid != -1234)
                    forum_status_update();
            }
            else
            {
                document.getElementById(statusid).innerHTML = '';
                alert('An error occured, please make sure you have written your reply according to the rules and try again.');
            }
        }
    };
    httpRequest.open('POST', app_root+'forum_ajax.php', true);
    var params =
        "action=postreply"+
        "&message="+encodeURIComponent(document.getElementById(elementid).value)+
        "&topicid="+topicid+
        "&checkcode="+checkcode;
    document.getElementById(statusid).innerHTML += 'Working...';
    httpRequest.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    httpRequest.send(params);
}
