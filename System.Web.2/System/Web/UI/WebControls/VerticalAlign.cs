using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000510 RID: 1296
	[TypeConverter(typeof(VerticalAlignConverter))]
	public enum VerticalAlign
	{
		// Token: 0x04002501 RID: 9473
		NotSet,
		// Token: 0x04002502 RID: 9474
		Top,
		// Token: 0x04002503 RID: 9475
		Middle,
		// Token: 0x04002504 RID: 9476
		Bottom
	}
}
