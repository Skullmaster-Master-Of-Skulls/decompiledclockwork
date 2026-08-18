using System;
using System.Drawing;
using System.Xml;
using Spire.Xls;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlReaders.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlSerialization;

// Token: 0x0200055B RID: 1371
internal class spr\u1AA7 : ShapeParser
{
	// Token: 0x060052BD RID: 21181 RVA: 0x0033B134 File Offset: 0x0033A134
	public virtual XlsShape ᜀ(XmlReader A_0, ShapeCollectionBase A_1)
	{
		int a_ = 1;
		int num = 2;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				goto IL_58;
			case 1:
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				goto IL_A1;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6E;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 3:
				goto IL_81;
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			IL_6E:
			num = 1;
		}
		IL_58:
		throw new ArgumentNullException(RecordTableEnumerator.b("䔶尸娺夼娾㍀", a_));
		IL_81:
		throw new ArgumentNullException(RecordTableEnumerator.b("䜶堸䤺堼儾㕀", a_));
		IL_A1:
		A_0.Skip();
		return new ExcelPicture((spr\u2158)A_1.AppImplementation, A_1);
	}

	// Token: 0x060052BE RID: 21182 RVA: 0x0033B1FC File Offset: 0x0033A1FC
	public virtual bool ᜀ(XmlReader A_0, XlsShape A_1, RelationsCollection A_2, string A_3)
	{
		int a_ = 4;
		switch (0)
		{
		default:
		{
			int num = 13;
			for (;;)
			{
				XlsBitmapShape xlsBitmapShape;
				switch (num)
				{
				case 0:
				{
					int num2;
					xlsBitmapShape.ShapeId = num2;
					num = 23;
					continue;
				}
				case 1:
					num = 6;
					continue;
				case 2:
					if (A_1 == null)
					{
						num = 9;
						continue;
					}
					num = 19;
					continue;
				case 3:
				{
					int num2;
					if (num2 == -1)
					{
						num = 34;
						continue;
					}
					goto IL_373;
				}
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_209;
					default:
						goto IL_246;
					}
					break;
				case 5:
					goto IL_326;
				case 6:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 21;
						continue;
					}
					goto IL_326;
				}
				case 7:
				{
					string text = A_0.Value;
					int num2 = this.ᜀ(text);
					num = 17;
					continue;
				}
				case 8:
				{
					int num2;
					if (num2 != -1)
					{
						num = 0;
						continue;
					}
					goto IL_20B;
				}
				case 9:
					goto IL_461;
				case 10:
					num = 5;
					continue;
				case 11:
					goto IL_373;
				case 12:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("礹倻圽┿ⱁぃɅ⥇㹉ⵋ", a_)))
					{
						num = 10;
						continue;
					}
					this.ᜀ(A_0, xlsBitmapShape);
					num = 15;
					continue;
				}
				case 14:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("匹堻", a_)))
					{
						num = 18;
						continue;
					}
					goto IL_373;
				case 15:
					goto IL_209;
				case 16:
					goto IL_2F9;
				case 17:
					goto IL_40D;
				case 18:
				{
					string text = A_0.Value;
					num = 3;
					continue;
				}
				case 19:
				{
					if (A_0.LocalName != RecordTableEnumerator.b("䤹吻弽〿❁", a_))
					{
						num = 16;
						continue;
					}
					string text = null;
					int num2 = -1;
					num = 29;
					continue;
				}
				case 20:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 4;
						continue;
					}
					num = 28;
					continue;
				case 21:
					num = 27;
					continue;
				case 22:
					num = 12;
					continue;
				case 23:
					goto IL_20B;
				case 24:
					goto IL_106;
				case 25:
					goto IL_20B;
				case 26:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䤹䠻䰽⼿⥁⅃≅", a_)))
					{
						num = 35;
						continue;
					}
					xlsBitmapShape.HasBorder = false;
					if (true)
					{
					}
					num = 31;
					continue;
				case 27:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("匹儻弽✿❁⁃❅㱇⭉", a_)))
					{
						num = 22;
						continue;
					}
					string text;
					this.ᜀ(A_0, text, xlsBitmapShape, A_2, A_3);
					num = 32;
					continue;
				}
				case 28:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 1;
						continue;
					}
					A_0.Read();
					num = 30;
					continue;
				case 29:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䤹䰻圽␿", a_), RecordTableEnumerator.b("伹主倽稿ㅁ❃⹅ⵇ❉ⵋ㵍絏㽑㵓㕕⩗㕙⽛ㅝٟᙡ䥣եݧݩ噫ŭᙯᑱᵳᕵᵷ䁹፻᡽", a_)))
					{
						num = 7;
						continue;
					}
					goto IL_40D;
				case 30:
					goto IL_20B;
				case 31:
					goto IL_106;
				case 32:
					goto IL_20B;
				case 33:
					goto IL_CE;
				case 34:
				{
					string text;
					int num2 = this.ᜀ(text);
					num = 11;
					continue;
				}
				case 35:
					xlsBitmapShape.HasBorder = true;
					num = 24;
					continue;
				}
				if (A_0 == null)
				{
					num = 33;
					continue;
				}
				num = 2;
				continue;
				IL_106:
				A_0.Read();
				num = 8;
				continue;
				IL_20B:
				num = 20;
				continue;
				IL_209:
				goto IL_20B;
				IL_326:
				A_0.Skip();
				num = 25;
				continue;
				IL_373:
				xlsBitmapShape = (XlsBitmapShape)A_1.Clone(A_1.Parent, null, null, false);
				xlsBitmapShape.ᜀ(A_1);
				num = 26;
				continue;
				IL_40D:
				num = 14;
			}
			IL_CE:
			throw new ArgumentNullException(RecordTableEnumerator.b("䠹夻弽␿❁㙃", a_));
			IL_246:
			if (false)
			{
			}
			A_0.Read();
			return true;
			IL_2F9:
			throw new XmlException(RecordTableEnumerator.b("漹刻嬽㠿㉁⅃╅㱇⽉⡋湍⡏㽑㡓癕ⱗ㭙㭛灝", a_));
			IL_461:
			throw new ArgumentNullException(RecordTableEnumerator.b("帹夻堽ℿ㝁⡃㉅ᭇ≉ⵋ㹍㕏", a_));
		}
		}
	}

	// Token: 0x060052BF RID: 21183 RVA: 0x0033B6A0 File Offset: 0x0033A6A0
	private int ᜀ(string A_0)
	{
		int a_ = 2;
		int result;
		for (;;)
		{
			IL_25:
			int num = A_0.IndexOf(RecordTableEnumerator.b("朷䤹", a_));
			result = -1;
			for (;;)
			{
				IL_3C:
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (num >= 0)
						{
							num2 = 2;
							continue;
						}
						return result;
					case 1:
						return result;
					case 2:
					{
						string s = A_0.Substring(num + 2);
						num2 = 3;
						continue;
					}
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3C;
						default:
						{
							if (false)
							{
							}
							if (true)
							{
							}
							string s;
							if (int.TryParse(s, out num))
							{
								num2 = 4;
								continue;
							}
							return result;
						}
						}
						break;
					case 4:
						result = num;
						num2 = 1;
						continue;
					}
					goto IL_25;
				}
			}
		}
		return result;
	}

	// Token: 0x060052C0 RID: 21184 RVA: 0x0033B764 File Offset: 0x0033A764
	private void ᜀ(XmlReader A_0, XlsBitmapShape A_1)
	{
		int a_ = 1;
		int num = 18;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_203;
			case 1:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("搶倸䄺堼栾⡀㝂ⵄцⱈ❊⅌㱎", a_)))
				{
					num = 16;
					continue;
				}
				A_1.IsSizeWithCell = spr\u2316.ᜀ(A_0, true);
				num = 27;
				continue;
			}
			case 2:
				goto IL_226;
			case 3:
				num = 13;
				continue;
			case 4:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 2;
					continue;
				}
				num = 14;
				continue;
			case 5:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("琶堸嘺堼䴾⁀", a_)))
				{
					num = 3;
					continue;
				}
				A_1.IsCamera = true;
				A_0.Read();
				num = 28;
				continue;
			}
			case 6:
				num = 26;
				continue;
			case 7:
				goto IL_203;
			case 8:
				goto IL_203;
			case 9:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("瘶圸堺唼倾㍀", a_)))
				{
					num = 23;
					continue;
				}
				base.ParseAnchor(A_0, A_1);
				num = 19;
				continue;
			}
			case 10:
				num = 25;
				continue;
			case 11:
				goto IL_B6;
			case 12:
				num = 1;
				continue;
			case 13:
				goto IL_1F0;
			case 14:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 10;
					continue;
				}
				A_0.Skip();
				num = 22;
				continue;
			case 15:
				num = 5;
				continue;
			case 16:
				goto IL_2B9;
			case 17:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("猶紸縺", a_)))
				{
					num = 15;
					continue;
				}
				A_1.IsDDE = true;
				A_0.Read();
				num = 24;
				continue;
			}
			case 19:
				goto IL_203;
			case 20:
				goto IL_2B4;
			case 21:
				if (A_1 == null)
				{
					num = 20;
					continue;
				}
				A_0.Read();
				A_1.IsMoveWithCell = true;
				A_1.IsSizeWithCell = true;
				num = 0;
				continue;
			case 22:
				goto IL_203;
			case 23:
				num = 17;
				continue;
			case 24:
				goto IL_203;
			case 25:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 6;
					continue;
				}
				goto IL_1F0;
			}
			case 26:
			{
				string localName;
				if (localName == RecordTableEnumerator.b("稶嘸䴺堼栾⡀㝂ⵄцⱈ❊⅌㱎", a_))
				{
					A_1.IsMoveWithCell = spr\u2316.ᜀ(A_0, true);
					if (true)
					{
					}
					num = 7;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2B9;
				default:
					if (false)
					{
					}
					num = 12;
					continue;
				}
				break;
			}
			case 27:
				goto IL_203;
			case 28:
				goto IL_203;
			}
			if (A_0.LocalName != RecordTableEnumerator.b("琶唸刺堼儾㕀݂⑄㍆⡈", a_))
			{
				num = 11;
				continue;
			}
			num = 21;
			continue;
			IL_1F0:
			A_0.Skip();
			num = 8;
			continue;
			IL_203:
			num = 4;
			continue;
			IL_2B9:
			num = 9;
		}
		IL_B6:
		throw new XmlException(RecordTableEnumerator.b("戶圸帺䔼伾⑀⁂ㅄ≆ⵈ歊㕌≎㵐獒⅔㡖㉘㹚㍜", a_));
		IL_226:
		A_0.Read();
		return;
		IL_2B4:
		throw new ArgumentNullException(RecordTableEnumerator.b("䐶儸娺䴼娾", a_));
	}

	// Token: 0x060052C1 RID: 21185 RVA: 0x0033BB08 File Offset: 0x0033AB08
	private void ᜀ(XmlReader A_0, string A_1, XlsBitmapShape A_2, RelationsCollection A_3, string A_4)
	{
		int a_ = 13;
		XlsWorksheetBase worksheet;
		Image image;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_242:
			worksheet.HeaderFooterShapes.SetPicture(A_1, image, -1, false);
			num = 20;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				num = 0;
				break;
			}
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_218;
			case 2:
				if (A_1 != null)
				{
					num = 13;
					continue;
				}
				goto IL_2BA;
			case 3:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("ㅂ⁄⭆⁈⽊", a_), RecordTableEnumerator.b("㙂㝄⥆獈㡊⹌❎㑐㹒㑔⑖瑘㙚㑜㱞፠ౢᙤࡦཨὪ䁬౮ṰṲ佴ᡶὸᵺᑼ᱾릂", a_)))
				{
					num = 14;
					continue;
				}
				goto IL_264;
			case 4:
				if (A_0.LocalName != RecordTableEnumerator.b("⩂⡄♆⹈⹊⥌⹎═㉒", a_))
				{
					num = 19;
					continue;
				}
				num = 3;
				continue;
			case 5:
				goto IL_BE;
			case 6:
				goto IL_2A4;
			case 7:
				goto IL_2EC;
			case 8:
				goto IL_177;
			case 9:
				goto IL_240;
			case 10:
				if (true)
				{
				}
				if (A_4.Length == 0)
				{
					num = 6;
					continue;
				}
				num = 17;
				continue;
			case 11:
				num = 10;
				continue;
			case 12:
				goto IL_EE;
			case 13:
				num = 15;
				continue;
			case 14:
			{
				string value = A_0.Value;
				sprᦨ sprᦨ = A_3[value];
				num = 18;
				continue;
			}
			case 15:
				if (A_1.Length == 0)
				{
					num = 9;
					continue;
				}
				num = 23;
				continue;
			case 16:
				if (A_2 == null)
				{
					num = 7;
					continue;
				}
				num = 2;
				continue;
			case 17:
				if (A_3 == null)
				{
					num = 1;
					continue;
				}
				num = 4;
				continue;
			case 18:
			{
				sprᦨ sprᦨ;
				if (sprᦨ == null)
				{
					num = 12;
					continue;
				}
				worksheet = A_2.Worksheet;
				string a_2 = sprវ.ᜀ(A_4, sprᦨ.ᜂ());
				image = worksheet.DataHolder.ᜋ().ᜋ(a_2);
				num = 21;
				continue;
			}
			case 19:
				goto IL_384;
			case 20:
				goto IL_25F;
			case 21:
				if (A_2.ParentShapes is XlsHeaderFooterShapeCollection)
				{
					num = 8;
					continue;
				}
				A_2.Picture = image;
				worksheet.InnerShapes.Add(A_2);
				num = 22;
				continue;
			case 22:
				goto IL_1F4;
			case 23:
				if (A_4 != null)
				{
					num = 11;
					continue;
				}
				goto IL_2F1;
			}
			if (A_0 == null)
			{
				num = 5;
			}
			else
			{
				num = 16;
			}
		}
		IL_BE:
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄♆ⵈ⹊㽌", a_));
		IL_EE:
		throw new XmlException(RecordTableEnumerator.b("B⑄⥆❈⑊㥌潎㝐㩒㭔㍖祘⥚㡜⹞ᑠ੢ᝤɦ൨䭪Ὤ੮ᵰቲŴṶᙸᕺ卼", a_));
		IL_177:
		goto IL_242;
		IL_1F4:
		goto IL_386;
		IL_218:
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄⭆⡈㽊⑌⁎㽐⁒", a_));
		IL_240:
		goto IL_2BA;
		IL_25F:
		goto IL_386;
		IL_264:
		throw new XmlException(RecordTableEnumerator.b("ᑂ㝄⡆❈ⱊ浌㝎㱐㽒畔ㅖ㙘⥚ぜ㹞ᕠ䵢", a_));
		IL_2A4:
		goto IL_2F1;
		IL_2BA:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("あⵄ♆㥈⹊͌⹎㱐㙒", a_));
		IL_2EC:
		throw new ArgumentNullException(RecordTableEnumerator.b("あⵄ♆㥈⹊", a_));
		IL_2F1:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㍂⑄㕆ⱈ╊㥌َ═㙒㡔ݖ㡘⽚㕜", a_));
		IL_384:
		throw new XmlException(RecordTableEnumerator.b("ᙂ⭄≆ㅈ㭊⡌ⱎ═㙒ㅔ睖⅘㙚ㅜ罞ᕠɢɤ䥦", a_));
		IL_386:
		A_0.Skip();
	}
}
