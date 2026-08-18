using System;

namespace TechnoPro.Common.Public.Entities.MailMergeEntities.Labels
{
	// Token: 0x020002C7 RID: 711
	public class LabelTemplate
	{
		// Token: 0x0600159E RID: 5534 RVA: 0x0001B021 File Offset: 0x00019221
		public LabelTemplate()
		{
			this.DefaultPrinterSettings = new MailMergeDefaultPrinterSettings();
			this.Template = new MailMergeTemplate();
			this.Name = "";
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x0600159F RID: 5535 RVA: 0x0001B04F File Offset: 0x0001924F
		// (set) Token: 0x060015A0 RID: 5536 RVA: 0x0001B057 File Offset: 0x00019257
		public string Name { get; set; }

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x060015A1 RID: 5537 RVA: 0x0001B060 File Offset: 0x00019260
		// (set) Token: 0x060015A2 RID: 5538 RVA: 0x0001B068 File Offset: 0x00019268
		public MailMergeTemplate Template { get; set; }

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x060015A3 RID: 5539 RVA: 0x0001B071 File Offset: 0x00019271
		// (set) Token: 0x060015A4 RID: 5540 RVA: 0x0001B079 File Offset: 0x00019279
		public MailMergeDefaultPrinterSettings DefaultPrinterSettings { get; set; }
	}
}
