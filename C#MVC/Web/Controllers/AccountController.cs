using BLL;
using ClassLib4Net.Api;
using Common;
using Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            var entity = BLLRepository.adminBLL.Select(1);
            entity.Pwd = CryptoHelper.GeneratePassword("123456");
            entity.LastLoginTime = DateTime.Now;
            entity.LastLoginIP = ClassLib4Net.SystemInfo.GetIP();
            var r = BLLRepository.adminBLL.Update(entity.AdminID, entity.LastLoginTime.Value, entity.LastLoginIP);
            
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(int loginType, string loginEmail, string loginPwd, bool loginRememberMe)
        {
            var result = BLLRepository.adminBLL.Login(loginEmail, loginPwd);
            if(null != result && StatusCode.Success.GetHashCode() == result.Status)
            {
                //登录成功
                var aa = new AccessAccount();
                aa.ID = result.Data.AdminID;
                aa.Name = result.Data.UserName;
                aa.NickName = result.Data.NickName;
                aa.JobNumber = result.Data.JobNumber;
                aa.loginRememberMe = loginRememberMe;

                AccessService.Account = aa;
            }

            return Json(result);
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            ClassLib4Net.SessionHelper.Remove("");
            return View();
        }

    }
}