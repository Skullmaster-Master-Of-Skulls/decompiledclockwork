using System;

namespace Telerik.Pdf
{
	// Token: 0x0200165A RID: 5722
	public sealed class PdfGoTo : PdfDictionary, IPdfAction
	{
		// Token: 0x0600DDB9 RID: 56761 RVA: 0x00307274 File Offset: 0x00305474
		public PdfGoTo(PdfObjectReference pageReference, PdfObjectId objectId) : base(objectId)
		{
			base[PdfName.Names.Type] = PdfName.Names.Action;
			base[PdfName.Names.S] = PdfName.Names.GoTo;
			this.pageReference = pageReference;
		}

		// Token: 0x170043DE RID: 17374
		// (set) Token: 0x0600DDBA RID: 56762 RVA: 0x003072A4 File Offset: 0x003054A4
		public PdfObjectReference PageReference
		{
			set
			{
				this.pageReference = value;
			}
		}

		// Token: 0x170043DF RID: 17375
		// (set) Token: 0x0600DDBB RID: 56763 RVA: 0x003072AD File Offset: 0x003054AD
		public int X
		{
			set
			{
				this.xPosition = value / 1000m;
			}
		}

		// Token: 0x170043E0 RID: 17376
		// (set) Token: 0x0600DDBC RID: 56764 RVA: 0x003072CA File Offset: 0x003054CA
		public int Y
		{
			set
			{
				this.yPosition = value / 1000m;
			}
		}

		// Token: 0x0600DDBD RID: 56765 RVA: 0x003072E7 File Offset: 0x003054E7
		public PdfObject GetAction()
		{
			return base.GetReference();
		}

		// Token: 0x0600DDBE RID: 56766 RVA: 0x003072F0 File Offset: 0x003054F0
		protected internal override void Write(PdfWriter writer)
		{
			PdfArray pdfArray = new PdfArray();
			pdfArray.Add(this.pageReference);
			pdfArray.Add(PdfName.Names.XYZ);
			pdfArray.Add(new PdfNumeric(this.xPosition));
			pdfArray.Add(new PdfNumeric(this.yPosition));
			pdfArray.Add(PdfNull.Null);
			base[PdfName.Names.D] = pdfArray;
			base.Write(writer);
		}

		// Token: 0x04003F1B RID: 16155
		private PdfObjectReference pageReference;

		// Token: 0x04003F1C RID: 16156
		private decimal xPosition;

		// Token: 0x04003F1D RID: 16157
		private decimal yPosition;
	}
}
