using System;
using System.Xml;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlSerialization;

// Token: 0x020004BA RID: 1210
internal class spr\u1979 : spr\u1A65
{
	// Token: 0x06004AB3 RID: 19123 RVA: 0x002D4AD4 File Offset: 0x002D3AD4
	protected override int ᜀ()
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
		return 201;
	}

	// Token: 0x06004AB4 RID: 19124 RVA: 0x002D4B14 File Offset: 0x002D3B14
	protected override string ᜁ()
	{
		int a_ = 12;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return RecordTableEnumerator.b("Łⱃ⍅⭇ⅉ⹋⅍⡏", a_);
	}

	// Token: 0x06004AB5 RID: 19125 RVA: 0x002D4B68 File Offset: 0x002D3B68
	public override void ᜀ(XmlWriter A_0, XlsShape A_1, sprᡟ A_2, RelationsCollection A_3)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				string value3;
				switch (num)
				{
				case 0:
				{
					sprថ sprថ;
					A_0.WriteAttributeString(RecordTableEnumerator.b("ⅇ⹉", a_), sprថ.Name);
					string value;
					A_0.WriteAttributeString(RecordTableEnumerator.b("㭇㩉╋⩍", a_), RecordTableEnumerator.b("㵇㡉≋瑍⍏ㅑ㱓㍕㕗㭙⽛獝ൟୡݣᑥݧᥩͫ࡭ѯ影ᝳ᥵ᕷ䁹፻᡽늇憐", a_), value);
					num = 22;
					continue;
				}
				case 1:
				{
					sprថ sprថ;
					if (sprថ.Line.BackColor != spr\u1D39.ᜂ)
					{
						num = 14;
						continue;
					}
					goto IL_CF;
				}
				case 2:
					goto IL_295;
				case 4:
					goto IL_CF;
				case 5:
				{
					sprថ sprថ;
					if (sprថ.Fill.FillType == ShapeFillType.SolidColor)
					{
						num = 25;
						continue;
					}
					goto IL_295;
				}
				case 6:
					goto IL_CA;
				case 7:
				{
					sprថ sprថ;
					this.ᜂ(A_0, sprថ, A_2.ᜋ(), A_3);
					num = 32;
					continue;
				}
				case 8:
					goto IL_295;
				case 9:
					goto IL_270;
				case 10:
					goto IL_190;
				case 11:
					goto IL_581;
				case 12:
				{
					sprថ sprថ;
					if (!string.IsNullOrEmpty(sprថ.Name))
					{
						goto IL_437;
					}
					string value;
					A_0.WriteAttributeString(RecordTableEnumerator.b("ⅇ⹉", a_), value);
					num = 34;
					continue;
				}
				case 13:
					A_0.WriteAttributeString(RecordTableEnumerator.b("⹇⍉⁋≍㕏㙑", a_), RecordTableEnumerator.b("⹇", a_));
					num = 28;
					continue;
				case 14:
				{
					sprថ sprថ;
					string value2 = base.ᜁ(sprថ.Line.BackColor);
					A_0.WriteAttributeString(RecordTableEnumerator.b("㭇㹉㹋⅍㭏㝑㝓㥕㑗㕙⹛", a_), value2);
					num = 4;
					continue;
				}
				case 15:
					A_0.WriteAttributeString(RecordTableEnumerator.b("㭇㹉㹋⅍㭏㝑こ", a_), RecordTableEnumerator.b("⹇", a_));
					num = 26;
					continue;
				case 16:
					goto IL_5BE;
				case 17:
				{
					sprថ sprថ;
					if (!sprថ.HasLineFormat)
					{
						num = 15;
						continue;
					}
					num = 1;
					continue;
				}
				case 18:
				{
					sprថ sprថ;
					A_0.WriteAttributeString(RecordTableEnumerator.b("⥇♉㡋", a_), sprថ.AlternativeText);
					num = 10;
					continue;
				}
				case 19:
				{
					sprថ sprថ;
					if (!sprថ.HasFill)
					{
						num = 13;
						continue;
					}
					num = 5;
					continue;
				}
				case 20:
				{
					sprថ sprថ;
					if (sprថ.AlternativeText != null)
					{
						num = 18;
						continue;
					}
					goto IL_190;
				}
				case 21:
				{
					sprថ sprថ;
					if (sprថ.HasFill)
					{
						num = 30;
						continue;
					}
					goto IL_270;
				}
				case 22:
					goto IL_361;
				case 23:
				{
					if (A_1 == null)
					{
						num = 11;
						continue;
					}
					sprថ sprថ = A_1 as sprថ;
					A_0.WriteStartElement(RecordTableEnumerator.b("㭇≉ⵋ㹍㕏", a_), RecordTableEnumerator.b("㵇㡉≋瑍⍏ㅑ㱓㍕㕗㭙⽛獝ൟୡݣᑥݧᥩͫ࡭ѯ影ᝳ᥵ᕷ䁹੻፽", a_));
					value3 = '#' + string.Format(RecordTableEnumerator.b("ᝇ㉉籋繍恏扑୓≕⍗橙⅛", a_), A_1.InnerSpRecord.\u1714());
					string value = string.Format(RecordTableEnumerator.b("ᝇ㉉籋繍恏扑୓╕⍗橙⅛", a_), A_1.ShapeId);
					num = 12;
					continue;
				}
				case 24:
				{
					if (true)
					{
					}
					sprថ sprថ;
					sprថ.HasFill = false;
					num = 2;
					continue;
				}
				case 25:
					num = 31;
					continue;
				case 26:
					goto IL_5BE;
				case 27:
				{
					sprថ sprថ;
					if (sprថ.Line.Weight > 0.0)
					{
						num = 33;
						continue;
					}
					goto IL_5BE;
				}
				case 28:
					goto IL_295;
				case 29:
				{
					sprថ sprថ;
					if (sprថ.HasLineFormat)
					{
						num = 7;
						continue;
					}
					goto IL_5E6;
				}
				case 30:
				{
					sprថ sprថ;
					this.ᜁ(A_0, sprថ, A_2, A_3);
					num = 9;
					continue;
				}
				case 31:
				{
					sprថ sprថ;
					if (spr\u2175.ᜀ(sprថ.Fill.BackColor))
					{
						num = 24;
						continue;
					}
					string value4 = base.ᜁ(sprថ.Fill.BackColor);
					A_0.WriteAttributeString(RecordTableEnumerator.b("⹇⍉⁋≍㍏㵑㡓㥕⩗", a_), value4);
					num = 8;
					continue;
				}
				case 32:
					goto IL_31B;
				case 33:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_437;
					default:
					{
						if (false)
						{
						}
						sprថ sprថ;
						string value5 = sprថ.Line.Weight.ToString() + RecordTableEnumerator.b("㡇㹉", a_);
						A_0.WriteAttributeString(RecordTableEnumerator.b("㭇㹉㹋⅍㭏㝑⍓㍕ㅗ㵙㑛⩝", a_), value5);
						num = 16;
						continue;
					}
					}
					break;
				case 34:
					goto IL_361;
				}
				if (A_0 == null)
				{
					num = 6;
					continue;
				}
				num = 23;
				continue;
				IL_CF:
				num = 27;
				continue;
				IL_190:
				this.ᜄ(A_0, A_1);
				A_0.WriteAttributeString(RecordTableEnumerator.b("ⅇ⑉㽋⭍⑏㽑㭓㉕㵗", a_), RecordTableEnumerator.b("㵇㡉≋瑍⍏ㅑ㱓㍕㕗㭙⽛獝ൟୡݣᑥݧᥩͫ࡭ѯ影ᝳ᥵ᕷ䁹፻᡽늇憐", a_), RecordTableEnumerator.b("⥇㽉㡋⅍", a_));
				num = 21;
				continue;
				IL_270:
				num = 29;
				continue;
				IL_295:
				num = 17;
				continue;
				IL_361:
				A_0.WriteAttributeString(RecordTableEnumerator.b("㱇㍉㱋⭍", a_), value3);
				base.ᜅ(A_0, A_1);
				num = 19;
				continue;
				IL_437:
				num = 0;
				continue;
				IL_5BE:
				num = 20;
			}
			IL_CA:
			throw new ArgumentNullException(RecordTableEnumerator.b("㽇㡉╋㩍㕏⁑", a_));
			IL_31B:
			goto IL_5E6;
			IL_581:
			throw new ArgumentNullException(RecordTableEnumerator.b("㭇≉ⵋ㹍㕏", a_));
			IL_5E6:
			A_0.WriteStartElement(RecordTableEnumerator.b("㱇⽉㑋㩍㉏㵑ⱓ", a_), RecordTableEnumerator.b("㵇㡉≋瑍⍏ㅑ㱓㍕㕗㭙⽛獝ൟୡݣᑥݧᥩͫ࡭ѯ影ᝳ᥵ᕷ䁹੻፽", a_));
			base.ᜆ(A_0, A_1);
			A_0.WriteStartElement(RecordTableEnumerator.b("ⱇ⍉㩋", a_));
			A_0.WriteAttributeString(RecordTableEnumerator.b("㭇㹉㕋≍㕏", a_), RecordTableEnumerator.b("㱇⽉㑋㩍絏㍑㡓㽕㽗㑙晛㉝՟ѡၣ", a_));
			this.ᜂ(A_0, A_1);
			A_0.WriteEndElement();
			A_0.WriteEndElement();
			base.ᜀ(A_0, A_1, this.ᜁ());
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06004AB6 RID: 19126 RVA: 0x002D51E4 File Offset: 0x002D41E4
	protected override void ᜀ(XmlWriter A_0, XlsShape A_1)
	{
		int a_ = 7;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_74;
			case 1:
			{
				sprថ sprថ;
				if (sprថ.ᜁ() != null)
				{
					num = 6;
					continue;
				}
				return;
			}
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
					break;
				}
				break;
			case 3:
			{
				sprថ sprថ;
				if (!sprថ.ᜈ())
				{
					num = 4;
					continue;
				}
				goto IL_101;
			}
			case 4:
				A_0.WriteElementString(RecordTableEnumerator.b("猼倾ᕀ⭂㝄≆ⱈཊ", a_), RecordTableEnumerator.b("䠼䴾⽀祂㙄⑆ⅈ⹊⁌⹎≐繒㡔㹖㩘⥚㉜ⱞ๠բᅤ䩦੨Ѫl啮Ṱᕲ፴Ṷ᩸Ṻ䝼᩾呂", a_), string.Empty);
				num = 10;
				continue;
			case 5:
				return;
			case 6:
			{
				sprថ sprថ;
				string rangeGlobalAddress = sprថ.ᜁ().RangeGlobalAddress;
				A_0.WriteElementString(RecordTableEnumerator.b("笼刾ⵀ≂ॄ⹆❈⁊", a_), RecordTableEnumerator.b("䠼䴾⽀祂㙄⑆ⅈ⹊⁌⹎≐繒㡔㹖㩘⥚㉜ⱞ๠բᅤ䩦੨Ѫl啮Ṱᕲ፴Ṷ᩸Ṻ䝼᩾呂", a_), rangeGlobalAddress);
				num = 5;
				continue;
			}
			case 7:
			{
				int num2;
				if (num2 != 0)
				{
					num = 9;
					continue;
				}
				goto IL_138;
			}
			case 8:
			{
				if (A_1 == null)
				{
					num = 11;
					continue;
				}
				if (true)
				{
				}
				sprថ sprថ = A_1 as sprថ;
				A_0.WriteElementString(RecordTableEnumerator.b("簼䨾㕀ⱂ̈́⹆╈❊", a_), RecordTableEnumerator.b("䠼䴾⽀祂㙄⑆ⅈ⹊⁌⹎≐繒㡔㹖㩘⥚㉜ⱞ๠բᅤ䩦੨Ѫl啮Ṱᕲ፴Ṷ᩸Ṻ䝼᩾呂", a_), RecordTableEnumerator.b("笼帾ⵀあ⁄", a_));
				A_0.WriteElementString(RecordTableEnumerator.b("簼䨾㕀ⱂॄ⹆❈⹊", a_), RecordTableEnumerator.b("䠼䴾⽀祂㙄⑆ⅈ⹊⁌⹎≐繒㡔㹖㩘⥚㉜ⱞ๠բᅤ䩦੨Ѫl啮Ṱᕲ፴Ṷ᩸Ṻ䝼᩾呂", a_), RecordTableEnumerator.b("笼帾ⵀあ⁄", a_));
				int num2 = (int)sprថ.ᜂ();
				num = 7;
				continue;
			}
			case 9:
			{
				int num2;
				A_0.WriteElementString(RecordTableEnumerator.b("縼圾⑀⁂⹄≆ⵈ", a_), RecordTableEnumerator.b("䠼䴾⽀祂㙄⑆ⅈ⹊⁌⹎≐繒㡔㹖㩘⥚㉜ⱞ๠բᅤ䩦੨Ѫl啮Ṱᕲ፴Ṷ᩸Ṻ䝼᩾呂", a_), num2.ToString());
				num = 12;
				continue;
			}
			case 10:
				goto IL_101;
			case 11:
				goto IL_FC;
			case 12:
				goto IL_138;
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			num = 8;
			continue;
			IL_101:
			num = 1;
			continue;
			IL_138:
			num = 3;
		}
		IL_74:
		goto IL_124;
		IL_FC:
		throw new ArgumentNullException(RecordTableEnumerator.b("丼圾⁀㍂⁄", a_));
		IL_124:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨼䴾⡀㝂⁄㕆", a_));
	}

	// Token: 0x06004AB7 RID: 19127 RVA: 0x002D5440 File Offset: 0x002D4440
	protected override void ᜂ(XmlWriter A_0, XlsShape A_1)
	{
		int a_ = 8;
		switch (0)
		{
		default:
			for (;;)
			{
				sprថ sprថ = (sprថ)A_1;
				IRichTextString richText = sprថ.RichText;
				string text = richText.Text;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (text.Length > 0)
						{
							num = 4;
							continue;
						}
						return;
					case 1:
						goto IL_7D;
					case 2:
						return;
					case 3:
						if (text == null)
						{
							return;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_7D;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					case 4:
					{
						IFont font = sprថ.Workbook.CreateFont();
						font.Size -= 2.0;
						A_0.WriteStartElement(RecordTableEnumerator.b("堽⼿ⱁぃ", a_));
						A_0.WriteAttributeString(RecordTableEnumerator.b("堽ℿ⅁⅃", a_), font.FontName);
						A_0.WriteAttributeString(RecordTableEnumerator.b("䴽⤿㡁⅃", a_), (font.Size * 20.0).ToString());
						A_0.WriteAttributeString(RecordTableEnumerator.b("崽⼿⹁⭃㑅", a_), RecordTableEnumerator.b("弽㔿㙁⭃", a_));
						A_0.WriteString(text);
						A_0.WriteEndElement();
						if (true)
						{
						}
						num = 2;
						continue;
					}
					}
					break;
					IL_7D:
					num = 0;
				}
			}
			return;
		}
	}

	// Token: 0x06004AB8 RID: 19128 RVA: 0x002D55C4 File Offset: 0x002D45C4
	protected override void ᜀ(XmlWriter A_0)
	{
		int a_ = 5;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		A_0.WriteStartElement(RecordTableEnumerator.b("䠺䤼䴾⹀⡂⁄", a_), RecordTableEnumerator.b("为似儾筀あ♄⽆ⱈ♊ⱌ㱎籐㹒㱔㑖⭘㑚⹜ぞݠᝢ䡤Ѧ٨٪坬᥮ᱰὲ", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("儺刼嘾⽀あㅄ㹆╈⹊", a_), RecordTableEnumerator.b("嘺吼䬾⑀ㅂ", a_));
		A_0.WriteEndElement();
		A_0.WriteStartElement(RecordTableEnumerator.b("䬺尼䬾⥀", a_), RecordTableEnumerator.b("为似儾筀あ♄⽆ⱈ♊ⱌ㱎籐㹒㱔㑖⭘㑚⹜ぞݠᝢ䡤Ѧ٨٪坬᥮ᱰὲ", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("䠺唼帾╀ⱂ㉄⡆≈", a_), RecordTableEnumerator.b("崺", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("帺䔼䬾㍀㙂㙄⹆♈╊≌⑎", a_), RecordTableEnumerator.b("为似儾筀あ♄⽆ⱈ♊ⱌ㱎籐㹒㱔㑖⭘㑚⹜ぞݠᝢ䡤Ѧ٨٪坬nᝰᕲᱴᑶᱸ䅺ቼ᥾", a_), RecordTableEnumerator.b("崺", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("䠺䤼䴾⹀⡂⁄⡆≈", a_), RecordTableEnumerator.b("崺", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("崺吼匾ⵀⱂ⹄", a_), RecordTableEnumerator.b("崺", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("堺刼儾⽀♂♄㍆㵈㉊㵌⩎", a_), RecordTableEnumerator.b("为似儾筀あ♄⽆ⱈ♊ⱌ㱎籐㹒㱔㑖⭘㑚⹜ぞݠᝢ䡤Ѧ٨٪坬nᝰᕲᱴᑶᱸ䅺ቼ᥾", a_), RecordTableEnumerator.b("䤺堼尾㕀", a_));
		A_0.WriteEndElement();
		A_0.WriteStartElement(RecordTableEnumerator.b("场刼尾⩀", a_), RecordTableEnumerator.b("为似儾筀あ♄⽆ⱈ♊ⱌ㱎籐㹒㱔㑖⭘㑚⹜ぞݠᝢ䡤Ѧ٨٪坬nᝰᕲᱴᑶᱸ䅺ቼ᥾", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("帺䔼䬾", a_), RecordTableEnumerator.b("为似儾筀あ♄⽆ⱈ♊ⱌ㱎籐㹒㱔㑖⭘㑚⹜ぞݠᝢ䡤Ѧ٨٪坬᥮ᱰὲ", a_), RecordTableEnumerator.b("帺夼嘾㕀", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("䠺唼帾ㅀ♂ㅄ㹆㥈⹊", a_), RecordTableEnumerator.b("伺", a_));
		A_0.WriteEndElement();
	}
}
