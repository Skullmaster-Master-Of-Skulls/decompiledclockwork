using System;
using System.Globalization;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.factories
{
	// Token: 0x02000537 RID: 1335
	public class RomanAlphabetFactory
	{
		// Token: 0x06002DF2 RID: 11762 RVA: 0x0011C090 File Offset: 0x0011B090
		public static string GetString(int index)
		{
			if (index < 1)
			{
				throw new FormatException(MessageLocalization.GetComposedMessage("you.can.t.translate.a.negative.number.into.an.alphabetical.value"));
			}
			index--;
			int i = 1;
			int num = 0;
			int num2 = 26;
			while (index >= num2 + num)
			{
				i++;
				num += num2;
				num2 *= 26;
			}
			int num3 = index - num;
			char[] array = new char[i];
			while (i > 0)
			{
				array[--i] = (char)(97 + num3 % 26);
				num3 /= 26;
			}
			return new string(array);
		}

		// Token: 0x06002DF3 RID: 11763 RVA: 0x0011C100 File Offset: 0x0011B100
		public static string GetLowerCaseString(int index)
		{
			return RomanAlphabetFactory.GetString(index);
		}

		// Token: 0x06002DF4 RID: 11764 RVA: 0x0011C108 File Offset: 0x0011B108
		public static string GetUpperCaseString(int index)
		{
			return RomanAlphabetFactory.GetString(index).ToUpper(CultureInfo.InvariantCulture);
		}

		// Token: 0x06002DF5 RID: 11765 RVA: 0x0011C11A File Offset: 0x0011B11A
		public static string GetString(int index, bool lowercase)
		{
			if (lowercase)
			{
				return RomanAlphabetFactory.GetLowerCaseString(index);
			}
			return RomanAlphabetFactory.GetUpperCaseString(index);
		}
	}
}
