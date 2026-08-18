using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.PivotTables;
using Spire.Xls.Core.Spreadsheet.XmlReaders.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlSerialization;

// Token: 0x0200023F RID: 575
internal class sprℳ
{
	// Token: 0x060022F1 RID: 8945 RVA: 0x00141A58 File Offset: 0x00140A58
	public static void ᜀ(XmlReader A_0, XlsPivotCache A_1, IWorkbook A_2, string A_3, RelationsCollection A_4)
	{
		int a_ = 0;
		int num = 12;
		XlsWorkbook xlsWorkbook;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_83;
			case 1:
				goto IL_31F;
			case 2:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䐵崷尹主嬽㌿⩁⅃≅ੇ㍉", a_)))
				{
					num = 6;
					continue;
				}
				goto IL_180;
			case 3:
			{
				double d = XmlConvert.ToDouble(A_0.Value);
				A_1.RefreshDate = DateTime.FromOADate(d);
				num = 1;
				continue;
			}
			case 4:
				IL_103:
				if (A_2 == null)
				{
					num = 11;
					continue;
				}
				num = 16;
				continue;
			case 5:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䐵崷尹主嬽㌿⩁⅃≅ే⭉㡋⭍", a_)))
				{
					num = 15;
					continue;
				}
				goto IL_212;
			case 6:
				A_1.RefreshedBy = A_0.Value;
				num = 17;
				continue;
			case 7:
				goto IL_88;
			case 8:
				goto IL_1F9;
			case 9:
				goto IL_212;
			case 10:
				new XmlException(RecordTableEnumerator.b("挵嘷弹䐻丽┿⅁ぃ⍅ⱇ橉㑋⍍㱏牑⁓㝕㽗瑙", a_));
				num = 22;
				continue;
			case 11:
				goto IL_114;
			case 13:
			{
				string value = A_0.Value;
				num = 7;
				continue;
			}
			case 14:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("張尷", a_), RecordTableEnumerator.b("帵䰷丹䰻н漿流㝃╅⁇⽉⅋⽍⍏籑㭓♕㵗㑙⑛㍝౟ѡୣᑥէ୩ᡫᵭ幯ᵱٳᅵ坷ᕹ᩻᡽슅曆ﲑ릕ꪗꪙ겛ꢝ辟킡솣쪥즧\udea9얫솭\udeaf솱\udcb3\udfb5좷즹", a_)))
				{
					num = 13;
					continue;
				}
				goto IL_88;
			case 15:
			{
				double a_2 = XmlConvert.ToDouble(A_0.Value);
				A_1.RefreshDate = UtilityMethods.ᜀ(a_2);
				num = 9;
				continue;
			}
			case 16:
				if (A_0.LocalName != RecordTableEnumerator.b("䘵儷䰹医䨽̿⍁❃⹅ⵇ้⥋⡍㥏㱑㵓≕ㅗ㕙㉛", a_))
				{
					num = 10;
					continue;
				}
				goto IL_1B4;
			case 17:
				goto IL_180;
			case 18:
				goto IL_153;
			case 19:
				if (xlsWorkbook.Options == ExcelParseOptions.DoNotParsePivotTable)
				{
					num = 8;
					continue;
				}
				num = 14;
				continue;
			case 20:
				if (A_1 == null)
				{
					num = 18;
					continue;
				}
				num = 4;
				continue;
			case 21:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䐵崷尹主嬽㌿⩁⅃≅ే⭉㡋⭍", a_)))
				{
					num = 3;
					continue;
				}
				goto IL_3D9;
			case 22:
				goto IL_1B4;
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			num = 20;
			continue;
			IL_88:
			num = 5;
			continue;
			IL_1B4:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_103;
			default:
				if (false)
				{
				}
				xlsWorkbook = (A_2 as XlsWorkbook);
				num = 19;
				continue;
			}
			IL_180:
			num = 21;
			continue;
			IL_212:
			A_1.IsBackgroundQuery = sprℳ.ᜁ(A_0, RecordTableEnumerator.b("吵夷夹圻夽㈿ⵁㅃ⡅ⱇᭉ㥋⭍≏⭑", a_));
			A_1.CreatedVersion = sprℳ.ᜂ(A_0, RecordTableEnumerator.b("唵䨷弹崻䨽┿♁ቃ⍅㩇㥉╋⅍㹏", a_));
			A_1.EnableRefresh = sprℳ.ᜁ(A_0, RecordTableEnumerator.b("匵嘷嬹帻刽┿၁⅃⁅㩇⽉㽋♍", a_));
			A_1.IsRefreshOnLoad = sprℳ.ᜁ(A_0, RecordTableEnumerator.b("䐵崷尹主嬽㌿⩁ୃ⡅ч╉ⵋ⩍", a_));
			A_1.IsInvalidData = sprℳ.ᜁ(A_0, RecordTableEnumerator.b("張嘷䰹崻刽⤿♁", a_));
			A_1.MinRefreshableVersion = sprℳ.ᜂ(A_0, RecordTableEnumerator.b("嬵儷吹渻嬽☿ぁ⅃㕅⁇⭉⹋≍㕏ёㅓ⑕⭗㍙㍛そ", a_));
			A_1.IsOptimizedCache = sprℳ.ᜁ(A_0, RecordTableEnumerator.b("夵䠷丹唻匽⤿㡁⅃୅ⵇ❉⍋㱍⥏", a_));
			num = 2;
		}
		IL_83:
		throw new ArgumentNullException(RecordTableEnumerator.b("䐵崷嬹堻嬽㈿", a_));
		IL_114:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䄵圷䠹圻尽⼿ⵁ⽃", a_));
		IL_153:
		throw new ArgumentNullException(RecordTableEnumerator.b("䘵儷䰹医䨽怿⅁╃╅⁇⽉", a_));
		IL_1F9:
		xlsWorkbook.PreservesPivotCache.Add(ShapeParser.ReadNodeAsStream(A_0));
		return;
		IL_31F:
		IL_3D9:
		A_1.RefreshedVersion = sprℳ.ᜂ(A_0, RecordTableEnumerator.b("䐵崷尹主嬽㌿⩁⅃≅ṇ⽉㹋㵍㥏㵑㩓", a_));
		A_1.IsRefreshOnLoad = sprℳ.ᜁ(A_0, RecordTableEnumerator.b("䐵崷尹主嬽㌿⩁ୃ⡅ч╉ⵋ⩍", a_));
		A_1.IsSaveData = sprℳ.ᜀ(A_0, RecordTableEnumerator.b("䔵夷䰹夻稽ℿ㙁╃", a_), true);
		A_1.SupportAdvancedDrill = sprℳ.ᜁ(A_0, RecordTableEnumerator.b("䔵䴷䨹䰻儽㈿㙁Ճ≅㹇⭉≋ⵍ㕏㙑ၓ⑕ㅗ㙙せ", a_));
		A_1.IsSupportSubQuery = sprℳ.ᜁ(A_0, RecordTableEnumerator.b("䔵䴷䨹䰻儽㈿㙁ᝃ㍅⩇㭉㥋⭍≏⭑", a_));
		A_1.IsUpgradeOnRefresh = sprℳ.ᜁ(A_0, RecordTableEnumerator.b("䌵䠷崹主弽␿❁ୃ⡅ᩇ⽉⩋㱍㕏⅑㱓", a_));
		A_0.Read();
		sprℳ.ᜂ(A_0, A_1, A_2, A_4);
		sprℳ.ᜆ(A_0, A_1);
		sprℳ.ᜀ(A_0, A_1.CacheFields);
		sprℳ.ᜄ(A_0, A_1);
		sprℳ.ᜃ(A_0, A_1);
		sprℳ.ᜂ(A_0, A_1);
		sprℳ.ᜁ(A_0, A_1);
		sprℳ.ᜀ(A_0, A_1);
	}

	// Token: 0x060022F2 RID: 8946 RVA: 0x00141F24 File Offset: 0x00140F24
	private static XmlReader ᜀ(string A_0, string A_1, XlsWorkbook A_2, RelationsCollection A_3, out string A_4)
	{
		int a_ = 2;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_D6;
			case 1:
				if (A_3 == null)
				{
					num = 3;
					continue;
				}
				goto IL_D8;
			case 2:
				goto IL_6A;
			case 3:
				goto IL_7F;
			case 5:
				if (A_2 == null)
				{
					num = 0;
					continue;
				}
				num = 1;
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
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				break;
			}
			num = 5;
		}
		IL_6A:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹倻弽㐿⭁⭃⡅Ň⹉", a_));
		IL_7F:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹倻弽㐿⭁⭃⡅㭇", a_));
		IL_D6:
		throw new ArgumentNullException(RecordTableEnumerator.b("伷唹主唽∿ⵁ⭃ⵅ", a_));
		IL_D8:
		sprᦨ sprᦨ = A_3[A_0];
		sprᦨ.ᜂ();
		return A_2.DataHolder.ᜂ(sprᦨ, A_1, out A_4);
	}

	// Token: 0x060022F3 RID: 8947 RVA: 0x00142028 File Offset: 0x00141028
	private static void ᜂ(XmlReader A_0, XlsPivotCache A_1, IWorkbook A_2, RelationsCollection A_3)
	{
		int a_ = 2;
		int num = 2;
		for (;;)
		{
			DataSourceType sourceType;
			switch (num)
			{
			case 0:
				goto IL_EB;
			case 1:
				goto IL_13F;
			case 3:
				A_0.Read();
				num = 9;
				continue;
			case 4:
				if (A_2 == null)
				{
					goto IL_E0;
				}
				num = 6;
				continue;
			case 5:
				goto IL_17C;
			case 6:
				if (A_0.LocalName != RecordTableEnumerator.b("嬷嬹弻嘽┿ᅁ⭃㍅㩇⥉⥋", a_))
				{
					num = 7;
					continue;
				}
				goto IL_17C;
			case 7:
				new XmlException(RecordTableEnumerator.b("洷吹夻䘽〿❁❃㉅ⵇ⹉汋㙍㵏㹑瑓≕㥗㵙牛", a_));
				num = 5;
				continue;
			case 8:
				if (A_0.LocalName == RecordTableEnumerator.b("嬷嬹弻嘽┿ᅁ⭃㍅㩇⥉⥋", a_))
				{
					num = 3;
					continue;
				}
				return;
			case 9:
				return;
			case 10:
				switch (sourceType)
				{
				case DataSourceType.Worksheet:
					A_0.Read();
					sprℳ.ᜁ(A_0, A_1, A_2, A_3);
					num = 22;
					continue;
				case DataSourceType.ExternalData:
					sprℳ.ᜀ(A_0, A_1, A_2, A_3);
					num = 21;
					continue;
				case (DataSourceType)3:
				case DataSourceType.Consolidation:
					goto IL_13F;
				default:
					num = 11;
					continue;
				}
				break;
			case 11:
				num = 19;
				continue;
			case 12:
				goto IL_95;
			case 13:
				A_1.SourceType = sprℳ.ᜁ(A_0.Value);
				num = 12;
				continue;
			case 14:
				num = 1;
				continue;
			case 15:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䰷䌹䰻嬽", a_)))
				{
					num = 13;
					continue;
				}
				goto IL_95;
			case 16:
				goto IL_13F;
			case 17:
				goto IL_83;
			case 18:
				if (A_1 == null)
				{
					num = 20;
					continue;
				}
				num = 4;
				continue;
			case 19:
				if (sourceType != DataSourceType.ScenarioPivotTable)
				{
					num = 14;
					continue;
				}
				A_0.Read();
				num = 16;
				continue;
			case 20:
				goto IL_11D;
			case 21:
				goto IL_13F;
			case 22:
				goto IL_13F;
			}
			if (A_0 == null)
			{
				num = 17;
				continue;
			}
			num = 18;
			continue;
			IL_95:
			sourceType = A_1.SourceType;
			num = 10;
			continue;
			IL_E0:
			num = 0;
			continue;
			IL_17C:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_E0;
			default:
				if (false)
				{
				}
				num = 15;
				continue;
			}
			IL_13F:
			A_0.MoveToElement();
			num = 8;
		}
		IL_83:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹崻娽┿ぁ", a_));
		IL_EB:
		throw new ArgumentNullException(RecordTableEnumerator.b("伷唹主唽∿ⵁ⭃ⵅ", a_));
		IL_11D:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䠷匹䨻儽㐿扁❃❅⭇≉⥋", a_));
	}

	// Token: 0x060022F4 RID: 8948 RVA: 0x0014231C File Offset: 0x0014131C
	private static void ᜁ(XmlReader A_0, XlsPivotCache A_1, IWorkbook A_2, RelationsCollection A_3)
	{
		int a_ = 11;
		switch (0)
		{
		default:
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
				int num = 0;
				for (;;)
				{
					bool flag;
					XlsWorkbook xlsWorkbook;
					string a_2;
					string sheetName;
					IXLSRange a_4;
					string text;
					bool flag2;
					string text2;
					switch (num)
					{
					case 1:
						goto IL_47C;
					case 2:
					{
						if (flag)
						{
							num = 5;
							continue;
						}
						FormulaUtil formulaUtil = xlsWorkbook.DataHolder.\u1718().ᜀ();
						Ptg[] array = formulaUtil.ᜃ(a_2);
						sprỜ sprỜ = array[0] as sprỜ;
						IWorksheet a_3 = A_2.Worksheets[sheetName];
						a_4 = sprỜ.ᜀ(A_2, a_3);
						num = 19;
						continue;
					}
					case 3:
						if (true)
						{
						}
						sheetName = A_0.Value;
						num = 15;
						continue;
					case 4:
						goto IL_408;
					case 5:
					{
						INamedRange namedRange = A_2.Names[text];
						num = 18;
						continue;
					}
					case 6:
						goto IL_1E8;
					case 7:
					{
						if (!flag2)
						{
							num = 28;
							continue;
						}
						A_1.RelationId = text2;
						A_1.PreservedExtenalRelation = A_3[text2];
						A_0.MoveToElement();
						Stream value = ShapeParser.ReadNodeAsStream(A_0);
						A_1.PreservedElements.Add(RecordTableEnumerator.b("㙀ⱂ㝄ⱆ㩈⍊⡌⩎═R㩔≖⭘㡚㡜", a_), value);
						num = 6;
						continue;
					}
					case 8:
						new XmlException(RecordTableEnumerator.b("ᑀⵂ⁄㽆㥈⹊⹌㭎㑐㝒畔⽖㑘㝚絜⭞`Ѣ䭤", a_));
						num = 32;
						continue;
					case 9:
						goto IL_108;
					case 10:
						text = A_0.Value;
						num = 9;
						continue;
					case 11:
						goto IL_3A3;
					case 12:
						if (A_0.MoveToAttribute(RecordTableEnumerator.b("⽀≂⡄≆", a_)))
						{
							num = 10;
							continue;
						}
						goto IL_108;
					case 13:
						A_0.Read();
						num = 21;
						continue;
					case 14:
						if (A_0.MoveToAttribute(RecordTableEnumerator.b("㉀⭂⁄≆㵈", a_)))
						{
							num = 3;
							continue;
						}
						goto IL_3BC;
					case 15:
						goto IL_3BC;
					case 16:
						if (A_2 == null)
						{
							num = 27;
							continue;
						}
						num = 30;
						continue;
					case 17:
						if (A_0.MoveToAttribute(RecordTableEnumerator.b("⡀❂", a_), RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂햐ﲒﺚ躠醢閤鞦龨蒪\udfac쪮\uddb0튲솴\udeb6횸햺캼ힾꣀ돂뛄", a_)))
						{
							num = 24;
							continue;
						}
						goto IL_408;
					case 18:
					{
						INamedRange namedRange;
						if (namedRange != null)
						{
							num = 29;
							continue;
						}
						goto IL_3A3;
					}
					case 19:
						goto IL_1E8;
					case 20:
						if (A_0.MoveToAttribute(RecordTableEnumerator.b("㍀♂⍄", a_)))
						{
							num = 23;
							continue;
						}
						goto IL_47C;
					case 21:
						goto IL_2CF;
					case 22:
						goto IL_477;
					case 23:
						a_2 = A_0.Value;
						num = 1;
						continue;
					case 24:
						text2 = A_0.Value;
						num = 4;
						continue;
					case 25:
						if (A_0.NodeType != XmlNodeType.EndElement)
						{
							num = 13;
							continue;
						}
						return;
					case 26:
						goto IL_1E8;
					case 27:
						goto IL_34A;
					case 28:
						num = 2;
						continue;
					case 29:
					{
						INamedRange namedRange;
						a_4 = namedRange.RefersToRange;
						num = 11;
						continue;
					}
					case 30:
						if (A_0.LocalName != RecordTableEnumerator.b("㙀ⱂ㝄ⱆ㩈⍊⡌⩎═R㩔≖⭘㡚㡜", a_))
						{
							num = 8;
							continue;
						}
						goto IL_34F;
					case 31:
						goto IL_E2;
					case 32:
						goto IL_34F;
					case 33:
						if (A_1 == null)
						{
							num = 22;
							continue;
						}
						num = 16;
						continue;
					}
					if (A_0 == null)
					{
						num = 31;
						continue;
					}
					num = 33;
					continue;
					IL_108:
					num = 14;
					continue;
					IL_1E8:
					A_1.SourceRange = a_4;
					num = 25;
					continue;
					IL_34F:
					xlsWorkbook = (A_2 as XlsWorkbook);
					a_4 = null;
					text2 = null;
					a_2 = null;
					text = null;
					sheetName = null;
					num = 17;
					continue;
					IL_3A3:
					A_1.RangeName = text;
					num = 26;
					continue;
					IL_3BC:
					flag2 = (text2 != null);
					flag = (text != null);
					num = 7;
					continue;
					IL_408:
					num = 20;
					continue;
					IL_47C:
					num = 12;
				}
				IL_E2:
				throw new ArgumentNullException(RecordTableEnumerator.b("㍀♂⑄⍆ⱈ㥊", a_));
				IL_2CF:
				return;
				IL_34A:
				break;
				IL_477:
				throw new ArgumentNullException(RecordTableEnumerator.b("ㅀ⩂㍄⡆㵈歊⹌⹎㉐㭒ご", a_));
			}
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㙀ⱂ㝄ⱆ⭈⑊≌⑎", a_));
		}
	}

	// Token: 0x060022F5 RID: 8949 RVA: 0x001427F4 File Offset: 0x001417F4
	private static void ᜀ(XmlReader A_0, XlsPivotCache A_1, IWorkbook A_2, RelationsCollection A_3)
	{
		int a_ = 2;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_48;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_7E;
				default:
				{
					if (false)
					{
					}
					A_0.MoveToElement();
					Stream value = ShapeParser.ReadNodeAsStream(A_0);
					A_1.PreservedElements.Add(RecordTableEnumerator.b("崷䈹䠻嬽㈿ⱁ╃⩅", a_), value);
					num = 3;
					continue;
				}
				}
				break;
			case 2:
				goto IL_71;
			case 3:
				return;
			case 4:
				if (A_2 == null)
				{
					num = 2;
					continue;
				}
				if (true)
				{
				}
				num = 8;
				continue;
			case 6:
				goto IL_86;
			case 7:
				if (A_1 == null)
				{
					goto IL_7E;
				}
				num = 4;
				continue;
			case 8:
				if (A_1.PreservedElements != null)
				{
					num = 1;
					continue;
				}
				return;
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			num = 7;
			continue;
			IL_7E:
			num = 6;
		}
		IL_48:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹崻娽┿ぁ", a_));
		IL_71:
		throw new ArgumentNullException(RecordTableEnumerator.b("伷唹主唽∿ⵁ⭃ⵅ", a_));
		IL_86:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠷匹䨻儽㐿扁❃❅⭇≉⥋", a_));
	}

	// Token: 0x060022F6 RID: 8950 RVA: 0x0014293C File Offset: 0x0014193C
	private static void ᜆ(XmlReader A_0, XlsPivotCache A_1)
	{
		int a_ = 17;
		int num = 14;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_121;
			case 1:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 10;
					continue;
				}
				goto IL_194;
			case 2:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 4;
					continue;
				}
				goto IL_121;
			case 3:
				goto IL_121;
			case 4:
				num = 12;
				continue;
			case 5:
				if (A_1 == null)
				{
					num = 9;
					continue;
				}
				num = 8;
				continue;
			case 6:
				goto IL_1EF;
			case 7:
			{
				XlsPivotCacheField xlsPivotCacheField = new XlsPivotCacheField();
				sprℳ.ᜁ(A_0, xlsPivotCacheField, A_1);
				sprᾷ sprᾷ;
				sprᾷ.ᜀ(xlsPivotCacheField);
				num = 13;
				continue;
			}
			case 8:
			{
				if (A_0.LocalName != RecordTableEnumerator.b("⑆⡈⡊╌⩎ᝐ㩒ご㭖㵘⡚", a_))
				{
					num = 6;
					continue;
				}
				sprᾷ sprᾷ = A_1.CacheFields;
				A_0.Read();
				num = 3;
				continue;
			}
			case 9:
				goto IL_D1;
			case 10:
				goto IL_15D;
			case 11:
				goto IL_60;
			case 12:
				if (A_0.LocalName == RecordTableEnumerator.b("⑆⡈⡊╌⩎ᝐ㩒ご㭖㵘", a_))
				{
					num = 7;
					continue;
				}
				goto IL_D6;
			case 13:
				goto IL_D6;
			}
			if (A_0 == null)
			{
				num = 11;
				continue;
			}
			if (true)
			{
			}
			num = 5;
			continue;
			IL_D6:
			A_0.Read();
			num = 0;
			continue;
			IL_121:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_194:
				num = 2;
				break;
			default:
				if (false)
				{
				}
				num = 1;
				break;
			}
		}
		IL_60:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕆ⱈ⩊⥌⩎⍐", a_));
		IL_D1:
		throw new ArgumentNullException(RecordTableEnumerator.b("㝆⁈㵊≌㭎煐げ㑔㑖ㅘ㹚絜", a_));
		IL_15D:
		A_0.Read();
		return;
		IL_1EF:
		throw new XmlException(RecordTableEnumerator.b("ц⡈⡊╌⩎ᝐ㩒ご㭖㵘⡚", a_));
	}

	// Token: 0x060022F7 RID: 8951 RVA: 0x00142B44 File Offset: 0x00141B44
	private static void ᜁ(XmlReader A_0, XlsPivotCacheField A_1, XlsPivotCache A_2)
	{
		int a_ = 4;
		for (;;)
		{
			IL_09:
			int num = 16;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("刹唻嬽㈿⍁㙃╅⁇㍉", a_)))
					{
						num = 10;
						continue;
					}
					goto IL_142;
				case 1:
					A_1.Level = sprℳ.ᜂ(A_0, RecordTableEnumerator.b("嘹夻䠽┿⹁", a_));
					num = 8;
					continue;
				case 2:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("吹䤻匽ؿ⽁ぃཅⱇ", a_)))
					{
						num = 9;
						continue;
					}
					goto IL_349;
				case 3:
					goto IL_142;
				case 4:
					if (A_0.LocalName != RecordTableEnumerator.b("夹崻崽⠿❁Ƀ⽅ⵇ♉⡋", a_))
					{
						num = 6;
						continue;
					}
					goto IL_285;
				case 5:
					if (A_1 == null)
					{
						num = 11;
						continue;
					}
					num = 4;
					continue;
				case 6:
					new XmlException(RecordTableEnumerator.b("漹刻嬽㠿㉁⅃╅㱇⽉⡋湍⡏㽑㡓癕ⱗ㭙㭛灝", a_));
					num = 13;
					continue;
				case 7:
					goto IL_98;
				case 8:
					goto IL_1AB;
				case 9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					default:
						if (false)
						{
						}
						A_1.NumFormatIndex = (int)XmlConvert.ToInt16(A_0.Value);
						num = 19;
						continue;
					}
					break;
				case 10:
					A_1.Hierarchy = sprℳ.ᜂ(A_0, RecordTableEnumerator.b("刹唻嬽㈿⍁㙃╅⁇㍉", a_));
					num = 3;
					continue;
				case 11:
					goto IL_2CF;
				case 12:
					goto IL_2D4;
				case 13:
					goto IL_285;
				case 14:
					return;
				case 15:
					A_1.Name = A_0.Value;
					num = 12;
					continue;
				case 17:
					A_1.Formula = A_0.Value;
					num = 22;
					continue;
				case 18:
					if (A_0.IsEmptyElement)
					{
						num = 14;
						continue;
					}
					goto IL_3A3;
				case 19:
					goto IL_349;
				case 20:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("嘹夻䠽┿⹁", a_)))
					{
						num = 1;
						continue;
					}
					goto IL_1AB;
				case 21:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("夹崻丽", a_)))
					{
						num = 23;
						continue;
					}
					goto IL_98;
				case 22:
					goto IL_1D8;
				case 23:
					A_1.Caption = A_0.Value;
					num = 7;
					continue;
				case 24:
					goto IL_93;
				case 25:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("尹医䰽ⴿ㝁⡃❅", a_)))
					{
						num = 17;
						continue;
					}
					goto IL_1D8;
				case 26:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("吹崻匽┿", a_)))
					{
						num = 15;
						continue;
					}
					goto IL_2D4;
				}
				if (A_0 == null)
				{
					num = 24;
					continue;
				}
				num = 5;
				continue;
				IL_98:
				num = 0;
				continue;
				IL_142:
				if (true)
				{
				}
				num = 20;
				continue;
				IL_1AB:
				A_0.MoveToElement();
				num = 18;
				continue;
				IL_1D8:
				num = 2;
				continue;
				IL_285:
				num = 26;
				continue;
				IL_2D4:
				A_1.IsDataBaseField = sprℳ.ᜁ(A_0, RecordTableEnumerator.b("帹崻䨽ℿ⁁╃㕅ⵇ౉╋⭍㱏㙑", a_));
				num = 25;
				continue;
				IL_349:
				num = 21;
			}
		}
		IL_93:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠹夻弽␿❁㙃", a_));
		IL_2CF:
		throw new ArgumentNullException(RecordTableEnumerator.b("尹唻嬽ⰿ♁", a_));
		IL_3A3:
		A_0.Read();
		sprℳ.ᜃ(A_0, A_1);
		sprℳ.ᜀ(A_0, A_1, A_2);
	}

	// Token: 0x060022F8 RID: 8952 RVA: 0x00142F0C File Offset: 0x00141F0C
	private static void ᜃ(XmlReader A_0, XlsPivotCacheField A_1)
	{
		int a_ = 19;
		switch (0)
		{
		default:
		{
			int num = 55;
			for (;;)
			{
				string a;
				bool flag;
				PivotDataType pivotDataType;
				bool flag2;
				bool flag4;
				bool flag5;
				string text;
				bool flag6;
				switch (num)
				{
				case 0:
					if (!(a == RecordTableEnumerator.b("⑈", a_)))
					{
						num = 43;
						continue;
					}
					A_1.IsParsed = new bool?(true);
					A_1.ᜁ(null);
					num = 10;
					continue;
				case 1:
					goto IL_66F;
				case 2:
					goto IL_66F;
				case 3:
					num = 56;
					continue;
				case 4:
					goto IL_66F;
				case 5:
					num = 49;
					continue;
				case 6:
					if (flag)
					{
						num = 9;
						continue;
					}
					goto IL_569;
				case 7:
					goto IL_401;
				case 8:
					goto IL_316;
				case 9:
					pivotDataType |= PivotDataType.LongText;
					num = 20;
					continue;
				case 10:
					goto IL_66F;
				case 11:
					goto IL_5E1;
				case 12:
					goto IL_2EC;
				case 13:
					goto IL_311;
				case 14:
					if (A_1 == null)
					{
						num = 11;
						continue;
					}
					num = 53;
					continue;
				case 15:
					goto IL_1B8;
				case 16:
					goto IL_2EC;
				case 17:
					if (flag2)
					{
						num = 48;
						continue;
					}
					goto IL_38E;
				case 18:
				{
					bool flag3;
					if (flag3)
					{
						num = 52;
						continue;
					}
					goto IL_1B8;
				}
				case 19:
					A_0.Read();
					num = 47;
					continue;
				case 20:
					goto IL_569;
				case 21:
					num = 16;
					continue;
				case 22:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㽈", a_)))
					{
						num = 41;
						continue;
					}
					goto IL_59C;
				case 23:
					num = 54;
					continue;
				case 24:
					if (A_0.LocalName != RecordTableEnumerator.b("㩈⍊ⱌ㵎㑐㝒᱔⍖㱘㙚⹜", a_))
					{
						num = 21;
						continue;
					}
					goto IL_768;
				case 25:
					if (flag4)
					{
						num = 28;
						continue;
					}
					goto IL_316;
				case 26:
					goto IL_59C;
				case 27:
					if (flag5)
					{
						num = 40;
						continue;
					}
					goto IL_519;
				case 28:
					pivotDataType |= PivotDataType.Integer;
					num = 8;
					continue;
				case 29:
					if (!A_0.IsEmptyElement)
					{
						num = 19;
						continue;
					}
					goto IL_61A;
				case 30:
					num = 2;
					continue;
				case 31:
					goto IL_519;
				case 32:
					goto IL_28C;
				case 33:
					goto IL_66F;
				case 34:
					goto IL_66F;
				case 35:
					goto IL_38E;
				case 36:
				{
					string localName;
					if ((a = localName) != null)
					{
						num = 37;
						continue;
					}
					goto IL_66F;
				}
				case 37:
					num = 38;
					continue;
				case 38:
				{
					if (!(a == RecordTableEnumerator.b("❈", a_)))
					{
						num = 23;
						continue;
					}
					double num2 = XmlConvert.ToDouble(text);
					A_1.ᜁ(num2);
					num = 34;
					continue;
				}
				case 39:
					goto IL_66F;
				case 40:
					pivotDataType |= PivotDataType.String;
					num = 31;
					continue;
				case 41:
					text = A_0.Value;
					A_1.IsParsed = new bool?(true);
					num = 26;
					continue;
				case 42:
					if (flag6)
					{
						num = 45;
						continue;
					}
					goto IL_28C;
				case 43:
					num = 7;
					continue;
				case 44:
					num = 0;
					continue;
				case 45:
					pivotDataType |= PivotDataType.Number;
					num = 32;
					continue;
				case 46:
					goto IL_13E;
				case 47:
					goto IL_61A;
				case 48:
					pivotDataType |= PivotDataType.Date;
					num = 35;
					continue;
				case 49:
				{
					if (!(a == RecordTableEnumerator.b("ⵈ", a_)))
					{
						num = 3;
						continue;
					}
					DateTime dateTime = XmlConvert.ToDateTime(A_0.Value, XmlDateTimeSerializationMode.Unspecified);
					A_1.ᜁ(dateTime);
					num = 33;
					continue;
				}
				case 50:
				{
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 13;
						continue;
					}
					string localName = A_0.LocalName;
					num = 22;
					continue;
				}
				case 51:
					return;
				case 52:
					if (true)
					{
					}
					pivotDataType |= PivotDataType.Blank;
					num = 15;
					continue;
				case 53:
				{
					if (A_0.LocalName != RecordTableEnumerator.b("㩈⍊ⱌ㵎㑐㝒᱔⍖㱘㙚⹜", a_))
					{
						num = 51;
						continue;
					}
					pivotDataType = (PivotDataType)0;
					bool flag3 = false;
					flag6 = false;
					flag2 = false;
					flag5 = false;
					flag4 = false;
					flag = false;
					flag3 = sprℳ.ᜁ(A_0, RecordTableEnumerator.b("⩈⑊⍌㭎ぐ㩒㭔⑖᭘㝚㱜ㅞ੠", a_));
					num = 18;
					continue;
				}
				case 54:
					if (!(a == RecordTableEnumerator.b("㩈", a_)))
					{
						num = 5;
						continue;
					}
					A_1.ᜁ(text);
					num = 4;
					continue;
				case 55:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_401;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 56:
				{
					if (!(a == RecordTableEnumerator.b("⭈", a_)))
					{
						num = 44;
						continue;
					}
					bool flag7 = XmlConvert.ToBoolean(text);
					A_1.ᜁ(flag7);
					num = 39;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 46;
					continue;
				}
				num = 14;
				continue;
				IL_1B8:
				flag6 = sprℳ.ᜁ(A_0, RecordTableEnumerator.b("⩈⑊⍌㭎ぐ㩒㭔⑖᝘⹚ぜ㵞Ѡᅢ", a_));
				num = 42;
				continue;
				IL_28C:
				flag2 = sprℳ.ᜁ(A_0, RecordTableEnumerator.b("⩈⑊⍌㭎ぐ㩒㭔⑖ᵘ㩚⥜㩞", a_));
				num = 17;
				continue;
				IL_2EC:
				num = 50;
				continue;
				IL_316:
				flag = sprℳ.ᜁ(A_0, RecordTableEnumerator.b("╈⑊⍌⡎Ր㙒ⵔ⍖", a_));
				num = 6;
				continue;
				IL_38E:
				flag5 = sprℳ.ᜁ(A_0, RecordTableEnumerator.b("⩈⑊⍌㭎ぐ㩒㭔⑖੘⽚⽜㙞འѢ", a_));
				num = 27;
				continue;
				IL_401:
				if (!(a == RecordTableEnumerator.b("ⱈ", a_)))
				{
					num = 30;
					continue;
				}
				A_1.ᜁ(text);
				num = 1;
				continue;
				IL_519:
				flag4 = sprℳ.ᜁ(A_0, RecordTableEnumerator.b("⩈⑊⍌㭎ぐ㩒㭔⑖ၘ㕚⥜㩞٠٢ᝤ", a_));
				num = 25;
				continue;
				IL_569:
				A_1.DataType = pivotDataType;
				A_0.MoveToElement();
				num = 29;
				continue;
				IL_59C:
				num = 36;
				continue;
				IL_61A:
				text = null;
				A_1.IsParsed = new bool?(false);
				num = 24;
				continue;
				IL_66F:
				A_0.Read();
				num = 12;
			}
			IL_13E:
			throw new ArgumentNullException(RecordTableEnumerator.b("㭈⹊ⱌ⭎㑐⅒", a_));
			IL_311:
			goto IL_768;
			IL_5E1:
			throw new ArgumentNullException(RecordTableEnumerator.b("⩈⩊⹌❎㑐獒㍔㹖㱘㝚㥜", a_));
			IL_768:
			A_0.Read();
			return;
		}
		}
	}

	// Token: 0x060022F9 RID: 8953 RVA: 0x00143688 File Offset: 0x00142688
	public static void ᜀ(XmlReader A_0, XlsPivotCacheField A_1, XlsPivotCache A_2)
	{
		int a_ = 10;
		switch (0)
		{
		default:
		{
			int num = 3;
			int num3;
			for (;;)
			{
				string[] array;
				int[] array2;
				switch (num)
				{
				case 0:
				{
					int num2;
					if (num2 == -1)
					{
						num = 14;
						continue;
					}
					XlsPivotCacheField a_2;
					A_1.FieldGroup = new spr\u1920(a_2, num3);
					num = 9;
					continue;
				}
				case 1:
					if (array != null)
					{
						num = 13;
						continue;
					}
					goto IL_1DB;
				case 2:
					goto IL_FC;
				case 4:
				{
					int num2 = XmlConvert.ToInt32(A_0.Value);
					num = 24;
					continue;
				}
				case 5:
					num3 = XmlConvert.ToInt32(A_0.Value);
					num = 8;
					continue;
				case 6:
					goto IL_349;
				case 7:
				{
					XlsPivotCacheField a_2 = A_1;
					num = 6;
					continue;
				}
				case 8:
					goto IL_1F9;
				case 9:
					goto IL_FC;
				case 10:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("〿⍁㙃", a_)))
					{
						num = 5;
						continue;
					}
					goto IL_1F9;
				case 11:
					if (A_0.LocalName != RecordTableEnumerator.b("☿⭁⅃⩅ⱇ൉㹋⅍╏≑", a_))
					{
						num = 26;
						continue;
					}
					num = 12;
					continue;
				case 12:
				{
					if (A_2 == null)
					{
						num = 18;
						continue;
					}
					XlsPivotCacheField a_2 = null;
					num3 = -1;
					int num2 = -1;
					num = 10;
					continue;
				}
				case 13:
					A_1.FieldGroup.ᜀ(array2, array);
					num = 19;
					continue;
				case 14:
					goto IL_168;
				case 15:
					goto IL_A7;
				case 16:
					goto IL_1F4;
				case 17:
					if (array2 != null)
					{
						num = 23;
						continue;
					}
					goto IL_1DB;
				case 18:
					goto IL_F7;
				case 19:
					goto IL_187;
				case 20:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("∿⍁㝃⍅", a_)))
					{
						num = 4;
						continue;
					}
					goto IL_349;
				case 21:
				{
					XlsPivotCacheField a_2;
					A_1.FieldGroup = new spr\u1920(a_2);
					num = 2;
					continue;
				}
				case 22:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						if (num3 == -1)
						{
							num = 21;
							continue;
						}
						num = 0;
						continue;
					}
					break;
				case 23:
					if (true)
					{
					}
					num = 1;
					continue;
				case 24:
				{
					int num2;
					if (num2 == A_2.CacheFields.Count)
					{
						num = 7;
						continue;
					}
					XlsPivotCacheField a_2 = A_2.CacheFields.ᜀ(num2);
					num = 25;
					continue;
				}
				case 25:
					goto IL_349;
				case 26:
					goto IL_2B2;
				}
				if (A_0 == null)
				{
					num = 15;
					continue;
				}
				num = 11;
				continue;
				IL_FC:
				array2 = null;
				array = null;
				A_0.Read();
				array2 = sprℳ.ᜂ(A_0, A_1);
				sprℳ.ᜀ(A_0, A_1);
				array = sprℳ.ᜁ(A_0, A_1);
				num = 17;
				continue;
				IL_1DB:
				A_1.FieldGroup.ᜀ(array);
				num = 16;
				continue;
				IL_1F9:
				num = 20;
				continue;
				IL_349:
				num = 22;
			}
			IL_A7:
			throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁╃≅ⵇ㡉", a_));
			IL_F7:
			throw new ArgumentNullException(RecordTableEnumerator.b("⌿⍁❃⹅ⵇ橉", a_));
			IL_168:
			A_1.ParentFeildGroupIndex = num3;
			A_0.Read();
			return;
			IL_187:
			IL_1F4:
			goto IL_386;
			IL_2B2:
			return;
			IL_386:
			A_0.Read();
			A_0.Read();
			return;
		}
		}
	}

	// Token: 0x060022FA RID: 8954 RVA: 0x00143A2C File Offset: 0x00142A2C
	public static int[] ᜂ(XmlReader A_0, XlsPivotCacheField A_1)
	{
		int a_ = 17;
		int num = 5;
		List<int> list;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_E7;
			case 1:
				goto IL_E3;
			case 2:
				goto IL_C3;
			case 3:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("ㅆ", a_)))
				{
					num = 10;
					continue;
				}
				goto IL_E7;
			case 4:
				goto IL_68;
			case 6:
				num = 3;
				continue;
			case 7:
				if (true)
				{
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
					if (A_0.LocalName == RecordTableEnumerator.b("㽆", a_))
					{
						num = 6;
						continue;
					}
					goto IL_E7;
				}
				break;
			case 8:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 1;
					continue;
				}
				num = 7;
				continue;
			case 9:
				goto IL_C3;
			case 10:
				list.Add((int)XmlConvert.ToByte(A_0.Value));
				num = 0;
				continue;
			}
			if (A_0.LocalName != RecordTableEnumerator.b("⍆⁈㡊⹌㵎㑐❒ごݖ⭘", a_))
			{
				num = 4;
				continue;
			}
			list = new List<int>();
			num = 9;
			continue;
			IL_C3:
			num = 8;
			continue;
			IL_E7:
			A_0.Read();
			num = 2;
		}
		IL_68:
		return null;
		IL_E3:
		A_0.Read();
		return list.ToArray();
	}

	// Token: 0x060022FB RID: 8955 RVA: 0x00143BA8 File Offset: 0x00142BA8
	public static string[] ᜁ(XmlReader A_0, XlsPivotCacheField A_1)
	{
		int a_ = 17;
		int num = 7;
		List<string> list;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				goto IL_F1;
			case 1:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 6;
					continue;
				}
				num = 2;
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
					if (A_0.LocalName == RecordTableEnumerator.b("㑆", a_))
					{
						num = 10;
						continue;
					}
					goto IL_F1;
				}
				break;
			case 3:
				goto IL_CD;
			case 4:
				list.Add(A_0.Value);
				num = 0;
				continue;
			case 5:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("ㅆ", a_)))
				{
					num = 4;
					continue;
				}
				goto IL_F1;
			case 6:
				goto IL_ED;
			case 8:
				goto IL_70;
			case 9:
				goto IL_CD;
			case 10:
				num = 5;
				continue;
			}
			if (A_0.LocalName != RecordTableEnumerator.b("⁆㭈⑊㡌㽎ᡐ❒ご㩖⩘", a_))
			{
				num = 8;
				continue;
			}
			A_0.Read();
			list = new List<string>();
			num = 9;
			continue;
			IL_CD:
			num = 1;
			continue;
			IL_F1:
			A_0.Read();
			num = 3;
		}
		IL_70:
		return null;
		IL_ED:
		return list.ToArray();
	}

	// Token: 0x060022FC RID: 8956 RVA: 0x00143D1C File Offset: 0x00142D1C
	public static void ᜀ(XmlReader A_0, XlsPivotCacheField A_1)
	{
		int a_ = 1;
		int num = 13;
		for (;;)
		{
			spr\u1920 spr_u;
			switch (num)
			{
			case 0:
				spr_u.ᜂ(XmlConvert.ToDouble(A_0.Value));
				num = 19;
				continue;
			case 1:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䐶䴸娺似䬾ཀ㙂⡄", a_)))
				{
					num = 20;
					continue;
				}
				goto IL_333;
			case 2:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("倶䬸吺䠼伾̀㩂", a_)))
				{
					num = 10;
					continue;
				}
				goto IL_184;
			case 3:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("制圸强猼䨾ⱀ", a_)))
				{
					num = 7;
					continue;
				}
				goto IL_2FF;
			case 4:
				goto IL_1FA;
			case 5:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("倶䬸吺䠼伾ࡀⵂㅄ≆㭈㵊ⱌ⍎", a_)))
				{
					num = 0;
					continue;
				}
				goto IL_27E;
			case 6:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("嘶䰸伺刼稾⽀❂", a_)))
				{
					num = 12;
					continue;
				}
				goto IL_1FA;
			case 7:
				spr_u.ᜀ(XmlConvert.ToDouble(A_0.Value));
				num = 17;
				continue;
			case 8:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䐶䴸娺似䬾Հ≂ㅄ≆", a_)))
				{
					num = 15;
					continue;
				}
				goto IL_22E;
			case 9:
				spr_u.ᜀ(Convert.ToDateTime(A_0.Value));
				num = 14;
				continue;
			case 10:
				spr_u.ᜀ((PivotFieldGroupType)Enum.Parse(typeof(PivotFieldGroupType), A_0.Value, true));
				num = 18;
				continue;
			case 11:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("制圸强礼帾㕀♂", a_)))
				{
					num = 9;
					continue;
				}
				goto IL_2CE;
			case 12:
				spr_u.ᜁ(XmlConvert.ToBoolean(A_0.Value));
				num = 4;
				continue;
			case 14:
				goto IL_2CE;
			case 15:
				spr_u.ᜁ(Convert.ToDateTime(A_0.Value));
				num = 21;
				continue;
			case 16:
				goto IL_1D4;
			case 17:
				goto IL_2FF;
			case 18:
				goto IL_184;
			case 19:
				goto IL_27E;
			case 20:
				spr_u.ᜁ(XmlConvert.ToDouble(A_0.Value));
				num = 16;
				continue;
			case 21:
				goto IL_22E;
			case 22:
				return;
			}
			if (A_0.LocalName != RecordTableEnumerator.b("䔶堸唺娼娾ᅀㅂ", a_))
			{
				num = 22;
				continue;
			}
			spr_u = A_1.FieldGroup;
			if (true)
			{
			}
			num = 6;
			continue;
			IL_184:
			num = 5;
			continue;
			IL_1FA:
			num = 11;
			continue;
			IL_22E:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			default:
				if (false)
				{
				}
				num = 1;
				continue;
			}
			IL_27E:
			num = 8;
			continue;
			IL_2CE:
			num = 3;
			continue;
			IL_2FF:
			num = 2;
		}
		return;
		IL_1D4:
		IL_333:
		A_0.Read();
	}

	// Token: 0x060022FD RID: 8957 RVA: 0x00144064 File Offset: 0x00143064
	public static void ᜅ(XmlReader A_0, XlsPivotCache A_1)
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
		int a_ = 0;
		sprℳ.ᜀ(A_0, A_1, a_);
	}

	// Token: 0x060022FE RID: 8958 RVA: 0x001440AC File Offset: 0x001430AC
	public static void ᜀ(XmlReader A_0, XlsPivotCache A_1, int A_2)
	{
		int a_ = 10;
		int num = 9;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				if (A_0.LocalName == RecordTableEnumerator.b("㈿", a_))
				{
					num = 2;
					continue;
				}
				goto IL_E2;
			case 2:
			{
				int a_2;
				sprℳ.ᜀ(A_0, A_1, A_2, a_2);
				int num2;
				num2++;
				A_0.Read();
				num = 8;
				continue;
			}
			case 3:
				goto IL_E0;
			case 4:
				goto IL_68;
			case 5:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 0;
					continue;
				}
				if (true)
				{
				}
				num = 1;
				continue;
			case 6:
			{
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				A_0.Read();
				int num2 = 0;
				int a_2 = A_1.CacheFields.ᜀ();
				num = 7;
				continue;
			}
			case 7:
				goto IL_E2;
			case 8:
				goto IL_E2;
			case 9:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_111;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			num = 6;
			continue;
			IL_E2:
			num = 5;
		}
		IL_68:
		goto IL_111;
		IL_E0:
		throw new ArgumentNullException(RecordTableEnumerator.b("〿⭁㉃⥅㱇橉⽋⽍㍏㩑ㅓ", a_));
		IL_111:
		throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁╃≅ⵇ㡉", a_));
	}

	// Token: 0x060022FF RID: 8959 RVA: 0x00144214 File Offset: 0x00143214
	public static byte[] ᜀ(XmlReader A_0, XlsPivotCache A_1, int A_2, int A_3)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			int num = 13;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					goto IL_1B7;
				case 1:
					num = 11;
					continue;
				case 2:
					goto IL_BE;
				case 3:
					goto IL_1B7;
				case 4:
				{
					string a;
					if (!(a == RecordTableEnumerator.b("♁", a_)))
					{
						num = 5;
						continue;
					}
					DateTime dateTime = XmlConvert.ToDateTime(A_0.Value, XmlDateTimeSerializationMode.Unspecified);
					byte[] array;
					array[num2] = A_1.ᜀ(num2, dateTime);
					goto IL_35F;
				}
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_35F;
					default:
						if (false)
						{
						}
						num = 19;
						continue;
					}
					break;
				case 6:
				{
					if (A_1 == null)
					{
						num = 31;
						continue;
					}
					num2 = 0;
					string text = null;
					byte[] array = new byte[A_3];
					A_0.Read();
					num = 14;
					continue;
				}
				case 7:
					goto IL_1B7;
				case 8:
				{
					string a;
					if (!(a == RecordTableEnumerator.b("ⱁ", a_)))
					{
						num = 22;
						continue;
					}
					string text;
					double num3 = XmlConvert.ToDouble(text);
					byte[] array;
					array[num2] = A_1.ᜀ(num2, num3);
					num = 3;
					continue;
				}
				case 9:
					num = 4;
					continue;
				case 10:
				{
					string a;
					if (!(a == RecordTableEnumerator.b("ㅁ", a_)))
					{
						num = 9;
						continue;
					}
					byte[] array;
					string text;
					array[num2] = A_1.ᜀ(num2, text);
					num = 7;
					continue;
				}
				case 11:
				{
					string a;
					if (!(a == RecordTableEnumerator.b("㩁", a_)))
					{
						num = 28;
						continue;
					}
					string text;
					uint num4 = XmlConvert.ToUInt32(text);
					byte[] array;
					array[num2] = (byte)num4;
					num = 0;
					continue;
				}
				case 12:
					goto IL_1B7;
				case 14:
					goto IL_243;
				case 15:
				{
					string text = A_0.Value;
					num = 23;
					continue;
				}
				case 16:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㑁", a_)))
					{
						num = 15;
						continue;
					}
					goto IL_2DB;
				case 17:
				{
					string a;
					if (!(a == RecordTableEnumerator.b("⽁", a_)))
					{
						num = 24;
						continue;
					}
					byte[] array;
					array[num2] = A_1.ᜀ(num2, null);
					num = 29;
					continue;
				}
				case 18:
					goto IL_1B7;
				case 19:
				{
					string a;
					if (!(a == RecordTableEnumerator.b("⁁", a_)))
					{
						num = 30;
						continue;
					}
					string text;
					bool flag = XmlConvert.ToBoolean(text);
					byte[] array;
					array[num2] = A_1.ᜀ(num2, flag);
					num = 18;
					continue;
				}
				case 20:
				{
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 26;
						continue;
					}
					string localName = A_0.LocalName;
					num = 16;
					continue;
				}
				case 21:
					goto IL_243;
				case 22:
					num = 10;
					continue;
				case 23:
					goto IL_2DB;
				case 24:
					num = 27;
					continue;
				case 25:
				{
					string a;
					string localName;
					if ((a = localName) != null)
					{
						num = 1;
						continue;
					}
					goto IL_1B7;
				}
				case 26:
				{
					byte[] array;
					return array;
				}
				case 27:
					goto IL_1B7;
				case 28:
					num = 8;
					continue;
				case 29:
					if (true)
					{
					}
					goto IL_1B7;
				case 30:
					num = 17;
					continue;
				case 31:
					goto IL_442;
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				num = 6;
				continue;
				IL_1B7:
				num2++;
				A_0.Read();
				num = 21;
				continue;
				IL_243:
				num = 20;
				continue;
				IL_2DB:
				num = 25;
				continue;
				IL_35F:
				num = 12;
			}
			IL_BE:
			throw new ArgumentNullException(RecordTableEnumerator.b("ぁ⅃❅ⱇ⽉㹋", a_));
			IL_442:
			throw new ArgumentNullException(RecordTableEnumerator.b("㉁ⵃぅ❇㹉汋ⵍㅏㅑ㱓㍕", a_));
		}
		}
	}

	// Token: 0x06002300 RID: 8960 RVA: 0x0014466C File Offset: 0x0014366C
	private static void ᜀ(XmlReader A_0, sprᾷ A_1)
	{
		int a_ = 13;
		switch (0)
		{
		default:
		{
			int num = 21;
			for (;;)
			{
				if (true)
				{
				}
				spr\u25B2 spr_u25B;
				XlsPivotCacheField xlsPivotCacheField;
				sprỺ sprỺ;
				switch (num)
				{
				case 0:
					goto IL_245;
				case 1:
					if (A_0.LocalName == RecordTableEnumerator.b("⁂⑄⭆⩈㹊⅌⹎═㙒ㅔṖⵘ㹚ぜ", a_))
					{
						num = 7;
						continue;
					}
					goto IL_1BA;
				case 2:
				{
					string text;
					if (text != null)
					{
						num = 16;
						continue;
					}
					goto IL_245;
				}
				case 3:
				{
					int num2;
					if (num2 == -1)
					{
						num = 5;
						continue;
					}
					goto IL_186;
				}
				case 4:
				{
					int a_2 = spr_u25B.ᜁ()[0].ᜂ();
					xlsPivotCacheField = A_1.ᜀ(a_2);
					sprỺ.ᜀ(a_2);
					num = 15;
					continue;
				}
				case 5:
				{
					string text;
					string a_3 = sprℳ.ᜀ(text);
					xlsPivotCacheField = A_1.ᜀ(a_3);
					int num2 = xlsPivotCacheField.Index;
					num = 18;
					continue;
				}
				case 6:
					goto IL_2F0;
				case 7:
				{
					int num2 = sprℳ.ᜀ(A_0, RecordTableEnumerator.b("╂⥄⍆", a_), -1);
					num = 8;
					continue;
				}
				case 8:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("╂⩄㕆⑈㹊⅌⹎", a_)))
					{
						num = 20;
						continue;
					}
					goto IL_2F0;
				case 9:
				{
					int num2;
					if (num2 == -1)
					{
						num = 4;
						continue;
					}
					goto IL_220;
				}
				case 10:
					return;
				case 11:
					goto IL_184;
				case 12:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 10;
						continue;
					}
					num = 1;
					continue;
				case 13:
					goto IL_240;
				case 14:
				{
					if (A_1 == null)
					{
						num = 11;
						continue;
					}
					A_0.Read();
					string text = null;
					int num2 = -1;
					xlsPivotCacheField = A_1.ᜀ(0);
					num = 19;
					continue;
				}
				case 15:
					goto IL_220;
				case 16:
				{
					string text;
					sprỺ.ᜀ(text);
					num = 0;
					continue;
				}
				case 17:
					return;
				case 18:
					goto IL_186;
				case 19:
					goto IL_1BA;
				case 20:
				{
					string text = A_0.Value;
					num = 6;
					continue;
				}
				}
				if (!(A_0.LocalName != RecordTableEnumerator.b("⁂⑄⭆⩈㹊⅌⹎═㙒ㅔṖⵘ㹚ぜⱞ", a_)))
				{
					num = 14;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_240;
				default:
					if (false)
					{
					}
					num = 17;
					continue;
				}
				IL_186:
				sprỺ = new sprỺ(xlsPivotCacheField);
				spr_u25B = sprỺ.ᜀ();
				num = 2;
				continue;
				IL_1BA:
				num = 12;
				continue;
				IL_240:
				goto IL_1BA;
				IL_220:
				xlsPivotCacheField.CalculatedItems.ᜀ(sprỺ);
				A_0.Read();
				num = 13;
				continue;
				IL_245:
				sprℳ.ᜁ(A_0, spr_u25B);
				num = 9;
				continue;
				IL_2F0:
				num = 3;
			}
			return;
			IL_184:
			throw new ArgumentNullException(RecordTableEnumerator.b("⁂⑄⑆ⅈ⹊ୌ♎㑐㽒ㅔ", a_));
		}
		}
	}

	// Token: 0x06002301 RID: 8961 RVA: 0x00144990 File Offset: 0x00143990
	private static void ᜁ(XmlReader A_0, spr\u25B2 A_1)
	{
		int a_ = 2;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				A_1.ᜀ((AxisTypes)Enum.Parse(typeof(PivotAxisTypes2007), A_0.Value, true));
				num = 6;
				continue;
			case 1:
				A_1.ᜀ((PivotAreaType)Enum.Parse(typeof(PivotAreaType), A_0.Value, false));
				num = 9;
				continue;
			case 3:
				goto IL_CA;
			case 4:
				if (!A_0.MoveToAttribute(RecordTableEnumerator.b("夷䠹夻弽ᐿ㭁㑃⍅", a_)))
				{
					goto IL_25A;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
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
			case 5:
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				A_0.Read();
				num = 7;
				continue;
			case 6:
				goto IL_114;
			case 7:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("夷䈹唻䴽", a_)))
				{
					num = 0;
					continue;
				}
				goto IL_114;
			case 8:
				goto IL_4C;
			case 9:
				goto IL_FB;
			}
			if (A_0 == null)
			{
				num = 8;
				continue;
			}
			num = 5;
			continue;
			IL_114:
			A_1.ᜆ(sprℳ.ᜁ(A_0, RecordTableEnumerator.b("嬷嬹弻嘽┿ୁ⩃≅ⵇ㉉", a_)));
			A_1.ᜅ(sprℳ.ᜁ(A_0, RecordTableEnumerator.b("尷嬹䠻弽༿ⱁ⡃㽅", a_)));
			A_1.ᜁ(sprℳ.ᜀ(A_0, RecordTableEnumerator.b("帷匹夻刽␿", a_), -1));
			A_1.ᜀ(sprℳ.ᜀ(A_0, RecordTableEnumerator.b("帷匹夻刽␿ቁ⭃㕅ⅇ㹉╋⅍㹏", a_), -1));
			A_1.ᜄ(sprℳ.ᜁ(A_0, RecordTableEnumerator.b("嬷唹倻礽㈿⍁⩃≅᱇╉㡋⽍㱏⅑", a_)));
			A_1.ᜀ(sprℳ.ᜁ(A_0, RecordTableEnumerator.b("強䠹崻倽␿၁⭃ㅅ", a_)));
			A_1.ᜂ(sprℳ.ᜁ(A_0, RecordTableEnumerator.b("吷嬹帻嬽ⰿു⩃⩅ㅇ", a_)));
			A_1.ᜃ(sprℳ.ᜀ(A_0, RecordTableEnumerator.b("圷伹䠻刽⤿ⱁ⅃", a_), true));
			num = 4;
		}
		IL_4C:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹崻娽┿ぁ", a_));
		IL_CA:
		throw new ArgumentNullException(RecordTableEnumerator.b("样匹䨻儽㐿扁Ճ㑅ⵇ⭉", a_));
		IL_FB:
		IL_25A:
		A_0.Read();
		sprℳ.ᜀ(A_0, A_1);
		A_0.Read();
	}

	// Token: 0x06002302 RID: 8962 RVA: 0x00144C0C File Offset: 0x00143C0C
	private static void ᜀ(XmlReader A_0, spr\u25B2 A_1)
	{
		int a_ = 4;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 8;
					continue;
				}
				num = 5;
				continue;
			case 1:
				if (A_1 == null)
				{
					num = 7;
					continue;
				}
				A_0.Read();
				num = 2;
				continue;
			case 2:
				goto IL_D1;
			case 3:
			{
				sprᲹ sprᲹ = new sprᲹ();
				sprℳ.ᜀ(A_0, sprᲹ);
				A_1.ᜁ().Add(sprᲹ);
				A_0.Read();
				num = 9;
				continue;
			}
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_FD;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 5:
				if (true)
				{
				}
				if (A_0.LocalName == RecordTableEnumerator.b("䠹夻堽┿ぁ⅃⡅⭇⽉", a_))
				{
					num = 3;
					continue;
				}
				goto IL_D1;
			case 6:
				goto IL_68;
			case 7:
				goto IL_CF;
			case 8:
				goto IL_FB;
			case 9:
				goto IL_D1;
			}
			if (A_0 == null)
			{
				num = 6;
				continue;
			}
			num = 1;
			continue;
			IL_D1:
			num = 0;
		}
		IL_68:
		goto IL_FD;
		IL_CF:
		throw new ArgumentNullException(RecordTableEnumerator.b("樹唻䠽⼿㙁摃݅㩇⽉ⵋ", a_));
		IL_FB:
		A_0.Read();
		return;
		IL_FD:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠹夻弽␿❁㙃", a_));
	}

	// Token: 0x06002303 RID: 8963 RVA: 0x00144D74 File Offset: 0x00143D74
	private static void ᜀ(XmlReader A_0, sprᲹ A_1)
	{
		int a_ = 15;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_F4;
			case 2:
				goto IL_174;
			case 3:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				A_1.ᜀ(sprℳ.ᜀ(A_0, RecordTableEnumerator.b("⍄⹆ⱈ❊⥌", a_), -1));
				A_1.ᜀ(spr\u2005.ᜀ(A_0));
				A_1.ᜁ(sprℳ.ᜁ(A_0, RecordTableEnumerator.b("❄㹆᥈⑊㹌♎═㩒㩔㥖", a_)));
				A_1.ᜂ(sprℳ.ᜁ(A_0, RecordTableEnumerator.b("㝄≆╈⩊㥌♎❐㙒", a_)));
				A_1.ᜀ(sprℳ.ᜁ(A_0, RecordTableEnumerator.b("㙄≆╈⹊⹌㭎㑐㝒", a_)));
				A_0.Read();
				num = 5;
				continue;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_120;
				}
				break;
			case 5:
				if (A_0.LocalName != RecordTableEnumerator.b("㵄", a_))
				{
					num = 1;
					continue;
				}
				goto IL_176;
			}
			IL_31:
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			num = 3;
			continue;
			goto IL_31;
		}
		IL_F4:
		throw new XmlException(RecordTableEnumerator.b("い⥆ⱈ㍊㵌⩎㉐❒ご㍖祘Ṛㅜ㩞ౠ٢୤፦䥨Ὢ౬࡮", a_));
		IL_120:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㝄≆⡈⽊⡌㵎", a_));
		IL_174:
		throw new ArgumentNullException(RecordTableEnumerator.b("㝄≆⽈⹊㽌⩎㽐げご", a_));
		IL_176:
		if (true)
		{
		}
		int item = sprℳ.ᜂ(A_0, RecordTableEnumerator.b("㍄", a_));
		A_1.ᜃ().Add(item);
		A_0.Read();
	}

	// Token: 0x06002304 RID: 8964 RVA: 0x00144F28 File Offset: 0x00143F28
	private static void ᜄ(XmlReader A_0, XlsPivotCache A_1)
	{
		int a_ = 5;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				if (A_0.LocalName != RecordTableEnumerator.b("堺尼尾⥀♂ൄ⹆ⱈ㥊ⱌ㵎㉐㭒㱔㉖⩘", a_))
				{
					num = 8;
					continue;
				}
				num = 2;
				continue;
			case 2:
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				num = 5;
				continue;
			case 3:
				goto IL_89;
			case 4:
				goto IL_50;
			case 5:
				if (A_1.PreservedElements != null)
				{
					num = 6;
					continue;
				}
				return;
			case 6:
			{
				A_0.MoveToElement();
				Stream value = ShapeParser.ReadNodeAsStream(A_0);
				A_1.PreservedElements.Add(RecordTableEnumerator.b("堺尼尾⥀♂ൄ⹆ⱈ㥊ⱌ㵎㉐㭒㱔㉖⩘", a_), value);
				num = 7;
				continue;
			}
			case 7:
				return;
			case 8:
				goto IL_B9;
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 4;
			}
			else
			{
				num = 1;
			}
		}
		IL_50:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return;
		default:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䤺堼帾╀♂㝄", a_));
		}
		IL_89:
		throw new ArgumentNullException(RecordTableEnumerator.b("堺尼尾⥀♂", a_));
		IL_B9:;
	}

	// Token: 0x06002305 RID: 8965 RVA: 0x0014507C File Offset: 0x0014407C
	private static void ᜃ(XmlReader A_0, XlsPivotCache A_1)
	{
		int a_ = 8;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1.PreservedElements != null)
				{
					num = 1;
					continue;
				}
				return;
			case 1:
			{
				A_0.MoveToElement();
				Stream value = ShapeParser.ReadNodeAsStream(A_0);
				A_1.PreservedElements.Add(RecordTableEnumerator.b("唽〿⭁㝃", a_), value);
				num = 7;
				continue;
			}
			case 2:
				goto IL_48;
			case 4:
				if (A_1 == null)
				{
					num = 5;
					continue;
				}
				num = 0;
				continue;
			case 5:
				goto IL_81;
			case 6:
				goto IL_B1;
			case 7:
				goto IL_E9;
			case 8:
				if (A_0.LocalName != RecordTableEnumerator.b("唽〿⭁㝃", a_))
				{
					num = 6;
					continue;
				}
				num = 4;
				continue;
			}
			if (A_0 == null)
			{
				num = 2;
			}
			else
			{
				num = 8;
			}
		}
		IL_48:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return;
		default:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䰽┿⍁⁃⍅㩇", a_));
		}
		IL_81:
		throw new ArgumentNullException(RecordTableEnumerator.b("崽ℿ⅁ⱃ⍅", a_));
		IL_B1:
		return;
		IL_E9:
		if (true)
		{
		}
	}

	// Token: 0x06002306 RID: 8966 RVA: 0x001451D0 File Offset: 0x001441D0
	private static void ᜂ(XmlReader A_0, XlsPivotCache A_1)
	{
		int a_ = 19;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				A_0.MoveToElement();
				Stream value = ShapeParser.ReadNodeAsStream(A_0);
				A_1.PreservedElements.Add(RecordTableEnumerator.b("ⵈ≊⁌⩎㽐⁒㱔㡖㝘⡚", a_), value);
				num = 1;
				continue;
			}
			case 1:
				return;
			case 2:
				goto IL_81;
			case 4:
				goto IL_48;
			case 5:
				if (A_0.LocalName != RecordTableEnumerator.b("ⵈ≊⁌⩎㽐⁒㱔㡖㝘⡚", a_))
				{
					num = 6;
					continue;
				}
				num = 7;
				continue;
			case 6:
				goto IL_B1;
			case 7:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				num = 8;
				continue;
			case 8:
				if (true)
				{
				}
				if (A_1.PreservedElements != null)
				{
					num = 0;
					continue;
				}
				return;
			}
			if (A_0 == null)
			{
				num = 4;
			}
			else
			{
				num = 5;
			}
		}
		IL_48:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return;
		default:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㭈⹊ⱌ⭎㑐⅒", a_));
		}
		IL_81:
		throw new ArgumentNullException(RecordTableEnumerator.b("⩈⩊⹌❎㑐", a_));
		IL_B1:;
	}

	// Token: 0x06002307 RID: 8967 RVA: 0x00145324 File Offset: 0x00144324
	private static void ᜁ(XmlReader A_0, XlsPivotCache A_1)
	{
		int a_ = 6;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.LocalName != RecordTableEnumerator.b("儻嬽ℿㅁㅃ㑅ⵇ൉㹋⅍╏≑❓", a_))
				{
					num = 8;
					continue;
				}
				num = 7;
				continue;
			case 1:
				if (true)
				{
				}
				break;
			case 2:
				goto IL_89;
			case 3:
			{
				A_0.MoveToElement();
				Stream value = ShapeParser.ReadNodeAsStream(A_0);
				A_1.PreservedElements.Add(RecordTableEnumerator.b("儻嬽ℿㅁㅃ㑅ⵇ൉㹋⅍╏≑❓", a_), value);
				num = 4;
				continue;
			}
			case 4:
				return;
			case 5:
				goto IL_50;
			case 6:
				if (A_1.PreservedElements != null)
				{
					num = 3;
					continue;
				}
				return;
			case 7:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				num = 6;
				continue;
			case 8:
				goto IL_B9;
			}
			if (A_0 == null)
			{
				num = 5;
			}
			else
			{
				num = 0;
			}
		}
		IL_50:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return;
		default:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("主嬽ℿ♁⅃㑅", a_));
		}
		IL_89:
		throw new ArgumentNullException(RecordTableEnumerator.b("弻弽⌿⩁⅃", a_));
		IL_B9:;
	}

	// Token: 0x06002308 RID: 8968 RVA: 0x00145478 File Offset: 0x00144478
	private static void ᜀ(XmlReader A_0, XlsPivotCache A_1)
	{
		int a_ = 5;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.LocalName != RecordTableEnumerator.b("嘺尼伾㉀", a_))
				{
					num = 8;
					continue;
				}
				num = 4;
				continue;
			case 1:
				goto IL_48;
			case 2:
			{
				A_0.MoveToElement();
				Stream value = ShapeParser.ReadNodeAsStream(A_0);
				A_1.PreservedElements.Add(RecordTableEnumerator.b("嘺尼伾㉀", a_), value);
				num = 7;
				continue;
			}
			case 3:
				goto IL_81;
			case 4:
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				num = 5;
				continue;
			case 5:
				if (A_1.PreservedElements != null)
				{
					num = 2;
					continue;
				}
				return;
			case 7:
				return;
			case 8:
				goto IL_B1;
			}
			if (A_0 == null)
			{
				num = 1;
			}
			else
			{
				num = 0;
			}
		}
		IL_48:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return;
		default:
			if (false)
			{
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䤺堼帾╀♂㝄", a_));
		}
		IL_81:
		throw new ArgumentNullException(RecordTableEnumerator.b("堺尼尾⥀♂", a_));
		IL_B1:;
	}

	// Token: 0x06002309 RID: 8969 RVA: 0x001455CC File Offset: 0x001445CC
	internal static DataSourceType ᜁ(string A_0)
	{
		int a_ = 15;
		DataSourceType result;
		for (;;)
		{
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
				result = DataSourceType.Worksheet;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return result;
					case 1:
						if (A_0 != null)
						{
							num = 3;
							continue;
						}
						return result;
					case 2:
						return result;
					case 3:
						num = 10;
						continue;
					case 4:
						if (!(A_0 == RecordTableEnumerator.b("㙄⑆ⱈ╊ⱌ㵎㡐㱒", a_)))
						{
							num = 7;
							continue;
						}
						result = DataSourceType.ScenarioPivotTable;
						num = 13;
						continue;
					case 5:
						if (!(A_0 == RecordTableEnumerator.b("♄⡆❈㡊≌⍎㡐㝒㑔⍖じ㑚㍜", a_)))
						{
							num = 14;
							continue;
						}
						result = DataSourceType.Consolidation;
						num = 11;
						continue;
					case 6:
						return result;
					case 7:
						num = 6;
						continue;
					case 8:
						if (true)
						{
						}
						num = 4;
						continue;
					case 9:
						if (!(A_0 == RecordTableEnumerator.b("⁄㽆㵈⹊㽌ⅎぐ㽒", a_)))
						{
							num = 8;
							continue;
						}
						result = DataSourceType.ExternalData;
						num = 2;
						continue;
					case 10:
						if (!(A_0 == RecordTableEnumerator.b("㉄⡆㭈⁊㹌❎㑐㙒⅔", a_)))
						{
							num = 12;
							continue;
						}
						result = DataSourceType.Worksheet;
						num = 0;
						continue;
					case 11:
						return result;
					case 12:
						num = 5;
						continue;
					case 13:
						return result;
					case 14:
						num = 9;
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

	// Token: 0x0600230A RID: 8970 RVA: 0x00145788 File Offset: 0x00144788
	private static int ᜂ(XmlReader A_0, string A_1)
	{
		while (!A_0.MoveToAttribute(A_1))
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
				if (true)
				{
				}
				return 0;
			}
		}
		string value = A_0.Value;
		return XmlConvert.ToInt32(value);
	}

	// Token: 0x0600230B RID: 8971 RVA: 0x001457E0 File Offset: 0x001447E0
	private static int ᜀ(XmlReader A_0, string A_1, int A_2)
	{
		while (!A_0.MoveToAttribute(A_1))
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			}
			if (false)
			{
			}
			return A_2;
		}
		if (true)
		{
		}
		string value = A_0.Value;
		return XmlConvert.ToInt32(value);
	}

	// Token: 0x0600230C RID: 8972 RVA: 0x00145838 File Offset: 0x00144838
	private static bool ᜁ(XmlReader A_0, string A_1)
	{
		while (!A_0.MoveToAttribute(A_1))
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
				return false;
			}
		}
		string value = A_0.Value;
		return XmlConvert.ToBoolean(value);
	}

	// Token: 0x0600230D RID: 8973 RVA: 0x00145890 File Offset: 0x00144890
	private static bool ᜀ(XmlReader A_0, string A_1, bool A_2)
	{
		while (!A_0.MoveToAttribute(A_1))
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
				return A_2;
			}
		}
		string value = A_0.Value;
		return XmlConvert.ToBoolean(value);
	}

	// Token: 0x0600230E RID: 8974 RVA: 0x001458E8 File Offset: 0x001448E8
	private static string ᜀ(XmlReader A_0, string A_1)
	{
		while (!A_0.MoveToAttribute(A_1))
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			}
			if (false)
			{
			}
			return null;
		}
		if (true)
		{
		}
		return A_0.Value;
	}

	// Token: 0x0600230F RID: 8975 RVA: 0x00145938 File Offset: 0x00144938
	private static string ᜀ(string A_0)
	{
		int a_ = 18;
		if (true)
		{
		}
		switch (0)
		{
		default:
		{
			string text;
			for (;;)
			{
				char c = '[';
				text = A_0.Split(new char[]
				{
					c
				})[0];
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_A1;
					case 1:
						IL_55:
						if (text.Contains(RecordTableEnumerator.b("潇", a_)))
						{
							num = 2;
							continue;
						}
						goto IL_A1;
					case 2:
						text = text.Split(new char[]
						{
							'\''
						})[1];
						num = 0;
						continue;
					}
					break;
					IL_A1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_55;
					default:
						goto IL_B7;
					}
				}
			}
			IL_B7:
			if (false)
			{
			}
			return text;
		}
		}
	}
}
