using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x020005D1 RID: 1489
	public class PdfRectangle : PdfArray
	{
		// Token: 0x06003348 RID: 13128 RVA: 0x0013E7CC File Offset: 0x0013D7CC
		public PdfRectangle(float llx, float lly, float urx, float ury, int rotation)
		{
			if (rotation == 90 || rotation == 270)
			{
				this.llx = lly;
				this.lly = llx;
				this.urx = ury;
				this.ury = urx;
			}
			else
			{
				this.llx = llx;
				this.lly = lly;
				this.urx = urx;
				this.ury = ury;
			}
			base.Add(new PdfNumber(this.llx));
			base.Add(new PdfNumber(this.lly));
			base.Add(new PdfNumber(this.urx));
			base.Add(new PdfNumber(this.ury));
		}

		// Token: 0x06003349 RID: 13129 RVA: 0x0013E872 File Offset: 0x0013D872
		public PdfRectangle(float llx, float lly, float urx, float ury) : this(llx, lly, urx, ury, 0)
		{
		}

		// Token: 0x0600334A RID: 13130 RVA: 0x0013E880 File Offset: 0x0013D880
		public PdfRectangle(float urx, float ury, int rotation) : this(0f, 0f, urx, ury, rotation)
		{
		}

		// Token: 0x0600334B RID: 13131 RVA: 0x0013E895 File Offset: 0x0013D895
		public PdfRectangle(float urx, float ury) : this(0f, 0f, urx, ury, 0)
		{
		}

		// Token: 0x0600334C RID: 13132 RVA: 0x0013E8AA File Offset: 0x0013D8AA
		public PdfRectangle(Rectangle rectangle, int rotation) : this(rectangle.Left, rectangle.Bottom, rectangle.Right, rectangle.Top, rotation)
		{
		}

		// Token: 0x0600334D RID: 13133 RVA: 0x0013E8CB File Offset: 0x0013D8CB
		public PdfRectangle(Rectangle rectangle) : this(rectangle.Left, rectangle.Bottom, rectangle.Right, rectangle.Top, 0)
		{
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x0600334E RID: 13134 RVA: 0x0013E8EC File Offset: 0x0013D8EC
		public Rectangle Rectangle
		{
			get
			{
				return new Rectangle(this.Left, this.Bottom, this.Right, this.Top);
			}
		}

		// Token: 0x0600334F RID: 13135 RVA: 0x0013E90B File Offset: 0x0013D90B
		public override bool Add(PdfObject obj)
		{
			return false;
		}

		// Token: 0x06003350 RID: 13136 RVA: 0x0013E90E File Offset: 0x0013D90E
		public override bool Add(float[] values)
		{
			return false;
		}

		// Token: 0x06003351 RID: 13137 RVA: 0x0013E911 File Offset: 0x0013D911
		public override bool Add(int[] values)
		{
			return false;
		}

		// Token: 0x06003352 RID: 13138 RVA: 0x0013E914 File Offset: 0x0013D914
		public override void AddFirst(PdfObject obj)
		{
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06003353 RID: 13139 RVA: 0x0013E916 File Offset: 0x0013D916
		public float Left
		{
			get
			{
				return this.llx;
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06003354 RID: 13140 RVA: 0x0013E91E File Offset: 0x0013D91E
		public float Right
		{
			get
			{
				return this.urx;
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06003355 RID: 13141 RVA: 0x0013E926 File Offset: 0x0013D926
		public float Top
		{
			get
			{
				return this.ury;
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06003356 RID: 13142 RVA: 0x0013E92E File Offset: 0x0013D92E
		public float Bottom
		{
			get
			{
				return this.lly;
			}
		}

		// Token: 0x06003357 RID: 13143 RVA: 0x0013E936 File Offset: 0x0013D936
		public float GetLeft(int margin)
		{
			return this.llx + (float)margin;
		}

		// Token: 0x06003358 RID: 13144 RVA: 0x0013E941 File Offset: 0x0013D941
		public float GetRight(int margin)
		{
			return this.urx - (float)margin;
		}

		// Token: 0x06003359 RID: 13145 RVA: 0x0013E94C File Offset: 0x0013D94C
		public float GetTop(int margin)
		{
			return this.ury - (float)margin;
		}

		// Token: 0x0600335A RID: 13146 RVA: 0x0013E957 File Offset: 0x0013D957
		public float GetBottom(int margin)
		{
			return this.lly + (float)margin;
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x0600335B RID: 13147 RVA: 0x0013E962 File Offset: 0x0013D962
		public float Width
		{
			get
			{
				return this.urx - this.llx;
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x0600335C RID: 13148 RVA: 0x0013E971 File Offset: 0x0013D971
		public float Height
		{
			get
			{
				return this.ury - this.lly;
			}
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x0600335D RID: 13149 RVA: 0x0013E980 File Offset: 0x0013D980
		public PdfRectangle Rotate
		{
			get
			{
				return new PdfRectangle(this.lly, this.llx, this.ury, this.urx, 0);
			}
		}

		// Token: 0x040022CD RID: 8909
		private float llx;

		// Token: 0x040022CE RID: 8910
		private float lly;

		// Token: 0x040022CF RID: 8911
		private float urx;

		// Token: 0x040022D0 RID: 8912
		private float ury;
	}
}
