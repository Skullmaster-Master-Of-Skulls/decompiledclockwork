using System;

namespace TechnoPro.Common.Public.Entities.MailMergeEntities
{
	// Token: 0x020002C4 RID: 708
	public class MailMergeTemplate
	{
		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x0600158C RID: 5516 RVA: 0x0001AF00 File Offset: 0x00019100
		// (set) Token: 0x0600158D RID: 5517 RVA: 0x0001AF08 File Offset: 0x00019108
		public string Template { get; set; }

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x0600158E RID: 5518 RVA: 0x0001AF11 File Offset: 0x00019111
		// (set) Token: 0x0600158F RID: 5519 RVA: 0x0001AF19 File Offset: 0x00019119
		public string FontName { get; set; }

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06001590 RID: 5520 RVA: 0x0001AF22 File Offset: 0x00019122
		// (set) Token: 0x06001591 RID: 5521 RVA: 0x0001AF2A File Offset: 0x0001912A
		public int FontSize { get; set; }

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06001592 RID: 5522 RVA: 0x0001AF33 File Offset: 0x00019133
		// (set) Token: 0x06001593 RID: 5523 RVA: 0x0001AF3B File Offset: 0x0001913B
		public bool AllCaps { get; set; }
	}
}
