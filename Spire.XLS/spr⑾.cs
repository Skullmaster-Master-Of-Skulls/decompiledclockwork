using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Xml;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Interfaces;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlSerialization;

// Token: 0x0200041C RID: 1052
internal class spr\u247E : XlsObject
{
	// Token: 0x06003EC2 RID: 16066 RVA: 0x0022E0A0 File Offset: 0x0022D0A0
	static spr\u247E()
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
		spr\u247E.\u171C = new Dictionary<string, HorizontalAlignType>(9);
		spr\u247E.\u171D = new Dictionary<string, VerticalAlignType>(7);
		spr\u247E.\u171E = new Dictionary<string, string>(10);
		spr\u247E.\u171C.Add(RecordTableEnumerator.b("紻䬽㐿ⵁ⥃❅㱇⍉⽋", a_), HorizontalAlignType.General);
		spr\u247E.\u171C.Add(RecordTableEnumerator.b("瀻嬽☿㙁", a_), HorizontalAlignType.Left);
		spr\u247E.\u171C.Add(RecordTableEnumerator.b("缻嬽⸿㙁⅃㑅", a_), HorizontalAlignType.Center);
		spr\u247E.\u171C.Add(RecordTableEnumerator.b("渻圽✿⩁ぃ", a_), HorizontalAlignType.Right);
		spr\u247E.\u171C.Add(RecordTableEnumerator.b("搻刽㌿сⵃ⩅⑇", a_), HorizontalAlignType.Fill);
		spr\u247E.\u171C.Add(RecordTableEnumerator.b("瘻䬽㌿㙁ⵃ⁅ㅇ", a_), HorizontalAlignType.Justify);
		spr\u247E.\u171C.Add(RecordTableEnumerator.b("缻嬽⸿㙁⅃㑅े⥉㹋⅍⍏⅑ݓ㍕㑗㽙㽛⩝य़ൡ੣", a_), HorizontalAlignType.CenterAcrossSelection);
		spr\u247E.\u171C.Add(RecordTableEnumerator.b("砻圽㌿㙁㙃⽅⩇㽉㡋⭍㑏", a_), HorizontalAlignType.Distributed);
		spr\u247E.\u171C.Add(RecordTableEnumerator.b("瘻䬽㌿㙁ⵃ⁅ㅇ้╋㵍⑏⁑㵓㑕ⵗ⹙㥛㩝", a_), HorizontalAlignType.General);
		spr\u247E.\u171D.Add(RecordTableEnumerator.b("紻䬽㐿ⵁ⥃❅㱇⍉⽋", a_), VerticalAlignType.Bottom);
		spr\u247E.\u171D.Add(RecordTableEnumerator.b("栻儽〿", a_), VerticalAlignType.Top);
		spr\u247E.\u171D.Add(RecordTableEnumerator.b("縻儽㐿㙁⭃⭅", a_), VerticalAlignType.Bottom);
		spr\u247E.\u171D.Add(RecordTableEnumerator.b("缻嬽⸿㙁⅃㑅", a_), VerticalAlignType.Center);
		spr\u247E.\u171D.Add(RecordTableEnumerator.b("瘻䬽㌿㙁ⵃ⁅ㅇ", a_), VerticalAlignType.Justify);
		spr\u247E.\u171D.Add(RecordTableEnumerator.b("砻圽㌿㙁㙃⽅⩇㽉㡋⭍㑏", a_), VerticalAlignType.Distributed);
		spr\u247E.\u171D.Add(RecordTableEnumerator.b("瘻䬽㌿㙁ⵃ⁅ㅇ้╋㵍⑏⁑㵓㑕ⵗ⹙㥛㩝", a_), VerticalAlignType.Bottom);
		spr\u247E.\u171E.Add(RecordTableEnumerator.b("稻圽㠿❁⁃", a_), RecordTableEnumerator.b("఻ွ瀿牁", a_));
		spr\u247E.\u171E.Add(RecordTableEnumerator.b("漻䨽ℿⱁ⁃❅㩇⹉", a_), RecordTableEnumerator.b("Ἳሽ挿慁瑃桅硇穉", a_));
		spr\u247E.\u171E.Add(RecordTableEnumerator.b("氻嬽㈿⅁⅃⡅㱇", a_), RecordTableEnumerator.b("఻ွ瀿牁慃", a_));
		spr\u247E.\u171E.Add(RecordTableEnumerator.b("漻崽⤿❁⩃㉅ⅇⱉ╋ⵍ", a_), RecordTableEnumerator.b("఻ွ瀿牁Ń浅硇", a_));
		spr\u247E.\u171E.Add(RecordTableEnumerator.b("漻嘽⼿ぁぃ晅ే⭉㡋⭍", a_), RecordTableEnumerator.b("儻ᄽ␿流㵃㽅ㅇ㍉", a_));
		spr\u247E.\u171E.Add(RecordTableEnumerator.b("焻嬽␿⭁ㅃ⭅桇้ⵋ㩍㕏", a_), RecordTableEnumerator.b("堻戽洿⽁⥃⭅ᑇ杉㕋㝍", a_));
		spr\u247E.\u171E.Add(RecordTableEnumerator.b("焻嬽␿⭁ㅃ⭅桇ṉ╋⍍㕏", a_), RecordTableEnumerator.b("吻нⴿ⽁摃݅Շ敉᱋͍", a_));
		spr\u247E.\u171E.Add(RecordTableEnumerator.b("瀻儽⸿╁摃ቅⅇ❉⥋", a_), RecordTableEnumerator.b("吻нⴿ⽁繃㕅㭇橉ോ͍罏ɑᥓ", a_));
		spr\u247E.\u171E.Add(RecordTableEnumerator.b("漻嘽⼿ぁぃ晅᱇⍉⅋⭍", a_), RecordTableEnumerator.b("吻нⴿ⽁", a_));
		spr\u247E.\u171E.Add(RecordTableEnumerator.b("笻嬽⸿❁㙃❅⑇橉ࡋ⽍⑏㝑", a_), RecordTableEnumerator.b("儻ᄽ␿流㵃㽅桇≉癋⍍㵏", a_));
	}

	// Token: 0x06003EC3 RID: 16067 RVA: 0x0022E414 File Offset: 0x0022D414
	public spr\u247E(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x06003EC4 RID: 16068 RVA: 0x0022E44C File Offset: 0x0022D44C
	private void ᜃ(XmlReader A_0, XlsWorkbook A_1)
	{
		int a_ = 9;
		int num = 11;
		for (;;)
		{
			XlsWorksheet xlsWorksheet;
			switch (num)
			{
			case 0:
				num = 28;
				continue;
			case 1:
				goto IL_2F1;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_12B;
				default:
					if (false)
					{
					}
					goto IL_429;
				}
				break;
			case 3:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("䨾㍀ⵂ罄㑆⩈⍊⡌≎ぐ⁒硔㩖じ㡚⽜ぞበౢͤ፦䑨ࡪɬɮ䭰ᱲ፴ᅶၸ᡺᡼䕾ﮂ", a_))
				{
					num = 25;
					continue;
				}
				goto IL_497;
			case 4:
				if (A_0.Value != null)
				{
					num = 0;
					continue;
				}
				goto IL_355;
			case 5:
				goto IL_2C9;
			case 6:
				goto IL_15C;
			case 7:
				num = 4;
				continue;
			case 8:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("䨾㍀ⵂ罄㑆⩈⍊⡌≎ぐ⁒硔㩖じ㡚⽜ぞበౢͤ፦䑨ࡪɬɮ䭰ᱲ፴ᅶၸ᡺᡼䕾ﮂ", a_))
				{
					num = 30;
					continue;
				}
				goto IL_15C;
			case 9:
				num = 3;
				continue;
			case 10:
				if (A_0.LocalName == RecordTableEnumerator.b("栾⹀ㅂ⹄㑆ⅈ⹊⡌㭎Ṑ⍒⅔㹖㙘㕚⹜", a_))
				{
					num = 21;
					continue;
				}
				goto IL_15C;
			case 12:
				goto IL_CD;
			case 13:
				if (A_0.IsEmptyElement)
				{
					num = 36;
					continue;
				}
				A_0.Read();
				num = 19;
				continue;
			case 14:
				if (A_0.LocalName == RecordTableEnumerator.b("焾⁀⹂⁄㑆", a_))
				{
					num = 23;
					continue;
				}
				goto IL_1D7;
			case 15:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("䨾㍀ⵂ罄㑆⩈⍊⡌≎ぐ⁒硔㩖じ㡚⽜ぞበౢͤ፦䑨ࡪɬɮ䭰ᱲ፴ᅶၸ᡺᡼䕾ﺌ", a_))
				{
					num = 34;
					continue;
				}
				goto IL_429;
			case 16:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("焾⁀⹂⁄", a_), RecordTableEnumerator.b("䨾㍀ⵂ罄㑆⩈⍊⡌≎ぐ⁒硔㩖じ㡚⽜ぞበౢͤ፦䑨ࡪɬɮ䭰ᱲ፴ᅶၸ᡺᡼䕾ﺌ", a_)))
				{
					num = 7;
					continue;
				}
				goto IL_355;
			case 17:
				this.ᜁ(A_0, xlsWorksheet.Names, xlsWorksheet.Index + 1);
				num = 22;
				continue;
			case 18:
				return;
			case 19:
				goto IL_2C9;
			case 20:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 18;
					continue;
				}
				num = 37;
				continue;
			case 21:
				num = 8;
				continue;
			case 22:
				goto IL_1D7;
			case 23:
				num = 31;
				continue;
			case 24:
				num = 15;
				continue;
			case 25:
				this.ᜉ(A_0, xlsWorksheet);
				num = 27;
				continue;
			case 26:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("派⡀⑂ⵄ㍆ᵈ⑊Ō⩎㝐❒", a_), RecordTableEnumerator.b("䨾㍀ⵂ罄㑆⩈⍊⡌≎ぐ⁒硔㩖じ㡚⽜ぞበౢͤ፦䑨ࡪɬɮ䭰ᱲ፴ᅶၸ᡺᡼䕾ﺌ", a_)))
				{
					num = 35;
					continue;
				}
				goto IL_2F1;
			case 27:
				if (true)
				{
				}
				goto IL_497;
			case 28:
				if (A_0.Value.Length > 0)
				{
					num = 33;
					continue;
				}
				goto IL_355;
			case 29:
				if (A_0.LocalName == RecordTableEnumerator.b("漾⁀⑂⁄Ն㭈⹊ⱌ⑎≐", a_))
				{
					num = 9;
					continue;
				}
				goto IL_497;
			case 30:
				this.ᜈ(A_0, xlsWorksheet);
				num = 6;
				continue;
			case 31:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("䨾㍀ⵂ罄㑆⩈⍊⡌≎ぐ⁒硔㩖じ㡚⽜ぞበౢͤ፦䑨ࡪɬɮ䭰ᱲ፴ᅶၸ᡺᡼䕾ﺌ", a_))
				{
					num = 17;
					continue;
				}
				goto IL_1D7;
			case 32:
				if (A_1 == null)
				{
					num = 38;
					continue;
				}
				num = 16;
				continue;
			case 33:
				xlsWorksheet = (XlsWorksheet)A_1.Worksheets.Create(A_0.Value);
				num = 39;
				continue;
			case 34:
				goto IL_12B;
			case 35:
				xlsWorksheet.IsRightToLeft = XmlConvert.ToBoolean(A_0.Value);
				num = 1;
				continue;
			case 36:
				return;
			case 37:
				if (A_0.LocalName == RecordTableEnumerator.b("款⁀⅂⥄≆", a_))
				{
					num = 24;
					continue;
				}
				goto IL_429;
			case 38:
				goto IL_4C9;
			case 39:
				num = 26;
				continue;
			}
			if (A_0 == null)
			{
				num = 12;
				continue;
			}
			num = 32;
			continue;
			IL_12B:
			this.ᜊ(A_0, xlsWorksheet);
			num = 2;
			continue;
			IL_15C:
			num = 29;
			continue;
			IL_1D7:
			num = 10;
			continue;
			IL_2C9:
			num = 20;
			continue;
			IL_2F1:
			A_0.MoveToElement();
			num = 13;
			continue;
			IL_429:
			num = 14;
			continue;
			IL_497:
			A_0.Skip();
			num = 5;
		}
		IL_CD:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴾⑀≂⅄≆㭈", a_));
		IL_355:
		throw new spr\u23EE(RecordTableEnumerator.b("栾⹀ㅂ⹄㑆ⅈ⹊⡌㭎", a_), RecordTableEnumerator.b("栾⹀ㅂ⹄㑆ⅈ⹊⡌㭎煐㵒㑔㩖㱘筚㱜⭞ᕠᅢ౤զᱨὪ࡬佮ተቲ᭴坶᝸ᑺॼ彾ꖄﺊ뾐", a_));
		IL_4C9:
		throw new ArgumentNullException(RecordTableEnumerator.b("崾⹀ⱂ⹄", a_));
	}

	// Token: 0x06003EC5 RID: 16069 RVA: 0x0022E994 File Offset: 0x0022D994
	private void ᜊ(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 18;
		int num = 7;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_8F;
			case 1:
			{
				int a_2 = this.ᜀ(A_0, A_1, a_2);
				num = 12;
				continue;
			}
			case 2:
				num = 4;
				continue;
			case 3:
				if (A_0.LocalName == RecordTableEnumerator.b("ᩇ╉㭋", a_))
				{
					num = 15;
					continue;
				}
				goto IL_313;
			case 4:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("㵇㡉≋瑍⍏ㅑ㱓㍕㕗㭙⽛獝ൟୡݣᑥݧᥩͫ࡭ѯ影ᝳ᥵ᕷ䁹፻᡽늇黎ﲋﲍﾙ鍊", a_))
				{
					num = 1;
					continue;
				}
				goto IL_179;
			case 5:
			{
				double num2 = XmlConvert.ToDouble(A_0.Value);
				num2 = base.ReservedHandle.ᜀ((double)((float)num2), MeasureUnits.Point, MeasureUnits.Pixel);
				num2 = A_1.PixelsToColumnWidth((double)((int)num2));
				A_1.DefaultColumnWidth = num2;
				num = 19;
				continue;
			}
			case 6:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("ే⽉⩋⽍╏㹑⁓ѕ㝗ⵙᑛ㭝य़աౣብ", a_), RecordTableEnumerator.b("㵇㡉≋瑍⍏ㅑ㱓㍕㕗㭙⽛獝ൟୡݣᑥݧᥩͫ࡭ѯ影ᝳ᥵ᕷ䁹፻᡽늇黎ﲋﲍﾙ鍊", a_)))
				{
					num = 9;
					continue;
				}
				goto IL_283;
			case 8:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("㵇㡉≋瑍⍏ㅑ㱓㍕㕗㭙⽛獝ൟୡݣᑥݧᥩͫ࡭ѯ影ᝳ᥵ᕷ䁹፻᡽늇黎ﲋﲍﾙ鍊", a_))
				{
					num = 25;
					continue;
				}
				goto IL_313;
			case 9:
				A_1.DefaultRowHeight = XmlConvert.ToDouble(A_0.Value);
				A_1.StandardHeightFlag = true;
				num = 14;
				continue;
			case 10:
				goto IL_1F5;
			case 11:
				return;
			case 12:
				goto IL_179;
			case 13:
				if (A_0.LocalName == RecordTableEnumerator.b("େ╉⁋㭍㵏㱑", a_))
				{
					num = 2;
					continue;
				}
				goto IL_179;
			case 14:
				goto IL_283;
			case 15:
				num = 8;
				continue;
			case 16:
				goto IL_13B;
			case 17:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("ే⽉⩋⽍╏㹑⁓ᕕ㝗㙙⥛㍝๟㕡ൣɥᱧɩ", a_), RecordTableEnumerator.b("㵇㡉≋瑍⍏ㅑ㱓㍕㕗㭙⽛獝ൟୡݣᑥݧᥩͫ࡭ѯ影ᝳ᥵ᕷ䁹፻᡽늇黎ﲋﲍﾙ鍊", a_)))
				{
					num = 5;
					continue;
				}
				goto IL_2DE;
			case 18:
				goto IL_1F5;
			case 19:
				goto IL_2DE;
			case 20:
				if (A_1 == null)
				{
					num = 16;
					continue;
				}
				num = 6;
				continue;
			case 21:
			{
				if (A_0.IsEmptyElement)
				{
					num = 22;
					continue;
				}
				A_0.Read();
				int a_3 = 0;
				int a_2 = 0;
				num = 18;
				continue;
			}
			case 22:
				return;
			case 23:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 11;
					continue;
				}
				num = 3;
				continue;
			case 24:
				goto IL_313;
			case 25:
			{
				int a_3 = this.ᜁ(A_0, A_1, a_3);
				num = 24;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			IL_122:
			num = 20;
			continue;
			IL_283:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_122;
			default:
				if (false)
				{
				}
				num = 17;
				continue;
			}
			IL_179:
			A_0.Skip();
			num = 10;
			continue;
			IL_1F5:
			num = 23;
			continue;
			IL_2DE:
			A_0.MoveToElement();
			if (true)
			{
			}
			num = 21;
			continue;
			IL_313:
			num = 13;
		}
		IL_8F:
		throw new ArgumentNullException(RecordTableEnumerator.b("㩇⽉ⵋ⩍㕏⁑", a_));
		IL_13B:
		throw new ArgumentNullException(RecordTableEnumerator.b("㭇≉⥋⭍⑏", a_));
	}

	// Token: 0x06003EC6 RID: 16070 RVA: 0x0022ED24 File Offset: 0x0022DD24
	private int ᜁ(XmlReader A_0, XlsWorksheet A_1, int A_2)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			int num = 1;
			int num7;
			for (;;)
			{
				int num2;
				int num3;
				int num4;
				bool a_2;
				bool flag2;
				switch (num)
				{
				case 0:
					num2 = XmlConvert.ToInt32(A_0.Value);
					goto IL_2EB;
				case 2:
				{
					if (num3 > num4)
					{
						num = 19;
						continue;
					}
					sprᱧ sprᱧ = sprᜑ.ᜀ(A_1, num3 - 1, true);
					double num5 = Math.Min(num5, 409.5);
					sprᱧ.ᜃ((ushort)(num5 * 20.0));
					sprᱧ.ᜊ(true);
					int num6;
					sprᱧ.ᜀ((ushort)num6);
					sprᱧ.ᜅ(a_2);
					num = 38;
					continue;
				}
				case 3:
				{
					if (A_1 == null)
					{
						num = 16;
						continue;
					}
					bool flag = false;
					num7 = 0;
					double num5 = A_1.DefaultRowHeight;
					int num6 = A_1.ParentWorkbook.DefaultXFIndex;
					int a_3 = 0;
					num = 9;
					continue;
				}
				case 4:
				{
					bool flag;
					if (flag)
					{
						num = 33;
						continue;
					}
					goto IL_6DB;
				}
				case 5:
					goto IL_5E4;
				case 6:
				{
					bool flag = XmlConvert.ToBoolean(A_0.Value);
					num = 23;
					continue;
				}
				case 7:
					num = 4;
					continue;
				case 8:
					if (A_0.LocalName == RecordTableEnumerator.b("稸帺儼匾", a_))
					{
						num = 32;
						continue;
					}
					goto IL_28B;
				case 9:
					if (!A_0.MoveToAttribute(RecordTableEnumerator.b("瀸唺夼娾㥀", a_), RecordTableEnumerator.b("䰸䤺匼Ծ㉀⁂ⵄ≆⑈⩊㹌扎㱐㩒㙔╖㙘⡚㉜㥞ᕠ乢٤ࡦѨ兪ɬ८ᝰᩲᙴቶ䍸ࡺർൾﮎ", a_)))
					{
						num = 20;
						continue;
					}
					num = 0;
					continue;
				case 10:
					A_1.AutoFitRow(A_2);
					goto IL_2A9;
				case 11:
					flag2 = false;
					goto IL_43A;
				case 12:
					if (A_0.NamespaceURI == RecordTableEnumerator.b("䰸䤺匼Ծ㉀⁂ⵄ≆⑈⩊㹌扎㱐㩒㙔╖㙘⡚㉜㥞ᕠ乢٤ࡦѨ兪ɬ८ᝰᩲᙴቶ䍸ࡺർൾﮎ", a_))
					{
						num = 14;
						continue;
					}
					goto IL_28B;
				case 13:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2A9;
					default:
					{
						if (false)
						{
						}
						double num5;
						if (num5 == A_1.DefaultRowHeight)
						{
							num = 10;
							continue;
						}
						goto IL_6DB;
					}
					}
					break;
				case 14:
				{
					int a_3 = this.ᜁ(A_0, A_1, A_2, a_3);
					num = 22;
					continue;
				}
				case 15:
					goto IL_3E0;
				case 16:
					goto IL_5A7;
				case 17:
					goto IL_13D;
				case 18:
					goto IL_2B5;
				case 19:
					A_0.MoveToElement();
					num = 35;
					continue;
				case 20:
					num = 40;
					continue;
				case 21:
					if (A_1.FirstRow > num3)
					{
						num = 39;
						continue;
					}
					goto IL_562;
				case 22:
					goto IL_28B;
				case 23:
					goto IL_39C;
				case 24:
					if (!A_0.MoveToAttribute(RecordTableEnumerator.b("焸刺夼嬾⑀ⵂ", a_), RecordTableEnumerator.b("䰸䤺匼Ծ㉀⁂ⵄ≆⑈⩊㹌扎㱐㩒㙔╖㙘⡚㉜㥞ᕠ乢٤ࡦѨ兪ɬ८ᝰᩲᙴቶ䍸ࡺർൾﮎ", a_)))
					{
						num = 42;
						continue;
					}
					num = 36;
					continue;
				case 25:
					goto IL_10E;
				case 26:
					goto IL_562;
				case 27:
				{
					double num5 = XmlConvert.ToDouble(A_0.Value);
					num = 29;
					continue;
				}
				case 28:
					num = 21;
					continue;
				case 29:
					goto IL_247;
				case 30:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 7;
						continue;
					}
					num = 8;
					continue;
				case 31:
					goto IL_1D4;
				case 32:
					num = 12;
					continue;
				case 33:
					num = 13;
					continue;
				case 34:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("焸帺吼堾⥀㝂", a_), RecordTableEnumerator.b("䰸䤺匼Ծ㉀⁂ⵄ≆⑈⩊㹌扎㱐㩒㙔╖㙘⡚㉜㥞ᕠ乢٤ࡦѨ兪ɬ८ᝰᩲᙴቶ䍸ࡺർൾﮎ", a_)))
					{
						num = 27;
						continue;
					}
					goto IL_247;
				case 35:
					if (A_0.IsEmptyElement)
					{
						num = 17;
						continue;
					}
					A_0.Read();
					num = 37;
					continue;
				case 36:
					flag2 = XmlConvert.ToBoolean(A_0.Value);
					goto IL_43A;
				case 37:
					goto IL_5E4;
				case 38:
					if (A_1.FirstRow >= 0)
					{
						num = 28;
						continue;
					}
					goto IL_142;
				case 39:
					goto IL_142;
				case 40:
					num2 = ++A_2;
					goto IL_2EB;
				case 41:
					A_1.LastRow = num3;
					num = 45;
					continue;
				case 42:
					num = 11;
					continue;
				case 43:
					num7 = XmlConvert.ToInt32(A_0.Value);
					num = 51;
					continue;
				case 44:
					goto IL_1D4;
				case 45:
					goto IL_5AC;
				case 46:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("砸为䤼倾݀⩂ㅄཆⱈ≊⩌❎═", a_), RecordTableEnumerator.b("䰸䤺匼Ծ㉀⁂ⵄ≆⑈⩊㹌扎㱐㩒㙔╖㙘⡚㉜㥞ᕠ乢٤ࡦѨ兪ɬ८ᝰᩲᙴቶ䍸ࡺർൾﮎ", a_)))
					{
						num = 6;
						continue;
					}
					goto IL_39C;
				case 47:
					if (A_1.LastRow < num3)
					{
						num = 41;
						continue;
					}
					goto IL_5AC;
				case 48:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("樸䬺尼儾", a_), RecordTableEnumerator.b("䰸䤺匼Ծ㉀⁂ⵄ≆⑈⩊㹌扎㱐㩒㙔╖㙘⡚㉜㥞ᕠ乢٤ࡦѨ兪ɬ८ᝰᩲᙴቶ䍸ࡺർൾﮎ", a_)))
					{
						num = 43;
						continue;
					}
					goto IL_5C3;
				case 49:
				{
					int num6 = this.\u1717[A_0.Value];
					num = 15;
					continue;
				}
				case 50:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("樸伺䐼匾⑀ੂń", a_), RecordTableEnumerator.b("䰸䤺匼Ծ㉀⁂ⵄ≆⑈⩊㹌扎㱐㩒㙔╖㙘⡚㉜㥞ᕠ乢٤ࡦѨ兪ɬ८ᝰᩲᙴቶ䍸ࡺർൾﮎ", a_)))
					{
						num = 49;
						continue;
					}
					goto IL_3E0;
				case 51:
					goto IL_5C3;
				}
				if (A_0 == null)
				{
					num = 25;
					continue;
				}
				num = 3;
				continue;
				IL_142:
				A_1.FirstRow = num3;
				num = 26;
				continue;
				IL_1D4:
				num = 2;
				continue;
				IL_247:
				num = 24;
				continue;
				IL_28B:
				A_0.Skip();
				num = 5;
				continue;
				IL_2A9:
				num = 18;
				continue;
				IL_2EB:
				A_2 = num2;
				num = 34;
				continue;
				IL_39C:
				num = 48;
				continue;
				IL_3E0:
				num = 46;
				continue;
				IL_43A:
				a_2 = flag2;
				num = 50;
				continue;
				IL_562:
				num = 47;
				continue;
				IL_5AC:
				num3++;
				num = 44;
				continue;
				IL_5C3:
				num3 = A_2;
				num4 = A_2 + num7;
				if (true)
				{
				}
				num = 31;
				continue;
				IL_5E4:
				num = 30;
			}
			IL_10E:
			throw new ArgumentNullException(RecordTableEnumerator.b("䬸帺尼嬾⑀ㅂ", a_));
			IL_13D:
			return A_2 + num7;
			IL_2B5:
			goto IL_6DB;
			IL_5A7:
			throw new ArgumentNullException(RecordTableEnumerator.b("䨸区堼娾㕀", a_));
			IL_6DB:
			return A_2 + num7;
		}
		}
	}

	// Token: 0x06003EC7 RID: 16071 RVA: 0x0022F410 File Offset: 0x0022E410
	private int ᜀ(XmlReader A_0, XlsWorksheet A_1, int A_2)
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			int num = 10;
			int num4;
			for (;;)
			{
				int num5;
				int num6;
				int num7;
				switch (num)
				{
				case 0:
				{
					double num2;
					if (num2 == A_1.DefaultColumnWidth)
					{
						num = 27;
						continue;
					}
					goto IL_4EA;
				}
				case 1:
					goto IL_E0;
				case 2:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("漻䨽㤿⹁⅃ཅే", a_), RecordTableEnumerator.b("䤻䰽⸿硁㝃╅⁇⽉⅋⽍⍏网㥓㽕㭗⡙㍛ⵝཟѡၣ䭥୧թū呭Ὧᑱታή᭷ό䙻ൽ黎", a_)))
					{
						num = 4;
						continue;
					}
					goto IL_172;
				case 3:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("漻丽ℿⱁ", a_), RecordTableEnumerator.b("䤻䰽⸿硁㝃╅⁇⽉⅋⽍⍏网㥓㽕㭗⡙㍛ⵝཟѡၣ䭥୧թū呭Ὧᑱታή᭷ό䙻ൽ黎", a_)))
					{
						num = 8;
						continue;
					}
					goto IL_450;
				case 4:
				{
					int num3 = this.\u1717[A_0.Value];
					num = 22;
					continue;
				}
				case 5:
					goto IL_1DF;
				case 6:
				{
					bool flag = XmlConvert.ToBoolean(A_0.Value);
					num = 5;
					continue;
				}
				case 7:
					if (!A_0.MoveToAttribute(RecordTableEnumerator.b("画倽␿❁㱃", a_), RecordTableEnumerator.b("䤻䰽⸿硁㝃╅⁇⽉⅋⽍⍏网㥓㽕㭗⡙㍛ⵝཟѡၣ䭥୧թū呭Ὧᑱታή᭷ό䙻ൽ黎", a_)))
					{
						num = 29;
						continue;
					}
					num = 17;
					continue;
				case 8:
					num4 = XmlConvert.ToInt32(A_0.Value);
					num = 26;
					continue;
				case 9:
					num = 15;
					continue;
				case 11:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_279;
					default:
						if (false)
						{
						}
						if (A_0.MoveToAttribute(RecordTableEnumerator.b("琻圽␿♁⅃⡅", a_), RecordTableEnumerator.b("䤻䰽⸿硁㝃╅⁇⽉⅋⽍⍏网㥓㽕㭗⡙㍛ⵝཟѡၣ䭥୧թū呭Ὧᑱታή᭷ό䙻ൽ黎", a_)))
						{
							num = 12;
							continue;
						}
						goto IL_E0;
					}
					break;
				case 12:
				{
					bool a_2 = XmlConvert.ToBoolean(A_0.Value);
					num = 1;
					continue;
				}
				case 13:
					if (true)
					{
					}
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("欻圽␿㙁ⱃ", a_), RecordTableEnumerator.b("䤻䰽⸿硁㝃╅⁇⽉⅋⽍⍏网㥓㽕㭗⡙㍛ⵝཟѡၣ䭥୧թū呭Ὧᑱታή᭷ό䙻ൽ黎", a_)))
					{
						num = 21;
						continue;
					}
					goto IL_296;
				case 14:
					goto IL_138;
				case 15:
				{
					bool flag;
					if (flag)
					{
						num = 18;
						continue;
					}
					goto IL_4EA;
				}
				case 16:
					goto IL_296;
				case 17:
					num5 = XmlConvert.ToInt32(A_0.Value);
					goto IL_356;
				case 18:
					goto IL_279;
				case 19:
					goto IL_BE;
				case 20:
					goto IL_291;
				case 21:
				{
					double num2 = XmlConvert.ToDouble(A_0.Value);
					num2 = base.ReservedHandle.ᜀ((double)((float)num2), MeasureUnits.Point, MeasureUnits.Pixel);
					num2 = A_1.PixelsToColumnWidth((double)((int)num2)) * 256.0;
					num = 16;
					continue;
				}
				case 22:
					goto IL_172;
				case 23:
					goto IL_138;
				case 24:
					num5 = ++A_2;
					goto IL_356;
				case 25:
				{
					if (A_1 == null)
					{
						num = 30;
						continue;
					}
					double num2 = A_1.DefaultColumnWidth * 256.0;
					bool a_2 = false;
					bool flag = false;
					num4 = 0;
					int num3 = A_1.ParentWorkbook.DefaultXFIndex;
					num = 7;
					continue;
				}
				case 26:
					goto IL_450;
				case 27:
					A_1.AutoFitColumn(A_2);
					num = 20;
					continue;
				case 28:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("紻䬽㐿ⵁɃ⽅㱇ᵉ╋⩍⑏㩑", a_), RecordTableEnumerator.b("䤻䰽⸿硁㝃╅⁇⽉⅋⽍⍏网㥓㽕㭗⡙㍛ⵝཟѡၣ䭥୧թū呭Ὧᑱታή᭷ό䙻ൽ黎", a_)))
					{
						num = 6;
						continue;
					}
					goto IL_1DF;
				case 29:
					num = 24;
					continue;
				case 30:
					goto IL_4E5;
				case 31:
				{
					if (num6 > num7)
					{
						num = 9;
						continue;
					}
					spr\u216E spr_u216E = (spr\u216E)spr\u175E.ᜀ(TBIFFRecord.ColumnInfo);
					spr_u216E.ᜄ((ushort)(num6 - 1));
					spr_u216E.ᜀ((ushort)(num6 - 1));
					bool a_2;
					spr_u216E.ᜄ(a_2);
					int num3;
					spr_u216E.ᜃ((ushort)num3);
					double num2;
					spr_u216E.ᜅ((ushort)num2);
					A_1.ColumnInformation[num6] = spr_u216E;
					num6++;
					num = 23;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 19;
					continue;
				}
				num = 25;
				continue;
				IL_E0:
				num = 2;
				continue;
				IL_138:
				num = 31;
				continue;
				IL_172:
				num = 3;
				continue;
				IL_1DF:
				num = 11;
				continue;
				IL_279:
				num = 0;
				continue;
				IL_296:
				num = 28;
				continue;
				IL_356:
				A_2 = num5;
				num = 13;
				continue;
				IL_450:
				num6 = A_2;
				num7 = A_2 + num4;
				num = 14;
			}
			IL_BE:
			throw new ArgumentNullException(RecordTableEnumerator.b("主嬽ℿ♁⅃㑅", a_));
			IL_291:
			goto IL_4EA;
			IL_4E5:
			throw new ArgumentNullException(RecordTableEnumerator.b("伻嘽┿❁ぃ", a_));
			IL_4EA:
			A_0.MoveToElement();
			return A_2 + num4;
		}
		}
	}

	// Token: 0x06003EC8 RID: 16072 RVA: 0x0022F914 File Offset: 0x0022E914
	private int ᜁ(XmlReader A_0, XlsWorksheet A_1, int A_2, int A_3)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			int num = 40;
			int num4;
			for (;;)
			{
				XlsCellRecordCollection cellRecords;
				int num2;
				XmlSerializationCellType xmlSerializationCellType;
				spr\u223A a_2;
				string text;
				int num3;
				string text2;
				string text3;
				switch (num)
				{
				case 0:
					goto IL_3A8;
				case 1:
					num = 21;
					continue;
				case 2:
					goto IL_526;
				case 3:
					goto IL_220;
				case 4:
					goto IL_EE;
				case 5:
					goto IL_526;
				case 6:
					num = 34;
					continue;
				case 7:
					text = this.ᜀ(A_0, cellRecords, num2, out xmlSerializationCellType, out a_2);
					num = 41;
					continue;
				case 8:
					A_0.Read();
					num = 30;
					continue;
				case 9:
					goto IL_5E6;
				case 10:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 32;
						continue;
					}
					num = 28;
					continue;
				case 11:
					if (!A_0.MoveToAttribute(RecordTableEnumerator.b("็╉㹋⍍╏㹑㕓", a_), RecordTableEnumerator.b("㵇㡉≋瑍⍏ㅑ㱓㍕㕗㭙⽛獝ൟୡݣᑥݧᥩͫ࡭ѯ影ᝳ᥵ᕷ䁹፻᡽늇黎ﲋﲍﾙ鍊", a_)))
					{
						num = 23;
						continue;
					}
					num = 37;
					continue;
				case 12:
					if (A_0.NamespaceURI == RecordTableEnumerator.b("㵇㡉≋瑍⍏ㅑ㱓㍕㕗㭙⽛獝ൟୡݣᑥݧᥩͫ࡭ѯ影ᝳ᥵ᕷ䁹፻᡽늇黎ﲋﲍﾙ鍊", a_))
					{
						num = 36;
						continue;
					}
					goto IL_220;
				case 13:
					num3 = XmlConvert.ToInt32(A_0.Value);
					goto IL_3AD;
				case 14:
					this.ᜀ(A_1, A_2, A_3, text2, num2, text, xmlSerializationCellType);
					num = 22;
					continue;
				case 15:
					if (text != null)
					{
						num = 27;
						continue;
					}
					cellRecords.SetBlank(A_2, A_3, num2);
					num = 35;
					continue;
				case 16:
					text3 = null;
					goto IL_42D;
				case 17:
					num = 12;
					continue;
				case 18:
					num2 = (A_1.GetDefaultRowStyle(A_2) as AddtionalFormatWrapper).ExtendedFormatIndex;
					num = 2;
					continue;
				case 19:
				{
					spr\u216E spr_u216E;
					if (spr_u216E != null)
					{
						num = 29;
						continue;
					}
					goto IL_526;
				}
				case 20:
					goto IL_526;
				case 21:
					num3 = ++A_3;
					goto IL_3AD;
				case 22:
					goto IL_186;
				case 23:
					num = 16;
					continue;
				case 24:
					if (text2 != null)
					{
						num = 14;
						continue;
					}
					num = 15;
					continue;
				case 25:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_386;
					default:
					{
						if (false)
						{
						}
						int extendedFormatIndex;
						if (extendedFormatIndex != num2)
						{
							num = 18;
							continue;
						}
						num = 19;
						continue;
					}
					}
					break;
				case 26:
					num2 = this.ᜀ(A_1, A_0.Value);
					num = 20;
					continue;
				case 27:
					this.ᜀ(xmlSerializationCellType, text, cellRecords, A_2, A_3, num2, a_2);
					num = 0;
					continue;
				case 28:
					if (A_0.LocalName == RecordTableEnumerator.b("ే⭉㡋⽍", a_))
					{
						num = 6;
						continue;
					}
					goto IL_4B3;
				case 29:
				{
					spr\u216E spr_u216E;
					num2 = (int)spr_u216E.ᜌ();
					num = 5;
					continue;
				}
				case 30:
					goto IL_361;
				case 31:
				{
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("ᭇ㹉㕋≍㕏᭑ၓ", a_), RecordTableEnumerator.b("㵇㡉≋瑍⍏ㅑ㱓㍕㕗㭙⽛獝ൟୡݣᑥݧᥩͫ࡭ѯ影ᝳ᥵ᕷ䁹፻᡽늇黎ﲋﲍﾙ鍊", a_)))
					{
						num = 26;
						continue;
					}
					if (true)
					{
					}
					spr\u216E spr_u216E = A_1.ColumnInformation[A_3];
					int extendedFormatIndex = (A_1.GetDefaultRowStyle(A_2) as AddtionalFormatWrapper).ExtendedFormatIndex;
					num = 25;
					continue;
				}
				case 32:
					goto IL_386;
				case 33:
					if (A_0.LocalName == RecordTableEnumerator.b("େ╉⅋⍍㕏㱑⁓", a_))
					{
						num = 17;
						continue;
					}
					goto IL_220;
				case 34:
					if (A_0.NamespaceURI == RecordTableEnumerator.b("㵇㡉≋瑍⍏ㅑ㱓㍕㕗㭙⽛獝ൟୡݣᑥݧᥩͫ࡭ѯ影ᝳ᥵ᕷ䁹፻᡽늇黎ﲋﲍﾙ鍊", a_))
					{
						num = 7;
						continue;
					}
					goto IL_4B3;
				case 35:
					goto IL_5C6;
				case 36:
				{
					XlsComment a_3 = (XlsComment)A_1.InnerComments.AddComment(A_2, A_3);
					this.ᜀ(A_0, a_3, num2);
					num = 3;
					continue;
				}
				case 37:
					text3 = A_0.Value;
					goto IL_42D;
				case 38:
					if (!A_0.MoveToAttribute(RecordTableEnumerator.b("Ň⑉⡋⭍⡏", a_), RecordTableEnumerator.b("㵇㡉≋瑍⍏ㅑ㱓㍕㕗㭙⽛獝ൟୡݣᑥݧᥩͫ࡭ѯ影ᝳ᥵ᕷ䁹፻᡽늇黎ﲋﲍﾙ鍊", a_)))
					{
						num = 1;
						continue;
					}
					num = 13;
					continue;
				case 39:
					if (!A_0.IsEmptyElement)
					{
						num = 8;
						continue;
					}
					goto IL_302;
				case 41:
					goto IL_4B3;
				case 42:
					if (A_1 == null)
					{
						num = 9;
						continue;
					}
					num = 38;
					continue;
				case 43:
					goto IL_361;
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				num = 42;
				continue;
				IL_220:
				A_0.Skip();
				num = 43;
				continue;
				IL_302:
				num = 24;
				continue;
				IL_386:
				goto IL_302;
				IL_361:
				num = 10;
				continue;
				IL_3AD:
				A_3 = num3;
				num2 = A_1.ParentWorkbook.DefaultXFIndex;
				cellRecords = A_1.CellRecords;
				num = 31;
				continue;
				IL_42D:
				text2 = text3;
				this.ᜀ(A_0, A_1, A_2, A_3);
				A_0.MoveToElement();
				xmlSerializationCellType = XmlSerializationCellType.Number;
				a_2 = null;
				text = null;
				num = 39;
				continue;
				IL_4B3:
				num = 33;
				continue;
				IL_526:
				num4 = this.ᜀ(A_0, A_1, A_2 - 1, A_3 - 1, num2);
				num = 11;
			}
			IL_EE:
			throw new ArgumentNullException(RecordTableEnumerator.b("㩇⽉ⵋ⩍㕏⁑", a_));
			IL_186:
			IL_3A8:
			IL_5C6:
			goto IL_5EB;
			IL_5E6:
			throw new ArgumentNullException(RecordTableEnumerator.b("㭇≉⥋⭍⑏", a_));
			IL_5EB:
			return A_3 + num4;
		}
		}
	}

	// Token: 0x06003EC9 RID: 16073 RVA: 0x0022FF14 File Offset: 0x0022EF14
	private void ᜀ(XmlReader A_0, XlsWorksheet A_1, int A_2, int A_3)
	{
		int a_ = 0;
		switch (0)
		{
		default:
			for (;;)
			{
				int num = 26;
				for (;;)
				{
					string text;
					string text2;
					XlsHyperLink xlsHyperLink;
					string text4;
					switch (num)
					{
					case 0:
						text = A_0.Value;
						goto IL_213;
					case 1:
						goto IL_28C;
					case 2:
						if (text2.StartsWith(RecordTableEnumerator.b("樵搷", a_)))
						{
							num = 7;
							continue;
						}
						num = 10;
						continue;
					case 3:
						goto IL_36B;
					case 4:
						num = 11;
						continue;
					case 5:
						goto IL_28C;
					case 6:
					{
						string text3;
						if (!FormulaUtil.IsCell3D(text2, false, out text3, out text3, out text3))
						{
							num = 24;
							continue;
						}
						goto IL_36B;
					}
					case 7:
						xlsHyperLink.Type = HyperLinkType.Unc;
						num = 8;
						continue;
					case 8:
						goto IL_28C;
					case 9:
						if (!A_0.MoveToAttribute(RecordTableEnumerator.b("縵樷弹娻洽⌿ぁ⅃⍅♇ṉ╋㹍", a_), RecordTableEnumerator.b("䌵䨷吹ػ䴽⌿⩁⅃⭅⥇㥉態⍍㥏ㅑ♓㥕⭗㕙㩛⩝䵟šୣ୥剧թ੫࡭᥯ᅱᅳ䱵ᵷɹύ᭽", a_)))
						{
							num = 4;
							continue;
						}
						num = 22;
						continue;
					case 10:
						if (!text2.StartsWith(RecordTableEnumerator.b("嬵夷匹倻䨽⼿", a_)))
						{
							num = 20;
							continue;
						}
						goto IL_114;
					case 11:
						text4 = null;
						goto IL_311;
					case 12:
					{
						IXLSRange range = A_1[A_2, A_3];
						xlsHyperLink = ((A_1.HyperLinks as XlsHyperLinksCollection).Add(range) as XlsHyperLink);
						num = 6;
						continue;
					}
					case 13:
						num = 15;
						continue;
					case 14:
						goto IL_114;
					case 15:
						text = null;
						goto IL_213;
					case 16:
						if (text2 != null)
						{
							num = 12;
							continue;
						}
						return;
					case 17:
						goto IL_28C;
					case 18:
						goto IL_2A7;
					case 19:
						if (text2.IndexOf(RecordTableEnumerator.b("వ᜷ᔹ", a_)) != -1)
						{
							num = 14;
							continue;
						}
						xlsHyperLink.Type = HyperLinkType.File;
						num = 1;
						continue;
					case 20:
						num = 19;
						continue;
					case 21:
					{
						string text3;
						if (this.\u171B.IsCellRange3D(text2, false, out text3, out text3, out text3, out text3, out text3))
						{
							num = 3;
							continue;
						}
						num = 2;
						continue;
					}
					case 22:
						text4 = A_0.Value;
						goto IL_311;
					case 23:
						if (true)
						{
						}
						if (!A_0.MoveToAttribute(RecordTableEnumerator.b("縵樷弹娻", a_), RecordTableEnumerator.b("䌵䨷吹ػ䴽⌿⩁⅃⭅⥇㥉態⍍㥏ㅑ♓㥕⭗㕙㩛⩝䵟šୣ୥剧թ੫࡭᥯ᅱᅳ䱵୷੹๻᭽", a_)))
						{
							num = 13;
							continue;
						}
						num = 0;
						continue;
					case 24:
						num = 21;
						continue;
					case 25:
						goto IL_A7;
					}
					if (A_1 == null)
					{
						num = 25;
						continue;
					}
					num = 23;
					continue;
					IL_114:
					xlsHyperLink.Type = HyperLinkType.Url;
					num = 5;
					continue;
					IL_213:
					text2 = text;
					num = 9;
					continue;
					IL_28C:
					xlsHyperLink.SetAddress(text2, false);
					string screenTip;
					xlsHyperLink.ScreenTip = screenTip;
					num = 18;
					continue;
					IL_311:
					screenTip = text4;
					num = 16;
					continue;
					IL_36B:
					xlsHyperLink.Type = HyperLinkType.Workbook;
					num = 17;
				}
				IL_A7:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_2D6;
				}
			}
			IL_2A7:
			return;
			IL_2D6:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䔵倷弹夻䨽", a_));
		}
	}

	// Token: 0x06003ECA RID: 16074 RVA: 0x002302B0 File Offset: 0x0022F2B0
	private string ᜀ(XmlReader A_0, XlsCellRecordCollection A_1, int A_2, out XmlSerializationCellType A_3, out spr\u223A A_4)
	{
		int a_ = 18;
		int num = 4;
		string result;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				string value = A_0.Value;
				num = 6;
				continue;
			}
			case 1:
				if (!A_0.IsEmptyElement)
				{
					num = 3;
					continue;
				}
				return result;
			case 2:
				goto IL_51;
			case 3:
				result = this.ᜀ(A_0, A_2, A_4);
				goto IL_C4;
			case 5:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("᱇㍉㱋⭍", a_), RecordTableEnumerator.b("㵇㡉≋瑍⍏ㅑ㱓㍕㕗㭙⽛獝ൟୡݣᑥݧᥩͫ࡭ѯ影ᝳ᥵ᕷ䁹፻᡽늇黎ﲋﲍﾙ鍊", a_)))
				{
					num = 0;
					continue;
				}
				goto IL_165;
			case 6:
			{
				string value;
				A_3 = (XmlSerializationCellType)Enum.Parse(typeof(XmlSerializationCellType), value, true);
				A_0.MoveToElement();
				num = 1;
				continue;
			}
			case 7:
				goto IL_D7;
			}
			if (A_0 == null)
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
			{
				if (false)
				{
				}
				result = null;
				XlsFontsCollection innerFonts = this.\u1718.InnerFonts;
				int a_2 = this.\u1718.InnerExtFormats.ᜁ(A_2).\u173B();
				A_4 = new spr\u223A(a_2);
				num = 5;
				continue;
			}
			}
			IL_C4:
			if (true)
			{
			}
			num = 7;
		}
		IL_51:
		throw new ArgumentNullException(RecordTableEnumerator.b("㩇⽉ⵋ⩍㕏⁑", a_));
		IL_D7:
		return result;
		IL_165:
		throw new spr\u23EE(RecordTableEnumerator.b("㱇⭉⹋≍㕏", a_), RecordTableEnumerator.b("େ⽉⁋≍灏♑ⵓ♕㵗穙㽛㽝๟䉡੣॥ᱧ䩩๫୭偯ᑱ᭳͵ᙷṹ剻", a_));
	}

	// Token: 0x06003ECB RID: 16075 RVA: 0x00230448 File Offset: 0x0022F448
	private int ᜀ(XmlReader A_0, XlsWorksheet A_1, int A_2, int A_3, int A_4)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			int num = 7;
			for (;;)
			{
				int num2;
				int num3;
				int num4;
				XlsCellRecordCollection cellRecords;
				switch (num)
				{
				case 0:
					if (num2 == 0)
					{
						num = 6;
						continue;
					}
					goto IL_D3;
				case 1:
					goto IL_289;
				case 2:
					goto IL_17F;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_10B;
					default:
						if (false)
						{
						}
						goto IL_17F;
					}
					break;
				case 4:
					goto IL_8C;
				case 5:
					goto IL_223;
				case 6:
					num = 12;
					continue;
				case 8:
					num3++;
					num = 16;
					continue;
				case 9:
					return num2;
				case 10:
					goto IL_10B;
				case 11:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("琸帺似堾⑀ɂ♄㕆♈㡊㹌", a_), RecordTableEnumerator.b("䰸䤺匼Ծ㉀⁂ⵄ≆⑈⩊㹌扎㱐㩒㙔╖㙘⡚㉜㥞ᕠ乢٤ࡦѨ兪ɬ८ᝰᩲᙴቶ䍸ࡺർൾﮎ", a_)))
					{
						num = 10;
						continue;
					}
					goto IL_1E5;
				case 12:
					if (num4 != 0)
					{
						num = 13;
						continue;
					}
					return num2;
				case 13:
					goto IL_D3;
				case 14:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("琸帺似堾⑀݂⩄う❈", a_), RecordTableEnumerator.b("䰸䤺匼Ծ㉀⁂ⵄ≆⑈⩊㹌扎㱐㩒㙔╖㙘⡚㉜㥞ᕠ乢٤ࡦѨ兪ɬ८ᝰᩲᙴቶ䍸ࡺർൾﮎ", a_)))
					{
						num = 17;
						continue;
					}
					goto IL_289;
				case 15:
					cellRecords.Remove(A_2 + 1, A_3 + 1);
					num = 9;
					continue;
				case 16:
					goto IL_223;
				case 17:
					num4 = XmlConvert.ToInt32(A_0.Value);
					num = 1;
					continue;
				case 18:
					goto IL_1E5;
				case 19:
				{
					if (num3 > A_2 + num4)
					{
						num = 15;
						continue;
					}
					int num5 = A_3;
					num = 2;
					continue;
				}
				case 20:
				{
					int num5;
					if (num5 > A_3 + num2)
					{
						num = 8;
						continue;
					}
					cellRecords.SetBlank(num3 + 1, num5 + 1, A_4);
					num5++;
					num = 3;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				num2 = 0;
				num4 = 0;
				num = 11;
				continue;
				IL_D3:
				cellRecords = A_1.CellRecords;
				A_1.MergeCells.ᜀ(A_2, A_2 + num4, A_3, A_3 + num2, MergeOperationType.Leave);
				num3 = A_2;
				if (true)
				{
				}
				num = 5;
				continue;
				IL_10B:
				num2 = XmlConvert.ToInt32(A_0.Value);
				num = 18;
				continue;
				IL_17F:
				num = 20;
				continue;
				IL_1E5:
				num = 14;
				continue;
				IL_223:
				num = 19;
				continue;
				IL_289:
				num = 0;
			}
			IL_8C:
			throw new ArgumentNullException(RecordTableEnumerator.b("䬸帺尼嬾⑀ㅂ", a_));
		}
		}
	}

	// Token: 0x06003ECC RID: 16076 RVA: 0x0023070C File Offset: 0x0022F70C
	private void ᜂ(XmlReader A_0, XlsWorkbook A_1)
	{
		int a_ = 17;
		int num = 14;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				goto IL_129;
			case 2:
				this.ᜁ(A_0, A_1);
				num = 6;
				continue;
			case 3:
				goto IL_7C;
			case 4:
				num = 12;
				continue;
			case 5:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 11;
					continue;
				}
				if (true)
				{
				}
				num = 13;
				continue;
			case 6:
				goto IL_ED;
			case 7:
				goto IL_129;
			case 8:
				if (A_1 == null)
				{
					num = 9;
					continue;
				}
				num = 10;
				continue;
			case 9:
				goto IL_EB;
			case 10:
				if (A_0.IsEmptyElement)
				{
					num = 0;
					continue;
				}
				A_0.Read();
				num = 1;
				continue;
			case 11:
				return;
			case 12:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("㉆㭈╊睌㱎㉐㭒ご㩖㡘⡚灜㉞ࡠbᝤࡦᩨѪ୬᭮屰ၲᩴ᩶䍸ᑺ᭼᥾붆愈ﮊﾌﾖﲘﺚ", a_))
				{
					num = 2;
					continue;
				}
				goto IL_ED;
			case 13:
				if (A_0.LocalName == RecordTableEnumerator.b("ᑆ㵈㉊⅌⩎", a_))
				{
					num = 4;
					continue;
				}
				goto IL_ED;
			}
			if (A_0 != null)
			{
				num = 8;
				continue;
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
				num = 3;
				continue;
			}
			IL_ED:
			A_0.Skip();
			num = 7;
			continue;
			IL_129:
			num = 5;
		}
		IL_7C:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕆ⱈ⩊⥌⩎⍐", a_));
		IL_EB:
		throw new ArgumentNullException(RecordTableEnumerator.b("╆♈⑊♌", a_));
	}

	// Token: 0x06003ECD RID: 16077 RVA: 0x002308E0 File Offset: 0x0022F8E0
	private void ᜁ(XmlReader A_0, XlsWorkbook A_1)
	{
		int a_ = 13;
		switch (0)
		{
		default:
		{
			int num = 47;
			string text;
			spr\u192F spr_u192F;
			string text2;
			sprᢖ sprᢖ;
			for (;;)
			{
				string a_2;
				switch (num)
				{
				case 0:
					if (text != RecordTableEnumerator.b("݂⁄ⅆ⡈㹊⅌㭎", a_))
					{
						num = 21;
						continue;
					}
					goto IL_78B;
				case 1:
					a_2 = A_0.Value;
					num = 34;
					continue;
				case 2:
					goto IL_668;
				case 3:
					goto IL_342;
				case 4:
					goto IL_718;
				case 5:
					this.ᜄ(A_0, spr_u192F);
					num = 4;
					continue;
				case 6:
					goto IL_6A0;
				case 7:
					num = 10;
					continue;
				case 8:
					if (A_0.LocalName == RecordTableEnumerator.b("ፂ㝄⡆㵈⹊⹌㭎㡐㱒㭔", a_))
					{
						num = 7;
						continue;
					}
					goto IL_1C4;
				case 9:
					if (true)
					{
					}
					if (A_0.NamespaceURI == RecordTableEnumerator.b("㙂㝄⥆獈㡊⹌❎㑐㹒㑔⑖瑘㙚㑜㱞፠ౢᙤࡦཨὪ䁬౮ṰṲ佴ᡶὸᵺᑼ᱾릂ﮈﮒ", a_))
					{
						num = 54;
						continue;
					}
					goto IL_750;
				case 10:
					if (A_0.NamespaceURI == RecordTableEnumerator.b("㙂㝄⥆獈㡊⹌❎㑐㹒㑔⑖瑘㙚㑜㱞፠ౢᙤࡦཨὪ䁬౮ṰṲ佴ᡶὸᵺᑼ᱾릂ﮈﮒ", a_))
					{
						num = 51;
						continue;
					}
					goto IL_1C4;
				case 11:
					if (A_0.IsEmptyElement)
					{
						num = 33;
						continue;
					}
					A_0.Read();
					num = 16;
					continue;
				case 12:
					text2 = A_0.Value;
					num = 43;
					continue;
				case 13:
					num = 20;
					continue;
				case 14:
					spr_u192F.ᜀ(sprỶ.TXFType.XF_STYLE);
					goto IL_38B;
				case 15:
					goto IL_62D;
				case 16:
					if (text2 != null)
					{
						num = 45;
						continue;
					}
					goto IL_231;
				case 17:
					num = 48;
					continue;
				case 18:
					num = 41;
					continue;
				case 19:
					goto IL_231;
				case 20:
					if (A_0.NamespaceURI == RecordTableEnumerator.b("㙂㝄⥆獈㡊⹌❎㑐㹒㑔⑖瑘㙚㑜㱞፠ౢᙤࡦཨὪ䁬౮ṰṲ佴ᡶὸᵺᑼ᱾릂ﮈﮒ", a_))
					{
						num = 28;
						continue;
					}
					goto IL_668;
				case 21:
					goto IL_6FF;
				case 22:
					num = 0;
					continue;
				case 23:
					if (A_0.NamespaceURI == RecordTableEnumerator.b("㙂㝄⥆獈㡊⹌❎㑐㹒㑔⑖瑘㙚㑜㱞፠ౢᙤࡦཨὪ䁬౮ṰṲ佴ᡶὸᵺᑼ᱾릂ﮈﮒ", a_))
					{
						num = 5;
						continue;
					}
					goto IL_718;
				case 24:
					goto IL_231;
				case 25:
					if (A_1 == null)
					{
						num = 36;
						continue;
					}
					sprᢖ = A_1.InnerExtFormats;
					text = null;
					text2 = null;
					a_2 = null;
					num = 40;
					continue;
				case 26:
					if (text == RecordTableEnumerator.b("݂⁄ⅆ⡈㹊⅌㭎", a_))
					{
						num = 14;
						continue;
					}
					goto IL_4A9;
				case 27:
					num = 23;
					continue;
				case 28:
					this.ᜆ(A_0, spr_u192F);
					num = 2;
					continue;
				case 29:
					if (A_0.LocalName == RecordTableEnumerator.b("ɂ⥄⹆⹈╊⁌⩎㽐❒", a_))
					{
						num = 13;
						continue;
					}
					goto IL_668;
				case 30:
					goto IL_11E;
				case 31:
					this.ᜅ(A_0, spr_u192F);
					num = 15;
					continue;
				case 32:
					goto IL_4A9;
				case 33:
					goto IL_4EC;
				case 34:
					goto IL_180;
				case 35:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 22;
						continue;
					}
					num = 29;
					continue;
				case 36:
					goto IL_628;
				case 37:
					text = A_0.Value;
					num = 3;
					continue;
				case 38:
					if (A_0.LocalName == RecordTableEnumerator.b("Ղ⩄⥆㵈", a_))
					{
						num = 18;
						continue;
					}
					goto IL_62D;
				case 39:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("ൂ⑄⩆ⱈ", a_), RecordTableEnumerator.b("㙂㝄⥆獈㡊⹌❎㑐㹒㑔⑖瑘㙚㑜㱞፠ౢᙤࡦཨὪ䁬౮ṰṲ佴ᡶὸᵺᑼ᱾릂ﮈﮒ", a_)))
					{
						num = 12;
						continue;
					}
					goto IL_4EE;
				case 40:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("ੂń", a_), RecordTableEnumerator.b("㙂㝄⥆獈㡊⹌❎㑐㹒㑔⑖瑘㙚㑜㱞፠ౢᙤࡦཨὪ䁬౮ṰṲ佴ᡶὸᵺᑼ᱾릂ﮈﮒ", a_)))
					{
						num = 37;
						continue;
					}
					goto IL_342;
				case 41:
					if (A_0.NamespaceURI == RecordTableEnumerator.b("㙂㝄⥆獈㡊⹌❎㑐㹒㑔⑖瑘㙚㑜㱞፠ౢᙤࡦཨὪ䁬౮ṰṲ佴ᡶὸᵺᑼ᱾릂ﮈﮒ", a_))
					{
						num = 31;
						continue;
					}
					goto IL_62D;
				case 42:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("ፂ⑄㕆ⱈ╊㥌", a_), RecordTableEnumerator.b("㙂㝄⥆獈㡊⹌❎㑐㹒㑔⑖瑘㙚㑜㱞፠ౢᙤࡦཨὪ䁬౮ṰṲ佴ᡶὸᵺᑼ᱾릂ﮈﮒ", a_)))
					{
						num = 1;
						continue;
					}
					goto IL_180;
				case 43:
					goto IL_4EE;
				case 44:
					if (A_0.LocalName == RecordTableEnumerator.b("ੂ⭄㍆ⱈ㥊⑌⁎⍐", a_))
					{
						num = 27;
						continue;
					}
					goto IL_718;
				case 45:
					spr_u192F.ᜈ(true);
					spr_u192F.ᜉ(true);
					spr_u192F.\u170D(true);
					spr_u192F.ᜃ(true);
					spr_u192F.ᜋ(true);
					spr_u192F.ᜊ(true);
					num = 24;
					continue;
				case 46:
					if (A_0.LocalName == RecordTableEnumerator.b("ൂい⩆⭈⹊㽌ॎ㹐⅒㡔㙖ⵘ", a_))
					{
						num = 55;
						continue;
					}
					goto IL_750;
				case 48:
					if (A_0.NamespaceURI == RecordTableEnumerator.b("㙂㝄⥆獈㡊⹌❎㑐㹒㑔⑖瑘㙚㑜㱞፠ౢᙤࡦཨὪ䁬౮ṰṲ佴ᡶὸᵺᑼ᱾릂ﮈﮒ", a_))
					{
						num = 53;
						continue;
					}
					goto IL_6A0;
				case 49:
					goto IL_750;
				case 50:
					if (A_0.LocalName == RecordTableEnumerator.b("ł⩄㕆ⵈ⹊㽌㱎", a_))
					{
						num = 17;
						continue;
					}
					goto IL_6A0;
				case 51:
					this.ᜂ(A_0, spr_u192F);
					num = 52;
					continue;
				case 52:
					goto IL_1C4;
				case 53:
					this.ᜁ(A_0, spr_u192F);
					num = 6;
					continue;
				case 54:
					this.ᜃ(A_0, spr_u192F);
					num = 49;
					continue;
				case 55:
					num = 9;
					continue;
				}
				if (A_0 == null)
				{
					num = 30;
					continue;
				}
				num = 25;
				continue;
				IL_180:
				num = 39;
				continue;
				IL_1C4:
				num = 50;
				continue;
				IL_231:
				num = 35;
				continue;
				IL_342:
				num = 42;
				continue;
				IL_38B:
				num = 32;
				continue;
				IL_4A9:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_38B;
				default:
					if (false)
					{
					}
					A_0.MoveToElement();
					num = 11;
					continue;
				}
				IL_4EE:
				spr_u192F = this.ᜀ(sprᢖ, text, text2, a_2);
				num = 26;
				continue;
				IL_62D:
				num = 44;
				continue;
				IL_668:
				num = 38;
				continue;
				IL_6A0:
				A_0.Skip();
				num = 19;
				continue;
				IL_718:
				num = 46;
				continue;
				IL_750:
				num = 8;
			}
			IL_11E:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄♆ⵈ⹊㽌", a_));
			IL_4EC:
			this.ᜀ(A_1, sprᢖ, spr_u192F, text, text2);
			return;
			IL_628:
			throw new ArgumentNullException(RecordTableEnumerator.b("⅂⩄⡆≈", a_));
			IL_6FF:
			this.ᜀ(A_1, sprᢖ, spr_u192F, text, text2);
			return;
			IL_78B:
			spr_u192F.ᜀ(sprỶ.TXFType.XF_CELL);
			return;
		}
		}
	}

	// Token: 0x06003ECE RID: 16078 RVA: 0x00231080 File Offset: 0x00230080
	private void ᜆ(XmlReader A_0, spr\u192F A_1)
	{
		int a_ = 8;
		int num = 7;
		for (;;)
		{
			switch (num)
			{
			case 0:
				A_1.ᜁ(XmlConvert.ToInt32(A_0.Value));
				num = 18;
				continue;
			case 1:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("氽⼿㙁╃㉅ⵇ", a_), RecordTableEnumerator.b("䬽㈿ⱁ繃㕅⭇≉⥋⍍ㅏ⅑祓㭕ㅗ㥙⹛ㅝ፟ൡɣብ䕧३ͫͭ䩯ᵱታၵᅷ᥹᥻䑽ﾋ", a_)))
				{
					num = 22;
					continue;
				}
				goto IL_2E0;
			case 2:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("洽⠿ぁⵃ⡅⍇ṉ⍋ࡍ㥏♑", a_), RecordTableEnumerator.b("䬽㈿ⱁ繃㕅⭇≉⥋⍍ㅏ⅑祓㭕ㅗ㥙⹛ㅝ፟ൡɣብ䕧३ͫͭ䩯ᵱታၵᅷ᥹᥻䑽ﾋ", a_)))
				{
					num = 10;
					continue;
				}
				goto IL_C6;
			case 3:
				A_1.ᜅ(255);
				num = 27;
				continue;
			case 4:
				if (A_0.AttributeCount == 0)
				{
					num = 34;
					continue;
				}
				num = 1;
				continue;
			case 5:
				return;
			case 6:
				goto IL_4E4;
			case 8:
				A_1.ᜈ(false);
				num = 30;
				continue;
			case 9:
				if (XmlConvert.ToBoolean(A_0.Value))
				{
					num = 3;
					continue;
				}
				goto IL_35A;
			case 10:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1A2;
				default:
					if (false)
					{
					}
					A_1.ᜇ(XmlConvert.ToBoolean(A_0.Value));
					num = 20;
					continue;
				}
				break;
			case 11:
				num = 9;
				continue;
			case 12:
				goto IL_2E0;
			case 13:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("瘽⼿ぁⵃ㱅❇⑉㡋⽍㱏", a_), RecordTableEnumerator.b("䬽㈿ⱁ繃㕅⭇≉⥋⍍ㅏ⅑祓㭕ㅗ㥙⹛ㅝ፟ൡɣብ䕧३ͫͭ䩯ᵱታၵᅷ᥹᥻䑽ﾋ", a_)))
				{
					num = 21;
					continue;
				}
				goto IL_287;
			case 14:
				A_1.ᜅ(XmlConvert.ToBoolean(A_0.Value));
				num = 23;
				continue;
			case 15:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("眽⸿♁⅃⡅㱇", a_), RecordTableEnumerator.b("䬽㈿ⱁ繃㕅⭇≉⥋⍍ㅏ⅑祓㭕ㅗ㥙⹛ㅝ፟ൡɣብ䕧३ͫͭ䩯ᵱታၵᅷ᥹᥻䑽ﾋ", a_)))
				{
					num = 0;
					continue;
				}
				goto IL_108;
			case 16:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("栽┿ぁぃ⽅⭇⭉⁋ᩍ㕏⩑⁓", a_), RecordTableEnumerator.b("䬽㈿ⱁ繃㕅⭇≉⥋⍍ㅏ⅑祓㭕ㅗ㥙⹛ㅝ፟ൡɣብ䕧३ͫͭ䩯ᵱታၵᅷ᥹᥻䑽ﾋ", a_)))
				{
					num = 11;
					continue;
				}
				goto IL_35A;
			case 17:
				if (true)
				{
				}
				goto IL_49B;
			case 18:
				goto IL_108;
			case 19:
				goto IL_C1;
			case 20:
				goto IL_C6;
			case 21:
				A_1.ᜀ(spr\u247E.\u171C[A_0.Value]);
				goto IL_1A2;
			case 22:
			{
				double num2 = 0.0;
				double.TryParse(A_0.Value, out num2);
				num = 32;
				continue;
			}
			case 23:
				goto IL_3D2;
			case 24:
				goto IL_287;
			case 25:
				if (A_1.\u171E() == sprỶ.TXFType.XF_CELL)
				{
					num = 8;
					continue;
				}
				return;
			case 26:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("栽┿ぁぃ⽅⭇⭉⁋", a_), RecordTableEnumerator.b("䬽㈿ⱁ繃㕅⭇≉⥋⍍ㅏ⅑祓㭕ㅗ㥙⹛ㅝ፟ൡɣብ䕧३ͫͭ䩯ᵱታၵᅷ᥹᥻䑽ﾋ", a_)))
				{
					num = 33;
					continue;
				}
				goto IL_49B;
			case 27:
				goto IL_35A;
			case 28:
				goto IL_414;
			case 29:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("氽┿⍁⁃⽅♇ⵉ͋㱍㑏㝑♓", a_), RecordTableEnumerator.b("䬽㈿ⱁ繃㕅⭇≉⥋⍍ㅏ⅑祓㭕ㅗ㥙⹛ㅝ፟ൡɣብ䕧३ͫͭ䩯ᵱታၵᅷ᥹᥻䑽ﾋ", a_)))
				{
					num = 31;
					continue;
				}
				goto IL_414;
			case 30:
				goto IL_2DB;
			case 31:
				A_1.ᜀ((ReadingOrderType)Enum.Parse(typeof(ReadingOrderType), A_0.Value, true));
				num = 28;
				continue;
			case 32:
			{
				double num2;
				A_1.ᜅ((num2 < 0.0) ? ((int)(90.0 - num2)) : ((int)num2));
				num = 12;
				continue;
			}
			case 33:
				A_1.ᜀ(spr\u247E.\u171D[A_0.Value]);
				num = 17;
				continue;
			case 34:
				num = 5;
				continue;
			case 35:
				if (A_1 == null)
				{
					num = 6;
					continue;
				}
				num = 4;
				continue;
			case 36:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("椽㈿⍁㑃ቅⵇ㉉㡋", a_), RecordTableEnumerator.b("䬽㈿ⱁ繃㕅⭇≉⥋⍍ㅏ⅑祓㭕ㅗ㥙⹛ㅝ፟ൡɣብ䕧३ͫͭ䩯ᵱታၵᅷ᥹᥻䑽ﾋ", a_)))
				{
					num = 14;
					continue;
				}
				goto IL_3D2;
			}
			if (A_0 == null)
			{
				num = 19;
				continue;
			}
			num = 35;
			continue;
			IL_C6:
			num = 16;
			continue;
			IL_108:
			num = 29;
			continue;
			IL_1A2:
			num = 24;
			continue;
			IL_287:
			num = 26;
			continue;
			IL_2E0:
			num = 36;
			continue;
			IL_35A:
			num = 13;
			continue;
			IL_3D2:
			num = 15;
			continue;
			IL_414:
			num = 2;
			continue;
			IL_49B:
			A_0.MoveToElement();
			num = 25;
		}
		IL_C1:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰽┿⍁⁃⍅㩇", a_));
		IL_2DB:
		return;
		IL_4E4:
		throw new ArgumentNullException(RecordTableEnumerator.b("堽⼿ぁ⥃❅㱇", a_));
	}

	// Token: 0x06003ECF RID: 16079 RVA: 0x002315D0 File Offset: 0x002305D0
	private void ᜅ(XmlReader A_0, spr\u192F A_1)
	{
		int a_ = 2;
		int num = 39;
		for (;;)
		{
			IFont font;
			switch (num)
			{
			case 0:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("笷唹倻儽㈿", a_), RecordTableEnumerator.b("䴷䠹刻н㌿⅁ⱃ⍅╇⭉㽋捍㵏㭑㝓⑕㝗⥙㍛㡝ᑟ佡ݣ॥է偩ͫ࡭ᙯ᭱ᝳ፵䉷ॹ౻౽揄", a_)))
				{
					num = 41;
					continue;
				}
				goto IL_3AA;
			case 1:
				goto IL_3AA;
			case 2:
				if (A_0.AttributeCount == 0)
				{
					num = 25;
					continue;
				}
				font = new XlsFont(base.ReservedHandle, A_1.ᜎ().InnerFonts);
				num = 29;
				continue;
			case 3:
				if (A_1.ᜠ() == 0)
				{
					num = 4;
					continue;
				}
				font = A_1.ᜎ().InnerFonts.Add(font);
				A_1.ᜂ(((IInternalFont)font).Index);
				num = 31;
				continue;
			case 4:
			{
				XlsFont xlsFont = (XlsFont)font;
				xlsFont.CopyTo((XlsFont)A_1.ᜎ().InnerFonts[0]);
				A_1.ᜂ(((IInternalFont)A_1.ᜎ().InnerFonts[0]).Index);
				num = 21;
				continue;
			}
			case 5:
				goto IL_5A4;
			case 6:
				goto IL_2F5;
			case 7:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("欷丹主圽⬿❁၃⹅㩇╉㥋⥍㡏", a_), RecordTableEnumerator.b("䴷䠹刻н㌿⅁ⱃ⍅╇⭉㽋捍㵏㭑㝓⑕㝗⥙㍛㡝ᑟ佡ݣ॥է偩ͫ࡭ᙯ᭱ᝳ፵䉷ॹ౻౽揄", a_)))
				{
					num = 23;
					continue;
				}
				goto IL_5A4;
			case 8:
				goto IL_3B5;
			case 9:
				A_1.ᜉ(false);
				num = 6;
				continue;
			case 10:
				font.Size = (double)((int)XmlConvert.ToDouble(A_0.Value));
				num = 16;
				continue;
			case 11:
				goto IL_3EC;
			case 12:
				font.Underline = (FontUnderlineType)Enum.Parse(typeof(FontUnderlineType), A_0.Value, true);
				num = 13;
				continue;
			case 13:
				goto IL_4DB;
			case 14:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3B5;
				default:
					goto IL_622;
				}
				break;
			case 15:
				font.FontName = A_0.Value;
				num = 24;
				continue;
			case 16:
				goto IL_18E;
			case 17:
				goto IL_62D;
			case 18:
				goto IL_2FA;
			case 19:
				((XlsFont)font).MacOSShadow = XmlConvert.ToBoolean(A_0.Value);
				num = 17;
				continue;
			case 20:
				font = this.ᜀ(font, A_0.Value);
				num = 11;
				continue;
			case 21:
				goto IL_66F;
			case 22:
				font.IsBold = XmlConvert.ToBoolean(A_0.Value);
				num = 33;
				continue;
			case 23:
				font.IsStrikethrough = XmlConvert.ToBoolean(A_0.Value);
				num = 5;
				continue;
			case 24:
				goto IL_2FA;
			case 25:
				return;
			case 26:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("欷匹䘻嬽", a_), RecordTableEnumerator.b("䴷䠹刻н㌿⅁ⱃ⍅╇⭉㽋捍㵏㭑㝓⑕㝗⥙㍛㡝ᑟ佡ݣ॥է偩ͫ࡭ᙯ᭱ᝳ፵䉷ॹ౻౽揄", a_)))
				{
					num = 10;
					continue;
				}
				goto IL_18E;
			case 27:
				if (A_1.\u171E() == sprỶ.TXFType.XF_CELL)
				{
					num = 9;
					continue;
				}
				return;
			case 28:
				((XlsFont)font).MacOSOutlineFont = XmlConvert.ToBoolean(A_0.Value);
				num = 36;
				continue;
			case 29:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("稷唹倻娽", a_), RecordTableEnumerator.b("䴷䠹刻н㌿⅁ⱃ⍅╇⭉㽋捍㵏㭑㝓⑕㝗⥙㍛㡝ᑟ佡ݣ॥է偩ͫ࡭ᙯ᭱ᝳ፵䉷ॹ౻౽揄", a_)))
				{
					num = 22;
					continue;
				}
				goto IL_24D;
			case 30:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("眷伹䠻刽⤿ⱁ⅃", a_), RecordTableEnumerator.b("䴷䠹刻н㌿⅁ⱃ⍅╇⭉㽋捍㵏㭑㝓⑕㝗⥙㍛㡝ᑟ佡ݣ॥է偩ͫ࡭ᙯ᭱ᝳ፵䉷ॹ౻౽揄", a_)))
				{
					num = 28;
					continue;
				}
				goto IL_203;
			case 31:
				goto IL_66F;
			case 32:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("洷吹堻嬽㈿⹁ⵃ⡅ⵇ", a_), RecordTableEnumerator.b("䴷䠹刻н㌿⅁ⱃ⍅╇⭉㽋捍㵏㭑㝓⑕㝗⥙㍛㡝ᑟ佡ݣ॥է偩ͫ࡭ᙯ᭱ᝳ፵䉷ॹ౻౽揄", a_)))
				{
					num = 12;
					continue;
				}
				goto IL_4DB;
			case 33:
				goto IL_24D;
			case 34:
				goto IL_DD;
			case 35:
				if (A_1 == null)
				{
					num = 14;
					continue;
				}
				num = 2;
				continue;
			case 36:
				goto IL_203;
			case 37:
				goto IL_53F;
			case 38:
				font.IsItalic = XmlConvert.ToBoolean(A_0.Value);
				num = 37;
				continue;
			case 40:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("欷刹崻娽⼿㕁", a_), RecordTableEnumerator.b("䴷䠹刻н㌿⅁ⱃ⍅╇⭉㽋捍㵏㭑㝓⑕㝗⥙㍛㡝ᑟ佡ݣ॥է偩ͫ࡭ᙯ᭱ᝳ፵䉷ॹ౻౽揄", a_)))
				{
					if (true)
					{
					}
					num = 19;
					continue;
				}
				goto IL_62D;
			case 41:
				font.Color = this.ᜀ(A_0.Value);
				num = 1;
				continue;
			case 42:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("渷弹主䨽⤿⅁╃⩅े♉╋⥍㹏", a_), RecordTableEnumerator.b("䴷䠹刻н㌿⅁ⱃ⍅╇⭉㽋捍㵏㭑㝓⑕㝗⥙㍛㡝ᑟ佡ݣ॥է偩ͫ࡭ᙯ᭱ᝳ፵䉷ॹ౻౽揄", a_)))
				{
					num = 20;
					continue;
				}
				goto IL_3EC;
			case 43:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("焷丹崻刽⤿⅁", a_), RecordTableEnumerator.b("䴷䠹刻н㌿⅁ⱃ⍅╇⭉㽋捍㵏㭑㝓⑕㝗⥙㍛㡝ᑟ佡ݣ॥է偩ͫ࡭ᙯ᭱ᝳ፵䉷ॹ౻౽揄", a_)))
				{
					num = 38;
					continue;
				}
				goto IL_53F;
			}
			if (A_0 == null)
			{
				num = 34;
				continue;
			}
			num = 35;
			continue;
			IL_18E:
			num = 7;
			continue;
			IL_203:
			num = 40;
			continue;
			IL_24D:
			num = 0;
			continue;
			IL_2FA:
			num = 43;
			continue;
			IL_3AA:
			num = 8;
			continue;
			IL_3B5:
			if (A_0.MoveToAttribute(RecordTableEnumerator.b("縷唹刻䨽฿⍁⥃⍅", a_), RecordTableEnumerator.b("䴷䠹刻н㌿⅁ⱃ⍅╇⭉㽋捍㵏㭑㝓⑕㝗⥙㍛㡝ᑟ佡ݣ॥է偩ͫ࡭ᙯ᭱ᝳ፵䉷ॹ౻౽揄", a_)))
			{
				num = 15;
				continue;
			}
			font.FontName = RecordTableEnumerator.b("礷䠹唻弽ⰿ", a_);
			num = 18;
			continue;
			IL_3EC:
			num = 3;
			continue;
			IL_4DB:
			num = 42;
			continue;
			IL_53F:
			num = 30;
			continue;
			IL_5A4:
			num = 32;
			continue;
			IL_62D:
			num = 26;
			continue;
			IL_66F:
			A_0.MoveToElement();
			num = 27;
		}
		IL_DD:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹崻娽┿ぁ", a_));
		IL_2F5:
		return;
		IL_622:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("帷唹主匽ℿ㙁", a_));
	}

	// Token: 0x06003ED0 RID: 16080 RVA: 0x00231C78 File Offset: 0x00230C78
	private void ᜄ(XmlReader A_0, spr\u192F A_1)
	{
		int a_ = 13;
		int num = 7;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_6F;
			case 1:
				if (A_0.AttributeCount == 0)
				{
					num = 15;
					continue;
				}
				num = 8;
				continue;
			case 2:
				A_1.ᜃ(this.ᜀ(A_0.Value));
				num = 12;
				continue;
			case 3:
				A_1.\u170D(false);
				num = 10;
				continue;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_67;
				default:
					if (false)
					{
					}
					if (A_1.\u171E() == sprỶ.TXFType.XF_CELL)
					{
						num = 3;
						continue;
					}
					return;
				}
				break;
			case 5:
				goto IL_15A;
			case 6:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("ፂ⑄㍆㵈⹊㽌ⅎ", a_), RecordTableEnumerator.b("㙂㝄⥆獈㡊⹌❎㑐㹒㑔⑖瑘㙚㑜㱞፠ౢᙤࡦཨὪ䁬౮ṰṲ佴ᡶὸᵺᑼ᱾릂ﮈﮒ", a_)))
				{
					num = 13;
					continue;
				}
				goto IL_E6;
			case 8:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("B⩄⭆♈㥊", a_), RecordTableEnumerator.b("㙂㝄⥆獈㡊⹌❎㑐㹒㑔⑖瑘㙚㑜㱞፠ౢᙤࡦཨὪ䁬౮ṰṲ佴ᡶὸᵺᑼ᱾릂ﮈﮒ", a_)))
				{
					num = 2;
					continue;
				}
				goto IL_21D;
			case 9:
				goto IL_E6;
			case 10:
				goto IL_193;
			case 11:
				if (A_1 == null)
				{
					num = 5;
					continue;
				}
				num = 1;
				continue;
			case 12:
				goto IL_21D;
			case 13:
				A_1.ᜀ((ExcelPatternType)Array.IndexOf<string>(sprỉ.\u17F0, A_0.Value));
				num = 9;
				continue;
			case 14:
				A_1.ᜂ(this.ᜀ(A_0.Value));
				num = 17;
				continue;
			case 15:
				return;
			case 16:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("ፂ⑄㍆㵈⹊㽌ⅎቐ㱒㥔㡖⭘", a_), RecordTableEnumerator.b("㙂㝄⥆獈㡊⹌❎㑐㹒㑔⑖瑘㙚㑜㱞፠ౢᙤࡦཨὪ䁬౮ṰṲ佴ᡶὸᵺᑼ᱾릂ﮈﮒ", a_)))
				{
					num = 14;
					continue;
				}
				goto IL_74;
			case 17:
				goto IL_74;
			}
			goto IL_61;
			IL_67:
			num = 0;
			continue;
			IL_61:
			if (A_0 == null)
			{
				goto IL_67;
			}
			num = 11;
			continue;
			IL_74:
			num = 6;
			continue;
			IL_E6:
			A_0.MoveToElement();
			if (true)
			{
			}
			num = 4;
			continue;
			IL_21D:
			num = 16;
		}
		IL_6F:
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄♆ⵈ⹊㽌", a_));
		IL_15A:
		throw new ArgumentNullException(RecordTableEnumerator.b("╂⩄㕆⑈⩊㥌", a_));
		IL_193:;
	}

	// Token: 0x06003ED1 RID: 16081 RVA: 0x00231EFC File Offset: 0x00230EFC
	private void ᜃ(XmlReader A_0, spr\u192F A_1)
	{
		int a_ = 14;
		int num = 4;
		for (;;)
		{
			string text;
			switch (num)
			{
			case 0:
				goto IL_60;
			case 1:
				goto IL_13D;
			case 2:
				if (A_1 == null)
				{
					num = 10;
					continue;
				}
				A_1.ᜁ(RecordTableEnumerator.b("̓⍅♇⽉㹋⽍㱏", a_));
				num = 9;
				continue;
			case 3:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("Ƀ⥅㩇❉ⵋ㩍", a_), RecordTableEnumerator.b("ㅃ㑅♇灉㽋ⵍ㡏㝑㥓㝕⭗睙ㅛ㝝͟ၡୣᕥݧ౩ᡫ䍭፯ᵱᥳ䱵᝷ᱹ᩻᝽뺃ﲓﶗ", a_)))
				{
					num = 7;
					continue;
				}
				goto IL_D8;
			case 5:
				if (spr\u247E.\u171E.ContainsKey(text))
				{
					num = 11;
					continue;
				}
				goto IL_117;
			case 6:
				if (true)
				{
				}
				goto IL_12B;
			case 7:
				text = A_0.Value;
				num = 5;
				continue;
			case 8:
				goto IL_D8;
			case 9:
				if (A_0.AttributeCount != 0)
				{
					num = 3;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_12B;
				default:
					if (false)
					{
					}
					num = 14;
					continue;
				}
				break;
			case 10:
				goto IL_D6;
			case 11:
				text = spr\u247E.\u171E[text];
				num = 12;
				continue;
			case 12:
				goto IL_117;
			case 13:
				if (A_1.\u171E() == sprỶ.TXFType.XF_CELL)
				{
					num = 6;
					continue;
				}
				goto IL_1E6;
			case 14:
				return;
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			num = 2;
			continue;
			IL_D8:
			num = 13;
			continue;
			IL_117:
			A_1.ᜁ(text);
			num = 8;
			continue;
			IL_12B:
			A_1.ᜃ(false);
			num = 1;
		}
		IL_60:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙃⍅⥇⹉⥋㱍", a_));
		IL_D6:
		throw new ArgumentNullException(RecordTableEnumerator.b("≃⥅㩇❉ⵋ㩍", a_));
		IL_13D:
		IL_1E6:
		A_0.MoveToElement();
	}

	// Token: 0x06003ED2 RID: 16082 RVA: 0x002320F8 File Offset: 0x002310F8
	private void ᜂ(XmlReader A_0, spr\u192F A_1)
	{
		int a_ = 14;
		int num = 7;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					num = 13;
					continue;
				}
				num = 9;
				continue;
			case 1:
				goto IL_6B;
			case 2:
				A_1.ᜁ(XmlConvert.ToBoolean(A_0.Value));
				num = 4;
				continue;
			case 3:
				goto IL_188;
			case 4:
				goto IL_70;
			case 5:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("ᑃ㑅❇㹉⥋ⵍ⑏㝑こ", a_), RecordTableEnumerator.b("ㅃ㑅♇灉㽋ⵍ㡏㝑㥓㝕⭗睙ㅛ㝝͟ၡୣᕥݧ౩ᡫ䍭፯ᵱᥳ䱵᝷ᱹ᩻᝽뺃ﲓﶗ", a_)))
				{
					num = 2;
					continue;
				}
				goto IL_70;
			case 6:
				goto IL_110;
			case 8:
				return;
			case 9:
				if (A_0.AttributeCount != 0)
				{
					A_1.ᜋ(true);
					num = 5;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_14E;
				default:
					if (false)
					{
					}
					num = 8;
					continue;
				}
				break;
			case 10:
				if (A_1.\u171E() == sprỶ.TXFType.XF_CELL)
				{
					num = 12;
					continue;
				}
				return;
			case 11:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("ృ⽅ⱇ⽉ੋ⅍≏㽑⅓㩕㥗", a_), RecordTableEnumerator.b("ㅃ㑅♇灉㽋ⵍ㡏㝑㥓㝕⭗睙ㅛ㝝͟ၡୣᕥݧ౩ᡫ䍭፯ᵱᥳ䱵᝷ᱹ᩻᝽뺃", a_)))
				{
					num = 14;
					continue;
				}
				goto IL_110;
			case 12:
				A_1.ᜋ(false);
				num = 3;
				continue;
			case 13:
				goto IL_10E;
			case 14:
				goto IL_14E;
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			num = 0;
			continue;
			IL_70:
			num = 11;
			continue;
			IL_110:
			A_0.MoveToElement();
			num = 10;
			continue;
			IL_14E:
			A_1.ᜆ(XmlConvert.ToBoolean(A_0.Value));
			num = 6;
		}
		IL_6B:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙃⍅⥇⹉⥋㱍", a_));
		IL_10E:
		throw new ArgumentNullException(RecordTableEnumerator.b("≃⥅㩇❉ⵋ㩍", a_));
		IL_188:;
	}

	// Token: 0x06003ED3 RID: 16083 RVA: 0x00232304 File Offset: 0x00231304
	private void ᜁ(XmlReader A_0, spr\u192F A_1)
	{
		int a_ = 10;
		int num = 11;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_10A;
			case 1:
				return;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_124;
				default:
					if (false)
					{
					}
					if (A_0.NamespaceURI == RecordTableEnumerator.b("㔿ぁ⩃籅㭇⥉⑋⭍㵏㍑❓筕㕗㍙㽛ⱝཟᅡୣeᱧ䝩ཫŭᵯ䡱᭳ၵṷ፹ύ᭽멿ﶍ", a_))
					{
						num = 13;
						continue;
					}
					goto IL_74;
				}
				break;
			case 3:
				goto IL_136;
			case 4:
				if (A_0.LocalName == RecordTableEnumerator.b("ȿⵁ㙃≅ⵇ㡉", a_))
				{
					num = 16;
					continue;
				}
				goto IL_74;
			case 5:
				num = 9;
				continue;
			case 6:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 5;
					continue;
				}
				num = 4;
				continue;
			case 7:
				goto IL_172;
			case 8:
				goto IL_74;
			case 9:
				if (A_1.\u171E() == sprỶ.TXFType.XF_CELL)
				{
					num = 17;
					continue;
				}
				return;
			case 10:
				goto IL_6F;
			case 12:
				goto IL_172;
			case 13:
				this.ᜀ(A_0, A_1);
				num = 8;
				continue;
			case 14:
				if (A_0.IsEmptyElement)
				{
					num = 1;
					continue;
				}
				A_0.Read();
				num = 7;
				continue;
			case 15:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				num = 14;
				continue;
			case 16:
				if (true)
				{
				}
				num = 2;
				continue;
			case 17:
				goto IL_124;
			}
			if (A_0 == null)
			{
				num = 10;
				continue;
			}
			num = 15;
			continue;
			IL_74:
			A_0.Skip();
			num = 12;
			continue;
			IL_124:
			A_1.ᜊ(false);
			num = 3;
			continue;
			IL_172:
			num = 6;
		}
		IL_6F:
		throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁╃≅ⵇ㡉", a_));
		IL_10A:
		throw new ArgumentNullException(RecordTableEnumerator.b("☿ⵁ㙃⭅⥇㹉", a_));
		IL_136:;
	}

	// Token: 0x06003ED4 RID: 16084 RVA: 0x00232528 File Offset: 0x00231528
	private void ᜀ(XmlReader A_0, spr\u192F A_1)
	{
		int a_ = 7;
		switch (0)
		{
		default:
		{
			int num = 7;
			IBorder border;
			string a_2;
			string a_3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					border.Color = this.ᜀ(A_0.Value);
					num = 5;
					continue;
				case 1:
					goto IL_74;
				case 2:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("焼嘾⽀♂ᙄ㍆え❊⡌", a_), RecordTableEnumerator.b("䠼䴾⽀祂㙄⑆ⅈ⹊⁌⹎≐繒㡔㹖㩘⥚㉜ⱞ๠բᅤ䩦੨Ѫl啮Ṱᕲ፴Ṷ᩸Ṻ䝼౾", a_)))
					{
						num = 6;
						continue;
					}
					goto IL_192;
				case 3:
					return;
				case 4:
					goto IL_18D;
				case 5:
					goto IL_79;
				case 6:
					goto IL_1E7;
				case 8:
					if (A_1 == null)
					{
						num = 4;
						continue;
					}
					num = 10;
					continue;
				case 9:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("縼倾ⵀⱂ㝄", a_), RecordTableEnumerator.b("䠼䴾⽀祂㙄⑆ⅈ⹊⁌⹎≐繒㡔㹖㩘⥚㉜ⱞ๠բᅤ䩦੨Ѫl啮Ṱᕲ፴Ṷ᩸Ṻ䝼౾", a_)))
					{
						num = 0;
						continue;
					}
					goto IL_79;
				case 10:
					if (A_0.AttributeCount != 0)
					{
						A_1.ᜊ(true);
						A_0.MoveToAttribute(RecordTableEnumerator.b("洼倾㉀⩂ㅄ⹆♈╊", a_), RecordTableEnumerator.b("䠼䴾⽀祂㙄⑆ⅈ⹊⁌⹎≐繒㡔㹖㩘⥚㉜ⱞ๠բᅤ䩦੨Ѫl啮Ṱᕲ፴Ṷ᩸Ṻ䝼౾", a_));
						string value = A_0.Value;
						a_2 = RecordTableEnumerator.b("猼倾⽀♂", a_);
						a_3 = RecordTableEnumerator.b("഼", a_);
						int index = Array.IndexOf<string>(sprỉ.\u17EB, value);
						border = A_1.ᜪ()[(BordersLineType)index];
						num = 9;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1E7;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				case 11:
					goto IL_192;
				case 12:
					goto IL_219;
				case 13:
					a_3 = A_0.Value;
					num = 12;
					continue;
				case 14:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("樼娾⡀⑂ⵄ㍆", a_), RecordTableEnumerator.b("䠼䴾⽀祂㙄⑆ⅈ⹊⁌⹎≐繒㡔㹖㩘⥚㉜ⱞ๠բᅤ䩦੨Ѫl啮Ṱᕲ፴Ṷ᩸Ṻ䝼౾", a_)))
					{
						num = 13;
						continue;
					}
					goto IL_295;
				}
				if (A_0 == null)
				{
					num = 1;
					continue;
				}
				num = 8;
				continue;
				IL_79:
				num = 2;
				continue;
				IL_192:
				num = 14;
				continue;
				IL_1E7:
				a_2 = A_0.Value;
				num = 11;
			}
			IL_74:
			throw new ArgumentNullException(RecordTableEnumerator.b("似娾⁀❂⁄㕆", a_));
			IL_18D:
			throw new ArgumentNullException(RecordTableEnumerator.b("嬼倾㍀⹂⑄㍆", a_));
			IL_219:
			IL_295:
			border.LineStyle = this.ᜀ(a_2, a_3);
			A_0.MoveToElement();
			return;
		}
		}
	}

	// Token: 0x06003ED5 RID: 16085 RVA: 0x002327E0 File Offset: 0x002317E0
	private void ᜉ(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 11;
		switch (0)
		{
		default:
		{
			int num = 13;
			for (;;)
			{
				Stack<KeyValuePair<string, List<object>>> stack;
				KeyValuePair<string, List<object>> keyValuePair;
				KeyValuePair<string, List<object>> keyValuePair2;
				switch (num)
				{
				case 0:
					if (A_0.NodeType == XmlNodeType.Text)
					{
						num = 11;
						continue;
					}
					num = 15;
					continue;
				case 1:
					if (A_0.NodeType != XmlNodeType.Element)
					{
						num = 2;
						continue;
					}
					num = 19;
					continue;
				case 2:
					goto IL_FB0;
				case 3:
					return;
				case 4:
					stack.Pop();
					num = 23;
					continue;
				case 5:
					num = 10;
					continue;
				case 6:
					if (true)
					{
					}
					stack.Push(keyValuePair);
					num = 20;
					continue;
				case 7:
					goto IL_10D3;
				case 8:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 6;
						continue;
					}
					goto IL_F6;
				case 9:
					if (!A_0.Read())
					{
						num = 22;
						continue;
					}
					num = 17;
					continue;
				case 10:
					if (!(A_0.NamespaceURI == RecordTableEnumerator.b("㑀ㅂ⭄絆㩈⡊╌⩎㱐㉒♔穖㑘㉚㹜ⵞ๠ၢ੤Ŧᵨ䙪๬nᱰ䥲ᩴᅶὸቺṼ᩾뮀ﶄ", a_)))
					{
						num = 7;
						continue;
					}
					goto IL_1129;
				case 11:
					goto IL_1129;
				case 12:
					if (A_0.NamespaceURI == RecordTableEnumerator.b("㑀ㅂ⭄絆㩈⡊╌⩎㱐㉒♔穖㑘㉚㹜ⵞ๠ၢ੤Ŧᵨ䙪๬nᱰ䥲ᩴᅶὸቺṼ᩾뮀ﶄ", a_))
					{
						num = 4;
						continue;
					}
					goto IL_F6;
				case 14:
					goto IL_F6;
				case 15:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 25;
						continue;
					}
					goto IL_F6;
				case 16:
					goto IL_AB;
				case 17:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 5;
						continue;
					}
					goto IL_10D3;
				case 18:
				{
					if (A_0.IsEmptyElement)
					{
						num = 3;
						continue;
					}
					A_0 = A_0.ReadSubtree();
					stack = new Stack<KeyValuePair<string, List<object>>>();
					KeyValuePair<string, List<object>> item = new KeyValuePair<string, List<object>>(string.Empty, new List<object>());
					stack.Push(item);
					num = 14;
					continue;
				}
				case 19:
					keyValuePair2 = new KeyValuePair<string, List<object>>(A_0.Name, new List<object>());
					goto IL_1175;
				case 20:
					goto IL_F6;
				case 21:
					keyValuePair2 = new KeyValuePair<string, List<object>>(A_0.Value.Trim(), null);
					goto IL_1175;
				case 22:
				{
					KeyValuePair<string, List<object>> item;
					List<object>.Enumerator enumerator = item.Value.GetEnumerator();
					num = 24;
					continue;
				}
				case 23:
					goto IL_F6;
				case 24:
					try
					{
						num = 2;
						for (;;)
						{
							List<object>.Enumerator enumerator;
							List<object>.Enumerator enumerator8;
							switch (num)
							{
							case 0:
								num = 3;
								continue;
							case 1:
								try
								{
									num = 3;
									for (;;)
									{
										switch (num)
										{
										case 0:
											if (A_1.VPageBreaks is XlsVPageBreaksCollection)
											{
												num = 10;
												continue;
											}
											goto IL_8AA;
										case 1:
											goto IL_817;
										case 2:
											try
											{
												num = 17;
												for (;;)
												{
													int num5;
													int num6;
													int a_2;
													switch (num)
													{
													case 0:
														try
														{
															num = 13;
															for (;;)
															{
																KeyValuePair<string, List<object>> keyValuePair3;
																List<object>.Enumerator enumerator3;
																switch (num)
																{
																case 0:
																	num = 8;
																	continue;
																case 1:
																	goto IL_658;
																case 2:
																	num = 3;
																	continue;
																case 3:
																{
																	string key;
																	if (!(key == RecordTableEnumerator.b("ɀⱂ⥄㉆⑈╊", a_)))
																	{
																		num = 12;
																		continue;
																	}
																	List<object>.Enumerator enumerator2 = keyValuePair3.Value.GetEnumerator();
																	num = 5;
																	continue;
																}
																case 4:
																	num = 10;
																	continue;
																case 5:
																	try
																	{
																		num = 0;
																		for (;;)
																		{
																			switch (num)
																			{
																			case 1:
																				num = 2;
																				continue;
																			case 2:
																				goto IL_645;
																			case 4:
																			{
																				List<object>.Enumerator enumerator2;
																				if (!enumerator2.MoveNext())
																				{
																					num = 1;
																					continue;
																				}
																				KeyValuePair<string, List<object>> keyValuePair4 = (KeyValuePair<string, List<object>>)enumerator2.Current;
																				int? num2 = new int?(int.Parse(keyValuePair4.Key));
																				num = 3;
																				continue;
																			}
																			}
																			IL_61F:
																			num = 4;
																			continue;
																			goto IL_61F;
																		}
																		IL_645:
																		break;
																	}
																	finally
																	{
																		List<object>.Enumerator enumerator2;
																		((IDisposable)enumerator2).Dispose();
																	}
																	goto Block_35;
																case 6:
																	try
																	{
																		num = 1;
																		for (;;)
																		{
																			switch (num)
																			{
																			case 0:
																				goto IL_599;
																			case 2:
																				num = 0;
																				continue;
																			case 4:
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
																					if (!enumerator3.MoveNext())
																					{
																						num = 2;
																						continue;
																					}
																					KeyValuePair<string, List<object>> keyValuePair5 = (KeyValuePair<string, List<object>>)enumerator3.Current;
																					int? num3 = new int?(int.Parse(keyValuePair5.Key));
																					num = 3;
																					continue;
																				}
																				}
																				break;
																			}
																			IL_528:
																			num = 4;
																			continue;
																			goto IL_528;
																		}
																		IL_599:
																		break;
																	}
																	finally
																	{
																		((IDisposable)enumerator3).Dispose();
																	}
																	goto IL_5AC;
																case 7:
																{
																	List<object>.Enumerator enumerator4;
																	if (!enumerator4.MoveNext())
																	{
																		num = 0;
																		continue;
																	}
																	keyValuePair3 = (KeyValuePair<string, List<object>>)enumerator4.Current;
																	num = 15;
																	continue;
																}
																case 8:
																	goto IL_725;
																case 9:
																	num = 14;
																	continue;
																case 11:
																{
																	string key;
																	if (!(key == RecordTableEnumerator.b("ፀⱂ㉄ᑆ㵈⩊㽌㭎", a_)))
																	{
																		num = 9;
																		continue;
																	}
																	List<object>.Enumerator enumerator5 = keyValuePair3.Value.GetEnumerator();
																	num = 1;
																	continue;
																}
																case 12:
																	num = 11;
																	continue;
																case 14:
																{
																	string key;
																	if (!(key == RecordTableEnumerator.b("ፀⱂ㉄Ɇ❈⽊", a_)))
																	{
																		num = 4;
																		continue;
																	}
																	goto IL_5AC;
																}
																case 15:
																{
																	string key;
																	if ((key = keyValuePair3.Key) != null)
																	{
																		num = 2;
																		continue;
																	}
																	break;
																}
																}
																goto IL_3E7;
																IL_5AC:
																enumerator3 = keyValuePair3.Value.GetEnumerator();
																num = 6;
																continue;
																IL_6E2:
																num = 7;
																continue;
																Block_35:
																try
																{
																	IL_658:
																	num = 4;
																	for (;;)
																	{
																		switch (num)
																		{
																		case 0:
																		{
																			List<object>.Enumerator enumerator5;
																			if (!enumerator5.MoveNext())
																			{
																				num = 1;
																				continue;
																			}
																			KeyValuePair<string, List<object>> keyValuePair6 = (KeyValuePair<string, List<object>>)enumerator5.Current;
																			int? num4 = new int?(int.Parse(keyValuePair6.Key));
																			num = 2;
																			continue;
																		}
																		case 1:
																			num = 3;
																			continue;
																		case 3:
																			goto IL_6D2;
																		}
																		IL_6AC:
																		num = 0;
																		continue;
																		goto IL_6AC;
																	}
																	IL_6D2:;
																}
																finally
																{
																	List<object>.Enumerator enumerator5;
																	((IDisposable)enumerator5).Dispose();
																}
																IL_3E7:
																goto IL_6E2;
															}
															IL_725:
															goto IL_264;
														}
														finally
														{
															List<object>.Enumerator enumerator4;
															((IDisposable)enumerator4).Dispose();
														}
														goto IL_738;
														IL_264:
														num = 12;
														continue;
													case 1:
														num5 = 65536;
														goto IL_763;
													case 2:
													{
														int? num4;
														if (num4 != null)
														{
															num = 7;
															continue;
														}
														num = 6;
														continue;
													}
													case 3:
													{
														int? num3;
														num5 = num3.Value + 1;
														goto IL_763;
													}
													case 5:
													{
														List<object>.Enumerator enumerator6;
														if (!enumerator6.MoveNext())
														{
															num = 14;
															continue;
														}
														KeyValuePair<string, List<object>> keyValuePair7 = (KeyValuePair<string, List<object>>)enumerator6.Current;
														num = 9;
														continue;
													}
													case 6:
														num6 = 1;
														goto IL_738;
													case 7:
														num = 15;
														continue;
													case 8:
													{
														int? num2 = null;
														int? num4 = null;
														int? num3 = null;
														KeyValuePair<string, List<object>> keyValuePair7;
														List<object>.Enumerator enumerator4 = keyValuePair7.Value.GetEnumerator();
														num = 0;
														continue;
													}
													case 9:
													{
														KeyValuePair<string, List<object>> keyValuePair7;
														if (!(keyValuePair7.Key != RecordTableEnumerator.b("ɀⱂ⥄Ն㭈⹊ⱌ⑎", a_)))
														{
															num = 8;
															continue;
														}
														break;
													}
													case 10:
														num = 19;
														continue;
													case 11:
													{
														int? num3;
														if (num3 != null)
														{
															num = 13;
															continue;
														}
														num = 1;
														continue;
													}
													case 12:
													{
														int? num2;
														if (num2 != null)
														{
															num = 10;
															continue;
														}
														break;
													}
													case 13:
														num = 3;
														continue;
													case 14:
														num = 16;
														continue;
													case 15:
													{
														int? num4;
														num6 = num4.Value + 1;
														goto IL_738;
													}
													case 16:
														goto IL_804;
													case 18:
													{
														int? num2;
														a_2 = num2.Value + 1;
														num = 2;
														continue;
													}
													case 19:
													{
														int? num2;
														if (num2.Value > 0)
														{
															num = 18;
															continue;
														}
														break;
													}
													}
													IL_36C:
													num = 5;
													continue;
													goto IL_36C;
													IL_738:
													int a_3 = num6;
													num = 11;
													continue;
													IL_763:
													int a_4 = num5;
													XlsVPageBreaksCollection xlsVPageBreaksCollection = A_1.VPageBreaks as XlsVPageBreaksCollection;
													XlsVPageBreak xlsVPageBreak = new XlsVPageBreak((spr\u2158)xlsVPageBreaksCollection.ReservedHandle, xlsVPageBreaksCollection);
													xlsVPageBreak.ᜀ(a_2, a_3, a_4);
													xlsVPageBreaksCollection.ᜀ(xlsVPageBreak);
													num = 4;
												}
												IL_804:
												break;
											}
											finally
											{
												List<object>.Enumerator enumerator6;
												((IDisposable)enumerator6).Dispose();
											}
											goto IL_817;
										case 4:
											num = 0;
											continue;
										case 5:
										{
											KeyValuePair<string, List<object>> keyValuePair8;
											List<object>.Enumerator enumerator7 = keyValuePair8.Value.GetEnumerator();
											num = 12;
											continue;
										}
										case 6:
										{
											if (!enumerator8.MoveNext())
											{
												num = 9;
												continue;
											}
											KeyValuePair<string, List<object>> keyValuePair8 = (KeyValuePair<string, List<object>>)enumerator8.Current;
											num = 7;
											continue;
										}
										case 7:
										{
											KeyValuePair<string, List<object>> keyValuePair8;
											if (keyValuePair8.Key == RecordTableEnumerator.b("ɀⱂ⥄Ն㭈⹊ⱌ⑎≐", a_))
											{
												num = 4;
												continue;
											}
											goto IL_8AA;
										}
										case 8:
										{
											KeyValuePair<string, List<object>> keyValuePair8;
											if (keyValuePair8.Key == RecordTableEnumerator.b("ፀⱂ㉄Ն㭈⹊ⱌ⑎≐", a_))
											{
												num = 1;
												continue;
											}
											break;
										}
										case 9:
											goto IL_F22;
										case 10:
										{
											KeyValuePair<string, List<object>> keyValuePair8;
											List<object>.Enumerator enumerator6 = keyValuePair8.Value.GetEnumerator();
											num = 2;
											continue;
										}
										case 11:
											if (A_1.HPageBreaks is XlsHPageBreaksCollection)
											{
												num = 5;
												continue;
											}
											break;
										case 12:
											try
											{
												num = 8;
												for (;;)
												{
													int a_5;
													int num10;
													int num11;
													switch (num)
													{
													case 0:
														try
														{
															num = 6;
															for (;;)
															{
																KeyValuePair<string, List<object>> keyValuePair9;
																List<object>.Enumerator enumerator12;
																switch (num)
																{
																case 0:
																	num = 1;
																	continue;
																case 1:
																	goto IL_E15;
																case 2:
																{
																	string key2;
																	if (!(key2 == RecordTableEnumerator.b("ፀⱂ㉄", a_)))
																	{
																		num = 10;
																		continue;
																	}
																	List<object>.Enumerator enumerator9 = keyValuePair9.Value.GetEnumerator();
																	num = 8;
																	continue;
																}
																case 3:
																{
																	string key2;
																	if (!(key2 == RecordTableEnumerator.b("ɀⱂ⥄Ɇ❈⽊", a_)))
																	{
																		num = 11;
																		continue;
																	}
																	List<object>.Enumerator enumerator10 = keyValuePair9.Value.GetEnumerator();
																	num = 12;
																	continue;
																}
																case 5:
																	try
																	{
																		num = 1;
																		for (;;)
																		{
																			switch (num)
																			{
																			case 0:
																				goto IL_B91;
																			case 3:
																				num = 0;
																				continue;
																			case 4:
																			{
																				List<object>.Enumerator enumerator11;
																				if (!enumerator11.MoveNext())
																				{
																					num = 3;
																					continue;
																				}
																				KeyValuePair<string, List<object>> keyValuePair10 = (KeyValuePair<string, List<object>>)enumerator11.Current;
																				int? num7 = new int?(int.Parse(keyValuePair10.Key));
																				num = 2;
																				continue;
																			}
																			}
																			IL_B6B:
																			num = 4;
																			continue;
																			goto IL_B6B;
																		}
																		IL_B91:
																		break;
																	}
																	finally
																	{
																		List<object>.Enumerator enumerator11;
																		((IDisposable)enumerator11).Dispose();
																	}
																	goto Block_53;
																case 7:
																{
																	string key2;
																	if ((key2 = keyValuePair9.Key) != null)
																	{
																		num = 9;
																		continue;
																	}
																	break;
																}
																case 8:
																	try
																	{
																		num = 0;
																		for (;;)
																		{
																			switch (num)
																			{
																			case 1:
																				goto IL_DBF;
																			case 3:
																				num = 1;
																				continue;
																			case 4:
																			{
																				List<object>.Enumerator enumerator9;
																				if (!enumerator9.MoveNext())
																				{
																					num = 3;
																					continue;
																				}
																				KeyValuePair<string, List<object>> keyValuePair11 = (KeyValuePair<string, List<object>>)enumerator9.Current;
																				int? num8 = new int?(int.Parse(keyValuePair11.Key));
																				num = 2;
																				continue;
																			}
																			}
																			IL_D6D:
																			num = 4;
																			continue;
																			goto IL_D6D;
																		}
																		IL_DBF:
																		break;
																	}
																	finally
																	{
																		List<object>.Enumerator enumerator9;
																		((IDisposable)enumerator9).Dispose();
																	}
																	goto IL_DD2;
																case 9:
																	num = 2;
																	continue;
																case 10:
																	goto IL_DD2;
																case 11:
																	num = 4;
																	continue;
																case 12:
																	goto IL_BA4;
																case 13:
																{
																	string key2;
																	if (!(key2 == RecordTableEnumerator.b("ɀⱂ⥄ᑆ㵈⩊㽌㭎", a_)))
																	{
																		num = 15;
																		continue;
																	}
																	List<object>.Enumerator enumerator11 = keyValuePair9.Value.GetEnumerator();
																	num = 5;
																	continue;
																}
																case 14:
																	if (!enumerator12.MoveNext())
																	{
																		num = 0;
																		continue;
																	}
																	goto IL_C2E;
																case 15:
																	num = 3;
																	continue;
																}
																goto IL_AF9;
																IL_C2E:
																keyValuePair9 = (KeyValuePair<string, List<object>>)enumerator12.Current;
																num = 7;
																continue;
																Block_53:
																try
																{
																	IL_BA4:
																	num = 2;
																	for (;;)
																	{
																		switch (num)
																		{
																		case 1:
																			num = 3;
																			continue;
																		case 3:
																			goto IL_C1E;
																		case 4:
																		{
																			List<object>.Enumerator enumerator10;
																			if (!enumerator10.MoveNext())
																			{
																				num = 1;
																				continue;
																			}
																			KeyValuePair<string, List<object>> keyValuePair12 = (KeyValuePair<string, List<object>>)enumerator10.Current;
																			int? num9 = new int?(int.Parse(keyValuePair12.Key));
																			num = 0;
																			continue;
																		}
																		}
																		IL_BF8:
																		num = 4;
																		continue;
																		goto IL_BF8;
																	}
																	IL_C1E:
																	goto IL_C9C;
																}
																finally
																{
																	List<object>.Enumerator enumerator10;
																	((IDisposable)enumerator10).Dispose();
																}
																goto IL_C2E;
																IL_C9C:
																num = 14;
																continue;
																IL_AF9:
																goto IL_C9C;
																IL_DD2:
																num = 13;
															}
															IL_E15:
															goto IL_E6D;
														}
														finally
														{
															List<object>.Enumerator enumerator12;
															((IDisposable)enumerator12).Dispose();
														}
														goto IL_E25;
														IL_E6D:
														num = 19;
														continue;
													case 1:
														num = 17;
														continue;
													case 2:
													{
														int? num8;
														a_5 = num8.Value + 1;
														num = 16;
														continue;
													}
													case 3:
														num10 = 256;
														goto IL_E25;
													case 4:
													{
														int? num8 = null;
														int? num7 = null;
														int? num9 = null;
														KeyValuePair<string, List<object>> keyValuePair13;
														List<object>.Enumerator enumerator12 = keyValuePair13.Value.GetEnumerator();
														num = 0;
														continue;
													}
													case 5:
														num = 6;
														continue;
													case 6:
													{
														int? num8;
														if (num8.Value > 0)
														{
															num = 2;
															continue;
														}
														break;
													}
													case 7:
														num = 14;
														continue;
													case 9:
													{
														List<object>.Enumerator enumerator7;
														if (!enumerator7.MoveNext())
														{
															num = 13;
															continue;
														}
														KeyValuePair<string, List<object>> keyValuePair13 = (KeyValuePair<string, List<object>>)enumerator7.Current;
														num = 11;
														continue;
													}
													case 11:
													{
														KeyValuePair<string, List<object>> keyValuePair13;
														if (!(keyValuePair13.Key != RecordTableEnumerator.b("ፀⱂ㉄Ն㭈⹊ⱌ⑎", a_)))
														{
															num = 4;
															continue;
														}
														break;
													}
													case 12:
													{
														int? num9;
														if (num9 != null)
														{
															num = 7;
															continue;
														}
														num = 3;
														continue;
													}
													case 13:
														num = 18;
														continue;
													case 14:
													{
														int? num9;
														num10 = num9.Value + 1;
														goto IL_E25;
													}
													case 15:
														num11 = 1;
														goto IL_A48;
													case 16:
													{
														int? num7;
														if (num7 != null)
														{
															num = 1;
															continue;
														}
														num = 15;
														continue;
													}
													case 17:
													{
														int? num7;
														num11 = num7.Value + 1;
														goto IL_A48;
													}
													case 18:
														goto IL_F0F;
													case 19:
													{
														int? num8;
														if (num8 != null)
														{
															num = 5;
															continue;
														}
														break;
													}
													}
													goto IL_99E;
													IL_A48:
													int a_6 = num11;
													num = 12;
													continue;
													IL_E25:
													int a_7 = num10;
													XlsHPageBreaksCollection xlsHPageBreaksCollection = A_1.HPageBreaks as XlsHPageBreaksCollection;
													HPageBreak hpageBreak = new HPageBreak((spr\u2158)xlsHPageBreaksCollection.ReservedHandle, xlsHPageBreaksCollection);
													hpageBreak.ᜀ(a_5, a_6, a_7);
													xlsHPageBreaksCollection.ᜀ(hpageBreak);
													num = 10;
													continue;
													IL_E93:
													num = 9;
													continue;
													IL_99E:
													goto IL_E93;
												}
												IL_F0F:
												break;
											}
											finally
											{
												List<object>.Enumerator enumerator7;
												((IDisposable)enumerator7).Dispose();
											}
											goto IL_F22;
										case 13:
											goto IL_F2E;
										}
										goto IL_1DF;
										IL_817:
										num = 11;
										continue;
										IL_8AA:
										num = 8;
										continue;
										IL_8E6:
										num = 6;
										continue;
										IL_1DF:
										goto IL_8E6;
										IL_F22:
										num = 13;
									}
									IL_F2E:
									break;
								}
								finally
								{
									((IDisposable)enumerator8).Dispose();
								}
								goto IL_F3E;
							case 3:
								goto IL_F9D;
							case 4:
								if (!enumerator.MoveNext())
								{
									num = 0;
									continue;
								}
								goto IL_F3E;
							}
							goto IL_190;
							IL_F3E:
							KeyValuePair<string, List<object>> keyValuePair14 = (KeyValuePair<string, List<object>>)enumerator.Current;
							enumerator8 = keyValuePair14.Value.GetEnumerator();
							num = 1;
							continue;
							IL_F6B:
							num = 4;
							continue;
							IL_190:
							goto IL_F6B;
						}
						IL_F9D:
						goto IL_11C0;
					}
					finally
					{
						List<object>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					goto IL_FB0;
				case 25:
					num = 12;
					continue;
				case 26:
					if (A_1 == null)
					{
						num = 27;
						continue;
					}
					A_0.MoveToElement();
					num = 18;
					continue;
				case 27:
					goto IL_1170;
				}
				if (A_0 == null)
				{
					num = 16;
					continue;
				}
				num = 26;
				continue;
				IL_F6:
				num = 9;
				continue;
				IL_FB0:
				num = 21;
				continue;
				IL_10D3:
				num = 0;
				continue;
				IL_1129:
				num = 1;
				continue;
				IL_1175:
				keyValuePair = keyValuePair2;
				stack.Peek().Value.Add(keyValuePair);
				num = 8;
			}
			IL_AB:
			throw new ArgumentNullException(RecordTableEnumerator.b("㍀♂⑄⍆ⱈ㥊", a_));
			IL_1170:
			throw new ArgumentNullException(RecordTableEnumerator.b("㉀⭂⁄≆㵈", a_));
			IL_11C0:
			A_0.Close();
			return;
		}
		}
	}

	// Token: 0x06003ED6 RID: 16086 RVA: 0x00233AD8 File Offset: 0x00232AD8
	private void ᜈ(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 14;
		int num = 47;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_296;
			case 1:
				goto IL_20D;
			case 2:
				if (A_0.LocalName == RecordTableEnumerator.b("၃❅⩇ॉ⍋≍㽏⁑ᵓ㡕㱗㽙⑛", a_))
				{
					num = 29;
					continue;
				}
				goto IL_42C;
			case 3:
				goto IL_89A;
			case 4:
				if (A_0.LocalName == RecordTableEnumerator.b("ᝃ㙅⑇⍉㡋ٍ㽏⁑㵓ⱕ㝗㑙⡛㽝౟", a_))
				{
					num = 39;
					continue;
				}
				goto IL_743;
			case 5:
				this.ᜆ(A_0, A_1);
				num = 1;
				continue;
			case 6:
				goto IL_2E7;
			case 7:
				if (A_0.LocalName == RecordTableEnumerator.b("ṃ⥅❇❉", a_))
				{
					num = 34;
					continue;
				}
				goto IL_49B;
			case 8:
				if (A_0.LocalName == RecordTableEnumerator.b("ᝃ⹅❇㵉᱋⽍㝏㝑ᙓ⑕㵗㭙㝛ѝཟൡॣ", a_))
				{
					num = 73;
					continue;
				}
				goto IL_A6B;
			case 9:
				A_0.Read();
				A_1.ActivePane = (int)XmlConvert.ToUInt16(A_0.Value);
				A_0.Skip();
				num = 37;
				continue;
			case 10:
				num = 61;
				continue;
			case 11:
				if (A_0.LocalName == RecordTableEnumerator.b("၃⥅㡇ᡉ⍋㥍ُ㭑❓㽕㩗㙙㥛", a_))
				{
					num = 10;
					continue;
				}
				goto IL_9C0;
			case 12:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("ㅃ㑅♇灉㽋ⵍ㡏㝑㥓㝕⭗睙ㅛ㝝͟ၡୣᕥݧ౩ᡫ䍭፯ᵱᥳ䱵᝷ᱹ᩻᝽뺃", a_))
				{
					num = 92;
					continue;
				}
				goto IL_49B;
			case 13:
				goto IL_4D4;
			case 14:
				goto IL_C52;
			case 15:
				goto IL_6BB;
			case 16:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("ㅃ㑅♇灉㽋ⵍ㡏㝑㥓㝕⭗睙ㅛ㝝͟ၡୣᕥݧ౩ᡫ䍭፯ᵱᥳ䱵᝷ᱹ᩻᝽뺃", a_))
				{
					num = 74;
					continue;
				}
				goto IL_A6B;
			case 17:
				A_1.DisplayPageBreaks = true;
				num = 24;
				continue;
			case 18:
				return;
			case 19:
				goto IL_C52;
			case 20:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("ㅃ㑅♇灉㽋ⵍ㡏㝑㥓㝕⭗睙ㅛ㝝͟ၡୣᕥݧ౩ᡫ䍭፯ᵱᥳ䱵᝷ᱹ᩻᝽뺃", a_))
				{
					num = 63;
					continue;
				}
				goto IL_6BB;
			case 21:
				if (A_0.LocalName == RecordTableEnumerator.b("Ƀ㑅❇ぉ⥋⁍ṏ㵑ݓ♕㑗㍙⡛", a_))
				{
					num = 25;
					continue;
				}
				goto IL_296;
			case 22:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("ㅃ㑅♇灉㽋ⵍ㡏㝑㥓㝕⭗睙ㅛ㝝͟ၡୣᕥݧ౩ᡫ䍭፯ᵱᥳ䱵᝷ᱹ᩻᝽뺃", a_))
				{
					num = 9;
					continue;
				}
				goto IL_462;
			case 23:
				goto IL_A6B;
			case 24:
				goto IL_A32;
			case 25:
				num = 57;
				continue;
			case 26:
				if (A_0.LocalName == RecordTableEnumerator.b("ቃ⽅㭇⍉⹋≍㕏", a_))
				{
					num = 65;
					continue;
				}
				A_0.Skip();
				num = 85;
				continue;
			case 27:
				num = 31;
				continue;
			case 28:
				goto IL_49B;
			case 29:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_AF2;
				default:
					if (false)
					{
					}
					num = 36;
					continue;
				}
				break;
			case 30:
				if (A_1 == null)
				{
					num = 79;
					continue;
				}
				A_0.MoveToElement();
				num = 64;
				continue;
			case 31:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("ㅃ㑅♇灉㽋ⵍ㡏㝑㥓㝕⭗睙ㅛ㝝͟ၡୣᕥݧ౩ᡫ䍭፯ᵱᥳ䱵᝷ᱹ᩻᝽뺃", a_))
				{
					num = 77;
					continue;
				}
				goto IL_3A6;
			case 32:
				goto IL_1A1;
			case 33:
				this.ᜂ(A_0, A_1);
				num = 48;
				continue;
			case 34:
				num = 12;
				continue;
			case 35:
				num = 91;
				continue;
			case 36:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("ㅃ㑅♇灉㽋ⵍ㡏㝑㥓㝕⭗睙ㅛ㝝͟ၡୣᕥݧ౩ᡫ䍭፯ᵱᥳ䱵᝷ᱹ᩻᝽뺃", a_))
				{
					num = 56;
					continue;
				}
				goto IL_42C;
			case 37:
				goto IL_462;
			case 38:
				if (A_0.LocalName == RecordTableEnumerator.b("၃⥅㡇ᡉ⍋㥍቏㵑⁓≕㝗㝙౛㽝๟ݡ", a_))
				{
					num = 27;
					continue;
				}
				goto IL_3A6;
			case 39:
				num = 69;
				continue;
			case 40:
				num = 46;
				continue;
			case 41:
				num = 20;
				continue;
			case 42:
				if (A_0.LocalName == RecordTableEnumerator.b("ࡃ⍅⹇㹉ཋ⅍㱏❑㥓㡕੗㍙㭛㙝ᑟ㉡գࡥ൧", a_))
				{
					num = 81;
					continue;
				}
				goto IL_2E7;
			case 43:
				num = 50;
				continue;
			case 44:
				A_1.WindowTwo.ᜈ(true);
				num = 0;
				continue;
			case 45:
				if (A_0.LocalName == RecordTableEnumerator.b("ᑃ❅♇⽉㽋", a_))
				{
					num = 35;
					continue;
				}
				goto IL_5AA;
			case 46:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("ㅃ㑅♇灉㽋ⵍ㡏㝑㥓㝕⭗睙ㅛ㝝͟ၡୣᕥݧ౩ᡫ䍭፯ᵱᥳ䱵᝷ᱹ᩻᝽뺃", a_))
				{
					num = 5;
					continue;
				}
				goto IL_20D;
			case 48:
				goto IL_5AA;
			case 49:
				goto IL_9C0;
			case 50:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("ㅃ㑅♇灉㽋ⵍ㡏㝑㥓㝕⭗睙ㅛ㝝͟ၡୣᕥݧ౩ᡫ䍭፯ᵱᥳ䱵᝷ᱹ᩻᝽뺃", a_))
				{
					num = 78;
					continue;
				}
				goto IL_89A;
			case 51:
				A_0.Read();
				A_1.HorizontalSplit = (int)XmlConvert.ToUInt16(A_0.Value);
				A_0.Skip();
				num = 55;
				continue;
			case 52:
				num = 83;
				continue;
			case 53:
				A_0.Read();
				A_1.FirstVisibleColumn = (int)XmlConvert.ToUInt16(A_0.Value);
				A_0.Skip();
				num = 6;
				continue;
			case 54:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("ㅃ㑅♇灉㽋ⵍ㡏㝑㥓㝕⭗睙ㅛ㝝͟ၡୣᕥݧ౩ᡫ䍭፯ᵱᥳ䱵᝷ᱹ᩻᝽뺃", a_))
				{
					num = 58;
					continue;
				}
				goto IL_571;
			case 55:
				goto IL_743;
			case 56:
				A_0.Read();
				A_1.TabKnownColor = (ExcelColors)Enum.Parse(typeof(ExcelColors), A_0.Value, true);
				A_0.Skip();
				num = 75;
				continue;
			case 57:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("ㅃ㑅♇灉㽋ⵍ㡏㝑㥓㝕⭗睙ㅛ㝝͟ၡୣᕥݧ౩ᡫ䍭፯ᵱᥳ䱵᝷ᱹ᩻᝽뺃", a_))
				{
					num = 44;
					continue;
				}
				goto IL_296;
			case 58:
				A_1.WindowTwo.ᜊ(true);
				num = 84;
				continue;
			case 59:
				return;
			case 60:
				num = 54;
				continue;
			case 61:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("ㅃ㑅♇灉㽋ⵍ㡏㝑㥓㝕⭗睙ㅛ㝝͟ၡୣᕥݧ౩ᡫ䍭፯ᵱᥳ䱵᝷ᱹ᩻᝽뺃", a_))
				{
					num = 70;
					continue;
				}
				goto IL_9C0;
			case 62:
				num = 76;
				continue;
			case 63:
				A_0.Read();
				A_1.VerticalSplit = (int)XmlConvert.ToUInt16(A_0.Value);
				A_0.Skip();
				num = 15;
				continue;
			case 64:
				if (A_0.IsEmptyElement)
				{
					num = 59;
					continue;
				}
				A_0.Read();
				A_1.ZoomScalePageBreakView = 60;
				num = 19;
				continue;
			case 65:
				this.ᜇ(A_0, A_1);
				num = 14;
				continue;
			case 66:
				if (A_0.LocalName == RecordTableEnumerator.b("Ƀ㑅ⵇ⽉㙋⭍O㍑㩓㍕⭗", a_))
				{
					num = 60;
					continue;
				}
				goto IL_571;
			case 67:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 18;
					continue;
				}
				num = 2;
				continue;
			case 68:
				if (A_0.LocalName == RecordTableEnumerator.b("C⽅㭇㩉⁋⽍⥏ɑ㕓ㅕ㵗ᡙ⹛㭝şॡ", a_))
				{
					num = 62;
					continue;
				}
				goto IL_A32;
			case 69:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("ㅃ㑅♇灉㽋ⵍ㡏㝑㥓㝕⭗睙ㅛ㝝͟ၡୣᕥݧ౩ᡫ䍭፯ᵱᥳ䱵᝷ᱹ᩻᝽뺃", a_))
				{
					num = 51;
					continue;
				}
				goto IL_743;
			case 70:
				A_0.Read();
				A_1.WindowTwo.ᜃ(XmlConvert.ToUInt16(A_0.Value));
				A_0.Skip();
				num = 49;
				continue;
			case 71:
				if (A_0.LocalName == RecordTableEnumerator.b("ᑃ❅⽇⽉Ὃ⭍⑏❑⑓", a_))
				{
					num = 40;
					continue;
				}
				goto IL_20D;
			case 72:
				if (A_0.LocalName == RecordTableEnumerator.b("ᑃ❅⽇⽉๋㱍㕏㍑㽓ౕ㝗㕙ㅛ", a_))
				{
					num = 52;
					continue;
				}
				goto IL_4D4;
			case 73:
				num = 16;
				continue;
			case 74:
				A_1.ViewMode = ViewMode.Preview;
				num = 23;
				continue;
			case 75:
				goto IL_42C;
			case 76:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("ㅃ㑅♇灉㽋ⵍ㡏㝑㥓㝕⭗睙ㅛ㝝͟ၡୣᕥݧ౩ᡫ䍭፯ᵱᥳ䱵᝷ᱹ᩻᝽뺃", a_))
				{
					num = 17;
					continue;
				}
				goto IL_A32;
			case 77:
				A_0.Read();
				A_1.FirstVisibleRow = (int)XmlConvert.ToUInt16(A_0.Value);
				A_0.Skip();
				num = 82;
				continue;
			case 78:
				this.ᜀ(A_0, A_1);
				num = 3;
				continue;
			case 79:
				goto IL_710;
			case 80:
				num = 22;
				continue;
			case 81:
				num = 87;
				continue;
			case 82:
				goto IL_3A6;
			case 83:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("ㅃ㑅♇灉㽋ⵍ㡏㝑㥓㝕⭗睙ㅛ㝝͟ၡୣᕥݧ౩ᡫ䍭፯ᵱᥳ䱵᝷ᱹ᩻᝽뺃", a_))
				{
					num = 86;
					continue;
				}
				goto IL_4D4;
			case 84:
				goto IL_AF2;
			case 85:
				goto IL_C52;
			case 86:
				A_0.Read();
				A_1.ZoomScalePageBreakView = XmlConvert.ToInt32(A_0.Value);
				A_0.Skip();
				num = 13;
				continue;
			case 87:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("ㅃ㑅♇灉㽋ⵍ㡏㝑㥓㝕⭗睙ㅛ㝝͟ၡୣᕥݧ౩ᡫ䍭፯ᵱᥳ䱵᝷ᱹ᩻᝽뺃", a_))
				{
					num = 53;
					continue;
				}
				goto IL_2E7;
			case 88:
				if (A_0.LocalName == RecordTableEnumerator.b("ᝃ㙅⑇⍉㡋ᡍ㕏⁑⁓㽕㭗㭙せ", a_))
				{
					num = 41;
					continue;
				}
				goto IL_6BB;
			case 89:
				if (A_0.LocalName == RecordTableEnumerator.b("ᑃ㑅ⅇ⑉㡋", a_))
				{
					num = 43;
					continue;
				}
				goto IL_89A;
			case 90:
				if (A_0.LocalName == RecordTableEnumerator.b("Ճ╅㱇⍉㩋⭍O㍑㩓㍕", a_))
				{
					num = 80;
					continue;
				}
				goto IL_462;
			case 91:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("ㅃ㑅♇灉㽋ⵍ㡏㝑㥓㝕⭗睙ㅛ㝝͟ၡୣᕥݧ౩ᡫ䍭፯ᵱᥳ䱵᝷ᱹ᩻᝽뺃", a_))
				{
					num = 33;
					continue;
				}
				goto IL_5AA;
			case 92:
				A_0.Read();
				A_1.Zoom = XmlConvert.ToInt32(A_0.Value);
				A_1.ZoomScaleNormal = A_1.Zoom;
				A_0.Skip();
				num = 28;
				continue;
			}
			if (A_0 == null)
			{
				num = 32;
				continue;
			}
			num = 30;
			continue;
			IL_20D:
			num = 90;
			continue;
			IL_296:
			num = 68;
			continue;
			IL_2E7:
			if (true)
			{
			}
			num = 66;
			continue;
			IL_3A6:
			num = 11;
			continue;
			IL_42C:
			num = 71;
			continue;
			IL_462:
			num = 45;
			continue;
			IL_49B:
			num = 89;
			continue;
			IL_4D4:
			num = 8;
			continue;
			IL_571:
			num = 21;
			continue;
			IL_AF2:
			goto IL_571;
			IL_5AA:
			num = 4;
			continue;
			IL_6BB:
			num = 42;
			continue;
			IL_743:
			num = 38;
			continue;
			IL_89A:
			num = 26;
			continue;
			IL_9C0:
			num = 88;
			continue;
			IL_A32:
			num = 72;
			continue;
			IL_A6B:
			num = 7;
			continue;
			IL_C52:
			num = 67;
		}
		IL_1A1:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙃⍅⥇⹉⥋㱍", a_));
		IL_710:
		throw new ArgumentNullException(RecordTableEnumerator.b("㝃⹅ⵇ⽉㡋", a_));
	}

	// Token: 0x06003ED7 RID: 16087 RVA: 0x0023475C File Offset: 0x0023375C
	private void ᜇ(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 15;
		int num = 3;
		int num2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_5B;
			case 1:
				if (num2 < 0)
				{
					num = 2;
					continue;
				}
				goto IL_E1;
			case 2:
				goto IL_8E;
			case 4:
				goto IL_DF;
			case 5:
			{
				if (A_1 == null)
				{
					num = 4;
					continue;
				}
				string value = A_0.ReadElementContentAsString();
				num2 = Array.IndexOf<string>(sprỉ.\u17EF, value);
				num = 1;
				continue;
			}
			}
			if (A_0 == null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_C0;
				default:
					if (false)
					{
					}
					num = 0;
					break;
				}
			}
			else
			{
				num = 5;
			}
		}
		IL_5B:
		throw new ArgumentNullException(RecordTableEnumerator.b("㝄≆⡈⽊⡌㵎", a_));
		IL_8E:
		if (true)
		{
		}
		IL_C0:
		throw new XmlException();
		IL_DF:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙄⽆ⱈ⹊㥌", a_));
		IL_E1:
		A_1.Visibility = (WorksheetVisibility)num2;
	}

	// Token: 0x06003ED8 RID: 16088 RVA: 0x00234854 File Offset: 0x00233854
	private void ᜆ(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 14;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 13;
				continue;
			case 1:
				return;
			case 2:
				goto IL_17D;
			case 3:
				return;
			case 4:
				if (A_1 == null)
				{
					num = 8;
					continue;
				}
				A_0.MoveToElement();
				num = 14;
				continue;
			case 5:
				num = 26;
				continue;
			case 7:
				goto IL_190;
			case 8:
				goto IL_13F;
			case 9:
				num = 21;
				continue;
			case 10:
				goto IL_31E;
			case 11:
				goto IL_93;
			case 12:
				this.ᜄ(A_0, A_1);
				num = 17;
				continue;
			case 13:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("ㅃ㑅♇灉㽋ⵍ㡏㝑㥓㝕⭗睙ㅛ㝝͟ၡୣᕥݧ౩ᡫ䍭፯ᵱᥳ䱵᝷ᱹ᩻᝽뺃", a_))
				{
					num = 12;
					continue;
				}
				goto IL_2E5;
			case 14:
				if (A_0.IsEmptyElement)
				{
					num = 1;
					continue;
				}
				A_0.Read();
				num = 25;
				continue;
			case 15:
				if (!(A_0.LocalName == RecordTableEnumerator.b("Ƀ⥅❇㹉⥋㱍", a_)))
				{
					num = 5;
					continue;
				}
				goto IL_190;
			case 16:
				this.ᜃ(A_0, A_1);
				num = 2;
				continue;
			case 17:
				goto IL_2E5;
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
					this.ᜅ(A_0, A_1);
					num = 10;
					continue;
				}
				break;
			case 19:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 3;
					continue;
				}
				num = 15;
				continue;
			case 20:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("ㅃ㑅♇灉㽋ⵍ㡏㝑㥓㝕⭗睙ㅛ㝝͟ၡୣᕥݧ౩ᡫ䍭፯ᵱᥳ䱵᝷ᱹ᩻᝽뺃", a_))
				{
					num = 18;
					continue;
				}
				goto IL_31E;
			case 21:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("ㅃ㑅♇灉㽋ⵍ㡏㝑㥓㝕⭗睙ㅛ㝝͟ၡୣᕥݧ౩ᡫ䍭፯ᵱᥳ䱵᝷ᱹ᩻᝽뺃", a_))
				{
					num = 16;
					continue;
				}
				goto IL_17D;
			case 22:
				if (A_0.LocalName == RecordTableEnumerator.b("ࡃ❅ㅇ╉㥋㩍", a_))
				{
					num = 0;
					continue;
				}
				goto IL_2E5;
			case 23:
				if (A_0.LocalName == RecordTableEnumerator.b("ᑃ❅⽇⽉ŋ⽍≏㕑㵓㡕⭗", a_))
				{
					num = 9;
					continue;
				}
				goto IL_17D;
			case 24:
				goto IL_20A;
			case 25:
				goto IL_20A;
			case 26:
				if (true)
				{
				}
				if (A_0.LocalName == RecordTableEnumerator.b("ృ⍅⥇⹉⥋㱍", a_))
				{
					num = 7;
					continue;
				}
				goto IL_31E;
			}
			IL_85:
			if (A_0 == null)
			{
				num = 11;
				continue;
			}
			num = 4;
			continue;
			goto IL_85;
			IL_17D:
			A_0.Skip();
			num = 24;
			continue;
			IL_190:
			num = 20;
			continue;
			IL_20A:
			num = 19;
			continue;
			IL_2E5:
			num = 23;
			continue;
			IL_31E:
			num = 22;
		}
		IL_93:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙃⍅⥇⹉⥋㱍", a_));
		IL_13F:
		throw new ArgumentNullException(RecordTableEnumerator.b("㝃⹅ⵇ⽉㡋", a_));
	}

	// Token: 0x06003ED9 RID: 16089 RVA: 0x00234BB8 File Offset: 0x00233BB8
	private void ᜅ(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 18;
		int num = 0;
		XlsPageSetup xlsPageSetup;
		for (;;)
		{
			switch (num)
			{
			case 1:
			{
				string localName;
				if (localName == RecordTableEnumerator.b("G⽉ⵋ⩍㕏⁑", a_))
				{
					num = 8;
					continue;
				}
				return;
			}
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_7B;
				default:
					if (false)
					{
					}
					xlsPageSetup.FooterMarginInch = XmlConvert.ToDouble(A_0.Value);
					num = 10;
					continue;
				}
				break;
			case 3:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("ే⭉㡋⽍", a_), RecordTableEnumerator.b("㵇㡉≋瑍⍏ㅑ㱓㍕㕗㭙⽛獝ൟୡݣᑥݧᥩͫ࡭ѯ影ᝳ᥵ᕷ䁹፻᡽늇ﺑ", a_)))
				{
					num = 6;
					continue;
				}
				return;
			case 4:
				goto IL_11C;
			case 5:
				goto IL_F9;
			case 6:
				xlsPageSetup.FullHeaderString = A_0.Value;
				num = 13;
				continue;
			case 7:
				num = 17;
				continue;
			case 8:
				num = 12;
				continue;
			case 9:
				xlsPageSetup.HeaderMarginInch = XmlConvert.ToDouble(A_0.Value);
				num = 4;
				continue;
			case 10:
				goto IL_BC;
			case 11:
				goto IL_7B;
			case 12:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("Շ⭉㹋⥍㥏㱑", a_), RecordTableEnumerator.b("㵇㡉≋瑍⍏ㅑ㱓㍕㕗㭙⽛獝ൟୡݣᑥݧᥩͫ࡭ѯ影ᝳ᥵ᕷ䁹፻᡽늇ﺑ", a_)))
				{
					num = 9;
					continue;
				}
				goto IL_11C;
			case 13:
				return;
			case 14:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("ే⭉㡋⽍", a_), RecordTableEnumerator.b("㵇㡉≋瑍⍏ㅑ㱓㍕㕗㭙⽛獝ൟୡݣᑥݧᥩͫ࡭ѯ影ᝳ᥵ᕷ䁹፻᡽늇ﺑ", a_)))
				{
					num = 5;
					continue;
				}
				return;
			case 15:
			{
				if (A_1 == null)
				{
					num = 16;
					continue;
				}
				string localName = A_0.LocalName;
				xlsPageSetup = (XlsPageSetup)A_1.PageSetup;
				num = 18;
				continue;
			}
			case 16:
				goto IL_177;
			case 17:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("Շ⭉㹋⥍㥏㱑", a_), RecordTableEnumerator.b("㵇㡉≋瑍⍏ㅑ㱓㍕㕗㭙⽛獝ൟୡݣᑥݧᥩͫ࡭ѯ影ᝳ᥵ᕷ䁹፻᡽늇ﺑ", a_)))
				{
					num = 2;
					continue;
				}
				goto IL_BC;
			case 18:
			{
				string localName;
				if (localName == RecordTableEnumerator.b("็╉⍋㩍㕏⁑", a_))
				{
					num = 7;
					continue;
				}
				num = 1;
				continue;
			}
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 11;
				continue;
			}
			num = 15;
			continue;
			IL_BC:
			num = 14;
			continue;
			IL_11C:
			num = 3;
		}
		IL_7B:
		throw new ArgumentNullException(RecordTableEnumerator.b("㩇⽉ⵋ⩍㕏⁑", a_));
		IL_F9:
		xlsPageSetup.FullFooterString = A_0.Value;
		return;
		IL_177:
		throw new ArgumentNullException(RecordTableEnumerator.b("㭇≉⥋⭍⑏", a_));
	}

	// Token: 0x06003EDA RID: 16090 RVA: 0x00234E94 File Offset: 0x00233E94
	private void ᜄ(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 12;
		int num = 13;
		for (;;)
		{
			IPageSetup pageSetup;
			switch (num)
			{
			case 0:
				pageSetup.CenterHorizontally = XmlConvert.ToBoolean(A_0.Value);
				num = 7;
				continue;
			case 1:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("Ł⅃⡅㱇⽉㹋ᡍ㕏⁑⁓㽕㭗㭙せ", a_), RecordTableEnumerator.b("㝁㙃⡅片㥉⽋♍㕏㽑㕓╕畗㝙㕛㵝቟ൡᝣ॥๧ṩ䅫൭Ὧά乳᥵ṷᱹᕻᵽ뢁ﺅ", a_)))
				{
					num = 5;
					continue;
				}
				return;
			case 2:
				if (true)
				{
				}
				goto IL_AB;
			case 3:
				goto IL_6C;
			case 4:
				pageSetup.Orientation = (PageOrientationType)Enum.Parse(typeof(PageOrientationType), A_0.Value, true);
				num = 3;
				continue;
			case 5:
				pageSetup.CenterVertically = XmlConvert.ToBoolean(A_0.Value);
				num = 9;
				continue;
			case 6:
				goto IL_122;
			case 7:
				goto IL_127;
			case 8:
				if (A_1 != null)
				{
					pageSetup = A_1.PageSetup;
					pageSetup.Orientation = PageOrientationType.Portrait;
					num = 14;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_283;
				default:
					if (false)
					{
					}
					num = 6;
					continue;
				}
				break;
			case 9:
				return;
			case 10:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("Ł⅃⡅㱇⽉㹋ٍ㽏⁑㵓ⱕ㝗㑙⡛㽝౟", a_), RecordTableEnumerator.b("㝁㙃⡅片㥉⽋♍㕏㽑㕓╕畗㝙㕛㵝቟ൡᝣ॥๧ṩ䅫൭Ὧά乳᥵ṷᱹᕻᵽ뢁ﺅ", a_)))
				{
					num = 0;
					continue;
				}
				goto IL_127;
			case 11:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("ു㙃⽅ⵇ⑉㡋⽍⑏㭑㭓㡕", a_), RecordTableEnumerator.b("㝁㙃⡅片㥉⽋♍㕏㽑㕓╕畗㝙㕛㵝቟ൡᝣ॥๧ṩ䅫൭Ὧά乳᥵ṷᱹᕻᵽ뢁ﺅ", a_)))
				{
					num = 4;
					continue;
				}
				goto IL_6C;
			case 12:
				goto IL_283;
			case 14:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("ᅁぃ❅㩇㹉᱋⽍㝏㝑ᩓ⍕㕗㡙㥛ⱝ", a_), RecordTableEnumerator.b("㝁㙃⡅片㥉⽋♍㕏㽑㕓╕畗㝙㕛㵝቟ൡᝣ॥๧ṩ䅫൭Ὧά乳᥵ṷᱹᕻᵽ뢁ﺅ", a_)))
				{
					num = 12;
					continue;
				}
				goto IL_AB;
			case 15:
				goto IL_67;
			}
			if (A_0 == null)
			{
				num = 15;
				continue;
			}
			num = 8;
			continue;
			IL_6C:
			num = 10;
			continue;
			IL_AB:
			num = 11;
			continue;
			IL_127:
			num = 1;
			continue;
			IL_283:
			pageSetup.AutoFirstPageNumber = false;
			pageSetup.FirstPageNumber = XmlConvert.ToInt32(A_0.Value);
			num = 2;
		}
		IL_67:
		throw new ArgumentNullException(RecordTableEnumerator.b("ぁ⅃❅ⱇ⽉㹋", a_));
		IL_122:
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅁⱃ⍅ⵇ㹉", a_));
	}

	// Token: 0x06003EDB RID: 16091 RVA: 0x0023512C File Offset: 0x0023412C
	private void ᜃ(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 5;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("椺吼堾⥀㝂", a_), RecordTableEnumerator.b("为似儾筀あ♄⽆ⱈ♊ⱌ㱎籐㹒㱔㑖⭘㑚⹜ぞݠᝢ䡤Ѧ٨٪坬nᝰᕲᱴᑶᱸ䅺᡼ݾ", a_)))
				{
					num = 10;
					continue;
				}
				goto IL_AB;
			case 2:
			{
				XlsPageSetup xlsPageSetup;
				xlsPageSetup.TopMargin = XmlConvert.ToDouble(A_0.Value);
				num = 3;
				continue;
			}
			case 3:
				return;
			case 4:
				goto IL_12A;
			case 5:
				goto IL_AB;
			case 6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_135;
				default:
					if (false)
					{
					}
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("眺堼夾㕀", a_), RecordTableEnumerator.b("为似儾筀あ♄⽆ⱈ♊ⱌ㱎籐㹒㱔㑖⭘㑚⹜ぞݠᝢ䡤Ѧ٨٪坬nᝰᕲᱴᑶᱸ䅺᡼ݾ", a_)))
					{
						num = 9;
						continue;
					}
					goto IL_6C;
				}
				break;
			case 7:
			{
				XlsPageSetup xlsPageSetup;
				xlsPageSetup.BottomMargin = XmlConvert.ToDouble(A_0.Value);
				num = 4;
				continue;
			}
			case 8:
				goto IL_125;
			case 9:
			{
				XlsPageSetup xlsPageSetup;
				xlsPageSetup.LeftMargin = XmlConvert.ToDouble(A_0.Value);
				num = 13;
				continue;
			}
			case 10:
			{
				if (true)
				{
				}
				XlsPageSetup xlsPageSetup;
				xlsPageSetup.RightMargin = XmlConvert.ToDouble(A_0.Value);
				num = 5;
				continue;
			}
			case 11:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("示刼䬾㕀ⱂ⡄", a_), RecordTableEnumerator.b("为似儾筀あ♄⽆ⱈ♊ⱌ㱎籐㹒㱔㑖⭘㑚⹜ぞݠᝢ䡤Ѧ٨٪坬nᝰᕲᱴᑶᱸ䅺᡼ݾ", a_)))
				{
					num = 7;
					continue;
				}
				goto IL_12A;
			case 12:
			{
				if (A_1 == null)
				{
					num = 8;
					continue;
				}
				XlsPageSetup xlsPageSetup = (XlsPageSetup)A_1.PageSetup;
				num = 1;
				continue;
			}
			case 13:
				goto IL_6C;
			case 14:
				goto IL_135;
			case 15:
				goto IL_67;
			}
			if (A_0 == null)
			{
				num = 15;
				continue;
			}
			num = 12;
			continue;
			IL_6C:
			num = 11;
			continue;
			IL_AB:
			num = 6;
			continue;
			IL_12A:
			num = 14;
			continue;
			IL_135:
			if (!A_0.MoveToAttribute(RecordTableEnumerator.b("漺刼伾", a_), RecordTableEnumerator.b("为似儾筀あ♄⽆ⱈ♊ⱌ㱎籐㹒㱔㑖⭘㑚⹜ぞݠᝢ䡤Ѧ٨٪坬nᝰᕲᱴᑶᱸ䅺᡼ݾ", a_)))
			{
				return;
			}
			num = 2;
		}
		IL_67:
		throw new ArgumentNullException(RecordTableEnumerator.b("䤺堼帾╀♂㝄", a_));
		IL_125:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠺唼娾⑀㝂", a_));
	}

	// Token: 0x06003EDC RID: 16092 RVA: 0x002353AC File Offset: 0x002343AC
	private void ᜂ(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 17;
		int num = 8;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_60;
			case 1:
				if (A_1 == null)
				{
					num = 11;
					continue;
				}
				if (true)
				{
				}
				A_0.MoveToElement();
				num = 9;
				continue;
			case 2:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("㉆㭈╊睌㱎㉐㭒ご㩖㡘⡚灜㉞ࡠbᝤࡦᩨѪ୬᭮屰ၲᩴ᩶䍸ᑺ᭼᥾붆﶐", a_))
				{
					num = 4;
					continue;
				}
				goto IL_C1;
			case 3:
				goto IL_C1;
			case 4:
				this.ᜁ(A_0, A_1);
				num = 3;
				continue;
			case 5:
				goto IL_FD;
			case 6:
				goto IL_1C6;
			case 7:
				goto IL_FD;
			case 9:
				if (A_0.IsEmptyElement)
				{
					num = 6;
					continue;
				}
				A_0.Read();
				num = 7;
				continue;
			case 10:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 13;
					continue;
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
					num = 14;
					continue;
				}
				break;
			case 11:
				goto IL_BF;
			case 12:
				num = 2;
				continue;
			case 13:
				return;
			case 14:
				if (A_0.LocalName == RecordTableEnumerator.b("ᝆ⡈╊⡌", a_))
				{
					num = 12;
					continue;
				}
				goto IL_C1;
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			num = 1;
			continue;
			IL_C1:
			A_0.Skip();
			num = 5;
			continue;
			IL_FD:
			num = 10;
		}
		IL_60:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕆ⱈ⩊⥌⩎⍐", a_));
		IL_BF:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑆ⅈ⹊⡌㭎", a_));
		IL_1C6:;
	}

	// Token: 0x06003EDD RID: 16093 RVA: 0x00235584 File Offset: 0x00234584
	private void ᜁ(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 3;
		int num = 1;
		spr\u21A4 spr_u21A;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.LocalName == RecordTableEnumerator.b("眸为值崾⑀ㅂ", a_))
				{
					num = 13;
					continue;
				}
				goto IL_BE;
			case 2:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("䰸䤺匼Ծ㉀⁂ⵄ≆⑈⩊㹌扎㱐㩒㙔╖㙘⡚㉜㥞ᕠ乢٤ࡦѨ兪ɬ८ᝰᩲᙴቶ䍸Ṻռ᱾", a_))
				{
					num = 22;
					continue;
				}
				goto IL_BE;
			case 3:
				if (A_0.LocalName == RecordTableEnumerator.b("砸堺䤼嘾㝀♂ᝄ⡆㹈", a_))
				{
					num = 24;
					continue;
				}
				goto IL_1A8;
			case 4:
				if (A_0.IsEmptyElement)
				{
					num = 18;
					continue;
				}
				A_0.Read();
				spr_u21A = (spr\u21A4)spr\u175E.ᜀ(TBIFFRecord.Selection);
				num = 16;
				continue;
			case 5:
				goto IL_170;
			case 6:
				if (A_1 == null)
				{
					num = 5;
					continue;
				}
				A_0.MoveToElement();
				num = 4;
				continue;
			case 7:
				A_0.Read();
				spr_u21A.ᜀ(XmlConvert.ToUInt16(A_0.Value));
				A_0.Skip();
				num = 10;
				continue;
			case 8:
				if (A_0.LocalName == RecordTableEnumerator.b("砸堺䤼嘾㝀♂ل⡆╈", a_))
				{
					num = 9;
					continue;
				}
				goto IL_175;
			case 9:
				num = 19;
				continue;
			case 10:
				goto IL_175;
			case 11:
				goto IL_270;
			case 12:
				goto IL_1A8;
			case 13:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_BE;
				default:
					if (false)
					{
					}
					num = 2;
					continue;
				}
				break;
			case 14:
				A_0.Read();
				spr_u21A.ᜂ(XmlConvert.ToUInt16(A_0.Value));
				A_0.Skip();
				num = 12;
				continue;
			case 15:
				goto IL_8B;
			case 16:
				goto IL_24D;
			case 17:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("䰸䤺匼Ծ㉀⁂ⵄ≆⑈⩊㹌扎㱐㩒㙔╖㙘⡚㉜㥞ᕠ乢٤ࡦѨ兪ɬ८ᝰᩲᙴቶ䍸Ṻռ᱾", a_))
				{
					num = 14;
					continue;
				}
				goto IL_1A8;
			case 18:
				goto IL_11F;
			case 19:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("䰸䤺匼Ծ㉀⁂ⵄ≆⑈⩊㹌扎㱐㩒㙔╖㙘⡚㉜㥞ᕠ乢٤ࡦѨ兪ɬ८ᝰᩲᙴቶ䍸Ṻռ᱾", a_))
				{
					num = 7;
					continue;
				}
				goto IL_175;
			case 20:
				goto IL_BE;
			case 21:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 11;
					continue;
				}
				num = 0;
				continue;
			case 22:
				A_0.Read();
				spr_u21A.ᜀ(XmlConvert.ToByte(A_0.Value));
				A_0.Skip();
				num = 20;
				continue;
			case 23:
				goto IL_24D;
			case 24:
				num = 17;
				continue;
			}
			if (A_0 == null)
			{
				num = 15;
				continue;
			}
			num = 6;
			continue;
			IL_BE:
			num = 8;
			continue;
			IL_175:
			num = 3;
			continue;
			IL_1A8:
			A_0.Skip();
			num = 23;
			continue;
			IL_24D:
			num = 21;
		}
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬸帺尼嬾⑀ㅂ", a_));
		IL_11F:
		if (true)
		{
		}
		return;
		IL_170:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨸区堼娾㕀", a_));
		IL_270:
		spr_u21A.ᜀ(new spr\u21A4.ᜀ[]
		{
			new spr\u21A4.ᜀ(spr_u21A.ᜂ(), spr_u21A.ᜂ(), (byte)spr_u21A.ᜁ(), (byte)spr_u21A.ᜁ())
		});
		A_1.Selections.Add(spr_u21A);
	}

	// Token: 0x06003EDE RID: 16094 RVA: 0x00235944 File Offset: 0x00234944
	private void ᜀ(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 3;
		int num = 49;
		for (;;)
		{
			XlsPageSetup xlsPageSetup;
			switch (num)
			{
			case 0:
				if (A_0.LocalName == RecordTableEnumerator.b("缸刺䤼栾⡀❂ㅄ⽆", a_))
				{
					num = 47;
					continue;
				}
				goto IL_7BE;
			case 1:
			{
				A_0.Read();
				int num2 = Array.IndexOf<string>(sprỉ.\u17ED, A_0.Value);
				num = 5;
				continue;
			}
			case 2:
				xlsPageSetup.IsPrintGridlines = true;
				num = 69;
				continue;
			case 3:
				goto IL_2F7;
			case 4:
				if (A_0.LocalName == RecordTableEnumerator.b("眸为值崾⑀ㅂ⩄ⅆੈ⑊㵌♎㑐⁒", a_))
				{
					num = 77;
					continue;
				}
				goto IL_3AA;
			case 5:
			{
				int num2;
				if (num2 != -1)
				{
					num = 37;
					continue;
				}
				goto IL_82E;
			}
			case 6:
				if (A_0.IsEmptyElement)
				{
					num = 31;
					continue;
				}
				A_0.Read();
				xlsPageSetup = (XlsPageSetup)A_1.PageSetup;
				xlsPageSetup.PaperSize = PaperSizeType.PaperLetter;
				num = 24;
				continue;
			case 7:
				num = 45;
				continue;
			case 8:
				goto IL_8B1;
			case 9:
				if (A_0.LocalName == RecordTableEnumerator.b("甸帺嬼䬾ᕀⱂᝄ⹆⹈⍊㥌", a_))
				{
					num = 30;
					continue;
				}
				goto IL_347;
			case 10:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("䰸䤺匼Ծ㉀⁂ⵄ≆⑈⩊㹌扎㱐㩒㙔╖㙘⡚㉜㥞ᕠ乢٤ࡦѨ兪ɬ८ᝰᩲᙴቶ䍸Ṻռ᱾", a_))
				{
					num = 42;
					continue;
				}
				goto IL_87B;
			case 11:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("䰸䤺匼Ծ㉀⁂ⵄ≆⑈⩊㹌扎㱐㩒㙔╖㙘⡚㉜㥞ᕠ乢٤ࡦѨ兪ɬ८ᝰᩲᙴቶ䍸Ṻռ᱾", a_))
				{
					num = 12;
					continue;
				}
				goto IL_35D;
			case 12:
				goto IL_47F;
			case 13:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("䰸䤺匼Ծ㉀⁂ⵄ≆⑈⩊㹌扎㱐㩒㙔╖㙘⡚㉜㥞ᕠ乢٤ࡦѨ兪ɬ८ᝰᩲᙴቶ䍸Ṻռ᱾", a_))
				{
					num = 1;
					continue;
				}
				goto IL_1FF;
			case 14:
			{
				int num3;
				xlsPageSetup.PrintErrors = (PrintErrorsType)num3;
				num = 74;
				continue;
			}
			case 15:
				num = 43;
				continue;
			case 16:
				goto IL_82E;
			case 17:
				if (A_0.LocalName == RecordTableEnumerator.b("稸吺值刾⑀ⵂㅄ㑆Ո⩊㑌⁎⑐❒", a_))
				{
					num = 22;
					continue;
				}
				goto IL_1FF;
			case 18:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("䰸䤺匼Ծ㉀⁂ⵄ≆⑈⩊㹌扎㱐㩒㙔╖㙘⡚㉜㥞ᕠ乢٤ࡦѨ兪ɬ८ᝰᩲᙴቶ䍸Ṻռ᱾", a_))
				{
					num = 73;
					continue;
				}
				goto IL_A57;
			case 19:
				goto IL_87B;
			case 20:
				A_0.Read();
				xlsPageSetup.FitToPagesWide = XmlConvert.ToInt32(A_0.Value);
				A_0.Skip();
				num = 58;
				continue;
			case 21:
				goto IL_35D;
			case 22:
				num = 13;
				continue;
			case 23:
				xlsPageSetup.Draft = true;
				num = 72;
				continue;
			case 24:
				goto IL_993;
			case 25:
				A_1.PageSetup.Order = OrderType.OverThenDown;
				num = 76;
				continue;
			case 26:
				goto IL_993;
			case 27:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("䰸䤺匼Ծ㉀⁂ⵄ≆⑈⩊㹌扎㱐㩒㙔╖㙘⡚㉜㥞ᕠ乢٤ࡦѨ兪ɬ८ᝰᩲᙴቶ䍸Ṻռ᱾", a_))
				{
					num = 68;
					continue;
				}
				goto IL_3AA;
			case 28:
				num = 44;
				continue;
			case 29:
				goto IL_2AA;
			case 30:
				num = 48;
				continue;
			case 31:
				return;
			case 32:
				if (A_1 == null)
				{
					num = 39;
					continue;
				}
				A_0.MoveToElement();
				num = 6;
				continue;
			case 33:
				if (A_0.LocalName == RecordTableEnumerator.b("焸吺似嘾㭀ⱂ⭄㍆⡈❊Ὄ⩎≐㱒㥔≖ⵘ㉚㉜ㅞ", a_))
				{
					num = 28;
					continue;
				}
				goto IL_2F7;
			case 34:
				num = 11;
				continue;
			case 35:
				A_0.Read();
				xlsPageSetup.PaperSize = (PaperSizeType)XmlConvert.ToInt16(A_0.Value);
				A_0.Skip();
				num = 71;
				continue;
			case 36:
				if (A_0.LocalName == RecordTableEnumerator.b("紸䤺尼夾㕀ቂい♆╈≊㥌㙎", a_))
				{
					num = 53;
					continue;
				}
				goto IL_9BB;
			case 37:
			{
				int num2;
				xlsPageSetup.PrintComments = (PrintCommentType)num2;
				num = 16;
				continue;
			}
			case 38:
				if (A_0.LocalName == RecordTableEnumerator.b("椸䤺吼儾㕀ق㝄㕆♈㥊㹌", a_))
				{
					num = 60;
					continue;
				}
				goto IL_2AA;
			case 39:
				goto IL_69D;
			case 40:
				if (A_0.LocalName == RecordTableEnumerator.b("笸场尼尾⩀ɂ⭄⍆Ṉ⍊⑌㭎㑐", a_))
				{
					num = 80;
					continue;
				}
				goto IL_A57;
			case 41:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("䰸䤺匼Ծ㉀⁂ⵄ≆⑈⩊㹌扎㱐㩒㙔╖㙘⡚㉜㥞ᕠ乢٤ࡦѨ兪ɬ८ᝰᩲᙴቶ䍸Ṻռ᱾", a_))
				{
					num = 51;
					continue;
				}
				goto IL_2AA;
			case 42:
				A_0.Read();
				xlsPageSetup.Zoom = XmlConvert.ToInt32(A_0.Value);
				A_0.Skip();
				num = 19;
				continue;
			case 43:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("䰸䤺匼Ծ㉀⁂ⵄ≆⑈⩊㹌扎㱐㩒㙔╖㙘⡚㉜㥞ᕠ乢٤ࡦѨ兪ɬ८ᝰᩲᙴቶ䍸Ṻռ᱾", a_))
				{
					num = 2;
					continue;
				}
				goto IL_62C;
			case 44:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("䰸䤺匼Ծ㉀⁂ⵄ≆⑈⩊㹌扎㱐㩒㙔╖㙘⡚㉜㥞ᕠ乢٤ࡦѨ兪ɬ८ᝰᩲᙴቶ䍸Ṻռ᱾", a_))
				{
					num = 66;
					continue;
				}
				goto IL_2F7;
			case 45:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("䰸䤺匼Ծ㉀⁂ⵄ≆⑈⩊㹌扎㱐㩒㙔╖㙘⡚㉜㥞ᕠ乢٤ࡦѨ兪ɬ८ᝰᩲᙴቶ䍸Ṻռ᱾", a_))
				{
					num = 35;
					continue;
				}
				goto IL_59A;
			case 46:
				goto IL_A57;
			case 47:
				num = 57;
				continue;
			case 48:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("䰸䤺匼Ծ㉀⁂ⵄ≆⑈⩊㹌扎㱐㩒㙔╖㙘⡚㉜㥞ᕠ乢٤ࡦѨ兪ɬ८ᝰᩲᙴቶ䍸Ṻռ᱾", a_))
				{
					num = 25;
					continue;
				}
				goto IL_347;
			case 50:
				goto IL_3AA;
			case 51:
			{
				A_0.Read();
				int num3 = Array.IndexOf<string>(sprỉ.\u17EE, A_0.Value);
				num = 52;
				continue;
			}
			case 52:
			{
				int num3;
				if (num3 != -1)
				{
					num = 14;
					continue;
				}
				goto IL_452;
			}
			case 53:
				num = 75;
				continue;
			case 54:
				goto IL_171;
			case 55:
				if (A_0.LocalName == RecordTableEnumerator.b("缸刺䤼眾⑀⩂≄⽆㵈", a_))
				{
					num = 34;
					continue;
				}
				goto IL_35D;
			case 56:
				num = 10;
				continue;
			case 57:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("䰸䤺匼Ծ㉀⁂ⵄ≆⑈⩊㹌扎㱐㩒㙔╖㙘⡚㉜㥞ᕠ乢٤ࡦѨ兪ɬ८ᝰᩲᙴቶ䍸Ṻռ᱾", a_))
				{
					num = 20;
					continue;
				}
				goto IL_7BE;
			case 58:
				goto IL_7BE;
			case 59:
				return;
			case 60:
				num = 41;
				continue;
			case 61:
				num = 64;
				continue;
			case 62:
				if (A_0.LocalName == RecordTableEnumerator.b("欸吺䨼簾⹀⽂ൄ≆⡈⽊⑌ⅎ㙐⁒", a_))
				{
					num = 61;
					continue;
				}
				goto IL_8B1;
			case 63:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 59;
					continue;
				}
				num = 4;
				continue;
			case 64:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("䰸䤺匼Ծ㉀⁂ⵄ≆⑈⩊㹌扎㱐㩒㙔╖㙘⡚㉜㥞ᕠ乢٤ࡦѨ兪ɬ८ᝰᩲᙴቶ䍸Ṻռ᱾", a_))
				{
					num = 67;
					continue;
				}
				goto IL_8B1;
			case 65:
				if (A_0.LocalName == RecordTableEnumerator.b("樸堺尼匾⑀", a_))
				{
					num = 56;
					continue;
				}
				goto IL_87B;
			case 66:
				A_0.Read();
				xlsPageSetup.PrintQuality = XmlConvert.ToInt32(A_0.Value);
				A_0.Skip();
				num = 3;
				continue;
			case 67:
				xlsPageSetup.IsPrintGridlines = true;
				num = 8;
				continue;
			case 68:
				A_0.Read();
				xlsPageSetup.Copies = XmlConvert.ToInt32(A_0.Value);
				A_0.Read();
				if (true)
				{
				}
				num = 50;
				continue;
			case 69:
				goto IL_62C;
			case 70:
				goto IL_1FF;
			case 71:
				goto IL_59A;
			case 72:
				goto IL_9BB;
			case 73:
				xlsPageSetup.BlackAndWhite = true;
				num = 46;
				continue;
			case 74:
				goto IL_452;
			case 75:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("䰸䤺匼Ծ㉀⁂ⵄ≆⑈⩊㹌扎㱐㩒㙔╖㙘⡚㉜㥞ᕠ乢٤ࡦѨ兪ɬ८ᝰᩲᙴቶ䍸Ṻռ᱾", a_))
				{
					num = 23;
					continue;
				}
				goto IL_9BB;
			case 76:
				goto IL_347;
			case 77:
				num = 27;
				continue;
			case 78:
				if (A_0.LocalName == RecordTableEnumerator.b("縸䤺吼嬾ⵀ⩂⭄≆㩈", a_))
				{
					num = 15;
					continue;
				}
				goto IL_62C;
			case 79:
				if (A_0.LocalName == RecordTableEnumerator.b("椸娺䴼娾㍀၂ⱄ㵆ⱈɊ⍌⭎㑐⭒", a_))
				{
					num = 7;
					continue;
				}
				goto IL_59A;
			case 80:
				num = 18;
				continue;
			}
			if (A_0 == null)
			{
				num = 54;
				continue;
			}
			num = 32;
			continue;
			IL_1FF:
			num = 38;
			continue;
			IL_2AA:
			num = 9;
			continue;
			IL_2F7:
			num = 79;
			continue;
			IL_347:
			A_0.Skip();
			num = 26;
			continue;
			IL_35D:
			num = 65;
			continue;
			IL_3AA:
			num = 33;
			continue;
			IL_452:
			A_0.Skip();
			num = 29;
			continue;
			IL_47F:
			A_0.Read();
			xlsPageSetup.FitToPagesTall = XmlConvert.ToInt32(A_0.Value);
			A_0.Skip();
			num = 21;
			continue;
			IL_62C:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_47F;
			default:
				if (false)
				{
				}
				num = 40;
				continue;
			}
			IL_59A:
			num = 0;
			continue;
			IL_7BE:
			num = 55;
			continue;
			IL_82E:
			A_0.Skip();
			num = 70;
			continue;
			IL_87B:
			num = 78;
			continue;
			IL_8B1:
			num = 17;
			continue;
			IL_993:
			num = 63;
			continue;
			IL_9BB:
			num = 62;
			continue;
			IL_A57:
			num = 36;
		}
		IL_171:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬸帺尼嬾⑀ㅂ", a_));
		IL_69D:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨸区堼娾㕀", a_));
	}

	// Token: 0x06003EDF RID: 16095 RVA: 0x002363E0 File Offset: 0x002353E0
	private void ᜁ(XmlReader A_0, INameRanges A_1, int A_2)
	{
		int a_ = 14;
		int num = 14;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_15D;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_150;
				default:
					goto IL_8D;
				}
				break;
			case 2:
				goto IL_15B;
			case 3:
				num = 17;
				continue;
			case 4:
				if (A_1 == null)
				{
					goto IL_150;
				}
				num = 18;
				continue;
			case 5:
				goto IL_98;
			case 6:
				this.ᜀ(A_0, A_1, A_2);
				num = 5;
				continue;
			case 7:
			{
				INamedRange namedRange;
				if (namedRange != null)
				{
					num = 15;
					continue;
				}
				return;
			}
			case 8:
			{
				IWorksheet parentWorksheet;
				if (parentWorksheet != null)
				{
					num = 11;
					continue;
				}
				return;
			}
			case 9:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 10;
					continue;
				}
				num = 16;
				continue;
			case 10:
			{
				INamedRange namedRange = A_1[RecordTableEnumerator.b("ᭃEⅇ♉㡋⭍≏ᙑ㕓≕㥗㡙㵛ⵝ՟", a_)];
				IWorksheet parentWorksheet = A_1.ParentWorksheet;
				num = 7;
				continue;
			}
			case 11:
			{
				if (true)
				{
				}
				INamedRange namedRange;
				IWorksheet parentWorksheet;
				parentWorksheet.AutoFilters.Range = namedRange.RefersToRange;
				num = 19;
				continue;
			}
			case 12:
				return;
			case 13:
				goto IL_15D;
			case 15:
				num = 8;
				continue;
			case 16:
				if (A_0.LocalName == RecordTableEnumerator.b("੃❅╇⽉⡋ᱍㅏ㱑㍓㍕", a_))
				{
					num = 3;
					continue;
				}
				goto IL_98;
			case 17:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("ㅃ㑅♇灉㽋ⵍ㡏㝑㥓㝕⭗睙ㅛ㝝͟ၡୣᕥݧ౩ᡫ䍭፯ᵱᥳ䱵᝷ᱹ᩻᝽뺃ﲓﶗ", a_))
				{
					num = 6;
					continue;
				}
				goto IL_98;
			case 18:
				if (A_0.IsEmptyElement)
				{
					num = 12;
					continue;
				}
				A_0.Read();
				num = 0;
				continue;
			case 19:
				goto IL_19E;
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			num = 4;
			continue;
			IL_98:
			A_0.Skip();
			num = 13;
			continue;
			IL_150:
			num = 2;
			continue;
			IL_15D:
			num = 9;
		}
		IL_8D:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㙃⍅⥇⹉⥋㱍", a_));
		IL_15B:
		throw new ArgumentNullException(RecordTableEnumerator.b("⩃❅╇⽉㽋്㽏㹑㡓", a_));
		IL_19E:;
	}

	// Token: 0x06003EE0 RID: 16096 RVA: 0x00236654 File Offset: 0x00235654
	private void ᜀ(XmlReader A_0, INameRanges A_1, int A_2)
	{
		int a_ = 7;
		int num = 11;
		string text;
		XlsName xlsName;
		for (;;)
		{
			sprῚ sprῚ;
			switch (num)
			{
			case 0:
				goto IL_AC;
			case 1:
				if (text[0] == '=')
				{
					num = 7;
					continue;
				}
				goto IL_22D;
			case 2:
				goto IL_54;
			case 3:
				goto IL_214;
			case 4:
				sprῚ.ᜂ(XmlConvert.ToBoolean(A_0.Value));
				num = 5;
				continue;
			case 5:
				goto IL_B1;
			case 6:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("甼嘾╀❂⁄⥆", a_), RecordTableEnumerator.b("䠼䴾⽀祂㙄⑆ⅈ⹊⁌⹎≐繒㡔㹖㩘⥚㉜ⱞ๠բᅤ䩦੨Ѫl啮Ṱᕲ፴Ṷ᩸Ṻ䝼౾", a_)))
				{
					num = 4;
					continue;
				}
				goto IL_B1;
			case 7:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_7C;
				}
				if (false)
				{
				}
				text = A_0.Value.Substring(1);
				num = 9;
				continue;
			case 8:
				if (A_2 < 0)
				{
					num = 3;
					continue;
				}
				sprῚ = (sprῚ)spr\u175E.ᜀ(TBIFFRecord.Name);
				num = 6;
				continue;
			case 9:
				goto IL_1F8;
			case 10:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				num = 8;
				continue;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			num = 10;
			continue;
			IL_B1:
			A_0.MoveToAttribute(RecordTableEnumerator.b("猼帾ⱀ♂", a_), RecordTableEnumerator.b("䠼䴾⽀祂㙄⑆ⅈ⹊⁌⹎≐繒㡔㹖㩘⥚㉜ⱞ๠բᅤ䩦੨Ѫl啮Ṱᕲ፴Ṷ᩸Ṻ䝼౾", a_));
			sprῚ.ᜆ(A_0.Value);
			A_0.MoveToAttribute(RecordTableEnumerator.b("漼娾❀♂㝄㑆ᵈ⑊", a_), RecordTableEnumerator.b("䠼䴾⽀祂㙄⑆ⅈ⹊⁌⹎≐繒㡔㹖㩘⥚㉜ⱞ๠բᅤ䩦੨Ѫl啮Ṱᕲ፴Ṷ᩸Ṻ䝼౾", a_));
			this.\u1719.Add(A_0.Value);
			sprῚ.ᜀ((ushort)A_2);
			xlsName = new XlsName(base.ReservedHandle, A_1, sprῚ);
			text = A_0.Value;
			num = 1;
		}
		IL_54:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("似娾⁀❂⁄㕆", a_));
		IL_7C:
		throw new ArgumentNullException(RecordTableEnumerator.b("丼圾⑀♂ㅄๆ❈⽊⡌㝎", a_));
		IL_AC:
		throw new ArgumentNullException(RecordTableEnumerator.b("匼帾ⱀ♂㙄ц♈❊⅌", a_));
		IL_1F8:
		goto IL_22D;
		IL_214:
		goto IL_7C;
		IL_22D:
		xlsName.ᜀ(this.\u171B.ᜀ(text, null, null, 0, 0, true));
		A_1.Add(xlsName);
		A_0.MoveToElement();
	}

	// Token: 0x06003EE1 RID: 16097 RVA: 0x002368B4 File Offset: 0x002358B4
	private void ᜀ(XmlReader A_0, XlsComment A_1, int A_2)
	{
		int a_ = 9;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_116;
			case 1:
				if (A_0.LocalName == RecordTableEnumerator.b("笾⁀㝂⑄", a_))
				{
					num = 15;
					continue;
				}
				goto IL_1D0;
			case 2:
				goto IL_292;
			case 3:
				goto IL_12C;
			case 4:
				return;
			case 5:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("績㑀㝂ⵄ⡆㭈", a_), RecordTableEnumerator.b("䨾㍀ⵂ罄㑆⩈⍊⡌≎ぐ⁒硔㩖じ㡚⽜ぞበౢͤ፦䑨ࡪɬɮ䭰ᱲ፴ᅶၸ᡺᡼䕾ﺌ", a_)))
				{
					num = 11;
					continue;
				}
				goto IL_253;
			case 7:
				goto IL_83;
			case 8:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("氾⥀ⱂ㉄ن╈㱊ⱌ㙎≐", a_), RecordTableEnumerator.b("䨾㍀ⵂ罄㑆⩈⍊⡌≎ぐ⁒硔㩖じ㡚⽜ぞበౢͤ፦䑨ࡪɬɮ䭰ᱲ፴ᅶၸ᡺᡼䕾ﺌ", a_)))
				{
					num = 20;
					continue;
				}
				goto IL_292;
			case 9:
				goto IL_1D0;
			case 10:
			{
				spr\u223A a_2 = ((RichTextString)A_1.RichText).TextObject;
				this.ᜁ(A_0, A_2, a_2);
				num = 9;
				continue;
			}
			case 11:
				A_1.Author = A_0.Value;
				num = 17;
				continue;
			case 12:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("䨾㍀ⵂ罄㑆⩈⍊⡌≎ぐ⁒硔㩖じ㡚⽜ぞበౢͤ፦䑨ࡪɬɮ䭰ᱲ፴ᅶၸ᡺᡼䕾ﺌ", a_))
				{
					num = 10;
					continue;
				}
				goto IL_1D0;
			case 13:
				goto IL_12C;
			case 14:
				return;
			case 15:
				num = 12;
				continue;
			case 16:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1D0;
				default:
					if (false)
					{
					}
					num = 5;
					continue;
				}
				break;
			case 17:
				goto IL_253;
			case 18:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 4;
					continue;
				}
				num = 1;
				continue;
			case 19:
				if (A_0.IsEmptyElement)
				{
					num = 14;
					continue;
				}
				A_0.Read();
				num = 3;
				continue;
			case 20:
				A_1.IsVisible = XmlConvert.ToBoolean(A_0.Value);
				num = 2;
				continue;
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 7;
				continue;
			}
			num = 16;
			continue;
			IL_12C:
			num = 18;
			continue;
			IL_1D0:
			A_0.Skip();
			num = 13;
			continue;
			IL_253:
			num = 8;
			continue;
			IL_292:
			A_1.ShapeType = ExcelShapeType.Comment;
			A_0.MoveToElement();
			num = 19;
		}
		IL_83:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴾⑀≂⅄≆㭈", a_));
		IL_116:
		throw new ArgumentNullException(RecordTableEnumerator.b("尾⹀⹂⡄≆❈㽊", a_));
	}

	// Token: 0x06003EE2 RID: 16098 RVA: 0x00236B88 File Offset: 0x00235B88
	private void ᜁ(XmlReader A_0, int A_1, spr\u223A A_2)
	{
		int a_ = 13;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				return;
			case 2:
				goto IL_3E;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_86;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					if (A_0.IsEmptyElement)
					{
						num = 1;
						continue;
					}
					goto IL_9A;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 2;
			}
			else
			{
				A_0.MoveToElement();
				num = 3;
			}
		}
		IL_3E:
		IL_86:
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄♆ⵈ⹊㽌", a_));
		IL_9A:
		this.ᜀ(A_0, A_1, A_2);
	}

	// Token: 0x06003EE3 RID: 16099 RVA: 0x00236C3C File Offset: 0x00235C3C
	public void ᜄ(XmlReader A_0, XlsWorkbook A_1)
	{
		int a_ = 11;
		int num = 17;
		for (;;)
		{
			bool flag;
			bool flag2;
			bool flag3;
			bool flag4;
			bool flag5;
			bool flag6;
			switch (num)
			{
			case 0:
				flag = true;
				goto IL_325;
			case 1:
				goto IL_44B;
			case 2:
				flag2 = (A_0.NodeType != XmlNodeType.ProcessingInstruction);
				goto IL_34A;
			case 3:
				goto IL_3DC;
			case 4:
				goto IL_336;
			case 5:
				if (!flag3)
				{
					num = 18;
					continue;
				}
				num = 26;
				continue;
			case 6:
				goto IL_44B;
			case 7:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 25;
					continue;
				}
				num = 30;
				continue;
			case 8:
				flag4 = !(XmlConvert.DecodeName(A_0.Value) == RecordTableEnumerator.b("ㅀㅂ⩄⁆⁈⽊灌济ᑐ⭒㙔㉖㕘畚๜㝞Ѡ٢ᅤ䕦", a_));
				goto IL_28C;
			case 9:
				if (!A_0.EOF)
				{
					num = 41;
					continue;
				}
				flag3 = true;
				num = 32;
				continue;
			case 10:
				if (!flag3)
				{
					num = 33;
					continue;
				}
				num = 36;
				continue;
			case 11:
				flag5 = true;
				goto IL_493;
			case 12:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㝀♂㝄㑆⁈⑊⍌", a_)))
				{
					num = 27;
					continue;
				}
				goto IL_44B;
			case 13:
				goto IL_3F8;
			case 14:
				goto IL_3DC;
			case 15:
				flag5 = !(A_0.Name == RecordTableEnumerator.b("㥀⹂⥄", a_));
				goto IL_493;
			case 16:
				goto IL_F1;
			case 18:
				num = 21;
				continue;
			case 19:
				goto IL_1EF;
			case 20:
				if (A_0.NodeType == XmlNodeType.Whitespace)
				{
					num = 4;
					continue;
				}
				goto IL_3A4;
			case 21:
				flag6 = !(A_0.Name == RecordTableEnumerator.b("ⱀあ⩄橆⡈㭊㵌⍎㡐げ㑔⍖じ㑚㍜", a_));
				goto IL_3FD;
			case 22:
				num = 23;
				continue;
			case 23:
				flag = (A_0.LocalName != RecordTableEnumerator.b("ᙀⱂ㝄ⱆ⭈⑊≌⑎", a_));
				goto IL_325;
			case 24:
				if (!flag3)
				{
					num = 22;
					continue;
				}
				num = 0;
				continue;
			case 25:
				num = 24;
				continue;
			case 26:
				flag6 = true;
				goto IL_3FD;
			case 27:
				flag3 |= !(XmlConvert.DecodeName(A_0.Value) == RecordTableEnumerator.b("灀浂畄", a_));
				num = 1;
				continue;
			case 28:
				if (flag3)
				{
					num = 13;
					continue;
				}
				goto IL_4C5;
			case 29:
				if (A_1 == null)
				{
					num = 19;
					continue;
				}
				flag3 = false;
				num = 35;
				continue;
			case 30:
				if (!flag3)
				{
					num = 38;
					continue;
				}
				num = 31;
				continue;
			case 31:
				flag2 = true;
				goto IL_34A;
			case 32:
				goto IL_3DC;
			case 33:
				num = 8;
				continue;
			case 34:
				num = 15;
				continue;
			case 35:
				if (!A_0.EOF)
				{
					num = 39;
					continue;
				}
				flag3 = true;
				num = 6;
				continue;
			case 36:
				flag4 = true;
				goto IL_28C;
			case 37:
				if (!flag3)
				{
					num = 34;
					continue;
				}
				num = 11;
				continue;
			case 38:
				num = 2;
				continue;
			case 39:
				A_0.Read();
				flag3 = (A_0.NodeType != XmlNodeType.XmlDeclaration);
				num = 37;
				continue;
			case 40:
				goto IL_3A4;
			case 41:
				A_0.Read();
				num = 20;
				continue;
			}
			if (A_0 != null)
			{
				if (true)
				{
				}
				num = 29;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_336;
			default:
				if (false)
				{
				}
				num = 16;
				continue;
			}
			IL_28C:
			flag3 = flag4;
			num = 14;
			continue;
			IL_325:
			flag3 = flag;
			num = 3;
			continue;
			IL_336:
			A_0.Read();
			num = 40;
			continue;
			IL_34A:
			flag3 = flag2;
			num = 5;
			continue;
			IL_3A4:
			num = 7;
			continue;
			IL_3DC:
			num = 28;
			continue;
			IL_3FD:
			flag3 = flag6;
			num = 10;
			continue;
			IL_44B:
			num = 9;
			continue;
			IL_493:
			flag3 = flag5;
			num = 12;
		}
		IL_F1:
		throw new ArgumentNullException(RecordTableEnumerator.b("㍀♂⑄⍆ⱈ㥊", a_));
		IL_1EF:
		throw new ArgumentNullException(RecordTableEnumerator.b("⍀ⱂ⩄ⱆ", a_));
		IL_3F8:
		throw new spr\u23EE(RecordTableEnumerator.b("㉀㝂㝄⹆⩈㽊", a_), RecordTableEnumerator.b("ࡀⵂ㍄♆╈≊⥌潎㝐㩒㥔㉖祘㵚㉜ⵞౠɢᅤ䥦", a_));
		IL_4C5:
		this.ᜀ(A_0, A_1);
	}

	// Token: 0x06003EE4 RID: 16100 RVA: 0x00237118 File Offset: 0x00236118
	private void ᜀ(XmlReader A_0, XlsWorkbook A_1)
	{
		int a_ = 5;
		int num = 7;
		int activeSheetIndex;
		int displayedTab;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 6;
				continue;
			case 1:
				goto IL_12F;
			case 2:
				this.ᜁ(A_0, A_1.Names, 0);
				num = 32;
				continue;
			case 3:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("为似儾筀あ♄⽆ⱈ♊ⱌ㱎籐㹒㱔㑖⭘㑚⹜ぞݠᝢ䡤Ѧ٨٪坬nᝰᕲᱴᑶᱸ䅺᡼ݾ", a_))
				{
					num = 13;
					continue;
				}
				goto IL_437;
			case 4:
			{
				bool flag;
				if (!flag)
				{
					num = 9;
					continue;
				}
				goto IL_691;
			}
			case 5:
				if (A_0.LocalName == RecordTableEnumerator.b("紺吼䴾㉀㝂ፄ⹆㩈≊⽌⍎㑐R㵔㉖㱘⽚", a_))
				{
					num = 0;
					continue;
				}
				goto IL_12F;
			case 6:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("为似儾筀あ♄⽆ⱈ♊ⱌ㱎籐㹒㱔㑖⭘㑚⹜ぞݠᝢ䡤Ѧ٨٪坬nᝰᕲᱴᑶᱸ䅺᡼ݾ", a_))
				{
					num = 39;
					continue;
				}
				goto IL_12F;
			case 8:
				if (A_0.LocalName == RecordTableEnumerator.b("栺䤼䘾ⵀ♂㙄", a_))
				{
					num = 15;
					continue;
				}
				goto IL_4F9;
			case 9:
				goto IL_68C;
			case 10:
				if (A_0.LocalName == RecordTableEnumerator.b("氺刼䴾⩀あⵄ≆ⱈ㽊", a_))
				{
					num = 24;
					continue;
				}
				goto IL_F6;
			case 11:
				if (A_0.LocalName == RecordTableEnumerator.b("稺帼䬾⡀㕂⁄ᑆⅈ⹊⡌㭎", a_))
				{
					num = 26;
					continue;
				}
				goto IL_437;
			case 12:
				num = 25;
				continue;
			case 13:
				A_0.Read();
				activeSheetIndex = XmlConvert.ToInt32(A_0.Value);
				A_0.Skip();
				num = 14;
				continue;
			case 14:
				goto IL_437;
			case 15:
				num = 18;
				continue;
			case 16:
				if (!(A_0.LocalName != RecordTableEnumerator.b("氺刼䴾⩀⅂⩄⡆≈", a_)))
				{
					num = 12;
					continue;
				}
				goto IL_361;
			case 17:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 45;
					continue;
				}
				num = 11;
				continue;
			case 18:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("为似儾筀あ♄⽆ⱈ♊ⱌ㱎籐㹒㱔㑖⭘㑚⹜ぞݠᝢ䡤Ѧ٨٪坬nᝰᕲᱴᑶᱸ䅺๼ཾ愈", a_))
				{
					num = 36;
					continue;
				}
				goto IL_4F9;
			case 19:
				goto IL_236;
			case 20:
				num = 4;
				continue;
			case 21:
				A_0.MoveToElement();
				num = 44;
				continue;
			case 22:
				A_0.Read();
				num = 37;
				continue;
			case 23:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("为似儾筀あ♄⽆ⱈ♊ⱌ㱎籐㹒㱔㑖⭘㑚⹜ぞݠᝢ䡤Ѧ٨٪坬nᝰᕲᱴᑶᱸ䅺๼ཾ愈", a_))
				{
					num = 41;
					continue;
				}
				goto IL_F6;
			case 24:
				num = 23;
				continue;
			case 25:
			{
				if (A_0.IsEmptyElement)
				{
					num = 42;
					continue;
				}
				A_0.Read();
				bool flag = false;
				this.\u1719.Clear();
				activeSheetIndex = 0;
				displayedTab = 0;
				num = 19;
				continue;
			}
			case 26:
				num = 3;
				continue;
			case 27:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 20;
					continue;
				}
				num = 40;
				continue;
			case 28:
				goto IL_4F9;
			case 29:
				goto IL_F1;
			case 30:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("为似儾筀あ♄⽆ⱈ♊ⱌ㱎籐㹒㱔㑖⭘㑚⹜ぞݠᝢ䡤Ѧ٨٪坬nᝰᕲᱴᑶᱸ䅺๼ཾ愈", a_))
				{
					num = 2;
					continue;
				}
				goto IL_2C0;
			case 31:
				goto IL_60D;
			case 32:
				goto IL_2C0;
			case 33:
				if (A_0.LocalName == RecordTableEnumerator.b("町尼刾⑀あ", a_))
				{
					num = 47;
					continue;
				}
				goto IL_2C0;
			case 34:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("为似儾筀あ♄⽆ⱈ♊ⱌ㱎籐㹒㱔㑖⭘㑚⹜ぞݠᝢ䡤Ѧ٨٪坬nᝰᕲᱴᑶᱸ䅺᡼ݾ", a_))
				{
					num = 21;
					continue;
				}
				goto IL_470;
			case 35:
				goto IL_236;
			case 36:
				this.ᜂ(A_0, A_1);
				num = 28;
				continue;
			case 37:
				goto IL_64B;
			case 38:
				goto IL_F6;
			case 39:
				A_0.Read();
				displayedTab = XmlConvert.ToInt32(A_0.Value);
				A_0.Skip();
				num = 1;
				continue;
			case 40:
				if (A_0.LocalName == RecordTableEnumerator.b("縺䔼尾⑀⽂ቄ⡆㭈⁊⽌⁎㹐㡒", a_))
				{
					num = 43;
					continue;
				}
				goto IL_470;
			case 41:
			{
				this.ᜃ(A_0, A_1);
				bool flag = true;
				goto IL_226;
			}
			case 42:
				goto IL_3A4;
			case 43:
				num = 34;
				continue;
			case 44:
				if (!A_0.IsEmptyElement)
				{
					if (true)
					{
					}
					num = 22;
					continue;
				}
				goto IL_470;
			case 45:
				goto IL_470;
			case 46:
				goto IL_64B;
			case 47:
				num = 30;
				continue;
			case 48:
				if (A_1 == null)
				{
					num = 31;
					continue;
				}
				this.\u1718 = A_1;
				this.\u171B = new FormulaUtil(this.\u1718.AppImplementation, this.\u1718, NumberFormatInfo.InvariantInfo, ',', ';');
				A_0.MoveToContent();
				num = 16;
				continue;
			}
			if (A_0 == null)
			{
				num = 29;
				continue;
			}
			num = 48;
			continue;
			IL_F6:
			num = 8;
			continue;
			IL_12F:
			A_0.Skip();
			num = 46;
			continue;
			IL_226:
			num = 38;
			continue;
			IL_2C0:
			A_0.Skip();
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_226;
			default:
				if (false)
				{
				}
				num = 35;
				continue;
			}
			IL_236:
			num = 27;
			continue;
			IL_437:
			num = 5;
			continue;
			IL_470:
			num = 10;
			continue;
			IL_4F9:
			num = 33;
			continue;
			IL_64B:
			num = 17;
		}
		IL_F1:
		throw new ArgumentNullException(RecordTableEnumerator.b("䤺堼帾╀♂㝄", a_));
		IL_361:
		throw new spr\u23EE(RecordTableEnumerator.b("夺刼倾⩀", a_), RecordTableEnumerator.b("氺刼䴾⩀⅂⩄⡆≈歊⍌⁎㕐㙒畔㑖㡘㕚絜ㅞ๠ᝢ䕤զ౨䭪୬nѰᵲᅴ奶", a_));
		IL_3A4:
		goto IL_361;
		IL_60D:
		throw new ArgumentNullException(RecordTableEnumerator.b("夺刼倾⩀", a_));
		IL_68C:
		throw new spr\u23EE(RecordTableEnumerator.b("夺刼倾⩀", a_), RecordTableEnumerator.b("氺刼䴾⩀あⵄ≆ⱈ㽊浌ⅎ㹐㝒ご睖㩘㩚㍜罞འౢᅤ䝦୨๪䵬८Ṱٲ᭴፶坸", a_));
		IL_691:
		this.ᜁ(A_1);
		this.ᜀ(A_1);
		A_1.ActiveSheetIndex = activeSheetIndex;
		A_1.DisplayedTab = displayedTab;
	}

	// Token: 0x06003EE5 RID: 16101 RVA: 0x002377D4 File Offset: 0x002367D4
	private spr\u192F ᜀ(sprᢖ A_0, string A_1, string A_2, string A_3)
	{
		int a_ = 16;
		int num;
		spr\u192F spr_u192F;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_164:
			if (A_2 == null)
			{
				num = 2;
			}
			else
			{
				spr_u192F = (spr\u192F)A_0.ᜁ(0).\u1758();
				spr_u192F.ᜄ(spr_u192F.ᜎ().MaxXFCount);
				num = 3;
			}
			break;
		default:
			if (false)
			{
			}
			num = 9;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				goto IL_1CB;
			case 1:
			{
				int a_2 = this.\u1717[A_3];
				spr\u192F spr_u192F2 = A_0.ᜁ(a_2);
				spr_u192F = (spr\u192F)spr_u192F2.\u1758();
				spr_u192F.ᜄ(a_2);
				num = 0;
				continue;
			}
			case 2:
				A_0.ᜁ(0);
				spr_u192F = (spr\u192F)this.\u1718.CreateExtFormat(false);
				num = 10;
				continue;
			case 3:
				goto IL_1CB;
			case 4:
				if (A_3 != null)
				{
					num = 1;
					continue;
				}
				num = 6;
				continue;
			case 5:
				goto IL_7A;
			case 6:
				goto IL_164;
			case 7:
				goto IL_C7;
			case 8:
				if (A_1 == RecordTableEnumerator.b("Ʌⵇⱉⵋ㭍㱏♑", a_))
				{
					num = 7;
					continue;
				}
				num = 4;
				continue;
			case 10:
				goto IL_1CB;
			case 11:
				goto IL_1D6;
			}
			if (A_1 == "")
			{
				num = 5;
				continue;
			}
			num = 8;
			continue;
			IL_1CB:
			num = 11;
		}
		IL_7A:
		throw new spr\u23EE(RecordTableEnumerator.b("㕅㱇㍉⁋⭍", a_), RecordTableEnumerator.b("ཅ♇㱉ⵋ≍㥏㙑瑓さㅗ㙙㥛繝ٟൡᙣ୥१ṩ䉫", a_));
		IL_C7:
		this.\u1717.Add(RecordTableEnumerator.b("Ʌⵇⱉⵋ㭍㱏♑", a_), A_0.ᜀ().DefaultXFIndex);
		return A_0.ᜁ(0);
		IL_1D6:
		spr_u192F.ᜀ((A_2 == null || A_2 == RecordTableEnumerator.b("Ʌⵇⱉⵋ㭍㱏♑", a_)) ? sprỶ.TXFType.XF_STYLE : sprỶ.TXFType.XF_CELL);
		return spr_u192F;
	}

	// Token: 0x06003EE6 RID: 16102 RVA: 0x002379E4 File Offset: 0x002369E4
	private void ᜀ(XlsWorkbook A_0, sprᢖ A_1, spr\u192F A_2, string A_3, string A_4)
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			int num = 4;
			XlsStylesCollection xlsStylesCollection;
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
					num = 9;
					continue;
				case 1:
					goto IL_1F9;
				case 2:
					if (A_2 == null)
					{
						num = 10;
						continue;
					}
					num = 0;
					continue;
				case 3:
					xlsStylesCollection = A_0.InnerStyles;
					num = 12;
					continue;
				case 5:
					if (A_2.\u171E() == sprỶ.TXFType.XF_CELL)
					{
						num = 3;
						continue;
					}
					return;
				case 6:
					goto IL_219;
				case 7:
					A_2 = A_1.ᜁ(A_2);
					this.\u1717.Add(A_3, A_2.ᜌ());
					num = 5;
					continue;
				case 8:
					goto IL_69;
				case 9:
					if (A_3 != RecordTableEnumerator.b("ń≆⽈⩊㡌⍎═", a_))
					{
						num = 7;
						continue;
					}
					return;
				case 10:
					goto IL_F3;
				case 11:
					goto IL_9C;
				case 12:
				{
					if (xlsStylesCollection.ᜁ(A_4))
					{
						num = 11;
						continue;
					}
					sprᬐ sprᬐ = (sprᬐ)spr\u175E.ᜀ(TBIFFRecord.Style);
					sprᬐ.ᜀ((ushort)A_2.ᜌ());
					sprᬐ.ᜀ(A_4);
					XlsStyle style = base.AppImplementation.ᜀ(A_0, sprᬐ);
					xlsStylesCollection.Add(style);
					num = 1;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 8;
				}
				else
				{
					num = 2;
				}
			}
			IL_69:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("❄⡆♈⁊", a_));
			IL_9C:
			XlsStyle xlsStyle = (XlsStyle)xlsStylesCollection[A_4];
			spr\u192F a_2 = A_1.ᜁ(xlsStyle.Index);
			A_2.ᜇ(a_2);
			AddtionalFormatWrapper addtionalFormatWrapper = (AddtionalFormatWrapper)xlsStylesCollection[A_4];
			addtionalFormatWrapper.UpdateFont();
			return;
			IL_F3:
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
			throw new ArgumentNullException(RecordTableEnumerator.b("⍄⡆㭈♊ⱌ㭎", a_));
			IL_1F9:
			return;
			IL_219:
			throw new ArgumentNullException(RecordTableEnumerator.b("♄⡆╈❊", a_));
		}
		}
	}

	// Token: 0x06003EE7 RID: 16103 RVA: 0x00237C34 File Offset: 0x00236C34
	private Color ᜀ(string A_0)
	{
		int a_ = 16;
		int num = 1;
		int num2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_95;
			case 2:
				if (A_0.Length == 0)
				{
					num = 5;
					continue;
				}
				if (true)
				{
				}
				num2 = A_0.IndexOf(RecordTableEnumerator.b("故", a_));
				num = 3;
				continue;
			case 3:
				if (num2 != -1)
				{
					num = 0;
					continue;
				}
				goto IL_E9;
			case 4:
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
				break;
			case 5:
				goto IL_C9;
			}
			if (A_0 == null)
			{
				goto IL_97;
			}
			num = 4;
		}
		IL_95:
		A_0 = A_0.Substring(num2 + 1);
		int a_2 = int.Parse(A_0, NumberStyles.HexNumber);
		return spr\u1D39.ᜀ(a_2);
		IL_97:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕅㱇㡉ཋ⅍㱏㵑♓", a_));
		IL_C9:
		goto IL_97;
		IL_E9:
		return Color.FromName(A_0);
	}

	// Token: 0x06003EE8 RID: 16104 RVA: 0x00237D30 File Offset: 0x00236D30
	private IFont ᜀ(IFont A_0, string A_1)
	{
		int a_ = 17;
		if (true)
		{
		}
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_95:
			A_0.IsSubscript = true;
			num = 1;
			break;
		default:
			if (false)
			{
			}
			num = 2;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_D6;
			case 1:
				goto IL_6A;
			case 3:
				A_0.IsSuperscript = true;
				num = 0;
				continue;
			case 4:
				goto IL_68;
			case 5:
				if (A_1 == RecordTableEnumerator.b("ᑆ㱈⥊㹌ⱎ⍐㩒╔⍖", a_))
				{
					num = 7;
					continue;
				}
				goto IL_6A;
			case 6:
				if (A_1 == RecordTableEnumerator.b("ᑆ㱈㭊⡌㵎≐げ❔㹖⥘⽚", a_))
				{
					num = 3;
					continue;
				}
				return A_0;
			case 7:
				goto IL_107;
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			num = 5;
			continue;
			IL_6A:
			num = 6;
		}
		IL_68:
		throw new ArgumentNullException(RecordTableEnumerator.b("ⅆ♈╊㥌", a_));
		IL_D6:
		return A_0;
		IL_107:
		goto IL_95;
	}

	// Token: 0x06003EE9 RID: 16105 RVA: 0x00237E48 File Offset: 0x00236E48
	private void ᜀ(XmlSerializationCellType A_0, string A_1, XlsCellRecordCollection A_2, int A_3, int A_4, int A_5, spr\u223A A_6)
	{
		int a_ = 18;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch (A_0)
				{
				case XmlSerializationCellType.Number:
					goto IL_D6;
				case XmlSerializationCellType.DateTime:
					goto IL_54;
				case XmlSerializationCellType.Boolean:
					goto IL_7F;
				case XmlSerializationCellType.String:
					goto IL_15D;
				case XmlSerializationCellType.Error:
					goto IL_E9;
				default:
					num = 6;
					continue;
				}
				break;
			case 1:
				if (A_1.Length == 0)
				{
					num = 4;
					continue;
				}
				goto IL_10B;
			case 2:
				num = 1;
				continue;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_10B;
				default:
					goto IL_CE;
				}
				break;
			case 5:
				goto IL_124;
			case 6:
				num = 8;
				continue;
			case 7:
				if (A_2 == null)
				{
					num = 5;
					continue;
				}
				num = 0;
				continue;
			case 8:
				goto IL_79;
			}
			if (A_1 != null)
			{
				num = 2;
				continue;
			}
			return;
			IL_10B:
			num = 7;
		}
		IL_54:
		DateTime a_2 = XmlConvert.ToDateTime(A_1, XmlDateTimeSerializationMode.Unspecified);
		double dValue = UtilityMethods.ᜀ(a_2);
		A_2.SetNumberValue(A_3, A_4, dValue, A_5);
		return;
		IL_79:
		goto IL_15D;
		IL_7F:
		if (true)
		{
		}
		A_2.SetBooleanValue(A_3, A_4, XmlConvert.ToBoolean(A_1), A_5);
		return;
		IL_CE:
		if (false)
		{
		}
		return;
		IL_D6:
		A_2.SetNumberValue(A_3, A_4, XmlConvert.ToDouble(A_1), A_5);
		return;
		IL_E9:
		A_2.SetErrorValue(A_3, A_4, A_1, A_5);
		return;
		IL_124:
		throw new ArgumentNullException(RecordTableEnumerator.b("⭇⽉⁋≍⍏", a_));
		IL_15D:
		A_2.ᜀ(A_3, A_4, A_5, A_6);
	}

	// Token: 0x06003EEA RID: 16106 RVA: 0x00237FC0 File Offset: 0x00236FC0
	private int ᜀ(XlsWorksheet A_0, string A_1)
	{
		int a_ = 19;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1.Length != 0)
				{
					num = 1;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_5B;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			case 1:
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				goto IL_C6;
			case 2:
				goto IL_5B;
			case 3:
				goto IL_A8;
			case 5:
				num = 0;
				continue;
			}
			if (A_1 == null)
			{
				goto IL_5D;
			}
			num = 5;
		}
		IL_5B:
		throw new ArgumentNullException(RecordTableEnumerator.b("㩈⍊⡌⩎═", a_));
		IL_5D:
		throw new ArgumentNullException(RecordTableEnumerator.b("㩈㽊㽌᥎ぐ㽒⁔㉖", a_));
		IL_A8:
		if (true)
		{
		}
		goto IL_5D;
		IL_C6:
		sprᢖ sprᢖ = A_0.ParentWorkbook.InnerExtFormats;
		spr\u192F spr_u192F = sprᢖ.ᜁ(this.\u1717[A_1]);
		spr_u192F = spr_u192F.ᜭ();
		int num2 = spr_u192F.ᜌ();
		this.\u1717[A_1] = num2;
		return num2;
	}

	// Token: 0x06003EEB RID: 16107 RVA: 0x002380D0 File Offset: 0x002370D0
	private LineStyleType ᜀ(string A_0, string A_1)
	{
		int a_ = 17;
		for (;;)
		{
			IL_09:
			int num = 1;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					num = 3;
					continue;
				case 2:
					if (A_0.Length == 0)
					{
						num = 8;
						continue;
					}
					num = 10;
					continue;
				case 3:
					if (A_1.Length == 0)
					{
						num = 7;
						continue;
					}
					num = 11;
					continue;
				case 4:
					return LineStyleType.None;
				case 5:
					goto IL_B8;
				case 6:
					num = 2;
					continue;
				case 7:
					goto IL_173;
				case 8:
					goto IL_11D;
				case 9:
					A_0 = A_1 + RecordTableEnumerator.b("杆", a_) + A_0;
					num = 5;
					continue;
				case 10:
					if (A_1 != null)
					{
						num = 0;
						continue;
					}
					goto IL_90;
				case 11:
					if (A_1 != RecordTableEnumerator.b("睆", a_))
					{
						num = 9;
						continue;
					}
					goto IL_B8;
				case 12:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					default:
						if (false)
						{
						}
						if (num2 <= 0)
						{
							num = 4;
							continue;
						}
						return (LineStyleType)num2;
					}
					break;
				}
				if (A_0 != null)
				{
					if (true)
					{
					}
					num = 6;
					continue;
				}
				goto IL_A4;
				IL_B8:
				num2 = Array.IndexOf<string>(sprỉ.\u17EC, A_0);
				num = 12;
			}
		}
		IL_90:
		throw new ArgumentNullException(RecordTableEnumerator.b("うⱈ≊⩌❎═", a_));
		IL_A4:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑆㵈㉊⅌⩎", a_));
		IL_11D:
		goto IL_A4;
		IL_173:
		goto IL_90;
	}

	// Token: 0x06003EEC RID: 16108 RVA: 0x00238274 File Offset: 0x00237274
	private void ᜁ(XlsWorkbook A_0)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				string text;
				int num2;
				int count;
				sprឦ sprឦ;
				sprῚ sprῚ;
				switch (num)
				{
				case 0:
					goto IL_106;
				case 1:
					goto IL_66;
				case 2:
					goto IL_106;
				case 3:
					if (text[0] != '=')
					{
						goto IL_6B;
					}
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
						num = 7;
						continue;
					}
					break;
				case 5:
					if (num2 >= count)
					{
						num = 8;
						continue;
					}
					sprῚ = sprឦ.ᜂ(num2);
					text = this.\u1719[num2];
					num = 3;
					continue;
				case 6:
					goto IL_6B;
				case 7:
					text = text.Substring(1);
					num = 6;
					continue;
				case 8:
					return;
				}
				if (A_0 == null)
				{
					num = 1;
					continue;
				}
				sprឦ = A_0.InnerNamesColection;
				num2 = 0;
				count = this.\u1719.Count;
				num = 0;
				continue;
				IL_6B:
				sprῚ.ᜀ(this.\u171B.ᜀ(text, null, null, 0, 0, true));
				num2++;
				num = 2;
				continue;
				IL_106:
				num = 5;
			}
			IL_66:
			throw new ArgumentNullException(RecordTableEnumerator.b("崾⹀ⱂ⹄", a_));
		}
		}
	}

	// Token: 0x06003EED RID: 16109 RVA: 0x002383E8 File Offset: 0x002373E8
	private string ᜀ(XmlReader A_0, int A_1, spr\u223A A_2)
	{
		int a_ = 19;
		switch (0)
		{
		default:
		{
			int num = 18;
			for (;;)
			{
				XlsFont xlsFont;
				XlsFontsCollection innerFonts;
				StringBuilder stringBuilder;
				switch (num)
				{
				case 0:
					xlsFont = this.ᜀ(A_0, xlsFont.Clone(innerFonts), A_0.LocalName, true);
					num = 13;
					continue;
				case 1:
					xlsFont = this.ᜀ(A_0, xlsFont.Clone(innerFonts), A_0.LocalName, false);
					num = 5;
					continue;
				case 2:
					goto IL_A5;
				case 3:
					num = 6;
					continue;
				case 4:
				{
					if (stringBuilder.Length <= 0)
					{
						num = 14;
						continue;
					}
					string result;
					return result;
				}
				case 5:
					goto IL_CC;
				case 6:
					if (!(A_0.LocalName != RecordTableEnumerator.b("ൈ⩊㥌⹎", a_)))
					{
						num = 16;
						continue;
					}
					goto IL_F6;
				case 7:
					goto IL_A5;
				case 8:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 1;
						continue;
					}
					goto IL_CC;
				case 9:
					goto IL_A0;
				case 10:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 3;
						continue;
					}
					goto IL_F6;
				case 11:
					xlsFont = (XlsFont)innerFonts.Add(xlsFont);
					A_2.ᜇ().Add(stringBuilder.Length, xlsFont.Index);
					stringBuilder.Append(A_0.Value);
					num = 15;
					continue;
				case 12:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 0;
						continue;
					}
					goto IL_1BB;
				case 13:
					goto IL_1BB;
				case 14:
					goto IL_156;
				case 15:
					goto IL_2C3;
				case 16:
				{
					string text;
					A_2.ᜁ(text = stringBuilder.ToString());
					string result = text;
					num = 4;
					continue;
				}
				case 17:
					if (A_0.NodeType == XmlNodeType.Text)
					{
						goto IL_10E;
					}
					goto IL_2C3;
				case 18:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_10E;
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
					num = 9;
					continue;
				}
				innerFonts = this.\u1718.InnerFonts;
				int index = this.\u1718.InnerExtFormats.ᜁ(A_1).\u173B();
				xlsFont = (XlsFont)innerFonts[index];
				stringBuilder = new StringBuilder();
				xlsFont = xlsFont.Clone(this.\u1718.InnerFonts);
				A_0.Read();
				num = 2;
				continue;
				IL_A5:
				num = 10;
				continue;
				IL_CC:
				num = 12;
				continue;
				IL_F6:
				num = 17;
				continue;
				IL_10E:
				num = 11;
				continue;
				IL_1BB:
				if (true)
				{
				}
				A_0.Read();
				num = 7;
				continue;
				IL_2C3:
				num = 8;
			}
			IL_A0:
			throw new ArgumentNullException(RecordTableEnumerator.b("㭈⹊ⱌ⭎㑐⅒", a_));
			IL_156:
			return null;
		}
		}
	}

	// Token: 0x06003EEE RID: 16110 RVA: 0x002386E4 File Offset: 0x002376E4
	private XlsFont ᜀ(XmlReader A_0, XlsFont A_1, string A_2, bool A_3)
	{
		int a_ = 2;
		int num = 7;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_25A;
				default:
					if (false)
					{
					}
					if (spr\u22D2.ᝉ == null)
					{
						num = 16;
						continue;
					}
					goto IL_2FE;
				}
				break;
			case 1:
				num = 0;
				continue;
			case 2:
				num = 21;
				continue;
			case 3:
				num = 20;
				continue;
			case 4:
				return A_1;
			case 5:
				return A_1;
			case 6:
				goto IL_7C;
			case 8:
				if (A_2 != null)
				{
					num = 1;
					continue;
				}
				return A_1;
			case 9:
				return A_1;
			case 10:
				return A_1;
			case 11:
				if (A_0 == null)
				{
					num = 19;
					continue;
				}
				if (true)
				{
				}
				num = 8;
				continue;
			case 12:
				goto IL_AB;
			case 13:
				goto IL_2FE;
			case 14:
				return A_1;
			case 15:
				return A_1;
			case 16:
				spr\u22D2.ᝉ = new Dictionary<string, int>(7)
				{
					{
						RecordTableEnumerator.b("稷", a_),
						0
					},
					{
						RecordTableEnumerator.b("焷", a_),
						1
					},
					{
						RecordTableEnumerator.b("洷", a_),
						2
					},
					{
						RecordTableEnumerator.b("欷伹帻", a_),
						3
					},
					{
						RecordTableEnumerator.b("欷伹䰻", a_),
						4
					},
					{
						RecordTableEnumerator.b("欷", a_),
						5
					},
					{
						RecordTableEnumerator.b("縷唹刻䨽", a_),
						6
					}
				};
				num = 13;
				continue;
			case 17:
			{
				int num2;
				if (spr\u22D2.ᝉ.TryGetValue(A_2, out num2))
				{
					num = 2;
					continue;
				}
				return A_1;
			}
			case 18:
				A_1.Underline = (A_3 ? FontUnderlineType.None : FontUnderlineType.Single);
				num = 4;
				continue;
			case 19:
				goto IL_CC;
			case 20:
				return A_1;
			case 21:
			{
				int num2;
				switch (num2)
				{
				case 0:
					A_1.IsBold = !A_3;
					goto IL_25A;
				case 1:
					A_1.IsItalic = !A_3;
					num = 10;
					continue;
				case 2:
					num = 18;
					continue;
				case 3:
					A_1.IsSubscript = !A_3;
					num = 12;
					continue;
				case 4:
					A_1.IsSuperscript = !A_3;
					num = 14;
					continue;
				case 5:
					A_1.IsStrikethrough = !A_3;
					num = 15;
					continue;
				case 6:
					A_1 = this.ᜀ(A_0, A_1);
					num = 9;
					continue;
				default:
					num = 3;
					continue;
				}
				break;
			}
			}
			if (A_1 == null)
			{
				num = 6;
				continue;
			}
			num = 11;
			continue;
			IL_25A:
			num = 5;
			continue;
			IL_2FE:
			num = 17;
		}
		IL_7C:
		throw new ArgumentNullException(RecordTableEnumerator.b("帷唹刻䨽", a_));
		IL_AB:
		return A_1;
		IL_CC:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹崻娽┿ぁ", a_));
	}

	// Token: 0x06003EEF RID: 16111 RVA: 0x00238A24 File Offset: 0x00237A24
	private XlsFont ᜀ(XmlReader A_0, XlsFont A_1)
	{
		int a_ = 14;
		int num = 7;
		for (;;)
		{
			int num2;
			int attributeCount;
			switch (num)
			{
			case 0:
				if (A_0.LocalName == RecordTableEnumerator.b("Ƀ❅⭇⽉", a_))
				{
					num = 13;
					continue;
				}
				goto IL_EA;
			case 1:
				goto IL_14D;
			case 2:
				if (!(A_0.LocalName == RecordTableEnumerator.b("݃⥅⑇╉㹋", a_)))
				{
					goto IL_A3;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_106;
				default:
					if (false)
					{
					}
					num = 16;
					continue;
				}
				break;
			case 3:
				goto IL_A3;
			case 4:
				goto IL_70;
			case 5:
				A_1.Size = (double)XmlConvert.ToInt32(A_0.Value);
				if (true)
				{
				}
				num = 4;
				continue;
			case 6:
				goto IL_117;
			case 8:
				goto IL_6B;
			case 9:
				goto IL_106;
			case 10:
				goto IL_14D;
			case 11:
				goto IL_EA;
			case 12:
				if (A_0.LocalName == RecordTableEnumerator.b("ᝃ⽅㉇⽉", a_))
				{
					num = 5;
					continue;
				}
				goto IL_70;
			case 13:
				A_1.FontName = A_0.Value;
				num = 11;
				continue;
			case 14:
				if (num2 >= attributeCount)
				{
					num = 15;
					continue;
				}
				A_0.MoveToAttribute(num2);
				num = 2;
				continue;
			case 15:
				goto IL_167;
			case 16:
				A_1.Color = this.ᜀ(A_0.Value);
				num = 3;
				continue;
			}
			if (A_0 == null)
			{
				num = 8;
				continue;
			}
			num = 9;
			continue;
			IL_70:
			num = 0;
			continue;
			IL_A3:
			num = 12;
			continue;
			IL_EA:
			num2++;
			num = 10;
			continue;
			IL_106:
			if (A_1 == null)
			{
				num = 6;
				continue;
			}
			A_1.Color = spr\u1D39.ᜀ;
			A_1.Size = 10.0;
			num2 = 0;
			attributeCount = A_0.AttributeCount;
			num = 1;
			continue;
			IL_14D:
			num = 14;
		}
		IL_6B:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙃⍅⥇⹉⥋㱍", a_));
		IL_117:
		throw new ArgumentNullException(RecordTableEnumerator.b("≃⥅♇㹉", a_));
		IL_167:
		A_0.MoveToElement();
		return A_1;
	}

	// Token: 0x06003EF0 RID: 16112 RVA: 0x00238C84 File Offset: 0x00237C84
	private void ᜀ(XlsWorksheet A_0, int A_1, int A_2, string A_3, int A_4, string A_5, XmlSerializationCellType A_6)
	{
		int a_ = 16;
		int num = 3;
		for (;;)
		{
			IL_13:
			switch (num)
			{
			case 0:
				goto IL_B4;
			case 1:
				while (A_3.IndexOf('!') != -1)
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
						num = 4;
						goto IL_13;
					}
				}
				goto IL_134;
			case 2:
				goto IL_47;
			case 4:
				goto IL_93;
			case 5:
				if (A_3 != null)
				{
					num = 6;
					continue;
				}
				goto IL_120;
			case 6:
				num = 7;
				continue;
			case 7:
			{
				if (A_3.Length == 0)
				{
					num = 0;
					continue;
				}
				XlsCellRecordCollection cellRecords = A_0.CellRecords;
				num = 1;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 2;
			}
			else
			{
				num = 5;
			}
		}
		IL_47:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕅⁇⽉⥋㩍", a_));
		IL_93:
		long key = sprỉ.ᜀ(A_0.Index, sprṔ.ᜀ(A_2, A_1));
		this.\u171A.Add(key, new spr\u247E.ᜀ(A_3, A_5, A_6, A_4));
		return;
		IL_B4:
		IL_120:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕅㱇㡉ੋ⅍≏㽑⅓㩕㥗", a_));
		IL_134:
		this.ᜀ(A_0, A_1, A_2, A_3, A_5, A_6, A_4);
	}

	// Token: 0x06003EF1 RID: 16113 RVA: 0x00238DD8 File Offset: 0x00237DD8
	private void ᜀ(XlsWorksheet A_0, int A_1, int A_2, string A_3, string A_4, XmlSerializationCellType A_5, int A_6)
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
		XlsCellRecordCollection cellRecords = A_0.CellRecords;
		spr᱒ spr᱒ = (spr᱒)spr\u175E.ᜀ(TBIFFRecord.Formula);
		A_3 = UtilityMethods.ᜀ(A_3);
		spr᱒.ᜁ(this.\u171B.ᜀ(A_3, A_0, null, A_1 - 1, A_2 - 1, true));
		spr᱒.ᜇ(A_1 - 1);
		spr᱒.ᜆ(A_2 - 1);
		spr᱒.ᜁ((ushort)A_6);
		cellRecords.ᜁ(A_1, A_2, spr᱒);
		this.ᜀ(A_0, A_1, A_2, A_4, A_5);
	}

	// Token: 0x06003EF2 RID: 16114 RVA: 0x00238E7C File Offset: 0x00237E7C
	private void ᜀ(XlsWorksheet A_0, int A_1, int A_2, string A_3, XmlSerializationCellType A_4)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch (A_4)
				{
				case XmlSerializationCellType.Number:
					goto IL_D4;
				case XmlSerializationCellType.DateTime:
					goto IL_B0;
				case XmlSerializationCellType.Boolean:
					goto IL_3D;
				case XmlSerializationCellType.String:
					goto IL_4D;
				case XmlSerializationCellType.Error:
					goto IL_C9;
				default:
					num = 3;
					continue;
				}
				break;
			case 2:
				return;
			case 3:
				num = 4;
				continue;
			case 4:
				goto IL_EF;
			}
			if (true)
			{
			}
			if (A_3 == null)
			{
				num = 2;
			}
			else
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3D;
				default:
					if (false)
					{
					}
					num = 0;
					break;
				}
			}
		}
		return;
		IL_3D:
		A_0.SetFormulaBoolValue(A_1, A_2, XmlConvert.ToBoolean(A_3));
		return;
		IL_4D:
		A_0.SetFormulaStringValue(A_1, A_2, A_3);
		return;
		IL_B0:
		DateTime formulaDateTime = XmlConvert.ToDateTime(A_3, XmlDateTimeSerializationMode.Unspecified);
		A_0[A_1, A_2].FormulaDateTime = formulaDateTime;
		return;
		IL_C9:
		A_0.SetFormulaErrorValue(A_1, A_2, A_3);
		return;
		IL_D4:
		A_0.SetFormulaNumberValue(A_1, A_2, XmlConvert.ToDouble(A_3));
		return;
		IL_EF:
		throw new XmlException();
	}

	// Token: 0x06003EF3 RID: 16115 RVA: 0x00238F80 File Offset: 0x00237F80
	private void ᜀ(XlsWorkbook A_0)
	{
		int a_ = 1;
		int num;
		Dictionary<long, spr\u247E.ᜀ>.Enumerator enumerator;
		XlsWorksheetsCollection innerWorksheets;
		FormulaUtil formulaUtil;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_1E0:
			try
			{
				num = 7;
				for (;;)
				{
					string text;
					int index;
					int a_3;
					int a_4;
					spr\u247E.ᜀ value;
					switch (num)
					{
					case 0:
						goto IL_13B;
					case 1:
						num = 2;
						continue;
					case 2:
						goto IL_19E;
					case 3:
						if (text[0] == '=')
						{
							num = 4;
							continue;
						}
						goto IL_13B;
					case 4:
						text = UtilityMethods.ᜀ(text);
						num = 0;
						continue;
					case 6:
					{
						if (!enumerator.MoveNext())
						{
							num = 1;
							continue;
						}
						KeyValuePair<long, spr\u247E.ᜀ> keyValuePair = enumerator.Current;
						long key = keyValuePair.Key;
						index = sprỉ.ᜁ(key);
						long a_2 = sprỉ.ᜀ(key);
						a_3 = sprṔ.ᜁ(a_2);
						a_4 = sprṔ.ᜀ(a_2);
						value = keyValuePair.Value;
						text = value.ᜀ;
						num = 3;
						continue;
					}
					}
					IL_115:
					num = 6;
					continue;
					goto IL_115;
					IL_13B:
					XlsWorksheet a_5 = (XlsWorksheet)innerWorksheets[index];
					this.ᜀ(a_5, a_3, a_4, text, value.ᜁ, value.ᜂ, value.ᜃ);
					num = 5;
				}
				IL_19E:
				goto IL_1F9;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			goto IL_1AE;
			IL_1F9:
			formulaUtil.NumberFormat = null;
			return;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				num = 1;
				break;
			}
			break;
		}
		for (;;)
		{
			IL_48:
			switch (num)
			{
			case 0:
				goto IL_1E0;
			case 2:
				goto IL_72;
			}
			if (true)
			{
			}
			if (A_0 != null)
			{
				goto IL_1AE;
			}
			num = 2;
		}
		IL_72:
		throw new ArgumentNullException(RecordTableEnumerator.b("唶嘸吺嘼", a_));
		IL_1AE:
		formulaUtil = A_0.FormulaUtil;
		innerWorksheets = A_0.InnerWorksheets;
		formulaUtil.NumberFormat = NumberFormatInfo.InvariantInfo;
		enumerator = this.\u171A.GetEnumerator();
		num = 0;
		goto IL_48;
	}

	// Token: 0x04001C88 RID: 7304
	private const string ᜀ = "version=\"1.0\"";

	// Token: 0x04001C89 RID: 7305
	private const string ᜁ = "xml";

	// Token: 0x04001C8A RID: 7306
	private const string ᜂ = "progid=\"Excel.Sheet\"";

	// Token: 0x04001C8B RID: 7307
	private const string ᜃ = "mso-application";

	// Token: 0x04001C8C RID: 7308
	private const string ᜄ = "urn:schemas-microsoft-com:office:office";

	// Token: 0x04001C8D RID: 7309
	private const string ᜅ = "urn:schemas-microsoft-com:office:excel";

	// Token: 0x04001C8E RID: 7310
	private const string ᜆ = "urn:schemas-microsoft-com:office:spreadsheet";

	// Token: 0x04001C8F RID: 7311
	private const string ᜇ = "http://www.w3.org/TR/REC-html40";

	// Token: 0x04001C90 RID: 7312
	private const string ᜈ = "Subscript";

	// Token: 0x04001C91 RID: 7313
	private const string ᜉ = "Superscript";

	// Token: 0x04001C92 RID: 7314
	private const string ᜊ = "B";

	// Token: 0x04001C93 RID: 7315
	private const string ᜋ = "I";

	// Token: 0x04001C94 RID: 7316
	private const string ᜌ = "U";

	// Token: 0x04001C95 RID: 7317
	private const string \u170D = "S";

	// Token: 0x04001C96 RID: 7318
	private const string ᜎ = "Span";

	// Token: 0x04001C97 RID: 7319
	private const string ᜏ = "Sub";

	// Token: 0x04001C98 RID: 7320
	private const string ᜐ = "Sup";

	// Token: 0x04001C99 RID: 7321
	private const string ᜑ = "Font";

	// Token: 0x04001C9A RID: 7322
	private const int \u1712 = 10;

	// Token: 0x04001C9B RID: 7323
	private const string \u1713 = "None";

	// Token: 0x04001C9C RID: 7324
	private const string \u1714 = "Arial";

	// Token: 0x04001C9D RID: 7325
	private const string \u1715 = "version";

	// Token: 0x04001C9E RID: 7326
	private const string \u1716 = "1.0";

	// Token: 0x04001C9F RID: 7327
	private Dictionary<string, int> \u1717 = new Dictionary<string, int>();

	// Token: 0x04001CA0 RID: 7328
	private XlsWorkbook \u1718;

	// Token: 0x04001CA1 RID: 7329
	private List<string> \u1719 = new List<string>();

	// Token: 0x04001CA2 RID: 7330
	private Dictionary<long, spr\u247E.ᜀ> \u171A = new Dictionary<long, spr\u247E.ᜀ>();

	// Token: 0x04001CA3 RID: 7331
	private FormulaUtil \u171B;

	// Token: 0x04001CA4 RID: 7332
	private static Dictionary<string, HorizontalAlignType> \u171C;

	// Token: 0x04001CA5 RID: 7333
	private static Dictionary<string, VerticalAlignType> \u171D;

	// Token: 0x04001CA6 RID: 7334
	private static Dictionary<string, string> \u171E;

	// Token: 0x0200041D RID: 1053
	private class ᜀ
	{
		// Token: 0x06003EF4 RID: 16116 RVA: 0x002391AC File Offset: 0x002381AC
		public ᜀ(string A_0, string A_1, XmlSerializationCellType A_2, int A_3)
		{
			this.ᜀ = A_0;
			this.ᜁ = A_1;
			this.ᜂ = A_2;
			this.ᜃ = A_3;
		}

		// Token: 0x04001CA7 RID: 7335
		public string ᜀ;

		// Token: 0x04001CA8 RID: 7336
		public string ᜁ;

		// Token: 0x04001CA9 RID: 7337
		public XmlSerializationCellType ᜂ;

		// Token: 0x04001CAA RID: 7338
		public int ᜃ;
	}
}
