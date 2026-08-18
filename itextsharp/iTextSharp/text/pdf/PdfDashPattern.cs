using System;
using System.IO;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200052D RID: 1325
	public class PdfDashPattern : PdfArray
	{
		// Token: 0x06002D77 RID: 11639 RVA: 0x00115E5C File Offset: 0x00114E5C
		public PdfDashPattern()
		{
		}

		// Token: 0x06002D78 RID: 11640 RVA: 0x00115E85 File Offset: 0x00114E85
		public PdfDashPattern(float dash) : base(new PdfNumber(dash))
		{
			this.dash = dash;
		}

		// Token: 0x06002D79 RID: 11641 RVA: 0x00115EBC File Offset: 0x00114EBC
		public PdfDashPattern(float dash, float gap) : base(new PdfNumber(dash))
		{
			this.Add(new PdfNumber(gap));
			this.dash = dash;
			this.gap = gap;
		}

		// Token: 0x06002D7A RID: 11642 RVA: 0x00115F14 File Offset: 0x00114F14
		public PdfDashPattern(float dash, float gap, float phase) : base(new PdfNumber(dash))
		{
			this.Add(new PdfNumber(gap));
			this.dash = dash;
			this.gap = gap;
			this.phase = phase;
		}

		// Token: 0x06002D7B RID: 11643 RVA: 0x00115F70 File Offset: 0x00114F70
		public void Add(float n)
		{
			this.Add(new PdfNumber(n));
		}

		// Token: 0x06002D7C RID: 11644 RVA: 0x00115F80 File Offset: 0x00114F80
		public override void ToPdf(PdfWriter writer, Stream os)
		{
			os.WriteByte(91);
			if (this.dash >= 0f)
			{
				new PdfNumber(this.dash).ToPdf(writer, os);
				if (this.gap >= 0f)
				{
					os.WriteByte(32);
					new PdfNumber(this.gap).ToPdf(writer, os);
				}
			}
			os.WriteByte(93);
			if (this.phase >= 0f)
			{
				os.WriteByte(32);
				new PdfNumber(this.phase).ToPdf(writer, os);
			}
		}

		// Token: 0x04001F5B RID: 8027
		private float dash = -1f;

		// Token: 0x04001F5C RID: 8028
		private float gap = -1f;

		// Token: 0x04001F5D RID: 8029
		private float phase = -1f;
	}
}
