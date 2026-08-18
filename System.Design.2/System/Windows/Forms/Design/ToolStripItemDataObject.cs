using System;
using System.Collections;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200035B RID: 859
	internal class ToolStripItemDataObject : DataObject
	{
		// Token: 0x0600228D RID: 8845 RVA: 0x000D4E75 File Offset: 0x000D3075
		internal ToolStripItemDataObject(ArrayList dragComponents, ToolStripItem primarySelection, ToolStrip owner)
		{
			this.dragComponents = dragComponents;
			this.owner = owner;
			this.primarySelection = primarySelection;
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x0600228E RID: 8846 RVA: 0x000D4E92 File Offset: 0x000D3092
		internal ArrayList DragComponents
		{
			get
			{
				return this.dragComponents;
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x0600228F RID: 8847 RVA: 0x000D4E9A File Offset: 0x000D309A
		internal ToolStrip Owner
		{
			get
			{
				return this.owner;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06002290 RID: 8848 RVA: 0x000D4EA2 File Offset: 0x000D30A2
		internal ToolStripItem PrimarySelection
		{
			get
			{
				return this.primarySelection;
			}
		}

		// Token: 0x040019BC RID: 6588
		private ArrayList dragComponents;

		// Token: 0x040019BD RID: 6589
		private ToolStrip owner;

		// Token: 0x040019BE RID: 6590
		private ToolStripItem primarySelection;
	}
}
