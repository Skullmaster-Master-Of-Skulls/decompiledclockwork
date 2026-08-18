using System;
using TechnoPro.Common.UI.Web.Entity.AlternateFormat;

namespace TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Models
{
	// Token: 0x02000169 RID: 361
	public class MakeRequestViewModel
	{
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x000490B9 File Offset: 0x000472B9
		// (set) Token: 0x06000AC6 RID: 2758 RVA: 0x000490C1 File Offset: 0x000472C1
		public MediaContentWebView MediaContent { get; set; }

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x000490CA File Offset: 0x000472CA
		// (set) Token: 0x06000AC8 RID: 2760 RVA: 0x000490D2 File Offset: 0x000472D2
		public string ReturnUrl { get; set; }
	}
}
