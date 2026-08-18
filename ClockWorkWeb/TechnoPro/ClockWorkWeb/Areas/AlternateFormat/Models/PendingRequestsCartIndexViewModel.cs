using System;
using TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Models.StudentRequests;

namespace TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Models
{
	// Token: 0x0200016C RID: 364
	public class PendingRequestsCartIndexViewModel : AlternateFormatBaseViewModel
	{
		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x00049117 File Offset: 0x00047317
		// (set) Token: 0x06000AD3 RID: 2771 RVA: 0x0004911F File Offset: 0x0004731F
		public PendingRequestsCart Cart { get; set; }

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x00049128 File Offset: 0x00047328
		// (set) Token: 0x06000AD5 RID: 2773 RVA: 0x00049130 File Offset: 0x00047330
		public string ReturnUrl { get; set; }
	}
}
