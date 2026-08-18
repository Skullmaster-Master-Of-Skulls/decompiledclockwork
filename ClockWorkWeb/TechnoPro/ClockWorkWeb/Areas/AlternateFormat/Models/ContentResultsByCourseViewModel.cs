using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkWeb.Models;
using TechnoPro.Common.UI.Web.Entity.AlternateFormat;

namespace TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Models
{
	// Token: 0x02000168 RID: 360
	public class ContentResultsByCourseViewModel
	{
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x00049086 File Offset: 0x00047286
		// (set) Token: 0x06000ABF RID: 2751 RVA: 0x0004908E File Offset: 0x0004728E
		public int SelectedCourseId { get; set; }

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x00049097 File Offset: 0x00047297
		// (set) Token: 0x06000AC1 RID: 2753 RVA: 0x0004909F File Offset: 0x0004729F
		public IList<MediaContentWebView> MediaContentList { get; set; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x000490A8 File Offset: 0x000472A8
		// (set) Token: 0x06000AC3 RID: 2755 RVA: 0x000490B0 File Offset: 0x000472B0
		public PagingInfo PagingInfo { get; set; }
	}
}
