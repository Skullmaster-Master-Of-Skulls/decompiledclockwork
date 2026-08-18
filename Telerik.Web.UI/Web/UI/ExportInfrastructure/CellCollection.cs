using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Telerik.Web.UI.Export;

namespace Telerik.Web.UI.ExportInfrastructure
{
	// Token: 0x02000A49 RID: 2633
	public class CellCollection : IEnumerable<Cell>, IEnumerable
	{
		// Token: 0x17002189 RID: 8585
		// (get) Token: 0x060065CE RID: 26062 RVA: 0x0017D0AF File Offset: 0x0017B2AF
		// (set) Token: 0x060065CF RID: 26063 RVA: 0x0017D0B7 File Offset: 0x0017B2B7
		public Table Table
		{
			get
			{
				return this._table;
			}
			internal set
			{
				this._table = value;
			}
		}

		// Token: 0x060065D0 RID: 26064 RVA: 0x0017D0C0 File Offset: 0x0017B2C0
		internal CellCollection(Table tbl)
		{
			this.Table = tbl;
		}

		// Token: 0x060065D1 RID: 26065 RVA: 0x0017D115 File Offset: 0x0017B315
		public IEnumerator<Cell> GetEnumerator()
		{
			return this._cellCollection.Values.GetEnumerator();
		}

		// Token: 0x060065D2 RID: 26066 RVA: 0x0017D12C File Offset: 0x0017B32C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._cellCollection.Values.GetEnumerator();
		}

		// Token: 0x1700218A RID: 8586
		// (get) Token: 0x060065D3 RID: 26067 RVA: 0x0017D143 File Offset: 0x0017B343
		public int Count
		{
			get
			{
				return this._cellCollection.Values.Count;
			}
		}

		// Token: 0x1700218B RID: 8587
		public Cell this[int column, int row]
		{
			get
			{
				long hashCode = Utils.GetHashCode(column, row);
				if (!this._cellCollection.ContainsKey(hashCode))
				{
					this._cellCollection.Add(hashCode, new Cell(this.Table)
					{
						Index = new Point(column, row)
					});
				}
				return this._cellCollection[hashCode];
			}
			set
			{
				long hashCode = Utils.GetHashCode(column, row);
				if (!this._cellCollection.ContainsKey(hashCode))
				{
					this._cellCollection.Add(hashCode, new Cell(this.Table)
					{
						Index = new Point(column, row)
					});
				}
				this._cellCollection[hashCode] = value;
			}
		}

		// Token: 0x1700218C RID: 8588
		public Cell this[string index]
		{
			get
			{
				Point index2 = Utils.ConvertExcelCellIndexToPoint(index);
				long hashCode = Utils.GetHashCode(index2.X, index2.Y);
				if (!this._cellCollection.ContainsKey(hashCode))
				{
					this._cellCollection.Add(hashCode, new Cell(this.Table)
					{
						Index = index2
					});
				}
				return this._cellCollection[hashCode];
			}
			set
			{
				Point index2 = Utils.ConvertExcelCellIndexToPoint(index);
				long hashCode = Utils.GetHashCode(index2.X, index2.Y);
				if (!this._cellCollection.ContainsKey(hashCode))
				{
					this._cellCollection.Add(hashCode, new Cell(this.Table)
					{
						Index = index2
					});
				}
				this._cellCollection[hashCode] = value;
			}
		}

		// Token: 0x1700218D RID: 8589
		public Cell this[Point index]
		{
			get
			{
				long hashCode = Utils.GetHashCode(index.X, index.Y);
				if (!this._cellCollection.ContainsKey(hashCode))
				{
					this._cellCollection.Add(hashCode, new Cell(this.Table)
					{
						Index = new Point(index.X, index.Y)
					});
				}
				return this._cellCollection[hashCode];
			}
			set
			{
				long hashCode = Utils.GetHashCode(index.X, index.Y);
				if (!this._cellCollection.ContainsKey(hashCode))
				{
					this._cellCollection.Add(hashCode, new Cell(this.Table)
					{
						Index = new Point(index.X, index.Y)
					});
				}
				this._cellCollection[hashCode] = value;
			}
		}

		// Token: 0x060065DA RID: 26074 RVA: 0x0017D3B0 File Offset: 0x0017B5B0
		internal CellCollection GetCellsByRowIndex(int rowIndex)
		{
			CellCollection cellCollection = new CellCollection(this.Table);
			foreach (long num in this._cellCollection.Keys)
			{
				if (Utils.GetRowFromHashCode(num) == rowIndex)
				{
					cellCollection.Add(this._cellCollection[num]);
				}
			}
			return cellCollection;
		}

		// Token: 0x060065DB RID: 26075 RVA: 0x0017D42C File Offset: 0x0017B62C
		internal CellCollection GetCellsByColumnIndex(int colIndex)
		{
			CellCollection cellCollection = new CellCollection(this.Table);
			foreach (long num in this._cellCollection.Keys)
			{
				if (Utils.GetColFromHashCode(num) == colIndex)
				{
					cellCollection.Add(this._cellCollection[num]);
				}
			}
			return cellCollection;
		}

		// Token: 0x060065DC RID: 26076 RVA: 0x0017D4A8 File Offset: 0x0017B6A8
		internal void Add(Cell cell)
		{
			this._cellCollection.Add(Utils.GetHashCode(cell.Index.X, cell.Index.Y), cell);
		}

