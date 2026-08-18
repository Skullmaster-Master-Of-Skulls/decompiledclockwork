using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000430 RID: 1072
	[TypeConverter(typeof(HorizontalAlignConverter))]
	public enum HorizontalAlign
	{
		// Token: 0x0400218A RID: 8586
		NotSet,
		// Token: 0x0400218B RID: 8587
		Left,
		// Token: 0x0400218C RID: 8588
		Center,
		// Token: 0x0400218D RID: 8589
		Right,
		// Token: 0x0400218E RID: 8590
		Justify
	}
}
