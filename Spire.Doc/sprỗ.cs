using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Spire.CompoundFile.Doc;

// Token: 0x020002FC RID: 764
internal abstract class sprỗ
{
	// Token: 0x060029AE RID: 10670 RVA: 0x002989A4 File Offset: 0x002979A4
	internal static XmlAttribute ᜀ(XmlNode A_0, string A_1, string A_2, string A_3)
	{
		switch (0)
		{
		default:
		{
			XmlAttribute xmlAttribute;
			for (;;)
			{
				string b = A_1;
				xmlAttribute = null;
				int num = 0;
				int count = A_0.ChildNodes.Count;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_140;
					case 1:
						goto IL_140;
					case 2:
						goto IL_C2;
					case 3:
						return xmlAttribute;
					case 4:
					{
						if (A_3 == null)
						{
							num2 = 14;
							continue;
						}
						XmlNode xmlNode;
						xmlAttribute = xmlNode.Attributes[A_2, A_3];
						num2 = 2;
						continue;
					}
					case 5:
					{
						XmlNode xmlNode;
						if (xmlNode.Attributes.Count > 0)
						{
							num2 = 10;
							continue;
						}
						goto IL_93;
					}
					case 6:
						if (xmlAttribute != null)
						{
							num2 = 9;
							continue;
						}
						goto IL_1AF;
					case 7:
						return xmlAttribute;
					case 8:
					{
						XmlNode xmlNode;
						if (xmlNode.Attributes == null)
						{
							goto IL_93;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_130;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num2 = 17;
							continue;
						}
						break;
					}
					case 9:
						return xmlAttribute;
					case 10:
						num2 = 21;
						continue;
					case 11:
					{
						if (num >= count)
						{
							num2 = 7;
							continue;
						}
						XmlNode xmlNode = A_0.ChildNodes[num];
						num2 = 20;
						continue;
					}
					case 12:
						goto IL_DF;
					case 13:
						if (xmlAttribute != null)
						{
							num2 = 3;
							continue;
						}
						goto IL_93;
					case 14:
					{
						XmlNode xmlNode;
						xmlAttribute = xmlNode.Attributes[A_2];
						num2 = 15;
						continue;
					}
					case 15:
						goto IL_C2;
					case 16:
					{
						XmlNode xmlNode;
						if (xmlNode.ChildNodes.Count > 0)
						{
							num2 = 19;
							continue;
						}
						goto IL_1AF;
					}
					case 17:
						num2 = 5;
						continue;
					case 18:
						num2 = 4;
						continue;
					case 19:
					{
						XmlNode xmlNode;
						xmlAttribute = sprỗ.ᜀ(xmlNode, A_1, A_2, A_3);
						num2 = 6;
						continue;
					}
					case 20:
						if (A_1 == null)
						{
							num2 = 22;
							continue;
						}
						goto IL_DF;
					case 21:
					{
						XmlNode xmlNode;
						if (xmlNode.LocalName == b)
						{
							num2 = 18;
							continue;
						}
						goto IL_93;
					}
					case 22:
					{
						XmlNode xmlNode;
						b = xmlNode.LocalName;
						goto IL_130;
					}
					}
					break;
					IL_93:
					num2 = 16;
					continue;
					IL_C2:
					num2 = 13;
					continue;
					IL_DF:
					num2 = 8;
					continue;
					IL_130:
					num2 = 12;
					continue;
					IL_140:
					num2 = 11;
					continue;
					IL_1AF:
					num++;
					num2 = 0;
				}
			}
			return xmlAttribute;
		}
		}
	}

	// Token: 0x060029AF RID: 10671 RVA: 0x00298C48 File Offset: 0x00297C48
	internal static XmlNode ᜀ(XmlNode A_0, string A_1)
	{
		XmlNode xmlNode;
		for (;;)
		{
			int num = 0;
			int count = A_0.ChildNodes.Count;
			int num2 = 9;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return xmlNode;
				case 1:
					if (xmlNode.ChildNodes.Count > 0)
					{
						num2 = 8;
						continue;
					}
					goto IL_EC;
				case 2:
					goto IL_98;
				case 3:
					if (xmlNode != null)
					{
						if (true)
						{
						}
						num2 = 0;
						continue;
					}
					goto IL_EC;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						if (xmlNode.LocalName == A_1)
						{
							num2 = 7;
							continue;
						}
						num2 = 1;
						continue;
					}
					break;
				case 5:
					if (num >= count)
					{
						num2 = 2;
						continue;
					}
					xmlNode = A_0.ChildNodes[num];
					num2 = 4;
					continue;
				case 6:
					goto IL_7E;
				case 7:
					return xmlNode;
				case 8:
					xmlNode = sprỗ.ᜀ(xmlNode, A_1);
					num2 = 3;
					continue;
				case 9:
					goto IL_7E;
				}
				break;
				IL_7E:
				num2 = 5;
				continue;
				IL_EC:
				num++;
				num2 = 6;
			}
		}
		return xmlNode;
		IL_98:
		return null;
	}

	// Token: 0x060029B0 RID: 10672 RVA: 0x00298D7C File Offset: 0x00297D7C
	internal static string ᜁ(XmlNode A_0)
	{
		int a_ = 0;
		XmlAttribute xmlAttribute;
		for (;;)
		{
			xmlAttribute = sprỗ.ᜀ(A_0, null, ClipboardData.b("ཥ౧", a_), ClipboardData.b("๥ᱧṩᱫ呭彯嵱ݳᕵၷόᅻώ겁ﲏﮓﮙ躟춡횣솥螧얩쪫좭\ud9af톱톳ힷ\ud9b9즻펽ꖿ곁냃難韛ﳋￏꃑ뇓뫕맗껙뗛뇝軟釡賣迥飧駩", a_));
			if (xmlAttribute != null)
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_59;
			}
		}
		return xmlAttribute.Value;
		IL_59:
		if (true)
		{
		}
		if (false)
		{
		}
		return null;
	}

	// Token: 0x060029B1 RID: 10673 RVA: 0x00298DF4 File Offset: 0x00297DF4
	internal static List<string> ᜀ(XmlNode A_0)
	{
		int a_ = 13;
		List<string> list;
		for (;;)
		{
			XmlTextReader xmlTextReader = new XmlTextReader(A_0.OuterXml, XmlNodeType.Element, null);
			xmlTextReader.Read();
			list = new List<string>();
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (xmlTextReader.EOF)
					{
						num = 6;
						continue;
					}
					goto IL_169;
				case 1:
					if (xmlTextReader.LocalName == ClipboardData.b("rŴնᙸၺ᡼", a_))
					{
						goto IL_154;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B2;
					default:
						if (false)
						{
						}
						num = 19;
						continue;
					}
					break;
				case 2:
					if (!(xmlTextReader.LocalName == ClipboardData.b("ၲᩴ᥶൸ॺቼ፾", a_)))
					{
						num = 8;
						continue;
					}
					goto IL_154;
				case 3:
					num = 5;
					continue;
				case 4:
					if (!(xmlTextReader.LocalName == ClipboardData.b("ၲᵴᙶ୸ེ", a_)))
					{
						num = 3;
						continue;
					}
					goto IL_154;
				case 5:
					if (!(xmlTextReader.LocalName == ClipboardData.b("ᩲᡴᙶṸṺ᥼Ṿ", a_)))
					{
						num = 7;
						continue;
					}
					goto IL_154;
				case 6:
					return list;
				case 7:
					num = 1;
					continue;
				case 8:
					num = 12;
					continue;
				case 9:
					goto IL_169;
				case 10:
					goto IL_154;
				case 11:
					if (true)
					{
					}
					if (!(xmlTextReader.LocalName == ClipboardData.b("Ųၴ᭶へὺ๼", a_)))
					{
						num = 16;
						continue;
					}
					goto IL_154;
				case 12:
					if (xmlTextReader.LocalName == ClipboardData.b("㱲㥴㉶㙸᥺᝼᩾", a_))
					{
						goto IL_B2;
					}
					goto IL_1DC;
				case 13:
					num = 11;
					continue;
				case 14:
					if (!(xmlTextReader.LocalName == ClipboardData.b("ᅲᥴṶॸ", a_)))
					{
						num = 13;
						continue;
					}
					goto IL_154;
				case 15:
					goto IL_1DC;
				case 16:
					num = 4;
					continue;
				case 17:
					num = 14;
					continue;
				case 18:
					if (!(xmlTextReader.LocalName == ClipboardData.b("ᕲᱴ᭶ᕸ", a_)))
					{
						num = 17;
						continue;
					}
					goto IL_154;
				case 19:
					num = 2;
					continue;
				}
				break;
				IL_B2:
				num = 10;
				continue;
				IL_154:
				list = sprỗ.ᜁ(xmlTextReader, list);
				num = 15;
				continue;
				IL_169:
				xmlTextReader.Read();
				num = 18;
				continue;
				IL_1DC:
				num = 0;
			}
		}
		return list;
	}

	// Token: 0x060029B2 RID: 10674 RVA: 0x002990B4 File Offset: 0x002980B4
	internal static List<string> ᜁ(XmlTextReader A_0, List<string> A_1)
	{
		int a_ = 8;
		switch (0)
		{
		default:
			for (;;)
			{
				string text = string.Empty;
				string text2 = string.Empty;
				int num = 19;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						string localName;
						int num2;
						if (spr᧓.ឤ.TryGetValue(localName, out num2))
						{
							num = 14;
							continue;
						}
						goto IL_2FD;
					}
					case 1:
						return A_1;
					case 2:
						goto IL_2FD;
					case 3:
						if (text2 != string.Empty)
						{
							num = 6;
							continue;
						}
						return A_1;
					case 4:
						goto IL_2FD;
					case 5:
						goto IL_122;
					case 6:
						A_1.Add(text2);
						num = 1;
						continue;
					case 7:
						goto IL_2FD;
					case 8:
					{
						int num2;
						switch (num2)
						{
						case 0:
						case 1:
						case 2:
						case 3:
						case 4:
						case 5:
							text = A_0.GetAttribute(ClipboardData.b("ݭᑯ", a_), ClipboardData.b("٭ѯٱѳ䱵坷啹ཻᵽﮇꒉﺍﲑﮕﲙ춟쎡킣향蚧얩\udeab즭龯\uddb1튳킵톷\ud9b9\ud9bb諾꾿ꇁ뇃ꯅ귇꓉룋꣙맛닝臟雡跣觥蛧駩蓫蟭胯臱", a_));
							text2 = A_0.GetAttribute(ClipboardData.b("٭ɯ᝱ታ", a_), ClipboardData.b("٭ѯٱѳ䱵坷啹ཻᵽﮇꒉﺍﲑﮕﲙ춟쎡킣향蚧얩\udeab즭龯\uddb1튳킵톷\ud9b9\ud9bb諾꾿ꇁ뇃ꯅ귇꓉룋꣙맛닝臟雡跣觥蛧駩蓫蟭胯臱", a_));
							num = 7;
							continue;
						case 6:
							text = A_0.GetAttribute(ClipboardData.b("୭ᵯၱᅳት", a_), ClipboardData.b("٭ѯٱѳ䱵坷啹ཻᵽﮇꒉﺍﲑﮕﲙ춟쎡킣향蚧얩\udeab즭龯\uddb1튳킵톷\ud9b9\ud9bb諾꾿ꇁ뇃ꯅ귇꓉룋꣙맛닝臟雡跣觥蛧駩蓫蟭胯臱", a_));
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_11D;
							default:
								if (false)
								{
								}
								num = 18;
								continue;
							}
							break;
						case 7:
							sprỗ.ᜀ(A_0, A_1);
							num = 2;
							continue;
						default:
							num = 22;
							continue;
						}
						break;
					}
					case 9:
						if (text != string.Empty)
						{
							num = 20;
							continue;
						}
						goto IL_122;
					case 10:
						num = 3;
						continue;
					case 11:
						if (text2 != null)
						{
							num = 10;
							continue;
						}
						return A_1;
					case 12:
						goto IL_F1;
					case 13:
						num = 15;
						continue;
					case 14:
						goto IL_11D;
					case 15:
						if (spr᧓.ឤ == null)
						{
							num = 21;
							continue;
						}
						goto IL_F1;
					case 16:
						num = 9;
						continue;
					case 17:
						if (text != null)
						{
							num = 16;
							continue;
						}
						goto IL_122;
					case 18:
						goto IL_2FD;
					case 19:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num = 13;
							continue;
						}
						goto IL_2FD;
					}
					case 20:
						A_1.Add(text);
						num = 5;
						continue;
					case 21:
						spr᧓.ឤ = new Dictionary<string, int>(8)
						{
							{
								ClipboardData.b("࡭᥯ṱᡳ", a_),
								0
							},
							{
								ClipboardData.b("൭ᡯ፱ٳɵ", a_),
								1
							},
							{
								ClipboardData.b("ݭᵯ፱፳፵ᱷ᭹ࡻώ", a_),
								2
							},
							{
								ClipboardData.b("ᵭѯq᭳ᵵᵷ", a_),
								3
							},
							{
								ClipboardData.b("൭Ὧᱱsѵ᝷ᙹ", a_),
								4
							},
							{
								ClipboardData.b("Ⅽ㱯㝱㭳ᑵቷόύ੽", a_),
								5
							},
							{
								ClipboardData.b("౭ᱯ᭱ѳ", a_),
								6
							},
							{
								ClipboardData.b("ᱭᕯṱ㵳ት୷", a_),
								7
							}
						};
						num = 12;
						continue;
					case 22:
						if (true)
						{
						}
						num = 4;
						continue;
					}
					break;
					IL_F1:
					num = 0;
					continue;
					IL_11D:
					num = 8;
					continue;
					IL_122:
					num = 11;
					continue;
					IL_2FD:
					num = 17;
				}
			}
			return A_1;
		}
	}

	// Token: 0x060029B3 RID: 10675 RVA: 0x0029947C File Offset: 0x0029847C
	internal static void ᜀ(XmlTextReader A_0, List<string> A_1)
	{
		int a_ = 6;
		for (;;)
		{
			string text = string.Empty;
			int num = 0;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_1D3;
				case 1:
					goto IL_135;
				case 2:
					goto IL_1D3;
				case 3:
					goto IL_1D3;
				case 4:
					num2 = 7;
					continue;
				case 5:
					goto IL_1D3;
				case 6:
					goto IL_135;
				case 7:
					if (text != string.Empty)
					{
						num2 = 15;
						continue;
					}
					goto IL_D8;
				case 8:
					return;
				case 9:
					if (text != null)
					{
						num2 = 4;
						continue;
					}
					goto IL_D8;
				case 10:
				{
					if (num >= 4)
					{
						num2 = 8;
						continue;
					}
					int num3 = num;
					num2 = 14;
					continue;
				}
				case 11:
					goto IL_1D3;
				case 12:
					num2 = 2;
					continue;
				case 13:
					goto IL_D8;
				case 14:
				{
					int num3;
					switch (num3)
					{
					case 0:
						text = A_0.GetAttribute(ClipboardData.b("࡫ͭ", a_), ClipboardData.b("ѫᩭѯɱ乳奵坷ॹύᙽꚇﲋﺏ煉歹ﺗ솟횡힣袥잧\ud8a9쮫膭\udfaf풱튳\udfb5\udbb7\udfb9톽ꎿ럁꧃ꏅꛇ뻉ﳍ崙꫗뿙냛뿝铟诡诣裥鯧苩藫黭華", a_));
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							num2 = 5;
							continue;
						}
						break;
					case 1:
						text = A_0.GetAttribute(ClipboardData.b("kŭ", a_), ClipboardData.b("ѫᩭѯɱ乳奵坷ॹύᙽꚇﲋﺏ煉歹ﺗ솟횡힣袥잧\ud8a9쮫膭\udfaf풱튳\udfb5\udbb7\udfb9톽ꎿ럁꧃ꏅꛇ뻉ﳍ崙꫗뿙냛뿝铟诡诣裥鯧苩藫黭華", a_));
						num2 = 11;
						continue;
					case 2:
						text = A_0.GetAttribute(ClipboardData.b("ᵫᵭ", a_), ClipboardData.b("ѫᩭѯɱ乳奵坷ॹύᙽꚇﲋﺏ煉歹ﺗ솟횡힣袥잧\ud8a9쮫膭\udfaf풱튳\udfb5\udbb7\udfb9톽ꎿ럁꧃ꏅꛇ뻉ﳍ崙꫗뿙냛뿝铟诡诣裥鯧苩藫黭華", a_));
						num2 = 3;
						continue;
					case 3:
						text = A_0.GetAttribute(ClipboardData.b("ཫᵭ", a_), ClipboardData.b("ѫᩭѯɱ乳奵坷ॹύᙽꚇﲋﺏ煉歹ﺗ솟횡힣袥잧\ud8a9쮫膭\udfaf풱튳\udfb5\udbb7\udfb9톽ꎿ럁꧃ꏅꛇ뻉ﳍ崙꫗뿙냛뿝铟诡诣裥鯧苩藫黭華", a_));
						num2 = 0;
						continue;
					default:
						num2 = 12;
						continue;
					}
					break;
				}
				case 15:
					A_1.Add(text);
					num2 = 13;
					continue;
				}
				break;
				IL_D8:
				if (true)
				{
				}
				num++;
				num2 = 6;
				continue;
				IL_135:
				num2 = 10;
				continue;
				IL_1D3:
				num2 = 9;
			}
		}
	}

	// Token: 0x060029B4 RID: 10676 RVA: 0x002996B0 File Offset: 0x002986B0
	internal static Stream ᜀ(XmlReader A_0)
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
		string s = A_0.ReadOuterXml();
		byte[] bytes = Encoding.UTF8.GetBytes(s);
		return new MemoryStream(bytes);
	}

	// Token: 0x060029B5 RID: 10677 RVA: 0x00299708 File Offset: 0x00298708
	protected sprỗ()
	{
		int a_ = 7;
		this.\u1775 = ClipboardData.b("ᩬnͰᝲ", a_) + sprỗ.\u1774 + ClipboardData.b("६nተٲᡴቶ᝸ེ卼ݾ", a_);
		this.\u1776 = ClipboardData.b("ᩬnͰᝲ", a_) + sprỗ.\u1774 + ClipboardData.b("Ṭ᭮ࡰὲၴѶ坸ͺၼ፾", a_);
		this.\u1777 = ClipboardData.b("ᩬnͰᝲ", a_) + sprỗ.\u1774 + ClipboardData.b("ͬᩮᱰᅲၴնၸᕺ᩼兾呂", a_);
		this.\u1778 = ClipboardData.b("ᩬnͰᝲ", a_) + sprỗ.\u1774 + ClipboardData.b("Ṭ੮հݲᱴ᥶Ṹࡺ卼ݾ", a_);
		this.\u1779 = ClipboardData.b("ᩬnͰᝲ", a_) + sprỗ.\u1774 + ClipboardData.b("୬nṰݲ᭴ᡶ൸Ṻ๼兾呂", a_);
		this.\u177A = ClipboardData.b("ᩬnͰᝲ", a_) + sprỗ.\u1774 + ClipboardData.b("࡬ŮᕰᵲᩴͶᱸࡺ卼ݾ", a_);
		this.\u177B = ClipboardData.b("ᩬnͰᝲ", a_) + sprỗ.\u1774 + ClipboardData.b("լ੮ၰᝲၴն", a_);
		this.\u177C = ClipboardData.b("ᩬnͰᝲ", a_) + sprỗ.\u1774 + ClipboardData.b("୬nṰݲၴն", a_);
		this.\u177D = ClipboardData.b("ᩬnͰᝲ", a_) + sprỗ.\u1774 + ClipboardData.b("๬nᱰṲၴ᥶൸ࡺ卼ݾ", a_);
		this.\u177E = string.Concat(new string[]
		{
			ClipboardData.b("ᩬnͰᝲ", a_),
			sprỗ.\u1774,
			ClipboardData.b("l੮ᕰᩲᑴ", a_),
			sprỗ.\u1774,
			ClipboardData.b("Ѭɮၰᑲၴ", a_)
		});
		this.\u177F = ClipboardData.b("㉬ᵮᑰὲٴ", a_) + sprỗ.\u1774 + ClipboardData.b("䍬ᵮᑰὲٴ", a_);
		this.ក = string.Concat(new string[]
		{
			ClipboardData.b("ᩬnͰᝲ", a_),
			sprỗ.\u1774,
			ClipboardData.b("㉬ᵮᑰὲٴ", a_),
			sprỗ.\u1774,
			ClipboardData.b("६nተٲᡴቶ᝸ེ卼ݾꮄﺌ", a_)
		});
		this.ខ = string.Concat(new string[]
		{
			ClipboardData.b("ᩬnͰᝲ", a_),
			sprỗ.\u1774,
			ClipboardData.b("㉬ᵮᑰὲٴ", a_),
			sprỗ.\u1774,
			ClipboardData.b("๬nᱰṲၴ᥶൸ࡺ卼ݾꮄﺌ", a_)
		});
		this.គ = string.Concat(new string[]
		{
			ClipboardData.b("ᩬnͰᝲ", a_),
			sprỗ.\u1774,
			ClipboardData.b("㉬ᵮᑰὲٴ", a_),
			sprỗ.\u1774,
			ClipboardData.b("୬nṰݲ᭴ᡶ൸Ṻ๼兾呂ꦆﮈﲎ", a_)
		});
		this.ឃ = string.Concat(new string[]
		{
			ClipboardData.b("ᩬnͰᝲ", a_),
			sprỗ.\u1774,
			ClipboardData.b("㉬ᵮᑰὲٴ", a_),
			sprỗ.\u1774,
			ClipboardData.b("࡬ŮᕰᵲᩴͶᱸࡺ卼ݾꮄﺌ", a_)
		});
		this.ង = string.Concat(new string[]
		{
			ClipboardData.b("ᩬnͰᝲ", a_),
			sprỗ.\u1774,
			ClipboardData.b("㉬ᵮᑰὲٴ", a_),
			sprỗ.\u1774,
			ClipboardData.b("ͬᩮᱰᅲၴնၸᕺ᩼兾呂ꦆﮈﲎ", a_)
		});
		this.ច = string.Concat(new string[]
		{
			ClipboardData.b("ᩬnͰᝲ", a_),
			sprỗ.\u1774,
			ClipboardData.b("㉬ᵮᑰὲٴ", a_),
			sprỗ.\u1774,
			ClipboardData.b("լ੮ၰᝲၴն", a_)
		});
		this.ឆ = string.Concat(new string[]
		{
			ClipboardData.b("ᩬnͰᝲ", a_),
			sprỗ.\u1774,
			ClipboardData.b("㉬ᵮᑰὲٴ", a_),
			sprỗ.\u1774,
			ClipboardData.b("୬nṰݲၴն", a_)
		});
		this.ជ = ClipboardData.b("६nተ⍲ݴᡶॸࡺ", a_) + sprỗ.\u1774 + ClipboardData.b("౬ὮŰ嵲൴᩶ᕸ", a_);
		this.ឈ = ClipboardData.b("६nተ⍲ݴᡶॸࡺ", a_) + sprỗ.\u1774 + ClipboardData.b("๬nͰᙲ孴ྲྀᑸ᝺", a_);
		this.ញ = ClipboardData.b("६nተ⍲ݴᡶॸࡺ", a_) + sprỗ.\u1774 + ClipboardData.b("๬ᩮɰݲᩴ᩶坸ͺၼ፾", a_);
		this.ដ = ClipboardData.b("ᩬnͰᝲ", a_) + sprỗ.\u1774 + ClipboardData.b("୬nὰݲⅴᙶ᭸᝺᡼兾呂", a_);
		this.ឋ = ClipboardData.b("㙬ⱮṰᵲŴቶ᝸ེ≼⭾품ꖊ﶐", a_);
		this.ឌ = ClipboardData.b("ᩬnͰᝲ", a_) + sprỗ.\u1774 + ClipboardData.b("๬ݮၰŲŴѶ", a_) + sprỗ.\u1774;
		this.ឍ = ClipboardData.b("ᩬnͰᝲ", a_) + sprỗ.\u1774 + ClipboardData.b("࡬ɮ፰ᙲᅴ፶ၸᕺ᩼౾", a_) + sprỗ.\u1774;
		this.ណ = string.Concat(new string[]
		{
			ClipboardData.b("ᩬnͰᝲ", a_),
			sprỗ.\u1774,
			ClipboardData.b("६ᵮၰѲᱴ᥶Ṹࡺ", a_),
			sprỗ.\u1774,
			ClipboardData.b("६ᵮၰѲᱴ᥶Ṹ䩺卼ݾ", a_)
		});
		this.ត = string.Concat(new string[]
		{
			ClipboardData.b("ᩬnͰᝲ", a_),
			sprỗ.\u1774,
			ClipboardData.b("㉬ᵮᑰὲٴ", a_),
			sprỗ.\u1774,
			ClipboardData.b("Ṭ੮հݲᱴ᥶Ṹࡺ卼ݾꮄﺌ", a_)
		});
		this.ថ = ClipboardData.b("ᩬnͰᝲ", a_) + sprỗ.\u1774 + ClipboardData.b("᭬൮ၰ⍲ݴᡶ፸ṺṼ୾꾀", a_);
		this.ទ = string.Concat(new string[]
		{
			ClipboardData.b("ᩬnͰᝲ", a_),
			sprỗ.\u1774,
			ClipboardData.b("㉬ᵮᑰὲٴ", a_),
			sprỗ.\u1774,
			ClipboardData.b("᭬൮ၰ⍲ݴᡶ፸ṺṼ୾꾀ꞈ力", a_)
		});
		this.ធ = ClipboardData.b("ᩬnͰᝲ", a_) + sprỗ.\u1774 + ClipboardData.b("᭬൮ၰ㝲ᑴͶᡸ啺ռቾ", a_);
		this.ន = ClipboardData.b("๬ᩮɰݲᩴ᩶ⅸᙺᅼ", a_) + sprỗ.\u1774;
		base..ctor();
	}

	// Token: 0x060029B6 RID: 10678 RVA: 0x00299E5C File Offset: 0x00298E5C
	// Note: this type is marked as 'beforefieldinit'.
	static sprỗ()
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
		sprỗ.\u1774 = '\\'.ToString();
	}

	// Token: 0x0400241B RID: 9243
	internal const string ᜀ = "vbaProject.bin";

	// Token: 0x0400241C RID: 9244
	internal const string ᜁ = "vbaData.xml";

	// Token: 0x0400241D RID: 9245
	internal const string ᜂ = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument";

	// Token: 0x0400241E RID: 9246
	internal const string ᜃ = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles";

	// Token: 0x0400241F RID: 9247
	internal const string ᜄ = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/numbering";

	// Token: 0x04002420 RID: 9248
	internal const string ᜅ = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/settings";

	// Token: 0x04002421 RID: 9249
	internal const string ᜆ = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/header";

	// Token: 0x04002422 RID: 9250
	internal const string ᜇ = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/footer";

	// Token: 0x04002423 RID: 9251
	internal const string ᜈ = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/endnotes";

	// Token: 0x04002424 RID: 9252
	internal const string ᜉ = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/footnotes";

	// Token: 0x04002425 RID: 9253
	internal const string ᜊ = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/comments";

	// Token: 0x04002426 RID: 9254
	internal const string ᜋ = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image";

	// Token: 0x04002427 RID: 9255
	internal const string ᜌ = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/hyperlink";

	// Token: 0x04002428 RID: 9256
	internal const string \u170D = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/extended-properties";

	// Token: 0x04002429 RID: 9257
	internal const string ᜎ = "http://schemas.openxmlformats.org/package/2006/relationships/metadata/core-properties";

	// Token: 0x0400242A RID: 9258
	internal const string ᜏ = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/custom-properties";

	// Token: 0x0400242B RID: 9259
	internal const string ᜐ = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/fontTable";

	// Token: 0x0400242C RID: 9260
	internal const string ᜑ = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/chart";

	// Token: 0x0400242D RID: 9261
	internal const string \u1712 = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/theme";

	// Token: 0x0400242E RID: 9262
	internal const string \u1713 = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/control";

	// Token: 0x0400242F RID: 9263
	internal const string \u1714 = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/oleObject";

	// Token: 0x04002430 RID: 9264
	internal const string \u1715 = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/package";

	// Token: 0x04002431 RID: 9265
	internal const string \u1716 = "http://schemas.microsoft.com/office/2006/relationships/vbaProject";

	// Token: 0x04002432 RID: 9266
	internal const string \u1717 = "http://schemas.microsoft.com/office/2006/relationships/wordVbaData";

	// Token: 0x04002433 RID: 9267
	internal const string \u1718 = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/customXml";

	// Token: 0x04002434 RID: 9268
	internal const string \u1719 = "http://schemas.microsoft.com/office/2006/relationships/ui/extensibility";

	// Token: 0x04002435 RID: 9269
	internal const string \u171A = "application/xml";

	// Token: 0x04002436 RID: 9270
	internal const string \u171B = "application/vnd.openxmlformats-officedocument.wordprocessingml.document.main+xml";

	// Token: 0x04002437 RID: 9271
	internal const string \u171C = "application/vnd.openxmlformats-officedocument.wordprocessingml.styles+xml";

	// Token: 0x04002438 RID: 9272
	internal const string \u171D = "application/vnd.openxmlformats-officedocument.wordprocessingml.numbering+xml";

	// Token: 0x04002439 RID: 9273
	internal const string \u171E = "application/vnd.openxmlformats-officedocument.wordprocessingml.settings+xml";

	// Token: 0x0400243A RID: 9274
	internal const string \u171F = "application/vnd.openxmlformats-officedocument.wordprocessingml.footer+xml";

	// Token: 0x0400243B RID: 9275
	internal const string ᜠ = "application/vnd.openxmlformats-officedocument.wordprocessingml.header+xml";

	// Token: 0x0400243C RID: 9276
	internal const string ᜡ = "application/vnd.openxmlformats-officedocument.wordprocessingml.footnotes+xml";

	// Token: 0x0400243D RID: 9277
	internal const string ᜢ = "application/vnd.openxmlformats-officedocument.wordprocessingml.endnotes+xml";

	// Token: 0x0400243E RID: 9278
	internal const string ᜣ = "application/vnd.openxmlformats-officedocument.wordprocessingml.comments+xml";

	// Token: 0x0400243F RID: 9279
	internal const string ᜤ = "application/vnd.openxmlformats-package.relationships+xml";

	// Token: 0x04002440 RID: 9280
	internal const string ᜥ = "application/vnd.openxmlformats-officedocument.extended-properties+xml";

	// Token: 0x04002441 RID: 9281
	internal const string ᜦ = "application/vnd.openxmlformats-package.core-properties+xml";

	// Token: 0x04002442 RID: 9282
	internal const string ᜧ = "application/vnd.openxmlformats-officedocument.custom-properties+xml";

	// Token: 0x04002443 RID: 9283
	internal const string ᜨ = "application/vnd.openxmlformats-officedocument.wordprocessingml.fontTable+xml";

	// Token: 0x04002444 RID: 9284
	internal const string ᜩ = "application/vnd.openxmlformats-officedocument.drawingml.chart+xml";

	// Token: 0x04002445 RID: 9285
	internal const string ᜪ = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

	// Token: 0x04002446 RID: 9286
	internal const string ᜫ = "application/vnd.openxmlformats-officedocument.theme+xml";

	// Token: 0x04002447 RID: 9287
	internal const string ᜬ = "application/vnd.openxmlformats-officedocument.drawingml.chartshapes+xml";

	// Token: 0x04002448 RID: 9288
	internal const string ᜭ = "application/vnd.ms-office.activeX+xml";

	// Token: 0x04002449 RID: 9289
	internal const string ᜮ = "application/vnd.ms-office.activeX";

	// Token: 0x0400244A RID: 9290
	internal const string ᜯ = "application/vnd.ms-office.vbaProject";

	// Token: 0x0400244B RID: 9291
	internal const string ᜰ = "application/vnd.ms-word.vbaData+xml";

	// Token: 0x0400244C RID: 9292
	internal const string ᜱ = "application/vnd.ms-word.document.macroEnabled.main+xml";

	// Token: 0x0400244D RID: 9293
	internal const string \u1732 = "application/vnd.ms-word.template.macroEnabledTemplate.main+xml";

	// Token: 0x0400244E RID: 9294
	internal const string \u1733 = "application/vnd.openxmlformats-officedocument.wordprocessingml.template.main+xml";

	// Token: 0x0400244F RID: 9295
	internal const string \u1734 = "application/vnd.openxmlformats-officedocument.customXmlProperties+xml";

	// Token: 0x04002450 RID: 9296
	internal const string \u1735 = "application/vnd.openxmlformats-officedocument.drawingml.diagramStyle+xml";

	// Token: 0x04002451 RID: 9297
	internal const string \u1736 = "application/vnd.openxmlformats-officedocument.drawingml.diagramColors+xml";

	// Token: 0x04002452 RID: 9298
	internal const string \u1737 = "application/vnd.openxmlformats-officedocument.drawingml.diagramData+xml";

	// Token: 0x04002453 RID: 9299
	internal const string \u1738 = "application/vnd.openxmlformats-officedocument.drawingml.diagramLayout+xml";

	// Token: 0x04002454 RID: 9300
	internal const string \u1739 = "http://schemas.openxmlformats.org/package/2006/content-types";

	// Token: 0x04002455 RID: 9301
	internal const string \u173A = "Relationships";

	// Token: 0x04002456 RID: 9302
	internal const string \u173B = "Relationship";

	// Token: 0x04002457 RID: 9303
	internal const string \u173C = "Types";

	// Token: 0x04002458 RID: 9304
	internal const string \u173D = "Type";

	// Token: 0x04002459 RID: 9305
	internal const string \u173E = "Target";

	// Token: 0x0400245A RID: 9306
	internal const string \u173F = "TargetMode";

	// Token: 0x0400245B RID: 9307
	internal const string ᝀ = "Id";

	// Token: 0x0400245C RID: 9308
	internal const string ᝁ = "xmlns";

	// Token: 0x0400245D RID: 9309
	internal const string ᝂ = "Default";

	// Token: 0x0400245E RID: 9310
	internal const string ᝃ = "Override";

	// Token: 0x0400245F RID: 9311
	internal const string ᝄ = "Extension";

	// Token: 0x04002460 RID: 9312
	internal const string ᝅ = "ContentType";

	// Token: 0x04002461 RID: 9313
	internal const string ᝆ = "PartName";

	// Token: 0x04002462 RID: 9314
	internal const string ᝇ = "Properties";

	// Token: 0x04002463 RID: 9315
	internal const string ᝈ = "coreProperties";

	// Token: 0x04002464 RID: 9316
	internal const string ᝉ = "styles";

	// Token: 0x04002465 RID: 9317
	internal const string ᝊ = "style";

	// Token: 0x04002466 RID: 9318
	internal const string ᝋ = "tblStylePr";

	// Token: 0x04002467 RID: 9319
	internal const string ᝌ = "tblPr";

	// Token: 0x04002468 RID: 9320
	internal const string ᝍ = "trPr";

	// Token: 0x04002469 RID: 9321
	internal const string ᝎ = "tcPr";

	// Token: 0x0400246A RID: 9322
	internal const string ᝏ = "pPr";

	// Token: 0x0400246B RID: 9323
	internal const string ᝐ = "rPr";

	// Token: 0x0400246C RID: 9324
	internal const string ᝑ = "w:hdr";

	// Token: 0x0400246D RID: 9325
	internal const string \u1752 = "w:ftr";

	// Token: 0x0400246E RID: 9326
	internal const string \u1753 = "w:headerReference";

	// Token: 0x0400246F RID: 9327
	internal const string \u1754 = "w:footerReference";

	// Token: 0x04002470 RID: 9328
	internal const string \u1755 = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";

	// Token: 0x04002471 RID: 9329
	internal const string \u1756 = "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing";

	// Token: 0x04002472 RID: 9330
	internal const string \u1757 = "http://schemas.openxmlformats.org/drawingml/2006/picture";

	// Token: 0x04002473 RID: 9331
	internal const string \u1758 = "http://schemas.openxmlformats.org/drawingml/2006/main";

	// Token: 0x04002474 RID: 9332
	internal const string \u1759 = "http://schemas.openxmlformats.org/officeDocument/2006/relationships";

	// Token: 0x04002475 RID: 9333
	internal const string \u175A = "http://schemas.openxmlformats.org/package/2006/relationships";

	// Token: 0x04002476 RID: 9334
	internal const string \u175B = "urn:schemas-microsoft-com:vml";

	// Token: 0x04002477 RID: 9335
	internal const string \u175C = "urn:schemas-microsoft-com:office:office";

	// Token: 0x04002478 RID: 9336
	internal const string \u175D = "http://www.w3.org/XML/1998/namespace";

	// Token: 0x04002479 RID: 9337
	internal const string \u175E = "urn:schemas-microsoft-com:office:word";

	// Token: 0x0400247A RID: 9338
	internal const string \u175F = "http://schemas.openxmlformats.org/package/2006/metadata/core-properties";

	// Token: 0x0400247B RID: 9339
	internal const string ᝠ = "http://purl.org/dc/elements/1.1/";

	// Token: 0x0400247C RID: 9340
	internal const string ᝡ = "http://purl.org/dc/terms/";

	// Token: 0x0400247D RID: 9341
	internal const string ᝢ = "http://www.w3.org/2001/XMLSchema-instance";

	// Token: 0x0400247E RID: 9342
	internal const string ᝣ = "http://schemas.openxmlformats.org/officeDocument/2006/extended-properties";

	// Token: 0x0400247F RID: 9343
	internal const string ᝤ = "http://schemas.openxmlformats.org/markup-compatibility/2006";

	// Token: 0x04002480 RID: 9344
	internal const string ᝥ = "http://schemas.openxmlformats.org/officeDocument/2006/math";

	// Token: 0x04002481 RID: 9345
	internal const string ᝦ = "http://schemas.microsoft.com/office/word/2006/wordml";

	// Token: 0x04002482 RID: 9346
	internal const string ᝧ = "http://purl.org/dc/dcmitype/";

	// Token: 0x04002483 RID: 9347
	internal const string ᝨ = "http://schemas.openxmlformats.org/officeDocument/2006/custom-properties";

	// Token: 0x04002484 RID: 9348
	internal const string ᝩ = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes";

	// Token: 0x04002485 RID: 9349
	internal const string ᝪ = "http://schemas.openxmlformats.org/drawingml/2006/chart";

	// Token: 0x04002486 RID: 9350
	internal const int ᝫ = 20;

	// Token: 0x04002487 RID: 9351
	internal const int ᝬ = 8;

	// Token: 0x04002488 RID: 9352
	internal const char \u176D = '\u001e';

	// Token: 0x04002489 RID: 9353
	internal const char ᝮ = '\u001f';

	// Token: 0x0400248A RID: 9354
	internal const char ᝯ = '\u00a0';

	// Token: 0x0400248B RID: 9355
	internal const string ᝰ = "mso-fit-shape-to-text:t";

	// Token: 0x0400248C RID: 9356
	internal const string \u1771 = "mso-fit-text-to-shape:t";

	// Token: 0x0400248D RID: 9357
	internal const string \u1772 = "#whitespace";

	// Token: 0x0400248E RID: 9358
	internal const string \u1773 = "#comment";

	// Token: 0x0400248F RID: 9359
	private static readonly string \u1774;

	// Token: 0x04002490 RID: 9360
	internal readonly string \u1775;

	// Token: 0x04002491 RID: 9361
	internal readonly string \u1776;

	// Token: 0x04002492 RID: 9362
	internal readonly string \u1777;

	// Token: 0x04002493 RID: 9363
	internal readonly string \u1778;

	// Token: 0x04002494 RID: 9364
	internal readonly string \u1779;

	// Token: 0x04002495 RID: 9365
	internal readonly string \u177A;

	// Token: 0x04002496 RID: 9366
	internal readonly string \u177B;

	// Token: 0x04002497 RID: 9367
	internal readonly string \u177C;

	// Token: 0x04002498 RID: 9368
	internal readonly string \u177D;

	// Token: 0x04002499 RID: 9369
	internal readonly string \u177E;

	// Token: 0x0400249A RID: 9370
	internal readonly string \u177F;

	// Token: 0x0400249B RID: 9371
	internal readonly string ក;

	// Token: 0x0400249C RID: 9372
	internal readonly string ខ;

	// Token: 0x0400249D RID: 9373
	internal readonly string គ;

	// Token: 0x0400249E RID: 9374
	internal readonly string ឃ;

	// Token: 0x0400249F RID: 9375
	internal readonly string ង;

	// Token: 0x040024A0 RID: 9376
	internal readonly string ច;

	// Token: 0x040024A1 RID: 9377
	internal readonly string ឆ;

	// Token: 0x040024A2 RID: 9378
	internal readonly string ជ;

	// Token: 0x040024A3 RID: 9379
	internal readonly string ឈ;

	// Token: 0x040024A4 RID: 9380
	internal readonly string ញ;

	// Token: 0x040024A5 RID: 9381
	internal readonly string ដ;

	// Token: 0x040024A6 RID: 9382
	internal readonly string ឋ;

	// Token: 0x040024A7 RID: 9383
	internal readonly string ឌ;

	// Token: 0x040024A8 RID: 9384
	internal readonly string ឍ;

	// Token: 0x040024A9 RID: 9385
	internal readonly string ណ;

	// Token: 0x040024AA RID: 9386
	internal readonly string ត;

	// Token: 0x040024AB RID: 9387
	internal readonly string ថ;

	// Token: 0x040024AC RID: 9388
	internal readonly string ទ;

	// Token: 0x040024AD RID: 9389
	internal readonly string ធ;

	// Token: 0x040024AE RID: 9390
	internal readonly string ន;
}
