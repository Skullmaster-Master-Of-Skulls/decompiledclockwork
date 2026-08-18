using System;
using Spire.Xls.Core.FormatParser.FormatTokens;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020005AD RID: 1453
internal class spr᱕ : spr\u20C6
{
	// Token: 0x060057F2 RID: 22514 RVA: 0x0037CCDC File Offset: 0x0037BCDC
	protected internal override string ᜀ(double A_0, int A_1, bool A_2)
	{
		int a_ = 19;
		int num = 1;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_AC;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					num = 4;
					continue;
				case 2:
					if (!A_2)
					{
						num = 0;
						continue;
					}
					goto IL_62;
				case 3:
					goto IL_88;
				case 4:
					if (A_0 >= 1.0)
					{
						num = 3;
						continue;
					}
					goto IL_AC;
				case 5:
					num = 2;
					continue;
				}
				if (A_1 != 0)
				{
					goto IL_62;
				}
				num = 5;
				break;
			}
		}
		IL_62:
		return base.ᜀ(A_0, A_1, A_2);
		IL_88:
		goto IL_62;
		IL_AC:
		return RecordTableEnumerator.b("楈", a_);
	}

	// Token: 0x060057F3 RID: 22515 RVA: 0x0037CDA4 File Offset: 0x0037BDA4
	internal override TokenType ᜀ()
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
		return TokenType.PlaceReservedDigit;
	}

	// Token: 0x060057F4 RID: 22516 RVA: 0x0037CDE4 File Offset: 0x0037BDE4
	public override char ᜁ()
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
		return '?';
	}

	// Token: 0x040029D2 RID: 10706
	private new const char ᜀ = '?';

	// Token: 0x040029D3 RID: 10707
	private new const string ᜁ = " ";
}
