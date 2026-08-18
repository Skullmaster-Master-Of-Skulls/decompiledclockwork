using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Spire.Xls.Core.FormatParser.FormatTokens;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020002F6 RID: 758
internal class spr\u20A9 : sprἏ
{
	// Token: 0x06002EEB RID: 12011 RVA: 0x001A3764 File Offset: 0x001A2764
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
		return base.ᜀ(spr\u20A9.ᜁ, A_0, A_1);
	}

	// Token: 0x06002EEC RID: 12012 RVA: 0x001A37AC File Offset: 0x001A27AC
	public override string ᜀ(ref double A_0, bool A_1, CultureInfo A_2, sprᨠ A_3)
	{
		int a_ = 2;
		int year = DateTime.FromOADate(A_0).Year;
		if (this.ᜁ.Length > 2)
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
				if (false)
				{
				}
				return year.ToString();
			}
		}
		return (year % 100).ToString(RecordTableEnumerator.b("࠷ਹ", a_));
	}

	// Token: 0x06002EED RID: 12013 RVA: 0x001A3834 File Offset: 0x001A2834
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

	// Token: 0x06002EEE RID: 12014 RVA: 0x001A3874 File Offset: 0x001A2874
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
		return TokenType.Year;
	}

	// Token: 0x06002EEF RID: 12015 RVA: 0x001A38B0 File Offset: 0x001A28B0
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u20A9()
	{
		int a_ = 14;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u20A9.ᜁ = new Regex(RecordTableEnumerator.b("ὃ㽅ᅇᝉ杋", a_), RegexOptions.Compiled);
	}

	// Token: 0x04001512 RID: 5394
	private new const string ᜀ = "00";

	// Token: 0x04001513 RID: 5395
	private new static readonly Regex ᜁ;
}
