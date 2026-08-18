using System;
using System.Collections.Generic;
using iTextSharp.text.error_messages;
using iTextSharp.text.pdf.draw;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200045F RID: 1119
	public class ColumnText
	{
		// Token: 0x060025DF RID: 9695 RVA: 0x000E460C File Offset: 0x000E360C
		public ColumnText(PdfContentByte canvas)
		{
			this.canvas = canvas;
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x000E4668 File Offset: 0x000E3668
		public static ColumnText Duplicate(ColumnText org)
		{
			ColumnText columnText = new ColumnText(null);
			columnText.SetACopy(org);
			return columnText;
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x000E4685 File Offset: 0x000E3685
		public ColumnText SetACopy(ColumnText org)
		{
			this.SetSimpleVars(org);
			if (org.bidiLine != null)
			{
				this.bidiLine = new BidiLine(org.bidiLine);
			}
			return this;
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x000E46A8 File Offset: 0x000E36A8
		protected internal void SetSimpleVars(ColumnText org)
		{
			this.maxY = org.maxY;
			this.minY = org.minY;
			this.alignment = org.alignment;
			this.leftWall = null;
			if (org.leftWall != null)
			{
				this.leftWall = new List<float[]>(org.leftWall);
			}
			this.rightWall = null;
			if (org.rightWall != null)
			{
				this.rightWall = new List<float[]>(org.rightWall);
			}
			this.yLine = org.yLine;
			this.currentLeading = org.currentLeading;
			this.fixedLeading = org.fixedLeading;
			this.multipliedLeading = org.multipliedLeading;
			this.canvas = org.canvas;
			this.canvases = org.canvases;
			this.lineStatus = org.lineStatus;
			this.indent = org.indent;
			this.followingIndent = org.followingIndent;
			this.rightIndent = org.rightIndent;
			this.extraParagraphSpace = org.extraParagraphSpace;
			this.rectangularWidth = org.rectangularWidth;
			this.rectangularMode = org.rectangularMode;
			this.spaceCharRatio = org.spaceCharRatio;
			this.lastWasNewline = org.lastWasNewline;
			this.linesWritten = org.linesWritten;
			this.arabicOptions = org.arabicOptions;
			this.runDirection = org.runDirection;
			this.descender = org.descender;
			this.composite = org.composite;
			this.splittedRow = org.splittedRow;
			if (org.composite)
			{
				this.compositeElements = new List<IElement>(org.compositeElements);
				if (this.splittedRow)
				{
					PdfPTable table = (PdfPTable)this.compositeElements[0];
					this.compositeElements[0] = new PdfPTable(table);
				}
				if (org.compositeColumn != null)
				{
					this.compositeColumn = ColumnText.Duplicate(org.compositeColumn);
				}
			}
			this.listIdx = org.listIdx;
			this.firstLineY = org.firstLineY;
			this.leftX = org.leftX;
			this.rightX = org.rightX;
			this.firstLineYDone = org.firstLineYDone;
			this.waitPhrase = org.waitPhrase;
			this.useAscender = org.useAscender;
			this.filledWidth = org.filledWidth;
			this.adjustFirstLine = org.adjustFirstLine;
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x000E48E0 File Offset: 0x000E38E0
		private void AddWaitingPhrase()
		{
			if (this.bidiLine == null && this.waitPhrase != null)
			{
				this.bidiLine = new BidiLine();
				foreach (Chunk chunk in this.waitPhrase.Chunks)
				{
					this.bidiLine.AddChunk(new PdfChunk(chunk, null));
				}
				this.waitPhrase = null;
			}
		}

		// Token: 0x060025E4 RID: 9700 RVA: 0x000E4968 File Offset: 0x000E3968
		public void AddText(Phrase phrase)
		{
			if (phrase == null || this.composite)
			{
				return;
			}
			this.AddWaitingPhrase();
			if (this.bidiLine == null)
			{
				this.waitPhrase = phrase;
				return;
			}
			foreach (Chunk chunk in phrase.Chunks)
			{
				this.bidiLine.AddChunk(new PdfChunk(chunk, null));
			}
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x000E49E8 File Offset: 0x000E39E8
		public void SetText(Phrase phrase)
		{
			this.bidiLine = null;
			this.composite = false;
			this.compositeColumn = null;
			this.compositeElements = null;
			this.listIdx = 0;
			this.splittedRow = false;
			this.waitPhrase = phrase;
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x000E4A1B File Offset: 0x000E3A1B
		public void AddText(Chunk chunk)
		{
			if (chunk == null || this.composite)
			{
				return;
			}
			this.AddText(new Phrase(chunk));
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x000E4A38 File Offset: 0x000E3A38
		public void AddElement(IElement element)
		{
			if (element == null)
			{
				return;
			}
			if (element is Image)
			{
				Image image = (Image)element;
				PdfPTable pdfPTable = new PdfPTable(1);
				float widthPercentage = image.WidthPercentage;
				if (widthPercentage == 0f)
				{
					pdfPTable.TotalWidth = image.ScaledWidth;
					pdfPTable.LockedWidth = true;
				}
				else
				{
					pdfPTable.WidthPercentage = widthPercentage;
				}
				pdfPTable.SpacingAfter = image.SpacingAfter;
				pdfPTable.SpacingBefore = image.SpacingBefore;
				switch (image.Alignment)
				{
				case 0:
					pdfPTable.HorizontalAlignment = 0;
					goto IL_96;
				case 2:
					pdfPTable.HorizontalAlignment = 2;
					goto IL_96;
				}
				pdfPTable.HorizontalAlignment = 1;
				IL_96:
				pdfPTable.AddCell(new PdfPCell(image, true)
				{
					Padding = 0f,
					Border = image.Border,
					BorderColor = image.BorderColor,
					BorderWidth = image.BorderWidth,
					BackgroundColor = image.BackgroundColor
				});
				element = pdfPTable;
			}
			if (element.Type == 10)
			{
				element = new Paragraph((Chunk)element);
			}
			else if (element.Type == 11)
			{
				element = new Paragraph((Phrase)element);
			}
			if (element.Type != 12 && element.Type != 14 && element.Type != 23 && element.Type != 55)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("element.not.allowed"));
			}
			if (!this.composite)
			{
				this.composite = true;
				this.compositeElements = new List<IElement>();
				this.bidiLine = null;
				this.waitPhrase = null;
			}
			this.compositeElements.Add(element);
		}

		// Token: 0x060025E8 RID: 9704 RVA: 0x000E4BC4 File Offset: 0x000E3BC4
		protected List<float[]> ConvertColumn(float[] cLine)
		{
			if (cLine.Length < 4)
			{
				throw new Exception(MessageLocalization.GetComposedMessage("no.valid.column.line.found"));
			}
			List<float[]> list = new List<float[]>();
			for (int i = 0; i < cLine.Length - 2; i += 2)
			{
				float num = cLine[i];
				float num2 = cLine[i + 1];
				float num3 = cLine[i + 2];
				float num4 = cLine[i + 3];
				if (num2 != num4)
				{
					float num5 = (num - num3) / (num2 - num4);
					float num6 = num - num5 * num2;
					float[] array = new float[]
					{
						Math.Min(num2, num4),
						Math.Max(num2, num4),
						num5,
						num6
					};
					list.Add(array);
					this.maxY = Math.Max(this.maxY, array[1]);
					this.minY = Math.Min(this.minY, array[0]);
				}
			}
			if (list.Count == 0)
			{
				throw new Exception(MessageLocalization.GetComposedMessage("no.valid.column.line.found"));
			}
			return list;
		}

		// Token: 0x060025E9 RID: 9705 RVA: 0x000E4CAC File Offset: 0x000E3CAC
		protected float FindLimitsPoint(List<float[]> wall)
		{
			this.lineStatus = 0;
			if (this.yLine < this.minY || this.yLine > this.maxY)
			{
				this.lineStatus = 1;
				return 0f;
			}
			for (int i = 0; i < wall.Count; i++)
			{
				float[] array = wall[i];
				if (this.yLine >= array[0] && this.yLine <= array[1])
				{
					return array[2] * this.yLine + array[3];
				}
			}
			this.lineStatus = 2;
			return 0f;
		}

		// Token: 0x060025EA RID: 9706 RVA: 0x000E4D34 File Offset: 0x000E3D34
		protected float[] FindLimitsOneLine()
		{
			float num = this.FindLimitsPoint(this.leftWall);
			if (this.lineStatus == 1 || this.lineStatus == 2)
			{
				return null;
			}
			float num2 = this.FindLimitsPoint(this.rightWall);
			if (this.lineStatus == 2)
			{
				return null;
			}
			return new float[]
			{
				num,
				num2
			};
		}

		// Token: 0x060025EB RID: 9707 RVA: 0x000E4D8C File Offset: 0x000E3D8C
		protected float[] FindLimitsTwoLines()
		{
			bool flag = false;
			while (!flag || this.currentLeading != 0f)
			{
				flag = true;
				float[] array = this.FindLimitsOneLine();
				if (this.lineStatus == 1)
				{
					return null;
				}
				this.yLine -= this.currentLeading;
				if (this.lineStatus != 2)
				{
					float[] array2 = this.FindLimitsOneLine();
					if (this.lineStatus == 1)
					{
						return null;
					}
					if (this.lineStatus == 2)
					{
						this.yLine -= this.currentLeading;
					}
					else if (array[0] < array2[1] && array2[0] < array[1])
					{
						return new float[]
						{
							array[0],
							array[1],
							array2[0],
							array2[1]
						};
					}
				}
			}
			return null;
		}

		// Token: 0x060025EC RID: 9708 RVA: 0x000E4E40 File Offset: 0x000E3E40
		public void SetColumns(float[] leftLine, float[] rightLine)
		{
			this.maxY = -1E+21f;
			this.minY = 1E+21f;
			this.YLine = Math.Max(leftLine[1], leftLine[leftLine.Length - 1]);
			this.rightWall = this.ConvertColumn(rightLine);
			this.leftWall = this.ConvertColumn(leftLine);
			this.rectangularWidth = -1f;
			this.rectangularMode = false;
		}

		// Token: 0x060025ED RID: 9709 RVA: 0x000E4EA4 File Offset: 0x000E3EA4
		public void SetSimpleColumn(Phrase phrase, float llx, float lly, float urx, float ury, float leading, int alignment)
		{
			this.AddText(phrase);
			this.SetSimpleColumn(llx, lly, urx, ury, leading, alignment);
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x000E4EBD File Offset: 0x000E3EBD
		public void SetSimpleColumn(float llx, float lly, float urx, float ury, float leading, int alignment)
		{
			this.Leading = leading;
			this.alignment = alignment;
			this.SetSimpleColumn(llx, lly, urx, ury);
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x000E4EDC File Offset: 0x000E3EDC
		public void SetSimpleColumn(float llx, float lly, float urx, float ury)
		{
			this.leftX = Math.Min(llx, urx);
			this.maxY = Math.Max(lly, ury);
			this.minY = Math.Min(lly, ury);
			this.rightX = Math.Max(llx, urx);
			this.yLine = this.maxY;
			this.rectangularWidth = this.rightX - this.leftX;
			if (this.rectangularWidth < 0f)
			{
				this.rectangularWidth = 0f;
			}
			this.rectangularMode = true;
		}

		// Token: 0x060025F0 RID: 9712 RVA: 0x000E4F5D File Offset: 0x000E3F5D
		public void SetLeading(float fixedLeading, float multipliedLeading)
		{
			this.fixedLeading = fixedLeading;
			this.multipliedLeading = multipliedLeading;
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x060025F1 RID: 9713 RVA: 0x000E4F6D File Offset: 0x000E3F6D
		// (set) Token: 0x060025F2 RID: 9714 RVA: 0x000E4F75 File Offset: 0x000E3F75
		public float Leading
		{
			get
			{
				return this.fixedLeading;
			}
			set
			{
				this.fixedLeading = value;
				this.multipliedLeading = 0f;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x060025F3 RID: 9715 RVA: 0x000E4F89 File Offset: 0x000E3F89
		public float MultipliedLeading
		{
			get
			{
				return this.multipliedLeading;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x060025F4 RID: 9716 RVA: 0x000E4F91 File Offset: 0x000E3F91
		// (set) Token: 0x060025F5 RID: 9717 RVA: 0x000E4F99 File Offset: 0x000E3F99
		public float YLine
		{
			get
			{
				return this.yLine;
			}
			set
			{
				this.yLine = value;
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x060025F6 RID: 9718 RVA: 0x000E4FA2 File Offset: 0x000E3FA2
		// (set) Token: 0x060025F7 RID: 9719 RVA: 0x000E4FAA File Offset: 0x000E3FAA
		public int Alignment
		{
			get
			{
				return this.alignment;
			}
			set
			{
				this.alignment = value;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x060025F8 RID: 9720 RVA: 0x000E4FB3 File Offset: 0x000E3FB3
		// (set) Token: 0x060025F9 RID: 9721 RVA: 0x000E4FBB File Offset: 0x000E3FBB
		public float Indent
		{
			get
			{
				return this.indent;
			}
			set
			{
				this.indent = value;
				this.lastWasNewline = true;
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x060025FA RID: 9722 RVA: 0x000E4FCB File Offset: 0x000E3FCB
		// (set) Token: 0x060025FB RID: 9723 RVA: 0x000E4FD3 File Offset: 0x000E3FD3
		public float FollowingIndent
		{
			get
			{
				return this.followingIndent;
			}
			set
			{
				this.followingIndent = value;
				this.lastWasNewline = true;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x060025FC RID: 9724 RVA: 0x000E4FE3 File Offset: 0x000E3FE3
		// (set) Token: 0x060025FD RID: 9725 RVA: 0x000E4FEB File Offset: 0x000E3FEB
		public float RightIndent
		{
			get
			{
				return this.rightIndent;
			}
			set
			{
				this.rightIndent = value;
				this.lastWasNewline = true;
			}
		}

		// Token: 0x060025FE RID: 9726 RVA: 0x000E4FFB File Offset: 0x000E3FFB
		public int Go()
		{
			return this.Go(false);
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x000E5004 File Offset: 0x000E4004
		public int Go(bool simulate)
		{
			if (this.composite)
			{
				return this.GoComposite(simulate);
			}
			this.AddWaitingPhrase();
			if (this.bidiLine == null)
			{
				return 1;
			}
			this.descender = 0f;
			this.linesWritten = 0;
			this.lastX = 0f;
			bool flag = false;
			float num = this.spaceCharRatio;
			object[] array = new object[2];
			PdfFont pdfFont = null;
			float num2 = 0f;
			array[1] = num2;
			PdfDocument pdfDocument = null;
			PdfContentByte graphics = null;
			PdfContentByte pdfContentByte = null;
			this.firstLineY = float.NaN;
			int num3 = 1;
			if (this.runDirection != 0)
			{
				num3 = this.runDirection;
			}
			if (this.canvas != null)
			{
				graphics = this.canvas;
				pdfDocument = this.canvas.PdfDocument;
				pdfContentByte = this.canvas.Duplicate;
			}
			else if (!simulate)
			{
				throw new Exception(MessageLocalization.GetComposedMessage("columntext.go.with.simulate.eq.eq.false.and.text.eq.eq.null"));
			}
			if (!simulate)
			{
				if (num == ColumnText.GLOBAL_SPACE_CHAR_RATIO)
				{
					num = pdfContentByte.PdfWriter.SpaceCharRatio;
				}
				else if (num < 0.001f)
				{
					num = 0.001f;
				}
			}
			float num6;
			for (;;)
			{
				float num4 = this.lastWasNewline ? this.indent : this.followingIndent;
				PdfLine pdfLine;
				float num5;
				if (this.rectangularMode)
				{
					if (this.rectangularWidth <= num4 + this.rightIndent)
					{
						break;
					}
					if (this.bidiLine.IsEmpty())
					{
						goto Block_13;
					}
					pdfLine = this.bidiLine.ProcessLine(this.leftX, this.rectangularWidth - num4 - this.rightIndent, this.alignment, num3, this.arabicOptions);
					if (pdfLine == null)
					{
						goto Block_14;
					}
					float[] maxSize = pdfLine.GetMaxSize();
					if (this.UseAscender && float.IsNaN(this.firstLineY))
					{
						this.currentLeading = pdfLine.Ascender;
					}
					else
					{
						this.currentLeading = Math.Max(this.fixedLeading + maxSize[0] * this.multipliedLeading, maxSize[1]);
					}
					if (this.yLine > this.maxY || this.yLine - this.currentLeading < this.minY)
					{
						goto IL_20D;
					}
					this.yLine -= this.currentLeading;
					if (!simulate && !flag)
					{
						pdfContentByte.BeginText();
						flag = true;
					}
					if (float.IsNaN(this.firstLineY))
					{
						this.firstLineY = this.yLine;
					}
					this.UpdateFilledWidth(this.rectangularWidth - pdfLine.WidthLeft);
					num5 = this.leftX;
				}
				else
				{
					num6 = this.yLine;
					float[] array2 = this.FindLimitsTwoLines();
					if (array2 == null)
					{
						goto Block_21;
					}
					if (this.bidiLine.IsEmpty())
					{
						goto Block_23;
					}
					num5 = Math.Max(array2[0], array2[2]);
					float num7 = Math.Min(array2[1], array2[3]);
					if (num7 - num5 <= num4 + this.rightIndent)
					{
						continue;
					}
					if (!simulate && !flag)
					{
						pdfContentByte.BeginText();
						flag = true;
					}
					pdfLine = this.bidiLine.ProcessLine(num5, num7 - num5 - num4 - this.rightIndent, this.alignment, num3, this.arabicOptions);
					if (pdfLine == null)
					{
						goto Block_27;
					}
				}
				if (!simulate)
				{
					array[0] = pdfFont;
					pdfContentByte.SetTextMatrix(num5 + (pdfLine.RTL ? this.rightIndent : num4) + pdfLine.IndentLeft, this.yLine);
					this.lastX = pdfDocument.WriteLineToContent(pdfLine, pdfContentByte, graphics, array, num);
					pdfFont = (PdfFont)array[0];
				}
				this.lastWasNewline = pdfLine.NewlineSplit;
				this.yLine -= (pdfLine.NewlineSplit ? this.extraParagraphSpace : 0f);
				this.linesWritten++;
				this.descender = pdfLine.Descender;
			}
			int num8 = 2;
			if (this.bidiLine.IsEmpty())
			{
				num8 |= 1;
				goto IL_3F0;
			}
			goto IL_3F0;
			Block_13:
			num8 = 1;
			goto IL_3F0;
			Block_14:
			num8 = 1;
			goto IL_3F0;
			IL_20D:
			num8 = 2;
			this.bidiLine.Restore();
			goto IL_3F0;
			Block_21:
			num8 = 2;
			if (this.bidiLine.IsEmpty())
			{
				num8 |= 1;
			}
			this.yLine = num6;
			goto IL_3F0;
			Block_23:
			num8 = 1;
			this.yLine = num6;
			goto IL_3F0;
			Block_27:
			num8 = 1;
			this.yLine = num6;
			IL_3F0:
			if (flag)
			{
				pdfContentByte.EndText();
				this.canvas.Add(pdfContentByte);
			}
			return num8;
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06002600 RID: 9728 RVA: 0x000E541A File Offset: 0x000E441A
		// (set) Token: 0x06002601 RID: 9729 RVA: 0x000E5422 File Offset: 0x000E4422
		public float ExtraParagraphSpace
		{
			get
			{
				return this.extraParagraphSpace;
			}
			set
			{
				this.extraParagraphSpace = value;
			}
		}

		// Token: 0x06002602 RID: 9730 RVA: 0x000E542B File Offset: 0x000E442B
		public void ClearChunks()
		{
			if (this.bidiLine != null)
			{
				this.bidiLine.ClearChunks();
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06002603 RID: 9731 RVA: 0x000E5440 File Offset: 0x000E4440
		// (set) Token: 0x06002604 RID: 9732 RVA: 0x000E5448 File Offset: 0x000E4448
		public float SpaceCharRatio
		{
			get
			{
				return this.spaceCharRatio;
			}
			set
			{
				this.spaceCharRatio = value;
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06002605 RID: 9733 RVA: 0x000E5451 File Offset: 0x000E4451
		// (set) Token: 0x06002606 RID: 9734 RVA: 0x000E5459 File Offset: 0x000E4459
		public int RunDirection
		{
			get
			{
				return this.runDirection;
			}
			set
			{
				if (value < 0 || value > 3)
				{
					throw new Exception(MessageLocalization.GetComposedMessage("invalid.run.direction.1", value));
				}
				this.runDirection = value;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06002607 RID: 9735 RVA: 0x000E5480 File Offset: 0x000E4480
		public int LinesWritten
		{
			get
			{
				return this.linesWritten;
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06002608 RID: 9736 RVA: 0x000E5488 File Offset: 0x000E4488
		public float LastX
		{
			get
			{
				return this.lastX;
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x0600260A RID: 9738 RVA: 0x000E5499 File Offset: 0x000E4499
		// (set) Token: 0x06002609 RID: 9737 RVA: 0x000E5490 File Offset: 0x000E4490
		public int ArabicOptions
		{
			get
			{
				return this.arabicOptions;
			}
			set
			{
				this.arabicOptions = value;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x0600260B RID: 9739 RVA: 0x000E54A1 File Offset: 0x000E44A1
		public float Descender
		{
			get
			{
				return this.descender;
			}
		}

		// Token: 0x0600260C RID: 9740 RVA: 0x000E54AC File Offset: 0x000E44AC
		public static float GetWidth(Phrase phrase, int runDirection, int arabicOptions)
		{
			ColumnText columnText = new ColumnText(null);
			columnText.AddText(phrase);
			columnText.AddWaitingPhrase();
			PdfLine pdfLine = columnText.bidiLine.ProcessLine(0f, 20000f, 0, runDirection, arabicOptions);
			if (pdfLine == null)
			{
				return 0f;
			}
			return 20000f - pdfLine.WidthLeft;
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x000E54FB File Offset: 0x000E44FB
		public static float GetWidth(Phrase phrase)
		{
			return ColumnText.GetWidth(phrase, 1, 0);
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x000E5508 File Offset: 0x000E4508
		public static void ShowTextAligned(PdfContentByte canvas, int alignment, Phrase phrase, float x, float y, float rotation, int runDirection, int arabicOptions)
		{
			if (alignment != 0 && alignment != 1 && alignment != 2)
			{
				alignment = 0;
			}
			canvas.SaveState();
			ColumnText columnText = new ColumnText(canvas);
			float num = -1f;
			float num2 = 2f;
			float num3;
			float num4;
			switch (alignment)
			{
			case 0:
				num3 = 0f;
				num4 = 20000f;
				goto IL_6A;
			case 2:
				num3 = -20000f;
				num4 = 0f;
				goto IL_6A;
			}
			num3 = -20000f;
			num4 = 20000f;
			IL_6A:
			if (rotation == 0f)
			{
				num3 += x;
				num += y;
				num4 += x;
				num2 += y;
			}
			else
			{
				double num5 = (double)rotation * 3.141592653589793 / 180.0;
				float num6 = (float)Math.Cos(num5);
				float num7 = (float)Math.Sin(num5);
				canvas.ConcatCTM(num6, num7, -num7, num6, x, y);
			}
			columnText.SetSimpleColumn(phrase, num3, num, num4, num2, 2f, alignment);
			if (runDirection == 3)
			{
				if (alignment == 0)
				{
					alignment = 2;
				}
				else if (alignment == 2)
				{
					alignment = 0;
				}
			}
			columnText.Alignment = alignment;
			columnText.ArabicOptions = arabicOptions;
			columnText.RunDirection = runDirection;
			columnText.Go();
			canvas.RestoreState();
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x000E5627 File Offset: 0x000E4627
		public static void ShowTextAligned(PdfContentByte canvas, int alignment, Phrase phrase, float x, float y, float rotation)
		{
			ColumnText.ShowTextAligned(canvas, alignment, phrase, x, y, rotation, 1, 0);
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x000E5638 File Offset: 0x000E4638
		protected int GoComposite(bool simulate)
		{
			if (!this.rectangularMode)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("irregular.columns.are.not.supported.in.composite.mode"));
			}
			this.linesWritten = 0;
			this.descender = 0f;
			bool flag = this.adjustFirstLine;
			IL_31:
			while (this.compositeElements.Count != 0)
			{
				IElement element = this.compositeElements[0];
				if (element.Type == 12)
				{
					Paragraph paragraph = (Paragraph)element;
					int num = 0;
					for (int i = 0; i < 2; i++)
					{
						float num2 = this.yLine;
						bool flag2 = false;
						if (this.compositeColumn == null)
						{
							this.compositeColumn = new ColumnText(this.canvas);
							this.compositeColumn.Alignment = paragraph.Alignment;
							this.compositeColumn.Indent = paragraph.IndentationLeft + paragraph.FirstLineIndent;
							this.compositeColumn.ExtraParagraphSpace = paragraph.ExtraParagraphSpace;
							this.compositeColumn.FollowingIndent = paragraph.IndentationLeft;
							this.compositeColumn.RightIndent = paragraph.IndentationRight;
							this.compositeColumn.SetLeading(paragraph.Leading, paragraph.MultipliedLeading);
							this.compositeColumn.RunDirection = this.runDirection;
							this.compositeColumn.ArabicOptions = this.arabicOptions;
							this.compositeColumn.SpaceCharRatio = this.spaceCharRatio;
							this.compositeColumn.AddText(paragraph);
							if (!flag)
							{
								this.yLine -= paragraph.SpacingBefore;
							}
							flag2 = true;
						}
						this.compositeColumn.UseAscender = (flag && this.useAscender);
						this.compositeColumn.leftX = this.leftX;
						this.compositeColumn.rightX = this.rightX;
						this.compositeColumn.yLine = this.yLine;
						this.compositeColumn.rectangularWidth = this.rectangularWidth;
						this.compositeColumn.rectangularMode = this.rectangularMode;
						this.compositeColumn.minY = this.minY;
						this.compositeColumn.maxY = this.maxY;
						bool flag3 = paragraph.KeepTogether && flag2 && !flag;
						num = this.compositeColumn.Go(simulate || (flag3 && i == 0));
						this.lastX = this.compositeColumn.LastX;
						this.UpdateFilledWidth(this.compositeColumn.filledWidth);
						if ((num & 1) == 0 && flag3)
						{
							this.compositeColumn = null;
							this.yLine = num2;
							return 2;
						}
						if (simulate || !flag3)
						{
							break;
						}
						if (i == 0)
						{
							this.compositeColumn = null;
							this.yLine = num2;
						}
					}
					flag = false;
					this.yLine = this.compositeColumn.yLine;
					this.linesWritten += this.compositeColumn.linesWritten;
					this.descender = this.compositeColumn.descender;
					if ((num & 1) != 0)
					{
						this.compositeColumn = null;
						this.compositeElements.RemoveAt(0);
						this.yLine -= paragraph.SpacingAfter;
					}
					if ((num & 2) != 0)
					{
						return 2;
					}
				}
				else if (element.Type == 14)
				{
					List list = (List)element;
					List<IElement> items = list.Items;
					ListItem listItem = null;
					float num3 = list.IndentationLeft;
					int num4 = 0;
					Stack<object[]> stack = new Stack<object[]>();
					int j = 0;
					while (j < items.Count)
					{
						object obj = items[j];
						if (obj is ListItem)
						{
							if (num4 == this.listIdx)
							{
								listItem = (ListItem)obj;
								break;
							}
							num4++;
							goto IL_3BC;
						}
						else
						{
							if (!(obj is List))
							{
								goto IL_3BC;
							}
							stack.Push(new object[]
							{
								list,
								j,
								num3
							});
							list = (List)obj;
							items = list.Items;
							num3 += list.IndentationLeft;
							j = -1;
						}
						IL_406:
						j++;
						continue;
						IL_3BC:
						if (j == items.Count - 1 && stack.Count > 0)
						{
							object[] array = stack.Pop();
							list = (List)array[0];
							items = list.Items;
							j = (int)array[1];
							num3 = (float)array[2];
							goto IL_406;
						}
						goto IL_406;
					}
					int num5 = 0;
					for (int k = 0; k < 2; k++)
					{
						float num6 = this.yLine;
						bool flag4 = false;
						if (this.compositeColumn == null)
						{
							if (listItem == null)
							{
								this.listIdx = 0;
								this.compositeElements.RemoveAt(0);
								goto IL_31;
							}
							this.compositeColumn = new ColumnText(this.canvas);
							this.compositeColumn.UseAscender = (flag && this.useAscender);
							this.compositeColumn.Alignment = listItem.Alignment;
							this.compositeColumn.Indent = listItem.IndentationLeft + num3 + listItem.FirstLineIndent;
							this.compositeColumn.ExtraParagraphSpace = listItem.ExtraParagraphSpace;
							this.compositeColumn.FollowingIndent = this.compositeColumn.Indent;
							this.compositeColumn.RightIndent = listItem.IndentationRight + list.IndentationRight;
							this.compositeColumn.SetLeading(listItem.Leading, listItem.MultipliedLeading);
							this.compositeColumn.RunDirection = this.runDirection;
							this.compositeColumn.ArabicOptions = this.arabicOptions;
							this.compositeColumn.SpaceCharRatio = this.spaceCharRatio;
							this.compositeColumn.AddText(listItem);
							if (!flag)
							{
								this.yLine -= listItem.SpacingBefore;
							}
							flag4 = true;
						}
						this.compositeColumn.leftX = this.leftX;
						this.compositeColumn.rightX = this.rightX;
						this.compositeColumn.yLine = this.yLine;
						this.compositeColumn.rectangularWidth = this.rectangularWidth;
						this.compositeColumn.rectangularMode = this.rectangularMode;
						this.compositeColumn.minY = this.minY;
						this.compositeColumn.maxY = this.maxY;
						bool flag5 = listItem.KeepTogether && flag4 && !flag;
						num5 = this.compositeColumn.Go(simulate || (flag5 && k == 0));
						this.lastX = this.compositeColumn.LastX;
						this.UpdateFilledWidth(this.compositeColumn.filledWidth);
						if ((num5 & 1) == 0 && flag5)
						{
							this.compositeColumn = null;
							this.yLine = num6;
							return 2;
						}
						if (simulate || !flag5)
						{
							break;
						}
						if (k == 0)
						{
							this.compositeColumn = null;
							this.yLine = num6;
						}
					}
					flag = false;
					this.yLine = this.compositeColumn.yLine;
					this.linesWritten += this.compositeColumn.linesWritten;
					this.descender = this.compositeColumn.descender;
					if (!float.IsNaN(this.compositeColumn.firstLineY) && !this.compositeColumn.firstLineYDone)
					{
						if (!simulate)
						{
							ColumnText.ShowTextAligned(this.canvas, 0, new Phrase(listItem.ListSymbol), this.compositeColumn.leftX + num3, this.compositeColumn.firstLineY, 0f);
						}
						this.compositeColumn.firstLineYDone = true;
					}
					if ((num5 & 1) != 0)
					{
						this.compositeColumn = null;
						this.listIdx++;
						this.yLine -= listItem.SpacingAfter;
					}
					if ((num5 & 2) != 0)
					{
						return 2;
					}
				}
				else if (element.Type == 23)
				{
					if (this.yLine < this.minY || this.yLine > this.maxY)
					{
						return 2;
					}
					PdfPTable pdfPTable = (PdfPTable)element;
					if (pdfPTable.Size <= pdfPTable.HeaderRows)
					{
						this.compositeElements.RemoveAt(0);
					}
					else
					{
						float num7 = this.yLine;
						if (!flag && this.listIdx == 0)
						{
							num7 -= pdfPTable.SpacingBefore;
						}
						float yPos = num7;
						if (num7 < this.minY || num7 > this.maxY)
						{
							return 2;
						}
						this.currentLeading = 0f;
						float num8 = this.leftX;
						float num9;
						if (pdfPTable.LockedWidth)
						{
							num9 = pdfPTable.TotalWidth;
							this.UpdateFilledWidth(num9);
						}
						else
						{
							num9 = this.rectangularWidth * pdfPTable.WidthPercentage / 100f;
							pdfPTable.TotalWidth = num9;
						}
						int headerRows = pdfPTable.HeaderRows;
						int num10 = pdfPTable.FooterRows;
						if (num10 > headerRows)
						{
							num10 = headerRows;
						}
						int num11 = headerRows - num10;
						float headerHeight = pdfPTable.HeaderHeight;
						float footerHeight = pdfPTable.FooterHeight;
						bool flag6 = !flag && pdfPTable.SkipFirstHeader && this.listIdx <= headerRows;
						if (!flag6)
						{
							num7 -= headerHeight;
							if (num7 < this.minY || num7 > this.maxY)
							{
								if (flag)
								{
									this.compositeElements.RemoveAt(0);
									continue;
								}
								return 2;
							}
						}
						if (this.listIdx < headerRows)
						{
							this.listIdx = headerRows;
						}
						if (!pdfPTable.ElementComplete)
						{
							num7 -= footerHeight;
						}
						int l;
						for (l = this.listIdx; l < pdfPTable.Size; l++)
						{
							float rowHeight = pdfPTable.GetRowHeight(l);
							if (num7 - rowHeight < this.minY)
							{
								break;
							}
							num7 -= rowHeight;
						}
						if (!pdfPTable.ElementComplete)
						{
							num7 += footerHeight;
						}
						if (l < pdfPTable.Size)
						{
							if (pdfPTable.SplitRows && (!pdfPTable.SplitLate || (l == this.listIdx && flag)))
							{
								if (!this.splittedRow)
								{
									this.splittedRow = true;
									pdfPTable = new PdfPTable(pdfPTable);
									this.compositeElements[0] = pdfPTable;
									List<PdfPRow> rows = pdfPTable.Rows;
									for (int m = headerRows; m < this.listIdx; m++)
									{
										rows[m] = null;
									}
								}
								float new_height = num7 - this.minY;
								PdfPRow pdfPRow = pdfPTable.GetRow(l).SplitRow(pdfPTable, l, new_height);
								if (pdfPRow == null)
								{
									if (l == this.listIdx)
									{
										return 2;
									}
								}
								else
								{
									num7 = this.minY;
									pdfPTable.Rows.Insert(++l, pdfPRow);
								}
							}
							else
							{
								if (!pdfPTable.SplitRows && l == this.listIdx && flag)
								{
									this.compositeElements.RemoveAt(0);
									this.splittedRow = false;
									continue;
								}
								if (l == this.listIdx && !flag && (!pdfPTable.SplitRows || pdfPTable.SplitLate) && (pdfPTable.FooterRows == 0 || pdfPTable.ElementComplete))
								{
									return 2;
								}
							}
						}
						flag = false;
						if (!simulate)
						{
							switch (pdfPTable.HorizontalAlignment)
							{
							case 0:
								break;
							case 1:
								goto IL_A8C;
							case 2:
								num8 += this.rectangularWidth - num9;
								break;
							default:
								goto IL_A8C;
							}
							IL_AA0:
							PdfPTable pdfPTable2 = PdfPTable.ShallowCopy(pdfPTable);
							List<PdfPRow> rows2 = pdfPTable2.Rows;
							if (!flag6 && num11 > 0)
							{
								rows2.AddRange(pdfPTable.GetRows(0, num11));
							}
							else
							{
								pdfPTable2.HeaderRows = num10;
							}
							rows2.AddRange(pdfPTable.GetRows(this.listIdx, l));
							bool flag7 = !pdfPTable.SkipLastFooter;
							bool newPageFollows = false;
							if (l < pdfPTable.Size)
							{
								pdfPTable2.ElementComplete = true;
								flag7 = true;
								newPageFollows = true;
							}
							int num12 = 0;
							while (num12 < num10 && pdfPTable2.ElementComplete && flag7)
							{
								rows2.Add(pdfPTable.GetRow(num12 + num11));
								num12++;
							}
							float num13 = 0f;
							int num14 = rows2.Count - 1;
							if (flag7)
							{
								num14 -= num10;
							}
							PdfPRow pdfPRow2 = rows2[num14];
							if (pdfPTable.IsExtendLastRow(newPageFollows))
							{
								num13 = pdfPRow2.MaxHeights;
								pdfPRow2.MaxHeights = num7 - this.minY + num13;
								num7 = this.minY;
							}
							if (this.canvases != null)
							{
								pdfPTable2.WriteSelectedRows(0, -1, num8, yPos, this.canvases);
							}
							else
							{
								pdfPTable2.WriteSelectedRows(0, -1, num8, yPos, this.canvas);
							}
							if (pdfPTable.IsExtendLastRow(newPageFollows))
							{
								pdfPRow2.MaxHeights = num13;
								goto IL_C03;
							}
							goto IL_C03;
							IL_A8C:
							num8 += (this.rectangularWidth - num9) / 2f;
							goto IL_AA0;
						}
						if (pdfPTable.ExtendLastRow && this.minY > -1.0737418E+09f)
						{
							num7 = this.minY;
						}
						IL_C03:
						this.yLine = num7;
						if (!flag6 && !pdfPTable.ElementComplete)
						{
							this.yLine += footerHeight;
						}
						if (l < pdfPTable.Size)
						{
							if (this.splittedRow)
							{
								List<PdfPRow> rows3 = pdfPTable.Rows;
								for (int n = this.listIdx; n < l; n++)
								{
									rows3[n] = null;
								}
							}
							this.listIdx = l;
							return 2;
						}
						this.yLine -= pdfPTable.SpacingAfter;
						this.compositeElements.RemoveAt(0);
						this.splittedRow = false;
						this.listIdx = 0;
					}
				}
				else if (element.Type == 55)
				{
					if (!simulate)
					{
						IDrawInterface drawInterface = (IDrawInterface)element;
						drawInterface.Draw(this.canvas, this.leftX, this.minY, this.rightX, this.maxY, this.yLine);
					}
					this.compositeElements.RemoveAt(0);
				}
				else
				{
					this.compositeElements.RemoveAt(0);
				}
			}
			return 1;
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06002612 RID: 9746 RVA: 0x000E636A File Offset: 0x000E536A
		// (set) Token: 0x06002611 RID: 9745 RVA: 0x000E6346 File Offset: 0x000E5346
		public PdfContentByte Canvas
		{
			get
			{
				return this.canvas;
			}
			set
			{
				this.canvas = value;
				this.canvases = null;
				if (this.compositeColumn != null)
				{
					this.compositeColumn.Canvas = value;
				}
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06002614 RID: 9748 RVA: 0x000E63A2 File Offset: 0x000E53A2
		// (set) Token: 0x06002613 RID: 9747 RVA: 0x000E6372 File Offset: 0x000E5372
		public PdfContentByte[] Canvases
		{
			get
			{
				return this.canvases;
			}
			set
			{
				this.canvases = value;
				this.canvas = this.canvases[3];
				if (this.compositeColumn != null)
				{
					this.compositeColumn.Canvases = this.canvases;
				}
			}
		}

		// Token: 0x06002615 RID: 9749 RVA: 0x000E63AA File Offset: 0x000E53AA
		public bool ZeroHeightElement()
		{
			return this.composite && this.compositeElements.Count != 0 && this.compositeElements[0].Type == 55;
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06002617 RID: 9751 RVA: 0x000E63E1 File Offset: 0x000E53E1
		// (set) Token: 0x06002616 RID: 9750 RVA: 0x000E63D8 File Offset: 0x000E53D8
		public bool UseAscender
		{
			get
			{
				return this.useAscender;
			}
			set
			{
				this.useAscender = value;
			}
		}

		// Token: 0x06002618 RID: 9752 RVA: 0x000E63E9 File Offset: 0x000E53E9
		public static bool HasMoreText(int status)
		{
			return (status & 1) == 0;
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x0600261A RID: 9754 RVA: 0x000E63FA File Offset: 0x000E53FA
		// (set) Token: 0x06002619 RID: 9753 RVA: 0x000E63F1 File Offset: 0x000E53F1
		public float FilledWidth
		{
			get
			{
				return this.filledWidth;
			}
			set
			{
				this.filledWidth = value;
			}
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x000E6402 File Offset: 0x000E5402
		public void UpdateFilledWidth(float w)
		{
			if (w > this.filledWidth)
			{
				this.filledWidth = w;
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x0600261D RID: 9757 RVA: 0x000E641D File Offset: 0x000E541D
		// (set) Token: 0x0600261C RID: 9756 RVA: 0x000E6414 File Offset: 0x000E5414
		public bool AdjustFirstLine
		{
			get
			{
				return this.adjustFirstLine;
			}
			set
			{
				this.adjustFirstLine = value;
			}
		}

		// Token: 0x04001A41 RID: 6721
		public const int AR_COMPOSEDTASHKEEL = 4;

		// Token: 0x04001A42 RID: 6722
		public const int AR_LIG = 8;

		// Token: 0x04001A43 RID: 6723
		public const int DIGITS_EN2AN = 32;

		// Token: 0x04001A44 RID: 6724
		public const int DIGITS_AN2EN = 64;

		// Token: 0x04001A45 RID: 6725
		public const int DIGITS_EN2AN_INIT_LR = 96;

		// Token: 0x04001A46 RID: 6726
		public const int DIGITS_EN2AN_INIT_AL = 128;

		// Token: 0x04001A47 RID: 6727
		public const int DIGIT_TYPE_AN = 0;

		// Token: 0x04001A48 RID: 6728
		public const int DIGIT_TYPE_AN_EXTENDED = 256;

		// Token: 0x04001A49 RID: 6729
		public const int NO_MORE_TEXT = 1;

		// Token: 0x04001A4A RID: 6730
		public const int NO_MORE_COLUMN = 2;

		// Token: 0x04001A4B RID: 6731
		protected const int LINE_STATUS_OK = 0;

		// Token: 0x04001A4C RID: 6732
		protected const int LINE_STATUS_OFFLIMITS = 1;

		// Token: 0x04001A4D RID: 6733
		protected const int LINE_STATUS_NOLINE = 2;

		// Token: 0x04001A4E RID: 6734
		public int AR_NOVOWEL = 1;

		// Token: 0x04001A4F RID: 6735
		protected int runDirection;

		// Token: 0x04001A50 RID: 6736
		public static float GLOBAL_SPACE_CHAR_RATIO;

		// Token: 0x04001A51 RID: 6737
		protected float maxY;

		// Token: 0x04001A52 RID: 6738
		protected float minY;

		// Token: 0x04001A53 RID: 6739
		protected float leftX;

		// Token: 0x04001A54 RID: 6740
		protected float rightX;

		// Token: 0x04001A55 RID: 6741
		protected int alignment;

		// Token: 0x04001A56 RID: 6742
		protected List<float[]> leftWall;

		// Token: 0x04001A57 RID: 6743
		protected List<float[]> rightWall;

		// Token: 0x04001A58 RID: 6744
		protected BidiLine bidiLine;

		// Token: 0x04001A59 RID: 6745
		protected float yLine;

		// Token: 0x04001A5A RID: 6746
		protected float lastX;

		// Token: 0x04001A5B RID: 6747
		protected float currentLeading = 16f;

		// Token: 0x04001A5C RID: 6748
		protected float fixedLeading = 16f;

		// Token: 0x04001A5D RID: 6749
		protected float multipliedLeading;

		// Token: 0x04001A5E RID: 6750
		protected PdfContentByte canvas;

		// Token: 0x04001A5F RID: 6751
		protected PdfContentByte[] canvases;

		// Token: 0x04001A60 RID: 6752
		protected int lineStatus;

		// Token: 0x04001A61 RID: 6753
		protected float indent;

		// Token: 0x04001A62 RID: 6754
		protected float followingIndent;

		// Token: 0x04001A63 RID: 6755
		protected float rightIndent;

		// Token: 0x04001A64 RID: 6756
		protected float extraParagraphSpace;

		// Token: 0x04001A65 RID: 6757
		protected float rectangularWidth = -1f;

		// Token: 0x04001A66 RID: 6758
		protected bool rectangularMode;

		// Token: 0x04001A67 RID: 6759
		private float spaceCharRatio = ColumnText.GLOBAL_SPACE_CHAR_RATIO;

		// Token: 0x04001A68 RID: 6760
		private bool lastWasNewline = true;

		// Token: 0x04001A69 RID: 6761
		private int linesWritten;

		// Token: 0x04001A6A RID: 6762
		private float firstLineY;

		// Token: 0x04001A6B RID: 6763
		private bool firstLineYDone;

		// Token: 0x04001A6C RID: 6764
		private int arabicOptions;

		// Token: 0x04001A6D RID: 6765
		protected float descender;

		// Token: 0x04001A6E RID: 6766
		protected bool composite;

		// Token: 0x04001A6F RID: 6767
		protected ColumnText compositeColumn;

		// Token: 0x04001A70 RID: 6768
		protected internal List<IElement> compositeElements;

		// Token: 0x04001A71 RID: 6769
		protected int listIdx;

		// Token: 0x04001A72 RID: 6770
		private bool splittedRow;

		// Token: 0x04001A73 RID: 6771
		protected Phrase waitPhrase;

		// Token: 0x04001A74 RID: 6772
		private bool useAscender;

		// Token: 0x04001A75 RID: 6773
		private float filledWidth;

		// Token: 0x04001A76 RID: 6774
		private bool adjustFirstLine = true;
	}
}
