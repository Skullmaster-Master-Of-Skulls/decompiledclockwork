using System;
using System.Collections;
using System.Reflection;

// Token: 0x02000021 RID: 33
[DefaultMember("Item")]
internal class spr\u1D65 : IEnumerable
{
	// Token: 0x06000113 RID: 275 RVA: 0x0000AE54 File Offset: 0x00009E54
	public IEnumerator ᜀ()
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
		return this.ᜀ.GetEnumerator();
	}

	// Token: 0x06000114 RID: 276 RVA: 0x0000AE9C File Offset: 0x00009E9C
	public int ᜀ(sprᤑ A_0)
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
		return this.ᜀ.Add(A_0);
	}

	// Token: 0x06000115 RID: 277 RVA: 0x0000AEE4 File Offset: 0x00009EE4
	public int ᜀ(ushort A_0)
	{
		int num;
		for (;;)
		{
			num = 0;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (this.ᜀ(num).ᜂ().ᜀ() == A_0)
					{
						num2 = 5;
						continue;
					}
					num++;
					num2 = 2;
					continue;
				case 1:
					goto IL_6C;
				case 2:
					goto IL_6C;
				case 3:
					if (num >= this.ᜁ())
					{
						num2 = 4;
						continue;
					}
					num2 = 0;
					continue;
				case 4:
					goto IL_88;
				case 5:
					return num;
				}
				break;
				IL_6C:
				num2 = 3;
			}
		}
		return num;
		IL_88:
		if (true)
		{
		}
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return num;
		default:
			if (false)
			{
			}
			return -1;
		}
	}

	// Token: 0x06000116 RID: 278 RVA: 0x0000AFA0 File Offset: 0x00009FA0
	public ushort ᜀ(int A_0, int A_1)
	{
		int num;
		for (;;)
		{
			num = 0;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_9E;
				case 1:
					if (this.ᜀ(num).ᜀ() == A_1)
					{
						num2 = 6;
						continue;
					}
					goto IL_3E;
				case 2:
					goto IL_3C;
				case 3:
					if (num >= this.ᜁ())
					{
						num2 = 7;
						continue;
					}
					num2 = 1;
					continue;
				case 4:
					if (this.ᜀ(num).ᜁ() == A_0)
					{
						num2 = 5;
						continue;
					}
					goto IL_3E;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3C;
					default:
						goto IL_62;
					}
					break;
				case 6:
					if (true)
					{
					}
					num2 = 4;
					continue;
				case 7:
					return 15;
				}
				break;
				IL_3E:
				num++;
				num2 = 0;
				continue;
				IL_9E:
				num2 = 3;
				continue;
				IL_3C:
				goto IL_9E;
			}
		}
		IL_62:
		if (false)
		{
		}
		return this.ᜀ(num).ᜂ().ᜀ();
	}

	// Token: 0x06000117 RID: 279 RVA: 0x0000B0A4 File Offset: 0x0000A0A4
	public sprᤑ ᜀ(int A_0)
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
		return this.ᜀ[A_0] as sprᤑ;
	}

	// Token: 0x06000118 RID: 280 RVA: 0x0000B0F0 File Offset: 0x0000A0F0
	public void ᜀ(int A_0, sprᤑ A_1)
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
		this.ᜀ[A_0] = A_1;
	}

	// Token: 0x06000119 RID: 281 RVA: 0x0000B138 File Offset: 0x0000A138
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
		return this.ᜀ.Count;
	}

	// Token: 0x04000055 RID: 85
	private ArrayList ᜀ = new ArrayList();
}
