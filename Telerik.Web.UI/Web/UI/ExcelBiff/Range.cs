using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AD3 RID: 2771
	internal class Range
	{
		// Token: 0x17002235 RID: 8757
		// (get) Token: 0x06006878 RID: 26744 RVA: 0x001875E9 File Offset: 0x001857E9
		public Worksheet Parent
		{
			get
			{
				return this.worksheet;
			}
		}

		// Token: 0x17002236 RID: 8758
		// (get) Token: 0x06006879 RID: 26745 RVA: 0x001875F1 File Offset: 0x001857F1
		public int Col
		{
			get
			{
				return this.col;
			}
		}

		// Token: 0x17002237 RID: 8759
		// (get) Token: 0x0600687A RID: 26746 RVA: 0x001875F9 File Offset: 0x001857F9
		public int Row
		{
			get
			{
				return this.row;
			}
		}

		// Token: 0x17002238 RID: 8760
		// (get) Token: 0x0600687B RID: 26747 RVA: 0x00187601 File Offset: 0x00185801
		public int ColSpan
		{
			get
			{
				return this.colSpan;
			}
		}

		// Token: 0x17002239 RID: 8761
		// (get) Token: 0x0600687C RID: 26748 RVA: 0x00187609 File Offset: 0x00185809
		public int RowSpan
		{
			get
			{
				return this.rowSpan;
			}
		}

		// Token: 0x1700223A RID: 8762
		// (get) Token: 0x0600687D RID: 26749 RVA: 0x00187611 File Offset: 0x00185811
		// (set) Token: 0x0600687E RID: 26750 RVA: 0x00187624 File Offset: 0x00185824
		public bool Merged
		{
			get
			{
				return this.worksheet.MergedRanges.Contains(this);
			}
			set
			{
				if (value != this.Merged)
				{
					if (value)
					{
						if (!this.CanBeMerged())
						{
							string message = string.Format("Attempting to merge range {0}, that intersects with an already merged range.", this);
							throw new InvalidOperationException(message);
						}
						this.worksheet.MergedRanges.Add(this);
					}
					else
					{
						this.worksheet.MergedRanges.Remove(this);
					}
					this.Merge(value);
				}
			}
		}

		// Token: 0x1700223B RID: 8763
		// (set) Token: 0x0600687F RID: 26751 RVA: 0x0018769C File Offset: 0x0018589C
		public object Value
		{
			set
			{
				this.ForEachCell(delegate(Cell cell)
				{
					cell.Value = value;
				});
			}
		}

		// Token: 0x1700223C RID: 8764
		// (set) Token: 0x06006880 RID: 26752 RVA: 0x001876E0 File Offset: 0x001858E0
		public string Format
		{
			set
			{
				this.ForEachCell(delegate(Cell cell)
				{
					cell.Format = value;
				});
			}
		}

		// Token: 0x1700223D RID: 8765
		// (set) Token: 0x06006881 RID: 26753 RVA: 0x00187724 File Offset: 0x00185924
		public Color BackgroundColor
		{
			set
			{
				this.ForEachCell(delegate(Cell cell)
				{
					cell.BackgroundColor = value;
				});
			}
		}

		// Token: 0x1700223E RID: 8766
		// (set) Token: 0x06006882 RID: 26754 RVA: 0x00187768 File Offset: 0x00185968
		public Color Color
		{
			set
			{
				this.ForEachCell(delegate(Cell cell)
				{
					cell.Color = value;
				});
			}
		}

		// Token: 0x1700223F RID: 8767
		// (set) Token: 0x06006883 RID: 26755 RVA: 0x001877AC File Offset: 0x001859AC
		public string FontName
		{
			set
			{
				this.ForEachCell(delegate(Cell cell)
				{
					cell.FontName = value;
				});
			}
		}

		// Token: 0x17002240 RID: 8768
		// (set) Token: 0x06006884 RID: 26756 RVA: 0x001877F0 File Offset: 0x001859F0
		public float FontSizeInPoints
		{
			set
			{
				this.ForEachCell(delegate(Cell cell)
				{
					cell.FontSizeInPoints = value;
				});
			}
		}

		// Token: 0x17002241 RID: 8769
		// (set) Token: 0x06006885 RID: 26757 RVA: 0x00187834 File Offset: 0x00185A34
		public bool FontBold
		{
			set
			{
				this.ForEachCell(delegate(Cell cell)
				{
					cell.FontBold = value;
				});
			}
		}

		// Token: 0x17002242 RID: 8770
		// (set) Token: 0x06006886 RID: 26758 RVA: 0x00187878 File Offset: 0x00185A78
		public bool FontItalic
		{
			set
			{
				this.ForEachCell(delegate(Cell cell)
				{
					cell.FontItalic = value;
				});
			}
		}

		// Token: 0x17002243 RID: 8771
		// (set) Token: 0x06006887 RID: 26759 RVA: 0x001878BC File Offset: 0x00185ABC
		public bool FontUnderline
		{
			set
			{
				this.ForEachCell(delegate(Cell cell)
				{
					cell.FontUnderline = value;
				});
			}
		}

		// Token: 0x17002244 RID: 8772
		// (set) Token: 0x06006888 RID: 26760 RVA: 0x00187900 File Offset: 0x00185B00
		public bool FontStrikeout
		{
			set
			{
				this.ForEachCell(delegate(Cell cell)
				{
					cell.FontStrikeout = value;
				});
			}
		}

		// Token: 0x17002245 RID: 8773
		// (set) Token: 0x06006889 RID: 26761 RVA: 0x00187944 File Offset: 0x00185B44
		public bool RTL
		{
			set
			{
				this.ForEachCell(delegate(Cell cell)
				{
					cell.RTL = value;
				});
			}
		}

		// Token: 0x17002246 RID: 8774
		// (set) Token: 0x0600688A RID: 26762 RVA: 0x00187988 File Offset: 0x00185B88
		public double RotationAngle
		{
			set
			{
				this.ForEachCell(delegate(Cell cell)
				{
					cell.RotationAngle = value;
				});
			}
		}

		// Token: 0x17002247 RID: 8775
		// (set) Token: 0x0600688B RID: 26763 RVA: 0x001879CC File Offset: 0x00185BCC
		public VerticalAlignment VerticalAlignment
		{
			set
			{
				this.ForEachCell(delegate(Cell cell)
				{
					cell.VerticalAlignment = value;
				});
			}
		}

		// Token: 0x17002248 RID: 8776
		// (set) Token: 0x0600688C RID: 26764 RVA: 0x00187A10 File Offset: 0x00185C10
		public HorizontalAlignment HorizontalAlignment
		{
			set
			{
				this.ForEachCell(delegate(Cell cell)
				{
					cell.HorizontalAlignment = value;
				});
			}
		}

		// Token: 0x17002249 RID: 8777
		// (set) Token: 0x0600688D RID: 26765 RVA: 0x00187A54 File Offset: 0x00185C54
		public bool TextWrap
		{
			set
			{
				this.ForEachCell(delegate(Cell cell)
				{
					cell.TextWrap = value;
				});
			}
		}

		// Token: 0x1700224A RID: 8778
		// (get) Token: 0x0600688E RID: 26766 RVA: 0x00187A80 File Offset: 0x00185C80
		public Borders Borders
		{
			get
			{
				if (this.borders == null)
				{
					this.borders = new Borders(this);
				}
				return this.borders;
			}
		}

		// Token: 0x1700224B RID: 8779
		// (set) Token: 0x0600688F RID: 26767 RVA: 0x00187A9C File Offset: 0x00185C9C
		public PageBreak PageBreak
		{
			set
			{
				if (value == PageBreak.None)
				{
					if (this.worksheet.PageBreakInfos.ContainsKey(this))
					{
						this.worksheet.PageBreakInfos.Remove(this);
						return;
					}
				}
				else
				{
					this.worksheet.PageBreakInfos[this] = value;
				}
			}
		}

		// Token: 0x1700224C RID: 8780
		// (get) Token: 0x06006890 RID: 26768 RVA: 0x00187AD9 File Offset: 0x00185CD9
		internal int FirstCol
		{
			get
			{
				return this.col;
			}
		}

		// Token: 0x1700224D RID: 8781
		// (get) Token: 0x06006891 RID: 26769 RVA: 0x00187AE1 File Offset: 0x00185CE1
		internal int LastCol
		{
			get
			{
				return this.col + this.colSpan - 1;
			}
		}

		// Token: 0x1700224E RID: 8782
		// (get) Token: 0x06006892 RID: 26770 RVA: 0x00187AF2 File Offset: 0x00185CF2
		internal int FirstRow
		{
			get
			{
				return this.row;
			}
		}

		// Token: 0x1700224F RID: 8783
		// (get) Token: 0x06006893 RID: 26771 RVA: 0x00187AFA File Offset: 0x00185CFA
		internal int LastRow
		{
			get
			{
				return this.row + this.rowSpan - 1;
			}
		}

		// Token: 0x06006894 RID: 26772 RVA: 0x00187B0B File Offset: 0x00185D0B
		public Range(Worksheet parent, int col, int row, int colSpan, int rowSpan)
		{
			this.worksheet = parent;
			this.col = col;
			this.row = row;
			this.colSpan = colSpan;
			this.rowSpan = rowSpan;
		}

		// Token: 0x06006895 RID: 26773 RVA: 0x00187B38 File Offset: 0x00185D38
		private void ForEachCell(Action<Cell> cellAction)
		{
			for (int i = this.FirstRow; i <= this.LastRow; i++)
			{
				for (int j = this.FirstCol; j <= this.LastCol; j++)
				{
					cellAction(this.worksheet.CellGrid[j, i]);
				}
			}
		}

		// Token: 0x06006896 RID: 26774 RVA: 0x00187B8C File Offset: 0x00185D8C
		private void Merge(bool merge)
		{
			bool eaten = false;
			for (int i = this.FirstRow; i <= this.LastRow; i++)
			{
				for (int j = this.FirstCol; j <= this.LastCol; j++)
				{
					Cell cell = this.worksheet.CellGrid[j, i];
					if (merge)
					{
						cell.Eaten = eaten;
					}
					else
					{
						cell.Eaten = false;
					}
					eaten = true;
				}
			}
		}

		// Token: 0x06006897 RID: 26775 RVA: 0x00187BF0 File Offset: 0x00185DF0
		internal bool CanBeMerged()
		{
			foreach (Range range in this.worksheet.MergedRanges)
			{
				if (this != range && this.IntersectsWith(range))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006898 RID: 26776 RVA: 0x00187C58 File Offset: 0x00185E58
		internal bool IntersectsWith(Range range)
		{
			return range.FirstCol <= this.LastCol && this.FirstCol <= range.LastCol && range.FirstRow <= this.LastRow && this.FirstRow <= range.LastRow;
		}

		// Token: 0x06006899 RID: 26777 RVA: 0x00187C98 File Offset: 0x00185E98
		internal void SetBorderColor(Color color, BorderKind kind)
		{
			switch (kind)
			{
			case BorderKind.Top:
				for (int i = this.Col; i <= this.LastCol; i++)
				{
					this.worksheet.CellGrid[i, this.Row].TopBorderColor = color;
				}
				return;
			case BorderKind.Bottom:
				for (int j = this.Col; j <= this.LastCol; j++)
				{
					this.worksheet.CellGrid[j, this.LastRow].BottomBorderColor = color;
				}
				return;
			case BorderKind.Left:
				for (int k = this.Row; k <= this.LastRow; k++)
				{
					this.worksheet.CellGrid[this.Col, k].LeftBorderColor = color;
				}
				return;
			case BorderKind.Right:
				for (int l = this.Row; l <= this.LastRow; l++)
				{
					this.worksheet.CellGrid[this.LastCol, l].RightBorderColor = color;
				}
				return;
			default:
				return;
			}
		}

		// Token: 0x0600689A RID: 26778 RVA: 0x00187D90 File Offset: 0x00185F90
		internal void SetBorderStyle(BorderStyle borderStyle, BorderKind kind)
		{
			switch (kind)
			{
			case BorderKind.Top:
				for (int i = this.Col; i <= this.LastCol; i++)
				{
					this.worksheet.CellGrid[i, this.Row].TopBorderStyle = borderStyle;
				}
				return;
			case BorderKind.Bottom:
				for (int j = this.Col; j <= this.LastCol; j++)
				{
					this.worksheet.CellGrid[j, this.LastRow].BottomBorderStyle = borderStyle;
				}
				return;
			case BorderKind.Left:
				for (int k = this.Row; k <= this.LastRow; k++)
				{
					this.worksheet.CellGrid[this.Col, k].LeftBorderStyle = borderStyle;
				}
				return;
			case BorderKind.Right:
				for (int l = this.Row; l <= this.LastRow; l++)
				{
					this.worksheet.CellGrid[this.LastCol, l].RightBorderStyle = borderStyle;
				}
				return;
			default:
				return;
			}
		}

		// Token: 0x0600689B RID: 26779 RVA: 0x00187E88 File Offset: 0x00186088
		public void AddPicture(Image image, double left, double top, double width, double height)
		{
			ImageFormat format;
			if (image.RawFormat.Equals(ImageFormat.Bmp) || image.RawFormat.Equals(ImageFormat.Jpeg) || image.RawFormat.Equals(ImageFormat.Gif) || image.RawFormat.Equals(ImageFormat.Png))
			{
				format = image.RawFormat;
			}
			else
			{
				format = ImageFormat.Png;
			}
			MemoryStream memoryStream = new MemoryStream();
			image.Save(memoryStream, format);
			byte[] imageData = memoryStream.ToArray();
			Escher.RecordType imageType = ImageHandler.GetImageType(format);
			string uniqueImageID = ImageHandler.GetUniqueImageID();
			ushort leftAnchorCol;
			double num;
			this.CalculateAnchor(Range.Direction.Horizontal, left * 1.3333333333333333, (ushort)this.FirstCol, out leftAnchorCol, out num);
			ushort topAnchorRow;
			double num2;
			this.CalculateAnchor(Range.Direction.Vertical, top * 1.3333333333333333, (ushort)this.FirstRow, out topAnchorRow, out num2);
			ushort num3;
			double num4;
			this.CalculateAnchor(Range.Direction.Horizontal, (left + width) * 1.3333333333333333, (ushort)this.FirstCol, out num3, out num4);
			if (num4 == 0.0)
			{
				num3 += 1;
			}
			ushort num5;
			double num6;
			this.CalculateAnchor(Range.Direction.Vertical, (top + height) * 1.3333333333333333, (ushort)this.FirstRow, out num5, out num6);
			if (num6 == 0.0)
			{
				num5 += 1;
			}
			Escher.ClientAnchor.SPRC clientSPRC = new Escher.ClientAnchor.SPRC(leftAnchorCol, (short)num, topAnchorRow, (short)num2, num3, (short)num4, num5, (short)num6);
			this.worksheet.AddImage(imageData, imageType, clientSPRC, uniqueImageID);
		}

		// Token: 0x0600689C RID: 26780 RVA: 0x00187FE0 File Offset: 0x001861E0
		internal void CalculateAnchor(Range.Direction direction, double shapeEdge, ushort startIndex, out ushort index, out double value)
		{
			index = startIndex;
			value = 0.0;
			double num = 0.0;
			double num2 = this.GetDimension(direction, index);
			while (Math.Round(shapeEdge - num2, 1) > 0.1)
			{
				num = num2;
				index += 1;
				num2 += this.GetDimension(direction, index);
			}
			if (Math.Round(num2 - num, 1) > 0.0)
			{
				double num3 = shapeEdge - num;
				double num4 = num2 - num;
				double num5 = num3 / num4;
				if (num5 < 0.0)
				{
					num5 = 0.0;
				}
				else if (num5 > 1.0)
				{
					num5 = 1.0;
				}
				int num6 = 0;
				switch (direction)
				{
				case Range.Direction.Horizontal:
					num6 = 1024;
					break;
				case Range.Direction.Vertical:
					num6 = 255;
					break;
				}
				value = num5 * (double)num6;
			}
		}

		// Token: 0x0600689D RID: 26781 RVA: 0x001880C4 File Offset: 0x001862C4
		internal double GetDimension(Range.Direction dir, ushort index)
		{
			Font defaultFont = new Font(this.worksheet.workbook.DefaultFontName, this.worksheet.workbook.DefaultFontSize);
			ExcelConverter excelConverter = new ExcelConverter(defaultFont);
			double result = 0.0;
			switch (dir)
			{
			case Range.Direction.Horizontal:
			{
				double characters = 8.43;
				if (this.worksheet.Columns.Count > (int)index)
				{
					characters = this.worksheet.Columns[(int)index].Width;
				}
				result = excelConverter.CharactersToPixels(characters);
				break;
			}
			case Range.Direction.Vertical:
			{
				double num = 12.75;
				if (this.worksheet.Rows.Count > (int)index)
				{
					num = this.worksheet.Rows[(int)index].Height;
				}
				result = num * 1.3333333333333333;
				break;
			}
			}
			return result;
		}

		// Token: 0x0600689E RID: 26782 RVA: 0x001881A4 File Offset: 0x001863A4
		public override string ToString()
		{
			return string.Format("{{{0},{1}}}{{{2},{3}}},Merged={4},Parent={5}", new object[]
			{
				this.col,
				this.row,
				this.colSpan,
				this.rowSpan,
				this.Merged,
				this.worksheet
			});
		}

		// Token: 0x04001BB4 RID: 7092
		private readonly Worksheet worksheet;

		// Token: 0x04001BB5 RID: 7093
		private readonly int col;

		// Token: 0x04001BB6 RID: 7094
		private readonly int row;

		// Token: 0x04001BB7 RID: 7095
		private readonly int colSpan;

		// Token: 0x04001BB8 RID: 7096
		private readonly int rowSpan;

		// Token: 0x04001BB9 RID: 7097
		private Borders borders;

		// Token: 0x02000AD4 RID: 2772
		public enum Direction
		{
			// Token: 0x04001BBB RID: 7099
			Horizontal,
			// Token: 0x04001BBC RID: 7100
			Vertical
		}
	}
}
