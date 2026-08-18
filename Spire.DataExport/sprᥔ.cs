using System;

// Token: 0x0200010E RID: 270
internal class sprᥔ
{
	// Token: 0x0600062E RID: 1582 RVA: 0x0003BDA8 File Offset: 0x0003ADA8
	public sprᥔ(ushort A_0, ushort A_1, ushort A_2, ushort A_3)
	{
		this.ᜀ = A_0;
		this.ᜁ = A_1;
		this.ᜂ = A_2;
		this.ᜃ = A_3;
	}

	// Token: 0x0600062F RID: 1583 RVA: 0x0003BDD8 File Offset: 0x0003ADD8
	public bool ᜀ(sprᥔ A_0)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_96;
			case 2:
				num = 3;
				continue;
			case 3:
				if (this.ᜂ == A_0.ᜂ())
				{
					num = 0;
					continue;
				}
				return false;
			}
			if (true)
			{
			}
			if (this.ᜁ != A_0.ᜃ())
			{
				return false;
			}
			num = 2;
		}
		IL_96:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return false;
		default:
			if (false)
			{
			}
			return this.ᜃ == A_0.ᜀ();
		}
	}

	// Token: 0x06000630 RID: 1584 RVA: 0x0003BE80 File Offset: 0x0003AE80
	public ushort ᜁ()
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

	// Token: 0x06000631 RID: 1585 RVA: 0x0003BEC4 File Offset: 0x0003AEC4
	public void ᜃ(ushort A_0)
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
		this.ᜀ = A_0;
	}

	// Token: 0x06000632 RID: 1586 RVA: 0x0003BF08 File Offset: 0x0003AF08
	public ushort ᜃ()
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
		return this.ᜁ;
	}

	// Token: 0x06000633 RID: 1587 RVA: 0x0003BF4C File Offset: 0x0003AF4C
	public void ᜂ(ushort A_0)
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

	// Token: 0x06000634 RID: 1588 RVA: 0x0003BF90 File Offset: 0x0003AF90
	public ushort ᜂ()
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

	// Token: 0x06000635 RID: 1589 RVA: 0x0003BFD4 File Offset: 0x0003AFD4
	public void ᜁ(ushort A_0)
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

	// Token: 0x06000636 RID: 1590 RVA: 0x0003C018 File Offset: 0x0003B018
	public ushort ᜀ()
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
		return this.ᜃ;
	}

	// Token: 0x06000637 RID: 1591 RVA: 0x0003C05C File Offset: 0x0003B05C
	public void ᜀ(ushort A_0)
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
		this.ᜃ = A_0;
	}

	// Token: 0x040005A6 RID: 1446
	private ushort ᜀ;

	// Token: 0x040005A7 RID: 1447
	private ushort ᜁ;

	// Token: 0x040005A8 RID: 1448
	private ushort ᜂ;

	// Token: 0x040005A9 RID: 1449
	private ushort ᜃ;
}
