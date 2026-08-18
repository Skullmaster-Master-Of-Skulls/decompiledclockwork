using System;

namespace Telerik.Pdf
{
	// Token: 0x02001665 RID: 5733
	public sealed class PdfNull : PdfObject
	{
		// Token: 0x0600DE00 RID: 56832 RVA: 0x00308655 File Offset: 0x00306855
		private PdfNull()
		{
		}

		// Token: 0x0600DE01 RID: 56833 RVA: 0x0030865D File Offset: 0x0030685D
		public PdfNull(PdfObjectId objectId) : base(objectId)
		{
		}

		// Token: 0x0600DE02 RID: 56834 RVA: 0x00308666 File Offset: 0x00306866
		protected internal override void Write(PdfWriter writer)
		{
			writer.WriteKeyword(Keyword.Null);
		}

		// Token: 0x04003FC6 RID: 16326
		public static readonly PdfNull Null = new PdfNull();
	}
}
