using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using a;
using a.i;
using MailBee.Security;
using MailBee.SmtpMail;

namespace MailBee.Mime
{
	// Token: 0x02000550 RID: 1360
	public class MailMessage : IDisposable
	{
		// Token: 0x06002C5C RID: 11356 RVA: 0x000D2DF8 File Offset: 0x000D1DF8
		public MailMessage ConvertToSystemNetMail()
		{
			this.Builder.Apply();
			MailMessage mailMessage = new MailMessage();
			if (this.Charset != string.Empty)
			{
				try
				{
					mailMessage.BodyEncoding = Encoding.GetEncoding(this.Charset);
				}
				catch (ArgumentException)
				{
				}
			}
			for (int i = 0; i < this.Headers.Count; i++)
			{
				string[] array = this.Headers[i].RawBody.c().Split(new char[]
				{
					':'
				}, 2);
				if (array.Length == 2)
				{
					string text = array[1].Trim().Replace("\r", string.Empty).Replace("\n", string.Empty);
					try
					{
						if (text != string.Empty && this.Headers[i].Name.ToLower() != "content-type")
						{
							mailMessage.Headers.Add(this.Headers[i].Name, text);
						}
					}
					catch (FormatException)
					{
						if (this.Charset != string.Empty)
						{
							string value = global::a.i.h.a(string.Empty, text, MailTransferEncoding.Base64, this.Charset, HeaderEncodingOptions.None, true);
							mailMessage.Headers.Add(this.Headers[i].Name, value);
						}
					}
				}
			}
			if (this.From != null)
			{
				try
				{
					mailMessage.From = new MailAddress(this.From.ToString());
				}
				catch (FormatException)
				{
				}
			}
			foreach (object obj in this.To)
			{
				EmailAddress emailAddress = (EmailAddress)obj;
				try
				{
					mailMessage.To.Add(emailAddress.ToString());
				}
				catch (FormatException)
				{
				}
			}
			foreach (object obj2 in this.Cc)
			{
				EmailAddress emailAddress2 = (EmailAddress)obj2;
				try
				{
					mailMessage.CC.Add(emailAddress2.ToString());
				}
				catch (FormatException)
				{
				}
			}
			foreach (object obj3 in this.Bcc)
			{
				EmailAddress emailAddress3 = (EmailAddress)obj3;
				try
				{
					mailMessage.Bcc.Add(emailAddress3.ToString());
				}
				catch (FormatException)
				{
				}
			}
			mailMessage.Subject = this.Subject.Replace("\n", "");
			switch (this.Priority)
			{
			case MailPriority.Highest:
			case MailPriority.High:
				mailMessage.Priority = MailPriority.High;
				break;
			case MailPriority.Normal:
				mailMessage.Priority = MailPriority.Normal;
				break;
			case MailPriority.Low:
			case MailPriority.Lowest:
				mailMessage.Priority = MailPriority.Low;
				break;
			}
			if (this.BodyHtmlText != string.Empty)
			{
				mailMessage.IsBodyHtml = true;
				mailMessage.Body = this.BodyHtmlText;
			}
			else if (this.BodyPlainText != string.Empty)
			{
				mailMessage.IsBodyHtml = false;
				mailMessage.Body = this.BodyPlainText;
			}
			foreach (object obj4 in this.Attachments)
			{
				Attachment attachment = (Attachment)obj4;
				Attachment attachment2 = new Attachment(new MemoryStream(attachment.GetData()), attachment.Name);
				try
				{
					attachment2.ContentType.Name = attachment.Name;
					attachment2.ContentType.MediaType = attachment.ContentType;
				}
				catch (FormatException)
				{
				}
				string[] array2 = attachment.Headers.a("Content-Type").RawBody.c().Split(new char[]
				{
					':'
				}, 2);
				if (array2.Length > 1)
				{
					string text2 = array2[1].Trim();
					int num = text2.IndexOf("=?");
					if (num != -1)
					{
						int num2 = text2.Substring(num + 2).IndexOf('?');
						if (num2 != -1)
						{
							attachment2.NameEncoding = Encoding.GetEncoding(text2.Substring(num + 2, num2));
						}
					}
				}
				if (attachment.ContentID != string.Empty)
				{
					attachment2.ContentId = attachment.ContentID;
				}
				mailMessage.Attachments.Add(attachment2);
			}
			return mailMessage;
		}

		// Token: 0x06002C5D RID: 11357 RVA: 0x000D3358 File Offset: 0x000D2358
		private string a(MailMessage A_0, bool A_1)
		{
			string text = A_1 ? "text/html" : "text/plain";
			foreach (AlternateView alternateView in A_0.AlternateViews)
			{
				if (alternateView.ContentType != null && alternateView.ContentType.MediaType != null && alternateView.ContentType.MediaType.ToLower() == text)
				{
					alternateView.ContentStream.Position = 0L;
					byte[] array = new byte[alternateView.ContentStream.Length];
					alternateView.ContentStream.Read(array, 0, array.Length);
					Encoding encoding = null;
					if (alternateView.ContentType.CharSet == null || alternateView.ContentType.CharSet == string.Empty)
					{
						encoding = Global.DefaultEncoding;
					}
					else
					{
						try
						{
							encoding = Encoding.GetEncoding(alternateView.ContentType.CharSet);
							this.Charset = alternateView.ContentType.CharSet;
						}
						catch (ArgumentException)
						{
							encoding = Global.DefaultEncoding;
						}
					}
					return encoding.GetString(array);
				}
			}
			return string.Empty;
		}

