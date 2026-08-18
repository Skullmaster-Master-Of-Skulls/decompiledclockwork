using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200096B RID: 2411
	internal class TreeListMobileEditView : TreeListMobileView
	{
		// Token: 0x06005BC5 RID: 23493 RVA: 0x00117B7B File Offset: 0x00115D7B
		public TreeListMobileEditView(RadTreeList treelist, TreeListEditFormItem editItem) : base(treelist)
		{
			this.editItem = editItem;
			this.CssClass = "rtlMobileEditForm";
			base.OverrideClientID = false;
		}

		// Token: 0x17001E3F RID: 7743
		// (get) Token: 0x06005BC6 RID: 23494 RVA: 0x00117B9D File Offset: 0x00115D9D
		public override TreeListMobileViewType Type
		{
			get
			{
				return TreeListMobileViewType.Edit;
			}
		}

		// Token: 0x06005BC7 RID: 23495 RVA: 0x00117BA0 File Offset: 0x00115DA0
		protected override void CreateContent(HtmlGenericControl container)
		{
			if ((this.NamingContainer as TreeListEditFormItem).ParentItem == null)
			{
				base.Title = base.Localization.MobileInsertViewTitle;
			}
			else
			{
				base.Title = base.Localization.MobileEditViewTitle;
			}
			int num = 0;
			foreach (TreeListColumn treeListColumn in base.TreeList.RenderColumns)
			{
				TreeListTemplateColumn treeListTemplateColumn = treeListColumn as TreeListTemplateColumn;
				if (!(treeListColumn is TreeListEditableColumn) || (treeListTemplateColumn != null && !treeListTemplateColumn.IsEditable) || (treeListTemplateColumn != null && treeListTemplateColumn.ReadOnly))
				{
					num++;
				}
				else
				{
					DivTableCell divTableCell = new DivTableCell();
					divTableCell.ID = treeListColumn.UniqueName;
					divTableCell.CssClass = "rtlValueDiv";
					container.Controls.Add(divTableCell);
					this.InitializeColumnEditor(divTableCell, num, treeListColumn as TreeListEditableColumn);
					num++;
					TreeListEditableColumn treeListEditableColumn = treeListColumn as TreeListEditableColumn;
					divTableCell.Visible = treeListEditableColumn.IsEditable;
					if (treeListEditableColumn != null)
					{
						divTableCell.Visible = !treeListEditableColumn.ReadOnly;
						if (divTableCell.Controls.Count > 0 && treeListEditableColumn.UseNativeEditorsInMobileMode && (treeListEditableColumn is TreeListBoundColumn || treeListEditableColumn is TreeListDateTimeColumn || treeListEditableColumn is TreeListNumericColumn))
						{
							WebControl webControl = divTableCell.Controls[0] as WebControl;
							if (webControl != null)
							{
								webControl.CssClass = "rtlValue";
							}
						}
						foreach (object obj in divTableCell.Controls)
						{
							Control control = (Control)obj;
							if (this.ShouldLabelControl(control) && control.Visible)
							{
								break;
							}
						}
						string title = string.Format(treeListEditableColumn.EditFormHeaderTextFormat, treeListEditableColumn.HeaderText);
						container.Controls.AddAt(container.Controls.Count - 1, base.CreateLabel(title, ""));
					}
				}
			}
		}

		// Token: 0x06005BC8 RID: 23496 RVA: 0x00117D98 File Offset: 0x00115F98
		private void InitializeColumnEditor(TableCell cell, int columnIndex, TreeListEditableColumn column)
		{
			TreeListEditFormItem treeListEditFormItem = this.NamingContainer as TreeListEditFormItem;
			ITreeListColumnEditor columnEditor = treeListEditFormItem.GetColumnEditor(column);
			if (columnEditor != null)
			{
				columnEditor.Initialize(treeListEditFormItem, cell);
			}
		}

		// Token: 0x06005BC9 RID: 23497 RVA: 0x00117DC4 File Offset: 0x00115FC4
		private bool ShouldLabelControl(Control ctrl)
		{
			if (ctrl is TextBox || ctrl is HtmlTextArea || ctrl is RadInputControl || ctrl is RadDatePicker || ctrl is RadComboBox || ctrl is DropDownList)
			{
				return !string.IsNullOrEmpty(ctrl.ClientID);
			}
			return ctrl is CheckBox || ctrl is RadioButton || ctrl is RadioButtonList;
		}

		// Token: 0x06005BCA RID: 23498 RVA: 0x00117E2C File Offset: 0x0011602C
		protected override void DescribeProperties(ScriptControlDescriptor descriptor)
		{
			base.DescribeProperties(descriptor);
			descriptor.AddProperty("_ownerId", base.TreeList.ClientID);
			TreeListDataItem parentItem = (this.NamingContainer as TreeListEditFormItem).ParentItem;
			if (parentItem != null)
			{
				if (parentItem.InsertItem == null)
				{
					descriptor.AddProperty("_editIndex", parentItem.DisplayIndex.ToString());
					return;
				}
				descriptor.AddProperty("_insertIndex", parentItem.DisplayIndex.ToString());
			}
		}

		// Token: 0x0400160A RID: 5642
		private TreeListEditFormItem editItem;
	}
}
