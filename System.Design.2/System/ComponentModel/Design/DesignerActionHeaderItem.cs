using System;

namespace System.ComponentModel.Design
{
	// Token: 0x0200019B RID: 411
	public sealed class DesignerActionHeaderItem : DesignerActionTextItem
	{
		// Token: 0x06000F26 RID: 3878 RVA: 0x0005761C File Offset: 0x0005581C
		public DesignerActionHeaderItem(string displayName) : base(displayName, displayName)
		{
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x00057626 File Offset: 0x00055826
		public DesignerActionHeaderItem(string displayName, string category) : base(displayName, category)
		{
		}
	}
}
