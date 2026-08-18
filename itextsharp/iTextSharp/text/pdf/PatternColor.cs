using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200039E RID: 926
	public class PatternColor : ExtendedColor
	{
		// Token: 0x0600200A RID: 8202 RVA: 0x000BF3DE File Offset: 0x000BE3DE
		public PatternColor(PdfPatternPainter painter) : base(4, 0.5f, 0.5f, 0.5f)
		{
			this.painter = painter;
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x0600200B RID: 8203 RVA: 0x000BF3FD File Offset: 0x000BE3FD
		public PdfPatternPainter Painter
		{
			get
			{
				return this.painter;
			}
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x000BF405 File Offset: 0x000BE405
		public override bool Equals(object obj)
		{
			return this == obj;
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x000BF40B File Offset: 0x000BE40B
		public override int GetHashCode()
		{
			return this.painter.GetHashCode();
		}

		// Token: 0x04001619 RID: 5657
		private PdfPatternPainter painter;
	}
}
