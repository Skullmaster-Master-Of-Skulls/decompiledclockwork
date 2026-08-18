using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x020000CC RID: 204
	public class PdfFont : IComparable<PdfFont>
	{
		// Token: 0x06000716 RID: 1814 RVA: 0x000259A4 File Offset: 0x000249A4
		internal PdfFont(BaseFont bf, float size)
		{
			this.size = size;
			this.font = bf;
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x000259C8 File Offset: 0x000249C8
		public int CompareTo(PdfFont pdfFont)
		{
			if (this.image != null)
			{
				return 0;
			}
			if (pdfFont == null)
			{
				return -1;
			}
			int result;
			try
			{
				if (this.font != pdfFont.font)
				{
					result = 1;
				}
				else if (this.Size != pdfFont.Size)
				{
					result = 2;
				}
				else
				{
					result = 0;
				}
			}
			catch (InvalidCastException)
			{
				result = -2;
			}
			return result;
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x00025A24 File Offset: 0x00024A24
		internal float Size
		{
			get
			{
				if (this.image == null)
				{
					return this.size;
				}
				return this.image.ScaledHeight;
			}
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00025A40 File Offset: 0x00024A40
		internal float Width()
		{
			return this.Width(32);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00025A4A File Offset: 0x00024A4A
		internal float Width(int character)
		{
			if (this.image == null)
			{
				return this.font.GetWidthPoint(character, this.size) * this.hScale;
			}
			return this.image.ScaledWidth;
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00025A79 File Offset: 0x00024A79
		internal float Width(string s)
		{
			if (this.image == null)
			{
				return this.font.GetWidthPoint(s, this.size) * this.hScale;
			}
			return this.image.ScaledWidth;
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x00025AA8 File Offset: 0x00024AA8
		internal BaseFont Font
		{
			get
			{
				return this.font;
			}
		}

		// Token: 0x1700017A RID: 378
		// (set) Token: 0x0600071D RID: 1821 RVA: 0x00025AB0 File Offset: 0x00024AB0
		internal Image Image
		{
			set
			{
				this.image = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x00025ABC File Offset: 0x00024ABC
		internal static PdfFont DefaultFont
		{
			get
			{
				BaseFont bf = BaseFont.CreateFont("Helvetica", "Cp1252", false);
				return new PdfFont(bf, 12f);
			}
		}

		// Token: 0x1700017C RID: 380
		// (set) Token: 0x0600071F RID: 1823 RVA: 0x00025AE5 File Offset: 0x00024AE5
		internal float HorizontalScaling
		{
			set
			{
				this.hScale = value;
			}
		}

		// Token: 0x04000612 RID: 1554
		private BaseFont font;

		// Token: 0x04000613 RID: 1555
		private float size;

		// Token: 0x04000614 RID: 1556
		protected Image image;

		// Token: 0x04000615 RID: 1557
		protected float hScale = 1f;
	}
}
