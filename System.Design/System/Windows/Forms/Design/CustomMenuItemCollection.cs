using System;
using System.Collections;

namespace System.Windows.Forms.Design
{
	// Token: 0x020001D5 RID: 469
	internal class CustomMenuItemCollection : CollectionBase
	{
		// Token: 0x0600122A RID: 4650 RVA: 0x00059EF8 File Offset: 0x00058EF8
		public int Add(ToolStripItem value)
		{
			return base.List.Add(value);
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00059F08 File Offset: 0x00058F08
		public void AddRange(ToolStripItem[] value)
		{
			for (int i = 0; i < value.Length; i++)
			{
				this.Add(value[i]);
			}
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00059F2D File Offset: 0x00058F2D
		public virtual void RefreshItems()
		{
		}
	}
}
