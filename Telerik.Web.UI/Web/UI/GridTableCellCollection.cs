using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02000B80 RID: 2944
	public class GridTableCellCollection : ICollection, IEnumerable
	{
		// Token: 0x06006F44 RID: 28484 RVA: 0x0019FFDE File Offset: 0x0019E1DE
		public GridTableCellCollection()
		{
			this.cells = new ArrayList();
		}

		// Token: 0x06006F45 RID: 28485 RVA: 0x0019FFF1 File Offset: 0x0019E1F1
		public GridTableCellCollection(ArrayList cellsCollection)
		{
			this.cells = cellsCollection;
		}

		// Token: 0x17002473 RID: 9331
		public GridTableCell this[int index]
		{
			get
			{
				return (GridTableCell)this.cells[index];
			}
		}

		// Token: 0x17002474 RID: 9332
		public GridTableCell this[string hierarchicalIndex]
		{
			get
			{
				GridTableCell gridTableCell = this.FindByHierarchicalIndex(hierarchicalIndex);
				if (gridTableCell == null)
				{
					throw new ArgumentOutOfRangeException("CellHierarchicalIndex");
				}
				return gridTableCell;
			}
		}

		// Token: 0x06006F48 RID: 28488 RVA: 0x001A0038 File Offset: 0x0019E238
		internal GridTableCell FindByHierarchicalIndex(string hierarchicalIndex)
		{
			GridTableCell result = null;
			foreach (object obj in this.cells)
			{
				GridTableCell gridTableCell = (GridTableCell)obj;
				if (gridTableCell.CellIndexHierarchical == hierarchicalIndex)
				{
					result = gridTableCell;
				}
			}
			return result;
		}

		// Token: 0x06006F49 RID: 28489 RVA: 0x001A00A0 File Offset: 0x0019E2A0
		public void Add(GridTableCell cell)
		{
			this.cells.Add(cell);
		}

		// Token: 0x06006F4A RID: 28490 RVA: 0x001A00AF File Offset: 0x0019E2AF
		public void AddRange(GridTableCellCollection extraCells)
		{
			this.cells.AddRange(extraCells);
		}

		// Token: 0x06006F4B RID: 28491 RVA: 0x001A00BD File Offset: 0x0019E2BD
		public void AddRange(GridTableCell[] extraCells)
		{
			this.cells.AddRange(extraCells);
		}

		// Token: 0x06006F4C RID: 28492 RVA: 0x001A00CB File Offset: 0x0019E2CB
		public IEnumerator GetEnumerator()
		{
			return this.cells.GetEnumerator();
		}

		// Token: 0x06006F4D RID: 28493 RVA: 0x001A00D8 File Offset: 0x0019E2D8
		public void CopyTo(Array array, int index)
		{
			this.cells.CopyTo(array, index);
		}

		// Token: 0x17002475 RID: 9333
		// (get) Token: 0x06006F4E RID: 28494 RVA: 0x001A00E7 File Offset: 0x0019E2E7
		public int Count
		{
			get
			{
				return this.cells.Count;
			}
		}

		// Token: 0x17002476 RID: 9334
		// (get) Token: 0x06006F4F RID: 28495 RVA: 0x001A00F4 File Offset: 0x0019E2F4
		public bool IsSynchronized
		{
			get
			{
				return this.cells.IsSynchronized;
			}
		}

		// Token: 0x17002477 RID: 9335
		// (get) Token: 0x06006F50 RID: 28496 RVA: 0x001A0101 File Offset: 0x0019E301
		public object SyncRoot
		{
			get
			{
				return this.cells.SyncRoot;
			}
		}

		// Token: 0x04001E05 RID: 7685
		private ArrayList cells;
	}
}
