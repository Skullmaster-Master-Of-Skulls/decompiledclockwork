using System;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000485 RID: 1157
[CLSCompliant(false)]
[spr\u2400(FormulaToken.tArea3d1)]
[spr\u2400(FormulaToken.tArea3d3)]
[spr\u2400(FormulaToken.tArea3d2)]
internal class spr\u1BFD : sprᲔ, spr\u2086, sprỜ
{
	// Token: 0x06004725 RID: 18213 RVA: 0x002B3328 File Offset: 0x002B2328
	public spr\u1BFD()
	{
	}

	// Token: 0x06004726 RID: 18214 RVA: 0x002B333C File Offset: 0x002B233C
	public spr\u1BFD(DataProvider A_0, int A_1, ExcelVersion A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06004727 RID: 18215 RVA: 0x002B3354 File Offset: 0x002B2354
	public spr\u1BFD(string A_0, IWorkbook A_1)
	{
		int a_ = 18;
		base..ctor();
		Match match = FormulaUtil.CellRange3DRegex.Match(A_0);
		if (match.Success)
		{
			if (match.Value == A_0)
			{
				goto IL_B4;
			}
		}
		match = FormulaUtil.CellRange3DRegex2.Match(A_0);
		if (match.Success && match.Value == A_0)
		{
			if (match.Groups[RecordTableEnumerator.b("ᭇ≉⥋⭍⑏᱑㕓㭕㵗", a_)].Value == match.Groups[RecordTableEnumerator.b("ᭇ≉⥋⭍⑏᱑㕓㭕㵗桙", a_)].Value)
			{
				goto IL_B4;
			}
		}
		throw new ArgumentException(RecordTableEnumerator.b("ه╉㡋湍♏㍑㡓㽕㱗穙㵛ⱝ՟͡䑣啥Ⱨ䩩Ὣᩭɯ᭱ᩳᅵ噷", a_));
		IL_B4:
		this.ᜀ(match, A_1);
	}

	// Token: 0x06004728 RID: 18216 RVA: 0x002B3420 File Offset: 0x002B2420
	public spr\u1BFD(spr\u1BFD A_0) : base(A_0)
	{
		this.ᜀ = A_0.ᜀ;
	}

	// Token: 0x06004729 RID: 18217 RVA: 0x002B3440 File Offset: 0x002B2440
	public spr\u1BFD(int A_0, int A_1, int A_2, int A_3, int A_4, byte A_5, byte A_6) : base(A_1, A_2, A_3, A_4, A_5, A_6)
	{
		this.ᜀ = (ushort)A_0;
	}

	// Token: 0x0600472A RID: 18218 RVA: 0x002B3468 File Offset: 0x002B2468
	public spr\u1BFD(int A_0, int A_1, int A_2, string A_3, string A_4, string A_5, string A_6, bool A_7, IWorkbook A_8) : base(A_0, A_1, A_3, A_4, A_5, A_6, A_7, A_8)
	{
		this.ᜀ = (ushort)A_2;
	}

	// Token: 0x0600472B RID: 18219 RVA: 0x002B3494 File Offset: 0x002B2494
	public ushort ᜆ()
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
		return this.ᜀ;
	}

