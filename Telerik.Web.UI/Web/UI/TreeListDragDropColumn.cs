using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000F19 RID: 3865
	public class TreeListDragDropColumn : TreeListColumn
	{
		// Token: 0x0600939E RID: 37790 RVA: 0x002126E4 File Offset: 0x002108E4
		protected override void InitializeDataCells(TableCell cell, int columnIndex, TreeListDataItem inItem)
		{
			cell.Controls.Clear();
			WebControl webControl;
			if (string.IsNullOrEmpty(this.DragImageUrl))
			{
				if (base.Owner.ResolvedRenderMode == RenderMode.Lightweight || base.Owner.ResolvedRenderMode == RenderMode.Mobile)
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
				(webControl as Image).ImageUrl = this.DragImageUrl;
			}
			webControl.ID = this.UniqueName + "RowDragHandle";
			cell.Controls.Add(webControl);
		}

		// Token: 0x0600939F RID: 37791 RVA: 0x00212788 File Offset: 0x00210988
		internal override void PrepareCell(TableCell cell, TreeListItem item)
		{
			base.PrepareCell(cell, item);
			cell.CssClass = string.Format("rtlDragCol{0}{1}", string.IsNullOrEmpty(cell.CssClass) ? "" : " ", cell.CssClass);
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
				string text = (base.Owner.ResolvedRenderMode == RenderMode.Lightweight || base.Owner.ResolvedRenderMode == RenderMode.Mobile) ? "rtlDragIcon t-font-icon rtlIcon" : "rtlDragHandle";
				webControl.CssClass = (string.IsNullOrEmpty(webControl.CssClass) ? text : (webControl.CssClass + " " + text));
				webControl.ToolTip = this.DragImageToolTip;
			}
		}

		// Token: 0x17002EAE RID: 11950
		// (get) Token: 0x060093A0 RID: 37792 RVA: 0x00212883 File Offset: 0x00210A83
		// (set) Token: 0x060093A1 RID: 37793 RVA: 0x002128A3 File Offset: 0x00210AA3
		[NotifyParentProperty(true)]
		[Description("Gets or sets the ToolTip of the Drag image for the TreeListDragDropColumn")]
		[DefaultValue("")]
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
			}
		}

		// Token: 0x17002EAF RID: 11951
		// (get) Token: 0x060093A2 RID: 37794 RVA: 0x002128B6 File Offset: 0x00210AB6
		// (set) Token: 0x060093A3 RID: 37795 RVA: 0x002128D6 File Offset: 0x00210AD6
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the URL of the drag image that will be displayed instead of the default Drag image for the TreeListDragDropColumn")]
		[Localizable(true)]
		public virtual string DragImageUrl
		{
			get
			{
				return (string)(base.ViewState["DragImageUrl"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DragImageUrl"] = value;
			}
		}

		// Token: 0x17002EB0 RID: 11952
		// (get) Token: 0x060093A4 RID: 37796 RVA: 0x002128E9 File Offset: 0x00210AE9
		// (set) Token: 0x060093A5 RID: 37797 RVA: 0x002128F1 File Offset: 0x00210AF1
		[Browsable(true)]
		[DefaultValue("DragDropColumn")]
		[Description("Gets or sets the unique name for this column")]
		[NotifyParentProperty(true)]
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

		// Token: 0x060093A6 RID: 37798 RVA: 0x002128FA File Offset: 0x00210AFA
		protected override string GenerateUniqueName()
		{
			return base.GenerateUniqueNameBase("DragDropColumn");
		}

		// Token: 0x17002EB1 RID: 11953
		// (get) Token: 0x060093A7 RID: 37799 RVA: 0x00212907 File Offset: 0x00210B07
		// (set) Token: 0x060093A8 RID: 37800 RVA: 0x0021290A File Offset: 0x00210B0A
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[Description("Gets or sets a value indicating whether the column can be resized client-side")]
		[Category("Behavior")]
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
	}
}
