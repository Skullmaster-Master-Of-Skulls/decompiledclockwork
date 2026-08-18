using System;

namespace Telerik.Pdf
{
	// Token: 0x02001668 RID: 5736
	public sealed class PdfObjectReference : PdfObject
	{
		// Token: 0x0600DE0B RID: 56843 RVA: 0x003086D9 File Offset: 0x003068D9
		public PdfObjectReference(PdfObject obj)
		{
			this.refId = obj.ObjectId;
		}

		// Token: 0x0600DE0C RID: 56844 RVA: 0x003086ED File Offset: 0x003068ED
		protected internal override void Write(PdfWriter writer)
		{
			writer.Write(this.refId.ObjectNumber);
			writer.WriteSpace();
			writer.Write(this.refId.GenerationNumber);
			writer.WriteSpace();
			writer.WriteKeyword(Keyword.R);
		}

		// Token: 0x04003FCA RID: 16330
		private PdfObjectId refId;
	}
}
