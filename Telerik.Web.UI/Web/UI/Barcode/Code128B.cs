using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020009CC RID: 2508
	internal class Code128B : Code128
	{
		// Token: 0x06006035 RID: 24629 RVA: 0x001260C4 File Offset: 0x001242C4
		[Description("Initializes a new instance of Code128B type.")]
		public Code128B()
		{
			this.charset = new List<char>();
			this.charset.Add(' ');
			this.charset.Add('!');
			this.charset.Add('"');
			this.charset.Add('#');
			this.charset.Add('$');
			this.charset.Add('%');
			this.charset.Add('&');
			this.charset.Add('\'');
			this.charset.Add('(');
			this.charset.Add(')');
			this.charset.Add('*');
			this.charset.Add('+');
			this.charset.Add(',');
			this.charset.Add('-');
			this.charset.Add('.');
			this.charset.Add('/');
			this.charset.Add('0');
			this.charset.Add('1');
			this.charset.Add('2');
			this.charset.Add('3');
			this.charset.Add('4');
			this.charset.Add('5');
			this.charset.Add('6');
			this.charset.Add('7');
			this.charset.Add('8');
			this.charset.Add('9');
			this.charset.Add(':');
			this.charset.Add(';');
			this.charset.Add('<');
			this.charset.Add('=');
			this.charset.Add('>');
			this.charset.Add('?');
			this.charset.Add('@');
			this.charset.Add('A');
			this.charset.Add('B');
			this.charset.Add('C');
			this.charset.Add('D');
			this.charset.Add('E');
			this.charset.Add('F');
			this.charset.Add('G');
			this.charset.Add('H');
			this.charset.Add('I');
			this.charset.Add('J');
			this.charset.Add('K');
			this.charset.Add('L');
			this.charset.Add('M');
			this.charset.Add('N');
			this.charset.Add('O');
			this.charset.Add('P');
			this.charset.Add('Q');
			this.charset.Add('R');
			this.charset.Add('S');
			this.charset.Add('T');
			this.charset.Add('U');
			this.charset.Add('V');
			this.charset.Add('W');
			this.charset.Add('X');
			this.charset.Add('Y');
			this.charset.Add('Z');
			this.charset.Add('[');
			this.charset.Add('\\');
			this.charset.Add(']');
			this.charset.Add('^');
			this.charset.Add('_');
			this.charset.Add('`');
			this.charset.Add('a');
			this.charset.Add('b');
			this.charset.Add('c');
			this.charset.Add('d');
			this.charset.Add('e');
			this.charset.Add('f');
			this.charset.Add('g');
			this.charset.Add('h');
			this.charset.Add('i');
			this.charset.Add('j');
			this.charset.Add('k');
			this.charset.Add('l');
			this.charset.Add('m');
			this.charset.Add('n');
			this.charset.Add('o');
			this.charset.Add('p');
			this.charset.Add('q');
			this.charset.Add('r');
			this.charset.Add('s');
			this.charset.Add('t');
			this.charset.Add('u');
			this.charset.Add('v');
			this.charset.Add('w');
			this.charset.Add('x');
			this.charset.Add('y');
			this.charset.Add('z');
			this.charset.Add('{');
			this.charset.Add('|');
			this.charset.Add('}');
			this.charset.Add('~');
			this.charset.Add('\u007f');
			this.charset.Add('ù');
			this.charset.Add('ø');
			this.charset.Add('û');
			this.charset.Add('ö');
			this.charset.Add('ú');
			this.charset.Add('ô');
			this.charset.Add('÷');
			this.charset.Add('ü');
			this.charset.Add('ý');
			this.charset.Add('þ');
			this.charset.Add('ÿ');
		}

		// Token: 0x06006036 RID: 24630 RVA: 0x00126674 File Offset: 0x00124874
		internal int GetSwitch(string value, int start, int final)
		{
			Code128C code128C = new Code128C();
			for (int i = start; i < final; i++)
			{
				if (!this.charset.Contains(value[i]))
				{
					return i;
				}
				int @switch = code128C.GetSwitch(value, i, final);
				if (@switch > i)
				{
					return i;
				}
			}
			return final;
		}

		// Token: 0x06006037 RID: 24631 RVA: 0x001266BC File Offset: 0x001248BC
		internal int[] GetIndices(string value, int start, int final)
		{
			List<int> list = new List<int>();
			if (start > 0)
			{
				list.Add(Code128B.Switch);
			}
			else
			{
				list.Add(Code128B.Prefix);
			}
			for (int i = start; i < final; i++)
			{
				if (this.charset.Contains(value[i]))
				{
					list.Add(this.charset.IndexOf(value[i]));
				}
			}
			return list.ToArray();
		}

		// Token: 0x06006038 RID: 24632 RVA: 0x00126729 File Offset: 0x00124929
		protected override int[] GetIndices(string value)
		{
			return this.GetIndices(value, 0, value.Length);
		}

		// Token: 0x04001748 RID: 5960
		private static readonly int Switch = 100;

		// Token: 0x04001749 RID: 5961
		private static readonly int Prefix = 104;

		// Token: 0x0400174A RID: 5962
		private List<char> charset;
	}
}
