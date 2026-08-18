using System;
using System.Collections.Generic;
using System.Reflection;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020002E0 RID: 736
[DefaultMember("Item")]
internal class sprᢖ : CollectionExtended<spr\u192F>
{
	// Token: 0x06002D0D RID: 11533 RVA: 0x001955E8 File Offset: 0x001945E8
	internal sprᢖ(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x06002D0E RID: 11534 RVA: 0x00195608 File Offset: 0x00194608
	public new spr\u192F ᜁ(int A_0)
	{
		int a_ = 18;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0 < base.Count)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_8E;
					}
				}
				num = 2;
				continue;
			case 2:
				goto IL_6C;
			case 3:
				num = 0;
				continue;
			}
			if (true)
			{
			}
			if (A_0 < 0)
			{
				break;
			}
			num = 3;
		}
		IL_3F:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⅇ⑉⡋⭍⡏", a_));
		IL_6C:
		goto IL_3F;
		IL_8E:
		if (false)
		{
		}
		return base.InnerList[A_0];
	}

	// Token: 0x06002D0F RID: 11535 RVA: 0x001956B8 File Offset: 0x001946B8
	internal new XlsWorkbook ᜀ()
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
		return this.ᜁ(0).ᜎ();
	}

	// Token: 0x06002D10 RID: 11536 RVA: 0x00195700 File Offset: 0x00194700
	public new spr\u192F ᜁ(spr\u192F A_0)
	{
		int a_ = 8;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_175:
			num = 6;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				num = 20;
				break;
			}
			break;
		}
		for (;;)
		{
			XlsWorkbook xlsWorkbook;
			XlsWorkbook xlsWorkbook2;
			bool flag;
			switch (num)
			{
			case 0:
				if (base.Count >= xlsWorkbook.MaxXFCount)
				{
					num = 15;
					continue;
				}
				this.ᜁ.Add(A_0, A_0);
				num = 1;
				continue;
			case 1:
				goto IL_D3;
			case 2:
			{
				spr\u192F spr_u192F = A_0;
				A_0 = this.ᜁ[A_0];
				num = 14;
				continue;
			}
			case 3:
				goto IL_AC;
			case 4:
				num = 16;
				continue;
			case 5:
				goto IL_D3;
			case 6:
				num = 9;
				continue;
			case 7:
			{
				if (this.ᜁ.ContainsKey(A_0))
				{
					num = 2;
					continue;
				}
				int count = base.Count;
				num = 17;
				continue;
			}
			case 8:
				goto IL_D3;
			case 9:
				if (A_0.ᜠ() != 0)
				{
					num = 10;
					continue;
				}
				goto IL_125;
			case 10:
				num = 11;
				continue;
			case 11:
				if (A_0.ᜠ() < 15)
				{
					num = 21;
					continue;
				}
				goto IL_125;
			case 12:
				xlsWorkbook2 = this.ᜀ();
				goto IL_23C;
			case 13:
				A_0.ᜃ((int)((ushort)base.List.Count));
				base.Add(A_0);
				num = 18;
				continue;
			case 14:
				if (this.ᜀ().Version == ExcelVersion.Version97to2003)
				{
					goto IL_175;
				}
				goto IL_125;
			case 15:
				goto IL_266;
			case 16:
				xlsWorkbook2 = A_0.ᜎ();
				goto IL_23C;
			case 17:
			{
				int count;
				if (count <= 0)
				{
					num = 4;
					continue;
				}
				num = 12;
				continue;
			}
			case 18:
				return A_0;
			case 19:
				if (flag)
				{
					num = 13;
					continue;
				}
				return A_0;
			case 21:
			{
				spr\u192F spr_u192F;
				A_0 = spr_u192F;
				num = 8;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			flag = true;
			num = 7;
			continue;
			IL_D3:
			num = 19;
			continue;
			IL_125:
			flag = false;
			num = 5;
			continue;
			IL_23C:
			xlsWorkbook = xlsWorkbook2;
			num = 0;
		}
		IL_AC:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("堽⼿ぁ⥃❅㱇", a_));
		IL_266:
		throw new ApplicationException(RecordTableEnumerator.b("猽ℿ㩁ⵃ⭅㵇❉汋⁍╏㽑㙓㍕⩗穙㍛㡝䁟ݡᱣብ൧ѩ࡫୭ᑯ剱ታ᥵੷᝹ᵻ੽ꊁﺅ몓", a_));
	}

	// Token: 0x06002D11 RID: 11537 RVA: 0x001959B4 File Offset: 0x001949B4
	public new spr\u192F ᜀ(spr\u192F A_0)
	{
		int a_ = 3;
		int num = 4;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_B6;
			default:
				if (false)
				{
				}
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					this.ᜁ.Add(A_0, A_0);
					num = 3;
					continue;
				case 1:
					if (!this.ᜁ.ContainsKey(A_0))
					{
						num = 0;
						continue;
					}
					goto IL_B6;
				case 2:
					goto IL_54;
				case 3:
					goto IL_75;
				}
				if (A_0 == null)
				{
					num = 2;
				}
				else
				{
					num = 1;
				}
				break;
			}
		}
		IL_54:
		throw new ArgumentNullException(RecordTableEnumerator.b("弸吺似刾⁀㝂", a_));
		IL_75:
		IL_B6:
		A_0.ᜃ((int)((ushort)base.List.Count));
		base.Add(A_0);
		return A_0;
	}

	// Token: 0x06002D12 RID: 11538 RVA: 0x00195A94 File Offset: 0x00194A94
	public new int ᜀ(spr\u192F A_0, Dictionary<int, int> A_1)
	{
		int a_ = 2;
		switch (0)
		{
		default:
		{
			int num = 4;
			int num2;
			int num3;
			spr\u192F spr_u192F;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_BE;
				case 1:
				{
					int count;
					if (count > num2)
					{
						num = 3;
						continue;
					}
					goto IL_D7;
				}
				case 2:
					if (this.ᜁ(num2) == A_0)
					{
						num = 8;
						continue;
					}
					goto IL_D7;
				case 3:
					num = 2;
					continue;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return num2;
					default:
						if (false)
						{
						}
						num3 = A_1[num3];
						num = 0;
						continue;
					}
					break;
				case 6:
					goto IL_9B;
				case 7:
				{
					if (A_1 == null)
					{
						num = 6;
						continue;
					}
					num2 = A_0.ᜠ();
					int count = base.Count;
					num = 1;
					continue;
				}
				case 8:
					goto IL_81;
				case 9:
					goto IL_61;
				case 10:
					if (A_1.ContainsKey(num3))
					{
						if (true)
						{
						}
						num = 5;
						continue;
					}
					goto IL_173;
				}
				if (A_0 == null)
				{
					num = 9;
					continue;
				}
				num = 7;
				continue;
				IL_D7:
				spr_u192F = A_0.ᜀ(this);
				num3 = spr_u192F.ᜯ();
				num = 10;
			}
			IL_61:
			throw new ArgumentNullException(RecordTableEnumerator.b("帷唹主匽ℿ㙁", a_));
			IL_81:
			return num2;
			IL_9B:
			throw new ArgumentNullException(RecordTableEnumerator.b("倷嬹伻嘽Կ㩁ぃE❇㡉⅋⽍⑏᭑㩓㉕㵗≙㥛ⵝ", a_));
			IL_BE:
			IL_173:
			spr_u192F.ᜄ(num3);
			spr_u192F = this.ᜁ(spr_u192F);
			return spr_u192F.ᜠ();
		}
		}
	}

	// Token: 0x06002D13 RID: 11539 RVA: 0x00195C2C File Offset: 0x00194C2C
	public new Dictionary<int, int> ᜀ(IList<spr\u192F> A_0, out Dictionary<int, int> A_1)
	{
		int a_ = 11;
		switch (0)
		{
		default:
		{
			int num = 6;
			XlsWorkbook xlsWorkbook;
			XlsWorkbook xlsWorkbook2;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					int count;
					if (count == 0)
					{
						num = 3;
						continue;
					}
					spr\u192F spr_u192F = A_0[0];
					xlsWorkbook = spr_u192F.ᜎ();
					xlsWorkbook2 = (base.FindParent(typeof(XlsWorkbook)) as XlsWorkbook);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_57;
					default:
						if (false)
						{
						}
						num = 7;
						continue;
					}
					break;
				}
				case 1:
				{
					if (A_0 == this)
					{
						num = 4;
						continue;
					}
					int count = A_0.Count;
					if (true)
					{
					}
					num = 0;
					continue;
				}
				case 2:
					goto IL_121;
				case 3:
					goto IL_7D;
				case 4:
					goto IL_98;
				case 5:
					goto IL_55;
				case 7:
					if (xlsWorkbook2 == null)
					{
						num = 2;
						continue;
					}
					goto IL_128;
				}
				if (A_0 == null)
				{
					num = 5;
				}
				else
				{
					A_1 = null;
					num = 1;
				}
			}
			IL_55:
			throw new ArgumentNullException(RecordTableEnumerator.b("⁀ㅂ㝄὆཈⑊㽌≎ぐ❒♔", a_));
			IL_57:
			return null;
			IL_7D:
			goto IL_57;
			IL_98:
			return null;
			IL_121:
			throw new ArgumentNullException(RecordTableEnumerator.b("Հ♂㙄㍆⁈╊ⱌ㭎㡐㱒㭔睖⹘㑚⽜㑞͠ౢ੤౦䥨ࡪ౬ŮὰᱲŴ坶᭸Ṻ嵼᥾ꞈ", a_));
			IL_128:
			A_1 = xlsWorkbook2.InnerFonts.AddRange(xlsWorkbook.InnerFonts);
			Dictionary<int, int> a_2 = xlsWorkbook2.InnerFormats.ᜀ(xlsWorkbook.InnerFormats);
			return this.ᜀ(A_0, A_1, a_2);
		}
		}
	}

	// Token: 0x06002D14 RID: 11540 RVA: 0x00195D9C File Offset: 0x00194D9C
	public new Dictionary<int, int> ᜂ(IList<spr\u192F> A_0)
	{
		int a_ = 5;
		if (A_0 != null)
		{
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
				Dictionary<int, int> a_2 = this.ᜁ(A_0);
				Dictionary<int, int> a_3 = this.ᜀ(A_0);
				return this.ᜀ(A_0, a_2, a_3);
			}
			}
		}
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("娺似䴾᥀Ղ⩄㕆⑈⩊㥌㱎", a_));
	}

	// Token: 0x06002D15 RID: 11541 RVA: 0x00195E14 File Offset: 0x00194E14
	public new void ᜀ(Dictionary<int, object> A_0, IList<spr\u192F> A_1, int A_2)
	{
		int a_ = 9;
		int num = 1;
		for (;;)
		{
			IL_13:
			switch (num)
			{
			case 0:
			{
				A_0.Add(A_2, null);
				spr\u192F spr_u192F = this.ᜁ(A_2);
				A_1.Add(spr_u192F);
				num = 2;
				continue;
			}
			case 2:
			{
				spr\u192F spr_u192F;
				if (spr_u192F.ᝇ())
				{
					num = 6;
					continue;
				}
				return;
			}
			case 3:
				goto IL_C2;
			case 4:
				goto IL_EF;
			case 5:
				if (A_1 == null)
				{
					num = 4;
					continue;
				}
				num = 7;
				continue;
			case 6:
			{
				spr\u192F spr_u192F;
				this.ᜀ(A_0, A_1, spr_u192F.ᜯ());
				num = 3;
				continue;
			}
			case 7:
				while (!A_0.ContainsKey(A_2))
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
						num = 0;
						goto IL_13;
					}
				}
				return;
			case 8:
				goto IL_4B;
			}
			if (A_0 == null)
			{
				num = 8;
			}
			else
			{
				if (true)
				{
				}
				num = 5;
			}
		}
		IL_4B:
		throw new ArgumentNullException(RecordTableEnumerator.b("圾⁀あⵄፆ♈੊⥌⭎", a_));
		IL_C2:
		return;
		IL_EF:
		throw new ArgumentNullException(RecordTableEnumerator.b("帾㍀ㅂᵄņ♈㥊⁌⹎═⁒", a_));
	}

	// Token: 0x06002D16 RID: 11542 RVA: 0x00195F54 File Offset: 0x00194F54
	public new spr\u192F ᜀ(int A_0, int A_1)
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			int num = 5;
			spr\u192F spr_u192F3;
			IBorder border;
			IBorder border2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_1D4;
				case 1:
				{
					if (A_0 == A_1)
					{
						num = 7;
						continue;
					}
					spr\u192F spr_u192F = this.ᜁ(A_0);
					spr\u192F spr_u192F2 = this.ᜁ(A_1);
					spr_u192F3 = (spr\u192F)spr_u192F.\u1758();
					num = 8;
					continue;
				}
				case 2:
					goto IL_15C;
				case 3:
					goto IL_1B3;
				case 4:
					num = 9;
					continue;
				case 6:
					if (A_0 < 0)
					{
						num = 2;
						continue;
					}
					num = 13;
					continue;
				case 7:
					goto IL_DA;
				case 8:
				{
					spr\u192F spr_u192F2;
					if (spr_u192F2.\u1719())
					{
						if (true)
						{
						}
						num = 12;
						continue;
					}
					spr\u192F spr_u192F4 = this.ᜁ(spr_u192F2.ᜯ());
					border = spr_u192F4.ᜪ()[BordersLineType.EdgeRight];
					border2 = spr_u192F4.ᜪ()[BordersLineType.EdgeBottom];
					num = 3;
					continue;
				}
				case 9:
					if (A_1 < 0)
					{
						num = 0;
						continue;
					}
					num = 1;
					continue;
				case 10:
					goto IL_11C;
				case 11:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_DA;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					break;
				case 12:
				{
					spr\u192F spr_u192F2;
					border = spr_u192F2.ᜪ()[BordersLineType.EdgeRight];
					border2 = spr_u192F2.ᜪ()[BordersLineType.EdgeBottom];
					num = 10;
					continue;
				}
				case 13:
					if (A_1 < base.Count)
					{
						num = 4;
						continue;
					}
					goto IL_1F9;
				}
				if (A_0 >= base.Count)
				{
					goto IL_DF;
				}
				num = 11;
			}
			IL_DA:
			return (spr\u192F)this.ᜁ(A_0).\u1758();
			IL_DF:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("張縷匹主䴽㐿ᩁɃ", a_));
			IL_11C:
			goto IL_20D;
			IL_15C:
			goto IL_DF;
			IL_1B3:
			goto IL_20D;
			IL_1D4:
			IL_1F9:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("張紷吹堻昽ؿ", a_));
			IL_20D:
			IBorder border3 = spr_u192F3.ᜪ()[BordersLineType.EdgeRight];
			border3.OColor.ᜀ(border.OColor, true);
			border3.LineStyle = border.LineStyle;
			border3 = spr_u192F3.ᜪ()[BordersLineType.EdgeBottom];
			border3.OColor.ᜀ(border2.OColor, true);
			border3.LineStyle = border2.LineStyle;
			return spr_u192F3;
		}
		}
	}

	// Token: 0x06002D17 RID: 11543 RVA: 0x001961D0 File Offset: 0x001951D0
	public new Dictionary<int, int> ᜀ(int A_0)
	{
		switch (0)
		{
		default:
		{
			Dictionary<int, int> dictionary;
			for (;;)
			{
				int num = base.Count;
				int num2 = 47;
				for (;;)
				{
					int num4;
					int num5;
					int num6;
					int num7;
					int num10;
					switch (num2)
					{
					case 0:
					{
						int num3;
						if (num3 == A_0)
						{
							num2 = 43;
							continue;
						}
						goto IL_2C4;
					}
					case 1:
						base.InnerList.RemoveAt(num4);
						num--;
						num4--;
						num2 = 18;
						continue;
					case 2:
						goto IL_51B;
					case 3:
					{
						if (num5 >= num)
						{
							num2 = 39;
							continue;
						}
						spr\u192F spr_u192F = this.ᜁ(num5);
						num2 = 49;
						continue;
					}
					case 4:
						goto IL_3B7;
					case 5:
					{
						if (num6 >= num)
						{
							num2 = 11;
							continue;
						}
						spr\u192F spr_u192F = this.ᜁ(num6);
						num2 = 44;
						continue;
					}
					case 6:
					{
						int count;
						if (num7 >= count)
						{
							num2 = 37;
							continue;
						}
						IList<int> keys;
						int num8 = keys[num7];
						spr\u192F spr_u192F = this.ᜁ(num8);
						num2 = 8;
						continue;
					}
					case 7:
						goto IL_255;
					case 8:
					{
						spr\u192F spr_u192F;
						if (spr_u192F != null)
						{
							num2 = 34;
							continue;
						}
						goto IL_3B7;
					}
					case 9:
					{
						spr\u192F spr_u192F;
						if (spr_u192F == null)
						{
							num2 = 1;
							continue;
						}
						goto IL_5D0;
					}
					case 10:
						goto IL_255;
					case 11:
						num4 = 0;
						num2 = 10;
						continue;
					case 12:
					{
						spr\u192F spr_u192F;
						int num9;
						spr_u192F.ᜄ((int)((ushort)dictionary[num9]));
						num2 = 24;
						continue;
					}
					case 13:
					{
						if (A_0 > num)
						{
							num2 = 23;
							continue;
						}
						SortedList<int, spr\u192F> sortedList = new SortedList<int, spr\u192F>();
						spr\u192F spr_u192F = this.ᜁ(A_0);
						this.ᜁ.Remove(spr_u192F);
						dictionary = new Dictionary<int, int>();
						base.InnerList[A_0] = null;
						dictionary[A_0] = 0;
						num10 = 0;
						num2 = 36;
						continue;
					}
					case 14:
						goto IL_2C4;
					case 15:
					{
						int num12;
						int num11 = num5 - num12;
						dictionary.Add(num5, num11);
						spr\u192F spr_u192F;
						spr_u192F.ᜃ((int)((ushort)num11));
						num2 = 25;
						continue;
					}
					case 16:
						goto IL_27A;
					case 17:
					{
						SortedList<int, spr\u192F> sortedList;
						int count = sortedList.Count;
						IList<int> keys = sortedList.Keys;
						num7 = 0;
						num2 = 26;
						continue;
					}
					case 18:
						goto IL_5D0;
					case 19:
						num2 = 52;
						continue;
					case 20:
					{
						if (num4 >= num)
						{
							num2 = 30;
							continue;
						}
						spr\u192F spr_u192F = this.ᜁ(num4);
						num2 = 9;
						continue;
					}
					case 21:
					{
						spr\u192F spr_u192F;
						int num3 = spr_u192F.ᜯ();
						num2 = 0;
						continue;
					}
					case 22:
						goto IL_167;
					case 23:
						goto IL_2BF;
					case 24:
						goto IL_18C;
					case 25:
						goto IL_20C;
					case 26:
						goto IL_27A;
					case 27:
					{
						spr\u192F spr_u192F;
						spr\u192F value;
						if (this.ᜁ.TryGetValue(spr_u192F, out value))
						{
							num2 = 46;
							continue;
						}
						this.ᜁ.Add(spr_u192F, spr_u192F);
						int num8;
						SortedList<int, spr\u192F> sortedList;
						sortedList[num8] = spr_u192F;
						num2 = 50;
						continue;
					}
					case 28:
						goto IL_142;
					case 29:
					{
						spr\u192F spr_u192F;
						int num9 = spr_u192F.ᜯ();
						num2 = 40;
						continue;
					}
					case 30:
						return dictionary;
					case 31:
						goto IL_51B;
					case 32:
						num2 = 13;
						continue;
					case 33:
					{
						int count;
						int num13;
						if (num13 >= count)
						{
							num2 = 48;
							continue;
						}
						IList<int> keys;
						int key = keys[num13];
						SortedList<int, spr\u192F> sortedList;
						spr\u192F spr_u192F = sortedList[key];
						dictionary[key] = spr_u192F.ᜠ();
						num13++;
						num2 = 2;
						continue;
					}
					case 34:
					{
						spr\u192F spr_u192F;
						spr\u192F value = this.ᜁ[spr_u192F];
						num2 = 27;
						continue;
					}
					case 35:
					{
						if (num10 >= num)
						{
							num2 = 17;
							continue;
						}
						spr\u192F spr_u192F = this.ᜁ(num10);
						num2 = 42;
						continue;
					}
					case 36:
						goto IL_167;
					case 37:
					{
						num = base.Count;
						int num12 = 0;
						num5 = 0;
						num2 = 51;
						continue;
					}
					case 38:
						goto IL_20C;
					case 39:
					{
						SortedList<int, spr\u192F> sortedList;
						IList<int> keys = sortedList.Keys;
						int num13 = 0;
						num2 = 31;
						continue;
					}
					case 40:
					{
						int num9;
						if (num9 != 4095)
						{
							num2 = 19;
							continue;
						}
						goto IL_18C;
					}
					case 41:
						goto IL_34A;
					case 42:
					{
						spr\u192F spr_u192F;
						if (spr_u192F != null)
						{
							num2 = 21;
							continue;
						}
						goto IL_2C4;
					}
					case 43:
					{
						spr\u192F spr_u192F;
						this.ᜁ.Remove(spr_u192F);
						spr_u192F.ᜄ(0);
						spr_u192F.ᝂ();
						SortedList<int, spr\u192F> sortedList;
						sortedList.Add(num10, null);
						num2 = 14;
						continue;
					}
					case 44:
					{
						spr\u192F spr_u192F;
						if (spr_u192F != null)
						{
							num2 = 29;
							continue;
						}
						goto IL_18C;
					}
					case 45:
						goto IL_34A;
					case 46:
					{
						int num8;
						SortedList<int, spr\u192F> sortedList;
						spr\u192F value;
						sortedList[num8] = value;
						base.InnerList[num8] = null;
						num2 = 4;
						continue;
					}
					case 47:
						if (A_0 >= 0)
						{
							num2 = 32;
							continue;
						}
						goto IL_519;
					case 48:
						num6 = 0;
						num2 = 41;
						continue;
					case 49:
					{
						spr\u192F spr_u192F;
						if (spr_u192F != null)
						{
							num2 = 15;
							continue;
						}
						int num12;
						num12++;
						goto IL_5BF;
					}
					case 50:
						goto IL_3B7;
					case 51:
						goto IL_142;
					case 52:
						if (dictionary.ContainsKey(num6))
						{
							num2 = 12;
							continue;
						}
						goto IL_18C;
					}
					break;
					IL_142:
					num2 = 3;
					continue;
					IL_167:
					num2 = 35;
					continue;
					IL_18C:
					num6++;
					num2 = 45;
					continue;
					IL_20C:
					num5++;
					num2 = 28;
					continue;
					IL_255:
					num2 = 20;
					continue;
					IL_27A:
					num2 = 6;
					continue;
					IL_2C4:
					num10++;
					num2 = 22;
					continue;
					IL_34A:
					num2 = 5;
					continue;
					IL_3B7:
					num7++;
					num2 = 16;
					continue;
					IL_51B:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_5BF:
						num2 = 38;
						continue;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						num2 = 33;
						continue;
					}
					IL_5D0:
					num4++;
					num2 = 7;
				}
			}
			return dictionary;
			IL_2BF:
			IL_519:
			return null;
		}
		}
	}

	// Token: 0x06002D18 RID: 11544 RVA: 0x0019684C File Offset: 0x0019584C
	public virtual object ᜀ(object A_0)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			int num = 7;
			for (;;)
			{
				sprᢖ sprᢖ;
				spr\u192F spr_u192F;
				int num2;
				int count;
				List<spr\u192F> innerList;
				switch (num)
				{
				case 0:
					return sprᢖ;
				case 1:
					this.ᜁ.Add(spr_u192F, spr_u192F);
					num = 2;
					continue;
				case 2:
					goto IL_66;
				case 3:
					goto IL_E9;
				case 4:
					goto IL_E9;
				case 5:
					if (num2 >= count)
					{
						num = 0;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return sprᢖ;
					default:
						if (false)
						{
						}
						spr_u192F = innerList[num2];
						num = 6;
						continue;
					}
					break;
				case 6:
					if (!this.ᜁ.ContainsKey(spr_u192F))
					{
						num = 1;
						continue;
					}
					goto IL_66;
				case 7:
					if (true)
					{
					}
					break;
				case 8:
					goto IL_64;
				}
				if (A_0 == null)
				{
					num = 8;
					continue;
				}
				sprᢖ = (sprᢖ)base.Clone(A_0);
				innerList = sprᢖ.InnerList;
				num2 = 0;
				count = innerList.Count;
				num = 4;
				continue;
				IL_66:
				spr_u192F.ᜃ(num2);
				num2++;
				num = 3;
				continue;
				IL_E9:
				num = 5;
			}
			IL_64:
			throw new ArgumentNullException(RecordTableEnumerator.b("伾⁀ㅂ⁄⥆㵈", a_));
		}
		}
	}

	// Token: 0x06002D19 RID: 11545 RVA: 0x001969AC File Offset: 0x001959AC
	public new void ᜂ(int A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 5;
			for (;;)
			{
				int num2;
				int count;
				switch (num)
				{
				case 0:
				{
					if (num2 >= count)
					{
						num = 9;
						continue;
					}
					List<spr\u192F> innerList;
					spr\u192F spr_u192F = innerList[num2];
					num = 1;
					continue;
				}
				case 1:
				{
					IL_171:
					spr\u192F spr_u192F;
					int maxXFCount;
					if (spr_u192F.ᜯ() == maxXFCount)
					{
						num = 7;
						continue;
					}
					goto IL_6B;
				}
				case 2:
					return;
				case 3:
				{
					XlsWorkbook xlsWorkbook = this.ᜁ(0).ᜎ();
					int maxXFCount = xlsWorkbook.MaxXFCount;
					List<spr\u192F> innerList = base.InnerList;
					int count2 = innerList.Count;
					num = 10;
					continue;
				}
				case 4:
				{
					List<spr\u192F> innerList;
					int count2;
					innerList.RemoveRange(A_0 - 1, count2 - A_0);
					num = 11;
					continue;
				}
				case 6:
					if (true)
					{
					}
					goto IL_6B;
				case 7:
				{
					spr\u192F spr_u192F;
					spr_u192F.ᜄ(A_0);
					num = 6;
					continue;
				}
				case 8:
					goto IL_112;
				case 9:
				{
					XlsWorkbook xlsWorkbook;
					xlsWorkbook.UpdateXFIndexes(A_0);
					num = 2;
					continue;
				}
				case 10:
				{
					int count2;
					if (count2 >= A_0)
					{
						num = 4;
						continue;
					}
					goto IL_18F;
				}
				case 11:
					goto IL_18F;
				case 12:
					goto IL_112;
				}
				if (base.Count > 0)
				{
					num = 3;
					continue;
				}
				break;
				IL_6B:
				num2++;
				num = 12;
				continue;
				IL_112:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_171;
				default:
					if (false)
					{
					}
					num = 0;
					continue;
				}
				IL_18F:
				num2 = 0;
				count = base.Count;
				num = 8;
			}
			return;
		}
		}
	}

	// Token: 0x06002D1A RID: 11546 RVA: 0x00196B64 File Offset: 0x00195B64
	internal new void ᜀ(int A_0, spr\u192F A_1)
	{
		int a_ = 18;
		for (;;)
		{
			IL_09:
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_68;
				case 2:
					if (A_1 == null)
					{
						num = 3;
						continue;
					}
					goto IL_A7;
				case 3:
					goto IL_91;
				}
				if (true)
				{
				}
				if (A_0 >= base.Count)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					default:
						if (false)
						{
						}
						num = 1;
						break;
					}
				}
				else
				{
					num = 2;
				}
			}
		}
		IL_68:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⅇ቉ੋݍ㹏㙑ㅓ⹕", a_));
		IL_91:
		throw new ArgumentNullException(RecordTableEnumerator.b("⹇╉㹋⍍ㅏ♑", a_));
		IL_A7:
		spr\u192F key = this.ᜁ(A_0);
		this.ᜁ.Remove(key);
		base.InnerList[A_0] = A_1;
		this.ᜁ.Add(A_1, A_1);
	}

	// Token: 0x06002D1B RID: 11547 RVA: 0x00196C48 File Offset: 0x00195C48
	private new Dictionary<int, int> ᜀ(IList<spr\u192F> A_0, Dictionary<int, int> A_1, Dictionary<int, int> A_2)
	{
		int a_ = 4;
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 1:
				{
					if (A_2 == null)
					{
						num = 4;
						continue;
					}
					int count = A_0.Count;
					Dictionary<int, int> dictionary = new Dictionary<int, int>(count);
					num2 = 0;
					num = 3;
					continue;
				}
				case 2:
					if (A_1 == null)
					{
						num = 12;
						continue;
					}
					goto IL_16A;
				case 3:
					if (true)
					{
					}
					goto IL_14C;
				case 4:
					goto IL_188;
				case 5:
					goto IL_14C;
				case 6:
				{
					int count;
					if (num2 >= count)
					{
						num = 9;
						continue;
					}
					spr\u192F spr_u192F = A_0[num2];
					int key = spr_u192F.ᜠ();
					num = 10;
					continue;
				}
				case 7:
				{
					Dictionary<int, int> dictionary;
					spr\u192F spr_u192F;
					this.ᜀ(spr_u192F, dictionary, A_1, A_2);
					num = 11;
					continue;
				}
				case 8:
					goto IL_69;
				case 9:
				{
					Dictionary<int, int> dictionary;
					return dictionary;
				}
				case 10:
				{
					Dictionary<int, int> dictionary;
					int key;
					if (dictionary.ContainsKey(key))
					{
						goto IL_C9;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_16A;
					default:
						if (false)
						{
						}
						num = 7;
						continue;
					}
					break;
				}
				case 11:
					goto IL_C9;
				case 12:
					goto IL_C4;
				}
				if (A_0 == null)
				{
					num = 8;
					continue;
				}
				num = 2;
				continue;
				IL_C9:
				num2++;
				num = 5;
				continue;
				IL_14C:
				num = 6;
				continue;
				IL_16A:
				num = 1;
			}
			IL_69:
			throw new ArgumentNullException(RecordTableEnumerator.b("嬹主䰽ᠿс⭃㑅╇⭉㡋㵍", a_));
			IL_C4:
			throw new ArgumentNullException(RecordTableEnumerator.b("帹唻崽ؿⵁ⩃㉅Ň⑉⡋⭍⡏㝑❓", a_));
			IL_188:
			throw new ArgumentNullException(RecordTableEnumerator.b("帹唻崽ؿⵁ㙃⭅⥇㹉Ջ⁍㑏㝑ⱓ㍕⭗", a_));
		}
		}
	}

	// Token: 0x06002D1C RID: 11548 RVA: 0x00196E08 File Offset: 0x00195E08
	private new void ᜀ(spr\u192F A_0, Dictionary<int, int> A_1, Dictionary<int, int> A_2, Dictionary<int, int> A_3)
	{
		int a_ = 4;
		switch (0)
		{
		default:
		{
			int num = 4;
			spr\u192F spr_u192F;
			int key;
			for (;;)
			{
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
					int num2;
					int num3;
					switch (num)
					{
					case 0:
						goto IL_131;
					case 1:
					{
						sprᢖ sprᢖ;
						this.ᜀ(sprᢖ.ᜁ(num2), A_1, A_2, A_3);
						num = 2;
						continue;
					}
					case 2:
						goto IL_B6;
					case 3:
						if (A_0.ᝇ())
						{
							num = 5;
							continue;
						}
						goto IL_1EF;
					case 5:
						spr_u192F.ᜄ(num2);
						num = 16;
						continue;
					case 6:
						goto IL_1B0;
					case 7:
						if (A_3 != null)
						{
							num = 13;
							continue;
						}
						goto IL_27C;
					case 8:
						num3 = A_3[num3];
						spr_u192F.ᜑ().ᜈ((ushort)num3);
						num = 6;
						continue;
					case 9:
						num = 12;
						continue;
					case 10:
						goto IL_133;
					case 11:
						if (A_3.ContainsKey(num3))
						{
							num = 8;
							continue;
						}
						goto IL_27C;
					case 12:
						if (!A_1.ContainsKey(num2))
						{
							goto IL_1CD;
						}
						goto IL_B6;
					case 13:
						num = 11;
						continue;
					case 14:
					{
						if (A_1 == null)
						{
							if (true)
							{
							}
							num = 0;
							continue;
						}
						sprᢖ sprᢖ = A_0.\u1714();
						num2 = A_0.ᜯ();
						num = 15;
						continue;
					}
					case 15:
						if (A_0.ᝇ())
						{
							num = 9;
							continue;
						}
						goto IL_133;
					case 16:
						goto IL_1EF;
					case 17:
						goto IL_99;
					}
					if (A_0 == null)
					{
						num = 17;
						continue;
					}
					num = 14;
					continue;
					IL_B6:
					num2 = A_1[num2];
					num = 10;
					continue;
					IL_133:
					key = A_0.ᜠ();
					spr_u192F = A_0.ᜀ(this);
					num3 = (int)A_0.ᜑ().ᜂ();
					int key2 = (int)A_0.ᜑ().\u171D();
					num = 3;
					continue;
					IL_1EF:
					spr_u192F.ᜑ().ᜉ((ushort)A_2[key2]);
					num = 7;
					continue;
				}
				}
				IL_1CD:
				num = 1;
			}
			IL_99:
			throw new ArgumentNullException(RecordTableEnumerator.b("尹医䰽ⴿ⍁ぃ", a_));
			IL_131:
			throw new ArgumentNullException(RecordTableEnumerator.b("刹崻䴽⠿၁⅃㕅㵇♉㡋", a_));
			IL_1B0:
			IL_27C:
			spr_u192F = this.ᜁ(spr_u192F);
			A_1.Add(key, spr_u192F.ᜠ());
			return;
		}
		}
	}

	// Token: 0x06002D1D RID: 11549 RVA: 0x001970A8 File Offset: 0x001960A8
	private new Dictionary<int, int> ᜁ(IList<spr\u192F> A_0)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			int num = 4;
			Dictionary<int, object> dictionary;
			for (;;)
			{
				int num2;
				int count;
				switch (num)
				{
				case 0:
					goto IL_7E;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7E;
					default:
						goto IL_EC;
					}
					break;
				case 2:
				{
					if (num2 >= count)
					{
						num = 1;
						continue;
					}
					spr\u192F spr_u192F = A_0[num2];
					dictionary[spr_u192F.\u173B()] = -1;
					num2++;
					num = 0;
					continue;
				}
				case 3:
					goto IL_55;
				case 5:
					goto IL_AE;
				}
				if (A_0 == null)
				{
					num = 3;
					continue;
				}
				dictionary = new Dictionary<int, object>();
				num2 = 0;
				count = A_0.Count;
				num = 5;
				continue;
				IL_AE:
				num = 2;
				continue;
				IL_7E:
				goto IL_AE;
			}
			IL_55:
			throw new ArgumentNullException(RecordTableEnumerator.b("堸䤺似朾݀ⱂ㝄⩆⡈㽊㹌", a_));
			IL_EC:
			if (false)
			{
			}
			spr\u192F spr_u192F2 = A_0[0];
			XlsWorkbook xlsWorkbook = spr_u192F2.ᜎ();
			XlsWorkbook xlsWorkbook2 = (XlsWorkbook)base.FindParent(typeof(XlsWorkbook));
			XlsFontsCollection innerFonts = xlsWorkbook2.InnerFonts;
			return innerFonts.AddRange(dictionary.Keys, xlsWorkbook.InnerFonts);
		}
		}
	}

	// Token: 0x06002D1E RID: 11550 RVA: 0x001971F0 File Offset: 0x001961F0
	protected virtual void ᜁ()
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
		this.ᜁ.Clear();
	}

	// Token: 0x06002D1F RID: 11551 RVA: 0x00197238 File Offset: 0x00196238
	private new Dictionary<int, int> ᜀ(IList<spr\u192F> A_0)
	{
		int a_ = 10;
		switch (0)
		{
		default:
		{
			int num = 0;
			Dictionary<int, int> dictionary;
			for (;;)
			{
				int count2;
				switch (num)
				{
				case 1:
					if (true)
					{
					}
					goto IL_A5;
				case 2:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 3;
						continue;
					}
					spr\u192F spr_u192F = A_0[num2];
					dictionary[spr_u192F.ᝊ()] = -1;
					num2++;
					num = 1;
					continue;
				}
				case 3:
					goto IL_C1;
				case 4:
					goto IL_A5;
				case 5:
					goto IL_5F;
				case 6:
					goto IL_EB;
				case 7:
				{
					if (count2 == 0)
					{
						num = 6;
						continue;
					}
					int num2 = 0;
					int count = A_0.Count;
					num = 4;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 5;
					continue;
				}
				count2 = A_0.Count;
				dictionary = new Dictionary<int, int>();
				num = 7;
				continue;
				IL_A5:
				num = 2;
			}
			IL_5F:
			throw new ArgumentNullException(RecordTableEnumerator.b("ℿぁ㙃ṅ็╉㹋⍍ㅏ♑❓", a_));
			IL_C1:
			spr\u192F spr_u192F2 = A_0[0];
			XlsWorkbook xlsWorkbook = spr_u192F2.ᜎ();
			XlsWorkbook xlsWorkbook2 = base.FindParent(typeof(XlsWorkbook)) as XlsWorkbook;
			spr\u21FF spr_u21FF = xlsWorkbook2.InnerFormats;
			return spr_u21FF.ᜀ(dictionary, xlsWorkbook.InnerFormats);
			IL_EB:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_C1;
			default:
				if (false)
				{
				}
				return dictionary;
			}
			break;
		}
		}
	}

	// Token: 0x040014C9 RID: 5321
	private new const int ᜀ = 21;

	// Token: 0x040014CA RID: 5322
	private new Dictionary<spr\u192F, spr\u192F> ᜁ = new Dictionary<spr\u192F, spr\u192F>();
}
