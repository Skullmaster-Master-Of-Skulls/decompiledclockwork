using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x020002C8 RID: 712
	public class ShadingColor : ExtendedColor
	{
		// Token: 0x06001AA7 RID: 6823 RVA: 0x0009CF0C File Offset: 0x0009BF0C
		public ShadingColor(PdfShadingPattern shadingPattern) : base(5, 0.5f, 0.5f, 0.5f)
		{
			this.shadingPattern = shadingPattern;
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001AA8 RID: 6824 RVA: 0x0009CF2B File Offset: 0x0009BF2B
		public PdfShadingPattern PdfShadingPattern
		{
			get
			{
				return this.shadingPattern;
			}
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x0009CF33 File Offset: 0x0009BF33
		public override bool Equals(object obj)
		{
			return this == obj;
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x0009CF39 File Offset: 0x0009BF39
		public override int GetHashCode()
		{
			return this.shadingPattern.GetHashCode();
		}

		// Token: 0x040011C2 RID: 4546
		private PdfShadingPattern shadingPattern;
	}
}
