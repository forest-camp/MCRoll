using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MCRoll.Utils
{
    internal class Mail
    {
        /// <summary>
        /// 发送者
        /// </summary>
        public String Sender { get; set; }

        /// <summary>
        /// 发送者显示名称
        /// </summary>
        public String DisplayName { get; set; }

        /// <summary>
        /// 接收者
        /// </summary>
        public List<String> Receivers { get; }

        /// <summary>
        /// 标题
        /// </summary>
        public String Subject { get; set; }

        /// <summary>
        /// 正文
        /// </summary>
        public String Body { get; set; }

        /// <summary>
        /// 正文是否是 html 格式
        /// </summary>
        public Boolean IsBodyHtml { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public String Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public String Password { get; set; }

        /// <summary>
        /// SMTP 服务器
        /// </summary>
        public String Host { get; set; }

        /// <summary>
        /// SMTP 服务器端口
        /// </summary>
        public Int32 Port { get; set; }

        /// <summary>
        /// 是否启用 SSL
        /// </summary>
        public Boolean EnableSSL { get; set; }

        /// <summary>
        /// 是否加密
        /// </summary>
        public Boolean EnableCredential { get; set; }

        private readonly SmtpClient client;

        private readonly MailMessage message;

        public Mail()
        {
            this.client = new SmtpClient();
            this.message = new MailMessage();
            this.Receivers = new List<string>();
            this.IsBodyHtml = true;
            this.EnableSSL = true;
            this.EnableCredential = true;
        }

        /// <summary>
        /// 初始化邮件
        /// </summary>
        public void Init()
        {
            // 初始化邮件消息
            this.message.From = this.DisplayName != null || this.DisplayName != String.Empty ?
               new MailAddress(Sender, DisplayName) : new MailAddress(Sender);
            if (this.Receivers != null && this.Receivers.Count != 0)
            {
                foreach (var receiver in Receivers)
                {
                    this.message.To.Add(receiver);
                }
            }
            this.message.CC.Add(Sender);
            this.message.Subject = Subject;
            this.message.Body = Body;
            this.message.IsBodyHtml = IsBodyHtml;

            // 初始化 SMTP 服务器
            this.client.Host = Host;
            this.client.Port = Port;
            this.client.EnableSsl = EnableSSL;
            if (this.EnableCredential)
            {
                this.client.UseDefaultCredentials = false;
                this.client.Credentials = new System.Net.NetworkCredential(Username, Password);
            }

        }

        /// <summary>
        /// 添加图片
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="mail"></param>
        public void InsertImage(String filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }
            Attachment attachment = new Attachment(filePath);
            this.message.Attachments.Add(attachment);
            this.message.Body += "<img src=\"cid:" + attachment.ContentId + "\"/>";
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <returns></returns>
        public Boolean Send()
        {
            try
            {
                this.client.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("########## Send Error ##########");
                Console.WriteLine(ex);
                Console.WriteLine("########## Send Error ##########");
                return false;
            }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <returns></returns>
        public async Task<Boolean> SendAsync()
        {
            try
            {
                await this.client.SendMailAsync(message);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("########## SendAsync Error ##########");
                Console.WriteLine(ex);
                Console.WriteLine("########## SendAsync Error ##########");
                return false;
            }
        }
    }
}
