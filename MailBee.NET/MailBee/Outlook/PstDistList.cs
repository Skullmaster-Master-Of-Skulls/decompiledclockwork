using System;
using System.Collections;
using System.Text;
using a.b;
using MailBee.Mime;

namespace MailBee.Outlook
{
	// Token: 0x020005B0 RID: 1456
	public class PstDistList : PstItem
	{
		// Token: 0x060030F5 RID: 12533 RVA: 0x000E61BA File Offset: 0x000E51BA
		internal PstDistList(el A_0) : base(A_0)
		{
			this.c = "X-DistList-";
			this.b["DisplayName"] = A_0.kn();
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x060030F6 RID: 12534 RVA: 0x000E61E4 File Offset: 0x000E51E4
		public override PstItemType PstType
		{
			get
			{
				return base.PstType;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x060030F7 RID: 12535 RVA: 0x000E61EC File Offset: 0x000E51EC
		public override Hashtable PstFields
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x060030F8 RID: 12536 RVA: 0x000E61F4 File Offset: 0x000E51F4
		public override int PstID
		{
			get
			{
				return base.PstID;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x060030F9 RID: 12537 RVA: 0x000E61FC File Offset: 0x000E51FC
		public string DisplayName
		{
			get
			{
				return this.a.kn();
			}
		}

		// Token: 0x060030FA RID: 12538 RVA: 0x000E620C File Offset: 0x000E520C
		public override MailMessage GetAsMailMessage()
		{
			MailMessage mailMessage = new MailMessage();
			CollectionBase members = this.GetMembers();
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in members)
			{
				EmailAddress emailAddress = (EmailAddress)obj;
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append("\r\n");
				}
				stringBuilder.Append(emailAddress.ToString());
			}
			mailMessage.BodyPlainText = stringBuilder.ToString();
			return base.a(mailMessage);
		}

		// Token: 0x060030FB RID: 12539 RVA: 0x000E62A4 File Offset: 0x000E52A4
		public EmailAddressCollection GetMembers()
		{
			EmailAddressCollection emailAddressCollection = new EmailAddressCollection();
			object[] array = ((el)this.a).a();
			for (int i = 0; i < array.Length; i++)
			{
				el.a a = array[i] as el.a;
				if (a != null)
				{
					emailAddressCollection.Add(a.c ?? string.Empty, a.a ?? string.Empty);
				}
			}
			return emailAddressCollection;
		}
	}
}
