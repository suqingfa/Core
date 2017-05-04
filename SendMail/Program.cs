using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace SendMail
{
    class Program
    {
        static void Main(string[] args)
        {
            SendEmailAsync("524246918@qq.com", "主题", "消息").Wait();
        }

        public static async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("管理员", "admin@suqingfa.win"));
            emailMessage.To.Add(new MailboxAddress("mail", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = message };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtpdm.aliyun.com", 25);
                await client.AuthenticateAsync("admin@suqingfa.win", "eXfZIsYJX23mN6Hp");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}