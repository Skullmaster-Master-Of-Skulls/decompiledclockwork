using System;
using System.Collections;
using System.IO;
using System.Text;
using Telerik.Pdf;
using Telerik.Pdf.Gdi;
using Telerik.Web.Apoc.DataTypes;
using Telerik.Web.Apoc.Extensions;
using Telerik.Web.Apoc.Image;
using Telerik.Web.Apoc.Layout;
using Telerik.Web.Apoc.Layout.Inline;
using Telerik.Web.Apoc.Pdf;
using Telerik.Web.Apoc.Render.Pdf.Fonts;

namespace Telerik.Web.Apoc.Render.Pdf
{
	// Token: 0x0200169E RID: 5790
	internal sealed class PdfRenderer : IRenderer
	{
		// Token: 0x0600DF77 RID: 57207 RVA: 0x0031A4A5 File Offset: 0x003186A5
		internal PdfRenderer(Stream stream)
		{
			this.pdfDoc = new PdfCreator(stream);
		}

		// Token: 0x17004487 RID: 17543
		// (set) Token: 0x0600DF78 RID: 57208 RVA: 0x0031A4D0 File Offset: 0x003186D0
		public IRendererOptions Options
		{
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				PdfRendererOptions pdfRendererOptions = value as PdfRendererOptions;
				if (pdfRendererOptions == null)
				{
					throw new ArgumentException("Options must be an instance of PdfRendererOptions");
				}
				this.options = pdfRendererOptions;
			}
		}

		// Token: 0x0600DF79 RID: 57209 RVA: 0x0031A507 File Offset: 0x00318707
		public void StartRenderer()
		{
			if (this.options != null)
			{
				this.pdfDoc.SetOptions(this.options);
			}
			this.pdfDoc.outputHeader();
		}

		// Token: 0x0600DF7A RID: 57210 RVA: 0x0031A530 File Offset: 0x00318730
		public void StopRenderer()
		{
			this.RenderRootExtensions(this.extensions);
			this.fontSetup.AddToResources(new PdfFontCreator(this.pdfDoc), this.pdfDoc.getResources());
			this.pdfDoc.outputTrailer();
			this.pdfDoc = null;
			this.pdfResources = null;
			this.extensions = null;
			this.currentStream = null;
			this.currentAnnotList = null;
			this.currentPage = null;
			this.idReferences = null;
			this.currentFontName = string.Empty;
			this.currentFill = null;
			this.prevUnderlineColor = null;
			this.prevOverlineColor = null;
			this.prevLineThroughColor = null;
			this.fontSetup = null;
			this.fontInfo = null;
		}

		// Token: 0x0600DF7B RID: 57211 RVA: 0x0031A5DB File Offset: 0x003187DB
		public void SetupFontInfo(FontInfo fontInfo)
		{
			this.fontInfo = fontInfo;
			this.fontSetup = new FontSetup(fontInfo, (this.options == null) ? FontType.Link : this.options.FontType);
		}

		// Token: 0x0600DF7C RID: 57212 RVA: 0x0031A608 File Offset: 0x00318808
		public void RenderSpanArea(SpanArea area)
		{
			foreach (object obj in area.getChildren())
			{
				Box box = (Box)obj;
				box.render(this);
			}
		}

		// Token: 0x0600DF7D RID: 57213 RVA: 0x0031A664 File Offset: 0x00318864
		public void RenderBodyAreaContainer(BodyAreaContainer area)
		{
			int num = this.currentYPosition;
			int num2 = this.currentAreaContainerXPosition;
			if (area.getPosition() == 1)
			{
				this.currentYPosition = area.GetYPosition();
				this.currentAreaContainerXPosition = area.getXPosition();
			}
			else if (area.getPosition() == 61)
			{
				this.currentYPosition -= area.GetYPosition();
				this.currentAreaContainerXPosition += area.getXPosition();
			}
			this.currentXPosition = this.currentAreaContainerXPosition;
			int x = this.currentAreaContainerXPosition;
			int y = this.currentYPosition;
			int allocationWidth = area.getAllocationWidth();
			int maxHeight = area.getMaxHeight();
			this.DoBackground(area, x, y, allocationWidth, maxHeight);
			this.RenderAreaContainer(area.getBeforeFloatReferenceArea());
			this.RenderAreaContainer(area.getFootnoteReferenceArea());
			foreach (object obj in area.getMainReferenceArea().getChildren())
			{
				Box box = (Box)obj;
				box.render(this);
			}
			if (area.getPosition() != 73)
			{
				this.currentYPosition = num;
				this.currentAreaContainerXPosition = num2;
				return;
			}
			this.currentYPosition -= area.GetHeight();
		}

