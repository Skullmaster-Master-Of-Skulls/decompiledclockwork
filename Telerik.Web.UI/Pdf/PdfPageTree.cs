using System;

namespace Telerik.Pdf
{
	// Token: 0x0200166B RID: 5739
	public sealed class PdfPageTree : PdfDictionary
	{
		// Token: 0x0600DE15 RID: 56853 RVA: 0x00308A30 File Offset: 0x00306C30
		public PdfPageTree(PdfObjectId objectId) : base(objectId)
		{
			base[PdfName.Names.Type] = PdfName.Names.Pages;
			this.kids = new PdfArray();
			base[PdfName.Names.Kids] = this.kids;
		}

		// Token: 0x170043F1 RID: 17393
		// (get) Token: 0x0600DE16 RID: 56854 RVA: 0x00308A65 File Offset: 0x00306C65
		public PdfArray Kids
		{
			get
			{
				return this.kids;
			}
		}

		// Token: 0x0600DE17 RID: 56855 RVA: 0x00308A70 File Offset: 0x00306C70
		protected internal override void Write(PdfWriter writer)
		{
			int num = 0;
			for (int i = 0; i < this.kids.Count; i++)
			{
				num++;
			}
			base[PdfName.Names.Count] = new PdfNumeric(num);
			base.Write(writer);
		}

		// Token: 0x04003FD4 RID: 16340
		private PdfArray kids;
	}
}
