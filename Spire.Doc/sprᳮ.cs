using System;
using System.Collections.Generic;

// Token: 0x02000147 RID: 327
internal class sprᳮ
{
	// Token: 0x060008CE RID: 2254 RVA: 0x000703C0 File Offset: 0x0006F3C0
	public sprᳮ()
	{
	}

	// Token: 0x060008CF RID: 2255 RVA: 0x000703EC File Offset: 0x0006F3EC
	public sprᳮ(float A_0)
	{
		this.ᜁ = A_0;
	}

	// Token: 0x060008D0 RID: 2256 RVA: 0x0007041C File Offset: 0x0006F41C
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

	// Token: 0x060008D1 RID: 2257 RVA: 0x00070464 File Offset: 0x0006F464
	public void ᜀ(float A_0)
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

	// Token: 0x060008D2 RID: 2258 RVA: 0x000704A8 File Offset: 0x0006F4A8
	public float ᜂ()
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
		return this.ᜀ();
	}

	// Token: 0x060008D3 RID: 2259 RVA: 0x000704EC File Offset: 0x0006F4EC
	public float ᜀ(string A_0)
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
		return this.ᜀ[A_0].ᜁ();
	}

	// Token: 0x060008D4 RID: 2260 RVA: 0x00070538 File Offset: 0x0006F538
	public spr\u2569 ᜁ(string A_0)
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
		return this.ᜀ[A_0];
	}

	// Token: 0x060008D5 RID: 2261 RVA: 0x00070580 File Offset: 0x0006F580
	private float ᜀ()
	{
		float num = 0f;
		using (Dictionary<string, spr\u2569>.Enumerator enumerator = this.ᜀ.GetEnumerator())
		{
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_87;
				case 1:
					goto IL_A5;
				case 3:
					num2 = 1;
					continue;
				case 4:
					goto IL_7F;
				}
				goto IL_38;
				IL_87:
				if (!enumerator.MoveNext())
				{
					num2 = 3;
					continue;
				}
				KeyValuePair<string, spr\u2569> keyValuePair = enumerator.Current;
				num += keyValuePair.Value.ᜁ();
				num2 = 4;
				continue;
				IL_7F:
				num2 = 0;
				continue;
				IL_38:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_87;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					goto IL_7F;
				}
			}
			IL_A5:;
		}
		return num;
	}

	// Token: 0x060008D6 RID: 2262 RVA: 0x00070660 File Offset: 0x0006F660
	public void ᜀ(string A_0, spr\u2569 A_1)
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
		this.ᜀ.Add(A_0, A_1);
	}

	// Token: 0x04001358 RID: 4952
	private Dictionary<string, spr\u2569> ᜀ = new Dictionary<string, spr\u2569>();

	// Token: 0x04001359 RID: 4953
	private float ᜁ;

	// Token: 0x0400135A RID: 4954
	private float ᜂ = 1f;
}