		// Token: 0x0600DF7E RID: 57214 RVA: 0x0031A7A8 File Offset: 0x003189A8
		public void RenderAreaContainer(AreaContainer area)
		{
			int num = this.currentYPosition;
			int num2 = this.currentAreaContainerXPosition;
			if (area.getPosition() == 1)
			{
				this.currentYPosition = area.GetYPosition();
				this.currentAreaContainerXPosition = area.getXPosition();
			}
			else if (area.getPosition() == 61)
			{
				this.currentYPosition -= area.GetYPosition();
				this.currentAreaContainerXPosition += area.getXPosition();
			}
			else if (area.getPosition() == 73)
			{
				this.currentYPosition -= area.getPaddingTop() + area.getBorderTopWidth();
			}
			this.currentXPosition = this.currentAreaContainerXPosition;
			this.DoFrame(area);
			foreach (object obj in area.getChildren())
			{
				Box box = (Box)obj;
				box.render(this);
			}
			this.currentYPosition = num;
			this.currentAreaContainerXPosition = num2;
			if (area.getPosition() == 73)
			{
				this.currentYPosition -= area.GetHeight();
			}
		}

		// Token: 0x0600DF7F RID: 57215 RVA: 0x0031A8CC File Offset: 0x00318ACC
		public void RenderBlockArea(BlockArea area)
		{
			this.currentYPosition -= area.getPaddingTop() + area.getBorderTopWidth();
			this.DoFrame(area);
			foreach (object obj in area.getChildren())
			{
				Box box = (Box)obj;
				box.render(this);
			}
			this.currentYPosition -= area.getPaddingBottom() + area.getBorderBottomWidth();
		}

		// Token: 0x0600DF80 RID: 57216 RVA: 0x0031A960 File Offset: 0x00318B60
		public void RenderLineArea(LineArea area)
		{
			int num = this.currentAreaContainerXPosition + area.getStartIndent();
			int num2 = this.currentYPosition;
			area.getContentWidth();
			int height = area.GetHeight();
			this.currentYPosition -= area.getPlacementOffset();
			this.currentXPosition = num;
			foreach (object obj in area.getChildren())
			{
				Box box = (Box)obj;
				InlineArea inlineArea = box as InlineArea;
				if (inlineArea != null)
				{
					this.currentYPosition = num2 - inlineArea.getYOffset();
				}
				else
				{
					this.currentYPosition = num2 - area.getPlacementOffset();
				}
				box.render(this);
			}
			this.currentYPosition = num2 - height;
			this.currentXPosition = num;
		}

		// Token: 0x0600DF81 RID: 57217 RVA: 0x0031AA38 File Offset: 0x00318C38
		private void AddLine(int x1, int y1, int x2, int y2, int th, PdfColor stroke)
		{
			this.CloseText();
			this.currentStream.Write(string.Concat(new string[]
			{
				"ET\nq\n",
				stroke.getColorSpaceOut(false),
				PdfNumber.doubleOut((double)((float)x1 / 1000f)),
				" ",
				PdfNumber.doubleOut((double)((float)y1 / 1000f)),
				" m ",
				PdfNumber.doubleOut((double)((float)x2 / 1000f)),
				" ",
				PdfNumber.doubleOut((double)((float)y2 / 1000f)),
				" l ",
				PdfNumber.doubleOut((double)((float)th / 1000f)),
				" w S\nQ\nBT\n"
			}));
		}

		// Token: 0x0600DF82 RID: 57218 RVA: 0x0031AAFC File Offset: 0x00318CFC
		private void AddLine(int x1, int y1, int x2, int y2, int th, int rs, PdfColor stroke)
		{
			this.CloseText();
			this.currentStream.Write(string.Concat(new string[]
			{
				"ET\nq\n",
				stroke.getColorSpaceOut(false),
				this.SetRuleStylePattern(rs),
				PdfNumber.doubleOut((double)((float)x1 / 1000f)),
				" ",
				PdfNumber.doubleOut((double)((float)y1 / 1000f)),
				" m ",
				PdfNumber.doubleOut((double)((float)x2 / 1000f)),
				" ",
				PdfNumber.doubleOut((double)((float)y2 / 1000f)),
				" l ",
				PdfNumber.doubleOut((double)((float)th / 1000f)),
				" w S\nQ\nBT\n"
			}));
		}

		// Token: 0x0600DF83 RID: 57219 RVA: 0x0031ABCC File Offset: 0x00318DCC
		private void AddRect(int x, int y, int w, int h, PdfColor stroke)
		{
			this.CloseText();
			this.currentStream.Write(string.Concat(new string[]
			{
				"ET\nq\n",
				stroke.getColorSpaceOut(false),
				PdfNumber.doubleOut((double)((float)x / 1000f)),
				" ",
				PdfNumber.doubleOut((double)((float)y / 1000f)),
				" ",
				PdfNumber.doubleOut((double)((float)w / 1000f)),
				" ",
				PdfNumber.doubleOut((double)((float)h / 1000f)),
				" re s\nQ\nBT\n"
			}));
		}

		// Token: 0x0600DF84 RID: 57220 RVA: 0x0031AC74 File Offset: 0x00318E74
		private void AddRect(int x, int y, int w, int h, PdfColor stroke, PdfColor fill)
		{
			this.CloseText();
			this.currentStream.Write(string.Concat(new string[]
			{
				"ET\nq\n",
				fill.getColorSpaceOut(true),
				stroke.getColorSpaceOut(false),
				PdfNumber.doubleOut((double)((float)x / 1000f)),
				" ",
				PdfNumber.doubleOut((double)((float)y / 1000f)),
				" ",
				PdfNumber.doubleOut((double)((float)w / 1000f)),
				" ",
				PdfNumber.doubleOut((double)((float)h / 1000f)),
				" re b\nQ\nBT\n"
			}));
		}

