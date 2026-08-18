using System;
using System.Collections.Generic;
using System.Text;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020003D1 RID: 977
[spr\u2400(FormulaToken.tFunctionVar3)]
[CLSCompliant(false)]
[spr\u2400(FormulaToken.tFunctionVar1)]
[spr\u2400(FormulaToken.tFunctionVar2)]
internal class spr\u231A : spr\u1B43
{
	// Token: 0x06003B34 RID: 15156 RVA: 0x00212CE8 File Offset: 0x00211CE8
	public spr\u231A(DataProvider A_0, int A_1, ExcelVersion A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06003B35 RID: 15157 RVA: 0x00212D00 File Offset: 0x00211D00
	public spr\u231A(ExcelFunction A_0) : base(A_0)
	{
		this.TokenCode = FormulaToken.tFunctionVar2;
	}

	// Token: 0x06003B36 RID: 15158 RVA: 0x00212D1C File Offset: 0x00211D1C
	public spr\u231A(string A_0) : base(A_0)
	{
		this.TokenCode = FormulaToken.tFunctionVar2;
	}

	// Token: 0x06003B37 RID: 15159 RVA: 0x00212D38 File Offset: 0x00211D38
	public spr\u231A()
	{
		this.TokenCode = FormulaToken.tFunctionVar2;
	}

	// Token: 0x06003B38 RID: 15160 RVA: 0x00212D54 File Offset: 0x00211D54
	public override int ᜁ(ExcelVersion A_0)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return base.ᜁ(A_0) + 1;
	}

	// Token: 0x06003B39 RID: 15161 RVA: 0x00212D98 File Offset: 0x00211D98
	public override string[] ᜀ(string A_0, ref int A_1, FormulaUtil A_2)
	{
		string[] array;
		for (;;)
		{
			IL_18:
			array = base.ᜀ(A_0, ref A_1, false, A_2);
			for (;;)
			{
				IL_23:
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_23;
						}
						if (false)
						{
						}
						base.ᜀ((byte)array.Length);
						num = 1;
						continue;
					case 1:
						return array;
					case 2:
						if (true)
						{
						}
						if (base.ᜑ() != ExcelFunction.CustomFunction)
						{
							num = 0;
							continue;
						}
						base.ᜀ((byte)(array.Length + 1));
						num = 3;
						continue;
					case 3:
						return array;
					}
					goto IL_18;
				}
			}
		}
		return array;
	}

	// Token: 0x06003B3A RID: 15162 RVA: 0x00212E48 File Offset: 0x00211E48
	public override byte[] ᜀ(ExcelVersion A_0)
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		byte[] array = base.ᜀ(A_0);
		array[1] = base.ᜐ();
		BitConverter.GetBytes((ushort)base.ᜑ()).CopyTo(array, 2);
		return array;
	}

	// Token: 0x06003B3B RID: 15163 RVA: 0x00212EA8 File Offset: 0x00211EA8
	public override void ᜀ(FormulaUtil A_0, Stack<object> A_1, bool A_2)
	{
		int a_ = 4;
		switch (0)
		{
		default:
		{
			int num = 11;
			string text;
			StringBuilder stringBuilder;
			for (;;)
			{
				int num3;
				switch (num)
				{
				case 0:
				{
					int length;
					if (text[length - 1] == '\'')
					{
						num = 1;
						continue;
					}
					goto IL_185;
				}
				case 1:
				{
					int length;
					int num2 = text.LastIndexOf('\'', length - 2);
					num = 3;
					continue;
				}
				case 2:
					goto IL_137;
				case 3:
				{
					int num2;
					if (num2 >= 0)
					{
						num = 13;
						continue;
					}
					goto IL_185;
				}
				case 4:
					goto IL_236;
				case 5:
					if (A_1.Count < (int)base.ᜐ())
					{
						num = 9;
						continue;
					}
					num = 7;
					continue;
				case 6:
					goto IL_80;
				case 7:
					if (base.ᜑ() == ExcelFunction.CustomFunction)
					{
						num = 14;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_236;
					default:
						goto IL_2BB;
					}
					break;
				case 8:
				{
					int num4;
					if (num3 > num4)
					{
						num = 16;
						continue;
					}
					string value = (string)A_1.Pop();
					stringBuilder.Insert(1, value);
					num = 15;
					continue;
				}
				case 9:
					goto IL_162;
				case 10:
				{
					string operandsSeparator;
					stringBuilder.Insert(1, operandsSeparator);
					num = 4;
					continue;
				}
				case 12:
					if (true)
					{
					}
					goto IL_164;
				case 13:
				{
					int length;
					int num2;
					text = text.Substring(num2 + 1, length - num2 - 2);
					num = 2;
					continue;
				}
				case 14:
				{
					string operandsSeparator = A_0.OperandsSeparator;
					stringBuilder = new StringBuilder();
					stringBuilder.Append(RecordTableEnumerator.b("ሹ", a_));
					num3 = 1;
					int num4 = (int)(base.ᜐ() - 1);
					num = 17;
					continue;
				}
				case 15:
				{
					int num4;
					if (num3 != num4)
					{
						num = 10;
						continue;
					}
					goto IL_236;
				}
				case 16:
				{
					stringBuilder.Append(RecordTableEnumerator.b("ጹ", a_));
					text = (string)A_1.Pop();
					int length = text.Length;
					num = 0;
					continue;
				}
				case 17:
					goto IL_164;
				}
				if (A_1 == null)
				{
					num = 6;
					continue;
				}
				num = 5;
				continue;
				IL_164:
				num = 8;
				continue;
				IL_236:
				num3++;
				num = 12;
			}
			IL_80:
			throw new ArgumentNullException(RecordTableEnumerator.b("唹䰻嬽㈿⍁⩃≅㭇", a_));
			IL_137:
			goto IL_185;
			IL_162:
			throw new ArgumentException(RecordTableEnumerator.b("琹医䨽怿❁⩃⥅㵇ⵉ⑋湍㕏㹑ㅓ㭕㵗㑙⡛ⵝ䁟ୡ੣䙥᭧ṩ൫൭᭯", a_));
			IL_185:
			stringBuilder.Insert(0, text);
			string item = stringBuilder.ToString();
			A_1.Push(item);
			return;
			IL_2BB:
			if (false)
			{
			}
			base.ᜀ(A_0, A_1, A_2);
			return;
		}
		}
	}

	// Token: 0x06003B3C RID: 15164 RVA: 0x00213180 File Offset: 0x00212180
	public new static FormulaToken ᜀ(int A_0)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return Ptg.IndexToCode(FormulaToken.tFunctionVar1, A_0);
	}

	// Token: 0x06003B3D RID: 15165 RVA: 0x002131C4 File Offset: 0x002121C4
	public override void ᜀ(DataProvider A_0, ref int A_1, ExcelVersion A_2)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		base.ᜀ(A_0.ReadByte(A_1++));
		base.ᜀ((ExcelFunction)A_0.ReadUInt16(A_1));
		A_1 += 2;
	}
}
