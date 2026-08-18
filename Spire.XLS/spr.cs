using System;
using System.Collections;
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

// Token: 0x02000303 RID: 771
internal class spr\u2005
{
	// Token: 0x06002F80 RID: 12160 RVA: 0x001AB128 File Offset: 0x001AA128
	public static void ᜑ(XmlReader A_0, XlsPivotTable A_1)
	{
		int a_ = 13;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_702:
			goto IL_16CB;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_2D5;
			}
			break;
		}
		int num;
		spr\u1A79 spr_u1A;
		XlsWorkbook xlsWorkbook;
		for (;;)
		{
			IL_42:
			PivotReportFilter pivotReportFilter;
			int num2;
			bool? flag2;
			bool? flag3;
			switch (num)
			{
			case 0:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⁂い㑆㵈⑊⁌͎㡐⁒⅔і㙘⥚⥜", a_)))
				{
					num = 76;
					continue;
				}
				goto IL_6B4;
			case 1:
				goto IL_1573;
			case 2:
				goto IL_CF3;
			case 3:
				spr_u1A.ᜋ(XmlConvert.ToBoolean(A_0.Value));
				num = 144;
				continue;
			case 4:
				spr_u1A.ᜇ(XmlConvert.ToBoolean(A_0.Value));
				num = 129;
				continue;
			case 5:
				spr_u1A.ᜂ(!XmlConvert.ToBoolean(A_0.Value));
				num = 162;
				continue;
			case 6:
				spr_u1A.ᜅ(XmlConvert.ToBoolean(A_0.Value));
				num = 78;
				continue;
			case 7:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("あⵄ⡆㹈ཊⱌ㭎ぐݒ㱔❖⩘", a_)))
				{
					num = 27;
					continue;
				}
				goto IL_707;
			case 8:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㍂⑄⁆ⱈъ㭌⩎⍐ݒ㵔㉖㝘὚㉜⡞འ", a_)))
				{
					num = 57;
					continue;
				}
				goto IL_12A8;
			case 9:
				goto IL_707;
			case 10:
				goto IL_B98;
			case 11:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⹂ⱄ㑆㩈≊⍌⡎ቐ㉒╔⍖じ㑚㍜", a_)))
				{
					num = 50;
					continue;
				}
				goto IL_1027;
			case 12:
				goto IL_51B;
			case 13:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("≂㕄㝆╈㉊ཌ⁎⍐㝒ご╖὘㑚⽜㉞`ᝢᙤ", a_)))
				{
					num = 113;
					continue;
				}
				goto IL_EE0;
			case 14:
			{
				XlsPivotField xlsPivotField;
				if (xlsPivotField.ItemIndex > -1)
				{
					num = 47;
					continue;
				}
				pivotReportFilter.IsMultipleSelect = xlsPivotField.IsMultiSelected;
				pivotReportFilter.FieldIndex = xlsPivotField.FieldIndex;
				Dictionary<int, spr\u1B6A>.Enumerator enumerator = xlsPivotField.ItemOptions.GetEnumerator();
				num = 34;
				continue;
			}
			case 15:
				goto IL_167A;
			case 16:
				A_1.ErrorString = A_0.Value;
				num = 61;
				continue;
			case 17:
				goto IL_152E;
			case 18:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("あⵄ⡆㹈ࡊⱌ⍎㉐Ṓ㝔╖⩘", a_)))
				{
					num = 107;
					continue;
				}
				goto IL_346;
			case 19:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("ㅂ⩄うň⹊ⱌ⭎㑐⅒ᙔ㙖⥘⽚㑜ぞའ", a_)))
				{
					num = 156;
					continue;
				}
				goto IL_587;
			case 20:
				num = 71;
				continue;
			case 21:
				spr_u1A.ᜀ(A_0.Value);
				num = 106;
				continue;
			case 22:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("ㅂ⩄う่㥊ⱌⅎ㕐ݒ㩔⍖㡘㝚⹜", a_)))
				{
					num = 92;
					continue;
				}
				goto IL_A49;
			case 23:
				goto IL_3F6;
			case 24:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㍂⑄⁆ⱈ᱊㽌⹎⅐", a_)))
				{
					num = 124;
					continue;
				}
				goto IL_5BD;
			case 25:
				A_1.Options.RowLayout = PivotTableLayoutType.Compact;
				num = 93;
				continue;
			case 26:
				goto IL_D4B;
			case 27:
				spr_u1A.\u171A(XmlConvert.ToBoolean(A_0.Value));
				num = 9;
				continue;
			case 28:
				spr_u1A.\u1714(XmlConvert.ToBoolean(A_0.Value));
				num = 38;
				continue;
			case 29:
				goto IL_1322;
			case 30:
				spr_u1A.\u1718(XmlConvert.ToBoolean(A_0.Value));
				num = 120;
				continue;
			case 31:
				goto IL_15A9;
			case 32:
				spr_u1A.ᜀ(XmlConvert.ToByte(A_0.Value));
				num = 35;
				continue;
			case 33:
				goto IL_5BD;
			case 34:
				try
				{
					num = 2;
					for (;;)
					{
						KeyValuePair<int, spr\u1B6A> keyValuePair;
						switch (num)
						{
						case 0:
							if (keyValuePair.Key > -1)
							{
								num = 1;
								continue;
							}
							break;
						case 1:
							num = 7;
							continue;
						case 4:
							num = 10;
							continue;
						case 5:
							if (!keyValuePair.Value.ᜅ())
							{
								num = 9;
								continue;
							}
							break;
						case 6:
						{
							Dictionary<int, spr\u1B6A>.Enumerator enumerator;
							if (!enumerator.MoveNext())
							{
								num = 4;
								continue;
							}
							keyValuePair = enumerator.Current;
							num = 0;
							continue;
						}
						case 7:
							if (keyValuePair.Value != null)
							{
								num = 8;
								continue;
							}
							goto IL_14C5;
						case 8:
							num = 5;
							continue;
						case 9:
							goto IL_14C5;
						case 10:
							goto IL_151B;
						}
						IL_1475:
						num = 6;
						continue;
						goto IL_1475;
						IL_14C5:
						XlsPivotField xlsPivotField;
						pivotReportFilter.FilterItemStrings.Add(A_1.Cache.CacheFields.ᜀ(xlsPivotField.FieldIndex).Items[keyValuePair.Key].ToString());
						num = 3;
					}
					IL_151B:
					goto IL_15DF;
				}
				finally
				{
					Dictionary<int, spr\u1B6A>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				goto IL_152E;
			case 35:
				goto IL_75F;
			case 36:
				goto IL_10B5;
			case 37:
				A_1.EnableDrilldown = XmlConvert.ToBoolean(A_0.Value);
				num = 97;
				continue;
			case 38:
				goto IL_B0A;
			case 39:
				spr_u1A.ᜁ(XmlConvert.ToBoolean(A_0.Value));
				num = 1;
				continue;
			case 40:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("♂⅄⹆㵈ཊⱌ㭎ぐ", a_)))
				{
					num = 94;
					continue;
				}
				goto IL_F52;
			case 41:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("≂㕄㝆╈㉊ୌ⁎㽐❒ፔ㡖⭘㙚㱜⭞በ", a_)))
				{
					num = 77;
					continue;
				}
				goto IL_C29;
			case 42:
				goto IL_10B0;
			case 43:
				A_1.ShowDataFieldInRow = XmlConvert.ToBoolean(A_0.Value);
				num = 139;
				continue;
			case 44:
				goto IL_31D;
			case 45:
				A_1.RepeatItemsOnEachPrintedPage = XmlConvert.ToBoolean(A_0.Value);
				num = 83;
				continue;
			case 46:
				A_1.EnableWizard = XmlConvert.ToBoolean(A_0.Value);
				num = 2;
				continue;
			case 47:
			{
				pivotReportFilter.IsMultipleSelect = false;
				XlsPivotField xlsPivotField;
				pivotReportFilter.FieldIndex = xlsPivotField.FieldIndex;
				pivotReportFilter.ItemIndex = xlsPivotField.ItemIndex;
				pivotReportFilter.FilterItemStrings.Add(A_1.Cache.CacheFields.ᜀ(xlsPivotField.FieldIndex).Items[xlsPivotField.ItemIndex].ToString());
				num = 146;
				continue;
			}
			case 48:
				goto IL_12A8;
			case 49:
				spr_u1A.ᜀ(XmlConvert.ToBoolean(A_0.Value));
				num = 31;
				continue;
			case 50:
				A_1.NullString = A_0.Value;
				num = 122;
				continue;
			case 51:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("ⱂい㍆╈≊⍌⩎", a_)))
				{
					num = 105;
					continue;
				}
				goto IL_3F6;
			case 52:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("ⵂ⑄⩆ⱈ", a_)))
				{
					num = 160;
					continue;
				}
				goto IL_10B5;
			case 53:
				spr_u1A.\u1717(XmlConvert.ToBoolean(A_0.Value));
				num = 143;
				continue;
			case 54:
				A_1.Options.RowLayout = PivotTableLayoutType.Tabular;
				num = 133;
				continue;
			case 55:
				spr_u1A.ᜑ(XmlConvert.ToBoolean(A_0.Value));
				num = 158;
				continue;
			case 56:
				goto IL_6B4;
			case 57:
			{
				bool flag = XmlConvert.ToBoolean(A_0.Value);
				num = 121;
				continue;
			}
			case 58:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("♂㝄㕆♈㥊์⹎⅐❒㱔㡖㝘", a_)))
				{
					num = 16;
					continue;
				}
				goto IL_820;
			case 59:
				goto IL_BF3;
			case 60:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㍂㝄≆㩈⹊㽌㥎㑐ᕒ㩔╖㑘㩚⥜⭞ࡠൢɤ", a_)))
				{
					num = 6;
					continue;
				}
				goto IL_5F3;
			case 61:
				goto IL_820;
			case 62:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("♂⭄♆⭈❊⡌ᡎ㡐⥒㑔╖㵘", a_)))
				{
					num = 46;
					continue;
				}
				goto IL_CF3;
			case 63:
				goto IL_587;
			case 64:
				goto IL_EE0;
			case 65:
				spr_u1A.ᜑ(XmlConvert.ToBoolean(A_0.Value));
				num = 12;
				continue;
			case 66:
				spr_u1A.ᜀ(XmlConvert.ToUInt16(A_0.Value));
				num = 96;
				continue;
			case 67:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㙂㙄≆ࡈ㹊㥌⁎ᝐ㱒❔㩖㡘⽚⥜㙞འѢ", a_)))
				{
					num = 151;
					continue;
				}
				goto IL_105D;
			case 68:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("╂ⱄ≆╈⽊ᵌ㵎㡐㵒⅔͖じ⽚ㅜ㩞በ", a_)))
				{
					num = 28;
					continue;
				}
				goto IL_B0A;
			case 69:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("≂㕄㝆╈㉊͌㩎㱐ㅒご╖὘㑚⽜㉞`ᝢᙤ", a_)))
				{
					num = 3;
					continue;
				}
				goto IL_EAD;
			case 70:
			{
				if (num2 >= A_1.PageFields.Count)
				{
					num = 88;
					continue;
				}
				XlsPivotField xlsPivotField = A_1.PageFields[num2] as PivotField;
				num = 14;
				continue;
			}
			case 71:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⁂⑄⑆ⅈ⹊ь⭎", a_)))
				{
					num = 145;
					continue;
				}
				goto IL_950;
			case 72:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("❂⑄㍆⡈ᭊ≌㱎㡐❒㱔㡖㝘", a_)))
				{
					num = 66;
					continue;
				}
				goto IL_8B1;
			case 73:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⁂⑄⑆ⅈ⹊ь⭎", a_)))
				{
					num = 142;
					continue;
				}
				goto IL_1701;
			case 74:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⑂㝄⹆ⵈཊ㽌⁎⅐॒㩔㥖㱘⡚", a_)))
				{
					num = 53;
					continue;
				}
				goto IL_13B0;
			case 75:
				if (flag2 == null)
				{
					num = 25;
					continue;
				}
				A_1.Options.RowLayout = PivotTableLayoutType.Outline;
				num = 115;
				continue;
			case 76:
				spr_u1A.ᜉ(XmlConvert.ToBoolean(A_0.Value));
				num = 56;
				continue;
			case 77:
				spr_u1A.ᜏ(XmlConvert.ToBoolean(A_0.Value));
				num = 87;
				continue;
			case 78:
				goto IL_5F3;
			case 79:
				spr_u1A.ᜁ(XmlConvert.ToByte(A_0.Value));
				num = 29;
				continue;
			case 80:
				goto IL_105D;
			case 81:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("♂⭄♆⭈❊⡌ॎ㡐㙒㥔㍖क़⥚㉜⽞Ѡᅢᅤ๦౨ᡪ", a_)))
				{
					num = 114;
					continue;
				}
				goto IL_C9E;
			case 82:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⁂㝄≆⡈㽊⡌⭎ݐ㙒❔⑖じ㑚㍜", a_)))
				{
					num = 32;
					continue;
				}
				goto IL_75F;
			case 83:
				goto IL_44E;
			case 84:
				spr_u1A.ᜀ(PivotPageAreaFieldsOrderType.OverThenDown);
				num = 48;
				continue;
			case 85:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("❂ⱄ㑆⡈⥊⅌⩎ᝐ㩒ご㭖㵘᝚㑜ⱞᕠ", a_)))
				{
					num = 5;
					continue;
				}
				goto IL_64B;
			case 86:
				goto IL_A49;
			case 87:
				goto IL_C29;
			case 88:
				return;
			case 89:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("❂⑄㍆⡈ࡊⱌ㽎═㩒㩔㥖", a_)))
				{
					num = 21;
					continue;
				}
				goto IL_1358;
			case 90:
				goto IL_551;
			case 91:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("≂㙄㍆ⱈ㥊⑌㱎㩐ݒ㩔⍖㡘㝚⹜", a_)))
				{
					num = 65;
					continue;
				}
				goto IL_51B;
			case 92:
				A_1.IsRowGrand = XmlConvert.ToBoolean(A_0.Value);
				num = 86;
				continue;
			case 93:
				goto IL_AD4;
			case 94:
				spr_u1A.\u1712(XmlConvert.ToBoolean(A_0.Value));
				num = 99;
				continue;
			case 95:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⁂⩄⩆㥈⩊⹌㭎", a_)))
				{
					num = 17;
					continue;
				}
				goto IL_31D;
			case 96:
				goto IL_8B1;
			case 97:
				goto IL_1644;
			case 98:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⩂ㅄ≆⑈ᭊ㽌♎㽐❒Ŕ㹖ⵘ㝚㡜ⱞ", a_)))
				{
					num = 45;
					continue;
				}
				goto IL_44E;
			case 99:
				goto IL_F52;
			case 100:
				spr_u1A.ᜆ(XmlConvert.ToBoolean(A_0.Value));
				num = 26;
				continue;
			case 101:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("❂⑄㍆⡈ъ⍌ᵎ㹐⑒♔", a_)))
				{
					num = 43;
					continue;
				}
				goto IL_FF1;
			case 102:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⹂⁄㕆⹈⹊ь㭎㑐㹒", a_)))
				{
					num = 4;
					continue;
				}
				goto IL_B40;
			case 103:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("╂ⱄ≆╈⽊Ō♎≐❒ٔ㡖⭘⽚ᱜⱞɠ٢୤ͦhժ੬", a_)))
				{
					num = 100;
					continue;
				}
				goto IL_D4B;
			case 104:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⹂い⭆㵈≊㵌⍎㑐ᕒ㱔㉖㕘㽚᭜㙞ൠᝢdᕦᩨ", a_)))
				{
					num = 39;
					continue;
				}
				goto IL_1573;
			case 105:
				flag3 = new bool?(XmlConvert.ToBoolean(A_0.Value));
				num = 23;
				continue;
			case 106:
				goto IL_1358;
			case 107:
				spr_u1A.\u1713(XmlConvert.ToBoolean(A_0.Value));
				num = 154;
				continue;
			case 108:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("≂㕄㝆╈㉊ౌ⍎㡐㑒㭔㩖㱘㕚⥜ᥞ๠ᅢࡤ٦ᵨᡪ", a_)))
				{
					num = 119;
					continue;
				}
				goto IL_551;
			case 109:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("≂㕄㝆╈㉊ᵌ⹎═❒ご╖㝘ᵚ㉜ⵞౠɢᅤᑦ", a_)))
				{
					num = 137;
					continue;
				}
				goto IL_91D;
			case 110:
				goto IL_7B4;
			case 111:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("あⵄ⡆㹈๊㽌㵎㹐⅒", a_)))
				{
					num = 117;
					continue;
				}
				goto IL_7B4;
			case 112:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㙂㕄⍆⡈㽊⡌⭎ݐ㙒❔⑖じ㑚㍜", a_)))
				{
					num = 136;
					continue;
				}
				goto IL_A7F;
			case 113:
				spr_u1A.ᜄ(XmlConvert.ToBoolean(A_0.Value));
				num = 64;
				continue;
			case 114:
				spr_u1A.\u1715(XmlConvert.ToBoolean(A_0.Value));
				num = 157;
				continue;
			case 115:
				goto IL_AD4;
			case 116:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("≂㕄㝆╈㉊ᩌ♎㕐❒㵔ὖ㱘㉚㩜㝞ᕠ╢੤ᕦѨ੪ᥬᱮ", a_)))
				{
					num = 30;
					continue;
				}
				goto IL_A13;
			case 117:
				A_1.DisplayErrorString = XmlConvert.ToBoolean(A_0.Value);
				num = 110;
				continue;
			case 118:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⁂⩄⭆ň⹊ⱌ⭎㑐⅒ᙔ㙖⥘⽚㑜ぞའ", a_)))
				{
					num = 132;
					continue;
				}
				goto IL_16CB;
			case 119:
				spr_u1A.ᜈ(XmlConvert.ToBoolean(A_0.Value));
				num = 90;
				continue;
			case 120:
				goto IL_A13;
			case 121:
			{
				bool flag;
				if (flag)
				{
					num = 84;
					continue;
				}
				goto IL_12A8;
			}
			case 122:
				goto IL_1027;
			case 123:
				goto IL_67E;
			case 124:
				spr_u1A.ᜀ(XmlConvert.ToInt32(A_0.Value));
				num = 33;
				continue;
			case 125:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("あⵄ⡆㹈͊⡌⹎㕐㙒❔⑖", a_)))
				{
					if (true)
					{
					}
					num = 49;
					continue;
				}
				goto IL_15A9;
			case 126:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("♂⭄♆⭈❊⡌୎⍐㩒㥔㭖", a_)))
				{
					num = 37;
					continue;
				}
				goto IL_1644;
			case 127:
				if (xlsWorkbook.Options == ExcelParseOptions.DoNotParsePivotTable)
				{
					num = 20;
					continue;
				}
				num = 52;
				continue;
			case 128:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("≂㙄㍆ⱈ㥊⑌㱎㩐ݒ㩔⍖㡘㝚⹜", a_)))
				{
					num = 55;
					continue;
				}
				goto IL_8E7;
			case 129:
				goto IL_B40;
			case 130:
				goto IL_37C;
			case 131:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⩂⭄⍆ⱈ╊㥌", a_)))
				{
					num = 134;
					continue;
				}
				goto IL_B98;
			case 132:
				spr_u1A.ᜂ(A_0.Value);
				num = 147;
				continue;
			case 133:
				goto IL_AD4;
			case 134:
				spr_u1A.ᜀ(XmlConvert.ToUInt32(A_0.Value));
				num = 10;
				continue;
			case 135:
				goto IL_167A;
			case 136:
				spr_u1A.ᜂ(XmlConvert.ToByte(A_0.Value));
				num = 149;
				continue;
			case 137:
				spr_u1A.ᜃ(XmlConvert.ToBoolean(A_0.Value));
				num = 155;
				continue;
			case 138:
				if (flag3 == null)
				{
					num = 54;
					continue;
				}
				num = 75;
				continue;
			case 139:
				goto IL_FF1;
			case 140:
				A_1.ShowColumnGrand = XmlConvert.ToBoolean(A_0.Value);
				num = 130;
				continue;
			case 141:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⁂⩄⭆่㥊ⱌⅎ㕐ݒ㩔⍖㡘㝚⹜", a_)))
				{
					num = 140;
					continue;
				}
				goto IL_37C;
			case 142:
				A_1.CacheIndex = XmlConvert.ToInt32(A_0.Value);
				num = 161;
				continue;
			case 143:
				goto IL_13B0;
			case 144:
				goto IL_EAD;
			case 145:
				A_1.CacheIndex = XmlConvert.ToInt32(A_0.Value);
				num = 42;
				continue;
			case 146:
				goto IL_15DF;
			case 147:
				goto IL_702;
			case 148:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("あⵄ⡆㹈ي⑌㱎≐㩒㭔ざ祘", a_)))
				{
					num = 159;
					continue;
				}
				goto IL_BF3;
			case 149:
				goto IL_A7F;
			case 150:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⁂⩄⭆่㥊ⱌⅎ㕐ݒ㩔⍖㡘㝚⹜", a_)))
				{
					num = 152;
					continue;
				}
				goto IL_67E;
			case 151:
				spr_u1A.\u1719(XmlConvert.ToBoolean(A_0.Value));
				num = 80;
				continue;
			case 152:
				A_1.IsColumnGrand = XmlConvert.ToBoolean(A_0.Value);
				num = 123;
				continue;
			case 153:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⹂ⱄ⥆ᭈ⹊⭌㵎㑐⁒㵔㙖㭘㝚㡜फ़Ѡᅢᙤ๦٨ժ", a_)))
				{
					num = 79;
					continue;
				}
				goto IL_1322;
			case 154:
				goto IL_346;
			case 155:
				goto IL_91D;
			case 156:
				spr_u1A.ᜃ(A_0.Value);
				num = 63;
				continue;
			case 157:
				goto IL_C9E;
			case 158:
				goto IL_8E7;
			case 159:
				A_1.DisplayNullString = XmlConvert.ToBoolean(A_0.Value);
				num = 59;
				continue;
			case 160:
				A_1.Name = A_0.Value;
				num = 36;
				continue;
			case 161:
				goto IL_1701;
			case 162:
				goto IL_64B;
			}
			goto IL_2D5;
			IL_31D:
			num = 138;
			continue;
			IL_346:
			num = 98;
			continue;
			IL_37C:
			num = 118;
			continue;
			IL_3F6:
			num = 95;
			continue;
			IL_44E:
			num = 131;
			continue;
			IL_51B:
			num = 141;
			continue;
			IL_551:
			num = 116;
			continue;
			IL_587:
			num = 82;
			continue;
			IL_5BD:
			num = 104;
			continue;
			IL_5F3:
			num = 67;
			continue;
			IL_64B:
			num = 40;
			continue;
			IL_67E:
			num = 74;
			continue;
			IL_6B4:
			num = 89;
			continue;
			IL_707:
			num = 68;
			continue;
			IL_75F:
			num = 112;
			continue;
			IL_7B4:
			num = 11;
			continue;
			IL_820:
			num = 125;
			continue;
			IL_8B1:
			num = 85;
			continue;
			IL_8E7:
			num = 102;
			continue;
			IL_91D:
			num = 108;
			continue;
			IL_A13:
			num = 91;
			continue;
			IL_A49:
			num = 150;
			continue;
			IL_A7F:
			num = 153;
			continue;
			IL_AD4:
			num = 8;
			continue;
			IL_B0A:
			num = 128;
			continue;
			IL_B40:
			num = 24;
			continue;
			IL_B98:
			num = 22;
			continue;
			IL_BF3:
			num = 18;
			continue;
			IL_C29:
			num = 109;
			continue;
			IL_C9E:
			num = 62;
			continue;
			IL_CF3:
			num = 58;
			continue;
			IL_D4B:
			A_0.Read();
			spr\u2005.\u170D(A_0, A_1);
			spr\u2005.ᜌ(A_0, A_1);
			spr\u2005.ᜋ(A_0, A_1);
			spr\u2005.ᜊ(A_0, A_1);
			spr\u2005.ᜉ(A_0, A_1);
			spr\u2005.ᜈ(A_0, A_1);
			spr\u2005.ᜇ(A_0, A_1);
			spr\u2005.ᜅ(A_0, A_1);
			spr\u2005.ᜏ(A_0, A_1);
			spr\u2005.ᜃ(A_0, A_1);
			spr\u2005.ᜐ(A_0, A_1);
			spr\u2005.ᜂ(A_0, A_1);
			spr\u2005.ᜁ(A_0, A_1);
			spr\u2005.ᜀ(A_0, A_1);
			spr\u2005.ᜎ(A_0, A_1);
			A_1.ReportFilters = new CollectionExtended<PivotReportFilter>();
			pivotReportFilter = new PivotReportFilter();
			num2 = 0;
			num = 135;
			continue;
			IL_EAD:
			num = 13;
			continue;
			IL_EE0:
			num = 41;
			continue;
			IL_F52:
			num = 126;
			continue;
			IL_FF1:
			num = 72;
			continue;
			IL_1027:
			num = 148;
			continue;
			IL_105D:
			num = 7;
			continue;
			IL_10B5:
			num = 73;
			continue;
			IL_12A8:
			num = 60;
			continue;
			IL_1322:
			num = 0;
			continue;
			IL_1358:
			num = 101;
			continue;
			IL_13B0:
			flag3 = null;
			flag2 = null;
			num = 51;
			continue;
			IL_152E:
			flag2 = new bool?(XmlConvert.ToBoolean(A_0.Value));
			num = 44;
			continue;
			IL_1573:
			num = 103;
			continue;
			IL_15A9:
			num = 111;
			continue;
			IL_15DF:
			A_1.ReportFilters.Add(pivotReportFilter);
			num2++;
			num = 15;
			continue;
			IL_1644:
			num = 81;
			continue;
			IL_167A:
			num = 70;
			continue;
			IL_1701:
			num = 69;
		}
		IL_950:
		A_0.MoveToElement();
		XlsWorksheet worksheet;
		worksheet.PreservePivotTables.Add(ShapeParser.ReadNodeAsStream(A_0));
		return;
		IL_10B0:
		goto IL_950;
		IL_2D5:
		spr_u1A = (A_1.Options as spr\u1A79);
		worksheet = A_1.Worksheet;
		xlsWorkbook = (worksheet.Workbook as XlsWorkbook);
		num = 127;
		goto IL_42;
		IL_16CB:
		num = 19;
		goto IL_42;
	}

	// Token: 0x06002F81 RID: 12161 RVA: 0x001AC8C4 File Offset: 0x001AB8C4
	private static void ᜐ(XmlReader A_0, XlsPivotTable A_1)
	{
		int a_ = 6;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.LocalName != RecordTableEnumerator.b("弻嘽ℿぁぃE❇㡉⅋⽍⑏⅑", a_))
				{
					num = 1;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A6;
				default:
					goto IL_D7;
				}
				break;
			case 1:
				return;
			case 2:
				if (A_1 == null)
				{
					num = 4;
					continue;
				}
				if (true)
				{
				}
				num = 0;
				continue;
			case 3:
				goto IL_46;
			case 4:
				goto IL_BF;
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			IL_A6:
			num = 2;
		}
		IL_46:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬻䰽⤿㙁⅃㑅", a_));
		IL_BF:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰻圽㘿ⵁぃቅ⥇⡉⁋⭍", a_));
		IL_D7:
		if (false)
		{
		}
		Stream value = ShapeParser.ReadNodeAsStream(A_0);
		A_1.PreservedElements.Add(RecordTableEnumerator.b("弻嘽ℿぁぃE❇㡉⅋⽍⑏⅑", a_), value);
	}

	// Token: 0x06002F82 RID: 12162 RVA: 0x001AC9D0 File Offset: 0x001AB9D0
	private static void ᜏ(XmlReader A_0, XlsPivotTable A_1)
	{
		int a_ = 11;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				if (A_0.LocalName != RecordTableEnumerator.b("❀ⱂ㝄⩆⡈㽊㹌", a_))
				{
					num = 0;
					continue;
				}
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_9E;
				default:
					goto IL_D7;
				}
				break;
			case 2:
				goto IL_B7;
			case 3:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				num = 1;
				continue;
			case 4:
				goto IL_46;
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			IL_9E:
			num = 3;
		}
		IL_46:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙀ㅂⱄ㍆ⱈ㥊", a_));
		IL_B7:
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅀ⩂㍄⡆㵈Ὂⱌⵎ㵐㙒", a_));
		IL_D7:
		if (false)
		{
		}
		Stream value = ShapeParser.ReadNodeAsStream(A_0);
		A_1.PreservedElements.Add(RecordTableEnumerator.b("❀ⱂ㝄⩆⡈㽊㹌", a_), value);
	}

	// Token: 0x06002F83 RID: 12163 RVA: 0x001ACADC File Offset: 0x001ABADC
	private static void ᜎ(XmlReader A_0, XlsPivotTable A_1)
	{
		int a_ = 19;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.LocalName != RecordTableEnumerator.b("⽈≊⅌㭎㑐⅒♔", a_))
				{
					num = 4;
					continue;
				}
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_9E;
				default:
					goto IL_D7;
				}
				break;
			case 1:
				goto IL_46;
			case 2:
				goto IL_B7;
			case 4:
				return;
			case 5:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				num = 0;
				continue;
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			IL_9E:
			num = 5;
		}
		IL_46:
		throw new ArgumentNullException(RecordTableEnumerator.b("㹈㥊⑌㭎㑐⅒", a_));
		IL_B7:
		throw new ArgumentNullException(RecordTableEnumerator.b("㥈≊㭌⁎═ݒ㑔㕖㕘㹚", a_));
		IL_D7:
		if (false)
		{
		}
		Stream value = ShapeParser.ReadNodeAsStream(A_0);
		A_1.PreservedElements.Add(RecordTableEnumerator.b("⽈≊⅌㭎㑐⅒♔", a_), value);
	}

	// Token: 0x06002F84 RID: 12164 RVA: 0x001ACBE8 File Offset: 0x001ABBE8
	private static void \u170D(XmlReader A_0, XlsPivotTable A_1)
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			string text;
			for (;;)
			{
				text = null;
				int num = 16;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1A1;
						default:
							if (false)
							{
							}
							A_1.RowsPerPage = XmlConvert.ToInt32(A_0.Value);
							num = 14;
							continue;
						}
						break;
					case 1:
						goto IL_1A1;
					case 2:
						if (A_0.MoveToAttribute(RecordTableEnumerator.b("娻圽㈿ㅁぃๅⵇ⭉⡋⭍≏Q㭓⅕", a_)))
						{
							num = 10;
							continue;
						}
						goto IL_249;
					case 3:
						if (A_0.MoveToAttribute(RecordTableEnumerator.b("娻圽㈿ㅁぃɅ⥇㹉ⵋ്㽏㹑", a_)))
						{
							num = 12;
							continue;
						}
						goto IL_349;
					case 4:
						text = A_0.Value;
						num = 17;
						continue;
					case 5:
						goto IL_E2;
					case 6:
						if (A_0.MoveToAttribute(RecordTableEnumerator.b("娻圽㈿ㅁぃɅ⥇㹉ⵋᱍ㽏║", a_)))
						{
							num = 9;
							continue;
						}
						goto IL_1C8;
					case 7:
						if (text == null)
						{
							num = 13;
							continue;
						}
						num = 22;
						continue;
					case 8:
						goto IL_2A4;
					case 9:
						A_1.FirstDataRow = XmlConvert.ToInt32(A_0.Value);
						num = 15;
						continue;
					case 10:
						A_1.FirstHeaderRow = XmlConvert.ToInt32(A_0.Value);
						num = 23;
						continue;
					case 11:
						if (A_0.MoveToAttribute(RecordTableEnumerator.b("主嬽☿", a_)))
						{
							num = 4;
							continue;
						}
						goto IL_2A9;
					case 12:
						A_1.FirstDataCol = XmlConvert.ToInt32(A_0.Value);
						num = 8;
						continue;
					case 13:
						goto IL_2C7;
					case 14:
						goto IL_A2;
					case 15:
						goto IL_1C8;
					case 16:
						if (A_0 == null)
						{
							num = 19;
							continue;
						}
						num = 21;
						continue;
					case 17:
						goto IL_2A9;
					case 18:
						if (A_0.MoveToAttribute(RecordTableEnumerator.b("主儽㜿ቁ╃ⅅⵇॉ⍋㭍㹏♑", a_)))
						{
							num = 0;
							continue;
						}
						goto IL_A2;
					case 19:
						goto IL_9D;
					case 20:
						A_1.ColumnsPerPage = XmlConvert.ToInt32(A_0.Value);
						num = 5;
						continue;
					case 21:
						if (A_1 == null)
						{
							num = 1;
							continue;
						}
						num = 11;
						continue;
					case 22:
						if (A_0.MoveToAttribute(RecordTableEnumerator.b("弻儽ⰿቁ╃ⅅⵇॉ⍋㭍㹏♑", a_)))
						{
							num = 20;
							continue;
						}
						goto IL_E2;
					case 23:
						goto IL_249;
					}
					break;
					IL_A2:
					num = 2;
					continue;
					IL_E2:
					num = 18;
					continue;
					IL_1C8:
					num = 3;
					continue;
					IL_249:
					if (true)
					{
					}
					num = 6;
					continue;
					IL_2A9:
					num = 7;
				}
			}
			IL_9D:
			throw new ArgumentNullException(RecordTableEnumerator.b("主嬽ℿ♁⅃㑅", a_));
			IL_1A1:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰻圽㘿ⵁぃቅ⥇⡉⁋⭍", a_));
			IL_2A4:
			goto IL_349;
			IL_2C7:
			throw new Exception(RecordTableEnumerator.b("渻嬽☿❁㙃⍅♇⥉⥋", a_));
			IL_349:
			XlsWorkbook workbook = A_1.Workbook;
			FormulaUtil formulaUtil = workbook.DataHolder.\u1718().ᜀ();
			Ptg[] array = formulaUtil.ᜃ(text);
			sprỜ sprỜ = array[0] as sprỜ;
			IWorksheet worksheet = A_1.Worksheet;
			A_1.Location = (CellRange)sprỜ.ᜀ(workbook, worksheet);
			A_0.Read();
			return;
		}
		}
	}

	// Token: 0x06002F85 RID: 12165 RVA: 0x001ACF8C File Offset: 0x001ABF8C
	private static void ᜌ(XmlReader A_0, XlsPivotTable A_1)
	{
		int a_ = 17;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_86:
			num = 7;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				num = 0;
				break;
			}
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_101;
			case 2:
			{
				PivotTableFields pivotTableFields;
				int num2;
				XlsPivotField a_2 = pivotTableFields[num2];
				spr\u2005.ᜀ(A_0, a_2, A_1);
				num2++;
				num = 1;
				continue;
			}
			case 3:
			{
				if (A_1 == null)
				{
					num = 4;
					continue;
				}
				PivotTableFields pivotTableFields = A_1.InternalFields;
				XlsPivotCache cache = A_1.Cache;
				cache.CacheFields;
				A_0.Read();
				int num2 = 0;
				num = 8;
				continue;
			}
			case 4:
				goto IL_FF;
			case 5:
				goto IL_126;
			case 6:
				goto IL_79;
			case 7:
				if (A_0.LocalName == RecordTableEnumerator.b("㝆⁈㵊≌㭎ᝐ㩒ご㭖㵘", a_))
				{
					num = 2;
					continue;
				}
				goto IL_101;
			case 8:
				goto IL_101;
			case 9:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 5;
					continue;
				}
				goto IL_86;
			}
			if (A_0 == null)
			{
				num = 6;
				continue;
			}
			num = 3;
			continue;
			IL_101:
			num = 9;
		}
		IL_79:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("う㭈≊㥌⩎⍐", a_));
		IL_FF:
		throw new ArgumentNullException(RecordTableEnumerator.b("㝆⁈㵊≌㭎Ր㉒㝔㭖㱘", a_));
		IL_126:
		A_0.Read();
	}

	// Token: 0x06002F86 RID: 12166 RVA: 0x001AD11C File Offset: 0x001AC11C
	private static void ᜀ(XmlReader A_0, XlsPivotField A_1, XlsPivotTable A_2)
	{
		int a_ = 16;
		int num = 65;
		for (;;)
		{
			bool flag;
			switch (num)
			{
			case 0:
				goto IL_8AA;
			case 1:
				A_1.Name = A_0.Value;
				num = 9;
				continue;
			case 2:
				goto IL_69E;
			case 3:
				goto IL_5B1;
			case 4:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("⹅㱇㹉㱋瑍罏絑❓㕕し㽙ㅛ㽝፟䱡ୣᙥ൧ѩᑫͭᱯᑱ᭳ѵᕷ᭹ࡻൽ깿ꞇ黎ﲋﲍﾙ鍊춟캡讣钥颧骩骫膭\uddaf펱\uddb3\ud8b5", a_))
				{
					num = 18;
					continue;
				}
				goto IL_7BC;
			case 5:
				goto IL_392;
			case 6:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㍅♇⍉㵋㭍㕏ὑㅓ㭕㩗㽙⹛๝቟ൡᑣͥᩧṩᕫ", a_)))
				{
					num = 63;
					continue;
				}
				goto IL_DD0;
			case 7:
				goto IL_2A7;
			case 8:
				goto IL_E6C;
			case 9:
				goto IL_876;
			case 10:
				goto IL_805;
			case 11:
				A_1.ShowPageBreak = XmlConvert.ToBoolean(A_0.Value);
				num = 61;
				continue;
			case 12:
				A_1.Compact = XmlConvert.ToBoolean(A_0.Value);
				num = 0;
				continue;
			case 13:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("╅❇❉㱋⽍㍏♑", a_)))
				{
					num = 12;
					continue;
				}
				goto IL_8AA;
			case 14:
				goto IL_ACD;
			case 15:
				if (A_0.NodeType == XmlNodeType.None)
				{
					num = 53;
					continue;
				}
				num = 112;
				continue;
			case 16:
				A_1.ShowBlankRow = XmlConvert.ToBoolean(A_0.Value);
				num = 74;
				continue;
			case 17:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⹅ⅇ⹉⥋M㕏║ᵓ≕㵗㝙⽛", a_)))
				{
					num = 98;
					continue;
				}
				goto IL_E04;
			case 18:
				return;
			case 19:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⽅㱇⽉⅋ṍㅏ㕑ㅓᕕ㝗⽙㉛⩝", a_)))
				{
					num = 93;
					continue;
				}
				goto IL_392;
			case 20:
				goto IL_C5B;
			case 21:
				if (true)
				{
				}
				A_1.IsAllDrilled = XmlConvert.ToBoolean(A_0.Value);
				num = 46;
				continue;
			case 22:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⭅ⵇ⭉㽋㭍≏㝑ቓ㽕㑗⹙㥛ⱝ", a_)))
				{
					num = 90;
					continue;
				}
				goto IL_B1F;
			case 23:
				A_1.IsShowAllItems = XmlConvert.ToBoolean(A_0.Value);
				num = 80;
				continue;
			case 24:
				goto IL_A78;
			case 25:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 62;
					continue;
				}
				goto IL_37B;
			case 26:
				A_1.IsAutoShow = XmlConvert.ToBoolean(A_0.Value);
				num = 7;
				continue;
			case 27:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("≅㩇⭉⭋ᩍ㽏ᅑ㭓㩕", a_)))
				{
					num = 42;
					continue;
				}
				goto IL_9D4;
			case 28:
				A_1.SubtotalTop = XmlConvert.ToBoolean(A_0.Value);
				num = 68;
				continue;
			case 29:
				if (A_2.Options.RowLayout == PivotTableLayoutType.Compact)
				{
					num = 52;
					continue;
				}
				goto IL_E38;
			case 30:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㕅⁇╉㭋ṍ≏㵑⑓᝕⭗ᥙ㵛⹝ᑟୡୣࡥ", a_)))
				{
					num = 70;
					continue;
				}
				goto IL_4FC;
			case 31:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㕅⁇╉㭋ṍ≏㵑⑓ɕㅗ⩙", a_)))
				{
					num = 104;
					continue;
				}
				goto IL_C27;
			case 32:
				A_1.CanDragToRow = XmlConvert.ToBoolean(A_0.Value);
				num = 3;
				continue;
			case 33:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㕅⁇╉㭋ཌྷ㱏㹑", a_)))
				{
					num = 23;
					continue;
				}
				goto IL_A4A;
			case 34:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⽅♇⥉⁋㭍㑏㝑ᩓ㍕⽗ፙ⡛㭝ൟᅡⵣࡥ⹧ͩkᩭᕯq", a_)))
				{
					num = 55;
					continue;
				}
				goto IL_69E;
			case 35:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㕅❇㡉㡋ᩍ⥏≑ㅓ", a_)))
				{
					num = 75;
					continue;
				}
				goto IL_D65;
			case 36:
				goto IL_842;
			case 37:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㕅㵇⡉㡋⅍⑏㍑㡓ᕕ㥗⩙⡛㝝ཟౡ", a_)))
				{
					num = 66;
					continue;
				}
				goto IL_A78;
			case 38:
				goto IL_E38;
			case 39:
				goto IL_7B7;
			case 40:
				A_1.CanDragToData = XmlConvert.ToBoolean(A_0.Value);
				num = 81;
				continue;
			case 41:
				goto IL_B1F;
			case 42:
				A_1.CanDragToColumn = XmlConvert.ToBoolean(A_0.Value);
				num = 85;
				continue;
			case 43:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⽅♇㥉⥋㱍⑏ၑ㡓㝕㙗ㅙ๛ㅝ᝟", a_)))
				{
					num = 16;
					continue;
				}
				goto IL_703;
			case 44:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_98E;
				default:
					if (false)
					{
					}
					if (A_1 == null)
					{
						num = 39;
						continue;
					}
					num = 71;
					continue;
				}
				break;
			case 45:
				goto IL_E6C;
			case 46:
				goto IL_273;
			case 47:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("≅⥇㹉ⵋࡍ㥏㝑㡓㉕", a_)))
				{
					num = 89;
					continue;
				}
				goto IL_6CF;
			case 48:
				if (A_2.Options.RowLayout != PivotTableLayoutType.Outline)
				{
					num = 67;
					continue;
				}
				goto IL_634;
			case 49:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⥅㵇㹉⁋❍㹏㝑", a_)))
				{
					num = 102;
					continue;
				}
				goto IL_E38;
			case 50:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("≅⥇㹉ⵋᵍ㽏❑♓㕕㵗ख़㍛ⱝᑟ", a_)))
				{
					num = 105;
					continue;
				}
				goto IL_C5B;
			case 51:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("≅㩇⭉⭋ᩍ㽏ɑ㕓ㅕ㵗", a_)))
				{
					num = 72;
					continue;
				}
				goto IL_5E5;
			case 52:
				goto IL_634;
			case 53:
				return;
			case 54:
				return;
			case 55:
				A_1.ShowNewItemsInFilter = XmlConvert.ToBoolean(A_0.Value);
				num = 2;
				continue;
			case 56:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("≅ⵇⱉⵋ㭍㱏♑ᕓ≕ⱗ⡙㕛㱝ᕟᙡţ≥ᩧͩkɭ⍯ٱᕳɵᵷ", a_)))
				{
					num = 113;
					continue;
				}
				goto IL_920;
			case 57:
				A_1.NumberFormatIndex = XmlConvert.ToInt32(A_0.Value);
				num = 36;
				continue;
			case 58:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("❅ぇ⍉㽋", a_)))
				{
					num = 100;
					continue;
				}
				goto IL_805;
			case 59:
				goto IL_E04;
			case 60:
				num = 96;
				continue;
			case 61:
				goto IL_4C8;
			case 62:
				num = 84;
				continue;
			case 63:
				A_1.Caption = A_0.Value;
				num = 87;
				continue;
			case 64:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("≅㩇⭉⭋ō㙏㑑", a_)))
				{
					num = 91;
					continue;
				}
				goto IL_554;
			case 66:
				A_1.SubtotalCaption = A_0.Value;
				num = 24;
				continue;
			case 67:
				num = 29;
				continue;
			case 68:
				goto IL_21E;
			case 69:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("⹅㱇㹉㱋瑍罏絑❓㕕し㽙ㅛ㽝፟䱡ୣᙥ൧ѩᑫͭᱯᑱ᭳ѵᕷ᭹ࡻൽ깿ꞇ黎ﲋﲍﾙ鍊춟캡讣钥颧骩骫膭\uddaf펱\uddb3\ud8b5", a_))
				{
					num = 54;
					continue;
				}
				goto IL_37B;
			case 70:
				A_1.ShowPropAsCaption = XmlConvert.ToBoolean(A_0.Value);
				num = 97;
				continue;
			case 71:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⡅⥇❉⥋", a_)))
				{
					num = 1;
					continue;
				}
				goto IL_876;
			case 72:
				A_1.CanDragToPage = XmlConvert.ToBoolean(A_0.Value);
				num = 99;
				continue;
			case 73:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("❅⑇♉ࡋ㱍㥏㹑㡓㍕㱗", a_)))
				{
					num = 21;
					continue;
				}
				goto IL_273;
			case 74:
				goto IL_703;
			case 75:
				A_1.SortType = new PivotFieldSortType?((PivotFieldSortType)Enum.Parse(typeof(PivotFieldSortType), A_0.Value, true));
				num = 88;
				continue;
			case 76:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("≅㩇⭉⭋ᩍ㽏ᙑ㕓≕㥗", a_)))
				{
					num = 40;
					continue;
				}
				goto IL_41C;
			case 77:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⡅㵇❉ੋ⍍⑏᭑こ", a_)))
				{
					num = 57;
					continue;
				}
				goto IL_842;
			case 78:
				if (flag)
				{
					num = 110;
					continue;
				}
				goto IL_E6C;
			case 79:
				goto IL_B4D;
			case 80:
				goto IL_A4A;
			case 81:
				goto IL_41C;
			case 82:
				goto IL_C27;
			case 83:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㕅⁇╉㭋੍≏㵑⑓ቕ㝗ⵙ㉛ⵝ", a_)))
				{
					num = 111;
					continue;
				}
				goto IL_ACD;
			case 84:
				if (A_0.LocalName == RecordTableEnumerator.b("㙅ⅇ㱉⍋㩍ᙏ㭑ㅓ㩕㱗⥙", a_))
				{
					num = 107;
					continue;
				}
				goto IL_37B;
			case 85:
				goto IL_9D4;
			case 86:
				goto IL_6CF;
			case 87:
				goto IL_DD0;
			case 88:
				goto IL_D65;
			case 89:
				A_1.DataField = XmlConvert.ToBoolean(A_0.Value);
				num = 86;
				continue;
			case 90:
				A_1.IsMeasureField = XmlConvert.ToBoolean(A_0.Value);
				num = 41;
				continue;
			case 91:
				A_1.CanDragOff = XmlConvert.ToBoolean(A_0.Value);
				num = 101;
				continue;
			case 92:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⽅♇㥉⥋㱍⑏ɑ㕓ㅕ㵗ᡙ⹛㭝şॡ", a_)))
				{
					num = 11;
					continue;
				}
				goto IL_4C8;
			case 93:
				A_1.ItemsPerPage = XmlConvert.ToInt32(A_0.Value);
				num = 5;
				continue;
			case 94:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⭅㵇♉㡋❍⁏㹑ㅓὕⱗ㽙ㅛ൝՟๡ţեᱧͩͫmㅯṱᡳ᥵ཷό᡻", a_)))
				{
					num = 114;
					continue;
				}
				goto IL_B4D;
			case 95:
				goto IL_1FD;
			case 96:
				if (A_0.LocalName == RecordTableEnumerator.b("㙅ⅇ㱉⍋㩍ᙏ㭑ㅓ㩕㱗", a_))
				{
					num = 115;
					continue;
				}
				goto IL_7BC;
			case 97:
				goto IL_4FC;
			case 98:
				A_1.ShowNewItemsOnRefresh = !XmlConvert.ToBoolean(A_0.Value);
				num = 59;
				continue;
			case 99:
				goto IL_5E5;
			case 100:
				A_1.Axis = (AxisTypes)Enum.Parse(typeof(PivotAxisTypes2007), A_0.Value, false);
				num = 10;
				continue;
			case 101:
				goto IL_554;
			case 102:
				A_1.ShowOutline = XmlConvert.ToBoolean(A_0.Value);
				num = 48;
				continue;
			case 103:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㕅㵇⡉㡋⅍⑏㍑㡓ɕ㝗⩙", a_)))
				{
					num = 28;
					continue;
				}
				goto IL_21E;
			case 104:
				A_1.ShowToolTip = XmlConvert.ToBoolean(A_0.Value);
				num = 82;
				continue;
			case 105:
				A_1.IsDataSourceSorted = XmlConvert.ToBoolean(A_0.Value);
				num = 20;
				continue;
			case 106:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("❅㵇㹉⍋ᵍ㡏㵑⍓", a_)))
				{
					num = 26;
					continue;
				}
				goto IL_2A7;
			case 107:
				num = 69;
				continue;
			case 108:
				goto IL_920;
			case 109:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("≅㩇⭉⭋ᩍ㽏Q㭓⅕", a_)))
				{
					num = 32;
					continue;
				}
				goto IL_5B1;
			case 110:
				A_0.Read();
				num = 45;
				continue;
			case 111:
				A_1.ShowDropDown = XmlConvert.ToBoolean(A_0.Value);
				num = 14;
				continue;
			case 112:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 60;
					continue;
				}
				goto IL_7BC;
			case 113:
				A_1.IsDefaultDrill = XmlConvert.ToBoolean(A_0.Value);
				goto IL_98E;
			case 114:
				A_1.IsMultiSelected = XmlConvert.ToBoolean(A_0.Value);
				num = 79;
				continue;
			case 115:
				num = 4;
				continue;
			}
			if (A_0 == null)
			{
				num = 95;
				continue;
			}
			num = 44;
			continue;
			IL_21E:
			num = 33;
			continue;
			IL_273:
			num = 56;
			continue;
			IL_2A7:
			num = 58;
			continue;
			IL_37B:
			A_0.Read();
			num = 8;
			continue;
			IL_392:
			num = 22;
			continue;
			IL_41C:
			num = 51;
			continue;
			IL_4C8:
			num = 19;
			continue;
			IL_4FC:
			num = 31;
			continue;
			IL_554:
			num = 27;
			continue;
			IL_5B1:
			num = 17;
			continue;
			IL_5E5:
			num = 109;
			continue;
			IL_634:
			A_2.Options.RowLayout = PivotTableLayoutType.Tabular;
			num = 38;
			continue;
			IL_69E:
			num = 43;
			continue;
			IL_6CF:
			num = 77;
			continue;
			IL_703:
			num = 92;
			continue;
			IL_7BC:
			num = 25;
			continue;
			IL_805:
			A_1.Subtotals = spr\u2005.ᜀ(A_0);
			num = 13;
			continue;
			IL_842:
			num = 50;
			continue;
			IL_876:
			num = 37;
			continue;
			IL_8AA:
			num = 64;
			continue;
			IL_920:
			A_0.Read();
			flag = false;
			flag = spr\u2005.ᜁ(A_0, A_1);
			flag |= spr\u2005.ᜂ(A_0, A_1);
			num = 78;
			continue;
			IL_98E:
			num = 108;
			continue;
			IL_9D4:
			num = 76;
			continue;
			IL_A4A:
			num = 83;
			continue;
			IL_A78:
			num = 106;
			continue;
			IL_ACD:
			num = 30;
			continue;
			IL_B1F:
			num = 94;
			continue;
			IL_B4D:
			num = 49;
			continue;
			IL_C27:
			num = 35;
			continue;
			IL_C5B:
			num = 73;
			continue;
			IL_D65:
			num = 6;
			continue;
			IL_DD0:
			num = 47;
			continue;
			IL_E04:
			num = 34;
			continue;
			IL_E38:
			num = 103;
			continue;
			IL_E6C:
			num = 15;
		}
		IL_1FD:
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅅ㩇⍉㡋⭍≏", a_));
		IL_7B7:
		throw new ArgumentException(RecordTableEnumerator.b("㙅ⅇ㱉⍋㩍灏ᑑ㵓㍕㑗㹙", a_));
	}

	// Token: 0x06002F87 RID: 12167 RVA: 0x001ADFB8 File Offset: 0x001ACFB8
	private static bool ᜂ(XmlReader A_0, XlsPivotField A_1)
	{
		int a_ = 6;
		int num = 4;
		for (;;)
		{
			IL_1D:
			switch (num)
			{
			case 0:
				goto IL_5E;
			case 1:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				goto IL_E7;
			case 2:
				goto IL_46;
			case 3:
				if (A_0.LocalName != RecordTableEnumerator.b("崻䬽㐿ⵁᝃ⥅㩇㹉Ὃⵍ㽏≑ㅓ", a_))
				{
					num = 5;
					continue;
				}
				num = 1;
				continue;
			case 5:
				return false;
			}
			while (A_0 != null)
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
					num = 3;
					goto IL_1D;
				}
			}
			num = 2;
		}
		IL_46:
		throw new ArgumentNullException(RecordTableEnumerator.b("主嬽ℿ♁⅃㑅", a_));
		IL_5E:
		if (true)
		{
		}
		throw new ArgumentException(RecordTableEnumerator.b("䰻圽㘿ⵁぃ晅็⍉⥋≍㑏", a_));
		IL_E7:
		Stream preservedAutoSort = ShapeParser.ReadNodeAsStream(A_0);
		A_1.PreservedAutoSort = preservedAutoSort;
		return true;
	}

	// Token: 0x06002F88 RID: 12168 RVA: 0x001AE0BC File Offset: 0x001AD0BC
	private static bool ᜁ(XmlReader A_0, XlsPivotField A_1)
	{
		int a_ = 12;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_14D;
			case 1:
				if (A_0.LocalName != RecordTableEnumerator.b("⭁ぃ⍅╇㥉", a_))
				{
					num = 10;
					continue;
				}
				num = 7;
				continue;
			case 2:
				goto IL_EF;
			case 4:
				goto IL_58;
			case 5:
				goto IL_16D;
			case 6:
				goto IL_B1;
			case 7:
				if (A_1 == null)
				{
					num = 12;
					continue;
				}
				A_0.Read();
				num = 6;
				continue;
			case 8:
				if (A_0.LocalName == RecordTableEnumerator.b("⭁ぃ⍅╇", a_))
				{
					num = 11;
					continue;
				}
				goto IL_EF;
			case 9:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 5;
					continue;
				}
				num = 8;
				continue;
			case 10:
				return false;
			case 11:
				spr\u2005.ᜀ(A_0, A_1);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_B1;
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
				break;
			case 12:
				goto IL_18B;
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			num = 1;
			continue;
			IL_EF:
			A_0.Read();
			num = 0;
			continue;
			IL_14D:
			num = 9;
			continue;
			IL_B1:
			goto IL_14D;
		}
		IL_58:
		throw new ArgumentNullException(RecordTableEnumerator.b("ぁ⅃❅ⱇ⽉㹋", a_));
		IL_16D:
		A_0.Read();
		return true;
		IL_18B:
		throw new ArgumentException(RecordTableEnumerator.b("㉁ⵃぅ❇㹉汋ࡍ㥏㝑㡓㉕", a_));
	}

	// Token: 0x06002F89 RID: 12169 RVA: 0x001AE274 File Offset: 0x001AD274
	private static void ᜁ(XmlReader A_0, PivotDataField A_1)
	{
		int a_ = 1;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_5A;
			case 2:
				return;
			case 3:
				goto IL_C0;
			case 4:
				A_0.MoveToAttribute(RecordTableEnumerator.b("䜶倸䴺刼䬾ቀ⭂⩄うࡈ㡊", a_));
				A_1.ᜀ(A_0.Value);
				A_0.Read();
				num = 2;
				continue;
			}
			if (A_0.LocalName != RecordTableEnumerator.b("制䄸伺", a_))
			{
				num = 1;
				continue;
			}
			A_0.Read();
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				num = 3;
				continue;
			}
			IL_C0:
			if (!A_0.IsEmptyElement)
			{
				return;
			}
			if (true)
			{
			}
			num = 4;
		}
		IL_5A:
		throw new ArgumentException(RecordTableEnumerator.b("䔶尸娺夼娾㍀", a_));
	}

	// Token: 0x06002F8A RID: 12170 RVA: 0x001AE374 File Offset: 0x001AD374
	private static void ᜀ(XmlReader A_0, PivotDataField A_1)
	{
		int a_ = 16;
		int num = 6;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				num = 7;
				continue;
			case 1:
				num = 2;
				continue;
			case 2:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 0;
					continue;
				}
				goto IL_E4;
			}
			case 3:
				goto IL_E4;
			case 4:
				goto IL_74;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					spr\u2005.ᜁ(A_0, A_1);
					break;
				}
				num = 3;
				continue;
			case 7:
			{
				string localName;
				if (localName == RecordTableEnumerator.b("⍅ぇ㹉", a_))
				{
					num = 5;
					continue;
				}
				goto IL_E4;
			}
			case 8:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 1;
					continue;
				}
				goto IL_E4;
			case 9:
				goto IL_E4;
			case 10:
				goto IL_104;
			case 11:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 10;
					continue;
				}
				num = 8;
				continue;
			}
			if (A_0.LocalName != RecordTableEnumerator.b("⍅ぇ㹉K㵍⑏", a_))
			{
				num = 4;
				continue;
			}
			A_0.Read();
			num = 9;
			continue;
			IL_E4:
			num = 11;
		}
		IL_74:
		throw new ArgumentException(RecordTableEnumerator.b("㑅ⵇ⭉⡋⭍≏", a_));
		IL_104:
		A_0.Read();
	}

	// Token: 0x06002F8B RID: 12171 RVA: 0x001AE4F8 File Offset: 0x001AD4F8
	private static void ᜀ(XmlReader A_0, XlsPivotField A_1)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			int num = 26;
			spr\u1B6A spr_u1B6A;
			int num2;
			for (;;)
			{
				string text;
				switch (num)
				{
				case 0:
					goto IL_5BE;
				case 1:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("♁", a_)))
					{
						num = 32;
						continue;
					}
					goto IL_237;
				case 2:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("ㅁ⁃", a_)))
					{
						num = 40;
						continue;
					}
					goto IL_FD;
				case 3:
				{
					bool flag = true;
					spr_u1B6A.ᜆ(XmlConvert.ToBoolean(A_0.Value));
					num = 4;
					continue;
				}
				case 4:
					goto IL_5E2;
				case 5:
					if (text == RecordTableEnumerator.b("♁⅃⁅⥇㽉⁋㩍", a_))
					{
						num = 43;
						continue;
					}
					goto IL_36A;
				case 6:
					goto IL_36A;
				case 7:
					goto IL_F8;
				case 8:
				{
					if (A_1 == null)
					{
						num = 0;
						continue;
					}
					num2 = 0;
					bool flag = false;
					spr_u1B6A = new spr\u1B6A();
					num = 35;
					continue;
				}
				case 9:
				{
					bool flag = true;
					spr_u1B6A.ᜀ(A_0.Value);
					num = 24;
					continue;
				}
				case 10:
					goto IL_602;
				case 11:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㙁", a_)))
					{
						num = 36;
						continue;
					}
					goto IL_26D;
				case 12:
				{
					bool flag = true;
					spr_u1B6A.ᜂ(XmlConvert.ToBoolean(A_0.Value));
					num = 16;
					continue;
				}
				case 13:
					num2 = XmlConvert.ToInt32(A_0.Value);
					num = 30;
					continue;
				case 14:
					return;
				case 15:
					goto IL_3CD;
				case 16:
					goto IL_403;
				case 17:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("⩁", a_)))
					{
						num = 29;
						continue;
					}
					goto IL_3CD;
				case 18:
					goto IL_52A;
				case 19:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("ㅁ", a_)))
					{
						num = 3;
						continue;
					}
					goto IL_436;
				case 20:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("⽁", a_)))
					{
						num = 33;
						continue;
					}
					goto IL_397;
				case 21:
					goto IL_334;
				case 22:
				{
					bool flag;
					if (flag)
					{
						num = 10;
						continue;
					}
					goto IL_607;
				}
				case 23:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5E2;
					default:
						if (false)
						{
						}
						goto IL_397;
					}
					break;
				case 24:
					goto IL_133;
				case 25:
					goto IL_26D;
				case 27:
				{
					bool flag = true;
					spr_u1B6A.ᜄ(XmlConvert.ToBoolean(A_0.Value));
					num = 21;
					continue;
				}
				case 28:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("ⱁ", a_)))
					{
						num = 9;
						continue;
					}
					goto IL_133;
				case 29:
				{
					bool flag = true;
					spr_u1B6A.ᜃ(XmlConvert.ToBoolean(A_0.Value));
					num = 15;
					continue;
				}
				case 30:
					goto IL_2E4;
				case 31:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("⽁", a_)))
					{
						num = 37;
						continue;
					}
					goto IL_52A;
				case 32:
				{
					bool flag = true;
					spr_u1B6A.ᜁ(XmlConvert.ToBoolean(A_0.Value));
					num = 39;
					continue;
				}
				case 33:
				{
					bool flag = true;
					spr_u1B6A.ᜀ(XmlConvert.ToBoolean(A_0.Value));
					num = 23;
					continue;
				}
				case 34:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("⑁", a_)))
					{
						num = 12;
						continue;
					}
					goto IL_403;
				case 35:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㩁", a_)))
					{
						num = 13;
						continue;
					}
					goto IL_2E4;
				case 36:
				{
					num2 = -1;
					bool flag = true;
					text = A_0.Value;
					num = 5;
					continue;
				}
				case 37:
				{
					bool flag = true;
					spr_u1B6A.ᜀ(XmlConvert.ToBoolean(A_0.Value));
					num = 18;
					continue;
				}
				case 38:
					goto IL_FD;
				case 39:
					goto IL_237;
				case 40:
				{
					bool flag = true;
					spr_u1B6A.ᜅ(XmlConvert.ToBoolean(A_0.Value));
					num = 38;
					continue;
				}
				case 41:
					if (A_1.ItemOptions.ContainsKey(num2))
					{
						num = 14;
						continue;
					}
					num = 22;
					continue;
				case 42:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("❁", a_)))
					{
						num = 27;
						continue;
					}
					goto IL_334;
				case 43:
					text += RecordTableEnumerator.b("ㅁ", a_);
					num = 6;
					continue;
				}
				if (A_0 == null)
				{
					num = 7;
					continue;
				}
				num = 8;
				continue;
				IL_FD:
				num = 20;
				continue;
				IL_133:
				num = 19;
				continue;
				IL_237:
				num = 42;
				continue;
				IL_26D:
				if (true)
				{
				}
				num = 41;
				continue;
				IL_2E4:
				num = 1;
				continue;
				IL_334:
				num = 34;
				continue;
				IL_36A:
				spr_u1B6A.ᜀ((PivotItemType)Enum.Parse(typeof(XLSXPivotItemType), text, false));
				num = 25;
				continue;
				IL_397:
				num = 11;
				continue;
				IL_3CD:
				num = 31;
				continue;
				IL_403:
				num = 17;
				continue;
				IL_436:
				num = 2;
				continue;
				IL_5E2:
				goto IL_436;
				IL_52A:
				num = 28;
			}
			IL_F8:
			throw new ArgumentNullException(RecordTableEnumerator.b("㕁㙃⽅㱇⽉㹋", a_));
			IL_5BE:
			throw new ArgumentException(RecordTableEnumerator.b("㉁ⵃぅ❇㹉汋ࡍ㥏㝑㡓㉕", a_));
			IL_602:
			A_1.ᜀ(num2, spr_u1B6A);
			return;
			IL_607:
			A_1.AddItemOption(num2);
			return;
		}
		}
	}

	// Token: 0x06002F8C RID: 12172 RVA: 0x001AEB14 File Offset: 0x001ADB14
	public static SubtotalTypes ᜀ(XmlReader A_0)
	{
		int a_ = 9;
		int num = 25;
		for (;;)
		{
			SubtotalTypes subtotalTypes;
			switch (num)
			{
			case 0:
				goto IL_F3;
			case 1:
				subtotalTypes |= spr\u2005.ᜀ(SubtotalTypes.Min, A_0.Value);
				num = 14;
				continue;
			case 2:
				goto IL_F8;
			case 3:
				goto IL_49D;
			case 4:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("刾⁀㭂ᙄ㉆⭈㽊≌㭎ぐ㽒", a_)))
				{
					num = 17;
					continue;
				}
				goto IL_4C0;
			case 5:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䰾㕀❂ń≆㽈ᭊṌ㩎㍐❒㩔⍖㡘㝚", a_)))
				{
					num = 18;
					continue;
				}
				goto IL_282;
			case 6:
				goto IL_282;
			case 7:
				goto IL_30C;
			case 8:
				subtotalTypes |= spr\u2005.ᜀ(SubtotalTypes.Counta, A_0.Value);
				num = 26;
				continue;
			case 9:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("尾⹀㙂⭄㍆ᩈ㹊⽌㭎㹐❒㑔㭖", a_)))
				{
					num = 16;
					continue;
				}
				goto IL_F8;
			case 10:
				subtotalTypes |= spr\u2005.ᜀ(SubtotalTypes.Varp, A_0.Value);
				num = 38;
				continue;
			case 11:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("尾⹀㙂⭄㍆ࡈᡊ㡌ⵎ═㱒⅔㙖㕘", a_)))
				{
					num = 8;
					continue;
				}
				goto IL_34F;
			case 12:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("帾㝀⑂ᙄ㉆⭈㽊≌㭎ぐ㽒", a_)))
				{
					num = 32;
					continue;
				}
				goto IL_3F7;
			case 13:
				goto IL_24E;
			case 14:
				goto IL_12C;
			case 15:
			{
				bool flag;
				if (!flag)
				{
					num = 22;
					continue;
				}
				goto IL_30C;
			}
			case 16:
				subtotalTypes |= spr\u2005.ᜀ(SubtotalTypes.Count, A_0.Value);
				num = 2;
				continue;
			case 17:
				subtotalTypes |= spr\u2005.ᜀ(SubtotalTypes.Max, A_0.Value);
				num = 30;
				continue;
			case 18:
				subtotalTypes |= spr\u2005.ᜀ(SubtotalTypes.Stdevp, A_0.Value);
				num = 6;
				continue;
			case 19:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("伾㍀ⱂ⅄㉆⩈㽊Ṍ㩎㍐❒㩔⍖㡘㝚", a_)))
				{
					num = 27;
					continue;
				}
				goto IL_4F4;
			case 20:
				goto IL_383;
			case 21:
				goto IL_3F7;
			case 22:
				if (true)
				{
				}
				subtotalTypes = SubtotalTypes.None;
				num = 7;
				continue;
			case 23:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䰾㕀❂ń≆㽈ᡊ㡌ⵎ═㱒⅔㙖㕘", a_)))
				{
					num = 29;
					continue;
				}
				goto IL_383;
			case 24:
			{
				bool flag = XmlConvert.ToBoolean(A_0.Value);
				num = 15;
				continue;
			}
			case 25:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_49D;
				}
				if (false)
				{
				}
				break;
			case 26:
				goto IL_34F;
			case 27:
				subtotalTypes |= spr\u2005.ᜀ(SubtotalTypes.Product, A_0.Value);
				num = 34;
				continue;
			case 28:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("嬾⑀╂⑄㉆╈㽊Ṍ㩎㍐❒㩔⍖㡘㝚", a_)))
				{
					num = 24;
					continue;
				}
				goto IL_30C;
			case 29:
				subtotalTypes |= spr\u2005.ᜀ(SubtotalTypes.Stdev, A_0.Value);
				num = 20;
				continue;
			case 30:
				goto IL_4C0;
			case 31:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䰾㑀⹂ᙄ㉆⭈㽊≌㭎ぐ㽒", a_)))
				{
					num = 39;
					continue;
				}
				goto IL_24E;
			case 32:
				subtotalTypes |= spr\u2005.ᜀ(SubtotalTypes.Average, A_0.Value);
				num = 21;
				continue;
			case 33:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䤾⁀ㅂᙄ㉆⭈㽊≌㭎ぐ㽒", a_)))
				{
					num = 3;
					continue;
				}
				goto IL_1FA;
			case 34:
				goto IL_4F4;
			case 35:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䤾⁀ㅂᕄᑆ㱈⥊㥌⁎═㉒㥔", a_)))
				{
					num = 10;
					continue;
				}
				return subtotalTypes;
			case 36:
				goto IL_1FA;
			case 37:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("刾⡀ⵂᙄ㉆⭈㽊≌㭎ぐ㽒", a_)))
				{
					num = 1;
					continue;
				}
				goto IL_12C;
			case 38:
				return subtotalTypes;
			case 39:
				subtotalTypes |= spr\u2005.ᜀ(SubtotalTypes.Sum, A_0.Value);
				num = 13;
				continue;
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			subtotalTypes = SubtotalTypes.Default;
			num = 28;
			continue;
			IL_F8:
			num = 23;
			continue;
			IL_12C:
			num = 19;
			continue;
			IL_1FA:
			num = 35;
			continue;
			IL_24E:
			num = 11;
			continue;
			IL_282:
			num = 33;
			continue;
			IL_30C:
			num = 31;
			continue;
			IL_34F:
			num = 12;
			continue;
			IL_383:
			num = 5;
			continue;
			IL_3F7:
			num = 4;
			continue;
			IL_49D:
			subtotalTypes |= spr\u2005.ᜀ(SubtotalTypes.Var, A_0.Value);
			num = 36;
			continue;
			IL_4C0:
			num = 37;
			continue;
			IL_4F4:
			num = 9;
		}
		IL_F3:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴾⑀≂⅄≆㭈", a_));
	}

	// Token: 0x06002F8D RID: 12173 RVA: 0x001AF04C File Offset: 0x001AE04C
	private static void ᜋ(XmlReader A_0, XlsPivotTable A_1)
	{
		int a_ = 16;
		int num = 18;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				int num2 = XmlConvert.ToInt32(A_0.Value);
				num = 7;
				continue;
			}
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_12D;
				default:
					goto IL_24A;
				}
				break;
			case 2:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 1;
					continue;
				}
				num = 5;
				continue;
			case 3:
				A_1.ShowDataFieldInRow = true;
				goto IL_12D;
			case 4:
				if (A_1 == null)
				{
					num = 8;
					continue;
				}
				num = 11;
				continue;
			case 5:
				if (A_0.LocalName == RecordTableEnumerator.b("⁅ⅇ⽉⁋⩍", a_))
				{
					num = 12;
					continue;
				}
				goto IL_13D;
			case 6:
				goto IL_13D;
			case 7:
				goto IL_78;
			case 8:
				goto IL_124;
			case 9:
				goto IL_73;
			case 10:
				goto IL_1CB;
			case 11:
				if (A_0.LocalName != RecordTableEnumerator.b("㑅❇㵉ੋ❍㕏㹑こ╕", a_))
				{
					num = 14;
					continue;
				}
				A_0.Read();
				num = 16;
				continue;
			case 12:
			{
				int num2 = -1;
				num = 13;
				continue;
			}
			case 13:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㹅", a_)))
				{
					num = 0;
					continue;
				}
				goto IL_78;
			case 14:
				return;
			case 15:
			{
				int num2;
				if (num2 == -2)
				{
					num = 3;
					continue;
				}
				if (true)
				{
				}
				A_1.PivotRowFields.Add(A_1.PivotFields[num2]);
				num = 10;
				continue;
			}
			case 16:
				goto IL_13D;
			case 17:
				goto IL_1CB;
			}
			if (A_0 == null)
			{
				num = 9;
				continue;
			}
			num = 4;
			continue;
			IL_78:
			num = 15;
			continue;
			IL_12D:
			num = 17;
			continue;
			IL_13D:
			num = 2;
			continue;
			IL_1CB:
			A_0.Read();
			num = 6;
		}
		IL_73:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑅ⵇ⭉⡋⭍≏", a_));
		IL_124:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙅ⅇ㱉⍋㩍я㍑㙓㩕㵗", a_));
		IL_24A:
		if (false)
		{
		}
		A_0.Read();
	}

	// Token: 0x06002F8E RID: 12174 RVA: 0x001AF2B0 File Offset: 0x001AE2B0
	private static void ᜊ(XmlReader A_0, XlsPivotTable A_1)
	{
		int a_ = 15;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				if (A_0.LocalName != RecordTableEnumerator.b("㝄⡆㹈Ɋ㥌⩎㱐⁒", a_))
				{
					num = 0;
					continue;
				}
				goto IL_E0;
			case 2:
				goto IL_62;
			case 3:
				if (A_1 == null)
				{
					num = 4;
					continue;
				}
				if (true)
				{
				}
				num = 1;
				continue;
			case 4:
				goto IL_DE;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_62;
			default:
				if (false)
				{
				}
				if (A_0 == null)
				{
					num = 2;
				}
				else
				{
					num = 3;
				}
				break;
			}
		}
		IL_62:
		throw new ArgumentNullException(RecordTableEnumerator.b("㝄≆⡈⽊⡌㵎", a_));
		IL_DE:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕄⹆㽈⑊㥌᭎ぐㅒ㥔㉖", a_));
		IL_E0:
		A_1.RowItemsStream = ShapeParser.ReadNodeAsStream(A_0);
	}

	// Token: 0x06002F8F RID: 12175 RVA: 0x001AF3AC File Offset: 0x001AE3AC
	private static void ᜉ(XmlReader A_0, XlsPivotTable A_1)
	{
		int a_ = 6;
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
				goto IL_13B;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_161;
				}
				if (false)
				{
				}
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䐻", a_)))
				{
					num = 13;
					continue;
				}
				goto IL_F0;
			case 3:
				goto IL_EE;
			case 4:
				if (A_0.LocalName == RecordTableEnumerator.b("娻圽┿⹁⁃", a_))
				{
					num = 0;
					continue;
				}
				goto IL_F0;
			case 5:
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				num = 6;
				continue;
			case 6:
				if (A_0.LocalName != RecordTableEnumerator.b("弻儽ⰿсⵃ⍅⑇⹉㽋", a_))
				{
					num = 9;
					continue;
				}
				A_0.Read();
				num = 1;
				continue;
			case 7:
				goto IL_13B;
			case 9:
				return;
			case 10:
				goto IL_68;
			case 11:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 12;
					continue;
				}
				num = 4;
				continue;
			case 12:
				goto IL_15B;
			case 13:
				A_1.ColFieldsOrder.Add((int)Convert.ToInt16(A_0.Value));
				num = 14;
				continue;
			case 14:
				goto IL_F0;
			}
			if (A_0 == null)
			{
				num = 10;
				continue;
			}
			num = 5;
			continue;
			IL_F0:
			A_0.Read();
			num = 7;
			continue;
			IL_13B:
			num = 11;
		}
		IL_68:
		throw new ArgumentNullException(RecordTableEnumerator.b("主嬽ℿ♁⅃㑅", a_));
		IL_EE:
		goto IL_161;
		IL_15B:
		A_0.Read();
		return;
		IL_161:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰻圽㘿ⵁぃቅ⥇⡉⁋⭍", a_));
	}

	// Token: 0x06002F90 RID: 12176 RVA: 0x001AF5A8 File Offset: 0x001AE5A8
	private static void ᜈ(XmlReader A_0, XlsPivotTable A_1)
	{
		int a_ = 19;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.LocalName != RecordTableEnumerator.b("⩈⑊⅌َ═㙒㡔⑖", a_))
				{
					num = 4;
					continue;
				}
				goto IL_DD;
			case 1:
				goto IL_DB;
			case 2:
				if (true)
				{
				}
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				num = 0;
				continue;
			case 3:
				goto IL_62;
			case 4:
				return;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_62;
			default:
				if (false)
				{
				}
				if (A_0 == null)
				{
					num = 3;
				}
				else
				{
					num = 2;
				}
				break;
			}
		}
		IL_62:
		throw new ArgumentNullException(RecordTableEnumerator.b("㭈⹊ⱌ⭎㑐⅒", a_));
		IL_DB:
		throw new ArgumentNullException(RecordTableEnumerator.b("㥈≊㭌⁎═ݒ㑔㕖㕘㹚", a_));
		IL_DD:
		A_1.ColumnItemsStream = ShapeParser.ReadNodeAsStream(A_0);
	}

	// Token: 0x06002F91 RID: 12177 RVA: 0x001AF6A0 File Offset: 0x001AE6A0
	private static void ᜇ(XmlReader A_0, XlsPivotTable A_1)
	{
		int a_ = 2;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_117;
			case 1:
				spr\u2005.ᜆ(A_0, A_1);
				num = 10;
				continue;
			case 2:
			{
				if (A_0.LocalName != RecordTableEnumerator.b("䠷嬹嬻嬽ؿ⭁⅃⩅ⱇ㥉", a_))
				{
					num = 12;
					continue;
				}
				PivotTableFields pivotFields = A_1.PivotFields;
				A_0.Read();
				num = 0;
				continue;
			}
			case 3:
				if (A_0.LocalName == RecordTableEnumerator.b("䠷嬹嬻嬽ؿ⭁⅃⩅ⱇ", a_))
				{
					num = 1;
					continue;
				}
				goto IL_B9;
			case 5:
				if (A_1 == null)
				{
					num = 7;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_B4;
				default:
					if (false)
					{
					}
					num = 2;
					continue;
				}
				break;
			case 6:
				goto IL_117;
			case 7:
				goto IL_B4;
			case 8:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 11;
					continue;
				}
				num = 3;
				continue;
			case 9:
				goto IL_58;
			case 10:
				goto IL_B9;
			case 11:
				goto IL_137;
			case 12:
				return;
			}
			if (A_0 == null)
			{
				num = 9;
				continue;
			}
			if (true)
			{
			}
			num = 5;
			continue;
			IL_B9:
			A_0.Read();
			num = 6;
			continue;
			IL_117:
			num = 8;
		}
		IL_58:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹崻娽┿ぁ", a_));
		IL_B4:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠷匹䨻儽㐿ᙁ╃⑅⑇⽉", a_));
		IL_137:
		A_0.Read();
	}

	// Token: 0x06002F92 RID: 12178 RVA: 0x001AF854 File Offset: 0x001AE854
	private static void ᜆ(XmlReader A_0, XlsPivotTable A_1)
	{
		int a_ = 6;
		int num = 13;
		for (;;)
		{
			PivotTableFields pivotFields;
			XlsPivotField xlsPivotField;
			int num2;
			switch (num)
			{
			case 0:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("吻圽┿ぁ", a_)))
				{
					num = 2;
					continue;
				}
				goto IL_265;
			case 1:
				if (A_1 == null)
				{
					num = 14;
					continue;
				}
				pivotFields = A_1.PivotFields;
				xlsPivotField = null;
				num2 = 0;
				num = 11;
				continue;
			case 2:
				A_1.PivotPageFields.Add(xlsPivotField);
				num = 12;
				continue;
			case 3:
				num2 = XmlConvert.ToInt32(A_0.Value);
				num = 15;
				continue;
			case 4:
				xlsPivotField.Caption = A_0.Value;
				num = 16;
				continue;
			case 5:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("唻䨽┿⽁", a_)))
				{
					num = 6;
					continue;
				}
				goto IL_D6;
			case 6:
				xlsPivotField.ItemIndex = XmlConvert.ToInt32(A_0.Value);
				num = 10;
				continue;
			case 7:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("弻弽〿", a_)))
				{
					num = 4;
					continue;
				}
				goto IL_78;
			case 8:
				goto IL_73;
			case 9:
				goto IL_1F7;
			case 10:
				goto IL_D6;
			case 11:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("娻刽␿", a_)))
				{
					num = 3;
					continue;
				}
				goto IL_15D;
			case 12:
				goto IL_265;
			case 14:
				goto IL_123;
			case 15:
				goto IL_15D;
			case 16:
				IL_13C:
				goto IL_78;
			case 17:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("刻弽ⴿ❁", a_)))
				{
					num = 18;
					continue;
				}
				goto IL_1F7;
			case 18:
				xlsPivotField.Name = A_0.Value;
				num = 9;
				continue;
			}
			if (A_0 == null)
			{
				num = 8;
				continue;
			}
			num = 1;
			continue;
			IL_78:
			num = 5;
			continue;
			IL_265:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_13C;
			default:
				goto IL_27B;
			}
			IL_D6:
			num = 0;
			continue;
			IL_15D:
			xlsPivotField = pivotFields[num2];
			xlsPivotField.FieldIndex = num2;
			num = 17;
			continue;
			IL_1F7:
			num = 7;
		}
		IL_73:
		throw new ArgumentNullException(RecordTableEnumerator.b("主嬽ℿ♁⅃㑅", a_));
		IL_123:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰻圽㘿ⵁぃቅ⥇⡉⁋⭍", a_));
		IL_27B:
		if (false)
		{
		}
		if (true)
		{
		}
	}

	// Token: 0x06002F93 RID: 12179 RVA: 0x001AFAEC File Offset: 0x001AEAEC
	private static void ᜅ(XmlReader A_0, XlsPivotTable A_1)
	{
		int a_ = 10;
		int num = 10;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				if (A_0.LocalName != RecordTableEnumerator.b("␿⍁ぃ❅็⍉⥋≍㑏⅑", a_))
				{
					num = 6;
					continue;
				}
				PivotDataFields dataFields = A_1.DataFields;
				A_0.Read();
				num = 5;
				continue;
			}
			case 1:
				goto IL_BF;
			case 2:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 7;
					continue;
				}
				num = 3;
				continue;
			case 3:
				if (A_0.LocalName == RecordTableEnumerator.b("␿⍁ぃ❅็⍉⥋≍㑏", a_))
				{
					num = 9;
					continue;
				}
				goto IL_C4;
			case 4:
				goto IL_60;
			case 5:
				goto IL_122;
			case 6:
				return;
			case 7:
				goto IL_142;
			case 8:
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_BF;
				default:
					if (false)
					{
					}
					num = 0;
					continue;
				}
				break;
			case 9:
			{
				PivotDataField item = spr\u2005.ᜄ(A_0, A_1);
				PivotDataFields dataFields;
				dataFields.Add(item);
				num = 11;
				continue;
			}
			case 11:
				goto IL_C4;
			case 12:
				goto IL_122;
			}
			if (A_0 == null)
			{
				if (true)
				{
				}
				num = 4;
				continue;
			}
			num = 8;
			continue;
			IL_C4:
			A_0.Read();
			num = 12;
			continue;
			IL_122:
			num = 2;
		}
		IL_60:
		throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁╃≅ⵇ㡉", a_));
		IL_BF:
		throw new ArgumentNullException(RecordTableEnumerator.b("〿⭁㉃⥅㱇ṉⵋⱍ㱏㝑", a_));
		IL_142:
		A_0.Read();
	}

	// Token: 0x06002F94 RID: 12180 RVA: 0x001AFCAC File Offset: 0x001AECAC
	private static PivotDataField ᜄ(XmlReader A_0, XlsPivotTable A_1)
	{
		int a_ = 1;
		switch (0)
		{
		default:
		{
			int num = 12;
			PivotDataField pivotDataField;
			for (;;)
			{
				sprᾷ sprᾷ;
				string a_2;
				SubtotalTypes a_3;
				int a_4;
				XlsPivotField xlsPivotField;
				switch (num)
				{
				case 0:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("夶䰸嘺笼刾㕀ੂ⅄", a_)))
					{
						num = 37;
						continue;
					}
					goto IL_262;
				case 1:
				{
					if (A_1 == null)
					{
						num = 34;
						continue;
					}
					bool isEmptyElement = A_0.IsEmptyElement;
					sprᾷ = A_1.Cache.CacheFields;
					a_2 = null;
					a_3 = SubtotalTypes.Default;
					a_4 = 0;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_123;
					default:
						if (false)
						{
						}
						num = 27;
						continue;
					}
					break;
				}
				case 2:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䐶䰸夺䤼倾㕀≂⥄", a_)))
					{
						num = 25;
						continue;
					}
					goto IL_3EE;
				case 3:
					goto IL_2AC;
				case 4:
					goto IL_25D;
				case 5:
					num = 26;
					continue;
				case 6:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("唶堸䠺堼社⡀♂⥄⍆", a_)))
					{
						goto IL_123;
					}
					goto IL_42F;
				case 7:
					goto IL_262;
				case 8:
					pivotDataField.ShowDataAs = pivotDataField.ᜀ(A_0.Value);
					num = 21;
					continue;
				case 9:
					goto IL_3EE;
				case 10:
					A_0.Read();
					num = 4;
					continue;
				case 11:
					spr\u2005.ᜀ(A_0, pivotDataField);
					num = 35;
					continue;
				case 13:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("唶堸䠺堼瘾㕀♂⡄", a_)))
					{
						num = 24;
						continue;
					}
					goto IL_DB;
				case 14:
					a_4 = XmlConvert.ToInt32(A_0.Value);
					num = 3;
					continue;
				case 15:
				{
					bool isEmptyElement;
					if (!isEmptyElement)
					{
						num = 22;
						continue;
					}
					return pivotDataField;
				}
				case 16:
					goto IL_DB;
				case 17:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 5;
						continue;
					}
					goto IL_1B0;
				}
				case 18:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䐶儸吺䨼笾⁀㝂⑄ن㩈", a_)))
					{
						num = 8;
						continue;
					}
					goto IL_FE;
				case 19:
					goto IL_D6;
				case 20:
					goto IL_42F;
				case 21:
					goto IL_FE;
				case 22:
					A_0.Read();
					num = 31;
					continue;
				case 23:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 10;
						continue;
					}
					num = 28;
					continue;
				case 24:
					pivotDataField.BaseItem = XmlConvert.ToInt32(A_0.Value);
					num = 16;
					continue;
				case 25:
					a_3 = (SubtotalTypes)Enum.Parse(typeof(PivotSubtotalTypes2007), A_0.Value, false);
					num = 9;
					continue;
				case 26:
				{
					string localName;
					if (localName == RecordTableEnumerator.b("制䄸伺焼䰾㕀", a_))
					{
						num = 11;
						continue;
					}
					goto IL_1B0;
				}
				case 27:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("儶唸强", a_)))
					{
						num = 14;
						continue;
					}
					goto IL_2AC;
				case 28:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 29;
						continue;
					}
					goto IL_1B0;
				case 29:
					num = 17;
					continue;
				case 30:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("夶堸嘺堼", a_)))
					{
						num = 32;
						continue;
					}
					goto IL_465;
				case 31:
					goto IL_1B0;
				case 32:
					a_2 = A_0.Value;
					num = 33;
					continue;
				case 33:
					goto IL_465;
				case 34:
					goto IL_4B9;
				case 35:
					goto IL_1B0;
				case 36:
					pivotDataField.BaseField = XmlConvert.ToInt32(A_0.Value);
					num = 20;
					continue;
				case 37:
					xlsPivotField.NumberFormatIndex = XmlConvert.ToInt32(A_0.Value);
					if (true)
					{
					}
					num = 7;
					continue;
				}
				if (A_0 == null)
				{
					num = 19;
					continue;
				}
				num = 1;
				continue;
				IL_DB:
				num = 15;
				continue;
				IL_FE:
				num = 6;
				continue;
				IL_123:
				num = 36;
				continue;
				IL_1B0:
				num = 23;
				continue;
				IL_262:
				num = 2;
				continue;
				IL_2AC:
				xlsPivotField = new XlsPivotField(sprᾷ.ᜀ(a_4), A_1);
				num = 30;
				continue;
				IL_3EE:
				pivotDataField = new PivotDataField(a_2, a_3, xlsPivotField);
				num = 18;
				continue;
				IL_42F:
				num = 13;
				continue;
				IL_465:
				num = 0;
			}
			IL_D6:
			throw new ArgumentNullException(RecordTableEnumerator.b("䔶尸娺夼娾㍀", a_));
			IL_25D:
			return pivotDataField;
			IL_4B9:
			throw new ArgumentNullException(RecordTableEnumerator.b("䜶倸䴺刼䬾ᕀ≂❄⭆ⱈ", a_));
		}
		}
	}

	// Token: 0x06002F95 RID: 12181 RVA: 0x001B01C4 File Offset: 0x001AF1C4
	private static void ᜃ(XmlReader A_0, XlsPivotTable A_1)
	{
		int a_ = 17;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_46;
			case 2:
				if (true)
				{
				}
				if (A_1 == null)
				{
					num = 5;
					continue;
				}
				num = 4;
				continue;
			case 3:
				return;
			case 4:
				if (!(A_0.LocalName != RecordTableEnumerator.b("⑆♈╊⥌♎═㩒㩔㥖㡘㝚᭜ぞ፠๢Ѥ፦ᩨ", a_)))
				{
					goto IL_E0;
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
					num = 3;
					continue;
				}
				break;
			case 5:
				goto IL_DE;
			}
			IL_3B:
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			num = 2;
			continue;
			goto IL_3B;
		}
		IL_46:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕆ⱈ⩊⥌⩎⍐", a_));
		IL_DE:
		throw new ArgumentNullException(RecordTableEnumerator.b("㝆⁈㵊≌㭎Ր㉒㝔㭖㱘", a_));
		IL_E0:
		Stream value = ShapeParser.ReadNodeAsStream(A_0);
		A_1.PreservedElements.Add(RecordTableEnumerator.b("⑆♈╊⥌♎═㩒㩔㥖㡘㝚᭜ぞ፠๢Ѥ፦ᩨ", a_), value);
	}

	// Token: 0x06002F96 RID: 12182 RVA: 0x001B02D4 File Offset: 0x001AF2D4
	private static void ᜂ(XmlReader A_0, XlsPivotTable A_1)
	{
		int a_ = 4;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				if (true)
				{
				}
				break;
			case 2:
				return;
			case 3:
				goto IL_A2;
			case 4:
				if (A_1 == null)
				{
					num = 8;
					continue;
				}
				num = 6;
				continue;
			case 5:
				goto IL_5A;
			case 6:
				if (A_0.LocalName != RecordTableEnumerator.b("䨹唻䠽⼿㙁ృ⽅ⵇ㡉ⵋ㱍㍏㩑㵓㍕⭗", a_))
				{
					num = 2;
					continue;
				}
				num = 7;
				continue;
			case 7:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A2;
				default:
					if (false)
					{
					}
					if (A_1.PreservedElements != null)
					{
						num = 3;
						continue;
					}
					return;
				}
				break;
			case 8:
				goto IL_9D;
			}
			if (A_0 == null)
			{
				num = 5;
				continue;
			}
			num = 4;
			continue;
			IL_A2:
			A_0.MoveToElement();
			Stream value = ShapeParser.ReadNodeAsStream(A_0);
			A_1.PreservedElements.Add(RecordTableEnumerator.b("䨹唻䠽⼿㙁ృ⽅ⵇ㡉ⵋ㱍㍏㩑㵓㍕⭗", a_), value);
			num = 0;
		}
		IL_5A:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠹夻弽␿❁㙃", a_));
		IL_9D:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨹唻䠽⼿㙁၃❅⩇♉⥋", a_));
	}

	// Token: 0x06002F97 RID: 12183 RVA: 0x001B0420 File Offset: 0x001AF420
	private static void ᜁ(XmlReader A_0, XlsPivotTable A_1)
	{
		int a_ = 0;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_116;
			case 2:
				A_1.ShowRowStripes = XmlConvert.ToBoolean(A_0.Value);
				num = 21;
				continue;
			case 3:
				if (true)
				{
				}
				A_1.ShowColStripes = XmlConvert.ToBoolean(A_0.Value);
				num = 4;
				continue;
			case 4:
				goto IL_1CA;
			case 5:
				A_1.ShowColHeaderStyle = XmlConvert.ToBoolean(A_0.Value);
				num = 0;
				continue;
			case 6:
				goto IL_7F;
			case 7:
				goto IL_114;
			case 8:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䔵倷唹䬻爽ℿㅁぃՅ❇♉㥋⍍㹏", a_)))
				{
					num = 19;
					continue;
				}
				goto IL_301;
			case 9:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䔵倷唹䬻紽⼿⹁ృ⍅⥇⹉⥋㱍⍏", a_)))
				{
					num = 5;
					continue;
				}
				goto IL_116;
			case 10:
				goto IL_170;
			case 11:
				A_1.ShowRowHeaderStyle = XmlConvert.ToBoolean(A_0.Value);
				num = 15;
				continue;
			case 12:
				IL_225:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䔵倷唹䬻紽⼿⹁ᝃ㉅㩇⍉㱋⭍⍏", a_)))
				{
					num = 3;
					continue;
				}
				goto IL_1CA;
			case 13:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䔵倷唹䬻氽⼿㕁ᝃ㉅㩇⍉㱋⭍⍏", a_)))
				{
					num = 2;
					continue;
				}
				goto IL_1FE;
			case 14:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䔵倷唹䬻氽⼿㕁ృ⍅⥇⹉⥋㱍⍏", a_)))
				{
					num = 11;
					continue;
				}
				goto IL_2CD;
			case 15:
				goto IL_2CD;
			case 16:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("堵夷圹夻", a_)))
				{
					num = 18;
					continue;
				}
				goto IL_29C;
			case 17:
				if (A_1 == null)
				{
					num = 7;
					continue;
				}
				num = 16;
				continue;
			case 18:
			{
				string value = A_0.Value;
				A_1.BuiltInStyle = new PivotBuiltInStyles?((PivotBuiltInStyles)Enum.Parse(typeof(PivotBuiltInStyles), value, false));
				num = 20;
				continue;
			}
			case 19:
				A_1.ShowLastCol = XmlConvert.ToBoolean(A_0.Value);
				num = 10;
				continue;
			case 20:
				goto IL_29C;
			case 21:
				goto IL_1FE;
			}
			if (A_0 == null)
			{
				num = 6;
				continue;
			}
			num = 17;
			continue;
			IL_116:
			num = 13;
			continue;
			IL_1CA:
			num = 8;
			continue;
			IL_1FE:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_225;
			default:
				if (false)
				{
				}
				num = 12;
				continue;
			}
			IL_29C:
			num = 14;
			continue;
			IL_2CD:
			num = 9;
		}
		IL_7F:
		throw new ArgumentNullException(RecordTableEnumerator.b("䐵崷嬹堻嬽㈿", a_));
		IL_114:
		throw new ArgumentNullException(RecordTableEnumerator.b("䘵儷䰹医䨽ᐿ⍁♃⩅ⵇ", a_));
		IL_170:
		IL_301:
		A_0.Read();
	}

	// Token: 0x06002F98 RID: 12184 RVA: 0x001B0738 File Offset: 0x001AF738
	private static void ᜀ(XmlReader A_0, XlsPivotTable A_1)
	{
		int a_ = 9;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_9A;
			case 1:
				if (A_1 == null)
				{
					num = 4;
					continue;
				}
				num = 3;
				continue;
			case 3:
				if (A_0.LocalName != RecordTableEnumerator.b("䴾⹀㑂ൄ⹆ⱈ㥊ⱌ㵎㉐㭒㱔㉖⩘๚⹜㹞٠٢", a_))
				{
					num = 8;
					continue;
				}
				num = 7;
				continue;
			case 4:
				goto IL_95;
			case 5:
				goto IL_CD;
			case 6:
				goto IL_52;
			case 7:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_9A;
				default:
					if (false)
					{
					}
					if (A_1.PreservedElements != null)
					{
						num = 0;
						continue;
					}
					goto IL_136;
				}
				break;
			case 8:
				return;
			}
			if (A_0 == null)
			{
				num = 6;
				continue;
			}
			num = 1;
			continue;
			IL_9A:
			A_0.MoveToElement();
			Stream value = ShapeParser.ReadNodeAsStream(A_0);
			A_1.PreservedElements.Add(RecordTableEnumerator.b("䴾⹀㑂ൄ⹆ⱈ㥊ⱌ㵎㉐㭒㱔㉖⩘๚⹜㹞٠٢", a_), value);
			num = 5;
		}
		IL_52:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴾⑀≂⅄≆㭈", a_));
		IL_95:
		throw new ArgumentNullException(RecordTableEnumerator.b("伾⡀㕂⩄㍆ᵈ⩊⽌⍎㑐", a_));
		IL_CD:
		IL_136:
		if (true)
		{
		}
	}

	// Token: 0x06002F99 RID: 12185 RVA: 0x001B0884 File Offset: 0x001AF884
	public static SubtotalTypes ᜀ(SubtotalTypes A_0, string A_1)
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
			if (XmlConvert.ToBoolean(A_1))
			{
				return A_0;
			}
			break;
		}
		if (true)
		{
		}
		return SubtotalTypes.None;
	}

	// Token: 0x0400154F RID: 5455
	private const int ᜀ = -2;

	// Token: 0x02000304 RID: 772
	private class ᜀ : IComparable
	{
		// Token: 0x06002F9A RID: 12186 RVA: 0x001B08CC File Offset: 0x001AF8CC
		public int ᜀ(object A_0)
		{
			int num;
			for (;;)
			{
				spr\u2005.ᜀ ᜀ = A_0 as spr\u2005.ᜀ;
				num = 1;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (true)
						{
						}
						if (ᜀ != null)
						{
							num2 = 1;
							continue;
						}
						return num;
					case 1:
						num = this.ᜂ.Compare(this.ᜀ, ᜀ.ᜀ);
						num2 = 4;
						continue;
					case 2:
						return num;
					case 3:
						num = this.ᜂ.Compare(this.ᜁ, ᜀ.ᜁ);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							num2 = 2;
							continue;
						}
						break;
					case 4:
						if (num == 0)
						{
							num2 = 3;
							continue;
						}
						return num;
					}
					break;
				}
			}
			return num;
		}

		// Token: 0x04001550 RID: 5456
		public object ᜀ;

		// Token: 0x04001551 RID: 5457
		public int ᜁ;

		// Token: 0x04001552 RID: 5458
		public IComparer ᜂ = new spr\u2005.ᜀ.ᜀ();

		// Token: 0x02000305 RID: 773
		private class ᜀ : IComparer
		{
			// Token: 0x06002F9C RID: 12188 RVA: 0x001B09CC File Offset: 0x001AF9CC
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

			// Token: 0x04001553 RID: 5459
			private IComparer ᜀ = Comparer<object>.Default;
		}
	}
}
