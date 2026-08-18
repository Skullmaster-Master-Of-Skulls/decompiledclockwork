using System;
using System.Drawing;
using Spire.CompoundFile.Doc;
using Spire.Doc.Fields.Shape;

// Token: 0x020001E1 RID: 481
internal class spr\u1A9B
{
	// Token: 0x060014F1 RID: 5361 RVA: 0x0015586C File Offset: 0x0015486C
	internal spr\u1A9B(sprᩍ A_0, sprᤎ A_1, spr\u21E4 A_2)
	{
		this.ᜀ = A_1;
		this.ᜁ = A_0;
		this.ᜂ = A_2;
	}

	// Token: 0x060014F2 RID: 5362 RVA: 0x00155894 File Offset: 0x00154894
	internal void ᜀ(int A_0, object A_1)
	{
		int a_ = 13;
		for (;;)
		{
			this.ᜃ++;
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if ((bool)A_1)
					{
						num = 11;
						continue;
					}
					this.ᜃ--;
					num = 9;
					continue;
				case 1:
					goto IL_351;
				case 2:
					num = 3;
					continue;
				case 3:
					if (A_0 != 4122)
					{
						num = 7;
						continue;
					}
					return;
				case 4:
					if ((FillType)A_1 == FillType.ShadeCenter)
					{
						num = 12;
						continue;
					}
					num = 10;
					continue;
				case 5:
					if (true)
					{
					}
					if (A_0 != 4111)
					{
						num = 2;
						continue;
					}
					goto IL_38D;
				case 6:
					goto IL_45C;
				case 7:
					return;
				case 8:
					switch (A_0)
					{
					case 384:
						this.ᜈ = sprᥜ.ᜀ((FillType)A_1);
						num = 4;
						continue;
					case 385:
						goto IL_2A5;
					case 386:
						goto IL_302;
					case 387:
						goto IL_484;
					case 388:
						goto IL_1BF;
					case 389:
					case 390:
					case 392:
					case 393:
					case 394:
					case 406:
					case 413:
					case 416:
					case 417:
					case 418:
					case 419:
					case 420:
					case 421:
					case 423:
					case 424:
					case 425:
					case 426:
					case 427:
					case 428:
					case 429:
					case 430:
					case 431:
					case 432:
					case 433:
					case 434:
					case 435:
					case 436:
					case 437:
					case 438:
					case 439:
					case 440:
						return;
					case 391:
						goto IL_3AC;
					case 395:
						goto IL_4D2;
					case 396:
						goto IL_39A;
					case 397:
						goto IL_2E8;
					case 398:
						goto IL_1B2;
					case 399:
						goto IL_380;
					case 400:
						goto IL_4C5;
					case 401:
						goto IL_3B9;
					case 402:
						goto IL_207;
					case 403:
						goto IL_196;
					case 404:
						goto IL_3EC;
					case 405:
						goto IL_214;
					case 407:
						goto IL_4DF;
					case 408:
						goto IL_25B;
					case 409:
						goto IL_2F5;
					case 410:
						goto IL_189;
					case 411:
						goto IL_4F1;
					case 412:
						num = 15;
						continue;
					case 414:
						goto IL_296;
					case 415:
						goto IL_1A3;
					case 422:
						goto IL_371;
					case 441:
						num = 14;
						continue;
					case 442:
						num = 0;
						continue;
					case 443:
						goto IL_2C5;
					case 444:
						goto IL_356;
					case 445:
						goto IL_1CC;
					case 446:
						goto IL_1EC;
					case 447:
						goto IL_4B8;
					default:
						num = 13;
						continue;
					}
					break;
				case 9:
					goto IL_52C;
				case 10:
					if ((FillType)A_1 == FillType.ShadeUnscale)
					{
						num = 16;
						continue;
					}
					return;
				case 11:
					goto IL_47F;
				case 12:
					goto IL_439;
				case 13:
					num = 5;
					continue;
				case 14:
					if ((bool)A_1)
					{
						num = 6;
						continue;
					}
					goto IL_4A9;
				case 15:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2E8;
					default:
						if (false)
						{
						}
						if ((int)A_1 == 1073741835)
						{
							num = 1;
							continue;
						}
						goto IL_52E;
					}
					break;
				case 16:
					goto IL_249;
				}
				break;
			}
		}
		IL_189:
		this.\u171C = sprṍ.ᜁ(A_1);
		return;
		IL_196:
		this.\u1716 = sprṍ.ᜁ(A_1);
		return;
		IL_1A3:
		this.ᜃ--;
		return;
		IL_1B2:
		this.ᜑ = sprṍ.ᜁ(A_1);
		return;
		IL_1BF:
		this.ᜌ = sprṍ.ᜁ(A_1);
		return;
		IL_1CC:
		this.ᜣ = sprṍ.ᜀ((bool)A_1);
		this.ᜃ--;
		return;
		IL_1EC:
		this.ᜤ = sprṍ.ᜀ(A_1);
		this.ᜃ--;
		return;
		IL_207:
		this.\u1715 = sprṍ.ᜁ(A_1);
		return;
		IL_214:
		this.\u1718 = sprᥜ.ᜀ((FillDimensionType)A_1);
		return;
		IL_249:
		this.ᜇ = true;
		return;
		IL_25B:
		this.\u171A = sprṍ.ᜁ(A_1);
		return;
		IL_296:
		this.ᜃ--;
		return;
		IL_2A5:
		this.ᜉ = spr\u23B0.ᜁ((Color)A_1);
		this.ᜃ--;
		return;
		IL_2C5:
		this.ᜡ = sprṍ.ᜀ(A_1);
		this.ᜃ--;
		return;
		IL_2E8:
		this.ᜐ = sprṍ.ᜁ(A_1);
		return;
		IL_2F5:
		this.\u171B = sprṍ.ᜁ(A_1);
		return;
		IL_302:
		this.ᜊ = sprṍ.ᜁ(A_1);
		return;
		IL_351:
		this.\u171E = ClipboardData.b("ὲᱴ᥶ᱸོ᩺彾", a_);
		return;
		IL_356:
		this.ᜢ = sprṍ.ᜀ(A_1);
		this.ᜃ--;
		return;
		IL_371:
		this.ᜃ--;
		return;
		IL_380:
		this.\u1712 = sprṍ.ᜁ(A_1);
		return;
		IL_38D:
		this.ᜄ = (byte[])A_1;
		return;
		IL_39A:
		this.ᜏ = sprṍ.ᜁ((int)A_1);
		return;
		IL_3AC:
		this.\u170D = (string)A_1;
		return;
		IL_3B9:
		this.\u1714 = sprṍ.ᜁ(A_1);
		return;
		IL_3EC:
		this.\u1717 = sprṍ.ᜁ(A_1);
		return;
		IL_439:
		this.ᜆ = true;
		return;
		IL_45C:
		this.\u171F = sprṍ.ᜀ(A_1);
		return;
		IL_47F:
		this.ᜠ = sprṍ.ᜀ(A_1);
		return;
		IL_484:
		this.ᜋ = spr\u23B0.ᜁ((Color)A_1);
		return;
		IL_4A9:
		this.ᜃ--;
		return;
		IL_4B8:
		this.ᜥ = sprṍ.ᜀ(A_1);
		return;
		IL_4C5:
		this.\u1713 = sprṍ.ᜁ(A_1);
		return;
		IL_4D2:
		this.ᜎ = sprṍ.ᜁ(A_1);
		return;
		IL_4DF:
		this.\u1719 = sprṍ.ᜀ((spr\u2143[])A_1);
		return;
		IL_4F1:
		this.\u171D = sprṍ.ᜁ(A_1);
		return;
		IL_52C:
		return;
		IL_52E:
		this.\u171E = ClipboardData.b("ᵲᩴ᥶ᱸ", a_);
	}

	// Token: 0x060014F3 RID: 5363 RVA: 0x00155DE4 File Offset: 0x00154DE4
	internal void ᜃ()
	{
		int a_ = 15;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_77;
			case 2:
				this.ᜀ.ᜁ(ClipboardData.b("፴Ṷᕸ᝺᡼᭾", a_), this.ᜡ);
				num = 1;
				continue;
			}
			if (true)
			{
			}
			if (this.ᜁ.\u1774() == ShapeType.Line)
			{
				break;
			}
			num = 2;
		}
		IL_77:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_77;
		default:
			if (false)
			{
			}
			this.ᜀ.ᜁ(ClipboardData.b("፴Ṷᕸ᝺Ṽၾ", a_), this.ᜉ);
			return;
		}
	}

	// Token: 0x060014F4 RID: 5364 RVA: 0x00155EA8 File Offset: 0x00154EA8
	internal void ᜂ()
	{
		int a_ = 12;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_76;
			case 2:
				return;
			case 3:
				goto IL_DE;
			case 4:
				this.ᜀ.ᜅ(ClipboardData.b("ᵱ乳ɵᅷ๹ၻ᭽", a_), this.\u170D);
				num = 16;
				continue;
			case 5:
				if (!this.ᜆ)
				{
					num = 11;
					continue;
				}
				goto IL_76;
			case 6:
				this.ᜀ.ᜁ(ClipboardData.b("ᵱ乳ၵᅷᙹၻ", a_), new object[]
				{
					ClipboardData.b("ѱ乳፵w๹", a_),
					ClipboardData.b("ѱᵳ፵ཷ", a_),
					ClipboardData.b("ٱ൳ٵᵷ", a_),
					this.ᜆ ? ClipboardData.b("ᕱٳ᝵ᱷ፹᥻ၽ솁ﲇﺋ", a_) : ClipboardData.b("ᕱٳ᝵ᱷ፹᥻ၽ힁", a_)
				});
				num = 15;
				continue;
			case 7:
				if (this.ᜇ)
				{
					num = 1;
					continue;
				}
				goto IL_478;
			case 8:
				goto IL_2B5;
			case 9:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_468;
				default:
					if (false)
					{
					}
					num = 13;
					continue;
				}
				break;
			case 10:
				if (this.ᜅ != null)
				{
					num = 4;
					continue;
				}
				goto IL_310;
			case 11:
				num = 7;
				continue;
			case 12:
				num = 2;
				continue;
			case 13:
				if (this.ᜑ != null)
				{
					num = 8;
					continue;
				}
				goto IL_DE;
			case 14:
				if (this.ᜐ == null)
				{
					goto IL_468;
				}
				goto IL_2B5;
			case 15:
				goto IL_255;
			case 16:
				goto IL_310;
			}
			if (this.ᜃ <= 0)
			{
				num = 12;
				continue;
			}
			this.ᜀ.ᜉ(ClipboardData.b("ѱ乳ၵᅷᙹၻ", a_));
			this.ᜀ.ᜁ(this.ᜂ.ᜂ(), this.ᜅ);
			num = 10;
			continue;
			IL_76:
			num = 6;
			continue;
			IL_DE:
			this.ᜀ.ᜁ(ClipboardData.b("άᅳɵၷᕹ᡻", a_), this.\u171E);
			this.ᜀ.ᜁ(ClipboardData.b("ᑱ᭳ᕵ൷ॹ", a_), this.ᜏ);
			this.ᜀ.ᜁ(ClipboardData.b("ٱ൳ٵᵷ", a_), this.ᜈ);
			this.ᜀ.ᜁ(ClipboardData.b("ᵱ乳ትᵷ๹᥻ᵽﮇ憐ﾓ", a_), this.ᜥ);
			num = 5;
			continue;
			IL_2B5:
			if (true)
			{
			}
			this.ᜀ.ᜂ(ClipboardData.b("ᑱ᭳ᕵ൷ॹ౻ᅽ", a_), this.ᜐ, this.ᜑ);
			this.ᜀ.ᜅ(ClipboardData.b("ᑱ᭳ᕵ൷ॹཻ᝽奔", a_), "");
			num = 3;
			continue;
			IL_310:
			this.ᜀ.ᜁ(ClipboardData.b("ᵱѳ᝵᭷፹ࡻݽ", a_), this.ᜊ);
			this.ᜀ.ᜁ(ClipboardData.b("ᅱ᭳᩵᝷ࡹ乻", a_), this.ᜋ);
			this.ᜀ.ᜁ(ClipboardData.b("ᵱ乳᥵ࡷ᭹ύ᝽ﮁ뚃", a_), this.ᜌ);
			this.ᜀ.ᜁ(ClipboardData.b("፱ݳٵᵷ᥹ࡻ", a_), this.\u1718);
			this.ᜀ.ᜂ(ClipboardData.b("ᵱٳήί፹ቻ", a_), this.\u171A, this.\u171B);
			this.ᜀ.ᜂ(ClipboardData.b("ɱ᭳յᅷ๹ᕻᅽ", a_), this.\u171C, this.\u171D);
			this.ᜀ.ᜁ(ClipboardData.b("qᅳᕵ᝷ᙹ፻౽", a_), this.\u171F);
			this.ᜀ.ᜁ(ClipboardData.b("q᭳ɵ᥷๹᥻", a_), this.ᜠ);
			this.ᜀ.ᜁ(ClipboardData.b("፱ᩳᅵᑷό", a_), this.ᜎ);
			this.ᜀ.ᜁ(ClipboardData.b("ᅱ᭳᩵᝷ࡹཻ", a_), this.\u1719);
			num = 14;
			continue;
			IL_468:
			num = 9;
		}
		IL_255:
		IL_478:
		this.ᜀ.ᜈ();
	}

	// Token: 0x060014F5 RID: 5365 RVA: 0x00156344 File Offset: 0x00155344
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
		return this.ᜄ;
	}

	// Token: 0x060014F6 RID: 5366 RVA: 0x00156388 File Offset: 0x00155388
	internal string ᜀ()
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
		return this.ᜅ;
	}

	// Token: 0x060014F7 RID: 5367 RVA: 0x001563CC File Offset: 0x001553CC
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
		this.ᜅ = A_0;
	}

	// Token: 0x04001968 RID: 6504
	private readonly sprᤎ ᜀ;

	// Token: 0x04001969 RID: 6505
	private readonly sprᩍ ᜁ;

	// Token: 0x0400196A RID: 6506
	private readonly spr\u21E4 ᜂ;

	// Token: 0x0400196B RID: 6507
	private int ᜃ;

	// Token: 0x0400196C RID: 6508
	private byte[] ᜄ;

	// Token: 0x0400196D RID: 6509
	private string ᜅ;

	// Token: 0x0400196E RID: 6510
	private bool ᜆ;

	// Token: 0x0400196F RID: 6511
	private bool ᜇ;

	// Token: 0x04001970 RID: 6512
	private string ᜈ;

	// Token: 0x04001971 RID: 6513
	private string ᜉ;

	// Token: 0x04001972 RID: 6514
	private string ᜊ;

	// Token: 0x04001973 RID: 6515
	private string ᜋ;

	// Token: 0x04001974 RID: 6516
	private string ᜌ;

	// Token: 0x04001975 RID: 6517
	private string \u170D;

	// Token: 0x04001976 RID: 6518
	private string ᜎ;

	// Token: 0x04001977 RID: 6519
	private string ᜏ;

	// Token: 0x04001978 RID: 6520
	private string ᜐ;

	// Token: 0x04001979 RID: 6521
	private string ᜑ;

	// Token: 0x0400197A RID: 6522
	private string \u1712;

	// Token: 0x0400197B RID: 6523
	private string \u1713;

	// Token: 0x0400197C RID: 6524
	private string \u1714;

	// Token: 0x0400197D RID: 6525
	private string \u1715;

	// Token: 0x0400197E RID: 6526
	private string \u1716;

	// Token: 0x0400197F RID: 6527
	private string \u1717;

	// Token: 0x04001980 RID: 6528
	private string \u1718;

	// Token: 0x04001981 RID: 6529
	private string \u1719;

	// Token: 0x04001982 RID: 6530
	private string \u171A;

	// Token: 0x04001983 RID: 6531
	private string \u171B;

	// Token: 0x04001984 RID: 6532
	private string \u171C;

	// Token: 0x04001985 RID: 6533
	private string \u171D;

	// Token: 0x04001986 RID: 6534
	private string \u171E;

	// Token: 0x04001987 RID: 6535
	private string \u171F;

	// Token: 0x04001988 RID: 6536
	private string ᜠ;

	// Token: 0x04001989 RID: 6537
	private string ᜡ;

	// Token: 0x0400198A RID: 6538
	private string ᜢ;

	// Token: 0x0400198B RID: 6539
	private string ᜣ;

	// Token: 0x0400198C RID: 6540
	private string ᜤ;

	// Token: 0x0400198D RID: 6541
	private string ᜥ;
}
