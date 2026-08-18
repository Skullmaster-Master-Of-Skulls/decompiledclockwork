using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000390 RID: 912
	internal class GridMobileEditView : GridMobileView
	{
		// Token: 0x06001F6F RID: 8047 RVA: 0x00063672 File Offset: 0x00061872
		public GridMobileEditView(GridTableView tableView, GridEditFormItem editItem) : base(tableView)
		{
			this.editItem = editItem;
			this.CssClass = "rgMobileEditForm";
			base.OverrideClientID = false;
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06001F70 RID: 8048 RVA: 0x00063694 File Offset: 0x00061894
		public override GridMobileViewType Type
		{
			get
			{
				return GridMobileViewType.Edit;
			}
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x00063698 File Offset: 0x00061898
		protected override void CreateContent(HtmlGenericControl container)
		{
			if ((this.NamingContainer as GridEditFormItem).ParentItem == null)
			{
				base.Title = base.Localization.MobileInsertViewTitle;
			}
			else
			{
				base.Title = base.Localization.MobileEditViewTitle;
			}
			int num = 0;
			int num2 = 0;
			foreach (GridColumn gridColumn in base.TableView.RenderColumns)
			{
				GridTemplateColumn gridTemplateColumn = gridColumn as GridTemplateColumn;
				if ((!(gridColumn is GridEditableColumn) && !gridColumn.IsEditable) || (gridTemplateColumn != null && gridTemplateColumn.InsertVisiblityMode == GridColumnVisibilityMode.Inherited && !gridTemplateColumn.IsEditable) || (gridTemplateColumn != null && gridTemplateColumn.InsertVisiblityMode != GridColumnVisibilityMode.Inherited && gridTemplateColumn.IsReadOnly(this.editItem)))
				{
					num++;
				}
				else
				{
					DivTableCell divTableCell = new DivTableCell();
					divTableCell.ID = gridColumn.UniqueName;
					divTableCell.CssClass = "rgValueDiv";
					container.Controls.Add(divTableCell);
					gridColumn.InitializeCell(divTableCell, num, this.editItem);
					num++;
					num2++;
					divTableCell.Visible = gridColumn.IsEditable;
					GridEditableColumn gridEditableColumn = gridColumn as GridEditableColumn;
					if (gridEditableColumn != null)
					{
						divTableCell.Visible = !gridEditableColumn.IsReadOnly(this.editItem);
						if (divTableCell.Controls.Count > 0 && gridEditableColumn.UseNativeEditorsInMobileMode && (gridEditableColumn is GridBoundColumn || gridEditableColumn is GridCheckBoxColumn || gridEditableColumn is GridDateTimeColumn || gridEditableColumn is GridDropDownColumn || gridEditableColumn is GridNumericColumn))
						{
							WebControl webControl = divTableCell.Controls[0] as WebControl;
							if (webControl != null)
							{
								webControl.CssClass = "rgValue";
							}
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
					if (divTableCell.Visible)
					{
						string title = string.Format(gridColumn.EditFormHeaderTextFormat, gridColumn.HeaderText);
						container.Controls.AddAt(container.Controls.Count - 1, base.CreateLabel(title, ""));
					}
				}
			}
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x000638DC File Offset: 0x00061ADC
		protected override void DescribeProperties(ScriptControlDescriptor descriptor)
		{
			base.DescribeProperties(descriptor);
			descriptor.AddProperty("_ownerId", base.TableView.ClientID);
			GridDataItem parentItem = (this.NamingContainer as GridEditFormItem).ParentItem;
			if (parentItem != null)
			{
				descriptor.AddProperty("_editIndexHierarhical", parentItem.ItemIndexHierarchical);
			}
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x0006392C File Offset: 0x00061B2C
		private bool ShouldLabelControl(Control ctrl)
		{
			if (ctrl is TextBox || ctrl is HtmlTextArea || ctrl is RadInputControl || ctrl is RadDatePicker || ctrl is RadComboBox || ctrl is DropDownList)
			{
				return !string.IsNullOrEmpty(ctrl.ClientID);
			}
			return ctrl is CheckBox || ctrl is RadioButton || ctrl is RadioButtonList;
		}

		// Token: 0x04000808 RID: 2056
		private GridEditFormItem editItem;
	}
}
