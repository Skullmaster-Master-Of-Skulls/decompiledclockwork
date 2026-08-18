using System;
using System.Collections.Generic;
using iTextSharp.text.error_messages;
using iTextSharp.text.pdf.events;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000211 RID: 529
	public class PdfPTable : ILargeElement, IElement
	{
		// Token: 0x06001434 RID: 5172 RVA: 0x0007394C File Offset: 0x0007294C
		protected PdfPTable()
		{
			bool[] array = new bool[2];
			this.extendLastRow = array;
			this.splitLate = true;
			this.complete = true;
			this.rowCompleted = true;
			base..ctor();
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x000739B4 File Offset: 0x000729B4
		public PdfPTable(float[] relativeWidths)
		{
			bool[] array = new bool[2];
			this.extendLastRow = array;
			this.splitLate = true;
			this.complete = true;
			this.rowCompleted = true;
			base..ctor();
			if (relativeWidths == null)
			{
				throw new ArgumentNullException(MessageLocalization.GetComposedMessage("the.widths.array.in.pdfptable.constructor.can.not.be.null"));
			}
			if (relativeWidths.Length == 0)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("the.widths.array.in.pdfptable.constructor.can.not.have.zero.length"));
			}
			this.relativeWidths = new float[relativeWidths.Length];
			Array.Copy(relativeWidths, 0, this.relativeWidths, 0, relativeWidths.Length);
			this.absoluteWidths = new float[relativeWidths.Length];
			this.CalculateWidths();
			this.currentRow = new PdfPCell[this.absoluteWidths.Length];
			this.keepTogether = false;
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x00073A90 File Offset: 0x00072A90
		public PdfPTable(int numColumns)
		{
			bool[] array = new bool[2];
			this.extendLastRow = array;
			this.splitLate = true;
			this.complete = true;
			this.rowCompleted = true;
			base..ctor();
			if (numColumns <= 0)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("the.number.of.columns.in.pdfptable.constructor.must.be.greater.than.zero"));
			}
			this.relativeWidths = new float[numColumns];
			for (int i = 0; i < numColumns; i++)
			{
				this.relativeWidths[i] = 1f;
			}
			this.absoluteWidths = new float[this.relativeWidths.Length];
			this.CalculateWidths();
			this.currentRow = new PdfPCell[this.absoluteWidths.Length];
			this.keepTogether = false;
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x00073B64 File Offset: 0x00072B64
		public PdfPTable(PdfPTable table)
		{
			bool[] array = new bool[2];
			this.extendLastRow = array;
			this.splitLate = true;
			this.complete = true;
			this.rowCompleted = true;
			base..ctor();
			this.CopyFormat(table);
			int num = 0;
			while (num < this.currentRow.Length && table.currentRow[num] != null)
			{
				this.currentRow[num] = new PdfPCell(table.currentRow[num]);
				num++;
			}
			for (int i = 0; i < table.rows.Count; i++)
			{
				PdfPRow pdfPRow = table.rows[i];
				if (pdfPRow != null)
				{
					pdfPRow = new PdfPRow(pdfPRow);
				}
				this.rows.Add(pdfPRow);
			}
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x00073C3C File Offset: 0x00072C3C
		public static PdfPTable ShallowCopy(PdfPTable table)
		{
			PdfPTable pdfPTable = new PdfPTable();
			pdfPTable.CopyFormat(table);
			return pdfPTable;
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x00073C58 File Offset: 0x00072C58
		protected internal void CopyFormat(PdfPTable sourceTable)
		{
			this.relativeWidths = new float[sourceTable.NumberOfColumns];
			this.absoluteWidths = new float[sourceTable.NumberOfColumns];
			Array.Copy(sourceTable.relativeWidths, 0, this.relativeWidths, 0, this.NumberOfColumns);
			Array.Copy(sourceTable.absoluteWidths, 0, this.absoluteWidths, 0, this.NumberOfColumns);
			this.totalWidth = sourceTable.totalWidth;
			this.totalHeight = sourceTable.totalHeight;
			this.currentRowIdx = 0;
			this.tableEvent = sourceTable.tableEvent;
			this.runDirection = sourceTable.runDirection;
			this.defaultCell = new PdfPCell(sourceTable.defaultCell);
			this.currentRow = new PdfPCell[sourceTable.currentRow.Length];
			this.isColspan = sourceTable.isColspan;
			this.splitRows = sourceTable.splitRows;
			this.spacingAfter = sourceTable.spacingAfter;
			this.spacingBefore = sourceTable.spacingBefore;
			this.headerRows = sourceTable.headerRows;
			this.footerRows = sourceTable.footerRows;
			this.lockedWidth = sourceTable.lockedWidth;
			this.extendLastRow = sourceTable.extendLastRow;
			this.headersInEvent = sourceTable.headersInEvent;
			this.widthPercentage = sourceTable.widthPercentage;
			this.splitLate = sourceTable.splitLate;
			this.skipFirstHeader = sourceTable.skipFirstHeader;
			this.skipLastFooter = sourceTable.skipLastFooter;
			this.horizontalAlignment = sourceTable.horizontalAlignment;
			this.keepTogether = sourceTable.keepTogether;
			this.complete = sourceTable.complete;
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x00073DD4 File Offset: 0x00072DD4
		public void SetWidths(float[] relativeWidths)
		{
			if (relativeWidths.Length != this.NumberOfColumns)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("wrong.number.of.columns"));
			}
			this.relativeWidths = new float[relativeWidths.Length];
			Array.Copy(relativeWidths, 0, this.relativeWidths, 0, relativeWidths.Length);
			this.absoluteWidths = new float[relativeWidths.Length];
			this.totalHeight = 0f;
			this.CalculateWidths();
			this.CalculateHeights(true);
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x00073E44 File Offset: 0x00072E44
		public void SetWidths(int[] relativeWidths)
		{
			float[] array = new float[relativeWidths.Length];
			for (int i = 0; i < relativeWidths.Length; i++)
			{
				array[i] = (float)relativeWidths[i];
			}
			this.SetWidths(array);
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x00073E78 File Offset: 0x00072E78
		protected internal void CalculateWidths()
		{
			if (this.totalWidth <= 0f)
			{
				return;
			}
			float num = 0f;
			int numberOfColumns = this.NumberOfColumns;
			for (int i = 0; i < numberOfColumns; i++)
			{
				num += this.relativeWidths[i];
			}
			for (int j = 0; j < numberOfColumns; j++)
			{
				this.absoluteWidths[j] = this.totalWidth * this.relativeWidths[j] / num;
			}
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x00073EDC File Offset: 0x00072EDC
		public void SetTotalWidth(float[] columnWidth)
		{
			if (columnWidth.Length != this.NumberOfColumns)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("wrong.number.of.columns"));
			}
			this.totalWidth = 0f;
			for (int i = 0; i < columnWidth.Length; i++)
			{
				this.totalWidth += columnWidth[i];
			}
			this.SetWidths(columnWidth);
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x00073F34 File Offset: 0x00072F34
		public void SetWidthPercentage(float[] columnWidth, Rectangle pageSize)
		{
			if (columnWidth.Length != this.NumberOfColumns)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("wrong.number.of.columns"));
			}
			float num = 0f;
			for (int i = 0; i < columnWidth.Length; i++)
			{
				num += columnWidth[i];
			}
			this.widthPercentage = num / (pageSize.Right - pageSize.Left) * 100f;
			this.SetWidths(columnWidth);
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x0600143F RID: 5183 RVA: 0x00073F98 File Offset: 0x00072F98
		// (set) Token: 0x06001440 RID: 5184 RVA: 0x00073FA0 File Offset: 0x00072FA0
		public float TotalWidth
		{
			get
			{
				return this.totalWidth;
			}
			set
			{
				if (this.totalWidth == value)
				{
					return;
				}
				this.totalWidth = value;
				this.totalHeight = 0f;
				this.CalculateWidths();
				this.CalculateHeights(true);
			}
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x00073FCC File Offset: 0x00072FCC
		public float CalculateHeights(bool firsttime)
		{
			if (this.totalWidth <= 0f)
			{
				return 0f;
			}
			this.totalHeight = 0f;
			for (int i = 0; i < this.rows.Count; i++)
			{
				this.totalHeight += this.GetRowHeight(i, firsttime);
			}
			return this.totalHeight;
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x00074028 File Offset: 0x00073028
		public void CalculateHeightsFast()
		{
			this.CalculateHeights(false);
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x00074034 File Offset: 0x00073034
		public void ResetColumnCount(int newColCount)
		{
			if (newColCount <= 0)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("the.number.of.columns.in.pdfptable.constructor.must.be.greater.than.zero"));
			}
			this.relativeWidths = new float[newColCount];
			for (int i = 0; i < newColCount; i++)
			{
				this.relativeWidths[i] = 1f;
			}
			this.absoluteWidths = new float[this.relativeWidths.Length];
			this.CalculateWidths();
			this.currentRow = new PdfPCell[this.absoluteWidths.Length];
			this.totalHeight = 0f;
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x000740B1 File Offset: 0x000730B1
		public PdfPCell DefaultCell
		{
			get
			{
				return this.defaultCell;
			}
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x000740BC File Offset: 0x000730BC
		public void AddCell(PdfPCell cell)
		{
			this.rowCompleted = false;
			PdfPCell pdfPCell = new PdfPCell(cell);
			int num = pdfPCell.Colspan;
			num = Math.Max(num, 1);
			num = Math.Min(num, this.currentRow.Length - this.currentRowIdx);
			pdfPCell.Colspan = num;
			if (num != 1)
			{
				this.isColspan = true;
			}
			if (pdfPCell.RunDirection == 0)
			{
				pdfPCell.RunDirection = this.runDirection;
			}
			this.SkipColsWithRowspanAbove();
			bool flag = false;
			if (this.currentRowIdx < this.currentRow.Length)
			{
				this.currentRow[this.currentRowIdx] = pdfPCell;
				this.currentRowIdx += num;
				flag = true;
			}
			this.SkipColsWithRowspanAbove();
			while (this.currentRowIdx >= this.currentRow.Length)
			{
				int numberOfColumns = this.NumberOfColumns;
				if (this.runDirection == 3)
				{
					PdfPCell[] array = new PdfPCell[numberOfColumns];
					int num2 = this.currentRow.Length;
					for (int i = 0; i < this.currentRow.Length; i++)
					{
						PdfPCell pdfPCell2 = this.currentRow[i];
						int colspan = pdfPCell2.Colspan;
						num2 -= colspan;
						array[num2] = pdfPCell2;
						i += colspan - 1;
					}
					this.currentRow = array;
				}
				PdfPRow pdfPRow = new PdfPRow(this.currentRow);
				if (this.totalWidth > 0f)
				{
					pdfPRow.SetWidths(this.absoluteWidths);
					this.totalHeight += pdfPRow.MaxHeights;
				}
				this.rows.Add(pdfPRow);
				this.currentRow = new PdfPCell[numberOfColumns];
				this.currentRowIdx = 0;
				this.SkipColsWithRowspanAbove();
				this.rowCompleted = true;
			}
			if (!flag)
			{
				this.currentRow[this.currentRowIdx] = pdfPCell;
				this.currentRowIdx += num;
			}
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x00074270 File Offset: 0x00073270
		private void SkipColsWithRowspanAbove()
		{
			int num = 1;
			if (this.runDirection == 3)
			{
				num = -1;
			}
			while (this.RowSpanAbove(this.rows.Count, this.currentRowIdx))
			{
				this.currentRowIdx += num;
			}
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x000742B4 File Offset: 0x000732B4
		internal PdfPCell CellAt(int row, int col)
		{
			PdfPCell[] cells = this.rows[row].GetCells();
			for (int i = 0; i < cells.Length; i++)
			{
				if (cells[i] != null && col >= i && col < i + cells[i].Colspan)
				{
					return cells[i];
				}
			}
			return null;
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x000742FC File Offset: 0x000732FC
		internal bool RowSpanAbove(int currRow, int currCol)
		{
			if (currCol >= this.NumberOfColumns || currCol < 0 || currRow == 0)
			{
				return false;
			}
			int num = currRow - 1;
			if (this.rows[num] == null)
			{
				return false;
			}
			PdfPCell pdfPCell = this.CellAt(num, currCol);
			while (pdfPCell == null && num > 0)
			{
				if (this.rows[--num] == null)
				{
					return false;
				}
				pdfPCell = this.CellAt(num, currCol);
			}
			int num2 = currRow - num;
			if (pdfPCell.Rowspan == 1 && num2 > 1)
			{
				int num3 = currCol - 1;
				PdfPRow pdfPRow = this.rows[num + 1];
				num2--;
				pdfPCell = pdfPRow.GetCells()[num3];
				while (pdfPCell == null && num3 > 0)
				{
					pdfPCell = pdfPRow.GetCells()[--num3];
				}
			}
			return pdfPCell != null && pdfPCell.Rowspan > num2;
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x000743BC File Offset: 0x000733BC
		public void AddCell(string text)
		{
			this.AddCell(new Phrase(text));
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x000743CA File Offset: 0x000733CA
		public void AddCell(PdfPTable table)
		{
			this.defaultCell.Table = table;
			this.AddCell(this.defaultCell);
			this.defaultCell.Table = null;
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x000743F0 File Offset: 0x000733F0
		public void AddCell(Image image)
		{
			this.defaultCell.Image = image;
			this.AddCell(this.defaultCell);
			this.defaultCell.Image = null;
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x00074416 File Offset: 0x00073416
		public void AddCell(Phrase phrase)
		{
			this.defaultCell.Phrase = phrase;
			this.AddCell(this.defaultCell);
			this.defaultCell.Phrase = null;
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x0007443C File Offset: 0x0007343C
		public float WriteSelectedRows(int rowStart, int rowEnd, float xPos, float yPos, PdfContentByte[] canvases)
		{
			return this.WriteSelectedRows(0, -1, rowStart, rowEnd, xPos, yPos, canvases);
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x00074450 File Offset: 0x00073450
		public float WriteSelectedRows(int colStart, int colEnd, int rowStart, int rowEnd, float xPos, float yPos, PdfContentByte[] canvases)
		{
			if (this.totalWidth <= 0f)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("the.table.width.must.be.greater.than.zero"));
			}
			int count = this.rows.Count;
			if (rowStart < 0)
			{
				rowStart = 0;
			}
			if (rowEnd < 0)
			{
				rowEnd = count;
			}
			else
			{
				rowEnd = Math.Min(rowEnd, count);
			}
			if (rowStart >= rowEnd)
			{
				return yPos;
			}
			int numberOfColumns = this.NumberOfColumns;
			if (colStart < 0)
			{
				colStart = 0;
			}
			else
			{
				colStart = Math.Min(colStart, numberOfColumns);
			}
			if (colEnd < 0)
			{
				colEnd = numberOfColumns;
			}
			else
			{
				colEnd = Math.Min(colEnd, numberOfColumns);
			}
			float num = yPos;
			for (int i = rowStart; i < rowEnd; i++)
			{
				PdfPRow pdfPRow = this.rows[i];
				if (pdfPRow != null)
				{
					pdfPRow.WriteCells(colStart, colEnd, xPos, yPos, canvases);
					yPos -= pdfPRow.MaxHeights;
				}
			}
			if (this.tableEvent != null && colStart == 0 && colEnd == numberOfColumns)
			{
				float[] array = new float[rowEnd - rowStart + 1];
				array[0] = num;
				for (int j = rowStart; j < rowEnd; j++)
				{
					PdfPRow pdfPRow2 = this.rows[j];
					float num2 = 0f;
					if (pdfPRow2 != null)
					{
						num2 = pdfPRow2.MaxHeights;
					}
					array[j - rowStart + 1] = array[j - rowStart] - num2;
				}
				this.tableEvent.TableLayout(this, this.GetEventWidths(xPos, rowStart, rowEnd, this.headersInEvent), array, this.headersInEvent ? this.headerRows : 0, rowStart, canvases);
			}
			return yPos;
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x000745B7 File Offset: 0x000735B7
		public float WriteSelectedRows(int rowStart, int rowEnd, float xPos, float yPos, PdfContentByte canvas)
		{
			return this.WriteSelectedRows(0, -1, rowStart, rowEnd, xPos, yPos, canvas);
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x000745C8 File Offset: 0x000735C8
		public float WriteSelectedRows(int colStart, int colEnd, int rowStart, int rowEnd, float xPos, float yPos, PdfContentByte canvas)
		{
			int numberOfColumns = this.NumberOfColumns;
			if (colStart < 0)
			{
				colStart = 0;
			}
			else
			{
				colStart = Math.Min(colStart, numberOfColumns);
			}
			if (colEnd < 0)
			{
				colEnd = numberOfColumns;
			}
			else
			{
				colEnd = Math.Min(colEnd, numberOfColumns);
			}
			bool flag = colStart != 0 || colEnd != numberOfColumns;
			if (flag)
			{
				float num = 0f;
				for (int i = colStart; i < colEnd; i++)
				{
					num += this.absoluteWidths[i];
				}
				canvas.SaveState();
				float num2 = (float)((colStart == 0) ? 10000 : 0);
				float num3 = (float)((colEnd == numberOfColumns) ? 10000 : 0);
				canvas.Rectangle(xPos - num2, -10000f, num + num2 + num3, 20000f);
				canvas.Clip();
				canvas.NewPath();
			}
			PdfContentByte[] canvases = PdfPTable.BeginWritingRows(canvas);
			float result = this.WriteSelectedRows(colStart, colEnd, rowStart, rowEnd, xPos, yPos, canvases);
			PdfPTable.EndWritingRows(canvases);
			if (flag)
			{
				canvas.RestoreState();
			}
			return result;
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x000746AC File Offset: 0x000736AC
		public static PdfContentByte[] BeginWritingRows(PdfContentByte canvas)
		{
			return new PdfContentByte[]
			{
				canvas,
				canvas.Duplicate,
				canvas.Duplicate,
				canvas.Duplicate
			};
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x000746E0 File Offset: 0x000736E0
		public static void EndWritingRows(PdfContentByte[] canvases)
		{
			PdfContentByte pdfContentByte = canvases[0];
			pdfContentByte.SaveState();
			pdfContentByte.Add(canvases[1]);
			pdfContentByte.RestoreState();
			pdfContentByte.SaveState();
			pdfContentByte.SetLineCap(2);
			pdfContentByte.ResetRGBColorStroke();
			pdfContentByte.Add(canvases[2]);
			pdfContentByte.RestoreState();
			pdfContentByte.Add(canvases[3]);
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001453 RID: 5203 RVA: 0x00074731 File Offset: 0x00073731
		public int Size
		{
			get
			{
				return this.rows.Count;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x0007473E File Offset: 0x0007373E
		public float TotalHeight
		{
			get
			{
				return this.totalHeight;
			}
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x00074746 File Offset: 0x00073746
		public float GetRowHeight(int idx)
		{
			return this.GetRowHeight(idx, false);
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x00074750 File Offset: 0x00073750
		public float GetRowHeight(int idx, bool firsttime)
		{
			if (this.totalWidth <= 0f || idx < 0 || idx >= this.rows.Count)
			{
				return 0f;
			}
			PdfPRow pdfPRow = this.rows[idx];
			if (pdfPRow == null)
			{
				return 0f;
			}
			if (firsttime)
			{
				pdfPRow.SetWidths(this.absoluteWidths);
			}
			float num = pdfPRow.MaxHeights;
			for (int i = 0; i < this.relativeWidths.Length; i++)
			{
				if (this.RowSpanAbove(idx, i))
				{
					int j = 1;
					while (this.RowSpanAbove(idx - j, i))
					{
						j++;
					}
					PdfPRow pdfPRow2 = this.rows[idx - j];
					PdfPCell pdfPCell = pdfPRow2.GetCells()[i];
					float num2 = 0f;
					if (pdfPCell != null && pdfPCell.Rowspan == j + 1)
					{
						num2 = pdfPCell.GetMaxHeight();
						while (j > 0)
						{
							num2 -= this.GetRowHeight(idx - j);
							j--;
						}
					}
					if (num2 > num)
					{
						num = num2;
					}
				}
			}
			pdfPRow.MaxHeights = num;
			return num;
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x00074854 File Offset: 0x00073854
		public float GetRowspanHeight(int rowIndex, int cellIndex)
		{
			if (this.totalWidth <= 0f || rowIndex < 0 || rowIndex >= this.rows.Count)
			{
				return 0f;
			}
			PdfPRow pdfPRow = this.rows[rowIndex];
			if (pdfPRow == null || cellIndex >= pdfPRow.GetCells().Length)
			{
				return 0f;
			}
			PdfPCell pdfPCell = pdfPRow.GetCells()[cellIndex];
			if (pdfPCell == null)
			{
				return 0f;
			}
			float num = 0f;
			for (int i = 0; i < pdfPCell.Rowspan; i++)
			{
				num += this.GetRowHeight(rowIndex + i);
			}
			return num;
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x000748E0 File Offset: 0x000738E0
		public float HeaderHeight
		{
			get
			{
				float num = 0f;
				int num2 = Math.Min(this.rows.Count, this.headerRows);
				for (int i = 0; i < num2; i++)
				{
					PdfPRow pdfPRow = this.rows[i];
					if (pdfPRow != null)
					{
						num += pdfPRow.MaxHeights;
					}
				}
				return num;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x00074930 File Offset: 0x00073930
		public float FooterHeight
		{
			get
			{
				float num = 0f;
				int num2 = Math.Max(0, this.headerRows - this.footerRows);
				int num3 = Math.Min(this.rows.Count, this.headerRows);
				for (int i = num2; i < num3; i++)
				{
					PdfPRow pdfPRow = this.rows[i];
					if (pdfPRow != null)
					{
						num += pdfPRow.MaxHeights;
					}
				}
				return num;
			}
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x00074998 File Offset: 0x00073998
		public bool DeleteRow(int rowNumber)
		{
			if (rowNumber < 0 || rowNumber >= this.rows.Count)
			{
				return false;
			}
			if (this.totalWidth > 0f)
			{
				PdfPRow pdfPRow = this.rows[rowNumber];
				if (pdfPRow != null)
				{
					this.totalHeight -= pdfPRow.MaxHeights;
				}
			}
			this.rows.RemoveAt(rowNumber);
			if (rowNumber < this.headerRows)
			{
				this.headerRows--;
				if (rowNumber >= this.headerRows - this.footerRows)
				{
					this.footerRows--;
				}
			}
			return true;
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x00074A2B File Offset: 0x00073A2B
		public bool DeleteLastRow()
		{
			return this.DeleteRow(this.rows.Count - 1);
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x00074A40 File Offset: 0x00073A40
		public void DeleteBodyRows()
		{
			List<PdfPRow> list = new List<PdfPRow>();
			for (int i = 0; i < this.headerRows; i++)
			{
				list.Add(this.rows[i]);
			}
			this.rows = list;
			this.totalHeight = 0f;
			if (this.totalWidth > 0f)
			{
				this.totalHeight = this.HeaderHeight;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x00074AA1 File Offset: 0x00073AA1
		public int NumberOfColumns
		{
			get
			{
				return this.relativeWidths.Length;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x0600145E RID: 5214 RVA: 0x00074AAB File Offset: 0x00073AAB
		// (set) Token: 0x0600145F RID: 5215 RVA: 0x00074AB3 File Offset: 0x00073AB3
		public int HeaderRows
		{
			get
			{
				return this.headerRows;
			}
			set
			{
				this.headerRows = value;
				if (this.headerRows < 0)
				{
					this.headerRows = 0;
				}
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001460 RID: 5216 RVA: 0x00074ACC File Offset: 0x00073ACC
		// (set) Token: 0x06001461 RID: 5217 RVA: 0x00074AD4 File Offset: 0x00073AD4
		public int FooterRows
		{
			get
			{
				return this.footerRows;
			}
			set
			{
				this.footerRows = value;
				if (this.footerRows < 0)
				{
					this.footerRows = 0;
				}
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001462 RID: 5218 RVA: 0x00074AED File Offset: 0x00073AED
		public List<Chunk> Chunks
		{
			get
			{
				return new List<Chunk>();
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001463 RID: 5219 RVA: 0x00074AF4 File Offset: 0x00073AF4
		public int Type
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x00074AF8 File Offset: 0x00073AF8
		public bool IsContent()
		{
			return true;
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x00074AFB File Offset: 0x00073AFB
		public bool IsNestable()
		{
			return true;
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x00074B00 File Offset: 0x00073B00
		public bool Process(IElementListener listener)
		{
			bool result;
			try
			{
				result = listener.Add(this);
			}
			catch (DocumentException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x00074B30 File Offset: 0x00073B30
		// (set) Token: 0x06001468 RID: 5224 RVA: 0x00074B38 File Offset: 0x00073B38
		public float WidthPercentage
		{
			get
			{
				return this.widthPercentage;
			}
			set
			{
				this.widthPercentage = value;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001469 RID: 5225 RVA: 0x00074B41 File Offset: 0x00073B41
		// (set) Token: 0x0600146A RID: 5226 RVA: 0x00074B49 File Offset: 0x00073B49
		public int HorizontalAlignment
		{
			get
			{
				return this.horizontalAlignment;
			}
			set
			{
				this.horizontalAlignment = value;
			}
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x00074B52 File Offset: 0x00073B52
		public PdfPRow GetRow(int idx)
		{
			return this.rows[idx];
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x00074B60 File Offset: 0x00073B60
		public List<PdfPRow> Rows
		{
			get
			{
				return this.rows;
			}
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x00074B68 File Offset: 0x00073B68
		public List<PdfPRow> GetRows(int start, int end)
		{
			List<PdfPRow> list = new List<PdfPRow>();
			if (start < 0 || end > this.Size)
			{
				return list;
			}
			PdfPRow pdfPRow = this.AdjustCellsInRow(start, end);
			int i = 0;
			while (i < this.NumberOfColumns)
			{
				int num = start;
				while (this.RowSpanAbove(num--, i))
				{
					PdfPRow row = this.GetRow(num);
					if (row != null)
					{
						PdfPCell pdfPCell = row.GetCells()[i];
						if (pdfPCell != null)
						{
							pdfPRow.GetCells()[i] = new PdfPCell(pdfPCell);
							float num2 = 0f;
							int num3 = Math.Min(num + pdfPCell.Rowspan, end);
							for (int j = start + 1; j < num3; j++)
							{
								num2 += this.GetRowHeight(j);
							}
							pdfPRow.SetExtraHeight(i, num2);
							float height = this.GetRowspanHeight(num, i) - this.GetRowHeight(start) - num2;
							pdfPRow.GetCells()[i].ConsumeHeight(height);
						}
					}
				}
				PdfPCell pdfPCell2 = pdfPRow.GetCells()[i];
				if (pdfPCell2 == null)
				{
					i++;
				}
				else
				{
					i += pdfPCell2.Colspan;
				}
			}
			list.Add(pdfPRow);
			for (int k = start + 1; k < end; k++)
			{
				list.Add(this.AdjustCellsInRow(k, end));
			}
			return list;
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x00074C9C File Offset: 0x00073C9C
		protected PdfPRow AdjustCellsInRow(int start, int end)
		{
			PdfPRow pdfPRow = new PdfPRow(this.GetRow(start));
			pdfPRow.InitExtraHeights();
			PdfPCell[] cells = pdfPRow.GetCells();
			for (int i = 0; i < cells.Length; i++)
			{
				PdfPCell pdfPCell = cells[i];
				if (pdfPCell != null && pdfPCell.Rowspan != 1)
				{
					int num = Math.Min(end, start + pdfPCell.Rowspan);
					float num2 = 0f;
					for (int j = start + 1; j < num; j++)
					{
						num2 += this.GetRowHeight(j);
					}
					pdfPRow.SetExtraHeight(i, num2);
				}
			}
			return pdfPRow;
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x0600146F RID: 5231 RVA: 0x00074D22 File Offset: 0x00073D22
		// (set) Token: 0x06001470 RID: 5232 RVA: 0x00074D2C File Offset: 0x00073D2C
		public IPdfPTableEvent TableEvent
		{
			get
			{
				return this.tableEvent;
			}
			set
			{
				if (value == null)
				{
					this.tableEvent = null;
					return;
				}
				if (this.tableEvent == null)
				{
					this.tableEvent = value;
					return;
				}
				if (this.tableEvent is PdfPTableEventForwarder)
				{
					((PdfPTableEventForwarder)this.tableEvent).AddTableEvent(value);
					return;
				}
				PdfPTableEventForwarder pdfPTableEventForwarder = new PdfPTableEventForwarder();
				pdfPTableEventForwarder.AddTableEvent(this.tableEvent);
				pdfPTableEventForwarder.AddTableEvent(value);
				this.tableEvent = pdfPTableEventForwarder;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001471 RID: 5233 RVA: 0x00074D93 File Offset: 0x00073D93
		public float[] AbsoluteWidths
		{
			get
			{
				return this.absoluteWidths;
			}
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x00074D9C File Offset: 0x00073D9C
		internal float[][] GetEventWidths(float xPos, int firstRow, int lastRow, bool includeHeaders)
		{
			if (includeHeaders)
			{
				firstRow = Math.Max(firstRow, this.headerRows);
				lastRow = Math.Max(lastRow, this.headerRows);
			}
			float[][] array = new float[(includeHeaders ? this.headerRows : 0) + lastRow - firstRow][];
			if (this.isColspan)
			{
				int num = 0;
				if (includeHeaders)
				{
					for (int i = 0; i < this.headerRows; i++)
					{
						PdfPRow pdfPRow = this.rows[i];
						if (pdfPRow == null)
						{
							num++;
						}
						else
						{
							array[num++] = pdfPRow.GetEventWidth(xPos);
						}
					}
				}
				while (firstRow < lastRow)
				{
					PdfPRow pdfPRow2 = this.rows[firstRow];
					if (pdfPRow2 == null)
					{
						num++;
					}
					else
					{
						array[num++] = pdfPRow2.GetEventWidth(xPos);
					}
					firstRow++;
				}
			}
			else
			{
				int numberOfColumns = this.NumberOfColumns;
				float[] array2 = new float[numberOfColumns + 1];
				array2[0] = xPos;
				for (int j = 0; j < numberOfColumns; j++)
				{
					array2[j + 1] = array2[j] + this.absoluteWidths[j];
				}
				for (int k = 0; k < array.Length; k++)
				{
					array[k] = array2;
				}
			}
			return array;
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x00074EAF File Offset: 0x00073EAF
		// (set) Token: 0x06001474 RID: 5236 RVA: 0x00074EB7 File Offset: 0x00073EB7
		public bool SkipFirstHeader
		{
			get
			{
				return this.skipFirstHeader;
			}
			set
			{
				this.skipFirstHeader = value;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001475 RID: 5237 RVA: 0x00074EC0 File Offset: 0x00073EC0
		// (set) Token: 0x06001476 RID: 5238 RVA: 0x00074EC8 File Offset: 0x00073EC8
		public bool SkipLastFooter
		{
			get
			{
				return this.skipLastFooter;
			}
			set
			{
				this.skipLastFooter = value;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001477 RID: 5239 RVA: 0x00074ED1 File Offset: 0x00073ED1
		// (set) Token: 0x06001478 RID: 5240 RVA: 0x00074EDC File Offset: 0x00073EDC
		public int RunDirection
		{
			get
			{
				return this.runDirection;
			}
			set
			{
				switch (value)
				{
				case 0:
				case 1:
				case 2:
				case 3:
					this.runDirection = value;
					return;
				default:
					throw new ArgumentException(MessageLocalization.GetComposedMessage("invalid.run.direction.1", this.runDirection));
				}
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x00074F25 File Offset: 0x00073F25
		// (set) Token: 0x0600147A RID: 5242 RVA: 0x00074F2D File Offset: 0x00073F2D
		public bool LockedWidth
		{
			get
			{
				return this.lockedWidth;
			}
			set
			{
				this.lockedWidth = value;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x0600147B RID: 5243 RVA: 0x00074F36 File Offset: 0x00073F36
		// (set) Token: 0x0600147C RID: 5244 RVA: 0x00074F3E File Offset: 0x00073F3E
		public bool SplitRows
		{
			get
			{
				return this.splitRows;
			}
			set
			{
				this.splitRows = value;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x0600147D RID: 5245 RVA: 0x00074F47 File Offset: 0x00073F47
		// (set) Token: 0x0600147E RID: 5246 RVA: 0x00074F4F File Offset: 0x00073F4F
		public float SpacingBefore
		{
			get
			{
				return this.spacingBefore;
			}
			set
			{
				this.spacingBefore = value;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x0600147F RID: 5247 RVA: 0x00074F58 File Offset: 0x00073F58
		// (set) Token: 0x06001480 RID: 5248 RVA: 0x00074F60 File Offset: 0x00073F60
		public float SpacingAfter
		{
			get
			{
				return this.spacingAfter;
			}
			set
			{
				this.spacingAfter = value;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06001481 RID: 5249 RVA: 0x00074F69 File Offset: 0x00073F69
		// (set) Token: 0x06001482 RID: 5250 RVA: 0x00074F73 File Offset: 0x00073F73
		public bool ExtendLastRow
		{
			get
			{
				return this.extendLastRow[0];
			}
			set
			{
				this.extendLastRow[0] = value;
				this.extendLastRow[1] = value;
			}
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x00074F87 File Offset: 0x00073F87
		public void SetExtendLastRow(bool extendLastRows, bool extendFinalRow)
		{
			this.extendLastRow[0] = extendLastRows;
			this.extendLastRow[1] = extendFinalRow;
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x00074F9B File Offset: 0x00073F9B
		public bool IsExtendLastRow(bool newPageFollows)
		{
			if (newPageFollows)
			{
				return this.extendLastRow[0];
			}
			return this.extendLastRow[1];
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x00074FB1 File Offset: 0x00073FB1
		// (set) Token: 0x06001486 RID: 5254 RVA: 0x00074FB9 File Offset: 0x00073FB9
		public bool HeadersInEvent
		{
			get
			{
				return this.headersInEvent;
			}
			set
			{
				this.headersInEvent = value;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001487 RID: 5255 RVA: 0x00074FC2 File Offset: 0x00073FC2
		// (set) Token: 0x06001488 RID: 5256 RVA: 0x00074FCA File Offset: 0x00073FCA
		public bool SplitLate
		{
			get
			{
				return this.splitLate;
			}
			set
			{
				this.splitLate = value;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x0600148A RID: 5258 RVA: 0x00074FDC File Offset: 0x00073FDC
		// (set) Token: 0x06001489 RID: 5257 RVA: 0x00074FD3 File Offset: 0x00073FD3
		public bool KeepTogether
		{
			get
			{
				return this.keepTogether;
			}
			set
			{
				this.keepTogether = value;
			}
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x00074FE4 File Offset: 0x00073FE4
		public void CompleteRow()
		{
			while (!this.rowCompleted)
			{
				this.AddCell(this.defaultCell);
			}
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x00074FFC File Offset: 0x00073FFC
		public void FlushContent()
		{
			this.DeleteBodyRows();
			this.SkipFirstHeader = true;
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x0600148D RID: 5261 RVA: 0x0007500B File Offset: 0x0007400B
		// (set) Token: 0x0600148E RID: 5262 RVA: 0x00075013 File Offset: 0x00074013
		public bool ElementComplete
		{
			get
			{
				return this.complete;
			}
			set
			{
				this.complete = value;
			}
		}

		// Token: 0x04000DF0 RID: 3568
		public const int BASECANVAS = 0;

		// Token: 0x04000DF1 RID: 3569
		public const int BACKGROUNDCANVAS = 1;

		// Token: 0x04000DF2 RID: 3570
		public const int LINECANVAS = 2;

		// Token: 0x04000DF3 RID: 3571
		public const int TEXTCANVAS = 3;

		// Token: 0x04000DF4 RID: 3572
		protected List<PdfPRow> rows = new List<PdfPRow>();

		// Token: 0x04000DF5 RID: 3573
		protected float totalHeight;

		// Token: 0x04000DF6 RID: 3574
		protected PdfPCell[] currentRow;

		// Token: 0x04000DF7 RID: 3575
		protected int currentRowIdx;

		// Token: 0x04000DF8 RID: 3576
		protected PdfPCell defaultCell = new PdfPCell(null);

		// Token: 0x04000DF9 RID: 3577
		protected float totalWidth;

		// Token: 0x04000DFA RID: 3578
		protected float[] relativeWidths;

		// Token: 0x04000DFB RID: 3579
		protected float[] absoluteWidths;

		// Token: 0x04000DFC RID: 3580
		protected IPdfPTableEvent tableEvent;

		// Token: 0x04000DFD RID: 3581
		protected int headerRows;

		// Token: 0x04000DFE RID: 3582
		protected float widthPercentage = 80f;

		// Token: 0x04000DFF RID: 3583
		private int horizontalAlignment = 1;

		// Token: 0x04000E00 RID: 3584
		private bool skipFirstHeader;

		// Token: 0x04000E01 RID: 3585
		private bool skipLastFooter;

		// Token: 0x04000E02 RID: 3586
		protected bool isColspan;

		// Token: 0x04000E03 RID: 3587
		protected int runDirection;

		// Token: 0x04000E04 RID: 3588
		private bool lockedWidth;

		// Token: 0x04000E05 RID: 3589
		private bool splitRows = true;

		// Token: 0x04000E06 RID: 3590
		protected float spacingBefore;

		// Token: 0x04000E07 RID: 3591
		protected float spacingAfter;

		// Token: 0x04000E08 RID: 3592
		private bool[] extendLastRow;

		// Token: 0x04000E09 RID: 3593
		private bool headersInEvent;

		// Token: 0x04000E0A RID: 3594
		private bool splitLate;

		// Token: 0x04000E0B RID: 3595
		private bool keepTogether;

		// Token: 0x04000E0C RID: 3596
		protected bool complete;

		// Token: 0x04000E0D RID: 3597
		private int footerRows;

		// Token: 0x04000E0E RID: 3598
		protected bool rowCompleted;
	}
}
