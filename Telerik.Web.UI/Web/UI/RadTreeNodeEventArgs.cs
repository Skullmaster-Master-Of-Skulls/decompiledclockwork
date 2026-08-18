using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001B62 RID: 7010
	public class RadTreeNodeEventArgs : EventArgs
	{
		// Token: 0x170052D4 RID: 21204
		// (get) Token: 0x06010F85 RID: 69509 RVA: 0x003C1463 File Offset: 0x003BF663
		// (set) Token: 0x06010F86 RID: 69510 RVA: 0x003C146B File Offset: 0x003BF66B
		public RadTreeNode Node
		{
			get
			{
				return this._node;
			}
			set
			{
				this._node = value;
			}
		}

		// Token: 0x06010F87 RID: 69511 RVA: 0x003C1474 File Offset: 0x003BF674
		public RadTreeNodeEventArgs(RadTreeNode node)
		{
			this._node = node;
		}

		// Token: 0x04004BFA RID: 19450
		private RadTreeNode _node;
	}
}
