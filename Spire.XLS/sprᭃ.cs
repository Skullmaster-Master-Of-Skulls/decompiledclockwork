using System;
using System.Collections.Generic;
using System.Globalization;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020003D0 RID: 976
[spr\u2400(FormulaToken.tFunction2)]
[spr\u2400(FormulaToken.tFunction3)]
[spr\u2400(FormulaToken.tFunction1)]
[CLSCompliant(false)]
internal class spr\u1B43 : sprឯ
{
	// Token: 0x06003B22 RID: 15138 RVA: 0x00212450 File Offset: 0x00211450
	public spr\u1B43()
	{
		this.TokenCode = FormulaToken.tFunction2;
	}

	// Token: 0x06003B23 RID: 15139 RVA: 0x00212478 File Offset: 0x00211478
	public spr\u1B43(DataProvider A_0, int A_1, ExcelVersion A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06003B24 RID: 15140 RVA: 0x0021249C File Offset: 0x0021149C
	public spr\u1B43(ExcelFunction A_0)
	{
		this.ᜁ = A_0;
		this.TokenCode = FormulaToken.tFunction2;
		int num;
		if (FormulaUtil.FunctionIdToParamCount.TryGetValue(A_0, out num))
		{
			this.ᜂ = (byte)num;
		}
	}

	// Token: 0x06003B25 RID: 15141 RVA: 0x002124E4 File Offset: 0x002114E4
	public spr\u1B43(string A_0) : this(FormulaUtil.FunctionAliasToId[A_0])
	{
	}

	// Token: 0x06003B26 RID: 15142 RVA: 0x00212504 File Offset: 0x00211504
	public ExcelFunction ᜑ()
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
		return this.ᜁ;
	}

	// Token: 0x06003B27 RID: 15143 RVA: 0x00212548 File Offset: 0x00211548
	public void ᜀ(ExcelFunction A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x06003B28 RID: 15144 RVA: 0x0021258C File Offset: 0x0021158C
	public byte ᜐ()
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
		return this.ᜂ;
	}

