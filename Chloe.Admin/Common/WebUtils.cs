﻿using Ace;
using Chloe.Application;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Chloe.Admin.Common
{
    public class WebUtils
    {
        public const string STokenName = "stoken";
        public static AdminSession GetCurrentSession()
        {
            FormsAuthenticationTicket ticket = WebHelper.GetTicketByCookieName(WebUtils.STokenName, true);
            if (ticket == null)
                return null;

            string userData = ticket.UserData;

            try
            {
                AdminSession session = JsonHelper.Deserialize<AdminSession>(userData);
                return session;
            }
            catch
            {
                return null;
            }
        }
        public static void SetSession(AdminSession session)
        {
            if (session != null)
            {
                string encryptedTicket = WebHelper.CreateEncryptedTicket(session.UserId, DateTime.Now.AddMinutes(60 * 24), JsonHelper.Serialize(session));
                WebHelper.SetCookie(WebUtils.STokenName, encryptedTicket);
            }
        }
        public static void AbandonSession()
        {
            HttpCookie authCookie = WebHelper.GetCookie(WebUtils.STokenName);
            if (authCookie != null)
            {
                authCookie.Expires = DateTime.Now.AddDays(-1);
                WebHelper.SetCookie(authCookie);
            }
        }
    }
}