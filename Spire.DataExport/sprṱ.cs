using System;
using System.Text;

// Token: 0x02000123 RID: 291
internal class sprṱ : spr\u2320
{
	// Token: 0x060006B9 RID: 1721 RVA: 0x000402BC File Offset: 0x0003F2BC
	public sprṱ(sprᲤ A_0, ushort A_1, ushort A_2, byte[] A_3) : base(A_0, A_1, A_2, A_3)
	{
	}

	// Token: 0x060006BA RID: 1722 RVA: 0x000402D4 File Offset: 0x0003F2D4
	public string ᜅ()
	{
		byte[] array;
		if ((this.ᜃ() & 1) != 1)
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
				array = new byte[(int)this.ᜆ()];
				Array.Copy(base.ᜢ(), 15, array, 0, (int)this.ᜆ());
				return Encoding.ASCII.GetString(array);
			}
		}
		if (true)
		{
		}
		array = new byte[(int)(this.ᜆ() * 2)];
		Array.Copy(base.ᜢ(), 15, array, 0, (int)(this.ᜆ() * 2));
		return Encoding.Unicode.GetString(array);
	}

	// Token: 0x060006BB RID: 1723 RVA: 0x0004037C File Offset: 0x0003F37C
	public byte ᜆ()
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
		return base.ᜢ()[3];
	}

	// Token: 0x060006BC RID: 1724 RVA: 0x000403C0 File Offset: 0x0003F3C0
	public int ᜀ()
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
		return sprᮌ.ᜀ(false, base.ᜢ(), 14, true, (int)this.ᜆ());
	}

	// Token: 0x060006BD RID: 1725 RVA: 0x00040410 File Offset: 0x0003F410
	public byte ᜃ()
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
		return base.ᜢ()[14];
	}

	// Token: 0x060006BE RID: 1726 RVA: 0x00040454 File Offset: 0x0003F454
	public int ᜂ()
	{
		for (;;)
		{
			byte b = base.ᜢ()[14 + this.ᜀ()];
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (b != 91)
					{
						num = 1;
						continue;
					}
					goto IL_52;
				case 1:
					num = 3;
					continue;
				case 2:
					if (b != 59)
					{
						num = 5;
						continue;
					}
					goto IL_52;
				case 3:
					if (b == 123)
					{
						if (true)
						{
						}
						num = 4;
						continue;
					}
					return -1;
				case 4:
					goto IL_52;
				case 5:
					num = 0;
					continue;
				}
				break;
				IL_52:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_68;
				}
			}
		}
		IL_68:
		if (false)
		{
		}
		return (int)sprᮌ.ᜁ(base.ᜢ(), 17 + this.ᜀ());
	}

	// Token: 0x060006BF RID: 1727 RVA: 0x00040528 File Offset: 0x0003F528
	public int ᜄ()
	{
		for (;;)
		{
			byte b = base.ᜢ()[14 + this.ᜀ()];
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (b != 91)
					{
						num = 4;
						continue;
					}
					goto IL_5A;
				case 1:
					if (b == 123)
					{
						num = 2;
						continue;
					}
					return -1;
				case 2:
					goto IL_5A;
				case 3:
					num = 0;
					continue;
				case 4:
					num = 1;
					continue;
				case 5:
					if (b != 59)
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					goto IL_5A;
				}
				break;
				IL_5A:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_70;
				}
			}
		}
		IL_70:
		if (false)
		{
		}
		return (int)sprᮌ.ᜁ(base.ᜢ(), 19 + this.ᜀ());
	}

	// Token: 0x060006C0 RID: 1728 RVA: 0x000405FC File Offset: 0x0003F5FC
	public int ᜁ()
	{
		for (;;)
		{
			byte b = base.ᜢ()[14 + this.ᜀ()];
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 5;
					continue;
				case 1:
					goto IL_5A;
				case 2:
					num = 4;
					continue;
				case 3:
					if (b != 59)
					{
						if (true)
						{
						}
						num = 0;
						continue;
					}
					goto IL_5A;
				case 4:
					if (b == 123)
					{
						num = 1;
						continue;
					}
					return -1;
				case 5:
					if (b != 91)
					{
						num = 2;
						continue;
					}
					goto IL_5A;
				}
				break;
				IL_5A:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_70;
				}
			}
		}
		IL_70:
		if (false)
		{
		}
		return (int)sprᮌ.ᜁ(base.ᜢ(), 21 + this.ᜀ());
	}

	// Token: 0x060006C1 RID: 1729 RVA: 0x000406D0 File Offset: 0x0003F6D0
	public int ᜇ()
	{
		for (;;)
		{
			byte b = base.ᜢ()[14 + this.ᜀ()];
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (b != 91)
					{
						num = 5;
						continue;
					}
					goto IL_52;
				case 1:
					num = 0;
					continue;
				case 2:
					goto IL_52;
				case 3:
					if (true)
					{
					}
					if (b == 123)
					{
						num = 2;
						continue;
					}
					return -1;
				case 4:
					if (b != 59)
					{
						num = 1;
						continue;
					}
					goto IL_52;
				case 5:
					num = 3;
					continue;
				}
				break;
				IL_52:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_68;
				}
			}
		}
		IL_68:
		if (false)
		{
		}
		return (int)sprᮌ.ᜁ(base.ᜢ(), 23 + this.ᜀ());
	}
}
