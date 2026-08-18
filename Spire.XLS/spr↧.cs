using System;
using Spire.Xls;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020004EF RID: 1263
internal class spr\u21A7
{
	// Token: 0x06004D4B RID: 19787 RVA: 0x002F20F0 File Offset: 0x002F10F0
	public XlsFill ᜂ()
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

	// Token: 0x06004D4C RID: 19788 RVA: 0x002F2134 File Offset: 0x002F1134
	public void ᜀ(XlsFill A_0)
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

	// Token: 0x06004D4D RID: 19789 RVA: 0x002F2178 File Offset: 0x002F1178
	public XlsFont ᜀ()
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
		return this.ᜂ;
	}

	// Token: 0x06004D4E RID: 19790 RVA: 0x002F21BC File Offset: 0x002F11BC
	public void ᜀ(XlsFont A_0)
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
		this.ᜂ = A_0;
	}

	// Token: 0x06004D4F RID: 19791 RVA: 0x002F2200 File Offset: 0x002F1200
	public XlsBordersCollection ᜁ()
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
		return this.ᜀ;
	}

	// Token: 0x06004D50 RID: 19792 RVA: 0x002F2244 File Offset: 0x002F1244
	public void ᜀ(XlsBordersCollection A_0)
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
		this.ᜀ = A_0;
	}

	// Token: 0x06004D51 RID: 19793 RVA: 0x002F2288 File Offset: 0x002F1288
	internal void ᜀ(sprᲖ A_0)
	{
		int num = 21;
		for (;;)
		{
			sprᡦ sprᡦ;
			switch (num)
			{
			case 0:
				goto IL_162;
			case 1:
				goto IL_2AF;
			case 2:
				goto IL_188;
			case 3:
				goto IL_249;
			case 4:
				sprᡦ = (sprᡦ)this.ᜀ[BordersLineType.EdgeBottom];
				num = 10;
				continue;
			case 5:
				if (sprᡦ != null)
				{
					num = 18;
					continue;
				}
				goto IL_249;
			case 6:
				if (sprᡦ != null)
				{
					num = 11;
					continue;
				}
				return;
			case 7:
				if (this.ᜂ != null)
				{
					num = 15;
					continue;
				}
				goto IL_37F;
			case 8:
				A_0.ᜈ().ᜀ(sprᡦ.ᜅ(), true);
				A_0.LeftBorderStyle = sprᡦ.ᜂ();
				num = 2;
				continue;
			case 9:
				if (this.ᜀ != null)
				{
					num = 4;
					continue;
				}
				return;
			case 10:
				if (sprᡦ != null)
				{
					num = 16;
					continue;
				}
				goto IL_27C;
			case 11:
				A_0.ᜆ().ᜀ(sprᡦ.ᜅ(), true);
				A_0.TopBorderStyle = sprᡦ.ᜂ();
				num = 12;
				continue;
			case 12:
				return;
			case 13:
				A_0.FillPattern = this.ᜁ.Pattern;
				A_0.ᜅ().ᜀ(this.ᜁ.PatternColorObject, true);
				A_0.ᜄ().ᜀ(this.ᜁ.OColor, true);
				A_0.IsPatternFormatPresent = true;
				A_0.ᜀ(true);
				num = 0;
				continue;
			case 14:
				if (sprᡦ != null)
				{
					num = 8;
					continue;
				}
				goto IL_188;
			case 15:
				num = 20;
				continue;
			case 16:
				goto IL_B2;
			case 17:
				A_0.ᜊ().ᜀ(this.ᜂ.OColor, true);
				num = 1;
				continue;
			case 18:
				A_0.ᜉ().ᜀ(sprᡦ.ᜅ(), true);
				A_0.RightBorderStyle = sprᡦ.ᜂ();
				num = 3;
				continue;
			case 19:
				goto IL_27C;
			case 20:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_B2;
				default:
					if (false)
					{
					}
					if (this.ᜂ.KnownColor != (ExcelColors)32767)
					{
						if (true)
						{
						}
						num = 17;
						continue;
					}
					goto IL_2AF;
				}
				break;
			case 22:
				goto IL_37F;
			}
			if (this.ᜁ != null)
			{
				num = 13;
				continue;
			}
			goto IL_162;
			IL_B2:
			A_0.ᜇ().ᜀ(sprᡦ.ᜅ(), true);
			A_0.BottomBorderStyle = sprᡦ.ᜂ();
			num = 19;
			continue;
			IL_162:
			num = 7;
			continue;
			IL_188:
			sprᡦ = (sprᡦ)this.ᜀ[BordersLineType.EdgeRight];
			num = 5;
			continue;
			IL_249:
			sprᡦ = (sprᡦ)this.ᜀ[BordersLineType.EdgeTop];
			num = 6;
			continue;
			IL_27C:
			sprᡦ = (sprᡦ)this.ᜀ[BordersLineType.EdgeLeft];
			num = 14;
			continue;
			IL_2AF:
			A_0.IsBold = this.ᜂ.IsBold;
			A_0.IsItalic = this.ᜂ.IsItalic;
			A_0.IsStrikeThrough = this.ᜂ.IsStrikethrough;
			A_0.IsSubScript = this.ᜂ.IsSubscript;
			A_0.IsSuperScript = this.ᜂ.IsSuperscript;
			A_0.Underline = this.ᜂ.Underline;
			num = 22;
			continue;
			IL_37F:
			num = 9;
		}
	}

	// Token: 0x06004D52 RID: 19794 RVA: 0x002F263C File Offset: 0x002F163C
	public spr\u21A7 ᜀ(XlsWorkbook A_0)
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
		spr\u21A7 spr_u21A = (spr\u21A7)base.MemberwiseClone();
		spr_u21A.ᜀ = (XlsBordersCollection)this.ᜀ.Clone(A_0);
		spr_u21A.ᜁ = this.ᜁ.Clone();
		spr_u21A.ᜂ = this.ᜂ.Clone(A_0);
		return spr_u21A;
	}

	// Token: 0x04002322 RID: 8994
	private XlsBordersCollection ᜀ;

	// Token: 0x04002323 RID: 8995
	private XlsFill ᜁ;

	// Token: 0x04002324 RID: 8996
	private XlsFont ᜂ;
}
