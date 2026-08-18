using System;
using System.Collections;
using System.Collections.Generic;
using Spire.CompoundFile.Doc;
using Spire.Doc;
using Spire.Doc.Fields.Shape;

// Token: 0x020003A4 RID: 932
internal class sprḕ
{
	// Token: 0x060034B2 RID: 13490 RVA: 0x00306810 File Offset: 0x00305810
	internal static FramesetBorderType ᜐ(string A_0)
	{
		int a_ = 6;
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
					break;
				default:
					if (false)
					{
					}
					num = 5;
					continue;
				}
				break;
			case 1:
				if (!(A_0 == ClipboardData.b("ɫŭ㉯ᵱٳትᵷࡹ", a_)))
				{
					num = 0;
					continue;
				}
				return FramesetBorderType.None;
			case 2:
				num = 1;
				continue;
			case 3:
				if (true)
				{
				}
				num = 6;
				continue;
			case 5:
				goto IL_59;
			case 6:
				if (!(A_0 == ClipboardData.b("੫ɭᅯٱ㙳᥵੷ṹ᥻౽", a_)))
				{
					num = 2;
					continue;
				}
				return FramesetBorderType.Simple;
			}
			if (A_0 == null)
			{
				return FramesetBorderType.Raised;
			}
			num = 3;
		}
		return FramesetBorderType.Simple;
		IL_59:
		return FramesetBorderType.Raised;
	}

	// Token: 0x060034B3 RID: 13491 RVA: 0x003068FC File Offset: 0x003058FC
	internal static string ᜀ(FramesetBorderType A_0)
	{
		int a_ = 9;
		for (;;)
		{
			IL_39:
			if (true)
			{
			}
			int num = 1;
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
						num = 2;
						continue;
					case 1:
						goto IL_4B;
					case 2:
						goto IL_96;
					}
					goto IL_39;
				}
				IL_4B:
				switch (A_0)
				{
				case FramesetBorderType.None:
					goto IL_6D;
				case FramesetBorderType.Simple:
					goto IL_7C;
				default:
					num = 0;
					break;
				}
			}
		}
		IL_6D:
		return ClipboardData.b("ŮṰㅲᩴնᵸṺོ", a_);
		IL_7C:
		return ClipboardData.b("८ᵰቲŴ㕶ᙸॺ᥼᩾", a_);
		IL_96:
		return "";
	}

	// Token: 0x060034B4 RID: 13492 RVA: 0x003069A8 File Offset: 0x003059A8
	internal static FrameLayoutType ᜏ(string A_0)
	{
		int a_ = 9;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 3;
				continue;
			case 2:
				goto IL_51;
			case 3:
				if (!(A_0 == ClipboardData.b("ᵮṰѲٴ", a_)))
				{
					num = 5;
					continue;
				}
				return FrameLayoutType.Vertical;
			case 4:
				num = 6;
				continue;
			case 5:
				if (true)
				{
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
					num = 2;
					continue;
				}
				break;
			case 6:
				if (!(A_0 == ClipboardData.b("౮Ṱὲٴ", a_)))
				{
					num = 0;
					continue;
				}
				return FrameLayoutType.Horizontal;
			}
			if (A_0 == null)
			{
				return FrameLayoutType.None;
			}
			num = 4;
		}
		return FrameLayoutType.Horizontal;
		IL_51:
		return FrameLayoutType.None;
	}

	// Token: 0x060034B5 RID: 13493 RVA: 0x00306A94 File Offset: 0x00305A94
	internal static string ᜀ(FrameLayoutType A_0)
	{
		int a_ = 16;
		for (;;)
		{
			IL_39:
			int num = 0;
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
						goto IL_43;
					case 1:
						if (true)
						{
						}
						num = 2;
						continue;
					case 2:
						goto IL_96;
					}
					goto IL_39;
				}
				IL_43:
				switch (A_0)
				{
				case FrameLayoutType.Vertical:
					goto IL_5B;
				case FrameLayoutType.Horizontal:
					goto IL_74;
				default:
					num = 1;
					break;
				}
			}
		}
		IL_5B:
		return ClipboardData.b("ѵ᝷൹ཻ", a_);
		IL_74:
		return ClipboardData.b("ᕵ᝷ᙹཻ", a_);
		IL_96:
		return "";
	}

	// Token: 0x060034B6 RID: 13494 RVA: 0x00306B40 File Offset: 0x00305B40
	internal static TableStyleOverrideType ᜎ(string A_0)
	{
		int a_ = 9;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				int num2;
				switch (num2)
				{
				case 0:
					return TableStyleOverrideType.Band1Horz;
				case 1:
					return TableStyleOverrideType.Band1Vert;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1FE;
					default:
						goto IL_27B;
					}
					break;
				case 3:
					return TableStyleOverrideType.Band2Vert;
				case 4:
					return TableStyleOverrideType.FirstCol;
				case 5:
					return TableStyleOverrideType.FirstRow;
				case 6:
					return TableStyleOverrideType.LastCol;
				case 7:
					return TableStyleOverrideType.LastRow;
				case 8:
					return TableStyleOverrideType.NECell;
				case 9:
					return TableStyleOverrideType.NWCell;
				case 10:
					return TableStyleOverrideType.SECell;
				case 11:
					return TableStyleOverrideType.SWCell;
				case 12:
					return TableStyleOverrideType.WholeTable;
				default:
					num = 2;
					continue;
				}
				break;
			}
			case 1:
				num = 0;
				continue;
			case 2:
				num = 6;
				continue;
			case 3:
			{
				int num2;
				if (spr᧓.\u17C5.TryGetValue(A_0, out num2))
				{
					num = 1;
					continue;
				}
				return TableStyleOverrideType.None;
			}
			case 4:
				if (true)
				{
				}
				spr᧓.\u17C5 = new Dictionary<string, int>(13)
				{
					{
						ClipboardData.b("൮ၰᵲᅴ䙶ㅸᑺོվ", a_),
						0
					},
					{
						ClipboardData.b("൮ၰᵲᅴ䙶⽸Ṻོ୾", a_),
						1
					},
					{
						ClipboardData.b("൮ၰᵲᅴ䕶ㅸᑺོվ", a_),
						2
					},
					{
						ClipboardData.b("൮ၰᵲᅴ䕶⽸Ṻོ୾", a_),
						3
					},
					{
						ClipboardData.b("८ᡰŲٴͶ㩸ᑺᅼ", a_),
						4
					},
					{
						ClipboardData.b("८ᡰŲٴͶ⭸ᑺ੼", a_),
						5
					},
					{
						ClipboardData.b("ͮၰrŴ㑶ᙸ᝺", a_),
						6
					},
					{
						ClipboardData.b("ͮၰrŴ╶ᙸ౺", a_),
						7
					},
					{
						ClipboardData.b("Ůᑰひၴ᭶ᕸ", a_),
						8
					},
					{
						ClipboardData.b("Ůٰひၴ᭶ᕸ", a_),
						9
					},
					{
						ClipboardData.b("ᱮᑰひၴ᭶ᕸ", a_),
						10
					},
					{
						ClipboardData.b("ᱮٰひၴ᭶ᕸ", a_),
						11
					},
					{
						ClipboardData.b("ᡮᥰᱲᥴቶ⵸᩺ὼ፾", a_),
						12
					}
				};
				num = 7;
				continue;
			case 6:
				goto IL_1F1;
			case 7:
				goto IL_237;
			case 8:
				if (spr᧓.\u17C5 == null)
				{
					num = 4;
					continue;
				}
				goto IL_237;
			case 9:
				goto IL_1FE;
			}
			if (A_0 != null)
			{
				num = 9;
				continue;
			}
			return TableStyleOverrideType.None;
			IL_1FE:
			num = 8;
			continue;
			IL_237:
			num = 3;
		}
		return TableStyleOverrideType.LastRow;
		IL_1F1:
		return TableStyleOverrideType.None;
		IL_27B:
		if (false)
		{
		}
		return TableStyleOverrideType.Band2Horz;
	}

	// Token: 0x060034B7 RID: 13495 RVA: 0x00306DD8 File Offset: 0x00305DD8
	internal static string ᜀ(TableStyleOverrideType A_0)
	{
		int a_ = 9;
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
					case TableStyleOverrideType.Band1Horz:
						goto IL_B4;
					case TableStyleOverrideType.Band1Vert:
						goto IL_96;
					case TableStyleOverrideType.Band2Horz:
						goto IL_F0;
					case TableStyleOverrideType.Band2Vert:
						goto IL_127;
					case TableStyleOverrideType.FirstCol:
						goto IL_15F;
					case TableStyleOverrideType.FirstRow:
						goto IL_E1;
					case TableStyleOverrideType.LastCol:
						goto IL_87;
					case TableStyleOverrideType.LastRow:
						goto IL_118;
					case TableStyleOverrideType.NECell:
						goto IL_70;
					case TableStyleOverrideType.NWCell:
						goto IL_C3;
					case TableStyleOverrideType.SECell:
						goto IL_A5;
					case TableStyleOverrideType.SWCell:
						goto IL_109;
					case TableStyleOverrideType.WholeTable:
						goto IL_D2;
					default:
						num = 0;
						continue;
					}
					break;
				case 2:
					goto IL_141;
				}
				break;
			}
		}
		IL_70:
		if (true)
		{
		}
		return ClipboardData.b("Ůᑰひၴ᭶ᕸ", a_);
		IL_87:
		return ClipboardData.b("ͮၰrŴ㑶ᙸ᝺", a_);
		IL_96:
		return ClipboardData.b("൮ၰᵲᅴ䙶⽸Ṻོ୾", a_);
		IL_A5:
		return ClipboardData.b("ᱮᑰひၴ᭶ᕸ", a_);
		IL_B4:
		return ClipboardData.b("൮ၰᵲᅴ䙶ㅸᑺོվ", a_);
		IL_C3:
		return ClipboardData.b("Ůٰひၴ᭶ᕸ", a_);
		IL_D2:
		return ClipboardData.b("ᡮᥰᱲᥴቶ⵸᩺ὼ፾", a_);
		IL_E1:
		return ClipboardData.b("८ᡰŲٴͶ⭸ᑺ੼", a_);
		IL_F0:
		return ClipboardData.b("൮ၰᵲᅴ䕶ㅸᑺོվ", a_);
		IL_109:
		return ClipboardData.b("ᱮٰひၴ᭶ᕸ", a_);
		IL_118:
		return ClipboardData.b("ͮၰrŴ╶ᙸ౺", a_);
		IL_127:
		return ClipboardData.b("൮ၰᵲᅴ䕶⽸Ṻོ୾", a_);
		IL_141:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_109;
		default:
			if (false)
			{
			}
			return ClipboardData.b("ᡮᥰᱲᥴቶ⵸᩺ὼ፾", a_);
		}
		IL_15F:
		return ClipboardData.b("८ᡰŲٴͶ㩸ᑺᅼ", a_);
	}

	// Token: 0x060034B8 RID: 13496 RVA: 0x00306F64 File Offset: 0x00305F64
	internal static FontPitch \u170D(string A_0)
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
		return (FontPitch)spr\u19FA.ᜀ(sprḕ.ᜀ, A_0, FontPitch.Default);
	}

	// Token: 0x060034B9 RID: 13497 RVA: 0x00306FB8 File Offset: 0x00305FB8
	internal static string ᜀ(FontPitch A_0)
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
		return (string)spr\u19FA.ᜀ(sprḕ.ᜁ, A_0, "");
	}

	// Token: 0x060034BA RID: 13498 RVA: 0x00307010 File Offset: 0x00306010
	internal static RelativeHeight ᜌ(string A_0)
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
		return (RelativeHeight)spr\u19FA.ᜀ(sprḕ.ᜂ, A_0, RelativeHeight.Page);
	}

	// Token: 0x060034BB RID: 13499 RVA: 0x00307064 File Offset: 0x00306064
	internal static string ᜀ(RelativeHeight A_0)
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
		return (string)spr\u19FA.ᜀ(sprḕ.ᜃ, A_0, "");
	}

	// Token: 0x060034BC RID: 13500 RVA: 0x003070BC File Offset: 0x003060BC
	internal static RelativeWidth ᜋ(string A_0)
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
		return (RelativeWidth)spr\u19FA.ᜀ(sprḕ.ᜄ, A_0, RelativeWidth.Page);
	}

	// Token: 0x060034BD RID: 13501 RVA: 0x00307110 File Offset: 0x00306110
	internal static string ᜀ(RelativeWidth A_0)
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
		return (string)spr\u19FA.ᜀ(sprḕ.ᜅ, A_0, "");
	}

	// Token: 0x060034BE RID: 13502 RVA: 0x00307168 File Offset: 0x00306168
	internal static RelativeVerticalPosition ᜊ(string A_0)
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
		return (RelativeVerticalPosition)spr\u19FA.ᜀ(sprḕ.ᜆ, A_0, RelativeVerticalPosition.Margin);
	}

	// Token: 0x060034BF RID: 13503 RVA: 0x003071BC File Offset: 0x003061BC
	internal static RelativeVerticalPosition ᜀ(string A_0, RelativeVerticalPosition A_1)
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
		return (RelativeVerticalPosition)spr\u19FA.ᜀ(sprḕ.ᜆ, A_0, A_1);
	}

	// Token: 0x060034C0 RID: 13504 RVA: 0x00307210 File Offset: 0x00306210
	internal static string ᜀ(RelativeVerticalPosition A_0)
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
		return (string)spr\u19FA.ᜀ(sprḕ.ᜇ, A_0, "");
	}

	// Token: 0x060034C1 RID: 13505 RVA: 0x00307268 File Offset: 0x00306268
	internal static RelativeHorizontalPosition ᜉ(string A_0)
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
		return (RelativeHorizontalPosition)spr\u19FA.ᜀ(sprḕ.ᜈ, A_0, RelativeHorizontalPosition.Column);
	}

	// Token: 0x060034C2 RID: 13506 RVA: 0x003072BC File Offset: 0x003062BC
	internal static string ᜀ(RelativeHorizontalPosition A_0)
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
		return (string)spr\u19FA.ᜀ(sprḕ.ᜉ, A_0, "");
	}

	// Token: 0x060034C3 RID: 13507 RVA: 0x00307314 File Offset: 0x00306314
	internal static HorizontalPosition ᜈ(string A_0)
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
		return (HorizontalPosition)spr\u19FA.ᜀ(sprḕ.ᜊ, A_0, HorizontalPosition.None);
	}

	// Token: 0x060034C4 RID: 13508 RVA: 0x00307368 File Offset: 0x00306368
	internal static string ᜀ(HorizontalAlignment A_0)
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
		return (string)spr\u19FA.ᜀ(sprḕ.ᜋ, A_0, "");
	}

	// Token: 0x060034C5 RID: 13509 RVA: 0x003073C0 File Offset: 0x003063C0
	internal static VerticalPosition ᜇ(string A_0)
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
		return (VerticalPosition)spr\u19FA.ᜀ(sprḕ.ᜌ, A_0, VerticalPosition.None);
	}

	// Token: 0x060034C6 RID: 13510 RVA: 0x00307414 File Offset: 0x00306414
	internal static string ᜀ(VerticalAlignment A_0)
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
		return (string)spr\u19FA.ᜀ(sprḕ.\u170D, A_0, "");
	}

	// Token: 0x060034C7 RID: 13511 RVA: 0x0030746C File Offset: 0x0030646C
	internal static StyleIdentifier ᜆ(string A_0)
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
		return (StyleIdentifier)spr\u19FA.ᜀ(sprḕ.ᜎ, A_0, StyleIdentifier.User);
	}

	// Token: 0x060034C8 RID: 13512 RVA: 0x003074C4 File Offset: 0x003064C4
	internal static string ᜀ(StyleIdentifier A_0, string A_1)
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
		return (string)spr\u19FA.ᜀ(sprḕ.ᜏ, A_0, A_1);
	}

	// Token: 0x060034C9 RID: 13513 RVA: 0x00307518 File Offset: 0x00306518
	internal static string ᜅ(string A_0)
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
		return (string)spr\u19FA.ᜀ(sprḕ.ᜐ, A_0, A_0);
	}

	// Token: 0x060034CA RID: 13514 RVA: 0x00307564 File Offset: 0x00306564
	internal static string ᜄ(string A_0)
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
		return (string)spr\u19FA.ᜀ(sprḕ.ᜑ, A_0, A_0);
	}

	// Token: 0x060034CB RID: 13515 RVA: 0x003075B0 File Offset: 0x003065B0
	internal static ScreenSize ᜃ(string A_0)
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
		return (ScreenSize)spr\u19FA.ᜀ(sprḕ.\u1712, A_0, ScreenSize.Size800x600);
	}

	// Token: 0x060034CC RID: 13516 RVA: 0x00307604 File Offset: 0x00306604
	internal static string ᜀ(ScreenSize A_0)
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
		return (string)spr\u19FA.ᜀ(sprḕ.\u1713, A_0, "");
	}

	// Token: 0x060034CD RID: 13517 RVA: 0x0030765C File Offset: 0x0030665C
	internal static WidthType ᜂ(string A_0)
	{
		int a_ = 2;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 4;
				continue;
			case 1:
				if (!(A_0 == ClipboardData.b("ᡧ३ᡫ", a_)))
				{
					num = 8;
					continue;
				}
				return WidthType.Percentage;
			case 3:
				num = 1;
				continue;
			case 4:
				goto IL_59;
			case 5:
				if (true)
				{
				}
				num = 6;
				continue;
			case 6:
				if (!(A_0 == ClipboardData.b("१Ὡᡫŭ", a_)))
				{
					num = 0;
					continue;
				}
				return WidthType.Auto;
			case 7:
				if (!(A_0 == ClipboardData.b("౧ቩ൫", a_)))
				{
					num = 5;
					continue;
				}
				return WidthType.Twip;
			case 8:
				num = 7;
				continue;
			}
			if (A_0 == null)
			{
				goto IL_F8;
			}
			num = 3;
		}
		return WidthType.Percentage;
		IL_59:
		IL_F8:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return WidthType.Twip;
		default:
			if (false)
			{
			}
			return WidthType.Auto;
		}
	}

	// Token: 0x060034CE RID: 13518 RVA: 0x00307780 File Offset: 0x00306780
	internal static string ᜀ(WidthType A_0)
	{
		int a_ = 18;
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
					case WidthType.Auto:
						goto IL_9E;
					case WidthType.Percentage:
						goto IL_5E;
					case WidthType.Twip:
						goto IL_45;
					default:
						num = 2;
						continue;
					}
					break;
				case 1:
					goto IL_94;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_45;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				}
				break;
			}
		}
		IL_45:
		return ClipboardData.b("ᱷɹᵻ", a_);
		IL_5E:
		return ClipboardData.b("ࡷ᥹ࡻ", a_);
		IL_94:
		if (true)
		{
		}
		return "";
		IL_9E:
		return ClipboardData.b("᥷ཹࡻᅽ", a_);
	}

	// Token: 0x060034CF RID: 13519 RVA: 0x00307840 File Offset: 0x00306840
	internal static TextureStyle ᜁ(string A_0)
	{
		int a_ = 16;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 5;
				continue;
			case 1:
				goto IL_89B;
			case 2:
				goto IL_8DA;
			case 4:
			{
				int num2;
				if (spr᧓.\u17C6.TryGetValue(A_0, out num2))
				{
					num = 0;
					continue;
				}
				return TextureStyle.TextureNone;
			}
			case 5:
			{
				int num2;
				switch (num2)
				{
				case 0:
					goto IL_5E;
				case 1:
					return TextureStyle.TextureNil;
				case 2:
					return TextureStyle.TextureSolid;
				case 3:
				case 4:
					return TextureStyle.TextureDarkHorizontal;
				case 5:
				case 6:
					return TextureStyle.TextureDarkVertical;
				case 7:
				case 8:
					return TextureStyle.TextureDarkDiagonalDown;
				case 9:
				case 10:
					return TextureStyle.TextureDarkDiagonalUp;
				case 11:
				case 12:
					return TextureStyle.TextureDarkCross;
				case 13:
				case 14:
					return TextureStyle.TextureDarkDiagonalCross;
				case 15:
				case 16:
					return TextureStyle.TextureHorizontal;
				case 17:
				case 18:
					return TextureStyle.TextureVertical;
				case 19:
				case 20:
					return TextureStyle.TextureDiagonalDown;
				case 21:
				case 22:
					return TextureStyle.TextureDiagonalUp;
				case 23:
				case 24:
					return TextureStyle.TextureCross;
				case 25:
				case 26:
					return TextureStyle.TextureDiagonalCross;
				case 27:
				case 28:
					return TextureStyle.Texture5Percent;
				case 29:
				case 30:
					return TextureStyle.Texture10Percent;
				case 31:
				case 32:
					return TextureStyle.Texture12Pt5Percent;
				case 33:
				case 34:
					return TextureStyle.Texture15Percent;
				case 35:
				case 36:
					return TextureStyle.Texture20Percent;
				case 37:
				case 38:
					return TextureStyle.Texture25Percent;
				case 39:
				case 40:
					return TextureStyle.Texture30Percent;
				case 41:
				case 42:
					return TextureStyle.Texture35Percent;
				case 43:
				case 44:
					return TextureStyle.Texture37Pt5Percent;
				case 45:
				case 46:
					return TextureStyle.Texture40Percent;
				case 47:
				case 48:
					return TextureStyle.Texture45Percent;
				case 49:
				case 50:
					return TextureStyle.Texture50Percent;
				case 51:
				case 52:
					return TextureStyle.Texture55Percent;
				case 53:
				case 54:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7C;
					default:
						goto IL_74A;
					}
					break;
				case 55:
				case 56:
					return TextureStyle.Texture62Pt5Percent;
				case 57:
				case 58:
					return TextureStyle.Texture65Percent;
				case 59:
				case 60:
					return TextureStyle.Texture70Percent;
				case 61:
				case 62:
					return TextureStyle.Texture75Percent;
				case 63:
				case 64:
					return TextureStyle.Texture80Percent;
				case 65:
				case 66:
					return TextureStyle.Texture85Percent;
				case 67:
				case 68:
					return TextureStyle.Texture87Pt5Percent;
				case 69:
				case 70:
					return TextureStyle.Texture90Percent;
				case 71:
				case 72:
					return TextureStyle.Texture95Percent;
				default:
					num = 7;
					continue;
				}
				break;
			}
			case 6:
				spr᧓.\u17C6 = new Dictionary<string, int>(73)
				{
					{
						ClipboardData.b("ᕵᑷόᵻ౽", a_),
						0
					},
					{
						ClipboardData.b("ᡵᅷᙹ", a_),
						1
					},
					{
						ClipboardData.b("յ᝷ᙹᕻ᩽", a_),
						2
					},
					{
						ClipboardData.b("ṵ᝷ࡹٻ⵽", a_),
						3
					},
					{
						ClipboardData.b("ṵ᝷ࡹٻ卽", a_),
						4
					},
					{
						ClipboardData.b("uᵷࡹࡻ⵽", a_),
						5
					},
					{
						ClipboardData.b("uᵷࡹࡻ卽", a_),
						6
					},
					{
						ClipboardData.b("ѵᵷ౹᥻౽삃\udf8b揄ﮑ", a_),
						7
					},
					{
						ClipboardData.b("ѵᵷ౹᥻౽ꦃꎍﾕﾙ", a_),
						8
					},
					{
						ClipboardData.b("ትᅷ᭹᭻⵽", a_),
						9
					},
					{
						ClipboardData.b("ትᅷ᭹᭻卽", a_),
						10
					},
					{
						ClipboardData.b("ṵ᝷ࡹٻ㵽", a_),
						11
					},
					{
						ClipboardData.b("ṵ᝷ࡹٻ卽ﮇ", a_),
						12
					},
					{
						ClipboardData.b("ትᅷ᭹᭻㵽", a_),
						13
					},
					{
						ClipboardData.b("ትᅷ᭹᭻卽ﮇ", a_),
						14
					},
					{
						ClipboardData.b("ɵၷ፹ቻ㙽ﺃ햅ﲇﺍ", a_),
						15
					},
					{
						ClipboardData.b("ɵၷ፹ቻ卽ﲅꖇ黎ﲍ憐", a_),
						16
					},
					{
						ClipboardData.b("ɵၷ፹ቻ⡽햅ﲇﺍ", a_),
						17
					},
					{
						ClipboardData.b("ɵၷ፹ቻ卽ꖇ黎ﲍ憐", a_),
						18
					},
					{
						ClipboardData.b("ɵၷ፹ቻⱽﮇ좋잓ﮝ", a_),
						19
					},
					{
						ClipboardData.b("ɵၷ፹ቻ卽慎黎ꎍﮑ떗즟튡솣", a_),
						20
					},
					{
						ClipboardData.b("ɵၷ፹ቻ㩽햅ﲇﺍ", a_),
						21
					},
					{
						ClipboardData.b("ɵၷ፹ቻ卽ꖇ黎ﲍ憐", a_),
						22
					},
					{
						ClipboardData.b("ɵၷ፹ቻ㙽ﺃ얅慎ﾋﶍ", a_),
						23
					},
					{
						ClipboardData.b("ɵၷ፹ቻ卽ﲅꖇﺋ", a_),
						24
					},
					{
						ClipboardData.b("ɵၷ፹ቻ㩽얅慎ﾋﶍ", a_),
						25
					},
					{
						ClipboardData.b("ɵၷ፹ቻ卽ꖇﺋ", a_),
						26
					},
					{
						ClipboardData.b("ٵ᭷๹䥻", a_),
						27
					},
					{
						ClipboardData.b("ٵ᭷๹养䭽", a_),
						28
					},
					{
						ClipboardData.b("ٵ᭷๹䵻乽", a_),
						29
					},
					{
						ClipboardData.b("ٵ᭷๹养佽끿", a_),
						30
					},
					{
						ClipboardData.b("ٵ᭷๹䵻䱽", a_),
						31
					},
					{
						ClipboardData.b("ٵ᭷๹养佽뉿", a_),
						32
					},
					{
						ClipboardData.b("ٵ᭷๹䵻䭽", a_),
						33
					},
					{
						ClipboardData.b("ٵ᭷๹养佽땿", a_),
						34
					},
					{
						ClipboardData.b("ٵ᭷๹乻乽", a_),
						35
					},
					{
						ClipboardData.b("ٵ᭷๹养䱽끿", a_),
						36
					},
					{
						ClipboardData.b("ٵ᭷๹乻䭽", a_),
						37
					},
					{
						ClipboardData.b("ٵ᭷๹养䱽땿", a_),
						38
					},
					{
						ClipboardData.b("ٵ᭷๹佻乽", a_),
						39
					},
					{
						ClipboardData.b("ٵ᭷๹养䵽끿", a_),
						40
					},
					{
						ClipboardData.b("ٵ᭷๹佻䭽", a_),
						41
					},
					{
						ClipboardData.b("ٵ᭷๹养䵽땿", a_),
						42
					},
					{
						ClipboardData.b("ٵ᭷๹佻䥽", a_),
						43
					},
					{
						ClipboardData.b("ٵ᭷๹养䵽띿", a_),
						44
					},
					{
						ClipboardData.b("ٵ᭷๹䡻乽", a_),
						45
					},
					{
						ClipboardData.b("ٵ᭷๹养䩽끿", a_),
						46
					},
					{
						ClipboardData.b("ٵ᭷๹䡻䭽", a_),
						47
					},
					{
						ClipboardData.b("ٵ᭷๹养䩽땿", a_),
						48
					},
					{
						ClipboardData.b("ٵ᭷๹䥻乽", a_),
						49
					},
					{
						ClipboardData.b("ٵ᭷๹养䭽끿", a_),
						50
					},
					{
						ClipboardData.b("ٵ᭷๹䥻䭽", a_),
						51
					},
					{
						ClipboardData.b("ٵ᭷๹养䭽땿", a_),
						52
					},
					{
						ClipboardData.b("ٵ᭷๹䩻乽", a_),
						53
					},
					{
						ClipboardData.b("ٵ᭷๹养䡽끿", a_),
						54
					},
					{
						ClipboardData.b("ٵ᭷๹䩻䱽", a_),
						55
					},
					{
						ClipboardData.b("ٵ᭷๹养䡽뉿", a_),
						56
					},
					{
						ClipboardData.b("ٵ᭷๹䩻䭽", a_),
						57
					},
					{
						ClipboardData.b("ٵ᭷๹养䡽땿", a_),
						58
					},
					{
						ClipboardData.b("ٵ᭷๹䭻乽", a_),
						59
					},
					{
						ClipboardData.b("ٵ᭷๹养䥽끿", a_),
						60
					},
					{
						ClipboardData.b("ٵ᭷๹䭻䭽", a_),
						61
					},
					{
						ClipboardData.b("ٵ᭷๹养䥽땿", a_),
						62
					},
					{
						ClipboardData.b("ٵ᭷๹䑻乽", a_),
						63
					},
					{
						ClipboardData.b("ٵ᭷๹养䙽끿", a_),
						64
					},
					{
						ClipboardData.b("ٵ᭷๹䑻䭽", a_),
						65
					},
					{
						ClipboardData.b("ٵ᭷๹养䙽땿", a_),
						66
					},
					{
						ClipboardData.b("ٵ᭷๹䑻䥽", a_),
						67
					},
					{
						ClipboardData.b("ٵ᭷๹养䙽띿", a_),
						68
					},
					{
						ClipboardData.b("ٵ᭷๹䕻乽", a_),
						69
					},
					{
						ClipboardData.b("ٵ᭷๹养䝽끿", a_),
						70
					},
					{
						ClipboardData.b("ٵ᭷๹䕻䭽", a_),
						71
					},
					{
						ClipboardData.b("ٵ᭷๹养䝽땿", a_),
						72
					}
				};
				num = 1;
				continue;
			case 7:
				num = 2;
				continue;
			case 8:
				goto IL_7C;
			case 9:
				if (spr᧓.\u17C6 == null)
				{
					num = 6;
					continue;
				}
				goto IL_89B;
			}
			if (A_0 != null)
			{
				num = 8;
				continue;
			}
			return TextureStyle.TextureNone;
			IL_7C:
			num = 9;
			continue;
			IL_89B:
			num = 4;
		}
		return TextureStyle.Texture45Percent;
		IL_5E:
		if (true)
		{
		}
		return TextureStyle.TextureNone;
		IL_74A:
		if (false)
		{
		}
		return TextureStyle.Texture60Percent;
		IL_8DA:
		return TextureStyle.TextureNone;
	}

	// Token: 0x060034D0 RID: 13520 RVA: 0x00308138 File Offset: 0x00307138
	internal static string ᜀ(TextureStyle A_0, bool A_1)
	{
		int a_ = 13;
		for (;;)
		{
			int num = 28;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!A_1)
					{
						num = 54;
						continue;
					}
					goto IL_BFF;
				case 1:
					goto IL_EFD;
				case 2:
					if (!A_1)
					{
						num = 92;
						continue;
					}
					goto IL_920;
				case 3:
					goto IL_48C;
				case 4:
					goto IL_3BF;
				case 5:
					if (!A_1)
					{
						num = 86;
						continue;
					}
					goto IL_4DC;
				case 6:
					if (!A_1)
					{
						num = 23;
						continue;
					}
					goto IL_8E4;
				case 7:
					if (!A_1)
					{
						num = 80;
						continue;
					}
					goto IL_F63;
				case 8:
					goto IL_91B;
				case 9:
					if (A_0 != TextureStyle.TextureNil)
					{
						num = 53;
						continue;
					}
					goto IL_5D8;
				case 10:
					goto IL_ACA;
				case 11:
					if (!A_1)
					{
						num = 30;
						continue;
					}
					goto IL_4BE;
				case 12:
					goto IL_E46;
				case 13:
					if (!A_1)
					{
						num = 69;
						continue;
					}
					goto IL_76D;
				case 14:
					if (!A_1)
					{
						num = 3;
						continue;
					}
					goto IL_E1B;
				case 15:
					goto IL_A4C;
				case 16:
					goto IL_BFA;
				case 17:
					goto IL_35F;
				case 18:
					if (!A_1)
					{
						num = 29;
						continue;
					}
					goto IL_EC3;
				case 19:
					goto IL_708;
				case 20:
					if (!A_1)
					{
						num = 4;
						continue;
					}
					goto IL_51B;
				case 21:
					if (!A_1)
					{
						num = 93;
						continue;
					}
					goto IL_7B8;
				case 22:
					goto IL_5D3;
				case 23:
					goto IL_A1C;
				case 24:
					goto IL_F8E;
				case 25:
					if (!A_1)
					{
						num = 76;
						continue;
					}
					goto IL_4CD;
				case 26:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B19;
					default:
						goto IL_B60;
					}
					break;
				case 27:
					goto IL_D56;
				case 28:
					switch (A_0)
					{
					case TextureStyle.TextureNone:
						goto IL_976;
					case TextureStyle.TextureSolid:
						goto IL_54B;
					case TextureStyle.Texture5Percent:
						num = 36;
						continue;
					case TextureStyle.Texture10Percent:
						num = 55;
						continue;
					case TextureStyle.Texture20Percent:
						num = 78;
						continue;
					case TextureStyle.Texture25Percent:
						num = 72;
						continue;
					case TextureStyle.Texture30Percent:
						num = 37;
						continue;
					case TextureStyle.Texture40Percent:
						num = 79;
						continue;
					case TextureStyle.Texture50Percent:
						num = 57;
						continue;
					case TextureStyle.Texture60Percent:
						num = 2;
						continue;
					case TextureStyle.Texture70Percent:
						num = 13;
						continue;
					case TextureStyle.Texture75Percent:
						num = 95;
						continue;
					case TextureStyle.Texture80Percent:
						num = 56;
						continue;
					case TextureStyle.Texture90Percent:
						num = 6;
						continue;
					case TextureStyle.TextureDarkHorizontal:
						num = 52;
						continue;
					case TextureStyle.TextureDarkVertical:
						num = 0;
						continue;
					case TextureStyle.TextureDarkDiagonalDown:
						num = 70;
						continue;
					case TextureStyle.TextureDarkDiagonalUp:
						num = 33;
						continue;
					case TextureStyle.TextureDarkCross:
						num = 96;
						continue;
					case TextureStyle.TextureDarkDiagonalCross:
						num = 47;
						continue;
					case TextureStyle.TextureHorizontal:
						num = 51;
						continue;
					case TextureStyle.TextureVertical:
						num = 64;
						continue;
					case TextureStyle.TextureDiagonalDown:
						num = 18;
						continue;
					case TextureStyle.TextureDiagonalUp:
						num = 25;
						continue;
					case TextureStyle.TextureCross:
						num = 71;
						continue;
					case TextureStyle.TextureDiagonalCross:
						num = 20;
						continue;
					case (TextureStyle)26:
					case (TextureStyle)27:
					case (TextureStyle)28:
					case (TextureStyle)29:
					case (TextureStyle)30:
					case (TextureStyle)31:
					case (TextureStyle)32:
					case (TextureStyle)33:
					case (TextureStyle)34:
						goto IL_FDE;
					case TextureStyle.Texture2Pt5Percent:
						num = 102;
						continue;
					case TextureStyle.Texture7Pt5Percent:
						num = 94;
						continue;
					case TextureStyle.Texture12Pt5Percent:
						num = 11;
						continue;
					case TextureStyle.Texture15Percent:
						num = 58;
						continue;
					case TextureStyle.Texture17Pt5Percent:
						num = 42;
						continue;
					case TextureStyle.Texture22Pt5Percent:
						num = 48;
						continue;
					case TextureStyle.Texture27Pt5Percent:
						num = 97;
						continue;
					case TextureStyle.Texture32Pt5Percent:
						num = 61;
						continue;
					case TextureStyle.Texture35Percent:
						num = 14;
						continue;
					case TextureStyle.Texture37Pt5Percent:
						num = 35;
						continue;
					case TextureStyle.Texture42Pt5Percent:
						num = 5;
						continue;
					case TextureStyle.Texture45Percent:
						num = 101;
						continue;
					case TextureStyle.Texture47Pt5Percent:
						num = 34;
						continue;
					case TextureStyle.Texture52Pt5Percent:
						num = 63;
						continue;
					case TextureStyle.Texture55Percent:
						num = 75;
						continue;
					case TextureStyle.Texture57Pt5Percent:
						num = 91;
						continue;
					case TextureStyle.Texture62Pt5Percent:
						num = 21;
						continue;
					case TextureStyle.Texture65Percent:
						num = 7;
						continue;
					case TextureStyle.Texture67Pt5Percent:
						num = 104;
						continue;
					case TextureStyle.Texture72Pt5Percent:
						num = 68;
						continue;
					case TextureStyle.Texture77Pt5Percent:
						num = 87;
						continue;
					case TextureStyle.Texture82Pt5Percent:
						num = 32;
						continue;
					case TextureStyle.Texture85Percent:
						num = 59;
						continue;
					case TextureStyle.Texture87Pt5Percent:
						num = 77;
						continue;
					case TextureStyle.Texture92Pt5Percent:
						num = 84;
						continue;
					case TextureStyle.Texture95Percent:
						num = 41;
						continue;
					case TextureStyle.Texture97Pt5Percent:
						num = 99;
						continue;
					default:
						num = 39;
						continue;
					}
					break;
				case 29:
					goto IL_F22;
				case 30:
					goto IL_E16;
				case 31:
					goto IL_4B9;
				case 32:
					if (!A_1)
					{
						num = 49;
						continue;
					}
					goto IL_BC0;
				case 33:
					if (!A_1)
					{
						num = 44;
						continue;
					}
					goto IL_EB4;
				case 34:
					if (!A_1)
					{
						num = 90;
						continue;
					}
					goto IL_9F1;
				case 35:
					if (!A_1)
					{
						num = 16;
						continue;
					}
					goto IL_77C;
				case 36:
					if (!A_1)
					{
						num = 40;
						continue;
					}
					goto IL_BA2;
				case 37:
					if (!A_1)
					{
						num = 10;
						continue;
					}
					goto IL_78B;
				case 38:
					goto IL_CB4;
				case 39:
					num = 9;
					continue;
				case 40:
					goto IL_516;
				case 41:
					if (!A_1)
					{
						num = 73;
						continue;
					}
					goto IL_8D5;
				case 42:
					if (!A_1)
					{
						num = 65;
						continue;
					}
					goto IL_6B0;
				case 43:
					goto IL_861;
				case 44:
					goto IL_882;
				case 45:
					goto IL_DD4;
				case 46:
					goto IL_D86;
				case 47:
					if (!A_1)
					{
						num = 17;
						continue;
					}
					goto IL_605;
				case 48:
					if (!A_1)
					{
						num = 26;
						continue;
					}
					goto IL_A90;
				case 49:
					goto IL_DF5;
				case 50:
					goto IL_630;
				case 51:
					if (!A_1)
					{
						num = 22;
						continue;
					}
					goto IL_422;
				case 52:
					if (!A_1)
					{
						num = 100;
						continue;
					}
					goto IL_FB1;
				case 53:
					num = 74;
					continue;
				case 54:
					goto IL_B9D;
				case 55:
					if (!A_1)
					{
						num = 24;
						continue;
					}
					goto IL_4EB;
				case 56:
					if (!A_1)
					{
						num = 19;
						continue;
					}
					goto IL_F54;
				case 57:
					if (!A_1)
					{
						num = 103;
						continue;
					}
					goto IL_C4A;
				case 58:
					if (!A_1)
					{
						num = 15;
						continue;
					}
					goto IL_F45;
				case 59:
					if (!A_1)
					{
						goto IL_B19;
					}
					goto IL_ADE;
				case 60:
					goto IL_7B6;
				case 61:
					if (!A_1)
					{
						num = 105;
						continue;
					}
					goto IL_E4B;
				case 62:
					goto IL_66F;
				case 63:
					if (!A_1)
					{
						num = 81;
						continue;
					}
					goto IL_FA2;
				case 64:
					if (!A_1)
					{
						num = 62;
						continue;
					}
					goto IL_6DD;
				case 65:
					goto IL_94B;
				case 66:
					goto IL_38F;
				case 67:
					goto IL_C93;
				case 68:
					if (!A_1)
					{
						num = 66;
						continue;
					}
					goto IL_7C7;
				case 69:
					goto IL_840;
				case 70:
					if (!A_1)
					{
						num = 98;
						continue;
					}
					goto IL_C68;
				case 71:
					if (!A_1)
					{
						num = 43;
						continue;
					}
					goto IL_683;
				case 72:
					if (!A_1)
					{
						num = 8;
						continue;
					}
					goto IL_8C6;
				case 73:
					goto IL_B45;
				case 74:
					goto IL_40E;
				case 75:
					if (!A_1)
					{
						num = 27;
						continue;
					}
					goto IL_E5A;
				case 76:
					goto IL_D35;
				case 77:
					if (!A_1)
					{
						num = 31;
						continue;
					}
					goto IL_8F3;
				case 78:
					if (!A_1)
					{
						num = 82;
						continue;
					}
					goto IL_70D;
				case 79:
					if (!A_1)
					{
						num = 60;
						continue;
					}
					goto IL_3F4;
				case 80:
					goto IL_738;
				case 81:
					goto IL_A6D;
				case 82:
					goto IL_585;
				case 83:
					goto IL_CD5;
				case 84:
					if (!A_1)
					{
						num = 45;
						continue;
					}
					goto IL_F36;
				case 85:
					goto IL_B24;
				case 86:
					goto IL_336;
				case 87:
					if (!A_1)
					{
						num = 12;
						continue;
					}
					goto IL_D5B;
				case 88:
					goto IL_9A1;
				case 89:
					goto IL_3E0;
				case 90:
					goto IL_6AE;
				case 91:
					if (!A_1)
					{
						num = 89;
						continue;
					}
					goto IL_E96;
				case 92:
					goto IL_546;
				case 93:
					goto IL_D05;
				case 94:
					if (!A_1)
					{
						num = 38;
						continue;
					}
					goto IL_ACF;
				case 95:
					if (!A_1)
					{
						num = 88;
						continue;
					}
					goto IL_806;
				case 96:
					if (!A_1)
					{
						num = 67;
						continue;
					}
					goto IL_8B7;
				case 97:
					if (!A_1)
					{
						num = 50;
						continue;
					}
					goto IL_815;
				case 98:
					goto IL_651;
				case 99:
					if (!A_1)
					{
						num = 46;
						continue;
					}
					goto IL_C3B;
				case 100:
					goto IL_8B2;
				case 101:
					if (!A_1)
					{
						num = 1;
						continue;
					}
					goto IL_9A6;
				case 102:
					if (!A_1)
					{
						num = 106;
						continue;
					}
					goto IL_AFC;
				case 103:
					goto IL_7F2;
				case 104:
					if (!A_1)
					{
						num = 83;
						continue;
					}
					goto IL_AED;
				case 105:
					goto IL_46B;
				case 106:
					goto IL_2F7;
				}
				break;
				IL_B19:
				num = 85;
			}
		}
		IL_2F7:
		return ClipboardData.b("ͲᙴͶ呸乺", a_);
		IL_336:
		if (true)
		{
		}
		return ClipboardData.b("ͲᙴͶ呸佺䵼", a_);
		IL_35F:
		return ClipboardData.b("ᝲᱴᙶṸ噺Ṽൾ", a_);
		IL_38F:
		return ClipboardData.b("ͲᙴͶ呸䱺䵼", a_);
		IL_3BF:
		return ClipboardData.b("ݲᵴṶ᝸噺᥼ᙾꢄﮈﺌﲎ", a_);
		IL_3E0:
		return ClipboardData.b("ͲᙴͶ呸乺䡼", a_);
		IL_3F4:
		return ClipboardData.b("ͲᙴͶ䵸䭺", a_);
		IL_40E:
		goto IL_FDE;
		IL_422:
		return ClipboardData.b("ݲᵴṶ᝸㍺ቼൾﮀ킂ﮊ", a_);
		IL_46B:
		return ClipboardData.b("ͲᙴͶ呸䡺䡼", a_);
		IL_48C:
		return ClipboardData.b("ͲᙴͶ呸䡺䡼", a_);
		IL_4B9:
		return ClipboardData.b("ͲᙴͶ呸䍺䩼", a_);
		IL_4BE:
		return ClipboardData.b("ͲᙴͶ䡸䥺", a_);
		IL_4CD:
		return ClipboardData.b("ݲᵴṶ᝸㽺ᑼṾ킂ﮊ", a_);
		IL_4DC:
		return ClipboardData.b("ͲᙴͶ䵸䭺", a_);
		IL_4EB:
		return ClipboardData.b("ͲᙴͶ䡸䭺", a_);
		IL_516:
		return ClipboardData.b("ͲᙴͶ呸乺", a_);
		IL_51B:
		return ClipboardData.b("ݲᵴṶ᝸㽺ᑼṾ삂愈", a_);
		IL_546:
		return ClipboardData.b("ͲᙴͶ呸䵺䵼", a_);
		IL_54B:
		return ClipboardData.b("rᩴ᭶ၸὺ", a_);
		IL_585:
		return ClipboardData.b("ͲᙴͶ呸䥺䵼", a_);
		IL_5D3:
		return ClipboardData.b("ݲᵴṶ᝸噺ᕼၾ廬ꢄﶈ力ﾎ", a_);
		IL_5D8:
		return ClipboardData.b("ᵲᱴ᭶", a_);
		IL_605:
		return ClipboardData.b("ᝲᱴᙶṸ㡺ོၾ", a_);
		IL_630:
		return ClipboardData.b("ͲᙴͶ呸䡺䵼", a_);
		IL_651:
		return ClipboardData.b("ŲၴŶᱸॺ๼᩾검Ꚋﺌﮎ朗", a_);
		IL_66F:
		return ClipboardData.b("ݲᵴṶ᝸噺୼᩾ꢄﶈ力ﾎ", a_);
		IL_683:
		return ClipboardData.b("ݲᵴṶ᝸㍺ቼൾﮀ삂愈", a_);
		IL_6AE:
		return ClipboardData.b("ͲᙴͶ呸佺䡼", a_);
		IL_6B0:
		return ClipboardData.b("ͲᙴͶ䭸䭺", a_);
		IL_6DD:
		return ClipboardData.b("ݲᵴṶ᝸⵺᡼ൾ킂ﮊ", a_);
		IL_708:
		return ClipboardData.b("ͲᙴͶ呸䍺䵼", a_);
		IL_70D:
		return ClipboardData.b("ͲᙴͶ䭸䭺", a_);
		IL_738:
		return ClipboardData.b("ͲᙴͶ呸䵺䡼", a_);
		IL_76D:
		return ClipboardData.b("ͲᙴͶ乸䭺", a_);
		IL_77C:
		return ClipboardData.b("ͲᙴͶ䩸䱺", a_);
		IL_78B:
		return ClipboardData.b("ͲᙴͶ䩸䭺", a_);
		IL_7B6:
		return ClipboardData.b("ͲᙴͶ呸佺䵼", a_);
		IL_7B8:
		return ClipboardData.b("ͲᙴͶ佸䥺", a_);
		IL_7C7:
		return ClipboardData.b("ͲᙴͶ乸䭺", a_);
		IL_7F2:
		return ClipboardData.b("ͲᙴͶ呸乺䵼", a_);
		IL_806:
		return ClipboardData.b("ͲᙴͶ乸乺", a_);
		IL_815:
		return ClipboardData.b("ͲᙴͶ䩸䭺", a_);
		IL_840:
		return ClipboardData.b("ͲᙴͶ呸䱺䵼", a_);
		IL_861:
		return ClipboardData.b("ݲᵴṶ᝸噺ᕼၾ廬ꢄﮈﺌﲎ", a_);
		IL_882:
		return ClipboardData.b("ᝲᱴᙶṸ噺๼୾", a_);
		IL_8B2:
		return ClipboardData.b("᭲ᩴն͸噺๼୾", a_);
		IL_8B7:
		return ClipboardData.b("᭲ᩴն͸㡺ོၾ", a_);
		IL_8C6:
		return ClipboardData.b("ͲᙴͶ䭸乺", a_);
		IL_8D5:
		return ClipboardData.b("ͲᙴͶ䁸乺", a_);
		IL_8E4:
		return ClipboardData.b("ͲᙴͶ䁸䭺", a_);
		IL_8F3:
		return ClipboardData.b("ͲᙴͶ䅸䱺", a_);
		IL_91B:
		return ClipboardData.b("ͲᙴͶ呸䥺䡼", a_);
		IL_920:
		return ClipboardData.b("ͲᙴͶ佸䭺", a_);
		IL_94B:
		return ClipboardData.b("ͲᙴͶ呸䥺䵼", a_);
		IL_976:
		return ClipboardData.b("ၲᥴቶᡸॺ", a_);
		IL_9A1:
		return ClipboardData.b("ͲᙴͶ呸䱺䡼", a_);
		IL_9A6:
		return ClipboardData.b("ͲᙴͶ䵸乺", a_);
		IL_9F1:
		return ClipboardData.b("ͲᙴͶ䵸乺", a_);
		IL_A1C:
		return ClipboardData.b("ͲᙴͶ呸䉺䵼", a_);
		IL_A4C:
		return ClipboardData.b("ͲᙴͶ呸䩺䡼", a_);
		IL_A6D:
		return ClipboardData.b("ͲᙴͶ呸乺䵼", a_);
		IL_A90:
		return ClipboardData.b("ͲᙴͶ䭸乺", a_);
		IL_ACA:
		return ClipboardData.b("ͲᙴͶ呸䡺䵼", a_);
		IL_ACF:
		return ClipboardData.b("ͲᙴͶ䡸䭺", a_);
		IL_ADE:
		return ClipboardData.b("ͲᙴͶ䅸乺", a_);
		IL_AED:
		return ClipboardData.b("ͲᙴͶ佸乺", a_);
		IL_AFC:
		return ClipboardData.b("ͲᙴͶ䱸", a_);
		IL_B24:
		return ClipboardData.b("ͲᙴͶ呸䍺䡼", a_);
		IL_B45:
		return ClipboardData.b("ͲᙴͶ呸䉺䡼", a_);
		IL_B60:
		if (false)
		{
		}
		return ClipboardData.b("ͲᙴͶ呸䥺䡼", a_);
		IL_B9D:
		return ClipboardData.b("ղၴն൸噺๼୾", a_);
		IL_BA2:
		return ClipboardData.b("ͲᙴͶ䱸", a_);
		IL_BC0:
		return ClipboardData.b("ͲᙴͶ䅸乺", a_);
		IL_BFA:
		return ClipboardData.b("ͲᙴͶ呸䡺䩼", a_);
		IL_BFF:
		return ClipboardData.b("ղၴն൸⡺ॼൾ", a_);
		IL_C3B:
		return ClipboardData.b("ͲᙴͶ䁸乺", a_);
		IL_C4A:
		return ClipboardData.b("ͲᙴͶ䱸䭺", a_);
		IL_C68:
		return ClipboardData.b("ŲၴŶᱸॺ๼᩾얀\uda88ﾊﾌ", a_);
		IL_C93:
		return ClipboardData.b("᭲ᩴն͸噺Ṽൾ", a_);
		IL_CB4:
		return ClipboardData.b("ͲᙴͶ呸䩺䵼", a_);
		IL_CD5:
		return ClipboardData.b("ͲᙴͶ呸䵺䡼", a_);
		IL_D05:
		return ClipboardData.b("ͲᙴͶ呸䵺佼", a_);
		IL_D35:
		return ClipboardData.b("ݲᵴṶ᝸噺᥼ᙾꢄﶈ力ﾎ", a_);
		IL_D56:
		return ClipboardData.b("ͲᙴͶ呸乺䡼", a_);
		IL_D5B:
		return ClipboardData.b("ͲᙴͶ䅸䭺", a_);
		IL_D86:
		return ClipboardData.b("ͲᙴͶ呸䉺䡼", a_);
		IL_DD4:
		return ClipboardData.b("ͲᙴͶ呸䉺䵼", a_);
		IL_DF5:
		return ClipboardData.b("ͲᙴͶ呸䍺䡼", a_);
		IL_E16:
		return ClipboardData.b("ͲᙴͶ呸䩺佼", a_);
		IL_E1B:
		return ClipboardData.b("ͲᙴͶ䩸乺", a_);
		IL_E46:
		return ClipboardData.b("ͲᙴͶ呸䍺䵼", a_);
		IL_E4B:
		return ClipboardData.b("ͲᙴͶ䩸乺", a_);
		IL_E5A:
		return ClipboardData.b("ͲᙴͶ䱸乺", a_);
		IL_E96:
		return ClipboardData.b("ͲᙴͶ䱸乺", a_);
		IL_EB4:
		return ClipboardData.b("ᝲᱴᙶṸ⡺ॼൾ", a_);
		IL_EC3:
		return ClipboardData.b("ݲᵴṶ᝸⥺᡼ॾ춈슐ﺖﺚ", a_);
		IL_EFD:
		return ClipboardData.b("ͲᙴͶ呸佺䡼", a_);
		IL_F22:
		return ClipboardData.b("ݲᵴṶ᝸噺ོ᩾Ꚋ뢔쒠", a_);
		IL_F36:
		return ClipboardData.b("ͲᙴͶ䁸䭺", a_);
		IL_F45:
		return ClipboardData.b("ͲᙴͶ䡸乺", a_);
		IL_F54:
		return ClipboardData.b("ͲᙴͶ䅸䭺", a_);
		IL_F63:
		return ClipboardData.b("ͲᙴͶ佸乺", a_);
		IL_F8E:
		return ClipboardData.b("ͲᙴͶ呸䩺䵼", a_);
		IL_FA2:
		return ClipboardData.b("ͲᙴͶ䱸䭺", a_);
		IL_FB1:
		return ClipboardData.b("᭲ᩴն͸⡺ॼൾ", a_);
		IL_FDE:
		return ClipboardData.b("ᵲᱴ᭶", a_);
	}

	// Token: 0x060034D1 RID: 13521 RVA: 0x00309134 File Offset: 0x00308134
	internal static TextOrientation ᜀ(string A_0)
	{
		int a_ = 18;
		int num = 7;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 6;
				continue;
			case 1:
				num = 5;
				continue;
			case 2:
				return TextOrientation.Horizontal;
			case 3:
				goto IL_264;
			case 4:
				num = 2;
				continue;
			case 5:
			{
				int num2;
				switch (num2)
				{
				case 0:
				case 1:
				case 2:
					return TextOrientation.Upward;
				case 3:
				case 4:
				case 5:
					return TextOrientation.Horizontal;
				case 6:
				case 7:
				case 8:
					return TextOrientation.HorizontalRotatedFarEast;
				case 9:
				case 10:
				case 11:
					return TextOrientation.VerticalFarEast;
				case 12:
				case 13:
				case 14:
					goto IL_200;
				default:
					num = 4;
					continue;
				}
				break;
			}
			case 6:
				if (spr᧓.\u17C7 == null)
				{
					num = 9;
					continue;
				}
				goto IL_264;
			case 8:
			{
				int num2;
				if (spr᧓.\u17C7.TryGetValue(A_0, out num2))
				{
					num = 1;
					continue;
				}
				return TextOrientation.Horizontal;
			}
			case 9:
				spr᧓.\u17C7 = new Dictionary<string, int>(15)
				{
					{
						ClipboardData.b("ᑷࡹ", a_),
						0
					},
					{
						ClipboardData.b("᩷๹ほ౽", a_),
						1
					},
					{
						ClipboardData.b("᩷๹养ች", a_),
						2
					},
					{
						ClipboardData.b("౷᡹", a_),
						3
					},
					{
						ClipboardData.b("ᑷࡹ⡻ᱽ", a_),
						4
					},
					{
						ClipboardData.b("ᑷࡹ养੽", a_),
						5
					},
					{
						ClipboardData.b("౷᡹⩻", a_),
						6
					},
					{
						ClipboardData.b("ᑷࡹ⡻ᱽ홿", a_),
						7
					},
					{
						ClipboardData.b("ᑷࡹ养੽꾁", a_),
						8
					},
					{
						ClipboardData.b("੷ᙹ⩻", a_),
						9
					},
					{
						ClipboardData.b("౷᡹⹻ች홿", a_),
						10
					},
					{
						ClipboardData.b("౷᡹养౽꾁", a_),
						11
					},
					{
						ClipboardData.b("੷ᙹ", a_),
						12
					},
					{
						ClipboardData.b("౷᡹⹻ች", a_),
						13
					},
					{
						ClipboardData.b("౷᡹养౽", a_),
						14
					}
				};
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_200;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			}
			if (A_0 != null)
			{
				num = 0;
				continue;
			}
			return TextOrientation.Horizontal;
			IL_264:
			num = 8;
		}
		return TextOrientation.HorizontalRotatedFarEast;
		IL_200:
		if (true)
		{
		}
		return TextOrientation.Downward;
	}

	// Token: 0x060034D2 RID: 13522 RVA: 0x003093EC File Offset: 0x003083EC
	internal static string ᜀ(TextOrientation A_0, bool A_1)
	{
		int a_ = 17;
		for (;;)
		{
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_12E;
				case 1:
					if (!A_1)
					{
						num = 0;
						continue;
					}
					goto IL_1C2;
				case 2:
					goto IL_E3;
				case 3:
					if (!A_1)
					{
						num = 9;
						continue;
					}
					goto IL_1B3;
				case 4:
					goto IL_FE;
				case 5:
					if (true)
					{
					}
					if (!A_1)
					{
						num = 10;
						continue;
					}
					goto IL_103;
				case 6:
					goto IL_17A;
				case 7:
					switch (A_0)
					{
					case TextOrientation.Horizontal:
						num = 3;
						continue;
					case TextOrientation.Downward:
						num = 5;
						continue;
					case (TextOrientation)2:
						goto IL_1D1;
					case TextOrientation.Upward:
						num = 12;
						continue;
					case TextOrientation.HorizontalRotatedFarEast:
						num = 1;
						continue;
					case TextOrientation.VerticalFarEast:
						num = 11;
						continue;
					default:
						num = 8;
						continue;
					}
					break;
				case 8:
					num = 6;
					continue;
				case 9:
					goto IL_195;
				case 10:
					goto IL_C8;
				case 11:
					if (!A_1)
					{
						num = 4;
						continue;
					}
					goto IL_151;
				case 12:
					if (!A_1)
					{
						num = 2;
						continue;
					}
					goto IL_98;
				}
				break;
			}
		}
		IL_7A:
		return ClipboardData.b("Ͷ᭸噺ོ፾검", a_);
		IL_98:
		return ClipboardData.b("ᕶ൸㝺ོ", a_);
		IL_C8:
		return ClipboardData.b("Ͷ᭸噺ོ፾", a_);
		IL_E3:
		return ClipboardData.b("ᕶ൸噺ᅼൾ", a_);
		IL_FE:
		goto IL_7A;
		IL_103:
		return ClipboardData.b("Ͷ᭸⥺ᅼ", a_);
		IL_12E:
		return ClipboardData.b("᭶୸噺ॼᵾ검", a_);
		IL_151:
		return ClipboardData.b("Ͷ᭸⥺ᅼ⥾", a_);
		IL_17A:
		goto IL_1D1;
		IL_195:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_7A;
		default:
			if (false)
			{
			}
			return ClipboardData.b("᭶୸噺ॼᵾ", a_);
		}
		IL_1B3:
		return ClipboardData.b("᭶୸⽺ὼ", a_);
		IL_1C2:
		return ClipboardData.b("᭶୸⽺ὼ⥾", a_);
		IL_1D1:
		return "";
	}

	// Token: 0x060034D3 RID: 13523 RVA: 0x003095DC File Offset: 0x003085DC
	internal static StylePaneSortMethod ᜀ(int A_0)
	{
		for (;;)
		{
			IL_14:
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_56;
				case 1:
					switch (A_0)
					{
					case 0:
						return StylePaneSortMethod.Name;
					case 1:
						goto IL_44;
					case 2:
						return StylePaneSortMethod.Font;
					case 3:
						return StylePaneSortMethod.BasedOn;
					case 4:
						return StylePaneSortMethod.StyleType;
					default:
						num = 2;
						continue;
					}
					break;
				case 2:
					num = 0;
					continue;
				}
				goto IL_14;
			}
			IL_56:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			}
			goto Block_2;
		}
		return StylePaneSortMethod.BasedOn;
		IL_44:
		if (true)
		{
		}
		return StylePaneSortMethod.Default;
		Block_2:
		if (false)
		{
		}
		return StylePaneSortMethod.Default;
	}

	// Token: 0x060034D4 RID: 13524 RVA: 0x00309670 File Offset: 0x00308670
	internal static int ᜀ(StylePaneSortMethod A_0)
	{
		for (;;)
		{
			IL_14:
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_58:
				num = 1;
				break;
			default:
				if (false)
				{
				}
				num = 2;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_80;
				case 1:
					if (true)
					{
					}
					num = 0;
					continue;
				case 2:
					goto IL_3A;
				}
				goto IL_14;
			}
			IL_3A:
			switch (A_0)
			{
			case StylePaneSortMethod.Name:
				return 0;
			case StylePaneSortMethod.Priority:
			case StylePaneSortMethod.Default:
				return 1;
			case StylePaneSortMethod.Font:
				return 2;
			case StylePaneSortMethod.BasedOn:
				return 3;
			case StylePaneSortMethod.StyleType:
				return 4;
			}
			goto IL_58;
		}
		return 3;
		IL_80:
		return 1;
	}

	// Token: 0x060034D5 RID: 13525 RVA: 0x00309704 File Offset: 0x00308704
	static sprḕ()
	{
		int a_ = 0;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		sprḕ.ᜀ = new Hashtable();
		sprḕ.ᜁ = new Hashtable();
		sprḕ.ᜂ = new Hashtable();
		sprḕ.ᜃ = new Hashtable();
		sprḕ.ᜄ = new Hashtable();
		sprḕ.ᜅ = new Hashtable();
		sprḕ.ᜆ = new Hashtable();
		sprḕ.ᜇ = new Hashtable();
		sprḕ.ᜈ = new Hashtable();
		sprḕ.ᜉ = new Hashtable();
		sprḕ.ᜊ = new Hashtable();
		sprḕ.ᜋ = new Hashtable();
		sprḕ.ᜌ = new Hashtable();
		sprḕ.\u170D = new Hashtable();
		sprḕ.ᜎ = new Hashtable();
		sprḕ.ᜏ = new Hashtable();
		sprḕ.ᜐ = new Hashtable();
		sprḕ.ᜑ = new Hashtable();
		sprḕ.\u1712 = new Hashtable();
		sprḕ.\u1713 = new Hashtable();
		spr\u19FA.ᜁ(new object[]
		{
			ClipboardData.b("ɥ൧౩൫᭭ᱯٱ", a_),
			FontPitch.Default,
			ClipboardData.b("eŧቩ५੭", a_),
			FontPitch.Fixed,
			ClipboardData.b("ၥ१ᡩի཭ቯṱᅳ", a_),
			FontPitch.Variable
		}, sprḕ.ᜀ, sprḕ.ᜁ);
		spr\u19FA.ᜁ(new object[]
		{
			ClipboardData.b("୥१ᡩ୫ݭṯ", a_),
			RelativeHeight.Margin,
			ClipboardData.b("ᙥ१൩५", a_),
			RelativeHeight.Page,
			ClipboardData.b("॥ᵧṩ५ᱭ嵯άᕳѵί፹ቻ卽", a_),
			RelativeHeight.OutsideMargin,
			ClipboardData.b("ཥ٧ѩ५ᱭ嵯άᕳѵί፹ቻ卽", a_),
			RelativeHeight.InsideMargin,
			ClipboardData.b("ብݧᩩ䅫ͭᅯq፳ήᙷ坹ᵻ౽", a_),
			RelativeHeight.TopMargin,
			ClipboardData.b("ѥݧṩᡫŭᵯ影ᥳ᝵੷ᵹᕻၽ굿", a_),
			RelativeHeight.BottomMargin
		}, sprḕ.ᜂ, sprḕ.ᜃ);
		spr\u19FA.ᜁ(new object[]
		{
			ClipboardData.b("୥१ᡩ୫ݭṯ", a_),
			RelativeWidth.Margin,
			ClipboardData.b("ᙥ१൩५", a_),
			RelativeWidth.Page,
			ClipboardData.b("॥ᵧṩ५ᱭ嵯άᕳѵί፹ቻ卽", a_),
			RelativeWidth.OutsideMargin,
			ClipboardData.b("ཥ٧ѩ५ᱭ嵯άᕳѵί፹ቻ卽", a_),
			RelativeWidth.InsideMargin,
			ClipboardData.b("੥൧౩ᡫ䍭ᵯ፱ٳᅵᅷᑹ养ώ", a_),
			RelativeWidth.LeftMargin,
			ClipboardData.b("ᑥŧ൩ѫᩭ嵯άᕳѵί፹ቻ卽", a_),
			RelativeWidth.RightMargin
		}, sprḕ.ᜄ, sprḕ.ᜅ);
		spr\u19FA.ᜁ(new object[]
		{
			ClipboardData.b("୥१ᡩ୫ݭṯ", a_),
			RelativeVerticalPosition.Margin,
			ClipboardData.b("ᙥ१൩५", a_),
			RelativeVerticalPosition.Page,
			ClipboardData.b("ብ൧ቩᡫ", a_),
			RelativeVerticalPosition.Paragraph,
			ClipboardData.b("੥ŧѩ५", a_),
			RelativeVerticalPosition.Line,
			ClipboardData.b("॥ᵧṩ५ᱭ嵯άᕳѵί፹ቻ卽", a_),
			RelativeVerticalPosition.OutsideMargin,
			ClipboardData.b("ཥ٧ѩ५ᱭ嵯άᕳѵί፹ቻ卽", a_),
			RelativeVerticalPosition.InsideMargin,
			ClipboardData.b("ብݧᩩ䅫ͭᅯq፳ήᙷ坹ᵻ౽", a_),
			RelativeVerticalPosition.TopMargin,
			ClipboardData.b("ѥݧṩᡫŭᵯ影ᥳ᝵੷ᵹᕻၽ굿", a_),
			RelativeVerticalPosition.BottomMargin
		}, sprḕ.ᜆ, sprḕ.ᜇ);
		spr\u19FA.ᜁ(new object[]
		{
			ClipboardData.b("୥१ᡩ୫ݭṯ", a_),
			RelativeHorizontalPosition.Margin,
			ClipboardData.b("ᙥ१൩५", a_),
			RelativeHorizontalPosition.Page,
			ClipboardData.b("ብ൧ቩᡫ", a_),
			RelativeHorizontalPosition.Column,
			ClipboardData.b("եg୩ṫ", a_),
			RelativeHorizontalPosition.Character,
			ClipboardData.b("॥ᵧṩ५ᱭ嵯άᕳѵί፹ቻ卽", a_),
			RelativeHorizontalPosition.OutsideMargin,
			ClipboardData.b("ཥ٧ѩ५ᱭ嵯άᕳѵί፹ቻ卽", a_),
			RelativeHorizontalPosition.InsideMargin,
			ClipboardData.b("੥൧౩ᡫ䍭ᵯ፱ٳᅵᅷᑹ养ώ", a_),
			RelativeHorizontalPosition.LeftMargin,
			ClipboardData.b("ᑥŧ൩ѫᩭ嵯άᕳѵί፹ቻ卽", a_),
			RelativeHorizontalPosition.RightMargin
		}, sprḕ.ᜈ, sprḕ.ᜉ);
		spr\u19FA.ᜁ(new object[]
		{
			ClipboardData.b("ݥ੧ᥩͫɭկٱᅳ", a_),
			HorizontalPosition.None,
			ClipboardData.b("੥൧౩ᡫ", a_),
			HorizontalPosition.Left,
			ClipboardData.b("ե൧ѩᡫ୭ɯ", a_),
			HorizontalPosition.Center,
			ClipboardData.b("ᑥŧ൩ѫᩭ", a_),
			HorizontalPosition.Right,
			ClipboardData.b("ཥ٧ᥩի੭ᕯ", a_),
			HorizontalPosition.Inside,
			ClipboardData.b("॥ᵧṩὫݭᑯ᝱", a_),
			HorizontalPosition.Outside
		}, sprḕ.ᜊ, sprḕ.ᜋ);
		spr\u19FA.ᜁ(new object[]
		{
			ClipboardData.b("ݥ੧ᥩͫɭկٱᅳ", a_),
			VerticalPosition.None,
			ClipboardData.b("ብݧᩩ", a_),
			VerticalPosition.Top,
			ClipboardData.b("ե൧ѩᡫ୭ɯ", a_),
			VerticalPosition.Center,
			ClipboardData.b("ѥݧṩᡫŭᵯ", a_),
			VerticalPosition.Bottom,
			ClipboardData.b("ཥ٧ᥩի੭ᕯ", a_),
			VerticalPosition.Inside,
			ClipboardData.b("॥ᵧṩὫݭᑯ᝱", a_),
			VerticalPosition.Outside
		}, sprḕ.ᜌ, sprḕ.\u170D);
		spr\u19FA.ᜁ(new object[]
		{
			ClipboardData.b("⑥ݧթݫ乭⑯᭱s᩵ᵷ", a_),
			StyleIdentifier.BookTitle,
			ClipboardData.b("ݥ٧ѩͫᩭᅯٱᵳ᥵ᙷ婹๻᭽", a_),
			StyleIdentifier.CommentReference,
			ClipboardData.b("≥൧౩൫᭭ᱯٱ味♵᥷ࡹᵻ᥽ꢇ첉", a_),
			StyleIdentifier.DefaultParagraphFont,
			ClipboardData.b("⍥էᩩѫ཭ͯ᭱ݳ", a_),
			StyleIdentifier.Emphasis,
			ClipboardData.b("ͥ٧๩ɫŭѯ᝱味ѵᵷᱹ᥻౽", a_),
			StyleIdentifier.EndnoteReference,
			ClipboardData.b("⁥ݧ٩kŭݯ᝱ၳ㹵ŷ੹᥻౽", a_),
			StyleIdentifier.FollowedHyperlink,
			ClipboardData.b("eݧթᡫmὯٱᅳ噵੷ό᩻᭽", a_),
			StyleIdentifier.FootnoteReference,
			ClipboardData.b("⹥㱧❩⁫乭ㅯᅱٳ᥵ᙷ͹ᅻ", a_),
			StyleIdentifier.HtmlAcronym,
			ClipboardData.b("⹥㱧❩⁫乭㍯᭱s፵", a_),
			StyleIdentifier.HtmlCite,
			ClipboardData.b("⹥㱧❩⁫乭㍯ᵱၳ፵", a_),
			StyleIdentifier.HtmlCode,
			ClipboardData.b("⹥㱧❩⁫乭㑯᝱ታήᙷ፹ࡻ᝽", a_),
			StyleIdentifier.HtmlDefinition,
			ClipboardData.b("⹥㱧❩⁫乭㭯᝱൳ᑵ᝷᭹๻᩽", a_),
			StyleIdentifier.HtmlKeyboard,
			ClipboardData.b("⹥㱧❩⁫乭⍯፱ᥳٵᑷό", a_),
			StyleIdentifier.HtmlSample,
			ClipboardData.b("⹥㱧❩⁫乭⑯ୱѳ፵ཷࡹᕻ੽", a_),
			StyleIdentifier.HtmlTypewriter,
			ClipboardData.b("⹥㱧❩⁫乭♯፱ٳή᥷᡹ၻ᭽", a_),
			StyleIdentifier.HtmlVariable,
			ClipboardData.b("⹥ᅧᩩ५ᱭᱯ᭱ᩳᵵ", a_),
			StyleIdentifier.Hyperlink,
			ClipboardData.b("⽥٧ṩ५mͯ᝱味㍵ᕷ੹ᑻώ", a_),
			StyleIdentifier.IntenseEmphasis,
			ClipboardData.b("⽥٧ṩ५mͯ᝱味⑵ᵷᱹ᥻౽", a_),
			StyleIdentifier.IntenseReference,
			ClipboardData.b("੥ŧѩ५乭ṯݱᥳᑵᵷࡹ", a_),
			StyleIdentifier.LineNumber,
			ClipboardData.b("ᙥ१൩५乭ṯݱᥳᑵᵷࡹ", a_),
			StyleIdentifier.PageNumber,
			ClipboardData.b("㙥ѧ୩ཫ୭ᡯᵱᡳትᵷࡹ屻⩽嬨", a_),
			StyleIdentifier.PlaceholderText,
			ClipboardData.b("㕥ᱧᡩͫmᝯ", a_),
			StyleIdentifier.Strong,
			ClipboardData.b("㕥ᵧࡩᡫɭᕯ剱ㅳ᭵ࡷቹᵻൽ", a_),
			StyleIdentifier.SubtleEmphasis,
			ClipboardData.b("㕥ᵧࡩᡫɭᕯ剱♳፵ṷό๻᭽", a_),
			StyleIdentifier.SubtleReference,
			ClipboardData.b("⑥१٩kŭὯᱱ味≵ᵷɹࡻ", a_),
			StyleIdentifier.BalloonText,
			ClipboardData.b("⑥ݧ๩ᕫ乭⑯᝱౳ɵ", a_),
			StyleIdentifier.BodyText,
			ClipboardData.b("⑥ݧ๩ᕫ乭⑯᝱౳ɵ塷䡹", a_),
			StyleIdentifier.BodyText2,
			ClipboardData.b("⑥ݧ๩ᕫ乭⑯᝱౳ɵ塷䥹", a_),
			StyleIdentifier.BodyText3,
			ClipboardData.b("⑥ݧ๩ᕫ乭⑯᝱౳ɵ塷㱹ᕻ౽ꒃ쾅", a_),
			StyleIdentifier.BodyText1I,
			ClipboardData.b("⑥ݧ๩ᕫ乭⑯᝱౳ɵ塷㱹ᕻ౽ꒃ쾅늑ꚓ", a_),
			StyleIdentifier.BodyText1I2,
			ClipboardData.b("⑥ݧ๩ᕫ乭⑯᝱౳ɵ塷㍹ቻ᩽", a_),
			StyleIdentifier.BodyTextInd,
			ClipboardData.b("⑥ݧ๩ᕫ乭⑯᝱౳ɵ塷㍹ቻ᩽ꚅ몇", a_),
			StyleIdentifier.BodyTextInd2,
			ClipboardData.b("⑥ݧ๩ᕫ乭⑯᝱౳ɵ塷㍹ቻ᩽ꚅ뮇", a_),
			StyleIdentifier.BodyTextInd3,
			ClipboardData.b("╥ѧթὫݭṯᕱ", a_),
			StyleIdentifier.Closing,
			ClipboardData.b("ݥ٧ѩͫᩭᅯٱᵳ᥵ᙷ婹ཻ୽ﲇ", a_),
			StyleIdentifier.CommentSubject,
			ClipboardData.b("ݥ٧ѩͫᩭᅯٱᵳ᥵ᙷ婹ࡻ᭽", a_),
			StyleIdentifier.CommentText,
			ClipboardData.b("≥१ṩ५", a_),
			StyleIdentifier.Date,
			ClipboardData.b("≥ݧ३ᥫͭᕯᱱs噵㕷᭹౻", a_),
			StyleIdentifier.DocumentMap,
			ClipboardData.b("⍥䕧ݩ൫ݭᱯ剱❳ήίᑹᵻ੽", a_),
			StyleIdentifier.EmailSignature,
			ClipboardData.b("ͥ٧๩ɫŭѯ᝱味ɵᵷɹࡻ", a_),
			StyleIdentifier.EndnoteText,
			ClipboardData.b("eݧթᡫ୭ɯ", a_),
			StyleIdentifier.Footer,
			ClipboardData.b("eݧթᡫmὯٱᅳ噵౷όѻ੽", a_),
			StyleIdentifier.FootnoteText,
			ClipboardData.b("๥൧୩࡫୭ɯ", a_),
			StyleIdentifier.Header,
			ClipboardData.b("๥൧୩࡫ݭṯᕱ味䝵", a_),
			StyleIdentifier.Heading1,
			ClipboardData.b("๥൧୩࡫ݭṯᕱ味䑵", a_),
			StyleIdentifier.Heading2,
			ClipboardData.b("๥൧୩࡫ݭṯᕱ味䕵", a_),
			StyleIdentifier.Heading3,
			ClipboardData.b("๥൧୩࡫ݭṯᕱ味䉵", a_),
			StyleIdentifier.Heading4,
			ClipboardData.b("๥൧୩࡫ݭṯᕱ味䍵", a_),
			StyleIdentifier.Heading5,
			ClipboardData.b("๥൧୩࡫ݭṯᕱ味䁵", a_),
			StyleIdentifier.Heading6,
			ClipboardData.b("๥൧୩࡫ݭṯᕱ味䅵", a_),
			StyleIdentifier.Heading7,
			ClipboardData.b("๥൧୩࡫ݭṯᕱ味乵", a_),
			StyleIdentifier.Heading8,
			ClipboardData.b("๥൧୩࡫ݭṯᕱ味併", a_),
			StyleIdentifier.Heading9,
			ClipboardData.b("⹥㱧❩⁫乭ㅯᙱၳѵᵷॹཻ", a_),
			StyleIdentifier.HtmlAddress,
			ClipboardData.b("⹥㱧❩⁫乭㉯ᵱsɵ᝷᝹屻ᅽꊁ슃慎", a_),
			StyleIdentifier.HtmlBottomOfForm,
			ClipboardData.b("⹥㱧❩⁫乭⁯qᅳၵ᝷ࡹᅻώ", a_),
			StyleIdentifier.HtmlPreformatted,
			ClipboardData.b("⹥㱧❩⁫乭⑯ᵱѳ噵᝷ᱹ屻㡽", a_),
			StyleIdentifier.HtmlTopOfForm,
			ClipboardData.b("⽥٧ṩ५mͯ᝱味❵൷ᕹࡻ᭽", a_),
			StyleIdentifier.IntenseQuote,
			ClipboardData.b("୥१३ṫŭ", a_),
			StyleIdentifier.Macro,
			ClipboardData.b("⭥൧ᥩὫ཭ᝯ᝱味㹵ᵷ᭹᡻᭽", a_),
			StyleIdentifier.MessageHeader,
			ClipboardData.b("ࡥݧṩ५乭ᡯ᝱ᕳትᅷᑹ᭻", a_),
			StyleIdentifier.NoteHeading,
			ClipboardData.b("㙥ѧ୩իm偯♱ᅳ๵౷", a_),
			StyleIdentifier.PlainText,
			ClipboardData.b("㝥ᵧթᡫ୭", a_),
			StyleIdentifier.Quote,
			ClipboardData.b("㕥१٩ᥫᩭᅯٱᵳ᥵ᙷ", a_),
			StyleIdentifier.Salutation,
			ClipboardData.b("㕥ŧ൩ɫ཭ѯݱٳ፵", a_),
			StyleIdentifier.Signature,
			ClipboardData.b("㕥ᵧࡩᡫݭѯṱᅳ", a_),
			StyleIdentifier.Subtitle,
			ClipboardData.b("㉥ŧṩk୭", a_),
			StyleIdentifier.Title,
			ClipboardData.b("⑥ŧࡩkݭὯᕱٳ᝵ࡷቹջ", a_),
			StyleIdentifier.Bibliography,
			ClipboardData.b("⑥ѧթཫխ偯♱ᅳ๵౷", a_),
			StyleIdentifier.BlockText,
			ClipboardData.b("ե१ᩩᡫݭὯᱱ", a_),
			StyleIdentifier.Caption,
			ClipboardData.b("ͥ٧ᱩ५ɭὯɱᅳ噵᥷ṹ᡻౽", a_),
			StyleIdentifier.EnvelopeAddress,
			ClipboardData.b("ͥ٧ᱩ५ɭὯɱᅳ噵੷όࡻ୽", a_),
			StyleIdentifier.EnvelopeReturn,
			ClipboardData.b("ཥ٧๩५᙭偯䍱", a_),
			StyleIdentifier.Index1,
			ClipboardData.b("ཥ٧๩५᙭偯䁱", a_),
			StyleIdentifier.Index2,
			ClipboardData.b("ཥ٧๩५᙭偯䅱", a_),
			StyleIdentifier.Index3,
			ClipboardData.b("ཥ٧๩५᙭偯䙱", a_),
			StyleIdentifier.Index4,
			ClipboardData.b("ཥ٧๩५᙭偯䝱", a_),
			StyleIdentifier.Index5,
			ClipboardData.b("ཥ٧๩५᙭偯䑱", a_),
			StyleIdentifier.Index6,
			ClipboardData.b("ཥ٧๩५᙭偯䕱", a_),
			StyleIdentifier.Index7,
			ClipboardData.b("ཥ٧๩५᙭偯䩱", a_),
			StyleIdentifier.Index8,
			ClipboardData.b("ཥ٧๩५᙭偯䭱", a_),
			StyleIdentifier.Index9,
			ClipboardData.b("ཥ٧๩५᙭偯ᩱᅳ᝵ᱷ፹ቻ᥽", a_),
			StyleIdentifier.IndexHeading,
			ClipboardData.b("⩥ŧᥩᡫ", a_),
			StyleIdentifier.List,
			ClipboardData.b("⩥ŧᥩᡫ乭䉯", a_),
			StyleIdentifier.List2,
			ClipboardData.b("⩥ŧᥩᡫ乭䍯", a_),
			StyleIdentifier.List3,
			ClipboardData.b("⩥ŧᥩᡫ乭䑯", a_),
			StyleIdentifier.List4,
			ClipboardData.b("⩥ŧᥩᡫ乭䕯", a_),
			StyleIdentifier.List5,
			ClipboardData.b("⩥ŧᥩᡫ乭㉯ݱᡳ᩵ᵷ๹", a_),
			StyleIdentifier.ListBullet,
			ClipboardData.b("⩥ŧᥩᡫ乭㉯ݱᡳ᩵ᵷ๹屻䱽", a_),
			StyleIdentifier.ListBullet2,
			ClipboardData.b("⩥ŧᥩᡫ乭㉯ݱᡳ᩵ᵷ๹屻䵽", a_),
			StyleIdentifier.ListBullet3,
			ClipboardData.b("⩥ŧᥩᡫ乭㉯ݱᡳ᩵ᵷ๹屻䩽", a_),
			StyleIdentifier.ListBullet4,
			ClipboardData.b("⩥ŧᥩᡫ乭㉯ݱᡳ᩵ᵷ๹屻䭽", a_),
			StyleIdentifier.ListBullet5,
			ClipboardData.b("⩥ŧᥩᡫ乭㍯ᵱᩳɵᅷᑹॻ᭽", a_),
			StyleIdentifier.ListContinue,
			ClipboardData.b("⩥ŧᥩᡫ乭㍯ᵱᩳɵᅷᑹॻ᭽ꁿ낁", a_),
			StyleIdentifier.ListContinue2,
			ClipboardData.b("⩥ŧᥩᡫ乭㍯ᵱᩳɵᅷᑹॻ᭽ꁿ놁", a_),
			StyleIdentifier.ListContinue3,
			ClipboardData.b("⩥ŧᥩᡫ乭㍯ᵱᩳɵᅷᑹॻ᭽ꁿ뚁", a_),
			StyleIdentifier.ListContinue4,
			ClipboardData.b("⩥ŧᥩᡫ乭㍯ᵱᩳɵᅷᑹॻ᭽ꁿ랁", a_),
			StyleIdentifier.ListContinue5,
			ClipboardData.b("⩥ŧᥩᡫ乭㹯ݱᥳᑵᵷࡹ", a_),
			StyleIdentifier.ListNumber,
			ClipboardData.b("⩥ŧᥩᡫ乭㹯ݱᥳᑵᵷࡹ屻䱽", a_),
			StyleIdentifier.ListNumber2,
			ClipboardData.b("⩥ŧᥩᡫ乭㹯ݱᥳᑵᵷࡹ屻䵽", a_),
			StyleIdentifier.ListNumber3,
			ClipboardData.b("⩥ŧᥩᡫ乭㹯ݱᥳᑵᵷࡹ屻䩽", a_),
			StyleIdentifier.ListNumber4,
			ClipboardData.b("⩥ŧᥩᡫ乭㹯ݱᥳᑵᵷࡹ屻䭽", a_),
			StyleIdentifier.ListNumber5,
			ClipboardData.b("⩥ŧᥩᡫ乭⁯፱ٳ᝵ίࡹᵻ๽", a_),
			StyleIdentifier.ListParagraph,
			ClipboardData.b("⡥ݧ䩩㽫ṭᅯᅱᵳᡵί", a_),
			StyleIdentifier.NoSpacing,
			ClipboardData.b("⡥ݧᡩū཭ᱯ", a_),
			StyleIdentifier.Normal,
			ClipboardData.b("⡥ݧᡩū཭ᱯ剱屳ⅵᵷ᡹啻", a_),
			StyleIdentifier.NormalWeb,
			ClipboardData.b("⡥ݧᡩū཭ᱯ剱㵳ᡵᱷόቻ੽", a_),
			StyleIdentifier.NormalIndent,
			ClipboardData.b("㑥൧ᱩիᵭ᥯ᵱᩳ", a_),
			StyleIdentifier.Revision,
			ClipboardData.b("ብ१ࡩk୭偯ᵱታ噵᥷ཹࡻᙽﾋ", a_),
			StyleIdentifier.TableOfAuthorities,
			ClipboardData.b("ብ१ࡩk୭偯ᵱታ噵ṷ፹᭻୽", a_),
			StyleIdentifier.TableOfFigures,
			ClipboardData.b("ብݧ୩䱫٭ᕯ፱ၳήᙷᵹ", a_),
			StyleIdentifier.ToaHeading,
			ClipboardData.b("ብݧ३䱫彭", a_),
			StyleIdentifier.Toc1,
			ClipboardData.b("ብݧ३䱫屭", a_),
			StyleIdentifier.Toc2,
			ClipboardData.b("ብݧ३䱫嵭", a_),
			StyleIdentifier.Toc3,
			ClipboardData.b("ብݧ३䱫婭", a_),
			StyleIdentifier.Toc4,
			ClipboardData.b("ብݧ३䱫孭", a_),
			StyleIdentifier.Toc5,
			ClipboardData.b("ብݧ३䱫塭", a_),
			StyleIdentifier.Toc6,
			ClipboardData.b("ብݧ३䱫奭", a_),
			StyleIdentifier.Toc7,
			ClipboardData.b("ብݧ३䱫噭", a_),
			StyleIdentifier.Toc8,
			ClipboardData.b("ብݧ३䱫坭", a_),
			StyleIdentifier.Toc9,
			ClipboardData.b("㉥❧⥩䱫♭ᕯ፱ၳήᙷᵹ", a_),
			StyleIdentifier.TocHeading,
			ClipboardData.b("⥥ᵧṩkݭṯ᝱味㩵ᅷॹࡻ幽녿", a_),
			StyleIdentifier.OutlineList1,
			ClipboardData.b("⥥ᵧṩkݭṯ᝱味㩵ᅷॹࡻ幽뉿", a_),
			StyleIdentifier.OutlineList2,
			ClipboardData.b("⥥ᵧṩkݭṯ᝱味㩵ᅷॹࡻ幽덿", a_),
			StyleIdentifier.OutlineList3,
			ClipboardData.b("⡥ݧ䩩⁫ݭͯٱ", a_),
			StyleIdentifier.NoList,
			ClipboardData.b("╥ݧ٩ͫᱭᙯݱᡳ噵㽷ࡹᕻ᩽", a_),
			StyleIdentifier.ColorfulGrid,
			ClipboardData.b("╥ݧ٩ͫᱭᙯݱᡳ噵㽷ࡹᕻ᩽ꁿ쎁꺍ꆏ", a_),
			StyleIdentifier.ColorfulGridAccent1,
			ClipboardData.b("╥ݧ٩ͫᱭᙯݱᡳ噵㽷ࡹᕻ᩽ꁿ쎁꺍ꊏ", a_),
			StyleIdentifier.ColorfulGridAccent2,
			ClipboardData.b("╥ݧ٩ͫᱭᙯݱᡳ噵㽷ࡹᕻ᩽ꁿ쎁꺍ꎏ", a_),
			StyleIdentifier.ColorfulGridAccent3,
			ClipboardData.b("╥ݧ٩ͫᱭᙯݱᡳ噵㽷ࡹᕻ᩽ꁿ쎁꺍꒏", a_),
			StyleIdentifier.ColorfulGridAccent4,
			ClipboardData.b("╥ݧ٩ͫᱭᙯݱᡳ噵㽷ࡹᕻ᩽ꁿ쎁꺍ꖏ", a_),
			StyleIdentifier.ColorfulGridAccent5,
			ClipboardData.b("╥ݧ٩ͫᱭᙯݱᡳ噵㽷ࡹᕻ᩽ꁿ쎁꺍ꚏ", a_),
			StyleIdentifier.ColorfulGridAccent6,
			ClipboardData.b("╥ݧ٩ͫᱭᙯݱᡳ噵㑷፹ཻ੽", a_),
			StyleIdentifier.ColorfulList,
			ClipboardData.b("╥ݧ٩ͫᱭᙯݱᡳ噵㑷፹ཻ੽ꁿ쎁꺍ꆏ", a_),
			StyleIdentifier.ColorfulListAccent1,
			ClipboardData.b("╥ݧ٩ͫᱭᙯݱᡳ噵㑷፹ཻ੽ꁿ쎁꺍ꊏ", a_),
			StyleIdentifier.ColorfulListAccent2,
			ClipboardData.b("╥ݧ٩ͫᱭᙯݱᡳ噵㑷፹ཻ੽ꁿ쎁꺍ꎏ", a_),
			StyleIdentifier.ColorfulListAccent3,
			ClipboardData.b("╥ݧ٩ͫᱭᙯݱᡳ噵㑷፹ཻ੽ꁿ쎁꺍꒏", a_),
			StyleIdentifier.ColorfulListAccent4,
			ClipboardData.b("╥ݧ٩ͫᱭᙯݱᡳ噵㑷፹ཻ੽ꁿ쎁꺍ꖏ", a_),
			StyleIdentifier.ColorfulListAccent5,
			ClipboardData.b("╥ݧ٩ͫᱭᙯݱᡳ噵㑷፹ཻ੽ꁿ쎁꺍ꚏ", a_),
			StyleIdentifier.ColorfulListAccent6,
			ClipboardData.b("╥ݧ٩ͫᱭᙯݱᡳ噵⭷ቹᵻ᩽", a_),
			StyleIdentifier.ColorfulShading,
			ClipboardData.b("╥ݧ٩ͫᱭᙯݱᡳ噵⭷ቹᵻ᩽ꚅ즇ﺏ뒓ꞕ", a_),
			StyleIdentifier.ColorfulShadingAccent1,
			ClipboardData.b("╥ݧ٩ͫᱭᙯݱᡳ噵⭷ቹᵻ᩽ꚅ즇ﺏ뒓꒕", a_),
			StyleIdentifier.ColorfulShadingAccent2,
			ClipboardData.b("╥ݧ٩ͫᱭᙯݱᡳ噵⭷ቹᵻ᩽ꚅ즇ﺏ뒓ꖕ", a_),
			StyleIdentifier.ColorfulShadingAccent3,
			ClipboardData.b("╥ݧ٩ͫᱭᙯݱᡳ噵⭷ቹᵻ᩽ꚅ즇ﺏ뒓ꊕ", a_),
			StyleIdentifier.ColorfulShadingAccent4,
			ClipboardData.b("╥ݧ٩ͫᱭᙯݱᡳ噵⭷ቹᵻ᩽ꚅ즇ﺏ뒓ꎕ", a_),
			StyleIdentifier.ColorfulShadingAccent5,
			ClipboardData.b("╥ݧ٩ͫᱭᙯݱᡳ噵⭷ቹᵻ᩽ꚅ즇ﺏ뒓ꂕ", a_),
			StyleIdentifier.ColorfulShadingAccent6,
			ClipboardData.b("≥१ᡩݫ乭㱯᭱ݳɵ", a_),
			StyleIdentifier.DarkList,
			ClipboardData.b("≥१ᡩݫ乭㱯᭱ݳɵ塷㭹ύᵽꚅ릇", a_),
			StyleIdentifier.DarkListAccent1,
			ClipboardData.b("≥१ᡩݫ乭㱯᭱ݳɵ塷㭹ύᵽꚅ몇", a_),
			StyleIdentifier.DarkListAccent2,
			ClipboardData.b("≥१ᡩݫ乭㱯᭱ݳɵ塷㭹ύᵽꚅ뮇", a_),
			StyleIdentifier.DarkListAccent3,
			ClipboardData.b("≥१ᡩݫ乭㱯᭱ݳɵ塷㭹ύᵽꚅ벇", a_),
			StyleIdentifier.DarkListAccent4,
			ClipboardData.b("≥१ᡩݫ乭㱯᭱ݳɵ塷㭹ύᵽꚅ붇", a_),
			StyleIdentifier.DarkListAccent5,
			ClipboardData.b("≥१ᡩݫ乭㱯᭱ݳɵ塷㭹ύᵽꚅ뺇", a_),
			StyleIdentifier.DarkListAccent6,
			ClipboardData.b("⩥ŧ൩ѫᩭ偯㕱ٳήᱷ", a_),
			StyleIdentifier.LightGrid,
			ClipboardData.b("⩥ŧ൩ѫᩭ偯㕱ٳήᱷ婹㵻ᵽꢇ뮉", a_),
			StyleIdentifier.LightGridAccent1,
			ClipboardData.b("⩥ŧ൩ѫᩭ偯㕱ٳήᱷ婹㵻ᵽꢇ뢉", a_),
			StyleIdentifier.LightGridAccent2,
			ClipboardData.b("⩥ŧ൩ѫᩭ偯㕱ٳήᱷ婹㵻ᵽꢇ릉", a_),
			StyleIdentifier.LightGridAccent3,
			ClipboardData.b("⩥ŧ൩ѫᩭ偯㕱ٳήᱷ婹㵻ᵽꢇ뺉", a_),
			StyleIdentifier.LightGridAccent4,
			ClipboardData.b("⩥ŧ൩ѫᩭ偯㕱ٳήᱷ婹㵻ᵽꢇ뾉", a_),
			StyleIdentifier.LightGridAccent5,
			ClipboardData.b("⩥ŧ൩ѫᩭ偯㕱ٳήᱷ婹㵻ᵽꢇ벉", a_),
			StyleIdentifier.LightGridAccent6,
			ClipboardData.b("⩥ŧ൩ѫᩭ偯㹱ᵳյ౷", a_),
			StyleIdentifier.LightList,
			ClipboardData.b("⩥ŧ൩ѫᩭ偯㹱ᵳյ౷婹㵻ᵽꢇ뮉", a_),
			StyleIdentifier.LightListAccent1,
			ClipboardData.b("⩥ŧ൩ѫᩭ偯㹱ᵳյ౷婹㵻ᵽꢇ뢉", a_),
			StyleIdentifier.LightListAccent2,
			ClipboardData.b("⩥ŧ൩ѫᩭ偯㹱ᵳյ౷婹㵻ᵽꢇ릉", a_),
			StyleIdentifier.LightListAccent3,
			ClipboardData.b("⩥ŧ൩ѫᩭ偯㹱ᵳյ౷婹㵻ᵽꢇ뺉", a_),
			StyleIdentifier.LightListAccent4,
			ClipboardData.b("⩥ŧ൩ѫᩭ偯㹱ᵳյ౷婹㵻ᵽꢇ뾉", a_),
			StyleIdentifier.LightListAccent5,
			ClipboardData.b("⩥ŧ൩ѫᩭ偯㹱ᵳյ౷婹㵻ᵽꢇ벉", a_),
			StyleIdentifier.LightListAccent6,
			ClipboardData.b("⩥ŧ൩ѫᩭ偯ⅱᱳ᝵ᱷ፹ቻ᥽", a_),
			StyleIdentifier.LightShading,
			ClipboardData.b("⩥ŧ൩ѫᩭ偯ⅱᱳ᝵ᱷ፹ቻ᥽ꁿ쎁꺍ꆏ", a_),
			StyleIdentifier.LightShadingAccent1,
			ClipboardData.b("⩥ŧ൩ѫᩭ偯ⅱᱳ᝵ᱷ፹ቻ᥽ꁿ쎁꺍ꊏ", a_),
			StyleIdentifier.LightShadingAccent2,
			ClipboardData.b("⩥ŧ൩ѫᩭ偯ⅱᱳ᝵ᱷ፹ቻ᥽ꁿ쎁꺍ꎏ", a_),
			StyleIdentifier.LightShadingAccent3,
			ClipboardData.b("⩥ŧ൩ѫᩭ偯ⅱᱳ᝵ᱷ፹ቻ᥽ꁿ쎁꺍꒏", a_),
			StyleIdentifier.LightShadingAccent4,
			ClipboardData.b("⩥ŧ൩ѫᩭ偯ⅱᱳ᝵ᱷ፹ቻ᥽ꁿ쎁꺍ꖏ", a_),
			StyleIdentifier.LightShadingAccent5,
			ClipboardData.b("⩥ŧ൩ѫᩭ偯ⅱᱳ᝵ᱷ፹ቻ᥽ꁿ쎁꺍ꚏ", a_),
			StyleIdentifier.LightShadingAccent6,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㍳ѵᅷṹ屻佽", a_),
			StyleIdentifier.MediumGrid1,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㍳ѵᅷṹ屻佽ꁿ쎁꺍ꆏ", a_),
			StyleIdentifier.MediumGrid1Accent1,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㍳ѵᅷṹ屻佽ꁿ쎁꺍ꊏ", a_),
			StyleIdentifier.MediumGrid1Accent2,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㍳ѵᅷṹ屻佽ꁿ쎁꺍ꎏ", a_),
			StyleIdentifier.MediumGrid1Accent3,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㍳ѵᅷṹ屻佽ꁿ쎁꺍꒏", a_),
			StyleIdentifier.MediumGrid1Accent4,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㍳ѵᅷṹ屻佽ꁿ쎁꺍ꖏ", a_),
			StyleIdentifier.MediumGrid1Accent5,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㍳ѵᅷṹ屻佽ꁿ쎁꺍ꚏ", a_),
			StyleIdentifier.MediumGrid1Accent6,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㍳ѵᅷṹ屻䱽", a_),
			StyleIdentifier.MediumGrid2,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㍳ѵᅷṹ屻䱽ꁿ쎁꺍ꆏ", a_),
			StyleIdentifier.MediumGrid2Accent1,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㍳ѵᅷṹ屻䱽ꁿ쎁꺍ꊏ", a_),
			StyleIdentifier.MediumGrid2Accent2,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㍳ѵᅷṹ屻䱽ꁿ쎁꺍ꎏ", a_),
			StyleIdentifier.MediumGrid2Accent3,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㍳ѵᅷṹ屻䱽ꁿ쎁꺍꒏", a_),
			StyleIdentifier.MediumGrid2Accent4,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㍳ѵᅷṹ屻䱽ꁿ쎁꺍ꖏ", a_),
			StyleIdentifier.MediumGrid2Accent5,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㍳ѵᅷṹ屻䱽ꁿ쎁꺍ꚏ", a_),
			StyleIdentifier.MediumGrid2Accent6,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㍳ѵᅷṹ屻䵽", a_),
			StyleIdentifier.MediumGrid3,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㍳ѵᅷṹ屻䵽ꁿ쎁꺍ꆏ", a_),
			StyleIdentifier.MediumGrid3Accent1,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㍳ѵᅷṹ屻䵽ꁿ쎁꺍ꊏ", a_),
			StyleIdentifier.MediumGrid3Accent2,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㍳ѵᅷṹ屻䵽ꁿ쎁꺍ꎏ", a_),
			StyleIdentifier.MediumGrid3Accent3,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㍳ѵᅷṹ屻䵽ꁿ쎁꺍꒏", a_),
			StyleIdentifier.MediumGrid3Accent4,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㍳ѵᅷṹ屻䵽ꁿ쎁꺍ꖏ", a_),
			StyleIdentifier.MediumGrid3Accent5,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㍳ѵᅷṹ屻䵽ꁿ쎁꺍ꚏ", a_),
			StyleIdentifier.MediumGrid3Accent6,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㡳ή୷๹屻佽", a_),
			StyleIdentifier.MediumList1,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㡳ή୷๹屻佽ꁿ쎁꺍ꆏ", a_),
			StyleIdentifier.MediumList1Accent1,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㡳ή୷๹屻佽ꁿ쎁꺍ꊏ", a_),
			StyleIdentifier.MediumList1Accent2,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㡳ή୷๹屻佽ꁿ쎁꺍ꎏ", a_),
			StyleIdentifier.MediumList1Accent3,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㡳ή୷๹屻佽ꁿ쎁꺍꒏", a_),
			StyleIdentifier.MediumList1Accent4,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㡳ή୷๹屻佽ꁿ쎁꺍ꖏ", a_),
			StyleIdentifier.MediumList1Accent5,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㡳ή୷๹屻佽ꁿ쎁꺍ꚏ", a_),
			StyleIdentifier.MediumList1Accent6,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㡳ή୷๹屻䱽", a_),
			StyleIdentifier.MediumList2,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㡳ή୷๹屻䱽ꁿ쎁꺍ꆏ", a_),
			StyleIdentifier.MediumList2Accent1,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㡳ή୷๹屻䱽ꁿ쎁꺍ꊏ", a_),
			StyleIdentifier.MediumList2Accent2,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㡳ή୷๹屻䱽ꁿ쎁꺍ꎏ", a_),
			StyleIdentifier.MediumList2Accent3,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㡳ή୷๹屻䱽ꁿ쎁꺍꒏", a_),
			StyleIdentifier.MediumList2Accent4,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㡳ή୷๹屻䱽ꁿ쎁꺍ꖏ", a_),
			StyleIdentifier.MediumList2Accent5,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱㡳ή୷๹屻䱽ꁿ쎁꺍ꚏ", a_),
			StyleIdentifier.MediumList2Accent6,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱❳ṵ᥷ṹᕻၽꊁ떃", a_),
			StyleIdentifier.MediumShading1,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱❳ṵ᥷ṹᕻၽꊁ떃ꚅ즇ﺏ뒓ꞕ", a_),
			StyleIdentifier.MediumShading1Accent1,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱❳ṵ᥷ṹᕻၽꊁ떃ꚅ즇ﺏ뒓꒕", a_),
			StyleIdentifier.MediumShading1Accent2,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱❳ṵ᥷ṹᕻၽꊁ떃ꚅ즇ﺏ뒓ꖕ", a_),
			StyleIdentifier.MediumShading1Accent3,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱❳ṵ᥷ṹᕻၽꊁ떃ꚅ즇ﺏ뒓ꊕ", a_),
			StyleIdentifier.MediumShading1Accent4,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱❳ṵ᥷ṹᕻၽꊁ떃ꚅ즇ﺏ뒓ꎕ", a_),
			StyleIdentifier.MediumShading1Accent5,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱❳ṵ᥷ṹᕻၽꊁ떃ꚅ즇ﺏ뒓ꂕ", a_),
			StyleIdentifier.MediumShading1Accent6,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱❳ṵ᥷ṹᕻၽꊁ뚃", a_),
			StyleIdentifier.MediumShading2,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱❳ṵ᥷ṹᕻၽꊁ뚃ꚅ즇ﺏ뒓ꞕ", a_),
			StyleIdentifier.MediumShading2Accent1,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱❳ṵ᥷ṹᕻၽꊁ뚃ꚅ즇ﺏ뒓꒕", a_),
			StyleIdentifier.MediumShading2Accent2,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱❳ṵ᥷ṹᕻၽꊁ뚃ꚅ즇ﺏ뒓ꖕ", a_),
			StyleIdentifier.MediumShading2Accent3,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱❳ṵ᥷ṹᕻၽꊁ뚃ꚅ즇ﺏ뒓ꊕ", a_),
			StyleIdentifier.MediumShading2Accent4,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱❳ṵ᥷ṹᕻၽꊁ뚃ꚅ즇ﺏ뒓ꎕ", a_),
			StyleIdentifier.MediumShading2Accent5,
			ClipboardData.b("⭥൧๩ի᭭ᵯ剱❳ṵ᥷ṹᕻၽꊁ뚃ꚅ즇ﺏ뒓ꂕ", a_),
			StyleIdentifier.MediumShading2Accent6,
			ClipboardData.b("㉥१ࡩk୭偯䅱び噵ᵷᱹ᩻᭽ꚅ릇", a_),
			StyleIdentifier.Table3DEffects1,
			ClipboardData.b("㉥१ࡩk୭偯䅱び噵ᵷᱹ᩻᭽ꚅ몇", a_),
			StyleIdentifier.Table3DEffects2,
			ClipboardData.b("㉥१ࡩk୭偯䅱び噵ᵷᱹ᩻᭽ꚅ뮇", a_),
			StyleIdentifier.Table3DEffects3,
			ClipboardData.b("㉥१ࡩk୭偯ㅱᡳ᝵୷ॹᕻᵽꁿ뎁", a_),
			StyleIdentifier.TableClassic1,
			ClipboardData.b("㉥१ࡩk୭偯ㅱᡳ᝵୷ॹᕻᵽꁿ낁", a_),
			StyleIdentifier.TableClassic2,
			ClipboardData.b("㉥१ࡩk୭偯ㅱᡳ᝵୷ॹᕻᵽꁿ놁", a_),
			StyleIdentifier.TableClassic3,
			ClipboardData.b("㉥१ࡩk୭偯ㅱᡳ᝵୷ॹᕻᵽꁿ뚁", a_),
			StyleIdentifier.TableClassic4,
			ClipboardData.b("㉥१ࡩk୭偯ㅱ᭳᩵᝷ࡹ᩻୽ꊁ떃", a_),
			StyleIdentifier.TableColorful1,
			ClipboardData.b("㉥१ࡩk୭偯ㅱ᭳᩵᝷ࡹ᩻୽ꊁ뚃", a_),
			StyleIdentifier.TableColorful2,
			ClipboardData.b("㉥१ࡩk୭偯ㅱ᭳᩵᝷ࡹ᩻୽ꊁ랃", a_),
			StyleIdentifier.TableColorful3,
			ClipboardData.b("㉥१ࡩk୭偯ㅱ᭳᩵൷᝹ቻൽꁿ뎁", a_),
			StyleIdentifier.TableColumns1,
			ClipboardData.b("㉥१ࡩk୭偯ㅱ᭳᩵൷᝹ቻൽꁿ낁", a_),
			StyleIdentifier.TableColumns2,
			ClipboardData.b("㉥१ࡩk୭偯ㅱ᭳᩵൷᝹ቻൽꁿ놁", a_),
			StyleIdentifier.TableColumns3,
			ClipboardData.b("㉥१ࡩk୭偯ㅱ᭳᩵൷᝹ቻൽꁿ뚁", a_),
			StyleIdentifier.TableColumns4,
			ClipboardData.b("㉥१ࡩk୭偯ㅱ᭳᩵൷᝹ቻൽꁿ랁", a_),
			StyleIdentifier.TableColumns5,
			ClipboardData.b("㉥१ࡩk୭偯ㅱ᭳ᡵ౷όᅻ๽", a_),
			StyleIdentifier.TableContemporary,
			ClipboardData.b("㉥१ࡩk୭偯㝱ᡳ፵ί᭹ቻ੽", a_),
			StyleIdentifier.TableElegant,
			ClipboardData.b("㉥१ࡩk୭偯㕱ٳήᱷ", a_),
			StyleIdentifier.TableGrid,
			ClipboardData.b("㉥१ࡩk୭偯㕱ٳήᱷ婹䵻", a_),
			StyleIdentifier.TableGrid1,
			ClipboardData.b("㉥१ࡩk୭偯㕱ٳήᱷ婹乻", a_),
			StyleIdentifier.TableGrid2,
			ClipboardData.b("㉥१ࡩk୭偯㕱ٳήᱷ婹佻", a_),
			StyleIdentifier.TableGrid3,
			ClipboardData.b("㉥१ࡩk୭偯㕱ٳήᱷ婹䡻", a_),
			StyleIdentifier.TableGrid4,
			ClipboardData.b("㉥१ࡩk୭偯㕱ٳήᱷ婹䥻", a_),
			StyleIdentifier.TableGrid5,
			ClipboardData.b("㉥१ࡩk୭偯㕱ٳήᱷ婹䩻", a_),
			StyleIdentifier.TableGrid6,
			ClipboardData.b("㉥१ࡩk୭偯㕱ٳήᱷ婹䭻", a_),
			StyleIdentifier.TableGrid7,
			ClipboardData.b("㉥१ࡩk୭偯㕱ٳήᱷ婹䑻", a_),
			StyleIdentifier.TableGrid8,
			ClipboardData.b("㉥१ࡩk୭偯㹱ᵳյ౷婹䵻", a_),
			StyleIdentifier.TableList1,
			ClipboardData.b("㉥१ࡩk୭偯㹱ᵳյ౷婹乻", a_),
			StyleIdentifier.TableList2,
			ClipboardData.b("㉥१ࡩk୭偯㹱ᵳյ౷婹佻", a_),
			StyleIdentifier.TableList3,
			ClipboardData.b("㉥१ࡩk୭偯㹱ᵳյ౷婹䡻", a_),
			StyleIdentifier.TableList4,
			ClipboardData.b("㉥१ࡩk୭偯㹱ᵳյ౷婹䥻", a_),
			StyleIdentifier.TableList5,
			ClipboardData.b("㉥१ࡩk୭偯㹱ᵳյ౷婹䩻", a_),
			StyleIdentifier.TableList6,
			ClipboardData.b("㉥१ࡩk୭偯㹱ᵳյ౷婹䭻", a_),
			StyleIdentifier.TableList7,
			ClipboardData.b("㉥१ࡩk୭偯㹱ᵳյ౷婹䑻", a_),
			StyleIdentifier.TableList8,
			ClipboardData.b("⡥ݧᡩū཭ᱯ剱⁳᝵᩷ᙹ᥻", a_),
			StyleIdentifier.TableNormal,
			ClipboardData.b("㉥१ࡩk୭偯≱ٳ᥵ṷόཻൽ", a_),
			StyleIdentifier.TableProfessional,
			ClipboardData.b("㉥१ࡩk୭偯ⅱᵳ᭵ࡷᙹ᥻幽녿", a_),
			StyleIdentifier.TableSimple1,
			ClipboardData.b("㉥१ࡩk୭偯ⅱᵳ᭵ࡷᙹ᥻幽뉿", a_),
			StyleIdentifier.TableSimple2,
			ClipboardData.b("㉥१ࡩk୭偯ⅱᵳ᭵ࡷᙹ᥻幽덿", a_),
			StyleIdentifier.TableSimple3,
			ClipboardData.b("㉥१ࡩk୭偯ⅱųᑵ౷ᙹ᥻幽녿", a_),
			StyleIdentifier.TableSubtle1,
			ClipboardData.b("㉥१ࡩk୭偯ⅱųᑵ౷ᙹ᥻幽뉿", a_),
			StyleIdentifier.TableSubtle2,
			ClipboardData.b("㉥१ࡩk୭偯♱ᱳ፵ᕷό", a_),
			StyleIdentifier.TableTheme,
			ClipboardData.b("㉥१ࡩk୭偯╱ᅳᑵ塷䭹", a_),
			StyleIdentifier.TableWeb1,
			ClipboardData.b("㉥१ࡩk୭偯╱ᅳᑵ塷䡹", a_),
			StyleIdentifier.TableWeb2,
			ClipboardData.b("㉥१ࡩk୭偯╱ᅳᑵ塷䥹", a_),
			StyleIdentifier.TableWeb3
		}, sprḕ.ᜎ, sprḕ.ᜏ);
		spr\u19FA.ᜁ(new object[]
		{
			ClipboardData.b("ݥ٧ѩͫᩭᅯٱᵳ᥵ᙷ婹๻᭽", a_),
			ClipboardData.b("╥ݧݩū୭ṯٱ味⑵ᵷᱹ᥻౽", a_),
			ClipboardData.b("ͥ٧๩ɫŭѯ᝱味ѵᵷᱹ᥻౽", a_),
			ClipboardData.b("⍥٧๩ɫŭѯ᝱味⑵ᵷᱹ᥻౽", a_),
			ClipboardData.b("eݧթᡫmὯٱᅳ噵੷ό᩻᭽", a_),
			ClipboardData.b("⁥ݧթᡫmὯٱᅳ噵⩷ό᩻᭽", a_),
			ClipboardData.b("੥ŧѩ५乭ṯݱᥳᑵᵷࡹ", a_),
			ClipboardData.b("⩥ŧѩ५乭㹯ݱᥳᑵᵷࡹ", a_),
			ClipboardData.b("ᙥ१൩५乭ṯݱᥳᑵᵷࡹ", a_),
			ClipboardData.b("㙥१൩५乭㹯ݱᥳᑵᵷࡹ", a_),
			ClipboardData.b("ݥ٧ѩͫᩭᅯٱᵳ᥵ᙷ婹ཻ୽ﲇ", a_),
			ClipboardData.b("╥ݧݩū୭ṯٱ味╵൷᡹ᙻ᭽", a_),
			ClipboardData.b("ݥ٧ѩͫᩭᅯٱᵳ᥵ᙷ婹ࡻ᭽", a_),
			ClipboardData.b("╥ݧݩū୭ṯٱ味≵ᵷɹࡻ", a_),
			ClipboardData.b("ե१ᩩᡫݭὯᱱ", a_),
			ClipboardData.b("╥१ᩩᡫݭὯᱱ", a_),
			ClipboardData.b("ͥ٧๩ɫŭѯ᝱味ɵᵷɹࡻ", a_),
			ClipboardData.b("⍥٧๩ɫŭѯ᝱味≵ᵷɹࡻ", a_),
			ClipboardData.b("ͥ٧ᱩ५ɭὯɱᅳ噵᥷ṹ᡻౽", a_),
			ClipboardData.b("⍥٧ᱩ५ɭὯɱᅳ噵㥷ṹ᡻౽", a_),
			ClipboardData.b("ͥ٧ᱩ५ɭὯɱᅳ噵੷όࡻ୽", a_),
			ClipboardData.b("⍥٧ᱩ५ɭὯɱᅳ噵⩷όࡻ୽", a_),
			ClipboardData.b("eݧթᡫ୭ɯ", a_),
			ClipboardData.b("⁥ݧթᡫ୭ɯ", a_),
			ClipboardData.b("eݧթᡫmὯٱᅳ噵౷όѻ੽", a_),
			ClipboardData.b("⁥ݧթᡫmὯٱᅳ噵ⱷόѻ੽", a_),
			ClipboardData.b("๥൧୩࡫୭ɯ", a_),
			ClipboardData.b("⹥൧୩࡫୭ɯ", a_),
			ClipboardData.b("๥൧୩࡫ݭṯᕱ味䝵", a_),
			ClipboardData.b("⹥൧୩࡫ݭṯᕱ味䝵", a_),
			ClipboardData.b("๥൧୩࡫ݭṯᕱ味䑵", a_),
			ClipboardData.b("⹥൧୩࡫ݭṯᕱ味䑵", a_),
			ClipboardData.b("๥൧୩࡫ݭṯᕱ味䕵", a_),
			ClipboardData.b("⹥൧୩࡫ݭṯᕱ味䕵", a_),
			ClipboardData.b("๥൧୩࡫ݭṯᕱ味䉵", a_),
			ClipboardData.b("⹥൧୩࡫ݭṯᕱ味䉵", a_),
			ClipboardData.b("๥൧୩࡫ݭṯᕱ味䍵", a_),
			ClipboardData.b("⹥൧୩࡫ݭṯᕱ味䍵", a_),
			ClipboardData.b("๥൧୩࡫ݭṯᕱ味䁵", a_),
			ClipboardData.b("⹥൧୩࡫ݭṯᕱ味䁵", a_),
			ClipboardData.b("๥൧୩࡫ݭṯᕱ味䅵", a_),
			ClipboardData.b("⹥൧୩࡫ݭṯᕱ味䅵", a_),
			ClipboardData.b("๥൧୩࡫ݭṯᕱ味乵", a_),
			ClipboardData.b("⹥൧୩࡫ݭṯᕱ味乵", a_),
			ClipboardData.b("๥൧୩࡫ݭṯᕱ味併", a_),
			ClipboardData.b("⹥൧୩࡫ݭṯᕱ味併", a_),
			ClipboardData.b("ཥ٧๩५᙭偯䍱", a_),
			ClipboardData.b("⽥٧๩५᙭偯䍱", a_),
			ClipboardData.b("ཥ٧๩५᙭偯䁱", a_),
			ClipboardData.b("⽥٧๩५᙭偯䁱", a_),
			ClipboardData.b("ཥ٧๩५᙭偯䅱", a_),
			ClipboardData.b("⽥٧๩५᙭偯䅱", a_),
			ClipboardData.b("ཥ٧๩५᙭偯䙱", a_),
			ClipboardData.b("⽥٧๩५᙭偯䙱", a_),
			ClipboardData.b("ཥ٧๩५᙭偯䝱", a_),
			ClipboardData.b("⽥٧๩५᙭偯䝱", a_),
			ClipboardData.b("ཥ٧๩५᙭偯䑱", a_),
			ClipboardData.b("⽥٧๩५᙭偯䑱", a_),
			ClipboardData.b("ཥ٧๩५᙭偯䕱", a_),
			ClipboardData.b("⽥٧๩५᙭偯䕱", a_),
			ClipboardData.b("ཥ٧๩५᙭偯䩱", a_),
			ClipboardData.b("⽥٧๩५᙭偯䩱", a_),
			ClipboardData.b("ཥ٧๩५᙭偯䭱", a_),
			ClipboardData.b("⽥٧๩५᙭偯䭱", a_),
			ClipboardData.b("ཥ٧๩५᙭偯ᩱᅳ᝵ᱷ፹ቻ᥽", a_),
			ClipboardData.b("⽥٧๩५᙭偯㩱ᅳ᝵ᱷ፹ቻ᥽", a_),
			ClipboardData.b("୥१३ṫŭ", a_),
			ClipboardData.b("⭥१३ṫŭ", a_),
			ClipboardData.b("ࡥݧṩ५乭ᡯ᝱ᕳትᅷᑹ᭻", a_),
			ClipboardData.b("⡥ݧṩ५乭㡯᝱ᕳትᅷᑹ᭻", a_),
			ClipboardData.b("ብ१ࡩk୭偯ᵱታ噵᥷ཹࡻᙽﾋ", a_),
			ClipboardData.b("㉥१ࡩk୭偯ᵱታ噵㥷ཹࡻᙽﾋ", a_),
			ClipboardData.b("ብ१ࡩk୭偯ᵱታ噵ṷ፹᭻୽", a_),
			ClipboardData.b("㉥१ࡩk୭偯ᵱታ噵㹷፹᭻୽", a_),
			ClipboardData.b("ብݧ୩䱫٭ᕯ፱ၳήᙷᵹ", a_),
			ClipboardData.b("㉥❧⭩䱫♭ᕯ፱ၳήᙷᵹ", a_),
			ClipboardData.b("ብݧ३䱫彭", a_),
			ClipboardData.b("㉥❧⥩䱫彭", a_),
			ClipboardData.b("ብݧ३䱫屭", a_),
			ClipboardData.b("㉥❧⥩䱫屭", a_),
			ClipboardData.b("ብݧ३䱫嵭", a_),
			ClipboardData.b("㉥❧⥩䱫嵭", a_),
			ClipboardData.b("ብݧ३䱫婭", a_),
			ClipboardData.b("㉥❧⥩䱫婭", a_),
			ClipboardData.b("ብݧ३䱫孭", a_),
			ClipboardData.b("㉥❧⥩䱫孭", a_),
			ClipboardData.b("ብݧ३䱫塭", a_),
			ClipboardData.b("㉥❧⥩䱫塭", a_),
			ClipboardData.b("ብݧ३䱫奭", a_),
			ClipboardData.b("㉥❧⥩䱫奭", a_),
			ClipboardData.b("ብݧ३䱫噭", a_),
			ClipboardData.b("㉥❧⥩䱫噭", a_),
			ClipboardData.b("ብݧ३䱫坭", a_),
			ClipboardData.b("㉥❧⥩䱫坭", a_),
			ClipboardData.b("⥥ᵧṩkݭṯ᝱味㩵ᅷॹࡻ幽녿", a_),
			ClipboardData.b("坥䡧䕩䱫཭偯嵱味ή", a_),
			ClipboardData.b("⥥ᵧṩkݭṯ᝱味㩵ᅷॹࡻ幽뉿", a_),
			ClipboardData.b("坥䡧䕩䱫彭幯䍱味奵塷䭹剻佽깿뎁", a_),
			ClipboardData.b("⥥ᵧṩkݭṯ᝱味㩵ᅷॹࡻ幽덿", a_),
			ClipboardData.b("❥ᩧṩի൭ᱯ᝱味奵塷⥹᥻ᵽ", a_),
			ClipboardData.b("⡥ݧᡩū཭ᱯ剱⁳᝵᩷ᙹ᥻", a_),
			ClipboardData.b("㉥१ࡩk୭偯㱱᭳ѵᕷ᭹ၻ", a_)
		}, sprḕ.ᜐ, sprḕ.ᜑ);
		spr\u19FA.ᜁ(new object[]
		{
			ClipboardData.b("坥塧塩填᙭䝯䑱䱳", a_),
			ScreenSize.Size1024x768,
			ClipboardData.b("坥奧彩幫᙭䡯䩱䙳", a_),
			ScreenSize.Size1152x882,
			ClipboardData.b("坥奧彩幫᙭䥯䉱䑳", a_),
			ScreenSize.Size1152x900,
			ClipboardData.b("坥婧剩屫᙭䅯䉱䙳䉵", a_),
			ScreenSize.Size1280x1024,
			ClipboardData.b("坥幧婩屫᙭䅯䁱䑳䙵", a_),
			ScreenSize.Size1600x1200,
			ClipboardData.b("坥偧婩屫᙭䅯䙱䁳䙵", a_),
			ScreenSize.Size1800x1440,
			ClipboardData.b("坥內塩屫᙭䅯䁱䑳䙵", a_),
			ScreenSize.Size1920x1200,
			ClipboardData.b("卥屧幩ᑫ嵭䝯䑱", a_),
			ScreenSize.Size544x376,
			ClipboardData.b("健屧婩ᑫ婭䡯䉱", a_),
			ScreenSize.Size640x480,
			ClipboardData.b("入婧婩ᑫ孭䅯䁱", a_),
			ScreenSize.Size720x512,
			ClipboardData.b("幥塧婩ᑫ塭䁯䉱", a_),
			ScreenSize.Size800x600
		}, sprḕ.\u1712, sprḕ.\u1713);
	}

	// Token: 0x04002862 RID: 10338
	private static readonly Hashtable ᜀ;

	// Token: 0x04002863 RID: 10339
	private static readonly Hashtable ᜁ;

	// Token: 0x04002864 RID: 10340
	private static readonly Hashtable ᜂ;

	// Token: 0x04002865 RID: 10341
	private static readonly Hashtable ᜃ;

	// Token: 0x04002866 RID: 10342
	private static readonly Hashtable ᜄ;

	// Token: 0x04002867 RID: 10343
	private static readonly Hashtable ᜅ;

	// Token: 0x04002868 RID: 10344
	private static readonly Hashtable ᜆ;

	// Token: 0x04002869 RID: 10345
	private static readonly Hashtable ᜇ;

	// Token: 0x0400286A RID: 10346
	private static readonly Hashtable ᜈ;

	// Token: 0x0400286B RID: 10347
	private static readonly Hashtable ᜉ;

	// Token: 0x0400286C RID: 10348
	private static readonly Hashtable ᜊ;

	// Token: 0x0400286D RID: 10349
	private static readonly Hashtable ᜋ;

	// Token: 0x0400286E RID: 10350
	private static readonly Hashtable ᜌ;

	// Token: 0x0400286F RID: 10351
	private static readonly Hashtable \u170D;

	// Token: 0x04002870 RID: 10352
	private static readonly Hashtable ᜎ;

	// Token: 0x04002871 RID: 10353
	private static readonly Hashtable ᜏ;

	// Token: 0x04002872 RID: 10354
	private static readonly Hashtable ᜐ;

	// Token: 0x04002873 RID: 10355
	private static readonly Hashtable ᜑ;

	// Token: 0x04002874 RID: 10356
	private static readonly Hashtable \u1712;

	// Token: 0x04002875 RID: 10357
	private static readonly Hashtable \u1713;
}
