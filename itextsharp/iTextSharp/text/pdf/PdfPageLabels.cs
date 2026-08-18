using System;
using System.Collections.Generic;
using iTextSharp.text.error_messages;
using iTextSharp.text.factories;

namespace iTextSharp.text.pdf
{
	// Token: 0x020002CC RID: 716
	public class PdfPageLabels
	{
		// Token: 0x06001AD2 RID: 6866 RVA: 0x0009DF28 File Offset: 0x0009CF28
		public PdfPageLabels()
		{
			this.map = new Dictionary<int, PdfDictionary>();
			this.AddPageLabel(1, 0, null, 1);
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x0009DF48 File Offset: 0x0009CF48
		public void AddPageLabel(int page, int numberStyle, string text, int firstPage)
		{
			if (page < 1 || firstPage < 1)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("in.a.page.label.the.page.numbers.must.be.greater.or.equal.to.1"));
			}
			PdfDictionary pdfDictionary = new PdfDictionary();
			if (numberStyle >= 0 && numberStyle < PdfPageLabels.numberingStyle.Length)
			{
				pdfDictionary.Put(PdfName.S, PdfPageLabels.numberingStyle[numberStyle]);
			}
			if (text != null)
			{
				pdfDictionary.Put(PdfName.P, new PdfString(text, "UnicodeBig"));
			}
			if (firstPage != 1)
			{
				pdfDictionary.Put(PdfName.ST, new PdfNumber(firstPage));
			}
			this.map[page - 1] = pdfDictionary;
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x0009DFD3 File Offset: 0x0009CFD3
		public void AddPageLabel(int page, int numberStyle, string text)
		{
			this.AddPageLabel(page, numberStyle, text, 1);
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x0009DFDF File Offset: 0x0009CFDF
		public void AddPageLabel(int page, int numberStyle)
		{
			this.AddPageLabel(page, numberStyle, null, 1);
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x0009DFEB File Offset: 0x0009CFEB
		public void AddPageLabel(PdfPageLabels.PdfPageLabelFormat format)
		{
			this.AddPageLabel(format.physicalPage, format.numberStyle, format.prefix, format.logicalPage);
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x0009E00B File Offset: 0x0009D00B
		public void RemovePageLabel(int page)
		{
			if (page <= 1)
			{
				return;
			}
			this.map.Remove(page - 1);
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x0009E021 File Offset: 0x0009D021
		internal PdfDictionary GetDictionary(PdfWriter writer)
		{
			return PdfNumberTree.WriteTree<PdfDictionary>(this.map, writer);
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x0009E030 File Offset: 0x0009D030
		public static string[] GetPageLabels(PdfReader reader)
		{
			int numberOfPages = reader.NumberOfPages;
			PdfDictionary catalog = reader.Catalog;
			PdfDictionary pdfDictionary = (PdfDictionary)PdfReader.GetPdfObjectRelease(catalog.Get(PdfName.PAGELABELS));
			if (pdfDictionary == null)
			{
				return null;
			}
			string[] array = new string[numberOfPages];
			Dictionary<int, PdfObject> dictionary = PdfNumberTree.ReadTree(pdfDictionary);
			int num = 1;
			string text = "";
			char c = 'D';
			int i = 0;
			while (i < numberOfPages)
			{
				if (dictionary.ContainsKey(i))
				{
					PdfDictionary pdfDictionary2 = (PdfDictionary)PdfReader.GetPdfObjectRelease(dictionary[i]);
					if (pdfDictionary2.Contains(PdfName.ST))
					{
						num = ((PdfNumber)pdfDictionary2.Get(PdfName.ST)).IntValue;
					}
					else
					{
						num = 1;
					}
					if (pdfDictionary2.Contains(PdfName.P))
					{
						text = ((PdfString)pdfDictionary2.Get(PdfName.P)).ToUnicodeString();
					}
					if (pdfDictionary2.Contains(PdfName.S))
					{
						c = ((PdfName)pdfDictionary2.Get(PdfName.S)).ToString()[1];
					}
				}
				char c2 = c;
				if (c2 <= 'R')
				{
					if (c2 != 'A')
					{
						if (c2 != 'R')
						{
							goto IL_112;
						}
						array[i] = text + RomanNumberFactory.GetUpperCaseString(num);
					}
					else
					{
						array[i] = text + RomanAlphabetFactory.GetUpperCaseString(num);
					}
				}
				else if (c2 != 'a')
				{
					if (c2 != 'r')
					{
						goto IL_112;
					}
					array[i] = text + RomanNumberFactory.GetLowerCaseString(num);
				}
				else
				{
					array[i] = text + RomanAlphabetFactory.GetLowerCaseString(num);
				}
				IL_174:
				num++;
				i++;
				continue;
				IL_112:
				array[i] = text + num;
				goto IL_174;
			}
			return array;
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x0009E1C8 File Offset: 0x0009D1C8
		public static PdfPageLabels.PdfPageLabelFormat[] GetPageLabelFormats(PdfReader reader)
		{
			PdfDictionary catalog = reader.Catalog;
			PdfDictionary pdfDictionary = (PdfDictionary)PdfReader.GetPdfObjectRelease(catalog.Get(PdfName.PAGELABELS));
			if (pdfDictionary == null)
			{
				return null;
			}
			Dictionary<int, PdfObject> dictionary = PdfNumberTree.ReadTree(pdfDictionary);
			int[] array = new int[dictionary.Count];
			dictionary.Keys.CopyTo(array, 0);
			Array.Sort<int>(array);
			PdfPageLabels.PdfPageLabelFormat[] array2 = new PdfPageLabels.PdfPageLabelFormat[dictionary.Count];
			for (int i = 0; i < array.Length; i++)
			{
				int num = array[i];
				PdfDictionary pdfDictionary2 = (PdfDictionary)PdfReader.GetPdfObjectRelease(dictionary[num]);
				int logicalPage;
				if (pdfDictionary2.Contains(PdfName.ST))
				{
					logicalPage = ((PdfNumber)pdfDictionary2.Get(PdfName.ST)).IntValue;
				}
				else
				{
					logicalPage = 1;
				}
				string prefix;
				if (pdfDictionary2.Contains(PdfName.P))
				{
					prefix = ((PdfString)pdfDictionary2.Get(PdfName.P)).ToUnicodeString();
				}
				else
				{
					prefix = "";
				}
				int numberStyle;
				if (pdfDictionary2.Contains(PdfName.S))
				{
					char c = ((PdfName)pdfDictionary2.Get(PdfName.S)).ToString()[1];
					char c2 = c;
					if (c2 <= 'R')
					{
						if (c2 == 'A')
						{
							numberStyle = 3;
							goto IL_13F;
						}
						if (c2 == 'R')
						{
							numberStyle = 1;
							goto IL_13F;
						}
					}
					else
					{
						if (c2 == 'a')
						{
							numberStyle = 4;
							goto IL_13F;
						}
						if (c2 == 'r')
						{
							numberStyle = 2;
							goto IL_13F;
						}
					}
					numberStyle = 0;
				}
				else
				{
					numberStyle = 5;
				}
				IL_13F:
				array2[i] = new PdfPageLabels.PdfPageLabelFormat(num + 1, numberStyle, prefix, logicalPage);
			}
			return array2;
		}

		// Token: 0x040011DB RID: 4571
		public const int DECIMAL_ARABIC_NUMERALS = 0;

		// Token: 0x040011DC RID: 4572
		public const int UPPERCASE_ROMAN_NUMERALS = 1;

		// Token: 0x040011DD RID: 4573
		public const int LOWERCASE_ROMAN_NUMERALS = 2;

		// Token: 0x040011DE RID: 4574
		public const int UPPERCASE_LETTERS = 3;

		// Token: 0x040011DF RID: 4575
		public const int LOWERCASE_LETTERS = 4;

		// Token: 0x040011E0 RID: 4576
		public const int EMPTY = 5;

		// Token: 0x040011E1 RID: 4577
		internal static PdfName[] numberingStyle = new PdfName[]
		{
			PdfName.D,
			PdfName.R,
			new PdfName("r"),
			PdfName.A,
			new PdfName("a")
		};

		// Token: 0x040011E2 RID: 4578
		internal Dictionary<int, PdfDictionary> map;

		// Token: 0x020002CD RID: 717
		public class PdfPageLabelFormat
		{
			// Token: 0x06001ADC RID: 6876 RVA: 0x0009E388 File Offset: 0x0009D388
			public PdfPageLabelFormat(int physicalPage, int numberStyle, string prefix, int logicalPage)
			{
				this.physicalPage = physicalPage;
				this.numberStyle = numberStyle;
				this.prefix = prefix;
				this.logicalPage = logicalPage;
			}

			// Token: 0x040011E3 RID: 4579
			public int physicalPage;

			// Token: 0x040011E4 RID: 4580
			public int numberStyle;

			// Token: 0x040011E5 RID: 4581
			public string prefix;

			// Token: 0x040011E6 RID: 4582
			public int logicalPage;
		}
	}
}
