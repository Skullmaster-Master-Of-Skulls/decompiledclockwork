using System;

namespace Telerik.Web.UI.ExportInfrastructure
{
	// Token: 0x02000A4B RID: 2635
	public class Column
	{
		// Token: 0x170021A5 RID: 8613
		// (get) Token: 0x06006611 RID: 26129 RVA: 0x0017DDC0 File Offset: 0x0017BFC0
		// (set) Token: 0x06006612 RID: 26130 RVA: 0x0017DDC8 File Offset: 0x0017BFC8
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

		// Token: 0x06006613 RID: 26131 RVA: 0x0017DDD1 File Offset: 0x0017BFD1
		public Column(Table tbl)
		{
			this.Table = tbl;
		}

		// Token: 0x170021A6 RID: 8614
		// (get) Token: 0x06006614 RID: 26132 RVA: 0x0017DDF8 File Offset: 0x0017BFF8
		// (set) Token: 0x06006615 RID: 26133 RVA: 0x0017DE13 File Offset: 0x0017C013
		public ExportStyle Style
		{
			get
			{
				if (this._style == null)
				{
					this._style = new ExportStyle();
				}
				return this._style;
			}
			set
			{
				this._style = value;
			}
		}

		// Token: 0x170021A7 RID: 8615
		// (get) Token: 0x06006616 RID: 26134 RVA: 0x0017DE1C File Offset: 0x0017C01C
		// (set) Token: 0x06006617 RID: 26135 RVA: 0x0017DE24 File Offset: 0x0017C024
		public double Width
		{
			get
			{
				return this._width;
			}
			set
			{
				this._width = value;
			}
		}

		// Token: 0x170021A8 RID: 8616
		// (get) Token: 0x06006618 RID: 26136 RVA: 0x0017DE2D File Offset: 0x0017C02D
		public CellCollection Cells
		{
			get
			{
				return this.Table.Cells.GetCellsByColumnIndex(this.Index);
			}
		}

		// Token: 0x170021A9 RID: 8617
		// (get) Token: 0x06006619 RID: 26137 RVA: 0x0017DE45 File Offset: 0x0017C045
		// (set) Token: 0x0600661A RID: 26138 RVA: 0x0017DE4D File Offset: 0x0017C04D
		public int Index
		{
			get
			{
				return this._index;
			}
			set
			{
				this._index = value;
			}
		}

		// Token: 0x170021AA RID: 8618
		// (get) Token: 0x0600661B RID: 26139 RVA: 0x0017DE56 File Offset: 0x0017C056
		internal int FirstRowIndex
		{
			get
			{
				if (this._firstRowIndex == null)
				{
					this.PopulateMinMaxRowIndices();
				}
				return this._firstRowIndex.Value;
			}
		}

		// Token: 0x170021AB RID: 8619
		// (get) Token: 0x0600661C RID: 26140 RVA: 0x0017DE76 File Offset: 0x0017C076
		internal int LastRowIndex
		{
			get
			{
				if (this._lastRowIndex == null)
				{
					this.PopulateMinMaxRowIndices();
				}
				return this._lastRowIndex.Value;
			}
		}

		// Token: 0x0600661D RID: 26141 RVA: 0x0017DE98 File Offset: 0x0017C098
		private void PopulateMinMaxRowIndices()
		{
			CellCollection cells = this.Cells;
			if (cells.Count == 0)
			{
				this._firstRowIndex = (this._lastRowIndex = new int?(-1));
				return;
			}
			foreach (Cell cell in this.Cells)
			{
				if (this._firstRowIndex == null)
				{
					this._firstRowIndex = new int?(cell.RowIndex);
				}
				if (this._lastRowIndex == null)
				{
					this._lastRowIndex = new int?(cell.RowIndex);
				}
				if (cell.RowIndex < this._firstRowIndex)
				{
					this._firstRowIndex = new int?(cell.RowIndex);
				}
				if (cell.RowIndex > this._lastRowIndex)
				{
					this._lastRowIndex = new int?(cell.RowIndex);
				}
			}
		}

		// Token: 0x040018B3 RID: 6323
		private ExportStyle _style;

		// Token: 0x040018B4 RID: 6324
		private int _index;

		// Token: 0x040018B5 RID: 6325
		private double _width;

		// Token: 0x040018B6 RID: 6326
		private Table _table;

		// Token: 0x040018B7 RID: 6327
		private int? _firstRowIndex = null;

		// Token: 0x040018B8 RID: 6328
		private int? _lastRowIndex = null;
	}
}
