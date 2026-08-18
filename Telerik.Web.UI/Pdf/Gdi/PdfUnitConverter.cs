using System;

namespace Telerik.Pdf.Gdi
{
	// Token: 0x02001635 RID: 5685
	internal class PdfUnitConverter
	{
		// Token: 0x0600DD05 RID: 56581 RVA: 0x003048C5 File Offset: 0x00302AC5
		public PdfUnitConverter(int emSquare)
		{
			this.emSquare = emSquare;
		}

		// Token: 0x0600DD06 RID: 56582 RVA: 0x003048D4 File Offset: 0x00302AD4
		public int ToPdfUnits(int value)
		{
			if (this.emSquare == 0)
			{
				return value;
			}
			if (value < 0)
			{
				long num = (long)(value % this.emSquare);
				long num2 = 1000L * num;
				long num3 = num / num2;
				return -(-1000 * value / this.emSquare - (int)num3);
			}
			return value / this.emSquare * 1000 + value % this.emSquare * 1000 / this.emSquare;
		}

		// Token: 0x04003E6B RID: 15979
		private int emSquare;
	}
}
