using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Spire.Xls.Core.FormatParser.FormatTokens;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000578 RID: 1400
internal class sprᩆ : sprἏ
{
	// Token: 0x0600540F RID: 21519 RVA: 0x00343204 File Offset: 0x00342204
	public override int ᜀ(string A_0, int A_1)
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
		return base.ᜀ(sprᩆ.ᜁ, A_0, A_1);
	}

	// Token: 0x06005410 RID: 21520 RVA: 0x0034324C File Offset: 0x0034224C
	public override string ᜀ(ref double A_0, bool A_1, CultureInfo A_2, sprᨠ A_3)
	{
		int a_ = 15;
		int num;
		for (;;)
		{
			num = DateTime.FromOADate(A_0).Hour;
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_9C;
				case 1:
					num2 = 2;
					continue;
				case 2:
					if (num > 12)
					{
						num2 = 6;
						continue;
					}
					goto IL_7B;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						if (this.ᜁ())
						{
							num2 = 1;
							continue;
						}
						goto IL_7B;
					}
					break;
				case 4:
					if (this.ᜁ.Length > 1)
					{
						num2 = 0;
						continue;
					}
					goto IL_ED;
				case 5:
					goto IL_7B;
				case 6:
					num -= 12;
					num2 = 5;
					continue;
				}
				break;
				IL_7B:
				num2 = 4;
			}
		}
		IL_9C:
		return num.ToString(RecordTableEnumerator.b("畄睆", a_));
		IL_ED:
		return num.ToString();
	}

	// Token: 0x06005411 RID: 21521 RVA: 0x00343350 File Offset: 0x00342350
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

	// Token: 0x06005412 RID: 21522 RVA: 0x00343390 File Offset: 0x00342390
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
		return TokenType.Hour;
	}

	// Token: 0x06005413 RID: 21523 RVA: 0x003433CC File Offset: 0x003423CC
	public new bool ᜁ()
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

	// Token: 0x06005414 RID: 21524 RVA: 0x00343410 File Offset: 0x00342410
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
		this.ᜂ = A_0;
	}

	// Token: 0x06005415 RID: 21525 RVA: 0x00343454 File Offset: 0x00342454
	// Note: this type is marked as 'beforefieldinit'.
	static sprᩆ()
	{
		int a_ = 7;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		sprᩆ.ᜁ = new Regex(RecordTableEnumerator.b("昼圾ीṂ湄", a_), RegexOptions.Compiled);
	}

	// Token: 0x0400274C RID: 10060
	private new const string ᜀ = "00";

	// Token: 0x0400274D RID: 10061
	private new static readonly Regex ᜁ;

	// Token: 0x0400274E RID: 10062
	private new bool ᜂ;
}