	// Token: 0x0600472C RID: 18220 RVA: 0x002B34D8 File Offset: 0x002B24D8
	public void ᜀ(ushort A_0)
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
		this.ᜀ = A_0;
	}

	// Token: 0x0600472D RID: 18221 RVA: 0x002B351C File Offset: 0x002B251C
	public override int ᜁ(ExcelVersion A_0)
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
		return base.ᜁ(A_0) + 2;
	}

	// Token: 0x0600472E RID: 18222 RVA: 0x002B3560 File Offset: 0x002B2560
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
		Buffer.BlockCopy(array, 1, array, 3, array.Length - 3);
		BitConverter.GetBytes(this.ᜀ).CopyTo(array, 1);
		return array;
	}

	// Token: 0x0600472F RID: 18223 RVA: 0x002B35C4 File Offset: 0x002B25C4
	public override string ᜀ(FormulaUtil A_0, int A_1, int A_2, bool A_3, NumberFormatInfo A_4, bool A_5)
	{
		int a_ = 14;
		string text;
		string str;
		for (;;)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_13F:
				text = string.Empty;
				num = 6;
				break;
			default:
				if (false)
				{
				}
				str = base.ᜀ(A_0, A_1, A_2, A_3, A_4, A_5);
				num = 0;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0 == null)
					{
						num = 1;
						continue;
					}
					text = ((XlsWorkbook)A_0.ParentWorkbook).GetSheetNameByReference((int)this.ᜀ, false);
					num = 2;
					continue;
				case 1:
					text = RecordTableEnumerator.b("ὃᑅⵇⱉ⥋㱍㕏㱑㝓㍕ᅗ㑙㡛㭝ᡟ䉡奣䙥", a_) + this.ᜀ + RecordTableEnumerator.b("摃ᭅ桇", a_);
					num = 5;
					continue;
				case 2:
					if (text != null)
					{
						num = 3;
						continue;
					}
					goto IL_13F;
				case 3:
					if (true)
					{
					}
					text = text.Replace(RecordTableEnumerator.b("捃", a_), RecordTableEnumerator.b("捃慅", a_));
					text = RecordTableEnumerator.b("捃", a_) + text + RecordTableEnumerator.b("捃杅", a_);
					num = 4;
					continue;
				case 4:
					goto IL_C2;
				case 5:
					goto IL_13D;
				case 6:
					goto IL_15A;
				}
				break;
			}
		}
		IL_C2:
		IL_13D:
		IL_15A:
		return text + str;
	}

	// Token: 0x06004730 RID: 18224 RVA: 0x002B3734 File Offset: 0x002B2734
	public override int ᜀ()
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
		return spr\u1BFD.ᜀ(this.TokenCode);
	}

	// Token: 0x06004731 RID: 18225 RVA: 0x002B377C File Offset: 0x002B277C
	public override FormulaToken ᜅ()
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
		int a_ = this.ᜀ();
		return sprខ.ᜀ(a_);
	}

	// Token: 0x06004732 RID: 18226 RVA: 0x002B37C4 File Offset: 0x002B27C4
	public override Ptg ᜀ(int A_0, int A_1, int A_2, int A_3, Rectangle A_4, int A_5, Rectangle A_6, out bool A_7, XlsWorkbook A_8)
	{
		spr\u1BFD spr_u1BFD;
		for (;;)
		{
			IL_1C:
			A_7 = false;
			for (;;)
			{
				IL_2A:
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜀ == (ushort)A_3)
						{
							num = 4;
							continue;
						}
						goto IL_B9;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2A;
						default:
							if (false)
							{
							}
							spr_u1BFD.ᜀ = (ushort)A_5;
							num = 3;
							continue;
						}
						break;
					case 2:
						if (A_7)
						{
							num = 1;
							continue;
						}
						return spr_u1BFD;
					case 3:
						goto IL_5A;
					case 4:
						spr_u1BFD = (spr\u1BFD)base.ᜀ(A_5, A_1, A_2, A_5, A_4, A_5, A_6, out A_7, A_8);
						if (true)
						{
						}
						num = 2;
						continue;
					}
					goto IL_1C;
				}
			}
		}
		return spr_u1BFD;
		IL_5A:
		return spr_u1BFD;
		IL_B9:
		return (Ptg)base.Clone();
	}

	// Token: 0x06004733 RID: 18227 RVA: 0x002B3898 File Offset: 0x002B2898
	public override sprᲔ ᜃ()
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
		return new sprខ(this);
	}

	// Token: 0x06004734 RID: 18228 RVA: 0x002B38DC File Offset: 0x002B28DC
	public string ᜀ(FormulaUtil A_0, int A_1, int A_2, bool A_3)
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
		return base.ToString(A_0, A_1, A_2, A_3);
	}

	// Token: 0x06004735 RID: 18229 RVA: 0x002B3924 File Offset: 0x002B2924
	protected void ᜀ(string A_0, IWorkbook A_1)
	{
		for (;;)
		{
			XlsWorkbook xlsWorkbook = (XlsWorkbook)A_1;
			int num = 2;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					if (A_0[A_0.Length - 1] == '\'')
					{
						num = 1;
						continue;
					}
					goto IL_52;
				case 1:
					IL_AC:
					goto IL_69;
				case 2:
					if (A_0[0] == '\'')
					{
						num = 4;
						continue;
					}
					goto IL_52;
				case 3:
					goto IL_52;
				case 4:
					num = 0;
					continue;
				}
				break;
				IL_69:
				A_0 = A_0.Substring(1, A_0.Length - 2);
				num = 3;
				continue;
				try
				{
					IL_52:
					this.ᜀ = (ushort)xlsWorkbook.AddSheetReference(A_0);
					goto IL_AE;
				}
				catch (ArgumentException)
				{
					throw new spr\u2313();
				}
				goto IL_69;
				IL_AE:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_AC;
				default:
					goto IL_C4;
				}
			}
		}
		IL_C4:
		if (false)
		{
		}
	}

	// Token: 0x06004736 RID: 18230 RVA: 0x002B3A0C File Offset: 0x002B2A0C
	protected void ᜀ(Match A_0, IWorkbook A_1)
	{
		int a_ = 11;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		string value = A_0.Groups[RecordTableEnumerator.b("ቀ⭂⁄≆㵈Պⱌ≎㑐", a_)].Value;
		string value2 = A_0.Groups[RecordTableEnumerator.b("ɀⱂ⥄㉆⑈╊籌", a_)].Value;
		string value3 = A_0.Groups[RecordTableEnumerator.b("ፀⱂ㉄癆", a_)].Value;
		string value4 = A_0.Groups[RecordTableEnumerator.b("ɀⱂ⥄㉆⑈╊罌", a_)].Value;
		string value5 = A_0.Groups[RecordTableEnumerator.b("ፀⱂ㉄畆", a_)].Value;
		base.ᜀ(0, 0, value3, value2, value5, value4, false, A_1);
		this.ᜀ(value, A_1);
	}

	// Token: 0x06004737 RID: 18231 RVA: 0x002B3B04 File Offset: 0x002B2B04
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
		this.ᜀ = A_0.ReadUInt16(A_1);
		A_1 += 2;
		base.ᜀ(A_0, ref A_1, A_2);
	}

	// Token: 0x06004738 RID: 18232 RVA: 0x002B3B5C File Offset: 0x002B2B5C
	public new static FormulaToken ᜀ(int A_0)
	{
		int a_ = 11;
		for (;;)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					goto IL_77;
				case 2:
					if (true)
					{
					}
					switch (A_0)
					{
					case 1:
						return FormulaToken.tArea3d1;
					case 2:
						goto IL_4D;
					case 3:
						return FormulaToken.tArea3d3;
					default:
						num = 0;
						continue;
					}
					break;
				}
				break;
			}
		}
		IL_4D:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_86:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⡀ⵂ⅄≆ㅈ", a_), RecordTableEnumerator.b("ీ㙂㙄㍆楈⥊⡌潎㵐㙒♔⑖祘⽚㕜㹞འ䍢兤䝦ࡨժ६佮ᙰŲၴᙶ൸Ṻོ彾ꦈﾊﾐ뎒ꖔ릖", a_));
		default:
			if (false)
			{
			}
			return FormulaToken.tArea3d2;
		}
		return FormulaToken.tArea3d1;
		IL_77:
		goto IL_86;
	}

	// Token: 0x06004739 RID: 18233 RVA: 0x002B3C10 File Offset: 0x002B2C10
	public new static int ᜀ(FormulaToken A_0)
	{
		int a_ = 19;
		for (;;)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_6A:
				num = 3;
				break;
			default:
				if (false)
				{
				}
				num = 2;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0 != FormulaToken.tArea3d3)
					{
						num = 1;
						continue;
					}
					return 3;
				case 1:
					num = 5;
					continue;
				case 2:
					if (A_0 != FormulaToken.tArea3d1)
					{
						num = 6;
						continue;
					}
					return 1;
				case 3:
					if (A_0 != FormulaToken.tArea3d2)
					{
						num = 4;
						continue;
					}
					goto IL_6C;
				case 4:
					num = 0;
					continue;
				case 5:
					goto IL_7E;
				case 6:
					goto IL_6A;
				}
				break;
			}
		}
		IL_6C:
		if (true)
		{
		}
		return 2;
		IL_7E:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⁈╊⥌⩎⥐", a_));
	}

	// Token: 0x0600473A RID: 18234 RVA: 0x002B3CEC File Offset: 0x002B2CEC
	public new IXLSRange ᜀ(IWorkbook A_0, IWorksheet A_1)
	{
		int a_ = 19;
		int num = 6;
		for (;;)
		{
			XlsWorkbook xlsWorkbook;
			switch (num)
			{
			case 0:
				goto IL_75;
			case 1:
			{
				IXLSRange result = A_1[base.ᜋ() + 1, base.ᜄ() + 1, base.ᜉ() + 1, base.ᜂ() + 1];
				num = 4;
				continue;
			}
			case 2:
			{
				if (A_1 != null)
				{
					num = 1;
					continue;
				}
				IXLSRange result;
				return result;
			}
			case 3:
			{
				IXLSRange result;
				return result;
			}
			case 4:
			{
				IXLSRange result;
				return result;
			}
			case 5:
			{
				if (!xlsWorkbook.IsExternalReference((int)this.ᜀ))
				{
					num = 7;
					continue;
				}
				XlsExternWorksheet a_2 = xlsWorkbook.ᜄ((int)this.ᜀ);
				IXLSRange result = new spr\u20A6(a_2, base.ᜋ() + 1, base.ᜄ() + 1, base.ᜉ() + 1, base.ᜂ() + 1);
				num = 3;
				continue;
			}
			case 6:
				if (true)
				{
				}
				break;
			case 7:
				goto IL_AE;
			}
			if (A_0 != null)
			{
				xlsWorkbook = (XlsWorkbook)A_0;
				IXLSRange result = null;
				num = 5;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				num = 0;
				continue;
			}
			IL_AE:
			A_1 = xlsWorkbook.GetSheetByReference((int)this.ᜀ, false);
			num = 2;
		}
		IL_75:
		throw new ArgumentNullException(RecordTableEnumerator.b("⭈⑊≌⑎", a_));
	}

	// Token: 0x04002054 RID: 8276
	private new ushort ᜀ;
}
