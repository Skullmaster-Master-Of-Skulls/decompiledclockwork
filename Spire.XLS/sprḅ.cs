using System;

// Token: 0x02000459 RID: 1113
internal class sprḅ
{
	// Token: 0x060042D6 RID: 17110 RVA: 0x00256E5C File Offset: 0x00255E5C
	public sprḅ(int A_0, int A_1, int A_2, int A_3)
	{
		this.ᜀ = A_0;
		this.ᜃ = A_2;
		this.ᜁ = A_1;
		this.ᜂ = A_3;
	}

	// Token: 0x060042D7 RID: 17111 RVA: 0x00256E8C File Offset: 0x00255E8C
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
		return this.ᜃ;
	}

	// Token: 0x060042D8 RID: 17112 RVA: 0x00256ED0 File Offset: 0x00255ED0
	public void ᜁ(int A_0)
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

	// Token: 0x060042D9 RID: 17113 RVA: 0x00256F14 File Offset: 0x00255F14
	public int ᜃ()
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

	// Token: 0x060042DA RID: 17114 RVA: 0x00256F58 File Offset: 0x00255F58
	public void ᜃ(int A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x060042DB RID: 17115 RVA: 0x00256F9C File Offset: 0x00255F9C
	public int ᜂ()
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

	// Token: 0x060042DC RID: 17116 RVA: 0x00256FE0 File Offset: 0x00255FE0
	public void ᜄ(int A_0)
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

	// Token: 0x060042DD RID: 17117 RVA: 0x00257024 File Offset: 0x00256024
	public int ᜁ()
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

	// Token: 0x060042DE RID: 17118 RVA: 0x00257068 File Offset: 0x00256068
	public void ᜂ(int A_0)
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

	// Token: 0x060042DF RID: 17119 RVA: 0x002570AC File Offset: 0x002560AC
	public static sprḅ ᜀ(int A_0, int A_1, int A_2, int A_3)
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
		return new sprḅ(A_0, A_1, A_2, A_3);
	}

	// Token: 0x060042E0 RID: 17120 RVA: 0x002570F0 File Offset: 0x002560F0
	public static string ᜀ(int A_0)
	{
		switch (0)
		{
		default:
		{
			char[] array2;
			for (;;)
			{
				char[] array = new char[10];
				int num = 0;
				int num2 = 8;
				for (;;)
				{
					int num3;
					switch (num2)
					{
					case 0:
						goto IL_FC;
					case 1:
						if (num3 >= num)
						{
							goto IL_F0;
						}
						array2[num - num3 - 1] = array[num3];
						num3++;
						num2 = 4;
						continue;
					case 2:
						goto IL_FE;
					case 3:
						goto IL_127;
					case 4:
						goto IL_DD;
					case 5:
						num2 = 6;
						continue;
					case 6:
						if (num >= 9)
						{
							num2 = 3;
							continue;
						}
						A_0--;
						array[num] = (char)(A_0 % 26 + 65);
						A_0 /= 26;
						num++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_F0;
						default:
							if (false)
							{
							}
							num2 = 2;
							continue;
						}
						break;
					case 7:
						if (true)
						{
						}
						if (A_0 > 0)
						{
							num2 = 5;
							continue;
						}
						goto IL_127;
					case 8:
						goto IL_FE;
					case 9:
						goto IL_DD;
					}
					break;
					IL_DD:
					num2 = 1;
					continue;
					IL_F0:
					num2 = 0;
					continue;
					IL_FE:
					num2 = 7;
					continue;
					IL_127:
					array2 = new char[num];
					num3 = 0;
					num2 = 9;
				}
			}
			IL_FC:
			return new string(array2);
		}
		}
	}

	// Token: 0x04001D98 RID: 7576
	private int ᜀ;

	// Token: 0x04001D99 RID: 7577
	private int ᜁ;

	// Token: 0x04001D9A RID: 7578
	private int ᜂ;

	// Token: 0x04001D9B RID: 7579
	private int ᜃ;
}
