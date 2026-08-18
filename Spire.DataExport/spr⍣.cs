using System;
using System.Collections;
using System.Reflection;

// Token: 0x02000092 RID: 146
[DefaultMember("Item")]
internal class spr\u2363 : IEnumerable
{
	// Token: 0x06000476 RID: 1142 RVA: 0x0002B9CC File Offset: 0x0002A9CC
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

	// Token: 0x06000477 RID: 1143 RVA: 0x0002BA14 File Offset: 0x0002AA14
	public int ᜀ(sprḚ A_0)
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

	// Token: 0x06000478 RID: 1144 RVA: 0x0002BA5C File Offset: 0x0002AA5C
	public int ᜁ(int A_0, string A_1)
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
					num2 = 4;
					continue;
				case 1:
					goto IL_B7;
				case 2:
					goto IL_EB;
				case 3:
					goto IL_72;
				case 4:
					if (this.ᜀ(num).ᜂ() != null)
					{
						num2 = 3;
						continue;
					}
					goto IL_46;
				case 5:
					if (this.ᜀ(num).ᜁ() != A_0)
					{
						goto IL_46;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_EB;
					default:
						if (false)
						{
						}
						num2 = 7;
						continue;
					}
					break;
				case 6:
					if (num >= this.ᜁ())
					{
						num2 = 8;
						continue;
					}
					num2 = 5;
					continue;
				case 7:
					if (true)
					{
					}
					num2 = 2;
					continue;
				case 8:
					return 15;
				case 9:
					goto IL_B7;
				}
				break;
				IL_46:
				num++;
				num2 = 9;
				continue;
				IL_B7:
				num2 = 6;
				continue;
				IL_EB:
				if (string.Compare(this.ᜀ(num).ᜀ(), A_1, true) != 0)
				{
					goto IL_46;
				}
				num2 = 0;
			}
		}
		IL_72:
		return (int)this.ᜀ(num).ᜂ().ᜀ();
	}

	// Token: 0x06000479 RID: 1145 RVA: 0x0002BB90 File Offset: 0x0002AB90
	public int ᜀ(int A_0, string A_1)
	{
		int num;
		for (;;)
		{
			num = 0;
			int num2 = 8;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num >= this.ᜁ())
					{
						num2 = 9;
						continue;
					}
					num2 = 6;
					continue;
				case 1:
					if (this.ᜀ(num).ᜂ() != null)
					{
						num2 = 2;
						continue;
					}
					goto IL_46;
				case 2:
					return num;
				case 3:
					num2 = 4;
					continue;
				case 4:
					goto IL_E3;
				case 5:
					num2 = 1;
					continue;
				case 6:
					if (this.ᜀ(num).ᜁ() != A_0)
					{
						goto IL_46;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E3;
					default:
						if (false)
						{
						}
						num2 = 3;
						continue;
					}
					break;
				case 7:
					goto IL_B7;
				case 8:
					goto IL_B7;
				case 9:
					return -1;
				}
				break;
				IL_46:
				num++;
				num2 = 7;
				continue;
				IL_B7:
				num2 = 0;
				continue;
				IL_E3:
				if (true)
				{
				}
				if (string.Compare(this.ᜀ(num).ᜀ(), A_1, true) != 0)
				{
					goto IL_46;
				}
				num2 = 5;
			}
		}
		return num;
	}

	// Token: 0x0600047A RID: 1146 RVA: 0x0002BCB4 File Offset: 0x0002ACB4
	public sprḚ ᜀ(int A_0)
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
		return this.ᜀ[A_0] as sprḚ;
	}

	// Token: 0x0600047B RID: 1147 RVA: 0x0002BD00 File Offset: 0x0002AD00
	public void ᜀ(int A_0, sprḚ A_1)
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

	// Token: 0x0600047C RID: 1148 RVA: 0x0002BD48 File Offset: 0x0002AD48
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

	// Token: 0x040002C1 RID: 705
	private ArrayList ᜀ = new ArrayList();
}
