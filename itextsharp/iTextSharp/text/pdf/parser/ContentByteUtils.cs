using System;
using System.IO;
using System.util;

namespace iTextSharp.text.pdf.parser
{
	// Token: 0x02000283 RID: 643
	public class ContentByteUtils
	{
		// Token: 0x0600184F RID: 6223 RVA: 0x0008C9B6 File Offset: 0x0008B9B6
		private ContentByteUtils()
		{
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x0008C9C0 File Offset: 0x0008B9C0
		public static byte[] GetContentBytesFromContentObject(PdfObject contentObject)
		{
			int type = contentObject.Type;
			switch (type)
			{
			case 5:
			{
				MemoryStream memoryStream = new MemoryStream();
				PdfArray pdfArray = (PdfArray)contentObject;
				ListIterator<PdfObject> listIterator = pdfArray.GetListIterator();
				while (listIterator.HasNext())
				{
					PdfObject contentObject2 = listIterator.Next();
					byte[] contentBytesFromContentObject;
					memoryStream.Write(contentBytesFromContentObject = ContentByteUtils.GetContentBytesFromContentObject(contentObject2), 0, contentBytesFromContentObject.Length);
				}
				return memoryStream.ToArray();
			}
			case 6:
				break;
			case 7:
			{
				PRStream stream = (PRStream)PdfReader.GetPdfObject(contentObject);
				return PdfReader.GetStreamBytes(stream);
			}
			default:
				if (type == 10)
				{
					PRIndirectReference obj = (PRIndirectReference)contentObject;
					PdfObject pdfObject = PdfReader.GetPdfObject(obj);
					return ContentByteUtils.GetContentBytesFromContentObject(pdfObject);
				}
				break;
			}
			string message = "Unable to handle Content of type " + contentObject.GetType();
			throw new InvalidOperationException(message);
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x0008CA84 File Offset: 0x0008BA84
		public static byte[] GetContentBytesForPage(PdfReader reader, int pageNum)
		{
			PdfDictionary pageN = reader.GetPageN(pageNum);
			PdfObject pdfObject = pageN.Get(PdfName.CONTENTS);
			if (pdfObject == null)
			{
				return new byte[0];
			}
			return ContentByteUtils.GetContentBytesFromContentObject(pdfObject);
		}
	}
}
