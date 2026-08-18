using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x020001C9 RID: 457
	public class PdfPRow
	{
		// Token: 0x060011D4 RID: 4564 RVA: 0x0006665C File Offset: 0x0006565C
		public PdfPRow(PdfPCell[] cells)
		{
			this.cells = cells;
			this.widths = new float[cells.Length];
			this.InitExtraHeights();
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x00066680 File Offset: 0x00065680
		public PdfPRow(PdfPRow row)
		{
			this.maxHeight = row.maxHeight;
			this.calculated = row.calculated;
			this.cells = new PdfPCell[row.cells.Length];
			for (int i = 0; i < this.cells.Length; i++)
			{
				if (row.cells[i] != null)
				{
					this.cells[i] = new PdfPCell(row.cells[i]);
				}
			}
			this.widths = new float[this.cells.Length];
			Array.Copy(row.widths, 0, this.widths, 0, this.cells.Length);
			this.InitExtraHeights();
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x00066724 File Offset: 0x00065724
		public bool SetWidths(float[] widths)
		{
			if (widths.Length != this.cells.Length)
			{
				return false;
			}
			Array.Copy(widths, 0, this.widths, 0, this.cells.Length);
			float num = 0f;
			this.calculated = false;
			for (int i = 0; i < widths.Length; i++)
			{
				PdfPCell pdfPCell = this.cells[i];
				if (pdfPCell == null)
				{
					num += widths[i];
				}
				else
				{
					pdfPCell.Left = num;
					int num2 = i + pdfPCell.Colspan;
					while (i < num2)
					{
						num += widths[i];
						i++;
					}
					i--;
					pdfPCell.Right = num;
					pdfPCell.Top = 0f;
				}
			}
			return true;
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x000667BC File Offset: 0x000657BC
		public void InitExtraHeights()
		{
			this.extraHeights = new float[this.cells.Length];
			for (int i = 0; i < this.extraHeights.Length; i++)
			{
				this.extraHeights[i] = 0f;
			}
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x000667FC File Offset: 0x000657FC
		public void SetExtraHeight(int cell, float height)
		{
			if (cell < 0 || cell >= this.cells.Length)
			{
				return;
			}
			this.extraHeights[cell] = height;
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x00066818 File Offset: 0x00065818
		public float CalculateHeights()
		{
			this.maxHeight = 0f;
			for (int i = 0; i < this.cells.Length; i++)
			{
				PdfPCell pdfPCell = this.cells[i];
				if (pdfPCell != null)
				{
					float num = pdfPCell.GetMaxHeight();
					if (num > this.maxHeight && pdfPCell.Rowspan == 1)
					{
						this.maxHeight = num;
					}
				}
			}
			this.calculated = true;
			return this.maxHeight;
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x00066884 File Offset: 0x00065884
		public void WriteBorderAndBackground(float xPos, float yPos, float currentMaxHeight, PdfPCell cell, PdfContentByte[] canvases)
		{
			BaseColor backgroundColor = cell.BackgroundColor;
			if (backgroundColor != null || cell.HasBorders())
			{
				float num = cell.Right + xPos;
				float num2 = cell.Top + yPos;
				float num3 = cell.Left + xPos;
				float num4 = num2 - currentMaxHeight;
				if (backgroundColor != null)
				{
					PdfContentByte pdfContentByte = canvases[1];
					pdfContentByte.SetColorFill(backgroundColor);
					pdfContentByte.Rectangle(num3, num4, num - num3, num2 - num4);
					pdfContentByte.Fill();
				}
				if (cell.HasBorders())
				{
					Rectangle rectangle = new Rectangle(num3, num4, num, num2);
					rectangle.CloneNonPositionParameters(cell);
					rectangle.BackgroundColor = null;
					PdfContentByte pdfContentByte2 = canvases[2];
					pdfContentByte2.Rectangle(rectangle);
				}
			}
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x0006692C File Offset: 0x0006592C
		protected void SaveAndRotateCanvases(PdfContentByte[] canvases, float a, float b, float c, float d, float e, float f)
		{
			int num = 4;
			if (this.canvasesPos == null)
			{
				this.canvasesPos = new int[num * 2];
			}
			for (int i = 0; i < num; i++)
			{
				ByteBuffer internalBuffer = canvases[i].InternalBuffer;
				this.canvasesPos[i * 2] = internalBuffer.Size;
				canvases[i].SaveState();
				canvases[i].ConcatCTM(a, b, c, d, e, f);
				this.canvasesPos[i * 2 + 1] = internalBuffer.Size;
			}
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x000669A4 File Offset: 0x000659A4
		protected void RestoreCanvases(PdfContentByte[] canvases)
		{
			int num = 4;
			for (int i = 0; i < num; i++)
			{
				ByteBuffer internalBuffer = canvases[i].InternalBuffer;
				int size = internalBuffer.Size;
				canvases[i].RestoreState();
				if (size == this.canvasesPos[i * 2 + 1])
				{
					internalBuffer.Size = this.canvasesPos[i * 2];
				}
			}
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x000669F6 File Offset: 0x000659F6
		public static float SetColumn(ColumnText ct, float left, float bottom, float right, float top)
		{
			if (left > right)
			{
				right = left;
			}
			if (bottom > top)
			{
				top = bottom;
			}
			ct.SetSimpleColumn(left, bottom, right, top);
			return top;
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x00066A14 File Offset: 0x00065A14
		public void WriteCells(int colStart, int colEnd, float xPos, float yPos, PdfContentByte[] canvases)
		{
			if (!this.calculated)
			{
				this.CalculateHeights();
			}
			if (colEnd < 0)
			{
				colEnd = this.cells.Length;
			}
			else
			{
				colEnd = Math.Min(colEnd, this.cells.Length);
			}
			if (colStart < 0)
			{
				colStart = 0;
			}
			if (colStart >= colEnd)
			{
				return;
			}
			int num = colStart;
			while (num >= 0 && this.cells[num] == null)
			{
				if (num > 0)
				{
					xPos -= this.widths[num - 1];
				}
				num--;
			}
			if (num < 0)
			{
				num = 0;
			}
			if (this.cells[num] != null)
			{
				xPos -= this.cells[num].Left;
			}
			for (int i = num; i < colEnd; i++)
			{
				PdfPCell pdfPCell = this.cells[i];
				if (pdfPCell != null)
				{
					float num2 = this.maxHeight + this.extraHeights[i];
					this.WriteBorderAndBackground(xPos, yPos, num2, pdfPCell, canvases);
					Image image = pdfPCell.Image;
					float num3 = pdfPCell.Top + yPos - pdfPCell.EffectivePaddingTop;
					if (pdfPCell.Height <= num2)
					{
						switch (pdfPCell.VerticalAlignment)
						{
						case 5:
							num3 = pdfPCell.Top + yPos + (pdfPCell.Height - num2) / 2f - pdfPCell.EffectivePaddingTop;
							break;
						case 6:
							num3 = pdfPCell.Top + yPos - num2 + pdfPCell.Height - pdfPCell.EffectivePaddingTop;
							break;
						}
					}
					if (image != null)
					{
						if (pdfPCell.Rotation != 0)
						{
							image = Image.GetInstance(image);
							image.Rotation = image.GetImageRotation() + (float)((double)pdfPCell.Rotation * 3.141592653589793 / 180.0);
						}
						bool flag = false;
						if (pdfPCell.Height > num2)
						{
							image.ScalePercent(100f);
							float num4 = (num2 - pdfPCell.EffectivePaddingTop - pdfPCell.EffectivePaddingBottom) / image.ScaledHeight;
							image.ScalePercent(num4 * 100f);
							flag = true;
						}
						float absoluteX = pdfPCell.Left + xPos + pdfPCell.EffectivePaddingLeft;
						if (flag)
						{
							switch (pdfPCell.HorizontalAlignment)
							{
							case 1:
								absoluteX = xPos + (pdfPCell.Left + pdfPCell.EffectivePaddingLeft + pdfPCell.Right - pdfPCell.EffectivePaddingRight - image.ScaledWidth) / 2f;
								break;
							case 2:
								absoluteX = xPos + pdfPCell.Right - pdfPCell.EffectivePaddingRight - image.ScaledWidth;
								break;
							}
							num3 = pdfPCell.Top + yPos - pdfPCell.EffectivePaddingTop;
						}
						image.SetAbsolutePosition(absoluteX, num3 - image.ScaledHeight);
						canvases[3].AddImage(image);
					}
					else
					{
						if (pdfPCell.Rotation == 90 || pdfPCell.Rotation == 270)
						{
							float num5 = num2 - pdfPCell.EffectivePaddingTop - pdfPCell.EffectivePaddingBottom;
							float num6 = pdfPCell.Width - pdfPCell.EffectivePaddingLeft - pdfPCell.EffectivePaddingRight;
							ColumnText columnText = ColumnText.Duplicate(pdfPCell.Column);
							columnText.Canvases = canvases;
							columnText.SetSimpleColumn(0f, 0f, num5 + 0.001f, -num6);
							columnText.Go(true);
							float num7 = -columnText.YLine;
							if (num5 <= 0f || num6 <= 0f)
							{
								num7 = 0f;
							}
							if (num7 <= 0f)
							{
								goto IL_672;
							}
							if (pdfPCell.UseDescender)
							{
								num7 -= columnText.Descender;
							}
							columnText = ColumnText.Duplicate(pdfPCell.Column);
							columnText.Canvases = canvases;
							columnText.SetSimpleColumn(-0.003f, -0.001f, num5 + 0.003f, num7);
							if (pdfPCell.Rotation == 90)
							{
								float f = pdfPCell.Top + yPos - num2 + pdfPCell.EffectivePaddingBottom;
								float e;
								switch (pdfPCell.VerticalAlignment)
								{
								case 5:
									e = pdfPCell.Left + xPos + (pdfPCell.Width + pdfPCell.EffectivePaddingLeft - pdfPCell.EffectivePaddingRight + num7) / 2f;
									break;
								case 6:
									e = pdfPCell.Left + xPos + pdfPCell.Width - pdfPCell.EffectivePaddingRight;
									break;
								default:
									e = pdfPCell.Left + xPos + pdfPCell.EffectivePaddingLeft + num7;
									break;
								}
								this.SaveAndRotateCanvases(canvases, 0f, 1f, -1f, 0f, e, f);
							}
							else
							{
								float f = pdfPCell.Top + yPos - pdfPCell.EffectivePaddingTop;
								float e;
								switch (pdfPCell.VerticalAlignment)
								{
								case 5:
									e = pdfPCell.Left + xPos + (pdfPCell.Width + pdfPCell.EffectivePaddingLeft - pdfPCell.EffectivePaddingRight - num7) / 2f;
									break;
								case 6:
									e = pdfPCell.Left + xPos + pdfPCell.EffectivePaddingLeft;
									break;
								default:
									e = pdfPCell.Left + xPos + pdfPCell.Width - pdfPCell.EffectivePaddingRight - num7;
									break;
								}
								this.SaveAndRotateCanvases(canvases, 0f, -1f, 1f, 0f, e, f);
							}
							try
							{
								columnText.Go();
								goto IL_672;
							}
							finally
							{
								this.RestoreCanvases(canvases);
							}
						}
						float fixedHeight = pdfPCell.FixedHeight;
						float num8 = pdfPCell.Right + xPos - pdfPCell.EffectivePaddingRight;
						float num9 = pdfPCell.Left + xPos + pdfPCell.EffectivePaddingLeft;
						if (pdfPCell.NoWrap)
						{
							switch (pdfPCell.HorizontalAlignment)
							{
							case 1:
								num8 += 10000f;
								num9 -= 10000f;
								break;
							case 2:
								if (pdfPCell.Rotation == 180)
								{
									num8 += 20000f;
								}
								else
								{
									num9 -= 20000f;
								}
								break;
							default:
								if (pdfPCell.Rotation == 180)
								{
									num9 -= 20000f;
								}
								else
								{
									num8 += 20000f;
								}
								break;
							}
						}
						ColumnText columnText2 = ColumnText.Duplicate(pdfPCell.Column);
						columnText2.Canvases = canvases;
						float num10 = num3 - (num2 - pdfPCell.EffectivePaddingTop - pdfPCell.EffectivePaddingBottom);
						if (fixedHeight > 0f && pdfPCell.Height > num2)
						{
							num3 = pdfPCell.Top + yPos - pdfPCell.EffectivePaddingTop;
							num10 = pdfPCell.Top + yPos - num2 + pdfPCell.EffectivePaddingBottom;
						}
						if ((num3 > num10 || columnText2.ZeroHeightElement()) && num9 < num8)
						{
							columnText2.SetSimpleColumn(num9, num10 - 0.001f, num8, num3);
							if (pdfPCell.Rotation == 180)
							{
								float e2 = num9 + num8;
								float f2 = yPos + yPos - num2 + pdfPCell.EffectivePaddingBottom - pdfPCell.EffectivePaddingTop;
								this.SaveAndRotateCanvases(canvases, -1f, 0f, 0f, -1f, e2, f2);
							}
							try
							{
								columnText2.Go();
							}
							finally
							{
								if (pdfPCell.Rotation == 180)
								{
									this.RestoreCanvases(canvases);
								}
							}
						}
					}
					IL_672:
					IPdfPCellEvent cellEvent = pdfPCell.CellEvent;
					if (cellEvent != null)
					{
						Rectangle position = new Rectangle(pdfPCell.Left + xPos, pdfPCell.Top + yPos - num2, pdfPCell.Right + xPos, pdfPCell.Top + yPos);
						cellEvent.CellLayout(pdfPCell, position, canvases);
					}
				}
			}
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x00067100 File Offset: 0x00066100
		public bool IsCalculated()
		{
			return this.calculated;
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x00067108 File Offset: 0x00066108
		// (set) Token: 0x060011E1 RID: 4577 RVA: 0x0006711F File Offset: 0x0006611F
		public float MaxHeights
		{
			get
			{
				if (this.calculated)
				{
					return this.maxHeight;
				}
				return this.CalculateHeights();
			}
			set
			{
				this.maxHeight = value;
			}
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x00067128 File Offset: 0x00066128
		internal float[] GetEventWidth(float xPos)
		{
			int num = 0;
			for (int i = 0; i < this.cells.Length; i++)
			{
				if (this.cells[i] != null)
				{
					num++;
				}
			}
			float[] array = new float[num + 1];
			num = 0;
			array[num++] = xPos;
			for (int j = 0; j < this.cells.Length; j++)
			{
				if (this.cells[j] != null)
				{
					array[num] = array[num - 1] + this.cells[j].Width;
					num++;
				}
			}
			return array;
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x000671A4 File Offset: 0x000661A4
		public PdfPRow SplitRow(PdfPTable table, int rowIndex, float new_height)
		{
			PdfPCell[] array = new PdfPCell[this.cells.Length];
			float[] array2 = new float[this.cells.Length];
			float[] array3 = new float[this.cells.Length];
			bool flag = true;
			for (int i = 0; i < this.cells.Length; i++)
			{
				PdfPCell pdfPCell = this.cells[i];
				if (pdfPCell == null)
				{
					int num = rowIndex;
					if (table.RowSpanAbove(num, i))
					{
						float num2 = new_height + table.GetRowHeight(num);
						while (table.RowSpanAbove(--num, i))
						{
							num2 += table.GetRowHeight(num);
						}
						PdfPRow row = table.GetRow(num);
						if (row != null && row.GetCells()[i] != null)
						{
							array[i] = new PdfPCell(row.GetCells()[i]);
							array[i].ConsumeHeight(num2);
							array[i].Rowspan = row.GetCells()[i].Rowspan - rowIndex + num;
							flag = false;
						}
					}
				}
				else
				{
					array2[i] = pdfPCell.FixedHeight;
					array3[i] = pdfPCell.MinimumHeight;
					Image image = pdfPCell.Image;
					PdfPCell pdfPCell2 = new PdfPCell(pdfPCell);
					if (image != null)
					{
						if (new_height > pdfPCell.EffectivePaddingBottom + pdfPCell.EffectivePaddingTop + 2f)
						{
							pdfPCell2.Phrase = null;
							flag = false;
						}
					}
					else
					{
						ColumnText columnText = ColumnText.Duplicate(pdfPCell.Column);
						float num3 = pdfPCell.Left + pdfPCell.EffectivePaddingLeft;
						float num4 = pdfPCell.Top + pdfPCell.EffectivePaddingBottom - new_height;
						float num5 = pdfPCell.Right - pdfPCell.EffectivePaddingRight;
						float num6 = pdfPCell.Top - pdfPCell.EffectivePaddingTop;
						int rotation = pdfPCell.Rotation;
						float num7;
						if (rotation == 90 || rotation == 270)
						{
							num7 = PdfPRow.SetColumn(columnText, num4, num3, num6, num5);
						}
						else
						{
							num7 = PdfPRow.SetColumn(columnText, num3, num4, pdfPCell.NoWrap ? 20000f : num5, num6);
						}
						int num8 = columnText.Go(true);
						bool flag2 = columnText.YLine == num7;
						if (flag2)
						{
							pdfPCell2.Column = ColumnText.Duplicate(pdfPCell.Column);
							columnText.FilledWidth = 0f;
						}
						else if ((num8 & 1) == 0)
						{
							pdfPCell2.Column = columnText;
							columnText.FilledWidth = 0f;
						}
						else
						{
							pdfPCell2.Phrase = null;
						}
						flag = (flag && flag2);
					}
					array[i] = pdfPCell2;
					pdfPCell.FixedHeight = new_height - pdfPCell.EffectivePaddingBottom;
				}
			}
			if (flag)
			{
				for (int j = 0; j < this.cells.Length; j++)
				{
					PdfPCell pdfPCell3 = this.cells[j];
					if (pdfPCell3 != null)
					{
						if (array2[j] > 0f)
						{
							pdfPCell3.FixedHeight = array2[j];
						}
						else
						{
							pdfPCell3.MinimumHeight = array3[j];
						}
					}
				}
				return null;
			}
			this.CalculateHeights();
			PdfPRow pdfPRow = new PdfPRow(array);
			pdfPRow.widths = (float[])this.widths.Clone();
			pdfPRow.CalculateHeights();
			return pdfPRow;
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x000674A7 File Offset: 0x000664A7
		public PdfPCell[] GetCells()
		{
			return this.cells;
		}

		// Token: 0x04000C8D RID: 3213
		public const float BOTTOM_LIMIT = -1.0737418E+09f;

		// Token: 0x04000C8E RID: 3214
		public const float RIGHT_LIMIT = 20000f;

		// Token: 0x04000C8F RID: 3215
		protected PdfPCell[] cells;

		// Token: 0x04000C90 RID: 3216
		protected float[] widths;

		// Token: 0x04000C91 RID: 3217
		protected float[] extraHeights;

		// Token: 0x04000C92 RID: 3218
		protected float maxHeight;

		// Token: 0x04000C93 RID: 3219
		protected bool calculated;

		// Token: 0x04000C94 RID: 3220
		private int[] canvasesPos;
	}
}
