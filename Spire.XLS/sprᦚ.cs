using System;
using System.Globalization;
using Spire.Xls.Core.FormatParser.FormatTokens;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020002F8 RID: 760
internal class sprᦚ : sprἏ
{
	// Token: 0x06002EF8 RID: 12024 RVA: 0x001A3B34 File Offset: 0x001A2B34
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
		throw new NotImplementedException();
	}

	// Token: 0x06002EF9 RID: 12025 RVA: 0x001A3B74 File Offset: 0x001A2B74
	public override string ᜀ(ref double A_0, bool A_1, CultureInfo A_2, sprᨠ A_3)
	{
		int a_ = 8;
		switch (0)
		{
		default:
		{
			string format;
			string str;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_99:
				format = RecordTableEnumerator.b("฽瀿牁", a_);
				str = this.ᜁ.Substring(sprᦚ.ᜂ);
				num = 3;
				break;
			default:
				if (false)
				{
				}
				goto IL_4C;
			}
			int length;
			int num3;
			for (;;)
			{
				IL_35:
				switch (num)
				{
				case 0:
				{
					int num2 = sprᦚ.ᜂ - length;
					num3 = (int)sprᨠ.ᜀ((double)num3 / Math.Pow(10.0, (double)num2));
					format = this.ᜁ.Substring(1, length - 1);
					num = 2;
					continue;
				}
				case 1:
					if (length < sprᦚ.ᜂ)
					{
						num = 0;
						continue;
					}
					goto IL_99;
				case 2:
					goto IL_108;
				case 3:
					goto IL_C6;
				}
				goto IL_4C;
			}
			IL_C6:
			IL_108:
			return RecordTableEnumerator.b("ွ", a_) + num3.ToString(format) + str;
			IL_4C:
			if (true)
			{
			}
			num3 = DateTime.FromOADate(A_0).Millisecond;
			length = this.ᜁ.Length;
			format = string.Empty;
			str = string.Empty;
			num = 1;
			goto IL_35;
		}
		}
	}

	// Token: 0x06002EFA RID: 12026 RVA: 0x001A3CB4 File Offset: 0x001A2CB4
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

	// Token: 0x06002EFB RID: 12027 RVA: 0x001A3CF4 File Offset: 0x001A2CF4
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
		return TokenType.MilliSecond;
	}

	// Token: 0x06002EFC RID: 12028 RVA: 0x001A3D34 File Offset: 0x001A2D34
	// Note: this type is marked as 'beforefieldinit'.
	static sprᦚ()
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
		sprᦚ.ᜂ = RecordTableEnumerator.b("ܶस଺", a_).Length;
	}

	// Token: 0x04001516 RID: 5398
	private new const string ᜀ = "000";

	// Token: 0x04001517 RID: 5399
	private new const string ᜁ = ".";

	// Token: 0x04001518 RID: 5400
	private new static readonly int ᜂ;
}