		// Token: 0x0600DF85 RID: 57221 RVA: 0x0031AD28 File Offset: 0x00318F28
		private void AddFilledRect(int x, int y, int w, int h, PdfColor fill)
		{
			this.CloseText();
			this.currentStream.Write(string.Concat(new string[]
			{
				"ET\nq\n",
				fill.getColorSpaceOut(true),
				PdfNumber.doubleOut((double)((float)x / 1000f)),
				" ",
				PdfNumber.doubleOut((double)((float)y / 1000f)),
				" ",
				PdfNumber.doubleOut((double)((float)w / 1000f)),
				" ",
				PdfNumber.doubleOut((double)((float)h / 1000f)),
				" re f\nQ\nBT\n"
			}));
		}

		// Token: 0x0600DF86 RID: 57222 RVA: 0x0031ADD0 File Offset: 0x00318FD0
		public void RenderImageArea(ImageArea area)
		{
			int num = this.currentXPosition + area.getXOffset();
			int num2 = this.currentYPosition;
			int contentWidth = area.getContentWidth();
			int height = area.GetHeight();
			this.currentYPosition -= height;
			ApocImage image = area.getImage();
			PdfXObject pdfXObject = this.pdfDoc.AddImage(image);
			this.CloseText();
			this.currentStream.Write(string.Concat(new string[]
			{
				"ET\nq\n",
				PdfNumber.doubleOut((double)((float)contentWidth / 1000f)),
				" 0 0 ",
				PdfNumber.doubleOut((double)((float)height / 1000f)),
				" ",
				PdfNumber.doubleOut((double)((float)num / 1000f)),
				" ",
				PdfNumber.doubleOut((double)((float)(num2 - height) / 1000f)),
				" cm\n/",
				pdfXObject.Name.Name,
				" Do\nQ\nBT\n"
			}));
			this.currentXPosition += area.getContentWidth();
		}

		// Token: 0x0600DF87 RID: 57223 RVA: 0x0031AEEC File Offset: 0x003190EC
		public void RenderForeignObjectArea(ForeignObjectArea area)
		{
			this.currentXPosition += area.getXOffset();
			int align = area.getAlign();
			if (align <= 22)
			{
				if (align != 13 && align != 22)
				{
				}
			}
			else if (align != 37)
			{
			}
			int verticalAlign = area.getVerticalAlign();
			if (verticalAlign <= 12)
			{
				if (verticalAlign != 8 && verticalAlign != 12)
				{
				}
			}
			else if (verticalAlign != 43)
			{
				switch (verticalAlign)
				{
				}
			}
			this.CloseText();
			this.currentStream.Write("ET\n");
			this.currentStream.Write("q\n");
			int num = area.scalingMethod();
			if (num != 50)
			{
			}
			int overflow = area.getOverflow();
			if (overflow <= 34)
			{
				if (overflow != 7 && overflow != 34)
				{
				}
			}
			else if (overflow != 67)
			{
			}
			area.getObject().render(this);
			this.currentStream.Write("Q\n");
			this.currentStream.Write("BT\n");
			this.currentXPosition += area.getEffectiveWidth();
		}

