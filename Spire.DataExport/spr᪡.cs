using System;
using System.Collections;
using System.Reflection;
using Spire.DataExport.CollectionEditors;

// Token: 0x0200014E RID: 334
[DefaultMember("Item")]
internal class spr\u1AA1 : IEnumerable
{
	// Token: 0x06000833 RID: 2099 RVA: 0x000526C4 File Offset: 0x000516C4
	public IEnumerator ᜃ()
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

	// Token: 0x06000834 RID: 2100 RVA: 0x0005270C File Offset: 0x0005170C
	public int ᜀ(spr\u2266 A_0)
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

	// Token: 0x06000835 RID: 2101 RVA: 0x00052754 File Offset: 0x00051754
	public int ᜀ(string A_0)
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
					if (string.Compare(A_0, this.ᜀ(num).ᜁ(), true) == 0)
					{
						num2 = 4;
						continue;
					}
					num++;
					num2 = 5;
					continue;
				case 1:
					if (true)
					{
					}
					if (num >= this.ᜀ.Count)
					{
						num2 = 3;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return -1;
					default:
						if (false)
						{
						}
						num2 = 0;
						continue;
					}
					break;
				case 2:
					goto IL_89;
				case 3:
					return -1;
				case 4:
					return num;
				case 5:
					goto IL_89;
				}
				break;
				IL_89:
				num2 = 1;
			}
		}
		return num;
	}

	// Token: 0x06000836 RID: 2102 RVA: 0x0005281C File Offset: 0x0005181C
	public int ᜂ()
	{
		int num;
		for (;;)
		{
			num = -1;
			int num2 = 0;
			int num3 = 4;
			for (;;)
			{
				IL_02:
				switch (num3)
				{
				case 0:
					goto IL_5A;
				case 1:
					return num;
				case 2:
					if (num != -1)
					{
						num3 = 3;
						continue;
					}
					return num;
				case 3:
					num++;
					num3 = 1;
					continue;
				case 4:
					goto IL_E0;
				case 5:
					while (this.ᜀ(num2).ᜃ() > num)
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
							num3 = 6;
							goto IL_02;
						}
					}
					goto IL_5A;
				case 6:
					num = this.ᜀ(num2).ᜃ();
					num3 = 0;
					continue;
				case 7:
					num3 = 2;
					continue;
				case 8:
					goto IL_E0;
				case 9:
					if (num2 >= this.ᜀ.Count)
					{
						num3 = 7;
						continue;
					}
					num3 = 5;
					continue;
				}
				break;
				IL_5A:
				num2++;
				num3 = 8;
				continue;
				IL_E0:
				if (true)
				{
				}
				num3 = 9;
			}
		}
		return num;
	}

	// Token: 0x06000837 RID: 2103 RVA: 0x00052940 File Offset: 0x00051940
	public spr\u2266 ᜀ(int A_0)
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
		return this.ᜀ[A_0] as spr\u2266;
	}

	// Token: 0x06000838 RID: 2104 RVA: 0x0005298C File Offset: 0x0005198C
	public void ᜀ(int A_0, spr\u2266 A_1)
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

	// Token: 0x06000839 RID: 2105 RVA: 0x000529D4 File Offset: 0x000519D4
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

	// Token: 0x0600083A RID: 2106 RVA: 0x00052A1C File Offset: 0x00051A1C
	public int ᜀ()
	{
		int a_ = 5;
		int num;
		for (;;)
		{
			num = HyperlinksCollectionEditor.b("娠缢䌤䠦䜨弪夬䴮崰", a_).Length + 2;
			int num2 = 0;
			int num3 = 2;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_43;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						if (num2 >= this.ᜀ.Count)
						{
							num3 = 3;
							continue;
						}
						num += this.ᜀ(num2).ᜂ();
						num2++;
						num3 = 1;
						continue;
					}
					break;
				case 1:
					goto IL_43;
				case 2:
					goto IL_43;
				case 3:
					return num;
				}
				break;
				IL_43:
				num3 = 0;
			}
		}
		return num;
	}

	// Token: 0x04000637 RID: 1591
	private ArrayList ᜀ = new ArrayList();
}
