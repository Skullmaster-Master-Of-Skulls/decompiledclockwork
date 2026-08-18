using System;
using System.Globalization;
using Spire.Xls.Core.FormatParser.FormatTokens;

// Token: 0x02000493 RID: 1171
internal class spr\u2595 : sprṷ
{
	// Token: 0x0600483A RID: 18490 RVA: 0x002B9D38 File Offset: 0x002B8D38
	public override string ᜀ(ref double A_0, bool A_1, CultureInfo A_2, sprᨠ A_3)
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
		return string.Empty;
	}

	// Token: 0x0600483B RID: 18491 RVA: 0x002B9D78 File Offset: 0x002B8D78
	public override string ᜀ(string A_0, bool A_1)
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
		return string.Empty;
	}

	// Token: 0x0600483C RID: 18492 RVA: 0x002B9DB8 File Offset: 0x002B8DB8
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
		return ',';
	}

	// Token: 0x0600483D RID: 18493 RVA: 0x002B9DF8 File Offset: 0x002B8DF8
	internal override TokenType ᜀ()
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
		return TokenType.ThousandsSeparator;
	}

	// Token: 0x0600483E RID: 18494 RVA: 0x002B9E38 File Offset: 0x002B8E38
	public bool ᜂ()
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

	// Token: 0x0600483F RID: 18495 RVA: 0x002B9E7C File Offset: 0x002B8E7C
	public new void ᜀ(bool A_0)
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

	// Token: 0x06004840 RID: 18496 RVA: 0x002B9EC0 File Offset: 0x002B8EC0
	public new double ᜀ(double A_0)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					A_0 /= 1000.0;
					num = 2;
					continue;
				}
				break;
			case 2:
				return A_0;
			}
			if (true)
			{
			}
			if (!this.ᜁ)
			{
				break;
			}
			num = 0;
		}
		return A_0;
	}

	// Token: 0x040020D0 RID: 8400
	private new const char ᜀ = ',';

	// Token: 0x040020D1 RID: 8401
	private new bool ᜁ;
}
