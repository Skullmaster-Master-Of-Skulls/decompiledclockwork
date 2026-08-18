using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;
using Spire.CompoundFile.XLS;
using Spire.Xls;
using Spire.Xls.Collections;
using Spire.Xls.Core;
using Spire.Xls.Core.Interfaces;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Charts;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlReaders.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlSerialization;
using Spire.Xls.Core.Spreadsheet.XmlSerialization.Charts;

// Token: 0x02000520 RID: 1312
internal class spr\u2306
{
	// Token: 0x06004F78 RID: 20344 RVA: 0x00301718 File Offset: 0x00300718
	internal FormulaUtil ᜀ()
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
		return this.ᜊ;
	}

	// Token: 0x06004F79 RID: 20345 RVA: 0x0030175C File Offset: 0x0030075C
	internal XlsWorksheet ᜁ()
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
		return this.\u1712;
	}

	// Token: 0x06004F7A RID: 20346 RVA: 0x003017A0 File Offset: 0x003007A0
	public spr\u2306(XlsWorkbook A_0)
	{
		int a_ = 17;
		this.ᜋ = new Dictionary<int, ShapeParser>();
		this.ᜎ = null;
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("╆♈⑊♌", a_));
		}
		this.ᜉ = A_0;
		this.ᜊ = new FormulaUtil(this.ᜉ.AppImplementation, this.ᜉ, NumberFormatInfo.InvariantInfo, ',', ';');
		this.ᜋ.Add(202, new spr\u1BEC());
		this.ᜋ.Add(201, new sprᦞ());
		this.ᜋ.Add(75, new spr\u1AA7());
	}

	// Token: 0x06004F7B RID: 20347 RVA: 0x00301854 File Offset: 0x00300854
	public Color ᜎ(string A_0)
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
		return spr\u2306.ᜀ(A_0, this.\u170D);
	}

	// Token: 0x06004F7C RID: 20348 RVA: 0x0030189C File Offset: 0x0030089C
	public static Color ᜀ(string A_0, Dictionary<string, Color> A_1)
	{
		int a_ = 18;
		int num = 11;
		Color result;
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
					if (A_0 == RecordTableEnumerator.b("⩇ⵉ繋", a_))
					{
						num = 3;
						continue;
					}
					return result;
				case 1:
					goto IL_F7;
				case 2:
					goto IL_BB;
				case 3:
					A_0 = RecordTableEnumerator.b("⑇㹉繋", a_);
					result = spr\u2306.ᜀ(A_0, A_1);
					num = 1;
					continue;
				case 4:
					if (A_0 == RecordTableEnumerator.b("⩇ⵉ絋", a_))
					{
						num = 10;
						continue;
					}
					goto IL_7D;
				case 5:
					goto IL_124;
				case 6:
					num = 8;
					continue;
				case 7:
					goto IL_126;
				case 8:
				{
					if (A_0.Length == 0)
					{
						num = 5;
						continue;
					}
					result = spr\u1D39.ᜂ;
					bool flag = A_1.TryGetValue(A_0, out result);
					num = 12;
					continue;
				}
				case 9:
				{
					bool flag;
					if (!flag)
					{
						num = 7;
						continue;
					}
					return result;
				}
				case 10:
					if (true)
					{
					}
					A_0 = RecordTableEnumerator.b("⑇㹉絋", a_);
					result = spr\u2306.ᜀ(A_0, A_1);
					num = 2;
					continue;
				case 12:
				{
					bool flag;
					if (!flag)
					{
						num = 13;
						continue;
					}
					goto IL_7D;
				}
				case 13:
					num = 4;
					continue;
				}
				if (A_0 != null)
				{
					num = 6;
					continue;
				}
				goto IL_C0;
				IL_7D:
				num = 9;
				continue;
			}
			IL_126:
			num = 0;
		}
		IL_BB:
		return result;
		IL_C0:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⭇╉⁋⅍≏᱑㕓㭕㵗", a_));
		IL_F7:
		return result;
		IL_124:
		goto IL_C0;
	}

	// Token: 0x06004F7D RID: 20349 RVA: 0x00301A68 File Offset: 0x00300A68
	public void ᜀ(XmlReader A_0, IDictionary<string, string> A_1, IDictionary<string, string> A_2)
	{
		int a_ = 3;
		int num = 27;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_25D;
			case 1:
				goto IL_21E;
			case 2:
				if (A_2 == null)
				{
					num = 1;
					continue;
				}
				goto IL_18B;
			case 3:
				if (A_0.NamespaceURI == RecordTableEnumerator.b("儸伺䤼伾筀求橄㑆⩈⍊⡌≎ぐ⁒答㡖⥘㹚㍜❞ౠརͤࡦ᭨٪౬᭮ɰ嵲ᩴնṸ呺ർṾꒊ뾌뾎ꆐꖒ몔爵쾠힢袤펦킨\udbaa좬\udcae", a_))
				{
					num = 15;
					continue;
				}
				goto IL_405;
			case 4:
				if (A_0.EOF)
				{
					num = 19;
					continue;
				}
				A_0.Read();
				num = 29;
				continue;
			case 5:
				goto IL_B1;
			case 6:
				num = 14;
				continue;
			case 7:
				num = 31;
				continue;
			case 8:
				if (true)
				{
				}
				if (A_0.LocalName == RecordTableEnumerator.b("洸䈺䴼娾㉀", a_))
				{
					num = 25;
					continue;
				}
				goto IL_405;
			case 9:
				num = 18;
				continue;
			case 10:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("瘸䴺堼䴾㍀⩂⅄≆", a_)))
				{
					num = 6;
					continue;
				}
				this.ᜀ(A_0, A_2, RecordTableEnumerator.b("椸娺似䬾ཀ≂⡄≆", a_), RecordTableEnumerator.b("稸吺匼䬾⑀ⵂㅄፆえ㭊⡌", a_));
				num = 32;
				continue;
			}
			case 11:
				goto IL_152;
			case 12:
				goto IL_353;
			case 13:
				goto IL_39A;
			case 14:
				goto IL_C1;
			case 15:
				A_0.Read();
				num = 21;
				continue;
			case 16:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 28;
					continue;
				}
				num = 22;
				continue;
			case 17:
				num = 4;
				continue;
			case 18:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("紸帺嬼帾㑀⽂ㅄ", a_)))
				{
					num = 12;
					continue;
				}
				this.ᜀ(A_0, A_1, RecordTableEnumerator.b("簸䌺䤼娾⽀あⱄ⡆❈", a_), RecordTableEnumerator.b("稸吺匼䬾⑀ⵂㅄፆえ㭊⡌", a_));
				num = 20;
				continue;
			}
			case 19:
				goto IL_223;
			case 20:
				goto IL_1B2;
			case 21:
				goto IL_152;
			case 22:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 7;
					continue;
				}
				A_0.Read();
				num = 24;
				continue;
			case 23:
				if (A_1 == null)
				{
					num = 13;
					continue;
				}
				num = 2;
				continue;
			case 24:
				goto IL_152;
			case 25:
				num = 3;
				continue;
			case 26:
				if (A_0.NodeType != XmlNodeType.Element)
				{
					num = 17;
					continue;
				}
				goto IL_223;
			case 28:
				return;
			case 29:
				goto IL_18B;
			case 30:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_353;
				default:
					if (false)
					{
					}
					if (A_0.EOF)
					{
						num = 0;
						continue;
					}
					num = 8;
					continue;
				}
				break;
			case 31:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 9;
					continue;
				}
				goto IL_FE;
			}
			case 32:
				goto IL_1B2;
			}
			if (A_0 == null)
			{
				num = 5;
				continue;
			}
			num = 23;
			continue;
			IL_152:
			num = 16;
			continue;
			IL_18B:
			num = 26;
			continue;
			IL_1B2:
			A_0.Skip();
			num = 11;
			continue;
			IL_223:
			num = 30;
			continue;
			IL_353:
			num = 10;
		}
		IL_B1:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬸帺尼嬾⑀ㅂ", a_));
		IL_C1:
		IL_FE:
		throw new NotImplementedException(A_0.LocalName);
		IL_21E:
		throw new ArgumentNullException(RecordTableEnumerator.b("娸吺匼䬾⑀ⵂㅄࡆ㽈⹊㽌㵎㡐㝒ご⑖", a_));
		IL_25D:
		throw new XmlException(RecordTableEnumerator.b("稸娺匼儾⹀㝂敄⭆♈⡊ⱌ㭎㑐獒ご⽖⥘⽚㡜㱞ᕠ٢Ť䝦ᅨ٪Ŭ佮հቲቴ", a_));
		IL_39A:
		throw new ArgumentNullException(RecordTableEnumerator.b("娸吺匼䬾⑀ⵂㅄ͆ⱈⵊⱌ㩎㵐❒♔", a_));
		IL_405:
		throw new XmlException(RecordTableEnumerator.b("稸娺匼儾⹀㝂敄⭆♈⡊ⱌ㭎㑐獒㑔❖⥘⥚㉜⽞፠੢Ѥ፦౨䭪ᕬɮᵰ卲ŴᙶṸ", a_));
	}

	// Token: 0x06004F7E RID: 20350 RVA: 0x00301E90 File Offset: 0x00300E90
	public void ᜀ(XmlReader A_0, RelationsCollection A_1, sprវ A_2, string A_3, Stream A_4, Stream A_5, ref List<Dictionary<string, string>> A_6, Stream A_7)
	{
		int a_ = 17;
		switch (0)
		{
		default:
		{
			int num = 11;
			XmlWriter xmlWriter;
			int activeSheetIndex;
			int displayedTab;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					int num2;
					switch (num2)
					{
					case 0:
					{
						this.ᜫ(A_0);
						bool flag;
						StreamWriter a_2;
						this.ᜀ(ref flag, ref xmlWriter, ref a_2, A_5);
						num = 24;
						continue;
					}
					case 1:
					{
						this.ᜁ(A_0, A_1, A_2, A_3);
						bool flag;
						StreamWriter a_2;
						this.ᜀ(ref flag, ref xmlWriter, ref a_2, A_5);
						num = 29;
						continue;
					}
					case 2:
					{
						A_6 = this.ᜀ(A_0, out activeSheetIndex, out displayedTab);
						bool flag;
						StreamWriter a_2;
						this.ᜀ(ref flag, ref xmlWriter, ref a_2, A_5);
						num = 12;
						continue;
					}
					case 3:
						this.\u171F(A_0);
						num = 1;
						continue;
					case 4:
						this.ᜂ(A_0);
						num = 3;
						continue;
					case 5:
						this.\u171E(A_0);
						num = 22;
						continue;
					case 6:
						this.ᜀ(A_0, this.ᜉ.DataHolder.\u171B());
						num = 31;
						continue;
					case 7:
					{
						XmlWriter xmlWriter2 = UtilityMethods.ᜀ(A_7, Encoding.UTF8);
						xmlWriter2.WriteNode(A_0, false);
						num = 2;
						continue;
					}
					case 8:
						this.ᜠ(A_0);
						num = 13;
						continue;
					case 9:
						this.ᜢ(A_0);
						num = 4;
						continue;
					default:
						num = 15;
						continue;
					}
					break;
				}
				case 1:
					goto IL_5EA;
				case 2:
					goto IL_5EA;
				case 3:
					goto IL_5EA;
				case 4:
					goto IL_5EA;
				case 5:
					num = 7;
					continue;
				case 6:
					num = 33;
					continue;
				case 7:
					if (spr\u22D2.\u177B == null)
					{
						num = 36;
						continue;
					}
					goto IL_425;
				case 8:
					num = 0;
					continue;
				case 9:
					goto IL_5EA;
				case 10:
					goto IL_60F;
				case 12:
					goto IL_5EA;
				case 13:
					goto IL_5EA;
				case 14:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 5;
						continue;
					}
					goto IL_225;
				}
				case 15:
					num = 27;
					continue;
				case 16:
					goto IL_D6;
				case 17:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_125;
					default:
					{
						if (false)
						{
						}
						bool flag = false;
						StreamWriter a_2 = new StreamWriter(A_4);
						xmlWriter = UtilityMethods.ᜀ(a_2);
						xmlWriter.WriteStartElement(RecordTableEnumerator.b("㕆♈⑊㥌", a_), RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒♔㑖ㅘ㹚ぜ㹞በ䵢੤ᝦ౨ժᕬɮᵰᕲᩴնᑸ᩺ॼ౾꾀Ꚉﶌﶎﺚ철쾢誤閦馨鮪鮬肮\udcb0튲\udcb4\ud9b6", a_));
						A_0.Read();
						activeSheetIndex = 0;
						displayedTab = 0;
						num = 21;
						continue;
					}
					}
					break;
				case 18:
					goto IL_119;
				case 19:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 10;
						continue;
					}
					num = 32;
					continue;
				case 20:
					goto IL_425;
				case 21:
					goto IL_5EA;
				case 22:
					goto IL_5EA;
				case 23:
					goto IL_5EA;
				case 24:
					goto IL_5EA;
				case 25:
					goto IL_125;
				case 26:
					num = 14;
					continue;
				case 27:
					goto IL_225;
				case 28:
					if (A_5 == null)
					{
						num = 30;
						continue;
					}
					goto IL_119;
				case 29:
					goto IL_5EA;
				case 30:
					goto IL_176;
				case 31:
					goto IL_5EA;
				case 32:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 26;
						continue;
					}
					A_0.Read();
					num = 23;
					continue;
				case 33:
					if (true)
					{
					}
					if (A_0.LocalName == RecordTableEnumerator.b("う♈㥊♌ⵎ㹐㱒㹔", a_))
					{
						num = 17;
						continue;
					}
					goto IL_614;
				case 34:
				{
					int num2;
					string localName;
					if (spr\u22D2.\u177B.TryGetValue(localName, out num2))
					{
						num = 8;
						continue;
					}
					goto IL_225;
				}
				case 35:
					if (A_4 == null)
					{
						num = 37;
						continue;
					}
					num = 28;
					continue;
				case 36:
					spr\u22D2.\u177B = new Dictionary<string, int>(10)
					{
						{
							RecordTableEnumerator.b("⍆ⱈⵊ⑌ⅎ㑐㝒᭔㙖㑘㹚⹜", a_),
							0
						},
						{
							RecordTableEnumerator.b("㑆ⅈ⹊⡌㭎≐", a_),
							1
						},
						{
							RecordTableEnumerator.b("╆♈⑊♌᥎㡐㙒≔⑖", a_),
							2
						},
						{
							RecordTableEnumerator.b("⑆⡈❊⹌὎⍐", a_),
							3
						},
						{
							RecordTableEnumerator.b("≆ㅈ㽊⡌㵎㽐㉒㥔Ֆ㱘㵚㡜ⵞѠൢ٤ɦᩨ", a_),
							4
						},
						{
							RecordTableEnumerator.b("う♈㥊♌ⵎ㹐㱒㹔ݖ⭘㑚⥜㩞ɠᝢ౤ࡦݨ", a_),
							5
						},
						{
							RecordTableEnumerator.b("ⅆ⁈❊⡌᥎㑐⅒♔㹖㙘㕚", a_),
							6
						},
						{
							RecordTableEnumerator.b("ⅆ㱈╊⹌㭎㡐㱒㭔ၖ⭘㑚⡜⽞በ", a_),
							7
						},
						{
							RecordTableEnumerator.b("う♈㥊♌ⵎ㹐㱒㹔ݖ⭘", a_),
							8
						},
						{
							RecordTableEnumerator.b("㝆⁈㵊≌㭎ቐ㉒㙔㽖㱘⡚", a_),
							9
						}
					};
					num = 20;
					continue;
				case 37:
					goto IL_5A6;
				}
				if (A_0 == null)
				{
					num = 16;
					continue;
				}
				num = 35;
				continue;
				IL_119:
				num = 25;
				continue;
				IL_125:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 6;
					continue;
				}
				A_0.Read();
				num = 18;
				continue;
				IL_225:
				xmlWriter.WriteNode(A_0, false);
				num = 9;
				continue;
				IL_425:
				num = 34;
				continue;
				IL_5EA:
				num = 19;
			}
			IL_D6:
			throw new ArgumentNullException(RecordTableEnumerator.b("㕆ⱈ⩊⥌⩎⍐", a_));
			IL_176:
			throw new ArgumentNullException(RecordTableEnumerator.b("㑆㵈㥊⡌⹎㱐ᙒ㭔㍖", a_));
			IL_5A6:
			throw new ArgumentNullException(RecordTableEnumerator.b("㑆㵈㥊⡌⹎㱐R⅔㙖⭘⽚", a_));
			IL_60F:
			this.ᜉ.ActiveSheetIndex = activeSheetIndex;
			this.ᜉ.DisplayedTab = displayedTab;
			xmlWriter.WriteEndElement();
			xmlWriter.Flush();
			return;
			IL_614:
			throw new XmlException(RecordTableEnumerator.b("ቆ❈⹊㕌㽎㑐げ⅔㉖㵘筚╜㉞ൠ䍢ᅤ٦๨兪䵬", a_) + A_0.LocalName);
		}
		}
	}

	// Token: 0x06004F7F RID: 20351 RVA: 0x003024D0 File Offset: 0x003014D0
	public void ᜂ()
	{
		WorksheetsCollection worksheetsCollection = this.ᜉ.Worksheets as WorksheetsCollection;
		IEnumerator<IWorksheet> enumerator = worksheetsCollection.GetEnumerator();
		try
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_B5;
				case 2:
					num = 1;
					continue;
				case 4:
				{
					if (!enumerator.MoveNext())
					{
						num = 2;
						continue;
					}
					XlsWorksheet xlsWorksheet = (XlsWorksheet)enumerator.Current;
					xlsWorksheet.DataHolder.ᜀ(xlsWorksheet);
					num = 3;
					continue;
				}
				}
				IL_6C:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num = 4;
					continue;
				}
				goto IL_6C;
			}
			IL_B5:;
		}
		finally
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					enumerator.Dispose();
					num = 2;
					continue;
				case 2:
					goto IL_EE;
				}
				if (enumerator == null)
				{
					break;
				}
				num = 1;
			}
			IL_EE:;
		}
	}

	// Token: 0x06004F80 RID: 20352 RVA: 0x003025E0 File Offset: 0x003015E0
	public void ᜀ(Dictionary<int, int> A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				if (true)
				{
				}
				ITabSheets objects = this.ᜉ.Objects;
				spr\u17FF spr_u17FF = this.ᜉ.AppImplementation;
				int num = objects.Count + 4;
				spr_u17FF.ᜀ(4L, (long)num);
				int num2 = 0;
				int count = objects.Count;
				int num3 = 3;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						return;
					case 1:
						goto IL_79;
					case 2:
						if (num2 < count)
						{
							XlsWorksheetBase xlsWorksheetBase = (XlsWorksheetBase)objects[num2];
							xlsWorksheetBase.ParseData(A_0);
							xlsWorksheetBase.IsSaved = false;
							spr_u17FF.ᜀ((long)(num2 + 4 + 1), (long)num);
							num2++;
							num3 = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_77;
						default:
							if (false)
							{
							}
							num3 = 0;
							continue;
						}
						break;
					case 3:
						goto IL_77;
					}
					break;
					IL_79:
					num3 = 2;
					continue;
					IL_77:
					goto IL_79;
				}
			}
			return;
		}
	}

	// Token: 0x06004F81 RID: 20353 RVA: 0x003026DC File Offset: 0x003016DC
	private void ᜢ(XmlReader A_0)
	{
		int a_ = 10;
		int num = 10;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!A_0.IsEmptyElement)
				{
					num = 13;
					continue;
				}
				goto IL_1C5;
			case 1:
				goto IL_112;
			case 2:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 14;
					continue;
				}
				A_0.Skip();
				num = 6;
				continue;
			case 3:
			{
				string localName;
				if (localName == RecordTableEnumerator.b("〿⭁㉃⥅㱇ॉⵋⵍ㡏㝑", a_))
				{
					num = 15;
					continue;
				}
				goto IL_7E;
			}
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
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 5;
						continue;
					}
					num = 2;
					continue;
				}
				break;
			case 5:
				goto IL_158;
			case 6:
				goto IL_112;
			case 7:
				num = 3;
				continue;
			case 8:
				goto IL_112;
			case 9:
				goto IL_112;
			case 11:
				goto IL_7C;
			case 12:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 7;
					continue;
				}
				goto IL_7E;
			}
			case 13:
				A_0.Read();
				num = 9;
				continue;
			case 14:
				if (true)
				{
				}
				num = 12;
				continue;
			case 15:
				this.ᜡ(A_0);
				num = 1;
				continue;
			}
			if (A_0.LocalName != RecordTableEnumerator.b("〿⭁㉃⥅㱇ॉⵋⵍ㡏㝑❓", a_))
			{
				num = 11;
				continue;
			}
			num = 0;
			continue;
			IL_7E:
			A_0.Skip();
			num = 8;
			continue;
			IL_112:
			num = 4;
		}
		IL_7C:
		throw new XmlException();
		IL_158:
		IL_1C5:
		A_0.Read();
	}

	// Token: 0x06004F82 RID: 20354 RVA: 0x003028B8 File Offset: 0x003018B8
	private void ᜡ(XmlReader A_0)
	{
		int a_ = 12;
		int num = 5;
		string a_2;
		string a_3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⭁⁃", a_), RecordTableEnumerator.b("⩁ぃ㉅㡇灉捋慍⍏ㅑ㱓㍕㕗㭙⽛灝ཟቡţࡥၧݩk࡭Ὧqᥳ᝵౷ॹ剻ᅽꮃ횑ﮓ鍊풟趡隣隥颧鲩莫\udcad햯\udeb1햳습톷햹튻춽ꢿꯁ듃뗅", a_)))
				{
					num = 3;
					continue;
				}
				goto IL_12B;
			case 1:
				goto IL_8D;
			case 2:
				goto IL_F4;
			case 3:
				a_2 = A_0.Value;
				num = 2;
				continue;
			case 4:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⅁╃╅⁇⽉Ջ⩍", a_)))
				{
					num = 6;
					continue;
				}
				goto IL_8F;
			case 5:
				if (true)
				{
				}
				break;
			case 6:
				a_3 = A_0.Value;
				num = 7;
				continue;
			case 7:
				goto IL_8F;
			}
			if (!(A_0.LocalName != RecordTableEnumerator.b("㉁ⵃぅ❇㹉ཋ⽍㍏㩑ㅓ", a_)))
			{
				a_3 = null;
				a_2 = null;
				num = 4;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_8D;
			default:
				if (false)
				{
				}
				num = 1;
				continue;
			}
			IL_8F:
			num = 0;
		}
		IL_8D:
		throw new XmlException();
		IL_F4:
		IL_12B:
		this.ᜉ.DataHolder.ᜁ(a_3, a_2);
		A_0.MoveToElement();
		A_0.Skip();
	}

	// Token: 0x06004F83 RID: 20355 RVA: 0x00302A10 File Offset: 0x00301A10
	private void ᜀ(XmlReader A_0, FileVersion A_1)
	{
		int a_ = 15;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				A_1.LowestEdited = (A_0.MoveToAttribute(RecordTableEnumerator.b("⥄⡆㹈⹊㹌㭎ᑐ㝒㱔⍖㱘㽚", a_)) ? A_0.Value : null);
				num = 5;
				continue;
			case 2:
				A_1.ApplicationName = (A_0.MoveToAttribute(RecordTableEnumerator.b("⑄㝆㥈Պⱌ≎㑐", a_)) ? (A_1.ApplicationName = A_0.Value) : null);
				if (true)
				{
				}
				num = 4;
				continue;
			case 3:
				goto IL_62;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					A_1.BuildVersion = (A_0.MoveToAttribute(RecordTableEnumerator.b("㝄㉆㥈ॊ㡌♎㵐㝒", a_)) ? A_0.Value : null);
					num = 1;
					continue;
				}
				break;
			case 5:
				A_1.LastEdited = (A_0.MoveToAttribute(RecordTableEnumerator.b("⥄♆㩈㽊ࡌ⭎㡐❒ご㍖", a_)) ? A_0.Value : null);
				num = 6;
				continue;
			case 6:
				goto IL_16F;
			}
			IL_3F:
			if (A_0.LocalName != RecordTableEnumerator.b("⍄⹆╈⹊ᭌ⩎⍐⁒㱔㡖㝘", a_))
			{
				num = 3;
				continue;
			}
			num = 2;
			continue;
			goto IL_3F;
		}
		IL_62:
		throw new XmlException();
		IL_16F:
		A_1.CodeName = (A_0.MoveToAttribute(RecordTableEnumerator.b("♄⡆ⵈ⹊͌⹎㱐㙒", a_)) ? A_0.Value : null);
		A_0.MoveToElement();
		A_0.Skip();
	}

	// Token: 0x06004F84 RID: 20356 RVA: 0x00302BC4 File Offset: 0x00301BC4
	private void ᜠ(XmlReader A_0)
	{
		int a_ = 6;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.LocalName != RecordTableEnumerator.b("䬻儽㈿⥁♃⥅❇ⅉ᱋㱍", a_))
				{
					num = 1;
					continue;
				}
				num = 7;
				continue;
			case 1:
				goto IL_107;
			case 2:
				goto IL_125;
			case 3:
				this.ᜉ.Date1904 = XmlConvert.ToBoolean(A_0.Value);
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_127;
				default:
					if (false)
					{
					}
					num = 4;
					continue;
				}
				break;
			case 4:
				goto IL_13B;
			case 6:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("弻儽␿❁੃❅╇⽉", a_)))
				{
					num = 9;
					continue;
				}
				goto IL_16F;
			case 7:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("堻弽㐿❁畃罅硇繉", a_)))
				{
					num = 3;
					continue;
				}
				goto IL_13B;
			case 8:
				goto IL_59;
			case 9:
				this.ᜉ.CodeName = A_0.Value;
				num = 2;
				continue;
			}
			if (A_0 == null)
			{
				num = 8;
				continue;
			}
			num = 0;
			continue;
			IL_13B:
			num = 6;
		}
		IL_59:
		goto IL_127;
		IL_107:
		throw new XmlException();
		IL_125:
		goto IL_16F;
		IL_127:
		throw new ArgumentException(RecordTableEnumerator.b("主嬽ℿ♁⅃㑅", a_));
		IL_16F:
		A_0.Skip();
	}

	// Token: 0x06004F85 RID: 20357 RVA: 0x00302D48 File Offset: 0x00301D48
	private void \u171F(XmlReader A_0)
	{
		int a_ = 10;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⌿⍁⡃╅Ň⹉", a_)))
				{
					num = 8;
					continue;
				}
				goto IL_177;
			case 1:
				if (A_0.LocalName != RecordTableEnumerator.b("⌿⍁⡃╅ᡇ㡉", a_))
				{
					num = 9;
					continue;
				}
				num = 2;
				continue;
			case 2:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("☿㝁⡃⩅ᡇ㡉⥋ⵍ㥏⅑㵓㥕㙗", a_)))
				{
					num = 5;
					continue;
				}
				goto IL_143;
			case 3:
				goto IL_59;
			case 4:
				goto IL_12D;
			case 5:
				this.ᜉ.IsDisplayPrecision = !XmlConvert.ToBoolean(A_0.Value);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_12F;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num = 7;
					continue;
				}
				break;
			case 7:
				goto IL_143;
			case 8:
				this.ᜉ.DataHolder.ᜊ(A_0.Value);
				num = 4;
				continue;
			case 9:
				goto IL_10A;
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			num = 1;
			continue;
			IL_143:
			num = 0;
		}
		IL_59:
		goto IL_12F;
		IL_10A:
		throw new XmlException();
		IL_12D:
		goto IL_177;
		IL_12F:
		throw new ArgumentException(RecordTableEnumerator.b("㈿❁╃≅ⵇ㡉", a_));
		IL_177:
		A_0.Skip();
	}

	// Token: 0x06004F86 RID: 20358 RVA: 0x00302ED4 File Offset: 0x00301ED4
	private void \u171E(XmlReader A_0)
	{
		int a_ = 17;
		int num = 9;
		ushort a_2;
		for (;;)
		{
			bool flag;
			bool flag2;
			switch (num)
			{
			case 0:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⭆♈⡊♌ᱎ═⅒⁔㑖ⵘ⹚⽜㩞", a_)))
				{
					num = 4;
					continue;
				}
				goto IL_127;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1AC;
				default:
					goto IL_85;
				}
				break;
			case 2:
				goto IL_158;
			case 3:
				goto IL_1D1;
			case 4:
				flag = XmlConvert.ToBoolean(A_0.Value);
				num = 5;
				continue;
			case 5:
				goto IL_127;
			case 6:
				if (A_0.LocalName != RecordTableEnumerator.b("う♈㥊♌ⵎ㹐㱒㹔ݖ⭘㑚⥜㩞ɠᝢ౤ࡦݨ", a_))
				{
					if (true)
					{
					}
					num = 11;
					continue;
				}
				flag = false;
				flag2 = false;
				a_2 = 0;
				num = 17;
				continue;
			case 7:
				if (!flag)
				{
					num = 8;
					continue;
				}
				goto IL_158;
			case 8:
				num = 16;
				continue;
			case 10:
				goto IL_1AC;
			case 11:
				goto IL_125;
			case 12:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⭆♈⡊♌ᡎ㡐㵒ㅔ㡖⹘⡚", a_)))
				{
					num = 14;
					continue;
				}
				goto IL_1AC;
			case 13:
				a_2 = ushort.Parse(A_0.Value, NumberStyles.HexNumber, CultureInfo.CurrentCulture);
				num = 3;
				continue;
			case 14:
				flag2 = XmlConvert.ToBoolean(A_0.Value);
				num = 10;
				continue;
			case 15:
				goto IL_170;
			case 16:
				if (flag2)
				{
					num = 2;
					continue;
				}
				goto IL_246;
			case 17:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("う♈㥊♌ⵎ㹐㱒㹔ݖ㡘⡚⹜⡞๠ᅢŤ", a_)))
				{
					num = 13;
					continue;
				}
				goto IL_1D1;
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			num = 6;
			continue;
			IL_127:
			num = 12;
			continue;
			IL_158:
			this.ᜉ.Protect(flag2, flag);
			num = 15;
			continue;
			IL_1AC:
			A_0.Read();
			num = 7;
			continue;
			IL_1D1:
			num = 0;
		}
		IL_85:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㕆ⱈ⩊⥌⩎⍐", a_));
		IL_125:
		throw new XmlException();
		IL_170:
		IL_246:
		this.ᜉ.Password.ᜀ(a_2);
	}

	// Token: 0x06004F87 RID: 20359 RVA: 0x00303138 File Offset: 0x00302138
	private List<Dictionary<string, string>> ᜀ(XmlReader A_0, out int A_1, out int A_2)
	{
		int a_ = 19;
		int num = 5;
		List<Dictionary<string, string>> list;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
			{
				Dictionary<string, string> dictionary = list[0];
				num = 1;
				continue;
			}
			case 1:
			{
				Dictionary<string, string> dictionary;
				string s;
				if (dictionary.TryGetValue(RecordTableEnumerator.b("⡈⡊㥌♎❐㙒Ŕ㙖㭘", a_), out s))
				{
					num = 8;
					continue;
				}
				goto IL_E3;
			}
			case 2:
				goto IL_E3;
			case 3:
				goto IL_1C9;
			case 4:
				if (A_0.LocalName == RecordTableEnumerator.b("㹈⑊㽌⑎㍐㱒㩔㱖མ㉚㡜⡞", a_))
				{
					num = 10;
					continue;
				}
				goto IL_1C9;
			case 6:
				goto IL_9E;
			case 7:
			{
				string s;
				A_2 = XmlConvert.ToInt32(s);
				num = 9;
				continue;
			}
			case 8:
			{
				string s;
				A_1 = XmlConvert.ToInt32(s);
				num = 2;
				continue;
			}
			case 9:
				goto IL_178;
			case 10:
			{
				Dictionary<string, string> dictionary = this.\u171D(A_0);
				list.Add(dictionary);
				num = 3;
				continue;
			}
			case 11:
			{
				Dictionary<string, string> dictionary;
				string s;
				if (dictionary.TryGetValue(RecordTableEnumerator.b("⽈≊㽌㱎═R㵔㉖㱘⽚", a_), out s))
				{
					num = 7;
					continue;
				}
				goto IL_1DF;
			}
			case 12:
				goto IL_84;
			case 13:
				goto IL_9E;
			case 14:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 0;
					continue;
				}
				num = 4;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_1CF:
				num = 13;
				continue;
			default:
				if (false)
				{
				}
				if (A_0 == null)
				{
					num = 12;
					continue;
				}
				A_0.Read();
				A_1 = 0;
				A_2 = 0;
				list = new List<Dictionary<string, string>>();
				num = 6;
				continue;
			}
			IL_9E:
			num = 14;
			continue;
			IL_E3:
			num = 11;
			continue;
			IL_1C9:
			A_0.Skip();
			goto IL_1CF;
		}
		IL_84:
		throw new ArgumentNullException(RecordTableEnumerator.b("㭈⹊ⱌ⭎㑐⅒", a_));
		IL_178:
		IL_1DF:
		A_0.Skip();
		return list;
	}

	// Token: 0x06004F88 RID: 20360 RVA: 0x00303338 File Offset: 0x00302338
	private Dictionary<string, string> \u171D(XmlReader A_0)
	{
		int a_ = 15;
		int num = 2;
		Dictionary<string, string> dictionary;
		for (;;)
		{
			int num2;
			int attributeCount;
			switch (num)
			{
			case 0:
				goto IL_9C;
			case 1:
				if (num2 >= attributeCount)
				{
					num = 4;
					continue;
				}
				A_0.MoveToAttribute(num2);
				dictionary.Add(A_0.Name, A_0.Value);
				num2++;
				num = 5;
				continue;
			case 3:
				goto IL_3C;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_7E;
				default:
					goto IL_CC;
				}
				break;
			case 5:
				goto IL_9C;
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			dictionary = new Dictionary<string, string>();
			num2 = 0;
			attributeCount = A_0.AttributeCount;
			IL_7E:
			num = 0;
			continue;
			IL_9C:
			num = 1;
		}
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("㝄≆⡈⽊⡌㵎", a_));
		IL_CC:
		if (true)
		{
		}
		if (false)
		{
		}
		return dictionary;
	}

	// Token: 0x06004F89 RID: 20361 RVA: 0x00303424 File Offset: 0x00302424
	public void ᜀ(XmlReader A_0, XlsWorksheet A_1, string A_2, ref MemoryStream A_3, ref MemoryStream A_4, List<int> A_5, Dictionary<string, object> A_6, Dictionary<int, int> A_7)
	{
		int a_ = 16;
		int num = 7;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.LocalName != RecordTableEnumerator.b("ㅅ❇㡉❋㵍㡏㝑ㅓ≕", a_))
				{
					num = 2;
					continue;
				}
				A_0.Read();
				this.\u1712 = A_1;
				this.ᜀ(A_0, A_1, A_3, A_5);
				num = 11;
				continue;
			case 1:
				if (A_1 == null)
				{
					num = 4;
					continue;
				}
				num = 6;
				continue;
			case 2:
				goto IL_237;
			case 3:
				goto IL_BE;
			case 4:
				goto IL_143;
			case 5:
				goto IL_F9;
			case 6:
				if (A_3 == null)
				{
					num = 8;
					continue;
				}
				goto IL_BE;
			case 8:
				goto IL_1BC;
			case 9:
				this.ᜀ(A_0, A_1, A_5, RecordTableEnumerator.b("╅", a_));
				num = 5;
				continue;
			case 10:
				A_1.ᜀ(A_7, new spr\u202C(A_1.ParentWorkbook.InnerSST.AddIncrease));
				num = 12;
				continue;
			case 11:
				if (A_0.LocalName == RecordTableEnumerator.b("㕅⁇⽉⥋㩍ᑏ㍑⁓㝕", a_))
				{
					num = 9;
					continue;
				}
				goto IL_F9;
			case 12:
				goto IL_182;
			case 13:
				num = 0;
				continue;
			case 14:
				if (A_7 != null)
				{
					num = 10;
					continue;
				}
				goto IL_264;
			case 15:
				goto IL_6B;
			case 16:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 13;
					continue;
				}
				A_0.Read();
				num = 3;
				continue;
			}
			if (A_0 == null)
			{
				num = 15;
				continue;
			}
			num = 1;
			continue;
			IL_BE:
			num = 16;
			continue;
			IL_F9:
			if (true)
			{
			}
			num = 14;
		}
		IL_6B:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑅ⵇ⭉⡋⭍≏", a_));
		IL_143:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕅⁇⽉⥋㩍", a_));
		IL_182:
		goto IL_264;
		IL_1BC:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_237:
			throw new XmlException(RecordTableEnumerator.b("ㅅ❇㡉❋㵍㡏㝑ㅓ≕硗⹙㵛㥝䁟ᕡգᕥ䡧ѩͫᩭ偯ᑱ᭳͵ᙷṹ剻", a_));
		default:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㕅㱇㡉⥋⽍㵏ő⁓㝕⩗⹙", a_));
		}
		IL_264:
		this.ᜀ(A_0, A_1, ref A_4, A_2, A_6);
	}

	// Token: 0x06004F8A RID: 20362 RVA: 0x003036A4 File Offset: 0x003026A4
	private void ᜀ(XmlReader A_0, XlsWorksheet A_1, Stream A_2, List<int> A_3)
	{
		int a_ = 13;
		XmlWriter xmlWriter;
		for (;;)
		{
			xmlWriter = UtilityMethods.ᜀ(A_2, Encoding.UTF8);
			xmlWriter.WriteStartElement(RecordTableEnumerator.b("ㅂ⩄⡆㵈", a_), RecordTableEnumerator.b("⭂ㅄ㍆㥈煊扌恎≐げ㵔㉖㑘㩚⹜煞๠።d०ᅨ٪Ŭ८ṰŲᡴᙶ൸ࡺ卼ၾꪄ麗力ﶔﲘ躠醢閤鞦龨蒪사캮\ud8b0\uddb2", a_));
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_24E;
				case 1:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 19;
						continue;
					}
					goto IL_24E;
				}
				case 2:
					goto IL_131;
				case 3:
					num = 0;
					continue;
				case 4:
					goto IL_131;
				case 5:
					if (!A_0.EOF)
					{
						num = 24;
						continue;
					}
					goto IL_33F;
				case 6:
					if (!(A_0.LocalName != RecordTableEnumerator.b("あⵄ≆ⱈ㽊ौ⹎═㉒", a_)))
					{
						num = 7;
						continue;
					}
					num = 1;
					continue;
				case 7:
					goto IL_1CC;
				case 8:
					num = 14;
					continue;
				case 9:
					goto IL_131;
				case 10:
					if (A_0.NodeType != XmlNodeType.EndElement)
					{
						num = 15;
						continue;
					}
					goto IL_33F;
				case 11:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("あⵄ≆ⱈ㽊ୌ⁎⍐㹒㑔⍖क़⥚", a_)))
					{
						num = 3;
						continue;
					}
					this.ᜑ(A_0, A_1);
					this.ᜐ(A_0, A_1);
					A_0.MoveToElement();
					num = 18;
					continue;
				}
				case 12:
					goto IL_131;
				case 13:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("あⵄ≆ⱈ㽊ᭌ♎㑐⑒♔", a_)))
					{
						num = 8;
						continue;
					}
					this.ᜄ(A_0, A_1);
					num = 23;
					continue;
				}
				case 14:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("❂ⱄ⩆ⱈ╊㹌♎㹐㵒", a_)))
					{
						num = 16;
						continue;
					}
					A_0.Skip();
					num = 2;
					continue;
				}
				case 15:
					num = 6;
					continue;
				case 16:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1E2;
					default:
						if (false)
						{
						}
						num = 11;
						continue;
					}
					break;
				case 17:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("⁂⩄⭆㩈", a_)))
					{
						num = 22;
						continue;
					}
					this.ᜁ(A_0, A_1, A_3);
					A_0.Read();
					goto IL_1E2;
				}
				case 18:
					goto IL_24E;
				case 19:
					num = 17;
					continue;
				case 20:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("あⵄ≆ⱈ㽊ᵌ㵎", a_)))
					{
						num = 21;
						continue;
					}
					this.ᜀ(A_0, A_1);
					num = 12;
					continue;
				}
				case 21:
					num = 13;
					continue;
				case 22:
					num = 20;
					continue;
				case 23:
					if (true)
					{
					}
					goto IL_131;
				case 24:
					num = 10;
					continue;
				case 25:
					goto IL_131;
				}
				break;
				IL_131:
				num = 5;
				continue;
				IL_1E2:
				num = 9;
				continue;
				IL_24E:
				xmlWriter.WriteNode(A_0, false);
				num = 25;
			}
		}
		IL_1CC:
		IL_33F:
		xmlWriter.WriteEndElement();
		xmlWriter.Flush();
		A_2.Position = 0L;
	}

	// Token: 0x06004F8B RID: 20363 RVA: 0x00303A04 File Offset: 0x00302A04
	private void ᜄ(XmlReader A_0, XlsWorksheetBase A_1)
	{
		int a_ = 5;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				A_0.Read();
				num = 15;
				continue;
			case 2:
				goto IL_14E;
			case 3:
				if (A_1 == null)
				{
					num = 4;
					continue;
				}
				num = 16;
				continue;
			case 4:
				goto IL_107;
			case 5:
				goto IL_12E;
			case 6:
				this.ᜃ(A_0, A_1);
				num = 13;
				continue;
			case 7:
				goto IL_6B;
			case 8:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 9;
					continue;
				}
				goto IL_CE;
			}
			case 9:
				num = 11;
				continue;
			case 10:
				goto IL_200;
			case 11:
			{
				string localName;
				if (localName == RecordTableEnumerator.b("䠺唼娾⑀㝂ፄ⹆ⱈ㱊", a_))
				{
					num = 6;
					continue;
				}
				goto IL_CE;
			}
			case 12:
				if (!A_0.IsEmptyElement)
				{
					num = 0;
					continue;
				}
				goto IL_205;
			case 13:
				goto IL_12E;
			case 14:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 2;
					continue;
				}
				num = 8;
				continue;
			case 15:
				goto IL_12E;
			case 16:
				if (A_0.LocalName != RecordTableEnumerator.b("䠺唼娾⑀㝂ፄ⹆ⱈ㱊㹌", a_))
				{
					num = 10;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_14E;
				default:
					if (false)
					{
					}
					num = 12;
					continue;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 7;
				continue;
			}
			num = 3;
			continue;
			IL_CE:
			A_0.Skip();
			num = 5;
			continue;
			IL_12E:
			num = 14;
		}
		IL_6B:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䤺堼帾╀♂㝄", a_));
		IL_107:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠺唼娾⑀㝂", a_));
		IL_14E:
		goto IL_205;
		IL_200:
		throw new XmlException(RecordTableEnumerator.b("氺似倾⽀⑂敄㽆⑈❊浌㭎ぐ㑒", a_));
		IL_205:
		A_0.Read();
	}

	// Token: 0x06004F8C RID: 20364 RVA: 0x00303C20 File Offset: 0x00302C20
	private void ᜃ(XmlReader A_0, XlsWorksheetBase A_1)
	{
		int a_ = 1;
		int num = 15;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				string value;
				if (!(value == RecordTableEnumerator.b("䜶堸尺堼紾㍀♂⑄ⱆ᥈㥊⡌㥎㡐㙒≔", a_)))
				{
					num = 61;
					continue;
				}
				XlsWorksheet xlsWorksheet;
				xlsWorksheet.ViewMode = ViewMode.Preview;
				xlsWorksheet.WindowTwo.ᜅ(true);
				num = 46;
				continue;
			}
			case 1:
			{
				string value;
				if (!(value == RecordTableEnumerator.b("䜶堸尺堼猾⁀㩂⩄㉆㵈", a_)))
				{
					num = 14;
					continue;
				}
				XlsWorksheet xlsWorksheet;
				xlsWorksheet.ViewMode = ViewMode.Layout;
				num = 73;
				continue;
			}
			case 2:
			{
				string value;
				if ((value = A_0.Value) != null)
				{
					num = 64;
					continue;
				}
				goto IL_647;
			}
			case 3:
				goto IL_1BB;
			case 4:
				num = 2;
				continue;
			case 5:
				goto IL_2B2;
			case 6:
			{
				XlsWorksheet xlsWorksheet;
				xlsWorksheet.GridLinesVisible = XmlConvert.ToBoolean(A_0.Value);
				num = 43;
				continue;
			}
			case 7:
				goto IL_7A1;
			case 8:
				A_1.Select();
				num = 3;
				continue;
			case 9:
				num = 70;
				continue;
			case 10:
				goto IL_2C8;
			case 11:
			{
				XlsWorksheet xlsWorksheet;
				xlsWorksheet.IsDisplayZeros = XmlConvert.ToBoolean(A_0.Value);
				num = 50;
				continue;
			}
			case 12:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䔶倸尺唼䬾ᕀⱂॄ≆⽈㽊", a_)))
				{
					num = 63;
					continue;
				}
				goto IL_52E;
			case 13:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䴶嘸吺值款⹀Ղⱄ㍆", a_)))
				{
					num = 17;
					continue;
				}
				goto IL_7EC;
			case 14:
				num = 0;
				continue;
			case 16:
				goto IL_57D;
			case 17:
				goto IL_7A6;
			case 18:
			{
				XlsWorksheet xlsWorksheet;
				xlsWorksheet.WindowTwo.ᜃ(XmlConvert.ToBoolean(A_0.Value));
				num = 57;
				continue;
			}
			case 19:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䴶嘸吺值氾≀≂⥄≆݈⑊㽌≎ぐ㽒", a_)))
				{
					num = 35;
					continue;
				}
				goto IL_441;
			case 20:
				goto IL_683;
			case 21:
				goto IL_15D;
			case 22:
			{
				XlsWorksheet xlsWorksheet;
				xlsWorksheet.GridLineColor = (ExcelColors)XmlConvert.ToInt32(A_0.Value);
				num = 69;
				continue;
			}
			case 23:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䌶堸夺渼娾ⵀ♂♄㍆ⱈ⽊", a_)))
				{
					if (true)
					{
					}
					num = 31;
					continue;
				}
				goto IL_1BB;
			case 24:
				if (!A_0.IsEmptyElement)
				{
					num = 39;
					continue;
				}
				goto IL_949;
			case 25:
			{
				XlsWorksheet xlsWorksheet;
				xlsWorksheet.ZoomScalePageLayoutView = XmlConvert.ToInt32(A_0.Value);
				num = 20;
				continue;
			}
			case 26:
				goto IL_85F;
			case 27:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䴶嘸吺值氾≀≂⥄≆᥈⩊⩌⩎ᵐ㉒ⱔ㡖ⱘ⽚ଡ଼㙞Ѡᑢ", a_)))
				{
					num = 25;
					continue;
				}
				goto IL_683;
			case 28:
				goto IL_85F;
			case 29:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_7A6;
				default:
					if (false)
					{
					}
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䐶儸吺䨼派⹀㑂ل⡆╈͊⡌⹎㕐㙒❔⑖", a_)))
					{
						num = 48;
						continue;
					}
					goto IL_375;
				}
				break;
			case 30:
				goto IL_85F;
			case 31:
				num = 56;
				continue;
			case 32:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("吶嘸场刼䴾ࡀ❂", a_)))
				{
					num = 22;
					continue;
				}
				goto IL_229;
			case 33:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䄶倸帺䨼", a_)))
				{
					num = 4;
					continue;
				}
				goto IL_647;
			case 34:
				goto IL_87F;
			case 35:
			{
				XlsWorksheet xlsWorksheet;
				xlsWorksheet.ZoomScaleNormal = XmlConvert.ToInt32(A_0.Value);
				num = 74;
				continue;
			}
			case 36:
				num = 75;
				continue;
			case 37:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("匶尸崺尼䨾ⵀ㝂Ʉ㕆⁈⽊์⁎㵐㱒❔", a_)))
				{
					num = 18;
					continue;
				}
				goto IL_5B1;
			case 38:
				A_1.Zoom = XmlConvert.ToInt32(A_0.Value);
				num = 16;
				continue;
			case 39:
				A_0.Read();
				num = 26;
				continue;
			case 40:
				goto IL_647;
			case 41:
			{
				XlsWorksheet xlsWorksheet;
				xlsWorksheet.TopLeftCell = (xlsWorksheet[A_0.Value] as CellRange);
				num = 67;
				continue;
			}
			case 42:
			{
				XlsWorksheet xlsWorksheet;
				xlsWorksheet.ZoomScalePageBreakView = XmlConvert.ToInt32(A_0.Value);
				num = 10;
				continue;
			}
			case 43:
				goto IL_344;
			case 44:
				if (A_1 == null)
				{
					num = 47;
					continue;
				}
				num = 49;
				continue;
			case 45:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䴶嘸吺值氾≀≂⥄≆ᩈ⍊⡌⩎═ὒ㑔⹖㙘⹚⥜फ़ࡠ٢ቤ", a_)))
				{
					num = 42;
					continue;
				}
				goto IL_2C8;
			case 46:
				goto IL_647;
			case 47:
				goto IL_601;
			case 48:
			{
				XlsWorksheet xlsWorksheet;
				xlsWorksheet.RowColumnHeadersVisible = XmlConvert.ToBoolean(A_0.Value);
				num = 55;
				continue;
			}
			case 49:
			{
				if (A_0.LocalName != RecordTableEnumerator.b("䐶儸帺堼䬾ᝀ⩂⁄う", a_))
				{
					num = 7;
					continue;
				}
				XlsWorksheet xlsWorksheet = A_1 as XlsWorksheet;
				num = 51;
				continue;
			}
			case 50:
				goto IL_4C7;
			case 51:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䐶儸吺䨼砾㍀⩂⅄୆⁈╊⡌㱎", a_)))
				{
					num = 6;
					continue;
				}
				goto IL_344;
			case 52:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䴶嘸吺值氾≀≂⥄≆", a_)))
				{
					num = 38;
					continue;
				}
				goto IL_57D;
			case 53:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䌶嘸䬺焼娾❀㝂ل≆╈❊", a_)))
				{
					num = 41;
					continue;
				}
				goto IL_310;
			case 54:
				num = 5;
				continue;
			case 55:
				goto IL_375;
			case 56:
				if (A_0.Value != RecordTableEnumerator.b("ܶ", a_))
				{
					num = 8;
					continue;
				}
				goto IL_1BB;
			case 57:
				goto IL_5B1;
			case 58:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 34;
					continue;
				}
				num = 59;
				continue;
			case 59:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 9;
					continue;
				}
				goto IL_2B2;
			}
			case 60:
				goto IL_85F;
			case 61:
				num = 65;
				continue;
			case 62:
				goto IL_7EC;
			case 63:
			{
				XlsWorksheet xlsWorksheet;
				xlsWorksheet.IsRightToLeft = XmlConvert.ToBoolean(A_0.Value);
				num = 66;
				continue;
			}
			case 64:
				num = 1;
				continue;
			case 65:
			{
				string value;
				if (!(value == RecordTableEnumerator.b("夶嘸䤺值帾ⵀ", a_)))
				{
					num = 68;
					continue;
				}
				XlsWorksheet xlsWorksheet;
				xlsWorksheet.ViewMode = ViewMode.Normal;
				num = 40;
				continue;
			}
			case 66:
				goto IL_52E;
			case 67:
				goto IL_310;
			case 68:
				num = 71;
				continue;
			case 69:
				goto IL_229;
			case 70:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("䜶堸唺堼", a_)))
				{
					num = 36;
					continue;
				}
				XlsWorksheet xlsWorksheet;
				this.\u1712(A_0, xlsWorksheet);
				num = 60;
				continue;
			}
			case 71:
				goto IL_647;
			case 72:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䐶儸吺䨼放⑀ㅂ⩄㑆", a_)))
				{
					num = 11;
					continue;
				}
				goto IL_4C7;
			case 73:
				goto IL_647;
			case 74:
				goto IL_441;
			case 75:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("䐶尸场堼尾㕀⩂⩄⥆", a_)))
				{
					num = 54;
					continue;
				}
				XlsWorksheet xlsWorksheet;
				this.\u1713(A_0, xlsWorksheet);
				num = 28;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 21;
				continue;
			}
			num = 44;
			continue;
			IL_1BB:
			A_0.MoveToElement();
			num = 24;
			continue;
			IL_229:
			num = 33;
			continue;
			IL_2B2:
			A_0.Skip();
			num = 30;
			continue;
			IL_2C8:
			num = 27;
			continue;
			IL_310:
			num = 72;
			continue;
			IL_344:
			num = 53;
			continue;
			IL_375:
			num = 12;
			continue;
			IL_441:
			num = 45;
			continue;
			IL_4C7:
			num = 29;
			continue;
			IL_52E:
			num = 52;
			continue;
			IL_57D:
			num = 19;
			continue;
			IL_5B1:
			num = 32;
			continue;
			IL_647:
			num = 23;
			continue;
			IL_683:
			num = 13;
			continue;
			IL_7A6:
			(A_1 as XlsChart).ZoomToFit = XmlConvert.ToBoolean(A_0.Value);
			num = 62;
			continue;
			IL_7EC:
			num = 37;
			continue;
			IL_85F:
			num = 58;
		}
		IL_15D:
		throw new ArgumentNullException(RecordTableEnumerator.b("䔶尸娺夼娾㍀", a_));
		IL_601:
		throw new ArgumentNullException(RecordTableEnumerator.b("䐶儸帺堼䬾", a_));
		IL_7A1:
		throw new XmlException(RecordTableEnumerator.b("怶䬸吺匼堾慀㭂⡄⭆楈㽊ⱌ⡎", a_));
		IL_87F:
		IL_949:
		A_0.Read();
	}

	// Token: 0x06004F8D RID: 20365 RVA: 0x00304580 File Offset: 0x00303580
	private void \u1713(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 2;
		int num = 8;
		for (;;)
		{
			switch (num)
			{
			case 0:
				A_1.Pane.ᜀ((ushort)this.\u170D(A_0.Value));
				num = 4;
				continue;
			case 1:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䠷嬹刻嬽", a_)))
				{
					num = 0;
					continue;
				}
				goto IL_E4;
			case 2:
			{
				string value = A_0.Value;
				A_1.SetActiveCell(A_1.Range[value], false);
				num = 9;
				continue;
			}
			case 3:
				if (A_0.LocalName != RecordTableEnumerator.b("䬷弹倻嬽⌿㙁ⵃ⥅♇", a_))
				{
					num = 6;
					continue;
				}
				goto IL_189;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_189;
				default:
					if (false)
					{
					}
					goto IL_E4;
				}
				break;
			case 5:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("夷夹䠻圽㘿❁݃⍅⑇♉", a_)))
				{
					num = 2;
					continue;
				}
				goto IL_1B7;
			case 6:
				goto IL_DF;
			case 7:
				if (A_1 == null)
				{
					num = 10;
					continue;
				}
				num = 1;
				continue;
			case 9:
				goto IL_17F;
			case 10:
				goto IL_1AC;
			case 11:
				goto IL_54;
			}
			if (A_0 == null)
			{
				num = 11;
				continue;
			}
			num = 3;
			continue;
			IL_E4:
			num = 5;
			continue;
			IL_189:
			num = 7;
		}
		IL_54:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹崻娽┿ぁ", a_));
		IL_DF:
		throw new XmlException();
		IL_17F:
		if (true)
		{
		}
		goto IL_1B7;
		IL_1AC:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬷刹夻嬽㐿", a_));
		IL_1B7:
		A_0.MoveToElement();
		A_0.Skip();
	}

	// Token: 0x06004F8E RID: 20366 RVA: 0x00304754 File Offset: 0x00303754
	private sprᱭ.ActivePane \u170D(string A_0)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return sprᱭ.ᜌ[A_0];
	}

	// Token: 0x06004F8F RID: 20367 RVA: 0x0030479C File Offset: 0x0030379C
	private void \u1712(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 5;
		int num = 11;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_1E0;
			case 1:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("娺帼䬾⡀㕂⁄ᝆ⡈╊⡌", a_)))
				{
					num = 12;
					continue;
				}
				goto IL_1E0;
			case 2:
				if (A_1 == null)
				{
					num = 5;
					continue;
				}
				num = 6;
				continue;
			case 3:
				goto IL_2A6;
			case 4:
				goto IL_131;
			case 5:
				goto IL_125;
			case 6:
				if (A_0.LocalName != RecordTableEnumerator.b("䬺尼儾⑀", a_))
				{
					goto IL_1D3;
				}
				num = 14;
				continue;
			case 7:
				goto IL_1DE;
			case 8:
				A_1.VerticalSplit = XmlConvert.ToInt32(A_0.Value);
				num = 3;
				continue;
			case 9:
				goto IL_214;
			case 10:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("伺刼伾ീ♂⍄㍆ੈ⹊⅌⍎", a_)))
				{
					num = 17;
					continue;
				}
				goto IL_214;
			case 12:
			{
				sprᱭ.ActivePane activePane = (sprᱭ.ActivePane)Enum.Parse(typeof(sprᱭ.ActivePane), A_0.Value, false);
				A_1.ActivePane = (int)activePane;
				num = 0;
				continue;
			}
			case 13:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䈺渼伾ⵀ⩂ㅄ", a_)))
				{
					num = 16;
					continue;
				}
				goto IL_131;
			case 14:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䌺渼伾ⵀ⩂ㅄ", a_)))
				{
					num = 8;
					continue;
				}
				goto IL_2A6;
			case 15:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䠺䤼帾㕀♂", a_)))
				{
					num = 19;
					continue;
				}
				return;
			case 16:
				A_1.HorizontalSplit = XmlConvert.ToInt32(A_0.Value);
				num = 4;
				continue;
			case 17:
			{
				string value = A_0.Value;
				A_1.PaneFirstVisible = A_1[value];
				num = 9;
				continue;
			}
			case 18:
				goto IL_7B;
			case 19:
			{
				sprṫ a_2 = A_1.WindowTwo;
				this.ᜀ(a_2, A_0.Value);
				num = 20;
				continue;
			}
			case 20:
				goto IL_184;
			}
			if (A_0 == null)
			{
				num = 18;
				continue;
			}
			num = 2;
			continue;
			IL_131:
			num = 10;
			continue;
			IL_1D3:
			num = 7;
			continue;
			IL_2A6:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_1D3;
			default:
				if (false)
				{
				}
				num = 13;
				continue;
			}
			IL_1E0:
			num = 15;
			continue;
			IL_214:
			num = 1;
		}
		IL_7B:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䤺堼帾╀♂㝄", a_));
		IL_125:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠺唼娾⑀㝂", a_));
		IL_184:
		return;
		IL_1DE:
		throw new XmlException(RecordTableEnumerator.b("氺似倾⽀⑂敄㽆⑈❊浌㭎ぐ㑒", a_));
	}

	// Token: 0x06004F90 RID: 20368 RVA: 0x00304AA0 File Offset: 0x00303AA0
	private void ᜀ(sprṫ A_0, string A_1)
	{
		int a_ = 11;
		int num = 9;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 6;
				continue;
			case 1:
				goto IL_D0;
			case 2:
				goto IL_58;
			case 3:
				goto IL_65;
			case 4:
				if (true)
				{
				}
				num = 12;
				continue;
			case 5:
				if (A_1 != null)
				{
					num = 4;
					continue;
				}
				goto IL_1BB;
			case 6:
				if (A_1 == RecordTableEnumerator.b("❀ㅂ⩄㵆ⱈ╊Ṍ㽎㵐㩒⅔", a_))
				{
					goto IL_D2;
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
					num = 10;
					continue;
				}
				break;
			case 7:
				if (!(A_1 == RecordTableEnumerator.b("㉀㍂⥄⹆㵈", a_)))
				{
					num = 8;
					continue;
				}
				goto IL_104;
			case 8:
				num = 3;
				continue;
			case 10:
				num = 7;
				continue;
			case 11:
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				num = 5;
				continue;
			case 12:
				if (!(A_1 == RecordTableEnumerator.b("❀ㅂ⩄㵆ⱈ╊", a_)))
				{
					num = 0;
					continue;
				}
				goto IL_F5;
			}
			if (A_0 == null)
			{
				num = 2;
			}
			else
			{
				num = 11;
			}
		}
		IL_58:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙀⩂⭄⍆♈㱊᥌㡎㹐", a_));
		IL_65:
		goto IL_1BB;
		IL_D0:
		throw new ArgumentNullException(RecordTableEnumerator.b("㉀㝂⑄㍆ⱈ", a_));
		IL_D2:
		A_0.ᜊ(true);
		A_0.ᜈ(false);
		return;
		IL_F5:
		A_0.ᜊ(true);
		A_0.ᜈ(true);
		return;
		IL_104:
		A_0.ᜊ(false);
		A_0.ᜈ(false);
		return;
		IL_1BB:
		throw new XmlException();
	}

	// Token: 0x06004F91 RID: 20369 RVA: 0x00304C70 File Offset: 0x00303C70
	public void ᜁ(XmlReader A_0, XlsChart A_1)
	{
		int a_ = 13;
		switch (0)
		{
		default:
		{
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_246;
				case 1:
					goto IL_246;
				case 2:
					goto IL_246;
				case 3:
					goto IL_246;
				case 4:
					goto IL_4FF;
				case 5:
					goto IL_246;
				case 6:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 14;
						continue;
					}
					goto IL_1B0;
				}
				case 7:
					goto IL_14F;
				case 8:
					goto IL_252;
				case 9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_252;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 10:
					num = 6;
					continue;
				case 11:
					if (spr\u22D2.\u177C == null)
					{
						num = 17;
						continue;
					}
					goto IL_43A;
				case 12:
					num = 24;
					continue;
				case 13:
				{
					string localName;
					int num2;
					if (spr\u22D2.\u177C.TryGetValue(localName, out num2))
					{
						num = 12;
						continue;
					}
					goto IL_1B0;
				}
				case 14:
					num = 11;
					continue;
				case 15:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 10;
						continue;
					}
					A_0.Skip();
					if (true)
					{
					}
					num = 28;
					continue;
				case 16:
					num = 27;
					continue;
				case 17:
					spr\u22D2.\u177C = new Dictionary<string, int>(9)
					{
						{
							RecordTableEnumerator.b("あⵄ≆ⱈ㽊ᵌ㵎", a_),
							0
						},
						{
							RecordTableEnumerator.b("あⵄ≆ⱈ㽊ᭌ♎㑐⑒♔", a_),
							1
						},
						{
							RecordTableEnumerator.b("㍂⑄⁆ⱈيⱌ㵎㙐㩒㭔⑖", a_),
							2
						},
						{
							RecordTableEnumerator.b("㍂⑄⁆ⱈᡊ⡌㭎⑐⍒", a_),
							3
						},
						{
							RecordTableEnumerator.b("⭂⁄♆ⵈ⹊㽌ॎ㹐㱒⅔㉖⭘", a_),
							4
						},
						{
							RecordTableEnumerator.b("❂㝄♆㹈≊⍌⡎", a_),
							5
						},
						{
							RecordTableEnumerator.b("⽂⁄⁆⡈⡊㑌୎⍐㉒≔㹖㝘㱚", a_),
							6
						},
						{
							RecordTableEnumerator.b("⽂⁄⁆⡈⡊㑌୎⍐㉒≔㹖㝘㱚ᕜᥞ", a_),
							7
						},
						{
							RecordTableEnumerator.b("あⵄ≆ⱈ㽊ᵌ㵎㹐❒ご㑖ⵘ㉚㉜ㅞ", a_),
							8
						}
					};
					num = 19;
					continue;
				case 18:
					return;
				case 19:
					goto IL_43A;
				case 20:
					goto IL_246;
				case 21:
					num = 22;
					continue;
				case 22:
				{
					if (A_0.LocalName != RecordTableEnumerator.b("⁂ⵄ♆㭈㽊㹌❎㑐㙒⅔", a_))
					{
						num = 7;
						continue;
					}
					A_0.Read();
					XlsPageSetupBase pageSetupBase = A_1.PageSetupBase;
					pageSetupBase.IsSettingsNotValid = true;
					num = 32;
					continue;
				}
				case 23:
					goto IL_246;
				case 24:
				{
					int num2;
					switch (num2)
					{
					case 0:
						this.ᜀ(A_0, A_1);
						num = 3;
						continue;
					case 1:
						this.ᜄ(A_0, A_1);
						num = 26;
						continue;
					case 2:
					{
						XlsPageSetupBase pageSetupBase;
						bool isSettingsNotValid = pageSetupBase.IsSettingsNotValid;
						spr\u2306.ᜀ(A_0, A_1.PageSetup, new spr\u1CDC());
						pageSetupBase.IsSettingsNotValid = isSettingsNotValid;
						num = 0;
						continue;
					}
					case 3:
						spr\u2306.ᜁ(A_0, A_1.PageSetupBase);
						num = 20;
						continue;
					case 4:
					{
						XlsPageSetupBase pageSetupBase;
						bool isSettingsNotValid = pageSetupBase.IsSettingsNotValid;
						spr\u2306.ᜀ(A_0, A_1.PageSetupBase);
						pageSetupBase.IsSettingsNotValid = isSettingsNotValid;
						num = 25;
						continue;
					}
					case 5:
						this.ᜀ(A_0, A_1);
						num = 2;
						continue;
					case 6:
						this.ᜁ(A_0, A_1);
						num = 23;
						continue;
					case 7:
						spr\u2306.ᜀ(A_0, A_1, null);
						num = 31;
						continue;
					case 8:
						this.ᜀ(A_0, A_1, RecordTableEnumerator.b("⁂⩄⥆㵈⹊⍌㭎", a_));
						num = 5;
						continue;
					default:
						num = 16;
						continue;
					}
					break;
				}
				case 25:
					goto IL_246;
				case 26:
					goto IL_246;
				case 27:
					goto IL_1B0;
				case 28:
					goto IL_246;
				case 29:
					if (A_1 == null)
					{
						num = 4;
						continue;
					}
					goto IL_2B7;
				case 30:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 21;
						continue;
					}
					A_0.Read();
					num = 34;
					continue;
				case 31:
					goto IL_246;
				case 32:
					goto IL_246;
				case 33:
					goto IL_E6;
				case 34:
					goto IL_2B7;
				}
				if (A_0 == null)
				{
					num = 33;
					continue;
				}
				num = 29;
				continue;
				IL_1B0:
				A_0.Skip();
				num = 1;
				continue;
				IL_246:
				num = 8;
				continue;
				IL_252:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 18;
					continue;
				}
				num = 15;
				continue;
				IL_2B7:
				num = 30;
				continue;
				IL_43A:
				num = 13;
			}
			IL_E6:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄♆ⵈ⹊㽌", a_));
			IL_14F:
			throw new XmlException(RecordTableEnumerator.b("ᙂ⭄≆ㅈ㭊⡌ⱎ═㙒ㅔ睖⅘㙚ㅜ罞ᕠɢɤ䥦", a_));
			IL_4FF:
			throw new ArgumentNullException(RecordTableEnumerator.b("⁂ⵄ♆㭈㽊", a_));
		}
		}
	}

	// Token: 0x06004F92 RID: 20370 RVA: 0x003051C8 File Offset: 0x003041C8
	private void ᜀ(XmlReader A_0, XlsChart A_1)
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			int num = 18;
			XmlReader xmlReader;
			sprវ sprវ;
			string text;
			RelationsCollection a_4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!(xmlReader.LocalName != RecordTableEnumerator.b("堺唼帾㍀㝂", a_)))
					{
						num = 9;
						continue;
					}
					num = 10;
					continue;
				case 1:
				{
					if (!A_0.MoveToAttribute(RecordTableEnumerator.b("刺夼", a_), RecordTableEnumerator.b("区䤼䬾ㅀ祂橄框㩈⡊╌⩎㱐㉒♔祖㙘⭚㡜ㅞᥠ๢।Ŧ٨ᥪl๮հr孴ᡶ୸ᱺ剼ၾ쾊ﺒ練뒚꾜꾞醠関誤햦첨잪첬\udbae\ud8b0\udcb2\udbb4쒶톸튺춼첾", a_)))
					{
						num = 13;
						continue;
					}
					string value = A_0.Value;
					sprᦨ sprᦨ = A_1.ᜠ.ᜇ()[value];
					A_1.ᜠ.ᜇ().Remove(value);
					goto IL_307;
				}
				case 2:
				{
					Size size = this.\u171C(xmlReader);
					A_1.Width = (double)size.Width;
					A_1.Height = (double)size.Height;
					num = 6;
					continue;
				}
				case 3:
					if (A_1 == null)
					{
						num = 7;
						continue;
					}
					num = 15;
					continue;
				case 4:
					goto IL_84;
				case 5:
				{
					sprᦨ sprᦨ;
					if (sprᦨ == null)
					{
						num = 11;
						continue;
					}
					sprᦨ.ᜂ();
					sprវ = A_1.ᜠ.ᜋ();
					text = A_1.ᜠ.ᜉ().ᜇ();
					string a_2;
					sprវ.ᜀ(text, out a_2);
					xmlReader = sprវ.ᜂ(sprᦨ, a_2, out text);
					string a_3 = sprវ.ᜁ(text);
					a_4 = sprវ.ᜇ(a_3);
					num = 16;
					continue;
				}
				case 6:
					goto IL_19E;
				case 7:
					goto IL_140;
				case 8:
					goto IL_19E;
				case 9:
					goto IL_1D5;
				case 10:
					if (xmlReader.NodeType != XmlNodeType.Element)
					{
						goto IL_222;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_307;
					default:
						if (false)
						{
						}
						num = 12;
						continue;
					}
					break;
				case 11:
					goto IL_325;
				case 12:
					num = 14;
					continue;
				case 13:
					goto IL_2D8;
				case 14:
					if (xmlReader.LocalName == RecordTableEnumerator.b("娺弼䰾⹀⽂い㍆ⱈ੊⍌ⱎ㥐㱒❔", a_))
					{
						num = 2;
						continue;
					}
					goto IL_222;
				case 15:
					if (A_0.LocalName != RecordTableEnumerator.b("强似帾㙀⩂⭄⁆", a_))
					{
						num = 17;
						continue;
					}
					num = 1;
					continue;
				case 16:
					goto IL_19E;
				case 17:
					goto IL_220;
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				num = 3;
				continue;
				IL_19E:
				num = 0;
				continue;
				IL_222:
				xmlReader.Read();
				num = 8;
				continue;
				IL_307:
				num = 5;
			}
			IL_84:
			throw new ArgumentNullException(RecordTableEnumerator.b("䤺堼帾╀♂㝄", a_));
			IL_140:
			throw new ArgumentNullException(RecordTableEnumerator.b("堺唼帾㍀㝂", a_));
			IL_1D5:
			this.ᜀ(xmlReader, A_1, a_4, sprវ, text);
			return;
			IL_220:
			throw new XmlException(RecordTableEnumerator.b("渺匼娾㥀㍂⁄⑆㵈⹊⥌潎⥐㹒㥔睖ⵘ㩚㩜煞", a_));
			IL_2D8:
			throw new XmlException();
			IL_325:
			if (true)
			{
			}
			throw new XmlException();
		}
		}
	}

	// Token: 0x06004F93 RID: 20371 RVA: 0x00305510 File Offset: 0x00304510
	private Size \u171C(XmlReader A_0)
	{
		int a_ = 18;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!(A_0.LocalName != RecordTableEnumerator.b("ⵇ㉉㡋", a_)))
				{
					num = 3;
					continue;
				}
				goto IL_58;
			case 1:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_58;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 3:
				goto IL_56;
			}
			IL_2B:
			num = 0;
			continue;
			goto IL_2B;
			IL_58:
			A_0.Read();
			num = 1;
		}
		IL_56:
		return this.\u1719(A_0);
	}

	// Token: 0x06004F94 RID: 20372 RVA: 0x003055C0 File Offset: 0x003045C0
	private void ᜀ(XmlReader A_0, XlsChart A_1, RelationsCollection A_2, sprវ A_3, string A_4)
	{
		int a_ = 7;
		switch (0)
		{
		default:
		{
			int num = 7;
			RelationsCollection relationsCollection;
			string value;
			XmlReader reader;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_13D;
				case 1:
				{
					IEnumerator enumerator = relationsCollection.GetEnumerator();
					num = 4;
					continue;
				}
				case 2:
					A_1.Relations.ItemPath = relationsCollection.ItemPath;
					num = 6;
					continue;
				case 3:
					goto IL_7C;
				case 4:
					goto IL_7C;
				case 5:
				{
					IEnumerator enumerator;
					if (!enumerator.MoveNext())
					{
						num = 2;
						continue;
					}
					object obj = enumerator.Current;
					KeyValuePair<string, sprᦨ> keyValuePair = (KeyValuePair<string, sprᦨ>)obj;
					A_1.Relations[keyValuePair.Key] = keyValuePair.Value;
					num = 3;
					continue;
				}
				case 6:
					goto IL_D7;
				case 8:
					goto IL_7A;
				}
				if (!A_0.MoveToAttribute(RecordTableEnumerator.b("吼嬾", a_), RecordTableEnumerator.b("唼䬾㕀㍂罄框晈㡊⹌❎㑐㹒㑔⑖睘㑚ⵜ㩞འ᭢ࡤ୦ཨѪὬɮၰݲٴ奶ᙸॺ᩼偾즌늜궞醠鎢鎤袦\udba8캪솬캮얰\udab2\udab4\ud9b6쪸펺풼쾾닀", a_)))
				{
					num = 8;
					continue;
				}
				value = A_0.Value;
				sprᦨ a_2 = A_2[value];
				string a_3;
				sprវ.ᜀ(A_4, out a_3);
				string a_4;
				reader = A_3.ᜂ(a_2, a_3, out a_4);
				string a_5 = sprវ.ᜁ(a_4);
				relationsCollection = A_3.ᜇ(a_5);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_13D;
				default:
					if (false)
					{
					}
					num = 0;
					continue;
				}
				IL_7C:
				num = 5;
				continue;
				IL_13D:
				if (relationsCollection == null)
				{
					goto IL_196;
				}
				if (true)
				{
				}
				num = 1;
			}
			IL_7A:
			throw new XmlException();
			IL_D7:
			IL_196:
			ChartParser chartParser = new ChartParser(this.ᜉ);
			chartParser.ParseChart(reader, A_1, relationsCollection);
			A_3.\u1714().ᜀ(A_4);
			A_2.Remove(value);
			return;
		}
		}
	}

	// Token: 0x06004F95 RID: 20373 RVA: 0x0030579C File Offset: 0x0030479C
	private void ᜑ(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 8;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_C4;
			case 1:
				A_1.DefaultRowHeight = XmlConvert.ToDouble(A_0.Value);
				num = 8;
				continue;
			case 2:
				A_1.BaseColumnWidth = (int)XmlConvert.ToInt16(A_0.Value);
				num = 6;
				continue;
			case 3:
				goto IL_168;
			case 4:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("娽┿⑁╃㍅⑇㹉ṋ⅍❏ᩑㅓ㽕㽗㉙⡛", a_)))
				{
					num = 1;
					continue;
				}
				goto IL_285;
			case 6:
				goto IL_230;
			case 7:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("尽ℿㅁ⅃Յ❇♉ᭋ❍㑏♑㱓", a_)))
				{
					num = 2;
					continue;
				}
				goto IL_230;
			case 8:
				goto IL_285;
			case 9:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("儽㔿㙁⡃⽅♇⽉K⭍♏㝑㡓ᕕ㝗㙙", a_)))
				{
					num = 17;
					continue;
				}
				goto IL_C4;
			case 10:
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				num = 4;
				continue;
			case 11:
				goto IL_280;
			case 12:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䨽⠿⭁❃ⵅ᱇╉㱋", a_)))
				{
					num = 13;
					continue;
				}
				goto IL_352;
			case 13:
				A_1.IsThickTop = XmlConvert.ToBoolean(A_0.Value);
				num = 11;
				continue;
			case 14:
				A_1.OutlineLevelRow = XmlConvert.ToByte(A_0.Value);
				num = 18;
				continue;
			case 15:
				A_1.CustomHeight = XmlConvert.ToBoolean(A_0.Value);
				num = 16;
				continue;
			case 16:
				goto IL_1C2;
			case 17:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_254;
				default:
					if (false)
					{
					}
					A_1.OutlineLevelColumn = XmlConvert.ToByte(A_0.Value);
					num = 0;
					continue;
				}
				break;
			case 18:
				goto IL_90;
			case 19:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("儽㔿㙁⡃⽅♇⽉K⭍♏㝑㡓ѕ㝗ⵙ", a_)))
				{
					num = 14;
					continue;
				}
				goto IL_90;
			case 20:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("崽㔿ㅁぃ⥅╇ɉ⥋❍㝏㩑⁓", a_)))
				{
					num = 15;
					continue;
				}
				goto IL_1C2;
			case 21:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䨽⠿⭁❃ⵅੇ╉㡋㩍㽏㽑", a_)))
				{
					goto IL_254;
				}
				goto IL_18E;
			case 22:
				A_1.IsThickBottom = XmlConvert.ToBoolean(A_0.Value);
				num = 24;
				continue;
			case 23:
				goto IL_8B;
			case 24:
				goto IL_18E;
			}
			if (A_0 == null)
			{
				num = 23;
				continue;
			}
			num = 10;
			continue;
			IL_90:
			num = 7;
			continue;
			IL_C4:
			if (true)
			{
			}
			num = 19;
			continue;
			IL_18E:
			num = 12;
			continue;
			IL_1C2:
			num = 9;
			continue;
			IL_230:
			num = 21;
			continue;
			IL_254:
			num = 22;
			continue;
			IL_285:
			A_1.CustomHeight = false;
			num = 20;
		}
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰽┿⍁⁃⍅㩇", a_));
		IL_168:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴽⠿❁⅃㉅", a_));
		IL_280:
		IL_352:
		A_0.MoveToElement();
	}

	// Token: 0x06004F96 RID: 20374 RVA: 0x00305B04 File Offset: 0x00304B04
	private void ᜐ(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 17;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					num = 5;
					continue;
				}
				num = 4;
				continue;
			case 1:
				goto IL_4B;
			case 2:
				goto IL_D7;
			case 3:
				A_1.IsZeroHeight = XmlConvert.ToBoolean(A_0.Value);
				(A_1.PageSetup as XlsPageSetup).DefaultRowHeightFlag = false;
				num = 2;
				continue;
			case 4:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㵆ⱈ㥊≌ݎ㑐㩒㉔㽖ⵘ", a_)))
				{
					num = 3;
					continue;
				}
				goto IL_F7;
			case 5:
				goto IL_F5;
			case 6:
				if (true)
				{
				}
				break;
			}
			if (A_0 == null)
			{
				num = 1;
			}
			else
			{
				num = 0;
			}
		}
		IL_4B:
		IL_96:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕆ⱈ⩊⥌⩎⍐", a_));
		IL_D7:
		goto IL_F7;
		IL_F5:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑆ⅈ⹊⡌㭎", a_));
		IL_F7:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_96;
		default:
			if (false)
			{
			}
			A_0.MoveToElement();
			return;
		}
	}

	// Token: 0x06004F97 RID: 20375 RVA: 0x00305C2C File Offset: 0x00304C2C
	public void \u1717(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 18;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_139;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 1:
				goto IL_C3;
			case 2:
				goto IL_BE;
			case 3:
				this.ᜃ(A_0, A_1);
				num = 8;
				continue;
			case 4:
				if (A_0.LocalName == RecordTableEnumerator.b("╇⽉㹋⥍㕏ᅑㅓ㩕㑗⥙", a_))
				{
					num = 6;
					continue;
				}
				return;
			case 5:
				goto IL_C3;
			case 6:
				A_0.Read();
				num = 1;
				continue;
			case 7:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 15;
					continue;
				}
				num = 13;
				continue;
			case 8:
				goto IL_C3;
			case 9:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 12;
					continue;
				}
				return;
			case 10:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				num = 9;
				continue;
			case 11:
				return;
			case 12:
				goto IL_139;
			case 13:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 3;
					continue;
				}
				A_0.Read();
				num = 5;
				continue;
			case 14:
				goto IL_80;
			case 15:
				A_0.Read();
				if (true)
				{
				}
				num = 11;
				continue;
			}
			if (A_0 == null)
			{
				num = 14;
				continue;
			}
			num = 10;
			continue;
			IL_C3:
			num = 7;
			continue;
			IL_139:
			num = 4;
		}
		IL_80:
		throw new ArgumentNullException(RecordTableEnumerator.b("㩇⽉ⵋ⩍㕏⁑", a_));
		IL_BE:
		throw new ArgumentNullException(RecordTableEnumerator.b("㭇≉⥋⭍⑏", a_));
	}

	// Token: 0x06004F98 RID: 20376 RVA: 0x00305E08 File Offset: 0x00304E08
	public void ᜫ(XmlReader A_0)
	{
		int a_ = 11;
		switch (0)
		{
		default:
		{
			int num2;
			List<string> list;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
			{
				IL_13C:
				int num;
				int count;
				if (num >= count)
				{
					num2 = 0;
				}
				else
				{
					INameRanges names;
					XlsName xlsName = (XlsName)names[num];
					xlsName.ᜀ(this.ᜊ.ᜃ(list[num]));
					num++;
					num2 = 13;
				}
				break;
			}
			default:
				if (false)
				{
				}
				num2 = 8;
				break;
			}
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_1A2;
				case 1:
					A_0.Read();
					list = new List<string>();
					num2 = 12;
					continue;
				case 2:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num2 = 3;
						continue;
					}
					goto IL_241;
				case 3:
				{
					string item = this.\u1716(A_0);
					list.Add(item);
					if (true)
					{
					}
					num2 = 16;
					continue;
				}
				case 4:
					if (!A_0.IsEmptyElement)
					{
						num2 = 1;
						continue;
					}
					goto IL_1A2;
				case 5:
				{
					INameRanges names = this.ᜉ.Names;
					int num = 0;
					int count = list.Count;
					num2 = 9;
					continue;
				}
				case 6:
					goto IL_17B;
				case 7:
					num2 = 4;
					continue;
				case 9:
					goto IL_130;
				case 10:
					num2 = 19;
					continue;
				case 11:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num2 = 5;
						continue;
					}
					num2 = 2;
					continue;
				case 12:
					goto IL_17B;
				case 13:
					goto IL_130;
				case 14:
					goto IL_A4;
				case 15:
					return;
				case 16:
					goto IL_241;
				case 17:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num2 = 10;
						continue;
					}
					return;
				case 18:
					goto IL_13C;
				case 19:
					if (A_0.LocalName == RecordTableEnumerator.b("╀♂⍄⹆❈⹊⥌Ŏぐ㹒ご⑖", a_))
					{
						num2 = 7;
						continue;
					}
					return;
				}
				if (A_0 == null)
				{
					num2 = 14;
					continue;
				}
				num2 = 17;
				continue;
				IL_130:
				num2 = 18;
				continue;
				IL_17B:
				num2 = 11;
				continue;
				IL_1A2:
				A_0.Read();
				num2 = 15;
				continue;
				IL_241:
				A_0.Read();
				num2 = 6;
			}
			IL_A4:
			throw new ArgumentNullException(RecordTableEnumerator.b("㍀♂⑄⍆ⱈ㥊", a_));
		}
		}
	}

	// Token: 0x06004F99 RID: 20377 RVA: 0x003060AC File Offset: 0x003050AC
	public List<int> ᜀ(XmlReader A_0, ref Stream A_1)
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			int num = 1;
			List<int> result;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_193;
				case 2:
					goto IL_173;
				case 3:
				{
					bool flag;
					if (flag)
					{
						num = 14;
						continue;
					}
					goto IL_205;
				}
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_21E;
					default:
						if (false)
						{
						}
						num = 31;
						continue;
					}
					break;
				case 5:
					goto IL_CE;
				case 6:
					goto IL_193;
				case 7:
					if (spr\u22D2.\u177D == null)
					{
						num = 19;
						continue;
					}
					goto IL_22F;
				case 8:
					goto IL_193;
				case 9:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 27;
						continue;
					}
					A_0.Read();
					num = 16;
					continue;
				case 10:
					num = 12;
					continue;
				case 11:
					goto IL_205;
				case 12:
				{
					int num2;
					switch (num2)
					{
					case 0:
						this.ᜑ(A_0);
						num = 22;
						continue;
					case 1:
					{
						List<int> a_2 = this.\u1715(A_0);
						num = 34;
						continue;
					}
					case 2:
					{
						List<XlsFill> a_3 = this.ᜎ(A_0);
						num = 30;
						continue;
					}
					case 3:
					{
						List<XlsBordersCollection> a_4 = this.ᜉ(A_0);
						num = 20;
						continue;
					}
					case 4:
					{
						List<int> a_2;
						List<XlsFill> a_3;
						List<XlsBordersCollection> a_4;
						List<int> list = this.ᜀ(A_0, a_2, a_3, a_4);
						num = 32;
						continue;
					}
					case 5:
					{
						List<int> a_2;
						List<XlsFill> a_3;
						List<XlsBordersCollection> a_4;
						List<int> list;
						result = this.ᜀ(A_0, a_2, a_3, a_4, list);
						num = 0;
						continue;
					}
					case 6:
					{
						List<int> list;
						this.ᜂ(A_0, list);
						num = 8;
						continue;
					}
					case 7:
					{
						A_1 = new MemoryStream();
						StreamWriter a_5 = new StreamWriter(A_1);
						XmlWriter xmlWriter = UtilityMethods.ᜀ(a_5);
						xmlWriter.WriteNode(A_0, false);
						xmlWriter.Flush();
						bool flag = false;
						num = 6;
						continue;
					}
					case 8:
					{
						this.ᜉ.CustomTableStylesStream = ShapeParser.ReadNodeAsStream(A_0);
						bool flag = false;
						num = 25;
						continue;
					}
					case 9:
						this.ᜆ(A_0);
						num = 28;
						continue;
					case 10:
					{
						this.\u171B(A_0);
						bool flag = false;
						num = 35;
						continue;
					}
					default:
						num = 4;
						continue;
					}
					break;
				}
				case 13:
				{
					int num2;
					string localName;
					if (spr\u22D2.\u177D.TryGetValue(localName, out num2))
					{
						num = 10;
						continue;
					}
					goto IL_1F9;
				}
				case 14:
					A_0.Read();
					num = 11;
					continue;
				case 15:
					num = 33;
					continue;
				case 16:
					goto IL_4EA;
				case 17:
					num = 7;
					continue;
				case 18:
				{
					if (A_0.LocalName != RecordTableEnumerator.b("伻䨽㤿⹁⅃ᕅ⁇⽉⥋㩍", a_))
					{
						num = 2;
						continue;
					}
					List<int> a_2 = null;
					List<XlsBordersCollection> a_4 = null;
					List<XlsFill> a_3 = null;
					List<int> list = null;
					result = null;
					A_0.Read();
					num = 24;
					continue;
				}
				case 19:
					spr\u22D2.\u177D = new Dictionary<string, int>(11)
					{
						{
							RecordTableEnumerator.b("刻䬽ⴿс⥃㉅㭇", a_),
							0
						},
						{
							RecordTableEnumerator.b("娻儽⸿㙁㝃", a_),
							1
						},
						{
							RecordTableEnumerator.b("娻圽ⰿ⹁㝃", a_),
							2
						},
						{
							RecordTableEnumerator.b("帻儽㈿♁⅃㑅㭇", a_),
							3
						},
						{
							RecordTableEnumerator.b("弻嬽ⰿ⹁ᝃ㉅ㅇ♉⥋ᙍ㙏⅑", a_),
							4
						},
						{
							RecordTableEnumerator.b("弻嬽ⰿ⹁᱃⁅㭇", a_),
							5
						},
						{
							RecordTableEnumerator.b("弻嬽ⰿ⹁ᝃ㉅ㅇ♉⥋㵍", a_),
							6
						},
						{
							RecordTableEnumerator.b("堻䘽☿ㅁ", a_),
							7
						},
						{
							RecordTableEnumerator.b("䠻弽∿⹁⅃ᕅ㱇㍉⁋⭍⍏", a_),
							8
						},
						{
							RecordTableEnumerator.b("弻儽ⰿⵁ㙃㕅", a_),
							9
						},
						{
							RecordTableEnumerator.b("夻䘽㐿แ㝃㉅", a_),
							10
						}
					};
					num = 21;
					continue;
				case 20:
					goto IL_193;
				case 21:
					goto IL_22F;
				case 22:
					goto IL_193;
				case 23:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 15;
						continue;
					}
					goto IL_193;
				case 24:
					goto IL_205;
				case 25:
					goto IL_193;
				case 26:
				{
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						goto IL_21E;
					}
					bool flag = true;
					num = 23;
					continue;
				}
				case 27:
					num = 18;
					continue;
				case 28:
					goto IL_193;
				case 29:
					goto IL_22A;
				case 30:
					goto IL_193;
				case 31:
					goto IL_3B7;
				case 32:
					goto IL_193;
				case 33:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 17;
						continue;
					}
					goto IL_1F9;
				}
				case 34:
					goto IL_193;
				case 35:
					goto IL_193;
				}
				if (A_0 == null)
				{
					num = 5;
					continue;
				}
				goto IL_4EA;
				IL_193:
				num = 3;
				continue;
				IL_205:
				num = 26;
				continue;
				IL_21E:
				num = 29;
				continue;
				IL_22F:
				num = 13;
				continue;
				IL_4EA:
				num = 9;
			}
			IL_CE:
			throw new ArgumentNullException(RecordTableEnumerator.b("主嬽ℿ♁⅃㑅", a_));
			IL_173:
			if (true)
			{
			}
			throw new XmlException(RecordTableEnumerator.b("椻倽┿㩁㑃⍅⭇㹉⥋⩍灏⩑㥓㩕硗⹙㵛㥝䁟", a_) + A_0.Name);
			IL_1F9:
			throw new NotImplementedException(A_0.LocalName);
			IL_22A:
			AddtionalFormatWrapper addtionalFormatWrapper = (AddtionalFormatWrapper)this.ᜉ.InnerStyles[RecordTableEnumerator.b("爻儽㈿⽁╃⩅", a_)];
			spr\u192F spr_u192F = addtionalFormatWrapper.Wrapped.ᜭ();
			spr_u192F = this.ᜉ.InnerExtFormats.ᜁ(spr_u192F);
			this.ᜉ.DefaultXFIndex = spr_u192F.ᜠ();
			return result;
			IL_3B7:
			goto IL_1F9;
		}
		}
	}

	// Token: 0x06004F9A RID: 20378 RVA: 0x003066A8 File Offset: 0x003056A8
	private void \u171B(XmlReader A_0)
	{
		int a_ = 16;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				if (!(A_0.LocalName != RecordTableEnumerator.b("⍅ぇ㹉K㵍⑏", a_)))
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_A5;
					}
				}
				num = 2;
				continue;
			case 2:
				goto IL_79;
			case 3:
				goto IL_3E;
			}
			if (A_0 == null)
			{
				num = 3;
			}
			else
			{
				num = 0;
			}
		}
		IL_3E:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑅ⵇ⭉⡋⭍≏", a_));
		IL_79:
		throw new XmlException();
		IL_A5:
		if (false)
		{
		}
		Stream a_2 = ShapeParser.ReadNodeAsStream(A_0, true);
		this.ᜉ.DataHolder.ᜀ(a_2);
	}

	// Token: 0x06004F9B RID: 20379 RVA: 0x0030677C File Offset: 0x0030577C
	public Dictionary<int, int> ᜤ(XmlReader A_0)
	{
		int a_ = 7;
		switch (0)
		{
		default:
		{
			int num = 9;
			Dictionary<int, int> dictionary;
			int num2;
			SSTDictionary sstdictionary;
			for (;;)
			{
				XmlReader xmlReader;
				switch (num)
				{
				case 0:
				{
					int num3;
					dictionary[num2] = num3;
					num = 2;
					continue;
				}
				case 1:
					if (xmlReader.NodeType != XmlNodeType.EndElement)
					{
						num = 6;
						continue;
					}
					goto IL_2D3;
				case 2:
					goto IL_1F5;
				case 3:
					goto IL_1CB;
				case 4:
				{
					int num3 = this.ᜭ(xmlReader);
					num = 20;
					continue;
				}
				case 5:
					goto IL_1C9;
				case 6:
					num = 17;
					continue;
				case 7:
					goto IL_205;
				case 8:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 16;
						continue;
					}
					A_0.Read();
					num = 11;
					continue;
				case 10:
					goto IL_185;
				case 11:
					goto IL_E2;
				case 12:
					if (xmlReader.LocalName == RecordTableEnumerator.b("丼嘾", a_))
					{
						num = 4;
						continue;
					}
					goto IL_108;
				case 13:
					if (A_0.LocalName != RecordTableEnumerator.b("丼䰾㕀", a_))
					{
						num = 5;
						continue;
					}
					num = 19;
					continue;
				case 14:
					goto IL_252;
				case 15:
					goto IL_1CB;
				case 16:
					num = 13;
					continue;
				case 17:
					if (xmlReader.NodeType == XmlNodeType.None)
					{
						num = 10;
						continue;
					}
					if (true)
					{
					}
					num = 12;
					continue;
				case 18:
					goto IL_89;
				case 19:
					if (A_0.IsEmptyElement)
					{
						num = 14;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_205;
					default:
						if (false)
						{
						}
						this.ᜉ.SSTStream = ShapeParser.ReadNodeAsStream(A_0);
						this.ᜉ.SSTStream.Position = 0L;
						xmlReader = UtilityMethods.ᜀ(this.ᜉ.SSTStream);
						xmlReader.Read();
						num2 = 0;
						dictionary = new Dictionary<int, int>();
						sstdictionary = this.ᜉ.InnerSST;
						num = 3;
						continue;
					}
					break;
				case 20:
				{
					int num3;
					if (num2 != num3)
					{
						num = 0;
						continue;
					}
					goto IL_1F5;
				}
				}
				if (A_0 == null)
				{
					num = 18;
					continue;
				}
				IL_E2:
				num = 8;
				continue;
				IL_108:
				xmlReader.Skip();
				num = 15;
				continue;
				IL_205:
				goto IL_108;
				IL_1CB:
				num = 1;
				continue;
				IL_1F5:
				num2++;
				num = 7;
			}
			IL_89:
			throw new ArgumentNullException(RecordTableEnumerator.b("似娾⁀❂⁄㕆", a_));
			IL_185:
			goto IL_2D3;
			IL_1C9:
			throw new XmlException(RecordTableEnumerator.b("似娾⁀❂⁄㕆", a_));
			IL_252:
			return null;
			IL_2D3:
			sstdictionary.UpdateRefCounts(num2);
			return dictionary;
		}
		}
	}

	// Token: 0x06004F9C RID: 20380 RVA: 0x00306A64 File Offset: 0x00305A64
	public int ᜭ(XmlReader A_0)
	{
		int a_ = 11;
		int num = 1;
		int result;
		for (;;)
		{
			bool a_2;
			switch (num)
			{
			case 0:
				num = 17;
				continue;
			case 2:
				result = this.ᜉ.InnerSST.AddIncrease(string.Empty, false);
				A_0.Skip();
				num = 6;
				continue;
			case 3:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("㍀", a_)))
				{
					num = 13;
					continue;
				}
				result = this.ᜬ(A_0);
				num = 8;
				continue;
			}
			case 4:
				goto IL_F5;
			case 5:
				if (A_0.IsEmptyElement)
				{
					num = 2;
					continue;
				}
				goto IL_168;
			case 6:
				return result;
			case 7:
				goto IL_1EA;
			case 8:
				goto IL_168;
			case 9:
				num = 3;
				continue;
			case 10:
				goto IL_168;
			case 11:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 18;
					continue;
				}
				num = 16;
				continue;
			case 12:
				goto IL_7B;
			case 13:
				num = 7;
				continue;
			case 14:
				num = 15;
				continue;
			case 15:
				if (A_0.LocalName != RecordTableEnumerator.b("⡀あ", a_))
				{
					num = 4;
					continue;
				}
				goto IL_234;
			case 16:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 0;
					continue;
				}
				goto IL_1EA;
			}
			case 17:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("㕀", a_)))
				{
					num = 9;
					continue;
				}
				result = this.ᜂ(A_0, a_2);
				num = 10;
				continue;
			}
			case 18:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_10F;
				default:
					goto IL_1A6;
				}
				break;
			case 19:
				goto IL_168;
			case 20:
				if (A_0.LocalName != RecordTableEnumerator.b("㉀⩂", a_))
				{
					num = 14;
					continue;
				}
				goto IL_234;
			}
			if (A_0 == null)
			{
				num = 12;
				continue;
			}
			IL_10F:
			num = 20;
			continue;
			IL_168:
			if (true)
			{
			}
			num = 11;
			continue;
			IL_1EA:
			A_0.Skip();
			num = 19;
			continue;
			IL_234:
			a_2 = (A_0.LocalName == RecordTableEnumerator.b("⡀あ", a_));
			result = -1;
			A_0.Read();
			num = 5;
		}
		IL_7B:
		throw new ArgumentNullException(RecordTableEnumerator.b("㍀♂⑄⍆ⱈ㥊", a_));
		IL_F5:
		throw new XmlException(RecordTableEnumerator.b("㍀♂⑄⍆ⱈ㥊", a_));
		IL_1A6:
		if (false)
		{
		}
		return result;
	}

	// Token: 0x06004F9D RID: 20381 RVA: 0x00306D2C File Offset: 0x00305D2C
	internal int ᜀ(XmlReader A_0, out string A_1)
	{
		int a_ = 10;
		if (true)
		{
		}
		int num = 20;
		int result;
		for (;;)
		{
			bool a_2;
			switch (num)
			{
			case 0:
				goto IL_83;
			case 1:
				goto IL_171;
			case 2:
				result = this.ᜉ.InnerSST.AddIncrease(string.Empty, false);
				A_0.Skip();
				num = 4;
				continue;
			case 3:
				if (A_0.LocalName != RecordTableEnumerator.b("⤿ㅁ", a_))
				{
					num = 8;
					continue;
				}
				goto IL_235;
			case 4:
				return result;
			case 5:
				goto IL_171;
			case 6:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("㈿", a_)))
				{
					num = 12;
					continue;
				}
				result = this.ᜬ(A_0);
				num = 5;
				continue;
			}
			case 7:
				num = 6;
				continue;
			case 8:
				goto IL_FD;
			case 9:
				goto IL_171;
			case 10:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 14;
					continue;
				}
				goto IL_1EB;
			}
			case 11:
				if (A_0.LocalName != RecordTableEnumerator.b("㌿⭁", a_))
				{
					num = 15;
					continue;
				}
				goto IL_235;
			case 12:
				num = 16;
				continue;
			case 13:
				if (A_0.IsEmptyElement)
				{
					num = 2;
					continue;
				}
				goto IL_171;
			case 14:
				num = 17;
				continue;
			case 15:
				num = 3;
				continue;
			case 16:
				goto IL_1EB;
			case 17:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("㐿", a_)))
				{
					num = 7;
					continue;
				}
				result = this.ᜀ(A_0, a_2, out A_1);
				num = 9;
				continue;
			}
			case 18:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 19;
					continue;
				}
				num = 10;
				continue;
			case 19:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_117;
				default:
					goto IL_1A7;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			IL_117:
			num = 11;
			continue;
			IL_171:
			num = 18;
			continue;
			IL_1EB:
			A_0.Skip();
			num = 1;
			continue;
			IL_235:
			a_2 = (A_0.LocalName == RecordTableEnumerator.b("⤿ㅁ", a_));
			result = -1;
			A_0.Read();
			A_1 = string.Empty;
			num = 13;
		}
		IL_83:
		throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁╃≅ⵇ㡉", a_));
		IL_FD:
		throw new XmlException(RecordTableEnumerator.b("㈿❁╃≅ⵇ㡉", a_));
		IL_1A7:
		if (false)
		{
		}
		return result;
	}

	// Token: 0x06004F9E RID: 20382 RVA: 0x00306FFC File Offset: 0x00305FFC
	public void ᜀ(XmlReader A_0, ShapeCollectionBase A_1, RelationsCollection A_2, string A_3)
	{
		int a_ = 16;
		int num = 16;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 24;
				continue;
			case 1:
				goto IL_231;
			case 2:
				goto IL_15F;
			case 3:
				if (A_0.NodeType != XmlNodeType.EndElement)
				{
					if (true)
					{
					}
					num = 4;
					continue;
				}
				return;
			case 4:
				num = 12;
				continue;
			case 5:
				goto IL_15F;
			case 6:
				goto IL_15F;
			case 7:
			{
				if (A_0.LocalName != RecordTableEnumerator.b("㹅╇♉", a_))
				{
					num = 13;
					continue;
				}
				A_0.Read();
				Dictionary<string, XlsShape> dictionary = new Dictionary<string, XlsShape>();
				Stream stream = null;
				num = 2;
				continue;
			}
			case 8:
				if (A_0.NodeType != XmlNodeType.None)
				{
					num = 9;
					continue;
				}
				goto IL_1C3;
			case 9:
				goto IL_F9;
			case 10:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2CB;
				default:
					if (false)
					{
					}
					num = 17;
					continue;
				}
				break;
			case 11:
				goto IL_367;
			case 12:
				if (A_0.EOF)
				{
					num = 1;
					continue;
				}
				num = 23;
				continue;
			case 13:
				goto IL_1F4;
			case 14:
				goto IL_15F;
			case 15:
				num = 20;
				continue;
			case 17:
				goto IL_120;
			case 18:
				goto IL_15F;
			case 19:
				goto IL_A9;
			case 20:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("㕅⁇⭉㱋⭍⑏⭑⑓㍕", a_)))
				{
					num = 0;
					continue;
				}
				Dictionary<string, XlsShape> dictionary;
				Stream stream;
				this.ᜀ(A_0, A_1, dictionary, stream);
				num = 5;
				continue;
			}
			case 21:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 15;
					continue;
				}
				goto IL_120;
			}
			case 22:
				if (A_1 == null)
				{
					num = 11;
					continue;
				}
				goto IL_F9;
			case 23:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					goto IL_2CB;
				}
				A_0.Read();
				num = 14;
				continue;
			case 24:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("㕅⁇⭉㱋⭍", a_)))
				{
					num = 29;
					continue;
				}
				Dictionary<string, XlsShape> dictionary;
				this.ᜀ(A_0, dictionary, A_2, A_3);
				num = 18;
				continue;
			}
			case 25:
				num = 21;
				continue;
			case 26:
				goto IL_1C3;
			case 27:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 26;
					continue;
				}
				A_0.Read();
				num = 8;
				continue;
			case 28:
				goto IL_15F;
			case 29:
				num = 30;
				continue;
			case 30:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("㕅⁇⭉㱋⭍㱏㍑ⵓ㥕ⵗ⹙", a_)))
				{
					num = 10;
					continue;
				}
				Stream stream = ShapeParser.ReadNodeAsStream(A_0);
				stream.Position = 0L;
				num = 28;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 19;
				continue;
			}
			num = 22;
			continue;
			IL_F9:
			num = 27;
			continue;
			IL_120:
			A_0.Skip();
			num = 6;
			continue;
			IL_15F:
			num = 3;
			continue;
			IL_1C3:
			num = 7;
			continue;
			IL_2CB:
			num = 25;
		}
		IL_A9:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑅ⵇ⭉⡋⭍≏", a_));
		IL_1F4:
		throw new XmlException(RecordTableEnumerator.b("ፅ♇⽉㑋㹍㕏ㅑ⁓㍕㱗穙⡛㽝ݟ", a_));
		IL_231:
		return;
		IL_367:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕅⁇⽉⥋㩍", a_));
	}

	// Token: 0x06004F9F RID: 20383 RVA: 0x003073AC File Offset: 0x003063AC
	public RelationsCollection ᜧ(XmlReader A_0)
	{
		int a_ = 0;
		int num = 5;
		for (;;)
		{
			RelationsCollection relationsCollection;
			switch (num)
			{
			case 0:
				goto IL_12F;
			case 1:
				if (true)
				{
				}
				num = 8;
				continue;
			case 2:
				return relationsCollection;
			case 3:
				goto IL_12F;
			case 4:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 2;
					continue;
				}
				num = 14;
				continue;
			case 6:
				num = 10;
				continue;
			case 7:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					goto IL_18A;
				}
				A_0.Read();
				num = 12;
				continue;
			case 8:
				if (A_0.LocalName != RecordTableEnumerator.b("搵崷嘹崻䨽⤿ⵁ⩃㕅⁇⍉㱋㵍", a_))
				{
					num = 16;
					continue;
				}
				A_0.Read();
				num = 3;
				continue;
			case 9:
				goto IL_173;
			case 10:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_18A;
				default:
					if (false)
					{
					}
					if (A_0.LocalName == RecordTableEnumerator.b("搵崷嘹崻䨽⤿ⵁ⩃㕅⁇⍉㱋", a_))
					{
						num = 15;
						continue;
					}
					goto IL_197;
				}
				break;
			case 11:
				goto IL_68;
			case 12:
				goto IL_173;
			case 13:
				goto IL_A9;
			case 14:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 6;
					continue;
				}
				goto IL_A9;
			case 15:
				spr\u2306.ᜂ(A_0, relationsCollection);
				num = 13;
				continue;
			case 16:
				goto IL_22D;
			}
			if (A_0 == null)
			{
				num = 11;
				continue;
			}
			relationsCollection = new RelationsCollection();
			num = 9;
			continue;
			IL_A9:
			A_0.Read();
			num = 0;
			continue;
			IL_12F:
			num = 4;
			continue;
			IL_173:
			num = 7;
			continue;
			IL_18A:
			num = 1;
		}
		IL_68:
		throw new ArgumentNullException(RecordTableEnumerator.b("䐵崷嬹堻嬽㈿", a_));
		IL_197:
		throw new XmlException(RecordTableEnumerator.b("挵嘷弹䐻丽┿⅁ぃ⍅ⱇ橉㡋⽍㝏牑", a_) + A_0.Value);
		IL_22D:
		throw new XmlException(RecordTableEnumerator.b("挵嘷弹䐻丽┿⅁ぃ⍅ⱇ橉㡋⽍㝏牑", a_) + A_0.Name);
	}

	// Token: 0x06004FA0 RID: 20384 RVA: 0x003075EC File Offset: 0x003065EC
	public Dictionary<string, string> ᜀ(XmlReader A_0, IInternalWorksheet A_1, List<int> A_2, string A_3)
	{
		int a_ = 8;
		int num = 7;
		Dictionary<string, string> dictionary;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				A_0.Read();
				int num2 = 1;
				num = 18;
				continue;
			}
			case 1:
				if (A_1 == null)
				{
					num = 16;
					continue;
				}
				num = 19;
				continue;
			case 2:
				goto IL_FF;
			case 3:
				goto IL_7F;
			case 4:
				goto IL_15C;
			case 5:
				goto IL_1E7;
			case 6:
				if (A_0.MoveToFirstAttribute())
				{
					num = 11;
					continue;
				}
				goto IL_15C;
			case 8:
				goto IL_1A2;
			case 9:
				goto IL_A5;
			case 10:
				if (!A_0.IsEmptyElement)
				{
					num = 0;
					continue;
				}
				goto IL_2C3;
			case 11:
				dictionary = new Dictionary<string, string>();
				dictionary.Add(A_0.LocalName, A_0.Value);
				num = 9;
				continue;
			case 12:
				A_0.MoveToElement();
				num = 4;
				continue;
			case 13:
				if (A_0.LocalName == RecordTableEnumerator.b("䰽⼿㕁", a_))
				{
					num = 20;
					continue;
				}
				goto IL_1E7;
			case 14:
				if (!A_0.MoveToNextAttribute())
				{
					num = 12;
					continue;
				}
				dictionary.Add(A_0.LocalName, A_0.Value);
				num = 15;
				continue;
			case 15:
				goto IL_1C4;
			case 16:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1C4;
				default:
					goto IL_154;
				}
				break;
			case 17:
				goto IL_182;
			case 18:
				if (true)
				{
				}
				goto IL_182;
			case 19:
				if (A_0.LocalName != RecordTableEnumerator.b("䴽⠿❁⅃㉅ే⭉㡋⽍", a_))
				{
					num = 2;
					continue;
				}
				A_0.MoveToElement();
				dictionary = null;
				num = 6;
				continue;
			case 20:
			{
				int num2 = this.ᜀ(A_0, A_1, A_2, A_3, num2);
				num2++;
				num = 5;
				continue;
			}
			case 21:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 8;
					continue;
				}
				num = 13;
				continue;
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			num = 1;
			continue;
			IL_A5:
			num = 14;
			continue;
			IL_1C4:
			goto IL_A5;
			IL_15C:
			num = 10;
			continue;
			IL_182:
			num = 21;
			continue;
			IL_1E7:
			A_0.Skip();
			num = 17;
		}
		IL_7F:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰽┿⍁⁃⍅㩇", a_));
		IL_FF:
		throw new XmlException(RecordTableEnumerator.b("䰽┿⍁⁃⍅㩇", a_));
		IL_154:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䴽⠿❁⅃㉅", a_));
		IL_1A2:
		IL_2C3:
		A_0.Read();
		return dictionary;
	}

	// Token: 0x06004FA1 RID: 20385 RVA: 0x003078C4 File Offset: 0x003068C4
	public void \u1714(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 8;
		int num = 6;
		for (;;)
		{
			List<string> a_2;
			switch (num)
			{
			case 0:
				num = 16;
				continue;
			case 1:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 4;
					continue;
				}
				goto IL_CA;
			case 2:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 19;
					continue;
				}
				A_0.Skip();
				num = 11;
				continue;
			case 3:
				goto IL_257;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1DC;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num = 13;
					continue;
				}
				break;
			case 5:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 23;
					continue;
				}
				goto IL_F5;
			}
			case 7:
				return;
			case 8:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("弽㔿㙁ⱃ⥅㩇㥉", a_)))
				{
					num = 0;
					continue;
				}
				a_2 = this.\u1717(A_0);
				num = 10;
				continue;
			}
			case 9:
				goto IL_87;
			case 10:
				goto IL_1F3;
			case 11:
				goto IL_1F3;
			case 12:
				num = 18;
				continue;
			case 13:
				if (A_0.LocalName == RecordTableEnumerator.b("崽⼿⽁⥃⍅♇㹉㽋", a_))
				{
					num = 21;
					continue;
				}
				goto IL_257;
			case 14:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 7;
					continue;
				}
				num = 2;
				continue;
			case 15:
				goto IL_1F3;
			case 16:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("崽⼿⽁⥃⍅♇㹉K❍⍏♑", a_)))
				{
					num = 12;
					continue;
				}
				this.ᜀ(A_0, a_2, A_1);
				goto IL_1DC;
			}
			case 17:
				goto IL_122;
			case 18:
				goto IL_97;
			case 19:
				num = 5;
				continue;
			case 20:
				if (A_1 == null)
				{
					num = 17;
					continue;
				}
				goto IL_CA;
			case 21:
				A_0.Read();
				num = 3;
				continue;
			case 22:
				goto IL_1F3;
			case 23:
				num = 8;
				continue;
			}
			if (A_0 == null)
			{
				num = 9;
				continue;
			}
			num = 20;
			continue;
			IL_CA:
			A_0.Read();
			num = 1;
			continue;
			IL_1DC:
			num = 15;
			continue;
			IL_1F3:
			num = 14;
			continue;
			IL_257:
			a_2 = null;
			num = 22;
		}
		IL_87:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰽┿⍁⁃⍅㩇", a_));
		IL_97:
		IL_F5:
		throw new XmlException(RecordTableEnumerator.b("欽⸿❁㱃㙅ⵇ⥉㡋⭍㑏牑ⱓ㭕㑗穙⡛㽝ݟ䱡", a_));
		IL_122:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴽⠿❁⅃㉅", a_));
	}

	// Token: 0x06004FA2 RID: 20386 RVA: 0x00307B94 File Offset: 0x00306B94
	public void ᜄ(XmlReader A_0, XlsWorksheetBase A_1, string A_2, List<string> A_3, Dictionary<string, object> A_4)
	{
		int a_ = 4;
		int num = 23;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_1EE;
			case 1:
				num = 7;
				continue;
			case 2:
				goto IL_247;
			case 3:
				num = 6;
				continue;
			case 4:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("丹䬻儽̿❁⡃⩅े⑉⽋♍㽏⁑", a_)))
				{
					num = 12;
					continue;
				}
				goto IL_2FD;
			}
			case 5:
				num = 28;
				continue;
			case 6:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("笹倻䨽┿ぁ⩃❅㱇⽉ཋ⅍㹏♑ㅓ㡕ⱗ", a_)))
				{
					goto IL_17D;
				}
				this.ᜃ(A_0, A_1, A_2, A_3, A_4);
				num = 22;
				continue;
			}
			case 7:
				if (A_0.LocalName != RecordTableEnumerator.b("䴹伻稽㈿", a_))
				{
					num = 20;
					continue;
				}
				goto IL_34B;
			case 8:
				if (A_3 == null)
				{
					num = 34;
					continue;
				}
				goto IL_260;
			case 9:
				if (A_0.NodeType != XmlNodeType.Element)
				{
					A_0.Read();
					num = 26;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_17D;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					num = 1;
					continue;
				}
				break;
			case 10:
				if (A_0.LocalName != RecordTableEnumerator.b("伹伻嬽㈿ᅁⱃ❅㡇⽉㽋", a_))
				{
					num = 2;
					continue;
				}
				goto IL_34B;
			case 11:
				goto IL_1EE;
			case 12:
				num = 30;
				continue;
			case 13:
				goto IL_1EE;
			case 14:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 31;
					continue;
				}
				goto IL_1DB;
			}
			case 15:
				goto IL_B9;
			case 16:
				goto IL_3EE;
			case 17:
				num = 24;
				continue;
			case 18:
				goto IL_20E;
			case 19:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 25;
					continue;
				}
				A_0.Skip();
				num = 11;
				continue;
			case 20:
				num = 10;
				continue;
			case 21:
				num = 13;
				continue;
			case 22:
				goto IL_1EE;
			case 24:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("䠹夻刽ጿ⭁㹃⍅े⑉⽋♍㽏⁑", a_)))
				{
					num = 3;
					continue;
				}
				goto IL_2FD;
			}
			case 25:
				num = 14;
				continue;
			case 26:
				goto IL_260;
			case 27:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 18;
					continue;
				}
				num = 19;
				continue;
			case 28:
				goto IL_1DB;
			case 29:
				if (A_0.NodeType != XmlNodeType.None)
				{
					num = 21;
					continue;
				}
				return;
			case 30:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("唹刻嬽̿❁⡃⩅े⑉⽋♍㽏⁑", a_)))
				{
					num = 17;
					continue;
				}
				goto IL_2FD;
			}
			case 31:
				num = 4;
				continue;
			case 32:
				if (A_1 == null)
				{
					num = 16;
					continue;
				}
				num = 8;
				continue;
			case 33:
				goto IL_1EE;
			case 34:
				goto IL_2C2;
			}
			if (A_0 == null)
			{
				num = 15;
				continue;
			}
			num = 32;
			continue;
			IL_17D:
			num = 5;
			continue;
			IL_1DB:
			A_0.Skip();
			num = 33;
			continue;
			IL_1EE:
			num = 27;
			continue;
			IL_260:
			num = 9;
			continue;
			IL_2FD:
			this.ᜁ(A_0, A_1, A_2, A_3, A_4);
			num = 0;
			continue;
			IL_34B:
			A_0.Read();
			num = 29;
		}
		IL_B9:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠹夻弽␿❁㙃", a_));
		IL_20E:
		return;
		IL_247:
		throw new XmlException(RecordTableEnumerator.b("漹刻嬽㠿㉁⅃╅㱇⽉⡋湍⡏㽑㡓癕ⱗ㭙㭛繝", a_) + A_0.LocalName);
		IL_2C2:
		throw new ArgumentNullException(RecordTableEnumerator.b("嘹伻䨽ሿ❁⡃❅㱇⍉⍋⁍㑏᭑こ╕", a_));
		IL_3EE:
		throw new ArgumentNullException(RecordTableEnumerator.b("䤹吻嬽┿㙁", a_));
	}

	// Token: 0x06004FA3 RID: 20387 RVA: 0x00307FB4 File Offset: 0x00306FB4
	private void ᜃ(XmlReader A_0, XlsWorksheetBase A_1, string A_2, List<string> A_3, Dictionary<string, object> A_4)
	{
		int a_ = 19;
		int num = 11;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				string localName;
				if (localName == RecordTableEnumerator.b("ੈ⍊≌♎㉐㙒", a_))
				{
					num = 13;
					continue;
				}
				goto IL_1B2;
			}
			case 1:
				goto IL_176;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					if (A_0.NodeType != XmlNodeType.EndElement)
					{
						num = 4;
						continue;
					}
					break;
				}
				num = 1;
				continue;
			case 3:
				goto IL_1B0;
			case 4:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 6;
					continue;
				}
				A_0.Skip();
				num = 10;
				continue;
			case 5:
				goto IL_125;
			case 6:
				num = 14;
				continue;
			case 7:
				goto IL_125;
			case 8:
				goto IL_125;
			case 9:
				goto IL_6C;
			case 10:
				goto IL_125;
			case 12:
				if (A_3 == null)
				{
					num = 3;
					continue;
				}
				A_0.Read();
				this.ᜑ = true;
				num = 5;
				continue;
			case 13:
				this.ᜂ(A_0, A_1, A_2, A_3, A_4);
				num = 8;
				continue;
			case 14:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 16;
					continue;
				}
				goto IL_1B2;
			}
			case 15:
				if (A_1 == null)
				{
					num = 17;
					continue;
				}
				num = 12;
				continue;
			case 16:
				num = 0;
				continue;
			case 17:
				goto IL_EF;
			}
			if (A_0 == null)
			{
				num = 9;
				continue;
			}
			num = 15;
			continue;
			IL_125:
			num = 2;
			continue;
			IL_1B2:
			A_0.Skip();
			num = 7;
		}
		IL_6C:
		throw new ArgumentNullException(RecordTableEnumerator.b("㭈⹊ⱌ⭎㑐⅒", a_));
		IL_EF:
		throw new ArgumentNullException(RecordTableEnumerator.b("㩈⍊⡌⩎═", a_));
		IL_176:
		A_0.Read();
		return;
		IL_1B0:
		throw new ArgumentNullException(RecordTableEnumerator.b("╈㡊㥌ᵎ㑐㽒㑔⍖じ㑚㍜ᙞՠၢ", a_));
	}

	// Token: 0x06004FA4 RID: 20388 RVA: 0x003081E0 File Offset: 0x003071E0
	private void ᜏ(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 2;
		int num = 15;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_149;
			case 1:
				goto IL_D8;
			case 2:
				goto IL_129;
			case 3:
				goto IL_129;
			case 4:
			{
				string localName;
				if (localName == RecordTableEnumerator.b("笷刹医圽⌿❁", a_))
				{
					num = 14;
					continue;
				}
				goto IL_10C;
			}
			case 5:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 13;
					continue;
				}
				goto IL_10C;
			}
			case 6:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 0;
					continue;
				}
				num = 10;
				continue;
			case 7:
				goto IL_129;
			case 8:
				if (A_1 != null)
				{
					A_0.Read();
					num = 3;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_170;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				break;
			case 9:
				goto IL_170;
			case 10:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 9;
					continue;
				}
				A_0.Skip();
				if (true)
				{
				}
				num = 7;
				continue;
			case 11:
				goto IL_129;
			case 12:
				goto IL_64;
			case 13:
				num = 4;
				continue;
			case 14:
				this.ᜎ(A_0, A_1);
				num = 2;
				continue;
			}
			if (A_0 == null)
			{
				num = 12;
				continue;
			}
			num = 8;
			continue;
			IL_10C:
			A_0.Skip();
			num = 11;
			continue;
			IL_129:
			num = 6;
			continue;
			IL_170:
			num = 5;
		}
		IL_64:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹崻娽┿ぁ", a_));
		IL_D8:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬷刹夻嬽㐿", a_));
		IL_149:
		A_0.Read();
	}

	// Token: 0x06004FA5 RID: 20389 RVA: 0x003083C8 File Offset: 0x003073C8
	private void ᜂ(XmlReader A_0, XlsWorksheetBase A_1, string A_2, List<string> A_3, Dictionary<string, object> A_4)
	{
		int a_ = 11;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				num = 4;
				continue;
			case 2:
				goto IL_16E;
			case 3:
				num = 18;
				continue;
			case 4:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("㕀㑂⩄цⱈ❊⅌๎㽐げ㵔㡖⭘", a_)))
				{
					num = 20;
					continue;
				}
				goto IL_1E9;
			}
			case 5:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("⹀ⵂ⁄цⱈ❊⅌๎㽐げ㵔㡖⭘", a_)))
				{
					num = 14;
					continue;
				}
				goto IL_1E9;
			}
			case 6:
				goto IL_16E;
			case 7:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_107;
				default:
					goto IL_21C;
				}
				break;
			case 8:
				goto IL_18E;
			case 9:
				goto IL_89;
			case 10:
				goto IL_1E9;
			case 11:
				if (A_3 == null)
				{
					num = 7;
					continue;
				}
				A_0.Read();
				num = 15;
				continue;
			case 12:
				goto IL_130;
			case 13:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 8;
					continue;
				}
				num = 16;
				continue;
			case 14:
				num = 17;
				continue;
			case 15:
				goto IL_16E;
			case 16:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 3;
					continue;
				}
				A_0.Skip();
				num = 21;
				continue;
			case 17:
			{
				string localName;
				if (localName == RecordTableEnumerator.b("㍀♂⥄ᑆ⁈ㅊ⡌๎㽐げ㵔㡖⭘", a_))
				{
					num = 10;
					continue;
				}
				goto IL_101;
			}
			case 18:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 1;
					continue;
				}
				goto IL_101;
			}
			case 19:
				if (A_1 == null)
				{
					num = 12;
					continue;
				}
				num = 11;
				continue;
			case 20:
				if (true)
				{
				}
				num = 5;
				continue;
			case 21:
				goto IL_16E;
			}
			if (A_0 == null)
			{
				num = 9;
				continue;
			}
			num = 19;
			continue;
			IL_107:
			num = 6;
			continue;
			IL_101:
			A_0.Skip();
			goto IL_107;
			IL_16E:
			num = 13;
			continue;
			IL_1E9:
			this.ᜁ(A_0, A_1, A_2, A_3, A_4);
			num = 2;
		}
		IL_89:
		throw new ArgumentNullException(RecordTableEnumerator.b("㍀♂⑄⍆ⱈ㥊", a_));
		IL_130:
		throw new ArgumentNullException(RecordTableEnumerator.b("㉀⭂⁄≆㵈", a_));
		IL_18E:
		A_0.Read();
		return;
		IL_21C:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("ⵀあㅄᕆⱈ❊ⱌ㭎㡐㱒㭔Ṗ㵘⡚", a_));
	}

	// Token: 0x06004FA6 RID: 20390 RVA: 0x00308668 File Offset: 0x00307668
	private void ᜎ(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 17;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_16D;
			case 1:
			{
				string localName;
				if (localName == RecordTableEnumerator.b("⑆♈╊㥌㵎㹐㽒♔", a_))
				{
					num = 13;
					continue;
				}
				goto IL_10C;
			}
			case 2:
				goto IL_129;
			case 3:
				goto IL_E0;
			case 4:
				goto IL_149;
			case 5:
				goto IL_129;
			case 7:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 8;
					continue;
				}
				goto IL_10C;
			}
			case 8:
				num = 1;
				continue;
			case 9:
				goto IL_129;
			case 10:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 4;
					continue;
				}
				num = 14;
				continue;
			case 11:
				if (A_1 != null)
				{
					A_0.Read();
					num = 5;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_16D;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			case 12:
				goto IL_64;
			case 13:
				this.ᜈ(A_0, A_1);
				num = 9;
				continue;
			case 14:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 0;
					continue;
				}
				A_0.Skip();
				num = 2;
				continue;
			case 15:
				goto IL_129;
			}
			if (A_0 == null)
			{
				num = 12;
				continue;
			}
			num = 11;
			continue;
			IL_10C:
			A_0.Skip();
			num = 15;
			continue;
			IL_129:
			num = 10;
			continue;
			IL_16D:
			num = 7;
		}
		IL_64:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕆ⱈ⩊⥌⩎⍐", a_));
		IL_E0:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑆ⅈ⹊⡌㭎", a_));
		IL_149:
		A_0.Read();
	}

	// Token: 0x06004FA7 RID: 20391 RVA: 0x00308850 File Offset: 0x00307850
	private void ᜀ(XmlReader A_0, XlsWorksheet A_1, ref MemoryStream A_2, string A_3, Dictionary<string, object> A_4)
	{
		int a_ = 5;
		XmlWriter xmlWriter;
		for (;;)
		{
			A_2 = new MemoryStream();
			xmlWriter = UtilityMethods.ᜀ(A_2, Encoding.UTF8);
			xmlWriter.WriteStartElement(RecordTableEnumerator.b("䤺刼倾㕀", a_), RecordTableEnumerator.b("区䤼䬾ㅀ祂橄框㩈⡊╌⩎㱐㉒♔祖㙘⭚㡜ㅞᥠ๢।Ŧ٨ᥪl๮հr孴ᡶ୸ᱺ剼౾ﮖ뚘ꦚ궜꾞鞠貢좤욦삨얪", a_));
			int num = 24;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_6D7;
				case 1:
					goto IL_6D7;
				case 2:
					goto IL_6D7;
				case 3:
					spr\u22D2.\u177E = new Dictionary<string, int>(23)
					{
						{
							RecordTableEnumerator.b("嘺堼䴾♀♂ل≆╈❊㹌", a_),
							0
						},
						{
							RecordTableEnumerator.b("䬺唼倾⽀♂ㅄ⹆⩈ᭊ㽌", a_),
							1
						},
						{
							RecordTableEnumerator.b("场堼堾⁀⁂㱄͆㭈⩊㩌♎㽐㑒", a_),
							2
						},
						{
							RecordTableEnumerator.b("吺儼娾เ⅂⽄≆⩈㽊㹌", a_),
							3
						},
						{
							RecordTableEnumerator.b("场堼堾⁀⁂㱄͆㭈⩊㩌♎㽐㑒ᵔᅖ", a_),
							4
						},
						{
							RecordTableEnumerator.b("强似帾㙀⩂⭄⁆", a_),
							5
						},
						{
							RecordTableEnumerator.b("堺刼儾╀⩂ㅄ⹆♈╊ⱌ⍎ᝐ㱒❔㩖㡘⽚⥜㙞འѢ", a_),
							6
						},
						{
							RecordTableEnumerator.b("䬺吼尾㕀㙂㝄≆", a_),
							7
						},
						{
							RecordTableEnumerator.b("强尼䬾⁀ᕂ⑄⭆⁈⽊ⱌ㭎㡐㱒㭔⑖", a_),
							8
						},
						{
							RecordTableEnumerator.b("娺䠼䬾⹀Ղⱄ⭆㵈⹊㽌", a_),
							9
						},
						{
							RecordTableEnumerator.b("区䐼伾⑀ㅂ⥄⹆❈⁊㹌", a_),
							10
						},
						{
							RecordTableEnumerator.b("䬺似嘾⽀㝂੄㝆㵈≊≌ⅎ≐", a_),
							11
						},
						{
							RecordTableEnumerator.b("䬺尼堾⑀โ⑄㕆⹈≊⍌㱎", a_),
							12
						},
						{
							RecordTableEnumerator.b("䬺尼堾⑀၂⁄㍆㱈㭊", a_),
							13
						},
						{
							RecordTableEnumerator.b("区堼帾╀♂㝄ņ♈⑊㥌⩎⍐", a_),
							14
						},
						{
							RecordTableEnumerator.b("䤺刼䠾̀ㅂ⁄♆≈㡊", a_),
							15
						},
						{
							RecordTableEnumerator.b("堺刼匾̀ㅂ⁄♆≈㡊", a_),
							16
						},
						{
							RecordTableEnumerator.b("堺䠼䰾㕀ⱂ⡄ᝆ㭈⑊㵌⩎⍐❒㱔㉖⩘", a_),
							17
						},
						{
							RecordTableEnumerator.b("刺娼儾⹀ㅂ⁄⍆ై㥊㽌⁎⍐⁒", a_),
							18
						},
						{
							RecordTableEnumerator.b("䠺唼娾⑀㝂ᕄ㕆♈㽊⡌ⱎ═㩒㩔㥖", a_),
							19
						},
						{
							RecordTableEnumerator.b("稺儼䬾⑀ㅂ⭄♆㵈⹊์⁎㽐❒ご㥖ⵘ", a_),
							20
						},
						{
							RecordTableEnumerator.b("堺刼儾㕀ㅂ⩄⭆㩈", a_),
							21
						},
						{
							RecordTableEnumerator.b("伺尼崾ⵀ♂ᕄ♆㭈㽊㹌", a_),
							22
						}
					};
					num = 28;
					continue;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2B9;
					default:
					{
						if (false)
						{
						}
						if (true)
						{
						}
						string localName;
						int num2;
						if (spr\u22D2.\u177E.TryGetValue(localName, out num2))
						{
							num = 10;
							continue;
						}
						goto IL_34E;
					}
					}
					break;
				case 5:
					goto IL_6D7;
				case 6:
					goto IL_6D7;
				case 7:
					goto IL_6D7;
				case 8:
					num = 39;
					continue;
				case 9:
					goto IL_6D7;
				case 10:
					goto IL_2B9;
				case 11:
					goto IL_6D7;
				case 12:
					if (spr\u22D2.\u177E == null)
					{
						num = 3;
						continue;
					}
					goto IL_168;
				case 13:
					goto IL_6D7;
				case 14:
					goto IL_6D7;
				case 15:
					goto IL_6D7;
				case 16:
					if (!A_0.EOF)
					{
						num = 34;
						continue;
					}
					goto IL_735;
				case 17:
					goto IL_6D7;
				case 18:
					goto IL_6D7;
				case 19:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 35;
						continue;
					}
					goto IL_34E;
				}
				case 20:
				{
					int num2;
					switch (num2)
					{
					case 0:
						this.\u1717(A_0, A_1);
						num = 40;
						continue;
					case 1:
						A_0.Skip();
						num = 6;
						continue;
					case 2:
						this.ᜁ(A_0, A_1);
						num = 26;
						continue;
					case 3:
						this.ᜂ(A_0, A_1);
						num = 15;
						continue;
					case 4:
						spr\u2306.ᜀ(A_0, A_1, null);
						num = 22;
						continue;
					case 5:
						this.ᜀ(A_0, A_1, A_4);
						num = 1;
						continue;
					case 6:
						xmlWriter.WriteNode(A_0, false);
						num = 2;
						continue;
					case 7:
						this.ᜀ(A_0, A_1, A_3);
						num = 31;
						continue;
					case 8:
						this.\u1716(A_0, A_1);
						num = 29;
						continue;
					case 9:
						this.\u1715(A_0, A_1);
						num = 13;
						continue;
					case 10:
						this.ᜂ(A_0, A_1);
						num = 9;
						continue;
					case 11:
						spr\u2306.ᜀ(A_0, A_1.PageSetup);
						num = 18;
						continue;
					case 12:
						spr\u2306.ᜀ(A_0, A_1.PageSetup, new spr\u1CDC());
						num = 0;
						continue;
					case 13:
						spr\u2306.ᜁ(A_0, (XlsPageSetup)A_1.PageSetup);
						num = 11;
						continue;
					case 14:
						spr\u2306.ᜀ(A_0, (XlsPageSetup)A_1.PageSetup);
						num = 17;
						continue;
					case 15:
						this.ᜁ(A_0, A_1);
						num = 36;
						continue;
					case 16:
						this.ᜀ(A_0, A_1);
						num = 21;
						continue;
					case 17:
						this.ᜅ(A_0, A_1);
						num = 33;
						continue;
					case 18:
						this.ᜇ(A_0, A_1);
						num = 38;
						continue;
					case 19:
						this.ᜀ(A_0, A_1, RecordTableEnumerator.b("䠺唼娾⑀㝂", a_));
						num = 25;
						continue;
					case 20:
						this.ᜏ(A_0, A_1);
						num = 23;
						continue;
					case 21:
						this.ᜈ(A_0, A_1);
						num = 5;
						continue;
					case 22:
						this.ᜂ(A_0, A_1, A_3);
						num = 14;
						continue;
					default:
						num = 8;
						continue;
					}
					break;
				}
				case 21:
					goto IL_6D7;
				case 22:
					goto IL_6D7;
				case 23:
					goto IL_6D7;
				case 24:
					goto IL_6D7;
				case 25:
					goto IL_6D7;
				case 26:
					goto IL_6D7;
				case 27:
					goto IL_25D;
				case 28:
					goto IL_168;
				case 29:
					goto IL_6D7;
				case 30:
					num = 19;
					continue;
				case 31:
					goto IL_6D7;
				case 32:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 27;
						continue;
					}
					num = 37;
					continue;
				case 33:
					goto IL_6D7;
				case 34:
					num = 32;
					continue;
				case 35:
					num = 12;
					continue;
				case 36:
					goto IL_6D7;
				case 37:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 30;
						continue;
					}
					A_0.Skip();
					num = 41;
					continue;
				case 38:
					goto IL_6D7;
				case 39:
					goto IL_34E;
				case 40:
					goto IL_6D7;
				case 41:
					goto IL_6D7;
				}
				break;
				IL_168:
				num = 4;
				continue;
				IL_2B9:
				num = 20;
				continue;
				IL_34E:
				A_0.Skip();
				num = 7;
				continue;
				IL_6D7:
				num = 16;
			}
		}
		IL_25D:
		IL_735:
		xmlWriter.WriteEndElement();
		xmlWriter.Flush();
		A_2.Position = 0L;
	}

	// Token: 0x06004FA8 RID: 20392 RVA: 0x00308FA8 File Offset: 0x00307FA8
	private void \u170D(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 12;
		int num = 9;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					if (true)
					{
					}
					num = 8;
					continue;
				}
				num = 10;
				continue;
			case 1:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 3;
					continue;
				}
				goto IL_EC;
			}
			case 2:
				goto IL_13A;
			case 3:
				num = 14;
				continue;
			case 4:
				goto IL_FD;
			case 5:
				goto IL_13A;
			case 6:
				num = 1;
				continue;
			case 7:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_FD;
				default:
					if (false)
					{
					}
					goto IL_13A;
				}
				break;
			case 8:
				goto IL_123;
			case 10:
				if (A_0.LocalName != RecordTableEnumerator.b("❁㱃㉅ч㥉㡋", a_))
				{
					num = 19;
					continue;
				}
				num = 15;
				continue;
			case 11:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 6;
					continue;
				}
				A_0.Skip();
				num = 2;
				continue;
			case 12:
				goto IL_77;
			case 13:
				this.ᜁ(A_1, A_0);
				num = 5;
				continue;
			case 14:
			{
				string localName;
				if (localName == RecordTableEnumerator.b("❁㱃㉅", a_))
				{
					num = 13;
					continue;
				}
				goto IL_EC;
			}
			case 15:
				if (!A_0.IsEmptyElement)
				{
					num = 16;
					continue;
				}
				goto IL_240;
			case 16:
				A_0.Read();
				num = 7;
				continue;
			case 17:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 18;
					continue;
				}
				num = 11;
				continue;
			case 18:
				goto IL_15D;
			case 19:
				goto IL_1C3;
			}
			if (A_0 == null)
			{
				num = 12;
				continue;
			}
			num = 0;
			continue;
			IL_EC:
			A_0.Skip();
			num = 4;
			continue;
			IL_13A:
			num = 17;
			continue;
			IL_FD:
			goto IL_13A;
		}
		IL_77:
		throw new ArgumentNullException(RecordTableEnumerator.b("ぁ⅃❅ⱇ⽉㹋", a_));
		IL_123:
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅁⱃ⍅ⵇ㹉", a_));
		IL_15D:
		goto IL_240;
		IL_1C3:
		throw new XmlException();
		IL_240:
		A_0.Read();
	}

	// Token: 0x06004FA9 RID: 20393 RVA: 0x003091FC File Offset: 0x003081FC
	private void ᜁ(XlsWorksheet A_0, XmlReader A_1)
	{
		int a_ = 7;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1.NodeType == XmlNodeType.Element)
				{
					num = 8;
					continue;
				}
				A_1.Skip();
				num = 3;
				continue;
			case 1:
				this.ᜀ(A_0, A_1);
				num = 15;
				continue;
			case 2:
				if (!A_1.IsEmptyElement)
				{
					num = 9;
					continue;
				}
				goto IL_23D;
			case 3:
				if (true)
				{
				}
				goto IL_12F;
			case 4:
			{
				string localName;
				if (localName == RecordTableEnumerator.b("丼伾⁀ㅂ⹄⭆⁈╊⡌ࡎ⍐㱒⁔❖⩘", a_))
				{
					num = 1;
					continue;
				}
				goto IL_EC;
			}
			case 6:
				num = 4;
				continue;
			case 7:
				goto IL_FD;
			case 8:
				num = 19;
				continue;
			case 9:
				A_1.Read();
				num = 13;
				continue;
			case 10:
				goto IL_152;
			case 11:
				goto IL_77;
			case 12:
				goto IL_1B8;
			case 13:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_FD;
				default:
					if (false)
					{
					}
					goto IL_12F;
				}
				break;
			case 14:
				goto IL_118;
			case 15:
				goto IL_12F;
			case 16:
				if (A_1 == null)
				{
					num = 14;
					continue;
				}
				num = 17;
				continue;
			case 17:
				if (A_1.LocalName != RecordTableEnumerator.b("堼䜾㕀", a_))
				{
					num = 12;
					continue;
				}
				num = 2;
				continue;
			case 18:
				if (A_1.NodeType == XmlNodeType.EndElement)
				{
					num = 10;
					continue;
				}
				num = 0;
				continue;
			case 19:
			{
				string localName;
				if ((localName = A_1.LocalName) != null)
				{
					num = 6;
					continue;
				}
				goto IL_EC;
			}
			}
			if (A_0 == null)
			{
				num = 11;
				continue;
			}
			num = 16;
			continue;
			IL_EC:
			A_1.Skip();
			num = 7;
			continue;
			IL_12F:
			num = 18;
			continue;
			IL_FD:
			goto IL_12F;
		}
		IL_77:
		throw new ArgumentNullException(RecordTableEnumerator.b("丼圾⑀♂ㅄ", a_));
		IL_118:
		throw new ArgumentNullException(RecordTableEnumerator.b("似娾⁀❂⁄㕆", a_));
		IL_152:
		goto IL_23D;
		IL_1B8:
		throw new XmlException();
		IL_23D:
		A_1.Read();
	}

	// Token: 0x06004FAA RID: 20394 RVA: 0x00309450 File Offset: 0x00308450
	private void ᜀ(XlsWorksheet A_0, XmlReader A_1)
	{
		int a_ = 8;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_144;
			case 1:
				if (A_1.LocalName != RecordTableEnumerator.b("䴽〿⍁㙃ⵅ⑇⍉≋⭍ᝏ⁑㭓⍕⡗⥙", a_))
				{
					num = 4;
					continue;
				}
				num = 10;
				continue;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_FD;
				default:
					if (false)
					{
					}
					goto IL_144;
				}
				break;
			case 4:
				goto IL_1CD;
			case 5:
				goto IL_144;
			case 6:
				goto IL_77;
			case 7:
				goto IL_11B;
			case 8:
			{
				string localName;
				if (localName == RecordTableEnumerator.b("䴽〿⍁㙃ⵅ⑇⍉≋⭍ᝏ⁑㭓⍕⡗", a_))
				{
					num = 9;
					continue;
				}
				goto IL_EC;
			}
			case 9:
				A_0.SparklineGroups.Add(this.ᜌ(A_1, A_0));
				A_1.Read();
				num = 0;
				continue;
			case 10:
				if (!A_1.IsEmptyElement)
				{
					num = 19;
					continue;
				}
				return;
			case 11:
				if (A_1.NodeType == XmlNodeType.Element)
				{
					num = 13;
					continue;
				}
				A_1.Skip();
				num = 5;
				continue;
			case 12:
			{
				string localName;
				if ((localName = A_1.LocalName) != null)
				{
					num = 17;
					continue;
				}
				goto IL_EC;
			}
			case 13:
				num = 12;
				continue;
			case 14:
				if (A_1.NodeType == XmlNodeType.EndElement)
				{
					num = 16;
					continue;
				}
				num = 11;
				continue;
			case 15:
				if (A_1 == null)
				{
					num = 7;
					continue;
				}
				num = 1;
				continue;
			case 16:
				goto IL_167;
			case 17:
				num = 8;
				continue;
			case 18:
				goto IL_FD;
			case 19:
				A_1.Read();
				num = 3;
				continue;
			}
			if (A_0 == null)
			{
				num = 6;
				continue;
			}
			num = 15;
			continue;
			IL_EC:
			A_1.Skip();
			num = 18;
			continue;
			IL_144:
			num = 14;
			continue;
			IL_FD:
			goto IL_144;
		}
		IL_77:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴽⠿❁⅃㉅", a_));
		IL_11B:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰽┿⍁⁃⍅㩇", a_));
		IL_167:
		return;
		IL_1CD:
		if (true)
		{
		}
		throw new XmlException();
	}

	// Token: 0x06004FAB RID: 20395 RVA: 0x003096B0 File Offset: 0x003086B0
	private SparklineGroup ᜌ(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			int num = 123;
			SparklineGroup sparklineGroup;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("圾⡀⑂ⵄ", a_)))
					{
						num = 34;
						continue;
					}
					goto IL_10D1;
				case 1:
					goto IL_FFE;
				case 2:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("刾⁀㭂ф㽆⁈㡊᥌㙎⅐㙒", a_)))
					{
						num = 36;
						continue;
					}
					goto IL_299;
				case 3:
					if (true)
					{
					}
					goto IL_1107;
				case 4:
					goto IL_975;
				case 5:
				{
					string value;
					if (!(value == RecordTableEnumerator.b("䰾㕀≂♄ⱆⱈ⽊", a_)))
					{
						num = 113;
						continue;
					}
					sparklineGroup.SparklineType = SparklineType.Stacked;
					num = 4;
					continue;
				}
				case 6:
					goto IL_1107;
				case 7:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䴾⡀⑂ⵄ㍆ᵈ⑊Ō⩎㝐❒", a_)))
					{
						num = 119;
						continue;
					}
					goto IL_E80;
				case 8:
				{
					string value2;
					if (!(value2 == RecordTableEnumerator.b("尾㑀あㅄ⡆⑈", a_)))
					{
						num = 51;
						continue;
					}
					sparklineGroup.VerticalAxisMinimum.ᜀ(SpartlineVerticalAxisType.Custom);
					num = 12;
					continue;
				}
				case 9:
					sparklineGroup.LineWeight = XmlConvert.ToDouble(A_0.Value);
					num = 97;
					continue;
				case 10:
					goto IL_1131;
				case 11:
					goto IL_975;
				case 12:
					goto IL_1131;
				case 13:
					sparklineGroup.VerticalAxisMaximum.ᜀ(Convert.ToDouble(A_0.Value));
					num = 133;
					continue;
				case 14:
					sparklineGroup.IsHorizontalDateAxis = true;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1023;
					default:
						if (false)
						{
						}
						num = 104;
						continue;
					}
					break;
				case 15:
					num = 132;
					continue;
				case 16:
					goto IL_299;
				case 17:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 111;
						continue;
					}
					goto IL_44E;
				}
				case 18:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 117;
						continue;
					}
					A_0.Read();
					num = 126;
					continue;
				case 19:
					goto IL_640;
				case 20:
					goto IL_640;
				case 21:
					sparklineGroup.ShowLastPoint = XmlConvert.ToBoolean(A_0.Value);
					num = 125;
					continue;
				case 22:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䬾㡀㍂⁄", a_)))
					{
						num = 59;
						continue;
					}
					goto IL_975;
				case 23:
					goto IL_5C6;
				case 24:
				{
					string value;
					if (!(value == RecordTableEnumerator.b("尾⹀⽂い⩆❈", a_)))
					{
						num = 114;
						continue;
					}
					sparklineGroup.SparklineType = SparklineType.Column;
					num = 128;
					continue;
				}
				case 25:
					sparklineGroup.ShowHorizontalAxis = XmlConvert.ToBoolean(A_0.Value);
					num = 69;
					continue;
				case 26:
					spr\u22D2.\u177F = new Dictionary<string, int>(10)
					{
						{
							RecordTableEnumerator.b("尾⹀⽂⩄㕆ᩈ⹊㽌♎㑐⁒", a_),
							0
						},
						{
							RecordTableEnumerator.b("尾⹀⽂⩄㕆݈⹊⩌⹎═㩒⍔㉖", a_),
							1
						},
						{
							RecordTableEnumerator.b("尾⹀⽂⩄㕆ࡈ㍊⑌㱎", a_),
							2
						},
						{
							RecordTableEnumerator.b("尾⹀⽂⩄㕆ш⩊㽌⑎㑐⅒♔", a_),
							3
						},
						{
							RecordTableEnumerator.b("尾⹀⽂⩄㕆཈≊㽌㱎═", a_),
							4
						},
						{
							RecordTableEnumerator.b("尾⹀⽂⩄㕆Ո⩊㹌㭎", a_),
							5
						},
						{
							RecordTableEnumerator.b("尾⹀⽂⩄㕆ň≊⩌❎", a_),
							6
						},
						{
							RecordTableEnumerator.b("尾⹀⽂⩄㕆Ո⑊㩌", a_),
							7
						},
						{
							RecordTableEnumerator.b("夾", a_),
							8
						},
						{
							RecordTableEnumerator.b("䰾ㅀ≂㝄ⱆ╈≊⍌⩎≐", a_),
							9
						}
					};
					num = 72;
					continue;
				case 27:
					goto IL_1131;
				case 28:
					sparklineGroup.ShowMarkers = XmlConvert.ToBoolean(A_0.Value);
					num = 23;
					continue;
				case 29:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("匾⁀あㅄ", a_)))
					{
						num = 21;
						continue;
					}
					goto IL_573;
				case 30:
					sparklineGroup.IsDisplayHidden = XmlConvert.ToBoolean(A_0.Value);
					num = 127;
					continue;
				case 31:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("匾⡀ⵂ⁄၆ⱈ≊⩌❎═", a_)))
					{
						num = 9;
						continue;
					}
					goto IL_D94;
				case 32:
					goto IL_1107;
				case 33:
					num = 24;
					continue;
				case 34:
					sparklineGroup.ShowHighPoint = XmlConvert.ToBoolean(A_0.Value);
					num = 96;
					continue;
				case 35:
				{
					int num2;
					switch (num2)
					{
					case 0:
						sparklineGroup.SparklineColor = this.ᜏ(A_0).ᜁ(this.ᜉ);
						num = 45;
						continue;
					case 1:
						sparklineGroup.NegativePointColor = this.ᜏ(A_0).ᜁ(this.ᜉ);
						num = 99;
						continue;
					case 2:
						sparklineGroup.HorizontalAxisColor = this.ᜏ(A_0).ᜁ(this.ᜉ);
						num = 6;
						continue;
					case 3:
						sparklineGroup.MarkersColor = this.ᜏ(A_0).ᜁ(this.ᜉ);
						num = 39;
						continue;
					case 4:
						sparklineGroup.FirstPointColor = this.ᜏ(A_0).ᜁ(this.ᜉ);
						num = 58;
						continue;
					case 5:
						sparklineGroup.LastPointColor = this.ᜏ(A_0).ᜁ(this.ᜉ);
						num = 47;
						continue;
					case 6:
						sparklineGroup.HighPointColor = this.ᜏ(A_0).ᜁ(this.ᜉ);
						num = 3;
						continue;
					case 7:
						sparklineGroup.LowPointColor = this.ᜏ(A_0).ᜁ(this.ᜉ);
						num = 32;
						continue;
					case 8:
					{
						bool isEmptyElement = A_0.IsEmptyElement;
						num = 92;
						continue;
					}
					case 9:
						sparklineGroup.Add(this.ᜋ(A_0, A_1));
						num = 57;
						continue;
					default:
						num = 15;
						continue;
					}
					break;
				}
				case 36:
					num = 107;
					continue;
				case 37:
					goto IL_7ED;
				case 38:
				{
					string localName;
					int num2;
					if (spr\u22D2.\u177F.TryGetValue(localName, out num2))
					{
						num = 116;
						continue;
					}
					goto IL_44E;
				}
				case 39:
					goto IL_1107;
				case 40:
					goto IL_E0F;
				case 41:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("嬾⡀あ㕄⭆⡈㉊ࡌ≎⅐❒ⱔᑖ㱘㝚ㅜⱞ⁠ၢ", a_)))
					{
						num = 94;
						continue;
					}
					goto IL_640;
				case 42:
					num = 73;
					continue;
				case 43:
					num = 100;
					continue;
				case 44:
					num = 50;
					continue;
				case 45:
					goto IL_1107;
				case 46:
					num = 20;
					continue;
				case 47:
					goto IL_1107;
				case 48:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 124;
						continue;
					}
					num = 18;
					continue;
				case 49:
					num = 8;
					continue;
				case 50:
				{
					string value3;
					if (!(value3 == RecordTableEnumerator.b("尾㑀あㅄ⡆⑈", a_)))
					{
						num = 53;
						continue;
					}
					sparklineGroup.VerticalAxisMaximum.ᜀ(SpartlineVerticalAxisType.Custom);
					num = 88;
					continue;
				}
				case 51:
					num = 91;
					continue;
				case 52:
					goto IL_E80;
				case 53:
					num = 16;
					continue;
				case 54:
					num = 106;
					continue;
				case 55:
					goto IL_1107;
				case 56:
					goto IL_299;
				case 57:
					goto IL_1107;
				case 58:
					goto IL_1107;
				case 59:
					num = 102;
					continue;
				case 60:
					goto IL_1107;
				case 61:
				{
					string value2;
					if (!(value2 == RecordTableEnumerator.b("堾㍀ⱂい㝆", a_)))
					{
						num = 49;
						continue;
					}
					sparklineGroup.VerticalAxisMinimum.ᜀ(SpartlineVerticalAxisType.Same);
					num = 10;
					continue;
				}
				case 62:
				{
					string value3;
					if (!(value3 == RecordTableEnumerator.b("堾㍀ⱂい㝆", a_)))
					{
						num = 44;
						continue;
					}
					sparklineGroup.VerticalAxisMaximum.ᜀ(SpartlineVerticalAxisType.Same);
					num = 67;
					continue;
				}
				case 63:
				{
					string value;
					if (!(value == RecordTableEnumerator.b("匾⡀ⵂ⁄", a_)))
					{
						num = 33;
						continue;
					}
					sparklineGroup.SparklineType = SparklineType.Line;
					num = 121;
					continue;
				}
				case 64:
					num = 109;
					continue;
				case 65:
				{
					string value2;
					if ((value2 = A_0.Value) != null)
					{
						num = 42;
						continue;
					}
					goto IL_1131;
				}
				case 66:
					num = 61;
					continue;
				case 67:
					goto IL_299;
				case 68:
					goto IL_747;
				case 69:
					goto IL_BF5;
				case 70:
				{
					string value4;
					if (!(value4 == RecordTableEnumerator.b("䰾ㅀ≂⭄", a_)))
					{
						num = 43;
						continue;
					}
					sparklineGroup.EmptyCellsType = SparklineEmptyCells.Line;
					num = 19;
					continue;
				}
				case 71:
					sparklineGroup.VerticalAxisMinimum.ᜀ(Convert.ToDouble(A_0.Value));
					num = 81;
					continue;
				case 72:
					goto IL_522;
				case 73:
				{
					string value2;
					if (!(value2 == RecordTableEnumerator.b("嘾⽀❂ⱄㅆ⁈⽊㡌⹎㵐", a_)))
					{
						num = 66;
						continue;
					}
					sparklineGroup.VerticalAxisMinimum.ᜀ(SpartlineVerticalAxisType.Automatic);
					num = 27;
					continue;
				}
				case 74:
					num = 65;
					continue;
				case 75:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("夾⡀ㅂ㙄㍆", a_)))
					{
						goto IL_1023;
					}
					goto IL_747;
				case 76:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("刾⡀ⵂф㽆⁈㡊᥌㙎⅐㙒", a_)))
					{
						num = 74;
						continue;
					}
					goto IL_1131;
				case 77:
					sparklineGroup.ShowFirstPoint = XmlConvert.ToBoolean(A_0.Value);
					num = 68;
					continue;
				case 78:
					num = 63;
					continue;
				case 79:
					num = 70;
					continue;
				case 80:
				{
					bool isEmptyElement;
					if (!isEmptyElement)
					{
						num = 105;
						continue;
					}
					goto IL_C7E;
				}
				case 81:
					goto IL_263;
				case 82:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("刾⁀ㅂ⹄≆㭈㡊", a_)))
					{
						num = 28;
						continue;
					}
					goto IL_5C6;
				case 83:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("嬾⁀㝂⁄نㅈ≊㹌", a_)))
					{
						num = 14;
						continue;
					}
					goto IL_A8F;
				case 84:
					A_0.Read();
					num = 55;
					continue;
				case 85:
					A_0.Read();
					num = 89;
					continue;
				case 86:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("刾⁀ⵂい♆╈يⱌ㝎", a_)))
					{
						num = 13;
						continue;
					}
					goto IL_358;
				case 87:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("嬾⡀あ㕄⭆⡈㉊ᕌ๎⥐㩒♔", a_)))
					{
						num = 25;
						continue;
					}
					goto IL_BF5;
				case 88:
					goto IL_299;
				case 89:
					goto IL_7F2;
				case 90:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("儾⑀⑂⑄㍆⁈㵊⡌", a_)))
					{
						num = 110;
						continue;
					}
					goto IL_CCC;
				case 91:
					goto IL_1131;
				case 92:
				{
					bool isEmptyElement;
					if (!isEmptyElement)
					{
						num = 85;
						continue;
					}
					goto IL_7F2;
				}
				case 93:
					if (spr\u22D2.\u177F == null)
					{
						num = 26;
						continue;
					}
					goto IL_522;
				case 94:
					num = 118;
					continue;
				case 95:
					goto IL_CCC;
				case 96:
					goto IL_10D1;
				case 97:
					goto IL_D94;
				case 98:
					num = 62;
					continue;
				case 99:
					goto IL_1107;
				case 100:
				{
					string value4;
					if (!(value4 == RecordTableEnumerator.b("堾⁀㍂", a_)))
					{
						num = 54;
						continue;
					}
					sparklineGroup.EmptyCellsType = SparklineEmptyCells.Gaps;
					num = 122;
					continue;
				}
				case 101:
					goto IL_25E;
				case 102:
				{
					string value;
					if ((value = A_0.Value) != null)
					{
						num = 78;
						continue;
					}
					goto IL_975;
				}
				case 103:
					goto IL_640;
				case 104:
					goto IL_A8F;
				case 105:
					A_0.Skip();
					num = 115;
					continue;
				case 106:
				{
					string value4;
					if (!(value4 == RecordTableEnumerator.b("䔾⑀ㅂ⩄", a_)))
					{
						num = 46;
						continue;
					}
					sparklineGroup.EmptyCellsType = SparklineEmptyCells.Zero;
					num = 103;
					continue;
				}
				case 107:
				{
					string value3;
					if ((value3 = A_0.Value) != null)
					{
						num = 64;
						continue;
					}
					goto IL_299;
				}
				case 108:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("嬾⡀あ㕄⭆⡈㉊Ռ♎㕐㝒ご㥖", a_)))
					{
						num = 30;
						continue;
					}
					goto IL_E4A;
				case 109:
				{
					string value3;
					if (!(value3 == RecordTableEnumerator.b("嘾⽀❂ⱄㅆ⁈⽊㡌⹎㵐", a_)))
					{
						num = 98;
						continue;
					}
					sparklineGroup.VerticalAxisMaximum.ᜀ(SpartlineVerticalAxisType.Automatic);
					num = 56;
					continue;
				}
				case 110:
					sparklineGroup.ShowNegativePoint = XmlConvert.ToBoolean(A_0.Value);
					num = 95;
					continue;
				case 111:
					num = 93;
					continue;
				case 112:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("匾⹀㑂", a_)))
					{
						num = 130;
						continue;
					}
					goto IL_FFE;
				case 113:
					num = 11;
					continue;
				case 114:
					num = 5;
					continue;
				case 115:
					goto IL_C7E;
				case 116:
					num = 35;
					continue;
				case 117:
					num = 17;
					continue;
				case 118:
				{
					string value4;
					if ((value4 = A_0.Value) != null)
					{
						num = 79;
						continue;
					}
					goto IL_640;
				}
				case 119:
					sparklineGroup.PlotRightToLeft = true;
					num = 52;
					continue;
				case 120:
					if (A_1 == null)
					{
						num = 40;
						continue;
					}
					num = 129;
					continue;
				case 121:
					goto IL_975;
				case 122:
					goto IL_640;
				case 124:
					goto IL_112C;
				case 125:
					goto IL_573;
				case 126:
					goto IL_1107;
				case 127:
					goto IL_E4A;
				case 128:
					goto IL_975;
				case 129:
					if (A_0.LocalName != RecordTableEnumerator.b("䰾ㅀ≂㝄ⱆ╈≊⍌⩎ᙐ⅒㩔≖⥘", a_))
					{
						num = 37;
						continue;
					}
					sparklineGroup = new SparklineGroup(A_1.ParentWorkbook);
					num = 31;
					continue;
				case 130:
					sparklineGroup.ShowLowPoint = XmlConvert.ToBoolean(A_0.Value);
					num = 1;
					continue;
				case 131:
					goto IL_1107;
				case 132:
					goto IL_44E;
				case 133:
					goto IL_358;
				case 134:
					if (!A_0.IsEmptyElement)
					{
						num = 84;
						continue;
					}
					goto IL_11B9;
				case 135:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("刾⁀ⵂい♆╈ي⑌ⅎ", a_)))
					{
						num = 71;
						continue;
					}
					goto IL_263;
				}
				if (A_0 == null)
				{
					num = 101;
					continue;
				}
				num = 120;
				continue;
				IL_263:
				num = 7;
				continue;
				IL_299:
				num = 76;
				continue;
				IL_358:
				num = 135;
				continue;
				IL_44E:
				A_0.Read();
				num = 60;
				continue;
				IL_522:
				num = 38;
				continue;
				IL_573:
				num = 90;
				continue;
				IL_5C6:
				num = 0;
				continue;
				IL_640:
				num = 82;
				continue;
				IL_747:
				num = 29;
				continue;
				IL_7F2:
				sparklineGroup.HorizontalDateAxisRange = (CellRange)A_1.Range[A_0.Value];
				num = 80;
				continue;
				IL_975:
				num = 83;
				continue;
				IL_A8F:
				num = 41;
				continue;
				IL_BF5:
				num = 108;
				continue;
				IL_C7E:
				A_0.Skip();
				num = 131;
				continue;
				IL_CCC:
				num = 87;
				continue;
				IL_D94:
				num = 22;
				continue;
				IL_E4A:
				num = 2;
				continue;
				IL_E80:
				num = 134;
				continue;
				IL_FFE:
				num = 75;
				continue;
				IL_1023:
				num = 77;
				continue;
				IL_10D1:
				num = 112;
				continue;
				IL_1107:
				num = 48;
				continue;
				IL_1131:
				num = 86;
			}
			IL_25E:
			throw new ArgumentNullException(RecordTableEnumerator.b("䴾⑀≂⅄≆㭈", a_));
			IL_7ED:
			throw new XmlException();
			IL_E0F:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰾⥀♂⁄㍆", a_));
			IL_112C:
			IL_11B9:
			A_0.Read();
			return sparklineGroup;
		}
		}
	}

	// Token: 0x06004FAC RID: 20396 RVA: 0x0030A880 File Offset: 0x00309880
	private SparklineCollection ᜋ(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 9;
		int num = 2;
		SparklineCollection sparklineCollection;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					num = 17;
					continue;
				}
				num = 18;
				continue;
			case 1:
				if (true)
				{
				}
				sparklineCollection.Add(this.ᜊ(A_0, A_1));
				A_0.Read();
				num = 13;
				continue;
			case 3:
				goto IL_144;
			case 4:
				goto IL_144;
			case 5:
				goto IL_234;
			case 6:
				A_0.Read();
				num = 14;
				continue;
			case 7:
				if (!A_0.IsEmptyElement)
				{
					num = 6;
					continue;
				}
				goto IL_234;
			case 8:
				IL_14F:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 5;
					continue;
				}
				num = 11;
				continue;
			case 9:
				num = 10;
				continue;
			case 10:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 16;
					continue;
				}
				goto IL_F4;
			}
			case 11:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 9;
					continue;
				}
				A_0.Skip();
				num = 3;
				continue;
			case 12:
			{
				string localName;
				if (localName == RecordTableEnumerator.b("䰾ㅀ≂㝄ⱆ╈≊⍌⩎", a_))
				{
					num = 1;
					continue;
				}
				goto IL_F4;
			}
			case 13:
				goto IL_144;
			case 14:
				goto IL_144;
			case 15:
				goto IL_77;
			case 16:
				num = 12;
				continue;
			case 17:
				goto IL_120;
			case 18:
				if (A_0.LocalName != RecordTableEnumerator.b("䰾ㅀ≂㝄ⱆ╈≊⍌⩎≐", a_))
				{
					num = 19;
					continue;
				}
				sparklineCollection = new SparklineCollection();
				num = 7;
				continue;
			case 19:
				goto IL_1B1;
			}
			if (A_0 == null)
			{
				num = 15;
				continue;
			}
			num = 0;
			continue;
			IL_234:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_14F;
			default:
				goto IL_24A;
			}
			IL_F4:
			A_0.Skip();
			num = 4;
			continue;
			IL_144:
			num = 8;
		}
		IL_77:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴾⑀≂⅄≆㭈", a_));
		IL_120:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰾⥀♂⁄㍆", a_));
		IL_1B1:
		throw new XmlException();
		IL_24A:
		if (false)
		{
		}
		return sparklineCollection;
	}

	// Token: 0x06004FAD RID: 20397 RVA: 0x0030AAE0 File Offset: 0x00309AE0
	private Sparkline ᜊ(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 10;
		int num = 20;
		Sparkline sparkline;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!A_0.IsEmptyElement)
				{
					num = 33;
					continue;
				}
				goto IL_410;
			case 1:
				goto IL_131;
			case 2:
				goto IL_1D1;
			case 3:
				num = 13;
				continue;
			case 4:
				goto IL_3D3;
			case 5:
				goto IL_2A0;
			case 6:
				goto IL_1D3;
			case 7:
			{
				bool isEmptyElement;
				if (!isEmptyElement)
				{
					num = 28;
					continue;
				}
				goto IL_396;
			}
			case 8:
				goto IL_1D3;
			case 9:
				A_0.Skip();
				num = 1;
				continue;
			case 10:
				A_0.Skip();
				num = 24;
				continue;
			case 11:
				if (A_1 == null)
				{
					num = 22;
					continue;
				}
				num = 27;
				continue;
			case 12:
				num = 19;
				continue;
			case 13:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("㌿㍁㙃⍅⹇", a_)))
				{
					num = 21;
					continue;
				}
				bool isEmptyElement = A_0.IsEmptyElement;
				num = 7;
				continue;
			}
			case 14:
				goto IL_2CD;
			case 15:
				goto IL_1D3;
			case 16:
				goto IL_BD;
			case 17:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 35;
					continue;
				}
				num = 18;
				continue;
			case 18:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 12;
					continue;
				}
				A_0.Skip();
				num = 8;
				continue;
			case 19:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 23;
					continue;
				}
				goto IL_2CD;
			}
			case 21:
				num = 14;
				continue;
			case 22:
				goto IL_391;
			case 23:
				num = 26;
				continue;
			case 24:
				goto IL_1BF;
			case 25:
				goto IL_396;
			case 26:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("☿", a_)))
				{
					num = 3;
					continue;
				}
				bool isEmptyElement = A_0.IsEmptyElement;
				num = 29;
				continue;
			}
			case 27:
				if (A_0.LocalName != RecordTableEnumerator.b("㌿㉁╃㑅⍇♉╋⁍㕏", a_))
				{
					num = 5;
					continue;
				}
				sparkline = new Sparkline();
				num = 0;
				continue;
			case 28:
				A_0.Read();
				num = 25;
				continue;
			case 29:
			{
				bool isEmptyElement;
				if (!isEmptyElement)
				{
					num = 34;
					continue;
				}
				goto IL_3D3;
			}
			case 30:
				goto IL_1D3;
			case 31:
			{
				bool isEmptyElement;
				if (!isEmptyElement)
				{
					num = 10;
					continue;
				}
				goto IL_1BF;
			}
			case 32:
			{
				bool isEmptyElement;
				if (!isEmptyElement)
				{
					num = 9;
					continue;
				}
				goto IL_131;
			}
			case 33:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1D1;
				default:
					if (false)
					{
					}
					A_0.Read();
					num = 30;
					continue;
				}
				break;
			case 34:
				A_0.Read();
				num = 4;
				continue;
			case 35:
				goto IL_1F6;
			}
			if (A_0 == null)
			{
				num = 16;
				continue;
			}
			num = 11;
			continue;
			IL_131:
			A_0.Skip();
			num = 6;
			continue;
			IL_1BF:
			A_0.Read();
			num = 2;
			continue;
			IL_1D3:
			num = 17;
			continue;
			IL_1D1:
			goto IL_1D3;
			IL_2CD:
			A_0.Skip();
			num = 15;
			continue;
			IL_396:
			sparkline.RefRange = (CellRange)A_1.Range[A_0.Value];
			num = 31;
			continue;
			IL_3D3:
			sparkline.DataRange = (CellRange)A_1.Range[A_0.Value];
			num = 32;
		}
		IL_BD:
		throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁╃≅ⵇ㡉", a_));
		IL_1F6:
		goto IL_410;
		IL_2A0:
		throw new XmlException();
		IL_391:
		throw new ArgumentNullException(RecordTableEnumerator.b("㌿⩁⅃⍅㱇", a_));
		IL_410:
		if (true)
		{
		}
		return sparkline;
	}

	// Token: 0x06004FAE RID: 20398 RVA: 0x0030AF08 File Offset: 0x00309F08
	private void ᜂ(XmlReader A_0, XlsWorksheetBase A_1)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			int num = 15;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_17D;
				case 1:
				{
					string localName;
					if (localName == RecordTableEnumerator.b("ⵁ⡃⍅݇⡉♋⭍㍏♑", a_))
					{
						num = 16;
						continue;
					}
					goto IL_122;
				}
				case 2:
					goto IL_17D;
				case 3:
					A_0.Read();
					num = 0;
					continue;
				case 4:
					goto IL_11D;
				case 5:
					num = 1;
					continue;
				case 6:
				{
					XlsWorksheet xlsWorksheet;
					if (xlsWorksheet == null)
					{
						num = 21;
						continue;
					}
					sprᜭ sprᜭ = (sprᜭ)xlsWorksheet.OleObjects;
					num = 7;
					continue;
				}
				case 7:
					if (true)
					{
					}
					if (!A_0.IsEmptyElement)
					{
						num = 3;
						continue;
					}
					return;
				case 8:
					goto IL_17D;
				case 9:
				{
					if (A_0.LocalName != RecordTableEnumerator.b("ⵁ⡃⍅݇⡉♋⭍㍏♑❓", a_))
					{
						num = 4;
						continue;
					}
					XlsWorksheet xlsWorksheet = A_1 as XlsWorksheet;
					num = 6;
					continue;
				}
				case 10:
					if (A_1 == null)
					{
						num = 13;
						continue;
					}
					num = 9;
					continue;
				case 11:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 5;
						continue;
					}
					goto IL_122;
				}
				case 12:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_87;
					default:
						if (false)
						{
						}
						if (A_0.NodeType == XmlNodeType.EndElement)
						{
							num = 18;
							continue;
						}
						num = 19;
						continue;
					}
					break;
				case 13:
					goto IL_151;
				case 14:
					num = 11;
					continue;
				case 16:
				{
					XlsWorksheet xlsWorksheet;
					sprᰑ a_2 = this.ᜉ(A_0, xlsWorksheet);
					sprᜭ sprᜭ;
					sprᜭ.ᜁ(a_2);
					num = 20;
					continue;
				}
				case 17:
					goto IL_90;
				case 18:
					goto IL_1BE;
				case 19:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 14;
						continue;
					}
					A_0.Skip();
					num = 2;
					continue;
				case 20:
					goto IL_17D;
				case 21:
					return;
				}
				goto IL_81;
				IL_87:
				num = 17;
				continue;
				IL_81:
				if (A_0 == null)
				{
					goto IL_87;
				}
				num = 10;
				continue;
				IL_122:
				A_0.Skip();
				num = 8;
				continue;
				IL_17D:
				num = 12;
			}
			IL_90:
			throw new ArgumentNullException(RecordTableEnumerator.b("ぁ⅃❅ⱇ⽉㹋", a_));
			IL_11D:
			throw new XmlException();
			IL_151:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅁⱃ⍅ⵇ㹉", a_));
			IL_1BE:
			return;
		}
		}
	}

	// Token: 0x06004FAF RID: 20399 RVA: 0x0030B1CC File Offset: 0x0030A1CC
	private sprᰑ ᜉ(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 14;
		switch (0)
		{
		default:
		{
			int num = 6;
			sprᰑ sprᰑ;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.LocalName != RecordTableEnumerator.b("⭃⩅ⵇՉ⹋⑍㕏ㅑ⁓", a_))
					{
						num = 19;
						continue;
					}
					sprᰑ = new sprᰑ(A_1);
					num = 3;
					continue;
				case 1:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("⁃ぅे㥉㱋⭍㍏♑", a_)))
					{
						num = 4;
						continue;
					}
					goto IL_31D;
				case 2:
					goto IL_255;
				case 3:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㑃㑅❇ⵉՋ⩍", a_)))
					{
						num = 12;
						continue;
					}
					goto IL_1CF;
				case 4:
					num = 9;
					continue;
				case 5:
				{
					if (true)
					{
					}
					sprᰑ.ᜀ(OleLinkType.Link);
					string a_2 = this.ᜀ(A_0.Value, sprᰑ);
					sprᰑ.ᜈ(a_2);
					num = 14;
					continue;
				}
				case 7:
					goto IL_31D;
				case 8:
					goto IL_15F;
				case 9:
					if (A_0.Value == DVAspect.DVASPECT_ICON.ToString())
					{
						num = 11;
						continue;
					}
					goto IL_31D;
				case 10:
					sprᰑ.ᜀ(XmlConvert.ToInt32(A_0.Value));
					num = 2;
					continue;
				case 11:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2B2;
					default:
						if (false)
						{
						}
						sprᰑ.ᜀ(true);
						num = 7;
						continue;
					}
					break;
				case 12:
					sprᰑ.ᜀ(spr\u20E9.ᜁ(A_0.Value));
					num = 18;
					continue;
				case 13:
					goto IL_19C;
				case 14:
					goto IL_19C;
				case 15:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㝃⹅⥇㩉⥋ݍ㑏", a_)))
					{
						num = 10;
						continue;
					}
					goto IL_361;
				case 16:
				{
					string value = A_0.Value;
					sprᡟ sprᡟ = A_1.DataHolder;
					sprᡟ.ᜀ(A_1, value, sprᰑ);
					sprᰑ.ᜂ(value);
					sprᰑ.ᜀ(OleLinkType.Embed);
					num = 13;
					continue;
				}
				case 17:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("⡃⽅♇ⅉ", a_)))
					{
						num = 5;
						continue;
					}
					goto IL_13E;
				case 18:
					goto IL_1CF;
				case 19:
					goto IL_139;
				case 20:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("ⵃ≅", a_), RecordTableEnumerator.b("ⱃ㉅㱇㩉癋慍罏⅑㝓㹕㵗㝙㵛ⵝ也ൡᑣͥ٧ቩūɭᙯᵱٳ᭵᥷๹ཻ偽ꦅ킓秊ﮗﮝ캟횡讣钥颧骩骫膭슯ힱ\ud8b3ힵ첷펹펻킽뎿꫁귃뛅믇", a_)))
					{
						num = 16;
						continue;
					}
					num = 17;
					continue;
				case 21:
					goto IL_94;
				case 22:
					if (A_1 == null)
					{
						num = 8;
						continue;
					}
					num = 0;
					continue;
				}
				if (A_0 == null)
				{
					num = 21;
					continue;
				}
				num = 22;
				continue;
				IL_19C:
				num = 15;
				continue;
				IL_1CF:
				num = 1;
				continue;
				IL_31D:
				num = 20;
			}
			IL_94:
			throw new ArgumentNullException(RecordTableEnumerator.b("㙃⍅⥇⹉⥋㱍", a_));
			IL_139:
			throw new XmlException();
			IL_13E:
			throw new XmlException();
			IL_15F:
			goto IL_2B2;
			IL_255:
			goto IL_361;
			IL_2B2:
			throw new ArgumentNullException(RecordTableEnumerator.b("㝃⹅ⵇ⽉㡋", a_));
			IL_361:
			A_0.Read();
			return sprᰑ;
		}
		}
	}

	// Token: 0x06004FB0 RID: 20400 RVA: 0x0030B544 File Offset: 0x0030A544
	private string ᜀ(string A_0, sprᰑ A_1)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			string text;
			string text2;
			for (;;)
			{
				int num = A_0.IndexOf('!');
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						num2 = 8;
						continue;
					case 1:
						goto IL_19B;
					case 2:
						if (num < 0)
						{
							num2 = 6;
							continue;
						}
						text = A_0.Substring(0, num);
						text2 = A_0.Substring(num + 1, A_0.Length - num - 1);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_13C;
						default:
							if (false)
							{
							}
							num2 = 9;
							continue;
						}
						break;
					case 3:
						num2 = 4;
						continue;
					case 4:
						if (text[text.Length - 1] != ']')
						{
							num2 = 1;
							continue;
						}
						if (true)
						{
						}
						text = text.Substring(1, text.Length - 2);
						num2 = 7;
						continue;
					case 5:
						goto IL_13A;
					case 6:
						goto IL_75;
					case 7:
						if (text2[0] == '\'')
						{
							num2 = 0;
							continue;
						}
						goto IL_1A0;
					case 8:
						if (text2[text2.Length - 1] == '\'')
						{
							num2 = 10;
							continue;
						}
						goto IL_1A0;
					case 9:
						if (text[0] == '[')
						{
							num2 = 3;
							continue;
						}
						goto IL_BA;
					case 10:
						text2 = text2.Substring(1, text2.Length - 2);
						num2 = 5;
						continue;
					}
					break;
				}
			}
			IL_75:
			goto IL_13C;
			IL_BA:
			throw new XmlException();
			IL_13A:
			goto IL_1A0;
			IL_13C:
			throw new XmlException();
			IL_19B:
			goto IL_BA;
			IL_1A0:
			int num3 = int.Parse(text);
			text2 = text2.Replace(RecordTableEnumerator.b("ᠾ晀", a_), RecordTableEnumerator.b("ᠾ", a_));
			XlsExternWorkbook xlsExternWorkbook = this.ᜉ.ExternWorkbooks[num3 - 1];
			return xlsExternWorkbook.URL;
		}
		}
	}

	// Token: 0x06004FB1 RID: 20401 RVA: 0x0030B738 File Offset: 0x0030A738
	private void ᜂ(XmlReader A_0, XlsWorksheet A_1, string A_2)
	{
		int a_ = 12;
		for (;;)
		{
			IL_09:
			int num = 10;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_77;
				case 1:
					if (A_0 == null)
					{
						num = 8;
						continue;
					}
					new List<string>();
					num = 16;
					continue;
				case 2:
					num = 12;
					continue;
				case 3:
					goto IL_14A;
				case 4:
					goto IL_14A;
				case 5:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 2;
						continue;
					}
					A_0.Skip();
					num = 6;
					continue;
				case 6:
					goto IL_14A;
				case 7:
					A_0.Read();
					num = 4;
					continue;
				case 8:
					goto IL_117;
				case 9:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 17;
						continue;
					}
					num = 5;
					continue;
				case 11:
					this.ᜁ(A_0, A_1, A_2);
					num = 3;
					continue;
				case 12:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					default:
					{
						if (false)
						{
						}
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num = 15;
							continue;
						}
						goto IL_197;
					}
					}
					break;
				case 13:
					goto IL_14A;
				case 14:
				{
					string localName;
					if (localName == RecordTableEnumerator.b("㙁╃⑅⑇⽉᱋⽍≏♑", a_))
					{
						num = 11;
						continue;
					}
					goto IL_197;
				}
				case 15:
					num = 14;
					continue;
				case 16:
					if (!A_0.IsEmptyElement)
					{
						num = 7;
						continue;
					}
					goto IL_204;
				case 17:
					goto IL_16A;
				}
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				num = 1;
				continue;
				IL_14A:
				num = 9;
				continue;
				IL_197:
				A_0.Skip();
				num = 13;
			}
		}
		IL_77:
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅁⱃ⍅ⵇ㹉", a_));
		IL_117:
		throw new ArgumentNullException(RecordTableEnumerator.b("ぁ⅃❅ⱇ⽉㹋", a_));
		IL_16A:
		IL_204:
		A_0.Read();
	}

	// Token: 0x06004FB2 RID: 20402 RVA: 0x0030B950 File Offset: 0x0030A950
	private string ᜁ(XmlReader A_0, XlsWorksheet A_1, string A_2)
	{
		int a_ = 14;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_DC:
			if (A_0.MoveToAttribute(RecordTableEnumerator.b("ⵃ≅", a_), RecordTableEnumerator.b("ⱃ㉅㱇㩉癋慍罏⅑㝓㹕㵗㝙㵛ⵝ也ൡᑣͥ٧ቩūɭᙯᵱٳ᭵᥷๹ཻ偽ꦅ킓秊ﮗﮝ캟횡讣钥颧骩骫膭슯ힱ\ud8b3ힵ첷펹펻킽뎿꫁귃뛅믇", a_)))
			{
				string value = A_0.Value;
				A_1.DataHolder.ᜀ(A_1, value, A_2);
				A_0.MoveToElement();
				A_0.Skip();
				return value;
			}
			if (true)
			{
			}
			num = 2;
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
				if (A_0.LocalName != RecordTableEnumerator.b("ぃ❅⩇♉⥋ṍㅏ⁑⁓", a_))
				{
					num = 4;
					continue;
				}
				num = 7;
				continue;
			case 2:
				goto IL_113;
			case 3:
				goto IL_B5;
			case 4:
				goto IL_9D;
			case 5:
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				num = 1;
				continue;
			case 6:
				goto IL_60;
			case 7:
				goto IL_DC;
			}
			if (A_0 == null)
			{
				num = 6;
			}
			else
			{
				num = 5;
			}
		}
		IL_60:
		throw new ArgumentException(RecordTableEnumerator.b("㙃⍅⥇⹉⥋㱍", a_));
		IL_9D:
		throw new XmlException();
		IL_B5:
		throw new ArgumentException(RecordTableEnumerator.b("㝃⹅ⵇ⽉㡋", a_));
		IL_113:
		throw new XmlException();
	}

	// Token: 0x06004FB3 RID: 20403 RVA: 0x0030BAAC File Offset: 0x0030AAAC
	private void ᜈ(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 4;
		for (;;)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					if (A_0 == null)
					{
						num = 4;
						continue;
					}
					num = 5;
					continue;
				case 2:
					goto IL_3F;
				case 3:
					goto IL_92;
				case 4:
					goto IL_E3;
				case 5:
					if (A_0.LocalName != RecordTableEnumerator.b("夹医倽㐿ぁ⭃⩅㭇", a_))
					{
						num = 3;
						continue;
					}
					goto IL_E5;
				}
				if (A_1 == null)
				{
					num = 2;
				}
				else
				{
					num = 0;
				}
			}
			IL_3F:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_55;
			}
		}
		IL_55:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䤹吻嬽┿㙁", a_));
		IL_92:
		throw new XmlException();
		IL_E3:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠹夻弽␿❁㙃", a_));
		IL_E5:
		sprᡟ sprᡟ = A_1.DataHolder;
		MemoryStream a_2 = new MemoryStream();
		XmlWriter xmlWriter = UtilityMethods.ᜀ(a_2, Encoding.UTF8);
		xmlWriter.WriteNode(A_0, false);
		xmlWriter.Flush();
		sprᡟ.ᜀ(a_2);
	}

	// Token: 0x06004FB4 RID: 20404 RVA: 0x0030BBCC File Offset: 0x0030ABCC
	private void ᜀ(XmlReader A_0, XlsWorksheetBase A_1, string A_2)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			int num = 8;
			ushort a_2;
			bool protectContents;
			SheetProtectionType sheetProtectionType;
			for (;;)
			{
				string[] array;
				spr\u2306.ᜀ ᜀ;
				int num2;
				int num3;
				switch (num)
				{
				case 0:
				{
					string value = A_0.Value;
					a_2 = ushort.Parse(value, NumberStyles.AllowHexSpecifier);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_203;
					default:
						if (false)
						{
						}
						num = 13;
						continue;
					}
					break;
				}
				case 1:
					goto IL_15E;
				case 2:
					goto IL_139;
				case 3:
					array = sprᱳ.\u1717;
					ᜀ = new spr\u2306.ᜀ(this.ᜀ);
					num = 12;
					continue;
				case 4:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㡇⭉㽋㵍❏㵑♓㉕", a_)))
					{
						num = 0;
						continue;
					}
					goto IL_C8;
				case 5:
					goto IL_91;
				case 6:
					goto IL_15E;
				case 7:
					if (A_1 == null)
					{
						num = 2;
						continue;
					}
					num = 17;
					continue;
				case 8:
					if (true)
					{
					}
					break;
				case 9:
					goto IL_17F;
				case 10:
					protectContents = XmlConvert.ToBoolean(A_0.Value);
					num = 5;
					continue;
				case 11:
					if (A_0.MoveToAttribute(A_2))
					{
						num = 10;
						continue;
					}
					goto IL_91;
				case 12:
					goto IL_104;
				case 13:
					goto IL_C8;
				case 14:
					if (num2 >= num3)
					{
						num = 9;
						continue;
					}
					goto IL_203;
				case 15:
					if (A_1 is XlsChart)
					{
						num = 3;
						continue;
					}
					goto IL_104;
				case 16:
					goto IL_201;
				case 17:
					if (A_0.LocalName != RecordTableEnumerator.b("㭇≉⥋⭍⑏ɑ♓㥕ⱗ㽙㽛⩝य़ൡ੣", a_))
					{
						num = 16;
						continue;
					}
					sheetProtectionType = SheetProtectionType.None;
					a_2 = 0;
					num = 4;
					continue;
				case 18:
					goto IL_8C;
				}
				if (A_0 == null)
				{
					num = 18;
					continue;
				}
				num = 7;
				continue;
				IL_91:
				array = sprᱳ.\u1719;
				ᜀ = new spr\u2306.ᜀ(this.ᜁ);
				num = 15;
				continue;
				IL_C8:
				protectContents = false;
				num = 11;
				continue;
				IL_104:
				num2 = 0;
				num3 = array.Length;
				num = 1;
				continue;
				IL_15E:
				num = 14;
				continue;
				IL_203:
				sheetProtectionType = ᜀ(A_0, array[num2], sprᱳ.\u171A[num2], sprᱳ.\u171B[num2], sheetProtectionType);
				num2++;
				num = 6;
			}
			IL_8C:
			throw new ArgumentNullException(RecordTableEnumerator.b("㽇㡉╋㩍㕏⁑", a_));
			IL_139:
			throw new ArgumentNullException(RecordTableEnumerator.b("㭇≉⥋⭍⑏", a_));
			IL_17F:
			A_1.ᜀ(a_2, sheetProtectionType);
			A_1.ProtectContents = protectContents;
			A_0.Read();
			return;
			IL_201:
			throw new XmlException();
		}
		}
	}

	// Token: 0x06004FB5 RID: 20405 RVA: 0x0030BE9C File Offset: 0x0030AE9C
	private SheetProtectionType ᜁ(XmlReader A_0, string A_1, SheetProtectionType A_2, bool A_3, SheetProtectionType A_4)
	{
		int a_ = 18;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				A_4 |= A_2;
				goto IL_B4;
			case 1:
			{
				bool flag;
				if (flag)
				{
					num = 0;
					continue;
				}
				A_4 &= ~A_2;
				num = 8;
				continue;
			}
			case 3:
				goto IL_16B;
			case 4:
				if (A_1 != null)
				{
					num = 5;
					continue;
				}
				goto IL_11A;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_B4;
				default:
					if (false)
					{
					}
					num = 7;
					continue;
				}
				break;
			case 6:
				if (A_0.MoveToAttribute(A_1))
				{
					num = 10;
					continue;
				}
				goto IL_5A;
			case 7:
			{
				if (A_1.Length == 0)
				{
					num = 3;
					continue;
				}
				bool flag = A_3;
				num = 6;
				continue;
			}
			case 8:
				goto IL_118;
			case 9:
				goto IL_5A;
			case 10:
			{
				bool flag = XmlConvert.ToBoolean(A_0.Value);
				num = 9;
				continue;
			}
			case 11:
				goto IL_C7;
			case 12:
				goto IL_58;
			}
			if (A_0 == null)
			{
				num = 12;
				continue;
			}
			num = 4;
			continue;
			IL_5A:
			num = 1;
			continue;
			IL_B4:
			if (true)
			{
			}
			num = 11;
		}
		IL_58:
		throw new ArgumentNullException(RecordTableEnumerator.b("㩇⽉ⵋ⩍㕏⁑", a_));
		IL_C7:
		IL_118:
		return A_4;
		IL_11A:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⥇㹉㡋㱍㥏け⅓≕㵗ᑙ㵛㍝՟", a_));
		IL_16B:
		goto IL_11A;
	}

	// Token: 0x06004FB6 RID: 20406 RVA: 0x0030C018 File Offset: 0x0030B018
	private SheetProtectionType ᜀ(XmlReader A_0, string A_1, SheetProtectionType A_2, bool A_3, SheetProtectionType A_4)
	{
		int a_ = 2;
		int num = 8;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				if (A_1.Length == 0)
				{
					num = 3;
					continue;
				}
				bool flag = A_3;
				num = 12;
				continue;
			}
			case 1:
				goto IL_110;
			case 2:
				goto IL_BF;
			case 3:
				goto IL_16B;
			case 4:
				goto IL_5A;
			case 5:
			{
				bool flag = XmlConvert.ToBoolean(A_0.Value);
				num = 4;
				continue;
			}
			case 6:
				A_4 |= A_2;
				goto IL_B4;
			case 7:
				if (A_1 != null)
				{
					num = 9;
					continue;
				}
				goto IL_112;
			case 9:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_B4;
				default:
					if (false)
					{
					}
					num = 0;
					continue;
				}
				break;
			case 10:
				goto IL_58;
			case 11:
			{
				bool flag;
				if (flag)
				{
					num = 6;
					continue;
				}
				A_4 &= ~A_2;
				num = 1;
				continue;
			}
			case 12:
				if (A_0.MoveToAttribute(A_1))
				{
					num = 5;
					continue;
				}
				goto IL_5A;
			}
			if (A_0 == null)
			{
				num = 10;
				continue;
			}
			num = 7;
			continue;
			IL_5A:
			num = 11;
			continue;
			IL_B4:
			num = 2;
		}
		IL_58:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹崻娽┿ぁ", a_));
		IL_BF:
		IL_110:
		return A_4;
		IL_112:
		if (true)
		{
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("夷丹䠻䰽⤿⁁ㅃ㉅ⵇщⵋ⍍㕏", a_));
		IL_16B:
		goto IL_112;
	}

	// Token: 0x06004FB7 RID: 20407 RVA: 0x0030C194 File Offset: 0x0030B194
	private void ᜇ(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 3;
		int num = 15;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_152;
			case 1:
				goto IL_172;
			case 2:
				num = 16;
				continue;
			case 3:
				if (A_0.LocalName != RecordTableEnumerator.b("倸尺匼倾㍀♂⅄Ɇ㭈㥊≌㵎≐", a_))
				{
					num = 4;
					continue;
				}
				num = 13;
				continue;
			case 4:
				goto IL_1FA;
			case 5:
				goto IL_152;
			case 6:
				if (A_1 == null)
				{
					num = 8;
					continue;
				}
				num = 3;
				continue;
			case 7:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 1;
					continue;
				}
				num = 12;
				continue;
			case 8:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A6;
				default:
					goto IL_D8;
				}
				break;
			case 9:
				A_0.Read();
				num = 10;
				continue;
			case 10:
				goto IL_152;
			case 11:
				goto IL_73;
			case 12:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 2;
					continue;
				}
				goto IL_F2;
			case 13:
				if (!A_0.IsEmptyElement)
				{
					num = 9;
					continue;
				}
				goto IL_1FF;
			case 14:
				goto IL_A6;
			case 16:
				if (A_0.LocalName == RecordTableEnumerator.b("倸尺匼倾㍀♂⅄Ɇ㭈㥊≌㵎", a_))
				{
					num = 14;
					continue;
				}
				goto IL_F2;
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 11;
				continue;
			}
			num = 6;
			continue;
			IL_A6:
			this.ᜆ(A_0, A_1);
			num = 5;
			continue;
			IL_F2:
			A_0.Read();
			num = 0;
			continue;
			IL_152:
			num = 7;
		}
		IL_73:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬸帺尼嬾⑀ㅂ", a_));
		IL_D8:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䨸区堼娾㕀", a_));
		IL_172:
		goto IL_1FF;
		IL_1FA:
		throw new XmlException();
		IL_1FF:
		A_0.Read();
	}

	// Token: 0x06004FB8 RID: 20408 RVA: 0x0030C3A8 File Offset: 0x0030B3A8
	private void ᜆ(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			int num = 3;
			string text;
			IgnoreErrorType ignoreErrorType;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					goto IL_222;
				case 1:
					goto IL_E4;
				case 2:
					goto IL_88;
				case 4:
					goto IL_13F;
				case 5:
					if (true)
					{
					}
					if (text == null)
					{
						num = 0;
						continue;
					}
					goto IL_289;
				case 6:
				{
					if (A_0.LocalName != RecordTableEnumerator.b("⭁⍃⡅❇㡉⥋⩍ᕏ⁑♓㥕⩗", a_))
					{
						num = 17;
						continue;
					}
					ignoreErrorType = IgnoreErrorType.None;
					text = null;
					num2 = 0;
					int attributeCount = A_0.AttributeCount;
					num = 11;
					continue;
				}
				case 7:
				{
					int num3;
					ignoreErrorType |= spr\u1B7A.ᡂ[num3];
					num = 9;
					continue;
				}
				case 8:
					if (A_1 == null)
					{
						num = 4;
						continue;
					}
					num = 6;
					continue;
				case 9:
					goto IL_227;
				case 10:
					text = A_0.Value;
					num = 13;
					continue;
				case 11:
					goto IL_E4;
				case 12:
				{
					int num3 = Array.IndexOf<string>(spr\u1B7A.ᡅ, A_0.LocalName);
					num = 19;
					continue;
				}
				case 13:
					goto IL_227;
				case 14:
					num = 5;
					continue;
				case 15:
				{
					int attributeCount;
					if (num2 >= attributeCount)
					{
						num = 14;
						continue;
					}
					A_0.MoveToAttribute(num2);
					num = 16;
					continue;
				}
				case 16:
					if (A_0.LocalName == RecordTableEnumerator.b("ㅁ㕃㑅ⵇⱉ", a_))
					{
						num = 10;
						continue;
					}
					num = 18;
					continue;
				case 17:
					goto IL_1F3;
				case 18:
					if (XmlConvert.ToBoolean(A_0.Value))
					{
						num = 12;
						continue;
					}
					goto IL_227;
				case 19:
				{
					int num3;
					if (num3 >= 0)
					{
						num = 7;
						continue;
					}
					goto IL_227;
				}
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				num = 8;
				continue;
				IL_E4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_23C;
				default:
					if (false)
					{
					}
					num = 15;
					continue;
				}
				IL_227:
				num2++;
				num = 1;
			}
			IL_88:
			throw new ArgumentNullException(RecordTableEnumerator.b("ぁ⅃❅ⱇ⽉㹋", a_));
			IL_13F:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅁⱃ⍅ⵇ㹉", a_));
			IL_1F3:
			goto IL_23C;
			IL_222:
			throw new XmlException();
			IL_23C:
			throw new XmlException();
			IL_289:
			this.ᜀ(text, ignoreErrorType, A_1);
			A_0.MoveToElement();
			A_0.Read();
			return;
		}
		}
	}

	// Token: 0x06004FB9 RID: 20409 RVA: 0x0030C658 File Offset: 0x0030B658
	private void ᜀ(string A_0, IgnoreErrorType A_1, XlsWorksheet A_2)
	{
		switch (0)
		{
		default:
		{
			spr\u1F7E spr_u1F7E;
			for (;;)
			{
				spr_u1F7E = new spr\u1F7E(A_1);
				string[] array = A_0.Split(new char[]
				{
					' '
				});
				IWorkbook workbook = A_2.Workbook;
				int num = 0;
				int num2 = array.Length;
				int num3 = 1;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_5F;
					case 1:
						goto IL_5F;
					case 2:
					{
						if (num >= num2)
						{
							goto IL_92;
						}
						int num4;
						int num5;
						int num6;
						int num7;
						sprṔ.ᜀ(array[num], workbook, out num4, out num5, out num6, out num7);
						Rectangle a_ = Rectangle.FromLTRB(num5 - 1, num4 - 1, num7 - 1, num6 - 1);
						spr_u1F7E.ᜄ(a_);
						num++;
						num3 = 0;
						continue;
					}
					case 3:
						goto IL_A8;
					}
					break;
					IL_5F:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_92:
						num3 = 3;
						break;
					default:
						if (false)
						{
						}
						num3 = 2;
						break;
					}
				}
			}
			IL_A8:
			A_2.ErrorIndicators.ᜀ(spr_u1F7E);
			return;
		}
		}
	}

	// Token: 0x06004FBA RID: 20410 RVA: 0x0030C764 File Offset: 0x0030B764
	private void ᜅ(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 4;
		int num = 16;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				goto IL_73;
			case 1:
				goto IL_147;
			case 2:
				this.ᜄ(A_0, A_1);
				num = 13;
				continue;
			case 3:
				goto IL_147;
			case 4:
				if (A_0.LocalName == RecordTableEnumerator.b("夹䤻䴽㐿ⵁ⥃ᙅ㩇", a_))
				{
					num = 2;
					continue;
				}
				goto IL_D6;
			case 5:
				num = 4;
				continue;
			case 6:
				goto IL_167;
			case 7:
				if (A_1 == null)
				{
					num = 8;
					continue;
				}
				num = 9;
				continue;
			case 8:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1D3;
				default:
					goto IL_11B;
				}
				break;
			case 9:
				goto IL_1D3;
			case 10:
				if (!A_0.IsEmptyElement)
				{
					num = 14;
					continue;
				}
				goto IL_1FE;
			case 11:
				goto IL_1F9;
			case 12:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 5;
					continue;
				}
				goto IL_D6;
			case 13:
				goto IL_147;
			case 14:
				A_0.Read();
				num = 1;
				continue;
			case 15:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 6;
					continue;
				}
				num = 12;
				continue;
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			num = 7;
			continue;
			IL_D6:
			A_0.Skip();
			num = 3;
			continue;
			IL_147:
			num = 15;
			continue;
			IL_1D3:
			if (A_0.LocalName != RecordTableEnumerator.b("夹䤻䴽㐿ⵁ⥃ᙅ㩇╉㱋⭍≏♑㵓㍕⭗", a_))
			{
				num = 11;
			}
			else
			{
				num = 10;
			}
		}
		IL_73:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠹夻弽␿❁㙃", a_));
		IL_11B:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䤹吻嬽┿㙁", a_));
		IL_167:
		goto IL_1FE;
		IL_1F9:
		throw new XmlException();
		IL_1FE:
		A_0.Read();
	}

	// Token: 0x06004FBB RID: 20411 RVA: 0x0030C978 File Offset: 0x0030B978
	private void ᜄ(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 19;
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!A_0.MoveToAttribute(RecordTableEnumerator.b("⁈⽊", a_), RecordTableEnumerator.b("ⅈ㽊㥌㽎歐籒穔⑖㩘㍚㡜㉞`ၢ䭤ࡦᥨ๪ͬᝮᱰὲ፴ᡶ୸ᙺᱼ୾궂ꒊ朗\udd98ﺜ철욢쮤펦蚨馪鶬龮螰鲲잴튶햸\udaba즼횾껀귂뛄꿆ꃈ믊뻌", a_)))
					{
						num = 6;
						continue;
					}
					goto IL_178;
				case 1:
					if (A_1 == null)
					{
						num = 8;
						continue;
					}
					num = 2;
					continue;
				case 2:
					if (true)
					{
					}
					if (A_0.LocalName != RecordTableEnumerator.b("⩈㹊㹌㭎㹐㹒Ք╖", a_))
					{
						num = 9;
						continue;
					}
					num = 5;
					continue;
				case 3:
					goto IL_67;
				case 5:
				{
					if (!A_0.MoveToAttribute(RecordTableEnumerator.b("❈⩊⁌⩎", a_)))
					{
						num = 7;
						continue;
					}
					string value = A_0.Value;
					num = 0;
					continue;
				}
				case 6:
					goto IL_D5;
				case 7:
					goto IL_119;
				case 8:
					goto IL_8D;
				case 9:
					goto IL_15F;
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
			IL_67:
			throw new ArgumentNullException(RecordTableEnumerator.b("㭈⹊ⱌ⭎㑐⅒", a_));
			IL_6F:
			throw new XmlException();
			IL_8D:
			throw new ArgumentNullException(RecordTableEnumerator.b("㩈⍊⡌⩎═", a_));
			IL_D5:
			throw new XmlException();
			IL_119:
			throw new XmlException();
			IL_15F:
			goto IL_6F;
			IL_178:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_6F;
			default:
			{
				if (false)
				{
				}
				string value2 = A_0.Value;
				IWorksheetCustomProperties customProperties = A_1.CustomProperties;
				string value;
				ICustomProperty customProperty = customProperties.Add(value);
				customProperty.Value = this.ᜀ(value2, A_1.DataHolder);
				A_0.MoveToElement();
				A_0.Skip();
				return;
			}
			}
			break;
		}
		}
	}

	// Token: 0x06004FBC RID: 20412 RVA: 0x0030CB50 File Offset: 0x0030BB50
	private static int ᜌ(string A_0)
	{
		int a_ = 14;
		while (A_0.StartsWith(RecordTableEnumerator.b("楃", a_)))
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				return XmlConvert.ToInt32(A_0);
			}
		}
		return (int)XmlConvert.ToUInt32(A_0);
	}

	// Token: 0x06004FBD RID: 20413 RVA: 0x0030CBBC File Offset: 0x0030BBBC
	private string ᜀ(string A_0, sprᡟ A_1)
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
		RelationsCollection relationsCollection = A_1.ᜇ();
		sprᦨ a_ = relationsCollection[A_0];
		relationsCollection.Remove(A_0);
		string text = A_1.ᜉ().ᜇ();
		text = Path.GetDirectoryName(text);
		text = text.Replace('\\', '/');
		byte[] array = A_1.ᜋ().ᜀ(a_, text, true);
		return Encoding.Unicode.GetString(array, 0, array.Length);
	}

	// Token: 0x06004FBE RID: 20414 RVA: 0x0030CC4C File Offset: 0x0030BC4C
	internal static void ᜀ(XmlReader A_0, XlsWorksheetBase A_1, RelationsCollection A_2)
	{
		int a_ = 7;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_A0;
			case 2:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				num = 6;
				continue;
			case 3:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("吼嬾", a_), RecordTableEnumerator.b("唼䬾㕀㍂罄框晈㡊⹌❎㑐㹒㑔⑖睘㑚ⵜ㩞འ᭢ࡤ୦ཨѪὬɮၰݲٴ奶ᙸॺ᩼偾즌늜궞醠鎢鎤袦\udba8캪솬캮얰\udab2\udab4\ud9b6쪸펺풼쾾닀", a_)))
				{
					num = 7;
					continue;
				}
				goto IL_146;
			case 4:
				goto IL_88;
			case 5:
				goto IL_44;
			case 6:
				if (A_0.LocalName != RecordTableEnumerator.b("儼娾♀≂♄㹆ൈ㥊ⱌ㡎㡐㵒㉔ὖ὘", a_))
				{
					num = 4;
					continue;
				}
				num = 3;
				continue;
			case 7:
				goto IL_12D;
			}
			if (A_0 == null)
			{
				num = 5;
			}
			else
			{
				num = 2;
			}
		}
		IL_44:
		throw new ArgumentNullException(RecordTableEnumerator.b("似娾⁀❂⁄㕆", a_));
		IL_88:
		throw new XmlException(RecordTableEnumerator.b("格儾⑀㭂㕄≆⩈㽊⡌⭎煐⭒㡔㭖祘⽚㱜㡞你", a_));
		IL_A0:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("丼圾⑀♂ㅄ", a_));
		IL_12D:
		string value = A_0.Value;
		sprᡟ sprᡟ = A_1.DataHolder;
		sprᡟ.ᜁ(value);
		sprᡟ.ᜀ(A_1.HeaderFooterShapes, value, A_2);
		A_0.MoveToElement();
		A_0.Skip();
		return;
		IL_146:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_44;
		default:
			if (false)
			{
			}
			throw new XmlException(RecordTableEnumerator.b("樼䴾⹀ⵂ≄杆ㅈ♊⅌潎㝐㱒❔㩖㡘⽚", a_));
		}
	}

	// Token: 0x06004FBF RID: 20415 RVA: 0x0030CDD8 File Offset: 0x0030BDD8
	private void ᜀ(XmlReader A_0, XlsWorksheet A_1, Dictionary<string, object> A_2)
	{
		int a_ = 4;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_11F;
			case 1:
				if (A_1 == null)
				{
					num = 6;
					continue;
				}
				num = 7;
				continue;
			case 2:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("匹堻", a_), RecordTableEnumerator.b("刹䠻䨽〿硁歃楅㭇⥉⑋⭍㵏㍑❓硕㝗⩙㥛そᡟཡࡣeݧᡩū཭ѯű婳᥵੷ᵹ卻ᅽ캉ﾑ떙꺛꺝邟钡讣풥춧용춫\udaad\ud9af\uddb1\udab3억킷펹첻춽", a_)))
				{
					num = 0;
					continue;
				}
				goto IL_140;
			case 4:
				goto IL_88;
			case 5:
				goto IL_44;
			case 6:
				goto IL_A0;
			case 7:
				if (A_0.LocalName != RecordTableEnumerator.b("帹主弽㜿⭁⩃ⅅ", a_))
				{
					num = 4;
					continue;
				}
				num = 2;
				continue;
			}
			if (A_0 == null)
			{
				num = 5;
			}
			else
			{
				num = 1;
			}
		}
		IL_44:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠹夻弽␿❁㙃", a_));
		IL_88:
		throw new XmlException(RecordTableEnumerator.b("漹刻嬽㠿㉁⅃╅㱇⽉⡋湍⡏㽑㡓癕ⱗ㭙㭛灝", a_));
		IL_A0:
		throw new ArgumentNullException(RecordTableEnumerator.b("䤹吻嬽┿㙁", a_));
		IL_11F:
		if (true)
		{
		}
		string value = A_0.Value;
		sprᡟ sprᡟ = A_1.DataHolder;
		sprᡟ.ᜀ(A_1, value, A_2);
		sprᡟ.ᜃ(value);
		A_0.MoveToElement();
		A_0.Skip();
		return;
		IL_140:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_44;
		default:
			if (false)
			{
			}
			throw new XmlException(RecordTableEnumerator.b("洹主儽⸿╁摃㹅╇♉汋⡍㽏⁑㥓㝕ⱗ", a_));
		}
	}

	// Token: 0x06004FC0 RID: 20416 RVA: 0x0030CF60 File Offset: 0x0030BF60
	private void ᜁ(XmlReader A_0, XlsWorksheetBase A_1)
	{
		int a_ = 0;
		if (true)
		{
		}
		int num = 7;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("張尷", a_), RecordTableEnumerator.b("帵䰷丹䰻н漿流㝃╅⁇⽉⅋⽍⍏籑㭓♕㵗㑙⑛㍝౟ѡୣᑥէ୩ᡫᵭ幯ᵱٳᅵ坷ᕹ᩻᡽슅曆ﲑ릕ꪗꪙ겛ꢝ辟킡솣쪥즧\udea9얫솭\udeaf솱\udcb3\udfb5좷즹", a_)))
				{
					num = 1;
					continue;
				}
				goto IL_146;
			case 1:
				goto IL_12D;
			case 2:
				goto IL_90;
			case 3:
				goto IL_4C;
			case 4:
				if (A_1 == null)
				{
					num = 6;
					continue;
				}
				num = 5;
				continue;
			case 5:
				if (A_0.LocalName != RecordTableEnumerator.b("娵崷崹崻崽㤿ف㙃❅㽇⍉≋⥍", a_))
				{
					num = 2;
					continue;
				}
				num = 0;
				continue;
			case 6:
				goto IL_A8;
			}
			if (A_0 == null)
			{
				num = 3;
			}
			else
			{
				num = 4;
			}
		}
		IL_4C:
		throw new ArgumentNullException(RecordTableEnumerator.b("䐵崷嬹堻嬽㈿", a_));
		IL_90:
		throw new XmlException(RecordTableEnumerator.b("挵嘷弹䐻丽┿⅁ぃ⍅ⱇ橉㑋⍍㱏牑⁓㝕㽗瑙", a_));
		IL_A8:
		throw new ArgumentNullException(RecordTableEnumerator.b("䔵倷弹夻䨽", a_));
		IL_12D:
		string value = A_0.Value;
		sprᡟ sprᡟ = A_1.DataHolder;
		sprᡟ.ᜀ(A_1.InnerShapes, value, null);
		sprᡟ.ᜄ(value);
		A_0.MoveToElement();
		A_0.Skip();
		return;
		IL_146:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_4C;
		default:
			if (false)
			{
			}
			throw new XmlException(RecordTableEnumerator.b("愵䨷唹刻夽怿㩁⥃⩅桇ⱉ⍋㱍㵏㍑⁓", a_));
		}
	}

	// Token: 0x06004FC1 RID: 20417 RVA: 0x0030D0EC File Offset: 0x0030C0EC
	private void ᜁ(XmlReader A_0, XlsWorksheetBase A_1, string A_2, List<string> A_3, Dictionary<string, object> A_4)
	{
		int a_ = 13;
		switch (0)
		{
		default:
		{
			int num = 40;
			XlsShape xlsShape;
			MemoryStream memoryStream;
			string a_6;
			for (;;)
			{
				Rectangle a_2;
				Rectangle a_3;
				Size a_4;
				Stream item;
				switch (num)
				{
				case 0:
					goto IL_20B;
				case 1:
					goto IL_555;
				case 2:
					xlsShape = new XlsShape(A_1.AppImplementation, A_1.InnerShapes);
					A_1.InnerShapes.AddShape(xlsShape);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5CC;
					default:
						if (false)
						{
						}
						num = 15;
						continue;
					}
					break;
				case 3:
					num = 34;
					continue;
				case 4:
					num = 10;
					continue;
				case 5:
					goto IL_20B;
				case 6:
					if (this.ᜑ)
					{
						num = 48;
						continue;
					}
					goto IL_20B;
				case 7:
					A_0.Read();
					num = 22;
					continue;
				case 8:
					goto IL_20B;
				case 9:
					if (xlsShape == null)
					{
						num = 28;
						continue;
					}
					goto IL_6EB;
				case 10:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 44;
						continue;
					}
					goto IL_71E;
				}
				case 11:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("♂⅄⹆㵈੊㹌", a_)))
					{
						num = 39;
						continue;
					}
					goto IL_168;
				case 12:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 7;
						continue;
					}
					num = 36;
					continue;
				case 13:
					goto IL_20B;
				case 14:
				{
					int num2;
					switch (num2)
					{
					case 0:
						a_2 = this.\u1718(A_0);
						num = 0;
						continue;
					case 1:
						a_3 = this.\u1718(A_0);
						num = 8;
						continue;
					case 2:
						a_4 = this.\u1719(A_0);
						num = 46;
						continue;
					case 3:
						xlsShape = this.ᜀ(A_0, A_1, A_2, A_3, A_4);
						num = 5;
						continue;
					case 4:
						A_0.Skip();
						num = 35;
						continue;
					case 5:
						if (true)
						{
						}
						xlsShape = this.ᜀ(A_0, A_1, ref memoryStream);
						num = 6;
						continue;
					case 6:
						item = this.\u171A(A_0);
						num = 9;
						continue;
					case 7:
						goto IL_71E;
					case 8:
						memoryStream = this.\u171A(A_0);
						xlsShape = this.ᜁ(memoryStream, A_1, A_2);
						num = 26;
						continue;
					default:
						num = 3;
						continue;
					}
					break;
				}
				case 15:
					goto IL_20B;
				case 16:
					goto IL_20B;
				case 17:
					goto IL_168;
				case 18:
				{
					bool a_5;
					this.ᜀ(xlsShape, a_2, a_3, a_4, a_5);
					num = 38;
					continue;
				}
				case 19:
				{
					string localName;
					int num2;
					if (spr\u22D2.ក.TryGetValue(localName, out num2))
					{
						num = 24;
						continue;
					}
					goto IL_71E;
				}
				case 20:
					goto IL_20B;
				case 21:
					goto IL_5EB;
				case 22:
					if (xlsShape != null)
					{
						num = 18;
						continue;
					}
					goto IL_757;
				case 23:
					goto IL_20B;
				case 24:
					num = 14;
					continue;
				case 25:
				{
					if (A_3 == null)
					{
						num = 21;
						continue;
					}
					bool a_5 = A_0.LocalName == RecordTableEnumerator.b("ㅂ⁄⭆ᩈ≊㝌⩎ၐ㵒㙔㽖㙘⥚", a_);
					a_6 = null;
					num = 11;
					continue;
				}
				case 26:
					if (xlsShape == null)
					{
						num = 2;
						continue;
					}
					memoryStream = null;
					num = 49;
					continue;
				case 27:
					xlsShape = new XlsShape(A_1.AppImplementation, A_1.InnerShapes);
					A_1.InnerShapes.AddShape(xlsShape);
					num = 47;
					continue;
				case 28:
					xlsShape = new XlsShape(A_1.AppImplementation, A_1.InnerShapes);
					A_1.InnerShapes.AddShape(xlsShape);
					num = 29;
					continue;
				case 29:
					goto IL_6EB;
				case 30:
					if (xlsShape == null)
					{
						num = 27;
						continue;
					}
					memoryStream = null;
					num = 42;
					continue;
				case 31:
					goto IL_1EC;
				case 32:
					if (xlsShape.ᜪ == null)
					{
						num = 45;
						continue;
					}
					goto IL_1EC;
				case 33:
					spr\u22D2.ក = new Dictionary<string, int>(9)
					{
						{
							RecordTableEnumerator.b("╂㝄⡆⑈", a_),
							0
						},
						{
							RecordTableEnumerator.b("㝂⩄", a_),
							1
						},
						{
							RecordTableEnumerator.b("♂㵄㍆", a_),
							2
						},
						{
							RecordTableEnumerator.b("㍂ⱄ⑆", a_),
							3
						},
						{
							RecordTableEnumerator.b("⁂⥄⹆ⱈ╊㥌୎ぐ❒㑔", a_),
							4
						},
						{
							RecordTableEnumerator.b("あ㕄", a_),
							5
						},
						{
							RecordTableEnumerator.b("⁂㵄⥆ᩈ㭊", a_),
							6
						},
						{
							RecordTableEnumerator.b("⑂㝄㝆ᩈ㭊", a_),
							7
						},
						{
							RecordTableEnumerator.b("⑂㝄♆㥈⍊⑌ⱎᝐ⅒㑔㩖㱘", a_),
							8
						}
					};
					num = 1;
					continue;
				case 34:
					goto IL_71E;
				case 35:
					goto IL_20B;
				case 36:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 4;
						continue;
					}
					A_0.Skip();
					num = 13;
					continue;
				case 37:
					if (spr\u22D2.ក == null)
					{
						num = 33;
						continue;
					}
					goto IL_555;
				case 38:
					goto IL_29F;
				case 39:
					a_6 = A_0.Value;
					num = 17;
					continue;
				case 41:
					if (A_1 == null)
					{
						num = 43;
						continue;
					}
					goto IL_5CC;
				case 42:
					goto IL_20B;
				case 43:
					goto IL_638;
				case 44:
					num = 37;
					continue;
				case 45:
					xlsShape.ᜪ = new List<Stream>();
					num = 31;
					continue;
				case 46:
					goto IL_20B;
				case 47:
					goto IL_20B;
				case 48:
					xlsShape.EnableAlternateContent = this.ᜑ;
					num = 23;
					continue;
				case 49:
					goto IL_20B;
				case 50:
					goto IL_10A;
				}
				if (A_0 == null)
				{
					num = 50;
					continue;
				}
				num = 41;
				continue;
				IL_168:
				A_0.Read();
				a_2 = default(Rectangle);
				a_3 = default(Rectangle);
				xlsShape = null;
				memoryStream = null;
				a_4 = new Size(-1, -1);
				num = 16;
				continue;
				IL_1EC:
				xlsShape.ᜪ.Add(item);
				num = 20;
				continue;
				IL_20B:
				num = 12;
				continue;
				IL_555:
				num = 19;
				continue;
				IL_5CC:
				num = 25;
				continue;
				IL_6EB:
				num = 32;
				continue;
				IL_71E:
				memoryStream = this.\u171A(A_0);
				xlsShape = this.ᜀ(memoryStream, A_1, A_2);
				num = 30;
			}
			IL_10A:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄♆ⵈ⹊㽌", a_));
			IL_29F:
			goto IL_757;
			IL_5EB:
			throw new ArgumentNullException(RecordTableEnumerator.b("⽂㙄㍆ᭈ⹊⅌⹎═㩒㩔㥖ၘ㽚⹜", a_));
			IL_638:
			throw new ArgumentNullException(RecordTableEnumerator.b("あⵄ≆ⱈ㽊", a_));
			IL_757:
			this.ᜀ(xlsShape, a_6);
			xlsShape.XmlDataStream = memoryStream;
			this.ᜑ = false;
			return;
		}
		}
	}

	// Token: 0x06004FC2 RID: 20418 RVA: 0x0030D86C File Offset: 0x0030C86C
	private XlsShape ᜀ(XmlReader A_0, XlsWorksheetBase A_1, ref MemoryStream A_2)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			XlsShape xlsShape;
			for (;;)
			{
				A_2 = this.\u171A(A_0);
				A_2.Position = 0L;
				A_0 = UtilityMethods.ᜀ(A_2);
				ExcelShapeType excelShapeType = ExcelShapeType.Unknown;
				xlsShape = null;
				int? num = null;
				int num2 = 25;
				for (;;)
				{
					ExcelShapeType excelShapeType2;
					switch (num2)
					{
					case 0:
						goto IL_270;
					case 1:
						goto IL_289;
					case 2:
						if (this.ᜑ)
						{
							num2 = 19;
							continue;
						}
						goto IL_3D8;
					case 3:
						if (A_0.LocalName == RecordTableEnumerator.b("╅ه㱉᱋㱍", a_))
						{
							num2 = 8;
							continue;
						}
						goto IL_3D8;
					case 4:
						num2 = 12;
						continue;
					case 5:
						xlsShape.ShapeId = num.Value;
						num2 = 22;
						continue;
					case 6:
						num2 = 11;
						continue;
					case 7:
						if (A_0.MoveToAttribute(RecordTableEnumerator.b("㉅ぇࡉ⍋㙍", a_)))
						{
							num2 = 4;
							continue;
						}
						goto IL_1BA;
					case 8:
						num2 = 21;
						continue;
					case 9:
						num2 = 7;
						continue;
					case 10:
						if (num != null)
						{
							goto IL_3C7;
						}
						goto IL_41C;
					case 11:
					{
						int value;
						if (int.TryParse(A_0.Value, out value))
						{
							num2 = 31;
							continue;
						}
						goto IL_3D8;
					}
					case 12:
						if (XmlConvert.ToBoolean(A_0.Value))
						{
							num2 = 24;
							continue;
						}
						goto IL_1BA;
					case 13:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num2 = 16;
							continue;
						}
						goto IL_3D8;
					case 14:
						goto IL_3D8;
					case 15:
						if (num != null)
						{
							num2 = 26;
							continue;
						}
						goto IL_270;
					case 16:
						num2 = 17;
						continue;
					case 17:
						if (A_0.LocalName == RecordTableEnumerator.b("╅ه㱉Ὃ㹍O⁑", a_))
						{
							num2 = 9;
							continue;
						}
						num2 = 2;
						continue;
					case 18:
						goto IL_1BA;
					case 19:
						num2 = 3;
						continue;
					case 20:
						if (excelShapeType2 == ExcelShapeType.TextBox)
						{
							num2 = 29;
							continue;
						}
						goto IL_41C;
					case 21:
						if (A_0.MoveToAttribute(RecordTableEnumerator.b("⽅ⱇ", a_)))
						{
							num2 = 6;
							continue;
						}
						goto IL_3D8;
					case 22:
						goto IL_26B;
					case 23:
						if (A_0.NodeType != XmlNodeType.None)
						{
							A_0.Read();
							num2 = 13;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3C7;
						default:
							if (false)
							{
							}
							num2 = 28;
							continue;
						}
						break;
					case 24:
						excelShapeType = ExcelShapeType.TextBox;
						num2 = 18;
						continue;
					case 25:
						goto IL_3D8;
					case 26:
						xlsShape.ShapeId = num.Value;
						num2 = 0;
						continue;
					case 27:
						if (excelShapeType2 != ExcelShapeType.Unknown)
						{
							num2 = 30;
							continue;
						}
						xlsShape = new XlsShape(A_1.AppImplementation, A_1.InnerShapes);
						num2 = 15;
						continue;
					case 28:
						goto IL_1BA;
					case 29:
					{
						ITextBoxShape textBoxShape = A_1.Shapes.AddTextBox();
						xlsShape = (XlsShape)textBoxShape;
						spr\u1F16.ᜁ(textBoxShape, A_0, this);
						num2 = 10;
						continue;
					}
					case 30:
						num2 = 20;
						continue;
					case 31:
					{
						int value;
						num = new int?(value);
						num2 = 14;
						continue;
					}
					}
					break;
					IL_1BA:
					A_2.Position = 0L;
					A_0 = UtilityMethods.ᜀ(A_2);
					excelShapeType2 = excelShapeType;
					num2 = 27;
					continue;
					IL_270:
					A_1.InnerShapes.AddShape(xlsShape);
					num2 = 1;
					continue;
					IL_3C7:
					num2 = 5;
					continue;
					IL_3D8:
					num2 = 23;
				}
			}
			IL_26B:
			IL_289:
			IL_41C:
			if (true)
			{
			}
			return xlsShape;
		}
		}
	}

	// Token: 0x06004FC3 RID: 20419 RVA: 0x0030DCA0 File Offset: 0x0030CCA0
	private XlsShape ᜁ(MemoryStream A_0, XlsWorksheetBase A_1, string A_2)
	{
		int a_ = 13;
		switch (0)
		{
		default:
		{
			int num = 5;
			for (;;)
			{
				XmlReader xmlReader;
				XlsChartShape xlsChartShape;
				switch (num)
				{
				case 0:
					if (xmlReader.MoveToAttribute(RecordTableEnumerator.b("ⵂ⑄⩆ⱈ", a_)))
					{
						num = 7;
						continue;
					}
					goto IL_10F;
				case 1:
					goto IL_10F;
				case 2:
					return xlsChartShape;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_205;
					default:
						goto IL_C7;
					}
					break;
				case 4:
				{
					string text;
					xlsChartShape.Name = text;
					num = 8;
					continue;
				}
				case 6:
					num = 15;
					continue;
				case 7:
				{
					string text = xmlReader.Value;
					num = 16;
					continue;
				}
				case 8:
					goto IL_205;
				case 9:
					if (xmlReader.LocalName == RecordTableEnumerator.b("⁂ⵄ♆㭈㽊", a_))
					{
						num = 12;
						continue;
					}
					goto IL_205;
				case 10:
				{
					string s = xmlReader.Value;
					num = 1;
					continue;
				}
				case 11:
				{
					string text;
					if (text != null)
					{
						num = 4;
						continue;
					}
					goto IL_205;
				}
				case 12:
				{
					xlsChartShape = (XlsChartShape)(A_1.Charts as XlsWorksheetChartsCollection).Add();
					XlsChart xlsChart = xlsChartShape.ChartObject;
					sprᡟ sprᡟ = A_1.DataHolder;
					sprវ a_2 = sprᡟ.ᜋ();
					RelationsCollection a_3 = sprᡟ.ᜈ();
					xlsChart.DataHolder = sprᡟ;
					this.ᜀ(xmlReader, xlsChart, a_3, a_2, A_2);
					xlsChart.DataHolder = null;
					num = 11;
					continue;
				}
				case 13:
					goto IL_192;
				case 14:
					goto IL_D2;
				case 15:
					if (xmlReader.NodeType == XmlNodeType.None)
					{
						num = 14;
						continue;
					}
					num = 18;
					continue;
				case 16:
					if (xmlReader.MoveToAttribute(RecordTableEnumerator.b("⩂⅄", a_)))
					{
						num = 10;
						continue;
					}
					goto IL_10F;
				case 17:
					num = 0;
					continue;
				case 18:
					if (xmlReader.LocalName == RecordTableEnumerator.b("⁂ୄㅆ᥈㥊", a_))
					{
						num = 17;
						continue;
					}
					goto IL_10F;
				case 19:
				{
					string s;
					xlsChartShape.ShapeId = int.Parse(s);
					num = 2;
					continue;
				}
				case 20:
					if (xlsChartShape != null)
					{
						num = 19;
						continue;
					}
					return xlsChartShape;
				case 21:
				{
					if (A_1 == null)
					{
						num = 13;
						continue;
					}
					A_0.Position = 0L;
					xmlReader = UtilityMethods.ᜀ(A_0);
					xmlReader.Read();
					string text = null;
					string s = RecordTableEnumerator.b("獂", a_);
					num = 22;
					continue;
				}
				case 22:
					goto IL_35B;
				case 23:
					goto IL_35B;
				case 24:
					if (xmlReader.LocalName != RecordTableEnumerator.b("⁂ⵄ♆㭈㽊", a_))
					{
						num = 6;
						continue;
					}
					goto IL_D2;
				}
				if (A_0 == null)
				{
					if (true)
					{
					}
					num = 3;
					continue;
				}
				num = 21;
				continue;
				IL_D2:
				xlsChartShape = null;
				num = 9;
				continue;
				IL_10F:
				xmlReader.Read();
				num = 23;
				continue;
				IL_205:
				num = 20;
				continue;
				IL_35B:
				num = 24;
			}
			IL_C7:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("❂⑄㍆⡈", a_));
			IL_192:
			throw new ArgumentNullException(RecordTableEnumerator.b("あⵄ≆ⱈ㽊", a_));
		}
		}
	}

	// Token: 0x06004FC4 RID: 20420 RVA: 0x0030E044 File Offset: 0x0030D044
	private XlsShape ᜀ(MemoryStream A_0, XlsWorksheetBase A_1, string A_2)
	{
		int a_ = 17;
		switch (0)
		{
		default:
		{
			int num = 33;
			for (;;)
			{
				XmlReader xmlReader;
				XlsShape xlsShape;
				XlsChartShape xlsChartShape;
				string text;
				bool flag;
				Stream item;
				Stream item2;
				Stream item3;
				Stream item4;
				switch (num)
				{
				case 0:
					goto IL_558;
				case 1:
					if (xmlReader.MoveToAttribute(RecordTableEnumerator.b("⑆ㅈ", a_)))
					{
						num = 18;
						continue;
					}
					goto IL_39C;
				case 2:
					goto IL_32F;
				case 3:
					goto IL_32F;
				case 4:
					xlsShape.ᜩ = new List<Stream>();
					num = 62;
					continue;
				case 5:
					xlsChartShape.Name = text;
					num = 0;
					continue;
				case 6:
					if (xmlReader.MoveToAttribute(RecordTableEnumerator.b("㹆", a_)))
					{
						num = 58;
						continue;
					}
					goto IL_32F;
				case 7:
					goto IL_16E;
				case 8:
					num = 10;
					continue;
				case 9:
					if (xmlReader.LocalName == RecordTableEnumerator.b("⑆݈㵊ᵌ㵎", a_))
					{
						num = 17;
						continue;
					}
					goto IL_32F;
				case 10:
					if (spr\u22D2.ខ == null)
					{
						num = 19;
						continue;
					}
					goto IL_476;
				case 11:
				{
					if (A_1 == null)
					{
						num = 53;
						continue;
					}
					A_0.Position = 0L;
					xmlReader = UtilityMethods.ᜀ(A_0);
					xmlReader.Read();
					text = null;
					string s = RecordTableEnumerator.b("睆", a_);
					xlsChartShape = null;
					xlsShape = A_1.InnerShapes.AddShape(new XlsShape(A_1.AppImplementation, A_1.InnerShapes));
					flag = false;
					num = 57;
					continue;
				}
				case 12:
					goto IL_7FA;
				case 13:
					return xlsShape;
				case 14:
					if (xlsShape.ᜫ == null)
					{
						num = 42;
						continue;
					}
					goto IL_71A;
				case 15:
				{
					int num2;
					switch (num2)
					{
					case 0:
						item = this.\u171A(xmlReader);
						num = 16;
						continue;
					case 1:
						item2 = this.\u171A(xmlReader);
						num = 21;
						continue;
					case 2:
						num = 9;
						continue;
					case 3:
						item3 = this.\u171A(xmlReader);
						num = 14;
						continue;
					case 4:
						item4 = this.\u171A(xmlReader);
						num = 32;
						continue;
					case 5:
						num = 73;
						continue;
					case 6:
						xlsChartShape = new XlsChartShape(A_1.AppImplementation, xlsShape);
						num = 43;
						continue;
					case 7:
						num = 23;
						continue;
					default:
						num = 51;
						continue;
					}
					break;
				}
				case 16:
					if (((XlsWorksheet)A_1).\u173E == null)
					{
						num = 26;
						continue;
					}
					goto IL_376;
				case 17:
					num = 48;
					continue;
				case 18:
					xlsChartShape.ExtentsX = XmlConvert.ToInt32(xmlReader.Value);
					num = 68;
					continue;
				case 19:
					spr\u22D2.ខ = new Dictionary<string, int>(8)
					{
						{
							RecordTableEnumerator.b("⥆㽈ొ㽌㽎ɐ⍒Ք╖", a_),
							0
						},
						{
							RecordTableEnumerator.b("⁆㭈㭊Ṍ㽎Ő⅒", a_),
							1
						},
						{
							RecordTableEnumerator.b("⑆݈㵊ᵌ㵎", a_),
							2
						},
						{
							RecordTableEnumerator.b("㝆⁈⡊", a_),
							3
						},
						{
							RecordTableEnumerator.b("㑆㥈", a_),
							4
						},
						{
							RecordTableEnumerator.b("⑆ⅈ⩊㽌㭎", a_),
							5
						},
						{
							RecordTableEnumerator.b("⡆⽈ⵊ", a_),
							6
						},
						{
							RecordTableEnumerator.b("≆ㅈ㽊", a_),
							7
						}
					};
					num = 49;
					continue;
				case 20:
					xlsChartShape = new XlsChartShape(A_1.AppImplementation, xlsShape);
					num = 29;
					continue;
				case 21:
					if (((XlsWorksheet)A_1).\u173E == null)
					{
						num = 36;
						continue;
					}
					goto IL_612;
				case 22:
				{
					string s = xmlReader.Value;
					num = 2;
					continue;
				}
				case 23:
					if (xlsChartShape == null)
					{
						num = 30;
						continue;
					}
					goto IL_5D7;
				case 24:
					if (text != null)
					{
						num = 5;
						continue;
					}
					goto IL_558;
				case 25:
					goto IL_5D7;
				case 26:
					((XlsWorksheet)A_1).\u173E = new List<Stream>();
					num = 37;
					continue;
				case 27:
					num = 41;
					continue;
				case 28:
					goto IL_A80;
				case 29:
					goto IL_944;
				case 30:
					xlsChartShape = new XlsChartShape(A_1.AppImplementation, xlsShape);
					num = 25;
					continue;
				case 31:
				{
					string localName;
					if ((localName = xmlReader.LocalName) != null)
					{
						num = 8;
						continue;
					}
					goto IL_32F;
				}
				case 32:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A80;
					default:
						if (false)
						{
						}
						if (xlsShape.ᜩ == null)
						{
							num = 4;
							continue;
						}
						goto IL_6F8;
					}
					break;
				case 34:
					num = 64;
					continue;
				case 35:
					num = 55;
					continue;
				case 36:
					((XlsWorksheet)A_1).\u173E = new List<Stream>();
					num = 46;
					continue;
				case 37:
					goto IL_376;
				case 38:
				{
					int num2;
					string localName;
					if (spr\u22D2.ខ.TryGetValue(localName, out num2))
					{
						num = 63;
						continue;
					}
					goto IL_32F;
				}
				case 39:
					goto IL_32F;
				case 40:
					xlsChartShape.ExtentsY = XmlConvert.ToInt32(xmlReader.Value);
					num = 3;
					continue;
				case 41:
					if (xmlReader.MoveToAttribute(RecordTableEnumerator.b("㽆", a_)))
					{
						num = 67;
						continue;
					}
					goto IL_9D7;
				case 42:
					xlsShape.ᜫ = new List<Stream>();
					num = 66;
					continue;
				case 43:
					if (xmlReader.LocalName == RecordTableEnumerator.b("⡆⽈ⵊ", a_))
					{
						num = 27;
						continue;
					}
					goto IL_32F;
				case 44:
					if (!flag)
					{
						num = 71;
						continue;
					}
					goto IL_7FA;
				case 45:
					goto IL_32F;
				case 46:
					goto IL_612;
				case 47:
					if (xlsChartShape != null)
					{
						num = 60;
						continue;
					}
					goto IL_32F;
				case 48:
					if (xmlReader.MoveToAttribute(RecordTableEnumerator.b("⥆⡈♊⡌", a_)))
					{
						num = 28;
						continue;
					}
					goto IL_32F;
				case 49:
					goto IL_476;
				case 50:
					if (xmlReader.MoveToAttribute(RecordTableEnumerator.b("⑆え", a_)))
					{
						num = 40;
						continue;
					}
					goto IL_32F;
				case 51:
					num = 75;
					continue;
				case 52:
					if (xmlReader.LocalName != RecordTableEnumerator.b("⁆㭈㭊Ṍ㽎", a_))
					{
						num = 34;
						continue;
					}
					return xlsShape;
				case 53:
					goto IL_6F3;
				case 54:
					goto IL_32F;
				case 55:
					if (xlsChartShape == null)
					{
						num = 20;
						continue;
					}
					goto IL_944;
				case 56:
					goto IL_32F;
				case 57:
					goto IL_3DC;
				case 58:
					xlsChartShape.OffsetY = XmlConvert.ToInt32(xmlReader.Value);
					num = 54;
					continue;
				case 59:
					goto IL_9D7;
				case 60:
				{
					string s;
					xlsChartShape.ShapeId = int.Parse(s);
					num = 45;
					continue;
				}
				case 61:
					goto IL_32F;
				case 62:
					goto IL_6F8;
				case 63:
					num = 15;
					continue;
				case 64:
					if (xmlReader.NodeType == XmlNodeType.None)
					{
						num = 13;
						continue;
					}
					num = 31;
					continue;
				case 65:
					if (xmlReader.MoveToAttribute(RecordTableEnumerator.b("⹆ⵈ", a_)))
					{
						num = 22;
						continue;
					}
					goto IL_32F;
				case 66:
					goto IL_71A;
				case 67:
					xlsChartShape.OffsetX = XmlConvert.ToInt32(xmlReader.Value);
					num = 59;
					continue;
				case 68:
					goto IL_39C;
				case 69:
					goto IL_3DC;
				case 70:
					goto IL_32F;
				case 71:
					xmlReader.Read();
					num = 12;
					continue;
				case 72:
					if (xmlReader.LocalName == RecordTableEnumerator.b("≆ㅈ㽊", a_))
					{
						num = 74;
						continue;
					}
					goto IL_32F;
				case 73:
					if (xmlReader.LocalName == RecordTableEnumerator.b("⑆ⅈ⩊㽌㭎", a_))
					{
						num = 35;
						continue;
					}
					goto IL_558;
				case 74:
					num = 1;
					continue;
				case 75:
					goto IL_32F;
				}
				if (A_0 == null)
				{
					num = 7;
					continue;
				}
				num = 11;
				continue;
				IL_32F:
				num = 44;
				continue;
				IL_376:
				((XlsWorksheet)A_1).\u173E.Add(item);
				flag = true;
				num = 39;
				continue;
				IL_39C:
				num = 50;
				continue;
				IL_3DC:
				if (true)
				{
				}
				num = 52;
				continue;
				IL_476:
				num = 38;
				continue;
				IL_558:
				num = 47;
				continue;
				IL_5D7:
				num = 72;
				continue;
				IL_612:
				((XlsWorksheet)A_1).\u173E.Add(item2);
				flag = true;
				num = 61;
				continue;
				IL_6F8:
				xlsShape.ᜩ.Add(item4);
				flag = true;
				num = 56;
				continue;
				IL_71A:
				xlsShape.ᜫ.Add(item3);
				flag = true;
				num = 70;
				continue;
				IL_7FA:
				flag = false;
				num = 69;
				continue;
				IL_944:
				xlsShape.ChildShapes.Add(xlsChartShape);
				XlsChart xlsChart = xlsChartShape.ChartObject;
				sprᡟ sprᡟ = A_1.DataHolder;
				sprវ a_2 = sprᡟ.ᜋ();
				RelationsCollection a_3 = sprᡟ.ᜈ();
				xlsChart.DataHolder = sprᡟ;
				this.ᜀ(xmlReader, xlsChart, a_3, a_2, A_2);
				xlsChart.DataHolder = null;
				num = 24;
				continue;
				IL_9D7:
				num = 6;
				continue;
				IL_A80:
				text = xmlReader.Value;
				num = 65;
			}
			IL_16E:
			throw new ArgumentNullException(RecordTableEnumerator.b("⍆⡈㽊ⱌ", a_));
			IL_6F3:
			throw new ArgumentNullException(RecordTableEnumerator.b("㑆ⅈ⹊⡌㭎", a_));
		}
		}
	}

	// Token: 0x06004FC5 RID: 20421 RVA: 0x0030EAF0 File Offset: 0x0030DAF0
	private MemoryStream \u171A(XmlReader A_0)
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
		MemoryStream memoryStream = new MemoryStream();
		XmlWriter xmlWriter = UtilityMethods.ᜀ(memoryStream, Encoding.UTF8);
		xmlWriter.WriteNode(A_0, false);
		xmlWriter.Flush();
		return memoryStream;
	}

	// Token: 0x06004FC6 RID: 20422 RVA: 0x0030EB4C File Offset: 0x0030DB4C
	private Size \u1719(XmlReader A_0)
	{
		int a_ = 2;
		int num = 5;
		int num2;
		int num3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("嬷䌹", a_)))
				{
					num = 1;
					continue;
				}
				goto IL_120;
			case 1:
				num2 = int.Parse(A_0.Value);
				num = 7;
				continue;
			case 2:
				num3 = int.Parse(A_0.Value);
				num = 4;
				continue;
			case 3:
				goto IL_51;
			case 4:
				goto IL_53;
			case 6:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("嬷䈹", a_)))
				{
					num = 2;
					continue;
				}
				goto IL_53;
			case 7:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_49;
				default:
					goto IL_E0;
				}
				break;
			}
			goto IL_39;
			IL_49:
			num = 3;
			continue;
			IL_39:
			if (A_0 == null)
			{
				goto IL_49;
			}
			num3 = -1;
			num2 = -1;
			num = 6;
			continue;
			IL_53:
			if (true)
			{
			}
			num = 0;
		}
		IL_51:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹崻娽┿ぁ", a_));
		IL_E0:
		if (false)
		{
		}
		IL_120:
		num3 = (int)Math.Round(spr\u17FF.ᜁ((double)num3, MeasureUnits.EMU));
		num2 = (int)Math.Round(spr\u17FF.ᜁ((double)num2, MeasureUnits.EMU));
		return new Size(num3, num2);
	}

	// Token: 0x06004FC7 RID: 20423 RVA: 0x0030ECA0 File Offset: 0x0030DCA0
	private void ᜀ(XlsShape A_0, string A_1)
	{
		int a_ = 19;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!(A_1 == RecordTableEnumerator.b("♈╊⡌౎㑐㽒㥔", a_)))
				{
					num = 3;
					continue;
				}
				goto IL_BA;
			case 2:
				goto IL_B8;
			case 3:
				num = 12;
				continue;
			case 4:
				if (A_1 != null)
				{
					num = 10;
					continue;
				}
				goto IL_1A2;
			case 5:
				return;
			case 6:
				goto IL_62;
			case 7:
				num = 6;
				continue;
			case 8:
				if (!(A_1 == RecordTableEnumerator.b("㵈㱊≌౎㑐㽒㥔", a_)))
				{
					num = 11;
					continue;
				}
				goto IL_CA;
			case 9:
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				num = 4;
				continue;
			case 10:
				if (true)
				{
				}
				num = 8;
				continue;
			case 11:
				num = 0;
				continue;
			case 12:
				if (A_1 == RecordTableEnumerator.b("⡈⥊㹌⁎㵐♒⅔㉖", a_))
				{
					goto IL_D9;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_92;
				default:
					if (false)
					{
					}
					num = 7;
					continue;
				}
				break;
			}
			if (A_1 == null)
			{
				num = 5;
				continue;
			}
			IL_92:
			num = 9;
		}
		return;
		IL_62:
		goto IL_1A2;
		IL_B8:
		throw new ArgumentNullException(RecordTableEnumerator.b("㩈⍊ⱌ㽎㑐", a_));
		IL_BA:
		A_0.IsMoveWithCell = true;
		A_0.IsSizeWithCell = false;
		return;
		IL_CA:
		A_0.IsMoveWithCell = true;
		A_0.IsSizeWithCell = true;
		return;
		IL_D9:
		A_0.IsMoveWithCell = false;
		A_0.IsSizeWithCell = false;
		return;
		IL_1A2:
		throw new XmlException();
	}

	// Token: 0x06004FC8 RID: 20424 RVA: 0x0030EE54 File Offset: 0x0030DE54
	private void ᜀ(XlsShape A_0, Rectangle A_1, Rectangle A_2, Size A_3, bool A_4)
	{
		int a_ = 16;
		sprᮋ sprᮋ;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
		{
			IL_6A:
			IWorksheet a_2 = A_0.Worksheet as XlsWorksheet;
			A_1 = this.ᜀ(A_1, a_2);
			A_2 = this.ᜀ(A_2, a_2);
			sprᮋ = A_0.ClientAnchor;
			sprᮋ.ᜇ(A_1.Left);
			sprᮋ.ᜀ(A_1.Width);
			sprᮋ.ᜆ(A_1.Top);
			sprᮋ.ᜁ(A_1.Height);
			sprᮋ.ᜂ(A_2.Left);
			sprᮋ.ᜃ(A_2.Width);
			sprᮋ.ᜅ(A_2.Top);
			sprᮋ.ᜄ(A_2.Height);
			A_0.EvaluateTopLeftPosition();
			num = 3;
			break;
		}
		default:
			if (false)
			{
			}
			num = 1;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_58;
			case 2:
				goto IL_11F;
			case 3:
				if (A_3.Width < 0)
				{
					num = 2;
					continue;
				}
				goto IL_138;
			}
			if (A_0 != null)
			{
				goto IL_6A;
			}
			if (true)
			{
			}
			num = 0;
		}
		IL_58:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕅⁇⭉㱋⭍", a_));
		IL_11F:
		A_0.UpdateHeight();
		A_0.UpdateWidth();
		return;
		IL_138:
		A_0.Width = A_3.Width;
		A_0.Height = A_3.Height;
		sprᮋ.ᜂ(true);
	}

	// Token: 0x06004FC9 RID: 20425 RVA: 0x0030EFC4 File Offset: 0x0030DFC4
	private Rectangle ᜀ(Rectangle A_0, IWorksheet A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 17;
			int num3;
			int num6;
			for (;;)
			{
				int num2;
				int num4;
				int num7;
				int num9;
				switch (num)
				{
				case 0:
					num2 = 0;
					goto IL_152;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_25D;
					default:
						goto IL_92;
					}
					break;
				case 2:
					if (num3 == 0)
					{
						num = 9;
						continue;
					}
					num = 6;
					continue;
				case 3:
					goto IL_1AD;
				case 4:
					goto IL_211;
				case 5:
					goto IL_211;
				case 6:
				{
					double num5;
					num4 = (int)Math.Round(num5 * 1024.0 / (double)num3);
					goto IL_202;
				}
				case 7:
					if (num6 == 0)
					{
						num = 15;
						continue;
					}
					num = 14;
					continue;
				case 8:
				{
					if (num7 > A_1.Workbook.MaxRowCount)
					{
						num = 10;
						continue;
					}
					double num8 = (double)A_0.Height;
					num8 = spr\u17FF.ᜁ(num8, MeasureUnits.EMU);
					num6 = (A_1 as XlsWorksheet).GetRowHeightPixels(num7);
					num = 7;
					continue;
				}
				case 9:
					num = 12;
					continue;
				case 10:
					A_0.Y = A_1.Workbook.MaxRowCount - 1;
					num6 = 256;
					num = 4;
					continue;
				case 11:
					A_0.X = A_1.Workbook.MaxColumnCount - 1;
					num3 = 1024;
					num = 3;
					continue;
				case 12:
					num4 = 0;
					goto IL_202;
				case 13:
					goto IL_20F;
				case 14:
				{
					double num8;
					num2 = (int)Math.Round(num8 * 256.0 / (double)num6);
					goto IL_152;
				}
				case 15:
					num = 0;
					continue;
				case 16:
				{
					if (num9 > A_1.Workbook.MaxColumnCount)
					{
						num = 11;
						continue;
					}
					double num5 = (double)A_0.Width;
					num5 = spr\u17FF.ᜁ(num5, MeasureUnits.EMU);
					num3 = (A_1 as XlsWorksheet).GetColumnWidthPixels(num9);
					goto IL_25D;
				}
				}
				if (true)
				{
				}
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				num3 = 0;
				num6 = 0;
				num9 = A_0.Left + 1;
				num7 = A_0.Top + 1;
				num = 8;
				continue;
				IL_152:
				num6 = num2;
				num = 5;
				continue;
				IL_202:
				num3 = num4;
				num = 13;
				continue;
				IL_211:
				num = 16;
				continue;
				IL_25D:
				num = 2;
			}
			IL_92:
			if (false)
			{
			}
			return A_0;
			IL_1AD:
			IL_20F:
			A_0.Width = num3;
			A_0.Height = num6;
			return A_0;
		}
		}
	}

	// Token: 0x06004FCA RID: 20426 RVA: 0x0030F264 File Offset: 0x0030E264
	private Rectangle \u1718(XmlReader A_0)
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			int num = 6;
			Rectangle result;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_1D8;
				case 1:
					goto IL_1D8;
				case 2:
					goto IL_202;
				case 3:
					goto IL_1D8;
				case 4:
					goto IL_1D8;
				case 5:
				{
					string localName = A_0.LocalName;
					string s = A_0.ReadElementContentAsString();
					num = 12;
					continue;
				}
				case 7:
					goto IL_1FD;
				case 8:
					spr\u22D2.គ = new Dictionary<string, int>(6)
					{
						{
							RecordTableEnumerator.b("堺刼匾", a_),
							0
						},
						{
							RecordTableEnumerator.b("堺刼匾เ╂⍄", a_),
							1
						},
						{
							RecordTableEnumerator.b("䤺刼䠾", a_),
							2
						},
						{
							RecordTableEnumerator.b("䤺刼䠾เ╂⍄", a_),
							3
						},
						{
							RecordTableEnumerator.b("䌺", a_),
							4
						},
						{
							RecordTableEnumerator.b("䈺", a_),
							5
						}
					};
					num = 2;
					continue;
				case 9:
					if (spr\u22D2.គ == null)
					{
						num = 8;
						continue;
					}
					goto IL_202;
				case 10:
					num = 23;
					continue;
				case 11:
				{
					int num2;
					switch (num2)
					{
					case 0:
					{
						string s;
						result.X = XmlConvert.ToInt32(s);
						num = 1;
						continue;
					}
					case 1:
					{
						string s;
						result.Width = XmlConvert.ToInt32(s);
						num = 18;
						continue;
					}
					case 2:
					{
						if (true)
						{
						}
						string s;
						result.Y = XmlConvert.ToInt32(s);
						num = 16;
						continue;
					}
					case 3:
					{
						string s;
						result.Height = XmlConvert.ToInt32(s);
						num = 0;
						continue;
					}
					case 4:
					{
						string s;
						result.X = (int)(XmlConvert.ToDouble(s) * 1000.0);
						num = 4;
						continue;
					}
					case 5:
					{
						string s;
						result.Y = (int)(XmlConvert.ToDouble(s) * 1000.0);
						num = 22;
						continue;
					}
					default:
						num = 10;
						continue;
					}
					break;
				}
				case 12:
				{
					string localName;
					string key;
					if ((key = localName) != null)
					{
						num = 21;
						continue;
					}
					goto IL_113;
				}
				case 13:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 5;
						continue;
					}
					A_0.Skip();
					num = 15;
					continue;
				case 14:
				{
					int num2;
					string key;
					if (!spr\u22D2.គ.TryGetValue(key, out num2))
					{
						goto IL_113;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						num = 20;
						continue;
					}
					break;
				}
				case 15:
					goto IL_1D8;
				case 16:
					goto IL_1D8;
				case 17:
					goto IL_98;
				case 18:
					goto IL_1D8;
				case 19:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 7;
						continue;
					}
					num = 13;
					continue;
				case 20:
					num = 11;
					continue;
				case 21:
					num = 9;
					continue;
				case 22:
					goto IL_1D8;
				case 23:
					goto IL_18A;
				}
				if (A_0 == null)
				{
					num = 17;
					continue;
				}
				result = default(Rectangle);
				A_0.Read();
				num = 3;
				continue;
				IL_1D8:
				num = 19;
				continue;
				IL_202:
				num = 14;
			}
			IL_98:
			throw new ArgumentNullException(RecordTableEnumerator.b("䤺堼帾╀♂㝄", a_));
			IL_113:
			throw new XmlException(RecordTableEnumerator.b("渺匼娾㥀㍂⁄⑆㵈⹊⥌潎⥐㹒㥔睖ⵘ㩚㩜煞", a_));
			IL_18A:
			goto IL_113;
			IL_1FD:
			A_0.Read();
			return result;
		}
		}
	}

	// Token: 0x06004FCB RID: 20427 RVA: 0x0030F624 File Offset: 0x0030E624
	private XlsShape ᜀ(XmlReader A_0, XlsWorksheetBase A_1, string A_2, List<string> A_3, Dictionary<string, object> A_4)
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			int num = 20;
			XlsBitmapShape xlsBitmapShape;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_400;
				case 1:
					num = 5;
					continue;
				case 2:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 6;
						continue;
					}
					num = 11;
					continue;
				case 3:
					num = 10;
					continue;
				case 4:
					if (A_1 == null)
					{
						num = 0;
						continue;
					}
					num = 7;
					continue;
				case 5:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("䔵䠷樹主", a_)))
					{
						num = 3;
						continue;
					}
					this.ᜂ(A_0, xlsBitmapShape);
					num = 21;
					continue;
				}
				case 6:
					goto IL_24A;
				case 7:
					if (A_3 == null)
					{
						num = 18;
						continue;
					}
					num = 13;
					continue;
				case 8:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("堵丷樹唻崽ဿぁ", a_)))
					{
						num = 14;
						continue;
					}
					RelationsCollection a_2;
					sprវ a_3;
					this.ᜁ(A_0, xlsBitmapShape, a_2, A_2, a_3, A_3, A_4);
					num = 9;
					continue;
				}
				case 9:
					goto IL_209;
				case 10:
					goto IL_1F5;
				case 11:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 25;
						continue;
					}
					A_0.Skip();
					num = 26;
					continue;
				case 12:
					goto IL_209;
				case 13:
				{
					if (A_0.LocalName != RecordTableEnumerator.b("䘵儷夹", a_))
					{
						num = 17;
						continue;
					}
					xlsBitmapShape = new ExcelPicture((spr\u2158)A_1.AppImplementation, A_1.InnerShapes);
					sprᡟ sprᡟ = A_1.DataHolder;
					RelationsCollection a_2 = sprᡟ.ᜈ();
					sprវ a_3 = sprᡟ.ᜋ();
					num = 16;
					continue;
				}
				case 14:
					num = 24;
					continue;
				case 15:
					goto IL_209;
				case 16:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("嬵夷夹主儽", a_)))
					{
						num = 27;
						continue;
					}
					goto IL_35E;
				case 17:
					goto IL_1F0;
				case 18:
					goto IL_2A6;
				case 19:
					goto IL_209;
				case 21:
					goto IL_209;
				case 22:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 28;
						continue;
					}
					goto IL_1F5;
				}
				case 23:
					goto IL_BE;
				case 24:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("吵吷匹䰻砽⤿⹁⡃", a_)))
					{
						num = 1;
						continue;
					}
					RelationsCollection a_2;
					sprវ a_3;
					this.ᜁ(A_0, xlsBitmapShape, a_2, A_2, a_3, A_3, A_4);
					num = 12;
					continue;
				}
				case 25:
					num = 22;
					continue;
				case 26:
					goto IL_209;
				case 27:
					xlsBitmapShape.Macro = A_0.Value;
					A_0.MoveToElement();
					num = 29;
					continue;
				case 28:
					num = 8;
					continue;
				case 29:
					goto IL_35E;
				}
				if (A_0 == null)
				{
					num = 23;
					continue;
				}
				num = 4;
				continue;
				IL_1F5:
				A_0.Skip();
				num = 19;
				continue;
				IL_209:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_405;
				default:
					if (false)
					{
					}
					num = 2;
					continue;
				}
				IL_35E:
				A_0.Read();
				num = 15;
			}
			IL_BE:
			throw new ArgumentNullException(RecordTableEnumerator.b("䐵崷嬹堻嬽㈿", a_));
			IL_1F0:
			throw new XmlException(RecordTableEnumerator.b("挵嘷弹䐻丽┿⅁ぃ⍅ⱇ橉㑋⍍㱏牑⁓㝕㽗瑙", a_));
			IL_24A:
			goto IL_405;
			IL_2A6:
			throw new ArgumentNullException(RecordTableEnumerator.b("娵䬷丹渻嬽ⰿ⍁ぃ⽅❇⑉Ջ⩍⍏", a_));
			IL_400:
			throw new ArgumentNullException(RecordTableEnumerator.b("䔵倷弹夻䨽", a_));
			IL_405:
			A_0.Read();
			A_1.InnerShapes.ᜀ(xlsBitmapShape);
			return xlsBitmapShape;
		}
		}
	}

	// Token: 0x06004FCC RID: 20428 RVA: 0x0030FA4C File Offset: 0x0030EA4C
	private void ᜂ(XmlReader A_0, XlsShape A_1)
	{
		int a_ = 0;
		int num = 3;
		XlsBitmapShape xlsBitmapShape;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					num = 5;
					continue;
				}
				if (true)
				{
				}
				num = 7;
				continue;
			case 1:
				goto IL_116;
			case 2:
				goto IL_4E;
			case 4:
				goto IL_9D;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_10B;
				default:
					goto IL_12E;
				}
				break;
			case 6:
				if (xlsBitmapShape != null)
				{
					goto IL_10B;
				}
				goto IL_148;
			case 7:
				if (A_0.LocalName != RecordTableEnumerator.b("䔵䠷樹主", a_))
				{
					num = 4;
					continue;
				}
				xlsBitmapShape = (A_1 as XlsBitmapShape);
				num = 6;
				continue;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			num = 0;
			continue;
			IL_10B:
			num = 1;
		}
		IL_4E:
		throw new ArgumentNullException(RecordTableEnumerator.b("䐵崷嬹堻嬽㈿", a_));
		IL_9D:
		throw new XmlException(RecordTableEnumerator.b("挵嘷弹䐻丽┿⅁ぃ⍅ⱇ橉㑋⍍㱏牑⁓㝕㽗瑙", a_));
		IL_116:
		Stream a_2 = new MemoryStream();
		XmlWriter xmlWriter = UtilityMethods.ᜀ(a_2, Encoding.UTF8);
		xmlWriter.WriteNode(A_0, false);
		xmlWriter.Flush();
		xlsBitmapShape.ShapePropertiesStream = a_2;
		return;
		IL_12E:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䔵倷嬹䰻嬽", a_));
		IL_148:
		A_0.Skip();
	}

	// Token: 0x06004FCD RID: 20429 RVA: 0x0030FBA8 File Offset: 0x0030EBA8
	private void ᜁ(XmlReader A_0, XlsBitmapShape A_1, RelationsCollection A_2, string A_3, sprវ A_4, List<string> A_5, Dictionary<string, object> A_6)
	{
		int a_ = 8;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 13;
				continue;
			case 1:
				goto IL_1D0;
			case 2:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("䴽㈿⅁ᙃ⍅⭇㹉", a_)))
				{
					num = 5;
					continue;
				}
				MemoryStream a_2 = new MemoryStream();
				XmlWriter xmlWriter = UtilityMethods.ᜀ(a_2, Encoding.UTF8);
				xmlWriter.WriteNode(A_0, false);
				xmlWriter.Flush();
				A_1.SourceRectStream = a_2;
				num = 1;
				continue;
			}
			case 3:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("䴽㐿ぁ⅃㉅⭇≉", a_)))
				{
					num = 0;
					continue;
				}
				goto IL_F7;
			}
			case 4:
				goto IL_9A;
			case 5:
				num = 3;
				continue;
			case 7:
				if (true)
				{
				}
				goto IL_1D0;
			case 8:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 9;
					continue;
				}
				goto IL_1BC;
			}
			case 9:
				num = 12;
				continue;
			case 10:
				goto IL_1D0;
			case 11:
				num = 23;
				continue;
			case 12:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("尽ⰿ⭁㑃", a_)))
				{
					num = 26;
					continue;
				}
				this.ᜀ(A_0, A_1, A_2, A_3, A_4, A_5, A_6);
				num = 10;
				continue;
			}
			case 13:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("䨽⤿⹁⅃", a_)))
				{
					num = 11;
					continue;
				}
				goto IL_F7;
			}
			case 14:
				goto IL_35A;
			case 15:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 18;
					continue;
				}
				A_0.Skip();
				num = 25;
				continue;
			case 16:
				if (A_5 == null)
				{
					num = 19;
					continue;
				}
				num = 21;
				continue;
			case 17:
				goto IL_1F3;
			case 18:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_AF;
				default:
					if (false)
					{
					}
					num = 8;
					continue;
				}
				break;
			case 19:
				goto IL_248;
			case 20:
				goto IL_1B7;
			case 21:
				if (A_0.LocalName != RecordTableEnumerator.b("尽ⰿ⭁㑃Eⅇ♉⁋", a_))
				{
					num = 20;
					continue;
				}
				A_0.Read();
				num = 7;
				continue;
			case 22:
				goto IL_1D0;
			case 23:
				goto IL_AA;
			case 24:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 17;
					continue;
				}
				num = 15;
				continue;
			case 25:
				goto IL_1D0;
			case 26:
				goto IL_AF;
			case 27:
				if (A_1 == null)
				{
					num = 14;
					continue;
				}
				num = 16;
				continue;
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			num = 27;
			continue;
			IL_AF:
			num = 2;
			continue;
			IL_F7:
			A_0.Skip();
			num = 22;
			continue;
			IL_1D0:
			num = 24;
		}
		IL_9A:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰽┿⍁⁃⍅㩇", a_));
		IL_AA:
		goto IL_1BC;
		IL_1B7:
		throw new XmlException(RecordTableEnumerator.b("欽⸿❁㱃㙅ⵇ⥉㡋⭍㑏牑ⱓ㭕㑗穙⡛㽝ݟ䱡", a_));
		IL_1BC:
		throw new XmlException(RecordTableEnumerator.b("欽⸿❁㱃㙅ⵇ⥉㡋⭍㑏牑ⱓ㭕㑗穙⡛㽝ݟ䱡", a_));
		IL_1F3:
		A_0.Read();
		return;
		IL_248:
		throw new ArgumentNullException(RecordTableEnumerator.b("刽㌿㙁ᙃ⍅⑇⭉㡋❍㽏㱑ᵓ㉕⭗", a_));
		IL_35A:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴽⠿⍁㑃⍅", a_));
	}

	// Token: 0x06004FCE RID: 20430 RVA: 0x0030FF50 File Offset: 0x0030EF50
	private void ᜀ(XmlReader A_0, XlsBitmapShape A_1, RelationsCollection A_2, string A_3, sprវ A_4, List<string> A_5, Dictionary<string, object> A_6)
	{
		int a_ = 8;
		switch (0)
		{
		default:
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_26D:
				num = 22;
				break;
			default:
				if (false)
				{
				}
				num = 5;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					string value = A_0.Value;
					sprᦨ sprᦨ = A_2[value];
					A_5.Add(value);
					num = 21;
					continue;
				}
				case 1:
					if (!A_0.IsEmptyElement)
					{
						num = 14;
						continue;
					}
					goto IL_3C5;
				case 2:
					goto IL_20E;
				case 3:
					goto IL_B4;
				case 4:
					goto IL_1CB;
				case 6:
					goto IL_304;
				case 7:
				{
					XmlWriter xmlWriter;
					xmlWriter.WriteEndElement();
					xmlWriter.Flush();
					MemoryStream a_2;
					A_1.BlipSubNodesStream = a_2;
					num = 12;
					continue;
				}
				case 8:
					goto IL_1CB;
				case 9:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("嬽ⴿ⁁⅃≅", a_), RecordTableEnumerator.b("嘽㐿㙁㑃籅杇敉㽋ⵍ㡏㝑㥓㝕⭗瑙㍛⹝՟ౡᱣ୥ѧ౩ͫᱭᵯ፱sյ噷ᕹ๻᥽꽿쪍ﾏﮕﶗ놝銟銡钣邥螧\ud8a9즫슭톯욱\uddb3\ud9b5횷즹풻ힽ낿뇁", a_)))
					{
						num = 0;
						continue;
					}
					goto IL_304;
				case 10:
					if (A_3 == null)
					{
						num = 11;
						continue;
					}
					goto IL_26D;
				case 11:
					goto IL_2BE;
				case 12:
					goto IL_268;
				case 13:
					if (A_1 == null)
					{
						num = 23;
						continue;
					}
					num = 20;
					continue;
				case 14:
				{
					MemoryStream a_2 = new MemoryStream();
					XmlWriter xmlWriter = UtilityMethods.ᜀ(a_2, Encoding.UTF8);
					xmlWriter.WriteStartElement(RecordTableEnumerator.b("䰽⼿ⵁぃ", a_));
					A_0.Read();
					num = 8;
					continue;
				}
				case 15:
					goto IL_28C;
				case 16:
					if (A_5 == null)
					{
						num = 2;
						continue;
					}
					num = 9;
					continue;
				case 17:
				{
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 7;
						continue;
					}
					XmlWriter xmlWriter;
					xmlWriter.WriteNode(A_0, false);
					num = 4;
					continue;
				}
				case 18:
					goto IL_15F;
				case 19:
					goto IL_368;
				case 20:
					if (A_2 == null)
					{
						num = 18;
						continue;
					}
					num = 10;
					continue;
				case 21:
				{
					sprᦨ sprᦨ;
					if (sprᦨ == null)
					{
						num = 19;
						continue;
					}
					spr\u2570 spr_u = A_4.ᜀ(sprᦨ, A_3);
					Image picture = A_4.ᜋ(spr_u.ᜇ());
					A_1.Picture = picture;
					XlsWorkbook xlsWorkbook = (XlsWorkbook)A_1.Workbook;
					sprᜪ sprᜪ = xlsWorkbook.ShapesData.Pictures[(int)(A_1.BlipId - 1U)];
					sprᜪ.ᜀ(spr_u.ᜇ());
					A_6[spr_u.ᜇ()] = null;
					num = 6;
					continue;
				}
				case 22:
					if (A_4 == null)
					{
						num = 15;
						continue;
					}
					num = 16;
					continue;
				case 23:
					goto IL_2EB;
				}
				if (A_0 == null)
				{
					num = 3;
					continue;
				}
				num = 13;
				continue;
				IL_1CB:
				num = 17;
				continue;
				IL_304:
				A_0.MoveToElement();
				num = 1;
			}
			IL_B4:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰽┿⍁⁃⍅㩇", a_));
			IL_15F:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰽┿⹁╃㉅ⅇ╉≋㵍", a_));
			IL_20E:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("刽㌿㙁ᙃ⍅⑇⭉㡋❍㽏㱑ᵓ㉕⭗", a_));
			IL_268:
			goto IL_3C5;
			IL_28C:
			throw new ArgumentNullException(RecordTableEnumerator.b("嘽⼿⹁⁃⍅㩇", a_));
			IL_2BE:
			throw new ArgumentNullException(RecordTableEnumerator.b("䴽㐿ぁᑃ❅㩇⽉≋㩍O㍑⁓㹕", a_));
			IL_2EB:
			throw new ArgumentNullException(RecordTableEnumerator.b("䴽⠿⍁㑃⍅", a_));
			IL_368:
			throw new XmlException(RecordTableEnumerator.b("紽ℿⱁ⩃⥅㱇橉⩋❍㹏㙑瑓⑕㵗⭙⥛㝝቟ݡc䙥ᩧཀྵk཭ѯ᭱᭳ᡵ", a_));
			IL_3C5:
			A_0.Skip();
			return;
		}
		}
	}

	// Token: 0x06004FCF RID: 20431 RVA: 0x00310328 File Offset: 0x0030F328
	private void ᜁ(XmlReader A_0, XlsShape A_1, RelationsCollection A_2, string A_3, sprវ A_4, List<string> A_5, Dictionary<string, object> A_6)
	{
		int a_ = 0;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_38B;
			case 1:
				if (A_2 == null)
				{
					num = 8;
					continue;
				}
				num = 14;
				continue;
			case 2:
				goto IL_312;
			case 3:
				num = 6;
				continue;
			case 5:
				if (A_4 == null)
				{
					num = 26;
					continue;
				}
				num = 23;
				continue;
			case 6:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("唵瘷䰹氻圽⌿ቁ㙃", a_)))
				{
					num = 16;
					continue;
				}
				this.ᜁ(A_0, A_1);
				if (true)
				{
				}
				num = 24;
				continue;
			}
			case 7:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 20;
					continue;
				}
				goto IL_390;
			}
			case 8:
				goto IL_2A1;
			case 9:
				goto IL_258;
			case 10:
				if (A_0.LocalName != RecordTableEnumerator.b("堵丷樹唻崽ဿぁ", a_))
				{
					num = 2;
					continue;
				}
				A_0.Read();
				num = 15;
				continue;
			case 11:
				num = 7;
				continue;
			case 12:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("帵吷匹刻唽̿⹁ⵃ╅⍇", a_)))
				{
					num = 25;
					continue;
				}
				this.ᜀ(A_0, A_1, A_2, A_3, A_4, A_5, A_6);
				num = 28;
				continue;
			}
			case 13:
				goto IL_213;
			case 14:
				if (A_3 == null)
				{
					num = 17;
					continue;
				}
				num = 5;
				continue;
			case 15:
				goto IL_213;
			case 16:
				num = 12;
				continue;
			case 17:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_14A;
				}
				break;
			case 18:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 11;
					continue;
				}
				A_0.Skip();
				num = 19;
				continue;
			case 19:
				goto IL_213;
			case 20:
				num = 21;
				continue;
			case 21:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("唵瘷䰹氻䰽", a_)))
				{
					num = 3;
					continue;
				}
				spr\u2306.ᜀ(A_0, A_1);
				num = 13;
				continue;
			}
			case 22:
				goto IL_B7;
			case 23:
				if (A_5 == null)
				{
					num = 9;
					continue;
				}
				num = 10;
				continue;
			case 24:
				goto IL_213;
			case 25:
				num = 30;
				continue;
			case 26:
				goto IL_36A;
			case 27:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				num = 1;
				continue;
			case 28:
				goto IL_213;
			case 29:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 31;
					continue;
				}
				num = 18;
				continue;
			case 30:
				goto IL_2D9;
			case 31:
				goto IL_236;
			}
			if (A_0 == null)
			{
				num = 22;
				continue;
			}
			num = 27;
			continue;
			IL_213:
			num = 29;
		}
		IL_B7:
		throw new ArgumentNullException(RecordTableEnumerator.b("䐵崷嬹堻嬽㈿", a_));
		IL_14A:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䔵䰷䠹氻弽㈿❁⩃㉅ᡇ⭉㡋♍", a_));
		IL_236:
		A_0.Read();
		return;
		IL_258:
		throw new ArgumentNullException(RecordTableEnumerator.b("娵䬷丹渻嬽ⰿ⍁ぃ⽅❇⑉Ջ⩍⍏", a_));
		IL_2A1:
		throw new ArgumentNullException(RecordTableEnumerator.b("䐵崷嘹崻䨽⤿ⵁ⩃㕅", a_));
		IL_2D9:
		goto IL_390;
		IL_312:
		throw new XmlException(RecordTableEnumerator.b("挵嘷弹䐻崽〿❁❃㉅ⵇ⹉汋㙍㵏㹑瑓≕㥗㵙", a_));
		IL_36A:
		throw new ArgumentNullException(RecordTableEnumerator.b("帵圷嘹堻嬽㈿", a_));
		IL_38B:
		throw new ArgumentNullException(RecordTableEnumerator.b("䔵倷嬹䰻嬽", a_));
		IL_390:
		throw new XmlException(RecordTableEnumerator.b("挵嘷弹䐻丽┿⅁ぃ⍅ⱇ橉㑋⍍㱏牑⁓㝕㽗瑙", a_));
	}

	// Token: 0x06004FD0 RID: 20432 RVA: 0x00310728 File Offset: 0x0030F728
	private void ᜁ(XmlReader A_0, XlsShape A_1)
	{
		int a_ = 14;
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
					goto IL_D0;
				default:
					if (false)
					{
					}
					if (A_0.LocalName != RecordTableEnumerator.b("❃ࡅ㹇ᩉ╋ⵍO⁑", a_))
					{
						num = 4;
						continue;
					}
					goto IL_F6;
				}
				break;
			case 2:
				goto IL_3F;
			case 3:
				goto IL_F4;
			case 4:
				goto IL_88;
			case 5:
				if (A_1 == null)
				{
					if (true)
					{
					}
					num = 3;
					continue;
				}
				num = 1;
				continue;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			IL_D0:
			num = 5;
		}
		IL_3F:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙃⍅⥇⹉⥋㱍", a_));
		IL_88:
		throw new XmlException(RecordTableEnumerator.b("ᅃ⡅ⵇ㉉㱋⭍㍏♑ㅓ㉕硗≙ㅛ㉝䁟ᙡգť䙧", a_));
		IL_F4:
		throw new ArgumentNullException(RecordTableEnumerator.b("㝃⹅⥇㩉⥋", a_));
		IL_F6:
		A_0.Skip();
	}

	// Token: 0x06004FD1 RID: 20433 RVA: 0x00310834 File Offset: 0x0030F834
	internal static void ᜀ(XmlReader A_0, XlsShape A_1)
	{
		int a_ = 1;
		int num = 13;
		for (;;)
		{
			switch (num)
			{
			case 0:
				A_1.AlternativeText = A_0.Value;
				num = 2;
				continue;
			case 1:
				goto IL_7C;
			case 2:
				goto IL_DF;
			case 3:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("弶倸强夼娾⽀", a_)))
				{
					num = 5;
					continue;
				}
				goto IL_27A;
			case 4:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("帶崸", a_)))
				{
					num = 15;
					continue;
				}
				goto IL_21E;
			case 5:
				A_1.Visible = !XmlConvert.ToBoolean(A_0.Value);
				num = 14;
				continue;
			case 6:
				if (A_1 == null)
				{
					num = 7;
					continue;
				}
				num = 8;
				continue;
			case 7:
				goto IL_12C;
			case 8:
				if (!(A_0.LocalName != RecordTableEnumerator.b("吶眸䴺洼䴾", a_)))
				{
					A_1.Visible = true;
					num = 4;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_110;
				default:
					if (false)
					{
					}
					num = 9;
					continue;
				}
				break;
			case 9:
				goto IL_1F6;
			case 10:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("夶堸嘺堼", a_)))
				{
					num = 11;
					continue;
				}
				goto IL_7C;
			case 11:
				A_1.Name = A_0.Value;
				num = 1;
				continue;
			case 12:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("匶尸䠺帼䴾", a_)))
				{
					num = 0;
					continue;
				}
				goto IL_DF;
			case 14:
				goto IL_169;
			case 15:
				A_1.ShapeId = XmlConvert.ToInt32(A_0.Value);
				num = 17;
				continue;
			case 16:
				goto IL_77;
			case 17:
				goto IL_21E;
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 16;
				continue;
			}
			goto IL_110;
			IL_7C:
			num = 12;
			continue;
			IL_DF:
			num = 3;
			continue;
			IL_110:
			num = 6;
			continue;
			IL_21E:
			num = 10;
		}
		IL_77:
		throw new ArgumentNullException(RecordTableEnumerator.b("䔶尸娺夼娾㍀", a_));
		IL_12C:
		throw new ArgumentNullException(RecordTableEnumerator.b("䐶儸娺䴼娾", a_));
		IL_169:
		goto IL_27A;
		IL_1F6:
		throw new XmlException(RecordTableEnumerator.b("戶圸帺䔼伾⑀⁂ㅄ≆ⵈ歊㕌≎㵐獒⅔㙖㹘畚", a_));
		IL_27A:
		A_0.MoveToElement();
		A_0.Skip();
	}

	// Token: 0x06004FD2 RID: 20434 RVA: 0x00310AC8 File Offset: 0x0030FAC8
	private void ᜀ(XmlReader A_0, XlsShape A_1, RelationsCollection A_2, string A_3, sprវ A_4, List<string> A_5, Dictionary<string, object> A_6)
	{
		int a_ = 18;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				if (true)
				{
				}
				if (A_4 == null)
				{
					num = 11;
					continue;
				}
				num = 7;
				continue;
			case 2:
				goto IL_179;
			case 3:
				if (A_2 == null)
				{
					num = 2;
					continue;
				}
				num = 9;
				continue;
			case 4:
				goto IL_6B;
			case 5:
				goto IL_104;
			case 6:
				goto IL_DC;
			case 7:
				if (A_5 == null)
				{
					num = 10;
					continue;
				}
				num = 12;
				continue;
			case 8:
			{
				string value = A_0.Value;
				sprᦨ sprᦨ = A_2[value];
				A_5.Add(value);
				num = 14;
				continue;
			}
			case 9:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_EC;
				default:
					if (false)
					{
					}
					if (A_3 == null)
					{
						num = 16;
						continue;
					}
					num = 1;
					continue;
				}
				break;
			case 10:
				goto IL_120;
			case 11:
				goto IL_259;
			case 12:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("ⅇ⹉", a_), RecordTableEnumerator.b("⁇㹉㡋㹍橏絑筓╕㭗㉙㥛㍝şᅡ䩣॥ᡧཀྵɫ᙭ᵯṱታ᥵੷᝹ᵻ੽겁ꖉﮑ\udc97ﾛ춟잡쪣튥螧颩鲫麭蚯鶱욳펵풷\udbb9좻ힽ꾿곁럃껅ꇇ뫉뿋", a_)))
				{
					num = 8;
					continue;
				}
				goto IL_25E;
			case 13:
				goto IL_EC;
			case 14:
			{
				sprᦨ sprᦨ;
				if (sprᦨ == null)
				{
					num = 6;
					continue;
				}
				A_1.ImageRelation = sprᦨ;
				A_1.IsHyperlink = true;
				A_0.Skip();
				num = 15;
				continue;
			}
			case 15:
				goto IL_144;
			case 16:
				goto IL_22F;
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			num = 13;
			continue;
			IL_EC:
			if (A_1 == null)
			{
				num = 5;
			}
			else
			{
				num = 3;
			}
		}
		IL_6B:
		throw new ArgumentNullException(RecordTableEnumerator.b("㩇⽉ⵋ⩍㕏⁑", a_));
		IL_DC:
		throw new XmlException(RecordTableEnumerator.b("େ⭉≋⁍㽏♑瑓さㅗ㑙㡛繝቟ݡᕣ፥ŧᡩ५੭偯qᅳ᩵᥷๹ᕻᅽ", a_));
		IL_104:
		throw new ArgumentNullException(RecordTableEnumerator.b("㭇≉ⵋ㹍㕏", a_));
		IL_120:
		throw new ArgumentNullException(RecordTableEnumerator.b("⑇㥉㡋ᱍ㕏㹑㕓≕ㅗ㕙㉛᝝џᅡ", a_));
		IL_144:
		goto IL_25E;
		IL_179:
		throw new ArgumentNullException(RecordTableEnumerator.b("㩇⽉⁋⽍⑏㭑㭓㡕⭗", a_));
		IL_22F:
		throw new ArgumentNullException(RecordTableEnumerator.b("㭇㹉㹋ṍㅏ⁑ㅓ㡕ⱗਖ਼㵛⩝࡟", a_));
		IL_259:
		throw new ArgumentNullException(RecordTableEnumerator.b("⁇╉⁋⩍㕏⁑", a_));
		IL_25E:
		A_0.Skip();
	}

	// Token: 0x06004FD3 RID: 20435 RVA: 0x00310D3C File Offset: 0x0030FD3C
	private void ᜀ(XmlReader A_0, List<string> A_1, XlsWorksheet A_2)
	{
		int a_ = 19;
		int num = 13;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_128;
			case 1:
				goto IL_17E;
			case 2:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 3;
					continue;
				}
				num = 16;
				continue;
			case 3:
				return;
			case 4:
				goto IL_75;
			case 5:
				this.ᜀ(A_0, A_1, A_2);
				num = 10;
				continue;
			case 6:
				num = 9;
				continue;
			case 7:
				goto IL_14E;
			case 8:
				if (A_1 == null)
				{
					if (true)
					{
					}
					num = 0;
					continue;
				}
				num = 15;
				continue;
			case 9:
				if (A_0.LocalName == RecordTableEnumerator.b("⩈⑊⁌≎㑐㵒⅔", a_))
				{
					num = 5;
					continue;
				}
				goto IL_F1;
			case 10:
				goto IL_14E;
			case 11:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_17E;
				default:
					goto IL_C3;
				}
				break;
			case 12:
				goto IL_1C2;
			case 14:
				goto IL_14E;
			case 15:
				if (A_2 == null)
				{
					num = 12;
					continue;
				}
				num = 1;
				continue;
			case 16:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 6;
					continue;
				}
				goto IL_F1;
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			num = 8;
			continue;
			IL_F1:
			A_0.Skip();
			num = 7;
			continue;
			IL_14E:
			num = 2;
			continue;
			IL_17E:
			if (A_0.LocalName != RecordTableEnumerator.b("⩈⑊⁌≎㑐㵒⅔᭖じ⡚⥜", a_))
			{
				num = 11;
			}
			else
			{
				A_0.Read();
				num = 14;
			}
		}
		IL_75:
		throw new ArgumentNullException(RecordTableEnumerator.b("㭈⹊ⱌ⭎㑐⅒", a_));
		IL_C3:
		if (false)
		{
		}
		throw new XmlException(RecordTableEnumerator.b("᱈╊⡌㝎⅐㙒㙔⍖㱘㽚絜⭞`Ѣ", a_));
		IL_128:
		throw new ArgumentNullException(RecordTableEnumerator.b("⡈㥊㽌๎⑐❒㵔㡖⭘⡚", a_));
		IL_1C2:
		throw new ArgumentNullException(RecordTableEnumerator.b("㩈⍊⡌⩎═", a_));
	}

	// Token: 0x06004FD4 RID: 20436 RVA: 0x00310F68 File Offset: 0x0030FF68
	private void ᜀ(XmlReader A_0, IList<string> A_1, XlsWorksheet A_2)
	{
		int a_ = 16;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			}
			break;
		}
		int num = 11;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_28B;
			case 1:
				goto IL_124;
			case 2:
				if (A_1 == null)
				{
					num = 16;
					continue;
				}
				num = 10;
				continue;
			case 3:
				num = 13;
				continue;
			case 4:
			{
				if (!A_0.MoveToAttribute(RecordTableEnumerator.b("㑅ⵇⱉ", a_)))
				{
					num = 15;
					continue;
				}
				string value = A_0.Value;
				num = 8;
				continue;
			}
			case 5:
				if (A_0.LocalName != RecordTableEnumerator.b("╅❇❉⅋⭍㹏♑", a_))
				{
					num = 0;
					continue;
				}
				num = 4;
				continue;
			case 6:
				goto IL_15C;
			case 7:
				if (!A_0.IsEmptyElement)
				{
					num = 14;
					continue;
				}
				return;
			case 8:
			{
				if (!A_0.MoveToAttribute(RecordTableEnumerator.b("❅㵇㹉⑋⅍≏᭑こ", a_)))
				{
					num = 17;
					continue;
				}
				int index = int.Parse(A_0.Value);
				string value;
				XlsComment xlsComment = A_2[value].AddComment() as XlsComment;
				xlsComment.Author = A_1[index];
				num = 7;
				continue;
			}
			case 9:
				goto IL_1C5;
			case 10:
				if (A_2 == null)
				{
					num = 1;
					continue;
				}
				num = 5;
				continue;
			case 12:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 3;
					continue;
				}
				goto IL_228;
			case 13:
			{
				if (A_0.LocalName != RecordTableEnumerator.b("㉅ⵇ㉉㡋", a_))
				{
					num = 6;
					continue;
				}
				A_0.Read();
				spr\u223A a_2 = this.ᜂ(A_0, RecordTableEnumerator.b("㉅ⵇ㉉㡋", a_));
				A_0.Read();
				XlsComment xlsComment;
				xlsComment.ᜀ(a_2);
				A_0.Read();
				num = 9;
				continue;
			}
			case 14:
				goto IL_228;
			case 15:
				goto IL_1FB;
			case 16:
				goto IL_17C;
			case 17:
				goto IL_301;
			case 18:
				goto IL_A8;
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 18;
				continue;
			}
			num = 2;
			continue;
			IL_228:
			A_0.Read();
			num = 12;
		}
		IL_A8:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑅ⵇ⭉⡋⭍≏", a_));
		IL_124:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕅⁇⽉⥋㩍", a_));
		IL_15C:
		throw new XmlException(RecordTableEnumerator.b("ፅ♇⽉㑋㹍㕏ㅑ⁓㍕㱗穙⑛㍝౟䉡ၣݥཧ", a_));
		IL_17C:
		throw new ArgumentNullException(RecordTableEnumerator.b("❅㵇㹉⑋⅍≏⅑", a_));
		IL_1C5:
		return;
		IL_1FB:
		throw new XmlException();
		IL_28B:
		throw new XmlException(RecordTableEnumerator.b("ፅ♇⽉㑋㹍㕏ㅑ⁓㍕㱗穙⑛㍝౟䉡ၣݥཧ", a_));
		IL_301:
		throw new XmlException();
	}

	// Token: 0x06004FD5 RID: 20437 RVA: 0x00311288 File Offset: 0x00310288
	private List<string> \u1717(XmlReader A_0)
	{
		int a_ = 11;
		int num = 10;
		List<string> list;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 3;
					continue;
				}
				goto IL_174;
			case 1:
				if (A_0.LocalName != RecordTableEnumerator.b("⁀㙂ㅄ⽆♈㥊㹌", a_))
				{
					num = 8;
					continue;
				}
				list = new List<string>();
				A_0.Read();
				num = 11;
				continue;
			case 2:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 12;
					continue;
				}
				goto IL_88;
			case 3:
				num = 1;
				continue;
			case 4:
				list.Add(A_0.ReadElementContentAsString());
				num = 5;
				continue;
			case 5:
				goto IL_14F;
			case 6:
				goto IL_14F;
			case 7:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 14;
					continue;
				}
				goto IL_CD;
			case 8:
				goto IL_1AB;
			case 9:
				if (A_0.LocalName == RecordTableEnumerator.b("⁀㙂ㅄ⽆♈㥊", a_))
				{
					num = 4;
					continue;
				}
				goto IL_CD;
			case 11:
				goto IL_14F;
			case 12:
				goto IL_172;
			case 13:
				goto IL_60;
			case 14:
				num = 9;
				continue;
			}
			if (A_0 == null)
			{
				num = 13;
				continue;
			}
			num = 0;
			continue;
			IL_88:
			num = 7;
			continue;
			IL_CD:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_88;
			default:
				if (false)
				{
				}
				A_0.Read();
				if (true)
				{
				}
				num = 6;
				continue;
			}
			IL_14F:
			num = 2;
		}
		IL_60:
		throw new ArgumentNullException(RecordTableEnumerator.b("㍀♂⑄⍆ⱈ㥊", a_));
		IL_172:
		A_0.Read();
		return list;
		IL_174:
		throw new XmlException();
		IL_1AB:
		goto IL_174;
	}

	// Token: 0x06004FD6 RID: 20438 RVA: 0x00311464 File Offset: 0x00310464
	private void ᜀ(XmlReader A_0, Dictionary<string, XlsShape> A_1, RelationsCollection A_2, string A_3)
	{
		int a_ = 1;
		switch (0)
		{
		default:
		{
			int num = 11;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_F7;
				case 1:
					goto IL_2DE;
				case 2:
					if (A_3.Length == 0)
					{
						num = 17;
						continue;
					}
					goto IL_2B0;
				case 3:
					goto IL_1D6;
				case 4:
					goto IL_89;
				case 5:
				{
					ShapeParser shapeParser;
					XlsShape xlsShape;
					if (!shapeParser.ParseShape(A_0, xlsShape, A_2, A_3))
					{
						num = 9;
						continue;
					}
					return;
				}
				case 6:
				{
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䐶䤸伺", a_)))
					{
						num = 13;
						continue;
					}
					XlsShape xlsShape;
					int key = xlsShape.InnerSpRecord.\u1714();
					num = 3;
					continue;
				}
				case 7:
				{
					ShapeParser shapeParser;
					int key;
					if (!this.ᜋ.TryGetValue(key, out shapeParser))
					{
						num = 19;
						continue;
					}
					A_0.MoveToElement();
					MemoryStream memoryStream = new MemoryStream();
					XmlWriter xmlWriter = UtilityMethods.ᜀ(memoryStream, Encoding.UTF8);
					xmlWriter.WriteNode(A_0, false);
					xmlWriter.Flush();
					memoryStream.Position = 0L;
					A_0 = UtilityMethods.ᜀ(memoryStream);
					num = 5;
					continue;
				}
				case 8:
					if (A_1 == null)
					{
						num = 0;
						continue;
					}
					num = 14;
					continue;
				case 9:
				{
					ShapeParser shapeParser = new spr\u181F();
					MemoryStream memoryStream;
					memoryStream.Position = 0L;
					A_0 = UtilityMethods.ᜀ(memoryStream);
					XlsShape xlsShape;
					int instance = xlsShape.Instance;
					Stream xmlTypeStream = xlsShape.XmlTypeStream;
					xlsShape = new XlsShape(xlsShape.AppImplementation, xlsShape.Parent);
					xlsShape.VmlShape = true;
					xlsShape.XmlTypeStream = xmlTypeStream;
					xlsShape.ᜄ(instance);
					shapeParser.ParseShape(A_0, xlsShape, A_2, A_3);
					num = 18;
					continue;
				}
				case 10:
					goto IL_31A;
				case 12:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2B0;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 13:
				{
					int key = int.Parse(A_0.Value);
					num = 20;
					continue;
				}
				case 14:
					if (A_3 != null)
					{
						num = 12;
						continue;
					}
					goto IL_274;
				case 15:
				{
					XlsShape xlsShape;
					string key2;
					if (!A_1.TryGetValue(key2, out xlsShape))
					{
						num = 10;
						continue;
					}
					int key = -1;
					num = 6;
					continue;
				}
				case 16:
				{
					if (!A_0.MoveToAttribute(RecordTableEnumerator.b("䌶䀸䬺堼", a_)))
					{
						num = 1;
						continue;
					}
					string key2 = UtilityMethods.ᜀ(A_0.Value);
					if (true)
					{
					}
					num = 15;
					continue;
				}
				case 17:
					goto IL_1D1;
				case 18:
					goto IL_26F;
				case 19:
					goto IL_201;
				case 20:
					goto IL_1D6;
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				num = 8;
				continue;
				IL_1D6:
				num = 7;
				continue;
				IL_2B0:
				num = 16;
			}
			IL_89:
			throw new ArgumentNullException(RecordTableEnumerator.b("䔶尸娺夼娾㍀", a_));
			IL_F7:
			throw new ArgumentNullException(RecordTableEnumerator.b("匶倸堺䤼氾⥀≂㕄≆H⽊᥌⁎ɐ㭒㑔❖㱘", a_));
			IL_1D1:
			goto IL_274;
			IL_201:
			throw new XmlException();
			IL_26F:
			return;
			IL_274:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䜶堸䤺堼儾㕀ੂㅄ≆⑈ᭊⱌ㭎㥐", a_));
			IL_2DE:
			throw new XmlException();
			IL_31A:
			A_0.Skip();
			return;
		}
		}
	}

	// Token: 0x06004FD7 RID: 20439 RVA: 0x003117C8 File Offset: 0x003107C8
	private void ᜀ(XmlReader A_0, ShapeCollectionBase A_1, Dictionary<string, XlsShape> A_2, Stream A_3)
	{
		int a_ = 10;
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				int num2;
				ShapeParser shapeParser;
				XlsShape xlsShape;
				switch (num)
				{
				case 0:
					goto IL_DF;
				case 1:
					goto IL_2A7;
				case 3:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㌿㉁ぃ", a_), RecordTableEnumerator.b("㔿ぁ⩃籅㭇⥉⑋⭍㵏㍑❓筕㕗㍙㽛ⱝཟᅡୣeᱧ䝩ཫŭᵯ䡱᭳ၵṷ፹ύ᭽멿", a_)))
					{
						num = 10;
						continue;
					}
					goto IL_DF;
				case 4:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("⤿♁", a_)))
					{
						num = 5;
						continue;
					}
					goto IL_2A7;
				case 5:
				{
					string text = A_0.Value;
					num = 1;
					continue;
				}
				case 6:
				{
					string text2;
					if (text2 == null)
					{
						num = 7;
						continue;
					}
					num2 = int.Parse(text2);
					A_0.MoveToElement();
					num = 19;
					continue;
				}
				case 7:
					goto IL_32D;
				case 8:
					goto IL_139;
				case 9:
					goto IL_227;
				case 10:
				{
					string text2 = A_0.Value;
					num = 0;
					continue;
				}
				case 11:
				{
					string text;
					if (!A_2.ContainsKey(text))
					{
						num = 13;
						continue;
					}
					return;
				}
				case 12:
					shapeParser = new spr\u181F();
					this.ᜋ[num2] = shapeParser;
					num = 21;
					continue;
				case 13:
				{
					string text;
					A_2.Add(text, xlsShape);
					num = 9;
					continue;
				}
				case 14:
					if (A_3 != null)
					{
						num = 23;
						continue;
					}
					goto IL_17A;
				case 15:
				{
					if (A_2 == null)
					{
						num = 8;
						continue;
					}
					string text = null;
					string text2 = null;
					num = 4;
					continue;
				}
				case 16:
					goto IL_9C;
				case 17:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_93;
					default:
					{
						if (false)
						{
						}
						string text;
						if (text != null)
						{
							num = 20;
							continue;
						}
						goto IL_174;
					}
					}
					break;
				case 18:
					goto IL_16F;
				case 19:
					if (!this.ᜋ.TryGetValue(num2, out shapeParser))
					{
						num = 12;
						continue;
					}
					goto IL_1E6;
				case 20:
					num = 6;
					continue;
				case 21:
					goto IL_1E6;
				case 22:
					if (A_1 == null)
					{
						num = 18;
						continue;
					}
					num = 15;
					continue;
				case 23:
					A_1.ShapeLayoutStream = A_3;
					num = 24;
					continue;
				case 24:
					goto IL_17A;
				}
				goto IL_8D;
				IL_93:
				num = 16;
				continue;
				IL_8D:
				if (A_0 == null)
				{
					goto IL_93;
				}
				num = 22;
				continue;
				IL_DF:
				num = 17;
				continue;
				IL_17A:
				xlsShape = shapeParser.ParseShapeType(A_0, A_1);
				xlsShape.ᜄ(num2);
				xlsShape.VmlShape = true;
				num = 11;
				continue;
				IL_1E6:
				if (true)
				{
				}
				num = 14;
				continue;
				IL_2A7:
				num = 3;
			}
			IL_9C:
			throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁╃≅ⵇ㡉", a_));
			IL_139:
			throw new ArgumentNullException(RecordTableEnumerator.b("␿⭁❃㉅ᭇ≉ⵋ㹍㕏᭑こɕ㝗ख़㑛㽝ၟݡ", a_));
			IL_16F:
			throw new ArgumentNullException(RecordTableEnumerator.b("㌿⩁⅃⍅㱇", a_));
			IL_174:
			throw new XmlException();
			IL_227:
			return;
			IL_32D:
			goto IL_174;
		}
		}
	}

	// Token: 0x06004FD8 RID: 20440 RVA: 0x00311B08 File Offset: 0x00310B08
	internal int ᜬ(XmlReader A_0)
	{
		int a_ = 5;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_34;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_BC;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					if (A_0.LocalName != RecordTableEnumerator.b("䤺", a_))
					{
						num = 3;
						continue;
					}
					goto IL_BC;
				}
				break;
			case 3:
				goto IL_A6;
			}
			if (A_0 == null)
			{
				num = 1;
			}
			else
			{
				num = 2;
			}
		}
		IL_34:
		throw new ArgumentNullException(RecordTableEnumerator.b("䤺堼帾╀♂㝄", a_));
		IL_A6:
		throw new XmlException(RecordTableEnumerator.b("䤺堼帾╀♂㝄", a_));
		IL_BC:
		SSTDictionary sstdictionary = this.ᜉ.InnerSST;
		spr\u223A key = this.ᜂ(A_0, RecordTableEnumerator.b("䠺吼", a_));
		return sstdictionary.AddIncrease(key, false);
	}

	// Token: 0x06004FD9 RID: 20441 RVA: 0x00311BFC File Offset: 0x00310BFC
	private spr\u223A ᜂ(XmlReader A_0, string A_1)
	{
		int a_ = 0;
		int num = 16;
		spr\u223A spr_u223A;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜀ(A_0, spr_u223A);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_146;
				default:
					if (false)
					{
					}
					num = 15;
					continue;
				}
				break;
			case 1:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 14;
					continue;
				}
				goto IL_135;
			case 2:
				if (A_1.Length == 0)
				{
					num = 9;
					continue;
				}
				spr_u223A = new spr\u223A();
				num = 11;
				continue;
			case 3:
				num = 2;
				continue;
			case 4:
				if (true)
				{
				}
				if (A_0.LocalName != A_1)
				{
					num = 12;
					continue;
				}
				return spr_u223A;
			case 5:
				goto IL_146;
			case 6:
				if (A_0.LocalName == RecordTableEnumerator.b("䐵", a_))
				{
					num = 0;
					continue;
				}
				goto IL_135;
			case 7:
				if (A_1 != null)
				{
					num = 3;
					continue;
				}
				goto IL_1DF;
			case 8:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 10;
					continue;
				}
				num = 1;
				continue;
			case 9:
				goto IL_1A4;
			case 10:
				goto IL_16B;
			case 11:
				goto IL_F0;
			case 12:
				num = 8;
				continue;
			case 13:
				goto IL_68;
			case 14:
				num = 6;
				continue;
			case 15:
				goto IL_F0;
			}
			if (A_0 == null)
			{
				num = 13;
				continue;
			}
			num = 7;
			continue;
			IL_F0:
			num = 4;
			continue;
			IL_146:
			goto IL_F0;
			IL_135:
			A_0.Skip();
			num = 5;
		}
		IL_68:
		throw new ArgumentNullException(RecordTableEnumerator.b("䐵崷嬹堻嬽㈿", a_));
		IL_16B:
		return spr_u223A;
		IL_1A4:
		IL_1DF:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("唵吷唹伻圽⸿╁၃❅⽇щⵋ⍍㕏", a_));
	}

	// Token: 0x06004FDA RID: 20442 RVA: 0x00311E00 File Offset: 0x00310E00
	private void ᜀ(XmlReader A_0, spr\u223A A_1)
	{
		int a_ = 6;
		int num = 8;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 9;
					continue;
				}
				goto IL_202;
			case 1:
				A_0.Skip();
				num = 22;
				continue;
			case 2:
				goto IL_35D;
			case 3:
			{
				int num2 = this.ᜀ(A_0, null);
				A_0.Skip();
				num = 17;
				continue;
			}
			case 4:
			{
				int num2;
				int num3;
				A_1.ᜀ(num3, A_1.ᜏ().Length - 1, num2);
				num = 6;
				continue;
			}
			case 5:
				goto IL_167;
			case 6:
				return;
			case 7:
				goto IL_35D;
			case 9:
				num = 11;
				continue;
			case 10:
			{
				int num3;
				if (num3 >= 0)
				{
					num = 4;
					continue;
				}
				return;
			}
			case 11:
				if (!(A_0.LocalName != RecordTableEnumerator.b("主", a_)))
				{
					num = 1;
					continue;
				}
				goto IL_202;
			case 12:
			{
				int num3 = A_1.ᜏ().Length;
				A_0.Read();
				string text = A_0.Value;
				text = text.Replace(RecordTableEnumerator.b("ㄻ", a_), string.Empty);
				A_1.ᜁ(A_1.ᜏ() + text);
				A_0.Skip();
				num = 27;
				continue;
			}
			case 13:
				num = 18;
				continue;
			case 14:
				goto IL_147;
			case 15:
				if (A_0.LocalName == RecordTableEnumerator.b("主渽㈿", a_))
				{
					num = 3;
					continue;
				}
				num = 25;
				continue;
			case 16:
			{
				if (A_1 == null)
				{
					num = 14;
					continue;
				}
				int num2 = -1;
				int num3 = -1;
				A_0.Read();
				num = 21;
				continue;
			}
			case 17:
				goto IL_35D;
			case 18:
				if (A_0.LocalName == RecordTableEnumerator.b("䠻", a_))
				{
					num = 20;
					continue;
				}
				goto IL_35D;
			case 19:
				if (!A_0.IsEmptyElement)
				{
					num = 12;
					continue;
				}
				A_0.Skip();
				num = 5;
				continue;
			case 20:
				A_0.Skip();
				num = 2;
				continue;
			case 21:
				goto IL_35D;
			case 22:
			{
				int num2;
				if (num2 >= 0)
				{
					num = 26;
					continue;
				}
				return;
			}
			case 23:
				if (true)
				{
				}
				num = 19;
				continue;
			case 24:
				goto IL_9A;
			case 25:
				if (!(A_0.LocalName == RecordTableEnumerator.b("䠻", a_)))
				{
					A_0.Skip();
					num = 7;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_167;
				default:
					if (false)
					{
					}
					num = 23;
					continue;
				}
				break;
			case 26:
				num = 10;
				continue;
			case 27:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 13;
					continue;
				}
				goto IL_35D;
			}
			if (A_0 == null)
			{
				num = 24;
				continue;
			}
			num = 16;
			continue;
			IL_202:
			num = 15;
			continue;
			IL_35D:
			num = 0;
			continue;
			IL_167:
			goto IL_35D;
		}
		IL_9A:
		throw new ArgumentNullException(RecordTableEnumerator.b("主嬽ℿ♁⅃㑅", a_));
		IL_147:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠻嬽㠿㙁ፃ⽅㱇≉ੋ⅍≏㽑㕓≕", a_));
	}

	// Token: 0x06004FDB RID: 20443 RVA: 0x003121A8 File Offset: 0x003111A8
	private int ᜂ(XmlReader A_0, bool A_1)
	{
		int a_ = 8;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_109;
				default:
					goto IL_D1;
				}
				break;
			case 1:
			{
				int result;
				return result;
			}
			case 2:
			{
				string text;
				if (!string.IsNullOrEmpty(text))
				{
					num = 5;
					continue;
				}
				int result;
				return result;
			}
			case 3:
				if (true)
				{
				}
				break;
			case 4:
				goto IL_4B;
			case 5:
				goto IL_109;
			case 6:
			{
				if (A_0.LocalName != RecordTableEnumerator.b("䨽", a_))
				{
					num = 0;
					continue;
				}
				SSTDictionary sstdictionary = this.ᜉ.InnerSST;
				A_0.Read();
				string text = XmlConvert.DecodeName(A_0.Value);
				text = text.Replace(RecordTableEnumerator.b("㌽", a_), string.Empty);
				int result = sstdictionary.AddIncrease(text, A_1);
				A_0.Skip();
				num = 2;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			num = 6;
			continue;
			IL_109:
			A_0.Skip();
			num = 1;
		}
		IL_4B:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰽┿⍁⁃⍅㩇", a_));
		IL_D1:
		if (false)
		{
		}
		throw new XmlException(RecordTableEnumerator.b("䰽┿⍁⁃⍅㩇", a_));
	}

	// Token: 0x06004FDC RID: 20444 RVA: 0x0031230C File Offset: 0x0031130C
	private int ᜀ(XmlReader A_0, bool A_1, out string A_2)
	{
		int a_ = 7;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_5D:
			if (A_0 == null)
			{
				num = 1;
			}
			else
			{
				num = 0;
			}
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				num = 2;
				break;
			}
			break;
		}
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				if (A_0.LocalName != RecordTableEnumerator.b("䤼", a_))
				{
					num = 3;
					continue;
				}
				goto IL_C2;
			case 1:
				goto IL_69;
			case 3:
				goto IL_AC;
			}
			break;
		}
		goto IL_5D;
		IL_69:
		throw new ArgumentNullException(RecordTableEnumerator.b("似娾⁀❂⁄㕆", a_));
		IL_AC:
		throw new XmlException(RecordTableEnumerator.b("似娾⁀❂⁄㕆", a_));
		IL_C2:
		SSTDictionary sstdictionary = this.ᜉ.InnerSST;
		A_0.Read();
		string text;
		A_2 = (text = XmlConvert.DecodeName(A_0.Value));
		string text2 = text;
		text2 = text2.Replace(RecordTableEnumerator.b("〼", a_), string.Empty);
		int result = sstdictionary.AddIncrease(text2, A_1);
		A_0.Skip();
		A_0.Skip();
		return result;
	}

	// Token: 0x06004FDD RID: 20445 RVA: 0x0031243C File Offset: 0x0031143C
	private List<int> ᜀ(XmlReader A_0, List<int> A_1, List<XlsFill> A_2, List<XlsBordersCollection> A_3)
	{
		int a_ = 18;
		int num = 12;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.LocalName != RecordTableEnumerator.b("⭇⽉⁋≍͏♑ⵓ㩕㵗ə㩛ⵝ", a_))
				{
					num = 11;
					continue;
				}
				num = 8;
				continue;
			case 1:
				num = 0;
				continue;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_F6;
				default:
					goto IL_218;
				}
				break;
			case 3:
				if (A_1 == null)
				{
					num = 16;
					continue;
				}
				num = 10;
				continue;
			case 4:
				if (A_3 == null)
				{
					num = 2;
					continue;
				}
				num = 13;
				continue;
			case 5:
				goto IL_1B8;
			case 6:
				goto IL_7B;
			case 7:
			{
				spr\u192F spr_u192F = this.ᜀ(A_0, A_1, A_2, A_3, null, null);
				spr_u192F = this.ᜉ.InnerExtFormats.ᜀ(spr_u192F);
				List<int> list;
				list.Add(spr_u192F.ᜠ());
				goto IL_F6;
			}
			case 8:
			{
				if (A_0.IsEmptyElement)
				{
					num = 19;
					continue;
				}
				A_0.Read();
				List<int> list = new List<int>();
				num = 14;
				continue;
			}
			case 9:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 7;
					continue;
				}
				goto IL_145;
			case 10:
				if (A_2 == null)
				{
					num = 18;
					continue;
				}
				num = 4;
				continue;
			case 11:
				goto IL_28D;
			case 13:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 1;
					continue;
				}
				goto IL_82;
			case 14:
				goto IL_1B8;
			case 15:
			{
				List<int> list;
				return list;
			}
			case 16:
				goto IL_126;
			case 17:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 15;
					continue;
				}
				num = 9;
				continue;
			case 18:
				goto IL_BD;
			case 19:
				goto IL_2B3;
			case 20:
				goto IL_145;
			}
			if (A_0 == null)
			{
				num = 6;
				continue;
			}
			num = 3;
			continue;
			IL_F6:
			num = 20;
			continue;
			IL_145:
			A_0.Read();
			num = 5;
			continue;
			IL_1B8:
			if (true)
			{
			}
			num = 17;
		}
		IL_7B:
		throw new ArgumentNullException(RecordTableEnumerator.b("㩇⽉ⵋ⩍㕏⁑", a_));
		IL_82:
		throw new XmlException(RecordTableEnumerator.b("ᵇ⑉⥋㙍⁏㝑㝓≕㵗㹙籛♝ൟ๡䑣ብ१൩䱫", a_) + A_0.LocalName);
		IL_BD:
		throw new ArgumentNullException(RecordTableEnumerator.b("⥇㡉㹋ࡍ㥏㹑㡓╕", a_));
		IL_126:
		throw new ArgumentNullException(RecordTableEnumerator.b("⥇㡉㹋ࡍ㽏㱑⁓ὕ㙗㹙㥛♝՟ᅡ", a_));
		IL_218:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("⥇㡉㹋్㽏⁑こ㍕⩗⥙", a_));
		IL_28D:
		goto IL_82;
		IL_2B3:
		return null;
	}

	// Token: 0x06004FDE RID: 20446 RVA: 0x00312704 File Offset: 0x00311704
	private List<int> ᜀ(XmlReader A_0, List<int> A_1, List<XlsFill> A_2, List<XlsBordersCollection> A_3, List<int> A_4)
	{
		int a_ = 10;
		int num = 10;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 3;
					continue;
				}
				goto IL_2C7;
			case 1:
			{
				if (A_0.IsEmptyElement)
				{
					num = 17;
					continue;
				}
				A_0.Read();
				List<int> list = new List<int>();
				num = 12;
				continue;
			}
			case 2:
			{
				List<int> list;
				return list;
			}
			case 3:
				num = 6;
				continue;
			case 4:
				goto IL_2A8;
			case 5:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 22;
					continue;
				}
				goto IL_146;
			case 6:
				if (A_0.LocalName != RecordTableEnumerator.b("⌿❁⡃⩅၇ⱉ㽋", a_))
				{
					num = 18;
					continue;
				}
				num = 1;
				continue;
			case 7:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 2;
					continue;
				}
				num = 5;
				continue;
			case 8:
				goto IL_286;
			case 9:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_29D;
				default:
					if (false)
					{
					}
					goto IL_146;
				}
				break;
			case 11:
				goto IL_83;
			case 12:
				goto IL_1FE;
			case 13:
				goto IL_267;
			case 14:
				goto IL_D9;
			case 15:
				if (A_2 == null)
				{
					num = 13;
					continue;
				}
				num = 19;
				continue;
			case 16:
				if (A_1 == null)
				{
					num = 14;
					continue;
				}
				num = 15;
				continue;
			case 17:
				goto IL_141;
			case 18:
				goto IL_B8;
			case 19:
				if (A_3 == null)
				{
					goto IL_29D;
				}
				num = 0;
				continue;
			case 20:
				if (A_4 == null)
				{
					num = 8;
					continue;
				}
				num = 16;
				continue;
			case 21:
				goto IL_1FE;
			case 22:
			{
				spr\u192F spr_u192F = this.ᜀ(A_0, A_1, A_2, A_3, A_4, new bool?(false));
				spr_u192F = this.ᜉ.InnerExtFormats.ᜁ(spr_u192F);
				List<int> list;
				list.Add(spr_u192F.ᜠ());
				num = 9;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 11;
				continue;
			}
			num = 20;
			continue;
			IL_146:
			A_0.Read();
			if (true)
			{
			}
			num = 21;
			continue;
			IL_1FE:
			num = 7;
			continue;
			IL_29D:
			num = 4;
		}
		IL_83:
		throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁╃≅ⵇ㡉", a_));
		IL_B8:
		goto IL_2C7;
		IL_D9:
		throw new ArgumentNullException(RecordTableEnumerator.b("ℿぁ㙃ࡅⵇ㵉ੋ⅍㹏♑ᵓ㡕㱗㽙⑛㭝፟", a_));
		IL_141:
		return null;
		IL_267:
		throw new ArgumentNullException(RecordTableEnumerator.b("ℿぁ㙃Eⅇ♉⁋㵍", a_));
		IL_286:
		throw new ArgumentNullException(RecordTableEnumerator.b("⸿⍁⥃⍅ⱇ᥉㡋㝍㱏㝑ᵓ㡕㱗㽙⑛㭝፟", a_));
		IL_2A8:
		throw new ArgumentNullException(RecordTableEnumerator.b("ℿぁ㙃х❇㡉⡋⭍≏⅑", a_));
		IL_2C7:
		throw new XmlException(RecordTableEnumerator.b("ᔿⱁ⅃㹅㡇⽉⽋㩍㕏㙑瑓⹕㕗㙙籛⩝şա䑣", a_) + A_0.LocalName);
	}

	// Token: 0x06004FDF RID: 20447 RVA: 0x00312A0C File Offset: 0x00311A0C
	private void ᜂ(XmlReader A_0, List<int> A_1)
	{
		int a_ = 2;
		int num = 8;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_13A;
				default:
					if (false)
					{
					}
					num = 2;
					continue;
				}
				break;
			case 1:
				goto IL_7D;
			case 2:
				if (A_0.LocalName != RecordTableEnumerator.b("嬷弹倻刽ጿ㙁㵃⩅ⵇ㥉", a_))
				{
					num = 4;
					continue;
				}
				A_0.Read();
				this.ᜉ.InnerStyles.Clear();
				num = 16;
				continue;
			case 3:
				goto IL_13A;
			case 4:
				goto IL_24C;
			case 5:
				goto IL_FF;
			case 6:
				if (A_0.LocalName == RecordTableEnumerator.b("嬷弹倻刽ጿ㙁㵃⩅ⵇ", a_))
				{
					num = 10;
					continue;
				}
				goto IL_1BE;
			case 7:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 0;
					continue;
				}
				goto IL_162;
			case 9:
				return;
			case 10:
				this.ᜁ(A_0, A_1);
				num = 11;
				continue;
			case 11:
				goto IL_CF;
			case 12:
				num = 6;
				continue;
			case 13:
				if (A_1 == null)
				{
					num = 5;
					continue;
				}
				num = 7;
				continue;
			case 14:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 9;
					continue;
				}
				num = 15;
				continue;
			case 15:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 12;
					continue;
				}
				goto IL_CF;
			case 16:
				goto IL_13A;
			}
			if (A_0 == null)
			{
				if (true)
				{
				}
				num = 1;
				continue;
			}
			num = 13;
			continue;
			IL_CF:
			A_0.Read();
			num = 3;
			continue;
			IL_13A:
			num = 14;
		}
		IL_7D:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹崻娽┿ぁ", a_));
		IL_FF:
		throw new ArgumentNullException(RecordTableEnumerator.b("夷䠹主瀽ℿ⽁⅃≅ᭇ㹉㕋≍㕏᭑㩓㉕㵗≙㥛ⵝ", a_));
		IL_162:
		throw new XmlException(RecordTableEnumerator.b("洷吹夻䘽〿❁❃㉅ⵇ⹉汋㙍㵏㹑瑓㍕㑗㽙ㅛ㭝๟ᙡ䑣", a_) + A_0.Name);
		IL_1BE:
		throw new XmlException(RecordTableEnumerator.b("洷吹夻䘽〿❁❃㉅ⵇ⹉汋㙍㵏㹑瑓≕㥗㵙籛", a_) + A_0.LocalName);
		IL_24C:
		goto IL_162;
	}

	// Token: 0x06004FE0 RID: 20448 RVA: 0x00312C6C File Offset: 0x00311C6C
	private void ᜁ(XmlReader A_0, List<int> A_1)
	{
		int a_ = 19;
		int num = 16;
		sprᬐ sprᬐ;
		for (;;)
		{
			switch (num)
			{
			case 0:
				sprᬐ.ᜀ(A_0.Value);
				num = 5;
				continue;
			case 1:
				sprᬐ.ᜀ(XmlConvert.ToByte(A_0.Value));
				num = 10;
				continue;
			case 2:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("❈⩊⁌⩎", a_)))
				{
					num = 0;
					continue;
				}
				goto IL_222;
			case 3:
				goto IL_77;
			case 4:
				goto IL_131;
			case 5:
				if (true)
				{
				}
				goto IL_222;
			case 6:
				goto IL_2A6;
			case 7:
				goto IL_1EE;
			case 8:
				num = 9;
				continue;
			case 9:
				if (A_0.LocalName != RecordTableEnumerator.b("⩈⹊⅌⍎ɐ❒ⱔ㭖㱘", a_))
				{
					num = 6;
					continue;
				}
				sprᬐ = (sprᬐ)spr\u175E.ᜀ(TBIFFRecord.Style);
				num = 2;
				continue;
			case 10:
				goto IL_181;
			case 11:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("ㅈⵊь⭎", a_)))
				{
					num = 19;
					continue;
				}
				goto IL_131;
			case 12:
				if (A_1 == null)
				{
					num = 18;
					continue;
				}
				num = 15;
				continue;
			case 13:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⭈㹊⑌⍎═㩒㭔Ṗ㵘", a_)))
				{
					num = 17;
					continue;
				}
				goto IL_1EE;
			case 14:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⁈݊⡌㥎㑐㽒", a_)))
				{
					num = 1;
					continue;
				}
				goto IL_2DB;
			case 15:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 8;
					continue;
				}
				goto IL_2BC;
			case 16:
				IL_11:
				break;
			case 17:
			{
				int num2 = XmlConvert.ToInt32(A_0.Value);
				sprᬐ.ᜁ((byte)num2);
				sprᬐ.ᜀ(true);
				num = 7;
				continue;
			}
			case 18:
				goto IL_12F;
			case 19:
			{
				int index = XmlConvert.ToInt32(A_0.Value);
				sprᬐ.ᜀ((ushort)A_1[index]);
				num = 4;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			num = 12;
			continue;
			IL_222:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_11;
			default:
				if (false)
				{
				}
				num = 11;
				continue;
			}
			IL_131:
			num = 13;
			continue;
			IL_1EE:
			num = 14;
		}
		IL_77:
		throw new ArgumentNullException(RecordTableEnumerator.b("㭈⹊ⱌ⭎㑐⅒", a_));
		IL_12F:
		throw new ArgumentNullException(RecordTableEnumerator.b("⡈㥊㽌Ŏぐ㹒ご㍖੘⽚⑜㍞Ѡ⩢୤ͦ౨፪࡬ᱮ", a_));
		IL_181:
		goto IL_2DB;
		IL_2A6:
		IL_2BC:
		throw new XmlException(RecordTableEnumerator.b("᱈╊⡌㝎⅐㙒㙔⍖㱘㽚絜❞ౠར䕤๦ᵨ๪l佮", a_) + A_0.LocalName);
		IL_2DB:
		this.ᜉ.InnerStyles.ᜀ(sprᬐ);
	}

	// Token: 0x06004FE1 RID: 20449 RVA: 0x00312F68 File Offset: 0x00311F68
	private spr\u192F ᜀ(XmlReader A_0, List<int> A_1, List<XlsFill> A_2, List<XlsBordersCollection> A_3, List<int> A_4, bool? A_5)
	{
		int a_ = 4;
		int num = 4;
		sprỶ sprỶ;
		spr\u192F spr_u192F;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䈹娻眽␿", a_)))
				{
					num = 9;
					continue;
				}
				sprỶ.ᜇ((ushort)this.ᜉ.MaxXFCount);
				sprỶ.ᜀ(sprỶ.TXFType.XF_CELL);
				num = 2;
				continue;
			case 1:
				goto IL_22E;
			case 2:
				goto IL_167;
			case 3:
			{
				int index;
				sprỶ.ᜇ((ushort)A_4[index]);
				num = 8;
				continue;
			}
			case 5:
				goto IL_167;
			case 6:
				if (A_0.LocalName != RecordTableEnumerator.b("䈹娻", a_))
				{
					goto IL_2B4;
				}
				spr_u192F = new spr\u192F(this.ᜉ.AppImplementation, this.ᜉ);
				spr_u192F.ᜏ(true);
				sprỶ = spr_u192F.ᜑ();
				num = 0;
				continue;
			case 7:
				if (A_4 != null)
				{
					num = 3;
					continue;
				}
				goto IL_1FB;
			case 8:
				goto IL_1FB;
			case 9:
			{
				int index = (int)XmlConvert.ToUInt16(A_0.Value);
				num = 7;
				continue;
			}
			case 10:
				goto IL_143;
			case 11:
				if (A_3 == null)
				{
					num = 21;
					continue;
				}
				num = 20;
				continue;
			case 12:
				goto IL_87;
			case 13:
				goto IL_F5;
			case 14:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2B4;
				default:
					if (false)
					{
					}
					num = 6;
					continue;
				}
				break;
			case 15:
				if (A_2 == null)
				{
					num = 13;
					continue;
				}
				num = 11;
				continue;
			case 16:
				goto IL_2BF;
			case 17:
				if (A_1 == null)
				{
					num = 10;
					continue;
				}
				num = 15;
				continue;
			case 18:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("吹䤻匽ؿ⽁ぃཅⱇ", a_)))
				{
					num = 19;
					continue;
				}
				goto IL_31D;
			case 19:
				sprỶ.ᜈ(XmlConvert.ToUInt16(A_0.Value));
				num = 1;
				continue;
			case 20:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 14;
					continue;
				}
				goto IL_BA;
			case 21:
				goto IL_250;
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 12;
				continue;
			}
			num = 17;
			continue;
			IL_167:
			this.ᜀ(A_0, spr_u192F, A_1, A_2, A_3);
			num = 18;
			continue;
			IL_1FB:
			sprỶ.ᜀ(sprỶ.TXFType.XF_STYLE);
			num = 5;
			continue;
			IL_2B4:
			num = 16;
		}
		IL_87:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠹夻弽␿❁㙃", a_));
		IL_BA:
		throw new XmlException(RecordTableEnumerator.b("漹刻嬽㠿㉁⅃╅㱇⽉⡋湍⡏㽑㡓癕ⱗ㭙㭛繝", a_) + A_0.LocalName);
		IL_F5:
		throw new ArgumentNullException(RecordTableEnumerator.b("嬹主䰽ؿ⭁⡃⩅㭇", a_));
		IL_143:
		throw new ArgumentNullException(RecordTableEnumerator.b("嬹主䰽ؿⵁ⩃㉅Ň⑉⡋⭍⡏㝑❓", a_));
		IL_22E:
		goto IL_31D;
		IL_250:
		throw new ArgumentNullException(RecordTableEnumerator.b("嬹主䰽ȿⵁ㙃≅ⵇ㡉㽋", a_));
		IL_2BF:
		goto IL_BA;
		IL_31D:
		this.ᜀ(A_0, spr_u192F, A_5);
		this.ᜂ(A_0, sprỶ);
		return spr_u192F;
	}

	// Token: 0x06004FE2 RID: 20450 RVA: 0x003132A8 File Offset: 0x003122A8
	private void ᜂ(XmlReader A_0, sprỶ A_1)
	{
		int a_ = 8;
		int num = 17;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_109;
			case 1:
				goto IL_18A;
			case 2:
				goto IL_18A;
			case 3:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					if (true)
					{
					}
					num = 5;
					continue;
				}
				goto IL_D7;
			}
			case 4:
				goto IL_78;
			case 5:
				num = 8;
				continue;
			case 6:
				num = 0;
				continue;
			case 7:
				return;
			case 8:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("弽ⰿ⭁⍃⡅╇⽉≋㩍", a_)))
				{
					num = 15;
					continue;
				}
				this.ᜁ(A_0, A_1);
				num = 1;
				continue;
			}
			case 9:
				goto IL_FC;
			case 10:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 7;
					continue;
				}
				num = 11;
				continue;
			case 11:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 18;
					continue;
				}
				goto IL_18A;
			case 12:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("丽㈿ⵁぃ⍅⭇㹉╋⅍㹏", a_)))
				{
					num = 6;
					continue;
				}
				this.ᜀ(A_0, A_1);
				num = 2;
				continue;
			}
			case 13:
				goto IL_10B;
			case 14:
				if (A_1 == null)
				{
					num = 9;
					continue;
				}
				A_0.MoveToElement();
				num = 16;
				continue;
			case 15:
				num = 12;
				continue;
			case 16:
				if (!A_0.IsEmptyElement)
				{
					num = 20;
					continue;
				}
				return;
			case 18:
				num = 3;
				continue;
			case 19:
				goto IL_10B;
			case 20:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					A_0.Read();
					num = 13;
					continue;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			num = 14;
			continue;
			IL_10B:
			num = 10;
			continue;
			IL_18A:
			A_0.Read();
			num = 19;
		}
		IL_78:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰽┿⍁⁃⍅㩇", a_));
		IL_D7:
		throw new NotImplementedException(A_0.LocalName);
		IL_FC:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰽┿⅁⭃㑅ⱇ", a_));
		IL_109:
		goto IL_D7;
	}

	// Token: 0x06004FE3 RID: 20451 RVA: 0x00313518 File Offset: 0x00312518
	private void ᜁ(XmlReader A_0, sprỶ A_1)
	{
		int a_ = 16;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_280;
			case 1:
				A_1.ᜁ(XmlConvert.ToUInt16(A_0.Value));
				num = 28;
				continue;
			case 2:
				A_1.ᜌ(XmlConvert.ToBoolean(A_0.Value));
				goto IL_C3;
			case 3:
				if (A_0.LocalName != RecordTableEnumerator.b("❅⑇⍉⭋⁍㵏㝑㩓≕", a_))
				{
					num = 22;
					continue;
				}
				num = 26;
				continue;
			case 5:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⽅♇⹉⥋⁍⑏", a_)))
				{
					num = 14;
					continue;
				}
				goto IL_1F2;
			case 6:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㉅ⵇ㉉㡋ᱍ㽏♑㕓≕ㅗ㕙㉛", a_)))
				{
					num = 1;
					continue;
				}
				goto IL_148;
			case 7:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("ⱅ㵇㥉㡋❍㙏⭑ᡓ㝕⭗⹙ၛ㝝๟ݡ", a_)))
				{
					num = 30;
					continue;
				}
				goto IL_DB;
			case 8:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㑅ⵇ⭉⡋❍㹏㕑᭓⑕㱗㽙⹛", a_)))
				{
					num = 18;
					continue;
				}
				goto IL_18D;
			case 9:
			{
				string value = A_0.Value;
				A_1.ᜀ((HorizontalAlignType)Enum.Parse(typeof(XLSXHAlign), value, true));
				num = 0;
				continue;
			}
			case 10:
				A_1.ᜈ(XmlConvert.ToBoolean(A_0.Value));
				num = 15;
				continue;
			case 11:
				goto IL_AD;
			case 12:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_C3;
				default:
					if (false)
					{
					}
					goto IL_1F2;
				}
				break;
			case 13:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("ぅⵇ㡉㡋❍㍏㍑㡓", a_)))
				{
					num = 31;
					continue;
				}
				return;
			case 14:
				A_1.ᜀ(XmlConvert.ToByte(A_0.Value));
				num = 12;
				continue;
			case 15:
				goto IL_1C1;
			case 16:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("ㅅ㩇⭉㱋ᩍ㕏⩑⁓", a_)))
				{
					num = 10;
					continue;
				}
				goto IL_1C1;
			case 17:
				num = 3;
				continue;
			case 18:
				A_1.ᜋ(XmlConvert.ToUInt16(A_0.Value));
				num = 23;
				continue;
			case 19:
				goto IL_37E;
			case 20:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㕅⁇㡉╋⁍㭏ّ㭓ၕㅗ⹙", a_)))
				{
					num = 2;
					continue;
				}
				goto IL_37E;
			case 21:
				goto IL_44A;
			case 22:
				goto IL_143;
			case 23:
				goto IL_18D;
			case 24:
				goto IL_DB;
			case 25:
				if (A_1 == null)
				{
					num = 21;
					continue;
				}
				num = 29;
				continue;
			case 26:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⹅❇㡉╋㑍㽏㱑⁓㝕㑗", a_)))
				{
					num = 9;
					continue;
				}
				goto IL_280;
			case 27:
				goto IL_27B;
			case 28:
				goto IL_148;
			case 29:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 17;
					continue;
				}
				goto IL_226;
			case 30:
				A_1.ᜄ(XmlConvert.ToBoolean(A_0.Value));
				num = 24;
				continue;
			case 31:
			{
				string value2 = A_0.Value;
				A_1.ᜀ((VerticalAlignType)Enum.Parse(typeof(XLSXVAlign), value2, true));
				num = 27;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 11;
				continue;
			}
			num = 25;
			continue;
			IL_C3:
			if (true)
			{
			}
			num = 19;
			continue;
			IL_DB:
			num = 8;
			continue;
			IL_148:
			num = 16;
			continue;
			IL_18D:
			num = 20;
			continue;
			IL_1C1:
			num = 13;
			continue;
			IL_1F2:
			num = 7;
			continue;
			IL_280:
			num = 5;
			continue;
			IL_37E:
			num = 6;
		}
		IL_AD:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑅ⵇ⭉⡋⭍≏", a_));
		IL_143:
		IL_226:
		throw new XmlException();
		IL_27B:
		return;
		IL_44A:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑅ⵇ⥉⍋㱍㑏", a_));
	}

	// Token: 0x06004FE4 RID: 20452 RVA: 0x00313974 File Offset: 0x00312974
	private void ᜀ(XmlReader A_0, sprỶ A_1)
	{
		int a_ = 13;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					num = 12;
					continue;
				}
				num = 9;
				continue;
			case 1:
				num = 10;
				continue;
			case 2:
				A_1.ᜎ(XmlConvert.ToBoolean(A_0.Value));
				num = 4;
				continue;
			case 3:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⽂⩄⑆≈⹊⥌", a_)))
				{
					num = 2;
					continue;
				}
				goto IL_1DF;
			case 4:
				goto IL_16C;
			case 5:
				goto IL_D0;
			case 7:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⭂ⱄ⍆ⵈ⹊⍌", a_)))
				{
					num = 13;
					continue;
				}
				goto IL_D0;
			case 8:
				goto IL_1DA;
			case 9:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 1;
					continue;
				}
				goto IL_9B;
			case 10:
				if (A_0.LocalName != RecordTableEnumerator.b("㍂㝄⡆㵈⹊⹌㭎㡐㱒㭔", a_))
				{
					num = 8;
					continue;
				}
				num = 7;
				continue;
			case 11:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_141;
				default:
					goto IL_72;
				}
				break;
			case 12:
				goto IL_CB;
			case 13:
				goto IL_141;
			}
			if (A_0 == null)
			{
				num = 11;
				continue;
			}
			num = 0;
			continue;
			IL_D0:
			num = 3;
			continue;
			IL_141:
			A_1.ᜂ(XmlConvert.ToBoolean(A_0.Value));
			num = 5;
		}
		IL_72:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄♆ⵈ⹊㽌", a_));
		IL_9B:
		throw new XmlException(RecordTableEnumerator.b("ᙂ⭄♆⭈❊⡌潎═㱒畔㭖㙘㡚㱜⭞Ѡ䍢୤ɦ੨๪ṬᱮၰŲ౴坶Ÿᙺᅼ彾", a_));
		IL_CB:
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄⑆♈㥊⥌", a_));
		IL_16C:
		goto IL_1DF;
		IL_1DA:
		goto IL_9B;
		IL_1DF:
		if (true)
		{
		}
	}

	// Token: 0x06004FE5 RID: 20453 RVA: 0x00313B68 File Offset: 0x00312B68
	private void ᜀ(XmlReader A_0, spr\u192F A_1, bool? A_2)
	{
		int a_ = 18;
		int num = 29;
		for (;;)
		{
			switch (num)
			{
			case 0:
				A_1.ᜊ(XmlConvert.ToBoolean(A_0.Value));
				num = 12;
				continue;
			case 1:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⥇㩉㱋≍⥏ፑ㡓㽕㽗㑙ㅛ㭝๟ᙡ", a_)))
				{
					num = 35;
					continue;
				}
				num = 15;
				continue;
			case 2:
				goto IL_184;
			case 3:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⥇㩉㱋≍⥏ᑑ㭓㡕ⱗ", a_)))
				{
					num = 25;
					continue;
				}
				num = 32;
				continue;
			case 4:
				goto IL_2D2;
			case 5:
				goto IL_20E;
			case 6:
				goto IL_464;
			case 7:
				if (true)
				{
				}
				goto IL_2D2;
			case 8:
				if (A_2 != null)
				{
					num = 33;
					continue;
				}
				goto IL_369;
			case 9:
				A_1.ᜃ(XmlConvert.ToBoolean(A_0.Value));
				num = 2;
				continue;
			case 10:
				goto IL_20E;
			case 11:
				goto IL_276;
			case 12:
				goto IL_369;
			case 13:
				if (A_2 != null)
				{
					num = 16;
					continue;
				}
				goto IL_20E;
			case 14:
			{
				sprỶ sprỶ;
				sprỶ.ᜃ(A_2.Value);
				num = 38;
				continue;
			}
			case 15:
				if (A_2 != null)
				{
					num = 19;
					continue;
				}
				goto IL_2D2;
			case 16:
			{
				sprỶ sprỶ;
				sprỶ.ᜋ(A_2.Value);
				num = 5;
				continue;
			}
			case 17:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⥇㩉㱋≍⥏ᑑ㵓㩕㑗", a_)))
				{
					num = 20;
					continue;
				}
				num = 13;
				continue;
			case 18:
				goto IL_369;
			case 19:
			{
				sprỶ sprỶ;
				sprỶ.\u170D(A_2.Value);
				num = 4;
				continue;
			}
			case 20:
				A_1.\u170D(XmlConvert.ToBoolean(A_0.Value));
				num = 10;
				continue;
			case 21:
				if (!A_0.MoveToAttribute(RecordTableEnumerator.b("⥇㩉㱋≍⥏ɑ♓㥕ⱗ㽙㽛⩝य़ൡ੣", a_)))
				{
					num = 34;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_422;
				default:
					if (false)
					{
					}
					num = 28;
					continue;
				}
				break;
			case 22:
				if (A_2 != null)
				{
					num = 30;
					continue;
				}
				goto IL_184;
			case 23:
				goto IL_184;
			case 24:
			{
				sprỶ sprỶ;
				sprỶ.ᜅ(A_2.Value);
				num = 11;
				continue;
			}
			case 25:
				A_1.ᜉ(XmlConvert.ToBoolean(A_0.Value));
				num = 26;
				continue;
			case 26:
				goto IL_469;
			case 27:
			{
				if (A_1 == null)
				{
					num = 6;
					continue;
				}
				sprỶ sprỶ = A_1.ᜑ();
				num = 1;
				continue;
			}
			case 28:
				goto IL_259;
			case 30:
			{
				sprỶ sprỶ;
				sprỶ.ᜁ(A_2.Value);
				num = 23;
				continue;
			}
			case 31:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⥇㩉㱋≍⥏ၑ㭓⑕㱗㽙⹛", a_)))
				{
					num = 0;
					continue;
				}
				num = 8;
				continue;
			case 32:
				if (A_2 != null)
				{
					num = 14;
					continue;
				}
				goto IL_469;
			case 33:
			{
				sprỶ sprỶ;
				sprỶ.ᜆ(A_2.Value);
				num = 18;
				continue;
			}
			case 34:
				goto IL_422;
			case 35:
				A_1.ᜈ(XmlConvert.ToBoolean(A_0.Value));
				num = 7;
				continue;
			case 36:
				goto IL_C9;
			case 37:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⥇㩉㱋≍⥏᱑⅓㭕㩗㽙⹛ᡝཟၡॣݥᱧ", a_)))
				{
					num = 9;
					continue;
				}
				num = 22;
				continue;
			case 38:
				goto IL_469;
			}
			if (A_0 == null)
			{
				num = 36;
				continue;
			}
			num = 27;
			continue;
			IL_184:
			num = 17;
			continue;
			IL_20E:
			num = 21;
			continue;
			IL_2D2:
			num = 31;
			continue;
			IL_369:
			num = 3;
			continue;
			IL_422:
			if (A_2 != null)
			{
				num = 24;
				continue;
			}
			return;
			IL_469:
			num = 37;
		}
		IL_C9:
		throw new ArgumentNullException(RecordTableEnumerator.b("㩇⽉ⵋ⩍㕏⁑", a_));
		IL_259:
		A_1.ᜋ(XmlConvert.ToBoolean(A_0.Value));
		return;
		IL_276:
		return;
		IL_464:
		throw new ArgumentNullException(RecordTableEnumerator.b("⹇╉㹋⍍ㅏ♑", a_));
	}

	// Token: 0x06004FE6 RID: 20454 RVA: 0x00314038 File Offset: 0x00313038
	private void ᜀ(XmlReader A_0, spr\u192F A_1, List<int> A_2, List<XlsFill> A_3, List<XlsBordersCollection> A_4)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 6;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
				{
					int index = XmlConvert.ToInt32(A_0.Value);
					sprỶ sprỶ;
					sprỶ.ᜉ((ushort)A_2[index]);
					num = 1;
					continue;
				}
				case 1:
					goto IL_BE;
				case 2:
					goto IL_154;
				case 3:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("⁅ⅇ♉⁋ݍ㑏", a_)))
					{
						if (true)
						{
						}
						num = 10;
						continue;
					}
					goto IL_229;
				case 4:
					if (A_1 == null)
					{
						num = 8;
						continue;
					}
					num = 12;
					continue;
				case 5:
					num2 = A_4.Count - 1;
					num = 21;
					continue;
				case 7:
					goto IL_B9;
				case 8:
					goto IL_1A9;
				case 9:
					if (A_3 == null)
					{
						num = 13;
						continue;
					}
					num = 17;
					continue;
				case 10:
				{
					int index2 = XmlConvert.ToInt32(A_0.Value);
					XlsFill a_2 = A_3[index2];
					spr\u2306.ᜀ(a_2, A_1);
					num = 11;
					continue;
				}
				case 11:
					goto IL_229;
				case 12:
					if (A_2 == null)
					{
						num = 2;
						continue;
					}
					num = 9;
					continue;
				case 13:
					goto IL_2A3;
				case 14:
					if (num2 == A_4.Count)
					{
						num = 5;
						continue;
					}
					goto IL_25F;
				case 15:
					num2 = XmlConvert.ToInt32(A_0.Value);
					num = 14;
					continue;
				case 16:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("⑅❇㡉⡋⭍≏᭑こ", a_)))
					{
						num = 15;
						continue;
					}
					return;
				case 17:
				{
					if (A_4 == null)
					{
						num = 19;
						continue;
					}
					sprỶ sprỶ = A_1.ᜑ();
					num = 20;
					continue;
				}
				case 18:
					goto IL_27F;
				case 19:
					goto IL_210;
				case 20:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("⁅❇⑉㡋ݍ㑏", a_)))
					{
						num = 0;
						continue;
					}
					goto IL_BE;
				case 21:
					goto IL_25F;
				}
				if (A_0 != null)
				{
					num = 4;
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
					num = 7;
					continue;
				}
				IL_BE:
				num = 3;
				continue;
				IL_229:
				num = 16;
				continue;
				IL_25F:
				XlsBordersCollection a_3 = A_4[num2];
				this.ᜀ(a_3, A_1);
				num = 18;
			}
			IL_B9:
			throw new ArgumentNullException(RecordTableEnumerator.b("㑅ⵇ⭉⡋⭍≏", a_));
			IL_154:
			throw new ArgumentNullException(RecordTableEnumerator.b("❅㩇㡉ੋ⅍㹏♑ᵓ㡕㱗㽙⑛㭝፟", a_));
			IL_1A9:
			goto IL_2B9;
			IL_210:
			throw new ArgumentNullException(RecordTableEnumerator.b("❅㩇㡉๋⅍≏㙑ㅓ⑕⭗", a_));
			IL_27F:
			return;
			IL_2A3:
			throw new ArgumentNullException(RecordTableEnumerator.b("❅㩇㡉ੋ❍㱏㹑❓", a_));
			IL_2B9:
			throw new ArgumentNullException(RecordTableEnumerator.b("⍅ぇ㹉⥋⁍㑏㝑こၕ㝗⡙ㅛ㽝ᑟ", a_));
		}
		}
	}

	// Token: 0x06004FE7 RID: 20455 RVA: 0x00314368 File Offset: 0x00313368
	private void ᜀ(XlsBordersCollection A_0, spr\u192F A_1)
	{
		int a_ = 16;
		int num = 12;
		for (;;)
		{
			IBorder border;
			switch (num)
			{
			case 0:
				num = 30;
				continue;
			case 1:
				goto IL_170;
			case 2:
				goto IL_2CE;
			case 3:
				goto IL_1DE;
			case 4:
				if (spr\u2306.ᜀ(border, A_1.\u171F(), A_1.\u1736()))
				{
					num = 22;
					continue;
				}
				goto IL_2B7;
			case 5:
				num = 6;
				continue;
			case 6:
				if (spr\u2306.ᜀ(border, A_1.\u173F(), A_1.\u1738()))
				{
					num = 21;
					continue;
				}
				goto IL_239;
			case 7:
				num = 18;
				continue;
			case 8:
				num = 44;
				continue;
			case 9:
				if (A_1 == null)
				{
					num = 19;
					continue;
				}
				border = A_0[BordersLineType.EdgeLeft];
				num = 14;
				continue;
			case 10:
				goto IL_397;
			case 11:
				goto IL_310;
			case 13:
				num = 38;
				continue;
			case 14:
				if (border != null)
				{
					num = 34;
					continue;
				}
				goto IL_260;
			case 15:
				num = 36;
				continue;
			case 16:
				goto IL_5A0;
			case 17:
				if (spr\u2306.ᜀ(border, A_1.ᝅ(), A_1.ᝉ()))
				{
					num = 11;
					continue;
				}
				goto IL_260;
			case 18:
				if (!A_1.\u1719())
				{
					num = 39;
					continue;
				}
				goto IL_56B;
			case 19:
				goto IL_566;
			case 20:
				if (border == null)
				{
					return;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_46F;
				default:
					if (false)
					{
					}
					num = 7;
					continue;
				}
				break;
			case 21:
				goto IL_515;
			case 22:
				goto IL_56B;
			case 23:
				if (!A_1.\u1719())
				{
					num = 5;
					continue;
				}
				goto IL_515;
			case 24:
				if (!A_1.\u1719())
				{
					num = 0;
					continue;
				}
				goto IL_408;
			case 25:
				if (border != null)
				{
					num = 37;
					continue;
				}
				goto IL_170;
			case 26:
				goto IL_4B1;
			case 27:
				if (border != null)
				{
					num = 29;
					continue;
				}
				goto IL_2D3;
			case 28:
				goto IL_2B7;
			case 29:
				goto IL_46F;
			case 30:
				if (spr\u2306.ᜀ(border, A_1.\u1756(), A_1.ᜫ()))
				{
					num = 43;
					continue;
				}
				goto IL_2D3;
			case 31:
				goto IL_ED;
			case 32:
				if (border != null)
				{
					num = 8;
					continue;
				}
				goto IL_1DE;
			case 33:
				goto IL_2D3;
			case 34:
				num = 41;
				continue;
			case 35:
				if (border != null)
				{
					num = 47;
					continue;
				}
				goto IL_239;
			case 36:
				if (spr\u2306.ᜀ(border, A_1.\u171F(), A_1.\u173C()))
				{
					num = 26;
					continue;
				}
				goto IL_397;
			case 37:
				num = 46;
				continue;
			case 38:
				if (spr\u2306.ᜀ(border, A_1.ᜡ(), A_1.\u170D()))
				{
					num = 16;
					continue;
				}
				goto IL_1DE;
			case 39:
				num = 4;
				continue;
			case 40:
				goto IL_239;
			case 41:
				if (!A_1.\u1719())
				{
					num = 45;
					continue;
				}
				goto IL_310;
			case 42:
				goto IL_260;
			case 43:
				goto IL_408;
			case 44:
				if (!A_1.\u1719())
				{
					num = 13;
					continue;
				}
				goto IL_5A0;
			case 45:
				num = 17;
				continue;
			case 46:
				if (!A_1.\u1719())
				{
					num = 15;
					continue;
				}
				goto IL_4B1;
			case 47:
				num = 23;
				continue;
			}
			if (A_0 == null)
			{
				num = 31;
				continue;
			}
			num = 9;
			continue;
			IL_170:
			border = A_0[BordersLineType.DiagonalUp];
			num = 20;
			continue;
			IL_1DE:
			border = A_0[BordersLineType.DiagonalDown];
			num = 25;
			continue;
			IL_239:
			border = A_0[BordersLineType.EdgeBottom];
			num = 32;
			continue;
			IL_260:
			border = A_0[BordersLineType.EdgeRight];
			num = 27;
			continue;
			IL_2B7:
			A_1.ᜀ(border.ShowDiagonalLine);
			num = 2;
			continue;
			IL_2D3:
			border = A_0[BordersLineType.EdgeTop];
			num = 35;
			continue;
			IL_310:
			A_1.ᜊ(true);
			A_1.ᝅ().ᜀ(border.OColor, true);
			A_1.ᜀ(border.LineStyle);
			num = 42;
			continue;
			IL_397:
			A_1.ᜄ(border.ShowDiagonalLine);
			num = 1;
			continue;
			IL_408:
			A_1.ᜊ(true);
			A_1.\u1756().ᜀ(border.OColor, true);
			A_1.ᜂ(border.LineStyle);
			num = 33;
			continue;
			IL_46F:
			if (true)
			{
			}
			num = 24;
			continue;
			IL_4B1:
			A_1.ᜊ(true);
			A_1.\u171F().ᜀ(border.OColor, true);
			A_1.ᜁ(border.LineStyle);
			num = 10;
			continue;
			IL_515:
			A_1.ᜊ(true);
			A_1.\u173F().ᜀ(border.OColor, true);
			A_1.ᜄ(border.LineStyle);
			num = 40;
			continue;
			IL_56B:
			A_1.ᜊ(true);
			A_1.\u171F().ᜀ(border.OColor, true);
			A_1.ᜃ(border.LineStyle);
			num = 28;
			continue;
			IL_5A0:
			A_1.ᜊ(true);
			A_1.ᜡ().ᜀ(border.OColor, true);
			A_1.ᜅ(border.LineStyle);
			num = 3;
		}
		IL_ED:
		throw new ArgumentNullException(RecordTableEnumerator.b("⑅❇㡉⡋⭍≏⅑", a_));
		IL_2CE:
		return;
		IL_566:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑅ⵇ⥉⍋㱍㑏", a_));
	}

	// Token: 0x06004FE8 RID: 20456 RVA: 0x0031494C File Offset: 0x0031394C
	private static bool ᜀ(IBorder A_0, OColor A_1, LineStyleType A_2)
	{
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return true;
			default:
			{
				if (false)
				{
				}
				OColor ocolor = A_0.OColor;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						num = 3;
						continue;
					case 1:
						if (ocolor.ColorType == A_1.ColorType)
						{
							num = 0;
							continue;
						}
						return true;
					case 2:
						goto IL_9B;
					case 3:
						if (ocolor.Value == A_1.Value)
						{
							num = 2;
							continue;
						}
						return true;
					}
					break;
				}
				break;
			}
			}
		}
		IL_9B:
		return A_0.LineStyle != A_2;
	}

	// Token: 0x06004FE9 RID: 20457 RVA: 0x003149F8 File Offset: 0x003139F8
	private static void ᜂ(XmlReader A_0, RelationsCollection A_1)
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			int num = 4;
			string a_2;
			string a_3;
			string a_4;
			bool a_5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					a_2 = A_0.Value;
					num = 3;
					continue;
				case 1:
					goto IL_A7;
				case 2:
					if (true)
					{
					}
					goto IL_1C2;
				case 3:
					goto IL_105;
				case 5:
					if (A_1 == null)
					{
						num = 13;
						continue;
					}
					a_3 = null;
					a_4 = null;
					a_2 = null;
					a_5 = false;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C2;
					default:
						if (false)
						{
						}
						num = 12;
						continue;
					}
					break;
				case 6:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("ᅄ㹆㥈⹊", a_)))
					{
						num = 2;
						continue;
					}
					goto IL_7A;
				case 7:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("ᅄ♆㭈ⱊ⡌㭎", a_)))
					{
						num = 0;
						continue;
					}
					goto IL_105;
				case 8:
					a_3 = A_0.Value;
					num = 1;
					continue;
				case 9:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("ᅄ♆㭈ⱊ⡌㭎᱐㱒ㅔ㉖", a_)))
					{
						num = 14;
						continue;
					}
					goto IL_234;
				case 10:
					goto IL_7A;
				case 11:
					goto IL_75;
				case 12:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("ౄ⍆", a_)))
					{
						num = 8;
						continue;
					}
					goto IL_A7;
				case 13:
					goto IL_100;
				case 14:
					a_5 = (A_0.Value == RecordTableEnumerator.b("D㽆㵈⹊㽌ⅎぐ㽒", a_));
					num = 15;
					continue;
				case 15:
					goto IL_191;
				}
				if (A_0 == null)
				{
					num = 11;
					continue;
				}
				num = 5;
				continue;
				IL_7A:
				num = 7;
				continue;
				IL_A7:
				num = 6;
				continue;
				IL_105:
				num = 9;
				continue;
				IL_1C2:
				a_4 = A_0.Value;
				num = 10;
			}
			IL_75:
			throw new ArgumentNullException(RecordTableEnumerator.b("㝄≆⡈⽊⡌㵎", a_));
			IL_100:
			throw new ArgumentNullException(RecordTableEnumerator.b("㝄≆╈⩊㥌♎㹐㵒♔", a_));
			IL_191:
			IL_234:
			sprᦨ a_6 = new sprᦨ(a_2, a_4, a_5);
			A_1[a_3] = a_6;
			return;
		}
		}
	}

	// Token: 0x06004FEA RID: 20458 RVA: 0x00314C4C File Offset: 0x00313C4C
	private void ᜁ(XmlReader A_0, RelationsCollection A_1, sprវ A_2, string A_3)
	{
		int a_ = 8;
		int num = 8;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 13;
					continue;
				}
				num = 7;
				continue;
			case 1:
				if (A_0.LocalName == RecordTableEnumerator.b("䴽⠿❁⅃㉅", a_))
				{
					num = 14;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_E9;
				default:
					goto IL_190;
				}
				break;
			case 2:
				goto IL_60;
			case 3:
				goto IL_11F;
			case 4:
				goto IL_E9;
			case 5:
				if (A_1 == null)
				{
					num = 12;
					continue;
				}
				num = 10;
				continue;
			case 6:
				goto IL_11F;
			case 7:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 4;
					continue;
				}
				if (true)
				{
				}
				A_0.Read();
				num = 6;
				continue;
			case 9:
				goto IL_11F;
			case 10:
				if (A_0.LocalName != RecordTableEnumerator.b("䴽⠿❁⅃㉅㭇", a_))
				{
					num = 11;
					continue;
				}
				this.ᜉ.Objects.Clear();
				this.ᜉ.InnerWorksheets.Clear();
				this.ᜉ.InnerCharts.Clear();
				A_0.Read();
				num = 3;
				continue;
			case 11:
				goto IL_237;
			case 12:
				goto IL_E7;
			case 13:
				goto IL_142;
			case 14:
				this.ᜀ(A_0, A_1, A_2, A_3);
				num = 9;
				continue;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			num = 5;
			continue;
			IL_E9:
			num = 1;
			continue;
			IL_11F:
			num = 0;
		}
		IL_60:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰽┿⍁⁃⍅㩇", a_));
		IL_E7:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰽┿⹁╃㉅ⅇ╉≋㵍", a_));
		IL_142:
		A_0.Read();
		return;
		IL_190:
		if (false)
		{
		}
		throw new XmlException(RecordTableEnumerator.b("欽⸿⥁⩃⥅㽇⑉汋㩍ㅏ㕑瑓", a_) + A_0.Value);
		IL_237:
		throw new XmlException(RecordTableEnumerator.b("欽⸿❁㱃㙅ⵇ⥉㡋⭍㑏牑⁓㝕㽗穙㉛㽝ൟݡ䑣", a_) + A_0.Name);
	}

	// Token: 0x06004FEB RID: 20459 RVA: 0x00314E9C File Offset: 0x00313E9C
	private void ᜀ(XmlReader A_0, RelationsCollection A_1, sprវ A_2, string A_3)
	{
		int a_ = 17;
		switch (0)
		{
		default:
		{
			int num = 22;
			sprᦨ sprᦨ;
			XlsWorksheetBase xlsWorksheetBase;
			string text;
			string a_2;
			string a_3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					if (sprᦨ == null)
					{
						num = 9;
						continue;
					}
					xlsWorksheetBase = null;
					num = 19;
					continue;
				case 1:
					num = 10;
					continue;
				case 2:
					text = A_0.Value;
					num = 16;
					continue;
				case 3:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㑆ⅈ⹊⡌㭎ᡐ㝒", a_)))
					{
						num = 23;
						continue;
					}
					goto IL_39C;
				case 4:
					goto IL_39C;
				case 5:
					goto IL_269;
				case 6:
				{
					string text2 = A_0.Value;
					num = 17;
					continue;
				}
				case 7:
				{
					if (A_1 == null)
					{
						num = 13;
						continue;
					}
					string text2 = null;
					a_2 = null;
					text = null;
					a_3 = null;
					num = 26;
					continue;
				}
				case 8:
					goto IL_1CD;
				case 9:
					goto IL_3CC;
				case 10:
				{
					string a;
					if (a == RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒♔㑖ㅘ㹚ぜ㹞በ䵢੤ᝦ౨ժᕬɮᵰᕲᩴնᑸ᩺ॼ౾꾀Ꚉ펖쒠춢톤袦鮨鮪鶬馮麰솲킴\udbb6\ud8b8쾺풼킾꿀냂귄껆마룊룎뻐ꇒ뻔ꓖ뇘뻚룜ꯞ", a_))
					{
						string text2;
						xlsWorksheetBase = (XlsWorksheetBase)this.ᜉ.InnerWorksheets.Add(text2);
						num = 11;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2ED;
					default:
						if (false)
						{
						}
						num = 15;
						continue;
					}
					break;
				}
				case 11:
					goto IL_244;
				case 12:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("⹆ⵈ", a_), RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒♔㑖ㅘ㹚ぜ㹞በ䵢੤ᝦ౨ժᕬɮᵰᕲᩴնᑸ᩺ॼ౾꾀Ꚉ펖쒠춢톤袦鮨鮪鶬馮麰솲킴\udbb6\ud8b8쾺풼킾꿀냂귄껆마룊", a_)))
					{
						num = 2;
						continue;
					}
					goto IL_1D2;
				case 13:
					goto IL_31C;
				case 14:
					a_2 = A_0.Value;
					num = 5;
					continue;
				case 15:
					num = 25;
					continue;
				case 16:
					goto IL_1D2;
				case 17:
					goto IL_2C8;
				case 18:
					num = 24;
					continue;
				case 19:
				{
					string a;
					if ((a = sprᦨ.ᜃ()) != null)
					{
						num = 1;
						continue;
					}
					goto IL_249;
				}
				case 20:
					goto IL_A7;
				case 21:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㑆㵈⩊㥌⩎", a_)))
					{
						goto IL_2ED;
					}
					goto IL_269;
				case 23:
					a_3 = A_0.Value;
					num = 4;
					continue;
				case 24:
					goto IL_161;
				case 25:
				{
					string a;
					if (!(a == RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒♔㑖ㅘ㹚ぜ㹞በ䵢੤ᝦ౨ժᕬɮᵰᕲᩴնᑸ᩺ॼ౾꾀Ꚉ펖쒠춢톤袦鮨鮪鶬馮麰솲킴\udbb6\ud8b8쾺풼킾꿀냂귄껆마룊곎말닒꟔ꏖ꫘돚룜뫞闠", a_)))
					{
						num = 18;
						continue;
					}
					string text2;
					xlsWorksheetBase = (XlsWorksheetBase)this.ᜉ.InnerCharts.Add(text2);
					num = 8;
					continue;
				}
				case 26:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("⥆⡈♊⡌", a_)))
					{
						num = 6;
						continue;
					}
					goto IL_2C8;
				}
				if (A_0 == null)
				{
					num = 20;
					continue;
				}
				num = 7;
				continue;
				IL_1D2:
				num = 3;
				continue;
				IL_269:
				num = 12;
				continue;
				IL_2C8:
				num = 21;
				continue;
				IL_2ED:
				num = 14;
				continue;
				IL_39C:
				sprᦨ = A_1[text];
				num = 0;
			}
			IL_A7:
			throw new ArgumentNullException(RecordTableEnumerator.b("㕆ⱈ⩊⥌⩎⍐", a_));
			IL_161:
			goto IL_249;
			IL_1CD:
			IL_244:
			goto IL_3CE;
			IL_249:
			throw new XmlException(RecordTableEnumerator.b("ቆ❈⁊⍌⁎♐㵒畔❖㡘⥚⥜罞ᕠᩢᕤɦ卨䭪", a_) + sprᦨ.ᜃ());
			IL_31C:
			throw new ArgumentNullException(RecordTableEnumerator.b("㕆ⱈ❊ⱌ㭎㡐㱒㭔⑖", a_));
			IL_3CC:
			throw new XmlException(RecordTableEnumerator.b("ᕆⱈ❊ⱌ㭎㡐㱒㭔睖㡘⽚⥜ⵞࡠŢၤ፦౨䭪lٮɰrၴ፶", a_));
			IL_3CE:
			xlsWorksheetBase.DataHolder = new sprᡟ(A_2, sprᦨ, A_3);
			xlsWorksheetBase.ᜠ.ᜂ(text);
			xlsWorksheetBase.ᜠ.ᜅ(a_3);
			xlsWorksheetBase.IsSaved = true;
			this.ᜀ(xlsWorksheetBase, a_2);
			A_1.Remove(text);
			return;
		}
		}
	}

	// Token: 0x06004FEC RID: 20460 RVA: 0x003152BC File Offset: 0x003142BC
	private void ᜀ(XlsWorksheetBase A_0, string A_1)
	{
		int a_ = 12;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_C5;
			case 1:
				if (!(A_1 == RecordTableEnumerator.b("⩁ⵃ≅ⱇ⽉≋", a_)))
				{
					goto IL_143;
				}
				goto IL_52;
			case 2:
				num = 4;
				continue;
			case 3:
				num = 0;
				continue;
			case 4:
				if (!(A_1 == RecordTableEnumerator.b("㑁⅃㑅ㅇɉ╋⩍㑏㝑㩓", a_)))
				{
					num = 7;
					continue;
				}
				goto IL_B1;
			case 6:
				return;
			case 7:
				num = 9;
				continue;
			case 8:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_143;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				break;
			case 9:
				if (!(A_1 == RecordTableEnumerator.b("㑁ⵃ㕅ⅇ⡉⁋⭍", a_)))
				{
					num = 3;
					continue;
				}
				goto IL_CA;
			case 10:
				if (A_1 != null)
				{
					num = 8;
					continue;
				}
				goto IL_150;
			}
			if (A_1 == null)
			{
				num = 6;
				continue;
			}
			num = 10;
			continue;
			IL_143:
			num = 2;
		}
		return;
		IL_52:
		if (true)
		{
		}
		A_0.Visibility = WorksheetVisibility.Hidden;
		return;
		IL_B1:
		A_0.Visibility = WorksheetVisibility.StrongHidden;
		return;
		IL_C5:
		goto IL_150;
		IL_CA:
		A_0.Visibility = WorksheetVisibility.Visible;
		return;
		IL_150:
		throw new ArgumentException(RecordTableEnumerator.b("ᝁ⩃ⵅ♇╉㭋⁍灏⑑㵓╕ㅗ㡙㕛㉝य़ᙡᵣ䙥᭧ṩ൫ᩭᕯ剱sཱུࡷό", a_));
	}

	// Token: 0x06004FED RID: 20461 RVA: 0x0031542C File Offset: 0x0031442C
	private void ᜀ(XmlReader A_0, IDictionary<string, string> A_1, string A_2, string A_3)
	{
		int a_ = 2;
		string text;
		string text2;
		for (;;)
		{
			text = null;
			text2 = null;
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_C8;
				case 1:
					goto IL_F2;
				case 2:
					text2 = A_0.Value;
					num = 1;
					continue;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_45;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 4:
					text = A_0.Value;
					num = 0;
					continue;
				case 5:
					if (text2 == null)
					{
						num = 8;
						continue;
					}
					goto IL_11A;
				case 6:
					if (text != null)
					{
						num = 3;
						continue;
					}
					goto IL_B4;
				case 7:
					goto IL_45;
				case 8:
					goto IL_8A;
				case 9:
					if (true)
					{
					}
					if (A_0.MoveToAttribute(A_3))
					{
						num = 2;
						continue;
					}
					goto IL_F2;
				}
				break;
				IL_45:
				if (A_0.MoveToAttribute(A_2))
				{
					num = 4;
					continue;
				}
				IL_C8:
				num = 9;
				continue;
				IL_F2:
				num = 6;
			}
		}
		IL_8A:
		IL_B4:
		throw new spr\u23EE(RecordTableEnumerator.b("洷吹崻尽ⰿ❁摃㉅❇橉㱋⽍≏⅑ㅓ癕㱗㍙㽛⩝य़ൡ੣ݥᩧ፩䱫୭ṯٱٳཱུ塷፹ࡻ᭽ꊁ겋춍ﾏﲑ벛\ud99f튡솣", a_));
		IL_11A:
		A_1.Add(text, text2);
	}

	// Token: 0x06004FEE RID: 20462 RVA: 0x0031555C File Offset: 0x0031455C
	private void ᜃ(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 3;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 3;
				continue;
			case 1:
				goto IL_7C;
			case 2:
				if (A_1 == null)
				{
					num = 6;
					continue;
				}
				num = 5;
				continue;
			case 3:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䬸帺嬼", a_)))
				{
					goto IL_74;
				}
				goto IL_F3;
			case 5:
				if (A_0.LocalName == RecordTableEnumerator.b("吸帺似堾⑀B⁄⭆╈", a_))
				{
					num = 0;
					continue;
				}
				return;
			case 6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_74;
				default:
					goto IL_E3;
				}
				break;
			case 7:
				goto IL_44;
			}
			if (A_0 == null)
			{
				num = 7;
				continue;
			}
			num = 2;
			continue;
			IL_74:
			num = 1;
		}
		IL_44:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬸帺尼嬾⑀ㅂ", a_));
		IL_7C:
		string value = A_0.Value;
		A_1.AllocatedRange[value].Merge();
		A_0.Skip();
		return;
		IL_E3:
		if (false)
		{
		}
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䨸区堼娾㕀", a_));
		IL_F3:
		throw new InvalidDataException();
	}

	// Token: 0x06004FEF RID: 20463 RVA: 0x003156AC File Offset: 0x003146AC
	private string \u1716(XmlReader A_0)
	{
		int a_ = 4;
		switch (0)
		{
		default:
		{
			int num = 12;
			for (;;)
			{
				XlsWorksheet xlsWorksheet;
				bool flag;
				string text;
				int num2;
				bool flag2;
				bool flag3;
				string name;
				XlsWorksheet xlsWorksheet2;
				bool flag4;
				switch (num)
				{
				case 0:
					goto IL_124;
				case 1:
					num = 3;
					continue;
				case 2:
					goto IL_31F;
				case 3:
					flag = (xlsWorksheet != null);
					goto IL_3F8;
				case 4:
					return text;
				case 5:
				{
					XlsName xlsName;
					xlsName.Record.ᜀ((ushort)(num2 + 1));
					num = 18;
					continue;
				}
				case 6:
					goto IL_34B;
				case 7:
					flag = true;
					goto IL_3F8;
				case 8:
				{
					this.ᜉ.HasApostrophe = text.Contains(RecordTableEnumerator.b("ᴹ", a_));
					INamedRange namedRange;
					XlsName xlsName = (XlsName)namedRange;
					xlsName.ᜀ(this.ᜊ.ᜃ(text));
					xlsName.Visible = !flag2;
					num = 29;
					continue;
				}
				case 9:
					flag3 = true;
					num2 = int.Parse(A_0.Value);
					num = 14;
					continue;
				case 10:
					if (flag3)
					{
						num = 7;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_34B;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 11:
				{
					INamedRange namedRange = xlsWorksheet.Names.Add(name);
					num = 21;
					continue;
				}
				case 13:
					num = 6;
					continue;
				case 14:
					goto IL_16D;
				case 15:
					if (A_0.LocalName == RecordTableEnumerator.b("帹夻堽⤿ⱁ⅃≅ه⭉⅋⭍", a_))
					{
						num = 17;
						continue;
					}
					return text;
				case 16:
					xlsWorksheet2 = (this.ᜉ.Objects[num2] as XlsWorksheet);
					goto IL_2A8;
				case 17:
					num = 20;
					continue;
				case 18:
					goto IL_20E;
				case 19:
					flag2 = XmlConvert.ToBoolean(A_0.Value);
					num = 2;
					continue;
				case 20:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("吹崻匽┿", a_)))
					{
						num = 27;
						continue;
					}
					goto IL_1DC;
				case 21:
					goto IL_124;
				case 22:
					if (!flag3)
					{
						num = 28;
						continue;
					}
					num = 16;
					continue;
				case 23:
				{
					if (flag4)
					{
						num = 11;
						continue;
					}
					INamedRange namedRange = this.ᜉ.Names.Add(name);
					num = 0;
					continue;
				}
				case 24:
					xlsWorksheet2 = null;
					goto IL_2A8;
				case 25:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("刹唻娽␿❁⩃", a_)))
					{
						num = 19;
						continue;
					}
					goto IL_31F;
				case 26:
					goto IL_B6;
				case 27:
					name = A_0.Value;
					num = 13;
					continue;
				case 28:
					num = 24;
					continue;
				case 29:
					if (flag3)
					{
						num = 5;
						continue;
					}
					goto IL_20E;
				}
				if (A_0 == null)
				{
					num = 26;
					continue;
				}
				name = null;
				text = null;
				flag3 = false;
				num2 = -1;
				flag2 = false;
				num = 15;
				continue;
				IL_124:
				if (true)
				{
				}
				A_0.Read();
				text = A_0.Value;
				num = 8;
				continue;
				IL_16D:
				num = 25;
				continue;
				IL_34B:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("嘹医崽ℿ⹁ᝃ⹅ⵇ⽉㡋ݍ㑏", a_)))
				{
					num = 9;
					continue;
				}
				goto IL_16D;
				IL_20E:
				A_0.Skip();
				num = 4;
				continue;
				IL_2A8:
				xlsWorksheet = xlsWorksheet2;
				num = 10;
				continue;
				IL_31F:
				num = 22;
				continue;
				IL_3F8:
				flag4 = flag;
				num = 23;
			}
			IL_B6:
			throw new ArgumentNullException(RecordTableEnumerator.b("䠹夻弽␿❁㙃", a_));
			IL_1DC:
			throw new ApplicationException(RecordTableEnumerator.b("礹崻倽⸿ⵁぃ晅⹇⍉≋⩍灏㱑㕓㭕㵗穙㩛ㅝ቟䉡੣ݥէཀྵ࡫乭ɯ፱ᩳᅵᵷ", a_));
		}
		}
	}

	// Token: 0x06004FF0 RID: 20464 RVA: 0x00315AD8 File Offset: 0x00314AD8
	private List<int> \u1715(XmlReader A_0)
	{
		int a_ = 6;
		int num = 2;
		for (;;)
		{
			List<int> list;
			switch (num)
			{
			case 0:
				goto IL_58;
			case 1:
				goto IL_9F;
			case 3:
				this.ᜀ(A_0, list);
				num = 8;
				continue;
			case 4:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 3;
					continue;
				}
				goto IL_F1;
			case 5:
				if (!(A_0.LocalName == RecordTableEnumerator.b("娻儽⸿㙁㝃", a_)))
				{
					return list;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_9F;
				default:
					if (false)
					{
					}
					num = 11;
					continue;
				}
				break;
			case 6:
				return list;
			case 7:
				goto IL_B8;
			case 8:
				goto IL_F1;
			case 9:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 6;
					continue;
				}
				num = 4;
				continue;
			case 10:
				goto IL_B8;
			case 11:
				if (true)
				{
				}
				A_0.Read();
				num = 7;
				continue;
			case 12:
				num = 5;
				continue;
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			list = new List<int>();
			num = 1;
			continue;
			IL_9F:
			if (A_0.NodeType == XmlNodeType.Element)
			{
				num = 12;
				continue;
			}
			return list;
			IL_B8:
			num = 9;
			continue;
			IL_F1:
			A_0.Read();
			num = 10;
		}
		IL_58:
		throw new ArgumentNullException(RecordTableEnumerator.b("主嬽ℿ♁⅃㑅", a_));
	}

	// Token: 0x06004FF1 RID: 20465 RVA: 0x00315C60 File Offset: 0x00314C60
	private int ᜀ(XmlReader A_0, List<int> A_1)
	{
		int a_ = 3;
		int num = 5;
		XlsFont xlsFont;
		for (;;)
		{
			switch (num)
			{
			case 0:
				A_1.Add(xlsFont.Index);
				num = 6;
				continue;
			case 1:
				goto IL_51;
			case 2:
				xlsFont.OColor.SetTheme(this.ᜎ.Value, this.ᜉ);
				num = 4;
				continue;
			case 3:
				if (A_1 != null)
				{
					num = 0;
					continue;
				}
				goto IL_149;
			case 4:
				goto IL_53;
			case 6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3F;
				default:
					goto IL_F4;
				}
				break;
			case 7:
				if (this.ᜎ != null)
				{
					num = 2;
					continue;
				}
				goto IL_53;
			}
			goto IL_39;
			IL_3F:
			num = 1;
			continue;
			IL_39:
			if (A_0 == null)
			{
				goto IL_3F;
			}
			xlsFont = (XlsFont)this.ᜉ.CreateFont(null, false);
			A_0.Read();
			num = 7;
			continue;
			IL_53:
			this.ᜀ(A_0, xlsFont);
			xlsFont = (XlsFont)this.ᜉ.InnerFonts.Add(xlsFont);
			num = 3;
		}
		IL_51:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬸帺尼嬾⑀ㅂ", a_));
		IL_F4:
		if (false)
		{
		}
		if (true)
		{
		}
		IL_149:
		return xlsFont.Index;
	}

	// Token: 0x06004FF2 RID: 20466 RVA: 0x00315DBC File Offset: 0x00314DBC
	private XlsFont \u1714(XmlReader A_0)
	{
		int a_ = 4;
		if (A_0 == null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_48;
			}
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䠹夻弽␿❁㙃", a_));
		}
		IL_48:
		if (true)
		{
		}
		XlsFont xlsFont = new XlsFont(this.ᜉ.AppImplementation, this.ᜉ);
		A_0.Read();
		this.ᜀ(A_0, xlsFont);
		return xlsFont;
	}

	// Token: 0x06004FF3 RID: 20467 RVA: 0x00315E40 File Offset: 0x00314E40
	private void ᜀ(XmlReader A_0, XlsFont A_1)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			int num = 13;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_270;
				case 2:
					num = 7;
					continue;
				case 3:
					goto IL_270;
				case 4:
				{
					int num2;
					switch (num2)
					{
					case 0:
						A_1.IsBold = this.ᜀ(A_0, RecordTableEnumerator.b("㑁╃⩅", a_), true);
						num = 12;
						continue;
					case 1:
						A_1.IsItalic = this.ᜀ(A_0, RecordTableEnumerator.b("㑁╃⩅", a_), true);
						num = 24;
						continue;
					case 2:
					case 3:
						A_1.FontName = this.ᜁ(A_0, RecordTableEnumerator.b("㑁╃⩅", a_));
						num = 14;
						continue;
					case 4:
					{
						string s = this.ᜁ(A_0, RecordTableEnumerator.b("㑁╃⩅", a_));
						A_1.Size = double.Parse(s, CultureInfo.InvariantCulture);
						num = 27;
						continue;
					}
					case 5:
						A_1.IsStrikethrough = this.ᜀ(A_0, RecordTableEnumerator.b("㑁╃⩅", a_), true);
						num = 18;
						continue;
					case 6:
					{
						string text = this.ᜁ(A_0, RecordTableEnumerator.b("㑁╃⩅", a_));
						num = 26;
						continue;
					}
					case 7:
					{
						string value = this.ᜁ(A_0, RecordTableEnumerator.b("㑁╃⩅", a_));
						A_1.VerticalAlignment = (FontVertialAlignmentType)Enum.Parse(typeof(FontVertialAlignmentType), value, true);
						num = 5;
						continue;
					}
					case 8:
						A_1.MacOSShadow = this.ᜀ(A_0, RecordTableEnumerator.b("㑁╃⩅", a_), true);
						num = 3;
						continue;
					case 9:
						A_1.OColor.ᜀ(this.ᜏ(A_0), true);
						num = 22;
						continue;
					case 10:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							A_1.CharSet = this.\u1712(A_0);
							num = 21;
							continue;
						}
						break;
					case 11:
						break;
					default:
						num = 16;
						continue;
					}
					A_1.Family = this.\u1713(A_0);
					num = 15;
					continue;
				}
				case 5:
					goto IL_270;
				case 6:
					num = 4;
					continue;
				case 7:
					if (spr\u22D2.ឃ == null)
					{
						num = 19;
						continue;
					}
					goto IL_12E;
				case 8:
				{
					int num2;
					string localName;
					if (spr\u22D2.ឃ.TryGetValue(localName, out num2))
					{
						num = 6;
						continue;
					}
					goto IL_270;
				}
				case 9:
					goto IL_270;
				case 10:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 11;
						continue;
					}
					num = 23;
					continue;
				case 11:
					return;
				case 12:
					goto IL_270;
				case 14:
					goto IL_270;
				case 15:
					if (true)
					{
					}
					goto IL_270;
				case 16:
					num = 0;
					continue;
				case 17:
					goto IL_12E;
				case 18:
					goto IL_270;
				case 19:
					spr\u22D2.ឃ = new Dictionary<string, int>(12)
					{
						{
							RecordTableEnumerator.b("⁁", a_),
							0
						},
						{
							RecordTableEnumerator.b("⭁", a_),
							1
						},
						{
							RecordTableEnumerator.b("ⱁ╃⭅ⵇ", a_),
							2
						},
						{
							RecordTableEnumerator.b("ぁɃ⥅♇㹉", a_),
							3
						},
						{
							RecordTableEnumerator.b("ㅁ㹃", a_),
							4
						},
						{
							RecordTableEnumerator.b("ㅁぃ㑅ⅇⅉ⥋", a_),
							5
						},
						{
							RecordTableEnumerator.b("㝁", a_),
							6
						},
						{
							RecordTableEnumerator.b("㑁⅃㑅㱇୉⁋❍㝏㱑", a_),
							7
						},
						{
							RecordTableEnumerator.b("ㅁⱃ❅ⱇ╉㭋", a_),
							8
						},
						{
							RecordTableEnumerator.b("⅁⭃⩅❇㡉", a_),
							9
						},
						{
							RecordTableEnumerator.b("⅁ⱃ❅㩇㥉⥋㩍", a_),
							10
						},
						{
							RecordTableEnumerator.b("⑁╃⭅ⅇ♉㕋", a_),
							11
						}
					};
					num = 17;
					continue;
				case 20:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 2;
						continue;
					}
					goto IL_270;
				}
				case 21:
					goto IL_270;
				case 22:
					goto IL_270;
				case 23:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 25;
						continue;
					}
					break;
				case 24:
					goto IL_270;
				case 25:
					num = 20;
					continue;
				case 26:
				{
					string text;
					A_1.Underline = ((text != null) ? ((FontUnderlineType)Enum.Parse(typeof(FontUnderlineType), text, true)) : (A_1.Underline = FontUnderlineType.Single));
					num = 9;
					continue;
				}
				case 27:
					goto IL_270;
				}
				goto IL_99;
				IL_12E:
				num = 8;
				continue;
				IL_270:
				A_0.Read();
				num = 1;
				continue;
				IL_51D:
				num = 10;
				continue;
				IL_99:
				goto IL_51D;
			}
			return;
		}
		}
	}

	// Token: 0x06004FF4 RID: 20468 RVA: 0x003163A8 File Offset: 0x003153A8
	private byte \u1713(XmlReader A_0)
	{
		int a_ = 11;
		byte result;
		for (;;)
		{
			IL_43:
			if (true)
			{
			}
			result = 0;
			int num = 1;
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
						return result;
					case 1:
						if (A_0.MoveToAttribute(RecordTableEnumerator.b("㝀≂⥄", a_)))
						{
							num = 2;
							continue;
						}
						return result;
					case 2:
						goto IL_75;
					}
					goto IL_43;
				}
				IL_75:
				result = byte.Parse(A_0.Value);
				num = 0;
			}
		}
		return result;
	}

	// Token: 0x06004FF5 RID: 20469 RVA: 0x00316444 File Offset: 0x00315444
	private byte \u1712(XmlReader A_0)
	{
		int a_ = 8;
		byte result;
		for (;;)
		{
			result = 1;
			int num = 2;
			for (;;)
			{
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_75;
				}
				if (false)
				{
				}
				switch (num)
				{
				case 0:
					return result;
				case 1:
					goto IL_75;
				case 2:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䠽ℿ⹁", a_)))
					{
						num = 1;
						continue;
					}
					return result;
				}
				break;
				IL_75:
				result = byte.Parse(A_0.Value);
				num = 0;
			}
		}
		return result;
	}

	// Token: 0x06004FF6 RID: 20470 RVA: 0x003164E0 File Offset: 0x003154E0
	private void ᜑ(XmlReader A_0)
	{
		int a_ = 8;
		int num = 9;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!A_0.IsEmptyElement)
				{
					num = 4;
					continue;
				}
				return;
			case 1:
				if (A_0.LocalName != RecordTableEnumerator.b("倽㔿⽁Ƀ⭅㱇㥉", a_))
				{
					num = 14;
					continue;
				}
				num = 0;
				continue;
			case 2:
				goto IL_F4;
			case 3:
				goto IL_60;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					break;
				}
				A_0.Read();
				num = 2;
				continue;
			case 5:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 10;
					continue;
				}
				goto IL_121;
			case 6:
				this.ᜐ(A_0);
				num = 13;
				continue;
			case 7:
				goto IL_114;
			case 8:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 6;
					continue;
				}
				goto IL_A8;
			case 10:
				num = 1;
				continue;
			case 11:
				goto IL_F4;
			case 12:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 7;
					continue;
				}
				num = 8;
				continue;
			case 13:
				goto IL_A8;
			case 14:
				goto IL_17B;
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			num = 5;
			continue;
			IL_A8:
			A_0.Read();
			num = 11;
			continue;
			IL_F4:
			num = 12;
		}
		IL_60:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰽┿⍁⁃⍅㩇", a_));
		IL_114:
		if (true)
		{
		}
		return;
		IL_121:
		throw new XmlException(RecordTableEnumerator.b("欽⸿❁㱃㙅ⵇ⥉㡋⭍㑏牑⁓㝕㽗穙", a_) + A_0.LocalName);
		IL_17B:
		goto IL_121;
	}

	// Token: 0x06004FF7 RID: 20471 RVA: 0x003166AC File Offset: 0x003156AC
	private void ᜐ(XmlReader A_0)
	{
		int a_ = 8;
		int num = 11;
		int a_2;
		string a_3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_54;
			case 1:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("堽⼿ぁ⥃❅㱇ॉ⍋⩍㕏", a_)))
				{
					num = 4;
					continue;
				}
				goto IL_105;
			case 2:
				goto IL_169;
			case 3:
				if (A_0.LocalName != RecordTableEnumerator.b("倽㔿⽁Ƀ⭅㱇", a_))
				{
					num = 2;
					continue;
				}
				a_2 = -1;
				a_3 = null;
				num = 8;
				continue;
			case 4:
				a_3 = A_0.Value;
				num = 5;
				continue;
			case 5:
				goto IL_D8;
			case 6:
				num = 1;
				continue;
			case 7:
				a_2 = Convert.ToInt32(A_0.Value);
				num = 6;
				continue;
			case 8:
				if (!A_0.MoveToAttribute(RecordTableEnumerator.b("倽㔿⽁Ƀ⭅㱇͉⡋", a_)))
				{
					goto IL_F1;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_B1;
				default:
					if (false)
					{
					}
					num = 7;
					continue;
				}
				break;
			case 9:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					goto IL_B1;
				}
				goto IL_119;
			case 10:
				if (true)
				{
				}
				num = 3;
				continue;
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			num = 9;
			continue;
			IL_B1:
			num = 10;
		}
		IL_54:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰽┿⍁⁃⍅㩇", a_));
		IL_D8:
		this.ᜉ.InnerFormats.ᜀ(a_2, a_3);
		return;
		IL_F1:
		throw new XmlException(RecordTableEnumerator.b("倽㔿⽁Ƀ⭅㱇͉⡋湍❏㍑❓㡕罗⹙籛㡝ཟᝡ੣ɥ", a_));
		IL_105:
		throw new XmlException(RecordTableEnumerator.b("堽⼿ぁ⥃❅㱇ॉ⍋⩍㕏牑⍓㝕⭗㑙筛⩝䁟ѡୣ፥٧๩", a_));
		IL_119:
		throw new XmlException(RecordTableEnumerator.b("欽⸿❁㱃㙅ⵇ⥉㡋⭍㑏牑⁓㝕㽗穙", a_) + A_0.LocalName);
		IL_169:
		goto IL_119;
	}

	// Token: 0x06004FF8 RID: 20472 RVA: 0x00316894 File Offset: 0x00315894
	private OColor ᜏ(XmlReader A_0)
	{
		int a_ = 11;
		if (A_0 == null)
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
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("㍀♂⑄⍆ⱈ㥊", a_));
			}
		}
		OColor ocolor = new OColor(ExcelColors.BlackCustom);
		this.ᜀ(A_0, ocolor);
		return ocolor;
	}

	// Token: 0x06004FF9 RID: 20473 RVA: 0x00316904 File Offset: 0x00315904
	private void ᜀ(XmlReader A_0, OColor A_1)
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			int num = 2;
			double dTintValue;
			Color rgb;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					int themeIndex = Convert.ToInt32(A_0.Value);
					A_1.SetTheme(themeIndex, this.ᜉ, dTintValue);
					num = 6;
					continue;
				}
				case 1:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䠻圽⸿㙁", a_)))
					{
						num = 11;
						continue;
					}
					goto IL_131;
				case 3:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("主夽∿", a_)))
					{
						num = 8;
						continue;
					}
					goto IL_EA;
				case 4:
					goto IL_65;
				case 5:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䠻嘽┿⽁⅃", a_)))
					{
						num = 0;
						continue;
					}
					return;
				case 6:
					return;
				case 7:
					goto IL_131;
				case 8:
					goto IL_15F;
				case 9:
					goto IL_DD;
				case 10:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("唻倽␿❁㱃⍅ⱇ", a_)))
					{
						num = 9;
						continue;
					}
					rgb = spr\u1D39.ᜂ;
					dTintValue = 0.0;
					num = 1;
					continue;
				case 11:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_EA;
					default:
						if (false)
						{
						}
						dTintValue = XmlConvert.ToDouble(A_0.Value);
						num = 7;
						continue;
					}
					break;
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				num = 10;
				continue;
				IL_EA:
				num = 5;
				continue;
				IL_131:
				num = 3;
			}
			IL_65:
			throw new ArgumentNullException(RecordTableEnumerator.b("主嬽ℿ♁⅃㑅", a_));
			IL_DD:
			if (true)
			{
			}
			ExcelColors a_2 = (ExcelColors)Convert.ToInt32(A_0.Value);
			A_1.ᜀ(a_2, true, this.ᜉ);
			return;
			IL_15F:
			rgb = spr\u1D39.ᜀ(int.Parse(A_0.Value, NumberStyles.HexNumber));
			A_1.SetRGB(rgb, this.ᜉ, dTintValue);
			return;
		}
		}
	}

	// Token: 0x06004FFA RID: 20474 RVA: 0x00316B28 File Offset: 0x00315B28
	internal static Color ᜀ(Color A_0, double A_1)
	{
		double a_;
		double num;
		double a_2;
		for (;;)
		{
			spr\u2306.ᜀ(A_0, out a_, out num, out a_2);
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_B3;
				case 1:
					goto IL_34;
				case 2:
					goto IL_B1;
				case 3:
					num = num * (1.0 - A_1) + (255.0 - 255.0 * (1.0 - A_1));
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_34;
					default:
						if (false)
						{
						}
						num2 = 2;
						continue;
					}
					break;
				case 4:
					if (true)
					{
					}
					num *= 1.0 + A_1;
					num2 = 0;
					continue;
				case 5:
					if (A_1 > 0.0)
					{
						num2 = 3;
						continue;
					}
					goto IL_F2;
				}
				break;
				IL_34:
				if (A_1 < 0.0)
				{
					num2 = 4;
					continue;
				}
				IL_B3:
				num2 = 5;
			}
		}
		IL_B1:
		IL_F2:
		spr\u2306.ᜁ(a_, num, a_2);
		return spr\u2306.ᜁ(a_, num, a_2);
	}

	// Token: 0x06004FFB RID: 20475 RVA: 0x00316C38 File Offset: 0x00315C38
	internal static void ᜀ(Color A_0, out double A_1, out double A_2, out double A_3)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				A_1 = 0.0;
				A_2 = 0.0;
				A_3 = 0.0;
				byte r = A_0.R;
				byte g = A_0.G;
				byte b = A_0.B;
				byte b2 = Math.Min(r, Math.Min(g, b));
				byte b3 = Math.Max(r, Math.Max(g, b));
				double num = (double)(b3 - b2);
				double num2 = (double)(b3 + b2);
				A_2 = (num2 * 255.0 + 255.0) / 510.0;
				int num3 = 14;
				for (;;)
				{
					double num4;
					double num5;
					double num6;
					switch (num3)
					{
					case 0:
						goto IL_415;
					case 1:
						A_3 = 255.0;
						if (true)
						{
						}
						num3 = 15;
						continue;
					case 2:
						if (g == b3)
						{
							num3 = 10;
							continue;
						}
						A_1 = 170.0 + num4 - num5;
						num3 = 5;
						continue;
					case 3:
						A_1 -= 255.0;
						num3 = 0;
						continue;
					case 4:
						A_1 = num6 - num4;
						num3 = 27;
						continue;
					case 5:
						goto IL_45E;
					case 6:
						if (A_2 > 255.0)
						{
							num3 = 31;
							continue;
						}
						return;
					case 7:
						A_1 += 255.0;
						num3 = 29;
						continue;
					case 8:
						goto IL_45E;
					case 9:
						A_3 = 0.0;
						A_1 = 170.0;
						num3 = 24;
						continue;
					case 10:
						A_1 = 85.0 + num5 - num6;
						num3 = 8;
						continue;
					case 11:
						if (A_2 <= 127.0)
						{
							num3 = 21;
							continue;
						}
						A_3 = (num * 255.0 + (510.0 - num2) / 2.0) / (510.0 - num2);
						num3 = 22;
						continue;
					case 12:
						if (A_1 > 255.0)
						{
							num3 = 3;
							continue;
						}
						goto IL_415;
					case 13:
						if (r == b3)
						{
							num3 = 4;
							continue;
						}
						num3 = 2;
						continue;
					case 14:
						if (b3 == b2)
						{
							num3 = 9;
							continue;
						}
						num3 = 11;
						continue;
					case 15:
						goto IL_201;
					case 16:
						return;
					case 17:
						if (A_1 < 0.0)
						{
							num3 = 7;
							continue;
						}
						goto IL_161;
					case 18:
						goto IL_18E;
					case 19:
						goto IL_284;
					case 20:
						A_2 = 0.0;
						goto IL_2F0;
					case 21:
						A_3 = (num * 255.0 + num2 / 2.0) / num2;
						num3 = 18;
						continue;
					case 22:
						goto IL_18E;
					case 23:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2F0;
						default:
							if (false)
							{
							}
							A_3 = 0.0;
							num3 = 26;
							continue;
						}
						break;
					case 24:
						goto IL_415;
					case 25:
						if (A_2 < 0.0)
						{
							num3 = 20;
							continue;
						}
						goto IL_284;
					case 26:
						goto IL_25A;
					case 27:
						goto IL_45E;
					case 28:
						if (A_3 < 0.0)
						{
							num3 = 23;
							continue;
						}
						goto IL_25A;
					case 29:
						goto IL_161;
					case 30:
						if (A_3 > 255.0)
						{
							num3 = 1;
							continue;
						}
						goto IL_201;
					case 31:
						A_2 = 255.0;
						num3 = 16;
						continue;
					}
					break;
					IL_161:
					num3 = 12;
					continue;
					IL_18E:
					num5 = ((double)((b3 - r) * 42) + num / 2.0) / num;
					num4 = ((double)((b3 - g) * 42) + num / 2.0) / num;
					num6 = ((double)((b3 - b) * 42) + num / 2.0) / num;
					num3 = 13;
					continue;
					IL_201:
					num3 = 25;
					continue;
					IL_25A:
					num3 = 30;
					continue;
					IL_284:
					num3 = 6;
					continue;
					IL_2F0:
					num3 = 19;
					continue;
					IL_415:
					num3 = 28;
					continue;
					IL_45E:
					num3 = 17;
				}
			}
			return;
		}
	}

	// Token: 0x06004FFC RID: 20476 RVA: 0x0031711C File Offset: 0x0031611C
	internal static Color ᜁ(double A_0, double A_1, double A_2)
	{
		switch (0)
		{
		default:
		{
			int num;
			int num2;
			int num3;
			for (;;)
			{
				num = 0;
				num2 = 0;
				num3 = 0;
				int num4 = 23;
				for (;;)
				{
					double num5;
					switch (num4)
					{
					case 0:
						goto IL_1C5;
					case 1:
						num5 = (A_1 * (255.0 + A_2) + 127.0) / 255.0;
						num4 = 0;
						continue;
					case 2:
						goto IL_271;
					case 3:
						goto IL_2B8;
					case 4:
						goto IL_AE;
					case 5:
						if (num2 < 0)
						{
							num4 = 17;
							continue;
						}
						goto IL_D6;
					case 6:
						if (num2 > 255)
						{
							num4 = 22;
							continue;
						}
						goto IL_271;
					case 7:
						if (A_1 <= 127.0)
						{
							num4 = 1;
							continue;
						}
						num5 = A_1 + A_2 - (A_1 * A_2 + 127.0) / 255.0;
						num4 = 18;
						continue;
					case 8:
						num3 = (int)(A_1 * 255.0 / 255.0);
						num = num3;
						num2 = num3;
						num4 = 15;
						continue;
					case 9:
						num3 = 255;
						num4 = 16;
						continue;
					case 10:
						if (true)
						{
						}
						num2 = 255;
						num4 = 13;
						continue;
					case 11:
						if (num2 > 255)
						{
							num4 = 10;
							continue;
						}
						goto IL_179;
					case 12:
						goto IL_1A1;
					case 13:
						goto IL_179;
					case 14:
						if (num < 0)
						{
							num4 = 24;
							continue;
						}
						goto IL_1A1;
					case 15:
						goto IL_2B8;
					case 16:
						goto IL_2B3;
					case 17:
						num2 = 0;
						num4 = 20;
						continue;
					case 18:
						goto IL_1C5;
					case 19:
						if (num3 < 0)
						{
							num4 = 21;
							continue;
						}
						goto IL_AE;
					case 20:
						goto IL_D6;
					case 21:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_8E;
						default:
							if (false)
							{
							}
							num3 = 0;
							num4 = 4;
							continue;
						}
						break;
					case 22:
						num = 255;
						num4 = 2;
						continue;
					case 23:
						goto IL_8E;
					case 24:
						num = 0;
						num4 = 12;
						continue;
					case 25:
						if (num3 > 255)
						{
							num4 = 9;
							continue;
						}
						goto IL_398;
					}
					break;
					IL_8E:
					if (A_2 == 0.0)
					{
						num4 = 8;
						continue;
					}
					num4 = 7;
					continue;
					IL_AE:
					num4 = 6;
					continue;
					IL_D6:
					num4 = 19;
					continue;
					IL_179:
					num4 = 25;
					continue;
					IL_1A1:
					num4 = 5;
					continue;
					IL_1C5:
					double a_ = 2.0 * A_1 - num5;
					num = (int)((spr\u2306.ᜀ(a_, num5, A_0 + 85.0) * 255.0 + 127.0) / 255.0);
					num2 = (int)((spr\u2306.ᜀ(a_, num5, A_0) * 255.0 + 127.0) / 255.0);
					num3 = (int)((spr\u2306.ᜀ(a_, num5, A_0 - 85.0) * 255.0 + 127.0) / 255.0);
					num4 = 3;
					continue;
					IL_271:
					num4 = 11;
					continue;
					IL_2B8:
					num4 = 14;
				}
			}
			IL_2B3:
			IL_398:
			return Color.FromArgb(0, (int)((byte)num), (int)((byte)num2), (int)((byte)num3));
		}
		}
	}

	// Token: 0x06004FFD RID: 20477 RVA: 0x003174D0 File Offset: 0x003164D0
	internal static double ᜀ(double A_0, double A_1, double A_2)
	{
		int num = 14;
		double result;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_2 < 127.0)
				{
					num = 10;
					continue;
				}
				num = 1;
				continue;
			case 1:
				if (A_2 < 170.0)
				{
					if (true)
					{
					}
					num = 12;
					continue;
				}
				result = A_0;
				num = 6;
				continue;
			case 2:
				goto IL_157;
			case 3:
				return result;
			case 4:
				goto IL_B9;
			case 5:
				return result;
			case 6:
				return result;
			case 7:
				if (A_2 < 42.0)
				{
					num = 13;
					continue;
				}
				num = 0;
				continue;
			case 8:
				A_2 -= 255.0;
				num = 2;
				continue;
			case 9:
				A_2 += 255.0;
				num = 4;
				continue;
			case 10:
				result = A_1;
				num = 11;
				continue;
			case 11:
				return result;
			case 12:
				result = A_0 + ((A_1 - A_0) * (170.0 - A_2) + 21.0) / 42.0;
				num = 5;
				continue;
			case 13:
				result = A_0 + ((A_1 - A_0) * A_2 + 21.0) / 42.0;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			case 15:
				if (A_2 > 255.0)
				{
					num = 8;
					continue;
				}
				goto IL_157;
			}
			if (A_2 < 0.0)
			{
				num = 9;
				continue;
			}
			IL_B9:
			num = 15;
			continue;
			IL_157:
			num = 7;
		}
		return result;
	}

	// Token: 0x06004FFE RID: 20478 RVA: 0x003176C8 File Offset: 0x003166C8
	private bool ᜀ(XmlReader A_0, string A_1, bool A_2)
	{
		bool result;
		for (;;)
		{
			for (;;)
			{
				if (true)
				{
				}
				result = A_2;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						result = XmlConvert.ToBoolean(A_0.Value);
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
							continue;
						}
						break;
					case 1:
						if (A_0.MoveToAttribute(A_1))
						{
							num = 0;
							continue;
						}
						return result;
					case 2:
						return result;
					}
					break;
				}
			}
		}
		return result;
	}

	// Token: 0x06004FFF RID: 20479 RVA: 0x0031774C File Offset: 0x0031674C
	private string ᜁ(XmlReader A_0, string A_1)
	{
		string result;
		for (;;)
		{
			for (;;)
			{
				result = null;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						result = A_0.Value;
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
							num = 1;
							continue;
						}
						break;
					case 1:
						return result;
					case 2:
						if (A_0.MoveToAttribute(A_1))
						{
							num = 0;
							continue;
						}
						return result;
					}
					break;
				}
			}
		}
		return result;
	}

	// Token: 0x06005000 RID: 20480 RVA: 0x003177CC File Offset: 0x003167CC
	private List<XlsFill> ᜎ(XmlReader A_0)
	{
		int a_ = 9;
		int num = 0;
		List<XlsFill> list;
		for (;;)
		{
			switch (num)
			{
			case 1:
				A_0.Read();
				num = 12;
				continue;
			case 2:
			{
				XlsFill item = this.ᜁ(A_0, true);
				list.Add(item);
				num = 11;
				continue;
			}
			case 3:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 10;
					continue;
				}
				num = 8;
				continue;
			case 4:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 9;
					continue;
				}
				return list;
			case 5:
				if (A_0.LocalName == RecordTableEnumerator.b("夾⡀⽂⥄㑆", a_))
				{
					num = 1;
					continue;
				}
				return list;
			case 6:
				goto IL_EB;
			case 7:
				goto IL_62;
			case 8:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 2;
					continue;
				}
				goto IL_129;
			case 9:
				num = 5;
				continue;
			case 10:
				goto IL_10B;
			case 11:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					goto IL_129;
				}
				break;
			case 12:
				goto IL_EB;
			}
			IL_57:
			if (A_0 == null)
			{
				num = 7;
				continue;
			}
			list = new List<XlsFill>();
			num = 4;
			continue;
			goto IL_57;
			IL_EB:
			num = 3;
			continue;
			IL_129:
			A_0.Read();
			num = 6;
		}
		IL_62:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴾⑀≂⅄≆㭈", a_));
		IL_10B:
		if (true)
		{
		}
		return list;
	}

	// Token: 0x06005001 RID: 20481 RVA: 0x00317960 File Offset: 0x00316960
	private XlsFill ᜁ(XmlReader A_0, bool A_1)
	{
		int a_ = 3;
		int num = 3;
		XlsFill result;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_96;
				}
				break;
			case 1:
				goto IL_100;
			case 2:
				num = 10;
				continue;
			case 4:
				num = 6;
				continue;
			case 5:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("常䤺尼嬾⡀♂⭄㍆཈≊⅌⍎", a_)))
				{
					num = 4;
					continue;
				}
				result = this.\u170D(A_0);
				num = 7;
				continue;
			}
			case 6:
				goto IL_13C;
			case 7:
				goto IL_118;
			case 8:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 2;
					continue;
				}
				goto IL_13E;
			}
			case 9:
				num = 5;
				continue;
			case 10:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("䤸娺䤼䬾⑀ㅂ⭄ņ⁈❊⅌", a_)))
				{
					num = 9;
					continue;
				}
				result = this.ᜀ(A_0, A_1);
				if (true)
				{
				}
				num = 0;
				continue;
			}
			case 11:
				if (A_0.LocalName != RecordTableEnumerator.b("弸刺儼匾", a_))
				{
					num = 1;
					continue;
				}
				A_0.Read();
				result = null;
				num = 8;
				continue;
			case 12:
				goto IL_62;
			}
			IL_4D:
			if (A_0 == null)
			{
				num = 12;
				continue;
			}
			num = 11;
			continue;
			goto IL_4D;
		}
		IL_62:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬸帺尼嬾⑀ㅂ", a_));
		IL_96:
		if (false)
		{
		}
		return result;
		IL_100:
		throw new XmlException(RecordTableEnumerator.b("游䤺刼儾♀捂ㅄ♆⹈歊⍌⹎㱐㙒畔", a_) + A_0.LocalName);
		IL_118:
		return result;
		IL_13C:
		IL_13E:
		throw new ArgumentException(RecordTableEnumerator.b("永唺堼䜾ㅀ♂♄㍆ⱈ⽊浌㭎ぐ㑒畔睖", a_) + A_0.LocalName);
	}

	// Token: 0x06005002 RID: 20482 RVA: 0x00317B4C File Offset: 0x00316B4C
	private XlsFill \u170D(XmlReader A_0)
	{
		int a_ = 0;
		XlsFill result;
		for (;;)
		{
			IL_09:
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.LocalName != RecordTableEnumerator.b("儵䨷嬹堻圽┿ⱁぃEⅇ♉⁋", a_))
					{
						num = 4;
						continue;
					}
					result = null;
					num = 6;
					continue;
				case 1:
					num = 8;
					continue;
				case 2:
					goto IL_14E;
				case 3:
					goto IL_11D;
				case 4:
					goto IL_108;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 6:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䈵䄷䨹夻", a_)))
					{
						num = 1;
						continue;
					}
					goto IL_10A;
				case 7:
					goto IL_68;
				case 8:
					if (A_0.Value == RecordTableEnumerator.b("䘵夷丹吻", a_))
					{
						num = 9;
						continue;
					}
					goto IL_10A;
				case 9:
					result = this.ᜌ(A_0);
					num = 2;
					continue;
				}
				if (A_0 == null)
				{
					num = 7;
					continue;
				}
				num = 0;
				continue;
				IL_10A:
				result = this.ᜊ(A_0);
				num = 3;
			}
		}
		IL_68:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䐵崷嬹堻嬽㈿", a_));
		IL_108:
		throw new XmlException(RecordTableEnumerator.b("挵嘷弹䐻丽┿⅁ぃ⍅ⱇ橉㡋⽍㝏牑", a_) + A_0.LocalName);
		IL_11D:
		IL_14E:
		A_0.Skip();
		return result;
	}

	// Token: 0x06005003 RID: 20483 RVA: 0x00317CD0 File Offset: 0x00316CD0
	private XlsFill ᜌ(XmlReader A_0)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			XlsFill xlsFill;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (false)
				{
				}
				int num = 33;
				for (;;)
				{
					double num2;
					double num3;
					double num4;
					double num5;
					switch (num)
					{
					case 0:
						if (num2 == 1.0)
						{
							num = 5;
							continue;
						}
						goto IL_1E7;
					case 1:
						num = 8;
						continue;
					case 2:
						xlsFill.GradientStyle = GradientStyleType.From_Corner;
						xlsFill.GradientVariant = GradientVariantsType.ShadingVariants2;
						num = 20;
						continue;
					case 3:
						goto IL_F6;
					case 4:
						xlsFill.GradientStyle = GradientStyleType.From_Corner;
						xlsFill.GradientVariant = GradientVariantsType.ShadingVariants1;
						num = 11;
						continue;
					case 5:
						num = 14;
						continue;
					case 6:
						num = 34;
						continue;
					case 7:
						num = 29;
						continue;
					case 8:
						if (num3 == 0.5)
						{
							num = 28;
							continue;
						}
						goto IL_327;
					case 9:
						if (num4 == 0.5)
						{
							num = 13;
							continue;
						}
						goto IL_327;
					case 10:
						if (double.IsNaN(num5))
						{
							num = 4;
							continue;
						}
						goto IL_4CA;
					case 11:
						goto IL_2B9;
					case 12:
						num = 24;
						continue;
					case 13:
						num = 25;
						continue;
					case 14:
						if (num3 == 1.0)
						{
							num = 27;
							continue;
						}
						goto IL_1E7;
					case 15:
						if (num2 == 0.5)
						{
							num = 1;
							continue;
						}
						goto IL_327;
					case 16:
						xlsFill.GradientStyle = GradientStyleType.From_Center;
						xlsFill.GradientVariant = GradientVariantsType.ShadingVariants1;
						num = 31;
						continue;
					case 17:
						num = 36;
						continue;
					case 18:
						if (double.IsNaN(num2))
						{
							num = 7;
							continue;
						}
						goto IL_4CA;
					case 19:
						xlsFill.GradientStyle = GradientStyleType.From_Corner;
						xlsFill.GradientVariant = GradientVariantsType.ShadingVariants4;
						num = 26;
						continue;
					case 20:
						goto IL_3BE;
					case 21:
						if (num4 == 1.0)
						{
							num = 17;
							continue;
						}
						goto IL_37C;
					case 22:
						if (num5 == 1.0)
						{
							num = 19;
							continue;
						}
						goto IL_3ED;
					case 23:
						num = 32;
						continue;
					case 24:
						if (num4 == 1.0)
						{
							num = 38;
							continue;
						}
						goto IL_3ED;
					case 25:
						if (true)
						{
						}
						if (num5 == 0.5)
						{
							num = 16;
							continue;
						}
						goto IL_327;
					case 26:
						goto IL_253;
					case 27:
						xlsFill.GradientStyle = GradientStyleType.From_Corner;
						xlsFill.GradientVariant = GradientVariantsType.ShadingVariants3;
						num = 30;
						continue;
					case 28:
						num = 9;
						continue;
					case 29:
						if (double.IsNaN(num3))
						{
							num = 23;
							continue;
						}
						goto IL_4CA;
					case 30:
						goto IL_433;
					case 31:
						goto IL_272;
					case 32:
						if (double.IsNaN(num4))
						{
							num = 37;
							continue;
						}
						goto IL_4CA;
					case 34:
						if (num3 == 1.0)
						{
							num = 12;
							continue;
						}
						goto IL_3ED;
					case 35:
						if (num2 == 1.0)
						{
							num = 6;
							continue;
						}
						goto IL_3ED;
					case 36:
						if (num5 == 1.0)
						{
							num = 2;
							continue;
						}
						goto IL_37C;
					case 37:
						num = 10;
						continue;
					case 38:
						num = 22;
						continue;
					}
					if (A_0 == null)
					{
						num = 3;
						continue;
					}
					xlsFill = new XlsFill();
					xlsFill.FillType = ShapeFillType.Gradient;
					num2 = this.ᜀ(A_0, RecordTableEnumerator.b("㉅❇㩉", a_));
					num3 = this.ᜀ(A_0, RecordTableEnumerator.b("⑅❇㹉㡋⅍㵏", a_));
					num4 = this.ᜀ(A_0, RecordTableEnumerator.b("⩅ⵇⱉ㡋", a_));
					num5 = this.ᜀ(A_0, RecordTableEnumerator.b("㑅ⅇⵉ⑋㩍", a_));
					num = 15;
					continue;
					IL_1E7:
					num = 21;
					continue;
					IL_327:
					num = 35;
					continue;
					IL_37C:
					num = 18;
					continue;
					IL_3ED:
					num = 0;
				}
				IL_F6:
				throw new ArgumentNullException(RecordTableEnumerator.b("㑅ⵇ⭉⡋⭍≏", a_));
				IL_272:
				IL_2B9:
				IL_3BE:
				IL_433:
				break;
			}
			}
			IL_253:
			IL_4CA:
			A_0.Read();
			List<OColor> list = this.ᜋ(A_0);
			xlsFill.PatternColorObject.ᜀ(list[0], true);
			xlsFill.OColor.ᜀ(list[1], true);
			return xlsFill;
		}
		}
	}

	// Token: 0x06005004 RID: 20484 RVA: 0x003181E0 File Offset: 0x003171E0
	private List<OColor> ᜋ(XmlReader A_0)
	{
		int a_ = 19;
		if (true)
		{
		}
		List<OColor> list;
		for (;;)
		{
			list = new List<OColor>();
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_D8;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E3;
					default:
						if (false)
						{
						}
						A_0.Read();
						list.Add(this.ᜏ(A_0));
						A_0.Skip();
						num = 4;
						continue;
					}
					break;
				case 2:
					goto IL_D8;
				case 3:
					goto IL_E3;
				case 4:
					goto IL_52;
				case 5:
					if (A_0.LocalName == RecordTableEnumerator.b("㩈㽊≌㽎", a_))
					{
						num = 1;
						continue;
					}
					goto IL_52;
				case 6:
					return list;
				}
				break;
				IL_52:
				A_0.Skip();
				num = 2;
				continue;
				IL_E3:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 6;
					continue;
				}
				num = 5;
				continue;
				IL_D8:
				num = 3;
			}
		}
		return list;
	}

	// Token: 0x06005005 RID: 20485 RVA: 0x003182E8 File Offset: 0x003172E8
	private XlsFill ᜊ(XmlReader A_0)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			int num = 16;
			XlsFill xlsFill;
			for (;;)
			{
				double num3;
				int num2;
				int num4;
				List<OColor> list;
				switch (num)
				{
				case 0:
					num = 23;
					continue;
				case 1:
					num = 4;
					continue;
				case 2:
					num = 7;
					continue;
				case 3:
					goto IL_1B8;
				case 4:
					num2 = (int)num3;
					goto IL_288;
				case 5:
					num = 12;
					continue;
				case 6:
					num = 24;
					continue;
				case 7:
					if (num4 != 45)
					{
						num = 9;
						continue;
					}
					xlsFill.GradientStyle = GradientStyleType.Diagonl_Up;
					num = 11;
					continue;
				case 8:
					goto IL_FF;
				case 9:
					num = 3;
					continue;
				case 10:
					if (num4 != 90)
					{
						num = 0;
						continue;
					}
					xlsFill.GradientStyle = GradientStyleType.Horizontal;
					num = 15;
					continue;
				case 11:
					return xlsFill;
				case 12:
					goto IL_196;
				case 13:
					return xlsFill;
				case 14:
					if (true)
					{
					}
					goto IL_259;
				case 15:
					goto IL_B1;
				case 17:
					num2 = 0;
					goto IL_288;
				case 18:
					if (!double.IsNaN(num3))
					{
						num = 1;
						continue;
					}
					num = 17;
					continue;
				case 19:
					return xlsFill;
				case 20:
					if (list.Count == 3)
					{
						num = 14;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_259;
					}
					if (false)
					{
					}
					this.ᜀ(xlsFill, num3);
					num = 13;
					continue;
				case 21:
					goto IL_99;
				case 22:
					if (num4 <= 45)
					{
						num = 6;
						continue;
					}
					num = 10;
					continue;
				case 23:
					if (num4 != 135)
					{
						num = 5;
						continue;
					}
					xlsFill.GradientStyle = GradientStyleType.Diagonl_Down;
					num = 8;
					continue;
				case 24:
					if (num4 != 0)
					{
						num = 2;
						continue;
					}
					xlsFill.GradientStyle = GradientStyleType.Vertical;
					num = 19;
					continue;
				}
				if (A_0 == null)
				{
					num = 21;
					continue;
				}
				xlsFill = new XlsFill();
				xlsFill.FillType = ShapeFillType.Gradient;
				num3 = this.ᜀ(A_0, RecordTableEnumerator.b("♁⅃ⅅ㩇⽉⥋", a_));
				A_0.Read();
				list = this.ᜋ(A_0);
				xlsFill.PatternColorObject.ᜀ(list[0], true);
				xlsFill.OColor.ᜀ(list[1], true);
				num = 20;
				continue;
				IL_259:
				xlsFill.GradientVariant = GradientVariantsType.ShadingVariants3;
				num = 18;
				continue;
				IL_288:
				int num5 = num2;
				num4 = num5;
				num = 22;
			}
			IL_99:
			throw new ArgumentNullException(RecordTableEnumerator.b("ぁ⅃❅ⱇ⽉㹋", a_));
			IL_B1:
			IL_FF:
			return xlsFill;
			IL_196:
			IL_198:
			throw new ArgumentException(RecordTableEnumerator.b("ᝁ⩃㕅㵇㩉㱋⅍≏♑ㅓ㉕硗㹙㥛㥝቟ݡţ䙥ṧ୩k᭭ᕯ", a_));
			IL_1B8:
			goto IL_198;
		}
		}
	}

	// Token: 0x06005006 RID: 20486 RVA: 0x0031860C File Offset: 0x0031760C
	private void ᜀ(XlsFill A_0, double A_1)
	{
		int a_ = 6;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
		{
			if (false)
			{
			}
			int num = 16;
			for (;;)
			{
				int num2;
				int num3;
				switch (num)
				{
				case 0:
					if (num2 != 90)
					{
						num = 1;
						continue;
					}
					goto IL_280;
				case 1:
					num = 5;
					continue;
				case 2:
					num = 13;
					continue;
				case 3:
					if (num2 != 0)
					{
						num = 20;
						continue;
					}
					goto IL_218;
				case 4:
					if (num2 != 180)
					{
						num = 10;
						continue;
					}
					goto IL_152;
				case 5:
					if (num2 != 135)
					{
						num = 27;
						continue;
					}
					goto IL_1C4;
				case 6:
					goto IL_103;
				case 7:
					if (num2 != 45)
					{
						num = 9;
						continue;
					}
					goto IL_2AF;
				case 8:
					num = 23;
					continue;
				case 9:
					num = 14;
					continue;
				case 10:
					num = 19;
					continue;
				case 11:
					if (num2 != 270)
					{
						num = 8;
						continue;
					}
					goto IL_2BE;
				case 12:
					num = 29;
					continue;
				case 13:
					if (num2 <= 45)
					{
						num = 26;
						continue;
					}
					num = 0;
					continue;
				case 14:
					goto IL_D3;
				case 15:
					num3 = 0;
					goto IL_227;
				case 17:
					num3 = (int)A_1;
					goto IL_227;
				case 18:
					num = 4;
					continue;
				case 19:
					if (num2 != 225)
					{
						if (true)
						{
						}
						num = 21;
						continue;
					}
					goto IL_187;
				case 20:
					num = 7;
					continue;
				case 21:
					num = 6;
					continue;
				case 22:
					if (num2 <= 225)
					{
						num = 18;
						continue;
					}
					num = 11;
					continue;
				case 23:
					if (num2 != 315)
					{
						num = 12;
						continue;
					}
					goto IL_2CD;
				case 24:
					num = 17;
					continue;
				case 25:
					goto IL_14D;
				case 26:
					num = 3;
					continue;
				case 27:
					num = 25;
					continue;
				case 28:
					if (num2 <= 135)
					{
						num = 2;
						continue;
					}
					num = 22;
					continue;
				case 29:
					goto IL_2E7;
				}
				if (!double.IsNaN(A_1))
				{
					num = 24;
					continue;
				}
				num = 15;
				continue;
				IL_227:
				int num4 = num3;
				num2 = num4;
				num = 28;
			}
			IL_D3:
			IL_14D:
			break;
			IL_152:
			A_0.GradientStyle = GradientStyleType.Vertical;
			A_0.GradientVariant = GradientVariantsType.ShadingVariants2;
			return;
			IL_187:
			A_0.GradientStyle = GradientStyleType.Diagonl_Up;
			A_0.GradientVariant = GradientVariantsType.ShadingVariants2;
			return;
			IL_1C4:
			A_0.GradientStyle = GradientStyleType.Diagonl_Down;
			A_0.GradientVariant = GradientVariantsType.ShadingVariants1;
			return;
			IL_218:
			A_0.GradientStyle = GradientStyleType.Vertical;
			A_0.GradientVariant = GradientVariantsType.ShadingVariants1;
			return;
			IL_280:
			A_0.GradientStyle = GradientStyleType.Horizontal;
			A_0.GradientVariant = GradientVariantsType.ShadingVariants1;
			return;
			IL_2AF:
			A_0.GradientStyle = GradientStyleType.Diagonl_Up;
			A_0.GradientVariant = GradientVariantsType.ShadingVariants1;
			return;
			IL_2BE:
			A_0.GradientStyle = GradientStyleType.Horizontal;
			A_0.GradientVariant = GradientVariantsType.ShadingVariants2;
			return;
			IL_2CD:
			A_0.GradientStyle = GradientStyleType.Diagonl_Down;
			A_0.GradientVariant = GradientVariantsType.ShadingVariants2;
			return;
			IL_2E7:
			break;
		}
		}
		IL_103:
		throw new ArgumentException(RecordTableEnumerator.b("椻倽㌿㝁㑃㙅❇㡉㡋⭍㑏牑こ㍕㽗⡙㥛㭝䁟ᑡգ੥ᵧཀྵ", a_));
	}

	// Token: 0x06005007 RID: 20487 RVA: 0x00318958 File Offset: 0x00317958
	private double ᜀ(XmlReader A_0, string A_1)
	{
		if (A_0.MoveToAttribute(A_1))
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_09;
			}
			if (false)
			{
			}
			return XmlConvert.ToDouble(A_0.Value);
		}
		IL_09:
		if (true)
		{
		}
		return double.NaN;
	}

	// Token: 0x06005008 RID: 20488 RVA: 0x003189B4 File Offset: 0x003179B4
	private XlsFill ᜀ(XmlReader A_0, bool A_1)
	{
		int a_ = 4;
		switch (0)
		{
		default:
		{
			int num = 6;
			XlsFill xlsFill;
			for (;;)
			{
				OColor ocolor;
				OColor ocolor2;
				switch (num)
				{
				case 0:
					if (xlsFill.Pattern != ExcelPatternType.Solid)
					{
						num = 2;
						continue;
					}
					goto IL_33F;
				case 1:
					xlsFill.PatternColorObject.ᜀ(ocolor, true);
					num = 4;
					continue;
				case 2:
					goto IL_368;
				case 3:
					goto IL_33F;
				case 4:
					goto IL_1E6;
				case 5:
					goto IL_1AA;
				case 7:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("堹嬻紽⼿⹁⭃㑅", a_)))
					{
						num = 13;
						continue;
					}
					ocolor = new OColor(ExcelColors.BlackCustom);
					this.ᜀ(A_0, ocolor);
					num = 22;
					continue;
				}
				case 8:
					if (ocolor != null)
					{
						num = 1;
						continue;
					}
					xlsFill.PatternColorObject.SetKnownColor(ExcelColors.BlackCustom);
					num = 30;
					continue;
				case 9:
					xlsFill.Pattern = spr\u2306.ᜋ(A_0.Value);
					num = 23;
					continue;
				case 10:
					A_0.Read();
					num = 5;
					continue;
				case 11:
					if (A_1)
					{
						num = 26;
						continue;
					}
					goto IL_33F;
				case 12:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 25;
						continue;
					}
					goto IL_1EB;
				}
				case 13:
					num = 24;
					continue;
				case 14:
					goto IL_457;
				case 15:
					num = 16;
					continue;
				case 16:
					goto IL_1EB;
				case 17:
					if (A_0.LocalName != RecordTableEnumerator.b("䨹崻䨽㐿❁㙃⡅็⍉⁋≍", a_))
					{
						num = 32;
						continue;
					}
					xlsFill = new XlsFill();
					num = 33;
					continue;
				case 18:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 34;
						continue;
					}
					goto IL_1EB;
				case 19:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 20;
						continue;
					}
					num = 18;
					continue;
				case 20:
					num = 36;
					continue;
				case 21:
					goto IL_104;
				case 22:
					goto IL_1EB;
				case 23:
					goto IL_12E;
				case 24:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("尹嬻紽⼿⹁⭃㑅", a_)))
					{
						num = 15;
						continue;
					}
					ocolor2 = new OColor((ExcelColors)65);
					this.ᜀ(A_0, ocolor2);
					num = 28;
					continue;
				}
				case 25:
					num = 7;
					continue;
				case 26:
					num = 0;
					continue;
				case 27:
					goto IL_104;
				case 28:
					goto IL_1EB;
				case 29:
					goto IL_457;
				case 30:
					goto IL_260;
				case 31:
					goto IL_D6;
				case 32:
					goto IL_44A;
				case 33:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䨹崻䨽㐿❁㙃⡅᱇㍉㱋⭍", a_)))
					{
						num = 9;
						continue;
					}
					goto IL_12E;
				case 34:
					num = 12;
					continue;
				case 35:
					if (ocolor2 != null)
					{
						num = 37;
						continue;
					}
					xlsFill.OColor.SetKnownColor((ExcelColors)65);
					num = 29;
					continue;
				case 36:
					if (A_0.LocalName == RecordTableEnumerator.b("䨹崻䨽㐿❁㙃⡅็⍉⁋≍", a_))
					{
						num = 10;
						continue;
					}
					goto IL_1AA;
				case 37:
					xlsFill.OColor.ᜀ(ocolor2, true);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_368;
					default:
						if (false)
						{
						}
						num = 14;
						continue;
					}
					break;
				}
				if (A_0 == null)
				{
					num = 31;
					continue;
				}
				num = 17;
				continue;
				IL_104:
				num = 19;
				continue;
				IL_12E:
				A_0.Read();
				ocolor = null;
				ocolor2 = null;
				num = 21;
				continue;
				IL_1AA:
				num = 11;
				continue;
				IL_1EB:
				A_0.Read();
				num = 27;
				continue;
				IL_33F:
				num = 35;
				continue;
				IL_368:
				OColor ocolor3 = ocolor2;
				ocolor2 = ocolor;
				ocolor = ocolor3;
				num = 3;
				continue;
				IL_457:
				num = 8;
			}
			IL_D6:
			throw new ArgumentNullException(RecordTableEnumerator.b("䠹夻弽␿❁㙃", a_));
			IL_1E6:
			IL_260:
			return xlsFill;
			IL_44A:
			if (true)
			{
			}
			throw new XmlException(RecordTableEnumerator.b("漹刻嬽㠿㉁⅃╅㱇⽉⡋湍⑏㍑㍓癕", a_) + A_0.LocalName);
		}
		}
	}

	// Token: 0x06005009 RID: 20489 RVA: 0x00318E7C File Offset: 0x00317E7C
	private static ExcelPatternType ᜋ(string A_0)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return (ExcelPatternType)((XLSXPattern)Enum.Parse(typeof(XLSXPattern), A_0, true));
	}

	// Token: 0x0600500A RID: 20490 RVA: 0x00318ED0 File Offset: 0x00317ED0
	private List<XlsBordersCollection> ᜉ(XmlReader A_0)
	{
		int a_ = 9;
		int num = 4;
		List<XlsBordersCollection> list;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 5;
				continue;
			case 1:
				num = 14;
				continue;
			case 2:
				goto IL_1B1;
			case 3:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 1;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return list;
				default:
					if (false)
					{
					}
					A_0.Read();
					num = 8;
					continue;
				}
				break;
			case 5:
				if (A_0.LocalName != RecordTableEnumerator.b("崾⹀ㅂ⅄≆㭈㡊", a_))
				{
					num = 2;
					continue;
				}
				A_0.Read();
				num = 10;
				continue;
			case 6:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 9;
					continue;
				}
				num = 3;
				continue;
			case 7:
			{
				XlsBordersCollection item = this.ᜈ(A_0);
				list.Add(item);
				num = 13;
				continue;
			}
			case 8:
				goto IL_132;
			case 9:
				goto IL_155;
			case 10:
				goto IL_132;
			case 11:
				goto IL_60;
			case 12:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 0;
					continue;
				}
				goto IL_1EC;
			case 13:
				goto IL_132;
			case 14:
				if (A_0.LocalName == RecordTableEnumerator.b("崾⹀ㅂ⅄≆㭈", a_))
				{
					num = 7;
					continue;
				}
				goto IL_113;
			}
			if (A_0 == null)
			{
				num = 11;
				continue;
			}
			list = new List<XlsBordersCollection>();
			num = 12;
			continue;
			IL_132:
			num = 6;
		}
		IL_60:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴾⑀≂⅄≆㭈", a_));
		IL_113:
		throw new XmlException(RecordTableEnumerator.b("樾⽀♂㵄㝆ⱈ⡊㥌⩎㕐獒ⵔ㩖㕘筚⥜㹞٠䍢", a_) + A_0.LocalName);
		IL_155:
		if (true)
		{
		}
		return list;
		IL_1B1:
		IL_1EC:
		throw new XmlException(RecordTableEnumerator.b("樾⽀♂㵄㝆ⱈ⡊㥌⩎㕐獒ⵔ㩖㕘筚⥜㹞٠䍢", a_) + A_0.LocalName);
	}

	// Token: 0x0600500B RID: 20491 RVA: 0x003190EC File Offset: 0x003180EC
	private XlsBordersCollection ᜈ(XmlReader A_0)
	{
		int a_ = 1;
		XlsBordersCollection xlsBordersCollection;
		for (;;)
		{
			IL_09:
			switch (0)
			{
			default:
			{
				int num = 20;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_12A;
					case 1:
						goto IL_1B2;
					case 2:
						goto IL_264;
					case 3:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 18;
							continue;
						}
						goto IL_12A;
					case 4:
						num = 7;
						continue;
					case 5:
						if (A_0.MoveToAttribute(RecordTableEnumerator.b("匶倸娺娼倾⽀≂⥄ቆ㥈", a_)))
						{
							num = 9;
							continue;
						}
						goto IL_269;
					case 6:
						if (A_0.IsEmptyElement)
						{
							num = 23;
							continue;
						}
						A_0.Read();
						num = 24;
						continue;
					case 7:
					{
						if (A_0.LocalName != RecordTableEnumerator.b("唶嘸䤺夼娾㍀", a_))
						{
							num = 19;
							continue;
						}
						xlsBordersCollection = new XlsBordersCollection(this.ᜉ.AppImplementation, this.ᜉ, true);
						bool a_2 = false;
						bool a_3 = false;
						num = 5;
						continue;
					}
					case 8:
					{
						XLSXBorderIndex xlsxborderIndex;
						if (xlsxborderIndex == XLSXBorderIndex.diagonal)
						{
							num = 22;
							continue;
						}
						BordersLineType a_4 = (BordersLineType)xlsxborderIndex;
						sprᡦ sprᡦ;
						xlsBordersCollection.ᜀ(a_4, sprᡦ);
						num = 0;
						continue;
					}
					case 9:
					{
						bool a_2 = XmlConvert.ToBoolean(A_0.Value);
						num = 15;
						continue;
					}
					case 10:
					{
						bool a_3 = XmlConvert.ToBoolean(A_0.Value);
						num = 1;
						continue;
					}
					case 11:
						goto IL_BF;
					case 12:
						goto IL_106;
					case 13:
						A_0.Read();
						num = 2;
						continue;
					case 14:
						goto IL_18B;
					case 15:
						goto IL_269;
					case 16:
						goto IL_12A;
					case 17:
						if (A_0.NodeType == XmlNodeType.EndElement)
						{
							num = 13;
							continue;
						}
						num = 3;
						continue;
					case 18:
					{
						XLSXBorderIndex xlsxborderIndex;
						sprᡦ sprᡦ = this.ᜀ(A_0, out xlsxborderIndex);
						num = 8;
						continue;
					}
					case 19:
						goto IL_2E6;
					case 21:
						if (A_0.MoveToAttribute(RecordTableEnumerator.b("匶倸娺娼倾⽀≂⥄͆♈㱊⍌", a_)))
						{
							num = 10;
							continue;
						}
						goto IL_1B2;
					case 22:
					{
						sprᡦ sprᡦ;
						sprᡦ sprᡦ2 = (sprᡦ)sprᡦ.ᜄ();
						bool a_2;
						sprᡦ.ᜀ(a_2);
						bool a_3;
						sprᡦ2.ᜀ(a_3);
						xlsBordersCollection.ᜀ(BordersLineType.DiagonalUp, sprᡦ);
						xlsBordersCollection.ᜀ(BordersLineType.DiagonalDown, sprᡦ2);
						num = 16;
						continue;
					}
					case 23:
						A_0.Read();
						num = 12;
						continue;
					case 24:
						goto IL_18B;
					case 25:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 4;
							continue;
						}
						goto IL_10B;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					default:
						if (false)
						{
						}
						if (A_0 == null)
						{
							num = 11;
							continue;
						}
						num = 25;
						continue;
					}
					IL_12A:
					A_0.Read();
					num = 14;
					continue;
					IL_18B:
					num = 17;
					continue;
					IL_1B2:
					num = 6;
					continue;
					IL_269:
					num = 21;
				}
				break;
			}
			}
		}
		IL_BF:
		throw new ArgumentNullException(RecordTableEnumerator.b("䔶尸娺夼娾㍀", a_));
		IL_106:
		return xlsBordersCollection;
		IL_10B:
		throw new XmlException(RecordTableEnumerator.b("戶圸帺䔼伾⑀⁂ㅄ≆ⵈ歊㕌≎㵐獒⅔㙖㹘筚", a_) + A_0.LocalName);
		IL_264:
		return xlsBordersCollection;
		IL_2E6:
		if (true)
		{
		}
		goto IL_10B;
	}

	// Token: 0x0600500C RID: 20492 RVA: 0x00319480 File Offset: 0x00318480
	private sprᡦ ᜀ(XmlReader A_0, out XLSXBorderIndex A_1)
	{
		int a_ = 3;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				A_0.Read();
				num = 14;
				continue;
			case 1:
			{
				sprᡦ sprᡦ;
				return sprᡦ;
			}
			case 2:
			{
				if (A_0.NodeType != XmlNodeType.Element)
				{
					num = 11;
					continue;
				}
				A_1 = (XLSXBorderIndex)Enum.Parse(typeof(XLSXBorderIndex), A_0.LocalName, true);
				sprᡦ sprᡦ = new sprᡦ();
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_199;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			}
			case 3:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䨸伺䐼匾⑀", a_)))
				{
					num = 7;
					continue;
				}
				goto IL_1CC;
			case 5:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 8;
					continue;
				}
				goto IL_EA;
			case 6:
				if (A_0.LocalName == RecordTableEnumerator.b("娸吺儼倾㍀", a_))
				{
					if (true)
					{
					}
					num = 10;
					continue;
				}
				goto IL_EA;
			case 7:
			{
				XLSXBorderLineStyle a_2 = (XLSXBorderLineStyle)Enum.Parse(typeof(XLSXBorderLineStyle), A_0.Value, true);
				sprᡦ sprᡦ;
				sprᡦ.ᜀ((LineStyleType)a_2);
				num = 13;
				continue;
			}
			case 8:
				num = 6;
				continue;
			case 9:
				goto IL_146;
			case 10:
			{
				sprᡦ sprᡦ;
				this.ᜀ(A_0, sprᡦ.ᜅ());
				num = 17;
				continue;
			}
			case 11:
				goto IL_120;
			case 12:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 1;
					continue;
				}
				num = 5;
				continue;
			case 13:
				goto IL_199;
			case 14:
				goto IL_146;
			case 15:
				goto IL_6F;
			case 16:
			{
				if (!A_0.IsEmptyElement)
				{
					num = 0;
					continue;
				}
				sprᡦ sprᡦ;
				return sprᡦ;
			}
			case 17:
				goto IL_EA;
			}
			if (A_0 == null)
			{
				num = 15;
				continue;
			}
			num = 2;
			continue;
			IL_EA:
			A_0.Read();
			num = 9;
			continue;
			IL_146:
			num = 12;
			continue;
			IL_1CC:
			num = 16;
			continue;
			IL_199:
			goto IL_1CC;
		}
		IL_6F:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬸帺尼嬾⑀ㅂ", a_));
		IL_120:
		throw new XmlException(RecordTableEnumerator.b("永唺堼䜾ㅀ♂♄㍆ⱈ⽊浌ⅎ㹐㝒ご睖ⵘ≚ⵜ㩞䅠", a_) + A_0.NodeType);
	}

	// Token: 0x0600500D RID: 20493 RVA: 0x00319700 File Offset: 0x00318700
	private int ᜀ(XmlReader A_0, IInternalWorksheet A_1, List<int> A_2, string A_3, int A_4)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			int num = 9;
			bool a_2;
			sprᱧ sprᱧ;
			for (;;)
			{
				int num2;
				int num3;
				int num4;
				int num5;
				double num6;
				switch (num)
				{
				case 0:
					A_0.Read();
					num = 55;
					continue;
				case 1:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("嘸为䤼匾⡀ⵂ⁄୆ⱈ㵊⡌⍎", a_)))
					{
						num = 47;
						continue;
					}
					goto IL_3FC;
				case 2:
					goto IL_3C9;
				case 3:
					if (A_1.FirstRow > num2)
					{
						num = 5;
						continue;
					}
					goto IL_61A;
				case 4:
					goto IL_3FC;
				case 5:
					goto IL_4C5;
				case 6:
					goto IL_126;
				case 7:
					if (A_0.LocalName != RecordTableEnumerator.b("䬸吺䨼", a_))
					{
						num = 26;
						continue;
					}
					num2 = 0;
					num3 = this.ᜉ.DefaultXFIndex;
					num4 = A_1.DefaultPrintRowHeight;
					a_2 = false;
					num = 18;
					continue;
				case 8:
					goto IL_754;
				case 10:
					goto IL_2B2;
				case 11:
					sprᱧ.ᜅ(XmlConvert.ToBoolean(A_0.Value));
					num = 53;
					continue;
				case 12:
					sprᱧ.ᜄ(XmlConvert.ToBoolean(A_0.Value));
					num = 45;
					continue;
				case 13:
					a_2 = XmlConvert.ToBoolean(A_0.Value);
					num = 21;
					continue;
				case 14:
					if (A_1.FirstRow >= 0)
					{
						num = 24;
						continue;
					}
					goto IL_4C5;
				case 15:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䴸区吼尾⩀ł⩄㍆", a_)))
					{
						num = 12;
						continue;
					}
					goto IL_1DF;
				case 16:
					goto IL_2D7;
				case 17:
					num2 = (A_4 = XmlConvert.ToInt32(A_0.Value));
					num = 48;
					continue;
				case 18:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䬸", a_)))
					{
						num = 17;
						continue;
					}
					num2 = A_4;
					num = 19;
					continue;
				case 19:
					goto IL_171;
				case 20:
					if (!A_0.IsEmptyElement)
					{
						num = 0;
						continue;
					}
					goto IL_78F;
				case 21:
					goto IL_4DD;
				case 22:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 16;
						continue;
					}
					num = 42;
					continue;
				case 23:
					goto IL_36E;
				case 24:
					num = 3;
					continue;
				case 25:
					goto IL_70A;
				case 26:
					goto IL_6D2;
				case 27:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("娸为丼䬾⹀⹂ൄ≆⁈ⱊ╌㭎", a_)))
					{
						num = 35;
						continue;
					}
					goto IL_3C9;
				case 28:
					num5 = this.ᜀ(A_0, A_1, A_2, num2, num5);
					num5++;
					num = 56;
					continue;
				case 29:
					goto IL_244;
				case 30:
					goto IL_510;
				case 31:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䴸区吼尾⩀ᝂ⩄㝆", a_)))
					{
						num = 43;
						continue;
					}
					goto IL_754;
				case 32:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("儸伺", a_)))
					{
						num = 46;
						continue;
					}
					goto IL_244;
				case 33:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("娸为丼䬾⹀⹂̈́⡆㭈♊ⱌ㭎", a_)))
					{
						num = 13;
						continue;
					}
					a_2 = false;
					num = 34;
					continue;
				case 34:
					goto IL_4DD;
				case 35:
					sprᱧ.ᜊ(XmlConvert.ToBoolean(A_0.Value));
					num = 2;
					continue;
				case 36:
					A_1.LastRow = num2;
					num = 23;
					continue;
				case 37:
					if (num6 > 409.5)
					{
						num = 52;
						continue;
					}
					goto IL_70A;
				case 38:
					sprᱧ.ᜁ(XmlConvert.ToBoolean(A_0.Value));
					num = 30;
					continue;
				case 39:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䨸", a_)))
					{
						num = 51;
						continue;
					}
					goto IL_585;
				case 40:
					goto IL_585;
				case 41:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("娸吺儼匾⁀㍂㙄≆ⵈ", a_)))
					{
						num = 38;
						continue;
					}
					goto IL_510;
				case 42:
					if (A_0.LocalName == A_3)
					{
						num = 28;
						continue;
					}
					goto IL_6F3;
				case 43:
					sprᱧ.ᜂ(XmlConvert.ToBoolean(A_0.Value));
					num = 8;
					continue;
				case 44:
					if (A_1 == null)
					{
						num = 57;
						continue;
					}
					num = 7;
					continue;
				case 45:
					goto IL_1DF;
				case 46:
					num6 = XmlConvert.ToDouble(A_0.Value);
					goto IL_650;
				case 47:
					sprᱧ.ᜂ(XmlConvert.ToUInt16(A_0.Value));
					num = 4;
					continue;
				case 48:
					goto IL_171;
				case 49:
					if (A_1.LastRow < num2)
					{
						num = 36;
						continue;
					}
					goto IL_36E;
				case 50:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("儸刺夼嬾⑀ⵂ", a_)))
					{
						num = 11;
						continue;
					}
					goto IL_432;
				case 51:
					num3 = A_2[XmlConvert.ToInt32(A_0.Value)];
					num = 40;
					continue;
				case 52:
					num6 = 409.5;
					num = 25;
					continue;
				case 53:
					goto IL_432;
				case 54:
					goto IL_61A;
				case 55:
					goto IL_2B2;
				case 56:
					goto IL_6F3;
				case 57:
					goto IL_5D6;
				}
				if (A_0 == null)
				{
					num = 6;
					continue;
				}
				num = 44;
				continue;
				IL_171:
				sprᱧ = sprᜑ.ᜀ(A_1, num2 - 1, true);
				num = 41;
				continue;
				IL_1DF:
				num = 31;
				continue;
				IL_244:
				num = 39;
				continue;
				IL_2B2:
				num = 22;
				continue;
				IL_36E:
				A_0.MoveToElement();
				num5 = 1;
				num = 20;
				continue;
				IL_3C9:
				num = 1;
				continue;
				IL_3FC:
				num = 32;
				continue;
				IL_432:
				num = 15;
				continue;
				IL_4C5:
				A_1.FirstRow = num2;
				num = 54;
				continue;
				IL_4DD:
				num = 27;
				continue;
				IL_510:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_650:
					num = 37;
					continue;
				default:
					if (false)
					{
					}
					num = 33;
					continue;
				}
				IL_585:
				num = 50;
				continue;
				IL_61A:
				num = 49;
				continue;
				IL_6F3:
				A_0.Skip();
				num = 10;
				continue;
				IL_70A:
				num4 = (int)(num6 * 20.0);
				sprᱧ.ᜃ(true);
				num = 29;
				continue;
				IL_754:
				sprᱧ.ᜀ((ushort)num3);
				sprᱧ.ᜃ((ushort)num4);
				num = 14;
			}
			IL_126:
			throw new ArgumentNullException(RecordTableEnumerator.b("䬸帺尼嬾⑀ㅂ", a_));
			IL_2D7:
			goto IL_78F;
			IL_5D6:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䨸区堼娾㕀", a_));
			IL_6D2:
			throw new XmlException(RecordTableEnumerator.b("䬸帺尼嬾⑀ㅂ", a_));
			IL_78F:
			sprᱧ.ᜇ(a_2);
			return A_4;
		}
		}
	}

	// Token: 0x0600500E RID: 20494 RVA: 0x00319EA8 File Offset: 0x00318EA8
	private int ᜀ(XmlReader A_0, IInternalWorksheet A_1, List<int> A_2, int A_3, int A_4)
	{
		int a_ = 17;
		switch (0)
		{
		default:
		{
			int num = 14;
			int num3;
			for (;;)
			{
				bool flag;
				int num5;
				SSTDictionary sstdictionary;
				switch (num)
				{
				case 0:
				{
					bool isEmptyElement;
					if (!isEmptyElement)
					{
						num = 4;
						continue;
					}
					goto IL_1D2;
				}
				case 1:
				{
					spr\u1B7A.CellType cellType = spr\u2306.ᜊ(A_0.Value);
					num = 36;
					continue;
				}
				case 2:
				{
					if (A_1 == null)
					{
						num = 39;
						continue;
					}
					flag = false;
					int num2 = A_3;
					num3 = A_4;
					int num4 = this.ᜉ.DefaultXFIndex;
					spr\u1B7A.CellType cellType = spr\u1B7A.CellType.n;
					XlsCellRecordCollection cellRecords = A_1.CellRecords;
					num = 17;
					continue;
				}
				case 3:
					goto IL_265;
				case 4:
					A_0.Read();
					num = 22;
					continue;
				case 5:
					A_0.Read();
					num = 47;
					continue;
				case 6:
					goto IL_22A;
				case 7:
					goto IL_36A;
				case 8:
					goto IL_FE;
				case 9:
					goto IL_5E0;
				case 10:
				{
					int num4 = A_2[XmlConvert.ToInt32(A_0.Value)];
					num = 3;
					continue;
				}
				case 11:
					return num3;
				case 12:
					goto IL_166;
				case 13:
				{
					bool isEmptyElement = A_0.IsEmptyElement;
					num = 0;
					continue;
				}
				case 15:
					goto IL_416;
				case 16:
					if (A_0.LocalName == RecordTableEnumerator.b("⹆㩈", a_))
					{
						num = 18;
						continue;
					}
					goto IL_22A;
				case 17:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㕆", a_)))
					{
						num = 41;
						continue;
					}
					goto IL_36A;
				case 18:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_356;
					default:
					{
						if (false)
						{
						}
						string empty = string.Empty;
						num5 = this.ᜀ(A_0, out empty);
						sstdictionary = this.ᜉ.InnerSST;
						int num2;
						int num4;
						XlsCellRecordCollection cellRecords;
						cellRecords.SetSingleStringValue(num2, num3, num4, num5);
						num = 45;
						continue;
					}
					}
					break;
				case 19:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㍆", a_)))
					{
						num = 1;
						continue;
					}
					goto IL_3BD;
				case 20:
				{
					int num2;
					if (sprᜑ.ᜀ(A_1, num2, num3))
					{
						num = 25;
						continue;
					}
					spr\u1B7A.CellType cellType;
					int num4;
					XlsCellRecordCollection cellRecords;
					this.ᜀ(cellType, A_0.Value, cellRecords, num2, num3, num4);
					num = 15;
					continue;
				}
				case 21:
				{
					XlsCellRecordCollection cellRecords;
					if (cellRecords.sheet is XlsExternWorksheet)
					{
						num = 30;
						continue;
					}
					int num2;
					int num4;
					cellRecords.SetBlank(num2, num3, num4);
					num = 27;
					continue;
				}
				case 22:
					goto IL_1D2;
				case 23:
					goto IL_568;
				case 24:
					A_0.Skip();
					num = 12;
					continue;
				case 25:
				{
					spr\u1B7A.CellType cellType;
					int num2;
					this.ᜀ(A_1, cellType, A_0.Value, num2, num3);
					num = 33;
					continue;
				}
				case 26:
				{
					bool isEmptyElement;
					if (!isEmptyElement)
					{
						num = 24;
						continue;
					}
					goto IL_166;
				}
				case 27:
					goto IL_2DE;
				case 28:
					if (A_0.LocalName == RecordTableEnumerator.b("ㅆ", a_))
					{
						num = 13;
						continue;
					}
					goto IL_166;
				case 29:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㑆", a_)))
					{
						num = 10;
						continue;
					}
					goto IL_265;
				case 30:
				{
					int num2;
					int num4;
					XlsCellRecordCollection cellRecords;
					this.ᜀ(spr\u1B7A.CellType.n, RecordTableEnumerator.b("睆", a_), cellRecords, num2, num3, num4);
					num = 11;
					continue;
				}
				case 31:
					goto IL_3EC;
				case 32:
					if (A_0.LocalName == RecordTableEnumerator.b("ⅆ", a_))
					{
						num = 35;
						continue;
					}
					goto IL_5E0;
				case 33:
					goto IL_416;
				case 34:
					if (!flag)
					{
						num = 38;
						continue;
					}
					return num3;
				case 35:
				{
					int num2;
					int num4;
					this.ᜀ(A_0, A_1, num2, num3, num4);
					flag = true;
					num = 9;
					continue;
				}
				case 36:
					goto IL_3BD;
				case 37:
					if (!A_0.IsEmptyElement)
					{
						num = 5;
						continue;
					}
					goto IL_29B;
				case 38:
					num = 21;
					continue;
				case 39:
					goto IL_5BA;
				case 40:
					if (!this.ᜉ.HasInlineStrings)
					{
						num = 44;
						continue;
					}
					goto IL_568;
				case 41:
				{
					int num2;
					sprṔ.ᜀ(A_0.Value, out num2, out num3);
					num = 7;
					continue;
				}
				case 42:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 46;
						continue;
					}
					num = 16;
					continue;
				case 43:
				{
					int num2;
					string key = sprṔ.ᜂ(num3, num2);
					string empty;
					this.ᜁ().InlineStrings.Add(key, empty);
					num = 40;
					continue;
				}
				case 44:
					this.ᜉ.HasInlineStrings = true;
					num = 23;
					continue;
				case 45:
				{
					string empty;
					if (empty != null)
					{
						num = 43;
						continue;
					}
					goto IL_568;
				}
				case 46:
					goto IL_29B;
				case 47:
					goto IL_3EC;
				}
				if (A_0 == null)
				{
					num = 8;
					continue;
				}
				num = 2;
				continue;
				IL_166:
				A_0.Skip();
				num = 31;
				continue;
				IL_1D2:
				num = 20;
				continue;
				IL_22A:
				num = 32;
				continue;
				IL_265:
				num = 19;
				continue;
				IL_29B:
				num = 34;
				continue;
				IL_36A:
				num = 29;
				continue;
				IL_3BD:
				A_0.MoveToElement();
				num = 37;
				continue;
				IL_3EC:
				num = 42;
				continue;
				IL_416:
				flag = true;
				num = 26;
				continue;
				IL_568:
				sstdictionary.RemoveDecrease(num5);
				flag = true;
				num = 6;
				continue;
				IL_5E0:
				num = 28;
			}
			IL_FE:
			goto IL_356;
			IL_2DE:
			if (true)
			{
			}
			return num3;
			IL_356:
			throw new ArgumentNullException(RecordTableEnumerator.b("㕆ⱈ⩊⥌⩎⍐", a_));
			IL_5BA:
			throw new ArgumentNullException(RecordTableEnumerator.b("㑆ⅈ⹊⡌㭎", a_));
		}
		}
	}

	// Token: 0x0600500F RID: 20495 RVA: 0x0031A4FC File Offset: 0x003194FC
	private static spr\u1B7A.CellType ᜊ(string A_0)
	{
		int a_ = 2;
		if (true)
		{
		}
		int num = 14;
		spr\u1B7A.CellType result;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return result;
			case 1:
				goto IL_8F;
			case 2:
				goto IL_80;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					goto IL_94;
				}
				break;
			case 4:
				goto IL_D0;
			case 5:
				spr\u22D2.ង = new Dictionary<string, int>(6)
				{
					{
						RecordTableEnumerator.b("娷", a_),
						0
					},
					{
						RecordTableEnumerator.b("崷", a_),
						1
					},
					{
						RecordTableEnumerator.b("儷吹倻圽⸿❁ᝃ㉅㩇", a_),
						2
					},
					{
						RecordTableEnumerator.b("嘷", a_),
						3
					},
					{
						RecordTableEnumerator.b("䬷", a_),
						4
					},
					{
						RecordTableEnumerator.b("䬷丹主", a_),
						5
					}
				};
				num = 3;
				continue;
			case 6:
				num = 7;
				continue;
			case 7:
				if (spr\u22D2.ង == null)
				{
					num = 5;
					continue;
				}
				goto IL_94;
			case 8:
			{
				int num2;
				switch (num2)
				{
				case 0:
					result = spr\u1B7A.CellType.b;
					num = 15;
					continue;
				case 1:
					result = spr\u1B7A.CellType.e;
					num = 1;
					continue;
				case 2:
					result = spr\u1B7A.CellType.inlineStr;
					num = 9;
					continue;
				case 3:
					result = spr\u1B7A.CellType.n;
					num = 2;
					continue;
				case 4:
					result = spr\u1B7A.CellType.s;
					num = 0;
					continue;
				case 5:
					result = spr\u1B7A.CellType.str;
					num = 4;
					continue;
				default:
					num = 11;
					continue;
				}
				break;
			}
			case 9:
				goto IL_199;
			case 10:
			{
				int num2;
				if (spr\u22D2.ង.TryGetValue(A_0, out num2))
				{
					num = 13;
					continue;
				}
				goto IL_19E;
			}
			case 11:
				num = 12;
				continue;
			case 12:
				goto IL_1AF;
			case 13:
				num = 8;
				continue;
			case 15:
				return result;
			}
			IL_61:
			if (A_0 != null)
			{
				num = 6;
				continue;
			}
			goto IL_19E;
			goto IL_61;
			IL_94:
			num = 10;
		}
		IL_80:
		IL_8F:
		IL_D0:
		IL_199:
		return result;
		IL_19E:
		throw new ArgumentOutOfRangeException();
		IL_1AF:
		goto IL_19E;
	}

	// Token: 0x06005010 RID: 20496 RVA: 0x0031A744 File Offset: 0x00319744
	private void ᜀ(XmlReader A_0, IInternalWorksheet A_1, int A_2, int A_3, int A_4)
	{
		int a_ = 17;
		switch (0)
		{
		default:
		{
			int num = 18;
			for (;;)
			{
				string text;
				bool flag;
				uint a_2;
				string a_3;
				XLSXFormulaType xlsxformulaType;
				XLSXFormulaType xlsxformulaType2;
				switch (num)
				{
				case 0:
					if (A_0.NodeType != XmlNodeType.EndElement)
					{
						num = 6;
						continue;
					}
					goto IL_3AF;
				case 1:
				{
					spr᱒ spr᱒ = (spr᱒)spr\u175E.ᜀ(TBIFFRecord.Formula);
					spr᱒.ᜁ(this.ᜊ.ᜀ(text, A_1, null));
					spr᱒.ᜇ(A_2 - 1);
					spr᱒.ᜆ(A_3 - 1);
					spr᱒.ᜁ((ushort)A_4);
					spr᱒.ᜃ(flag);
					XlsCellRecordCollection cellRecords;
					cellRecords.ᜁ(A_2, A_3, spr᱒);
					num = 28;
					continue;
				}
				case 2:
					a_2 = XmlConvert.ToUInt32(A_0.Value);
					num = 23;
					continue;
				case 3:
				{
					if (A_0.LocalName != RecordTableEnumerator.b("ⅆ", a_))
					{
						num = 20;
						continue;
					}
					a_3 = null;
					text = RecordTableEnumerator.b("穆", a_);
					a_2 = 0U;
					xlsxformulaType = XLSXFormulaType.normal;
					XlsCellRecordCollection cellRecords = A_1.CellRecords;
					flag = false;
					num = 11;
					continue;
				}
				case 4:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㕆ⱈⵊ", a_)))
					{
						num = 27;
						continue;
					}
					goto IL_416;
				case 5:
					goto IL_363;
				case 6:
					text += A_0.Value;
					A_0.Skip();
					num = 15;
					continue;
				case 7:
					goto IL_416;
				case 8:
					if (!A_0.IsEmptyElement)
					{
						num = 10;
						continue;
					}
					goto IL_3AF;
				case 9:
					goto IL_2A2;
				case 10:
					A_0.Read();
					num = 0;
					continue;
				case 11:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㍆", a_)))
					{
						num = 24;
						continue;
					}
					goto IL_363;
				case 12:
					flag = XmlConvert.ToBoolean(A_0.Value);
					num = 14;
					continue;
				case 13:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_280;
					default:
						goto IL_2DA;
					}
					break;
				case 14:
					goto IL_449;
				case 15:
					goto IL_3AF;
				case 16:
					goto IL_3AA;
				case 17:
					if (A_1 == null)
					{
						num = 29;
						continue;
					}
					num = 3;
					continue;
				case 19:
					if (text.Length > 0)
					{
						num = 1;
						continue;
					}
					goto IL_49B;
				case 20:
					goto IL_334;
				case 21:
					if (true)
					{
					}
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㑆⁈", a_)))
					{
						num = 2;
						continue;
					}
					goto IL_3E3;
				case 22:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("⑆⡈", a_)))
					{
						num = 12;
						continue;
					}
					goto IL_449;
				case 23:
					goto IL_3E3;
				case 24:
					xlsxformulaType = (XLSXFormulaType)Enum.Parse(typeof(XLSXFormulaType), A_0.Value, false);
					num = 5;
					continue;
				case 25:
					goto IL_1A6;
				case 26:
					switch (xlsxformulaType2)
					{
					case XLSXFormulaType.array:
						this.ᜀ(A_1 as XlsWorksheet, text, a_3, A_4);
						num = 25;
						continue;
					case XLSXFormulaType.dataTable:
						goto IL_49B;
					case XLSXFormulaType.normal:
						text = UtilityMethods.ᜀ(text);
						num = 19;
						continue;
					case XLSXFormulaType.shared:
						goto IL_280;
					default:
						num = 30;
						continue;
					}
					break;
				case 27:
					a_3 = A_0.Value;
					num = 7;
					continue;
				case 28:
					goto IL_27B;
				case 29:
					goto IL_496;
				case 30:
					num = 16;
					continue;
				}
				if (A_0 == null)
				{
					num = 13;
					continue;
				}
				num = 17;
				continue;
				IL_280:
				this.ᜀ(A_1 as XlsWorksheet, text, a_3, a_2, A_2, A_3, A_4, flag);
				num = 9;
				continue;
				IL_363:
				num = 21;
				continue;
				IL_3AF:
				xlsxformulaType2 = xlsxformulaType;
				num = 26;
				continue;
				IL_3E3:
				num = 4;
				continue;
				IL_416:
				num = 22;
				continue;
				IL_449:
				A_0.MoveToElement();
				num = 8;
			}
			IL_1A6:
			IL_27B:
			IL_2A2:
			goto IL_49B;
			IL_2DA:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㕆ⱈ⩊⥌⩎⍐", a_));
			IL_334:
			throw new XmlException(RecordTableEnumerator.b("㕆ⱈ⩊⥌⩎⍐", a_));
			IL_3AA:
			goto IL_49B;
			IL_496:
			throw new ArgumentNullException(RecordTableEnumerator.b("㑆ⅈ⹊⡌㭎", a_));
			IL_49B:
			A_0.Skip();
			return;
		}
		}
	}

	// Token: 0x06005011 RID: 20497 RVA: 0x0031ABF4 File Offset: 0x00319BF4
	private void ᜇ(XmlReader A_0)
	{
		int a_ = 2;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				num = 13;
				continue;
			case 2:
				goto IL_163;
			case 3:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 6;
					continue;
				}
				goto IL_190;
			case 4:
				return;
			case 5:
				goto IL_105;
			case 6:
				num = 12;
				continue;
			case 7:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 4;
					continue;
				}
				num = 10;
				continue;
			case 8:
				goto IL_1F1;
			case 9:
				goto IL_63;
			case 10:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 1;
					continue;
				}
				goto IL_105;
			case 11:
			{
				A_0.MoveToAttribute(RecordTableEnumerator.b("䨷崹帻", a_));
				int a_2 = int.Parse(A_0.Value, NumberStyles.HexNumber);
				Color color = spr\u1D39.ᜀ(a_2);
				int num2;
				this.ᜉ.SetPaletteColor(num2, color);
				num2++;
				num = 5;
				continue;
			}
			case 12:
			{
				if (A_0.LocalName != RecordTableEnumerator.b("儷吹堻嬽㠿❁⁃Յ❇♉⍋㱍⍏", a_))
				{
					num = 8;
					continue;
				}
				A_0.Read();
				int num2 = 0;
				num = 14;
				continue;
			}
			case 13:
				if (A_0.LocalName == RecordTableEnumerator.b("䨷崹帻紽⼿⹁⭃㑅", a_))
				{
					num = 11;
					continue;
				}
				goto IL_105;
			case 14:
				goto IL_163;
			}
			if (A_0 == null)
			{
				num = 9;
				continue;
			}
			num = 3;
			continue;
			IL_105:
			A_0.Read();
			num = 2;
			continue;
			IL_163:
			if (true)
			{
			}
			num = 7;
		}
		IL_63:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹崻娽┿ぁ", a_));
		IL_190:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_1F1:
			goto IL_190;
		default:
			if (false)
			{
			}
			throw new XmlException(RecordTableEnumerator.b("笷嬹刻倽⼿㙁摃⩅❇⥉ⵋ㩍㕏牑⁓㝕㽗穙㕛そџݡᱣͥ౧⥩ͫɭὯqݳ", a_));
		}
	}

	// Token: 0x06005012 RID: 20498 RVA: 0x0031AE10 File Offset: 0x00319E10
	private void ᜆ(XmlReader A_0)
	{
		int a_ = 1;
		for (;;)
		{
			IL_09:
			int num = 11;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!A_0.IsEmptyElement)
					{
						num = 15;
						continue;
					}
					return;
				case 1:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 3;
						continue;
					}
					goto IL_FF;
				case 2:
					if (A_0.LocalName != RecordTableEnumerator.b("吶嘸场刼䴾㉀", a_))
					{
						num = 16;
						continue;
					}
					num = 0;
					continue;
				case 3:
					num = 8;
					continue;
				case 4:
					num = 2;
					continue;
				case 5:
					goto IL_15B;
				case 6:
					goto IL_FF;
				case 7:
					goto IL_70;
				case 8:
					if (A_0.LocalName == RecordTableEnumerator.b("帶圸强堼䜾⑀❂ل⡆╈⑊㽌㱎", a_))
					{
						num = 14;
						continue;
					}
					goto IL_FF;
				case 9:
					goto IL_13B;
				case 10:
					goto IL_13B;
				case 12:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 4;
						continue;
					}
					goto IL_1EA;
				case 13:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 5;
						continue;
					}
					num = 1;
					continue;
				case 14:
					this.ᜇ(A_0);
					num = 6;
					continue;
				case 15:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					default:
						if (false)
						{
						}
						A_0.Read();
						num = 10;
						continue;
					}
					break;
				case 16:
					goto IL_1C1;
				}
				if (A_0 == null)
				{
					if (true)
					{
					}
					num = 7;
					continue;
				}
				num = 12;
				continue;
				IL_FF:
				A_0.Read();
				num = 9;
				continue;
				IL_13B:
				num = 13;
			}
		}
		IL_70:
		throw new ArgumentNullException(RecordTableEnumerator.b("䔶尸娺夼娾㍀", a_));
		IL_15B:
		return;
		IL_1C1:
		IL_1EA:
		throw new XmlException(RecordTableEnumerator.b("琶堸唺匼倾㕀捂⥄⡆⩈⩊㥌⩎煐❒㑔ざ祘㡚㉜㍞๠ᅢᙤ", a_));
	}

	// Token: 0x06005013 RID: 20499 RVA: 0x0031B01C File Offset: 0x0031A01C
	private void ᜁ(XmlReader A_0, XlsWorksheet A_1, List<int> A_2)
	{
		int a_ = 3;
		int num = 12;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_CE;
				default:
					if (false)
					{
					}
					if (A_1 == null)
					{
						num = 3;
						continue;
					}
					num = 5;
					continue;
				}
				break;
			case 1:
				this.ᜀ(A_0, A_1, A_2);
				goto IL_CE;
			case 2:
				goto IL_151;
			case 3:
				goto IL_116;
			case 4:
				if (A_0.LocalName == RecordTableEnumerator.b("娸吺儼", a_))
				{
					num = 1;
					continue;
				}
				goto IL_1D2;
			case 5:
				if (A_2 == null)
				{
					num = 10;
					continue;
				}
				num = 13;
				continue;
			case 6:
				goto IL_174;
			case 7:
				num = 14;
				continue;
			case 8:
				goto IL_1D2;
			case 9:
				goto IL_1B2;
			case 10:
				goto IL_1D0;
			case 11:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 16;
					continue;
				}
				goto IL_1D2;
			case 13:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 7;
					continue;
				}
				goto IL_9D;
			case 14:
				if (A_0.LocalName != RecordTableEnumerator.b("娸吺儼䰾", a_))
				{
					num = 9;
					continue;
				}
				A_0.Read();
				num = 15;
				continue;
			case 15:
				goto IL_151;
			case 16:
				num = 4;
				continue;
			case 17:
				goto IL_7A;
			case 18:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 6;
					continue;
				}
				num = 11;
				continue;
			}
			if (A_0 == null)
			{
				num = 17;
				continue;
			}
			num = 0;
			continue;
			IL_CE:
			num = 8;
			continue;
			IL_151:
			num = 18;
			continue;
			IL_1D2:
			A_0.Read();
			num = 2;
		}
		IL_7A:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬸帺尼嬾⑀ㅂ", a_));
		IL_9D:
		throw new XmlException(RecordTableEnumerator.b("永唺尼崾ⵀ♂敄㍆♈歊⅌⁎㉐㉒⅔㉖祘⍚ぜ㍞䅠ᝢѤf䥨ࡪɬͮɰ", a_));
		IL_116:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨸区堼娾㕀", a_));
		IL_174:
		if (true)
		{
		}
		return;
		IL_1B2:
		goto IL_9D;
		IL_1D0:
		throw new ArgumentNullException(RecordTableEnumerator.b("堸䤺似氾㕀㩂⥄≆㩈", a_));
	}

	// Token: 0x06005014 RID: 20500 RVA: 0x0031B278 File Offset: 0x0031A278
	private void ᜀ(XmlReader A_0, XlsWorksheet A_1, List<int> A_2)
	{
		int a_ = 13;
		switch (0)
		{
		default:
		{
			int num = 28;
			spr\u216E spr_u216E;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_335;
				case 1:
					if (A_0.LocalName != RecordTableEnumerator.b("⁂⩄⭆", a_))
					{
						num = 36;
						continue;
					}
					spr_u216E = (spr\u216E)spr\u175E.ᜀ(TBIFFRecord.ColumnInfo);
					num = 40;
					continue;
				case 2:
					spr_u216E.ᜃ(XmlConvert.ToBoolean(A_0.Value));
					num = 0;
					continue;
				case 3:
					spr_u216E.ᜂ(XmlConvert.ToBoolean(A_0.Value));
					num = 19;
					continue;
				case 4:
					goto IL_42D;
				case 5:
					spr_u216E.ᜀ(XmlConvert.ToUInt16(A_0.Value) - 1);
					num = 18;
					continue;
				case 6:
					num = 1;
					continue;
				case 7:
					goto IL_518;
				case 8:
					spr_u216E.ᜀ(XmlConvert.ToBoolean(A_0.Value));
					num = 35;
					continue;
				case 9:
					goto IL_3DE;
				case 10:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("⁂⩄⭆╈⩊㵌㱎㑐㝒", a_)))
					{
						num = 20;
						continue;
					}
					goto IL_230;
				case 11:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("ⱂい㍆╈≊⍌⩎ᵐ㙒⍔㉖㕘", a_)))
					{
						num = 30;
						continue;
					}
					goto IL_571;
				case 12:
					goto IL_230;
				case 13:
					goto IL_56C;
				case 14:
					if (true)
					{
					}
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("⅂⁄㑆㵈ൊ⑌㭎", a_)))
					{
						num = 3;
						continue;
					}
					goto IL_197;
				case 15:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("⭂ⱄ⍆ⵈ⹊⍌", a_)))
					{
						num = 17;
						continue;
					}
					goto IL_5C6;
				case 16:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㍂ⵄ⡆❈⹊㥌♎㉐", a_)))
					{
						num = 8;
						continue;
					}
					goto IL_375;
				case 17:
					spr_u216E.ᜄ(XmlConvert.ToBoolean(A_0.Value));
					num = 32;
					continue;
				case 18:
					goto IL_3A8;
				case 19:
					goto IL_197;
				case 20:
					spr_u216E.ᜁ(XmlConvert.ToBoolean(A_0.Value));
					num = 12;
					continue;
				case 21:
					spr_u216E.ᜄ(XmlConvert.ToUInt16(A_0.Value) - 1);
					num = 9;
					continue;
				case 22:
					goto IL_192;
				case 23:
				{
					double num2 = XmlConvert.ToDouble(A_0.Value);
					int num3 = (int)Math.Round(num2 * 256.0);
					spr_u216E.ᜅ((ushort)num3);
					num = 7;
					continue;
				}
				case 24:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 6;
						continue;
					}
					goto IL_21C;
				case 25:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("⹂⑄㽆", a_)))
					{
						num = 5;
						continue;
					}
					goto IL_3A8;
				case 26:
				{
					int num4 = int.Parse(A_0.Value);
					num4 = A_2[num4];
					spr_u216E.ᜃ((ushort)num4);
					num = 33;
					continue;
				}
				case 27:
					if (A_1 == null)
					{
						num = 13;
						continue;
					}
					num = 29;
					continue;
				case 29:
					if (A_2 == null)
					{
						num = 22;
						continue;
					}
					num = 24;
					continue;
				case 30:
					IL_261:
					spr_u216E.ᜂ(XmlConvert.ToUInt16(A_0.Value));
					num = 38;
					continue;
				case 31:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("あㅄ㹆╈⹊", a_)))
					{
						num = 26;
						continue;
					}
					spr_u216E.ᜃ((ushort)this.ᜉ.DefaultXFIndex);
					num = 4;
					continue;
				case 32:
					goto IL_283;
				case 33:
					goto IL_42D;
				case 34:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㑂ⱄ⍆㵈⍊", a_)))
					{
						num = 23;
						continue;
					}
					goto IL_518;
				case 35:
					goto IL_375;
				case 36:
					goto IL_2F7;
				case 37:
					goto IL_E2;
				case 38:
					goto IL_571;
				case 39:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("⁂い㑆㵈⑊⁌ᡎ㡐㝒⅔㽖", a_)))
					{
						num = 2;
						continue;
					}
					goto IL_335;
				case 40:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("⹂ⱄ⥆", a_)))
					{
						num = 21;
						continue;
					}
					goto IL_3DE;
				}
				if (A_0 == null)
				{
					num = 37;
					continue;
				}
				num = 27;
				continue;
				IL_197:
				num = 16;
				continue;
				IL_230:
				num = 11;
				continue;
				IL_3DE:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_261;
				default:
					if (false)
					{
					}
					num = 25;
					continue;
				}
				IL_335:
				num = 10;
				continue;
				IL_375:
				num = 39;
				continue;
				IL_3A8:
				num = 34;
				continue;
				IL_42D:
				num = 14;
				continue;
				IL_518:
				num = 31;
				continue;
				IL_571:
				num = 15;
			}
			IL_E2:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄♆ⵈ⹊㽌", a_));
			IL_192:
			throw new ArgumentNullException(RecordTableEnumerator.b("≂㝄㕆ᩈ㽊㑌⍎㑐⁒", a_));
			IL_21C:
			throw new XmlException(RecordTableEnumerator.b("ᙂ⭄♆⭈❊⡌潎═㱒畔㭖㙘㡚㱜⭞Ѡ䍢ᵤ੦ը䭪ᥬ๮ᙰ卲ᙴᡶᕸ", a_));
			IL_283:
			goto IL_5C6;
			IL_2F7:
			goto IL_21C;
			IL_56C:
			throw new ArgumentNullException(RecordTableEnumerator.b("あⵄ≆ⱈ㽊", a_));
			IL_5C6:
			A_1.ᜀ(spr_u216E, false);
			return;
		}
		}
	}

	// Token: 0x06005015 RID: 20501 RVA: 0x0031B854 File Offset: 0x0031A854
	private void ᜀ(ref bool A_0, ref XmlWriter A_1, ref StreamWriter A_2, Stream A_3)
	{
		int a_ = 17;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_7C;
				default:
					goto IL_A4;
				}
				break;
			case 2:
				A_0 = true;
				A_1.WriteEndElement();
				A_1.Flush();
				A_2 = new StreamWriter(A_3);
				A_1 = UtilityMethods.ᜀ(A_2);
				A_1.WriteStartElement(RecordTableEnumerator.b("㕆♈⑊㥌", a_), RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒♔㑖ㅘ㹚ぜ㹞በ䵢੤ᝦ౨ժᕬɮᵰᕲᩴնᑸ᩺ॼ౾꾀Ꚉﶌﶎﺚ철쾢誤閦馨鮪鮬肮\udcb0튲\udcb4\ud9b6", a_));
				goto IL_7C;
			}
			if (!A_0)
			{
				num = 2;
				continue;
			}
			return;
			IL_7C:
			num = 0;
		}
		IL_A4:
		if (true)
		{
		}
		if (false)
		{
		}
	}

	// Token: 0x06005016 RID: 20502 RVA: 0x0031B918 File Offset: 0x0031A918
	public void \u1716(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 10;
		int num = 17;
		for (;;)
		{
			spr\u22CB spr_u22CB;
			XlsDataValidationCollection a_2;
			switch (num)
			{
			case 0:
				spr_u22CB.ᜁ(XmlConvert.ToInt32(A_0.Value));
				num = 3;
				continue;
			case 1:
				goto IL_B0;
			case 2:
				goto IL_212;
			case 3:
				goto IL_8C;
			case 4:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("␿⭁㝃❅⩇♉⥋ṍ≏㵑㥓♕ⱗ⥙", a_)))
				{
					goto IL_26E;
				}
				goto IL_187;
			case 5:
				goto IL_212;
			case 6:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㤿ᕁⵃ⡅ⱇ╉㭋", a_)))
				{
					num = 0;
					continue;
				}
				goto IL_8C;
			case 7:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㠿ᕁⵃ⡅ⱇ╉㭋", a_)))
				{
					num = 20;
					continue;
				}
				goto IL_B0;
			case 8:
				goto IL_115;
			case 9:
				if (A_1 == null)
				{
					num = 13;
					continue;
				}
				num = 15;
				continue;
			case 10:
				goto IL_87;
			case 11:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 16;
					continue;
				}
				goto IL_171;
			case 12:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 23;
					continue;
				}
				num = 11;
				continue;
			case 13:
				goto IL_14B;
			case 14:
				goto IL_187;
			case 15:
				if (A_0.LocalName != RecordTableEnumerator.b("␿⍁ぃ❅ṇ⭉⁋❍㑏㍑⁓㽕㝗㑙⽛", a_))
				{
					num = 8;
					continue;
				}
				spr_u22CB = (spr\u22CB)spr\u175E.ᜀ(TBIFFRecord.DVal);
				num = 4;
				continue;
			case 16:
				num = 22;
				continue;
			case 18:
				spr_u22CB.ᜀ(XmlConvert.ToBoolean(A_0.Value));
				num = 14;
				continue;
			case 19:
				this.ᜀ(A_0, a_2);
				num = 2;
				continue;
			case 20:
				spr_u22CB.ᜀ(XmlConvert.ToInt32(A_0.Value));
				num = 1;
				continue;
			case 21:
				goto IL_212;
			case 22:
				if (A_0.LocalName == RecordTableEnumerator.b("␿⍁ぃ❅ṇ⭉⁋❍㑏㍑⁓㽕㝗㑙", a_))
				{
					num = 19;
					continue;
				}
				goto IL_171;
			case 23:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_26E;
				default:
					goto IL_324;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 10;
				continue;
			}
			num = 9;
			continue;
			IL_8C:
			a_2 = A_1.DVTable.ᜀ(spr_u22CB);
			A_0.Read();
			num = 21;
			continue;
			IL_B0:
			num = 6;
			continue;
			IL_171:
			A_0.Skip();
			num = 5;
			continue;
			IL_187:
			num = 7;
			continue;
			IL_212:
			num = 12;
			continue;
			IL_26E:
			num = 18;
		}
		IL_87:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁╃≅ⵇ㡉", a_));
		IL_115:
		throw new XmlException(RecordTableEnumerator.b("㈿❁╃≅ⵇ㡉", a_));
		IL_14B:
		throw new ArgumentNullException(RecordTableEnumerator.b("㌿⩁⅃⍅㱇", a_));
		IL_324:
		if (false)
		{
		}
		A_0.Skip();
	}

	// Token: 0x06005017 RID: 20503 RVA: 0x0031BC58 File Offset: 0x0031AC58
	private void ᜀ(XmlReader A_0, XlsDataValidationCollection A_1)
	{
		int a_ = 10;
		switch (0)
		{
		default:
		{
			int num = 33;
			XlsValidation xlsValidation;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_1C7;
				case 1:
					goto IL_2FB;
				case 2:
					xlsValidation.AllowType = this.ᜆ(A_0.Value);
					num = 11;
					continue;
				case 3:
					xlsValidation.InputMessage = this.ᜀ(A_0.Value);
					num = 18;
					continue;
				case 4:
					xlsValidation.InputTitle = A_0.Value;
					num = 26;
					continue;
				case 5:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("⼿㉁⅃㑅⥇㹉⍋㱍", a_)))
					{
						num = 29;
						continue;
					}
					goto IL_15D;
				case 6:
					goto IL_31D;
				case 7:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㌿㍁㙃⍅⹇", a_)))
					{
						num = 34;
						continue;
					}
					goto IL_28A;
				case 8:
					xlsValidation.ErrorTitle = A_0.Value;
					num = 40;
					continue;
				case 9:
					goto IL_70F;
				case 10:
					goto IL_122;
				case 11:
					goto IL_3B3;
				case 12:
					goto IL_127;
				case 13:
					xlsValidation.ShowError = XmlConvert.ToBoolean(A_0.Value);
					num = 43;
					continue;
				case 14:
					goto IL_5DA;
				case 15:
				{
					int num2;
					TAddr[] array;
					if (num2 >= array.Length)
					{
						num = 31;
						continue;
					}
					if (true)
					{
					}
					TAddr a_2 = array[num2];
					xlsValidation.ᜀ(a_2);
					num2++;
					num = 50;
					continue;
				}
				case 16:
					goto IL_36C;
				case 17:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("ℿ⹁⡃⥅㽇ࡉ⁋⽍㹏㥑", a_)))
					{
						num = 51;
						continue;
					}
					goto IL_4FA;
				case 18:
					goto IL_586;
				case 19:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("┿ぁ㙃⥅㩇ṉ╋㩍㱏㝑", a_)))
					{
						num = 8;
						continue;
					}
					goto IL_652;
				case 20:
					num = 52;
					continue;
				case 21:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㐿㭁㑃⍅", a_)))
					{
						num = 2;
						continue;
					}
					goto IL_3B3;
				case 22:
					goto IL_2FB;
				case 23:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("〿ぁ⭃⭅㡇㹉ᡋ❍⑏㹑ㅓ", a_)))
					{
						num = 4;
						continue;
					}
					goto IL_72D;
				case 24:
					xlsValidation.ErrorMessage = this.ᜀ(A_0.Value);
					num = 53;
					continue;
				case 25:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㌿⩁⭃ㅅŇ⑉㱋㭍⑏ὑㅓ╕⭗㭙㭛㭝", a_)))
					{
						num = 39;
						continue;
					}
					goto IL_1C7;
				case 26:
					goto IL_72D;
				case 27:
					A_0.Read();
					num = 22;
					continue;
				case 28:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("〿ぁ⭃⭅㡇㹉", a_)))
					{
						num = 3;
						continue;
					}
					goto IL_586;
				case 29:
					xlsValidation.CompareOperator = this.ᜄ(A_0.Value);
					num = 55;
					continue;
				case 30:
					xlsValidation.AlertStyle = this.ᜅ(A_0.Value);
					num = 48;
					continue;
				case 31:
					goto IL_28A;
				case 32:
					if (!(A_0.LocalName == RecordTableEnumerator.b("☿ⵁ㙃⭅㵇♉ⵋ罍", a_)))
					{
						num = 20;
						continue;
					}
					goto IL_714;
				case 34:
				{
					TAddr[] array2 = this.ᜃ(A_0.Value);
					TAddr[] array = array2;
					int num2 = 0;
					num = 16;
					continue;
				}
				case 35:
					goto IL_688;
				case 36:
					xlsValidation.IsSuppressDropDownArrow = XmlConvert.ToBoolean(A_0.Value);
					num = 12;
					continue;
				case 37:
					if (A_0.LocalName != RecordTableEnumerator.b("␿⍁ぃ❅ṇ⭉⁋❍㑏㍑⁓㽕㝗㑙", a_))
					{
						num = 9;
						continue;
					}
					xlsValidation = new XlsValidation(A_1);
					num = 7;
					continue;
				case 38:
					IL_5C8:
					if (A_1 == null)
					{
						num = 14;
						continue;
					}
					num = 37;
					continue;
				case 39:
					xlsValidation.ShowInput = XmlConvert.ToBoolean(A_0.Value);
					num = 0;
					continue;
				case 40:
					goto IL_652;
				case 41:
					if (!A_0.IsEmptyElement)
					{
						num = 27;
						continue;
					}
					goto IL_785;
				case 42:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("┿ぁ㙃⥅㩇᥉㡋㝍㱏㝑", a_)))
					{
						num = 30;
						continue;
					}
					goto IL_553;
				case 43:
					goto IL_61C;
				case 44:
					goto IL_61C;
				case 45:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("┿ぁ㙃⥅㩇", a_)))
					{
						num = 24;
						continue;
					}
					goto IL_322;
				case 46:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㌿⩁⭃ㅅే㡉⍋㹍ᑏ㵑⍓㡕", a_)))
					{
						num = 36;
						continue;
					}
					goto IL_127;
				case 47:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㌿⩁⭃ㅅേ㡉㹋⅍≏ὑㅓ╕⭗㭙㭛㭝", a_)))
					{
						num = 13;
						continue;
					}
					xlsValidation.ShowError = false;
					num = 44;
					continue;
				case 48:
					goto IL_553;
				case 49:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 6;
						continue;
					}
					num = 32;
					continue;
				case 50:
					goto IL_36C;
				case 51:
					xlsValidation.IgnoreBlank = XmlConvert.ToBoolean(A_0.Value);
					num = 54;
					continue;
				case 52:
					if (A_0.LocalName == RecordTableEnumerator.b("☿ⵁ㙃⭅㵇♉ⵋ籍", a_))
					{
						num = 56;
						continue;
					}
					goto IL_688;
				case 53:
					goto IL_322;
				case 54:
					goto IL_4FA;
				case 55:
					goto IL_15D;
				case 56:
					goto IL_714;
				}
				if (A_0 == null)
				{
					num = 10;
					continue;
				}
				num = 38;
				continue;
				IL_127:
				num = 47;
				continue;
				IL_15D:
				num = 28;
				continue;
				IL_1C7:
				A_0.MoveToElement();
				num = 41;
				continue;
				IL_28A:
				num = 21;
				continue;
				IL_2FB:
				num = 49;
				continue;
				IL_322:
				num = 42;
				continue;
				IL_36C:
				num = 15;
				continue;
				IL_3B3:
				num = 17;
				continue;
				IL_4FA:
				num = 45;
				continue;
				IL_553:
				num = 19;
				continue;
				IL_586:
				num = 23;
				continue;
				IL_688:
				A_0.Skip();
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_5C8;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				IL_61C:
				num = 25;
				continue;
				IL_652:
				num = 5;
				continue;
				IL_714:
				this.ᜀ(A_0, xlsValidation);
				num = 35;
				continue;
				IL_72D:
				num = 46;
			}
			IL_122:
			throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁╃≅ⵇ㡉", a_));
			IL_31D:
			goto IL_785;
			IL_5DA:
			throw new ArgumentNullException(RecordTableEnumerator.b("␿㑁݃⥅⑇♉⥋ⵍ⑏㭑㭓㡕", a_));
			IL_70F:
			throw new XmlException(RecordTableEnumerator.b("㈿❁╃≅ⵇ㡉", a_));
			IL_785:
			this.ᜀ(xlsValidation);
			A_0.Read();
			A_1.Add(xlsValidation);
			return;
		}
		}
	}

	// Token: 0x06005018 RID: 20504 RVA: 0x0031C400 File Offset: 0x0031B400
	private void ᜀ(XlsValidation A_0)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		sprᡣ sprᡣ = A_0.DVRecord;
		Ptg[] array = sprᡣ.\u1713();
		sprᡣ.ᜃ(sprᡣ.\u170D() == CellDataType.User && array != null && sprᡣ.\u1714() == null && array.Length == 1 && array[0].TokenCode == FormulaToken.tStringConstant);
	}

	// Token: 0x06005019 RID: 20505 RVA: 0x0031C484 File Offset: 0x0031B484
	private void ᜀ(XmlReader A_0, XlsValidation A_1)
	{
		int a_ = 0;
		if (true)
		{
		}
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				if (A_1 == null)
				{
					num = 6;
					continue;
				}
				string localName = A_0.LocalName;
				A_0.Read();
				num = 7;
				continue;
			}
			case 2:
				goto IL_92;
			case 3:
				goto IL_DD;
			case 4:
				goto IL_4F;
			case 5:
				A_1.ᜀ(A_0.Value, this.ᜊ, true);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_139;
				default:
					if (false)
					{
					}
					num = 2;
					continue;
				}
				break;
			case 6:
				goto IL_F8;
			case 7:
			{
				string localName;
				if (localName == RecordTableEnumerator.b("倵圷䠹儻䬽ⰿ⍁畃", a_))
				{
					num = 5;
					continue;
				}
				A_1.ᜀ(A_0.Value, this.ᜊ, false);
				num = 3;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 4;
			}
			else
			{
				num = 0;
			}
		}
		IL_4F:
		throw new ArgumentNullException(RecordTableEnumerator.b("䐵崷嬹堻嬽㈿", a_));
		IL_92:
		IL_DD:
		goto IL_139;
		IL_F8:
		throw new ArgumentNullException(RecordTableEnumerator.b("刵夷丹崻栽ℿ⹁ⵃ≅⥇㹉╋⅍㹏", a_));
		IL_139:
		A_0.Skip();
	}

	// Token: 0x0600501A RID: 20506 RVA: 0x0031C5D0 File Offset: 0x0031B5D0
	public void \u1715(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 4;
		int num = 12;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_13B;
			case 1:
				if (A_0.IsEmptyElement)
				{
					num = 11;
					continue;
				}
				goto IL_234;
			case 2:
				goto IL_FF;
			case 3:
				goto IL_FA;
			case 4:
			{
				TAddr taddr = this.ᜂ(A_0.Value);
				XlsAutoFiltersCollection xlsAutoFiltersCollection;
				xlsAutoFiltersCollection.Range = A_1[taddr.FirstRow + 1, taddr.FirstCol + 1, taddr.LastRow + 1, taddr.LastCol + 1];
				num = 6;
				continue;
			}
			case 5:
				goto IL_64;
			case 6:
				goto IL_9C;
			case 7:
				if (true)
				{
				}
				goto IL_13B;
			case 8:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 10;
					continue;
				}
				num = 9;
				continue;
			case 9:
				if (A_0.LocalName == RecordTableEnumerator.b("尹唻刽㐿❁㙃Յ❇♉㥋⍍㹏", a_))
				{
					num = 13;
					continue;
				}
				goto IL_FF;
			case 10:
				goto IL_168;
			case 11:
				A_0.Read();
				num = 7;
				continue;
			case 13:
			{
				XlsAutoFiltersCollection xlsAutoFiltersCollection;
				this.ᜀ(A_0, xlsAutoFiltersCollection);
				num = 2;
				continue;
			}
			case 14:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䠹夻堽", a_)))
				{
					num = 4;
					continue;
				}
				goto IL_9C;
			case 15:
			{
				if (A_1 == null)
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
						num = 3;
						continue;
					}
				}
				bool isEmptyElement = A_0.IsEmptyElement;
				XlsAutoFiltersCollection xlsAutoFiltersCollection = (XlsAutoFiltersCollection)A_1.AutoFilters;
				num = 14;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 5;
				continue;
			}
			num = 15;
			continue;
			IL_9C:
			num = 1;
			continue;
			IL_FF:
			A_0.Skip();
			num = 0;
			continue;
			IL_13B:
			num = 8;
		}
		IL_64:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠹夻弽␿❁㙃", a_));
		IL_FA:
		throw new ArgumentNullException(RecordTableEnumerator.b("䤹吻嬽┿㙁", a_));
		IL_168:
		IL_234:
		A_0.Skip();
	}

	// Token: 0x0600501B RID: 20507 RVA: 0x0031C818 File Offset: 0x0031B818
	private void ᜀ(XmlReader A_0, XlsAutoFiltersCollection A_1)
	{
		int a_ = 7;
		int num = 12;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				if (!A_0.MoveToAttribute(RecordTableEnumerator.b("帼倾ⵀੂ⅄", a_)))
				{
					num = 7;
					continue;
				}
				int num2 = XmlConvert.ToInt32(A_0.Value);
				XlsAutoFilter xlsAutoFilter = (XlsAutoFilter)A_1[num2];
				xlsAutoFilter.Index = num2;
				A_0.Read();
				num = 3;
				continue;
			}
			case 1:
				if (A_0.LocalName == RecordTableEnumerator.b("嬼嘾ⵀ㝂⁄㕆㩈", a_))
				{
					num = 18;
					continue;
				}
				goto IL_1D9;
			case 2:
				if (A_1 == null)
				{
					num = 6;
					continue;
				}
				num = 0;
				continue;
			case 3:
				goto IL_11F;
			case 4:
				if (A_0.NodeType != XmlNodeType.EndElement)
				{
					num = 16;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1FA;
				default:
					if (false)
					{
					}
					num = 15;
					continue;
				}
				break;
			case 5:
				goto IL_11F;
			case 6:
				goto IL_E7;
			case 7:
				goto IL_1D7;
			case 8:
			{
				XlsAutoFilter xlsAutoFilter;
				this.ᜁ(A_0, xlsAutoFilter);
				num = 13;
				continue;
			}
			case 9:
				goto IL_1D9;
			case 10:
				goto IL_E9;
			case 11:
				goto IL_1FA;
			case 13:
				goto IL_1EF;
			case 14:
				goto IL_70;
			case 15:
				goto IL_165;
			case 16:
				if (A_0.LocalName == RecordTableEnumerator.b("帼䨾㉀㝂⩄⩆཈≊⅌㭎㑐⅒♔", a_))
				{
					num = 8;
					continue;
				}
				goto IL_1EF;
			case 17:
			{
				XlsAutoFilter xlsAutoFilter;
				this.ᜂ(A_0, xlsAutoFilter);
				num = 10;
				continue;
			}
			case 18:
			{
				XlsAutoFilter xlsAutoFilter;
				this.ᜃ(A_0, xlsAutoFilter);
				num = 9;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 14;
				continue;
			}
			num = 2;
			continue;
			IL_E9:
			num = 1;
			continue;
			IL_1FA:
			if (A_0.LocalName == RecordTableEnumerator.b("䤼倾ㅀ牂畄", a_))
			{
				num = 17;
				continue;
			}
			goto IL_E9;
			IL_11F:
			num = 4;
			continue;
			IL_1D9:
			A_0.Skip();
			num = 5;
			continue;
			IL_1EF:
			num = 11;
		}
		IL_70:
		throw new ArgumentNullException(RecordTableEnumerator.b("似娾⁀❂⁄㕆", a_));
		IL_E7:
		throw new ArgumentNullException(RecordTableEnumerator.b("尼䨾㕀ⱂ̈́⹆╈㽊⡌㵎≐", a_));
		IL_165:
		if (true)
		{
		}
		A_0.Skip();
		return;
		IL_1D7:
		throw new spr\u23EE(RecordTableEnumerator.b("漼娾぀㙂ⱄ㕆ⱈ⽊浌⹎═❒❔㹖㭘⹚⥜㩞䅠ᑢѤ०䥨ժɬ᭮兰Ͳݴቶ੸Ṻ፼୾꾀", a_));
	}

	// Token: 0x0600501C RID: 20508 RVA: 0x0031CAB4 File Offset: 0x0031BAB4
	private void ᜃ(XmlReader A_0, XlsAutoFilter A_1)
	{
		int a_ = 10;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 3;
				continue;
			case 1:
				goto IL_53;
			case 2:
				goto IL_12A;
			case 3:
				if (!A_0.MoveToAttribute(RecordTableEnumerator.b("㘿⍁⡃", a_)))
				{
					return;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					num = 5;
					continue;
				}
				break;
			case 4:
				goto IL_10F;
			case 5:
			{
				A_1.IsSimple1 = true;
				XlsAutoFilterCondition xlsAutoFilterCondition = (XlsAutoFilterCondition)A_1.FirstCondition;
				xlsAutoFilterCondition.DataType = FilterDataType.String;
				xlsAutoFilterCondition.ConditionOperator = FilterConditionType.Equal;
				xlsAutoFilterCondition.String = A_0.Value;
				A_0.Skip();
				num = 4;
				continue;
			}
			case 7:
				if (A_0.LocalName == RecordTableEnumerator.b("☿⭁⡃㉅ⵇ㡉", a_))
				{
					num = 0;
					continue;
				}
				return;
			case 8:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				A_0.Read();
				num = 7;
				continue;
			}
			if (A_0 == null)
			{
				if (true)
				{
				}
				num = 1;
			}
			else
			{
				num = 8;
			}
		}
		IL_53:
		throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁╃≅ⵇ㡉", a_));
		IL_10F:
		return;
		IL_12A:
		throw new ArgumentNullException(RecordTableEnumerator.b("ℿ㝁ぃ⥅็⍉⁋㩍㕏⁑", a_));
	}

	// Token: 0x0600501D RID: 20509 RVA: 0x0031CC30 File Offset: 0x0031BC30
	private void ᜂ(XmlReader A_0, XlsAutoFilter A_1)
	{
		int a_ = 19;
		int num = 9;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_23A;
				default:
				{
					if (false)
					{
					}
					XlsAutoFilterCondition xlsAutoFilterCondition = (XlsAutoFilterCondition)A_1.FirstCondition;
					xlsAutoFilterCondition.ConditionOperator = FilterConditionType.GreaterOrEqual;
					xlsAutoFilterCondition.DataType = FilterDataType.FloatingPoint;
					xlsAutoFilterCondition.Double = XmlConvert.ToDouble(A_0.Value);
					num = 8;
					continue;
				}
				}
				break;
			case 1:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㽈⩊⅌", a_)))
				{
					num = 6;
					continue;
				}
				goto IL_E6;
			case 2:
				goto IL_64;
			case 3:
				if (true)
				{
				}
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⽈≊⅌㭎㑐⅒͔㙖㕘", a_)))
				{
					num = 0;
					continue;
				}
				return;
			case 4:
				A_1.IsTop10Percent = XmlConvert.ToBoolean(A_0.Value);
				num = 5;
				continue;
			case 5:
				goto IL_69;
			case 6:
				A_1.Top10Items = XmlConvert.ToInt32(A_0.Value);
				num = 10;
				continue;
			case 7:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㵈⑊㵌", a_)))
				{
					goto IL_23A;
				}
				goto IL_94;
			case 8:
				return;
			case 10:
				goto IL_E6;
			case 11:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㥈⹊㽌ⱎ㑐㵒⅔", a_)))
				{
					num = 4;
					continue;
				}
				goto IL_69;
			case 12:
				goto IL_94;
			case 13:
				A_1.ShowTopItem = XmlConvert.ToBoolean(A_0.Value);
				num = 12;
				continue;
			case 14:
				if (A_1 == null)
				{
					num = 15;
					continue;
				}
				A_1.IsTop10Items = true;
				A_1.ShowTopItem = true;
				num = 7;
				continue;
			case 15:
				goto IL_E1;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			num = 14;
			continue;
			IL_69:
			num = 1;
			continue;
			IL_94:
			num = 11;
			continue;
			IL_E6:
			num = 3;
			continue;
			IL_23A:
			num = 13;
		}
		IL_64:
		throw new ArgumentNullException(RecordTableEnumerator.b("㭈⹊ⱌ⭎㑐⅒", a_));
		IL_E1:
		throw new ArgumentNullException(RecordTableEnumerator.b("⡈㹊㥌⁎ᝐ㩒㥔⍖㱘⥚", a_));
	}

	// Token: 0x0600501E RID: 20510 RVA: 0x0031CE88 File Offset: 0x0031BE88
	private void ᜁ(XmlReader A_0, XlsAutoFilter A_1)
	{
		int a_ = 9;
		int num = 10;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_AD;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					A_1.IsAnd = XmlConvert.ToBoolean(A_0.Value);
					break;
				}
				num = 7;
				continue;
			case 2:
				if (A_0.LocalName == RecordTableEnumerator.b("尾㑀あㅄ⡆⑈ൊ⑌⍎═㙒❔", a_))
				{
					num = 3;
					continue;
				}
				goto IL_AD;
			case 3:
				this.ᜀ(A_0, A_1);
				num = 0;
				continue;
			case 4:
				if (A_1 == null)
				{
					num = 5;
					continue;
				}
				num = 8;
				continue;
			case 5:
				goto IL_A8;
			case 6:
				goto IL_114;
			case 7:
				goto IL_78;
			case 8:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("帾⽀❂", a_)))
				{
					num = 1;
					continue;
				}
				goto IL_78;
			case 9:
				goto IL_5C;
			case 11:
				return;
			case 12:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 11;
					continue;
				}
				num = 2;
				continue;
			case 13:
				goto IL_114;
			}
			if (A_0 == null)
			{
				num = 9;
				continue;
			}
			num = 4;
			continue;
			IL_78:
			A_0.Read();
			num = 13;
			continue;
			IL_AD:
			A_0.Skip();
			num = 6;
			continue;
			IL_114:
			num = 12;
		}
		IL_5C:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䴾⑀≂⅄≆㭈", a_));
		IL_A8:
		throw new ArgumentNullException(RecordTableEnumerator.b("帾㑀㝂⩄ņ⁈❊㥌⩎⍐", a_));
	}

	// Token: 0x0600501F RID: 20511 RVA: 0x0031D050 File Offset: 0x0031C050
	private void ᜀ(XmlReader A_0, XlsAutoFilter A_1)
	{
		int a_ = 14;
		int num = 3;
		for (;;)
		{
			XlsAutoFilterCondition xlsAutoFilterCondition;
			XlsAutoFilterCondition xlsAutoFilterCondition2;
			switch (num)
			{
			case 0:
				return;
			case 1:
				if (!A_1.HasFirstCondition)
				{
					num = 6;
					continue;
				}
				num = 7;
				continue;
			case 2:
				goto IL_78;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_AE;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 4:
				if (A_1 == null)
				{
					num = 11;
					continue;
				}
				num = 1;
				continue;
			case 5:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㉃❅⑇", a_)))
				{
					num = 9;
					continue;
				}
				return;
			case 6:
				num = 13;
				continue;
			case 7:
				xlsAutoFilterCondition = (XlsAutoFilterCondition)A_1.FirstCondition;
				goto IL_11C;
			case 8:
				goto IL_CF;
			case 9:
				if (true)
				{
				}
				xlsAutoFilterCondition2.String = A_0.Value;
				xlsAutoFilterCondition2.DataType = FilterDataType.String;
				num = 0;
				continue;
			case 10:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⭃㙅ⵇ㡉ⵋ㩍㽏⁑", a_)))
				{
					num = 12;
					continue;
				}
				goto IL_CF;
			case 11:
				goto IL_CA;
			case 12:
				xlsAutoFilterCondition2.ConditionOperator = this.ᜁ(A_0.Value);
				num = 8;
				continue;
			case 13:
				xlsAutoFilterCondition = (XlsAutoFilterCondition)A_1.SecondCondition;
				goto IL_11C;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			IL_AE:
			num = 4;
			continue;
			IL_CF:
			num = 5;
			continue;
			IL_11C:
			xlsAutoFilterCondition2 = xlsAutoFilterCondition;
			xlsAutoFilterCondition2.ConditionOperator = FilterConditionType.Equal;
			num = 10;
		}
		IL_78:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙃⍅⥇⹉⥋㱍", a_));
		IL_CA:
		throw new ArgumentNullException(RecordTableEnumerator.b("╃㍅㱇╉ੋ❍㱏♑ㅓ⑕", a_));
	}

	// Token: 0x06005020 RID: 20512 RVA: 0x0031D230 File Offset: 0x0031C230
	internal List<Color> ᜪ(XmlReader A_0)
	{
		int a_ = 2;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 11;
					continue;
				}
				A_0.Read();
				num = 14;
				continue;
			case 1:
				goto IL_105;
			case 2:
				if (A_0.LocalName != RecordTableEnumerator.b("䰷刹夻匽┿", a_))
				{
					goto IL_1E4;
				}
				num = 6;
				continue;
			case 3:
				goto IL_CA;
			case 4:
				goto IL_6C;
			case 6:
				if (A_0.IsEmptyElement)
				{
					num = 10;
					continue;
				}
				A_0.Read();
				num = 1;
				continue;
			case 7:
				if (A_0.NodeType != XmlNodeType.EndElement)
				{
					num = 8;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1E4;
				default:
					if (false)
					{
					}
					num = 12;
					continue;
				}
				break;
			case 8:
				if (A_0.LocalName == RecordTableEnumerator.b("䰷刹夻匽┿݁⡃⍅╇⽉≋㩍⍏", a_))
				{
					num = 9;
					continue;
				}
				goto IL_CA;
			case 9:
				this.ᜌ = this.ᜥ(A_0);
				num = 3;
				continue;
			case 10:
				goto IL_1B9;
			case 11:
				num = 2;
				continue;
			case 12:
				goto IL_14E;
			case 13:
				goto IL_105;
			case 14:
				goto IL_A6;
			case 15:
				goto IL_1EF;
			}
			if (A_0 == null)
			{
				if (true)
				{
				}
				num = 4;
				continue;
			}
			IL_A6:
			num = 0;
			continue;
			IL_CA:
			A_0.Skip();
			num = 13;
			continue;
			IL_105:
			num = 7;
			continue;
			IL_1E4:
			num = 15;
		}
		IL_6C:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹崻娽┿ぁ", a_));
		IL_14E:
		this.ᜉ.MajorFonts = this.ᜏ;
		this.ᜉ.MinorFonts = this.ᜐ;
		return this.ᜌ;
		IL_1B9:
		return null;
		IL_1EF:
		throw new XmlException(RecordTableEnumerator.b("䨷弹崻娽┿ぁ", a_));
	}

	// Token: 0x06005021 RID: 20513 RVA: 0x0031D45C File Offset: 0x0031C45C
	internal List<Color> ᜥ(XmlReader A_0)
	{
		int a_ = 10;
		int num = 16;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 11;
					continue;
				}
				A_0.Skip();
				num = 5;
				continue;
			case 1:
				if (A_0.NodeType != XmlNodeType.EndElement)
				{
					num = 0;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1A2;
				default:
					if (false)
					{
					}
					num = 4;
					continue;
				}
				break;
			case 2:
				num = 10;
				continue;
			case 3:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 2;
					continue;
				}
				goto IL_103;
			}
			case 4:
			{
				List<Color> result;
				return result;
			}
			case 5:
				goto IL_103;
			case 6:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("☿ⵁ⩃㉅ᭇ⥉⑋⭍㵏㝑", a_)))
				{
					num = 18;
					continue;
				}
				this.ᜅ(A_0);
				goto IL_1A2;
			}
			case 7:
				goto IL_103;
			case 8:
				goto IL_F4;
			case 9:
				goto IL_103;
			case 10:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("⌿⹁㙃ᕅ⭇≉⥋⍍㕏", a_)))
				{
					num = 15;
					continue;
				}
				List<Color> result = this.ᜀ(A_0, out this.\u170D);
				num = 13;
				continue;
			}
			case 11:
				num = 3;
				continue;
			case 12:
				goto IL_70;
			case 13:
				goto IL_103;
			case 14:
			{
				if (A_0.IsEmptyElement)
				{
					num = 8;
					continue;
				}
				List<Color> result = new List<Color>();
				A_0.Read();
				num = 17;
				continue;
			}
			case 15:
				num = 6;
				continue;
			case 17:
				goto IL_103;
			case 18:
				num = 7;
				continue;
			}
			if (A_0 == null)
			{
				num = 12;
				continue;
			}
			num = 14;
			continue;
			IL_103:
			if (true)
			{
			}
			num = 1;
			continue;
			IL_1A2:
			num = 9;
		}
		IL_70:
		throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁╃≅ⵇ㡉", a_));
		IL_F4:
		return null;
	}

	// Token: 0x06005022 RID: 20514 RVA: 0x0031D688 File Offset: 0x0031C688
	private void ᜅ(XmlReader A_0)
	{
		int a_ = 8;
		int num = 11;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_86;
			case 1:
				num = 12;
				continue;
			case 2:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("匽⤿ⱁ⭃㑅็╉≋㩍", a_)))
				{
					num = 10;
					continue;
				}
				this.ᜁ(A_0, out this.ᜐ);
				num = 0;
				continue;
			}
			case 3:
				goto IL_198;
			case 4:
				return;
			case 5:
				goto IL_10F;
			case 6:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 1;
					continue;
				}
				goto IL_198;
			}
			case 7:
				goto IL_10F;
			case 8:
				goto IL_6C;
			case 9:
				goto IL_10F;
			case 10:
				num = 3;
				continue;
			case 12:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("匽ℿ⡁⭃㑅็╉≋㩍", a_)))
				{
					num = 14;
					continue;
				}
				this.ᜀ(A_0, out this.ᜏ);
				num = 7;
				continue;
			}
			case 13:
				if (A_0.NodeType != XmlNodeType.EndElement)
				{
					num = 17;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_86;
				default:
					if (false)
					{
					}
					num = 4;
					continue;
				}
				break;
			case 14:
				num = 2;
				continue;
			case 15:
				goto IL_10F;
			case 16:
				num = 6;
				continue;
			case 17:
				if (true)
				{
				}
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 16;
					continue;
				}
				A_0.Skip();
				num = 9;
				continue;
			}
			if (A_0 == null)
			{
				num = 8;
				continue;
			}
			A_0.Read();
			num = 15;
			continue;
			IL_10F:
			num = 13;
			continue;
			IL_86:
			goto IL_10F;
			IL_198:
			A_0.Skip();
			num = 5;
		}
		IL_6C:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰽┿⍁⁃⍅㩇", a_));
	}

	// Token: 0x06005023 RID: 20515 RVA: 0x0031D89C File Offset: 0x0031C89C
	private void ᜁ(XmlReader A_0, out Dictionary<string, XlsFont> A_1)
	{
		int a_ = 9;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 17;
					continue;
				}
				goto IL_EC;
			}
			case 1:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("尾㉀", a_)))
				{
					num = 15;
					continue;
				}
				XlsFont value = this.ᜩ(A_0);
				A_1.Add(RecordTableEnumerator.b("尾㉀", a_), value);
				num = 19;
				continue;
			}
			case 2:
				num = 1;
				continue;
			case 3:
				goto IL_14A;
			case 5:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("匾⁀㝂ⱄ⥆", a_)))
				{
					num = 7;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_85;
				default:
				{
					if (false)
					{
					}
					XlsFont value = this.ᜩ(A_0);
					A_1.Add(RecordTableEnumerator.b("匾⁀㝂ⱄ⥆", a_), value);
					num = 6;
					continue;
				}
				}
				break;
			}
			case 6:
				goto IL_14A;
			case 7:
				num = 10;
				continue;
			case 8:
				goto IL_16D;
			case 9:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 13;
					continue;
				}
				A_0.Skip();
				num = 3;
				continue;
			case 10:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("娾⁀", a_)))
				{
					num = 2;
					continue;
				}
				XlsFont value = this.ᜩ(A_0);
				A_1.Add(RecordTableEnumerator.b("娾⁀", a_), value);
				num = 20;
				continue;
			}
			case 11:
				goto IL_14A;
			case 12:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 8;
					continue;
				}
				num = 9;
				continue;
			case 13:
				num = 0;
				continue;
			case 14:
				goto IL_EC;
			case 15:
				num = 14;
				continue;
			case 16:
				goto IL_14A;
			case 17:
				num = 5;
				continue;
			case 18:
				goto IL_85;
			case 19:
				goto IL_14A;
			case 20:
				goto IL_14A;
			}
			if (A_0 == null)
			{
				num = 18;
				continue;
			}
			A_0.Read();
			A_1 = new Dictionary<string, XlsFont>();
			num = 16;
			continue;
			IL_EC:
			A_0.Skip();
			num = 11;
			continue;
			IL_14A:
			num = 12;
		}
		IL_85:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴾⑀≂⅄≆㭈", a_));
		IL_16D:
		if (true)
		{
		}
		A_0.Read();
	}

	// Token: 0x06005024 RID: 20516 RVA: 0x0031DB58 File Offset: 0x0031CB58
	private void ᜀ(XmlReader A_0, out Dictionary<string, XlsFont> A_1)
	{
		int a_ = 1;
		int num = 20;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 19;
				continue;
			case 1:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("嬶堸伺吼儾", a_)))
				{
					num = 0;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_85;
				default:
				{
					if (false)
					{
					}
					XlsFont value = this.ᜩ(A_0);
					A_1.Add(RecordTableEnumerator.b("嬶䴸", a_), value);
					num = 8;
					continue;
				}
				}
				break;
			}
			case 2:
				goto IL_14A;
			case 3:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("吶䨸", a_)))
				{
					num = 15;
					continue;
				}
				XlsFont value = this.ᜩ(A_0);
				A_1.Add(RecordTableEnumerator.b("吶䨸", a_), value);
				num = 11;
				continue;
			}
			case 4:
				num = 1;
				continue;
			case 5:
				goto IL_EC;
			case 6:
				goto IL_14A;
			case 7:
				num = 14;
				continue;
			case 8:
				goto IL_14A;
			case 9:
				goto IL_14A;
			case 10:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 7;
					continue;
				}
				A_0.Skip();
				num = 2;
				continue;
			case 11:
				goto IL_14A;
			case 12:
				goto IL_85;
			case 13:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 17;
					continue;
				}
				num = 10;
				continue;
			case 14:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 4;
					continue;
				}
				goto IL_EC;
			}
			case 15:
				num = 5;
				continue;
			case 16:
				goto IL_14A;
			case 17:
				goto IL_16D;
			case 18:
				num = 3;
				continue;
			case 19:
			{
				if (true)
				{
				}
				string localName;
				if (!(localName == RecordTableEnumerator.b("制堸", a_)))
				{
					num = 18;
					continue;
				}
				XlsFont value = this.ᜩ(A_0);
				A_1.Add(RecordTableEnumerator.b("制堸", a_), value);
				num = 6;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 12;
				continue;
			}
			A_0.Read();
			A_1 = new Dictionary<string, XlsFont>();
			num = 16;
			continue;
			IL_EC:
			A_0.Skip();
			num = 9;
			continue;
			IL_14A:
			num = 13;
		}
		IL_85:
		throw new ArgumentNullException(RecordTableEnumerator.b("䔶尸娺夼娾㍀", a_));
		IL_16D:
		A_0.Read();
	}

	// Token: 0x06005025 RID: 20517 RVA: 0x0031DE14 File Offset: 0x0031CE14
	internal XlsFont ᜩ(XmlReader A_0)
	{
		int a_ = 7;
		XlsFont xlsFont;
		for (;;)
		{
			xlsFont = null;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return xlsFont;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						xlsFont = (XlsFont)this.ᜉ.CreateFont(null, false);
						xlsFont.FontName = A_0.Value;
						num = 2;
						continue;
					}
					break;
				case 1:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䤼䘾ㅀ♂⍄♆⩈⹊", a_)))
					{
						num = 0;
						continue;
					}
					return xlsFont;
				case 2:
					return xlsFont;
				}
				break;
			}
		}
		return xlsFont;
	}

	// Token: 0x06005026 RID: 20518 RVA: 0x0031DEC4 File Offset: 0x0031CEC4
	internal List<Color> ᜀ(XmlReader A_0, out Dictionary<string, Color> A_1)
	{
		int a_ = 4;
		switch (0)
		{
		default:
		{
			int num = 20;
			List<Color> list;
			for (;;)
			{
				Color color;
				string localName;
				switch (num)
				{
				case 0:
					goto IL_152;
				case 1:
					goto IL_3D2;
				case 2:
				{
					int num2;
					if (num2 >= list.Count)
					{
						num = 16;
						continue;
					}
					num = 4;
					continue;
				}
				case 3:
					color = spr\u1D39.ᜀ(A_0.Value);
					goto IL_DE;
				case 4:
				{
					int num2;
					if (SystemColors.WindowText == list[num2])
					{
						num = 17;
						continue;
					}
					num2++;
					num = 12;
					continue;
				}
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_DE;
					default:
						if (false)
						{
						}
						goto IL_23D;
					}
					break;
				case 6:
					if (A_0.LocalName == RecordTableEnumerator.b("䤹主夽∿Ł⡃㑅", a_))
					{
						num = 8;
						continue;
					}
					num = 19;
					continue;
				case 7:
				{
					list.Reverse(0, 2);
					list.Reverse(2, 2);
					int num2 = 0;
					num = 5;
					continue;
				}
				case 8:
					num = 11;
					continue;
				case 9:
					goto IL_152;
				case 10:
					if (A_0.IsEmptyElement)
					{
						num = 1;
						continue;
					}
					A_0.Read();
					list = new List<Color>();
					A_1 = new Dictionary<string, Color>();
					num = 0;
					continue;
				case 11:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䰹崻刽", a_)))
					{
						num = 18;
						continue;
					}
					goto IL_2F6;
				case 12:
					goto IL_23D;
				case 13:
					goto IL_349;
				case 14:
					goto IL_152;
				case 15:
					num = 25;
					continue;
				case 16:
					goto IL_261;
				case 17:
				{
					int num2;
					this.ᜎ = new int?(num2);
					num = 27;
					continue;
				}
				case 18:
					color = spr\u1D39.ᜀ(int.Parse(A_0.Value, NumberStyles.HexNumber));
					num = 28;
					continue;
				case 19:
					if (A_0.LocalName == RecordTableEnumerator.b("䤹䔻䴽̿⹁㙃", a_))
					{
						num = 15;
						continue;
					}
					goto IL_349;
				case 21:
					goto IL_B6;
				case 22:
					goto IL_349;
				case 23:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 26;
						continue;
					}
					A_0.Skip();
					num = 14;
					continue;
				case 24:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 7;
						continue;
					}
					if (true)
					{
					}
					num = 23;
					continue;
				case 25:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䰹崻刽", a_)))
					{
						num = 3;
						continue;
					}
					goto IL_3D7;
				case 26:
					localName = A_0.LocalName;
					A_0.Read();
					color = spr\u1D39.ᜀ;
					num = 6;
					continue;
				case 27:
					goto IL_238;
				case 28:
					goto IL_2F6;
				case 29:
					goto IL_3D7;
				}
				if (A_0 == null)
				{
					num = 21;
					continue;
				}
				A_1 = null;
				num = 10;
				continue;
				IL_DE:
				num = 29;
				continue;
				IL_152:
				num = 24;
				continue;
				IL_23D:
				num = 2;
				continue;
				IL_2F6:
				A_0.Skip();
				num = 13;
				continue;
				IL_349:
				list.Add(color);
				this.\u170D.Add(localName, color);
				A_0.Skip();
				num = 9;
				continue;
				IL_3D7:
				A_0.Skip();
				num = 22;
			}
			IL_B6:
			throw new ArgumentNullException(RecordTableEnumerator.b("䠹夻弽␿❁㙃", a_));
			IL_238:
			IL_261:
			goto IL_3EE;
			IL_3D2:
			return null;
			IL_3EE:
			this.\u170D.Add(RecordTableEnumerator.b("丹䐻༽", a_), list[1]);
			this.\u170D.Add(RecordTableEnumerator.b("丹䐻ఽ", a_), list[3]);
			A_0.Read();
			return list;
		}
		}
	}

	// Token: 0x06005027 RID: 20519 RVA: 0x0031E308 File Offset: 0x0031D308
	public List<spr\u21A7> ᜣ(XmlReader A_0)
	{
		int a_ = 14;
		int num = 2;
		List<spr\u21A7> list;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.NodeType != XmlNodeType.None)
				{
					num = 9;
					continue;
				}
				return list;
			case 1:
				goto IL_89;
			case 3:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 6;
					continue;
				}
				num = 4;
				continue;
			case 4:
				if (A_0.LocalName == RecordTableEnumerator.b("⁃㹅⹇", a_))
				{
					if (true)
					{
					}
					num = 12;
					continue;
				}
				A_0.Skip();
				num = 10;
				continue;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_89;
				default:
					goto IL_18E;
				}
				break;
			case 6:
				goto IL_13C;
			case 7:
				goto IL_11C;
			case 8:
				goto IL_58;
			case 9:
				num = 11;
				continue;
			case 10:
				goto IL_11C;
			case 11:
				goto IL_11C;
			case 12:
				list.Add(this.ᜄ(A_0));
				num = 7;
				continue;
			}
			if (A_0 == null)
			{
				num = 8;
				continue;
			}
			num = 1;
			continue;
			IL_89:
			if (A_0.LocalName != RecordTableEnumerator.b("⁃㹅⹇㥉", a_))
			{
				num = 5;
				continue;
			}
			list = new List<spr\u21A7>();
			A_0.Read();
			num = 0;
			continue;
			IL_11C:
			num = 3;
		}
		IL_58:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙃⍅⥇⹉⥋㱍", a_));
		IL_13C:
		return list;
		IL_18E:
		if (false)
		{
		}
		throw new XmlException(RecordTableEnumerator.b("㙃⍅⥇⹉⥋㱍", a_));
	}

	// Token: 0x06005028 RID: 20520 RVA: 0x0031E4C0 File Offset: 0x0031D4C0
	private spr\u21A7 ᜄ(XmlReader A_0)
	{
		int a_ = 16;
		int num = 11;
		spr\u21A7 spr_u21A;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("⁅❇⑉㡋", a_)))
				{
					num = 5;
					continue;
				}
				spr_u21A.ᜀ(this.\u1714(A_0));
				A_0.Skip();
				num = 8;
				continue;
			}
			case 1:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("⑅❇㡉⡋⭍≏", a_)))
				{
					num = 12;
					continue;
				}
				spr_u21A.ᜀ(this.ᜈ(A_0));
				num = 3;
				continue;
			}
			case 2:
				goto IL_129;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1CF;
				default:
					if (false)
					{
					}
					goto IL_129;
				}
				break;
			case 4:
				goto IL_129;
			case 5:
				goto IL_1CF;
			case 6:
				num = 14;
				continue;
			case 7:
				goto IL_D3;
			case 8:
				goto IL_129;
			case 9:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 13;
					continue;
				}
				num = 16;
				continue;
			case 10:
				goto IL_129;
			case 12:
				num = 7;
				continue;
			case 13:
				goto IL_14C;
			case 14:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("⁅ⅇ♉⁋", a_)))
				{
					num = 17;
					continue;
				}
				spr_u21A.ᜀ(this.ᜁ(A_0, false));
				A_0.Skip();
				if (true)
				{
				}
				num = 4;
				continue;
			}
			case 15:
				goto IL_79;
			case 16:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 6;
					continue;
				}
				goto IL_D3;
			}
			case 17:
				num = 0;
				continue;
			}
			if (A_0 == null)
			{
				num = 15;
				continue;
			}
			spr_u21A = new spr\u21A7();
			A_0.Read();
			num = 2;
			continue;
			IL_D3:
			A_0.Skip();
			num = 10;
			continue;
			IL_129:
			num = 9;
			continue;
			IL_1CF:
			num = 1;
		}
		IL_79:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑅ⵇ⭉⡋⭍≏", a_));
		IL_14C:
		A_0.Read();
		return spr_u21A;
	}

	// Token: 0x06005029 RID: 20521 RVA: 0x0031E70C File Offset: 0x0031D70C
	public void ᜀ(XmlReader A_0, XlsWorksheetConditionalFormats A_1, List<spr\u21A7> A_2)
	{
		int a_ = 8;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 14;
				continue;
			case 1:
			{
				XlsConditionalFormats xlsConditionalFormats;
				A_1.Add(xlsConditionalFormats);
				num = 15;
				continue;
			}
			case 2:
				if (A_0.LocalName == RecordTableEnumerator.b("崽⼿ⱁ⁃⽅㱇⍉⍋⁍ㅏ㹑ቓ㥕⩗㝙㵛⩝ᑟୡ੣ť", a_))
				{
					num = 5;
					continue;
				}
				A_0.Skip();
				num = 10;
				continue;
			case 3:
				goto IL_1DF;
			case 5:
			{
				XlsConditionalFormats xlsConditionalFormats = new XlsConditionalFormats(this.ᜉ.AppImplementation, A_1);
				bool flag = this.ᜀ(A_0, xlsConditionalFormats, A_2);
				num = 9;
				continue;
			}
			case 6:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 13;
					continue;
				}
				num = 2;
				continue;
			case 7:
				if (A_0.NodeType == XmlNodeType.None)
				{
					goto IL_D2;
				}
				num = 8;
				continue;
			case 8:
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				goto IL_126;
			case 9:
			{
				bool flag;
				if (flag)
				{
					num = 0;
					continue;
				}
				goto IL_126;
			}
			case 10:
				goto IL_126;
			case 11:
				goto IL_64;
			case 12:
				return;
			case 13:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_D2;
				default:
					goto IL_15F;
				}
				break;
			case 14:
			{
				XlsConditionalFormats xlsConditionalFormats;
				if (xlsConditionalFormats.Count != 0)
				{
					num = 1;
					continue;
				}
				goto IL_126;
			}
			case 15:
				if (true)
				{
				}
				goto IL_126;
			}
			if (A_0 == null)
			{
				num = 11;
				continue;
			}
			num = 7;
			continue;
			IL_D2:
			num = 12;
			continue;
			IL_126:
			num = 6;
		}
		IL_64:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰽┿⍁⁃⍅㩇", a_));
		IL_15F:
		if (false)
		{
		}
		return;
		IL_1DF:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴽⠿❁⅃㉅େ╉≋⩍㥏♑㵓㥕㙗㭙せᡝཟၡॣݥᱧᥩ", a_));
	}

	// Token: 0x0600502A RID: 20522 RVA: 0x0031E8FC File Offset: 0x0031D8FC
	public bool ᜀ(XmlReader A_0, XlsConditionalFormats A_1, List<spr\u21A7> A_2)
	{
		int a_ = 8;
		switch (0)
		{
		default:
		{
			int num = 15;
			bool result;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 1;
						continue;
					}
					goto IL_252;
				}
				case 1:
					num = 22;
					continue;
				case 2:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 10;
						continue;
					}
					num = 13;
					continue;
				case 3:
				{
					XlsConditionalFormats xlsConditionalFormats = null;
					WorksheetConditionalFormats worksheetConditionalFormats;
					XlsRange xlsRange;
					xlsConditionalFormats = worksheetConditionalFormats.Find(xlsRange.GetRectangles());
					num = 17;
					continue;
				}
				case 4:
					if (A_1 == null)
					{
						num = 5;
						continue;
					}
					result = true;
					num = 9;
					continue;
				case 5:
					goto IL_19F;
				case 6:
				{
					XlsWorksheet xlsWorksheet;
					if (xlsWorksheet != null)
					{
						num = 11;
						continue;
					}
					goto IL_1E6;
				}
				case 7:
					goto IL_267;
				case 8:
				{
					XlsRange xlsRange;
					if (xlsRange != null)
					{
						num = 3;
						continue;
					}
					goto IL_1E6;
				}
				case 9:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䴽ㄿぁ⅃⁅", a_)))
					{
						goto IL_159;
					}
					goto IL_3B8;
				case 10:
					goto IL_28C;
				case 11:
				{
					XlsWorksheet xlsWorksheet;
					XlsRange xlsRange = xlsWorksheet.GetRangeByString(A_0.Value) as XlsRange;
					num = 8;
					continue;
				}
				case 12:
				{
					TAddr[] array = this.ᜃ(A_0.Value);
					WorksheetConditionalFormats worksheetConditionalFormats = A_1.Parent as WorksheetConditionalFormats;
					num = 26;
					continue;
				}
				case 13:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 23;
						continue;
					}
					A_0.Skip();
					num = 20;
					continue;
				case 14:
					goto IL_3FB;
				case 16:
				{
					int num2;
					TAddr[] array2;
					if (num2 >= array2.Length)
					{
						num = 24;
						continue;
					}
					TAddr taddr = array2[num2];
					Rectangle rectangle = taddr.GetRectangle();
					A_1.ᜀ(rectangle);
					num2++;
					num = 28;
					continue;
				}
				case 17:
				{
					XlsConditionalFormats xlsConditionalFormats;
					if (xlsConditionalFormats != null)
					{
						if (true)
						{
						}
						num = 30;
						continue;
					}
					goto IL_1E6;
				}
				case 18:
					goto IL_1E6;
				case 19:
					this.ᜁ(A_0, A_1, A_2);
					num = 25;
					continue;
				case 20:
					goto IL_267;
				case 21:
					num = 32;
					continue;
				case 22:
				{
					string localName;
					if (localName == RecordTableEnumerator.b("崽☿၁ㅃ⩅ⵇ", a_))
					{
						num = 19;
						continue;
					}
					goto IL_252;
				}
				case 23:
					num = 0;
					continue;
				case 24:
					goto IL_3B8;
				case 25:
					goto IL_267;
				case 26:
				{
					WorksheetConditionalFormats worksheetConditionalFormats;
					if (worksheetConditionalFormats != null)
					{
						num = 27;
						continue;
					}
					goto IL_1E6;
				}
				case 27:
				{
					WorksheetConditionalFormats worksheetConditionalFormats;
					XlsWorksheet xlsWorksheet = worksheetConditionalFormats.Parent as XlsWorksheet;
					num = 6;
					continue;
				}
				case 28:
					goto IL_3FB;
				case 29:
					goto IL_267;
				case 30:
				{
					XlsConditionalFormats xlsConditionalFormats;
					A_1 = xlsConditionalFormats;
					result = false;
					num = 18;
					continue;
				}
				case 31:
					goto IL_2FE;
				case 32:
					if (A_0.LocalName != RecordTableEnumerator.b("崽⼿ⱁ⁃⽅㱇⍉⍋⁍ㅏ㹑ቓ㥕⩗㝙㵛⩝ᑟୡ੣ť", a_))
					{
						num = 31;
						continue;
					}
					num = 4;
					continue;
				}
				if (A_0 != null)
				{
					num = 21;
					continue;
				}
				goto IL_3E7;
				IL_159:
				num = 12;
				continue;
				IL_1E6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_159;
				default:
				{
					if (false)
					{
					}
					TAddr[] array;
					TAddr[] array2 = array;
					int num2 = 0;
					num = 14;
					continue;
				}
				}
				IL_252:
				A_0.Read();
				num = 29;
				continue;
				IL_267:
				num = 2;
				continue;
				IL_3B8:
				A_0.Read();
				num = 7;
				continue;
				IL_3FB:
				num = 16;
			}
			IL_19F:
			throw new ArgumentNullException(RecordTableEnumerator.b("崽⼿ⱁ⁃⽅㱇⍉⍋⁍ㅏ㹑ቓ㥕⩗㝙㵛⩝፟", a_));
			IL_28C:
			A_0.Read();
			return result;
			IL_2FE:
			IL_3E7:
			throw new ArgumentException(RecordTableEnumerator.b("䰽┿⍁⁃⍅㩇", a_));
		}
		}
	}

	// Token: 0x0600502B RID: 20523 RVA: 0x0031ED34 File Offset: 0x0031DD34
	public void ᜁ(XmlReader A_0, XlsConditionalFormats A_1, List<spr\u21A7> A_2)
	{
		int a_ = 10;
		switch (0)
		{
		default:
			for (;;)
			{
				bool flag = false;
				bool flag2 = false;
				ConditionalFormatType formatType = ConditionalFormatType.CellValue;
				ComparisonOperatorType @operator = ComparisonOperatorType.Between;
				int num = 1;
				for (;;)
				{
					sprᲖ sprᲖ;
					switch (num)
					{
					case 0:
						@operator = this.ᜀ(A_0.Value, out flag2);
						num = 2;
						continue;
					case 1:
						if (A_0.MoveToAttribute(RecordTableEnumerator.b("㐿㭁㑃⍅", a_)))
						{
							if (true)
							{
							}
							num = 7;
							continue;
						}
						goto IL_E2;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_BD;
						default:
							if (false)
							{
							}
							goto IL_C2;
						}
						break;
					case 3:
						return;
					case 4:
						if (flag2)
						{
							num = 6;
							continue;
						}
						goto IL_112;
					case 5:
						sprᲖ = (A_1.AddCondition() as sprᲖ);
						sprᲖ.FormatType = formatType;
						num = 4;
						continue;
					case 6:
						goto IL_BD;
					case 7:
						formatType = this.ᜁ(A_0.Value, out flag);
						num = 8;
						continue;
					case 8:
						goto IL_E2;
					case 9:
						goto IL_112;
					case 10:
						if (flag)
						{
							num = 5;
							continue;
						}
						return;
					case 11:
						if (A_0.MoveToAttribute(RecordTableEnumerator.b("⼿㉁⅃㑅⥇㹉⍋㱍", a_)))
						{
							num = 0;
							continue;
						}
						goto IL_C2;
					}
					break;
					IL_BD:
					sprᲖ.Operator = @operator;
					num = 9;
					continue;
					IL_C2:
					num = 10;
					continue;
					IL_E2:
					num = 11;
					continue;
					IL_112:
					this.ᜀ(A_0, sprᲖ, A_2);
					num = 3;
				}
			}
			return;
		}
	}

	// Token: 0x0600502C RID: 20524 RVA: 0x0031EEE4 File Offset: 0x0031DEE4
	private void ᜀ(XmlReader A_0, sprᲖ A_1, List<spr\u21A7> A_2)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_247;
				case 2:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 3;
						continue;
					}
					num = 18;
					continue;
				case 3:
					goto IL_26C;
				case 4:
					num = 9;
					continue;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_221;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 22;
						continue;
					}
					break;
				case 6:
					A_0.Read();
					num = 15;
					continue;
				case 7:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 28;
						continue;
					}
					goto IL_232;
				}
				case 8:
					num = 16;
					continue;
				case 9:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("ⱇ⭉㡋⽍቏㍑♓", a_)))
					{
						num = 5;
						continue;
					}
					XlsConditionalFormat xlsConditionalFormat;
					this.ᜀ(A_0, xlsConditionalFormat.InnerDataBar, xlsConditionalFormat.Workbook);
					num = 1;
					continue;
				}
				case 10:
				{
					int index = XmlConvert.ToInt32(A_0.Value);
					spr\u21A7 spr_u21A = A_2[index];
					spr_u21A.ᜀ(A_1);
					num = 14;
					continue;
				}
				case 11:
					goto IL_247;
				case 12:
					goto IL_247;
				case 13:
					goto IL_3B5;
				case 14:
					goto IL_20A;
				case 15:
					goto IL_247;
				case 16:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("⭇╉⁋⅍≏ő㝓㝕㑗㽙", a_)))
					{
						num = 21;
						continue;
					}
					XlsConditionalFormat xlsConditionalFormat;
					this.ᜀ(A_0, xlsConditionalFormat.ColorScale.Wrapped, xlsConditionalFormat.Workbook);
					num = 11;
					continue;
				}
				case 17:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("ⱇ㉉⩋ݍ㑏", a_)))
					{
						num = 10;
						continue;
					}
					goto IL_20A;
				case 18:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 20;
						continue;
					}
					A_0.Read();
					num = 12;
					continue;
				case 19:
					if (!A_0.IsEmptyElement)
					{
						goto IL_221;
					}
					goto IL_3FA;
				case 20:
				{
					XlsConditionalFormat xlsConditionalFormat = (XlsConditionalFormat)A_1;
					num = 7;
					continue;
				}
				case 21:
					num = 25;
					continue;
				case 22:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("ⅇ⥉⍋⁍͏㝑⁓", a_)))
					{
						num = 8;
						continue;
					}
					XlsConditionalFormat xlsConditionalFormat;
					this.ᜀ(A_0, xlsConditionalFormat.IconSet.Wrapped, xlsConditionalFormat.Workbook);
					num = 23;
					continue;
				}
				case 23:
					goto IL_247;
				case 24:
					goto IL_247;
				case 25:
					goto IL_232;
				case 26:
					goto IL_BA;
				case 27:
					goto IL_247;
				case 28:
					num = 30;
					continue;
				case 29:
					if (A_1 == null)
					{
						num = 13;
						continue;
					}
					num = 17;
					continue;
				case 30:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("⹇╉㹋⍍╏㹑㕓", a_)))
					{
						num = 4;
						continue;
					}
					XlsConditionalFormat xlsConditionalFormat;
					this.ᜀ(A_0, xlsConditionalFormat);
					num = 27;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 26;
					continue;
				}
				num = 29;
				continue;
				IL_20A:
				num = 19;
				continue;
				IL_221:
				num = 6;
				continue;
				IL_232:
				A_0.Read();
				num = 24;
				continue;
				IL_247:
				num = 2;
			}
			IL_BA:
			throw new ArgumentNullException(RecordTableEnumerator.b("㩇⽉ⵋ⩍㕏⁑", a_));
			IL_26C:
			goto IL_3FA;
			IL_3B5:
			throw new ArgumentNullException(RecordTableEnumerator.b("⭇╉≋ࡍ㽏⁑㥓㝕ⱗ", a_));
			IL_3FA:
			A_0.Read();
			return;
		}
		}
	}

	// Token: 0x0600502D RID: 20525 RVA: 0x0031F2F4 File Offset: 0x0031E2F4
	private void ᜀ(XmlReader A_0, IColorScale A_1, XlsWorkbook A_2)
	{
		int a_ = 7;
		switch (0)
		{
		default:
		{
			int num = 22;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_98;
				case 1:
					goto IL_1E3;
				case 2:
				{
					A_0.Read();
					int num2 = 0;
					num = 1;
					continue;
				}
				case 3:
					num = 9;
					continue;
				case 4:
					goto IL_14E;
				case 5:
					if (!A_0.IsEmptyElement)
					{
						num = 2;
						continue;
					}
					goto IL_30C;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						num = 15;
						continue;
					}
					break;
				case 7:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("帼夾㝀ⱂ", a_)))
					{
						num = 6;
						continue;
					}
					spr\u24B3 spr_u24B = new spr\u24B3();
					this.ᜀ(A_0, A_2, spr_u24B);
					IList<IColorConditionValue> criteria;
					criteria.Add(spr_u24B);
					num = 23;
					continue;
				}
				case 8:
					goto IL_117;
				case 9:
					goto IL_11C;
				case 10:
					goto IL_205;
				case 11:
					goto IL_1E3;
				case 12:
					if (A_1 == null)
					{
						num = 4;
						continue;
					}
					num = 18;
					continue;
				case 13:
					goto IL_1E3;
				case 14:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 10;
						continue;
					}
					num = 16;
					continue;
				case 15:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("帼倾ⵀⱂ㝄", a_)))
					{
						num = 3;
						continue;
					}
					int num2;
					IList<IColorConditionValue> criteria;
					criteria[num2].FormatColor = this.ᜏ(A_0).ᜁ(A_2);
					num2++;
					num = 20;
					continue;
				}
				case 16:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 17;
						continue;
					}
					A_0.Read();
					num = 11;
					continue;
				case 17:
					num = 19;
					continue;
				case 18:
				{
					if (A_0.LocalName != RecordTableEnumerator.b("帼倾ⵀⱂ㝄ᑆ⩈⩊⅌⩎", a_))
					{
						num = 8;
						continue;
					}
					IList<IColorConditionValue> criteria = A_1.Criteria;
					criteria.Clear();
					num = 5;
					continue;
				}
				case 19:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						if (true)
						{
						}
						num = 21;
						continue;
					}
					goto IL_11C;
				}
				case 20:
					goto IL_1E3;
				case 21:
					num = 7;
					continue;
				case 23:
					goto IL_1E3;
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				num = 12;
				continue;
				IL_11C:
				A_0.Skip();
				num = 13;
				continue;
				IL_1E3:
				num = 14;
			}
			IL_98:
			throw new ArgumentNullException(RecordTableEnumerator.b("似娾⁀❂⁄㕆", a_));
			IL_117:
			throw new XmlException();
			IL_14E:
			throw new ArgumentNullException(RecordTableEnumerator.b("帼倾ⵀⱂ㝄ᑆ⩈⩊⅌⩎", a_));
			IL_205:
			IL_30C:
			A_0.Read();
			return;
		}
		}
	}

	// Token: 0x0600502E RID: 20526 RVA: 0x0031F614 File Offset: 0x0031E614
	private void ᜀ(XmlReader A_0, spr\u24CD A_1, IWorkbook A_2)
	{
		int a_ = 4;
		int num = 29;
		for (;;)
		{
			sprἫ sprἫ;
			int num2;
			switch (num)
			{
			case 0:
				A_1.ᜀ(sprἫ);
				num = 35;
				continue;
			case 1:
				goto IL_14A;
			case 2:
				if (num2 == 0)
				{
					if (true)
					{
					}
					num = 0;
					continue;
				}
				num = 22;
				continue;
			case 3:
				goto IL_1CC;
			case 4:
				goto IL_31C;
			case 5:
				goto IL_245;
			case 6:
				num = 33;
				continue;
			case 7:
				goto IL_437;
			case 8:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("夹医刽⼿ぁ", a_)))
				{
					num = 9;
					continue;
				}
				A_1.ᜀ(this.ᜏ(A_0).ᜁ(A_2));
				num = 28;
				continue;
			}
			case 9:
				num = 3;
				continue;
			case 10:
				goto IL_245;
			case 11:
				A_1.ᜁ(XmlConvert.ToInt32(A_0.Value));
				num = 26;
				continue;
			case 12:
				goto IL_245;
			case 13:
				A_1.ᜀ(XmlConvert.ToInt32(A_0.Value));
				num = 14;
				continue;
			case 14:
				goto IL_2A8;
			case 15:
				if (!A_0.IsEmptyElement)
				{
					num = 25;
					continue;
				}
				goto IL_487;
			case 16:
				if (A_0.LocalName != RecordTableEnumerator.b("帹崻䨽ℿA╃㑅", a_))
				{
					num = 38;
					continue;
				}
				num = 37;
				continue;
			case 17:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 31;
					continue;
				}
				num = 18;
				continue;
			case 18:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 6;
					continue;
				}
				A_0.Read();
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_317;
				default:
					if (false)
					{
					}
					num = 10;
					continue;
				}
				break;
			case 19:
				goto IL_317;
			case 20:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("圹崻䘽ి❁⩃ⅅ㱇≉", a_)))
				{
					num = 11;
					continue;
				}
				goto IL_36F;
			case 21:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䤹吻儽㜿ᑁ╃⩅㵇⽉", a_)))
				{
					num = 23;
					continue;
				}
				goto IL_31C;
			case 22:
				if (num2 == 1)
				{
					num = 19;
					continue;
				}
				goto IL_2F7;
			case 23:
				A_1.ᜀ(XmlConvert.ToBoolean(A_0.Value));
				num = 4;
				continue;
			case 24:
				num = 34;
				continue;
			case 25:
				A_0.Read();
				num2 = 0;
				num = 12;
				continue;
			case 26:
				goto IL_36F;
			case 27:
				if (spr\u24CD.ᜁ(A_1, null))
				{
					num = 7;
					continue;
				}
				num = 16;
				continue;
			case 28:
				goto IL_245;
			case 30:
				goto IL_245;
			case 31:
				goto IL_268;
			case 32:
				num = 8;
				continue;
			case 33:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 24;
					continue;
				}
				goto IL_1CC;
			}
			case 34:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("夹娻䠽⼿", a_)))
				{
					num = 32;
					continue;
				}
				sprἫ = new sprἫ();
				this.ᜀ(A_0, A_2, sprἫ);
				num = 2;
				continue;
			}
			case 35:
				goto IL_14A;
			case 36:
				goto IL_C9;
			case 37:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("圹唻倽ి❁⩃ⅅ㱇≉", a_)))
				{
					num = 13;
					continue;
				}
				goto IL_2A8;
			case 38:
				goto IL_18F;
			}
			if (A_0 == null)
			{
				num = 36;
				continue;
			}
			num = 27;
			continue;
			IL_14A:
			num2++;
			num = 30;
			continue;
			IL_1CC:
			A_0.Skip();
			num = 5;
			continue;
			IL_245:
			num = 17;
			continue;
			IL_2A8:
			num = 20;
			continue;
			IL_317:
			A_1.ᜁ(sprἫ);
			num = 1;
			continue;
			IL_31C:
			A_0.MoveToElement();
			num = 15;
			continue;
			IL_36F:
			num = 21;
		}
		IL_C9:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠹夻弽␿❁㙃", a_));
		IL_18F:
		throw new XmlException();
		IL_268:
		goto IL_487;
		IL_2F7:
		throw new XmlException();
		IL_437:
		throw new ArgumentNullException(RecordTableEnumerator.b("帹崻䨽ℿA╃㑅", a_));
		IL_487:
		A_0.Read();
	}

	// Token: 0x0600502F RID: 20527 RVA: 0x0031FAB0 File Offset: 0x0031EAB0
	private void ᜀ(XmlReader A_0, IIconSet A_1, IWorkbook A_2)
	{
		int a_ = 7;
		switch (0)
		{
		default:
		{
			int num = 29;
			for (;;)
			{
				IList<IConditionValue> iconCriteria;
				switch (num)
				{
				case 0:
					num = 3;
					continue;
				case 1:
					A_1.ShowIconOnly = !XmlConvert.ToBoolean(A_0.Value);
					num = 5;
					continue;
				case 2:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("丼圾⹀㑂ፄ♆╈㹊⡌", a_)))
					{
						num = 1;
						continue;
					}
					goto IL_3D8;
				case 3:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 11;
						continue;
					}
					goto IL_20F;
				}
				case 4:
					if (A_0.LocalName != RecordTableEnumerator.b("吼尾⹀ⵂᙄ≆㵈", a_))
					{
						num = 22;
						continue;
					}
					num = 17;
					continue;
				case 5:
					goto IL_3D8;
				case 6:
					A_1.IconSet = (IconSetType)Array.IndexOf<string>(spr\u21EF.ᜥ, A_0.Value);
					num = 9;
					continue;
				case 7:
					goto IL_3A5;
				case 8:
				{
					string localName;
					if (localName == RecordTableEnumerator.b("帼夾㝀ⱂ", a_))
					{
						num = 26;
						continue;
					}
					goto IL_20F;
				}
				case 9:
					goto IL_2E2;
				case 10:
				{
					A_0.Read();
					int num2 = 0;
					num = 20;
					continue;
				}
				case 11:
					num = 8;
					continue;
				case 12:
					if (!A_0.IsEmptyElement)
					{
						num = 10;
						continue;
					}
					goto IL_42E;
				case 13:
					A_1.IsReverseOrder = !XmlConvert.ToBoolean(A_0.Value);
					num = 7;
					continue;
				case 14:
					goto IL_429;
				case 15:
					goto IL_223;
				case 16:
					goto IL_248;
				case 17:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("吼尾⹀ⵂᙄ≆㵈", a_)))
					{
						num = 6;
						continue;
					}
					goto IL_2E2;
				case 18:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 0;
						continue;
					}
					A_0.Read();
					num = 27;
					continue;
				case 19:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("䴼娾㍀⁂⁄⥆㵈", a_)))
					{
						num = 30;
						continue;
					}
					goto IL_372;
				case 20:
					goto IL_223;
				case 21:
					if (A_1 == null)
					{
						num = 14;
						continue;
					}
					num = 4;
					continue;
				case 22:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_22F;
					default:
						goto IL_127;
					}
					break;
				case 23:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("似娾㝀♂㝄㑆ⱈ", a_)))
					{
						num = 13;
						continue;
					}
					goto IL_3A5;
				case 24:
					goto IL_BE;
				case 25:
					goto IL_372;
				case 26:
				{
					sprἫ sprἫ = new sprἫ();
					this.ᜀ(A_0, A_2, sprἫ);
					int num2;
					iconCriteria[num2] = sprἫ;
					num2++;
					num = 28;
					continue;
				}
				case 27:
					if (true)
					{
					}
					goto IL_223;
				case 28:
					goto IL_223;
				case 30:
					A_1.PercentileValues = XmlConvert.ToBoolean(A_0.Value);
					num = 25;
					continue;
				case 31:
					goto IL_22F;
				}
				if (A_0 == null)
				{
					num = 24;
					continue;
				}
				num = 21;
				continue;
				IL_22F:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 16;
					continue;
				}
				num = 18;
				continue;
				IL_20F:
				A_0.Skip();
				num = 15;
				continue;
				IL_223:
				num = 31;
				continue;
				IL_2E2:
				num = 19;
				continue;
				IL_372:
				num = 23;
				continue;
				IL_3A5:
				num = 2;
				continue;
				IL_3D8:
				A_0.MoveToElement();
				iconCriteria = A_1.IconCriteria;
				num = 12;
			}
			IL_BE:
			throw new ArgumentNullException(RecordTableEnumerator.b("似娾⁀❂⁄㕆", a_));
			IL_127:
			if (false)
			{
			}
			throw new XmlException();
			IL_248:
			goto IL_42E;
			IL_429:
			throw new ArgumentNullException(RecordTableEnumerator.b("吼尾⹀ⵂᙄ≆㵈", a_));
			IL_42E:
			A_0.Read();
			return;
		}
		}
	}

	// Token: 0x06005030 RID: 20528 RVA: 0x0031FEF4 File Offset: 0x0031EEF4
	private void ᜀ(XmlReader A_0, IWorkbook A_1, sprἫ A_2)
	{
		int a_ = 9;
		int num = 9;
		string a_2;
		string a_3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				a_2 = A_0.Value;
				num = 11;
				continue;
			case 1:
				goto IL_178;
			case 2:
				goto IL_66;
			case 3:
				goto IL_95;
			case 4:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䤾⁀⽂", a_)))
				{
					num = 7;
					continue;
				}
				goto IL_191;
			case 5:
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				num = 8;
				continue;
			case 6:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䬾㡀㍂⁄", a_)))
				{
					num = 0;
					continue;
				}
				goto IL_9A;
			case 7:
				a_3 = A_0.Value;
				num = 10;
				continue;
			case 8:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					if (A_0.LocalName != RecordTableEnumerator.b("尾❀㕂⩄", a_))
					{
						num = 1;
						continue;
					}
					a_2 = null;
					a_3 = null;
					num = 6;
					continue;
				}
				break;
			case 9:
				if (true)
				{
				}
				break;
			case 10:
				goto IL_126;
			case 11:
				goto IL_9A;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			num = 5;
			continue;
			IL_9A:
			num = 4;
		}
		IL_66:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴾⑀≂⅄≆㭈", a_));
		IL_95:
		throw new ArgumentNullException(RecordTableEnumerator.b("崾⹀ⱂ⹄", a_));
		IL_126:
		goto IL_191;
		IL_178:
		throw new XmlException();
		IL_191:
		ConditionValueType a_4 = this.ᜉ(a_2);
		A_2.ᜀ(a_4);
		A_2.ᜀ(a_3);
	}

	// Token: 0x06005031 RID: 20529 RVA: 0x003200A8 File Offset: 0x0031F0A8
	private ConditionValueType ᜉ(string A_0)
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
		return (ConditionValueType)Array.IndexOf<string>(spr\u21EF.ᜤ, A_0);
	}

	// Token: 0x06005032 RID: 20530 RVA: 0x003200F0 File Offset: 0x0031F0F0
	private void ᜀ(XmlReader A_0, XlsConditionalFormat A_1)
	{
		int a_ = 19;
		int num = 6;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				if (A_0.LocalName == RecordTableEnumerator.b("⽈⑊㽌≎⑐㽒㑔", a_))
				{
					num = 8;
					continue;
				}
				return;
			case 1:
				A_0.Read();
				A_1.ᜀ(this.ᜊ, A_0.Value, true);
				A_0.Skip();
				A_0.Skip();
				num = 5;
				continue;
			case 2:
				goto IL_5E;
			case 3:
				return;
			case 4:
				if (A_1 == null)
				{
					num = 9;
					continue;
				}
				num = 7;
				continue;
			case 5:
				goto IL_14D;
			case 7:
				if (A_0.LocalName == RecordTableEnumerator.b("⽈⑊㽌≎⑐㽒㑔", a_))
				{
					num = 1;
					continue;
				}
				goto IL_14D;
			case 8:
				A_0.Read();
				A_1.ᜀ(this.ᜊ, A_0.Value, false);
				A_0.Skip();
				A_0.Skip();
				num = 3;
				continue;
			case 9:
				goto IL_E5;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			num = 4;
			continue;
			IL_14D:
			num = 0;
		}
		IL_5E:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_183:
			throw new ArgumentNullException(RecordTableEnumerator.b("⩈ൊ≌㵎㱐㉒⅔", a_));
		default:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㭈⹊ⱌ⭎㑐⅒", a_));
		}
		IL_E5:
		goto IL_183;
	}

	// Token: 0x06005033 RID: 20531 RVA: 0x00320294 File Offset: 0x0031F294
	internal static void ᜀ(XmlReader A_0, IPageSetupBase A_1)
	{
		int a_ = 4;
		if (true)
		{
		}
		int num = 7;
		for (;;)
		{
			XlsPageSetup xlsPageSetup;
			switch (num)
			{
			case 0:
				A_1.CenterHorizontally = (A_0.MoveToAttribute(RecordTableEnumerator.b("刹医䰽⤿㡁⭃⡅㱇⭉⁋്㕏㱑⁓㍕⩗㽙㡛", a_)) && XmlConvert.ToBoolean(A_0.Value));
				num = 1;
				continue;
			case 1:
				goto IL_231;
			case 2:
				goto IL_1A8;
			case 3:
				goto IL_F0;
			case 4:
				goto IL_64;
			case 5:
				num = 9;
				continue;
			case 6:
				goto IL_1E3;
			case 8:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1A8;
				default:
					goto IL_A3;
				}
				break;
			case 9:
				xlsPageSetup.IsPrintGridlines = (A_0.MoveToAttribute(RecordTableEnumerator.b("崹主圽␿แⵃ⡅ⵇ㥉", a_)) && XmlConvert.ToBoolean(A_0.Value));
				num = 11;
				continue;
			case 10:
				if (A_1 == null)
				{
					num = 8;
					continue;
				}
				num = 13;
				continue;
			case 11:
				xlsPageSetup.IsPrintGridlines = (A_0.MoveToAttribute(RecordTableEnumerator.b("崹主圽␿แⵃ⡅ⵇ㥉", a_)) && XmlConvert.ToBoolean(A_0.Value));
				num = 2;
				continue;
			case 12:
				if (xlsPageSetup != null)
				{
					num = 5;
					continue;
				}
				goto IL_1E3;
			case 13:
				if (A_0.LocalName != RecordTableEnumerator.b("䨹主圽⸿㙁ୃ㙅㱇⍉⍋⁍⍏", a_))
				{
					num = 3;
					continue;
				}
				xlsPageSetup = (A_1 as XlsPageSetup);
				num = 12;
				continue;
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			num = 10;
			continue;
			IL_1A8:
			xlsPageSetup.IsPrintHeadings = (A_0.MoveToAttribute(RecordTableEnumerator.b("刹夻弽␿⭁⩃ⅅ㭇", a_)) && XmlConvert.ToBoolean(A_0.Value));
			num = 6;
			continue;
			IL_1E3:
			num = 0;
		}
		IL_64:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠹夻弽␿❁㙃", a_));
		IL_A3:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䨹崻夽┿ᅁ⅃㉅㵇㩉", a_));
		IL_F0:
		throw new XmlException(RecordTableEnumerator.b("漹刻嬽㠿㉁⅃╅㱇⽉⡋湍⡏㽑㡓癕ⱗ㭙㭛灝", a_));
		IL_231:
		A_1.CenterVertically = (A_0.MoveToAttribute(RecordTableEnumerator.b("䰹夻䰽㐿⭁❃❅⑇ॉ⥋⁍⑏㝑♓㍕㱗", a_)) && XmlConvert.ToBoolean(A_0.Value));
		A_0.Read();
	}

	// Token: 0x06005034 RID: 20532 RVA: 0x00320508 File Offset: 0x0031F508
	internal static void ᜀ(XmlReader A_0, IPageSetupBase A_1, spr\u171C A_2)
	{
		int a_ = 12;
		for (;;)
		{
			int num = 22;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_CF;
				case 1:
					A_1.RightMargin = XmlConvert.ToDouble(A_0.Value);
					num = 0;
					continue;
				case 2:
					if (A_0.MoveToAttribute(A_2.ᜅ()))
					{
						num = 7;
						continue;
					}
					goto IL_19F;
				case 3:
					goto IL_19F;
				case 4:
					if (A_0.MoveToAttribute(A_2.ᜂ()))
					{
						num = 1;
						continue;
					}
					goto IL_CF;
				case 5:
					goto IL_9E;
				case 6:
					goto IL_A3;
				case 7:
					A_1.HeaderMarginInch = XmlConvert.ToDouble(A_0.Value);
					num = 3;
					continue;
				case 8:
					if (A_0.MoveToAttribute(A_2.ᜆ()))
					{
						num = 12;
						continue;
					}
					goto IL_309;
				case 9:
					goto IL_208;
				case 10:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_CF;
					default:
						if (false)
						{
						}
						if (A_0.LocalName != A_2.ᜀ())
						{
							num = 17;
							continue;
						}
						num = 14;
						continue;
					}
					break;
				case 11:
					A_1.TopMargin = XmlConvert.ToDouble(A_0.Value);
					num = 6;
					continue;
				case 12:
					A_1.FooterMarginInch = XmlConvert.ToDouble(A_0.Value);
					num = 21;
					continue;
				case 13:
					if (A_0.MoveToAttribute(A_2.ᜄ()))
					{
						num = 23;
						continue;
					}
					goto IL_208;
				case 14:
					if (A_0.MoveToAttribute(A_2.ᜁ()))
					{
						num = 16;
						continue;
					}
					goto IL_1C8;
				case 15:
					if (A_0.MoveToAttribute(A_2.ᜃ()))
					{
						num = 11;
						continue;
					}
					goto IL_A3;
				case 16:
					A_1.LeftMargin = XmlConvert.ToDouble(A_0.Value);
					num = 18;
					continue;
				case 17:
					goto IL_140;
				case 18:
					goto IL_1C8;
				case 19:
					goto IL_17C;
				case 20:
					if (A_1 == null)
					{
						num = 19;
						continue;
					}
					num = 10;
					continue;
				case 21:
					goto IL_250;
				case 22:
					if (true)
					{
					}
					if (A_0 == null)
					{
						num = 5;
						continue;
					}
					num = 20;
					continue;
				case 23:
					A_1.BottomMargin = XmlConvert.ToDouble(A_0.Value);
					num = 9;
					continue;
				}
				break;
				IL_A3:
				num = 13;
				continue;
				IL_CF:
				num = 15;
				continue;
				IL_19F:
				num = 8;
				continue;
				IL_1C8:
				num = 4;
				continue;
				IL_208:
				num = 2;
			}
		}
		IL_9E:
		throw new ArgumentNullException(RecordTableEnumerator.b("ぁ⅃❅ⱇ⽉㹋", a_));
		IL_140:
		throw new XmlException(RecordTableEnumerator.b("ᝁ⩃⍅ぇ㩉⥋ⵍ⑏㝑こ癕⁗㝙せ繝ᑟͣ͡䡥", a_));
		IL_17C:
		throw new ArgumentNullException(RecordTableEnumerator.b("㉁╃ⅅⵇ᥉⥋㩍╏≑", a_));
		IL_250:
		IL_309:
		A_0.MoveToElement();
		A_0.Skip();
	}

	// Token: 0x06005035 RID: 20533 RVA: 0x0032082C File Offset: 0x0031F82C
	internal static void ᜁ(XmlReader A_0, XlsPageSetupBase A_1)
	{
		int a_ = 8;
		int num = 19;
		for (;;)
		{
			int num2;
			int num3;
			int num4;
			XlsPageSetup xlsPageSetup;
			switch (num)
			{
			case 0:
				goto IL_6A4;
			case 1:
				num2 = XmlConvert.ToInt32(A_0.Value);
				num = 29;
				continue;
			case 2:
				num3 = num2;
				goto IL_4F3;
			case 3:
				goto IL_456;
			case 4:
				goto IL_422;
			case 5:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("堽⤿ぁ㝃㉅ᡇ⭉⭋⭍ṏ❑㥓㑕㵗⡙", a_)))
				{
					num = 43;
					continue;
				}
				goto IL_422;
			case 6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_487;
				default:
					if (false)
					{
					}
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("丽ℿ╁⅃ॅ㩇⹉⥋㱍", a_)))
					{
						num = 30;
						continue;
					}
					goto IL_15B;
				}
				break;
			case 7:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("儽㈿⭁⅃⡅㱇⭉㡋❍㽏㱑", a_)))
				{
					num = 11;
					continue;
				}
				goto IL_76B;
			case 8:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("丽ℿ㉁⅃㑅ᭇ⍉㙋⭍", a_)))
				{
					num = 38;
					continue;
				}
				A_1.PaperSize = PaperSizeType.PaperLetter;
				num = 15;
				continue;
			case 9:
				goto IL_79C;
			case 10:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䠽┿ぁぃ⽅⭇⭉⁋੍⁏㭑", a_)))
				{
					num = 32;
					continue;
				}
				goto IL_6A4;
			case 11:
				A_1.Orientation = (PageOrientationType)Enum.Parse(typeof(PageOrientationType), A_0.Value, true);
				num = 50;
				continue;
			case 12:
				goto IL_766;
			case 13:
				goto IL_818;
			case 14:
				goto IL_5E4;
			case 15:
				goto IL_34E;
			case 16:
				goto IL_15B;
			case 17:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("圽␿", a_)))
				{
					num = 52;
					continue;
				}
				goto IL_83B;
			case 18:
				A_1.FitToPagesWide = XmlConvert.ToInt32(A_0.Value);
				num = 14;
				continue;
			case 20:
				A_1.Draft = XmlConvert.ToBoolean(A_0.Value);
				num = 28;
				continue;
			case 21:
				num3 = 10;
				goto IL_4F3;
			case 22:
				goto IL_7D0;
			case 23:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("堽⤿㙁၃⥅G⽉╋⥍㡏♑", a_)))
				{
					num = 64;
					continue;
				}
				goto IL_594;
			case 24:
				A_1.HResolution = spr\u2306.ᜌ(A_0.Value);
				num = 40;
				continue;
			case 25:
				if (A_0.LocalName != RecordTableEnumerator.b("丽ℿ╁⅃ᕅⵇ㹉㥋㹍", a_))
				{
					num = 12;
					continue;
				}
				num = 8;
				continue;
			case 26:
				A_1.PrintComments = spr\u2306.ᜈ(A_0.Value);
				num = 9;
				continue;
			case 27:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("尽ⰿ⍁❃ⵅे⑉⡋᥍㡏㭑⁓㍕", a_)))
				{
					num = 35;
					continue;
				}
				goto IL_7D0;
			case 28:
				goto IL_229;
			case 29:
				if (num2 <= 400)
				{
					num = 62;
					continue;
				}
				num = 48;
				continue;
			case 30:
				A_1.Order = (OrderType)Enum.Parse(typeof(OrderType), A_0.Value, true);
				num = 16;
				continue;
			case 31:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("嘽⼿ぁⵃ㱅❇⑉㡋⽍㱏ᙑ⑓㽕", a_)))
				{
					num = 24;
					continue;
				}
				goto IL_3BA;
			case 32:
				A_1.VResolution = spr\u2306.ᜌ(A_0.Value);
				num = 0;
				continue;
			case 33:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("堽⤿㙁၃⥅὇⍉⡋㩍㡏", a_)))
				{
					num = 18;
					continue;
				}
				goto IL_5E4;
			case 34:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("崽⼿㉁ⵃ⍅㭇", a_)))
				{
					num = 45;
					continue;
				}
				goto IL_654;
			case 35:
				goto IL_487;
			case 36:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䴽⌿⍁⡃⍅", a_)))
				{
					num = 1;
					continue;
				}
				A_1.Zoom = 100;
				num = 3;
				continue;
			case 37:
				A_1.AutoFirstPageNumber = !XmlConvert.ToBoolean(A_0.Value);
				num = 46;
				continue;
			case 38:
				A_1.PaperSize = (PaperSizeType)XmlConvert.ToInt32(A_0.Value);
				num = 58;
				continue;
			case 39:
				A_1.PrintErrors = spr\u2306.ᜇ(A_0.Value);
				num = 60;
				continue;
			case 40:
				goto IL_3BA;
			case 41:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("嬽㈿ぁ⭃㑅㭇", a_)))
				{
					num = 39;
					continue;
				}
				goto IL_1C1;
			case 42:
				goto IL_58F;
			case 43:
				A_1.FirstPageNumber = XmlConvert.ToInt32(A_0.Value);
				num = 4;
				continue;
			case 44:
				goto IL_654;
			case 45:
				A_1.Copies = XmlConvert.ToInt32(A_0.Value);
				num = 44;
				continue;
			case 46:
				goto IL_3EE;
			case 47:
				if (A_1 == null)
				{
					if (true)
					{
					}
					num = 42;
					continue;
				}
				num = 25;
				continue;
			case 48:
				num4 = 400;
				goto IL_396;
			case 49:
				goto IL_594;
			case 50:
				goto IL_76B;
			case 51:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("䬽㌿❁Ƀ⽅㩇㥉㡋ṍㅏ㕑ㅓᡕⵗ㝙㹛㭝቟", a_)))
				{
					num = 37;
					continue;
				}
				goto IL_3EE;
			case 52:
				xlsPageSetup.RelationId = A_0.Value;
				num = 13;
				continue;
			case 53:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("崽┿⹁⡃Յ❇❉⅋⭍㹏♑❓", a_)))
				{
					num = 26;
					continue;
				}
				goto IL_79C;
			case 54:
				goto IL_456;
			case 55:
				if (num2 >= 10)
				{
					num = 61;
					continue;
				}
				num = 21;
				continue;
			case 56:
				goto IL_135;
			case 57:
				num = 17;
				continue;
			case 58:
				goto IL_34E;
			case 59:
				num4 = num2;
				goto IL_396;
			case 60:
				goto IL_1C1;
			case 61:
				num = 2;
				continue;
			case 62:
				num = 59;
				continue;
			case 63:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("娽㈿⍁≃㉅", a_)))
				{
					num = 20;
					continue;
				}
				goto IL_229;
			case 64:
				A_1.FitToPagesTall = XmlConvert.ToInt32(A_0.Value);
				num = 49;
				continue;
			case 65:
				if (xlsPageSetup != null)
				{
					num = 57;
					continue;
				}
				goto IL_83B;
			}
			if (A_0 == null)
			{
				num = 56;
				continue;
			}
			num = 47;
			continue;
			IL_15B:
			num = 7;
			continue;
			IL_1C1:
			num = 31;
			continue;
			IL_229:
			num = 53;
			continue;
			IL_34E:
			num = 36;
			continue;
			IL_396:
			num2 = num4;
			num = 55;
			continue;
			IL_3BA:
			num = 10;
			continue;
			IL_3EE:
			num = 41;
			continue;
			IL_422:
			num = 33;
			continue;
			IL_456:
			num = 5;
			continue;
			IL_487:
			A_1.BlackAndWhite = XmlConvert.ToBoolean(A_0.Value);
			num = 22;
			continue;
			IL_4F3:
			num2 = num3;
			A_1.Zoom = num2;
			num = 54;
			continue;
			IL_594:
			num = 6;
			continue;
			IL_5E4:
			num = 23;
			continue;
			IL_654:
			xlsPageSetup = (A_1 as XlsPageSetup);
			num = 65;
			continue;
			IL_6A4:
			num = 34;
			continue;
			IL_76B:
			num = 27;
			continue;
			IL_79C:
			num = 51;
			continue;
			IL_7D0:
			num = 63;
		}
		IL_135:
		throw new ArgumentNullException(RecordTableEnumerator.b("䤽㈿⭁ぃ⍅㩇", a_));
		IL_58F:
		throw new ArgumentNullException(RecordTableEnumerator.b("丽ℿ╁⅃ᕅⵇ㹉㥋㹍", a_));
		IL_766:
		throw new XmlException(RecordTableEnumerator.b("欽⸿❁㱃㙅ⵇ⥉㡋⭍㑏牑ⱓ㭕㑗穙⡛㽝ݟ䱡", a_));
		IL_818:
		IL_83B:
		A_0.MoveToElement();
		A_0.Skip();
	}

	// Token: 0x06005036 RID: 20534 RVA: 0x00321084 File Offset: 0x00320084
	internal static void ᜀ(XmlReader A_0, XlsPageSetupBase A_1)
	{
		int a_ = 4;
		int num = 9;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 6;
					continue;
				}
				num = 15;
				continue;
			case 1:
				if (!A_0.IsEmptyElement)
				{
					num = 17;
					continue;
				}
				goto IL_2CF;
			case 2:
				num = 21;
				continue;
			case 3:
				goto IL_110;
			case 4:
				goto IL_144;
			case 5:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("唹堻娽ؿⵁ⭃㉅ⵇ㡉", a_)))
				{
					num = 7;
					continue;
				}
				goto IL_1B2;
			}
			case 6:
				goto IL_20C;
			case 7:
				num = 20;
				continue;
			case 8:
				goto IL_1E9;
			case 10:
				if (true)
				{
				}
				num = 23;
				continue;
			case 11:
				goto IL_87;
			case 12:
				if (A_0.LocalName != RecordTableEnumerator.b("刹夻弽␿❁㙃E❇╉㡋⭍≏", a_))
				{
					num = 3;
					continue;
				}
				num = 1;
				continue;
			case 13:
				goto IL_1E9;
			case 14:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1B2;
				default:
					if (false)
					{
					}
					goto IL_1E9;
				}
				break;
			case 15:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 2;
					continue;
				}
				A_0.Skip();
				num = 16;
				continue;
			case 16:
				goto IL_1E9;
			case 17:
				A_0.Read();
				num = 8;
				continue;
			case 18:
				num = 5;
				continue;
			case 19:
				goto IL_1E9;
			case 20:
				goto IL_115;
			case 21:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 10;
					continue;
				}
				goto IL_115;
			}
			case 22:
				if (A_1 == null)
				{
					num = 4;
					continue;
				}
				num = 12;
				continue;
			case 23:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("唹堻娽࠿❁╃≅ⵇ㡉", a_)))
				{
					num = 18;
					continue;
				}
				string fullHeaderString = A_0.ReadElementContentAsString();
				A_1.FullHeaderString = fullHeaderString;
				num = 19;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 11;
				continue;
			}
			num = 22;
			continue;
			IL_115:
			A_0.Skip();
			num = 13;
			continue;
			IL_1B2:
			string fullFooterString = A_0.ReadElementContentAsString();
			A_1.FullFooterString = fullFooterString;
			num = 14;
			continue;
			IL_1E9:
			num = 0;
		}
		IL_87:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠹夻弽␿❁㙃", a_));
		IL_110:
		throw new XmlException(RecordTableEnumerator.b("漹刻嬽㠿㉁⅃╅㱇⽉⡋湍⡏㽑㡓癕ⱗ㭙㭛灝", a_));
		IL_144:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨹崻夽┿ᅁ⅃㉅㵇㩉", a_));
		IL_20C:
		IL_2CF:
		A_0.Read();
	}

	// Token: 0x06005037 RID: 20535 RVA: 0x00321368 File Offset: 0x00320368
	private static PrintCommentType ᜈ(string A_0)
	{
		int a_ = 12;
		int num = 5;
		PrintCommentType result;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!(A_0 == RecordTableEnumerator.b("ⱁ⭃⡅ⵇ", a_)))
				{
					num = 9;
					continue;
				}
				result = PrintCommentType.NoComments;
				num = 4;
				continue;
			case 1:
				num = 10;
				continue;
			case 2:
				num = 8;
				continue;
			case 3:
				return result;
			case 4:
				goto IL_62;
			case 6:
				if (!(A_0 == RecordTableEnumerator.b("⍁ぃͅ♇⹉", a_)))
				{
					if (true)
					{
					}
					num = 2;
					continue;
				}
				result = PrintCommentType.SheetEnd;
				num = 3;
				continue;
			case 7:
				num = 0;
				continue;
			case 8:
				goto IL_A4;
			case 9:
				IL_9F:
				num = 6;
				continue;
			case 10:
				if (!(A_0 == RecordTableEnumerator.b("⍁㝃Ʌⅇ㥉㱋≍ㅏ⭑ㅓ㉕", a_)))
				{
					num = 7;
					continue;
				}
				result = PrintCommentType.InPlace;
				num = 11;
				continue;
			case 11:
				goto IL_71;
			}
			if (A_0 != null)
			{
				num = 1;
				continue;
			}
			IL_A4:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_9F;
			default:
				goto IL_BA;
			}
		}
		IL_62:
		IL_71:
		return result;
		IL_BA:
		if (false)
		{
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㉁㙃⽅♇㹉K⅍㍏㍑⁓㽕㝗㑙", a_));
	}

	// Token: 0x06005038 RID: 20536 RVA: 0x003214E0 File Offset: 0x003204E0
	private static PrintErrorsType ᜇ(string A_0)
	{
		int a_ = 12;
		int num = 1;
		PrintErrorsType result;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 8;
				continue;
			case 2:
				goto IL_C0;
			case 3:
				if (!(A_0 == RecordTableEnumerator.b("♁╃㕅⁇", a_)))
				{
					num = 5;
					continue;
				}
				result = PrintErrorsType.Dash;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_CD;
				default:
					if (false)
					{
					}
					num = 9;
					continue;
				}
				break;
			case 4:
				num = 3;
				continue;
			case 5:
				num = 7;
				continue;
			case 6:
				goto IL_80;
			case 7:
				if (!(A_0 == RecordTableEnumerator.b("♁ⵃ㕅㡇♉ⵋ㝍㕏㙑", a_)))
				{
					num = 11;
					continue;
				}
				result = PrintErrorsType.Displayed;
				num = 12;
				continue;
			case 8:
				goto IL_1B4;
			case 9:
				goto IL_124;
			case 10:
				if (!(A_0 == RecordTableEnumerator.b("ుՃ", a_)))
				{
					num = 0;
					continue;
				}
				result = PrintErrorsType.NA;
				num = 2;
				continue;
			case 11:
				num = 10;
				continue;
			case 12:
				goto IL_71;
			case 13:
				goto IL_CD;
			case 14:
				if (!(A_0 == RecordTableEnumerator.b("⁁⡃❅♇ⅉ", a_)))
				{
					num = 4;
					continue;
				}
				result = PrintErrorsType.Blank;
				num = 6;
				continue;
			}
			if (A_0 != null)
			{
				num = 13;
				continue;
			}
			goto IL_129;
			IL_CD:
			num = 14;
		}
		IL_71:
		IL_80:
		return result;
		IL_C0:
		if (true)
		{
		}
		IL_124:
		return result;
		IL_129:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㉁㙃⽅♇㹉K⅍㍏㍑⁓㽕㝗㑙", a_));
		IL_1B4:
		goto IL_129;
	}

	// Token: 0x06005039 RID: 20537 RVA: 0x003216A8 File Offset: 0x003206A8
	private void ᜂ(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 10;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_50;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_12C;
				default:
					goto IL_163;
				}
				break;
			case 2:
			{
				XlsHyperLinksCollection innerHyperLinks;
				RelationsCollection a_2;
				this.ᜀ(A_0, A_1, innerHyperLinks, a_2);
				num = 5;
				continue;
			}
			case 4:
				goto IL_C1;
			case 5:
				goto IL_F7;
			case 6:
				goto IL_BF;
			case 7:
				if (A_0.LocalName == RecordTableEnumerator.b("⠿㭁㑃⍅㩇♉╋⁍㭏", a_))
				{
					num = 2;
					continue;
				}
				goto IL_F7;
			case 8:
			{
				if (A_1 == null)
				{
					num = 6;
					continue;
				}
				XlsHyperLinksCollection innerHyperLinks = A_1.InnerHyperLinks;
				RelationsCollection a_2 = A_1.DataHolder.ᜇ();
				A_0.Read();
				goto IL_12C;
			}
			case 9:
				goto IL_C1;
			case 10:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 1;
					continue;
				}
				num = 7;
				continue;
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			num = 8;
			continue;
			IL_C1:
			num = 10;
			continue;
			IL_F7:
			A_0.Skip();
			if (true)
			{
			}
			num = 4;
			continue;
			IL_12C:
			num = 9;
		}
		IL_50:
		throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁╃≅ⵇ㡉", a_));
		IL_BF:
		throw new ArgumentNullException(RecordTableEnumerator.b("㌿⩁⅃⍅㱇", a_));
		IL_163:
		if (false)
		{
		}
		A_0.Skip();
	}

	// Token: 0x0600503A RID: 20538 RVA: 0x00321824 File Offset: 0x00320824
	private void ᜀ(XmlReader A_0, XlsWorksheet A_1, XlsHyperLinksCollection A_2, RelationsCollection A_3)
	{
		int a_ = 0;
		HyperLink hyperLink;
		for (;;)
		{
			IL_09:
			switch (0)
			{
			default:
			{
				int num = 26;
				for (;;)
				{
					string text;
					string text2;
					string value;
					switch (num)
					{
					case 0:
						goto IL_253;
					case 1:
					{
						IXLSRange ixlsrange;
						hyperLink.TextToDisplay = (ixlsrange.HasFormula ? ixlsrange.FormulaStringValue : ixlsrange.Text);
						num = 16;
						continue;
					}
					case 2:
						if (A_0.MoveToAttribute(RecordTableEnumerator.b("張尷", a_), RecordTableEnumerator.b("帵䰷丹䰻н漿流㝃╅⁇⽉⅋⽍⍏籑㭓♕㵗㑙⑛㍝౟ѡୣᑥէ୩ᡫᵭ幯ᵱٳᅵ坷ᕹ᩻᡽슅曆ﲑ릕ꪗꪙ겛ꢝ辟킡솣쪥즧\udea9얫솭\udeaf솱\udcb3\udfb5좷즹", a_)))
						{
							num = 29;
							continue;
						}
						hyperLink.Type = HyperLinkType.Workbook;
						hyperLink.SetAddress(text, false);
						num = 28;
						continue;
					case 3:
						hyperLink.ScreenTip = A_0.Value;
						num = 30;
						continue;
					case 4:
						if (A_0.MoveToAttribute(RecordTableEnumerator.b("䐵崷尹", a_)))
						{
							num = 24;
							continue;
						}
						goto IL_33F;
					case 5:
						text = A_0.Value;
						num = 25;
						continue;
					case 6:
						if (A_0.MoveToAttribute(RecordTableEnumerator.b("刵儷䤹䰻刽ℿ㭁", a_)))
						{
							num = 13;
							continue;
						}
						goto IL_54B;
					case 7:
						if (!text2.StartsWith(RecordTableEnumerator.b("嬵夷匹倻䨽⼿", a_)))
						{
							num = 33;
							continue;
						}
						goto IL_406;
					case 8:
						num = 6;
						continue;
					case 9:
						if (A_0.MoveToAttribute(RecordTableEnumerator.b("娵圷夹崻䨽⤿ⵁ⩃", a_)))
						{
							num = 5;
							continue;
						}
						goto IL_28D;
					case 10:
						goto IL_4FA;
					case 11:
						text2 = text2.Remove(0, RecordTableEnumerator.b("倵儷嘹夻н漿流歃", a_).Length);
						num = 19;
						continue;
					case 12:
						if (text2.StartsWith(RecordTableEnumerator.b("樵搷", a_)))
						{
							num = 31;
							continue;
						}
						num = 7;
						continue;
					case 13:
						hyperLink.TextToDisplay = A_0.Value;
						num = 0;
						continue;
					case 14:
						goto IL_476;
					case 15:
						goto IL_476;
					case 16:
						goto IL_33F;
					case 17:
						goto IL_F0;
					case 18:
						if (text2.StartsWith(RecordTableEnumerator.b("倵儷嘹夻н漿流歃", a_)))
						{
							num = 11;
							continue;
						}
						goto IL_16A;
					case 19:
						goto IL_16A;
					case 20:
						if (text2.IndexOf(RecordTableEnumerator.b("వ᜷ᔹ", a_)) != -1)
						{
							num = 32;
							continue;
						}
						hyperLink.Type = HyperLinkType.File;
						num = 15;
						continue;
					case 21:
						if (A_1 == null)
						{
							num = 10;
							continue;
						}
						hyperLink = new HyperLink((spr\u2158)this.ᜉ.AppImplementation, A_2);
						text = string.Empty;
						num = 4;
						continue;
					case 22:
						if (A_0.MoveToAttribute(RecordTableEnumerator.b("䈵圷唹倻䨽⤿㉁", a_)))
						{
							num = 3;
							continue;
						}
						goto IL_3BB;
					case 23:
						if (hyperLink.TextToDisplay == string.Empty)
						{
							num = 8;
							continue;
						}
						goto IL_54B;
					case 24:
					{
						TAddr taddr = this.ᜂ(A_0.Value);
						IXLSRange ixlsrange = A_1[taddr.FirstRow + 1, taddr.FirstCol + 1, taddr.LastRow + 1, taddr.LastCol + 1];
						hyperLink.Range = (ixlsrange as CellRange);
						ixlsrange = A_1[taddr.FirstRow + 1, taddr.FirstCol + 1];
						num = 1;
						continue;
					}
					case 25:
						goto IL_28D;
					case 26:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_09;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 27:
						goto IL_476;
					case 28:
						goto IL_51C;
					case 29:
					{
						value = A_0.Value;
						sprᦨ sprᦨ = A_3[value];
						text2 = HttpUtility.UrlDecode(sprᦨ.ᜂ());
						num = 18;
						continue;
					}
					case 30:
						goto IL_3BB;
					case 31:
						hyperLink.Type = HyperLinkType.Unc;
						num = 27;
						continue;
					case 32:
						goto IL_406;
					case 33:
						num = 20;
						continue;
					case 34:
						goto IL_51C;
					}
					if (A_0 == null)
					{
						num = 17;
						continue;
					}
					num = 21;
					continue;
					IL_16A:
					num = 12;
					continue;
					IL_28D:
					num = 2;
					continue;
					IL_33F:
					num = 22;
					continue;
					IL_3BB:
					num = 9;
					continue;
					IL_406:
					hyperLink.Type = HyperLinkType.Url;
					num = 14;
					continue;
					IL_476:
					if (true)
					{
					}
					hyperLink.SetAddress(text2, false);
					hyperLink.SetSubAddress(text);
					A_3.Remove(value);
					num = 34;
					continue;
					IL_51C:
					num = 23;
				}
				break;
			}
			}
		}
		IL_F0:
		throw new ArgumentNullException(RecordTableEnumerator.b("䐵崷嬹堻嬽㈿", a_));
		IL_253:
		goto IL_54B;
		IL_4FA:
		throw new ArgumentNullException(RecordTableEnumerator.b("帵䄷䨹夻䰽ⰿ⭁⩃ⵅ㭇", a_));
		IL_54B:
		A_2.Add(hyperLink);
		A_2.ᜀ(hyperLink);
	}

	// Token: 0x0600503B RID: 20539 RVA: 0x00321D8C File Offset: 0x00320D8C
	private void ᜀ(XmlReader A_0, XlsWorksheetBase A_1)
	{
		int a_ = 19;
		if (true)
		{
		}
		int num = 25;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_1DF;
			case 1:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("⩈⑊⥌⩎ὐ㉒㡔㉖", a_)))
				{
					num = 19;
					continue;
				}
				goto IL_19B;
			case 2:
				num = 14;
				continue;
			case 3:
				num = 10;
				continue;
			case 4:
				if (A_1 == null)
				{
					num = 7;
					continue;
				}
				num = 1;
				continue;
			case 5:
				num = 30;
				continue;
			case 6:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 13;
					continue;
				}
				A_0.Skip();
				num = 11;
				continue;
			case 7:
				goto IL_354;
			case 8:
				if (!A_0.IsEmptyElement)
				{
					num = 20;
					continue;
				}
				goto IL_3A9;
			case 9:
				goto IL_1DF;
			case 10:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("㥈⩊⩌⩎ɐ㙒⅔ɖ⥘୚⽜", a_)))
				{
					num = 29;
					continue;
				}
				this.ᜁ(A_0, A_1.PageSetupBase as IPageSetup);
				num = 21;
				continue;
			}
			case 11:
				goto IL_1DF;
			case 12:
				goto IL_B1;
			case 13:
				num = 28;
				continue;
			case 14:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("㵈⩊⽌౎㹐㽒㩔╖", a_)))
				{
					num = 5;
					continue;
				}
				this.ᜀ(A_0, A_1.TabColorObject);
				num = 26;
				continue;
			}
			case 15:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 17;
					continue;
				}
				num = 6;
				continue;
			case 16:
				goto IL_211;
			case 17:
				goto IL_20C;
			case 18:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㵈㥊ⱌⅎ≐㩒⅔㹖㙘㕚ᡜ⥞`རၤ٦ᵨɪɬŮ", a_)))
				{
					num = 27;
					continue;
				}
				goto IL_211;
			case 19:
				A_1.CodeName = A_0.Value;
				num = 22;
				continue;
			case 20:
				A_0.Read();
				num = 9;
				continue;
			case 21:
				goto IL_1DF;
			case 22:
				goto IL_19B;
			case 23:
				goto IL_1CC;
			case 24:
				goto IL_1DD;
			case 26:
				goto IL_1DF;
			case 27:
				A_1.IsTransitionEvaluation = XmlConvert.ToBoolean(A_0.Value);
				num = 16;
				continue;
			case 28:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 2;
					continue;
				}
				goto IL_1CC;
			}
			case 29:
				num = 23;
				continue;
			case 30:
			{
				string localName;
				if (localName == RecordTableEnumerator.b("♈㹊㥌⍎㡐㵒ごݖ⭘", a_))
				{
					this.ᜀ(A_0, A_1.PageSetupBase as IPageSetup);
					num = 0;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1DD;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			}
			}
			if (A_0 == null)
			{
				num = 12;
				continue;
			}
			num = 4;
			continue;
			IL_19B:
			num = 18;
			continue;
			IL_1CC:
			A_0.Skip();
			num = 24;
			continue;
			IL_1DF:
			num = 15;
			continue;
			IL_1DD:
			goto IL_1DF;
			IL_211:
			A_0.MoveToElement();
			num = 8;
		}
		IL_B1:
		throw new ArgumentNullException(RecordTableEnumerator.b("㭈⹊ⱌ⭎㑐⅒", a_));
		IL_20C:
		goto IL_3A9;
		IL_354:
		throw new ArgumentNullException(RecordTableEnumerator.b("㩈⍊⡌⩎═", a_));
		IL_3A9:
		A_0.Read();
	}

	// Token: 0x0600503C RID: 20540 RVA: 0x0032214C File Offset: 0x0032114C
	private void ᜁ(XmlReader A_0, IPageSetup A_1)
	{
		int a_ = 7;
		int num = 8;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_73;
				default:
					goto IL_11D;
				}
				break;
			case 1:
				if (A_1 == null)
				{
					if (true)
					{
					}
					num = 0;
					continue;
				}
				num = 3;
				continue;
			case 2:
				A_1.IsFitToPage = XmlConvert.ToBoolean(A_0.Value);
				num = 6;
				continue;
			case 3:
				if (A_0.LocalName != RecordTableEnumerator.b("䴼帾♀♂ᙄ≆㵈Ṋ㵌὎⍐", a_))
				{
					goto IL_73;
				}
				num = 5;
				continue;
			case 4:
				goto IL_48;
			case 5:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("嬼嘾㕀ᝂ⩄ᝆ⡈ⱊ⡌", a_)))
				{
					num = 2;
					continue;
				}
				goto IL_137;
			case 6:
				goto IL_C3;
			case 7:
				goto IL_7B;
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			num = 1;
			continue;
			IL_73:
			num = 7;
		}
		IL_48:
		throw new ArgumentNullException(RecordTableEnumerator.b("似娾⁀❂⁄㕆", a_));
		IL_7B:
		throw new XmlException();
		IL_C3:
		goto IL_137;
		IL_11D:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䴼帾♀♂ᙄ≆㵈㹊㵌", a_));
		IL_137:
		A_0.Read();
	}

	// Token: 0x0600503D RID: 20541 RVA: 0x00322298 File Offset: 0x00321298
	private void ᜀ(XmlReader A_0, IPageSetup A_1)
	{
		int a_ = 19;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_11B;
			case 1:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㩈㹊⁌≎ぐ⅒ⱔՖじ㱚㕜⭞", a_)))
				{
					num = 8;
					continue;
				}
				goto IL_15D;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					A_1.IsSummaryRowBelow = XmlConvert.ToBoolean(A_0.Value);
					num = 0;
					continue;
				}
				break;
			case 4:
				goto IL_DF;
			case 5:
				goto IL_105;
			case 6:
				goto IL_4F;
			case 7:
				if (A_1 == null)
				{
					num = 4;
					continue;
				}
				num = 9;
				continue;
			case 8:
				if (true)
				{
				}
				A_1.IsSummaryColumnRight = XmlConvert.ToBoolean(A_0.Value);
				num = 5;
				continue;
			case 9:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㩈㹊⁌≎ぐ⅒ⱔᕖ㱘㝚㉜⡞", a_)))
				{
					num = 3;
					continue;
				}
				goto IL_11B;
			}
			if (A_0 == null)
			{
				num = 6;
				continue;
			}
			num = 7;
			continue;
			IL_11B:
			num = 1;
		}
		IL_4F:
		throw new ArgumentNullException(RecordTableEnumerator.b("㭈⹊ⱌ⭎㑐⅒", a_));
		IL_DF:
		throw new ArgumentNullException(RecordTableEnumerator.b("㥈⩊⩌⩎ɐ㙒⅔≖⥘", a_));
		IL_105:
		IL_15D:
		A_0.MoveToElement();
		A_0.Skip();
	}

	// Token: 0x0600503E RID: 20542 RVA: 0x00322410 File Offset: 0x00321410
	private void ᜀ(XmlReader A_0, XlsWorksheet A_1, string A_2)
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_CE;
				case 1:
					goto IL_1B0;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1B0;
					default:
						goto IL_77;
					}
					break;
				case 4:
					if (A_1 == null)
					{
						num = 0;
						continue;
					}
					num = 6;
					continue;
				case 5:
					goto IL_15E;
				case 6:
					if (A_2 == null)
					{
						num = 7;
						continue;
					}
					num = 8;
					continue;
				case 7:
					goto IL_B1;
				case 8:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("刺夼", a_), RecordTableEnumerator.b("区䤼䬾ㅀ祂橄框㩈⡊╌⩎㱐㉒♔祖㙘⭚㡜ㅞᥠ๢।Ŧ٨ᥪl๮հr孴ᡶ୸ᱺ剼ၾ쾊ﺒ練뒚꾜꾞醠関誤햦첨잪첬\udbae\ud8b0\udcb2\udbb4쒶톸튺춼첾", a_)))
					{
						num = 1;
						continue;
					}
					goto IL_1C9;
				}
				if (true)
				{
				}
				if (A_0 == null)
				{
					num = 3;
					continue;
				}
				num = 4;
				continue;
				IL_1B0:
				string value = A_0.Value;
				sprᡟ sprᡟ = A_1.DataHolder;
				RelationsCollection relationsCollection = sprᡟ.ᜇ();
				sprᦨ a_2 = relationsCollection[value];
				sprវ sprវ = sprᡟ.ᜋ();
				spr\u2570 spr_u = sprវ.ᜀ(a_2, A_2);
				Stream stream = new MemoryStream();
				MemoryStream memoryStream = (MemoryStream)spr_u.ᜐ();
				memoryStream.WriteTo(stream);
				A_1.PageSetup.BackgoundImage = (Bitmap)Image.FromStream(stream);
				relationsCollection.Remove(value);
				sprវ.\u1714().ᜀ(spr_u.ᜇ());
				num = 5;
			}
			IL_77:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䤺堼帾╀♂㝄", a_));
			IL_B1:
			throw new ArgumentNullException(RecordTableEnumerator.b("䠺䤼䴾ᅀ≂㝄≆❈㽊ᵌ⹎═㭒", a_));
			IL_CE:
			throw new ArgumentNullException(RecordTableEnumerator.b("䠺唼娾⑀㝂", a_));
			IL_15E:
			IL_1C9:
			A_0.Skip();
			return;
		}
		}
	}

	// Token: 0x0600503F RID: 20543 RVA: 0x003225F8 File Offset: 0x003215F8
	public void ᜮ(XmlReader A_0)
	{
		int a_ = 4;
		int num = 22;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 10;
					continue;
				}
				A_0.Read();
				num = 26;
				continue;
			case 2:
				goto IL_1D7;
			case 3:
				if (spr\u22D2.ច == null)
				{
					num = 33;
					continue;
				}
				goto IL_3F9;
			case 4:
				goto IL_1D7;
			case 5:
				goto IL_1D7;
			case 6:
				goto IL_1D7;
			case 7:
				num = 32;
				continue;
			case 8:
				goto IL_1D7;
			case 9:
				if (true)
				{
				}
				num = 3;
				continue;
			case 10:
				num = 27;
				continue;
			case 11:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					goto IL_2A0;
				}
				A_0.Skip();
				num = 5;
				continue;
			case 12:
			{
				int num2;
				switch (num2)
				{
				case 0:
				{
					XlsBuiltInDocumentProperties xlsBuiltInDocumentProperties;
					xlsBuiltInDocumentProperties.Category = this.ᜀ(A_0);
					num = 8;
					continue;
				}
				case 1:
				{
					XlsBuiltInDocumentProperties xlsBuiltInDocumentProperties;
					xlsBuiltInDocumentProperties.CreatedTime = DateTime.Parse(this.ᜀ(A_0));
					num = 6;
					continue;
				}
				case 2:
				{
					XlsBuiltInDocumentProperties xlsBuiltInDocumentProperties;
					xlsBuiltInDocumentProperties.Author = this.ᜀ(A_0);
					num = 2;
					continue;
				}
				case 3:
				{
					XlsBuiltInDocumentProperties xlsBuiltInDocumentProperties;
					xlsBuiltInDocumentProperties.Comments = this.ᜀ(A_0);
					num = 28;
					continue;
				}
				case 4:
				{
					XlsBuiltInDocumentProperties xlsBuiltInDocumentProperties;
					xlsBuiltInDocumentProperties.Keywords = this.ᜀ(A_0);
					num = 14;
					continue;
				}
				case 5:
				{
					XlsBuiltInDocumentProperties xlsBuiltInDocumentProperties;
					xlsBuiltInDocumentProperties.LastAuthor = this.ᜀ(A_0);
					num = 34;
					continue;
				}
				case 6:
				{
					XlsBuiltInDocumentProperties xlsBuiltInDocumentProperties;
					xlsBuiltInDocumentProperties.LastPrinted = DateTime.Parse(this.ᜀ(A_0));
					num = 4;
					continue;
				}
				case 7:
				{
					XlsBuiltInDocumentProperties xlsBuiltInDocumentProperties;
					xlsBuiltInDocumentProperties.LastSaveTime = DateTime.Parse(this.ᜀ(A_0));
					num = 16;
					continue;
				}
				case 8:
				{
					XlsBuiltInDocumentProperties xlsBuiltInDocumentProperties;
					xlsBuiltInDocumentProperties.Subject = this.ᜀ(A_0);
					num = 19;
					continue;
				}
				case 9:
				{
					XlsBuiltInDocumentProperties xlsBuiltInDocumentProperties;
					xlsBuiltInDocumentProperties.Title = this.ᜀ(A_0);
					num = 13;
					continue;
				}
				default:
					num = 7;
					continue;
				}
				break;
			}
			case 13:
				goto IL_1D7;
			case 14:
				goto IL_1D7;
			case 15:
				num = 12;
				continue;
			case 16:
				goto IL_1D7;
			case 17:
				num = 24;
				continue;
			case 18:
				goto IL_3F9;
			case 19:
				goto IL_1D7;
			case 20:
			{
				if (A_0.IsEmptyElement)
				{
					num = 0;
					continue;
				}
				XlsBuiltInDocumentProperties xlsBuiltInDocumentProperties = this.ᜉ.BuiltInDocumentProperties as XlsBuiltInDocumentProperties;
				A_0.Read();
				num = 30;
				continue;
			}
			case 21:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 23;
					continue;
				}
				num = 11;
				continue;
			case 23:
				return;
			case 24:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 9;
					continue;
				}
				goto IL_199;
			}
			case 25:
			{
				int num2;
				string localName;
				if (spr\u22D2.ច.TryGetValue(localName, out num2))
				{
					num = 15;
					continue;
				}
				goto IL_199;
			}
			case 26:
				goto IL_46D;
			case 27:
				if (A_0.LocalName != RecordTableEnumerator.b("夹医䰽┿ቁ㙃⥅㡇⽉㹋㩍㥏㝑❓", a_))
				{
					num = 31;
					continue;
				}
				num = 20;
				continue;
			case 28:
				goto IL_1D7;
			case 29:
				goto IL_1D7;
			case 30:
				goto IL_1D7;
			case 31:
				goto IL_17A;
			case 32:
				goto IL_199;
			case 33:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2A0;
				default:
					if (false)
					{
					}
					spr\u22D2.ច = new Dictionary<string, int>(10)
					{
						{
							RecordTableEnumerator.b("夹崻䨽┿╁⭃㑅ㅇ", a_),
							0
						},
						{
							RecordTableEnumerator.b("夹主嬽ℿ㙁⅃≅", a_),
							1
						},
						{
							RecordTableEnumerator.b("夹主嬽ℿ㙁⭃㑅", a_),
							2
						},
						{
							RecordTableEnumerator.b("帹夻䴽⌿ぁⵃ㙅㱇⍉⍋⁍", a_),
							3
						},
						{
							RecordTableEnumerator.b("儹夻䜽㜿ⵁ㙃≅㭇", a_),
							4
						},
						{
							RecordTableEnumerator.b("嘹崻䴽㐿ཁ⭃≅ⅇⱉ╋⭍㑏ၑⵓ", a_),
							5
						},
						{
							RecordTableEnumerator.b("嘹崻䴽㐿ቁ㙃⽅♇㹉⥋⩍", a_),
							6
						},
						{
							RecordTableEnumerator.b("圹医娽⤿⑁ⵃ⍅ⱇ", a_),
							7
						},
						{
							RecordTableEnumerator.b("䤹䤻尽⨿❁❃㉅", a_),
							8
						},
						{
							RecordTableEnumerator.b("丹唻䨽ⰿ❁", a_),
							9
						}
					};
					num = 18;
					continue;
				}
				break;
			case 34:
				goto IL_1D7;
			case 35:
				goto IL_BD;
			}
			if (A_0 == null)
			{
				num = 35;
				continue;
			}
			goto IL_46D;
			IL_199:
			A_0.Skip();
			num = 29;
			continue;
			IL_1D7:
			num = 21;
			continue;
			IL_2A0:
			num = 17;
			continue;
			IL_3F9:
			num = 25;
			continue;
			IL_46D:
			num = 1;
		}
		IL_BD:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠹夻弽␿❁㙃", a_));
		IL_17A:
		throw new XmlException(RecordTableEnumerator.b("漹刻嬽㠿㉁⅃╅㱇⽉⡋湍⡏㽑㡓癕ⱗ㭙㭛繝", a_) + A_0.LocalName);
	}

	// Token: 0x06005040 RID: 20544 RVA: 0x00322B50 File Offset: 0x00321B50
	public void ᜦ(XmlReader A_0)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			int num = 33;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_269;
				case 1:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 18;
						continue;
					}
					A_0.Skip();
					num = 12;
					continue;
				case 2:
					goto IL_269;
				case 3:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 17;
						continue;
					}
					goto IL_210;
				case 4:
					goto IL_269;
				case 5:
					num = 7;
					continue;
				case 6:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 19;
						continue;
					}
					A_0.Read();
					num = 32;
					continue;
				case 7:
					if (spr\u22D2.ឆ == null)
					{
						num = 34;
						continue;
					}
					goto IL_3E6;
				case 8:
				{
					if (A_0.IsEmptyElement)
					{
						num = 20;
						continue;
					}
					XlsBuiltInDocumentProperties xlsBuiltInDocumentProperties = this.ᜉ.BuiltInDocumentProperties as XlsBuiltInDocumentProperties;
					A_0.Read();
					num = 2;
					continue;
				}
				case 9:
					goto IL_EA;
				case 10:
					this.ᜉ.Version = ExcelVersion.Version2010;
					num = 35;
					continue;
				case 11:
				{
					string localName;
					int num2;
					if (spr\u22D2.ឆ.TryGetValue(localName, out num2))
					{
						num = 13;
						continue;
					}
					goto IL_1DE;
				}
				case 12:
					goto IL_269;
				case 13:
					num = 38;
					continue;
				case 14:
					goto IL_269;
				case 15:
					goto IL_1DE;
				case 16:
					goto IL_269;
				case 17:
					return;
				case 18:
					num = 42;
					continue;
				case 19:
					num = 28;
					continue;
				case 20:
					return;
				case 21:
					num = 15;
					continue;
				case 22:
					goto IL_5D1;
				case 23:
					goto IL_269;
				case 24:
					if (true)
					{
					}
					goto IL_269;
				case 25:
					goto IL_269;
				case 26:
					goto IL_269;
				case 27:
					goto IL_3E6;
				case 28:
					if (A_0.LocalName != RecordTableEnumerator.b("ቁ㙃⥅㡇⽉㹋㩍㥏㝑❓", a_))
					{
						num = 22;
						continue;
					}
					num = 8;
					continue;
				case 29:
					goto IL_269;
				case 30:
					goto IL_269;
				case 31:
					goto IL_269;
				case 32:
					goto IL_642;
				case 34:
					spr\u22D2.ឆ = new Dictionary<string, int>(15)
					{
						{
							RecordTableEnumerator.b("́㑃㙅⑇⍉⽋⽍⑏㭑㭓㡕", a_),
							0
						},
						{
							RecordTableEnumerator.b("Łⱃ❅㩇⭉⽋㩍㕏⁑❓", a_),
							1
						},
						{
							RecordTableEnumerator.b("Ł⭃⭅㡇⭉≋㝍", a_),
							2
						},
						{
							RecordTableEnumerator.b("แⵃ⡅ⵇ㥉", a_),
							3
						},
						{
							RecordTableEnumerator.b("ཁ╃⡅⥇ⵉ⥋㱍", a_),
							4
						},
						{
							RecordTableEnumerator.b("ཁृՅ⑇⍉㱋㵍", a_),
							5
						},
						{
							RecordTableEnumerator.b("ు⭃㉅ⵇ㥉", a_),
							6
						},
						{
							RecordTableEnumerator.b("ቁ╃ⅅⵇ㥉", a_),
							7
						},
						{
							RecordTableEnumerator.b("ቁ╃㑅⥇ⵉ㹋⽍⁏㩑❓", a_),
							8
						},
						{
							RecordTableEnumerator.b("ቁ㙃⍅㭇⽉≋㩍ㅏ♑㵓㥕㙗᱙㍛ⱝൟ͡ၣ", a_),
							9
						},
						{
							RecordTableEnumerator.b("ᙁ⅃⭅㡇♉ⵋ㩍㕏", a_),
							10
						},
						{
							RecordTableEnumerator.b("ᙁ⭃㉅⥇♉ᡋ❍㵏㝑", a_),
							11
						},
						{
							RecordTableEnumerator.b("ᕁ⭃㑅ⱇ㥉", a_),
							12
						},
						{
							RecordTableEnumerator.b("ੁ㵃㙅ⵇ㡉⁋❍㹏㥑ᙓ㝕⭗㽙", a_),
							13
						},
						{
							RecordTableEnumerator.b("́㑃㙅ṇ⽉㹋㵍㥏㵑㩓", a_),
							14
						}
					};
					num = 27;
					continue;
				case 35:
					goto IL_269;
				case 36:
					goto IL_269;
				case 37:
					goto IL_269;
				case 38:
				{
					int num2;
					switch (num2)
					{
					case 0:
					{
						XlsBuiltInDocumentProperties xlsBuiltInDocumentProperties;
						xlsBuiltInDocumentProperties.ApplicationName = this.ᜀ(A_0);
						num = 29;
						continue;
					}
					case 1:
					{
						XlsBuiltInDocumentProperties xlsBuiltInDocumentProperties;
						xlsBuiltInDocumentProperties.Characters = XmlConvert.ToInt32(this.ᜀ(A_0));
						num = 0;
						continue;
					}
					case 2:
					{
						XlsBuiltInDocumentProperties xlsBuiltInDocumentProperties;
						xlsBuiltInDocumentProperties.Company = this.ᜀ(A_0);
						num = 30;
						continue;
					}
					case 3:
					{
						XlsBuiltInDocumentProperties xlsBuiltInDocumentProperties;
						xlsBuiltInDocumentProperties.LineCount = XmlConvert.ToInt32(this.ᜀ(A_0));
						num = 41;
						continue;
					}
					case 4:
					{
						XlsBuiltInDocumentProperties xlsBuiltInDocumentProperties;
						xlsBuiltInDocumentProperties.Manager = this.ᜀ(A_0);
						num = 26;
						continue;
					}
					case 5:
					{
						XlsBuiltInDocumentProperties xlsBuiltInDocumentProperties;
						xlsBuiltInDocumentProperties.MultimediaClipCount = XmlConvert.ToInt32(this.ᜀ(A_0));
						num = 37;
						continue;
					}
					case 6:
					{
						XlsBuiltInDocumentProperties xlsBuiltInDocumentProperties;
						xlsBuiltInDocumentProperties.SlideCount = XmlConvert.ToInt32(this.ᜀ(A_0));
						num = 23;
						continue;
					}
					case 7:
					{
						XlsBuiltInDocumentProperties xlsBuiltInDocumentProperties;
						xlsBuiltInDocumentProperties.PageCount = XmlConvert.ToInt32(this.ᜀ(A_0));
						num = 25;
						continue;
					}
					case 8:
					{
						XlsBuiltInDocumentProperties xlsBuiltInDocumentProperties;
						xlsBuiltInDocumentProperties.ParagraphCount = XmlConvert.ToInt32(this.ᜀ(A_0));
						num = 14;
						continue;
					}
					case 9:
					{
						XlsBuiltInDocumentProperties xlsBuiltInDocumentProperties;
						xlsBuiltInDocumentProperties.PresentationTarget = this.ᜀ(A_0);
						num = 36;
						continue;
					}
					case 10:
					{
						XlsBuiltInDocumentProperties xlsBuiltInDocumentProperties;
						xlsBuiltInDocumentProperties.Template = this.ᜀ(A_0);
						num = 40;
						continue;
					}
					case 11:
					{
						XlsBuiltInDocumentProperties xlsBuiltInDocumentProperties;
						xlsBuiltInDocumentProperties.EditTime = TimeSpan.FromMinutes((double)XmlConvert.ToInt32(this.ᜀ(A_0)));
						num = 24;
						continue;
					}
					case 12:
					{
						XlsBuiltInDocumentProperties xlsBuiltInDocumentProperties;
						xlsBuiltInDocumentProperties.WordCount = XmlConvert.ToInt32(this.ᜀ(A_0));
						num = 31;
						continue;
					}
					case 13:
					{
						spr\u1AA2 spr_u1AA = (spr\u1AA2)this.ᜉ.CustomDocumentProperties;
						XlsDocumentProperty xlsDocumentProperty = (XlsDocumentProperty)spr_u1AA.ᜃ(RecordTableEnumerator.b("ᵁᑃཅేᕉKݍṏᥑᙓ᝕ୗὙ", a_));
						xlsDocumentProperty.Blob = Encoding.Unicode.GetBytes(this.ᜀ(A_0) + RecordTableEnumerator.b("䉁", a_));
						num = 4;
						continue;
					}
					case 14:
					{
						double num3 = A_0.ReadElementContentAsDouble();
						num = 39;
						continue;
					}
					default:
						num = 21;
						continue;
					}
					break;
				}
				case 39:
				{
					double num3;
					if (num3 > 14.0)
					{
						num = 10;
						continue;
					}
					goto IL_269;
				}
				case 40:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_210;
					default:
						if (false)
						{
						}
						goto IL_269;
					}
					break;
				case 41:
					goto IL_269;
				case 42:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 5;
						continue;
					}
					goto IL_1DE;
				}
				}
				if (A_0 == null)
				{
					num = 9;
					continue;
				}
				goto IL_642;
				IL_1DE:
				A_0.Skip();
				num = 16;
				continue;
				IL_210:
				num = 1;
				continue;
				IL_269:
				num = 3;
				continue;
				IL_3E6:
				num = 11;
				continue;
				IL_642:
				num = 6;
			}
			IL_EA:
			throw new ArgumentNullException(RecordTableEnumerator.b("ぁ⅃❅ⱇ⽉㹋", a_));
			IL_5D1:
			throw new XmlException(RecordTableEnumerator.b("ᝁ⩃⍅ぇ㩉⥋ⵍ⑏㝑こ癕⁗㝙せ繝ᑟͣ͡䙥", a_) + A_0.LocalName);
		}
		}
	}

	// Token: 0x06005041 RID: 20545 RVA: 0x003232B0 File Offset: 0x003222B0
	public void ᜨ(XmlReader A_0)
	{
		int a_ = 14;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_129;
			case 1:
				num = 6;
				continue;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_DA;
				default:
				{
					if (false)
					{
					}
					if (A_0.IsEmptyElement)
					{
						num = 11;
						continue;
					}
					spr\u1AA2 a_2 = (spr\u1AA2)this.ᜉ.CustomDocumentProperties;
					A_0.Read();
					num = 14;
					continue;
				}
				}
				break;
			case 3:
				goto IL_6C;
			case 5:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 15;
					continue;
				}
				A_0.Skip();
				goto IL_DA;
			case 6:
				if (A_0.LocalName != RecordTableEnumerator.b("ᑃ㑅❇㩉⥋㱍⑏㭑ㅓ╕", a_))
				{
					num = 12;
					continue;
				}
				num = 2;
				continue;
			case 7:
				goto IL_129;
			case 8:
				return;
			case 9:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 8;
					continue;
				}
				num = 5;
				continue;
			case 10:
			{
				spr\u1AA2 a_2;
				this.ᜀ(A_0, a_2);
				num = 7;
				continue;
			}
			case 11:
				return;
			case 12:
				goto IL_1BE;
			case 13:
				if (A_0.LocalName == RecordTableEnumerator.b("㑃㑅❇㩉⥋㱍⑏⭑", a_))
				{
					num = 10;
					continue;
				}
				goto IL_129;
			case 14:
				goto IL_129;
			case 15:
				num = 13;
				continue;
			case 16:
				goto IL_B0;
			case 17:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 1;
					continue;
				}
				A_0.Read();
				num = 16;
				continue;
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			IL_B0:
			num = 17;
			continue;
			IL_DA:
			num = 0;
			continue;
			IL_129:
			num = 9;
		}
		IL_6C:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㙃⍅⥇⹉⥋㱍", a_));
		IL_1BE:
		throw new XmlException(RecordTableEnumerator.b("ᅃ⡅ⵇ㉉㱋⭍㍏♑ㅓ㉕硗≙ㅛ㉝䁟ᙡգť䡧", a_) + A_0.LocalName);
	}

	// Token: 0x06005042 RID: 20546 RVA: 0x003234F0 File Offset: 0x003224F0
	public void ᜀ(XmlReader A_0, spr\u1AA2 A_1)
	{
		int a_ = 18;
		int num = 13;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				num = 33;
				continue;
			case 1:
				goto IL_254;
			case 2:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 3;
					continue;
				}
				goto IL_443;
			}
			case 3:
				num = 4;
				continue;
			case 4:
				if (true)
				{
				}
				if (spr\u22D2.ជ == null)
				{
					num = 31;
					continue;
				}
				goto IL_1DC;
			case 5:
				goto IL_B9;
			case 6:
			{
				if (A_0.LocalName != RecordTableEnumerator.b("㡇㡉⍋㹍㕏⁑⁓⽕", a_))
				{
					num = 15;
					continue;
				}
				XlsDocumentProperty xlsDocumentProperty = null;
				num = 21;
				continue;
			}
			case 7:
				if (A_1 == null)
				{
					num = 28;
					continue;
				}
				num = 6;
				continue;
			case 8:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 32;
					continue;
				}
				num = 18;
				continue;
			case 9:
				num = 23;
				continue;
			case 10:
				goto IL_254;
			case 11:
				A_0.Read();
				num = 26;
				continue;
			case 12:
				num = 2;
				continue;
			case 14:
				goto IL_254;
			case 15:
				goto IL_30A;
			case 16:
				goto IL_254;
			case 17:
				goto IL_254;
			case 18:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 12;
					continue;
				}
				A_0.Read();
				num = 17;
				continue;
			case 19:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_F3;
				default:
					if (false)
					{
					}
					goto IL_254;
				}
				break;
			case 20:
				goto IL_1DC;
			case 21:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("♇⭉⅋⭍", a_)))
				{
					num = 25;
					continue;
				}
				goto IL_3F5;
			case 22:
				if (!A_0.IsEmptyElement)
				{
					num = 11;
					continue;
				}
				goto IL_52E;
			case 23:
				goto IL_F3;
			case 24:
				goto IL_3F5;
			case 25:
			{
				XlsDocumentProperty xlsDocumentProperty = (XlsDocumentProperty)A_1.ᜃ(A_0.Value);
				num = 24;
				continue;
			}
			case 26:
				goto IL_254;
			case 27:
				goto IL_254;
			case 28:
				goto IL_43E;
			case 29:
				goto IL_254;
			case 30:
			{
				string localName;
				if (spr\u22D2.ជ.TryGetValue(localName, out num2))
				{
					num = 9;
					continue;
				}
				goto IL_443;
			}
			case 31:
				spr\u22D2.ជ = new Dictionary<string, int>(7)
				{
					{
						RecordTableEnumerator.b("⑇㩉㭋㵍⑏⁑", a_),
						0
					},
					{
						RecordTableEnumerator.b("⑇㩉㽋㩍≏", a_),
						1
					},
					{
						RecordTableEnumerator.b("⹇⍉⁋⭍⑏㭑㥓㍕", a_),
						2
					},
					{
						RecordTableEnumerator.b("㩇牉", a_),
						3
					},
					{
						RecordTableEnumerator.b("ⅇ繉", a_),
						4
					},
					{
						RecordTableEnumerator.b("ⅇ⑉㡋", a_),
						5
					},
					{
						RecordTableEnumerator.b("⩇╉⍋≍", a_),
						6
					}
				};
				num = 20;
				continue;
			case 32:
				goto IL_277;
			case 33:
				goto IL_443;
			case 34:
				goto IL_254;
			}
			if (A_0 == null)
			{
				num = 5;
				continue;
			}
			num = 7;
			continue;
			IL_F3:
			switch (num2)
			{
			case 0:
			{
				XlsDocumentProperty xlsDocumentProperty;
				xlsDocumentProperty.PropertyType = PropertyType.String;
				xlsDocumentProperty.Text = this.ᜀ(A_0);
				num = 14;
				continue;
			}
			case 1:
			{
				XlsDocumentProperty xlsDocumentProperty;
				xlsDocumentProperty.PropertyType = PropertyType.AsciiString;
				xlsDocumentProperty.Text = this.ᜀ(A_0);
				num = 16;
				continue;
			}
			case 2:
			{
				XlsDocumentProperty xlsDocumentProperty;
				xlsDocumentProperty.PropertyType = PropertyType.DateTime;
				xlsDocumentProperty.DateTime = DateTime.Parse(this.ᜀ(A_0));
				num = 1;
				continue;
			}
			case 3:
			{
				XlsDocumentProperty xlsDocumentProperty;
				xlsDocumentProperty.PropertyType = PropertyType.Double;
				xlsDocumentProperty.Double = XmlConvert.ToDouble(this.ᜀ(A_0));
				num = 27;
				continue;
			}
			case 4:
			{
				XlsDocumentProperty xlsDocumentProperty;
				xlsDocumentProperty.PropertyType = PropertyType.Int32;
				xlsDocumentProperty.Int32 = XmlConvert.ToInt32(this.ᜀ(A_0));
				num = 19;
				continue;
			}
			case 5:
			{
				XlsDocumentProperty xlsDocumentProperty;
				xlsDocumentProperty.PropertyType = PropertyType.Int;
				xlsDocumentProperty.Integer = XmlConvert.ToInt32(this.ᜀ(A_0));
				num = 10;
				continue;
			}
			case 6:
			{
				XlsDocumentProperty xlsDocumentProperty;
				xlsDocumentProperty.PropertyType = PropertyType.Bool;
				xlsDocumentProperty.Boolean = bool.Parse(this.ᜀ(A_0));
				num = 34;
				continue;
			}
			default:
				num = 0;
				continue;
			}
			IL_1DC:
			num = 30;
			continue;
			IL_254:
			num = 8;
			continue;
			IL_3F5:
			A_0.MoveToElement();
			num = 22;
			continue;
			IL_443:
			A_0.Skip();
			num = 29;
		}
		IL_B9:
		throw new ArgumentNullException(RecordTableEnumerator.b("㩇⽉ⵋ⩍㕏⁑", a_));
		IL_277:
		goto IL_52E;
		IL_30A:
		throw new XmlException(RecordTableEnumerator.b("ᵇ⑉⥋㙍⁏㝑㝓≕㵗㹙籛♝ൟ๡䑣ብ१൩䱫", a_) + A_0.LocalName);
		IL_43E:
		throw new ArgumentNullException(RecordTableEnumerator.b("⭇㽉㽋㩍㽏㽑ѓ⑕㝗⩙㥛ⱝᑟୡţᕥ", a_));
		IL_52E:
		A_0.Read();
	}

	// Token: 0x06005043 RID: 20547 RVA: 0x00323A34 File Offset: 0x00322A34
	internal void ᜃ(XmlReader A_0, RelationsCollection A_1)
	{
		int a_ = 13;
		int num = 7;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_116;
			case 1:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 9;
					continue;
				}
				goto IL_1D2;
			}
			case 2:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("ⱂ⥄≆Ո≊⍌⑎", a_)))
				{
					num = 5;
					continue;
				}
				goto IL_F9;
			}
			case 3:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("♂㵄㍆ⱈ㥊⍌⹎㵐ᅒ㩔㡖㉘", a_)))
				{
					num = 4;
					continue;
				}
				goto IL_CC;
			}
			case 4:
				num = 2;
				continue;
			case 5:
				goto IL_E9;
			case 6:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 8;
					continue;
				}
				A_0.Read();
				num = 0;
				continue;
			case 8:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_E9;
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
			case 9:
				num = 3;
				continue;
			case 10:
				if (A_0.LocalName != RecordTableEnumerator.b("♂㵄㍆ⱈ㥊⍌⹎㵐ὒ㱔㥖㉘", a_))
				{
					num = 13;
					continue;
				}
				A_0.Read();
				num = 1;
				continue;
			case 11:
				goto IL_5F;
			case 12:
				goto IL_F4;
			case 13:
				goto IL_1CD;
			}
			if (A_0 == null)
			{
				num = 11;
				continue;
			}
			goto IL_116;
			IL_E9:
			num = 12;
			continue;
			IL_116:
			num = 6;
		}
		IL_5F:
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄♆ⵈ⹊㽌", a_));
		IL_CC:
		this.ᜀ(A_0, A_1);
		return;
		IL_F4:
		goto IL_1D2;
		IL_F9:
		this.ᜁ(A_0, A_1);
		return;
		IL_1CD:
		throw new XmlException(RecordTableEnumerator.b("ᙂ⭄≆ㅈ㭊⡌ⱎ═㙒ㅔ睖⅘㙚ㅜ罞ᕠɢɤ䥦", a_));
		IL_1D2:
		throw new XmlException(RecordTableEnumerator.b("ᙂ⭄㑆㱈㭊㵌⁎⍐❒ご㍖祘⍚ぜ㍞䅠ᝢѤf", a_));
	}

	// Token: 0x06005044 RID: 20548 RVA: 0x00323C28 File Offset: 0x00322C28
	private void ᜁ(XmlReader A_0, RelationsCollection A_1)
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			int num = 3;
			XlsExternWorkbook xlsExternWorkbook;
			string text;
			for (;;)
			{
				string text2;
				switch (num)
				{
				case 0:
					goto IL_69;
				case 1:
					xlsExternWorkbook.ProgramId = A_0.Value;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_212;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					break;
				case 2:
					goto IL_EF;
				case 4:
					text = text.Substring(RecordTableEnumerator.b("⍄⹆╈⹊睌恎繐籒", a_).Length);
					num = 5;
					continue;
				case 5:
					goto IL_212;
				case 6:
					goto IL_F4;
				case 7:
					goto IL_190;
				case 8:
					if (A_0.LocalName != RecordTableEnumerator.b("⩄⭆ⱈ݊⑌ⅎ㩐", a_))
					{
						num = 2;
						continue;
					}
					text2 = null;
					num = 10;
					continue;
				case 9:
					if (text.StartsWith(RecordTableEnumerator.b("⍄⹆╈⹊睌恎繐籒", a_)))
					{
						num = 4;
						continue;
					}
					goto IL_278;
				case 10:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("ⱄ⍆", a_), RecordTableEnumerator.b("ⵄ㍆㵈㭊睌恎繐⁒㙔㽖㱘㙚㱜ⱞ你ౢᕤɦݨ፪lͮᝰᱲݴ᩶ᡸེ๼兾ꢆ톔滛爵쾠힢誤閦馨鮪鮬肮쎰횲\ud9b4횶춸튺튼톾닀ꯂ계럆뫈", a_)))
					{
						num = 11;
						continue;
					}
					goto IL_190;
				case 11:
					text2 = A_0.Value;
					num = 7;
					continue;
				case 12:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㕄㕆♈ⱊь⭎", a_)))
					{
						num = 1;
						continue;
					}
					goto IL_F4;
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				num = 8;
				continue;
				IL_F4:
				int a_2;
				spr\u2141 spr_u = xlsExternWorkbook.ExternNames.ᜀ(a_2).ᜄ();
				spr_u.ᜄ(true);
				spr_u.ᜃ(false);
				spr_u.ᜁ(true);
				spr_u.ᜀ(true);
				spr_u.ᜂ(false);
				sprᦨ sprᦨ = A_1[text2];
				text = Uri.UnescapeDataString(sprᦨ.ᜂ());
				num = 9;
				continue;
				IL_190:
				xlsExternWorkbook = this.ᜀ(A_1, text2, null);
				a_2 = xlsExternWorkbook.ExternNames.ᜃ(RecordTableEnumerator.b("扄", a_));
				num = 12;
			}
			IL_69:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㝄≆⡈⽊⡌㵎", a_));
			IL_EF:
			throw new XmlException(RecordTableEnumerator.b("၄⥆ⱈ㍊㵌⩎㉐❒ご㍖祘⍚ぜ㍞䅠ᝢѤf䝨", a_));
			IL_212:
			IL_278:
			xlsExternWorkbook.URL = text;
			A_0.Skip();
			return;
		}
		}
	}

	// Token: 0x06005045 RID: 20549 RVA: 0x00323EBC File Offset: 0x00322EBC
	private void ᜀ(XmlReader A_0, RelationsCollection A_1)
	{
		int a_ = 7;
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				XlsExternWorkbook a_2;
				switch (num)
				{
				case 0:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("丼圾⑀♂ㅄ͆⡈㽊ⱌᱎ㑐❒", a_)))
					{
						num = 20;
						continue;
					}
					this.ᜁ(A_0, a_2);
					num = 10;
					continue;
				}
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1A1;
					default:
						if (false)
						{
						}
						num = 19;
						continue;
					}
					break;
				case 3:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 5;
						continue;
					}
					A_0.Read();
					if (true)
					{
					}
					num = 16;
					continue;
				case 4:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("夼娾❀⩂⭄≆ⵈՊⱌ≎㑐⁒", a_)))
					{
						num = 8;
						continue;
					}
					this.ᜃ(A_0, a_2);
					num = 6;
					continue;
				}
				case 5:
					num = 11;
					continue;
				case 6:
					goto IL_20C;
				case 7:
					goto IL_2B7;
				case 8:
					num = 26;
					continue;
				case 9:
				{
					if (A_0.LocalName != RecordTableEnumerator.b("堼䜾㕀♂㝄⥆⡈❊ཌ⁎㹐㡒", a_))
					{
						num = 7;
						continue;
					}
					string a_3 = null;
					num = 14;
					continue;
				}
				case 10:
					goto IL_20C;
				case 11:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 2;
						continue;
					}
					goto IL_1F8;
				}
				case 12:
					goto IL_A7;
				case 13:
					goto IL_20C;
				case 14:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("吼嬾", a_), RecordTableEnumerator.b("唼䬾㕀㍂罄框晈㡊⹌❎㑐㹒㑔⑖睘㑚ⵜ㩞འ᭢ࡤ୦ཨѪὬɮၰݲٴ奶ᙸॺ᩼偾즌늜궞醠鎢鎤袦\udba8캪솬캮얰\udab2\udab4\ud9b6쪸펺풼쾾닀", a_)))
					{
						num = 25;
						continue;
					}
					goto IL_258;
				case 15:
					num = 0;
					continue;
				case 16:
					goto IL_1A1;
				case 17:
					goto IL_258;
				case 18:
					goto IL_20C;
				case 19:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("丼圾⑀♂ㅄॆ⡈♊⡌㱎", a_)))
					{
						num = 15;
						continue;
					}
					List<string> a_4 = this.ᜃ(A_0);
					string a_3;
					a_2 = this.ᜀ(A_1, a_3, a_4);
					num = 18;
					continue;
				}
				case 20:
					num = 4;
					continue;
				case 21:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 23;
						continue;
					}
					num = 3;
					continue;
				case 22:
					num = 13;
					continue;
				case 23:
					goto IL_231;
				case 24:
					if (!A_0.IsEmptyElement)
					{
						num = 22;
						continue;
					}
					goto IL_377;
				case 25:
				{
					string a_3 = A_0.Value;
					num = 17;
					continue;
				}
				case 26:
					goto IL_184;
				}
				if (A_0 == null)
				{
					num = 12;
					continue;
				}
				num = 9;
				continue;
				IL_20C:
				num = 21;
				continue;
				IL_1A1:
				goto IL_20C;
				IL_258:
				a_2 = null;
				num = 24;
			}
			IL_A7:
			throw new ArgumentNullException(RecordTableEnumerator.b("似娾⁀❂⁄㕆", a_));
			IL_184:
			IL_1F8:
			throw new XmlException(RecordTableEnumerator.b("格儾⑀㭂㕄≆⩈㽊⡌⭎煐⭒㡔㭖祘⽚㱜㡞你", a_));
			IL_231:
			goto IL_377;
			IL_2B7:
			throw new XmlException(RecordTableEnumerator.b("格儾⑀㭂㕄≆⩈㽊⡌⭎煐⭒㡔㭖祘⽚㱜㡞你", a_));
			IL_377:
			A_0.Read();
			return;
		}
		}
	}

	// Token: 0x06005046 RID: 20550 RVA: 0x00324248 File Offset: 0x00323248
	private void ᜃ(XmlReader A_0, XlsExternWorkbook A_1)
	{
		int a_ = 0;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 12;
				continue;
			case 1:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 13;
					continue;
				}
				num = 14;
				continue;
			case 2:
				if (!A_0.IsEmptyElement)
				{
					num = 15;
					continue;
				}
				goto IL_1C6;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1C6;
				default:
					if (false)
					{
					}
					goto IL_13D;
				}
				break;
			case 4:
				num = 7;
				continue;
			case 5:
				this.ᜂ(A_0, A_1);
				num = 11;
				continue;
			case 7:
			{
				string localName;
				if (localName == RecordTableEnumerator.b("刵崷尹唻倽┿♁੃❅╇⽉", a_))
				{
					num = 5;
					continue;
				}
				goto IL_89;
			}
			case 8:
				goto IL_84;
			case 9:
				goto IL_13D;
			case 10:
				goto IL_13D;
			case 11:
				goto IL_13D;
			case 12:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 4;
					continue;
				}
				goto IL_89;
			}
			case 13:
				goto IL_15D;
			case 14:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 0;
					continue;
				}
				A_0.Skip();
				num = 3;
				continue;
			case 15:
				A_0.Read();
				num = 9;
				continue;
			}
			if (true)
			{
			}
			if (A_0.LocalName != RecordTableEnumerator.b("刵崷尹唻倽┿♁੃❅╇⽉㽋", a_))
			{
				num = 8;
				continue;
			}
			num = 2;
			continue;
			IL_89:
			A_0.Skip();
			num = 10;
			continue;
			IL_13D:
			num = 1;
		}
		IL_84:
		throw new XmlException();
		IL_15D:
		IL_1C6:
		A_0.Read();
	}

	// Token: 0x06005047 RID: 20551 RVA: 0x00324424 File Offset: 0x00323424
	private void ᜂ(XmlReader A_0, XlsExternWorkbook A_1)
	{
		int a_ = 9;
		int num = 0;
		string a_2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				break;
			case 1:
				goto IL_58;
			case 2:
				a_2 = A_0.Value;
				num = 3;
				continue;
			case 3:
				goto IL_69;
			case 4:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("儾⁀⹂⁄", a_)))
				{
					goto IL_A7;
				}
				goto IL_C4;
			}
			if (A_0.LocalName != RecordTableEnumerator.b("嬾⑀╂ⱄ⥆ⱈ⽊͌⹎㱐㙒", a_))
			{
				num = 1;
				continue;
			}
			a_2 = null;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				num = 4;
				continue;
			}
			IL_A7:
			num = 2;
		}
		IL_58:
		throw new XmlException();
		IL_69:
		IL_C4:
		A_1.ExternNames.ᜃ(a_2);
	}

	// Token: 0x06005048 RID: 20552 RVA: 0x00324504 File Offset: 0x00323504
	private void ᜁ(XmlReader A_0, XlsExternWorkbook A_1)
	{
		int a_ = 17;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_1C0;
			case 1:
				num = 8;
				continue;
			case 2:
				goto IL_153;
			case 4:
				num = 16;
				continue;
			case 5:
				goto IL_176;
			case 6:
				if (!A_0.IsEmptyElement)
				{
					num = 17;
					continue;
				}
				goto IL_23D;
			case 7:
				goto IL_153;
			case 8:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_E4;
				default:
				{
					if (false)
					{
					}
					string localName;
					if (localName == RecordTableEnumerator.b("㑆ⅈ⹊⡌㭎ᕐ㉒⅔㙖", a_))
					{
						num = 14;
						continue;
					}
					goto IL_108;
				}
				}
				break;
			case 9:
				goto IL_153;
			case 10:
				goto IL_153;
			case 11:
				goto IL_13C;
			case 12:
				goto IL_81;
			case 13:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 5;
					continue;
				}
				num = 19;
				continue;
			case 14:
				this.ᜀ(A_0, A_1);
				num = 7;
				continue;
			case 15:
				if (A_1 == null)
				{
					if (true)
					{
					}
					num = 11;
					continue;
				}
				num = 18;
				continue;
			case 16:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 1;
					continue;
				}
				goto IL_108;
			}
			case 17:
				A_0.Read();
				num = 2;
				continue;
			case 18:
				if (A_0.LocalName != RecordTableEnumerator.b("㑆ⅈ⹊⡌㭎ᕐ㉒⅔㙖੘㹚⥜", a_))
				{
					num = 0;
					continue;
				}
				num = 6;
				continue;
			case 19:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					goto IL_E4;
				}
				A_0.Skip();
				num = 9;
				continue;
			}
			if (A_0 == null)
			{
				num = 12;
				continue;
			}
			num = 15;
			continue;
			IL_E4:
			num = 4;
			continue;
			IL_108:
			A_0.Skip();
			num = 10;
			continue;
			IL_153:
			num = 13;
		}
		IL_81:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕆ⱈ⩊⥌⩎⍐", a_));
		IL_13C:
		throw new ArgumentNullException(RecordTableEnumerator.b("≆ㅈ㽊⡌㵎㽐ᅒ㩔㡖㉘", a_));
		IL_176:
		goto IL_23D;
		IL_1C0:
		throw new XmlException();
		IL_23D:
		A_0.Skip();
	}

	// Token: 0x06005049 RID: 20553 RVA: 0x00324754 File Offset: 0x00323754
	private void ᜀ(XmlReader A_0, XlsExternWorkbook A_1)
	{
		int a_ = 7;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_44;
			case 1:
				goto IL_B3;
			case 2:
				goto IL_DA;
			case 3:
				goto IL_FB;
			case 4:
				if (A_0.LocalName != RecordTableEnumerator.b("丼圾⑀♂ㅄ͆⡈㽊ⱌ", a_))
				{
					num = 5;
					continue;
				}
				num = 2;
				continue;
			case 5:
				goto IL_77;
			case 7:
				if (A_1 != null)
				{
					num = 4;
					continue;
				}
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_DA;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			num = 7;
			continue;
			IL_DA:
			if (A_0.MoveToAttribute(RecordTableEnumerator.b("丼圾⑀♂ㅄๆⵈ", a_)))
			{
				goto IL_11B;
			}
			num = 3;
		}
		IL_44:
		throw new ArgumentException(RecordTableEnumerator.b("似娾⁀❂⁄㕆", a_));
		IL_77:
		throw new XmlException();
		IL_B3:
		throw new ArgumentNullException(RecordTableEnumerator.b("堼䜾㕀♂㝄⥆ୈ⑊≌⑎", a_));
		IL_FB:
		throw new XmlException();
		IL_11B:
		int key = XmlConvert.ToInt32(A_0.Value);
		XlsExternWorksheet xlsExternWorksheet = A_1.Worksheets[key];
		A_0.MoveToElement();
		xlsExternWorksheet.AdditionalAttributes = this.ᜀ(A_0, xlsExternWorksheet, null, RecordTableEnumerator.b("帼娾ⵀ⽂", a_));
	}

	// Token: 0x0600504A RID: 20554 RVA: 0x003248BC File Offset: 0x003238BC
	private XlsExternWorkbook ᜀ(RelationsCollection A_0, string A_1, List<string> A_2)
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			string text;
			for (;;)
			{
				sprᦨ sprᦨ = A_0[A_1];
				text = sprᦨ.ᜂ();
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_B9;
					case 1:
						if (!text.StartsWith(RecordTableEnumerator.b("娻圽ⰿ❁繃楅杇敉", a_)))
						{
							goto IL_BB;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 2:
						text = text.Substring(RecordTableEnumerator.b("娻圽ⰿ❁繃楅杇敉", a_).Length);
						num = 0;
						continue;
					}
					break;
				}
			}
			IL_B9:
			IL_BB:
			text = Uri.UnescapeDataString(text);
			string fileName = Path.GetFileName(text);
			string filePath = text.Substring(0, text.Length - fileName.Length);
			int index = this.ᜉ.ExternWorkbooks.Add(filePath, fileName, A_2, null);
			return this.ᜉ.ExternWorkbooks[index];
		}
		}
	}

	// Token: 0x0600504B RID: 20555 RVA: 0x003249D0 File Offset: 0x003239D0
	private List<string> ᜃ(XmlReader A_0)
	{
		int a_ = 5;
		int num = 0;
		List<string> list;
		for (;;)
		{
			switch (num)
			{
			case 1:
				if (!A_0.IsEmptyElement)
				{
					num = 2;
					continue;
				}
				goto IL_214;
			case 2:
				goto IL_15F;
			case 3:
				goto IL_135;
			case 4:
			{
				if (!A_0.MoveToAttribute(RecordTableEnumerator.b("䴺尼匾", a_)))
				{
					num = 3;
					continue;
				}
				string value = A_0.Value;
				list.Add(value);
				num = 9;
				continue;
			}
			case 5:
				goto IL_13A;
			case 6:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 10;
					continue;
				}
				goto IL_BF;
			case 7:
				if (A_0.LocalName != RecordTableEnumerator.b("䠺唼娾⑀㝂ୄ♆⑈⹊㹌", a_))
				{
					num = 15;
					continue;
				}
				list = new List<string>();
				num = 1;
				continue;
			case 8:
				goto IL_15A;
			case 9:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_15F;
				default:
					if (false)
					{
					}
					goto IL_BF;
				}
				break;
			case 10:
				num = 14;
				continue;
			case 11:
				goto IL_13A;
			case 12:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 8;
					continue;
				}
				num = 6;
				continue;
			case 13:
				num = 4;
				continue;
			case 14:
				if (A_0.LocalName == RecordTableEnumerator.b("䠺唼娾⑀㝂ୄ♆⑈⹊", a_))
				{
					num = 13;
					continue;
				}
				goto IL_BF;
			case 15:
				goto IL_107;
			case 16:
				goto IL_68;
			}
			if (A_0 == null)
			{
				num = 16;
				continue;
			}
			num = 7;
			continue;
			IL_BF:
			A_0.Read();
			num = 5;
			continue;
			IL_13A:
			num = 12;
			continue;
			IL_15F:
			num = 11;
		}
		IL_68:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䤺堼帾╀♂㝄", a_));
		IL_107:
		throw new XmlException(RecordTableEnumerator.b("渺匼娾㥀㍂⁄⑆㵈⹊⥌潎⥐㹒㥔睖ⵘ㩚㩜", a_));
		IL_135:
		throw new XmlException();
		IL_15A:
		IL_214:
		A_0.Read();
		return list;
	}

	// Token: 0x0600504C RID: 20556 RVA: 0x00324BFC File Offset: 0x00323BFC
	private void ᜁ(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			int num;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_D7:
				num = (int)XmlConvert.ToUInt16(A_0.Value);
				goto IL_299;
			default:
				if (false)
				{
				}
				num2 = 12;
				break;
			}
			XlsHPageBreak xlsHPageBreak;
			int num5;
			int num6;
			for (;;)
			{
				IL_48:
				int num3;
				int num4;
				XlsHPageBreaksCollection xlsHPageBreaksCollection;
				switch (num2)
				{
				case 0:
					if (!A_0.MoveToAttribute(RecordTableEnumerator.b("嘾╀", a_)))
					{
						num2 = 15;
						continue;
					}
					num2 = 2;
					continue;
				case 1:
					goto IL_1DA;
				case 2:
					num3 = XmlConvert.ToInt32(A_0.Value);
					goto IL_324;
				case 3:
					goto IL_1FF;
				case 4:
					num4 = 0;
					goto IL_2ED;
				case 5:
					goto IL_1C4;
				case 6:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num2 = 3;
						continue;
					}
					num2 = 8;
					continue;
				case 7:
					goto IL_160;
				case 8:
					if (A_0.LocalName == RecordTableEnumerator.b("崾㍀⡂", a_))
					{
						num2 = 22;
						continue;
					}
					goto IL_166;
				case 9:
					num2 = 7;
					continue;
				case 10:
					xlsHPageBreak.Type = PageBreakType.Manual;
					num2 = 5;
					continue;
				case 11:
					if (!A_0.MoveToAttribute(RecordTableEnumerator.b("刾⡀ⵂ", a_)))
					{
						num2 = 18;
						continue;
					}
					num2 = 20;
					continue;
				case 13:
					goto IL_166;
				case 14:
					goto IL_D7;
				case 15:
					num2 = 17;
					continue;
				case 16:
					if (!A_0.MoveToAttribute(RecordTableEnumerator.b("刾⁀㭂", a_)))
					{
						num2 = 9;
						continue;
					}
					num2 = 14;
					continue;
				case 17:
					num3 = 0;
					goto IL_324;
				case 18:
					num2 = 4;
					continue;
				case 19:
					if (A_1 == null)
					{
						num2 = 21;
						continue;
					}
					A_0.Read();
					xlsHPageBreaksCollection = (XlsHPageBreaksCollection)A_1.HPageBreaks;
					num2 = 23;
					continue;
				case 20:
					num4 = (int)XmlConvert.ToUInt16(A_0.Value);
					goto IL_2ED;
				case 21:
					goto IL_278;
				case 22:
					num2 = 0;
					continue;
				case 23:
					goto IL_1DA;
				case 24:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("刾⁀ⵂ", a_)))
					{
						num2 = 10;
						continue;
					}
					goto IL_1C4;
				case 25:
					goto IL_C6;
				}
				if (A_0 == null)
				{
					num2 = 25;
					continue;
				}
				num2 = 19;
				continue;
				IL_166:
				A_0.Skip();
				num2 = 1;
				continue;
				IL_1C4:
				xlsHPageBreaksCollection.ᜀ(xlsHPageBreak);
				num2 = 13;
				continue;
				IL_1DA:
				num2 = 6;
				continue;
				IL_2ED:
				num5 = num4;
				num2 = 16;
				continue;
				IL_324:
				num6 = num3;
				if (true)
				{
				}
				num2 = 11;
			}
			IL_C6:
			throw new ArgumentNullException(RecordTableEnumerator.b("䴾⑀≂⅄≆㭈", a_));
			IL_160:
			num = 0;
			goto IL_299;
			IL_1FF:
			A_0.Skip();
			return;
			IL_278:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰾⥀♂⁄㍆", a_));
			IL_299:
			int num7 = num;
			spr\u2539.ᜀ a_2 = new spr\u2539.ᜀ((ushort)num6, (ushort)num5, (ushort)num7);
			xlsHPageBreak = new XlsHPageBreak(A_1.AppImplementation, A_1, a_2);
			num2 = 24;
			goto IL_48;
		}
		}
	}

	// Token: 0x0600504D RID: 20557 RVA: 0x00324F74 File Offset: 0x00323F74
	private void ᜀ(XmlReader A_0, XlsWorksheet A_1)
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			int num;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_D7:
				num = XmlConvert.ToInt32(A_0.Value);
				goto IL_2A1;
			default:
				if (false)
				{
				}
				num2 = 14;
				break;
			}
			XlsVPageBreak xlsVPageBreak;
			int num5;
			int num6;
			for (;;)
			{
				IL_48:
				int num3;
				int num4;
				XlsVPageBreaksCollection xlsVPageBreaksCollection;
				switch (num2)
				{
				case 0:
					goto IL_160;
				case 1:
					if (!A_0.MoveToAttribute(RecordTableEnumerator.b("張尷", a_)))
					{
						num2 = 12;
						continue;
					}
					num2 = 15;
					continue;
				case 2:
					num3 = 0;
					goto IL_2F5;
				case 3:
					if (!A_0.MoveToAttribute(RecordTableEnumerator.b("嬵夷䈹", a_)))
					{
						num2 = 18;
						continue;
					}
					num2 = 17;
					continue;
				case 4:
					goto IL_166;
				case 5:
					goto IL_C6;
				case 6:
					goto IL_1DA;
				case 7:
					num2 = 1;
					continue;
				case 8:
					num4 = 0;
					goto IL_32C;
				case 9:
					goto IL_1C4;
				case 10:
					xlsVPageBreak.Type = PageBreakType.Manual;
					num2 = 9;
					continue;
				case 11:
					num3 = XmlConvert.ToInt32(A_0.Value);
					goto IL_2F5;
				case 12:
					num2 = 8;
					continue;
				case 13:
					num2 = 2;
					continue;
				case 15:
					num4 = XmlConvert.ToInt32(A_0.Value);
					goto IL_32C;
				case 16:
					goto IL_280;
				case 17:
					goto IL_D7;
				case 18:
					num2 = 0;
					continue;
				case 19:
					goto IL_1DA;
				case 20:
					if (A_1 == null)
					{
						num2 = 16;
						continue;
					}
					A_0.Read();
					xlsVPageBreaksCollection = (XlsVPageBreaksCollection)A_1.VPageBreaks;
					num2 = 19;
					continue;
				case 21:
					if (!A_0.MoveToAttribute(RecordTableEnumerator.b("嬵儷吹", a_)))
					{
						num2 = 13;
						continue;
					}
					num2 = 11;
					continue;
				case 22:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("嬵夷吹", a_)))
					{
						num2 = 10;
						continue;
					}
					goto IL_1C4;
				case 23:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num2 = 24;
						continue;
					}
					num2 = 25;
					continue;
				case 24:
					goto IL_1FF;
				case 25:
					if (A_0.LocalName == RecordTableEnumerator.b("吵䨷儹", a_))
					{
						num2 = 7;
						continue;
					}
					goto IL_166;
				}
				if (A_0 == null)
				{
					num2 = 5;
					continue;
				}
				num2 = 20;
				continue;
				IL_166:
				A_0.Skip();
				num2 = 6;
				continue;
				IL_1C4:
				xlsVPageBreaksCollection.ᜀ(xlsVPageBreak);
				num2 = 4;
				continue;
				IL_1DA:
				num2 = 23;
				continue;
				IL_2F5:
				num5 = num3;
				num2 = 3;
				continue;
				IL_32C:
				num6 = num4;
				num2 = 21;
			}
			IL_C6:
			throw new ArgumentNullException(RecordTableEnumerator.b("䐵崷嬹堻嬽㈿", a_));
			IL_160:
			num = 0;
			goto IL_2A1;
			IL_1FF:
			if (true)
			{
			}
			A_0.Skip();
			return;
			IL_280:
			throw new ArgumentNullException(RecordTableEnumerator.b("䔵倷弹夻䨽", a_));
			IL_2A1:
			int num7 = num;
			spr\u2583.ᜀ a_2 = new spr\u2583.ᜀ((ushort)num6, (ushort)num5, (ushort)num7);
			xlsVPageBreak = new XlsVPageBreak(A_1.AppImplementation, A_1, a_2);
			num2 = 22;
			goto IL_48;
		}
		}
	}

	// Token: 0x0600504E RID: 20558 RVA: 0x003252EC File Offset: 0x003242EC
	private void ᜂ(XmlReader A_0)
	{
		int a_ = 11;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				bool isEmptyElement;
				if (!isEmptyElement)
				{
					num = 7;
					continue;
				}
				return;
			}
			case 1:
				goto IL_D7;
			case 2:
				goto IL_64;
			case 3:
				goto IL_D7;
			case 5:
				this.ᜁ(A_0);
				num = 12;
				continue;
			case 6:
				if (A_0.LocalName == RecordTableEnumerator.b("⑀㭂ㅄ≆㭈╊ⱌ⍎͐㙒㍔㉖⭘㹚㍜㱞Ѡ", a_))
				{
					num = 5;
					continue;
				}
				goto IL_129;
			case 7:
				num = 3;
				continue;
			case 8:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 11;
					continue;
				}
				goto IL_129;
			case 9:
			{
				if (A_0.LocalName != RecordTableEnumerator.b("⑀㭂ㅄ≆㭈╊ⱌ⍎͐㙒㍔㉖⭘㹚㍜㱞Ѡၢ", a_))
				{
					num = 15;
					continue;
				}
				bool isEmptyElement = A_0.IsEmptyElement;
				A_0.Read();
				num = 0;
				continue;
			}
			case 10:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 13;
					continue;
				}
				goto IL_7A;
			case 11:
				num = 6;
				continue;
			case 12:
				goto IL_D7;
			case 13:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_7A;
				default:
					if (false)
					{
					}
					A_0.Read();
					num = 14;
					continue;
				}
				break;
			case 14:
				goto IL_14F;
			case 15:
				goto IL_D2;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			num = 9;
			continue;
			IL_7A:
			num = 8;
			continue;
			IL_D7:
			num = 10;
			continue;
			IL_129:
			A_0.Read();
			num = 1;
		}
		IL_64:
		throw new ArgumentNullException(RecordTableEnumerator.b("㍀♂⑄⍆ⱈ㥊", a_));
		IL_D2:
		throw new XmlException(RecordTableEnumerator.b("ᑀⵂ⁄㽆㥈⹊⹌㭎㑐㝒畔⽖㑘㝚絜⭞`Ѣ䭤", a_));
		IL_14F:
		if (true)
		{
		}
	}

	// Token: 0x0600504F RID: 20559 RVA: 0x003254E8 File Offset: 0x003244E8
	private void ᜁ(XmlReader A_0)
	{
		int a_ = 8;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.LocalName != RecordTableEnumerator.b("嬽㠿㙁⅃㑅♇⭉⁋ᱍ㕏㑑ㅓ⑕㵗㑙㽛㭝", a_))
				{
					num = 1;
					continue;
				}
				num = 4;
				continue;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6D;
				default:
					goto IL_F7;
				}
				break;
			case 2:
				goto IL_75;
			case 3:
				goto IL_3C;
			case 4:
				if (!A_0.MoveToAttribute(RecordTableEnumerator.b("圽␿", a_), RecordTableEnumerator.b("嘽㐿㙁㑃籅杇敉㽋ⵍ㡏㝑㥓㝕⭗瑙㍛⹝՟ౡᱣ୥ѧ౩ͫᱭᵯ፱sյ噷ᕹ๻᥽꽿쪍ﾏﮕﶗ놝銟銡钣邥螧\ud8a9즫슭톯욱\uddb3\ud9b5횷즹풻ힽ낿뇁", a_)))
				{
					goto IL_6D;
				}
				goto IL_10C;
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			num = 0;
			continue;
			IL_6D:
			num = 2;
		}
		IL_3C:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䰽┿⍁⁃⍅㩇", a_));
		IL_75:
		throw new XmlException();
		IL_F7:
		if (false)
		{
		}
		throw new XmlException(RecordTableEnumerator.b("欽⸿❁㱃㙅ⵇ⥉㡋⭍㑏牑ⱓ㭕㑗穙⡛㽝ݟ䱡", a_));
		IL_10C:
		string value = A_0.Value;
		this.ᜉ.DataHolder.ᜈ(value);
	}

	// Token: 0x06005050 RID: 20560 RVA: 0x0032561C File Offset: 0x0032461C
	private void ᜀ(spr\u1B7A.CellType A_0, string A_1, XlsCellRecordCollection A_2, int A_3, int A_4, int A_5)
	{
		int a_ = 15;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_8B;
			case 2:
				return;
			case 3:
				num = 6;
				continue;
			case 4:
				switch (A_0)
				{
				case spr\u1B7A.CellType.b:
					goto IL_46;
				case spr\u1B7A.CellType.e:
					goto IL_5B;
				case spr\u1B7A.CellType.inlineStr:
				case spr\u1B7A.CellType.s:
					goto IL_A0;
				case spr\u1B7A.CellType.n:
					goto IL_8D;
				case spr\u1B7A.CellType.str:
					goto IL_141;
				default:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5B;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				}
				break;
			case 5:
				if (A_2 == null)
				{
					num = 7;
					continue;
				}
				if (true)
				{
				}
				num = 4;
				continue;
			case 6:
				if (A_1.Length == 0)
				{
					num = 1;
					continue;
				}
				num = 5;
				continue;
			case 7:
				goto IL_E0;
			}
			if (A_1 == null)
			{
				return;
			}
			num = 3;
		}
		IL_46:
		A_2.SetBooleanValue(A_3, A_4, XmlConvert.ToBoolean(A_1), A_5);
		return;
		IL_5B:
		A_2.SetErrorValue(A_3, A_4, A_1, A_5);
		return;
		IL_8B:
		return;
		IL_8D:
		A_2.SetNumberValue(A_3, A_4, XmlConvert.ToDouble(A_1), A_5);
		return;
		IL_A0:
		A_2.SetSingleStringValue(A_3, A_4, A_5, XmlConvert.ToInt32(A_1));
		return;
		IL_E0:
		throw new ArgumentNullException(RecordTableEnumerator.b("♄≆╈❊㹌", a_));
		IL_141:
		A_2.ᜀ(A_3, A_4, A_5, A_1);
	}

	// Token: 0x06005051 RID: 20561 RVA: 0x00325778 File Offset: 0x00324778
	private void ᜀ(IInternalWorksheet A_0, spr\u1B7A.CellType A_1, string A_2, int A_3, int A_4)
	{
		int a_ = 17;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_D0;
			case 2:
				switch (A_1)
				{
				case spr\u1B7A.CellType.b:
					goto IL_62;
				case spr\u1B7A.CellType.e:
					goto IL_56;
				case spr\u1B7A.CellType.inlineStr:
				case spr\u1B7A.CellType.s:
					return;
				case spr\u1B7A.CellType.n:
					goto IL_E6;
				case spr\u1B7A.CellType.str:
					A_0.SetFormulaStringValue(A_3, A_4, A_2);
					num = 0;
					continue;
				default:
					num = 3;
					continue;
				}
				break;
			case 3:
				return;
			case 4:
				goto IL_54;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			default:
				if (false)
				{
				}
				if (A_2 == null)
				{
					num = 4;
				}
				else
				{
					if (true)
					{
					}
					num = 2;
				}
				break;
			}
		}
		IL_54:
		throw new NullReferenceException(RecordTableEnumerator.b("㑆㵈㥊ᭌ⹎㵐♒ご", a_));
		IL_56:
		A_0.SetFormulaErrorValue(A_3, A_4, A_2);
		return;
		IL_62:
		A_0.SetFormulaBoolValue(A_3, A_4, XmlConvert.ToBoolean(A_2));
		return;
		IL_D0:
		return;
		IL_E6:
		A_0.SetFormulaNumberValue(A_3, A_4, XmlConvert.ToDouble(A_2));
	}

	// Token: 0x06005052 RID: 20562 RVA: 0x00325880 File Offset: 0x00324880
	private void ᜀ(XlsWorksheet A_0, string A_1, string A_2, int A_3)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			int num = 0;
			spr\u225F spr_u225F;
			for (;;)
			{
				switch (num)
				{
				case 1:
				{
					string value;
					spr_u225F.ᜂ(Convert.ToInt32(value) - 1);
					string a_2;
					spr_u225F.ᜃ(sprṔ.ᜀ(a_2) - 1);
					string value2;
					spr_u225F.ᜀ(Convert.ToInt32(value2) - 1);
					string a_3;
					spr_u225F.ᜁ(sprṔ.ᜀ(a_3) - 1);
					num = 8;
					continue;
				}
				case 2:
					goto IL_1EB;
				case 3:
					goto IL_16D;
				case 4:
					goto IL_60;
				case 5:
				{
					if (A_2 == null)
					{
						num = 2;
						continue;
					}
					spr_u225F = (spr\u225F)spr\u175E.ᜀ(TBIFFRecord.Array);
					A_1 = UtilityMethods.ᜀ(A_1);
					spr_u225F.ᜀ(this.ᜊ.ᜀ(A_1, A_0, null));
					string value = null;
					string a_2 = null;
					string value2 = null;
					string a_3 = null;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D9;
					default:
						if (false)
						{
						}
						num = 7;
						continue;
					}
					break;
				}
				case 6:
					goto IL_124;
				case 7:
				{
					string value;
					string a_2;
					string value2;
					string a_3;
					if (this.ᜊ.ᜀ(A_2, false, out value, out a_2, out value2, out a_3))
					{
						goto IL_D9;
					}
					int num2 = 0;
					int num3 = 0;
					sprṔ.ᜀ(A_2, out num2, out num3);
					spr_u225F.ᜂ(num2 - 1);
					spr_u225F.ᜃ(num3 - 1);
					spr_u225F.ᜀ(num2 - 1);
					spr_u225F.ᜁ(num3 - 1);
					num = 3;
					continue;
				}
				case 8:
					goto IL_1CB;
				case 9:
					if (true)
					{
					}
					if (A_1 == null)
					{
						num = 6;
						continue;
					}
					num = 5;
					continue;
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				num = 9;
				continue;
				IL_D9:
				num = 1;
			}
			IL_60:
			throw new ArgumentNullException(RecordTableEnumerator.b("㭇≉⥋⭍⑏", a_));
			IL_124:
			throw new ArgumentNullException(RecordTableEnumerator.b("㭇㹉㹋ࡍ㽏⁑㥓⍕㑗㭙ཛ⩝቟ୡ੣ť", a_));
			IL_16D:
			IL_1CB:
			goto IL_20E;
			IL_1EB:
			throw new ArgumentNullException(RecordTableEnumerator.b("㭇㹉㹋്㕏㹑㡓ѕ㥗㑙㭛㭝", a_));
			IL_20E:
			XlsRange xlsRange = (XlsRange)A_0.AllocatedRange[A_2];
			xlsRange.ᜀ(spr_u225F, A_3);
			return;
		}
		}
	}

	// Token: 0x06005053 RID: 20563 RVA: 0x00325AB8 File Offset: 0x00324AB8
	private void ᜀ(XlsWorksheet A_0, string A_1, string A_2, uint A_3, int A_4, int A_5, int A_6, bool A_7)
	{
		switch (0)
		{
		default:
		{
			XlsCellRecordCollection cellRecords;
			for (;;)
			{
				cellRecords = A_0.CellRecords;
				int num = 7;
				for (;;)
				{
					string text;
					string text2;
					string value;
					string a_3;
					switch (num)
					{
					case 0:
					{
						bool flag;
						if (flag = FormulaUtil.IsCell(A_2, false, out text, out text2))
						{
							num = 10;
							continue;
						}
						goto IL_68;
					}
					case 1:
						goto IL_68;
					case 2:
						goto IL_183;
					case 3:
						if (A_1 != null)
						{
							num = 6;
							continue;
						}
						goto IL_1FA;
					case 4:
					{
						int a_ = Convert.ToInt32(text);
						int a_2 = sprṔ.ᜀ(text2);
						spr\u1DE2 spr_u1DE;
						spr_u1DE.ᜂ(a_);
						spr_u1DE.ᜃ(a_2);
						spr_u1DE.ᜀ(Convert.ToInt32(value));
						spr_u1DE.ᜁ(sprṔ.ᜀ(a_3));
						A_1 = UtilityMethods.ᜀ(A_1);
						spr_u1DE.ᜀ(this.ᜊ.ᜀ(A_1, A_4, A_5, A_0));
						sprủ sprủ = cellRecords.Table;
						int count = sprủ.ᜅ().Count;
						sprủ.ᜀ(0, (int)A_3, spr_u1DE);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1E4;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					}
					case 5:
					{
						bool flag;
						if (!(flag = this.ᜊ.ᜀ(A_2, false, out text, out text2, out value, out a_3)))
						{
							if (true)
							{
							}
							num = 11;
							continue;
						}
						goto IL_68;
					}
					case 6:
					{
						text = null;
						text2 = null;
						value = null;
						a_3 = null;
						spr\u1DE2 spr_u1DE = (spr\u1DE2)spr\u175E.ᜀ(TBIFFRecord.SharedFormula2);
						bool flag = false;
						num = 5;
						continue;
					}
					case 7:
						if (A_2 != null)
						{
							num = 9;
							continue;
						}
						goto IL_1FA;
					case 8:
					{
						bool flag;
						if (flag)
						{
							num = 4;
							continue;
						}
						goto IL_1FA;
					}
					case 9:
						num = 3;
						continue;
					case 10:
						goto IL_1E4;
					case 11:
						num = 0;
						continue;
					}
					break;
					IL_68:
					num = 8;
					continue;
					IL_1E4:
					value = text;
					a_3 = text2;
					num = 1;
				}
			}
			IL_183:
			IL_1FA:
			spr᱒ spr᱒ = (spr᱒)spr\u175E.ᜀ(TBIFFRecord.Formula);
			spr\u1DE2 a_4 = cellRecords.Table.ᜅ()[(long)A_3];
			spr᱒.ᜁ(FormulaUtil.ᜀ(a_4, A_0.ParentWorkbook, A_4 - 1, A_5 - 1));
			spr᱒.ᜇ(A_4 - 1);
			spr᱒.ᜆ(A_5 - 1);
			spr᱒.ᜁ((ushort)A_6);
			spr᱒.ᜃ(A_7);
			cellRecords.ᜁ(A_4, A_5, spr᱒);
			return;
		}
		}
	}

	// Token: 0x06005054 RID: 20564 RVA: 0x00325D34 File Offset: 0x00324D34
	private CellDataType ᜆ(string A_0)
	{
		int a_ = 0;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_B3;
			case 1:
				goto IL_FB;
			case 3:
				goto IL_15A;
			case 4:
				num = 6;
				continue;
			case 5:
			{
				int num2;
				if (spr\u22D2.ឈ.TryGetValue(A_0, out num2))
				{
					num = 4;
					continue;
				}
				goto IL_267;
			}
			case 6:
			{
				int num2;
				switch (num2)
				{
				case 0:
					return CellDataType.Formula;
				case 1:
					return CellDataType.Date;
				case 2:
					return CellDataType.Decimal;
				case 3:
					return CellDataType.User;
				case 4:
					return CellDataType.Any;
				case 5:
					return CellDataType.TextLength;
				case 6:
					return CellDataType.Time;
				case 7:
					goto IL_9E;
				default:
					num = 13;
					continue;
				}
				break;
			}
			case 7:
				if (A_0 == null)
				{
					goto IL_267;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6D;
				default:
					if (false)
					{
					}
					num = 11;
					continue;
				}
				break;
			case 8:
				if (A_0 == string.Empty)
				{
					num = 3;
					continue;
				}
				num = 7;
				continue;
			case 9:
				spr\u22D2.ឈ = new Dictionary<string, int>(8)
				{
					{
						RecordTableEnumerator.b("唵䴷䤹䠻儽ⴿ", a_),
						0
					},
					{
						RecordTableEnumerator.b("刵夷丹夻", a_),
						1
					},
					{
						RecordTableEnumerator.b("刵崷夹唻匽ℿ⹁", a_),
						2
					},
					{
						RecordTableEnumerator.b("娵儷䤹䠻", a_),
						3
					},
					{
						RecordTableEnumerator.b("堵圷吹夻", a_),
						4
					},
					{
						RecordTableEnumerator.b("䈵崷䈹䠻爽┿ⱁ⍃㉅⁇", a_),
						5
					},
					{
						RecordTableEnumerator.b("䈵儷圹夻", a_),
						6
					},
					{
						RecordTableEnumerator.b("䄵倷唹倻嬽", a_),
						7
					}
				};
				num = 1;
				continue;
			case 10:
				goto IL_6D;
			case 11:
				num = 10;
				continue;
			case 12:
				num = 8;
				continue;
			case 13:
				num = 0;
				continue;
			}
			if (A_0 != null)
			{
				num = 12;
				continue;
			}
			goto IL_88;
			IL_6D:
			if (spr\u22D2.ឈ == null)
			{
				num = 9;
				continue;
			}
			IL_FB:
			num = 5;
		}
		return CellDataType.User;
		IL_88:
		throw new ArgumentNullException(RecordTableEnumerator.b("䔵䰷䠹礻䰽㈿ⵁ㙃ᕅ㱇㍉⁋⭍", a_));
		IL_9E:
		if (true)
		{
		}
		return CellDataType.Integer;
		IL_B3:
		goto IL_267;
		IL_15A:
		goto IL_88;
		IL_267:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("刵夷丹崻栽ℿ⹁ⵃ≅⥇㹉╋⅍㹏ّⵓ♕㵗", a_));
	}

	// Token: 0x06005055 RID: 20565 RVA: 0x00325FBC File Offset: 0x00324FBC
	private AlertStyleType ᜅ(string A_0)
	{
		int a_ = 5;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_EB;
			case 1:
				num = 5;
				continue;
			case 2:
				if (true)
				{
				}
				num = 10;
				continue;
			case 3:
				num = 8;
				continue;
			case 5:
				if (!(A_0 == RecordTableEnumerator.b("刺匼夾⹀ㅂ⡄♆㵈≊≌ⅎ", a_)))
				{
					num = 7;
					continue;
				}
				return AlertStyleType.Info;
			case 6:
				if (A_0 != null)
				{
					goto IL_140;
				}
				goto IL_181;
			case 7:
				num = 12;
				continue;
			case 8:
				if (!(A_0 == RecordTableEnumerator.b("䰺尼䴾⽀⩂⭄⁆", a_)))
				{
					num = 9;
					continue;
				}
				return AlertStyleType.Warning;
			case 9:
				num = 11;
				continue;
			case 10:
				if (A_0 == string.Empty)
				{
					num = 0;
					continue;
				}
				num = 6;
				continue;
			case 11:
				goto IL_F8;
			case 12:
				if (!(A_0 == RecordTableEnumerator.b("䠺䤼倾ㅀ", a_)))
				{
					num = 3;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_140;
				default:
					goto IL_B6;
				}
				break;
			}
			if (A_0 != null)
			{
				num = 2;
				continue;
			}
			goto IL_8C;
			IL_140:
			num = 1;
		}
		return AlertStyleType.Info;
		IL_8C:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠺䤼䴾рㅂ㝄⡆㭈ᡊ㥌㙎㵐㙒", a_));
		IL_B6:
		if (false)
		{
		}
		return AlertStyleType.Stop;
		IL_EB:
		goto IL_8C;
		IL_F8:
		IL_181:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䠺䤼䴾рㅂ㝄⡆㭈ᡊ㥌㙎㵐㙒", a_));
	}

	// Token: 0x06005056 RID: 20566 RVA: 0x00326160 File Offset: 0x00325160
	private ValidationComparisonOperator ᜄ(string A_0)
	{
		int a_ = 2;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				int num2;
				if (spr\u22D2.ញ.TryGetValue(A_0, out num2))
				{
					num = 12;
					continue;
				}
				goto IL_264;
			}
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_21C;
				default:
					if (false)
					{
					}
					num = 9;
					continue;
				}
				break;
			case 2:
				goto IL_157;
			case 3:
				num = 13;
				continue;
			case 4:
				spr\u22D2.ញ = new Dictionary<string, int>(8)
				{
					{
						RecordTableEnumerator.b("娷弹䠻䤽┿❁⩃", a_),
						0
					},
					{
						RecordTableEnumerator.b("崷䬹䤻弽ⰿ", a_),
						1
					},
					{
						RecordTableEnumerator.b("強䠹夻弽㐿❁㙃ቅ⁇⭉≋", a_),
						2
					},
					{
						RecordTableEnumerator.b("強䠹夻弽㐿❁㙃ቅ⁇⭉≋ō≏ᝑ╓⍕㥗㙙", a_),
						3
					},
					{
						RecordTableEnumerator.b("吷弹伻䴽ᐿ⩁╃⡅", a_),
						4
					},
					{
						RecordTableEnumerator.b("吷弹伻䴽ᐿ⩁╃⡅݇㡉ो㽍╏㍑㡓", a_),
						5
					},
					{
						RecordTableEnumerator.b("嘷唹䠻簽┿㙁㍃⍅ⵇ⑉", a_),
						6
					},
					{
						RecordTableEnumerator.b("嘷唹䠻笽ㄿ㝁╃⩅", a_),
						7
					}
				};
				num = 8;
				continue;
			case 5:
				if (A_0 != null)
				{
					num = 1;
					continue;
				}
				goto IL_264;
			case 7:
				num = 10;
				continue;
			case 8:
				goto IL_21C;
			case 9:
				if (spr\u22D2.ញ == null)
				{
					num = 4;
					continue;
				}
				goto IL_105;
			case 10:
				if (A_0 == string.Empty)
				{
					num = 2;
					continue;
				}
				num = 5;
				continue;
			case 11:
			{
				int num2;
				switch (num2)
				{
				case 0:
					return ValidationComparisonOperator.Between;
				case 1:
					return ValidationComparisonOperator.Equal;
				case 2:
					return ValidationComparisonOperator.Greater;
				case 3:
					return ValidationComparisonOperator.GreaterOrEqual;
				case 4:
					return ValidationComparisonOperator.Less;
				case 5:
					return ValidationComparisonOperator.LessOrEqual;
				case 6:
					return ValidationComparisonOperator.NotBetween;
				case 7:
					return ValidationComparisonOperator.NotEqual;
				default:
					num = 3;
					continue;
				}
				break;
			}
			case 12:
				num = 11;
				continue;
			case 13:
				goto IL_D1;
			}
			if (A_0 != null)
			{
				num = 7;
				continue;
			}
			goto IL_AE;
			IL_105:
			num = 0;
			continue;
			IL_21C:
			goto IL_105;
		}
		return ValidationComparisonOperator.GreaterOrEqual;
		IL_AE:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬷丹主焽〿❁㙃❅㱇╉㹋", a_));
		IL_D1:
		if (true)
		{
		}
		goto IL_264;
		IL_157:
		goto IL_AE;
		IL_264:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䬷丹主焽〿❁㙃❅㱇╉㹋", a_));
	}

	// Token: 0x06005057 RID: 20567 RVA: 0x003263E4 File Offset: 0x003253E4
	private TAddr[] ᜃ(string A_0)
	{
		int a_ = 14;
		switch (0)
		{
		default:
		{
			int num = 6;
			List<TAddr> list;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_DB;
				case 1:
				{
					int num2;
					string[] array;
					if (num2 >= array.Length)
					{
						num = 3;
						continue;
					}
					string a_2 = array[num2];
					list.Add(this.ᜂ(a_2));
					num2++;
					num = 0;
					continue;
				}
				case 2:
					goto IL_100;
				case 3:
					goto IL_FE;
				case 4:
				{
					if (A_0 == string.Empty)
					{
						num = 2;
						continue;
					}
					string[] array2 = A_0.Split(new char[]
					{
						' '
					});
					list = new List<TAddr>();
					string[] array = array2;
					int num2 = 0;
					num = 5;
					continue;
				}
				case 5:
					if (true)
					{
					}
					goto IL_DB;
				case 7:
					num = 4;
					continue;
				}
				goto IL_49;
				IL_4F:
				num = 7;
				continue;
				IL_49:
				if (A_0 != null)
				{
					goto IL_4F;
				}
				goto IL_100;
				IL_DB:
				num = 1;
				continue;
				IL_100:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_4F;
				default:
					goto IL_116;
				}
			}
			IL_FE:
			return list.ToArray();
			IL_116:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㝃㉅㩇ᡉⵋ⁍㝏㝑", a_));
		}
		}
	}

	// Token: 0x06005058 RID: 20568 RVA: 0x00326534 File Offset: 0x00325534
	private TAddr ᜂ(string A_0)
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			int num = 5;
			TAddr result;
			for (;;)
			{
				string empty;
				string empty2;
				string empty3;
				string empty4;
				switch (num)
				{
				case 0:
					if (A_0 == string.Empty)
					{
						num = 7;
						continue;
					}
					goto IL_BE;
				case 1:
					return result;
				case 2:
					if (FormulaUtil.IsCell(A_0, false, out empty, out empty2))
					{
						num = 3;
						continue;
					}
					num = 4;
					continue;
				case 3:
				{
					int num2 = Convert.ToInt32(empty) - 1;
					int num3 = sprṔ.ᜀ(empty2);
					result = new TAddr(num2, num3, num2, num3);
					num = 1;
					continue;
				}
				case 4:
					if (this.ᜊ.ᜀ(A_0, false, out empty, out empty2, out empty3, out empty4))
					{
						num = 6;
						continue;
					}
					return result;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_BE;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 6:
				{
					int iFirstRow = Convert.ToInt32(empty) - 1;
					int iFirstCol = sprṔ.ᜀ(empty2) - 1;
					int iLastRow = Convert.ToInt32(empty3) - 1;
					int iLastCol = sprṔ.ᜀ(empty4) - 1;
					result = new TAddr(iFirstRow, iFirstCol, iLastRow, iLastCol);
					num = 9;
					continue;
				}
				case 7:
					goto IL_12D;
				case 8:
					if (true)
					{
					}
					num = 0;
					continue;
				case 9:
					goto IL_172;
				}
				if (A_0 != null)
				{
					num = 8;
					continue;
				}
				break;
				IL_BE:
				empty = string.Empty;
				empty2 = string.Empty;
				empty3 = string.Empty;
				empty4 = string.Empty;
				result = default(TAddr);
				num = 2;
			}
			IL_12D:
			goto IL_174;
			IL_172:
			return result;
			IL_174:
			throw new ArgumentNullException(RecordTableEnumerator.b("伻䨽㈿၁╃⡅⽇⽉", a_));
		}
		}
	}

	// Token: 0x06005059 RID: 20569 RVA: 0x003266FC File Offset: 0x003256FC
	private FilterConditionType ᜁ(string A_0)
	{
		int a_ = 16;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_157;
			case 1:
				spr\u22D2.ដ = new Dictionary<string, int>(6)
				{
					{
						RecordTableEnumerator.b("⍅㥇㽉ⵋ≍", a_),
						0
					},
					{
						RecordTableEnumerator.b("ⅅ㩇⽉ⵋ㩍㕏⁑S㹕㥗㑙", a_),
						1
					},
					{
						RecordTableEnumerator.b("ⅅ㩇⽉ⵋ㩍㕏⁑S㹕㥗㑙፛ⱝ╟፡ᅣݥѧ", a_),
						2
					},
					{
						RecordTableEnumerator.b("⩅ⵇ㥉㽋ᩍ㡏㍑㩓", a_),
						3
					},
					{
						RecordTableEnumerator.b("⩅ⵇ㥉㽋ᩍ㡏㍑㩓ᥕ⩗Ὑⵛ⭝ş๡", a_),
						4
					},
					{
						RecordTableEnumerator.b("⡅❇㹉ो㽍╏㍑㡓", a_),
						5
					}
				};
				num = 0;
				continue;
			case 2:
			{
				int num2;
				if (spr\u22D2.ដ.TryGetValue(A_0, out num2))
				{
					num = 6;
					continue;
				}
				goto IL_1B8;
			}
			case 3:
				num = 9;
				continue;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_12D;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 5:
			{
				int num2;
				switch (num2)
				{
				case 0:
					goto IL_7E;
				case 1:
					return FilterConditionType.Greater;
				case 2:
					return FilterConditionType.GreaterOrEqual;
				case 3:
					return FilterConditionType.Less;
				case 4:
					return FilterConditionType.LessOrEqual;
				case 5:
					return FilterConditionType.NotEqual;
				default:
					num = 8;
					continue;
				}
				break;
			}
			case 6:
				num = 5;
				continue;
			case 7:
				goto IL_155;
			case 8:
				num = 7;
				continue;
			case 9:
				goto IL_12D;
			}
			if (A_0 != null)
			{
				num = 3;
				continue;
			}
			goto IL_1B8;
			IL_12D:
			if (spr\u22D2.ដ == null)
			{
				num = 1;
				continue;
			}
			IL_157:
			num = 2;
		}
		return FilterConditionType.GreaterOrEqual;
		IL_7E:
		if (true)
		{
		}
		return FilterConditionType.Equal;
		IL_155:
		IL_1B8:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㕅㱇㡉ཋ⅍㹏㙑㵓≕ㅗ㕙㉛", a_));
	}

	// Token: 0x0600505A RID: 20570 RVA: 0x003268D4 File Offset: 0x003258D4
	private ConditionalFormatType ᜁ(string A_0, out bool A_1)
	{
		int a_ = 17;
		for (;;)
		{
			A_1 = true;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0 != null)
					{
						num = 12;
						continue;
					}
					goto IL_196;
				case 1:
					num = 5;
					continue;
				case 2:
					if (!(A_0 == RecordTableEnumerator.b("⑆♈❊≌㵎ɐげ㑔㭖㱘", a_)))
					{
						num = 9;
						continue;
					}
					goto IL_D1;
				case 3:
					if (!(A_0 == RecordTableEnumerator.b("≆ㅈ㭊㽌⩎≐⁒㱔㡖㝘", a_)))
					{
						num = 1;
						continue;
					}
					return ConditionalFormatType.Formula;
				case 4:
					goto IL_CD;
				case 5:
					if (!(A_0 == RecordTableEnumerator.b("⍆⡈㽊ⱌൎぐ⅒", a_)))
					{
						num = 7;
						continue;
					}
					return ConditionalFormatType.DataBar;
				case 6:
					if (!(A_0 == RecordTableEnumerator.b("⑆ⱈ❊⅌َ≐", a_)))
					{
						num = 4;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_CD;
					default:
						goto IL_15A;
					}
					break;
				case 7:
					num = 11;
					continue;
				case 8:
					num = 2;
					continue;
				case 9:
					num = 10;
					continue;
				case 10:
					goto IL_6C;
				case 11:
					if (!(A_0 == RecordTableEnumerator.b("⹆⩈⑊⍌ᱎ㑐❒", a_)))
					{
						num = 8;
						continue;
					}
					return ConditionalFormatType.IconSet;
				case 12:
					num = 6;
					continue;
				}
				break;
				IL_CD:
				num = 3;
			}
		}
		return ConditionalFormatType.Formula;
		IL_6C:
		goto IL_196;
		IL_D1:
		if (true)
		{
		}
		return ConditionalFormatType.ColorScale;
		IL_15A:
		if (false)
		{
		}
		return ConditionalFormatType.CellValue;
		IL_196:
		A_1 = false;
		return ConditionalFormatType.CellValue;
	}

	// Token: 0x0600505B RID: 20571 RVA: 0x00326A7C File Offset: 0x00325A7C
	private ComparisonOperatorType ᜀ(string A_0, out bool A_1)
	{
		int a_ = 11;
		for (;;)
		{
			A_1 = true;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					int num2;
					if (spr\u22D2.ឋ.TryGetValue(A_0, out num2))
					{
						num = 7;
						continue;
					}
					goto IL_266;
				}
				case 1:
					if (A_0 != null)
					{
						num = 6;
						continue;
					}
					goto IL_266;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1AF;
					default:
						if (false)
						{
						}
						if (spr\u22D2.ឋ == null)
						{
							num = 8;
							continue;
						}
						goto IL_65;
					}
					break;
				case 3:
					goto IL_65;
				case 4:
					num = 9;
					continue;
				case 5:
				{
					int num2;
					switch (num2)
					{
					case 0:
						return ComparisonOperatorType.Between;
					case 1:
						return ComparisonOperatorType.Equal;
					case 2:
						return ComparisonOperatorType.Greater;
					case 3:
						return ComparisonOperatorType.GreaterOrEqual;
					case 4:
						return ComparisonOperatorType.Less;
					case 5:
						goto IL_59;
					case 6:
						return ComparisonOperatorType.None;
					case 7:
						return ComparisonOperatorType.NotBetween;
					case 8:
						return ComparisonOperatorType.NotEqual;
					case 9:
					case 10:
					case 11:
						goto IL_1AF;
					default:
						num = 4;
						continue;
					}
					break;
				}
				case 6:
					num = 2;
					continue;
				case 7:
					num = 5;
					continue;
				case 8:
					spr\u22D2.ឋ = new Dictionary<string, int>(12)
					{
						{
							RecordTableEnumerator.b("⍀♂ㅄうⱈ⹊⍌", a_),
							0
						},
						{
							RecordTableEnumerator.b("⑀㉂い♆╈", a_),
							1
						},
						{
							RecordTableEnumerator.b("♀ㅂ⁄♆㵈⹊㽌᭎㥐㉒㭔", a_),
							2
						},
						{
							RecordTableEnumerator.b("♀ㅂ⁄♆㵈⹊㽌᭎㥐㉒㭔ᡖ⭘Ṛⱜ⩞`ར", a_),
							3
						},
						{
							RecordTableEnumerator.b("ⵀ♂㙄㑆ᵈ⍊ⱌⅎ", a_),
							4
						},
						{
							RecordTableEnumerator.b("ⵀ♂㙄㑆ᵈ⍊ⱌⅎṐ⅒ၔ♖ⱘ㩚ㅜ", a_),
							5
						},
						{
							RecordTableEnumerator.b("⽀ⱂㅄц♈╊㥌⹎㡐㵒♔", a_),
							6
						},
						{
							RecordTableEnumerator.b("⽀ⱂㅄՆⱈ㽊㩌⩎㑐㵒", a_),
							7
						},
						{
							RecordTableEnumerator.b("⽀ⱂㅄɆ㡈㹊ⱌ⍎", a_),
							8
						},
						{
							RecordTableEnumerator.b("⍀♂≄⹆❈㡊ᩌ♎═㭒", a_),
							9
						},
						{
							RecordTableEnumerator.b("≀ⱂ⭄㍆⡈≊⍌㱎Ր㙒ⵔ⍖", a_),
							10
						},
						{
							RecordTableEnumerator.b("⑀ⵂ⅄㑆Ṉ≊㥌❎", a_),
							11
						}
					};
					num = 3;
					continue;
				case 9:
					goto IL_21D;
				}
				break;
				IL_65:
				num = 0;
			}
		}
		IL_59:
		if (true)
		{
		}
		return ComparisonOperatorType.LessOrEqual;
		IL_1AF:
		A_1 = false;
		return ComparisonOperatorType.Between;
		IL_21D:
		IL_266:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㉀㝂㝄ࡆ㥈⹊㽌⹎═㱒❔", a_));
	}

	// Token: 0x0600505C RID: 20572 RVA: 0x00326D04 File Offset: 0x00325D04
	private string ᜀ(XmlReader A_0)
	{
		int num = 3;
		string result;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_B6;
			case 1:
				goto IL_38;
			case 2:
				goto IL_4F;
			case 4:
				result = A_0.Value;
				A_0.Skip();
				num = 2;
				continue;
			case 5:
				if (A_0.NodeType != XmlNodeType.EndElement)
				{
					num = 4;
					continue;
				}
				result = string.Empty;
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_4F;
				default:
					if (false)
					{
					}
					num = 0;
					continue;
				}
				break;
			}
			if (A_0.IsEmptyElement)
			{
				num = 1;
			}
			else
			{
				A_0.Read();
				num = 5;
			}
		}
		IL_38:
		A_0.Read();
		return string.Empty;
		IL_4F:
		IL_B6:
		A_0.Skip();
		return result;
	}

	// Token: 0x0600505D RID: 20573 RVA: 0x00326DDC File Offset: 0x00325DDC
	internal Color ᜁ(Color A_0, double A_1)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		byte alpha = (byte)((double)A_0.A * A_1);
		byte red = (byte)((double)A_0.R * A_1);
		byte green = (byte)((double)A_0.G * A_1);
		byte blue = (byte)((double)A_0.B * A_1);
		return Color.FromArgb((int)alpha, (int)red, (int)green, (int)blue);
	}

	// Token: 0x0600505E RID: 20574 RVA: 0x00326E50 File Offset: 0x00325E50
	private string ᜀ(string A_0)
	{
		int a_ = 6;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		A_0 = A_0.Replace(RecordTableEnumerator.b("挻䘽瀿牁瑃❅ᝇ", a_), RecordTableEnumerator.b("㘻", a_));
		A_0 = A_0.Replace(RecordTableEnumerator.b("挻䘽瀿牁瑃≅ᝇ", a_), RecordTableEnumerator.b("ㄻ", a_));
		A_0 = A_0.Replace(RecordTableEnumerator.b("挻䘽瀿牁瑃罅ᝇ", a_), RecordTableEnumerator.b("㔻", a_));
		A_0 = A_0.Replace(RecordTableEnumerator.b("挻䘽瀿牁瑃繅ᝇ", a_), RecordTableEnumerator.b("㐻", a_));
		A_0 = A_0.Replace(RecordTableEnumerator.b("挻䘽瀿牁瑃癅ᝇ", a_), RecordTableEnumerator.b("㰻", a_));
		return A_0;
	}

	// Token: 0x0600505F RID: 20575 RVA: 0x00326F4C File Offset: 0x00325F4C
	public static void ᜀ(XlsFill A_0, spr\u192F A_1)
	{
		while (A_0.FillType == ShapeFillType.Gradient)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (false)
				{
				}
				if (true)
				{
				}
				A_1.ᜀ(new XlsShapeFill(A_1.AppImplementation, A_1, ShapeFillType.Gradient));
				IGradient gradient = A_1.ᝐ();
				gradient.GradientStyle = A_0.GradientStyle;
				gradient.GradientVariant = A_0.GradientVariant;
				gradient.BackColorObject.ᜀ(A_0.PatternColorObject, true);
				gradient.ForeColorObject.ᜀ(A_0.OColor, true);
				A_1.ᜑ().ᜌ(4000);
				return;
			}
			}
		}
		A_1.\u170D(true);
		A_1.ᝄ().ᜀ(A_0.OColor, true);
		A_1.\u1754().ᜀ(A_0.PatternColorObject, true);
		A_1.ᜀ(A_0.Pattern);
	}

	// Token: 0x040023FC RID: 9212
	internal const byte ᜀ = 255;

	// Token: 0x040023FD RID: 9213
	private const byte ᜁ = 255;

	// Token: 0x040023FE RID: 9214
	private const double ᜂ = 170.0;

	// Token: 0x040023FF RID: 9215
	public const int ᜃ = 4;

	// Token: 0x04002400 RID: 9216
	private const string ᜄ = "_x000d_";

	// Token: 0x04002401 RID: 9217
	private const string ᜅ = "_x000a_";

	// Token: 0x04002402 RID: 9218
	private const string ᜆ = "_x0000_";

	// Token: 0x04002403 RID: 9219
	private const string ᜇ = "_x0008_";

	// Token: 0x04002404 RID: 9220
	private const string ᜈ = "_x0009_";

	// Token: 0x04002405 RID: 9221
	private XlsWorkbook ᜉ;

	// Token: 0x04002406 RID: 9222
	private FormulaUtil ᜊ;

	// Token: 0x04002407 RID: 9223
	private Dictionary<int, ShapeParser> ᜋ;

	// Token: 0x04002408 RID: 9224
	private List<Color> ᜌ;

	// Token: 0x04002409 RID: 9225
	private Dictionary<string, Color> \u170D;

	// Token: 0x0400240A RID: 9226
	private int? ᜎ;

	// Token: 0x0400240B RID: 9227
	private Dictionary<string, XlsFont> ᜏ;

	// Token: 0x0400240C RID: 9228
	private Dictionary<string, XlsFont> ᜐ;

	// Token: 0x0400240D RID: 9229
	private bool ᜑ;

	// Token: 0x0400240E RID: 9230
	private XlsWorksheet \u1712;

	// Token: 0x02000521 RID: 1313
	// (Invoke) Token: 0x06005061 RID: 20577
	private delegate SheetProtectionType ᜀ(XmlReader A_0, string A_1, SheetProtectionType A_2, bool A_3, SheetProtectionType A_4);
}
