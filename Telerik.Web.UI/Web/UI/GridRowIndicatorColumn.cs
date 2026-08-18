using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001168 RID: 4456
	public class GridRowIndicatorColumn : GridColumn
	{
		// Token: 0x17003ABA RID: 15034
		// (get) Token: 0x0600B5A1 RID: 46497 RVA: 0x00280180 File Offset: 0x0027E380
		// (set) Token: 0x0600B5A2 RID: 46498 RVA: 0x00280188 File Offset: 0x0027E388
		[DefaultValue("Filter RowIndicator column")]
		public override string FilterControlAltText
		{
			get
			{
				return base.FilterControlAltText;
			}
			set
			{
				base.FilterControlAltText = value;
			}
		}

		// Token: 0x0600B5A3 RID: 46499 RVA: 0x00280191 File Offset: 0x0027E391
		public override void InitializeCell(TableCell cell, int columnIndex, GridItem inItem)
		{
			base.InitializeCell(cell, columnIndex, inItem);
			cell.Text = "&nbsp;";
		}

		// Token: 0x0600B5A4 RID: 46500 RVA: 0x002801A8 File Offset: 0x0027E3A8
		public override GridColumn Clone()
		{
			GridRowIndicatorColumn gridRowIndicatorColumn = new GridRowIndicatorColumn();
			gridRowIndicatorColumn.CopyBaseProperties(this);
			return gridRowIndicatorColumn;
		}

		// Token: 0x17003ABB RID: 15035
		// (get) Token: 0x0600B5A5 RID: 46501 RVA: 0x002801C3 File Offset: 0x0027E3C3
		// (set) Token: 0x0600B5A6 RID: 46502 RVA: 0x002801C6 File Offset: 0x0027E3C6
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

		// Token: 0x17003ABC RID: 15036
		// (get) Token: 0x0600B5A7 RID: 46503 RVA: 0x002801C8 File Offset: 0x0027E3C8
		// (set) Token: 0x0600B5A8 RID: 46504 RVA: 0x002801CB File Offset: 0x0027E3CB
		[DefaultValue(false)]
		public override bool Reorderable
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17003ABD RID: 15037
		// (get) Token: 0x0600B5A9 RID: 46505 RVA: 0x002801CD File Offset: 0x0027E3CD
		// (set) Token: 0x0600B5AA RID: 46506 RVA: 0x002801D5 File Offset: 0x0027E3D5
		[DefaultValue(false)]
		public override bool Resizable
		{
			get
			{
				return base.Resizable;
			}
			set
			{
				base.Resizable = value;
			}
		}

		// Token: 0x17003ABE RID: 15038
		// (get) Token: 0x0600B5AB RID: 46507 RVA: 0x002801DE File Offset: 0x0027E3DE
		// (set) Token: 0x0600B5AC RID: 46508 RVA: 0x002801E6 File Offset: 0x0027E3E6
		[DefaultValue(true)]
		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}

		// Token: 0x17003ABF RID: 15039
		// (get) Token: 0x0600B5AD RID: 46509 RVA: 0x002801EF File Offset: 0x0027E3EF
		// (set) Token: 0x0600B5AE RID: 46510 RVA: 0x002801F7 File Offset: 0x0027E3F7
		[DefaultValue("RowIndicator")]
		[NotifyParentProperty(true)]
		[Browsable(true)]
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

		// Token: 0x0600B5AF RID: 46511 RVA: 0x00280200 File Offset: 0x0027E400
		protected override string GenerateUniqueName()
		{
			return base.GenerateUniqueNameBase("RowIndicator");
		}

		// Token: 0x0600B5B0 RID: 46512 RVA: 0x00280210 File Offset: 0x0027E410
		public override void Initialize()
		{
			base.Initialize();
			if (!base.Owner.IsDesignMode)
			{
				Unit width = Unit.Pixel(20);
				if (base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile)
				{
					width = Unit.Pixel(41);
				}
				this.HeaderStyle.Width = width;
			}
		}
	}
}
