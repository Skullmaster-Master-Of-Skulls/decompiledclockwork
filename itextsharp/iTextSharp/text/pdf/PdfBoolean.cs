using System;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200058E RID: 1422
	public class PdfBoolean : PdfObject
	{
		// Token: 0x06003084 RID: 12420 RVA: 0x0012BCF2 File Offset: 0x0012ACF2
		public PdfBoolean(bool value) : base(1)
		{
			if (value)
			{
				base.Content = "true";
			}
			else
			{
				base.Content = "false";
			}
			this.value = value;
		}

		// Token: 0x06003085 RID: 12421 RVA: 0x0012BD20 File Offset: 0x0012AD20
		public PdfBoolean(string value) : base(1, value)
		{
			if (value.Equals("true"))
			{
				this.value = true;
				return;
			}
			if (value.Equals("false"))
			{
				this.value = false;
				return;
			}
			throw new BadPdfFormatException(MessageLocalization.GetComposedMessage("the.value.has.to.be.true.of.false.instead.of.1", value));
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06003086 RID: 12422 RVA: 0x0012BD6F File Offset: 0x0012AD6F
		public bool BooleanValue
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06003087 RID: 12423 RVA: 0x0012BD77 File Offset: 0x0012AD77
		public override string ToString()
		{
			if (!this.value)
			{
				return "false";
			}
			return "true";
		}

		// Token: 0x04002161 RID: 8545
		public const string TRUE = "true";

		// Token: 0x04002162 RID: 8546
		public const string FALSE = "false";

		// Token: 0x04002163 RID: 8547
		public static readonly PdfBoolean PDFTRUE = new PdfBoolean(true);

		// Token: 0x04002164 RID: 8548
		public static readonly PdfBoolean PDFFALSE = new PdfBoolean(false);

		// Token: 0x04002165 RID: 8549
		private bool value;
	}
}
