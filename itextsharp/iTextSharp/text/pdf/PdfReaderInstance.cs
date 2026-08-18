using System;
using System.Collections.Generic;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x020004DF RID: 1247
	public class PdfReaderInstance
	{
		// Token: 0x06002A89 RID: 10889 RVA: 0x001035D4 File Offset: 0x001025D4
		internal PdfReaderInstance(PdfReader reader, PdfWriter writer)
		{
			this.reader = reader;
			this.writer = writer;
			this.file = reader.SafeFile;
			this.myXref = new int[reader.XrefSize];
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06002A8A RID: 10890 RVA: 0x00103633 File Offset: 0x00102633
		internal PdfReader Reader
		{
			get
			{
				return this.reader;
			}
		}

		// Token: 0x06002A8B RID: 10891 RVA: 0x0010363C File Offset: 0x0010263C
		internal PdfImportedPage GetImportedPage(int pageNumber)
		{
			if (!this.reader.IsOpenedWithFullPermissions)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("pdfreader.not.opened.with.owner.password"));
			}
			if (pageNumber < 1 || pageNumber > this.reader.NumberOfPages)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("invalid.page.number.1", pageNumber));
			}
			PdfImportedPage pdfImportedPage;
			if (!this.importedPages.TryGetValue(pageNumber, out pdfImportedPage))
			{
				pdfImportedPage = new PdfImportedPage(this, this.writer, pageNumber);
				this.importedPages[pageNumber] = pdfImportedPage;
			}
			return pdfImportedPage;
		}

		// Token: 0x06002A8C RID: 10892 RVA: 0x001036BA File Offset: 0x001026BA
		internal int GetNewObjectNumber(int number, int generation)
		{
			if (this.myXref[number] == 0)
			{
				this.myXref[number] = this.writer.IndirectReferenceNumber;
				this.nextRound.Add(number);
			}
			return this.myXref[number];
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06002A8D RID: 10893 RVA: 0x001036ED File Offset: 0x001026ED
		internal RandomAccessFileOrArray ReaderFile
		{
			get
			{
				return this.file;
			}
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x001036F8 File Offset: 0x001026F8
		internal PdfObject GetResources(int pageNumber)
		{
			return PdfReader.GetPdfObjectRelease(this.reader.GetPageNRelease(pageNumber).Get(PdfName.RESOURCES));
		}

		// Token: 0x06002A8F RID: 10895 RVA: 0x00103724 File Offset: 0x00102724
		internal PdfStream GetFormXObject(int pageNumber, int compressionLevel)
		{
			PdfDictionary pageNRelease = this.reader.GetPageNRelease(pageNumber);
			PdfObject pdfObjectRelease = PdfReader.GetPdfObjectRelease(pageNRelease.Get(PdfName.CONTENTS));
			PdfDictionary pdfDictionary = new PdfDictionary();
			byte[] array = null;
			if (pdfObjectRelease != null)
			{
				if (pdfObjectRelease.IsStream())
				{
					pdfDictionary.Merge((PRStream)pdfObjectRelease);
				}
				else
				{
					array = this.reader.GetPageContent(pageNumber, this.file);
				}
			}
			else
			{
				array = new byte[0];
			}
			pdfDictionary.Put(PdfName.RESOURCES, PdfReader.GetPdfObjectRelease(pageNRelease.Get(PdfName.RESOURCES)));
			pdfDictionary.Put(PdfName.TYPE, PdfName.XOBJECT);
			pdfDictionary.Put(PdfName.SUBTYPE, PdfName.FORM);
			PdfImportedPage pdfImportedPage = this.importedPages[pageNumber];
			pdfDictionary.Put(PdfName.BBOX, new PdfRectangle(pdfImportedPage.BoundingBox));
			PdfArray matrix = pdfImportedPage.Matrix;
			if (matrix == null)
			{
				pdfDictionary.Put(PdfName.MATRIX, PdfReaderInstance.IDENTITYMATRIX);
			}
			else
			{
				pdfDictionary.Put(PdfName.MATRIX, matrix);
			}
			pdfDictionary.Put(PdfName.FORMTYPE, PdfReaderInstance.ONE);
			PRStream prstream;
			if (array == null)
			{
				prstream = new PRStream((PRStream)pdfObjectRelease, pdfDictionary);
			}
			else
			{
				prstream = new PRStream(this.reader, array);
				prstream.Merge(pdfDictionary);
			}
			return prstream;
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x00103854 File Offset: 0x00102854
		internal void WriteAllVisited()
		{
			while (this.nextRound.Count > 0)
			{
				List<int> list = this.nextRound;
				this.nextRound = new List<int>();
				foreach (int num in list)
				{
					if (!this.visited.ContainsKey(num))
					{
						this.visited[num] = null;
						this.writer.AddToBody(this.reader.GetPdfObjectRelease(num), this.myXref[num]);
					}
				}
			}
		}

		// Token: 0x06002A91 RID: 10897 RVA: 0x001038FC File Offset: 0x001028FC
		internal void WriteAllPages()
		{
			try
			{
				this.file.ReOpen();
				foreach (PdfImportedPage pdfImportedPage in this.importedPages.Values)
				{
					this.writer.AddToBody(pdfImportedPage.GetFormXObject(this.writer.CompressionLevel), pdfImportedPage.IndirectReference);
				}
				this.WriteAllVisited();
			}
			finally
			{
				try
				{
					this.reader.Close();
					this.file.Close();
				}
				catch
				{
				}
			}
		}

		// Token: 0x04001D8F RID: 7567
		internal static PdfLiteral IDENTITYMATRIX = new PdfLiteral("[1 0 0 1 0 0]");

		// Token: 0x04001D90 RID: 7568
		internal static PdfNumber ONE = new PdfNumber(1);

		// Token: 0x04001D91 RID: 7569
		internal int[] myXref;

		// Token: 0x04001D92 RID: 7570
		internal PdfReader reader;

		// Token: 0x04001D93 RID: 7571
		internal RandomAccessFileOrArray file;

		// Token: 0x04001D94 RID: 7572
		internal Dictionary<int, PdfImportedPage> importedPages = new Dictionary<int, PdfImportedPage>();

		// Token: 0x04001D95 RID: 7573
		internal PdfWriter writer;

		// Token: 0x04001D96 RID: 7574
		internal Dictionary<int, object> visited = new Dictionary<int, object>();

		// Token: 0x04001D97 RID: 7575
		internal List<int> nextRound = new List<int>();
	}
}
