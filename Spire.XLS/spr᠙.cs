using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Xml;
using Spire.Xls;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlSerialization;

// Token: 0x0200023B RID: 571
internal class spr᠙ : spr\u2175
{
	// Token: 0x06002298 RID: 8856 RVA: 0x00135958 File Offset: 0x00134958
	public override void ᜀ(XmlWriter A_0, XlsShape A_1, sprᡟ A_2, RelationsCollection A_3)
	{
		int a_ = 11;
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_4D;
				case 2:
					goto IL_78;
				case 3:
					if (A_1 == null)
					{
						num = 2;
						continue;
					}
					goto IL_B4;
				}
				if (true)
				{
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
			IL_4D:
			goto IL_A0;
			IL_78:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_A0:
				throw new ArgumentNullException(RecordTableEnumerator.b("㙀ㅂⱄ㍆ⱈ㥊", a_));
			default:
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("㉀⭂⑄㝆ⱈ", a_));
			}
			IL_B4:
			string value = '#' + string.Format(RecordTableEnumerator.b("Ṁ㭂畄睆祈筊ቌ㭎⩐捒⡔", a_), A_1.InnerSpRecord.\u1714());
			A_0.WriteStartElement(RecordTableEnumerator.b("㉀⭂⑄㝆ⱈ", a_), RecordTableEnumerator.b("㑀ㅂ⭄絆㩈⡊╌⩎㱐㉒♔穖㑘㉚㹜ⵞ๠ၢ੤Ŧᵨ䙪๬nᱰ䥲ʹ᩶ᕸ", a_));
			A_0.WriteAttributeString(RecordTableEnumerator.b("⡀❂", a_), A_1.Name);
			A_0.WriteAttributeString(RecordTableEnumerator.b("㕀㩂㕄≆", a_), value);
			XlsBitmapShape xlsBitmapShape = (XlsBitmapShape)A_1;
			Image picture = xlsBitmapShape.Picture;
			XlsPageSetupBase pageSetupBase = xlsBitmapShape.Worksheet.PageSetupBase;
			string value2 = string.Format(CultureInfo.InvariantCulture.NumberFormat, RecordTableEnumerator.b("㙀⩂⅄㍆ⅈ煊㙌罎ⱐ⍒⅔汖ㅘ㹚㑜㡞ॠᝢ彤ᱦ塨ᙪᵬ᭮", a_), new object[]
			{
				spr\u17FF.ᜀ((double)picture.Width, MeasureUnits.Point),
				spr\u17FF.ᜀ((double)picture.Height, MeasureUnits.Point)
			});
			A_0.WriteAttributeString(RecordTableEnumerator.b("㉀㝂㱄⭆ⱈ", a_), value2);
			this.ᜀ(A_0, A_1, A_2, null, false, A_2.ᜏ());
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06002299 RID: 8857 RVA: 0x00135B34 File Offset: 0x00134B34
	public override void ᜀ(XmlWriter A_0, Type A_1)
	{
		int a_ = 2;
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				int num2;
				int num3;
				switch (num)
				{
				case 0:
					goto IL_4D;
				case 1:
					goto IL_280;
				case 2:
					goto IL_2BB;
				case 4:
					if (num2 < num3)
					{
						A_0.WriteStartElement(RecordTableEnumerator.b("帷", a_), RecordTableEnumerator.b("䴷䠹刻н㌿⅁ⱃ⍅╇⭉㽋捍㵏㭑㝓⑕㝗⥙㍛㡝ᑟ佡ݣ॥է偩ᩫͭᱯ", a_));
						A_0.WriteAttributeString(RecordTableEnumerator.b("崷䬹刻", a_), spr᠙.ᜀ[num2]);
						A_0.WriteEndElement();
						num2++;
						num = 5;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_25E;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 5:
					goto IL_280;
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("䬷刹崻丽┿㙁㵃㙅ⵇ", a_), RecordTableEnumerator.b("䴷䠹刻н㌿⅁ⱃ⍅╇⭉㽋捍㵏㭑㝓⑕㝗⥙㍛㡝ᑟ佡ݣ॥է偩ᩫͭᱯ", a_));
				string value = string.Format(RecordTableEnumerator.b("朷䈹఻฽瀿牁ᭃ㉅㍇穉ㅋ", a_), 75);
				A_0.WriteAttributeString(RecordTableEnumerator.b("儷帹", a_), value);
				A_0.WriteAttributeString(RecordTableEnumerator.b("嬷唹医䰽␿ㅁⵃ㱅ⵇ", a_), RecordTableEnumerator.b("਷ହ਻฽瀿湁癃睅繇穉籋", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("䬷䨹䠻", a_), RecordTableEnumerator.b("䴷䠹刻н㌿⅁ⱃ⍅╇⭉㽋捍㵏㭑㝓⑕㝗⥙㍛㡝ᑟ佡ݣ॥է偩ͫ࡭ᙯ᭱ᝳ፵䉷ᕹ᩻᡽", a_), 75.ToString());
				A_0.WriteAttributeString(RecordTableEnumerator.b("䠷䠹夻堽┿ぁ㙃⍅⑇⭉㡋❍♏㝑", a_), RecordTableEnumerator.b("䴷䠹刻н㌿⅁ⱃ⍅╇⭉㽋捍㵏㭑㝓⑕㝗⥙㍛㡝ᑟ佡ݣ॥է偩ͫ࡭ᙯ᭱ᝳ፵䉷ᕹ᩻᡽", a_), RecordTableEnumerator.b("䰷", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("䠷嬹䠻嘽", a_), RecordTableEnumerator.b("唷稹࠻總甿⹁у牅ࡇ等絋ํ楏ቑ敓杕ᡗ捙ᱛ歝ᡟݡ", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("帷匹倻刽┿♁", a_), RecordTableEnumerator.b("帷", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("䬷丹主儽⬿❁⁃", a_), RecordTableEnumerator.b("帷", a_));
				A_0.WriteStartElement(RecordTableEnumerator.b("䬷丹主儽⬿❁", a_), RecordTableEnumerator.b("䴷䠹刻н㌿⅁ⱃ⍅╇⭉㽋捍㵏㭑㝓⑕㝗⥙㍛㡝ᑟ佡ݣ॥է偩ᩫͭᱯ", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("刷唹唻倽㌿㙁㵃⩅ⵇ", a_), RecordTableEnumerator.b("唷匹䠻嬽㈿", a_));
				A_0.WriteEndElement();
				A_0.WriteStartElement(RecordTableEnumerator.b("帷唹主匽㔿⹁╃㕅", a_), RecordTableEnumerator.b("䴷䠹刻н㌿⅁ⱃ⍅╇⭉㽋捍㵏㭑㝓⑕㝗⥙㍛㡝ᑟ佡ݣ॥է偩ᩫͭᱯ", a_));
				num2 = 0;
				num3 = spr᠙.ᜀ.Length;
				IL_25E:
				num = 1;
				continue;
				IL_280:
				num = 4;
			}
			IL_4D:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("伷䠹唻䨽┿ぁ", a_));
			IL_2BB:
			A_0.WriteEndElement();
			A_0.WriteStartElement(RecordTableEnumerator.b("䠷嬹䠻嘽", a_), RecordTableEnumerator.b("䴷䠹刻н㌿⅁ⱃ⍅╇⭉㽋捍㵏㭑㝓⑕㝗⥙㍛㡝ᑟ佡ݣ॥է偩ᩫͭᱯ", a_));
			A_0.WriteAttributeString(RecordTableEnumerator.b("崷䈹䠻䰽㔿ㅁⵃ⥅♇╉❋", a_), RecordTableEnumerator.b("䴷䠹刻н㌿⅁ⱃ⍅╇⭉㽋捍㵏㭑㝓⑕㝗⥙㍛㡝ᑟ佡ݣ॥է偩ͫ࡭ᙯ᭱ᝳ፵䉷ᕹ᩻᡽", a_), RecordTableEnumerator.b("帷", a_));
			A_0.WriteAttributeString(RecordTableEnumerator.b("強䠹崻娽⤿❁⩃㉅㭇≉ⵋ㹍㕏㵑㽓", a_), RecordTableEnumerator.b("䰷", a_));
			A_0.WriteAttributeString(RecordTableEnumerator.b("嬷唹刻倽┿⅁ぃ㉅ㅇ㩉⥋", a_), RecordTableEnumerator.b("䴷䠹刻н㌿⅁ⱃ⍅╇⭉㽋捍㵏㭑㝓⑕㝗⥙㍛㡝ᑟ佡ݣ॥է偩ͫ࡭ᙯ᭱ᝳ፵䉷ᕹ᩻᡽", a_), RecordTableEnumerator.b("䨷弹弻䨽", a_));
			A_0.WriteEndElement();
			A_0.WriteStartElement(RecordTableEnumerator.b("吷唹弻唽", a_), RecordTableEnumerator.b("䴷䠹刻н㌿⅁ⱃ⍅╇⭉㽋捍㵏㭑㝓⑕㝗⥙㍛㡝ᑟ佡ݣ॥է偩ͫ࡭ᙯ᭱ᝳ፵䉷ᕹ᩻᡽", a_));
			A_0.WriteAttributeString(RecordTableEnumerator.b("崷䈹䠻", a_), RecordTableEnumerator.b("䴷䠹刻н㌿⅁ⱃ⍅╇⭉㽋捍㵏㭑㝓⑕㝗⥙㍛㡝ᑟ佡ݣ॥է偩ᩫͭᱯ", a_), RecordTableEnumerator.b("崷帹唻䨽", a_));
			A_0.WriteAttributeString(RecordTableEnumerator.b("夷䤹䰻嬽⌿㙁㙃❅㱇⍉⍋", a_), RecordTableEnumerator.b("䰷", a_));
			A_0.WriteEndElement();
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x0600229A RID: 8858 RVA: 0x00135F38 File Offset: 0x00134F38
	internal new void ᜀ(XmlWriter A_0, XlsShape A_1, sprᡟ A_2, string A_3, bool A_4, RelationsCollection A_5)
	{
		int a_ = 18;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_15C;
			case 2:
				goto IL_43;
			case 3:
				goto IL_136;
			case 4:
				A_0.WriteAttributeString(RecordTableEnumerator.b("㱇⍉㡋≍㕏", a_), RecordTableEnumerator.b("㵇㡉≋瑍⍏ㅑ㱓㍕㕗㭙⽛獝ൟୡݣᑥݧᥩͫ࡭ѯ影ᝳ᥵ᕷ䁹፻᡽늇憐", a_), A_3);
				num = 3;
				continue;
			case 5:
			{
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("ⅇ❉ⵋ⥍㕏㙑㕓≕㥗", a_), RecordTableEnumerator.b("㵇㡉≋瑍⍏ㅑ㱓㍕㕗㭙⽛獝ൟୡݣᑥݧᥩͫ࡭ѯ影ᝳ᥵ᕷ䁹੻፽", a_));
				string value = this.ᜀ(A_1, A_2, A_4, A_5);
				A_0.WriteAttributeString(RecordTableEnumerator.b("㩇⽉⁋❍㑏", a_), RecordTableEnumerator.b("㵇㡉≋瑍⍏ㅑ㱓㍕㕗㭙⽛獝ൟୡݣᑥݧᥩͫ࡭ѯ影ᝳ᥵ᕷ䁹፻᡽늇憐", a_), value);
				num = 6;
				continue;
			}
			case 6:
				if (A_3 != null)
				{
					num = 4;
					continue;
				}
				goto IL_161;
			}
			if (A_0 == null)
			{
				num = 2;
			}
			else
			{
				if (true)
				{
				}
				num = 5;
			}
		}
		IL_43:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_15C:
			throw new ArgumentNullException(RecordTableEnumerator.b("㭇≉ⵋ㹍㕏", a_));
		default:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㽇㡉╋㩍㕏⁑", a_));
		}
		IL_136:
		IL_161:
		A_0.WriteEndElement();
	}

	// Token: 0x0600229B RID: 8859 RVA: 0x001360AC File Offset: 0x001350AC
	protected new virtual string ᜀ(XlsShape A_0, sprᡟ A_1, bool A_2, RelationsCollection A_3)
	{
		int a_ = 11;
		switch (0)
		{
		default:
		{
			int num = 0;
			Image picture;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_74;
				case 2:
					goto IL_4D;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_93;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 4:
					if (!A_2)
					{
						num = 3;
						continue;
					}
					num = 5;
					continue;
				case 5:
					goto IL_D5;
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				XlsBitmapShape xlsBitmapShape = A_0 as XlsBitmapShape;
				picture = xlsBitmapShape.Picture;
				IL_93:
				num = 4;
			}
			IL_4D:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㉀⭂⑄㝆ⱈ", a_));
			IL_74:
			ImageFormat imageFormat = ImageFormat.Png;
			goto IL_DD;
			IL_D5:
			imageFormat = picture.RawFormat;
			IL_DD:
			ImageFormat imageFormat2 = imageFormat;
			sprវ sprវ = A_1.ᜋ();
			A_1.ᜋ().ᜁ(imageFormat2);
			string arg = sprវ.ᜀ(picture, imageFormat2, null);
			string text = A_3.GenerateRelationId();
			A_3[text] = new sprᦨ('/' + arg, RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂햐ﲒﺚ躠醢閤鞦龨蒪\udfac쪮\uddb0튲솴\udeb6횸햺캼ힾꣀ돂뛄ꃈꛊ곌꣎듐", a_));
			return text;
		}
		}
	}

	// Token: 0x0600229C RID: 8860 RVA: 0x001361EC File Offset: 0x001351EC
	private new string ᜀ(XlsShape A_0, sprᡟ A_1)
	{
		int a_ = 4;
		if (A_0 == null)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_24;
				}
			}
			IL_24:
			if (true)
			{
			}
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䤹吻弽〿❁", a_));
		}
		return this.ᜀ(A_0, A_1, false, A_1.ᜏ());
	}

	// Token: 0x0600229E RID: 8862 RVA: 0x0013626C File Offset: 0x0013526C
	// Note: this type is marked as 'beforefieldinit'.
	static spr᠙()
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
		spr᠙.ᜀ = new string[]
		{
			RecordTableEnumerator.b("⡀╂敄⭆⁈╊⡌୎⍐㉒≔㥖祘⭚㑜❞Ѡར⥤๦ݨ๪㩬ٮᕰݲᵴ坶䥸", a_),
			RecordTableEnumerator.b("㉀㙂⡄杆ै筊浌繎煐捒", a_),
			RecordTableEnumerator.b("㉀㙂⡄杆祈歊経潎ᅐ扒", a_),
			RecordTableEnumerator.b("ㅀㅂ⩄⍆楈୊罌潎恐獒杔", a_),
			RecordTableEnumerator.b("ㅀㅂ⩄⍆楈୊繌潎捐扒捔杖楘筚ⵜ㙞ᥠ٢।てhཪᥬݮ", a_),
			RecordTableEnumerator.b("ㅀㅂ⩄⍆楈୊繌潎捐扒捔杖楘筚ⵜ㙞ᥠ٢।⽦౨ɪ੬ݮհ", a_),
			RecordTableEnumerator.b("㉀㙂⡄杆ै筊浌罎煐扒", a_),
			RecordTableEnumerator.b("ㅀㅂ⩄⍆楈୊筌潎恐獒杔", a_),
			RecordTableEnumerator.b("ㅀㅂ⩄⍆楈୊穌潎捐扒捔杖楘筚ⵜ㙞ᥠ٢।てhཪᥬݮ", a_),
			RecordTableEnumerator.b("㉀㙂⡄杆ै獊浌絎恐敒敔杖祘歚", a_),
			RecordTableEnumerator.b("ㅀㅂ⩄⍆楈୊穌潎捐扒捔杖楘筚ⵜ㙞ᥠ٢।⽦౨ɪ੬ݮհ", a_),
			RecordTableEnumerator.b("㉀㙂⡄杆ै穊経潎捐扒捔杖楘筚浜", a_)
		};
	}

	// Token: 0x04001206 RID: 4614
	private new static string[] ᜀ;
}
