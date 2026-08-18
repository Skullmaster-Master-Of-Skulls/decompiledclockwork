
function CheckAll(sender, chks_id) {
    var obj = returnObjById( chks_id );
    if (obj != null) {
        var checkBoxes = obj.getElementsByTagName("input");
        for (var i = 0; i < checkBoxes.length; i++) {
            if (!checkBoxes[i].disabled) checkBoxes[i].checked = true;
        }
    }
}

function CheckNone( sender, chks_id ) {
    var obj = returnObjById(chks_id);
    if (obj != null) {
        var checkBoxes = obj.getElementsByTagName("input");
        for (var i = 0; i < checkBoxes.length; i++) {
            if (!checkBoxes[i].disabled) checkBoxes[i].checked = false;
        }
    }
}

function returnObjById(id) {
    if (document.getElementById)
        var returnVar = document.getElementById(id);
    else if (document.all)
        var returnVar = document.all[id];
    else if (document.layers)
        var returnVar = document.layers[id];
    return returnVar;
}
