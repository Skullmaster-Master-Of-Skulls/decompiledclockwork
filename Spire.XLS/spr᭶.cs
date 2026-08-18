using System;
using System.Drawing;
using System.Globalization;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020004A9 RID: 1193
[spr\u2400(FormulaToken.tNameX1)]
[CLSCompliant(false)]
[spr\u2400(FormulaToken.tNameX3)]
[spr\u2400(FormulaToken.tNameX2)]
internal class spr\u1B76 : Ptg, spr\u2086, sprỜ
{
	// Token: 0x060049B5 RID: 18869 RVA: 0x002CB3DC File Offset: 0x002CA3DC
	public spr\u1B76()
	{
	}

	// Token: 0x060049B6 RID: 18870 RVA: 0x002CB3F0 File Offset: 0x002CA3F0
	public spr\u1B76(DataProvider A_0, int A_1, ExcelVersion A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x060049B7 RID: 18871 RVA: 0x002CB408 File Offset: 0x002CA408
	public spr\u1B76(string A_0, IWorkbook A_1)
	{
		int a_ = 6;
		base..ctor();
		INamedRange namedRange = A_1.Names[A_0];
		if (namedRange == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("礻䘽㐿❁㙃⡅桇⑉ⵋ⍍㕏牑", a_) + A_0 + RecordTableEnumerator.b("᰻娽⼿❁㝃晅♇╉㡋湍㕏⩑㵓╕ⱗ", a_));
		}
		this.ᜁ = (ushort)(namedRange.Index + 1);
		Ptg ptg = ((XlsName)namedRange).Record.ᜈ()[0];
		if (ptg is spr\u1BFD)
		{
			this.ᜀ = ((spr\u1BFD)ptg).ᜆ();
			return;
		}
		if (ptg is sprᣋ)
		{
			this.ᜀ = ((sprᣋ)ptg).ᜁ();
		}
	}

	// Token: 0x060049B8 RID: 18872 RVA: 0x002CB4C0 File Offset: 0x002CA4C0
	public spr\u1B76(int A_0, int A_1)
	{
		this.ᜀ = (ushort)A_0;
		this.ᜁ = (ushort)(A_1 + 1);
	}

	// Token: 0x060049B9 RID: 18873 RVA: 0x002CB4E8 File Offset: 0x002CA4E8
	public ushort ᜂ()
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

