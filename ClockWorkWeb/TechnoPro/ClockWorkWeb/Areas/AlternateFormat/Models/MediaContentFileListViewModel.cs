using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.UI.Web.Entity.AlternateFormat;

namespace TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Models
{
	// Token: 0x02000170 RID: 368
	public class MediaContentFileListViewModel
	{
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x0004918E File Offset: 0x0004738E
		// (set) Token: 0x06000AE5 RID: 2789 RVA: 0x00049196 File Offset: 0x00047396
		public MediaContentWebView MediaContent { get; set; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x0004919F File Offset: 0x0004739F
		// (set) Token: 0x06000AE7 RID: 2791 RVA: 0x000491A7 File Offset: 0x000473A7
		public IList<MediaContentFileWithoutDataWebView> MediaContentFileList { get; set; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x000491B0 File Offset: 0x000473B0
		// (set) Token: 0x06000AE9 RID: 2793 RVA: 0x000491B8 File Offset: 0x000473B8
		public int ProofOfPurchaseId { get; set; }

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x000491C1 File Offset: 0x000473C1
		// (set) Token: 0x06000AEB RID: 2795 RVA: 0x000491C9 File Offset: 0x000473C9
		public eStudentMediaContentFileStatus? FileStatus { get; set; }
	}
}
