using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000682 RID: 1666
	[TypeConverter(typeof(VerticalAlignConverter))]
	public enum VerticalAlign
	{
		// Token: 0x04002DCD RID: 11725
		NotSet,
		// Token: 0x04002DCE RID: 11726
		Top,
		// Token: 0x04002DCF RID: 11727
		Middle,
		// Token: 0x04002DD0 RID: 11728
		Bottom
	}
}
