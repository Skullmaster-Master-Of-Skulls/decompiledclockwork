using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001200 RID: 4608
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	public class TreeListCheckBoxColumn : TreeListEditableColumn
	{
		// Token: 0x17003D69 RID: 15721
		// (get) Token: 0x0600BE56 RID: 48726 RVA: 0x002A2E72 File Offset: 0x002A1072
		// (set) Token: 0x0600BE57 RID: 48727 RVA: 0x002A2E92 File Offset: 0x002A1092
		[Description("Gets or sets the tooltip of each select checkbox.")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Localizable(true)]
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

		// Token: 0x0600BE58 RID: 48728 RVA: 0x002A2EA8 File Offset: 0x002A10A8
		protected override void InitializeDataCells(TableCell cell, int columnIndex, TreeListDataItem inItem)
		{
			this._checkBox = new CheckBox();
			this._checkBox.Enabled = false;
			AccessibilityHelper.AddToolTip(this._checkBox, this.ToolTip);
			cell.Controls.Add(this._checkBox);
			base.InitializeDataCells(cell, columnIndex, inItem);
		}

		// Token: 0x0600BE59 RID: 48729 RVA: 0x002A2EF8 File Offset: 0x002A10F8
		protected override void OnColumnDataCellBinding(object sender, EventArgs e)
		{
			TableCell control = (TableCell)sender;
			TreeListDataItem treeListDataItem = (TreeListDataItem)TreeListColumn.GetBindingParentItem(control);
			object dataItem = treeListDataItem.DataItem;
			object obj = null;
			if (!string.IsNullOrEmpty(base.DataField) && base.TryExtractDataValue(dataItem, base.DataField, out obj))
			{
				this._checkBox.Checked = bool.Parse(obj.ToString());
			}
		}

		// Token: 0x0600BE5A RID: 48730 RVA: 0x002A2F55 File Offset: 0x002A1155
		public override ITreeListColumnEditor CreateDefaultColumnEditor()
		{
			return new TreeListCheckBoxColumnEditor(this);
		}

		// Token: 0x0600BE5B RID: 48731 RVA: 0x002A2F60 File Offset: 0x002A1160
		protected override object GetColumnValueFromDataCell(TableCell cell)
		{
			foreach (object obj in cell.Controls)
			{
				Control control = (Control)obj;
				CheckBox checkBox = control as CheckBox;
				if (checkBox != null)
				{
					return checkBox.Checked;
				}
			}
			return false;
		}

		// Token: 0x04003209 RID: 12809
		private CheckBox _checkBox;
	}
}
