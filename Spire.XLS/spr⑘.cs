using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.XmlReaders.Shapes;

// Token: 0x0200043A RID: 1082
internal class spr\u2458
{
	// Token: 0x0600412C RID: 16684 RVA: 0x00247778 File Offset: 0x00246778
	public void ᜂ(XmlWriter A_0, IListObject A_1)
	{
		int a_ = 17;
		int num = 5;
		for (;;)
		{
			spr\u1C4A spr_u1C4A;
			switch (num)
			{
			case 0:
				goto IL_53;
			case 1:
				if (!spr_u1C4A.ᜀ())
				{
					num = 2;
					continue;
				}
				goto IL_58;
			case 2:
				A_0.WriteAttributeString(RecordTableEnumerator.b("⽆ⱈ⩊⥌⩎⍐Œ㩔⁖ᩘ㑚⡜ㅞᕠ", a_), RecordTableEnumerator.b("睆", a_));
				num = 9;
				continue;
			case 3:
				if (A_1 == null)
				{
					num = 4;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("㍆⡈⥊⅌⩎", a_), RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒♔㑖ㅘ㹚ぜ㹞በ䵢੤ᝦ౨ժᕬɮᵰᕲᩴնᑸ᩺ॼ౾꾀Ꚉﶌﶎﺚ철쾢誤閦馨鮪鮬肮\udcb0튲\udcb4\ud9b6", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("⹆ⵈ", a_), A_1.Index.ToString());
				A_0.WriteAttributeString(RecordTableEnumerator.b("⥆⡈♊⡌", a_), A_1.Name);
				A_0.WriteAttributeString(RecordTableEnumerator.b("⍆⁈㡊㵌⍎ぐ⩒᭔㙖㑘㹚", a_), A_1.DisplayName);
				A_0.WriteAttributeString(RecordTableEnumerator.b("㕆ⱈⵊ", a_), A_1.Location.RangeAddressLocal);
				spr_u1C4A = (A_1 as spr\u1C4A);
				num = 1;
				continue;
			case 4:
				goto IL_10D;
			case 6:
				goto IL_17C;
			case 7:
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("㍆♈㽊ⱌ⍎≐Œ㩔⁖੘㍚㉜⡞འ", a_), false, true);
				num = 6;
				continue;
			case 8:
				goto IL_138;
			case 9:
				goto IL_58;
			case 10:
				if (!(A_1 as spr\u1C4A).ᜂ())
				{
					num = 7;
					continue;
				}
				spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("㍆♈㽊ⱌ⍎≐Œ㩔⁖ᩘ㑚⡜ㅞᕠ", a_), A_1.TotalsRowCount, 0);
				num = 8;
				continue;
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			num = 3;
			continue;
			IL_58:
			spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("⹆❈㡊⡌㵎═Œ㩔⁖੘㍚㑜㥞ᕠ", a_), spr_u1C4A.ᜌ(), 0);
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_259;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				num = 10;
				break;
			}
		}
		IL_53:
		throw new ArgumentNullException(RecordTableEnumerator.b("う㭈≊㥌⩎⍐", a_));
		IL_10D:
		throw new ArgumentNullException(RecordTableEnumerator.b("㍆⡈⥊⅌⩎", a_));
		IL_138:
		IL_17C:
		IL_259:
		this.ᜁ(A_0, A_1);
		this.ᜀ(A_0, A_1.Columns);
		this.ᜀ(A_0, A_1);
		A_0.WriteEndElement();
	}

	// Token: 0x0600412D RID: 16685 RVA: 0x00247A04 File Offset: 0x00246A04
	private void ᜁ(XmlWriter A_0, IListObject A_1)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			int num = 8;
			Stream ᜌ;
			IXLSRange ixlsrange;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					if (!A_1.DisplayHeaderRow)
					{
						num = 5;
						continue;
					}
					spr\u1C4A spr_u1C4A = (spr\u1C4A)A_1;
					ᜌ = spr_u1C4A.ᜌ;
					num = 3;
					continue;
				}
				case 1:
				{
					int totalsRowCount;
					if (totalsRowCount > 0)
					{
						num = 9;
						continue;
					}
					goto IL_1A0;
				}
				case 2:
					goto IL_162;
				case 3:
				{
					if (ᜌ != null)
					{
						num = 6;
						continue;
					}
					int totalsRowCount = A_1.TotalsRowCount;
					ixlsrange = A_1.Location;
					num = 1;
					continue;
				}
				case 4:
					goto IL_61;
				case 5:
					return;
				case 6:
					goto IL_105;
				case 7:
					goto IL_94;
				case 9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1A0;
					default:
					{
						if (false)
						{
						}
						int totalsRowCount;
						ixlsrange = ixlsrange.Worksheet[ixlsrange.Row, ixlsrange.Column, ixlsrange.LastRow - totalsRowCount, ixlsrange.LastColumn];
						num = 2;
						continue;
					}
					}
					break;
				case 10:
					if (true)
					{
					}
					if (A_1 == null)
					{
						num = 7;
						continue;
					}
					num = 0;
					continue;
				}
				if (A_0 == null)
				{
					num = 4;
				}
				else
				{
					num = 10;
				}
			}
			IL_61:
			throw new ArgumentNullException(RecordTableEnumerator.b("㕁㙃⽅㱇⽉㹋", a_));
			IL_94:
			throw new ArgumentNullException(RecordTableEnumerator.b("㙁╃⑅⑇⽉", a_));
			IL_105:
			ᜌ.Position = 0L;
			ShapeParser.WriteNodeFromStream(A_0, ᜌ);
			return;
			IL_162:
			IL_1A0:
			A_0.WriteStartElement(RecordTableEnumerator.b("⍁ㅃ㉅❇౉╋≍⑏㝑♓", a_));
			A_0.WriteAttributeString(RecordTableEnumerator.b("ぁ⅃⁅", a_), ixlsrange.RangeAddressLocal);
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x0600412E RID: 16686 RVA: 0x00247BE8 File Offset: 0x00246BE8
	private void ᜀ(XmlWriter A_0, IListObject A_1)
	{
		int a_ = 4;
		switch (0)
		{
		default:
			for (;;)
			{
				spr\u1C4A spr_u1C4A = (spr\u1C4A)A_1;
				TableBuiltInStyles builtInTableStyle = A_1.BuiltInTableStyle;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (spr_u1C4A.ᜅ() == null)
						{
							num = 10;
							continue;
						}
						goto IL_A5;
					case 1:
						A_0.WriteAttributeString(RecordTableEnumerator.b("吹崻匽┿", a_), builtInTableStyle.ToString());
						num = 6;
						continue;
					case 2:
						goto IL_12B;
					case 3:
						A_0.WriteAttributeString(RecordTableEnumerator.b("䤹吻儽㜿แ╃㕅㱇ॉ⍋≍╏㽑㩓", a_), (spr_u1C4A.ᜁ() ? 1 : 0).ToString());
						num = 9;
						continue;
					case 4:
						goto IL_A5;
					case 5:
						goto IL_91;
					case 6:
						goto IL_12B;
					case 7:
						return;
					case 8:
						A_0.WriteAttributeString(RecordTableEnumerator.b("䤹吻儽㜿Ł⭃⩅㵇❉≋ᵍ⑏⁑㵓♕㵗⥙", a_), (spr_u1C4A.ᜃ() ? 1 : 0).ToString());
						A_0.WriteEndElement();
						num = 7;
						continue;
					case 9:
						A_0.WriteAttributeString(RecordTableEnumerator.b("䤹吻儽㜿၁⭃ㅅᭇ㹉㹋❍⁏㝑❓", a_), (spr_u1C4A.ᜇ() ? 1 : 0).ToString());
						if (true)
						{
						}
						num = 8;
						continue;
					case 10:
						num = 5;
						continue;
					case 11:
						if (builtInTableStyle != TableBuiltInStyles.None)
						{
							num = 1;
							continue;
						}
						A_0.WriteAttributeString(RecordTableEnumerator.b("吹崻匽┿", a_), spr_u1C4A.ᜅ());
						num = 2;
						continue;
					case 12:
						A_0.WriteAttributeString(RecordTableEnumerator.b("䤹吻儽㜿сⵃ㑅㭇㹉ཋ⅍㱏❑㥓㡕", a_), (spr_u1C4A.ᜏ() ? 1 : 0).ToString());
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_91;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					}
					break;
					IL_91:
					if (builtInTableStyle != TableBuiltInStyles.None)
					{
						num = 4;
						continue;
					}
					return;
					IL_A5:
					A_0.WriteStartElement(RecordTableEnumerator.b("丹崻尽ⰿ❁ᝃ㉅ㅇ♉⥋ݍ㹏㑑㭓", a_));
					num = 11;
					continue;
					IL_12B:
					num = 12;
				}
			}
			return;
		}
	}

	// Token: 0x0600412F RID: 16687 RVA: 0x00247E3C File Offset: 0x00246E3C
	private void ᜀ(XmlWriter A_0, IList<IListObjectColumn> A_1)
	{
		int a_ = 3;
		int num = 4;
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
					goto IL_CC;
				case 1:
					goto IL_E7;
				case 2:
				{
					if (A_1 == null)
					{
						num = 1;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("䴸娺弼匾⑀B⩄⭆㱈♊⍌㱎", a_));
					int num2 = 0;
					int count = A_1.Count;
					num = 7;
					continue;
				}
				case 3:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						goto IL_C1;
					}
					IListObjectColumn a_2 = A_1[num2];
					this.ᜀ(A_0, a_2);
					num2++;
					num = 5;
					continue;
				}
				case 5:
					goto IL_B2;
				case 6:
					goto IL_6A;
				case 7:
					goto IL_B2;
				}
				if (A_0 == null)
				{
					num = 6;
					continue;
				}
				num = 2;
				continue;
				IL_B2:
				num = 3;
				continue;
			}
			IL_C1:
			num = 0;
		}
		IL_6A:
		throw new ArgumentNullException(RecordTableEnumerator.b("丸䤺吼䬾⑀ㅂ", a_));
		IL_CC:
		A_0.WriteEndElement();
		return;
		IL_E7:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("娸吺儼䨾ⱀⵂ㙄", a_));
	}

	// Token: 0x06004130 RID: 16688 RVA: 0x00247F6C File Offset: 0x00246F6C
	private void ᜀ(XmlWriter A_0, IListObjectColumn A_1)
	{
		int a_ = 18;
		int num = 2;
		for (;;)
		{
			string calculatedFormula;
			switch (num)
			{
			case 0:
				goto IL_1A1;
			case 1:
				if (calculatedFormula != null)
				{
					num = 7;
					continue;
				}
				goto IL_1D7;
			case 3:
			{
				string text = A_1.TotalsCalculation.ToString();
				text = spr\u1B7A.ᜄ(text);
				A_0.WriteAttributeString(RecordTableEnumerator.b("㱇╉㡋⽍㱏⅑ٓ㥕⽗᱙⥛そ͟ᙡൣ॥٧", a_), text);
				goto IL_8B;
			}
			case 4:
				if (A_1 == null)
				{
					num = 8;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("㱇⭉⹋≍㕏ᅑ㭓㩕ⵗ㝙㉛", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("ⅇ⹉", a_), A_1.Id.ToString());
				A_0.WriteAttributeString(RecordTableEnumerator.b("♇⭉⅋⭍", a_), A_1.Name);
				spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("㱇╉㡋⽍㱏⅑ٓ㥕⽗ᙙ㵛㱝՟๡", a_), A_1.TotalsRowLabel, null);
				num = 5;
				continue;
			case 5:
				if (A_1.TotalsCalculation != ExcelTotalsCalculation.None)
				{
					num = 3;
					continue;
				}
				goto IL_1A1;
			case 6:
				goto IL_18B;
			case 7:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_8B;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					A_0.WriteElementString(RecordTableEnumerator.b("⭇⭉⁋ⵍ╏㹑㕓≕㵗㹙Ὓㅝ౟ᝡॣࡥ⹧թṫͭկṱᕳ", a_), calculatedFormula);
					num = 6;
					continue;
				}
				break;
			case 8:
				goto IL_145;
			case 9:
				goto IL_4F;
			}
			if (A_0 == null)
			{
				num = 9;
				continue;
			}
			num = 4;
			continue;
			IL_8B:
			num = 0;
			continue;
			IL_1A1:
			calculatedFormula = A_1.CalculatedFormula;
			num = 1;
		}
		IL_4F:
		throw new ArgumentNullException(RecordTableEnumerator.b("㽇㡉╋㩍㕏⁑", a_));
		IL_145:
		throw new ArgumentNullException(RecordTableEnumerator.b("⭇╉⁋㭍㵏㱑", a_));
		IL_18B:
		IL_1D7:
		A_0.WriteEndElement();
	}
}
