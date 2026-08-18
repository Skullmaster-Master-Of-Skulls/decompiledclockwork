using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x020011AC RID: 4524
	[ToolboxItem(false)]
	public class GridFilterMenu : GridContextMenu
	{
		// Token: 0x0600B9DA RID: 47578 RVA: 0x002940AF File Offset: 0x002922AF
		public GridFilterMenu() : this(null)
		{
		}

		// Token: 0x0600B9DB RID: 47579 RVA: 0x002940B8 File Offset: 0x002922B8
		public GridFilterMenu(RadGrid ownerGrid)
		{
			this._ownerGrid = ownerGrid;
		}

		// Token: 0x04003120 RID: 12576
		private readonly RadGrid _ownerGrid;
	}
}
