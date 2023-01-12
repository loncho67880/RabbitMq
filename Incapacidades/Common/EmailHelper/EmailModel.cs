using System.Net.Mail;

namespace Common.EmailHelper
{
    public class EmailModel
    {
        public EmailModel()
        {
            Addressee = new DestinationMail();
            Body = new BodyMail();
            Attachements = new List<Attachment>();
        }

        #region Public Properties
        public MailType Type { get; set; }

        public DestinationMail Addressee { get; set; }

        public BodyMail Body { get; set; }

        public string Subject { get; set; }

        public string FromDisplayName { get; set; }
        public string SenderEmail { get; set; }
        public string File { get; set; }

        public List<Attachment> Attachements { get; set; }

        #endregion

        #region Models
        public enum MailType
        {
            Notificacion,
            Alerta,
            Email
        }

        public class DestinationMail
        {
            public DestinationMail()
            {
                To = new List<string>();
                CC = new List<string>();
                CCO = new List<string>();
            }

            public List<string> To { get; set; }
            public List<string> CC { get; set; }
            public List<string> CCO { get; set; }
        }

        public class BodyMail
        {
            public BodyMail()
            {
                HeaderMessages = new List<string>();
                FooterMessages = new List<string>();
            }

            public string Title { get; set; }
            public string HtmlContent { get; set; }
            public List<string> HeaderMessages { get; set; }
            public List<string> FooterMessages { get; set; }
            public object ModelToPrint { get; set; }
            public LinkButton Button { get; set; }

            public class LinkButton
            {
                public string Text { get; set; }
                public string Url { get; set; }
            }
        }
        #endregion
    }
}
