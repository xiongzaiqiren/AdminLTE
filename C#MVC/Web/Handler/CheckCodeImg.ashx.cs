using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Handler
{
    /// <summary>
    /// CheckCodeImg 的摘要说明
    /// </summary>
    public class CheckCodeImg : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            string CheckCode = context.Request.Params["CheckCode"] ?? ClassLib4Net.RandomCode.createRandomCode(4);
            //ClassLib4Net.SessionHelper.SetValue("CheckCode", CheckCode);

            System.Drawing.Image _CodeImage = ClassLib4Net.VerificationCode.ValidateImg.CreateCheckCodeImage(CheckCode);
            //System.Drawing.Image _CodeImage = ClassLib4Net.VerificationCode.Captcha.Generate(OrderNo);
            System.IO.MemoryStream _Stream = new System.IO.MemoryStream();
            _CodeImage.Save(_Stream, System.Drawing.Imaging.ImageFormat.Jpeg);

            context.Response.ContentType = "image/tiff";
            context.Response.Clear();
            context.Response.BufferOutput = true;
            context.Response.BinaryWrite(_Stream.GetBuffer());
            context.Response.Flush();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}