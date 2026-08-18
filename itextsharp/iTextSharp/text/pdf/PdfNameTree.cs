using System;
using System.Collections.Generic;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000417 RID: 1047
	public class PdfNameTree
	{
		// Token: 0x060023A2 RID: 9122 RVA: 0x000D9F08 File Offset: 0x000D8F08
		public static PdfDictionary WriteTree<T>(Dictionary<string, T> items, PdfWriter writer) where T : PdfObject
		{
			if (items.Count == 0)
			{
				return null;
			}
			string[] array = new string[items.Count];
			items.Keys.CopyTo(array, 0);
			Array.Sort<string>(array, new PdfNameTree.CompareSrt());
			if (array.Length <= 64)
			{
				PdfDictionary pdfDictionary = new PdfDictionary();
				PdfArray pdfArray = new PdfArray();
				for (int i = 0; i < array.Length; i++)
				{
					pdfArray.Add(new PdfString(array[i], null));
					pdfArray.Add(items[array[i]]);
				}
				pdfDictionary.Put(PdfName.NAMES, pdfArray);
				return pdfDictionary;
			}
			int num = 64;
			PdfIndirectReference[] array2 = new PdfIndirectReference[(array.Length + 64 - 1) / 64];
			for (int j = 0; j < array2.Length; j++)
			{
				int k = j * 64;
				int num2 = Math.Min(k + 64, array.Length);
				PdfDictionary pdfDictionary2 = new PdfDictionary();
				PdfArray pdfArray2 = new PdfArray();
				pdfArray2.Add(new PdfString(array[k], null));
				pdfArray2.Add(new PdfString(array[num2 - 1], null));
				pdfDictionary2.Put(PdfName.LIMITS, pdfArray2);
				pdfArray2 = new PdfArray();
				while (k < num2)
				{
					pdfArray2.Add(new PdfString(array[k], null));
					pdfArray2.Add(items[array[k]]);
					k++;
				}
				pdfDictionary2.Put(PdfName.NAMES, pdfArray2);
				array2[j] = writer.AddToBody(pdfDictionary2).IndirectReference;
			}
			int l;
			int num3;
			for (l = array2.Length; l > 64; l = num3)
			{
				num *= 64;
				num3 = (array.Length + num - 1) / num;
				for (int m = 0; m < num3; m++)
				{
					int n = m * 64;
					int num4 = Math.Min(n + 64, l);
					PdfDictionary pdfDictionary3 = new PdfDictionary();
					PdfArray pdfArray3 = new PdfArray();
					pdfArray3.Add(new PdfString(array[m * num], null));
					pdfArray3.Add(new PdfString(array[Math.Min((m + 1) * num, array.Length) - 1], null));
					pdfDictionary3.Put(PdfName.LIMITS, pdfArray3);
					pdfArray3 = new PdfArray();
					while (n < num4)
					{
						pdfArray3.Add(array2[n]);
						n++;
					}
					pdfDictionary3.Put(PdfName.KIDS, pdfArray3);
					array2[m] = writer.AddToBody(pdfDictionary3).IndirectReference;
				}
			}
			PdfArray pdfArray4 = new PdfArray();
			for (int num5 = 0; num5 < l; num5++)
			{
				pdfArray4.Add(array2[num5]);
			}
			PdfDictionary pdfDictionary4 = new PdfDictionary();
			pdfDictionary4.Put(PdfName.KIDS, pdfArray4);
			return pdfDictionary4;
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x000DA1A4 File Offset: 0x000D91A4
		private static void IterateItems(PdfDictionary dic, Dictionary<string, PdfObject> items)
		{
			PdfArray pdfArray = (PdfArray)PdfReader.GetPdfObjectRelease(dic.Get(PdfName.NAMES));
			if (pdfArray != null)
			{
				for (int i = 0; i < pdfArray.Size; i++)
				{
					PdfString pdfString = (PdfString)PdfReader.GetPdfObjectRelease(pdfArray[i++]);
					items[PdfEncodings.ConvertToString(pdfString.GetBytes(), null)] = pdfArray[i];
				}
				return;
			}
			if ((pdfArray = (PdfArray)PdfReader.GetPdfObjectRelease(dic.Get(PdfName.KIDS))) != null)
			{
				for (int j = 0; j < pdfArray.Size; j++)
				{
					PdfDictionary dic2 = (PdfDictionary)PdfReader.GetPdfObjectRelease(pdfArray[j]);
					PdfNameTree.IterateItems(dic2, items);
				}
			}
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x000DA250 File Offset: 0x000D9250
		public static Dictionary<string, PdfObject> ReadTree(PdfDictionary dic)
		{
			Dictionary<string, PdfObject> dictionary = new Dictionary<string, PdfObject>();
			if (dic != null)
			{
				PdfNameTree.IterateItems(dic, dictionary);
			}
			return dictionary;
		}

		// Token: 0x0400188C RID: 6284
		private const int leafSize = 64;

		// Token: 0x02000418 RID: 1048
		internal class CompareSrt : IComparer<string>
		{
			// Token: 0x060023A6 RID: 9126 RVA: 0x000DA278 File Offset: 0x000D9278
			public int Compare(string x, string y)
			{
				char[] array = x.ToCharArray();
				char[] array2 = y.ToCharArray();
				int num = Math.Min(array.Length, array2.Length);
				for (int i = 0; i < num; i++)
				{
					if (array[i] < array2[i])
					{
						return -1;
					}
					if (array[i] > array2[i])
					{
						return 1;
					}
				}
				if (array.Length < array2.Length)
				{
					return -1;
				}
				if (array.Length > array2.Length)
				{
					return 1;
				}
				return 0;
			}
		}
	}
}
