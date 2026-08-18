using System;
using System.Reflection;

// Token: 0x0200036B RID: 875
[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
[DefaultMember("Item")]
internal sealed class sprᨳ : Attribute
{
	// Token: 0x0600356E RID: 13678 RVA: 0x001E8174 File Offset: 0x001E7174
	private sprᨳ()
	{
	}

	// Token: 0x0600356F RID: 13679 RVA: 0x001E8190 File Offset: 0x001E7190
	public sprᨳ(int A_0)
	{
		if (A_0 >= 1)
		{
			if (A_0 <= 3)
			{
				this.ᜀ = A_0;
				this.ᜂ = typeof(sprᦊ);
				return;
			}
		}
		throw new ArgumentOutOfRangeException();
	}

	// Token: 0x06003570 RID: 13680 RVA: 0x001E81D4 File Offset: 0x001E71D4
	public sprᨳ(params int[] A_0) : this(typeof(sprᦊ), A_0)
	{
	}

	// Token: 0x06003571 RID: 13681 RVA: 0x001E81F4 File Offset: 0x001E71F4
	public sprᨳ(Type A_0, params int[] A_1)
	{
		this.ᜁ = new int[A_1.Length];
		A_1.CopyTo(this.ᜁ, 0);
		this.ᜂ = A_0;
	}

	// Token: 0x06003572 RID: 13682 RVA: 0x001E8230 File Offset: 0x001E7230
	public sprᨳ(Type A_0, int A_1)
	{
		this.ᜂ = A_0;
		this.ᜀ = A_1;
	}

	// Token: 0x06003573 RID: 13683 RVA: 0x001E8258 File Offset: 0x001E7258
	public int ᜀ()
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

	// Token: 0x06003574 RID: 13684 RVA: 0x001E829C File Offset: 0x001E729C
	public int ᜀ(int A_0)
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				num = 2;
				continue;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_4B;
				default:
					if (false)
					{
					}
					if (A_0 < this.ᜁ.Length)
					{
						num = 4;
						continue;
					}
					goto IL_AA;
				}
				break;
			case 2:
				if (A_0 >= 0)
				{
					num = 5;
					continue;
				}
				goto IL_AA;
			case 4:
				goto IL_82;
			case 5:
				goto IL_4B;
			}
			if (this.ᜁ != null)
			{
				num = 0;
				continue;
			}
			goto IL_AA;
			IL_4B:
			num = 1;
		}
		IL_82:
		return this.ᜁ[A_0];
		IL_AA:
		return this.ᜀ();
	}

	// Token: 0x06003575 RID: 13685 RVA: 0x001E835C File Offset: 0x001E735C
	public Type ᜁ()
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

	// Token: 0x06003576 RID: 13686 RVA: 0x001E83A0 File Offset: 0x001E73A0
	public int ᜂ()
	{
		if (this.ᜁ == null)
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
				return 0;
			}
		}
		return this.ᜁ.Length;
	}

	// Token: 0x0400174B RID: 5963
	private int ᜀ = 1;

	// Token: 0x0400174C RID: 5964
	private int[] ᜁ;

	// Token: 0x0400174D RID: 5965
	private Type ᜂ;
}
