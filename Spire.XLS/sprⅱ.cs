using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.PivotTables;
using Spire.Xls.Core.Spreadsheet.XmlReaders.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlSerialization;

// Token: 0x02000499 RID: 1177
internal class spr\u2171
{
	// Token: 0x06004862 RID: 18530 RVA: 0x002BAB5C File Offset: 0x002B9B5C
	public static void ᜀ(XmlWriter A_0, XlsPivotCache A_1, IWorkbook A_2, string A_3, RelationsCollection A_4)
	{
		int a_ = 2;
		switch (0)
		{
		default:
		{
			XlsWorkbook xlsWorkbook;
			for (;;)
			{
				xlsWorkbook = (A_2 as XlsWorkbook);
				int num = 3;
				for (;;)
				{
					spr\u20A6 spr_u20A;
					switch (num)
					{
					case 0:
						A_0.WriteAttributeString(RecordTableEnumerator.b("儷帹", a_), RecordTableEnumerator.b("倷丹䠻丽稿流歃㕅⭇≉⥋⍍ㅏ⅑穓㥕⡗㽙㉛♝ൟ๡ɣ॥ᩧݩ൫ᩭͯ山᭳ѵί啹፻᡽첇ﮍﶏ望랗ꢙ겛꺝隟趡횣쎥쒧쮩\ud8ab잭\udfaf\udcb1잳\udeb5톷쪹쾻", a_), A_3);
						num = 7;
						continue;
					case 1:
						spr\u2171.ᜀ(A_2 as XlsWorkbook, spr_u20A);
						num = 4;
						continue;
					case 2:
						goto IL_98;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2FC;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							if (xlsWorkbook.PreservesPivotCache.Count > 0)
							{
								num = 2;
								continue;
							}
							A_0.WriteStartElement(RecordTableEnumerator.b("䠷匹䨻儽㐿Ł╃╅⁇⽉ࡋ⭍㙏㭑㩓㽕ⱗ㍙㍛そ", a_), RecordTableEnumerator.b("倷丹䠻丽稿流歃㕅⭇≉⥋⍍ㅏ⅑穓㥕⡗㽙㉛♝ൟ๡ɣ॥ᩧݩ൫ᩭͯ山᭳ѵί啹ཻ๽ﮇﾑ릕ꪗꪙ겛ꢝ辟쾡얣쾥욧", a_));
							num = 6;
							continue;
						}
						break;
					case 4:
						return;
					case 5:
						if (spr_u20A != null)
						{
							num = 1;
							continue;
						}
						return;
					case 6:
						if (A_3 != null)
						{
							num = 0;
							continue;
						}
						goto IL_9D;
					case 7:
						goto IL_2FC;
					}
					break;
					IL_9D:
					A_0.WriteAttributeString(RecordTableEnumerator.b("䨷弹娻䰽┿ㅁⱃ⍅ⱇࡉ㕋", a_), A_2.Author);
					double value = A_1.RefreshDate.ToOADate();
					A_0.WriteAttributeString(RecordTableEnumerator.b("䨷弹娻䰽┿ㅁⱃ⍅ⱇ้ⵋ㩍㕏", a_), XmlConvert.ToString(value));
					A_0.WriteAttributeString(RecordTableEnumerator.b("嬷䠹夻弽㐿❁⁃၅ⵇ㡉㽋❍㽏㱑", a_), 3.ToString());
					A_0.WriteAttributeString(RecordTableEnumerator.b("䨷弹娻䰽┿ㅁⱃ⍅ⱇ᱉⥋㱍⍏㭑㭓㡕", a_), 3.ToString());
					A_0.WriteAttributeString(RecordTableEnumerator.b("唷匹刻氽┿⑁㙃⍅㭇≉ⵋⱍ㱏㝑ɓ㍕⩗⥙㕛ㅝ๟", a_), 3.ToString());
					spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("娷嬹弻唽✿ぁ⭃㍅♇⹉ᵋ㭍㕏⁑ⵓ", a_), A_1.IsBackgroundQuery, false);
					spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("崷吹崻尽ⰿ❁ᙃ⍅⹇㡉⥋㵍㡏", a_), A_1.EnableRefresh, false);
					spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("䨷弹娻䰽┿ㅁⱃॅ♇ى⍋⽍㑏", a_), A_1.IsRefreshOnLoad, false);
					spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("儷吹䨻弽ⰿ⭁⁃", a_), A_1.IsInvalidData, false);
					spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("圷䨹䠻圽ⴿ⭁㹃⍅Շ⽉⅋⅍≏⭑", a_), A_1.IsOptimizedCache, false);
					spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("䴷䨹嬻䰽ℿ♁⅃ॅ♇ᡉ⥋⡍≏㝑❓㹕", a_), A_1.IsUpgradeOnRefresh, false);
					spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("䬷伹䰻丽⼿ぁぃ݅ⱇ㱉ⵋ⁍㍏㝑こቕ⩗㍙せ㉝", a_), A_1.SupportAdvancedDrill, false);
					spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("䬷伹䰻丽⼿ぁぃᕅ㵇⡉㵋㭍㕏⁑ⵓ", a_), A_1.IsSupportSubQuery, false);
					spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("䬷嬹䨻嬽п⍁ぃ❅", a_), A_1.IsSaveData, true);
					A_0.WriteAttributeString(RecordTableEnumerator.b("䨷弹弻儽㈿♁݃⥅㵇⑉㡋", a_), A_1.RecordCount.ToString());
					spr\u2171.ᜁ(A_0, A_1, A_4);
					spr\u2171.ᜀ(A_0, A_1.CacheFields, A_1.HasNamedRange);
					spr\u2171.ᜀ(A_0, A_1.CacheFields);
					spr\u2171.ᜄ(A_0, A_1);
					spr\u2171.ᜃ(A_0, A_1);
					spr\u2171.ᜂ(A_0, A_1);
					spr\u2171.ᜁ(A_0, A_1);
					spr\u2171.ᜀ(A_0, A_1);
					A_0.WriteEndElement();
					spr_u20A = (A_1.SourceRange as spr\u20A6);
					num = 5;
					continue;
					IL_2FC:
					goto IL_9D;
				}
			}
			IL_98:
			Stream stream = xlsWorkbook.PreservesPivotCache[0];
			stream.Position = 0L;
			ShapeParser.WriteNodeFromStream(A_0, stream);
			xlsWorkbook.PreservesPivotCache.RemoveAt(0);
			return;
		}
		}
	}

	// Token: 0x06004863 RID: 18531 RVA: 0x002BAEF4 File Offset: 0x002B9EF4
	private static void ᜀ(XlsWorkbook A_0, spr\u20A6 A_1)
	{
		for (;;)
		{
			A_1.ᝈ();
			int num = A_1.\u1754();
			int num2 = 8;
			for (;;)
			{
				int num3;
				IXLSRange ixlsrange;
				switch (num2)
				{
				case 0:
					goto IL_6A;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_80;
					default:
						if (false)
						{
						}
						num++;
						num2 = 10;
						continue;
					}
					break;
				case 2:
					return;
				case 3:
					if (num3 > A_1.\u171E())
					{
						num2 = 1;
						continue;
					}
					goto IL_80;
				case 4:
					goto IL_4C;
				case 5:
					if (ixlsrange.HasString)
					{
						num2 = 9;
						continue;
					}
					goto IL_6A;
				case 6:
					goto IL_4C;
				case 7:
					if (num > A_1.ᝉ())
					{
						num2 = 2;
						continue;
					}
					num3 = A_1.ᝎ();
					num2 = 6;
					continue;
				case 8:
					goto IL_A9;
				case 9:
					A_0.InnerSST.AddIncrease(ixlsrange.Text, true);
					num2 = 0;
					continue;
				case 10:
					goto IL_A9;
				}
				break;
				IL_4C:
				num2 = 3;
				continue;
				IL_6A:
				num3++;
				if (true)
				{
				}
				num2 = 4;
				continue;
				IL_80:
				ixlsrange = A_1.ᜀ(num, num3);
				num2 = 5;
				continue;
				IL_A9:
				num2 = 7;
			}
		}
	}

	// Token: 0x06004864 RID: 18532 RVA: 0x002BB03C File Offset: 0x002BA03C
	private static void ᜀ(XmlWriter A_0, sprᾷ A_1, bool A_2)
	{
		int a_ = 11;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_63;
			case 1:
				goto IL_EA;
			case 2:
				goto IL_AB;
			case 3:
				goto IL_CF;
			case 4:
			{
				int num2;
				int count;
				if (num2 >= count)
				{
					num = 3;
					continue;
				}
				XlsPivotCacheField a_2 = A_1.ᜀ(num2);
				spr\u2171.ᜁ(A_0, a_2, A_2);
				num2++;
				num = 5;
				continue;
			}
			case 5:
				goto IL_AB;
			case 7:
			{
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				int count = A_1.Count;
				A_0.WriteStartElement(RecordTableEnumerator.b("≀≂♄⽆ⱈൊ⑌⩎㵐㝒♔", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("≀ⱂい⥆㵈", a_), count.ToString());
				int num2 = 0;
				num = 2;
				continue;
			}
			}
			if (A_0 != null)
			{
				num = 7;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_CF;
			default:
				if (false)
				{
				}
				num = 0;
				continue;
			}
			IL_AB:
			num = 4;
		}
		IL_63:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙀ㅂⱄ㍆ⱈ㥊", a_));
		IL_CF:
		A_0.WriteEndElement();
		return;
		IL_EA:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("❀⩂⁄⭆ⵈ㡊์⁎㵐㽒ご㑖ⵘ㉚㉜ㅞ", a_));
	}

	// Token: 0x06004865 RID: 18533 RVA: 0x002BB18C File Offset: 0x002BA18C
	private static void ᜁ(XmlWriter A_0, XlsPivotCacheField A_1, bool A_2)
	{
		int a_ = 1;
		int num = 25;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_24F;
			case 1:
				A_0.WriteAttributeString(RecordTableEnumerator.b("弶倸帺似帾㍀⁂ⵄ㹆", a_), A_1.Hierarchy.ToString());
				num = 18;
				continue;
			case 2:
				goto IL_3CC;
			case 3:
				if (A_1.Caption != null)
				{
					num = 13;
					continue;
				}
				goto IL_3CC;
			case 4:
				spr\u2171.ᜀ(A_0, A_1, A_1.ParentFeildGroupIndex);
				num = 14;
				continue;
			case 5:
				goto IL_26B;
			case 6:
				goto IL_176;
			case 7:
				if (true)
				{
				}
				goto IL_176;
			case 8:
				if (A_1.Level > 0)
				{
					num = 26;
					continue;
				}
				goto IL_28D;
			case 9:
				A_0.WriteAttributeString(RecordTableEnumerator.b("儶嘸䤺值䨾ⵀ≂", a_), A_1.Formula);
				num = 24;
				continue;
			case 10:
				goto IL_A5;
			case 11:
				goto IL_AA;
			case 12:
				goto IL_1AD;
			case 13:
				A_0.WriteAttributeString(RecordTableEnumerator.b("吶堸䬺䤼嘾⹀ⵂ", a_), A_1.Caption);
				num = 2;
				continue;
			case 14:
				goto IL_288;
			case 15:
				if (A_1.Hierarchy > 0)
				{
					num = 1;
					continue;
				}
				goto IL_2E0;
			case 16:
				if (A_1.IsFieldGroup)
				{
					num = 0;
					continue;
				}
				num = 19;
				continue;
			case 17:
				if (A_1 == null)
				{
					num = 23;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("吶堸堺唼娾݀⩂⁄⭆ⵈ", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("夶堸嘺堼", a_), A_1.Name);
				A_0.WriteAttributeString(RecordTableEnumerator.b("夶䰸嘺笼刾㕀ੂ⅄", a_), A_1.NumFormatIndex.ToString());
				num = 15;
				continue;
			case 18:
				goto IL_2E0;
			case 19:
				if (A_1.ParentFeildGroupIndex != -1)
				{
					num = 4;
					continue;
				}
				goto IL_3EF;
			case 20:
				goto IL_28D;
			case 21:
				if (!A_1.FieldGroup.ᜊ())
				{
					num = 12;
					continue;
				}
				goto IL_AA;
			case 22:
				if (A_1.IsFormulaField)
				{
					num = 11;
					continue;
				}
				spr\u2171.ᜀ(A_0, A_1, A_2);
				num = 7;
				continue;
			case 23:
				goto IL_320;
			case 24:
				goto IL_1F7;
			case 26:
				A_0.WriteAttributeString(RecordTableEnumerator.b("嬶尸䴺堼匾", a_), A_1.Level.ToString());
				num = 20;
				continue;
			case 27:
				if (A_1.IsFieldGroup)
				{
					num = 29;
					continue;
				}
				goto IL_1AD;
			case 28:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_24F;
				default:
					if (false)
					{
					}
					if (A_1.IsFormulaField)
					{
						num = 9;
						continue;
					}
					goto IL_1F7;
				}
				break;
			case 29:
				num = 21;
				continue;
			}
			if (A_0 == null)
			{
				num = 10;
				continue;
			}
			num = 17;
			continue;
			IL_AA:
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("匶堸伺尼崾⁀あ⁄ņ⁈⹊⅌⭎", a_), A_1.IsDataBaseField, true);
			num = 6;
			continue;
			IL_176:
			num = 16;
			continue;
			IL_1AD:
			num = 22;
			continue;
			IL_1F7:
			num = 3;
			continue;
			IL_24F:
			spr\u2171.ᜀ(A_0, A_1);
			num = 5;
			continue;
			IL_28D:
			num = 28;
			continue;
			IL_2E0:
			num = 8;
			continue;
			IL_3CC:
			num = 27;
		}
		IL_A5:
		throw new ArgumentNullException(RecordTableEnumerator.b("䀶䬸刺䤼娾㍀", a_));
		IL_26B:
		IL_288:
		goto IL_3EF;
		IL_320:
		throw new ArgumentNullException(RecordTableEnumerator.b("儶倸帺儼嬾", a_));
		IL_3EF:
		A_0.WriteEndElement();
	}

	// Token: 0x06004866 RID: 18534 RVA: 0x002BB590 File Offset: 0x002BA590
	private static void ᜀ(XmlWriter A_0, XlsPivotCacheField A_1, bool A_2)
	{
		int a_ = 1;
		switch (0)
		{
		default:
			for (;;)
			{
				IList<object> list = A_1.Items;
				A_0.WriteStartElement(RecordTableEnumerator.b("䐶儸娺似娾╀ੂㅄ≆⑈㡊", a_));
				spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("吶嘸为匼䬾", a_), A_1.ItemCount, 0);
				PivotDataType dataType = A_1.DataType;
				bool flag = (dataType & PivotDataType.String) != (PivotDataType)0;
				int num = 19;
				for (;;)
				{
					bool flag2;
					bool flag3;
					bool flag4;
					bool flag5;
					bool flag6;
					bool flag7;
					bool flag8;
					bool flag10;
					bool flag11;
					bool? flag12;
					int num2;
					int count;
					List<int> list2;
					switch (num)
					{
					case 0:
						goto IL_45D;
					case 1:
						spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("嬶嘸唺娼款⑀㭂ㅄ", a_), flag2, false);
						flag3 = true;
						num = 44;
						continue;
					case 2:
						if (!flag4)
						{
							num = 53;
							continue;
						}
						goto IL_45D;
					case 3:
						goto IL_882;
					case 4:
						goto IL_367;
					case 5:
						goto IL_866;
					case 6:
						if (true)
						{
						}
						num = 63;
						continue;
					case 7:
						goto IL_3A4;
					case 8:
						flag5 = false;
						goto IL_380;
					case 9:
						spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("吶嘸唺䤼帾⡀ⵂ㙄੆⁈㍊⡌⭎Ր⩒╔㉖⩘", a_), flag4, false);
						num = 16;
						continue;
					case 10:
						goto IL_882;
					case 11:
						num = 22;
						continue;
					case 12:
						goto IL_3A4;
					case 13:
						if (!flag3)
						{
							num = 5;
							continue;
						}
						goto IL_8A6;
					case 14:
						if ((dataType & (PivotDataType.Number | PivotDataType.Date)) != (PivotDataType)0)
						{
							num = 35;
							continue;
						}
						spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("吶嘸唺䤼帾⡀ⵂ㙄Ն╈⩊⍌⑎", a_), flag6, false);
						num = 60;
						continue;
					case 15:
						if (!flag)
						{
							num = 23;
							continue;
						}
						goto IL_493;
					case 16:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_5B2;
						default:
							if (false)
							{
							}
							if (!flag4)
							{
								num = 26;
								continue;
							}
							flag3 = false;
							num = 12;
							continue;
						}
						break;
					case 17:
						num = 50;
						continue;
					case 18:
						flag7 = true;
						goto IL_635;
					case 19:
						if ((dataType & PivotDataType.Integer) != (PivotDataType)0)
						{
							num = 31;
							continue;
						}
						num = 39;
						continue;
					case 20:
						if (!flag8)
						{
							num = 9;
							continue;
						}
						num = 40;
						continue;
					case 21:
					{
						bool? flag9 = A_1.IsParsed;
						num = 55;
						continue;
					}
					case 22:
						flag7 = flag;
						goto IL_635;
					case 23:
						goto IL_241;
					case 24:
						if (flag10)
						{
							num = 30;
							continue;
						}
						goto IL_241;
					case 25:
						if (!flag6)
						{
							num = 17;
							continue;
						}
						goto IL_45D;
					case 26:
						spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("吶嘸唺䤼帾⡀ⵂ㙄ᑆ㵈㥊⑌ⅎ㙐", a_), flag, !flag);
						num = 7;
						continue;
					case 27:
						goto IL_6FB;
					case 28:
						if (flag6)
						{
							num = 38;
							continue;
						}
						goto IL_4DF;
					case 29:
						goto IL_4DF;
					case 30:
						num = 15;
						continue;
					case 31:
						num = 41;
						continue;
					case 32:
						if (flag)
						{
							num = 48;
							continue;
						}
						goto IL_2AE;
					case 33:
						goto IL_346;
					case 34:
					{
						bool? flag9;
						flag5 = (flag9 != null);
						goto IL_380;
					}
					case 35:
						num = 25;
						continue;
					case 36:
						goto IL_71C;
					case 37:
						if (flag2)
						{
							num = 1;
							continue;
						}
						goto IL_74E;
					case 38:
						flag3 = false;
						num = 29;
						continue;
					case 39:
						flag11 = false;
						goto IL_813;
					case 40:
						if (flag8)
						{
							num = 54;
							continue;
						}
						goto IL_2AE;
					case 41:
						flag11 = ((dataType & PivotDataType.Float) == (PivotDataType)0);
						goto IL_813;
					case 42:
						num = 43;
						continue;
					case 43:
						if (!flag8)
						{
							num = 27;
							continue;
						}
						goto IL_493;
					case 44:
						goto IL_74E;
					case 45:
						num = 62;
						continue;
					case 46:
						if (flag12 != null)
						{
							num = 21;
							continue;
						}
						goto IL_71C;
					case 47:
					{
						if (num2 >= count)
						{
							num = 4;
							continue;
						}
						bool a_2 = list.Contains(num2);
						object a_3 = list[num2];
						spr\u2171.ᜀ(A_0, a_3, a_2);
						num2++;
						num = 61;
						continue;
					}
					case 48:
						spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("吶嘸唺䤼帾⡀ⵂ㙄͆⡈㽊⡌", a_), flag8, !flag8);
						spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("吶嘸唺䤼帾⡀ⵂ㙄੆⁈㍊⡌⭎Ր⩒╔㉖⩘", a_), flag4, false);
						spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("吶嘸唺䤼帾⡀ⵂ㙄Ն╈⩊⍌⑎", a_), flag6, false);
						flag3 = false;
						num = 3;
						continue;
					case 49:
						if (dataType != (PivotDataType.Number | PivotDataType.Integer | PivotDataType.Blank))
						{
							num = 6;
							continue;
						}
						goto IL_45D;
					case 50:
						if (dataType != (PivotDataType.Blank | PivotDataType.Date))
						{
							num = 45;
							continue;
						}
						goto IL_45D;
					case 51:
						num = 49;
						continue;
					case 52:
						goto IL_882;
					case 53:
						A_0.WriteAttributeString(RecordTableEnumerator.b("吶嘸唺䤼帾⡀ⵂ㙄ᑆⱈ♊⑌Ɏ㡐⭒ご㍖൘≚ⵜ㩞በ", a_), RecordTableEnumerator.b("ܶ", a_));
						flag3 = false;
						num = 0;
						continue;
					case 54:
						num = 32;
						continue;
					case 55:
					{
						bool? flag9;
						if (flag9.GetValueOrDefault())
						{
							goto IL_5B2;
						}
						num = 8;
						continue;
					}
					case 56:
						flag3 = false;
						num = 36;
						continue;
					case 57:
						flag7 = false;
						goto IL_635;
					case 58:
						num = 34;
						continue;
					case 59:
						if (list2.Count <= 0)
						{
							num = 66;
							continue;
						}
						goto IL_866;
					case 60:
						goto IL_882;
					case 61:
						goto IL_346;
					case 62:
						if (dataType != (PivotDataType.Number | PivotDataType.Blank))
						{
							num = 51;
							continue;
						}
						goto IL_45D;
					case 63:
						if (dataType != (PivotDataType.Number | PivotDataType.Integer | PivotDataType.Blank | PivotDataType.Float))
						{
							num = 67;
							continue;
						}
						goto IL_45D;
					case 64:
						if (flag8)
						{
							num = 11;
							continue;
						}
						num = 57;
						continue;
					case 65:
						if (flag10)
						{
							num = 42;
							continue;
						}
						goto IL_6FB;
					case 66:
						num = 13;
						continue;
					case 67:
						num = 2;
						continue;
					case 68:
						goto IL_71C;
					}
					break;
					IL_241:
					num = 65;
					continue;
					IL_2AE:
					bool flag13;
					spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("吶嘸唺䤼帾⡀ⵂ㙄ॆ♈╊ौ⹎═㙒", a_), flag13, !flag13);
					spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("吶嘸唺䤼帾⡀ⵂ㙄͆⡈㽊⡌", a_), flag8, !flag8);
					spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("吶嘸唺䤼帾⡀ⵂ㙄ᑆ㵈㥊⑌ⅎ㙐", a_), flag, !flag);
					spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("吶嘸唺䤼帾⡀ⵂ㙄Ն╈⩊⍌⑎", a_), flag6, false);
					flag3 = false;
					num = 10;
					continue;
					IL_346:
					num = 47;
					continue;
					IL_380:
					if (flag5)
					{
						num = 56;
						continue;
					}
					flag3 = true;
					num = 68;
					continue;
					IL_3A4:
					spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("吶嘸唺䤼帾⡀ⵂ㙄Ն╈⩊⍌⑎", a_), flag6, false);
					spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("吶嘸唺䤼帾⡀ⵂ㙄ॆ㱈♊⽌⩎⍐", a_), flag10, !flag10);
					bool flag14;
					spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("吶嘸唺䤼帾⡀ⵂ㙄ๆ❈㽊⡌⡎㑐⅒", a_), flag14, !flag14);
					num = 52;
					continue;
					IL_45D:
					num = 20;
					continue;
					IL_493:
					num = 18;
					continue;
					IL_4DF:
					num = 37;
					continue;
					IL_5B2:
					num = 58;
					continue;
					IL_635:
					flag4 = flag7;
					flag3 = false;
					flag2 = ((dataType & PivotDataType.LongText) != (PivotDataType)0);
					num = 14;
					continue;
					IL_6FB:
					num = 64;
					continue;
					IL_71C:
					list2 = spr\u2171.ᜀ(A_1);
					num = 59;
					continue;
					IL_74E:
					flag12 = A_1.IsParsed;
					num = 46;
					continue;
					IL_813:
					flag14 = flag11;
					flag10 = ((dataType & PivotDataType.Number) != (PivotDataType)0);
					flag8 = ((dataType & PivotDataType.Date) != (PivotDataType)0);
					flag13 = ((dataType & ~(PivotDataType.Blank | PivotDataType.Date)) != (PivotDataType)0);
					flag6 = ((dataType & PivotDataType.Blank) != (PivotDataType)0);
					num = 24;
					continue;
					IL_866:
					num2 = 0;
					count = list.Count;
					num = 33;
					continue;
					IL_882:
					num = 28;
				}
			}
			IL_367:
			IL_8A6:
			A_0.WriteEndElement();
			return;
		}
	}

	// Token: 0x06004867 RID: 18535 RVA: 0x002BBE4C File Offset: 0x002BAE4C
	private static void ᜁ(XmlWriter A_0, XlsPivotCache A_1, RelationsCollection A_2)
	{
		int a_ = 2;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				if (true)
				{
				}
				if (A_1 == null)
				{
					num = 7;
					continue;
				}
				DataSourceType sourceType = A_1.SourceType;
				num = 5;
				continue;
			}
			case 1:
				goto IL_B6;
			case 2:
				return;
			case 3:
			{
				DataSourceType sourceType;
				if (sourceType != DataSourceType.ScenarioPivotTable)
				{
					num = 2;
					continue;
				}
				goto IL_7F;
			}
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_12C;
				default:
				{
					if (false)
					{
					}
					DataSourceType sourceType;
					switch (sourceType)
					{
					case DataSourceType.Worksheet:
						goto IL_9B;
					case DataSourceType.ExternalData:
						spr\u2171.ᜅ(A_0, A_1);
						num = 1;
						continue;
					case (DataSourceType)3:
						return;
					case DataSourceType.Consolidation:
						goto IL_4A;
					default:
						num = 8;
						continue;
					}
					break;
				}
				}
				break;
			case 6:
				goto IL_48;
			case 7:
				goto IL_7A;
			case 8:
				goto IL_12C;
			}
			if (A_0 == null)
			{
				num = 6;
				continue;
			}
			num = 0;
			continue;
			IL_12C:
			num = 3;
		}
		IL_48:
		throw new ArgumentNullException(RecordTableEnumerator.b("伷䠹唻䨽┿ぁ", a_));
		IL_4A:
		throw new NotImplementedException(RecordTableEnumerator.b("笷唹刻䴽⼿⹁ⵃ≅⥇㹉╋⅍㹏牑ѓ㽕⹗㕙⡛繝͟͡ݣ๥൧", a_));
		IL_7A:
		throw new ArgumentNullException(RecordTableEnumerator.b("嬷嬹弻嘽┿", a_));
		IL_7F:
		spr\u2171.ᜆ(A_0, A_1);
		return;
		IL_9B:
		spr\u2171.ᜀ(A_0, A_1, A_2);
		return;
		IL_B6:;
	}

	// Token: 0x06004868 RID: 18536 RVA: 0x002BBFA8 File Offset: 0x002BAFA8
	private static void ᜆ(XmlWriter A_0, XlsPivotCache A_1)
	{
		int a_ = 15;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_58;
			case 1:
				goto IL_80;
			case 3:
				goto IL_8B;
			}
			if (A_0 != null)
			{
				num = 1;
				continue;
			}
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
				num = 0;
				continue;
			}
			IL_80:
			if (A_1 != null)
			{
				goto IL_A1;
			}
			num = 3;
		}
		IL_58:
		throw new ArgumentNullException(RecordTableEnumerator.b("㉄㕆⁈㽊⡌㵎", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("♄♆⩈⍊⡌", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("♄♆⩈⍊⡌ᱎ㹐♒❔㑖㱘", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("ㅄ㹆㥈⹊", a_), RecordTableEnumerator.b("㙄⑆ⱈ╊ⱌ㵎㡐㱒", a_));
		A_0.WriteEndElement();
	}

	// Token: 0x06004869 RID: 18537 RVA: 0x002BC094 File Offset: 0x002BB094
	private static void ᜀ(XmlWriter A_0, XlsPivotCache A_1, RelationsCollection A_2)
	{
		int a_ = 13;
		switch (0)
		{
		default:
			for (;;)
			{
				A_0.WriteStartElement(RecordTableEnumerator.b("⁂⑄⑆ⅈ⹊Ṍ⁎⑐⅒㙔㉖", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("㝂㱄㝆ⱈ", a_), RecordTableEnumerator.b("㑂⩄㕆≈㡊╌⩎㑐❒", a_));
				int num = 11;
				for (;;)
				{
					string text;
					spr\u20A6 spr_u20A;
					IXLSRange sourceRange;
					switch (num)
					{
					case 0:
						goto IL_22E;
					case 1:
						A_0.WriteAttributeString(RecordTableEnumerator.b("⩂⅄", a_), RecordTableEnumerator.b("⭂ㅄ㍆㥈煊扌恎≐げ㵔㉖㑘㩚⹜煞๠።d०ᅨ٪Ŭ८ṰŲᡴᙶ൸ࡺ卼ၾꪄ힒杖햠貢鞤鞦馨鶪芬\uddae풰\udfb2풴쎶킸풺펼첾꧀ꫂ뗄듆", a_), text);
						num = 12;
						continue;
					case 2:
						goto IL_C3;
					case 3:
						goto IL_C3;
					case 4:
						goto IL_230;
					case 5:
						if (spr_u20A != null)
						{
							num = 8;
							continue;
						}
						goto IL_230;
					case 6:
						A_0.WriteAttributeString(RecordTableEnumerator.b("ⵂ⑄⩆ⱈ", a_), A_1.RangeName);
						num = 2;
						continue;
					case 7:
						if (!A_1.HasNamedRange)
						{
							if (true)
							{
							}
							A_0.WriteAttributeString(RecordTableEnumerator.b("ㅂ⁄ⅆ", a_), sourceRange.RangeAddressLocal);
							A_0.WriteAttributeString(RecordTableEnumerator.b("あⵄ≆ⱈ㽊", a_), (sourceRange as ICombinedRange).WorksheetName);
							num = 3;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_10B;
						default:
							if (false)
							{
							}
							num = 6;
							continue;
						}
						break;
					case 8:
						text = spr\u2171.ᜀ(spr_u20A, A_2);
						num = 4;
						continue;
					case 9:
					{
						Stream stream = A_1.PreservedElements[RecordTableEnumerator.b("㑂⩄㕆≈㡊╌⩎㑐❒ٔ㡖ⱘ⥚㹜㩞", a_)];
						stream.Position = 0L;
						ShapeParser.WriteNodeFromStream(A_0, stream);
						num = 13;
						continue;
					}
					case 10:
						if (text != null)
						{
							num = 1;
							continue;
						}
						goto IL_21C;
					case 11:
						if (A_1.PreservedElements.ContainsKey(RecordTableEnumerator.b("㑂⩄㕆≈㡊╌⩎㑐❒ٔ㡖ⱘ⥚㹜㩞", a_)))
						{
							num = 9;
							continue;
						}
						goto IL_10B;
					case 12:
						goto IL_21C;
					case 13:
						goto IL_1C5;
					}
					break;
					IL_C3:
					num = 10;
					continue;
					IL_10B:
					A_0.WriteStartElement(RecordTableEnumerator.b("㑂⩄㕆≈㡊╌⩎㑐❒ٔ㡖ⱘ⥚㹜㩞", a_));
					sourceRange = A_1.SourceRange;
					spr_u20A = (sourceRange as spr\u20A6);
					text = null;
					num = 5;
					continue;
					IL_21C:
					A_0.WriteEndElement();
					num = 0;
					continue;
					IL_230:
					num = 7;
				}
			}
			IL_1C5:
			IL_22E:
			A_0.WriteEndElement();
			return;
		}
	}

	// Token: 0x0600486A RID: 18538 RVA: 0x002BC330 File Offset: 0x002BB330
	private static string ᜀ(spr\u20A6 A_0, RelationsCollection A_1)
	{
		int a_ = 6;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		string text = A_1.GenerateRelationId();
		string fileName = Path.GetFileName(A_0.ᝡ().Workbook.URL);
		A_1[text] = new sprᦨ(fileName, RecordTableEnumerator.b("吻䨽㐿㉁繃楅杇㥉⽋♍㕏㽑㕓╕癗㕙ⱛ㭝๟ᩡॣ੥๧թṫͭᅯٱݳ塵᝷ࡹ᭻兽좋煉뎛겝邟銡銣覥\udaa7쾩삫쾭쒯\udbb1\udbb3\ud8b5쮷특햻캽뎿ꇃ뻅볇꿉뻋ꃍ뇏뻑飓뿕뛗뇙賛뿝铟諡", a_), true);
		return text;
	}

	// Token: 0x0600486B RID: 18539 RVA: 0x002BC3B0 File Offset: 0x002BB3B0
	public static void ᜅ(XmlWriter A_0, XlsPivotCache A_1)
	{
		int a_ = 14;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					num = 6;
					continue;
				}
				num = 1;
				continue;
			case 1:
				if (A_1.PreservedElements.ContainsKey(RecordTableEnumerator.b("⅃㹅㱇⽉㹋⁍ㅏ㹑", a_)))
				{
					num = 5;
					continue;
				}
				return;
			case 2:
				goto IL_4D;
			case 4:
				goto IL_DB;
			case 5:
			{
				Stream stream = A_1.PreservedElements[RecordTableEnumerator.b("⅃㹅㱇⽉㹋⁍ㅏ㹑", a_)];
				stream.Position = 0L;
				ShapeParser.WriteNodeFromStream(A_0, stream);
				num = 4;
				continue;
			}
			case 6:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_117;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 2;
			}
			else
			{
				num = 0;
			}
		}
		IL_4D:
		throw new ArgumentNullException(RecordTableEnumerator.b("㍃㑅ⅇ㹉⥋㱍", a_));
		IL_DB:
		return;
		IL_117:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("❃❅⭇≉⥋", a_));
	}

	// Token: 0x0600486C RID: 18540 RVA: 0x002BC4E0 File Offset: 0x002BB4E0
	public static void ᜀ(XmlWriter A_0, XlsPivotCache A_1, MemoryStream A_2)
	{
		int a_ = 1;
		for (;;)
		{
			IL_09:
			switch (0)
			{
			default:
			{
				int num = 2;
				for (;;)
				{
					if (true)
					{
					}
					int num4;
					switch (num)
					{
					case 0:
						goto IL_12A;
					case 1:
						goto IL_16A;
					case 3:
					{
						int num2;
						int num3;
						if (num2 >= num3)
						{
							num = 14;
							continue;
						}
						object a_2 = A_1.ᜀ(num2, num4);
						spr\u2171.ᜀ(A_0, a_2, false);
						num2++;
						num = 4;
						continue;
					}
					case 4:
						goto IL_1C3;
					case 5:
						goto IL_125;
					case 6:
						A_2.SetLength(2000L);
						num = 0;
						continue;
					case 7:
						if (A_2.Length >= (long)(A_2.Capacity - 2000))
						{
							num = 6;
							continue;
						}
						goto IL_12A;
					case 8:
						goto IL_1C3;
					case 9:
						goto IL_83;
					case 10:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_09;
						default:
						{
							if (false)
							{
							}
							if (A_1 == null)
							{
								num = 5;
								continue;
							}
							A_0.WriteStartElement(RecordTableEnumerator.b("䜶倸䴺刼䬾ɀ≂♄⽆ⱈ᥊⡌ⱎ㹐⅒ㅔ⑖", a_), RecordTableEnumerator.b("弶䴸伺䴼Ծ湀求㙄⑆ⅈ⹊⁌⹎≐絒㩔❖㱘㕚╜㉞ൠբ੤ᕦѨ੪ᥬᱮ彰ᱲݴၶ噸ࡺർൾﮎﲐﾒ몔ꖖꦘꮚꮜ낞철슢첤즦", a_));
							A_1.CacheFields;
							int num3 = A_1.CacheFields.ᜀ();
							num4 = 0;
							int recordCount = A_1.RecordCount;
							num = 13;
							continue;
						}
						}
						break;
					case 11:
					{
						int recordCount;
						if (num4 >= recordCount)
						{
							num = 12;
							continue;
						}
						A_0.WriteStartElement(RecordTableEnumerator.b("䔶", a_));
						int num2 = 0;
						num = 8;
						continue;
					}
					case 12:
						goto IL_186;
					case 13:
						goto IL_16A;
					case 14:
						A_0.WriteEndElement();
						A_0.Flush();
						num = 7;
						continue;
					}
					if (A_0 == null)
					{
						num = 9;
						continue;
					}
					num = 10;
					continue;
					IL_12A:
					num4++;
					num = 1;
					continue;
					IL_16A:
					num = 11;
					continue;
					IL_1C3:
					num = 3;
				}
				break;
			}
			}
		}
		IL_83:
		throw new ArgumentNullException(RecordTableEnumerator.b("䀶䬸刺䤼娾㍀", a_));
		IL_125:
		throw new ArgumentNullException(RecordTableEnumerator.b("吶堸堺唼娾", a_));
		IL_186:
		A_0.WriteEndElement();
	}

	// Token: 0x0600486D RID: 18541 RVA: 0x002BC72C File Offset: 0x002BB72C
	private static void ᜀ(XmlWriter A_0, object A_1, bool A_2)
	{
		int a_ = 8;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				for (;;)
				{
					string localName = null;
					string text = null;
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							localName = RecordTableEnumerator.b("嬽", a_);
							ushort a_2 = (ushort)A_1;
							text = spr\u2171.ᜀ(a_2);
							num = 17;
							continue;
						}
						case 1:
							if (A_1 is bool)
							{
								num = 2;
								continue;
							}
							num = 6;
							continue;
						case 2:
							localName = RecordTableEnumerator.b("尽", a_);
							text = XmlConvert.ToString((bool)A_1);
							num = 19;
							continue;
						case 3:
							goto IL_23F;
						case 4:
							if (A_1 is double)
							{
								num = 14;
								continue;
							}
							num = 21;
							continue;
						case 5:
							if (text != null)
							{
								num = 18;
								continue;
							}
							goto IL_212;
						case 6:
							if (A_1 is ushort)
							{
								num = 0;
								continue;
							}
							num = 13;
							continue;
						case 7:
							text = (string)A_1;
							localName = RecordTableEnumerator.b("䴽", a_);
							num = 3;
							continue;
						case 8:
							goto IL_23F;
						case 9:
							goto IL_23F;
						case 10:
							text = null;
							localName = RecordTableEnumerator.b("匽", a_);
							num = 23;
							continue;
						case 11:
							if (A_2)
							{
								num = 12;
								continue;
							}
							goto IL_343;
						case 12:
							A_0.WriteAttributeString(RecordTableEnumerator.b("堽", a_), RecordTableEnumerator.b("༽", a_));
							num = 15;
							continue;
						case 13:
							if (A_1 == null)
							{
								num = 10;
								continue;
							}
							goto IL_C5;
						case 14:
							localName = RecordTableEnumerator.b("倽", a_);
							text = XmlConvert.ToString((double)A_1);
							num = 8;
							continue;
						case 15:
							goto IL_1C1;
						case 16:
							if (A_1 is DateTime)
							{
								num = 22;
								continue;
							}
							num = 1;
							continue;
						case 17:
							goto IL_23F;
						case 18:
							A_0.WriteAttributeString(RecordTableEnumerator.b("䠽", a_), text);
							num = 20;
							continue;
						case 19:
							goto IL_23F;
						case 20:
							if (true)
							{
							}
							goto IL_212;
						case 21:
							if (A_1 is string)
							{
								num = 7;
								continue;
							}
							num = 16;
							continue;
						case 22:
							localName = RecordTableEnumerator.b("娽", a_);
							text = ((DateTime)A_1).ToString(RecordTableEnumerator.b("䜽㤿㭁㵃歅Շ݉態⩍㑏๑Sṕၗ恙ㅛ㍝婟ᅡᝣ", a_));
							num = 9;
							continue;
						case 23:
							goto IL_23F;
						}
						break;
						IL_212:
						num = 11;
						continue;
						IL_23F:
						A_0.WriteStartElement(localName);
						num = 5;
					}
				}
				IL_C5:
				throw new NotImplementedException();
				IL_1C1:
				break;
			}
			break;
		}
		IL_343:
		A_0.WriteEndElement();
	}

	// Token: 0x0600486E RID: 18542 RVA: 0x002BCA84 File Offset: 0x002BBA84
	private static void ᜀ(XmlWriter A_0, sprᾷ A_1)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					try
					{
						num = 8;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								XlsPivotCacheField xlsPivotCacheField;
								if (xlsPivotCacheField.CalculatedItems != null)
								{
									num = 4;
									continue;
								}
								goto IL_20A;
							}
							case 1:
								num = 5;
								continue;
							case 2:
							{
								XlsPivotCacheField xlsPivotCacheField;
								List<spr\u23FD> list;
								list.Add(xlsPivotCacheField.CalculatedItems);
								num = 6;
								continue;
							}
							case 3:
							{
								IEnumerator<XlsPivotCacheField> enumerator;
								if (!enumerator.MoveNext())
								{
									num = 1;
									continue;
								}
								XlsPivotCacheField xlsPivotCacheField = enumerator.Current;
								num = 0;
								continue;
							}
							case 4:
								num = 7;
								continue;
							case 5:
								goto IL_238;
							case 6:
								goto IL_20A;
							case 7:
							{
								XlsPivotCacheField xlsPivotCacheField;
								if (xlsPivotCacheField.CalculatedItems.Count > 0)
								{
									goto IL_1E2;
								}
								goto IL_20A;
							}
							}
							goto IL_18A;
							IL_1E2:
							num = 2;
							continue;
							IL_18A:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1E2;
							default:
								if (false)
								{
								}
								break;
							}
							IL_20A:
							num = 3;
						}
						IL_238:
						goto IL_28C;
					}
					finally
					{
						num = 1;
						for (;;)
						{
							IEnumerator<XlsPivotCacheField> enumerator;
							switch (num)
							{
							case 0:
								enumerator.Dispose();
								num = 2;
								continue;
							case 2:
								goto IL_275;
							}
							if (enumerator == null)
							{
								break;
							}
							num = 0;
						}
						IL_275:;
					}
					goto IL_278;
					IL_28C:
					num = 3;
					continue;
				case 1:
					goto IL_14F;
				case 2:
				{
					if (A_1 == null)
					{
						num = 8;
						continue;
					}
					List<spr\u23FD> list = new List<spr\u23FD>();
					IEnumerator<XlsPivotCacheField> enumerator = A_1.GetEnumerator();
					num = 0;
					continue;
				}
				case 3:
				{
					List<spr\u23FD> list;
					if (list.Count > 0)
					{
						num = 4;
						continue;
					}
					return;
				}
				case 4:
				{
					if (true)
					{
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("╅⥇♉⽋㭍㱏㍑⁓㍕㱗ፙ⡛㭝ൟᅡ", a_));
					List<spr\u23FD> list;
					List<spr\u23FD>.Enumerator enumerator2 = list.GetEnumerator();
					num = 7;
					continue;
				}
				case 6:
					goto IL_59;
				case 7:
					try
					{
						num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_12D;
							case 3:
								num = 0;
								continue;
							case 4:
							{
								List<spr\u23FD>.Enumerator enumerator2;
								if (!enumerator2.MoveNext())
								{
									num = 3;
									continue;
								}
								spr\u23FD a_2 = enumerator2.Current;
								spr\u2171.ᜀ(A_0, a_2);
								num = 1;
								continue;
							}
							}
							IL_107:
							num = 4;
							continue;
							goto IL_107;
						}
						IL_12D:;
					}
					finally
					{
						List<spr\u23FD>.Enumerator enumerator2;
						((IDisposable)enumerator2).Dispose();
					}
					A_0.WriteEndElement();
					num = 1;
					continue;
				case 8:
					goto IL_A2;
				}
				if (A_0 == null)
				{
					num = 6;
				}
				else
				{
					num = 2;
				}
			}
			IL_59:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅅ㩇⍉㡋⭍≏", a_));
			IL_A2:
			goto IL_278;
			IL_14F:
			return;
			IL_278:
			throw new ArgumentNullException(RecordTableEnumerator.b("⁅ⅇ⽉⁋⩍⍏", a_));
		}
		}
	}

	// Token: 0x0600486F RID: 18543 RVA: 0x002BCD80 File Offset: 0x002BBD80
	private static void ᜀ(XmlWriter A_0, spr\u23FD A_1)
	{
		int a_ = 16;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				if (A_1.Count == 0)
				{
					num = 5;
					continue;
				}
				List<sprỺ>.Enumerator enumerator = A_1.GetEnumerator();
				if (true)
				{
				}
				num = 2;
				continue;
			}
			case 1:
				goto IL_72;
			case 2:
				try
				{
					num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 3;
							continue;
						case 2:
						{
							List<sprỺ>.Enumerator enumerator;
							if (!enumerator.MoveNext())
							{
								num = 0;
								continue;
							}
							sprỺ a_2 = enumerator.Current;
							spr\u2171.ᜀ(A_0, a_2);
							num = 1;
							continue;
						}
						case 3:
							goto IL_ED;
						}
						IL_CA:
						num = 2;
						continue;
						goto IL_CA;
					}
					IL_ED:
					goto IL_141;
				}
				finally
				{
					List<sprỺ>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				goto IL_FD;
				IL_141:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_FD;
				default:
					goto IL_157;
				}
				break;
			case 3:
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				goto IL_FD;
			case 5:
				return;
			case 6:
				goto IL_40;
			}
			if (A_0 == null)
			{
				num = 6;
				continue;
			}
			num = 3;
			continue;
			IL_FD:
			num = 0;
		}
		IL_40:
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅅ㩇⍉㡋⭍≏", a_));
		IL_72:
		throw new ArgumentNullException(RecordTableEnumerator.b("╅⥇⥉⑋⭍", a_));
		IL_157:
		if (false)
		{
		}
	}

	// Token: 0x06004870 RID: 18544 RVA: 0x002BCEFC File Offset: 0x002BBEFC
	private static void ᜀ(XmlWriter A_0, sprỺ A_1)
	{
		int a_ = 13;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		A_0.WriteStartElement(RecordTableEnumerator.b("⁂⑄⭆⩈㹊⅌⹎═㙒ㅔṖⵘ㹚ぜ", a_));
		spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("╂ⱄ≆╈⽊", a_), A_1.ᜁ(), -1);
		spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("╂⩄㕆⑈㹊⅌⹎", a_), A_1.ᜂ(), null);
		spr\u2171.ᜀ(A_0, A_1.ᜀ());
		A_0.WriteEndElement();
	}

	// Token: 0x06004871 RID: 18545 RVA: 0x002BCF9C File Offset: 0x002BBF9C
	private static void ᜀ(XmlWriter A_0, spr\u25B2 A_1)
	{
		int a_ = 6;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		A_0.WriteStartElement(RecordTableEnumerator.b("䰻圽㘿ⵁぃ݅㩇⽉ⵋ", a_));
		spr\u2171.ᜀ(A_0, RecordTableEnumerator.b("崻䘽⤿ㅁ", a_), A_1.ᜈ(), AxisTypes.None);
		spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("弻弽⌿⩁⅃ཅ♇⹉⥋㙍", a_), A_1.\u170D(), false);
		spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("堻弽㐿⍁ୃ⡅⑇㍉", a_), A_1.ᜂ(), false);
		spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("娻圽┿⹁⁃", a_), A_1.ᜀ(), -1);
		spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("娻圽┿⹁⁃ᙅ❇㥉╋㩍㥏㵑㩓", a_), A_1.ᜆ(), -1);
		spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("弻儽ⰿՁ㙃❅♇⹉ᡋ⅍⑏㍑㡓╕", a_), A_1.ᜊ(), false);
		spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("嬻䰽ℿⱁ⁃ᑅ❇㵉", a_), A_1.ᜇ(), false);
		spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("倻弽∿❁⡃ॅ♇♉㕋", a_), A_1.ᜉ(), false);
		spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("医䬽㐿⹁ⵃ⡅ⵇ", a_), A_1.ᜃ(), true);
		spr\u2171.ᜀ(A_0, RecordTableEnumerator.b("崻䰽┿⍁၃㽅㡇⽉", a_), A_1.ᜌ(), PivotAreaType.None);
		spr\u2171.ᜀ(A_0, A_1.ᜁ());
		A_0.WriteEndElement();
	}

	// Token: 0x06004872 RID: 18546 RVA: 0x002BD128 File Offset: 0x002BC128
	private static void ᜀ(XmlWriter A_0, sprἔ A_1)
	{
		int a_ = 6;
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
			A_0.WriteStartElement(RecordTableEnumerator.b("主嬽☿❁㙃⍅♇⥉⥋㵍", a_));
			A_0.WriteAttributeString(RecordTableEnumerator.b("弻儽㔿ⱁぃ", a_), A_1.Count.ToString());
			List<sprᲹ>.Enumerator enumerator = A_1.GetEnumerator();
			try
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 2:
						num = 3;
						continue;
					case 3:
						goto IL_D0;
					case 4:
					{
						if (!enumerator.MoveNext())
						{
							num = 2;
							continue;
						}
						sprᲹ a_2 = enumerator.Current;
						spr\u2171.ᜀ(A_0, a_2);
						num = 0;
						continue;
					}
					}
					IL_AD:
					num = 4;
					continue;
					goto IL_AD;
				}
				IL_D0:;
			}
			finally
			{
				if (true)
				{
				}
				((IDisposable)enumerator).Dispose();
			}
			break;
		}
		}
		A_0.WriteEndElement();
	}

	// Token: 0x06004873 RID: 18547 RVA: 0x002BD234 File Offset: 0x002BC234
	private static void ᜀ(XmlWriter A_0, sprᲹ A_1)
	{
		int a_ = 17;
		for (;;)
		{
			A_0.WriteStartElement(RecordTableEnumerator.b("㕆ⱈⵊ⡌㵎㑐㵒㙔㉖", a_));
			spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("ⅆ⁈⹊⅌⭎", a_), A_1.ᜂ(), -1);
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					try
					{
						num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								num = 4;
								continue;
							case 1:
							{
								List<int>.Enumerator enumerator;
								if (!enumerator.MoveNext())
								{
									num = 0;
									continue;
								}
								int num2 = enumerator.Current;
								A_0.WriteStartElement(RecordTableEnumerator.b("㽆", a_));
								A_0.WriteAttributeString(RecordTableEnumerator.b("ㅆ", a_), num2.ToString());
								A_0.WriteEndElement();
								num = 3;
								continue;
							}
							case 4:
								goto IL_103;
							}
							IL_DD:
							num = 1;
							continue;
							goto IL_DD;
						}
						IL_103:
						goto IL_1C5;
					}
					finally
					{
						List<int>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					goto IL_116;
				case 1:
					IL_58:
					if (A_1.ᜄ() != SubtotalTypes.None)
					{
						num = 2;
						continue;
					}
					goto IL_116;
				case 2:
					spr\u2514.ᜂ(A_0, A_1.ᜄ());
					num = 3;
					continue;
				case 3:
					goto IL_116;
				}
				break;
				IL_116:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_58;
				default:
				{
					if (false)
					{
					}
					spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("╆えᭊ≌㱎㡐❒㱔㡖㝘", a_), A_1.ᜀ(), false);
					spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("㕆ⱈ❊ⱌ㭎㡐╒ご", a_), A_1.ᜆ(), false);
					spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("㑆ⱈ❊⡌ⱎ═㙒ㅔ", a_), A_1.ᜅ(), false);
					List<int>.Enumerator enumerator = A_1.ᜃ().GetEnumerator();
					num = 0;
					break;
				}
				}
			}
		}
		IL_1C5:
		if (true)
		{
		}
		A_0.WriteEndElement();
	}

	// Token: 0x06004874 RID: 18548 RVA: 0x002BD424 File Offset: 0x002BC424
	private static void ᜀ(XmlWriter A_0, XlsPivotCacheField A_1, int A_2)
	{
		int a_ = 12;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		A_0.WriteStartElement(RecordTableEnumerator.b("⑁ⵃ⍅⑇⹉ୋ㱍㽏❑⑓", a_));
		spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("㉁╃㑅", a_), A_2, -1);
		A_0.WriteEndElement();
	}

	// Token: 0x06004875 RID: 18549 RVA: 0x002BD498 File Offset: 0x002BC498
	private static void ᜀ(XmlWriter A_0, XlsPivotCacheField A_1)
	{
		int a_ = 16;
		for (;;)
		{
			spr\u1920 spr_u = A_1.FieldGroup;
			A_0.WriteStartElement(RecordTableEnumerator.b("⁅ⅇ⽉⁋⩍ᝏ⁑㭓⍕⡗", a_));
			spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("㙅⥇㡉", a_), spr_u.ᜑ(), -1);
			spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("⑅⥇㥉⥋", a_), spr_u.ᜈ(), -1);
			int num = 1;
			for (;;)
			{
				IL_0B:
				switch (num)
				{
				case 0:
					goto IL_F8;
				case 1:
					while (!spr_u.ᜊ())
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
							spr\u2171.ᜁ(A_0, spr_u);
							spr\u2171.ᜀ(A_0, spr_u.ᜎ());
							num = 3;
							goto IL_0B;
						}
					}
					num = 2;
					continue;
				case 2:
					spr\u2171.ᜀ(A_0, spr_u);
					spr\u2171.ᜀ(A_0, spr_u.ᜉ());
					num = 0;
					continue;
				case 3:
					goto IL_D8;
				}
				break;
			}
		}
		IL_D8:
		IL_F8:
		A_0.WriteEndElement();
	}

	// Token: 0x06004876 RID: 18550 RVA: 0x002BD5A8 File Offset: 0x002BC5A8
	private static void ᜀ(XmlWriter A_0, List<string> A_1)
	{
		int a_ = 1;
		int num = 0;
		for (;;)
		{
			List<string>.Enumerator enumerator;
			switch (num)
			{
			case 1:
				try
				{
					num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							if (!enumerator.MoveNext())
							{
								num = 3;
								continue;
							}
							string value = enumerator.Current;
							A_0.WriteStartElement(RecordTableEnumerator.b("䐶", a_));
							A_0.WriteAttributeString(RecordTableEnumerator.b("䄶", a_), value);
							A_0.WriteEndElement();
							num = 2;
							continue;
						}
						case 3:
							num = 4;
							continue;
						case 4:
							goto IL_D1;
						}
						IL_AE:
						num = 0;
						continue;
						goto IL_AE;
					}
					IL_D1:
					goto IL_10D;
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
				goto IL_E1;
			case 2:
				return;
			}
			if (A_1.Count == 0)
			{
				num = 2;
				continue;
			}
			IL_E1:
			A_0.WriteStartElement(RecordTableEnumerator.b("倶䬸吺䠼伾ࡀ㝂⁄⩆㩈", a_));
			enumerator = A_1.GetEnumerator();
			num = 1;
		}
		return;
		IL_10D:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			A_0.WriteEndElement();
			return;
		}
	}

	// Token: 0x06004877 RID: 18551 RVA: 0x002BD6FC File Offset: 0x002BC6FC
	private static void ᜁ(XmlWriter A_0, spr\u1920 A_1)
	{
		int a_ = 7;
		switch (0)
		{
		default:
			if (true)
			{
			}
			for (;;)
			{
				A_0.WriteStartElement(RecordTableEnumerator.b("似帾⽀⑂⁄ᝆ㭈", a_));
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_173;
					case 1:
						if (A_1.\u170D())
						{
							num = 5;
							continue;
						}
						A_0.WriteAttributeString(RecordTableEnumerator.b("尼䨾㕀ⱂD⥆ⵈ", a_), A_1.ᜆ().ToString());
						A_0.WriteAttributeString(RecordTableEnumerator.b("尼䨾㕀ⱂᙄ㍆⡈㥊㥌", a_), A_1.ᜐ().ToString());
						num = 2;
						continue;
					case 2:
						goto IL_1C7;
					case 3:
						goto IL_ED;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_112;
						default:
							if (false)
							{
							}
							if (A_1.ᜏ())
							{
								num = 6;
								continue;
							}
							num = 1;
							continue;
						}
						break;
					case 5:
						goto IL_112;
					case 6:
						A_0.WriteAttributeString(RecordTableEnumerator.b("堼儾╀݂⑄㍆ⱈ", a_), A_1.ᜀ().ToString());
						A_0.WriteAttributeString(RecordTableEnumerator.b("丼䬾⁀ㅂㅄ͆⡈㽊⡌", a_), A_1.ᜇ().ToString());
						num = 0;
						continue;
					}
					break;
					IL_112:
					A_0.WriteAttributeString(RecordTableEnumerator.b("堼儾╀ൂい⩆", a_), A_1.ᜁ().ToString());
					A_0.WriteAttributeString(RecordTableEnumerator.b("丼䬾⁀ㅂㅄॆ㱈♊", a_), A_1.ᜄ().ToString());
					num = 3;
				}
			}
			IL_ED:
			IL_173:
			IL_1C7:
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("娼䴾⹀㙂㕄Նえ", a_), A_1.ᜅ(), PivotFieldGroupType.None);
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("娼䴾⹀㙂㕄ๆ❈㽊⡌㵎❐㉒㥔", a_), A_1.ᜌ(), -1.0);
			A_0.WriteEndElement();
			return;
		}
	}

	// Token: 0x06004878 RID: 18552 RVA: 0x002BD920 File Offset: 0x002BC920
	private static void ᜀ(XmlWriter A_0, spr\u1920 A_1)
	{
		int a_ = 0;
		int num = 0;
		switch (num)
		{
		default:
			for (;;)
			{
				byte[] array2;
				int num2;
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
					A_0.WriteStartElement(RecordTableEnumerator.b("刵儷䤹弻䰽┿㙁⅃ᙅ㩇", a_));
					byte[] array = A_1.ᜂ();
					array2 = array;
					num2 = 0;
					num = 2;
					break;
				}
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						if (num2 >= array2.Length)
						{
							num = 1;
							continue;
						}
						if (true)
						{
						}
						byte b = array2[num2];
						A_0.WriteStartElement(RecordTableEnumerator.b("丵", a_));
						A_0.WriteAttributeString(RecordTableEnumerator.b("䀵", a_), b.ToString());
						A_0.WriteEndElement();
						num2++;
						num = 3;
						continue;
					}
					case 1:
						goto IL_98;
					case 2:
						goto IL_80;
					case 3:
						goto IL_80;
					}
					break;
					IL_80:
					num = 0;
				}
			}
			IL_98:
			A_0.WriteEndElement();
			return;
		}
	}

	// Token: 0x06004879 RID: 18553 RVA: 0x002BDA20 File Offset: 0x002BCA20
	private static void ᜄ(XmlWriter A_0, XlsPivotCache A_1)
	{
		int a_ = 10;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				if (A_1.PreservedElements.ContainsKey(RecordTableEnumerator.b("⌿⍁❃⹅ⵇɉ╋⭍≏㍑♓㕕し㍙㥛ⵝ", a_)))
				{
					num = 6;
					continue;
				}
				goto IL_10D;
			case 2:
				goto IL_10B;
			case 3:
				goto IL_ED;
			case 4:
				goto IL_43;
			case 5:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				num = 1;
				continue;
			case 6:
			{
				Stream stream = A_1.PreservedElements[RecordTableEnumerator.b("⌿⍁❃⹅ⵇɉ╋⭍≏㍑♓㕕し㍙㥛ⵝ", a_)];
				stream.Position = 0L;
				ShapeParser.WriteNodeFromStream(A_0, stream);
				num = 3;
				continue;
			}
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
		IL_43:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_10D:
			if (true)
			{
			}
			return;
		default:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㜿ぁⵃ㉅ⵇ㡉", a_));
		}
		IL_ED:
		goto IL_10D;
		IL_10B:
		throw new ArgumentNullException(RecordTableEnumerator.b("⌿⍁❃⹅ⵇ", a_));
	}

	// Token: 0x0600487A RID: 18554 RVA: 0x002BDB4C File Offset: 0x002BCB4C
	private static void ᜃ(XmlWriter A_0, XlsPivotCache A_1)
	{
		int a_ = 13;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_113;
			case 1:
				goto IL_ED;
			case 2:
				if (A_1.PreservedElements.ContainsKey(RecordTableEnumerator.b("⡂㕄⹆㩈", a_)))
				{
					num = 6;
					continue;
				}
				return;
			case 4:
				goto IL_43;
			case 5:
				if (true)
				{
				}
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				num = 2;
				continue;
			case 6:
			{
				Stream stream = A_1.PreservedElements[RecordTableEnumerator.b("⡂㕄⹆㩈", a_)];
				stream.Position = 0L;
				ShapeParser.WriteNodeFromStream(A_0, stream);
				num = 1;
				continue;
			}
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
		IL_43:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return;
		default:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㑂㝄⹆㵈⹊㽌", a_));
		}
		IL_ED:
		return;
		IL_113:
		throw new ArgumentNullException(RecordTableEnumerator.b("⁂⑄⑆ⅈ⹊", a_));
	}

	// Token: 0x0600487B RID: 18555 RVA: 0x002BDC7C File Offset: 0x002BCC7C
	private static void ᜂ(XmlWriter A_0, XlsPivotCache A_1)
	{
		int a_ = 18;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
			{
				Stream stream = A_1.PreservedElements[RecordTableEnumerator.b("ⱇ⍉⅋⭍㹏⅑㵓㥕㙗⥙", a_)];
				stream.Position = 0L;
				ShapeParser.WriteNodeFromStream(A_0, stream);
				num = 4;
				continue;
			}
			case 2:
				goto IL_116;
			case 3:
				if (A_1.PreservedElements.ContainsKey(RecordTableEnumerator.b("ⱇ⍉⅋⭍㹏⅑㵓㥕㙗⥙", a_)))
				{
					num = 1;
					continue;
				}
				return;
			case 4:
				goto IL_F8;
			case 5:
				goto IL_43;
			case 6:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				num = 3;
				continue;
			}
			if (A_0 == null)
			{
				num = 5;
			}
			else
			{
				num = 6;
			}
		}
		IL_43:
		if (true)
		{
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
			throw new ArgumentNullException(RecordTableEnumerator.b("㽇㡉╋㩍㕏⁑", a_));
		}
		IL_F8:
		return;
		IL_116:
		throw new ArgumentNullException(RecordTableEnumerator.b("⭇⭉⽋♍㕏", a_));
	}

	// Token: 0x0600487C RID: 18556 RVA: 0x002BDDB0 File Offset: 0x002BCDB0
	private static void ᜁ(XmlWriter A_0, XlsPivotCache A_1)
	{
		int a_ = 4;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				Stream stream = A_1.PreservedElements[RecordTableEnumerator.b("圹夻弽㌿㝁㙃⍅ཇ㡉⍋㭍⁏⅑", a_)];
				stream.Position = 0L;
				ShapeParser.WriteNodeFromStream(A_0, stream);
				if (true)
				{
				}
				num = 6;
				continue;
			}
			case 1:
				goto IL_43;
			case 3:
				if (A_1.PreservedElements.ContainsKey(RecordTableEnumerator.b("圹夻弽㌿㝁㙃⍅ཇ㡉⍋㭍⁏⅑", a_)))
				{
					num = 0;
					continue;
				}
				return;
			case 4:
				if (A_1 == null)
				{
					num = 5;
					continue;
				}
				num = 3;
				continue;
			case 5:
				goto IL_113;
			case 6:
				goto IL_F5;
			}
			if (A_0 == null)
			{
				num = 1;
			}
			else
			{
				num = 4;
			}
		}
		IL_43:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return;
		default:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䴹主圽㐿❁㙃", a_));
		}
		IL_F5:
		return;
		IL_113:
		throw new ArgumentNullException(RecordTableEnumerator.b("夹崻崽⠿❁", a_));
	}

	// Token: 0x0600487D RID: 18557 RVA: 0x002BDEE0 File Offset: 0x002BCEE0
	private static void ᜀ(XmlWriter A_0, XlsPivotCache A_1)
	{
		int a_ = 5;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_113;
			case 1:
				goto IL_43;
			case 3:
				goto IL_ED;
			case 4:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				num = 6;
				continue;
			case 5:
			{
				Stream stream = A_1.PreservedElements[RecordTableEnumerator.b("嘺尼伾㉀", a_)];
				stream.Position = 0L;
				ShapeParser.WriteNodeFromStream(A_0, stream);
				num = 3;
				continue;
			}
			case 6:
				if (A_1.PreservedElements.ContainsKey(RecordTableEnumerator.b("嘺尼伾㉀", a_)))
				{
					num = 5;
					continue;
				}
				return;
			}
			if (A_0 == null)
			{
				num = 1;
			}
			else
			{
				if (true)
				{
				}
				num = 4;
			}
		}
		IL_43:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return;
		default:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䰺似嘾㕀♂㝄", a_));
		}
		IL_ED:
		return;
		IL_113:
		throw new ArgumentNullException(RecordTableEnumerator.b("堺尼尾⥀♂", a_));
	}

	// Token: 0x0600487E RID: 18558 RVA: 0x002BE010 File Offset: 0x002BD010
	private static List<int> ᜀ(XlsPivotCacheField A_0)
	{
		for (;;)
		{
			switch (0)
			{
			default:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				goto Block_0;
			}
		}
		Block_0:
		if (false)
		{
		}
		spr\u23FD spr_u23FD = A_0.CalculatedItems;
		List<int> list = new List<int>();
		using (List<sprỺ>.Enumerator enumerator = spr_u23FD.GetEnumerator())
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_D5;
				case 2:
				{
					if (!enumerator.MoveNext())
					{
						num = 4;
						continue;
					}
					sprỺ sprỺ = enumerator.Current;
					list.Add(sprỺ.ᜀ().ᜁ()[0].ᜃ()[0]);
					num = 0;
					continue;
				}
				case 4:
					num = 1;
					continue;
				}
				IL_AF:
				num = 2;
				continue;
				goto IL_AF;
			}
			IL_D5:;
		}
		return list;
	}

	// Token: 0x0600487F RID: 18559 RVA: 0x002BE114 File Offset: 0x002BD114
	internal static void ᜀ(XmlWriter A_0, string A_1, Enum A_2, Enum A_3)
	{
		if (true)
		{
		}
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
					A_0.WriteAttributeString(A_1, spr\u1B7A.ᜄ(A_2.ToString()));
					num = 0;
					continue;
				}
				break;
			}
			if (A_2.CompareTo(A_3) == 0)
			{
				break;
			}
			num = 1;
		}
	}

	// Token: 0x06004880 RID: 18560 RVA: 0x002BE19C File Offset: 0x002BD19C
	private static string ᜀ(ushort A_0)
	{
		int a_ = 5;
		string result = RecordTableEnumerator.b("ᠺ猼ှ@", a_);
		if (A_0 == 1)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_3E;
				}
			}
			IL_3E:
			if (true)
			{
			}
			if (false)
			{
			}
			return result;
		}
		throw new NotImplementedException(RecordTableEnumerator.b("欺吼䤾⹀㝂敄Ɇ㭈㥊≌㵎煐R⅔╖じ㕚㩜", a_));
	}

	// Token: 0x040020E4 RID: 8420
	private const int ᜀ = 3;
}
