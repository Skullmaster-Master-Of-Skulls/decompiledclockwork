using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.util;

namespace iTextSharp.text.pdf.parser
{
	// Token: 0x02000328 RID: 808
	public class PdfContentReaderTool
	{
		// Token: 0x06001D4C RID: 7500 RVA: 0x000AFF0B File Offset: 0x000AEF0B
		public static string GetDictionaryDetail(PdfDictionary dic)
		{
			return PdfContentReaderTool.GetDictionaryDetail(dic, 0);
		}

		// Token: 0x06001D4D RID: 7501 RVA: 0x000AFF14 File Offset: 0x000AEF14
		public static string GetDictionaryDetail(PdfDictionary dic, int depth)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			IList<PdfName> list = new List<PdfName>();
			foreach (PdfName pdfName in dic.Keys)
			{
				PdfObject directObject = dic.GetDirectObject(pdfName);
				if (directObject.IsDictionary())
				{
					list.Add(pdfName);
				}
				stringBuilder.Append(pdfName);
				stringBuilder.Append('=');
				stringBuilder.Append(directObject);
				stringBuilder.Append(", ");
			}
			stringBuilder.Length -= 2;
			stringBuilder.Append(')');
			foreach (PdfName pdfName2 in list)
			{
				stringBuilder.Append('\n');
				for (int i = 0; i < depth + 1; i++)
				{
					stringBuilder.Append('\t');
				}
				stringBuilder.Append("Subdictionary ");
				stringBuilder.Append(pdfName2);
				stringBuilder.Append(" = ");
				stringBuilder.Append(PdfContentReaderTool.GetDictionaryDetail(dic.GetAsDict(pdfName2), depth + 1));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001D4E RID: 7502 RVA: 0x000B0064 File Offset: 0x000AF064
		public static string GetXObjectDetail(PdfDictionary resourceDic)
		{
			StringBuilder stringBuilder = new StringBuilder();
			PdfDictionary asDict = resourceDic.GetAsDict(PdfName.XOBJECT);
			if (asDict == null)
			{
				return "No XObjects";
			}
			foreach (PdfName pdfName in asDict.Keys)
			{
				PdfStream asStream = asDict.GetAsStream(pdfName);
				stringBuilder.Append(string.Concat(new object[]
				{
					"------ ",
					pdfName,
					" - subtype = ",
					asStream.Get(PdfName.SUBTYPE),
					" = ",
					asStream.GetAsNumber(PdfName.LENGTH),
					" bytes ------\n"
				}));
				if (!asStream.Get(PdfName.SUBTYPE).Equals(PdfName.IMAGE))
				{
					byte[] contentBytesFromContentObject = ContentByteUtils.GetContentBytesFromContentObject(asStream);
					foreach (byte value in contentBytesFromContentObject)
					{
						stringBuilder.Append((char)value);
					}
					stringBuilder.Append(string.Concat(new object[]
					{
						"------ ",
						pdfName,
						" - subtype = ",
						asStream.Get(PdfName.SUBTYPE),
						"End of Content------\n"
					}));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x000B01D4 File Offset: 0x000AF1D4
		public static void ListContentStreamForPage(PdfReader reader, int pageNum, TextWriter outp)
		{
			outp.WriteLine("==============Page " + pageNum + "====================");
			outp.WriteLine("- - - - - Dictionary - - - - - -");
			PdfDictionary pageN = reader.GetPageN(pageNum);
			outp.WriteLine(PdfContentReaderTool.GetDictionaryDetail(pageN));
			outp.WriteLine("- - - - - XObject Summary - - - - - -");
			outp.WriteLine(PdfContentReaderTool.GetXObjectDetail(pageN.GetAsDict(PdfName.RESOURCES)));
			outp.WriteLine("- - - - - Content Stream - - - - - -");
			RandomAccessFileOrArray safeFile = reader.SafeFile;
			byte[] pageContent = reader.GetPageContent(pageNum, safeFile);
			safeFile.Close();
			outp.Flush();
			foreach (byte value in pageContent)
			{
				outp.Write((char)value);
			}
			outp.Flush();
			outp.WriteLine("- - - - - Text Extraction - - - - - -");
			string textFromPage = PdfTextExtractor.GetTextFromPage(reader, pageNum, new LocationTextExtractionStrategy());
			if (textFromPage.Length != 0)
			{
				outp.WriteLine(textFromPage);
			}
			else
			{
				outp.WriteLine("No text found on page " + pageNum);
			}
			outp.WriteLine();
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x000B02D4 File Offset: 0x000AF2D4
		public static void ListContentStream(string pdfFile, TextWriter outp)
		{
			PdfReader pdfReader = new PdfReader(pdfFile);
			int numberOfPages = pdfReader.NumberOfPages;
			for (int i = 1; i <= numberOfPages; i++)
			{
				PdfContentReaderTool.ListContentStreamForPage(pdfReader, i, outp);
			}
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x000B0304 File Offset: 0x000AF304
		public static void ListContentStream(string pdfFile, int pageNum, TextWriter outp)
		{
			PdfReader reader = new PdfReader(pdfFile);
			PdfContentReaderTool.ListContentStreamForPage(reader, pageNum, outp);
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x000B0320 File Offset: 0x000AF320
		public static void Main(string[] args)
		{
			try
			{
				if (args.Length < 1 || args.Length > 3)
				{
					Console.WriteLine("Usage:  PdfContentReaderTool <pdf file> [<output file>|stdout] [<page num>]");
				}
				else
				{
					TextWriter textWriter = Console.Out;
					if (args.Length >= 2 && !Util.EqualsIgnoreCase(args[1], "stdout"))
					{
						Console.WriteLine("Writing PDF content to " + args[1]);
						textWriter = new StreamWriter(args[1]);
					}
					int num = -1;
					if (args.Length >= 3)
					{
						num = int.Parse(args[2]);
					}
					if (num == -1)
					{
						PdfContentReaderTool.ListContentStream(args[0], textWriter);
					}
					else
					{
						PdfContentReaderTool.ListContentStream(args[0], num, textWriter);
					}
					textWriter.Flush();
					if (args.Length >= 2)
					{
						textWriter.Close();
						Console.WriteLine("Finished writing content to " + args[1]);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}
	}
}
