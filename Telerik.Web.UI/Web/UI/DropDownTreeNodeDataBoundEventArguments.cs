using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000B31 RID: 2865
	public class DropDownTreeNodeDataBoundEventArguments : EventArgs
	{
		// Token: 0x06006BC1 RID: 27585 RVA: 0x00191587 File Offset: 0x0018F787
		public DropDownTreeNodeDataBoundEventArguments(DropDownTreeNode dropDownTreeNode)
		{
			this._dropDownTreeNode = dropDownTreeNode;
		}

		// Token: 0x17002350 RID: 9040
		// (get) Token: 0x06006BC2 RID: 27586 RVA: 0x00191596 File Offset: 0x0018F796
		public DropDownTreeNode DropDownTreeNode
		{
			get
			{
				return this._dropDownTreeNode;
			}
		}

		// Token: 0x04001D08 RID: 7432
		private DropDownTreeNode _dropDownTreeNode;
	}
}
