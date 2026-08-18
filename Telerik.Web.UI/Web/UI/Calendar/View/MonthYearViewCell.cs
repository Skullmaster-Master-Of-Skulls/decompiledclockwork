using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Calendar.Utils;

namespace Telerik.Web.UI.Calendar.View
{
	// Token: 0x02000A3B RID: 2619
	public class MonthYearViewCell : TableCell
	{
		// Token: 0x060063E1 RID: 25569 RVA: 0x00177927 File Offset: 0x00175B27
		public MonthYearViewCell()
		{
		}

		// Token: 0x170020CC RID: 8396
		// (get) Token: 0x060063E2 RID: 25570 RVA: 0x0017792F File Offset: 0x00175B2F
		// (set) Token: 0x060063E3 RID: 25571 RVA: 0x00177937 File Offset: 0x00175B37
		public MonthYearViewCellType CellType { get; protected set; }

		// Token: 0x170020CD RID: 8397
		// (get) Token: 0x060063E4 RID: 25572 RVA: 0x00177940 File Offset: 0x00175B40
		// (set) Token: 0x060063E5 RID: 25573 RVA: 0x00177948 File Offset: 0x00175B48
		public RadMonthYearPicker OwnerMonthYearPicker { get; internal set; }

		// Token: 0x060063E6 RID: 25574 RVA: 0x00177951 File Offset: 0x00175B51
		public MonthYearViewCell(RadMonthYearPicker ownerMonthYearPicker, MonthYearViewCellType cellType)
		{
			this.OwnerMonthYearPicker = ownerMonthYearPicker;
			this.CellType = cellType;
		}

		// Token: 0x060063E7 RID: 25575 RVA: 0x00177967 File Offset: 0x00175B67
		public virtual void Initialize(int? index)
		{
			MonthYearViewCellHelper.CreateChildControls(this, this.CellType, index, this.OwnerMonthYearPicker);
		}

		// Token: 0x060063E8 RID: 25576 RVA: 0x0017797C File Offset: 0x00175B7C
		protected override void Render(HtmlTextWriter writer)
		{
			if (this.CellType == MonthYearViewCellType.MonthCell)
			{
				base.ApplyStyle(this.OwnerMonthYearPicker.MonthCellsStyle);
			}
			if (this.CellType == MonthYearViewCellType.YearCell)
			{
				base.ApplyStyle(this.OwnerMonthYearPicker.YearCellsStyle);
			}
			base.Render(writer);
		}
	}
}
