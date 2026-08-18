using System;
using System.Text;

namespace Telerik.Web.Apoc.Fo.Pagination
{
	// Token: 0x0200142E RID: 5166
	internal class PageNumberGenerator
	{
		// Token: 0x0600D32A RID: 54058 RVA: 0x002EDB84 File Offset: 0x002EBD84
		public PageNumberGenerator(string format, char groupingSeparator, int groupingSize, int letterValue)
		{
			this.format = format;
			this.groupingSeparator = groupingSeparator;
			this.groupingSize = groupingSize;
			this.letterValue = letterValue;
			int length = format.Length;
			if (length != 1)
			{
				for (int i = 0; i < length - 1; i++)
				{
					if (format[i] != '0')
					{
						this.formatType = 1;
						this.minPadding = 0;
					}
					else
					{
						this.minPadding = length - 1;
					}
				}
				return;
			}
			if (format.Equals("1"))
			{
				this.formatType = 1;
				this.minPadding = 0;
				return;
			}
			if (format.Equals("a"))
			{
				this.formatType = 2;
				return;
			}
			if (format.Equals("A"))
			{
				this.formatType = 3;
				return;
			}
			if (format.Equals("i"))
			{
				this.formatType = 4;
				return;
			}
			if (format.Equals("I"))
			{
				this.formatType = 5;
				return;
			}
			this.formatType = 1;
			this.minPadding = 0;
		}

		// Token: 0x0600D32B RID: 54059 RVA: 0x002EDCB8 File Offset: 0x002EBEB8
		public string makeFormattedPageNumber(int number)
		{
			string text;
			if (this.formatType == 1)
			{
				text = number.ToString();
				if (this.minPadding >= text.Length)
				{
					int num = this.minPadding - text.Length + 1;
					text = this.zeros[num] + text;
				}
			}
			else if (this.formatType == 4 || this.formatType == 5)
			{
				text = this.makeRoman(number);
				if (this.formatType == 5)
				{
					text = text.ToUpper();
				}
			}
			else
			{
				text = this.makeAlpha(number);
				if (this.formatType == 3)
				{
					text = text.ToUpper();
				}
			}
			return text;
		}

		// Token: 0x0600D32C RID: 54060 RVA: 0x002EDD84 File Offset: 0x002EBF84
		private string makeRoman(int num)
		{
			int[] array = new int[]
			{
				1000,
				900,
				500,
				400,
				100,
				90,
				50,
				40,
				10,
				9,
				5,
				4,
				1
			};
			string[] array2 = new string[]
			{
				"m",
				"cm",
				"d",
				"cd",
				"c",
				"xc",
				"l",
				"xl",
				"x",
				"ix",
				"v",
				"iv",
				"i"
			};
			int num2 = 0;
			StringBuilder stringBuilder = new StringBuilder();
			while (num > 0)
			{
				while (num >= array[num2])
				{
					num -= array[num2];
					stringBuilder.Append(array2[num2]);
				}
				num2++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600D32D RID: 54061 RVA: 0x002EDE58 File Offset: 0x002EC058
		private string makeAlpha(int num)
		{
			string text = "abcdefghijklmnopqrstuvwxyz";
			StringBuilder stringBuilder = new StringBuilder();
			int num2 = 26;
			num--;
			if (num < num2)
			{
				stringBuilder.Append(text[num]);
			}
			else
			{
				while (num >= num2)
				{
					int index = num % num2;
					stringBuilder.Append(text[index]);
					num /= num2;
				}
				stringBuilder.Append(text[num - 1]);
			}
			char[] array = stringBuilder.ToString().ToCharArray();
			Array.Reverse(array);
			return new string(array).ToString();
		}

		// Token: 0x04003933 RID: 14643
		private const int DECIMAL = 1;

		// Token: 0x04003934 RID: 14644
		private const int LOWERALPHA = 2;

		// Token: 0x04003935 RID: 14645
		private const int UPPERALPHA = 3;

		// Token: 0x04003936 RID: 14646
		private const int LOWERROMAN = 4;

		// Token: 0x04003937 RID: 14647
		private const int UPPERROMAN = 5;

		// Token: 0x04003938 RID: 14648
		private string format;

		// Token: 0x04003939 RID: 14649
		private char groupingSeparator;

		// Token: 0x0400393A RID: 14650
		private int groupingSize;

		// Token: 0x0400393B RID: 14651
		private int letterValue;

		// Token: 0x0400393C RID: 14652
		private int formatType = 1;

		// Token: 0x0400393D RID: 14653
		private int minPadding;

		// Token: 0x0400393E RID: 14654
		private string[] zeros = new string[]
		{
			"",
			"0",
			"00",
			"000",
			"0000",
			"00000"
		};
	}
}
