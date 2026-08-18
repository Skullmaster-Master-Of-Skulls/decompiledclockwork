using System;
using System.Drawing;
using Spire.CompoundFile.Doc;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;

// Token: 0x0200036F RID: 879
internal class spr\u1B3A
{
	// Token: 0x06003138 RID: 12600 RVA: 0x002D5B74 File Offset: 0x002D4B74
	public static void ᜁ(sprᨽ A_0, RowFormat A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				A_1.Sprms = A_0.ᜪ();
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (A_1.Sprms.ᜂ(13316))
						{
							num = 26;
							continue;
						}
						goto IL_12D;
					case 1:
						if (A_1.GridBeforeWidth.ᜁ() > 0)
						{
							num = 9;
							continue;
						}
						goto IL_438;
					case 2:
						goto IL_1AB;
					case 3:
						if (A_1.Sprms != null)
						{
							num = 35;
							continue;
						}
						goto IL_12D;
					case 4:
						A_1.OwnerRow.IsHeader = true;
						num = 24;
						continue;
					case 5:
						goto IL_49C;
					case 6:
						if (A_1.Sprms.ᜂ(62999))
						{
							num = 20;
							continue;
						}
						goto IL_438;
					case 7:
						if (A_1.OwnerRow != null)
						{
							num = 4;
							continue;
						}
						goto IL_12D;
					case 8:
						goto IL_49C;
					case 9:
						A_1.GridBefore = 1;
						num = 14;
						continue;
					case 10:
					{
						int num2;
						switch (num2)
						{
						case 1:
						{
							Table table;
							table.TableFormat.IsAutoResized = true;
							table.PreferredTableWidth.ᜀ(FtsWidth.Auto);
							table.PreferredTableWidth.ᜀ(0);
							num = 5;
							continue;
						}
						case 2:
						{
							Table table;
							table.TableFormat.IsAutoResized = false;
							table.PreferredTableWidth.ᜀ(FtsWidth.Percentage);
							int num3;
							table.PreferredTableWidth.ᜀ(num3 / 50);
							num = 8;
							continue;
						}
						case 3:
						{
							Table table;
							table.TableFormat.IsAutoResized = false;
							table.PreferredTableWidth.ᜀ(FtsWidth.Point);
							int num3;
							table.PreferredTableWidth.ᜀ(num3);
							num = 12;
							continue;
						}
						default:
							num = 34;
							continue;
						}
						break;
					}
					case 11:
						goto IL_1D9;
					case 12:
						goto IL_49C;
					case 13:
					{
						A_1.GridAfterWidth.ᜀ(FtsWidth.Percentage);
						int a_;
						A_1.GridAfterWidth.ᜀ(a_);
						num = 11;
						continue;
					}
					case 14:
						goto IL_438;
					case 15:
					{
						Table table;
						table.TableFormat.IsAutoResized = false;
						table.PreferredTableWidth.ᜀ(FtsWidth.None);
						table.PreferredTableWidth.ᜀ(0);
						num = 36;
						continue;
					}
					case 16:
						goto IL_4F3;
					case 17:
					{
						A_1.GridBeforeWidth.ᜀ(FtsWidth.Percentage);
						int a_2;
						A_1.GridBeforeWidth.ᜀ(a_2);
						num = 2;
						continue;
					}
					case 18:
						if (A_1.GridAfterWidth.ᜁ() <= 0)
						{
							return;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4F3;
						default:
							if (false)
							{
							}
							num = 23;
							continue;
						}
						break;
					case 19:
						return;
					case 20:
					{
						spr\u1CC1 spr_u1CC = A_1.Sprms.ᜇ(62999);
						byte[] array = spr_u1CC.ᜎ();
						int a_2 = (int)array[1] + ((int)array[2] << 8);
						num = 25;
						continue;
					}
					case 21:
					{
						spr\u1CC1 spr_u1CC2;
						if (Convert.ToInt32(spr_u1CC2.ᜎ()[0]) == 3)
						{
							num = 31;
							continue;
						}
						goto IL_1D9;
					}
					case 22:
					{
						spr\u1CC1 spr_u1CC3 = A_1.Sprms.ᜇ(62996);
						byte[] array2 = spr_u1CC3.ᜎ();
						int num3 = (int)array2[1] + ((int)array2[2] << 8);
						Table table = A_1.OwnerRow.Owner as Table;
						int num2 = Convert.ToInt32(spr_u1CC3.ᜎ()[0]);
						num = 10;
						continue;
					}
					case 23:
						A_1.GridAfter = 1;
						num = 19;
						continue;
					case 24:
						goto IL_12D;
					case 25:
					{
						spr\u1CC1 spr_u1CC;
						if (Convert.ToInt32(spr_u1CC.ᜎ()[0]) == 2)
						{
							num = 17;
							continue;
						}
						num = 33;
						continue;
					}
					case 26:
						num = 38;
						continue;
					case 27:
					{
						spr\u1CC1 spr_u1CC2;
						if (Convert.ToInt32(spr_u1CC2.ᜎ()[0]) == 2)
						{
							num = 13;
							continue;
						}
						num = 21;
						continue;
					}
					case 28:
					{
						spr\u1CC1 spr_u1CC2 = A_1.Sprms.ᜇ(63000);
						byte[] array3 = spr_u1CC2.ᜎ();
						int a_ = (int)array3[1] + ((int)array3[2] << 8);
						num = 27;
						continue;
					}
					case 29:
						if (A_1.Sprms.ᜂ(63000))
						{
							num = 28;
							continue;
						}
						return;
					case 30:
					{
						A_1.GridBeforeWidth.ᜀ(FtsWidth.Point);
						int a_2;
						A_1.GridBeforeWidth.ᜀ(a_2);
						num = 16;
						continue;
					}
					case 31:
					{
						A_1.GridAfterWidth.ᜀ(FtsWidth.Point);
						int a_;
						A_1.GridAfterWidth.ᜀ(a_);
						num = 37;
						continue;
					}
					case 32:
						num = 7;
						continue;
					case 33:
					{
						spr\u1CC1 spr_u1CC;
						if (Convert.ToInt32(spr_u1CC.ᜎ()[0]) == 3)
						{
							num = 30;
							continue;
						}
						goto IL_1AB;
					}
					case 34:
						num = 15;
						continue;
					case 35:
						num = 0;
						continue;
					case 36:
						goto IL_49C;
					case 37:
						goto IL_1D9;
					case 38:
						if (A_1.Sprms.ᜇ(13316).ᜉ())
						{
							num = 32;
							continue;
						}
						goto IL_12D;
					case 39:
						if (A_1.Sprms.ᜂ(62996))
						{
							num = 22;
							continue;
						}
						goto IL_49C;
					}
					break;
					IL_12D:
					num = 39;
					continue;
					IL_1AB:
					num = 1;
					continue;
					IL_4F3:
					goto IL_1AB;
					IL_1D9:
					if (true)
					{
					}
					num = 18;
					continue;
					IL_438:
					num = 29;
					continue;
					IL_49C:
					num = 6;
				}
			}
			return;
		}
	}

	// Token: 0x06003139 RID: 12601 RVA: 0x002D617C File Offset: 0x002D517C
	public static void ᜀ(sprᨽ A_0, RowFormat A_1)
	{
		if (A_1.Sprms == null)
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
				return;
			}
		}
		A_1.Sprms.ᜆ(25707);
		A_1.Sprms.ᜆ(9238);
		A_1.Sprms.ᜆ(26185);
		A_1.Sprms.ᜆ(9239);
		A_0.ᜪ().ᜂ().AddRange(A_1.Sprms.ᜂ());
	}

	// Token: 0x0600313A RID: 12602 RVA: 0x002D6220 File Offset: 0x002D5220
	internal static void ᜀ(sprᯚ A_0, Borders A_1)
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
		spr\u1B3A.ᜀ(A_0.ᜃ(), A_1.Left);
		spr\u1B3A.ᜀ(A_0.ᜁ(), A_1.Right);
		spr\u1B3A.ᜀ(A_0.ᜆ(), A_1.Top);
		spr\u1B3A.ᜀ(A_0.ᜂ(), A_1.Bottom);
		spr\u1B3A.ᜀ(A_0.ᜅ(), A_1.Horizontal);
		spr\u1B3A.ᜀ(A_0.ᜄ(), A_1.Vertical);
	}

	// Token: 0x0600313B RID: 12603 RVA: 0x002D62C4 File Offset: 0x002D52C4
	internal static void ᜀ(int A_0, RowFormat A_1, sprḍ A_2)
	{
		for (;;)
		{
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch (A_0)
					{
					case 103:
						goto IL_5C;
					case 104:
						goto IL_9C;
					case 105:
						goto IL_D7;
					case 106:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6E;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num = 8;
							continue;
						}
						break;
					default:
						num = 1;
						continue;
					}
					break;
				case 1:
					return;
				case 2:
					goto IL_6E;
				case 3:
					num = 4;
					continue;
				case 4:
					switch (A_0)
					{
					case 52:
						goto IL_15D;
					case 53:
						goto IL_108;
					default:
						num = 2;
						continue;
					}
					break;
				case 5:
					goto IL_158;
				case 6:
					goto IL_FC;
				case 7:
					if (A_0 != 2)
					{
						num = 3;
						continue;
					}
					spr\u1B3A.ᜀ(A_1, A_2);
					num = 6;
					continue;
				case 8:
					if (!A_1.IsBreakAcrossPages)
					{
						num = 5;
						continue;
					}
					return;
				}
				break;
				IL_6E:
				num = 0;
			}
		}
		IL_5C:
		A_2.ᜁ(13845, A_1.IsAutoResized);
		return;
		IL_9C:
		A_2.ᜁ(22027, A_1.Bidi);
		return;
		IL_D7:
		A_2.ᜀ(21504, (short)A_1.HorizontalAlignment);
		return;
		IL_FC:
		return;
		IL_108:
		spr\u1B3A.ᜀ(A_1.LeftIndent, A_2);
		return;
		IL_158:
		A_2.ᜁ(13315, !A_1.IsBreakAcrossPages);
		return;
		IL_15D:
		spr\u1B3A.ᜁ(A_1.CellSpacing, A_2);
	}

	// Token: 0x0600313C RID: 12604 RVA: 0x002D643C File Offset: 0x002D543C
	internal static void ᜀ(int A_0, RowFormat A_1, spr\u1739 A_2)
	{
		switch (0)
		{
		default:
		{
			float cellSpacing;
			for (;;)
			{
				int num = 6;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						goto IL_282;
					case 1:
						return;
					case 2:
						if (cellSpacing >= 0f)
						{
							num = 9;
							continue;
						}
						return;
					case 3:
						num = 17;
						continue;
					case 4:
						num = 5;
						continue;
					case 5:
						switch (A_0)
						{
						case 103:
							goto IL_255;
						case 104:
							goto IL_1A3;
						case 105:
						case 106:
						case 107:
							return;
						case 108:
							num = 12;
							continue;
						default:
							num = 1;
							continue;
						}
						break;
					case 6:
						if (A_0 != 2)
						{
							num = 3;
							continue;
						}
						if (true)
						{
						}
						A_2.ᜄ((short)Math.Round((double)(A_1.Height * 20f)));
						num = 13;
						continue;
					case 7:
						goto IL_113;
					case 8:
						goto IL_1C5;
					case 9:
						goto IL_10E;
					case 10:
						goto IL_113;
					case 11:
					{
						Color color;
						if (color.IsEmpty)
						{
							num = 14;
							continue;
						}
						goto IL_1C5;
					}
					case 12:
						if (A_2.ᜐ() != null)
						{
							num = 0;
							continue;
						}
						num2 = 0;
						num = 10;
						continue;
					case 13:
						goto IL_207;
					case 14:
						A_2.ᜇ()[num2].ᜀ(A_1.BackColor);
						num = 8;
						continue;
					case 15:
						return;
					case 16:
						if (num2 >= A_2.ᜊ())
						{
							num = 15;
							continue;
						}
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
							Color color = A_2.ᜇ()[num2].ᜂ();
							num = 11;
							continue;
						}
						}
						break;
					case 17:
						switch (A_0)
						{
						case 52:
							cellSpacing = A_1.CellSpacing;
							num = 2;
							continue;
						case 53:
							goto IL_20C;
						default:
							num = 4;
							continue;
						}
						break;
					}
					break;
					IL_113:
					num = 16;
					continue;
					IL_1C5:
					num2++;
					num = 7;
				}
			}
			IL_10E:
			A_2.ᜄ((int)Math.Round((double)(cellSpacing * 20f)));
			return;
			IL_1A3:
			A_2.ᜁ(A_1.Bidi);
			return;
			IL_207:
			return;
			IL_20C:
			A_2.ᜊ((short)Math.Round((double)(A_1.LeftIndent * 20f)));
			return;
			IL_255:
			A_2.ᜄ(A_1.IsAutoResized);
			return;
			IL_282:
			A_2.ᜐ().ᜀ(A_1.BackColor);
			return;
		}
		}
	}

	// Token: 0x0600313D RID: 12605 RVA: 0x002D66E4 File Offset: 0x002D56E4
	internal static void ᜀ(int A_0, CellFormat A_1, spr\u1739 A_2, int A_3)
	{
		switch (0)
		{
		default:
		{
			TableCell tableCell;
			for (;;)
			{
				tableCell = null;
				int num = 17;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_26B;
					case 1:
						if (A_1.VerticalMerge == CellMerge.Continue)
						{
							num = 10;
							continue;
						}
						return;
					case 2:
						goto IL_2EF;
					case 3:
						if (A_1.HorizontalMerge == CellMerge.Start)
						{
							num = 8;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_253;
						default:
							if (false)
							{
							}
							num = 18;
							continue;
						}
						break;
					case 4:
						goto IL_154;
					case 5:
						goto IL_218;
					case 6:
						goto IL_1F4;
					case 7:
					{
						int num2;
						if (num2 >= A_2.ᜊ())
						{
							num = 5;
							continue;
						}
						TableRow ownerRow;
						A_2.ᜀ(num2, (short)Math.Round((double)(ownerRow.Cells[num2].Width * 20f)));
						num2++;
						num = 9;
						continue;
					}
					case 8:
						goto IL_294;
					case 9:
						goto IL_1F4;
					case 10:
						goto IL_1CF;
					case 11:
						if (tableCell != null)
						{
							num = 15;
							continue;
						}
						return;
					case 12:
						return;
					case 13:
						if (tableCell != null)
						{
							num = 2;
							continue;
						}
						return;
					case 14:
						goto IL_253;
					case 15:
						goto IL_2C0;
					case 16:
						goto IL_34C;
					case 17:
						switch (A_0)
						{
						case 1:
							goto IL_D5;
						case 2:
							goto IL_362;
						case 3:
							goto IL_34E;
						case 4:
							goto IL_116;
						case 5:
							tableCell = (A_1.OwnerBase as TableCell);
							num = 13;
							continue;
						case 6:
							num = 14;
							continue;
						case 7:
							tableCell = (A_1.OwnerBase as TableCell);
							num = 19;
							continue;
						case 8:
							num = 3;
							continue;
						case 9:
							return;
						case 10:
							goto IL_376;
						case 11:
							goto IL_190;
						case 12:
						{
							tableCell = (A_1.OwnerBase as TableCell);
							TableRow ownerRow = tableCell.OwnerRow;
							int num2 = 0;
							num = 6;
							continue;
						}
						case 13:
							tableCell = (A_1.OwnerBase as TableCell);
							num = 11;
							continue;
						default:
							num = 12;
							continue;
						}
						break;
					case 18:
						if (A_1.HorizontalMerge == CellMerge.Continue)
						{
							num = 16;
							continue;
						}
						return;
					case 19:
						if (tableCell != null)
						{
							num = 4;
							continue;
						}
						return;
					}
					break;
					IL_253:
					if (A_1.VerticalMerge == CellMerge.Start)
					{
						num = 0;
						continue;
					}
					num = 1;
					continue;
					IL_1F4:
					num = 7;
				}
			}
			return;
			IL_D5:
			spr\u1B3A.ᜁ(A_2.ᜂ(A_3), A_1.Borders);
			return;
			IL_116:
			A_2.ᜇ()[A_3].ᜀ(A_1.BackColor);
			return;
			IL_154:
			A_2.ᜇ()[A_3].ᜀ(tableCell.TextureStyle);
			return;
			IL_190:
			if (true)
			{
			}
			A_2.ᜂ(A_3).ᜁ(A_1.TextDirection);
			return;
			IL_1CF:
			A_2.ᜂ(A_3).ᜇ(true);
			return;
			IL_218:
			return;
			IL_26B:
			A_2.ᜂ(A_3).ᜅ(true);
			A_2.ᜂ(A_3).ᜇ(true);
			return;
			IL_294:
			A_2.ᜂ(A_3).ᜃ(true);
			A_2.ᜂ(A_3).ᜆ(false);
			return;
			IL_2C0:
			A_2.ᜂ(A_3).ᜁ(tableCell.WidthUnit);
			return;
			IL_2EF:
			A_2.ᜇ()[A_3].ᜁ(tableCell.ForeColor);
			return;
			IL_34C:
			A_2.ᜂ(A_3).ᜆ(true);
			return;
			IL_34E:
			spr\u1B3A.ᜁ(A_2.ᜁ()[A_3], A_1.Paddings);
			return;
			IL_362:
			A_2.ᜂ(A_3).ᜀ((byte)A_1.VerticalAlignment);
			return;
			IL_376:
			A_2.ᜂ(A_3).ᜀ(A_1.FitText);
			return;
		}
		}
	}

	// Token: 0x0600313E RID: 12606 RVA: 0x002D6A90 File Offset: 0x002D5A90
	public static void ᜀ(spr\u1739 A_0, TableRow A_1, ISection A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_C0:
				short num = 0;
				bool flag = false;
				int num2 = 0;
				int num3 = 18;
				for (;;)
				{
					float num4;
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
						short a_;
						switch (num3)
						{
						case 0:
							goto IL_1F3;
						case 1:
							num += 1;
							A_0.ᜀ(num2, (short)Math.Round((double)A_1.Cells[num2].Width));
							num3 = 24;
							continue;
						case 2:
							goto IL_26C;
						case 3:
							num3 = 5;
							continue;
						case 4:
							goto IL_1C7;
						case 5:
							if (A_1.OwnerTable.TableGrid.Count > (int)(num + 1))
							{
								num3 = 20;
								continue;
							}
							goto IL_1C7;
						case 6:
							if (A_1.OwnerTable.TableGrid.Count > num2 + 1)
							{
								num3 = 16;
								continue;
							}
							goto IL_1C7;
						case 7:
							goto IL_28D;
						case 8:
							num3 = 9;
							continue;
						case 9:
							if (A_1.Cells[num2].GridSpan > 1)
							{
								num3 = 15;
								continue;
							}
							goto IL_D7;
						case 10:
							goto IL_1F3;
						case 11:
						{
							if (num2 >= A_0.ᜊ())
							{
								num3 = 21;
								continue;
							}
							float width = A_1.Cells[num2].Width;
							num3 = 28;
							continue;
						}
						case 12:
							goto IL_1C7;
						case 13:
							if (flag)
							{
								num3 = 3;
								continue;
							}
							num3 = 6;
							continue;
						case 14:
							a_ = (short)Math.Round((double)(A_1.OwnerTable.TableGrid[(int)(A_1.Cells[num2].GridSpan + num)] - A_1.OwnerTable.TableGrid[(int)num]));
							flag = true;
							num3 = 4;
							continue;
						case 15:
							goto IL_23A;
						case 16:
							a_ = (short)Math.Round((double)(A_1.OwnerTable.TableGrid[num2 + 1] - A_1.OwnerTable.TableGrid[num2]));
							num3 = 27;
							continue;
						case 17:
						{
							float width;
							num4 = width;
							num3 = 26;
							continue;
						}
						case 18:
							goto IL_28D;
						case 19:
						{
							if (A_1.Cells[num2].WidthType == FtsWidth.Percentage)
							{
								num3 = 17;
								continue;
							}
							float width;
							num4 = width * 20f;
							num3 = 2;
							continue;
						}
						case 20:
							a_ = (short)Math.Round((double)(A_1.OwnerTable.TableGrid[(int)(num + 1)] - A_1.OwnerTable.TableGrid[(int)num]));
							num3 = 12;
							continue;
						case 21:
							return;
						case 22:
							a_ = 0;
							num3 = 25;
							continue;
						case 23:
							if (A_1.OwnerTable.TableGrid.Count > (int)(A_1.Cells[num2].GridSpan + num))
							{
								num3 = 14;
								continue;
							}
							goto IL_1C7;
						case 24:
							goto IL_1F3;
						case 25:
							if (A_1.Cells[num2].GridSpan != 1)
							{
								num3 = 30;
								continue;
							}
							num3 = 13;
							continue;
						case 26:
							goto IL_3D9;
						case 27:
							goto IL_1C7;
						case 28:
						{
							float width;
							if (width > 1638f)
							{
								num3 = 1;
								continue;
							}
							if (true)
							{
							}
							num3 = 31;
							continue;
						}
						case 29:
							if (A_1.OwnerTable.TableGrid.Count != 0)
							{
								num3 = 22;
								continue;
							}
							goto IL_D7;
						case 30:
							num3 = 23;
							continue;
						case 31:
						{
							float width;
							if (width != 0f)
							{
								num3 = 8;
								continue;
							}
							goto IL_23A;
						}
						}
						goto IL_C0;
						IL_D7:
						num += 1;
						num3 = 19;
						continue;
						IL_1C7:
						num += A_1.Cells[num2].GridSpan;
						A_0.ᜀ(num2, a_);
						num3 = 0;
						continue;
						IL_1F3:
						num2++;
						num3 = 7;
						continue;
						IL_23A:
						num3 = 29;
						continue;
						IL_28D:
						num3 = 11;
						continue;
					}
					}
					IL_26C:
					A_0.ᜀ(num2, (short)Math.Round((double)num4));
					num3 = 10;
					continue;
					IL_3D9:
					goto IL_26C;
				}
			}
			return;
		}
	}

	// Token: 0x0600313F RID: 12607 RVA: 0x002D6F34 File Offset: 0x002D5F34
	private static void ᜀ(Border A_0, spr\u224E A_1)
	{
		for (;;)
		{
			IL_00:
			int num = 13;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!A_0.HasNoneStyle)
					{
						num = 9;
						continue;
					}
					goto IL_10D;
				case 1:
					if (A_0.BorderType != BorderStyle.None)
					{
						num = 2;
						continue;
					}
					return;
				case 2:
					A_1.ᜃ((byte)A_0.BorderType);
					A_1.ᜁ((byte)sprṡ.ᜁ(A_0.Color));
					A_1.ᜀ(A_0.Color);
					A_1.ᜀ((byte)(A_0.LineWidth * 8f));
					A_1.ᜀ(A_0.Shadow);
					num = 8;
					continue;
				case 3:
					A_0.BorderType = BorderStyle.Single;
					num = 6;
					continue;
				case 4:
					if (A_0.BorderPosition != Border.BorderPositions.Horizontal)
					{
						num = 3;
						continue;
					}
					goto IL_B7;
				case 5:
				{
					Color color = A_0.Color;
					float lineWidth = A_0.LineWidth;
					A_0.BorderType = BorderStyle.Single;
					A_0.Color = color;
					A_0.LineWidth = lineWidth;
					num = 15;
					continue;
				}
				case 6:
					goto IL_B7;
				case 7:
					num = 4;
					continue;
				case 8:
					return;
				case 9:
					num = 16;
					continue;
				case 10:
					goto IL_B7;
				case 11:
					if (A_0.BorderType == BorderStyle.None)
					{
						num = 14;
						continue;
					}
					goto IL_10D;
				case 12:
					if (A_0.BorderType == BorderStyle.Hairline)
					{
						num = 17;
						continue;
					}
					goto IL_B7;
				case 14:
					num = 0;
					continue;
				case 15:
					goto IL_B7;
				case 16:
					if (A_0.BorderPosition != Border.BorderPositions.Vertical)
					{
						num = 7;
						continue;
					}
					goto IL_B7;
				case 17:
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
						A_0.BorderType = BorderStyle.Single;
						num = 10;
						continue;
					}
					break;
				}
				if (A_0.BorderType == BorderStyle.Cleared)
				{
					num = 5;
					continue;
				}
				num = 11;
				continue;
				IL_B7:
				num = 1;
				continue;
				IL_10D:
				num = 12;
			}
		}
	}

	// Token: 0x06003140 RID: 12608 RVA: 0x002D7168 File Offset: 0x002D6168
	private static void ᜀ(Border A_0, spr\u22D4 A_1)
	{
		for (;;)
		{
			IL_00:
			int num = 18;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 3;
					continue;
				case 1:
					goto IL_1F6;
				case 2:
					if (A_1.ᜄ() == 255)
					{
						num = 12;
						continue;
					}
					goto IL_155;
				case 3:
					if (A_0.BorderType == BorderStyle.Cleared)
					{
						num = 1;
						continue;
					}
					goto IL_1A3;
				case 4:
					A_0.BorderType = BorderStyle.Single;
					num = 14;
					continue;
				case 5:
					goto IL_8E;
				case 6:
					if (!A_0.IsDefault)
					{
						num = 17;
						continue;
					}
					return;
				case 7:
					goto IL_19E;
				case 8:
					A_1.ᜂ(0);
					num = 13;
					continue;
				case 9:
					if (A_0.HasNoneStyle)
					{
						num = 5;
						continue;
					}
					return;
				case 10:
					num = 9;
					continue;
				case 11:
					if (A_0.BorderType == BorderStyle.None)
					{
						num = 10;
						continue;
					}
					goto IL_8E;
				case 12:
					if (true)
					{
					}
					num = 15;
					continue;
				case 13:
					goto IL_155;
				case 14:
					goto IL_E1;
				case 15:
					if ((byte)A_0.BorderType != 255)
					{
						num = 8;
						continue;
					}
					goto IL_155;
				case 16:
					if (!A_0.IsDefault)
					{
						num = 0;
						continue;
					}
					goto IL_1A3;
				case 17:
					num = 11;
					continue;
				}
				if (A_0.BorderType != BorderStyle.Hairline)
				{
					goto IL_E1;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_00;
				default:
					if (false)
					{
					}
					num = 4;
					continue;
				}
				IL_8E:
				num = 2;
				continue;
				IL_E1:
				num = 16;
				continue;
				IL_155:
				A_1.ᜄ((byte)A_0.BorderType);
				A_1.ᜀ((byte)(A_0.LineWidth * 8f));
				A_1.ᜀ(A_0.Shadow);
				A_1.ᜁ((byte)sprṡ.ᜃ(A_0.Color));
				num = 7;
				continue;
				IL_1A3:
				num = 6;
			}
		}
		IL_19E:
		return;
		IL_1F6:
		A_1.ᜄ(byte.MaxValue);
		A_1.ᜁ(byte.MaxValue);
		A_1.ᜀ(byte.MaxValue);
		A_1.ᜂ(byte.MaxValue);
	}

	// Token: 0x06003141 RID: 12609 RVA: 0x002D73C0 File Offset: 0x002D63C0
	internal static void ᜀ(spr\u224E A_0, Border A_1)
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
		Color color = A_0.ᜅ();
		float lineWidth = (float)A_0.ᜊ() / 8f;
		BorderStyle borderType = (BorderStyle)A_0.ᜄ();
		bool shadow = A_0.ᜋ();
		A_1.InitFormatting(color, lineWidth, borderType, shadow);
	}

	// Token: 0x06003142 RID: 12610 RVA: 0x002D7428 File Offset: 0x002D6428
	private static void ᜀ(spr\u22D4 A_0, Border A_1)
	{
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					return;
				case 2:
					goto IL_91;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_91;
					default:
						goto IL_BB;
					}
					break;
				}
				if (spr\u1B3A.ᜀ(A_0))
				{
					num = 1;
					continue;
				}
				num = 2;
				continue;
				IL_91:
				if (A_0.ᜇ())
				{
					goto IL_C4;
				}
				num = 3;
			}
			return;
			IL_BB:
			if (false)
			{
			}
			Color color = sprṡ.ᜂ((int)A_0.ᜃ());
			float lineWidth = (float)A_0.ᜆ() / 8f;
			BorderStyle borderType = (BorderStyle)A_0.ᜄ();
			bool shadow = A_0.ᜁ();
			A_1.InitFormatting(color, lineWidth, borderType, shadow);
			return;
			IL_C4:
			A_1.BorderType = BorderStyle.Cleared;
			A_1.HasNoneStyle = false;
			return;
		}
		}
	}

	// Token: 0x06003143 RID: 12611 RVA: 0x002D750C File Offset: 0x002D650C
	private static bool ᜀ(spr\u22D4 A_0)
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				num = 5;
				continue;
			case 1:
				goto IL_56;
			case 2:
				return true;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_56;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				break;
			case 5:
				if (A_0.ᜆ() == 0)
				{
					num = 4;
					continue;
				}
				return false;
			}
			if (A_0.ᜃ() == 0)
			{
				num = 0;
				continue;
			}
			return false;
			IL_56:
			if (A_0.ᜄ() != 0)
			{
				return false;
			}
			num = 2;
		}
		return true;
	}

	// Token: 0x06003144 RID: 12612 RVA: 0x002D75B8 File Offset: 0x002D65B8
	public static void ᜁ(spr\u2375 A_0, Paddings A_1)
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
		A_0.ᜂ((short)Math.Round((double)(A_1.Left * 20f)));
		A_0.ᜃ((short)Math.Round((double)(A_1.Right * 20f)));
		A_0.ᜁ((short)Math.Round((double)(A_1.Top * 20f)));
		A_0.ᜀ((short)Math.Round((double)(A_1.Bottom * 20f)));
	}

	// Token: 0x06003145 RID: 12613 RVA: 0x002D7658 File Offset: 0x002D6658
	public static void ᜀ(spr\u2375 A_0, Paddings A_1)
	{
		for (;;)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_88;
				case 2:
					A_1.Left = (float)A_0.ᜄ() / 20f;
					A_1.Right = (float)A_0.ᜆ() / 20f;
					A_1.Top = (float)A_0.ᜅ() / 20f;
					A_1.Bottom = (float)A_0.ᜂ() / 20f;
					num = 0;
					continue;
				}
				if (true)
				{
				}
				if (A_0 == null)
				{
					return;
				}
				num = 2;
			}
			IL_88:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_9E;
			}
		}
		IL_9E:
		if (false)
		{
		}
	}

	// Token: 0x06003146 RID: 12614 RVA: 0x002D7718 File Offset: 0x002D6718
	private static void ᜂ(sprẊ A_0, Borders A_1)
	{
		int num = 10;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.\u1717() != 4278190080U)
				{
					num = 11;
					continue;
				}
				return;
			case 1:
				if (A_0.ᜄ() != 4278190080U)
				{
					if (true)
					{
					}
					num = 5;
					continue;
				}
				goto IL_7D;
			case 2:
				if (A_0.\u1713() != 4278190080U)
				{
					num = 3;
					continue;
				}
				goto IL_144;
			case 3:
				goto IL_A0;
			case 4:
				return;
			case 5:
				A_1.Left.Color = sprṡ.ᜀ(A_0.ᜄ());
				num = 8;
				continue;
			case 6:
				goto IL_144;
			case 7:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A0;
				default:
					if (false)
					{
					}
					A_1.Bottom.Color = sprṡ.ᜀ(A_0.ᜈ());
					num = 9;
					continue;
				}
				break;
			case 8:
				goto IL_7D;
			case 9:
				goto IL_A2;
			case 11:
				A_1.Right.Color = sprṡ.ᜀ(A_0.\u1717());
				num = 4;
				continue;
			}
			if (A_0.ᜈ() != 4278190080U)
			{
				num = 7;
				continue;
			}
			goto IL_A2;
			IL_7D:
			num = 2;
			continue;
			IL_A0:
			A_1.Top.Color = sprṡ.ᜀ(A_0.\u1713());
			num = 6;
			continue;
			IL_A2:
			num = 1;
			continue;
			IL_144:
			num = 0;
		}
	}

	// Token: 0x06003147 RID: 12615 RVA: 0x002D78B8 File Offset: 0x002D68B8
	internal static void ᜀ(sprẊ A_0, Borders A_1, bool A_2 = false)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_0E:
				if (true)
				{
				}
				for (;;)
				{
					spr\u22D4[] array = new spr\u22D4[]
					{
						A_0.ᜅ(),
						A_0.ᜋ(),
						A_0.\u1715(),
						A_0.\u1712()
					};
					Border[] array2 = new Border[]
					{
						A_1.Left,
						A_1.Top,
						A_1.Right,
						A_1.Bottom
					};
					int num = 0;
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							bool a_ = array2[num].IsRead;
							array2[num].IsRead = true;
							spr\u1B3A.ᜀ(array[num], array2[num]);
							array2[num].IsRead = a_;
							num2 = 4;
							continue;
						}
						case 1:
							goto IL_137;
						case 2:
							goto IL_119;
						case 3:
							goto IL_119;
						case 4:
							goto IL_B5;
						case 5:
							if (A_2)
							{
								num2 = 0;
								continue;
							}
							spr\u1B3A.ᜀ(array[num], array2[num]);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_0E;
							default:
								if (false)
								{
								}
								num2 = 6;
								continue;
							}
							break;
						case 6:
							goto IL_B5;
						case 7:
							if (num >= array.Length)
							{
								num2 = 1;
								continue;
							}
							num2 = 5;
							continue;
						}
						break;
						IL_B5:
						num++;
						num2 = 3;
						continue;
						IL_119:
						num2 = 7;
					}
				}
			}
			IL_137:
			spr\u1B3A.ᜂ(A_0, A_1);
			return;
		}
	}

	// Token: 0x06003148 RID: 12616 RVA: 0x002D7A48 File Offset: 0x002D6A48
	internal static void ᜀ(byte[] A_0, Borders A_1, int A_2)
	{
		for (;;)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					A_1.Top.BorderType = (BorderStyle)A_0[A_2];
					A_1.Left.BorderType = (BorderStyle)A_0[A_2 + 1];
					A_1.Bottom.BorderType = (BorderStyle)A_0[A_2 + 2];
					A_1.Right.BorderType = (BorderStyle)A_0[A_2 + 3];
					num = 2;
					continue;
				case 2:
					goto IL_75;
				}
				if (A_2 >= A_0.Length)
				{
					return;
				}
				num = 0;
			}
			IL_75:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_8B;
			}
		}
		IL_8B:
		if (true)
		{
		}
		if (false)
		{
		}
	}

	// Token: 0x06003149 RID: 12617 RVA: 0x002D7AFC File Offset: 0x002D6AFC
	private static void ᜁ(sprẊ A_0, Borders A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 6;
			for (;;)
			{
				Color color4;
				switch (num)
				{
				case 0:
					goto IL_341;
				case 1:
					num = 38;
					continue;
				case 2:
				{
					Color color = A_1.Right.Color;
					num = 62;
					continue;
				}
				case 3:
					if (A_1.Bottom.Color != Color.Black)
					{
						num = 16;
						continue;
					}
					return;
				case 4:
					if (A_1.Top.BorderType == BorderStyle.None)
					{
						num = 58;
						continue;
					}
					goto IL_2F8;
				case 5:
					if (A_1.Left.BorderType == BorderStyle.None)
					{
						num = 37;
						continue;
					}
					goto IL_3D4;
				case 7:
					num = 9;
					continue;
				case 8:
					num = 13;
					continue;
				case 9:
					if (A_1.Left.BorderType == BorderStyle.None)
					{
						num = 36;
						continue;
					}
					goto IL_5CF;
				case 10:
					if (A_1.Right.BorderType == BorderStyle.None)
					{
						num = 25;
						continue;
					}
					goto IL_2D6;
				case 11:
					A_0.ᜀ(sprṡ.ᜂ(Color.White));
					num = 21;
					continue;
				case 12:
					if (A_1.Right.Color != Color.Black)
					{
						num = 57;
						continue;
					}
					goto IL_74B;
				case 13:
					if (A_1.Top.HasNoneStyle)
					{
						num = 43;
						continue;
					}
					goto IL_54B;
				case 14:
					goto IL_74B;
				case 15:
					goto IL_2D6;
				case 16:
					A_0.ᜁ(sprṡ.ᜂ(Color.White));
					num = 0;
					continue;
				case 17:
				{
					Color color2;
					if (!color2.IsEmpty)
					{
						num = 48;
						continue;
					}
					return;
				}
				case 18:
					if (A_1.Top.BorderType == BorderStyle.None)
					{
						num = 8;
						continue;
					}
					goto IL_4CD;
				case 19:
					num = 39;
					continue;
				case 20:
					goto IL_346;
				case 21:
					goto IL_2F8;
				case 22:
					num = 46;
					continue;
				case 23:
					num = 12;
					continue;
				case 24:
					if (A_1.Bottom.BorderType == BorderStyle.None)
					{
						num = 30;
						continue;
					}
					return;
				case 25:
					num = 31;
					continue;
				case 26:
					goto IL_578;
				case 27:
					num = 47;
					continue;
				case 28:
					if (A_1.Right.HasNoneStyle)
					{
						num = 54;
						continue;
					}
					goto IL_74B;
				case 29:
					num = 24;
					continue;
				case 30:
				{
					Color color2 = A_1.Bottom.Color;
					num = 17;
					continue;
				}
				case 31:
					if (A_1.Right.HasNoneStyle)
					{
						num = 15;
						continue;
					}
					goto IL_4EC;
				case 32:
					A_0.ᜃ(sprṡ.ᜂ(Color.White));
					num = 35;
					continue;
				case 33:
					if (A_1.Left.HasNoneStyle)
					{
						num = 7;
						continue;
					}
					goto IL_5CF;
				case 34:
					num = 55;
					continue;
				case 35:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_686;
					default:
						if (false)
						{
						}
						goto IL_5CF;
					}
					break;
				case 36:
					goto IL_686;
				case 37:
					num = 53;
					continue;
				case 38:
					if (A_1.Top.Color != Color.Black)
					{
						num = 11;
						continue;
					}
					goto IL_2F8;
				case 39:
					if (A_1.Top.BorderType == BorderStyle.None)
					{
						num = 34;
						continue;
					}
					goto IL_578;
				case 40:
					goto IL_54B;
				case 41:
					if (A_1.Top.HasNoneStyle)
					{
						num = 60;
						continue;
					}
					goto IL_2F8;
				case 42:
					num = 56;
					continue;
				case 43:
					goto IL_4CD;
				case 44:
					goto IL_3D4;
				case 45:
					if (A_1.Bottom.HasNoneStyle)
					{
						num = 29;
						continue;
					}
					return;
				case 46:
					if (A_1.Right.BorderType == BorderStyle.None)
					{
						num = 19;
						continue;
					}
					goto IL_578;
				case 47:
					if (A_1.Bottom.HasNoneStyle)
					{
						num = 20;
						continue;
					}
					goto IL_46E;
				case 48:
					num = 3;
					continue;
				case 49:
					goto IL_3A7;
				case 50:
					goto IL_4EC;
				case 51:
					if (A_1.Bottom.BorderType == BorderStyle.None)
					{
						num = 27;
						continue;
					}
					goto IL_346;
				case 52:
					if (A_1.Right.BorderType == BorderStyle.None)
					{
						num = 2;
						continue;
					}
					goto IL_74B;
				case 53:
					if (A_1.Left.HasNoneStyle)
					{
						num = 44;
						continue;
					}
					goto IL_3A7;
				case 54:
					num = 52;
					continue;
				case 55:
					if (A_1.Bottom.BorderType != BorderStyle.None)
					{
						num = 26;
						continue;
					}
					num = 33;
					continue;
				case 56:
					if (A_1.Left.Color != Color.Black)
					{
						num = 32;
						continue;
					}
					goto IL_5CF;
				case 57:
					A_0.ᜂ(sprṡ.ᜂ(Color.White));
					num = 14;
					continue;
				case 58:
				{
					Color color3 = A_1.Top.Color;
					num = 61;
					continue;
				}
				case 59:
					goto IL_36B;
				case 60:
					num = 4;
					continue;
				case 61:
				{
					Color color3;
					if (!color3.IsEmpty)
					{
						num = 1;
						continue;
					}
					goto IL_2F8;
				}
				case 62:
				{
					Color color;
					if (!color.IsEmpty)
					{
						num = 23;
						continue;
					}
					goto IL_74B;
				}
				case 63:
					if (!color4.IsEmpty)
					{
						num = 42;
						continue;
					}
					goto IL_5CF;
				}
				if (A_1.Left.BorderType == BorderStyle.None)
				{
					num = 22;
					continue;
				}
				goto IL_578;
				IL_2D6:
				spr\u1B3A.ᜀ(A_1.Right, A_0.\u1715());
				num = 50;
				continue;
				IL_2F8:
				num = 45;
				continue;
				IL_346:
				spr\u1B3A.ᜀ(A_1.Bottom, A_0.\u1712());
				if (true)
				{
				}
				num = 59;
				continue;
				IL_3A7:
				num = 10;
				continue;
				IL_3D4:
				spr\u1B3A.ᜀ(A_1.Left, A_0.ᜅ());
				num = 49;
				continue;
				IL_4CD:
				spr\u1B3A.ᜀ(A_1.Top, A_0.ᜋ());
				num = 40;
				continue;
				IL_4EC:
				num = 18;
				continue;
				IL_54B:
				num = 51;
				continue;
				IL_578:
				num = 5;
				continue;
				IL_5CF:
				num = 28;
				continue;
				IL_686:
				color4 = A_1.Left.Color;
				num = 63;
				continue;
				IL_74B:
				num = 41;
			}
			IL_341:
			return;
			IL_36B:
			IL_46E:
			spr\u1B3A.ᜀ(A_0, A_1);
			return;
		}
		}
	}

	// Token: 0x0600314A RID: 12618 RVA: 0x002D8284 File Offset: 0x002D7284
	private static void ᜀ(sprẊ A_0, Borders A_1)
	{
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				A_0.ᜀ(sprṡ.ᜂ(A_1.Top.Color));
				num = 2;
				continue;
			case 1:
				goto IL_F8;
			case 2:
				goto IL_120;
			case 3:
				if (A_1.Top.HasKey(1))
				{
					num = 0;
					continue;
				}
				goto IL_120;
			case 4:
				if (A_1.Left.HasKey(1))
				{
					num = 8;
					continue;
				}
				goto IL_88;
			case 6:
				A_0.ᜂ(sprṡ.ᜂ(A_1.Right.Color));
				num = 1;
				continue;
			case 7:
				A_0.ᜁ(sprṡ.ᜂ(A_1.Bottom.Color));
				num = 11;
				continue;
			case 8:
				goto IL_165;
			case 9:
				goto IL_88;
			case 10:
				if (A_1.Right.HasKey(1))
				{
					num = 6;
					continue;
				}
				goto IL_18B;
			case 11:
				goto IL_AE;
			}
			if (A_1.Bottom.HasKey(1))
			{
				num = 7;
				continue;
			}
			goto IL_AE;
			IL_88:
			num = 3;
			continue;
			IL_AE:
			num = 4;
			continue;
			IL_120:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_165:
				A_0.ᜃ(sprṡ.ᜂ(A_1.Left.Color));
				num = 9;
				break;
			default:
				if (false)
				{
				}
				num = 10;
				break;
			}
		}
		IL_F8:
		IL_18B:
		if (true)
		{
		}
	}

	// Token: 0x0600314B RID: 12619 RVA: 0x002D8424 File Offset: 0x002D7424
	private static void ᜁ(float A_0, sprḍ A_1)
	{
		for (;;)
		{
			if (true)
			{
			}
			int num = (int)Math.Round((double)(A_0 * 20f));
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
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
						byte[] array = new byte[]
						{
							0,
							1,
							15,
							3,
							0,
							0
						};
						byte[] bytes = BitConverter.GetBytes((ushort)num);
						bytes.CopyTo(array, 4);
						A_1.ᜀ(54835, array);
						break;
					}
					}
					num2 = 1;
					continue;
				case 1:
					return;
				case 2:
					if (num > 0)
					{
						num2 = 0;
						continue;
					}
					return;
				}
				break;
			}
		}
	}

	// Token: 0x0600314C RID: 12620 RVA: 0x002D84D4 File Offset: 0x002D74D4
	private static void ᜀ(float A_0, sprḍ A_1)
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
		short a_ = (short)Math.Round((double)(A_0 * 20f));
		spr\u1739 spr_u = new spr\u1739();
		spr_u.ᜌ(A_1);
		spr_u.ᜊ(a_);
		spr_u.ᜈ(A_1);
	}

	// Token: 0x0600314D RID: 12621 RVA: 0x002D853C File Offset: 0x002D753C
	private static void ᜀ(RowFormat A_0, sprḍ A_1)
	{
		short num;
		for (;;)
		{
			TableRow tableRow = A_0.OwnerBase as TableRow;
			num = (short)Math.Round((double)(tableRow.Height * 20f));
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_8C;
				case 1:
					if (tableRow.HeightType != TableRowHeightType.AtLeast)
					{
						num2 = 2;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_76;
					default:
						if (false)
						{
						}
						num2 = 3;
						continue;
					}
					break;
				case 2:
					if (true)
					{
					}
					num2 = 0;
					continue;
				case 3:
					goto IL_76;
				}
				break;
			}
		}
		IL_76:
		short num3 = num;
		goto IL_9A;
		IL_8C:
		num3 = ~num;
		IL_9A:
		num = num3;
		A_1.ᜀ(37895, num);
	}

	// Token: 0x0600314E RID: 12622 RVA: 0x002D85F4 File Offset: 0x002D75F4
	private static spr\u1CC1 ᜀ(int A_0, sprḍ A_1)
	{
		spr\u1CC1 spr_u1CC;
		for (;;)
		{
			spr_u1CC = A_1.ᜇ(A_0);
			int num = 2;
			for (;;)
			{
				if (true)
				{
				}
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
						spr_u1CC = new spr\u1CC1(A_0);
						A_1.ᜆ(spr_u1CC);
						break;
					}
					num = 1;
					continue;
				case 1:
					return spr_u1CC;
				case 2:
					if (spr_u1CC == null)
					{
						num = 0;
						continue;
					}
					return spr_u1CC;
				}
				break;
			}
		}
		return spr_u1CC;
	}

	// Token: 0x0600314F RID: 12623 RVA: 0x002D867C File Offset: 0x002D767C
	internal static void ᜁ(spr\u1739 A_0, RowFormat.TablePositioning A_1)
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
		A_0.ᜃ(A_1.ᜋ.WrapTextAround);
		A_0.ᜃ((short)(A_1.DistanceFromTop * 20f));
		A_0.ᜈ((short)(A_1.DistanceFromBottom * 20f));
		A_0.ᜅ((short)(A_1.DistanceFromLeft * 20f));
		A_0.ᜇ((short)(A_1.DistanceFromRight * 20f));
		A_0.ᜂ((short)(A_1.HorizPosition * 20f));
		A_0.ᜆ((short)(A_1.VertPosition * 20f));
		A_0.ᜀ(A_1.HorizPositionAbs);
		A_0.ᜀ(A_1.VertPositionAbs);
		A_0.ᜀ(A_1.HorizRelationTo);
		A_0.ᜀ(A_1.VertRelationTo);
	}

	// Token: 0x06003150 RID: 12624 RVA: 0x002D876C File Offset: 0x002D776C
	internal static void ᜀ(spr\u1739 A_0, RowFormat.TablePositioning A_1)
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
		A_1.ᜋ.WrapTextAround = A_0.\u1717();
		A_1.DistanceFromTop = (float)A_0.ᜋ() / 20f;
		A_1.DistanceFromBottom = (float)A_0.\u1712() / 20f;
		A_1.DistanceFromLeft = (float)A_0.\u1718() / 20f;
		A_1.DistanceFromRight = (float)A_0.ᜈ() / 20f;
		A_1.HorizPosition = (float)A_0.ᜉ() / 20f;
		A_1.VertPosition = (float)A_0.\u1715() / 20f;
		A_1.HorizPositionAbs = A_0.\u1713();
		A_1.VertPositionAbs = A_0.ᜅ();
		A_1.HorizRelationTo = A_0.ᜑ();
		A_1.VertRelationTo = A_0.ᜄ();
	}

	// Token: 0x06003151 RID: 12625 RVA: 0x002D885C File Offset: 0x002D785C
	public static void ᜀ(spr\u1739 A_0, TableRow A_1, bool A_2)
	{
		int a_ = 19;
		switch (0)
		{
		default:
		{
			string message;
			for (;;)
			{
				IL_B7:
				int num;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_1C3:
					num = 3;
					break;
				default:
					if (false)
					{
					}
					num2 = 0;
					num = 27;
					break;
				}
				CellFormat cellFormat;
				for (;;)
				{
					spr\u2375 spr_u;
					float num3;
					TableCell tableCell;
					Borders borders;
					sprẊ sprẊ;
					switch (num)
					{
					case 0:
						A_1.RowFormat.BackColor = A_0.ᜐ().ᜂ();
						A_1.RowFormat.TextureStyle = A_0.ᜐ().ᜁ();
						num = 26;
						continue;
					case 1:
						spr\u1B3A.ᜀ(spr_u, cellFormat.Paddings);
						cellFormat.SamePaddingsAsTable = false;
						num = 4;
						continue;
					case 2:
						goto IL_E6;
					case 3:
						goto IL_2F9;
					case 4:
						goto IL_228;
					case 5:
						A_1.RowFormat.CellSpacing = num3;
						num = 15;
						continue;
					case 6:
						goto IL_432;
					case 7:
						if (A_0.ᜇ()[num2].ᜂ() != Color.Empty)
						{
							num = 36;
							continue;
						}
						goto IL_14B;
					case 8:
						goto IL_1D4;
					case 9:
						goto IL_24E;
					case 10:
						if (num3 >= 0f)
						{
							num = 5;
							continue;
						}
						goto IL_583;
					case 11:
						if (num2 < A_0.ᜊ())
						{
							num = 31;
							continue;
						}
						goto IL_292;
					case 12:
						cellFormat.VerticalMerge = CellMerge.Continue;
						num = 8;
						continue;
					case 13:
						goto IL_292;
					case 14:
						message = string.Format(ClipboardData.b("㩸Ṻᅼ፾ꎂﲈ歷꾎ﶒ떔ﲞ쒠삤풦쪨\ud9aa쒬\udfae얰\udcb2잴鞶슸论삼龾ꣀ냂ꃆ믈껊곌믎듐ꇒꏖ뇘뫚돜￞藠蛢雤鏦裨藪賬鯮飰鳲鯴ퟶ苸쫺胼", a_), A_0.ᜊ(), A_1.Cells.Count);
						num = 29;
						continue;
					case 15:
						goto IL_483;
					case 16:
						if (num2 > A_1.Cells.Count - 1)
						{
							num = 14;
							continue;
						}
						tableCell = A_1.Cells[num2];
						cellFormat = tableCell.CellFormat;
						borders = cellFormat.Borders;
						sprẊ = A_0.ᜂ(num2);
						num = 7;
						continue;
					case 17:
						goto IL_488;
					case 18:
						if (A_1.Cells.Count <= 0)
						{
							num = 13;
							continue;
						}
						num = 16;
						continue;
					case 19:
						goto IL_4AC;
					case 20:
						if (A_0.ᜃ() != 0)
						{
							num = 23;
							continue;
						}
						goto IL_24E;
					case 21:
						if (sprẊ.ᜊ())
						{
							num = 35;
							continue;
						}
						goto IL_488;
					case 22:
						if (sprẊ.\u1716())
						{
							num = 32;
							continue;
						}
						goto IL_432;
					case 23:
						num = 28;
						continue;
					case 24:
						if (sprẊ.\u1714())
						{
							num = 19;
							continue;
						}
						goto IL_2F9;
					case 25:
						if (sprẊ.ᜁ())
						{
							num = 12;
							continue;
						}
						goto IL_1D4;
					case 26:
						goto IL_37F;
					case 27:
						goto IL_E6;
					case 28:
						A_1.HeightType = ((A_0.ᜃ() > 0) ? TableRowHeightType.AtLeast : TableRowHeightType.Exactly);
						A_1.Height = (float)Math.Abs(A_0.ᜃ()) / 20f;
						num = 9;
						continue;
					case 29:
						goto IL_428;
					case 30:
						if (A_0.ᜐ() != null)
						{
							num = 0;
							continue;
						}
						goto IL_37F;
					case 31:
						num = 18;
						continue;
					case 32:
						cellFormat.VerticalMerge = CellMerge.Start;
						num = 6;
						continue;
					case 33:
						goto IL_14B;
					case 34:
						if (spr_u != null)
						{
							num = 1;
							continue;
						}
						goto IL_228;
					case 35:
						cellFormat.HorizontalMerge = CellMerge.Continue;
						num = 17;
						continue;
					case 36:
						cellFormat.BackColor = A_0.ᜇ()[num2].ᜂ();
						num = 33;
						continue;
					}
					goto IL_B7;
					IL_E6:
					num = 11;
					continue;
					IL_14B:
					tableCell.ForeColor = A_0.ᜇ()[num2].ᜃ();
					tableCell.TextureStyle = A_0.ᜇ()[num2].ᜁ();
					cellFormat.VerticalAlignment = (VerticalAlignment)sprẊ.ᜌ();
					spr\u1B3A.ᜀ(sprẊ, borders, false);
					spr_u = A_0.ᜁ()[num2];
					num = 34;
					continue;
					IL_1D4:
					num = 22;
					continue;
					IL_228:
					num = 25;
					continue;
					IL_24E:
					A_1.IsHeader = A_0.ᜎ();
					num3 = (float)A_0.\u170D() / 20f;
					num = 10;
					continue;
					IL_292:
					spr\u1B3A.ᜀ(A_0, A_1.RowFormat.Positioning);
					spr\u1B3A.ᜀ(A_0.ᜆ(), A_1.RowFormat.Paddings);
					A_1.RowFormat.LeftIndent = (float)A_0.\u1716() / 20f;
					num = 30;
					continue;
					IL_2F9:
					TableCell tableCell2 = tableCell;
					tableCell2.CellFormat.TextDirection = sprẊ.ᜎ();
					tableCell2.CellFormat.FitText = sprẊ.ᜆ();
					num2++;
					num = 2;
					continue;
					IL_37F:
					num = 20;
					continue;
					IL_432:
					num = 21;
					continue;
					IL_488:
					num = 24;
				}
				IL_4AC:
				if (true)
				{
				}
				cellFormat.HorizontalMerge = CellMerge.Start;
				goto IL_1C3;
			}
			IL_428:
			throw new DLSException(message);
			IL_483:
			IL_583:
			A_1.RowFormat.IsAutoResized = A_0.ᜀ();
			A_1.RowFormat.Bidi = A_0.ᜏ();
			return;
		}
		}
	}

	// Token: 0x06003152 RID: 12626 RVA: 0x002D8E10 File Offset: 0x002D7E10
	public static void ᜀ(sprᯚ A_0, RowFormat A_1, TableRow A_2)
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
		Borders borders = A_1.Borders;
		spr\u1B3A.ᜀ(A_0.ᜃ(), borders.Left);
		spr\u1B3A.ᜀ(A_0.ᜁ(), borders.Right);
		spr\u1B3A.ᜀ(A_0.ᜆ(), borders.Top);
		spr\u1B3A.ᜀ(A_0.ᜂ(), borders.Bottom);
		spr\u1B3A.ᜀ(A_0.ᜅ(), borders.Horizontal);
		spr\u1B3A.ᜀ(A_0.ᜄ(), borders.Vertical);
	}

	// Token: 0x06003153 RID: 12627 RVA: 0x002D8EB8 File Offset: 0x002D7EB8
	public static void ᜁ(spr\u1739 A_0, TableRow A_1)
	{
		for (;;)
		{
			int num = 0;
			int num2 = 4;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return;
				case 1:
					goto IL_6D;
				case 2:
					goto IL_96;
				case 3:
					if (num < A_1.Cells.Count)
					{
						if (true)
						{
						}
						num2 = 2;
						continue;
					}
					return;
				case 4:
					goto IL_6D;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_96;
					default:
						if (false)
						{
						}
						if (num >= A_0.ᜊ())
						{
							num2 = 0;
							continue;
						}
						A_1.Cells[num].Width = (float)A_0.ᜃ(num) / 20f;
						num++;
						num2 = 1;
						continue;
					}
					break;
				}
				break;
				IL_6D:
				num2 = 3;
				continue;
				IL_96:
				num2 = 5;
			}
		}
	}

	// Token: 0x06003154 RID: 12628 RVA: 0x002D8F90 File Offset: 0x002D7F90
	public static void ᜀ(spr\u1739 A_0, TableRow A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = 0;
				int num2 = 16;
				for (;;)
				{
					float cellSpacing;
					CellFormat cellFormat;
					sprẊ sprẊ;
					Borders borders;
					switch (num2)
					{
					case 0:
						goto IL_72D;
					case 1:
						A_0.ᜄ((int)Math.Round((double)(cellSpacing * 20f)));
						num2 = 34;
						continue;
					case 2:
					{
						TableCell tableCell;
						if (tableCell.CellFormat.TextDirection != TextDirection.LeftToRight)
						{
							num2 = 32;
							continue;
						}
						goto IL_41C;
					}
					case 3:
						goto IL_26A;
					case 4:
					{
						A_0.ᜇ()[num].ᜀ(cellFormat.BackColor);
						TableCell tableCell;
						A_0.ᜇ()[num].ᜁ(tableCell.ForeColor);
						A_0.ᜇ()[num].ᜀ(tableCell.TextureStyle);
						num2 = 9;
						continue;
					}
					case 5:
						goto IL_72D;
					case 6:
						A_0.ᜂ(num).ᜆ(true);
						num2 = 44;
						continue;
					case 7:
						goto IL_3D5;
					case 8:
					{
						TableCell tableCell;
						if (tableCell.WidthUnit != 0)
						{
							num2 = 19;
							continue;
						}
						goto IL_4A0;
					}
					case 9:
						goto IL_26A;
					case 10:
						A_0.ᜂ(num).ᜇ(true);
						num2 = 45;
						continue;
					case 11:
						goto IL_4A0;
					case 12:
						goto IL_50E;
					case 13:
						if (cellFormat.HorizontalMerge == CellMerge.Continue)
						{
							num2 = 6;
							continue;
						}
						goto IL_3D5;
					case 14:
						if (A_1.Height > 0f)
						{
							num2 = 27;
							continue;
						}
						goto IL_5BF;
					case 15:
					{
						TableCell tableCell;
						if (tableCell.CellFormat.FitText)
						{
							num2 = 41;
							continue;
						}
						goto IL_463;
					}
					case 16:
						goto IL_50E;
					case 17:
						goto IL_26A;
					case 18:
						if (!cellFormat.SamePaddingsAsTable)
						{
							num2 = 31;
							continue;
						}
						goto IL_2BB;
					case 19:
					{
						TableCell tableCell;
						sprẊ.ᜁ(tableCell.WidthUnit);
						num2 = 11;
						continue;
					}
					case 20:
						A_0.ᜀ(FtsWidth.Percentage);
						num2 = 29;
						continue;
					case 21:
						A_0.ᜐ().ᜀ(A_1.RowFormat.BackColor);
						num2 = 14;
						continue;
					case 22:
						if (A_1.OwnerTable.PreferredTableWidth.ᜀ() == FtsWidth.Percentage)
						{
							num2 = 20;
							continue;
						}
						num2 = 25;
						continue;
					case 23:
						if (cellFormat.VerticalMerge == CellMerge.Continue)
						{
							num2 = 10;
							continue;
						}
						goto IL_34C;
					case 24:
					{
						sprẊ.ᜁ(2);
						TableCell tableCell;
						sprẊ.ᜀ.ᜁ((ushort)(tableCell.Scaling * 50f));
						num2 = 43;
						continue;
					}
					case 25:
						A_0.ᜉ((A_1.OwnerTable.TableGrid.Count > 0 && A_1.OwnerTable.DocxTableFormat.HasFormat) ? ((short)A_1.OwnerTable.TableGrid[A_1.OwnerTable.TableGrid.Count - 1]) : ((short)Math.Round((double)(A_1.OwnerTable.Width * 20f))));
						num2 = 5;
						continue;
					case 26:
						if (A_1.RowFormat.HasKey(108))
						{
							num2 = 40;
							continue;
						}
						num2 = 47;
						continue;
					case 27:
					{
						short num3 = (short)Math.Round((double)(A_1.Height * 20f));
						num2 = 28;
						continue;
					}
					case 28:
					{
						short num3;
						A_0.ᜄ((A_1.HeightType == TableRowHeightType.AtLeast) ? num3 : (~num3));
						num2 = 39;
						continue;
					}
					case 29:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_30C;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							A_0.ᜉ((short)(A_1.OwnerTable.DocxTableFormat.HasFormat ? (A_1.OwnerTable.DocxTableFormat.Format.Scaling * 50f) : (A_1.OwnerTable.TableFormat.Scaling * 50f)));
							num2 = 0;
							continue;
						}
						break;
					case 30:
						if (cellSpacing >= 0f)
						{
							num2 = 1;
							continue;
						}
						goto IL_7B3;
					case 31:
						spr\u1B3A.ᜁ(A_0.ᜁ()[num], cellFormat.Paddings);
						num2 = 38;
						continue;
					case 32:
					{
						TableCell tableCell;
						sprẊ.ᜁ(tableCell.CellFormat.TextDirection);
						num2 = 33;
						continue;
					}
					case 33:
						goto IL_41C;
					case 34:
						goto IL_7B1;
					case 35:
						if (cellFormat.VerticalMerge == CellMerge.Start)
						{
							num2 = 37;
							continue;
						}
						num2 = 23;
						continue;
					case 36:
						goto IL_30C;
					case 37:
						A_0.ᜂ(num).ᜅ(true);
						A_0.ᜂ(num).ᜇ(true);
						num2 = 36;
						continue;
					case 38:
						goto IL_2BB;
					case 39:
						goto IL_5BF;
					case 40:
					{
						RowFormat rowFormat = A_1.RowFormat;
						A_0.ᜇ()[num].ᜀ(rowFormat.BackColor);
						A_0.ᜇ()[num].ᜀ(rowFormat.TextureStyle);
						num2 = 3;
						continue;
					}
					case 41:
					{
						TableCell tableCell;
						sprẊ.ᜀ(tableCell.CellFormat.FitText);
						num2 = 42;
						continue;
					}
					case 42:
						goto IL_463;
					case 43:
						goto IL_4FC;
					case 44:
						goto IL_3D5;
					case 45:
						goto IL_34C;
					case 46:
					{
						if (num >= A_1.Cells.Count)
						{
							num2 = 21;
							continue;
						}
						TableCell tableCell = A_1.Cells[num];
						cellFormat = tableCell.CellFormat;
						borders = cellFormat.Borders;
						sprẊ = A_0.ᜂ(num);
						num2 = 51;
						continue;
					}
					case 47:
						if (A_1.OwnerTable.TableFormat.HasKey(108))
						{
							num2 = 49;
							continue;
						}
						goto IL_26A;
					case 48:
						A_0.ᜂ(num).ᜃ(true);
						A_0.ᜂ(num).ᜆ(false);
						num2 = 7;
						continue;
					case 49:
					{
						RowFormat tableFormat = A_1.OwnerTable.TableFormat;
						A_0.ᜇ()[num].ᜀ(tableFormat.BackColor);
						A_0.ᜇ()[num].ᜀ(tableFormat.TextureStyle);
						num2 = 17;
						continue;
					}
					case 50:
						if (cellFormat.HorizontalMerge == CellMerge.Start)
						{
							num2 = 48;
							continue;
						}
						num2 = 13;
						continue;
					case 51:
						if (cellFormat.HasKey(4))
						{
							num2 = 4;
							continue;
						}
						num2 = 26;
						continue;
					case 52:
					{
						TableCell tableCell;
						if (tableCell.WidthType == FtsWidth.Percentage)
						{
							num2 = 24;
							continue;
						}
						goto IL_4FC;
					}
					}
					break;
					IL_26A:
					sprẊ.ᜀ((byte)cellFormat.VerticalAlignment);
					num2 = 18;
					continue;
					IL_2BB:
					spr\u1B3A.ᜁ(sprẊ, borders);
					num2 = 35;
					continue;
					IL_34C:
					num2 = 50;
					continue;
					IL_30C:
					goto IL_34C;
					IL_3D5:
					num2 = 2;
					continue;
					IL_41C:
					num2 = 15;
					continue;
					IL_463:
					num2 = 8;
					continue;
					IL_4A0:
					num2 = 52;
					continue;
					IL_4FC:
					num++;
					num2 = 12;
					continue;
					IL_50E:
					num2 = 46;
					continue;
					IL_5BF:
					A_0.ᜊ((short)Math.Round((double)(A_1.RowFormat.LeftIndent * 20f)));
					num2 = 22;
					continue;
					IL_72D:
					spr\u1B3A.ᜁ(A_0.ᜆ(), A_1.RowFormat.Paddings);
					spr\u1B3A.ᜁ(A_0, A_1.RowFormat.Positioning);
					A_0.ᜀ(A_1.IsHeader);
					cellSpacing = A_1.RowFormat.CellSpacing;
					num2 = 30;
				}
			}
			IL_7B1:
			IL_7B3:
			A_0.ᜄ(A_1.RowFormat.IsAutoResized);
			A_0.ᜁ(A_1.RowFormat.Bidi);
			return;
		}
	}

	// Token: 0x06003155 RID: 12629 RVA: 0x002D9774 File Offset: 0x002D8774
	public static void ᜀ(sprᯚ A_0, RowFormat A_1)
	{
		switch (0)
		{
		default:
		{
			Borders borders;
			for (;;)
			{
				borders = A_1.Borders;
				int num = 10;
				for (;;)
				{
					Border right;
					Border bottom;
					Border left;
					Border top;
					switch (num)
					{
					case 0:
						right = A_1.Borders.Right;
						goto IL_260;
					case 1:
						right = borders.Right;
						goto IL_260;
					case 2:
						if (borders.Right.BorderType == BorderStyle.None)
						{
							num = 17;
							continue;
						}
						num = 1;
						continue;
					case 3:
						goto IL_140;
					case 4:
						if (borders.Top.BorderType == BorderStyle.None)
						{
							num = 15;
							continue;
						}
						num = 18;
						continue;
					case 5:
						num = 3;
						continue;
					case 6:
						num = 7;
						continue;
					case 7:
						bottom = A_1.Borders.Bottom;
						goto IL_21A;
					case 8:
						left = borders.Left;
						goto IL_1CF;
					case 9:
						top = A_1.Borders.Top;
						goto IL_E9;
					case 10:
						if (borders.Left.BorderType == BorderStyle.None)
						{
							num = 19;
							continue;
						}
						num = 8;
						continue;
					case 11:
						goto IL_1AD;
					case 12:
						if (borders.Horizontal.BorderType == BorderStyle.None)
						{
							num = 5;
							continue;
						}
						num = 11;
						continue;
					case 13:
						if (borders.Bottom.BorderType == BorderStyle.None)
						{
							num = 6;
							continue;
						}
						num = 14;
						continue;
					case 14:
						bottom = borders.Bottom;
						goto IL_21A;
					case 15:
						num = 9;
						continue;
					case 16:
						left = A_1.Borders.Left;
						goto IL_1CF;
					case 17:
						num = 0;
						continue;
					case 18:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							top = borders.Top;
							goto IL_E9;
						}
						break;
					case 19:
						if (true)
						{
						}
						num = 16;
						continue;
					}
					break;
					IL_E9:
					Border a_ = top;
					spr\u1B3A.ᜀ(a_, A_0.ᜆ());
					num = 13;
					continue;
					IL_1CF:
					Border a_2 = left;
					spr\u1B3A.ᜀ(a_2, A_0.ᜃ());
					num = 2;
					continue;
					IL_21A:
					Border a_3 = bottom;
					spr\u1B3A.ᜀ(a_3, A_0.ᜂ());
					num = 12;
					continue;
					IL_260:
					Border a_4 = right;
					spr\u1B3A.ᜀ(a_4, A_0.ᜁ());
					num = 4;
				}
			}
			IL_140:
			Border horizontal = A_1.Borders.Horizontal;
			goto IL_29A;
			IL_1AD:
			horizontal = borders.Horizontal;
			IL_29A:
			Border a_5 = horizontal;
			spr\u1B3A.ᜀ(a_5, A_0.ᜅ());
			spr\u1B3A.ᜀ(A_1.Borders.Vertical, A_0.ᜄ());
			return;
		}
		}
	}
}
