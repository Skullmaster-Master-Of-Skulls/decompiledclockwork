using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x02000391 RID: 913
	[TypeConverter(typeof(TableLayoutPanelCellPositionTypeConverter))]
	public struct TableLayoutPanelCellPosition
	{
		// Token: 0x06003BE6 RID: 15334 RVA: 0x001060CC File Offset: 0x001042CC
		public TableLayoutPanelCellPosition(int column, int row)
		{
			if (row < -1)
			{
				throw new ArgumentOutOfRangeException("row", SR.GetString("InvalidArgument", new object[]
				{
					"row",
					row.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (column < -1)
			{
				throw new ArgumentOutOfRangeException("column", SR.GetString("InvalidArgument", new object[]
				{
					"column",
					column.ToString(CultureInfo.CurrentCulture)
				}));
			}
			this.row = row;
			this.column = column;
		}

		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x06003BE7 RID: 15335 RVA: 0x00106153 File Offset: 0x00104353
		// (set) Token: 0x06003BE8 RID: 15336 RVA: 0x0010615B File Offset: 0x0010435B
		public int Row
		{
			get
			{
				return this.row;
			}
			set
			{
				this.row = value;
			}
		}

		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x06003BE9 RID: 15337 RVA: 0x00106164 File Offset: 0x00104364
		// (set) Token: 0x06003BEA RID: 15338 RVA: 0x0010616C File Offset: 0x0010436C
		public int Column
		{
			get
			{
				return this.column;
			}
			set
			{
				this.column = value;
			}
		}

		// Token: 0x06003BEB RID: 15339 RVA: 0x00106178 File Offset: 0x00104378
		public override bool Equals(object other)
		{
			if (other is TableLayoutPanelCellPosition)
			{
				TableLayoutPanelCellPosition tableLayoutPanelCellPosition = (TableLayoutPanelCellPosition)other;
				return tableLayoutPanelCellPosition.row == this.row && tableLayoutPanelCellPosition.column == this.column;
			}
			return false;
		}

		// Token: 0x06003BEC RID: 15340 RVA: 0x001061B4 File Offset: 0x001043B4
		public static bool operator ==(TableLayoutPanelCellPosition p1, TableLayoutPanelCellPosition p2)
		{
			return p1.Row == p2.Row && p1.Column == p2.Column;
		}

		// Token: 0x06003BED RID: 15341 RVA: 0x001061D8 File Offset: 0x001043D8
		public static bool operator !=(TableLayoutPanelCellPosition p1, TableLayoutPanelCellPosition p2)
		{
			return !(p1 == p2);
		}

		// Token: 0x06003BEE RID: 15342 RVA: 0x001061E4 File Offset: 0x001043E4
		public override string ToString()
		{
			return this.Column.ToString(CultureInfo.CurrentCulture) + "," + this.Row.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06003BEF RID: 15343 RVA: 0x00106221 File Offset: 0x00104421
		public override int GetHashCode()
		{
			return WindowsFormsUtils.GetCombinedHashCodes(new int[]
			{
				this.row,
				this.column
			});
		}

		// Token: 0x0400238F RID: 9103
		private int row;

		// Token: 0x04002390 RID: 9104
		private int column;
	}
}
