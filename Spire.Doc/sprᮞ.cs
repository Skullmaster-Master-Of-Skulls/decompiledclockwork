using System;
using Spire.CompoundFile.Doc;
using Spire.Doc.Core;

// Token: 0x020002A5 RID: 677
[CLSCompliant(false)]
internal class sprᮞ : sprᥕ
{
	// Token: 0x06002483 RID: 9347 RVA: 0x0024ED10 File Offset: 0x0024DD10
	internal sprᮞ(sprច A_0) : base(A_0)
	{
		this.\u1712 = WordSubdocument.HeaderFooter;
	}

	// Token: 0x06002484 RID: 9348 RVA: 0x0024ED2C File Offset: 0x0024DD2C
	internal new HeaderType ᜆ()
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

	// Token: 0x06002485 RID: 9349 RVA: 0x0024ED70 File Offset: 0x0024DD70
	internal void ᜀ(HeaderType A_0)
	{
		int a_ = 13;
		if (A_0 < this.ᜁ)
		{
			for (;;)
			{
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				break;
			}
			if (false)
			{
			}
			throw new ArgumentOutOfRangeException(string.Format(ClipboardData.b("㭲ၴᙶᵸṺོ⭾Ꞇﺊﺌﮎ놐랖ﺘﺞ햠욢힤螦쾨\ud9aa슬슮醰좲薴쪶", a_), this.ᜁ));
		}
		this.ᜁ(A_0);
	}

	// Token: 0x06002486 RID: 9350 RVA: 0x0024EDEC File Offset: 0x0024DDEC
	public virtual void ᜇ()
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
		this.ᜁ((HeaderType)6);
		base.ᜁ('\r');
		int num = this.\u171F();
		this.ᜂ.ᜃ().\u1713()[this.ᜂ] = num + 3;
	}

	// Token: 0x06002487 RID: 9351 RVA: 0x0024EE58 File Offset: 0x0024DE58
	internal new void ᜅ()
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
		this.ᜀ((HeaderType)6);
		this.ᜃ++;
		this.ᜂ = (this.ᜃ + 1) * 6 + 1;
		this.ᜁ = HeaderType.EvenHeader;
	}

	// Token: 0x06002488 RID: 9352 RVA: 0x0024EEC4 File Offset: 0x0024DEC4
	protected new void ᜁ(HeaderType A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 2:
				IL_154:
				goto IL_35;
			case 3:
			{
				if (true)
				{
				}
				base.ᜁ('\r');
				this.ᜁ++;
				int num2 = this.\u171F();
				this.ᜂ.ᜃ().\u1713()[this.ᜂ] = num2;
				num = 2;
				continue;
			}
			case 4:
			{
				int num3;
				if (num3 != this.ᜂ.ᜃ().\u1713()[this.ᜂ - 1])
				{
					num = 3;
					continue;
				}
				this.ᜁ++;
				int num4 = this.\u171F();
				this.ᜂ.ᜃ().\u1713()[this.ᜂ] = num4;
				num = 7;
				continue;
			}
			case 5:
			{
				if (this.ᜁ == A_0)
				{
					num = 6;
					continue;
				}
				int num3 = this.\u171F();
				num = 4;
				continue;
			}
			case 6:
				return;
			case 7:
				goto IL_35;
			}
			goto IL_30;
			IL_35:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_154;
			default:
				if (false)
				{
				}
				this.ᜂ++;
				num = 1;
				continue;
			}
			IL_F0:
			num = 5;
			continue;
			IL_30:
			goto IL_F0;
		}
	}

	// Token: 0x06002489 RID: 9353 RVA: 0x0024F02C File Offset: 0x0024E02C
	protected override void ᜀ(int A_0)
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
		sprᾱ sprᾱ = this.ᜂ.ᜀ();
		sprᾱ.ᜠ(sprᾱ.ណ() + A_0);
	}

	// Token: 0x0600248A RID: 9354 RVA: 0x0024F080 File Offset: 0x0024E080
	protected override void ᜂ()
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
		int num = this.ᜂ.ᜄ().ᜂ();
		this.ᜂ.ᜃ().ᜀ(new int[7 + num * 6 + 1]);
		this.ᜀ();
		this.ᜁ = HeaderType.EvenHeader;
		this.ᜌ = 4050;
		this.\u170D = 4500;
	}

	// Token: 0x0600248B RID: 9355 RVA: 0x0024F114 File Offset: 0x0024E114
	private void ᜀ()
	{
		for (;;)
		{
			IL_3E:
			int num = 0;
			int num2 = 3;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3E;
				default:
					if (false)
					{
					}
					switch (num2)
					{
					case 0:
						goto IL_66;
					case 1:
						if (true)
						{
						}
						if (num >= 6)
						{
							num2 = 0;
							continue;
						}
						this.ᜂ.ᜃ().\u1713()[num] = 0;
						num++;
						num2 = 2;
						continue;
					case 2:
						goto IL_4A;
					case 3:
						goto IL_4A;
					}
					goto IL_3E;
					IL_4A:
					num2 = 1;
					break;
				}
			}
		}
		IL_66:
		this.ᜂ = 7;
	}

	// Token: 0x040021B1 RID: 8625
	private new const int ᜀ = 7;

	// Token: 0x040021B2 RID: 8626
	protected new HeaderType ᜁ;

	// Token: 0x040021B3 RID: 8627
	private new int ᜂ;

	// Token: 0x040021B4 RID: 8628
	private new int ᜃ;
}
