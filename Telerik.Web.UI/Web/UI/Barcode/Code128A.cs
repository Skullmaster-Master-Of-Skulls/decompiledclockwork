using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020009CB RID: 2507
	internal class Code128A : Code128
	{
		// Token: 0x06006030 RID: 24624 RVA: 0x00125A44 File Offset: 0x00123C44
		[Description("Initializes a new instance of Code128A type.")]
		public Code128A()
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
			this.charset.Add('\0');
			this.charset.Add('\u0001');
			this.charset.Add('\u0002');
			this.charset.Add('\u0003');
			this.charset.Add('\u0004');
			this.charset.Add('\u0005');
			this.charset.Add('\u0006');
			this.charset.Add('\a');
			this.charset.Add('\b');
			this.charset.Add('\t');
			this.charset.Add('\n');
			this.charset.Add('\v');
			this.charset.Add('\f');
			this.charset.Add('\r');
			this.charset.Add('\u000e');
			this.charset.Add('\u000f');
			this.charset.Add('\u0010');
			this.charset.Add('\u0011');
			this.charset.Add('\u0012');
			this.charset.Add('\u0013');
			this.charset.Add('\u0014');
			this.charset.Add('\u0015');
			this.charset.Add('\u0016');
			this.charset.Add('\u0017');
			this.charset.Add('\u0018');
			this.charset.Add('\u0019');
			this.charset.Add('\u001a');
			this.charset.Add('\u001b');
			this.charset.Add('\u001c');
			this.charset.Add('\u001d');
			this.charset.Add('\u001e');
			this.charset.Add('\u001f');
			this.charset.Add('ù');
			this.charset.Add('ø');
			this.charset.Add('û');
			this.charset.Add('ö');
			this.charset.Add('õ');
			this.charset.Add('ú');
			this.charset.Add('÷');
			this.charset.Add('ü');
			this.charset.Add('ý');
			this.charset.Add('þ');
			this.charset.Add('ÿ');
		}

		// Token: 0x06006031 RID: 24625 RVA: 0x00125FEC File Offset: 0x001241EC
		internal int GetSwitch(string value, int start, int final)
		{
			for (int i = start; i < final; i++)
			{
				if (!this.charset.Contains(value[i]))
				{
					return i;
				}
				Code128C code128C = new Code128C();
				int @switch = code128C.GetSwitch(value, i, final);
				if (@switch > i)
				{
					return i;
				}
			}
			return final;
		}

		// Token: 0x06006032 RID: 24626 RVA: 0x00126034 File Offset: 0x00124234
		internal int[] GetIndices(string value, int start, int final)
		{
			List<int> list = new List<int>();
			if (start > 0)
			{
				list.Add(Code128A.Switch);
			}
			else
			{
				list.Add(Code128A.Prefix);
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

		// Token: 0x06006033 RID: 24627 RVA: 0x001260A1 File Offset: 0x001242A1
		protected override int[] GetIndices(string value)
		{
			return this.GetIndices(value, 0, value.Length);
		}

		// Token: 0x04001745 RID: 5957
		private static readonly int Switch = 101;

		// Token: 0x04001746 RID: 5958
		private static readonly int Prefix = 103;

		// Token: 0x04001747 RID: 5959
		private List<char> charset;
	}
}
