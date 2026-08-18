using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02001B61 RID: 7009
	public class RadTreeNodeDragDropEventArgs : EventArgs
	{
		// Token: 0x06010F7C RID: 69500 RVA: 0x003C13E0 File Offset: 0x003BF5E0
		public RadTreeNodeDragDropEventArgs(IList<RadTreeNode> sourceNodes, RadTreeNode destinationNode, RadTreeViewDropPosition dropPosition)
		{
			this._sourceNodes = sourceNodes;
			this._destinationNode = destinationNode;
			this._dropPosition = dropPosition;
		}

		// Token: 0x06010F7D RID: 69501 RVA: 0x003C13FD File Offset: 0x003BF5FD
		public RadTreeNodeDragDropEventArgs(IList<RadTreeNode> sourceNodes, string htmlElementId)
		{
			this._sourceNodes = sourceNodes;
			this._htmlElementID = htmlElementId;
		}

		// Token: 0x170052CF RID: 21199
		// (get) Token: 0x06010F7E RID: 69502 RVA: 0x003C1413 File Offset: 0x003BF613
		public RadTreeNode SourceDragNode
		{
			get
			{
				if (this.DraggedNodes.Count > 0)
				{
					return this.DraggedNodes[0];
				}
				return null;
			}
		}

		// Token: 0x170052D0 RID: 21200
		// (get) Token: 0x06010F7F RID: 69503 RVA: 0x003C1431 File Offset: 0x003BF631
		public RadTreeNode DestDragNode
		{
			get
			{
				return this._destinationNode;
			}
		}

		// Token: 0x170052D1 RID: 21201
		// (get) Token: 0x06010F80 RID: 69504 RVA: 0x003C1439 File Offset: 0x003BF639
		public IList<RadTreeNode> DraggedNodes
		{
			get
			{
				return this._sourceNodes;
			}
		}

		// Token: 0x170052D2 RID: 21202
		// (get) Token: 0x06010F81 RID: 69505 RVA: 0x003C1441 File Offset: 0x003BF641
		// (set) Token: 0x06010F82 RID: 69506 RVA: 0x003C1449 File Offset: 0x003BF649
		public RadTreeViewDropPosition DropPosition
		{
			get
			{
				return this._dropPosition;
			}
			set
			{
				this._dropPosition = value;
			}
		}

		// Token: 0x170052D3 RID: 21203
		// (get) Token: 0x06010F83 RID: 69507 RVA: 0x003C1452 File Offset: 0x003BF652
		// (set) Token: 0x06010F84 RID: 69508 RVA: 0x003C145A File Offset: 0x003BF65A
		public string HtmlElementID
		{
			get
			{
				return this._htmlElementID;
			}
			set
			{
				this._htmlElementID = value;
			}
		}

		// Token: 0x04004BF6 RID: 19446
		private readonly RadTreeNode _destinationNode;

		// Token: 0x04004BF7 RID: 19447
		private readonly IList<RadTreeNode> _sourceNodes;

		// Token: 0x04004BF8 RID: 19448
		private RadTreeViewDropPosition _dropPosition;

		// Token: 0x04004BF9 RID: 19449
		private string _htmlElementID;
	}
}
