using System;
using System.Text;
using a.l;
using MailBee.Mime;
using Microsoft.Exchange.WebServices.Data;

namespace MailBee.EwsMail
{
	// Token: 0x02000526 RID: 1318
	public class EwsItem
	{
		// Token: 0x06002B52 RID: 11090 RVA: 0x000CC74A File Offset: 0x000CB74A
		internal EwsItem(Item A_0, ExchangeVersion A_1)
		{
			this.k = A_0;
			this.j = (this.k as EmailMessage);
			this.m = A_1;
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x000CC778 File Offset: 0x000CB778
		public EwsItem(ItemId id)
		{
			if (id == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			this.n = id;
			this.m = ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x06002B54 RID: 11092 RVA: 0x000CC7A0 File Offset: 0x000CB7A0
		public EwsItem(string uniqueId)
		{
			if (uniqueId == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			this.n = new ItemId(uniqueId);
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x000CC7C8 File Offset: 0x000CB7C8
		private MailBee.Mime.EmailAddressCollection a(Microsoft.Exchange.WebServices.Data.EmailAddressCollection A_0)
		{
			MailBee.Mime.EmailAddressCollection emailAddressCollection = new MailBee.Mime.EmailAddressCollection();
			foreach (Microsoft.Exchange.WebServices.Data.EmailAddress emailAddress in A_0)
			{
				emailAddressCollection.Add(emailAddress.Address, emailAddress.Name);
			}
			return emailAddressCollection;
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06002B56 RID: 11094 RVA: 0x000CC824 File Offset: 0x000CB824
		public MailBee.Mime.EmailAddressCollection To
		{
			get
			{
				if (this.e == null && this.j != null && this.j.ToRecipients != null)
				{
					this.e = this.a(this.j.ToRecipients);
				}
				return this.e;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06002B57 RID: 11095 RVA: 0x000CC860 File Offset: 0x000CB860
		public MailBee.Mime.EmailAddressCollection Cc
		{
			get
			{
				if (this.f == null && this.j != null && this.j.CcRecipients != null)
				{
					this.f = this.a(this.j.CcRecipients);
				}
				return this.f;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06002B58 RID: 11096 RVA: 0x000CC89C File Offset: 0x000CB89C
		public MailBee.Mime.EmailAddressCollection Bcc
		{
			get
			{
				if (this.g == null && this.j != null && this.j.BccRecipients != null)
				{
					this.g = this.a(this.j.BccRecipients);
				}
				return this.g;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06002B59 RID: 11097 RVA: 0x000CC8D8 File Offset: 0x000CB8D8
		public MailBee.Mime.EmailAddressCollection ReplyTo
		{
			get
			{
				if (this.h == null && this.j != null && this.j.ReplyTo != null)
				{
					this.h = this.a(this.j.ReplyTo);
				}
				return this.h;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06002B5A RID: 11098 RVA: 0x000CC914 File Offset: 0x000CB914
		public string Subject
		{
			get
			{
				if (this.k != null)
				{
					return this.k.Subject;
				}
				return null;
			}
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x000CC92B File Offset: 0x000CB92B
		private MailBee.Mime.EmailAddress a(Microsoft.Exchange.WebServices.Data.EmailAddress A_0)
		{
			return new MailBee.Mime.EmailAddress(A_0.Address, A_0.Name);
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06002B5C RID: 11100 RVA: 0x000CC93E File Offset: 0x000CB93E
		public MailBee.Mime.EmailAddress From
		{
			get
			{
				if (this.d == null && this.j != null && this.j.From != null)
				{
					this.d = this.a(this.j.From);
				}
				return this.d;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06002B5D RID: 11101 RVA: 0x000CC97C File Offset: 0x000CB97C
		public string BodyHtmlText
		{
			get
			{
				if (this.a == null && this.k != null)
				{
					byte[] bytes = null;
					MessageBody messageBody;
					if (this.k.TryGetProperty<MessageBody>(ItemSchema.Body, out messageBody) && this.k.Body != null && this.k.Body.BodyType == BodyType.HTML)
					{
						this.a = this.k.Body.Text;
					}
					else if (this.k.TryGetProperty<byte[]>(global::a.l.d.b, out bytes))
					{
						this.a = Encoding.GetEncoding(this.CodePage).GetString(bytes);
					}
					else if (this.MailBeeMessage != null)
					{
						this.a = this.MailBeeMessage.BodyHtmlText;
					}
					else
					{
						this.a = string.Empty;
					}
				}
				return this.a;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06002B5E RID: 11102 RVA: 0x000CCA48 File Offset: 0x000CBA48
		public string BodyPlainText
		{
			get
			{
				if (this.b == null && this.k != null)
				{
					MessageBody messageBody;
					if (this.m >= ExchangeVersion.Exchange2013 && this.k.TryGetProperty<MessageBody>(ItemSchema.TextBody, out messageBody) && this.k.TextBody != null && this.k.TextBody.BodyType == BodyType.Text)
					{
						this.b = this.k.TextBody.Text;
					}
					else if (this.k.TryGetProperty<MessageBody>(ItemSchema.Body, out messageBody) && this.k.Body != null && this.k.Body.BodyType == BodyType.Text)
					{
						this.b = this.k.Body.Text;
					}
					else if (!this.k.TryGetProperty<string>(global::a.l.d.c, out this.b))
					{
						if (this.MailBeeMessage != null)
						{
							this.b = this.MailBeeMessage.BodyPlainText;
						}
						else
						{
							this.b = string.Empty;
						}
					}
				}
				return this.b;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06002B5F RID: 11103 RVA: 0x000CCB53 File Offset: 0x000CBB53
		public int CodePage
		{
			get
			{
				if (this.c < 0 && this.k != null && !this.k.TryGetProperty<int>(global::a.l.d.e, out this.c))
				{
					this.c = 0;
				}
				return this.c;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06002B60 RID: 11104 RVA: 0x000CCB8B File Offset: 0x000CBB8B
		public int Size
		{
			get
			{
				if (this.k != null)
				{
					return this.k.Size;
				}
				return -1;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06002B61 RID: 11105 RVA: 0x000CCBA2 File Offset: 0x000CBBA2
		// (set) Token: 0x06002B62 RID: 11106 RVA: 0x000CCBAA File Offset: 0x000CBBAA
		public bool DatesAsUtc
		{
			get
			{
				return this.i;
			}
			set
			{
				this.i = value;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06002B63 RID: 11107 RVA: 0x000CCBB4 File Offset: 0x000CBBB4
		public DateTime Date
		{
			get
			{
				if (this.k == null)
				{
					return DateTime.MinValue;
				}
				if (this.i)
				{
					return this.k.DateTimeCreated.ToUniversalTime();
				}
				return this.k.DateTimeCreated;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06002B64 RID: 11108 RVA: 0x000CCBF8 File Offset: 0x000CBBF8
		public DateTime DateReceived
		{
			get
			{
				if (this.k == null)
				{
					return DateTime.MinValue;
				}
				if (this.i)
				{
					return this.k.DateTimeCreated.ToUniversalTime();
				}
				return this.k.DateTimeReceived.ToUniversalTime();
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06002B65 RID: 11109 RVA: 0x000CCC44 File Offset: 0x000CBC44
		public MailMessage MailBeeMessage
		{
			get
			{
				if (this.l == null && this.j != null && this.j.GetLoadedPropertyDefinitions().Contains(ItemSchema.MimeContent))
				{
					this.l = new MailMessage(this.j.MimeContent.Content);
				}
				return this.l;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06002B66 RID: 11110 RVA: 0x000CCC99 File Offset: 0x000CBC99
		public EmailMessage NativeMessage
		{
			get
			{
				return this.j;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06002B67 RID: 11111 RVA: 0x000CCCA1 File Offset: 0x000CBCA1
		public Item NativeItem
		{
			get
			{
				return this.k;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06002B68 RID: 11112 RVA: 0x000CCCA9 File Offset: 0x000CBCA9
		public ItemId Id
		{
			get
			{
				if (this.k != null)
				{
					return this.k.Id;
				}
				return this.n;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06002B69 RID: 11113 RVA: 0x000CCCC8 File Offset: 0x000CBCC8
		public string UniqueId
		{
			get
			{
				ItemId id = this.Id;
				if (id != null)
				{
					return id.UniqueId;
				}
				return null;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06002B6A RID: 11114 RVA: 0x000CCCE7 File Offset: 0x000CBCE7
		// (set) Token: 0x06002B6B RID: 11115 RVA: 0x000CCD15 File Offset: 0x000CBD15
		public bool IsRead
		{
			get
			{
				return this.j != null && this.j.GetLoadedPropertyDefinitions().Contains(EmailMessageSchema.IsRead) && this.j.IsRead;
			}
			set
			{
				if (this.j == null)
				{
					throw new MailBeeInvalidStateException(11);
				}
				this.j.IsRead = value;
			}
		}

		// Token: 0x04001DDF RID: 7647
		private string a;

		// Token: 0x04001DE0 RID: 7648
		private string b;

		// Token: 0x04001DE1 RID: 7649
		private int c = -1;

		// Token: 0x04001DE2 RID: 7650
		private MailBee.Mime.EmailAddress d;

		// Token: 0x04001DE3 RID: 7651
		private MailBee.Mime.EmailAddressCollection e;

		// Token: 0x04001DE4 RID: 7652
		private MailBee.Mime.EmailAddressCollection f;

		// Token: 0x04001DE5 RID: 7653
		private MailBee.Mime.EmailAddressCollection g;

		// Token: 0x04001DE6 RID: 7654
		private MailBee.Mime.EmailAddressCollection h;

		// Token: 0x04001DE7 RID: 7655
		private bool i;

		// Token: 0x04001DE8 RID: 7656
		private EmailMessage j;

		// Token: 0x04001DE9 RID: 7657
		private Item k;

		// Token: 0x04001DEA RID: 7658
		private MailMessage l;

		// Token: 0x04001DEB RID: 7659
		private ExchangeVersion m;

		// Token: 0x04001DEC RID: 7660
		private ItemId n;
	}
}
