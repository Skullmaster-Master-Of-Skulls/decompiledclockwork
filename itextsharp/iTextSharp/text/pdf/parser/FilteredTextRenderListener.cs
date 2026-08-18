using System;

namespace iTextSharp.text.pdf.parser
{
	// Token: 0x020004E5 RID: 1253
	public class FilteredTextRenderListener : FilteredRenderListener, ITextExtractionStrategy, IRenderListener
	{
		// Token: 0x06002ADB RID: 10971 RVA: 0x00104639 File Offset: 0x00103639
		public FilteredTextRenderListener(ITextExtractionStrategy deleg, RenderFilter[] filters) : base(deleg, filters)
		{
			this.deleg = deleg;
		}

		// Token: 0x06002ADC RID: 10972 RVA: 0x0010464A File Offset: 0x0010364A
		public string GetResultantText()
		{
			return this.deleg.GetResultantText();
		}

		// Token: 0x04001DA4 RID: 7588
		private ITextExtractionStrategy deleg;
	}
}
