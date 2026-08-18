using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x020013FF RID: 5119
	internal class RowSpanMgr
	{
		// Token: 0x0600D258 RID: 53848 RVA: 0x002EA1FE File Offset: 0x002E83FE
		public RowSpanMgr(int numCols)
		{
			this.spanInfo = new RowSpanMgr.SpanInfo[numCols];
		}

		// Token: 0x0600D259 RID: 53849 RVA: 0x002EA214 File Offset: 0x002E8414
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		public void AddRowSpan(TableCell cell, int firstCol, int numCols, int cellHeight, int rowsSpanned)
		{
			this.spanInfo[firstCol - 1] = new RowSpanMgr.SpanInfo(cell, cellHeight, rowsSpanned);
			for (int i = 0; i < numCols - 1; i++)
			{
				this.spanInfo[firstCol + i] = new RowSpanMgr.SpanInfo(null, cellHeight, rowsSpanned);
			}
		}

		// Token: 0x0600D25A RID: 53850 RVA: 0x002EA257 File Offset: 0x002E8457
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		public bool IsSpanned(int colNum)
		{
			return this.spanInfo[colNum - 1] != null;
		}

		// Token: 0x0600D25B RID: 53851 RVA: 0x002EA269 File Offset: 0x002E8469
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		public TableCell GetSpanningCell(int colNum)
		{
			if (this.spanInfo[colNum - 1] != null)
			{
				return this.spanInfo[colNum - 1].cell;
			}
			return null;
		}

		// Token: 0x0600D25C RID: 53852 RVA: 0x002EA288 File Offset: 0x002E8488
		public bool HasUnfinishedSpans()
		{
			for (int i = 0; i < this.spanInfo.Length; i++)
			{
				if (this.spanInfo[i] != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600D25D RID: 53853 RVA: 0x002EA2B8 File Offset: 0x002E84B8
		public void FinishRow(int rowHeight)
		{
			for (int i = 0; i < this.spanInfo.Length; i++)
			{
				if (this.spanInfo[i] != null && this.spanInfo[i].finishRow(rowHeight))
				{
					this.spanInfo[i] = null;
				}
			}
		}

		// Token: 0x0600D25E RID: 53854 RVA: 0x002EA2FB File Offset: 0x002E84FB
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		public int GetRemainingHeight(int colNum)
		{
			if (this.spanInfo[colNum - 1] != null)
			{
				return this.spanInfo[colNum - 1].heightRemaining();
			}
			return 0;
		}

		// Token: 0x0600D25F RID: 53855 RVA: 0x002EA31A File Offset: 0x002E851A
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		public bool IsInLastRow(int colNum)
		{
			return this.spanInfo[colNum - 1] != null && this.spanInfo[colNum - 1].isInLastRow();
		}

		// Token: 0x0600D260 RID: 53856 RVA: 0x002EA339 File Offset: 0x002E8539
		public void SetIgnoreKeeps(bool ignoreKeeps)
		{
			this.ignoreKeeps = ignoreKeeps;
		}

		// Token: 0x0600D261 RID: 53857 RVA: 0x002EA342 File Offset: 0x002E8542
		public bool IgnoreKeeps()
		{
			return this.ignoreKeeps;
		}

		// Token: 0x040038C1 RID: 14529
		private RowSpanMgr.SpanInfo[] spanInfo;

		// Token: 0x040038C2 RID: 14530
		private bool ignoreKeeps;

		// Token: 0x02001400 RID: 5120
		public class SpanInfo
		{
			// Token: 0x0600D262 RID: 53858 RVA: 0x002EA34A File Offset: 0x002E854A
			public SpanInfo(TableCell cell, int cellHeight, int rowsSpanned)
			{
				this.cell = cell;
				this.cellHeight = cellHeight;
				this.totalRowHeight = 0;
				this.rowsRemaining = rowsSpanned;
			}

			// Token: 0x0600D263 RID: 53859 RVA: 0x002EA370 File Offset: 0x002E8570
			public int heightRemaining()
			{
				int num = this.cellHeight - this.totalRowHeight;
				if (num <= 0)
				{
					return 0;
				}
				return num;
			}

			// Token: 0x0600D264 RID: 53860 RVA: 0x002EA392 File Offset: 0x002E8592
			public bool isInLastRow()
			{
				return this.rowsRemaining == 1;
			}

			// Token: 0x0600D265 RID: 53861 RVA: 0x002EA3A0 File Offset: 0x002E85A0
			public bool finishRow(int rowHeight)
			{
				this.totalRowHeight += rowHeight;
				if (--this.rowsRemaining == 0)
				{
					if (this.cell != null)
					{
						this.cell.SetRowHeight(this.totalRowHeight);
					}
					return true;
				}
				return false;
			}

			// Token: 0x040038C3 RID: 14531
			public int cellHeight;

			// Token: 0x040038C4 RID: 14532
			public int totalRowHeight;

			// Token: 0x040038C5 RID: 14533
			public int rowsRemaining;

			// Token: 0x040038C6 RID: 14534
			public TableCell cell;
		}
	}
}
