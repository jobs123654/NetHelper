using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Test01.Common
{
  
        public class SMSafd
        {
         private static string key = "7d11838ff50e443d9023c0662bfaee14";
        private static string[] source = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P,", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="num">位数</param>
        /// <returns></returns>
        public static string GenCode(int num)
        {
            string code = "";
            //获取验证码
            for (int i = 0; i < num; i++)
            {
                code += source[new Random().Next(0,source.Length)];
            }
            //返回产生的验证码
            return code.Replace(",", "");
        }
        /// <summary>
        /// 短信发送
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <param name="templateid">模板ID</param>
        /// <param name="smscontent">发送内容</param>
        /// <returns></returns>
        public static Boolean sendSMS(string mobile, string templateid, string smscontent)
            {
                Boolean bRet = false;
                string retdata = "";
                try
                {
                    try
                    {
                        if (mobile.Length == 11)
                        {
                            string url = "http://v1.avatardata.cn/Sms/Send?key=" + key + "&mobile=" + mobile +
                            "&templateId=" + templateid + "&param=" + smscontent;
                            string ret = "";
                            WebClient client = new WebClient();
                            client.Encoding = System.Text.Encoding.GetEncoding("GB2312");
                            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                            // "geotable_id=98210&coord_type=3&ak=FT5VAs2fqymIEYoGBv0wqfOG";
                            string postString = url;
                            //string postString = "geotable_id=97335&coord_type=3&ak=yxdgG3XAEYdL2tva5CBbh7wC";
                            byte[] postData = Encoding.Default.GetBytes(postString);
                            byte[] responseData = client.UploadData(url, "POST", postData);
                            ret = Encoding.Default.GetString(responseData);
                            JObject jo = (JObject)JsonConvert.DeserializeObject(ret);
                            string ss = jo["success"].ToString();
                            if (ss == "True")
                            {
                                bRet = true;
                                //BaseDal.RecordError("手机号", mobile+"内容"+smscontent);
                            }
                            else
                            {
                                bRet = false;
                                retdata = "error:" + jo["reason"].ToString();
                                //BaseDal.RecordError("发送短信失败信息",retdata);
                            }
                        }
                        else
                        {
                            retdata = "error:客户电话不是手机号，不能发送短信！";
                            //BaseDal.RecordError("发送短信失败信息", retdata);
                        }
                    }
                    catch (Exception ex)
                    {
                        bRet = false;
                        //retdata = "error:" + this.tdal + ":" + "发送短信处理出错" + ":" + ex.Message;
                    }
                    //return retdata;

                }
                catch
                {

                }
                return bRet;
            }

        }
    }


