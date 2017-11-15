using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using CardiologicalResearch.Models;

namespace CardiologicalResearch
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // следует обновить сайт. Дополнительные сведения: http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: string.Empty,
            //    clientSecret: string.Empty);

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: string.Empty,
            //    consumerSecret: string.Empty);

            //OAuthWebSecurity.RegisterFacebookClient(
            //    appId: string.Empty,
            //    appSecret: string.Empty);

            //OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
