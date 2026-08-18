using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x02000185 RID: 389
	internal class DataGridParentRows
	{
		// Token: 0x060016D8 RID: 5848 RVA: 0x00051884 File Offset: 0x0004FA84
		internal DataGridParentRows(DataGrid dataGrid)
		{
			this.colorMap[0].OldColor = Color.Black;
			this.dataGrid = dataGrid;
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060016D9 RID: 5849 RVA: 0x0005191D File Offset: 0x0004FB1D
		public AccessibleObject AccessibleObject
		{
			get
			{
				if (this.accessibleObject == null)
				{
					this.accessibleObject = new DataGridParentRows.DataGridParentRowsAccessibleObject(this);
				}
				return this.accessibleObject;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060016DA RID: 5850 RVA: 0x00051939 File Offset: 0x0004FB39
		// (set) Token: 0x060016DB RID: 5851 RVA: 0x00051948 File Offset: 0x0004FB48
		internal Color BackColor
		{
			get
			{
				return this.backBrush.Color;
			}
			set
			{
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("DataGridEmptyColor", new object[]
					{
						"Parent Rows BackColor"
					}));
				}
				if (value != this.backBrush.Color)
				{
					this.backBrush = new SolidBrush(value);
					this.Invalidate();
				}
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060016DC RID: 5852 RVA: 0x000519A1 File Offset: 0x0004FBA1
		// (set) Token: 0x060016DD RID: 5853 RVA: 0x000519A9 File Offset: 0x0004FBA9
		internal SolidBrush BackBrush
		{
			get
			{
				return this.backBrush;
			}
			set
			{
				if (value != this.backBrush)
				{
					this.CheckNull(value, "BackBrush");
					this.backBrush = value;
					this.Invalidate();
				}
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060016DE RID: 5854 RVA: 0x000519CD File Offset: 0x0004FBCD
		// (set) Token: 0x060016DF RID: 5855 RVA: 0x000519D5 File Offset: 0x0004FBD5
		internal SolidBrush ForeBrush
		{
			get
			{
				return this.foreBrush;
			}
			set
			{
				if (value != this.foreBrush)
				{
					this.CheckNull(value, "BackBrush");
					this.foreBrush = value;
					this.Invalidate();
				}
			}
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x000519FC File Offset: 0x0004FBFC
		internal Rectangle GetBoundsForDataGridStateAccesibility(DataGridState dgs)
		{
			Rectangle empty = Rectangle.Empty;
			int num = 0;
			for (int i = 0; i < this.parentsCount; i++)
			{
				int num2 = (int)this.rowHeights[i];
				if (this.parents[i] == dgs)
				{
					empty.X = (this.layout.leftArrow.IsEmpty ? this.layout.data.X : this.layout.leftArrow.Right);
					empty.Height = num2;
					empty.Y = num;
					empty.Width = this.layout.data.Width;
					return empty;
				}
				num += num2;
			}
			return empty;
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060016E1 RID: 5857 RVA: 0x00051AB2 File Offset: 0x0004FCB2
		// (set) Token: 0x060016E2 RID: 5858 RVA: 0x00051ABA File Offset: 0x0004FCBA
		internal Brush BorderBrush
		{
			get
			{
				return this.borderBrush;
			}
			set
			{
				if (value != this.borderBrush)
				{
					this.borderBrush = value;
					this.Invalidate();
				}
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060016E3 RID: 5859 RVA: 0x00051AD2 File Offset: 0x0004FCD2
		internal int Height
		{
			get
			{
				return this.totalHeight;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060016E4 RID: 5860 RVA: 0x00051ADA File Offset: 0x0004FCDA
		// (set) Token: 0x060016E5 RID: 5861 RVA: 0x00051AE8 File Offset: 0x0004FCE8
		internal Color ForeColor
		{
			get
			{
				return this.foreBrush.Color;
			}
			set
			{
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("DataGridEmptyColor", new object[]
					{
						"Parent Rows ForeColor"
					}));
				}
				if (value != this.foreBrush.Color)
				{
					this.foreBrush = new SolidBrush(value);
					this.Invalidate();
				}
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060016E6 RID: 5862 RVA: 0x00051B41 File Offset: 0x0004FD41
		// (set) Token: 0x060016E7 RID: 5863 RVA: 0x00051B4E File Offset: 0x0004FD4E
		internal bool Visible
		{
			get
			{
				return this.dataGrid.ParentRowsVisible;
			}
			set
			{
				this.dataGrid.ParentRowsVisible = value;
			}
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x00051B5C File Offset: 0x0004FD5C
		internal void AddParent(DataGridState dgs)
		{
			CurrencyManager currencyManager = (CurrencyManager)this.dataGrid.BindingContext[dgs.DataSource, dgs.DataMember];
			this.parents.Add(dgs);
			this.SetParentCount(this.parentsCount + 1);
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x00051BA8 File Offset: 0x0004FDA8
		internal void Clear()
		{
			for (int i = 0; i < this.parents.Count; i++)
			{
				DataGridState dataGridState = this.parents[i] as DataGridState;
				dataGridState.RemoveChangeNotification();
			}
			this.parents.Clear();
			this.rowHeights.Clear();
			this.totalHeight = 0;
			this.SetParentCount(0);
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x00051C07 File Offset: 0x0004FE07
		internal void SetParentCount(int count)
		{
			this.parentsCount = count;
			this.dataGrid.Caption.BackButtonVisible = (this.parentsCount > 0 && this.dataGrid.AllowNavigation);
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x00051C37 File Offset: 0x0004FE37
		internal void CheckNull(object value, string propName)
		{
			if (value == null)
			{
				throw new ArgumentNullException("propName");
			}
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x00051C47 File Offset: 0x0004FE47
		internal void Dispose()
		{
			this.gridLinePen.Dispose();
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x00051C54 File Offset: 0x0004FE54
		internal DataGridState GetTopParent()
		{
			if (this.parentsCount < 1)
			{
				return null;
			}
			return (DataGridState)((ICloneable)this.parents[this.parentsCount - 1]).Clone();
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x00051C83 File Offset: 0x0004FE83
		internal bool IsEmpty()
		{
			return this.parentsCount == 0;
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x00051C90 File Offset: 0x0004FE90
		internal DataGridState PopTop()
		{
			if (this.parentsCount < 1)
			{
				return null;
			}
			this.SetParentCount(this.parentsCount - 1);
			DataGridState dataGridState = (DataGridState)this.parents[this.parentsCount];
			dataGridState.RemoveChangeNotification();
			this.parents.RemoveAt(this.parentsCount);
			return dataGridState;
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x00051CE5 File Offset: 0x0004FEE5
		internal void Invalidate()
		{
			if (this.dataGrid != null)
			{
				this.dataGrid.InvalidateParentRows();
			}
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x00051CFC File Offset: 0x0004FEFC
		internal void InvalidateRect(Rectangle rect)
		{
			if (this.dataGrid != null)
			{
				Rectangle r = new Rectangle(rect.X, rect.Y, rect.Width + this.borderWidth, rect.Height + this.borderWidth);
				this.dataGrid.InvalidateParentRowsRect(r);
			}
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x00051D50 File Offset: 0x0004FF50
		internal void OnLayout()
		{
			if (this.parentsCount == this.rowHeights.Count)
			{
				return;
			}
			if (this.totalHeight == 0)
			{
				this.totalHeight += 2 * this.borderWidth;
			}
			this.textRegionHeight = this.dataGrid.Font.Height + 2;
			if (this.parentsCount > this.rowHeights.Count)
			{
				int count = this.rowHeights.Count;
				for (int i = count; i < this.parentsCount; i++)
				{
					DataGridState dataGridState = (DataGridState)this.parents[i];
					GridColumnStylesCollection gridColumnStyles = dataGridState.GridColumnStyles;
					int val = 0;
					for (int j = 0; j < gridColumnStyles.Count; j++)
					{
						val = Math.Max(val, gridColumnStyles[j].GetMinimumHeight());
					}
					int num = Math.Max(val, this.textRegionHeight);
					num++;
					this.rowHeights.Add(num);
					this.totalHeight += num;
				}
				return;
			}
			if (this.parentsCount == 0)
			{
				this.totalHeight = 0;
			}
			else
			{
				this.totalHeight -= (int)this.rowHeights[this.rowHeights.Count - 1];
			}
			this.rowHeights.RemoveAt(this.rowHeights.Count - 1);
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x00051EB4 File Offset: 0x000500B4
		private int CellCount()
		{
			int num = this.ColsCount();
			if (this.dataGrid.ParentRowsLabelStyle == DataGridParentRowsLabelStyle.TableName || this.dataGrid.ParentRowsLabelStyle == DataGridParentRowsLabelStyle.Both)
			{
				num++;
			}
			return num;
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x00051EEB File Offset: 0x000500EB
		private void ResetMouseInfo()
		{
			this.downLeftArrow = false;
			this.downRightArrow = false;
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x00051EFB File Offset: 0x000500FB
		private void LeftArrowClick(int cellCount)
		{
			if (this.horizOffset > 0)
			{
				this.ResetMouseInfo();
				this.horizOffset--;
				this.Invalidate();
				return;
			}
			this.ResetMouseInfo();
			this.InvalidateRect(this.layout.leftArrow);
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x00051F38 File Offset: 0x00050138
		private void RightArrowClick(int cellCount)
		{
			if (this.horizOffset < cellCount - 1)
			{
				this.ResetMouseInfo();
				this.horizOffset++;
				this.Invalidate();
				return;
			}
			this.ResetMouseInfo();
			this.InvalidateRect(this.layout.rightArrow);
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x00051F78 File Offset: 0x00050178
		internal void OnMouseDown(int x, int y, bool alignToRight)
		{
			if (this.layout.rightArrow.IsEmpty)
			{
				return;
			}
			int cellCount = this.CellCount();
			if (this.layout.rightArrow.Contains(x, y))
			{
				this.downRightArrow = true;
				if (alignToRight)
				{
					this.LeftArrowClick(cellCount);
					return;
				}
				this.RightArrowClick(cellCount);
				return;
			}
			else
			{
				if (!this.layout.leftArrow.Contains(x, y))
				{
					if (this.downLeftArrow)
					{
						this.downLeftArrow = false;
						this.InvalidateRect(this.layout.leftArrow);
					}
					if (this.downRightArrow)
					{
						this.downRightArrow = false;
						this.InvalidateRect(this.layout.rightArrow);
					}
					return;
				}
				this.downLeftArrow = true;
				if (alignToRight)
				{
					this.RightArrowClick(cellCount);
					return;
				}
				this.LeftArrowClick(cellCount);
				return;
			}
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x0005203C File Offset: 0x0005023C
		internal void OnMouseLeave()
		{
			if (this.downLeftArrow)
			{
				this.downLeftArrow = false;
				this.InvalidateRect(this.layout.leftArrow);
			}
			if (this.downRightArrow)
			{
				this.downRightArrow = false;
				this.InvalidateRect(this.layout.rightArrow);
			}
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x0005208C File Offset: 0x0005028C
		internal void OnMouseMove(int x, int y)
		{
			if (this.downLeftArrow)
			{
				this.downLeftArrow = false;
				this.InvalidateRect(this.layout.leftArrow);
			}
			if (this.downRightArrow)
			{
				this.downRightArrow = false;
				this.InvalidateRect(this.layout.rightArrow);
			}
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x000520DC File Offset: 0x000502DC
		internal void OnMouseUp(int x, int y)
		{
			this.ResetMouseInfo();
			if (!this.layout.rightArrow.IsEmpty && this.layout.rightArrow.Contains(x, y))
			{
				this.InvalidateRect(this.layout.rightArrow);
				return;
			}
			if (!this.layout.leftArrow.IsEmpty && this.layout.leftArrow.Contains(x, y))
			{
				this.InvalidateRect(this.layout.leftArrow);
				return;
			}
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x0005215F File Offset: 0x0005035F
		internal void OnResize(Rectangle oldBounds)
		{
			this.Invalidate();
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x00052168 File Offset: 0x00050368
		internal void Paint(Graphics g, Rectangle visualbounds, bool alignRight)
		{
			Rectangle bounds = visualbounds;
			if (this.borderWidth > 0)
			{
				this.PaintBorder(g, bounds);
				bounds.Inflate(-this.borderWidth, -this.borderWidth);
			}
			this.PaintParentRows(g, bounds, alignRight);
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x000521A8 File Offset: 0x000503A8
		private void PaintBorder(Graphics g, Rectangle bounds)
		{
			Rectangle rect = bounds;
			rect.Height = this.borderWidth;
			g.FillRectangle(this.borderBrush, rect);
			rect.Y = bounds.Bottom - this.borderWidth;
			g.FillRectangle(this.borderBrush, rect);
			rect = new Rectangle(bounds.X, bounds.Y + this.borderWidth, this.borderWidth, bounds.Height - 2 * this.borderWidth);
			g.FillRectangle(this.borderBrush, rect);
			rect.X = bounds.Right - this.borderWidth;
			g.FillRectangle(this.borderBrush, rect);
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x00052254 File Offset: 0x00050454
		private int GetTableBoxWidth(Graphics g, Font font)
		{
			Font font2 = font;
			try
			{
				font2 = new Font(font, FontStyle.Bold);
			}
			catch
			{
			}
			int num = 0;
			for (int i = 0; i < this.parentsCount; i++)
			{
				DataGridState dataGridState = (DataGridState)this.parents[i];
				string text = dataGridState.ListManager.GetListName() + " :";
				int val = (int)g.MeasureString(text, font2).Width;
				num = Math.Max(val, num);
			}
			return num;
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x000522DC File Offset: 0x000504DC
		private int GetColBoxWidth(Graphics g, Font font, int colNum)
		{
			int num = 0;
			for (int i = 0; i < this.parentsCount; i++)
			{
				DataGridState dataGridState = (DataGridState)this.parents[i];
				GridColumnStylesCollection gridColumnStyles = dataGridState.GridColumnStyles;
				if (colNum < gridColumnStyles.Count)
				{
					string text = gridColumnStyles[colNum].HeaderText + " :";
					int val = (int)g.MeasureString(text, font).Width;
					num = Math.Max(val, num);
				}
			}
			return num;
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x00052358 File Offset: 0x00050558
		private int GetColDataBoxWidth(Graphics g, int colNum)
		{
			int num = 0;
			for (int i = 0; i < this.parentsCount; i++)
			{
				DataGridState dataGridState = (DataGridState)this.parents[i];
				GridColumnStylesCollection gridColumnStyles = dataGridState.GridColumnStyles;
				if (colNum < gridColumnStyles.Count)
				{
					object columnValueAtRow = gridColumnStyles[colNum].GetColumnValueAtRow((CurrencyManager)this.dataGrid.BindingContext[dataGridState.DataSource, dataGridState.DataMember], dataGridState.LinkingRow.RowNumber);
					int width = gridColumnStyles[colNum].GetPreferredSize(g, columnValueAtRow).Width;
					num = Math.Max(width, num);
				}
			}
			return num;
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x00052400 File Offset: 0x00050600
		private int ColsCount()
		{
			int num = 0;
			for (int i = 0; i < this.parentsCount; i++)
			{
				DataGridState dataGridState = (DataGridState)this.parents[i];
				num = Math.Max(num, dataGridState.GridColumnStyles.Count);
			}
			return num;
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x00052448 File Offset: 0x00050648
		private int TotalWidth(int tableNameBoxWidth, int[] colsNameWidths, int[] colsDataWidths)
		{
			int num = 0;
			num += tableNameBoxWidth;
			for (int i = 0; i < colsNameWidths.Length; i++)
			{
				num += colsNameWidths[i];
				num += colsDataWidths[i];
			}
			return num + 3 * (colsNameWidths.Length - 1);
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x00052480 File Offset: 0x00050680
		private void ComputeLayout(Rectangle bounds, int tableNameBoxWidth, int[] colsNameWidths, int[] colsDataWidths)
		{
			int num = this.TotalWidth(tableNameBoxWidth, colsNameWidths, colsDataWidths);
			if (num > bounds.Width)
			{
				this.layout.leftArrow = new Rectangle(bounds.X, bounds.Y, 15, bounds.Height);
				this.layout.data = new Rectangle(this.layout.leftArrow.Right, bounds.Y, bounds.Width - 30, bounds.Height);
				this.layout.rightArrow = new Rectangle(this.layout.data.Right, bounds.Y, 15, bounds.Height);
				return;
			}
			this.layout.data = bounds;
			this.layout.leftArrow = Rectangle.Empty;
			this.layout.rightArrow = Rectangle.Empty;
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x00052564 File Offset: 0x00050764
		private void PaintParentRows(Graphics g, Rectangle bounds, bool alignToRight)
		{
			int tableNameBoxWidth = 0;
			int num = this.ColsCount();
			int[] array = new int[num];
			int[] array2 = new int[num];
			if (this.dataGrid.ParentRowsLabelStyle == DataGridParentRowsLabelStyle.TableName || this.dataGrid.ParentRowsLabelStyle == DataGridParentRowsLabelStyle.Both)
			{
				tableNameBoxWidth = this.GetTableBoxWidth(g, this.dataGrid.Font);
			}
			for (int i = 0; i < num; i++)
			{
				if (this.dataGrid.ParentRowsLabelStyle == DataGridParentRowsLabelStyle.ColumnName || this.dataGrid.ParentRowsLabelStyle == DataGridParentRowsLabelStyle.Both)
				{
					array[i] = this.GetColBoxWidth(g, this.dataGrid.Font, i);
				}
				else
				{
					array[i] = 0;
				}
				array2[i] = this.GetColDataBoxWidth(g, i);
			}
			this.ComputeLayout(bounds, tableNameBoxWidth, array, array2);
			if (!this.layout.leftArrow.IsEmpty)
			{
				g.FillRectangle(this.BackBrush, this.layout.leftArrow);
				this.PaintLeftArrow(g, this.layout.leftArrow, alignToRight);
			}
			Rectangle data = this.layout.data;
			for (int j = 0; j < this.parentsCount; j++)
			{
				data.Height = (int)this.rowHeights[j];
				if (data.Y > bounds.Bottom)
				{
					break;
				}
				int num2 = this.PaintRow(g, data, j, this.dataGrid.Font, alignToRight, tableNameBoxWidth, array, array2);
				if (j == this.parentsCount - 1)
				{
					break;
				}
				g.DrawLine(this.gridLinePen, data.X, data.Bottom, data.X + num2, data.Bottom);
				data.Y += data.Height;
			}
			if (!this.layout.rightArrow.IsEmpty)
			{
				g.FillRectangle(this.BackBrush, this.layout.rightArrow);
				this.PaintRightArrow(g, this.layout.rightArrow, alignToRight);
			}
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x0005274C File Offset: 0x0005094C
		private Bitmap GetBitmap(string bitmapName, Color transparentColor)
		{
			Bitmap bitmap = null;
			try
			{
				bitmap = new Bitmap(typeof(DataGridParentRows), bitmapName);
				bitmap.MakeTransparent(transparentColor);
			}
			catch (Exception ex)
			{
			}
			return bitmap;
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x0005278C File Offset: 0x0005098C
		private Bitmap GetRightArrowBitmap()
		{
			if (DataGridParentRows.rightArrow == null)
			{
				DataGridParentRows.rightArrow = this.GetBitmap("DataGridParentRows.RightArrow.bmp", Color.White);
			}
			return DataGridParentRows.rightArrow;
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x000527AF File Offset: 0x000509AF
		private Bitmap GetLeftArrowBitmap()
		{
			if (DataGridParentRows.leftArrow == null)
			{
				DataGridParentRows.leftArrow = this.GetBitmap("DataGridParentRows.LeftArrow.bmp", Color.White);
			}
			return DataGridParentRows.leftArrow;
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x000527D4 File Offset: 0x000509D4
		private void PaintBitmap(Graphics g, Bitmap b, Rectangle bounds)
		{
			int x = bounds.X + (bounds.Width - b.Width) / 2;
			int y = bounds.Y + (bounds.Height - b.Height) / 2;
			Rectangle rectangle = new Rectangle(x, y, b.Width, b.Height);
			g.FillRectangle(this.BackBrush, rectangle);
			ImageAttributes imageAttributes = new ImageAttributes();
			this.colorMap[0].NewColor = this.ForeColor;
			imageAttributes.SetRemapTable(this.colorMap, ColorAdjustType.Bitmap);
			g.DrawImage(b, rectangle, 0, 0, rectangle.Width, rectangle.Height, GraphicsUnit.Pixel, imageAttributes);
			imageAttributes.Dispose();
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x0005287C File Offset: 0x00050A7C
		private void PaintDownButton(Graphics g, Rectangle bounds)
		{
			g.DrawLine(Pens.Black, bounds.X, bounds.Y, bounds.X + bounds.Width, bounds.Y);
			g.DrawLine(Pens.White, bounds.X + bounds.Width, bounds.Y, bounds.X + bounds.Width, bounds.Y + bounds.Height);
			g.DrawLine(Pens.White, bounds.X + bounds.Width, bounds.Y + bounds.Height, bounds.X, bounds.Y + bounds.Height);
			g.DrawLine(Pens.Black, bounds.X, bounds.Y + bounds.Height, bounds.X, bounds.Y);
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x00052968 File Offset: 0x00050B68
		private void PaintLeftArrow(Graphics g, Rectangle bounds, bool alignToRight)
		{
			Bitmap leftArrowBitmap = this.GetLeftArrowBitmap();
			if (this.downLeftArrow)
			{
				this.PaintDownButton(g, bounds);
				this.layout.leftArrow.Inflate(-1, -1);
				Bitmap obj = leftArrowBitmap;
				lock (obj)
				{
					this.PaintBitmap(g, leftArrowBitmap, bounds);
				}
				this.layout.leftArrow.Inflate(1, 1);
				return;
			}
			Bitmap obj2 = leftArrowBitmap;
			lock (obj2)
			{
				this.PaintBitmap(g, leftArrowBitmap, bounds);
			}
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x00052A14 File Offset: 0x00050C14
		private void PaintRightArrow(Graphics g, Rectangle bounds, bool alignToRight)
		{
			Bitmap rightArrowBitmap = this.GetRightArrowBitmap();
			if (this.downRightArrow)
			{
				this.PaintDownButton(g, bounds);
				this.layout.rightArrow.Inflate(-1, -1);
				Bitmap obj = rightArrowBitmap;
				lock (obj)
				{
					this.PaintBitmap(g, rightArrowBitmap, bounds);
				}
				this.layout.rightArrow.Inflate(1, 1);
				return;
			}
			Bitmap obj2 = rightArrowBitmap;
			lock (obj2)
			{
				this.PaintBitmap(g, rightArrowBitmap, bounds);
			}
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x00052AC0 File Offset: 0x00050CC0
		private int PaintRow(Graphics g, Rectangle bounds, int row, Font font, bool alignToRight, int tableNameBoxWidth, int[] colsNameWidths, int[] colsDataWidths)
		{
			DataGridState dataGridState = (DataGridState)this.parents[row];
			Rectangle rectangle = bounds;
			Rectangle bounds2 = bounds;
			rectangle.Height = (int)this.rowHeights[row];
			bounds2.Height = (int)this.rowHeights[row];
			int num = 0;
			int num2 = 0;
			if (this.dataGrid.ParentRowsLabelStyle == DataGridParentRowsLabelStyle.TableName || this.dataGrid.ParentRowsLabelStyle == DataGridParentRowsLabelStyle.Both)
			{
				if (num2 < this.horizOffset)
				{
					num2++;
				}
				else
				{
					rectangle.Width = Math.Min(rectangle.Width, tableNameBoxWidth);
					rectangle.X = this.MirrorRect(bounds, rectangle, alignToRight);
					string text = dataGridState.ListManager.GetListName() + ": ";
					this.PaintText(g, rectangle, text, font, true, alignToRight);
					num += rectangle.Width;
				}
			}
			if (num >= bounds.Width)
			{
				return bounds.Width;
			}
			bounds2.Width -= num;
			bounds2.X += (alignToRight ? 0 : num);
			num += this.PaintColumns(g, bounds2, dataGridState, font, alignToRight, colsNameWidths, colsDataWidths, num2);
			if (num < bounds.Width)
			{
				rectangle.X = bounds.X + num;
				rectangle.Width = bounds.Width - num;
				rectangle.X = this.MirrorRect(bounds, rectangle, alignToRight);
				g.FillRectangle(this.BackBrush, rectangle);
			}
			return num;
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x00052C34 File Offset: 0x00050E34
		private int PaintColumns(Graphics g, Rectangle bounds, DataGridState dgs, Font font, bool alignToRight, int[] colsNameWidths, int[] colsDataWidths, int skippedCells)
		{
			Rectangle rectangle = bounds;
			GridColumnStylesCollection gridColumnStyles = dgs.GridColumnStyles;
			int num = 0;
			int num2 = 0;
			while (num2 < gridColumnStyles.Count && num < bounds.Width)
			{
				if ((this.dataGrid.ParentRowsLabelStyle == DataGridParentRowsLabelStyle.ColumnName || this.dataGrid.ParentRowsLabelStyle == DataGridParentRowsLabelStyle.Both) && skippedCells >= this.horizOffset)
				{
					rectangle.X = bounds.X + num;
					rectangle.Width = Math.Min(bounds.Width - num, colsNameWidths[num2]);
					rectangle.X = this.MirrorRect(bounds, rectangle, alignToRight);
					string text = gridColumnStyles[num2].HeaderText + ": ";
					this.PaintText(g, rectangle, text, font, false, alignToRight);
					num += rectangle.Width;
				}
				if (num >= bounds.Width)
				{
					break;
				}
				if (skippedCells < this.horizOffset)
				{
					skippedCells++;
				}
				else
				{
					rectangle.X = bounds.X + num;
					rectangle.Width = Math.Min(bounds.Width - num, colsDataWidths[num2]);
					rectangle.X = this.MirrorRect(bounds, rectangle, alignToRight);
					gridColumnStyles[num2].Paint(g, rectangle, (CurrencyManager)this.dataGrid.BindingContext[dgs.DataSource, dgs.DataMember], this.dataGrid.BindingContext[dgs.DataSource, dgs.DataMember].Position, this.BackBrush, this.ForeBrush, alignToRight);
					num += rectangle.Width;
					g.DrawLine(new Pen(SystemColors.ControlDark), alignToRight ? rectangle.X : rectangle.Right, rectangle.Y, alignToRight ? rectangle.X : rectangle.Right, rectangle.Bottom);
					num++;
					if (num2 < gridColumnStyles.Count - 1)
					{
						rectangle.X = bounds.X + num;
						rectangle.Width = Math.Min(bounds.Width - num, 3);
						rectangle.X = this.MirrorRect(bounds, rectangle, alignToRight);
						g.FillRectangle(this.BackBrush, rectangle);
						num += 3;
					}
				}
				num2++;
			}
			return num;
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x00052E70 File Offset: 0x00051070
		private int PaintText(Graphics g, Rectangle textBounds, string text, Font font, bool bold, bool alignToRight)
		{
			Font font2 = font;
			if (bold)
			{
				try
				{
					font2 = new Font(font, FontStyle.Bold);
					goto IL_18;
				}
				catch
				{
					goto IL_18;
				}
			}
			font2 = font;
			IL_18:
			g.FillRectangle(this.BackBrush, textBounds);
			StringFormat stringFormat = new StringFormat();
			if (alignToRight)
			{
				stringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
				stringFormat.Alignment = StringAlignment.Far;
			}
			stringFormat.FormatFlags |= StringFormatFlags.NoWrap;
			textBounds.Offset(0, 2);
			textBounds.Height -= 2;
			g.DrawString(text, font2, this.ForeBrush, textBounds, stringFormat);
			stringFormat.Dispose();
			return textBounds.Width;
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x00052F20 File Offset: 0x00051120
		private int MirrorRect(Rectangle surroundingRect, Rectangle containedRect, bool alignToRight)
		{
			if (alignToRight)
			{
				return surroundingRect.Right - containedRect.Right + surroundingRect.X;
			}
			return containedRect.X;
		}

		// Token: 0x04000A5F RID: 2655
		private DataGrid dataGrid;

		// Token: 0x04000A60 RID: 2656
		private SolidBrush backBrush = DataGrid.DefaultParentRowsBackBrush;

		// Token: 0x04000A61 RID: 2657
		private SolidBrush foreBrush = DataGrid.DefaultParentRowsForeBrush;

		// Token: 0x04000A62 RID: 2658
		private int borderWidth = 1;

		// Token: 0x04000A63 RID: 2659
		private Brush borderBrush = new SolidBrush(SystemColors.WindowFrame);

		// Token: 0x04000A64 RID: 2660
		private static Bitmap rightArrow;

		// Token: 0x04000A65 RID: 2661
		private static Bitmap leftArrow;

		// Token: 0x04000A66 RID: 2662
		private ColorMap[] colorMap = new ColorMap[]
		{
			new ColorMap()
		};

		// Token: 0x04000A67 RID: 2663
		private Pen gridLinePen = SystemPens.Control;

		// Token: 0x04000A68 RID: 2664
		private int totalHeight;

		// Token: 0x04000A69 RID: 2665
		private int textRegionHeight;

		// Token: 0x04000A6A RID: 2666
		private DataGridParentRows.Layout layout = new DataGridParentRows.Layout();

		// Token: 0x04000A6B RID: 2667
		private bool downLeftArrow;

		// Token: 0x04000A6C RID: 2668
		private bool downRightArrow;

		// Token: 0x04000A6D RID: 2669
		private int horizOffset;

		// Token: 0x04000A6E RID: 2670
		private ArrayList parents = new ArrayList();

		// Token: 0x04000A6F RID: 2671
		private int parentsCount;

		// Token: 0x04000A70 RID: 2672
		private ArrayList rowHeights = new ArrayList();

		// Token: 0x04000A71 RID: 2673
		private AccessibleObject accessibleObject;

		// Token: 0x0200064F RID: 1615
		private class Layout
		{
			// Token: 0x060064EF RID: 25839 RVA: 0x00177D2E File Offset: 0x00175F2E
			public Layout()
			{
				this.data = Rectangle.Empty;
				this.leftArrow = Rectangle.Empty;
				this.rightArrow = Rectangle.Empty;
			}

			// Token: 0x060064F0 RID: 25840 RVA: 0x00177D58 File Offset: 0x00175F58
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder(200);
				stringBuilder.Append("ParentRows Layout: \n");
				stringBuilder.Append("data = ");
				stringBuilder.Append(this.data.ToString());
				stringBuilder.Append("\n leftArrow = ");
				stringBuilder.Append(this.leftArrow.ToString());
				stringBuilder.Append("\n rightArrow = ");
				stringBuilder.Append(this.rightArrow.ToString());
				stringBuilder.Append("\n");
				return stringBuilder.ToString();
			}

			// Token: 0x040039DD RID: 14813
			public Rectangle data;

			// Token: 0x040039DE RID: 14814
			public Rectangle leftArrow;

			// Token: 0x040039DF RID: 14815
			public Rectangle rightArrow;
		}

		// Token: 0x02000650 RID: 1616
		[ComVisible(true)]
		protected internal class DataGridParentRowsAccessibleObject : AccessibleObject
		{
			// Token: 0x060064F1 RID: 25841 RVA: 0x00177DFA File Offset: 0x00175FFA
			public DataGridParentRowsAccessibleObject(DataGridParentRows owner)
			{
				this.owner = owner;
			}

			// Token: 0x170015A9 RID: 5545
			// (get) Token: 0x060064F2 RID: 25842 RVA: 0x00177E09 File Offset: 0x00176009
			internal DataGridParentRows Owner
			{
				get
				{
					return this.owner;
				}
			}

			// Token: 0x170015AA RID: 5546
			// (get) Token: 0x060064F3 RID: 25843 RVA: 0x00177E11 File Offset: 0x00176011
			public override Rectangle Bounds
			{
				get
				{
					return this.owner.dataGrid.RectangleToScreen(this.owner.dataGrid.ParentRowsBounds);
				}
			}

			// Token: 0x170015AB RID: 5547
			// (get) Token: 0x060064F4 RID: 25844 RVA: 0x00177E33 File Offset: 0x00176033
			public override string DefaultAction
			{
				get
				{
					return SR.GetString("AccDGNavigateBack");
				}
			}

			// Token: 0x170015AC RID: 5548
			// (get) Token: 0x060064F5 RID: 25845 RVA: 0x00177E3F File Offset: 0x0017603F
			public override string Name
			{
				get
				{
					return SR.GetString("AccDGParentRows");
				}
			}

			// Token: 0x170015AD RID: 5549
			// (get) Token: 0x060064F6 RID: 25846 RVA: 0x00177E4B File Offset: 0x0017604B
			public override AccessibleObject Parent
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.owner.dataGrid.AccessibilityObject;
				}
			}

			// Token: 0x170015AE RID: 5550
			// (get) Token: 0x060064F7 RID: 25847 RVA: 0x00177E5D File Offset: 0x0017605D
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.List;
				}
			}

			// Token: 0x170015AF RID: 5551
			// (get) Token: 0x060064F8 RID: 25848 RVA: 0x00177E64 File Offset: 0x00176064
			public override AccessibleStates State
			{
				get
				{
					AccessibleStates accessibleStates = AccessibleStates.ReadOnly;
					if (this.owner.parentsCount == 0)
					{
						accessibleStates |= AccessibleStates.Invisible;
					}
					if (this.owner.dataGrid.ParentRowsVisible)
					{
						accessibleStates |= AccessibleStates.Expanded;
					}
					else
					{
						accessibleStates |= AccessibleStates.Collapsed;
					}
					return accessibleStates;
				}
			}

			// Token: 0x170015B0 RID: 5552
			// (get) Token: 0x060064F9 RID: 25849 RVA: 0x00015ECC File Offset: 0x000140CC
			public override string Value
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return null;
				}
			}

			// Token: 0x060064FA RID: 25850 RVA: 0x00177EAE File Offset: 0x001760AE
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				this.owner.dataGrid.NavigateBack();
			}

			// Token: 0x060064FB RID: 25851 RVA: 0x00177EC0 File Offset: 0x001760C0
			public override AccessibleObject GetChild(int index)
			{
				return ((DataGridState)this.owner.parents[index]).ParentRowAccessibleObject;
			}

			// Token: 0x060064FC RID: 25852 RVA: 0x00177EDD File Offset: 0x001760DD
			public override int GetChildCount()
			{
				return this.owner.parentsCount;
			}

			// Token: 0x060064FD RID: 25853 RVA: 0x00015ECC File Offset: 0x000140CC
			public override AccessibleObject GetFocused()
			{
				return null;
			}

			// Token: 0x060064FE RID: 25854 RVA: 0x00177EEC File Offset: 0x001760EC
			internal AccessibleObject GetNext(AccessibleObject child)
			{
				int childCount = this.GetChildCount();
				bool flag = false;
				for (int i = 0; i < childCount; i++)
				{
					if (flag)
					{
						return this.GetChild(i);
					}
					if (this.GetChild(i) == child)
					{
						flag = true;
					}
				}
				return null;
			}

			// Token: 0x060064FF RID: 25855 RVA: 0x00177F28 File Offset: 0x00176128
			internal AccessibleObject GetPrev(AccessibleObject child)
			{
				int childCount = this.GetChildCount();
				bool flag = false;
				for (int i = childCount - 1; i >= 0; i--)
				{
					if (flag)
					{
						return this.GetChild(i);
					}
					if (this.GetChild(i) == child)
					{
						flag = true;
					}
				}
				return null;
			}

			// Token: 0x06006500 RID: 25856 RVA: 0x00177F64 File Offset: 0x00176164
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override AccessibleObject Navigate(AccessibleNavigation navdir)
			{
				switch (navdir)
				{
				case AccessibleNavigation.Up:
				case AccessibleNavigation.Left:
				case AccessibleNavigation.Previous:
					return this.Parent.GetChild(this.GetChildCount() - 1);
				case AccessibleNavigation.Down:
				case AccessibleNavigation.Right:
				case AccessibleNavigation.Next:
					return this.Parent.GetChild(1);
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

			// Token: 0x06006501 RID: 25857 RVA: 0x000072B6 File Offset: 0x000054B6
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void Select(AccessibleSelection flags)
			{
			}

			// Token: 0x040039E0 RID: 14816
			private DataGridParentRows owner;
		}
	}
}
