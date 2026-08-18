using System;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x02000216 RID: 534
	public class DataGridViewRowsRemovedEventArgs : EventArgs
	{
		// Token: 0x060022D5 RID: 8917 RVA: 0x000A7154 File Offset: 0x000A5354
		public DataGridViewRowsRemovedEventArgs(int rowIndex, int rowCount)
		{
			if (rowIndex < 0)
			{
				throw new ArgumentOutOfRangeException("rowIndex", SR.GetString("InvalidLowBoundArgumentEx", new object[]
				{
					"rowIndex",
					rowIndex.ToString(CultureInfo.CurrentCulture),
					0.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (rowCount < 1)
			{
				throw new ArgumentOutOfRangeException("rowCount", SR.GetString("InvalidLowBoundArgumentEx", new object[]
				{
					"rowCount",
					rowCount.ToString(CultureInfo.CurrentCulture),
					1.ToString(CultureInfo.CurrentCulture)
				}));
			}
			this.rowIndex = rowIndex;
			this.rowCount = rowCount;
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x060022D6 RID: 8918 RVA: 0x000A7203 File Offset: 0x000A5403
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x060022D7 RID: 8919 RVA: 0x000A720B File Offset: 0x000A540B
		public int RowCount
		{
			get
			{
				return this.rowCount;
			}
		}

		// Token: 0x04000E68 RID: 3688
		private int rowIndex;

		// Token: 0x04000E69 RID: 3689
		private int rowCount;
	}
}
