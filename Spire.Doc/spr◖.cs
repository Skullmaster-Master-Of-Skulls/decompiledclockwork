using System;

// Token: 0x02000136 RID: 310
[CLSCompliant(false)]
internal class spr\u25D6 : spr\u23FC
{
	// Token: 0x060007B9 RID: 1977 RVA: 0x000584D0 File Offset: 0x000574D0
	internal spr\u25D6(spr\u1DD0 A_0) : base(A_0)
	{
	}

	// Token: 0x060007BA RID: 1978 RVA: 0x000584E4 File Offset: 0x000574E4
	internal override int ᜀ(int A_0)
	{
		int num;
		for (;;)
		{
			IL_42:
			this.ᜂ = A_0;
			num = this.ᜈ.ᜁ().\u1774() + this.ᜈ.ᜁ().ព() + this.ᜈ.ᜁ().ណ() + this.ᜈ.ᜁ().\u1753();
			int num2 = 2;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_42;
				}
				if (false)
				{
				}
				if (true)
				{
				}
				switch (num2)
				{
				case 0:
					goto IL_F7;
				case 1:
					this.ᜀ = (int)this.ᜈ.ᜀ().ᜁ((uint)num);
					this.ᜇ = (int)this.ᜈ.ᜀ().ᜁ((uint)(num + this.ᜈ.ᜁ().ᝋ()));
					num2 = 0;
					continue;
				case 2:
					if (this.ᜀ == 0)
					{
						num2 = 1;
						continue;
					}
					goto IL_F9;
				}
				break;
			}
		}
		IL_F7:
		IL_F9:
		this.ᜆ = (int)this.ᜈ.ᜀ().ᜁ((uint)(num + this.ᜈ.ᜀ().\u1719().ᜊ(this.ᜂ)));
		this.ᜁ = (int)this.ᜈ.ᜀ().ᜁ((uint)(num + this.ᜈ.ᜀ().\u1719().ᜊ(this.ᜂ + 1)));
		base.ᜏ();
		return this.ᜆ;
	}

	// Token: 0x060007BB RID: 1979 RVA: 0x00058660 File Offset: 0x00057660
	internal override bool ᜀ(long A_0)
	{
		while (A_0 >= (long)this.ᜁ)
		{
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (false)
				{
				}
				uint num = (uint)(this.ᜈ.ᜁ().\u1774() + this.ᜈ.ᜁ().ព() + this.ᜈ.ᜁ().ណ() + this.ᜈ.ᜁ().\u1753());
				this.ᜂ++;
				this.ᜁ = (int)this.ᜈ.ᜀ().ᜁ((uint)((ulong)num + (ulong)((long)this.ᜈ.ᜀ().\u1719().ᜊ(this.ᜂ + 1))));
				return true;
			}
			}
		}
		return false;
	}
}
