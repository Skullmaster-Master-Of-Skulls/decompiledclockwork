using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000298 RID: 664
	[Flags]
	public enum EditorFilters
	{
		// Token: 0x0400062F RID: 1583
		None = 0,
		// Token: 0x04000630 RID: 1584
		RemoveScripts = 1,
		// Token: 0x04000631 RID: 1585
		MakeUrlsAbsolute = 2,
		// Token: 0x04000632 RID: 1586
		FixUlBoldItalic = 4,
		// Token: 0x04000633 RID: 1587
		FixEnclosingP = 8,
		// Token: 0x04000634 RID: 1588
		IECleanAnchors = 16,
		// Token: 0x04000635 RID: 1589
		MozEmStrong = 32,
		// Token: 0x04000636 RID: 1590
		ConvertFontToSpan = 64,
		// Token: 0x04000637 RID: 1591
		ConvertToXhtml = 128,
		// Token: 0x04000638 RID: 1592
		IndentHTMLContent = 256,
		// Token: 0x04000639 RID: 1593
		EncodeScripts = 512,
		// Token: 0x0400063A RID: 1594
		OptimizeSpans = 1024,
		// Token: 0x0400063B RID: 1595
		ConvertCharactersToEntities = 2048,
		// Token: 0x0400063C RID: 1596
		PdfExportFilter = 4096,
		// Token: 0x0400063D RID: 1597
		ConvertInlineStylesToAttributes = 8192,
		// Token: 0x0400063E RID: 1598
		ConvertTags = 16384,
		// Token: 0x0400063F RID: 1599
		StripCssExpressions = 32768,
		// Token: 0x04000640 RID: 1600
		StripDomEventAttributes = 65536,
		// Token: 0x04000641 RID: 1601
		RemoveExtraBreaks = 131072,
		// Token: 0x04000642 RID: 1602
		DefaultFilters = 184309
	}
}
