using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SahaCloudManager.Models
{
    public class Token
    {
        public Token(string appId,string secret, string accessToken, double exprires)
        {
            AppId = appId;
            Secret = secret;
            AccessToken = accessToken;
            Exprires = exprires;
        }

        public Token()
        {
        }

        public int Id { get; set; }
        public string AppId { get; set; }
        public string Secret { get; set; }
        public string AccessToken { get; set; }
        public double Exprires { get; set; }
    }
}