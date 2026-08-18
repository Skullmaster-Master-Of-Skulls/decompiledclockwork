using System;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x0200017F RID: 383
	public struct DataGridCell
	{
		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x000507E0 File Offset: 0x0004E9E0
		// (set) Token: 0x06001651 RID: 5713 RVA: 0x000507E8 File Offset: 0x0004E9E8
		public int ColumnNumber
		{
			get
			{
				return this.columnNumber;
			}
			set
			{
				this.columnNumber = value;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x000507F1 File Offset: 0x0004E9F1
		// (set) Token: 0x06001653 RID: 5715 RVA: 0x000507F9 File Offset: 0x0004E9F9
		public int RowNumber
		{
			get
			{
				return this.rowNumber;
			}
			set
			{
				this.rowNumber = value;
			}
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x00050802 File Offset: 0x0004EA02
		public DataGridCell(int r, int c)
		{
			this.rowNumber = r;
			this.columnNumber = c;
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x00050814 File Offset: 0x0004EA14
		public override bool Equals(object o)
		{
			if (o is DataGridCell)
			{
				DataGridCell dataGridCell = (DataGridCell)o;
				return dataGridCell.RowNumber == this.RowNumber && dataGridCell.ColumnNumber == this.ColumnNumber;
			}
			return false;
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x00050852 File Offset: 0x0004EA52
		public override int GetHashCode()
		{
			return (~this.rowNumber * (this.columnNumber + 1) & 16776960) >> 8;
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x0005086C File Offset: 0x0004EA6C
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"DataGridCell {RowNumber = ",
				this.RowNumber.ToString(CultureInfo.CurrentCulture),
				", ColumnNumber = ",
				this.ColumnNumber.ToString(CultureInfo.CurrentCulture),
				"}"
			});
		}

		// Token: 0x04000A41 RID: 2625
		private int rowNumber;

		// Token: 0x04000A42 RID: 2626
		private int columnNumber;
	}
}
