using System;
using Telerik.Pdf;
using Telerik.Pdf.Filter;
using Telerik.Web.Apoc.Layout;
using Telerik.Web.Apoc.Render.Pdf.Fonts;

namespace Telerik.Web.Apoc.Pdf
{
	// Token: 0x02001655 RID: 5717
	internal sealed class PdfFontCreator
	{
		// Token: 0x0600DDA7 RID: 56743 RVA: 0x00306DF7 File Offset: 0x00304FF7
		public PdfFontCreator(PdfCreator creator)
		{
			this.creator = creator;
		}

		// Token: 0x0600DDA8 RID: 56744 RVA: 0x00306E08 File Offset: 0x00305008
		public PdfFont MakeFont(string pdfFontID, Font font)
		{
			Base14Font base14Font = font as Base14Font;
			PdfFont pdfFont;
			if (base14Font != null)
			{
				pdfFont = this.CreateBase14Font(pdfFontID, base14Font);
			}
			else
			{
				IFontMetric fontMetrics = this.GetFontMetrics(font);
				base14Font = (fontMetrics as Base14Font);
				if (base14Font != null)
				{
					pdfFont = this.CreateBase14Font(pdfFontID, base14Font);
				}
				else
				{
					TrueTypeFont trueTypeFont = fontMetrics as TrueTypeFont;
					if (trueTypeFont != null)
					{
						pdfFont = this.CreateTrueTypeFont(pdfFontID, font, trueTypeFont);
					}
					else
					{
						CIDFont cidFont = (CIDFont)fontMetrics;
						pdfFont = this.CreateCIDFont(pdfFontID, font, cidFont);
					}
				}
			}
			if (pdfFont == null)
			{
				throw new Exception("Unable to create Pdf font object for " + pdfFontID);
			}
			this.creator.AddObject(pdfFont);
			return pdfFont;
		}

		// Token: 0x0600DDA9 RID: 56745 RVA: 0x00306E94 File Offset: 0x00305094
		private PdfFont CreateCIDFont(string pdfFontID, Font font, CIDFont cidFont)
		{
			IFontDescriptor descriptor = font.Descriptor;
			PdfFontFile pdfFontFile = new PdfFontFile(this.NextObjectId(), descriptor.FontData, this.creator);
			PdfFontDescriptor pdfFontDescriptor = this.MakeFontDescriptor(pdfFontID, cidFont);
			pdfFontDescriptor.FontFile2 = pdfFontFile;
			PdfCIDSystemInfo systemInfo = new PdfCIDSystemInfo(cidFont.Registry, cidFont.Ordering, cidFont.Supplement);
			PdfCIDFont pdfCIDFont = new PdfCIDFont(this.NextObjectId(), PdfFontSubTypeEnum.CIDFontType2, font.FontName);
			pdfCIDFont.SystemInfo = systemInfo;
			pdfCIDFont.Descriptor = pdfFontDescriptor;
			pdfCIDFont.DefaultWidth = new PdfNumeric(cidFont.DefaultWidth);
			pdfCIDFont.Widths = cidFont.WArray;
			PdfCMap pdfCMap = new PdfCMap(this.NextObjectId());
			IFilter activeFilter = this.creator.RendererOptions.GetActiveFilter();
			if (activeFilter != null)
			{
				pdfCMap.AddFilter(activeFilter);
			}
			pdfCMap.SystemInfo = systemInfo;
			pdfCMap.AddBfRanges(cidFont.CMapEntries);
			PdfType0Font pdfType0Font = new PdfType0Font(this.NextObjectId(), pdfFontID, font.FontName);
			pdfType0Font.Encoding = new PdfName(cidFont.Encoding);
			pdfType0Font.Descendant = pdfCIDFont;
			pdfType0Font.ToUnicode = pdfCMap;
			this.creator.AddObject(pdfFontDescriptor);
			this.creator.AddObject(pdfCIDFont);
			this.creator.AddObject(pdfCMap);
			this.creator.AddObject(pdfFontFile);
			return pdfType0Font;
		}

		// Token: 0x0600DDAA RID: 56746 RVA: 0x00306FDD File Offset: 0x003051DD
		private PdfObjectId NextObjectId()
		{
			return this.creator.Doc.NextObjectId();
		}

		// Token: 0x0600DDAB RID: 56747 RVA: 0x00306FF0 File Offset: 0x003051F0
		private PdfType1Font CreateBase14Font(string pdfFontID, Base14Font base14)
		{
			return new PdfType1Font(this.NextObjectId(), pdfFontID, base14.FontName)
			{
				Encoding = new PdfName(base14.Encoding)
			};
		}

		// Token: 0x0600DDAC RID: 56748 RVA: 0x00307024 File Offset: 0x00305224
		private PdfTrueTypeFont CreateTrueTypeFont(string pdfFontID, Font font, TrueTypeFont ttf)
		{
			PdfFontDescriptor pdfFontDescriptor = this.MakeFontDescriptor(pdfFontID, ttf);
			PdfTrueTypeFont pdfTrueTypeFont = new PdfTrueTypeFont(this.NextObjectId(), pdfFontID, font.FontName);
			pdfTrueTypeFont.Encoding = new PdfName("WinAnsiEncoding");
			pdfTrueTypeFont.Descriptor = pdfFontDescriptor;
			pdfTrueTypeFont.FirstChar = new PdfNumeric(ttf.FirstChar);
			pdfTrueTypeFont.LastChar = new PdfNumeric(ttf.LastChar);
			pdfTrueTypeFont.Widths = ttf.Array;
			this.creator.AddObject(pdfFontDescriptor);
			return pdfTrueTypeFont;
		}

		// Token: 0x0600DDAD RID: 56749 RVA: 0x003070AC File Offset: 0x003052AC
		private IFontMetric GetFontMetrics(Font font)
		{
			ProxyFont proxyFont = font as ProxyFont;
			if (proxyFont != null)
			{
				return proxyFont.RealFont;
			}
			return font;
		}

		// Token: 0x0600DDAE RID: 56750 RVA: 0x003070CC File Offset: 0x003052CC
		private PdfFontDescriptor MakeFontDescriptor(string fontName, IFontMetric metrics)
		{
			IFontDescriptor descriptor = metrics.Descriptor;
			PdfFontDescriptor pdfFontDescriptor = new PdfFontDescriptor(fontName, this.NextObjectId());
			pdfFontDescriptor.Ascent = new PdfNumeric(metrics.Ascender);
			pdfFontDescriptor.CapHeight = new PdfNumeric(metrics.CapHeight);
			pdfFontDescriptor.Descent = new PdfNumeric(metrics.Descender);
			pdfFontDescriptor.Flags = new PdfNumeric(descriptor.Flags);
			pdfFontDescriptor.ItalicAngle = new PdfNumeric(descriptor.ItalicAngle);
			pdfFontDescriptor.StemV = new PdfNumeric(descriptor.StemV);
			PdfArray pdfArray = new PdfArray();
			pdfArray.AddArray(descriptor.FontBBox);
			pdfFontDescriptor.FontBBox = pdfArray;
			return pdfFontDescriptor;
		}

		// Token: 0x04003F0C RID: 16140
		private PdfCreator creator;
	}
}
