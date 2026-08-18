using System;
using Spire.Xls.Core.Parser.Biff_Records.Formula;

// Token: 0x0200036A RID: 874
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
internal sealed class spr\u2400 : Attribute
{
	// Token: 0x06003567 RID: 13671 RVA: 0x001E8000 File Offset: 0x001E7000
	private spr\u2400()
	{
	}

	// Token: 0x06003568 RID: 13672 RVA: 0x001E8020 File Offset: 0x001E7020
	public spr\u2400(FormulaToken A_0)
	{
		this.ᜀ = A_0;
	}

	// Token: 0x06003569 RID: 13673 RVA: 0x001E8048 File Offset: 0x001E7048
	public spr\u2400(FormulaToken A_0, string A_1)
	{
		this.ᜀ = A_0;
		this.ᜁ = A_1;
	}

	// Token: 0x0600356A RID: 13674 RVA: 0x001E8074 File Offset: 0x001E7074
	public spr\u2400(FormulaToken A_0, string A_1, bool A_2)
	{
		this.ᜀ = A_0;
		this.ᜁ = A_1;
		this.ᜂ = A_2;
	}

	// Token: 0x0600356B RID: 13675 RVA: 0x001E80A8 File Offset: 0x001E70A8
	public FormulaToken ᜀ()
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

	// Token: 0x0600356C RID: 13676 RVA: 0x001E80EC File Offset: 0x001E70EC
	public string ᜂ()
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

	// Token: 0x0600356D RID: 13677 RVA: 0x001E8130 File Offset: 0x001E7130
	public bool ᜁ()
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

	// Token: 0x04001748 RID: 5960
	private FormulaToken ᜀ;

	// Token: 0x04001749 RID: 5961
	private string ᜁ = string.Empty;

	// Token: 0x0400174A RID: 5962
	private bool ᜂ;
}
