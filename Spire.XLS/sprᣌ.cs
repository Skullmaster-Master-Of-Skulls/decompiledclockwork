using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Spire.Xls.Core.FormatParser.FormatTokens;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000432 RID: 1074
internal class sprᣌ : sprἏ
{
	// Token: 0x060040ED RID: 16621 RVA: 0x00245980 File Offset: 0x00244980
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
		return base.ᜀ(sprᣌ.ᜅ, A_0, A_1);
	}

	// Token: 0x060040EE RID: 16622 RVA: 0x002459C8 File Offset: 0x002449C8
	public override string ᜀ(ref double A_0, bool A_1, CultureInfo A_2, sprᨠ A_3)
	{
		int a_ = 17;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return DateTime.FromOADate(A_0).ToString(RecordTableEnumerator.b("杆", a_) + this.ᜁ, A_2).Substring(1);
	}

	// Token: 0x060040EF RID: 16623 RVA: 0x00245A3C File Offset: 0x00244A3C
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

	// Token: 0x060040F0 RID: 16624 RVA: 0x00245A7C File Offset: 0x00244A7C
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
		return TokenType.Month;
	}

	// Token: 0x060040F1 RID: 16625 RVA: 0x00245ABC File Offset: 0x00244ABC
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
		this.ᜁ = this.ᜁ.ToUpper();
	}

	// Token: 0x060040F2 RID: 16626 RVA: 0x00245B10 File Offset: 0x00244B10
	// Note: this type is marked as 'beforefieldinit'.
	static sprᣌ()
	{
		int a_ = 6;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		sprᣌ.ᜅ = new Regex(RecordTableEnumerator.b("朻猽ⴿὁ㽃畅摇㝉", a_), RegexOptions.Compiled);
	}

	// Token: 0x04001CF3 RID: 7411
	private new const string ᜀ = "00";

	// Token: 0x04001CF4 RID: 7412
	private new const int ᜁ = 4;

	// Token: 0x04001CF5 RID: 7413
	private new const int ᜂ = 3;

	// Token: 0x04001CF6 RID: 7414
	private new const int ᜃ = 3;

	// Token: 0x04001CF7 RID: 7415
	private const string ᜄ = "00";

	// Token: 0x04001CF8 RID: 7416
	private static readonly Regex ᜅ;
}
