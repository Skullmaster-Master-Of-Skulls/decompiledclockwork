using System;
using System.Collections.Generic;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002AC RID: 684
	internal class ContextMenuStripGroup
	{
		// Token: 0x06001AC0 RID: 6848 RVA: 0x0009C41A File Offset: 0x0009A61A
		public ContextMenuStripGroup(string name)
		{
			this.name = name;
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001AC1 RID: 6849 RVA: 0x0009C429 File Offset: 0x0009A629
		public List<ToolStripItem> Items
		{
			get
			{
				if (this.items == null)
				{
					this.items = new List<ToolStripItem>();
				}
				return this.items;
			}
		}

		// Token: 0x0400160E RID: 5646
		private List<ToolStripItem> items;

		// Token: 0x0400160F RID: 5647
		private string name;
	}
}
