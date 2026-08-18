using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001209 RID: 4617
	public class TreeListSelectColumn : TreeListColumn
	{
		// Token: 0x17003DA3 RID: 15779
		// (get) Token: 0x0600BEFB RID: 48891 RVA: 0x002A4D7C File Offset: 0x002A2F7C
		// (set) Token: 0x0600BEFC RID: 48892 RVA: 0x002A4D9C File Offset: 0x002A2F9C
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Localizable(true)]
		[Description("Gets or sets the tooltip of each select checkbox.")]
		public virtual string ToolTip
		{
			get
			{
				return (base.ViewState["ToolTip"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["ToolTip"] = value;
			}
		}

		// Token: 0x0600BEFD RID: 48893 RVA: 0x002A4DB0 File Offset: 0x002A2FB0
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
		protected override void InitializeHeaderCells(TableCell cell, int columnIndex, TreeListHeaderItem inItem)
		{
			base.InitializeHeaderCells(cell, columnIndex, inItem);
			if (inItem == null || !base.Owner.AllowMultiItemSelection)
			{
				return;
			}
			CheckBox checkBox = new CheckBox();
			checkBox.ID = string.Format("{0}SelectCheckBox", this.UniqueName);
			AccessibilityHelper.AddToolTip(checkBox, this.HeaderTooltip);
			checkBox.PreRender += this.OnHeaderItemCheckBoxPreRender;
			if (this.IsClientSelection())
			{
				string value = string.Format("this.checked ? $find('{0}').selectAllItems() : $find('{0}').deselectAllItems()", base.Owner.ClientID);
				checkBox.Attributes["onclick"] = value;
			}
			else
			{
				checkBox.AutoPostBack = true;
				checkBox.CheckedChanged += this.OnHeaderItemCheckBoxCheckedChanged;
			}
			cell.Controls.Clear();
			cell.Controls.Add(checkBox);
		}

		// Token: 0x0600BEFE RID: 48894 RVA: 0x002A4E74 File Offset: 0x002A3074
		private void OnHeaderItemCheckBoxCheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			TreeListHeaderItem treeListHeaderItem = checkBox.NamingContainer as TreeListHeaderItem;
			string commandName = checkBox.Checked ? "SelectAll" : "DeselectAll";
			treeListHeaderItem.FireCommandEvent(commandName, string.Empty);
		}

		// Token: 0x0600BEFF RID: 48895 RVA: 0x002A4EB8 File Offset: 0x002A30B8
		private void OnHeaderItemCheckBoxPreRender(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			TreeListHeaderItem treeListHeaderItem = checkBox.NamingContainer as TreeListHeaderItem;
			checkBox.Checked = treeListHeaderItem.OwnerTreeList.GetAllItemsSelected();
		}

		// Token: 0x0600BF00 RID: 48896 RVA: 0x002A4EEC File Offset: 0x002A30EC
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
		protected override void InitializeDataCells(TableCell cell, int columnIndex, TreeListDataItem inItem)
		{
			if (inItem == null)
			{
				return;
			}
			CheckBox checkBox = new CheckBox();
			checkBox.ID = string.Format("{0}SelectCheckBox", this.UniqueName);
			AccessibilityHelper.AddToolTip(checkBox, this.ToolTip);
			checkBox.PreRender += this.OnDataItemCheckBoxPreRender;
			if (!this.IsClientSelection())
			{
				checkBox.AutoPostBack = true;
				checkBox.CheckedChanged += this.OnDataItemCheckBoxCheckedChanged;
			}
			cell.Controls.Clear();
			cell.Controls.Add(checkBox);
		}

		// Token: 0x0600BF01 RID: 48897 RVA: 0x002A4F74 File Offset: 0x002A3174
		private void OnDataItemCheckBoxCheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			TreeListDataItem treeListDataItem = checkBox.NamingContainer as TreeListDataItem;
			string commandName = checkBox.Checked ? "Select" : "Deselect";
			treeListDataItem.FireCommandEvent(commandName, string.Empty);
		}

		// Token: 0x0600BF02 RID: 48898 RVA: 0x002A4FB8 File Offset: 0x002A31B8
		private void OnDataItemCheckBoxPreRender(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			TreeListDataItem treeListDataItem = checkBox.NamingContainer as TreeListDataItem;
			checkBox.Checked = treeListDataItem.Selected;
		}

		// Token: 0x0600BF03 RID: 48899 RVA: 0x002A4FE4 File Offset: 0x002A31E4
		private bool IsClientSelection()
		{
			return base.Owner.ClientSettings.Selecting.AllowItemSelection && !base.Owner.AllowRecursiveSelection;
		}
	}
}
