using System;

namespace TechnoPro.Common.Public.Entities
{
	// Token: 0x020000F3 RID: 243
	public class MailingInfo : BusinessBase<string>
	{
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0000EB04 File Offset: 0x0000CD04
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public string From
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0000EB1C File Offset: 0x0000CD1C
		// (set) Token: 0x060005A7 RID: 1447 RVA: 0x0000EB24 File Offset: 0x0000CD24
		public string To { get; set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0000EB2D File Offset: 0x0000CD2D
		// (set) Token: 0x060005A9 RID: 1449 RVA: 0x0000EB35 File Offset: 0x0000CD35
		public string Subject { get; set; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x0000EB3E File Offset: 0x0000CD3E
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x0000EB46 File Offset: 0x0000CD46
		public string Body { get; set; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x0000EB4F File Offset: 0x0000CD4F
		// (set) Token: 0x060005AD RID: 1453 RVA: 0x0000EB57 File Offset: 0x0000CD57
		public string Cc { get; set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x0000EB60 File Offset: 0x0000CD60
		// (set) Token: 0x060005AF RID: 1455 RVA: 0x0000EB68 File Offset: 0x0000CD68
		public string Bcc { get; set; }
	}
}
