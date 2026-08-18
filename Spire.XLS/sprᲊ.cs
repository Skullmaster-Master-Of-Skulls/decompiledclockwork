using System;
using System.Xml;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlSerialization;

// Token: 0x0200051C RID: 1308
internal class spr\u1C8A : spr\u1A78
{
	// Token: 0x06004F5F RID: 20319 RVA: 0x00300534 File Offset: 0x002FF534
	public override void ᜀ(XmlWriter A_0, XlsShape A_1, sprᡟ A_2, RelationsCollection A_3)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			int num = 5;
			XlsBitmapShape xlsBitmapShape;
			string text;
			string a_2;
			for (;;)
			{
				bool flag;
				string localName;
				switch (num)
				{
				case 0:
					A_0.WriteAttributeString(RecordTableEnumerator.b("尸强吼䬾@あ", a_), spr\u1A78.ᜀ(xlsBitmapShape));
					num = 3;
					continue;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_DE;
					default:
						goto IL_136;
					}
					break;
				case 2:
					goto IL_F0;
				case 3:
					goto IL_11B;
				case 4:
					goto IL_150;
				case 6:
					if (flag)
					{
						num = 8;
						continue;
					}
					localName = RecordTableEnumerator.b("䴸䰺刼簾⑀⽂⥄ن❈⡊╌⁎⍐", a_);
					text = RecordTableEnumerator.b("儸伺䤼伾筀求橄㑆⩈⍊⡌≎ぐ⁒答㡖⥘㹚㍜❞ౠརͤࡦ᭨٪౬᭮ɰ嵲ᩴնṸ呺᥼ൾꂎꎐꎒꖔꆖ뚘쒠슢솤풦솨캪좬\udbae솲풴삶킸햺\udabc", a_);
					num = 10;
					continue;
				case 7:
					if (!flag)
					{
						num = 0;
						continue;
					}
					goto IL_1DB;
				case 8:
					if (true)
					{
					}
					localName = RecordTableEnumerator.b("䬸帺儼氾⡀㥂⁄ن❈⡊╌⁎⍐", a_);
					text = RecordTableEnumerator.b("儸伺䤼伾筀求橄㑆⩈⍊⡌≎ぐ⁒答㡖⥘㹚㍜❞ౠརͤࡦ᭨٪౬᭮ɰ嵲ᩴնṸ呺᥼ൾꂎꎐꎒꖔꆖ뚘ﺞ펠힢햦좨\udcaa쒬솮횰", a_);
					num = 4;
					continue;
				case 9:
					goto IL_DE;
				case 10:
					goto IL_150;
				}
				if (A_0 == null)
				{
					num = 1;
					continue;
				}
				xlsBitmapShape = (A_1 as XlsBitmapShape);
				num = 9;
				continue;
				IL_DE:
				if (xlsBitmapShape == null)
				{
					num = 2;
					continue;
				}
				A_2.ᜋ();
				a_2 = this.ᜀ(A_2, xlsBitmapShape, A_3);
				flag = !(A_1.Worksheet is XlsWorksheet);
				num = 6;
				continue;
				IL_150:
				A_0.WriteStartElement(localName, text);
				num = 7;
			}
			IL_F0:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䤸刺帼䬾㑀ㅂ⁄", a_));
			IL_11B:
			goto IL_1DB;
			IL_136:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("丸䤺吼䬾⑀ㅂ", a_));
			IL_1DB:
			base.ᜁ(A_0, RecordTableEnumerator.b("弸䤺刼刾", a_), A_1.LeftColumn, A_1.LeftColumnOffset, A_1.TopRow, A_1.TopRowOffset, A_1.Worksheet, text);
			base.ᜁ(A_0, RecordTableEnumerator.b("䴸吺", a_), A_1.RightColumn, A_1.RightColumnOffset, A_1.BottomRow, A_1.BottomRowOffset, A_1.Worksheet, text);
			this.ᜀ(A_0, xlsBitmapShape, a_2, A_2, text);
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06004F60 RID: 20320 RVA: 0x00300798 File Offset: 0x002FF798
	private new void ᜀ(XmlWriter A_0, XlsBitmapShape A_1, string A_2, sprᡟ A_3, string A_4)
	{
		int a_ = 1;
		int num = 3;
		for (;;)
		{
			string macro;
			switch (num)
			{
			case 0:
				goto IL_5C;
			case 1:
				goto IL_216;
			case 2:
				num = 9;
				continue;
			case 4:
				if (A_1 == null)
				{
					if (true)
					{
					}
					num = 11;
					continue;
				}
				num = 10;
				continue;
			case 5:
				goto IL_1A5;
			case 6:
				goto IL_C7;
			case 7:
				goto IL_61;
			case 8:
				if (macro != null)
				{
					num = 7;
					continue;
				}
				goto IL_C7;
			case 9:
				if (A_2.Length != 0)
				{
					A_0.WriteStartElement(RecordTableEnumerator.b("䜶倸堺", a_), A_4);
					macro = A_1.Macro;
					num = 8;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_61;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				break;
			case 10:
				if (A_2 != null)
				{
					num = 2;
					continue;
				}
				goto IL_80;
			case 11:
				goto IL_C2;
			case 12:
				if (A_4 == RecordTableEnumerator.b("弶䴸伺䴼Ծ湀求㙄⑆ⅈ⹊⁌⹎≐絒㩔❖㱘㕚╜㉞ൠբ੤ᕦѨ੪ᥬᱮ彰ᱲݴၶ噸ὺོṾꊌ붎ꆐꎒꎔ뢖爵삠잢횤쾦첨캪\ud9ac쎰튲슴\udeb6ힸ\udcba", a_))
				{
					num = 13;
					continue;
				}
				return;
			case 13:
				A_0.WriteStartElement(RecordTableEnumerator.b("吶唸刺堼儾㕀݂⑄㍆⡈", a_), RecordTableEnumerator.b("弶䴸伺䴼Ծ湀求㙄⑆ⅈ⹊⁌⹎≐絒㩔❖㱘㕚╜㉞ൠբ੤ᕦѨ੪ᥬᱮ彰ᱲݴၶ噸ὺོṾꊌ붎ꆐꎒꎔ뢖爵삠잢횤쾦첨캪\ud9ac쎰튲슴\udeb6ힸ\udcba", a_));
				A_0.WriteEndElement();
				num = 5;
				continue;
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			num = 4;
			continue;
			IL_61:
			A_0.WriteAttributeString(RecordTableEnumerator.b("娶堸堺似倾", a_), macro);
			num = 6;
			continue;
			IL_C7:
			this.ᜀ(A_0, A_1, A_3, A_4);
			this.ᜀ(A_0, A_1, A_2, A_4);
			this.ᜁ(A_0, A_1, A_4);
			A_0.WriteEndElement();
			num = 12;
		}
		IL_5C:
		throw new ArgumentNullException(RecordTableEnumerator.b("䀶䬸刺䤼娾㍀", a_));
		IL_80:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䔶尸场尼䬾⡀ⱂ⭄ๆⵈ", a_));
		IL_C2:
		throw new ArgumentNullException(RecordTableEnumerator.b("䜶倸堺䤼䨾㍀♂", a_));
		IL_1A5:
		return;
		IL_216:
		goto IL_80;
	}

	// Token: 0x06004F61 RID: 20321 RVA: 0x003009C0 File Offset: 0x002FF9C0
	private new void ᜁ(XmlWriter A_0, XlsBitmapShape A_1, string A_2)
	{
		int a_ = 7;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_B1;
			case 1:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				num = 4;
				continue;
			case 2:
				goto IL_56;
			case 3:
				goto IL_3C;
			case 4:
				while (A_1.ShapePropertiesStream == null)
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
						goto IL_D1;
					}
				}
				num = 2;
				continue;
			}
			if (A_0 == null)
			{
				num = 3;
			}
			else
			{
				num = 1;
			}
		}
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨼䴾⡀㝂⁄㕆", a_));
		IL_56:
		spr\u1B7A.ᜀ(A_0, A_1.ShapePropertiesStream, null);
		return;
		IL_B1:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴼嘾≀㝂い㕆ⱈ", a_));
		IL_D1:
		if (false)
		{
		}
		A_0.WriteStartElement(RecordTableEnumerator.b("丼伾ᅀㅂ", a_), A_2);
		spr\u1A78.ᜀ(A_0, RecordTableEnumerator.b("唼䬾㕀㍂罄框晈㡊⹌❎㑐㹒㑔⑖睘㑚ⵜ㩞འ᭢ࡤ୦ཨѪὬɮၰݲٴ奶ᙸॺ᩼偾﶐벒ꞔꞖꦘ궚늜삠쪢쮤", a_), RecordTableEnumerator.b("唼䬾㕀㍂罄框晈㡊⹌❎㑐㹒㑔⑖睘㑚ⵜ㩞འ᭢ࡤ୦ཨѪὬɮၰݲٴ奶ᙸॺ᩼偾﶐벒ꞔꞖꦘ궚늜삠쪢쮤", a_), 0, 1, 2076450, 1557338, A_1);
		base.ᜀ(A_0);
		A_0.WriteEndElement();
	}

	// Token: 0x06004F62 RID: 20322 RVA: 0x00300AF8 File Offset: 0x002FFAF8
	private new void ᜀ(XmlWriter A_0, XlsBitmapShape A_1, sprᡟ A_2, string A_3)
	{
		int a_ = 0;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_3E;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_8D;
				default:
					if (false)
					{
					}
					if (A_1 == null)
					{
						num = 2;
						continue;
					}
					goto IL_A1;
				}
				break;
			case 2:
				goto IL_83;
			}
			if (A_0 == null)
			{
				num = 0;
			}
			else
			{
				num = 1;
			}
		}
		IL_3E:
		goto IL_8D;
		IL_83:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䘵儷夹䠻䬽㈿❁", a_));
		IL_8D:
		throw new ArgumentNullException(RecordTableEnumerator.b("䄵䨷匹䠻嬽㈿", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("堵丷樹唻崽ဿぁ", a_), A_3);
		base.ᜀ(A_0, A_1, A_2, A_3);
		this.ᜀ(A_0, A_1, A_3);
		A_0.WriteEndElement();
	}

	// Token: 0x06004F63 RID: 20323 RVA: 0x00300BD8 File Offset: 0x002FFBD8
	private new void ᜀ(XmlWriter A_0, XlsBitmapShape A_1, string A_2)
	{
		int a_ = 13;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_8B;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_8D;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					if (A_1 == null)
					{
						num = 0;
						continue;
					}
					goto IL_A1;
				}
				break;
			case 3:
				goto IL_3E;
			}
			if (A_0 == null)
			{
				num = 3;
			}
			else
			{
				num = 1;
			}
		}
		IL_3E:
		goto IL_8D;
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("㍂ⱄ⑆㵈㹊㽌⩎", a_));
		IL_8D:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑂㝄⹆㵈⹊㽌", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("⁂ୄㅆ᥈≊⹌὎⍐", a_), A_2);
		A_0.WriteStartElement(RecordTableEnumerator.b("㍂ⱄ⑆Ո⑊⹌⑎≐", a_), RecordTableEnumerator.b("⭂ㅄ㍆㥈煊扌恎≐げ㵔㉖㑘㩚⹜煞๠።d०ᅨ٪Ŭ८ṰŲᡴᙶ൸ࡺ卼ၾꪄﮈ戴ﾐﮖ뚘ꦚ궜꾞鞠貢좤욦삨얪", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("ⵂ⩄цⅈ⩊⍌⡎㑐ቒ♔❖㱘㡚⥜", a_), RecordTableEnumerator.b("牂", a_));
		A_0.WriteEndElement();
		A_0.WriteEndElement();
	}

	// Token: 0x06004F64 RID: 20324 RVA: 0x00300CEC File Offset: 0x002FFCEC
	private new void ᜀ(XmlWriter A_0, XlsBitmapShape A_1, string A_2, string A_3)
	{
		int a_ = 8;
		int num = 8;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1.BlipSubNodesStream != null)
				{
					num = 5;
					continue;
				}
				goto IL_CC;
			case 1:
				if (A_2 != null)
				{
					num = 6;
					continue;
				}
				goto IL_85;
			case 2:
				goto IL_223;
			case 3:
				if (A_1 == null)
				{
					if (true)
					{
					}
					num = 10;
					continue;
				}
				num = 1;
				continue;
			case 4:
				if (A_1.SourceRectStream != null)
				{
					num = 9;
					continue;
				}
				goto IL_228;
			case 5:
				goto IL_61;
			case 6:
				num = 7;
				continue;
			case 7:
				if (A_2.Length != 0)
				{
					A_0.WriteStartElement(RecordTableEnumerator.b("尽ⰿ⭁㑃Eⅇ♉⁋", a_), A_3);
					A_0.WriteStartElement(RecordTableEnumerator.b("尽ⰿ⭁㑃", a_), RecordTableEnumerator.b("嘽㐿㙁㑃籅杇敉㽋ⵍ㡏㝑㥓㝕⭗瑙㍛⹝՟ౡᱣ୥ѧ౩ͫᱭᵯ፱sյ噷ᕹ๻᥽꽿ﾇﶏﺑ뮓꒕ꢗꪙꪛ놝춟쎡춣좥", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("嬽ⴿ⁁⅃≅", a_), RecordTableEnumerator.b("嘽㐿㙁㑃籅杇敉㽋ⵍ㡏㝑㥓㝕⭗瑙㍛⹝՟ౡᱣ୥ѧ౩ͫᱭᵯ፱sյ噷ᕹ๻᥽꽿쪍ﾏﮕﶗ놝銟銡钣邥螧\ud8a9즫슭톯욱\uddb3\ud9b5횷즹풻ힽ낿뇁", a_), A_2);
					num = 0;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_61;
				default:
					if (false)
					{
					}
					num = 2;
					continue;
				}
				break;
			case 9:
				spr\u1B7A.ᜀ(A_0, A_1.SourceRectStream, RecordTableEnumerator.b("䰽⼿ⵁぃ", a_));
				num = 13;
				continue;
			case 10:
				goto IL_C7;
			case 11:
				goto IL_CC;
			case 12:
				goto IL_5C;
			case 13:
				goto IL_1B2;
			}
			if (A_0 == null)
			{
				num = 12;
				continue;
			}
			num = 3;
			continue;
			IL_61:
			spr\u1B7A.ᜀ(A_0, A_1.BlipSubNodesStream, RecordTableEnumerator.b("䰽⼿ⵁぃ", a_));
			num = 11;
			continue;
			IL_CC:
			A_0.WriteEndElement();
			num = 4;
		}
		IL_5C:
		throw new ArgumentNullException(RecordTableEnumerator.b("䤽㈿⭁ぃ⍅㩇", a_));
		IL_85:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䰽┿⹁╃㉅ⅇ╉≋ݍ㑏", a_));
		IL_C7:
		throw new ArgumentNullException(RecordTableEnumerator.b("丽⤿⅁ぃ㍅㩇⽉", a_));
		IL_1B2:
		goto IL_228;
		IL_223:
		goto IL_85;
		IL_228:
		A_0.WriteStartElement(RecordTableEnumerator.b("䴽㐿ぁ⅃㉅⭇≉", a_), RecordTableEnumerator.b("嘽㐿㙁㑃籅杇敉㽋ⵍ㡏㝑㥓㝕⭗瑙㍛⹝՟ౡᱣ୥ѧ౩ͫᱭᵯ፱sյ噷ᕹ๻᥽꽿ﾇﶏﺑ뮓꒕ꢗꪙꪛ놝춟쎡춣좥", a_));
		A_0.WriteElementString(RecordTableEnumerator.b("堽⤿⹁⡃ᑅⵇ⥉㡋", a_), RecordTableEnumerator.b("嘽㐿㙁㑃籅杇敉㽋ⵍ㡏㝑㥓㝕⭗瑙㍛⹝՟ౡᱣ୥ѧ౩ͫᱭᵯ፱sյ噷ᕹ๻᥽꽿ﾇﶏﺑ뮓꒕ꢗꪙꪛ놝춟쎡춣좥", a_), string.Empty);
		A_0.WriteEndElement();
		A_0.WriteEndElement();
	}

	// Token: 0x06004F65 RID: 20325 RVA: 0x00300F78 File Offset: 0x002FFF78
	public override void ᜀ(XmlWriter A_0, Type A_1)
	{
		int a_ = 9;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		throw new Exception(RecordTableEnumerator.b("款⥀♂敄⩆ⱈ㽊╌⁎㕐獒㩔╖祘㑚ⵜ㩞፠ɢᅤ๦٨ժ䵬ٮɰ卲᭴ᡶ൸孺ᑼቾ歷붒", a_));
	}

	// Token: 0x06004F66 RID: 20326 RVA: 0x00300FD0 File Offset: 0x002FFFD0
	public new string ᜀ(sprᡟ A_0, XlsBitmapShape A_1, RelationsCollection A_2)
	{
		int a_ = 16;
		int num = 5;
		string text2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_4D;
			case 1:
			{
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				RelationsCollection relationsCollection = A_0.ᜈ();
				string text = '/' + A_0.ᜋ().ᜃ((int)(A_1.BlipId - 1U));
				text2 = relationsCollection.FindRelationByTarget(text);
				num = 6;
				continue;
			}
			case 2:
				goto IL_130;
			case 3:
				goto IL_F6;
			case 4:
			{
				RelationsCollection relationsCollection;
				text2 = relationsCollection.GenerateRelationId();
				string text;
				relationsCollection[text2] = new sprᦨ(text, RecordTableEnumerator.b("⹅㱇㹉㱋瑍罏絑❓㕕し㽙ㅛ㽝፟䱡ୣᙥ൧ѩᑫͭᱯᑱ᭳ѵᕷ᭹ࡻൽ깿ꞇ憐튕蓮얟첡킣覥骧骩鲫颭龯삱톳\udab5\ud9b7캹햻톽꺿뇁곃꿅룇막ꟍ뷏돑돓돕", a_));
				goto IL_E3;
			}
			case 6:
				if (text2 == null)
				{
					num = 4;
					continue;
				}
				return text2;
			}
			if (A_0 == null)
			{
				num = 0;
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
				num = 1;
				continue;
			}
			IL_E3:
			if (true)
			{
			}
			num = 3;
		}
		IL_4D:
		throw new ArgumentNullException(RecordTableEnumerator.b("⹅❇♉⡋⭍≏", a_));
		IL_F6:
		return text2;
		IL_130:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙅ⅇ⥉㡋㭍≏㝑", a_));
	}
}
