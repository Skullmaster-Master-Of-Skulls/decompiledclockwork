using System;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x020005D0 RID: 1488
	public class PdfShadingPattern : PdfDictionary
	{
		// Token: 0x0600333D RID: 13117 RVA: 0x0013E69C File Offset: 0x0013D69C
		public PdfShadingPattern(PdfShading shading)
		{
			float[] array = new float[6];
			array[0] = 1f;
			array[3] = 1f;
			this.matrix = array;
			base..ctor();
			this.writer = shading.Writer;
			base.Put(PdfName.PATTERNTYPE, new PdfNumber(2));
			this.shading = shading;
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x0600333E RID: 13118 RVA: 0x0013E6F1 File Offset: 0x0013D6F1
		internal PdfName PatternName
		{
			get
			{
				return this.patternName;
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x0600333F RID: 13119 RVA: 0x0013E6F9 File Offset: 0x0013D6F9
		internal PdfName ShadingName
		{
			get
			{
				return this.shading.ShadingName;
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06003340 RID: 13120 RVA: 0x0013E706 File Offset: 0x0013D706
		internal PdfIndirectReference PatternReference
		{
			get
			{
				if (this.patternReference == null)
				{
					this.patternReference = this.writer.PdfIndirectReference;
				}
				return this.patternReference;
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06003341 RID: 13121 RVA: 0x0013E727 File Offset: 0x0013D727
		internal PdfIndirectReference ShadingReference
		{
			get
			{
				return this.shading.ShadingReference;
			}
		}

		// Token: 0x170008E4 RID: 2276
		// (set) Token: 0x06003342 RID: 13122 RVA: 0x0013E734 File Offset: 0x0013D734
		internal int Name
		{
			set
			{
				this.patternName = new PdfName("P" + value);
			}
		}

		// Token: 0x06003343 RID: 13123 RVA: 0x0013E751 File Offset: 0x0013D751
		internal void AddToBody()
		{
			base.Put(PdfName.SHADING, this.ShadingReference);
			base.Put(PdfName.MATRIX, new PdfArray(this.matrix));
			this.writer.AddToBody(this, this.PatternReference);
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06003344 RID: 13124 RVA: 0x0013E78D File Offset: 0x0013D78D
		// (set) Token: 0x06003345 RID: 13125 RVA: 0x0013E795 File Offset: 0x0013D795
		public float[] Matrix
		{
			get
			{
				return this.matrix;
			}
			set
			{
				if (value.Length != 6)
				{
					throw new Exception(MessageLocalization.GetComposedMessage("the.matrix.size.must.be.6"));
				}
				this.matrix = value;
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06003346 RID: 13126 RVA: 0x0013E7B4 File Offset: 0x0013D7B4
		public PdfShading Shading
		{
			get
			{
				return this.shading;
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06003347 RID: 13127 RVA: 0x0013E7BC File Offset: 0x0013D7BC
		internal ColorDetails ColorDetails
		{
			get
			{
				return this.shading.ColorDetails;
			}
		}

		// Token: 0x040022C8 RID: 8904
		protected PdfShading shading;

		// Token: 0x040022C9 RID: 8905
		protected PdfWriter writer;

		// Token: 0x040022CA RID: 8906
		protected float[] matrix;

		// Token: 0x040022CB RID: 8907
		protected PdfName patternName;

		// Token: 0x040022CC RID: 8908
		protected PdfIndirectReference patternReference;
	}
}
