using System;
using System.Collections.Generic;
using System.Drawing;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Formatting;

// Token: 0x0200036E RID: 878
internal class spr\u1B67
{
	// Token: 0x06003131 RID: 12593 RVA: 0x002D4020 File Offset: 0x002D3020
	public static void ᜀ(sprᶍ A_0, Section A_1, bool A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				Borders borders = null;
				PageSetup pageSetup = A_1.PageSetup;
				pageSetup.DefaultTabWidth = (float)A_0.\u1717() / 20f;
				List<spr\u1CC1> list = A_0.ᜥ().ᜂ();
				int count = list.Count;
				spr\u1B67.ᜀ(A_0.ᜥ(), pageSetup);
				int num = 0;
				int num2 = 9;
				for (;;)
				{
					spr\u2227 spr_u;
					int num6;
					int count2;
					switch (num2)
					{
					case 0:
						goto IL_97D;
					case 1:
						num2 = 70;
						continue;
					case 2:
						goto IL_97D;
					case 3:
					{
						int num3;
						if (num3 != 36913)
						{
							num2 = 30;
							continue;
						}
						pageSetup.LinePitch = (float)A_0.ᜆ() / 20f;
						num2 = 49;
						continue;
					}
					case 4:
						num2 = 48;
						continue;
					case 5:
					{
						int num3;
						if (num3 <= 36913)
						{
							num2 = 29;
							continue;
						}
						num2 = 8;
						continue;
					}
					case 6:
						goto IL_97D;
					case 7:
						num2 = 60;
						continue;
					case 8:
					{
						int num3;
						switch (num3)
						{
						case 45079:
							pageSetup.HeaderDistance = (float)A_0.\u170D() / 20f;
							num2 = 2;
							continue;
						case 45080:
							pageSetup.FooterDistance = (float)A_0.ᜦ() / 20f;
							num2 = 28;
							continue;
						default:
							num2 = 52;
							continue;
						}
						break;
					}
					case 9:
						goto IL_9AE;
					case 10:
						num2 = 80;
						continue;
					case 11:
						num2 = 34;
						continue;
					case 12:
					{
						Column column = A_1.AddColumn(0f, 0f);
						column.Width = (float)spr_u.ᜁ() / 20f;
						column.Space = (float)spr_u.ᜀ() / 20f;
						num2 = 65;
						continue;
					}
					case 13:
						return;
					case 14:
						goto IL_97D;
					case 15:
						goto IL_97D;
					case 16:
						goto IL_97D;
					case 17:
						goto IL_97D;
					case 18:
					{
						int num4;
						if (num4 != 1)
						{
							num2 = 83;
							continue;
						}
						A_1.TextDirection = TextDirection.RightToLeft;
						num2 = 59;
						continue;
					}
					case 19:
						goto IL_C6E;
					case 20:
					{
						int num3;
						switch (num3)
						{
						case 20530:
							pageSetup.PitchType = (GridPitchType)A_0.ᜣ();
							num2 = 21;
							continue;
						case 20531:
						{
							int num5 = A_0.ᜀ();
							int num4 = num5;
							num2 = 18;
							continue;
						}
						default:
							num2 = 4;
							continue;
						}
						break;
					}
					case 21:
						goto IL_97D;
					case 22:
						goto IL_97D;
					case 23:
						goto IL_97D;
					case 24:
						goto IL_97D;
					case 25:
						num2 = 88;
						continue;
					case 26:
						goto IL_97D;
					case 27:
						if (num6 >= count2)
						{
							num2 = 25;
							continue;
						}
						spr_u = A_0.ᜡ().ᜀ(num6);
						num2 = 43;
						continue;
					case 28:
						goto IL_97D;
					case 29:
						num2 = 57;
						continue;
					case 30:
						num2 = 17;
						continue;
					case 31:
						goto IL_97D;
					case 32:
						goto IL_97D;
					case 33:
						goto IL_97D;
					case 34:
						goto IL_97D;
					case 35:
						goto IL_97D;
					case 36:
						num2 = 3;
						continue;
					case 37:
						num2 = 75;
						continue;
					case 38:
						goto IL_97D;
					case 39:
					{
						int num3;
						if (num3 != 12307)
						{
							num2 = 7;
							continue;
						}
						num2 = 47;
						continue;
					}
					case 40:
					{
						if (num >= count)
						{
							num2 = 55;
							continue;
						}
						spr\u1CC1 spr_u1CC = list[num];
						int num7 = spr_u1CC.ᜈ();
						int num3 = num7;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_35F;
						default:
							if (false)
							{
							}
							num2 = 81;
							continue;
						}
						break;
					}
					case 41:
						num2 = 85;
						continue;
					case 42:
						num2 = 79;
						continue;
					case 43:
						if (spr_u.ᜁ() != 0)
						{
							num2 = 12;
							continue;
						}
						goto IL_9D3;
					case 44:
						goto IL_97D;
					case 45:
						if (A_0.ᜁ(36886))
						{
							num2 = 87;
							continue;
						}
						goto IL_97D;
					case 46:
						if (A_0.ᜄ())
						{
							num2 = 67;
							continue;
						}
						goto IL_7BB;
					case 47:
						if (A_0.ᜁ(20501))
						{
							num2 = 42;
							continue;
						}
						goto IL_97D;
					case 48:
					{
						int num3;
						if (num3 != 21039)
						{
							num2 = 82;
							continue;
						}
						pageSetup.PageBordersApplyType = A_0.ᜊ();
						pageSetup.IsFrontPageBorder = A_0.\u1719();
						pageSetup.PageBorderOffsetFrom = A_0.ᜨ();
						num2 = 73;
						continue;
					}
					case 49:
						goto IL_97D;
					case 50:
					{
						int num3;
						switch (num3)
						{
						case 45087:
							pageSetup.PageSize = new SizeF((float)A_0.ᜁ() / 20f, pageSetup.PageSize.Height);
							num2 = 14;
							continue;
						case 45088:
							pageSetup.PageSize = new SizeF(pageSetup.PageSize.Width, (float)A_0.ᜪ() / 20f);
							num2 = 86;
							continue;
						case 45089:
							pageSetup.Margins.Left = (float)A_0.\u1712() / 20f;
							num2 = 0;
							continue;
						case 45090:
							pageSetup.Margins.Right = (float)A_0.ᜏ() / 20f;
							num2 = 24;
							continue;
						case 45091:
						case 45092:
							goto IL_97D;
						case 45093:
							pageSetup.Margins.Gutter = (float)A_0.ᜧ() / 20f;
							num2 = 6;
							continue;
						default:
							num2 = 1;
							continue;
						}
						break;
					}
					case 51:
						goto IL_97D;
					case 52:
						num2 = 50;
						continue;
					case 53:
						goto IL_97D;
					case 54:
						goto IL_97D;
					case 55:
						goto IL_35F;
					case 56:
						goto IL_97D;
					case 57:
					{
						int num3;
						switch (num3)
						{
						case 28715:
						{
							borders = spr\u1B67.ᜀ(borders, pageSetup);
							spr\u1CC1 spr_u1CC;
							spr\u224E a_ = A_0.ᜀ(spr_u1CC);
							spr\u1B67.ᜁ(a_, borders.Top);
							num2 = 51;
							continue;
						}
						case 28716:
						{
							borders = spr\u1B67.ᜀ(borders, pageSetup);
							spr\u1CC1 spr_u1CC;
							spr\u224E a_2 = A_0.ᜀ(spr_u1CC);
							spr\u1B67.ᜁ(a_2, borders.Left);
							num2 = 16;
							continue;
						}
						case 28717:
						{
							borders = spr\u1B67.ᜀ(borders, pageSetup);
							spr\u1CC1 spr_u1CC;
							spr\u224E a_3 = A_0.ᜀ(spr_u1CC);
							spr\u1B67.ᜁ(a_3, borders.Bottom);
							num2 = 31;
							continue;
						}
						case 28718:
						{
							borders = spr\u1B67.ᜀ(borders, pageSetup);
							spr\u1CC1 spr_u1CC;
							spr\u224E a_4 = A_0.ᜀ(spr_u1CC);
							spr\u1B67.ᜁ(a_4, borders.Right);
							num2 = 38;
							continue;
						}
						default:
							num2 = 90;
							continue;
						}
						break;
					}
					case 58:
						goto IL_97D;
					case 59:
						goto IL_97D;
					case 60:
					{
						int num3;
						switch (num3)
						{
						case 12313:
							pageSetup.DrawLinesBetweenCols = A_0.\u171A();
							num2 = 61;
							continue;
						case 12314:
							pageSetup.VerticalAlignment = (PageAlignment)A_0.ᜤ();
							num2 = 53;
							continue;
						case 12315:
						case 12316:
							goto IL_97D;
						case 12317:
							pageSetup.Orientation = (PageOrientation)A_0.\u171E();
							num2 = 35;
							continue;
						default:
							num2 = 84;
							continue;
						}
						break;
					}
					case 61:
						goto IL_97D;
					case 62:
						num2 = 20;
						continue;
					case 63:
						goto IL_97D;
					case 64:
						goto IL_9AE;
					case 65:
						goto IL_9D3;
					case 66:
					{
						int num3;
						switch (num3)
						{
						case 36899:
							pageSetup.Margins.Top = (float)A_0.\u171B() / 20f;
							num2 = 23;
							continue;
						case 36900:
							pageSetup.Margins.Bottom = (float)A_0.ᜑ() / 20f;
							num2 = 54;
							continue;
						default:
							num2 = 36;
							continue;
						}
						break;
					}
					case 67:
						A_1.PageSetup.PageStartingNumber = (int)A_0.\u1716();
						num2 = 74;
						continue;
					case 68:
					{
						int num4;
						if (num4 != 4)
						{
							num2 = 41;
							continue;
						}
						A_1.TextDirection = TextDirection.TopToBottomRotated;
						num2 = 44;
						continue;
					}
					case 69:
						goto IL_97D;
					case 70:
					{
						int num3;
						switch (num3)
						{
						case 53812:
						{
							borders = spr\u1B67.ᜀ(borders, pageSetup);
							spr\u1CC1 spr_u1CC;
							spr\u224E a_5 = A_0.ᜀ(spr_u1CC);
							spr\u1B67.ᜁ(a_5, borders.Top);
							num2 = 26;
							continue;
						}
						case 53813:
						{
							borders = spr\u1B67.ᜀ(borders, pageSetup);
							spr\u1CC1 spr_u1CC;
							spr\u224E a_6 = A_0.ᜀ(spr_u1CC);
							spr\u1B67.ᜁ(a_6, borders.Left);
							num2 = 32;
							continue;
						}
						case 53814:
						{
							borders = spr\u1B67.ᜀ(borders, pageSetup);
							spr\u1CC1 spr_u1CC;
							spr\u224E a_7 = A_0.ᜀ(spr_u1CC);
							spr\u1B67.ᜁ(a_7, borders.Bottom);
							num2 = 56;
							continue;
						}
						case 53815:
						{
							borders = spr\u1B67.ᜀ(borders, pageSetup);
							spr\u1CC1 spr_u1CC;
							spr\u224E a_8 = A_0.ᜀ(spr_u1CC);
							spr\u1B67.ᜁ(a_8, borders.Right);
							num2 = 58;
							continue;
						}
						default:
							if (true)
							{
							}
							num2 = 11;
							continue;
						}
						break;
					}
					case 71:
					{
						sprḍ sprḍ = A_0.\u1718();
						A_1.DataArray = new byte[sprḍ.ᜇ()];
						sprḍ.ᜀ(A_1.DataArray, 0);
						num2 = 13;
						continue;
					}
					case 72:
						goto IL_97D;
					case 73:
						goto IL_97D;
					case 74:
						goto IL_7BB;
					case 75:
					{
						int num3;
						switch (num3)
						{
						case 12294:
							A_1.ProtectForm = !A_0.ᜂ();
							num2 = 15;
							continue;
						case 12295:
						case 12296:
							goto IL_97D;
						case 12297:
							A_1.BreakCode = (SectionBreakType)A_0.ᜩ();
							num2 = 63;
							continue;
						case 12298:
							pageSetup.DifferentFirstPageHeaderFooter = A_0.ᜈ();
							num2 = 91;
							continue;
						default:
							num2 = 78;
							continue;
						}
						break;
					}
					case 76:
					{
						int num3;
						if (num3 != 12840)
						{
							num2 = 62;
							continue;
						}
						pageSetup.Bidi = A_0.ᜢ();
						num2 = 22;
						continue;
					}
					case 77:
						num2 = 45;
						continue;
					case 78:
						num2 = 39;
						continue;
					case 79:
						if (A_0.ᜁ(20507))
						{
							num2 = 77;
							continue;
						}
						goto IL_97D;
					case 80:
					{
						int num3;
						if (num3 <= 12317)
						{
							num2 = 37;
							continue;
						}
						num2 = 76;
						continue;
					}
					case 81:
					{
						int num3;
						if (num3 <= 21039)
						{
							num2 = 10;
							continue;
						}
						num2 = 5;
						continue;
					}
					case 82:
						num2 = 69;
						continue;
					case 83:
						num2 = 68;
						continue;
					case 84:
						num2 = 92;
						continue;
					case 85:
						A_1.TextDirection = TextDirection.LeftToRight;
						num2 = 72;
						continue;
					case 86:
						goto IL_97D;
					case 87:
						pageSetup.LineNumberingRestartMode = A_0.\u1715();
						pageSetup.LineNumberingStep = (int)A_0.ᜐ();
						pageSetup.LineNumberingStartValue = (int)A_0.\u171C();
						pageSetup.LineNumberingDistanceFromText = (float)A_0.ᜌ() / 20f;
						num2 = 33;
						continue;
					case 88:
						if (A_2)
						{
							num2 = 71;
							continue;
						}
						return;
					case 89:
						goto IL_C6E;
					case 90:
						num2 = 66;
						continue;
					case 91:
						goto IL_97D;
					case 92:
						goto IL_97D;
					}
					break;
					IL_35F:
					A_1.PageSetup.EqualColumnWidth = A_0.ᜡ().ᜀ();
					A_1.PageSetup.PageNumberStyle = (PageNumberStyle)A_0.ᜋ();
					A_1.PageSetup.RestartPageNumbering = A_0.ᜄ();
					num2 = 46;
					continue;
					IL_7BB:
					spr_u = null;
					num6 = 0;
					count2 = A_0.ᜡ().Count;
					num2 = 19;
					continue;
					IL_97D:
					num++;
					num2 = 64;
					continue;
					IL_9AE:
					num2 = 40;
					continue;
					IL_9D3:
					num6++;
					num2 = 89;
					continue;
					IL_C6E:
					num2 = 27;
				}
			}
			return;
		}
	}

	// Token: 0x06003132 RID: 12594 RVA: 0x002D4D40 File Offset: 0x002D3D40
	private static void ᜀ(sprḍ A_0, PageSetup A_1)
	{
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				A_1.Orientation = PageOrientation.Portrait;
				num = 3;
				continue;
			case 1:
				num = 4;
				continue;
			case 2:
				IL_89:
				num = 6;
				continue;
			case 3:
				goto IL_8B;
			case 4:
				if (!A_0.ᜂ(12317))
				{
					num = 2;
					continue;
				}
				goto IL_8B;
			case 6:
				if (A_1.Orientation != PageOrientation.Portrait)
				{
					num = 0;
					continue;
				}
				goto IL_8B;
			}
			if (A_0 != null)
			{
				num = 1;
				continue;
			}
			IL_8B:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_89;
			default:
				goto IL_A1;
			}
		}
		IL_A1:
		if (false)
		{
		}
	}

	// Token: 0x06003133 RID: 12595 RVA: 0x002D4E00 File Offset: 0x002D3E00
	public static void ᜀ(sprᶍ A_0, Section A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				A_0.ᜁ((ushort)Math.Round((double)(A_1.PageSetup.DefaultTabWidth * 20f)));
				int num = 42;
				for (;;)
				{
					SizeF pageSize;
					spr\u1CC1 spr_u1CC;
					SizeF pageSize2;
					int num3;
					switch (num)
					{
					case 0:
						goto IL_19B;
					case 1:
						if (A_1.TextDirection == TextDirection.TopToBottomRotated)
						{
							num = 24;
							continue;
						}
						A_0.ᜀ(0);
						num = 6;
						continue;
					case 2:
						if (A_1.Columns.Count > 1)
						{
							num = 19;
							continue;
						}
						goto IL_B0A;
					case 3:
						goto IL_3F7;
					case 4:
						goto IL_858;
					case 5:
						goto IL_617;
					case 6:
						goto IL_617;
					case 7:
						if (A_1.PageSetup.LineNumberingStep != 0)
						{
							num = 61;
							continue;
						}
						goto IL_563;
					case 8:
					{
						Borders borders = A_1.PageSetup.Borders;
						spr\u224E a_ = new spr\u224E();
						spr\u1B67.ᜀ(a_, borders.Top);
						A_0.ᜂ(a_);
						A_0.ᜃ(a_);
						spr\u1B67.ᜀ(a_, borders.Left);
						A_0.ᜀ(a_);
						A_0.ᜅ(a_);
						spr\u1B67.ᜀ(a_, borders.Bottom);
						A_0.ᜇ(a_);
						A_0.ᜆ(a_);
						spr\u1B67.ᜀ(a_, borders.Right);
						A_0.ᜁ(a_);
						A_0.ᜄ(a_);
						num = 26;
						continue;
					}
					case 9:
						if (!A_1.PageSetup.Borders.IsDefault)
						{
							num = 8;
							continue;
						}
						goto IL_48C;
					case 10:
						goto IL_3F7;
					case 11:
						if (A_1.PageSetup.VerticalAlignment != PageAlignment.Top)
						{
							num = 34;
							continue;
						}
						goto IL_858;
					case 12:
						goto IL_A85;
					case 13:
						A_0.ᜃ(A_1.PageSetup.RestartPageNumbering);
						A_0.ᜆ((ushort)A_1.PageSetup.PageStartingNumber);
						num = 68;
						continue;
					case 14:
						goto IL_617;
					case 15:
						if (A_1.PageSetup.Bidi)
						{
							num = 58;
							continue;
						}
						goto IL_A85;
					case 16:
						if (A_1.DataArray.Length > 0)
						{
							num = 60;
							continue;
						}
						goto IL_1CD;
					case 17:
						if (pageSize.Height != 0f)
						{
							num = 38;
							continue;
						}
						goto IL_536;
					case 18:
						goto IL_652;
					case 19:
						num = 62;
						continue;
					case 20:
						goto IL_25B;
					case 21:
						goto IL_4FE;
					case 22:
						if (A_0.ᜁ(53799))
						{
							num = 65;
							continue;
						}
						goto IL_652;
					case 23:
						goto IL_563;
					case 24:
						A_0.ᜀ(4);
						num = 5;
						continue;
					case 25:
						goto IL_1CD;
					case 26:
						goto IL_48C;
					case 27:
						goto IL_938;
					case 28:
						if (A_1.PageSetup.LineNumberingRestartMode != LineNumberingRestartMode.None)
						{
							num = 32;
							continue;
						}
						goto IL_25B;
					case 29:
						goto IL_6EC;
					case 30:
						if (A_1.PageSetup.RestartPageNumbering)
						{
							num = 13;
							continue;
						}
						A_0.ᜥ().ᜆ(12305);
						A_0.ᜥ().ᜆ(20508);
						num = 52;
						continue;
					case 31:
						if (A_0.ᜁ(spr_u1CC.ᜈ()))
						{
							num = 67;
							continue;
						}
						goto IL_41B;
					case 32:
						A_0.ᜀ(A_1.PageSetup.LineNumberingRestartMode);
						num = 7;
						continue;
					case 33:
						num = 66;
						continue;
					case 34:
						A_0.ᜀ((byte)A_1.PageSetup.VerticalAlignment);
						num = 4;
						continue;
					case 35:
						A_0.ᜅ((ushort)Math.Round((double)(A_1.PageSetup.PageSize.Width * 20f)));
						num = 21;
						continue;
					case 36:
						if (!A_0.ᜁ(21039))
						{
							num = 63;
							continue;
						}
						goto IL_41B;
					case 37:
					{
						int num2;
						int count;
						if (num2 >= count)
						{
							num = 39;
							continue;
						}
						Column column = A_1.Columns[num2];
						spr\u2227 spr_u = A_0.ᜡ().ᜁ();
						spr_u.ᜁ((ushort)Math.Round((double)(column.Width * 20f)));
						spr_u.ᜀ((ushort)Math.Round((double)(column.Space * 20f)));
						num2++;
						num = 69;
						continue;
					}
					case 38:
						A_0.ᜇ((ushort)Math.Round((double)(A_1.PageSetup.PageSize.Height * 20f)));
						goto IL_3B7;
					case 39:
						num = 2;
						continue;
					case 40:
						A_0.ᜡ().ᜀ(false);
						num = 27;
						continue;
					case 41:
						A_0.ᜃ((byte)A_1.PageSetup.Orientation);
						num = 29;
						continue;
					case 42:
						if (A_1.DataArray != null)
						{
							num = 59;
							continue;
						}
						goto IL_1CD;
					case 43:
						if (A_1.PageSetup.Orientation != PageOrientation.Portrait)
						{
							num = 41;
							continue;
						}
						goto IL_6EC;
					case 44:
						num = 16;
						continue;
					case 45:
						A_0.ᜆ(A_1.PageSetup.DifferentFirstPageHeaderFooter);
						num = 0;
						continue;
					case 46:
						goto IL_536;
					case 47:
						if (A_1.PageSetup.DifferentFirstPageHeaderFooter)
						{
							num = 45;
							continue;
						}
						goto IL_19B;
					case 48:
						if (A_1.Columns.Count > 0)
						{
							num = 49;
							continue;
						}
						A_0.ᜥ().ᜀ(36876, 720);
						num = 64;
						continue;
					case 49:
					{
						int num2 = 0;
						int count = A_1.Columns.Count;
						num = 53;
						continue;
					}
					case 50:
						if (A_1.DataArray.Length < 300)
						{
							num = 44;
							continue;
						}
						goto IL_1CD;
					case 51:
						if (pageSize2.Width != 0f)
						{
							num = 35;
							continue;
						}
						goto IL_4FE;
					case 52:
						goto IL_1F5;
					case 53:
						goto IL_466;
					case 54:
						A_0.ᜃ((short)Math.Round((double)(A_1.PageSetup.LineNumberingDistanceFromText * 20f)));
						num = 20;
						continue;
					case 55:
					{
						int num4;
						if (num3 >= num4)
						{
							num = 25;
							continue;
						}
						sprḍ sprḍ;
						spr_u1CC = sprḍ.ᜁ(num3);
						num = 31;
						continue;
					}
					case 56:
						A_0.ᜀ(1);
						num = 14;
						continue;
					case 57:
						if (A_1.PageSetup.LineNumberingDistanceFromText != 0f)
						{
							num = 54;
							continue;
						}
						goto IL_25B;
					case 58:
						A_0.ᜂ(A_1.PageSetup.Bidi);
						num = 12;
						continue;
					case 59:
						num = 50;
						continue;
					case 60:
					{
						if (true)
						{
						}
						sprḍ sprḍ = new sprḍ(A_1.DataArray, 0);
						spr_u1CC = null;
						num3 = 0;
						int num4 = sprḍ.ᜈ();
						num = 3;
						continue;
					}
					case 61:
						A_0.ᜄ((ushort)A_1.PageSetup.LineNumberingStep);
						num = 23;
						continue;
					case 62:
						if (!A_1.PageSetup.EqualColumnWidth)
						{
							num = 40;
							continue;
						}
						goto IL_B0A;
					case 63:
						num = 22;
						continue;
					case 64:
						goto IL_38B;
					case 65:
						goto IL_41B;
					case 66:
						if (A_1.TextDirection == TextDirection.RightToLeft)
						{
							num = 56;
							continue;
						}
						num = 1;
						continue;
					case 67:
						num = 36;
						continue;
					case 68:
						goto IL_1F5;
					case 69:
						goto IL_466;
					case 70:
						if (A_1.TextDirection != TextDirection.LeftToRight)
						{
							num = 33;
							continue;
						}
						goto IL_617;
					}
					break;
					IL_19B:
					num = 28;
					continue;
					IL_1CD:
					num = 70;
					continue;
					IL_1F5:
					A_0.ᜡ().Clear();
					num = 48;
					continue;
					IL_25B:
					num = 15;
					continue;
					IL_3B7:
					num = 46;
					continue;
					IL_652:
					num3++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3B7;
					default:
						if (false)
						{
						}
						num = 10;
						continue;
					}
					IL_3F7:
					num = 55;
					continue;
					IL_41B:
					A_0.ᜥ().ᜂ().Add(spr_u1CC);
					num = 18;
					continue;
					IL_466:
					num = 37;
					continue;
					IL_48C:
					A_0.ᜀ(A_1.PageSetup.PageBordersApplyType);
					A_0.ᜄ(A_1.PageSetup.IsFrontPageBorder);
					A_0.ᜀ(A_1.PageSetup.PageBorderOffsetFrom);
					A_0.ᜁ((byte)A_1.PageSetup.PageNumberStyle);
					num = 30;
					continue;
					IL_4FE:
					pageSize = A_1.PageSetup.PageSize;
					num = 17;
					continue;
					IL_536:
					num = 11;
					continue;
					IL_563:
					A_0.ᜇ((short)A_1.PageSetup.LineNumberingStartValue);
					num = 57;
					continue;
					IL_617:
					pageSize2 = A_1.PageSetup.PageSize;
					num = 51;
					continue;
					IL_6EC:
					A_0.ᜂ((short)Math.Round((double)(A_1.PageSetup.Margins.Left * 20f)));
					A_0.ᜅ((short)Math.Round((double)(A_1.PageSetup.Margins.Right * 20f)));
					A_0.ᜆ((short)Math.Round((double)(A_1.PageSetup.Margins.Top * 20f)));
					A_0.ᜈ((short)Math.Round((double)(A_1.PageSetup.Margins.Bottom * 20f)));
					A_0.ᜁ((short)Math.Round((double)(A_1.PageSetup.Margins.Gutter * 20f)));
					A_0.ᜄ((short)Math.Round((double)(A_1.PageSetup.HeaderDistance * 20f)));
					A_0.ᜀ((short)Math.Round((double)(A_1.PageSetup.FooterDistance * 20f)));
					num = 47;
					continue;
					IL_858:
					num = 43;
					continue;
					IL_A85:
					num = 9;
				}
			}
			IL_38B:
			IL_938:
			IL_B0A:
			A_0.ᜅ(A_1.PageSetup.DrawLinesBetweenCols);
			A_0.ᜂ((byte)A_1.BreakCode);
			A_0.ᜁ(A_1.ProtectForm);
			return;
		}
	}

	// Token: 0x06003134 RID: 12596 RVA: 0x002D5944 File Offset: 0x002D4944
	private static void ᜁ(spr\u224E A_0, Border A_1)
	{
		for (;;)
		{
			IL_00:
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					return;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						A_1.Color = A_0.ᜅ();
						A_1.BorderType = (BorderStyle)A_0.ᜄ();
						A_1.LineWidth = (float)A_0.ᜊ() / 8f;
						A_1.Space = (float)A_0.ᜆ() / 20f;
						A_1.Shadow = A_0.ᜋ();
						if (true)
						{
						}
						num = 1;
						continue;
					}
					break;
				}
				if (A_0.ᜀ())
				{
					return;
				}
				num = 2;
			}
		}
	}

	// Token: 0x06003135 RID: 12597 RVA: 0x002D5A08 File Offset: 0x002D4A08
	private static void ᜀ(spr\u224E A_0, Border A_1)
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
			if (A_1.IsDefault)
			{
				A_0.ᜁ(0);
				A_0.ᜀ(Color.Empty);
				A_0.ᜃ(0);
				A_0.ᜀ(0);
				A_0.ᜂ(0);
				A_0.ᜀ(false);
				return;
			}
			if (true)
			{
			}
			break;
		}
		A_0.ᜁ((byte)sprṡ.ᜁ(A_1.Color));
		A_0.ᜀ(A_1.Color);
		A_0.ᜃ((byte)A_1.BorderType);
		A_0.ᜀ((byte)Math.Round((double)(A_1.LineWidth * 8f)));
		A_0.ᜂ((byte)Math.Round((double)(A_1.Space * 20f)));
		A_0.ᜀ(A_1.Shadow);
	}

	// Token: 0x06003136 RID: 12598 RVA: 0x002D5AE8 File Offset: 0x002D4AE8
	private static Borders ᜀ(Borders A_0, PageSetup A_1)
	{
		for (;;)
		{
			IL_00:
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						A_0 = A_1.Borders;
						num = 1;
						continue;
					}
					break;
				case 1:
					return A_0;
				}
				if (true)
				{
				}
				if (A_0 != null)
				{
					return A_0;
				}
				num = 0;
			}
		}
		return A_0;
	}
}
