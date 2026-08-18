using System;

// Token: 0x02000135 RID: 309
[CLSCompliant(false)]
internal class spr\u21E0 : spr\u23FC
{
	// Token: 0x060007B6 RID: 1974 RVA: 0x00058294 File Offset: 0x00057294
	internal spr\u21E0(spr\u1DD0 A_0) : base(A_0)
	{
	}

	// Token: 0x060007B7 RID: 1975 RVA: 0x000582A8 File Offset: 0x000572A8
	internal override int ᜀ(int A_0)
	{
		int num;
		for (;;)
		{
			this.ᜂ = A_0;
			num = this.ᜈ.ᜁ().\u1774() + this.ᜈ.ᜁ().ព() + this.ᜈ.ᜁ().ណ();
			if (true)
			{
			}
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					for (;;)
					{
						this.ᜀ = (int)this.ᜈ.ᜀ().ᜁ((uint)num);
						this.ᜇ = (int)this.ᜈ.ᜀ().ᜁ((uint)(num + this.ᜈ.ᜁ().\u1753()));
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_C5;
						}
					}
					IL_C5:
					if (false)
					{
					}
					num2 = 1;
					continue;
				case 1:
					goto IL_D6;
				case 2:
					if (this.ᜀ == 0)
					{
						num2 = 0;
						continue;
					}
					goto IL_E2;
				}
				break;
			}
		}
		IL_D6:
		IL_E2:
		this.ᜆ = (int)this.ᜈ.ᜀ().ᜁ((uint)(num + this.ᜈ.ᜀ().ᜉ().ᜊ(this.ᜂ)));
		this.ᜁ = (int)this.ᜈ.ᜀ().ᜁ((uint)(num + this.ᜈ.ᜀ().ᜉ().ᜊ(this.ᜂ + 1)));
		base.ᜏ();
		return this.ᜆ;
	}

	// Token: 0x060007B8 RID: 1976 RVA: 0x0005840C File Offset: 0x0005740C
	internal override bool ᜀ(long A_0)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			if (A_0 < (long)this.ᜁ)
			{
				return false;
			}
			break;
		}
		uint num = (uint)(this.ᜈ.ᜁ().\u1774() + this.ᜈ.ᜁ().ព() + this.ᜈ.ᜁ().ណ());
		this.ᜂ++;
		this.ᜁ = (int)this.ᜈ.ᜀ().ᜁ((uint)((ulong)num + (ulong)((long)this.ᜈ.ᜀ().ᜉ().ᜊ(this.ᜂ + 1))));
		return true;
	}
}
