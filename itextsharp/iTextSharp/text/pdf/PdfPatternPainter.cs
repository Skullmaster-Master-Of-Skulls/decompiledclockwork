using System;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x020004E0 RID: 1248
	public sealed class PdfPatternPainter : PdfTemplate
	{
		// Token: 0x06002A93 RID: 10899 RVA: 0x001039D4 File Offset: 0x001029D4
		private PdfPatternPainter()
		{
			this.type = 3;
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x001039E3 File Offset: 0x001029E3
		internal PdfPatternPainter(PdfWriter wr) : base(wr)
		{
			this.type = 3;
		}

		// Token: 0x06002A95 RID: 10901 RVA: 0x001039F3 File Offset: 0x001029F3
		internal PdfPatternPainter(PdfWriter wr, BaseColor defaultColor) : this(wr)
		{
			this.stencil = true;
			if (defaultColor == null)
			{
				this.defaultColor = BaseColor.GRAY;
				return;
			}
			this.defaultColor = defaultColor;
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06002A96 RID: 10902 RVA: 0x00103A19 File Offset: 0x00102A19
		// (set) Token: 0x06002A97 RID: 10903 RVA: 0x00103A21 File Offset: 0x00102A21
		public float XStep
		{
			get
			{
				return this.xstep;
			}
			set
			{
				this.xstep = value;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06002A98 RID: 10904 RVA: 0x00103A2A File Offset: 0x00102A2A
		// (set) Token: 0x06002A99 RID: 10905 RVA: 0x00103A32 File Offset: 0x00102A32
		public float YStep
		{
			get
			{
				return this.ystep;
			}
			set
			{
				this.ystep = value;
			}
		}

		// Token: 0x06002A9A RID: 10906 RVA: 0x00103A3B File Offset: 0x00102A3B
		public bool IsStencil()
		{
			return this.stencil;
		}

		// Token: 0x06002A9B RID: 10907 RVA: 0x00103A43 File Offset: 0x00102A43
		public void SetPatternMatrix(float a, float b, float c, float d, float e, float f)
		{
			base.SetMatrix(a, b, c, d, e, f);
		}

		// Token: 0x06002A9C RID: 10908 RVA: 0x00103A54 File Offset: 0x00102A54
		internal PdfPattern GetPattern()
		{
			return new PdfPattern(this);
		}

		// Token: 0x06002A9D RID: 10909 RVA: 0x00103A5C File Offset: 0x00102A5C
		internal PdfPattern GetPattern(int compressionLevel)
		{
			return new PdfPattern(this, compressionLevel);
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06002A9E RID: 10910 RVA: 0x00103A68 File Offset: 0x00102A68
		public override PdfContentByte Duplicate
		{
			get
			{
				return new PdfPatternPainter
				{
					writer = this.writer,
					pdf = this.pdf,
					thisReference = this.thisReference,
					pageResources = this.pageResources,
					bBox = new Rectangle(this.bBox),
					xstep = this.xstep,
					ystep = this.ystep,
					matrix = this.matrix,
					stencil = this.stencil,
					defaultColor = this.defaultColor
				};
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06002A9F RID: 10911 RVA: 0x00103AF9 File Offset: 0x00102AF9
		public BaseColor DefaultColor
		{
			get
			{
				return this.defaultColor;
			}
		}

		// Token: 0x06002AA0 RID: 10912 RVA: 0x00103B01 File Offset: 0x00102B01
		public override void SetGrayFill(float gray)
		{
			this.CheckNoColor();
			base.SetGrayFill(gray);
		}

		// Token: 0x06002AA1 RID: 10913 RVA: 0x00103B10 File Offset: 0x00102B10
		public override void ResetGrayFill()
		{
			this.CheckNoColor();
			base.ResetGrayFill();
		}

		// Token: 0x06002AA2 RID: 10914 RVA: 0x00103B1E File Offset: 0x00102B1E
		public override void SetGrayStroke(float gray)
		{
			this.CheckNoColor();
			base.SetGrayStroke(gray);
		}

		// Token: 0x06002AA3 RID: 10915 RVA: 0x00103B2D File Offset: 0x00102B2D
		public override void ResetGrayStroke()
		{
			this.CheckNoColor();
			base.ResetGrayStroke();
		}

		// Token: 0x06002AA4 RID: 10916 RVA: 0x00103B3B File Offset: 0x00102B3B
		public override void SetRGBColorFillF(float red, float green, float blue)
		{
			this.CheckNoColor();
			base.SetRGBColorFillF(red, green, blue);
		}

		// Token: 0x06002AA5 RID: 10917 RVA: 0x00103B4C File Offset: 0x00102B4C
		public override void ResetRGBColorFill()
		{
			this.CheckNoColor();
			base.ResetRGBColorFill();
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x00103B5A File Offset: 0x00102B5A
		public override void SetRGBColorStrokeF(float red, float green, float blue)
		{
			this.CheckNoColor();
			base.SetRGBColorStrokeF(red, green, blue);
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x00103B6B File Offset: 0x00102B6B
		public override void ResetRGBColorStroke()
		{
			this.CheckNoColor();
			base.ResetRGBColorStroke();
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x00103B79 File Offset: 0x00102B79
		public override void SetCMYKColorFillF(float cyan, float magenta, float yellow, float black)
		{
			this.CheckNoColor();
			base.SetCMYKColorFillF(cyan, magenta, yellow, black);
		}

		// Token: 0x06002AA9 RID: 10921 RVA: 0x00103B8C File Offset: 0x00102B8C
		public override void ResetCMYKColorFill()
		{
			this.CheckNoColor();
			base.ResetCMYKColorFill();
		}

		// Token: 0x06002AAA RID: 10922 RVA: 0x00103B9A File Offset: 0x00102B9A
		public override void SetCMYKColorStrokeF(float cyan, float magenta, float yellow, float black)
		{
			this.CheckNoColor();
			base.SetCMYKColorStrokeF(cyan, magenta, yellow, black);
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x00103BAD File Offset: 0x00102BAD
		public override void ResetCMYKColorStroke()
		{
			this.CheckNoColor();
			base.ResetCMYKColorStroke();
		}

		// Token: 0x06002AAC RID: 10924 RVA: 0x00103BBB File Offset: 0x00102BBB
		public override void AddImage(Image image, float a, float b, float c, float d, float e, float f)
		{
			if (this.stencil && !image.IsMask())
			{
				this.CheckNoColor();
			}
			base.AddImage(image, a, b, c, d, e, f);
		}

		// Token: 0x06002AAD RID: 10925 RVA: 0x00103BE4 File Offset: 0x00102BE4
		public override void SetCMYKColorFill(int cyan, int magenta, int yellow, int black)
		{
			this.CheckNoColor();
			base.SetCMYKColorFill(cyan, magenta, yellow, black);
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x00103BF7 File Offset: 0x00102BF7
		public override void SetCMYKColorStroke(int cyan, int magenta, int yellow, int black)
		{
			this.CheckNoColor();
			base.SetCMYKColorStroke(cyan, magenta, yellow, black);
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x00103C0A File Offset: 0x00102C0A
		public override void SetRGBColorFill(int red, int green, int blue)
		{
			this.CheckNoColor();
			base.SetRGBColorFill(red, green, blue);
		}

		// Token: 0x06002AB0 RID: 10928 RVA: 0x00103C1B File Offset: 0x00102C1B
		public override void SetRGBColorStroke(int red, int green, int blue)
		{
			this.CheckNoColor();
			base.SetRGBColorStroke(red, green, blue);
		}

		// Token: 0x06002AB1 RID: 10929 RVA: 0x00103C2C File Offset: 0x00102C2C
		public override void SetColorStroke(BaseColor color)
		{
			this.CheckNoColor();
			base.SetColorStroke(color);
		}

		// Token: 0x06002AB2 RID: 10930 RVA: 0x00103C3B File Offset: 0x00102C3B
		public override void SetColorFill(BaseColor color)
		{
			this.CheckNoColor();
			base.SetColorFill(color);
		}

		// Token: 0x06002AB3 RID: 10931 RVA: 0x00103C4A File Offset: 0x00102C4A
		public override void SetColorFill(PdfSpotColor sp, float tint)
		{
			this.CheckNoColor();
			base.SetColorFill(sp, tint);
		}

		// Token: 0x06002AB4 RID: 10932 RVA: 0x00103C5A File Offset: 0x00102C5A
		public override void SetColorStroke(PdfSpotColor sp, float tint)
		{
			this.CheckNoColor();
			base.SetColorStroke(sp, tint);
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x00103C6A File Offset: 0x00102C6A
		public override void SetPatternFill(PdfPatternPainter p)
		{
			this.CheckNoColor();
			base.SetPatternFill(p);
		}

		// Token: 0x06002AB6 RID: 10934 RVA: 0x00103C79 File Offset: 0x00102C79
		public override void SetPatternFill(PdfPatternPainter p, BaseColor color, float tint)
		{
			this.CheckNoColor();
			base.SetPatternFill(p, color, tint);
		}

		// Token: 0x06002AB7 RID: 10935 RVA: 0x00103C8A File Offset: 0x00102C8A
		public override void SetPatternStroke(PdfPatternPainter p, BaseColor color, float tint)
		{
			this.CheckNoColor();
			base.SetPatternStroke(p, color, tint);
		}

		// Token: 0x06002AB8 RID: 10936 RVA: 0x00103C9B File Offset: 0x00102C9B
		public override void SetPatternStroke(PdfPatternPainter p)
		{
			this.CheckNoColor();
			base.SetPatternStroke(p);
		}

		// Token: 0x06002AB9 RID: 10937 RVA: 0x00103CAA File Offset: 0x00102CAA
		internal void CheckNoColor()
		{
			if (this.stencil)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("colors.are.not.allowed.in.uncolored.tile.patterns"));
			}
		}

		// Token: 0x04001D98 RID: 7576
		internal float xstep;

		// Token: 0x04001D99 RID: 7577
		internal float ystep;

		// Token: 0x04001D9A RID: 7578
		internal bool stencil;

		// Token: 0x04001D9B RID: 7579
		internal BaseColor defaultColor;
	}
}
