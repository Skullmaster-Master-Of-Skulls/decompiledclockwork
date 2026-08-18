using System;

// Token: 0x02000132 RID: 306
[CLSCompliant(false)]
internal class spr\u2612 : spr\u23FC
{
	// Token: 0x060007A5 RID: 1957 RVA: 0x000579CC File Offset: 0x000569CC
	internal new int ᜁ()
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
		return this.ᜁ;
	}

	// Token: 0x060007A6 RID: 1958 RVA: 0x00057A10 File Offset: 0x00056A10
	internal new spr\u20CB ᜀ()
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
		return this.ᜈ.ᜃ(this.ᜁ);
	}

	// Token: 0x060007A7 RID: 1959 RVA: 0x00057A5C File Offset: 0x00056A5C
	internal spr\u2612(spr\u1DD0 A_0) : base(A_0)
	{
	}

	// Token: 0x060007A8 RID: 1960 RVA: 0x00057A70 File Offset: 0x00056A70
	internal new bool ᜀ(out int A_0)
	{
		bool result;
		for (;;)
		{
			result = true;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_9B;
				case 1:
					if (true)
					{
					}
					this.ᜁ++;
					goto IL_90;
				case 2:
					if (this.ᜁ < base.ᜆ().ᜄ().ᜀ().Length - 2)
					{
						num = 1;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_90;
					default:
						if (false)
						{
						}
						result = false;
						num = 3;
						continue;
					}
					break;
				case 3:
					goto IL_78;
				}
				break;
				IL_90:
				num = 0;
			}
		}
		IL_78:
		IL_9B:
		A_0 = base.ᜆ().ᜄ().ᜀ()[this.ᜁ + 1];
		return result;
	}

	// Token: 0x060007A9 RID: 1961 RVA: 0x00057B38 File Offset: 0x00056B38
	internal new bool ᜃ(long A_0)
	{
		bool result;
		for (;;)
		{
			result = false;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					if (true)
					{
					}
					int a_;
					if (this.ᜀ(out a_))
					{
						goto IL_B6;
					}
					this.ᜀ = -1;
					num = 4;
					continue;
				}
				case 1:
				{
					int a_;
					this.ᜀ = (int)base.ᜆ().ᜁ((uint)a_);
					result = true;
					num = 5;
					continue;
				}
				case 2:
					if (A_0 >= (long)this.ᜀ)
					{
						num = 3;
						continue;
					}
					return result;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B6;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 4:
					return result;
				case 5:
					return result;
				}
				break;
				IL_B6:
				num = 1;
			}
		}
		return result;
	}

	// Token: 0x060007AA RID: 1962 RVA: 0x00057C0C File Offset: 0x00056C0C
	internal override void ᜂ()
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
		base.ᜂ();
		this.ᜀ = (int)base.ᜆ().ᜁ((uint)base.ᜆ().ᜄ().ᜀ()[1]);
	}

	// Token: 0x060007AB RID: 1963 RVA: 0x00057C70 File Offset: 0x00056C70
	internal override long ᜂ(long A_0)
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
		return Math.Min(base.ᜂ(A_0), (long)this.ᜀ);
	}

	// Token: 0x0400113D RID: 4413
	private new int ᜀ;

	// Token: 0x0400113E RID: 4414
	private new int ᜁ;
}
