using System;
using System.Collections.Generic;
using System.Xml;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.XmlReaders.Shapes;

// Token: 0x0200046C RID: 1132
internal class sprᮜ
{
	// Token: 0x06004575 RID: 17781 RVA: 0x002A5604 File Offset: 0x002A4604
	public void ᜀ(XmlReader A_0, IWorksheet A_1)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 15;
			for (;;)
			{
				IListObject listObject;
				IXLSRange range;
				spr\u1C4A spr_u1C4A;
				string name;
				switch (num)
				{
				case 0:
					goto IL_25F;
				case 1:
					goto IL_5A8;
				case 2:
				{
					(listObject as spr\u1C4A).ᜁ(XmlConvert.ToInt32(A_0.Value));
					XlsWorkbook xlsWorkbook = A_1.Workbook as XlsWorkbook;
					int val = xlsWorkbook.MaxTableIndex;
					xlsWorkbook.MaxTableIndex = Math.Max(val, xlsWorkbook.MaxTableIndex);
					num = 34;
					continue;
				}
				case 3:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 48;
						continue;
					}
					goto IL_60F;
				}
				case 4:
					goto IL_25F;
				case 5:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("⽅♇㥉⥋㱍⑏Q㭓⅕ୗ㉙㕛㡝ᑟ", a_)))
					{
						num = 41;
						continue;
					}
					goto IL_5A8;
				case 6:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("⽅ⱇ", a_)))
					{
						num = 2;
						continue;
					}
					goto IL_320;
				case 7:
					goto IL_25F;
				case 8:
					listObject.DisplayName = A_0.Value;
					num = 17;
					continue;
				case 9:
					goto IL_531;
				case 10:
					goto IL_25F;
				case 11:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㉅❇㹉ⵋ≍⍏Q㭓⅕ୗ㉙㍛⥝๟", a_)))
					{
						num = 37;
						continue;
					}
					num = 50;
					continue;
				case 12:
					goto IL_67A;
				case 13:
					range = A_1[A_0.Value];
					num = 35;
					continue;
				case 14:
					num = 12;
					continue;
				case 16:
					spr_u1C4A.ᜄ(true);
					num = 24;
					continue;
				case 17:
					goto IL_4BD;
				case 18:
					goto IL_25F;
				case 19:
					num = 29;
					continue;
				case 20:
					num = 3;
					continue;
				case 21:
					spr_u1C4A.ᜀ(XmlConvert.ToBoolean(A_0.Value));
					num = 52;
					continue;
				case 22:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 20;
						continue;
					}
					A_0.Skip();
					num = 4;
					continue;
				case 23:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("⡅⥇❉⥋", a_)))
					{
						num = 25;
						continue;
					}
					goto IL_159;
				case 24:
					goto IL_536;
				case 25:
					name = A_0.Value;
					num = 43;
					continue;
				case 26:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("❅㵇㹉⍋ࡍ㥏㹑⁓㍕⩗", a_)))
					{
						num = 44;
						continue;
					}
					this.ᜁ(A_0, listObject);
					num = 0;
					continue;
				}
				case 27:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㑅ⵇⱉ", a_)))
					{
						num = 13;
						continue;
					}
					goto IL_200;
				case 28:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("⹅ⵇ⭉⡋⭍≏Q㭓⅕᭗㕙⥛そᑟ", a_)))
					{
						num = 21;
						continue;
					}
					goto IL_291;
				case 29:
					goto IL_60F;
				case 30:
					A_0.Read();
					num = 10;
					continue;
				case 31:
					goto IL_25F;
				case 32:
					goto IL_112;
				case 33:
					if (!A_0.IsEmptyElement)
					{
						num = 30;
						continue;
					}
					goto IL_700;
				case 34:
					goto IL_320;
				case 35:
					goto IL_200;
				case 36:
					spr_u1C4A.ᜀ(A_0.MoveToAttribute(RecordTableEnumerator.b("㉅❇㹉ⵋ≍⍏Q㭓⅕᭗㕙⥛そᑟ", a_)) ? XmlConvert.ToInt32(A_0.Value) : 0);
					num = 11;
					continue;
				case 37:
					spr_u1C4A.ᜄ(XmlConvert.ToInt32(A_0.Value) != 0);
					spr_u1C4A.ᜀ(0);
					num = 47;
					continue;
				case 38:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("㉅⥇⡉⁋⭍ፏ㵑㡓⍕㕗㑙⽛", a_)))
					{
						num = 51;
						continue;
					}
					this.ᜀ(A_0, listObject.Columns);
					num = 7;
					continue;
				}
				case 39:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("㉅⥇⡉⁋⭍͏♑ⵓ㩕㵗ፙ㉛㡝ཟ", a_)))
					{
						num = 19;
						continue;
					}
					this.ᜀ(A_0, listObject);
					num = 18;
					continue;
				}
				case 40:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("≅ⅇ㥉㱋≍ㅏ⭑ᩓ㝕㕗㽙", a_)))
					{
						num = 8;
						continue;
					}
					goto IL_4BD;
				case 41:
				{
					string value = A_0.Value;
					spr_u1C4A.ᜂ(XmlConvert.ToInt32(value));
					num = 1;
					continue;
				}
				case 42:
					if (A_1 == null)
					{
						num = 14;
						continue;
					}
					num = 49;
					continue;
				case 43:
					goto IL_159;
				case 44:
					num = 38;
					continue;
				case 45:
					goto IL_28C;
				case 46:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 45;
						continue;
					}
					num = 22;
					continue;
				case 47:
					goto IL_536;
				case 48:
					num = 26;
					continue;
				case 49:
					if (A_0.LocalName != RecordTableEnumerator.b("㉅⥇⡉⁋⭍", a_))
					{
						num = 9;
						continue;
					}
					name = null;
					range = null;
					num = 23;
					continue;
				case 50:
					if (spr_u1C4A.ᜉ() != 0)
					{
						num = 16;
						continue;
					}
					goto IL_536;
				case 51:
					num = 39;
					continue;
				case 52:
					goto IL_291;
				}
				if (A_0 == null)
				{
					num = 32;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_20E;
				default:
					if (false)
					{
					}
					num = 42;
					continue;
				}
				IL_159:
				num = 27;
				continue;
				IL_20E:
				num = 6;
				continue;
				IL_200:
				listObject = A_1.ListObjects.Create(name, range);
				goto IL_20E;
				IL_25F:
				if (true)
				{
				}
				num = 46;
				continue;
				IL_291:
				num = 36;
				continue;
				IL_320:
				num = 40;
				continue;
				IL_4BD:
				spr_u1C4A = (listObject as spr\u1C4A);
				num = 28;
				continue;
				IL_536:
				num = 5;
				continue;
				IL_5A8:
				A_0.MoveToElement();
				num = 33;
				continue;
				IL_60F:
				A_0.Skip();
				num = 31;
			}
			IL_112:
			throw new ArgumentNullException(RecordTableEnumerator.b("㑅ⵇ⭉⡋⭍≏", a_));
			IL_28C:
			goto IL_700;
			IL_531:
			throw new XmlException();
			IL_67A:
			throw new ArgumentNullException(RecordTableEnumerator.b("㕅⁇⽉⥋㩍", a_));
			IL_700:
			A_0.Read();
			return;
		}
		}
	}

	// Token: 0x06004576 RID: 17782 RVA: 0x002A5D18 File Offset: 0x002A4D18
	private void ᜁ(XmlReader A_0, IListObject A_1)
	{
		int a_ = 10;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_E6;
			case 1:
				goto IL_49;
			case 2:
				if (A_0.LocalName != RecordTableEnumerator.b("ℿ㝁ぃ⥅็⍉⁋㩍㕏⁑", a_))
				{
					num = 4;
					continue;
				}
				goto IL_E8;
			case 3:
				if (A_1 == null)
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
					goto IL_B0;
				default:
					if (false)
					{
					}
					num = 2;
					continue;
				}
				break;
			case 4:
				goto IL_9A;
			}
			if (A_0 == null)
			{
				num = 1;
			}
			else
			{
				num = 3;
			}
		}
		IL_49:
		goto IL_B0;
		IL_9A:
		throw new XmlException();
		IL_B0:
		throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁╃≅ⵇ㡉", a_));
		IL_E6:
		throw new ArgumentNullException(RecordTableEnumerator.b("㐿⍁♃⩅ⵇ", a_));
		IL_E8:
		spr\u1C4A spr_u1C4A = (spr\u1C4A)A_1;
		spr_u1C4A.ᜌ = ShapeParser.ReadNodeAsStream(A_0);
	}

	// Token: 0x06004577 RID: 17783 RVA: 0x002A5E20 File Offset: 0x002A4E20
	private void ᜀ(XmlReader A_0, IListObject A_1)
	{
		int a_ = 9;
		int num = 7;
		for (;;)
		{
			spr\u1C4A spr_u1C4A;
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_134;
				default:
					if (false)
					{
					}
					A_1.BuiltInTableStyle = (TableBuiltInStyles)Enum.Parse(typeof(TableBuiltInStyles), A_0.Value, false);
					num = 12;
					continue;
				}
				break;
			case 1:
				if (A_0.LocalName != RecordTableEnumerator.b("䬾⁀⅂⥄≆ᩈ㽊㑌⍎㑐ᩒ㭔ㅖ㙘", a_))
				{
					num = 10;
					continue;
				}
				spr_u1C4A = (spr\u1C4A)A_1;
				num = 6;
				continue;
			case 2:
				goto IL_8C;
			case 3:
				spr_u1C4A.ᜆ(XmlConvert.ToBoolean(A_0.Value));
				num = 22;
				continue;
			case 4:
				spr_u1C4A.ᜃ(XmlConvert.ToBoolean(A_0.Value));
				num = 5;
				continue;
			case 5:
				goto IL_246;
			case 6:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("儾⁀⹂⁄", a_)))
				{
					num = 16;
					continue;
				}
				goto IL_CA;
			case 8:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䰾⥀ⱂ㉄ᕆ♈㱊Ṍ㭎⍐㩒╔㉖⩘", a_)))
				{
					num = 14;
					continue;
				}
				goto IL_18E;
			case 9:
				goto IL_18E;
			case 10:
				goto IL_12F;
			case 11:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䰾⥀ⱂ㉄୆⡈㡊㥌౎㹐㽒⁔㩖㝘", a_)))
				{
					num = 3;
					continue;
				}
				goto IL_1F6;
			case 12:
				goto IL_303;
			case 13:
				goto IL_CA;
			case 14:
				goto IL_134;
			case 15:
			{
				bool flag;
				if (flag)
				{
					if (true)
					{
					}
					num = 0;
					continue;
				}
				goto IL_303;
			}
			case 16:
			{
				bool flag = Enum.IsDefined(typeof(TableBuiltInStyles), A_0.Value);
				num = 15;
				continue;
			}
			case 17:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䰾⥀ⱂ㉄ņ⁈㥊㹌㭎ቐ㱒㥔≖㑘㕚", a_)))
				{
					num = 18;
					continue;
				}
				goto IL_8C;
			case 18:
				spr_u1C4A.ᜁ(XmlConvert.ToBoolean(A_0.Value));
				num = 2;
				continue;
			case 19:
				if (A_1 == null)
				{
					num = 23;
					continue;
				}
				num = 1;
				continue;
			case 20:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䰾⥀ⱂ㉄ц♈❊㡌≎㽐R⅔╖じ⭚㡜ⱞ", a_)))
				{
					num = 4;
					continue;
				}
				goto IL_340;
			case 21:
				goto IL_87;
			case 22:
				goto IL_1F6;
			case 23:
				goto IL_16B;
			}
			if (A_0 == null)
			{
				num = 21;
				continue;
			}
			num = 19;
			continue;
			IL_8C:
			num = 11;
			continue;
			IL_CA:
			num = 17;
			continue;
			IL_134:
			spr_u1C4A.ᜅ(XmlConvert.ToBoolean(A_0.Value));
			num = 9;
			continue;
			IL_18E:
			num = 20;
			continue;
			IL_1F6:
			num = 8;
			continue;
			IL_303:
			spr_u1C4A.ᜀ(A_0.Value);
			num = 13;
		}
		IL_87:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴾⑀≂⅄≆㭈", a_));
		IL_12F:
		throw new XmlException();
		IL_16B:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬾⁀⅂⥄≆", a_));
		IL_246:
		IL_340:
		A_0.MoveToElement();
		A_0.Skip();
	}

	// Token: 0x06004578 RID: 17784 RVA: 0x002A617C File Offset: 0x002A517C
	private void ᜀ(XmlReader A_0, IList<IListObjectColumn> A_1)
	{
		int a_ = 16;
		int num;
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
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_125;
			case 2:
				if (A_1 == null)
				{
					num = 5;
					continue;
				}
				if (true)
				{
				}
				num = 4;
				continue;
			case 3:
			{
				A_0.Read();
				int num2 = 0;
				int count = A_1.Count;
				num = 7;
				continue;
			}
			case 4:
				if (A_0.LocalName != RecordTableEnumerator.b("㉅⥇⡉⁋⭍ፏ㵑㡓⍕㕗㑙⽛", a_))
				{
					num = 6;
					continue;
				}
				num = 11;
				continue;
			case 5:
				goto IL_A8;
			case 6:
				goto IL_160;
			case 7:
				goto IL_101;
			case 8:
			{
				int num2;
				int count;
				if (num2 >= count)
				{
					num = 1;
					continue;
				}
				this.ᜀ(A_0, A_1, num2);
				num2++;
				num = 9;
				continue;
			}
			case 9:
				goto IL_101;
			case 10:
				goto IL_70;
			case 11:
				if (!A_0.IsEmptyElement)
				{
					num = 3;
					continue;
				}
				goto IL_179;
			}
			if (A_0 == null)
			{
				num = 10;
				continue;
			}
			num = 2;
			continue;
			IL_101:
			num = 8;
		}
		IL_70:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑅ⵇ⭉⡋⭍≏", a_));
		IL_A8:
		throw new ArgumentNullException(RecordTableEnumerator.b("╅❇♉㥋⍍㹏⅑", a_));
		IL_125:
		goto IL_179;
		IL_160:
		throw new XmlException();
		IL_179:
		A_0.Read();
	}

	// Token: 0x06004579 RID: 17785 RVA: 0x002A630C File Offset: 0x002A530C
	private void ᜀ(XmlReader A_0, IList<IListObjectColumn> A_1, int A_2)
	{
		int a_ = 6;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_298;
			case 1:
				goto IL_228;
			case 2:
				goto IL_228;
			case 3:
				goto IL_228;
			case 5:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("刻弽ⴿ❁", a_)))
				{
					if (true)
					{
					}
					num = 25;
					continue;
				}
				goto IL_250;
			case 6:
				goto IL_250;
			case 7:
				goto IL_39C;
			case 8:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 29;
					continue;
				}
				A_0.Skip();
				num = 3;
				continue;
			case 9:
				goto IL_A5;
			case 10:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 17;
					continue;
				}
				num = 8;
				continue;
			case 11:
				goto IL_228;
			case 12:
			{
				IListObjectColumn listObjectColumn;
				listObjectColumn.CalculatedFormula = A_0.ReadElementContentAsString();
				num = 1;
				continue;
			}
			case 13:
			{
				IListObjectColumn listObjectColumn;
				listObjectColumn.TotalsCalculation = (ExcelTotalsCalculation)Enum.Parse(typeof(ExcelTotalsCalculation), A_0.Value, true);
				num = 7;
				continue;
			}
			case 14:
				num = 20;
				continue;
			case 15:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("唻娽", a_)))
				{
					num = 28;
					continue;
				}
				goto IL_298;
			case 16:
				A_0.Read();
				num = 2;
				continue;
			case 17:
				goto IL_24B;
			case 18:
			{
				if (A_0.LocalName != RecordTableEnumerator.b("䠻弽∿⹁⅃Յ❇♉㥋⍍㹏", a_))
				{
					num = 26;
					continue;
				}
				IListObjectColumn listObjectColumn = null;
				goto IL_105;
			}
			case 19:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䠻儽㐿⍁⡃㕅ᩇ╉㭋ɍㅏけㅓ㩕", a_)))
				{
					num = 21;
					continue;
				}
				goto IL_195;
			case 20:
			{
				string localName;
				if (localName == RecordTableEnumerator.b("弻弽ⰿ⅁ㅃ⩅⥇㹉⥋⩍ፏ㵑㡓⍕㕗㑙ᩛㅝ቟ཡᅣ੥१", a_))
				{
					num = 12;
					continue;
				}
				goto IL_215;
			}
			case 21:
			{
				IListObjectColumn listObjectColumn;
				listObjectColumn.TotalsRowLabel = A_0.Value;
				num = 22;
				continue;
			}
			case 22:
				goto IL_195;
			case 23:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 14;
					continue;
				}
				goto IL_215;
			}
			case 24:
				if (!A_0.IsEmptyElement)
				{
					num = 16;
					continue;
				}
				goto IL_3C3;
			case 25:
			{
				IListObjectColumn listObjectColumn;
				listObjectColumn.Name = sprᝐ.ᜀ(A_0.Value);
				num = 6;
				continue;
			}
			case 26:
				goto IL_305;
			case 27:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䠻儽㐿⍁⡃㕅ᩇ╉㭋ࡍ╏㱑㝓≕ㅗ㕙㉛", a_)))
				{
					num = 13;
					continue;
				}
				goto IL_39C;
			case 28:
			{
				int a_2 = XmlConvert.ToInt32(A_0.Value);
				IListObjectColumn listObjectColumn = A_1[A_2];
				(listObjectColumn as sprΊ).ᜀ(a_2);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_105;
				default:
					if (false)
					{
					}
					num = 0;
					continue;
				}
				break;
			}
			case 29:
				num = 23;
				continue;
			}
			if (A_0 == null)
			{
				num = 9;
				continue;
			}
			num = 18;
			continue;
			IL_105:
			num = 15;
			continue;
			IL_195:
			num = 27;
			continue;
			IL_215:
			A_0.Skip();
			num = 11;
			continue;
			IL_228:
			num = 10;
			continue;
			IL_250:
			num = 19;
			continue;
			IL_298:
			num = 5;
			continue;
			IL_39C:
			A_0.MoveToElement();
			num = 24;
		}
		IL_A5:
		throw new ArgumentNullException(RecordTableEnumerator.b("主嬽ℿ♁⅃㑅", a_));
		IL_24B:
		goto IL_3C3;
		IL_305:
		throw new XmlException();
		IL_3C3:
		A_0.Read();
	}
}
