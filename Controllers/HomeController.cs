using LitJson;
using SahaCloudManager.helper;
using SahaCloudManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SahaCloudManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public string Token(string appid,string secret)
        {
            try
            {
                // 从数据库找到token到期日
                SahaContext context = new SahaContext();
                var token = context.Tokens.FirstOrDefault(m => m.AppId.Equals(appid)&&m.Secret.Equals(secret));
                if (token != null) // 数据库中存储有当前appid的token
                {
                    var expires = token.Exprires;
                    var expiresTime = new DateTime(1970, 1, 1).AddMilliseconds(expires);
                    if (DateTime.Now >= expiresTime)
                    {
                        // 如果token已经过期，重新获取token并存储
                        string html = HttpHelper.GetHttp($"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={appid}&secret={secret}");
                        JsonData jsonData = JsonMapper.ToObject(html);
                        string access_token = jsonData["access_token"].ToString();
                        double expires_in = double.Parse(jsonData["expires_in"].ToString());

                        var indate = DateTime.UtcNow.AddSeconds(expires_in);
                        token.Exprires = indate.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
                        token.AccessToken = access_token;
                        context.SaveChanges();
                        return token.AccessToken + ":" + token.Exprires;
                    }
                    else
                    {
                        var indate = new DateTime(1970, 1, 1).AddMilliseconds(token.Exprires);
                        // 如果token没有过期，直接返回此token
                        return token.AccessToken + ":" + token.Exprires;
                    }
                }
                else
                {  // 数据库中没有当前appid的token
                    string html = HttpHelper.GetHttp($"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={appid}&secret={secret}");
                    JsonData jsonData = JsonMapper.ToObject(html);
                    string access_token = jsonData["access_token"].ToString();
                    double expires_in = double.Parse(jsonData["expires_in"].ToString());
                    var indate = DateTime.UtcNow.AddSeconds(expires_in);

                    token = new Token();
                    token.AppId = appid;
                    token.Secret = secret;
                    token.AccessToken = access_token;
                    token.Exprires = indate.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
                    context.Tokens.Add(token);
                    context.SaveChanges();
                    return token.AccessToken + ":" + token.Exprires;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}