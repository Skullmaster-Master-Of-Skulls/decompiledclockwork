using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000A77 RID: 2679
	internal sealed class CellGrid
	{
		// Token: 0x06006745 RID: 26437 RVA: 0x0018217C File Offset: 0x0018037C
		internal CellGrid(Worksheet worksheet)
		{
			this.worksheet = worksheet;
			this.cells = new Dictionary<long, Cell>();
		}

		// Token: 0x17002203 RID: 8707
		internal Cell this[int col, int row]
		{
			get
			{
				long key = this.GetKey(col, row);
				if (!this.cells.ContainsKey(key))
				{
					this.cells[key] = new Cell(this.worksheet);
				}
				return this.cells[key];
			}
			set
			{
				long key = this.GetKey(col, row);
				this.cells[key] = value;
			}
		}

		// Token: 0x17002204 RID: 8708
		// (get) Token: 0x06006748 RID: 26440 RVA: 0x00182204 File Offset: 0x00180404
		internal int LastColumn
		{
			get
			{
				int num = -1;
				foreach (long num2 in this.cells.Keys)
				{
					int num3 = (int)num2;
					if (num3 > num)
					{
						num = num3;
					}
				}
				return num;
			}
		}

		// Token: 0x17002205 RID: 8709
		// (get) Token: 0x06006749 RID: 26441 RVA: 0x00182264 File Offset: 0x00180464
		internal int LastRow
		{
			get
			{
				int num = -1;
				foreach (long num2 in this.cells.Keys)
				{
					int num3 = (int)(num2 >> 32);
					if (num3 > num)
					{
						num = num3;
					}
				}
				return num;
			}
		}

		// Token: 0x0600674A RID: 26442 RVA: 0x001822C4 File Offset: 0x001804C4
		private int GetCol(long key)
		{
			return (int)key;
		}

		// Token: 0x0600674B RID: 26443 RVA: 0x001822C8 File Offset: 0x001804C8
		private int GetRow(long key)
		{
			return (int)(key >> 32);
		}

		// Token: 0x0600674C RID: 26444 RVA: 0x001822D0 File Offset: 0x001804D0
		internal long GetKey(int col, int row)
		{
			long num = (long)col;
			long num2 = (long)row;
			return (num2 << 32) + num;
		}

		// Token: 0x04001A00 RID: 6656
		private Dictionary<long, Cell> cells;

		// Token: 0x04001A01 RID: 6657
		private Worksheet worksheet;
	}
}
