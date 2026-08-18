using System;

namespace TechnoPro.Common.Public.Entities.MailMergeEntities
{
	// Token: 0x020002C2 RID: 706
	public class MailMergeDefaultPrinterSettings
	{
		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06001571 RID: 5489 RVA: 0x0001AD5B File Offset: 0x00018F5B
		// (set) Token: 0x06001572 RID: 5490 RVA: 0x0001AD63 File Offset: 0x00018F63
		public string PrinterName { get; set; }

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06001573 RID: 5491 RVA: 0x0001AD6C File Offset: 0x00018F6C
		// (set) Token: 0x06001574 RID: 5492 RVA: 0x0001AD74 File Offset: 0x00018F74
		public string DefaultPageSize { get; set; }

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06001575 RID: 5493 RVA: 0x0001AD7D File Offset: 0x00018F7D
		// (set) Token: 0x06001576 RID: 5494 RVA: 0x0001AD85 File Offset: 0x00018F85
		public ePageOrientation Orientation { get; set; }

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06001577 RID: 5495 RVA: 0x0001AD8E File Offset: 0x00018F8E
		// (set) Token: 0x06001578 RID: 5496 RVA: 0x0001AD96 File Offset: 0x00018F96
		public int CopyCount { get; set; }

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06001579 RID: 5497 RVA: 0x0001AD9F File Offset: 0x00018F9F
		// (set) Token: 0x0600157A RID: 5498 RVA: 0x0001ADA7 File Offset: 0x00018FA7
		public int MarginLeft { get; set; }

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x0600157B RID: 5499 RVA: 0x0001ADB0 File Offset: 0x00018FB0
		// (set) Token: 0x0600157C RID: 5500 RVA: 0x0001ADB8 File Offset: 0x00018FB8
		public int MarginRight { get; set; }

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x0600157D RID: 5501 RVA: 0x0001ADC1 File Offset: 0x00018FC1
		// (set) Token: 0x0600157E RID: 5502 RVA: 0x0001ADC9 File Offset: 0x00018FC9
		public int MarginTop { get; set; }

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x0600157F RID: 5503 RVA: 0x0001ADD2 File Offset: 0x00018FD2
		// (set) Token: 0x06001580 RID: 5504 RVA: 0x0001ADDA File Offset: 0x00018FDA
		public int MarginBottom { get; set; }
	}
}
