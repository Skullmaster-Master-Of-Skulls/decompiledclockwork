using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Spire.Xls.Core.FormatParser.FormatTokens;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020005AB RID: 1451
internal class spr\u21F5 : sprἏ
{
	// Token: 0x060057E6 RID: 22502 RVA: 0x0037C978 File Offset: 0x0037B978
	public override int ᜀ(string A_0, int A_1)
	{
		int num;
		for (;;)
		{
			IL_14:
			num = base.ᜀ(spr\u21F5.ᜀ, A_0, A_1);
			for (;;)
			{
				if (true)
				{
				}
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_64;
					case 1:
						if (num != A_1)
						{
							num2 = 2;
							continue;
						}
						return num;
					case 2:
						this.ᜁ = A_0.Substring(A_1 + 1, this.ᜁ.Length - 2);
						num2 = 0;
						continue;
					}
					goto IL_14;
				}
				IL_64:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_7A;
				}
			}
		}
		IL_7A:
		if (false)
		{
		}
		return num;
	}

	// Token: 0x060057E7 RID: 22503 RVA: 0x0037CA14 File Offset: 0x0037BA14
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
		return this.ᜁ;
	}

	// Token: 0x060057E8 RID: 22504 RVA: 0x0037CA58 File Offset: 0x0037BA58
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

	// Token: 0x060057E9 RID: 22505 RVA: 0x0037CA9C File Offset: 0x0037BA9C
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
		return TokenType.String;
	}

	// Token: 0x060057EA RID: 22506 RVA: 0x0037CADC File Offset: 0x0037BADC
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u21F5()
	{
		int a_ = 1;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u21F5.ᜀ = new Regex(RecordTableEnumerator.b("ᔶ戸攺Ἴ戾歀慂", a_), RegexOptions.Compiled);
	}

	// Token: 0x040029D0 RID: 10704
	private new static readonly Regex ᜀ;
}
