/**
* 命名空间: Common
*
* 功 能： N/A
* 类 名： ToolHelper
*
* Ver 变更日期 负责人 当前系统用户名 CLR版本 变更内容
* ───────────────────────────────────
* V0.01 2018/12/2 13:35:41 熊仔其人 xxh 4.0.30319.42000 初版
*
* Copyright (c) 2018 熊仔其人 Corporation. All rights reserved.
*┌─────────────────────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．   │
*│　版权所有：熊仔其人 　　　　　　　　　　　　　　　　　　　　　　　 │
*└─────────────────────────────────────────────────┘
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public static class ToolHelper
    {
        static private ReaderWriterLock _rwlock = new ReaderWriterLock();
        /// <summary>
        /// 使用C#把发表的时间改为几个月,几天前,几小时前,几分钟前,或几秒前
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DateStringFromNow(DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;
            if(span.TotalDays > 60)
            {
                return dt.ToShortDateString();
            }
            else
            {
                if(span.TotalDays > 30)
                {
                    return
                    "1个月前";
                }
                else
                {
                    if(span.TotalDays > 14)
                    {
                        return
                        "2周前";
                    }
                    else
                    {
                        if(span.TotalDays > 7)
                        {
                            return
                            "1周前";
                        }
                        else
                        {
                            if(span.TotalDays > 1)
                            {
                                return
                                string.Format("{0}天前", (int)Math.Floor(span.TotalDays));
                            }
                            else
                            {
                                if(span.TotalHours > 1)
                                {
                                    return
                                    string.Format("{0}小时前", (int)Math.Floor(span.TotalHours));
                                }
                                else
                                {
                                    if(span.TotalMinutes > 1)
                                    {
                                        return
                                        string.Format("{0}分钟前", (int)Math.Floor(span.TotalMinutes));
                                    }
                                    else
                                    {
                                        if(span.TotalSeconds >= 1)
                                        {
                                            return
                                            string.Format("{0}秒前", (int)Math.Floor(span.TotalSeconds));
                                        }
                                        else
                                        {
                                            return
                                            "1秒前";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        ///<summary>
        /// 根据枚举类型获取描述
        ///</summary>
        ///<param name="enumSubitem">类型</param>
        ///<returns>描述</returns>
        public static string GetEnumDescription(object enumSubitem)
        {
            string strValue = enumSubitem.ToString();
            try
            {
                FieldInfo fieldinfo = enumSubitem.GetType().GetField(strValue);
                if(fieldinfo != null)
                {
                    Object[] objs = fieldinfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if(objs == null || objs.Length == 0)
                    {
                        return strValue;
                    }
                    var da = (DescriptionAttribute)objs[0];
                    return da.Description;
                }
            }
            catch(Exception ex) { }
            return strValue;
        }

        public static string GetEnumHtml(Type enumType, string id, bool isAll = false)
        {
            string sel = "   <select class=\"selectpicker\" name=\"" + id + "\" id=\"" + id + "\">";
            if(isAll)
            {
                sel += "<option value = \"-1\" >全部</option >";
            }
            foreach(int status in Enum.GetValues(enumType))
            {
                string strVaule = status.ToString();//获取值
                string strDesc = GetEnumDescription(Enum.Parse(enumType, status.ToString()));//获取描述
                sel += "<option value = \"" + strVaule + "\" >" + strDesc + "</option >";

            }
            sel += "</select > ";
            return sel;
        }
        public static string GetEnumOptionHtml(Type enumType)
        {
            string sel = "";
            sel += "<option value = \"-1\" >全部</option >";
            foreach(int status in Enum.GetValues(enumType))
            {
                string strVaule = status.ToString();//获取值
                string strDesc = GetEnumDescription(Enum.Parse(enumType, status.ToString()));//获取描述
                sel += "<option value = \"" + strVaule + "\" >" + strDesc + "</option >";

            }
            sel += " ";
            return sel;
        }
        public static string GetEnumRadioHtml(Type enumType, string name)
        {

            StringBuilder sb = new StringBuilder();
            foreach(int status in Enum.GetValues(enumType))
            {
                string strVaule = status.ToString();//获取值
                string strDesc = GetEnumDescription(Enum.Parse(enumType, status.ToString()));//获取描述
                                                                                             // sel += "<option value = \"" + strVaule + "\" >" + strDesc + "</option >";
                sb.Append("<input type=\"radio\" name=\"" + name + "\" value=\"" + strVaule + "\" />");
                sb.Append(strDesc);
            }

            return sb.ToString();
        }
        public static string GetEnumCheckBoxHtml(Type enumType, string name)
        {

            StringBuilder sb = new StringBuilder();
            foreach(int status in Enum.GetValues(enumType))
            {
                string strVaule = status.ToString();//获取值
                string strDesc = GetEnumDescription(Enum.Parse(enumType, status.ToString()));//获取描述
                sb.Append("<label class=\"checkbox-inline\">");
                sb.Append(" <input type=\"checkbox\" class=\"SelectOption\" value=\"" + strVaule + "\" name=\"" + name + "\">" + strDesc + "");
                sb.Append(" </label>");


            }

            return sb.ToString();
        }
        public static string GetEnumRadioShowHtml(Type enumType, string name)
        {

            StringBuilder sb = new StringBuilder();
            foreach(int status in Enum.GetValues(enumType))
            {
                sb.Append("<label class=\"radio-inline\">");
                string strVaule = status.ToString();//获取值
                string strDesc = GetEnumDescription(Enum.Parse(enumType, status.ToString()));//获取描述

                sb.Append("<input type=\"radio\" value=\"" + strVaule + "\" disabled=\"disabled\" name=\"" + name + "\">");
                sb.Append(strDesc);
                sb.Append("</label>");

            }

            return sb.ToString();
        }
        /// <summary>
        /// Object类型转String
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string O2S(object o)
        {
            return o == DBNull.Value || o == null ? "" : o.ToString().Trim();
        }
        public static string ReturnOrderStatus(int i)
        {
            string ret = "";
            switch(i)
            {
                case 0:
                    ret = "关闭";
                    return ret;
                case 10:
                    ret = "已提交";
                    return ret;
                case 11:
                    ret = "等待买家付款";
                    return ret;
                case 20:
                    ret = "买家已付款";
                    return ret;
                case 30:
                    ret = "卖家服务中";
                    return ret;
                case 40:
                    ret = "交易完成";
                    return ret;
                case 50:
                    ret = "待评价";
                    return ret;
                case 60:
                    ret = "服务完成";
                    return ret;
                default:
                    return ret;
            }
        }
        //VB IIF
        public static object IIF(bool b, object Obj1, object Obj2)
        {
            return b ? Obj1 : Obj2;
        }
        //VB IIF 返回string
        public static string IIFS(bool b, object Obj1, object Obj2)
        {
            return O2S(IIF(b, Obj1, Obj2));
        }
        public static bool IsPhoneNumber(string number)
        {
            var regex = new System.Text.RegularExpressions.Regex("^(0|86|17951)?(13[0-9]|15[012356789]|17[012356789]|18[0-9]|14[57])[0-9]{8}$",
                System.Text.RegularExpressions.RegexOptions.Compiled);
            if(string.IsNullOrWhiteSpace(number)) return false;
            return regex.IsMatch(number);
        }
        /// <summary>
        /// 是否字母和数字
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsCharInt(string number)
        {
            var regex = new System.Text.RegularExpressions.Regex("^[0-9a-zA-Z]*$",
                System.Text.RegularExpressions.RegexOptions.Compiled);
            if(string.IsNullOrWhiteSpace(number)) return false;
            return regex.IsMatch(number);
        }
        public static string IF2(object o, object o2)
        {
            string s = O2S(o);
            if(!string.IsNullOrEmpty(s)) return s;
            return O2S(o2);
        }
        public static void WriteLog(string Message, string sType)
        {

            WriteLog(Message, System.Web.HttpContext.Current.Server.MapPath("~/Logs"), sType);
        }
        public static void WritePathLog(string Message, string Path, string sType)
        {

            WriteLog(Message, Path, sType);
        }
        public static void WriteLogWinFrom(string Message, string sType)
        {

            WriteLog(Message, System.AppDomain.CurrentDomain.BaseDirectory + "Logs", sType);
        }
        //写日志
        public static void WriteLog(string Message, string sPath, string sType, int type = 0)
        {
            StreamWriter SW = null;
            _rwlock.AcquireReaderLock(10);
            try
            {
                if(!Directory.Exists(sPath + @"\" + DateTime.Now.ToString("yyyyMM")))
                {
                    Directory.CreateDirectory(sPath + @"\" + DateTime.Now.ToString("yyyyMM"));
                }
                SW = File.AppendText(sPath + @"\" + DateTime.Now.ToString("yyyyMM") + @"\" + DateTime.Now.ToString("MM-dd") + sType + ".txt");
                if(type == 0)
                {
                    SW.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "  " + Message.Replace("\r", "").Replace("\n", ""));
                }
                else
                {
                    SW.WriteLine(Message);
                }
                SW.Flush();
            }
            catch { }
            finally
            {
                if(SW != null) SW.Close();
                _rwlock.ReleaseReaderLock();
            }
        }


        public static string QRS(string Obj)
        {
            return "'" + Obj + "'";
        }
        public static string QRSN(string Obj)
        {
            return "N" + QRS(Obj);
        }
        //sql LIKE ‘%s%’处理
        public static string QSL(string s)
        {
            return "'%" + s.Replace("'", "").Trim() + "%'";
        }
        //sql LIKE ‘s%’处理
        public static string QSL_Suf(string s)
        {
            return "'" + s.Replace("'", "").Trim() + "%'";
        }
        //sql LIKE ‘%s’处理
        public static string QSL_Pre(string s)
        {
            return "'%" + s.Replace("'", "").Trim() + "'";
        }

        public static string SubStrLen(string s, int Len)
        {
            if(s.Length < Len) return s;
            return s.Substring(0, Len);
        }
        public static string RightStr(string s, int Len)
        {
            if(s.Length < Len) return s;
            return s.Substring(s.Length - Len);
        }

        public static string[] SplitStrEmpty(string s, string sp = ",")
        {
            if(s == null) return null;
            return s.Trim().Split(new string[] { sp }, StringSplitOptions.RemoveEmptyEntries);
        }
        public static string[] SplitStrNone(string s, string sp = ",")
        {
            if(s == null) return null;
            return s.Trim().Split(new string[] { sp }, StringSplitOptions.None);
        }

        /// <summary>
        /// DateTime转换为JavaScript时间戳
        /// </summary>
        /// <returns></returns>
        public static long DateTimeJavaScriptTimeSpan()
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            long timeStamp = (long)(DateTime.Now - startTime).TotalMilliseconds; // 相差毫秒数
            return timeStamp;
        }
        /// <summary>
        /// JavaScript时间戳转换为 DateTime
        /// </summary>
        /// <param name="jsTimeStamp"></param>
        /// <returns></returns>
        public static DateTime JavaScriptDateTimeTimeSpan(long jsTimeStamp)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            DateTime dt = startTime.AddMilliseconds(jsTimeStamp);
            return dt;
        }
        /// <summary>
        /// DateTime转换为Unix时间戳
        /// </summary>
        /// <returns></returns>
        public static long DateTimeUnixTimeSpan()
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            long timeStamp = (long)(DateTime.Now - startTime).TotalSeconds; // 相差毫秒数
            return timeStamp;
        }
        public static long DateTimeUnixTimeSpan(DateTime dt)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            long timeStamp = (long)(dt - startTime).TotalSeconds; // 相差毫秒数
            return timeStamp;
        }
        /// <summary>
        /// Unix时间戳转换为 DateTime
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static DateTime UnixDateTimeSpan(long unixTimeStamp)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            DateTime dt = startTime.AddSeconds(unixTimeStamp);
            return dt;
        }
        /// <summary>
        /// 返回隐藏中间的字符串
        /// </summary>
        /// <param name="Input">输入</param>
        /// <returns>输出</returns>
        public static string GetxxxString(string Input)
        {
            try
            {
                string Output = "";
                switch(Input.Length)
                {
                    case 1:
                        Output = "*";
                        break;
                    case 2:
                        Output = Input[0] + "*";
                        break;
                    case 0:
                        Output = "";
                        break;
                    default:
                        Output = Input.Substring(0, 1);
                        for(int i = 0; i < Input.Length - 2; i++)
                        {
                            Output += "*";
                        }
                        Output += Input.Substring(Input.Length - 1, 1);
                        break;
                }
                return Output;
            }
            catch(Exception ex)
            {
                return "";
            }
        }
    }
}