	// Token: 0x06003B29 RID: 15145 RVA: 0x002125D0 File Offset: 0x002115D0
	public void ᜀ(byte A_0)
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
		this.ᜂ = A_0;
	}

	// Token: 0x06003B2A RID: 15146 RVA: 0x00212614 File Offset: 0x00211614
	public override TOperation ᜂ()
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
		return TOperation.TYPE_FUNCTION;
	}

	// Token: 0x06003B2B RID: 15147 RVA: 0x00212650 File Offset: 0x00211650
	protected override spr\u2400[] ᜀ()
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
		return null;
	}

	// Token: 0x06003B2C RID: 15148 RVA: 0x0021268C File Offset: 0x0021168C
	protected string[] ᜀ(string A_0, ref int A_1, bool A_2, FormulaUtil A_3)
	{
		int a_ = 19;
		List<string> list;
		for (;;)
		{
			list = new List<string>();
			int num = 0;
			A_0 = A_0.Substring(A_1 + 1, A_0.Length - A_1 - 2);
			A_1 = -1;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_93:
				num2 = 8;
				break;
			default:
				if (false)
				{
				}
				num2 = 3;
				break;
			}
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_CC;
				case 1:
				{
					if (A_1 >= A_0.Length)
					{
						num2 = 2;
						continue;
					}
					string functionOperand = A_3.GetFunctionOperand(A_0, A_1);
					list.Add(functionOperand);
					A_1 += functionOperand.Length + 1;
					num++;
					num2 = 4;
					continue;
				}
				case 2:
					goto IL_141;
				case 3:
					if (true)
					{
					}
					if (A_0.Length > 0)
					{
						goto IL_93;
					}
					goto IL_141;
				case 4:
					goto IL_CE;
				case 5:
					if (A_2)
					{
						num2 = 9;
						continue;
					}
					goto IL_15F;
				case 6:
					goto IL_CE;
				case 7:
					if (num != (int)this.ᜂ)
					{
						num2 = 0;
						continue;
					}
					goto IL_15F;
				case 8:
					num2 = 6;
					continue;
				case 9:
					num2 = 7;
					continue;
				}
				break;
				IL_CE:
				num2 = 1;
				continue;
				IL_141:
				num2 = 5;
			}
		}
		IL_CC:
		throw new ArgumentException(RecordTableEnumerator.b("ᵈ⑊≌潎㱐㉒㭔⹖祘㑚⽜罞འౢᅤ䝦౨ժɬᩮᙰ᭲啴ᙶ୸ᱺࡼቾꞈ", a_));
		IL_15F:
		return list.ToArray();
	}

	// Token: 0x06003B2D RID: 15149 RVA: 0x00212800 File Offset: 0x00211800
	public static FormulaToken ᜀ(int A_0)
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
		return Ptg.IndexToCode(FormulaToken.tFunction1, A_0);
	}

	// Token: 0x06003B2E RID: 15150 RVA: 0x00212844 File Offset: 0x00211844
	public virtual int ᜁ(ExcelVersion A_0)
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
		return 3;
	}

	// Token: 0x06003B2F RID: 15151 RVA: 0x00212880 File Offset: 0x00211880
	public override string ᜀ(FormulaUtil A_0, int A_1, int A_2, bool A_3, NumberFormatInfo A_4, bool A_5)
	{
		int a_ = 7;
		int num = 2;
		string text;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_73;
			case 1:
				if (A_5)
				{
					num = 0;
					continue;
				}
				return text;
			case 3:
				if (true)
				{
				}
				goto IL_92;
			case 4:
				text = ((XLSXFunction)this.ᜁ).ToString();
				num = 3;
				continue;
			case 5:
				return text;
			case 6:
				if (FormulaUtil.ᜁ(this.ᜁ))
				{
					num = 7;
					continue;
				}
				return text;
			case 7:
				text = RecordTableEnumerator.b("戼䜾ⵀ╂⭄楆", a_) + text;
				num = 5;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_73:
				num = 6;
				continue;
			default:
				if (false)
				{
				}
				if (!FormulaUtil.FunctionIdToAlias.TryGetValue(this.ᜁ, out text))
				{
					num = 4;
					continue;
				}
				break;
			}
			IL_92:
			num = 1;
		}
		return text;
	}

	// Token: 0x06003B30 RID: 15152 RVA: 0x00212990 File Offset: 0x00211990
	public override void ᜀ(FormulaUtil A_0, Stack<object> A_1, bool A_2)
	{
		int a_ = 19;
		switch (0)
		{
		default:
		{
			int num = 1;
			string str;
			string text2;
			for (;;)
			{
				string text;
				int num2;
				string operandsSeparator;
				switch (num)
				{
				case 0:
					num = 2;
					continue;
				case 1:
					if (true)
					{
					}
					break;
				case 2:
					text = string.Empty;
					goto IL_14A;
				case 3:
					if (this.ᜂ <= 0)
					{
						num = 0;
						continue;
					}
					num = 8;
					continue;
				case 4:
					goto IL_D2;
				case 5:
					if (num2 >= (int)this.ᜂ)
					{
						num = 6;
						continue;
					}
					str = A_1.Pop().ToString() + operandsSeparator + str;
					num2++;
					num = 4;
					continue;
				case 6:
					goto IL_F3;
				case 7:
					goto IL_7A;
				case 8:
					text = A_1.Pop().ToString();
					goto IL_14A;
				case 9:
					goto IL_D2;
				}
				IL_59:
				if (A_1.Count < (int)this.ᜂ)
				{
					num = 7;
					continue;
				}
				text2 = this.ToString(A_0, 0, 0, false, null, A_2);
				FormulaUtil.PushOperandToStack(A_1, text2);
				text2 = (string)A_1.Pop();
				num = 3;
				continue;
				IL_D2:
				num = 5;
				continue;
				IL_14A:
				str = text;
				operandsSeparator = A_0.OperandsSeparator;
				num2 = 1;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_59;
				default:
					if (false)
					{
					}
					num = 9;
					break;
				}
			}
			IL_7A:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("݈⑊㥌潎㑐㵒㩔≖㹘㍚絜㹞፠Ѣၤ੦౨ժᥬᱮ彰", a_));
			IL_F3:
			string operand = text2 + RecordTableEnumerator.b("慈", a_) + str + RecordTableEnumerator.b("恈", a_);
			FormulaUtil.PushOperandToStack(A_1, operand);
			return;
		}
		}
	}

	// Token: 0x06003B31 RID: 15153 RVA: 0x00212B4C File Offset: 0x00211B4C
	public override string[] ᜀ(string A_0, ref int A_1, FormulaUtil A_2)
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
		return this.ᜀ(A_0, ref A_1, true, A_2);
	}

	// Token: 0x06003B32 RID: 15154 RVA: 0x00212B94 File Offset: 0x00211B94
	public override byte[] ᜀ(ExcelVersion A_0)
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
		byte[] array = base.ToByteArray(A_0);
		array[0] = (byte)this.TokenCode;
		BitConverter.GetBytes((ushort)this.ᜁ).CopyTo(array, 1);
		return array;
	}

	// Token: 0x06003B33 RID: 15155 RVA: 0x00212BF8 File Offset: 0x00211BF8
	public override void ᜀ(DataProvider A_0, ref int A_1, ExcelVersion A_2)
	{
		int a_ = 13;
		for (;;)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜁ = (ExcelFunction)A_0.ReadUInt16(A_1);
				num = 2;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_85;
				case 1:
					goto IL_9A;
				case 2:
				{
					string text;
					if (!FormulaUtil.FunctionIdToAlias.TryGetValue(this.ᜁ, out text))
					{
						num = 0;
						continue;
					}
					num = 4;
					continue;
				}
				case 3:
				{
					int num2;
					this.ᜂ = (byte)num2;
					num = 1;
					continue;
				}
				case 4:
				{
					int num2;
					if (FormulaUtil.FunctionIdToParamCount.TryGetValue(this.ᜁ, out num2))
					{
						num = 3;
						continue;
					}
					goto IL_DC;
				}
				}
				break;
			}
		}
		IL_85:
		throw new ArgumentNullException(RecordTableEnumerator.b("ᙂ⭄ⱆ❈⑊㩌ⅎ煐㕒⁔㥖㩘⽚㑜ぞའ", a_));
		IL_9A:
		IL_DC:
		A_1 += 2;
	}

	// Token: 0x040019B5 RID: 6581
	public new const string ᜀ = ",";

	// Token: 0x040019B6 RID: 6582
	private ExcelFunction ᜁ = ExcelFunction.NONE;

	// Token: 0x040019B7 RID: 6583
	private byte ᜂ;
}
