using System;
using System.Web.UI;

namespace Telerik.Web.UI.DropDownTree
{
	// Token: 0x02000B2C RID: 2860
	internal class TreeNodeAdapter : ITreeNodeAdapter<RadTreeNode>, ITreeNodeBase
	{
		// Token: 0x17002332 RID: 9010
		// (get) Token: 0x06006B73 RID: 27507 RVA: 0x00190FED File Offset: 0x0018F1ED
		// (set) Token: 0x06006B74 RID: 27508 RVA: 0x00190FFA File Offset: 0x0018F1FA
		public string Text
		{
			get
			{
				return this._treeViewNode.Text;
			}
			set
			{
				this._treeViewNode.Text = value;
			}
		}

		// Token: 0x17002333 RID: 9011
		// (get) Token: 0x06006B75 RID: 27509 RVA: 0x00191008 File Offset: 0x0018F208
		// (set) Token: 0x06006B76 RID: 27510 RVA: 0x00191015 File Offset: 0x0018F215
		public string Value
		{
			get
			{
				return this._treeViewNode.Value;
			}
			set
			{
				this._treeViewNode.Value = value;
			}
		}

		// Token: 0x17002334 RID: 9012
		// (get) Token: 0x06006B77 RID: 27511 RVA: 0x00191023 File Offset: 0x0018F223
		// (set) Token: 0x06006B78 RID: 27512 RVA: 0x00191030 File Offset: 0x0018F230
		public string CssClass
		{
			get
			{
				return this._treeViewNode.CssClass;
			}
			set
			{
				this._treeViewNode.CssClass = value;
			}
		}

		// Token: 0x06006B79 RID: 27513 RVA: 0x0019103E File Offset: 0x0018F23E
		public string FullPath(string delimiter)
		{
			return this._treeViewNode.GetFullPath(delimiter);
		}

		// Token: 0x17002335 RID: 9013
		// (get) Token: 0x06006B7A RID: 27514 RVA: 0x0019104C File Offset: 0x0018F24C
		public string ID
		{
			get
			{
				return this._treeViewNode.ID;
			}
		}

		// Token: 0x17002336 RID: 9014
		// (get) Token: 0x06006B7B RID: 27515 RVA: 0x00191059 File Offset: 0x0018F259
		public object DataItem
		{
			get
			{
				return this._treeViewNode.DataItem;
			}
		}

		// Token: 0x17002337 RID: 9015
		// (get) Token: 0x06006B7C RID: 27516 RVA: 0x00191066 File Offset: 0x0018F266
		public int Level
		{
			get
			{
				return this._treeViewNode.Level;
			}
		}

		// Token: 0x17002338 RID: 9016
		// (get) Token: 0x06006B7D RID: 27517 RVA: 0x00191073 File Offset: 0x0018F273
		// (set) Token: 0x06006B7E RID: 27518 RVA: 0x00191080 File Offset: 0x0018F280
		public bool Checkable
		{
			get
			{
				return this._treeViewNode.Checkable;
			}
			set
			{
				this._treeViewNode.Checkable = value;
			}
		}

		// Token: 0x17002339 RID: 9017
		// (get) Token: 0x06006B7F RID: 27519 RVA: 0x0019108E File Offset: 0x0018F28E
		// (set) Token: 0x06006B80 RID: 27520 RVA: 0x0019109B File Offset: 0x0018F29B
		public bool Expanded
		{
			get
			{
				return this._treeViewNode.Expanded;
			}
			set
			{
				this._treeViewNode.Expanded = value;
			}
		}

		// Token: 0x1700233A RID: 9018
		// (get) Token: 0x06006B81 RID: 27521 RVA: 0x001910A9 File Offset: 0x0018F2A9
		// (set) Token: 0x06006B82 RID: 27522 RVA: 0x001910B6 File Offset: 0x0018F2B6
		public bool Selected
		{
			get
			{
				return this._treeViewNode.Selected;
			}
			set
			{
				this._treeViewNode.Selected = value;
			}
		}

		// Token: 0x1700233B RID: 9019
		// (get) Token: 0x06006B83 RID: 27523 RVA: 0x001910C4 File Offset: 0x0018F2C4
		// (set) Token: 0x06006B84 RID: 27524 RVA: 0x001910D1 File Offset: 0x0018F2D1
		public bool Checked
		{
			get
			{
				return this._treeViewNode.Checked;
			}
			set
			{
				this._treeViewNode.Checked = value;
			}
		}

		// Token: 0x06006B85 RID: 27525 RVA: 0x001910DF File Offset: 0x0018F2DF
		public TreeNodeAdapter(RadTreeNode treeViewNode)
		{
			this._treeViewNode = treeViewNode;
		}

		// Token: 0x06006B86 RID: 27526 RVA: 0x001910EE File Offset: 0x0018F2EE
		public RadTreeNode GetTreeViewNode()
		{
			return this._treeViewNode;
		}

		// Token: 0x06006B87 RID: 27527 RVA: 0x001910F6 File Offset: 0x0018F2F6
		public Control FindControl(string controlID)
		{
			return this._treeViewNode.FindControl(controlID);
		}

		// Token: 0x04001CF8 RID: 7416
		private RadTreeNode _treeViewNode;
	}
}
