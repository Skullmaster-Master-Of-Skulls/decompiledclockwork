using System;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Calendar.Utils;

namespace Telerik.Web.UI.Calendar.View
{
	// Token: 0x02000A3C RID: 2620
	public class MonthYearViewRow : TableRow
	{
		// Token: 0x060063E9 RID: 25577 RVA: 0x001779B8 File Offset: 0x00175BB8
		public MonthYearViewRow()
		{
		}

		// Token: 0x170020CE RID: 8398
		// (get) Token: 0x060063EA RID: 25578 RVA: 0x001779C0 File Offset: 0x00175BC0
		// (set) Token: 0x060063EB RID: 25579 RVA: 0x001779C8 File Offset: 0x00175BC8
		public MonthYearViewRowType RowType { get; protected set; }

		// Token: 0x170020CF RID: 8399
		// (get) Token: 0x060063EC RID: 25580 RVA: 0x001779D1 File Offset: 0x00175BD1
		// (set) Token: 0x060063ED RID: 25581 RVA: 0x001779D9 File Offset: 0x00175BD9
		public RadMonthYearPicker OwnerMonthYearPicker { get; internal set; }

		// Token: 0x060063EE RID: 25582 RVA: 0x001779E2 File Offset: 0x00175BE2
		public MonthYearViewRow(RadMonthYearPicker ownerMonthYearPicker, MonthYearViewRowType rowType)
		{
			this.OwnerMonthYearPicker = ownerMonthYearPicker;
			this.RowType = rowType;
		}

		// Token: 0x060063EF RID: 25583 RVA: 0x001779F8 File Offset: 0x00175BF8
		public virtual void Initialize(int? index)
		{
			MonthYearViewRowHelper.CreateViewCells(this.RowType, index, this.OwnerMonthYearPicker, this.Cells);
		}
	}
}