		// Token: 0x0600DF88 RID: 57224 RVA: 0x0031B004 File Offset: 0x00319204
		public void RenderWordArea(WordArea area)
		{
			lock (this._wordAreaPDF)
			{
				StringBuilder stringBuilder = this._wordAreaPDF;
				stringBuilder.Length = 0;
				GdiKerningPairs gdiKerningPairs = null;
				bool flag2 = false;
				if (this.options != null && this.options.Kerning)
				{
					gdiKerningPairs = area.GetFontState().Kerning;
					if (gdiKerningPairs != null && gdiKerningPairs.Count > 0)
					{
						flag2 = true;
					}
				}
				string fontName = area.GetFontState().FontName;
				int fontSize = area.GetFontState().FontSize;
				Font font = (Font)area.GetFontState().FontInfo.GetFontByName(fontName);
				bool multiByteFont = font.MultiByteFont;
				string text = multiByteFont ? "<" : "(";
				string text2 = multiByteFont ? "> " : ") ";
				if (!fontName.Equals(this.currentFontName) || fontSize != this.currentFontSize)
				{
					this.CloseText();
					this.currentFontName = fontName;
					this.currentFontSize = fontSize;
					stringBuilder = stringBuilder.Append(string.Concat(new string[]
					{
						"/",
						fontName,
						" ",
						PdfNumber.doubleOut((double)((float)fontSize / 1000f)),
						" Tf\n"
					}));
				}
				float num = (float)area.GetFontState().LetterSpacing / 1000f;
				if (num != this.currentLetterSpacing)
				{
					this.currentLetterSpacing = num;
					this.CloseText();
					stringBuilder.Append(PdfNumber.doubleOut((double)num));
					stringBuilder.Append(" Tc\n");
				}
				PdfColor pdfColor = this.currentFill;
				if (pdfColor == null || pdfColor.getRed() != (double)area.getRed() || pdfColor.getGreen() != (double)area.getGreen() || pdfColor.getBlue() != (double)area.getBlue())
				{
					pdfColor = new PdfColor((double)area.getRed(), (double)area.getGreen(), (double)area.getBlue());
					this.CloseText();
					this.currentFill = pdfColor;
					stringBuilder.Append(this.currentFill.getColorSpaceOut(true));
				}
				int num2 = this.currentXPosition;
				int num3 = this.currentYPosition;
				this.AddWordLines(area, num2, num3, fontSize, pdfColor);
				if (!this.textOpen || num3 != this.prevWordY)
				{
					this.CloseText();
					stringBuilder.Append(string.Concat(new string[]
					{
						"1 0 0 1 ",
						PdfNumber.doubleOut((double)((float)num2 / 1000f)),
						" ",
						PdfNumber.doubleOut((double)((float)num3 / 1000f)),
						" Tm [",
						text
					}));
					this.prevWordY = num3;
					this.textOpen = true;
				}
				else
				{
					int num4 = this.prevWordX - num2 + this.prevWordWidth;
					float num5 = (float)num4 / (float)this.currentFontSize * 1000f;
					if (num5 < -33000f)
					{
						this.CloseText();
						stringBuilder.Append(string.Concat(new string[]
						{
							"1 0 0 1 ",
							PdfNumber.doubleOut((double)((float)num2 / 1000f)),
							" ",
							PdfNumber.doubleOut((double)((float)num3 / 1000f)),
							" Tm [",
							text
						}));
						this.textOpen = true;
					}
					else
					{
						stringBuilder.Append(PdfNumber.doubleOut((double)num5));
						stringBuilder.Append(" ");
						stringBuilder.Append(text);
					}
				}
				this.prevWordWidth = area.getContentWidth();
				this.prevWordX = num2;
				string text3;
				if (area.getPageNumberID() != null)
				{
					text3 = this.idReferences.getPageNumber(area.getPageNumberID());
					if (text3 == null)
					{
						text3 = string.Empty;
					}
				}
				else
				{
					text3 = area.getText();
				}
				int length = text3.Length;
				for (int i = 0; i < length; i++)
				{
					int num6 = area.GetFontState().MapCharacter(text3[i]);
					if (!multiByteFont)
					{
						if (num6 <= 127)
						{
							int num7 = num6;
							switch (num7)
							{
							case 40:
							case 41:
								goto IL_3FC;
							default:
								if (num7 == 92)
								{
									goto IL_3FC;
								}
								break;
							}
							IL_408:
							stringBuilder.Append((char)num6);
							goto IL_423;
							IL_3FC:
							stringBuilder.Append("\\");
							goto IL_408;
						}
						stringBuilder.Append("\\");
						stringBuilder.Append(Convert.ToString(num6, 8));
					}
					else
					{
						stringBuilder.Append(this.GetUnicodeString(num6));
					}
					IL_423:
					if (flag2 && i + 1 < length)
					{
						int rightIndex = area.GetFontState().MapCharacter(text3[i + 1]);
						this.AddKerning(stringBuilder, num6, rightIndex, gdiKerningPairs, text, text2);
					}
				}
				stringBuilder.Append(text2);
				this.currentStream.Write(stringBuilder.ToString());
				this.currentXPosition += area.getContentWidth();
			}
		}

