using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

using MailKit.Net.Smtp;
using MailKit.Security;

using MimeKit;
using MimeKit.Utils;

namespace MCRoll.Utils
{
    public class MailKit
    {
        // <summary>
        /// 发送者
        /// </summary>
        public String Sender { get; set; }

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
        public String Text { get; set; }

        /// <summary>
        /// 图片附件地址
        /// </summary>
        public String ImagePath { get; set; }

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

        private readonly SmtpClient client;

        private readonly MimeMessage message;

        public MailKit()
        {
            this.client = new SmtpClient();
            this.message = new MimeMessage();
            this.Receivers = new List<String>();
        }

        /// <summary>
        /// 初始化邮件
        /// </summary>
        public void Init()
        {
            this.message.From.Add(new MailboxAddress(Sender, Sender));
            if (this.Receivers != null && this.Receivers.Count != 0)
            {
                Receivers.ForEach(r =>
                {
                    this.message.To.Add(new MailboxAddress(r, r));
                });
            }
            this.message.Subject = Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = this.Text;
            if (!String.IsNullOrEmpty(ImagePath))
            {
                builder.Attachments.Add(ImagePath);
                builder.Attachments[0].ContentId = MimeUtils.GenerateMessageId();
                builder.HtmlBody += $"\n<img src=\"\"cid:{builder.Attachments[0].ContentId}\"\">";
            }
            message.Body = builder.ToMessageBody();
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <returns></returns>
        public Boolean Send()
        {
            try
            {
                this.client.ServerCertificateValidationCallback += ServerCertificateValidationCallback;
                this.client.Connect(Host, Port, SecureSocketOptions.Auto);
                this.client.Authenticate(Username, Password);
                this.client.Send(message);
                this.client.Disconnect(true);
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
        /// 异步发送邮件
        /// </summary>
        /// <returns></returns>
        public async Task<Boolean> SendAsync()
        {
            try
            {
                this.client.ServerCertificateValidationCallback += ServerCertificateValidationCallback;
                await this.client.ConnectAsync(Host, Port, SecureSocketOptions.Auto);
                await this.client.AuthenticateAsync(Username, Password);
                await this.client.SendAsync(message);
                await this.client.DisconnectAsync(true);
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

        private Boolean ServerCertificateValidationCallback(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}