		// Token: 0x060065DD RID: 26077 RVA: 0x0017D4E2 File Offset: 0x0017B6E2
		internal Cell GetCell(int col, int row)
		{
			return this._cellCollection[Utils.GetHashCode(col, row)];
		}

		// Token: 0x060065DE RID: 26078 RVA: 0x0017D4F8 File Offset: 0x0017B6F8
		internal Cell GetCellSafe(int col, int row)
		{
			Cell result = null;
			this._cellCollection.TryGetValue(Utils.GetHashCode(col, row), out result);
			return result;
		}

		// Token: 0x1700218E RID: 8590
		// (get) Token: 0x060065DF RID: 26079 RVA: 0x0017D51D File Offset: 0x0017B71D
		// (set) Token: 0x060065E0 RID: 26080 RVA: 0x0017D53D File Offset: 0x0017B73D
		internal int LastCellColumnIndex
		{
			get
			{
				if (this._lastCellColumnIndex == null)
				{
					this.PopulateMinMaxIndices();
				}
				return this._lastCellColumnIndex.Value;
			}
			set
			{
				this._lastCellColumnIndex = new int?(value);
			}
		}

		// Token: 0x1700218F RID: 8591
		// (get) Token: 0x060065E1 RID: 26081 RVA: 0x0017D54B File Offset: 0x0017B74B
		// (set) Token: 0x060065E2 RID: 26082 RVA: 0x0017D56B File Offset: 0x0017B76B
		internal int FirstCellColumnIndex
		{
			get
			{
				if (this._firstCellColumnIndex == null)
				{
					this.PopulateMinMaxIndices();
				}
				return this._firstCellColumnIndex.Value;
			}
			set
			{
				this._firstCellColumnIndex = new int?(value);
			}
		}

		// Token: 0x17002190 RID: 8592
		// (get) Token: 0x060065E3 RID: 26083 RVA: 0x0017D579 File Offset: 0x0017B779
		// (set) Token: 0x060065E4 RID: 26084 RVA: 0x0017D599 File Offset: 0x0017B799
		internal int FirstCellRowIndex
		{
			get
			{
				if (this._firstCellRowIndex == null)
				{
					this.PopulateMinMaxIndices();
				}
				return this._firstCellRowIndex.Value;
			}
			set
			{
				this._firstCellRowIndex = new int?(value);
			}
		}

		// Token: 0x17002191 RID: 8593
		// (get) Token: 0x060065E5 RID: 26085 RVA: 0x0017D5A7 File Offset: 0x0017B7A7
		// (set) Token: 0x060065E6 RID: 26086 RVA: 0x0017D5C7 File Offset: 0x0017B7C7
		internal int LastCellRowIndex
		{
			get
			{
				if (this._lastCellRowIndex == null)
				{
					this.PopulateMinMaxIndices();
				}
				return this._lastCellRowIndex.Value;
			}
			set
			{
				this._lastCellRowIndex = new int?(value);
			}
		}

		// Token: 0x060065E7 RID: 26087 RVA: 0x0017D5D8 File Offset: 0x0017B7D8
		private void PopulateMinMaxIndices()
		{
			if (this._cellCollection.Count == 0)
			{
				this._lastCellRowIndex = (this._firstCellRowIndex = (this._lastCellColumnIndex = (this._firstCellColumnIndex = new int?(-1))));
				return;
			}
			foreach (Cell cell in this._cellCollection.Values)
			{
				if (this._firstCellColumnIndex == null)
				{
					this._firstCellColumnIndex = new int?(cell.ColIndex);
				}
				if (this._firstCellRowIndex == null)
				{
					this._firstCellRowIndex = new int?(cell.RowIndex);
				}
				if (this._lastCellColumnIndex == null)
				{
					this._lastCellColumnIndex = new int?(cell.ColIndex);
				}
				if (this._lastCellRowIndex == null)
				{
					this._lastCellRowIndex = new int?(cell.RowIndex);
				}
				if (cell.ColIndex < this._firstCellColumnIndex)
				{
					this._firstCellColumnIndex = new int?(cell.ColIndex);
				}
				if (cell.ColIndex > this._lastCellColumnIndex)
				{
					this._lastCellColumnIndex = new int?(cell.ColIndex);
				}
				if (cell.RowIndex < this._firstCellRowIndex)
				{
					this._firstCellRowIndex = new int?(cell.RowIndex);
				}
				if (cell.RowIndex > this._lastCellRowIndex)
				{
					this._lastCellRowIndex = new int?(cell.RowIndex);
				}
			}
		}

		// Token: 0x060065E8 RID: 26088 RVA: 0x0017D7CC File Offset: 0x0017B9CC
		internal void ChangeCellIndex(Cell cell, Point newIndex)
		{
			this._cellCollection.Remove(Utils.GetHashCode(cell.Index.X, cell.Index.Y));
			cell.Index = newIndex;
			this._cellCollection.Add(Utils.GetHashCode(newIndex.X, newIndex.Y), cell);
		}

		// Token: 0x0400189C RID: 6300
		private Dictionary<long, Cell> _cellCollection = new Dictionary<long, Cell>();

		// Token: 0x0400189D RID: 6301
		private Table _table;

		// Token: 0x0400189E RID: 6302
		private int? _lastCellColumnIndex = null;

		// Token: 0x0400189F RID: 6303
		private int? _firstCellColumnIndex = null;

		// Token: 0x040018A0 RID: 6304
		private int? _lastCellRowIndex = null;

		// Token: 0x040018A1 RID: 6305
		private int? _firstCellRowIndex = null;
	}
}
