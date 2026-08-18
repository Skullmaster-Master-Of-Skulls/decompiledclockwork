using System;

// Token: 0x02000137 RID: 311
[CLSCompliant(false)]
internal class spr\u25DF : spr\u23FC
{
	// Token: 0x060007BC RID: 1980 RVA: 0x00058738 File Offset: 0x00057738
	internal spr\u25DF(spr\u1DD0 A_0) : base(A_0)
	{
	}

	// Token: 0x060007BD RID: 1981 RVA: 0x0005874C File Offset: 0x0005774C
	internal override int ᜀ(int A_0)
	{
		uint num;
		for (;;)
		{
			this.ᜂ = A_0;
			num = (uint)(this.ᜈ.ᜁ().\u1774() + this.ᜈ.ᜁ().ព() + this.ᜈ.ᜁ().\u1753() + this.ᜈ.ᜁ().ណ() + this.ᜈ.ᜁ().ᝋ());
			if (true)
			{
			}
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_BC:
				this.ᜀ = (int)this.ᜈ.ᜀ().ᜁ(num);
				this.ᜇ = (int)this.ᜈ.ᜀ().ᜁ((uint)((ulong)num + (ulong)((long)this.ᜈ.ᜁ().\u1752())));
				num2 = 0;
				break;
			default:
				if (false)
				{
				}
				num2 = 1;
				break;
			}
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_10B;
				case 1:
					if (this.ᜀ == 0)
					{
						num2 = 2;
						continue;
					}
					goto IL_10D;
				case 2:
					goto IL_BC;
				}
				break;
			}
		}
		IL_10B:
		IL_10D:
		this.ᜆ = (int)this.ᜈ.ᜀ().ᜁ((uint)((ulong)num + (ulong)((long)this.ᜈ.ᜀ().ᜐ().ᜀ(false, this.ᜂ))));
		this.ᜁ = (int)this.ᜈ.ᜀ().ᜁ((uint)((ulong)num + (ulong)((long)this.ᜈ.ᜀ().ᜐ().ᜀ(false, this.ᜂ + 1))));
		base.ᜏ();
		return this.ᜆ;
	}

	// Token: 0x060007BE RID: 1982 RVA: 0x000588E4 File Offset: 0x000578E4
	internal override bool ᜀ(long A_0)
	{
		uint num;
		bool flag;
		int num2;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_5D:
			num = (uint)(this.ᜈ.ᜁ().\u1774() + this.ᜈ.ᜁ().ព() + this.ᜈ.ᜁ().\u1753() + this.ᜈ.ᜁ().ណ() + this.ᜈ.ᜁ().ᝋ());
			flag = false;
			num2 = 2;
			break;
		default:
			if (false)
			{
			}
			num2 = 1;
			break;
		}
		for (;;)
		{
			switch (num2)
			{
			case 0:
				goto IL_5D;
			case 2:
				if (flag)
				{
					num2 = 3;
					continue;
				}
				goto IL_5F;
			case 3:
				num += (uint)this.ᜈ.ᜁ().\u1752();
				num2 = 4;
				continue;
			case 4:
				goto IL_C6;
			}
			if (true)
			{
			}
			if (A_0 < (long)this.ᜁ)
			{
				return false;
			}
			num2 = 0;
		}
		IL_5F:
		this.ᜂ++;
		this.ᜁ = (int)this.ᜈ.ᜀ().ᜁ((uint)((ulong)num + (ulong)((long)this.ᜈ.ᜀ().ᜐ().ᜀ(flag, this.ᜂ + 1))));
		return true;
		IL_C6:
		goto IL_5F;
	}
}
