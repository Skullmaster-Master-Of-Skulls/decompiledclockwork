using System;
using System.Collections.Generic;
using System.Drawing;
using Spire.CompoundFile.Doc;
using Spire.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Formatting;
using Spire.Layouting;

// Token: 0x02000211 RID: 529
internal class spr᱆ : spr\u25E5
{
	// Token: 0x060018F7 RID: 6391 RVA: 0x00183A80 File Offset: 0x00182A80
	public spr᱆(spr\u17C8 A_0, sprᴉ A_1) : base(A_0, A_1)
	{
		this.ᜄ = true;
	}

	// Token: 0x060018F8 RID: 6392 RVA: 0x00183A9C File Offset: 0x00182A9C
	private new bool ᜀ(spr\u2573 A_0)
	{
		switch (0)
		{
		default:
		{
			Paragraph paragraph;
			for (;;)
			{
				paragraph = (A_0.\u1718() as Paragraph);
				int num = 15;
				for (;;)
				{
					ListFormat listFormat;
					ListLevel nearLevel;
					int listLevelNumber;
					ListStyleCollection listStyles;
					ListStyle listStyle;
					switch (num)
					{
					case 0:
					{
						ParagraphStyle style;
						if (style.ListFormat.CurrentListLevel != null)
						{
							num = 9;
							continue;
						}
						goto IL_28F;
					}
					case 1:
						num = 2;
						continue;
					case 2:
					{
						ParagraphStyle style;
						if (style.ListFormat.CurrentListLevel != null)
						{
							num = 17;
							continue;
						}
						goto IL_25C;
					}
					case 3:
						goto IL_301;
					case 4:
						goto IL_160;
					case 5:
						goto IL_1AF;
					case 6:
						goto IL_160;
					case 7:
						if (paragraph.ListFormat.CurrentListLevel == null)
						{
							num = 10;
							continue;
						}
						goto IL_25C;
					case 8:
						if (listFormat.CurrentListLevel != null)
						{
							num = 22;
							continue;
						}
						return false;
					case 9:
						num = 24;
						continue;
					case 10:
						num = 11;
						continue;
					case 11:
					{
						ParagraphStyle style;
						if (style != null)
						{
							num = 1;
							continue;
						}
						goto IL_25C;
					}
					case 12:
						if (paragraph.ListFormat.CurrentListLevel == null)
						{
							num = 26;
							continue;
						}
						goto IL_28F;
					case 13:
						num = 8;
						continue;
					case 14:
						IL_192:
						if (nearLevel.NumberPosition == 0f)
						{
							num = 5;
							continue;
						}
						return true;
					case 15:
						if (paragraph != null)
						{
							num = 21;
							continue;
						}
						return false;
					case 16:
						if (listFormat != null)
						{
							num = 25;
							continue;
						}
						return false;
					case 17:
					{
						ParagraphStyle style;
						listLevelNumber = style.ListFormat.ListLevelNumber;
						listStyle = listStyles.FindByName(style.ListFormat.CustomStyleName);
						num = 6;
						continue;
					}
					case 18:
					{
						if (true)
						{
						}
						ParagraphStyle style;
						listFormat = style.ListFormat;
						num = 3;
						continue;
					}
					case 19:
						goto IL_301;
					case 20:
						num = 0;
						continue;
					case 21:
					{
						ParagraphStyle style = paragraph.GetStyle();
						listFormat = null;
						num = 12;
						continue;
					}
					case 22:
						listStyle = null;
						listStyles = paragraph.Document.ListStyles;
						num = 7;
						continue;
					case 23:
						if (listFormat.ListType != ListType.NoList)
						{
							num = 13;
							continue;
						}
						return false;
					case 24:
					{
						ParagraphStyle style;
						if (style.ListFormat.HasKey(0))
						{
							num = 18;
							continue;
						}
						goto IL_28F;
					}
					case 25:
						num = 23;
						continue;
					case 26:
						num = 27;
						continue;
					case 27:
					{
						ParagraphStyle style;
						if (style != null)
						{
							num = 20;
							continue;
						}
						goto IL_28F;
					}
					}
					break;
					IL_160:
					nearLevel = listStyle.GetNearLevel(listLevelNumber);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_192;
					default:
						if (false)
						{
						}
						num = 14;
						continue;
					}
					IL_25C:
					listLevelNumber = paragraph.ListFormat.ListLevelNumber;
					listStyle = listStyles.FindByName(paragraph.ListFormat.CustomStyleName);
					num = 4;
					continue;
					IL_28F:
					listFormat = paragraph.ListFormat;
					num = 19;
					continue;
					IL_301:
					num = 16;
				}
			}
			IL_1AF:
			return paragraph.Format.FirstLineIndent != 0f;
		}
		}
	}

	// Token: 0x060018F9 RID: 6393 RVA: 0x00183E1C File Offset: 0x00182E1C
	protected override void ᜃ(spr\u2573 A_0)
	{
		int a_ = 11;
		switch (0)
		{
		default:
		{
			RectangleF a_2;
			for (;;)
			{
				a_2 = this.ᜅ.ᜇ();
				sprℐ sprℐ = base.\u171A() as sprℐ;
				bool flag = false;
				int num = 15;
				for (;;)
				{
					switch (num)
					{
					case 0:
						a_2.X += sprℐ.ᜢ();
						a_2.Width -= sprℐ.ᜢ();
						num = 31;
						continue;
					case 1:
						if (flag)
						{
							num = 29;
							continue;
						}
						goto IL_137;
					case 2:
						a_2.X += sprℐ.\u171B() + sprℐ.ᜢ();
						a_2.Width -= sprℐ.\u171B() + sprℐ.ᜢ();
						num = 6;
						continue;
					case 3:
						goto IL_367;
					case 4:
						if (sprℐ.\u171D() != -1)
						{
							num = 27;
							continue;
						}
						goto IL_20A;
					case 5:
						num = 23;
						continue;
					case 6:
						goto IL_20A;
					case 7:
						num = 8;
						continue;
					case 8:
					{
						Paragraph paragraph;
						int count;
						if (paragraph.ChildObjects[count - 1 - (A_0.\u1718() as sprᴛ).ᜂ()].DocumentObjectType.ToString() == ClipboardData.b("㍰Ųၴᙶቸ", a_))
						{
							num = 21;
							continue;
						}
						goto IL_5B2;
					}
					case 9:
						if (!(A_0.\u1718() is sprᴛ))
						{
							num = 2;
							continue;
						}
						goto IL_137;
					case 10:
						if ((A_0.\u1718() as sprᴛ).ᜁ() is Paragraph)
						{
							num = 39;
							continue;
						}
						goto IL_5B2;
					case 11:
						goto IL_3DC;
					case 12:
					{
						Break @break;
						if (@break.BreakType == BreakType.ColumnBreak)
						{
							num = 20;
							continue;
						}
						goto IL_5B2;
					}
					case 13:
						if (this.ᜃ.ᜊ().Count != 0)
						{
							goto IL_5B2;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_55F;
						default:
							if (false)
							{
							}
							num = 28;
							continue;
						}
						break;
					case 14:
						if (!flag)
						{
							num = 0;
							continue;
						}
						goto IL_20A;
					case 15:
						if (sprℐ != null)
						{
							num = 33;
							continue;
						}
						goto IL_564;
					case 16:
						if (A_0.\u1718() is sprᴛ)
						{
							if (true)
							{
							}
							num = 18;
							continue;
						}
						goto IL_5B2;
					case 17:
						if (!(A_0.\u1718() is sprᴛ))
						{
							num = 38;
							continue;
						}
						goto IL_3DC;
					case 18:
						num = 10;
						continue;
					case 19:
						a_2.X += sprℐ.\u171B();
						a_2.Width -= sprℐ.\u171B();
						num = 11;
						continue;
					case 20:
						goto IL_320;
					case 21:
					{
						Paragraph paragraph;
						int count;
						DocumentObject documentObject = paragraph.ChildObjects[count - 1 - (A_0.\u1718() as sprᴛ).ᜂ()];
						Break @break = documentObject as Break;
						num = 24;
						continue;
					}
					case 22:
					{
						int count;
						if (count > 1)
						{
							num = 5;
							continue;
						}
						goto IL_5B2;
					}
					case 23:
					{
						int count;
						if (count != (A_0.\u1718() as sprᴛ).ᜂ())
						{
							num = 7;
							continue;
						}
						goto IL_5B2;
					}
					case 24:
					{
						Break @break;
						if (@break.BreakType != BreakType.PageBreak)
						{
							num = 30;
							continue;
						}
						goto IL_320;
					}
					case 25:
						goto IL_564;
					case 26:
						a_2.X += sprℐ.ᜢ();
						a_2.Width -= sprℐ.ᜢ();
						num = 3;
						continue;
					case 27:
						num = 1;
						continue;
					case 28:
						num = 35;
						continue;
					case 29:
						num = 9;
						continue;
					case 30:
						num = 12;
						continue;
					case 31:
						goto IL_55F;
					case 32:
						num = 17;
						continue;
					case 33:
						flag = (sprℐ.\u171B() != 0f);
						num = 25;
						continue;
					case 34:
						if (sprℐ.\u171D() < 0)
						{
							num = 26;
							continue;
						}
						goto IL_367;
					case 35:
						if (flag)
						{
							num = 32;
							continue;
						}
						goto IL_3DC;
					case 36:
						if (sprℐ.\u171D() < 0)
						{
							num = 19;
							continue;
						}
						goto IL_3DC;
					case 37:
						goto IL_362;
					case 38:
						num = 36;
						continue;
					case 39:
					{
						Paragraph paragraph = (A_0.\u1718() as sprᴛ).ᜁ() as Paragraph;
						int count = paragraph.ChildObjects.Count;
						num = 22;
						continue;
					}
					}
					break;
					IL_137:
					num = 14;
					continue;
					IL_20A:
					num = 16;
					continue;
					IL_55F:
					goto IL_20A;
					IL_320:
					a_2.X += sprℐ.\u171B() + sprℐ.ᜢ();
					a_2.Width -= sprℐ.\u171B() + sprℐ.ᜢ();
					num = 37;
					continue;
					IL_367:
					num = 4;
					continue;
					IL_3DC:
					num = 34;
					continue;
					IL_564:
					num = 13;
				}
			}
			IL_362:
			IL_5B2:
			this.ᜂ = A_0.ᜀ(a_2);
			(this.ᜆ as spr\u1DA4).ᜀ(new sprḈ.ᜀ());
			(this.ᜆ as spr\u1DA4).ᜀ(0f);
			return;
		}
		}
	}

	// Token: 0x060018FA RID: 6394 RVA: 0x00184414 File Offset: 0x00183414
	protected override spr\u2573 ᜃ()
	{
		if (base.ᜏ() != null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_0A;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return new spr\u25E5(base.ᜏ(), this.ᜆ);
		}
		IL_0A:
		return null;
	}

	// Token: 0x060018FB RID: 6395 RVA: 0x0018446C File Offset: 0x0018346C
	protected override void ᜁ(spr\u2573 A_0)
	{
		for (;;)
		{
			this.ᜇ = A_0.\u171C();
			int num = 6;
			for (;;)
			{
				bool flag;
				bool flag2;
				switch (num)
				{
				case 0:
				{
					sprℐ sprℐ;
					flag = sprℐ.\u1719();
					goto IL_BC;
				}
				case 1:
				{
					sprℐ sprℐ;
					if (sprℐ == null)
					{
						num = 3;
						continue;
					}
					num = 0;
					continue;
				}
				case 2:
					if (!flag2)
					{
						num = 7;
						continue;
					}
					goto IL_A6;
				case 3:
					num = 4;
					continue;
				case 4:
					flag = false;
					goto IL_BC;
				case 5:
				{
					sprℐ sprℐ = base.\u171A() as sprℐ;
					num = 1;
					continue;
				}
				case 6:
					if (this.ᜃ)
					{
						num = 5;
						continue;
					}
					goto IL_E3;
				case 7:
					goto IL_D6;
				}
				break;
				IL_BC:
				flag2 = flag;
				num = 2;
			}
		}
		IL_51:
		this.ᜁ = A_0.\u1716();
		this.ᜀ = LayoutState.Splitted;
		return;
		IL_A6:
		this.ᜀ = LayoutState.NotFitted;
		return;
		IL_D6:
		if (true)
		{
		}
		goto IL_51;
		IL_E3:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_51;
		default:
			if (false)
			{
			}
			this.ᜀ = LayoutState.NotFitted;
			return;
		}
	}

	// Token: 0x060018FC RID: 6396 RVA: 0x00184580 File Offset: 0x00183580
	protected override void ᜄ(spr\u2573 A_0)
	{
		for (;;)
		{
			base.ᜆ(A_0);
			this.ᜀ = LayoutState.Fitted;
			sprℐ sprℐ = A_0.\u171A() as sprℐ;
			int num = 1;
			for (;;)
			{
				bool flag;
				bool flag2;
				switch (num)
				{
				case 0:
					return;
				case 1:
					if (sprℐ == null)
					{
						num = 2;
						continue;
					}
					num = 5;
					continue;
				case 2:
					num = 6;
					continue;
				case 3:
					this.ᜅ.ᜊ();
					this.ᜀ = LayoutState.Breaked;
					goto IL_70;
				case 4:
					if (!flag)
					{
						return;
					}
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_70;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				case 5:
					flag2 = sprℐ.\u171E();
					goto IL_7A;
				case 6:
					flag2 = false;
					goto IL_7A;
				}
				break;
				IL_70:
				num = 0;
				continue;
				IL_7A:
				flag = flag2;
				num = 4;
			}
		}
	}

	// Token: 0x060018FD RID: 6397 RVA: 0x00184664 File Offset: 0x00183664
	protected override void ᜂ(spr\u2573 A_0)
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
		base.ᜆ(A_0);
		this.ᜂ = A_0.\u1716();
		this.ᜃ = true;
	}

	// Token: 0x060018FE RID: 6398 RVA: 0x001846BC File Offset: 0x001836BC
	protected override void ᜁ()
	{
		switch (0)
		{
		default:
		{
			RectangleF rectangleF;
			for (;;)
			{
				rectangleF = this.ᜂ.ᜁ();
				int num = 10;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						num = 12;
						continue;
					case 1:
					{
						if (true)
						{
						}
						sprᦰ sprᦰ;
						if (sprᦰ.ᜂ() is DocPicture)
						{
							num = 0;
							continue;
						}
						goto IL_12A;
					}
					case 2:
						goto IL_1D2;
					case 3:
						return;
					case 4:
					{
						int count;
						if (num2 >= count)
						{
							goto IL_1EA;
						}
						sprᦰ sprᦰ = this.ᜂ.ᜊ()[num2];
						num = 1;
						continue;
					}
					case 5:
						goto IL_1D7;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1EA;
						default:
						{
							if (false)
							{
							}
							RectangleF rectangleF2 = this.ᜅ.ᜆ();
							num = 11;
							continue;
						}
						}
						break;
					case 7:
						goto IL_125;
					case 8:
						goto IL_1D7;
					case 9:
						this.ᜅ.ᜃ();
						num = 7;
						continue;
					case 10:
					{
						if (!base.\u171E().\u171D().ᜃ())
						{
							num = 6;
							continue;
						}
						this.ᜅ.ᜁ((double)base.\u171E().\u171D().ᜇ());
						base.\u171E().\u171D().ᜀ(false);
						base.\u171E().\u171D().ᜀ(0f);
						num2 = 0;
						int count = this.ᜂ.ᜊ().Count;
						num = 8;
						continue;
					}
					case 11:
					{
						RectangleF rectangleF2;
						if (rectangleF2.X != this.ᜅ.ᜇ().X)
						{
							num = 9;
							continue;
						}
						goto IL_261;
					}
					case 12:
					{
						sprᦰ sprᦰ;
						if ((sprᦰ.ᜂ() as DocPicture).TextWrappingStyle == TextWrappingStyle.Inline)
						{
							num = 2;
							continue;
						}
						goto IL_12A;
					}
					}
					break;
					IL_12A:
					num2++;
					num = 5;
					continue;
					IL_1D7:
					num = 4;
					continue;
					IL_1EA:
					num = 3;
				}
			}
			IL_125:
			goto IL_261;
			IL_1D2:
			rectangleF.Height -= (float)this.ᜂ.ᜂ().ᜀ().ᜋ().ᜁ();
			this.ᜅ.ᜂ((double)rectangleF.Bottom);
			return;
			IL_261:
			rectangleF.Height -= (float)this.ᜂ.ᜂ().ᜀ().ᜋ().ᜁ();
			this.ᜅ.ᜂ((double)rectangleF.Bottom);
			return;
		}
		}
	}

	// Token: 0x060018FF RID: 6399 RVA: 0x00184968 File Offset: 0x00183968
	protected override void ᜂ()
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			double num4;
			for (;;)
			{
				this.ᜂ.ᜁ(base.\u171E());
				bool flag = false;
				sprℐ sprℐ = this.ᜂ.ᜂ().ᜀ() as sprℐ;
				int num = 51;
				for (;;)
				{
					RectangleF rectangleF;
					int num3;
					float num2;
					Paragraph paragraph;
					Spire.Layouting.HorizontalAlignment horizontalAlignment;
					Paragraph paragraph2;
					int num5;
					Spire.Layouting.HorizontalAlignment horizontalAlignment3;
					sprᡌ sprᡌ;
					RectangleF rectangleF2;
					CharacterSpacing characterSpacing;
					Paragraph paragraph3;
					switch (num)
					{
					case 0:
						if (rectangleF.Width < 0f)
						{
							num = 59;
							continue;
						}
						num2 = this.ᜂ.ᜊ()[num3].ᜁ().Right;
						num = 36;
						continue;
					case 1:
						num = 87;
						continue;
					case 2:
						if (!((this.ᜂ.ᜂ() as sprᴛ).ᜁ() is Paragraph))
						{
							goto IL_693;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_7BA;
						default:
							if (false)
							{
							}
							num = 83;
							continue;
						}
						break;
					case 3:
						paragraph = null;
						goto IL_376;
					case 4:
						if (!(this.ᜂ.ᜊ()[num3].ᜂ() is TextRange))
						{
							num = 92;
							continue;
						}
						goto IL_64C;
					case 5:
						if (horizontalAlignment == Spire.Layouting.HorizontalAlignment.Justify)
						{
							num = 46;
							continue;
						}
						goto IL_693;
					case 6:
						goto IL_693;
					case 7:
						num4 = (double)(this.ᜅ.ᜇ().Right - num2) - sprℐ.ᜰ().ᜂ();
						num = 9;
						continue;
					case 8:
						if (this.ᜂ.ᜊ().Count > 0)
						{
							num = 78;
							continue;
						}
						goto IL_693;
					case 9:
						goto IL_5AE;
					case 10:
						if (!((this.ᜂ.ᜂ() as sprᴛ).ᜁ() is Paragraph))
						{
							num = 96;
							continue;
						}
						num = 39;
						continue;
					case 11:
						if (paragraph2 != null)
						{
							num = 105;
							continue;
						}
						goto IL_D9E;
					case 12:
						goto IL_70D;
					case 13:
						paragraph = (this.ᜂ.ᜂ() as Paragraph);
						goto IL_376;
					case 14:
						if (this.ᜂ.ᜂ() is sprᴛ)
						{
							num = 16;
							continue;
						}
						goto IL_693;
					case 15:
						num = 107;
						continue;
					case 16:
						num = 2;
						continue;
					case 17:
						num = 85;
						continue;
					case 18:
						if (this.ᜂ.ᜊ().Count > 1)
						{
							goto IL_7BA;
						}
						num4 = (double)(this.ᜅ.ᜇ().Right - this.ᜂ.ᜁ().Right) - sprℐ.ᜰ().ᜂ();
						num = 75;
						continue;
					case 19:
						if (this.ᜂ.ᜅ() != ClipboardData.b("㡪ᵬͮᡰݲŴቶᵸ", a_))
						{
							num = 95;
							continue;
						}
						goto IL_EB2;
					case 20:
						if (this.ᜂ.ᜊ()[num3].ᜂ() is DocPicture)
						{
							num = 60;
							continue;
						}
						goto IL_C0C;
					case 21:
						goto IL_E24;
					case 22:
						goto IL_D69;
					case 23:
						goto IL_64C;
					case 24:
						if ((this.ᜂ.ᜊ()[num3].ᜂ() as DocPicture).TextWrappingStyle == TextWrappingStyle.Inline)
						{
							num = 43;
							continue;
						}
						goto IL_C0C;
					case 25:
						goto IL_D9E;
					case 26:
						if (this.ᜂ.ᜂ() is Paragraph)
						{
							num = 34;
							continue;
						}
						goto IL_EB2;
					case 27:
						if (!(this.ᜂ.ᜂ() is Paragraph))
						{
							num = 76;
							continue;
						}
						num = 13;
						continue;
					case 28:
						if (num4 < 0.0)
						{
							num = 70;
							continue;
						}
						goto IL_D69;
					case 29:
						if (this.ᜂ.ᜅ() != ClipboardData.b("㡪ᵬͮᡰݲŴቶᵸ", a_))
						{
							num = 45;
							continue;
						}
						goto IL_693;
					case 30:
						num = 64;
						continue;
					case 31:
					{
						Spire.Layouting.HorizontalAlignment horizontalAlignment2;
						switch (horizontalAlignment2)
						{
						case Spire.Layouting.HorizontalAlignment.Center:
							goto IL_868;
						case Spire.Layouting.HorizontalAlignment.Right:
							goto IL_363;
						case Spire.Layouting.HorizontalAlignment.Justify:
						case Spire.Layouting.HorizontalAlignment.Distributed:
							this.ᜂ.ᜀ(base.\u171E(), num4);
							num5 = 0;
							num = 68;
							continue;
						default:
							num = 74;
							continue;
						}
						break;
					}
					case 32:
						num = 56;
						continue;
					case 33:
						horizontalAlignment3 = sprℐ.ᜠ();
						goto IL_786;
					case 34:
						num = 19;
						continue;
					case 35:
						goto IL_51B;
					case 36:
						goto IL_4D0;
					case 37:
						if (this.ᜂ.ᜊ()[num3].ᜂ() is Table)
						{
							num = 80;
							continue;
						}
						goto IL_2ED;
					case 38:
					{
						Section section;
						if (section != null)
						{
							num = 100;
							continue;
						}
						goto IL_D37;
					}
					case 39:
						paragraph = ((this.ᜂ.ᜂ() as sprᴛ).ᜁ() as Paragraph);
						goto IL_376;
					case 40:
						if (sprᡌ.ᜂ().ᜁ() != TextWrappingStyle.Behind)
						{
							num = 101;
							continue;
						}
						goto IL_51B;
					case 41:
						this.ᜂ.ᜊ()[num5 + 1].ᜀ(new RectangleF(this.ᜂ.ᜊ()[num5 + 1].ᜁ().Location.X, this.ᜂ.ᜊ()[num5 + 1].ᜁ().Location.Y, this.ᜂ.ᜊ()[num5 + 1].ᜁ().Width, this.ᜂ.ᜊ()[num5 + 1].ᜁ().Height));
						num = 103;
						continue;
					case 42:
						goto IL_4E8;
					case 43:
						num2 = this.ᜂ.ᜊ()[num3].ᜁ().Right;
						num = 47;
						continue;
					case 44:
						if (sprᡌ.ᜂ().ᜁ() != TextWrappingStyle.InFrontOfText)
						{
							num = 77;
							continue;
						}
						goto IL_51B;
					case 45:
						num = 8;
						continue;
					case 46:
						flag = true;
						num = 6;
						continue;
					case 47:
						goto IL_C0C;
					case 48:
						goto IL_70D;
					case 49:
						if (num5 >= this.ᜂ.ᜊ().Count)
						{
							num = 98;
							continue;
						}
						this.ᜂ.ᜊ()[num5].ᜀ(horizontalAlignment);
						this.ᜂ.ᜊ()[num5].ᜀ(new RectangleF(this.ᜂ.ᜊ()[num5].ᜁ().X, this.ᜂ.ᜊ()[num5].ᜁ().Y, this.ᜂ.ᜊ()[num5].ᜁ().Width, this.ᜂ.ᜊ()[num5].ᜁ().Height));
						num = 65;
						continue;
					case 50:
						if (this.ᜂ.ᜊ()[0].ᜃ())
						{
							num = 89;
							continue;
						}
						goto IL_4E8;
					case 51:
						if (sprℐ == null)
						{
							num = 30;
							continue;
						}
						num = 33;
						continue;
					case 52:
						if (num3 >= this.ᜂ.ᜊ().Count)
						{
							num = 7;
							continue;
						}
						num = 37;
						continue;
					case 53:
						goto IL_D37;
					case 54:
						num = 86;
						continue;
					case 55:
						num3 = 0;
						num = 48;
						continue;
					case 56:
						if ((this.ᜂ.ᜊ()[num3].ᜂ() as Table).\u1712.TextWrappingStyle == TextWrappingStyle.Inline)
						{
							num = 106;
							continue;
						}
						goto IL_2ED;
					case 57:
						num = 10;
						continue;
					case 58:
						goto IL_2ED;
					case 59:
						num2 = this.ᜂ.ᜊ()[num3].ᜁ().Left;
						num = 72;
						continue;
					case 60:
						num = 24;
						continue;
					case 61:
						if (base.ᜂ(paragraph2))
						{
							num = 81;
							continue;
						}
						goto IL_D9E;
					case 62:
						if (!rectangleF2.IsEmpty)
						{
							num = 15;
							continue;
						}
						goto IL_51B;
					case 63:
						if (this.ᜂ.ᜂ() is Paragraph)
						{
							num = 93;
							continue;
						}
						goto IL_AF5;
					case 64:
						horizontalAlignment3 = Spire.Layouting.HorizontalAlignment.Left;
						goto IL_786;
					case 65:
						if (num5 < this.ᜂ.ᜊ().Count - 1)
						{
							num = 41;
							continue;
						}
						goto IL_408;
					case 66:
						goto IL_AF5;
					case 67:
						if (paragraph2.Format.FrameWidth == 0)
						{
							num = 102;
							continue;
						}
						goto IL_D9E;
					case 68:
						goto IL_E24;
					case 69:
						if (this.ᜂ.ᜊ().Count > 0)
						{
							num = 54;
							continue;
						}
						goto IL_EB2;
					case 70:
						num4 = 0.0;
						num = 22;
						continue;
					case 71:
						if ((this.ᜂ.ᜊ()[num3].ᜂ() as Table).IsTextBox)
						{
							num = 32;
							continue;
						}
						goto IL_2ED;
					case 72:
						goto IL_4D0;
					case 73:
					{
						Spire.Layouting.HorizontalAlignment horizontalAlignment2 = horizontalAlignment;
						num = 31;
						continue;
					}
					case 74:
						return;
					case 75:
						goto IL_5AE;
					case 76:
						num = 82;
						continue;
					case 77:
						num4 = (double)(sprᡌ.ᜀ().X - this.ᜂ.ᜁ().Right);
						num = 35;
						continue;
					case 78:
						num = 79;
						continue;
					case 79:
						if (characterSpacing == CharacterSpacing.doNotCompress)
						{
							num = 1;
							continue;
						}
						goto IL_693;
					case 80:
						num = 71;
						continue;
					case 81:
						num = 67;
						continue;
					case 82:
						if (this.ᜂ.ᜂ() is sprᴛ)
						{
							num = 57;
							continue;
						}
						goto IL_774;
					case 83:
						num = 29;
						continue;
					case 84:
						if (paragraph3 != null)
						{
							num = 97;
							continue;
						}
						goto IL_D37;
					case 85:
						if (this.ᜂ.ᜊ()[num3].ᜂ() is DocOleObject)
						{
							num = 23;
							continue;
						}
						goto IL_4D0;
					case 86:
						if (characterSpacing == CharacterSpacing.doNotCompress)
						{
							num = 104;
							continue;
						}
						goto IL_EB2;
					case 87:
						if (!this.ᜂ.ᜊ()[0].ᜃ())
						{
							num = 42;
							continue;
						}
						goto IL_693;
					case 88:
						if (!this.ᜁ(this.ᜂ.ᜂ() as Paragraph))
						{
							num = 66;
							continue;
						}
						return;
					case 89:
						goto IL_EB2;
					case 90:
						num = 40;
						continue;
					case 91:
						if (!flag)
						{
							num = 73;
							continue;
						}
						return;
					case 92:
						num = 108;
						continue;
					case 93:
						num = 88;
						continue;
					case 94:
						num = 5;
						continue;
					case 95:
						if (true)
						{
						}
						num = 69;
						continue;
					case 96:
						goto IL_774;
					case 97:
					{
						Section section = this.ᜀ(paragraph3);
						num = 38;
						continue;
					}
					case 98:
						return;
					case 99:
						if (this.ᜂ.ᜊ().Count > 0)
						{
							num = 94;
							continue;
						}
						goto IL_693;
					case 100:
					{
						Section section;
						characterSpacing = section.PageSetup.CharacterSpacingControl;
						num = 53;
						continue;
					}
					case 101:
						num = 44;
						continue;
					case 102:
						num4 = 0.0;
						num = 25;
						continue;
					case 103:
						goto IL_408;
					case 104:
						num = 50;
						continue;
					case 105:
						num = 61;
						continue;
					case 106:
						num2 = this.ᜂ.ᜊ()[num3].ᜁ().Right;
						num = 58;
						continue;
					case 107:
						if (sprᡌ.ᜂ().ᜁ() != TextWrappingStyle.Inline)
						{
							num = 90;
							continue;
						}
						goto IL_51B;
					case 108:
						if (!(this.ᜂ.ᜊ()[num3].ᜂ() is spr\u208E))
						{
							num = 17;
							continue;
						}
						goto IL_64C;
					}
					break;
					IL_2ED:
					num = 20;
					continue;
					IL_376:
					paragraph3 = paragraph;
					num = 84;
					continue;
					IL_408:
					num5++;
					num = 21;
					continue;
					IL_4D0:
					num3++;
					num = 12;
					continue;
					IL_4E8:
					num = 99;
					continue;
					IL_51B:
					paragraph2 = this.ᜀ();
					num = 11;
					continue;
					IL_5AE:
					RectangleF a_2 = new RectangleF(this.ᜂ.ᜁ().Location, new SizeF((float)((double)this.ᜂ.ᜁ().Width + num4), this.ᜂ.ᜁ().Height));
					sprᡌ = base.\u171E().\u171D().ᜀ(a_2, this.ᜃ.ᜂ());
					rectangleF2 = sprᡌ.ᜀ();
					num = 62;
					continue;
					IL_64C:
					rectangleF = this.ᜂ.ᜊ()[num3].ᜁ();
					num = 0;
					continue;
					IL_693:
					num = 63;
					continue;
					IL_70D:
					num = 52;
					continue;
					IL_774:
					num = 3;
					continue;
					IL_786:
					horizontalAlignment = horizontalAlignment3;
					num4 = 0.0;
					num2 = 0f;
					num = 18;
					continue;
					IL_7BA:
					num = 55;
					continue;
					IL_AF5:
					num = 91;
					continue;
					IL_C0C:
					num = 4;
					continue;
					IL_D37:
					num = 26;
					continue;
					IL_D69:
					characterSpacing = CharacterSpacing.compressPunctuation;
					num = 27;
					continue;
					IL_D9E:
					num = 28;
					continue;
					IL_E24:
					num = 49;
					continue;
					IL_EB2:
					num = 14;
				}
			}
			IL_363:
			this.ᜂ.ᜁ(base.\u171E(), num4);
			return;
			IL_868:
			num4 /= 2.0;
			this.ᜂ.ᜀ(num4, 0.0, true);
			return;
		}
		}
	}

	// Token: 0x06001900 RID: 6400 RVA: 0x00185998 File Offset: 0x00184998
	protected override void ᜀ(short A_0)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_A2;
		}
		if (false)
		{
		}
		RectangleF rectangleF;
		for (;;)
		{
			if (true)
			{
			}
			rectangleF = this.ᜂ.ᜁ();
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					if (A_0 != -4)
					{
						num = 2;
						continue;
					}
					goto IL_A2;
				case 2:
					return;
				case 3:
					num = 5;
					continue;
				case 4:
					if (A_0 != -16)
					{
						num = 3;
						continue;
					}
					goto IL_C7;
				case 5:
					if (A_0 != -8)
					{
						num = 0;
						continue;
					}
					goto IL_C7;
				}
				break;
			}
		}
		return;
		IL_C7:
		this.ᜂ.ᜀ((double)(-(double)rectangleF.Width), 0.0, false);
		return;
		IL_A2:
		this.ᜂ.ᜀ((double)(-(double)rectangleF.Width / 2f), 0.0, false);
	}

	// Token: 0x06001901 RID: 6401 RVA: 0x00185A8C File Offset: 0x00184A8C
	private new Section ᜀ(DocumentObject A_0)
	{
		DocumentObject documentObject;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
		{
			IL_3E:
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					if (documentObject.Owner != null)
					{
						num = 5;
						continue;
					}
					goto IL_9F;
				case 1:
					if (documentObject is Section)
					{
						num = 3;
						continue;
					}
					num = 0;
					continue;
				case 2:
					goto IL_48;
				case 3:
					goto IL_6A;
				case 4:
					goto IL_48;
				case 5:
					documentObject = documentObject.Owner;
					num = 4;
					continue;
				}
				goto IL_3C;
				IL_48:
				num = 1;
			}
			IL_6A:
			IL_9F:
			return documentObject as Section;
		}
		default:
			if (false)
			{
			}
			break;
		}
		IL_3C:
		documentObject = A_0;
		goto IL_3E;
	}

	// Token: 0x06001902 RID: 6402 RVA: 0x00185B40 File Offset: 0x00184B40
	private new Paragraph ᜀ()
	{
		for (;;)
		{
			Paragraph result = null;
			int num = 6;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					if ((this.ᜂ.ᜂ() as sprᴛ).ᜁ() is Paragraph)
					{
						num = 2;
						continue;
					}
					return result;
				case 1:
					num = 0;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						result = ((this.ᜂ.ᜂ() as sprᴛ).ᜁ() as Paragraph);
						num = 5;
						continue;
					}
					break;
				case 3:
					goto IL_50;
				case 4:
					if (this.ᜂ.ᜂ() is sprᴛ)
					{
						num = 1;
						continue;
					}
					return result;
				case 5:
					return result;
				case 6:
					if (this.ᜂ.ᜂ() is Paragraph)
					{
						num = 3;
						continue;
					}
					num = 4;
					continue;
				}
				break;
			}
		}
		IL_50:
		return this.ᜂ.ᜂ() as Paragraph;
	}

	// Token: 0x06001903 RID: 6403 RVA: 0x00185C5C File Offset: 0x00184C5C
	internal new bool ᜁ(Paragraph A_0)
	{
		int a_ = 16;
		int num = 13;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 8;
				continue;
			case 1:
				if (A_0.ChildObjects.FirstItem != null)
				{
					num = 9;
					continue;
				}
				return false;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					if (new Hyperlink(A_0.ChildObjects.FirstItem as Field).BookmarkName != null)
					{
						if (true)
						{
						}
						num = 4;
						continue;
					}
					return false;
				}
				break;
			case 3:
				if (new Hyperlink(A_0.ChildObjects.FirstItem as Field).BookmarkName.StartsWith(ClipboardData.b("⥵ⱷᕹύ", a_)))
				{
					num = 7;
					continue;
				}
				return false;
			case 4:
				num = 3;
				continue;
			case 5:
				if ((A_0.ChildObjects.FirstItem as Field).Type == FieldType.FieldHyperlink)
				{
					num = 6;
					continue;
				}
				return false;
			case 6:
				num = 2;
				continue;
			case 7:
				goto IL_199;
			case 8:
				if (A_0.ChildObjects.FirstItem is Field)
				{
					num = 12;
					continue;
				}
				return false;
			case 9:
				num = 11;
				continue;
			case 10:
				num = 1;
				continue;
			case 11:
				if (!(A_0.ChildObjects.FirstItem is TableOfContent))
				{
					num = 0;
					continue;
				}
				return true;
			case 12:
				num = 5;
				continue;
			}
			IL_5B:
			if (A_0 != null)
			{
				num = 10;
				continue;
			}
			return false;
			goto IL_5B;
		}
		return true;
		IL_199:
		return true;
	}

	// Token: 0x06001904 RID: 6404 RVA: 0x00185E34 File Offset: 0x00184E34
	protected override void ᜈ()
	{
		for (;;)
		{
			IL_50:
			base.\u171A();
			Paragraph a_ = null;
			int num = 3;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						this.ᜀ(a_);
						num = 2;
						continue;
					case 1:
					{
						sprᴛ sprᴛ;
						a_ = (sprᴛ.ᜁ() as Paragraph);
						num = 8;
						continue;
					}
					case 2:
						return;
					case 3:
						if (this.ᜂ.ᜂ() is Paragraph)
						{
							if (true)
							{
							}
							num = 4;
							continue;
						}
						num = 7;
						continue;
					case 4:
						a_ = (this.ᜂ.ᜂ() as Paragraph);
						num = 6;
						continue;
					case 5:
					{
						sprᴛ sprᴛ;
						if (sprᴛ.ᜁ() is Paragraph)
						{
							num = 1;
							continue;
						}
						goto IL_8B;
					}
					case 6:
						goto IL_8B;
					case 7:
						if (this.ᜂ.ᜂ() is sprᴛ)
						{
							num = 10;
							continue;
						}
						goto IL_8B;
					case 8:
						goto IL_8B;
					case 9:
						if ((this.ᜆ as spr\u1DA4).ᜅ())
						{
							num = 0;
							continue;
						}
						return;
					case 10:
					{
						sprᴛ sprᴛ = this.ᜂ.ᜂ() as sprᴛ;
						goto IL_C9;
					}
					}
					goto IL_50;
					IL_8B:
					num = 9;
					continue;
				}
				IL_C9:
				num = 5;
			}
		}
	}

	// Token: 0x06001905 RID: 6405 RVA: 0x00185FBC File Offset: 0x00184FBC
	private new void ᜀ(Paragraph A_0)
	{
		int a_ = 17;
		int num = 0;
		switch (num)
		{
		default:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				num = 1;
				break;
			}
			for (;;)
			{
				string text;
				Dictionary<int, string>.Enumerator enumerator;
				switch (num)
				{
				case 0:
					try
					{
						num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_19D;
							case 1:
							{
								RectangleF rectangleF = this.ᜃ.ᜁ();
								num = 3;
								continue;
							}
							case 3:
							{
								RectangleF rectangleF;
								if (rectangleF.Width > 0f)
								{
									num = 8;
									continue;
								}
								break;
							}
							case 4:
							{
								string a;
								if (a == text)
								{
									num = 1;
									continue;
								}
								break;
							}
							case 5:
							{
								if (!enumerator.MoveNext())
								{
									num = 6;
									continue;
								}
								KeyValuePair<int, string> keyValuePair = enumerator.Current;
								string a = keyValuePair.Value.ToLower().Replace(ClipboardData.b("坶", a_), "");
								num = 4;
								continue;
							}
							case 6:
								num = 0;
								continue;
							case 8:
								this.ᜆ.ᜀ(this.ᜃ);
								num = 7;
								continue;
							}
							IL_CA:
							num = 5;
							continue;
							goto IL_CA;
						}
						IL_19D:
						return;
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
					goto IL_1B0;
				case 2:
					goto IL_228;
				case 3:
					goto IL_228;
				case 4:
					if (text != null)
					{
						num = 6;
						continue;
					}
					goto IL_1B0;
				case 5:
					text = A_0.StyleName;
					num = 4;
					continue;
				case 6:
					text = text.ToLower().Replace(ClipboardData.b("坶", a_), "");
					num = 3;
					continue;
				}
				if ((this.ᜆ as spr\u1DA4).ᜉ().Count > 0)
				{
					if (true)
					{
					}
					num = 5;
					continue;
				}
				break;
				IL_1B0:
				text = ClipboardData.b("᥶ᙸॺၼṾ", a_);
				num = 2;
				continue;
				IL_228:
				enumerator = (this.ᜆ as spr\u1DA4).ᜉ().GetEnumerator();
				num = 0;
			}
			return;
		}
	}
}
