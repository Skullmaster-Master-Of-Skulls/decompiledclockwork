using System;
using System.IO;
using System.Net;
using iTextSharp.text.error_messages;
using iTextSharp.text.pdf.collection;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200015F RID: 351
	public class PdfFileSpecification : PdfDictionary
	{
		// Token: 0x06000D2A RID: 3370 RVA: 0x000486E4 File Offset: 0x000476E4
		public PdfFileSpecification() : base(PdfName.FILESPEC)
		{
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x000486F4 File Offset: 0x000476F4
		public static PdfFileSpecification Url(PdfWriter writer, string url)
		{
			PdfFileSpecification pdfFileSpecification = new PdfFileSpecification();
			pdfFileSpecification.writer = writer;
			pdfFileSpecification.Put(PdfName.FS, PdfName.URL);
			pdfFileSpecification.Put(PdfName.F, new PdfString(url));
			return pdfFileSpecification;
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x00048730 File Offset: 0x00047730
		public static PdfFileSpecification FileEmbedded(PdfWriter writer, string filePath, string fileDisplay, byte[] fileStore)
		{
			return PdfFileSpecification.FileEmbedded(writer, filePath, fileDisplay, fileStore, 9);
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0004873D File Offset: 0x0004773D
		public static PdfFileSpecification FileEmbedded(PdfWriter writer, string filePath, string fileDisplay, byte[] fileStore, int compressionLevel)
		{
			return PdfFileSpecification.FileEmbedded(writer, filePath, fileDisplay, fileStore, null, null, compressionLevel);
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x0004874C File Offset: 0x0004774C
		public static PdfFileSpecification FileEmbedded(PdfWriter writer, string filePath, string fileDisplay, byte[] fileStore, bool compress)
		{
			return PdfFileSpecification.FileEmbedded(writer, filePath, fileDisplay, fileStore, null, null, compress ? 9 : 0);
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x00048762 File Offset: 0x00047762
		public static PdfFileSpecification FileEmbedded(PdfWriter writer, string filePath, string fileDisplay, byte[] fileStore, bool compress, string mimeType, PdfDictionary fileParameter)
		{
			return PdfFileSpecification.FileEmbedded(writer, filePath, fileDisplay, fileStore, mimeType, fileParameter, compress ? 9 : 0);
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x0004877C File Offset: 0x0004777C
		public static PdfFileSpecification FileEmbedded(PdfWriter writer, string filePath, string fileDisplay, byte[] fileStore, string mimeType, PdfDictionary fileParameter, int compressionLevel)
		{
			PdfFileSpecification pdfFileSpecification = new PdfFileSpecification();
			pdfFileSpecification.writer = writer;
			pdfFileSpecification.Put(PdfName.F, new PdfString(fileDisplay));
			Stream stream = null;
			PdfIndirectReference pdfIndirectReference = null;
			PdfIndirectReference indirectReference;
			try
			{
				PdfEFStream pdfEFStream;
				if (fileStore == null)
				{
					pdfIndirectReference = writer.PdfIndirectReference;
					if (File.Exists(filePath))
					{
						stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
					}
					else if (filePath.StartsWith("file:/") || filePath.StartsWith("http://") || filePath.StartsWith("https://"))
					{
						WebRequest webRequest = WebRequest.Create(filePath);
						stream = webRequest.GetResponse().GetResponseStream();
					}
					else
					{
						stream = BaseFont.GetResourceStream(filePath);
						if (stream == null)
						{
							throw new IOException(MessageLocalization.GetComposedMessage("1.not.found.as.file.or.resource", filePath));
						}
					}
					pdfEFStream = new PdfEFStream(stream, writer);
				}
				else
				{
					pdfEFStream = new PdfEFStream(fileStore);
				}
				pdfEFStream.Put(PdfName.TYPE, PdfName.EMBEDDEDFILE);
				pdfEFStream.FlateCompress(compressionLevel);
				PdfDictionary pdfDictionary = new PdfDictionary();
				if (fileParameter != null)
				{
					pdfDictionary.Merge(fileParameter);
				}
				if (fileStore != null)
				{
					pdfDictionary.Put(PdfName.SIZE, new PdfNumber(pdfEFStream.RawLength));
					pdfEFStream.Put(PdfName.PARAMS, pdfDictionary);
				}
				else
				{
					pdfEFStream.Put(PdfName.PARAMS, pdfIndirectReference);
				}
				if (mimeType != null)
				{
					pdfEFStream.Put(PdfName.SUBTYPE, new PdfName(mimeType));
				}
				indirectReference = writer.AddToBody(pdfEFStream).IndirectReference;
				if (fileStore == null)
				{
					pdfEFStream.WriteLength();
					pdfDictionary.Put(PdfName.SIZE, new PdfNumber(pdfEFStream.RawLength));
					writer.AddToBody(pdfDictionary, pdfIndirectReference);
				}
			}
			finally
			{
				if (stream != null)
				{
					try
					{
						stream.Close();
					}
					catch
					{
					}
				}
			}
			PdfDictionary pdfDictionary2 = new PdfDictionary();
			pdfDictionary2.Put(PdfName.F, indirectReference);
			pdfFileSpecification.Put(PdfName.EF, pdfDictionary2);
			return pdfFileSpecification;
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x0004894C File Offset: 0x0004794C
		public static PdfFileSpecification FileExtern(PdfWriter writer, string filePath)
		{
			PdfFileSpecification pdfFileSpecification = new PdfFileSpecification();
			pdfFileSpecification.writer = writer;
			pdfFileSpecification.Put(PdfName.F, new PdfString(filePath));
			return pdfFileSpecification;
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x00048978 File Offset: 0x00047978
		public PdfIndirectReference Reference
		{
			get
			{
				if (this.refi != null)
				{
					return this.refi;
				}
				this.refi = this.writer.AddToBody(this).IndirectReference;
				return this.refi;
			}
		}

		// Token: 0x17000291 RID: 657
		// (set) Token: 0x06000D33 RID: 3379 RVA: 0x000489A6 File Offset: 0x000479A6
		public byte[] MultiByteFileName
		{
			set
			{
				base.Put(PdfName.F, new PdfString(value).SetHexWriting(true));
			}
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x000489BF File Offset: 0x000479BF
		public void SetUnicodeFileName(string filename, bool unicode)
		{
			base.Put(PdfName.UF, new PdfString(filename, unicode ? "UnicodeBig" : "PDF"));
		}

		// Token: 0x17000292 RID: 658
		// (set) Token: 0x06000D35 RID: 3381 RVA: 0x000489E1 File Offset: 0x000479E1
		public bool Volatile
		{
			set
			{
				base.Put(PdfName.V, new PdfBoolean(value));
			}
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x000489F4 File Offset: 0x000479F4
		public void AddDescription(string description, bool unicode)
		{
			base.Put(PdfName.DESC, new PdfString(description, unicode ? "UnicodeBig" : "PDF"));
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x00048A16 File Offset: 0x00047A16
		public void AddCollectionItem(PdfCollectionItem ci)
		{
			base.Put(PdfName.CI, ci);
		}

		// Token: 0x040009D7 RID: 2519
		protected PdfWriter writer;

		// Token: 0x040009D8 RID: 2520
		protected PdfIndirectReference refi;
	}
}
