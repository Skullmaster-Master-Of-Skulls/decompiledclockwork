using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x02000188 RID: 392
	internal abstract class DataGridRow : MarshalByRefObject
	{
		// Token: 0x06001734 RID: 5940 RVA: 0x000540A0 File Offset: 0x000522A0
		public DataGridRow(DataGrid dataGrid, DataGridTableStyle dgTable, int rowNumber)
		{
			if (dataGrid == null || dgTable.DataGrid == null)
			{
				throw new ArgumentNullException("dataGrid");
			}
			if (rowNumber < 0)
			{
				throw new ArgumentException(SR.GetString("DataGridRowRowNumber"), "rowNumber");
			}
			this.number = rowNumber;
			DataGridRow.colorMap[0].OldColor = Color.Black;
			DataGridRow.colorMap[0].NewColor = dgTable.HeaderForeColor;
			this.dgTable = dgTable;
			this.height = this.MinimumRowHeight(dgTable);
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001735 RID: 5941 RVA: 0x00054137 File Offset: 0x00052337
		public AccessibleObject AccessibleObject
		{
			get
			{
				if (this.accessibleObject == null)
				{
					this.accessibleObject = this.CreateAccessibleObject();
				}
				return this.accessibleObject;
			}
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x00054153 File Offset: 0x00052353
		protected virtual AccessibleObject CreateAccessibleObject()
		{
			return new DataGridRow.DataGridRowAccessibleObject(this);
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x0005415B File Offset: 0x0005235B
		protected internal virtual int MinimumRowHeight(DataGridTableStyle dgTable)
		{
			return this.MinimumRowHeight(dgTable.GridColumnStyles);
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x0005416C File Offset: 0x0005236C
		protected internal virtual int MinimumRowHeight(GridColumnStylesCollection columns)
		{
			int num = this.dgTable.IsDefault ? this.DataGrid.PreferredRowHeight : this.dgTable.PreferredRowHeight;
			try
			{
				if (this.dgTable.DataGrid.DataSource != null)
				{
					int count = columns.Count;
					for (int i = 0; i < count; i++)
					{
						if (columns[i].PropertyDescriptor != null)
						{
							num = Math.Max(num, columns[i].GetMinimumHeight());
						}
					}
				}
			}
			catch
			{
			}
			return num;
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001739 RID: 5945 RVA: 0x000541FC File Offset: 0x000523FC
		public DataGrid DataGrid
		{
			get
			{
				return this.dgTable.DataGrid;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x0600173A RID: 5946 RVA: 0x00054209 File Offset: 0x00052409
		// (set) Token: 0x0600173B RID: 5947 RVA: 0x00054211 File Offset: 0x00052411
		internal DataGridTableStyle DataGridTableStyle
		{
			get
			{
				return this.dgTable;
			}
			set
			{
				this.dgTable = value;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x0600173C RID: 5948 RVA: 0x0005421A File Offset: 0x0005241A
		// (set) Token: 0x0600173D RID: 5949 RVA: 0x00054222 File Offset: 0x00052422
		public virtual int Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = Math.Max(0, value);
				this.dgTable.DataGrid.OnRowHeightChanged(this);
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x0600173E RID: 5950 RVA: 0x00054242 File Offset: 0x00052442
		public int RowNumber
		{
			get
			{
				return this.number;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x0600173F RID: 5951 RVA: 0x0005424A File Offset: 0x0005244A
		// (set) Token: 0x06001740 RID: 5952 RVA: 0x00054252 File Offset: 0x00052452
		public virtual bool Selected
		{
			get
			{
				return this.selected;
			}
			set
			{
				this.selected = value;
				this.InvalidateRow();
			}
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x00054264 File Offset: 0x00052464
		protected Bitmap GetBitmap(string bitmapName)
		{
			Bitmap bitmap = null;
			try
			{
				bitmap = new Bitmap(typeof(DataGridCaption), bitmapName);
				bitmap.MakeTransparent();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return bitmap;
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x000542A0 File Offset: 0x000524A0
		public virtual Rectangle GetCellBounds(int col)
		{
			int firstVisibleColumn = this.dgTable.DataGrid.FirstVisibleColumn;
			int num = 0;
			Rectangle result = default(Rectangle);
			GridColumnStylesCollection gridColumnStyles = this.dgTable.GridColumnStyles;
			if (gridColumnStyles != null)
			{
				for (int i = firstVisibleColumn; i < col; i++)
				{
					if (gridColumnStyles[i].PropertyDescriptor != null)
					{
						num += gridColumnStyles[i].Width;
					}
				}
				int gridLineWidth = this.dgTable.GridLineWidth;
				result = new Rectangle(num, 0, gridColumnStyles[col].Width - gridLineWidth, this.Height - gridLineWidth);
			}
			return result;
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x00054335 File Offset: 0x00052535
		public virtual Rectangle GetNonScrollableArea()
		{
			return Rectangle.Empty;
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x0005433C File Offset: 0x0005253C
		protected Bitmap GetStarBitmap()
		{
			if (DataGridRow.starBmp == null)
			{
				DataGridRow.starBmp = this.GetBitmap("DataGridRow.star.bmp");
			}
			return DataGridRow.starBmp;
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x0005435A File Offset: 0x0005255A
		protected Bitmap GetPencilBitmap()
		{
			if (DataGridRow.pencilBmp == null)
			{
				DataGridRow.pencilBmp = this.GetBitmap("DataGridRow.pencil.bmp");
			}
			return DataGridRow.pencilBmp;
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x00054378 File Offset: 0x00052578
		protected Bitmap GetErrorBitmap()
		{
			if (DataGridRow.errorBmp == null)
			{
				DataGridRow.errorBmp = this.GetBitmap("DataGridRow.error.bmp");
			}
			DataGridRow.errorBmp.MakeTransparent();
			return DataGridRow.errorBmp;
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x000543A0 File Offset: 0x000525A0
		protected Bitmap GetLeftArrowBitmap()
		{
			if (DataGridRow.leftArrow == null)
			{
				DataGridRow.leftArrow = this.GetBitmap("DataGridRow.left.bmp");
			}
			return DataGridRow.leftArrow;
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x000543BE File Offset: 0x000525BE
		protected Bitmap GetRightArrowBitmap()
		{
			if (DataGridRow.rightArrow == null)
			{
				DataGridRow.rightArrow = this.GetBitmap("DataGridRow.right.bmp");
			}
			return DataGridRow.rightArrow;
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x000543DC File Offset: 0x000525DC
		public virtual void InvalidateRow()
		{
			this.dgTable.DataGrid.InvalidateRow(this.number);
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x000543F4 File Offset: 0x000525F4
		public virtual void InvalidateRowRect(Rectangle r)
		{
			this.dgTable.DataGrid.InvalidateRowRect(this.number, r);
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x000072B6 File Offset: 0x000054B6
		public virtual void OnEdit()
		{
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x00054410 File Offset: 0x00052610
		public virtual bool OnKeyPress(Keys keyData)
		{
			int columnNumber = this.dgTable.DataGrid.CurrentCell.ColumnNumber;
			GridColumnStylesCollection gridColumnStyles = this.dgTable.GridColumnStyles;
			if (gridColumnStyles != null && columnNumber >= 0 && columnNumber < gridColumnStyles.Count)
			{
				DataGridColumnStyle dataGridColumnStyle = gridColumnStyles[columnNumber];
				if (dataGridColumnStyle.KeyPress(this.RowNumber, keyData))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x0005446C File Offset: 0x0005266C
		public virtual bool OnMouseDown(int x, int y, Rectangle rowHeaders)
		{
			return this.OnMouseDown(x, y, rowHeaders, false);
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x00054478 File Offset: 0x00052678
		public virtual bool OnMouseDown(int x, int y, Rectangle rowHeaders, bool alignToRight)
		{
			this.LoseChildFocus(rowHeaders, alignToRight);
			return false;
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x00011A20 File Offset: 0x0000FC20
		public virtual bool OnMouseMove(int x, int y, Rectangle rowHeaders)
		{
			return false;
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x00011A20 File Offset: 0x0000FC20
		public virtual bool OnMouseMove(int x, int y, Rectangle rowHeaders, bool alignToRight)
		{
			return false;
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x000072B6 File Offset: 0x000054B6
		public virtual void OnMouseLeft(Rectangle rowHeaders, bool alignToRight)
		{
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x000072B6 File Offset: 0x000054B6
		public virtual void OnMouseLeft()
		{
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x000072B6 File Offset: 0x000054B6
		public virtual void OnRowEnter()
		{
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x000072B6 File Offset: 0x000054B6
		public virtual void OnRowLeave()
		{
		}

		// Token: 0x06001755 RID: 5973
		internal abstract bool ProcessTabKey(Keys keyData, Rectangle rowHeaders, bool alignToRight);

		// Token: 0x06001756 RID: 5974
		internal abstract void LoseChildFocus(Rectangle rowHeaders, bool alignToRight);

		// Token: 0x06001757 RID: 5975
		public abstract int Paint(Graphics g, Rectangle dataBounds, Rectangle rowBounds, int firstVisibleColumn, int numVisibleColumns);

		// Token: 0x06001758 RID: 5976
		public abstract int Paint(Graphics g, Rectangle dataBounds, Rectangle rowBounds, int firstVisibleColumn, int numVisibleColumns, bool alignToRight);

		// Token: 0x06001759 RID: 5977 RVA: 0x00054484 File Offset: 0x00052684
		protected virtual void PaintBottomBorder(Graphics g, Rectangle bounds, int dataWidth)
		{
			this.PaintBottomBorder(g, bounds, dataWidth, this.dgTable.GridLineWidth, false);
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x0005449C File Offset: 0x0005269C
		protected virtual void PaintBottomBorder(Graphics g, Rectangle bounds, int dataWidth, int borderWidth, bool alignToRight)
		{
			Rectangle rect = new Rectangle(alignToRight ? (bounds.Right - dataWidth) : bounds.X, bounds.Bottom - borderWidth, dataWidth, borderWidth);
			g.FillRectangle(this.dgTable.IsDefault ? this.DataGrid.GridLineBrush : this.dgTable.GridLineBrush, rect);
			if (dataWidth < bounds.Width)
			{
				g.FillRectangle(this.dgTable.DataGrid.BackgroundBrush, alignToRight ? bounds.X : rect.Right, rect.Y, bounds.Width - rect.Width, borderWidth);
			}
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x0005454B File Offset: 0x0005274B
		public virtual int PaintData(Graphics g, Rectangle bounds, int firstVisibleColumn, int columnCount)
		{
			return this.PaintData(g, bounds, firstVisibleColumn, columnCount, false);
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x0005455C File Offset: 0x0005275C
		public virtual int PaintData(Graphics g, Rectangle bounds, int firstVisibleColumn, int columnCount, bool alignToRight)
		{
			Rectangle cellBounds = bounds;
			int num = this.dgTable.IsDefault ? this.DataGrid.GridLineWidth : this.dgTable.GridLineWidth;
			int num2 = 0;
			DataGridCell currentCell = this.dgTable.DataGrid.CurrentCell;
			GridColumnStylesCollection gridColumnStyles = this.dgTable.GridColumnStyles;
			int count = gridColumnStyles.Count;
			int num3 = firstVisibleColumn;
			while (num3 < count && num2 <= bounds.Width)
			{
				if (gridColumnStyles[num3].PropertyDescriptor != null && gridColumnStyles[num3].Width > 0)
				{
					cellBounds.Width = gridColumnStyles[num3].Width - num;
					if (alignToRight)
					{
						cellBounds.X = bounds.Right - num2 - cellBounds.Width;
					}
					else
					{
						cellBounds.X = bounds.X + num2;
					}
					Brush backBr = this.BackBrushForDataPaint(ref currentCell, gridColumnStyles[num3], num3);
					Brush foreBrush = this.ForeBrushForDataPaint(ref currentCell, gridColumnStyles[num3], num3);
					this.PaintCellContents(g, cellBounds, gridColumnStyles[num3], backBr, foreBrush, alignToRight);
					if (num > 0)
					{
						g.FillRectangle(this.dgTable.IsDefault ? this.DataGrid.GridLineBrush : this.dgTable.GridLineBrush, alignToRight ? (cellBounds.X - num) : cellBounds.Right, cellBounds.Y, num, cellBounds.Height);
					}
					num2 += cellBounds.Width + num;
				}
				num3++;
			}
			if (num2 < bounds.Width)
			{
				g.FillRectangle(this.dgTable.DataGrid.BackgroundBrush, alignToRight ? bounds.X : (bounds.X + num2), bounds.Y, bounds.Width - num2, bounds.Height);
			}
			return num2;
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x00054739 File Offset: 0x00052939
		protected virtual void PaintCellContents(Graphics g, Rectangle cellBounds, DataGridColumnStyle column, Brush backBr, Brush foreBrush)
		{
			this.PaintCellContents(g, cellBounds, column, backBr, foreBrush, false);
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x00054749 File Offset: 0x00052949
		protected virtual void PaintCellContents(Graphics g, Rectangle cellBounds, DataGridColumnStyle column, Brush backBr, Brush foreBrush, bool alignToRight)
		{
			g.FillRectangle(backBr, cellBounds);
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x00054754 File Offset: 0x00052954
		protected Rectangle PaintIcon(Graphics g, Rectangle visualBounds, bool paintIcon, bool alignToRight, Bitmap bmp)
		{
			return this.PaintIcon(g, visualBounds, paintIcon, alignToRight, bmp, this.dgTable.IsDefault ? this.DataGrid.HeaderBackBrush : this.dgTable.HeaderBackBrush);
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x00054788 File Offset: 0x00052988
		protected Rectangle PaintIcon(Graphics g, Rectangle visualBounds, bool paintIcon, bool alignToRight, Bitmap bmp, Brush backBrush)
		{
			Size size = bmp.Size;
			Rectangle rectangle = new Rectangle(alignToRight ? (visualBounds.Right - 3 - size.Width) : (visualBounds.X + 3), visualBounds.Y + 2, size.Width, size.Height);
			g.FillRectangle(backBrush, visualBounds);
			if (paintIcon)
			{
				DataGridRow.colorMap[0].NewColor = (this.dgTable.IsDefault ? this.DataGrid.HeaderForeColor : this.dgTable.HeaderForeColor);
				DataGridRow.colorMap[0].OldColor = Color.Black;
				ImageAttributes imageAttributes = new ImageAttributes();
				imageAttributes.SetRemapTable(DataGridRow.colorMap, ColorAdjustType.Bitmap);
				g.DrawImage(bmp, rectangle, 0, 0, rectangle.Width, rectangle.Height, GraphicsUnit.Pixel, imageAttributes);
				imageAttributes.Dispose();
			}
			return rectangle;
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x0005485D File Offset: 0x00052A5D
		public virtual void PaintHeader(Graphics g, Rectangle visualBounds)
		{
			this.PaintHeader(g, visualBounds, false);
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x00054868 File Offset: 0x00052A68
		public virtual void PaintHeader(Graphics g, Rectangle visualBounds, bool alignToRight)
		{
			this.PaintHeader(g, visualBounds, alignToRight, false);
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x00054874 File Offset: 0x00052A74
		public virtual void PaintHeader(Graphics g, Rectangle visualBounds, bool alignToRight, bool rowIsDirty)
		{
			Rectangle visualBounds2 = visualBounds;
			Bitmap bitmap;
			if (this is DataGridAddNewRow)
			{
				bitmap = this.GetStarBitmap();
				Bitmap obj = bitmap;
				lock (obj)
				{
					visualBounds2.X += this.PaintIcon(g, visualBounds2, true, alignToRight, bitmap).Width + 3;
				}
				return;
			}
			if (rowIsDirty)
			{
				bitmap = this.GetPencilBitmap();
				Bitmap obj2 = bitmap;
				lock (obj2)
				{
					visualBounds2.X += this.PaintIcon(g, visualBounds2, this.RowNumber == this.DataGrid.CurrentCell.RowNumber, alignToRight, bitmap).Width + 3;
					goto IL_128;
				}
			}
			bitmap = (alignToRight ? this.GetLeftArrowBitmap() : this.GetRightArrowBitmap());
			Bitmap obj3 = bitmap;
			lock (obj3)
			{
				visualBounds2.X += this.PaintIcon(g, visualBounds2, this.RowNumber == this.DataGrid.CurrentCell.RowNumber, alignToRight, bitmap).Width + 3;
			}
			IL_128:
			object obj4 = this.DataGrid.ListManager[this.number];
			if (!(obj4 is IDataErrorInfo))
			{
				return;
			}
			string text = ((IDataErrorInfo)obj4).Error;
			if (text == null)
			{
				text = string.Empty;
			}
			if (this.tooltip != text && !string.IsNullOrEmpty(this.tooltip))
			{
				this.DataGrid.ToolTipProvider.RemoveToolTip(this.tooltipID);
				this.tooltip = string.Empty;
				this.tooltipID = new IntPtr(-1);
			}
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			bitmap = this.GetErrorBitmap();
			Bitmap obj5 = bitmap;
			Rectangle iconBounds;
			lock (obj5)
			{
				iconBounds = this.PaintIcon(g, visualBounds2, true, alignToRight, bitmap);
			}
			visualBounds2.X += iconBounds.Width + 3;
			this.tooltip = text;
			DataGrid dataGrid = this.DataGrid;
			int toolTipId = dataGrid.ToolTipId;
			dataGrid.ToolTipId = toolTipId + 1;
			this.tooltipID = (IntPtr)toolTipId;
			this.DataGrid.ToolTipProvider.AddToolTip(this.tooltip, this.tooltipID, iconBounds);
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x00054AF4 File Offset: 0x00052CF4
		protected Brush GetBackBrush()
		{
			Brush result = this.dgTable.IsDefault ? this.DataGrid.BackBrush : this.dgTable.BackBrush;
			if (this.DataGrid.LedgerStyle && this.RowNumber % 2 == 1)
			{
				result = (this.dgTable.IsDefault ? this.DataGrid.AlternatingBackBrush : this.dgTable.AlternatingBackBrush);
			}
			return result;
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x00054B68 File Offset: 0x00052D68
		protected Brush BackBrushForDataPaint(ref DataGridCell current, DataGridColumnStyle gridColumn, int column)
		{
			Brush result = this.GetBackBrush();
			if (this.Selected)
			{
				result = (this.dgTable.IsDefault ? this.DataGrid.SelectionBackBrush : this.dgTable.SelectionBackBrush);
			}
			return result;
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x00054BAC File Offset: 0x00052DAC
		protected Brush ForeBrushForDataPaint(ref DataGridCell current, DataGridColumnStyle gridColumn, int column)
		{
			Brush result = this.dgTable.IsDefault ? this.DataGrid.ForeBrush : this.dgTable.ForeBrush;
			if (this.Selected)
			{
				result = (this.dgTable.IsDefault ? this.DataGrid.SelectionForeBrush : this.dgTable.SelectionForeBrush);
			}
			return result;
		}

		// Token: 0x04000A7C RID: 2684
		protected internal int number;

		// Token: 0x04000A7D RID: 2685
		private bool selected;

		// Token: 0x04000A7E RID: 2686
		private int height;

		// Token: 0x04000A7F RID: 2687
		private IntPtr tooltipID = new IntPtr(-1);

		// Token: 0x04000A80 RID: 2688
		private string tooltip = string.Empty;

		// Token: 0x04000A81 RID: 2689
		private AccessibleObject accessibleObject;

		// Token: 0x04000A82 RID: 2690
		protected DataGridTableStyle dgTable;

		// Token: 0x04000A83 RID: 2691
		private static ColorMap[] colorMap = new ColorMap[]
		{
			new ColorMap()
		};

		// Token: 0x04000A84 RID: 2692
		private static Bitmap rightArrow = null;

		// Token: 0x04000A85 RID: 2693
		private static Bitmap leftArrow = null;

		// Token: 0x04000A86 RID: 2694
		private static Bitmap errorBmp = null;

		// Token: 0x04000A87 RID: 2695
		private static Bitmap pencilBmp = null;

		// Token: 0x04000A88 RID: 2696
		private static Bitmap starBmp = null;

		// Token: 0x04000A89 RID: 2697
		protected const int xOffset = 3;

		// Token: 0x04000A8A RID: 2698
		protected const int yOffset = 2;

		// Token: 0x02000653 RID: 1619
		[ComVisible(true)]
		protected class DataGridRowAccessibleObject : AccessibleObject
		{
			// Token: 0x06006518 RID: 25880 RVA: 0x00178548 File Offset: 0x00176748
			internal static string CellToDisplayString(DataGrid grid, int row, int column)
			{
				if (column < grid.myGridTable.GridColumnStyles.Count)
				{
					return grid.myGridTable.GridColumnStyles[column].PropertyDescriptor.Converter.ConvertToString(grid[row, column]);
				}
				return "";
			}

			// Token: 0x06006519 RID: 25881 RVA: 0x00178596 File Offset: 0x00176796
			internal static object DisplayStringToCell(DataGrid grid, int row, int column, string value)
			{
				if (column < grid.myGridTable.GridColumnStyles.Count)
				{
					return grid.myGridTable.GridColumnStyles[column].PropertyDescriptor.Converter.ConvertFromString(value);
				}
				return null;
			}

			// Token: 0x0600651A RID: 25882 RVA: 0x001785D0 File Offset: 0x001767D0
			public DataGridRowAccessibleObject(DataGridRow owner)
			{
				this.owner = owner;
				DataGrid dataGrid = this.DataGrid;
				this.EnsureChildren();
			}

			// Token: 0x0600651B RID: 25883 RVA: 0x001785F7 File Offset: 0x001767F7
			private void EnsureChildren()
			{
				if (this.cells == null)
				{
					this.cells = new ArrayList(this.DataGrid.myGridTable.GridColumnStyles.Count + 2);
					this.AddChildAccessibleObjects(this.cells);
				}
			}

			// Token: 0x0600651C RID: 25884 RVA: 0x00178630 File Offset: 0x00176830
			protected virtual void AddChildAccessibleObjects(IList children)
			{
				GridColumnStylesCollection gridColumnStyles = this.DataGrid.myGridTable.GridColumnStyles;
				int count = gridColumnStyles.Count;
				for (int i = 0; i < count; i++)
				{
					children.Add(this.CreateCellAccessibleObject(i));
				}
			}

			// Token: 0x0600651D RID: 25885 RVA: 0x0017866F File Offset: 0x0017686F
			protected virtual AccessibleObject CreateCellAccessibleObject(int column)
			{
				return new DataGridRow.DataGridCellAccessibleObject(this.owner, column);
			}

			// Token: 0x170015BD RID: 5565
			// (get) Token: 0x0600651E RID: 25886 RVA: 0x0017867D File Offset: 0x0017687D
			public override Rectangle Bounds
			{
				get
				{
					return this.DataGrid.RectangleToScreen(this.DataGrid.GetRowBounds(this.owner));
				}
			}

			// Token: 0x170015BE RID: 5566
			// (get) Token: 0x0600651F RID: 25887 RVA: 0x0017869B File Offset: 0x0017689B
			public override string Name
			{
				get
				{
					if (this.owner is DataGridAddNewRow)
					{
						return SR.GetString("AccDGNewRow");
					}
					return DataGridRow.DataGridRowAccessibleObject.CellToDisplayString(this.DataGrid, this.owner.RowNumber, 0);
				}
			}

			// Token: 0x170015BF RID: 5567
			// (get) Token: 0x06006520 RID: 25888 RVA: 0x001786CC File Offset: 0x001768CC
			protected DataGridRow Owner
			{
				get
				{
					return this.owner;
				}
			}

			// Token: 0x170015C0 RID: 5568
			// (get) Token: 0x06006521 RID: 25889 RVA: 0x001786D4 File Offset: 0x001768D4
			public override AccessibleObject Parent
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.DataGrid.AccessibilityObject;
				}
			}

			// Token: 0x170015C1 RID: 5569
			// (get) Token: 0x06006522 RID: 25890 RVA: 0x001786E1 File Offset: 0x001768E1
			private DataGrid DataGrid
			{
				get
				{
					return this.owner.DataGrid;
				}
			}

			// Token: 0x170015C2 RID: 5570
			// (get) Token: 0x06006523 RID: 25891 RVA: 0x001786EE File Offset: 0x001768EE
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.Row;
				}
			}

			// Token: 0x170015C3 RID: 5571
			// (get) Token: 0x06006524 RID: 25892 RVA: 0x001786F4 File Offset: 0x001768F4
			public override AccessibleStates State
			{
				get
				{
					AccessibleStates accessibleStates = AccessibleStates.Focusable | AccessibleStates.Selectable;
					if (this.DataGrid.CurrentCell.RowNumber == this.owner.RowNumber)
					{
						accessibleStates |= AccessibleStates.Focused;
					}
					if (this.DataGrid.CurrentRowIndex == this.owner.RowNumber)
					{
						accessibleStates |= AccessibleStates.Selected;
					}
					return accessibleStates;
				}
			}

			// Token: 0x170015C4 RID: 5572
			// (get) Token: 0x06006525 RID: 25893 RVA: 0x000163B4 File Offset: 0x000145B4
			public override string Value
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.Name;
				}
			}

			// Token: 0x06006526 RID: 25894 RVA: 0x00178748 File Offset: 0x00176948
			public override AccessibleObject GetChild(int index)
			{
				if (index < this.cells.Count)
				{
					return (AccessibleObject)this.cells[index];
				}
				return null;
			}

			// Token: 0x06006527 RID: 25895 RVA: 0x0017876B File Offset: 0x0017696B
			public override int GetChildCount()
			{
				return this.cells.Count;
			}

			// Token: 0x06006528 RID: 25896 RVA: 0x00178778 File Offset: 0x00176978
			public override AccessibleObject GetFocused()
			{
				if (this.DataGrid.Focused)
				{
					DataGridCell currentCell = this.DataGrid.CurrentCell;
					if (currentCell.RowNumber == this.owner.RowNumber)
					{
						return (AccessibleObject)this.cells[currentCell.ColumnNumber];
					}
				}
				return null;
			}

			// Token: 0x06006529 RID: 25897 RVA: 0x001787CC File Offset: 0x001769CC
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override AccessibleObject Navigate(AccessibleNavigation navdir)
			{
				switch (navdir)
				{
				case AccessibleNavigation.Up:
				case AccessibleNavigation.Left:
				case AccessibleNavigation.Previous:
					return this.DataGrid.AccessibilityObject.GetChild(1 + this.owner.dgTable.GridColumnStyles.Count + this.owner.RowNumber - 1);
				case AccessibleNavigation.Down:
				case AccessibleNavigation.Right:
				case AccessibleNavigation.Next:
					return this.DataGrid.AccessibilityObject.GetChild(1 + this.owner.dgTable.GridColumnStyles.Count + this.owner.RowNumber + 1);
				case AccessibleNavigation.FirstChild:
					if (this.GetChildCount() > 0)
					{
						return this.GetChild(0);
					}
					break;
				case AccessibleNavigation.LastChild:
					if (this.GetChildCount() > 0)
					{
						return this.GetChild(this.GetChildCount() - 1);
					}
					break;
				}
				return null;
			}

			// Token: 0x0600652A RID: 25898 RVA: 0x0017889C File Offset: 0x00176A9C
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void Select(AccessibleSelection flags)
			{
				if ((flags & AccessibleSelection.TakeFocus) == AccessibleSelection.TakeFocus)
				{
					this.DataGrid.Focus();
				}
				if ((flags & AccessibleSelection.TakeSelection) == AccessibleSelection.TakeSelection)
				{
					this.DataGrid.CurrentRowIndex = this.owner.RowNumber;
				}
			}

			// Token: 0x040039E3 RID: 14819
			private ArrayList cells;

			// Token: 0x040039E4 RID: 14820
			private DataGridRow owner;
		}

		// Token: 0x02000654 RID: 1620
		[ComVisible(true)]
		protected class DataGridCellAccessibleObject : AccessibleObject
		{
			// Token: 0x0600652B RID: 25899 RVA: 0x001788CC File Offset: 0x00176ACC
			public DataGridCellAccessibleObject(DataGridRow owner, int column)
			{
				this.owner = owner;
				this.column = column;
			}

			// Token: 0x170015C5 RID: 5573
			// (get) Token: 0x0600652C RID: 25900 RVA: 0x001788E2 File Offset: 0x00176AE2
			public override Rectangle Bounds
			{
				get
				{
					return this.DataGrid.RectangleToScreen(this.DataGrid.GetCellBounds(new DataGridCell(this.owner.RowNumber, this.column)));
				}
			}

			// Token: 0x170015C6 RID: 5574
			// (get) Token: 0x0600652D RID: 25901 RVA: 0x00178910 File Offset: 0x00176B10
			public override string Name
			{
				get
				{
					return this.DataGrid.myGridTable.GridColumnStyles[this.column].HeaderText;
				}
			}

			// Token: 0x170015C7 RID: 5575
			// (get) Token: 0x0600652E RID: 25902 RVA: 0x00178932 File Offset: 0x00176B32
			public override AccessibleObject Parent
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.owner.AccessibleObject;
				}
			}

			// Token: 0x170015C8 RID: 5576
			// (get) Token: 0x0600652F RID: 25903 RVA: 0x0017893F File Offset: 0x00176B3F
			protected DataGrid DataGrid
			{
				get
				{
					return this.owner.DataGrid;
				}
			}

			// Token: 0x170015C9 RID: 5577
			// (get) Token: 0x06006530 RID: 25904 RVA: 0x0017894C File Offset: 0x00176B4C
			public override string DefaultAction
			{
				get
				{
					return SR.GetString("AccDGEdit");
				}
			}

			// Token: 0x170015CA RID: 5578
			// (get) Token: 0x06006531 RID: 25905 RVA: 0x00178958 File Offset: 0x00176B58
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.Cell;
				}
			}

			// Token: 0x170015CB RID: 5579
			// (get) Token: 0x06006532 RID: 25906 RVA: 0x0017895C File Offset: 0x00176B5C
			public override AccessibleStates State
			{
				get
				{
					AccessibleStates accessibleStates = AccessibleStates.Focusable | AccessibleStates.Selectable;
					if (this.DataGrid.CurrentCell.RowNumber == this.owner.RowNumber && this.DataGrid.CurrentCell.ColumnNumber == this.column)
					{
						if (this.DataGrid.Focused)
						{
							accessibleStates |= AccessibleStates.Focused;
						}
						accessibleStates |= AccessibleStates.Selected;
					}
					return accessibleStates;
				}
			}

			// Token: 0x170015CC RID: 5580
			// (get) Token: 0x06006533 RID: 25907 RVA: 0x001789C0 File Offset: 0x00176BC0
			// (set) Token: 0x06006534 RID: 25908 RVA: 0x001789F0 File Offset: 0x00176BF0
			public override string Value
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					if (this.owner is DataGridAddNewRow)
					{
						return null;
					}
					return DataGridRow.DataGridRowAccessibleObject.CellToDisplayString(this.DataGrid, this.owner.RowNumber, this.column);
				}
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				set
				{
					if (!(this.owner is DataGridAddNewRow))
					{
						object value2 = DataGridRow.DataGridRowAccessibleObject.DisplayStringToCell(this.DataGrid, this.owner.RowNumber, this.column, value);
						this.DataGrid[this.owner.RowNumber, this.column] = value2;
					}
				}
			}

			// Token: 0x06006535 RID: 25909 RVA: 0x00178A45 File Offset: 0x00176C45
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				this.Select(AccessibleSelection.TakeFocus | AccessibleSelection.TakeSelection);
			}

			// Token: 0x06006536 RID: 25910 RVA: 0x00178A4E File Offset: 0x00176C4E
			public override AccessibleObject GetFocused()
			{
				return this.DataGrid.AccessibilityObject.GetFocused();
			}

			// Token: 0x06006537 RID: 25911 RVA: 0x00178A60 File Offset: 0x00176C60
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override AccessibleObject Navigate(AccessibleNavigation navdir)
			{
				switch (navdir)
				{
				case AccessibleNavigation.Up:
					return this.DataGrid.AccessibilityObject.GetChild(1 + this.owner.dgTable.GridColumnStyles.Count + this.owner.RowNumber - 1).Navigate(AccessibleNavigation.FirstChild);
				case AccessibleNavigation.Down:
					return this.DataGrid.AccessibilityObject.GetChild(1 + this.owner.dgTable.GridColumnStyles.Count + this.owner.RowNumber + 1).Navigate(AccessibleNavigation.FirstChild);
				case AccessibleNavigation.Left:
				case AccessibleNavigation.Previous:
				{
					if (this.column > 0)
					{
						return this.owner.AccessibleObject.GetChild(this.column - 1);
					}
					AccessibleObject child = this.DataGrid.AccessibilityObject.GetChild(1 + this.owner.dgTable.GridColumnStyles.Count + this.owner.RowNumber - 1);
					if (child != null)
					{
						return child.Navigate(AccessibleNavigation.LastChild);
					}
					break;
				}
				case AccessibleNavigation.Right:
				case AccessibleNavigation.Next:
				{
					if (this.column < this.owner.AccessibleObject.GetChildCount() - 1)
					{
						return this.owner.AccessibleObject.GetChild(this.column + 1);
					}
					AccessibleObject child2 = this.DataGrid.AccessibilityObject.GetChild(1 + this.owner.dgTable.GridColumnStyles.Count + this.owner.RowNumber + 1);
					if (child2 != null)
					{
						return child2.Navigate(AccessibleNavigation.FirstChild);
					}
					break;
				}
				}
				return null;
			}

			// Token: 0x06006538 RID: 25912 RVA: 0x00178BED File Offset: 0x00176DED
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void Select(AccessibleSelection flags)
			{
				if ((flags & AccessibleSelection.TakeFocus) == AccessibleSelection.TakeFocus)
				{
					this.DataGrid.Focus();
				}
				if ((flags & AccessibleSelection.TakeSelection) == AccessibleSelection.TakeSelection)
				{
					this.DataGrid.CurrentCell = new DataGridCell(this.owner.RowNumber, this.column);
				}
			}

			// Token: 0x040039E5 RID: 14821
			private DataGridRow owner;

			// Token: 0x040039E6 RID: 14822
			private int column;
		}
	}
}
