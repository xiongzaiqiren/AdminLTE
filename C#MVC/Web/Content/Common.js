;
function showAlert(content, title, icon) {
    layer.alert(content || 'Please be alert.', { title: title || '系统提示', icon: icon || 5 });
};
function showMsg(content, callback, title, icon) {
    layer.msg(content || 'this is message.', { title: title || 'Msg', icon: icon || 6 }, function () {
        callback();
    });
};
