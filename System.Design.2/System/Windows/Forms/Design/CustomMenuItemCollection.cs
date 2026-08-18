using System;
using System.Collections;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002B2 RID: 690
	internal class CustomMenuItemCollection : CollectionBase
	{
		// Token: 0x06001B67 RID: 7015 RVA: 0x0005799D File Offset: 0x00055B9D
		public int Add(ToolStripItem value)
		{
			return base.List.Add(value);
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x000A264C File Offset: 0x000A084C
		public void AddRange(ToolStripItem[] value)
		{
			for (int i = 0; i < value.Length; i++)
			{
				this.Add(value[i]);
			}
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x00003937 File Offset: 0x00001B37
		public virtual void RefreshItems()
		{
		}
	}
}
