using System;
using System.Collections.Generic;
using System.Text;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020009D4 RID: 2516
	internal class Code93Extended : Code93
	{
		// Token: 0x06006056 RID: 24662 RVA: 0x001295A8 File Offset: 0x001277A8
		public Code93Extended()
		{
			this.encoding = new Dictionary<char, string>();
			this.encoding.Add('\0', "~U");
			this.encoding.Add('\u0001', "@A");
			this.encoding.Add('\u0002', "@B");
			this.encoding.Add('\u0003', "@C");
			this.encoding.Add('\u0004', "@D");
			this.encoding.Add('\u0005', "@E");
			this.encoding.Add('\u0006', "@F");
			this.encoding.Add('\a', "@G");
			this.encoding.Add('\b', "@H");
			this.encoding.Add('\t', "@I");
			this.encoding.Add('\n', "@J");
			this.encoding.Add('\v', "@K");
			this.encoding.Add('\f', "@L");
			this.encoding.Add('\r', "@M");
			this.encoding.Add('\u000e', "@N");
			this.encoding.Add('\u000f', "@O");
			this.encoding.Add('\u0010', "@P");
			this.encoding.Add('\u0011', "@Q");
			this.encoding.Add('\u0012', "@R");
			this.encoding.Add('\u0013', "@S");
			this.encoding.Add('\u0014', "@T");
			this.encoding.Add('\u0015', "@U");
			this.encoding.Add('\u0016', "@V");
			this.encoding.Add('\u0017', "@W");
			this.encoding.Add('\u0018', "@X");
			this.encoding.Add('\u0019', "@Y");
			this.encoding.Add('\u001a', "@Z");
			this.encoding.Add('\u001b', "~A");
			this.encoding.Add('\u001c', "~B");
			this.encoding.Add('\u001d', "~C");
			this.encoding.Add('\u001e', "~D");
			this.encoding.Add('\u001f', "~E");
			this.encoding.Add(' ', " ");
			this.encoding.Add('!', "#A");
			this.encoding.Add('"', "#B");
			this.encoding.Add('#', "#C");
			this.encoding.Add('$', "#D");
			this.encoding.Add('%', "#E");
			this.encoding.Add('&', "#F");
			this.encoding.Add('\'', "#G");
			this.encoding.Add('(', "#H");
			this.encoding.Add(')', "#I");
			this.encoding.Add('*', "#J");
			this.encoding.Add('+', "#K");
			this.encoding.Add(',', "#L");
			this.encoding.Add('-', "-");
			this.encoding.Add('.', ".");
			this.encoding.Add('/', "#O");
			this.encoding.Add('0', "0");
			this.encoding.Add('1', "1");
			this.encoding.Add('2', "2");
			this.encoding.Add('3', "3");
			this.encoding.Add('4', "4");
			this.encoding.Add('5', "5");
			this.encoding.Add('6', "6");
			this.encoding.Add('7', "7");
			this.encoding.Add('8', "8");
			this.encoding.Add('9', "9");
			this.encoding.Add(':', "#Z");
			this.encoding.Add(';', "~F");
			this.encoding.Add('<', "~G");
			this.encoding.Add('=', "~H");
			this.encoding.Add('>', "~I");
			this.encoding.Add('?', "~J");
			this.encoding.Add('@', "~V");
			this.encoding.Add('A', "A");
			this.encoding.Add('B', "B");
			this.encoding.Add('C', "C");
			this.encoding.Add('D', "D");
			this.encoding.Add('E', "E");
			this.encoding.Add('F', "F");
			this.encoding.Add('G', "G");
			this.encoding.Add('H', "H");
			this.encoding.Add('I', "I");
			this.encoding.Add('J', "J");
			this.encoding.Add('K', "K");
			this.encoding.Add('L', "L");
			this.encoding.Add('M', "M");
			this.encoding.Add('N', "N");
			this.encoding.Add('O', "O");
			this.encoding.Add('P', "P");
			this.encoding.Add('Q', "Q");
			this.encoding.Add('R', "R");
			this.encoding.Add('S', "S");
			this.encoding.Add('T', "T");
			this.encoding.Add('U', "U");
			this.encoding.Add('V', "V");
			this.encoding.Add('W', "W");
			this.encoding.Add('X', "X");
			this.encoding.Add('Y', "Y");
			this.encoding.Add('Z', "Z");
			this.encoding.Add('[', "~K");
			this.encoding.Add('\\', "~L");
			this.encoding.Add(']', "~M");
			this.encoding.Add('^', "~N");
			this.encoding.Add('_', "~O");
			this.encoding.Add('`', "~W");
			this.encoding.Add('a', "&A");
			this.encoding.Add('b', "&B");
			this.encoding.Add('c', "&C");
			this.encoding.Add('d', "&D");
			this.encoding.Add('e', "&E");
			this.encoding.Add('f', "&F");
			this.encoding.Add('g', "&G");
			this.encoding.Add('h', "&H");
			this.encoding.Add('i', "&I");
			this.encoding.Add('j', "&J");
			this.encoding.Add('k', "&K");
			this.encoding.Add('l', "&L");
			this.encoding.Add('m', "&M");
			this.encoding.Add('n', "&N");
			this.encoding.Add('o', "&O");
			this.encoding.Add('p', "&P");
			this.encoding.Add('q', "&Q");
			this.encoding.Add('r', "&R");
			this.encoding.Add('s', "&S");
			this.encoding.Add('t', "&T");
			this.encoding.Add('u', "&U");
			this.encoding.Add('v', "&V");
			this.encoding.Add('w', "&W");
			this.encoding.Add('x', "&X");
			this.encoding.Add('y', "&Y");
			this.encoding.Add('z', "&Z");
			this.encoding.Add('{', "~P");
			this.encoding.Add('|', "~Q");
			this.encoding.Add('}', "~R");
			this.encoding.Add('~', "~S");
			this.encoding.Add('\u007f', "~T");
		}

		// Token: 0x06006057 RID: 24663 RVA: 0x00129EC0 File Offset: 0x001280C0
		public override string ValidateValue(string value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in value)
			{
				if (this.IsValid(c))
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006058 RID: 24664 RVA: 0x00129F04 File Offset: 0x00128104
		internal override string GetEncoding(string value)
		{
			value = this.ValidateValue(value);
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < value.Length; i++)
			{
				stringBuilder.Append(this.encoding[value[i]]);
			}
			return base.GetEncoding(stringBuilder.ToString());
		}

		// Token: 0x06006059 RID: 24665 RVA: 0x00129F64 File Offset: 0x00128164
		private bool IsValid(char symbol)
		{
			return symbol <= '\u007f';
		}

		// Token: 0x04001760 RID: 5984
		private Dictionary<char, string> encoding;
	}
}
