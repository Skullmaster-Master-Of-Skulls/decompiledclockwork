using System;
using System.Collections;
using System.Drawing;

// Token: 0x0200037B RID: 891
internal class spr\u2287 : sprᢿ
{
	// Token: 0x060031F9 RID: 12793 RVA: 0x002E2208 File Offset: 0x002E1208
	internal static spr\u1B70 ᜁ(spr\u1B70 A_0)
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
		spr\u2287 spr_u = new spr\u2287();
		return spr_u.ᜃ(A_0);
	}

	// Token: 0x060031FA RID: 12794 RVA: 0x002E2250 File Offset: 0x002E1250
	internal spr\u1B70 ᜃ(spr\u1B70 A_0)
	{
		spr\u1B70 spr_u1B;
		for (;;)
		{
			spr_u1B = A_0.ᜀ(false);
			int num = 3;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					return spr_u1B;
				case 1:
					goto IL_D6;
				case 2:
					goto IL_D6;
				case 3:
					goto IL_30;
				case 4:
				{
					if (num2 < 0)
					{
						num = 0;
						continue;
					}
					spr\u1926 spr_u = (spr\u1926)A_0.ᜀ(num2);
					this.ᜀ = new spr\u1926();
					this.ᜀ.ᜀ(spr_u.ᜁ());
					spr_u.ᜀ(this);
					spr_u1B.ᜁ(this.ᜀ);
					num2--;
					num = 2;
					continue;
				}
				case 5:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_30;
					default:
						goto IL_CE;
					}
					break;
				}
				break;
				IL_30:
				if (A_0.ᜉ() == 0)
				{
					num = 5;
					continue;
				}
				num2 = A_0.ᜉ() - 1;
				num = 1;
				continue;
				IL_D6:
				num = 4;
			}
		}
		IL_CE:
		if (false)
		{
		}
		return spr_u1B;
	}

	// Token: 0x060031FB RID: 12795 RVA: 0x002E2354 File Offset: 0x002E1354
	public override void ᜀ(sprᴎ A_0)
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
		ArrayList arrayList = (ArrayList)A_0.ᜀ().Clone();
		arrayList.Reverse();
		sprᴎ a_ = new sprᴎ((PointF[])arrayList.ToArray(typeof(PointF)));
		this.ᜀ.ᜀ(0, a_);
	}

	// Token: 0x060031FC RID: 12796 RVA: 0x002E23D0 File Offset: 0x002E13D0
	public override void ᜀ(spr\u17F0 A_0)
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
		spr\u17F0 a_ = new spr\u17F0(A_0.ᜀ().ᜀ(), A_0.ᜀ().ᜃ(), A_0.ᜀ().ᜄ(), A_0.ᜀ().ᜂ());
		this.ᜀ.ᜀ(0, a_);
	}

	// Token: 0x0400273D RID: 10045
	private new spr\u1926 ᜀ;
}
