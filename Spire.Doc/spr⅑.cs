using System;
using System.Collections.Generic;
using Spire.CompoundFile.Doc;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields.Shape;

// Token: 0x02000381 RID: 897
internal class spr\u2151
{
	// Token: 0x0600322F RID: 12847 RVA: 0x002E3DDC File Offset: 0x002E2DDC
	private spr\u2151()
	{
	}

	// Token: 0x06003230 RID: 12848 RVA: 0x002E3DF0 File Offset: 0x002E2DF0
	internal static FootnoteRestartRule ᜉ(string A_0)
	{
		int a_ = 14;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				if (!(A_0 == ClipboardData.b("ᅳ᝵᭷ቹ⽻᭽", a_)))
				{
					num = 10;
					continue;
				}
				return FootnoteRestartRule.RestartSection;
			case 1:
				if (!(A_0 == ClipboardData.b("ᅳ᝵᭷ቹⱻώ", a_)))
				{
					num = 11;
					continue;
				}
				return FootnoteRestartRule.RestartPage;
			case 2:
				goto IL_61;
			case 3:
				if (!(A_0 == ClipboardData.b("ᝳ᥵ᙷ๹ᕻၽ", a_)))
				{
					num = 12;
					continue;
				}
				return FootnoteRestartRule.DoNotRestart;
			case 4:
				if (!(A_0 == ClipboardData.b("ᅳ᝵᭷ቹ养๽", a_)))
				{
					num = 2;
					continue;
				}
				return FootnoteRestartRule.RestartPage;
			case 5:
				num = 3;
				continue;
			case 7:
				if (!(A_0 == ClipboardData.b("ᅳ᝵᭷ቹ养ൽ", a_)))
				{
					num = 8;
					continue;
				}
				return FootnoteRestartRule.RestartSection;
			case 8:
				num = 1;
				continue;
			case 9:
				goto IL_69;
			case 10:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_61;
				default:
					if (false)
					{
					}
					num = 7;
					continue;
				}
				break;
			case 11:
				num = 4;
				continue;
			case 12:
				num = 0;
				continue;
			}
			if (A_0 != null)
			{
				num = 5;
				continue;
			}
			return FootnoteRestartRule.DoNotRestart;
			IL_61:
			num = 9;
		}
		return FootnoteRestartRule.RestartSection;
		IL_69:
		return FootnoteRestartRule.DoNotRestart;
	}

	// Token: 0x06003231 RID: 12849 RVA: 0x002E3F94 File Offset: 0x002E2F94
	internal static string ᜀ(FootnoteRestartRule A_0, bool A_1)
	{
		int a_ = 15;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_63;
			case 1:
				num = 8;
				continue;
			case 2:
				switch (A_0)
				{
				case FootnoteRestartRule.DoNotRestart:
					goto IL_F7;
				case FootnoteRestartRule.RestartSection:
					num = 7;
					continue;
				case FootnoteRestartRule.RestartPage:
					num = 5;
					continue;
				default:
					num = 1;
					continue;
				}
				break;
			case 3:
				goto IL_48;
			case 5:
				if (!A_1)
				{
					num = 0;
					continue;
				}
				goto IL_E8;
			case 6:
				goto IL_136;
			case 7:
				if (!A_1)
				{
					num = 6;
					continue;
				}
				goto IL_D9;
			case 8:
				goto IL_111;
			}
			if (A_0 == FootnoteRestartRule.DoNotRestart)
			{
				num = 3;
			}
			else
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
					break;
				}
				num = 2;
			}
		}
		IL_48:
		return "";
		IL_63:
		if (true)
		{
		}
		return ClipboardData.b("ၴᙶ᩸፺偼ཾ", a_);
		IL_D9:
		return ClipboardData.b("ၴᙶ᩸፺⹼᩾", a_);
		IL_E8:
		return ClipboardData.b("ၴᙶ᩸፺⵼Ṿ", a_);
		IL_F7:
		return ClipboardData.b("ᙴᡶ᝸ེᑼᅾ", a_);
		IL_111:
		return "";
		IL_136:
		return ClipboardData.b("ၴᙶ᩸፺偼౾", a_);
	}

	// Token: 0x06003232 RID: 12850 RVA: 0x002E40E4 File Offset: 0x002E30E4
	internal static GridPitchType ᜈ(string A_0)
	{
		int a_ = 10;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 9;
				continue;
			case 1:
				if (!(A_0 == ClipboardData.b("ᱯ᭱ᩳ፵୷坹ᵻၽ꾁ﾋ", a_)))
				{
					num = 11;
					continue;
				}
				return GridPitchType.CharsAndLine;
			case 2:
				if (!(A_0 == ClipboardData.b("ͯᱱᕳٵ啷๹፻卽ﮇ", a_)))
				{
					num = 7;
					continue;
				}
				return GridPitchType.SnapToChars;
			case 3:
				num = 1;
				continue;
			case 5:
				if (!(A_0 == ClipboardData.b("ᱯ᭱ᩳ፵୷㭹ቻ᩽썿ﮇ", a_)))
				{
					num = 3;
					continue;
				}
				return GridPitchType.CharsAndLine;
			case 6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_97;
				default:
					if (false)
					{
					}
					num = 5;
					continue;
				}
				break;
			case 7:
				goto IL_97;
			case 8:
				goto IL_127;
			case 9:
				if (!(A_0 == ClipboardData.b("ͯᱱᕳٵⱷᕹ㽻ᙽ", a_)))
				{
					if (true)
					{
					}
					num = 10;
					continue;
				}
				return GridPitchType.SnapToChars;
			case 10:
				num = 2;
				continue;
			case 11:
				num = 12;
				continue;
			case 12:
				if (!(A_0 == ClipboardData.b("ᱯ᭱ᩳ፵୷", a_)))
				{
					num = 0;
					continue;
				}
				return GridPitchType.LinesOnly;
			}
			if (A_0 != null)
			{
				num = 6;
				continue;
			}
			return GridPitchType.NoGrid;
			IL_97:
			num = 8;
		}
		return GridPitchType.CharsAndLine;
		IL_127:
		return GridPitchType.NoGrid;
	}

	// Token: 0x06003233 RID: 12851 RVA: 0x002E4288 File Offset: 0x002E3288
	internal static string ᜀ(GridPitchType A_0, bool A_1)
	{
		int a_ = 0;
		for (;;)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!A_1)
					{
						num = 3;
						continue;
					}
					goto IL_EA;
				case 1:
					goto IL_AB;
				case 2:
					switch (A_0)
					{
					case GridPitchType.CharsAndLine:
						num = 0;
						continue;
					case GridPitchType.LinesOnly:
						goto IL_6A;
					case GridPitchType.SnapToChars:
						num = 4;
						continue;
					default:
						num = 6;
						continue;
					}
					break;
				case 3:
					goto IL_8F;
				case 4:
					if (!A_1)
					{
						num = 5;
						continue;
					}
					goto IL_91;
				case 5:
					goto IL_68;
				case 6:
					num = 1;
					continue;
				}
				break;
			}
		}
		IL_68:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_AB:
			return "";
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			return ClipboardData.b("ᕥ٧୩ᱫ䍭ѯᵱ女ᕵၷ᭹๻ൽ", a_);
		}
		IL_6A:
		return ClipboardData.b("੥ŧѩ५ᵭ", a_);
		IL_8F:
		return ClipboardData.b("੥ŧѩ५ᵭ嵯፱ᩳት啷᥹ᑻώ", a_);
		IL_91:
		return ClipboardData.b("ᕥ٧୩ᱫ㩭Ὧㅱᱳ᝵੷ॹ", a_);
		IL_EA:
		return ClipboardData.b("੥ŧѩ५ᵭㅯᱱၳ㕵ၷ᭹๻ൽ", a_);
	}

	// Token: 0x06003234 RID: 12852 RVA: 0x002E43A4 File Offset: 0x002E33A4
	internal static HeaderFooterType ᜀ(string A_0, bool A_1)
	{
		int a_ = 4;
		int num = 11;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1)
				{
					num = 9;
					continue;
				}
				return HeaderFooterType.FooterOdd;
			case 1:
				return HeaderFooterType.HeaderEven;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_113;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num = 10;
					continue;
				}
				break;
			case 3:
				if (A_1)
				{
					num = 1;
					continue;
				}
				return HeaderFooterType.FooterEven;
			case 4:
				num = 5;
				continue;
			case 5:
				if (!(A_0 == ClipboardData.b("ཀྵᩫ୭ṯ", a_)))
				{
					num = 12;
					continue;
				}
				num = 3;
				continue;
			case 6:
				return HeaderFooterType.HeaderFirstPage;
			case 7:
				if (A_1)
				{
					num = 6;
					continue;
				}
				return HeaderFooterType.FooterFirstPage;
			case 8:
				if (!(A_0 == ClipboardData.b("౩իᱭͯٱ", a_)))
				{
					num = 2;
					continue;
				}
				num = 7;
				continue;
			case 9:
				return HeaderFooterType.HeaderOdd;
			case 10:
				goto IL_113;
			case 12:
				num = 8;
				continue;
			}
			if (A_0 != null)
			{
				num = 4;
				continue;
			}
			IL_8B:
			num = 0;
			continue;
			IL_113:
			goto IL_8B;
		}
		return HeaderFooterType.FooterEven;
	}

	// Token: 0x06003235 RID: 12853 RVA: 0x002E4508 File Offset: 0x002E3508
	internal static string ᜀ(HeaderFooterType A_0, bool A_1)
	{
		int a_ = 14;
		for (;;)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch (A_0)
					{
					case HeaderFooterType.HeaderEven:
					case HeaderFooterType.FooterEven:
						goto IL_BF;
					case HeaderFooterType.HeaderOdd:
					case HeaderFooterType.FooterOdd:
						num = 4;
						continue;
					case HeaderFooterType.HeaderFirstPage:
					case HeaderFooterType.FooterFirstPage:
						goto IL_8A;
					default:
						num = 2;
						continue;
					}
					break;
				case 1:
					goto IL_AE;
				case 2:
					if (true)
					{
					}
					num = 1;
					continue;
				case 3:
					goto IL_E7;
				case 4:
					if (!A_1)
					{
						num = 3;
						continue;
					}
					goto IL_B0;
				}
				break;
			}
		}
		for (;;)
		{
			IL_E7:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_75;
			}
		}
		IL_75:
		if (false)
		{
		}
		return ClipboardData.b("᭳ትᱷ", a_);
		IL_8A:
		return ClipboardData.b("ታή੷ॹࡻ", a_);
		IL_AE:
		throw new InvalidOperationException(ClipboardData.b("ⅳᡵ፷ᑹ፻ॽꊁﲍ낏ﮓ秊ﾙ뺝풟\udba1풣쎥蚧", a_));
		IL_B0:
		return ClipboardData.b("ၳ፵ṷ᭹ॻች", a_);
		IL_BF:
		return ClipboardData.b("ᅳuᵷᑹ", a_);
	}

	// Token: 0x06003236 RID: 12854 RVA: 0x002E4614 File Offset: 0x002E3614
	internal static FootnotePosition ᜇ(string A_0)
	{
		int a_ = 5;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_60;
			case 1:
			{
				int num2;
				if (spr᧓.\u17BC.TryGetValue(A_0, out num2))
				{
					num = 6;
					continue;
				}
				return FootnotePosition.PrintAsEndnotes;
			}
			case 2:
			{
				int num2;
				switch (num2)
				{
				case 0:
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_58;
					default:
						goto IL_178;
					}
					break;
				case 2:
				case 3:
					return FootnotePosition.PrintAtBottomOfPage;
				case 4:
				case 5:
					return FootnotePosition.PrintImmediatelyBeneathText;
				case 6:
				case 7:
					return FootnotePosition.PrintAsEndOfDocument;
				default:
					num = 5;
					continue;
				}
				break;
			}
			case 3:
				spr᧓.\u17BC = new Dictionary<string, int>(8)
				{
					{
						ClipboardData.b("ᡪ࡬౮հ㙲᭴፶", a_),
						0
					},
					{
						ClipboardData.b("ᡪ࡬౮հ干ၴ᥶ᵸ", a_),
						1
					},
					{
						ClipboardData.b("᭪౬࡮ᑰㅲᩴͶ൸ᑺၼ", a_),
						2
					},
					{
						ClipboardData.b("᭪౬࡮ᑰ干᝴ᡶ൸ེቼቾ", a_),
						3
					},
					{
						ClipboardData.b("४࡬ŮᑰቲŴὶ⵸Ṻռ୾", a_),
						4
					},
					{
						ClipboardData.b("४࡬ŮᑰቲŴὶ呸ེ᡼ݾ", a_),
						5
					},
					{
						ClipboardData.b("ཪɬ౮㑰ᵲᅴ", a_),
						6
					},
					{
						ClipboardData.b("ཪɬ౮屰ᙲ᭴፶", a_),
						7
					}
				};
				num = 7;
				continue;
			case 5:
				goto IL_58;
			case 6:
				num = 2;
				continue;
			case 7:
				goto IL_1C3;
			case 8:
				num = 9;
				continue;
			case 9:
				if (spr᧓.\u17BC == null)
				{
					num = 3;
					continue;
				}
				goto IL_1C3;
			}
			if (A_0 != null)
			{
				num = 8;
				continue;
			}
			return FootnotePosition.PrintAsEndnotes;
			IL_58:
			num = 0;
			continue;
			IL_1C3:
			num = 1;
		}
		return FootnotePosition.PrintAtBottomOfPage;
		IL_60:
		if (true)
		{
		}
		return FootnotePosition.PrintAsEndnotes;
		IL_178:
		if (false)
		{
		}
		return FootnotePosition.PrintAsEndnotes;
	}

	// Token: 0x06003237 RID: 12855 RVA: 0x002E4810 File Offset: 0x002E3810
	internal static string ᜀ(FootnotePosition A_0, bool A_1)
	{
		int a_ = 6;
		for (;;)
		{
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!A_1)
					{
						num = 2;
						continue;
					}
					goto IL_79;
				case 1:
					goto IL_175;
				case 2:
					goto IL_193;
				case 3:
					num = 1;
					continue;
				case 4:
					goto IL_154;
				case 5:
					goto IL_BF;
				case 6:
					goto IL_120;
				case 7:
					if (!A_1)
					{
						num = 5;
						continue;
					}
					goto IL_F8;
				case 8:
					if (!A_1)
					{
						num = 6;
						continue;
					}
					goto IL_D0;
				case 9:
					switch (A_0)
					{
					case FootnotePosition.PrintAsEndnotes:
						num = 7;
						continue;
					case FootnotePosition.PrintAtBottomOfPage:
						num = 0;
						continue;
					case FootnotePosition.PrintImmediatelyBeneathText:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_154;
						default:
							if (false)
							{
							}
							num = 4;
							continue;
						}
						break;
					case FootnotePosition.PrintAsEndOfDocument:
						num = 8;
						continue;
					default:
						num = 3;
						continue;
					}
					break;
				case 10:
					goto IL_165;
				}
				break;
				IL_154:
				if (A_1)
				{
					goto IL_6A;
				}
				num = 10;
			}
		}
		IL_6A:
		return ClipboardData.b("๫୭ṯ᝱ᕳɵၷ⹹᥻ٽ", a_);
		IL_79:
		return ClipboardData.b("ᱫ཭ᝯ᝱㙳᥵౷๹፻፽", a_);
		IL_BF:
		return ClipboardData.b("Ὣ୭፯ٱ女፵ᙷṹ", a_);
		IL_D0:
		return ClipboardData.b("࡫ŭ፯㝱ᩳት", a_);
		IL_F8:
		return ClipboardData.b("Ὣ୭፯ٱㅳᡵᱷ", a_);
		IL_120:
		if (true)
		{
		}
		return ClipboardData.b("࡫ŭ፯影ᅳᡵᱷ", a_);
		IL_165:
		return ClipboardData.b("๫୭ṯ᝱ᕳɵၷ坹ࡻ᭽", a_);
		IL_175:
		return "";
		IL_193:
		return ClipboardData.b("ᱫ཭ᝯ᝱女ᑵ᝷๹ࡻᅽ", a_);
	}

	// Token: 0x06003238 RID: 12856 RVA: 0x002E49BC File Offset: 0x002E39BC
	internal static PageAlignment ᜆ(string A_0)
	{
		int a_ = 12;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 2;
				continue;
			case 1:
				num = 6;
				continue;
			case 2:
				if (!(A_0 == ClipboardData.b("ٱ᭳ٵ", a_)))
				{
					num = 5;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_4D;
				default:
					goto IL_EC;
				}
				break;
			case 4:
				num = 7;
				continue;
			case 5:
				num = 10;
				continue;
			case 6:
				if (!(A_0 == ClipboardData.b("ၱ᭳ɵၷ", a_)))
				{
					num = 4;
					continue;
				}
				return PageAlignment.Justified;
			case 7:
				if (!(A_0 == ClipboardData.b("ၱ᭳ɵ౷ᕹᅻ", a_)))
				{
					num = 8;
					continue;
				}
				return PageAlignment.Bottom;
			case 8:
				if (true)
				{
				}
				num = 9;
				continue;
			case 9:
				goto IL_73;
			case 10:
				if (!(A_0 == ClipboardData.b("ᅱᅳᡵ౷ό๻", a_)))
				{
					num = 1;
					continue;
				}
				return PageAlignment.Middle;
			}
			goto IL_45;
			IL_4D:
			num = 0;
			continue;
			IL_45:
			if (A_0 != null)
			{
				goto IL_4D;
			}
			return PageAlignment.Top;
		}
		return PageAlignment.Middle;
		IL_73:
		return PageAlignment.Top;
		IL_EC:
		if (false)
		{
		}
		return PageAlignment.Top;
	}

	// Token: 0x06003239 RID: 12857 RVA: 0x002E4B24 File Offset: 0x002E3B24
	internal static string ᜀ(PageAlignment A_0)
	{
		int a_ = 3;
		for (;;)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch (A_0)
					{
					case PageAlignment.Top:
						goto IL_88;
					case PageAlignment.Middle:
						goto IL_60;
					case PageAlignment.Justified:
						goto IL_47;
					case PageAlignment.Bottom:
						goto IL_79;
					default:
						num = 1;
						continue;
					}
					break;
				case 1:
					goto IL_6F;
				case 2:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6F;
					default:
						goto IL_B5;
					}
					break;
				}
				break;
				IL_6F:
				num = 2;
			}
		}
		IL_47:
		return ClipboardData.b("୨Ѫᥬݮ", a_);
		IL_60:
		return ClipboardData.b("੨๪ͬ᭮ᑰŲ", a_);
		IL_79:
		return ClipboardData.b("୨Ѫᥬ᭮ṰṲ", a_);
		IL_88:
		return ClipboardData.b("ᵨѪᵬ", a_);
		IL_B5:
		if (false)
		{
		}
		return "";
	}

	// Token: 0x0600323A RID: 12858 RVA: 0x002E4BF4 File Offset: 0x002E3BF4
	internal static ChapterPageSeparator ᜅ(string A_0)
	{
		int a_ = 11;
		int num = 8;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				goto IL_19C;
			case 1:
			{
				int num2;
				if (spr᧓.\u17BD.TryGetValue(A_0, out num2))
				{
					num = 4;
					continue;
				}
				return ChapterPageSeparator.Hyphen;
			}
			case 2:
				num = 9;
				continue;
			case 3:
				spr᧓.\u17BD = new Dictionary<string, int>(7)
				{
					{
						ClipboardData.b("ᥰੲմὶᱸᕺ", a_),
						0
					},
					{
						ClipboardData.b("ŰᙲݴṶᙸὺ", a_),
						1
					},
					{
						ClipboardData.b("ተᱲᥴᡶ᝸", a_),
						2
					},
					{
						ClipboardData.b("ᑰṲㅴᙶ੸፺", a_),
						3
					},
					{
						ClipboardData.b("ᑰṲ塴፶ᡸࡺᕼ", a_),
						4
					},
					{
						ClipboardData.b("ᑰᵲㅴᙶ੸፺", a_),
						5
					},
					{
						ClipboardData.b("ᑰᵲ塴፶ᡸࡺᕼ", a_),
						6
					}
				};
				num = 0;
				continue;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_51;
				default:
					if (false)
					{
					}
					num = 5;
					continue;
				}
				break;
			case 5:
			{
				int num2;
				switch (num2)
				{
				case 0:
					return ChapterPageSeparator.Hyphen;
				case 1:
					return ChapterPageSeparator.Period;
				case 2:
					return ChapterPageSeparator.Colon;
				case 3:
				case 4:
					return ChapterPageSeparator.EmDash;
				case 5:
				case 6:
					return ChapterPageSeparator.EnDash;
				default:
					num = 2;
					continue;
				}
				break;
			}
			case 6:
				if (spr᧓.\u17BD == null)
				{
					num = 3;
					continue;
				}
				goto IL_19C;
			case 7:
				num = 6;
				continue;
			case 9:
				return ChapterPageSeparator.Hyphen;
			}
			goto IL_49;
			IL_51:
			num = 7;
			continue;
			IL_49:
			if (A_0 != null)
			{
				goto IL_51;
			}
			return ChapterPageSeparator.Hyphen;
			IL_19C:
			num = 1;
		}
		return ChapterPageSeparator.Colon;
	}

	// Token: 0x0600323B RID: 12859 RVA: 0x002E4DD4 File Offset: 0x002E3DD4
	internal static string ᜀ(ChapterPageSeparator A_0, bool A_1)
	{
		int a_ = 16;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_DB;
			default:
			{
				if (false)
				{
				}
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_130;
					case 1:
						if (!A_1)
						{
							num = 0;
							continue;
						}
						goto IL_DB;
					case 2:
						if (!A_1)
						{
							num = 6;
							continue;
						}
						goto IL_EA;
					case 3:
						num = 5;
						continue;
					case 4:
						switch (A_0)
						{
						case ChapterPageSeparator.Hyphen:
							goto IL_A6;
						case ChapterPageSeparator.Period:
							goto IL_108;
						case ChapterPageSeparator.Colon:
							goto IL_F9;
						case ChapterPageSeparator.EmDash:
							num = 1;
							continue;
						case ChapterPageSeparator.EnDash:
							num = 2;
							continue;
						default:
							if (true)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 5:
						goto IL_D9;
					case 6:
						goto IL_95;
					}
					break;
				}
				break;
			}
			}
		}
		IL_95:
		return ClipboardData.b("፵ᙷ坹᡻ώ", a_);
		IL_A6:
		return ClipboardData.b("ṵŷ੹ᑻ᭽", a_);
		IL_D9:
		return "";
		IL_DB:
		return ClipboardData.b("፵ᕷ㹹ᵻൽ", a_);
		IL_EA:
		return ClipboardData.b("፵ᙷ㹹ᵻൽ", a_);
		IL_F9:
		return ClipboardData.b("ᕵ᝷ᙹ፻ၽ", a_);
		IL_108:
		return ClipboardData.b("ٵᵷࡹᕻᅽ", a_);
		IL_130:
		return ClipboardData.b("፵ᕷ坹᡻ώ", a_);
	}

	// Token: 0x0600323C RID: 12860 RVA: 0x002E4F1C File Offset: 0x002E3F1C
	internal static LineNumberingRestartMode ᜄ(string A_0)
	{
		int a_ = 7;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 3;
				continue;
			case 1:
				if (!(A_0 == ClipboardData.b("ͬ੮ٰ⍲ᑴၶᱸ", a_)))
				{
					num = 4;
					continue;
				}
				return LineNumberingRestartMode.RestartPage;
			case 2:
				goto IL_12F;
			case 3:
				if (!(A_0 == ClipboardData.b("ͬ੮ٰ⁲ၴᑶ൸ቺቼᅾ", a_)))
				{
					num = 8;
					continue;
				}
				return LineNumberingRestartMode.RestartSection;
			case 4:
				num = 11;
				continue;
			case 5:
				goto IL_9F;
			case 7:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_9F;
				default:
					if (false)
					{
					}
					num = 12;
					continue;
				}
				break;
			case 8:
				num = 10;
				continue;
			case 9:
				num = 1;
				continue;
			case 10:
				if (!(A_0 == ClipboardData.b("ͬ੮ٰ干ٴቶེ᩸ᑼၾ", a_)))
				{
					if (true)
					{
					}
					num = 5;
					continue;
				}
				return LineNumberingRestartMode.RestartSection;
			case 11:
				if (!(A_0 == ClipboardData.b("ͬ੮ٰ干մᙶṸṺ", a_)))
				{
					num = 0;
					continue;
				}
				return LineNumberingRestartMode.RestartPage;
			case 12:
				if (!(A_0 == ClipboardData.b("๬nὰݲᱴ᥶౸ᑺࡼ౾", a_)))
				{
					num = 9;
					continue;
				}
				return LineNumberingRestartMode.Continuous;
			}
			if (A_0 != null)
			{
				num = 7;
				continue;
			}
			return LineNumberingRestartMode.RestartPage;
			IL_9F:
			num = 2;
		}
		return LineNumberingRestartMode.Continuous;
		IL_12F:
		return LineNumberingRestartMode.RestartPage;
	}

	// Token: 0x0600323D RID: 12861 RVA: 0x002E50C0 File Offset: 0x002E40C0
	internal static string ᜀ(LineNumberingRestartMode A_0, bool A_1)
	{
		int a_ = 16;
		for (;;)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!A_1)
					{
						num = 1;
						continue;
					}
					goto IL_8E;
				case 1:
					goto IL_6E;
				case 2:
					goto IL_10A;
				case 3:
					switch (A_0)
					{
					case LineNumberingRestartMode.RestartPage:
						goto IL_C8;
					case LineNumberingRestartMode.RestartSection:
						if (true)
						{
						}
						num = 0;
						continue;
					case LineNumberingRestartMode.Continuous:
						goto IL_7F;
					default:
						num = 4;
						continue;
					}
					break;
				case 4:
					num = 6;
					continue;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C8;
					default:
						if (false)
						{
						}
						if (!A_1)
						{
							num = 2;
							continue;
						}
						goto IL_70;
					}
					break;
				case 6:
					goto IL_A8;
				}
				break;
				IL_C8:
				num = 5;
			}
		}
		IL_6E:
		return ClipboardData.b("ᡵᵷ൹养ൽ", a_);
		IL_70:
		return ClipboardData.b("ᡵᵷ൹ⱻώ", a_);
		IL_7F:
		return ClipboardData.b("ᕵ᝷ᑹࡻ᝽ﮇ", a_);
		IL_8E:
		return ClipboardData.b("ᡵᵷ൹⽻᭽", a_);
		IL_A8:
		return "";
		IL_10A:
		return ClipboardData.b("ᡵᵷ൹养๽", a_);
	}

	// Token: 0x0600323E RID: 12862 RVA: 0x002E51E0 File Offset: 0x002E41E0
	internal static PageBorderOffsetFrom ᜃ(string A_0)
	{
		int a_ = 1;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!(A_0 == ClipboardData.b("፦౨፪ᥬ", a_)))
				{
					num = 1;
					continue;
				}
				return PageBorderOffsetFrom.Text;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_88;
				}
				if (false)
				{
				}
				num = 3;
				continue;
			case 2:
				goto IL_88;
			case 3:
				goto IL_51;
			case 4:
				if (!(A_0 == ClipboardData.b("ᝦࡨ౪࡬", a_)))
				{
					num = 2;
					continue;
				}
				return PageBorderOffsetFrom.PageEdge;
			case 5:
				if (true)
				{
				}
				num = 4;
				continue;
			}
			if (A_0 != null)
			{
				num = 5;
				continue;
			}
			return PageBorderOffsetFrom.PageEdge;
			IL_88:
			num = 0;
		}
		return PageBorderOffsetFrom.PageEdge;
		IL_51:
		return PageBorderOffsetFrom.PageEdge;
	}

	// Token: 0x0600323F RID: 12863 RVA: 0x002E52CC File Offset: 0x002E42CC
	internal static string ᜀ(PageBorderOffsetFrom A_0)
	{
		int a_ = 6;
		if (true)
		{
		}
		for (;;)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 2;
					continue;
				case 1:
					switch (A_0)
					{
					case PageBorderOffsetFrom.Text:
						goto IL_47;
					case PageBorderOffsetFrom.PageEdge:
						goto IL_56;
					default:
						num = 0;
						continue;
					}
					break;
				case 2:
					goto IL_6D;
				}
				break;
			}
		}
		IL_47:
		return ClipboardData.b("ᡫ୭࡯ٱ", a_);
		IL_56:
		return ClipboardData.b("ᱫ཭ᝯ᝱", a_);
		IL_6D:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_47;
		default:
			if (false)
			{
			}
			return "";
		}
	}

	// Token: 0x06003240 RID: 12864 RVA: 0x002E5374 File Offset: 0x002E4374
	internal static PageBordersApplyType ᜂ(string A_0)
	{
		int a_ = 14;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				int num2;
				if (spr᧓.\u17BE.TryGetValue(A_0, out num2))
				{
					num = 8;
					continue;
				}
				return PageBordersApplyType.AllPages;
			}
			case 1:
				goto IL_60;
			case 2:
			{
				int num2;
				switch (num2)
				{
				case 0:
				case 1:
					return PageBordersApplyType.AllPages;
				case 2:
				case 3:
					return PageBordersApplyType.FirstPage;
				case 4:
				case 5:
					return PageBordersApplyType.AllExceptFirstPage;
				default:
					num = 4;
					continue;
				}
				break;
			}
			case 4:
				num = 1;
				continue;
			case 5:
				goto IL_168;
			case 6:
				spr᧓.\u17BE = new Dictionary<string, int>(6)
				{
					{
						ClipboardData.b("ᕳ᩵ᑷ⩹ᵻ᥽", a_),
						0
					},
					{
						ClipboardData.b("ᕳ᩵ᑷ坹౻ώ", a_),
						1
					},
					{
						ClipboardData.b("ታή੷ॹࡻ⹽", a_),
						2
					},
					{
						ClipboardData.b("ታή੷ॹࡻ卽", a_),
						3
					},
					{
						ClipboardData.b("ᩳ᥵౷㱹ᕻ౽풃", a_),
						4
					},
					{
						ClipboardData.b("ᩳ᥵౷坹᩻᝽ꮅ", a_),
						5
					}
				};
				num = 5;
				continue;
			case 7:
				num = 9;
				continue;
			case 8:
				num = 2;
				continue;
			case 9:
				if (spr᧓.\u17BE == null)
				{
					num = 6;
					continue;
				}
				goto IL_168;
			}
			if (A_0 != null)
			{
				num = 7;
				continue;
			}
			return PageBordersApplyType.AllPages;
			IL_168:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return PageBordersApplyType.FirstPage;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				num = 0;
				break;
			}
		}
		return PageBordersApplyType.AllPages;
		IL_60:
		return PageBordersApplyType.AllPages;
	}

	// Token: 0x06003241 RID: 12865 RVA: 0x002E553C File Offset: 0x002E453C
	internal static string ᜀ(PageBordersApplyType A_0, bool A_1)
	{
		int a_ = 6;
		for (;;)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_A3;
				case 1:
					switch (A_0)
					{
					case PageBordersApplyType.AllPages:
						num = 6;
						continue;
					case PageBordersApplyType.FirstPage:
						num = 8;
						continue;
					case PageBordersApplyType.AllExceptFirstPage:
						goto IL_5B;
					default:
						num = 4;
						continue;
					}
					break;
				case 2:
					goto IL_E5;
				case 3:
					goto IL_71;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5B;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 5:
					if (!A_1)
					{
						num = 3;
						continue;
					}
					goto IL_F6;
				case 6:
					if (!A_1)
					{
						num = 0;
						continue;
					}
					goto IL_105;
				case 7:
					goto IL_13C;
				case 8:
					if (!A_1)
					{
						num = 7;
						continue;
					}
					goto IL_E7;
				}
				break;
				IL_5B:
				num = 5;
			}
		}
		IL_71:
		if (true)
		{
		}
		return ClipboardData.b("ɫŭѯ影ታή੷ॹࡻ卽", a_);
		IL_A3:
		return ClipboardData.b("൫ɭᱯ影ѳ᝵ίόཻ", a_);
		IL_E5:
		return "";
		IL_E7:
		return ClipboardData.b("੫ݭɯűs♵᥷ᵹ᥻", a_);
		IL_F6:
		return ClipboardData.b("ɫŭѯ㑱ᵳѵ୷๹ⱻώ", a_);
		IL_105:
		return ClipboardData.b("൫ɭᱯ≱ᕳᅵᵷॹ", a_);
		IL_13C:
		return ClipboardData.b("੫ݭɯűs孵ࡷ᭹᭻᭽", a_);
	}

	// Token: 0x06003242 RID: 12866 RVA: 0x002E5690 File Offset: 0x002E4690
	internal static PageOrientation ᜁ(string A_0)
	{
		int a_ = 0;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 1;
				continue;
			case 1:
				if (!(A_0 == ClipboardData.b("੥१ѩ࡫ᵭ፯፱ѳ፵", a_)))
				{
					num = 4;
					continue;
				}
				return PageOrientation.Landscape;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_80;
				default:
					if (false)
					{
					}
					num = 6;
					continue;
				}
				break;
			case 4:
				goto IL_80;
			case 5:
				if (!(A_0 == ClipboardData.b("ᙥݧᡩᡫᱭᅯ᭱s", a_)))
				{
					num = 2;
					continue;
				}
				return PageOrientation.Portrait;
			case 6:
				goto IL_51;
			}
			if (A_0 != null)
			{
				num = 0;
				continue;
			}
			goto IL_D4;
			IL_80:
			num = 5;
		}
		return PageOrientation.Landscape;
		IL_51:
		IL_D4:
		if (true)
		{
		}
		return PageOrientation.Portrait;
	}

	// Token: 0x06003243 RID: 12867 RVA: 0x002E577C File Offset: 0x002E477C
	internal static string ᜀ(PageOrientation A_0)
	{
		int a_ = 8;
		for (;;)
		{
			if (true)
			{
			}
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 2;
					continue;
				case 1:
					switch (A_0)
					{
					case PageOrientation.Portrait:
						goto IL_4B;
					case (PageOrientation)1:
						goto IL_99;
					case PageOrientation.Landscape:
						goto IL_5A;
					default:
						num = 0;
						continue;
					}
					break;
				case 2:
					goto IL_71;
				}
				break;
			}
		}
		IL_4B:
		return ClipboardData.b("ṭὯqsѵ᥷፹ࡻ", a_);
		IL_5A:
		return ClipboardData.b("ɭᅯᱱၳյ᭷᭹౻᭽", a_);
		IL_71:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_4B;
		default:
			if (false)
			{
			}
			break;
		}
		IL_99:
		return "";
	}

	// Token: 0x06003244 RID: 12868 RVA: 0x002E5828 File Offset: 0x002E4828
	internal static SectionBreakType ᜀ(string A_0)
	{
		int a_ = 3;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
			{
				int num2;
				if (spr᧓.\u17BF.TryGetValue(A_0, out num2))
				{
					num = 7;
					continue;
				}
				return SectionBreakType.NewPage;
			}
			case 2:
				if (spr᧓.\u17BF == null)
				{
					num = 9;
					continue;
				}
				goto IL_1CF;
			case 3:
			{
				int num2;
				switch (num2)
				{
				case 0:
					return SectionBreakType.NoBreak;
				case 1:
				case 2:
					return SectionBreakType.EvenPage;
				case 3:
				case 4:
					return SectionBreakType.NewColumn;
				case 5:
				case 6:
					return SectionBreakType.NewPage;
				case 7:
				case 8:
					return SectionBreakType.Oddpage;
				default:
					num = 5;
					continue;
				}
				break;
			}
			case 4:
				goto IL_1CF;
			case 5:
				num = 6;
				continue;
			case 6:
				return SectionBreakType.NewPage;
			case 7:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_53;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			case 8:
				num = 2;
				continue;
			case 9:
				spr᧓.\u17BF = new Dictionary<string, int>(9)
				{
					{
						ClipboardData.b("੨Ѫͬ᭮ᡰᵲtᡶ౸ࡺ", a_),
						0
					},
					{
						ClipboardData.b("౨ᵪ࡬Ůⅰቲቴቶ", a_),
						1
					},
					{
						ClipboardData.b("౨ᵪ࡬Ů屰Ͳᑴၶᱸ", a_),
						2
					},
					{
						ClipboardData.b("ݨ๪ᕬ᭮㉰ᱲᥴɶᑸᕺ", a_),
						3
					},
					{
						ClipboardData.b("ݨ๪ᕬ᭮屰ၲᩴ᭶౸ᙺ፼", a_),
						4
					},
					{
						ClipboardData.b("ݨ๪ᕬ᭮ⅰቲቴቶ", a_),
						5
					},
					{
						ClipboardData.b("ݨ๪ᕬ᭮屰Ͳᑴၶᱸ", a_),
						6
					},
					{
						ClipboardData.b("٨ཪ६㽮ၰᑲၴ", a_),
						7
					},
					{
						ClipboardData.b("٨ཪ६䉮Űቲቴቶ", a_),
						8
					}
				};
				num = 4;
				continue;
			}
			goto IL_4B;
			IL_53:
			num = 8;
			continue;
			IL_4B:
			if (A_0 != null)
			{
				goto IL_53;
			}
			return SectionBreakType.NewPage;
			IL_1CF:
			num = 1;
		}
		return SectionBreakType.NewColumn;
	}

	// Token: 0x06003245 RID: 12869 RVA: 0x002E5A40 File Offset: 0x002E4A40
	internal static string ᜀ(SectionBreakType A_0, bool A_1)
	{
		int a_ = 16;
		for (;;)
		{
			IL_3D:
			int num = 10;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_DF;
				case 1:
					goto IL_C4;
				case 2:
					goto IL_1A3;
				case 3:
					goto IL_154;
				case 4:
					goto IL_17B;
				case 5:
					if (true)
					{
					}
					num = 4;
					continue;
				case 6:
					if (!A_1)
					{
						num = 3;
						continue;
					}
					goto IL_F0;
				case 7:
					if (!A_1)
					{
						num = 0;
						continue;
					}
					goto IL_FF;
				case 8:
					if (!A_1)
					{
						num = 1;
						continue;
					}
					goto IL_159;
				case 9:
					if (!A_1)
					{
						num = 2;
						continue;
					}
					goto IL_99;
				case 10:
					switch (A_0)
					{
					case SectionBreakType.NoBreak:
						goto IL_E1;
					case SectionBreakType.NewColumn:
						num = 9;
						continue;
					case SectionBreakType.NewPage:
						num = 6;
						continue;
					case SectionBreakType.EvenPage:
						num = 8;
						continue;
					case SectionBreakType.Oddpage:
						num = 7;
						continue;
					default:
						num = 5;
						continue;
					}
					break;
				}
				goto IL_3D;
			}
			IL_154:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_84;
			}
		}
		IL_84:
		if (false)
		{
		}
		return ClipboardData.b("ᡵᵷɹࡻ卽", a_);
		IL_99:
		return ClipboardData.b("ᡵᵷɹࡻ㵽", a_);
		IL_C4:
		return ClipboardData.b("፵๷όቻ卽", a_);
		IL_DF:
		return ClipboardData.b("᥵ᱷṹ养๽", a_);
		IL_E1:
		return ClipboardData.b("ᕵ᝷ᑹࡻ᝽ﮇ", a_);
		IL_F0:
		return ClipboardData.b("ᡵᵷɹࡻ⹽", a_);
		IL_FF:
		return ClipboardData.b("᥵ᱷṹⱻώ", a_);
		IL_159:
		return ClipboardData.b("፵๷όቻ⹽", a_);
		IL_17B:
		return "";
		IL_1A3:
		return ClipboardData.b("ᡵᵷɹࡻ卽", a_);
	}
}
