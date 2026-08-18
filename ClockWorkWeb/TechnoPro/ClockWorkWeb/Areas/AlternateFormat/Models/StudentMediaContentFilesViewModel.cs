using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkWeb.Models;

namespace TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Models
{
	// Token: 0x02000171 RID: 369
	public class StudentMediaContentFilesViewModel
	{
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x000491D2 File Offset: 0x000473D2
		// (set) Token: 0x06000AEE RID: 2798 RVA: 0x000491DA File Offset: 0x000473DA
		public string SelectedTermId { get; set; }

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x000491E3 File Offset: 0x000473E3
		// (set) Token: 0x06000AF0 RID: 2800 RVA: 0x000491EB File Offset: 0x000473EB
		public IList<MediaContentFileListViewModel> MediaContentList { get; set; }

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x000491F4 File Offset: 0x000473F4
		// (set) Token: 0x06000AF2 RID: 2802 RVA: 0x000491FC File Offset: 0x000473FC
		public PagingInfo PagingInfo { get; set; }
	}
}
