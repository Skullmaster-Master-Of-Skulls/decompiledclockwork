using System;
using System.Drawing;
using Spire.CompoundFile.Doc;

// Token: 0x0200031A RID: 794
internal class sprᨋ
{
	// Token: 0x06002B35 RID: 11061 RVA: 0x002A6034 File Offset: 0x002A5034
	internal sprᨋ(sprᩍ A_0, sprᤎ A_1, spr\u21E4 A_2)
	{
		this.ᜀ = A_1;
		this.ᜁ = A_0;
		this.ᜂ = A_2;
	}

	// Token: 0x06002B36 RID: 11062 RVA: 0x002A605C File Offset: 0x002A505C
	internal void ᜀ(int A_0, object A_1)
	{
		for (;;)
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					return;
				case 2:
					num = 5;
					continue;
				case 3:
					num = 7;
					continue;
				case 4:
					if (A_0 <= 285)
					{
						num = 3;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_295;
					default:
						if (false)
						{
						}
						num = 9;
						continue;
					}
					break;
				case 5:
					switch (A_0)
					{
					case 4102:
						goto IL_27B;
					case 4103:
						goto IL_F7;
					case 4104:
						goto IL_112;
					default:
						num = 1;
						continue;
					}
					break;
				case 6:
					goto IL_13B;
				case 7:
					switch (A_0)
					{
					case 256:
						goto IL_288;
					case 257:
						goto IL_2CC;
					case 258:
						goto IL_104;
					case 259:
						goto IL_6D;
					case 260:
					case 261:
					case 262:
					case 267:
					case 269:
					case 270:
						return;
					case 263:
						this.ᜋ = spr\u23B0.ᜁ((Color)A_1);
						num = 6;
						continue;
					case 264:
						goto IL_26E;
					case 265:
						goto IL_140;
					case 266:
						goto IL_60;
					case 268:
						goto IL_237;
					default:
						num = 10;
						continue;
					}
					break;
				case 8:
					switch (A_0)
					{
					case 282:
						goto IL_A7;
					case 283:
					case 284:
					case 285:
						return;
					default:
						num = 0;
						continue;
					}
					break;
				case 9:
					switch (A_0)
					{
					case 313:
						goto IL_22A;
					case 314:
					case 315:
						return;
					case 316:
						goto IL_2D9;
					case 317:
						goto IL_E9;
					case 318:
						goto IL_21C;
					case 319:
						goto IL_1B0;
					default:
						num = 2;
						continue;
					}
					break;
				case 10:
					goto IL_295;
				}
				break;
				IL_295:
				num = 8;
			}
		}
		IL_60:
		this.ᜎ = sprṍ.ᜁ(A_1);
		return;
		IL_6D:
		this.ᜊ = sprṍ.ᜁ(A_1);
		return;
		IL_A7:
		if (true)
		{
		}
		Color color = Color.FromArgb((int)A_1);
		this.\u1715 = spr\u23B0.ᜁ(Color.FromArgb((int)color.A, (int)color.B, (int)color.G, (int)color.R));
		return;
		IL_E9:
		this.\u1712 = sprṍ.ᜁ(A_1, false);
		return;
		IL_F7:
		this.ᜅ = (string)A_1;
		return;
		IL_104:
		this.ᜉ = sprṍ.ᜁ(A_1);
		return;
		IL_112:
		this.ᜆ = (string)A_1;
		return;
		IL_13B:
		return;
		IL_140:
		this.\u170D = sprṍ.ᜁ(A_1);
		return;
		IL_1B0:
		this.\u1714 = sprṍ.ᜀ(A_1);
		return;
		IL_21C:
		this.\u1713 = sprṍ.ᜁ(A_1, false);
		return;
		IL_22A:
		this.ᜐ = sprṍ.ᜀ(A_1);
		return;
		IL_237:
		Color color2 = Color.FromArgb((int)A_1);
		this.ᜏ = spr\u23B0.ᜁ(Color.FromArgb(239, (int)color2.B, (int)color2.G, (int)color2.R));
		return;
		IL_26E:
		this.ᜌ = sprṍ.ᜁ(A_1);
		return;
		IL_27B:
		this.ᜃ = (byte[])A_1;
		return;
		IL_288:
		this.ᜇ = sprṍ.ᜁ(A_1);
		return;
		IL_2CC:
		this.ᜈ = sprṍ.ᜁ(A_1);
		return;
		IL_2D9:
		this.ᜑ = sprṍ.ᜀ(A_1);
	}

	// Token: 0x06002B37 RID: 11063 RVA: 0x002A6350 File Offset: 0x002A5350
	internal void ᜂ()
	{
		int a_ = 8;
		int num = 8;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜏ == null)
				{
					num = 4;
					continue;
				}
				goto IL_11A;
			case 1:
				if (this.ᜆ == null)
				{
					num = 6;
					continue;
				}
				goto IL_11A;
			case 2:
				num = 1;
				continue;
			case 3:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_15A;
				default:
					if (false)
					{
					}
					this.ᜀ.ᜁ(this.ᜂ.ᜃ(), this.ᜆ);
					num = 9;
					continue;
				}
				break;
			case 4:
				return;
			case 5:
				num = 10;
				continue;
			case 6:
				num = 0;
				continue;
			case 7:
				goto IL_15A;
			case 9:
				goto IL_F8;
			case 10:
				if (this.ᜆ != this.ᜄ)
				{
					num = 3;
					continue;
				}
				goto IL_172;
			}
			if (this.ᜃ == null)
			{
				num = 2;
				continue;
			}
			IL_11A:
			this.ᜀ.ᜉ(ClipboardData.b("ᡭ䩯᭱ᥳ᝵ίό᡻ώ", a_));
			this.ᜀ.ᜁ(this.ᜂ.ᜂ(), this.ᜄ);
			num = 7;
			continue;
			IL_15A:
			if (this.ᜃ == null)
			{
				break;
			}
			num = 5;
		}
		IL_F8:
		IL_172:
		this.ᜀ.ᜅ(ClipboardData.b("ŭ䩯ٱᵳɵᑷό", a_), this.ᜅ);
		this.ᜀ.ᜁ(ClipboardData.b("൭ɯᵱѳɵ᝷੹", a_), this.ᜇ);
		this.ᜀ.ᜁ(ClipboardData.b("൭ɯᵱѳᑵ᝷๹ࡻᅽ", a_), this.ᜈ);
		this.ᜀ.ᜁ(ClipboardData.b("൭ɯᵱѳ᩵ᵷᱹࡻ", a_), this.ᜉ);
		this.ᜀ.ᜁ(ClipboardData.b("൭ɯᵱѳѵᅷᵹᑻ੽", a_), this.ᜊ);
		this.ᜀ.ᜁ(ClipboardData.b("൭ᡯq᭳᭵᥷ᅹ᥻ݽ", a_), this.ᜋ);
		this.ᜀ.ᜁ(ClipboardData.b("७ᅯ᭱ᩳ", a_), this.ᜌ);
		this.ᜀ.ᜁ(ClipboardData.b("౭ᱯ፱ᝳᵵᑷό੻᭽", a_), this.\u170D);
		this.ᜀ.ᜁ(ClipboardData.b("७ᅯάᥳ᝵", a_), this.ᜎ);
		this.ᜀ.ᜁ(ClipboardData.b("७ɯ፱൳յ᭷᭹ၻ᭽", a_), this.\u1712);
		this.ᜀ.ᜁ(ClipboardData.b("౭᥯ṱᅳuᵷᙹ", a_), this.\u1713);
		this.ᜀ.ᜁ(ClipboardData.b("୭ᵯၱ᭳յ୷᥹፻ች", a_), this.ᜏ);
		this.ᜀ.ᜁ(ClipboardData.b("ᱭᕯᅱ᭳᩵᝷ࡹࡻώ", a_), this.\u1715);
		this.ᜀ.ᜈ();
	}

	// Token: 0x06002B38 RID: 11064 RVA: 0x002A6670 File Offset: 0x002A5670
	internal byte[] ᜁ()
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
		return this.ᜃ;
	}

	// Token: 0x06002B39 RID: 11065 RVA: 0x002A66B4 File Offset: 0x002A56B4
	internal string ᜀ()
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
		return this.ᜄ;
	}

	// Token: 0x06002B3A RID: 11066 RVA: 0x002A66F8 File Offset: 0x002A56F8
	internal void ᜁ(string A_0)
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
		this.ᜄ = A_0;
	}

	// Token: 0x06002B3B RID: 11067 RVA: 0x002A673C File Offset: 0x002A573C
	internal string ᜃ()
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
		return this.ᜆ;
	}

	// Token: 0x06002B3C RID: 11068 RVA: 0x002A6780 File Offset: 0x002A5780
	internal void ᜀ(string A_0)
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
		this.ᜆ = A_0;
	}

	// Token: 0x04002534 RID: 9524
	private readonly sprᤎ ᜀ;

	// Token: 0x04002535 RID: 9525
	private readonly sprᩍ ᜁ;

	// Token: 0x04002536 RID: 9526
	private readonly spr\u21E4 ᜂ;

	// Token: 0x04002537 RID: 9527
	private byte[] ᜃ;

	// Token: 0x04002538 RID: 9528
	private string ᜄ;

	// Token: 0x04002539 RID: 9529
	private string ᜅ;

	// Token: 0x0400253A RID: 9530
	private string ᜆ;

	// Token: 0x0400253B RID: 9531
	private string ᜇ;

	// Token: 0x0400253C RID: 9532
	private string ᜈ;

	// Token: 0x0400253D RID: 9533
	private string ᜉ;

	// Token: 0x0400253E RID: 9534
	private string ᜊ;

	// Token: 0x0400253F RID: 9535
	private string ᜋ;

	// Token: 0x04002540 RID: 9536
	private string ᜌ;

	// Token: 0x04002541 RID: 9537
	private string \u170D;

	// Token: 0x04002542 RID: 9538
	private string ᜎ;

	// Token: 0x04002543 RID: 9539
	private string ᜏ;

	// Token: 0x04002544 RID: 9540
	private string ᜐ;

	// Token: 0x04002545 RID: 9541
	private string ᜑ;

	// Token: 0x04002546 RID: 9542
	private string \u1712;

	// Token: 0x04002547 RID: 9543
	private string \u1713;

	// Token: 0x04002548 RID: 9544
	private string \u1714;

	// Token: 0x04002549 RID: 9545
	private string \u1715;
}
