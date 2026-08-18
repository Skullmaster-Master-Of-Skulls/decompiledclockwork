using System;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200058A RID: 1418
	public class PdfShading
	{
		// Token: 0x06003041 RID: 12353 RVA: 0x0012AF7A File Offset: 0x00129F7A
		protected PdfShading(PdfWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x06003042 RID: 12354 RVA: 0x0012AF8C File Offset: 0x00129F8C
		protected void SetColorSpace(BaseColor color)
		{
			this.cspace = color;
			int type = ExtendedColor.GetType(color);
			PdfObject value = null;
			switch (type)
			{
			case 1:
				value = PdfName.DEVICEGRAY;
				break;
			case 2:
				value = PdfName.DEVICECMYK;
				break;
			case 3:
			{
				SpotColor spotColor = (SpotColor)color;
				this.colorDetails = this.writer.AddSimple(spotColor.PdfSpotColor);
				value = this.colorDetails.IndirectReference;
				break;
			}
			case 4:
			case 5:
				PdfShading.ThrowColorSpaceError();
				break;
			default:
				value = PdfName.DEVICERGB;
				break;
			}
			this.shading.Put(PdfName.COLORSPACE, value);
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06003043 RID: 12355 RVA: 0x0012B023 File Offset: 0x0012A023
		public BaseColor ColorSpace
		{
			get
			{
				return this.cspace;
			}
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x0012B02B File Offset: 0x0012A02B
		public static void ThrowColorSpaceError()
		{
			throw new ArgumentException(MessageLocalization.GetComposedMessage("a.tiling.or.shading.pattern.cannot.be.used.as.a.color.space.in.a.shading.pattern"));
		}

		// Token: 0x06003045 RID: 12357 RVA: 0x0012B03C File Offset: 0x0012A03C
		public static void CheckCompatibleColors(BaseColor c1, BaseColor c2)
		{
			int type = ExtendedColor.GetType(c1);
			int type2 = ExtendedColor.GetType(c2);
			if (type != type2)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("both.colors.must.be.of.the.same.type"));
			}
			if (type == 3 && ((SpotColor)c1).PdfSpotColor != ((SpotColor)c2).PdfSpotColor)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("the.spot.color.must.be.the.same.only.the.tint.can.vary"));
			}
			if (type == 4 || type == 5)
			{
				PdfShading.ThrowColorSpaceError();
			}
		}

		// Token: 0x06003046 RID: 12358 RVA: 0x0012B0A4 File Offset: 0x0012A0A4
		public static float[] GetColorArray(BaseColor color)
		{
			switch (ExtendedColor.GetType(color))
			{
			case 0:
				return new float[]
				{
					(float)color.R / 255f,
					(float)color.G / 255f,
					(float)color.B / 255f
				};
			case 1:
				return new float[]
				{
					((GrayColor)color).Gray
				};
			case 2:
			{
				CMYKColor cmykcolor = (CMYKColor)color;
				return new float[]
				{
					cmykcolor.Cyan,
					cmykcolor.Magenta,
					cmykcolor.Yellow,
					cmykcolor.Black
				};
			}
			case 3:
				return new float[]
				{
					((SpotColor)color).Tint
				};
			default:
				PdfShading.ThrowColorSpaceError();
				return null;
			}
		}

		// Token: 0x06003047 RID: 12359 RVA: 0x0012B184 File Offset: 0x0012A184
		public static PdfShading Type1(PdfWriter writer, BaseColor colorSpace, float[] domain, float[] tMatrix, PdfFunction function)
		{
			PdfShading pdfShading = new PdfShading(writer);
			pdfShading.shading = new PdfDictionary();
			pdfShading.shadingType = 1;
			pdfShading.shading.Put(PdfName.SHADINGTYPE, new PdfNumber(pdfShading.shadingType));
			pdfShading.SetColorSpace(colorSpace);
			if (domain != null)
			{
				pdfShading.shading.Put(PdfName.DOMAIN, new PdfArray(domain));
			}
			if (tMatrix != null)
			{
				pdfShading.shading.Put(PdfName.MATRIX, new PdfArray(tMatrix));
			}
			pdfShading.shading.Put(PdfName.FUNCTION, function.Reference);
			return pdfShading;
		}

		// Token: 0x06003048 RID: 12360 RVA: 0x0012B218 File Offset: 0x0012A218
		public static PdfShading Type2(PdfWriter writer, BaseColor colorSpace, float[] coords, float[] domain, PdfFunction function, bool[] extend)
		{
			PdfShading pdfShading = new PdfShading(writer);
			pdfShading.shading = new PdfDictionary();
			pdfShading.shadingType = 2;
			pdfShading.shading.Put(PdfName.SHADINGTYPE, new PdfNumber(pdfShading.shadingType));
			pdfShading.SetColorSpace(colorSpace);
			pdfShading.shading.Put(PdfName.COORDS, new PdfArray(coords));
			if (domain != null)
			{
				pdfShading.shading.Put(PdfName.DOMAIN, new PdfArray(domain));
			}
			pdfShading.shading.Put(PdfName.FUNCTION, function.Reference);
			if (extend != null && (extend[0] || extend[1]))
			{
				PdfArray pdfArray = new PdfArray(extend[0] ? PdfBoolean.PDFTRUE : PdfBoolean.PDFFALSE);
				pdfArray.Add(extend[1] ? PdfBoolean.PDFTRUE : PdfBoolean.PDFFALSE);
				pdfShading.shading.Put(PdfName.EXTEND, pdfArray);
			}
			return pdfShading;
		}

		// Token: 0x06003049 RID: 12361 RVA: 0x0012B2FC File Offset: 0x0012A2FC
		public static PdfShading Type3(PdfWriter writer, BaseColor colorSpace, float[] coords, float[] domain, PdfFunction function, bool[] extend)
		{
			PdfShading pdfShading = PdfShading.Type2(writer, colorSpace, coords, domain, function, extend);
			pdfShading.shadingType = 3;
			pdfShading.shading.Put(PdfName.SHADINGTYPE, new PdfNumber(pdfShading.shadingType));
			return pdfShading;
		}

		// Token: 0x0600304A RID: 12362 RVA: 0x0012B33C File Offset: 0x0012A33C
		public static PdfShading SimpleAxial(PdfWriter writer, float x0, float y0, float x1, float y1, BaseColor startColor, BaseColor endColor, bool extendStart, bool extendEnd)
		{
			PdfShading.CheckCompatibleColors(startColor, endColor);
			PdfFunction function = PdfFunction.Type2(writer, new float[]
			{
				0f,
				1f
			}, null, PdfShading.GetColorArray(startColor), PdfShading.GetColorArray(endColor), 1f);
			return PdfShading.Type2(writer, startColor, new float[]
			{
				x0,
				y0,
				x1,
				y1
			}, null, function, new bool[]
			{
				extendStart,
				extendEnd
			});
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x0012B3B4 File Offset: 0x0012A3B4
		public static PdfShading SimpleAxial(PdfWriter writer, float x0, float y0, float x1, float y1, BaseColor startColor, BaseColor endColor)
		{
			return PdfShading.SimpleAxial(writer, x0, y0, x1, y1, startColor, endColor, true, true);
		}

		// Token: 0x0600304C RID: 12364 RVA: 0x0012B3D4 File Offset: 0x0012A3D4
		public static PdfShading SimpleRadial(PdfWriter writer, float x0, float y0, float r0, float x1, float y1, float r1, BaseColor startColor, BaseColor endColor, bool extendStart, bool extendEnd)
		{
			PdfShading.CheckCompatibleColors(startColor, endColor);
			PdfFunction function = PdfFunction.Type2(writer, new float[]
			{
				0f,
				1f
			}, null, PdfShading.GetColorArray(startColor), PdfShading.GetColorArray(endColor), 1f);
			return PdfShading.Type3(writer, startColor, new float[]
			{
				x0,
				y0,
				r0,
				x1,
				y1,
				r1
			}, null, function, new bool[]
			{
				extendStart,
				extendEnd
			});
		}

		// Token: 0x0600304D RID: 12365 RVA: 0x0012B454 File Offset: 0x0012A454
		public static PdfShading SimpleRadial(PdfWriter writer, float x0, float y0, float r0, float x1, float y1, float r1, BaseColor startColor, BaseColor endColor)
		{
			return PdfShading.SimpleRadial(writer, x0, y0, r0, x1, y1, r1, startColor, endColor, true, true);
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x0600304E RID: 12366 RVA: 0x0012B476 File Offset: 0x0012A476
		internal PdfName ShadingName
		{
			get
			{
				return this.shadingName;
			}
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x0600304F RID: 12367 RVA: 0x0012B47E File Offset: 0x0012A47E
		internal PdfIndirectReference ShadingReference
		{
			get
			{
				if (this.shadingReference == null)
				{
					this.shadingReference = this.writer.PdfIndirectReference;
				}
				return this.shadingReference;
			}
		}

		// Token: 0x17000843 RID: 2115
		// (set) Token: 0x06003050 RID: 12368 RVA: 0x0012B49F File Offset: 0x0012A49F
		internal int Name
		{
			set
			{
				this.shadingName = new PdfName("Sh" + value);
			}
		}

		// Token: 0x06003051 RID: 12369 RVA: 0x0012B4BC File Offset: 0x0012A4BC
		internal void AddToBody()
		{
			if (this.bBox != null)
			{
				this.shading.Put(PdfName.BBOX, new PdfArray(this.bBox));
			}
			if (this.antiAlias)
			{
				this.shading.Put(PdfName.ANTIALIAS, PdfBoolean.PDFTRUE);
			}
			this.writer.AddToBody(this.shading, this.ShadingReference);
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06003052 RID: 12370 RVA: 0x0012B521 File Offset: 0x0012A521
		internal PdfWriter Writer
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06003053 RID: 12371 RVA: 0x0012B529 File Offset: 0x0012A529
		internal ColorDetails ColorDetails
		{
			get
			{
				return this.colorDetails;
			}
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06003054 RID: 12372 RVA: 0x0012B531 File Offset: 0x0012A531
		// (set) Token: 0x06003055 RID: 12373 RVA: 0x0012B539 File Offset: 0x0012A539
		public float[] BBox
		{
			get
			{
				return this.bBox;
			}
			set
			{
				if (value.Length != 4)
				{
					throw new ArgumentException(MessageLocalization.GetComposedMessage("bbox.must.be.a.4.element.array"));
				}
				this.bBox = value;
			}
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06003057 RID: 12375 RVA: 0x0012B561 File Offset: 0x0012A561
		// (set) Token: 0x06003056 RID: 12374 RVA: 0x0012B558 File Offset: 0x0012A558
		public bool AntiAlias
		{
			get
			{
				return this.antiAlias;
			}
			set
			{
				this.antiAlias = value;
			}
		}

		// Token: 0x04002130 RID: 8496
		protected PdfDictionary shading;

		// Token: 0x04002131 RID: 8497
		protected PdfWriter writer;

		// Token: 0x04002132 RID: 8498
		protected int shadingType;

		// Token: 0x04002133 RID: 8499
		protected ColorDetails colorDetails;

		// Token: 0x04002134 RID: 8500
		protected PdfName shadingName;

		// Token: 0x04002135 RID: 8501
		protected PdfIndirectReference shadingReference;

		// Token: 0x04002136 RID: 8502
		protected float[] bBox;

		// Token: 0x04002137 RID: 8503
		protected bool antiAlias;

		// Token: 0x04002138 RID: 8504
		private BaseColor cspace;
	}
}
