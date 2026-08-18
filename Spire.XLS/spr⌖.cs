using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Xml;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlReaders.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlSerialization;

// Token: 0x02000299 RID: 665
internal abstract class spr\u2316 : ShapeParser
{
	// Token: 0x06002714 RID: 10004 RVA: 0x00162988 File Offset: 0x00161988
	public static void ᜂ()
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
		spr\u2316.ᜎ = new Dictionary<string, ShapeLineStyleType>();
		spr\u2316.ᜎ.Add(RecordTableEnumerator.b("㩈≊⍌⡎㵐㙒", a_), ShapeLineStyleType.LineSingle);
		spr\u2316.ᜎ.Add(RecordTableEnumerator.b("㵈⍊⑌ⅎՐ㭒㱔㥖", a_), ShapeLineStyleType.LineThinThin);
		spr\u2316.ᜎ.Add(RecordTableEnumerator.b("㵈⍊⑌ⅎՐ㭒㱔㑖㉘", a_), ShapeLineStyleType.LineThinThick);
		spr\u2316.ᜎ.Add(RecordTableEnumerator.b("㵈⍊⑌ⱎ㩐ݒ㵔㹖㝘", a_), ShapeLineStyleType.LineThickThin);
		spr\u2316.ᜎ.Add(RecordTableEnumerator.b("㵈⍊⑌ⱎ㩐ᅒご⍖⹘㹚㡜ㅞ㕠ୢ౤०", a_), ShapeLineStyleType.LineThickBetweenThin);
	}

	// Token: 0x06002715 RID: 10005 RVA: 0x00162A54 File Offset: 0x00161A54
	public static void ᜁ()
	{
		int a_ = 10;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u2316.ᜏ = new Dictionary<string, ShapeDashLineStyleType>();
		spr\u2316.ᜏ.Add(RecordTableEnumerator.b("㌿ⵁ⡃⽅ⱇ", a_), ShapeDashLineStyleType.Solid);
		spr\u2316.ᜏ.Add(RecordTableEnumerator.b("焿扁畃", a_), ShapeDashLineStyleType.DottedRound);
		spr\u2316.ᜏ.Add(RecordTableEnumerator.b("㌿㍁ㅃ❅㩇⽉ࡋ⅍⑏", a_), ShapeDashLineStyleType.Dotted);
		spr\u2316.ᜏ.Add(RecordTableEnumerator.b("␿⍁㝃⹅", a_), ShapeDashLineStyleType.Dashed);
		spr\u2316.ᜏ.Add(RecordTableEnumerator.b("␿⍁㝃⹅ే╉㡋", a_), ShapeDashLineStyleType.DashDot);
		spr\u2316.ᜏ.Add(RecordTableEnumerator.b("ⰿⵁ⩃ⅅే⭉㽋♍", a_), ShapeDashLineStyleType.MediumDashed);
		spr\u2316.ᜏ.Add(RecordTableEnumerator.b("ⰿⵁ⩃ⅅే⭉㽋♍ᑏ㵑⁓", a_), ShapeDashLineStyleType.MediumDashDot);
		spr\u2316.ᜏ.Add(RecordTableEnumerator.b("ⰿⵁ⩃ⅅే⭉㽋♍ᑏ㵑⁓ቕ㝗⹙", a_), ShapeDashLineStyleType.DashDotDot);
	}

	// Token: 0x06002716 RID: 10006 RVA: 0x00162B6C File Offset: 0x00161B6C
	private bool ᜀ()
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
		return this.ᜐ;
	}

	// Token: 0x06002717 RID: 10007 RVA: 0x00162BB0 File Offset: 0x00161BB0
	private void ᜀ(bool A_0)
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
		this.ᜐ = A_0;
	}

	// Token: 0x06002718 RID: 10008 RVA: 0x00162BF4 File Offset: 0x00161BF4
	public virtual bool ᜀ(XmlReader A_0, XlsShape A_1, RelationsCollection A_2, string A_3)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 40;
			for (;;)
			{
				TextBoxShapeBase textBoxShapeBase;
				bool flag;
				bool flag2;
				string a;
				switch (num)
				{
				case 0:
					this.ᜁ(A_0, textBoxShapeBase);
					num = 35;
					continue;
				case 1:
				{
					string value;
					if (value.Contains(RecordTableEnumerator.b("㙅㱇", a_)))
					{
						num = 32;
						continue;
					}
					num = 64;
					continue;
				}
				case 2:
				{
					OColor ocolor = this.ᜈ(A_0.Value);
					textBoxShapeBase.FillColor = ocolor.ᜁ(textBoxShapeBase.Workbook);
					textBoxShapeBase.Fill.ForeColor = textBoxShapeBase.FillColor;
					num = 25;
					continue;
				}
				case 3:
					goto IL_2D7;
				case 4:
					goto IL_89F;
				case 5:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("㉅ⵇ㉉㡋ⱍ㽏⩑", a_)))
					{
						num = 46;
						continue;
					}
					this.ᜇ(A_0, textBoxShapeBase);
					num = 57;
					continue;
				}
				case 6:
					if (flag)
					{
						num = 54;
						continue;
					}
					return flag;
				case 7:
					goto IL_2D7;
				case 8:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("Յ⑇⍉⥋⁍⑏ᙑ㕓≕㥗", a_)))
					{
						num = 51;
						continue;
					}
					flag2 = true;
					flag = this.ᜀ(A_0, textBoxShapeBase, out a);
					num = 30;
					continue;
				}
				case 9:
					goto IL_382;
				case 10:
					goto IL_43D;
				case 11:
					goto IL_68D;
				case 12:
				{
					OColor ocolor2 = this.ᜈ(A_0.Value);
					textBoxShapeBase.Line.BackColor = ocolor2.ᜁ(textBoxShapeBase.Workbook);
					textBoxShapeBase.Line.DashStyle = ShapeDashLineStyleType.Solid;
					textBoxShapeBase.Line.HasPattern = false;
					textBoxShapeBase.Line.Style = ShapeLineStyleType.LineSingle;
					textBoxShapeBase.Line.Weight = 0.5;
					num = 26;
					continue;
				}
				case 13:
					goto IL_162;
				case 14:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㕅㱇㡉⍋╍㕏ㅑ㭓㩕㝗⡙", a_)))
					{
						num = 12;
						continue;
					}
					goto IL_3CD;
				case 15:
					goto IL_259;
				case 16:
					num = 42;
					continue;
				case 17:
					goto IL_836;
				case 18:
					if (true)
					{
					}
					textBoxShapeBase.AlternativeText = A_0.Value.Split(new char[]
					{
						'#'
					})[0];
					num = 4;
					continue;
				case 19:
					num = 70;
					continue;
				case 20:
					num = 17;
					continue;
				case 21:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("㕅㱇㡉⍋╍㕏", a_)))
					{
						num = 20;
						continue;
					}
					num = 45;
					continue;
				}
				case 22:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㕅㡇⍉⡋", a_), RecordTableEnumerator.b("㍅㩇⑉癋㵍㍏㩑ㅓ㭕㥗⥙煛㍝य़šᙣ॥᭧թ੫ᩭ嵯ᅱ᭳᭵䉷ᕹ᩻᡽벅", a_)))
					{
						num = 36;
						continue;
					}
					goto IL_178;
				case 23:
					goto IL_2D7;
				case 24:
					goto IL_516;
				case 25:
					goto IL_6C8;
				case 26:
					goto IL_3CD;
				case 27:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㕅㱇㍉⁋⭍", a_)))
					{
						num = 0;
						continue;
					}
					goto IL_1EC;
				case 28:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("⁅ⅇ♉⁋", a_)))
					{
						num = 33;
						continue;
					}
					goto IL_421;
				}
				case 29:
					textBoxShapeBase.HasLineFormat = (A_0.Value != RecordTableEnumerator.b("⁅", a_));
					num = 24;
					continue;
				case 30:
					goto IL_2D7;
				case 31:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 61;
						continue;
					}
					A_0.Skip();
					num = 69;
					continue;
				case 32:
				{
					string value;
					textBoxShapeBase.Line.Weight = Convert.ToDouble(value.Split(new char[]
					{
						'p'
					})[0]);
					num = 39;
					continue;
				}
				case 33:
					num = 21;
					continue;
				case 34:
					if (textBoxShapeBase.HasLineFormat)
					{
						num = 50;
						continue;
					}
					goto IL_43D;
				case 35:
					goto IL_1EC;
				case 36:
					num = 48;
					continue;
				case 37:
					textBoxShapeBase.Name = A_0.Value;
					num = 71;
					continue;
				case 38:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("⁅ⅇ♉⁋⭍㑏", a_)))
					{
						num = 65;
						continue;
					}
					textBoxShapeBase.HasFill = true;
					num = 9;
					continue;
				case 39:
					goto IL_43D;
				case 41:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("❅⑇㹉", a_)))
					{
						num = 18;
						continue;
					}
					goto IL_89F;
				case 42:
					if (!flag)
					{
						num = 15;
						continue;
					}
					num = 31;
					continue;
				case 43:
					goto IL_2D7;
				case 44:
					num = 8;
					continue;
				case 45:
					if (textBoxShapeBase.HasLineFormat)
					{
						num = 53;
						continue;
					}
					goto IL_836;
				case 46:
					num = 28;
					continue;
				case 47:
				{
					string value = A_0.Value;
					num = 1;
					continue;
				}
				case 48:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("⽅ⱇ", a_)))
					{
						num = 37;
						continue;
					}
					goto IL_178;
				case 49:
					num = 60;
					continue;
				case 50:
					num = 14;
					continue;
				case 51:
					num = 5;
					continue;
				case 52:
					goto IL_382;
				case 53:
					this.ᜁ(A_0, textBoxShapeBase, A_2, A_3);
					num = 3;
					continue;
				case 54:
					num = 62;
					continue;
				case 55:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㕅㱇㡉⍋╍㕏㙑", a_)))
					{
						num = 29;
						continue;
					}
					goto IL_516;
				case 56:
					if (A_1 == null)
					{
						num = 11;
						continue;
					}
					textBoxShapeBase = (TextBoxShapeBase)A_1.Clone(A_1.Parent, null, null, false);
					spr\u2316.ᜀ(A_0, textBoxShapeBase);
					num = 27;
					continue;
				case 57:
					goto IL_2D7;
				case 58:
					return flag;
				case 59:
					this.ᜀ(textBoxShapeBase);
					num = 58;
					continue;
				case 60:
					if (a != RecordTableEnumerator.b("ᕅ⁇⭉㱋⭍", a_))
					{
						num = 59;
						continue;
					}
					return flag;
				case 61:
					num = 66;
					continue;
				case 62:
					if (flag2)
					{
						num = 49;
						continue;
					}
					return flag;
				case 63:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㕅㱇㡉⍋╍㕏║ㅓ㽕㽗㉙⡛", a_)))
					{
						num = 47;
						continue;
					}
					goto IL_43D;
				case 64:
				{
					string value;
					if (value.Contains(RecordTableEnumerator.b("⭅╇", a_)))
					{
						num = 67;
						continue;
					}
					goto IL_43D;
				}
				case 65:
					textBoxShapeBase.HasFill = false;
					textBoxShapeBase.Fill.BackColor = spr\u1D39.ᜂ;
					textBoxShapeBase.Fill.ForeColor = spr\u1D39.ᜂ;
					num = 52;
					continue;
				case 66:
				{
					string localName;
					if ((localName = A_0.LocalName) == null)
					{
						goto IL_836;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_421;
					default:
						if (false)
						{
						}
						num = 44;
						continue;
					}
					break;
				}
				case 67:
				{
					string value;
					textBoxShapeBase.Line.Weight = Convert.ToDouble(value.Split(new char[]
					{
						'm'
					})[0]);
					num = 10;
					continue;
				}
				case 68:
					if (A_0.NodeType != XmlNodeType.EndElement)
					{
						num = 16;
						continue;
					}
					goto IL_259;
				case 69:
					goto IL_2D7;
				case 70:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("⁅ⅇ♉⁋ⵍ㽏㹑㭓⑕", a_)))
					{
						num = 2;
						continue;
					}
					goto IL_6C8;
				case 71:
					goto IL_178;
				case 72:
					if (textBoxShapeBase.HasFill)
					{
						num = 19;
						continue;
					}
					goto IL_6C8;
				}
				if (A_0 == null)
				{
					num = 13;
					continue;
				}
				num = 56;
				continue;
				IL_178:
				A_0.MoveToElement();
				A_0.Read();
				flag = true;
				flag2 = false;
				a = null;
				num = 43;
				continue;
				IL_1EC:
				num = 38;
				continue;
				IL_259:
				A_0.Read();
				num = 6;
				continue;
				IL_2D7:
				num = 68;
				continue;
				IL_382:
				num = 55;
				continue;
				IL_3CD:
				num = 63;
				continue;
				IL_421:
				this.ᜂ(A_0, textBoxShapeBase, A_2, A_3);
				num = 7;
				continue;
				IL_43D:
				num = 72;
				continue;
				IL_516:
				num = 34;
				continue;
				IL_6C8:
				num = 41;
				continue;
				IL_836:
				A_0.Skip();
				num = 23;
				continue;
				IL_89F:
				num = 22;
			}
			IL_162:
			throw new ArgumentNullException(RecordTableEnumerator.b("㑅ⵇ⭉⡋⭍≏", a_));
			IL_68D:
			throw new ArgumentNullException(RecordTableEnumerator.b("≅ⵇⱉⵋ㭍㱏♑ݓ㹕㥗⩙㥛", a_));
		}
		}
	}

	// Token: 0x06002719 RID: 10009 RVA: 0x001635D0 File Offset: 0x001625D0
	internal static void ᜀ(XmlReader A_0, XlsShape A_1)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				XlsShape xlsShape;
				spr\u1D9B spr_u1D9B;
				int num2;
				switch (num)
				{
				case 1:
					if (xlsShape.EnableAlternateContent)
					{
						num = 4;
						continue;
					}
					return;
				case 2:
					num = 1;
					continue;
				case 3:
					if (true)
					{
					}
					spr_u1D9B.StartId = num2;
					num = 13;
					continue;
				case 4:
					A_1.EnableAlternateContent = true;
					A_1.XmlDataStream = xlsShape.XmlDataStream;
					xlsShape.Remove();
					num = 7;
					continue;
				case 5:
					spr_u1D9B = A_1.Worksheet.InnerShapes;
					num = 11;
					continue;
				case 6:
				{
					string s;
					if (int.TryParse(s, out num2))
					{
						num = 5;
						continue;
					}
					return;
				}
				case 7:
					return;
				case 8:
					if (xlsShape != null)
					{
						num = 2;
						continue;
					}
					return;
				case 9:
				{
					string value;
					int num3;
					string s = value.Substring(num3 + 2);
					num = 6;
					continue;
				}
				case 10:
				{
					int num3;
					if (num3 >= 0)
					{
						num = 9;
						continue;
					}
					return;
				}
				case 11:
					if (spr_u1D9B.StartId != 0)
					{
						goto IL_88;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_15E;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				case 12:
				{
					string value = A_0.Value;
					int num3 = value.IndexOf(RecordTableEnumerator.b("ᵁ㝃", a_));
					goto IL_15E;
				}
				case 13:
					goto IL_88;
				}
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⭁⁃", a_)))
				{
					num = 12;
					continue;
				}
				break;
				IL_88:
				xlsShape = spr_u1D9B.ᜀ(num2);
				A_1.ShapeId = num2;
				num = 8;
				continue;
				IL_15E:
				num = 10;
			}
			return;
		}
		}
	}

	// Token: 0x0600271A RID: 10010 RVA: 0x001637D8 File Offset: 0x001627D8
	private void ᜇ(XmlReader A_0, TextBoxShapeBase A_1)
	{
		int a_ = 3;
		int num = 8;
		for (;;)
		{
			switch (num)
			{
			case 0:
				A_0.Read();
				num = 16;
				continue;
			case 1:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 21;
					continue;
				}
				num = 22;
				continue;
			case 2:
				if (!A_0.IsEmptyElement)
				{
					num = 0;
					continue;
				}
				goto IL_2CB;
			case 3:
			{
				string value = A_0.Value;
				Dictionary<string, string> a_2 = base.SplitStyle(value);
				this.ᜁ(A_1, a_2);
				num = 5;
				continue;
			}
			case 4:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䨸伺䐼匾⑀", a_)))
				{
					num = 3;
					continue;
				}
				goto IL_199;
			case 5:
				goto IL_199;
			case 6:
				goto IL_10E;
			case 7:
				this.ᜆ(A_0, A_1);
				num = 19;
				continue;
			case 9:
				goto IL_147;
			case 10:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 17;
					continue;
				}
				goto IL_113;
			}
			case 11:
				goto IL_8D;
			case 12:
				num = 10;
				continue;
			case 13:
				if (A_1 == null)
				{
					goto IL_13C;
				}
				num = 18;
				continue;
			case 14:
				goto IL_171;
			case 15:
				goto IL_171;
			case 16:
				goto IL_171;
			case 17:
				num = 20;
				continue;
			case 18:
				if (A_0.LocalName != RecordTableEnumerator.b("䴸帺䔼䬾⍀ⱂ㵄", a_))
				{
					num = 6;
					continue;
				}
				num = 4;
				continue;
			case 19:
				goto IL_171;
			case 20:
			{
				string localName;
				if (localName == RecordTableEnumerator.b("崸刺䬼", a_))
				{
					num = 7;
					continue;
				}
				goto IL_113;
			}
			case 21:
				goto IL_194;
			case 22:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 12;
					continue;
				}
				A_0.Skip();
				num = 14;
				continue;
			}
			if (A_0 == null)
			{
				num = 11;
				continue;
			}
			num = 13;
			continue;
			IL_113:
			if (true)
			{
			}
			A_0.Skip();
			num = 15;
			continue;
			IL_13C:
			num = 9;
			continue;
			IL_199:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_13C;
			default:
				if (false)
				{
				}
				A_0.MoveToElement();
				num = 2;
				continue;
			}
			IL_171:
			num = 1;
		}
		IL_8D:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬸帺尼嬾⑀ㅂ", a_));
		IL_10E:
		throw new XmlException(RecordTableEnumerator.b("永唺堼䜾≀㍂⁄⑆㵈⹊⥌潎⥐㹒㥔睖ⵘ㩚㩜煞", a_));
		IL_147:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴸帺䔼䬾̀ⱂ㵄", a_));
		IL_194:
		IL_2CB:
		A_0.Skip();
	}

	// Token: 0x0600271B RID: 10011 RVA: 0x00163AB8 File Offset: 0x00162AB8
	private void ᜆ(XmlReader A_0, TextBoxShapeBase A_1)
	{
		int a_ = 12;
		int num = 13;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_13D;
			case 1:
				goto IL_13D;
			case 2:
				goto IL_13D;
			case 3:
				goto IL_10A;
			case 4:
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				num = 16;
				continue;
			case 5:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 15;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1E1;
				default:
					if (false)
					{
					}
					A_0.Skip();
					num = 8;
					continue;
				}
				break;
			case 6:
			{
				string localName;
				if (localName == RecordTableEnumerator.b("⑁⭃⡅㱇", a_))
				{
					num = 7;
					continue;
				}
				goto IL_192;
			}
			case 7:
				this.ᜅ(A_0, A_1);
				num = 2;
				continue;
			case 8:
				goto IL_13D;
			case 9:
				goto IL_6F;
			case 10:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 17;
					continue;
				}
				goto IL_192;
			}
			case 11:
				A_0.Read();
				num = 1;
				continue;
			case 12:
				goto IL_165;
			case 14:
				if (true)
				{
				}
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 12;
					continue;
				}
				num = 5;
				continue;
			case 15:
				num = 10;
				continue;
			case 16:
				goto IL_1E1;
			case 17:
				num = 6;
				continue;
			}
			if (A_0 == null)
			{
				num = 9;
				continue;
			}
			num = 4;
			continue;
			IL_13D:
			num = 14;
			continue;
			IL_192:
			A_0.Skip();
			num = 0;
			continue;
			IL_1E1:
			if (A_0.IsEmptyElement)
			{
				goto IL_1F9;
			}
			num = 11;
		}
		IL_6F:
		throw new ArgumentNullException(RecordTableEnumerator.b("ぁ⅃❅ⱇ⽉㹋", a_));
		IL_10A:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙁⅃㹅㱇ࡉ⍋㙍", a_));
		IL_165:
		IL_1F9:
		A_0.Read();
	}

	// Token: 0x0600271C RID: 10012 RVA: 0x00163CC8 File Offset: 0x00162CC8
	private void ᜅ(XmlReader A_0, TextBoxShapeBase A_1)
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			int num = 3;
			XmlReader xmlReader;
			IFont font;
			string text;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					goto IL_FC;
				case 1:
					goto IL_65;
				case 2:
				{
					if (A_1 == null)
					{
						num = 4;
						continue;
					}
					Stream stream = ShapeParser.ReadNodeAsStream(A_0);
					stream.Position = 0L;
					xmlReader = UtilityMethods.ᜀ(stream);
					bool flag = this.ᜀ(xmlReader);
					stream.Position = 0L;
					xmlReader = UtilityMethods.ᜀ(stream);
					font = A_1.Workbook.CreateFont();
					num = 11;
					continue;
				}
				case 4:
					goto IL_F7;
				case 5:
					if (xmlReader.MoveToAttribute(RecordTableEnumerator.b("㙄⹆㍈⹊", a_)))
					{
						num = 8;
						continue;
					}
					goto IL_FC;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_208;
					}
					if (false)
					{
					}
					font.FontName = xmlReader.Value;
					num = 9;
					continue;
				case 7:
					goto IL_128;
				case 8:
					font.Size = (double)XmlConvert.ToInt32(xmlReader.Value) / 20.0;
					num = 0;
					continue;
				case 9:
					goto IL_13E;
				case 10:
				{
					bool flag;
					if (flag)
					{
						num = 7;
						continue;
					}
					goto IL_21C;
				}
				case 11:
					if (xmlReader.MoveToAttribute(RecordTableEnumerator.b("⍄♆⩈⹊", a_)))
					{
						num = 6;
						continue;
					}
					goto IL_13E;
				}
				if (A_0 == null)
				{
					num = 1;
					continue;
				}
				num = 2;
				continue;
				IL_FC:
				xmlReader.MoveToElement();
				text = string.Empty;
				num = 10;
				continue;
				IL_13E:
				num = 5;
			}
			IL_65:
			throw new ArgumentNullException(RecordTableEnumerator.b("㝄≆⡈⽊⡌㵎", a_));
			IL_F7:
			goto IL_208;
			IL_128:
			text = xmlReader.ReadElementContentAsString();
			IRichTextString richText = A_1.RichText;
			int length = richText.Text.Length;
			richText.Append(text, font);
			return;
			IL_208:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅄ≆ㅈ㽊ཌ⁎⥐", a_));
			IL_21C:
			xmlReader.Skip();
			return;
		}
		}
	}

	// Token: 0x0600271D RID: 10013 RVA: 0x00163EF8 File Offset: 0x00162EF8
	private bool ᜀ(XmlReader A_0)
	{
		int a_ = 8;
		int num = 7;
		for (;;)
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
				switch (num)
				{
				case 0:
					goto IL_6A;
				case 1:
					return true;
				case 2:
					if (A_0.LocalName != RecordTableEnumerator.b("堽⼿ⱁぃ", a_))
					{
						num = 5;
						continue;
					}
					if (true)
					{
					}
					A_0.Read();
					A_0.Read();
					num = 4;
					continue;
				case 3:
					num = 6;
					continue;
				case 4:
					if (A_0.LocalName == RecordTableEnumerator.b("堽⼿ⱁぃ", a_))
					{
						num = 3;
						continue;
					}
					return false;
				case 5:
					goto IL_CA;
				case 6:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 1;
						continue;
					}
					return false;
				}
				break;
			}
			IL_55:
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			num = 2;
			continue;
			goto IL_55;
		}
		IL_6A:
		throw new ArgumentNullException();
		IL_CA:
		throw new XmlException();
	}

	// Token: 0x0600271E RID: 10014 RVA: 0x00164020 File Offset: 0x00163020
	protected virtual void ᜀ(TextBoxShapeBase A_0)
	{
		int a_ = 7;
		if (true)
		{
		}
		if (A_0 != null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_14;
			}
			if (false)
			{
			}
			XlsWorksheet xlsWorksheet = (XlsWorksheet)A_0.Worksheet;
			xlsWorksheet.InnerShapes.AddShape(A_0);
			return;
		}
		IL_14:
		throw new ArgumentNullException(RecordTableEnumerator.b("帼倾ⱀ⹂⁄⥆㵈", a_));
	}

	// Token: 0x0600271F RID: 10015 RVA: 0x00164098 File Offset: 0x00163098
	private bool ᜀ(XmlReader A_0, TextBoxShapeBase A_1, out string A_2)
	{
		int a_ = 15;
		int num = 8;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_17B;
			case 1:
			{
				int num2;
				switch (num2)
				{
				case 0:
					A_1.IsMoveWithCell = !spr\u2316.ᜀ(A_0, true);
					num = 5;
					continue;
				case 1:
					A_1.IsSizeWithCell = !spr\u2316.ᜀ(A_0, true);
					num = 3;
					continue;
				case 2:
					base.ParseAnchor(A_0, A_1);
					num = 27;
					continue;
				case 3:
					A_1.HAlignment = (CommentHAlignType)Enum.Parse(typeof(CommentHAlignType), A_0.ReadElementContentAsString(), false);
					num = 0;
					continue;
				case 4:
					A_1.VAlignment = (CommentVAlignType)Enum.Parse(typeof(CommentVAlignType), A_0.ReadElementContentAsString(), false);
					num = 23;
					continue;
				case 5:
				{
					string text = A_0.ReadElementContentAsString();
					A_1.IsTextLocked = XmlConvert.ToBoolean(text.ToLower());
					num = 26;
					continue;
				}
				default:
					num = 29;
					continue;
				}
				break;
			}
			case 2:
				if (A_0.LocalName != RecordTableEnumerator.b("ل⭆⁈⹊⍌㭎ᕐ㉒⅔㙖", a_))
				{
					num = 7;
					continue;
				}
				A_2 = null;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					num = 14;
					continue;
				}
				break;
			case 3:
				goto IL_17B;
			case 4:
			{
				int num2;
				string localName;
				if (spr\u22D2.\u1712.TryGetValue(localName, out num2))
				{
					num = 32;
					continue;
				}
				goto IL_3B2;
			}
			case 5:
				goto IL_17B;
			case 6:
				goto IL_D6;
			case 7:
				goto IL_388;
			case 9:
				goto IL_3B2;
			case 10:
				return false;
			case 11:
				num = 13;
				continue;
			case 12:
				goto IL_45C;
			case 13:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 18;
					continue;
				}
				goto IL_3B2;
			}
			case 14:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("੄╆⍈⹊⹌㭎Ր⩒╔㉖", a_)))
				{
					num = 21;
					continue;
				}
				goto IL_38D;
			case 15:
				goto IL_2A4;
			case 16:
				if (spr\u22D2.\u1712 == null)
				{
					num = 19;
					continue;
				}
				goto IL_D6;
			case 17:
				goto IL_2A4;
			case 18:
				num = 16;
				continue;
			case 19:
				spr\u22D2.\u1712 = new Dictionary<string, int>(6)
				{
					{
						RecordTableEnumerator.b("ࡄ⡆㽈⹊ᩌ♎═㭒ᙔ㉖㕘㝚⹜", a_),
						0
					},
					{
						RecordTableEnumerator.b("ᙄ⹆㍈⹊ᩌ♎═㭒ᙔ㉖㕘㝚⹜", a_),
						1
					},
					{
						RecordTableEnumerator.b("ф⥆⩈⍊≌㵎", a_),
						2
					},
					{
						RecordTableEnumerator.b("ᅄ≆ㅈ㽊Ռ๎㵐㩒㉔㥖", a_),
						3
					},
					{
						RecordTableEnumerator.b("ᅄ≆ㅈ㽊ᭌ๎㵐㩒㉔㥖", a_),
						4
					},
					{
						RecordTableEnumerator.b("ॄ⡆⩈⁊᥌⩎⥐❒", a_),
						5
					}
				};
				num = 6;
				continue;
			case 20:
				if (A_1 == null)
				{
					num = 12;
					continue;
				}
				num = 2;
				continue;
			case 21:
				A_2 = A_0.Value;
				if (true)
				{
				}
				num = 24;
				continue;
			case 22:
				goto IL_17B;
			case 23:
				goto IL_17B;
			case 24:
				if (A_2 == RecordTableEnumerator.b("ᕄ⹆⩈㽊", a_))
				{
					num = 10;
					continue;
				}
				goto IL_38D;
			case 25:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 30;
					continue;
				}
				num = 31;
				continue;
			case 26:
				goto IL_17B;
			case 27:
				goto IL_17B;
			case 28:
				goto IL_B1;
			case 29:
				num = 9;
				continue;
			case 30:
				goto IL_2C7;
			case 31:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 11;
					continue;
				}
				goto IL_17B;
			case 32:
				num = 1;
				continue;
			}
			if (A_0 == null)
			{
				num = 28;
				continue;
			}
			num = 20;
			continue;
			IL_D6:
			num = 4;
			continue;
			IL_17B:
			A_0.Read();
			num = 17;
			continue;
			IL_2A4:
			num = 25;
			continue;
			IL_38D:
			A_0.Read();
			A_1.IsMoveWithCell = true;
			A_1.IsSizeWithCell = true;
			num = 15;
			continue;
			IL_3B2:
			this.ᜀ(A_0, A_1);
			num = 22;
		}
		IL_B1:
		throw new ArgumentNullException(RecordTableEnumerator.b("㝄≆⡈⽊⡌㵎", a_));
		IL_2C7:
		A_0.Read();
		return true;
		IL_388:
		throw new XmlException(RecordTableEnumerator.b("၄⥆ⱈ㍊㵌⩎㉐❒ご㍖祘⍚ぜ㍞䅠ᝢ੤౦౨ժ", a_));
		IL_45C:
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅄ≆ㅈ㽊ཌ⁎⥐", a_));
	}

	// Token: 0x06002720 RID: 10016 RVA: 0x00164578 File Offset: 0x00163578
	protected virtual void ᜀ(XmlReader A_0, TextBoxShapeBase A_1)
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
		A_0.Skip();
	}

	// Token: 0x06002721 RID: 10017 RVA: 0x001645BC File Offset: 0x001635BC
	public static bool ᜀ(XmlReader A_0, bool A_1)
	{
		int a_ = 13;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_63;
			case 1:
			{
				bool result;
				return result;
			}
			case 2:
				if (!A_0.IsEmptyElement)
				{
					num = 7;
					continue;
				}
				A_0.Read();
				num = 3;
				continue;
			case 3:
			{
				bool result;
				return result;
			}
			case 5:
			{
				string text;
				bool result = bool.Parse(text);
				goto IL_74;
			}
			case 6:
			{
				string text;
				if (text.Length != 0)
				{
					num = 5;
					continue;
				}
				bool result;
				return result;
			}
			case 7:
			{
				string text = A_0.ReadElementContentAsString();
				num = 6;
				continue;
			}
			}
			if (A_0 != null)
			{
				bool result = A_1;
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
				num = 0;
				continue;
			}
			IL_74:
			num = 1;
		}
		IL_63:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄♆ⵈ⹊㽌", a_));
	}

	// Token: 0x06002722 RID: 10018 RVA: 0x001646C0 File Offset: 0x001636C0
	private void ᜂ(XmlReader A_0, TextBoxShapeBase A_1, RelationsCollection A_2, string A_3)
	{
		int a_ = 10;
		int num = 17;
		for (;;)
		{
			ShapeFillType shapeFillType;
			ShapeFillType shapeFillType2;
			switch (num)
			{
			case 0:
			{
				OColor ocolor = this.ᜈ(A_0.Value);
				A_1.FillColor = ocolor.ᜁ(A_1.Workbook);
				num = 15;
				continue;
			}
			case 1:
				if (true)
				{
				}
				num = 13;
				continue;
			case 2:
				goto IL_2AC;
			case 3:
				switch (shapeFillType)
				{
				case ShapeFillType.SolidColor:
					goto IL_282;
				case ShapeFillType.Pattern:
					goto IL_125;
				case ShapeFillType.Texture:
					goto IL_1A7;
				case ShapeFillType.Picture:
					this.ᜀ(A_0, A_1, A_2);
					num = 14;
					continue;
				case ShapeFillType.UnknownGradient:
				case (ShapeFillType)5:
				case (ShapeFillType)6:
					return;
				case ShapeFillType.Gradient:
					goto IL_C5;
				default:
					num = 8;
					continue;
				}
				break;
			case 4:
				goto IL_E2;
			case 5:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⼿㉁╃╅ⅇ㹉㕋", a_)))
				{
					num = 10;
					continue;
				}
				goto IL_2AC;
			case 6:
				goto IL_24F;
			case 7:
				goto IL_77;
			case 8:
				return;
			case 9:
				if (!A_0.MoveToAttribute(RecordTableEnumerator.b("㐿㭁㑃⍅", a_)))
				{
					num = 1;
					continue;
				}
				num = 16;
				continue;
			case 10:
				A_1.Fill.Transparency = 1.0 - this.ᜇ(A_0.Value);
				num = 2;
				continue;
			case 11:
				A_1.HasFill = false;
				num = 18;
				continue;
			case 12:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_C5;
				default:
					if (false)
					{
					}
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("⌿ⵁ⡃⥅㩇", a_)))
					{
						num = 0;
						continue;
					}
					goto IL_E2;
				}
				break;
			case 13:
				shapeFillType2 = this.ᜊ(RecordTableEnumerator.b("㌿ⵁ⡃⽅ⱇ", a_));
				goto IL_1CA;
			case 14:
				goto IL_1C5;
			case 15:
				if (A_1.FillColor == spr\u1D39.ᜂ)
				{
					num = 11;
					continue;
				}
				goto IL_28B;
			case 16:
				shapeFillType2 = this.ᜊ(A_0.Value);
				goto IL_1CA;
			case 18:
				goto IL_28B;
			case 19:
				if (A_1 == null)
				{
					num = 6;
					continue;
				}
				num = 9;
				continue;
			}
			if (A_0 == null)
			{
				num = 7;
				continue;
			}
			num = 19;
			continue;
			IL_E2:
			ShapeFillType shapeFillType3;
			shapeFillType = shapeFillType3;
			num = 3;
			continue;
			IL_1CA:
			shapeFillType3 = shapeFillType2;
			num = 5;
			continue;
			IL_28B:
			A_1.Fill.ForeColor = A_1.FillColor;
			num = 4;
			continue;
			IL_2AC:
			num = 12;
		}
		IL_77:
		throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁╃≅ⵇ㡉", a_));
		IL_C5:
		this.ᜃ(A_0, A_1);
		return;
		IL_125:
		this.ᜁ(A_0, A_1, A_2);
		return;
		IL_1A7:
		this.ᜂ(A_0, A_1, A_2);
		return;
		IL_1C5:
		return;
		IL_24F:
		throw new ArgumentNullException(RecordTableEnumerator.b("㐿❁㱃㉅ੇ╉㑋", a_));
		IL_282:
		this.ᜄ(A_0, A_1);
	}

	// Token: 0x06002723 RID: 10019 RVA: 0x001649CC File Offset: 0x001639CC
	private void ᜄ(XmlReader A_0, TextBoxShapeBase A_1)
	{
		int a_ = 4;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					num = 4;
					continue;
				}
				goto IL_4D;
			case 1:
				goto IL_D8;
			case 2:
				if (true)
				{
				}
				break;
			case 3:
				goto IL_4B;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_4D;
				default:
					goto IL_10C;
				}
				break;
			case 5:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("唹䰻弽⌿⭁ぃ㽅", a_)))
				{
					num = 6;
					continue;
				}
				goto IL_121;
			case 6:
				A_1.Fill.Transparency = 1.0 - this.ᜇ(A_0.Value);
				num = 1;
				continue;
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			num = 0;
			continue;
			IL_4D:
			A_1.Fill.FillType = ShapeFillType.SolidColor;
			num = 5;
		}
		IL_4B:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠹夻弽␿❁㙃", a_));
		IL_D8:
		goto IL_121;
		IL_10C:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("丹夻䘽㐿A⭃㹅", a_));
		IL_121:
		A_0.MoveToElement();
		A_0.Skip();
	}

	// Token: 0x06002724 RID: 10020 RVA: 0x00164B08 File Offset: 0x00163B08
	private void ᜃ(XmlReader A_0, TextBoxShapeBase A_1)
	{
		int a_ = 5;
		int num = 0;
		for (;;)
		{
			string a_2;
			switch (num)
			{
			case 1:
				goto IL_7C;
			case 2:
				num = 5;
				continue;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_CB;
				default:
				{
					if (false)
					{
					}
					if (A_1 == null)
					{
						num = 7;
						continue;
					}
					A_1.Fill.FillType = ShapeFillType.Gradient;
					a_2 = null;
					A_1.Fill.GradientColorType = this.ᜀ(A_0, out a_2);
					GradientColorType gradientColorType = A_1.Fill.GradientColorType;
					if (true)
					{
					}
					num = 4;
					continue;
				}
				}
				break;
			case 4:
			{
				GradientColorType gradientColorType;
				switch (gradientColorType)
				{
				case GradientColorType.OneColor:
					A_1.Fill.BackColor = A_1.FillColor;
					A_1.Fill.GradientDegree = this.ᜆ(a_2);
					num = 1;
					continue;
				case GradientColorType.TwoColor:
				{
					OColor ocolor = this.ᜈ(a_2);
					A_1.Fill.BackColor = A_1.FillColor;
					A_1.Fill.ForeColor = ocolor.ᜁ(A_1.Workbook);
					num = 8;
					continue;
				}
				case GradientColorType.Preset:
					goto IL_CB;
				default:
					num = 2;
					continue;
				}
				break;
			}
			case 5:
				goto IL_89;
			case 6:
				goto IL_4C;
			case 7:
				goto IL_C6;
			case 8:
				goto IL_146;
			case 9:
				goto IL_F2;
			}
			if (A_0 == null)
			{
				num = 6;
				continue;
			}
			num = 3;
			continue;
			IL_CB:
			A_1.Fill.PresetGradient(this.ᜅ(a_2));
			num = 9;
		}
		IL_4C:
		throw new ArgumentNullException(RecordTableEnumerator.b("䤺堼帾╀♂㝄", a_));
		IL_7C:
		IL_89:
		goto IL_1BF;
		IL_C6:
		throw new ArgumentNullException(RecordTableEnumerator.b("伺堼䜾㕀ł⩄㽆", a_));
		IL_F2:
		IL_146:
		IL_1BF:
		this.ᜂ(A_0, A_1);
	}

	// Token: 0x06002725 RID: 10021 RVA: 0x00164CDC File Offset: 0x00163CDC
	private void ᜂ(XmlReader A_0, TextBoxShapeBase A_1, RelationsCollection A_2)
	{
		int a_ = 8;
		switch (0)
		{
		default:
		{
			int num = 0;
			string text;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_197;
				case 2:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䨽⤿㙁⡃⍅", a_), RecordTableEnumerator.b("䬽㈿ⱁ繃㕅⭇≉⥋⍍ㅏ⅑祓㭕ㅗ㥙⹛ㅝ፟ൡɣብ䕧३ͫͭ䩯ᵱታၵᅷ᥹᥻䑽", a_)))
					{
						num = 5;
						continue;
					}
					goto IL_71;
				case 3:
					goto IL_153;
				case 4:
					goto IL_6C;
				case 5:
					goto IL_1AD;
				case 6:
					goto IL_71;
				case 7:
					if (A_1 == null)
					{
						num = 3;
						continue;
					}
					num = 11;
					continue;
				case 8:
					if (A_1.Fill.Texture == GradientTextureType.UserDefined)
					{
						num = 9;
						continue;
					}
					goto IL_286;
				case 9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1AD;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 10;
						continue;
					}
					break;
				case 10:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䰽┿⹁ⵃ≅", a_), RecordTableEnumerator.b("䬽㈿ⱁ繃㕅⭇≉⥋⍍ㅏ⅑祓㭕ㅗ㥙⹛ㅝ፟ൡɣብ䕧३ͫͭ䩯ᵱታၵᅷ᥹᥻䑽", a_)))
					{
						num = 1;
						continue;
					}
					goto IL_286;
				case 11:
					if (A_2 == null)
					{
						num = 12;
						continue;
					}
					text = RecordTableEnumerator.b("圽ⴿ⍁⍃⍅", a_);
					num = 2;
					continue;
				case 12:
					goto IL_25C;
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				num = 7;
				continue;
				IL_71:
				A_1.Fill.FillType = ShapeFillType.Texture;
				A_1.Fill.Texture = this.ᜃ(text);
				num = 8;
				continue;
				IL_1AD:
				text = A_0.Value;
				num = 6;
			}
			IL_6C:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰽┿⍁⁃⍅㩇", a_));
			IL_153:
			throw new ArgumentNullException(RecordTableEnumerator.b("䨽┿㩁ぃх❇㉉", a_));
			IL_197:
			sprវ sprវ = A_1.ParentWorkbook.DataHolder;
			string value = A_0.Value;
			sprᦨ sprᦨ = A_2[value];
			string text2 = A_2.ItemPath;
			int length = text2.LastIndexOf('/');
			text2 = text2.Substring(0, length);
			length = text2.LastIndexOf('/');
			text2 = text2.Substring(0, length);
			text2 = sprវ.ᜀ(text2, sprᦨ.ᜂ());
			Image im = sprវ.ᜋ(text2);
			A_1.Fill.CustomTexture(im, text);
			return;
			IL_25C:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰽┿⹁╃㉅ⅇ╉≋湍㍏㵑㡓㩕㵗㥙⡛㝝ཟౡ", a_));
			IL_286:
			A_1.Fill.PresetTextured(this.ᜃ(text));
			return;
		}
		}
	}

	// Token: 0x06002726 RID: 10022 RVA: 0x00164F84 File Offset: 0x00163F84
	private void ᜁ(XmlReader A_0, TextBoxShapeBase A_1, RelationsCollection A_2)
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					string a_2 = A_0.Value;
					num = 8;
					continue;
				}
				case 1:
				{
					sprវ sprវ = A_1.ParentWorkbook.DataHolder;
					string value = A_0.Value;
					sprᦨ sprᦨ = A_2[value];
					string text = A_2.ItemPath;
					int length = text.LastIndexOf('/');
					text = text.Substring(0, length);
					length = text.LastIndexOf('/');
					text = text.Substring(0, length);
					text = sprវ.ᜀ(text, sprᦨ.ᜂ());
					sprវ.ᜋ(text);
					A_1.Fill.Patterned(A_1.Fill.Pattern);
					num = 10;
					continue;
				}
				case 2:
					goto IL_2EB;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_79;
					default:
						goto IL_13A;
					}
					break;
				case 5:
					goto IL_74;
				case 6:
					if (A_1 == null)
					{
						num = 4;
						continue;
					}
					num = 7;
					continue;
				case 7:
				{
					if (A_2 == null)
					{
						num = 2;
						continue;
					}
					string a_2 = RecordTableEnumerator.b("唻匽ℿ╁⅃", a_);
					num = 11;
					continue;
				}
				case 8:
					goto IL_79;
				case 9:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("弻儽ⰿⵁ㙃瑅", a_)))
					{
						num = 14;
						continue;
					}
					goto IL_145;
				case 10:
					goto IL_28B;
				case 11:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䠻圽㐿⹁⅃", a_), RecordTableEnumerator.b("䤻䰽⸿硁㝃╅⁇⽉⅋⽍⍏网㥓㽕㭗⡙㍛ⵝཟѡၣ䭥୧թū呭Ὧᑱታή᭷ό䙻ᅽ", a_)))
					{
						num = 0;
						continue;
					}
					goto IL_79;
				case 12:
					goto IL_145;
				case 13:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("主嬽ⰿ⭁⁃", a_), RecordTableEnumerator.b("䤻䰽⸿硁㝃╅⁇⽉⅋⽍⍏网㥓㽕㭗⡙㍛ⵝཟѡၣ䭥୧թū呭Ὧᑱታή᭷ό䙻ᅽ", a_)))
					{
						num = 1;
						continue;
					}
					goto IL_2ED;
				case 14:
				{
					OColor ocolor = this.ᜈ(A_0.Value);
					A_1.Fill.BackColor = A_1.FillColor;
					A_1.Fill.ForeColor = ocolor.ᜁ(A_1.Workbook);
					A_1.Fill.FillType = ShapeFillType.Pattern;
					string a_2;
					A_1.Fill.Pattern = this.ᜂ(a_2);
					num = 12;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 5;
					continue;
				}
				num = 6;
				continue;
				IL_79:
				num = 9;
				continue;
				IL_145:
				num = 13;
			}
			IL_74:
			throw new ArgumentNullException(RecordTableEnumerator.b("主嬽ℿ♁⅃㑅", a_));
			IL_13A:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䠻嬽㠿㙁ك⥅ぇ", a_));
			IL_28B:
			goto IL_2ED;
			IL_2EB:
			throw new ArgumentNullException(RecordTableEnumerator.b("主嬽ⰿ⍁ぃ⽅❇⑉汋ⵍ㽏㹑㡓㍕㭗⹙㕛ㅝ๟", a_));
			IL_2ED:
			if (true)
			{
			}
			return;
		}
		}
	}

	// Token: 0x06002727 RID: 10023 RVA: 0x00165288 File Offset: 0x00164288
	private void ᜀ(XmlReader A_0, TextBoxShapeBase A_1, RelationsCollection A_2)
	{
		int a_ = 1;
		switch (0)
		{
		default:
		{
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_B3;
				case 1:
				{
					if (A_2 == null)
					{
						num = 8;
						continue;
					}
					string name = RecordTableEnumerator.b("帶吸娺娼娾", a_);
					num = 3;
					continue;
				}
				case 2:
				{
					string name = A_0.Value;
					num = 9;
					continue;
				}
				case 3:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䌶倸伺儼娾", a_), RecordTableEnumerator.b("䈶䬸唺ܼ䰾≀⭂⁄⩆⡈㡊恌≎㡐げ❔㡖⩘㑚㭜⭞䱠b੤੦卨Ѫ୬८ᡰၲၴ䵶ᙸᵺ᭼ᙾ", a_)))
					{
						num = 2;
						continue;
					}
					goto IL_B8;
				case 4:
				{
					sprវ sprវ = A_1.ParentWorkbook.DataHolder;
					string value = A_0.Value;
					sprᦨ sprᦨ = A_2[value];
					string text = A_2.ItemPath;
					int length = text.LastIndexOf('/');
					text = text.Substring(0, length);
					length = text.LastIndexOf('/');
					text = text.Substring(0, length);
					text = sprវ.ᜀ(text, sprᦨ.ᜂ());
					Image im = sprវ.ᜋ(text);
					string name;
					A_1.Fill.CustomPicture(im, name);
					num = 6;
					continue;
				}
				case 5:
					if (A_1 == null)
					{
						num = 0;
						continue;
					}
					num = 1;
					continue;
				case 6:
					goto IL_203;
				case 8:
					goto IL_22D;
				case 9:
					goto IL_B8;
				case 10:
					goto IL_6D;
				case 11:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䔶尸场吼嬾", a_), RecordTableEnumerator.b("䈶䬸唺ܼ䰾≀⭂⁄⩆⡈㡊恌≎㡐げ❔㡖⩘㑚㭜⭞䱠b੤੦卨Ѫ୬८ᡰၲၴ䵶ᙸᵺ᭼ᙾ", a_)))
					{
						num = 4;
						continue;
					}
					return;
				}
				if (true)
				{
				}
				if (A_0 == null)
				{
					num = 10;
					continue;
				}
				num = 5;
				continue;
				IL_B8:
				num = 11;
			}
			IL_6D:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_203:
				return;
			default:
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("䔶尸娺夼娾㍀", a_));
			}
			IL_B3:
			throw new ArgumentNullException(RecordTableEnumerator.b("䌶尸䌺䤼紾⹀㭂", a_));
			IL_22D:
			throw new ArgumentNullException(RecordTableEnumerator.b("䔶尸场尼䬾⡀ⱂ⭄杆⩈⑊⅌⍎㑐げ⅔㹖㙘㕚", a_));
		}
		}
	}

	// Token: 0x06002728 RID: 10024 RVA: 0x001654DC File Offset: 0x001644DC
	private void ᜂ(XmlReader A_0, TextBoxShapeBase A_1)
	{
		int a_ = 9;
		int num = 28;
		for (;;)
		{
			IL_16:
			int num2;
			int num3;
			switch (num)
			{
			case 0:
				A_1.Fill.TransparencyFrom = this.ᜇ(A_0.Value);
				num = 16;
				continue;
			case 1:
				num2 = A_0.ReadContentAsInt();
				num = 29;
				continue;
			case 2:
				num = 8;
				continue;
			case 3:
				return;
			case 4:
				if (num3 != -135)
				{
					num = 2;
					continue;
				}
				goto IL_2A8;
			case 5:
				if (this.ᜀ())
				{
					num = 18;
					continue;
				}
				goto IL_3E8;
			case 6:
				if (A_1 == null)
				{
					num = 23;
					continue;
				}
				num2 = 0;
				num = 17;
				continue;
			case 7:
				goto IL_AD;
			case 8:
				if (num3 != -90)
				{
					num = 3;
					continue;
				}
				goto IL_2B5;
			case 9:
				if (A_0.NodeType != XmlNodeType.EndElement)
				{
					num = 31;
					continue;
				}
				goto IL_145;
			case 10:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("夾⹀⁂い㑆", a_)))
				{
					num = 13;
					continue;
				}
				goto IL_1DF;
			case 11:
				A_1.Fill.TransparencyTo = this.ᜇ(A_0.Value);
				num = 14;
				continue;
			case 12:
				num = 21;
				continue;
			case 13:
				A_1.Fill.GradientVariant = this.ᜄ(A_0.Value);
				num = 25;
				continue;
			case 14:
				goto IL_323;
			case 15:
				if (num3 <= -90)
				{
					num = 27;
					continue;
				}
				num = 20;
				continue;
			case 16:
				goto IL_C0;
			case 17:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("倾ㅀ≂♄⹆㵈㉊", a_)))
				{
					num = 0;
					continue;
				}
				goto IL_C0;
			case 18:
				A_0.Read();
				num = 9;
				continue;
			case 19:
				if (A_0.NodeType == XmlNodeType.Whitespace)
				{
					num = 26;
					continue;
				}
				goto IL_1D2;
			case 20:
				if (num3 != -45)
				{
					num = 12;
					continue;
				}
				num = 5;
				continue;
			case 21:
				if (num3 != 0)
				{
					num = 30;
					continue;
				}
				goto IL_B2;
			case 22:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("帾⽀⑂⥄≆", a_)))
				{
					num = 1;
					continue;
				}
				goto IL_2C2;
			case 23:
				goto IL_373;
			case 24:
				while (A_0.MoveToAttribute(RecordTableEnumerator.b("倾筀ⱂ㕄♆⩈≊㥌㙎捐", a_)))
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
						num = 11;
						goto IL_16;
					}
				}
				goto IL_323;
			case 25:
				goto IL_1DF;
			case 26:
				goto IL_39B;
			case 27:
				num = 4;
				continue;
			case 29:
				goto IL_2C2;
			case 30:
				return;
			case 31:
				num = 19;
				continue;
			}
			if (A_0 == null)
			{
				num = 7;
				continue;
			}
			num = 6;
			continue;
			IL_C0:
			num = 24;
			continue;
			IL_1DF:
			num = 22;
			continue;
			IL_2C2:
			num3 = num2;
			if (true)
			{
			}
			num = 15;
			continue;
			IL_323:
			num = 10;
		}
		IL_AD:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴾⑀≂⅄≆㭈", a_));
		IL_B2:
		A_1.Fill.GradientStyle = GradientStyleType.Horizontal;
		return;
		IL_145:
		A_1.Fill.GradientStyle = GradientStyleType.From_Center;
		return;
		IL_1D2:
		A_1.Fill.GradientStyle = GradientStyleType.From_Corner;
		return;
		IL_2A8:
		A_1.Fill.GradientStyle = GradientStyleType.Diagonl_Up;
		return;
		IL_2B5:
		A_1.Fill.GradientStyle = GradientStyleType.Vertical;
		return;
		IL_373:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬾⑀㭂ㅄՆ♈㍊", a_));
		IL_39B:
		goto IL_145;
		IL_3E8:
		A_1.Fill.GradientStyle = GradientStyleType.Diagonl_Down;
	}

	// Token: 0x06002729 RID: 10025 RVA: 0x001658E0 File Offset: 0x001648E0
	private void ᜁ(XmlReader A_0, TextBoxShapeBase A_1, RelationsCollection A_2, string A_3)
	{
		int a_ = 18;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					num = 9;
					continue;
				}
				num = 2;
				continue;
			case 1:
				goto IL_1AA;
			case 2:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⹇⍉⁋≍⑏⭑⑓㍕", a_)))
				{
					num = 1;
					continue;
				}
				num = 11;
				continue;
			case 3:
				A_1.Line.DashStyle = this.ᜁ(A_0.Value);
				num = 8;
				continue;
			case 4:
				goto IL_54;
			case 5:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⑇⍉≋⭍⍏♑ⵓ㩕㵗", a_)))
				{
					num = 7;
					continue;
				}
				goto IL_1C3;
			case 7:
				goto IL_E2;
			case 8:
				goto IL_B3;
			case 9:
				goto IL_AE;
			case 10:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_E2;
				default:
					goto IL_16B;
				}
				break;
			case 11:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("ⱇ⭉㽋♍⍏♑ⵓ㩕㵗", a_)))
				{
					num = 3;
					continue;
				}
				goto IL_B3;
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			num = 0;
			continue;
			IL_B3:
			num = 5;
			continue;
			IL_E2:
			A_1.Line.Style = this.ᜀ(A_0.Value);
			num = 10;
		}
		IL_54:
		throw new ArgumentNullException(RecordTableEnumerator.b("㩇⽉ⵋ⩍㕏⁑", a_));
		IL_AE:
		throw new ArgumentNullException(RecordTableEnumerator.b("㱇⽉㑋㩍቏㵑ⱓ", a_));
		IL_16B:
		if (false)
		{
		}
		if (true)
		{
		}
		goto IL_1C3;
		IL_1AA:
		A_1.Line.HasPattern = true;
		this.ᜀ(A_0, A_1, A_2, A_3);
		return;
		IL_1C3:
		A_0.MoveToElement();
		A_0.Skip();
	}

	// Token: 0x0600272A RID: 10026 RVA: 0x00165AC0 File Offset: 0x00164AC0
	private void ᜀ(XmlReader A_0, TextBoxShapeBase A_1, RelationsCollection A_2, string A_3)
	{
		int a_ = 10;
		switch (0)
		{
		default:
		{
			int num = 10;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_F5;
					default:
						goto IL_13D;
					}
					break;
				case 1:
					goto IL_B1;
				case 2:
					goto IL_1D1;
				case 3:
					goto IL_F5;
				case 4:
					goto IL_1F1;
				case 5:
					if (A_2 == null)
					{
						num = 4;
						continue;
					}
					num = 7;
					continue;
				case 6:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㈿❁⡃⽅ⱇ", a_), RecordTableEnumerator.b("㔿ぁ⩃籅㭇⥉⑋⭍㵏㍑❓筕㕗㍙㽛ⱝཟᅡୣeᱧ䝩ཫŭᵯ䡱᭳ၵṷ፹ύ᭽멿", a_)))
					{
						num = 3;
						continue;
					}
					return;
				case 7:
					if (A_3 == null)
					{
						num = 0;
						continue;
					}
					num = 6;
					continue;
				case 8:
					if (A_1 == null)
					{
						num = 1;
						continue;
					}
					num = 5;
					continue;
				case 9:
					goto IL_69;
				case 10:
					if (true)
					{
					}
					break;
				}
				if (A_0 == null)
				{
					num = 9;
					continue;
				}
				num = 8;
				continue;
				IL_F5:
				sprវ sprវ = A_1.ParentWorkbook.DataHolder;
				string value = A_0.Value;
				sprᦨ sprᦨ = A_2[value];
				string text = A_2.ItemPath;
				int length = text.LastIndexOf('/');
				text = text.Substring(0, length);
				length = text.LastIndexOf('/');
				text = text.Substring(0, length);
				text = sprវ.ᜀ(text, sprᦨ.ᜂ());
				sprវ.ᜋ(text);
				this.ᜀ((long)sprវ.ᜀ(sprᦨ, A_3, false).Length);
				num = 2;
			}
			IL_69:
			throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁╃≅ⵇ㡉", a_));
			IL_B1:
			throw new ArgumentNullException(RecordTableEnumerator.b("㐿❁㱃㉅ੇ╉㑋", a_));
			IL_13D:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁㝃⥅㵇㡉⽋⭍灏≑㕓≕し", a_));
			IL_1D1:
			return;
			IL_1F1:
			throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁⡃❅㱇⍉⍋⁍灏ㅑ㭓㩕㑗㽙㽛⩝य़ൡ੣", a_));
		}
		}
	}

	// Token: 0x0600272B RID: 10027 RVA: 0x00165CD8 File Offset: 0x00164CD8
	private void ᜁ(XmlReader A_0, TextBoxShapeBase A_1)
	{
		int a_ = 3;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_8B;
			case 1:
				if (A_1 == null)
				{
					if (true)
					{
					}
					num = 0;
					continue;
				}
				goto IL_A1;
			case 3:
				goto IL_5A;
			}
			if (A_0 == null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				if (false)
				{
				}
				num = 3;
			}
			else
			{
				num = 1;
			}
		}
		IL_5A:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬸帺尼嬾⑀ㅂ", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴸帺䔼䬾̀ⱂ㵄", a_));
		IL_A1:
		string value = A_0.Value;
		Dictionary<string, string> a_2 = base.SplitStyle(value);
		this.ᜀ(A_1, a_2);
	}

	// Token: 0x0600272C RID: 10028 RVA: 0x00165DA0 File Offset: 0x00164DA0
	protected virtual void ᜀ(TextBoxShapeBase A_0, Dictionary<string, string> A_1)
	{
		int a_ = 8;
		int num = 3;
		for (;;)
		{
			string a;
			switch (num)
			{
			case 0:
				num = 1;
				continue;
			case 1:
				if (a == RecordTableEnumerator.b("嘽⤿♁⁃⍅♇", a_))
				{
					goto IL_9A;
				}
				goto IL_A4;
			case 2:
				goto IL_7A;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_9A;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 4:
				A_0.Visible = false;
				num = 2;
				continue;
			}
			if (A_1.TryGetValue(RecordTableEnumerator.b("䠽⤿ㅁⵃ⑅ⅇ♉╋㩍⥏", a_), out a))
			{
				num = 0;
				continue;
			}
			break;
			IL_9A:
			num = 4;
		}
		IL_7A:
		IL_A4:
		if (true)
		{
		}
	}

	// Token: 0x0600272D RID: 10029 RVA: 0x00165E64 File Offset: 0x00164E64
	private void ᜁ(TextBoxShapeBase A_0, Dictionary<string, string> A_1)
	{
		int a_ = 9;
		int num = 12;
		for (;;)
		{
			TextRotationType textRotation;
			switch (num)
			{
			case 0:
				goto IL_13E;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_71;
				default:
				{
					if (false)
					{
					}
					string text;
					if (text == RecordTableEnumerator.b("䬾⹀㍂桄㍆♈晊⽌⁎═❒㩔㩖", a_))
					{
						num = 3;
						continue;
					}
					num = 8;
					continue;
				}
				}
				break;
			case 2:
			{
				string a;
				if (a == RecordTableEnumerator.b("䤾⑀ㅂㅄ⹆⩈⩊⅌", a_))
				{
					num = 10;
					continue;
				}
				return;
			}
			case 3:
				textRotation = TextRotationType.TopToBottom;
				num = 7;
				continue;
			case 4:
				num = 2;
				continue;
			case 5:
				textRotation = TextRotationType.CounterClockwise;
				num = 14;
				continue;
			case 6:
				goto IL_10A;
			case 7:
				goto IL_13E;
			case 8:
			{
				string text;
				if (text == RecordTableEnumerator.b("崾⹀㝂ㅄ⡆⑈晊㥌⁎籐❒㩔❖", a_))
				{
					num = 5;
					continue;
				}
				textRotation = TextRotationType.Clockwise;
				num = 0;
				continue;
			}
			case 9:
				goto IL_71;
			case 10:
				goto IL_A1;
			case 11:
			{
				string text;
				if (text == null)
				{
					num = 4;
					continue;
				}
				goto IL_A1;
			}
			case 13:
				return;
			case 14:
				goto IL_13E;
			case 15:
			{
				if (A_1 == null)
				{
					num = 6;
					continue;
				}
				if (true)
				{
				}
				string text;
				A_1.TryGetValue(RecordTableEnumerator.b("刾㉀ⱂ桄⭆⡈㉊≌㩎═繒㍔㭖㙘ⱚ灜㹞ൠᝢ", a_), out text);
				string a;
				A_1.TryGetValue(RecordTableEnumerator.b("匾⁀㩂⩄㉆㵈晊⭌⍎㹐⑒", a_), out a);
				num = 11;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 9;
				continue;
			}
			num = 15;
			continue;
			IL_A1:
			num = 1;
			continue;
			IL_13E:
			A_0.TextRotation = textRotation;
			num = 13;
		}
		IL_71:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬾⑀㭂ㅄՆ♈㍊", a_));
		IL_10A:
		throw new ArgumentNullException(RecordTableEnumerator.b("嬾⡀⁂ㅄᝆ㭈⑊㵌⩎⍐❒㱔㉖⩘", a_));
	}

	// Token: 0x0600272E RID: 10030 RVA: 0x00166074 File Offset: 0x00165074
	private ShapeFillType ᜊ(string A_0)
	{
		int a_ = 1;
		Dictionary<string, ShapeFillType> dictionary;
		for (;;)
		{
			IL_4B:
			dictionary = new Dictionary<string, ShapeFillType>();
			dictionary.Add(RecordTableEnumerator.b("倶䬸娺夼嘾⑀ⵂㅄ", a_), ShapeFillType.Gradient);
			dictionary.Add(RecordTableEnumerator.b("倶䬸娺夼嘾⑀ⵂㅄᕆ⡈⽊⑌⹎㵐", a_), ShapeFillType.Gradient);
			dictionary.Add(RecordTableEnumerator.b("䜶堸伺䤼娾㍀ⵂ", a_), ShapeFillType.Pattern);
			dictionary.Add(RecordTableEnumerator.b("儶䬸娺值娾", a_), ShapeFillType.Picture);
			dictionary.Add(RecordTableEnumerator.b("䌶倸场堼", a_), ShapeFillType.Texture);
			dictionary.Add(RecordTableEnumerator.b("䐶嘸场吼嬾", a_), ShapeFillType.SolidColor);
			for (;;)
			{
				IL_CF:
				int num = 1;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_CF;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						switch (num)
						{
						case 0:
							this.ᜀ(true);
							num = 2;
							continue;
						case 1:
							if (A_0.Equals(RecordTableEnumerator.b("倶䬸娺夼嘾⑀ⵂㅄᕆ⡈⽊⑌⹎㵐", a_)))
							{
								num = 0;
								continue;
							}
							goto IL_111;
						case 2:
							goto IL_10F;
						}
						goto IL_4B;
					}
				}
			}
		}
		IL_10F:
		IL_111:
		return dictionary[A_0];
	}

	// Token: 0x0600272F RID: 10031 RVA: 0x0016619C File Offset: 0x0016519C
	private byte ᜉ(string A_0)
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return 3;
			case 1:
				return 1;
			case 2:
				if (true)
				{
				}
				if (A_0.Contains('#'.ToString()))
				{
					num = 0;
					continue;
				}
				return 2;
			}
			if (A_0.Contains('['.ToString()))
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				if (false)
				{
				}
				num = 1;
			}
			else
			{
				num = 2;
			}
		}
		return 1;
	}

	// Token: 0x06002730 RID: 10032 RVA: 0x00166240 File Offset: 0x00165240
	private OColor ᜈ(string A_0)
	{
		for (;;)
		{
			IL_14:
			byte b = this.ᜉ(A_0);
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					goto IL_C2;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_14;
					}
					if (false)
					{
					}
					switch (b)
					{
					case 1:
						goto IL_82;
					case 2:
						goto IL_CC;
					case 3:
						goto IL_68;
					default:
						num = 0;
						continue;
					}
					break;
				}
				break;
			}
		}
		IL_68:
		return new OColor(ColorType.RGB, int.Parse(this.ᜀ(A_0, false), NumberStyles.HexNumber, null));
		IL_82:
		return new OColor(ColorType.Known, Convert.ToInt32(A_0.Split(new char[]
		{
			'['
		})[1].Split(new char[]
		{
			']'
		})[0]));
		IL_C2:
		if (true)
		{
		}
		return new OColor(ColorType.RGB, 1);
		IL_CC:
		return new OColor(spr\u1D39.ᜀ(A_0));
	}

	// Token: 0x06002731 RID: 10033 RVA: 0x0016632C File Offset: 0x0016532C
	private double ᜇ(string A_0)
	{
		int a_ = 0;
		while (!A_0.EndsWith(RecordTableEnumerator.b("倵", a_)))
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
				return Convert.ToDouble(A_0);
			}
		}
		if (true)
		{
		}
		A_0 = this.ᜀ(A_0, true);
		return Convert.ToDouble(A_0) / 65536.0;
	}

	// Token: 0x06002732 RID: 10034 RVA: 0x001663AC File Offset: 0x001653AC
	private string ᜀ(string A_0, bool A_1)
	{
		for (;;)
		{
			if (true)
			{
			}
			if (!A_1)
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_36;
			}
		}
		return A_0.Remove(0, 1);
		IL_36:
		if (false)
		{
		}
		return A_0.Remove(A_0.Length - 1);
	}

	// Token: 0x06002733 RID: 10035 RVA: 0x00166404 File Offset: 0x00165404
	private GradientColorType ᜀ(XmlReader A_0, out string A_1)
	{
		int a_ = 10;
		int num = 0;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_EF;
			default:
				if (false)
				{
				}
				switch (num)
				{
				case 1:
					return GradientColorType.OneColor;
				case 2:
					if (A_1.StartsWith(RecordTableEnumerator.b("☿⭁⡃⩅", a_)))
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					return GradientColorType.TwoColor;
				case 3:
					goto IL_6B;
				case 4:
					A_1 = A_0.Value;
					num = 2;
					continue;
				case 5:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("⌿ⵁ⡃⥅㩇硉", a_)))
					{
						num = 4;
						continue;
					}
					goto IL_EF;
				}
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⌿ⵁ⡃⥅㩇㥉", a_)))
				{
					num = 3;
				}
				else
				{
					num = 5;
				}
				break;
			}
		}
		IL_6B:
		A_1 = A_0.Value;
		return GradientColorType.Preset;
		IL_EF:
		A_1 = RecordTableEnumerator.b("☿⭁⡃⩅桇⹉ⵋ㱍㭏㝑㩓繕桗獙", a_);
		return GradientColorType.OneColor;
	}

	// Token: 0x06002734 RID: 10036 RVA: 0x00166514 File Offset: 0x00165514
	private double ᜆ(string A_0)
	{
		int a_ = 12;
		int num = 2;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
			{
				double num2;
				return num2;
			}
			default:
				if (false)
				{
				}
				switch (num)
				{
				case 0:
				{
					double num2;
					return num2;
				}
				case 1:
				{
					if (A_0.Contains(RecordTableEnumerator.b("⑁ⵃ⩅⑇橉⁋❍㝏㩑⁓㍕㙗", a_)))
					{
						num = 4;
						continue;
					}
					double num2 = (num2 - 0.5) / 255.0;
					num = 5;
					continue;
				}
				case 3:
					goto IL_60;
				case 4:
				{
					double num2;
					num2 /= 255.0;
					num = 0;
					continue;
				}
				case 5:
				{
					double num2;
					return num2;
				}
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
					double num2 = XmlConvert.ToDouble(A_0.Split(new char[]
					{
						'('
					})[1].Split(new char[]
					{
						')'
					})[0]);
					num = 1;
				}
				break;
			}
		}
		IL_60:
		throw new ArgumentNullException(RecordTableEnumerator.b("♁⅃ⅅ㩇⽉⥋", a_));
	}

	// Token: 0x06002735 RID: 10037 RVA: 0x00166640 File Offset: 0x00165640
	private GradientPresetType ᜅ(string A_0)
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			GradientPresetType[] array;
			int num;
			for (;;)
			{
				IL_54:
				array = new GradientPresetType[]
				{
					GradientPresetType.GradEarlySunset,
					GradientPresetType.GradLateSunset,
					GradientPresetType.GradNightfall,
					GradientPresetType.GradDaybreak,
					GradientPresetType.GradHorizon,
					GradientPresetType.GradDesert,
					GradientPresetType.GradOcean,
					GradientPresetType.GradCalmWater,
					GradientPresetType.GradFire,
					GradientPresetType.GradFog,
					GradientPresetType.GradMoss,
					GradientPresetType.GradPeacock,
					GradientPresetType.GradWheat,
					GradientPresetType.GradParchment,
					GradientPresetType.GradMahogany,
					GradientPresetType.GradRainbow,
					GradientPresetType.GradRainbow2,
					GradientPresetType.GradGold,
					GradientPresetType.GradGold2,
					GradientPresetType.GradBrass,
					GradientPresetType.GradChrome,
					GradientPresetType.GradChrome2,
					GradientPresetType.GradSilver,
					GradientPresetType.GradSapphire
				};
				ResourceManager resourceManager = new ResourceManager(RecordTableEnumerator.b("ᙄ㝆⁈㥊⡌慎ॐὒٔ祖མᙚᅜᡞ፠ɢŤ๦౨ժᥬ", a_), typeof(spr\u2316).Assembly);
				num = 0;
				int num2 = array.Length;
				int num3 = 2;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1A9;
					default:
						if (false)
						{
						}
						switch (num3)
						{
						case 0:
							if (num >= num2)
							{
								num3 = 4;
								continue;
							}
							num3 = 5;
							continue;
						case 1:
							goto IL_189;
						case 2:
							goto IL_18B;
						case 3:
							goto IL_18B;
						case 4:
							goto IL_1A7;
						case 5:
							if (resourceManager.GetString(array[num].ToString()).Equals(A_0))
							{
								num3 = 1;
								continue;
							}
							num++;
							if (true)
							{
							}
							num3 = 3;
							continue;
						}
						goto IL_54;
						IL_18B:
						num3 = 0;
						break;
					}
				}
			}
			IL_189:
			return array[num];
			IL_1A7:
			IL_1A9:
			throw new IndexOutOfRangeException(RecordTableEnumerator.b("ᕄ㕆ⱈ㡊⡌㭎≐", a_));
		}
		}
	}

	// Token: 0x06002736 RID: 10038 RVA: 0x0016680C File Offset: 0x0016580C
	private ExcelPatternType ᜀ(long A_0)
	{
		int a_ = 4;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		throw new NotImplementedException(RecordTableEnumerator.b("䨹崻䨽㐿❁㙃⡅", a_));
	}

	// Token: 0x06002737 RID: 10039 RVA: 0x00166864 File Offset: 0x00165864
	private GradientVariantsType ᜄ(string A_0)
	{
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return GradientVariantsType.ShadingVariants2;
			default:
			{
				if (true)
				{
				}
				if (false)
				{
				}
				int num = Convert.ToInt32(this.ᜀ(A_0, true));
				int num2 = num;
				int num3 = 0;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						if (num2 != -50)
						{
							num3 = 5;
							continue;
						}
						return GradientVariantsType.ShadingVariants4;
					case 1:
						if (num2 == 100)
						{
							num3 = 2;
							continue;
						}
						return GradientVariantsType.ShadingVariants2;
					case 2:
						return GradientVariantsType.ShadingVariants1;
					case 3:
						num3 = 1;
						continue;
					case 4:
						if (num2 != 50)
						{
							num3 = 3;
							continue;
						}
						return GradientVariantsType.ShadingVariants3;
					case 5:
						num3 = 4;
						continue;
					}
					break;
				}
				break;
			}
			}
		}
		return GradientVariantsType.ShadingVariants3;
	}

	// Token: 0x06002738 RID: 10040 RVA: 0x00166924 File Offset: 0x00165924
	private GradientTextureType ᜃ(string A_0)
	{
		GradientTextureType result;
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
			A_0 = A_0.Replace(' ', '_');
			try
			{
				result = (GradientTextureType)Enum.Parse(typeof(GradientTextureType), A_0, true);
			}
			catch (Exception)
			{
				result = GradientTextureType.UserDefined;
			}
			break;
		}
		return result;
	}

	// Token: 0x06002739 RID: 10041 RVA: 0x0016699C File Offset: 0x0016599C
	private GradientPatternType ᜂ(string A_0)
	{
		int a_ = 11;
		if (true)
		{
		}
		GradientPatternType result;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			A_0 = RecordTableEnumerator.b("ㅀ≂ㅄᡆ", a_) + A_0.Replace(' ', '_');
			try
			{
				result = (GradientPatternType)Enum.Parse(typeof(GradientPatternType), A_0, true);
			}
			catch (Exception)
			{
				result = GradientPatternType.Pat10Percent;
			}
			break;
		}
		return result;
	}

	// Token: 0x0600273A RID: 10042 RVA: 0x00166A30 File Offset: 0x00165A30
	private ShapeDashLineStyleType ᜁ(string A_0)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_90;
			case 1:
				if (!char.IsDigit(A_0[0]))
				{
					num = 0;
					continue;
				}
				return ShapeDashLineStyleType.DottedRound;
			case 3:
				goto IL_6F;
			case 4:
				spr\u2316.ᜁ();
				num = 3;
				continue;
			}
			IL_2E:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_2E;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				if (spr\u2316.ᜏ == null)
				{
					num = 4;
					continue;
				}
				break;
			}
			IL_6F:
			num = 1;
		}
		IL_90:
		return spr\u2316.ᜏ[A_0];
	}

	// Token: 0x0600273B RID: 10043 RVA: 0x00166AE4 File Offset: 0x00165AE4
	private ShapeLineStyleType ᜀ(string A_0)
	{
		for (;;)
		{
			IL_00:
			int num = 0;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_00;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 1:
						spr\u2316.ᜂ();
						num = 2;
						continue;
					case 2:
						goto IL_60;
					}
					if (spr\u2316.ᜎ != null)
					{
						goto IL_6A;
					}
					num = 1;
					break;
				}
			}
		}
		IL_60:
		if (true)
		{
		}
		IL_6A:
		return spr\u2316.ᜎ[A_0];
	}

	// Token: 0x04001353 RID: 4947
	internal const int ᜀ = 0;

	// Token: 0x04001354 RID: 4948
	internal const int ᜁ = -90;

	// Token: 0x04001355 RID: 4949
	internal const int ᜂ = -135;

	// Token: 0x04001356 RID: 4950
	internal const int ᜃ = -45;

	// Token: 0x04001357 RID: 4951
	private const byte ᜄ = 1;

	// Token: 0x04001358 RID: 4952
	private const byte ᜅ = 2;

	// Token: 0x04001359 RID: 4953
	private const byte ᜆ = 3;

	// Token: 0x0400135A RID: 4954
	internal const int ᜇ = 100;

	// Token: 0x0400135B RID: 4955
	internal const int ᜈ = 50;

	// Token: 0x0400135C RID: 4956
	internal const int ᜉ = -50;

	// Token: 0x0400135D RID: 4957
	internal const string ᜊ = "pat_";

	// Token: 0x0400135E RID: 4958
	private const string ᜋ = "LINE_";

	// Token: 0x0400135F RID: 4959
	private const string ᜌ = "Patt";

	// Token: 0x04001360 RID: 4960
	private const string \u170D = "solid";

	// Token: 0x04001361 RID: 4961
	public static Dictionary<string, ShapeLineStyleType> ᜎ;

	// Token: 0x04001362 RID: 4962
	public static Dictionary<string, ShapeDashLineStyleType> ᜏ;

	// Token: 0x04001363 RID: 4963
	private bool ᜐ;
}
