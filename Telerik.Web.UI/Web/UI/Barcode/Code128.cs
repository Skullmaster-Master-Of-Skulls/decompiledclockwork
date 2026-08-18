using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020009CA RID: 2506
	internal class Code128 : Symbology1D
	{
		// Token: 0x06006025 RID: 24613 RVA: 0x0012512C File Offset: 0x0012332C
		[Description("Initializes a new instance of Code128 type.")]
		public Code128()
		{
			this.encoding = new List<string>();
			this.encoding.Add("11011001100");
			this.encoding.Add("11001101100");
			this.encoding.Add("11001100110");
			this.encoding.Add("10010011000");
			this.encoding.Add("10010001100");
			this.encoding.Add("10001001100");
			this.encoding.Add("10011001000");
			this.encoding.Add("10011000100");
			this.encoding.Add("10001100100");
			this.encoding.Add("11001001000");
			this.encoding.Add("11001000100");
			this.encoding.Add("11000100100");
			this.encoding.Add("10110011100");
			this.encoding.Add("10011011100");
			this.encoding.Add("10011001110");
			this.encoding.Add("10111001100");
			this.encoding.Add("10011101100");
			this.encoding.Add("10011100110");
			this.encoding.Add("11001110010");
			this.encoding.Add("11001011100");
			this.encoding.Add("11001001110");
			this.encoding.Add("11011100100");
			this.encoding.Add("11001110100");
			this.encoding.Add("11101101110");
			this.encoding.Add("11101001100");
			this.encoding.Add("11100101100");
			this.encoding.Add("11100100110");
			this.encoding.Add("11101100100");
			this.encoding.Add("11100110100");
			this.encoding.Add("11100110010");
			this.encoding.Add("11011011000");
			this.encoding.Add("11011000110");
			this.encoding.Add("11000110110");
			this.encoding.Add("10100011000");
			this.encoding.Add("10001011000");
			this.encoding.Add("10001000110");
			this.encoding.Add("10110001000");
			this.encoding.Add("10001101000");
			this.encoding.Add("10001100010");
			this.encoding.Add("11010001000");
			this.encoding.Add("11000101000");
			this.encoding.Add("11000100010");
			this.encoding.Add("10110111000");
			this.encoding.Add("10110001110");
			this.encoding.Add("10001101110");
			this.encoding.Add("10111011000");
			this.encoding.Add("10111000110");
			this.encoding.Add("10001110110");
			this.encoding.Add("11101110110");
			this.encoding.Add("11010001110");
			this.encoding.Add("11000101110");
			this.encoding.Add("11011101000");
			this.encoding.Add("11011100010");
			this.encoding.Add("11011101110");
			this.encoding.Add("11101011000");
			this.encoding.Add("11101000110");
			this.encoding.Add("11100010110");
			this.encoding.Add("11101101000");
			this.encoding.Add("11101100010");
			this.encoding.Add("11100011010");
			this.encoding.Add("11101111010");
			this.encoding.Add("11001000010");
			this.encoding.Add("11110001010");
			this.encoding.Add("10100110000");
			this.encoding.Add("10100001100");
			this.encoding.Add("10010110000");
			this.encoding.Add("10010000110");
			this.encoding.Add("10000101100");
			this.encoding.Add("10000100110");
			this.encoding.Add("10110010000");
			this.encoding.Add("10110000100");
			this.encoding.Add("10011010000");
			this.encoding.Add("10011000010");
			this.encoding.Add("10000110100");
			this.encoding.Add("10000110010");
			this.encoding.Add("11000010010");
			this.encoding.Add("11001010000");
			this.encoding.Add("11110111010");
			this.encoding.Add("11000010100");
			this.encoding.Add("10001111010");
			this.encoding.Add("10100111100");
			this.encoding.Add("10010111100");
			this.encoding.Add("10010011110");
			this.encoding.Add("10111100100");
			this.encoding.Add("10011110100");
			this.encoding.Add("10011110010");
			this.encoding.Add("11110100100");
			this.encoding.Add("11110010100");
			this.encoding.Add("11110010010");
			this.encoding.Add("11011011110");
			this.encoding.Add("11011110110");
			this.encoding.Add("11110110110");
			this.encoding.Add("10101111000");
			this.encoding.Add("10100011110");
			this.encoding.Add("10001011110");
			this.encoding.Add("10111101000");
			this.encoding.Add("10111100010");
			this.encoding.Add("11110101000");
			this.encoding.Add("11110100010");
			this.encoding.Add("10111011110");
			this.encoding.Add("10111101110");
			this.encoding.Add("11101011110");
			this.encoding.Add("11110101110");
			this.encoding.Add("11010000100");
			this.encoding.Add("11010010000");
			this.encoding.Add("11010011100");
			this.encoding.Add("11000111010");
		}

		// Token: 0x06006026 RID: 24614 RVA: 0x001257FC File Offset: 0x001239FC
		public string ValidateValue(string value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!string.IsNullOrEmpty(value))
			{
				foreach (char c in value)
				{
					if (this.IsValid(c))
					{
						stringBuilder.Append(c);
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006027 RID: 24615 RVA: 0x00125848 File Offset: 0x00123A48
		internal override string GetEncoding(string value)
		{
			value = this.ValidateValue(value);
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			int[] indices = this.GetIndices(value);
			List<int> list = new List<int>(indices);
			base.CheckSum = this.GetChecksum(indices).ToString();
			if (base.CalculateCheckSum)
			{
				list.Add(this.GetChecksum(indices));
			}
			list.Add(Code128.Suffix);
			StringBuilder stringBuilder = new StringBuilder();
			foreach (int num in list)
			{
				if (num != -1)
				{
					stringBuilder.Append(this.encoding[num]);
				}
			}
			stringBuilder.Append("11");
			return stringBuilder.ToString();
		}

		// Token: 0x06006028 RID: 24616 RVA: 0x0012591C File Offset: 0x00123B1C
		internal int GetChecksum(int[] array)
		{
			return this.GetChecksum(array, 103);
		}

		// Token: 0x06006029 RID: 24617 RVA: 0x00125928 File Offset: 0x00123B28
		internal int GetChecksum(int[] array, int modulo)
		{
			int num = array[0];
			for (int i = 1; i < array.Length; i++)
			{
				num += array[i] * i;
			}
			return num % modulo;
		}

		// Token: 0x0600602A RID: 24618 RVA: 0x00125952 File Offset: 0x00123B52
		protected virtual int[] GetIndices(string value)
		{
			return this.GetIndices(value, 0, value.Length);
		}

		// Token: 0x0600602B RID: 24619 RVA: 0x00125962 File Offset: 0x00123B62
		private bool IsValid(char symbol)
		{
			return this.IsNormal(symbol) || this.IsSpecial(symbol);
		}

		// Token: 0x0600602C RID: 24620 RVA: 0x00125976 File Offset: 0x00123B76
		private bool IsNormal(char symbol)
		{
			return symbol <= '\u007f';
		}

		// Token: 0x0600602D RID: 24621 RVA: 0x00125980 File Offset: 0x00123B80
		private bool IsSpecial(char symbol)
		{
			return symbol >= 'ô' && symbol <= 'ÿ';
		}

		// Token: 0x0600602E RID: 24622 RVA: 0x00125998 File Offset: 0x00123B98
		private int[] GetIndices(string value, int start, int final)
		{
			List<int> list = new List<int>();
			Code128C code128C = new Code128C();
			Code128A code128A = new Code128A();
			Code128B code128B = new Code128B();
			while (start < final)
			{
				int num = code128C.GetSwitch(value, start, final);
				if (num > start)
				{
					list.AddRange(code128C.GetIndices(value, start, num));
				}
				else
				{
					int @switch = code128A.GetSwitch(value, start, final);
					int switch2 = code128B.GetSwitch(value, start, final);
					num = Math.Max(@switch, switch2);
					if (@switch >= switch2)
					{
						list.AddRange(code128A.GetIndices(value, start, num));
					}
					else
					{
						list.AddRange(code128B.GetIndices(value, start, num));
					}
				}
				start = num;
			}
			return list.ToArray();
		}

		// Token: 0x04001743 RID: 5955
		private static readonly int Suffix = 106;

		// Token: 0x04001744 RID: 5956
		private List<string> encoding;
	}
}
