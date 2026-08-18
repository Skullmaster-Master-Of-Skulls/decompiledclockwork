using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;

// Token: 0x0200034C RID: 844
internal class spr\u1F16
{
	// Token: 0x06003358 RID: 13144 RVA: 0x001D8F7C File Offset: 0x001D7F7C
	public static void ᜁ(ITextBox A_0, XmlReader A_1, spr\u2306 A_2)
	{
		int a_ = 19;
		int num = 16;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("㩈㭊ᵌ㵎", a_)))
				{
					num = 15;
					continue;
				}
				spr\u1F16.ᜀ(A_0, A_1, A_2);
				num = 17;
				continue;
			}
			case 1:
				goto IL_16E;
			case 2:
				num = 0;
				continue;
			case 3:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("❈㵊Ṍ㽎Ő⅒", a_)))
				{
					num = 9;
					continue;
				}
				spr\u1F16.ᜀ(A_0 as IShape, A_1, A_2);
				num = 1;
				continue;
			}
			case 4:
				if (A_1.NodeType == XmlNodeType.Element)
				{
					num = 12;
					continue;
				}
				A_1.Read();
				num = 14;
				continue;
			case 5:
				num = 7;
				continue;
			case 6:
				goto IL_83;
			case 7:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("㵈㍊ཌ⁎㕐⩒", a_)))
				{
					num = 2;
					continue;
				}
				spr\u1F16.ᜀ(A_1, A_2, A_0);
				num = 21;
				continue;
			}
			case 8:
				goto IL_16E;
			case 9:
				num = 18;
				continue;
			case 10:
				if (A_1.NodeType == XmlNodeType.None)
				{
					num = 13;
					continue;
				}
				num = 4;
				continue;
			case 11:
				goto IL_16E;
			case 12:
				num = 20;
				continue;
			case 13:
				return;
			case 14:
				goto IL_16E;
			case 15:
				num = 3;
				continue;
			case 17:
				goto IL_16E;
			case 18:
				goto IL_107;
			case 19:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_D4;
				default:
					goto IL_151;
				}
				break;
			case 20:
			{
				string localName;
				if ((localName = A_1.LocalName) != null)
				{
					num = 5;
					continue;
				}
				goto IL_107;
			}
			case 21:
				goto IL_16E;
			case 22:
				if (A_1 == null)
				{
					num = 19;
					continue;
				}
				goto IL_D4;
			}
			if (A_0 == null)
			{
				num = 6;
				continue;
			}
			num = 22;
			continue;
			IL_D4:
			A_1.Read();
			XlsShape xlsShape = A_0 as XlsShape;
			xlsShape.HasLineFormat = false;
			xlsShape.HasFill = false;
			num = 8;
			continue;
			IL_107:
			A_1.Skip();
			if (true)
			{
			}
			num = 11;
			continue;
			IL_16E:
			num = 10;
		}
		IL_83:
		throw new ArgumentNullException(RecordTableEnumerator.b("㵈⹊㕌㭎ፐ㱒ⵔ", a_));
		IL_151:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㭈⹊ⱌ⭎㑐⅒", a_));
	}

	// Token: 0x06003359 RID: 13145 RVA: 0x001D923C File Offset: 0x001D823C
	private static void ᜀ(IShape A_0, XmlReader A_1, spr\u2306 A_2)
	{
		int a_ = 17;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1A5;
				default:
					if (false)
					{
					}
					num = 11;
					continue;
				}
				break;
			case 2:
				goto IL_135;
			case 3:
				if (A_2 == null)
				{
					num = 12;
					continue;
				}
				num = 9;
				continue;
			case 4:
				goto IL_150;
			case 5:
				goto IL_173;
			case 6:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				num = 3;
				continue;
			case 7:
				if (A_1.NodeType == XmlNodeType.EndElement)
				{
					num = 5;
					continue;
				}
				num = 15;
				continue;
			case 8:
				if (true)
				{
				}
				num = 19;
				continue;
			case 9:
				if (!A_1.IsEmptyElement)
				{
					num = 17;
					continue;
				}
				goto IL_238;
			case 10:
				goto IL_150;
			case 11:
			{
				string localName;
				if (localName == RecordTableEnumerator.b("⑆݈㵊ᵌ㵎", a_))
				{
					num = 14;
					continue;
				}
				goto IL_109;
			}
			case 12:
				goto IL_1A5;
			case 13:
				goto IL_150;
			case 14:
				spr\u2306.ᜀ(A_1, A_0 as XlsShape);
				num = 16;
				continue;
			case 15:
				if (A_1.NodeType == XmlNodeType.Element)
				{
					num = 8;
					continue;
				}
				A_1.Read();
				num = 4;
				continue;
			case 16:
				goto IL_150;
			case 17:
				A_1.Read();
				num = 10;
				continue;
			case 18:
				goto IL_77;
			case 19:
			{
				string localName;
				if ((localName = A_1.LocalName) != null)
				{
					num = 1;
					continue;
				}
				goto IL_109;
			}
			}
			if (A_0 == null)
			{
				num = 18;
				continue;
			}
			num = 6;
			continue;
			IL_109:
			A_1.Skip();
			num = 13;
			continue;
			IL_150:
			num = 7;
		}
		IL_77:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑆ⅈ⩊㵌⩎", a_));
		IL_135:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕆ⱈ⩊⥌⩎⍐", a_));
		IL_173:
		goto IL_238;
		IL_1A5:
		throw new ArgumentNullException(RecordTableEnumerator.b("㝆⡈㥊㹌⩎⍐", a_));
		IL_238:
		A_1.Read();
	}

	// Token: 0x0600335A RID: 13146 RVA: 0x001D9488 File Offset: 0x001D8488
	private static void ᜀ(ITextBox A_0, XmlReader A_1, spr\u2306 A_2)
	{
		int a_ = 17;
		switch (0)
		{
		default:
			for (;;)
			{
				A_1.Read();
				XlsTextBoxShape xlsTextBoxShape = (XlsTextBoxShape)A_0;
				int num = 6;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 9;
						continue;
					case 1:
						if (A_1.MoveToAttribute(RecordTableEnumerator.b("㕆♈㽊", a_)))
						{
							num = 5;
							continue;
						}
						goto IL_212;
					case 2:
						num = 4;
						continue;
					case 3:
						if (A_1.NodeType == XmlNodeType.Element)
						{
							num = 8;
							continue;
						}
						goto IL_1A4;
					case 4:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("㑆♈❊⑌⭎ᝐ㩒㥔㭖", a_)))
						{
							num = 0;
							continue;
						}
						spr\u1C26 spr_u1C = xlsTextBoxShape.Fill as spr\u1C26;
						spr\u1AA0.ᜀ(A_1, A_2, spr_u1C.ᜁ());
						num = 10;
						continue;
					}
					case 5:
						xlsTextBoxShape.Rotation = (int)(Convert.ToInt64(A_1.Value) / 60000L);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_212;
						default:
							if (false)
							{
							}
							num = 7;
							continue;
						}
						break;
					case 6:
						goto IL_1A4;
					case 7:
						goto IL_212;
					case 8:
						num = 20;
						continue;
					case 9:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("⭆❈", a_)))
						{
							num = 19;
							continue;
						}
						XlsShapeLineFormat a_2 = (XlsShapeLineFormat)xlsTextBoxShape.Line;
						spr\u1F16.ᜀ(A_1, a_2, false, A_2);
						num = 16;
						continue;
					}
					case 10:
						goto IL_1A4;
					case 11:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("㽆⽈㥊⁌", a_)))
						{
							num = 13;
							continue;
						}
						num = 1;
						continue;
					}
					case 12:
						if (A_1.NodeType == XmlNodeType.EndElement)
						{
							num = 18;
							continue;
						}
						num = 3;
						continue;
					case 13:
						num = 15;
						continue;
					case 14:
						goto IL_1A4;
					case 15:
						goto IL_133;
					case 16:
						if (true)
						{
						}
						goto IL_1A4;
					case 17:
						goto IL_1A4;
					case 18:
						return;
					case 19:
						num = 11;
						continue;
					case 20:
					{
						string localName;
						if ((localName = A_1.LocalName) != null)
						{
							num = 2;
							continue;
						}
						goto IL_133;
					}
					}
					break;
					IL_133:
					A_1.Skip();
					num = 14;
					continue;
					IL_1A4:
					num = 12;
					continue;
					IL_212:
					xlsTextBoxShape.Coordinates2007 = spr\u1F16.ᜀ(A_1);
					num = 17;
				}
			}
			return;
		}
	}

	// Token: 0x0600335B RID: 13147 RVA: 0x001D975C File Offset: 0x001D875C
	private static Rectangle ᜀ(XmlReader A_0)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			Rectangle empty;
			for (;;)
			{
				empty = Rectangle.Empty;
				int num = 18;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 2;
							continue;
						}
						A_0.Skip();
						num = 20;
						continue;
					case 1:
					{
						int width = XmlConvert.ToInt32(A_0.Value);
						num = 3;
						continue;
					}
					case 2:
						num = 22;
						continue;
					case 3:
						goto IL_C2;
					case 4:
						goto IL_1C2;
					case 5:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("ⵇ㉉㡋", a_)))
						{
							num = 17;
							continue;
						}
						num = 9;
						continue;
					}
					case 6:
					{
						int x = XmlConvert.ToInt32(A_0.Value);
						num = 28;
						continue;
					}
					case 7:
						goto IL_1C2;
					case 8:
						if (true)
						{
						}
						goto IL_260;
					case 9:
						if (A_0.MoveToAttribute(RecordTableEnumerator.b("⭇㉉", a_)))
						{
							num = 1;
							continue;
						}
						goto IL_C2;
					case 10:
						if (A_0.NodeType == XmlNodeType.EndElement)
						{
							num = 12;
							continue;
						}
						num = 0;
						continue;
					case 11:
						goto IL_28F;
					case 12:
					{
						int width;
						int x;
						int y;
						int height;
						empty = new Rectangle(x, y, width, height);
						num = 11;
						continue;
					}
					case 13:
					{
						int height = XmlConvert.ToInt32(A_0.Value);
						num = 7;
						continue;
					}
					case 14:
						if (A_0.MoveToAttribute(RecordTableEnumerator.b("ㅇ", a_)))
						{
							num = 19;
							continue;
						}
						goto IL_1C2;
					case 15:
						if (A_0.MoveToAttribute(RecordTableEnumerator.b("ぇ", a_)))
						{
							num = 6;
							continue;
						}
						goto IL_1EC;
					case 16:
					{
						A_0.Read();
						int x = 0;
						int y = 0;
						int width = 0;
						int height = 0;
						num = 25;
						continue;
					}
					case 17:
						num = 8;
						continue;
					case 18:
						if (!A_0.IsEmptyElement)
						{
							goto IL_B1;
						}
						goto IL_39D;
					case 19:
					{
						int y = XmlConvert.ToInt32(A_0.Value);
						num = 4;
						continue;
					}
					case 20:
						goto IL_1C2;
					case 21:
						if (A_0.MoveToAttribute(RecordTableEnumerator.b("⭇㍉", a_)))
						{
							num = 13;
							continue;
						}
						goto IL_1C2;
					case 22:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num = 26;
							continue;
						}
						goto IL_260;
					}
					case 23:
						goto IL_1C2;
					case 24:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("❇ⱉ⩋", a_)))
						{
							num = 27;
							continue;
						}
						num = 15;
						continue;
					}
					case 25:
						goto IL_1C2;
					case 26:
						num = 24;
						continue;
					case 27:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_B1;
						default:
							if (false)
							{
							}
							num = 5;
							continue;
						}
						break;
					case 28:
						goto IL_1EC;
					}
					break;
					IL_B1:
					num = 16;
					continue;
					IL_C2:
					num = 21;
					continue;
					IL_1C2:
					num = 10;
					continue;
					IL_1EC:
					num = 14;
					continue;
					IL_260:
					A_0.Skip();
					num = 23;
				}
			}
			IL_28F:
			IL_39D:
			A_0.Read();
			return empty;
		}
		}
	}

	// Token: 0x0600335C RID: 13148 RVA: 0x001D9B10 File Offset: 0x001D8B10
	private static void ᜀ(XmlReader A_0, spr\u2306 A_1, ITextBox A_2)
	{
		int a_ = 13;
		int num = 25;
		RichTextString richTextString;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_1FA;
			case 1:
				A_2.IsTextLocked = XmlConvert.ToBoolean(A_0.Value);
				num = 16;
				continue;
			case 2:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("㍂", a_)))
				{
					num = 8;
					continue;
				}
				spr\u1F16.ᜀ(A_0, A_2, A_1);
				num = 13;
				continue;
			}
			case 3:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("⽂㙄㍆ᩈ㽊㑌⍎㑐", a_)))
				{
					num = 24;
					continue;
				}
				spr\u1F16.ᜀ(A_0, richTextString);
				num = 0;
				continue;
			}
			case 4:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 10;
					continue;
				}
				goto IL_13C;
			}
			case 5:
				num = 4;
				continue;
			case 6:
				num = 3;
				continue;
			case 7:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 18;
					continue;
				}
				num = 12;
				continue;
			case 8:
				num = 14;
				continue;
			case 9:
				goto IL_1FA;
			case 10:
				num = 19;
				continue;
			case 11:
				goto IL_1FA;
			case 12:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 5;
					continue;
				}
				A_0.Skip();
				num = 15;
				continue;
			case 13:
				goto IL_1FA;
			case 14:
				goto IL_13C;
			case 15:
				goto IL_1FA;
			case 16:
				goto IL_273;
			case 17:
				goto IL_1FA;
			case 18:
				goto IL_21A;
			case 19:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("⅂⩄⍆えᭊ㽌", a_)))
				{
					num = 6;
					continue;
				}
				spr\u1F16.ᜀ(A_0, richTextString, A_2);
				num = 9;
				continue;
			}
			case 20:
				if (A_2 == null)
				{
					num = 22;
					continue;
				}
				richTextString = (A_2.RichText as RichTextString);
				num = 21;
				continue;
			case 21:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1FA;
				default:
					if (false)
					{
					}
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("╂ॄ⡆⩈⁊㹌᭎㑐⭒⅔", a_)))
					{
						num = 1;
						continue;
					}
					goto IL_273;
				}
				break;
			case 22:
				goto IL_16B;
			case 23:
				goto IL_8F;
			case 24:
				num = 2;
				continue;
			}
			if (A_0 == null)
			{
				num = 23;
				continue;
			}
			num = 20;
			continue;
			IL_13C:
			A_0.Skip();
			num = 17;
			continue;
			IL_1FA:
			num = 7;
			continue;
			IL_273:
			A_0.Read();
			num = 11;
		}
		IL_8F:
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄♆ⵈ⹊㽌", a_));
		IL_16B:
		throw new ArgumentNullException(RecordTableEnumerator.b("㝂⁄㽆㵈੊㽌⩎ぐ", a_));
		IL_21A:
		if (true)
		{
		}
		richTextString.TextObject.ᜈ();
		A_0.Read();
	}

	// Token: 0x0600335D RID: 13149 RVA: 0x001D9E38 File Offset: 0x001D8E38
	private static void ᜀ(XmlReader A_0, RichTextString A_1, ITextBox A_2)
	{
		int a_ = 2;
		int num = 17;
		for (;;)
		{
			Dictionary<string, string> dictionary;
			switch (num)
			{
			case 0:
				goto IL_165;
			case 1:
				if (dictionary.ContainsKey(RecordTableEnumerator.b("夷吹弻嘽⼿ぁ", a_)))
				{
					num = 7;
					continue;
				}
				goto IL_118;
			case 2:
				goto IL_118;
			case 3:
				spr\u1F16.ᜁ(dictionary[RecordTableEnumerator.b("丷弹主䨽", a_)], A_2);
				dictionary.Remove(RecordTableEnumerator.b("丷弹主䨽", a_));
				num = 16;
				continue;
			case 4:
				num = 1;
				continue;
			case 5:
				if (A_0.HasAttributes)
				{
					num = 13;
					continue;
				}
				goto IL_2DF;
			case 6:
			{
				int num2;
				int attributeCount;
				if (num2 >= attributeCount)
				{
					num = 4;
					continue;
				}
				dictionary[A_0.LocalName] = A_0.Value;
				A_0.MoveToNextAttribute();
				num2++;
				num = 9;
				continue;
			}
			case 7:
				spr\u1F16.ᜀ(dictionary[RecordTableEnumerator.b("夷吹弻嘽⼿ぁ", a_)], A_2);
				dictionary.Remove(RecordTableEnumerator.b("夷吹弻嘽⼿ぁ", a_));
				num = 2;
				continue;
			case 8:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				num = 14;
				continue;
			case 9:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6B;
				default:
					if (false)
					{
					}
					goto IL_26F;
				}
				break;
			case 10:
				goto IL_73;
			case 11:
				goto IL_26F;
			case 12:
				goto IL_1D2;
			case 13:
			{
				dictionary = new Dictionary<string, string>();
				A_0.MoveToFirstAttribute();
				int num2 = 0;
				int attributeCount = A_0.AttributeCount;
				num = 11;
				continue;
			}
			case 14:
				if (A_0.LocalName != RecordTableEnumerator.b("娷唹堻䜽ဿぁ", a_))
				{
					num = 18;
					continue;
				}
				num = 5;
				continue;
			case 15:
				if (dictionary.ContainsKey(RecordTableEnumerator.b("丷弹主䨽", a_)))
				{
					num = 3;
					continue;
				}
				goto IL_1A6;
			case 16:
				goto IL_1A6;
			case 18:
				goto IL_22E;
			}
			goto IL_65;
			IL_6B:
			num = 10;
			continue;
			IL_65:
			if (A_0 == null)
			{
				goto IL_6B;
			}
			num = 8;
			continue;
			IL_118:
			num = 15;
			continue;
			IL_1A6:
			dictionary.Remove(RecordTableEnumerator.b("夷", a_));
			(A_2 as TextBoxShapeBase).UnknownBodyProperties = dictionary;
			num = 12;
			continue;
			IL_26F:
			num = 6;
		}
		IL_73:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹崻娽┿ぁ", a_));
		IL_165:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䰷弹䐻䨽Ŀぁ⅃❅", a_));
		IL_1D2:
		goto IL_2DF;
		IL_22E:
		throw new XmlException(RecordTableEnumerator.b("洷吹夻䘽〿❁❃㉅ⵇ⹉汋㙍㵏㹑瑓≕㥗㵙牛", a_));
		IL_2DF:
		A_0.MoveToElement();
		A_0.Skip();
	}

	// Token: 0x0600335E RID: 13150 RVA: 0x001DA134 File Offset: 0x001D9134
	private static void ᜁ(string A_0, ITextBox A_1)
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
		XLSXTextRotation textRotation = (XLSXTextRotation)Enum.Parse(typeof(XLSXTextRotation), A_0, false);
		A_1.TextRotation = (TextRotationType)textRotation;
	}

	// Token: 0x0600335F RID: 13151 RVA: 0x001DA190 File Offset: 0x001D9190
	private static void ᜀ(string A_0, ITextBox A_1)
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
		XLSXCommentVAlign valignment = (XLSXCommentVAlign)Enum.Parse(typeof(XLSXCommentVAlign), A_0, false);
		A_1.VAlignment = (CommentVAlignType)valignment;
	}

	// Token: 0x06003360 RID: 13152 RVA: 0x001DA1EC File Offset: 0x001D91EC
	private static void ᜀ(XmlReader A_0, RichTextString A_1)
	{
		int a_ = 17;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_F7;
			case 1:
				if (A_0.LocalName != RecordTableEnumerator.b("⭆㩈㽊Ṍ㭎⡐㽒ご", a_))
				{
					num = 2;
					continue;
				}
				goto IL_F9;
			case 2:
				goto IL_77;
			case 3:
				goto IL_47;
			case 5:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				num = 1;
				continue;
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
				num = 5;
			}
		}
		IL_47:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_77:
			throw new XmlException(RecordTableEnumerator.b("ቆ❈⹊㕌㽎㑐げ⅔㉖㵘筚╜㉞ൠ䍢ᅤ٦๨䕪", a_));
		default:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㕆ⱈ⩊⥌⩎⍐", a_));
		}
		IL_F7:
		throw new ArgumentNullException(RecordTableEnumerator.b("㍆ⱈ㍊㥌๎⍐㙒㑔", a_));
		IL_F9:
		A_0.Skip();
	}

	// Token: 0x06003361 RID: 13153 RVA: 0x001DA2F8 File Offset: 0x001D92F8
	private static void ᜀ(XmlReader A_0, ITextBox A_1, spr\u2306 A_2)
	{
		int a_ = 16;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("⍅♇⹉᱋⽍≏㍑ٕٓ⩗", a_)))
				{
					num = 13;
					continue;
				}
				RichTextString richTextString;
				spr\u1F16.ᜂ(A_0, richTextString, A_2);
				num = 15;
				continue;
			}
			case 1:
				goto IL_AD;
			case 2:
				goto IL_3C6;
			case 3:
			{
				if (A_0.LocalName != RecordTableEnumerator.b("㙅", a_))
				{
					num = 10;
					continue;
				}
				RichTextString richTextString = A_1.RichText as RichTextString;
				string text = richTextString.Text;
				num = 29;
				continue;
			}
			case 4:
			{
				string text;
				if (text.Length != 0)
				{
					num = 22;
					continue;
				}
				goto IL_325;
			}
			case 6:
				goto IL_201;
			case 7:
				goto IL_221;
			case 8:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("㙅ᡇ㡉", a_)))
				{
					num = 24;
					continue;
				}
				spr\u1F16.ᜀ(A_0, A_1);
				goto IL_315;
			}
			case 9:
			{
				RichTextString richTextString;
				string text;
				richTextString.ᜁ(RecordTableEnumerator.b("䱅", a_), richTextString.GetFont(text.Length - 1));
				num = 16;
				continue;
			}
			case 10:
				goto IL_2A0;
			case 11:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("㑅", a_)))
				{
					num = 20;
					continue;
				}
				RichTextString richTextString;
				spr\u1F16.ᜁ(A_0, richTextString, A_2);
				num = 18;
				continue;
			}
			case 12:
				goto IL_201;
			case 13:
				num = 21;
				continue;
			case 14:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 25;
					continue;
				}
				goto IL_1EE;
			}
			case 15:
				goto IL_201;
			case 16:
				goto IL_325;
			case 17:
				num = 14;
				continue;
			case 18:
				goto IL_201;
			case 19:
				num = 4;
				continue;
			case 20:
				num = 0;
				continue;
			case 21:
				goto IL_1EE;
			case 22:
				num = 27;
				continue;
			case 23:
				goto IL_201;
			case 24:
				num = 11;
				continue;
			case 25:
				num = 8;
				continue;
			case 26:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 7;
					continue;
				}
				num = 30;
				continue;
			case 27:
			{
				string text;
				if (!text.EndsWith(RecordTableEnumerator.b("䱅", a_)))
				{
					num = 9;
					continue;
				}
				goto IL_325;
			}
			case 28:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				num = 3;
				continue;
			case 29:
			{
				string text;
				if (text != null)
				{
					num = 19;
					continue;
				}
				goto IL_325;
			}
			case 30:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 17;
					continue;
				}
				A_0.Skip();
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_315;
				default:
					if (false)
					{
					}
					num = 6;
					continue;
				}
				break;
			case 31:
				goto IL_201;
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			num = 28;
			continue;
			IL_1EE:
			A_0.Skip();
			num = 31;
			continue;
			IL_201:
			num = 26;
			continue;
			IL_315:
			num = 23;
			continue;
			IL_325:
			if (true)
			{
			}
			A_0.Read();
			num = 12;
		}
		IL_AD:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑅ⵇ⭉⡋⭍≏", a_));
		IL_221:
		A_0.Read();
		return;
		IL_2A0:
		throw new XmlException(RecordTableEnumerator.b("ፅ♇⽉㑋㹍㕏ㅑ⁓㍕㱗穙⑛㍝౟䉡ၣݥཧ䑩", a_));
		IL_3C6:
		throw new ArgumentNullException(RecordTableEnumerator.b("㉅ⵇ㉉㡋ཌྷ≏㝑㕓", a_));
	}

	// Token: 0x06003362 RID: 13154 RVA: 0x001DA6D8 File Offset: 0x001D96D8
	private static void ᜂ(XmlReader A_0, RichTextString A_1, spr\u2306 A_2)
	{
		int a_ = 15;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		IFont a_2 = spr\u1F16.ᜀ(A_0, A_1, A_2);
		A_1.ᜁ(RecordTableEnumerator.b("佄", a_), a_2);
	}

	// Token: 0x06003363 RID: 13155 RVA: 0x001DA73C File Offset: 0x001D973C
	private static void ᜀ(XmlReader A_0, ITextBox A_1)
	{
		int a_ = 0;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("圵吷崹刻", a_)))
				{
					num = 2;
					continue;
				}
				goto IL_117;
			case 1:
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				num = 0;
				continue;
			case 2:
				for (;;)
				{
					XLSXCommentHAlign halignment = (XLSXCommentHAlign)Enum.Parse(typeof(XLSXCommentHAlign), A_0.Value, false);
					A_1.HAlignment = (CommentHAlignType)halignment;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_D9;
					}
				}
				IL_D9:
				if (false)
				{
				}
				num = 4;
				continue;
			case 3:
				goto IL_112;
			case 4:
				goto IL_EA;
			case 6:
				goto IL_43;
			}
			if (A_0 == null)
			{
				num = 6;
			}
			else
			{
				num = 1;
			}
		}
		IL_43:
		throw new ArgumentNullException(RecordTableEnumerator.b("䐵崷嬹堻嬽㈿", a_));
		IL_EA:
		goto IL_117;
		IL_112:
		throw new ArgumentNullException(RecordTableEnumerator.b("䈵崷䈹䠻簽⼿㩁", a_));
		IL_117:
		A_0.MoveToElement();
		A_0.Skip();
	}

	// Token: 0x06003364 RID: 13156 RVA: 0x001DA870 File Offset: 0x001D9870
	private static void ᜁ(XmlReader A_0, RichTextString A_1, spr\u2306 A_2)
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			int num = 3;
			string text2;
			IFont a_2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_1 == null)
					{
						num = 22;
						continue;
					}
					num = 2;
					continue;
				case 1:
					num = 14;
					continue;
				case 2:
					if (A_0.LocalName != RecordTableEnumerator.b("主", a_))
					{
						num = 17;
						continue;
					}
					goto IL_256;
				case 4:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("䠻", a_)))
					{
						num = 11;
						continue;
					}
					string text = A_0.ReadElementContentAsString();
					text2 = text;
					num = 19;
					continue;
				}
				case 5:
					num = 20;
					continue;
				case 6:
					goto IL_AC;
				case 7:
					goto IL_109;
				case 8:
					goto IL_217;
				case 9:
					goto IL_109;
				case 10:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					num = 18;
					continue;
				case 11:
					num = 6;
					continue;
				case 12:
					goto IL_1FC;
				case 13:
					goto IL_109;
				case 14:
					if (text2 != null)
					{
						num = 25;
						continue;
					}
					goto IL_1FC;
				case 15:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_256;
					default:
					{
						if (false)
						{
						}
						string localName;
						if (!(localName == RecordTableEnumerator.b("主渽㈿", a_)))
						{
							num = 21;
							continue;
						}
						a_2 = spr\u1F16.ᜀ(A_0, A_1, A_2);
						num = 26;
						continue;
					}
					}
					break;
				case 16:
					num = 15;
					continue;
				case 17:
					goto IL_104;
				case 18:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 5;
						continue;
					}
					A_0.Skip();
					num = 9;
					continue;
				case 19:
					goto IL_109;
				case 20:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 16;
						continue;
					}
					goto IL_AC;
				}
				case 21:
					num = 4;
					continue;
				case 22:
					goto IL_290;
				case 23:
					goto IL_A7;
				case 24:
					if (text2.Length == 0)
					{
						num = 12;
						continue;
					}
					goto IL_33C;
				case 25:
					num = 24;
					continue;
				case 26:
					goto IL_109;
				}
				if (A_0 == null)
				{
					num = 23;
					continue;
				}
				num = 0;
				continue;
				IL_AC:
				A_0.Skip();
				num = 7;
				continue;
				IL_109:
				num = 10;
				continue;
				IL_1FC:
				text2 = RecordTableEnumerator.b("㘻", a_);
				num = 8;
				continue;
				IL_256:
				A_0.Read();
				text2 = null;
				a_2 = null;
				num = 13;
			}
			IL_A7:
			throw new ArgumentNullException(RecordTableEnumerator.b("主嬽ℿ♁⅃㑅", a_));
			IL_104:
			throw new XmlException(RecordTableEnumerator.b("椻倽┿㩁㑃⍅⭇㹉⥋⩍灏⩑㥓㩕硗⹙㵛㥝也", a_));
			IL_217:
			goto IL_33C;
			IL_290:
			throw new ArgumentNullException(RecordTableEnumerator.b("䠻嬽㠿㙁Ճ㑅ⵇ⭉", a_));
			IL_33C:
			A_1.ᜁ(text2, a_2);
			A_0.Read();
			return;
		}
		}
	}

	// Token: 0x06003365 RID: 13157 RVA: 0x001DABC8 File Offset: 0x001D9BC8
	private static XlsFont ᜀ(XmlReader A_0, RichTextString A_1, spr\u2306 A_2)
	{
		int a_ = 11;
		int num = 36;
		XlsFont xlsFont;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 30;
				continue;
			case 1:
				goto IL_2BC;
			case 2:
				num = 18;
				continue;
			case 3:
				goto IL_37E;
			case 4:
				xlsFont.IsItalic = XmlConvert.ToBoolean(A_0.Value);
				num = 19;
				continue;
			case 5:
				goto IL_E1;
			case 6:
				xlsFont.IsBold = XmlConvert.ToBoolean(A_0.Value);
				num = 37;
				continue;
			case 7:
				goto IL_213;
			case 8:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⡀", a_)))
				{
					num = 4;
					continue;
				}
				goto IL_248;
			case 9:
				xlsFont.Underline = FontUnderlineType.Double;
				num = 29;
				continue;
			case 10:
				A_0.Read();
				num = 1;
				continue;
			case 11:
				xlsFont.IsStrikethrough = (A_0.Value != RecordTableEnumerator.b("⽀ⱂᙄ㍆㭈≊♌⩎", a_));
				num = 3;
				continue;
			case 12:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 2;
					continue;
				}
				goto IL_213;
			}
			case 13:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㉀㥂", a_)))
				{
					num = 34;
					continue;
				}
				goto IL_2E4;
			case 14:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 20;
					continue;
				}
				A_0.Skip();
				num = 16;
				continue;
			case 15:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㉀㝂㝄⹆≈⹊", a_)))
				{
					num = 11;
					continue;
				}
				goto IL_37E;
			case 16:
				goto IL_2BC;
			case 17:
				goto IL_2BC;
			case 18:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("ⵀ≂ㅄ⹆❈", a_)))
				{
					num = 0;
					continue;
				}
				num = 32;
				continue;
			}
			case 19:
				goto IL_248;
			case 20:
				num = 12;
				continue;
			case 21:
				goto IL_2DF;
			case 22:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㑀", a_)))
				{
					num = 35;
					continue;
				}
				goto IL_11A;
			case 23:
				goto IL_2BC;
			case 24:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 21;
					continue;
				}
				num = 14;
				continue;
			case 25:
				goto IL_3B2;
			case 26:
				if (!A_0.IsEmptyElement)
				{
					num = 10;
					continue;
				}
				goto IL_59E;
			case 27:
				goto IL_11A;
			case 28:
				goto IL_2BC;
			case 29:
				if (true)
				{
				}
				goto IL_11A;
			case 30:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("㉀ⱂ⥄⹆ⵈൊ⑌⍎㵐", a_)))
				{
					num = 33;
					continue;
				}
				spr\u1AA0.ᜀ(A_0, A_2, xlsFont.OColor);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_49A;
				default:
					if (false)
					{
					}
					num = 17;
					continue;
				}
				break;
			}
			case 31:
				if (A_0.Value == RecordTableEnumerator.b("╀⅂⥄", a_))
				{
					num = 9;
					continue;
				}
				goto IL_11A;
			case 32:
				goto IL_49A;
			case 33:
				num = 7;
				continue;
			case 34:
				xlsFont.Size = (double)int.Parse(A_0.Value) / 100.0;
				num = 40;
				continue;
			case 35:
				num = 43;
				continue;
			case 37:
				goto IL_32C;
			case 38:
				goto IL_544;
			case 39:
				xlsFont.FontName = A_0.Value;
				A_0.MoveToElement();
				num = 25;
				continue;
			case 40:
				goto IL_2E4;
			case 41:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⍀", a_)))
				{
					num = 6;
					continue;
				}
				goto IL_32C;
			case 42:
			{
				if (A_1 == null)
				{
					num = 38;
					continue;
				}
				XlsWorkbook xlsWorkbook = A_1.Workbook;
				xlsFont = (XlsFont)xlsWorkbook.CreateFont(xlsWorkbook.InnerFonts[0], false);
				num = 41;
				continue;
			}
			case 43:
				if (A_0.Value == RecordTableEnumerator.b("㉀ⵂ≄", a_))
				{
					num = 44;
					continue;
				}
				num = 31;
				continue;
			case 44:
				xlsFont.Underline = FontUnderlineType.Single;
				num = 27;
				continue;
			}
			if (A_0 == null)
			{
				num = 5;
				continue;
			}
			num = 42;
			continue;
			IL_11A:
			A_0.MoveToElement();
			num = 26;
			continue;
			IL_213:
			A_0.Skip();
			num = 23;
			continue;
			IL_248:
			num = 15;
			continue;
			IL_2BC:
			num = 24;
			continue;
			IL_2E4:
			num = 22;
			continue;
			IL_32C:
			num = 8;
			continue;
			IL_37E:
			num = 13;
			continue;
			IL_3B2:
			A_0.Skip();
			num = 28;
			continue;
			IL_49A:
			if (!A_0.MoveToAttribute(RecordTableEnumerator.b("㕀㩂㕄≆⽈⩊⹌⩎", a_)))
			{
				goto IL_3B2;
			}
			num = 39;
		}
		IL_E1:
		throw new ArgumentNullException(RecordTableEnumerator.b("㍀♂⑄⍆ⱈ㥊", a_));
		IL_2DF:
		goto IL_59E;
		IL_544:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕀♂㵄㍆ࡈ㥊⡌⹎", a_));
		IL_59E:
		A_0.Skip();
		return xlsFont;
	}

	// Token: 0x06003366 RID: 13158 RVA: 0x001DB17C File Offset: 0x001DA17C
	private static void ᜀ(XmlReader A_0, XlsShapeLineFormat A_1, bool A_2, spr\u2306 A_3)
	{
		int a_ = 11;
		switch (0)
		{
		default:
		{
			int num = 19;
			for (;;)
			{
				bool flag;
				switch (num)
				{
				case 0:
					num = 17;
					continue;
				case 1:
					A_1.DashStyle = ShapeDashLineStyleType.Solid;
					num = 39;
					continue;
				case 2:
					num = 29;
					continue;
				case 3:
					num = 21;
					continue;
				case 4:
					if (A_1 == null)
					{
						num = 18;
						continue;
					}
					num = 40;
					continue;
				case 5:
					num = 33;
					continue;
				case 6:
					num = 15;
					continue;
				case 7:
					if (flag)
					{
						num = 1;
						continue;
					}
					goto IL_5A3;
				case 8:
				{
					int num2 = (int)Math.Round((double)int.Parse(A_0.Value) / 12700.0);
					A_1.Weight = (double)num2;
					num = 42;
					continue;
				}
				case 9:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㙀", a_)))
					{
						num = 8;
						continue;
					}
					goto IL_277;
				case 10:
					goto IL_EE;
				case 11:
					goto IL_393;
				case 12:
					goto IL_393;
				case 13:
				{
					bool isEmptyElement;
					if (!isEmptyElement)
					{
						num = 31;
						continue;
					}
					flag = true;
					goto IL_213;
				}
				case 14:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("≀⹂㕄⍆", a_)))
					{
						num = 36;
						continue;
					}
					goto IL_36E;
				case 15:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("ㅀ≂ㅄ㍆཈≊⅌⍎", a_)))
					{
						num = 38;
						continue;
					}
					A_0.Skip();
					num = 26;
					continue;
				}
				case 16:
					goto IL_1EE;
				case 17:
					goto IL_583;
				case 18:
					goto IL_56D;
				case 20:
					goto IL_393;
				case 21:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("㍀ⱂい⥆ⵈ", a_)))
					{
						num = 2;
						continue;
					}
					A_1.IsRound = true;
					A_0.Skip();
					num = 20;
					continue;
				}
				case 22:
					goto IL_393;
				case 23:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 43;
						continue;
					}
					A_0.Skip();
					num = 12;
					continue;
				case 24:
					goto IL_36E;
				case 25:
				{
					string localName;
					if (localName == RecordTableEnumerator.b("ㅀㅂ㙄㍆ൈ⩊㹌❎", a_))
					{
						spr\u1AA0.ᜄ(A_0);
						num = 28;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_213;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					break;
				}
				case 26:
					goto IL_393;
				case 27:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 0;
						continue;
					}
					num = 23;
					continue;
				case 28:
					goto IL_393;
				case 29:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("㉀ⱂ⥄⹆ⵈൊ⑌⍎㵐", a_)))
					{
						num = 41;
						continue;
					}
					OColor ocolor = new OColor(ExcelColors.Black);
					spr\u1AA0.ᜀ(A_0, A_3, ocolor);
					A_1.ForeColor = ocolor.ᜁ(A_1.Workbook);
					flag = true;
					num = 11;
					continue;
				}
				case 30:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 5;
						continue;
					}
					goto IL_481;
				}
				case 31:
					A_0.Read();
					num = 22;
					continue;
				case 32:
					goto IL_481;
				case 33:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("⽀ⱂ̈́⹆╈❊", a_)))
					{
						num = 3;
						continue;
					}
					A_1.Weight = 0.0;
					A_0.Read();
					num = 37;
					continue;
				}
				case 34:
					goto IL_583;
				case 35:
					goto IL_393;
				case 36:
				{
					XLSXShapeLineStyle style = (XLSXShapeLineStyle)Enum.Parse(typeof(XLSXShapeLineStyle), A_0.Value, false);
					A_1.Style = (ShapeLineStyleType)style;
					num = 24;
					continue;
				}
				case 37:
					goto IL_393;
				case 38:
					num = 32;
					continue;
				case 39:
					goto IL_2D1;
				case 40:
				{
					if (A_0.LocalName != RecordTableEnumerator.b("ⵀⵂ", a_))
					{
						num = 16;
						continue;
					}
					bool isEmptyElement = A_0.IsEmptyElement;
					if (true)
					{
					}
					num = 9;
					continue;
				}
				case 41:
					num = 25;
					continue;
				case 42:
					goto IL_277;
				case 43:
					num = 30;
					continue;
				}
				if (A_0 == null)
				{
					num = 10;
					continue;
				}
				num = 4;
				continue;
				IL_213:
				num = 34;
				continue;
				IL_277:
				num = 14;
				continue;
				IL_36E:
				flag = false;
				num = 13;
				continue;
				IL_393:
				num = 27;
				continue;
				IL_481:
				A_0.Skip();
				num = 35;
				continue;
				IL_583:
				num = 7;
			}
			IL_EE:
			throw new ArgumentNullException(RecordTableEnumerator.b("㍀♂⑄⍆ⱈ㥊", a_));
			IL_1EE:
			throw new XmlException(RecordTableEnumerator.b("ᑀⵂ⁄㽆㥈⹊⹌㭎㑐㝒畔⽖㑘㝚絜⭞`Ѣ", a_));
			IL_2D1:
			goto IL_5A3;
			IL_56D:
			throw new ArgumentNullException(RecordTableEnumerator.b("⍀ⱂ㝄⍆ⱈ㥊", a_));
			IL_5A3:
			A_0.Read();
			return;
		}
		}
	}
}
