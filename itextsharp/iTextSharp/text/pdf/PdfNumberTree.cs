using System;
using System.Collections.Generic;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000324 RID: 804
	public class PdfNumberTree
	{
		// Token: 0x06001D41 RID: 7489 RVA: 0x000AFAE0 File Offset: 0x000AEAE0
		public static PdfDictionary WriteTree<T>(Dictionary<int, T> items, PdfWriter writer) where T : PdfObject
		{
			if (items.Count == 0)
			{
				return null;
			}
			int[] array = new int[items.Count];
			items.Keys.CopyTo(array, 0);
			Array.Sort<int>(array);
			if (array.Length <= 64)
			{
				PdfDictionary pdfDictionary = new PdfDictionary();
				PdfArray pdfArray = new PdfArray();
				for (int i = 0; i < array.Length; i++)
				{
					pdfArray.Add(new PdfNumber(array[i]));
					pdfArray.Add(items[array[i]]);
				}
				pdfDictionary.Put(PdfName.NUMS, pdfArray);
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
				pdfArray2.Add(new PdfNumber(array[k]));
				pdfArray2.Add(new PdfNumber(array[num2 - 1]));
				pdfDictionary2.Put(PdfName.LIMITS, pdfArray2);
				pdfArray2 = new PdfArray();
				while (k < num2)
				{
					pdfArray2.Add(new PdfNumber(array[k]));
					pdfArray2.Add(items[array[k]]);
					k++;
				}
				pdfDictionary2.Put(PdfName.NUMS, pdfArray2);
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
					pdfArray3.Add(new PdfNumber(array[m * num]));
					pdfArray3.Add(new PdfNumber(array[Math.Min((m + 1) * num, array.Length) - 1]));
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

		// Token: 0x06001D42 RID: 7490 RVA: 0x000AFD70 File Offset: 0x000AED70
		private static void IterateItems(PdfDictionary dic, Dictionary<int, PdfObject> items)
		{
			PdfArray pdfArray = (PdfArray)PdfReader.GetPdfObjectRelease(dic.Get(PdfName.NUMS));
			if (pdfArray != null)
			{
				for (int i = 0; i < pdfArray.Size; i++)
				{
					PdfNumber pdfNumber = (PdfNumber)PdfReader.GetPdfObjectRelease(pdfArray[i++]);
					items[pdfNumber.IntValue] = pdfArray[i];
				}
				return;
			}
			if ((pdfArray = (PdfArray)PdfReader.GetPdfObjectRelease(dic.Get(PdfName.KIDS))) != null)
			{
				for (int j = 0; j < pdfArray.Size; j++)
				{
					PdfDictionary dic2 = (PdfDictionary)PdfReader.GetPdfObjectRelease(pdfArray[j]);
					PdfNumberTree.IterateItems(dic2, items);
				}
			}
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x000AFE18 File Offset: 0x000AEE18
		public static Dictionary<int, PdfObject> ReadTree(PdfDictionary dic)
		{
			Dictionary<int, PdfObject> dictionary = new Dictionary<int, PdfObject>();
			if (dic != null)
			{
				PdfNumberTree.IterateItems(dic, dictionary);
			}
			return dictionary;
		}

		// Token: 0x0400142B RID: 5163
		private const int leafSize = 64;
	}
}
