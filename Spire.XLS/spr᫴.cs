using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Spire.Xls.Core.FormatParser.FormatTokens;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000434 RID: 1076
internal class spr\u1AF4 : sprἏ
{
	// Token: 0x060040FA RID: 16634 RVA: 0x00245D6C File Offset: 0x00244D6C
	public override int ᜀ(string A_0, int A_1)
	{
		int num;
		for (;;)
		{
			num = base.ᜀ(spr\u1AF4.ᜀ, A_0, A_1);
			if (true)
			{
			}
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_40;
				case 1:
					goto IL_5B;
				case 2:
					if (num != A_1)
					{
						num2 = 0;
						continue;
					}
					goto IL_5B;
				}
				break;
				IL_40:
				this.ᜁ = this.ᜁ.ToLower();
				num2 = 1;
				continue;
				IL_5B:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_40;
				default:
					goto IL_71;
				}
			}
		}
		IL_71:
		if (false)
		{
		}
		return num;
	}

	// Token: 0x060040FB RID: 16635 RVA: 0x00245DFC File Offset: 0x00244DFC
	public override string ᜀ(ref double A_0, bool A_1, CultureInfo A_2, sprᨠ A_3)
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
		return DateTime.FromOADate(A_0).ToString(RecordTableEnumerator.b("ᘵ", a_) + this.ᜁ, A_2).Substring(1);
	}

	// Token: 0x060040FC RID: 16636 RVA: 0x00245E70 File Offset: 0x00244E70
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
		return string.Empty;
	}

	// Token: 0x060040FD RID: 16637 RVA: 0x00245EB0 File Offset: 0x00244EB0
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
		return TokenType.Day;
	}

	// Token: 0x060040FE RID: 16638 RVA: 0x00245EF0 File Offset: 0x00244EF0
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u1AF4()
	{
		int a_ = 15;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u1AF4.ᜀ = new Regex(RecordTableEnumerator.b("Ṅ͆ⵈᙊ晌", a_), RegexOptions.Compiled);
	}

	// Token: 0x04001CFA RID: 7418
	private new static readonly Regex ᜀ;

	// Token: 0x04001CFB RID: 7419
	private new string ᜁ;
}
