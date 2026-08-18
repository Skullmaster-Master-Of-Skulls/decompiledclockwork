using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000161 RID: 353
	public class PdfDeveloperExtension
	{
		// Token: 0x06000D57 RID: 3415 RVA: 0x000499D5 File Offset: 0x000489D5
		public PdfDeveloperExtension(PdfName prefix, PdfName baseversion, int extensionLevel)
		{
			this.prefix = prefix;
			this.baseversion = baseversion;
			this.extensionLevel = extensionLevel;
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x000499F2 File Offset: 0x000489F2
		public PdfName Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000D59 RID: 3417 RVA: 0x000499FA File Offset: 0x000489FA
		public PdfName Baseversion
		{
			get
			{
				return this.baseversion;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x00049A02 File Offset: 0x00048A02
		public int ExtensionLevel
		{
			get
			{
				return this.extensionLevel;
			}
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x00049A0C File Offset: 0x00048A0C
		public PdfDictionary GetDeveloperExtensions()
		{
			PdfDictionary pdfDictionary = new PdfDictionary();
			pdfDictionary.Put(PdfName.BASEVERSION, this.baseversion);
			pdfDictionary.Put(PdfName.EXTENSIONLEVEL, new PdfNumber(this.extensionLevel));
			return pdfDictionary;
		}

		// Token: 0x040009F0 RID: 2544
		public static readonly PdfDeveloperExtension ADOBE_1_7_EXTENSIONLEVEL3 = new PdfDeveloperExtension(PdfName.ADBE, PdfWriter.PDF_VERSION_1_7, 3);

		// Token: 0x040009F1 RID: 2545
		protected PdfName prefix;

		// Token: 0x040009F2 RID: 2546
		protected PdfName baseversion;

		// Token: 0x040009F3 RID: 2547
		protected int extensionLevel;
	}
}
