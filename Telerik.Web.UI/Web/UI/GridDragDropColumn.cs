using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000F77 RID: 3959
	public class GridDragDropColumn : GridColumn
	{
		// Token: 0x17002FE4 RID: 12260
		// (get) Token: 0x060097A7 RID: 38823 RVA: 0x0021FED8 File Offset: 0x0021E0D8
		public override bool Selectable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060097A8 RID: 38824 RVA: 0x0021FEDC File Offset: 0x0021E0DC
		public override void InitializeCell(TableCell cell, int columnIndex, GridItem inItem)
		{
			base.InitializeCell(cell, columnIndex, inItem);
			cell.Controls.Clear();
			if (inItem.IsDataBound)
			{
				WebControl webControl;
				if (string.IsNullOrEmpty(this.DragImageUrl))
				{
					if (base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
					{
						webControl = new WebControl(HtmlTextWriterTag.A);
					}
					else
					{
						webControl = new WebControl(HtmlTextWriterTag.Input);
						webControl.Attributes["type"] = "button";
					}
				}
				else
				{
					webControl = new Image();
				}
				webControl.ID = this.UniqueName + "RowDragHandle";
				cell.Controls.Add(webControl);
			}
		}

		// Token: 0x060097A9 RID: 38825 RVA: 0x0021FF78 File Offset: 0x0021E178
		public override void PrepareCell(TableCell cell, GridItem item)
		{
			base.PrepareCell(cell, item);
			if (!this.Visible || !cell.Visible)
			{
				return;
			}
			GridHeaderItem gridHeaderItem = item as GridHeaderItem;
			if (gridHeaderItem == null && !(item is GridFilteringItem))
			{
				cell.CssClass = string.Format("rgDragCol{0}{1}", string.IsNullOrEmpty(cell.CssClass) ? "" : " ", cell.CssClass);
			}
			if (gridHeaderItem != null)
			{
				cell.CssClass = string.Format("{0} rgDragCol{1}{2}", base.Owner.RenderHeaderStyle.CssClass, string.IsNullOrEmpty(cell.CssClass) ? "" : " ", cell.CssClass);
			}
			if (cell.Controls.Count != 0 && cell.Controls[0] is WebControl)
			{
				WebControl webControl = (WebControl)cell.Controls[0];
				Image image = webControl as Image;
				if (image != null)
				{
					image.ImageUrl = this.DragImageUrl;
					image.ToolTip = this.DragImageToolTip;
					return;
				}
				string text = (base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight) ? "rgDragIcon t-font-icon rgIcon" : "rgDrag";
				webControl.CssClass = (string.IsNullOrEmpty(webControl.CssClass) ? text : (webControl.CssClass + " " + text));
				webControl.ToolTip = this.DragImageToolTip;
			}
		}

		// Token: 0x17002FE5 RID: 12261
		// (get) Token: 0x060097AA RID: 38826 RVA: 0x002200CF File Offset: 0x0021E2CF
		// (set) Token: 0x060097AB RID: 38827 RVA: 0x002200EF File Offset: 0x0021E2EF
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the ToolTip of the Drag image for the GridDragDropColumn")]
		[Localizable(true)]
		public virtual string DragImageToolTip
		{
			get
			{
				return (string)(base.ViewState["DragImageToolTip"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DragImageToolTip"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17002FE6 RID: 12262
		// (get) Token: 0x060097AC RID: 38828 RVA: 0x00220108 File Offset: 0x0021E308
		// (set) Token: 0x060097AD RID: 38829 RVA: 0x00220128 File Offset: 0x0021E328
		[Localizable(true)]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the URL of the drag image that will be displayed instead of the default Drag image for the GridDragDropColumn")]
		public virtual string DragImageUrl
		{
			get
			{
				return (string)(base.ViewState["DragImageUrl"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DragImageUrl"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17002FE7 RID: 12263
		// (get) Token: 0x060097AE RID: 38830 RVA: 0x00220141 File Offset: 0x0021E341
		// (set) Token: 0x060097AF RID: 38831 RVA: 0x00220149 File Offset: 0x0021E349
		[NotifyParentProperty(true)]
		[Browsable(true)]
		[Description("Gets or sets the unique name for this column")]
		[DefaultValue("DragDropColumn")]
		public override string UniqueName
		{
			get
			{
				return base.UniqueName;
			}
			set
			{
				base.UniqueName = value;
			}
		}

		// Token: 0x17002FE8 RID: 12264
		// (get) Token: 0x060097B0 RID: 38832 RVA: 0x00220152 File Offset: 0x0021E352
		// (set) Token: 0x060097B1 RID: 38833 RVA: 0x00220155 File Offset: 0x0021E355
		[DefaultValue(false)]
		public override bool Groupable
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17002FE9 RID: 12265
		// (get) Token: 0x060097B2 RID: 38834 RVA: 0x00220157 File Offset: 0x0021E357
		// (set) Token: 0x060097B3 RID: 38835 RVA: 0x0022015A File Offset: 0x0021E35A
		[DefaultValue(false)]
		public override bool Resizable
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x060097B4 RID: 38836 RVA: 0x0022015C File Offset: 0x0021E35C
		protected override string GenerateUniqueName()
		{
			return base.GenerateUniqueNameBase("DragDropColumn");
		}

		// Token: 0x060097B5 RID: 38837 RVA: 0x0022016C File Offset: 0x0021E36C
		public override GridColumn Clone()
		{
			GridDragDropColumn gridDragDropColumn = new GridDragDropColumn();
			gridDragDropColumn.CopyBaseProperties(this);
			return gridDragDropColumn;
		}

		// Token: 0x060097B6 RID: 38838 RVA: 0x00220188 File Offset: 0x0021E388
		protected override void CopyBaseProperties(GridColumn FromColumn)
		{
			base.CopyBaseProperties(FromColumn);
			GridDragDropColumn gridDragDropColumn = (GridDragDropColumn)FromColumn;
			this.DragImageUrl = gridDragDropColumn.DragImageUrl;
			this.DragImageToolTip = gridDragDropColumn.DragImageToolTip;
		}
	}
}
