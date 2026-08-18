using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.PivotTables;
using Spire.Xls.Core.Spreadsheet.XmlReaders.Shapes;

// Token: 0x020003B1 RID: 945
internal class spr\u2514
{
	// Token: 0x06003965 RID: 14693 RVA: 0x00201DEC File Offset: 0x00200DEC
	public static void \u170D(XmlWriter A_0, XlsPivotTable A_1)
	{
		int a_ = 4;
		switch (0)
		{
		default:
		{
			int num = 22;
			XlsWorksheet worksheet;
			for (;;)
			{
				string text;
				spr\u1A79 spr_u1A;
				switch (num)
				{
				case 0:
					if (text.Length > 0)
					{
						num = 7;
						continue;
					}
					goto IL_81D;
				case 1:
					A_0.WriteAttributeString(RecordTableEnumerator.b("夹医刽࠿❁╃≅ⵇ㡉ཋ⽍⁏♑㵓㥕㙗", a_), spr_u1A.ᜧ());
					num = 32;
					continue;
				case 2:
					goto IL_900;
				case 3:
					if (spr_u1A.ᜤ() == PivotPageAreaFieldsOrderType.OverThenDown)
					{
						num = 34;
						continue;
					}
					goto IL_7D7;
				case 4:
				{
					string text2;
					if (text2 != null)
					{
						num = 9;
						continue;
					}
					goto IL_762;
				}
				case 5:
				{
					string text2;
					if (text2.Length > 0)
					{
						num = 1;
						continue;
					}
					goto IL_762;
				}
				case 6:
					if (spr_u1A.ᜁ())
					{
						num = 30;
						continue;
					}
					goto IL_900;
				case 7:
					A_0.WriteAttributeString(RecordTableEnumerator.b("䠹医䤽࠿❁╃≅ⵇ㡉ཋ⽍⁏♑㵓㥕㙗", a_), spr_u1A.\u171E());
					num = 39;
					continue;
				case 8:
					goto IL_7D7;
				case 9:
					num = 5;
					continue;
				case 10:
					num = 0;
					continue;
				case 11:
				{
					if (worksheet.PreservePivotTables.Count > 0)
					{
						num = 24;
						continue;
					}
					spr_u1A = (A_1.Options as spr\u1A79);
					A_0.WriteStartElement(RecordTableEnumerator.b("䨹唻䠽⼿㙁၃❅⩇♉⥋੍㕏㑑㵓㡕ㅗ⹙㕛ㅝ๟", a_), RecordTableEnumerator.b("刹䠻䨽〿硁歃楅㭇⥉⑋⭍㵏㍑❓硕㝗⩙㥛そᡟཡࡣeݧᡩū཭ѯű婳᥵੷ᵹ卻ൽ黎煉歹랗ꢙ겛꺝隟趡즣장솧쒩", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("吹崻匽┿", a_), A_1.Name);
					A_0.WriteAttributeString(RecordTableEnumerator.b("夹崻崽⠿❁ൃ≅", a_), (A_1.CacheIndex + 1).ToString());
					spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("嬹䰻丽ⰿ㭁੃㍅╇⡉⥋㱍ᙏ㵑♓㭕㥗⹙⽛", a_), spr_u1A.ᜨ(), true);
					spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("嬹䰻丽ⰿ㭁ك⥅㩇⹉⥋㱍ᙏ㵑♓㭕㥗⹙⽛", a_), spr_u1A.ᜣ(), true);
					spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("嬹䰻丽ⰿ㭁Ƀ⥅♇㹉ੋ⅍≏㽑㕓≕⭗", a_), spr_u1A.ᜢ(), true);
					spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("嬹䰻丽ⰿ㭁ᑃ❅㱇㹉⥋㱍㹏ᑑ㭓⑕㕗㭙⡛ⵝ", a_), spr_u1A.ᜉ(), true);
					spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("嬹䰻丽ⰿ㭁Ճ⩅ⅇⵉ≋⍍㕏㱑⁓ၕ㝗⡙ㅛ㽝ᑟᅡ", a_), spr_u1A.\u1717(), true);
					spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("嬹䰻丽ⰿ㭁ፃ⽅ⱇ㹉⑋ٍ㕏㭑㍓㹕ⱗ᱙㍛ⱝൟ͡ၣᕥ", a_), spr_u1A.\u1719(), false);
					spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("夹医刽ܿぁ╃⡅ⱇṉ⍋㩍ㅏ㹑❓", a_), A_1.IsColumnGrand, true);
					spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("䠹医䤽ܿぁ╃⡅ⱇṉ⍋㩍ㅏ㹑❓", a_), A_1.IsRowGrand, true);
					spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("尹唻嬽ⰿ♁ᑃ㑅ⅇ⑉㡋ᩍ㥏♑㡓㍕⭗", a_), spr_u1A.ᜊ(), false);
					string text2 = spr_u1A.ᜧ();
					goto IL_2CA;
				}
				case 12:
					if (spr_u1A.ᜦ().Length > 0)
					{
						num = 33;
						continue;
					}
					goto IL_78E;
				case 13:
					goto IL_640;
				case 14:
					spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("帹崻䨽ℿቁ⭃㕅ⅇ㹉╋⅍㹏", a_), spr_u1A.\u171A());
					num = 35;
					continue;
				case 15:
					if (A_1 == null)
					{
						num = 17;
						continue;
					}
					worksheet = A_1.Worksheet;
					num = 11;
					continue;
				case 16:
					A_0.WriteAttributeString(RecordTableEnumerator.b("唹䤻䨽ⰿ⭁⩃⍅", a_), RecordTableEnumerator.b("ହ", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("唹䤻䨽ⰿ⭁⩃⍅ే⭉㡋⽍", a_), RecordTableEnumerator.b("ହ", a_));
					num = 28;
					continue;
				case 17:
					goto IL_96F;
				case 18:
					A_0.WriteAttributeString(RecordTableEnumerator.b("尹唻嬽ⰿ♁ࡃ⽅㭇㹉Ὃ⅍≏♑ᕓ╕㭗㽙㉛㩝य़ౡͣ", a_), RecordTableEnumerator.b("ହ", a_));
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2CA;
					default:
						if (false)
						{
						}
						num = 19;
						continue;
					}
					break;
				case 19:
					goto IL_6CD;
				case 20:
					if (spr_u1A.ᜐ() == PivotTableLayoutType.Outline)
					{
						num = 26;
						continue;
					}
					A_0.WriteAttributeString(RecordTableEnumerator.b("夹医匽〿⍁❃㉅", a_), RecordTableEnumerator.b("ਹ", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("夹医匽〿⍁❃㉅ే⭉㡋⽍", a_), RecordTableEnumerator.b("ਹ", a_));
					num = 25;
					continue;
				case 21:
					if (spr_u1A.\u171B().Length > 0)
					{
						num = 23;
						continue;
					}
					goto IL_640;
				case 23:
					A_0.WriteAttributeString(RecordTableEnumerator.b("弹主䰽⼿ぁ݃❅㡇㹉╋⅍㹏", a_), spr_u1A.\u171B());
					num = 13;
					continue;
				case 24:
					goto IL_348;
				case 25:
					goto IL_97E;
				case 26:
					A_0.WriteAttributeString(RecordTableEnumerator.b("夹医匽〿⍁❃㉅", a_), RecordTableEnumerator.b("ਹ", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("夹医匽〿⍁❃㉅ే⭉㡋⽍", a_), RecordTableEnumerator.b("ਹ", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("唹䤻䨽ⰿ⭁⩃⍅", a_), RecordTableEnumerator.b("ହ", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("唹䤻䨽ⰿ⭁⩃⍅ే⭉㡋⽍", a_), RecordTableEnumerator.b("ହ", a_));
					num = 40;
					continue;
				case 27:
					goto IL_E2;
				case 28:
					goto IL_97E;
				case 29:
					if (spr_u1A.ᜐ() == PivotTableLayoutType.Compact)
					{
						num = 16;
						continue;
					}
					num = 20;
					continue;
				case 30:
					A_0.WriteAttributeString(RecordTableEnumerator.b("尹唻嬽ⰿ♁ࡃ⽅㭇㹉Ὃ⅍≏♑ᕓ╕㭗㽙㉛㩝य़ౡͣ", a_), RecordTableEnumerator.b("ହ", a_));
					if (true)
					{
					}
					num = 2;
					continue;
				case 31:
					if (text != null)
					{
						num = 10;
						continue;
					}
					goto IL_81D;
				case 32:
					goto IL_762;
				case 33:
					A_0.WriteAttributeString(RecordTableEnumerator.b("圹唻䴽㌿⭁⩃ⅅେ⭉㱋㩍㥏㵑㩓", a_), spr_u1A.ᜦ());
					num = 37;
					continue;
				case 34:
					spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("䨹崻夽┿ു㉃⍅㩇ṉ⑋⭍㹏ᙑ㭓⅕㙗", a_), true, false);
					num = 8;
					continue;
				case 35:
					goto IL_34D;
				case 36:
					if (spr_u1A.\u171A() > 0)
					{
						num = 14;
						continue;
					}
					goto IL_34D;
				case 37:
					goto IL_78E;
				case 38:
					if (spr_u1A.ᜁ())
					{
						num = 18;
						continue;
					}
					goto IL_A70;
				case 39:
					goto IL_81D;
				case 40:
					goto IL_97E;
				}
				if (A_0 == null)
				{
					num = 27;
					continue;
				}
				num = 15;
				continue;
				IL_2CA:
				num = 4;
				continue;
				IL_34D:
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("帹唻䴽ℿ⁁⡃⍅็⍉⥋≍㑏ṑ㵓╕ⱗ", a_), !spr_u1A.\u1715(), false);
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("弹堻圽㐿ف╃㉅⥇", a_), spr_u1A.\u1713(), false);
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("弹刻弽∿⹁⅃Ʌ㩇⍉⁋≍", a_), A_1.EnableDrilldown, false);
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("弹刻弽∿⹁⅃Eⅇ⽉⁋⩍O⁑㭓♕㵗⡙⡛㝝՟ᅡ", a_), spr_u1A.\u1714(), false);
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("弹刻弽∿⹁⅃ᅅⅇぉⵋ㱍㑏", a_), A_1.EnableWizard, false);
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("嬹伻䨽┿ぁⵃ㕅⍇ṉ⍋㩍ㅏ㹑❓", a_), spr_u1A.\u1718(), false);
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("圹夻䰽✿❁ൃ㉅ⵇ❉", a_), spr_u1A.ᜑ(), false);
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("䤹吻儽㜿Ł╃⩅⭇݉⹋㱍⍏", a_), spr_u1A.ᜀ(), false);
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("䤹吻儽㜿ف㙃⽅⑇♉", a_), A_1.ShowDrillIndicators, true);
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("伹伻嬽Ŀ㝁ぃ⥅็╉㹋⍍ㅏ♑⁓㽕㙗㵙", a_), spr_u1A.ᜅ(), false);
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("䤹吻儽㜿ف╃㉅⥇ṉ╋㹍⍏", a_), spr_u1A.\u171D(), true);
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("匹䠻嬽ⴿቁ㙃⽅♇㹉ᡋ❍⑏㹑ㅓ╕", a_), A_1.RepeatItemsOnEachPrintedPage, false);
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("匹刻娽┿ⱁぃ", a_), spr_u1A.\u170D());
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("䤹吻儽㜿ੁ⅃❅ⱇ⽉㹋㵍", a_), A_1.DisplayFieldCaptions, true);
				spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("䨹崻夽┿ᕁ㙃❅㡇", a_), spr_u1A.ᜡ(), 0);
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("圹䤻刽㐿⭁㑃⩅ⵇ౉╋⭍㱏㙑ቓ㽕㑗⹙㥛ⱝ፟", a_), spr_u1A.ᜌ(), true);
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("崹主圽␿ف㙃⥅㡇၉⍋⁍㕏⅑", a_), spr_u1A.ᜥ(), false);
				num = 3;
				continue;
				IL_640:
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("䨹主嬽㌿❁㙃ぅⵇ౉⍋㱍㵏㍑⁓≕ㅗ㑙㭛", a_), spr_u1A.ᜋ(), true);
				num = 6;
				continue;
				IL_762:
				text = spr_u1A.\u171E();
				num = 31;
				continue;
				IL_78E:
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("䤹吻儽㜿݁㙃㑅❇㡉", a_), spr_u1A.ᜈ(), false);
				num = 21;
				continue;
				IL_7D7:
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("䤹吻儽㜿ཁⵃ㕅㭇⍉≋⥍灏", a_), spr_u1A.ᜆ(), true);
				num = 12;
				continue;
				IL_81D:
				num = 29;
				continue;
				IL_900:
				num = 38;
				continue;
				IL_97E:
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("夹主嬽ℿ㙁⅃≅ṇ⽉㹋㵍㥏㵑㩓", a_), spr_u1A.\u1716());
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("伹䰻娽ℿ㙁⅃≅ṇ⽉㹋㵍㥏㵑㩓", a_), spr_u1A.\u171C());
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("圹唻倽ሿ❁≃㑅ⵇ㥉⑋⽍㉏㹑ㅓU㵗⡙⽛㝝ཟౡ", a_), spr_u1A.\u1712());
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("夹䤻䴽㐿ⵁ⥃੅ⅇ㥉㡋ᵍ㽏⁑⁓", a_), spr_u1A.ᜇ(), true);
				A_0.WriteAttributeString(RecordTableEnumerator.b("帹崻䨽ℿŁ╃㙅㱇⍉⍋⁍", a_), spr_u1A.ᜃ());
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("帹崻䨽ℿു⩃ᑅ❇㵉㽋", a_), A_1.ShowDataFieldInRow, false);
				num = 36;
			}
			IL_E2:
			throw new ArgumentNullException(RecordTableEnumerator.b("䴹主圽㐿❁㙃", a_));
			IL_348:
			Stream stream = worksheet.PreservePivotTables[0];
			stream.Position = 0L;
			ShapeParser.WriteNodeFromStream(A_0, stream);
			worksheet.PreservePivotTables.RemoveAt(0);
			return;
			IL_6CD:
			goto IL_A70;
			IL_96F:
			throw new ArgumentNullException(RecordTableEnumerator.b("䨹唻䠽⼿㙁၃❅⩇♉⥋", a_));
			IL_A70:
			spr\u2514.ᜂ(A_0, A_1);
			spr\u2514.ᜃ(A_0, A_1);
			spr\u2514.ᜆ(A_0, A_1);
			spr\u2514.ᜅ(A_0, A_1);
			spr\u2514.ᜈ(A_0, A_1);
			spr\u2514.ᜄ(A_0, A_1);
			spr\u2514.ᜋ(A_0, A_1);
			spr\u2514.ᜉ(A_0, A_1);
			spr\u2514.ᜌ(A_0, A_1);
			spr\u2514.ᜁ(A_0, A_1);
			spr\u2514.ᜇ(A_0, A_1);
			spr\u2514.ᜀ(A_0, A_1);
			spr\u2514.ᜊ(A_0, A_1);
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06003966 RID: 14694 RVA: 0x002028CC File Offset: 0x002018CC
	private static void ᜌ(XmlWriter A_0, XlsPivotTable A_1)
	{
		int a_ = 13;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_E1;
			case 2:
				goto IL_63;
			case 3:
				return;
			case 4:
				goto IL_D3;
			case 5:
				if (!A_1.PreservedElements.ContainsKey(RecordTableEnumerator.b("⁂ⵄ♆㭈㽊ୌ⁎⍐㹒㑔⍖⩘", a_)))
				{
					num = 3;
					continue;
				}
				goto IL_E3;
			}
			if (A_0 != null)
			{
				num = 4;
				continue;
			}
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
				num = 2;
				continue;
			}
			IL_D3:
			if (A_1 == null)
			{
				num = 0;
			}
			else
			{
				num = 5;
			}
		}
		IL_63:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑂㝄⹆㵈⹊㽌", a_));
		IL_E1:
		throw new ArgumentNullException(RecordTableEnumerator.b("㍂ⱄㅆ♈㽊᥌⹎㍐㽒ご", a_));
		IL_E3:
		Stream stream = A_1.PreservedElements[RecordTableEnumerator.b("⁂ⵄ♆㭈㽊ୌ⁎⍐㹒㑔⍖⩘", a_)];
		stream.Position = 0L;
		ShapeParser.WriteNodeFromStream(A_0, stream);
	}

	// Token: 0x06003967 RID: 14695 RVA: 0x002029E8 File Offset: 0x002019E8
	private static void ᜋ(XmlWriter A_0, XlsPivotTable A_1)
	{
		int a_ = 18;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_C8;
			case 2:
				goto IL_DE;
			case 3:
				if (!A_1.PreservedElements.ContainsKey(RecordTableEnumerator.b("⹇╉㹋⍍ㅏ♑❓", a_)))
				{
					num = 4;
					continue;
				}
				goto IL_E0;
			case 4:
				return;
			case 5:
				goto IL_5B;
			}
			if (A_0 != null)
			{
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
				num = 5;
				continue;
			}
			IL_C8:
			if (true)
			{
			}
			if (A_1 == null)
			{
				num = 2;
			}
			else
			{
				num = 3;
			}
		}
		IL_5B:
		throw new ArgumentNullException(RecordTableEnumerator.b("㽇㡉╋㩍㕏⁑", a_));
		IL_DE:
		throw new ArgumentNullException(RecordTableEnumerator.b("㡇⍉㩋⅍⑏ّ㕓㑕㑗㽙", a_));
		IL_E0:
		Stream stream = A_1.PreservedElements[RecordTableEnumerator.b("⹇╉㹋⍍ㅏ♑❓", a_)];
		stream.Position = 0L;
		ShapeParser.WriteNodeFromStream(A_0, stream);
	}

	// Token: 0x06003968 RID: 14696 RVA: 0x00202B00 File Offset: 0x00201B00
	private static void ᜊ(XmlWriter A_0, XlsPivotTable A_1)
	{
		int a_ = 5;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_DE;
			case 2:
				goto IL_C8;
			case 3:
				return;
			case 4:
				if (!A_1.PreservedElements.ContainsKey(RecordTableEnumerator.b("崺吼匾㕀♂㝄㑆", a_)))
				{
					num = 3;
					continue;
				}
				goto IL_E0;
			case 5:
				goto IL_65;
			}
			if (A_0 != null)
			{
				num = 2;
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
				num = 5;
				continue;
			}
			IL_C8:
			if (true)
			{
			}
			if (A_1 == null)
			{
				num = 1;
			}
			else
			{
				num = 4;
			}
		}
		IL_65:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰺似嘾㕀♂㝄", a_));
		IL_DE:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬺吼䤾⹀㝂ᅄ♆⭈❊⡌", a_));
		IL_E0:
		Stream stream = A_1.PreservedElements[RecordTableEnumerator.b("崺吼匾㕀♂㝄㑆", a_)];
		stream.Position = 0L;
		ShapeParser.WriteNodeFromStream(A_0, stream);
	}

	// Token: 0x06003969 RID: 14697 RVA: 0x00202C18 File Offset: 0x00201C18
	private static void ᜉ(XmlWriter A_0, XlsPivotTable A_1)
	{
		int a_ = 6;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!A_1.PreservedElements.ContainsKey(RecordTableEnumerator.b("弻儽⸿♁ⵃ㉅ⅇ╉≋⽍㱏ᑑ㭓⑕㕗㭙⡛ⵝ", a_)))
				{
					num = 2;
					continue;
				}
				goto IL_E0;
			case 1:
				goto IL_3F;
			case 2:
				return;
			case 4:
				if (A_1 == null)
				{
					num = 5;
					continue;
				}
				num = 0;
				continue;
			case 5:
				goto IL_DE;
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
		IL_3F:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_94:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰻圽㘿ⵁぃቅ⥇⡉⁋⭍", a_));
		default:
			if (false)
			{
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䬻䰽⤿㙁⅃㑅", a_));
		}
		return;
		IL_DE:
		goto IL_94;
		IL_E0:
		Stream stream = A_1.PreservedElements[RecordTableEnumerator.b("弻儽⸿♁ⵃ㉅ⅇ╉≋⽍㱏ᑑ㭓⑕㕗㭙⡛ⵝ", a_)];
		stream.Position = 0L;
		ShapeParser.WriteNodeFromStream(A_0, stream);
	}

	// Token: 0x0600396A RID: 14698 RVA: 0x00202D30 File Offset: 0x00201D30
	private static void ᜈ(XmlWriter A_0, XlsPivotTable A_1)
	{
		int a_ = 0;
		switch (0)
		{
		default:
			for (;;)
			{
				bool flag = true;
				bool flag2 = false;
				int count = A_1.PageFields.Count;
				int num = 19;
				for (;;)
				{
					int num2;
					XlsPivotField xlsPivotField;
					XlsPivotField xlsPivotField2;
					int num3;
					switch (num)
					{
					case 0:
						goto IL_218;
					case 1:
						A_0.WriteEndElement();
						num = 15;
						continue;
					case 2:
						if (!flag)
						{
							num = 1;
							continue;
						}
						return;
					case 3:
						flag2 = true;
						count = A_1.PivotPageFields.Count;
						num = 0;
						continue;
					case 4:
						num = 24;
						continue;
					case 5:
						xlsPivotField = (A_1.PivotPageFields[num2] as XlsPivotField);
						goto IL_2DF;
					case 6:
						goto IL_C1;
					case 7:
						A_0.WriteStartElement(RecordTableEnumerator.b("䘵夷崹夻砽⤿❁⡃≅㭇", a_));
						flag = false;
						num = 8;
						continue;
					case 8:
						goto IL_30B;
					case 9:
						A_0.WriteAttributeString(RecordTableEnumerator.b("張䰷弹儻", a_), xlsPivotField2.ItemIndex.ToString());
						num = 6;
						continue;
					case 10:
						if (!flag2)
						{
							num = 21;
							continue;
						}
						num = 5;
						continue;
					case 11:
						num = 22;
						continue;
					case 12:
						if (xlsPivotField2.Axis == AxisTypes.Page)
						{
							goto IL_2FA;
						}
						goto IL_258;
					case 13:
						num = 2;
						continue;
					case 14:
						goto IL_144;
					case 15:
						return;
					case 16:
						if (xlsPivotField2.ItemIndex > -1)
						{
							num = 9;
							continue;
						}
						goto IL_C1;
					case 17:
						goto IL_144;
					case 18:
						goto IL_258;
					case 19:
						if (A_1.Workbook.Version != ExcelVersion.Version97to2003)
						{
							num = 11;
							continue;
						}
						goto IL_218;
					case 20:
						xlsPivotField = A_1.PivotFields[num2];
						goto IL_2DF;
					case 21:
						num = 20;
						continue;
					case 22:
						if (A_1.ᜀ(AxisTypes.Page).Count == A_1.PivotPageFields.Count)
						{
							num = 3;
							continue;
						}
						goto IL_218;
					case 23:
						if (num2 < num3)
						{
							num = 10;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2FA;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num = 13;
							continue;
						}
						break;
					case 24:
						if (flag)
						{
							num = 7;
							continue;
						}
						goto IL_30B;
					}
					break;
					IL_C1:
					A_0.WriteAttributeString(RecordTableEnumerator.b("帵儷弹主", a_), RecordTableEnumerator.b("ᬵष", a_));
					A_0.WriteEndElement();
					num = 18;
					continue;
					IL_144:
					num = 23;
					continue;
					IL_218:
					num2 = 0;
					num3 = count;
					num = 17;
					continue;
					IL_258:
					num2++;
					num = 14;
					continue;
					IL_2DF:
					xlsPivotField2 = xlsPivotField;
					num = 12;
					continue;
					IL_2FA:
					num = 4;
					continue;
					IL_30B:
					A_0.WriteStartElement(RecordTableEnumerator.b("䘵夷崹夻砽⤿❁⡃≅", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("倵吷帹", a_), xlsPivotField2.FieldIndex.ToString());
					num = 16;
				}
			}
			return;
		}
	}

	// Token: 0x0600396B RID: 14699 RVA: 0x002030AC File Offset: 0x002020AC
	private static void ᜇ(XmlWriter A_0, XlsPivotTable A_1)
	{
		int a_ = 13;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				if (true)
				{
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("㍂ⱄㅆ♈㽊᥌⹎㍐㽒ごіⵘ≚ㅜ㩞⡠ൢͤࡦ", a_));
				PivotBuiltInStyles? builtInStyle = A_1.BuiltInStyle;
				num = 3;
				continue;
			}
			case 1:
				goto IL_11E;
			case 2:
				goto IL_E4;
			case 3:
			{
				PivotBuiltInStyles? builtInStyle;
				if (builtInStyle != null)
				{
					num = 4;
					continue;
				}
				goto IL_123;
			}
			case 4:
			{
				PivotBuiltInStyles? builtInStyle;
				A_0.WriteAttributeString(RecordTableEnumerator.b("ⵂ⑄⩆ⱈ", a_), builtInStyle.ToString());
				num = 2;
				continue;
			}
			case 5:
				goto IL_43;
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
		IL_43:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑂㝄⹆㵈⹊㽌", a_));
		IL_E4:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_11E:
			throw new ArgumentNullException(RecordTableEnumerator.b("㍂ⱄㅆ♈㽊᥌⹎㍐㽒ご", a_));
		default:
			if (false)
			{
			}
			break;
		}
		IL_123:
		spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("あⵄ⡆㹈᥊≌㡎ᥐ㙒㑔㍖㱘⥚⹜", a_), A_1.ShowRowHeaderStyle, false);
		spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("あⵄ⡆㹈ࡊ≌⍎ᥐ㙒㑔㍖㱘⥚⹜", a_), A_1.ShowColHeaderStyle, false);
		spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("あⵄ⡆㹈᥊≌㡎ɐ❒❔㹖⥘㹚⹜", a_), A_1.ShowRowStripes, true);
		spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("あⵄ⡆㹈ࡊ≌⍎ɐ❒❔㹖⥘㹚⹜", a_), A_1.ShowColStripes, true);
		spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("あⵄ⡆㹈݊ⱌ㱎═ၒ㩔㭖ⱘ㙚㍜", a_), A_1.ShowLastCol, false);
		A_0.WriteEndElement();
	}

	// Token: 0x0600396C RID: 14700 RVA: 0x0020326C File Offset: 0x0020226C
	private static void ᜆ(XmlWriter A_0, XlsPivotTable A_1)
	{
		int a_ = 19;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u2514.ᜀ(A_0, A_1, AxisTypes.Row, RecordTableEnumerator.b("㭈⑊㩌ॎ㡐㙒㥔㍖⩘", a_), RecordTableEnumerator.b("㭈⑊㩌َ═㙒㡔⑖", a_), A_1.ShowDataFieldInRow);
	}

	// Token: 0x0600396D RID: 14701 RVA: 0x002032DC File Offset: 0x002022DC
	private static void ᜅ(XmlWriter A_0, XlsPivotTable A_1)
	{
		int a_ = 11;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u2514.ᜀ(A_0, A_1, AxisTypes.Column, RecordTableEnumerator.b("≀ⱂ⥄ņ⁈⹊⅌⭎≐", a_), RecordTableEnumerator.b("≀ⱂ⥄ๆ㵈⹊⁌㱎", a_), !A_1.ShowDataFieldInRow);
	}

	// Token: 0x0600396E RID: 14702 RVA: 0x00203350 File Offset: 0x00202350
	private static void ᜀ(XmlWriter A_0, XlsPivotTable A_1, AxisTypes A_2, string A_3, string A_4, bool A_5)
	{
		int a_ = 4;
		switch (0)
		{
		default:
		{
			int num = 23;
			for (;;)
			{
				List<int> list2;
				int count2;
				switch (num)
				{
				case 0:
					goto IL_159;
				case 1:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 24;
						continue;
					}
					List<XlsPivotField> list;
					PivotField item = (PivotField)list[num2];
					PivotTableFields pivotFields;
					list2.Add(pivotFields.IndexOf(item));
					num2++;
					num = 2;
					continue;
				}
				case 2:
					goto IL_1A9;
				case 3:
					num = 22;
					continue;
				case 4:
				{
					PivotDataFields dataFields;
					if (dataFields.Count > 1)
					{
						num = 7;
						continue;
					}
					goto IL_3D6;
				}
				case 5:
					goto IL_3D6;
				case 6:
					if (count2 > 0)
					{
						num = 18;
						continue;
					}
					return;
				case 7:
					list2.Add(-2);
					num = 5;
					continue;
				case 8:
				{
					try
					{
						num = 4;
						for (;;)
						{
							switch (num)
							{
							case 1:
								goto IL_3C3;
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
									num = 1;
									continue;
								}
								break;
							case 3:
							{
								List<IPivotField>.Enumerator enumerator;
								if (!enumerator.MoveNext())
								{
									num = 2;
									continue;
								}
								XlsPivotField xlsPivotField = (XlsPivotField)enumerator.Current;
								PivotTableFields pivotFields;
								list2.Add(pivotFields.IndexOf(xlsPivotField as PivotField));
								num = 0;
								continue;
							}
							}
							IL_37E:
							num = 3;
							continue;
							goto IL_37E;
						}
						IL_3C3:
						goto IL_136;
					}
					finally
					{
						List<IPivotField>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					goto IL_3D6;
					IL_136:
					int[] array;
					spr\u2514.ᜀ(list2, array);
					num = 17;
					continue;
				}
				case 9:
					if (list2.Count > 0)
					{
						num = 27;
						continue;
					}
					goto IL_2F5;
				case 10:
					goto IL_1A9;
				case 11:
					goto IL_AB;
				case 12:
					if (A_3 == RecordTableEnumerator.b("䠹医䤽ؿ⭁⅃⩅ⱇ㥉", a_))
					{
						num = 3;
						continue;
					}
					goto IL_2F5;
				case 13:
				{
					PivotDataFields dataFields = A_1.DataFields;
					num = 4;
					continue;
				}
				case 14:
				{
					if (A_1 == null)
					{
						num = 26;
						continue;
					}
					list2 = new List<int>();
					PivotTableFields pivotFields = A_1.PivotFields;
					List<XlsPivotField> list = A_1.ᜀ(A_2);
					int num2 = 0;
					int count = list.Count;
					num = 10;
					continue;
				}
				case 15:
					goto IL_240;
				case 16:
					goto IL_159;
				case 17:
					goto IL_2F5;
				case 18:
				{
					if (true)
					{
					}
					A_0.WriteStartElement(A_3);
					int num3 = 0;
					num = 0;
					continue;
				}
				case 19:
					if (A_5)
					{
						num = 13;
						continue;
					}
					goto IL_3D6;
				case 20:
				{
					int num3;
					if (num3 >= count2)
					{
						num = 21;
						continue;
					}
					int num4 = list2[num3];
					A_0.WriteStartElement(RecordTableEnumerator.b("尹唻嬽ⰿ♁", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("䈹", a_), num4.ToString());
					A_0.WriteEndElement();
					num3++;
					num = 16;
					continue;
				}
				case 21:
					A_0.WriteEndElement();
					spr\u2514.ᜀ(A_0, list2, A_4, A_1);
					num = 15;
					continue;
				case 22:
					if (A_1.Workbook.Version != ExcelVersion.Version97to2003)
					{
						num = 25;
						continue;
					}
					goto IL_2F5;
				case 24:
					num = 19;
					continue;
				case 25:
					num = 9;
					continue;
				case 26:
					goto IL_42A;
				case 27:
				{
					int[] array = new int[list2.Count];
					list2.CopyTo(array, 0);
					list2.Clear();
					List<IPivotField>.Enumerator enumerator = A_1.PivotRowFields.GetEnumerator();
					num = 8;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 11;
					continue;
				}
				num = 14;
				continue;
				IL_159:
				num = 20;
				continue;
				IL_1A9:
				num = 1;
				continue;
				IL_2F5:
				count2 = list2.Count;
				num = 6;
				continue;
				IL_3D6:
				num = 12;
			}
			IL_AB:
			throw new ArgumentNullException(RecordTableEnumerator.b("䴹主圽㐿❁㙃", a_));
			IL_240:
			return;
			IL_42A:
			throw new ArgumentNullException(RecordTableEnumerator.b("䨹唻䠽⼿㙁၃❅⩇♉⥋", a_));
		}
		}
	}

	// Token: 0x0600396F RID: 14703 RVA: 0x002037D8 File Offset: 0x002027D8
	private static void ᜀ(List<int> A_0, int[] A_1)
	{
		int num = 2;
		for (;;)
		{
			if (true)
			{
			}
			int num2;
			switch (num)
			{
			case 0:
				goto IL_100;
			case 1:
				A_0.Add(-2);
				num = 13;
				continue;
			case 3:
				if (A_1[num2] == -2)
				{
					num = 1;
					continue;
				}
				goto IL_C2;
			case 4:
				return;
			case 5:
				if (A_1.Length != 0)
				{
					num = 7;
					continue;
				}
				goto IL_93;
			case 6:
				if (num2 >= A_1.Length)
				{
					num = 11;
					continue;
				}
				num = 3;
				continue;
			case 7:
				A_0.AddRange(A_1);
				num = 8;
				continue;
			case 8:
				goto IL_93;
			case 9:
				if (A_0.Count == 0)
				{
					num = 10;
					continue;
				}
				goto IL_93;
			case 10:
				num = 5;
				continue;
			case 11:
				return;
			case 12:
				goto IL_100;
			case 13:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_93;
				default:
					if (false)
					{
					}
					goto IL_C2;
				}
				break;
			}
			if (A_0.Count == A_1.Length)
			{
				num = 4;
				continue;
			}
			num = 9;
			continue;
			IL_93:
			num2 = 0;
			num = 12;
			continue;
			IL_C2:
			num2++;
			num = 0;
			continue;
			IL_100:
			num = 6;
		}
	}

	// Token: 0x06003970 RID: 14704 RVA: 0x0020393C File Offset: 0x0020293C
	private static void ᜀ(XmlWriter A_0, List<int> A_1, string A_2, XlsPivotTable A_3)
	{
		int a_ = 3;
		int num = 4;
		for (;;)
		{
			int num2;
			int count;
			switch (num)
			{
			case 0:
				goto IL_175;
			case 1:
				num = 12;
				continue;
			case 2:
				if (num2 >= count)
				{
					num = 7;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("倸", a_));
				A_0.WriteStartElement(RecordTableEnumerator.b("䄸", a_));
				A_0.WriteEndElement();
				A_0.WriteEndElement();
				num2++;
				goto IL_103;
			case 3:
				goto IL_170;
			case 5:
				if (A_1 == null)
				{
					num = 11;
					continue;
				}
				num = 8;
				continue;
			case 6:
				if (A_2 == RecordTableEnumerator.b("䬸吺䨼瘾㕀♂⡄㑆", a_))
				{
					num = 1;
					continue;
				}
				goto IL_1D8;
			case 7:
				goto IL_19C;
			case 8:
				if (A_2 == RecordTableEnumerator.b("娸吺儼瘾㕀♂⡄㑆", a_))
				{
					num = 9;
					continue;
				}
				goto IL_86;
			case 9:
				num = 10;
				continue;
			case 10:
				if (A_3.ColumnItemsStream != null)
				{
					num = 14;
					continue;
				}
				goto IL_86;
			case 11:
				goto IL_134;
			case 12:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_103;
				default:
					if (false)
					{
					}
					if (A_3.RowItemsStream != null)
					{
						num = 3;
						continue;
					}
					goto IL_1D8;
				}
				break;
			case 13:
				if (true)
				{
				}
				goto IL_175;
			case 14:
				goto IL_1C2;
			case 15:
				goto IL_67;
			}
			if (A_0 == null)
			{
				num = 15;
				continue;
			}
			num = 5;
			continue;
			IL_86:
			num = 6;
			continue;
			IL_103:
			num = 13;
			continue;
			IL_175:
			num = 2;
			continue;
			IL_1D8:
			A_0.WriteStartElement(A_2);
			num2 = 0;
			count = A_1.Count;
			num = 0;
		}
		IL_67:
		throw new ArgumentNullException(RecordTableEnumerator.b("丸䤺吼䬾⑀ㅂ", a_));
		IL_134:
		throw new ArgumentNullException(RecordTableEnumerator.b("唸䠺䤼社⡀♂⥄⍆㩈", a_));
		IL_170:
		A_3.RowItemsStream.Position = 0L;
		ShapeParser.WriteNodeFromStream(A_0, A_3.RowItemsStream);
		return;
		IL_19C:
		A_0.WriteEndElement();
		return;
		IL_1C2:
		A_3.ColumnItemsStream.Position = 0L;
		ShapeParser.WriteNodeFromStream(A_0, A_3.ColumnItemsStream);
	}

	// Token: 0x06003971 RID: 14705 RVA: 0x00203B94 File Offset: 0x00202B94
	private static void ᜄ(XmlWriter A_0, XlsPivotTable A_1)
	{
		int a_ = 10;
		switch (0)
		{
		default:
			for (;;)
			{
				new List<int>();
				List<XlsPivotField> list = new List<XlsPivotField>();
				int num = 0;
				int num2 = 36;
				for (;;)
				{
					int num3;
					int num6;
					string name;
					int index;
					switch (num2)
					{
					case 0:
						if (A_1.DataFields[num3].ShowDataAs != PivotFieldFormatType.Normal)
						{
							num2 = 31;
							continue;
						}
						goto IL_489;
					case 1:
						goto IL_592;
					case 2:
					{
						int num4;
						if (num4 > 1)
						{
							num2 = 16;
							continue;
						}
						goto IL_592;
					}
					case 3:
					{
						if (num >= A_1.PivotFields.Count)
						{
							num2 = 8;
							continue;
						}
						int num4 = 0;
						num2 = 29;
						continue;
					}
					case 4:
					{
						XlsPivotField xlsPivotField;
						if (xlsPivotField.Name != A_1.DataFields[num3].Name)
						{
							num2 = 30;
							continue;
						}
						goto IL_D6;
					}
					case 5:
						A_0.WriteStartElement(RecordTableEnumerator.b("┿㩁ぃ੅㭇㹉", a_));
						A_0.WriteStartElement(RecordTableEnumerator.b("┿㩁ぃ", a_), RecordTableEnumerator.b("⠿㙁ぃ㙅片敉捋㵍㍏㩑ㅓ㭕㥗⥙牛㍝य़šᙣ॥᭧թ੫ᩭ幯ᅱ᭳᭵坷ᕹ᩻᡽ꦅﮇ憎ﺋﺕﶗﾙ첟趡隣隥颧鎩莫鞭龯\udfb1햳\udfb5횷", a_));
						A_0.WriteAttributeString(RecordTableEnumerator.b("㔿ぁⵃ", a_), RecordTableEnumerator.b("㬿݁畃獅े祉穋୍恏网浓慕橗扙煛橝՟孡嵣䭥⥧剩啫Ɑ嵯䅱㉳䅵䩷䍹䵻㱽끿쒁솃낅낇", a_));
						A_0.WriteStartElement(RecordTableEnumerator.b("㠿獁灃", a_), RecordTableEnumerator.b("␿⍁ぃ❅็⍉⥋≍㑏⅑", a_), RecordTableEnumerator.b("⠿㙁ぃ㙅片敉捋㵍㍏㩑ㅓ㭕㥗⥙牛㍝य़šᙣ॥᭧թ੫ᩭ幯ᅱ᭳᭵坷ᕹ᩻᡽ꦅﮇ憎ﺋﺕﶗﾙ첟趡隣隥颧鎩莫鞭龯\udfb1햳\udfb5횷", a_));
						A_0.WriteAttributeString(RecordTableEnumerator.b("〿⭁㉃⥅㱇᥉⑋⅍❏ፑ❓", a_), A_1.DataFields[num3].ᜀ(A_1.DataFields[num3].ShowDataAs));
						A_0.WriteEndElement();
						A_0.WriteEndElement();
						A_0.WriteEndElement();
						num2 = 23;
						continue;
					case 6:
						if (!A_1.DataFields[num3].ᜂ())
						{
							num2 = 24;
							continue;
						}
						goto IL_489;
					case 7:
					{
						int num4;
						int num5;
						if (num5 >= num4)
						{
							num2 = 1;
							continue;
						}
						list.Add(A_1.PivotFields[num]);
						num5++;
						num2 = 18;
						continue;
					}
					case 8:
					{
						int count = list.Count;
						num2 = 27;
						continue;
					}
					case 9:
						goto IL_1A1;
					case 10:
						num2 = 2;
						continue;
					case 11:
						goto IL_313;
					case 12:
						if (A_1.DataFields[num3].ᜂ())
						{
							num2 = 5;
							continue;
						}
						goto IL_391;
					case 13:
						goto IL_363;
					case 14:
						goto IL_313;
					case 15:
						list.Add(A_1.PivotFields[num]);
						num6 = 0;
						num2 = 13;
						continue;
					case 16:
					{
						int num5 = 1;
						num2 = 17;
						continue;
					}
					case 17:
						goto IL_5A7;
					case 18:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_11F;
						default:
							if (false)
							{
							}
							goto IL_5A7;
						}
						break;
					case 19:
						goto IL_363;
					case 20:
						goto IL_489;
					case 21:
						if (A_1.DataFields[num6].Field.CacheField.Index == num)
						{
							num2 = 34;
							continue;
						}
						goto IL_1A1;
					case 22:
					{
						int count;
						if (num3 >= count)
						{
							num2 = 33;
							continue;
						}
						A_0.WriteStartElement(RecordTableEnumerator.b("␿⍁ぃ❅็⍉⥋≍㑏", a_));
						XlsPivotField xlsPivotField = list[num3];
						name = xlsPivotField.Name;
						index = xlsPivotField.CacheField.Index;
						num2 = 4;
						continue;
					}
					case 23:
						goto IL_391;
					case 24:
						if (true)
						{
						}
						A_0.WriteAttributeString(RecordTableEnumerator.b("㌿⩁⭃ㅅే⭉㡋⽍ᅏ⅑", a_), A_1.DataFields[num3].ᜀ(A_1.DataFields[num3].ShowDataAs));
						num2 = 20;
						continue;
					case 25:
						if (num6 >= A_1.DataFields.Count)
						{
							num2 = 10;
							continue;
						}
						num2 = 21;
						continue;
					case 26:
						goto IL_663;
					case 27:
					{
						int count;
						if (count > 0)
						{
							num2 = 32;
							continue;
						}
						return;
					}
					case 28:
						return;
					case 29:
						if (A_1.PivotFields[num].DataField)
						{
							num2 = 15;
							continue;
						}
						goto IL_592;
					case 30:
						name = A_1.DataFields[num3].Name;
						index = A_1.DataFields[num3].Field.CacheField.Index;
						num2 = 35;
						continue;
					case 31:
						num2 = 6;
						continue;
					case 32:
					{
						A_0.WriteStartElement(RecordTableEnumerator.b("␿⍁ぃ❅็⍉⥋≍㑏⅑", a_));
						int count;
						A_0.WriteAttributeString(RecordTableEnumerator.b("⌿ⵁㅃ⡅㱇", a_), count.ToString());
						num3 = 0;
						num2 = 14;
						continue;
					}
					case 33:
						A_0.WriteEndElement();
						num2 = 28;
						continue;
					case 34:
					{
						int num4;
						num4++;
						num2 = 9;
						continue;
					}
					case 35:
						goto IL_D6;
					case 36:
						goto IL_663;
					}
					break;
					IL_11F:
					num2 = 0;
					continue;
					IL_D6:
					A_0.WriteAttributeString(RecordTableEnumerator.b("⸿⍁⥃⍅", a_), name);
					A_0.WriteAttributeString(RecordTableEnumerator.b("☿⹁⁃", a_), index.ToString());
					spr\u2514.ᜂ(A_0, A_1.DataFields[num3].Subtotal);
					goto IL_11F;
					IL_1A1:
					num6++;
					num2 = 19;
					continue;
					IL_313:
					num2 = 22;
					continue;
					IL_363:
					num2 = 25;
					continue;
					IL_391:
					A_0.WriteEndElement();
					num3++;
					num2 = 11;
					continue;
					IL_489:
					A_0.WriteAttributeString(RecordTableEnumerator.b("∿⍁㝃⍅็⍉⥋≍㑏", a_), A_1.DataFields[num3].BaseField.ToString());
					A_0.WriteAttributeString(RecordTableEnumerator.b("∿⍁㝃⍅Ň㹉⥋⍍", a_), A_1.DataFields[num3].BaseItem.ToString());
					A_0.WriteAttributeString(RecordTableEnumerator.b("⸿㝁⥃E╇㹉Ջ⩍", a_), A_1.DataFields[num3].Field.NumberFormatIndex.ToString());
					num2 = 12;
					continue;
					IL_592:
					num++;
					num2 = 26;
					continue;
					IL_5A7:
					num2 = 7;
					continue;
					IL_663:
					num2 = 3;
				}
			}
			return;
		}
	}

	// Token: 0x06003972 RID: 14706 RVA: 0x0020427C File Offset: 0x0020327C
	internal static void ᜂ(XmlWriter A_0, SubtotalTypes A_1)
	{
		int a_ = 7;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 3;
				continue;
			case 1:
				A_0.WriteAttributeString(RecordTableEnumerator.b("丼䨾⍀㝂⩄㍆⡈❊", a_), ((PivotSubtotalTypes2007)A_1).ToString());
				num = 2;
				continue;
			case 2:
				return;
			case 3:
				if (A_1 == SubtotalTypes.None)
				{
					return;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_31;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				break;
			}
			goto IL_2D;
			IL_31:
			if (true)
			{
			}
			num = 0;
			continue;
			IL_2D:
			if (A_1 != SubtotalTypes.Default)
			{
				goto IL_31;
			}
			break;
		}
	}

	// Token: 0x06003973 RID: 14707 RVA: 0x00204338 File Offset: 0x00203338
	private static List<XlsPivotField> ᜃ(XmlWriter A_0, XlsPivotTable A_1)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 9;
			List<XlsPivotField> list;
			for (;;)
			{
				int num2;
				XlsPivotField xlsPivotField;
				switch (num)
				{
				case 0:
				{
					int count;
					if (num2 >= count)
					{
						num = 1;
						continue;
					}
					PivotTableFields pivotFields;
					xlsPivotField = pivotFields[num2];
					num = 4;
					continue;
				}
				case 1:
					goto IL_F1;
				case 2:
					goto IL_CB;
				case 3:
					goto IL_10A;
				case 4:
					if (xlsPivotField.Axis == AxisTypes.Page)
					{
						num = 5;
						continue;
					}
					goto IL_10A;
				case 5:
					list.Add(xlsPivotField);
					num = 3;
					continue;
				case 6:
					goto IL_61;
				case 7:
					if (A_1 == null)
					{
						num = 8;
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
						A_0.WriteStartElement(RecordTableEnumerator.b("㙅ⅇ㱉⍋㩍ᙏ㭑ㅓ㩕㱗⥙", a_));
						PivotTableFields pivotFields = A_1.PivotFields;
						list = new List<XlsPivotField>();
						num2 = 0;
						int count = pivotFields.Count;
						num = 2;
						continue;
					}
					}
					break;
				case 8:
					goto IL_C6;
				case 10:
					goto IL_CB;
				}
				if (A_0 == null)
				{
					num = 6;
					continue;
				}
				if (true)
				{
				}
				num = 7;
				continue;
				IL_CB:
				num = 0;
				continue;
				IL_10A:
				spr\u2514.ᜀ(A_0, xlsPivotField, A_1);
				num2++;
				num = 10;
			}
			IL_61:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅅ㩇⍉㡋⭍≏", a_));
			IL_C6:
			throw new ArgumentNullException(RecordTableEnumerator.b("㙅ⅇ㱉⍋㩍я㍑㙓㩕㵗", a_));
			IL_F1:
			A_0.WriteEndElement();
			return list;
		}
		}
	}

	// Token: 0x06003974 RID: 14708 RVA: 0x002044DC File Offset: 0x002034DC
	private static void ᜀ(XmlWriter A_0, XlsPivotField A_1, XlsPivotTable A_2)
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			int num = 15;
			for (;;)
			{
				AxisTypes axis;
				string caption;
				PivotTableLayoutType rowLayout;
				PivotFieldSortType? sortType;
				switch (num)
				{
				case 0:
					if (axis != AxisTypes.Data)
					{
						num = 38;
						continue;
					}
					goto IL_437;
				case 1:
					if (caption != null)
					{
						num = 8;
						continue;
					}
					goto IL_167;
				case 2:
					goto IL_112;
				case 3:
					spr\u2514.ᜂ(A_0, A_1);
					num = 35;
					continue;
				case 4:
					if (A_1 == null)
					{
						num = 5;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("䬺吼䤾⹀㝂̈́⹆ⱈ❊⥌", a_));
					num = 37;
					continue;
				case 5:
					goto IL_519;
				case 6:
					goto IL_112;
				case 7:
					num = 0;
					continue;
				case 8:
					num = 34;
					continue;
				case 9:
					goto IL_437;
				case 10:
					goto IL_572;
				case 11:
					goto IL_167;
				case 12:
					if (axis != AxisTypes.None)
					{
						num = 7;
						continue;
					}
					goto IL_437;
				case 13:
					A_0.WriteAttributeString(RecordTableEnumerator.b("为匼嘾぀㙂⁄੆ⱈ♊⽌⩎⍐͒❔㡖⥘㹚⽜⭞ᡠ", a_), caption);
					num = 11;
					continue;
				case 14:
					A_0.WriteAttributeString(RecordTableEnumerator.b("䠺䠼崾㕀ⱂㅄ♆╈ࡊⱌ㽎═㩒㩔㥖", a_), A_1.SubtotalCaption);
					num = 32;
					continue;
				case 16:
					goto IL_EB;
				case 17:
					if (axis != AxisTypes.Data)
					{
						num = 26;
						continue;
					}
					goto IL_7A8;
				case 18:
					A_0.WriteAttributeString(RecordTableEnumerator.b("䠺䠼崾㕀ⱂㅄ♆╈Ὂ≌㽎", a_), RecordTableEnumerator.b("଺", a_));
					num = 10;
					continue;
				case 19:
					goto IL_2D5;
				case 20:
					if (A_1.DataField)
					{
						num = 3;
						continue;
					}
					goto IL_51E;
				case 21:
					A_0.WriteAttributeString(RecordTableEnumerator.b("䠺刼䴾㕀ᝂ㱄㝆ⱈ", a_), A_1.SortType.ToString().ToLower());
					num = 16;
					continue;
				case 22:
					if (!string.IsNullOrEmpty(A_1.SubtotalCaption))
					{
						num = 14;
						continue;
					}
					goto IL_4D1;
				case 23:
					if (axis != AxisTypes.None)
					{
						num = 31;
						continue;
					}
					goto IL_7A8;
				case 24:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D2;
					default:
						if (false)
						{
						}
						A_0.WriteAttributeString(RecordTableEnumerator.b("堺刼刾ㅀ≂♄㍆", a_), RecordTableEnumerator.b("଺", a_));
						num = 6;
						continue;
					}
					break;
				case 25:
					A_0.WriteAttributeString(RecordTableEnumerator.b("吺䠼䬾ⵀ⩂⭄≆", a_), RecordTableEnumerator.b("଺", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("堺刼刾ㅀ≂♄㍆", a_), RecordTableEnumerator.b("଺", a_));
					num = 2;
					continue;
				case 26:
					num = 23;
					continue;
				case 27:
					if (rowLayout == PivotTableLayoutType.Outline)
					{
						num = 24;
						continue;
					}
					num = 30;
					continue;
				case 28:
					goto IL_E6;
				case 29:
					if (!A_1.SubtotalTop)
					{
						num = 18;
						continue;
					}
					goto IL_572;
				case 30:
					if (rowLayout == PivotTableLayoutType.Tabular)
					{
						num = 25;
						continue;
					}
					goto IL_112;
				case 31:
					spr\u2514.ᜁ(A_0, A_1);
					num = 19;
					continue;
				case 32:
					goto IL_4D1;
				case 33:
					A_0.WriteAttributeString(RecordTableEnumerator.b("唺尼刾⑀", a_), A_1.Name);
					num = 36;
					continue;
				case 34:
					if (caption.Length > 0)
					{
						num = 13;
						continue;
					}
					goto IL_167;
				case 35:
					goto IL_51E;
				case 36:
					goto IL_13A;
				case 37:
					if (A_1.Name != A_1.CacheField.Name)
					{
						num = 33;
						continue;
					}
					goto IL_13A;
				case 38:
				{
					PivotAxisTypes2007 pivotAxisTypes = (PivotAxisTypes2007)axis;
					A_0.WriteAttributeString(RecordTableEnumerator.b("娺䔼嘾㉀", a_), pivotAxisTypes.ToString());
					num = 9;
					continue;
				}
				case 39:
					if (sortType != null)
					{
						num = 21;
						continue;
					}
					goto IL_EB;
				}
				goto IL_CC;
				IL_D2:
				if (true)
				{
				}
				num = 28;
				continue;
				IL_CC:
				if (A_0 == null)
				{
					goto IL_D2;
				}
				num = 4;
				continue;
				IL_EB:
				caption = A_1.Caption;
				num = 1;
				continue;
				IL_112:
				num = 29;
				continue;
				IL_13A:
				num = 22;
				continue;
				IL_167:
				spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("唺䠼刾݀⹂ㅄๆⵈ", a_), A_1.NumberFormatIndex, 0);
				num = 20;
				continue;
				IL_437:
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("娺䠼䬾⹀၂ⵄ⡆㹈", a_), A_1.IsAutoShow, false);
				rowLayout = A_2.Options.RowLayout;
				num = 27;
				continue;
				IL_4D1:
				axis = A_1.Axis;
				num = 12;
				continue;
				IL_51E:
				spr\u2514.ᜁ(A_0, A_1.Subtotals);
				num = 17;
				continue;
				IL_572:
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("强似帾♀ూ⍄ⅆ", a_), A_1.CanDragOff, true);
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("强似帾♀ᝂ⩄ц♈❊", a_), A_1.CanDragToColumn, true);
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("强似帾♀ᝂ⩄͆⡈㽊ⱌ", a_), A_1.CanDragToData, true);
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("强似帾♀ᝂ⩄ᝆ⡈ⱊ⡌", a_), A_1.CanDragToPage, true);
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("强似帾♀ᝂ⩄ᕆ♈㱊", a_), A_1.CanDragToRow, true);
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("区吼嬾⑀ൂ⁄うH㽊⡌≎≐", a_), !A_1.ShowNewItemsOnRefresh, true);
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("刺匼尾ⵀ㙂⅄≆݈⹊㩌َ═㙒㡔⑖ၘ㕚᭜㙞ൠᝢdᕦ", a_), A_1.ShowNewItemsInFilter, false);
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("刺匼䰾⑀ㅂㅄՆ╈⩊⍌⑎͐㱒≔", a_), A_1.ShowBlankRow, false);
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("刺匼䰾⑀ㅂㅄᝆ⡈ⱊ⡌ൎ⍐㙒㑔㱖", a_), A_1.ShowPageBreak, false);
				spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("刺䤼娾ⱀፂ⑄⁆ⱈࡊ≌㩎㽐❒", a_), A_1.ItemsPerPage, 10);
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("嘺堼帾㉀㙂㝄≆཈≊⅌㭎㑐⅒", a_), A_1.IsMeasureField, false);
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("嘺䠼匾㕀⩂㕄⭆ⱈɊ㥌⩎㱐Rご㭖㱘㡚⥜㙞๠ൢ⑤୦ըѪᩬ੮ᕰ", a_), A_1.IsMultiSelected, false);
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("䠺唼倾㙀ɂ⥄⭆", a_), A_1.IsShowAllItems, true);
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("䠺唼倾㙀݂㝄⡆㥈ཊ≌㡎㽐⁒", a_), A_1.ShowDropDown, false);
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("䠺唼倾㙀ፂ㝄⡆㥈੊㹌౎ぐ⍒⅔㹖㙘㕚", a_), A_1.ShowPropAsCaption, false);
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("䠺唼倾㙀ፂ㝄⡆㥈Ὂ⑌㽎", a_), A_1.ShowToolTip, false);
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("强堼夾⁀㙂⥄㍆ࡈ㽊㥌㵎㡐ㅒ⁔⍖㱘὚⽜㙞ൠར㙤፦ࡨὪ࡬", a_), A_1.IsDefaultDrill, false);
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("强尼䬾⁀၂⩄㉆㭈⡊⡌ᱎ㹐⅒⅔", a_), A_1.IsDataSourceSorted, false);
				spr\u2514.ᜀ(A_0, RecordTableEnumerator.b("娺儼匾Հㅂⱄ⭆╈⹊⥌", a_), A_1.IsAllDrilled, false);
				sortType = A_1.SortType;
				num = 39;
			}
			IL_E6:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰺似嘾㕀♂㝄", a_));
			IL_2D5:
			goto IL_7A8;
			IL_519:
			throw new ArgumentNullException(RecordTableEnumerator.b("崺吼娾ⵀ❂", a_));
			IL_7A8:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06003975 RID: 14709 RVA: 0x00204C98 File Offset: 0x00203C98
	private static void ᜁ(XmlWriter A_0, SubtotalTypes A_1)
	{
		int a_ = 3;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 != SubtotalTypes.Default)
				{
					num = 1;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_46;
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
			case 1:
				goto IL_46;
			case 3:
				return;
			case 4:
				goto IL_51;
			case 5:
				goto IL_3C;
			}
			if (A_0 == null)
			{
				num = 5;
				continue;
			}
			num = 0;
			continue;
			IL_46:
			if (A_1 != SubtotalTypes.None)
			{
				goto IL_C6;
			}
			num = 4;
		}
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("丸䤺吼䬾⑀ㅂ", a_));
		IL_51:
		spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("崸帺嬼帾㑀⽂ㅄᑆ㱈⥊㥌⁎═㉒㥔", a_), false, true);
		return;
		IL_C6:
		spr\u2514.ᜀ(A_0, A_1, SubtotalTypes.Sum, false, RecordTableEnumerator.b("䨸为值氾㑀⅂ㅄ⡆㵈⩊⅌", a_));
		spr\u2514.ᜀ(A_0, A_1, SubtotalTypes.Counta, false, RecordTableEnumerator.b("娸吺䠼儾㕀ɂᙄ㉆⭈㽊≌㭎ぐ㽒", a_));
		spr\u2514.ᜀ(A_0, A_1, SubtotalTypes.Average, false, RecordTableEnumerator.b("堸䴺娼氾㑀⅂ㅄ⡆㵈⩊⅌", a_));
		spr\u2514.ᜀ(A_0, A_1, SubtotalTypes.Max, false, RecordTableEnumerator.b("吸娺䔼氾㑀⅂ㅄ⡆㵈⩊⅌", a_));
		spr\u2514.ᜀ(A_0, A_1, SubtotalTypes.Min, false, RecordTableEnumerator.b("吸刺匼氾㑀⅂ㅄ⡆㵈⩊⅌", a_));
		spr\u2514.ᜀ(A_0, A_1, SubtotalTypes.Product, false, RecordTableEnumerator.b("䤸䤺刼嬾㑀⁂ㅄᑆ㱈⥊㥌⁎═㉒㥔", a_));
		spr\u2514.ᜀ(A_0, A_1, SubtotalTypes.Count, false, RecordTableEnumerator.b("娸吺䠼儾㕀၂い╆㵈⑊㥌⹎㵐", a_));
		spr\u2514.ᜀ(A_0, A_1, SubtotalTypes.Stdev, false, RecordTableEnumerator.b("䨸伺夼笾⑀㕂ᙄ㉆⭈㽊≌㭎ぐ㽒", a_));
		spr\u2514.ᜀ(A_0, A_1, SubtotalTypes.Stdevp, false, RecordTableEnumerator.b("䨸伺夼笾⑀㕂ᕄᑆ㱈⥊㥌⁎═㉒㥔", a_));
		spr\u2514.ᜀ(A_0, A_1, SubtotalTypes.Var, false, RecordTableEnumerator.b("伸娺似氾㑀⅂ㅄ⡆㵈⩊⅌", a_));
		spr\u2514.ᜀ(A_0, A_1, SubtotalTypes.Varp, false, RecordTableEnumerator.b("伸娺似漾ቀ㙂❄㍆♈㽊ⱌ⍎", a_));
	}

	// Token: 0x06003976 RID: 14710 RVA: 0x00204E80 File Offset: 0x00203E80
	private static void ᜀ(XmlWriter A_0, SubtotalTypes A_1, SubtotalTypes A_2, bool A_3, string A_4)
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
		bool a_ = (A_1 & A_2) != SubtotalTypes.None;
		spr\u1B7A.ᜀ(A_0, A_4, a_, A_3);
	}

	// Token: 0x06003977 RID: 14711 RVA: 0x00204ED0 File Offset: 0x00203ED0
	private static void ᜂ(XmlWriter A_0, XlsPivotField A_1)
	{
		int a_ = 11;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("╀≂ㅄ♆཈≊⡌⍎㕐", a_), A_1.DataField, false);
	}

	// Token: 0x06003978 RID: 14712 RVA: 0x00204F30 File Offset: 0x00203F30
	private static void ᜁ(XmlWriter A_0, XlsPivotField A_1)
	{
		int a_ = 14;
		switch (0)
		{
		default:
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_39D:
				try
				{
					num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
						{
							KeyValuePair<int, spr\u1B6A> keyValuePair;
							if (keyValuePair.Value != null)
							{
								num = 2;
								continue;
							}
							goto IL_1F8;
						}
						case 2:
						{
							KeyValuePair<int, spr\u1B6A> keyValuePair;
							spr\u2514.ᜀ(A_0, keyValuePair.Key, keyValuePair.Value);
							num = 6;
							continue;
						}
						case 4:
						{
							Dictionary<int, spr\u1B6A>.Enumerator enumerator;
							if (!enumerator.MoveNext())
							{
								num = 5;
								continue;
							}
							KeyValuePair<int, spr\u1B6A> keyValuePair = enumerator.Current;
							A_0.WriteStartElement(RecordTableEnumerator.b("ⵃ㉅ⵇ❉", a_));
							num = 7;
							continue;
						}
						case 5:
							num = 9;
							continue;
						case 6:
							goto IL_1F8;
						case 7:
						{
							KeyValuePair<int, spr\u1B6A> keyValuePair;
							if (keyValuePair.Key != -1)
							{
								num = 10;
								continue;
							}
							goto IL_18D;
						}
						case 8:
							goto IL_18D;
						case 9:
							goto IL_218;
						case 10:
						{
							KeyValuePair<int, spr\u1B6A> keyValuePair;
							A_0.WriteAttributeString(RecordTableEnumerator.b("㱃", a_), keyValuePair.Key.ToString());
							num = 8;
							continue;
						}
						}
						goto IL_11B;
						IL_18D:
						num = 1;
						continue;
						IL_1D2:
						num = 4;
						continue;
						IL_11B:
						goto IL_1D2;
						IL_1F8:
						A_0.WriteEndElement();
						num = 3;
					}
					IL_218:
					goto IL_3CD;
				}
				finally
				{
					Dictionary<int, spr\u1B6A>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				goto IL_22B;
			default:
				if (false)
				{
				}
				goto IL_7C;
			}
			SortedList<spr\u2514.ᜀ, object> sortedList;
			Dictionary<int, spr\u1B6A> dictionary;
			for (;;)
			{
				IL_35:
				switch (num)
				{
				case 0:
				{
					spr\u1B6A spr_u1B6A = new spr\u1B6A();
					spr_u1B6A.ᜀ(PivotItemType.Default);
					A_1.ItemOptions.Add(-1, spr_u1B6A);
					num = 4;
					continue;
				}
				case 1:
					goto IL_2E6;
				case 2:
					goto IL_2E1;
				case 3:
					goto IL_307;
				case 4:
					goto IL_30C;
				case 5:
					goto IL_340;
				case 6:
				{
					A_0.WriteStartElement(RecordTableEnumerator.b("ⵃ㉅ⵇ❉㽋", a_));
					int num2 = 0;
					int count = sortedList.Count;
					num = 8;
					continue;
				}
				case 7:
					if (dictionary.Count > 0)
					{
						num = 14;
						continue;
					}
					num = 11;
					continue;
				case 8:
					goto IL_2E6;
				case 9:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 3;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("ⵃ㉅ⵇ❉", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("㱃", a_), sortedList.Keys[num2].ᜁ.ToString());
					A_0.WriteEndElement();
					num2++;
					num = 1;
					continue;
				}
				case 10:
				{
					int num3;
					if (num3 >= sortedList.Count)
					{
						num = 0;
						continue;
					}
					A_1.ItemOptions.Add(sortedList.Keys[num3].ᜁ, null);
					num3++;
					num = 2;
					continue;
				}
				case 11:
					if (sortedList.Count > 0)
					{
						num = 6;
						continue;
					}
					goto IL_3CD;
				case 12:
					goto IL_39D;
				case 13:
				{
					int num3 = 0;
					num = 5;
					continue;
				}
				case 14:
				{
					A_0.WriteStartElement(RecordTableEnumerator.b("ⵃ㉅ⵇ❉㽋", a_));
					Dictionary<int, spr\u1B6A>.Enumerator enumerator = dictionary.GetEnumerator();
					num = 12;
					continue;
				}
				case 15:
					if (dictionary == null)
					{
						num = 13;
						continue;
					}
					goto IL_30C;
				}
				goto IL_7C;
				IL_2E6:
				num = 9;
				continue;
				IL_30C:
				num = 7;
			}
			IL_2E1:
			goto IL_22B;
			IL_307:
			goto IL_3CD;
			IL_340:
			goto IL_22B;
			IL_7C:
			if (true)
			{
			}
			sortedList = spr\u2514.ᜀ(A_1.CacheField);
			dictionary = A_1.ItemOptions;
			num = 15;
			goto IL_35;
			IL_22B:
			num = 10;
			goto IL_35;
			IL_3CD:
			spr\u2514.ᜀ(A_0, A_1.Subtotals);
			spr\u2514.ᜀ(A_0, A_1);
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06003979 RID: 14713 RVA: 0x00205340 File Offset: 0x00204340
	private static void ᜀ(XmlWriter A_0, XlsPivotField A_1)
	{
		int a_ = 3;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_A3;
			case 1:
				goto IL_40;
			case 2:
			{
				Stream preservedAutoSort;
				preservedAutoSort.Position = 0L;
				ShapeParser.WriteNodeFromStream(A_0, preservedAutoSort);
				num = 0;
				continue;
			}
			case 3:
			{
				if (A_1 == null)
				{
					num = 4;
					continue;
				}
				Stream preservedAutoSort = A_1.PreservedAutoSort;
				num = 6;
				continue;
			}
			case 4:
				goto IL_EF;
			case 6:
			{
				Stream preservedAutoSort;
				if (preservedAutoSort != null)
				{
					num = 2;
					continue;
				}
				return;
			}
			}
			if (A_0 == null)
			{
				num = 1;
			}
			else
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_75;
				}
				if (false)
				{
				}
				num = 3;
			}
		}
		IL_40:
		IL_75:
		throw new ArgumentNullException(RecordTableEnumerator.b("丸䤺吼䬾⑀ㅂ", a_));
		IL_A3:
		if (true)
		{
		}
		return;
		IL_EF:
		throw new ArgumentNullException(RecordTableEnumerator.b("弸刺堼匾╀", a_));
	}

	// Token: 0x0600397A RID: 14714 RVA: 0x00205444 File Offset: 0x00204444
	private static void ᜀ(XmlWriter A_0, int A_1, spr\u1B6A A_2)
	{
		int a_ = 18;
		int num = 7;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_1E4;
			case 1:
				A_0.WriteAttributeString(RecordTableEnumerator.b("ⵇ", a_), RecordTableEnumerator.b("祇", a_));
				num = 17;
				continue;
			case 2:
				goto IL_F5;
			case 3:
				goto IL_1BE;
			case 4:
				if (A_2.ᜄ())
				{
					num = 24;
					continue;
				}
				goto IL_F5;
			case 5:
				goto IL_2D7;
			case 6:
				if (A_2.ᜉ() != null)
				{
					num = 9;
					continue;
				}
				goto IL_11B;
			case 8:
				A_0.WriteAttributeString(RecordTableEnumerator.b("⹇", a_), RecordTableEnumerator.b("祇", a_));
				num = 0;
				continue;
			case 9:
				A_0.WriteAttributeString(RecordTableEnumerator.b("♇", a_), A_2.ᜉ());
				num = 13;
				continue;
			case 10:
				goto IL_9B;
			case 11:
				goto IL_372;
			case 12:
				if (A_2.ᜃ())
				{
					num = 19;
					continue;
				}
				goto IL_A0;
			case 13:
				goto IL_11B;
			case 14:
				A_0.WriteAttributeString(RecordTableEnumerator.b("ⱇ", a_), RecordTableEnumerator.b("祇", a_));
				num = 5;
				continue;
			case 15:
				goto IL_1DF;
			case 16:
				if (A_2.ᜇ())
				{
					num = 27;
					continue;
				}
				goto IL_1BE;
			case 17:
				goto IL_31D;
			case 18:
				goto IL_A0;
			case 19:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_328;
				default:
					if (false)
					{
					}
					A_0.WriteAttributeString(RecordTableEnumerator.b("╇", a_), RecordTableEnumerator.b("祇", a_));
					num = 18;
					continue;
				}
				break;
			case 20:
				if (A_2.ᜈ())
				{
					num = 1;
					continue;
				}
				goto IL_31D;
			case 21:
				if (A_2.ᜅ())
				{
					num = 26;
					continue;
				}
				goto IL_372;
			case 22:
				goto IL_328;
			case 23:
				if (A_2.ᜆ())
				{
					num = 14;
					continue;
				}
				goto IL_2D7;
			case 24:
				A_0.WriteAttributeString(RecordTableEnumerator.b("⭇", a_), RecordTableEnumerator.b("祇", a_));
				num = 2;
				continue;
			case 25:
				if (A_2.ᜀ())
				{
					num = 15;
					continue;
				}
				goto IL_3CA;
			case 26:
				A_0.WriteAttributeString(RecordTableEnumerator.b("⁇", a_), RecordTableEnumerator.b("祇", a_));
				num = 11;
				continue;
			case 27:
				A_0.WriteAttributeString(RecordTableEnumerator.b("㭇", a_), RecordTableEnumerator.b("祇", a_));
				num = 3;
				continue;
			}
			if (A_1 == -1)
			{
				num = 10;
				continue;
			}
			num = 4;
			continue;
			IL_A0:
			num = 6;
			continue;
			IL_F5:
			num = 23;
			continue;
			IL_11B:
			num = 16;
			continue;
			IL_1BE:
			num = 25;
			continue;
			IL_1E4:
			num = 21;
			continue;
			IL_328:
			if (A_2.ᜂ())
			{
				if (true)
				{
				}
				num = 8;
				continue;
			}
			goto IL_1E4;
			IL_2D7:
			num = 20;
			continue;
			IL_31D:
			num = 22;
			continue;
			IL_372:
			num = 12;
		}
		IL_9B:
		spr\u2514.ᜀ(A_0, A_2.ᜁ());
		return;
		IL_1DF:
		A_0.WriteAttributeString(RecordTableEnumerator.b("㭇⹉", a_), RecordTableEnumerator.b("祇", a_));
		return;
		IL_3CA:
		A_0.WriteAttributeString(RecordTableEnumerator.b("㭇⹉", a_), RecordTableEnumerator.b("硇", a_));
	}

	// Token: 0x0600397B RID: 14715 RVA: 0x00205840 File Offset: 0x00204840
	private static void ᜀ(XmlWriter A_0, PivotItemType A_1)
	{
		int a_ = 14;
		if (true)
		{
		}
		PivotSubtotalItems2007[] array = (PivotSubtotalItems2007[])Enum.GetValues(typeof(XLSXPivotItemType));
		if (A_1 == PivotItemType.Default)
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
				A_0.WriteAttributeString(RecordTableEnumerator.b("ぃ", a_), RecordTableEnumerator.b("⁃⍅⹇⭉㥋≍⑏", a_));
				return;
			}
		}
		A_0.WriteAttributeString(RecordTableEnumerator.b("ぃ", a_), ((XLSXPivotItemType)A_1).ToString());
	}

	// Token: 0x0600397C RID: 14716 RVA: 0x002058E4 File Offset: 0x002048E4
	private static void ᜀ(XmlWriter A_0, SubtotalTypes A_1)
	{
		int a_ = 6;
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
					A_0.WriteStartElement(RecordTableEnumerator.b("唻䨽┿⽁", a_));
					PivotSubtotalItems2007 pivotSubtotalItems;
					A_0.WriteAttributeString(RecordTableEnumerator.b("䠻", a_), pivotSubtotalItems.ToString());
					A_0.WriteEndElement();
					num = 11;
					continue;
				}
				case 2:
				{
					PivotSubtotalItems2007 pivotSubtotalItems;
					if ((A_1 & (SubtotalTypes)pivotSubtotalItems) != SubtotalTypes.None)
					{
						num = 1;
						continue;
					}
					goto IL_17A;
				}
				case 3:
				{
					if (A_1 == SubtotalTypes.Default)
					{
						num = 10;
						continue;
					}
					PivotSubtotalItems2007[] array = (PivotSubtotalItems2007[])Enum.GetValues(typeof(PivotSubtotalItems2007));
					num2 = 0;
					int num3 = array.Length;
					num = 8;
					continue;
				}
				case 4:
				{
					PivotSubtotalItems2007 pivotSubtotalItems;
					if (pivotSubtotalItems != (PivotSubtotalItems2007)0)
					{
						num = 12;
						continue;
					}
					goto IL_17A;
				}
				case 5:
					goto IL_138;
				case 6:
					goto IL_154;
				case 7:
					num = 3;
					continue;
				case 8:
					goto IL_138;
				case 9:
				{
					int num3;
					if (num2 >= num3)
					{
						num = 6;
						continue;
					}
					if (true)
					{
					}
					PivotSubtotalItems2007[] array;
					PivotSubtotalItems2007 pivotSubtotalItems = array[num2];
					num = 4;
					continue;
				}
				case 10:
					goto IL_175;
				case 11:
					goto IL_17A;
				case 12:
					num = 2;
					continue;
				}
				if (A_1 != SubtotalTypes.None)
				{
					num = 7;
					continue;
				}
				break;
				IL_138:
				num = 9;
				continue;
				IL_17A:
				num2++;
				num = 5;
			}
			IL_154:
			return;
			IL_175:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			default:
				if (false)
				{
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("唻䨽┿⽁", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("䠻", a_), RecordTableEnumerator.b("堻嬽☿⍁ㅃ⩅㱇", a_));
				A_0.WriteEndElement();
				return;
			}
			break;
		}
		}
	}

	// Token: 0x0600397D RID: 14717 RVA: 0x00205AE4 File Offset: 0x00204AE4
	private static SortedList<spr\u2514.ᜀ, object> ᜀ(XlsPivotCacheField A_0)
	{
		switch (0)
		{
		default:
		{
			SortedList<spr\u2514.ᜀ, object> sortedList;
			for (;;)
			{
				sortedList = new SortedList<spr\u2514.ᜀ, object>();
				int num = 0;
				int itemCount = A_0.ItemCount;
				int num2 = 6;
				for (;;)
				{
					IL_10:
					switch (num2)
					{
					case 0:
					{
						object value;
						while (Convert.ToString(value).Length >= 0)
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
								num2 = 1;
								goto IL_10;
							}
						}
						goto IL_50;
					}
					case 1:
					{
						object value;
						sortedList[new spr\u2514.ᜀ
						{
							ᜀ = value,
							ᜁ = num
						}] = null;
						num2 = 5;
						continue;
					}
					case 2:
					{
						if (num >= itemCount)
						{
							num2 = 4;
							continue;
						}
						object value = A_0.GetValue(num);
						num2 = 0;
						continue;
					}
					case 3:
						goto IL_D6;
					case 4:
						goto IL_F2;
					case 5:
						goto IL_50;
					case 6:
						goto IL_D6;
					}
					break;
					IL_50:
					num++;
					num2 = 3;
					continue;
					IL_D6:
					num2 = 2;
				}
			}
			IL_F2:
			if (true)
			{
			}
			return sortedList;
		}
		}
	}

	// Token: 0x0600397E RID: 14718 RVA: 0x00205BF8 File Offset: 0x00204BF8
	private static void ᜂ(XmlWriter A_0, XlsPivotTable A_1)
	{
		int a_ = 8;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_79;
			case 1:
				goto IL_50;
			case 3:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				goto IL_97;
			}
			if (A_0 == null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_97;
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
				num = 3;
			}
		}
		IL_50:
		throw new ArgumentNullException(RecordTableEnumerator.b("䤽㈿⭁ぃ⍅㩇", a_));
		IL_79:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("丽⤿㑁⭃㉅᱇⭉⹋≍㕏", a_));
		IL_97:
		A_0.WriteStartElement(RecordTableEnumerator.b("刽⼿⅁╃㉅ⅇ╉≋", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("䰽┿⑁", a_), A_1.Location.RangeAddressLocal);
		spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("崽⼿⹁ᑃ❅⽇⽉ཋ⅍╏㱑⁓", a_), A_1.ColumnsPerPage, 0);
		spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("䰽⼿㕁ᑃ❅⽇⽉ཋ⅍╏㱑⁓", a_), A_1.RowsPerPage, 0);
		spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("堽⤿ぁ㝃㉅G⽉ⵋ⩍㕏⁑ٓ㥕⽗", a_), A_1.FirstHeaderRow, -1);
		spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("堽⤿ぁ㝃㉅ే⭉㡋⽍ɏ㵑⍓", a_), A_1.FirstDataRow, -1);
		spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("堽⤿ぁ㝃㉅ే⭉㡋⽍ፏ㵑㡓", a_), A_1.FirstDataCol, -1);
		A_0.WriteEndElement();
	}

	// Token: 0x0600397F RID: 14719 RVA: 0x00205D68 File Offset: 0x00204D68
	private static void ᜁ(XmlWriter A_0, XlsPivotTable A_1)
	{
		int a_ = 17;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 != null)
				{
					num = 4;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_73;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			case 2:
				goto IL_3C;
			case 3:
				goto IL_DB;
			case 4:
				if (!A_1.PreservedElements.ContainsKey(RecordTableEnumerator.b("㝆⁈㵊≌㭎ᥐ㩒ご╖㡘⥚㹜㝞ࡠ٢ᙤ", a_)))
				{
					num = 5;
					continue;
				}
				goto IL_DD;
			case 5:
				return;
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
		IL_3C:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("う㭈≊㥌⩎⍐", a_));
		IL_73:
		throw new ArgumentNullException(RecordTableEnumerator.b("㝆⁈㵊≌㭎Ր㉒㝔㭖㱘", a_));
		IL_DB:
		goto IL_73;
		IL_DD:
		Stream stream = A_1.PreservedElements[RecordTableEnumerator.b("㝆⁈㵊≌㭎ᥐ㩒ご╖㡘⥚㹜㝞ࡠ٢ᙤ", a_)];
		stream.Position = 0L;
		ShapeParser.WriteNodeFromStream(A_0, stream);
	}

	// Token: 0x06003980 RID: 14720 RVA: 0x00205E7C File Offset: 0x00204E7C
	private static void ᜀ(XmlWriter A_0, XlsPivotTable A_1)
	{
		int a_ = 17;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				goto IL_3C;
			case 2:
				if (A_1 != null)
				{
					num = 3;
					continue;
				}
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6B;
				default:
					if (false)
					{
					}
					num = 4;
					continue;
				}
				break;
			case 3:
				if (!A_1.PreservedElements.ContainsKey(RecordTableEnumerator.b("㕆♈㱊Ռ♎㑐⅒㑔╖㩘㍚㑜㩞በ㙢ᙤ٦๨๪", a_)))
				{
					num = 0;
					continue;
				}
				goto IL_DD;
			case 4:
				goto IL_DB;
			}
			if (A_0 == null)
			{
				num = 1;
			}
			else
			{
				num = 2;
			}
		}
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("う㭈≊㥌⩎⍐", a_));
		IL_6B:
		throw new ArgumentNullException(RecordTableEnumerator.b("㝆⁈㵊≌㭎Ր㉒㝔㭖㱘", a_));
		IL_DB:
		goto IL_6B;
		IL_DD:
		Stream stream = A_1.PreservedElements[RecordTableEnumerator.b("㕆♈㱊Ռ♎㑐⅒㑔╖㩘㍚㑜㩞በ㙢ᙤ٦๨๪", a_)];
		stream.Position = 0L;
		ShapeParser.WriteNodeFromStream(A_0, stream);
	}

	// Token: 0x06003981 RID: 14721 RVA: 0x00205F90 File Offset: 0x00204F90
	internal static void ᜀ(XmlWriter A_0, string A_1, bool A_2, bool A_3)
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
		spr\u1B7A.ᜀ(A_0, A_1, A_2, A_3);
	}

	// Token: 0x06003982 RID: 14722 RVA: 0x00205FD4 File Offset: 0x00204FD4
	internal static void ᜀ(XmlWriter A_0, string A_1, byte A_2)
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
		A_0.WriteAttributeString(A_1, XmlConvert.ToString(A_2));
	}

	// Token: 0x06003983 RID: 14723 RVA: 0x0020601C File Offset: 0x0020501C
	internal static void ᜀ(XmlWriter A_0, string A_1, ushort A_2)
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
		A_0.WriteAttributeString(A_1, XmlConvert.ToString(A_2));
	}

	// Token: 0x06003984 RID: 14724 RVA: 0x00206064 File Offset: 0x00205064
	internal static void ᜀ(XmlWriter A_0, string A_1, uint A_2)
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
		A_0.WriteAttributeString(A_1, XmlConvert.ToString(A_2));
	}

	// Token: 0x06003985 RID: 14725 RVA: 0x002060AC File Offset: 0x002050AC
	internal static void ᜀ(XmlWriter A_0, string A_1, int A_2)
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
		A_0.WriteAttributeString(A_1, XmlConvert.ToString(A_2));
	}

	// Token: 0x06003986 RID: 14726 RVA: 0x002060F4 File Offset: 0x002050F4
	internal static bool ᜀ(XlsPivotTable A_0)
	{
		for (;;)
		{
			int num;
			int num2;
			int count;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_96:
				num = 5;
				break;
			default:
				if (false)
				{
				}
				num2 = 0;
				count = A_0.PivotFields.Count;
				num = 4;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					if (num2 >= count)
					{
						num = 1;
						continue;
					}
					XlsPivotField xlsPivotField = A_0.PivotFields[num2];
					num = 2;
					continue;
				}
				case 1:
					return false;
				case 2:
				{
					XlsPivotField xlsPivotField;
					if (xlsPivotField.DataField)
					{
						goto IL_96;
					}
					num2++;
					num = 3;
					continue;
				}
				case 3:
					if (true)
					{
					}
					goto IL_A3;
				case 4:
					goto IL_A3;
				case 5:
					return true;
				}
				break;
				IL_A3:
				num = 0;
			}
		}
		return true;
	}

	// Token: 0x04001927 RID: 6439
	private const int ᜀ = -2;

	// Token: 0x020003B2 RID: 946
	private class ᜀ : IComparable
	{
		// Token: 0x06003988 RID: 14728 RVA: 0x002061D8 File Offset: 0x002051D8
		public int ᜀ(object A_0)
		{
			spr\u2514.ᜀ ᜀ;
			int num;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_66:
				num = this.ᜂ.Compare(this.ᜀ, ᜀ.ᜀ);
				num2 = 4;
				break;
			case 1:
				goto IL_20;
			default:
				goto IL_20;
			}
			for (;;)
			{
				IL_28:
				switch (num2)
				{
				case 0:
					if (ᜀ != null)
					{
						num2 = 1;
						continue;
					}
					return num;
				case 1:
					goto IL_66;
				case 2:
					num = this.ᜂ.Compare(this.ᜁ, ᜀ.ᜁ);
					num2 = 3;
					continue;
				case 3:
					return num;
				case 4:
					if (num == 0)
					{
						num2 = 2;
						continue;
					}
					return num;
				}
				goto IL_42;
			}
			return num;
			IL_20:
			if (false)
			{
			}
			IL_42:
			if (true)
			{
			}
			ᜀ = (A_0 as spr\u2514.ᜀ);
			num = 1;
			num2 = 0;
			goto IL_28;
		}

		// Token: 0x04001928 RID: 6440
		public object ᜀ;

		// Token: 0x04001929 RID: 6441
		public int ᜁ;

		// Token: 0x0400192A RID: 6442
		public IComparer ᜂ = new spr\u2514.ᜀ.ᜀ();

		// Token: 0x020003B3 RID: 947
		private class ᜀ : IComparer
		{
			// Token: 0x0600398A RID: 14730 RVA: 0x002062D0 File Offset: 0x002052D0
			public int ᜀ(object A_0, object A_1)
			{
				int result;
				try
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
					result = this.ᜀ.Compare(A_0, A_1);
				}
				catch
				{
					result = A_0.GetHashCode() - A_1.GetHashCode();
				}
				return result;
			}

			// Token: 0x0400192B RID: 6443
			private IComparer ᜀ = Comparer<object>.Default;
		}
	}
}
