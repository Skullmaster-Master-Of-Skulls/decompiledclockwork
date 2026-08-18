using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Spire.Xls.Core.FormatParser.FormatTokens;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200040D RID: 1037
internal class sprᲸ : sprἏ
{
	// Token: 0x06003E68 RID: 15976 RVA: 0x00229ACC File Offset: 0x00228ACC
	public override int ᜀ(string A_0, int A_1)
	{
		int a_ = 12;
		int num;
		for (;;)
		{
			Match match;
			num = base.ᜀ(sprᲸ.ᜃ, A_0, A_1, out match);
			if (true)
			{
			}
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num != A_1)
					{
						num2 = 1;
						continue;
					}
					return num;
				case 1:
					goto IL_53;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_53;
					default:
						goto IL_A6;
					}
					break;
				}
				break;
				IL_53:
				string value = match.Groups[RecordTableEnumerator.b("แ⭃╅⥇♉⥋ݍᑏ", a_)].Value;
				this.ᜄ = int.Parse(value, NumberStyles.HexNumber);
				num2 = 2;
			}
		}
		IL_A6:
		if (false)
		{
		}
		return num;
	}

	// Token: 0x06003E69 RID: 15977 RVA: 0x00229B88 File Offset: 0x00228B88
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

	// Token: 0x06003E6A RID: 15978 RVA: 0x00229BC8 File Offset: 0x00228BC8
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

	// Token: 0x06003E6B RID: 15979 RVA: 0x00229C08 File Offset: 0x00228C08
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
		return TokenType.Culture;
	}

	// Token: 0x06003E6C RID: 15980 RVA: 0x00229C48 File Offset: 0x00228C48
	public CultureInfo ᜂ()
	{
		CultureInfo result;
		try
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			result = new CultureInfo(this.ᜄ);
		}
		catch (Exception)
		{
			result = CultureInfo.CurrentCulture;
		}
		if (true)
		{
		}
		return result;
	}

	// Token: 0x06003E6D RID: 15981 RVA: 0x00229CAC File Offset: 0x00228CAC
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
		return this.ᜄ == 63488;
	}

	// Token: 0x06003E6E RID: 15982 RVA: 0x00229CF4 File Offset: 0x00228CF4
	// Note: this type is marked as 'beforefieldinit'.
	static sprᲸ()
	{
		int a_ = 18;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		sprᲸ.ᜃ = new Regex(RecordTableEnumerator.b("ᑇᅉ။橍硏浑桓ᕕし㭙⹛㽝͟ᙡţᑥ噧䑩卫䝭Ɐ影屳䥵䑷㙹፻ᵽ쾅첇뒉힋뺍붏ꮑ햓뮕슗ﮙ놛ﶟ覡趣瘟", a_), RegexOptions.Compiled);
	}

	// Token: 0x04001ABA RID: 6842
	private new const string ᜀ = "LocaleID";

	// Token: 0x04001ABB RID: 6843
	private new const string ᜁ = "Character";

	// Token: 0x04001ABC RID: 6844
	private new const int ᜂ = 63488;

	// Token: 0x04001ABD RID: 6845
	private new static readonly Regex ᜃ;

	// Token: 0x04001ABE RID: 6846
	private int ᜄ;
}
