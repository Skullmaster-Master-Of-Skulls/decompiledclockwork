using System;
using System.Drawing;
using Spire.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Core.Biff_Records;
using Spire.Doc.Documents;
using Spire.Doc.Formatting;

// Token: 0x0200036D RID: 877
internal class spr\u192A
{
	// Token: 0x06003125 RID: 12581 RVA: 0x002D1F78 File Offset: 0x002D0F78
	internal static void ᜁ(spr\u224E A_0, Border A_1)
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
		A_1.BorderType = (BorderStyle)A_0.ᜄ();
		A_1.Color = A_0.ᜅ();
		A_1.LineWidth = (float)A_0.ᜊ() / 8f;
		A_1.Space = (float)A_0.ᜆ() / 20f;
		A_1.Shadow = A_0.ᜋ();
	}

	// Token: 0x06003126 RID: 12582 RVA: 0x002D2000 File Offset: 0x002D1000
	internal static void ᜀ(spr\u224E A_0, Border A_1)
	{
		if (!A_1.IsDefault)
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
					break;
				default:
					goto IL_35;
				}
			}
			IL_35:
			if (false)
			{
			}
			A_0.ᜁ((byte)sprṡ.ᜁ(A_1.Color));
			A_0.ᜀ(A_1.Color);
			A_0.ᜃ((byte)A_1.BorderType);
			A_0.ᜀ((byte)Math.Round((double)(A_1.LineWidth * 8f)));
			A_0.ᜂ((byte)Math.Round((double)(A_1.Space * 20f)));
			A_0.ᜀ(A_1.Shadow);
			return;
		}
		A_0.ᜁ(0);
		A_0.ᜀ(Color.Empty);
		A_0.ᜃ(0);
		A_0.ᜀ(0);
		A_0.ᜂ(0);
		A_0.ᜀ(false);
	}

	// Token: 0x06003127 RID: 12583 RVA: 0x002D20E0 File Offset: 0x002D10E0
	internal static void ᜀ(sprΐ A_0, TabCollection A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_67:
				int num = 0;
				int num2 = 2;
				for (;;)
				{
					sprᡖ sprᡖ;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
					{
						if (false)
						{
						}
						switch (num2)
						{
						case 0:
							goto IL_16E;
						case 1:
							if (sprᡖ == null)
							{
								num2 = 7;
								continue;
							}
							goto IL_16E;
						case 2:
							goto IL_136;
						case 3:
							return;
						case 4:
							goto IL_BE;
						case 5:
						{
							int num3;
							if (num3 >= A_0.ᜉ().Length)
							{
								num2 = 3;
								continue;
							}
							Tab tab = A_1.AddTab();
							tab.DeletePosition = (float)A_0.ᜉ()[num3];
							num3++;
							num2 = 8;
							continue;
						}
						case 6:
							if (A_0.ᜉ() != null)
							{
								num2 = 10;
								continue;
							}
							return;
						case 7:
							goto IL_A6;
						case 8:
							goto IL_BE;
						case 9:
							if (num >= (int)A_0.ᜋ())
							{
								num2 = 12;
								continue;
							}
							sprᡖ = A_0.ᜊ()[num];
							num2 = 1;
							continue;
						case 10:
						{
							int num3 = 0;
							num2 = 4;
							continue;
						}
						case 11:
							goto IL_136;
						case 12:
							num2 = 6;
							continue;
						}
						goto IL_67;
						IL_BE:
						num2 = 5;
						continue;
						IL_136:
						if (true)
						{
						}
						num2 = 9;
						continue;
						IL_16E:
						Tab tab2 = A_1.AddTab();
						tab2.Position = (float)A_0.ᜈ()[num] / 20f;
						tab2.Justification = sprᡖ.ᜂ();
						tab2.TabLeader = sprᡖ.ᜁ();
						num++;
						num2 = 11;
						continue;
					}
					}
					IL_A6:
					sprᡖ = new sprᡖ(0);
					num2 = 0;
				}
			}
			return;
		}
	}

	// Token: 0x06003128 RID: 12584 RVA: 0x002D22A4 File Offset: 0x002D12A4
	private static Borders ᜀ(Borders A_0, ParagraphFormat A_1)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3B;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 1:
				return A_0;
			case 2:
				A_0 = A_1.Borders;
				if (true)
				{
				}
				num = 1;
				continue;
			}
			goto IL_38;
			IL_3B:
			num = 2;
			continue;
			IL_38:
			if (A_0 == null)
			{
				goto IL_3B;
			}
			break;
		}
		return A_0;
	}

	// Token: 0x06003129 RID: 12585 RVA: 0x002D231C File Offset: 0x002D131C
	public static void ᜀ(sprᨽ A_0, ParagraphFormat A_1)
	{
		for (;;)
		{
			A_1.Sprms = A_0.ᜪ();
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
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
					{
						if (true)
						{
						}
						Color color = A_0.\u1719().ᜂ();
						num = 4;
						continue;
					}
					case 2:
						goto IL_6F;
					case 3:
						if (A_0.\u1719().ᜁ() == TextureStyle.TextureNone)
						{
							num = 1;
							continue;
						}
						goto IL_6F;
					case 4:
					{
						Color color;
						if (!color.IsEmpty)
						{
							num = 6;
							continue;
						}
						goto IL_6F;
					}
					case 5:
						A_1.TextureStyle = A_0.\u1719().ᜁ();
						A_1.ForeColor = A_0.\u1719().ᜃ();
						A_1.BackColor = A_0.\u1719().ᜂ();
						num = 0;
						continue;
					case 6:
						A_1.BackColor = A_0.\u1719().ᜂ();
						num = 2;
						continue;
					case 7:
						if (A_0.\u1719().ᜁ() != TextureStyle.TextureNone)
						{
							num = 5;
							continue;
						}
						return;
					}
					break;
					IL_6F:
					num = 7;
				}
				break;
			}
			}
		}
	}

	// Token: 0x0600312A RID: 12586 RVA: 0x002D245C File Offset: 0x002D145C
	public static void ᜀ(sprᨽ A_0, ParagraphFormat A_1, Paragraph A_2)
	{
		switch (0)
		{
		default:
		{
			int num = 11;
			for (;;)
			{
				int num2;
				int num3;
				spr\u1CC1 spr_u1CC;
				switch (num)
				{
				case 0:
					A_0.ᜁ(A_2.ParaStyle.ParagraphFormat.ParaProps.\u173C());
					num = 5;
					continue;
				case 1:
					num = 13;
					continue;
				case 2:
					goto IL_9FD;
				case 3:
					num = 81;
					continue;
				case 4:
					goto IL_98F;
				case 5:
					goto IL_B45;
				case 6:
					if (A_2 != null)
					{
						num = 76;
						continue;
					}
					return;
				case 7:
				{
					spr\u2179 spr_u;
					spr\u192A.ᜀ(9, spr_u.ᜀ().AfterSpacing, A_0, spr_u.ᜀ());
					num = 79;
					continue;
				}
				case 8:
					A_0.ᜀ(A_0.ᜠ());
					num = 90;
					continue;
				case 9:
					num = 44;
					continue;
				case 10:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_939;
					default:
						if (false)
						{
						}
						goto IL_6A6;
					}
					break;
				case 12:
					A_1.Sprms = new sprḍ();
					num = 10;
					continue;
				case 13:
					if (A_2.ParaStyle.ParagraphFormat.Sprms.ᜄ(9313))
					{
						num = 16;
						continue;
					}
					goto IL_757;
				case 14:
					if (A_0.ᜪ().ᜇ(9281).ᜉ())
					{
						num = 62;
						continue;
					}
					goto IL_98F;
				case 15:
					num = 35;
					continue;
				case 16:
					A_0.ᜁ(A_2.ParaStyle.ParagraphFormat.ParaProps.ᜠ());
					num = 94;
					continue;
				case 17:
					goto IL_B45;
				case 18:
					if (A_2 != null)
					{
						num = 19;
						continue;
					}
					goto IL_922;
				case 19:
					num = 54;
					continue;
				case 20:
					num = 48;
					continue;
				case 21:
					A_0.ᜪ().ᜆ(9313);
					num = 84;
					continue;
				case 22:
					goto IL_287;
				case 23:
					if (!A_0.ᜪ().ᜄ(9313))
					{
						num = 3;
						continue;
					}
					goto IL_B45;
				case 24:
					num = 61;
					continue;
				case 25:
					goto IL_B45;
				case 26:
					goto IL_B45;
				case 27:
					goto IL_B45;
				case 28:
					if (!A_0.ᜪ().ᜄ(9313))
					{
						num = 68;
						continue;
					}
					goto IL_B45;
				case 29:
					return;
				case 30:
					goto IL_B45;
				case 31:
					if (A_2.ParaStyle != null)
					{
						num = 93;
						continue;
					}
					goto IL_4E3;
				case 32:
					if (num2 >= num3)
					{
						num = 20;
						continue;
					}
					spr_u1CC = A_1.Sprms.ᜁ(num2);
					num = 95;
					continue;
				case 33:
				{
					spr\u2179 spr_u = (A_2.Owner.Owner.Owner as Table).TableStyle;
					num = 38;
					continue;
				}
				case 34:
					if (A_2.IsInCell)
					{
						num = 77;
						continue;
					}
					return;
				case 35:
				{
					spr\u2179 spr_u;
					if (spr_u.ᜀ().HasValue(9))
					{
						num = 7;
						continue;
					}
					goto IL_2BC;
				}
				case 36:
					A_0.ᜁ(A_2.Format.ParaProps.\u173C());
					num = 30;
					continue;
				case 37:
					if (!A_1.IsBidi)
					{
						goto IL_939;
					}
					goto IL_B45;
				case 38:
					if (!A_1.HasValue(9))
					{
						num = 15;
						continue;
					}
					goto IL_2BC;
				case 39:
					if (A_2.Owner.Owner.Owner is Table)
					{
						num = 67;
						continue;
					}
					return;
				case 40:
					if (A_0.ᜪ().ᜄ(9313))
					{
						num = 21;
						continue;
					}
					goto IL_A49;
				case 41:
					if (A_2.Format.Sprms.ᜄ(9219))
					{
						num = 36;
						continue;
					}
					num = 52;
					continue;
				case 42:
					num = 80;
					continue;
				case 43:
					num = 64;
					continue;
				case 44:
					if (!A_0.ᜪ().ᜄ(9219))
					{
						num = 8;
						continue;
					}
					goto IL_872;
				case 45:
					num = 40;
					continue;
				case 46:
					goto IL_98F;
				case 47:
				{
					spr\u2179 spr_u;
					spr\u192A.ᜀ(52, spr_u.ᜀ().LineSpacing, A_0, spr_u.ᜀ());
					num = 29;
					continue;
				}
				case 48:
					if (A_0.ᝠ())
					{
						num = 66;
						continue;
					}
					goto IL_922;
				case 49:
				{
					spr\u2179 spr_u;
					spr\u192A.ᜀ(8, spr_u.ᜀ().BeforeSpacing, A_0, spr_u.ᜀ());
					num = 2;
					continue;
				}
				case 50:
					num = 23;
					continue;
				case 51:
				{
					spr\u2179 spr_u;
					if (spr_u.ᜀ().HasValue(8))
					{
						num = 49;
						continue;
					}
					goto IL_9FD;
				}
				case 52:
					if (A_2.Format.Sprms.ᜄ(9313))
					{
						num = 55;
						continue;
					}
					num = 31;
					continue;
				case 53:
					A_0.ᜁ(A_1.ParaProps.\u173C());
					num = 17;
					continue;
				case 54:
					if (!A_1.IsBidi)
					{
						num = 50;
						continue;
					}
					goto IL_B45;
				case 55:
					A_0.ᜁ(A_2.Format.ParaProps.ᜠ());
					num = 26;
					continue;
				case 56:
					num = 75;
					continue;
				case 57:
					num = 46;
					continue;
				case 58:
					goto IL_A27;
				case 59:
					if ((A_2.Owner.Owner.Owner as Table).TableStyle != null)
					{
						num = 33;
						continue;
					}
					return;
				case 60:
					if (A_0.ᜪ().ᜄ(9219))
					{
						num = 45;
						continue;
					}
					goto IL_A49;
				case 61:
					if (A_2 != null)
					{
						num = 85;
						continue;
					}
					goto IL_A49;
				case 62:
				{
					HorizontalAlignment horizontalAlignment = A_1.HorizontalAlignment;
					num = 98;
					continue;
				}
				case 63:
					num = 28;
					continue;
				case 64:
					if (A_0.ᝠ())
					{
						num = 24;
						continue;
					}
					goto IL_A49;
				case 65:
					goto IL_98F;
				case 66:
					num = 18;
					continue;
				case 67:
					num = 59;
					continue;
				case 68:
					A_0.ᜁ(A_1.ParaProps.\u173C());
					num = 27;
					continue;
				case 69:
					if (A_1.ParaProps.ᜪ().ᜄ(9313))
					{
						num = 72;
						continue;
					}
					num = 41;
					continue;
				case 70:
					if (!A_1.HasValue(52))
					{
						num = 97;
						continue;
					}
					return;
				case 71:
					goto IL_287;
				case 72:
					A_0.ᜁ(A_1.ParaProps.ᜠ());
					num = 25;
					continue;
				case 73:
					if (A_0.ᜪ().ᜄ(9313))
					{
						num = 9;
						continue;
					}
					goto IL_872;
				case 74:
					if (!A_1.HasValue(8))
					{
						num = 83;
						continue;
					}
					goto IL_9FD;
				case 75:
					if (A_0.ᜂ(50799))
					{
						num = 58;
						continue;
					}
					goto IL_7A4;
				case 76:
					num = 34;
					continue;
				case 77:
					num = 39;
					continue;
				case 78:
					if (A_2.ParaStyle.ParagraphFormat.Sprms.ᜄ(9219))
					{
						num = 0;
						continue;
					}
					goto IL_4E3;
				case 79:
					goto IL_2BC;
				case 80:
					if (!A_0.ᜂ(50751))
					{
						num = 56;
						continue;
					}
					goto IL_A27;
				case 81:
					if (A_1.ParaProps.ᜪ().ᜄ(9219))
					{
						num = 53;
						continue;
					}
					num = 69;
					continue;
				case 82:
					goto IL_7A4;
				case 83:
					num = 51;
					continue;
				case 84:
					goto IL_A49;
				case 85:
					num = 60;
					continue;
				case 86:
					if (!A_1.IsBidi)
					{
						num = 43;
						continue;
					}
					goto IL_A49;
				case 87:
					if (!A_0.ᜪ().ᜂ(9219))
					{
						num = 99;
						continue;
					}
					goto IL_98F;
				case 88:
					if (A_0.ᜪ().ᜄ(9281))
					{
						num = 91;
						continue;
					}
					goto IL_98F;
				case 89:
				{
					spr\u2179 spr_u;
					if (spr_u.ᜀ().HasValue(52))
					{
						num = 47;
						continue;
					}
					return;
				}
				case 90:
					goto IL_872;
				case 91:
					num = 87;
					continue;
				case 92:
					goto IL_B45;
				case 93:
					num = 78;
					continue;
				case 94:
					goto IL_B45;
				case 95:
					if (A_0.ᜂ(spr_u1CC.ᜈ()))
					{
						num = 42;
						continue;
					}
					goto IL_A27;
				case 96:
					if (A_2.ParaStyle != null)
					{
						num = 1;
						continue;
					}
					goto IL_757;
				case 97:
					num = 89;
					continue;
				case 98:
				{
					HorizontalAlignment horizontalAlignment;
					switch (horizontalAlignment)
					{
					case HorizontalAlignment.Left:
						A_0.ᜀ(ParagraphJustify.Right);
						num = 65;
						continue;
					case HorizontalAlignment.Center:
						goto IL_98F;
					case HorizontalAlignment.Right:
						A_0.ᜀ(ParagraphJustify.Left);
						num = 4;
						continue;
					default:
						num = 57;
						continue;
					}
					break;
				}
				case 99:
					num = 14;
					continue;
				}
				if (A_1.Sprms == null)
				{
					num = 12;
					continue;
				}
				goto IL_6A6;
				IL_287:
				num = 32;
				continue;
				IL_2BC:
				num = 74;
				continue;
				IL_4E3:
				if (true)
				{
				}
				num = 96;
				continue;
				IL_6A6:
				spr_u1CC = null;
				A_1.Sprms.ᜆ(25703);
				num2 = 0;
				num3 = A_1.Sprms.ᜈ();
				num = 22;
				continue;
				IL_757:
				A_0.ᜁ(A_1.ParaProps.\u173C());
				num = 92;
				continue;
				IL_7A4:
				num2++;
				num = 71;
				continue;
				IL_872:
				num = 6;
				continue;
				IL_922:
				num = 37;
				continue;
				IL_939:
				num = 63;
				continue;
				IL_98F:
				num = 73;
				continue;
				IL_9FD:
				num = 70;
				continue;
				IL_A27:
				A_0.ᜪ().ᜂ().Add(spr_u1CC);
				num = 82;
				continue;
				IL_A49:
				num = 88;
				continue;
				IL_B45:
				num = 86;
			}
			return;
		}
		}
	}

	// Token: 0x0600312B RID: 12587 RVA: 0x002D3068 File Offset: 0x002D2068
	public static void ᜀ(sprᨽ A_0, TableRow A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 5;
			for (;;)
			{
				short num2;
				sprḍ sprḍ;
				int num3;
				switch (num)
				{
				case 0:
					if (A_1.RowFormat.RowIndent == 0f)
					{
						num = 6;
						continue;
					}
					goto IL_27A;
				case 1:
					goto IL_24B;
				case 2:
					num = 9;
					continue;
				case 3:
					if (!A_1.RowFormat.IsBreakAcrossPages)
					{
						num = 31;
						continue;
					}
					goto IL_24B;
				case 4:
					num2 = (short)Math.Round((double)(A_1.Height * 20f));
					num = 20;
					continue;
				case 6:
					num = 21;
					continue;
				case 7:
					goto IL_10C;
				case 8:
					if (A_0.ᜂ(sprḍ.ᜇ(num3).ᜈ()))
					{
						num = 2;
						continue;
					}
					goto IL_34A;
				case 9:
					if (!A_0.ᜂ(50751))
					{
						num = 10;
						continue;
					}
					goto IL_34A;
				case 10:
					num = 28;
					continue;
				case 11:
					A_0.ᜂ((ParagraphJustify)A_1.RowFormat.HorizontalAlignment);
					num = 35;
					continue;
				case 12:
					num = 24;
					continue;
				case 13:
					num = 17;
					continue;
				case 14:
					if (true)
					{
					}
					goto IL_DF;
				case 15:
				{
					int num4;
					if (num3 >= num4)
					{
						num = 18;
						continue;
					}
					num = 8;
					continue;
				}
				case 16:
				{
					sprḍ = new sprḍ(A_1.DataArray, 0);
					num3 = 0;
					int num4 = sprḍ.ᜈ();
					num = 33;
					continue;
				}
				case 17:
					if (A_1.HeightType != TableRowHeightType.AtLeast)
					{
						num = 7;
						continue;
					}
					goto IL_3B1;
				case 18:
					goto IL_3C7;
				case 19:
					goto IL_1A3;
				case 20:
					if (num2 < 0)
					{
						num = 13;
						continue;
					}
					goto IL_10C;
				case 21:
					if (A_1.RowFormat.LeftIndent == 0f)
					{
						num = 30;
						continue;
					}
					return;
				case 22:
					num = 26;
					continue;
				case 23:
					if (A_1.Height != 0f)
					{
						goto IL_3E3;
					}
					goto IL_DF;
				case 24:
					if (A_1.HeightType == TableRowHeightType.Exactly)
					{
						num = 32;
						continue;
					}
					goto IL_1A3;
				case 25:
					if (A_1.DataArray.Length > 0)
					{
						num = 16;
						continue;
					}
					goto IL_3C7;
				case 26:
					if (A_1.DataArray.Length < 400)
					{
						num = 39;
						continue;
					}
					goto IL_3C7;
				case 27:
					if (A_1.RowFormat.HorizontalAlignment != RowAlignment.Left)
					{
						num = 11;
						continue;
					}
					goto IL_384;
				case 28:
					if (A_0.ᜂ(50799))
					{
						num = 34;
						continue;
					}
					goto IL_36F;
				case 29:
					goto IL_36F;
				case 30:
					goto IL_27A;
				case 31:
					A_0.ᜋ(true);
					A_0.ᜉ(true);
					num = 1;
					continue;
				case 32:
					goto IL_3B1;
				case 33:
					goto IL_2BF;
				case 34:
					goto IL_34A;
				case 35:
					goto IL_384;
				case 36:
					goto IL_2BF;
				case 37:
					return;
				case 38:
					if (num2 > 0)
					{
						num = 12;
						continue;
					}
					goto IL_1A3;
				case 39:
					num = 25;
					continue;
				}
				if (A_1.DataArray != null)
				{
					num = 22;
					continue;
				}
				goto IL_3C7;
				IL_DF:
				num = 27;
				continue;
				IL_10C:
				num = 38;
				continue;
				IL_1A3:
				A_0.ᜄ((ushort)num2);
				num = 14;
				continue;
				IL_24B:
				num = 0;
				continue;
				IL_27A:
				A_0.\u1712((short)(A_1.RowFormat.RowIndent * 20f));
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_3E3:
					num = 4;
					continue;
				default:
					if (false)
					{
					}
					num = 37;
					continue;
				}
				IL_2BF:
				num = 15;
				continue;
				IL_34A:
				A_0.ᜪ().ᜂ().Add(sprḍ.ᜇ(num3));
				num = 29;
				continue;
				IL_36F:
				num3++;
				num = 36;
				continue;
				IL_384:
				num = 3;
				continue;
				IL_3B1:
				num2 *= -1;
				num = 19;
				continue;
				IL_3C7:
				num = 23;
			}
			return;
		}
		}
	}

	// Token: 0x0600312C RID: 12588 RVA: 0x002D3514 File Offset: 0x002D2514
	internal static void ᜀ(int A_0, object A_1, sprᨽ A_2, ParagraphFormat A_3)
	{
		for (;;)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_259:
				switch (A_0)
				{
				case 52:
					goto IL_1F4;
				case 53:
				{
					spr\u20F1 spr_u20F = new spr\u20F1();
					spr_u20F.ᜀ((short)Math.Round((double)(A_3.LineSpacing * 20f)));
					spr_u20F.ᜀ((LineSpacingRule)A_1);
					A_2.ᜀ(spr_u20F);
					num = 19;
					break;
				}
				case 54:
					num = 5;
					break;
				case 55:
					num = 15;
					break;
				case 56:
					goto IL_4B7;
				default:
					num = 1;
					break;
				}
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
					return;
				case 1:
					num = 11;
					continue;
				case 2:
					num = 17;
					continue;
				case 3:
					if (A_2.\u173D() == 1)
					{
						num = 8;
						continue;
					}
					return;
				case 4:
					goto IL_259;
				case 5:
					A_2.ᜃ(((bool)A_1) ? 1 : 0);
					num = 9;
					continue;
				case 6:
					if (A_0 <= 33)
					{
						num = 10;
						continue;
					}
					num = 4;
					continue;
				case 7:
					return;
				case 8:
					goto IL_44E;
				case 9:
					if (A_2.\u1734() == 1)
					{
						num = 18;
						continue;
					}
					return;
				case 10:
					num = 20;
					continue;
				case 11:
					switch (A_0)
					{
					case 65:
						goto IL_100;
					case 66:
					case 67:
					case 72:
					case 73:
					case 74:
					case 75:
					case 76:
					case 77:
					case 79:
					case 83:
					case 84:
						return;
					case 68:
						goto IL_466;
					case 69:
						goto IL_40C;
					case 70:
						goto IL_11A;
					case 71:
						goto IL_171;
					case 78:
						goto IL_134;
					case 80:
						goto IL_4AA;
					case 81:
						goto IL_3A6;
					case 82:
						goto IL_459;
					case 85:
						goto IL_CD;
					case 86:
						goto IL_227;
					case 87:
						goto IL_3B3;
					default:
						num = 12;
						continue;
					}
					break;
				case 12:
					return;
				case 13:
					return;
				case 14:
					return;
				case 15:
					A_2.ᜄ(((bool)A_1) ? 1 : 0);
					num = 3;
					continue;
				case 16:
					return;
				case 17:
					switch (A_0)
					{
					case 30:
						goto IL_3CD;
					case 31:
						goto IL_10D;
					case 32:
						spr\u192A.ᜀ(A_3, A_2);
						num = 7;
						continue;
					case 33:
						goto IL_4A2;
					default:
						num = 16;
						continue;
					}
					break;
				case 18:
					A_2.ᜂ(100);
					num = 0;
					continue;
				case 19:
					goto IL_52C;
				case 20:
					switch (A_0)
					{
					case 0:
						goto IL_A7;
					case 1:
					case 4:
					case 7:
					case 13:
					case 14:
					case 15:
					case 16:
					case 17:
					case 18:
					case 19:
						return;
					case 2:
						goto IL_1C1;
					case 3:
						goto IL_36B;
					case 5:
						goto IL_338;
					case 6:
						goto IL_241;
					case 8:
						goto IL_31E;
					case 9:
						A_2.ᜅ((ushort)Math.Round((double)((float)A_1 * 20f)));
						num = 14;
						continue;
					case 10:
						goto IL_C0;
					case 11:
						goto IL_480;
					case 12:
						A_2.ᜃ((bool)A_1);
						num = 13;
						continue;
					case 20:
						goto IL_48D;
					case 21:
						goto IL_49A;
					default:
						num = 2;
						continue;
					}
					break;
				}
				break;
			}
		}
		IL_A7:
		A_2.ᜀ((ParagraphJustify)((HorizontalAlignment)A_1));
		A_2.ᜁ((ParagraphJustify)((HorizontalAlignment)A_1));
		return;
		IL_C0:
		A_2.ᜑ((bool)A_1);
		return;
		IL_CD:
		A_2.ᜈ((short)Math.Round((double)((float)A_1 * 100f)));
		return;
		IL_100:
		A_2.\u1713((bool)A_1);
		return;
		IL_10D:
		A_2.ᜆ((bool)A_1);
		return;
		IL_11A:
		A_2.ᜌ((short)Math.Round((double)((float)A_1 * 20f)));
		return;
		IL_134:
		A_2.ᜀ((bool)A_1);
		return;
		IL_171:
		A_2.ᜊ((bool)A_1);
		return;
		IL_1C1:
		A_2.ᜅ((short)Math.Round((double)((float)A_1 * 20f)));
		A_2.ᜂ((short)Math.Round((double)((float)A_1 * 20f)));
		return;
		IL_1F4:
		spr\u20F1 spr_u20F2 = new spr\u20F1();
		spr_u20F2.ᜀ((short)Math.Round((double)((float)A_1 * 20f)));
		spr_u20F2.ᜀ(A_3.LineSpacingRule);
		A_2.ᜀ(spr_u20F2);
		return;
		IL_227:
		A_2.ᜇ((short)Math.Round((double)((float)A_1 * 100f)));
		return;
		IL_241:
		A_2.ᜏ((bool)A_1);
		return;
		IL_31E:
		A_2.ᜂ((ushort)Math.Round((double)((float)A_1 * 20f)));
		return;
		IL_338:
		A_2.ᜄ((short)Math.Round((double)((float)A_1 * 20f)));
		A_2.ᜌ((short)Math.Round((double)((float)A_1 * 20f)));
		return;
		IL_36B:
		if (true)
		{
		}
		A_2.\u170D((short)Math.Round((double)((float)A_1 * 20f)));
		A_2.ᜉ((short)Math.Round((double)((float)A_1 * 20f)));
		return;
		IL_3A6:
		A_2.ᜈ((bool)A_1);
		return;
		IL_3B3:
		A_2.ᜋ((short)Math.Round((double)((float)A_1 * 100f)));
		return;
		IL_3CD:
		spr\u192A.ᜀ(A_1 as TabCollection, A_2);
		return;
		IL_40C:
		A_2.ᜉ((short)Math.Round((double)((float)A_1 * 20f)));
		return;
		IL_44E:
		A_2.ᜅ(100);
		return;
		IL_459:
		A_2.\u170D((bool)A_1);
		return;
		IL_466:
		A_2.ᜂ((short)Math.Round((double)((float)A_1 * 20f)));
		return;
		IL_480:
		A_2.ᜐ((bool)A_1);
		return;
		IL_48D:
		spr\u192A.ᜀ(A_1 as Borders, A_2);
		return;
		IL_49A:
		spr\u192A.ᜀ(A_3, A_2);
		return;
		IL_4A2:
		spr\u192A.ᜀ(A_3, A_2);
		return;
		IL_4AA:
		A_2.ᜄ((bool)A_1);
		return;
		IL_4B7:
		A_2.ᜀ((byte)A_1);
		return;
		IL_52C:;
	}

	// Token: 0x0600312D RID: 12589 RVA: 0x002D3AD4 File Offset: 0x002D2AD4
	private static void ᜀ(TabCollection A_0, sprᨽ A_1)
	{
		switch (0)
		{
		default:
		{
			sprΐ sprΐ;
			for (;;)
			{
				bool flag = false;
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				int num5 = 7;
				for (;;)
				{
					switch (num5)
					{
					case 0:
						goto IL_DB;
					case 1:
						goto IL_1E8;
					case 2:
					{
						sprΐ = new sprΐ((byte)num);
						short[] array = new short[(int)((byte)A_0.Count) - num];
						int num6 = 0;
						int count = A_0.Count;
						num5 = 19;
						continue;
					}
					case 3:
					{
						short[] array;
						sprΐ.ᜀ(array);
						num5 = 5;
						continue;
					}
					case 4:
						if (num4 >= A_0.Count)
						{
							num5 = 2;
							continue;
						}
						num5 = 14;
						continue;
					case 5:
						goto IL_187;
					case 6:
						if (true)
						{
						}
						goto IL_80;
					case 7:
						goto IL_1C1;
					case 8:
						num5 = 17;
						continue;
					case 9:
					{
						int num6;
						int count;
						if (num6 >= count)
						{
							num5 = 8;
							continue;
						}
						Tab tab = A_0[num6];
						num5 = 12;
						continue;
					}
					case 10:
						goto IL_1C1;
					case 11:
					{
						Tab tab;
						sprΐ.ᜈ()[num3] = (short)Math.Round((double)(tab.Position * 20f));
						sprΐ.ᜊ()[num3] = new sprᡖ(tab.Justification, tab.TabLeader);
						num3++;
						num5 = 18;
						continue;
					}
					case 12:
					{
						Tab tab;
						if (tab.DeletePosition != 0f)
						{
							num5 = 13;
							continue;
						}
						num5 = 16;
						continue;
					}
					case 13:
					{
						flag = true;
						short[] array;
						Tab tab;
						array[num2] = (short)tab.DeletePosition;
						num2++;
						num5 = 6;
						continue;
					}
					case 14:
						if (A_0[num4].DeletePosition == 0f)
						{
							num5 = 15;
							continue;
						}
						goto IL_DB;
					case 15:
						num++;
						num5 = 0;
						continue;
					case 16:
						if (num > 0)
						{
							num5 = 11;
							continue;
						}
						goto IL_80;
					case 17:
						if (flag)
						{
							num5 = 3;
							continue;
						}
						goto IL_2A2;
					case 18:
						IL_16D:
						goto IL_80;
					case 19:
						goto IL_1E8;
					}
					break;
					IL_80:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_16D;
					default:
					{
						if (false)
						{
						}
						int num6;
						num6++;
						num5 = 1;
						continue;
					}
					}
					IL_DB:
					num4++;
					num5 = 10;
					continue;
					IL_1C1:
					num5 = 4;
					continue;
					IL_1E8:
					num5 = 9;
				}
			}
			IL_187:
			IL_2A2:
			A_1.ᜀ(sprΐ);
			return;
		}
		}
	}

	// Token: 0x0600312E RID: 12590 RVA: 0x002D3D8C File Offset: 0x002D2D8C
	private static void ᜀ(ParagraphFormat A_0, sprᨽ A_1)
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_53;
			case 1:
				goto IL_170;
			case 2:
				if (A_0.ForeColor != Color.Empty)
				{
					num = 7;
					continue;
				}
				goto IL_53;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_49;
				default:
					goto IL_156;
				}
				break;
			case 5:
				if (A_0.TextureStyle != TextureStyle.TextureNone)
				{
					num = 8;
					continue;
				}
				return;
			case 6:
			{
				spr\u24DB spr_u24DB = new spr\u24DB();
				spr_u24DB.ᜀ(A_0.BackColor);
				spr_u24DB.ᜁ(A_0.ForeColor);
				spr_u24DB.ᜀ(A_0.TextureStyle);
				A_1.ᜁ(spr_u24DB);
				A_1.ᜀ(spr_u24DB);
				num = 1;
				continue;
			}
			case 7:
			{
				spr\u24DB spr_u24DB2 = new spr\u24DB();
				spr_u24DB2.ᜀ(A_0.BackColor);
				spr_u24DB2.ᜁ(A_0.ForeColor);
				spr_u24DB2.ᜀ(A_0.TextureStyle);
				A_1.ᜁ(spr_u24DB2);
				A_1.ᜀ(spr_u24DB2);
				num = 0;
				continue;
			}
			case 8:
			{
				spr\u24DB spr_u24DB3 = new spr\u24DB();
				spr_u24DB3.ᜀ(A_0.BackColor);
				spr_u24DB3.ᜁ(A_0.ForeColor);
				spr_u24DB3.ᜀ(A_0.TextureStyle);
				A_1.ᜁ(spr_u24DB3);
				A_1.ᜀ(spr_u24DB3);
				num = 4;
				continue;
			}
			}
			goto IL_34;
			IL_49:
			num = 6;
			continue;
			IL_34:
			if (A_0.BackColor != Color.Empty)
			{
				goto IL_49;
			}
			goto IL_170;
			IL_53:
			num = 5;
			continue;
			IL_170:
			num = 2;
		}
		IL_156:
		if (false)
		{
		}
		if (true)
		{
		}
	}

	// Token: 0x0600312F RID: 12591 RVA: 0x002D3F3C File Offset: 0x002D2F3C
	private static void ᜀ(Borders A_0, sprᨽ A_1)
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
		spr\u224E a_ = new spr\u224E();
		spr\u192A.ᜀ(a_, A_0.Top);
		A_1.ᜃ(a_);
		A_1.ᜄ(a_);
		spr\u192A.ᜀ(a_, A_0.Left);
		A_1.ᜀ(a_);
		A_1.ᜉ(a_);
		spr\u192A.ᜀ(a_, A_0.Bottom);
		A_1.ᜂ(a_);
		A_1.ᜇ(a_);
		spr\u192A.ᜀ(a_, A_0.Right);
		A_1.ᜆ(a_);
		A_1.ᜅ(a_);
		spr\u192A.ᜀ(a_, A_0.Horizontal);
		A_1.ᜈ(a_);
		spr\u192A.ᜀ(a_, A_0.Vertical);
		A_1.ᜁ(a_);
	}
}
