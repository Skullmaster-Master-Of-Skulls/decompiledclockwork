using System;
using System.IO;
using System.Xml;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Charts;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlSerialization;

// Token: 0x020002D0 RID: 720
internal class spr\u21A8 : spr\u1A78
{
	// Token: 0x06002C3B RID: 11323 RVA: 0x0018AFA4 File Offset: 0x00189FA4
	public override void ᜀ(XmlWriter A_0, XlsShape A_1, sprᡟ A_2, RelationsCollection A_3)
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_2 == null)
					{
						num = 4;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C0;
					default:
						goto IL_E7;
					}
					break;
				case 1:
					goto IL_C0;
				case 3:
					goto IL_CF;
				case 4:
					goto IL_76;
				case 5:
					goto IL_5F;
				}
				if (A_0 == null)
				{
					if (true)
					{
					}
					num = 5;
					continue;
				}
				num = 1;
				continue;
				IL_C0:
				if (A_1 == null)
				{
					num = 3;
				}
				else
				{
					num = 0;
				}
			}
			IL_5F:
			throw new ArgumentNullException(RecordTableEnumerator.b("㉄㕆⁈㽊⡌㵎", a_));
			IL_76:
			throw new ArgumentNullException(RecordTableEnumerator.b("ⵄ⡆╈⽊⡌㵎", a_));
			IL_CF:
			throw new ArgumentNullException(RecordTableEnumerator.b("㙄⽆⡈㭊⡌", a_));
			IL_E7:
			if (false)
			{
			}
			XlsChartShape xlsChartShape = (XlsChartShape)A_1;
			XlsChart xlsChart = xlsChartShape.ChartObject;
			string text;
			string a_2 = this.ᜀ(A_2, xlsChart, out text);
			A_0.WriteStartElement(RecordTableEnumerator.b("ㅄう♈ࡊ⡌⍎㵐ቒ㭔㑖ㅘ㑚⽜", a_), RecordTableEnumerator.b("ⵄ㍆㵈㭊睌恎繐⁒㙔㽖㱘㙚㱜ⱞ你ౢᕤɦݨ፪lͮᝰᱲݴ᩶ᡸེ๼兾ꢆ力ﶒ殺뒚꾜꾞醠関誤풦\ud9a8\ud9aa좬캮햰삲\uddb4튶\udcb8쾺寮춾ꃀ듂계꧆껈", a_));
			base.ᜁ(A_0, RecordTableEnumerator.b("⍄㕆♈♊", a_), A_1.LeftColumn, A_1.LeftColumnOffset, A_1.TopRow, A_1.TopRowOffset, A_1.Worksheet, RecordTableEnumerator.b("ⵄ㍆㵈㭊睌恎繐⁒㙔㽖㱘㙚㱜ⱞ你ౢᕤɦݨ፪lͮᝰᱲݴ᩶ᡸེ๼兾ꢆ力ﶒ殺뒚꾜꾞醠関誤풦\ud9a8\ud9aa좬캮햰삲\uddb4튶\udcb8쾺寮춾ꃀ듂계꧆껈", a_));
			base.ᜁ(A_0, RecordTableEnumerator.b("ㅄ⡆", a_), A_1.RightColumn, A_1.RightColumnOffset, A_1.BottomRow, A_1.BottomRowOffset, A_1.Worksheet, RecordTableEnumerator.b("ⵄ㍆㵈㭊睌恎繐⁒㙔㽖㱘㙚㱜ⱞ你ౢᕤɦݨ፪lͮᝰᱲݴ᩶ᡸེ๼兾ꢆ力ﶒ殺뒚꾜꾞醠関誤풦\ud9a8\ud9aa좬캮햰삲\uddb4튶\udcb8쾺寮춾ꃀ듂계꧆껈", a_));
			this.ᜀ(A_0, xlsChartShape, a_2, A_2, false);
			A_0.WriteEndElement();
			A_2.ᜀ(xlsChart.Relations, text.Substring(1));
			return;
		}
		}
	}

	// Token: 0x06002C3C RID: 11324 RVA: 0x0018B180 File Offset: 0x0018A180
	internal new string ᜀ(sprᡟ A_0, XlsChart A_1, out string A_2)
	{
		int a_ = 17;
		switch (0)
		{
		default:
		{
			int num = 2;
			sprវ sprវ;
			MemoryStream memoryStream;
			StreamWriter streamWriter;
			XmlWriter xmlWriter;
			spr\u2541 spr_u;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_112;
				case 1:
					if (A_1.DataHolder != null)
					{
						goto IL_134;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_114;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				case 3:
					sprវ.ᜁ(A_1, A_2);
					num = 0;
					continue;
				case 4:
					goto IL_132;
				case 5:
					goto IL_5E;
				case 6:
					if (A_1 == null)
					{
						num = 4;
						continue;
					}
					if (true)
					{
					}
					memoryStream = new MemoryStream();
					streamWriter = new StreamWriter(memoryStream);
					xmlWriter = UtilityMethods.ᜀ(streamWriter);
					A_2 = spr\u21A8.ᜀ(A_0, A_1);
					spr_u = new spr\u2541();
					sprវ = A_0.ᜋ();
					num = 1;
					continue;
				}
				if (A_0 == null)
				{
					num = 5;
					continue;
				}
				IL_114:
				num = 6;
			}
			IL_5E:
			throw new ArgumentNullException(RecordTableEnumerator.b("⽆♈❊⥌⩎⍐", a_));
			IL_112:
			goto IL_134;
			IL_132:
			throw new ArgumentNullException(RecordTableEnumerator.b("⑆ⅈ⩊㽌㭎", a_));
			IL_134:
			spr_u.ᜁ(xmlWriter, A_1, A_2);
			xmlWriter.Flush();
			streamWriter.Flush();
			string a_2 = UtilityMethods.ᜀ(A_2);
			sprវ.\u1714().ᜀ(a_2, memoryStream, true, FileAttributes.Archive);
			string text = A_0.ᜈ().GenerateRelationId();
			A_0.ᜈ()[text] = new sprᦨ(A_2, RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒♔㑖ㅘ㹚ぜ㹞በ䵢੤ᝦ౨ժᕬɮᵰᕲᩴնᑸ᩺ॼ౾꾀Ꚉ펖쒠춢톤袦鮨鮪鶬馮麰솲킴\udbb6\ud8b8쾺풼킾꿀냂귄껆마룊곎말닒꟔ꏖ", a_));
			sprវ.ᜡ()[A_2] = RecordTableEnumerator.b("♆㥈㭊⅌♎㉐㉒⅔㹖㙘㕚牜⥞འݢ䭤ࡦᥨ๪ͬᝮᱰὲ፴ᡶ୸ᙺᱼ୾꺂ﲒﺚ辠잢힤욦\udea8슪쎬좮\udcb0\udfb2鮴풶톸\udaba쾼쮾믂꣄ꯆ", a_);
			return text;
		}
		}
	}

	// Token: 0x06002C3D RID: 11325 RVA: 0x0018B340 File Offset: 0x0018A340
	public new static string ᜀ(sprᡟ A_0, XlsChart A_1)
	{
		int a_ = 7;
		for (;;)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_34;
				case 2:
					goto IL_5D;
				case 3:
					if (A_1 == null)
					{
						num = 2;
						continue;
					}
					goto IL_73;
				}
				if (A_0 == null)
				{
					num = 0;
				}
				else
				{
					num = 3;
				}
			}
			IL_73:
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_9B;
			}
		}
		IL_34:
		throw new ArgumentNullException(RecordTableEnumerator.b("唼倾ⵀ❂⁄㕆", a_));
		IL_5D:
		throw new ArgumentNullException(RecordTableEnumerator.b("帼圾⁀ㅂㅄ", a_));
		IL_9B:
		if (false)
		{
		}
		sprវ sprវ = A_0.ᜋ();
		int num2;
		sprវ.ᜇ(num2 = sprវ.ᜎ() + 1);
		int num3 = num2;
		return string.Format(RecordTableEnumerator.b("ሼ䜾ⵀ求♄⽆⡈㥊㥌㱎繐げ㵔㙖⭘⽚♜潞ᱠ䵢ᵤ੦ը", a_), num3);
	}

	// Token: 0x06002C3E RID: 11326 RVA: 0x0018B420 File Offset: 0x0018A420
	internal new void ᜀ(XmlWriter A_0, XlsChartShape A_1, string A_2, sprᡟ A_3, bool A_4)
	{
		int a_ = 19;
		int num = 7;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_2B3;
			case 1:
				goto IL_102;
			case 2:
				if (!A_4)
				{
					num = 8;
					continue;
				}
				spr\u1A78.ᜀ(A_0, RecordTableEnumerator.b("ⅈ㽊㥌㽎歐籒穔⑖㩘㍚㡜㉞`ၢ䭤ࡦᥨ๪ͬᝮᱰὲ፴ᡶ୸ᙺᱼ୾궂ꒊﶎﲔ練ﺘ낞鎠鎢閤醦蚨\ud8aa\uddac\uddae풰튲톴쒶톸\udeba\ud8bc쮾藀뇂꓄냆ꃈꗊ꫌", a_), RecordTableEnumerator.b("ⅈ㽊㥌㽎歐籒穔⑖㩘㍚㡜㉞`ၢ䭤ࡦᥨ๪ͬᝮᱰὲ፴ᡶ୸ᙺᱼ୾궂ꒊﶎﲔ練ﺘ낞鎠鎢閤醦蚨욪첬욮\udfb0", a_), A_1.OffsetX, A_1.OffsetY, A_1.ExtentsX, A_1.ExtentsY);
				num = 4;
				continue;
			case 3:
				if (A_1 == null)
				{
					num = 6;
					continue;
				}
				num = 13;
				continue;
			case 4:
				if (true)
				{
				}
				goto IL_D6;
			case 5:
				if (!A_4)
				{
					num = 1;
					continue;
				}
				return;
			case 6:
				goto IL_123;
			case 8:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_102;
				}
				if (false)
				{
				}
				spr\u1A78.ᜀ(A_0, RecordTableEnumerator.b("ⅈ㽊㥌㽎歐籒穔⑖㩘㍚㡜㉞`ၢ䭤ࡦᥨ๪ͬᝮᱰὲ፴ᡶ୸ᙺᱼ୾궂ꒊﶎﲔ練ﺘ낞鎠鎢閤醦蚨\ud8aa\uddac\uddae풰튲톴쒶톸\udeba\ud8bc쮾藀뇂꓄냆ꃈꗊ꫌", a_), RecordTableEnumerator.b("ⅈ㽊㥌㽎歐籒穔⑖㩘㍚㡜㉞`ၢ䭤ࡦᥨ๪ͬᝮᱰὲ፴ᡶ୸ᙺᱼ୾궂ꒊﶎﲔ練ﺘ낞鎠鎢閤醦蚨욪첬욮\udfb0", a_), 0, 0, 0, 0);
				num = 9;
				continue;
			case 9:
				goto IL_D6;
			case 10:
				if (A_3 == null)
				{
					num = 15;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("⹈㥊ⱌ㽎㥐㩒㙔ᅖ⭘㩚ぜ㩞", a_), RecordTableEnumerator.b("ⅈ㽊㥌㽎歐籒穔⑖㩘㍚㡜㉞`ၢ䭤ࡦᥨ๪ͬᝮᱰὲ፴ᡶ୸ᙺᱼ୾궂ꒊﶎﲔ練ﺘ낞鎠鎢閤醦蚨\ud8aa\uddac\uddae풰튲톴쒶톸\udeba\ud8bc쮾藀뇂꓄냆ꃈꗊ꫌", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("⑈⩊⹌㵎㹐", a_), string.Empty);
				this.ᜀ(A_0, A_1, A_3);
				num = 2;
				continue;
			case 11:
				goto IL_6B;
			case 12:
				num = 16;
				continue;
			case 13:
				if (A_2 != null)
				{
					num = 12;
					continue;
				}
				goto IL_1E9;
			case 14:
				goto IL_1E4;
			case 15:
				goto IL_BD;
			case 16:
				if (A_2.Length == 0)
				{
					num = 0;
					continue;
				}
				num = 10;
				continue;
			}
			if (A_0 == null)
			{
				num = 11;
				continue;
			}
			num = 3;
			continue;
			IL_D6:
			this.ᜀ(A_0, A_1, A_2);
			A_0.WriteEndElement();
			num = 5;
			continue;
			IL_102:
			A_0.WriteElementString(RecordTableEnumerator.b("⩈❊⑌⩎㽐❒ᅔ㙖ⵘ㩚", a_), RecordTableEnumerator.b("ⅈ㽊㥌㽎歐籒穔⑖㩘㍚㡜㉞`ၢ䭤ࡦᥨ๪ͬᝮᱰὲ፴ᡶ୸ᙺᱼ୾궂ꒊﶎﲔ練ﺘ낞鎠鎢閤醦蚨\ud8aa\uddac\uddae풰튲톴쒶톸\udeba\ud8bc쮾藀뇂꓄냆ꃈꗊ꫌", a_), string.Empty);
			num = 14;
		}
		IL_6B:
		throw new ArgumentNullException(RecordTableEnumerator.b("㹈㥊⑌㭎㑐⅒", a_));
		IL_BD:
		throw new ArgumentNullException(RecordTableEnumerator.b("ⅈ⑊⅌⭎㑐⅒", a_));
		IL_123:
		throw new ArgumentNullException(RecordTableEnumerator.b("⩈⍊ⱌ㵎═", a_));
		IL_1E4:
		return;
		IL_1E9:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㩈㽊㽌ᵎ㑐㽒㑔⍖じ㑚㍜ᙞՠ", a_));
		IL_2B3:
		goto IL_1E9;
	}

	// Token: 0x06002C3F RID: 11327 RVA: 0x0018B6E8 File Offset: 0x0018A6E8
	private new void ᜀ(XmlWriter A_0, XlsChartShape A_1, string A_2)
	{
		int a_ = 13;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				goto IL_EB;
			case 1:
				goto IL_6E;
			case 2:
				goto IL_51;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_EB;
				default:
					goto IL_86;
				}
				break;
			case 4:
				num = 6;
				continue;
			case 6:
				if (A_2.Length == 0)
				{
					num = 1;
					continue;
				}
				goto IL_109;
			case 7:
				if (A_2 != null)
				{
					num = 4;
					continue;
				}
				goto IL_B4;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			num = 0;
			continue;
			IL_EB:
			num = 7;
		}
		IL_51:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑂㝄⹆㵈⹊㽌", a_));
		IL_6E:
		goto IL_B4;
		IL_86:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("⁂ⵄ♆㭈㽊", a_));
		IL_B4:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("あㅄ㕆ᭈ⹊⅌⹎═㩒㩔㥖ၘ㽚", a_));
		IL_109:
		A_0.WriteStartElement(RecordTableEnumerator.b("⑂㝄♆㥈⍊⑌ⱎ", a_), RecordTableEnumerator.b("⭂ㅄ㍆㥈煊扌恎≐げ㵔㉖㑘㩚⹜煞๠።d०ᅨ٪Ŭ८ṰŲᡴᙶ൸ࡺ卼ၾꪄﮈ戴ﾐﮖ뚘ꦚ궜꾞鞠貢좤욦삨얪", a_));
		A_0.WriteStartElement(RecordTableEnumerator.b("⑂㝄♆㥈⍊⑌ⱎᕐ㉒⅔㙖", a_), RecordTableEnumerator.b("⭂ㅄ㍆㥈煊扌恎≐げ㵔㉖㑘㩚⹜煞๠።d०ᅨ٪Ŭ८ṰŲᡴᙶ൸ࡺ卼ၾꪄﮈ戴ﾐﮖ뚘ꦚ궜꾞鞠貢좤욦삨얪", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("㙂㝄⹆", a_), RecordTableEnumerator.b("⭂ㅄ㍆㥈煊扌恎≐げ㵔㉖㑘㩚⹜煞๠።d०ᅨ٪Ŭ८ṰŲᡴᙶ൸ࡺ卼ၾꪄﮈ戴ﾐﮖ뚘ꦚ궜꾞鞠貢욤쾦좨\ud9aa\ud9ac", a_));
		A_0.WriteStartElement(RecordTableEnumerator.b("⁂", a_), RecordTableEnumerator.b("⁂ⵄ♆㭈㽊", a_), RecordTableEnumerator.b("⭂ㅄ㍆㥈煊扌恎≐げ㵔㉖㑘㩚⹜煞๠።d०ᅨ٪Ŭ८ṰŲᡴᙶ൸ࡺ卼ၾꪄﮈ戴ﾐﮖ뚘ꦚ궜꾞鞠貢욤쾦좨\ud9aa\ud9ac", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("⩂⅄", a_), RecordTableEnumerator.b("⭂ㅄ㍆㥈煊扌恎≐げ㵔㉖㑘㩚⹜煞๠።d०ᅨ٪Ŭ८ṰŲᡴᙶ൸ࡺ卼ၾꪄ힒杖햠貢鞤鞦馨鶪芬\uddae풰\udfb2풴쎶킸풺펼첾꧀ꫂ뗄듆", a_), A_2);
		A_0.WriteEndElement();
		A_0.WriteEndElement();
		A_0.WriteEndElement();
	}

	// Token: 0x06002C40 RID: 11328 RVA: 0x0018B8CC File Offset: 0x0018A8CC
	private new void ᜀ(XmlWriter A_0, XlsChartShape A_1, sprᡟ A_2)
	{
		int a_ = 3;
		for (;;)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_1 == null)
					{
						num = 1;
						continue;
					}
					goto IL_85;
				case 1:
					goto IL_65;
				case 2:
					goto IL_3C;
				}
				if (A_0 == null)
				{
					if (true)
					{
					}
					num = 2;
				}
				else
				{
					num = 0;
				}
			}
			IL_85:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_9B;
			}
		}
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("丸䤺吼䬾⑀ㅂ", a_));
		IL_65:
		throw new ArgumentNullException(RecordTableEnumerator.b("娸区尼䴾㕀", a_));
		IL_9B:
		if (false)
		{
		}
		A_0.WriteStartElement(RecordTableEnumerator.b("圸䴺稼䴾⁀㍂ⵄ⹆⩈ൊ㽌⹎㱐㙒Ք╖", a_), RecordTableEnumerator.b("儸伺䤼伾筀求橄㑆⩈⍊⡌≎ぐ⁒答㡖⥘㹚㍜❞ౠརͤࡦ᭨٪౬᭮ɰ嵲ᩴնṸ呺᥼ൾꂎꎐꎒꖔꆖ뚘쒠슢솤풦솨캪좬\udbae솲풴삶킸햺\udabc", a_));
		base.ᜀ(A_0, A_1, A_2, RecordTableEnumerator.b("儸伺䤼伾筀求橄㑆⩈⍊⡌≎ぐ⁒答㡖⥘㹚㍜❞ౠརͤࡦ᭨٪౬᭮ɰ嵲ᩴնṸ呺᥼ൾꂎꎐꎒꖔꆖ뚘쒠슢솤풦솨캪좬\udbae솲풴삶킸햺\udabc", a_));
		A_0.WriteElementString(RecordTableEnumerator.b("娸町䬼砾㍀≂㕄⽆⁈⡊ୌ㵎ぐ㹒ごݖ⭘", a_), RecordTableEnumerator.b("儸伺䤼伾筀求橄㑆⩈⍊⡌≎ぐ⁒答㡖⥘㹚㍜❞ౠརͤࡦ᭨٪౬᭮ɰ嵲ᩴնṸ呺᥼ൾꂎꎐꎒꖔꆖ뚘쒠슢솤풦솨캪좬\udbae솲풴삶킸햺\udabc", a_), string.Empty);
		A_0.WriteEndElement();
	}

	// Token: 0x04001467 RID: 5223
	public new const string ᜀ = "/xl/charts/chart{0}.xml";
}
