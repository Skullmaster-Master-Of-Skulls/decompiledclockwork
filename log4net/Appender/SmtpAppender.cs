using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using log4net.Core;

namespace log4net.Appender
{
	// Token: 0x02000041 RID: 65
	public class SmtpAppender : BufferingAppenderSkeleton
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00008116 File Offset: 0x00006316
		// (set) Token: 0x06000234 RID: 564 RVA: 0x0000811E File Offset: 0x0000631E
		public string To
		{
			get
			{
				return this.m_to;
			}
			set
			{
				this.m_to = SmtpAppender.MaybeTrimSeparators(value);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000812C File Offset: 0x0000632C
		// (set) Token: 0x06000236 RID: 566 RVA: 0x00008134 File Offset: 0x00006334
		public string Cc
		{
			get
			{
				return this.m_cc;
			}
			set
			{
				this.m_cc = SmtpAppender.MaybeTrimSeparators(value);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00008142 File Offset: 0x00006342
		// (set) Token: 0x06000238 RID: 568 RVA: 0x0000814A File Offset: 0x0000634A
		public string Bcc
		{
			get
			{
				return this.m_bcc;
			}
			set
			{
				this.m_bcc = SmtpAppender.MaybeTrimSeparators(value);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00008158 File Offset: 0x00006358
		// (set) Token: 0x0600023A RID: 570 RVA: 0x00008160 File Offset: 0x00006360
		public string From
		{
			get
			{
				return this.m_from;
			}
			set
			{
				this.m_from = value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00008169 File Offset: 0x00006369
		// (set) Token: 0x0600023C RID: 572 RVA: 0x00008171 File Offset: 0x00006371
		public string Subject
		{
			get
			{
				return this.m_subject;
			}
			set
			{
				this.m_subject = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000817A File Offset: 0x0000637A
		// (set) Token: 0x0600023E RID: 574 RVA: 0x00008182 File Offset: 0x00006382
		public string SmtpHost
		{
			get
			{
				return this.m_smtpHost;
			}
			set
			{
				this.m_smtpHost = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000818B File Offset: 0x0000638B
		// (set) Token: 0x06000240 RID: 576 RVA: 0x0000818E File Offset: 0x0000638E
		[Obsolete("Use the BufferingAppenderSkeleton Fix methods")]
		public bool LocationInfo
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00008190 File Offset: 0x00006390
		// (set) Token: 0x06000242 RID: 578 RVA: 0x00008198 File Offset: 0x00006398
		public SmtpAppender.SmtpAuthentication Authentication
		{
			get
			{
				return this.m_authentication;
			}
			set
			{
				this.m_authentication = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000243 RID: 579 RVA: 0x000081A1 File Offset: 0x000063A1
		// (set) Token: 0x06000244 RID: 580 RVA: 0x000081A9 File Offset: 0x000063A9
		public string Username
		{
			get
			{
				return this.m_username;
			}
			set
			{
				this.m_username = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000245 RID: 581 RVA: 0x000081B2 File Offset: 0x000063B2
		// (set) Token: 0x06000246 RID: 582 RVA: 0x000081BA File Offset: 0x000063BA
		public string Password
		{
			get
			{
				return this.m_password;
			}
			set
			{
				this.m_password = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000247 RID: 583 RVA: 0x000081C3 File Offset: 0x000063C3
		// (set) Token: 0x06000248 RID: 584 RVA: 0x000081CB File Offset: 0x000063CB
		public int Port
		{
			get
			{
				return this.m_port;
			}
			set
			{
				this.m_port = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000249 RID: 585 RVA: 0x000081D4 File Offset: 0x000063D4
		// (set) Token: 0x0600024A RID: 586 RVA: 0x000081DC File Offset: 0x000063DC
		public MailPriority Priority
		{
			get
			{
				return this.m_mailPriority;
			}
			set
			{
				this.m_mailPriority = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600024B RID: 587 RVA: 0x000081E5 File Offset: 0x000063E5
		// (set) Token: 0x0600024C RID: 588 RVA: 0x000081ED File Offset: 0x000063ED
		public bool EnableSsl
		{
			get
			{
				return this.m_enableSsl;
			}
			set
			{
				this.m_enableSsl = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600024D RID: 589 RVA: 0x000081F6 File Offset: 0x000063F6
		// (set) Token: 0x0600024E RID: 590 RVA: 0x000081FE File Offset: 0x000063FE
		public string ReplyTo
		{
			get
			{
				return this.m_replyTo;
			}
			set
			{
				this.m_replyTo = value;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00008207 File Offset: 0x00006407
		// (set) Token: 0x06000250 RID: 592 RVA: 0x0000820F File Offset: 0x0000640F
		public Encoding SubjectEncoding
		{
			get
			{
				return this.m_subjectEncoding;
			}
			set
			{
				this.m_subjectEncoding = value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000251 RID: 593 RVA: 0x00008218 File Offset: 0x00006418
		// (set) Token: 0x06000252 RID: 594 RVA: 0x00008220 File Offset: 0x00006420
		public Encoding BodyEncoding
		{
			get
			{
				return this.m_bodyEncoding;
			}
			set
			{
				this.m_bodyEncoding = value;
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000822C File Offset: 0x0000642C
		protected override void SendBuffer(LoggingEvent[] events)
		{
			try
			{
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				string text = this.Layout.Header;
				if (text != null)
				{
					stringWriter.Write(text);
				}
				for (int i = 0; i < events.Length; i++)
				{
					base.RenderLoggingEvent(stringWriter, events[i]);
				}
				text = this.Layout.Footer;
				if (text != null)
				{
					stringWriter.Write(text);
				}
				this.SendEmail(stringWriter.ToString());
			}
			catch (Exception e)
			{
				this.ErrorHandler.Error("Error occurred while sending e-mail notification.", e);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000254 RID: 596 RVA: 0x000082BC File Offset: 0x000064BC
		protected override bool RequiresLayout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x000082C0 File Offset: 0x000064C0
		protected virtual void SendEmail(string messageBody)
		{
			SmtpClient smtpClient = new SmtpClient();
			if (!string.IsNullOrEmpty(this.m_smtpHost))
			{
				smtpClient.Host = this.m_smtpHost;
			}
			smtpClient.Port = this.m_port;
			smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
			smtpClient.EnableSsl = this.m_enableSsl;
			if (this.m_authentication == SmtpAppender.SmtpAuthentication.Basic)
			{
				smtpClient.Credentials = new NetworkCredential(this.m_username, this.m_password);
			}
			else if (this.m_authentication == SmtpAppender.SmtpAuthentication.Ntlm)
			{
				smtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
			}
			using (MailMessage mailMessage = new MailMessage())
			{
				mailMessage.Body = messageBody;
				mailMessage.BodyEncoding = this.m_bodyEncoding;
				mailMessage.From = new MailAddress(this.m_from);
				mailMessage.To.Add(this.m_to);
				if (!string.IsNullOrEmpty(this.m_cc))
				{
					mailMessage.CC.Add(this.m_cc);
				}
				if (!string.IsNullOrEmpty(this.m_bcc))
				{
					mailMessage.Bcc.Add(this.m_bcc);
				}
				if (!string.IsNullOrEmpty(this.m_replyTo))
				{
					mailMessage.ReplyToList.Add(new MailAddress(this.m_replyTo));
				}
				mailMessage.Subject = this.m_subject;
				mailMessage.SubjectEncoding = this.m_subjectEncoding;
				mailMessage.Priority = this.m_mailPriority;
				smtpClient.Send(mailMessage);
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00008424 File Offset: 0x00006624
		private static string MaybeTrimSeparators(string s)
		{
			if (!string.IsNullOrEmpty(s))
			{
				return s.Trim(SmtpAppender.ADDRESS_DELIMITERS);
			}
			return s;
		}

		// Token: 0x04000120 RID: 288
		private string m_to;

		// Token: 0x04000121 RID: 289
		private string m_cc;

		// Token: 0x04000122 RID: 290
		private string m_bcc;

		// Token: 0x04000123 RID: 291
		private string m_from;

		// Token: 0x04000124 RID: 292
		private string m_subject;

		// Token: 0x04000125 RID: 293
		private string m_smtpHost;

		// Token: 0x04000126 RID: 294
		private Encoding m_subjectEncoding = Encoding.UTF8;

		// Token: 0x04000127 RID: 295
		private Encoding m_bodyEncoding = Encoding.UTF8;

		// Token: 0x04000128 RID: 296
		private SmtpAppender.SmtpAuthentication m_authentication;

		// Token: 0x04000129 RID: 297
		private string m_username;

		// Token: 0x0400012A RID: 298
		private string m_password;

		// Token: 0x0400012B RID: 299
		private int m_port = 25;

		// Token: 0x0400012C RID: 300
		private MailPriority m_mailPriority;

		// Token: 0x0400012D RID: 301
		private bool m_enableSsl;

		// Token: 0x0400012E RID: 302
		private string m_replyTo;

		// Token: 0x0400012F RID: 303
		private static readonly char[] ADDRESS_DELIMITERS = new char[]
		{
			',',
			';'
		};

		// Token: 0x02000042 RID: 66
		public enum SmtpAuthentication
		{
			// Token: 0x04000131 RID: 305
			None,
			// Token: 0x04000132 RID: 306
			Basic,
			// Token: 0x04000133 RID: 307
			Ntlm
		}
	}
}