		// Token: 0x06002C5E RID: 11358 RVA: 0x000D349C File Offset: 0x000D249C
		public void ConvertFromSystemNetMail(MailMessage msg)
		{
			for (int i = this.Headers.Count - 1; i >= 0; i--)
			{
				this.Headers.RemoveAt(i);
			}
			for (int j = this.Attachments.Count - 1; j >= 0; j--)
			{
				this.Attachments.RemoveAt(j);
			}
			if (msg.Headers.Count > 0)
			{
				for (int k = 0; k < msg.Headers.Count; k++)
				{
					string key = msg.Headers.GetKey(k);
					foreach (string value in msg.Headers.GetValues(k))
					{
						this.Headers.Add(key, value, true);
					}
				}
			}
			else
			{
				if (msg.From != null)
				{
					this.From.AsString = msg.From.ToString();
				}
				this.To.AsString = msg.To.ToString();
				this.Cc.AsString = msg.CC.ToString();
				this.Bcc.AsString = msg.Bcc.ToString();
				this.Subject = msg.Subject;
				switch (msg.Priority)
				{
				case MailPriority.Normal:
					this.Priority = MailPriority.Normal;
					break;
				case MailPriority.Low:
					this.Priority = MailPriority.Low;
					break;
				case MailPriority.High:
					this.Priority = MailPriority.High;
					break;
				default:
					this.Priority = MailPriority.None;
					break;
				}
			}
			this.Charset = global::a.i.h.b(msg.BodyEncoding);
			if (msg.IsBodyHtml)
			{
				if (msg.Body == null || msg.Body == string.Empty)
				{
					this.BodyHtmlText = this.a(msg, true);
				}
				else
				{
					this.BodyHtmlText = msg.Body;
				}
			}
			else if (msg.Body == null || msg.Body == string.Empty)
			{
				this.BodyPlainText = this.a(msg, false);
			}
			else
			{
				this.BodyPlainText = msg.Body;
			}
			foreach (Attachment attachment in msg.Attachments)
			{
				this.Attachments.Add(attachment.ContentStream, attachment.Name, null, attachment.ContentType.MediaType, null, NewAttachmentOptions.None, MailTransferEncoding.Base64);
			}
			if (this.Headers["MIME-Version"] == null)
			{
				this.Headers.Add("MIME-Version", "1.0", true);
			}
			if (this.Headers["X-Mailer"] == null)
			{
				this.Headers.Add("X-Mailer", string.Format(CultureInfo.InvariantCulture, "MailBee.NET {0}", new object[]
				{
					this.Version
				}), true);
			}
			if (this.Headers["Content-Type"] == null)
			{
				if (this.BodyHtmlText != null && this.BodyHtmlText != string.Empty && this.p["text/html"] == null)
				{
					this.Headers.Add("Content-Type", "text/html", true);
					return;
				}
				this.Headers.Add("Content-Type", "text/plain", true);
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06002C5F RID: 11359 RVA: 0x000D37D4 File Offset: 0x000D27D4
		// (set) Token: 0x06002C60 RID: 11360 RVA: 0x000D37DC File Offset: 0x000D27DC
		internal string FolderToDelete
		{
			get
			{
				return this.b;
			}
			set
			{
				this.b = value;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06002C61 RID: 11361 RVA: 0x000D37E5 File Offset: 0x000D27E5
		// (set) Token: 0x06002C62 RID: 11362 RVA: 0x000D37ED File Offset: 0x000D27ED
		internal bool FolderToDeleteCreated
		{
			get
			{
				return this.c;
			}
			set
			{
				this.c = value;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06002C63 RID: 11363 RVA: 0x000D37F8 File Offset: 0x000D27F8
		// (set) Token: 0x06002C64 RID: 11364 RVA: 0x000D384B File Offset: 0x000D284B
		internal bool NeedToRebuild
		{
			get
			{
				if (this.o.NeedToRebuild)
				{
					this.i = true;
				}
				if ((!this.i || this.m == null) && this.m != null && this.m.NeedToRebuild)
				{
					this.i = true;
				}
				return this.i;
			}
			set
			{
				this.o.NeedToRebuild = value;
				this.m.NeedToRebuild = value;
				this.i = value;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06002C65 RID: 11365 RVA: 0x000D386C File Offset: 0x000D286C
		// (set) Token: 0x06002C66 RID: 11366 RVA: 0x000D3874 File Offset: 0x000D2874
		internal bool NeedToReparse
		{
			get
			{
				return this.j;
			}
			set
			{
				this.j = value;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06002C67 RID: 11367 RVA: 0x000D387D File Offset: 0x000D287D
		// (set) Token: 0x06002C68 RID: 11368 RVA: 0x000D3885 File Offset: 0x000D2885
		internal ao RawBody
		{
			get
			{
				return this.k;
			}
			set
			{
				this.k = value;
				this.i = true;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06002C69 RID: 11369 RVA: 0x000D3895 File Offset: 0x000D2895
		internal MimePart MimePart
		{
			get
			{
				this.i();
				return this.l;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06002C6A RID: 11370 RVA: 0x000D38A3 File Offset: 0x000D28A3
		public MimePart MimePartTree
		{
			get
			{
				this.i();
				return this.l;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06002C6B RID: 11371 RVA: 0x000D38B1 File Offset: 0x000D28B1
		internal MimePartCollection MimeParts
		{
			get
			{
				this.i();
				return this.m;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06002C6C RID: 11372 RVA: 0x000D38C0 File Offset: 0x000D28C0
		// (set) Token: 0x06002C6D RID: 11373 RVA: 0x000D3934 File Offset: 0x000D2934
		public EmailAddressCollection Bcc
		{
			get
			{
				this.i();
				Header header = this.Headers.a("Bcc");
				if (header == null)
				{
					header = new Header("Bcc", string.Empty);
					this.Headers.b(header);
				}
				if (header.AddressCollection == null)
				{
					header.AddressCollection = EmailAddressCollection.Parse(header.ValueRawBody.c());
					header.AddressCollection.RecipientsHeader = header;
				}
				return header.AddressCollection;
			}
			set
			{
				if (value == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				this.i();
				Header header = this.Headers.a("Bcc");
				if (header != null)
				{
					header.Value = value.ToString();
				}
				else
				{
					header = new Header("Bcc", value.ToString());
					this.Headers.b(header);
					header.AddressCollection = value;
					value.RecipientsHeader = header;
				}
				this.i = true;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06002C6E RID: 11374 RVA: 0x000D39A8 File Offset: 0x000D29A8
		// (set) Token: 0x06002C6F RID: 11375 RVA: 0x000D3A1C File Offset: 0x000D2A1C
		public EmailAddressCollection Cc
		{
			get
			{
				this.i();
				Header header = this.Headers.a("Cc");
				if (header == null)
				{
					header = new Header("Cc", string.Empty);
					this.Headers.b(header);
				}
				if (header.AddressCollection == null)
				{
					header.AddressCollection = EmailAddressCollection.Parse(header.ValueRawBody.c());
					header.AddressCollection.RecipientsHeader = header;
				}
				return header.AddressCollection;
			}
			set
			{
				if (value == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				this.i();
				Header header = this.Headers.a("Cc");
				if (header != null)
				{
					header.Value = value.ToString();
				}
				else
				{
					header = new Header("Cc", value.ToString());
					this.Headers.b(header);
					header.AddressCollection = value;
					value.RecipientsHeader = header;
				}
				this.i = true;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06002C70 RID: 11376 RVA: 0x000D3A90 File Offset: 0x000D2A90
		// (set) Token: 0x06002C71 RID: 11377 RVA: 0x000D3AEC File Offset: 0x000D2AEC
		public string ConfirmRead
		{
			get
			{
				this.i();
				Header header = this.Headers.a("Disposition-Notification-To");
				if (header != null)
				{
					return this.f(header.Value);
				}
				header = this.Headers.a("X-Confirm-Reading-To");
				if (header != null)
				{
					return this.f(header.Value);
				}
				return string.Empty;
			}
			set
			{
				this.i();
				Header header = this.Headers.a("Disposition-Notification-To");
				if (header != null)
				{
					header.Value = value;
				}
				else
				{
					header = Header.a(string.Format("{0}: {1}", "Disposition-Notification-To", value));
					header.NeedToRebuild = true;
					this.Headers.b(header);
				}
				header = this.Headers.a("X-Confirm-Reading-To");
				if (header != null)
				{
					header.Value = value;
				}
				else
				{
					header = Header.a(string.Format("{0}: {1}", "X-Confirm-Reading-To", value));
					header.NeedToRebuild = true;
					this.Headers.b(header);
				}
				this.i = true;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06002C72 RID: 11378 RVA: 0x000D3B94 File Offset: 0x000D2B94
		// (set) Token: 0x06002C73 RID: 11379 RVA: 0x000D3BD0 File Offset: 0x000D2BD0
		public string ConfirmReceipt
		{
			get
			{
				this.i();
				Header header = this.Headers.a("Return-Receipt-To");
				if (header != null)
				{
					return this.f(header.Value);
				}
				return string.Empty;
			}
			set
			{
				this.i();
				Header header = this.Headers.a("Return-Receipt-To");
				if (header != null)
				{
					header.Value = value;
				}
				else
				{
					header = Header.a(string.Format("{0}: {1}", "Return-Receipt-To", value));
					header.NeedToRebuild = true;
					this.Headers.b(header);
				}
				this.i = true;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06002C74 RID: 11380 RVA: 0x000D3C34 File Offset: 0x000D2C34
		public string ContentType
		{
			get
			{
				this.i();
				Header header = this.Headers.a("Content-Type");
				if (header != null)
				{
					return this.f(header.Value);
				}
				return string.Empty;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06002C75 RID: 11381 RVA: 0x000D3C70 File Offset: 0x000D2C70
		// (set) Token: 0x06002C76 RID: 11382 RVA: 0x000D3CF8 File Offset: 0x000D2CF8
		public DateTime Date
		{
			get
			{
				this.i();
				Header header = this.Headers.a("Date");
				if (header != null)
				{
					try
					{
						if (this.ab.DatesAsUtc)
						{
							return global::a.i.k.a(header.Value, global::a.i.g.b);
						}
						return global::a.i.k.a(header.Value, global::a.i.g.a);
					}
					catch (MailBeeDateParsingException ex)
					{
						this.y = ex.ErrorCode;
						if (this.ae)
						{
							throw;
						}
						return DateTime.MinValue;
					}
				}
				return DateTime.MinValue;
			}
			set
			{
				this.i();
				Header header = this.Headers.a("Date");
				if (header != null)
				{
					header.Value = global::a.i.k.a(value);
				}
				else
				{
					this.Headers.Add("Date", global::a.i.k.a(value), false);
				}
				this.i = true;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06002C77 RID: 11383 RVA: 0x000D3D4C File Offset: 0x000D2D4C
		// (set) Token: 0x06002C78 RID: 11384 RVA: 0x000D3DD4 File Offset: 0x000D2DD4
		public DateTime DateSent
		{
			get
			{
				this.i();
				Header header = this.Headers.a("X-Date-Sent");
				if (header != null)
				{
					try
					{
						if (this.ab.DatesAsUtc)
						{
							return global::a.i.k.a(header.Value, global::a.i.g.b);
						}
						return global::a.i.k.a(header.Value, global::a.i.g.a);
					}
					catch (MailBeeDateParsingException ex)
					{
						this.y = ex.ErrorCode;
						if (this.ae)
						{
							throw;
						}
						return DateTime.MinValue;
					}
				}
				return DateTime.MinValue;
			}
			set
			{
				this.i();
				Header header = this.Headers.a("X-Date-Sent");
				if (header != null)
				{
					header.Value = global::a.i.k.a(value);
				}
				else
				{
					this.Headers.Add("X-Date-Sent", global::a.i.k.a(value), false);
				}
				this.i = true;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06002C79 RID: 11385 RVA: 0x000D3E28 File Offset: 0x000D2E28
		// (set) Token: 0x06002C7A RID: 11386 RVA: 0x000D3E64 File Offset: 0x000D2E64
		public string MessageID
		{
			get
			{
				this.i();
				Header header = this.Headers.a("Message-ID");
				if (header != null)
				{
					return this.f(header.Value);
				}
				return string.Empty;
			}
			set
			{
				this.i();
				string text = value;
				if (!text.StartsWith("<") || !text.EndsWith(">"))
				{
					text = string.Format(CultureInfo.InvariantCulture, "<{0}>", new object[]
					{
						text
					});
				}
				Header header = this.Headers.a("Message-ID");
				if (header != null)
				{
					header.Value = text;
				}
				else
				{
					this.Headers.Add("Message-ID", text, false);
				}
				this.i = true;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06002C7B RID: 11387 RVA: 0x000D3EE4 File Offset: 0x000D2EE4
		// (set) Token: 0x06002C7C RID: 11388 RVA: 0x000D3F20 File Offset: 0x000D2F20
		public string Organization
		{
			get
			{
				this.i();
				Header header = this.Headers.a("Organization");
				if (header != null)
				{
					return this.f(header.Value);
				}
				return string.Empty;
			}
			set
			{
				this.i();
				Header header = this.Headers.a("Organization");
				if (header != null)
				{
					header.Value = value;
				}
				else
				{
					this.Headers.Add("Organization", value, false);
				}
				this.i = true;
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06002C7D RID: 11389 RVA: 0x000D3F6C File Offset: 0x000D2F6C
		// (set) Token: 0x06002C7E RID: 11390 RVA: 0x000D3FA8 File Offset: 0x000D2FA8
		public string Sender
		{
			get
			{
				this.i();
				Header header = this.Headers.a("Sender");
				if (header != null)
				{
					return this.f(header.Value);
				}
				return string.Empty;
			}
			set
			{
				this.i();
				Header header = this.Headers.a("Sender");
				if (header != null)
				{
					header.Value = value;
				}
				else
				{
					this.Headers.Add("Sender", value, false);
				}
				this.i = true;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06002C7F RID: 11391 RVA: 0x000D3FF4 File Offset: 0x000D2FF4
		// (set) Token: 0x06002C80 RID: 11392 RVA: 0x000D4028 File Offset: 0x000D3028
		public MailPriority Priority
		{
			get
			{
				this.i();
				Header header = this.Headers.a("X-Priority");
				if (header != null)
				{
					return global::a.i.k.b(header.Value);
				}
				return MailPriority.None;
			}
			set
			{
				this.i();
				Header header = this.Headers.a("X-Priority");
				if (header != null)
				{
					header.Value = global::a.i.k.a(value, true);
				}
				else
				{
					this.Headers.Add("X-Priority", global::a.i.k.a(value, true), false);
				}
				this.i = true;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06002C81 RID: 11393 RVA: 0x000D4080 File Offset: 0x000D3080
		public string ReturnPath
		{
			get
			{
				this.i();
				Header header = this.Headers.a("Return-Path");
				if (header != null)
				{
					return this.f(header.Value);
				}
				return string.Empty;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06002C82 RID: 11394 RVA: 0x000D40BC File Offset: 0x000D30BC
		// (set) Token: 0x06002C83 RID: 11395 RVA: 0x000D40F8 File Offset: 0x000D30F8
		public string Subject
		{
			get
			{
				this.i();
				Header header = this.Headers.a("Subject");
				if (header != null)
				{
					return this.f(header.Value);
				}
				return string.Empty;
			}
			set
			{
				this.i();
				Header header = this.Headers.a("Subject");
				if (header != null)
				{
					header.Value = value;
				}
				else
				{
					this.Headers.Add("Subject", value, false);
				}
				this.i = true;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06002C84 RID: 11396 RVA: 0x000D4144 File Offset: 0x000D3144
		// (set) Token: 0x06002C85 RID: 11397 RVA: 0x000D41B8 File Offset: 0x000D31B8
		public EmailAddressCollection To
		{
			get
			{
				this.i();
				Header header = this.Headers.a("To");
				if (header == null)
				{
					header = new Header("To", string.Empty);
					this.Headers.b(header);
				}
				if (header.AddressCollection == null)
				{
					header.AddressCollection = EmailAddressCollection.Parse(header.ValueRawBody.c());
					header.AddressCollection.RecipientsHeader = header;
				}
				return header.AddressCollection;
			}
			set
			{
				if (value == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				this.i();
				Header header = this.Headers.a("To");
				if (header != null)
				{
					header.Value = value.ToString();
				}
				else
				{
					header = new Header("To", value.ToString());
					this.Headers.b(header);
					header.AddressCollection = value;
					value.RecipientsHeader = header;
				}
				this.i = true;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06002C86 RID: 11398 RVA: 0x000D422C File Offset: 0x000D322C
		// (set) Token: 0x06002C87 RID: 11399 RVA: 0x000D4268 File Offset: 0x000D3268
		public string XMailer
		{
			get
			{
				this.i();
				Header header = this.Headers.a("X-Mailer");
				if (header != null)
				{
					return this.f(header.Value);
				}
				return string.Empty;
			}
			set
			{
				this.i();
				Header header = this.Headers.a("X-Mailer");
				if (header != null)
				{
					header.Value = value;
				}
				else
				{
					this.Headers.Add("X-Mailer", value, false);
				}
				this.i = true;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06002C88 RID: 11400 RVA: 0x000D42B2 File Offset: 0x000D32B2
		public int SizeOnServer
		{
			get
			{
				return this.n;
			}
		}

		// Token: 0x06002C89 RID: 11401 RVA: 0x000D42BA File Offset: 0x000D32BA
		internal void b(int A_0)
		{
			this.n = A_0;
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06002C8A RID: 11402 RVA: 0x000D42C3 File Offset: 0x000D32C3
		public AttachmentCollection Attachments
		{
			get
			{
				this.i();
				return this.o;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06002C8B RID: 11403 RVA: 0x000D42D1 File Offset: 0x000D32D1
		// (set) Token: 0x06002C8C RID: 11404 RVA: 0x000D42F8 File Offset: 0x000D32F8
		public MailTransferEncoding MailTransferEncodingHtml
		{
			get
			{
				this.i();
				if (this.p.Html != null)
				{
					return this.p.Html.TransferEncoding;
				}
				return MailTransferEncoding.QuotedPrintable;
			}
			set
			{
				this.i();
				if (this.p.Html != null)
				{
					this.p.Html.TransferEncoding = value;
					if (this.m["text/html"] != null && this.m["text/html"].Headers["Content-Transfer-Encoding"] != null)
					{
						this.m["text/html"].Headers.a("Content-Transfer-Encoding").Value = global::a.i.h.a(this.p["text/html"].TransferEncoding);
					}
				}
				this.i = true;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06002C8D RID: 11405 RVA: 0x000D43A4 File Offset: 0x000D33A4
		// (set) Token: 0x06002C8E RID: 11406 RVA: 0x000D43CC File Offset: 0x000D33CC
		public MailTransferEncoding MailTransferEncodingPlain
		{
			get
			{
				this.i();
				if (this.p.Plain != null)
				{
					return this.p.Plain.TransferEncoding;
				}
				return MailTransferEncoding.QuotedPrintable;
			}
			set
			{
				this.i();
				if (this.p.Plain != null)
				{
					this.p.Plain.TransferEncoding = value;
					if (this.m["text/plain"] != null && this.m["text/plain"].Headers["Content-Transfer-Encoding"] != null)
					{
						this.m["text/plain"].Headers.a("Content-Transfer-Encoding").Value = global::a.i.h.a(this.p["text/plain"].TransferEncoding);
					}
				}
				this.i = true;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06002C8F RID: 11407 RVA: 0x000D4478 File Offset: 0x000D3478
		// (set) Token: 0x06002C90 RID: 11408 RVA: 0x000D44AC File Offset: 0x000D34AC
		public string BodyHtmlText
		{
			get
			{
				this.i();
				if (this.p.Html != null)
				{
					return this.f(this.p.Html.Text);
				}
				return string.Empty;
			}
			set
			{
				this.i();
				if (this.p.Html == null)
				{
					this.p.Add(new TextBodyPart("text/html"));
					this.p.Html.TransferEncoding = MailTransferEncoding.QuotedPrintable;
				}
				this.p.Html.Text = value;
				this.p.Html.NeedToRebuild = true;
				this.i = true;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06002C91 RID: 11409 RVA: 0x000D451C File Offset: 0x000D351C
		// (set) Token: 0x06002C92 RID: 11410 RVA: 0x000D4550 File Offset: 0x000D3550
		public string BodyPlainText
		{
			get
			{
				this.i();
				if (this.p.Plain != null)
				{
					return this.f(this.p.Plain.Text);
				}
				return string.Empty;
			}
			set
			{
				this.i();
				if (this.p.Plain == null)
				{
					this.p.c(new TextBodyPart("text/plain"));
					this.p["text/plain"].TransferEncoding = MailTransferEncoding.QuotedPrintable;
				}
				this.p.Plain.Text = value;
				this.p.Plain.NeedToRebuild = true;
				this.i = true;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06002C93 RID: 11411 RVA: 0x000D45C5 File Offset: 0x000D35C5
		public TextBodyPartCollection BodyParts
		{
			get
			{
				this.i();
				return this.p;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06002C94 RID: 11412 RVA: 0x000D45D3 File Offset: 0x000D35D3
		// (set) Token: 0x06002C95 RID: 11413 RVA: 0x000D45E4 File Offset: 0x000D35E4
		public string Charset
		{
			get
			{
				this.i();
				return this.q;
			}
			set
			{
				this.i();
				if (value == null)
				{
					value = string.Empty;
				}
				this.q = value;
				foreach (object obj in this.p)
				{
					((TextBodyPart)obj).CharsetInternal = value;
				}
				this.i = true;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06002C96 RID: 11414 RVA: 0x000D465C File Offset: 0x000D365C
		public DateTime DateReceived
		{
			get
			{
				this.i();
				if (this.af.Count > 0)
				{
					return this.af[0].Date;
				}
				return DateTime.MinValue;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06002C97 RID: 11415 RVA: 0x000D468C File Offset: 0x000D368C
		// (set) Token: 0x06002C98 RID: 11416 RVA: 0x000D4720 File Offset: 0x000D3720
		public EmailAddress From
		{
			get
			{
				this.i();
				Header header = this.Headers.a("From");
				if (header == null)
				{
					header = new Header("From", string.Empty);
					this.Headers.b(header);
				}
				if (header.Address == null)
				{
					EmailAddressCollection emailAddressCollection = EmailAddressCollection.Parse(header.ValueRawBody.c());
					if (emailAddressCollection.Count > 0)
					{
						header.Address = emailAddressCollection[0];
					}
					else
					{
						header.Address = new EmailAddress();
					}
					header.Address.EmailAddressHeader = header;
				}
				return header.Address;
			}
			set
			{
				if (value == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				this.i();
				Header header = this.Headers.a("From");
				if (header != null)
				{
					header.Value = value.ToString();
				}
				else
				{
					header = new Header("From", value.ToString());
					this.Headers.b(header);
					header.Address = value;
					value.EmailAddressHeader = header;
				}
				this.i = true;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06002C99 RID: 11417 RVA: 0x000D4794 File Offset: 0x000D3794
		public bool HasAttachments
		{
			get
			{
				this.i();
				if (this.u)
				{
					return this.o.Count > 0;
				}
				return this.ContentType.ToLower().IndexOf("multipart/") != -1 && string.Compare(this.ContentType, "multipart/alternative", true) != 0;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06002C9A RID: 11418 RVA: 0x000D47F0 File Offset: 0x000D37F0
		// (set) Token: 0x06002C9B RID: 11419 RVA: 0x000D4824 File Offset: 0x000D3824
		public MailPriority Importance
		{
			get
			{
				this.i();
				Header header = this.Headers.a("Importance");
				if (header != null)
				{
					return global::a.i.k.b(header.Value);
				}
				return MailPriority.None;
			}
			set
			{
				this.i();
				Header header = this.Headers.a("Importance");
				if (header != null)
				{
					header.Value = global::a.i.k.a(value, false);
				}
				else
				{
					this.Headers.Add("Importance", global::a.i.k.a(value, false), false);
				}
				this.i = true;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06002C9C RID: 11420 RVA: 0x000D487A File Offset: 0x000D387A
		public int IndexOnServer
		{
			get
			{
				return this.s;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06002C9D RID: 11421 RVA: 0x000D4882 File Offset: 0x000D3882
		// (set) Token: 0x06002C9E RID: 11422 RVA: 0x000D488A File Offset: 0x000D388A
		internal int IndexOnServerInternal
		{
			get
			{
				return this.s;
			}
			set
			{
				this.s = value;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06002C9F RID: 11423 RVA: 0x000D4893 File Offset: 0x000D3893
		public object UidOnServer
		{
			get
			{
				return this.t;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06002CA0 RID: 11424 RVA: 0x000D489B File Offset: 0x000D389B
		// (set) Token: 0x06002CA1 RID: 11425 RVA: 0x000D48A3 File Offset: 0x000D38A3
		internal object UidOnServerInternal
		{
			get
			{
				return this.t;
			}
			set
			{
				this.t = value;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06002CA2 RID: 11426 RVA: 0x000D48AC File Offset: 0x000D38AC
		public bool IsEncrypted
		{
			get
			{
				this.i();
				Header header = this.Headers.a("Content-Type");
				if (header != null && header.HeaderParameters != null)
				{
					global::a.i.n n = header.HeaderParameters.b("smime-type");
					if (n != null)
					{
						return n.c().ToLower() == "enveloped-data";
					}
				}
				string text = this.ContentType.ToLower();
				return text == "application/pkcs7-mime" || text == "application/x-pkcs7-mime";
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06002CA3 RID: 11427 RVA: 0x000D4937 File Offset: 0x000D3937
		public bool IsEntire
		{
			get
			{
				this.i();
				return this.u;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (set) Token: 0x06002CA4 RID: 11428 RVA: 0x000D4945 File Offset: 0x000D3945
		internal bool IsEntireInternal
		{
			set
			{
				this.u = value;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06002CA5 RID: 11429 RVA: 0x000D494E File Offset: 0x000D394E
		public bool IsSigned
		{
			get
			{
				if (this.v)
				{
					return true;
				}
				this.i();
				return Smime.b(this) || Smime.c(this);
			}
		}

		// Token: 0x06002CA6 RID: 11430 RVA: 0x000D4975 File Offset: 0x000D3975
		internal void z()
		{
			this.v = true;
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06002CA7 RID: 11431 RVA: 0x000D497E File Offset: 0x000D397E
		// (set) Token: 0x06002CA8 RID: 11432 RVA: 0x000D4986 File Offset: 0x000D3986
		internal bool AttachedSignatureVerified
		{
			get
			{
				return this.w;
			}
			set
			{
				this.w = value;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06002CA9 RID: 11433 RVA: 0x000D498F File Offset: 0x000D398F
		// (set) Token: 0x06002CAA RID: 11434 RVA: 0x000D4997 File Offset: 0x000D3997
		internal MessageVerificationFlags AttachedSignatureVerificationResult
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06002CAB RID: 11435 RVA: 0x000D49A0 File Offset: 0x000D39A0
		public int LastResult
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06002CAC RID: 11436 RVA: 0x000D49A8 File Offset: 0x000D39A8
		public MailMerge Merge
		{
			get
			{
				return this.z;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06002CAD RID: 11437 RVA: 0x000D49B0 File Offset: 0x000D39B0
		// (set) Token: 0x06002CAE RID: 11438 RVA: 0x000D49B8 File Offset: 0x000D39B8
		public MessageBuilderConfig Builder
		{
			get
			{
				return this.aa;
			}
			set
			{
				if (value == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				this.aa = value;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06002CAF RID: 11439 RVA: 0x000D49CC File Offset: 0x000D39CC
		// (set) Token: 0x06002CB0 RID: 11440 RVA: 0x000D49D4 File Offset: 0x000D39D4
		public MessageParserConfig Parser
		{
			get
			{
				return this.ab;
			}
			set
			{
				if (value == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				this.ab = value;
				this.ab.Message = this;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06002CB1 RID: 11441 RVA: 0x000D49F4 File Offset: 0x000D39F4
		internal string MessageFolder
		{
			get
			{
				return this.ab.WorkingFolder;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06002CB2 RID: 11442 RVA: 0x000D4A01 File Offset: 0x000D3A01
		public int PartCount
		{
			get
			{
				this.i();
				return this.ac;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06002CB3 RID: 11443 RVA: 0x000D4A0F File Offset: 0x000D3A0F
		public int PartIndex
		{
			get
			{
				this.i();
				return this.ad;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06002CB4 RID: 11444 RVA: 0x000D4A20 File Offset: 0x000D3A20
		public string RawHeader
		{
			get
			{
				this.i();
				if (this.ab.HeadersAsHtml)
				{
					return global::a.i.b.k(this.f(this.Headers.RawHeaders.c()));
				}
				return this.f(this.Headers.RawHeaders.c());
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06002CB5 RID: 11445 RVA: 0x000D4A74 File Offset: 0x000D3A74
		// (set) Token: 0x06002CB6 RID: 11446 RVA: 0x000D4AB0 File Offset: 0x000D3AB0
		public string References
		{
			get
			{
				this.i();
				Header header = this.Headers.a("References");
				if (header != null)
				{
					return this.f(header.Value);
				}
				return string.Empty;
			}
			set
			{
				this.i();
				Header header = this.Headers.a("References");
				if (header != null)
				{
					header.Value = value;
				}
				else
				{
					this.Headers.Add("References", value, false);
				}
				this.i = true;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06002CB7 RID: 11447 RVA: 0x000D4AFC File Offset: 0x000D3AFC
		// (set) Token: 0x06002CB8 RID: 11448 RVA: 0x000D4B70 File Offset: 0x000D3B70
		public EmailAddressCollection ReplyTo
		{
			get
			{
				this.i();
				Header header = this.Headers.a("Reply-To");
				if (header == null)
				{
					header = new Header("Reply-To", string.Empty);
					this.Headers.b(header);
				}
				if (header.AddressCollection == null)
				{
					header.AddressCollection = EmailAddressCollection.Parse(header.ValueRawBody.c());
					header.AddressCollection.RecipientsHeader = header;
				}
				return header.AddressCollection;
			}
			set
			{
				if (value == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				this.i();
				Header header = this.Headers.a("Reply-To");
				if (header != null)
				{
					header.Value = value.ToString();
				}
				else
				{
					header = new Header("Reply-To", value.ToString());
					this.Headers.b(header);
					header.AddressCollection = value;
					value.RecipientsHeader = header;
				}
				this.i = true;
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06002CB9 RID: 11449 RVA: 0x000D4BE4 File Offset: 0x000D3BE4
		// (set) Token: 0x06002CBA RID: 11450 RVA: 0x000D4C18 File Offset: 0x000D3C18
		public MailSensitivity Sensitivity
		{
			get
			{
				this.i();
				Header header = this.Headers.a("Sensitivity");
				if (header != null)
				{
					return global::a.i.k.c(header.Value);
				}
				return MailSensitivity.None;
			}
			set
			{
				this.i();
				Header header = this.Headers.a("Sensitivity");
				if (header != null)
				{
					header.Value = global::a.i.k.a(value);
				}
				else
				{
					this.Headers.Add("Sensitivity", global::a.i.k.a(value), false);
				}
				this.i = true;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06002CBB RID: 11451 RVA: 0x000D4C6C File Offset: 0x000D3C6C
		public int Size
		{
			get
			{
				this.h();
				return this.k.e();
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06002CBC RID: 11452 RVA: 0x000D4C7F File Offset: 0x000D3C7F
		// (set) Token: 0x06002CBD RID: 11453 RVA: 0x000D4C87 File Offset: 0x000D3C87
		public bool ThrowExceptions
		{
			get
			{
				return this.ae;
			}
			set
			{
				this.ae = value;
				this.o.ThrowExceptionsInternal = value;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06002CBE RID: 11454 RVA: 0x000D4C9C File Offset: 0x000D3C9C
		public TimeStampCollection TimeStamps
		{
			get
			{
				this.i();
				return this.af;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06002CBF RID: 11455 RVA: 0x000D4CAA File Offset: 0x000D3CAA
		public string Version
		{
			get
			{
				return Global.Version;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06002CC0 RID: 11456 RVA: 0x000D4CB1 File Offset: 0x000D3CB1
		public HeaderCollection Headers
		{
			get
			{
				this.i();
				return this.l.Headers;
			}
		}

		// Token: 0x06002CC1 RID: 11457 RVA: 0x000D4CC4 File Offset: 0x000D3CC4
		public void Dispose()
		{
			this.b(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002CC2 RID: 11458 RVA: 0x000D4CD3 File Offset: 0x000D3CD3
		private void b(bool A_0)
		{
			if (!this.r)
			{
				this.a(A_0);
			}
			this.r = true;
		}

		// Token: 0x06002CC3 RID: 11459 RVA: 0x000D4CEE File Offset: 0x000D3CEE
		public MailMessage()
		{
			this.Reset();
		}

		// Token: 0x06002CC4 RID: 11460 RVA: 0x000D4D24 File Offset: 0x000D3D24
		internal MailMessage(byte[] A_0)
		{
			this.a(new ao(A_0));
		}

		// Token: 0x06002CC5 RID: 11461 RVA: 0x000D4D60 File Offset: 0x000D3D60
		internal MailMessage(ao A_0)
		{
			this.a(A_0);
		}

		// Token: 0x06002CC6 RID: 11462 RVA: 0x000D4D98 File Offset: 0x000D3D98
		internal MailMessage(ao A_0, bool A_1)
		{
			this.ab = new MessageParserConfig(this);
			this.Parser.ParseHeaderOnly = A_1;
			this.a(A_0);
		}

		// Token: 0x06002CC7 RID: 11463 RVA: 0x000D4DF4 File Offset: 0x000D3DF4
		~MailMessage()
		{
			this.b(false);
		}

		// Token: 0x06002CC8 RID: 11464 RVA: 0x000D4E24 File Offset: 0x000D3E24
		public void AppendChunk(byte[] nextChunk)
		{
			if (nextChunk == null)
			{
				this.y = 21;
				throw new MailBeeInvalidArgumentException(this.y);
			}
			this.y = 0;
			this.NeedToRebuild = false;
			byte[] array = new byte[this.k.e() + nextChunk.Length];
			Buffer.BlockCopy(this.k.c(), 0, array, 0, this.k.e());
			Buffer.BlockCopy(nextChunk, 0, array, this.k.e(), nextChunk.Length);
			this.k = new ao(array);
			this.j = true;
		}

		// Token: 0x06002CC9 RID: 11465 RVA: 0x000D4EB4 File Offset: 0x000D3EB4
		public bool AppendPartialMessage(MailMessage nextPart)
		{
			if (nextPart == null)
			{
				this.y = 21;
				throw new MailBeeInvalidArgumentException(this.y);
			}
			if (string.Compare(nextPart.ContentType, "message/partial", true) != 0)
			{
				this.y = 20;
				throw new MailBeeInvalidArgumentException(this.y);
			}
			if (string.Compare(this.ContentType, "message/partial", true) != 0)
			{
				this.y = 11;
				throw new MailBeeInvalidStateException(this.y);
			}
			this.y = 0;
			this.AppendChunk(nextPart.MimePartTree.PartValueAsBytes);
			this.ad = nextPart.PartIndex;
			this.ac = nextPart.PartCount;
			if (this.ad == this.ac)
			{
				this.a(MimePart.a(this.RawBody, this).PartValue);
				return true;
			}
			return false;
		}

		// Token: 0x06002CCA RID: 11466 RVA: 0x000D4F80 File Offset: 0x000D3F80
		public void Clear(MessageElements elements)
		{
			this.y = 0;
			this.i();
			if ((elements & MessageElements.Attachments) == MessageElements.Attachments)
			{
				this.o.Clear();
			}
			if ((elements & MessageElements.CustomHeaders) == MessageElements.CustomHeaders)
			{
				this.Headers.RemoveCustomHeaders();
			}
			if ((elements & MessageElements.Recipients) == MessageElements.Recipients)
			{
				Header header = this.Headers.a("To");
				if (header != null && header.AddressCollection != null)
				{
					header.AddressCollection.Clear();
				}
				header = this.Headers.a("Bcc");
				if (header != null && header.AddressCollection != null)
				{
					header.AddressCollection.Clear();
				}
				header = this.Headers.a("Cc");
				if (header != null && header.AddressCollection != null)
				{
					header.AddressCollection.Clear();
				}
			}
			if ((elements & MessageElements.RouteHeaders) == MessageElements.RouteHeaders)
			{
				this.Headers.RemoveRouteHeaders();
			}
			if ((elements & MessageElements.RawBody) == MessageElements.RawBody)
			{
				this.k = new ao(new byte[0]);
				this.NeedToRebuild = true;
			}
		}

		// Token: 0x06002CCB RID: 11467 RVA: 0x000D5068 File Offset: 0x000D4068
		public MailMessage Clone()
		{
			this.y = 0;
			this.h();
			MailMessage mailMessage = new MailMessage();
			byte[] array = new byte[this.k.e()];
			Buffer.BlockCopy(this.k.c(), 0, array, 0, array.Length);
			mailMessage.RawBody = new ao(array);
			mailMessage.b = this.b;
			mailMessage.c = this.c;
			mailMessage.s = this.s;
			mailMessage.z = this.z.a(mailMessage);
			mailMessage.aa = this.aa.a(mailMessage);
			mailMessage.ab = this.ab.a(mailMessage);
			mailMessage.n = this.n;
			mailMessage.ae = this.ae;
			mailMessage.t = this.t;
			mailMessage.Parser.Apply();
			return mailMessage;
		}

		// Token: 0x06002CCC RID: 11468 RVA: 0x000D5148 File Offset: 0x000D4148
		internal MailMessage w()
		{
			MailMessage mailMessage = new MailMessage();
			if (this.Headers.Count > 0)
			{
				mailMessage.Headers.Clear();
			}
			foreach (object obj in this.Headers)
			{
				Header header = (Header)obj;
				string text = header.Name.ToLower();
				if (text == "from")
				{
					mailMessage.From = this.From;
				}
				else
				{
					mailMessage.Headers.b(header.i());
				}
			}
			mailMessage.q = this.q;
			if (this.BodyParts.Count > 0)
			{
				mailMessage.BodyParts.Clear();
			}
			mailMessage.BodyPlainText = this.BodyPlainText;
			mailMessage.BodyHtmlText = this.BodyHtmlText;
			if (this.Attachments.Count > 0)
			{
				mailMessage.Attachments.Clear();
			}
			foreach (object obj2 in this.Attachments)
			{
				Attachment attachment = (Attachment)obj2;
				mailMessage.Attachments.Add(attachment.a());
			}
			mailMessage.b = this.b;
			mailMessage.c = this.c;
			mailMessage.s = this.s;
			mailMessage.z = this.z.a(mailMessage);
			mailMessage.aa = this.aa.a(mailMessage);
			mailMessage.ab = this.ab.a(mailMessage);
			mailMessage.n = this.n;
			mailMessage.ae = this.ae;
			mailMessage.t = this.t;
			return mailMessage;
		}

		// Token: 0x06002CCD RID: 11469 RVA: 0x000D5324 File Offset: 0x000D4324
		public bool Deserialize(XmlReader xmlReader)
		{
			if (xmlReader == null)
			{
				this.y = 21;
				throw new MailBeeInvalidArgumentException(this.y);
			}
			this.y = 0;
			this.Reset();
			try
			{
				xmlReader.MoveToContent();
				xmlReader.ReadStartElement("MailMessage");
				this.l = MimePart.b(xmlReader, this);
				xmlReader.ReadEndElement();
				this.NeedToRebuild = true;
				this.f();
			}
			catch (XmlException a_)
			{
				this.y = 33;
				if (this.ae)
				{
					throw new MailBeeIOException(33, a_);
				}
				return false;
			}
			return true;
		}

		// Token: 0x06002CCE RID: 11470 RVA: 0x000D53BC File Offset: 0x000D43BC
		public bool Deserialize(string filename)
		{
			if (filename == null || filename == string.Empty)
			{
				this.y = 22;
				throw new MailBeeInvalidArgumentException(this.y);
			}
			this.y = 0;
			bool result = false;
			XmlReader xmlReader = null;
			try
			{
				xmlReader = XmlReader.Create(filename, new XmlReaderSettings
				{
					IgnoreWhitespace = true
				});
				result = this.Deserialize(xmlReader);
			}
			catch (XmlException a_)
			{
				this.y = 33;
				if (this.ae)
				{
					throw new MailBeeIOException(33, a_);
				}
				return false;
			}
			finally
			{
				if (xmlReader != null)
				{
					xmlReader.Close();
				}
			}
			return result;
		}

		// Token: 0x06002CCF RID: 11471 RVA: 0x000D5460 File Offset: 0x000D4460
		public void EncodeAllHeaders(Encoding targetEncoding, HeaderEncodingOptions options)
		{
			if (targetEncoding == null)
			{
				this.y = 21;
				throw new MailBeeInvalidArgumentException(this.y);
			}
			this.y = 0;
			this.i();
			global::a.i.h.a(this.m, options, global::a.i.h.b(targetEncoding), global::a.i.k.a(this.aa.AddressDelimeter));
		}

		// Token: 0x06002CD0 RID: 11472 RVA: 0x000D54B4 File Offset: 0x000D44B4
		public MailMessage ForwardAsAttachment()
		{
			this.y = 0;
			this.h();
			return new MailMessage
			{
				Attachments = 
				{
					{
						this.k.c(),
						this.Subject + ".eml",
						null,
						"message/rfc822",
						null,
						NewAttachmentOptions.Inline,
						MailTransferEncoding.None
					}
				}
			};
		}

		// Token: 0x06002CD1 RID: 11473 RVA: 0x000D5504 File Offset: 0x000D4504
		public EmailAddressCollection GetAllRecipients()
		{
			this.y = 0;
			this.i();
			EmailAddressCollection emailAddressCollection = new EmailAddressCollection();
			Header header = this.Headers.a("To");
			if (header != null)
			{
				if (header.AddressCollection == null)
				{
					header.AddressCollection = EmailAddressCollection.Parse(header.Value);
					header.AddressCollection.RecipientsHeader = header;
				}
				foreach (object obj in header.AddressCollection)
				{
					EmailAddress address = (EmailAddress)obj;
					emailAddressCollection.Add(address);
				}
			}
			header = this.Headers.a("Cc");
			if (header != null)
			{
				if (header.AddressCollection == null)
				{
					header.AddressCollection = EmailAddressCollection.Parse(header.Value);
					header.AddressCollection.RecipientsHeader = header;
				}
				foreach (object obj2 in header.AddressCollection)
				{
					EmailAddress address2 = (EmailAddress)obj2;
					emailAddressCollection.Add(address2);
				}
			}
			header = this.Headers.a("Bcc");
			if (header != null)
			{
				if (header.AddressCollection == null)
				{
					header.AddressCollection = EmailAddressCollection.Parse(header.Value);
					header.AddressCollection.RecipientsHeader = header;
				}
				foreach (object obj3 in header.AddressCollection)
				{
					EmailAddress address3 = (EmailAddress)obj3;
					emailAddressCollection.Add(address3);
				}
			}
			return emailAddressCollection;
		}

		// Token: 0x06002CD2 RID: 11474 RVA: 0x000D56BC File Offset: 0x000D46BC
		public static string GetEncodedHeaderValue(string headerName, string headerValue, Encoding targetEncoding, HeaderEncodingOptions options)
		{
			if (headerName == null || headerValue == null || targetEncoding == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			MailTransferEncoding a_ = MailTransferEncoding.QuotedPrintable;
			if ((options & HeaderEncodingOptions.Base64) == HeaderEncodingOptions.Base64)
			{
				a_ = MailTransferEncoding.Base64;
			}
			return global::a.i.h.a(headerName, headerValue, a_, global::a.i.h.b(targetEncoding), options, true);
		}

		// Token: 0x06002CD3 RID: 11475 RVA: 0x000D56F4 File Offset: 0x000D46F4
		public string GetHtmlAndSaveRelatedFiles()
		{
			this.y = 0;
			return global::a.i.e.b(this.ab.WorkingFolder, VirtualMappingType.NonWeb, MessageFolderBehavior.CreateOnly, this, this.ab, false);
		}

		// Token: 0x06002CD4 RID: 11476 RVA: 0x000D5717 File Offset: 0x000D4717
		public string GetHtmlWithBase64EncodedRelatedFiles()
		{
			this.y = 0;
			return global::a.i.e.b(this.ab.WorkingFolder, VirtualMappingType.Base64, MessageFolderBehavior.CreateOnly, this, this.ab, false);
		}

		// Token: 0x06002CD5 RID: 11477 RVA: 0x000D573A File Offset: 0x000D473A
		public string GetHtmlAndSaveRelatedFiles(string virtualPath, VirtualMappingType mappingType, MessageFolderBehavior folderMode)
		{
			if (mappingType == VirtualMappingType.NonWeb)
			{
				virtualPath = this.ab.WorkingFolder;
			}
			else if (virtualPath == null)
			{
				this.y = 21;
				throw new MailBeeInvalidArgumentException(this.y);
			}
			this.y = 0;
			return this.b(virtualPath, mappingType, folderMode, false);
		}

		// Token: 0x06002CD6 RID: 11478 RVA: 0x000D5778 File Offset: 0x000D4778
		private string b(string A_0, VirtualMappingType A_1, MessageFolderBehavior A_2, bool A_3)
		{
			bool headersAsHtml = this.ab.HeadersAsHtml;
			this.ab.HeadersAsHtmlInternal = false;
			string result = null;
			try
			{
				result = global::a.i.e.b(A_0, A_1, A_2, this, this.ab, A_3);
			}
			finally
			{
				this.ab.HeadersAsHtmlInternal = headersAsHtml;
			}
			return result;
		}

		// Token: 0x06002CD7 RID: 11479 RVA: 0x000D57D4 File Offset: 0x000D47D4
		public string GetHtmlAndRelatedFilesInMemory(string virtualPathPrefix)
		{
			return this.GetHtmlAndSaveRelatedFiles(virtualPathPrefix, VirtualMappingType.StaticInMemory, MessageFolderBehavior.DoNotCreate);
		}

		// Token: 0x06002CD8 RID: 11480 RVA: 0x000D57DF File Offset: 0x000D47DF
		public byte[] GetMessageRawData()
		{
			this.y = 0;
			this.h();
			return this.k.c();
		}

		// Token: 0x06002CD9 RID: 11481 RVA: 0x000D57F9 File Offset: 0x000D47F9
		internal ao n()
		{
			this.y = 0;
			this.h();
			return this.k;
		}

		// Token: 0x06002CDA RID: 11482 RVA: 0x000D5810 File Offset: 0x000D4810
		public bool ImportRelatedFiles(ImportRelatedFilesOptions options)
		{
			this.y = 0;
			try
			{
				bool a_ = false;
				string bodyHtmlText = this.BodyHtmlText;
				if ((options & ImportRelatedFilesOptions.ImportFromUris) == ImportRelatedFilesOptions.ImportFromUris)
				{
					a_ = true;
				}
				foreach (object obj in global::a.i.b.a(ref bodyHtmlText, this.aa.RelatedFilesFolder, a_, this.aa.OnReplaceUriWithCid))
				{
					global::a.i.a a = (global::a.i.a)obj;
					string text = (a.d() != null) ? ap.f(a.d()) : ap.f(Path.GetFileName(ap.g(a.c())));
					this.Attachments.Add(a.b(), text, a.a(), global::a.i.k.e(Path.GetExtension(text)), null, NewAttachmentOptions.Inline, MailTransferEncoding.Base64);
				}
				this.BodyHtmlText = bodyHtmlText;
			}
			catch (MailBeeException ex)
			{
				this.y = ex.ErrorCode;
				if (this.ae)
				{
					throw;
				}
				return false;
			}
			return true;
		}

		// Token: 0x06002CDB RID: 11483 RVA: 0x000D5928 File Offset: 0x000D4928
		public bool IsBodyAvail(string bodyFormat, bool originalBodyOnly)
		{
			if (bodyFormat == null)
			{
				this.y = 21;
				throw new MailBeeInvalidArgumentException(this.y);
			}
			this.y = 0;
			this.i();
			bool result = false;
			foreach (object obj in this.p)
			{
				TextBodyPart textBodyPart = (TextBodyPart)obj;
				if ((textBodyPart.IsOriginal || !originalBodyOnly) && string.Compare(textBodyPart.Headers["Content-Type"], bodyFormat, true) == 0)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x06002CDC RID: 11484 RVA: 0x000D59CC File Offset: 0x000D49CC
		public bool LoadBodyText(string path, MessageBodyType bodyType, Encoding sourceEncoding, ImportBodyOptions options)
		{
			if (path == null || path == string.Empty)
			{
				this.y = 22;
				throw new MailBeeInvalidArgumentException(this.y);
			}
			this.y = 0;
			global::a.i.i i = new global::a.i.i();
			bool flag = false;
			if ((options & ImportBodyOptions.PathIsUri) == ImportBodyOptions.PathIsUri)
			{
				try
				{
					i.a(global::a.i.b.d(path));
					goto IL_9E;
				}
				catch (MailBeeWebException ex)
				{
					this.y = ex.ErrorCode;
					if (this.ae)
					{
						throw;
					}
					return false;
				}
			}
			try
			{
				i.a(ap.b(path, true, 0).c());
			}
			catch (MailBeeIOException ex2)
			{
				this.y = ex2.ErrorCode;
				if (this.ae)
				{
					throw;
				}
				return false;
			}
			IL_9E:
			if ((options & ImportBodyOptions.Append) == ImportBodyOptions.Append)
			{
				flag = true;
			}
			if (bodyType != MessageBodyType.Plain)
			{
				if (bodyType == MessageBodyType.Html)
				{
					if (sourceEncoding == null || (options & ImportBodyOptions.PreferCharsetFromMetaTag) > ImportBodyOptions.None)
					{
						Encoding encoding = bb.b(i.c());
						if (encoding == null)
						{
							if (sourceEncoding == null)
							{
								sourceEncoding = Global.DefaultEncoding;
							}
						}
						else
						{
							sourceEncoding = encoding;
						}
					}
					if (sourceEncoding != Global.DefaultEncoding)
					{
						i.a(sourceEncoding.GetString(i.g(), 0, i.g().Length));
					}
					this.BodyHtmlText = (flag ? (this.BodyHtmlText + i.c()) : i.c());
					this.BodyParts.Html.Charset = global::a.i.h.b(sourceEncoding);
				}
			}
			else
			{
				if (sourceEncoding == null)
				{
					sourceEncoding = Global.DefaultEncoding;
				}
				if (sourceEncoding != Global.DefaultEncoding)
				{
					i.a(sourceEncoding.GetString(i.g(), 0, i.g().Length));
				}
				this.BodyPlainText = (flag ? (this.BodyPlainText + i.c()) : i.c());
				this.BodyParts.Plain.Charset = global::a.i.h.b(sourceEncoding);
			}
			ImportRelatedFilesOptions options2 = ImportRelatedFilesOptions.None;
			if ((options & ImportBodyOptions.ImportRelatedFilesFromUris) == ImportBodyOptions.ImportRelatedFilesFromUris)
			{
				options2 = ImportRelatedFilesOptions.ImportFromUris;
			}
			if (this.aa.RelatedFilesFolder == null || this.aa.RelatedFilesFolder.Length == 0)
			{
				if ((options & ImportBodyOptions.PathIsUri) > ImportBodyOptions.None)
				{
					this.aa.RelatedFilesFolder = MailMessage.c(path);
				}
				else
				{
					this.aa.RelatedFilesFolder = Path.GetDirectoryName(path);
				}
			}
			return (options & ImportBodyOptions.ImportRelatedFiles) <= ImportBodyOptions.None || this.ImportRelatedFiles(options2);
		}

		// Token: 0x06002CDD RID: 11485 RVA: 0x000D5C04 File Offset: 0x000D4C04
		public bool LoadBodyText(string filename, MessageBodyType bodyType)
		{
			return this.LoadBodyText(filename, bodyType, null, ImportBodyOptions.None);
		}

		// Token: 0x06002CDE RID: 11486 RVA: 0x000D5C10 File Offset: 0x000D4C10
		public bool LoadMessage(string filename)
		{
			if (filename == null || filename == string.Empty)
			{
				this.y = 22;
				throw new MailBeeInvalidArgumentException(this.y);
			}
			this.y = 0;
			return this.e(filename);
		}

		// Token: 0x06002CDF RID: 11487 RVA: 0x000D5C44 File Offset: 0x000D4C44
		public bool LoadMessage(byte[] rawData)
		{
			if (rawData == null)
			{
				this.y = 21;
				throw new MailBeeInvalidArgumentException(this.y);
			}
			this.y = 0;
			ao a_ = new ao(rawData);
			if (!this.b(a_))
			{
				return false;
			}
			this.a(a_);
			return true;
		}

		// Token: 0x06002CE0 RID: 11488 RVA: 0x000D5C8C File Offset: 0x000D4C8C
		public bool LoadMessage(Stream stream)
		{
			this.y = 0;
			if (stream == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (stream.CanRead)
			{
				ao ao = null;
				this.ag = null;
				try
				{
					if (this.ab.ParseHeaderOnly)
					{
						ao = ap.g(stream);
					}
					else
					{
						ao = ap.e(stream);
					}
				}
				catch (IOException a_)
				{
					this.y = 30;
					if (this.ae)
					{
						throw new MailBeeStreamException(30, a_);
					}
					return false;
				}
				if (!this.b(ao))
				{
					return false;
				}
				if (ao != null)
				{
					this.a(ao);
				}
				return true;
			}
			this.y = 40;
			if (this.ae)
			{
				throw new MailBeeStreamException(40);
			}
			return false;
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06002CE1 RID: 11489 RVA: 0x000D5D3C File Offset: 0x000D4D3C
		public string Filename
		{
			get
			{
				return this.ag;
			}
		}

		// Token: 0x06002CE2 RID: 11490 RVA: 0x000D5D44 File Offset: 0x000D4D44
		private bool e(string A_0)
		{
			this.ag = null;
			try
			{
				ao a_;
				if (this.ab.ParseHeaderOnly)
				{
					a_ = ap.d(A_0);
				}
				else
				{
					a_ = ap.b(A_0, true, 4096);
				}
				if (!this.b(a_))
				{
					return false;
				}
				this.a(a_);
				this.d(A_0);
				this.ag = A_0;
			}
			catch (MailBeeException ex)
			{
				this.y = ex.ErrorCode;
				if (this.ae)
				{
					throw;
				}
				return false;
			}
			return true;
		}

		// Token: 0x06002CE3 RID: 11491 RVA: 0x000D5DD0 File Offset: 0x000D4DD0
		private void d(string A_0)
		{
			if (string.Compare(Path.GetExtension(A_0), ".mht", true) == 0)
			{
				MimePart mimePart = this.MimeParts["text/html"];
				if (mimePart != null)
				{
					mimePart.Headers.Remove("Content-Disposition");
					string text = mimePart.Headers["Content-Location"];
					if (text != null)
					{
						int num = text.LastIndexOf('/');
						if (num > 0)
						{
							text = text.Substring(0, num + 1);
						}
						string bodyHtmlText = this.BodyHtmlText;
						foreach (object obj in this.Attachments)
						{
							Attachment a_ = (Attachment)obj;
							global::a.i.e.a(text, a_, ref bodyHtmlText);
						}
						this.BodyHtmlText = bodyHtmlText;
					}
				}
			}
		}

		// Token: 0x06002CE4 RID: 11492 RVA: 0x000D5EB0 File Offset: 0x000D4EB0
		private bool b(ao A_0)
		{
			if (!global::a.i.k.b(A_0))
			{
				return true;
			}
			this.y = 44;
			if (this.ae)
			{
				throw new MailBeeDataParsingException(44);
			}
			return false;
		}

		// Token: 0x06002CE5 RID: 11493 RVA: 0x000D5ED5 File Offset: 0x000D4ED5
		private void a(ao A_0)
		{
			A_0 = global::a.i.k.a(A_0);
			this.Reset();
			this.NeedToRebuild = false;
			this.k = A_0;
			this.j = true;
		}

		// Token: 0x06002CE6 RID: 11494 RVA: 0x000D5EFC File Offset: 0x000D4EFC
		public bool MakePlainBodyFromHtmlBody()
		{
			this.y = 0;
			this.i();
			if (this.p["text/html"] != null)
			{
				this.BodyPlainText = global::a.i.b.a(this.p["text/html"].Text, this.aa.HtmlToPlainOptions, false);
				return true;
			}
			this.y = 318;
			return false;
		}

		// Token: 0x06002CE7 RID: 11495 RVA: 0x000D5F64 File Offset: 0x000D4F64
		public void Reset()
		{
			if (this.ab == null)
			{
				this.ab = new MessageParserConfig(this);
			}
			if (this.aa == null)
			{
				this.aa = new MessageBuilderConfig(this);
			}
			this.z = new MailMerge(this);
			this.q = "utf-8";
			this.l = new MimePart(this);
			this.s = 0;
			this.t = null;
			this.h = false;
			this.u = true;
			this.d = false;
			this.g = false;
			this.ah = null;
			this.y = 0;
			this.e = 0;
			this.f = "";
			this.i = false;
			this.j = false;
			this.ac = 1;
			this.ad = 1;
			this.k = new ao(new byte[0]);
			this.v = false;
			this.w = false;
			this.x = MessageVerificationFlags.None;
			this.n = 0;
			this.af = new TimeStampCollection();
			this.Headers.Add("MIME-Version", "1.0", false);
			this.Headers.Add("Content-Type", "text/plain", false);
			this.Headers.Add("X-Mailer", string.Format(CultureInfo.InvariantCulture, "MailBee.NET {0}", new object[]
			{
				this.Version
			}), false);
			this.Headers.Add("Content-Transfer-Encoding", "quoted-printable", false);
			this.m = new MimePartCollection();
			this.m.b(this.l);
			this.o = new AttachmentCollection(this);
			this.p = new TextBodyPartCollection(this);
			this.p.Add(new TextBodyPart(this.l));
			this.a(true);
			this.b = string.Empty;
			this.c = false;
			this.ag = null;
		}

		// Token: 0x06002CE8 RID: 11496 RVA: 0x000D613C File Offset: 0x000D513C
		public bool SaveHtmlAndRelatedFiles(string filename)
		{
			if (filename == null || filename == string.Empty)
			{
				this.y = 22;
				throw new MailBeeInvalidArgumentException(this.y);
			}
			this.y = 0;
			try
			{
				string text = this.ab.WorkingFolder;
				if (text == null)
				{
					text = Path.GetTempPath();
				}
				else if (text == string.Empty)
				{
					text = Directory.GetParent(filename).FullName.ToString();
				}
				if (File.Exists(filename))
				{
					ap.c(filename);
				}
				string text2 = this.b(text, VirtualMappingType.NonWeb, MessageFolderBehavior.DoNotCreate, true);
				Encoding encoding;
				if (this.Charset == null || this.Charset == string.Empty)
				{
					encoding = this.ab.EncodingDefault;
				}
				else
				{
					encoding = bb.a(this.Charset);
				}
				byte[] bytes = encoding.GetBytes(text2);
				byte[] a_ = (encoding == Encoding.UTF8 && this.ab.WriteUtf8ByteOrderMark) ? global::a.i.k.a(bytes) : null;
				ap.b(filename, bytes, a_);
			}
			catch (MailBeeIOException ex)
			{
				this.y = ex.ErrorCode;
				if (this.ae)
				{
					throw;
				}
				return false;
			}
			return true;
		}

		// Token: 0x06002CE9 RID: 11497 RVA: 0x000D625C File Offset: 0x000D525C
		public bool SaveMessage(string filename)
		{
			if (filename == null || filename == string.Empty)
			{
				this.y = 22;
				throw new MailBeeInvalidArgumentException(this.y);
			}
			this.y = 0;
			this.h();
			try
			{
				if (File.Exists(filename))
				{
					ap.c(filename);
				}
				ap.b(filename, this.k.d(), this.k.b(), this.k.e(), null);
			}
			catch (MailBeeIOException ex)
			{
				this.y = ex.ErrorCode;
				if (this.ae)
				{
					throw;
				}
				return false;
			}
			return true;
		}

		// Token: 0x06002CEA RID: 11498 RVA: 0x000D6304 File Offset: 0x000D5304
		public bool SaveMessage(Stream stream)
		{
			if (stream == null)
			{
				this.y = 21;
				throw new MailBeeInvalidArgumentException(this.y);
			}
			if (stream.CanWrite)
			{
				this.y = 0;
				this.h();
				try
				{
					stream.Write(this.k.c(), 0, this.k.e());
				}
				catch (IOException a_)
				{
					this.y = 30;
					if (this.ae)
					{
						throw new MailBeeStreamException(30, a_);
					}
					return false;
				}
				return true;
			}
			this.y = 41;
			if (this.ae)
			{
				throw new MailBeeStreamException(this.y);
			}
			return false;
		}

		// Token: 0x06002CEB RID: 11499 RVA: 0x000D63AC File Offset: 0x000D53AC
		public void SetDateFromString(string date)
		{
			if (date == null)
			{
				this.y = 21;
				throw new MailBeeInvalidArgumentException(this.y);
			}
			this.y = 0;
			this.i();
			Header header = this.Headers.a("Date");
			if (header != null)
			{
				header.Value = date;
			}
			else
			{
				this.Headers.Add("Date", date, false);
			}
			this.i = true;
		}

		// Token: 0x06002CEC RID: 11500 RVA: 0x000D6414 File Offset: 0x000D5414
		public bool Serialize(XmlWriter xmlWriter)
		{
			if (xmlWriter == null)
			{
				this.y = 21;
				throw new MailBeeInvalidArgumentException(this.y);
			}
			this.y = 0;
			if (this.NeedToRebuild)
			{
				this.aa.Apply();
			}
			this.ab.Apply();
			xmlWriter.WriteStartDocument();
			xmlWriter.WriteStartElement("MailMessage");
			this.MimePartTree.a(xmlWriter);
			xmlWriter.WriteEndElement();
			xmlWriter.WriteEndDocument();
			return true;
		}

		// Token: 0x06002CED RID: 11501 RVA: 0x000D6488 File Offset: 0x000D5488
		public bool Serialize(string filename)
		{
			if (filename == null || filename == string.Empty)
			{
				this.y = 22;
				throw new MailBeeInvalidArgumentException(this.y);
			}
			this.y = 0;
			if (this.NeedToRebuild)
			{
				this.aa.Apply();
			}
			this.ab.Apply();
			bool result = false;
			XmlWriter xmlWriter = null;
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write);
				xmlWriter = XmlWriter.Create(fileStream, new XmlWriterSettings
				{
					Encoding = Encoding.UTF8,
					Indent = true
				});
				result = this.Serialize(xmlWriter);
			}
			catch (UnauthorizedAccessException a_)
			{
				this.y = 32;
				if (this.ae)
				{
					throw new MailBeeIOException(32, a_);
				}
				return false;
			}
			catch (IOException a_2)
			{
				this.y = 30;
				if (this.ae)
				{
					throw new MailBeeIOException(30, a_2);
				}
				return false;
			}
			finally
			{
				if (xmlWriter != null)
				{
					xmlWriter.Close();
				}
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
			return result;
		}

		// Token: 0x06002CEE RID: 11502 RVA: 0x000D6598 File Offset: 0x000D5598
		public string SetUniqueMessageID(string domain)
		{
			this.y = 0;
			this.i();
			if (domain == null || domain.Length == 0)
			{
				domain = Dns.GetHostName();
			}
			string text = string.Format(CultureInfo.InvariantCulture, "<{0}.{1}@{2}>", new object[]
			{
				Thread.GetDomainID().ToString(CultureInfo.InvariantCulture),
				global::a.i.k.a(),
				domain
			});
			if (this.Headers.c(this.Headers.a("Message-ID")))
			{
				this.Headers.a("Message-ID").Value = text;
			}
			else
			{
				this.Headers.b(new Header("Message-ID", text));
			}
			return text;
		}

		// Token: 0x06002CEF RID: 11503 RVA: 0x000D6648 File Offset: 0x000D5648
		private void i()
		{
			if (this.j)
			{
				this.ab.Apply();
			}
		}

		// Token: 0x06002CF0 RID: 11504 RVA: 0x000D6660 File Offset: 0x000D5660
		internal string f(string A_0)
		{
			if (this.ab.CharsetConverter.ConversionMode == StringConversionMode.NoConversion)
			{
				return A_0;
			}
			string result;
			try
			{
				string a_ = (this.Charset == null || this.Charset.Length == 0) ? Global.DefaultEncoding.HeaderName : this.Charset;
				result = global::a.i.l.a(A_0, this.ab.CharsetConverter.ConversionMode, bb.a(a_), this.ab.CharsetConverter.DestinationEncoding, this.ab.CharsetConverter.CustomByteEncoding);
			}
			catch
			{
				result = A_0;
			}
			return result;
		}

		// Token: 0x06002CF1 RID: 11505 RVA: 0x000D6700 File Offset: 0x000D5700
		private void h()
		{
			if (this.NeedToRebuild || this.aa.NeedToRebuild)
			{
				TextBodyPart textBodyPart = this.p["text/html"];
				TextBodyPart textBodyPart2 = this.p["text/plain"];
				if (this.aa.HtmlToPlainMode == HtmlToPlainAutoConvert.IfHtml)
				{
					if (textBodyPart != null && textBodyPart.Text != null && textBodyPart.Text.Length != 0)
					{
						this.BodyPlainText = global::a.i.b.a(textBodyPart.Text, this.aa.HtmlToPlainOptions, false);
						this.BodyParts.Plain.CharsetInternal = this.BodyParts.Html.CharsetInternal;
					}
				}
				else if (this.aa.HtmlToPlainMode == HtmlToPlainAutoConvert.IfNoPlain && textBodyPart != null && textBodyPart.Text != null && textBodyPart.Text.Length > 0 && (textBodyPart2 == null || textBodyPart2.Text == null || textBodyPart2.Text.Length == 0))
				{
					this.BodyPlainText = global::a.i.b.a(textBodyPart.Text, this.aa.HtmlToPlainOptions, false);
					this.BodyParts.Plain.CharsetInternal = this.BodyParts.Html.CharsetInternal;
				}
				MimePart a_ = this.e();
				a_ = this.g(a_);
				this.l = a_;
				bool headersAsHtml = this.ab.HeadersAsHtml;
				this.ab.HeadersAsHtmlInternal = false;
				try
				{
					this.k = this.l.a(this.g());
				}
				finally
				{
					this.ab.HeadersAsHtmlInternal = headersAsHtml;
				}
				this.NeedToRebuild = false;
				this.aa.NeedToRebuild = false;
			}
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x000D68A8 File Offset: 0x000D58A8
		private ao g()
		{
			if (this.aa.BuildHeaderOnly)
			{
				return this.k;
			}
			int num = 4096;
			foreach (object obj in this.p)
			{
				TextBodyPart textBodyPart = (TextBodyPart)obj;
				num += 1024;
				if (textBodyPart.TransferEncoding == MailTransferEncoding.Base64)
				{
					num += global::a.i.h.a(textBodyPart.Text.Length, Global.UnwrappedLineLengthLimit, 1.2f);
				}
				else if (textBodyPart.TransferEncoding == MailTransferEncoding.QuotedPrintable)
				{
					num += textBodyPart.Text.Length * 3;
				}
				else
				{
					num += textBodyPart.Text.Length;
				}
			}
			foreach (object obj2 in this.o)
			{
				Attachment attachment = (Attachment)obj2;
				num += 1024;
				if (attachment.AsMimePart != null)
				{
					if (attachment.AsMimePart.MimePartTransferEncoding == MailTransferEncoding.Base64)
					{
						num += global::a.i.h.a(attachment.Size, Global.UnwrappedLineLengthLimit, 1.2f);
					}
					else if (attachment.AsMimePart.MimePartTransferEncoding == MailTransferEncoding.QuotedPrintable)
					{
						num += attachment.Size * 3;
					}
					else
					{
						num += attachment.Size;
					}
				}
				else
				{
					num += global::a.i.h.a(attachment.Size, Global.UnwrappedLineLengthLimit, 1.2f);
				}
			}
			return new ao(new byte[num], 0);
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x000D6A40 File Offset: 0x000D5A40
		internal void r()
		{
			if (this.k.e() < this.n)
			{
				this.u = false;
			}
			global::a.i.k.c(this.k);
			this.l = MimePart.a(this.k, this);
			this.f();
		}

		// Token: 0x06002CF4 RID: 11508 RVA: 0x000D6A80 File Offset: 0x000D5A80
		private void f()
		{
			this.m.b();
			this.h(this.l);
			this.q = string.Empty;
			foreach (object obj in this.m)
			{
				MimePart mimePart = (MimePart)obj;
				if (MimePart.b(mimePart) && (this.q == null || this.q.Length == 0))
				{
					this.q = mimePart.CharsetInternal;
					break;
				}
			}
			if (this.q == null || this.q.Length == 0)
			{
				foreach (object obj2 in this.m)
				{
					MimePart mimePart2 = (MimePart)obj2;
					if (MimePart.b(mimePart2) && mimePart2.ContentType.ToLower() == "text/html")
					{
						string text = global::a.i.e.a(mimePart2.PartValueAsString);
						if (text != null)
						{
							this.q = text.Split(new char[]
							{
								'='
							})[1];
							break;
						}
					}
				}
			}
			if (this.q != null && this.q.Length > 0)
			{
				foreach (object obj3 in this.m)
				{
					MimePart mimePart3 = (MimePart)obj3;
					if (MimePart.b(mimePart3) && (mimePart3.Charset == null || mimePart3.Charset == string.Empty))
					{
						mimePart3.a(this.q, false);
					}
				}
			}
			if (this.ab.EncodingOverride == null && this.q != null && this.q.Length != 0 && !global::a.i.h.a(bb.a(this.q)))
			{
				foreach (object obj4 in this.m)
				{
					MimePart mimePart4 = (MimePart)obj4;
					for (int i = 0; i < mimePart4.Headers.Count; i++)
					{
						Header header = mimePart4.Headers[i];
						if (string.Compare(header.Name, "Subject", true) == 0 || string.Compare(header.Name, "Content-Type", true) == 0 || string.Compare(header.Name, "To", true) == 0 || string.Compare(header.Name, "Cc", true) == 0 || string.Compare(header.Name, "Bcc", true) == 0 || string.Compare(header.Name, "Reply-To", true) == 0 || string.Compare(header.Name, "From", true) == 0 || string.Compare(header.Name, "Content-Disposition", true) == 0)
						{
							string a_;
							if (string.Compare(header.Name, "Subject", true) == 0 || string.Compare(header.Name, "To", true) == 0 || string.Compare(header.Name, "Cc", true) == 0 || string.Compare(header.Name, "Bcc", true) == 0 || string.Compare(header.Name, "Reply-To", true) == 0 || string.Compare(header.Name, "From", true) == 0)
							{
								a_ = bb.a(this.q).GetString(header.RawBody.g(), 0, header.RawBody.g().Length);
							}
							else
							{
								a_ = header.a(MailTransferEncoding.QuotedPrintable, this.q);
							}
							header = Header.a(a_);
							if (header != null)
							{
								mimePart4.Headers[i] = header;
								mimePart4.Headers[i].ParentCollection = mimePart4.Headers;
							}
						}
					}
				}
			}
			this.b();
			this.a(this.l.Headers);
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x000D6EE4 File Offset: 0x000D5EE4
		private void h(MimePart A_0)
		{
			this.m.b(A_0);
			if (A_0.SubParts != null)
			{
				foreach (object obj in A_0.SubParts)
				{
					MimePart a_ = (MimePart)obj;
					this.h(a_);
				}
			}
			this.m.NeedToRebuild = false;
		}

		// Token: 0x06002CF6 RID: 11510 RVA: 0x000D6F60 File Offset: 0x000D5F60
		private MimePart g(MimePart A_0)
		{
			HeaderCollection headerCollection = new HeaderCollection();
			foreach (object obj in A_0.Headers)
			{
				Header a_ = (Header)obj;
				headerCollection.b(a_);
			}
			for (int i = this.Headers.Count - 1; i >= 0; i--)
			{
				Header header = this.Headers[i];
				if (header.Value != null && header.Value.Length != 0 && (!(header.Name.ToLower() == "content-disposition") || !(header.Value.ToLower() == "attachment") || A_0.SubParts == null || A_0.SubParts.Count <= 0) && (!(header.Name.ToLower() == "content-transfer-encoding") || A_0.SubParts == null || A_0.SubParts.Count <= 0) && !headerCollection.Exists(header.Name))
				{
					A_0.Headers.a(0, header);
				}
			}
			int j = 0;
			while (j < A_0.Headers.Count)
			{
				if (A_0.Headers[j].Value == null || A_0.Headers[j].Value.Length == 0)
				{
					A_0.Headers.RemoveAt(j);
				}
				else
				{
					j++;
				}
			}
			return A_0;
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x000D70F4 File Offset: 0x000D60F4
		private MimePart e()
		{
			MimePart mimePart = new MimePart(this);
			if (this.aa.BuildHeaderOnly)
			{
				return mimePart;
			}
			if (this.m.NeedToRebuild)
			{
				if (this.m.Count > 1 && this.m["text/plain"] == null && this.m["text/html"] == null)
				{
					this.BodyPlainText = null;
				}
				HeaderCollection headerCollection = new HeaderCollection();
				this.d();
				if (this.m["text/plain"] != null)
				{
					headerCollection.a(global::a.i.k.b(this.m["text/plain"].Headers));
				}
				if (this.m["text/html"] != null)
				{
					headerCollection.a(global::a.i.k.b(this.m["text/html"].Headers));
				}
				mimePart = this.c();
				headerCollection.a(global::a.i.k.b(mimePart.Headers));
				mimePart = this.f(mimePart);
				headerCollection.a(global::a.i.k.b(mimePart.Headers));
				mimePart = this.e(mimePart);
				headerCollection.a(global::a.i.k.b(mimePart.Headers));
				mimePart = this.d(mimePart);
				headerCollection.a(global::a.i.k.b(mimePart.Headers));
				mimePart = this.c(mimePart);
				headerCollection.a(global::a.i.k.b(mimePart.Headers));
				mimePart.Headers = global::a.i.k.a(headerCollection, mimePart.Headers);
			}
			else
			{
				this.d();
				mimePart = this.c();
				mimePart = this.f(mimePart);
				mimePart = this.e(mimePart);
				mimePart = this.d(mimePart);
				mimePart = this.c(mimePart);
			}
			return mimePart;
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x000D7290 File Offset: 0x000D6290
		private void d()
		{
			this.h = false;
			if (this.BodyParts.Count > 1)
			{
				int num = 0;
				foreach (object obj in this.BodyParts)
				{
					TextBodyPart textBodyPart = (TextBodyPart)obj;
					if (textBodyPart.Text != null && textBodyPart.Text.Length > 0)
					{
						num++;
					}
				}
				if (num > 1)
				{
					this.h = true;
				}
			}
			this.g = false;
			this.d = false;
			using (IEnumerator enumerator = this.Attachments.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((Attachment)enumerator.Current).IsRelated)
					{
						this.g = true;
					}
					else
					{
						this.d = true;
					}
				}
			}
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x000D7384 File Offset: 0x000D6384
		private MimePart c()
		{
			MimePart mimePart = new MimePart(this);
			if (this.h)
			{
				mimePart = this.m["multipart/alternative"];
				if (mimePart == null || mimePart.NeedToRebuild || this.p.NeedToRebuild)
				{
					HeaderCollection headers = new HeaderCollection();
					byte[] a_ = new byte[0];
					bool flag = false;
					if (mimePart != null)
					{
						headers = mimePart.Headers;
						a_ = mimePart.PartValueAsBytes;
						flag = true;
						this.m.a(mimePart);
					}
					mimePart = new MimePart(this);
					this.m.b(mimePart);
					if (flag)
					{
						mimePart.Headers = headers;
						mimePart.PartValueAsBytes = a_;
					}
					else
					{
						Header header = new Header("Content-Type", "multipart/alternative");
						header.HeaderParameters = new global::a.i.j();
						global::a.i.n a_2 = new global::a.i.n("boundary", this.x());
						header.HeaderParameters.c(a_2);
						mimePart.Headers.b(header);
					}
					if (!Global.PreserveMimePartOrder)
					{
						if (this.p.Plain != null && this.p.Plain.Text != null && this.p.Plain.Text.Length != 0)
						{
							mimePart.SubPartsInternal.b(this.p.Plain.AsMimePart);
						}
						if (this.p.Html != null && this.p.Html.Text != null && this.p.Html.Text.Length != 0)
						{
							mimePart.SubPartsInternal.b(this.p.Html.AsMimePart);
						}
					}
					using (IEnumerator enumerator = this.p.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							TextBodyPart textBodyPart = (TextBodyPart)obj;
							if (textBodyPart.Text != null && textBodyPart.Text.Length != 0 && !mimePart.SubPartsInternal.c(textBodyPart.AsMimePart))
							{
								mimePart.SubPartsInternal.b(textBodyPart.AsMimePart);
							}
						}
						return mimePart;
					}
				}
				return mimePart;
			}
			if (this.p.Count <= 0)
			{
				return this.l;
			}
			if (this.p[0] != null)
			{
				MimePart mimePart2 = null;
				for (int i = 0; i < this.p.Count; i++)
				{
					if (this.p[i] != null && this.p[i].Text != null && this.p[i].Text != string.Empty)
					{
						mimePart2 = this.p[i].AsMimePart;
					}
				}
				if (mimePart2 == null)
				{
					mimePart2 = this.p[0].AsMimePart;
				}
				return mimePart2;
			}
			return mimePart;
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x000D7664 File Offset: 0x000D6664
		private MimePart f(MimePart A_0)
		{
			MimePart mimePart = new MimePart(this);
			if (!this.g || A_0.IsSigned)
			{
				return A_0;
			}
			mimePart = this.m["multipart/related"];
			if (mimePart == null || mimePart.NeedToRebuild || this.p.NeedToRebuild || this.o.NeedToRebuild)
			{
				HeaderCollection headers = new HeaderCollection();
				byte[] a_ = new byte[0];
				bool flag = false;
				if (mimePart != null)
				{
					headers = mimePart.Headers;
					a_ = mimePart.PartValueAsBytes;
					flag = true;
					this.m.a(mimePart);
				}
				mimePart = new MimePart(this);
				this.m.b(mimePart);
				if (flag)
				{
					mimePart.Headers = headers;
					mimePart.PartValueAsBytes = a_;
				}
				else
				{
					Header header = new Header("Content-Type", "multipart/related");
					header.HeaderParameters = new global::a.i.j();
					global::a.i.n a_2 = new global::a.i.n("boundary", this.x());
					header.HeaderParameters.c(a_2);
					mimePart.Headers.b(header);
				}
				MimePart mimePart2 = MimePart.a(A_0, "multipart/alternative");
				if (mimePart2 != null)
				{
					mimePart.SubPartsInternal.b(mimePart2);
				}
				else
				{
					mimePart.SubPartsInternal.b(A_0);
				}
				return this.b(mimePart);
			}
			return mimePart;
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x000D77A4 File Offset: 0x000D67A4
		private MimePart e(MimePart A_0)
		{
			MimePart mimePart = new MimePart(this);
			if ((!this.d && string.IsNullOrEmpty(this.ah)) || A_0.IsSigned)
			{
				return A_0;
			}
			bool flag = !string.IsNullOrEmpty(this.ah);
			mimePart = (flag ? this.m["multipart/report"] : this.m["multipart/mixed"]);
			if (mimePart == null || mimePart.NeedToRebuild || this.p.NeedToRebuild || this.o.NeedToRebuild)
			{
				HeaderCollection headers = new HeaderCollection();
				byte[] a_ = new byte[0];
				bool flag2 = false;
				if (mimePart != null)
				{
					headers = mimePart.Headers;
					a_ = mimePart.PartValueAsBytes;
					flag2 = true;
					this.m.a(mimePart);
				}
				mimePart = new MimePart(this);
				this.m.b(mimePart);
				if (flag2)
				{
					mimePart.Headers = headers;
					mimePart.PartValueAsBytes = a_;
				}
				else
				{
					Header header = new Header("Content-Type", flag ? "multipart/report" : "multipart/mixed");
					header.HeaderParameters = new global::a.i.j();
					global::a.i.n a_2 = new global::a.i.n("boundary", this.x());
					if (flag)
					{
						global::a.i.n a_3 = new global::a.i.n("report-type", this.ah);
						header.HeaderParameters.c(a_3);
					}
					header.HeaderParameters.c(a_2);
					mimePart.Headers.b(header);
				}
				if (!MimePart.c(A_0) && A_0.ContentType.ToLower() != "multipart/mixed")
				{
					mimePart.SubPartsInternal.b(A_0);
				}
				return this.a(mimePart);
			}
			return mimePart;
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x000D7944 File Offset: 0x000D6944
		private MimePart d(MimePart A_0)
		{
			MimePart mimePart = new MimePart(this);
			if (!this.IsSigned || A_0.IsSigned)
			{
				return A_0;
			}
			mimePart = this.m["multipart/signed"];
			if (mimePart != null && mimePart.SubParts != null && (this.p.NeedToRebuild || this.o.NeedToRebuild))
			{
				mimePart.SubParts.a(0);
				mimePart.SubParts.a(0, A_0);
				return mimePart;
			}
			if (mimePart != null)
			{
				return mimePart;
			}
			return A_0;
		}

		// Token: 0x06002CFD RID: 11517 RVA: 0x000D79C4 File Offset: 0x000D69C4
		private MimePart c(MimePart A_0)
		{
			MimePart mimePart = new MimePart(this);
			if (this.IsEncrypted)
			{
				mimePart = this.m["application/pkcs7-mime"];
				if (mimePart != null)
				{
					return mimePart;
				}
				mimePart = this.m["application/x-pkcs7-mime"];
				if (mimePart != null)
				{
					return mimePart;
				}
			}
			return A_0;
		}

		// Token: 0x06002CFE RID: 11518 RVA: 0x000D7A10 File Offset: 0x000D6A10
		private MimePart b(MimePart A_0)
		{
			foreach (object obj in this.Attachments)
			{
				Attachment attachment = (Attachment)obj;
				if (attachment.IsRelated)
				{
					A_0.SubPartsInternal.b(attachment.AsMimePart);
				}
			}
			return A_0;
		}

		// Token: 0x06002CFF RID: 11519 RVA: 0x000D7A80 File Offset: 0x000D6A80
		private MimePart a(MimePart A_0)
		{
			foreach (object obj in this.Attachments)
			{
				Attachment attachment = (Attachment)obj;
				if (!attachment.IsRelated && string.Compare(attachment.FilenameOriginalInternal, "smime.p7s", true) != 0)
				{
					A_0.SubPartsInternal.b(attachment.AsMimePart);
				}
			}
			return A_0;
		}

		// Token: 0x06002D00 RID: 11520 RVA: 0x000D7B04 File Offset: 0x000D6B04
		private void a(HeaderCollection A_0)
		{
			foreach (object obj in A_0)
			{
				Header header = (Header)obj;
				string text = header.Name.ToLower();
				if (!(text == "content-type"))
				{
					if (text == "received")
					{
						TimeStamp a_ = TimeStamp.a(header.ValueRawBody.c(), header);
						this.af.a(a_);
					}
				}
				else
				{
					text = this.ContentType.ToLower();
					if (!(text == "multipart/alternative"))
					{
						if (!(text == "multipart/related"))
						{
							if (!(text == "multipart/mixed"))
							{
								if (!(text == "message/partial"))
								{
									if (!(text == "multipart/report"))
									{
										continue;
									}
								}
								else
								{
									this.u = false;
									if (header.HeaderParameters == null)
									{
										continue;
									}
									global::a.i.n n = header.HeaderParameters.b("total");
									if (n != null)
									{
										try
										{
											this.ac = int.Parse(n.c(), CultureInfo.InvariantCulture);
										}
										catch
										{
											this.ac = -1;
										}
									}
									global::a.i.n n2 = header.HeaderParameters.b("number");
									if (n2 == null || n2.c() == null)
									{
										continue;
									}
									try
									{
										this.ad = int.Parse(n2.c(), CultureInfo.InvariantCulture);
										continue;
									}
									catch
									{
										this.ad = -1;
										continue;
									}
								}
								if (header.HeaderParameters != null)
								{
									global::a.i.n n3 = header.HeaderParameters.b("report-type");
									if (n3 != null)
									{
										this.ah = n3.c();
									}
								}
							}
							else
							{
								this.d = true;
							}
						}
						else
						{
							this.g = true;
						}
					}
					else
					{
						this.h = true;
					}
				}
			}
		}

		// Token: 0x06002D01 RID: 11521 RVA: 0x000D7D14 File Offset: 0x000D6D14
		internal void k()
		{
			foreach (object obj in this.l.Headers)
			{
				Header header = (Header)obj;
				string text = header.Name.ToLower();
				if (text == "received")
				{
					TimeStamp a_ = TimeStamp.a((header.ValueRawBody.c() == string.Empty) ? header.Value : header.ValueRawBody.c(), header);
					this.af.a(a_);
				}
			}
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x000D7DC4 File Offset: 0x000D6DC4
		private bool a(bool A_0)
		{
			if (A_0 && this.ab != null)
			{
				this.ab.MessageFolderInternal = null;
			}
			if (this.c && Directory.Exists(this.b))
			{
				try
				{
					Directory.Delete(this.b, true);
					this.b = string.Empty;
					this.c = false;
				}
				catch (UnauthorizedAccessException a_)
				{
					this.y = 32;
					if (this.ae)
					{
						throw new MailBeeIOException(32, a_);
					}
					return false;
				}
				catch (IOException a_2)
				{
					this.y = 30;
					if (this.ae)
					{
						throw new MailBeeIOException(30, a_2);
					}
					return false;
				}
				return true;
			}
			return true;
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x000D7E78 File Offset: 0x000D6E78
		private void b()
		{
			this.o.Clear();
			this.p.Clear();
			MimePart mimePart = this.m["multipart/alternative"];
			if (mimePart == null)
			{
				mimePart = this.m["multipart/related"];
			}
			MimePart mimePart2 = null;
			MimePart mimePart3 = null;
			if (!Global.PreserveMimePartOrder && mimePart != null && mimePart.SubParts != null)
			{
				mimePart2 = mimePart.SubParts["text/plain"];
				if (mimePart2 != null && !MimePart.c(mimePart2))
				{
					TextBodyPart part = new TextBodyPart(mimePart2, true);
					this.BodyParts.Add(part);
					this.BodyParts.NeedToRebuild = false;
				}
				else
				{
					mimePart2 = null;
				}
				mimePart3 = mimePart.SubParts["text/html"];
				if (mimePart3 != null && !MimePart.c(mimePart3))
				{
					TextBodyPart part2 = new TextBodyPart(mimePart3, true);
					this.BodyParts.Add(part2);
					this.BodyParts.NeedToRebuild = false;
				}
				else
				{
					mimePart3 = null;
				}
			}
			foreach (object obj in this.m)
			{
				MimePart mimePart4 = (MimePart)obj;
				if (Global.PreserveMimePartOrder || (mimePart4 != mimePart2 && mimePart4 != mimePart3))
				{
					if (mimePart4 == this.l && MimePart.b(mimePart4) && !MimePart.c(mimePart4))
					{
						TextBodyPart part3 = new TextBodyPart(mimePart4, true);
						this.BodyParts.Add(part3);
						this.BodyParts.NeedToRebuild = false;
					}
					else if (MimePart.c(mimePart4))
					{
						Attachment attach = new Attachment(mimePart4);
						this.Attachments.Add(attach);
						this.Attachments.NeedToRebuild = false;
					}
					else if (MimePart.b(mimePart4))
					{
						TextBodyPart part4 = new TextBodyPart(mimePart4, true);
						this.BodyParts.Add(part4);
						this.BodyParts.NeedToRebuild = false;
					}
					else if (!MimePart.a(mimePart4))
					{
						Attachment attach2 = new Attachment(mimePart4);
						this.Attachments.Add(attach2);
						this.Attachments.NeedToRebuild = false;
					}
				}
			}
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x000D80A8 File Offset: 0x000D70A8
		internal string x()
		{
			string text = global::a.i.k.a();
			string text2 = global::a.i.k.a();
			string text3 = global::a.i.k.a();
			string format = "----=_NextPart_{0}_{1}_{2}.{3}";
			object[] array = new object[4];
			int num = 0;
			int num2 = this.e;
			this.e = num2 + 1;
			array[num] = num2.ToString("d3").ToUpper();
			array[1] = text.Substring(0, 4).ToUpper();
			array[2] = text2.Substring(0, 8).ToUpper();
			array[3] = text3.Substring(0, 8).ToUpper();
			this.f = string.Format(format, array);
			return this.f;
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x000D8138 File Offset: 0x000D7138
		private string a(string A_0, string A_1, string A_2, out string A_3, ref string A_4)
		{
			bool flag;
			do
			{
				if (A_2 == null || A_2 == string.Empty)
				{
					if (A_4 == string.Empty || A_4 == this.a)
					{
						A_4 = global::a.i.k.a();
					}
					this.a = A_4;
					A_2 = string.Format("{0}{1}", A_4, this.GetHashCode().ToString());
					A_2 = global::a.i.k.b(new SHA1CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(A_2)));
					A_2 = string.Format("{0}.eml", A_2);
				}
				A_3 = ap.a(A_0, ap.g(A_2));
				if (A_1 == null && File.Exists(A_3))
				{
					A_2 = string.Empty;
					A_4 = string.Empty;
					flag = false;
				}
				else
				{
					flag = true;
				}
			}
			while (!flag);
			return A_2;
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x000D8208 File Offset: 0x000D7208
		private void a(string A_0, EmailAddressCollection A_1)
		{
			Header header = new Header("x-sender", A_0);
			header.Address = new EmailAddress(A_0);
			this.Headers.a(0, header);
			foreach (object obj in A_1)
			{
				EmailAddress emailAddress = (EmailAddress)obj;
				header = new Header("x-receiver", emailAddress.Email);
				this.Headers.a(1, header);
			}
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x000D8298 File Offset: 0x000D7298
		private void a()
		{
			this.Headers.Remove("x-sender");
			this.Headers.Remove("x-receiver");
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x000D82BC File Offset: 0x000D72BC
		internal string a(string A_0, string A_1, string A_2, EmailAddressCollection A_3, bool A_4)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			string text = A_1;
			string empty = string.Empty;
			string messageID = this.MessageID;
			this.a(A_2, A_3);
			try
			{
				text = this.a(A_0, A_1, text, out empty, ref messageID);
				int num = 3;
				byte[] messageRawData = this.GetMessageRawData();
				for (int i = 0; i < num; i++)
				{
					try
					{
						if (A_4)
						{
							ap.b(empty, messageRawData, 0, messageRawData.Length);
						}
						else
						{
							ap.b(empty, messageRawData, 0, messageRawData.Length, null);
						}
						break;
					}
					catch (MailBeeIOException)
					{
						if (i == num - 1)
						{
							throw;
						}
					}
				}
			}
			finally
			{
				this.a();
			}
			return text;
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x000D836C File Offset: 0x000D736C
		private static string c(string A_0)
		{
			string text = string.Format("{0}/", A_0);
			Stream stream = null;
			try
			{
				stream = new WebClient().OpenRead(text);
			}
			catch (Exception)
			{
				int length = A_0.LastIndexOf('/');
				return A_0.Substring(0, length);
			}
			finally
			{
				if (stream != null)
				{
					stream.Close();
				}
			}
			return text;
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x000D83D4 File Offset: 0x000D73D4
		public void DomainKeysSign(bool isWebApp, string[] headersToSign, string privateKeyStr, bool isFilename, string selector, DomainKeysTypes dkTypes)
		{
			new DomainKeys(isWebApp)
			{
				ThrowExceptions = this.ThrowExceptions
			}.a(this, false, headersToSign, privateKeyStr, isFilename, selector, dkTypes);
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x000D83F8 File Offset: 0x000D73F8
		public DomainKeysVerifyResult DomainKeysVerify()
		{
			DomainKeys domainKeys = new DomainKeys();
			domainKeys.ThrowExceptions = this.ThrowExceptions;
			Smtp smtp = new Smtp(null, true);
			if (!smtp.DnsServers.Autodetect())
			{
				throw new MailBeeSystemSettingsException(214);
			}
			return domainKeys.Verify(this, smtp, DomainKeysTypes.Both);
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06002D0C RID: 11532 RVA: 0x000D843E File Offset: 0x000D743E
		// (set) Token: 0x06002D0D RID: 11533 RVA: 0x000D844C File Offset: 0x000D744C
		public string MdnReportType
		{
			get
			{
				this.i();
				return this.ah;
			}
			set
			{
				this.i();
				this.ah = value;
				this.i = true;
			}
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x000D8464 File Offset: 0x000D7464
		public Task<bool> ImportRelatedFilesAsync(ImportRelatedFilesOptions options)
		{
			MailMessage.i i;
			i.c = this;
			i.d = options;
			i.b = AsyncTaskMethodBuilder<bool>.Create();
			i.a = -1;
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = i.b;
			asyncTaskMethodBuilder.Start<MailMessage.i>(ref i);
			return i.b.Task;
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x000D84B1 File Offset: 0x000D74B1
		public Task<bool> LoadBodyTextAsync(string filename, MessageBodyType bodyType)
		{
			return this.LoadBodyTextAsync(filename, bodyType, null, ImportBodyOptions.None);
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x000D84C0 File Offset: 0x000D74C0
		public Task<bool> LoadBodyTextAsync(string path, MessageBodyType bodyType, Encoding sourceEncoding, ImportBodyOptions options)
		{
			MailMessage.m m;
			m.d = this;
			m.c = path;
			m.g = bodyType;
			m.h = sourceEncoding;
			m.e = options;
			m.b = AsyncTaskMethodBuilder<bool>.Create();
			m.a = -1;
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = m.b;
			asyncTaskMethodBuilder.Start<MailMessage.m>(ref m);
			return m.b.Task;
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x000D8528 File Offset: 0x000D7528
		public Task<DomainKeysVerifyResult> DomainKeysVerifyAsync()
		{
			DomainKeys domainKeys = new DomainKeys();
			domainKeys.ThrowExceptions = this.ThrowExceptions;
			Smtp smtp = new Smtp(null, true);
			if (!smtp.DnsServers.Autodetect())
			{
				throw new MailBeeSystemSettingsException(214);
			}
			return domainKeys.VerifyAsync(this, smtp, DomainKeysTypes.Both);
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x000D8570 File Offset: 0x000D7570
		public Task<bool> DeserializeAsync(string filename)
		{
			MailMessage.a a;
			a.d = this;
			a.c = filename;
			a.b = AsyncTaskMethodBuilder<bool>.Create();
			a.a = -1;
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = a.b;
			asyncTaskMethodBuilder.Start<MailMessage.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x06002D13 RID: 11539 RVA: 0x000D85C0 File Offset: 0x000D75C0
		public Task<bool> DeserializeAsync(XmlReader xmlReader)
		{
			MailMessage.j j;
			j.d = this;
			j.c = xmlReader;
			j.b = AsyncTaskMethodBuilder<bool>.Create();
			j.a = -1;
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = j.b;
			asyncTaskMethodBuilder.Start<MailMessage.j>(ref j);
			return j.b.Task;
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x000D8610 File Offset: 0x000D7610
		public Task<bool> SerializeAsync(XmlWriter xmlWriter)
		{
			MailMessage.l l;
			l.d = this;
			l.c = xmlWriter;
			l.b = AsyncTaskMethodBuilder<bool>.Create();
			l.a = -1;
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = l.b;
			asyncTaskMethodBuilder.Start<MailMessage.l>(ref l);
			return l.b.Task;
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x000D8660 File Offset: 0x000D7660
		public Task<bool> SerializeAsync(string filename)
		{
			MailMessage.n n;
			n.d = this;
			n.c = filename;
			n.b = AsyncTaskMethodBuilder<bool>.Create();
			n.a = -1;
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = n.b;
			asyncTaskMethodBuilder.Start<MailMessage.n>(ref n);
			return n.b.Task;
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x000D86AD File Offset: 0x000D76AD
		public Task<string> GetHtmlAndSaveRelatedFilesAsync()
		{
			this.y = 0;
			return global::a.i.e.a(this.ab.WorkingFolder, VirtualMappingType.NonWeb, MessageFolderBehavior.CreateOnly, this, this.ab, false);
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x000D86D0 File Offset: 0x000D76D0
		public Task<string> GetHtmlAndSaveRelatedFilesAsync(string virtualPath, VirtualMappingType mappingType, MessageFolderBehavior folderMode)
		{
			if (mappingType == VirtualMappingType.NonWeb)
			{
				virtualPath = this.ab.WorkingFolder;
			}
			else if (virtualPath == null)
			{
				this.y = 21;
				throw new MailBeeInvalidArgumentException(this.y);
			}
			this.y = 0;
			return this.a(virtualPath, mappingType, folderMode, false);
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x000D870C File Offset: 0x000D770C
		private Task<string> a(string A_0, VirtualMappingType A_1, MessageFolderBehavior A_2, bool A_3)
		{
			MailMessage.e e;
			e.c = this;
			e.d = A_0;
			e.e = A_1;
			e.f = A_2;
			e.g = A_3;
			e.b = AsyncTaskMethodBuilder<string>.Create();
			e.a = -1;
			AsyncTaskMethodBuilder<string> asyncTaskMethodBuilder = e.b;
			asyncTaskMethodBuilder.Start<MailMessage.e>(ref e);
			return e.b.Task;
		}

		// Token: 0x06002D19 RID: 11545 RVA: 0x000D8774 File Offset: 0x000D7774
		private static Task<string> b(string A_0)
		{
			MailMessage.k k;
			k.c = A_0;
			k.b = AsyncTaskMethodBuilder<string>.Create();
			k.a = -1;
			AsyncTaskMethodBuilder<string> asyncTaskMethodBuilder = k.b;
			asyncTaskMethodBuilder.Start<MailMessage.k>(ref k);
			return k.b.Task;
		}

		// Token: 0x06002D1A RID: 11546 RVA: 0x000D87B9 File Offset: 0x000D77B9
		public Task<bool> LoadMessageAsync(string filename)
		{
			if (filename == null || filename == string.Empty)
			{
				this.y = 22;
				throw new MailBeeInvalidArgumentException(this.y);
			}
			this.y = 0;
			return this.a(filename);
		}

		// Token: 0x06002D1B RID: 11547 RVA: 0x000D87F0 File Offset: 0x000D77F0
		public Task<bool> LoadMessageAsync(Stream stream)
		{
			MailMessage.b b;
			b.c = this;
			b.d = stream;
			b.b = AsyncTaskMethodBuilder<bool>.Create();
			b.a = -1;
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = b.b;
			asyncTaskMethodBuilder.Start<MailMessage.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x06002D1C RID: 11548 RVA: 0x000D8840 File Offset: 0x000D7840
		private Task<bool> a(string A_0)
		{
			MailMessage.c c;
			c.c = this;
			c.d = A_0;
			c.b = AsyncTaskMethodBuilder<bool>.Create();
			c.a = -1;
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = c.b;
			asyncTaskMethodBuilder.Start<MailMessage.c>(ref c);
			return c.b.Task;
		}

		// Token: 0x06002D1D RID: 11549 RVA: 0x000D8890 File Offset: 0x000D7890
		public Task<bool> SaveHtmlAndRelatedFilesAsync(string filename)
		{
			MailMessage.g g;
			g.d = this;
			g.c = filename;
			g.b = AsyncTaskMethodBuilder<bool>.Create();
			g.a = -1;
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = g.b;
			asyncTaskMethodBuilder.Start<MailMessage.g>(ref g);
			return g.b.Task;
		}

		// Token: 0x06002D1E RID: 11550 RVA: 0x000D88E0 File Offset: 0x000D78E0
		public Task<bool> SaveMessageAsync(string filename)
		{
			MailMessage.f f;
			f.d = this;
			f.c = filename;
			f.b = AsyncTaskMethodBuilder<bool>.Create();
			f.a = -1;
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = f.b;
			asyncTaskMethodBuilder.Start<MailMessage.f>(ref f);
			return f.b.Task;
		}

		// Token: 0x06002D1F RID: 11551 RVA: 0x000D8930 File Offset: 0x000D7930
		public Task<bool> SaveMessageAsync(Stream stream)
		{
			MailMessage.h h;
			h.d = this;
			h.c = stream;
			h.b = AsyncTaskMethodBuilder<bool>.Create();
			h.a = -1;
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = h.b;
			asyncTaskMethodBuilder.Start<MailMessage.h>(ref h);
			return h.b.Task;
		}

		// Token: 0x06002D20 RID: 11552 RVA: 0x000D8980 File Offset: 0x000D7980
		internal Task<string> b(string A_0, string A_1, string A_2, EmailAddressCollection A_3, bool A_4)
		{
			MailMessage.d d;
			d.e = this;
			d.c = A_0;
			d.d = A_1;
			d.f = A_2;
			d.g = A_3;
			d.h = A_4;
			d.b = AsyncTaskMethodBuilder<string>.Create();
			d.a = -1;
			AsyncTaskMethodBuilder<string> asyncTaskMethodBuilder = d.b;
			asyncTaskMethodBuilder.Start<MailMessage.d>(ref d);
			return d.b.Task;
		}

		// Token: 0x04001EDC RID: 7900
		private string a = string.Empty;

		// Token: 0x04001EDD RID: 7901
		private string b = string.Empty;

		// Token: 0x04001EDE RID: 7902
		private bool c;

		// Token: 0x04001EDF RID: 7903
		private bool d;

		// Token: 0x04001EE0 RID: 7904
		private int e;

		// Token: 0x04001EE1 RID: 7905
		private string f = "";

		// Token: 0x04001EE2 RID: 7906
		private bool g;

		// Token: 0x04001EE3 RID: 7907
		private bool h;

		// Token: 0x04001EE4 RID: 7908
		private bool i;

		// Token: 0x04001EE5 RID: 7909
		private bool j;

		// Token: 0x04001EE6 RID: 7910
		private ao k;

		// Token: 0x04001EE7 RID: 7911
		private MimePart l;

		// Token: 0x04001EE8 RID: 7912
		private MimePartCollection m;

		// Token: 0x04001EE9 RID: 7913
		private int n;

		// Token: 0x04001EEA RID: 7914
		private AttachmentCollection o;

		// Token: 0x04001EEB RID: 7915
		private TextBodyPartCollection p;

		// Token: 0x04001EEC RID: 7916
		private string q;

		// Token: 0x04001EED RID: 7917
		private bool r;

		// Token: 0x04001EEE RID: 7918
		private int s;

		// Token: 0x04001EEF RID: 7919
		private object t;

		// Token: 0x04001EF0 RID: 7920
		private bool u;

		// Token: 0x04001EF1 RID: 7921
		private bool v;

		// Token: 0x04001EF2 RID: 7922
		private bool w;

		// Token: 0x04001EF3 RID: 7923
		private MessageVerificationFlags x;

		// Token: 0x04001EF4 RID: 7924
		private int y;

		// Token: 0x04001EF5 RID: 7925
		private MailMerge z;

		// Token: 0x04001EF6 RID: 7926
		private MessageBuilderConfig aa;

		// Token: 0x04001EF7 RID: 7927
		private MessageParserConfig ab;

		// Token: 0x04001EF8 RID: 7928
		private int ac;

		// Token: 0x04001EF9 RID: 7929
		private int ad;

		// Token: 0x04001EFA RID: 7930
		private bool ae = true;

		// Token: 0x04001EFB RID: 7931
		private TimeStampCollection af;

		// Token: 0x04001EFC RID: 7932
		private string ag;

		// Token: 0x04001EFD RID: 7933
		private string ah;
	}
}
