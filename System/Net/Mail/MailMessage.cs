using System;
using System.Collections.Specialized;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x020006A0 RID: 1696
	public class MailMessage : IDisposable
	{
		// Token: 0x06003457 RID: 13399 RVA: 0x000DE2B4 File Offset: 0x000DD2B4
		public MailMessage()
		{
			this.message = new Message();
			if (Logging.On)
			{
				Logging.Associate(Logging.Web, this, this.message);
			}
			string from = SmtpClient.MailConfiguration.Smtp.From;
			if (from != null && from.Length > 0)
			{
				this.message.From = new MailAddress(from);
			}
		}

		// Token: 0x06003458 RID: 13400 RVA: 0x000DE324 File Offset: 0x000DD324
		public MailMessage(string from, string to)
		{
			if (from == null)
			{
				throw new ArgumentNullException("from");
			}
			if (to == null)
			{
				throw new ArgumentNullException("to");
			}
			if (from == string.Empty)
			{
				throw new ArgumentException(SR.GetString("net_emptystringcall", new object[]
				{
					"from"
				}), "from");
			}
			if (to == string.Empty)
			{
				throw new ArgumentException(SR.GetString("net_emptystringcall", new object[]
				{
					"to"
				}), "to");
			}
			this.message = new Message(from, to);
			if (Logging.On)
			{
				Logging.Associate(Logging.Web, this, this.message);
			}
		}

		// Token: 0x06003459 RID: 13401 RVA: 0x000DE3E7 File Offset: 0x000DD3E7
		public MailMessage(string from, string to, string subject, string body) : this(from, to)
		{
			this.Subject = subject;
			this.Body = body;
		}

		// Token: 0x0600345A RID: 13402 RVA: 0x000DE400 File Offset: 0x000DD400
		public MailMessage(MailAddress from, MailAddress to)
		{
			if (from == null)
			{
				throw new ArgumentNullException("from");
			}
			if (to == null)
			{
				throw new ArgumentNullException("to");
			}
			this.message = new Message(from, to);
		}

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x0600345B RID: 13403 RVA: 0x000DE43C File Offset: 0x000DD43C
		// (set) Token: 0x0600345C RID: 13404 RVA: 0x000DE449 File Offset: 0x000DD449
		public MailAddress From
		{
			get
			{
				return this.message.From;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.message.From = value;
			}
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x0600345D RID: 13405 RVA: 0x000DE465 File Offset: 0x000DD465
		// (set) Token: 0x0600345E RID: 13406 RVA: 0x000DE472 File Offset: 0x000DD472
		public MailAddress Sender
		{
			get
			{
				return this.message.Sender;
			}
			set
			{
				this.message.Sender = value;
			}
		}

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x0600345F RID: 13407 RVA: 0x000DE480 File Offset: 0x000DD480
		// (set) Token: 0x06003460 RID: 13408 RVA: 0x000DE48D File Offset: 0x000DD48D
		public MailAddress ReplyTo
		{
			get
			{
				return this.message.ReplyTo;
			}
			set
			{
				this.message.ReplyTo = value;
			}
		}

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x06003461 RID: 13409 RVA: 0x000DE49B File Offset: 0x000DD49B
		public MailAddressCollection To
		{
			get
			{
				return this.message.To;
			}
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x06003462 RID: 13410 RVA: 0x000DE4A8 File Offset: 0x000DD4A8
		public MailAddressCollection Bcc
		{
			get
			{
				return this.message.Bcc;
			}
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x06003463 RID: 13411 RVA: 0x000DE4B5 File Offset: 0x000DD4B5
		public MailAddressCollection CC
		{
			get
			{
				return this.message.CC;
			}
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x06003464 RID: 13412 RVA: 0x000DE4C2 File Offset: 0x000DD4C2
		// (set) Token: 0x06003465 RID: 13413 RVA: 0x000DE4CF File Offset: 0x000DD4CF
		public MailPriority Priority
		{
			get
			{
				return this.message.Priority;
			}
			set
			{
				this.message.Priority = value;
			}
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x06003466 RID: 13414 RVA: 0x000DE4DD File Offset: 0x000DD4DD
		// (set) Token: 0x06003467 RID: 13415 RVA: 0x000DE4E5 File Offset: 0x000DD4E5
		public DeliveryNotificationOptions DeliveryNotificationOptions
		{
			get
			{
				return this.deliveryStatusNotification;
			}
			set
			{
				if ((DeliveryNotificationOptions.OnSuccess | DeliveryNotificationOptions.OnFailure | DeliveryNotificationOptions.Delay) < value && value != DeliveryNotificationOptions.Never)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.deliveryStatusNotification = value;
			}
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x06003468 RID: 13416 RVA: 0x000DE505 File Offset: 0x000DD505
		// (set) Token: 0x06003469 RID: 13417 RVA: 0x000DE525 File Offset: 0x000DD525
		public string Subject
		{
			get
			{
				if (this.message.Subject == null)
				{
					return string.Empty;
				}
				return this.message.Subject;
			}
			set
			{
				this.message.Subject = value;
			}
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x0600346A RID: 13418 RVA: 0x000DE533 File Offset: 0x000DD533
		// (set) Token: 0x0600346B RID: 13419 RVA: 0x000DE540 File Offset: 0x000DD540
		public Encoding SubjectEncoding
		{
			get
			{
				return this.message.SubjectEncoding;
			}
			set
			{
				this.message.SubjectEncoding = value;
			}
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x0600346C RID: 13420 RVA: 0x000DE54E File Offset: 0x000DD54E
		public NameValueCollection Headers
		{
			get
			{
				return this.message.Headers;
			}
		}

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x0600346D RID: 13421 RVA: 0x000DE55B File Offset: 0x000DD55B
		// (set) Token: 0x0600346E RID: 13422 RVA: 0x000DE574 File Offset: 0x000DD574
		public string Body
		{
			get
			{
				if (this.body == null)
				{
					return string.Empty;
				}
				return this.body;
			}
			set
			{
				this.body = value;
				if (this.bodyEncoding == null && this.body != null)
				{
					if (MimeBasePart.IsAscii(this.body, true))
					{
						this.bodyEncoding = Encoding.ASCII;
						return;
					}
					this.bodyEncoding = Encoding.GetEncoding("utf-8");
				}
			}
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x0600346F RID: 13423 RVA: 0x000DE5C2 File Offset: 0x000DD5C2
		// (set) Token: 0x06003470 RID: 13424 RVA: 0x000DE5CA File Offset: 0x000DD5CA
		public Encoding BodyEncoding
		{
			get
			{
				return this.bodyEncoding;
			}
			set
			{
				this.bodyEncoding = value;
			}
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x06003471 RID: 13425 RVA: 0x000DE5D3 File Offset: 0x000DD5D3
		// (set) Token: 0x06003472 RID: 13426 RVA: 0x000DE5DB File Offset: 0x000DD5DB
		public bool IsBodyHtml
		{
			get
			{
				return this.isBodyHtml;
			}
			set
			{
				this.isBodyHtml = value;
			}
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x06003473 RID: 13427 RVA: 0x000DE5E4 File Offset: 0x000DD5E4
		public AttachmentCollection Attachments
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				if (this.attachments == null)
				{
					this.attachments = new AttachmentCollection();
				}
				return this.attachments;
			}
		}

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06003474 RID: 13428 RVA: 0x000DE618 File Offset: 0x000DD618
		public AlternateViewCollection AlternateViews
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				if (this.views == null)
				{
					this.views = new AlternateViewCollection();
				}
				return this.views;
			}
		}

		// Token: 0x06003475 RID: 13429 RVA: 0x000DE64C File Offset: 0x000DD64C
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06003476 RID: 13430 RVA: 0x000DE658 File Offset: 0x000DD658
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this.disposed)
			{
				this.disposed = true;
				if (this.views != null)
				{
					this.views.Dispose();
				}
				if (this.attachments != null)
				{
					this.attachments.Dispose();
				}
				if (this.bodyView != null)
				{
					this.bodyView.Dispose();
				}
			}
		}

		// Token: 0x06003477 RID: 13431 RVA: 0x000DE6B0 File Offset: 0x000DD6B0
		private void SetContent()
		{
			if (this.bodyView != null)
			{
				this.bodyView.Dispose();
				this.bodyView = null;
			}
			if (this.AlternateViews.Count == 0 && this.Attachments.Count == 0)
			{
				if (this.body != null && this.body != string.Empty)
				{
					this.bodyView = AlternateView.CreateAlternateViewFromString(this.body, this.bodyEncoding, this.isBodyHtml ? "text/html" : null);
					this.message.Content = this.bodyView.MimePart;
					return;
				}
			}
			else
			{
				if (this.AlternateViews.Count == 0 && this.Attachments.Count > 0)
				{
					MimeMultiPart mimeMultiPart = new MimeMultiPart(MimeMultiPartType.Mixed);
					if (this.body != null && this.body != string.Empty)
					{
						this.bodyView = AlternateView.CreateAlternateViewFromString(this.body, this.bodyEncoding, this.isBodyHtml ? "text/html" : null);
					}
					else
					{
						this.bodyView = AlternateView.CreateAlternateViewFromString(string.Empty);
					}
					mimeMultiPart.Parts.Add(this.bodyView.MimePart);
					foreach (Attachment attachment in this.Attachments)
					{
						if (attachment != null)
						{
							attachment.PrepareForSending();
							mimeMultiPart.Parts.Add(attachment.MimePart);
						}
					}
					this.message.Content = mimeMultiPart;
					return;
				}
				MimeMultiPart mimeMultiPart2 = null;
				MimeMultiPart mimeMultiPart3 = new MimeMultiPart(MimeMultiPartType.Alternative);
				if (this.body != null && this.body != string.Empty)
				{
					this.bodyView = AlternateView.CreateAlternateViewFromString(this.body, this.bodyEncoding, null);
					mimeMultiPart3.Parts.Add(this.bodyView.MimePart);
				}
				foreach (AlternateView alternateView in this.AlternateViews)
				{
					if (alternateView != null)
					{
						alternateView.PrepareForSending();
						if (alternateView.LinkedResources.Count > 0)
						{
							MimeMultiPart mimeMultiPart4 = new MimeMultiPart(MimeMultiPartType.Related);
							mimeMultiPart4.ContentType.Parameters["type"] = alternateView.ContentType.MediaType;
							mimeMultiPart4.ContentLocation = alternateView.MimePart.ContentLocation;
							mimeMultiPart4.Parts.Add(alternateView.MimePart);
							foreach (LinkedResource linkedResource in alternateView.LinkedResources)
							{
								linkedResource.PrepareForSending();
								mimeMultiPart4.Parts.Add(linkedResource.MimePart);
							}
							mimeMultiPart3.Parts.Add(mimeMultiPart4);
						}
						else
						{
							mimeMultiPart3.Parts.Add(alternateView.MimePart);
						}
					}
				}
				if (this.Attachments.Count > 0)
				{
					mimeMultiPart2 = new MimeMultiPart(MimeMultiPartType.Mixed);
					mimeMultiPart2.Parts.Add(mimeMultiPart3);
					MimeMultiPart mimeMultiPart5 = new MimeMultiPart(MimeMultiPartType.Mixed);
					foreach (Attachment attachment2 in this.Attachments)
					{
						if (attachment2 != null)
						{
							attachment2.PrepareForSending();
							mimeMultiPart5.Parts.Add(attachment2.MimePart);
						}
					}
					mimeMultiPart2.Parts.Add(mimeMultiPart5);
					this.message.Content = mimeMultiPart2;
					return;
				}
				if (mimeMultiPart3.Parts.Count == 1 && (this.body == null || this.body == string.Empty))
				{
					this.message.Content = mimeMultiPart3.Parts[0];
					return;
				}
				this.message.Content = mimeMultiPart3;
			}
		}

		// Token: 0x06003478 RID: 13432 RVA: 0x000DEAAC File Offset: 0x000DDAAC
		internal void Send(BaseWriter writer, bool sendEnvelope)
		{
			this.SetContent();
			this.message.Send(writer, sendEnvelope);
		}

		// Token: 0x06003479 RID: 13433 RVA: 0x000DEAC1 File Offset: 0x000DDAC1
		internal IAsyncResult BeginSend(BaseWriter writer, bool sendEnvelope, AsyncCallback callback, object state)
		{
			this.SetContent();
			return this.message.BeginSend(writer, sendEnvelope, callback, state);
		}

		// Token: 0x0600347A RID: 13434 RVA: 0x000DEAD9 File Offset: 0x000DDAD9
		internal void EndSend(IAsyncResult asyncResult)
		{
			this.message.EndSend(asyncResult);
		}

		// Token: 0x0600347B RID: 13435 RVA: 0x000DEAE8 File Offset: 0x000DDAE8
		internal string BuildDeliveryStatusNotificationString()
		{
			if (this.deliveryStatusNotification == DeliveryNotificationOptions.None)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(" NOTIFY=");
			bool flag = false;
			if (this.deliveryStatusNotification == DeliveryNotificationOptions.Never)
			{
				stringBuilder.Append("NEVER");
				return stringBuilder.ToString();
			}
			if ((this.deliveryStatusNotification & DeliveryNotificationOptions.OnSuccess) > DeliveryNotificationOptions.None)
			{
				stringBuilder.Append("SUCCESS");
				flag = true;
			}
			if ((this.deliveryStatusNotification & DeliveryNotificationOptions.OnFailure) > DeliveryNotificationOptions.None)
			{
				if (flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append("FAILURE");
				flag = true;
			}
			if ((this.deliveryStatusNotification & DeliveryNotificationOptions.Delay) > DeliveryNotificationOptions.None)
			{
				if (flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append("DELAY");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04003043 RID: 12355
		private AlternateViewCollection views;

		// Token: 0x04003044 RID: 12356
		private AttachmentCollection attachments;

		// Token: 0x04003045 RID: 12357
		private AlternateView bodyView;

		// Token: 0x04003046 RID: 12358
		private string body = string.Empty;

		// Token: 0x04003047 RID: 12359
		private Encoding bodyEncoding;

		// Token: 0x04003048 RID: 12360
		private bool isBodyHtml;

		// Token: 0x04003049 RID: 12361
		private bool disposed;

		// Token: 0x0400304A RID: 12362
		private Message message;

		// Token: 0x0400304B RID: 12363
		private DeliveryNotificationOptions deliveryStatusNotification;
	}
}
