using System;

namespace Telerik.Pdf
{
	// Token: 0x020015FE RID: 5630
	public abstract class PdfObject
	{
		// Token: 0x0600DB80 RID: 56192 RVA: 0x0030044C File Offset: 0x002FE64C
		public PdfObject()
		{
		}

		// Token: 0x0600DB81 RID: 56193 RVA: 0x00300454 File Offset: 0x002FE654
		public PdfObject(PdfObjectId objectId)
		{
			this.objectId = objectId;
		}

		// Token: 0x0600DB82 RID: 56194
		protected internal abstract void Write(PdfWriter writer);

		// Token: 0x0600DB83 RID: 56195 RVA: 0x00300464 File Offset: 0x002FE664
		protected internal void WriteIndirect(PdfWriter writer)
		{
			writer.Write(this.objectId.ObjectNumber);
			writer.WriteSpace();
			writer.Write(this.objectId.GenerationNumber);
			writer.WriteSpace();
			writer.WriteKeywordLine(Keyword.Obj);
			this.Write(writer);
			writer.WriteLine();
			writer.WriteKeyword(Keyword.EndObj);
		}

		// Token: 0x0600DB84 RID: 56196 RVA: 0x003004BA File Offset: 0x002FE6BA
		public PdfObjectReference GetReference()
		{
			return new PdfObjectReference(this);
		}

		// Token: 0x17004331 RID: 17201
		// (get) Token: 0x0600DB85 RID: 56197 RVA: 0x003004C2 File Offset: 0x002FE6C2
		public bool IsIndirect
		{
			get
			{
				return this.objectId.ObjectNumber != 0;
			}
		}

		// Token: 0x17004332 RID: 17202
		// (get) Token: 0x0600DB86 RID: 56198 RVA: 0x003004D5 File Offset: 0x002FE6D5
		public PdfObjectId ObjectId
		{
			get
			{
				return this.objectId;
			}
		}

		// Token: 0x04003D63 RID: 15715
		private PdfObjectId objectId;
	}
}
