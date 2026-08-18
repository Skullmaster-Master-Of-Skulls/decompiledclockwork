using System;
using Spire.CompoundFile.Doc;
using Spire.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Documents;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;

// Token: 0x02000371 RID: 881
internal class sprἹ
{
	// Token: 0x0600315E RID: 12638 RVA: 0x002DAA50 File Offset: 0x002D9A50
	public static void ᜁ(ListFormat A_0, spr\u1F8B A_1)
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
		int a_ = (int)A_1.ᜬ().\u1717();
		int a_2 = (int)A_1.ᜬ().ᝈ();
		sprἹ.ᜀ(a_, a_2, A_0, A_1);
	}

	// Token: 0x0600315F RID: 12639 RVA: 0x002DAAAC File Offset: 0x002D9AAC
	public static void ᜀ(int A_0, int A_1, ListFormat A_2, spr\u1F8B A_3)
	{
		switch (0)
		{
		default:
		{
			int num = 15;
			for (;;)
			{
				spr\u201A spr_u201A;
				int num2;
				sprហ a_;
				switch (num)
				{
				case 0:
				{
					sprℼ sprℼ;
					A_2.LFOStyleName = sprἹ.ᜀ(A_0, A_3, sprℼ, A_2);
					num = 8;
					continue;
				}
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 11;
						continue;
					}
					break;
				case 2:
				{
					if (spr_u201A.ᜄ().Count < A_0)
					{
						num = 9;
						continue;
					}
					num2 = spr_u201A.ᜄ().ᜀ(A_0 - 1).ᜁ();
					a_ = spr_u201A.ᜂ().ᜀ(num2);
					sprℼ sprℼ = spr_u201A.ᜄ().ᜀ(A_0 - 1);
					num = 14;
					continue;
				}
				case 3:
					num = 2;
					continue;
				case 4:
					if (A_0 > 0)
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					return;
				case 5:
					num = 10;
					continue;
				case 6:
					if (A_3.ᜬ().ᜫ())
					{
						num = 13;
						continue;
					}
					return;
				case 7:
					goto IL_18A;
				case 8:
					goto IL_C3;
				case 9:
					return;
				case 10:
					if (spr_u201A.ᜄ().Count > 0)
					{
						num = 3;
						continue;
					}
					return;
				case 11:
					if (spr_u201A != null)
					{
						num = 5;
						continue;
					}
					return;
				case 12:
					A_2.IsEmptyList = true;
					num = 16;
					continue;
				case 13:
					sprἹ.ᜀ(A_2, A_3);
					num = 7;
					continue;
				case 14:
				{
					sprℼ sprℼ;
					if (sprℼ.ᜀ().Count > 0)
					{
						num = 0;
						continue;
					}
					goto IL_C3;
				}
				case 16:
					goto IL_90;
				}
				if (A_0 == 0)
				{
					num = 12;
					continue;
				}
				IL_90:
				spr_u201A = A_3.\u173F();
				num = 4;
				continue;
				IL_C3:
				sprἹ.ᜀ(A_2, A_3, num2, a_, A_1);
				num = 6;
			}
			IL_18A:
			return;
		}
		}
	}

	// Token: 0x06003160 RID: 12640 RVA: 0x002DACDC File Offset: 0x002D9CDC
	public static void ᜀ(ListStyle A_0, sprហ A_1, spr\u2305 A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = 0;
				int count = A_0.Levels.Count;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_4A;
					case 1:
						return;
					case 2:
						if (num >= count)
						{
							num2 = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
						{
							if (false)
							{
							}
							if (true)
							{
							}
							ListLevel a_ = A_0.Levels[num];
							spr\u225B spr_u225B = new spr\u225B();
							sprἹ.ᜀ(a_, spr_u225B, A_2, num);
							A_1.ᜅ().Add(spr_u225B);
							num++;
							num2 = 3;
							continue;
						}
						}
						break;
					case 3:
						goto IL_4A;
					}
					break;
					IL_4A:
					num2 = 2;
				}
			}
			return;
		}
	}

	// Token: 0x06003161 RID: 12641 RVA: 0x002DADAC File Offset: 0x002D9DAC
	private static void ᜀ(ListFormat A_0, spr\u1F8B A_1, int A_2, sprហ A_3, int A_4)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				A_4 = 0;
				num = 5;
				continue;
			case 2:
			{
				sprἹ.ᜀ(A_2, A_0);
				string styleName = sprἹ.ᜀ(A_0, A_1, A_2, A_3);
				A_0.ListLevelNumber = A_4;
				A_0.ApplyStyle(styleName);
				num = 3;
				continue;
			}
			case 3:
				return;
			case 4:
				IL_9C:
				if (A_4 < A_3.ᜅ().Count)
				{
					num = 2;
					continue;
				}
				return;
			case 5:
				goto IL_6B;
			}
			if (A_4 > A_3.ᜅ().Count)
			{
				num = 0;
				continue;
			}
			IL_6B:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_9C;
			default:
				if (false)
				{
				}
				num = 4;
				break;
			}
		}
	}

	// Token: 0x06003162 RID: 12642 RVA: 0x002DAE8C File Offset: 0x002D9E8C
	private static void ᜀ(ListFormat A_0, spr\u1F8B A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = (int)A_1.ᜬ().\u1738();
				int num2 = 3;
				for (;;)
				{
					int num3;
					int num4;
					sprហ a_;
					switch (num2)
					{
					case 0:
						if (num3 != 255)
						{
							num2 = 7;
							continue;
						}
						return;
					case 1:
						num2 = 10;
						continue;
					case 2:
					{
						sprℼ sprℼ;
						A_0.NewLfoStyleName = sprἹ.ᜀ(num, A_1, sprℼ, A_0);
						goto IL_C7;
					}
					case 3:
						if (num != 32767)
						{
							num2 = 1;
							continue;
						}
						goto IL_6B;
					case 4:
						goto IL_6B;
					case 5:
					{
						sprℼ sprℼ;
						if (sprℼ.ᜀ().Count > 0)
						{
							num2 = 2;
							continue;
						}
						goto IL_185;
					}
					case 6:
					{
						spr\u201A spr_u201A = A_1.\u173F();
						num4 = spr_u201A.ᜄ().ᜀ(num - 1).ᜁ();
						a_ = spr_u201A.ᜂ().ᜀ(num4);
						sprℼ sprℼ = spr_u201A.ᜄ().ᜀ(num - 1);
						num2 = 5;
						continue;
					}
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_C7;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							A_0.NewListLevelNumber = num3;
							num2 = 8;
							continue;
						}
						break;
					case 8:
						return;
					case 9:
						goto IL_185;
					case 10:
						if (num > 0)
						{
							num2 = 6;
							continue;
						}
						goto IL_6B;
					}
					break;
					IL_6B:
					num3 = (int)A_1.ᜬ().\u1759();
					num2 = 0;
					continue;
					IL_C7:
					num2 = 9;
					continue;
					IL_185:
					A_0.NewStyleName = sprἹ.ᜀ(A_0, A_1, num4, a_);
					num2 = 4;
				}
			}
			return;
		}
	}

	// Token: 0x06003163 RID: 12643 RVA: 0x002DB040 File Offset: 0x002DA040
	private static string ᜀ(ListFormat A_0, spr\u1F8B A_1, int A_2, sprហ A_3)
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			int num = 7;
			ListStyle listStyle;
			for (;;)
			{
				ListType listType;
				switch (num)
				{
				case 0:
					listStyle.IsSimple = true;
					num = 5;
					continue;
				case 1:
					goto IL_D4;
				case 2:
					num = 1;
					continue;
				case 3:
				{
					if (true)
					{
					}
					bool flag = sprἹ.ᜀ(A_3.ᜅ());
					num = 6;
					continue;
				}
				case 4:
					if (A_3.ᜂ())
					{
						num = 0;
						continue;
					}
					goto IL_1E6;
				case 5:
					goto IL_1E4;
				case 6:
				{
					bool flag;
					if (!flag)
					{
						num = 2;
						continue;
					}
					num = 8;
					continue;
				}
				case 8:
					listType = ListType.Bulleted;
					goto IL_E6;
				case 9:
				{
					bool flag;
					listStyle.Name = (flag ? (ClipboardData.b("㝴ɶᕸ᝺᡼୾\uda84", a_) + Guid.NewGuid().ToString()) : (ClipboardData.b("㭴ɶᑸ᥺᡼ൾ\uda84", a_) + Guid.NewGuid().ToString()));
					A_0.IsRestartNumbering = true;
					sprᣄ.ᜀ().ᜂ().Add(A_2, listStyle.Name);
					A_0.Document.ListStyles.Add(listStyle);
					listStyle.IsHybrid = A_3.ᜀ();
					num = 4;
					continue;
				}
				}
				if (sprᣄ.ᜀ().ᜂ().ContainsKey(A_2))
				{
					goto IL_1ED;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_D4;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				IL_E6:
				ListType listType2 = listType;
				listStyle = ListStyle.CreateEmptyListStyle(A_0.Document, listType2, A_3.ᜂ());
				sprἹ.ᜀ(A_3.ᜅ(), listStyle.Levels, A_1);
				num = 9;
				continue;
				IL_D4:
				listType = ListType.Numbered;
				goto IL_E6;
			}
			IL_1E4:
			IL_1E6:
			return listStyle.Name;
			IL_1ED:
			return sprᣄ.ᜀ().ᜂ()[A_2];
		}
		}
	}

	// Token: 0x06003164 RID: 12644 RVA: 0x002DB24C File Offset: 0x002DA24C
	private static void ᜀ(spr\u19DC A_0, ListLevelCollection A_1, spr\u1F8B A_2)
	{
		for (;;)
		{
			int num = 0;
			int count = A_0.Count;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (true)
					{
					}
					goto IL_33;
				case 1:
					if (num >= count)
					{
						num2 = 3;
						continue;
					}
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
						spr\u225B a_ = A_0.ᜀ(num);
						sprἹ.ᜀ(a_, A_1[num], A_2, num);
						num++;
						num2 = 2;
						continue;
					}
					}
					break;
				case 2:
					goto IL_33;
				case 3:
					return;
				}
				break;
				IL_33:
				num2 = 1;
			}
		}
	}

	// Token: 0x06003165 RID: 12645 RVA: 0x002DB2F0 File Offset: 0x002DA2F0
	private static void ᜀ(spr\u225B A_0, ListLevel A_1, spr\u1F8B A_2, int A_3)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				A_1.PatternType = A_0.ᜁ;
				A_1.UsePrevLevelPattern = A_0.ᜅ;
				A_1.StartAt = A_0.ᜀ;
				A_1.NumberAlignment = A_0.ᜂ;
				A_1.FollowCharacter = A_0.ᜊ;
				A_1.IsLegalStyleNumbering = A_0.ᜃ;
				A_1.NoRestartByHigher = A_0.ᜄ;
				A_1.Word6Legacy = A_0.ᜇ;
				A_1.LegacySpace = A_0.ᜋ;
				A_1.LegacyIndent = A_0.ᜌ;
				int num = 6;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_20F;
						default:
						{
							if (false)
							{
							}
							string[] array;
							A_1.NumberPrefix = array[0];
							A_1.NumberSufix = array[1];
							num = 5;
							continue;
						}
						}
						break;
					case 1:
					{
						string[] array;
						if (array.Length > 1)
						{
							num = 0;
							continue;
						}
						A_1.NoPlaceholder = true;
						num = 4;
						continue;
					}
					case 2:
						goto IL_101;
					case 3:
						A_1.BulletCharacter = A_0.ᜐ;
						num = 9;
						continue;
					case 4:
					{
						string[] array;
						if (array[0] == string.Empty)
						{
							num = 8;
							continue;
						}
						if (true)
						{
						}
						A_1.NumberPrefix = array[0];
						A_1.NoLevelText = true;
						num = 7;
						continue;
					}
					case 5:
						goto IL_14A;
					case 6:
					{
						if (A_1.PatternType == ListPatternType.Bullet)
						{
							num = 3;
							continue;
						}
						char[] separator = new char[]
						{
							'\\',
							Convert.ToChar(A_3)
						};
						string[] array = A_0.ᜐ.Split(separator);
						num = 1;
						continue;
					}
					case 7:
						goto IL_190;
					case 8:
						A_1.NumberPrefix = (A_1.NumberSufix = null);
						A_1.NoLevelText = true;
						num = 2;
						continue;
					case 9:
						goto IL_167;
					}
					break;
				}
			}
			IL_101:
			IL_14A:
			IL_167:
			IL_190:
			IL_20F:
			A_0.ᜎ.ᜀ(A_2.ᜥ());
			spr\u1AFF.ᜀ(A_0.ᜎ, A_1.CharacterFormat);
			spr\u192A.ᜀ(A_0.ᜏ, A_1.ParagraphFormat);
			return;
		}
	}

	// Token: 0x06003166 RID: 12646 RVA: 0x002DB540 File Offset: 0x002DA540
	internal static void ᜀ(ListLevel A_0, spr\u225B A_1, spr\u2305 A_2, int A_3)
	{
		for (;;)
		{
			A_1.ᜁ = A_0.PatternType;
			A_1.ᜅ = A_0.UsePrevLevelPattern;
			A_1.ᜀ = A_0.StartAt;
			A_1.ᜂ = A_0.NumberAlignment;
			A_1.ᜊ = A_0.FollowCharacter;
			A_1.ᜃ = A_0.IsLegalStyleNumbering;
			A_1.ᜄ = A_0.NoRestartByHigher;
			A_1.ᜇ = A_0.Word6Legacy;
			A_1.ᜋ = A_0.LegacySpace;
			A_1.ᜌ = A_0.LegacyIndent;
			int num = 20;
			for (;;)
			{
				char c;
				switch (num)
				{
				case 0:
					goto IL_2A4;
				case 1:
					goto IL_2CA;
				case 2:
					if (A_1.ᜎ == null)
					{
						num = 3;
						continue;
					}
					goto IL_F9;
				case 3:
					A_1.ᜎ = new sprℵ(A_2);
					num = 16;
					continue;
				case 4:
					if (!A_0.NoLevelText)
					{
						num = 6;
						continue;
					}
					goto IL_212;
				case 5:
					A_1.ᜏ = new sprᨽ();
					num = 11;
					continue;
				case 6:
					A_1.ᜐ = A_1.ᜐ + c.ToString() + A_0.NumberSufix;
					num = 17;
					continue;
				case 7:
					if (A_0.NumberSufix == null)
					{
						num = 18;
						continue;
					}
					goto IL_230;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2CA;
					default:
						if (false)
						{
						}
						num = 7;
						continue;
					}
					break;
				case 9:
					if (A_0.CharacterFormat.HasKey(2))
					{
						num = 19;
						continue;
					}
					goto IL_17C;
				case 10:
					goto IL_212;
				case 11:
					goto IL_20D;
				case 12:
					if (A_1.ᜏ == null)
					{
						num = 5;
						continue;
					}
					goto IL_325;
				case 13:
					goto IL_17C;
				case 14:
					if (true)
					{
					}
					goto IL_2A4;
				case 15:
					if (A_0.NumberPrefix == null)
					{
						num = 8;
						continue;
					}
					goto IL_230;
				case 16:
					goto IL_F9;
				case 17:
					goto IL_212;
				case 18:
					A_1.ᜐ = string.Empty;
					num = 10;
					continue;
				case 19:
				{
					ushort a_ = (ushort)A_1.ᜎ.ᜄ().ᜀ(A_0.CharacterFormat.FontName);
					A_1.ᜎ.ᜃ(a_);
					A_1.ᜎ.ᜄ(a_);
					num = 13;
					continue;
				}
				case 20:
					if (A_0.PatternType != ListPatternType.Bullet)
					{
						num = 1;
						continue;
					}
					A_1.ᜐ = A_0.BulletCharacter;
					num = 14;
					continue;
				}
				break;
				IL_F9:
				spr\u1AFF.ᜀ(A_0.CharacterFormat, A_1.ᜎ);
				num = 9;
				continue;
				IL_17C:
				num = 12;
				continue;
				IL_212:
				sprἹ.ᜀ(A_1.ᜐ, ref A_1.ᜉ);
				num = 0;
				continue;
				IL_230:
				A_1.ᜐ = A_0.NumberPrefix;
				num = 4;
				continue;
				IL_2A4:
				num = 2;
				continue;
				IL_2CA:
				c = Convert.ToChar(A_3);
				num = 15;
			}
		}
		IL_20D:
		IL_325:
		spr\u192A.ᜀ(A_1.ᜏ, A_0.ParagraphFormat, null);
	}

	// Token: 0x06003167 RID: 12647 RVA: 0x002DB884 File Offset: 0x002DA884
	private static void ᜀ(string A_0, ref byte[] A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				int num3;
				switch (num)
				{
				case 0:
					return;
				case 1:
				{
					string[] array;
					if (array.Length > 1)
					{
						goto IL_C9;
					}
					goto IL_7B;
				}
				case 2:
					return;
				case 3:
					goto IL_D8;
				case 5:
				{
					string[] array;
					int num2;
					A_1[num2] = (byte)(array[0].Length + 1);
					num2++;
					num = 7;
					continue;
				}
				case 6:
				{
					if (num3 >= 9)
					{
						num = 0;
						continue;
					}
					char[] separator = new char[]
					{
						'\\',
						Convert.ToChar(num3)
					};
					string[] array = A_0.Split(separator);
					num = 1;
					continue;
				}
				case 7:
					goto IL_7B;
				case 8:
					goto IL_D8;
				}
				if (!(A_0 == string.Empty))
				{
					A_1[0] = 1;
					int num2 = 0;
					num3 = 0;
					if (true)
					{
					}
					num = 8;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_C9;
				default:
					if (false)
					{
					}
					num = 2;
					continue;
				}
				IL_7B:
				num3++;
				num = 3;
				continue;
				IL_C9:
				num = 5;
				continue;
				IL_D8:
				num = 6;
			}
			return;
		}
		}
	}

	// Token: 0x06003168 RID: 12648 RVA: 0x002DB9CC File Offset: 0x002DA9CC
	private static bool ᜀ(spr\u19DC A_0)
	{
		bool result;
		for (;;)
		{
			result = true;
			int num = 0;
			int count = A_0.Count;
			int num2 = 5;
			for (;;)
			{
				if (true)
				{
				}
				switch (num2)
				{
				case 0:
					result = false;
					num2 = 2;
					continue;
				case 1:
					if (A_0.ᜀ(num).ᜁ != ListPatternType.Bullet)
					{
						num2 = 0;
						continue;
					}
					num++;
					num2 = 6;
					continue;
				case 2:
					goto IL_63;
				case 3:
					goto IL_A4;
				case 4:
					if (num >= count)
					{
						num2 = 3;
						continue;
					}
					num2 = 1;
					continue;
				case 5:
					goto IL_8A;
				case 6:
					goto IL_8A;
				}
				break;
				IL_8A:
				num2 = 4;
			}
		}
		IL_63:
		return result;
		IL_A4:
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
		return result;
	}

	// Token: 0x06003169 RID: 12649 RVA: 0x002DBA9C File Offset: 0x002DAA9C
	private static void ᜀ(int A_0, ListFormat A_1)
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				ListStyle listStyle;
				if (listStyle == null)
				{
					num = 4;
					continue;
				}
				goto IL_AB;
			}
			case 1:
			{
				if (true)
				{
				}
				string name = sprᣄ.ᜀ().ᜂ()[A_0];
				ListStyle listStyle = A_1.Document.ListStyles.FindByName(name);
				num = 0;
				continue;
			}
			case 2:
				goto IL_AB;
			case 4:
				sprᣄ.ᜀ().ᜂ().Remove(A_0);
				num = 2;
				continue;
			}
			if (sprᣄ.ᜀ().ᜂ().ContainsKey(A_0))
			{
				num = 1;
				continue;
			}
			IL_AB:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_C1;
			}
		}
		IL_C1:
		if (false)
		{
		}
	}

	// Token: 0x0600316A RID: 12650 RVA: 0x002DBB70 File Offset: 0x002DAB70
	private static bool ᜀ(spr\u180A A_0)
	{
		bool result;
		for (;;)
		{
			result = false;
			spr\u1CC1 spr_u1CC = A_0.ᜇ().ᜪ().ᜇ(17931);
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return result;
			default:
			{
				if (false)
				{
				}
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return result;
					case 1:
						if (spr_u1CC == null)
						{
							num = 2;
							continue;
						}
						return result;
					case 2:
						result = true;
						num = 0;
						continue;
					}
					break;
				}
				break;
			}
			}
		}
		return result;
	}

	// Token: 0x0600316B RID: 12651 RVA: 0x002DBBFC File Offset: 0x002DABFC
	private static string ᜀ(int A_0, spr\u1F8B A_1, sprℼ A_2, ListFormat A_3)
	{
		int a_ = 13;
		if (!sprᣄ.ᜀ().ᜁ().ContainsKey(A_0 - 1))
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
					goto IL_40;
				}
			}
			IL_40:
			if (false)
			{
			}
			spr\u177D spr_u177D = new spr\u177D(A_3.Document);
			sprἹ.ᜀ(A_2, spr_u177D, A_1, A_3.Document);
			spr_u177D.Name = ClipboardData.b("㽲፴ᡶ⩸ེѼ፾\udc82", a_) + Guid.NewGuid().ToString();
			A_3.Document.ListOverrides.ᜀ(spr_u177D);
			sprᣄ.ᜀ().ᜁ().Add(A_0 - 1, spr_u177D.Name);
			return spr_u177D.Name;
		}
		return sprᣄ.ᜀ().ᜁ()[A_0 - 1];
	}

	// Token: 0x0600316C RID: 12652 RVA: 0x002DBCE4 File Offset: 0x002DACE4
	internal static void ᜀ(sprℼ A_0, spr\u177D A_1, spr\u1F8B A_2, IDocument A_3)
	{
		for (;;)
		{
			A_1.ᜁ = A_0.ᜃ;
			A_1.ᜂ = A_0.ᜄ;
			int num = 0;
			int num2 = 3;
			for (;;)
			{
				sprḁ sprḁ;
				OverrideLevelFormat overrideLevelFormat;
				switch (num2)
				{
				case 0:
					goto IL_137;
				case 1:
					num2 = 8;
					continue;
				case 2:
					goto IL_142;
				case 3:
					goto IL_137;
				case 4:
					if (sprḁ.ᜇ != null)
					{
						num2 = 1;
						continue;
					}
					goto IL_53;
				case 5:
					goto IL_53;
				case 6:
					return;
				case 7:
					sprἹ.ᜀ(sprḁ.ᜇ, overrideLevelFormat.OverrideListLevel, A_2, sprḁ.ᜁ);
					num2 = 5;
					continue;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_142;
					default:
						if (false)
						{
						}
						if (sprḁ.ᜃ)
						{
							num2 = 7;
							continue;
						}
						goto IL_53;
					}
					break;
				}
				break;
				IL_53:
				A_1.ᜃ().ᜀ(sprḁ.ᜁ, overrideLevelFormat);
				num++;
				num2 = 0;
				continue;
				IL_142:
				if (num >= A_0.ᜀ().Count)
				{
					num2 = 6;
					continue;
				}
				sprḁ = (A_0.ᜀ()[num] as sprḁ);
				overrideLevelFormat = new OverrideLevelFormat((Document)A_3);
				overrideLevelFormat.OverrideFormatting = sprḁ.ᜃ;
				overrideLevelFormat.OverrideStartAtValue = sprḁ.ᜂ;
				overrideLevelFormat.StartAt = sprḁ.ᜀ;
				overrideLevelFormat.ᜄ = sprḁ.ᜄ;
				overrideLevelFormat.ᜅ = sprḁ.ᜅ;
				overrideLevelFormat.ᜆ = sprḁ.ᜆ;
				if (true)
				{
				}
				num2 = 4;
				continue;
				IL_137:
				num2 = 2;
			}
		}
	}

	// Token: 0x0600316D RID: 12653 RVA: 0x002DBE94 File Offset: 0x002DAE94
	internal static void ᜀ(spr\u177D A_0, sprℼ A_1, spr\u2305 A_2)
	{
		switch (0)
		{
		default:
			if (true)
			{
			}
			for (;;)
			{
				A_1.ᜃ = A_0.ᜁ;
				A_1.ᜄ = A_0.ᜂ;
				int i = 0;
				int count = A_0.ᜃ().Count;
				int num = 3;
				for (;;)
				{
					IL_18:
					sprḁ sprḁ;
					switch (num)
					{
					case 0:
					{
						while (i >= count)
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
								num = 1;
								goto IL_18;
							}
						}
						OverrideLevelFormat overrideLevelFormat = A_0.ᜃ().ᜀ(i);
						sprḁ = new sprḁ(overrideLevelFormat.OverrideFormatting);
						sprḁ.ᜃ = overrideLevelFormat.OverrideFormatting;
						sprḁ.ᜂ = overrideLevelFormat.OverrideStartAtValue;
						sprḁ.ᜀ = overrideLevelFormat.StartAt;
						sprḁ.ᜄ = overrideLevelFormat.ᜄ;
						sprḁ.ᜅ = overrideLevelFormat.ᜅ;
						sprḁ.ᜆ = overrideLevelFormat.ᜆ;
						num = 2;
						continue;
					}
					case 1:
						return;
					case 2:
					{
						OverrideLevelFormat overrideLevelFormat;
						if (overrideLevelFormat.OverrideListLevel != null)
						{
							num = 7;
							continue;
						}
						goto IL_77;
					}
					case 3:
						goto IL_146;
					case 4:
					{
						OverrideLevelFormat overrideLevelFormat;
						if (overrideLevelFormat.OverrideFormatting)
						{
							num = 8;
							continue;
						}
						goto IL_77;
					}
					case 5:
						goto IL_146;
					case 6:
						goto IL_77;
					case 7:
						num = 4;
						continue;
					case 8:
					{
						OverrideLevelFormat overrideLevelFormat;
						sprἹ.ᜀ(overrideLevelFormat.OverrideListLevel, sprḁ.ᜇ, A_2, i);
						num = 6;
						continue;
					}
					}
					break;
					IL_77:
					A_1.ᜀ().Add(sprḁ);
					i++;
					num = 5;
					continue;
					IL_146:
					num = 0;
				}
			}
			return;
		}
	}
}
