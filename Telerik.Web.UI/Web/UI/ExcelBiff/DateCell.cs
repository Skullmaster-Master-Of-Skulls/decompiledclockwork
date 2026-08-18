using System;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000A7D RID: 2685
	internal class DateCell : BiffCell
	{
		// Token: 0x0600675F RID: 26463 RVA: 0x00182686 File Offset: 0x00180886
		internal DateCell(DateTime dtValue)
		{
			this.datetime = dtValue;
			base.XFIndex = 15;
		}

		// Token: 0x06006760 RID: 26464 RVA: 0x001826A0 File Offset: 0x001808A0
		internal static double DateToDays(DateTime dateTime)
		{
			double num = dateTime.Subtract(DateCell.epoch).TotalDays + 1.0;
			if (num >= 60.0)
			{
				num += 1.0;
			}
			return num;
		}

		// Token: 0x06006761 RID: 26465 RVA: 0x001826E8 File Offset: 0x001808E8
		public override IRecord GetRecord(int row, int col)
		{
			double dValue = DateCell.DateToDays(this.datetime);
			return new Number((ushort)row, (ushort)col, (ushort)base.XFIndex, dValue);
		}

		// Token: 0x04001A17 RID: 6679
		private static DateTime epoch = new DateTime(1900, 1, 1, 0, 0, 0);

		// Token: 0x04001A18 RID: 6680
		private DateTime datetime;
	}
}
