using System;
using System.Globalization;

namespace iTextSharp.text.factories
{
	// Token: 0x02000176 RID: 374
	public class GreekAlphabetFactory
	{
		// Token: 0x06000E81 RID: 3713 RVA: 0x00053858 File Offset: 0x00052858
		public static string GetString(int index)
		{
			return GreekAlphabetFactory.GetString(index, true);
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x00053861 File Offset: 0x00052861
		public static string GetLowerCaseString(int index)
		{
			return GreekAlphabetFactory.GetString(index);
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x00053869 File Offset: 0x00052869
		public static string GetUpperCaseString(int index)
		{
			return GreekAlphabetFactory.GetString(index).ToUpper(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x0005387C File Offset: 0x0005287C
		public static string GetString(int index, bool lowercase)
		{
			if (index < 1)
			{
				return "";
			}
			index--;
			int i = 1;
			int num = 0;
			int num2 = 24;
			while (index >= num2 + num)
			{
				i++;
				num += num2;
				num2 *= 24;
			}
			int num3 = index - num;
			char[] array = new char[i];
			while (i > 0)
			{
				i--;
				array[i] = (char)(num3 % 24);
				if (array[i] > '\u0010')
				{
					char[] array2 = array;
					int num4 = i;
					array2[num4] += '\u0001';
				}
				char[] array3 = array;
				int num5 = i;
				array3[num5] += (lowercase ? 'α' : 'Α');
				array[i] = SpecialSymbol.GetCorrespondingSymbol(array[i]);
				num3 /= 24;
			}
			return new string(array);
		}
	}
}
