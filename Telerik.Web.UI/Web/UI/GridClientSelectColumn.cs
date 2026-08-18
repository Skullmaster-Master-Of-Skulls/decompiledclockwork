using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020010AB RID: 4267
	public class GridClientSelectColumn : GridButtonColumn
	{
		// Token: 0x17003829 RID: 14377
		// (get) Token: 0x0600ADC7 RID: 44487 RVA: 0x0025792F File Offset: 0x00255B2F
		public override bool Selectable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600ADC8 RID: 44488 RVA: 0x00257934 File Offset: 0x00255B34
		public override void InitializeCell(TableCell cell, int columnIndex, GridItem inItem)
		{
			base.InitializeCell(cell, columnIndex, inItem);
			GridDataItem gridDataItem = inItem as GridDataItem;
			GridHeaderItem gridHeaderItem = inItem as GridHeaderItem;
			if (gridHeaderItem != null && !base.Owner.OwnerGrid.AllowMultiRowSelection)
			{
				return;
			}
			if (gridDataItem != null && !base.Owner.OwnerGrid.ClientSettings.Selecting.AllowRowSelect)
			{
				throw new GridException("Please set ClientSettings.Selecting.AllowRowSelect to \"True\" to start using GridClientSelectColumn.");
			}
			if (gridHeaderItem != null || gridDataItem != null)
			{
				CheckBox checkBox = new CheckBox();
				checkBox.ID = string.Format("{0}SelectCheckBox", this.UniqueName);
				AccessibilityHelper.AddToolTip(checkBox, this.HeaderTooltip);
				if (gridHeaderItem != null && base.Owner.OwnerGrid.AllowMultiRowSelection)
				{
					checkBox.Attributes["onclick"] = string.Format("$find(\"{0}\")._selectAllRows(\"{1}\", \"{2}\", event)", base.Owner.OwnerGrid.ClientID, base.Owner.ClientID, inItem.ItemIndexHierarchical);
				}
				if (gridDataItem != null)
				{
					checkBox.Checked = inItem.Selected;
				}
				cell.Controls.Clear();
				cell.Controls.Add(checkBox);
				inItem.PreRender += this.inItem_PreRender;
			}
		}

		// Token: 0x0600ADC9 RID: 44489 RVA: 0x00257A54 File Offset: 0x00255C54
		public override void PrepareCell(TableCell cell, GridItem item)
		{
			base.PrepareCell(cell, item);
			CheckBox checkBox = (CheckBox)item.FindControl(string.Format("{0}SelectCheckBox", this.UniqueName));
			if (checkBox != null)
			{
				GridDataItem gridDataItem = item as GridDataItem;
				if (gridDataItem != null)
				{
					checkBox.Checked = item.Selected;
					if (gridDataItem.SelectableMode != GridItemSelectableMode.ServerAndClientSide)
					{
						checkBox.Enabled = false;
					}
				}
				if (item is GridHeaderItem)
				{
					int num = 0;
					foreach (object obj in item.OwnerTableView.Items)
					{
						GridDataItem gridDataItem2 = (GridDataItem)obj;
						if (gridDataItem2.SelectableMode != GridItemSelectableMode.ServerAndClientSide && !gridDataItem2.Selected)
						{
							num++;
						}
					}
					int num2 = item.OwnerTableView.GetSelectedItems().Length;
					int num3 = item.OwnerTableView.Items.Count - num;
					checkBox.Checked = (num2 == num3 && num3 > 0);
					if (!string.IsNullOrEmpty(this.HeaderStyle.CssClass))
					{
						cell.CssClass = this.HeaderStyle.CssClass + " rgCheck";
					}
					else
					{
						cell.CssClass = item.OwnerTableView.RenderHeaderStyle.CssClass + " rgCheck";
					}
				}
				if (base.Owner.OwnerGrid.EnableAriaSupport)
				{
					checkBox.InputAttributes.Add("title", "Select Row");
				}
			}
		}

		// Token: 0x0600ADCA RID: 44490 RVA: 0x00257BD0 File Offset: 0x00255DD0
		private void inItem_PreRender(object sender, EventArgs e)
		{
		}

		// Token: 0x1700382A RID: 14378
		// (get) Token: 0x0600ADCB RID: 44491 RVA: 0x00257BD2 File Offset: 0x00255DD2
		// (set) Token: 0x0600ADCC RID: 44492 RVA: 0x00257BDA File Offset: 0x00255DDA
		[Browsable(false)]
		public override string ConfirmTitle
		{
			get
			{
				return base.ConfirmTitle;
			}
			set
			{
				base.ConfirmTitle = value;
			}
		}

		// Token: 0x1700382B RID: 14379
		// (get) Token: 0x0600ADCD RID: 44493 RVA: 0x00257BE3 File Offset: 0x00255DE3
		// (set) Token: 0x0600ADCE RID: 44494 RVA: 0x00257BEB File Offset: 0x00255DEB
		[Browsable(false)]
		public override GridButtonColumnType ButtonType
		{
			get
			{
				return base.ButtonType;
			}
			set
			{
				base.ButtonType = value;
			}
		}

		// Token: 0x1700382C RID: 14380
		// (get) Token: 0x0600ADCF RID: 44495 RVA: 0x00257BF4 File Offset: 0x00255DF4
		// (set) Token: 0x0600ADD0 RID: 44496 RVA: 0x00257BFC File Offset: 0x00255DFC
		[Browsable(false)]
		public override string ButtonCssClass
		{
			get
			{
				return base.ButtonCssClass;
			}
			set
			{
				base.ButtonCssClass = value;
			}
		}

		// Token: 0x1700382D RID: 14381
		// (get) Token: 0x0600ADD1 RID: 44497 RVA: 0x00257C05 File Offset: 0x00255E05
		// (set) Token: 0x0600ADD2 RID: 44498 RVA: 0x00257C0D File Offset: 0x00255E0D
		[Browsable(false)]
		public override GridConfirmDialogType ConfirmDialogType
		{
			get
			{
				return base.ConfirmDialogType;
			}
			set
			{
				base.ConfirmDialogType = value;
			}
		}

		// Token: 0x1700382E RID: 14382
		// (get) Token: 0x0600ADD3 RID: 44499 RVA: 0x00257C16 File Offset: 0x00255E16
		// (set) Token: 0x0600ADD4 RID: 44500 RVA: 0x00257C1E File Offset: 0x00255E1E
		[Browsable(false)]
		public override Unit ConfirmDialogWidth
		{
			get
			{
				return base.ConfirmDialogWidth;
			}
			set
			{
				base.ConfirmDialogWidth = value;
			}
		}

		// Token: 0x1700382F RID: 14383
		// (get) Token: 0x0600ADD5 RID: 44501 RVA: 0x00257C27 File Offset: 0x00255E27
		// (set) Token: 0x0600ADD6 RID: 44502 RVA: 0x00257C2F File Offset: 0x00255E2F
		[Browsable(false)]
		public override Unit ConfirmDialogHeight
		{
			get
			{
				return base.ConfirmDialogHeight;
			}
			set
			{
				base.ConfirmDialogHeight = value;
			}
		}

		// Token: 0x17003830 RID: 14384
		// (get) Token: 0x0600ADD7 RID: 44503 RVA: 0x00257C38 File Offset: 0x00255E38
		// (set) Token: 0x0600ADD8 RID: 44504 RVA: 0x00257C40 File Offset: 0x00255E40
		[Browsable(false)]
		public override string CommandName
		{
			get
			{
				return base.CommandName;
			}
			set
			{
				base.CommandName = value;
			}
		}

		// Token: 0x17003831 RID: 14385
		// (get) Token: 0x0600ADD9 RID: 44505 RVA: 0x00257C49 File Offset: 0x00255E49
		// (set) Token: 0x0600ADDA RID: 44506 RVA: 0x00257C51 File Offset: 0x00255E51
		[Browsable(false)]
		public override string CommandArgument
		{
			get
			{
				return base.CommandArgument;
			}
			set
			{
				base.CommandArgument = value;
			}
		}

		// Token: 0x17003832 RID: 14386
		// (get) Token: 0x0600ADDB RID: 44507 RVA: 0x00257C5A File Offset: 0x00255E5A
		// (set) Token: 0x0600ADDC RID: 44508 RVA: 0x00257C62 File Offset: 0x00255E62
		[Browsable(false)]
		public override string DataTextField
		{
			get
			{
				return base.DataTextField;
			}
			set
			{
				base.DataTextField = value;
			}
		}

		// Token: 0x17003833 RID: 14387
		// (get) Token: 0x0600ADDD RID: 44509 RVA: 0x00257C6B File Offset: 0x00255E6B
		// (set) Token: 0x0600ADDE RID: 44510 RVA: 0x00257C73 File Offset: 0x00255E73
		[Browsable(false)]
		public override string DataTextFormatString
		{
			get
			{
				return base.DataTextFormatString;
			}
			set
			{
				base.DataTextFormatString = value;
			}
		}

		// Token: 0x17003834 RID: 14388
		// (get) Token: 0x0600ADDF RID: 44511 RVA: 0x00257C7C File Offset: 0x00255E7C
		// (set) Token: 0x0600ADE0 RID: 44512 RVA: 0x00257C84 File Offset: 0x00255E84
		[Browsable(false)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		// Token: 0x17003835 RID: 14389
		// (get) Token: 0x0600ADE1 RID: 44513 RVA: 0x00257C8D File Offset: 0x00255E8D
		// (set) Token: 0x0600ADE2 RID: 44514 RVA: 0x00257C95 File Offset: 0x00255E95
		[Browsable(false)]
		public override string ImageUrl
		{
			get
			{
				return base.ImageUrl;
			}
			set
			{
				base.ImageUrl = value;
			}
		}

		// Token: 0x17003836 RID: 14390
		// (get) Token: 0x0600ADE3 RID: 44515 RVA: 0x00257C9E File Offset: 0x00255E9E
		// (set) Token: 0x0600ADE4 RID: 44516 RVA: 0x00257CA6 File Offset: 0x00255EA6
		[Browsable(false)]
		public override string ConfirmText
		{
			get
			{
				return base.ConfirmText;
			}
			set
			{
				base.ConfirmText = value;
			}
		}

		// Token: 0x17003837 RID: 14391
		// (get) Token: 0x0600ADE5 RID: 44517 RVA: 0x00257CAF File Offset: 0x00255EAF
		// (set) Token: 0x0600ADE6 RID: 44518 RVA: 0x00257CB7 File Offset: 0x00255EB7
		[Browsable(false)]
		public override string ConfirmTextFormatString
		{
			get
			{
				return base.ConfirmTextFormatString;
			}
			set
			{
				base.ConfirmTextFormatString = value;
			}
		}

		// Token: 0x17003838 RID: 14392
		// (get) Token: 0x0600ADE7 RID: 44519 RVA: 0x00257CC0 File Offset: 0x00255EC0
		// (set) Token: 0x0600ADE8 RID: 44520 RVA: 0x00257CC8 File Offset: 0x00255EC8
		[Browsable(false)]
		public override string[] ConfirmTextFields
		{
			get
			{
				return base.ConfirmTextFields;
			}
			set
			{
				base.ConfirmTextFields = value;
			}
		}

		// Token: 0x17003839 RID: 14393
		// (get) Token: 0x0600ADE9 RID: 44521 RVA: 0x00257CD1 File Offset: 0x00255ED1
		// (set) Token: 0x0600ADEA RID: 44522 RVA: 0x00257CD9 File Offset: 0x00255ED9
		[Browsable(false)]
		public override bool ShowInEditForm
		{
			get
			{
				return base.ShowInEditForm;
			}
			set
			{
				base.ShowInEditForm = value;
			}
		}

		// Token: 0x0600ADEB RID: 44523 RVA: 0x00257CE4 File Offset: 0x00255EE4
		public override GridColumn Clone()
		{
			GridClientSelectColumn gridClientSelectColumn = new GridClientSelectColumn();
			gridClientSelectColumn.CopyBaseProperties(this);
			return gridClientSelectColumn;
		}

		// Token: 0x0600ADEC RID: 44524 RVA: 0x00257CFF File Offset: 0x00255EFF
		protected override void CopyBaseProperties(GridColumn FromColumn)
		{
			base.CopyBaseProperties(FromColumn);
		}
	}
}
