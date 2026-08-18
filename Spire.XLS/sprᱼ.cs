using System;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000401 RID: 1025
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.LabelSST)]
internal class spr\u1C7C : spr\u22C6, ICloneable
{
	// Token: 0x06003DA0 RID: 15776 RVA: 0x002254E0 File Offset: 0x002244E0
	public new int ᜁ()
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
		return this.ᜂ;
	}

	// Token: 0x06003DA1 RID: 15777 RVA: 0x00225524 File Offset: 0x00224524
	public new void ᜀ(int A_0)
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
		this.ᜂ = A_0;
	}

	// Token: 0x06003DA2 RID: 15778 RVA: 0x00225568 File Offset: 0x00224568
	public virtual int ᜀ()
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
		return 10;
	}

	// Token: 0x06003DA3 RID: 15779 RVA: 0x002255A8 File Offset: 0x002245A8
	public virtual int ᜂ()
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
		return 10;
	}

	// Token: 0x06003DA4 RID: 15780 RVA: 0x002255E8 File Offset: 0x002245E8
	public virtual int ᜃ()
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
		return 10;
	}

	// Token: 0x06003DA6 RID: 15782 RVA: 0x0022563C File Offset: 0x0022463C
	protected override void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
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
		this.ᜂ = A_0.ReadInt32(A_1);
	}

	// Token: 0x06003DA7 RID: 15783 RVA: 0x00225684 File Offset: 0x00224684
	protected override void ᜁ(DataProvider A_0, int A_1, ExcelVersion A_2)
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
		A_0.WriteInt32(A_1, this.ᜂ);
	}

	// Token: 0x06003DA8 RID: 15784 RVA: 0x002256CC File Offset: 0x002246CC
	public override int ᜀ(ExcelVersion A_0)
	{
		int num;
		for (;;)
		{
			IL_30:
			num = 10;
			int num2 = 2;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_30;
				default:
					if (false)
					{
					}
					switch (num2)
					{
					case 0:
						num += 4;
						num2 = 1;
						continue;
					case 1:
						return num;
					case 2:
						if (A_0 != ExcelVersion.Version97to2003)
						{
							if (true)
							{
							}
							num2 = 0;
							continue;
						}
						return num;
					}
					goto IL_30;
				}
			}
		}
		return num;
	}

	// Token: 0x06003DA9 RID: 15785 RVA: 0x00225744 File Offset: 0x00224744
	public new static void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
	{
		int a_ = 15;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				A_1 += 4;
				num = 4;
				continue;
			case 1:
				goto IL_40;
			case 2:
				if (A_3 != ExcelVersion.Version97to2003)
				{
					num = 0;
					continue;
				}
				goto IL_A6;
			case 4:
				goto IL_4F;
			}
			if (A_0 == null)
			{
				if (true)
				{
				}
				num = 1;
			}
			else
			{
				A_1 += 10;
				num = 2;
			}
		}
		IL_40:
		goto IL_92;
		IL_4F:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_92:
			throw new ArgumentNullException(RecordTableEnumerator.b("㕄㕆♈㵊⑌⭎㑐⅒", a_));
		default:
			if (false)
			{
			}
			break;
		}
		IL_A6:
		A_0.WriteInt32(A_1, A_2);
	}

	// Token: 0x06003DAA RID: 15786 RVA: 0x00225800 File Offset: 0x00224800
	public new static int ᜂ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		int a_ = 9;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_38;
			case 2:
				goto IL_47;
			case 3:
				A_1 += 4;
				num = 2;
				continue;
			case 4:
				if (A_2 != ExcelVersion.Version97to2003)
				{
					if (true)
					{
					}
					num = 3;
					continue;
				}
				goto IL_A6;
			}
			if (A_0 == null)
			{
				num = 0;
			}
			else
			{
				A_1 += 10;
				num = 4;
			}
		}
		IL_38:
		goto IL_92;
		IL_47:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_92:
			throw new ArgumentNullException(RecordTableEnumerator.b("伾㍀ⱂ㍄⹆ⵈ⹊㽌", a_));
		default:
			if (false)
			{
			}
			break;
		}
		IL_A6:
		return A_0.ReadInt32(A_1);
	}

	// Token: 0x04001A86 RID: 6790
	private new const int ᜀ = 10;

	// Token: 0x04001A87 RID: 6791
	internal new const int ᜁ = 6;

	// Token: 0x04001A88 RID: 6792
	[spr\u2429(6, 4, true)]
	private new int ᜂ;
}
