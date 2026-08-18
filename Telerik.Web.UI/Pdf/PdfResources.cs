using System;

namespace Telerik.Pdf
{
	// Token: 0x0200166C RID: 5740
	public class PdfResources : PdfDictionary
	{
		// Token: 0x0600DE18 RID: 56856 RVA: 0x00308AB8 File Offset: 0x00306CB8
		static PdfResources()
		{
			PdfResources.DefaultProcedureSets.Add(PdfName.Names.PDF);
			PdfResources.DefaultProcedureSets.Add(PdfName.Names.Text);
			PdfResources.DefaultProcedureSets.Add(PdfName.Names.ImageB);
			PdfResources.DefaultProcedureSets.Add(PdfName.Names.ImageC);
			PdfResources.DefaultProcedureSets.Add(PdfName.Names.ImageI);
		}

		// Token: 0x0600DE19 RID: 56857 RVA: 0x00308B1F File Offset: 0x00306D1F
		public PdfResources(PdfObjectId objectId) : base(objectId)
		{
			base[PdfName.Names.ProcSet] = PdfResources.DefaultProcedureSets;
		}

		// Token: 0x0600DE1A RID: 56858 RVA: 0x00308B4E File Offset: 0x00306D4E
		public void AddFont(PdfFont font)
		{
			this.fonts.Add(font.Name, font.GetReference());
		}

		// Token: 0x0600DE1B RID: 56859 RVA: 0x00308B67 File Offset: 0x00306D67
		public void AddXObject(PdfXObject xObject)
		{
			this.xObjects.Add(xObject.Name, xObject.GetReference());
		}

		// Token: 0x0600DE1C RID: 56860 RVA: 0x00308B80 File Offset: 0x00306D80
		protected internal override void Write(PdfWriter writer)
		{
			if (this.fonts.Count > 0)
			{
				base[PdfName.Names.Font] = this.fonts;
			}
			if (this.xObjects.Count > 0)
			{
				base[PdfName.Names.XObject] = this.xObjects;
			}
			base.Write(writer);
		}

		// Token: 0x04003FD5 RID: 16341
		private static readonly PdfArray DefaultProcedureSets = new PdfArray();

		// Token: 0x04003FD6 RID: 16342
		private PdfDictionary fonts = new PdfDictionary();

		// Token: 0x04003FD7 RID: 16343
		private PdfDictionary xObjects = new PdfDictionary();
	}
}
