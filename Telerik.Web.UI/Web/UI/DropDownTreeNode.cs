using System;
using System.Web.UI;
using Telerik.Web.UI.DropDownTree;

namespace Telerik.Web.UI
{
	// Token: 0x02000B30 RID: 2864
	public class DropDownTreeNode
	{
		// Token: 0x17002345 RID: 9029
		// (get) Token: 0x06006BAC RID: 27564 RVA: 0x00191436 File Offset: 0x0018F636
		// (set) Token: 0x06006BAD RID: 27565 RVA: 0x00191443 File Offset: 0x0018F643
		internal bool Selected
		{
			get
			{
				return this._treeNodeAdapter.Selected;
			}
			set
			{
				this._treeNodeAdapter.Selected = value;
			}
		}

		// Token: 0x17002346 RID: 9030
		// (get) Token: 0x06006BAE RID: 27566 RVA: 0x00191451 File Offset: 0x0018F651
		// (set) Token: 0x06006BAF RID: 27567 RVA: 0x0019145E File Offset: 0x0018F65E
		internal bool Checked
		{
			get
			{
				return this._treeNodeAdapter.Checked;
			}
			set
			{
				this._treeNodeAdapter.Checked = value;
			}
		}

		// Token: 0x17002347 RID: 9031
		// (get) Token: 0x06006BB0 RID: 27568 RVA: 0x0019146C File Offset: 0x0018F66C
		// (set) Token: 0x06006BB1 RID: 27569 RVA: 0x00191479 File Offset: 0x0018F679
		public string Text
		{
			get
			{
				return this._treeNodeAdapter.Text;
			}
			set
			{
				this._treeNodeAdapter.Text = value;
			}
		}

		// Token: 0x17002348 RID: 9032
		// (get) Token: 0x06006BB2 RID: 27570 RVA: 0x00191487 File Offset: 0x0018F687
		// (set) Token: 0x06006BB3 RID: 27571 RVA: 0x00191494 File Offset: 0x0018F694
		public string Value
		{
			get
			{
				return this._treeNodeAdapter.Value;
			}
			set
			{
				this._treeNodeAdapter.Value = value;
			}
		}

		// Token: 0x17002349 RID: 9033
		// (get) Token: 0x06006BB4 RID: 27572 RVA: 0x001914A2 File Offset: 0x0018F6A2
		// (set) Token: 0x06006BB5 RID: 27573 RVA: 0x001914AF File Offset: 0x0018F6AF
		public string CssClass
		{
			get
			{
				return this._treeNodeAdapter.CssClass;
			}
			set
			{
				this._treeNodeAdapter.CssClass = value;
			}
		}

		// Token: 0x1700234A RID: 9034
		// (get) Token: 0x06006BB6 RID: 27574 RVA: 0x001914BD File Offset: 0x0018F6BD
		public string FullPath
		{
			get
			{
				return this._treeNodeAdapter.FullPath(this._dropDownTree.FullPathDelimiter);
			}
		}

		// Token: 0x1700234B RID: 9035
		// (get) Token: 0x06006BB7 RID: 27575 RVA: 0x001914D5 File Offset: 0x0018F6D5
		public string ID
		{
			get
			{
				return this._treeNodeAdapter.ID;
			}
		}

		// Token: 0x1700234C RID: 9036
		// (get) Token: 0x06006BB8 RID: 27576 RVA: 0x001914E2 File Offset: 0x0018F6E2
		public object DataItem
		{
			get
			{
				return this._treeNodeAdapter.DataItem;
			}
		}

		// Token: 0x1700234D RID: 9037
		// (get) Token: 0x06006BB9 RID: 27577 RVA: 0x001914EF File Offset: 0x0018F6EF
		public int Level
		{
			get
			{
				return this._treeNodeAdapter.Level;
			}
		}

		// Token: 0x1700234E RID: 9038
		// (get) Token: 0x06006BBA RID: 27578 RVA: 0x001914FC File Offset: 0x0018F6FC
		// (set) Token: 0x06006BBB RID: 27579 RVA: 0x00191509 File Offset: 0x0018F709
		public bool Checkable
		{
			get
			{
				return this._treeNodeAdapter.Checkable;
			}
			set
			{
				this._treeNodeAdapter.Checkable = value;
			}
		}

		// Token: 0x1700234F RID: 9039
		// (get) Token: 0x06006BBC RID: 27580 RVA: 0x00191517 File Offset: 0x0018F717
		// (set) Token: 0x06006BBD RID: 27581 RVA: 0x00191524 File Offset: 0x0018F724
		public bool Expanded
		{
			get
			{
				return this._treeNodeAdapter.Expanded;
			}
			set
			{
				this._treeNodeAdapter.Expanded = value;
			}
		}

		// Token: 0x06006BBE RID: 27582 RVA: 0x00191532 File Offset: 0x0018F732
		public DropDownTreeNode(RadDropDownTree dropDownTree)
		{
			this._dropDownTree = dropDownTree;
		}

		// Token: 0x06006BBF RID: 27583 RVA: 0x00191541 File Offset: 0x0018F741
		public Control FindControl(string controlID)
		{
			return this._treeNodeAdapter.FindControl(controlID);
		}

		// Token: 0x06006BC0 RID: 27584 RVA: 0x0019154F File Offset: 0x0018F74F
		public void CreateEntry()
		{
			if (this._dropDownTree.CheckBoxes == DropDownTreeCheckBoxes.TriState)
			{
				this._dropDownTree.NodesForEntries.Add(this._treeNodeAdapter.GetTreeViewNode());
				return;
			}
			this._dropDownTree.CreateEntryFromDropDownNode(this);
		}

		// Token: 0x04001D06 RID: 7430
		private RadDropDownTree _dropDownTree;

		// Token: 0x04001D07 RID: 7431
		internal TreeNodeAdapter _treeNodeAdapter;
	}
}
