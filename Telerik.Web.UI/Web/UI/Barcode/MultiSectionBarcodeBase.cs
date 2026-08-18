using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x02000A00 RID: 2560
	internal abstract class MultiSectionBarcodeBase : RadBarcodeBase
	{
		// Token: 0x17001FE0 RID: 8160
		// (get) Token: 0x06006131 RID: 24881 RVA: 0x0016DA29 File Offset: 0x0016BC29
		// (set) Token: 0x06006132 RID: 24882 RVA: 0x0016DA31 File Offset: 0x0016BC31
		[Description("Gets or sets the LeftText")]
		internal string LeftText { get; set; }

		// Token: 0x17001FE1 RID: 8161
		// (get) Token: 0x06006133 RID: 24883 RVA: 0x0016DA3A File Offset: 0x0016BC3A
		// (set) Token: 0x06006134 RID: 24884 RVA: 0x0016DA42 File Offset: 0x0016BC42
		[Description("Gets or sets the RightText")]
		internal string RightText { get; set; }
	}
}
