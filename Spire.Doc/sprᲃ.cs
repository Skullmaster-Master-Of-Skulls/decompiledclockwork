using System;

// Token: 0x0200028F RID: 655
internal class sprᲃ
{
	// Token: 0x060022CD RID: 8909 RVA: 0x00239990 File Offset: 0x00238990
	internal sprᲃ(sprᲨ A_0, sprᲨ A_1, int A_2, int A_3)
	{
		this.ᜃ = A_2;
		this.ᜄ = A_3;
		this.ᜀ(A_0, A_1);
	}

	// Token: 0x060022CE RID: 8910 RVA: 0x002399C8 File Offset: 0x002389C8
	private void ᜀ(sprᲨ A_0, sprᲨ A_1)
	{
		for (;;)
		{
			int num = 0;
			int num2 = 4;
			for (;;)
			{
				int num4;
				switch (num2)
				{
				case 0:
				{
					float num3;
					this.ᜀ = num3;
					this.ᜁ = num;
					this.ᜂ = num4;
					num2 = 5;
					continue;
				}
				case 1:
					goto IL_B3;
				case 2:
				{
					float num3;
					if (num3 < this.ᜀ)
					{
						num2 = 0;
						continue;
					}
					goto IL_5E;
				}
				case 3:
					return;
				case 4:
					goto IL_B3;
				case 5:
					goto IL_5E;
				case 6:
					goto IL_72;
				case 7:
					if (num >= A_0.ᜅ())
					{
						num2 = 3;
						continue;
					}
					num4 = 0;
					num2 = 10;
					continue;
				case 8:
				{
					if (num4 >= A_1.ᜅ())
					{
						num2 = 9;
						continue;
					}
					float num3 = sprὍ.ᜁ(A_0.ᜃ(num).ᜁ(), A_1.ᜃ(num4).ᜁ());
					num2 = 2;
					continue;
				}
				case 9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_72;
					default:
						if (false)
						{
						}
						num++;
						num2 = 1;
						continue;
					}
					break;
				case 10:
					goto IL_40;
				}
				break;
				IL_40:
				num2 = 8;
				continue;
				IL_72:
				goto IL_40;
				IL_5E:
				if (true)
				{
				}
				num4++;
				num2 = 6;
				continue;
				IL_B3:
				num2 = 7;
			}
		}
	}

	// Token: 0x060022CF RID: 8911 RVA: 0x00239B18 File Offset: 0x00238B18
	internal float ᜀ()
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

	// Token: 0x060022D0 RID: 8912 RVA: 0x00239B5C File Offset: 0x00238B5C
	internal int ᜂ()
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

	// Token: 0x060022D1 RID: 8913 RVA: 0x00239BA0 File Offset: 0x00238BA0
	internal int ᜄ()
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

	// Token: 0x060022D2 RID: 8914 RVA: 0x00239BE4 File Offset: 0x00238BE4
	internal int ᜁ()
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

	// Token: 0x060022D3 RID: 8915 RVA: 0x00239C28 File Offset: 0x00238C28
	internal int ᜃ()
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
		return this.ᜄ;
	}

	// Token: 0x0400212F RID: 8495
	private float ᜀ = float.MaxValue;

	// Token: 0x04002130 RID: 8496
	private int ᜁ;

	// Token: 0x04002131 RID: 8497
	private int ᜂ;

	// Token: 0x04002132 RID: 8498
	private readonly int ᜃ;

	// Token: 0x04002133 RID: 8499
	private readonly int ᜄ;
}