	// Token: 0x060049BA RID: 18874 RVA: 0x002CB52C File Offset: 0x002CA52C
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
		this.ᜁ = A_0;
	}

	// Token: 0x060049BB RID: 18875 RVA: 0x002CB570 File Offset: 0x002CA570
	public ushort ᜃ()
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
		return this.ᜀ;
	}

	// Token: 0x060049BC RID: 18876 RVA: 0x002CB5B4 File Offset: 0x002CA5B4
	public void ᜁ(ushort A_0)
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

	// Token: 0x060049BD RID: 18877 RVA: 0x002CB5F8 File Offset: 0x002CA5F8
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
		return 7;
	}

	// Token: 0x060049BE RID: 18878 RVA: 0x002CB634 File Offset: 0x002CA634
	public virtual string ᜀ(FormulaUtil A_0, int A_1, int A_2, bool A_3, NumberFormatInfo A_4, bool A_5)
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
		return this.ToString(A_0, A_1, A_2, A_3, A_4, A_5, null);
	}

	// Token: 0x060049BF RID: 18879 RVA: 0x002CB674 File Offset: 0x002CA674
	public virtual string ᜀ(FormulaUtil A_0, int A_1, int A_2, bool A_3, NumberFormatInfo A_4, bool A_5, IWorksheet A_6)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 5;
			IWorksheet worksheet;
			XlsExternWorkbook xlsExternWorkbook;
			INamedRange namedRange;
			sprἉ sprἉ;
			for (;;)
			{
				XlsWorkbook xlsWorkbook;
				switch (num)
				{
				case 0:
					goto IL_270;
				case 1:
					num = 9;
					continue;
				case 2:
					num = 16;
					continue;
				case 3:
					goto IL_80;
				case 4:
					goto IL_262;
				case 6:
					if (worksheet != A_6)
					{
						num = 2;
						continue;
					}
					goto IL_29C;
				case 7:
					goto IL_220;
				case 8:
					num = 0;
					continue;
				case 9:
					if (true)
					{
					}
					if ((A_0.ParentWorkbook as XlsWorkbook).InnerNamesColection.ᜊ() > (int)(this.ᜁ - 1))
					{
						num = 11;
						continue;
					}
					goto IL_2A3;
				case 10:
					if (xlsExternWorkbook.Workbook.Version == ExcelVersion.Version97to2003 | xlsExternWorkbook.Workbook.Version == ExcelVersion.Version2007)
					{
						num = 12;
						continue;
					}
					num = 17;
					continue;
				case 11:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_270;
					default:
						if (false)
						{
						}
						num = 13;
						continue;
					}
					break;
				case 12:
					goto IL_EF;
				case 13:
					if (this.ᜁ < 1)
					{
						num = 4;
						continue;
					}
					namedRange = (A_0.ParentWorkbook as XlsWorkbook).InnerNamesColection.ᜁ((int)(this.ᜁ - 1));
					worksheet = namedRange.Worksheet;
					num = 6;
					continue;
				case 14:
					goto IL_297;
				case 15:
				{
					if (xlsWorkbook.IsLocalReference((int)this.ᜀ))
					{
						num = 1;
						continue;
					}
					int bookIndex = xlsWorkbook.GetBookIndex((int)this.ᜀ);
					xlsExternWorkbook = xlsWorkbook.ExternWorkbooks[bookIndex];
					sprἉ = xlsExternWorkbook.ExternNames.ᜀ((int)(this.ᜁ - 1));
					num = 10;
					continue;
				}
				case 16:
					if (worksheet != null)
					{
						num = 7;
						continue;
					}
					goto IL_29C;
				case 17:
					if (xlsExternWorkbook.IsAddInFunctions)
					{
						num = 8;
						continue;
					}
					goto IL_31A;
				}
				if (A_0 == null)
				{
					num = 3;
					continue;
				}
				xlsWorkbook = (XlsWorkbook)A_0.ParentWorkbook;
				num = 15;
				continue;
				IL_270:
				if (xlsExternWorkbook.Worksheets.Count != 0)
				{
					goto IL_31A;
				}
				num = 14;
			}
			IL_80:
			return string.Format(RecordTableEnumerator.b("湅桇ཉ㑋㩍㕏⁑㩓ᡕ㥗㝙㥛᝝๟١ţṥ䡧坩䱫ᕭ䁯ཱ塳噵⩷ό᩻㝽ﺅꢇ랉겋ꆏ뒓뾕", a_), this.ᜁ, this.ᜀ);
			IL_EF:
			return string.Format(RecordTableEnumerator.b("慅㍇穉ㅋ楍煏畑⽓杕╗絙", a_), xlsExternWorkbook.URL, sprἉ.ᜃ());
			IL_220:
			return string.Format(RecordTableEnumerator.b("慅㍇穉ㅋ楍煏⥑敓⭕", a_), worksheet.Name, namedRange.Name);
			IL_262:
			goto IL_2A3;
			IL_297:
			return string.Format(RecordTableEnumerator.b("慅㍇穉ㅋ楍煏畑⽓杕╗絙", a_), xlsExternWorkbook.URL, sprἉ.ᜃ());
			IL_29C:
			return namedRange.Name;
			IL_2A3:
			throw new spr\u2313();
			IL_31A:
			return string.Format(RecordTableEnumerator.b("㵅硇㝉测㕍慏⽑", a_), xlsExternWorkbook.URL, sprἉ.ᜃ());
		}
		}
	}

	// Token: 0x060049C0 RID: 18880 RVA: 0x002CB9BC File Offset: 0x002CA9BC
	public string ᜀ(FormulaUtil A_0, int A_1, int A_2, bool A_3)
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
		return this.ToString(A_0, A_1, A_2, A_3);
	}

	// Token: 0x060049C1 RID: 18881 RVA: 0x002CBA04 File Offset: 0x002CAA04
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
		BitConverter.GetBytes(this.ᜁ).CopyTo(array, 3);
		return array;
	}

	// Token: 0x060049C2 RID: 18882 RVA: 0x002CBA6C File Offset: 0x002CAA6C
	public static FormulaToken ᜀ(int A_0)
	{
		int a_ = 2;
		for (;;)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch (A_0)
					{
					case 1:
						return FormulaToken.tNameX1;
					case 2:
						return FormulaToken.tNameX2;
					case 3:
						goto IL_55;
					default:
						num = 2;
						continue;
					}
					break;
				case 1:
					goto IL_53;
				case 2:
					num = 1;
					continue;
				}
				break;
			}
		}
		return FormulaToken.tNameX2;
		IL_53:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("儷吹堻嬽㠿", a_));
		IL_55:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_53;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			return FormulaToken.tNameX3;
		}
	}

	// Token: 0x060049C3 RID: 18883 RVA: 0x002CBB14 File Offset: 0x002CAB14
	public IXLSRange ᜀ(IWorkbook A_0, IWorksheet A_1)
	{
		int a_ = 15;
		if (true)
		{
		}
		if (A_0 == null)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_2C;
				}
			}
			IL_2C:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("❄⡆♈⁊", a_));
		}
		XlsWorkbook xlsWorkbook = (XlsWorkbook)A_0;
		xlsWorkbook.CheckForInternalReference((int)this.ᜃ());
		return (XlsName)xlsWorkbook.Names[(int)(this.ᜂ() - 1)];
	}

	// Token: 0x060049C4 RID: 18884 RVA: 0x002CBBA0 File Offset: 0x002CABA0
	public Rectangle ᜀ()
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

	// Token: 0x060049C5 RID: 18885 RVA: 0x002CBBE0 File Offset: 0x002CABE0
	public Ptg ᜀ(Rectangle A_0)
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

	// Token: 0x060049C6 RID: 18886 RVA: 0x002CBC20 File Offset: 0x002CAC20
	public virtual void ᜀ(DataProvider A_0, ref int A_1, ExcelVersion A_2)
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
		this.ᜁ = A_0.ReadUInt16(A_1);
		A_1 += this.GetSize(A_2) - 3;
	}

	// Token: 0x04002174 RID: 8564
	private ushort ᜀ;

	// Token: 0x04002175 RID: 8565
	private ushort ᜁ;
}
