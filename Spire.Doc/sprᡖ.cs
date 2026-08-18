using System;
using Spire.Doc.Documents;

// Token: 0x020002B9 RID: 697
internal class sprᡖ
{
	// Token: 0x06002596 RID: 9622 RVA: 0x00259F08 File Offset: 0x00258F08
	internal TabJustification ᜂ()
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

	// Token: 0x06002597 RID: 9623 RVA: 0x00259F4C File Offset: 0x00258F4C
	internal void ᜀ(TabJustification A_0)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				goto IL_51;
			}
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_51:
				this.ᜁ = A_0;
				num = 0;
				break;
			default:
				if (false)
				{
				}
				if (A_0 == this.ᜁ)
				{
					return;
				}
				num = 1;
				break;
			}
		}
	}

	// Token: 0x06002598 RID: 9624 RVA: 0x00259FC8 File Offset: 0x00258FC8
	internal TabLeader ᜁ()
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

	// Token: 0x06002599 RID: 9625 RVA: 0x0025A00C File Offset: 0x0025900C
	internal void ᜀ(TabLeader A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_49;
			case 2:
				return;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_49:
				this.ᜂ = A_0;
				if (true)
				{
				}
				num = 2;
				break;
			default:
				if (false)
				{
				}
				if (A_0 == this.ᜂ)
				{
					return;
				}
				num = 1;
				break;
			}
		}
	}

	// Token: 0x0600259A RID: 9626 RVA: 0x0025A088 File Offset: 0x00259088
	internal sprᡖ(byte A_0)
	{
		this.ᜁ = (TabJustification)(A_0 & 7);
		this.ᜂ = (TabLeader)((byte)((A_0 & 56) >> 3));
	}

	// Token: 0x0600259B RID: 9627 RVA: 0x0025A0B4 File Offset: 0x002590B4
	internal sprᡖ(TabJustification A_0, TabLeader A_1)
	{
		this.ᜁ = A_0;
		this.ᜂ = A_1;
	}

	// Token: 0x0600259C RID: 9628 RVA: 0x0025A0D8 File Offset: 0x002590D8
	internal byte ᜀ()
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
		int num = (int)((byte)this.ᜂ) << 3 | (int)((byte)this.ᜁ);
		return (byte)num;
	}

	// Token: 0x0600259D RID: 9629 RVA: 0x0025A128 File Offset: 0x00259128
	// Note: this type is marked as 'beforefieldinit'.
	static sprᡖ()
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
		sprᡖ.ᜀ = 1;
	}

	// Token: 0x040021FB RID: 8699
	internal static int ᜀ;

	// Token: 0x040021FC RID: 8700
	private TabJustification ᜁ;

	// Token: 0x040021FD RID: 8701
	private TabLeader ᜂ;
}
