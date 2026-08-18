using System;
using TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Models.StudentRequests;
using TechnoPro.Common.UI.Web.Entity.AlternateFormat;

namespace TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Models
{
	// Token: 0x0200016A RID: 362
	public class MediaContentAddedViewModel
	{
		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x000490DB File Offset: 0x000472DB
		// (set) Token: 0x06000ACB RID: 2763 RVA: 0x000490E3 File Offset: 0x000472E3
		public MediaContentWebView ContentAdded { get; set; }

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x000490EC File Offset: 0x000472EC
		// (set) Token: 0x06000ACD RID: 2765 RVA: 0x000490F4 File Offset: 0x000472F4
		public PendingRequestsCart Cart { get; set; }
	}
}
