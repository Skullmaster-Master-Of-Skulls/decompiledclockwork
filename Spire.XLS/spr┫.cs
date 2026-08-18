using System;
using System.Drawing;
using System.Globalization;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000482 RID: 1154
[CLSCompliant(false)]
[spr\u2400(FormulaToken.tExp)]
[spr\u2400(FormulaToken.tTbl)]
internal class spr\u252B : sprᦊ
{
	// Token: 0x060046C9 RID: 18121 RVA: 0x002AE6F4 File Offset: 0x002AD6F4
	public spr\u252B()
	{
	}

	// Token: 0x060046CA RID: 18122 RVA: 0x002AE708 File Offset: 0x002AD708
	public spr\u252B(DataProvider A_0, int A_1, ExcelVersion A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x060046CB RID: 18123 RVA: 0x002AE720 File Offset: 0x002AD720
	public spr\u252B(int A_0, int A_1)
	{
		this.ᜂ(A_0);
		this.ᜃ(A_1);
	}

	// Token: 0x060046CC RID: 18124 RVA: 0x002AE744 File Offset: 0x002AD744
	public override bool ᜅ()
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
		return true;
	}

	// Token: 0x060046CD RID: 18125 RVA: 0x002AE780 File Offset: 0x002AD780
	public override void ᜁ(bool A_0)
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

	// Token: 0x060046CE RID: 18126 RVA: 0x002AE7C0 File Offset: 0x002AD7C0
	public override bool ᜃ()
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
		return true;
	}

	// Token: 0x060046CF RID: 18127 RVA: 0x002AE7FC File Offset: 0x002AD7FC
	public override void ᜀ(bool A_0)
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
		throw new NotSupportedException();
	}

	// Token: 0x060046D0 RID: 18128 RVA: 0x002AE83C File Offset: 0x002AD83C
	public override int ᜁ(ExcelVersion A_0)
	{
		int a_ = 12;
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
					case ExcelVersion.Version97to2003:
						return 5;
					case ExcelVersion.Version2007:
					case ExcelVersion.Version2010:
						goto IL_43;
					default:
						num = 2;
						continue;
					}
					break;
				case 1:
					goto IL_74;
				case 2:
					num = 1;
					continue;
				}
				break;
			}
		}
		IL_43:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_74:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㑁⅃㑅㭇⍉⍋⁍", a_));
		default:
			if (false)
			{
			}
			if (true)
			{
			}
			return 9;
		}
		return 5;
	}

	// Token: 0x060046D1 RID: 18129 RVA: 0x002AE8DC File Offset: 0x002AD8DC
	public override string ᜀ(FormulaUtil A_0, int A_1, int A_2, bool A_3, NumberFormatInfo A_4, bool A_5)
	{
		int a_ = 0;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return RecordTableEnumerator.b("ḵᠷ礹医倽㐿ぁ⭃⩅᱇╉❋⭍㹏牑", a_) + sprṔ.ᜂ(this.ᜆ() + 1, this.ᜇ() + 1) + RecordTableEnumerator.b("ἵ", a_);
	}

	// Token: 0x060046D2 RID: 18130 RVA: 0x002AE958 File Offset: 0x002AD958
	public override byte[] ᜀ(ExcelVersion A_0)
	{
		int a_ = 13;
		byte[] array;
		for (;;)
		{
			array = new byte[this.GetSize(A_0)];
			array[0] = (byte)this.TokenCode;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_107;
			default:
			{
				if (true)
				{
				}
				if (false)
				{
				}
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch (A_0)
						{
						case ExcelVersion.Version97to2003:
							BitConverter.GetBytes((ushort)this.ᜇ()).CopyTo(array, 1);
							BitConverter.GetBytes((ushort)this.ᜆ()).CopyTo(array, 3);
							num = 4;
							continue;
						case ExcelVersion.Version2007:
						case ExcelVersion.Version2010:
						{
							int num2 = 1;
							BitConverter.GetBytes(this.ᜇ()).CopyTo(array, num2);
							num2 += 4;
							BitConverter.GetBytes(this.ᜆ()).CopyTo(array, num2);
							num = 1;
							continue;
						}
						default:
							num = 3;
							continue;
						}
						break;
					case 1:
						goto IL_BB;
					case 2:
						goto IL_FB;
					case 3:
						num = 2;
						continue;
					case 4:
						goto IL_EE;
					}
					break;
				}
				break;
			}
			}
		}
		IL_BB:
		IL_EE:
		return array;
		IL_FB:
		IL_107:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㕂⁄㕆㩈≊≌ⅎ", a_));
	}

	// Token: 0x060046D3 RID: 18131 RVA: 0x002AEA84 File Offset: 0x002ADA84
	public override FormulaToken ᜂ()
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
		return FormulaToken.tRef2;
	}

	// Token: 0x060046D4 RID: 18132 RVA: 0x002AEAC4 File Offset: 0x002ADAC4
	protected override Ptg ᜀ(sprᦊ A_0, int A_1, Rectangle A_2, int A_3, int A_4, int A_5, XlsWorkbook A_6)
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
		return this.Offset(A_4, A_5, A_6);
	}

	// Token: 0x060046D5 RID: 18133 RVA: 0x002AEB0C File Offset: 0x002ADB0C
	public override void ᜀ(DataProvider A_0, ref int A_1, ExcelVersion A_2)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 6;
				continue;
			case 1:
				goto IL_C0;
			case 3:
				return;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_E8;
				default:
					goto IL_4D;
				}
				break;
			case 5:
				if (A_2 != ExcelVersion.Version2007)
				{
					num = 0;
					continue;
				}
				goto IL_C0;
			case 6:
				if (A_2 == ExcelVersion.Version2010)
				{
					num = 1;
					continue;
				}
				return;
			}
			if (A_2 == ExcelVersion.Version97to2003)
			{
				num = 4;
				continue;
			}
			num = 5;
			continue;
			IL_E8:
			num = 3;
			continue;
			IL_C0:
			this.ᜂ(A_0.ReadInt32(A_1));
			A_1 += 4;
			this.ᜃ(A_0.ReadInt32(A_1));
			A_1 += 4;
			goto IL_E8;
		}
		IL_4D:
		if (false)
		{
		}
		if (true)
		{
		}
		this.ᜂ((int)A_0.ReadUInt16(A_1));
		A_1 += 2;
		this.ᜃ((int)((byte)A_0.ReadUInt16(A_1)));
		A_1 += 2;
	}
}
