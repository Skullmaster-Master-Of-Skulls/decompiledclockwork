using System;
using System.Collections.Generic;
using Spire.CompoundFile.Doc;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields.Shape;

// Token: 0x02000354 RID: 852
internal class spr\u2311
{
	// Token: 0x06002DAA RID: 11690 RVA: 0x002BAB18 File Offset: 0x002B9B18
	private spr\u2311()
	{
	}

	// Token: 0x06002DAB RID: 11691 RVA: 0x002BAB2C File Offset: 0x002B9B2C
	internal static BaselineAlignment ᜋ(string A_0)
	{
		int a_ = 19;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_158:
			num = 2;
			break;
		default:
			if (false)
			{
			}
			num = 6;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 3;
				continue;
			case 1:
				goto IL_97;
			case 2:
				if (!(A_0 == ClipboardData.b("᭸ᑺॼ୾", a_)))
				{
					num = 9;
					continue;
				}
				return BaselineAlignment.Bottom;
			case 3:
				if (!(A_0 == ClipboardData.b("᭸᩺๼᩾", a_)))
				{
					num = 11;
					continue;
				}
				return BaselineAlignment.Baseline;
			case 4:
				if (!(A_0 == ClipboardData.b("ᡸ๺ॼၾ", a_)))
				{
					num = 12;
					continue;
				}
				return BaselineAlignment.Auto;
			case 5:
				if (!(A_0 == ClipboardData.b("൸ᑺർ", a_)))
				{
					num = 7;
					continue;
				}
				return BaselineAlignment.Top;
			case 7:
				num = 8;
				continue;
			case 8:
				if (!(A_0 == ClipboardData.b("᩸Ṻ፼୾", a_)))
				{
					num = 0;
					continue;
				}
				return BaselineAlignment.Center;
			case 9:
				num = 4;
				continue;
			case 10:
				num = 5;
				continue;
			case 11:
				goto IL_158;
			case 12:
				if (true)
				{
				}
				num = 1;
				continue;
			}
			if (A_0 == null)
			{
				return BaselineAlignment.Auto;
			}
			num = 10;
		}
		return BaselineAlignment.Center;
		IL_97:
		return BaselineAlignment.Auto;
	}

	// Token: 0x06002DAC RID: 11692 RVA: 0x002BACCC File Offset: 0x002B9CCC
	internal static string ᜀ(BaselineAlignment A_0)
	{
		int a_ = 3;
		for (;;)
		{
			for (;;)
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_79;
					case 1:
						num = 0;
						continue;
					case 2:
						switch (A_0)
						{
						case BaselineAlignment.Top:
							goto IL_B0;
						case BaselineAlignment.Center:
							goto IL_62;
						case BaselineAlignment.Baseline:
							goto IL_BF;
						case BaselineAlignment.Bottom:
							goto IL_53;
						case BaselineAlignment.Auto:
							goto IL_7B;
						default:
							if (true)
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
			IL_7B:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			}
			goto Block_2;
		}
		IL_53:
		return ClipboardData.b("୨Ѫᥬ᭮ṰṲ", a_);
		IL_62:
		return ClipboardData.b("੨๪ͬ᭮ᑰŲ", a_);
		IL_79:
		return "";
		Block_2:
		if (false)
		{
		}
		return ClipboardData.b("ࡨṪᥬn", a_);
		IL_B0:
		return ClipboardData.b("ᵨѪᵬ", a_);
		IL_BF:
		return ClipboardData.b("୨੪Ṭ੮ᵰᩲ᭴ቶ", a_);
	}

	// Token: 0x06002DAD RID: 11693 RVA: 0x002BADAC File Offset: 0x002B9DAC
	internal static PsopCapPosition ᜊ(string A_0)
	{
		int a_ = 12;
		for (;;)
		{
			IL_09:
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					if (!(A_0 == ClipboardData.b("ᱱ᭳ᡵᵷ", a_)))
					{
						num = 6;
						continue;
					}
					return PsopCapPosition.None;
				case 2:
					num = 7;
					continue;
				case 3:
					num = 1;
					continue;
				case 4:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 5:
					goto IL_63;
				case 6:
					num = 8;
					continue;
				case 7:
					if (!(A_0 == ClipboardData.b("άᕳѵί፹ቻ", a_)))
					{
						num = 4;
						continue;
					}
					return PsopCapPosition.Margin;
				case 8:
					if (!(A_0 == ClipboardData.b("ᙱٳ᥵ࡷ", a_)))
					{
						num = 2;
						continue;
					}
					return PsopCapPosition.Normal;
				}
				if (A_0 == null)
				{
					return PsopCapPosition.None;
				}
				num = 3;
			}
		}
		return PsopCapPosition.None;
		IL_63:
		return PsopCapPosition.None;
	}

	// Token: 0x06002DAE RID: 11694 RVA: 0x002BAED4 File Offset: 0x002B9ED4
	internal static string ᜀ(PsopCapPosition A_0)
	{
		int a_ = 12;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			for (;;)
			{
				IL_27:
				switch (num)
				{
				case 0:
					switch (A_0)
					{
					case PsopCapPosition.None:
						goto IL_78;
					case PsopCapPosition.Normal:
						goto IL_69;
					case PsopCapPosition.Margin:
						goto IL_99;
					default:
						num = 2;
						continue;
					}
					break;
				case 1:
					goto IL_8F;
				case 2:
					num = 1;
					continue;
				}
				goto IL_39;
			}
			IL_69:
			return ClipboardData.b("ᙱٳ᥵ࡷ", a_);
			IL_78:
			return ClipboardData.b("ᱱ᭳ᡵᵷ", a_);
			IL_8F:
			if (true)
			{
			}
			return "";
			IL_99:
			return ClipboardData.b("άᕳѵί፹ቻ", a_);
		default:
			if (false)
			{
			}
			break;
		}
		IL_39:
		num = 0;
		goto IL_27;
	}

	// Token: 0x06002DAF RID: 11695 RVA: 0x002BAF90 File Offset: 0x002B9F90
	internal static VerticalPosition ᜉ(string A_0)
	{
		int a_ = 12;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_131;
			case 1:
				spr᧓.ឮ = new Dictionary<string, int>(6)
				{
					{
						ClipboardData.b("᭱ᩳ᩵ᅷᑹ᥻", a_),
						0
					},
					{
						ClipboardData.b("ٱ᭳ٵ", a_),
						1
					},
					{
						ClipboardData.b("ᅱᅳᡵ౷ό๻", a_),
						2
					},
					{
						ClipboardData.b("ၱ᭳ɵ౷ᕹᅻ", a_),
						3
					},
					{
						ClipboardData.b("᭱ᩳյᅷṹ᥻", a_),
						4
					},
					{
						ClipboardData.b("ᵱųɵ୷፹᡻᭽", a_),
						5
					}
				};
				num = 2;
				continue;
			case 2:
				goto IL_136;
			case 3:
			{
				int num2;
				if (spr᧓.ឮ.TryGetValue(A_0, out num2))
				{
					if (true)
					{
					}
					num = 9;
					continue;
				}
				return VerticalPosition.None;
			}
			case 4:
				num = 8;
				continue;
			case 6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return VerticalPosition.Inside;
				default:
				{
					if (false)
					{
					}
					int num2;
					switch (num2)
					{
					case 0:
						return VerticalPosition.Inline;
					case 1:
						return VerticalPosition.Top;
					case 2:
						return VerticalPosition.Center;
					case 3:
						return VerticalPosition.Bottom;
					case 4:
						return VerticalPosition.Inside;
					case 5:
						return VerticalPosition.Outside;
					default:
						num = 7;
						continue;
					}
					break;
				}
				}
				break;
			case 7:
				num = 0;
				continue;
			case 8:
				if (spr᧓.ឮ == null)
				{
					num = 1;
					continue;
				}
				goto IL_136;
			case 9:
				num = 6;
				continue;
			}
			if (A_0 != null)
			{
				num = 4;
				continue;
			}
			return VerticalPosition.None;
			IL_136:
			num = 3;
		}
		return VerticalPosition.Center;
		IL_131:
		return VerticalPosition.None;
	}

	// Token: 0x06002DB0 RID: 11696 RVA: 0x002BB15C File Offset: 0x002BA15C
	internal static string ᜀ(VerticalPosition A_0)
	{
		int a_ = 13;
		for (;;)
		{
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
						case VerticalPosition.Inline:
							goto IL_85;
						case VerticalPosition.None:
							goto IL_EA;
						case VerticalPosition.Top:
							goto IL_BF;
						case VerticalPosition.Center:
							goto IL_76;
						case VerticalPosition.Bottom:
							goto IL_67;
						case VerticalPosition.Inside:
							goto IL_B0;
						case VerticalPosition.Outside:
							goto IL_94;
						default:
							if (true)
							{
							}
							num = 1;
							continue;
						}
						break;
					case 1:
						num = 2;
						continue;
					case 2:
						goto IL_AE;
					}
					break;
				}
			}
			IL_BF:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_D5;
			}
		}
		IL_67:
		return ClipboardData.b("ᅲᩴͶ൸ᑺၼ", a_);
		IL_76:
		return ClipboardData.b("ၲၴ᥶൸Ṻོ", a_);
		IL_85:
		return ClipboardData.b("ᩲ᭴᭶ၸᕺ᡼", a_);
		IL_94:
		return ClipboardData.b("ᱲtͶ੸ቺ᥼᩾", a_);
		IL_AE:
		goto IL_EA;
		IL_B0:
		return ClipboardData.b("ᩲ᭴Ѷၸὺ᡼", a_);
		IL_D5:
		if (false)
		{
		}
		return ClipboardData.b("ݲᩴݶ", a_);
		IL_EA:
		return "";
	}

	// Token: 0x06002DB1 RID: 11697 RVA: 0x002BB258 File Offset: 0x002BA258
	internal static HorizontalPosition ᜈ(string A_0)
	{
		int a_ = 9;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_158:
			num = 0;
			break;
		default:
			if (false)
			{
			}
			num = 11;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!(A_0 == ClipboardData.b("ٮὰrᱴ፶ᱸ", a_)))
				{
					num = 3;
					continue;
				}
				return HorizontalPosition.Inside;
			case 1:
				goto IL_158;
			case 2:
				if (!(A_0 == ClipboardData.b("ᵮᡰᑲᵴͶ", a_)))
				{
					num = 1;
					continue;
				}
				return HorizontalPosition.Right;
			case 3:
				num = 10;
				continue;
			case 4:
				if (true)
				{
				}
				num = 7;
				continue;
			case 5:
				num = 2;
				continue;
			case 6:
				if (!(A_0 == ClipboardData.b("౮ᑰᵲŴቶ୸", a_)))
				{
					num = 5;
					continue;
				}
				return HorizontalPosition.Center;
			case 7:
				goto IL_97;
			case 8:
				num = 6;
				continue;
			case 9:
				if (!(A_0 == ClipboardData.b("ͮᑰᕲŴ", a_)))
				{
					num = 8;
					continue;
				}
				return HorizontalPosition.Left;
			case 10:
				if (!(A_0 == ClipboardData.b("nѰݲٴṶᵸṺ", a_)))
				{
					num = 4;
					continue;
				}
				return HorizontalPosition.Outside;
			case 12:
				num = 9;
				continue;
			}
			if (A_0 == null)
			{
				return HorizontalPosition.None;
			}
			num = 12;
		}
		return HorizontalPosition.Center;
		IL_97:
		return HorizontalPosition.None;
	}

	// Token: 0x06002DB2 RID: 11698 RVA: 0x002BB3F8 File Offset: 0x002BA3F8
	internal static string ᜀ(HorizontalPosition A_0)
	{
		int a_ = 8;
		for (;;)
		{
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
						case HorizontalPosition.Left:
							goto IL_B2;
						case HorizontalPosition.Center:
							goto IL_5C;
						case HorizontalPosition.Right:
							goto IL_C1;
						case HorizontalPosition.Inside:
							goto IL_4D;
						case HorizontalPosition.Outside:
							goto IL_7D;
						default:
							num = 1;
							continue;
						}
						break;
					case 1:
						num = 2;
						continue;
					case 2:
						goto IL_73;
					}
					break;
				}
			}
			IL_7D:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			}
			goto Block_2;
		}
		IL_4D:
		return ClipboardData.b("ݭṯűᵳትᵷ", a_);
		IL_5C:
		return ClipboardData.b("൭ᕯᱱs፵੷", a_);
		IL_73:
		if (true)
		{
		}
		return "";
		Block_2:
		if (false)
		{
		}
		return ClipboardData.b("ŭկٱݳήᱷό", a_);
		IL_B2:
		return ClipboardData.b("ɭᕯᑱs", a_);
		IL_C1:
		return ClipboardData.b("ᱭ᥯ᕱᱳɵ", a_);
	}

	// Token: 0x06002DB3 RID: 11699 RVA: 0x002BB4DC File Offset: 0x002BA4DC
	internal static RelativeHorizontalPosition ᜇ(string A_0)
	{
		int a_ = 13;
		for (;;)
		{
			IL_09:
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!(A_0 == ClipboardData.b("ݲၴྲྀ൸", a_)))
					{
						num = 6;
						continue;
					}
					goto IL_59;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 2:
					if (!(A_0 == ClipboardData.b("Ͳᑴၶᱸ", a_)))
					{
						num = 1;
						continue;
					}
					return RelativeHorizontalPosition.Page;
				case 4:
					if (!(A_0 == ClipboardData.b("ṲᑴնṸቺ፼", a_)))
					{
						num = 7;
						continue;
					}
					return RelativeHorizontalPosition.Margin;
				case 5:
					goto IL_6B;
				case 6:
					num = 4;
					continue;
				case 7:
					num = 2;
					continue;
				case 8:
					num = 0;
					continue;
				}
				if (A_0 == null)
				{
					return RelativeHorizontalPosition.Column;
				}
				num = 8;
			}
		}
		IL_59:
		if (true)
		{
		}
		return RelativeHorizontalPosition.Column;
		IL_6B:
		return RelativeHorizontalPosition.Column;
	}

	// Token: 0x06002DB4 RID: 11700 RVA: 0x002BB604 File Offset: 0x002BA604
	internal static string ᜀ(RelativeHorizontalPosition A_0)
	{
		int a_ = 12;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			for (;;)
			{
				IL_27:
				switch (num)
				{
				case 0:
					switch (A_0)
					{
					case RelativeHorizontalPosition.Margin:
						goto IL_69;
					case RelativeHorizontalPosition.Page:
						goto IL_99;
					case RelativeHorizontalPosition.Column:
						goto IL_78;
					default:
						num = 1;
						continue;
					}
					break;
				case 1:
					if (true)
					{
					}
					num = 2;
					continue;
				case 2:
					goto IL_97;
				}
				goto IL_39;
			}
			IL_69:
			return ClipboardData.b("άᕳѵί፹ቻ", a_);
			IL_78:
			return ClipboardData.b("ٱᅳ๵౷", a_);
			IL_97:
			return "";
			IL_99:
			return ClipboardData.b("ɱᕳᅵᵷ", a_);
		default:
			if (false)
			{
			}
			break;
		}
		IL_39:
		num = 0;
		goto IL_27;
	}

	// Token: 0x06002DB5 RID: 11701 RVA: 0x002BB6C0 File Offset: 0x002BA6C0
	internal static RelativeVerticalPosition ᜆ(string A_0)
	{
		int a_ = 0;
		for (;;)
		{
			IL_09:
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 4;
					continue;
				case 1:
					num = 8;
					continue;
				case 2:
					if (!(A_0 == ClipboardData.b("ᙥ१൩५", a_)))
					{
						num = 6;
						continue;
					}
					return RelativeVerticalPosition.Page;
				case 3:
					if (true)
					{
					}
					break;
				case 4:
					if (!(A_0 == ClipboardData.b("୥१ᡩ୫ݭṯ", a_)))
					{
						num = 5;
						continue;
					}
					return RelativeVerticalPosition.Margin;
				case 5:
					num = 2;
					continue;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					default:
						if (false)
						{
						}
						num = 7;
						continue;
					}
					break;
				case 7:
					goto IL_6B;
				case 8:
					if (!(A_0 == ClipboardData.b("ብ൧ቩᡫ", a_)))
					{
						num = 0;
						continue;
					}
					return RelativeVerticalPosition.Paragraph;
				}
				if (A_0 == null)
				{
					return RelativeVerticalPosition.Margin;
				}
				num = 1;
			}
		}
		return RelativeVerticalPosition.Paragraph;
		IL_6B:
		return RelativeVerticalPosition.Margin;
	}

	// Token: 0x06002DB6 RID: 11702 RVA: 0x002BB7E8 File Offset: 0x002BA7E8
	internal static string ᜀ(RelativeVerticalPosition A_0)
	{
		int a_ = 4;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			for (;;)
			{
				IL_27:
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					goto IL_97;
				case 2:
					switch (A_0)
					{
					case RelativeVerticalPosition.Margin:
						goto IL_71;
					case RelativeVerticalPosition.Page:
						goto IL_99;
					case RelativeVerticalPosition.Paragraph:
						goto IL_80;
					default:
						num = 0;
						continue;
					}
					break;
				}
				goto IL_39;
			}
			IL_71:
			return ClipboardData.b("ݩ൫ᱭᝯ᭱ᩳ", a_);
			IL_80:
			return ClipboardData.b("ṩ५᙭ѯ", a_);
			IL_97:
			return "";
			IL_99:
			return ClipboardData.b("ᩩ൫७ᕯ", a_);
		default:
			if (false)
			{
			}
			break;
		}
		IL_39:
		if (true)
		{
		}
		num = 2;
		goto IL_27;
	}

	// Token: 0x06002DB7 RID: 11703 RVA: 0x002BB8A4 File Offset: 0x002BA8A4
	internal static TextWrappingStyle ᜅ(string A_0)
	{
		int a_ = 2;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_1CC:
			num = 8;
			break;
		default:
			if (false)
			{
			}
			num = 3;
			break;
		}
		int num2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_88;
			case 1:
				if (true)
				{
				}
				num = 7;
				continue;
			case 2:
				goto IL_1AA;
			case 4:
				spr᧓.ឯ = new Dictionary<string, int>(7)
				{
					{
						ClipboardData.b("१Ὡᡫŭ", a_),
						0
					},
					{
						ClipboardData.b("٧թᡫⱭᕯűᵳትᵷ", a_),
						1
					},
					{
						ClipboardData.b("٧թᡫ䍭ቯ᝱ݳήᱷό", a_),
						2
					},
					{
						ClipboardData.b("१ᡩͫ᭭ṯᙱ", a_),
						3
					},
					{
						ClipboardData.b("ᱧͩ୫٭ѯ", a_),
						4
					},
					{
						ClipboardData.b("ᱧɩṫŭկᕱᱳ", a_),
						5
					},
					{
						ClipboardData.b("٧թɫ୭", a_),
						6
					}
				};
				num = 0;
				continue;
			case 5:
				num = 2;
				continue;
			case 6:
				if (spr᧓.ឯ.TryGetValue(A_0, out num2))
				{
					num = 5;
					continue;
				}
				return TextWrappingStyle.InFrontOfText;
			case 7:
				if (spr᧓.ឯ == null)
				{
					num = 4;
					continue;
				}
				goto IL_88;
			case 8:
				num = 9;
				continue;
			case 9:
				goto IL_172;
			}
			if (A_0 != null)
			{
				num = 1;
				continue;
			}
			break;
			IL_88:
			num = 6;
		}
		IL_172:
		return TextWrappingStyle.InFrontOfText;
		IL_1AA:
		switch (num2)
		{
		case 0:
			return TextWrappingStyle.Inline;
		case 1:
			return TextWrappingStyle.TopAndBottom;
		case 2:
			return TextWrappingStyle.TopAndBottom;
		case 3:
			return TextWrappingStyle.Square;
		case 4:
			return TextWrappingStyle.Tight;
		case 5:
			return TextWrappingStyle.Through;
		case 6:
			return TextWrappingStyle.InFrontOfText;
		}
		goto IL_1CC;
	}

	// Token: 0x06002DB8 RID: 11704 RVA: 0x002BBA8C File Offset: 0x002BAA8C
	internal static string ᜀ(TextWrappingStyle A_0, bool A_1)
	{
		int a_ = 9;
		for (;;)
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 2;
					continue;
				case 1:
					if (!A_1)
					{
						num = 3;
						continue;
					}
					goto IL_6E;
				case 2:
					goto IL_C2;
				case 3:
					goto IL_117;
				case 4:
					switch (A_0)
					{
					case TextWrappingStyle.Inline:
						goto IL_99;
					case TextWrappingStyle.TopAndBottom:
						num = 1;
						continue;
					case TextWrappingStyle.Square:
						goto IL_C4;
					case TextWrappingStyle.InFrontOfText:
						goto IL_D3;
					case TextWrappingStyle.Tight:
						goto IL_57;
					case TextWrappingStyle.Through:
						goto IL_A8;
					default:
						num = 0;
						continue;
					}
					break;
				}
				break;
			}
		}
		IL_57:
		if (true)
		{
		}
		return ClipboardData.b("᭮ᡰᑲᵴͶ", a_);
		IL_6E:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_EC:
			return ClipboardData.b("ŮṰݲ塴ᕶᱸࡺᑼ᭾", a_);
		default:
			if (false)
			{
			}
			return ClipboardData.b("ŮṰݲ㝴ቶ੸ቺ᥼᩾", a_);
		}
		IL_99:
		return ClipboardData.b("๮Ѱݲᩴ", a_);
		IL_A8:
		return ClipboardData.b("᭮ᥰŲᩴɶṸ፺", a_);
		IL_C2:
		return "";
		IL_C4:
		return ClipboardData.b("๮Ͱᱲt᥶ᵸ", a_);
		IL_D3:
		return ClipboardData.b("ŮṰᵲၴ", a_);
		IL_117:
		goto IL_EC;
	}

	// Token: 0x06002DB9 RID: 11705 RVA: 0x002BBBB8 File Offset: 0x002BABB8
	internal static HeightRule ᜄ(string A_0)
	{
		int a_ = 16;
		int num = 1;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_87;
			default:
				if (false)
				{
				}
				switch (num)
				{
				case 0:
					if (!(A_0 == ClipboardData.b("፵w᭹ύ੽", a_)))
					{
						if (true)
						{
						}
						num = 5;
						continue;
					}
					return HeightRule.Exactly;
				case 2:
					num = 3;
					continue;
				case 3:
					goto IL_87;
				case 4:
					if (!(A_0 == ClipboardData.b("᝵౷坹ၻ᭽", a_)))
					{
						num = 2;
						continue;
					}
					return HeightRule.AtLeast;
				case 5:
					num = 8;
					continue;
				case 6:
					num = 7;
					continue;
				case 7:
					if (!(A_0 == ClipboardData.b("᝵൷๹፻", a_)))
					{
						num = 9;
						continue;
					}
					return HeightRule.Auto;
				case 8:
					if (!(A_0 == ClipboardData.b("᝵౷㙹᥻ώ", a_)))
					{
						num = 10;
						continue;
					}
					return HeightRule.AtLeast;
				case 9:
					num = 0;
					continue;
				case 10:
					num = 4;
					continue;
				}
				if (A_0 == null)
				{
					return HeightRule.Auto;
				}
				num = 6;
				break;
			}
		}
		return HeightRule.Exactly;
		IL_87:
		return HeightRule.Auto;
	}

	// Token: 0x06002DBA RID: 11706 RVA: 0x002BBD1C File Offset: 0x002BAD1C
	internal static string ᜀ(HeightRule A_0, bool A_1)
	{
		int a_ = 18;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_8D:
			num = 0;
			break;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			goto IL_49;
		}
		for (;;)
		{
			IL_2F:
			switch (num)
			{
			case 0:
				goto IL_95;
			case 1:
				if (!A_1)
				{
					num = 4;
					continue;
				}
				goto IL_97;
			case 2:
				goto IL_6D;
			case 3:
				switch (A_0)
				{
				case HeightRule.AtLeast:
					num = 1;
					continue;
				case HeightRule.Exactly:
					goto IL_7E;
				case HeightRule.Auto:
					goto IL_A6;
				default:
					num = 2;
					continue;
				}
				break;
			case 4:
				goto IL_D8;
			}
			goto IL_49;
		}
		IL_6D:
		goto IL_8D;
		IL_7E:
		return ClipboardData.b("ᵷɹᵻᵽ", a_);
		IL_95:
		return "";
		IL_97:
		return ClipboardData.b("᥷๹ほ᭽", a_);
		IL_A6:
		return ClipboardData.b("᥷ཹࡻᅽ", a_);
		IL_D8:
		return ClipboardData.b("᥷๹养ች", a_);
		IL_49:
		num = 3;
		goto IL_2F;
	}

	// Token: 0x06002DBB RID: 11707 RVA: 0x002BBE08 File Offset: 0x002BAE08
	internal static LineSpacingRule ᜃ(string A_0)
	{
		int a_ = 1;
		int num = 0;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_87;
			default:
				if (false)
				{
				}
				switch (num)
				{
				case 1:
					if (!(A_0 == ClipboardData.b("٦ᵨ❪࡬๮ɰݲ", a_)))
					{
						num = 6;
						continue;
					}
					return LineSpacingRule.AtLeast;
				case 2:
					num = 1;
					continue;
				case 3:
					if (true)
					{
					}
					num = 5;
					continue;
				case 4:
					num = 10;
					continue;
				case 5:
					goto IL_87;
				case 6:
					num = 8;
					continue;
				case 7:
					num = 9;
					continue;
				case 8:
					if (!(A_0 == ClipboardData.b("٦ᵨ䙪Ŭ੮ၰrŴ", a_)))
					{
						num = 3;
						continue;
					}
					return LineSpacingRule.AtLeast;
				case 9:
					if (!(A_0 == ClipboardData.b("٦ᱨὪɬ", a_)))
					{
						num = 4;
						continue;
					}
					return LineSpacingRule.Multiple;
				case 10:
					if (!(A_0 == ClipboardData.b("ɦᅨ੪๬᭮", a_)))
					{
						num = 2;
						continue;
					}
					return LineSpacingRule.Exactly;
				}
				if (A_0 == null)
				{
					return LineSpacingRule.AtLeast;
				}
				num = 7;
				break;
			}
		}
		return LineSpacingRule.Exactly;
		IL_87:
		return LineSpacingRule.AtLeast;
	}

	// Token: 0x06002DBC RID: 11708 RVA: 0x002BBF6C File Offset: 0x002BAF6C
	internal static string ᜀ(LineSpacingRule A_0, bool A_1)
	{
		int a_ = 8;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_8D:
			num = 1;
			break;
		default:
			if (false)
			{
			}
			goto IL_41;
		}
		for (;;)
		{
			IL_27:
			switch (num)
			{
			case 0:
				goto IL_6D;
			case 1:
				goto IL_95;
			case 2:
				switch (A_0)
				{
				case LineSpacingRule.AtLeast:
					num = 4;
					continue;
				case LineSpacingRule.Exactly:
					goto IL_7E;
				case LineSpacingRule.Multiple:
					goto IL_A6;
				default:
					num = 0;
					continue;
				}
				break;
			case 3:
				goto IL_D8;
			case 4:
				if (!A_1)
				{
					num = 3;
					continue;
				}
				goto IL_97;
			}
			goto IL_41;
		}
		IL_6D:
		goto IL_8D;
		IL_7E:
		return ClipboardData.b("୭࡯፱ᝳɵ", a_);
		IL_95:
		return "";
		IL_97:
		return ClipboardData.b("཭ѯ㹱ᅳ᝵୷๹", a_);
		IL_A6:
		return ClipboardData.b("཭կٱ᭳", a_);
		IL_D8:
		return ClipboardData.b("཭ѯ影ᡳ፵᥷ॹࡻ", a_);
		IL_41:
		if (true)
		{
		}
		num = 2;
		goto IL_27;
	}

	// Token: 0x06002DBD RID: 11709 RVA: 0x002BC058 File Offset: 0x002BB058
	internal static Spire.Doc.Documents.HorizontalAlignment ᜂ(string A_0)
	{
		int a_ = 3;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_14E;
			case 1:
				goto IL_5A;
			case 2:
				num = 0;
				continue;
			case 3:
				goto IL_19A;
			case 4:
				spr᧓.ឰ = new Dictionary<string, int>(7)
				{
					{
						ClipboardData.b("ᩨὪ౬ᵮհ", a_),
						0
					},
					{
						ClipboardData.b("ը๪୬᭮", a_),
						1
					},
					{
						ClipboardData.b("੨๪ͬ᭮ᑰŲ", a_),
						2
					},
					{
						ClipboardData.b("᭨ɪ੬ݮհ", a_),
						3
					},
					{
						ClipboardData.b("౨ժ६", a_),
						4
					},
					{
						ClipboardData.b("୨Ѫᥬݮ", a_),
						5
					},
					{
						ClipboardData.b("൨ɪṬ᭮Ͱᩲ᝴ɶ൸Ṻ", a_),
						6
					}
				};
				num = 1;
				continue;
			case 5:
				if (spr᧓.ឰ != null)
				{
					goto IL_5A;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_19A;
				default:
					if (false)
					{
					}
					num = 4;
					continue;
				}
				break;
			case 7:
				num = 5;
				continue;
			case 8:
			{
				int num2;
				if (spr᧓.ឰ.TryGetValue(A_0, out num2))
				{
					num = 3;
					continue;
				}
				goto IL_1D7;
			}
			case 9:
			{
				int num2;
				switch (num2)
				{
				case 0:
					return Spire.Doc.Documents.HorizontalAlignment.Left;
				case 1:
					return Spire.Doc.Documents.HorizontalAlignment.Left;
				case 2:
					return Spire.Doc.Documents.HorizontalAlignment.Center;
				case 3:
					return Spire.Doc.Documents.HorizontalAlignment.Right;
				case 4:
					return Spire.Doc.Documents.HorizontalAlignment.Right;
				case 5:
					return Spire.Doc.Documents.HorizontalAlignment.Justify;
				case 6:
					return Spire.Doc.Documents.HorizontalAlignment.Distributed;
				default:
					num = 2;
					continue;
				}
				break;
			}
			}
			if (A_0 != null)
			{
				num = 7;
				continue;
			}
			goto IL_1D7;
			IL_5A:
			num = 8;
			continue;
			IL_19A:
			num = 9;
		}
		return Spire.Doc.Documents.HorizontalAlignment.Right;
		IL_14E:
		IL_1D7:
		if (true)
		{
		}
		return Spire.Doc.Documents.HorizontalAlignment.Left;
	}

	// Token: 0x06002DBE RID: 11710 RVA: 0x002BC248 File Offset: 0x002BB248
	internal static string ᜀ(Spire.Doc.Documents.HorizontalAlignment A_0)
	{
		int a_ = 0;
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
					for (;;)
					{
						switch (A_0)
						{
						case Spire.Doc.Documents.HorizontalAlignment.Left:
							goto IL_B3;
						case Spire.Doc.Documents.HorizontalAlignment.Center:
							goto IL_88;
						case Spire.Doc.Documents.HorizontalAlignment.Right:
							goto IL_C2;
						case Spire.Doc.Documents.HorizontalAlignment.Justify:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
								goto IL_73;
							}
							break;
						case Spire.Doc.Documents.HorizontalAlignment.Distributed:
							goto IL_A4;
						}
						break;
					}
					num = 0;
					continue;
				case 2:
					goto IL_A2;
				}
				break;
			}
		}
		IL_73:
		if (false)
		{
		}
		return ClipboardData.b("ѥݧṩѫ", a_);
		IL_88:
		return ClipboardData.b("ե൧ѩᡫ୭ɯ", a_);
		IL_A2:
		return "";
		IL_A4:
		return ClipboardData.b("ɥŧᥩᡫᱭ᥯ၱųɵᵷ", a_);
		IL_B3:
		return ClipboardData.b("੥൧౩ᡫ", a_);
		IL_C2:
		return ClipboardData.b("ᑥŧ൩ѫᩭ", a_);
	}

	// Token: 0x06002DBF RID: 11711 RVA: 0x002BC32C File Offset: 0x002BB32C
	internal static TabLeader ᜁ(string A_0)
	{
		int a_ = 12;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 8;
				continue;
			case 2:
				num = 7;
				continue;
			case 3:
			{
				int num2;
				if (spr᧓.ឱ.TryGetValue(A_0, out num2))
				{
					num = 9;
					continue;
				}
				return TabLeader.NoLeader;
			}
			case 4:
			{
				int num2;
				switch (num2)
				{
				case 0:
					return TabLeader.NoLeader;
				case 1:
					return TabLeader.Dotted;
				case 2:
					return TabLeader.Hyphenated;
				case 3:
					return TabLeader.Single;
				case 4:
					return TabLeader.Heavy;
				case 5:
					return TabLeader.MiddleDot;
				case 6:
					return TabLeader.MiddleDot;
				default:
					num = 2;
					continue;
				}
				break;
			}
			case 5:
				spr᧓.ឱ = new Dictionary<string, int>(7)
				{
					{
						ClipboardData.b("ᱱ᭳ᡵᵷ", a_),
						0
					},
					{
						ClipboardData.b("ᙱ᭳ɵ", a_),
						1
					},
					{
						ClipboardData.b("ᩱ൳ٵၷόቻ", a_),
						2
					},
					{
						ClipboardData.b("ݱᩳትᵷࡹཻᵽ", a_),
						3
					},
					{
						ClipboardData.b("ᩱᅳ᝵๷͹", a_),
						4
					},
					{
						ClipboardData.b("άᵳትᱷᙹ᥻㩽", a_),
						5
					},
					{
						ClipboardData.b("άᵳትᱷᙹ᥻卽", a_),
						6
					}
				};
				num = 6;
				continue;
			case 6:
				goto IL_5A;
			case 7:
				goto IL_14E;
			case 8:
				if (true)
				{
				}
				if (spr᧓.ឱ != null)
				{
					goto IL_5A;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1A2;
				default:
					if (false)
					{
					}
					num = 5;
					continue;
				}
				break;
			case 9:
				goto IL_1A2;
			}
			if (A_0 != null)
			{
				num = 0;
				continue;
			}
			return TabLeader.NoLeader;
			IL_5A:
			num = 3;
			continue;
			IL_1A2:
			num = 4;
		}
		return TabLeader.Single;
		IL_14E:
		return TabLeader.NoLeader;
	}

	// Token: 0x06002DC0 RID: 11712 RVA: 0x002BC51C File Offset: 0x002BB51C
	internal static string ᜀ(TabLeader A_0, bool A_1)
	{
		int a_ = 10;
		for (;;)
		{
			IL_41:
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
						goto IL_85;
					case 1:
						switch (A_0)
						{
						case TabLeader.NoLeader:
							goto IL_A4;
						case TabLeader.Dotted:
							goto IL_104;
						case TabLeader.Hyphenated:
							goto IL_F5;
						case TabLeader.Single:
							goto IL_95;
						case TabLeader.Heavy:
							goto IL_CF;
						case TabLeader.MiddleDot:
							num = 0;
							continue;
						default:
							num = 2;
							continue;
						}
						break;
					case 2:
						num = 4;
						continue;
					case 3:
						goto IL_93;
					case 4:
						goto IL_CD;
					}
					goto IL_41;
				}
				IL_85:
				if (A_1)
				{
					goto IL_E6;
				}
				num = 3;
			}
		}
		IL_93:
		return ClipboardData.b("ᵯ᭱ၳትᑷό养᩽", a_);
		IL_95:
		return ClipboardData.b("կᱱၳ፵੷ॹύᅽ", a_);
		IL_A4:
		return ClipboardData.b("ṯᵱᩳ፵", a_);
		IL_CD:
		return "";
		IL_CF:
		if (true)
		{
		}
		return ClipboardData.b("ᡯ᝱ᕳuŷ", a_);
		IL_E6:
		return ClipboardData.b("ᵯ᭱ၳትᑷό㡻ᅽ", a_);
		IL_F5:
		return ClipboardData.b("ᡯୱѳṵᵷᑹ", a_);
		IL_104:
		return ClipboardData.b("ᑯᵱs", a_);
	}

	// Token: 0x06002DC1 RID: 11713 RVA: 0x002BC644 File Offset: 0x002BB644
	internal static TabJustification ᜀ(string A_0)
	{
		int a_ = 5;
		int num = 8;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				num = 2;
				continue;
			case 1:
				goto IL_1FF;
			case 2:
			{
				int num2;
				switch (num2)
				{
				case 0:
					return TabJustification.Clear;
				case 1:
					return TabJustification.Left;
				case 2:
					return TabJustification.Left;
				case 3:
					return TabJustification.Centered;
				case 4:
					return TabJustification.Right;
				case 5:
					return TabJustification.Right;
				case 6:
					return TabJustification.Decimal;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1E4;
					default:
						goto IL_A1;
					}
					break;
				case 8:
					return TabJustification.List;
				case 9:
					return TabJustification.List;
				default:
					num = 5;
					continue;
				}
				break;
			}
			case 3:
				num = 4;
				continue;
			case 4:
				if (spr᧓.ឲ == null)
				{
					num = 7;
					continue;
				}
				goto IL_62;
			case 5:
				goto IL_1E4;
			case 6:
			{
				int num2;
				if (spr᧓.ឲ.TryGetValue(A_0, out num2))
				{
					num = 0;
					continue;
				}
				return TabJustification.Clear;
			}
			case 7:
				spr᧓.ឲ = new Dictionary<string, int>(10)
				{
					{
						ClipboardData.b("ࡪŬ੮ၰŲ", a_),
						0
					},
					{
						ClipboardData.b("ݪ࡬८հ", a_),
						1
					},
					{
						ClipboardData.b("ᡪᥬ๮Ͱݲ", a_),
						2
					},
					{
						ClipboardData.b("ࡪ࡬Ůհᙲݴ", a_),
						3
					},
					{
						ClipboardData.b("ᥪѬ࡮ᥰݲ", a_),
						4
					},
					{
						ClipboardData.b("๪ͬ୮", a_),
						5
					},
					{
						ClipboardData.b("ཪ࡬౮ᡰṲᑴ᭶", a_),
						6
					},
					{
						ClipboardData.b("४౬ᵮ", a_),
						7
					},
					{
						ClipboardData.b("ժᡬɮ", a_),
						8
					},
					{
						ClipboardData.b("ݪѬᱮհ", a_),
						9
					}
				};
				num = 9;
				continue;
			case 9:
				goto IL_62;
			}
			if (A_0 != null)
			{
				num = 3;
				continue;
			}
			return TabJustification.Clear;
			IL_62:
			num = 6;
			continue;
			IL_1E4:
			num = 1;
		}
		return TabJustification.Right;
		IL_A1:
		if (false)
		{
		}
		return TabJustification.Bar;
		IL_1FF:
		return TabJustification.Clear;
	}

	// Token: 0x06002DC2 RID: 11714 RVA: 0x002BC880 File Offset: 0x002BB880
	internal static string ᜀ(TabJustification A_0, bool A_1)
	{
		int a_ = 12;
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
						goto IL_6A;
					}
					goto IL_E1;
				case 1:
					num = 3;
					continue;
				case 2:
					switch (A_0)
					{
					case TabJustification.Left:
						goto IL_109;
					case TabJustification.Centered:
						goto IL_F0;
					case TabJustification.Right:
						goto IL_118;
					case TabJustification.Decimal:
						goto IL_74;
					case TabJustification.Bar:
						goto IL_CA;
					case (TabJustification)5:
						goto IL_127;
					case TabJustification.List:
						num = 0;
						continue;
					case TabJustification.Clear:
						goto IL_83;
					default:
						num = 1;
						continue;
					}
					break;
				case 3:
					goto IL_C8;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6A;
					default:
						goto IL_A8;
					}
					break;
				}
				break;
				IL_6A:
				num = 4;
			}
		}
		IL_74:
		return ClipboardData.b("ᙱᅳᕵᅷ᝹ᵻች", a_);
		IL_83:
		return ClipboardData.b("ᅱᡳ፵᥷ࡹ", a_);
		IL_A8:
		if (false)
		{
		}
		return ClipboardData.b("ṱᵳյ౷", a_);
		IL_C8:
		goto IL_127;
		IL_CA:
		if (true)
		{
		}
		return ClipboardData.b("ၱᕳѵ", a_);
		IL_E1:
		return ClipboardData.b("ᱱų᭵", a_);
		IL_F0:
		return ClipboardData.b("ᅱᅳᡵ౷ό๻", a_);
		IL_109:
		return ClipboardData.b("ṱᅳၵ౷", a_);
		IL_118:
		return ClipboardData.b("qᵳᅵၷ๹", a_);
		IL_127:
		return "";
	}
}
