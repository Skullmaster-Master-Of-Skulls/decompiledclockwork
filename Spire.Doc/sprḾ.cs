using System;

// Token: 0x02000138 RID: 312
[CLSCompliant(false)]
internal class sprḾ : spr\u23FC
{
	// Token: 0x060007BF RID: 1983 RVA: 0x00058A3C File Offset: 0x00057A3C
	internal sprḾ(spr\u1DD0 A_0) : base(A_0)
	{
	}

	// Token: 0x060007C0 RID: 1984 RVA: 0x00058A50 File Offset: 0x00057A50
	internal override int ᜀ(int A_0)
	{
		int num;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_119;
			default:
			{
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜂ = A_0;
				num = this.ᜈ.ᜁ().\u1774() + this.ᜈ.ᜁ().ព() + this.ᜈ.ᜁ().\u1753() + this.ᜈ.ᜁ().ណ() + this.ᜈ.ᜁ().ᝋ() + this.ᜈ.ᜁ().\u1752();
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_119;
					case 1:
						this.ᜀ = (int)this.ᜈ.ᜀ().ᜁ((uint)num);
						this.ᜇ = (int)this.ᜈ.ᜀ().ᜁ((uint)(num + this.ᜈ.ᜁ().\u17D1()));
						num2 = 0;
						continue;
					case 2:
						if (this.ᜀ == 0)
						{
							num2 = 1;
							continue;
						}
						goto IL_11B;
					}
					break;
				}
				break;
			}
			}
		}
		IL_119:
		IL_11B:
		this.ᜆ = (int)this.ᜈ.ᜀ().ᜁ((uint)(num + this.ᜈ.ᜀ().ᜐ().ᜀ(true, this.ᜂ)));
		this.ᜁ = (int)this.ᜈ.ᜀ().ᜁ((uint)(num + this.ᜈ.ᜀ().ᜐ().ᜀ(true, this.ᜂ + 1)));
		base.ᜏ();
		return this.ᜆ;
	}

	// Token: 0x060007C1 RID: 1985 RVA: 0x00058BF0 File Offset: 0x00057BF0
	internal override bool ᜀ(long A_0)
	{
		uint num2;
		bool flag;
		for (;;)
		{
			this.ᜈ.ᜁ().\u17B9();
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_E4;
				case 1:
					num2 += (uint)this.ᜈ.ᜁ().\u1752();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return false;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 2:
					if (flag)
					{
						num = 1;
						continue;
					}
					goto IL_61;
				case 3:
					num2 = (uint)(this.ᜈ.ᜁ().\u1774() + this.ᜈ.ᜁ().ព() + this.ᜈ.ᜁ().\u1753() + this.ᜈ.ᜁ().ណ() + this.ᜈ.ᜁ().ᝋ());
					flag = (this != null);
					num = 2;
					continue;
				case 4:
					if (A_0 >= (long)this.ᜁ)
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					return false;
				}
				break;
			}
		}
		IL_61:
		this.ᜂ++;
		this.ᜁ = (int)this.ᜈ.ᜀ().ᜁ((uint)((ulong)num2 + (ulong)((long)this.ᜈ.ᜀ().ᜐ().ᜀ(flag, this.ᜂ + 1))));
		return true;
		IL_E4:
		goto IL_61;
	}
}
