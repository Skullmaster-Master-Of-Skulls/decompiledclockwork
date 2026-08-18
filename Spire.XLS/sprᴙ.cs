using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Xml;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlSerialization;

// Token: 0x02000300 RID: 768
internal class sprᴙ : spr\u1A78
{
	// Token: 0x06002F70 RID: 12144 RVA: 0x001A9A78 File Offset: 0x001A8A78
	public override void ᜀ(XmlWriter A_0, XlsShape A_1, sprᡟ A_2, RelationsCollection A_3)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 11;
			for (;;)
			{
				string text;
				XlsWorksheet xlsWorksheet;
				XlsTextBoxShape xlsTextBoxShape;
				sprវ a_2;
				string text2;
				switch (num)
				{
				case 0:
					A_0.WriteElementString(RecordTableEnumerator.b("╅⑇⍉⥋⁍⑏ᙑ㕓≕㥗", a_), text, string.Empty);
					num = 5;
					continue;
				case 1:
					goto IL_1F7;
				case 2:
					goto IL_2C6;
				case 3:
					goto IL_F1;
				case 4:
					if (xlsWorksheet != null)
					{
						num = 0;
						continue;
					}
					goto IL_335;
				case 5:
					goto IL_252;
				case 6:
					num = 16;
					continue;
				case 7:
					goto IL_7C;
				case 8:
					if (xlsTextBoxShape == null)
					{
						num = 2;
						continue;
					}
					a_2 = A_2.ᜋ();
					num = 12;
					continue;
				case 9:
					text2 = RecordTableEnumerator.b("⹅㱇㹉㱋瑍罏絑❓㕕し㽙ㅛ㽝፟䱡ୣᙥ൧ѩᑫͭᱯᑱ᭳ѵᕷ᭹ࡻൽ깿ꞇﺋﮑ望뎛겝邟銡銣覥\udba7\udaa9\udeab쮭톯횱잳\udeb5\uddb7\udfb9좻諾늿ꏁ돃꿅ꛇ귉", a_);
					goto IL_1FC;
				case 10:
					A_0.WriteStartElement(RecordTableEnumerator.b("㉅㽇╉ཋ⭍㱏㹑ᕓ㡕㭗㉙㍛ⱝ", a_), text);
					A_0.WriteAttributeString(RecordTableEnumerator.b("⍅ⱇ⍉㡋ཌྷ⍏", a_), spr\u1A78.ᜀ(A_1));
					num = 3;
					continue;
				case 12:
					if (A_1.ParentShapes.Worksheet == null)
					{
						num = 6;
						continue;
					}
					num = 9;
					continue;
				case 13:
					if (A_2 == null)
					{
						num = 1;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_21B;
					default:
						if (false)
						{
						}
						xlsTextBoxShape = (A_1 as XlsTextBoxShape);
						num = 8;
						continue;
					}
					break;
				case 14:
					goto IL_F1;
				case 15:
					if (xlsWorksheet != null)
					{
						goto IL_21B;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("㑅ⵇ♉Ὃ❍⩏㝑ᕓ㡕㭗㉙㍛ⱝ", a_), text);
					num = 14;
					continue;
				case 16:
					text2 = RecordTableEnumerator.b("⹅㱇㹉㱋瑍罏絑❓㕕し㽙ㅛ㽝፟䱡ୣᙥ൧ѩᑫͭᱯᑱ᭳ѵᕷ᭹ࡻൽ깿ꞇﺋﮑ望뎛겝邟銡銣覥쮧슩춫\udcad쒯욳ힵ쾷펹튻\ud9bd", a_);
					goto IL_1FC;
				}
				if (A_0 == null)
				{
					num = 7;
					continue;
				}
				num = 13;
				continue;
				IL_F1:
				base.ᜁ(A_0, RecordTableEnumerator.b("⁅㩇╉⅋", a_), A_1.LeftColumn, A_1.LeftColumnOffset, A_1.TopRow, A_1.TopRowOffset, xlsWorksheet, text);
				base.ᜁ(A_0, RecordTableEnumerator.b("㉅❇", a_), A_1.RightColumn, A_1.RightColumnOffset, A_1.BottomRow, A_1.BottomRowOffset, xlsWorksheet, text);
				A_0.WriteStartElement(RecordTableEnumerator.b("㕅㡇", a_), text);
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("⁅ч╉⽋╍⍏ّㅓ⹕ⱗ", a_), xlsTextBoxShape.IsTextLocked, true);
				this.ᜀ(A_0, xlsTextBoxShape, A_2, text);
				this.ᜀ(A_0, xlsTextBoxShape, a_2, A_2.ᜇ(), text);
				sprᴙ.ᜀ(A_0, text, xlsTextBoxShape);
				A_0.WriteEndElement();
				if (true)
				{
				}
				num = 4;
				continue;
				IL_1FC:
				text = text2;
				xlsWorksheet = (A_1.Worksheet as XlsWorksheet);
				num = 15;
				continue;
				IL_21B:
				num = 10;
			}
			IL_7C:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅅ㩇⍉㡋⭍≏", a_));
			IL_1F7:
			throw new ArgumentNullException(RecordTableEnumerator.b("⹅❇♉⡋⭍≏", a_));
			IL_252:
			goto IL_335;
			IL_2C6:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㉅ⵇ㉉㡋్㽏⩑", a_));
			IL_335:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06002F71 RID: 12145 RVA: 0x001A9DC0 File Offset: 0x001A8DC0
	private new void ᜀ(XmlWriter A_0, XlsTextBoxShape A_1, sprវ A_2, RelationsCollection A_3, string A_4)
	{
		int a_ = 7;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				goto IL_A1;
			case 2:
				goto IL_8B;
			case 3:
				goto IL_46;
			}
			if (A_0 == null)
			{
				if (true)
				{
				}
				num = 3;
			}
			else
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
					num = 0;
					break;
				}
			}
		}
		IL_46:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨼䴾⡀㝂⁄㕆", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("䤼娾㥀㝂݄⡆ㅈ", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("丼伾ᅀㅂ", a_), A_4);
		Rectangle coordinates = A_1.Coordinates2007;
		spr\u1A78.ᜀ(A_0, RecordTableEnumerator.b("唼䬾㕀㍂罄框晈㡊⹌❎㑐㹒㑔⑖睘㑚ⵜ㩞འ᭢ࡤ୦ཨѪὬɮၰݲٴ奶ᙸॺ᩼偾﶐벒ꞔꞖꦘ궚늜삠쪢쮤", a_), RecordTableEnumerator.b("唼䬾㕀㍂罄框晈㡊⹌❎㑐㹒㑔⑖睘㑚ⵜ㩞འ᭢ࡤ୦ཨѪὬɮၰݲٴ奶ᙸॺ᩼偾﶐벒ꞔꞖꦘ궚늜삠쪢쮤", a_), coordinates.X, coordinates.Y, coordinates.Width, coordinates.Height, A_1);
		base.ᜀ(A_0);
		base.ᜀ(A_0, A_1, A_2, A_3);
		A_0.WriteEndElement();
	}

	// Token: 0x06002F72 RID: 12146 RVA: 0x001A9EE4 File Offset: 0x001A8EE4
	private new void ᜀ(XmlWriter A_0, XlsTextBoxShape A_1, sprᡟ A_2, string A_3)
	{
		int a_ = 1;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_3E;
			case 1:
				if (A_1 == null)
				{
					if (true)
					{
					}
					num = 3;
					continue;
				}
				goto IL_A1;
			case 3:
				goto IL_8B;
			}
			if (A_0 == null)
			{
				num = 0;
			}
			else
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
					break;
				}
			}
		}
		IL_3E:
		throw new ArgumentNullException(RecordTableEnumerator.b("䀶䬸刺䤼娾㍀", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("䌶尸䌺䤼紾⹀㭂", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("夶伸栺䴼漾㍀", a_), A_3);
		base.ᜀ(A_0, A_1, A_2, A_3);
		A_0.WriteStartElement(RecordTableEnumerator.b("吶眸䴺渼伾ᅀㅂ", a_), A_3);
		A_0.WriteAttributeString(RecordTableEnumerator.b("䌶䄸示刼䜾", a_), RecordTableEnumerator.b("ض", a_));
		A_0.WriteEndElement();
		A_0.WriteEndElement();
	}

	// Token: 0x06002F73 RID: 12147 RVA: 0x001A9FF8 File Offset: 0x001A8FF8
	public new static void ᜀ(XmlWriter A_0, string A_1, TextBoxShapeBase A_2)
	{
		int a_ = 0;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_83;
			case 2:
				if (A_2 == null)
				{
					num = 1;
					continue;
				}
				goto IL_A1;
			case 3:
				goto IL_3E;
			}
			if (A_0 == null)
			{
				num = 3;
			}
			else
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
					num = 2;
					break;
				}
			}
		}
		IL_3E:
		throw new ArgumentNullException(RecordTableEnumerator.b("䄵䨷匹䠻嬽㈿", a_));
		IL_83:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䈵崷䈹䠻簽⼿㩁", a_));
		IL_A1:
		RichTextString a_2 = (RichTextString)A_2.RichText;
		A_0.WriteStartElement(RecordTableEnumerator.b("䈵䀷砹医娽㤿", a_), A_1);
		sprᴙ.ᜁ(A_0, a_2, A_2);
		sprᴙ.ᜀ(A_0, a_2);
		sprᴙ.ᜀ(A_0, a_2, A_2);
		A_0.WriteEndElement();
	}

	// Token: 0x06002F74 RID: 12148 RVA: 0x001AA0E4 File Offset: 0x001A90E4
	private new static void ᜁ(XmlWriter A_0, RichTextString A_1, TextBoxShapeBase A_2)
	{
		int a_ = 8;
		int num = 4;
		for (;;)
		{
			Dictionary<string, string> unknownBodyProperties;
			switch (num)
			{
			case 0:
				goto IL_9C;
			case 1:
			{
				Dictionary<string, string>.Enumerator enumerator = unknownBodyProperties.GetEnumerator();
				num = 7;
				continue;
			}
			case 2:
				if (unknownBodyProperties.Count > 0)
				{
					num = 1;
					continue;
				}
				goto IL_1B6;
			case 3:
				num = 2;
				continue;
			case 5:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				goto IL_15B;
			case 6:
				if (unknownBodyProperties != null)
				{
					num = 3;
					continue;
				}
				goto IL_1B6;
			case 7:
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
							goto IL_14B;
						case 4:
						{
							Dictionary<string, string>.Enumerator enumerator;
							if (!enumerator.MoveNext())
							{
								num = 1;
								continue;
							}
							if (true)
							{
							}
							KeyValuePair<string, string> keyValuePair = enumerator.Current;
							A_0.WriteAttributeString(keyValuePair.Key, keyValuePair.Value);
							num = 3;
							continue;
						}
						}
						IL_109:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							num = 4;
							continue;
						}
						goto IL_109;
					}
					IL_14B:
					goto IL_1B6;
				}
				finally
				{
					Dictionary<string, string>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				goto IL_15B;
			case 8:
				goto IL_48;
			}
			if (A_0 == null)
			{
				num = 8;
				continue;
			}
			num = 5;
			continue;
			IL_15B:
			A_0.WriteStartElement(RecordTableEnumerator.b("尽⼿♁㵃ᙅ㩇", a_), RecordTableEnumerator.b("嘽㐿㙁㑃籅杇敉㽋ⵍ㡏㝑㥓㝕⭗瑙㍛⹝՟ౡᱣ୥ѧ౩ͫᱭᵯ፱sյ噷ᕹ๻᥽꽿ﾇﶏﺑ뮓꒕ꢗꪙꪛ놝춟쎡춣좥", a_));
			unknownBodyProperties = A_2.UnknownBodyProperties;
			num = 6;
		}
		IL_48:
		throw new ArgumentNullException(RecordTableEnumerator.b("䤽㈿⭁ぃ⍅㩇", a_));
		IL_9C:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨽┿㩁ぃ݅㩇⽉ⵋ", a_));
		IL_1B6:
		sprᴙ.ᜀ(A_0, A_2);
		sprᴙ.ᜁ(A_0, A_2);
		A_0.WriteEndElement();
	}

	// Token: 0x06002F75 RID: 12149 RVA: 0x001AA2CC File Offset: 0x001A92CC
	private new static void ᜁ(XmlWriter A_0, TextBoxShapeBase A_1)
	{
		int a_ = 19;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				return;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					A_0.WriteAttributeString(RecordTableEnumerator.b("⡈╊⹌❎㹐⅒", a_), ((XLSXCommentVAlign)A_1.VAlignment).ToString());
					num = 1;
					continue;
				}
				break;
			}
			if (true)
			{
			}
			if (A_1.VAlignment == CommentVAlignType.Top)
			{
				break;
			}
			num = 2;
		}
	}

	// Token: 0x06002F76 RID: 12150 RVA: 0x001AA370 File Offset: 0x001A9370
	private new static void ᜀ(XmlWriter A_0, TextBoxShapeBase A_1)
	{
		int a_ = 12;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				return;
			case 2:
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
					A_0.WriteAttributeString(RecordTableEnumerator.b("㑁⅃㑅㱇", a_), ((XLSXTextRotation)A_1.TextRotation).ToString());
					num = 1;
					continue;
				}
				break;
			}
			if (A_1.TextRotation == TextRotationType.LeftToRight)
			{
				break;
			}
			num = 2;
		}
	}

	// Token: 0x06002F77 RID: 12151 RVA: 0x001AA410 File Offset: 0x001A9410
	private new static void ᜀ(XmlWriter A_0, RichTextString A_1)
	{
		int a_ = 6;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				goto IL_A1;
			case 2:
				goto IL_8B;
			case 3:
				goto IL_46;
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 3;
			}
			else
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
					num = 0;
					break;
				}
			}
		}
		IL_46:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬻䰽⤿㙁⅃㑅", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠻嬽㠿㙁Ճ㑅ⵇ⭉", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("倻䴽㐿ᅁぃ㽅⑇⽉", a_), RecordTableEnumerator.b("吻䨽㐿㉁繃楅杇㥉⽋♍㕏㽑㕓╕癗㕙ⱛ㭝๟ᩡॣ੥๧թṫͭᅯٱݳ塵᝷ࡹ᭻兽ﲏ붑ꚓꚕꢗ겙뎛솟쮡쪣", a_));
		A_0.WriteEndElement();
	}

	// Token: 0x06002F78 RID: 12152 RVA: 0x001AA4E8 File Offset: 0x001A94E8
	private new static void ᜀ(XmlWriter A_0, RichTextString A_1, TextBoxShapeBase A_2)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			int num = 7;
			for (;;)
			{
				IFont a_2;
				XlsWorkbook xlsWorkbook;
				string text;
				spr\u223A spr_u223A;
				int num2;
				int num3;
				int index;
				int num5;
				XlsFontsCollection innerFonts;
				int num6;
				switch (num)
				{
				case 0:
					sprᴙ.ᜀ(A_0, a_2, RecordTableEnumerator.b("䴾ᅀㅂ", a_), xlsWorkbook, text, A_2);
					num = 2;
					continue;
				case 1:
					num = 4;
					continue;
				case 2:
					goto IL_386;
				case 3:
					num = 23;
					continue;
				case 4:
					if (spr_u223A.ᜄ(0) != 0)
					{
						num = 13;
						continue;
					}
					goto IL_2C3;
				case 5:
					if (true)
					{
					}
					if (num2 == num3 - 1)
					{
						num = 17;
						continue;
					}
					num = 22;
					continue;
				case 6:
					if (num2 >= num3)
					{
						num = 15;
						continue;
					}
					index = spr_u223A.ᜃ(num2);
					num = 5;
					continue;
				case 8:
					goto IL_260;
				case 9:
					if (num3 == 0)
					{
						num = 16;
						continue;
					}
					goto IL_3BC;
				case 10:
					goto IL_107;
				case 11:
					goto IL_99;
				case 12:
					goto IL_2C3;
				case 13:
				{
					spr_u223A = spr_u223A.\u170D();
					int num4 = A_1.DefaultFontIndex;
					num = 24;
					continue;
				}
				case 14:
					num5 = text.Length;
					goto IL_197;
				case 15:
					a_2 = innerFonts[0];
					num = 9;
					continue;
				case 16:
					num = 20;
					continue;
				case 17:
					num = 14;
					continue;
				case 18:
					goto IL_260;
				case 19:
					if (A_1 == null)
					{
						num = 10;
						continue;
					}
					spr_u223A = A_1.TextObject;
					num6 = 0;
					xlsWorkbook = A_1.Workbook;
					innerFonts = xlsWorkbook.InnerFonts;
					num = 21;
					continue;
				case 20:
					if (text != null)
					{
						num = 3;
						continue;
					}
					goto IL_3BC;
				case 21:
					if (spr_u223A.ᜆ() > 0)
					{
						num = 1;
						continue;
					}
					goto IL_2C3;
				case 22:
					num5 = spr_u223A.ᜄ(num2 + 1);
					goto IL_197;
				case 23:
					if (text.Length <= 0)
					{
						goto IL_3BC;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_386;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 24:
				{
					int num4;
					spr_u223A.ᜇ()[0] = ((num4 >= 0) ? num4 : 0);
					num = 12;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 11;
					continue;
				}
				num = 19;
				continue;
				IL_197:
				int num7 = num5 - 1;
				a_2 = innerFonts[index];
				string text2 = text.Substring(num6, num7 - num6 + 1);
				string[] array = text2.Split(new char[]
				{
					'\n'
				});
				sprᴙ.ᜀ(A_0, a_2, RecordTableEnumerator.b("䴾ᅀㅂ", a_), xlsWorkbook, array[0], A_2);
				sprᴙ.ᜀ(A_0, a_2, RecordTableEnumerator.b("娾⽀❂ᕄ♆㭈⩊Ὄ὎⍐", a_), xlsWorkbook);
				num6 = num7 + 1;
				num2++;
				num = 18;
				continue;
				IL_260:
				num = 6;
				continue;
				IL_2C3:
				text = spr_u223A.ᜏ();
				num3 = spr_u223A.ᜆ();
				A_0.WriteStartElement(RecordTableEnumerator.b("伾", a_), RecordTableEnumerator.b("圾㕀㝂㕄絆晈摊㹌ⱎ㥐㙒㡔㙖⩘畚㉜⽞Ѡൢᵤ੦ը൪ɬᵮᱰቲŴѶ坸ᑺོ᡾꺀ﺈﲐﾒ몔ꖖꦘꮚꮜ낞철슢첤즦", a_));
				sprᴙ.ᜀ(A_0, xlsWorkbook, A_2);
				num2 = 0;
				num = 8;
			}
			IL_99:
			throw new ArgumentNullException(RecordTableEnumerator.b("䠾㍀⩂ㅄ≆㭈", a_));
			IL_107:
			throw new ArgumentNullException(RecordTableEnumerator.b("䬾⑀㭂ㅄن㭈⹊ⱌ", a_));
			IL_386:
			IL_3BC:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06002F79 RID: 12153 RVA: 0x001AA8B8 File Offset: 0x001A98B8
	private new static void ᜀ(XmlWriter A_0, IWorkbook A_1, TextBoxShapeBase A_2)
	{
		int a_ = 0;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_2 == null)
				{
					num = 5;
					continue;
				}
				goto IL_D8;
			case 1:
				goto IL_6A;
			case 2:
				if (A_1 == null)
				{
					num = 4;
					continue;
				}
				num = 0;
				continue;
			case 3:
				if (true)
				{
				}
				break;
			case 4:
				goto IL_D6;
			case 5:
				goto IL_7F;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_81;
			default:
				if (false)
				{
				}
				if (A_0 == null)
				{
					num = 1;
				}
				else
				{
					num = 2;
				}
				break;
			}
		}
		IL_6A:
		throw new ArgumentNullException(RecordTableEnumerator.b("䄵䨷匹䠻嬽㈿", a_));
		IL_7F:
		throw new ArgumentNullException(RecordTableEnumerator.b("䈵崷䈹䠻簽⼿㩁", a_));
		IL_81:
		throw new ArgumentNullException(RecordTableEnumerator.b("吵圷唹圻", a_));
		IL_D6:
		goto IL_81;
		IL_D8:
		A_0.WriteStartElement(RecordTableEnumerator.b("䘵样䠹", a_), RecordTableEnumerator.b("帵䰷丹䰻н漿流㝃╅⁇⽉⅋⽍⍏籑㭓♕㵗㑙⑛㍝౟ѡୣᑥէ୩ᡫᵭ幯ᵱٳᅵ坷ṹ๻ώꎋ벍ꂏꊑꊓ릕ﮙ", a_));
		XLSXCommentHAlign halignment = (XLSXCommentHAlign)A_2.HAlignment;
		XLSXCommentHAlign xlsxcommentHAlign = XLSXCommentHAlign.l;
		spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("圵吷崹刻", a_), halignment.ToString(), xlsxcommentHAlign.ToString());
		A_0.WriteEndElement();
	}

	// Token: 0x06002F7A RID: 12154 RVA: 0x001AA9F8 File Offset: 0x001A99F8
	private new static void ᜀ(XmlWriter A_0, IFont A_1, string A_2, IWorkbook A_3, string A_4, TextBoxShapeBase A_5)
	{
		int a_ = 11;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				return;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("㍀", a_), RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂ﲊﺒ璉뢖ꮘꮚ궜ꦞ躠캢쒤캦잨", a_));
					sprᴙ.ᜀ(A_0, A_1, A_2, A_3);
					A_0.WriteStartElement(RecordTableEnumerator.b("㕀", a_), RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂ﲊﺒ璉뢖ꮘꮚ궜ꦞ躠캢쒤캦잨", a_));
					A_0.WriteString(A_4);
					A_0.WriteEndElement();
					A_0.WriteEndElement();
					num = 1;
					continue;
				}
				break;
			}
			if (true)
			{
			}
			if (A_4.Length <= 0)
			{
				break;
			}
			num = 2;
		}
	}

	// Token: 0x06002F7B RID: 12155 RVA: 0x001AAAE0 File Offset: 0x001A9AE0
	public new static void ᜀ(XmlWriter A_0, IFont A_1, string A_2, IWorkbook A_3)
	{
		int a_ = 11;
		switch (0)
		{
		default:
		{
			int num = 12;
			for (;;)
			{
				string text;
				int num2;
				string text2;
				string text3;
				switch (num)
				{
				case 0:
					text = RecordTableEnumerator.b("煀", a_);
					goto IL_1C3;
				case 1:
					num2 = 30000;
					num = 30;
					continue;
				case 2:
					goto IL_39F;
				case 3:
					goto IL_CA;
				case 4:
					goto IL_3C4;
				case 5:
					if (A_1 == null)
					{
						num = 2;
						continue;
					}
					num = 33;
					continue;
				case 6:
					A_0.WriteAttributeString(RecordTableEnumerator.b("⍀≂㙄≆╈≊⍌⩎", a_), num2.ToString());
					num = 22;
					continue;
				case 7:
					num2 = -25000;
					num = 18;
					continue;
				case 8:
					if (num2 != 0)
					{
						num = 6;
						continue;
					}
					goto IL_4CB;
				case 9:
					goto IL_141;
				case 10:
					num = 21;
					continue;
				case 11:
					goto IL_332;
				case 13:
					num = 20;
					continue;
				case 14:
					if (!A_1.IsBold)
					{
						num = 19;
						continue;
					}
					num = 31;
					continue;
				case 15:
					num = 23;
					continue;
				case 16:
					text2 = RecordTableEnumerator.b("灀", a_);
					goto IL_478;
				case 17:
					num = 32;
					continue;
				case 18:
					goto IL_326;
				case 19:
					num = 0;
					continue;
				case 20:
					text2 = RecordTableEnumerator.b("煀", a_);
					goto IL_478;
				case 21:
					text3 = RecordTableEnumerator.b("╀⅂⥄", a_);
					goto IL_3EE;
				case 22:
					goto IL_476;
				case 23:
					if (A_1.Underline != FontUnderlineType.Single)
					{
						num = 10;
						continue;
					}
					num = 34;
					continue;
				case 24:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_332;
					default:
						if (false)
						{
						}
						if (!A_1.IsItalic)
						{
							num = 13;
							continue;
						}
						num = 16;
						continue;
					}
					break;
				case 25:
					if (A_1.IsStrikethrough)
					{
						num = 28;
						continue;
					}
					goto IL_16C;
				case 26:
					if (A_1.IsSubscript)
					{
						num = 7;
						continue;
					}
					goto IL_326;
				case 27:
					goto IL_16C;
				case 28:
					A_0.WriteAttributeString(RecordTableEnumerator.b("㉀㝂㝄⹆≈⹊", a_), RecordTableEnumerator.b("㉀ⵂ≄ᑆ㵈㥊⑌⑎㑐", a_));
					num = 27;
					continue;
				case 29:
					if (A_1.Underline != FontUnderlineType.None)
					{
						if (true)
						{
						}
						num = 15;
						continue;
					}
					goto IL_141;
				case 30:
					goto IL_269;
				case 31:
					text = RecordTableEnumerator.b("灀", a_);
					goto IL_1C3;
				case 32:
					if (A_2.Length == 0)
					{
						num = 4;
						continue;
					}
					A_0.WriteStartElement(A_2, RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂ﲊﺒ璉뢖ꮘꮚ궜ꦞ躠캢쒤캦잨", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("ⵀ≂⭄⁆", a_), CultureInfo.CurrentCulture.Name);
					num = 14;
					continue;
				case 33:
					if (A_2 != null)
					{
						num = 17;
						continue;
					}
					goto IL_3DA;
				case 34:
					text3 = RecordTableEnumerator.b("㉀ⵂ≄", a_);
					goto IL_3EE;
				}
				if (A_0 == null)
				{
					num = 3;
					continue;
				}
				num = 5;
				continue;
				IL_141:
				num2 = 0;
				num = 26;
				continue;
				IL_16C:
				int num3 = (int)(A_1.Size * 100.0);
				A_0.WriteAttributeString(RecordTableEnumerator.b("㉀㥂", a_), num3.ToString());
				num = 29;
				continue;
				IL_1C3:
				string value = text;
				num = 24;
				continue;
				IL_269:
				num = 8;
				continue;
				IL_332:
				if (A_1.IsSuperscript)
				{
					num = 1;
					continue;
				}
				goto IL_269;
				IL_326:
				num = 11;
				continue;
				IL_3EE:
				string value2 = text3;
				A_0.WriteAttributeString(RecordTableEnumerator.b("㑀", a_), value2);
				num = 9;
				continue;
				IL_478:
				string value3 = text2;
				A_0.WriteAttributeString(RecordTableEnumerator.b("⍀", a_), value);
				A_0.WriteAttributeString(RecordTableEnumerator.b("⡀", a_), value3);
				num = 25;
			}
			IL_CA:
			throw new ArgumentNullException(RecordTableEnumerator.b("㙀ㅂⱄ㍆ⱈ㥊", a_));
			IL_39F:
			throw new ArgumentNullException(RecordTableEnumerator.b("㕀♂㵄㍆ࡈ㥊⡌⹎", a_));
			IL_3C4:
			IL_3DA:
			throw new ArgumentException(RecordTableEnumerator.b("ⱀ≂ⱄ⥆ᵈ⩊⩌Ŏぐ㹒ご", a_));
			IL_476:
			IL_4CB:
			A_0.WriteStartElement(RecordTableEnumerator.b("㉀ⱂ⥄⹆ⵈൊ⑌⍎㵐", a_), RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂ﲊﺒ璉뢖ꮘꮚ궜ꦞ躠캢쒤캦잨", a_));
			spr\u1CFF.ᜀ(A_0, A_1.KnownColor, A_3);
			A_0.WriteEndElement();
			A_0.WriteStartElement(RecordTableEnumerator.b("ⵀ≂ㅄ⹆❈", a_), RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂ﲊﺒ璉뢖ꮘꮚ궜ꦞ躠캢쒤캦잨", a_));
			A_0.WriteAttributeString(RecordTableEnumerator.b("㕀㩂㕄≆⽈⩊⹌⩎", a_), A_1.FontName);
			A_0.WriteEndElement();
			A_0.WriteEndElement();
			return;
		}
		}
	}
}
