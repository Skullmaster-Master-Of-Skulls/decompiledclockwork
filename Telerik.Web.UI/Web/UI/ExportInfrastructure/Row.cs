using System;

namespace Telerik.Web.UI.ExportInfrastructure
{
	// Token: 0x02000A53 RID: 2643
	public class Row
	{
		// Token: 0x170021BA RID: 8634
		// (get) Token: 0x06006666 RID: 26214 RVA: 0x0017FAB8 File Offset: 0x0017DCB8
		// (set) Token: 0x06006667 RID: 26215 RVA: 0x0017FAC0 File Offset: 0x0017DCC0
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

		// Token: 0x06006668 RID: 26216 RVA: 0x0017FAC9 File Offset: 0x0017DCC9
		public Row(Table tbl)
		{
			this.Table = tbl;
		}

		// Token: 0x170021BB RID: 8635
		// (get) Token: 0x06006669 RID: 26217 RVA: 0x0017FAF0 File Offset: 0x0017DCF0
		// (set) Token: 0x0600666A RID: 26218 RVA: 0x0017FB0B File Offset: 0x0017DD0B
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

		// Token: 0x170021BC RID: 8636
		// (get) Token: 0x0600666B RID: 26219 RVA: 0x0017FB14 File Offset: 0x0017DD14
		// (set) Token: 0x0600666C RID: 26220 RVA: 0x0017FB1C File Offset: 0x0017DD1C
		public double Height
		{
			get
			{
				return this._height;
			}
			set
			{
				this._height = value;
			}
		}

		// Token: 0x170021BD RID: 8637
		// (get) Token: 0x0600666D RID: 26221 RVA: 0x0017FB25 File Offset: 0x0017DD25
		public CellCollection Cells
		{
			get
			{
				return this.Table.Cells.GetCellsByRowIndex(this.Index);
			}
		}

		// Token: 0x170021BE RID: 8638
		// (get) Token: 0x0600666E RID: 26222 RVA: 0x0017FB3D File Offset: 0x0017DD3D
		// (set) Token: 0x0600666F RID: 26223 RVA: 0x0017FB45 File Offset: 0x0017DD45
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

		// Token: 0x170021BF RID: 8639
		// (get) Token: 0x06006670 RID: 26224 RVA: 0x0017FB4E File Offset: 0x0017DD4E
		internal int FirstColumnIndex
		{
			get
			{
				if (this._firstColumnIndex == null)
				{
					this.PopulateMinMaxColumnIndices();
				}
				return this._firstColumnIndex.Value;
			}
		}

		// Token: 0x170021C0 RID: 8640
		// (get) Token: 0x06006671 RID: 26225 RVA: 0x0017FB6E File Offset: 0x0017DD6E
		internal int LastColumnIndex
		{
			get
			{
				if (this._lastColumnIndex == null)
				{
					this.PopulateMinMaxColumnIndices();
				}
				return this._lastColumnIndex.Value;
			}
		}

		// Token: 0x06006672 RID: 26226 RVA: 0x0017FB90 File Offset: 0x0017DD90
		private void PopulateMinMaxColumnIndices()
		{
			CellCollection cells = this.Cells;
			if (cells.Count == 0)
			{
				this._firstColumnIndex = (this._lastColumnIndex = new int?(-1));
				return;
			}
			foreach (Cell cell in this.Cells)
			{
				if (this._firstColumnIndex == null)
				{
					this._firstColumnIndex = new int?(cell.ColIndex);
				}
				if (this._lastColumnIndex == null)
				{
					this._lastColumnIndex = new int?(cell.ColIndex);
				}
				if (cell.ColIndex < this._firstColumnIndex)
				{
					this._firstColumnIndex = new int?(cell.ColIndex);
				}
				if (cell.ColIndex > this._lastColumnIndex)
				{
					this._lastColumnIndex = new int?(cell.ColIndex);
				}
			}
		}

		// Token: 0x040018D8 RID: 6360
		private ExportStyle _style;

		// Token: 0x040018D9 RID: 6361
		private int _index;

		// Token: 0x040018DA RID: 6362
		private double _height;

		// Token: 0x040018DB RID: 6363
		private Table _table;

		// Token: 0x040018DC RID: 6364
		private int? _firstColumnIndex = null;

		// Token: 0x040018DD RID: 6365
		private int? _lastColumnIndex = null;
	}
}
