using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200029A RID: 666
	[Flags]
	public enum EditorStripFormattingOptions
	{
		// Token: 0x04000649 RID: 1609
		None = 0,
		// Token: 0x0400064A RID: 1610
		NoneSupressCleanMessage = 1,
		// Token: 0x0400064B RID: 1611
		MSWord = 2,
		// Token: 0x0400064C RID: 1612
		MSWordNoFonts = 4,
		// Token: 0x0400064D RID: 1613
		MSWordRemoveAll = 8,
		// Token: 0x0400064E RID: 1614
		Css = 16,
		// Token: 0x0400064F RID: 1615
		Font = 32,
		// Token: 0x04000650 RID: 1616
		Span = 64,
		// Token: 0x04000651 RID: 1617
		AllExceptNewLines = 128,
		// Token: 0x04000652 RID: 1618
		ConvertWordLists = 256,
		// Token: 0x04000653 RID: 1619
		All = 512,
		// Token: 0x04000654 RID: 1620
		MSWordNoMargins = 1024
	}
}
