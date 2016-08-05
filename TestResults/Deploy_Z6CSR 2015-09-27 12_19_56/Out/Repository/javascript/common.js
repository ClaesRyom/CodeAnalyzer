function changeimage(elementid) {

    var imgid = elementid + "img";

    var s = document.getElementById(imgid).src;
    var sp = s.split('/');
    var imgname = sp[sp.length - 1];

    if (imgname == "plus.png") {
        document.getElementById(imgid).src = "images/minus.png";
        showHide(elementid);
    }

    if (imgname == "minus.png") {
        document.getElementById(imgid).src = "images/plus.png";
        showHide(elementid);
    }
}

function showHide(elementid)
{
  if (document.getElementById(elementid).style.display == 'none')
  {
    document.getElementById(elementid).style.display = '';
  } 
  else 
  {
    document.getElementById(elementid).style.display = 'none';
  }
}


function getQueryStrings() {
    var assoc = {};
    var decode = function (s) { return decodeURIComponent(s.replace(/\+/g, " ")); };
    var queryString = location.search.substring(1);
    var keyValues = queryString.split('&');

    for (var i in keyValues) {
        var key = keyValues[i].split('=');
        if (key.length > 1) {
            assoc[decode(key[0])] = decode(key[1]);
        }
    }

    return assoc;
}


function replaceTitle() {
    var querystring = getQueryStrings();
    var param = querystring['type'];

    document.getElementById('Title').innerHTML = param + ' ' + 'Matches';
}
