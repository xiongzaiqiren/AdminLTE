/* 
* @Author: xiongzaiqiren
* @Date:   2017-11-11 18:22:35
* @Last Modified by:   xiongzaiqiren
* @Last Modified time: 2018-7-15 16:21:08
*/

/*@charset "utf-8";*/


/*左侧菜单定位-开始*/
function ExpandLeftMenu() {
    var pathname = location.pathname.toLowerCase();
    $(".main-sidebar .sidebar-menu>li").each(function (index, element) {
        if (!$(element).is(':has(ul)')) {
            activeLi(element, pathname);
        }
        else {
            //console.log($(element).children("ul"));
            $(element).children("ul").children("li").each(function (index2, element2) {
                if (!$(element2).is(':has(ul)')) {
                    var href = $(element2).children("a").attr("href");
                    if (!!href) {
                        if (href.toLowerCase().indexOf(pathname) >= 0) {
                            //console.log(href);
                            $(element2).parent().parent("li").addClass("active menu-open");
                        }
                    }
                    activeLi(element2, pathname);
                }
                else {
                }
            });
        }

    });
};
function activeLi(element, pathname) {
    var href = $(element).children("a").attr("href");
    if (!!href) {
        if (href.toLowerCase().indexOf(pathname) >= 0) {
            //console.log(href);
            $(element).addClass("active");
        }
    }
}
$(function () {
    ExpandLeftMenu();
});
/*左侧菜单定位-结束*/

$(function () {
    //列表筛选条件
    $("#ConditionalSwitch").click(function () {
        if ($("#ConditionalSwitch i").hasClass("fa-plus")) {
            $("#ConditionalSwitch i").removeClass("fa-plus").addClass("fa-minus");
            $("#ConditionalContainer").slideDown(1000);
        }
        else {
            $("#ConditionalSwitch i").removeClass("fa-minus").addClass("fa-plus");
            $("#ConditionalContainer").slideUp(1000);
        }
    });

});

$(document).keydown(function (event) {
    if (event.keyCode == 13) {
        //SubmitForm();
        //SubmitAjax();
    }
    console.log(event.keyCode);
});

