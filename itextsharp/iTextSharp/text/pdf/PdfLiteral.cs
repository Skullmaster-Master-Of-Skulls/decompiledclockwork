using System;
using System.IO;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200058C RID: 1420
	public class PdfLiteral : PdfObject
	{
		// Token: 0x06003059 RID: 12377 RVA: 0x0012B61C File Offset: 0x0012A61C
		public PdfLiteral(string text) : base(0, text)
		{
		}

		// Token: 0x0600305A RID: 12378 RVA: 0x0012B626 File Offset: 0x0012A626
		public PdfLiteral(byte[] b) : base(0, b)
		{
		}

		// Token: 0x0600305B RID: 12379 RVA: 0x0012B630 File Offset: 0x0012A630
		public PdfLiteral(int type, string text) : base(type, text)
		{
		}

		// Token: 0x0600305C RID: 12380 RVA: 0x0012B63A File Offset: 0x0012A63A
		public PdfLiteral(int type, byte[] b) : base(type, b)
		{
		}

		// Token: 0x0600305D RID: 12381 RVA: 0x0012B644 File Offset: 0x0012A644
		public PdfLiteral(int size) : base(0, null)
		{
			this.bytes = new byte[size];
			for (int i = 0; i < size; i++)
			{
				this.bytes[i] = 32;
			}
		}

		// Token: 0x0600305E RID: 12382 RVA: 0x0012B67B File Offset: 0x0012A67B
		public override void ToPdf(PdfWriter writer, Stream os)
		{
			if (os is OutputStreamCounter)
			{
				this.position = ((OutputStreamCounter)os).Counter;
			}
			base.ToPdf(writer, os);
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x0600305F RID: 12383 RVA: 0x0012B69E File Offset: 0x0012A69E
		public int Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06003060 RID: 12384 RVA: 0x0012B6A6 File Offset: 0x0012A6A6
		public int PosLength
		{
			get
			{
				if (this.bytes != null)
				{
					return this.bytes.Length;
				}
				return 0;
			}
		}

		// Token: 0x04002139 RID: 8505
		private int position;
	}
}
