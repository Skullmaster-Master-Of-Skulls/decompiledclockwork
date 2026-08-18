using System;

namespace TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Models
{
	// Token: 0x02000176 RID: 374
	public class StudentRequestsToolStripViewModel
	{
		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x000492C0 File Offset: 0x000474C0
		// (set) Token: 0x06000B0F RID: 2831 RVA: 0x000492C8 File Offset: 0x000474C8
		public string MediaContentUniqueId { get; set; }

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x000492D1 File Offset: 0x000474D1
		// (set) Token: 0x06000B11 RID: 2833 RVA: 0x000492D9 File Offset: 0x000474D9
		public string MediaContentTitle { get; set; }

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x000492E2 File Offset: 0x000474E2
		// (set) Token: 0x06000B13 RID: 2835 RVA: 0x000492EA File Offset: 0x000474EA
		public int ProofOfPurchaseId { get; set; }

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x000492F3 File Offset: 0x000474F3
		// (set) Token: 0x06000B15 RID: 2837 RVA: 0x000492FB File Offset: 0x000474FB
		public bool ProofOfPurchaseRequired { get; set; }

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x00049304 File Offset: 0x00047504
		public bool ProofOfPurchaseAvailable
		{
			get
			{
				return this.ProofOfPurchaseId > 0;
			}
		}
	}
}
