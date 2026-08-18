using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.ExportInfrastructure
{
	// Token: 0x02000A58 RID: 2648
	public class Table
	{
		// Token: 0x060066A3 RID: 26275 RVA: 0x00180814 File Offset: 0x0017EA14
		public Table()
		{
		}

		// Token: 0x060066A4 RID: 26276 RVA: 0x00180870 File Offset: 0x0017EA70
		public Table(string title)
		{
			this.Title = title;
		}

		// Token: 0x170021C8 RID: 8648
		// (get) Token: 0x060066A5 RID: 26277 RVA: 0x001808D3 File Offset: 0x0017EAD3
		// (set) Token: 0x060066A6 RID: 26278 RVA: 0x001808DB File Offset: 0x0017EADB
		public int Index
		{
			get
			{
				return this._index;
			}
			internal set
			{
				this._index = value;
			}
		}

		// Token: 0x170021C9 RID: 8649
		// (get) Token: 0x060066A7 RID: 26279 RVA: 0x001808E4 File Offset: 0x0017EAE4
		// (set) Token: 0x060066A8 RID: 26280 RVA: 0x001808EC File Offset: 0x0017EAEC
		public string Title
		{
			get
			{
				return this._title;
			}
			set
			{
				this._title = value;
			}
		}

		// Token: 0x170021CA RID: 8650
		// (get) Token: 0x060066A9 RID: 26281 RVA: 0x001808F5 File Offset: 0x0017EAF5
		// (set) Token: 0x060066AA RID: 26282 RVA: 0x001808FD File Offset: 0x0017EAFD
		internal int ImageCount
		{
			get
			{
				return this.imageCount;
			}
			set
			{
				this.imageCount = value;
			}
		}

		// Token: 0x170021CB RID: 8651
		// (get) Token: 0x060066AB RID: 26283 RVA: 0x00180906 File Offset: 0x0017EB06
		// (set) Token: 0x060066AC RID: 26284 RVA: 0x00180922 File Offset: 0x0017EB22
		public CellCollection Cells
		{
			get
			{
				if (this._cells == null)
				{
					this._cells = new CellCollection(this);
				}
				return this._cells;
			}
			internal set
			{
				this._cells = value;
			}
		}

		// Token: 0x170021CC RID: 8652
		// (get) Token: 0x060066AD RID: 26285 RVA: 0x0018092B File Offset: 0x0017EB2B
		// (set) Token: 0x060066AE RID: 26286 RVA: 0x00180947 File Offset: 0x0017EB47
		public RowCollection Rows
		{
			get
			{
				if (this._rows == null)
				{
					this._rows = new RowCollection(this);
				}
				return this._rows;
			}
			internal set
			{
				this._rows = value;
			}
		}

		// Token: 0x170021CD RID: 8653
		// (get) Token: 0x060066AF RID: 26287 RVA: 0x00180950 File Offset: 0x0017EB50
		// (set) Token: 0x060066B0 RID: 26288 RVA: 0x0018096C File Offset: 0x0017EB6C
		public ColumnCollection Columns
		{
			get
			{
				if (this._columns == null)
				{
					this._columns = new ColumnCollection(this);
				}
				return this._columns;
			}
			internal set
			{
				this._columns = value;
			}
		}

		// Token: 0x170021CE RID: 8654
		// (get) Token: 0x060066B1 RID: 26289 RVA: 0x00180975 File Offset: 0x0017EB75
		// (set) Token: 0x060066B2 RID: 26290 RVA: 0x00180990 File Offset: 0x0017EB90
		public ImageCollection Images
		{
			get
			{
				if (this._images == null)
				{
					this._images = new ImageCollection();
				}
				return this._images;
			}
			internal set
			{
				this._images = value;
			}
		}

		// Token: 0x170021CF RID: 8655
		// (get) Token: 0x060066B3 RID: 26291 RVA: 0x00180999 File Offset: 0x0017EB99
		// (set) Token: 0x060066B4 RID: 26292 RVA: 0x001809B4 File Offset: 0x0017EBB4
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

		// Token: 0x060066B5 RID: 26293 RVA: 0x001809C0 File Offset: 0x0017EBC0
		internal void InsertImage(Range cellIndex, string imageUrl, bool autoSize)
		{
			this.Images.Add(new Telerik.Web.UI.ExportInfrastructure.Image
			{
				ImageUrl = imageUrl,
				ImageRange = cellIndex,
				AutoSize = autoSize
			});
		}

		// Token: 0x060066B6 RID: 26294 RVA: 0x001809F4 File Offset: 0x0017EBF4
		public void InsertImage(Range cellIndex, string imageUrl)
		{
			this.Images.Add(new Telerik.Web.UI.ExportInfrastructure.Image
			{
				ImageUrl = imageUrl,
				ImageRange = cellIndex,
				AutoSize = false
			});
		}

		// Token: 0x060066B7 RID: 26295 RVA: 0x00180A28 File Offset: 0x0017EC28
		public void InsertImage(Cell cell, string imageUrl, bool autoSize)
		{
			Point point = new Point(cell.ColIndex, cell.RowIndex);
			this.InsertImage(new Range(point, point), imageUrl, autoSize);
		}

		// Token: 0x060066B8 RID: 26296 RVA: 0x00180A58 File Offset: 0x0017EC58
		public void InsertImage(Cell cell, string imageUrl)
		{
			Point point = new Point(cell.ColIndex, cell.RowIndex);
			this.InsertImage(new Range(point, point), imageUrl, false);
		}

		// Token: 0x060066B9 RID: 26297 RVA: 0x00180A88 File Offset: 0x0017EC88
		internal void InsertImage(Range cellIndex, byte[] imageData, bool autoSize)
		{
			this.Images.Add(new Telerik.Web.UI.ExportInfrastructure.Image
			{
				ImageData = imageData,
				ImageRange = cellIndex,
				AutoSize = autoSize
			});
		}

		// Token: 0x060066BA RID: 26298 RVA: 0x00180ABC File Offset: 0x0017ECBC
		public void InsertImage(Range cellIndex, byte[] imageData)
		{
			this.Images.Add(new Telerik.Web.UI.ExportInfrastructure.Image
			{
				ImageData = imageData,
				ImageRange = cellIndex,
				AutoSize = false
			});
		}

		// Token: 0x060066BB RID: 26299 RVA: 0x00180AF0 File Offset: 0x0017ECF0
		public void InsertImage(Cell cell, byte[] imageData, bool autoSize)
		{
			Point point = new Point(cell.ColIndex, cell.RowIndex);
			this.InsertImage(new Range(point, point), imageData, autoSize);
		}

		// Token: 0x060066BC RID: 26300 RVA: 0x00180B20 File Offset: 0x0017ED20
		public void InsertImage(Cell cell, byte[] imageData)
		{
			Point point = new Point(cell.ColIndex, cell.RowIndex);
			this.InsertImage(new Range(point, point), imageData, false);
		}

		// Token: 0x060066BD RID: 26301 RVA: 0x00180B6C File Offset: 0x0017ED6C
		public void ShiftRowsDown(int startRowIndex, int rowsCount)
		{
			if (startRowIndex == 0 || rowsCount == 0)
			{
				return;
			}
			List<Cell> list = new List<Cell>();
			foreach (Cell item in from x in this.Cells
			orderby x.Index.Y descending
			select x)
			{
				list.Add(item);
			}
			foreach (Cell cell in list)
			{
				if (cell.Index.Y >= startRowIndex)
				{
					this.Cells.ChangeCellIndex(cell, new Point(cell.Index.X, cell.Index.Y + rowsCount));
				}
			}
		}

		// Token: 0x170021D0 RID: 8656
		// (get) Token: 0x060066BE RID: 26302 RVA: 0x00180C68 File Offset: 0x0017EE68
		// (set) Token: 0x060066BF RID: 26303 RVA: 0x00180C70 File Offset: 0x0017EE70
		public Unit TopMargin
		{
			get
			{
				return this.topMargin;
			}
			set
			{
				this.topMargin = value;
			}
		}

		// Token: 0x170021D1 RID: 8657
		// (get) Token: 0x060066C0 RID: 26304 RVA: 0x00180C79 File Offset: 0x0017EE79
		// (set) Token: 0x060066C1 RID: 26305 RVA: 0x00180C81 File Offset: 0x0017EE81
		public Unit BottomMargin
		{
			get
			{
				return this.bottomMargin;
			}
			set
			{
				this.bottomMargin = value;
			}
		}

		// Token: 0x170021D2 RID: 8658
		// (get) Token: 0x060066C2 RID: 26306 RVA: 0x00180C8A File Offset: 0x0017EE8A
		// (set) Token: 0x060066C3 RID: 26307 RVA: 0x00180C92 File Offset: 0x0017EE92
		public Unit LeftMargin
		{
			get
			{
				return this.leftMargin;
			}
			set
			{
				this.leftMargin = value;
			}
		}

		// Token: 0x170021D3 RID: 8659
		// (get) Token: 0x060066C4 RID: 26308 RVA: 0x00180C9B File Offset: 0x0017EE9B
		// (set) Token: 0x060066C5 RID: 26309 RVA: 0x00180CA3 File Offset: 0x0017EEA3
		public Unit RightMargin
		{
			get
			{
				return this.rightMargin;
			}
			set
			{
				this.rightMargin = value;
			}
		}

		// Token: 0x170021D4 RID: 8660
		// (get) Token: 0x060066C6 RID: 26310 RVA: 0x00180CAC File Offset: 0x0017EEAC
		// (set) Token: 0x060066C7 RID: 26311 RVA: 0x00180CB4 File Offset: 0x0017EEB4
		public bool Landscape
		{
			get
			{
				return this.landscape;
			}
			set
			{
				this.landscape = value;
			}
		}

		// Token: 0x170021D5 RID: 8661
		// (get) Token: 0x060066C8 RID: 26312 RVA: 0x00180CBD File Offset: 0x0017EEBD
		// (set) Token: 0x060066C9 RID: 26313 RVA: 0x00180CC5 File Offset: 0x0017EEC5
		public string HeaderText
		{
			get
			{
				return this.headerText;
			}
			set
			{
				this.headerText = value;
			}
		}

		// Token: 0x170021D6 RID: 8662
		// (get) Token: 0x060066CA RID: 26314 RVA: 0x00180CCE File Offset: 0x0017EECE
		// (set) Token: 0x060066CB RID: 26315 RVA: 0x00180CD6 File Offset: 0x0017EED6
		public string FooterText
		{
			get
			{
				return this.footerText;
			}
			set
			{
				this.footerText = value;
			}
		}

		// Token: 0x170021D7 RID: 8663
		// (get) Token: 0x060066CC RID: 26316 RVA: 0x00180CDF File Offset: 0x0017EEDF
		// (set) Token: 0x060066CD RID: 26317 RVA: 0x00180CE7 File Offset: 0x0017EEE7
		public PaperKind PageSize
		{
			get
			{
				return this.pageSize;
			}
			set
			{
				this.pageSize = value;
			}
		}

		// Token: 0x170021D8 RID: 8664
		// (get) Token: 0x060066CE RID: 26318 RVA: 0x00180CF0 File Offset: 0x0017EEF0
		// (set) Token: 0x060066CF RID: 26319 RVA: 0x00180CF8 File Offset: 0x0017EEF8
		public bool ShowGridlines { get; set; }

		// Token: 0x040018F2 RID: 6386
		private string _title;

		// Token: 0x040018F3 RID: 6387
		private int _index;

		// Token: 0x040018F4 RID: 6388
		private CellCollection _cells;

		// Token: 0x040018F5 RID: 6389
		private ColumnCollection _columns;

		// Token: 0x040018F6 RID: 6390
		private RowCollection _rows;

		// Token: 0x040018F7 RID: 6391
		private ImageCollection _images;

		// Token: 0x040018F8 RID: 6392
		private ExportStyle _style;

		// Token: 0x040018F9 RID: 6393
		private int imageCount;

		// Token: 0x040018FA RID: 6394
		private string headerText = string.Empty;

		// Token: 0x040018FB RID: 6395
		private string footerText = string.Empty;

		// Token: 0x040018FC RID: 6396
		private Unit topMargin = Unit.Empty;

		// Token: 0x040018FD RID: 6397
		private Unit bottomMargin = Unit.Empty;

		// Token: 0x040018FE RID: 6398
		private Unit leftMargin = Unit.Empty;

		// Token: 0x040018FF RID: 6399
		private Unit rightMargin = Unit.Empty;

		// Token: 0x04001900 RID: 6400
		private bool landscape;

		// Token: 0x04001901 RID: 6401
		private PaperKind pageSize = PaperKind.Letter;
	}
}
