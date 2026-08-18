using System;
using System.Collections;
using a.b;
using MailBee.Mime;

namespace MailBee.Outlook
{
	// Token: 0x020005AB RID: 1451
	public class PstActivity : PstItem
	{
		// Token: 0x060030D4 RID: 12500 RVA: 0x000E400C File Offset: 0x000E300C
		internal PstActivity(fm A_0) : base(A_0)
		{
			this.c = "X-Activity-";
			this.b["LogType"] = A_0.g();
			this.b["LogStart"] = A_0.f();
			this.b["LogDuration"] = A_0.j();
			this.b["LogEnd"] = A_0.h();
			this.b["LogFlags"] = A_0.a();
			this.b["DocumentPrinted"] = A_0.i();
			this.b["DocumentSaved"] = A_0.b();
			this.b["DocumentRouted"] = A_0.c();
			this.b["DocumentPosted"] = A_0.d();
			this.b["LogTypeDesc"] = A_0.e();
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x060030D5 RID: 12501 RVA: 0x000E412F File Offset: 0x000E312F
		public override PstItemType PstType
		{
			get
			{
				return base.PstType;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x060030D6 RID: 12502 RVA: 0x000E4137 File Offset: 0x000E3137
		public override Hashtable PstFields
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x000E4140 File Offset: 0x000E3140
		public override MailMessage GetAsMailMessage()
		{
			MailMessage a_ = new MailMessage();
			return base.a(a_);
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x060030D8 RID: 12504 RVA: 0x000E415A File Offset: 0x000E315A
		public override int PstID
		{
			get
			{
				return base.PstID;
			}
		}
	}
}
