using System;
using System.Globalization;
using System.Text;

namespace iTextSharp.text.factories
{
	// Token: 0x02000067 RID: 103
	public class RomanNumberFactory
	{
		// Token: 0x0600035C RID: 860 RVA: 0x00011AF4 File Offset: 0x00010AF4
		public static string GetString(int index)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (index < 0)
			{
				stringBuilder.Append('-');
				index = -index;
			}
			if (index > 3000)
			{
				stringBuilder.Append('|');
				stringBuilder.Append(RomanNumberFactory.GetString(index / 1000));
				stringBuilder.Append('|');
				index -= index / 1000 * 1000;
			}
			int num = 0;
			for (;;)
			{
				RomanNumberFactory.RomanDigit romanDigit = RomanNumberFactory.roman[num];
				while (index >= romanDigit.value)
				{
					stringBuilder.Append(romanDigit.digit);
					index -= romanDigit.value;
				}
				if (index <= 0)
				{
					break;
				}
				int num2 = num;
				while (!RomanNumberFactory.roman[++num2].pre)
				{
				}
				if (index + RomanNumberFactory.roman[num2].value >= romanDigit.value)
				{
					stringBuilder.Append(RomanNumberFactory.roman[num2].digit).Append(romanDigit.digit);
					index -= romanDigit.value - RomanNumberFactory.roman[num2].value;
				}
				num++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00011BF4 File Offset: 0x00010BF4
		public static string GetLowerCaseString(int index)
		{
			return RomanNumberFactory.GetString(index);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00011BFC File Offset: 0x00010BFC
		public static string GetUpperCaseString(int index)
		{
			return RomanNumberFactory.GetString(index).ToUpper(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00011C0E File Offset: 0x00010C0E
		public static string GetString(int index, bool lowercase)
		{
			if (lowercase)
			{
				return RomanNumberFactory.GetLowerCaseString(index);
			}
			return RomanNumberFactory.GetUpperCaseString(index);
		}

		// Token: 0x040001C0 RID: 448
		private static RomanNumberFactory.RomanDigit[] roman = new RomanNumberFactory.RomanDigit[]
		{
			new RomanNumberFactory.RomanDigit('m', 1000, false),
			new RomanNumberFactory.RomanDigit('d', 500, false),
			new RomanNumberFactory.RomanDigit('c', 100, true),
			new RomanNumberFactory.RomanDigit('l', 50, false),
			new RomanNumberFactory.RomanDigit('x', 10, true),
			new RomanNumberFactory.RomanDigit('v', 5, false),
			new RomanNumberFactory.RomanDigit('i', 1, true)
		};

		// Token: 0x02000068 RID: 104
		internal class RomanDigit
		{
			// Token: 0x06000362 RID: 866 RVA: 0x00011CA1 File Offset: 0x00010CA1
			internal RomanDigit(char digit, int value, bool pre)
			{
				this.digit = digit;
				this.value = value;
				this.pre = pre;
			}

			// Token: 0x040001C1 RID: 449
			public char digit;

			// Token: 0x040001C2 RID: 450
			public int value;

			// Token: 0x040001C3 RID: 451
			public bool pre;
		}
	}
}
