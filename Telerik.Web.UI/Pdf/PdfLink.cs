using System;
using System.Drawing;

namespace Telerik.Pdf
{
	// Token: 0x02001662 RID: 5730
	public sealed class PdfLink : PdfDictionary
	{
		// Token: 0x0600DDF2 RID: 56818 RVA: 0x00307B2C File Offset: 0x00305D2C
		static PdfLink()
		{
			PdfLink.DefaultColor.Add(new PdfNumeric(0m));
			PdfLink.DefaultColor.Add(new PdfNumeric(0m));
			PdfLink.DefaultColor.Add(new PdfNumeric(0m));
			PdfLink.DefaultBorder = new PdfArray();
			PdfLink.DefaultBorder.Add(new PdfNumeric(0m));
			PdfLink.DefaultBorder.Add(new PdfNumeric(0m));
			PdfLink.DefaultBorder.Add(new PdfNumeric(0m));
		}

		// Token: 0x0600DDF3 RID: 56819 RVA: 0x00307BD4 File Offset: 0x00305DD4
		public PdfLink(PdfObjectId objectId, Rectangle r) : base(objectId)
		{
			base[PdfName.Names.Type] = PdfName.Names.Annot;
			base[PdfName.Names.Subtype] = PdfName.Names.Link;
			PdfArray pdfArray = new PdfArray();
			pdfArray.Add(new PdfNumeric(r.X / 1000m));
			pdfArray.Add(new PdfNumeric(r.Y / 1000m));
			pdfArray.Add(new PdfNumeric((r.X + r.Width) / 1000m));
			pdfArray.Add(new PdfNumeric((r.Y - r.Height) / 1000m));
			base[PdfName.Names.Rect] = pdfArray;
			base[PdfName.Names.H] = PdfName.Names.I;
			base[PdfName.Names.C] = PdfLink.DefaultColor;
			base[PdfName.Names.Border] = PdfLink.DefaultBorder;
		}

		// Token: 0x0600DDF4 RID: 56820 RVA: 0x00307CF6 File Offset: 0x00305EF6
		public void SetAction(IPdfAction action)
		{
			this.action = action;
		}

		// Token: 0x0600DDF5 RID: 56821 RVA: 0x00307CFF File Offset: 0x00305EFF
		protected internal override void Write(PdfWriter writer)
		{
			base[PdfName.Names.A] = this.action.GetAction();
			base.Write(writer);
		}

		// Token: 0x04003F3C RID: 16188
		private static readonly PdfArray DefaultColor = new PdfArray();

		// Token: 0x04003F3D RID: 16189
		private static readonly PdfArray DefaultBorder;

		// Token: 0x04003F3E RID: 16190
		private IPdfAction action;
	}
}
