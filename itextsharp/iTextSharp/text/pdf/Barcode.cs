using System;
using System.Drawing;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000061 RID: 97
	public abstract class Barcode
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00010626 File Offset: 0x0000F626
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x0001062E File Offset: 0x0000F62E
		public float X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00010637 File Offset: 0x0000F637
		// (set) Token: 0x060002EB RID: 747 RVA: 0x0001063F File Offset: 0x0000F63F
		public float N
		{
			get
			{
				return this.n;
			}
			set
			{
				this.n = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00010648 File Offset: 0x0000F648
		// (set) Token: 0x060002ED RID: 749 RVA: 0x00010650 File Offset: 0x0000F650
		public BaseFont Font
		{
			get
			{
				return this.font;
			}
			set
			{
				this.font = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00010659 File Offset: 0x0000F659
		// (set) Token: 0x060002EF RID: 751 RVA: 0x00010661 File Offset: 0x0000F661
		public float Size
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0001066A File Offset: 0x0000F66A
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x00010672 File Offset: 0x0000F672
		public float Baseline
		{
			get
			{
				return this.baseline;
			}
			set
			{
				this.baseline = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0001067B File Offset: 0x0000F67B
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x00010683 File Offset: 0x0000F683
		public float BarHeight
		{
			get
			{
				return this.barHeight;
			}
			set
			{
				this.barHeight = value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0001068C File Offset: 0x0000F68C
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x00010694 File Offset: 0x0000F694
		public int TextAlignment
		{
			get
			{
				return this.textAlignment;
			}
			set
			{
				this.textAlignment = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x000106A6 File Offset: 0x0000F6A6
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x0001069D File Offset: 0x0000F69D
		public bool GenerateChecksum
		{
			get
			{
				return this.generateChecksum;
			}
			set
			{
				this.generateChecksum = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x000106B7 File Offset: 0x0000F6B7
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x000106AE File Offset: 0x0000F6AE
		public bool ChecksumText
		{
			get
			{
				return this.checksumText;
			}
			set
			{
				this.checksumText = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002FB RID: 763 RVA: 0x000106C8 File Offset: 0x0000F6C8
		// (set) Token: 0x060002FA RID: 762 RVA: 0x000106BF File Offset: 0x0000F6BF
		public bool StartStopText
		{
			get
			{
				return this.startStopText;
			}
			set
			{
				this.startStopText = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002FD RID: 765 RVA: 0x000106D9 File Offset: 0x0000F6D9
		// (set) Token: 0x060002FC RID: 764 RVA: 0x000106D0 File Offset: 0x0000F6D0
		public bool Extended
		{
			get
			{
				return this.extended;
			}
			set
			{
				this.extended = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002FE RID: 766 RVA: 0x000106E1 File Offset: 0x0000F6E1
		// (set) Token: 0x060002FF RID: 767 RVA: 0x000106E9 File Offset: 0x0000F6E9
		public virtual string Code
		{
			get
			{
				return this.code;
			}
			set
			{
				this.code = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000301 RID: 769 RVA: 0x000106FB File Offset: 0x0000F6FB
		// (set) Token: 0x06000300 RID: 768 RVA: 0x000106F2 File Offset: 0x0000F6F2
		public bool GuardBars
		{
			get
			{
				return this.guardBars;
			}
			set
			{
				this.guardBars = value;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00010703 File Offset: 0x0000F703
		// (set) Token: 0x06000303 RID: 771 RVA: 0x0001070B File Offset: 0x0000F70B
		public int CodeType
		{
			get
			{
				return this.codeType;
			}
			set
			{
				this.codeType = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000304 RID: 772
		public abstract Rectangle BarcodeSize { get; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0001071D File Offset: 0x0000F71D
		// (set) Token: 0x06000305 RID: 773 RVA: 0x00010714 File Offset: 0x0000F714
		public float InkSpreading
		{
			get
			{
				return this.inkSpreading;
			}
			set
			{
				this.inkSpreading = value;
			}
		}

		// Token: 0x06000307 RID: 775
		public abstract Rectangle PlaceBarcode(PdfContentByte cb, BaseColor barColor, BaseColor textColor);

		// Token: 0x06000308 RID: 776 RVA: 0x00010728 File Offset: 0x0000F728
		public PdfTemplate CreateTemplateWithBarcode(PdfContentByte cb, BaseColor barColor, BaseColor textColor)
		{
			PdfTemplate pdfTemplate = cb.CreateTemplate(0f, 0f);
			Rectangle boundingBox = this.PlaceBarcode(pdfTemplate, barColor, textColor);
			pdfTemplate.BoundingBox = boundingBox;
			return pdfTemplate;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00010758 File Offset: 0x0000F758
		public Image CreateImageWithBarcode(PdfContentByte cb, BaseColor barColor, BaseColor textColor)
		{
			return Image.GetInstance(this.CreateTemplateWithBarcode(cb, barColor, textColor));
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00010771 File Offset: 0x0000F771
		// (set) Token: 0x0600030A RID: 778 RVA: 0x00010768 File Offset: 0x0000F768
		public string AltText
		{
			get
			{
				return this.altText;
			}
			set
			{
				this.altText = value;
			}
		}

		// Token: 0x0600030C RID: 780
		public abstract Image CreateDrawingImage(Color foreground, Color background);

		// Token: 0x0400018D RID: 397
		public const int EAN13 = 1;

		// Token: 0x0400018E RID: 398
		public const int EAN8 = 2;

		// Token: 0x0400018F RID: 399
		public const int UPCA = 3;

		// Token: 0x04000190 RID: 400
		public const int UPCE = 4;

		// Token: 0x04000191 RID: 401
		public const int SUPP2 = 5;

		// Token: 0x04000192 RID: 402
		public const int SUPP5 = 6;

		// Token: 0x04000193 RID: 403
		public const int POSTNET = 7;

		// Token: 0x04000194 RID: 404
		public const int PLANET = 8;

		// Token: 0x04000195 RID: 405
		public const int CODE128 = 9;

		// Token: 0x04000196 RID: 406
		public const int CODE128_UCC = 10;

		// Token: 0x04000197 RID: 407
		public const int CODE128_RAW = 11;

		// Token: 0x04000198 RID: 408
		public const int CODABAR = 12;

		// Token: 0x04000199 RID: 409
		protected float x;

		// Token: 0x0400019A RID: 410
		protected float n;

		// Token: 0x0400019B RID: 411
		protected BaseFont font;

		// Token: 0x0400019C RID: 412
		protected float size;

		// Token: 0x0400019D RID: 413
		protected float baseline;

		// Token: 0x0400019E RID: 414
		protected float barHeight;

		// Token: 0x0400019F RID: 415
		protected int textAlignment;

		// Token: 0x040001A0 RID: 416
		protected bool generateChecksum;

		// Token: 0x040001A1 RID: 417
		protected bool checksumText;

		// Token: 0x040001A2 RID: 418
		protected bool startStopText;

		// Token: 0x040001A3 RID: 419
		protected bool extended;

		// Token: 0x040001A4 RID: 420
		protected string code = "";

		// Token: 0x040001A5 RID: 421
		protected bool guardBars;

		// Token: 0x040001A6 RID: 422
		protected int codeType;

		// Token: 0x040001A7 RID: 423
		protected float inkSpreading;

		// Token: 0x040001A8 RID: 424
		protected string altText;
	}
}
