using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Spire.Xls.Core.FormatParser.FormatTokens;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020002F7 RID: 759
internal class sprᡥ : sprἏ
{
	// Token: 0x06002EF1 RID: 12017 RVA: 0x001A3924 File Offset: 0x001A2924
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
		return base.ᜀ(sprᡥ.ᜁ, A_0, A_1);
	}

	// Token: 0x06002EF2 RID: 12018 RVA: 0x001A396C File Offset: 0x001A296C
	public override string ᜀ(ref double A_0, bool A_1, CultureInfo A_2, sprᨠ A_3)
	{
		int a_ = 0;
		int minute = DateTime.FromOADate(A_0).Minute;
		if (this.ᜁ.Length > 1)
		{
			for (;;)
			{
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_51;
				}
			}
			IL_51:
			if (false)
			{
			}
			return minute.ToString(RecordTableEnumerator.b("ص࠷", a_));
		}
		return minute.ToString();
	}

	// Token: 0x06002EF3 RID: 12019 RVA: 0x001A39F0 File Offset: 0x001A29F0
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

	// Token: 0x06002EF4 RID: 12020 RVA: 0x001A3A30 File Offset: 0x001A2A30
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
		return TokenType.Minute;
	}

	// Token: 0x06002EF5 RID: 12021 RVA: 0x001A3A6C File Offset: 0x001A2A6C
	protected override void ᜃ()
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
		base.ᜃ();
		this.ᜁ = this.ᜁ.ToLower();
	}

	// Token: 0x06002EF6 RID: 12022 RVA: 0x001A3AC0 File Offset: 0x001A2AC0
	// Note: this type is marked as 'beforefieldinit'.
	static sprᡥ()
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
		sprᡥ.ᜁ = new Regex(RecordTableEnumerator.b("Ṅ⩆шᙊ㙌繎結慒⡔", a_), RegexOptions.Compiled);
	}

	// Token: 0x04001514 RID: 5396
	private new const string ᜀ = "00";

	// Token: 0x04001515 RID: 5397
	private new static readonly Regex ᜁ;
}
