﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace Core.Messaging
{
    public class Messaging
    {
        public static void SendEmailGmail(string messageText)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("kevinyangstockalert@gmail.com", "Asdf!2345"),
                EnableSsl = true
            };
            client.Send("kevinyangstockalert@gmail.com", "kevinyangstockalert@gmail.com", "Stock Alert", messageText);
        }
    }
}
