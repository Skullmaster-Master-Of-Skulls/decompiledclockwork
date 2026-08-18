using System;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AC3 RID: 2755
	internal class NumberCell : BiffCell
	{
		// Token: 0x06006844 RID: 26692 RVA: 0x00186A39 File Offset: 0x00184C39
		public NumberCell(object dValue)
		{
			this.value = Convert.ToDouble(dValue);
			base.XFIndex = 15;
		}

		// Token: 0x06006845 RID: 26693 RVA: 0x00186A55 File Offset: 0x00184C55
		public override IRecord GetRecord(int row, int col)
		{
			return new Number((ushort)row, (ushort)col, (ushort)base.XFIndex, this.value);
		}

		// Token: 0x04001B7C RID: 7036
		private double value;
	}
}