		// Token: 0x0600DF89 RID: 57225 RVA: 0x0031B4D0 File Offset: 0x003196D0
		private string GetUnicodeString(int c)
		{
			StringBuilder stringBuilder = new StringBuilder(4);
			byte[] bytes = Encoding.BigEndianUnicode.GetBytes(new char[]
			{
				(char)c
			});
			foreach (byte value in bytes)
			{
				string text = Convert.ToString(value, 16);
				if (text.Length == 1)
				{
					stringBuilder.Append("0");
				}
				stringBuilder.Append(text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600DF8A RID: 57226 RVA: 0x0031B547 File Offset: 0x00319747
		private void CloseText()
		{
			if (this.textOpen)
			{
				this.currentStream.Write("] TJ\n");
				this.textOpen = false;
				this.prevWordX = 0;
				this.prevWordY = 0;
			}
		}

		// Token: 0x0600DF8B RID: 57227 RVA: 0x0031B578 File Offset: 0x00319778
		private void AddKerning(StringBuilder buf, int leftIndex, int rightIndex, GdiKerningPairs kerning, string startText, string endText)
		{
			if (kerning.HasPair(leftIndex, rightIndex))
			{
				int num = kerning[leftIndex, rightIndex];
				buf.Append(endText).Append(-num).Append(' ').Append(startText);
			}
		}

		// Token: 0x0600DF8C RID: 57228 RVA: 0x0031B5B8 File Offset: 0x003197B8
		public void Render(Page page)
		{
			this.idReferences = page.getIDReferences();
			this.pdfResources = this.pdfDoc.getResources();
			this.pdfDoc.setIDReferences(this.idReferences);
			this.RenderPage(page);
			ArrayList arrayList = page.getExtensions();
			if (arrayList != null)
			{
				this.extensions = arrayList;
			}
			this.pdfDoc.output();
		}

		// Token: 0x0600DF8D RID: 57229 RVA: 0x0031B618 File Offset: 0x00319818
		public void RenderPage(Page page)
		{
			this.currentStream = this.pdfDoc.makeContentStream();
			BodyAreaContainer body = page.getBody();
			AreaContainer before = page.getBefore();
			AreaContainer after = page.getAfter();
			AreaContainer start = page.getStart();
			AreaContainer end = page.getEnd();
			this.currentFontName = "";
			this.currentFontSize = 0;
			this.currentLetterSpacing = float.NaN;
			this.currentStream.Write("BT\n");
			this.RenderBodyAreaContainer(body);
			if (before != null)
			{
				this.RenderAreaContainer(before);
			}
			if (after != null)
			{
				this.RenderAreaContainer(after);
			}
			if (start != null)
			{
				this.RenderAreaContainer(start);
			}
			if (end != null)
			{
				this.RenderAreaContainer(end);
			}
			this.CloseText();
			this.currentLetterSpacing = float.NaN;
			float num = (float)page.getWidth();
			float num2 = (float)page.GetHeight();
			this.currentStream.Write("ET\n");
			this.currentPage = this.pdfDoc.makePage(this.pdfResources, this.currentStream, Convert.ToInt32(Math.Round((double)(num / 1000f))), Convert.ToInt32(Math.Round((double)(num2 / 1000f))), page);
			if (page.hasLinks() || this.currentAnnotList != null)
			{
				if (this.currentAnnotList == null)
				{
					this.currentAnnotList = this.pdfDoc.makeAnnotList();
				}
				this.currentPage.SetAnnotList(this.currentAnnotList);
				ArrayList linkSets = page.getLinkSets();
				foreach (object obj in linkSets)
				{
					LinkSet linkSet = (LinkSet)obj;
					linkSet.align();
					string dest = linkSet.getDest();
					int linkType = linkSet.getLinkType();
					ArrayList rects = linkSet.getRects();
					foreach (object obj2 in rects)
					{
						LinkedRectangle linkedRectangle = (LinkedRectangle)obj2;
						this.currentAnnotList.Add(this.pdfDoc.makeLink(linkedRectangle.getRectangle(), dest, linkType).GetReference());
					}
				}
				this.currentAnnotList = null;
			}
			else
			{
				this.currentAnnotList = null;
			}
			this.currentFill = null;
		}

		// Token: 0x0600DF8E RID: 57230 RVA: 0x0031B868 File Offset: 0x00319A68
		private string SetRuleStylePattern(int style)
		{
			string result;
			if (style != 16)
			{
				switch (style)
				{
				case 20:
					result = "[1 3] 0 d ";
					break;
				case 21:
					result = "[] 0 d ";
					break;
				default:
					if (style == 70)
					{
						result = "[] 0 d ";
					}
					else
					{
						result = "[] 0 d ";
					}
					break;
				}
			}
			else
			{
				result = "[3 3] 0 d ";
			}
			return result;
		}

		// Token: 0x0600DF8F RID: 57231 RVA: 0x0031B8C0 File Offset: 0x00319AC0
		private void RenderRootExtensions(ArrayList exts)
		{
			if (exts != null)
			{
				foreach (object obj in exts)
				{
					ExtensionObj extensionObj = (ExtensionObj)obj;
					Outline outline = extensionObj as Outline;
					if (outline != null)
					{
						this.RenderOutline(outline);
					}
				}
			}
		}

		// Token: 0x0600DF90 RID: 57232 RVA: 0x0031B924 File Offset: 0x00319B24
		private void RenderOutline(Outline outline)
		{
			PdfOutline outlineRoot = this.pdfDoc.getOutlineRoot();
			PdfOutline rendererObject = null;
			Outline parentOutline = outline.ParentOutline;
			if (parentOutline == null)
			{
				rendererObject = this.pdfDoc.makeOutline(outlineRoot, outline.Label.toString(), outline.InternalDestination);
			}
			else
			{
				PdfOutline pdfOutline = (PdfOutline)parentOutline.RendererObject;
				if (pdfOutline != null)
				{
					rendererObject = this.pdfDoc.makeOutline(pdfOutline, outline.Label.toString(), outline.InternalDestination);
				}
			}
			outline.RendererObject = rendererObject;
			ArrayList outlines = outline.Outlines;
			foreach (object obj in outlines)
			{
				Outline outline2 = (Outline)obj;
				this.RenderOutline(outline2);
			}
		}

		// Token: 0x0600DF91 RID: 57233 RVA: 0x0031B9F8 File Offset: 0x00319BF8
		private void DoFrame(Area area)
		{
			int num = this.currentAreaContainerXPosition;
			int num2 = area.getContentWidth();
			BlockArea blockArea = area as BlockArea;
			if (blockArea != null)
			{
				num += blockArea.getStartIndent();
			}
			int num3 = area.getContentHeight();
			int num4 = this.currentYPosition;
			num -= area.getPaddingLeft();
			num4 += area.getPaddingTop();
			num2 = num2 + area.getPaddingLeft() + area.getPaddingRight();
			num3 = num3 + area.getPaddingTop() + area.getPaddingBottom();
			this.DoBackground(area, num, num4, num2, num3);
			BorderAndPadding borderAndPadding = area.GetBorderAndPadding();
			int borderLeftWidth = area.getBorderLeftWidth();
			int borderRightWidth = area.getBorderRightWidth();
			int borderTopWidth = area.getBorderTopWidth();
			int borderBottomWidth = area.getBorderBottomWidth();
			if (borderTopWidth != 0)
			{
				this.AddFilledRect(num, num4, num2, borderTopWidth, new PdfColor(borderAndPadding.getBorderColor(0)));
			}
			if (borderLeftWidth != 0)
			{
				this.AddFilledRect(num - borderLeftWidth, num4 - num3 - borderBottomWidth, borderLeftWidth, num3 + borderTopWidth + borderBottomWidth, new PdfColor(borderAndPadding.getBorderColor(3)));
			}
			if (borderRightWidth != 0)
			{
				this.AddFilledRect(num + num2, num4 - num3 - borderBottomWidth, borderRightWidth, num3 + borderTopWidth + borderBottomWidth, new PdfColor(borderAndPadding.getBorderColor(1)));
			}
			if (borderBottomWidth != 0)
			{
				this.AddFilledRect(num, num4 - num3 - borderBottomWidth, num2, borderBottomWidth, new PdfColor(borderAndPadding.getBorderColor(2)));
			}
		}

		// Token: 0x0600DF92 RID: 57234 RVA: 0x0031BB34 File Offset: 0x00319D34
		private void DoBackground(Area area, int x, int y, int w, int h)
		{
			if (h == 0 || w == 0)
			{
				return;
			}
			BackgroundProps background = area.getBackground();
			if (background == null)
			{
				return;
			}
			if (background.backColor.Alpha == 0f)
			{
				this.AddFilledRect(x, y, w, -h, new PdfColor(background.backColor));
			}
			if (background.backImage != null)
			{
				int num = background.backImage.Width * 1000;
				int num2 = background.backImage.Height * 1000;
				int i = x;
				int j = y;
				int num3 = x + w;
				int num4 = y - h;
				int clipW = w % num;
				int clipH = h % num2;
				bool flag = true;
				bool flag2 = true;
				int backRepeat = background.backRepeat;
				if (backRepeat != 35)
				{
					switch (backRepeat)
					{
					case 87:
						break;
					case 88:
						flag2 = false;
						break;
					case 89:
						flag = false;
						break;
					case 90:
						flag = false;
						flag2 = false;
						break;
					default:
						ApocDriver.ActiveDriver.FireApocWarning("Ignoring invalid background-repeat property");
						break;
					}
				}
				while (j > num4)
				{
					while (i < num3)
					{
						if (i + num <= num3)
						{
							if (j - num2 >= num4)
							{
								this.DrawImageScaled(i, j, num, num2, background.backImage);
							}
							else
							{
								this.DrawImageClipped(i, j, 0, 0, num, clipH, background.backImage);
							}
						}
						else if (j - num2 >= num4)
						{
							this.DrawImageClipped(i, j, 0, 0, clipW, num2, background.backImage);
						}
						else
						{
							this.DrawImageClipped(i, j, 0, 0, clipW, clipH, background.backImage);
						}
						if (!flag)
						{
							break;
						}
						i += num;
					}
					i = x;
					if (!flag2)
					{
						break;
					}
					j -= num2;
				}
			}
		}

		// Token: 0x0600DF93 RID: 57235 RVA: 0x0031BCB8 File Offset: 0x00319EB8
		private void DrawImage(int x, int y, ApocImage image)
		{
			int w = image.Width * 1000;
			int h = image.Height * 1000;
			this.DrawImageScaled(x, y, w, h, image);
		}

		// Token: 0x0600DF94 RID: 57236 RVA: 0x0031BCEC File Offset: 0x00319EEC
		private void DrawImageScaled(int x, int y, int w, int h, ApocImage image)
		{
			PdfXObject pdfXObject = this.pdfDoc.AddImage(image);
			this.CloseText();
			this.currentStream.Write(string.Concat(new string[]
			{
				"ET\nq\n",
				PdfNumber.doubleOut((double)((float)w / 1000f)),
				" 0 0 ",
				PdfNumber.doubleOut((double)((float)h / 1000f)),
				" ",
				PdfNumber.doubleOut((double)((float)x / 1000f)),
				" ",
				PdfNumber.doubleOut((double)((float)(y - h) / 1000f)),
				" cm\n/",
				pdfXObject.Name.Name,
				" Do\nQ\nBT\n"
			}));
		}

		// Token: 0x0600DF95 RID: 57237 RVA: 0x0031BDB0 File Offset: 0x00319FB0
		private void DrawImageClipped(int x, int y, int clipX, int clipY, int clipW, int clipH, ApocImage image)
		{
			float num = (float)x / 1000f;
			float num2 = ((float)y - (float)clipH) / 1000f;
			float num3 = ((float)x + (float)clipW) / 1000f;
			float num4 = (float)y / 1000f;
			int num5 = x - clipX;
			int num6 = y - clipY;
			int num7 = image.Width * 1000;
			int num8 = image.Height * 1000;
			PdfXObject pdfXObject = this.pdfDoc.AddImage(image);
			this.CloseText();
			this.currentStream.Write(string.Concat(new string[]
			{
				"ET\nq\n",
				PdfNumber.doubleOut((double)num),
				" ",
				PdfNumber.doubleOut((double)num2),
				" m\n",
				PdfNumber.doubleOut((double)num3),
				" ",
				PdfNumber.doubleOut((double)num2),
				" l\n",
				PdfNumber.doubleOut((double)num3),
				" ",
				PdfNumber.doubleOut((double)num4),
				" l\n",
				PdfNumber.doubleOut((double)num),
				" ",
				PdfNumber.doubleOut((double)num4),
				" l\nW\nn\n",
				PdfNumber.doubleOut((double)((float)num7 / 1000f)),
				" 0 0 ",
				PdfNumber.doubleOut((double)((float)num8 / 1000f)),
				" ",
				PdfNumber.doubleOut((double)((float)num5 / 1000f)),
				" ",
				PdfNumber.doubleOut((double)(((float)num6 - (float)num8) / 1000f)),
				" cm\ns\n/",
				pdfXObject.Name.Name,
				" Do\nQ\nBT\n"
			}));
		}

		// Token: 0x0600DF96 RID: 57238 RVA: 0x0031BF8C File Offset: 0x0031A18C
		public void RenderDisplaySpace(DisplaySpace space)
		{
			int size = space.getSize();
			this.currentYPosition -= size;
		}

		// Token: 0x0600DF97 RID: 57239 RVA: 0x0031BFB0 File Offset: 0x0031A1B0
		private void AddWordLines(WordArea area, int rx, int bl, int size, PdfColor theAreaColor)
		{
			if (area.getUnderlined())
			{
				int num = bl - size / 10;
				this.AddLine(rx, num, rx + area.getContentWidth(), num, size / 14, theAreaColor);
				this.prevUnderlineXEndPos = rx + area.getContentWidth();
				this.prevUnderlineYEndPos = num;
				this.prevUnderlineSize = size / 14;
				this.prevUnderlineColor = theAreaColor;
			}
			if (area.getOverlined())
			{
				int num2 = bl + area.GetFontState().Ascender + size / 10;
				this.AddLine(rx, num2, rx + area.getContentWidth(), num2, size / 14, theAreaColor);
				this.prevOverlineXEndPos = rx + area.getContentWidth();
				this.prevOverlineYEndPos = num2;
				this.prevOverlineSize = size / 14;
				this.prevOverlineColor = theAreaColor;
			}
			if (area.getLineThrough())
			{
				int num3 = bl + area.GetFontState().Ascender * 3 / 8;
				this.AddLine(rx, num3, rx + area.getContentWidth(), num3, size / 14, theAreaColor);
				this.prevLineThroughXEndPos = rx + area.getContentWidth();
				this.prevLineThroughYEndPos = num3;
				this.prevLineThroughSize = size / 14;
				this.prevLineThroughColor = theAreaColor;
			}
		}

		// Token: 0x0600DF98 RID: 57240 RVA: 0x0031C0C4 File Offset: 0x0031A2C4
		public void RenderInlineSpace(InlineSpace space)
		{
			this.currentXPosition += space.getSize();
			if (space.getUnderlined() && this.prevUnderlineColor != null)
			{
				this.AddLine(this.prevUnderlineXEndPos, this.prevUnderlineYEndPos, this.prevUnderlineXEndPos + space.getSize(), this.prevUnderlineYEndPos, this.prevUnderlineSize, this.prevUnderlineColor);
				this.prevUnderlineXEndPos += space.getSize();
			}
			if (space.getOverlined() && this.prevOverlineColor != null)
			{
				this.AddLine(this.prevOverlineXEndPos, this.prevOverlineYEndPos, this.prevOverlineXEndPos + space.getSize(), this.prevOverlineYEndPos, this.prevOverlineSize, this.prevOverlineColor);
				this.prevOverlineXEndPos += space.getSize();
			}
			if (space.getLineThrough() && this.prevLineThroughColor != null)
			{
				this.AddLine(this.prevLineThroughXEndPos, this.prevLineThroughYEndPos, this.prevLineThroughXEndPos + space.getSize(), this.prevLineThroughYEndPos, this.prevLineThroughSize, this.prevLineThroughColor);
				this.prevLineThroughXEndPos += space.getSize();
			}
		}

		// Token: 0x0600DF99 RID: 57241 RVA: 0x0031C1E0 File Offset: 0x0031A3E0
		public void RenderLeaderArea(LeaderArea area)
		{
			int num = this.currentXPosition;
			int num2 = this.currentYPosition;
			int contentWidth = area.getContentWidth();
			area.GetHeight();
			int ruleThickness = area.getRuleThickness();
			int ruleStyle = area.getRuleStyle();
			if (ruleThickness != 0)
			{
				int num3 = ruleStyle;
				if (num3 != 21)
				{
					if (num3 != 33)
					{
						if (num3 != 64)
						{
							this.AddLine(num, num2, num + contentWidth, num2, ruleThickness, ruleStyle, new PdfColor((double)area.getRed(), (double)area.getGreen(), (double)area.getBlue()));
						}
						else
						{
							this.AddLine(num, num2, num + contentWidth, num2, ruleThickness / 2, ruleStyle, new PdfColor(255, 255, 255));
							this.AddLine(num, num2 + ruleThickness / 2, num + contentWidth, num2 + ruleThickness / 2, ruleThickness / 2, ruleStyle, new PdfColor((double)area.getRed(), (double)area.getGreen(), (double)area.getBlue()));
						}
					}
					else
					{
						this.AddLine(num, num2, num + contentWidth, num2, ruleThickness / 2, ruleStyle, new PdfColor((double)area.getRed(), (double)area.getGreen(), (double)area.getBlue()));
						this.AddLine(num, num2 + ruleThickness / 2, num + contentWidth, num2 + ruleThickness / 2, ruleThickness / 2, ruleStyle, new PdfColor(255, 255, 255));
					}
				}
				else
				{
					this.AddLine(num, num2, num + contentWidth, num2, ruleThickness / 3, ruleStyle, new PdfColor((double)area.getRed(), (double)area.getGreen(), (double)area.getBlue()));
					this.AddLine(num, num2 + 2 * ruleThickness / 3, num + contentWidth, num2 + 2 * ruleThickness / 3, ruleThickness / 3, ruleStyle, new PdfColor((double)area.getRed(), (double)area.getGreen(), (double)area.getBlue()));
				}
				this.currentXPosition += area.getContentWidth();
				this.currentYPosition += ruleThickness;
			}
		}

		// Token: 0x04004074 RID: 16500
		private int currentYPosition;

		// Token: 0x04004075 RID: 16501
		private int currentXPosition;

		// Token: 0x04004076 RID: 16502
		private int currentAreaContainerXPosition;

		// Token: 0x04004077 RID: 16503
		private PdfCreator pdfDoc;

		// Token: 0x04004078 RID: 16504
		private PdfResources pdfResources;

		// Token: 0x04004079 RID: 16505
		private PdfContentStream currentStream;

		// Token: 0x0400407A RID: 16506
		private PdfAnnotList currentAnnotList;

		// Token: 0x0400407B RID: 16507
		private PdfPage currentPage;

		// Token: 0x0400407C RID: 16508
		private float currentLetterSpacing = float.NaN;

		// Token: 0x0400407D RID: 16509
		private bool textOpen;

		// Token: 0x0400407E RID: 16510
		private int prevWordY;

		// Token: 0x0400407F RID: 16511
		private int prevWordX;

		// Token: 0x04004080 RID: 16512
		private int prevWordWidth;

		// Token: 0x04004081 RID: 16513
		private StringBuilder _wordAreaPDF = new StringBuilder();

		// Token: 0x04004082 RID: 16514
		private PdfRendererOptions options;

		// Token: 0x04004083 RID: 16515
		private ArrayList extensions;

		// Token: 0x04004084 RID: 16516
		private string currentFontName;

		// Token: 0x04004085 RID: 16517
		private int currentFontSize;

		// Token: 0x04004086 RID: 16518
		private PdfColor currentFill;

		// Token: 0x04004087 RID: 16519
		private int prevUnderlineXEndPos;

		// Token: 0x04004088 RID: 16520
		private int prevUnderlineYEndPos;

		// Token: 0x04004089 RID: 16521
		private int prevUnderlineSize;

		// Token: 0x0400408A RID: 16522
		private PdfColor prevUnderlineColor;

		// Token: 0x0400408B RID: 16523
		private int prevOverlineXEndPos;

		// Token: 0x0400408C RID: 16524
		private int prevOverlineYEndPos;

		// Token: 0x0400408D RID: 16525
		private int prevOverlineSize;

		// Token: 0x0400408E RID: 16526
		private PdfColor prevOverlineColor;

		// Token: 0x0400408F RID: 16527
		private int prevLineThroughXEndPos;

		// Token: 0x04004090 RID: 16528
		private int prevLineThroughYEndPos;

		// Token: 0x04004091 RID: 16529
		private int prevLineThroughSize;

		// Token: 0x04004092 RID: 16530
		private PdfColor prevLineThroughColor;

		// Token: 0x04004093 RID: 16531
		private FontInfo fontInfo;

		// Token: 0x04004094 RID: 16532
		private FontSetup fontSetup;

		// Token: 0x04004095 RID: 16533
		private IDReferences idReferences;
	}
}
