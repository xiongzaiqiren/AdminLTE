/* 
* @Author: xiongzaiqiren
* @Date:   2017-11-11 18:22:35
* @Last Modified by:   xiongzaiqiren
* @Last Modified time: 2017-11-19 16:21:08
*/

/*@charset "utf-8";*/

$(function () {
    BG();
    $("#btnLogin").click(function () {
        //SubmitForm();
        SubmitAjax();
    });

});

$(document).keydown(function (event) {
    if (event.keyCode == 13) {
        //SubmitForm();
        SubmitAjax();
    }
    //console.log(event.keyCode);
});
/*不能有空格*/
$("input").keypress(function () {
    var keynum = event.keyCode;
    if (keynum == 32)
        return false;
});

function BG() {
    var bg = ["bg2", "bg3"];
    var now = new Date();//获取系统当前时间
    if (5 === now.getDate() || 10 === now.getDate() || 15 === now.getDate() || 20 === now.getDate() || 25 === now.getDate() || 29 < now.getDate()) {
        $("body").removeClass(bg[0]).addClass(bg[1]);
    }
    else {
        //$("body").removeClass(bg[1]).addClass(bg[0]);
    }
};
function Reset() {
    $("input,select").each(function (index, element) {
        $(this).val(null);
    });
};
function SubmitForm(id) {
    if (!!id)
        $("form#" + id).submit();
    else
        $("form")[0].submit();
};
function SubmitAjax() {
    var loginType = $("#loginType").val();
    var loginEmail = $("#loginEmail").val();
    var loginPwd = $("#loginPwd").val();
    var loginCheckCode = $("#loginCheckCode").val();
    var loginRememberMe = $("#loginRememberMe").is(':checked');

    //if (!loginType) {
    //    alert("请选择类型！");
    //    showAlert('请选择类型！');
    //    return false;
    //}
    if (!loginEmail || loginEmail.length < 5) {
        //alert("请输入Email/Mobile phone！");
        showAlert('请输入Email/Mobile phone！');
        return false;
    }
    if (!loginPwd || loginPwd.length < 5) {
        //alert("请输入密码！");
        showAlert('请输入密码！');
        return false;
    }
    if (!loginCheckCode || loginCheckCode.length < 4) {
        //alert("请输入验证码！");
        showAlert('请输入验证码！');
        return false;
    }

    $.ajax({
        url: JsConfig.domain + "/Account/Login",
        //url: "/Account/Login",
        data: {
            loginType: loginType||0,
            loginEmail: loginEmail,
            loginPwd: loginPwd,
            loginCheckCode:loginCheckCode,
            loginRememberMe: loginRememberMe
        },
        dataType: "json",
        type: "post",
        beforeSend: function (XHR) {
            $("#btnLogin").attr("disabled", "disabled").css("cursor", "not-allowed").addClass("disabled").html("登录中...");
        },
        dataFilter: function (data, type) {
            if (("json" === type || "jsonp" === type) && "object" === typeof (data)) {
                return data;
            }
            return data;
        },
        success: function (data) {
            if (data.Status == 200) {
                //alert("登录成功");
                showMsg("登录成功");
                if (!!loginRememberMe) {
                    //记住我
                }

                var rtn = CBJS.getUrlParamValue("rtn");
                if (!rtn)
                    rtn = "/Home/Index";
                location.href = rtn;
                //window.location.href = "/scholar/viewpointlist?t=" + (new Date()).getTime();
            }
            else {
                showAlert(data.Message);
                //alert(JSON.stringify(data));
                return;
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            //alert(XMLHttpRequest.status);
            //alert(XMLHttpRequest.readyState);
            //alert(textStatus);
            if (textStatus == "timeout") {
                alert("网络请求失败或超时请重新尝试！");
            }
            else {
                alert("ajax Error!\n\rtextStatus:" + textStatus + "\n\rerrorThrown:" + errorThrown);
            }
            //alert("网络异常");
            return;
        },
        complete: function (XHR, TS) {
            $("#btnLogin").removeAttr("disabled").css("cursor", "pointer").removeClass("disabled").html("登 录");
        }
    });
};