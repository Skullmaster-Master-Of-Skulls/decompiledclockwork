using System;
using Spire.Doc.Core;
using Spire.Doc.Documents;

// Token: 0x020002B2 RID: 690
[CLSCompliant(false)]
internal abstract class sprễ
{
	// Token: 0x06002527 RID: 9511 RVA: 0x0025643C File Offset: 0x0025543C
	internal sprễ()
	{
	}

	// Token: 0x06002528 RID: 9512 RVA: 0x00256450 File Offset: 0x00255450
	protected void ᜄ(spr\u20B1 A_0)
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
		this.ᜀ.ᜃ(null);
		this.ᜀ(this.ᜀ);
		this.ᜀ.\u1718();
	}

	// Token: 0x06002529 RID: 9513 RVA: 0x002564B8 File Offset: 0x002554B8
	protected virtual void ᜀ(spr\u20B1 A_0)
	{
		for (;;)
		{
			this.ᜅ(A_0);
			this.ᜀ(A_0, WordSubdocument.Footnote);
			this.ᜀ(A_0, WordSubdocument.Annotation);
			this.ᜀ(A_0, WordSubdocument.Endnote);
			this.ᜆ(A_0);
			(A_0 as sprᬛ).ᜎ();
			this.ᜂ = A_0.ᜉ();
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_8D;
				case 1:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_BC;
					default:
						if (false)
						{
						}
						goto IL_8D;
					}
					break;
				case 2:
					goto IL_AC;
				case 3:
					if (A_0.ᜋ() == WordChunkType.DocumentEnd)
					{
						num = 2;
						continue;
					}
					this.ᜌ(A_0);
					this.ᜆ(A_0);
					goto IL_BC;
				}
				break;
				IL_8D:
				num = 3;
				continue;
				IL_BC:
				num = 0;
			}
		}
		IL_AC:
		this.ᜇ(A_0);
	}

	// Token: 0x0600252A RID: 9514
	protected abstract void ᜇ(spr\u20B1 A_0);

	// Token: 0x0600252B RID: 9515
	protected abstract void ᜀ(sprᳱ A_0, int A_1);

	// Token: 0x0600252C RID: 9516 RVA: 0x00256598 File Offset: 0x00255598
	protected virtual void ᜆ(spr\u180A A_0)
	{
		spr\u20B1 spr_u20B;
		for (;;)
		{
			spr_u20B = (A_0 as spr\u20B1);
			WordChunkType wordChunkType = spr_u20B.ᜄ();
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					if (wordChunkType != WordChunkType.Footnote)
					{
						num = 8;
						continue;
					}
					this.ᜂ(spr_u20B);
					num = 10;
					continue;
				case 2:
					goto IL_F3;
				case 3:
					switch (wordChunkType)
					{
					case WordChunkType.SectionEnd:
						this.ᜃ(spr_u20B);
						num = 5;
						continue;
					case WordChunkType.PageBreak:
						this.ᜀ(spr_u20B, BreakType.PageBreak);
						num = 11;
						continue;
					case WordChunkType.ColumnBreak:
						this.ᜀ(spr_u20B, BreakType.ColumnBreak);
						num = 7;
						continue;
					default:
						num = 0;
						continue;
					}
					break;
				case 4:
					if (wordChunkType != WordChunkType.Annotation)
					{
						num = 6;
						continue;
					}
					this.ᜁ(spr_u20B);
					num = 2;
					continue;
				case 5:
					goto IL_14C;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A8;
					default:
						if (false)
						{
						}
						num = 9;
						continue;
					}
					break;
				case 7:
					goto IL_128;
				case 8:
					goto IL_A8;
				case 9:
					goto IL_159;
				case 10:
					goto IL_8B;
				case 11:
					goto IL_A3;
				}
				break;
				IL_A8:
				num = 4;
			}
		}
		IL_8B:
		IL_A3:
		IL_F3:
		IL_128:
		IL_14C:
		goto IL_15B;
		IL_159:
		if (true)
		{
		}
		this.ᜅ(spr_u20B);
		return;
		IL_15B:
		this.ᜂ = spr_u20B.ᜉ();
	}

	// Token: 0x0600252D RID: 9517 RVA: 0x0025670C File Offset: 0x0025570C
	protected virtual void ᜅ(spr\u180A A_0)
	{
		for (;;)
		{
			WordChunkType wordChunkType = A_0.ᜄ();
			int num = 26;
			for (;;)
			{
				spr\u20B1 spr_u20B;
				spr\u20B1 spr_u20B2;
				switch (num)
				{
				case 0:
					goto IL_359;
				case 1:
					if (A_0 is spr\u20B1)
					{
						num = 24;
						continue;
					}
					goto IL_204;
				case 2:
					if (A_0 is spr\u20B1)
					{
						num = 29;
						continue;
					}
					goto IL_2DA;
				case 3:
					goto IL_3AF;
				case 4:
					goto IL_2EC;
				case 5:
					goto IL_216;
				case 6:
					goto IL_3B1;
				case 7:
					goto IL_342;
				case 8:
					goto IL_281;
				case 9:
					goto IL_298;
				case 10:
					num = 8;
					continue;
				case 11:
					if (!spr_u20B.\u1712())
					{
						num = 15;
						continue;
					}
					goto IL_3B1;
				case 12:
					goto IL_375;
				case 13:
					goto IL_1E8;
				case 14:
					goto IL_1D6;
				case 15:
					num = 28;
					continue;
				case 16:
					goto IL_147;
				case 17:
					if (spr_u20B2.ᜑ())
					{
						num = 30;
						continue;
					}
					goto IL_204;
				case 18:
					if (!(A_0 is sprᤜ))
					{
						num = 21;
						continue;
					}
					goto IL_1D6;
				case 19:
					goto IL_15E;
				case 20:
					goto IL_271;
				case 21:
					num = 33;
					continue;
				case 22:
					goto IL_3C3;
				case 23:
					goto IL_250;
				case 24:
					spr_u20B2 = (A_0 as spr\u20B1);
					num = 25;
					continue;
				case 25:
					if (!spr_u20B2.\u1712())
					{
						num = 31;
						continue;
					}
					goto IL_14C;
				case 26:
					switch (wordChunkType)
					{
					case WordChunkType.Text:
						num = 1;
						continue;
					case WordChunkType.ParagraphEnd:
						this.ᜐ(A_0);
						num = 9;
						continue;
					case WordChunkType.SectionEnd:
					case WordChunkType.PageBreak:
					case WordChunkType.ColumnBreak:
					case WordChunkType.DocumentEnd:
					case WordChunkType.FieldSeparator:
					case WordChunkType.Tab:
					case WordChunkType.Annotation:
						goto IL_3C5;
					case WordChunkType.Image:
						this.ᜉ(A_0);
						num = 32;
						continue;
					case WordChunkType.Shape:
						this.ᜄ(A_0);
						num = 12;
						continue;
					case WordChunkType.Table:
						this.ᜂ(A_0);
						num = 20;
						continue;
					case WordChunkType.TableRow:
						this.ᜁ(A_0);
						num = 0;
						continue;
					case WordChunkType.TableCell:
						this.ᜊ(A_0);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1E8;
						default:
							if (false)
							{
							}
							num = 7;
							continue;
						}
						break;
					case WordChunkType.Footnote:
						num = 18;
						continue;
					case WordChunkType.FieldBeginMark:
						this.ᜎ(A_0);
						num = 3;
						continue;
					case WordChunkType.FieldEndMark:
						this.ᜇ(A_0);
						num = 27;
						continue;
					case WordChunkType.LineBreak:
						this.\u170D(A_0);
						num = 16;
						continue;
					case WordChunkType.Symbol:
						num = 2;
						continue;
					case WordChunkType.CurrentPageNumber:
						this.ᜋ(A_0);
						num = 23;
						continue;
					default:
						num = 10;
						continue;
					}
					break;
				case 27:
					goto IL_1FF;
				case 28:
					if (spr_u20B.ᜑ())
					{
						num = 6;
						continue;
					}
					goto IL_2DA;
				case 29:
					spr_u20B = (A_0 as spr\u20B1);
					num = 11;
					continue;
				case 30:
					goto IL_14C;
				case 31:
					num = 17;
					continue;
				case 32:
					goto IL_2D5;
				case 33:
					if (A_0 is spr\u1F4F)
					{
						num = 14;
						continue;
					}
					goto IL_3C5;
				}
				break;
				IL_14C:
				this.ᜂ(spr_u20B2);
				num = 19;
				continue;
				IL_1D6:
				this.ᜃ(A_0);
				num = 13;
				continue;
				IL_204:
				this.ᜏ(A_0);
				num = 5;
				continue;
				IL_2DA:
				this.ᜀ(A_0);
				num = 4;
				continue;
				IL_3B1:
				this.ᜂ(spr_u20B);
				num = 22;
			}
		}
		IL_147:
		IL_15E:
		IL_1E8:
		IL_1FF:
		IL_216:
		IL_250:
		IL_271:
		IL_281:
		IL_298:
		IL_2D5:
		IL_2EC:
		IL_342:
		goto IL_3C5;
		IL_359:
		if (true)
		{
		}
		IL_375:
		IL_3AF:
		IL_3C3:
		IL_3C5:
		this.ᜂ = A_0.ᜉ();
	}

	// Token: 0x0600252E RID: 9518
	protected abstract void ᜀ(spr\u180A A_0, WordSubdocument A_1);

	// Token: 0x0600252F RID: 9519
	protected abstract void ᜌ(spr\u180A A_0);

	// Token: 0x06002530 RID: 9520
	protected abstract void ᜈ(spr\u20B1 A_0);

	// Token: 0x06002531 RID: 9521
	protected abstract void ᜀ(spr\u20B1 A_0, BreakType A_1);

	// Token: 0x06002532 RID: 9522
	protected abstract void ᜃ(spr\u20B1 A_0);

	// Token: 0x06002533 RID: 9523
	protected abstract void ᜎ(spr\u180A A_0);

	// Token: 0x06002534 RID: 9524
	protected abstract void ᜂ(spr\u180A A_0);

	// Token: 0x06002535 RID: 9525
	protected abstract void ᜁ(spr\u180A A_0);

	// Token: 0x06002536 RID: 9526
	protected abstract void ᜊ(spr\u180A A_0);

	// Token: 0x06002537 RID: 9527
	protected abstract void ᜉ(spr\u180A A_0);

	// Token: 0x06002538 RID: 9528
	protected abstract void ᜐ(spr\u180A A_0);

	// Token: 0x06002539 RID: 9529
	protected abstract void ᜏ(spr\u180A A_0);

	// Token: 0x0600253A RID: 9530
	protected abstract void ᜅ(spr\u20B1 A_0);

	// Token: 0x0600253B RID: 9531
	protected abstract void \u170D(spr\u180A A_0);

	// Token: 0x0600253C RID: 9532
	protected abstract void ᜄ(spr\u180A A_0);

	// Token: 0x0600253D RID: 9533
	protected abstract void ᜀ(spr\u180A A_0, sprᡡ A_1);

	// Token: 0x0600253E RID: 9534
	protected abstract void ᜀ(spr\u180A A_0, spr\u1B7E A_1);

	// Token: 0x0600253F RID: 9535
	protected abstract void ᜆ(spr\u20B1 A_0);

	// Token: 0x06002540 RID: 9536
	protected abstract void ᜀ(spr\u180A A_0);

	// Token: 0x06002541 RID: 9537
	protected abstract void ᜇ(spr\u180A A_0);

	// Token: 0x06002542 RID: 9538
	protected abstract void ᜋ(spr\u180A A_0);

	// Token: 0x06002543 RID: 9539
	protected abstract void ᜈ(spr\u180A A_0);

	// Token: 0x06002544 RID: 9540
	protected abstract void ᜁ(spr\u20B1 A_0);

	// Token: 0x06002545 RID: 9541
	protected abstract void ᜑ(spr\u180A A_0);

	// Token: 0x06002546 RID: 9542
	protected abstract void ᜂ(spr\u20B1 A_0);

	// Token: 0x06002547 RID: 9543
	protected abstract void ᜃ(spr\u180A A_0);

	// Token: 0x040021DC RID: 8668
	protected spr\u20B1 ᜀ;

	// Token: 0x040021DD RID: 8669
	protected spr\u19E9 ᜁ;

	// Token: 0x040021DE RID: 8670
	protected int ᜂ;
}
