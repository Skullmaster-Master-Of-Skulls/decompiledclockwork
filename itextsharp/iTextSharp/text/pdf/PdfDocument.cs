using System;
using System.Collections.Generic;
using iTextSharp.text.error_messages;
using iTextSharp.text.pdf.collection;
using iTextSharp.text.pdf.draw;
using iTextSharp.text.pdf.intern;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000528 RID: 1320
	public class PdfDocument : Document
	{
		// Token: 0x06002D1B RID: 11547 RVA: 0x0011280C File Offset: 0x0011180C
		internal PdfDocument()
		{
			base.AddProducer();
			base.AddCreationDate();
		}

		// Token: 0x06002D1C RID: 11548 RVA: 0x001128BC File Offset: 0x001118BC
		internal void AddWriter(PdfWriter writer)
		{
			if (this.writer == null)
			{
				this.writer = writer;
				this.annotationsImp = new PdfAnnotationsImp(writer);
				return;
			}
			throw new DocumentException(MessageLocalization.GetComposedMessage("you.can.only.add.a.writer.to.a.pdfdocument.once"));
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06002D1D RID: 11549 RVA: 0x001128E9 File Offset: 0x001118E9
		// (set) Token: 0x06002D1E RID: 11550 RVA: 0x001128F1 File Offset: 0x001118F1
		public float Leading
		{
			get
			{
				return this.leading;
			}
			set
			{
				this.leading = value;
			}
		}

		// Token: 0x06002D1F RID: 11551 RVA: 0x001128FC File Offset: 0x001118FC
		public override bool Add(IElement element)
		{
			if (this.writer != null && this.writer.IsPaused())
			{
				return false;
			}
			int type = element.Type;
			switch (type)
			{
			case 0:
				this.info.Addkey(((Meta)element).Name, ((Meta)element).Content);
				goto IL_B4A;
			case 1:
				this.info.AddTitle(((Meta)element).Content);
				goto IL_B4A;
			case 2:
				this.info.AddSubject(((Meta)element).Content);
				goto IL_B4A;
			case 3:
				this.info.AddKeywords(((Meta)element).Content);
				goto IL_B4A;
			case 4:
				this.info.AddAuthor(((Meta)element).Content);
				goto IL_B4A;
			case 5:
				this.info.AddProducer();
				goto IL_B4A;
			case 6:
				this.info.AddCreationDate();
				goto IL_B4A;
			case 7:
				this.info.AddCreator(((Meta)element).Content);
				goto IL_B4A;
			case 8:
			case 9:
			case 18:
			case 19:
			case 20:
			case 21:
			case 22:
			case 24:
			case 25:
			case 26:
			case 27:
			case 28:
			case 31:
			case 37:
			case 38:
			case 39:
			case 41:
			case 42:
			case 43:
			case 44:
			case 45:
			case 46:
			case 47:
			case 48:
			case 49:
				break;
			case 10:
			{
				if (this.line == null)
				{
					this.CarriageReturn();
				}
				PdfChunk pdfChunk = new PdfChunk((Chunk)element, this.anchorAction);
				PdfChunk pdfChunk2;
				while ((pdfChunk2 = this.line.Add(pdfChunk)) != null)
				{
					this.CarriageReturn();
					pdfChunk = pdfChunk2;
					pdfChunk.TrimFirstSpace();
				}
				this.pageEmpty = false;
				if (pdfChunk.IsAttribute("NEWPAGE"))
				{
					this.NewPage();
					goto IL_B4A;
				}
				goto IL_B4A;
			}
			case 11:
				this.leadingCount++;
				this.leading = ((Phrase)element).Leading;
				element.Process(this);
				this.leadingCount--;
				goto IL_B4A;
			case 12:
			{
				this.leadingCount++;
				Paragraph paragraph = (Paragraph)element;
				this.AddSpacing(paragraph.SpacingBefore, this.leading, paragraph.Font);
				this.alignment = paragraph.Alignment;
				this.leading = paragraph.TotalLeading;
				this.CarriageReturn();
				if (this.currentHeight + this.line.Height + this.leading > this.IndentTop - this.IndentBottom)
				{
					this.NewPage();
				}
				this.indentation.indentLeft += paragraph.IndentationLeft;
				this.indentation.indentRight += paragraph.IndentationRight;
				this.CarriageReturn();
				IPdfPageEvent pageEvent = this.writer.PageEvent;
				if (pageEvent != null && !this.isSectionTitle)
				{
					pageEvent.OnParagraph(this.writer, this, this.IndentTop - this.currentHeight);
				}
				if (paragraph.KeepTogether)
				{
					this.CarriageReturn();
					PdfPTable pdfPTable = new PdfPTable(1);
					pdfPTable.WidthPercentage = 100f;
					PdfPCell pdfPCell = new PdfPCell();
					pdfPCell.AddElement(paragraph);
					pdfPCell.Border = 0;
					pdfPCell.Padding = 0f;
					pdfPTable.AddCell(pdfPCell);
					this.indentation.indentLeft -= paragraph.IndentationLeft;
					this.indentation.indentRight -= paragraph.IndentationRight;
					this.Add(pdfPTable);
					this.indentation.indentLeft += paragraph.IndentationLeft;
					this.indentation.indentRight += paragraph.IndentationRight;
				}
				else
				{
					this.line.SetExtraIndent(paragraph.FirstLineIndent);
					element.Process(this);
					this.CarriageReturn();
					this.AddSpacing(paragraph.SpacingAfter, paragraph.TotalLeading, paragraph.Font);
				}
				if (pageEvent != null && !this.isSectionTitle)
				{
					pageEvent.OnParagraphEnd(this.writer, this, this.IndentTop - this.currentHeight);
				}
				this.alignment = 0;
				this.indentation.indentLeft -= paragraph.IndentationLeft;
				this.indentation.indentRight -= paragraph.IndentationRight;
				this.CarriageReturn();
				this.leadingCount--;
				goto IL_B4A;
			}
			case 13:
			case 16:
			{
				Section section = (Section)element;
				IPdfPageEvent pageEvent2 = this.writer.PageEvent;
				bool flag = section.NotAddedYet && section.Title != null;
				if (section.TriggerNewPage)
				{
					this.NewPage();
				}
				if (flag)
				{
					float num = this.IndentTop - this.currentHeight;
					int rotation = this.pageSize.Rotation;
					if (rotation == 90 || rotation == 180)
					{
						num = this.pageSize.Height - num;
					}
					PdfDestination destination = new PdfDestination(2, num);
					while (this.currentOutline.Level >= section.Depth)
					{
						this.currentOutline = this.currentOutline.Parent;
					}
					PdfOutline pdfOutline = new PdfOutline(this.currentOutline, destination, section.GetBookmarkTitle(), section.BookmarkOpen);
					this.currentOutline = pdfOutline;
				}
				this.CarriageReturn();
				this.indentation.sectionIndentLeft += section.IndentationLeft;
				this.indentation.sectionIndentRight += section.IndentationRight;
				if (section.NotAddedYet && pageEvent2 != null)
				{
					if (element.Type == 16)
					{
						pageEvent2.OnChapter(this.writer, this, this.IndentTop - this.currentHeight, section.Title);
					}
					else
					{
						pageEvent2.OnSection(this.writer, this, this.IndentTop - this.currentHeight, section.Depth, section.Title);
					}
				}
				if (flag)
				{
					this.isSectionTitle = true;
					this.Add(section.Title);
					this.isSectionTitle = false;
				}
				this.indentation.sectionIndentLeft += section.Indentation;
				element.Process(this);
				this.indentation.sectionIndentLeft -= section.IndentationLeft + section.Indentation;
				this.indentation.sectionIndentRight -= section.IndentationRight;
				if (!section.ElementComplete || pageEvent2 == null)
				{
					goto IL_B4A;
				}
				if (element.Type == 16)
				{
					pageEvent2.OnChapterEnd(this.writer, this, this.IndentTop - this.currentHeight);
					goto IL_B4A;
				}
				pageEvent2.OnSectionEnd(this.writer, this, this.IndentTop - this.currentHeight);
				goto IL_B4A;
			}
			case 14:
			{
				List list = (List)element;
				if (list.Alignindent)
				{
					list.NormalizeIndentation();
				}
				this.indentation.listIndentLeft += list.IndentationLeft;
				this.indentation.indentRight += list.IndentationRight;
				element.Process(this);
				this.indentation.listIndentLeft -= list.IndentationLeft;
				this.indentation.indentRight -= list.IndentationRight;
				this.CarriageReturn();
				goto IL_B4A;
			}
			case 15:
			{
				this.leadingCount++;
				ListItem listItem = (ListItem)element;
				this.AddSpacing(listItem.SpacingBefore, this.leading, listItem.Font);
				this.alignment = listItem.Alignment;
				this.indentation.listIndentLeft += listItem.IndentationLeft;
				this.indentation.indentRight += listItem.IndentationRight;
				this.leading = listItem.TotalLeading;
				this.CarriageReturn();
				this.line.ListItem = listItem;
				element.Process(this);
				this.AddSpacing(listItem.SpacingAfter, listItem.TotalLeading, listItem.Font);
				if (this.line.HasToBeJustified())
				{
					this.line.ResetAlignment();
				}
				this.CarriageReturn();
				this.indentation.listIndentLeft -= listItem.IndentationLeft;
				this.indentation.indentRight -= listItem.IndentationRight;
				this.leadingCount--;
				goto IL_B4A;
			}
			case 17:
			{
				this.leadingCount++;
				Anchor anchor = (Anchor)element;
				string reference = anchor.Reference;
				this.leading = anchor.Leading;
				if (reference != null)
				{
					this.anchorAction = new PdfAction(reference);
				}
				element.Process(this);
				this.anchorAction = null;
				this.leadingCount--;
				goto IL_B4A;
			}
			case 23:
			{
				PdfPTable pdfPTable2 = (PdfPTable)element;
				if (pdfPTable2.Size > pdfPTable2.HeaderRows)
				{
					this.EnsureNewLine();
					this.FlushLines();
					this.AddPTable(pdfPTable2);
					this.pageEmpty = false;
					this.NewLine();
					goto IL_B4A;
				}
				goto IL_B4A;
			}
			case 29:
			{
				if (this.line == null)
				{
					this.CarriageReturn();
				}
				Annotation annotation = (Annotation)element;
				Rectangle defaultRect = new Rectangle(0f, 0f);
				if (this.line != null)
				{
					defaultRect = new Rectangle(annotation.GetLlx(this.IndentRight - this.line.WidthLeft), annotation.GetUry(this.IndentTop - this.currentHeight - 20f), annotation.GetUrx(this.IndentRight - this.line.WidthLeft + 20f), annotation.GetLly(this.IndentTop - this.currentHeight));
				}
				PdfAnnotation annot = PdfAnnotationsImp.ConvertAnnotation(this.writer, annotation, defaultRect);
				this.annotationsImp.AddPlainAnnotation(annot);
				this.pageEmpty = false;
				goto IL_B4A;
			}
			case 30:
			{
				Rectangle rectangle = (Rectangle)element;
				this.graphics.Rectangle(rectangle);
				this.pageEmpty = false;
				goto IL_B4A;
			}
			case 32:
			case 33:
			case 34:
			case 35:
			case 36:
				this.Add((Image)element);
				goto IL_B4A;
			case 40:
			{
				this.EnsureNewLine();
				this.FlushLines();
				MultiColumnText multiColumnText = (MultiColumnText)element;
				float num2 = multiColumnText.Write(this.writer.DirectContent, this, this.IndentTop - this.currentHeight);
				this.currentHeight += num2;
				this.text.MoveText(0f, -1f * num2);
				this.pageEmpty = false;
				goto IL_B4A;
			}
			case 50:
			{
				MarkedObject markedObject;
				if (element is MarkedSection)
				{
					markedObject = ((MarkedSection)element).Title;
					if (markedObject != null)
					{
						markedObject.Process(this);
					}
				}
				markedObject = (MarkedObject)element;
				markedObject.Process(this);
				goto IL_B4A;
			}
			default:
				if (type == 55)
				{
					IDrawInterface drawInterface = (IDrawInterface)element;
					drawInterface.Draw(this.graphics, this.IndentLeft, this.IndentBottom, this.IndentRight, this.IndentTop, this.IndentTop - this.currentHeight - ((this.leadingCount > 0) ? this.leading : 0f));
					this.pageEmpty = false;
					goto IL_B4A;
				}
				break;
			}
			return false;
			IL_B4A:
			this.lastElementType = element.Type;
			return true;
		}

		// Token: 0x06002D20 RID: 11552 RVA: 0x00113460 File Offset: 0x00112460
		public override void Open()
		{
			if (!this.open)
			{
				base.Open();
				this.writer.Open();
				this.rootOutline = new PdfOutline(this.writer);
				this.currentOutline = this.rootOutline;
			}
			this.InitPage();
		}

		// Token: 0x06002D21 RID: 11553 RVA: 0x001134A0 File Offset: 0x001124A0
		public override void Close()
		{
			if (this.close)
			{
				return;
			}
			bool flag = this.imageWait != null;
			this.NewPage();
			if (this.imageWait != null || flag)
			{
				this.NewPage();
			}
			if (this.annotationsImp.HasUnusedAnnotations())
			{
				throw new Exception(MessageLocalization.GetComposedMessage("not.all.annotations.could.be.added.to.the.document.the.document.doesn.t.have.enough.pages"));
			}
			IPdfPageEvent pageEvent = this.writer.PageEvent;
			if (pageEvent != null)
			{
				pageEvent.OnCloseDocument(this.writer, this);
			}
			base.Close();
			this.writer.AddLocalDestinations(this.localDestinations);
			this.CalculateOutlineCount();
			this.WriteOutlines();
			this.writer.Close();
		}

		// Token: 0x170007C6 RID: 1990
		// (set) Token: 0x06002D22 RID: 11554 RVA: 0x00113543 File Offset: 0x00112543
		public byte[] XmpMetadata
		{
			set
			{
				this.xmpMetadata = value;
			}
		}

		// Token: 0x06002D23 RID: 11555 RVA: 0x0011354C File Offset: 0x0011254C
		public override bool NewPage()
		{
			this.lastElementType = -1;
			if (this.PageEmpty)
			{
				this.SetNewPageSizeAndMargins();
				return false;
			}
			if (!this.open || this.close)
			{
				throw new Exception(MessageLocalization.GetComposedMessage("the.document.is.not.open"));
			}
			IPdfPageEvent pageEvent = this.writer.PageEvent;
			if (pageEvent != null)
			{
				pageEvent.OnEndPage(this.writer, this);
			}
			base.NewPage();
			this.indentation.imageIndentLeft = 0f;
			this.indentation.imageIndentRight = 0f;
			this.FlushLines();
			int rotation = this.pageSize.Rotation;
			if (this.writer.IsPdfX())
			{
				if (this.thisBoxSize.ContainsKey("art") && this.thisBoxSize.ContainsKey("trim"))
				{
					throw new PdfXConformanceException(MessageLocalization.GetComposedMessage("only.one.of.artbox.or.trimbox.can.exist.in.the.page"));
				}
				if (!this.thisBoxSize.ContainsKey("art") && !this.thisBoxSize.ContainsKey("trim"))
				{
					if (this.thisBoxSize.ContainsKey("crop"))
					{
						this.thisBoxSize["trim"] = this.thisBoxSize["crop"];
					}
					else
					{
						this.thisBoxSize["trim"] = new PdfRectangle(this.pageSize, this.pageSize.Rotation);
					}
				}
			}
			this.pageResources.AddDefaultColorDiff(this.writer.DefaultColorspace);
			if (this.writer.RgbTransparencyBlending)
			{
				PdfDictionary pdfDictionary = new PdfDictionary();
				pdfDictionary.Put(PdfName.CS, PdfName.DEVICERGB);
				this.pageResources.AddDefaultColorDiff(pdfDictionary);
			}
			PdfDictionary resources = this.pageResources.Resources;
			PdfPage pdfPage = new PdfPage(new PdfRectangle(this.pageSize, rotation), this.thisBoxSize, resources, rotation);
			pdfPage.Put(PdfName.TABS, this.writer.Tabs);
			if (this.xmpMetadata != null)
			{
				PdfStream pdfStream = new PdfStream(this.xmpMetadata);
				pdfStream.Put(PdfName.TYPE, PdfName.METADATA);
				pdfStream.Put(PdfName.SUBTYPE, PdfName.XML);
				PdfEncryption encryption = this.writer.Encryption;
				if (encryption != null && !encryption.IsMetadataEncrypted())
				{
					PdfArray pdfArray = new PdfArray();
					pdfArray.Add(PdfName.CRYPT);
					pdfStream.Put(PdfName.FILTER, pdfArray);
				}
				pdfPage.Put(PdfName.METADATA, this.writer.AddToBody(pdfStream).IndirectReference);
			}
			if (this.transition != null)
			{
				pdfPage.Put(PdfName.TRANS, this.transition.TransitionDictionary);
				this.transition = null;
			}
			if (this.duration > 0)
			{
				pdfPage.Put(PdfName.DUR, new PdfNumber(this.duration));
				this.duration = 0;
			}
			if (this.pageAA != null)
			{
				pdfPage.Put(PdfName.AA, this.writer.AddToBody(this.pageAA).IndirectReference);
				this.pageAA = null;
			}
			if (this.thumb != null)
			{
				pdfPage.Put(PdfName.THUMB, this.thumb);
				this.thumb = null;
			}
			if (this.writer.Userunit > 0f)
			{
				pdfPage.Put(PdfName.USERUNIT, new PdfNumber(this.writer.Userunit));
			}
			if (this.annotationsImp.HasUnusedAnnotations())
			{
				PdfArray pdfArray2 = this.annotationsImp.RotateAnnotations(this.writer, this.pageSize);
				if (pdfArray2.Size != 0)
				{
					pdfPage.Put(PdfName.ANNOTS, pdfArray2);
				}
			}
			if (this.writer.IsTagged())
			{
				pdfPage.Put(PdfName.STRUCTPARENTS, new PdfNumber(this.writer.CurrentPageNumber - 1));
			}
			if (this.text.Size > this.textEmptySize)
			{
				this.text.EndText();
			}
			else
			{
				this.text = null;
			}
			this.writer.Add(pdfPage, new PdfContents(this.writer.DirectContentUnder, this.graphics, this.text, this.writer.DirectContent, this.pageSize));
			this.InitPage();
			return true;
		}

		// Token: 0x06002D24 RID: 11556 RVA: 0x0011395D File Offset: 0x0011295D
		public override bool SetPageSize(Rectangle pageSize)
		{
			if (this.writer != null && this.writer.IsPaused())
			{
				return false;
			}
			this.nextPageSize = new Rectangle(pageSize);
			return true;
		}

		// Token: 0x06002D25 RID: 11557 RVA: 0x00113983 File Offset: 0x00112983
		public override bool SetMargins(float marginLeft, float marginRight, float marginTop, float marginBottom)
		{
			if (this.writer != null && this.writer.IsPaused())
			{
				return false;
			}
			this.nextMarginLeft = marginLeft;
			this.nextMarginRight = marginRight;
			this.nextMarginTop = marginTop;
			this.nextMarginBottom = marginBottom;
			return true;
		}

		// Token: 0x06002D26 RID: 11558 RVA: 0x001139BA File Offset: 0x001129BA
		public override bool SetMarginMirroring(bool MarginMirroring)
		{
			return (this.writer == null || !this.writer.IsPaused()) && base.SetMarginMirroring(MarginMirroring);
		}

		// Token: 0x06002D27 RID: 11559 RVA: 0x001139DA File Offset: 0x001129DA
		public override bool SetMarginMirroringTopBottom(bool MarginMirroringTopBottom)
		{
			return (this.writer == null || !this.writer.IsPaused()) && base.SetMarginMirroringTopBottom(MarginMirroringTopBottom);
		}

		// Token: 0x170007C7 RID: 1991
		// (set) Token: 0x06002D28 RID: 11560 RVA: 0x001139FA File Offset: 0x001129FA
		public override int PageCount
		{
			set
			{
				if (this.writer != null && this.writer.IsPaused())
				{
					return;
				}
				base.PageCount = value;
			}
		}

		// Token: 0x06002D29 RID: 11561 RVA: 0x00113A19 File Offset: 0x00112A19
		public override void ResetPageCount()
		{
			if (this.writer != null && this.writer.IsPaused())
			{
				return;
			}
			base.ResetPageCount();
		}

		// Token: 0x06002D2A RID: 11562 RVA: 0x00113A38 File Offset: 0x00112A38
		protected internal void InitPage()
		{
			this.pageN++;
			this.annotationsImp.ResetAnnotations();
			this.pageResources = new PageResources();
			this.writer.ResetContent();
			this.graphics = new PdfContentByte(this.writer);
			this.text = new PdfContentByte(this.writer);
			this.text.Reset();
			this.text.BeginText();
			this.textEmptySize = this.text.Size;
			this.markPoint = 0;
			this.SetNewPageSizeAndMargins();
			this.imageEnd = -1f;
			this.indentation.imageIndentRight = 0f;
			this.indentation.imageIndentLeft = 0f;
			this.indentation.indentBottom = 0f;
			this.indentation.indentTop = 0f;
			this.currentHeight = 0f;
			this.thisBoxSize = new Dictionary<string, PdfRectangle>(this.boxSize);
			if (this.pageSize.BackgroundColor != null || this.pageSize.HasBorders() || this.pageSize.BorderColor != null)
			{
				this.Add(this.pageSize);
			}
			float num = this.leading;
			int num2 = this.alignment;
			this.text.MoveText(base.Left, base.Top);
			this.pageEmpty = true;
			if (this.imageWait != null)
			{
				this.Add(this.imageWait);
				this.imageWait = null;
			}
			this.leading = num;
			this.alignment = num2;
			this.CarriageReturn();
			IPdfPageEvent pageEvent = this.writer.PageEvent;
			if (pageEvent != null)
			{
				if (this.firstPageEvent)
				{
					pageEvent.OnOpenDocument(this.writer, this);
				}
				pageEvent.OnStartPage(this.writer, this);
			}
			this.firstPageEvent = false;
		}

		// Token: 0x06002D2B RID: 11563 RVA: 0x00113BF8 File Offset: 0x00112BF8
		protected internal void NewLine()
		{
			this.lastElementType = -1;
			this.CarriageReturn();
			if (this.lines != null && this.lines.Count > 0)
			{
				this.lines.Add(this.line);
				this.currentHeight += this.line.Height;
			}
			this.line = new PdfLine(this.IndentLeft, this.IndentRight, this.alignment, this.leading);
		}

		// Token: 0x06002D2C RID: 11564 RVA: 0x00113C74 File Offset: 0x00112C74
		protected internal void CarriageReturn()
		{
			if (this.lines == null)
			{
				this.lines = new List<PdfLine>();
			}
			if (this.line != null)
			{
				if (this.currentHeight + this.line.Height + this.leading < this.IndentTop - this.IndentBottom)
				{
					if (this.line.Size > 0)
					{
						this.currentHeight += this.line.Height;
						this.lines.Add(this.line);
						this.pageEmpty = false;
					}
				}
				else
				{
					this.NewPage();
				}
			}
			if (this.imageEnd > -1f && this.currentHeight > this.imageEnd)
			{
				this.imageEnd = -1f;
				this.indentation.imageIndentRight = 0f;
				this.indentation.imageIndentLeft = 0f;
			}
			this.line = new PdfLine(this.IndentLeft, this.IndentRight, this.alignment, this.leading);
		}

		// Token: 0x06002D2D RID: 11565 RVA: 0x00113D74 File Offset: 0x00112D74
		public float GetVerticalPosition(bool ensureNewLine)
		{
			if (ensureNewLine)
			{
				this.EnsureNewLine();
			}
			return base.Top - this.currentHeight - this.indentation.indentTop;
		}

		// Token: 0x06002D2E RID: 11566 RVA: 0x00113D98 File Offset: 0x00112D98
		protected internal void EnsureNewLine()
		{
			if (this.lastElementType == 11 || this.lastElementType == 10)
			{
				this.NewLine();
				this.FlushLines();
			}
		}

		// Token: 0x06002D2F RID: 11567 RVA: 0x00113DBC File Offset: 0x00112DBC
		protected internal float FlushLines()
		{
			if (this.lines == null)
			{
				return 0f;
			}
			if (this.line != null && this.line.Size > 0)
			{
				this.lines.Add(this.line);
				this.line = new PdfLine(this.IndentLeft, this.IndentRight, this.alignment, this.leading);
			}
			if (this.lines.Count == 0)
			{
				return 0f;
			}
			object[] array = new object[2];
			PdfFont pdfFont = null;
			float num = 0f;
			array[1] = 0f;
			foreach (PdfLine pdfLine in this.lines)
			{
				float num2 = pdfLine.IndentLeft - this.IndentLeft + this.indentation.indentLeft + this.indentation.listIndentLeft + this.indentation.sectionIndentLeft;
				this.text.MoveText(num2, -pdfLine.Height);
				if (pdfLine.ListSymbol != null)
				{
					ColumnText.ShowTextAligned(this.graphics, 0, new Phrase(pdfLine.ListSymbol), this.text.XTLM - pdfLine.ListIndent, this.text.YTLM, 0f);
				}
				array[0] = pdfFont;
				this.WriteLineToContent(pdfLine, this.text, this.graphics, array, this.writer.SpaceCharRatio);
				pdfFont = (PdfFont)array[0];
				num += pdfLine.Height;
				this.text.MoveText(-num2, 0f);
			}
			this.lines = new List<PdfLine>();
			return num;
		}

		// Token: 0x06002D30 RID: 11568 RVA: 0x00113F74 File Offset: 0x00112F74
		internal float WriteLineToContent(PdfLine line, PdfContentByte text, PdfContentByte graphics, object[] currentValues, float ratio)
		{
			PdfFont pdfFont = (PdfFont)currentValues[0];
			float num = (float)currentValues[1];
			float num2 = 0f;
			float num3 = float.NaN;
			float num4 = 0f;
			float num5 = 0f;
			float num6 = 0f;
			float num7 = text.XTLM + line.OriginalWidth;
			int numberOfSpaces = line.NumberOfSpaces;
			int lineLengthUtf = line.GetLineLengthUtf32();
			bool flag = line.HasToBeJustified() && (numberOfSpaces != 0 || lineLengthUtf > 1);
			int separatorCount = line.GetSeparatorCount();
			if (separatorCount > 0)
			{
				num6 = line.WidthLeft / (float)separatorCount;
			}
			else if (flag && separatorCount == 0)
			{
				if (line.NewlineSplit && line.WidthLeft >= num * (ratio * (float)numberOfSpaces + (float)lineLengthUtf - 1f))
				{
					if (line.RTL)
					{
						text.MoveText(line.WidthLeft - num * (ratio * (float)numberOfSpaces + (float)lineLengthUtf - 1f), 0f);
					}
					num4 = ratio * num;
					num5 = num;
				}
				else
				{
					float num8 = line.WidthLeft;
					PdfChunk chunk = line.GetChunk(line.Size - 1);
					if (chunk != null)
					{
						string text2 = chunk.ToString();
						char character;
						if (text2.Length > 0 && ".,;:'".IndexOf(character = text2[text2.Length - 1]) >= 0)
						{
							float num9 = num8;
							num8 += chunk.Font.Width((int)character) * 0.4f;
							num2 = num8 - num9;
						}
					}
					float num10 = num8 / (ratio * (float)numberOfSpaces + (float)lineLengthUtf - 1f);
					num4 = ratio * num10;
					num5 = num10;
					num = num10;
				}
			}
			else if (line.alignment == 0 || line.alignment == -1)
			{
				num7 -= line.WidthLeft;
			}
			int lastStrokeChunk = line.LastStrokeChunk;
			int num11 = 0;
			float num12 = text.XTLM;
			float num13 = num12;
			float ytlm = text.YTLM;
			bool flag2 = false;
			float num14 = 0f;
			foreach (PdfChunk pdfChunk in line)
			{
				BaseColor color = pdfChunk.Color;
				float size = pdfChunk.Font.Size;
				float fontDescriptor = pdfChunk.Font.Font.GetFontDescriptor(1, size);
				float fontDescriptor2 = pdfChunk.Font.Font.GetFontDescriptor(3, size);
				float num15 = 1f;
				if (num11 <= lastStrokeChunk)
				{
					float num16;
					if (flag)
					{
						num16 = pdfChunk.GetWidthCorrected(num5, num4);
					}
					else
					{
						num16 = pdfChunk.Width;
					}
					if (pdfChunk.IsStroked())
					{
						PdfChunk chunk2 = line.GetChunk(num11 + 1);
						if (pdfChunk.IsSeparator())
						{
							num16 = num6;
							object[] array = (object[])pdfChunk.GetAttribute("SEPARATOR");
							IDrawInterface drawInterface = (IDrawInterface)array[0];
							bool flag3 = (bool)array[1];
							if (flag3)
							{
								drawInterface.Draw(graphics, num13, ytlm + fontDescriptor2, num13 + line.OriginalWidth, fontDescriptor - fontDescriptor2, ytlm);
							}
							else
							{
								drawInterface.Draw(graphics, num12, ytlm + fontDescriptor2, num12 + num16, fontDescriptor - fontDescriptor2, ytlm);
							}
						}
						if (pdfChunk.IsTab())
						{
							object[] array2 = (object[])pdfChunk.GetAttribute("TAB");
							IDrawInterface drawInterface2 = (IDrawInterface)array2[0];
							num14 = (float)array2[1] + (float)array2[3];
							if (num14 > num12)
							{
								drawInterface2.Draw(graphics, num12, ytlm + fontDescriptor2, num14, fontDescriptor - fontDescriptor2, ytlm);
							}
							float num17 = num12;
							num12 = num14;
							num14 = num17;
						}
						if (pdfChunk.IsAttribute("BACKGROUND"))
						{
							float num18 = num;
							if (chunk2 != null && chunk2.IsAttribute("BACKGROUND"))
							{
								num18 = 0f;
							}
							if (chunk2 == null)
							{
								num18 += num2;
							}
							object[] array3 = (object[])pdfChunk.GetAttribute("BACKGROUND");
							graphics.SetColorFill((BaseColor)array3[0]);
							float[] array4 = (float[])array3[1];
							graphics.Rectangle(num12 - array4[0], ytlm + fontDescriptor2 - array4[1] + pdfChunk.TextRise, num16 - num18 + array4[0] + array4[2], fontDescriptor - fontDescriptor2 + array4[1] + array4[3]);
							graphics.Fill();
							graphics.SetGrayFill(0f);
						}
						if (pdfChunk.IsAttribute("UNDERLINE"))
						{
							float num19 = num;
							if (chunk2 != null && chunk2.IsAttribute("UNDERLINE"))
							{
								num19 = 0f;
							}
							if (chunk2 == null)
							{
								num19 += num2;
							}
							foreach (object[] array6 in (object[][])pdfChunk.GetAttribute("UNDERLINE"))
							{
								BaseColor baseColor = (BaseColor)array6[0];
								float[] array7 = (float[])array6[1];
								if (baseColor == null)
								{
									baseColor = color;
								}
								if (baseColor != null)
								{
									graphics.SetColorStroke(baseColor);
								}
								graphics.SetLineWidth(array7[0] + size * array7[1]);
								float num20 = array7[2] + size * array7[3];
								int num21 = (int)array7[4];
								if (num21 != 0)
								{
									graphics.SetLineCap(num21);
								}
								graphics.MoveTo(num12, ytlm + num20);
								graphics.LineTo(num12 + num16 - num19, ytlm + num20);
								graphics.Stroke();
								if (baseColor != null)
								{
									graphics.ResetGrayStroke();
								}
								if (num21 != 0)
								{
									graphics.SetLineCap(0);
								}
							}
							graphics.SetLineWidth(1f);
						}
						if (pdfChunk.IsAttribute("ACTION"))
						{
							float num22 = num;
							if (chunk2 != null && chunk2.IsAttribute("ACTION"))
							{
								num22 = 0f;
							}
							if (chunk2 == null)
							{
								num22 += num2;
							}
							text.AddAnnotation(new PdfAnnotation(this.writer, num12, ytlm + fontDescriptor2 + pdfChunk.TextRise, num12 + num16 - num22, ytlm + fontDescriptor + pdfChunk.TextRise, (PdfAction)pdfChunk.GetAttribute("ACTION")));
						}
						if (pdfChunk.IsAttribute("REMOTEGOTO"))
						{
							float num23 = num;
							if (chunk2 != null && chunk2.IsAttribute("REMOTEGOTO"))
							{
								num23 = 0f;
							}
							if (chunk2 == null)
							{
								num23 += num2;
							}
							object[] array8 = (object[])pdfChunk.GetAttribute("REMOTEGOTO");
							string filename = (string)array8[0];
							if (array8[1] is string)
							{
								this.RemoteGoto(filename, (string)array8[1], num12, ytlm + fontDescriptor2 + pdfChunk.TextRise, num12 + num16 - num23, ytlm + fontDescriptor + pdfChunk.TextRise);
							}
							else
							{
								this.RemoteGoto(filename, (int)array8[1], num12, ytlm + fontDescriptor2 + pdfChunk.TextRise, num12 + num16 - num23, ytlm + fontDescriptor + pdfChunk.TextRise);
							}
						}
						if (pdfChunk.IsAttribute("LOCALGOTO"))
						{
							float num24 = num;
							if (chunk2 != null && chunk2.IsAttribute("LOCALGOTO"))
							{
								num24 = 0f;
							}
							if (chunk2 == null)
							{
								num24 += num2;
							}
							this.LocalGoto((string)pdfChunk.GetAttribute("LOCALGOTO"), num12, ytlm, num12 + num16 - num24, ytlm + size);
						}
						if (pdfChunk.IsAttribute("LOCALDESTINATION"))
						{
							float num25 = num;
							if (chunk2 != null && chunk2.IsAttribute("LOCALDESTINATION"))
							{
								num25 = 0f;
							}
							if (chunk2 == null)
							{
								num25 += num2;
							}
							this.LocalDestination((string)pdfChunk.GetAttribute("LOCALDESTINATION"), new PdfDestination(0, num12, ytlm + size, 0f));
						}
						if (pdfChunk.IsAttribute("GENERICTAG"))
						{
							float num26 = num;
							if (chunk2 != null && chunk2.IsAttribute("GENERICTAG"))
							{
								num26 = 0f;
							}
							if (chunk2 == null)
							{
								num26 += num2;
							}
							Rectangle rect = new Rectangle(num12, ytlm, num12 + num16 - num26, ytlm + size);
							IPdfPageEvent pageEvent = this.writer.PageEvent;
							if (pageEvent != null)
							{
								pageEvent.OnGenericTag(this.writer, this, rect, (string)pdfChunk.GetAttribute("GENERICTAG"));
							}
						}
						if (pdfChunk.IsAttribute("PDFANNOTATION"))
						{
							float num27 = num;
							if (chunk2 != null && chunk2.IsAttribute("PDFANNOTATION"))
							{
								num27 = 0f;
							}
							if (chunk2 == null)
							{
								num27 += num2;
							}
							PdfAnnotation pdfAnnotation = PdfAnnotation.ShallowDuplicate((PdfAnnotation)pdfChunk.GetAttribute("PDFANNOTATION"));
							pdfAnnotation.Put(PdfName.RECT, new PdfRectangle(num12, ytlm + fontDescriptor2, num12 + num16 - num27, ytlm + fontDescriptor));
							text.AddAnnotation(pdfAnnotation);
						}
						float[] array9 = (float[])pdfChunk.GetAttribute("SKEW");
						object attribute = pdfChunk.GetAttribute("HSCALE");
						if (array9 != null || attribute != null)
						{
							float b = 0f;
							float c = 0f;
							if (array9 != null)
							{
								b = array9[0];
								c = array9[1];
							}
							if (attribute != null)
							{
								num15 = (float)attribute;
							}
							text.SetTextMatrix(num15, b, c, 1f, num12, ytlm);
						}
						if (pdfChunk.IsAttribute("CHAR_SPACING"))
						{
							float characterSpacing = (float)pdfChunk.GetAttribute("CHAR_SPACING");
							text.SetCharacterSpacing(characterSpacing);
						}
						if (pdfChunk.IsImage())
						{
							Image image = pdfChunk.Image;
							float[] matrix = image.Matrix;
							matrix[4] = num12 + pdfChunk.ImageOffsetX - matrix[4];
							matrix[5] = ytlm + pdfChunk.ImageOffsetY - matrix[5];
							graphics.AddImage(image, matrix[0], matrix[1], matrix[2], matrix[3], matrix[4], matrix[5]);
							text.MoveText(num12 + num + image.ScaledWidth - text.XTLM, 0f);
						}
					}
					num12 += num16;
					num11++;
				}
				if (pdfChunk.Font.CompareTo(pdfFont) != 0)
				{
					pdfFont = pdfChunk.Font;
					text.SetFontAndSize(pdfFont.Font, pdfFont.Size);
				}
				float num28 = 0f;
				object[] array10 = (object[])pdfChunk.GetAttribute("TEXTRENDERMODE");
				int num29 = 0;
				float num30 = 1f;
				BaseColor baseColor2 = null;
				object attribute2 = pdfChunk.GetAttribute("SUBSUPSCRIPT");
				if (array10 != null)
				{
					num29 = ((int)array10[0] & 3);
					if (num29 != 0)
					{
						text.SetTextRenderingMode(num29);
					}
					if (num29 == 1 || num29 == 2)
					{
						num30 = (float)array10[1];
						if (num30 != 1f)
						{
							text.SetLineWidth(num30);
						}
						baseColor2 = (BaseColor)array10[2];
						if (baseColor2 == null)
						{
							baseColor2 = color;
						}
						if (baseColor2 != null)
						{
							text.SetColorStroke(baseColor2);
						}
					}
				}
				if (attribute2 != null)
				{
					num28 = (float)attribute2;
				}
				if (color != null)
				{
					text.SetColorFill(color);
				}
				if (num28 != 0f)
				{
					text.SetTextRise(num28);
				}
				if (pdfChunk.IsImage())
				{
					flag2 = true;
				}
				else if (pdfChunk.IsHorizontalSeparator())
				{
					PdfTextArray pdfTextArray = new PdfTextArray();
					pdfTextArray.Add(-num6 * 1000f / pdfChunk.Font.Size / num15);
					text.ShowText(pdfTextArray);
				}
				else if (pdfChunk.IsTab())
				{
					PdfTextArray pdfTextArray2 = new PdfTextArray();
					pdfTextArray2.Add((num14 - num12) * 1000f / pdfChunk.Font.Size / num15);
					text.ShowText(pdfTextArray2);
				}
				else if (flag && numberOfSpaces > 0 && pdfChunk.IsSpecialEncoding())
				{
					if (num15 != num3)
					{
						num3 = num15;
						text.SetWordSpacing(num4 / num15);
						text.SetCharacterSpacing(num5 / num15 + text.CharacterSpacing);
					}
					string text3 = pdfChunk.ToString();
					int num31 = text3.IndexOf(' ');
					if (num31 < 0)
					{
						text.ShowText(text3);
					}
					else
					{
						float number = -num4 * 1000f / pdfChunk.Font.Size / num15;
						PdfTextArray pdfTextArray3 = new PdfTextArray(text3.Substring(0, num31));
						int num32 = num31;
						while ((num31 = text3.IndexOf(' ', num32 + 1)) >= 0)
						{
							pdfTextArray3.Add(number);
							pdfTextArray3.Add(text3.Substring(num32, num31 - num32));
							num32 = num31;
						}
						pdfTextArray3.Add(number);
						pdfTextArray3.Add(text3.Substring(num32));
						text.ShowText(pdfTextArray3);
					}
				}
				else
				{
					if (flag && num15 != num3)
					{
						num3 = num15;
						text.SetWordSpacing(num4 / num15);
						text.SetCharacterSpacing(num5 / num15 + text.CharacterSpacing);
					}
					text.ShowText(pdfChunk.ToString());
				}
				if (num28 != 0f)
				{
					text.SetTextRise(0f);
				}
				if (color != null)
				{
					text.ResetRGBColorFill();
				}
				if (num29 != 0)
				{
					text.SetTextRenderingMode(0);
				}
				if (baseColor2 != null)
				{
					text.ResetRGBColorStroke();
				}
				if (num30 != 1f)
				{
					text.SetLineWidth(1f);
				}
				if (pdfChunk.IsAttribute("SKEW") || pdfChunk.IsAttribute("HSCALE"))
				{
					flag2 = true;
					text.SetTextMatrix(num12, ytlm);
				}
				if (pdfChunk.IsAttribute("CHAR_SPACING"))
				{
					text.SetCharacterSpacing(num5);
				}
			}
			if (flag)
			{
				text.SetWordSpacing(0f);
				text.SetCharacterSpacing(0f);
				if (line.NewlineSplit)
				{
					num = 0f;
				}
			}
			if (flag2)
			{
				text.MoveText(num13 - text.XTLM, 0f);
			}
			currentValues[0] = pdfFont;
			currentValues[1] = num;
			return num7;
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06002D31 RID: 11569 RVA: 0x00114C58 File Offset: 0x00113C58
		protected internal float IndentLeft
		{
			get
			{
				return base.GetLeft(this.indentation.indentLeft + this.indentation.listIndentLeft + this.indentation.imageIndentLeft + this.indentation.sectionIndentLeft);
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06002D32 RID: 11570 RVA: 0x00114C8F File Offset: 0x00113C8F
		protected internal float IndentRight
		{
			get
			{
				return base.GetRight(this.indentation.indentRight + this.indentation.sectionIndentRight + this.indentation.imageIndentRight);
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06002D33 RID: 11571 RVA: 0x00114CBA File Offset: 0x00113CBA
		protected internal float IndentTop
		{
			get
			{
				return base.GetTop(this.indentation.indentTop);
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06002D34 RID: 11572 RVA: 0x00114CCD File Offset: 0x00113CCD
		protected internal float IndentBottom
		{
			get
			{
				return base.GetBottom(this.indentation.indentBottom);
			}
		}

		// Token: 0x06002D35 RID: 11573 RVA: 0x00114CE0 File Offset: 0x00113CE0
		protected internal void AddSpacing(float extraspace, float oldleading, Font f)
		{
			if (extraspace == 0f)
			{
				return;
			}
			if (this.pageEmpty)
			{
				return;
			}
			if (this.currentHeight + this.line.Height + this.leading > this.IndentTop - this.IndentBottom)
			{
				return;
			}
			this.leading = extraspace;
			this.CarriageReturn();
			if (f.IsUnderlined() || f.IsStrikethru())
			{
				f = new Font(f);
				int num = f.Style;
				num &= -5;
				num &= -9;
				f.SetStyle(num);
			}
			Chunk chunk = new Chunk(" ", f);
			chunk.Process(this);
			this.CarriageReturn();
			this.leading = oldleading;
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06002D36 RID: 11574 RVA: 0x00114D86 File Offset: 0x00113D86
		internal PdfDocument.PdfInfo Info
		{
			get
			{
				return this.info;
			}
		}

		// Token: 0x06002D37 RID: 11575 RVA: 0x00114D90 File Offset: 0x00113D90
		internal PdfDocument.PdfCatalog GetCatalog(PdfIndirectReference pages)
		{
			PdfDocument.PdfCatalog pdfCatalog = new PdfDocument.PdfCatalog(pages, this.writer);
			if (this.rootOutline.Kids.Count > 0)
			{
				pdfCatalog.Put(PdfName.PAGEMODE, PdfName.USEOUTLINES);
				pdfCatalog.Put(PdfName.OUTLINES, this.rootOutline.IndirectReference);
			}
			this.writer.GetPdfVersion().AddToCatalog(pdfCatalog);
			this.viewerPreferences.AddToCatalog(pdfCatalog);
			if (this.pageLabels != null)
			{
				pdfCatalog.Put(PdfName.PAGELABELS, this.pageLabels.GetDictionary(this.writer));
			}
			pdfCatalog.AddNames(this.localDestinations, this.GetDocumentLevelJS(), this.documentFileAttachment, this.writer);
			if (this.openActionName != null)
			{
				PdfAction localGotoAction = this.GetLocalGotoAction(this.openActionName);
				pdfCatalog.OpenAction = localGotoAction;
			}
			else if (this.openActionAction != null)
			{
				pdfCatalog.OpenAction = this.openActionAction;
			}
			if (this.additionalActions != null)
			{
				pdfCatalog.AdditionalActions = this.additionalActions;
			}
			if (this.collection != null)
			{
				pdfCatalog.Put(PdfName.COLLECTION, this.collection);
			}
			if (this.annotationsImp.HasValidAcroForm())
			{
				pdfCatalog.Put(PdfName.ACROFORM, this.writer.AddToBody(this.annotationsImp.AcroForm).IndirectReference);
			}
			return pdfCatalog;
		}

		// Token: 0x06002D38 RID: 11576 RVA: 0x00114ED5 File Offset: 0x00113ED5
		internal void AddOutline(PdfOutline outline, string name)
		{
			this.LocalDestination(name, outline.PdfDestination);
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06002D39 RID: 11577 RVA: 0x00114EE5 File Offset: 0x00113EE5
		public PdfOutline RootOutline
		{
			get
			{
				return this.rootOutline;
			}
		}

		// Token: 0x06002D3A RID: 11578 RVA: 0x00114EED File Offset: 0x00113EED
		internal void CalculateOutlineCount()
		{
			if (this.rootOutline.Kids.Count == 0)
			{
				return;
			}
			this.TraverseOutlineCount(this.rootOutline);
		}

		// Token: 0x06002D3B RID: 11579 RVA: 0x00114F10 File Offset: 0x00113F10
		internal void TraverseOutlineCount(PdfOutline outline)
		{
			List<PdfOutline> kids = outline.Kids;
			PdfOutline parent = outline.Parent;
			if (kids.Count == 0)
			{
				if (parent != null)
				{
					parent.Count++;
					return;
				}
			}
			else
			{
				for (int i = 0; i < kids.Count; i++)
				{
					this.TraverseOutlineCount(kids[i]);
				}
				if (parent != null)
				{
					if (outline.Open)
					{
						parent.Count = outline.Count + parent.Count + 1;
						return;
					}
					parent.Count++;
					outline.Count = -outline.Count;
				}
			}
		}

		// Token: 0x06002D3C RID: 11580 RVA: 0x00114F9F File Offset: 0x00113F9F
		internal void WriteOutlines()
		{
			if (this.rootOutline.Kids.Count == 0)
			{
				return;
			}
			this.OutlineTree(this.rootOutline);
			this.writer.AddToBody(this.rootOutline, this.rootOutline.IndirectReference);
		}

		// Token: 0x06002D3D RID: 11581 RVA: 0x00114FE0 File Offset: 0x00113FE0
		internal void OutlineTree(PdfOutline outline)
		{
			outline.IndirectReference = this.writer.PdfIndirectReference;
			if (outline.Parent != null)
			{
				outline.Put(PdfName.PARENT, outline.Parent.IndirectReference);
			}
			List<PdfOutline> kids = outline.Kids;
			int count = kids.Count;
			for (int i = 0; i < count; i++)
			{
				this.OutlineTree(kids[i]);
			}
			for (int j = 0; j < count; j++)
			{
				if (j > 0)
				{
					kids[j].Put(PdfName.PREV, kids[j - 1].IndirectReference);
				}
				if (j < count - 1)
				{
					kids[j].Put(PdfName.NEXT, kids[j + 1].IndirectReference);
				}
			}
			if (count > 0)
			{
				outline.Put(PdfName.FIRST, kids[0].IndirectReference);
				outline.Put(PdfName.LAST, kids[count - 1].IndirectReference);
			}
			for (int k = 0; k < count; k++)
			{
				PdfOutline pdfOutline = kids[k];
				this.writer.AddToBody(pdfOutline, pdfOutline.IndirectReference);
			}
		}

		// Token: 0x170007CE RID: 1998
		// (set) Token: 0x06002D3E RID: 11582 RVA: 0x001150FA File Offset: 0x001140FA
		internal int ViewerPreferences
		{
			set
			{
				this.viewerPreferences.ViewerPreferences = value;
			}
		}

		// Token: 0x06002D3F RID: 11583 RVA: 0x00115108 File Offset: 0x00114108
		internal void AddViewerPreference(PdfName key, PdfObject value)
		{
			this.viewerPreferences.AddViewerPreference(key, value);
		}

		// Token: 0x170007CF RID: 1999
		// (set) Token: 0x06002D40 RID: 11584 RVA: 0x00115117 File Offset: 0x00114117
		internal PdfPageLabels PageLabels
		{
			set
			{
				this.pageLabels = value;
			}
		}

		// Token: 0x06002D41 RID: 11585 RVA: 0x00115120 File Offset: 0x00114120
		internal void LocalGoto(string name, float llx, float lly, float urx, float ury)
		{
			PdfAction localGotoAction = this.GetLocalGotoAction(name);
			this.annotationsImp.AddPlainAnnotation(new PdfAnnotation(this.writer, llx, lly, urx, ury, localGotoAction));
		}

		// Token: 0x06002D42 RID: 11586 RVA: 0x00115154 File Offset: 0x00114154
		internal void RemoteGoto(string filename, string name, float llx, float lly, float urx, float ury)
		{
			this.annotationsImp.AddPlainAnnotation(new PdfAnnotation(this.writer, llx, lly, urx, ury, new PdfAction(filename, name)));
		}

		// Token: 0x06002D43 RID: 11587 RVA: 0x00115188 File Offset: 0x00114188
		internal void RemoteGoto(string filename, int page, float llx, float lly, float urx, float ury)
		{
			this.AddAnnotation(new PdfAnnotation(this.writer, llx, lly, urx, ury, new PdfAction(filename, page)));
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x001151B4 File Offset: 0x001141B4
		internal void SetAction(PdfAction action, float llx, float lly, float urx, float ury)
		{
			this.AddAnnotation(new PdfAnnotation(this.writer, llx, lly, urx, ury, action));
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x001151D0 File Offset: 0x001141D0
		internal PdfAction GetLocalGotoAction(string name)
		{
			PdfDocument.Destination destination;
			if (this.localDestinations.ContainsKey(name))
			{
				destination = this.localDestinations[name];
			}
			else
			{
				destination = new PdfDocument.Destination();
			}
			PdfAction pdfAction;
			if (destination.action == null)
			{
				if (destination.reference == null)
				{
					destination.reference = this.writer.PdfIndirectReference;
				}
				pdfAction = new PdfAction(destination.reference);
				destination.action = pdfAction;
				this.localDestinations[name] = destination;
			}
			else
			{
				pdfAction = destination.action;
			}
			return pdfAction;
		}

		// Token: 0x06002D46 RID: 11590 RVA: 0x0011524C File Offset: 0x0011424C
		internal bool LocalDestination(string name, PdfDestination destination)
		{
			PdfDocument.Destination destination2;
			if (this.localDestinations.ContainsKey(name))
			{
				destination2 = this.localDestinations[name];
			}
			else
			{
				destination2 = new PdfDocument.Destination();
			}
			if (destination2.destination != null)
			{
				return false;
			}
			destination2.destination = destination;
			this.localDestinations[name] = destination2;
			if (!destination.HasPage())
			{
				destination.AddPage(this.writer.CurrentPage);
			}
			return true;
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x001152B8 File Offset: 0x001142B8
		internal void AddJavaScript(PdfAction js)
		{
			if (js.Get(PdfName.JS) == null)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("only.javascript.actions.are.allowed"));
			}
			this.documentLevelJS[this.jsCounter.ToString().PadLeft(16, '0')] = this.writer.AddToBody(js).IndirectReference;
			this.jsCounter++;
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x00115320 File Offset: 0x00114320
		internal void AddJavaScript(string name, PdfAction js)
		{
			if (js.Get(PdfName.JS) == null)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("only.javascript.actions.are.allowed"));
			}
			this.documentLevelJS[name] = this.writer.AddToBody(js).IndirectReference;
		}

		// Token: 0x06002D49 RID: 11593 RVA: 0x0011535C File Offset: 0x0011435C
		internal Dictionary<string, PdfObject> GetDocumentLevelJS()
		{
			return this.documentLevelJS;
		}

		// Token: 0x06002D4A RID: 11594 RVA: 0x00115364 File Offset: 0x00114364
		internal void AddFileAttachment(string description, PdfFileSpecification fs)
		{
			if (description == null)
			{
				PdfString pdfString = (PdfString)fs.Get(PdfName.DESC);
				if (pdfString == null)
				{
					description = "";
				}
				else
				{
					description = PdfEncodings.ConvertToString(pdfString.GetBytes(), null);
				}
			}
			fs.AddDescription(description, true);
			if (description.Length == 0)
			{
				description = "Unnamed";
			}
			string key = PdfEncodings.ConvertToString(new PdfString(description, "UnicodeBig").GetBytes(), null);
			int num = 0;
			while (this.documentFileAttachment.ContainsKey(key))
			{
				num++;
				key = PdfEncodings.ConvertToString(new PdfString(description + " " + num, "UnicodeBig").GetBytes(), null);
			}
			this.documentFileAttachment[key] = fs.Reference;
		}

		// Token: 0x06002D4B RID: 11595 RVA: 0x0011541C File Offset: 0x0011441C
		internal Dictionary<string, PdfObject> GetDocumentFileAttachment()
		{
			return this.documentFileAttachment;
		}

		// Token: 0x06002D4C RID: 11596 RVA: 0x00115424 File Offset: 0x00114424
		internal void SetOpenAction(string name)
		{
			this.openActionName = name;
			this.openActionAction = null;
		}

		// Token: 0x06002D4D RID: 11597 RVA: 0x00115434 File Offset: 0x00114434
		internal void SetOpenAction(PdfAction action)
		{
			this.openActionAction = action;
			this.openActionName = null;
		}

		// Token: 0x06002D4E RID: 11598 RVA: 0x00115444 File Offset: 0x00114444
		internal void AddAdditionalAction(PdfName actionType, PdfAction action)
		{
			if (this.additionalActions == null)
			{
				this.additionalActions = new PdfDictionary();
			}
			if (action == null)
			{
				this.additionalActions.Remove(actionType);
			}
			else
			{
				this.additionalActions.Put(actionType, action);
			}
			if (this.additionalActions.Size == 0)
			{
				this.additionalActions = null;
			}
		}

		// Token: 0x170007D0 RID: 2000
		// (set) Token: 0x06002D4F RID: 11599 RVA: 0x00115496 File Offset: 0x00114496
		public PdfCollection Collection
		{
			set
			{
				this.collection = value;
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06002D50 RID: 11600 RVA: 0x0011549F File Offset: 0x0011449F
		public PdfAcroForm AcroForm
		{
			get
			{
				return this.annotationsImp.AcroForm;
			}
		}

		// Token: 0x170007D2 RID: 2002
		// (set) Token: 0x06002D51 RID: 11601 RVA: 0x001154AC File Offset: 0x001144AC
		internal int SigFlags
		{
			set
			{
				this.annotationsImp.SigFlags = value;
			}
		}

		// Token: 0x06002D52 RID: 11602 RVA: 0x001154BA File Offset: 0x001144BA
		internal void AddCalculationOrder(PdfFormField formField)
		{
			this.annotationsImp.AddCalculationOrder(formField);
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x001154C8 File Offset: 0x001144C8
		internal void AddAnnotation(PdfAnnotation annot)
		{
			this.pageEmpty = false;
			this.annotationsImp.AddAnnotation(annot);
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x001154DD File Offset: 0x001144DD
		internal int GetMarkPoint()
		{
			return this.markPoint;
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x001154E5 File Offset: 0x001144E5
		internal void IncMarkPoint()
		{
			this.markPoint++;
		}

		// Token: 0x170007D3 RID: 2003
		// (set) Token: 0x06002D56 RID: 11606 RVA: 0x001154F5 File Offset: 0x001144F5
		internal Rectangle CropBoxSize
		{
			set
			{
				this.SetBoxSize("crop", value);
			}
		}

		// Token: 0x06002D57 RID: 11607 RVA: 0x00115503 File Offset: 0x00114503
		internal void SetBoxSize(string boxName, Rectangle size)
		{
			if (size == null)
			{
				this.boxSize.Remove(boxName);
				return;
			}
			this.boxSize[boxName] = new PdfRectangle(size);
		}

		// Token: 0x06002D58 RID: 11608 RVA: 0x00115528 File Offset: 0x00114528
		protected internal void SetNewPageSizeAndMargins()
		{
			this.pageSize = this.nextPageSize;
			if (this.marginMirroring && (base.PageNumber & 1) == 0)
			{
				this.marginRight = this.nextMarginLeft;
				this.marginLeft = this.nextMarginRight;
			}
			else
			{
				this.marginLeft = this.nextMarginLeft;
				this.marginRight = this.nextMarginRight;
			}
			if (this.marginMirroringTopBottom && (base.PageNumber & 1) == 0)
			{
				this.marginTop = this.nextMarginBottom;
				this.marginBottom = this.nextMarginTop;
				return;
			}
			this.marginTop = this.nextMarginTop;
			this.marginBottom = this.nextMarginBottom;
		}

		// Token: 0x06002D59 RID: 11609 RVA: 0x001155C8 File Offset: 0x001145C8
		internal Rectangle GetBoxSize(string boxName)
		{
			if (this.thisBoxSize.ContainsKey(boxName))
			{
				return this.thisBoxSize[boxName].Rectangle;
			}
			return null;
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06002D5B RID: 11611 RVA: 0x001155F4 File Offset: 0x001145F4
		// (set) Token: 0x06002D5A RID: 11610 RVA: 0x001155EB File Offset: 0x001145EB
		internal bool PageEmpty
		{
			get
			{
				return this.writer == null || (this.writer.DirectContent.Size == 0 && this.writer.DirectContentUnder.Size == 0 && (this.pageEmpty || this.writer.IsPaused()));
			}
			set
			{
				this.pageEmpty = value;
			}
		}

		// Token: 0x170007D5 RID: 2005
		// (set) Token: 0x06002D5C RID: 11612 RVA: 0x00115646 File Offset: 0x00114646
		internal int Duration
		{
			set
			{
				if (value > 0)
				{
					this.duration = value;
					return;
				}
				this.duration = -1;
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (set) Token: 0x06002D5D RID: 11613 RVA: 0x0011565B File Offset: 0x0011465B
		internal PdfTransition Transition
		{
			set
			{
				this.transition = value;
			}
		}

		// Token: 0x06002D5E RID: 11614 RVA: 0x00115664 File Offset: 0x00114664
		internal void SetPageAction(PdfName actionType, PdfAction action)
		{
			if (this.pageAA == null)
			{
				this.pageAA = new PdfDictionary();
			}
			this.pageAA.Put(actionType, action);
		}

		// Token: 0x170007D7 RID: 2007
		// (set) Token: 0x06002D5F RID: 11615 RVA: 0x00115686 File Offset: 0x00114686
		internal Image Thumbnail
		{
			set
			{
				this.thumb = this.writer.GetImageReference(this.writer.AddDirectImageSimple(value));
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06002D60 RID: 11616 RVA: 0x001156A5 File Offset: 0x001146A5
		internal PageResources PageResources
		{
			get
			{
				return this.pageResources;
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06002D62 RID: 11618 RVA: 0x001156B6 File Offset: 0x001146B6
		// (set) Token: 0x06002D61 RID: 11617 RVA: 0x001156AD File Offset: 0x001146AD
		internal bool StrictImageSequence
		{
			get
			{
				return this.strictImageSequence;
			}
			set
			{
				this.strictImageSequence = value;
			}
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x001156C0 File Offset: 0x001146C0
		public void ClearTextWrap()
		{
			float num = this.imageEnd - this.currentHeight;
			if (this.line != null)
			{
				num += this.line.Height;
			}
			if (this.imageEnd > -1f && num > 0f)
			{
				this.CarriageReturn();
				this.currentHeight += num;
			}
		}

		// Token: 0x06002D64 RID: 11620 RVA: 0x0011571C File Offset: 0x0011471C
		protected internal void Add(Image image)
		{
			if (image.HasAbsolutePosition())
			{
				this.graphics.AddImage(image);
				this.pageEmpty = false;
				return;
			}
			if (this.currentHeight != 0f && this.IndentTop - this.currentHeight - image.ScaledHeight < this.IndentBottom)
			{
				if (!this.strictImageSequence && this.imageWait == null)
				{
					this.imageWait = image;
					return;
				}
				this.NewPage();
				if (this.currentHeight != 0f && this.IndentTop - this.currentHeight - image.ScaledHeight < this.IndentBottom)
				{
					this.imageWait = image;
					return;
				}
			}
			this.pageEmpty = false;
			if (image == this.imageWait)
			{
				this.imageWait = null;
			}
			bool flag = (image.Alignment & 4) == 4 && (image.Alignment & 1) != 1;
			bool flag2 = (image.Alignment & 8) == 8;
			float num = this.leading / 2f;
			if (flag)
			{
				num += this.leading;
			}
			float num2 = this.IndentTop - this.currentHeight - image.ScaledHeight - num;
			float[] matrix = image.Matrix;
			float num3 = this.IndentLeft - matrix[4];
			if ((image.Alignment & 2) == 2)
			{
				num3 = this.IndentRight - image.ScaledWidth - matrix[4];
			}
			if ((image.Alignment & 1) == 1)
			{
				num3 = this.IndentLeft + (this.IndentRight - this.IndentLeft - image.ScaledWidth) / 2f - matrix[4];
			}
			if (image.HasAbsoluteX())
			{
				num3 = image.AbsoluteX;
			}
			if (flag)
			{
				if (this.imageEnd < 0f || this.imageEnd < this.currentHeight + image.ScaledHeight + num)
				{
					this.imageEnd = this.currentHeight + image.ScaledHeight + num;
				}
				if ((image.Alignment & 2) == 2)
				{
					this.indentation.imageIndentRight += image.ScaledWidth + image.IndentationLeft;
				}
				else
				{
					this.indentation.imageIndentLeft += image.ScaledWidth + image.IndentationRight;
				}
			}
			else if ((image.Alignment & 2) == 2)
			{
				num3 -= image.IndentationRight;
			}
			else if ((image.Alignment & 1) == 1)
			{
				num3 += image.IndentationLeft - image.IndentationRight;
			}
			else
			{
				num3 -= image.IndentationRight;
			}
			this.graphics.AddImage(image, matrix[0], matrix[1], matrix[2], matrix[3], num3, num2 - matrix[5]);
			if (!flag && !flag2)
			{
				this.currentHeight += image.ScaledHeight + num;
				this.FlushLines();
				this.text.MoveText(0f, -(image.ScaledHeight + num));
				this.NewLine();
			}
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x001159D8 File Offset: 0x001149D8
		internal void AddPTable(PdfPTable ptable)
		{
			ColumnText columnText = new ColumnText(this.writer.DirectContent);
			if (ptable.KeepTogether && !this.FitsPage(ptable, 0f) && this.currentHeight > 0f)
			{
				this.NewPage();
			}
			if (this.currentHeight > 0f)
			{
				columnText.AddElement(new Paragraph
				{
					Leading = 0f
				});
			}
			columnText.AddElement(ptable);
			bool headersInEvent = ptable.HeadersInEvent;
			ptable.HeadersInEvent = true;
			int num = 0;
			for (;;)
			{
				columnText.SetSimpleColumn(this.IndentLeft, this.IndentBottom, this.IndentRight, this.IndentTop - this.currentHeight);
				int num2 = columnText.Go();
				if ((num2 & 1) != 0)
				{
					break;
				}
				if (this.IndentTop - this.currentHeight == columnText.YLine)
				{
					num++;
				}
				else
				{
					num = 0;
				}
				if (num == 3)
				{
					goto Block_7;
				}
				this.NewPage();
			}
			this.text.MoveText(0f, columnText.YLine - this.IndentTop + this.currentHeight);
			this.currentHeight = this.IndentTop - columnText.YLine;
			goto IL_123;
			Block_7:
			this.Add(new Paragraph("ERROR: Infinite table loop"));
			IL_123:
			ptable.HeadersInEvent = headersInEvent;
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x00115B10 File Offset: 0x00114B10
		internal bool FitsPage(PdfPTable table, float margin)
		{
			if (!table.LockedWidth)
			{
				float totalWidth = (this.IndentRight - this.IndentLeft) * table.WidthPercentage / 100f;
				table.TotalWidth = totalWidth;
			}
			this.EnsureNewLine();
			return table.TotalHeight + ((this.currentHeight > 0f) ? table.SpacingBefore : 0f) <= this.IndentTop - this.currentHeight - this.IndentBottom - margin;
		}

		// Token: 0x04001F1E RID: 7966
		internal const string hangingPunctuation = ".,;:'";

		// Token: 0x04001F1F RID: 7967
		protected internal PdfWriter writer;

		// Token: 0x04001F20 RID: 7968
		protected internal PdfContentByte text;

		// Token: 0x04001F21 RID: 7969
		protected internal PdfContentByte graphics;

		// Token: 0x04001F22 RID: 7970
		protected internal float leading;

		// Token: 0x04001F23 RID: 7971
		protected internal float currentHeight;

		// Token: 0x04001F24 RID: 7972
		protected bool isSectionTitle;

		// Token: 0x04001F25 RID: 7973
		protected int leadingCount;

		// Token: 0x04001F26 RID: 7974
		protected internal int alignment;

		// Token: 0x04001F27 RID: 7975
		protected internal PdfAction anchorAction;

		// Token: 0x04001F28 RID: 7976
		protected internal int textEmptySize;

		// Token: 0x04001F29 RID: 7977
		protected byte[] xmpMetadata;

		// Token: 0x04001F2A RID: 7978
		protected float nextMarginLeft;

		// Token: 0x04001F2B RID: 7979
		protected float nextMarginRight;

		// Token: 0x04001F2C RID: 7980
		protected float nextMarginTop;

		// Token: 0x04001F2D RID: 7981
		protected float nextMarginBottom;

		// Token: 0x04001F2E RID: 7982
		protected internal bool firstPageEvent = true;

		// Token: 0x04001F2F RID: 7983
		protected internal PdfLine line;

		// Token: 0x04001F30 RID: 7984
		protected internal List<PdfLine> lines = new List<PdfLine>();

		// Token: 0x04001F31 RID: 7985
		protected internal int lastElementType = -1;

		// Token: 0x04001F32 RID: 7986
		protected internal PdfDocument.Indentation indentation = new PdfDocument.Indentation();

		// Token: 0x04001F33 RID: 7987
		protected internal PdfDocument.PdfInfo info = new PdfDocument.PdfInfo();

		// Token: 0x04001F34 RID: 7988
		protected internal PdfOutline rootOutline;

		// Token: 0x04001F35 RID: 7989
		protected internal PdfOutline currentOutline;

		// Token: 0x04001F36 RID: 7990
		protected PdfViewerPreferencesImp viewerPreferences = new PdfViewerPreferencesImp();

		// Token: 0x04001F37 RID: 7991
		protected internal PdfPageLabels pageLabels;

		// Token: 0x04001F38 RID: 7992
		protected internal SortedDictionary<string, PdfDocument.Destination> localDestinations = new SortedDictionary<string, PdfDocument.Destination>(StringComparer.Ordinal);

		// Token: 0x04001F39 RID: 7993
		private int jsCounter;

		// Token: 0x04001F3A RID: 7994
		protected internal Dictionary<string, PdfObject> documentLevelJS = new Dictionary<string, PdfObject>();

		// Token: 0x04001F3B RID: 7995
		protected internal Dictionary<string, PdfObject> documentFileAttachment = new Dictionary<string, PdfObject>();

		// Token: 0x04001F3C RID: 7996
		protected internal string openActionName;

		// Token: 0x04001F3D RID: 7997
		protected internal PdfAction openActionAction;

		// Token: 0x04001F3E RID: 7998
		protected internal PdfDictionary additionalActions;

		// Token: 0x04001F3F RID: 7999
		protected internal PdfCollection collection;

		// Token: 0x04001F40 RID: 8000
		internal PdfAnnotationsImp annotationsImp;

		// Token: 0x04001F41 RID: 8001
		protected int markPoint;

		// Token: 0x04001F42 RID: 8002
		protected Rectangle nextPageSize;

		// Token: 0x04001F43 RID: 8003
		protected Dictionary<string, PdfRectangle> thisBoxSize = new Dictionary<string, PdfRectangle>();

		// Token: 0x04001F44 RID: 8004
		protected Dictionary<string, PdfRectangle> boxSize = new Dictionary<string, PdfRectangle>();

		// Token: 0x04001F45 RID: 8005
		private bool pageEmpty = true;

		// Token: 0x04001F46 RID: 8006
		protected int duration = -1;

		// Token: 0x04001F47 RID: 8007
		protected PdfTransition transition;

		// Token: 0x04001F48 RID: 8008
		protected PdfDictionary pageAA;

		// Token: 0x04001F49 RID: 8009
		protected internal PdfIndirectReference thumb;

		// Token: 0x04001F4A RID: 8010
		protected internal PageResources pageResources;

		// Token: 0x04001F4B RID: 8011
		protected internal bool strictImageSequence;

		// Token: 0x04001F4C RID: 8012
		protected internal float imageEnd = -1f;

		// Token: 0x04001F4D RID: 8013
		protected internal Image imageWait;

		// Token: 0x02000529 RID: 1321
		public class PdfInfo : PdfDictionary
		{
			// Token: 0x06002D67 RID: 11623 RVA: 0x00115B89 File Offset: 0x00114B89
			internal PdfInfo()
			{
				this.AddProducer();
				this.AddCreationDate();
			}

			// Token: 0x06002D68 RID: 11624 RVA: 0x00115B9D File Offset: 0x00114B9D
			internal PdfInfo(string author, string title, string subject)
			{
				this.AddTitle(title);
				this.AddSubject(subject);
				this.AddAuthor(author);
			}

			// Token: 0x06002D69 RID: 11625 RVA: 0x00115BBA File Offset: 0x00114BBA
			internal void AddTitle(string title)
			{
				base.Put(PdfName.TITLE, new PdfString(title, "UnicodeBig"));
			}

			// Token: 0x06002D6A RID: 11626 RVA: 0x00115BD2 File Offset: 0x00114BD2
			internal void AddSubject(string subject)
			{
				base.Put(PdfName.SUBJECT, new PdfString(subject, "UnicodeBig"));
			}

			// Token: 0x06002D6B RID: 11627 RVA: 0x00115BEA File Offset: 0x00114BEA
			internal void AddKeywords(string keywords)
			{
				base.Put(PdfName.KEYWORDS, new PdfString(keywords, "UnicodeBig"));
			}

			// Token: 0x06002D6C RID: 11628 RVA: 0x00115C02 File Offset: 0x00114C02
			internal void AddAuthor(string author)
			{
				base.Put(PdfName.AUTHOR, new PdfString(author, "UnicodeBig"));
			}

			// Token: 0x06002D6D RID: 11629 RVA: 0x00115C1A File Offset: 0x00114C1A
			internal void AddCreator(string creator)
			{
				base.Put(PdfName.CREATOR, new PdfString(creator, "UnicodeBig"));
			}

			// Token: 0x06002D6E RID: 11630 RVA: 0x00115C32 File Offset: 0x00114C32
			internal void AddProducer()
			{
				base.Put(PdfName.PRODUCER, new PdfString(Document.Version));
			}

			// Token: 0x06002D6F RID: 11631 RVA: 0x00115C4C File Offset: 0x00114C4C
			internal void AddCreationDate()
			{
				PdfString value = new PdfDate();
				base.Put(PdfName.CREATIONDATE, value);
				base.Put(PdfName.MODDATE, value);
			}

			// Token: 0x06002D70 RID: 11632 RVA: 0x00115C77 File Offset: 0x00114C77
			internal void Addkey(string key, string value)
			{
				if (key.Equals("Producer") || key.Equals("CreationDate"))
				{
					return;
				}
				base.Put(new PdfName(key), new PdfString(value, "UnicodeBig"));
			}
		}

		// Token: 0x0200052A RID: 1322
		internal class PdfCatalog : PdfDictionary
		{
			// Token: 0x06002D71 RID: 11633 RVA: 0x00115CAB File Offset: 0x00114CAB
			internal PdfCatalog(PdfIndirectReference pages, PdfWriter writer) : base(PdfDictionary.CATALOG)
			{
				this.writer = writer;
				base.Put(PdfName.PAGES, pages);
			}

			// Token: 0x06002D72 RID: 11634 RVA: 0x00115CCC File Offset: 0x00114CCC
			internal void AddNames(SortedDictionary<string, PdfDocument.Destination> localDestinations, Dictionary<string, PdfObject> documentLevelJS, Dictionary<string, PdfObject> documentFileAttachment, PdfWriter writer)
			{
				if (localDestinations.Count == 0 && documentLevelJS.Count == 0 && documentFileAttachment.Count == 0)
				{
					return;
				}
				PdfDictionary pdfDictionary = new PdfDictionary();
				if (localDestinations.Count > 0)
				{
					PdfArray pdfArray = new PdfArray();
					foreach (string text in localDestinations.Keys)
					{
						PdfDocument.Destination destination;
						if (localDestinations.TryGetValue(text, out destination))
						{
							PdfIndirectReference reference = destination.reference;
							pdfArray.Add(new PdfString(text, null));
							pdfArray.Add(reference);
						}
					}
					if (pdfArray.Size > 0)
					{
						PdfDictionary pdfDictionary2 = new PdfDictionary();
						pdfDictionary2.Put(PdfName.NAMES, pdfArray);
						pdfDictionary.Put(PdfName.DESTS, writer.AddToBody(pdfDictionary2).IndirectReference);
					}
				}
				if (documentLevelJS.Count > 0)
				{
					PdfDictionary objecta = PdfNameTree.WriteTree<PdfObject>(documentLevelJS, writer);
					pdfDictionary.Put(PdfName.JAVASCRIPT, writer.AddToBody(objecta).IndirectReference);
				}
				if (documentFileAttachment.Count > 0)
				{
					pdfDictionary.Put(PdfName.EMBEDDEDFILES, writer.AddToBody(PdfNameTree.WriteTree<PdfObject>(documentFileAttachment, writer)).IndirectReference);
				}
				if (pdfDictionary.Size > 0)
				{
					base.Put(PdfName.NAMES, writer.AddToBody(pdfDictionary).IndirectReference);
				}
			}

			// Token: 0x170007DA RID: 2010
			// (set) Token: 0x06002D73 RID: 11635 RVA: 0x00115E20 File Offset: 0x00114E20
			internal PdfAction OpenAction
			{
				set
				{
					base.Put(PdfName.OPENACTION, value);
				}
			}

			// Token: 0x170007DB RID: 2011
			// (set) Token: 0x06002D74 RID: 11636 RVA: 0x00115E2E File Offset: 0x00114E2E
			internal PdfDictionary AdditionalActions
			{
				set
				{
					base.Put(PdfName.AA, this.writer.AddToBody(value).IndirectReference);
				}
			}

			// Token: 0x04001F4E RID: 8014
			internal PdfWriter writer;
		}

		// Token: 0x0200052B RID: 1323
		public class Indentation
		{
			// Token: 0x04001F4F RID: 8015
			internal float indentLeft;

			// Token: 0x04001F50 RID: 8016
			internal float sectionIndentLeft;

			// Token: 0x04001F51 RID: 8017
			internal float listIndentLeft;

			// Token: 0x04001F52 RID: 8018
			internal float imageIndentLeft;

			// Token: 0x04001F53 RID: 8019
			internal float indentRight;

			// Token: 0x04001F54 RID: 8020
			internal float sectionIndentRight;

			// Token: 0x04001F55 RID: 8021
			internal float imageIndentRight;

			// Token: 0x04001F56 RID: 8022
			internal float indentTop;

			// Token: 0x04001F57 RID: 8023
			internal float indentBottom;
		}

		// Token: 0x0200052C RID: 1324
		public class Destination
		{
			// Token: 0x04001F58 RID: 8024
			public PdfAction action;

			// Token: 0x04001F59 RID: 8025
			public PdfIndirectReference reference;

			// Token: 0x04001F5A RID: 8026
			public PdfDestination destination;
		}
	}
}
