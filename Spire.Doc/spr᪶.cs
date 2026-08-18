using System;
using Spire.Doc.Core;

// Token: 0x02000134 RID: 308
[CLSCompliant(false)]
internal class spr\u1AB6 : spr\u23FC
{
	// Token: 0x060007AF RID: 1967 RVA: 0x00057EBC File Offset: 0x00056EBC
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
		return this.ᜀ;
	}

	// Token: 0x060007B0 RID: 1968 RVA: 0x00057F00 File Offset: 0x00056F00
	internal new void ᜁ(int A_0)
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
		this.ᜀ = A_0;
	}

	// Token: 0x060007B1 RID: 1969 RVA: 0x00057F44 File Offset: 0x00056F44
	internal spr\u1AB6(spr\u1DD0 A_0) : base(A_0)
	{
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x00057F58 File Offset: 0x00056F58
	internal override int ᜀ(int A_0)
	{
		int num;
		for (;;)
		{
			this.ᜂ = 6 + this.ᜀ * 6 + A_0;
			num = this.ᜈ.ᜁ().\u1774() + this.ᜈ.ᜁ().ព();
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
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_DA;
					case 1:
						this.ᜀ = (int)this.ᜈ.ᜀ().ᜁ((uint)num);
						this.ᜇ = (int)this.ᜈ.ᜀ().ᜁ((uint)(num + this.ᜈ.ᜁ().ណ()));
						if (true)
						{
						}
						num2 = 0;
						continue;
					case 2:
						if (this.ᜀ == 0)
						{
							num2 = 1;
							continue;
						}
						goto IL_DC;
					}
					break;
				}
				break;
			}
			}
		}
		IL_DA:
		IL_DC:
		this.ᜆ = (int)this.ᜈ.ᜀ().ᜁ((uint)(num + this.ᜈ.ᜀ().\u1714().ᜀ()[this.ᜂ]));
		this.ᜁ = (int)this.ᜈ.ᜀ().ᜁ((uint)(num + this.ᜈ.ᜀ().\u1714().ᜀ()[this.ᜂ + 1]));
		base.ᜏ();
		return this.ᜆ;
	}

	// Token: 0x060007B3 RID: 1971 RVA: 0x000580B8 File Offset: 0x000570B8
	internal new void ᜀ()
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
		this.ᜂ++;
		int num = this.ᜈ.ᜁ().\u1774() + this.ᜈ.ᜁ().ព();
		this.ᜁ = (int)this.ᜈ.ᜀ().ᜁ((uint)(num + this.ᜈ.ᜀ().\u1714().ᜀ()[this.ᜂ + 1]));
	}

	// Token: 0x060007B4 RID: 1972 RVA: 0x0005815C File Offset: 0x0005715C
	internal new bool ᜀ(long A_0, HeaderType A_1)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				A_1++;
				this.ᜀ();
				num = 2;
				continue;
			case 2:
				goto IL_DE;
			case 3:
				goto IL_58;
			case 4:
				if (this.ᜁ)
				{
					num = 0;
					continue;
				}
				num = 5;
				continue;
			case 5:
				if (A_0 >= (long)this.ᜁ)
				{
					num = 6;
					continue;
				}
				goto IL_E0;
			case 6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					this.ᜁ = -1;
					num = 3;
					continue;
				}
				break;
			}
			if (A_0 < (long)this.ᜁ)
			{
				goto IL_60;
			}
			if (A_1 >= HeaderType.FirstPageFooter)
			{
				goto IL_60;
			}
			bool flag = true;
			IL_9B:
			this.ᜁ = flag;
			if (true)
			{
			}
			num = 4;
			continue;
			IL_60:
			flag = false;
			goto IL_9B;
		}
		IL_58:
		IL_DE:
		IL_E0:
		return this.ᜁ;
	}

	// Token: 0x060007B5 RID: 1973 RVA: 0x00058250 File Offset: 0x00057250
	internal override bool ᜁ(long A_0)
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

	// Token: 0x0400113F RID: 4415
	private new int ᜀ;

	// Token: 0x04001140 RID: 4416
	private new bool ᜁ;
}
