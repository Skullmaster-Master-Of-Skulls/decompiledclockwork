using System;

namespace Telerik.Pdf
{
	// Token: 0x0200165B RID: 5723
	public class PdfGoToRemote : PdfDictionary, IPdfAction
	{
		// Token: 0x170043E1 RID: 17377
		// (get) Token: 0x0600DDBF RID: 56767 RVA: 0x0030735F File Offset: 0x0030555F
		// (set) Token: 0x0600DDC0 RID: 56768 RVA: 0x00307367 File Offset: 0x00305567
		protected PdfFileSpec fileSpec { get; set; }

		// Token: 0x0600DDC1 RID: 56769 RVA: 0x00307370 File Offset: 0x00305570
		static PdfGoToRemote()
		{
			PdfGoToRemote.DefaultDestination.Add(new PdfNumeric(0m));
			PdfGoToRemote.DefaultDestination.Add(PdfName.Names.XYZ);
			PdfGoToRemote.DefaultDestination.Add(PdfNull.Null);
			PdfGoToRemote.DefaultDestination.Add(PdfNull.Null);
			PdfGoToRemote.DefaultDestination.Add(PdfNull.Null);
		}

		// Token: 0x0600DDC2 RID: 56770 RVA: 0x003073E0 File Offset: 0x003055E0
		public PdfGoToRemote(PdfFileSpec fileSpec, PdfObjectId objectId) : base(objectId)
		{
			this.fileSpec = fileSpec;
			base[PdfName.Names.Type] = PdfName.Names.Action;
			base[PdfName.Names.S] = PdfName.Names.GoToR;
			base[PdfName.Names.F] = fileSpec.GetReference();
			base[PdfName.Names.D] = PdfGoToRemote.DefaultDestination;
		}

		// Token: 0x0600DDC3 RID: 56771 RVA: 0x0030743C File Offset: 0x0030563C
		public PdfObject GetAction()
		{
			return base.GetReference();
		}

		// Token: 0x04003F1E RID: 16158
		private static readonly PdfArray DefaultDestination = new PdfArray();
	}
}
