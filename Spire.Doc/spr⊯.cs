using System;
using System.Collections;

// Token: 0x02000379 RID: 889
internal abstract class spr\u22AF
{
	// Token: 0x060031E8 RID: 12776 RVA: 0x002E1D1C File Offset: 0x002E0D1C
	internal spr\u22AF(sprά A_0, Hashtable A_1)
	{
		this.ᜁ = A_0;
		this.ᜂ = A_1;
	}

	// Token: 0x060031E9 RID: 12777 RVA: 0x002E1D40 File Offset: 0x002E0D40
	protected spr\u24A6 ᜁ(spr\u1F9B A_0, bool A_1)
	{
		spr\u24A6 result;
		for (;;)
		{
			result = null;
			spr\u2000 spr_u = (spr\u2000)this.ᜂ[A_0.ᜁ()];
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_60;
				case 1:
					IL_5E:
					A_0.ᜀ(spr_u.ᜂ());
					num = 4;
					continue;
				case 2:
					if (spr_u != null)
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					goto IL_CA;
				case 3:
					goto IL_CA;
				case 4:
					if (A_1)
					{
						num = 5;
						continue;
					}
					goto IL_60;
				case 5:
					A_0.ᜀ(spr_u.ᜂ().Size);
					num = 0;
					continue;
				}
				break;
				IL_CA:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_5E;
				default:
					goto IL_E0;
				}
				IL_60:
				A_0.ᜀ(spr_u.ᜁ());
				result = spr_u.ᜀ().ᜂ();
				num = 3;
			}
		}
		IL_E0:
		if (false)
		{
		}
		return result;
	}

	// Token: 0x060031EA RID: 12778 RVA: 0x002E1E34 File Offset: 0x002E0E34
	protected void ᜀ(sprᩍ A_0, spr\u2000 A_1)
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
		this.ᜂ[A_0] = A_1;
	}

	// Token: 0x060031EB RID: 12779 RVA: 0x002E1E7C File Offset: 0x002E0E7C
	protected void ᜁ(sprᩍ A_0)
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
		this.ᜂ[A_0] = null;
	}

	// Token: 0x04002734 RID: 10036
	internal const float ᜀ = 21600f;

	// Token: 0x04002735 RID: 10037
	protected readonly sprά ᜁ;

	// Token: 0x04002736 RID: 10038
	private readonly Hashtable ᜂ;
}
