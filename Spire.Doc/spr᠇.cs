using System;
using System.Drawing;
using Spire.CompoundFile.Doc;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields.Shape;

// Token: 0x020001DF RID: 479
internal class spr\u1807
{
	// Token: 0x060014D6 RID: 5334 RVA: 0x00152D38 File Offset: 0x00151D38
	internal spr\u1807(sprᤎ A_0, spr\u21E4 A_1)
	{
		this.ᜀ = A_0;
		this.ᜁ = A_1;
	}

	// Token: 0x060014D7 RID: 5335 RVA: 0x00152D5C File Offset: 0x00151D5C
	internal void ᜀ(int A_0, object A_1)
	{
		for (;;)
		{
			this.ᜂ++;
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					this.ᜊ = sprṍ.ᜅ((int)A_1);
					num = 9;
					continue;
				case 1:
					if (A_0 != 4110)
					{
						num = 7;
						continue;
					}
					goto IL_171;
				case 2:
					goto IL_252;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1E5;
					default:
						if (false)
						{
						}
						if (A_0 != 4121)
						{
							num = 5;
							continue;
						}
						goto IL_35D;
					}
					break;
				case 4:
					goto IL_1E5;
				case 5:
					return;
				case 6:
					num = 1;
					continue;
				case 7:
					num = 3;
					continue;
				case 8:
					switch (A_0)
					{
					case 448:
						goto IL_17E;
					case 449:
						goto IL_164;
					case 450:
						goto IL_327;
					case 451:
					case 473:
					case 474:
					case 475:
					case 481:
						goto IL_2A8;
					case 452:
						goto IL_3D6;
					case 453:
					case 472:
					case 476:
					case 480:
					case 483:
					case 484:
					case 485:
					case 486:
					case 487:
					case 488:
					case 489:
					case 490:
					case 491:
					case 492:
					case 493:
					case 494:
					case 495:
					case 496:
					case 497:
					case 498:
					case 499:
					case 500:
					case 501:
					case 502:
						return;
					case 454:
						goto IL_2F6;
					case 455:
					case 457:
					case 458:
					case 510:
						goto IL_35D;
					case 456:
					case 509:
						goto IL_206;
					case 459:
						num = 4;
						continue;
					case 460:
					case 511:
						goto IL_36D;
					case 461:
						goto IL_227;
					case 462:
						goto IL_215;
					case 463:
						goto IL_2B7;
					case 464:
						goto IL_409;
					case 465:
						goto IL_315;
					case 466:
						goto IL_19E;
					case 467:
						goto IL_339;
					case 468:
						goto IL_2E4;
					case 469:
						goto IL_34B;
					case 470:
						goto IL_303;
					case 471:
						goto IL_257;
					case 477:
					case 478:
					case 479:
					case 482:
						goto IL_28F;
					case 503:
						goto IL_2D5;
					case 504:
					case 506:
						goto IL_2C6;
					case 505:
						goto IL_3BB;
					case 507:
						goto IL_1B0;
					case 508:
						goto IL_1BF;
					default:
						num = 6;
						continue;
					}
					break;
				case 9:
					goto IL_239;
				}
				break;
				IL_1E5:
				if ((int)A_1 > 0)
				{
					num = 0;
					continue;
				}
				IL_239:
				this.ᜂ--;
				num = 2;
			}
		}
		IL_164:
		this.ᜆ = sprṍ.ᜁ(A_1);
		return;
		IL_171:
		this.ᜃ = (byte[])A_1;
		return;
		IL_17E:
		this.ᜅ = spr\u23B0.ᜁ((Color)A_1);
		this.ᜂ--;
		return;
		IL_19E:
		this.ᜏ = sprᥜ.ᜀ((StrokeArrowWidth)A_1);
		return;
		IL_1B0:
		this.ᜂ--;
		return;
		IL_1BF:
		this.\u1716 = sprṍ.ᜀ(A_1);
		this.ᜂ--;
		return;
		IL_206:
		this.ᜂ--;
		return;
		IL_215:
		this.ᜌ = sprᥜ.ᜀ((LineDashing)A_1);
		return;
		IL_227:
		this.ᜋ = sprᥜ.ᜀ((ShapeLineStyle)A_1);
		return;
		IL_252:
		return;
		IL_257:
		this.\u1714 = sprᥜ.ᜀ((StrokeEndCap)A_1);
		return;
		IL_28F:
		this.ᜂ--;
		return;
		IL_2A8:
		this.ᜂ--;
		return;
		IL_2B7:
		this.ᜂ--;
		return;
		IL_2C6:
		this.ᜂ--;
		return;
		IL_2D5:
		this.ᜂ--;
		return;
		IL_2E4:
		this.ᜑ = sprᥜ.ᜀ((StrokeArrowWidth)A_1);
		return;
		IL_2F6:
		this.ᜉ = (string)A_1;
		return;
		IL_303:
		this.\u1713 = sprᥜ.ᜀ((StrokeJoinStyle)A_1);
		return;
		IL_315:
		this.ᜎ = sprᥜ.ᜀ((ArrowType)A_1);
		return;
		IL_327:
		this.ᜇ = spr\u23B0.ᜁ((Color)A_1);
		return;
		IL_339:
		this.ᜐ = sprᥜ.ᜀ((StrokeArrowLength)A_1);
		return;
		IL_34B:
		this.\u1712 = sprᥜ.ᜀ((StrokeArrowLength)A_1);
		return;
		IL_35D:
		this.ᜂ--;
		return;
		IL_36D:
		this.ᜂ--;
		return;
		IL_3BB:
		this.\u1715 = sprṍ.ᜀ(A_1);
		this.ᜂ--;
		return;
		IL_3D6:
		this.ᜈ = sprᥜ.ᜀ((LineFillType)A_1);
		return;
		IL_409:
		this.\u170D = sprᥜ.ᜀ((ArrowType)A_1);
	}

	// Token: 0x060014D8 RID: 5336 RVA: 0x00153184 File Offset: 0x00152184
	internal void ᜄ()
	{
		int a_ = 13;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		this.ᜀ.ᜁ(ClipboardData.b("rŴնᙸၺ᡼᭾", a_), this.\u1716);
		this.ᜀ.ᜁ(ClipboardData.b("rŴնᙸၺ᡼᱾", a_), this.ᜅ);
		this.ᜀ.ᜁ(ClipboardData.b("rŴնᙸၺ᡼ࡾﶈ", a_), this.ᜊ);
		this.ᜀ.ᜁ(ClipboardData.b("ᩲ᭴Ѷᱸེർ᩾", a_), this.\u1715);
	}

	// Token: 0x060014D9 RID: 5337 RVA: 0x00153244 File Offset: 0x00152244
	internal void ᜃ()
	{
		int a_ = 12;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_46;
			case 1:
				goto IL_F7;
			case 2:
				goto IL_121;
			case 3:
				if (spr\u1CC6.ᜋ(this.ᜄ))
				{
					num = 1;
					continue;
				}
				goto IL_123;
			case 4:
				num = 3;
				continue;
			case 5:
				if (this.ᜉ == null)
				{
					num = 4;
					continue;
				}
				goto IL_F7;
			}
			if (this.ᜂ <= 0)
			{
				num = 0;
				continue;
			}
			this.ᜀ.ᜉ(ClipboardData.b("ѱ乳յ౷ࡹ፻ᕽ", a_));
			this.ᜀ.ᜁ(this.ᜁ.ᜂ(), this.ᜄ);
			num = 5;
			continue;
			IL_F7:
			this.ᜀ.ᜅ(ClipboardData.b("ᵱ乳ɵᅷ๹ၻ᭽", a_), this.ᜉ);
			num = 2;
		}
		IL_46:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_121:
			break;
		default:
			if (false)
			{
			}
			if (true)
			{
			}
			return;
		}
		IL_123:
		this.ᜀ.ᜁ(ClipboardData.b("ᡱ᭳ήᙷॹࡻݽ", a_), this.\u1713);
		this.ᜀ.ᜁ(ClipboardData.b("ᙱᕳյၷॹࡻݽ", a_), this.ᜌ);
		this.ᜀ.ᜁ(ClipboardData.b("ṱᵳᡵᵷॹࡻݽ", a_), this.ᜋ);
		this.ᜀ.ᜁ(ClipboardData.b("᝱ᩳት᭷᭹౻", a_), this.\u1714);
		this.ᜀ.ᜁ(ClipboardData.b("űs᝵੷๹ᵻ౽", a_), this.\u170D);
		this.ᜀ.ᜁ(ClipboardData.b("űs᝵੷๹ᵻ౽", a_), this.ᜏ);
		this.ᜀ.ᜁ(ClipboardData.b("űs᝵੷๹ᵻ౽揄", a_), this.ᜐ);
		this.ᜀ.ᜁ(ClipboardData.b("᝱ᩳት᥷ࡹ๻ᅽ", a_), this.ᜎ);
		this.ᜀ.ᜁ(ClipboardData.b("᝱ᩳት᥷ࡹ๻ᅽﲇ", a_), this.ᜑ);
		this.ᜀ.ᜁ(ClipboardData.b("᝱ᩳት᥷ࡹ๻ᅽﺉ", a_), this.\u1712);
		this.ᜀ.ᜁ(ClipboardData.b("ᵱѳ᝵᭷፹ࡻݽ", a_), this.ᜆ);
		this.ᜀ.ᜁ(ClipboardData.b("ᅱ᭳᩵᝷ࡹ乻", a_), this.ᜇ);
		this.ᜀ.ᜁ(ClipboardData.b("ᑱᵳ᩵ᑷ๹ջ๽", a_), this.ᜈ);
		this.ᜀ.ᜈ();
	}

	// Token: 0x060014DA RID: 5338 RVA: 0x00153514 File Offset: 0x00152514
	internal byte[] ᜂ()
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

	// Token: 0x060014DB RID: 5339 RVA: 0x00153558 File Offset: 0x00152558
	internal string ᜁ()
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
		return this.ᜄ;
	}

	// Token: 0x060014DC RID: 5340 RVA: 0x0015359C File Offset: 0x0015259C
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

	// Token: 0x060014DD RID: 5341 RVA: 0x001535E0 File Offset: 0x001525E0
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
		return this.\u1713;
	}

	// Token: 0x060014DE RID: 5342 RVA: 0x00153624 File Offset: 0x00152624
	internal void ᜀ(string A_0)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 4;
				continue;
			case 1:
				goto IL_7F;
			case 3:
				this.ᜂ++;
				if (true)
				{
				}
				num = 1;
				continue;
			case 4:
				goto IL_89;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_89:
				if (!spr\u1CC6.ᜋ(A_0))
				{
					goto IL_9E;
				}
				num = 3;
				break;
			default:
				if (false)
				{
				}
				if (spr\u1CC6.ᜋ(this.\u1713))
				{
					goto IL_9E;
				}
				num = 0;
				break;
			}
		}
		IL_7F:
		IL_9E:
		this.\u1713 = A_0;
	}

	// Token: 0x0400193D RID: 6461
	private readonly sprᤎ ᜀ;

	// Token: 0x0400193E RID: 6462
	private readonly spr\u21E4 ᜁ;

	// Token: 0x0400193F RID: 6463
	private int ᜂ;

	// Token: 0x04001940 RID: 6464
	private byte[] ᜃ;

	// Token: 0x04001941 RID: 6465
	private string ᜄ;

	// Token: 0x04001942 RID: 6466
	private string ᜅ;

	// Token: 0x04001943 RID: 6467
	private string ᜆ;

	// Token: 0x04001944 RID: 6468
	private string ᜇ;

	// Token: 0x04001945 RID: 6469
	private string ᜈ;

	// Token: 0x04001946 RID: 6470
	private string ᜉ;

	// Token: 0x04001947 RID: 6471
	private string ᜊ;

	// Token: 0x04001948 RID: 6472
	private string ᜋ;

	// Token: 0x04001949 RID: 6473
	private string ᜌ;

	// Token: 0x0400194A RID: 6474
	private string \u170D;

	// Token: 0x0400194B RID: 6475
	private string ᜎ;

	// Token: 0x0400194C RID: 6476
	private string ᜏ;

	// Token: 0x0400194D RID: 6477
	private string ᜐ;

	// Token: 0x0400194E RID: 6478
	private string ᜑ;

	// Token: 0x0400194F RID: 6479
	private string \u1712;

	// Token: 0x04001950 RID: 6480
	private string \u1713;

	// Token: 0x04001951 RID: 6481
	private string \u1714;

	// Token: 0x04001952 RID: 6482
	private string \u1715;

	// Token: 0x04001953 RID: 6483
	private string \u1716;
}
