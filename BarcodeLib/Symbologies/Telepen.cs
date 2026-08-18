using System;
using System.Collections;

namespace BarcodeLib.Symbologies
{
	// Token: 0x02000012 RID: 18
	internal class Telepen : BarcodeCommon, IBarcode
	{
		// Token: 0x06000085 RID: 133 RVA: 0x00008C97 File Offset: 0x00006E97
		public Telepen(string input)
		{
			this.Raw_Data = input;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00008CB0 File Offset: 0x00006EB0
		private string Encode_Telepen()
		{
			if (Telepen.Telepen_Code.Count == 0)
			{
				this.Init_Telepen();
			}
			this.iCheckSum = 0;
			string text = "";
			this.SetEncodingSequence();
			text = Telepen.Telepen_Code[this.StartCode].ToString();
			Telepen.StartStopCode startCode = this.StartCode;
			if (startCode != Telepen.StartStopCode.START2)
			{
				if (startCode != Telepen.StartStopCode.START3)
				{
					this.EncodeASCII(base.RawData, ref text);
				}
				else
				{
					this.EncodeASCII(base.RawData.Substring(0, this.SwitchModeIndex), ref text);
					this.EncodeSwitchMode(ref text);
					this.EncodeNumeric(base.RawData.Substring(this.SwitchModeIndex), ref text);
				}
			}
			else
			{
				this.EncodeNumeric(base.RawData.Substring(0, this.SwitchModeIndex), ref text);
				if (this.SwitchModeIndex < base.RawData.Length)
				{
					this.EncodeSwitchMode(ref text);
					this.EncodeASCII(base.RawData.Substring(this.SwitchModeIndex), ref text);
				}
			}
			text += Telepen.Telepen_Code[this.Calculate_Checksum(this.iCheckSum)];
			text += Telepen.Telepen_Code[this.StopCode];
			return text;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00008DEC File Offset: 0x00006FEC
		private void EncodeASCII(string input, ref string output)
		{
			try
			{
				foreach (char c in input)
				{
					output += Telepen.Telepen_Code[c];
					this.iCheckSum += Convert.ToInt32(c);
				}
			}
			catch
			{
				base.Error("ETELEPEN-1: Invalid data when encoding ASCII");
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00008E60 File Offset: 0x00007060
		private void EncodeNumeric(string input, ref string output)
		{
			try
			{
				if (input.Length % 2 > 0)
				{
					base.Error("ETELEPEN-3: Numeric encoding attempted on odd number of characters");
				}
				for (int i = 0; i < input.Length; i += 2)
				{
					output += Telepen.Telepen_Code[Convert.ToChar(int.Parse(input.Substring(i, 2)) + 27)];
					this.iCheckSum += int.Parse(input.Substring(i, 2)) + 27;
				}
			}
			catch
			{
				base.Error("ETELEPEN-2: Numeric encoding failed");
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00008F00 File Offset: 0x00007100
		private void EncodeSwitchMode(ref string output)
		{
			this.iCheckSum += 16;
			output += Telepen.Telepen_Code[Convert.ToChar(16)];
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00008F30 File Offset: 0x00007130
		private char Calculate_Checksum(int iCheckSum)
		{
			return Convert.ToChar(127 - iCheckSum % 127);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00008F40 File Offset: 0x00007140
		private void SetEncodingSequence()
		{
			this.StartCode = Telepen.StartStopCode.START1;
			this.StopCode = Telepen.StartStopCode.STOP1;
			this.SwitchModeIndex = this.Raw_Data.Length;
			int num = 0;
			string raw_Data = this.Raw_Data;
			int num2 = 0;
			while (num2 < raw_Data.Length && char.IsNumber(raw_Data[num2]))
			{
				num++;
				num2++;
			}
			if (num == this.Raw_Data.Length)
			{
				this.StartCode = Telepen.StartStopCode.START2;
				this.StopCode = Telepen.StartStopCode.STOP2;
				if (this.Raw_Data.Length % 2 > 0)
				{
					this.SwitchModeIndex = base.RawData.Length - 1;
					return;
				}
			}
			else
			{
				int num3 = 0;
				int num4 = this.Raw_Data.Length - 1;
				while (num4 >= 0 && char.IsNumber(this.Raw_Data[num4]))
				{
					num3++;
					num4--;
				}
				if (num >= 4 || num3 >= 4)
				{
					if (num > num3)
					{
						this.StartCode = Telepen.StartStopCode.START2;
						this.StopCode = Telepen.StartStopCode.STOP2;
						this.SwitchModeIndex = ((num % 2 == 1) ? (num - 1) : num);
						return;
					}
					this.StartCode = Telepen.StartStopCode.START3;
					this.StopCode = Telepen.StartStopCode.STOP3;
					this.SwitchModeIndex = ((num3 % 2 == 1) ? (this.Raw_Data.Length - num3 + 1) : (this.Raw_Data.Length - num3));
				}
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00009078 File Offset: 0x00007278
		private void Init_Telepen()
		{
			Telepen.Telepen_Code.Add(Convert.ToChar(0), "1110111011101110");
			Telepen.Telepen_Code.Add(Convert.ToChar(1), "1011101110111010");
			Telepen.Telepen_Code.Add(Convert.ToChar(2), "1110001110111010");
			Telepen.Telepen_Code.Add(Convert.ToChar(3), "1010111011101110");
			Telepen.Telepen_Code.Add(Convert.ToChar(4), "1110101110111010");
			Telepen.Telepen_Code.Add(Convert.ToChar(5), "1011100011101110");
			Telepen.Telepen_Code.Add(Convert.ToChar(6), "1000100011101110");
			Telepen.Telepen_Code.Add(Convert.ToChar(7), "1010101110111010");
			Telepen.Telepen_Code.Add(Convert.ToChar(8), "1110111000111010");
			Telepen.Telepen_Code.Add(Convert.ToChar(9), "1011101011101110");
			Telepen.Telepen_Code.Add(Convert.ToChar(10), "1110001011101110");
			Telepen.Telepen_Code.Add(Convert.ToChar(11), "1010111000111010");
			Telepen.Telepen_Code.Add(Convert.ToChar(12), "1110101011101110");
			Telepen.Telepen_Code.Add(Convert.ToChar(13), "1010001000111010");
			Telepen.Telepen_Code.Add(Convert.ToChar(14), "1000101000111010");
			Telepen.Telepen_Code.Add(Convert.ToChar(15), "1010101011101110");
			Telepen.Telepen_Code.Add(Convert.ToChar(16), "1110111010111010");
			Telepen.Telepen_Code.Add(Convert.ToChar(17), "1011101110001110");
			Telepen.Telepen_Code.Add(Convert.ToChar(18), "1110001110001110");
			Telepen.Telepen_Code.Add(Convert.ToChar(19), "1010111010111010");
			Telepen.Telepen_Code.Add(Convert.ToChar(20), "1110101110001110");
			Telepen.Telepen_Code.Add(Convert.ToChar(21), "1011100010111010");
			Telepen.Telepen_Code.Add(Convert.ToChar(22), "1000100010111010");
			Telepen.Telepen_Code.Add(Convert.ToChar(23), "1010101110001110");
			Telepen.Telepen_Code.Add(Convert.ToChar(24), "1110100010001110");
			Telepen.Telepen_Code.Add(Convert.ToChar(25), "1011101010111010");
			Telepen.Telepen_Code.Add(Convert.ToChar(26), "1110001010111010");
			Telepen.Telepen_Code.Add(Convert.ToChar(27), "1010100010001110");
			Telepen.Telepen_Code.Add(Convert.ToChar(28), "1110101010111010");
			Telepen.Telepen_Code.Add(Convert.ToChar(29), "1010001010001110");
			Telepen.Telepen_Code.Add(Convert.ToChar(30), "1000101010001110");
			Telepen.Telepen_Code.Add(Convert.ToChar(31), "1010101010111010");
			Telepen.Telepen_Code.Add(' ', "1110111011100010");
			Telepen.Telepen_Code.Add('!', "1011101110101110");
			Telepen.Telepen_Code.Add('"', "1110001110101110");
			Telepen.Telepen_Code.Add('#', "1010111011100010");
			Telepen.Telepen_Code.Add('$', "1110101110101110");
			Telepen.Telepen_Code.Add('%', "1011100011100010");
			Telepen.Telepen_Code.Add('&', "1000100011100010");
			Telepen.Telepen_Code.Add('\'', "1010101110101110");
			Telepen.Telepen_Code.Add('(', "1110111000101110");
			Telepen.Telepen_Code.Add(')', "1011101011100010");
			Telepen.Telepen_Code.Add('*', "1110001011100010");
			Telepen.Telepen_Code.Add('+', "1010111000101110");
			Telepen.Telepen_Code.Add(',', "1110101011100010");
			Telepen.Telepen_Code.Add('-', "1010001000101110");
			Telepen.Telepen_Code.Add('.', "1000101000101110");
			Telepen.Telepen_Code.Add('/', "1010101011100010");
			Telepen.Telepen_Code.Add('0', "1110111010101110");
			Telepen.Telepen_Code.Add('1', "1011101000100010");
			Telepen.Telepen_Code.Add('2', "1110001000100010");
			Telepen.Telepen_Code.Add('3', "1010111010101110");
			Telepen.Telepen_Code.Add('4', "1110101000100010");
			Telepen.Telepen_Code.Add('5', "1011100010101110");
			Telepen.Telepen_Code.Add('6', "1000100010101110");
			Telepen.Telepen_Code.Add('7', "1010101000100010");
			Telepen.Telepen_Code.Add('8', "1110100010100010");
			Telepen.Telepen_Code.Add('9', "1011101010101110");
			Telepen.Telepen_Code.Add(':', "1110001010101110");
			Telepen.Telepen_Code.Add(';', "1010100010100010");
			Telepen.Telepen_Code.Add('<', "1110101010101110");
			Telepen.Telepen_Code.Add('=', "1010001010100010");
			Telepen.Telepen_Code.Add('>', "1000101010100010");
			Telepen.Telepen_Code.Add('?', "1010101010101110");
			Telepen.Telepen_Code.Add('@', "1110111011101010");
			Telepen.Telepen_Code.Add('A', "1011101110111000");
			Telepen.Telepen_Code.Add('B', "1110001110111000");
			Telepen.Telepen_Code.Add('C', "1010111011101010");
			Telepen.Telepen_Code.Add('D', "1110101110111000");
			Telepen.Telepen_Code.Add('E', "1011100011101010");
			Telepen.Telepen_Code.Add('F', "1000100011101010");
			Telepen.Telepen_Code.Add('G', "1010101110111000");
			Telepen.Telepen_Code.Add('H', "1110111000111000");
			Telepen.Telepen_Code.Add('I', "1011101011101010");
			Telepen.Telepen_Code.Add('J', "1110001011101010");
			Telepen.Telepen_Code.Add('K', "1010111000111000");
			Telepen.Telepen_Code.Add('L', "1110101011101010");
			Telepen.Telepen_Code.Add('M', "1010001000111000");
			Telepen.Telepen_Code.Add('N', "1000101000111000");
			Telepen.Telepen_Code.Add('O', "1010101011101010");
			Telepen.Telepen_Code.Add('P', "1110111010111000");
			Telepen.Telepen_Code.Add('Q', "1011101110001010");
			Telepen.Telepen_Code.Add('R', "1110001110001010");
			Telepen.Telepen_Code.Add('S', "1010111010111000");
			Telepen.Telepen_Code.Add('T', "1110101110001010");
			Telepen.Telepen_Code.Add('U', "1011100010111000");
			Telepen.Telepen_Code.Add('V', "1000100010111000");
			Telepen.Telepen_Code.Add('W', "1010101110001010");
			Telepen.Telepen_Code.Add('X', "1110100010001010");
			Telepen.Telepen_Code.Add('Y', "1011101010111000");
			Telepen.Telepen_Code.Add('Z', "1110001010111000");
			Telepen.Telepen_Code.Add('[', "1010100010001010");
			Telepen.Telepen_Code.Add('\\', "1110101010111000");
			Telepen.Telepen_Code.Add(']', "1010001010001010");
			Telepen.Telepen_Code.Add('^', "1000101010001010");
			Telepen.Telepen_Code.Add('_', "1010101010111000");
			Telepen.Telepen_Code.Add('`', "1110111010001000");
			Telepen.Telepen_Code.Add('a', "1011101110101010");
			Telepen.Telepen_Code.Add('b', "1110001110101010");
			Telepen.Telepen_Code.Add('c', "1010111010001000");
			Telepen.Telepen_Code.Add('d', "1110101110101010");
			Telepen.Telepen_Code.Add('e', "1011100010001000");
			Telepen.Telepen_Code.Add('f', "1000100010001000");
			Telepen.Telepen_Code.Add('g', "1010101110101010");
			Telepen.Telepen_Code.Add('h', "1110111000101010");
			Telepen.Telepen_Code.Add('i', "1011101010001000");
			Telepen.Telepen_Code.Add('j', "1110001010001000");
			Telepen.Telepen_Code.Add('k', "1010111000101010");
			Telepen.Telepen_Code.Add('l', "1110101010001000");
			Telepen.Telepen_Code.Add('m', "1010001000101010");
			Telepen.Telepen_Code.Add('n', "1000101000101010");
			Telepen.Telepen_Code.Add('o', "1010101010001000");
			Telepen.Telepen_Code.Add('p', "1110111010101010");
			Telepen.Telepen_Code.Add('q', "1011101000101000");
			Telepen.Telepen_Code.Add('r', "1110001000101000");
			Telepen.Telepen_Code.Add('s', "1010111010101010");
			Telepen.Telepen_Code.Add('t', "1110101000101000");
			Telepen.Telepen_Code.Add('u', "1011100010101010");
			Telepen.Telepen_Code.Add('v', "1000100010101010");
			Telepen.Telepen_Code.Add('w', "1010101000101000");
			Telepen.Telepen_Code.Add('x', "1110100010101000");
			Telepen.Telepen_Code.Add('y', "1011101010101010");
			Telepen.Telepen_Code.Add('z', "1110001010101010");
			Telepen.Telepen_Code.Add('{', "1010100010101000");
			Telepen.Telepen_Code.Add('|', "1110101010101010");
			Telepen.Telepen_Code.Add('}', "1010001010101000");
			Telepen.Telepen_Code.Add('~', "1000101010101000");
			Telepen.Telepen_Code.Add(Convert.ToChar(127), "1010101010101010");
			Telepen.Telepen_Code.Add(Telepen.StartStopCode.START1, "1010101010111000");
			Telepen.Telepen_Code.Add(Telepen.StartStopCode.STOP1, "1110001010101010");
			Telepen.Telepen_Code.Add(Telepen.StartStopCode.START2, "1010101011101000");
			Telepen.Telepen_Code.Add(Telepen.StartStopCode.STOP2, "1110100010101010");
			Telepen.Telepen_Code.Add(Telepen.StartStopCode.START3, "1010101110101000");
			Telepen.Telepen_Code.Add(Telepen.StartStopCode.STOP3, "1110101000101010");
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00009C9F File Offset: 0x00007E9F
		public string Encoded_Value
		{
			get
			{
				return this.Encode_Telepen();
			}
		}

		// Token: 0x04000062 RID: 98
		private static Hashtable Telepen_Code = new Hashtable();

		// Token: 0x04000063 RID: 99
		private Telepen.StartStopCode StartCode;

		// Token: 0x04000064 RID: 100
		private Telepen.StartStopCode StopCode = Telepen.StartStopCode.STOP1;

		// Token: 0x04000065 RID: 101
		private int SwitchModeIndex;

		// Token: 0x04000066 RID: 102
		private int iCheckSum;

		// Token: 0x02000028 RID: 40
		private enum StartStopCode
		{
			// Token: 0x040000A9 RID: 169
			START1,
			// Token: 0x040000AA RID: 170
			STOP1,
			// Token: 0x040000AB RID: 171
			START2,
			// Token: 0x040000AC RID: 172
			STOP2,
			// Token: 0x040000AD RID: 173
			START3,
			// Token: 0x040000AE RID: 174
			STOP3
		}
	}
}
