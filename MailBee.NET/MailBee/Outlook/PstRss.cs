using System;
using System.Collections;
using a.b;
using MailBee.Mime;

namespace MailBee.Outlook
{
	// Token: 0x020005B9 RID: 1465
	public class PstRss : PstItem
	{
		// Token: 0x0600312A RID: 12586 RVA: 0x000E6A60 File Offset: 0x000E5A60
		internal PstRss(h5 A_0) : base(A_0)
		{
			this.c = "X-Rss-";
			this.b["PostRssChannel"] = A_0.f();
			this.b["PostRssChannelLink"] = A_0.b();
			this.b["PostRssItemLink"] = A_0.d();
			this.b["PostRssItemHash"] = A_0.e();
			this.b["PostRssItemGuid"] = A_0.a();
			this.b["PostRssItemXml"] = A_0.c();
			this.b["PostRssSubscription"] = A_0.g();
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x0600312B RID: 12587 RVA: 0x000E6B1E File Offset: 0x000E5B1E
		public override PstItemType PstType
		{
			get
			{
				return base.PstType;
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x0600312C RID: 12588 RVA: 0x000E6B26 File Offset: 0x000E5B26
		public override Hashtable PstFields
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x0600312D RID: 12589 RVA: 0x000E6B30 File Offset: 0x000E5B30
		public override MailMessage GetAsMailMessage()
		{
			MailMessage a_ = new MailMessage();
			return base.a(a_);
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x0600312E RID: 12590 RVA: 0x000E6B4A File Offset: 0x000E5B4A
		public override int PstID
		{
			get
			{
				return base.PstID;
			}
		}
	}
}
