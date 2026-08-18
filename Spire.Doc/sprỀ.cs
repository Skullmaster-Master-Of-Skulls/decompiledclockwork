using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Spire.CompoundFile.Doc;
using Spire.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Core;
using Spire.Doc.Core.Biff_Records;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;

// Token: 0x020003E0 RID: 992
internal class sprỀ : spr\u19CB
{
	// Token: 0x060037CD RID: 14285 RVA: 0x00343520 File Offset: 0x00342520
	public new void ᜀ(sprᬛ A_0, Document A_1)
	{
		for (;;)
		{
			sprᣄ.ᜀ().ᜁ().Clear();
			sprᣄ.ᜀ().ᜂ().Clear();
			base.ᜀ(A_1);
			this.ᜅ(A_0);
			A_0.ᜀ(A_1);
			A_1.WriteProtected = A_0.ᜱ().\u1718().ម();
			A_1.HasPicture = A_0.ᜱ().\u1718().\u177A();
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜁ(A_0, WordSubdocument.HeaderFooter);
					this.ᜂ(A_0);
					this.ᜀ(A_1.Sections[0]);
					num = 3;
					continue;
				case 1:
					goto IL_159;
				case 2:
					if (A_0.\u1735() == WordChunkType.DocumentEnd)
					{
						num = 0;
						continue;
					}
					goto IL_15B;
				case 3:
					if (this.\u1712.\u173C())
					{
						num = 5;
						continue;
					}
					goto IL_1F1;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_159;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						A_1.WordVersion = ((A_0.ᜱ().\u1718().\u171A() > 0) ? A_0.ᜱ().\u1718().\u171A() : A_0.ᜱ().\u1718().\u1739());
						this.ᜀ(A_0);
						A_1.ᜇ = true;
						this.ᜄ(A_0);
						A_1.FontSubstitutionTable = A_0.ᜥ().ᜅ();
						this.ᜃ(A_0);
						this.ᜁ();
						this.ᜁ(A_0, WordSubdocument.Footnote);
						this.ᜁ(A_0, WordSubdocument.Annotation);
						this.ᜁ(A_0, WordSubdocument.Endnote);
						this.ᜁ(A_0, WordSubdocument.TextBox);
						num = 1;
						continue;
					}
					break;
				case 5:
					this.ᜀ();
					num = 6;
					continue;
				case 6:
					goto IL_1A9;
				}
				break;
				IL_15B:
				ISection section = A_1.AddSection();
				base.ᜀ(A_0, section.Body);
				this.ᜀ(A_0, section);
				num = 2;
				continue;
				IL_159:
				goto IL_15B;
			}
		}
		IL_1A9:
		IL_1F1:
		A_0.\u171D();
		A_1.ᜇ = false;
	}

	// Token: 0x060037CE RID: 14286 RVA: 0x0034372C File Offset: 0x0034272C
	private void ᜅ(sprᬛ A_0)
	{
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_BA;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			case 1:
				return;
			case 2:
				goto IL_BA;
			case 3:
				if (this.\u1712.\u1734() != string.Empty)
				{
					num = 2;
					continue;
				}
				return;
			}
			if (this.\u1712.\u1734() != null)
			{
				if (true)
				{
				}
				num = 0;
				continue;
			}
			break;
			IL_BA:
			A_0.ᜀ(new spr\u2214(this.\u1712.\u1734));
			num = 1;
		}
	}

	// Token: 0x060037CF RID: 14287 RVA: 0x003437F8 File Offset: 0x003427F8
	private void ᜄ(sprᬛ A_0)
	{
		int a_ = 17;
		switch (0)
		{
		default:
		{
			spr\u2305 spr_u;
			for (;;)
			{
				spr_u = A_0.ᜥ();
				int num = spr_u.ᜆ();
				Dictionary<string, int> dictionary = new Dictionary<string, int>();
				int num2 = 0;
				int num3 = 65;
				for (;;)
				{
					sprᲵ sprᲵ;
					int num4;
					int num5;
					int a_2;
					ParagraphStyle paragraphStyle;
					string text;
					int num7;
					sprᯉ sprᯉ;
					switch (num3)
					{
					case 0:
						if (num2 >= num)
						{
							num3 = 56;
							continue;
						}
						sprᲵ = spr_u.ᜁ(num2);
						num3 = 44;
						continue;
					case 1:
						goto IL_191;
					case 2:
						if (sprᲵ.ᜌ() != ClipboardData.b("㥶ᙸॺၼṾ", a_))
						{
							num3 = 24;
							continue;
						}
						goto IL_1A6;
					case 3:
						if (num4 != 4095)
						{
							num3 = 53;
							continue;
						}
						goto IL_8BB;
					case 4:
						goto IL_296;
					case 5:
					{
						int count;
						if (num5 >= count)
						{
							num3 = 11;
							continue;
						}
						Style style = this.\u1712.Styles[num5] as Style;
						num3 = 64;
						continue;
					}
					case 6:
						spr_u.ᜆ = true;
						spr_u.ᜈ = sprᲵ.ᜌ();
						num3 = 55;
						continue;
					case 7:
						spr_u.ᜅ = true;
						spr_u.ᜇ = sprᲵ.ᜌ();
						num3 = 45;
						continue;
					case 8:
						goto IL_329;
					case 9:
						num3 = 33;
						continue;
					case 10:
						if (sprᲵ.ᜈ() == WordStyleType.TableStyle)
						{
							num3 = 46;
							continue;
						}
						goto IL_523;
					case 11:
						goto IL_382;
					case 12:
					{
						sprᲵ sprᲵ2;
						if (sprᲵ2 != null)
						{
							num3 = 42;
							continue;
						}
						goto IL_8BB;
					}
					case 13:
					{
						Style style;
						if (style is ParagraphStyle)
						{
							num3 = 48;
							continue;
						}
						goto IL_60D;
					}
					case 14:
						if (sprᲵ.ᜃ() != null)
						{
							num3 = 27;
							continue;
						}
						goto IL_523;
					case 15:
						if (sprᲵ.ᜈ() == WordStyleType.TableStyle)
						{
							goto IL_946;
						}
						goto IL_794;
					case 16:
						goto IL_1A6;
					case 17:
						goto IL_523;
					case 18:
						goto IL_60D;
					case 19:
					{
						Style style;
						a_2 = spr_u.ᜁ(style.Name, style.StyleType == StyleType.CharacterStyle);
						int num6 = spr_u.ᜁ(a_2).ᜀ();
						num3 = 25;
						continue;
					}
					case 20:
						goto IL_8BB;
					case 21:
						if (paragraphStyle.ListFormat.CurrentListLevel != null)
						{
							num3 = 38;
							continue;
						}
						goto IL_191;
					case 22:
						if (A_0.\u1732())
						{
							num3 = 43;
							continue;
						}
						goto IL_191;
					case 23:
						goto IL_361;
					case 24:
						sprᲵ.ᜁ(ClipboardData.b("㥶ᙸॺၼṾ", a_));
						num3 = 16;
						continue;
					case 25:
					{
						int num6;
						if (num6 != 4095)
						{
							num3 = 39;
							continue;
						}
						num3 = 59;
						continue;
					}
					case 26:
					{
						Style style;
						sprᲵ sprᲵ3;
						style.NextStyle = sprᲵ3.ᜌ();
						num3 = 8;
						continue;
					}
					case 27:
					{
						IStyle style3;
						Style style2 = style3 as Style;
						style2.TableStyleData = new byte[sprᲵ.ᜃ().Length];
						Buffer.BlockCopy(sprᲵ.ᜃ(), 0, style2.TableStyleData, 0, sprᲵ.ᜃ().Length);
						num3 = 17;
						continue;
					}
					case 28:
					{
						IStyle style3 = this.\u1712.ᜀ(StyleType.ParagraphStyle, text);
						paragraphStyle = (style3 as ParagraphStyle);
						paragraphStyle.StyleId = sprᲵ.ᜂ();
						paragraphStyle.IsPrimaryStyle = sprᲵ.ᜊ();
						paragraphStyle.IsSemiHidden = sprᲵ.ᜁ();
						paragraphStyle.UnhideWhenUsed = sprᲵ.ᜆ();
						(style3 as Style).TypeCode = sprᲵ.ᜈ();
						num3 = 10;
						continue;
					}
					case 29:
						if (num2 == 14)
						{
							num3 = 6;
							continue;
						}
						goto IL_20C;
					case 30:
						if (dictionary.ContainsKey(sprᲵ.ᜌ()))
						{
							num3 = 37;
							continue;
						}
						dictionary.Add(sprᲵ.ᜌ(), 0);
						num3 = 35;
						continue;
					case 31:
						num3 = 63;
						continue;
					case 32:
						goto IL_60D;
					case 33:
						if (sprᲵ.ᜃ() != null)
						{
							num3 = 62;
							continue;
						}
						goto IL_794;
					case 34:
						goto IL_191;
					case 35:
						goto IL_46F;
					case 36:
						goto IL_794;
					case 37:
					{
						string text2 = sprᲵ.ᜌ();
						text = sprᲵ.ᜌ() + ClipboardData.b("⡶", a_) + dictionary[sprᲵ.ᜌ()].ToString();
						Dictionary<string, int> dictionary2;
						string key;
						(dictionary2 = dictionary)[key = text2] = dictionary2[key] + 1;
						num3 = 49;
						continue;
					}
					case 38:
						paragraphStyle.ListFormat.CurrentListLevel.ParaStyleName = paragraphStyle.Name.Replace(ClipboardData.b("坶", a_), string.Empty);
						num3 = 34;
						continue;
					case 39:
					{
						int num6;
						sprᲵ sprᲵ4 = spr_u.ᜁ(num6);
						num3 = 60;
						continue;
					}
					case 40:
						num3 = 2;
						continue;
					case 41:
						if (num4 != 0)
						{
							num3 = 52;
							continue;
						}
						goto IL_8BB;
					case 42:
					{
						Style style;
						sprᲵ sprᲵ2;
						style.LinkStyle = sprᲵ2.ᜌ();
						if (true)
						{
						}
						num3 = 20;
						continue;
					}
					case 43:
					{
						int a_3 = (int)sprᲵ.ᜋ().\u1717();
						int a_4 = (int)sprᲵ.ᜋ().ᝈ();
						sprἹ.ᜀ(a_3, a_4, paragraphStyle.ListFormat, A_0);
						num3 = 21;
						continue;
					}
					case 44:
						if (num2 == 13)
						{
							num3 = 7;
							continue;
						}
						goto IL_899;
					case 45:
						goto IL_899;
					case 46:
						num3 = 14;
						continue;
					case 47:
					{
						sprᲵ sprᲵ3;
						if (sprᲵ3 != null)
						{
							num3 = 26;
							continue;
						}
						goto IL_329;
					}
					case 48:
					{
						Style style;
						style.\u170D();
						num3 = 18;
						continue;
					}
					case 49:
						goto IL_46F;
					case 50:
						if (num7 != 4095)
						{
							num3 = 58;
							continue;
						}
						goto IL_329;
					case 51:
						if (sprᲵ.ᜌ() != null)
						{
							num3 = 31;
							continue;
						}
						goto IL_191;
					case 52:
					{
						sprᲵ sprᲵ2 = spr_u.ᜁ(num4);
						num3 = 12;
						continue;
					}
					case 53:
						num3 = 41;
						continue;
					case 54:
						if (!sprᲵ.ᜇ())
						{
							num3 = 28;
							continue;
						}
						sprᯉ = (sprᯉ)this.\u1712.ᜀ(StyleType.CharacterStyle, text);
						sprᯉ.StyleId = sprᲵ.ᜂ();
						sprᯉ.IsPrimaryStyle = sprᲵ.ᜊ();
						sprᯉ.IsSemiHidden = sprᲵ.ᜁ();
						sprᯉ.UnhideWhenUsed = sprᲵ.ᜆ();
						sprᯉ.TypeCode = sprᲵ.ᜈ();
						num3 = 15;
						continue;
					case 55:
						goto IL_20C;
					case 56:
					{
						num5 = 0;
						int count = this.\u1712.Styles.Count;
						num3 = 57;
						continue;
					}
					case 57:
						goto IL_361;
					case 58:
					{
						sprᲵ sprᲵ3 = spr_u.ᜁ(num7);
						num3 = 47;
						continue;
					}
					case 59:
					{
						Style style;
						if (style.BaseStyle != null)
						{
							num3 = 66;
							continue;
						}
						goto IL_60D;
					}
					case 60:
					{
						sprᲵ sprᲵ4;
						if (sprᲵ4.ᜌ() != null)
						{
							num3 = 61;
							continue;
						}
						goto IL_60D;
					}
					case 61:
					{
						Style style;
						sprᲵ sprᲵ4;
						style.ApplyBaseStyle(sprᲵ4.ᜌ());
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_946;
						default:
							if (false)
							{
							}
							num3 = 32;
							continue;
						}
						break;
					}
					case 62:
					{
						Style style4 = sprᯉ;
						style4.TableStyleData = new byte[sprᲵ.ᜃ().Length];
						Buffer.BlockCopy(sprᲵ.ᜃ(), 0, style4.TableStyleData, 0, sprᲵ.ᜃ().Length);
						num3 = 36;
						continue;
					}
					case 63:
						if (sprᲵ.ᜂ() == 0)
						{
							num3 = 40;
							continue;
						}
						goto IL_1A6;
					case 64:
					{
						Style style;
						if (!string.IsNullOrEmpty(style.Name))
						{
							num3 = 19;
							continue;
						}
						goto IL_8BB;
					}
					case 65:
						goto IL_296;
					case 66:
						num3 = 13;
						continue;
					}
					break;
					IL_191:
					num2++;
					num3 = 4;
					continue;
					IL_1A6:
					text = sprᲵ.ᜌ();
					num3 = 30;
					continue;
					IL_20C:
					num3 = 51;
					continue;
					IL_296:
					num3 = 0;
					continue;
					IL_329:
					num4 = spr_u.ᜁ(a_2).\u170D();
					num3 = 3;
					continue;
					IL_361:
					num3 = 5;
					continue;
					IL_46F:
					spr_u.ᜃ().Add(num2, text);
					num3 = 54;
					continue;
					IL_523:
					spr\u192A.ᜀ(sprᲵ.ᜋ(), paragraphStyle.ParagraphFormat);
					spr\u1AFF.ᜀ(sprᲵ.ᜅ(), paragraphStyle.CharacterFormat);
					base.ᜀ(paragraphStyle.CharacterFormat);
					num3 = 22;
					continue;
					IL_60D:
					num7 = spr_u.ᜁ(a_2).ᜉ();
					num3 = 50;
					continue;
					IL_794:
					spr\u1AFF.ᜀ(sprᲵ.ᜅ(), sprᯉ.CharacterFormat, true);
					base.ᜀ(sprᯉ.CharacterFormat);
					num3 = 1;
					continue;
					IL_899:
					num3 = 29;
					continue;
					IL_8BB:
					num5++;
					num3 = 23;
					continue;
					IL_946:
					num3 = 9;
				}
			}
			IL_382:
			this.\u1712.Styles.FixedIndex13HasStyle = spr_u.ᜅ;
			this.\u1712.Styles.FixedIndex14HasStyle = spr_u.ᜆ;
			this.\u1712.Styles.FixedIndex13StyleName = spr_u.ᜇ;
			this.\u1712.Styles.FixedIndex14StyleName = spr_u.ᜈ;
			return;
		}
		}
	}

	// Token: 0x060037D0 RID: 14288 RVA: 0x00344254 File Offset: 0x00343254
	private void ᜃ(sprᬛ A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				return;
			case 2:
				this.\u1712.Escher = A_0.ᜯ();
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_24;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					num = 1;
					continue;
				}
				break;
			}
			goto IL_1C;
			IL_24:
			num = 2;
			continue;
			IL_1C:
			if (A_0.ᜯ() != null)
			{
				goto IL_24;
			}
			break;
		}
	}

	// Token: 0x060037D1 RID: 14289 RVA: 0x003442D8 File Offset: 0x003432D8
	private new void ᜁ()
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
		this.\u1712.\u171A();
	}

	// Token: 0x060037D2 RID: 14290 RVA: 0x00344320 File Offset: 0x00343320
	private new void ᜁ(sprᬛ A_0, WordSubdocument A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 7;
			for (;;)
			{
				sprỀ.ᜃ ᜃ;
				switch (num)
				{
				case 0:
					goto IL_108;
				case 1:
					goto IL_108;
				case 2:
					switch (A_1)
					{
					case WordSubdocument.Footnote:
						ᜃ = (this.ᜁ = new sprỀ.ᜅ());
						num = 12;
						continue;
					case WordSubdocument.HeaderFooter:
						ᜃ = (this.ᜄ = new sprỀ.ᜆ());
						num = 0;
						continue;
					case WordSubdocument.Endnote:
						ᜃ = (this.ᜂ = new sprỀ.ᜂ());
						num = 1;
						continue;
					case WordSubdocument.Annotation:
						ᜃ = (this.ᜀ = new sprỀ.ᜁ());
						num = 4;
						continue;
					case WordSubdocument.TextBox:
						ᜃ = (this.ᜃ = new sprỀ.ᜄ());
						num = 9;
						continue;
					default:
						num = 3;
						continue;
					}
					break;
				case 3:
					num = 10;
					continue;
				case 4:
					goto IL_108;
				case 5:
					return;
				case 6:
					ᜃ.ᜀ(A_0, this.\u1712);
					num = 5;
					continue;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 8:
					if (ᜃ != null)
					{
						num = 6;
						continue;
					}
					return;
				case 9:
					if (true)
					{
					}
					goto IL_108;
				case 10:
					goto IL_108;
				case 11:
					return;
				case 12:
					goto IL_108;
				}
				if (!this.ᜀ(A_0, A_1))
				{
					num = 11;
					continue;
				}
				ᜃ = null;
				num = 2;
				continue;
				IL_108:
				num = 8;
			}
			return;
		}
		}
	}

	// Token: 0x060037D3 RID: 14291 RVA: 0x003444EC File Offset: 0x003434EC
	private new void ᜀ(sprᬛ A_0, ISection A_1)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_A6;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A8;
				default:
					if (false)
					{
					}
					base.ᜀ(A_0, base.ᜆ().ListFormat);
					base.ᜀ(A_0, base.ᜆ().BreakCharacterFormat);
					base.ᜀ(A_0, base.ᜆ());
					base.ᜀ(base.ᜆ(), A_0);
					num = 0;
					continue;
				}
				break;
			}
			if (true)
			{
			}
			if (A_0.\u1735() == WordChunkType.DocumentEnd)
			{
				break;
			}
			num = 1;
		}
		IL_A6:
		IL_A8:
		spr\u1B67.ᜀ(A_0.\u1715(), A_1 as Section, true);
		this.ᜂ = null;
	}

	// Token: 0x060037D4 RID: 14292 RVA: 0x003445BC File Offset: 0x003435BC
	private new void ᜂ(sprᬛ A_0)
	{
		for (;;)
		{
			this.ᜁ(A_0);
			this.\u1712.ᜋ = A_0.\u1717();
			this.\u1712.IsEncrypted = A_0.\u1712();
			int num = 13;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_45C;
				case 1:
					this.\u1712.MacrosData = A_0.ᜊ().GetBuffer();
					num = 33;
					continue;
				case 2:
					goto IL_237;
				case 3:
					goto IL_348;
				case 4:
					this.\u1712.DigitalSignatures = A_0.ᜡ();
					num = 20;
					continue;
				case 5:
					if (A_0.\u1713().ᜆ() != 100)
					{
						num = 28;
						continue;
					}
					goto IL_4C9;
				case 6:
					goto IL_3EA;
				case 7:
					this.\u1712.ViewSetup.DocumentViewType = (DocumentViewType)A_0.\u1713().ᜋ();
					num = 32;
					continue;
				case 8:
					this.\u1712.ObjectPool = A_0.\u171C().GetBuffer();
					num = 0;
					continue;
				case 9:
					num = 26;
					continue;
				case 10:
					if (A_0.\u1719() != null)
					{
						num = 15;
						continue;
					}
					goto IL_237;
				case 11:
					this.\u1712.AssociatedStrings = A_0.\u171E();
					num = 6;
					continue;
				case 12:
					goto IL_2AE;
				case 13:
					if (A_0.ᜊ() != null)
					{
						num = 1;
						continue;
					}
					goto IL_436;
				case 14:
					goto IL_F2;
				case 15:
					this.\u1712.Variables.ᜀ(A_0.\u1719());
					num = 2;
					continue;
				case 16:
					if (A_0.\u1713().ᜅ() != 0)
					{
						num = 30;
						continue;
					}
					goto IL_25D;
				case 17:
					if (A_0.\u171E() != null)
					{
						num = 11;
						continue;
					}
					goto IL_3EA;
				case 18:
					goto IL_128;
				case 19:
					goto IL_25D;
				case 20:
					goto IL_1BB;
				case 21:
					goto IL_1C0;
				case 22:
					if (A_0.\u171C() != null)
					{
						num = 8;
						continue;
					}
					goto IL_45C;
				case 23:
					this.\u1712.ProtectionType = A_0.\u1713().ᜎ();
					num = 14;
					continue;
				case 24:
					if (A_0.ᜏ() != null)
					{
						num = 27;
						continue;
					}
					goto IL_348;
				case 25:
					this.\u1712.GrammarSpellingData = A_0.\u171F();
					num = 18;
					continue;
				case 26:
					if (A_0.\u1713().ᜎ() != ProtectionType.NoProtection)
					{
						num = 23;
						continue;
					}
					goto IL_F2;
				case 27:
					this.\u1712.MacroCommands = A_0.ᜏ();
					num = 3;
					continue;
				case 28:
					this.\u1712.ViewSetup.ᜀ((int)A_0.\u1713().ᜆ());
					num = 12;
					continue;
				case 29:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1BB;
					default:
						if (false)
						{
						}
						if (A_0.\u171F() != null)
						{
							num = 25;
							continue;
						}
						goto IL_128;
					}
					break;
				case 30:
					this.\u1712.ViewSetup.ZoomType = (ZoomType)A_0.\u1713().ᜅ();
					num = 19;
					continue;
				case 31:
					num = 5;
					continue;
				case 32:
					goto IL_376;
				case 33:
					goto IL_436;
				case 34:
					if (A_0.\u1713() != null)
					{
						num = 9;
						continue;
					}
					goto IL_1C0;
				case 35:
					if (A_0.ᜡ() != null)
					{
						num = 4;
						continue;
					}
					goto IL_318;
				case 36:
					if (A_0.\u1713().ᜆ() != 0)
					{
						num = 31;
						continue;
					}
					goto IL_4C9;
				case 37:
					if (A_0.\u1713().ᜋ() != 1)
					{
						num = 7;
						continue;
					}
					goto IL_376;
				}
				break;
				IL_F2:
				this.\u1712.Sections[0].PageSetup.DifferentOddAndEvenPagesHeaderFooter = A_0.\u1713().\u1719();
				num = 21;
				continue;
				IL_128:
				num = 34;
				continue;
				IL_1C0:
				num = 37;
				continue;
				IL_237:
				num = 24;
				continue;
				IL_25D:
				num = 36;
				continue;
				IL_318:
				num = 10;
				continue;
				IL_1BB:
				goto IL_318;
				IL_348:
				if (true)
				{
				}
				num = 17;
				continue;
				IL_376:
				num = 16;
				continue;
				IL_3EA:
				num = 22;
				continue;
				IL_436:
				num = 35;
				continue;
				IL_45C:
				num = 29;
			}
		}
		IL_2AE:
		IL_4C9:
		this.\u1712.StandardAsciiFont = A_0.ᜉ();
		this.\u1712.StandardFarEastFont = A_0.\u1718();
		this.\u1712.StandardNonFarEastFont = A_0.ᜢ();
		this.\u1712.StandardBidiFont = A_0.\u171A();
		this.\u1712.Properties.ᜀ(A_0.\u173A());
	}

	// Token: 0x060037D5 RID: 14293 RVA: 0x00344AEC File Offset: 0x00343AEC
	private new void ᜁ(sprᬛ A_0)
	{
		int num = 4;
		for (;;)
		{
			DateTime createDate;
			DateTime lastSaveDate;
			DateTime lastPrinted;
			switch (num)
			{
			case 0:
				if (A_0.ᜠ().Title != null)
				{
					num = 2;
					continue;
				}
				goto IL_767;
			case 1:
				this.\u1712.ᜊ.Comments = A_0.ᜠ().Comments.ToString();
				num = 32;
				continue;
			case 2:
				goto IL_2FB;
			case 3:
				this.\u1712.ᜊ.Company = A_0.ᜠ().Company.ToString();
				num = 24;
				continue;
			case 5:
				this.\u1712.ᜊ.LastSaveDate = A_0.ᜠ().LastSaveDate;
				num = 6;
				continue;
			case 6:
				goto IL_662;
			case 7:
				if (A_0.ᜠ().RevisionNumber != null)
				{
					num = 10;
					continue;
				}
				goto IL_483;
			case 8:
				goto IL_1A0;
			case 9:
				goto IL_326;
			case 10:
				this.\u1712.ᜊ.RevisionNumber = A_0.ᜠ().RevisionNumber.ToString();
				num = 25;
				continue;
			case 11:
				if (A_0.ᜠ().Company != null)
				{
					num = 3;
					continue;
				}
				goto IL_405;
			case 12:
				goto IL_145;
			case 13:
				if (A_0.ᜠ().LastAuthor != null)
				{
					num = 35;
					continue;
				}
				goto IL_54A;
			case 14:
				goto IL_4FC;
			case 15:
				goto IL_6E4;
			case 16:
				if (A_0.ᜠ().Keywords != null)
				{
					num = 29;
					continue;
				}
				goto IL_145;
			case 17:
				this.\u1712.ᜊ.Template = A_0.ᜠ().Template.ToString();
				num = 21;
				continue;
			case 18:
				goto IL_73F;
			case 19:
				if (A_0.ᜠ().Manager != null)
				{
					num = 20;
					continue;
				}
				goto IL_F3;
			case 20:
				this.\u1712.ᜊ.Manager = A_0.ᜠ().Manager.ToString();
				num = 44;
				continue;
			case 21:
				goto IL_2D0;
			case 22:
				goto IL_1CB;
			case 23:
				goto IL_54A;
			case 24:
				goto IL_405;
			case 25:
				goto IL_483;
			case 26:
				this.\u1712.ᜊ.LastPrinted = A_0.ᜠ().LastPrinted;
				num = 43;
				continue;
			case 27:
				this.\u1712.ᜊ.ApplicationName = A_0.ᜠ().ApplicationName.ToString();
				num = 15;
				continue;
			case 28:
				this.\u1712.ᜊ.CreateDate = A_0.ᜠ().CreateDate;
				num = 14;
				continue;
			case 29:
				this.\u1712.ᜊ.Keywords = A_0.ᜠ().Keywords.ToString();
				num = 12;
				continue;
			case 30:
				if (A_0.ᜠ().Thumbnail != null)
				{
					num = 42;
					continue;
				}
				goto IL_73F;
			case 31:
				this.\u1712.ᜊ.Subject = A_0.ᜠ().Subject.ToString();
				num = 8;
				continue;
			case 32:
				goto IL_2A5;
			case 33:
				goto IL_386;
			case 34:
				this.\u1712.ᜊ.Author = A_0.ᜠ().Author.ToString();
				num = 22;
				continue;
			case 35:
				this.\u1712.ᜊ.LastAuthor = A_0.ᜠ().LastAuthor.ToString();
				num = 23;
				continue;
			case 36:
				if (A_0.ᜠ().Author != null)
				{
					num = 34;
					continue;
				}
				goto IL_1CB;
			case 37:
				if (A_0.ᜠ().Subject != null)
				{
					num = 31;
					continue;
				}
				goto IL_1A0;
			case 38:
				this.\u1712.ᜊ.Category = A_0.ᜠ().Category.ToString();
				num = 33;
				continue;
			case 39:
				if (createDate.CompareTo(new DateTime(1900, 12, 31)) > 0)
				{
					num = 28;
					continue;
				}
				goto IL_4FC;
			case 40:
				if (A_0.ᜠ().Template != null)
				{
					num = 17;
					continue;
				}
				goto IL_2D0;
			case 41:
				if (lastSaveDate.CompareTo(new DateTime(1900, 12, 31)) > 0)
				{
					num = 5;
					continue;
				}
				goto IL_662;
			case 42:
				if (true)
				{
				}
				this.\u1712.ᜊ.Thumbnail = A_0.ᜠ().Thumbnail;
				num = 18;
				continue;
			case 43:
				goto IL_4AE;
			case 44:
				goto IL_F3;
			case 45:
				if (A_0.ᜠ().Category != null)
				{
					num = 38;
					continue;
				}
				goto IL_386;
			case 46:
				if (lastPrinted.CompareTo(new DateTime(1900, 12, 31)) > 0)
				{
					num = 26;
					continue;
				}
				goto IL_4AE;
			case 47:
				if (A_0.ᜠ().Comments != null)
				{
					num = 1;
					continue;
				}
				goto IL_2A5;
			}
			if (A_0.ᜠ().ApplicationName != null)
			{
				num = 27;
				continue;
			}
			goto IL_6E4;
			IL_F3:
			int paragraphCount = A_0.ᜠ().ParagraphCount;
			this.\u1712.ᜊ.ParagraphCount = A_0.ᜠ().ParagraphCount;
			num = 7;
			continue;
			IL_145:
			num = 13;
			continue;
			IL_1A0:
			num = 40;
			continue;
			IL_1CB:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_2FB:
				this.\u1712.ᜊ.Title = A_0.ᜠ().Title.ToString();
				num = 9;
				continue;
			default:
				if (false)
				{
				}
				num = 45;
				continue;
			}
			IL_2A5:
			num = 11;
			continue;
			IL_2D0:
			num = 30;
			continue;
			IL_386:
			int charCount = A_0.ᜠ().CharCount;
			this.\u1712.ᜊ.CharCount = A_0.ᜠ().CharCount;
			num = 47;
			continue;
			IL_405:
			DateTime createDate2 = A_0.ᜠ().CreateDate;
			createDate = A_0.ᜠ().CreateDate;
			num = 39;
			continue;
			IL_483:
			num = 37;
			continue;
			IL_4AE:
			DateTime lastSaveDate2 = A_0.ᜠ().LastSaveDate;
			lastSaveDate = A_0.ᜠ().LastSaveDate;
			num = 41;
			continue;
			IL_4FC:
			DateTime lastPrinted2 = A_0.ᜠ().LastPrinted;
			lastPrinted = A_0.ᜠ().LastPrinted;
			num = 46;
			continue;
			IL_54A:
			num = 19;
			continue;
			IL_662:
			int docSecurity = A_0.ᜠ().DocSecurity;
			this.\u1712.ᜊ.DocSecurity = A_0.ᜠ().DocSecurity;
			num = 16;
			continue;
			IL_6E4:
			num = 36;
			continue;
			IL_73F:
			num = 0;
		}
		IL_326:
		IL_767:
		TimeSpan totalEditingTime = A_0.ᜠ().TotalEditingTime;
		this.\u1712.ᜊ.TotalEditingTime = A_0.ᜠ().TotalEditingTime;
		int wordCount = A_0.ᜠ().WordCount;
		this.\u1712.ᜊ.WordCount = A_0.ᜠ().WordCount;
		int bytesCount = A_0.ᜠ().BytesCount;
		this.\u1712.ᜊ.BytesCount = A_0.ᜠ().BytesCount;
		int hiddenCount = A_0.ᜠ().HiddenCount;
		this.\u1712.ᜊ.HiddenCount = A_0.ᜠ().HiddenCount;
		int linesCount = A_0.ᜠ().LinesCount;
		this.\u1712.ᜊ.LinesCount = A_0.ᜠ().LinesCount;
		int noteCount = A_0.ᜠ().NoteCount;
		this.\u1712.ᜊ.NoteCount = A_0.ᜠ().NoteCount;
		int pageCount = A_0.ᜠ().PageCount;
		this.\u1712.ᜊ.PageCount = A_0.ᜠ().PageCount;
		int slideCount = A_0.ᜠ().SlideCount;
		this.\u1712.ᜊ.SlideCount = A_0.ᜠ().SlideCount;
	}

	// Token: 0x060037D6 RID: 14294 RVA: 0x00345398 File Offset: 0x00344398
	private new void ᜀ(sprᬛ A_0)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					this.\u1712.DOP = A_0.\u1713();
					if (true)
					{
					}
					num = 0;
					continue;
				}
				break;
			}
			if (A_0.\u1713() == null)
			{
				break;
			}
			num = 1;
		}
	}

	// Token: 0x060037D7 RID: 14295 RVA: 0x0034541C File Offset: 0x0034441C
	private new bool ᜀ(sprᬛ A_0, WordSubdocument A_1)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return A_0.ᜱ().ᜀ(A_1);
	}

	// Token: 0x060037D8 RID: 14296 RVA: 0x00345464 File Offset: 0x00344464
	protected override bool ᜀ(spr\u1F8B A_0, WordChunkType A_1)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return true;
		}
		if (true)
		{
		}
		if (false)
		{
		}
		if (A_1 != WordChunkType.SectionEnd)
		{
			return A_1 == WordChunkType.DocumentEnd;
		}
		return true;
	}

	// Token: 0x060037D9 RID: 14297 RVA: 0x003454AC File Offset: 0x003444AC
	protected override void ᜄ(spr\u1F8B A_0)
	{
		int num = 1;
		for (;;)
		{
			Comment comment;
			switch (num)
			{
			case 0:
				if (comment.Format.TagBkmk == -1)
				{
					num = 2;
					continue;
				}
				goto IL_C8;
			case 2:
				comment.Format.ᜁ();
				num = 6;
				continue;
			case 3:
				if (comment != null)
				{
					num = 7;
					continue;
				}
				return;
			case 4:
				return;
			case 5:
				comment = this.ᜀ.ᜁ();
				num = 3;
				continue;
			case 6:
				goto IL_C8;
			case 7:
				num = 0;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				if (this.ᜀ == null)
				{
					return;
				}
				break;
			}
			num = 5;
			continue;
			IL_C8:
			this.ᜀ(comment);
			base.ᜆ().Items.Add(comment);
			num = 4;
		}
	}

	// Token: 0x060037DA RID: 14298 RVA: 0x003455B4 File Offset: 0x003445B4
	protected override void ᜁ(spr\u1F8B A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				sprᬛ sprᬛ = A_0 as sprᬛ;
				bool flag = true;
				string customMarker = string.Empty;
				int num = 11;
				for (;;)
				{
					Footnote footnote;
					switch (num)
					{
					case 0:
						if (A_0.\u1735() != WordChunkType.Footnote)
						{
							num = 6;
							continue;
						}
						goto IL_152;
					case 1:
						if (!sprᬛ.\u171B())
						{
							num = 19;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_7F;
						default:
							if (false)
							{
							}
							num = 16;
							continue;
						}
						break;
					case 2:
						goto IL_19F;
					case 3:
						if (A_0.\u1735() == WordChunkType.Symbol)
						{
							num = 10;
							continue;
						}
						goto IL_221;
					case 4:
						goto IL_221;
					case 5:
						if (footnote != null)
						{
							num = 15;
							continue;
						}
						goto IL_110;
					case 6:
						footnote.CustomMarker = customMarker;
						footnote.IsAutoNumbered = false;
						num = 18;
						continue;
					case 7:
						goto IL_19F;
					case 8:
						goto IL_110;
					case 9:
						num = 14;
						continue;
					case 10:
					{
						sprᣂ sprᣂ = A_0.ᜭ().\u1737();
						footnote.SymbolCode = sprᣂ.ᜁ();
						footnote.SymbolFontName = A_0.ᜥ().ᜄ()[(int)sprᣂ.ᜀ()];
						num = 4;
						continue;
					}
					case 11:
						goto IL_7F;
					case 12:
						if (true)
						{
						}
						if (sprᬛ != null)
						{
							num = 9;
							continue;
						}
						return;
					case 13:
						footnote = this.ᜂ.ᜀ();
						num = 2;
						continue;
					case 14:
						if (!flag)
						{
							num = 17;
							continue;
						}
						footnote = null;
						customMarker = A_0.\u1736();
						num = 1;
						continue;
					case 15:
						base.ᜆ().Items.Add(footnote);
						num = 0;
						continue;
					case 16:
						footnote = this.ᜁ.ᜀ();
						num = 7;
						continue;
					case 17:
						return;
					case 18:
						goto IL_152;
					case 19:
						if (sprᬛ.ᜈ())
						{
							num = 13;
							continue;
						}
						goto IL_19F;
					}
					break;
					IL_110:
					num = 12;
					continue;
					IL_7F:
					goto IL_110;
					IL_152:
					num = 3;
					continue;
					IL_19F:
					flag = this.ᜀ(ref customMarker, sprᬛ, footnote);
					num = 5;
					continue;
					IL_221:
					base.ᜀ(A_0, footnote.MarkerCharacterFormat);
					base.ᜀ(A_0, base.ᜆ());
					num = 8;
				}
			}
			return;
		}
	}

	// Token: 0x060037DB RID: 14299 RVA: 0x00345868 File Offset: 0x00344868
	private new bool ᜀ(ref string A_0, sprᬛ A_1, Footnote A_2)
	{
		switch (0)
		{
		default:
		{
			string text;
			for (;;)
			{
				text = A_1.\u1736();
				int num = 20;
				for (;;)
				{
					Paragraph paragraph;
					switch (num)
					{
					case 0:
						paragraph = this.ᜁ.ᜀ[this.ᜁ.ᜂ].TextBody.Paragraphs[0];
						num = 6;
						continue;
					case 1:
						if (this.ᜂ.ᜂ >= this.ᜂ.ᜀ.Count)
						{
							goto IL_296;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return false;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num = 15;
							continue;
						}
						break;
					case 2:
						if (paragraph != null)
						{
							num = 22;
							continue;
						}
						goto IL_213;
					case 3:
						if (A_1.\u171B())
						{
							num = 24;
							continue;
						}
						goto IL_375;
					case 4:
						if (paragraph.Text.StartsWith(text))
						{
							num = 16;
							continue;
						}
						goto IL_213;
					case 5:
						if (!A_2.TextBody.Paragraphs[0].Text.StartsWith(text))
						{
							num = 17;
							continue;
						}
						return false;
					case 6:
						goto IL_296;
					case 7:
						if (this.ᜁ.ᜂ < this.ᜁ.ᜀ.Count)
						{
							num = 0;
							continue;
						}
						goto IL_375;
					case 8:
						goto IL_340;
					case 9:
						goto IL_316;
					case 10:
					{
						int num2;
						if (!A_2.TextBody.Paragraphs[0].Text.StartsWith(A_0 + text[num2].ToString()))
						{
							num = 8;
							continue;
						}
						A_0 += text[num2].ToString();
						num = 21;
						continue;
					}
					case 11:
					{
						int num2;
						if (++num2 < text.Length)
						{
							num = 18;
							continue;
						}
						goto IL_340;
					}
					case 12:
						if (A_1.ᜈ())
						{
							num = 14;
							continue;
						}
						goto IL_296;
					case 13:
						return false;
					case 14:
						num = 1;
						continue;
					case 15:
						paragraph = this.ᜂ.ᜀ[this.ᜂ.ᜂ].TextBody.Paragraphs[0];
						num = 23;
						continue;
					case 16:
						goto IL_16F;
					case 17:
					{
						int num2 = 0;
						A_0 = text[0].ToString();
						num = 9;
						continue;
					}
					case 18:
						num = 10;
						continue;
					case 19:
						return false;
					case 20:
						if (A_2 == null)
						{
							num = 13;
							continue;
						}
						num = 5;
						continue;
					case 21:
						goto IL_316;
					case 22:
						num = 4;
						continue;
					case 23:
						goto IL_296;
					case 24:
						num = 7;
						continue;
					}
					break;
					IL_213:
					A_0 = A_1.\u1736();
					num = 19;
					continue;
					IL_296:
					num = 2;
					continue;
					IL_316:
					num = 11;
					continue;
					IL_340:
					text = text.Replace(A_0, string.Empty);
					paragraph = null;
					num = 3;
					continue;
					IL_375:
					num = 12;
				}
			}
			return false;
			IL_16F:
			A_1.ᜀ(text);
			return true;
		}
		}
	}

	// Token: 0x060037DC RID: 14300 RVA: 0x00345C14 File Offset: 0x00344C14
	protected override void ᜂ(spr\u1F8B A_0)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
	}

	// Token: 0x060037DD RID: 14301 RVA: 0x00345C50 File Offset: 0x00344C50
	protected override void ᜀ(spr\u1F8B A_0, sprᨼ A_1)
	{
		for (;;)
		{
			bool a_ = A_0.ᜱ().\u1718().\u171A() > 193;
			TextBox textBox = this.ᜃ.ᜀ(A_1, a_);
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_A1;
				case 1:
					if (textBox != null)
					{
						goto IL_45;
					}
					goto IL_A1;
				case 2:
					textBox.Format.OrderIndex = base.ᜀ(this.\u1712.Escher.ᜈ(), A_1.ᜡ());
					spr\u1AFF.ᜀ(A_0, textBox.CharacterFormat);
					base.ᜆ().Items.Add(textBox);
					num = 0;
					continue;
				}
				break;
				IL_45:
				num = 2;
				continue;
				IL_A1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_45;
				default:
					goto IL_B7;
				}
			}
		}
		IL_B7:
		if (true)
		{
		}
		if (false)
		{
		}
	}

	// Token: 0x060037DE RID: 14302 RVA: 0x00345D2C File Offset: 0x00344D2C
	protected override void ᜀ(sprᩄ A_0, DocPicture A_1)
	{
		int num = 2;
		TextBox textBox;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				goto IL_3A;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3A;
				default:
					if (false)
					{
					}
					if (textBox == null)
					{
						num = 0;
						continue;
					}
					goto IL_91;
				}
				break;
			}
			if (this.ᜃ == null)
			{
				num = 1;
			}
			else
			{
				textBox = this.ᜃ.ᜀ(A_0.ᜃ().ᜡ());
				num = 3;
			}
		}
		IL_3A:
		if (true)
		{
		}
		return;
		IL_91:
		A_1.EmbedBody = textBox.Body;
	}

	// Token: 0x060037DF RID: 14303 RVA: 0x00345DD8 File Offset: 0x00344DD8
	protected override void ᜀ(int A_0, spr\u248F A_1)
	{
		int num = 4;
		for (;;)
		{
			TextBox textBox;
			switch (num)
			{
			case 0:
				goto IL_51;
			case 1:
				A_1.ᜁ(false);
				textBox.CharacterFormat.ApplyBase(A_1.ᜌ());
				A_1.ᜎ().Add(textBox);
				num = 7;
				continue;
			case 2:
				goto IL_49;
			case 3:
				goto IL_49;
			case 5:
				textBox = this.ᜃ.ᜀ(A_1.ᜏ(), false);
				num = 3;
				continue;
			case 6:
				if (A_1.ᜏ().ᜡ() > 0)
				{
					num = 5;
					continue;
				}
				textBox = this.ᜃ.ᜁ(A_0, A_1);
				num = 2;
				continue;
			case 7:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_51;
				default:
					goto IL_C5;
				}
				break;
			case 8:
				return;
			}
			if (this.ᜃ == null)
			{
				num = 8;
				continue;
			}
			textBox = null;
			num = 6;
			continue;
			IL_49:
			num = 0;
			continue;
			IL_51:
			if (textBox == null)
			{
				goto IL_120;
			}
			num = 1;
		}
		return;
		IL_C5:
		if (false)
		{
		}
		IL_120:
		if (true)
		{
		}
	}

	// Token: 0x060037E0 RID: 14304 RVA: 0x00345F10 File Offset: 0x00344F10
	protected override void ᜀ(spr\u1F8B A_0, Paragraph A_1)
	{
		int num = 10;
		Comment comment;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.\u1738() > comment.Format.Position)
				{
					num = 28;
					continue;
				}
				return;
			case 1:
				num = 26;
				continue;
			case 2:
				if (A_1.LastItem is TextRange)
				{
					num = 16;
					continue;
				}
				return;
			case 3:
				num = 32;
				continue;
			case 4:
				num = 0;
				continue;
			case 5:
				goto IL_29D;
			case 6:
				if (A_1 == null)
				{
					num = 17;
					continue;
				}
				num = 13;
				continue;
			case 7:
				return;
			case 8:
				if (A_0.\u1738() <= comment.Format.Position)
				{
					num = 31;
					continue;
				}
				goto IL_1BF;
			case 9:
				goto IL_377;
			case 11:
				if (A_0.ᜮ() < comment.Format.StartTextPos)
				{
					num = 4;
					continue;
				}
				return;
			case 12:
				if (comment.Format.StartTextPos <= A_0.\u1738())
				{
					num = 30;
					continue;
				}
				return;
			case 13:
				if (A_1.Items.Count == 0)
				{
					num = 7;
					continue;
				}
				num = 15;
				continue;
			case 14:
				num = 29;
				continue;
			case 15:
				if (this.ᜀ.ᜂ() != null)
				{
					num = 14;
					continue;
				}
				return;
			case 16:
				num = 12;
				continue;
			case 17:
				goto IL_47F;
			case 18:
				goto IL_31D;
			case 19:
				num = 34;
				continue;
			case 20:
				num = 25;
				continue;
			case 21:
				num = 8;
				continue;
			case 22:
				if (A_0.ᜮ() < comment.Format.StartTextPos)
				{
					num = 3;
					continue;
				}
				goto IL_15D;
			case 23:
				if (A_0.ᜮ() > comment.Format.StartTextPos)
				{
					num = 19;
					continue;
				}
				goto IL_215;
			case 24:
				num = 6;
				continue;
			case 25:
				if (this.ᜀ != null)
				{
					num = 24;
					continue;
				}
				return;
			case 26:
				if (A_0.ᜮ() < comment.Format.Position)
				{
					num = 9;
					continue;
				}
				goto IL_215;
			case 27:
				goto IL_3E8;
			case 28:
				this.ᜀ(A_1, A_0.ᜮ(), comment.Format.StartTextPos);
				this.ᜀ(A_1, A_0.ᜮ(), comment.Format.Position);
				comment.Items.Add(A_1.LastItem.PreviousSibling as ParagraphBase);
				num = 5;
				continue;
			case 29:
				if (this.ᜀ.ᜂ().Count == 0)
				{
					num = 18;
					continue;
				}
				comment = this.ᜀ.ᜀ();
				num = 36;
				continue;
			case 30:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3E8;
				default:
					if (false)
					{
					}
					num = 22;
					continue;
				}
				break;
			case 31:
				goto IL_3E3;
			case 32:
				if (A_0.\u1738() <= comment.Format.Position)
				{
					num = 33;
					continue;
				}
				goto IL_15D;
			case 33:
				goto IL_3A8;
			case 34:
				if (A_0.\u1738() > comment.Format.Position)
				{
					num = 1;
					continue;
				}
				goto IL_215;
			case 35:
				if (A_0.ᜮ() >= comment.Format.StartTextPos)
				{
					num = 21;
					continue;
				}
				goto IL_1BF;
			case 36:
				if (comment != null)
				{
					num = 27;
					continue;
				}
				return;
			}
			if (A_0 is sprᬛ)
			{
				num = 20;
				continue;
			}
			break;
			IL_15D:
			num = 23;
			continue;
			IL_1BF:
			num = 2;
			continue;
			IL_215:
			num = 11;
			continue;
			IL_3E8:
			num = 35;
		}
		return;
		IL_29D:
		return;
		IL_31D:
		return;
		IL_377:
		if (true)
		{
		}
		this.ᜀ(A_1, A_0.ᜮ(), comment.Format.Position);
		comment.Items.Add(A_1.LastItem.PreviousSibling as ParagraphBase);
		return;
		IL_3A8:
		this.ᜀ(A_1, A_0.ᜮ(), comment.Format.StartTextPos);
		comment.Items.Add(A_1.LastItem);
		return;
		IL_3E3:
		comment.Items.Add(A_1.LastItem);
		return;
		IL_47F:;
	}

	// Token: 0x060037E1 RID: 14305 RVA: 0x003463E8 File Offset: 0x003453E8
	private new void ᜀ(Paragraph A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					for (;;)
					{
						TextRange textRange = A_0.LastItem as TextRange;
						string text = textRange.Text;
						int num2 = A_2 - A_1;
						textRange.Text = text.Substring(0, num2);
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_C5;
						}
					}
					IL_C5:
					if (false)
					{
					}
					num = 2;
					continue;
				case 2:
				{
					string text;
					int num2;
					if (num2 > text.Length)
					{
						num = 4;
						continue;
					}
					string text2 = text.Substring(num2, text.Length - num2);
					ITextRange textRange2 = A_0.AppendText(text2);
					TextRange textRange;
					textRange2.ApplyCharacterFormat(textRange.CharacterFormat);
					num = 3;
					continue;
				}
				case 3:
					goto IL_7F;
				case 4:
					return;
				}
				if (A_2 <= A_1)
				{
					break;
				}
				num = 0;
			}
			IL_7F:
			return;
		}
		}
	}

	// Token: 0x060037E2 RID: 14306 RVA: 0x003464E8 File Offset: 0x003454E8
	private new void ᜀ(Comment A_0)
	{
		switch (0)
		{
		default:
		{
			ParagraphBase paragraphBase;
			ParagraphBase paragraphBase2;
			CommentMark entity2;
			for (;;)
			{
				int count = A_0.Items.Count;
				int num = 4;
				for (;;)
				{
					CommentMark entity;
					switch (num)
					{
					case 0:
						paragraphBase.OwnerParagraph.Items.Insert(0, entity);
						num = 1;
						continue;
					case 1:
						goto IL_67;
					case 2:
						goto IL_67;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_EB;
						default:
							goto IL_D1;
						}
						break;
					case 4:
						if (A_0.Items.Count == 0)
						{
							num = 6;
							continue;
						}
						goto IL_EB;
					case 5:
						if (paragraphBase2.NextSibling == null)
						{
							num = 3;
							continue;
						}
						goto IL_180;
					case 6:
						return;
					case 7:
					{
						if (paragraphBase.PreviousSibling == null)
						{
							if (true)
							{
							}
							num = 0;
							continue;
						}
						int index = paragraphBase.ឯ();
						paragraphBase.OwnerParagraph.Items.Insert(index, entity);
						num = 2;
						continue;
					}
					}
					break;
					IL_67:
					paragraphBase2 = A_0.Items[count - 1];
					num = 5;
					continue;
					IL_EB:
					entity = new CommentMark(this.\u1712, A_0.Format.TagBkmk);
					entity2 = new CommentMark(this.\u1712, A_0.Format.TagBkmk, CommentMarkType.CommentEnd);
					paragraphBase = A_0.Items[0];
					num = 7;
				}
			}
			return;
			IL_D1:
			if (false)
			{
			}
			paragraphBase2.OwnerParagraph.Items.Add(entity2);
			return;
			IL_180:
			int num2 = paragraphBase2.ឯ();
			paragraphBase.OwnerParagraph.Items.Insert(num2 + 1, entity2);
			return;
		}
		}
	}

	// Token: 0x060037E3 RID: 14307 RVA: 0x003466A0 File Offset: 0x003456A0
	private new void ᜀ()
	{
		if (true)
		{
		}
		switch (0)
		{
		default:
		{
			IEnumerator enumerator = this.\u1712.ListStyles.GetEnumerator();
			try
			{
				int num = 12;
				for (;;)
				{
					int num2;
					int num3;
					switch (num)
					{
					case 0:
					{
						int count;
						if (num2 < count)
						{
							ListStyle listStyle;
							ListLevel listLevel = listStyle.Levels[num2];
							num3 = listLevel.PicIndex;
							num = 6;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1F0;
						default:
							if (false)
							{
							}
							num = 14;
							continue;
						}
						break;
					}
					case 1:
					{
						DocPicture a_ = base.ᜅ()[num3];
						ListLevel listLevel;
						listLevel.PicBullet = a_;
						num = 4;
						continue;
					}
					case 2:
						num = 7;
						continue;
					case 3:
						num = 10;
						continue;
					case 4:
						goto IL_122;
					case 5:
						goto IL_122;
					case 6:
						if (num3 >= 0)
						{
							num = 2;
							continue;
						}
						goto IL_122;
					case 7:
						if (num3 != 2147483647)
						{
							num = 15;
							continue;
						}
						goto IL_122;
					case 8:
						goto IL_1F0;
					case 9:
						goto IL_BC;
					case 10:
						goto IL_21E;
					case 11:
					{
						if (!enumerator.MoveNext())
						{
							num = 3;
							continue;
						}
						ListStyle listStyle = (ListStyle)enumerator.Current;
						num2 = 0;
						int count = listStyle.Levels.Count;
						num = 9;
						continue;
					}
					case 13:
						goto IL_BC;
					case 15:
						num = 8;
						continue;
					}
					goto IL_86;
					IL_BC:
					num = 0;
					continue;
					IL_122:
					num2++;
					num = 13;
					continue;
					IL_134:
					num = 11;
					continue;
					IL_86:
					goto IL_134;
					IL_1F0:
					if (num3 <= base.ᜅ().Count - 1)
					{
						num = 1;
					}
					else
					{
						DocPicture docPicture = new DocPicture(this.\u1712);
						Bitmap image = new Bitmap(3, 3);
						docPicture.LoadImage(image);
						ListLevel listLevel;
						listLevel.PicBullet = docPicture;
						listLevel.IsEmptyPicture = true;
						num = 5;
					}
				}
				IL_21E:;
			}
			finally
			{
				for (;;)
				{
					IDisposable disposable = enumerator as IDisposable;
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							disposable.Dispose();
							num = 2;
							continue;
						case 1:
							if (disposable != null)
							{
								num = 0;
								continue;
							}
							goto IL_268;
						case 2:
							goto IL_266;
						}
						break;
					}
				}
				IL_266:
				IL_268:;
			}
			return;
		}
		}
	}

	// Token: 0x060037E4 RID: 14308 RVA: 0x00346934 File Offset: 0x00345934
	private new void ᜀ(Section A_0)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				if (true)
				{
				}
				num = 5;
				continue;
			case 3:
				if (!A_0.HeadersFooters.FirstPageHeader.WriteWatermark)
				{
					num = 4;
					continue;
				}
				return;
			case 4:
				goto IL_A9;
			case 5:
				if (!A_0.HeadersFooters.OddHeader.WriteWatermark)
				{
					num = 6;
					continue;
				}
				return;
			case 6:
				num = 3;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_A9:
				A_0.Document.ᜀ(WatermarkType.NoWatermark);
				num = 0;
				break;
			default:
				if (false)
				{
				}
				if (A_0.Document.Watermark.Type == WatermarkType.NoWatermark)
				{
					return;
				}
				num = 1;
				break;
			}
		}
	}

	// Token: 0x04002A15 RID: 10773
	private new sprỀ.ᜁ ᜀ;

	// Token: 0x04002A16 RID: 10774
	private new sprỀ.ᜅ ᜁ;

	// Token: 0x04002A17 RID: 10775
	private new sprỀ.ᜂ ᜂ;

	// Token: 0x04002A18 RID: 10776
	private new sprỀ.ᜄ ᜃ;

	// Token: 0x04002A19 RID: 10777
	private new sprỀ.ᜆ ᜄ;

	// Token: 0x020003E1 RID: 993
	internal new abstract class ᜃ : spr\u19CB
	{
		// Token: 0x060037E6 RID: 14310 RVA: 0x00346A34 File Offset: 0x00345A34
		internal new void ᜀ(sprᬛ A_0, Document A_1)
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
			base.ᜀ(A_1);
			this.ᜀ(A_0);
			A_0.ᜎ();
		}

		// Token: 0x060037E7 RID: 14311
		internal new abstract void ᜀ(sprᬛ A_0);
	}

	// Token: 0x020003E2 RID: 994
	internal class ᜆ : sprỀ.ᜃ
	{
		// Token: 0x060037E9 RID: 14313 RVA: 0x00346A98 File Offset: 0x00345A98
		internal new sprỀ.ᜀ ᜀ()
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
			return this.ᜂ;
		}

		// Token: 0x060037EA RID: 14314 RVA: 0x00346ADC File Offset: 0x00345ADC
		internal override void ᜀ(sprᬛ A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					IL_43:
					spr\u1DAC spr_u1DAC = A_0.ᜀ(WordSubdocument.HeaderFooter) as spr\u1DAC;
					spr_u1DAC.ᜀ(A_0.ᝂ());
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_81:
						if (!A_0.ᜱ().ᜀ(WordSubdocument.HeaderTextBox))
						{
							goto IL_106;
						}
						num = 0;
						break;
					default:
						if (false)
						{
						}
						num = 6;
						break;
					}
					int num2;
					int count;
					for (;;)
					{
						IL_10:
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							this.ᜀ(A_0, WordSubdocument.HeaderTextBox);
							num = 2;
							continue;
						case 1:
							if (num2 >= count)
							{
								num = 7;
								continue;
							}
							spr_u1DAC.ᜁ(num2 + 1);
							num = 4;
							continue;
						case 2:
							goto IL_164;
						case 3:
						{
							if (this.ᜁ)
							{
								num = 5;
								continue;
							}
							Body a_ = this.\u1712.Sections[num2].HeadersFooters[this.ᜀ];
							base.ᜀ(spr_u1DAC, a_);
							num = 10;
							continue;
						}
						case 4:
							goto IL_A8;
						case 5:
						{
							this.ᜁ = false;
							this.ᜀ = 0;
							ISection a_2 = this.\u1712.Sections[num2];
							this.ᜀ(a_2);
							num2++;
							num = 9;
							continue;
						}
						case 6:
							goto IL_81;
						case 7:
							return;
						case 8:
							goto IL_131;
						case 9:
							goto IL_131;
						case 10:
							goto IL_A8;
						}
						goto IL_43;
						IL_A8:
						num = 3;
						continue;
						IL_131:
						num = 1;
					}
					IL_164:
					IL_106:
					count = this.\u1712.Sections.Count;
					this.ᜋ = false;
					num2 = 0;
					num = 8;
					goto IL_10;
				}
				return;
			}
		}

		// Token: 0x060037EB RID: 14315 RVA: 0x00346CB0 File Offset: 0x00345CB0
		protected override bool ᜀ(spr\u1F8B A_0, WordChunkType A_1)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_1 == WordChunkType.DocumentEnd)
					{
						return true;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_45;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 1:
					goto IL_A4;
				case 2:
					this.ᜁ = (A_1 == WordChunkType.DocumentEnd);
					num = 0;
					continue;
				}
				goto IL_20;
				IL_45:
				num = 2;
				continue;
				IL_20:
				this.ᜀ += ((A_1 == WordChunkType.EndOfSubdocText && this.ᜀ < 5) ? 1 : 0);
				goto IL_45;
			}
			IL_A4:
			return A_1 == WordChunkType.EndOfSubdocText;
		}

		// Token: 0x060037EC RID: 14316 RVA: 0x00346D6C File Offset: 0x00345D6C
		protected override void ᜀ(spr\u1F8B A_0, sprᨼ A_1)
		{
			for (;;)
			{
				bool a_ = A_0.ᜱ().\u1718().\u171A() > 193;
				TextBox textBox = this.ᜂ.ᜀ(A_1, a_);
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_42;
						default:
							if (false)
							{
							}
							textBox.Format.OrderIndex = base.ᜀ(this.\u1712.Escher.ᜈ(), A_1.ᜡ());
							spr\u1AFF.ᜀ(A_0, textBox.CharacterFormat);
							base.ᜆ().Items.Add(textBox);
							if (true)
							{
							}
							num = 0;
							continue;
						}
						break;
					case 2:
						goto IL_42;
					}
					break;
					IL_42:
					if (textBox == null)
					{
						return;
					}
					num = 1;
				}
			}
		}

		// Token: 0x060037ED RID: 14317 RVA: 0x00346E4C File Offset: 0x00345E4C
		protected override bool ᜃ(spr\u1F8B A_0)
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
			return A_0.ᜂ(this.\u1712);
		}

		// Token: 0x060037EE RID: 14318 RVA: 0x00346E94 File Offset: 0x00345E94
		protected override void ᜀ(int A_0, spr\u248F A_1)
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_34;
				case 1:
					goto IL_5A;
				case 2:
				{
					A_1.ᜁ(true);
					TextBox textBox;
					A_1.ᜎ().Add(textBox);
					num = 1;
					continue;
				}
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
					{
						if (false)
						{
						}
						TextBox textBox;
						if (textBox != null)
						{
							num = 2;
							continue;
						}
						return;
					}
					}
					break;
				}
				if (this.ᜂ == null)
				{
					num = 0;
				}
				else
				{
					TextBox textBox = this.ᜂ.ᜀ(A_0);
					num = 3;
				}
			}
			IL_34:
			if (true)
			{
			}
			return;
			IL_5A:;
		}

		// Token: 0x060037EF RID: 14319 RVA: 0x00346F4C File Offset: 0x00345F4C
		protected override void ᜀ(sprᩄ A_0, DocPicture A_1)
		{
			int num = 0;
			TextBox textBox;
			for (;;)
			{
				switch (num)
				{
				case 1:
					return;
				case 2:
					goto IL_8E;
				case 3:
					if (textBox != null)
					{
						goto IL_91;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				}
				if (this.ᜂ == null)
				{
					if (true)
					{
					}
					num = 1;
				}
				else
				{
					textBox = this.ᜂ.ᜀ(A_0.ᜃ().ᜡ());
					num = 3;
				}
			}
			return;
			IL_8E:
			return;
			IL_91:
			A_1.EmbedBody = textBox.Body;
		}

		// Token: 0x060037F0 RID: 14320 RVA: 0x00346FF8 File Offset: 0x00345FF8
		private new void ᜀ(sprᬛ A_0, WordSubdocument A_1)
		{
			for (;;)
			{
				sprỀ.ᜃ ᜃ = null;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (A_1 == WordSubdocument.HeaderTextBox)
						{
							num = 5;
							continue;
						}
						goto IL_7F;
					case 1:
						if (ᜃ != null)
						{
							num = 2;
							continue;
						}
						return;
					case 2:
						ᜃ.ᜀ(A_0, this.\u1712);
						num = 3;
						continue;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_7F;
						default:
							goto IL_65;
						}
						break;
					case 4:
						goto IL_7F;
					case 5:
						ᜃ = (this.ᜂ = new sprỀ.ᜀ());
						num = 4;
						continue;
					}
					break;
					IL_7F:
					num = 1;
				}
			}
			IL_65:
			if (false)
			{
			}
			if (true)
			{
			}
		}

		// Token: 0x060037F1 RID: 14321 RVA: 0x003470BC File Offset: 0x003460BC
		private new void ᜀ(ISection A_0)
		{
			for (;;)
			{
				int num = 0;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_B1:
					num2 = 5;
					break;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					num2 = 0;
					break;
				}
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_BE;
					case 1:
					{
						BodyRegionCollection bodyRegionCollection;
						IParagraph paragraph;
						bodyRegionCollection.Remove(paragraph);
						num2 = 7;
						continue;
					}
					case 2:
					{
						IParagraph paragraph;
						if (paragraph != null)
						{
							goto IL_B1;
						}
						goto IL_5C;
					}
					case 3:
					{
						if (num >= 6)
						{
							num2 = 8;
							continue;
						}
						BodyRegionCollection bodyRegionCollection = A_0.HeadersFooters[num].Items;
						IParagraph paragraph = bodyRegionCollection.LastItem as IParagraph;
						num2 = 2;
						continue;
					}
					case 4:
						goto IL_BE;
					case 5:
						num2 = 6;
						continue;
					case 6:
					{
						IParagraph paragraph;
						if (paragraph.Items.Count == 0)
						{
							num2 = 1;
							continue;
						}
						goto IL_5C;
					}
					case 7:
						goto IL_5C;
					case 8:
						return;
					}
					break;
					IL_5C:
					num++;
					num2 = 4;
					continue;
					IL_BE:
					num2 = 3;
				}
			}
		}

		// Token: 0x04002A1A RID: 10778
		private new int ᜀ;

		// Token: 0x04002A1B RID: 10779
		private new bool ᜁ;

		// Token: 0x04002A1C RID: 10780
		private new sprỀ.ᜀ ᜂ;
	}

	// Token: 0x020003E3 RID: 995
	internal new class ᜁ : sprỀ.ᜃ
	{
		// Token: 0x060037F3 RID: 14323 RVA: 0x003471E4 File Offset: 0x003461E4
		internal new Comment ᜀ()
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				if (this.ᜂ >= this.ᜀ.Count)
				{
					return null;
				}
				break;
			}
			return this.ᜀ[this.ᜂ];
		}

		// Token: 0x060037F4 RID: 14324 RVA: 0x00347248 File Offset: 0x00346248
		internal new List<Comment> ᜂ()
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

		// Token: 0x060037F5 RID: 14325 RVA: 0x0034728C File Offset: 0x0034628C
		internal override void ᜀ(sprᬛ A_0)
		{
			for (;;)
			{
				sprᜩ sprᜩ = A_0.ᜀ(WordSubdocument.Annotation) as sprᜩ;
				sprᜩ.ᜀ(A_0.ᝂ());
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_37;
					case 1:
						goto IL_A8;
					case 2:
						if (sprᜩ.\u1735() == WordChunkType.DocumentEnd)
						{
							if (true)
							{
							}
							num = 1;
							continue;
						}
						goto IL_37;
					}
					break;
					IL_37:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						this.ᜀ(sprᜩ);
						base.ᜀ(sprᜩ, this.ᜁ.Body);
						this.ᜂ++;
						num = 2;
						break;
					}
				}
			}
			IL_A8:
			this.ᜂ = 0;
		}

		// Token: 0x060037F6 RID: 14326 RVA: 0x0034734C File Offset: 0x0034634C
		internal new Comment ᜁ()
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
				if (this.ᜂ >= this.ᜀ.Count)
				{
					return null;
				}
				break;
			}
			if (true)
			{
			}
			return this.ᜀ[this.ᜂ++];
		}

		// Token: 0x060037F7 RID: 14327 RVA: 0x003473BC File Offset: 0x003463BC
		private new void ᜀ(sprᜩ A_0)
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
			this.ᜁ = new Comment(this.\u1712);
			this.ᜀ(A_0, this.ᜁ.Format);
			this.ᜀ.Add(this.ᜁ);
		}

		// Token: 0x060037F8 RID: 14328 RVA: 0x0034742C File Offset: 0x0034642C
		protected override bool ᜀ(spr\u1F8B A_0, WordChunkType A_1)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				if ((A_0 as sprᜩ).\u1713() != this.ᜂ)
				{
					return true;
				}
				break;
			}
			return A_1 == WordChunkType.DocumentEnd;
		}

		// Token: 0x060037F9 RID: 14329 RVA: 0x00347484 File Offset: 0x00346484
		private new void ᜀ(sprᜩ A_0, CommentFormat A_1)
		{
			for (;;)
			{
				sprᝦ sprᝦ = A_0.ᜂ();
				if (true)
				{
				}
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						if (sprᝦ == null)
						{
							return;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 2:
						A_1.Initial = sprᝦ.ᜂ();
						A_1.Author = A_0.ᜈ();
						A_1.BookmarkStartOffset = A_0.ᜃ();
						A_1.BookmarkEndOffset = A_0.ᜅ();
						A_1.Position = A_0.ᜏ();
						A_1.TagBkmk = sprᝦ.ᜁ();
						num = 0;
						continue;
					}
					break;
				}
			}
		}

		// Token: 0x04002A1D RID: 10781
		private new List<Comment> ᜀ = new List<Comment>();

		// Token: 0x04002A1E RID: 10782
		private new Comment ᜁ;

		// Token: 0x04002A1F RID: 10783
		private new int ᜂ;
	}

	// Token: 0x020003E4 RID: 996
	internal class ᜅ : sprỀ.ᜃ
	{
		// Token: 0x060037FB RID: 14331 RVA: 0x00347568 File Offset: 0x00346568
		internal override void ᜀ(sprᬛ A_0)
		{
			for (;;)
			{
				sprᳱ sprᳱ = this.ᜁ(A_0);
				sprᳱ.ᜀ(A_0.ᝂ());
				int num = 0;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (true)
						{
						}
						goto IL_40;
					case 1:
						goto IL_63;
					case 2:
						goto IL_40;
					case 3:
						if (num >= this.ᜃ)
						{
							num2 = 1;
							continue;
						}
						this.ᜀ(sprᳱ);
						sprᳱ.ᜀ(this.ᜂ);
						base.ᜀ(sprᳱ, this.ᜁ.TextBody);
						this.ᜂ++;
						num++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num2 = 2;
							continue;
						}
						break;
					}
					break;
					IL_40:
					num2 = 3;
				}
			}
			IL_63:
			this.ᜂ = 0;
		}

		// Token: 0x060037FC RID: 14332 RVA: 0x00347644 File Offset: 0x00346644
		internal new Footnote ᜀ()
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				if (this.ᜂ >= this.ᜀ.Count)
				{
					return null;
				}
				break;
			}
			return this.ᜀ[this.ᜂ++];
		}

		// Token: 0x060037FD RID: 14333 RVA: 0x003476B4 File Offset: 0x003466B4
		protected new virtual sprᳱ ᜁ(sprᬛ A_0)
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
			this.ᜃ = A_0.ᜱ().ᜡ().ᜇ() - 1;
			return A_0.ᜀ(WordSubdocument.Footnote) as sprᤜ;
		}

		// Token: 0x060037FE RID: 14334 RVA: 0x00347714 File Offset: 0x00346714
		protected new virtual void ᜀ(sprῳ A_0)
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
			this.ᜁ = new Footnote(this.\u1712);
			this.ᜁ.FootnoteType = FootnoteType.Footnote;
			this.ᜀ.Add(this.ᜁ);
		}

		// Token: 0x060037FF RID: 14335 RVA: 0x00347780 File Offset: 0x00346780
		protected override bool ᜀ(spr\u1F8B A_0, WordChunkType A_1)
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
				if ((A_0 as sprᤜ).\u1713() != this.ᜂ)
				{
					return true;
				}
				if (true)
				{
				}
				break;
			}
			return A_1 == WordChunkType.DocumentEnd;
		}

		// Token: 0x06003800 RID: 14336 RVA: 0x003477D8 File Offset: 0x003467D8
		private new void ᜁ(sprῳ A_0)
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
		}

		// Token: 0x04002A20 RID: 10784
		internal new List<Footnote> ᜀ = new List<Footnote>();

		// Token: 0x04002A21 RID: 10785
		protected new Footnote ᜁ;

		// Token: 0x04002A22 RID: 10786
		internal new int ᜂ;

		// Token: 0x04002A23 RID: 10787
		protected new int ᜃ;
	}

	// Token: 0x020003E5 RID: 997
	internal new class ᜂ : sprỀ.ᜅ
	{
		// Token: 0x06003802 RID: 14338 RVA: 0x00347834 File Offset: 0x00346834
		protected override sprᳱ ᜁ(sprᬛ A_0)
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
			this.ᜃ = A_0.ᜱ().\u1719().ᜇ() - 1;
			return A_0.ᜀ(WordSubdocument.Endnote) as spr\u1F4F;
		}

		// Token: 0x06003803 RID: 14339 RVA: 0x00347894 File Offset: 0x00346894
		protected override void ᜀ(sprῳ A_0)
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
			this.ᜁ = new Footnote(this.\u1712);
			this.ᜁ.FootnoteType = FootnoteType.Endnote;
			this.ᜀ.Add(this.ᜁ);
		}
	}

	// Token: 0x020003E6 RID: 998
	internal new class ᜄ : sprỀ.ᜃ
	{
		// Token: 0x06003805 RID: 14341 RVA: 0x00347914 File Offset: 0x00346914
		internal override void ᜀ(sprᬛ A_0)
		{
			for (;;)
			{
				sprᳱ sprᳱ = this.ᜁ(A_0);
				sprᳱ.ᜀ(A_0.ᝂ());
				this.ᜋ = false;
				int num = 0;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						sprᳱ.ᜀ(this.ᜃ);
						base.ᜀ(sprᳱ, this.ᜀ.Body);
						this.ᜇ.Clear();
						num2 = 1;
						continue;
					case 1:
						goto IL_4B;
					case 2:
						IL_49:
						goto IL_C6;
					case 3:
						if (num >= this.ᜁ - 1)
						{
							num2 = 6;
							continue;
						}
						num2 = 5;
						continue;
					case 4:
						goto IL_C6;
					case 5:
						if (this.ᜀ(A_0))
						{
							num2 = 0;
							continue;
						}
						goto IL_4B;
					case 6:
						return;
					}
					break;
					IL_4B:
					this.ᜃ++;
					num++;
					if (true)
					{
					}
					num2 = 4;
					continue;
					IL_C6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_49;
					default:
						if (false)
						{
						}
						num2 = 3;
						break;
					}
				}
			}
		}

		// Token: 0x06003806 RID: 14342 RVA: 0x00347A30 File Offset: 0x00346A30
		protected override bool ᜀ(spr\u1F8B A_0, WordChunkType A_1)
		{
			if (A_1 != WordChunkType.DocumentEnd)
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
				return this.ᜃ != (A_0 as sprᲅ).\u1713();
			}
			return true;
		}

		// Token: 0x06003807 RID: 14343 RVA: 0x00347A8C File Offset: 0x00346A8C
		protected new virtual sprᳱ ᜁ(sprᬛ A_0)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_52;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_52;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 2:
					goto IL_7F;
				}
				if (A_0.ᜱ().ᜐ().ᜁ() != null)
				{
					num = 0;
					continue;
				}
				break;
				IL_52:
				this.ᜁ = A_0.ᜱ().ᜐ().ᜁ().Count;
				if (true)
				{
				}
				num = 2;
			}
			IL_7F:
			this.ᜂ = WordSubdocument.TextBox;
			return A_0.ᜀ(WordSubdocument.TextBox) as sprᲅ;
		}

		// Token: 0x06003808 RID: 14344 RVA: 0x00347B38 File Offset: 0x00346B38
		private new bool ᜀ(spr\u1F8B A_0)
		{
			bool result;
			for (;;)
			{
				IL_30:
				result = false;
				int num = A_0.ᜱ().ᜐ().ᜁ(this.ᜂ, this.ᜃ);
				int num2 = 2;
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
						switch (num2)
						{
						case 0:
							this.ᜀ = new TextBox(this.\u1712);
							this.ᜄ.AddTextBox(num, this.ᜀ);
							result = true;
							if (true)
							{
							}
							num2 = 1;
							continue;
						case 1:
							return result;
						case 2:
							goto IL_57;
						}
						goto IL_30;
					}
					IL_57:
					if (num == 0)
					{
						return result;
					}
					num2 = 0;
				}
			}
			return result;
		}

		// Token: 0x06003809 RID: 14345 RVA: 0x00347BF0 File Offset: 0x00346BF0
		internal new TextBox ᜀ(sprᨼ A_0, bool A_1)
		{
			TextBox textBox;
			spr\u2459 a_;
			for (;;)
			{
				textBox = this.ᜄ.GetTextBox(A_0.ᜡ());
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (textBox == null)
						{
							num = 1;
							continue;
						}
						a_ = null;
						goto IL_73;
					case 1:
						goto IL_41;
					case 2:
						a_ = (this.\u1712.Escher.ᜈ()[A_0.ᜡ()] as spr\u2459);
						num = 3;
						continue;
					case 3:
						goto IL_6F;
					case 4:
						if (!this.\u1712.Escher.ᜈ().ContainsKey(A_0.ᜡ()))
						{
							goto IL_D8;
						}
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_73;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					}
					break;
					IL_73:
					num = 4;
				}
			}
			IL_41:
			return null;
			IL_6F:
			IL_D8:
			spr᱙.ᜀ(a_, A_0, textBox.Format, A_1);
			return textBox;
		}

		// Token: 0x0600380A RID: 14346 RVA: 0x00347CE4 File Offset: 0x00346CE4
		internal new TextBox ᜀ(int A_0)
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
			return this.ᜄ.GetTextBox(A_0);
		}

		// Token: 0x0600380B RID: 14347 RVA: 0x00347D2C File Offset: 0x00346D2C
		internal new TextBox ᜁ(int A_0, spr\u248F A_1)
		{
			TextBox textBox;
			for (;;)
			{
				textBox = this.ᜄ.GetTextBox(A_0);
				int num = 3;
				for (;;)
				{
					spr\u2459 a_;
					switch (num)
					{
					case 0:
						return textBox;
					case 1:
						a_ = (this.\u1712.Escher.ᜈ()[A_0] as spr\u2459);
						num = 2;
						continue;
					case 2:
						goto IL_4D;
					case 3:
						goto IL_35;
					case 4:
						a_ = null;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_35;
						default:
							if (false)
							{
							}
							num = 5;
							continue;
						}
						break;
					case 5:
						if (this.\u1712.Escher.ᜈ().ContainsKey(A_0))
						{
							num = 1;
							continue;
						}
						goto IL_4D;
					}
					break;
					IL_35:
					if (textBox != null)
					{
						if (true)
						{
						}
						num = 4;
						continue;
					}
					return textBox;
					IL_4D:
					spr᱙.ᜀ(a_, A_1.ᜏ(), textBox.Format, true);
					num = 0;
				}
			}
			return textBox;
		}

		// Token: 0x04002A24 RID: 10788
		protected new TextBox ᜀ;

		// Token: 0x04002A25 RID: 10789
		protected new int ᜁ;

		// Token: 0x04002A26 RID: 10790
		protected new WordSubdocument ᜂ;

		// Token: 0x04002A27 RID: 10791
		protected new int ᜃ;

		// Token: 0x04002A28 RID: 10792
		protected new ShapeObjectTextCollection ᜄ = new ShapeObjectTextCollection();
	}

	// Token: 0x020003E7 RID: 999
	internal new class ᜀ : sprỀ.ᜄ
	{
		// Token: 0x0600380D RID: 14349 RVA: 0x00347E50 File Offset: 0x00346E50
		protected override sprᳱ ᜁ(sprᬛ A_0)
		{
			for (;;)
			{
				IL_00:
				int num = 1;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						switch (num)
						{
						case 0:
							this.ᜁ = A_0.ᜱ().ᜐ().ᜂ().Count;
							num = 2;
							continue;
						case 2:
							goto IL_89;
						}
						if (A_0.ᜱ().ᜐ().ᜂ() == null)
						{
							goto IL_8B;
						}
						num = 0;
						break;
					}
				}
			}
			IL_89:
			IL_8B:
			this.ᜂ = WordSubdocument.HeaderTextBox;
			return A_0.ᜀ(WordSubdocument.HeaderTextBox) as spr\u226D;
		}

		// Token: 0x0600380E RID: 14350 RVA: 0x00347EFC File Offset: 0x00346EFC
		protected override bool ᜀ(spr\u1F8B A_0, WordChunkType A_1)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				if (A_1 == WordChunkType.DocumentEnd)
				{
					return true;
				}
				break;
			}
			return this.ᜃ != (A_0 as spr\u226D).\u1713();
		}
	}
}
