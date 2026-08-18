using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Barcode.PDF417ClassLibrary
{
	// Token: 0x0200009A RID: 154
	internal static class ErrorCorrectionGenerator
	{
		// Token: 0x060005A4 RID: 1444 RVA: 0x0000E318 File Offset: 0x0000C518
		internal static List<long> GenerateErrorCorrectionSequence(List<long> dataCodeWords, int errorCorrectionLevel)
		{
			int count = SpecificationData.ErrorCorrectionLevels[errorCorrectionLevel].Count;
			List<long> list = new List<long>(count);
			for (int i = 0; i < count; i++)
			{
				list.Add(0L);
			}
			foreach (long num in dataCodeWords)
			{
				int num2 = (int)num;
				long num3 = ((long)num2 + list[count - 1]) % 929L;
				long num4;
				long num5;
				for (int j = list.Count - 1; j > 0; j--)
				{
					num4 = num3 * (long)SpecificationData.ErrorCorrectionLevels[errorCorrectionLevel][j] % 929L;
					num5 = 929L - num4;
					list[j] = (list[j - 1] + num5) % 929L;
				}
				num4 = num3 * (long)SpecificationData.ErrorCorrectionLevels[errorCorrectionLevel][0] % 929L;
				num5 = 929L - num4;
				list[0] = num5 % 929L;
			}
			for (int k = 0; k < list.Count; k++)
			{
				if (list[k] != 0L)
				{
					list[k] = 929L - list[k];
				}
			}
			list.Reverse();
			return list;
		}
	}
}
