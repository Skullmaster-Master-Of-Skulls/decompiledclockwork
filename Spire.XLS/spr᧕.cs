using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020004FB RID: 1275
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.WSBool)]
internal class spr᧕ : BiffRecordRaw
{
	// Token: 0x06004DDC RID: 19932 RVA: 0x002F8084 File Offset: 0x002F7084
	public bool ᜇ()
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
		return (ushort)(this.ᜃ & spr᧕.OptionFlags.AutoBreaks) != 0;
	}

	// Token: 0x06004DDD RID: 19933 RVA: 0x002F80D0 File Offset: 0x002F70D0
	public void ᜁ(bool A_0)
	{
		if (A_0)
		{
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
				break;
			}
			this.ᜃ |= spr᧕.OptionFlags.AutoBreaks;
			return;
		}
		this.ᜃ &= ~spr᧕.OptionFlags.AutoBreaks;
	}

	// Token: 0x06004DDE RID: 19934 RVA: 0x002F8134 File Offset: 0x002F7134
	public bool ᜊ()
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
		return (ushort)(this.ᜃ & spr᧕.OptionFlags.Dialog) != 0;
	}

	// Token: 0x06004DDF RID: 19935 RVA: 0x002F8180 File Offset: 0x002F7180
	public void ᜇ(bool A_0)
	{
		if (A_0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				break;
			}
			if (true)
			{
			}
			this.ᜃ |= spr᧕.OptionFlags.Dialog;
			return;
		}
		this.ᜃ &= ~spr᧕.OptionFlags.Dialog;
	}

	// Token: 0x06004DE0 RID: 19936 RVA: 0x002F81E4 File Offset: 0x002F71E4
	public bool ᜉ()
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
		return (ushort)(this.ᜃ & spr᧕.OptionFlags.ApplyStyles) != 0;
	}

	// Token: 0x06004DE1 RID: 19937 RVA: 0x002F8230 File Offset: 0x002F7230
	public void ᜆ(bool A_0)
	{
		if (A_0)
		{
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
				break;
			}
			this.ᜃ |= spr᧕.OptionFlags.ApplyStyles;
			return;
		}
		this.ᜃ &= ~spr᧕.OptionFlags.ApplyStyles;
	}

	// Token: 0x06004DE2 RID: 19938 RVA: 0x002F8294 File Offset: 0x002F7294
	public bool ᜂ()
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
		return (ushort)(this.ᜃ & spr᧕.OptionFlags.RowSumsBelow) != 0;
	}

	// Token: 0x06004DE3 RID: 19939 RVA: 0x002F82E0 File Offset: 0x002F72E0
	public void ᜀ(bool A_0)
	{
		if (A_0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				break;
			}
			this.ᜃ |= spr᧕.OptionFlags.RowSumsBelow;
			return;
		}
		this.ᜃ &= ~spr᧕.OptionFlags.RowSumsBelow;
	}

	// Token: 0x06004DE4 RID: 19940 RVA: 0x002F8344 File Offset: 0x002F7344
	public bool ᜀ()
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
		return (ushort)(this.ᜃ & spr᧕.OptionFlags.RowSumsRight) != 0;
	}

	// Token: 0x06004DE5 RID: 19941 RVA: 0x002F8394 File Offset: 0x002F7394
	public void ᜄ(bool A_0)
	{
		if (A_0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				break;
			}
			if (true)
			{
			}
			this.ᜃ |= spr᧕.OptionFlags.RowSumsRight;
			return;
		}
		this.ᜃ &= ~spr᧕.OptionFlags.RowSumsRight;
	}

	// Token: 0x06004DE6 RID: 19942 RVA: 0x002F83FC File Offset: 0x002F73FC
	public bool ᜁ()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return (ushort)(this.ᜃ & spr᧕.OptionFlags.FitToPage) != 0;
	}

	// Token: 0x06004DE7 RID: 19943 RVA: 0x002F844C File Offset: 0x002F744C
	public new void ᜃ(bool A_0)
	{
		if (A_0)
		{
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
				break;
			}
			this.ᜃ |= spr᧕.OptionFlags.FitToPage;
			return;
		}
		this.ᜃ &= ~spr᧕.OptionFlags.FitToPage;
	}

	// Token: 0x06004DE8 RID: 19944 RVA: 0x002F84B4 File Offset: 0x002F74B4
	public new ushort ᜃ()
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
		return (ushort)(BiffRecordRaw.ᜀ((ushort)this.ᜃ, 3072) >> 10);
	}

	// Token: 0x06004DE9 RID: 19945 RVA: 0x002F8504 File Offset: 0x002F7504
	public void ᜀ(ushort A_0)
	{
		if (A_0 > 3)
		{
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
				break;
			}
			throw new ArgumentOutOfRangeException();
		}
		ushort num = (ushort)this.ᜃ;
		BiffRecordRaw.ᜀ(ref num, 3072, (ushort)(A_0 << 10));
		this.ᜃ = (spr᧕.OptionFlags)num;
	}

	// Token: 0x06004DEA RID: 19946 RVA: 0x002F856C File Offset: 0x002F756C
	public bool ᜈ()
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
		return (ushort)(this.ᜃ & spr᧕.OptionFlags.AlternateExpression) != 0;
	}

	// Token: 0x06004DEB RID: 19947 RVA: 0x002F85BC File Offset: 0x002F75BC
	public void ᜅ(bool A_0)
	{
		if (true)
		{
		}
		if (A_0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_33;
			}
			if (false)
			{
			}
			IL_33:
			this.ᜃ |= spr᧕.OptionFlags.AlternateExpression;
			return;
		}
		this.ᜃ &= ~spr᧕.OptionFlags.AlternateExpression;
	}

	// Token: 0x06004DEC RID: 19948 RVA: 0x002F8624 File Offset: 0x002F7624
	public bool ᜄ()
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
		return (ushort)(this.ᜃ & spr᧕.OptionFlags.AlternateFormula) != 0;
	}

	// Token: 0x06004DED RID: 19949 RVA: 0x002F8674 File Offset: 0x002F7674
	public void ᜂ(bool A_0)
	{
		while (A_0)
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
				this.ᜃ |= spr᧕.OptionFlags.AlternateFormula;
				return;
			}
		}
		this.ᜃ &= ~spr᧕.OptionFlags.AlternateFormula;
	}

	// Token: 0x06004DEE RID: 19950 RVA: 0x002F86DC File Offset: 0x002F76DC
	public virtual int ᜆ()
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
		return 2;
	}

	// Token: 0x06004DEF RID: 19951 RVA: 0x002F8718 File Offset: 0x002F7718
	public virtual int ᜅ()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return 2;
	}

	// Token: 0x06004DF0 RID: 19952 RVA: 0x002F8754 File Offset: 0x002F7754
	public virtual int ᜋ()
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
		return 2;
	}

	// Token: 0x06004DF1 RID: 19953 RVA: 0x002F8790 File Offset: 0x002F7790
	public spr᧕()
	{
	}

	// Token: 0x06004DF2 RID: 19954 RVA: 0x002F87B0 File Offset: 0x002F77B0
	public spr᧕(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06004DF3 RID: 19955 RVA: 0x002F87D0 File Offset: 0x002F77D0
	public spr᧕(int A_0) : base(A_0)
	{
	}

	// Token: 0x06004DF4 RID: 19956 RVA: 0x002F87F0 File Offset: 0x002F77F0
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
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
		this.ᜃ = (spr᧕.OptionFlags)A_0.ReadUInt16(A_1);
	}

	// Token: 0x06004DF5 RID: 19957 RVA: 0x002F8838 File Offset: 0x002F7838
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		this.m_iLength = this.MinimumRecordSize;
		A_0.WriteUInt16(A_1, (ushort)this.ᜃ);
	}

	// Token: 0x04002339 RID: 9017
	public new const ushort ᜀ = 3072;

	// Token: 0x0400233A RID: 9018
	public const int ᜁ = 10;

	// Token: 0x0400233B RID: 9019
	private const int ᜂ = 2;

	// Token: 0x0400233C RID: 9020
	[spr\u2429(0, 2)]
	private new spr᧕.OptionFlags ᜃ = (spr᧕.OptionFlags)1217;

	// Token: 0x020004FC RID: 1276
	[Flags]
	private enum OptionFlags : ushort
	{
		// Token: 0x0400233E RID: 9022
		AutoBreaks = 1,
		// Token: 0x0400233F RID: 9023
		Dialog = 16,
		// Token: 0x04002340 RID: 9024
		ApplyStyles = 32,
		// Token: 0x04002341 RID: 9025
		RowSumsBelow = 64,
		// Token: 0x04002342 RID: 9026
		RowSumsRight = 128,
		// Token: 0x04002343 RID: 9027
		FitToPage = 256,
		// Token: 0x04002344 RID: 9028
		AlternateExpression = 16384,
		// Token: 0x04002345 RID: 9029
		AlternateFormula = 32768
	}
}
