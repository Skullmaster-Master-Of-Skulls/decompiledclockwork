using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x020004DD RID: 1245
	public class StampContent : PdfContentByte
	{
		// Token: 0x06002A6B RID: 10859 RVA: 0x001031FA File Offset: 0x001021FA
		internal StampContent(PdfStamperImp stamper, PdfStamperImp.PageStamp ps) : base(stamper)
		{
			this.ps = ps;
			this.pageResources = ps.pageResources;
		}

		// Token: 0x06002A6C RID: 10860 RVA: 0x00103216 File Offset: 0x00102216
		public override void SetAction(PdfAction action, float llx, float lly, float urx, float ury)
		{
			((PdfStamperImp)this.writer).AddAnnotation(new PdfAnnotation(this.writer, llx, lly, urx, ury, action), this.ps.pageN);
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06002A6D RID: 10861 RVA: 0x00103245 File Offset: 0x00102245
		public override PdfContentByte Duplicate
		{
			get
			{
				return new StampContent((PdfStamperImp)this.writer, this.ps);
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06002A6E RID: 10862 RVA: 0x0010325D File Offset: 0x0010225D
		internal override PageResources PageResources
		{
			get
			{
				return this.pageResources;
			}
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x00103265 File Offset: 0x00102265
		internal override void AddAnnotation(PdfAnnotation annot)
		{
			((PdfStamperImp)this.writer).AddAnnotation(annot, this.ps.pageN);
		}

		// Token: 0x04001D82 RID: 7554
		internal PdfStamperImp.PageStamp ps;

		// Token: 0x04001D83 RID: 7555
		internal PageResources pageResources;
	}
}
