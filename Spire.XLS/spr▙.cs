using System;
using System.Globalization;
using Spire.Xls;
using Spire.Xls.Core.FormatParser.FormatTokens;

// Token: 0x0200040C RID: 1036
internal class spr\u2599 : sprṷ
{
	// Token: 0x06003E63 RID: 15971 RVA: 0x0022995C File Offset: 0x0022895C
	public override char ᜁ()
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
		return '/';
	}

	// Token: 0x06003E64 RID: 15972 RVA: 0x0022999C File Offset: 0x0022899C
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
		return TokenType.Fraction;
	}

	// Token: 0x06003E65 RID: 15973 RVA: 0x002299DC File Offset: 0x002289DC
	public override string ᜀ(ref double A_0, bool A_1, CultureInfo A_2, sprᨠ A_3)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_7D;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_5B;
				}
				if (false)
				{
				}
				break;
			case 2:
				if (A_3.ᜑ() != CellFormatType.DateTime)
				{
					if (true)
					{
					}
					num = 0;
					continue;
				}
				goto IL_7F;
			case 3:
				goto IL_5B;
			}
			if (A_3 != null)
			{
				num = 3;
				continue;
			}
			break;
			IL_5B:
			num = 2;
		}
		IL_54:
		return this.ᜁ;
		IL_7D:
		goto IL_54;
		IL_7F:
		return DateTimeFormatInfo.CurrentInfo.DateSeparator;
	}

	// Token: 0x06003E66 RID: 15974 RVA: 0x00229A74 File Offset: 0x00228A74
	public override string ᜀ(string A_0, bool A_1)
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
		return this.ᜁ;
	}

	// Token: 0x04001AB9 RID: 6841
	private new const char ᜀ = '/';
}
