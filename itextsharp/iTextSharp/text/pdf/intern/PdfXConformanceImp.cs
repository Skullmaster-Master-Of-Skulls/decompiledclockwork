using System;
using iTextSharp.text.error_messages;
using iTextSharp.text.pdf.interfaces;

namespace iTextSharp.text.pdf.intern
{
	// Token: 0x020000E3 RID: 227
	public class PdfXConformanceImp : IPdfXConformance
	{
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x0002B84B File Offset: 0x0002A84B
		// (set) Token: 0x06000852 RID: 2130 RVA: 0x0002B842 File Offset: 0x0002A842
		public int PDFXConformance
		{
			get
			{
				return this.pdfxConformance;
			}
			set
			{
				this.pdfxConformance = value;
			}
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0002B853 File Offset: 0x0002A853
		public bool IsPdfX()
		{
			return this.pdfxConformance != 0;
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0002B861 File Offset: 0x0002A861
		public bool IsPdfX1A2001()
		{
			return this.pdfxConformance == 1;
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0002B86C File Offset: 0x0002A86C
		public bool IsPdfX32002()
		{
			return this.pdfxConformance == 2;
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0002B877 File Offset: 0x0002A877
		public bool IsPdfA1()
		{
			return this.pdfxConformance == 3 || this.pdfxConformance == 4;
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0002B88D File Offset: 0x0002A88D
		public bool IsPdfA1A()
		{
			return this.pdfxConformance == 3;
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0002B898 File Offset: 0x0002A898
		public void CompleteInfoDictionary(PdfDictionary info)
		{
			if (this.IsPdfX() && !this.IsPdfA1())
			{
				if (info.Get(PdfName.GTS_PDFXVERSION) == null)
				{
					if (this.IsPdfX1A2001())
					{
						info.Put(PdfName.GTS_PDFXVERSION, new PdfString("PDF/X-1:2001"));
						info.Put(new PdfName("GTS_PDFXConformance"), new PdfString("PDF/X-1a:2001"));
					}
					else if (this.IsPdfX32002())
					{
						info.Put(PdfName.GTS_PDFXVERSION, new PdfString("PDF/X-3:2002"));
					}
				}
				if (info.Get(PdfName.TITLE) == null)
				{
					info.Put(PdfName.TITLE, new PdfString("Pdf document"));
				}
				if (info.Get(PdfName.CREATOR) == null)
				{
					info.Put(PdfName.CREATOR, new PdfString("Unknown"));
				}
				if (info.Get(PdfName.TRAPPED) == null)
				{
					info.Put(PdfName.TRAPPED, new PdfName("False"));
				}
			}
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0002B984 File Offset: 0x0002A984
		public void CompleteExtraCatalog(PdfDictionary extraCatalog)
		{
			if (this.IsPdfX() && !this.IsPdfA1() && extraCatalog.Get(PdfName.OUTPUTINTENTS) == null)
			{
				PdfDictionary pdfDictionary = new PdfDictionary(PdfName.OUTPUTINTENT);
				pdfDictionary.Put(PdfName.OUTPUTCONDITION, new PdfString("SWOP CGATS TR 001-1995"));
				pdfDictionary.Put(PdfName.OUTPUTCONDITIONIDENTIFIER, new PdfString("CGATS TR 001"));
				pdfDictionary.Put(PdfName.REGISTRYNAME, new PdfString("http://www.color.org"));
				pdfDictionary.Put(PdfName.INFO, new PdfString(""));
				pdfDictionary.Put(PdfName.S, PdfName.GTS_PDFX);
				extraCatalog.Put(PdfName.OUTPUTINTENTS, new PdfArray(pdfDictionary));
			}
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0002BA38 File Offset: 0x0002AA38
		public static void CheckPDFXConformance(PdfWriter writer, int key, object obj1)
		{
			if (writer == null || !writer.IsPdfX())
			{
				return;
			}
			int pdfxconformance = writer.PDFXConformance;
			switch (key)
			{
			case 1:
			{
				int num = pdfxconformance;
				if (num != 1)
				{
					return;
				}
				if (obj1 is ExtendedColor)
				{
					ExtendedColor extendedColor = (ExtendedColor)obj1;
					switch (extendedColor.Type)
					{
					case 0:
						throw new PdfXConformanceException(MessageLocalization.GetComposedMessage("colorspace.rgb.is.not.allowed"));
					default:
						return;
					case 3:
					{
						SpotColor spotColor = (SpotColor)extendedColor;
						PdfXConformanceImp.CheckPDFXConformance(writer, 1, spotColor.PdfSpotColor.AlternativeCS);
						return;
					}
					case 4:
					{
						PatternColor patternColor = (PatternColor)extendedColor;
						PdfXConformanceImp.CheckPDFXConformance(writer, 1, patternColor.Painter.DefaultColor);
						return;
					}
					case 5:
					{
						ShadingColor shadingColor = (ShadingColor)extendedColor;
						PdfXConformanceImp.CheckPDFXConformance(writer, 1, shadingColor.PdfShadingPattern.Shading.ColorSpace);
						return;
					}
					}
				}
				else if (obj1 is BaseColor)
				{
					throw new PdfXConformanceException(MessageLocalization.GetComposedMessage("colorspace.rgb.is.not.allowed"));
				}
				break;
			}
			case 2:
				break;
			case 3:
				if (pdfxconformance == 1)
				{
					throw new PdfXConformanceException(MessageLocalization.GetComposedMessage("colorspace.rgb.is.not.allowed"));
				}
				break;
			case 4:
				if (!((BaseFont)obj1).IsEmbedded())
				{
					throw new PdfXConformanceException(MessageLocalization.GetComposedMessage("all.the.fonts.must.be.embedded.this.one.isn.t.1", ((BaseFont)obj1).PostscriptFontName));
				}
				break;
			case 5:
			{
				PdfImage pdfImage = (PdfImage)obj1;
				if (pdfImage.Get(PdfName.SMASK) != null)
				{
					throw new PdfXConformanceException(MessageLocalization.GetComposedMessage("the.smask.key.is.not.allowed.in.images"));
				}
				int num2 = pdfxconformance;
				if (num2 != 1)
				{
					return;
				}
				PdfObject pdfObject = pdfImage.Get(PdfName.COLORSPACE);
				if (pdfObject == null)
				{
					return;
				}
				if (pdfObject.IsName())
				{
					if (PdfName.DEVICERGB.Equals(pdfObject))
					{
						throw new PdfXConformanceException(MessageLocalization.GetComposedMessage("colorspace.rgb.is.not.allowed"));
					}
				}
				else if (pdfObject.IsArray() && PdfName.CALRGB.Equals(((PdfArray)pdfObject)[0]))
				{
					throw new PdfXConformanceException(MessageLocalization.GetComposedMessage("colorspace.calrgb.is.not.allowed"));
				}
				break;
			}
			case 6:
			{
				PdfDictionary pdfDictionary = (PdfDictionary)obj1;
				PdfObject pdfObject2 = pdfDictionary.Get(PdfName.BM);
				if (pdfObject2 != null && !PdfGState.BM_NORMAL.Equals(pdfObject2) && !PdfGState.BM_COMPATIBLE.Equals(pdfObject2))
				{
					throw new PdfXConformanceException(MessageLocalization.GetComposedMessage("blend.mode.1.not.allowed", pdfObject2.ToString()));
				}
				pdfObject2 = pdfDictionary.Get(PdfName.CA);
				double doubleValue;
				if (pdfObject2 != null && (doubleValue = ((PdfNumber)pdfObject2).DoubleValue) != 1.0)
				{
					throw new PdfXConformanceException(MessageLocalization.GetComposedMessage("transparency.is.not.allowed.ca.eq.1", doubleValue));
				}
				pdfObject2 = pdfDictionary.Get(PdfName.ca_);
				if (pdfObject2 != null && (doubleValue = ((PdfNumber)pdfObject2).DoubleValue) != 1.0)
				{
					throw new PdfXConformanceException(MessageLocalization.GetComposedMessage("transparency.is.not.allowed.ca.eq.1", doubleValue));
				}
				break;
			}
			case 7:
				throw new PdfXConformanceException(MessageLocalization.GetComposedMessage("layers.are.not.allowed"));
			default:
				return;
			}
		}

		// Token: 0x040006EB RID: 1771
		public const int PDFXKEY_COLOR = 1;

		// Token: 0x040006EC RID: 1772
		public const int PDFXKEY_CMYK = 2;

		// Token: 0x040006ED RID: 1773
		public const int PDFXKEY_RGB = 3;

		// Token: 0x040006EE RID: 1774
		public const int PDFXKEY_FONT = 4;

		// Token: 0x040006EF RID: 1775
		public const int PDFXKEY_IMAGE = 5;

		// Token: 0x040006F0 RID: 1776
		public const int PDFXKEY_GSTATE = 6;

		// Token: 0x040006F1 RID: 1777
		public const int PDFXKEY_LAYER = 7;

		// Token: 0x040006F2 RID: 1778
		protected internal int pdfxConformance;
	}
}
