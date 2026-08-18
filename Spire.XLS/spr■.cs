using System;
using System.Drawing;
using System.Globalization;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020005A0 RID: 1440
[spr\u2400(FormulaToken.tName3)]
[spr\u2400(FormulaToken.tName2)]
[spr\u2400(FormulaToken.tName1)]
[CLSCompliant(false)]
internal class spr\u25A0 : Ptg, sprỜ
{
	// Token: 0x0600574F RID: 22351 RVA: 0x00379144 File Offset: 0x00378144
	public spr\u25A0()
	{
	}

	// Token: 0x06005750 RID: 22352 RVA: 0x00379158 File Offset: 0x00378158
	public spr\u25A0(DataProvider A_0, int A_1, ExcelVersion A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06005751 RID: 22353 RVA: 0x00379170 File Offset: 0x00378170
	public spr\u25A0(string A_0, IWorkbook A_1)
	{
		int a_ = 18;
		base..ctor();
		INamedRange namedRange = A_1.Names[A_0];
		if (namedRange == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("േ㉉㡋⭍≏㱑瑓㡕㥗㝙㥛繝", a_) + A_0 + RecordTableEnumerator.b("桇⹉⍋⭍⍏牑㩓㥕ⱗ穙㥛♝य़ᅡၣ", a_));
		}
		this.ᜀ = (ushort)(namedRange.Index + 1);
	}

	// Token: 0x06005752 RID: 22354 RVA: 0x003791D8 File Offset: 0x003781D8
	public spr\u25A0(string A_0, IWorkbook A_1, IWorksheet A_2)
	{
		int a_ = 5;
		base..ctor();
		XlsWorksheet xlsWorksheet = A_2 as XlsWorksheet;
		INamedRange namedRange;
		if (xlsWorksheet.Names.Contains(A_0))
		{
			namedRange = xlsWorksheet.Names[A_0];
		}
		else
		{
			if (!A_1.Names.Contains(A_0))
			{
				throw new ArgumentException(RecordTableEnumerator.b("渺匼吾⽀ⱂ㉄⥆楈╊ⱌ≎㑐", a_), A_0);
			}
			namedRange = A_1.Names[A_0];
		}
		this.ᜀ = (ushort)(namedRange.Index + 1);
	}

	// Token: 0x06005753 RID: 22355 RVA: 0x00379260 File Offset: 0x00378260
	public spr\u25A0(int A_0)
	{
		this.ᜀ = (ushort)(A_0 + 1);
	}

	// Token: 0x06005754 RID: 22356 RVA: 0x00379280 File Offset: 0x00378280
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
		return 5;
	}

	// Token: 0x06005755 RID: 22357 RVA: 0x003792BC File Offset: 0x003782BC
	public ushort ᜀ()
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

	// Token: 0x06005756 RID: 22358 RVA: 0x00379300 File Offset: 0x00378300
	public void ᜀ(ushort A_0)
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
		this.ᜀ = A_0;
	}

	// Token: 0x06005757 RID: 22359 RVA: 0x00379344 File Offset: 0x00378344
	public virtual string ᜂ()
	{
		int a_ = 18;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		return RecordTableEnumerator.b("恇橉ɋ⽍㵏㝑ᵓ㡕㱗㽙⑛繝嵟䉡", a_) + this.ᜀ.ToString() + RecordTableEnumerator.b("桇捉", a_);
	}

	// Token: 0x06005758 RID: 22360 RVA: 0x003793B4 File Offset: 0x003783B4
	public virtual string ᜀ(FormulaUtil A_0, int A_1, int A_2, bool A_3, NumberFormatInfo A_4, bool A_5)
	{
		int num = 4;
		sprឦ sprឦ;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				num = 1;
				continue;
			case 1:
				if (this.ᜀ < 1)
				{
					num = 3;
					continue;
				}
				goto IL_C1;
			case 2:
				if (sprឦ.ᜊ() > (int)(this.ᜀ - 1))
				{
					num = 0;
					continue;
				}
				goto IL_9F;
			case 3:
				goto IL_9F;
			case 4:
				IL_08:
				break;
			case 5:
				goto IL_3D;
			}
			if (A_0 == null)
			{
				num = 5;
				continue;
			}
			sprឦ = (A_0.ParentWorkbook.Names as sprឦ);
			num = 2;
			continue;
			IL_9F:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_08;
			default:
				goto IL_B5;
			}
		}
		IL_3D:
		return this.ToString();
		IL_B5:
		if (false)
		{
		}
		throw new spr\u2313();
		IL_C1:
		return sprឦ.ᜁ((int)(this.ᜀ - 1)).Name;
	}

	// Token: 0x06005759 RID: 22361 RVA: 0x00379498 File Offset: 0x00378498
	public virtual byte[] ᜀ(ExcelVersion A_0)
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
		BitConverter.GetBytes(this.ᜀ).CopyTo(array, 1);
		return array;
	}

	// Token: 0x0600575A RID: 22362 RVA: 0x003794F0 File Offset: 0x003784F0
	public static FormulaToken ᜀ(int A_0)
	{
		int a_ = 6;
		for (;;)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					switch (A_0)
					{
					case 1:
						return FormulaToken.tName1;
					case 2:
						return FormulaToken.tName2;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_5D;
						default:
							goto IL_7D;
						}
						break;
					default:
						num = 1;
						continue;
					}
					break;
				case 1:
					goto IL_5D;
				case 2:
					goto IL_65;
				}
				break;
				IL_5D:
				num = 2;
			}
		}
		return FormulaToken.tName2;
		IL_65:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("唻倽␿❁㱃", a_));
		IL_7D:
		if (false)
		{
		}
		return FormulaToken.tName3;
	}

	// Token: 0x0600575B RID: 22363 RVA: 0x00379598 File Offset: 0x00378598
	public IXLSRange ᜀ(IWorkbook A_0, IWorksheet A_1)
	{
		int a_ = 16;
		if (A_0 == null)
		{
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
				break;
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("⑅❇╉❋", a_));
		}
		INamedRange namedRange = A_0.Names[(int)(this.ᜀ() - 1)];
		return namedRange as IXLSRange;
	}

	// Token: 0x0600575C RID: 22364 RVA: 0x00379610 File Offset: 0x00378610
	public Rectangle ᜃ()
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
		throw new NotSupportedException();
	}

	// Token: 0x0600575D RID: 22365 RVA: 0x00379650 File Offset: 0x00378650
	public virtual void ᜀ(DataProvider A_0, ref int A_1, ExcelVersion A_2)
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
		this.ᜀ = A_0.ReadUInt16(A_1);
		A_1 += this.GetSize(A_2) - 1;
	}

	// Token: 0x0400298E RID: 10638
	private ushort ᜀ;
}
