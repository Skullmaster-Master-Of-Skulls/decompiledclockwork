using System;
using System.IO;
using iTextSharp.text.xml.simpleparser;

namespace iTextSharp.text.pdf.parser
{
	// Token: 0x0200039F RID: 927
	public class TaggedPdfReaderTool
	{
		// Token: 0x0600200E RID: 8206 RVA: 0x000BF418 File Offset: 0x000BE418
		public void ConvertToXml(PdfReader reader, Stream os)
		{
			this.reader = reader;
			this.outp = new StreamWriter(os);
			PdfDictionary catalog = reader.Catalog;
			PdfDictionary asDict = catalog.GetAsDict(PdfName.STRUCTTREEROOT);
			this.InspectChild(asDict.GetDirectObject(PdfName.K));
			this.outp.Flush();
			this.outp.Close();
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x000BF472 File Offset: 0x000BE472
		public void InspectChild(PdfObject k)
		{
			if (k == null)
			{
				return;
			}
			if (k is PdfArray)
			{
				this.InspectChildArray((PdfArray)k);
				return;
			}
			if (k is PdfDictionary)
			{
				this.InspectChildDictionary((PdfDictionary)k);
			}
		}

		// Token: 0x06002010 RID: 8208 RVA: 0x000BF4A4 File Offset: 0x000BE4A4
		public void InspectChildArray(PdfArray k)
		{
			if (k == null)
			{
				return;
			}
			for (int i = 0; i < k.Size; i++)
			{
				this.InspectChild(k.GetDirectObject(i));
			}
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x000BF4D4 File Offset: 0x000BE4D4
		public void InspectChildDictionary(PdfDictionary k)
		{
			if (k == null)
			{
				return;
			}
			PdfName asName = k.GetAsName(PdfName.S);
			if (asName != null)
			{
				string text = asName.ToString().Substring(1);
				this.outp.Write("<");
				this.outp.Write(text);
				this.outp.Write(">");
				PdfDictionary asDict = k.GetAsDict(PdfName.PG);
				if (asDict != null)
				{
					this.ParseTag(text, k.GetDirectObject(PdfName.K), asDict);
				}
				this.InspectChild(k.Get(PdfName.K));
				this.outp.Write("</");
				this.outp.Write(text);
				this.outp.WriteLine(">");
				return;
			}
			this.InspectChild(k.Get(PdfName.K));
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x000BF5A4 File Offset: 0x000BE5A4
		public void ParseTag(string tag, PdfObject obj, PdfDictionary page)
		{
			PRStream stream = (PRStream)page.GetAsStream(PdfName.CONTENTS);
			if (obj is PdfNumber)
			{
				PdfNumber pdfNumber = (PdfNumber)obj;
				RenderFilter renderFilter = new MarkedContentRenderFilter(pdfNumber.IntValue);
				ITextExtractionStrategy deleg = new SimpleTextExtractionStrategy();
				FilteredTextRenderListener filteredTextRenderListener = new FilteredTextRenderListener(deleg, new RenderFilter[]
				{
					renderFilter
				});
				PdfContentStreamProcessor pdfContentStreamProcessor = new PdfContentStreamProcessor(filteredTextRenderListener);
				pdfContentStreamProcessor.ProcessContent(PdfReader.GetStreamBytes(stream), page.GetAsDict(PdfName.RESOURCES));
				this.outp.Write(SimpleXMLParser.EscapeXML(filteredTextRenderListener.GetResultantText(), true));
				return;
			}
			if (obj is PdfArray)
			{
				PdfArray pdfArray = (PdfArray)obj;
				int size = pdfArray.Size;
				for (int i = 0; i < size; i++)
				{
					this.ParseTag(tag, pdfArray[i], page);
					if (i < size - 1)
					{
						this.outp.WriteLine();
					}
				}
				return;
			}
			if (obj is PdfDictionary)
			{
				PdfDictionary pdfDictionary = (PdfDictionary)obj;
				this.ParseTag(tag, pdfDictionary.GetDirectObject(PdfName.MCID), pdfDictionary.GetAsDict(PdfName.PG));
			}
		}

		// Token: 0x0400161A RID: 5658
		private PdfReader reader;

		// Token: 0x0400161B RID: 5659
		private StreamWriter outp;
	}
}
