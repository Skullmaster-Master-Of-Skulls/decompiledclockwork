using System;

// Token: 0x02000133 RID: 307
[CLSCompliant(false)]
internal class spr\u2351 : spr\u23FC
{
	// Token: 0x060007AC RID: 1964 RVA: 0x00057CC0 File Offset: 0x00056CC0
	internal spr\u2351(spr\u1DD0 A_0) : base(A_0)
	{
	}

	// Token: 0x060007AD RID: 1965 RVA: 0x00057CD4 File Offset: 0x00056CD4
	internal override int ᜀ(int A_0)
	{
		int num;
		for (;;)
		{
			this.ᜂ = A_0;
			num = this.ᜈ.ᜁ().\u1774();
			if (true)
			{
			}
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (this.ᜀ != 0)
					{
						goto IL_C0;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_72;
					default:
						if (false)
						{
						}
						num2 = 1;
						continue;
					}
					break;
				case 1:
					goto IL_72;
				case 2:
					goto IL_BE;
				}
				break;
				IL_72:
				this.ᜀ = (int)this.ᜈ.ᜀ().ᜁ((uint)num);
				this.ᜇ = (int)this.ᜈ.ᜀ().ᜁ((uint)(num + this.ᜈ.ᜁ().ព()));
				num2 = 2;
			}
		}
		IL_BE:
		IL_C0:
		this.ᜆ = (int)this.ᜈ.ᜀ().ᜁ((uint)(num + this.ᜈ.ᜀ().ᜡ().ᜊ(this.ᜂ)));
		this.ᜁ = (int)this.ᜈ.ᜀ().ᜁ((uint)(num + this.ᜈ.ᜀ().ᜡ().ᜊ(this.ᜂ + 1)));
		base.ᜏ();
		return this.ᜆ;
	}

	// Token: 0x060007AE RID: 1966 RVA: 0x00057E18 File Offset: 0x00056E18
	internal override bool ᜀ(long A_0)
	{
		if (A_0 >= (long)this.ᜁ)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (true)
				{
				}
				if (false)
				{
				}
				uint num = (uint)this.ᜈ.ᜁ().\u1774();
				this.ᜂ++;
				this.ᜁ = (int)this.ᜈ.ᜀ().ᜁ((uint)((ulong)num + (ulong)((long)this.ᜈ.ᜀ().ᜡ().ᜊ(this.ᜂ + 1))));
				return true;
			}
			}
		}
		return false;
	}
}
