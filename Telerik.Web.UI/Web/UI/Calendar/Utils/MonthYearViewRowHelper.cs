using System;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Calendar.View;

namespace Telerik.Web.UI.Calendar.Utils
{
	// Token: 0x02000A39 RID: 2617
	internal class MonthYearViewRowHelper
	{
		// Token: 0x060063CF RID: 25551 RVA: 0x00177464 File Offset: 0x00175664
		internal static void CreateViewCells(MonthYearViewRowType rowType, int? index, RadMonthYearPicker ownerMonthYearPicker, TableCellCollection collection)
		{
			switch (rowType)
			{
			case MonthYearViewRowType.BodyRow:
			{
				int? num = index * 2;
				for (int i = 0; i < 2; i++)
				{
					MonthYearViewRowHelper.CreateViewCell(MonthYearViewCellType.MonthCell, ownerMonthYearPicker, num + i, collection);
				}
				for (int j = 0; j < 2; j++)
				{
					MonthYearViewRowHelper.CreateViewCell(MonthYearViewCellType.YearCell, ownerMonthYearPicker, index + j * 5, collection);
				}
				return;
			}
			case MonthYearViewRowType.NavigationRow:
				for (int k = 0; k < 2; k++)
				{
					MonthYearViewRowHelper.CreateViewCell(MonthYearViewCellType.MonthCell, ownerMonthYearPicker, index + k, collection);
				}
				for (int l = 0; l < 2; l++)
				{
					MonthYearViewRowHelper.CreateViewCell(MonthYearViewCellType.NavigationCell, ownerMonthYearPicker, index + l, collection);
				}
				return;
			case MonthYearViewRowType.FooterRow:
				MonthYearViewRowHelper.CreateViewCell(MonthYearViewCellType.ButtonCell, ownerMonthYearPicker, null, collection);
				return;
			default:
				return;
			}
		}

		// Token: 0x060063D0 RID: 25552 RVA: 0x001775C4 File Offset: 0x001757C4
		private static void CreateViewCell(MonthYearViewCellType cellType, RadMonthYearPicker ownerMonthYearPicker, int? index, TableCellCollection collection)
		{
			MonthYearViewCell monthYearViewCell = new MonthYearViewCell(ownerMonthYearPicker, cellType);
			collection.Add(monthYearViewCell);
			monthYearViewCell.Initialize(index);
			MonthYearViewRowHelper.FireCellCreatedEvent(monthYearViewCell, ownerMonthYearPicker);
		}

		// Token: 0x060063D1 RID: 25553 RVA: 0x001775F0 File Offset: 0x001757F0
		private static void FireCellCreatedEvent(MonthYearViewCell cell, RadMonthYearPicker ownerMonthYearPicker)
		{
			MonthYearViewCellCreatedEventArgs eventArgs = new MonthYearViewCellCreatedEventArgs(cell);
			ownerMonthYearPicker.FireViewCellCreated(eventArgs);
		}
	}
}
