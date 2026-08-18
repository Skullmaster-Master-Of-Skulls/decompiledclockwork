using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Xml;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.XmlSerialization;

// Token: 0x0200057E RID: 1406
[spr\u2576(XmlSaveType.MSExcel)]
internal class sprỉ : spr\u2127
{
	// Token: 0x0600547B RID: 21627 RVA: 0x00349F94 File Offset: 0x00348F94
	public static long ᜀ(int A_0, long A_1)
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
		long num = (long)sprṔ.ᜀ(A_1);
		long num2 = (long)sprṔ.ᜁ(A_1);
		return (num << 32) + (num2 << 16) + (long)A_0;
	}

	// Token: 0x0600547C RID: 21628 RVA: 0x00349FEC File Offset: 0x00348FEC
	public static int ᜁ(long A_0)
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
		return (int)(A_0 & 65535L);
	}

	// Token: 0x0600547D RID: 21629 RVA: 0x0034A030 File Offset: 0x00349030
	public static long ᜀ(long A_0)
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
		int a_ = (int)(A_0 >> 32 & (long)((ulong)-1));
		int a_2 = (int)(A_0 >> 16 & 65535L);
		return sprṔ.ᜀ(a_2, a_);
	}

	// Token: 0x0600547E RID: 21630 RVA: 0x0034A088 File Offset: 0x00349088
	public sprỉ()
	{
		int a_ = 10;
		this.៩ = new string[]
		{
			RecordTableEnumerator.b("⸿ⵁ⩃⍅", a_),
			RecordTableEnumerator.b("㌿ⵁ⡃⽅ⱇ", a_),
			RecordTableEnumerator.b("✿ぁ╃㽅敇罉籋", a_),
			RecordTableEnumerator.b("✿ぁ╃㽅敇絉祋", a_),
			RecordTableEnumerator.b("✿ぁ╃㽅敇硉祋", a_),
			RecordTableEnumerator.b("⠿ⵁ㙃㱅敇㥉㡋㱍㥏≑ㅓ", a_),
			RecordTableEnumerator.b("㘿❁㙃㉅敇㥉㡋㱍㥏≑ㅓ", a_),
			RecordTableEnumerator.b("㈿❁㉃⍅㩇㥉⥋捍㑏㭑㕓ㅕ畗⥙⡛ⱝय़ቡţ", a_),
			RecordTableEnumerator.b("␿⭁╃ⅅ敇㥉㡋㱍㥏≑ㅓ", a_),
			RecordTableEnumerator.b("␿⭁╃ⅅ敇⥉㹋⅍⍏⅑", a_),
			RecordTableEnumerator.b("㐿⩁ⵃ╅⍇杉⡋❍ㅏ㕑祓㕕⩗㕙⽛ⵝ", a_),
			RecordTableEnumerator.b("㐿⩁ⵃ⡅敇≉⍋㱍⩏网❓≕⩗㍙ⱛ㭝", a_),
			RecordTableEnumerator.b("㐿⩁ⵃ⡅敇㱉⥋㱍⑏网❓≕⩗㍙ⱛ㭝", a_),
			RecordTableEnumerator.b("㐿⩁ⵃ⡅敇㡉⥋㡍㕏⁑❓㍕畗㹙㕛㽝ݟ佡ᝣብᩧͩᱫ୭", a_),
			RecordTableEnumerator.b("㐿⩁ⵃ⡅敇⹉╋⽍㝏网❓≕⩗㍙ⱛ㭝", a_),
			RecordTableEnumerator.b("㐿⩁ⵃ⡅敇≉⍋㱍⩏网㝓⑕㝗⥙⽛", a_),
			RecordTableEnumerator.b("㐿⩁ⵃ⡅敇⹉╋⽍㝏网㝓⑕㝗⥙⽛", a_),
			RecordTableEnumerator.b("✿ぁ╃㽅敇等繋筍", a_),
			RecordTableEnumerator.b("✿ぁ╃㽅敇穉穋籍敏", a_)
		};
		this.\u17EA = new string[]
		{
			RecordTableEnumerator.b("⸿ⵁ⩃⍅", a_),
			RecordTableEnumerator.b("渿睁㑃㉅桇㥉⍋≍㥏㙑", a_),
			RecordTableEnumerator.b("焿汁瑃㙅㱇橉㽋⅍㱏㭑こ", a_),
			RecordTableEnumerator.b("渿睁㑃㉅桇⹉ⵋ㵍㡏㝑こ", a_),
			RecordTableEnumerator.b("渿睁㑃㉅桇⹉⍋㩍⑏㝑こ", a_),
			RecordTableEnumerator.b("焿汁煃㙅㱇橉㽋⅍㱏㭑こ", a_),
			RecordTableEnumerator.b("爿汁瑃㙅㱇橉⡋⅍╏け㡓㍕", a_),
			RecordTableEnumerator.b("渿睁㑃㉅桇≉ⵋ❍≏㹑㵓㡕㵗", a_),
			RecordTableEnumerator.b("焿汁瑃㙅㱇橉⡋⽍⍏㩑ㅓ㉕", a_),
			RecordTableEnumerator.b("渿睁㑃㉅桇⹉⍋㩍絏㙑㕓╕し", a_),
			RecordTableEnumerator.b("焿汁瑃㙅㱇橉⡋⅍⑏网こ㝕⭗㉙", a_),
			RecordTableEnumerator.b("渿睁㑃㉅桇⹉⍋㩍絏㙑㭓≕畗㹙㵛ⵝ࡟", a_),
			RecordTableEnumerator.b("焿汁瑃㙅㱇橉⡋⅍⑏网こ㥕ⱗ睙㡛㽝፟੡", a_),
			RecordTableEnumerator.b("焿汁瑃㙅㱇橉⡋⅍⑏网こ㝕⭗㉙煛ⵝ౟͡੣ብ൧๩", a_)
		};
		this.\u17F1 = new string[]
		{
			"",
			RecordTableEnumerator.b("ి❁㝃㕅᱇≉ⵋ⁍", a_),
			RecordTableEnumerator.b("Կ㍁ㅃ❅⑇㥉", a_),
			RecordTableEnumerator.b("ి❁㝃㕅᱇≉ⵋ⁍὏⁑ᅓ❕ⵗ㭙せ", a_),
			RecordTableEnumerator.b("ܿぁ⅃❅㱇⽉㹋ᩍ㡏㍑㩓", a_),
			RecordTableEnumerator.b("пⵁ⅃㕅ه╉㡋୍⅏❑㕓㩕", a_),
			RecordTableEnumerator.b("ܿぁ⅃❅㱇⽉㹋ᩍ㡏㍑㩓ᥕ⩗Ὑⵛ⭝ş๡", a_)
		};
		this.\u17F2 = new Dictionary<long, int>();
		base..ctor();
	}

	// Token: 0x0600547F RID: 21631 RVA: 0x0034A38C File Offset: 0x0034938C
	private void ᜀ(XmlWriter A_0, INameRanges A_1, bool A_2)
	{
		int a_ = 4;
		int num = 6;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				if (A_1 != null)
				{
					int count = A_1.Count;
					if (true)
					{
					}
					num = 12;
					continue;
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
					num = 9;
					continue;
				}
				break;
			case 1:
				goto IL_156;
			case 2:
				goto IL_13C;
			case 3:
				goto IL_58;
			case 4:
				goto IL_EB;
			case 5:
			{
				int count;
				if (num2 >= count)
				{
					num = 1;
					continue;
				}
				INamedRange namedRange = A_1[num2];
				num = 10;
				continue;
			}
			case 7:
				return;
			case 8:
			{
				INamedRange namedRange;
				this.ᜀ(A_0, namedRange);
				num = 4;
				continue;
			}
			case 9:
				goto IL_E6;
			case 10:
			{
				INamedRange namedRange;
				if (namedRange.IsLocal == A_2)
				{
					num = 8;
					continue;
				}
				goto IL_EB;
			}
			case 11:
				goto IL_13C;
			case 12:
			{
				int count;
				if (count == 0)
				{
					num = 7;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("䤹伻", a_), RecordTableEnumerator.b("琹崻匽┿ㅁ", a_), null);
				num2 = 0;
				num = 11;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			num = 0;
			continue;
			IL_EB:
			num2++;
			num = 2;
			continue;
			IL_13C:
			num = 5;
		}
		IL_58:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴹主圽㐿❁㙃", a_));
		IL_E6:
		throw new ArgumentNullException(RecordTableEnumerator.b("吹崻匽┿ㅁ", a_));
		IL_156:
		A_0.WriteEndElement();
	}

	// Token: 0x06005480 RID: 21632 RVA: 0x0034A53C File Offset: 0x0034953C
	private void ᜀ(XmlWriter A_0, INamedRange A_1)
	{
		int a_ = 7;
		int num = 2;
		for (;;)
		{
			string text;
			string text2;
			switch (num)
			{
			case 0:
				text = RecordTableEnumerator.b("<᰾ፀق̈́晆", a_);
				num = 12;
				continue;
			case 1:
				goto IL_69;
			case 3:
				if (A_1.Visible)
				{
					goto IL_27E;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1F5;
				default:
					if (false)
					{
					}
					num = 13;
					continue;
				}
				break;
			case 4:
				goto IL_67;
			case 5:
				if (text.IndexOf(RecordTableEnumerator.b("Ḽ派рՂ", a_)) != -1)
				{
					num = 0;
					continue;
				}
				goto IL_185;
			case 6:
			{
				string valueR1C;
				text2 = valueR1C;
				goto IL_20B;
			}
			case 7:
				num = 15;
				continue;
			case 8:
			{
				string valueR1C;
				if (valueR1C != null)
				{
					num = 7;
					continue;
				}
				goto IL_69;
			}
			case 9:
				goto IL_180;
			case 10:
				text2 = RecordTableEnumerator.b("<᰾ፀق̈́晆", a_);
				goto IL_20B;
			case 11:
				goto IL_279;
			case 12:
				goto IL_185;
			case 13:
				A_0.WriteAttributeString(RecordTableEnumerator.b("丼䰾", a_), RecordTableEnumerator.b("甼嘾╀❂⁄⥆", a_), null, RecordTableEnumerator.b("఼", a_));
				num = 9;
				continue;
			case 14:
			{
				if (A_1 == null)
				{
					num = 11;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("丼䰾", a_), RecordTableEnumerator.b("猼帾ⱀ♂⅄ᕆ⡈╊⩌⩎", a_), null);
				A_0.WriteAttributeString(RecordTableEnumerator.b("丼䰾", a_), RecordTableEnumerator.b("猼帾ⱀ♂", a_), null, A_1.Name);
				string valueR1C = A_1.ValueR1C1;
				num = 8;
				continue;
			}
			case 15:
			{
				string valueR1C;
				if (valueR1C.Length <= 0)
				{
					num = 1;
					continue;
				}
				goto IL_1F5;
			}
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			num = 14;
			continue;
			IL_69:
			num = 10;
			continue;
			IL_185:
			A_0.WriteAttributeString(RecordTableEnumerator.b("丼䰾", a_), RecordTableEnumerator.b("漼娾❀♂㝄㑆ᵈ⑊", a_), null, text);
			num = 3;
			continue;
			IL_1F5:
			if (true)
			{
			}
			num = 6;
			continue;
			IL_20B:
			text = text2;
			num = 5;
		}
		IL_67:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨼䴾⡀㝂⁄㕆", a_));
		IL_180:
		goto IL_27E;
		IL_279:
		throw new ArgumentNullException(RecordTableEnumerator.b("匼帾ⱀ♂", a_));
		IL_27E:
		A_0.WriteEndElement();
	}

	// Token: 0x06005481 RID: 21633 RVA: 0x0034A7D0 File Offset: 0x003497D0
	private void ᜀ(XmlWriter A_0, sprᢖ A_1, List<spr\u192F> A_2)
	{
		int a_ = 17;
		switch (0)
		{
		default:
		{
			int num = 9;
			for (;;)
			{
				spr\u192F spr_u192F;
				switch (num)
				{
				case 0:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 14;
						continue;
					}
					spr_u192F = A_1.ᜁ(num2);
					int num3 = spr_u192F.ᜯ();
					num = 7;
					continue;
				}
				case 1:
				{
					if (A_1 == null)
					{
						num = 4;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("㑆㩈", a_), RecordTableEnumerator.b("ᑆ㵈㉊⅌⩎≐", a_), null);
					int num2 = 0;
					int count = A_1.Count;
					num = 5;
					continue;
				}
				case 2:
					goto IL_1AA;
				case 3:
					goto IL_81;
				case 4:
					goto IL_131;
				case 5:
					goto IL_1AA;
				case 6:
					if (A_2.Count > 0)
					{
						num = 11;
						continue;
					}
					goto IL_231;
				case 7:
					if (spr_u192F.ᝇ())
					{
						num = 13;
						continue;
					}
					goto IL_133;
				case 8:
					A_2.Add(spr_u192F);
					num = 12;
					continue;
				case 10:
					goto IL_7C;
				case 11:
					this.ᜀ(A_0, A_2);
					num = 15;
					continue;
				case 12:
					if (true)
					{
					}
					goto IL_81;
				case 13:
					num = 16;
					continue;
				case 14:
					num = 6;
					continue;
				case 15:
					goto IL_160;
				case 16:
				{
					int num3;
					if (num3 > spr_u192F.ᜌ())
					{
						num = 8;
						continue;
					}
					goto IL_133;
				}
				}
				if (A_0 == null)
				{
					num = 10;
					continue;
				}
				num = 1;
				continue;
				IL_81:
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
					int num2;
					num2++;
					break;
				}
				}
				num = 2;
				continue;
				IL_133:
				this.ᜄ(A_0, spr_u192F);
				num = 3;
				continue;
				IL_1AA:
				num = 0;
			}
			IL_7C:
			throw new ArgumentNullException(RecordTableEnumerator.b("う㭈≊㥌⩎⍐", a_));
			IL_131:
			throw new ArgumentNullException(RecordTableEnumerator.b("㑆㵈㉊⅌⩎≐", a_));
			IL_160:
			IL_231:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06005482 RID: 21634 RVA: 0x0034AA14 File Offset: 0x00349A14
	private void ᜄ(XmlWriter A_0, spr\u192F A_1)
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			int num = 8;
			for (;;)
			{
				bool flag;
				string text;
				bool flag2;
				string text2;
				string text3;
				switch (num)
				{
				case 0:
					A_0.WriteAttributeString(RecordTableEnumerator.b("䠺丼", a_), RecordTableEnumerator.b("欺尼䴾⑀ⵂㅄ", a_), null, RecordTableEnumerator.b("䠺", a_) + A_1.ᜯ().ToString());
					num = 24;
					continue;
				case 1:
					flag = false;
					goto IL_1B0;
				case 2:
					if (A_1.\u171E() != sprỶ.TXFType.XF_CELL)
					{
						goto IL_2B9;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_34A;
					default:
						if (false)
						{
						}
						num = 25;
						continue;
					}
					break;
				case 3:
					A_0.WriteAttributeString(RecordTableEnumerator.b("䠺丼", a_), RecordTableEnumerator.b("町尼刾⑀", a_), null, text);
					flag2 = false;
					num = 4;
					continue;
				case 4:
					if (true)
					{
					}
					goto IL_2B9;
				case 5:
				{
					XlsStylesCollection xlsStylesCollection = (XlsStylesCollection)A_1.ᜎ().Styles;
					XlsStyle byXFIndex = xlsStylesCollection.GetByXFIndex(A_1.ᜠ());
					num = 26;
					continue;
				}
				case 6:
					if (!flag2)
					{
						num = 5;
						continue;
					}
					goto IL_2B9;
				case 7:
					goto IL_36D;
				case 9:
					flag = (A_1.ᜯ() != 0);
					goto IL_1B0;
				case 10:
					num = 11;
					continue;
				case 11:
					if (text.Length > 0)
					{
						num = 3;
						continue;
					}
					goto IL_2B9;
				case 12:
					if (text != null)
					{
						num = 10;
						continue;
					}
					goto IL_2B9;
				case 13:
				{
					XlsStyle byXFIndex;
					text2 = byXFIndex.Name;
					goto IL_3BB;
				}
				case 14:
					if (A_1.ᜌ() != 0)
					{
						num = 27;
						continue;
					}
					num = 16;
					continue;
				case 15:
					num = 19;
					continue;
				case 16:
					text3 = RecordTableEnumerator.b("缺堼夾⁀㙂⥄㍆", a_);
					goto IL_256;
				case 17:
					goto IL_AB;
				case 18:
					if (A_1 == null)
					{
						num = 7;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("䠺丼", a_), RecordTableEnumerator.b("栺䤼䘾ⵀ♂", a_), null);
					num = 20;
					continue;
				case 19:
					text2 = null;
					goto IL_3BB;
				case 20:
					if (A_1.ᝇ())
					{
						num = 22;
						continue;
					}
					num = 1;
					continue;
				case 21:
					text3 = RecordTableEnumerator.b("䠺", a_) + A_1.ᜌ().ToString();
					goto IL_256;
				case 22:
					num = 9;
					continue;
				case 23:
					if (flag2)
					{
						num = 0;
						continue;
					}
					goto IL_3DE;
				case 24:
					goto IL_34A;
				case 25:
					num = 6;
					continue;
				case 26:
				{
					XlsStyle byXFIndex;
					if (byXFIndex == null)
					{
						num = 15;
						continue;
					}
					num = 13;
					continue;
				}
				case 27:
					num = 21;
					continue;
				}
				if (A_0 == null)
				{
					num = 17;
					continue;
				}
				num = 18;
				continue;
				IL_1B0:
				flag2 = flag;
				num = 14;
				continue;
				IL_256:
				string value = text3;
				A_0.WriteAttributeString(RecordTableEnumerator.b("䠺丼", a_), RecordTableEnumerator.b("爺礼", a_), null, value);
				num = 2;
				continue;
				IL_2B9:
				num = 23;
				continue;
				IL_3BB:
				text = text2;
				num = 12;
			}
			IL_AB:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰺似嘾㕀♂㝄", a_));
			IL_34A:
			goto IL_3DE;
			IL_36D:
			throw new ArgumentNullException(RecordTableEnumerator.b("崺刼䴾ⱀ≂ㅄ", a_));
			IL_3DE:
			this.ᜃ(A_0, A_1);
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06005483 RID: 21635 RVA: 0x0034AE10 File Offset: 0x00349E10
	private void ᜀ(XmlWriter A_0, spr\u192F A_1, List<spr\u192F> A_2)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			int num = 7;
			for (;;)
			{
				string text;
				string text2;
				bool flag;
				bool flag2;
				string text3;
				switch (num)
				{
				case 0:
					text = null;
					goto IL_3BB;
				case 1:
				{
					XlsStyle byXFIndex;
					text = byXFIndex.Name;
					goto IL_3BB;
				}
				case 2:
					A_0.WriteAttributeString(RecordTableEnumerator.b("䰾㉀", a_), RecordTableEnumerator.b("焾⁀⹂⁄", a_), null, text2);
					flag = false;
					num = 3;
					continue;
				case 3:
					goto IL_2B9;
				case 4:
					goto IL_AB;
				case 5:
					if (A_1.ᜌ() != 0)
					{
						num = 23;
						continue;
					}
					num = 25;
					continue;
				case 6:
					flag2 = false;
					goto IL_1A8;
				case 8:
					num = 0;
					continue;
				case 9:
					if (A_1 == null)
					{
						num = 18;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("䰾㉀", a_), RecordTableEnumerator.b("氾㕀㩂⥄≆", a_), null);
					num = 10;
					continue;
				case 10:
					if (A_1.ᝇ())
					{
						num = 26;
						continue;
					}
					num = 6;
					continue;
				case 11:
					if (flag)
					{
						num = 14;
						continue;
					}
					goto IL_3DE;
				case 12:
				{
					XlsStylesCollection xlsStylesCollection = (XlsStylesCollection)A_1.ᜎ().Styles;
					XlsStyle byXFIndex = xlsStylesCollection.GetByXFIndex(A_1.ᜠ());
					num = 24;
					continue;
				}
				case 13:
					num = 17;
					continue;
				case 14:
					A_0.WriteAttributeString(RecordTableEnumerator.b("䰾㉀", a_), RecordTableEnumerator.b("漾⁀ㅂ⁄⥆㵈", a_), null, RecordTableEnumerator.b("䰾", a_) + A_1.ᜯ().ToString());
					num = 20;
					continue;
				case 15:
					if (A_1.\u171E() != sprỶ.TXFType.XF_CELL)
					{
						goto IL_2B9;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_34A;
					default:
						if (false)
						{
						}
						num = 16;
						continue;
					}
					break;
				case 16:
					if (true)
					{
					}
					num = 21;
					continue;
				case 17:
					if (text2.Length > 0)
					{
						num = 2;
						continue;
					}
					goto IL_2B9;
				case 18:
					goto IL_36D;
				case 19:
					flag2 = (A_1.ᜯ() != 0);
					goto IL_1A8;
				case 20:
					goto IL_34A;
				case 21:
					if (!flag)
					{
						num = 12;
						continue;
					}
					goto IL_2B9;
				case 22:
					if (text2 != null)
					{
						num = 13;
						continue;
					}
					goto IL_2B9;
				case 23:
					num = 27;
					continue;
				case 24:
				{
					XlsStyle byXFIndex;
					if (byXFIndex == null)
					{
						num = 8;
						continue;
					}
					num = 1;
					continue;
				}
				case 25:
					text3 = RecordTableEnumerator.b("笾⑀╂⑄㉆╈㽊", a_);
					goto IL_24E;
				case 26:
					num = 19;
					continue;
				case 27:
					text3 = RecordTableEnumerator.b("䰾", a_) + A_1.ᜌ().ToString();
					goto IL_24E;
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				num = 9;
				continue;
				IL_1A8:
				flag = flag2;
				num = 5;
				continue;
				IL_24E:
				string value = text3;
				A_0.WriteAttributeString(RecordTableEnumerator.b("䰾㉀", a_), RecordTableEnumerator.b("瘾Հ", a_), null, value);
				num = 15;
				continue;
				IL_2B9:
				num = 11;
				continue;
				IL_3BB:
				text2 = text;
				num = 22;
			}
			IL_AB:
			throw new ArgumentNullException(RecordTableEnumerator.b("䠾㍀⩂ㅄ≆㭈", a_));
			IL_34A:
			goto IL_3DE;
			IL_36D:
			throw new ArgumentNullException(RecordTableEnumerator.b("夾⹀ㅂ⡄♆㵈", a_));
			IL_3DE:
			this.ᜃ(A_0, A_1);
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06005484 RID: 21636 RVA: 0x0034B20C File Offset: 0x0034A20C
	private void ᜃ(XmlWriter A_0, spr\u192F A_1)
	{
		int a_ = 14;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜀ(A_0, A_1.ᜪ());
				num = 3;
				continue;
			case 1:
				goto IL_10A;
			case 2:
				goto IL_22B;
			case 3:
				return;
			case 4:
				this.ᜀ(A_0, A_1.ᜀ());
				num = 17;
				continue;
			case 5:
				if (A_1.\u1753())
				{
					num = 8;
					continue;
				}
				goto IL_191;
			case 7:
				if (A_1.ᝀ())
				{
					num = 4;
					continue;
				}
				goto IL_208;
			case 8:
				this.ᜀ(A_0, A_1);
				if (true)
				{
				}
				num = 20;
				continue;
			case 9:
				if (A_1.\u1717())
				{
					num = 11;
					continue;
				}
				goto IL_22B;
			case 10:
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				A_1.ᝇ();
				num = 7;
				continue;
			case 11:
				this.ᜂ(A_0, A_1);
				num = 2;
				continue;
			case 12:
				this.ᜀ(A_0, A_1.\u1715());
				num = 16;
				continue;
			case 13:
				if (A_1.\u173D())
				{
					num = 12;
					continue;
				}
				goto IL_1B7;
			case 14:
				goto IL_10C;
			case 15:
				if (!A_1.ᜦ())
				{
					goto IL_10C;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_B3;
				default:
					if (false)
					{
					}
					num = 18;
					continue;
				}
				break;
			case 16:
				goto IL_1B7;
			case 17:
				goto IL_208;
			case 18:
				this.ᜁ(A_0, A_1);
				num = 14;
				continue;
			case 19:
				if (A_1.\u1719())
				{
					num = 0;
					continue;
				}
				return;
			case 20:
				goto IL_191;
			case 21:
				goto IL_7C;
			}
			if (A_0 == null)
			{
				num = 21;
				continue;
			}
			num = 10;
			continue;
			IL_10C:
			num = 13;
			continue;
			IL_191:
			num = 19;
			continue;
			IL_1B7:
			num = 5;
			continue;
			IL_208:
			num = 9;
			continue;
			IL_22B:
			num = 15;
		}
		IL_7C:
		throw new ArgumentNullException(RecordTableEnumerator.b("㍃㑅ⅇ㹉⥋㱍", a_));
		IL_B3:
		throw new ArgumentNullException(RecordTableEnumerator.b("≃⥅㩇❉ⵋ㩍", a_));
		IL_10A:
		goto IL_B3;
	}

	// Token: 0x06005485 RID: 21637 RVA: 0x0034B488 File Offset: 0x0034A488
	private void ᜀ(XmlWriter A_0, IFont A_1)
	{
		int a_ = 13;
		int num = 20;
		for (;;)
		{
			string text;
			switch (num)
			{
			case 0:
				A_0.WriteAttributeString(RecordTableEnumerator.b("あ㙄", a_), RecordTableEnumerator.b("၂ⱄ㵆ⱈ", a_), null, XmlConvert.ToString(A_1.Size));
				num = 18;
				continue;
			case 1:
				if (A_1.FontName != RecordTableEnumerator.b("ɂ㝄⹆⡈❊", a_))
				{
					goto IL_274;
				}
				goto IL_2FB;
			case 2:
				A_0.WriteAttributeString(RecordTableEnumerator.b("あ㙄", a_), RecordTableEnumerator.b("ూい㍆╈≊⍌⩎", a_), null, RecordTableEnumerator.b("牂", a_));
				num = 14;
				continue;
			case 3:
				goto IL_2F6;
			case 4:
				A_0.WriteAttributeString(RecordTableEnumerator.b("あ㙄", a_), RecordTableEnumerator.b("၂ㅄ㕆⁈⁊⡌᭎㥐⅒㩔≖㹘㍚", a_), null, RecordTableEnumerator.b("牂", a_));
				num = 24;
				continue;
			case 5:
				goto IL_A9;
			case 6:
				if (text != RecordTableEnumerator.b("ൂ⩄⥆ⱈ", a_))
				{
					num = 27;
					continue;
				}
				goto IL_533;
			case 7:
				if (A_1.Underline != FontUnderlineType.None)
				{
					num = 29;
					continue;
				}
				goto IL_1CE;
			case 8:
				if (A_1.IsItalic)
				{
					num = 16;
					continue;
				}
				goto IL_49C;
			case 9:
				if (A_1 == null)
				{
					num = 10;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("あ㙄", a_), RecordTableEnumerator.b("Ղ⩄⥆㵈", a_), null);
				num = 11;
				continue;
			case 10:
				goto IL_4ED;
			case 11:
				if (A_1.IsBold)
				{
					num = 23;
					continue;
				}
				goto IL_24B;
			case 12:
				if (A_1.Size != 10.0)
				{
					num = 0;
					continue;
				}
				goto IL_1AB;
			case 13:
				if (!((XlsFont)A_1).MacOSShadow)
				{
					goto IL_465;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_274;
				default:
					if (false)
					{
					}
					num = 28;
					continue;
				}
				break;
			case 14:
				goto IL_EC;
			case 15:
				goto IL_24B;
			case 16:
				A_0.WriteAttributeString(RecordTableEnumerator.b("あ㙄", a_), RecordTableEnumerator.b("ੂㅄ♆╈≊⹌", a_), null, RecordTableEnumerator.b("牂", a_));
				num = 26;
				continue;
			case 17:
				if (A_1.IsStrikethrough)
				{
					num = 4;
					continue;
				}
				goto IL_174;
			case 18:
				goto IL_1AB;
			case 19:
				goto IL_1CE;
			case 21:
				goto IL_2FB;
			case 22:
				A_0.WriteAttributeString(RecordTableEnumerator.b("あ㙄", a_), RecordTableEnumerator.b("Ղ⩄⥆㵈Պⱌ≎㑐", a_), null, A_1.FontName);
				num = 21;
				continue;
			case 23:
				A_0.WriteAttributeString(RecordTableEnumerator.b("あ㙄", a_), RecordTableEnumerator.b("ł⩄⭆ⵈ", a_), null, RecordTableEnumerator.b("牂", a_));
				num = 15;
				continue;
			case 24:
				goto IL_174;
			case 25:
				if (((XlsFont)A_1).MacOSOutlineFont)
				{
					num = 2;
					continue;
				}
				goto IL_EC;
			case 26:
				goto IL_49C;
			case 27:
				A_0.WriteAttributeString(RecordTableEnumerator.b("あ㙄", a_), RecordTableEnumerator.b("ᕂ⁄㕆㵈≊⹌⹎㵐ቒ㥔㹖㹘㕚", a_), null, text);
				num = 3;
				continue;
			case 28:
				A_0.WriteAttributeString(RecordTableEnumerator.b("あ㙄", a_), RecordTableEnumerator.b("၂ⵄ♆ⵈ⑊㩌", a_), null, RecordTableEnumerator.b("牂", a_));
				num = 30;
				continue;
			case 29:
				A_0.WriteAttributeString(RecordTableEnumerator.b("あ㙄", a_), RecordTableEnumerator.b("ᙂ⭄⍆ⱈ㥊⅌♎㽐㙒", a_), null, A_1.Underline.ToString());
				num = 19;
				continue;
			case 30:
				goto IL_465;
			}
			if (A_0 == null)
			{
				num = 5;
				continue;
			}
			num = 9;
			continue;
			IL_EC:
			num = 13;
			continue;
			IL_174:
			num = 7;
			continue;
			IL_1AB:
			num = 17;
			continue;
			IL_1CE:
			text = this.ᜀ(A_1);
			num = 6;
			continue;
			IL_24B:
			num = 1;
			continue;
			IL_274:
			num = 22;
			continue;
			IL_2FB:
			A_0.WriteAttributeString(RecordTableEnumerator.b("あ㙄", a_), RecordTableEnumerator.b("B⩄⭆♈㥊", a_), null, this.ᜀ(A_1.Color));
			num = 8;
			continue;
			IL_465:
			if (true)
			{
			}
			num = 12;
			continue;
			IL_49C:
			num = 25;
		}
		IL_A9:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑂㝄⹆㵈⹊㽌", a_));
		IL_2F6:
		goto IL_533;
		IL_4ED:
		throw new ArgumentNullException(RecordTableEnumerator.b("╂⩄⥆㵈", a_));
		IL_533:
		A_0.WriteEndElement();
	}

	// Token: 0x06005486 RID: 21638 RVA: 0x0034B9D0 File Offset: 0x0034A9D0
	private void ᜂ(XmlWriter A_0, spr\u192F A_1)
	{
		int a_ = 3;
		int num = 5;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_98;
				default:
					if (false)
					{
					}
					A_0.WriteAttributeString(RecordTableEnumerator.b("䨸䠺", a_), RecordTableEnumerator.b("椸䤺刼䬾⑀⁂ㅄ≆ⵈ", a_), null, RecordTableEnumerator.b("स", a_));
					num = 6;
					continue;
				}
				break;
			case 1:
				goto IL_11B;
			case 2:
				if (A_1.\u1755())
				{
					num = 8;
					continue;
				}
				goto IL_1AD;
			case 3:
				if (!A_1.ᝎ())
				{
					num = 0;
					continue;
				}
				goto IL_16F;
			case 4:
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("䨸䠺", a_), RecordTableEnumerator.b("椸䤺刼䬾⑀⁂ㅄ⹆♈╊", a_), null);
				num = 3;
				continue;
			case 6:
				goto IL_98;
			case 7:
				goto IL_159;
			case 8:
				A_0.WriteAttributeString(RecordTableEnumerator.b("䄸", a_), RecordTableEnumerator.b("焸刺夼娾݀ⱂ㝄⩆㱈❊ⱌ", a_), null, RecordTableEnumerator.b("࠸", a_));
				num = 7;
				continue;
			case 9:
				goto IL_57;
			}
			if (A_0 == null)
			{
				num = 9;
				continue;
			}
			num = 4;
			continue;
			IL_16F:
			num = 2;
			continue;
			IL_98:
			goto IL_16F;
		}
		IL_57:
		throw new ArgumentNullException(RecordTableEnumerator.b("丸䤺吼䬾⑀ㅂ", a_));
		IL_11B:
		throw new ArgumentNullException(RecordTableEnumerator.b("弸吺似刾⁀㝂", a_));
		IL_159:
		IL_1AD:
		A_0.WriteEndElement();
	}

	// Token: 0x06005487 RID: 21639 RVA: 0x0034BB90 File Offset: 0x0034AB90
	private void ᜁ(XmlWriter A_0, spr\u192F A_1)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			int num = 15;
			for (;;)
			{
				int num3;
				int num2;
				switch (num)
				{
				case 0:
					goto IL_139;
				case 1:
					goto IL_32A;
				case 2:
					num2 = num3;
					goto IL_41B;
				case 3:
					if (A_1.ᜋ() != HorizontalAlignType.General)
					{
						num = 6;
						continue;
					}
					goto IL_C9;
				case 4:
					if (A_1.\u171A() != 0)
					{
						num = 27;
						continue;
					}
					goto IL_206;
				case 5:
					num = 2;
					continue;
				case 6:
				{
					string value = this.ᜀ(A_1.ᜋ());
					A_0.WriteAttributeString(RecordTableEnumerator.b("㭇㥉", a_), RecordTableEnumerator.b("G╉㹋❍⩏㵑㩓≕㥗㙙", a_), null, value);
					num = 23;
					continue;
				}
				case 7:
					goto IL_AF;
				case 8:
					num2 = 90 - num3;
					goto IL_41B;
				case 9:
					goto IL_3B2;
				case 10:
					if (A_1.ᝏ())
					{
						num = 11;
						continue;
					}
					goto IL_32A;
				case 11:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_452;
					default:
						if (false)
						{
						}
						A_0.WriteAttributeString(RecordTableEnumerator.b("㭇㥉", a_), RecordTableEnumerator.b("ᭇ≉㹋❍㹏㥑S㥕ṗ㍙⡛", a_), null, RecordTableEnumerator.b("祇", a_));
						num = 1;
						continue;
					}
					break;
				case 12:
					if (A_1.\u1733())
					{
						num = 19;
						continue;
					}
					goto IL_499;
				case 13:
					if (num3 != 255)
					{
						num = 24;
						continue;
					}
					A_0.WriteAttributeString(RecordTableEnumerator.b("㭇㥉", a_), RecordTableEnumerator.b("ṇ⽉㹋㩍㥏ㅑ㕓㩕౗㽙⑛⩝", a_), null, RecordTableEnumerator.b("祇", a_));
					num = 0;
					continue;
				case 14:
					goto IL_206;
				case 16:
					if (A_1.\u171C() != ReadingOrderType.Context)
					{
						num = 18;
						continue;
					}
					goto IL_27D;
				case 17:
					if (A_1 == null)
					{
						num = 9;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("㭇㥉", a_), RecordTableEnumerator.b("े♉╋⥍㹏㽑ㅓ㡕ⱗ", a_), null);
					num = 3;
					continue;
				case 18:
					A_0.WriteAttributeString(RecordTableEnumerator.b("㭇㥉", a_), RecordTableEnumerator.b("ᩇ⽉ⵋ⩍㥏㱑㍓ᥕ⩗㹙㥛ⱝ", a_), null, A_1.\u171C().ToString());
					num = 28;
					continue;
				case 19:
					A_0.WriteAttributeString(RecordTableEnumerator.b("㭇㥉", a_), RecordTableEnumerator.b("὇㡉ⵋ㹍я㝑ⱓ≕", a_), null, RecordTableEnumerator.b("祇", a_));
					num = 22;
					continue;
				case 20:
					goto IL_452;
				case 21:
					num = 13;
					continue;
				case 22:
					goto IL_3F1;
				case 23:
					goto IL_C9;
				case 24:
					num = 26;
					continue;
				case 25:
					if (num3 != 0)
					{
						num = 21;
						continue;
					}
					goto IL_139;
				case 26:
					if (num3 <= 90)
					{
						num = 5;
						continue;
					}
					num = 8;
					continue;
				case 27:
					if (true)
					{
					}
					A_0.WriteAttributeString(RecordTableEnumerator.b("㭇㥉", a_), RecordTableEnumerator.b("Ň⑉⡋⭍㹏♑", a_), null, A_1.\u171A().ToString());
					num = 14;
					continue;
				case 28:
					goto IL_27D;
				}
				if (A_0 == null)
				{
					num = 7;
					continue;
				}
				num = 17;
				continue;
				IL_C9:
				num = 4;
				continue;
				IL_139:
				num = 10;
				continue;
				IL_452:
				goto IL_139;
				IL_206:
				num = 16;
				continue;
				IL_27D:
				num3 = A_1.\u171B();
				num = 25;
				continue;
				IL_32A:
				string value2 = this.ᜀ(A_1.\u171D());
				A_0.WriteAttributeString(RecordTableEnumerator.b("㭇㥉", a_), RecordTableEnumerator.b("ṇ⽉㹋㩍㥏ㅑ㕓㩕", a_), null, value2);
				num = 12;
				continue;
				IL_41B:
				num3 = num2;
				A_0.WriteAttributeString(RecordTableEnumerator.b("㭇㥉", a_), RecordTableEnumerator.b("ᩇ╉㡋⽍⑏㝑", a_), null, num3.ToString());
				num = 20;
			}
			IL_AF:
			throw new ArgumentNullException(RecordTableEnumerator.b("㽇㡉╋㩍㕏⁑", a_));
			IL_3B2:
			throw new ArgumentNullException(RecordTableEnumerator.b("⹇╉㹋⍍ㅏ♑", a_));
			IL_3F1:
			IL_499:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06005488 RID: 21640 RVA: 0x0034C03C File Offset: 0x0034B03C
	private void ᜀ(XmlWriter A_0, string A_1)
	{
		int a_ = 17;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_3C;
			case 1:
				goto IL_65;
			case 2:
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				goto IL_A1;
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
				num = 2;
			}
		}
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("う㭈≊㥌⩎⍐", a_));
		IL_65:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_3C;
		default:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㑆㵈㥊͌㩎㱐ㅒご╖", a_));
		}
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("㑆㩈", a_), RecordTableEnumerator.b("ॆ㱈♊⽌⩎⍐ᕒ㩔╖㑘㩚⥜", a_), null);
		A_0.WriteAttributeString(RecordTableEnumerator.b("㑆㩈", a_), RecordTableEnumerator.b("ņ♈㥊⁌⹎═", a_), null, A_1);
		A_0.WriteEndElement();
	}

	// Token: 0x06005489 RID: 21641 RVA: 0x0034C138 File Offset: 0x0034B138
	private void ᜀ(XmlWriter A_0, spr\u192F A_1)
	{
		int a_ = 5;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!A_1.\u173E())
				{
					num = 1;
					continue;
				}
				goto IL_124;
			case 1:
			{
				string value = this.ᜀ(A_1.\u1732());
				A_0.WriteAttributeString(RecordTableEnumerator.b("䠺丼", a_), RecordTableEnumerator.b("欺尼䬾㕀♂㝄⥆ੈ⑊⅌⁎⍐", a_), null, value);
				num = 2;
				continue;
			}
			case 2:
				goto IL_124;
			case 3:
				if (!A_1.ᝑ())
				{
					num = 12;
					continue;
				}
				goto IL_15B;
			case 5:
			{
				string value2 = sprỉ.\u17F0[(int)A_1.ᜤ()];
				A_0.WriteAttributeString(RecordTableEnumerator.b("䠺丼", a_), RecordTableEnumerator.b("欺尼䬾㕀♂㝄⥆", a_), null, value2);
				num = 8;
				continue;
			}
			case 6:
				if (A_1.ᜤ() != ExcelPatternType.None)
				{
					num = 5;
					continue;
				}
				goto IL_223;
			case 7:
				goto IL_5B;
			case 8:
				goto IL_1BA;
			case 9:
				if (true)
				{
				}
				goto IL_15B;
			case 10:
				goto IL_11F;
			case 11:
				if (A_1 == null)
				{
					num = 10;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("䠺丼", a_), RecordTableEnumerator.b("爺匼䬾⑀ㅂⱄ⡆㭈", a_), null);
				num = 3;
				continue;
			case 12:
			{
				string value3 = this.ᜀ(A_1.ᜰ());
				A_0.WriteAttributeString(RecordTableEnumerator.b("䠺丼", a_), RecordTableEnumerator.b("砺刼匾⹀ㅂ", a_), null, value3);
				num = 9;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 7;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_223;
			default:
				if (false)
				{
				}
				num = 11;
				continue;
			}
			IL_124:
			num = 6;
			continue;
			IL_15B:
			num = 0;
		}
		IL_5B:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰺似嘾㕀♂㝄", a_));
		IL_11F:
		throw new ArgumentNullException(RecordTableEnumerator.b("崺刼䴾ⱀ≂ㅄ", a_));
		IL_1BA:
		IL_223:
		A_0.WriteEndElement();
	}

	// Token: 0x0600548A RID: 21642 RVA: 0x0034C370 File Offset: 0x0034B370
	private void ᜀ(XmlWriter A_0, IBorders A_1)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			int num = 7;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					goto IL_D2;
				case 1:
				{
					IBorder border;
					if (border != null)
					{
						num = 4;
						continue;
					}
					goto IL_107;
				}
				case 2:
					if (A_1 == null)
					{
						num = 9;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E2;
					default:
					{
						if (false)
						{
						}
						A_0.WriteStartElement(RecordTableEnumerator.b("䨸䠺", a_), RecordTableEnumerator.b("笸吺似嬾⑀ㅂ㙄", a_), null);
						BordersLineType[] array = new BordersLineType[]
						{
							BordersLineType.DiagonalDown,
							BordersLineType.DiagonalUp,
							BordersLineType.EdgeBottom,
							BordersLineType.EdgeLeft,
							BordersLineType.EdgeRight,
							BordersLineType.EdgeTop
						};
						num2 = 0;
						int num3 = array.Length;
						num = 5;
						continue;
					}
					}
					break;
				case 3:
					goto IL_61;
				case 4:
				{
					BordersLineType bordersLineType;
					int a_2 = (int)bordersLineType;
					IBorder border;
					this.ᜀ(A_0, border, a_2);
					num = 10;
					continue;
				}
				case 5:
					goto IL_D2;
				case 6:
				{
					int num3;
					if (num2 >= num3)
					{
						goto IL_E2;
					}
					BordersLineType[] array;
					BordersLineType bordersLineType = array[num2];
					IBorder border = A_1[bordersLineType];
					num = 1;
					continue;
				}
				case 8:
					goto IL_EE;
				case 9:
					goto IL_CD;
				case 10:
					goto IL_107;
				}
				if (A_0 == null)
				{
					num = 3;
					continue;
				}
				if (true)
				{
				}
				num = 2;
				continue;
				IL_D2:
				num = 6;
				continue;
				IL_E2:
				num = 8;
				continue;
				IL_107:
				num2++;
				num = 0;
			}
			IL_61:
			throw new ArgumentNullException(RecordTableEnumerator.b("丸䤺吼䬾⑀ㅂ", a_));
			IL_CD:
			throw new ArgumentNullException(RecordTableEnumerator.b("嬸吺似嬾⑀ㅂ㙄", a_));
			IL_EE:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x0600548B RID: 21643 RVA: 0x0034C53C File Offset: 0x0034B53C
	private void ᜀ(XmlWriter A_0, IBorder A_1, int A_2)
	{
		int a_ = 19;
		for (;;)
		{
			IL_09:
			switch (0)
			{
			default:
			{
				int num = 5;
				for (;;)
				{
					if (true)
					{
					}
					string text;
					switch (num)
					{
					case 0:
					{
						string text2;
						text = text2;
						goto IL_AF;
					}
					case 1:
						if (A_2 != 5)
						{
							num = 17;
							continue;
						}
						goto IL_199;
					case 2:
					{
						string text2;
						int num2;
						text = text2.Substring(num2 + 1);
						goto IL_AF;
					}
					case 3:
					{
						string text2;
						int num2;
						A_0.WriteAttributeString(RecordTableEnumerator.b("㩈㡊", a_), RecordTableEnumerator.b("Ṉ⹊⑌⡎㥐❒", a_), null, text2.Substring(0, num2));
						num = 16;
						continue;
					}
					case 4:
						if (A_1.LineStyle != LineStyleType.None)
						{
							num = 12;
							continue;
						}
						goto IL_2FC;
					case 6:
						if (A_1 == null)
						{
							num = 18;
							continue;
						}
						num = 1;
						continue;
					case 7:
						if (!A_1.ShowDiagonalLine)
						{
							num = 10;
							continue;
						}
						goto IL_1F9;
					case 8:
						goto IL_199;
					case 9:
						goto IL_89;
					case 10:
						return;
					case 11:
						if (A_2 == 6)
						{
							num = 8;
							continue;
						}
						goto IL_1F9;
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
							string text2 = sprỉ.\u17EC[(int)A_1.LineStyle];
							int num2 = text2.IndexOf(RecordTableEnumerator.b("楈", a_));
							num = 15;
							continue;
						}
						}
						break;
					case 13:
					{
						int num2;
						if (num2 != -1)
						{
							num = 3;
							continue;
						}
						goto IL_2FC;
					}
					case 14:
						num = 0;
						continue;
					case 15:
					{
						int num2;
						if (num2 == -1)
						{
							num = 14;
							continue;
						}
						num = 2;
						continue;
					}
					case 16:
						goto IL_194;
					case 17:
						num = 11;
						continue;
					case 18:
						goto IL_115;
					}
					if (A_0 == null)
					{
						num = 9;
						continue;
					}
					num = 6;
					continue;
					IL_AF:
					string value = text;
					A_0.WriteAttributeString(RecordTableEnumerator.b("㩈㡊", a_), RecordTableEnumerator.b("Ո≊⍌⩎ɐ❒ⱔ㭖㱘", a_), null, value);
					num = 13;
					continue;
					IL_199:
					num = 7;
					continue;
					IL_1F9:
					A_0.WriteStartElement(RecordTableEnumerator.b("㩈㡊", a_), RecordTableEnumerator.b("ୈ⑊㽌⭎㑐⅒", a_), null);
					string value2 = sprỉ.\u17EB[A_2];
					A_0.WriteAttributeString(RecordTableEnumerator.b("㩈㡊", a_), RecordTableEnumerator.b("᥈⑊㹌♎═㩒㩔㥖", a_), null, value2);
					string value3 = this.ᜀ(A_1.Color);
					A_0.WriteAttributeString(RecordTableEnumerator.b("㩈㡊", a_), RecordTableEnumerator.b("ੈ⑊⅌⁎⍐", a_), null, value3);
					num = 4;
				}
				break;
			}
			}
		}
		IL_89:
		throw new ArgumentNullException(RecordTableEnumerator.b("㹈㥊⑌㭎㑐⅒", a_));
		IL_115:
		throw new ArgumentNullException(RecordTableEnumerator.b("⭈⑊㽌⭎㑐⅒", a_));
		IL_194:
		IL_2FC:
		A_0.WriteEndElement();
	}

	// Token: 0x0600548C RID: 21644 RVA: 0x0034C84C File Offset: 0x0034B84C
	private void ᜀ(XmlWriter A_0, IAutoFilters A_1)
	{
		int a_ = 8;
		int num = 9;
		for (;;)
		{
			int num2;
			IAutoFilter autoFilter;
			switch (num)
			{
			case 0:
				this.ᜀ(A_0, A_1[num2]);
				num = 2;
				continue;
			case 1:
				if (autoFilter.IsFiltered)
				{
					num = 0;
					continue;
				}
				goto IL_F0;
			case 2:
				goto IL_F0;
			case 3:
				goto IL_50;
			case 4:
			{
				int count;
				if (num2 >= count)
				{
					num = 7;
					continue;
				}
				goto IL_70;
			}
			case 5:
				goto IL_B8;
			case 6:
				if (A_1 == null)
				{
					num = 5;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_70;
				default:
				{
					if (true)
					{
					}
					if (false)
					{
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("䘽", a_), RecordTableEnumerator.b("缽㔿㙁⭃Eⅇ♉㡋⭍≏", a_), null);
					A_0.WriteAttributeString(RecordTableEnumerator.b("䘽", a_), RecordTableEnumerator.b("氽ℿⱁ⍃⍅", a_), null, ((XlsAutoFiltersCollection)A_1).AddressR1C1);
					num2 = 0;
					int count = A_1.Count;
					num = 10;
					continue;
				}
				}
				break;
			case 7:
				goto IL_D7;
			case 8:
				goto IL_BD;
			case 10:
				goto IL_BD;
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			num = 6;
			continue;
			IL_70:
			autoFilter = A_1[num2];
			num = 1;
			continue;
			IL_BD:
			num = 4;
			continue;
			IL_F0:
			num2++;
			num = 8;
		}
		IL_50:
		throw new ArgumentNullException(RecordTableEnumerator.b("䤽㈿⭁ぃ⍅㩇", a_));
		IL_B8:
		throw new ArgumentNullException(RecordTableEnumerator.b("弽㔿㙁⭃⁅ⅇ♉㡋⭍≏⅑", a_));
		IL_D7:
		A_0.WriteEndElement();
	}

	// Token: 0x0600548D RID: 21645 RVA: 0x0034CA04 File Offset: 0x0034BA04
	private void ᜀ(XmlWriter A_0, IAutoFilter A_1)
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			int num = 9;
			for (;;)
			{
				string text;
				XlsAutoFilter xlsAutoFilter;
				string text2;
				string text3;
				switch (num)
				{
				case 0:
					num = 17;
					continue;
				case 1:
					goto IL_4B0;
				case 2:
					text = RecordTableEnumerator.b("縻儽㐿㙁⭃⭅", a_);
					goto IL_2BA;
				case 3:
					goto IL_2B5;
				case 4:
					if (!xlsAutoFilter.HasFirstCondition)
					{
						goto IL_3BC;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_175;
					default:
						if (false)
						{
						}
						num = 8;
						continue;
					}
					break;
				case 5:
					if (xlsAutoFilter.IsTop10Items)
					{
						num = 12;
						continue;
					}
					goto IL_215;
				case 6:
					text2 = RecordTableEnumerator.b("紻䬽㐿ⵁɃ⽅⑇㹉⥋㱍ᅏ㱑こ", a_);
					goto IL_27F;
				case 7:
					goto IL_426;
				case 8:
					if (true)
					{
					}
					A_0.WriteAttributeString(RecordTableEnumerator.b("䐻", a_), RecordTableEnumerator.b("栻䜽〿❁", a_), null, RecordTableEnumerator.b("缻䬽㌿㙁⭃⭅", a_));
					this.ᜀ(A_0, xlsAutoFilter.FirstCondition);
					num = 25;
					continue;
				case 10:
					num = 2;
					continue;
				case 11:
					text = RecordTableEnumerator.b("栻儽〿", a_);
					goto IL_2BA;
				case 12:
					num = 21;
					continue;
				case 13:
					A_0.WriteAttributeString(RecordTableEnumerator.b("䐻", a_), RecordTableEnumerator.b("栻䜽〿❁", a_), null, RecordTableEnumerator.b("縻刽ℿⱁ⽃㕅", a_));
					num = 1;
					continue;
				case 14:
					A_0.WriteAttributeString(RecordTableEnumerator.b("䐻", a_), RecordTableEnumerator.b("栻䜽〿❁", a_), null, RecordTableEnumerator.b("爻儽⸿A⡃❅♇ⅉ㽋", a_));
					num = 28;
					continue;
				case 15:
					goto IL_175;
				case 16:
					if (xlsAutoFilter.IsNonBlanks)
					{
						num = 14;
						continue;
					}
					goto IL_BB;
				case 17:
					text2 = RecordTableEnumerator.b("紻䬽㐿ⵁɃ⽅⑇㹉⥋㱍὏⁑", a_);
					goto IL_27F;
				case 18:
					if (xlsAutoFilter.IsBlanks)
					{
						num = 13;
						continue;
					}
					goto IL_4B0;
				case 19:
					if (A_1 == null)
					{
						num = 29;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("䐻", a_), RecordTableEnumerator.b("紻䬽㐿ⵁɃ⽅⑇㹉⥋㱍ፏ㵑㡓⍕㕗㑙", a_), null);
					xlsAutoFilter = (XlsAutoFilter)A_1;
					A_0.WriteAttributeString(RecordTableEnumerator.b("䐻", a_), RecordTableEnumerator.b("画倽␿❁㱃", a_), null, xlsAutoFilter.Index.ToString());
					num = 5;
					continue;
				case 20:
					goto IL_215;
				case 21:
					if (!xlsAutoFilter.ShowTopItem)
					{
						num = 10;
						continue;
					}
					num = 11;
					continue;
				case 22:
					if (xlsAutoFilter.IsTop10Percent)
					{
						num = 24;
						continue;
					}
					goto IL_426;
				case 23:
					if (xlsAutoFilter.HasSecondCondition)
					{
						num = 26;
						continue;
					}
					goto IL_4D8;
				case 24:
					text3 += RecordTableEnumerator.b("氻嬽㈿⅁⅃⡅㱇", a_);
					num = 7;
					continue;
				case 25:
					goto IL_3BC;
				case 26:
					num = 15;
					continue;
				case 27:
					goto IL_B6;
				case 28:
					goto IL_BB;
				case 29:
					goto IL_4AB;
				}
				if (A_0 == null)
				{
					num = 27;
					continue;
				}
				num = 19;
				continue;
				IL_BB:
				num = 4;
				continue;
				IL_175:
				if (!xlsAutoFilter.IsAnd)
				{
					num = 0;
					continue;
				}
				num = 6;
				continue;
				IL_215:
				num = 18;
				continue;
				IL_27F:
				string localName = text2;
				A_0.WriteStartElement(RecordTableEnumerator.b("䐻", a_), localName, null);
				this.ᜀ(A_0, xlsAutoFilter.SecondCondition);
				A_0.WriteEndElement();
				num = 3;
				continue;
				IL_2BA:
				text3 = text;
				num = 22;
				continue;
				IL_3BC:
				num = 23;
				continue;
				IL_426:
				A_0.WriteAttributeString(RecordTableEnumerator.b("䐻", a_), RecordTableEnumerator.b("樻弽ⰿ㝁⅃", a_), null, xlsAutoFilter.Top10Items.ToString());
				A_0.WriteAttributeString(RecordTableEnumerator.b("䐻", a_), RecordTableEnumerator.b("栻䜽〿❁", a_), null, text3);
				num = 20;
				continue;
				IL_4B0:
				num = 16;
			}
			IL_B6:
			throw new ArgumentNullException(RecordTableEnumerator.b("䬻䰽⤿㙁⅃㑅", a_));
			IL_2B5:
			goto IL_4D8;
			IL_4AB:
			throw new ArgumentNullException(RecordTableEnumerator.b("崻䬽㐿ⵁ≃⽅⑇㹉⥋㱍", a_));
			IL_4D8:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x0600548E RID: 21646 RVA: 0x0034CEF0 File Offset: 0x0034BEF0
	private void ᜀ(XmlWriter A_0, IAutoFilterCondition A_1)
	{
		int a_ = 11;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_67;
			case 1:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				goto IL_99;
			case 2:
				goto IL_3E;
			}
			if (A_0 == null)
			{
				num = 2;
			}
			else
			{
				num = 1;
			}
		}
		IL_3E:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙀ㅂⱄ㍆ⱈ㥊", a_));
		IL_67:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_3E;
		default:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("≀ⱂ⭄⍆⁈㽊⑌⁎㽐", a_));
		}
		IL_99:
		if (true)
		{
		}
		A_0.WriteStartElement(RecordTableEnumerator.b("㥀", a_), RecordTableEnumerator.b("@㙂ㅄ⡆཈≊⅌㭎㑐⅒ᙔ㡖㝘㽚㑜⭞ࡠౢ୤", a_), null);
		string value = this.\u17F1[(int)A_1.ConditionOperator];
		string value2 = this.ᜀ(A_1);
		A_0.WriteAttributeString(RecordTableEnumerator.b("㥀", a_), RecordTableEnumerator.b("เ㍂⁄㕆⡈㽊≌㵎", a_), null, value);
		A_0.WriteAttributeString(RecordTableEnumerator.b("㥀", a_), RecordTableEnumerator.b("ᝀ≂⥄㉆ⱈ", a_), null, value2);
		A_0.WriteEndElement();
	}

	// Token: 0x0600548F RID: 21647 RVA: 0x0034D028 File Offset: 0x0034C028
	private void ᜀ(XmlWriter A_0, XlsWorksheet A_1, int A_2)
	{
		int a_ = 1;
		switch (0)
		{
		default:
		{
			int num = 5;
			for (;;)
			{
				bool flag;
				XlsCellRecordCollection cellRecords;
				long num2;
				int num3;
				bool flag3;
				bool flag2;
				bool flag4;
				int num4;
				XlsHyperLinksCollection a_2;
				spr\u1FBC spr_u1FBC;
				switch (num)
				{
				case 0:
					num = 27;
					continue;
				case 1:
					num = 7;
					continue;
				case 2:
					if (!flag)
					{
						goto IL_3A8;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_49D;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 3:
					goto IL_C2;
				case 4:
					goto IL_FB;
				case 6:
					this.ᜀ(A_0, cellRecords, num2);
					num = 28;
					continue;
				case 7:
					if (cellRecords.ᜄ(num2).get_TypeCode() != TBIFFRecord.Blank)
					{
						num = 6;
						continue;
					}
					goto IL_3A8;
				case 8:
					A_0.WriteAttributeString(RecordTableEnumerator.b("䐶䨸", a_), RecordTableEnumerator.b("縶圸强堼䜾", a_), null, num3.ToString());
					num = 25;
					continue;
				case 9:
					flag2 = flag3;
					goto IL_3E4;
				case 10:
					if (flag3)
					{
						num = 4;
						continue;
					}
					goto IL_27D;
				case 11:
					if (!flag)
					{
						num = 0;
						continue;
					}
					goto IL_FB;
				case 12:
					if (flag4)
					{
						num = 21;
						continue;
					}
					goto IL_2D9;
				case 13:
					num = 9;
					continue;
				case 14:
					flag2 = false;
					goto IL_3E4;
				case 15:
					goto IL_2F6;
				case 16:
					goto IL_49D;
				case 17:
					num = 20;
					continue;
				case 18:
					flag2 = true;
					goto IL_3E4;
				case 19:
					num = 10;
					continue;
				case 20:
				{
					spr\u25A6.ᜀ ᜀ;
					if (ᜀ != null)
					{
						num = 13;
						continue;
					}
					num = 18;
					continue;
				}
				case 21:
					this.ᜀ(A_0, A_1.Comments[A_2, num3], A_1.ParentWorkbook.InnerFonts, cellRecords.GetCellFont(num2));
					num = 24;
					continue;
				case 22:
					goto IL_27D;
				case 23:
					if (cellRecords.Contains(num2))
					{
						num = 17;
						continue;
					}
					num = 14;
					continue;
				case 24:
					goto IL_2D9;
				case 25:
					goto IL_20D;
				case 26:
					goto IL_2F6;
				case 27:
					if (!flag4)
					{
						num = 19;
						continue;
					}
					goto IL_FB;
				case 28:
					goto IL_3A8;
				case 29:
					return;
				case 30:
					if (num4 + 1 != num3)
					{
						num = 8;
						continue;
					}
					goto IL_20D;
				case 31:
				{
					if (A_1 == null)
					{
						num = 16;
						continue;
					}
					cellRecords = A_1.CellRecords;
					a_2 = (XlsHyperLinksCollection)A_1.HyperLinks;
					spr_u1FBC = A_1.MergeCells;
					num4 = 0;
					num3 = A_1.FirstColumn;
					int lastColumn = A_1.LastColumn;
					num = 15;
					continue;
				}
				case 32:
				{
					int lastColumn;
					if (num3 > lastColumn)
					{
						num = 29;
						continue;
					}
					num2 = sprṔ.ᜀ(num3, A_2);
					Rectangle rectangle = spr_u1FBC.ᜁ(new Rectangle(num3 - 1, A_2 - 1, 0, 0));
					long num5 = sprṔ.ᜀ(rectangle.X + 1, rectangle.Y + 1);
					flag3 = (num5 == num2);
					spr\u25A6.ᜀ ᜀ = spr_u1FBC.ᜃ(new Rectangle(num3 - 1, A_2 - 1, 0, 0));
					num = 23;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 3;
					continue;
				}
				num = 31;
				continue;
				IL_FB:
				if (true)
				{
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("琶尸场儼", a_), null);
				num = 30;
				continue;
				IL_20D:
				num4 = num3;
				bool a_3 = this.ᜀ(A_0);
				this.ᜀ(A_0, num2, a_2);
				this.ᜀ(A_0, num2, flag3, cellRecords, A_1);
				this.ᜀ(A_0, A_2, num3, spr_u1FBC, flag3);
				num = 2;
				continue;
				IL_27D:
				num3++;
				num = 26;
				continue;
				IL_2D9:
				A_0.WriteEndElement();
				this.ᜀ(A_0, a_3);
				num = 22;
				continue;
				IL_2F6:
				num = 32;
				continue;
				IL_3A8:
				num = 12;
				continue;
				IL_3E4:
				flag = flag2;
				flag4 = (A_1.Comments[A_2, num3] != null);
				num = 11;
			}
			IL_C2:
			throw new ArgumentNullException(RecordTableEnumerator.b("䀶䬸刺䤼娾㍀", a_));
			IL_49D:
			throw new ArgumentNullException(RecordTableEnumerator.b("䐶儸帺堼䬾", a_));
		}
		}
	}

	// Token: 0x06005490 RID: 21648 RVA: 0x0034D4D8 File Offset: 0x0034C4D8
	private void ᜀ(XmlWriter A_0, int A_1, int A_2, spr\u1FBC A_3, bool A_4)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				Rectangle a_ = Rectangle.FromLTRB(A_2 - 1, A_1 - 1, A_2 - 1, A_1 - 1);
				this.ᜀ(A_0, A_3.ᜂ(a_));
				if (true)
				{
				}
				num = 2;
				continue;
			}
			case 2:
				goto IL_5B;
			}
			if (!A_4)
			{
				break;
			}
			num = 0;
		}
		IL_5B:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_5B;
		default:
			if (false)
			{
			}
			return;
		}
	}

	// Token: 0x06005491 RID: 21649 RVA: 0x0034D568 File Offset: 0x0034C568
	private void ᜀ(XmlWriter A_0, long A_1, XlsHyperLinksCollection A_2)
	{
		int a_ = 7;
		IHyperLink hyperlinkByCellIndex;
		string text;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_12D:
			text = hyperlinkByCellIndex.Address;
			goto IL_135;
		default:
			if (false)
			{
			}
			goto IL_59;
		}
		int num;
		for (;;)
		{
			IL_27:
			switch (num)
			{
			case 0:
				num = 4;
				continue;
			case 1:
				if (hyperlinkByCellIndex != null)
				{
					num = 3;
					continue;
				}
				return;
			case 2:
				A_0.WriteAttributeString(RecordTableEnumerator.b("䔼", a_), RecordTableEnumerator.b("甼派⑀╂ᙄ⑆㭈⹊⡌ⅎՐ㩒╔", a_), null, hyperlinkByCellIndex.ScreenTip);
				num = 5;
				continue;
			case 3:
				num = 7;
				continue;
			case 4:
				if (hyperlinkByCellIndex.ScreenTip.Length != 0)
				{
					num = 2;
					continue;
				}
				return;
			case 5:
				return;
			case 6:
				goto IL_12D;
			case 7:
				if (hyperlinkByCellIndex.Type != HyperLinkType.Workbook)
				{
					num = 9;
					continue;
				}
				num = 10;
				continue;
			case 8:
				if (hyperlinkByCellIndex.ScreenTip != null)
				{
					num = 0;
					continue;
				}
				return;
			case 9:
				num = 6;
				continue;
			case 10:
				goto IL_B0;
			}
			goto IL_59;
		}
		IL_B0:
		text = RecordTableEnumerator.b("Ḽ", a_) + hyperlinkByCellIndex.Address;
		goto IL_135;
		IL_59:
		hyperlinkByCellIndex = A_2.GetHyperlinkByCellIndex(A_1);
		num = 1;
		goto IL_27;
		IL_135:
		string value = text;
		A_0.WriteAttributeString(RecordTableEnumerator.b("丼䰾", a_), RecordTableEnumerator.b("甼派⑀╂", a_), null, value);
		if (true)
		{
		}
		num = 8;
		goto IL_27;
	}

	// Token: 0x06005492 RID: 21650 RVA: 0x0034D6FC File Offset: 0x0034C6FC
	private void ᜀ(XmlWriter A_0, long A_1, bool A_2, XlsCellRecordCollection A_3, XlsWorksheet A_4)
	{
		int a_ = 11;
		XlsWorkbook parentWorkbook;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_132:
			parentWorkbook = A_4.ParentWorkbook;
			num = 3;
			break;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			goto IL_5D;
		}
		int num2;
		for (;;)
		{
			IL_2F:
			switch (num)
			{
			case 0:
				if (num2 != -2147483648)
				{
					num = 7;
					continue;
				}
				return;
			case 1:
				num = 9;
				continue;
			case 2:
				goto IL_E6;
			case 3:
				if (num2 != parentWorkbook.DefaultXFIndex)
				{
					num = 1;
					continue;
				}
				return;
			case 4:
				if (A_2)
				{
					num = 8;
					continue;
				}
				goto IL_132;
			case 5:
				return;
			case 6:
				num = 0;
				continue;
			case 7:
				A_0.WriteAttributeString(RecordTableEnumerator.b("㉀あ", a_), RecordTableEnumerator.b("ቀ㝂㱄⭆ⱈɊौ", a_), null, RecordTableEnumerator.b("㉀", a_) + num2.ToString());
				num = 5;
				continue;
			case 8:
			{
				long key = sprỉ.ᜀ(A_4.Index, A_1);
				num2 = this.\u17F2[key];
				num = 2;
				continue;
			}
			case 9:
				if (num2 != 0)
				{
					num = 6;
					continue;
				}
				return;
			}
			goto IL_5D;
		}
		IL_E6:
		goto IL_132;
		IL_5D:
		num2 = A_3.GetExtendedFormatIndex(A_1);
		num = 4;
		goto IL_2F;
	}

	// Token: 0x06005493 RID: 21651 RVA: 0x0034D868 File Offset: 0x0034C868
	private void ᜀ(XmlWriter A_0, bool A_1)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				XmlTextWriter xmlTextWriter;
				if (xmlTextWriter == null)
				{
					return;
				}
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
					num = 4;
					continue;
				}
				break;
			}
			case 1:
			{
				XmlTextWriter xmlTextWriter = A_0 as XmlTextWriter;
				num = 0;
				continue;
			}
			case 3:
				return;
			case 4:
			{
				XmlTextWriter xmlTextWriter;
				xmlTextWriter.Formatting = Formatting.Indented;
				num = 3;
				continue;
			}
			}
			if (!A_1)
			{
				break;
			}
			num = 1;
		}
	}

	// Token: 0x06005494 RID: 21652 RVA: 0x0034D904 File Offset: 0x0034C904
	private bool ᜀ(XmlWriter A_0)
	{
		bool result;
		for (;;)
		{
			IL_30:
			result = false;
			XmlTextWriter xmlTextWriter = A_0 as XmlTextWriter;
			int num = 2;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_76;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_6A;
					case 1:
						result = (xmlTextWriter.Formatting != Formatting.None);
						xmlTextWriter.Formatting = Formatting.None;
						num = 0;
						continue;
					case 2:
						if (xmlTextWriter != null)
						{
							num = 1;
							continue;
						}
						goto IL_76;
					}
					goto IL_30;
				}
			}
		}
		IL_6A:
		IL_76:
		if (true)
		{
		}
		return result;
	}

	// Token: 0x06005495 RID: 21653 RVA: 0x0034D990 File Offset: 0x0034C990
	private void ᜀ(XmlWriter A_0, XlsCellRecordCollection A_1, long A_2)
	{
		int a_ = 2;
		switch (0)
		{
		default:
		{
			int num = 5;
			for (;;)
			{
				string a_2;
				XmlSerializationCellType xmlSerializationCellType;
				IStyle a_3;
				spr\u223A a_4;
				switch (num)
				{
				case 0:
					return;
				case 1:
				{
					string text = this.ᜀ(text);
					A_0.WriteAttributeString(RecordTableEnumerator.b("䬷䤹", a_), RecordTableEnumerator.b("縷唹主匽㔿⹁╃", a_), null, text);
					XlsWorksheet xlsWorksheet;
					xmlSerializationCellType = this.ᜀ(xlsWorksheet, A_2, out a_2);
					num = 4;
					continue;
				}
				case 2:
					num = 14;
					continue;
				case 3:
				{
					string text;
					if (text != null)
					{
						num = 2;
						continue;
					}
					goto IL_170;
				}
				case 4:
					goto IL_129;
				case 6:
					if (xmlSerializationCellType == XmlSerializationCellType.String)
					{
						num = 11;
						continue;
					}
					goto IL_129;
				case 7:
					goto IL_124;
				case 8:
				{
					if (A_1 == null)
					{
						num = 7;
						continue;
					}
					NumberFormatInfo numberFormat = CultureInfo.InvariantCulture.NumberFormat;
					string text = A_1.GetFormula(A_2, true, numberFormat);
					a_3 = null;
					a_4 = null;
					XlsWorksheet xlsWorksheet = (XlsWorksheet)A_1.sheet;
					num = 3;
					continue;
				}
				case 9:
				{
					string text;
					if (text != RecordTableEnumerator.b("Է᤹渻笽ؿ捁", a_))
					{
						num = 13;
						continue;
					}
					return;
				}
				case 10:
					goto IL_129;
				case 11:
				{
					a_3 = A_1.GetCellStyle(A_2);
					XlsWorksheet xlsWorksheet;
					a_4 = xlsWorksheet.ᜂ(A_2);
					num = 10;
					continue;
				}
				case 12:
					goto IL_9A;
				case 13:
					goto IL_15A;
				case 14:
				{
					string text;
					if (text.Length > 0)
					{
						num = 1;
						continue;
					}
					goto IL_170;
				}
				}
				if (A_0 != null)
				{
					num = 8;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_15A;
				default:
					if (false)
					{
					}
					num = 12;
					continue;
				}
				IL_129:
				num = 9;
				continue;
				IL_15A:
				this.ᜀ(A_0, xmlSerializationCellType, a_2, a_3, a_4, A_1, A_2);
				num = 0;
				continue;
				IL_170:
				a_2 = this.ᜀ(A_1, A_2, out xmlSerializationCellType);
				num = 6;
			}
			IL_9A:
			throw new ArgumentNullException(RecordTableEnumerator.b("伷䠹唻䨽┿ぁ", a_));
			IL_124:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("嬷弹倻刽㌿", a_));
		}
		}
	}

	// Token: 0x06005496 RID: 21654 RVA: 0x0034DBE8 File Offset: 0x0034CBE8
	private XmlSerializationCellType ᜀ(IWorksheet A_0, long A_1, out string A_2)
	{
		int a_ = 13;
		switch (0)
		{
		default:
		{
			XmlSerializationCellType result;
			for (;;)
			{
				int row = sprṔ.ᜁ(A_1);
				int column = sprṔ.ᜀ(A_1);
				IXLSRange ixlsrange = A_0[row, column];
				int num = 5;
				for (;;)
				{
					string formulaStringValue;
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D2;
						default:
							if (false)
							{
							}
							result = XmlSerializationCellType.String;
							num = 11;
							continue;
						}
						break;
					case 1:
						result = XmlSerializationCellType.DateTime;
						A_2 = XmlConvert.ToString(ixlsrange.FormulaDateTime, RecordTableEnumerator.b("㩂㱄㹆え晊LɎ籐㝒ㅔ͖ᅘፚ杜㉞ౠ奢ᙤᑦ", a_));
						num = 10;
						continue;
					case 2:
						return result;
					case 3:
						if (ixlsrange.HasFormulaErrorValue)
						{
							num = 8;
							continue;
						}
						if (true)
						{
						}
						A_2 = (formulaStringValue = ixlsrange.FormulaStringValue);
						num = 4;
						continue;
					case 4:
						goto IL_D2;
					case 5:
						if (ixlsrange.HasFormulaBoolValue)
						{
							num = 13;
							continue;
						}
						num = 7;
						continue;
					case 6:
						return result;
					case 7:
						if (ixlsrange.HasFormulaDateTime)
						{
							num = 1;
							continue;
						}
						num = 3;
						continue;
					case 8:
						result = XmlSerializationCellType.Error;
						A_2 = ixlsrange.FormulaErrorValue;
						num = 6;
						continue;
					case 9:
						return result;
					case 10:
						return result;
					case 11:
						return result;
					case 12:
					{
						double formulaNumberValue;
						A_2 = ((!double.IsNaN(formulaNumberValue)) ? XmlConvert.ToString(formulaNumberValue) : null);
						num = 9;
						continue;
					}
					case 13:
						result = XmlSerializationCellType.Boolean;
						A_2 = XmlConvert.ToString(ixlsrange.FormulaBoolValue);
						num = 2;
						continue;
					}
					break;
					IL_D2:
					if (formulaStringValue != null)
					{
						num = 0;
					}
					else
					{
						result = XmlSerializationCellType.Number;
						double formulaNumberValue = ixlsrange.FormulaNumberValue;
						num = 12;
					}
				}
			}
			return result;
		}
		}
	}

	// Token: 0x06005497 RID: 21655 RVA: 0x0034DDE4 File Offset: 0x0034CDE4
	private void ᜀ(XmlWriter A_0, XmlSerializationCellType A_1, string A_2, IStyle A_3, spr\u223A A_4, XlsCellRecordCollection A_5, long A_6)
	{
		int a_ = 10;
		switch (0)
		{
		default:
		{
			int num = 15;
			for (;;)
			{
				bool flag;
				string text;
				switch (num)
				{
				case 0:
					goto IL_247;
				case 1:
					if (A_3 != null)
					{
						num = 12;
						continue;
					}
					goto IL_1E5;
				case 2:
					num = 25;
					continue;
				case 3:
				{
					XlsWorksheet xlsWorksheet = (XlsWorksheet)A_5.sheet;
					A_0.WriteAttributeString(RecordTableEnumerator.b("㠿⽁⡃⡅㭇", a_), null, null, RecordTableEnumerator.b("⠿㙁ぃ㙅片敉捋㥍❏║穓⅕歗瑙㍛ⱝݟ䵡っ㑥䝧㡩⥫⵭嵯ᩱs᭵ᑷ乹䱻", a_));
					IFont cellFont = A_5.GetCellFont(A_6);
					XlsFontsCollection innerFonts = xlsWorksheet.ParentWorkbook.InnerFonts;
					this.ᜀ(A_0, A_4, A_2, innerFonts, cellFont);
					flag = true;
					num = 5;
					continue;
				}
				case 4:
					text = RecordTableEnumerator.b("瀿", a_);
					goto IL_1D5;
				case 5:
					goto IL_1B2;
				case 6:
					num = 1;
					continue;
				case 7:
					if (!XmlConvert.ToBoolean(A_2))
					{
						num = 21;
						continue;
					}
					num = 19;
					continue;
				case 8:
					num = 7;
					continue;
				case 9:
					if (!flag)
					{
						num = 14;
						continue;
					}
					goto IL_3C9;
				case 10:
					if (A_1 == XmlSerializationCellType.String)
					{
						num = 11;
						continue;
					}
					goto IL_108;
				case 11:
					if (true)
					{
					}
					num = 26;
					continue;
				case 12:
					num = 23;
					continue;
				case 13:
					if (A_1 == XmlSerializationCellType.Boolean)
					{
						num = 8;
						continue;
					}
					goto IL_247;
				case 14:
					num = 13;
					continue;
				case 16:
					if (A_4.ᜆ() != 0)
					{
						num = 3;
						continue;
					}
					goto IL_1B2;
				case 17:
					if (A_4 != null)
					{
						num = 22;
						continue;
					}
					goto IL_1B2;
				case 18:
					goto IL_1AD;
				case 19:
					text = RecordTableEnumerator.b("焿", a_);
					goto IL_1D5;
				case 20:
					A_0.WriteAttributeString(RecordTableEnumerator.b("㠿", a_), RecordTableEnumerator.b("ᐿ⭁❃ⵅⵇ⹉", a_), null, RecordTableEnumerator.b("焿", a_));
					num = 29;
					continue;
				case 21:
					goto IL_22B;
				case 22:
					num = 16;
					continue;
				case 23:
					if (A_3.IsFirstSymbolApostrophe)
					{
						num = 20;
						continue;
					}
					goto IL_1E5;
				case 24:
					goto IL_25A;
				case 25:
					if (A_2.Length != 0)
					{
						num = 6;
						continue;
					}
					goto IL_1B2;
				case 26:
					if (A_2.Length == 0)
					{
						num = 18;
						continue;
					}
					goto IL_108;
				case 27:
					if (A_1 == XmlSerializationCellType.String)
					{
						num = 2;
						continue;
					}
					goto IL_1B2;
				case 28:
					num = 10;
					continue;
				case 29:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_22B;
					default:
						if (false)
						{
						}
						goto IL_1E5;
					}
					break;
				}
				if (A_2 != null)
				{
					num = 28;
					continue;
				}
				break;
				IL_108:
				A_0.WriteStartElement(RecordTableEnumerator.b("п⍁ぃ❅", a_), null);
				flag = false;
				A_0.WriteAttributeString(RecordTableEnumerator.b("㌿ㅁ", a_), RecordTableEnumerator.b("ᐿ㭁㑃⍅", a_), null, A_1.ToString());
				num = 27;
				continue;
				IL_1B2:
				num = 9;
				continue;
				IL_1D5:
				A_2 = text;
				num = 0;
				continue;
				IL_1E5:
				num = 17;
				continue;
				IL_22B:
				num = 4;
				continue;
				IL_247:
				A_0.WriteString(A_2);
				num = 24;
			}
			IL_1AD:
			return;
			IL_25A:
			IL_3C9:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06005498 RID: 21656 RVA: 0x0034E1C0 File Offset: 0x0034D1C0
	private void ᜀ(XmlWriter A_0, IWorksheets A_1)
	{
		int a_ = 7;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				int num2;
				int count;
				if (num2 >= count)
				{
					num = 4;
					continue;
				}
				this.ᜇ(A_0, (XlsWorksheet)A_1[num2]);
				num2++;
				num = 6;
				continue;
			}
			case 2:
				goto IL_ED;
			case 3:
			{
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				int num2 = 0;
				int count = A_1.Count;
				num = 5;
				continue;
			}
			case 4:
				goto IL_D2;
			case 5:
				goto IL_B8;
			case 6:
				goto IL_B8;
			case 7:
				goto IL_51;
			}
			if (A_0 == null)
			{
				num = 7;
				continue;
			}
			num = 3;
			continue;
			IL_B8:
			num = 0;
		}
		IL_51:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return;
		default:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䨼䴾⡀㝂⁄㕆", a_));
		}
		IL_D2:
		return;
		IL_ED:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䨼倾㍀⡂㙄⽆ⱈ⹊㥌㱎", a_));
	}

	// Token: 0x06005499 RID: 21657 RVA: 0x0034E2E0 File Offset: 0x0034D2E0
	private void ᜇ(XmlWriter A_0, XlsWorksheet A_1)
	{
		int a_ = 18;
		int num = 5;
		for (;;)
		{
			XlsWorksheetConditionalFormats conditionalFormats;
			IAutoFilters autoFilters;
			INameRanges names;
			switch (num)
			{
			case 0:
				goto IL_97;
			case 1:
				goto IL_64;
			case 2:
				this.ᜀ(A_0, conditionalFormats);
				num = 8;
				continue;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_81;
				}
				if (false)
				{
				}
				if (conditionalFormats.Count > 0)
				{
					num = 2;
					continue;
				}
				goto IL_25C;
			case 4:
				this.ᜀ(A_0, autoFilters);
				num = 7;
				continue;
			case 6:
				goto IL_69;
			case 7:
				goto IL_F1;
			case 8:
				goto IL_177;
			case 9:
				A_0.WriteAttributeString(RecordTableEnumerator.b("㭇㥉", a_), RecordTableEnumerator.b("ᩇ⍉⭋♍⑏ّ㭓ᩕ㵗㱙⡛", a_), null, RecordTableEnumerator.b("祇", a_));
				num = 0;
				continue;
			case 10:
				goto IL_EC;
			case 11:
				goto IL_81;
			case 12:
				this.ᜀ(A_0, names, true);
				num = 6;
				continue;
			case 13:
				if (A_1 == null)
				{
					num = 10;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("㭇㥉", a_), RecordTableEnumerator.b("὇╉㹋╍⍏㩑ㅓ㍕ⱗ", a_), null);
				A_0.WriteAttributeString(RecordTableEnumerator.b("㭇㥉", a_), RecordTableEnumerator.b("ه⭉⅋⭍", a_), null, A_1.Name);
				num = 14;
				continue;
			case 14:
				if (A_1.IsRightToLeft)
				{
					num = 9;
					continue;
				}
				goto IL_97;
			case 15:
				if (names.Count > 0)
				{
					num = 12;
					continue;
				}
				goto IL_69;
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			num = 13;
			continue;
			IL_69:
			this.ᜆ(A_0, A_1);
			if (true)
			{
			}
			num = 11;
			continue;
			IL_81:
			if (autoFilters.Count > 0)
			{
				num = 4;
				continue;
			}
			goto IL_F1;
			IL_97:
			autoFilters = A_1.AutoFilters;
			conditionalFormats = A_1.ConditionalFormats;
			names = A_1.Names;
			num = 15;
			continue;
			IL_F1:
			num = 3;
		}
		IL_64:
		throw new ArgumentNullException(RecordTableEnumerator.b("㽇㡉╋㩍㕏⁑", a_));
		IL_EC:
		throw new ArgumentNullException(RecordTableEnumerator.b("㭇≉⥋⭍⑏", a_));
		IL_177:
		IL_25C:
		this.ᜂ(A_0, A_1);
		this.ᜃ(A_0, A_1);
		A_0.WriteEndElement();
	}

	// Token: 0x0600549A RID: 21658 RVA: 0x0034E560 File Offset: 0x0034D560
	private void ᜆ(XmlWriter A_0, XlsWorksheet A_1)
	{
		int a_ = 2;
		int num = 5;
		for (;;)
		{
			float num2;
			switch (num)
			{
			case 0:
				goto IL_128;
			case 1:
				if (A_1 != null)
				{
					A_0.WriteStartElement(RecordTableEnumerator.b("䬷䤹", a_), RecordTableEnumerator.b("氷嬹帻刽┿", a_), null);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 8;
						continue;
					}
				}
				num = 0;
				continue;
			case 2:
				if ((double)num2 != 48.0)
				{
					num = 6;
					continue;
				}
				goto IL_1CF;
			case 3:
				A_0.WriteAttributeString(RecordTableEnumerator.b("䬷䤹", a_), RecordTableEnumerator.b("簷弹娻弽㔿⹁ぃᑅ❇㵉ы⭍㥏㕑㱓≕", a_), null, XmlConvert.ToString(A_1.DefaultRowHeight));
				num = 4;
				continue;
			case 4:
				goto IL_177;
			case 5:
				if (true)
				{
				}
				break;
			case 6:
				A_0.WriteAttributeString(RecordTableEnumerator.b("䬷䤹", a_), RecordTableEnumerator.b("簷弹娻弽㔿⹁ぃՅ❇♉㥋⍍㹏Ց㵓㉕ⱗ㉙", a_), null, XmlConvert.ToString(num2));
				num = 9;
				continue;
			case 7:
				goto IL_57;
			case 8:
				if (A_1.DefaultRowHeight != 12.75)
				{
					num = 3;
					continue;
				}
				goto IL_177;
			case 9:
				goto IL_161;
			}
			if (A_0 == null)
			{
				num = 7;
				continue;
			}
			num = 1;
			continue;
			IL_177:
			num2 = (float)A_1.ReservedHandle.ᜀ((double)A_1.ColumnWidthToPixels(A_1.DefaultColumnWidth), MeasureUnits.Pixel, MeasureUnits.Point);
			num = 2;
		}
		IL_57:
		throw new ArgumentNullException(RecordTableEnumerator.b("伷䠹唻䨽┿ぁ", a_));
		IL_128:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬷刹夻嬽㐿", a_));
		IL_161:
		IL_1CF:
		this.ᜅ(A_0, A_1);
		this.ᜄ(A_0, A_1);
		A_0.WriteEndElement();
	}

	// Token: 0x0600549B RID: 21659 RVA: 0x0034E754 File Offset: 0x0034D754
	private void ᜅ(XmlWriter A_0, XlsWorksheet A_1)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
				{
					if (A_1 == null)
					{
						num = 5;
						continue;
					}
					spr\u216E[] array = A_1.ColumnInformation;
					num2 = 1;
					num = 10;
					continue;
				}
				case 1:
					goto IL_84;
				case 3:
				{
					int num3;
					if (num3 > 0)
					{
						num = 14;
						continue;
					}
					goto IL_1C9;
				}
				case 4:
				{
					float num4 = (float)A_1.ReservedHandle.ᜀ((double)num4, MeasureUnits.Pixel, MeasureUnits.Point);
					A_0.WriteAttributeString(RecordTableEnumerator.b("䨸䠺", a_), RecordTableEnumerator.b("游刺夼䬾⥀", a_), null, XmlConvert.ToString(num4));
					num = 18;
					continue;
				}
				case 5:
					goto IL_1C7;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1A9;
					default:
					{
						if (false)
						{
						}
						spr\u216E spr_u216E;
						if (spr_u216E != null)
						{
							num = 16;
							continue;
						}
						goto IL_197;
					}
					}
					break;
				case 7:
					goto IL_1C9;
				case 8:
				{
					int num5;
					if (num5 <= 256)
					{
						if (true)
						{
						}
						num = 9;
						continue;
					}
					goto IL_197;
				}
				case 9:
				{
					A_0.WriteStartElement(RecordTableEnumerator.b("䨸䠺", a_), RecordTableEnumerator.b("稸吺儼䨾ⱀⵂ", a_), null);
					int num5;
					A_0.WriteAttributeString(RecordTableEnumerator.b("䨸䠺", a_), RecordTableEnumerator.b("瀸唺夼娾㥀", a_), null, num5.ToString());
					spr\u216E spr_u216E;
					float num4 = (float)A_1.ColumnWidthToPixels((double)spr_u216E.ᜉ() / 256.0);
					int num3 = (int)(spr_u216E.ᜀ() - spr_u216E.ᜈ());
					this.ᜀ(A_0, spr_u216E, (int)spr_u216E.ᜀ(), A_1.ParentWorkbook);
					num = 3;
					continue;
				}
				case 10:
					goto IL_225;
				case 11:
					return;
				case 12:
				{
					float num4;
					if ((double)num4 != A_1.DefaultColumnWidth)
					{
						num = 4;
						continue;
					}
					goto IL_2AA;
				}
				case 13:
				{
					spr\u216E[] array;
					if (num2 > array.Length - 1)
					{
						num = 11;
						continue;
					}
					spr\u216E spr_u216E = array[num2];
					num = 6;
					continue;
				}
				case 14:
				{
					int num3;
					A_0.WriteAttributeString(RecordTableEnumerator.b("䨸䠺", a_), RecordTableEnumerator.b("樸䬺尼儾", a_), null, num3.ToString());
					num = 7;
					continue;
				}
				case 15:
					goto IL_197;
				case 16:
				{
					spr\u216E spr_u216E;
					int num5 = (int)(spr_u216E.ᜈ() + 1);
					num = 8;
					continue;
				}
				case 17:
					goto IL_225;
				case 18:
					goto IL_2AA;
				}
				if (A_0 == null)
				{
					num = 1;
					continue;
				}
				goto IL_1A9;
				IL_197:
				num2++;
				num = 17;
				continue;
				IL_1A9:
				num = 0;
				continue;
				IL_1C9:
				A_0.WriteAttributeString(RecordTableEnumerator.b("䨸䠺", a_), RecordTableEnumerator.b("砸为䤼倾݀⩂ㅄ၆⁈⽊㥌❎", a_), null, RecordTableEnumerator.b("स", a_));
				num = 12;
				continue;
				IL_225:
				num = 13;
				continue;
				IL_2AA:
				A_0.WriteEndElement();
				num = 15;
			}
			IL_84:
			throw new ArgumentNullException(RecordTableEnumerator.b("丸䤺吼䬾⑀ㅂ", a_));
			IL_1C7:
			throw new ArgumentNullException(RecordTableEnumerator.b("丸吺似吾ቀ⭂⁄≆㵈", a_));
		}
		}
	}

	// Token: 0x0600549C RID: 21660 RVA: 0x0034EAB4 File Offset: 0x0034DAB4
	private void ᜄ(XmlWriter A_0, XlsWorksheet A_1)
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				sprᱧ sprᱧ;
				int num2;
				int num3;
				XlsCellRecordCollection cellRecords;
				switch (num)
				{
				case 0:
					goto IL_1ED;
				case 2:
					goto IL_183;
				case 3:
					if (sprᱧ != null)
					{
						num = 16;
						continue;
					}
					goto IL_24D;
				case 4:
					if (num2 + 1 != num3)
					{
						num = 10;
						continue;
					}
					goto IL_1B5;
				case 5:
					goto IL_1B3;
				case 6:
					goto IL_1B5;
				case 7:
					return;
				case 8:
					if (cellRecords.ContainsRow(num3 - 1))
					{
						num = 15;
						continue;
					}
					goto IL_183;
				case 9:
					goto IL_24D;
				case 10:
					if (true)
					{
					}
					A_0.WriteAttributeString(RecordTableEnumerator.b("䠺丼", a_), RecordTableEnumerator.b("爺匼嬾⑀㭂", a_), null, num3.ToString());
					num = 6;
					continue;
				case 11:
					goto IL_7C;
				case 12:
				{
					if (A_1 == null)
					{
						num = 5;
						continue;
					}
					cellRecords = A_1.CellRecords;
					num2 = 0;
					num3 = A_1.FirstRow;
					int lastRow = A_1.LastRow;
					num = 0;
					continue;
				}
				case 13:
				{
					int lastRow;
					if (num3 > lastRow)
					{
						num = 7;
						continue;
					}
					num = 8;
					continue;
				}
				case 14:
					goto IL_1ED;
				case 15:
					A_0.WriteStartElement(RecordTableEnumerator.b("椺刼䠾", a_));
					num = 4;
					continue;
				case 16:
					A_0.WriteAttributeString(RecordTableEnumerator.b("䠺丼", a_), RecordTableEnumerator.b("猺堼嘾♀⭂ㅄ", a_), null, XmlConvert.ToString((double)sprᱧ.\u1718() / 20.0));
					A_0.WriteAttributeString(RecordTableEnumerator.b("䠺丼", a_), RecordTableEnumerator.b("稺䠼䬾⹀Ղⱄ㍆ň⹊⑌⡎㥐❒", a_), null, RecordTableEnumerator.b("଺", a_));
					this.ᜀ(A_0, sprᱧ, sprᱧ.\u171E(), A_1.ParentWorkbook);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1CC;
					default:
						if (false)
						{
						}
						num = 9;
						continue;
					}
					break;
				}
				if (A_0 == null)
				{
					num = 11;
					continue;
				}
				num = 12;
				continue;
				IL_183:
				num3++;
				num = 14;
				continue;
				IL_1CC:
				num = 3;
				continue;
				IL_1B5:
				num2 = num3;
				sprᱧ = cellRecords.Table.ᜄ().ᜁ(num3 - 1);
				goto IL_1CC;
				IL_1ED:
				num = 13;
				continue;
				IL_24D:
				this.ᜀ(A_0, A_1, num3);
				A_0.WriteEndElement();
				num = 2;
			}
			IL_7C:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰺似嘾㕀♂㝄", a_));
			IL_1B3:
			throw new ArgumentNullException(RecordTableEnumerator.b("䠺唼娾⑀㝂", a_));
		}
		}
	}

	// Token: 0x0600549D RID: 21661 RVA: 0x0034ED9C File Offset: 0x0034DD9C
	private void ᜀ(XmlWriter A_0, spr\u2502 A_1, int A_2, XlsWorkbook A_3)
	{
		int a_ = 0;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_61;
			case 1:
				goto IL_5C;
			case 2:
				goto IL_AF;
			case 3:
			{
				int num2;
				if (num2 != 0)
				{
					num = 10;
					continue;
				}
				goto IL_61;
			}
			case 4:
				num = 3;
				continue;
			case 6:
				return;
			case 7:
			{
				int num2;
				if (num2 != A_3.DefaultXFIndex)
				{
					num = 4;
					continue;
				}
				goto IL_61;
			}
			case 8:
				goto IL_151;
			case 9:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_AF;
				default:
				{
					if (false)
					{
					}
					int num2 = (int)A_1.ᜃ();
					num = 7;
					continue;
				}
				}
				break;
			case 10:
				A_0.WriteAttributeString(RecordTableEnumerator.b("䔵䬷", a_), RecordTableEnumerator.b("攵䰷䌹倻嬽िف", a_), null, RecordTableEnumerator.b("䔵", a_) + A_1.ᜃ().ToString());
				num = 0;
				continue;
			case 11:
				num = 12;
				continue;
			case 12:
				if (true)
				{
				}
				if (A_1.ᜁ())
				{
					num = 8;
					continue;
				}
				return;
			case 13:
				if (!A_1.ᜂ())
				{
					num = 11;
					continue;
				}
				goto IL_151;
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			num = 9;
			continue;
			IL_61:
			num = 13;
			continue;
			IL_151:
			A_0.WriteAttributeString(RecordTableEnumerator.b("䔵䬷", a_), RecordTableEnumerator.b("縵儷帹堻嬽⸿", a_), null, RecordTableEnumerator.b("ܵ", a_));
			num = 6;
		}
		IL_5C:
		throw new ArgumentNullException(RecordTableEnumerator.b("䄵䨷匹䠻嬽㈿", a_));
		IL_AF:
		throw new ArgumentNullException(RecordTableEnumerator.b("䐵崷夹医䰽␿", a_));
	}

	// Token: 0x0600549E RID: 21662 RVA: 0x0034EF98 File Offset: 0x0034DF98
	private void ᜀ(XmlWriter A_0, spr\u25A6.ᜀ A_1)
	{
		int a_ = 17;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				goto IL_A1;
			case 1:
				goto IL_5A;
			case 3:
				goto IL_8B;
			}
			IL_29:
			if (A_0 != null)
			{
				num = 0;
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
				num = 1;
				continue;
			}
			goto IL_29;
		}
		IL_5A:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("う㭈≊㥌⩎⍐", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕆ⱈⱊ⑌⁎㽐", a_));
		IL_A1:
		int num2 = A_1.ᜇ() - A_1.ᜂ();
		int num3 = A_1.ᜃ() - A_1.ᜅ();
		A_0.WriteAttributeString(RecordTableEnumerator.b("㑆㩈", a_), RecordTableEnumerator.b("੆ⱈ㥊⩌⩎ᕐ㱒≔㥖", a_), null, num2.ToString());
		A_0.WriteAttributeString(RecordTableEnumerator.b("㑆㩈", a_), RecordTableEnumerator.b("੆ⱈ㥊⩌⩎ၐげ❔㡖⩘⡚", a_), null, num3.ToString());
	}

	// Token: 0x0600549F RID: 21663 RVA: 0x0034F0B8 File Offset: 0x0034E0B8
	private void ᜀ(XmlWriter A_0, IComment A_1, XlsFontsCollection A_2, IFont A_3)
	{
		int a_ = 4;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_266;
				default:
					goto IL_79;
				}
				break;
			case 1:
				A_0.WriteAttributeString(RecordTableEnumerator.b("䤹伻", a_), RecordTableEnumerator.b("椹吻儽㜿́⡃ㅅ⥇㍉㽋", a_), null, RecordTableEnumerator.b("ହ", a_));
				num = 5;
				continue;
			case 3:
			{
				string author;
				if (author.Length != 0)
				{
					num = 13;
					continue;
				}
				goto IL_96;
			}
			case 4:
			{
				string text;
				if (text.Length != 0)
				{
					num = 8;
					continue;
				}
				goto IL_294;
			}
			case 5:
				goto IL_131;
			case 6:
				goto IL_12C;
			case 7:
				goto IL_292;
			case 8:
			{
				spr\u223A a_2 = ((RichTextString)A_1.RichText).TextObject;
				string text;
				this.ᜀ(A_0, a_2, text, A_2, A_3);
				num = 10;
				continue;
			}
			case 9:
			{
				if (A_2 == null)
				{
					num = 7;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("䤹伻", a_), RecordTableEnumerator.b("礹医匽ⴿ❁⩃㉅", a_), null);
				string author = A_1.Author;
				string text = A_1.Text;
				num = 3;
				continue;
			}
			case 10:
				goto IL_218;
			case 11:
				goto IL_96;
			case 12:
				if (A_1 == null)
				{
					num = 6;
					continue;
				}
				num = 9;
				continue;
			case 13:
			{
				string author;
				A_0.WriteAttributeString(RecordTableEnumerator.b("䤹伻", a_), RecordTableEnumerator.b("笹䤻䨽⠿ⵁ㙃", a_), null, author);
				goto IL_266;
			}
			case 14:
				if (A_1.IsVisible)
				{
					num = 1;
					continue;
				}
				goto IL_131;
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			num = 12;
			continue;
			IL_96:
			num = 14;
			continue;
			IL_131:
			A_0.WriteStartElement(RecordTableEnumerator.b("䤹伻", a_), RecordTableEnumerator.b("縹崻䨽ℿ", a_), null);
			A_0.WriteAttributeString(RecordTableEnumerator.b("䈹儻刽⸿ㅁ", a_), null, null, RecordTableEnumerator.b("刹䠻䨽〿硁歃楅㽇㵉㭋恍❏慑穓㥕⩗㵙獛੝㉟䵡㙣⍥⭧䝩ѫᩭᵯṱ䁳䙵", a_));
			num = 4;
			continue;
			IL_266:
			num = 11;
		}
		IL_79:
		if (true)
		{
		}
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䴹主圽㐿❁㙃", a_));
		IL_12C:
		throw new ArgumentNullException(RecordTableEnumerator.b("夹医匽ⴿ❁⩃㉅", a_));
		IL_218:
		goto IL_294;
		IL_292:
		throw new ArgumentNullException(RecordTableEnumerator.b("尹医倽㐿ㅁ", a_));
		IL_294:
		A_0.WriteEndElement();
		A_0.WriteEndElement();
	}

	// Token: 0x060054A0 RID: 21664 RVA: 0x0034F368 File Offset: 0x0034E368
	private void ᜀ(XmlWriter A_0, spr\u223A A_1, string A_2, XlsFontsCollection A_3, IFont A_4)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				int num2;
				int count;
				int num3;
				int num4;
				SortedList<int, int> sortedList;
				int num5;
				IFont a_2;
				switch (num)
				{
				case 1:
					if (num2 != 0)
					{
						num = 15;
						continue;
					}
					goto IL_91;
				case 2:
					return;
				case 3:
					goto IL_124;
				case 4:
					goto IL_21D;
				case 5:
					goto IL_21D;
				case 6:
					if (count - num3 != 1)
					{
						num = 16;
						continue;
					}
					num = 9;
					continue;
				case 7:
					goto IL_8C;
				case 8:
					goto IL_115;
				case 9:
				{
					int length;
					num4 = length;
					goto IL_175;
				}
				case 10:
				{
					if (A_2.Length == 0)
					{
						num = 18;
						continue;
					}
					sortedList = A_1.ᜇ();
					IList<int> values = sortedList.Values;
					IList<int> keys = sortedList.Keys;
					num2 = keys[0];
					bool flag = sortedList.Count > 0;
					num = 19;
					continue;
				}
				case 11:
				{
					if (num3 >= count)
					{
						num = 2;
						continue;
					}
					IList<int> values;
					int index = values[num3];
					IList<int> keys;
					num5 = keys[num3];
					a_2 = A_3[index];
					num = 6;
					continue;
				}
				case 12:
					if (A_3 == null)
					{
						num = 13;
						continue;
					}
					num = 10;
					continue;
				case 13:
					goto IL_F4;
				case 14:
					goto IL_91;
				case 15:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_115;
					default:
						if (false)
						{
						}
						this.ᜀ(A_0, A_4, A_2.Substring(0, num2));
						num = 14;
						continue;
					}
					break;
				case 16:
					if (true)
					{
					}
					num = 20;
					continue;
				case 17:
					return;
				case 18:
					goto IL_270;
				case 19:
				{
					bool flag;
					if (!flag)
					{
						num = 17;
						continue;
					}
					int length = A_2.Length;
					num = 1;
					continue;
				}
				case 20:
				{
					IList<int> keys;
					num4 = keys[num3 + 1];
					goto IL_175;
				}
				}
				if (A_0 == null)
				{
					num = 7;
					continue;
				}
				num = 8;
				continue;
				IL_91:
				num3 = 0;
				count = sortedList.Count;
				num = 5;
				continue;
				IL_115:
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				num = 12;
				continue;
				IL_175:
				num5 = num4;
				this.ᜀ(A_0, a_2, A_2.Substring(num2, num5 - num2));
				num2 = num5;
				num3++;
				num = 4;
				continue;
				IL_21D:
				num = 11;
			}
			IL_8C:
			throw new ArgumentNullException(RecordTableEnumerator.b("㕁㙃⽅㱇⽉㹋", a_));
			IL_F4:
			throw new ArgumentNullException(RecordTableEnumerator.b("с⭃⡅㱇㥉", a_));
			IL_124:
			throw new ArgumentNullException(RecordTableEnumerator.b("ぁぃ⁅", a_));
			IL_270:
			throw new ArgumentNullException(RecordTableEnumerator.b("㙁⅃㹅㱇", a_));
		}
		}
	}

	// Token: 0x060054A1 RID: 21665 RVA: 0x0034F664 File Offset: 0x0034E664
	private void ᜀ(XmlWriter A_0, IFont A_1, string A_2)
	{
		int a_ = 2;
		if (true)
		{
		}
		int num = 19;
		StringBuilder stringBuilder;
		StringBuilder stringBuilder2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_1E7;
			case 1:
				this.ᜁ(RecordTableEnumerator.b("з椹䤻尽縿", a_), RecordTableEnumerator.b("зᔹ漻䬽∿籁", a_), stringBuilder, stringBuilder2);
				num = 20;
				continue;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1E7;
				default:
					if (false)
					{
					}
					this.ᜁ(RecordTableEnumerator.b("з猹Ȼ", a_), RecordTableEnumerator.b("зᔹ画=", a_), stringBuilder, stringBuilder2);
					num = 9;
					continue;
				}
				break;
			case 3:
				this.ᜁ(RecordTableEnumerator.b("з砹Ȼ", a_), RecordTableEnumerator.b("зᔹ縻=", a_), stringBuilder, stringBuilder2);
				num = 0;
				continue;
			case 4:
				goto IL_83;
			case 5:
				if (A_1.IsSubscript)
				{
					num = 1;
					continue;
				}
				goto IL_2CF;
			case 6:
				return;
			case 7:
				if (A_1.IsItalic)
				{
					num = 2;
					continue;
				}
				goto IL_15D;
			case 8:
				goto IL_15B;
			case 9:
				goto IL_15D;
			case 10:
				this.ᜁ(RecordTableEnumerator.b("з椹Ȼ", a_), RecordTableEnumerator.b("зᔹ漻=", a_), stringBuilder, stringBuilder2);
				num = 16;
				continue;
			case 11:
				if (A_1.Underline == FontUnderlineType.Single)
				{
					num = 17;
					continue;
				}
				goto IL_232;
			case 12:
				if (A_2.Length == 0)
				{
					num = 6;
					continue;
				}
				stringBuilder = this.ᜁ();
				stringBuilder2 = this.ᜀ();
				num = 15;
				continue;
			case 13:
				goto IL_232;
			case 14:
				if (A_1 == null)
				{
					num = 8;
					continue;
				}
				num = 12;
				continue;
			case 15:
				if (A_1.IsBold)
				{
					num = 3;
					continue;
				}
				goto IL_28D;
			case 16:
				goto IL_20C;
			case 17:
				this.ᜁ(RecordTableEnumerator.b("з漹Ȼ", a_), RecordTableEnumerator.b("зᔹ椻=", a_), stringBuilder, stringBuilder2);
				num = 13;
				continue;
			case 18:
				if (A_1.IsStrikethrough)
				{
					num = 10;
					continue;
				}
				goto IL_20C;
			case 20:
				goto IL_1B3;
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			num = 14;
			continue;
			IL_15D:
			num = 11;
			continue;
			IL_20C:
			num = 5;
			continue;
			IL_232:
			num = 18;
			continue;
			IL_28D:
			num = 7;
			continue;
			IL_1E7:
			goto IL_28D;
		}
		IL_83:
		throw new ArgumentNullException();
		IL_15B:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨷丹娻洽㐿ぁⵃ⡅⽇", a_));
		IL_1B3:
		IL_2CF:
		stringBuilder.Append(RecordTableEnumerator.b("з簹医倽㐿", a_));
		stringBuilder2.Insert(0, RecordTableEnumerator.b("зᔹ稻儽⸿㙁穃", a_));
		this.ᜀ(RecordTableEnumerator.b("䀷9缻儽ⰿⵁ㙃", a_), this.ᜀ(A_1.Color), stringBuilder, stringBuilder2);
		this.ᜀ(RecordTableEnumerator.b("䀷9稻弽⌿❁", a_), A_1.FontName, stringBuilder, stringBuilder2);
		this.ᜀ(RecordTableEnumerator.b("䀷9漻圽㨿❁", a_), A_1.Size.ToString(), stringBuilder, stringBuilder2);
		stringBuilder.Append('>');
		A_0.WriteRaw(stringBuilder.ToString());
		A_2 = A_2.Replace(RecordTableEnumerator.b("ḷ", a_), RecordTableEnumerator.b("ḷ嬹儻丽笿", a_));
		A_2 = A_2.Replace(RecordTableEnumerator.b("㈷", a_), RecordTableEnumerator.b("ḷ഻᤹฽笿", a_));
		A_0.WriteRaw(A_2);
		A_0.WriteRaw(stringBuilder2.ToString());
	}

	// Token: 0x060054A2 RID: 21666 RVA: 0x0034FA40 File Offset: 0x0034EA40
	private void ᜀ(XmlWriter A_0, XlsDataValidationTable A_1)
	{
		int a_ = 2;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				return;
			case 2:
				goto IL_8B;
			case 3:
				goto IL_58;
			}
			if (A_0 == null)
			{
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_8B;
				default:
					if (false)
					{
					}
					num = 3;
					break;
				}
			}
			else
			{
				num = 1;
			}
		}
		IL_58:
		throw new ArgumentNullException(RecordTableEnumerator.b("伷䠹唻䨽┿ぁ", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("尷䰹栻弽∿⹁⅃", a_));
	}

	// Token: 0x060054A3 RID: 21667 RVA: 0x0034FAF0 File Offset: 0x0034EAF0
	private void ᜀ(XmlWriter A_0, IDataValidation A_1)
	{
		int a_ = 1;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_50;
			case 2:
				goto IL_83;
			case 3:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				return;
			}
			if (A_0 == null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_83;
				default:
					if (false)
					{
					}
					num = 0;
					break;
				}
			}
			else
			{
				num = 3;
			}
		}
		IL_50:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䀶䬸刺䤼娾㍀", a_));
		IL_83:
		throw new ArgumentNullException(RecordTableEnumerator.b("匶伸", a_));
	}

	// Token: 0x060054A4 RID: 21668 RVA: 0x0034FBA0 File Offset: 0x0034EBA0
	private void ᜀ(XmlWriter A_0, XlsWorksheetConditionalFormats A_1)
	{
		int a_ = 2;
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
					return;
				case 2:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 1;
						continue;
					}
					XlsConditionalFormats xlsConditionalFormats = A_1[num2];
					A_0.WriteStartElement(RecordTableEnumerator.b("䀷", a_), RecordTableEnumerator.b("笷唹刻娽⤿㙁ⵃ⥅♇⭉⁋ࡍ㽏⁑㥓㝕ⱗ⹙㕛そݟ", a_), null);
					A_0.WriteStartElement(RecordTableEnumerator.b("䀷", a_), RecordTableEnumerator.b("樷嬹刻夽┿", a_), null);
					A_0.WriteString(xlsConditionalFormats.AddressR1C1);
					A_0.WriteEndElement();
					IConditionalFormats a_2 = xlsConditionalFormats;
					this.ᜀ(A_0, a_2);
					A_0.WriteEndElement();
					num2++;
					num = 4;
					continue;
				}
				case 3:
					goto IL_119;
				case 4:
					goto IL_119;
				case 5:
					goto IL_6A;
				case 6:
				{
					if (A_1 == null)
					{
						num = 7;
						continue;
					}
					int num2 = 0;
					int count = A_1.Count;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_119;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				}
				case 7:
					goto IL_155;
				}
				if (true)
				{
				}
				if (A_0 == null)
				{
					num = 5;
					continue;
				}
				num = 6;
				continue;
				IL_119:
				num = 2;
			}
			IL_6A:
			throw new ArgumentNullException(RecordTableEnumerator.b("伷䠹唻䨽┿ぁ", a_));
			IL_155:
			throw new ArgumentNullException(RecordTableEnumerator.b("嬷唹刻娽⤿㙁ⵃ⥅♇㥉", a_));
		}
		}
	}

	// Token: 0x060054A5 RID: 21669 RVA: 0x0034FD38 File Offset: 0x0034ED38
	private void ᜀ(XmlWriter A_0, IConditionalFormats A_1)
	{
		int a_ = 3;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				int num2;
				int count;
				if (num2 >= count)
				{
					if (true)
					{
					}
					num = 2;
					continue;
				}
				IConditionalFormat a_2 = A_1[num2];
				this.ᜀ(A_0, a_2);
				num2++;
				num = 3;
				continue;
			}
			case 2:
				return;
			case 3:
				goto IL_96;
			case 4:
				goto IL_4E;
			case 5:
				if (A_1 == null)
				{
					num = 7;
					continue;
				}
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
					int num2 = 0;
					int count = A_1.Count;
					break;
				}
				}
				num = 6;
				continue;
			case 6:
				goto IL_96;
			case 7:
				goto IL_D3;
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			num = 5;
			continue;
			IL_96:
			num = 0;
		}
		IL_4E:
		throw new ArgumentNullException(RecordTableEnumerator.b("丸䤺吼䬾⑀ㅂ", a_));
		IL_D3:
		throw new ArgumentNullException(RecordTableEnumerator.b("弸吺似刾⁀㝂", a_));
	}

	// Token: 0x060054A6 RID: 21670 RVA: 0x0034FE4C File Offset: 0x0034EE4C
	private void ᜀ(XmlWriter A_0, IConditionalFormat A_1)
	{
		int a_ = 3;
		int num = 8;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				string secondFormulaR1C;
				if (secondFormulaR1C != null)
				{
					num = 11;
					continue;
				}
				goto IL_28F;
			}
			case 1:
			{
				string secondFormulaR1C;
				if (secondFormulaR1C.Length > 0)
				{
					num = 12;
					continue;
				}
				goto IL_28F;
			}
			case 2:
				if (A_1.FormatType == ConditionalFormatType.CellValue)
				{
					num = 15;
					continue;
				}
				goto IL_AA;
			case 3:
				goto IL_A8;
			case 4:
			{
				A_0.WriteStartElement(RecordTableEnumerator.b("䄸", a_), RecordTableEnumerator.b("漸娺儼䨾⑀牂", a_), null);
				string firstFormulaR1C;
				A_0.WriteString(firstFormulaR1C);
				A_0.WriteEndElement();
				num = 3;
				continue;
			}
			case 5:
				goto IL_182;
			case 6:
				goto IL_AA;
			case 7:
				num = 9;
				continue;
			case 9:
			{
				string firstFormulaR1C;
				if (firstFormulaR1C.Length > 0)
				{
					num = 4;
					continue;
				}
				goto IL_121;
			}
			case 10:
				goto IL_68;
			case 11:
				num = 1;
				continue;
			case 12:
			{
				if (true)
				{
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("䄸", a_), RecordTableEnumerator.b("漸娺儼䨾⑀煂", a_), null);
				string secondFormulaR1C;
				A_0.WriteString(secondFormulaR1C);
				A_0.WriteEndElement();
				num = 5;
				continue;
			}
			case 13:
			{
				string firstFormulaR1C;
				if (firstFormulaR1C != null)
				{
					num = 7;
					continue;
				}
				goto IL_121;
			}
			case 14:
			{
				if (A_1 == null)
				{
					num = 16;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("䄸", a_), RecordTableEnumerator.b("稸吺匼嬾⡀㝂ⱄ⡆❈", a_), null);
				string firstFormulaR1C = A_1.FirstFormulaR1C1;
				string secondFormulaR1C = A_1.SecondFormulaR1C1;
				num = 2;
				continue;
			}
			case 15:
				A_0.WriteStartElement(RecordTableEnumerator.b("䄸", a_), RecordTableEnumerator.b("核为尼匾⡀╂ⱄ≆㭈", a_), null);
				A_0.WriteString(A_1.Operator.ToString());
				A_0.WriteEndElement();
				num = 6;
				continue;
			case 16:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A8;
				}
				goto Block_6;
			}
			if (A_0 == null)
			{
				num = 10;
				continue;
			}
			num = 14;
			continue;
			IL_AA:
			num = 13;
			continue;
			IL_121:
			num = 0;
			continue;
			IL_A8:
			goto IL_121;
		}
		IL_68:
		throw new ArgumentNullException(RecordTableEnumerator.b("丸䤺吼䬾⑀ㅂ", a_));
		IL_182:
		goto IL_28F;
		Block_6:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("娸吺匼嬾⡀㝂ⱄ⡆❈", a_));
		IL_28F:
		A_0.WriteStartElement(RecordTableEnumerator.b("䄸", a_), RecordTableEnumerator.b("缸吺似刾⁀㝂", a_), null);
		A_0.WriteAttributeString(RecordTableEnumerator.b("䄸", a_), RecordTableEnumerator.b("樸伺䐼匾⑀", a_), null, this.ᜀ(A_1));
		A_0.WriteEndElement();
		A_0.WriteEndElement();
	}

	// Token: 0x060054A7 RID: 21671 RVA: 0x00350144 File Offset: 0x0034F144
	private void ᜃ(XmlWriter A_0, XlsWorksheet A_1)
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			int num = 14;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_397;
				case 1:
					try
					{
						num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								XlsHPageBreak xlsHPageBreak;
								if (xlsHPageBreak.EndColumn > 1)
								{
									num = 10;
									continue;
								}
								goto IL_2E7;
							}
							case 1:
							{
								XlsHPageBreak xlsHPageBreak;
								if (xlsHPageBreak.StartColumn > 1)
								{
									num = 7;
									continue;
								}
								goto IL_202;
							}
							case 3:
							{
								IEnumerator enumerator;
								if (!enumerator.MoveNext())
								{
									num = 14;
									continue;
								}
								XlsHPageBreak xlsHPageBreak = (XlsHPageBreak)enumerator.Current;
								A_0.WriteStartElement(RecordTableEnumerator.b("渻儽㜿A㙃⍅⥇ⅉ", a_), RecordTableEnumerator.b("䤻䰽⸿硁㝃╅⁇⽉⅋⽍⍏网㥓㽕㭗⡙㍛ⵝཟѡၣ䭥୧թū呭Ὧᑱታή᭷ό䙻᭽", a_));
								string value = xlsHPageBreak.Row.ToString();
								A_0.WriteElementString(RecordTableEnumerator.b("渻儽㜿", a_), RecordTableEnumerator.b("䤻䰽⸿硁㝃╅⁇⽉⅋⽍⍏网㥓㽕㭗⡙㍛ⵝཟѡၣ䭥୧թū呭Ὧᑱታή᭷ό䙻᭽", a_), value);
								num = 1;
								continue;
							}
							case 4:
							{
								XlsHPageBreak xlsHPageBreak;
								if (xlsHPageBreak.EndColumn <= 256)
								{
									num = 5;
									continue;
								}
								goto IL_2E7;
							}
							case 5:
							{
								XlsHPageBreak xlsHPageBreak;
								string value2 = Convert.ToString(xlsHPageBreak.EndColumn - 1);
								A_0.WriteElementString(RecordTableEnumerator.b("缻儽ⰿ݁⩃≅", a_), RecordTableEnumerator.b("䤻䰽⸿硁㝃╅⁇⽉⅋⽍⍏网㥓㽕㭗⡙㍛ⵝཟѡၣ䭥୧թū呭Ὧᑱታή᭷ό䙻᭽", a_), value2);
								num = 9;
								continue;
							}
							case 6:
							{
								XlsHPageBreak xlsHPageBreak;
								string value3 = Convert.ToString(xlsHPageBreak.StartColumn - 1);
								A_0.WriteElementString(RecordTableEnumerator.b("缻儽ⰿᅁぃ❅㩇㹉", a_), RecordTableEnumerator.b("䤻䰽⸿硁㝃╅⁇⽉⅋⽍⍏网㥓㽕㭗⡙㍛ⵝཟѡၣ䭥୧թū呭Ὧᑱታή᭷ό䙻᭽", a_), value3);
								num = 12;
								continue;
							}
							case 7:
								num = 13;
								continue;
							case 9:
								goto IL_2E7;
							case 10:
								num = 4;
								continue;
							case 11:
								goto IL_30A;
							case 12:
								goto IL_202;
							case 13:
							{
								XlsHPageBreak xlsHPageBreak;
								if (xlsHPageBreak.StartColumn <= 256)
								{
									num = 6;
									continue;
								}
								goto IL_202;
							}
							case 14:
								num = 11;
								continue;
							}
							IL_155:
							num = 3;
							continue;
							goto IL_155;
							IL_202:
							num = 0;
							continue;
							IL_2E7:
							A_0.WriteEndElement();
							num = 8;
						}
						IL_30A:
						goto IL_3BF;
					}
					finally
					{
						for (;;)
						{
							for (;;)
							{
								IEnumerator enumerator;
								IDisposable disposable = enumerator as IDisposable;
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
										disposable.Dispose();
										num = 2;
										continue;
									case 1:
										if (disposable == null)
										{
											goto IL_373;
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
										break;
									case 2:
										goto IL_371;
									}
									break;
								}
							}
						}
						IL_371:
						IL_373:;
					}
					goto IL_374;
					IL_3BF:
					A_0.WriteEndElement();
					num = 7;
					continue;
				case 2:
					if (A_1 == null)
					{
						num = 9;
						continue;
					}
					goto IL_646;
				case 3:
				{
					A_0.WriteStartElement(RecordTableEnumerator.b("渻儽㜿A㙃⍅⥇ⅉ㽋", a_), RecordTableEnumerator.b("䤻䰽⸿硁㝃╅⁇⽉⅋⽍⍏网㥓㽕㭗⡙㍛ⵝཟѡၣ䭥୧թū呭Ὧᑱታή᭷ό䙻᭽", a_));
					IEnumerator enumerator = A_1.HPageBreaks.GetEnumerator();
					num = 1;
					continue;
				}
				case 4:
					goto IL_78;
				case 5:
					if (A_1.HPageBreaks.Count > 0)
					{
						num = 3;
						continue;
					}
					goto IL_74C;
				case 6:
					if (A_1.VPageBreaks.Count > 0)
					{
						num = 13;
						continue;
					}
					goto IL_397;
				case 7:
					goto IL_3D1;
				case 8:
					num = 6;
					continue;
				case 9:
					goto IL_392;
				case 10:
					if (A_1.HPageBreaks != null)
					{
						num = 11;
						continue;
					}
					goto IL_74C;
				case 11:
					num = 5;
					continue;
				case 12:
					if (A_1.VPageBreaks != null)
					{
						num = 8;
						continue;
					}
					goto IL_397;
				case 13:
				{
					A_0.WriteStartElement(RecordTableEnumerator.b("缻儽ⰿA㙃⍅⥇ⅉ㽋", a_), RecordTableEnumerator.b("䤻䰽⸿硁㝃╅⁇⽉⅋⽍⍏网㥓㽕㭗⡙㍛ⵝཟѡၣ䭥୧թū呭Ὧᑱታή᭷ό䙻᭽", a_));
					IEnumerator enumerator2 = A_1.VPageBreaks.GetEnumerator();
					num = 15;
					continue;
				}
				case 15:
					try
					{
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								XlsVPageBreak xlsVPageBreak;
								if (xlsVPageBreak.StartRow <= 65536)
								{
									num = 6;
									continue;
								}
								goto IL_4A5;
							}
							case 2:
							{
								IEnumerator enumerator2;
								if (!enumerator2.MoveNext())
								{
									num = 14;
									continue;
								}
								XlsVPageBreak xlsVPageBreak = (XlsVPageBreak)enumerator2.Current;
								A_0.WriteStartElement(RecordTableEnumerator.b("缻儽ⰿA㙃⍅⥇ⅉ", a_), RecordTableEnumerator.b("䤻䰽⸿硁㝃╅⁇⽉⅋⽍⍏网㥓㽕㭗⡙㍛ⵝཟѡၣ䭥୧թū呭Ὧᑱታή᭷ό䙻᭽", a_));
								string value4 = Convert.ToString(xlsVPageBreak.Column - 1);
								A_0.WriteElementString(RecordTableEnumerator.b("缻儽ⰿ㝁⥃⡅", a_), RecordTableEnumerator.b("䤻䰽⸿硁㝃╅⁇⽉⅋⽍⍏网㥓㽕㭗⡙㍛ⵝཟѡၣ䭥୧թū呭Ὧᑱታή᭷ό䙻᭽", a_), value4);
								num = 11;
								continue;
							}
							case 3:
								goto IL_5F8;
							case 4:
								num = 13;
								continue;
							case 5:
								goto IL_4A5;
							case 6:
							{
								XlsVPageBreak xlsVPageBreak;
								string value5 = Convert.ToString(xlsVPageBreak.StartRow - 1);
								A_0.WriteElementString(RecordTableEnumerator.b("渻儽㜿ᅁぃ❅㩇㹉", a_), RecordTableEnumerator.b("䤻䰽⸿硁㝃╅⁇⽉⅋⽍⍏网㥓㽕㭗⡙㍛ⵝཟѡၣ䭥୧թū呭Ὧᑱታή᭷ό䙻᭽", a_), value5);
								num = 5;
								continue;
							}
							case 7:
							{
								XlsVPageBreak xlsVPageBreak;
								if (xlsVPageBreak.EndRow > 1)
								{
									num = 4;
									continue;
								}
								goto IL_431;
							}
							case 9:
							{
								XlsVPageBreak xlsVPageBreak;
								string value6 = Convert.ToString(xlsVPageBreak.EndRow - 1);
								A_0.WriteElementString(RecordTableEnumerator.b("渻儽㜿݁⩃≅", a_), RecordTableEnumerator.b("䤻䰽⸿硁㝃╅⁇⽉⅋⽍⍏网㥓㽕㭗⡙㍛ⵝཟѡၣ䭥୧թū呭Ὧᑱታή᭷ό䙻᭽", a_), value6);
								num = 10;
								continue;
							}
							case 10:
								goto IL_431;
							case 11:
							{
								XlsVPageBreak xlsVPageBreak;
								if (xlsVPageBreak.StartRow > 1)
								{
									num = 12;
									continue;
								}
								goto IL_4A5;
							}
							case 12:
								num = 0;
								continue;
							case 13:
							{
								XlsVPageBreak xlsVPageBreak;
								if (xlsVPageBreak.EndRow <= 65536)
								{
									num = 9;
									continue;
								}
								goto IL_431;
							}
							case 14:
								num = 3;
								continue;
							}
							goto IL_42C;
							IL_431:
							A_0.WriteEndElement();
							num = 8;
							continue;
							IL_4A5:
							num = 7;
							continue;
							IL_5C6:
							num = 2;
							continue;
							IL_42C:
							goto IL_5C6;
						}
						IL_5F8:
						goto IL_B2;
					}
					finally
					{
						for (;;)
						{
							IEnumerator enumerator2;
							IDisposable disposable2 = enumerator2 as IDisposable;
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 0:
									if (disposable2 != null)
									{
										num = 1;
										continue;
									}
									goto IL_645;
								case 1:
									disposable2.Dispose();
									num = 2;
									continue;
								case 2:
									goto IL_643;
								}
								break;
							}
						}
						IL_643:
						IL_645:;
					}
					goto IL_646;
					IL_B2:
					A_0.WriteEndElement();
					num = 0;
					continue;
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				IL_374:
				num = 2;
				continue;
				IL_397:
				num = 10;
				continue;
				IL_646:
				A_0.WriteStartElement(RecordTableEnumerator.b("氻弽✿❁ك㑅ⵇ⭉❋㵍", a_), RecordTableEnumerator.b("䤻䰽⸿硁㝃╅⁇⽉⅋⽍⍏网㥓㽕㭗⡙㍛ⵝཟѡၣ䭥୧թū呭Ὧᑱታή᭷ό䙻᭽", a_));
				num = 12;
			}
			IL_78:
			throw new ArgumentNullException(RecordTableEnumerator.b("䬻䰽⤿㙁⅃㑅", a_));
			IL_392:
			throw new ArgumentNullException(RecordTableEnumerator.b("伻嘽┿❁ぃ", a_));
			IL_3D1:
			if (true)
			{
			}
			IL_74C:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x060054A8 RID: 21672 RVA: 0x003508D8 File Offset: 0x0034F8D8
	private void ᜂ(XmlWriter A_0, XlsWorksheet A_1)
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			int num = 8;
			for (;;)
			{
				int zoomScaleNormal;
				XlsPageSetup xlsPageSetup;
				int tabKnownColor;
				int zoomScalePageBreakView;
				switch (num)
				{
				case 0:
				{
					int visibility = (int)A_1.Visibility;
					this.ᜀ(A_0, RecordTableEnumerator.b("丵", a_), RecordTableEnumerator.b("怵儷䤹唻尽ⰿ❁", a_), sprỉ.\u17EF[visibility]);
					num = 15;
					continue;
				}
				case 1:
					if (true)
					{
					}
					num = 21;
					continue;
				case 2:
					goto IL_2FF;
				case 3:
					if (zoomScaleNormal != 100)
					{
						num = 1;
						continue;
					}
					goto IL_3F8;
				case 4:
					goto IL_1F4;
				case 5:
					if (A_1 == null)
					{
						num = 12;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("丵", a_), RecordTableEnumerator.b("愵圷䠹圻䴽⠿❁⅃㉅݇㩉㡋❍㽏㱑❓", a_), null);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2FF;
					default:
						if (false)
						{
						}
						num = 28;
						continue;
					}
					break;
				case 6:
					goto IL_BB;
				case 7:
					if (!xlsPageSetup.IsSettingsNotValid)
					{
						num = 23;
						continue;
					}
					goto IL_493;
				case 9:
					goto IL_B6;
				case 10:
					goto IL_2E7;
				case 11:
					if (A_1.ViewMode == ViewMode.Preview)
					{
						num = 13;
						continue;
					}
					goto IL_1F4;
				case 12:
					goto IL_37F;
				case 13:
					A_0.WriteStartElement(RecordTableEnumerator.b("攵倷唹䬻渽ℿ╁⅃х㩇⽉ⵋ╍੏㵑㭓㭕", a_), RecordTableEnumerator.b("䌵䨷吹ػ䴽⌿⩁⅃⭅⥇㥉態⍍㥏ㅑ♓㥕⭗㕙㩛⩝䵟šୣ୥剧թ੫࡭᥯ᅱᅳ䱵ᵷɹύ᭽", a_));
					A_0.WriteEndElement();
					num = 4;
					continue;
				case 14:
					this.ᜀ(A_0, RecordTableEnumerator.b("丵", a_), RecordTableEnumerator.b("氵圷唹儻", a_), zoomScaleNormal.ToString());
					num = 24;
					continue;
				case 15:
					goto IL_328;
				case 16:
					this.ᜀ(A_0, RecordTableEnumerator.b("丵", a_), RecordTableEnumerator.b("戵夷堹缻儽ⰿⵁ㙃ཅ♇⹉⥋㙍", a_), tabKnownColor.ToString());
					num = 27;
					continue;
				case 17:
					if (zoomScalePageBreakView != 0)
					{
						num = 20;
						continue;
					}
					goto IL_BB;
				case 18:
					if (xlsPageSetup.IsFitToPage)
					{
						num = 25;
						continue;
					}
					goto IL_2EC;
				case 19:
					A_0.WriteStartElement(RecordTableEnumerator.b("爵儷䤹䰻刽ℿ㭁ᑃ❅⽇⽉๋㱍㕏㍑㽓", a_), RecordTableEnumerator.b("䌵䨷吹ػ䴽⌿⩁⅃⭅⥇㥉態⍍㥏ㅑ♓㥕⭗㕙㩛⩝䵟šୣ୥剧թ੫࡭᥯ᅱᅳ䱵ᵷɹύ᭽", a_));
					A_0.WriteEndElement();
					num = 26;
					continue;
				case 20:
					this.ᜀ(A_0, RecordTableEnumerator.b("丵", a_), RecordTableEnumerator.b("昵夷崹夻簽㈿❁╃ⵅቇ╉⍋⍍", a_), zoomScalePageBreakView.ToString());
					num = 6;
					continue;
				case 21:
					if (zoomScaleNormal != 0)
					{
						num = 14;
						continue;
					}
					goto IL_3F8;
				case 22:
					if (A_1.DisplayPageBreaks)
					{
						num = 19;
						continue;
					}
					goto IL_175;
				case 23:
					this.ᜀ(A_0, xlsPageSetup);
					num = 10;
					continue;
				case 24:
					goto IL_3F8;
				case 25:
					this.ᜀ(A_0, RecordTableEnumerator.b("丵", a_), RecordTableEnumerator.b("瀵儷丹栻儽ဿ⍁⍃⍅", a_));
					num = 29;
					continue;
				case 26:
					goto IL_175;
				case 27:
					goto IL_224;
				case 28:
					if (A_1.Visibility != WorksheetVisibility.Visible)
					{
						num = 0;
						continue;
					}
					goto IL_328;
				case 29:
					goto IL_2EC;
				}
				if (A_0 == null)
				{
					num = 9;
					continue;
				}
				num = 5;
				continue;
				IL_BB:
				this.ᜁ(A_0, xlsPageSetup);
				num = 22;
				continue;
				IL_175:
				num = 11;
				continue;
				IL_1F4:
				this.ᜁ(A_0, A_1);
				num = 7;
				continue;
				IL_224:
				zoomScaleNormal = A_1.ZoomScaleNormal;
				num = 3;
				continue;
				IL_2FF:
				if (tabKnownColor != -1)
				{
					num = 16;
					continue;
				}
				goto IL_224;
				IL_2EC:
				tabKnownColor = (int)A_1.TabKnownColor;
				num = 2;
				continue;
				IL_328:
				this.ᜀ(A_0, A_1);
				xlsPageSetup = (XlsPageSetup)A_1.PageSetup;
				num = 18;
				continue;
				IL_3F8:
				zoomScalePageBreakView = A_1.ZoomScalePageBreakView;
				num = 17;
			}
			IL_B6:
			throw new ArgumentNullException(RecordTableEnumerator.b("䄵䨷匹䠻嬽㈿", a_));
			IL_2E7:
			goto IL_493;
			IL_37F:
			throw new ArgumentNullException(RecordTableEnumerator.b("䔵倷弹夻䨽", a_));
			IL_493:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x060054A9 RID: 21673 RVA: 0x00350D80 File Offset: 0x0034FD80
	private void ᜁ(XmlWriter A_0, IPageSetup A_1)
	{
		int a_ = 10;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_34;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_81;
				default:
					if (false)
					{
					}
					if (A_1 == null)
					{
						num = 3;
						continue;
					}
					goto IL_A1;
				}
				break;
			case 3:
				goto IL_81;
			}
			if (A_0 == null)
			{
				num = 0;
			}
			else
			{
				num = 2;
			}
		}
		IL_34:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㜿ぁⵃ㉅ⵇ㡉", a_));
		IL_81:
		throw new ArgumentNullException(RecordTableEnumerator.b("〿⍁⍃⍅ᭇ⽉㡋㭍⁏", a_));
		IL_A1:
		XlsPageSetup a_2 = (XlsPageSetup)A_1;
		A_0.WriteStartElement(RecordTableEnumerator.b("㠿", a_), RecordTableEnumerator.b("ဿ⍁⍃⍅ᭇ⽉㡋㭍⁏", a_), null);
		this.ᜀ(A_0, a_2, true);
		this.ᜀ(A_0, a_2, false);
		this.ᜁ(A_0, a_2);
		this.ᜀ(A_0, a_2);
		A_0.WriteEndElement();
	}

	// Token: 0x060054AA RID: 21674 RVA: 0x00350E80 File Offset: 0x0034FE80
	private void ᜀ(XmlWriter A_0, XlsPageSetup A_1, bool A_2)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			int num = 24;
			for (;;)
			{
				double num2;
				string text;
				bool flag;
				bool flag2;
				bool flag3;
				string text2;
				string text3;
				switch (num)
				{
				case 0:
					num2 = A_1.FooterMarginInch;
					goto IL_12D;
				case 1:
					goto IL_2EF;
				case 2:
					text = A_1.FullFooterString;
					goto IL_1F1;
				case 3:
					num = 6;
					continue;
				case 4:
					flag = false;
					goto IL_1B1;
				case 5:
					num = 10;
					continue;
				case 6:
					if (flag2)
					{
						num = 25;
						continue;
					}
					return;
				case 7:
					if (true)
					{
					}
					text = A_1.FullHeaderString;
					goto IL_1F1;
				case 8:
					if (!A_2)
					{
						num = 5;
						continue;
					}
					num = 15;
					continue;
				case 9:
					if (!flag3)
					{
						num = 3;
						continue;
					}
					goto IL_31F;
				case 10:
					text2 = RecordTableEnumerator.b("眾⑀≂⅄≆㭈", a_);
					goto IL_212;
				case 11:
					if (!A_2)
					{
						num = 14;
						continue;
					}
					num = 0;
					continue;
				case 12:
					if (text3 != null)
					{
						num = 20;
						continue;
					}
					num = 4;
					continue;
				case 13:
					if (A_1 == null)
					{
						num = 1;
						continue;
					}
					num = 11;
					continue;
				case 14:
					num = 23;
					continue;
				case 15:
					text2 = RecordTableEnumerator.b("社⹀ⱂㅄ≆㭈", a_);
					goto IL_212;
				case 16:
					flag = (text3.Length > 0);
					goto IL_1B1;
				case 17:
					num = 7;
					continue;
				case 18:
					goto IL_2F1;
				case 19:
					if (!A_2)
					{
						num = 17;
						continue;
					}
					num = 2;
					continue;
				case 20:
					num = 16;
					continue;
				case 21:
					goto IL_AB;
				case 22:
					if (flag2)
					{
						num = 26;
						continue;
					}
					goto IL_2F1;
				case 23:
					num2 = A_1.HeaderMarginInch;
					goto IL_12D;
				case 25:
					goto IL_31F;
				case 26:
					A_0.WriteAttributeString(RecordTableEnumerator.b("䜾", a_), RecordTableEnumerator.b("笾⁀㝂⑄", a_), null, text3);
					num = 18;
					continue;
				case 27:
					return;
				}
				IL_99:
				if (A_0 == null)
				{
					num = 21;
					continue;
				}
				num = 13;
				continue;
				IL_12D:
				double num3 = num2;
				flag3 = (num3 != 0.5);
				num = 19;
				continue;
				IL_1F1:
				text3 = text;
				num = 12;
				continue;
				IL_212:
				string localName = text2;
				A_0.WriteStartElement(RecordTableEnumerator.b("䜾", a_), localName, null);
				A_0.WriteAttributeString(RecordTableEnumerator.b("䜾", a_), RecordTableEnumerator.b("爾⁀ㅂ≄⹆❈", a_), null, XmlConvert.ToString(num3));
				num = 22;
				continue;
				IL_2F1:
				A_0.WriteEndElement();
				num = 27;
				continue;
				IL_31F:
				num = 8;
				continue;
				IL_1B1:
				flag2 = flag;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_99;
				default:
					if (false)
					{
					}
					num = 9;
					break;
				}
			}
			IL_AB:
			throw new ArgumentNullException(RecordTableEnumerator.b("䠾㍀⩂ㅄ≆㭈", a_));
			IL_2EF:
			throw new ArgumentNullException(RecordTableEnumerator.b("伾⁀⑂⁄ᑆⱈ㽊㡌㽎", a_));
		}
		}
	}

	// Token: 0x060054AB RID: 21675 RVA: 0x003511EC File Offset: 0x003501EC
	private void ᜁ(XmlWriter A_0, XlsPageSetup A_1)
	{
		int a_ = 1;
		int num = 14;
		for (;;)
		{
			PageOrientationType orientation;
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_240;
				default:
					goto IL_111;
				}
				break;
			case 1:
				if (true)
				{
				}
				if (A_1.CenterHorizontally)
				{
					num = 12;
					continue;
				}
				goto IL_D8;
			case 2:
				if (orientation != PageOrientationType.Portrait)
				{
					num = 3;
					continue;
				}
				goto IL_69;
			case 3:
				A_0.WriteAttributeString(RecordTableEnumerator.b("伶", a_), RecordTableEnumerator.b("砶䬸刺堼儾㕀≂ㅄ⹆♈╊", a_), null, orientation.ToString());
				num = 7;
				continue;
			case 4:
				goto IL_98;
			case 5:
				goto IL_D3;
			case 6:
				if (!A_1.AutoFirstPageNumber)
				{
					num = 9;
					continue;
				}
				goto IL_98;
			case 7:
				goto IL_69;
			case 8:
				goto IL_D8;
			case 9:
				A_0.WriteAttributeString(RecordTableEnumerator.b("伶", a_), RecordTableEnumerator.b("搶䴸娺似䬾ᅀ≂≄≆݈㹊⁌ⵎ㑐⅒", a_), null, A_1.FirstPageNumber.ToString());
				num = 4;
				continue;
			case 10:
				goto IL_1A8;
			case 11:
				A_0.WriteAttributeString(RecordTableEnumerator.b("伶", a_), RecordTableEnumerator.b("琶尸唺䤼娾㍀ᕂ⁄㕆㵈≊⹌⹎㵐", a_), null, RecordTableEnumerator.b("ض", a_));
				num = 10;
				continue;
			case 12:
				A_0.WriteAttributeString(RecordTableEnumerator.b("伶", a_), RecordTableEnumerator.b("琶尸唺䤼娾㍀ୂ⩄㕆⁈ㅊ≌ⅎ═㉒㥔", a_), null, RecordTableEnumerator.b("ض", a_));
				num = 8;
				continue;
			case 13:
				if (A_1 == null)
				{
					num = 5;
					continue;
				}
				goto IL_240;
			case 15:
				if (A_1.CenterVertically)
				{
					num = 11;
					continue;
				}
				goto IL_290;
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			num = 13;
			continue;
			IL_69:
			num = 1;
			continue;
			IL_98:
			num = 2;
			continue;
			IL_D8:
			num = 15;
			continue;
			IL_240:
			orientation = A_1.Orientation;
			A_0.WriteStartElement(RecordTableEnumerator.b("伶", a_), RecordTableEnumerator.b("笶堸䈺刼䨾㕀", a_), null);
			num = 6;
		}
		IL_D3:
		throw new ArgumentNullException(RecordTableEnumerator.b("䜶堸尺堼氾⑀㝂い㝆", a_));
		IL_111:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䀶䬸刺䤼娾㍀", a_));
		IL_1A8:
		IL_290:
		A_0.WriteEndElement();
	}

	// Token: 0x060054AC RID: 21676 RVA: 0x00351490 File Offset: 0x00350490
	private void ᜀ(XmlWriter A_0, XlsPageSetup A_1)
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			int num = 5;
			for (;;)
			{
				double topMargin;
				switch (num)
				{
				case 0:
				{
					double rightMargin;
					if (rightMargin != 0.75)
					{
						num = 8;
						continue;
					}
					goto IL_C9;
				}
				case 1:
					goto IL_1C6;
				case 2:
					goto IL_118;
				case 3:
					goto IL_78;
				case 4:
				{
					double leftMargin;
					if (leftMargin != 0.75)
					{
						num = 9;
						continue;
					}
					goto IL_7D;
				}
				case 6:
				{
					if (A_1 == null)
					{
						num = 2;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("㵄", a_), RecordTableEnumerator.b("ᕄ♆⹈⹊L⹎⍐㑒㱔㥖⩘", a_), null);
					double rightMargin = A_1.RightMargin;
					double leftMargin = A_1.LeftMargin;
					topMargin = A_1.TopMargin;
					double bottomMargin = A_1.BottomMargin;
					num = 0;
					continue;
				}
				case 7:
					A_0.WriteAttributeString(RecordTableEnumerator.b("㵄", a_), RecordTableEnumerator.b("ᅄ⡆㥈", a_), null, XmlConvert.ToString(topMargin));
					num = 1;
					continue;
				case 8:
				{
					double rightMargin;
					A_0.WriteAttributeString(RecordTableEnumerator.b("㵄", a_), RecordTableEnumerator.b("ᝄ⹆⹈⍊㥌", a_), null, XmlConvert.ToString(rightMargin));
					num = 11;
					continue;
				}
				case 9:
				{
					if (true)
					{
					}
					double leftMargin;
					A_0.WriteAttributeString(RecordTableEnumerator.b("㵄", a_), RecordTableEnumerator.b("ॄ≆⽈㽊", a_), null, XmlConvert.ToString(leftMargin));
					num = 13;
					continue;
				}
				case 10:
				{
					double bottomMargin;
					if (bottomMargin != 1.0)
					{
						num = 15;
						continue;
					}
					goto IL_11D;
				}
				case 11:
					goto IL_C9;
				case 12:
					goto IL_129;
				case 13:
					goto IL_7D;
				case 14:
					goto IL_11D;
				case 15:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_129;
					default:
					{
						if (false)
						{
						}
						double bottomMargin;
						A_0.WriteAttributeString(RecordTableEnumerator.b("㵄", a_), RecordTableEnumerator.b("݄⡆㵈㽊≌≎", a_), null, XmlConvert.ToString(bottomMargin));
						num = 14;
						continue;
					}
					}
					break;
				}
				if (A_0 == null)
				{
					num = 3;
					continue;
				}
				num = 6;
				continue;
				IL_7D:
				num = 10;
				continue;
				IL_C9:
				num = 4;
				continue;
				IL_11D:
				num = 12;
				continue;
				IL_129:
				if (topMargin == 1.0)
				{
					goto IL_2BE;
				}
				num = 7;
			}
			IL_78:
			throw new ArgumentNullException(RecordTableEnumerator.b("㉄㕆⁈㽊⡌㵎", a_));
			IL_118:
			throw new ArgumentNullException(RecordTableEnumerator.b("㕄♆⹈⹊Ṍ⩎═♒╔", a_));
			IL_1C6:
			IL_2BE:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x060054AD RID: 21677 RVA: 0x00351764 File Offset: 0x00350764
	private void ᜁ(XmlWriter A_0, XlsWorksheet A_1)
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			int num = 5;
			for (;;)
			{
				sprṫ sprṫ;
				List<spr\u21A4> list;
				switch (num)
				{
				case 0:
					goto IL_131;
				case 1:
					if (sprṫ.ᜁ())
					{
						num = 17;
						continue;
					}
					goto IL_1FA;
				case 2:
					if (A_1.WindowTwo.ᜐ() != 0)
					{
						num = 22;
						continue;
					}
					goto IL_440;
				case 3:
				{
					int num2;
					if (num2 != 0)
					{
						num = 15;
						continue;
					}
					goto IL_AC;
				}
				case 4:
				{
					if (A_1 == null)
					{
						num = 18;
						continue;
					}
					spr\u2408 spr_u = A_1.Pane;
					list = A_1.Selections;
					num = 0;
					continue;
				}
				case 6:
				{
					spr\u2408 spr_u;
					if (spr_u == null)
					{
						num = 21;
						continue;
					}
					int num2 = spr_u.ᜄ();
					int num3 = spr_u.ᜃ();
					num = 2;
					continue;
				}
				case 7:
				{
					int num3;
					if (num3 != 0)
					{
						num = 8;
						continue;
					}
					goto IL_148;
				}
				case 8:
				{
					int num3;
					this.ᜀ(A_0, RecordTableEnumerator.b("䌺", a_), RecordTableEnumerator.b("栺䴼匾⡀㝂ፄ≆㭈㽊⑌ⱎぐ㽒", a_), num3.ToString());
					spr\u2408 spr_u;
					this.ᜀ(A_0, RecordTableEnumerator.b("䌺", a_), RecordTableEnumerator.b("眺堼夾㕀B⩄⭆㱈♊⍌ᵎ㡐㑒㵔⍖क़㩚㍜㩞", a_), spr_u.ᜅ().ToString());
					num = 11;
					continue;
				}
				case 9:
					goto IL_2F9;
				case 10:
					goto IL_2F4;
				case 11:
					goto IL_148;
				case 12:
					if (list.Count > 1)
					{
						num = 25;
						continue;
					}
					goto IL_2F9;
				case 13:
					goto IL_A7;
				case 14:
					goto IL_3B0;
				case 15:
				{
					int num2;
					this.ᜀ(A_0, RecordTableEnumerator.b("䌺", a_), RecordTableEnumerator.b("栺䴼匾⡀㝂ൄ⡆㭈≊㝌⁎㽐❒㑔㭖", a_), num2.ToString());
					spr\u2408 spr_u;
					this.ᜀ(A_0, RecordTableEnumerator.b("䌺", a_), RecordTableEnumerator.b("漺刼伾ፀⱂ㉄Ն♈㽊㥌⁎㱐͒㑔㥖㱘", a_), spr_u.ᜀ().ToString());
					num = 19;
					continue;
				}
				case 16:
					this.ᜀ(A_0, RecordTableEnumerator.b("䌺", a_), RecordTableEnumerator.b("紺似倾㭀♂⭄ॆ♈ᡊ㵌⍎㡐❒", a_));
					num = 10;
					continue;
				case 17:
					this.ᜀ(A_0, RecordTableEnumerator.b("䌺", a_), RecordTableEnumerator.b("紺似娾⑀㥂⁄ᝆ⡈╊⡌㱎", a_));
					num = 26;
					continue;
				case 18:
					goto IL_3AB;
				case 19:
					goto IL_AC;
				case 20:
					if (sprṫ.ᜀ())
					{
						num = 16;
						continue;
					}
					return;
				case 21:
					return;
				case 22:
					this.ᜀ(A_0, RecordTableEnumerator.b("䌺", a_), RecordTableEnumerator.b("漺刼伾ፀⱂ㉄ᅆ⁈㡊⑌ⵎ㵐㙒", a_), A_1.WindowTwo.ᜐ().ToString());
					num = 23;
					continue;
				case 23:
					goto IL_440;
				case 24:
					num = 12;
					continue;
				case 25:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_131;
					default:
					{
						if (false)
						{
						}
						spr\u2408 spr_u;
						this.ᜀ(A_0, RecordTableEnumerator.b("䌺", a_), RecordTableEnumerator.b("稺帼䬾⡀㕂⁄ᝆ⡈╊⡌", a_), spr_u.ᜆ().ToString());
						num = 9;
						continue;
					}
					}
					break;
				case 26:
					goto IL_1FA;
				}
				if (A_0 == null)
				{
					num = 13;
					continue;
				}
				if (true)
				{
				}
				num = 4;
				continue;
				IL_AC:
				num = 7;
				continue;
				IL_131:
				if (list != null)
				{
					num = 24;
					continue;
				}
				goto IL_3B0;
				IL_148:
				sprṫ = A_1.WindowTwo;
				num = 1;
				continue;
				IL_1FA:
				num = 20;
				continue;
				IL_2F9:
				this.ᜀ(A_0, list);
				num = 14;
				continue;
				IL_3B0:
				num = 6;
				continue;
				IL_440:
				num = 3;
			}
			IL_A7:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰺似嘾㕀♂㝄", a_));
			IL_2F4:
			return;
			IL_3AB:
			throw new ArgumentNullException(RecordTableEnumerator.b("䠺唼娾⑀㝂", a_));
		}
		}
	}

	// Token: 0x060054AE RID: 21678 RVA: 0x00351BD8 File Offset: 0x00350BD8
	private void ᜀ(XmlWriter A_0, List<spr\u21A4> A_1)
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			int num = 14;
			for (;;)
			{
				spr\u21A4 spr_u21A;
				int num2;
				switch (num)
				{
				case 0:
					goto IL_189;
				case 1:
					goto IL_78;
				case 2:
					goto IL_1CE;
				case 3:
					this.ᜀ(A_0, RecordTableEnumerator.b("䌺", a_), RecordTableEnumerator.b("稺帼䬾⡀㕂⁄ц♈❊", a_), spr_u21A.ᜁ().ToString());
					num = 13;
					continue;
				case 4:
					if (spr_u21A.ᜁ() != 0)
					{
						num = 3;
						continue;
					}
					goto IL_7D;
				case 5:
					goto IL_2C8;
				case 6:
				{
					int count;
					if (num2 >= count)
					{
						num = 12;
						continue;
					}
					spr_u21A = A_1[num2];
					A_0.WriteStartElement(RecordTableEnumerator.b("䌺", a_), RecordTableEnumerator.b("欺尼儾⑀", a_), null);
					this.ᜀ(A_0, RecordTableEnumerator.b("䌺", a_), RecordTableEnumerator.b("町䠼刾⍀♂㝄", a_), spr_u21A.ᜀ().ToString());
					num = 4;
					continue;
				}
				case 7:
					goto IL_21A;
				case 8:
					if (spr_u21A.ᜂ() == 0)
					{
						goto IL_153;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_21A;
					default:
						if (false)
						{
						}
						num = 7;
						continue;
					}
					break;
				case 9:
				{
					int count;
					if (count > 4)
					{
						num = 5;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("䌺", a_), RecordTableEnumerator.b("欺尼儾⑀あ", a_), null);
					num2 = 0;
					num = 2;
					continue;
				}
				case 10:
					goto IL_153;
				case 11:
				{
					if (A_1 == null)
					{
						num = 0;
						continue;
					}
					if (true)
					{
					}
					int count = A_1.Count;
					num = 9;
					continue;
				}
				case 12:
					goto IL_1ED;
				case 13:
					goto IL_7D;
				case 15:
					goto IL_1CE;
				}
				if (A_0 == null)
				{
					num = 1;
					continue;
				}
				num = 11;
				continue;
				IL_7D:
				num = 8;
				continue;
				IL_153:
				A_0.WriteEndElement();
				num2++;
				num = 15;
				continue;
				IL_1CE:
				num = 6;
				continue;
				IL_21A:
				this.ᜀ(A_0, RecordTableEnumerator.b("䌺", a_), RecordTableEnumerator.b("稺帼䬾⡀㕂⁄ᕆ♈㱊", a_), spr_u21A.ᜂ().ToString());
				num = 10;
			}
			IL_78:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰺似嘾㕀♂㝄", a_));
			IL_189:
			throw new ArgumentNullException(RecordTableEnumerator.b("娺似䴾ቀ♂⥄≆⩈㽊⑌⁎㽐", a_));
			IL_1ED:
			A_0.WriteEndElement();
			return;
			IL_2C8:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("稺似䴾⁀㩂敄⑆⡈╊⍌⁎═獒㙔㡖㝘⽚㱜㙞འ䍢ࡤࡦ᭨๪䵬᭮ᥰቲ᭴坶䵸孺๼᩾꾎ﾚ", a_));
		}
		}
	}

	// Token: 0x060054AF RID: 21679 RVA: 0x00351EB8 File Offset: 0x00350EB8
	private void ᜀ(XmlWriter A_0, IPageSetup A_1)
	{
		int a_ = 7;
		switch (0)
		{
		default:
		{
			int num = 26;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_1.BlackAndWhite)
					{
						num = 28;
						continue;
					}
					goto IL_212;
				case 1:
					if (A_1.PrintComments != PrintCommentType.NoComments)
					{
						num = 9;
						continue;
					}
					goto IL_2F2;
				case 2:
					goto IL_2C9;
				case 3:
					this.ᜀ(A_0, RecordTableEnumerator.b("䔼", a_), RecordTableEnumerator.b("甼倾㍀⩂㽄⡆❈㽊ⱌ⍎͐㙒♔㡖㕘⹚⥜㙞๠ൢ", a_), A_1.PrintQuality.ToString());
					num = 2;
					continue;
				case 4:
				{
					if (A_1 == null)
					{
						num = 39;
						continue;
					}
					int paperSize = (int)A_1.PaperSize;
					A_0.WriteStartElement(RecordTableEnumerator.b("䔼", a_), RecordTableEnumerator.b("洼䴾⡀ⵂㅄ", a_), null);
					num = 21;
					continue;
				}
				case 5:
					this.ᜀ(A_0, RecordTableEnumerator.b("䔼", a_), RecordTableEnumerator.b("漼倾㙀B⩄⭆ň⹊ⱌ⭎㡐㵒㉔⑖", a_));
					num = 10;
					continue;
				case 6:
					num = 24;
					continue;
				case 7:
				{
					int paperSize;
					this.ᜀ(A_0, RecordTableEnumerator.b("䔼", a_), RecordTableEnumerator.b("洼帾ㅀ♂㝄ᑆ⁈ㅊ⡌َ㽐㝒ご⽖", a_), paperSize.ToString());
					num = 16;
					continue;
				}
				case 8:
					if (A_1.Zoom != 100)
					{
						num = 13;
						continue;
					}
					goto IL_12B;
				case 9:
				{
					int printComments = (int)A_1.PrintComments;
					this.ᜀ(A_0, RecordTableEnumerator.b("䔼", a_), RecordTableEnumerator.b("縼倾ⱀ⹂⁄⥆㵈㡊Ō⹎⡐㱒⁔⍖", a_), sprỉ.\u17ED[printComments]);
					num = 35;
					continue;
				}
				case 10:
					goto IL_196;
				case 11:
					goto IL_F2;
				case 12:
					this.ᜀ(A_0, RecordTableEnumerator.b("䔼", a_), RecordTableEnumerator.b("焼娾❀㝂ᅄ⡆ᭈ≊⩌❎═", a_));
					num = 22;
					continue;
				case 13:
					this.ᜀ(A_0, RecordTableEnumerator.b("䔼", a_), RecordTableEnumerator.b("渼尾⁀⽂⁄", a_), A_1.Zoom.ToString());
					num = 43;
					continue;
				case 14:
					if (A_1.FitToPagesTall != 1)
					{
						num = 37;
						continue;
					}
					goto IL_12B;
				case 15:
					this.ᜀ(A_0, RecordTableEnumerator.b("䔼", a_), RecordTableEnumerator.b("礼䴾⁀╂ㅄᙆ㱈⩊⅌♎═⩒", a_));
					num = 41;
					continue;
				case 16:
					goto IL_419;
				case 17:
					if (A_1.IsPrintGridlines)
					{
						num = 18;
						continue;
					}
					goto IL_530;
				case 18:
					this.ᜀ(A_0, RecordTableEnumerator.b("䔼", a_), RecordTableEnumerator.b("稼䴾⡀❂⥄⹆❈⹊㹌", a_));
					num = 23;
					continue;
				case 19:
					if (A_1.Draft)
					{
						num = 15;
						continue;
					}
					goto IL_483;
				case 20:
					goto IL_2D5;
				case 21:
					if (A_1.Copies != 1)
					{
						num = 32;
						continue;
					}
					goto IL_3A5;
				case 22:
					goto IL_349;
				case 23:
					goto IL_530;
				case 24:
					if (A_1.FitToPagesWide != 1)
					{
						num = 36;
						continue;
					}
					goto IL_4C7;
				case 25:
					if (A_1.IsFitToPage)
					{
						num = 6;
						continue;
					}
					num = 8;
					continue;
				case 27:
					if (A_1.PrintQuality <= 32767)
					{
						num = 3;
						continue;
					}
					goto IL_2C9;
				case 28:
					this.ᜀ(A_0, RecordTableEnumerator.b("䔼", a_), RecordTableEnumerator.b("缼匾⁀⁂⹄ن❈⽊ᩌ❎㡐❒ご", a_));
					num = 44;
					continue;
				case 29:
					goto IL_6B9;
				case 30:
					if (A_1.Order == OrderType.OverThenDown)
					{
						num = 12;
						continue;
					}
					goto IL_6DF;
				case 31:
					goto IL_12B;
				case 32:
					this.ᜀ(A_0, RecordTableEnumerator.b("䔼", a_), RecordTableEnumerator.b("猼䨾ⱀ⅂⁄㕆♈ⵊ์⁎⅐㩒ご⑖", a_), A_1.Copies.ToString());
					num = 42;
					continue;
				case 33:
					goto IL_4C7;
				case 34:
				{
					int printErrors = (int)A_1.PrintErrors;
					this.ᜀ(A_0, RecordTableEnumerator.b("䔼", a_), RecordTableEnumerator.b("洼䴾⡀ⵂㅄɆ㭈㥊≌㵎≐", a_), sprỉ.\u17EE[printErrors]);
					num = 29;
					continue;
				}
				case 35:
					goto IL_2F2;
				case 36:
					this.ᜀ(A_0, RecordTableEnumerator.b("䔼", a_), RecordTableEnumerator.b("笼嘾㕀ᑂⱄ⍆㵈⍊", a_), A_1.FitToPagesWide.ToString());
					num = 33;
					continue;
				case 37:
					this.ᜀ(A_0, RecordTableEnumerator.b("䔼", a_), RecordTableEnumerator.b("笼嘾㕀ୂ⁄⹆⹈⍊㥌", a_), A_1.FitToPagesTall.ToString());
					num = 31;
					continue;
				case 38:
					if (A_1.PrintErrors != PrintErrorsType.Displayed)
					{
						num = 34;
						continue;
					}
					goto IL_6B9;
				case 39:
					goto IL_64C;
				case 40:
					if (!A_1.IsPrintHeadings)
					{
						goto IL_196;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2D5;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 41:
					goto IL_483;
				case 42:
					if (true)
					{
					}
					goto IL_3A5;
				case 43:
					goto IL_12B;
				case 44:
					goto IL_212;
				}
				if (A_0 == null)
				{
					num = 11;
					continue;
				}
				num = 4;
				continue;
				IL_12B:
				num = 17;
				continue;
				IL_196:
				num = 1;
				continue;
				IL_212:
				num = 19;
				continue;
				IL_2C9:
				num = 20;
				continue;
				IL_2D5:
				if (A_1.PaperSize != PaperSizeType.PaperLetter)
				{
					num = 7;
					continue;
				}
				goto IL_419;
				IL_2F2:
				num = 38;
				continue;
				IL_3A5:
				num = 27;
				continue;
				IL_419:
				num = 25;
				continue;
				IL_483:
				num = 40;
				continue;
				IL_4C7:
				num = 14;
				continue;
				IL_530:
				num = 0;
				continue;
				IL_6B9:
				num = 30;
			}
			IL_F2:
			throw new ArgumentNullException(RecordTableEnumerator.b("䨼䴾⡀㝂⁄㕆", a_));
			IL_349:
			goto IL_6DF;
			IL_64C:
			throw new ArgumentNullException(RecordTableEnumerator.b("䴼帾♀♂ᙄ≆㵈㹊㵌", a_));
			IL_6DF:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x060054B0 RID: 21680 RVA: 0x003525AC File Offset: 0x003515AC
	private void ᜀ(XmlWriter A_0, XlsWorksheet A_1)
	{
		int a_ = 8;
		int num = 8;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				sprṫ sprṫ;
				if (sprṫ.\u1712())
				{
					num = 13;
					continue;
				}
				return;
			}
			case 1:
				this.ᜀ(A_0, RecordTableEnumerator.b("䘽", a_), RecordTableEnumerator.b("稽⼿ు⭃㉅ే⍉㽋㹍㱏㍑ⵓṕ㵗㭙㡛㝝๟աᝣ", a_));
				num = 5;
				continue;
			case 2:
				goto IL_17F;
			case 3:
			{
				if (A_1 == null)
				{
					num = 6;
					continue;
				}
				sprṫ sprṫ = A_1.WindowTwo;
				num = 10;
				continue;
			}
			case 4:
			{
				sprṫ sprṫ;
				if (!sprṫ.ᜏ())
				{
					num = 1;
					continue;
				}
				goto IL_EA;
			}
			case 5:
				goto IL_EA;
			case 6:
				goto IL_E5;
			case 7:
				goto IL_89;
			case 9:
			{
				sprṫ sprṫ;
				if (!sprṫ.\u1713())
				{
					num = 14;
					continue;
				}
				goto IL_89;
			}
			case 10:
			{
				sprṫ sprṫ;
				if (sprṫ == null)
				{
					num = 12;
					continue;
				}
				num = 9;
				continue;
			}
			case 11:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_58;
				default:
					goto IL_76;
				}
				break;
			case 12:
				return;
			case 13:
				this.ᜀ(A_0, RecordTableEnumerator.b("䘽", a_), RecordTableEnumerator.b("洽┿⹁⅃╅㱇⽉⡋", a_));
				num = 2;
				continue;
			case 14:
				this.ᜀ(A_0, RecordTableEnumerator.b("䘽", a_), RecordTableEnumerator.b("稽⼿ు⭃㉅ే⍉㽋㹍㱏㍑ⵓᅕ⩗㍙㡛㉝य़ౡţᕥ", a_));
				num = 7;
				continue;
			}
			goto IL_55;
			IL_58:
			num = 11;
			continue;
			IL_55:
			if (A_0 == null)
			{
				goto IL_58;
			}
			num = 3;
			continue;
			IL_89:
			num = 4;
			continue;
			IL_EA:
			num = 0;
		}
		IL_76:
		if (true)
		{
		}
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䤽㈿⭁ぃ⍅㩇", a_));
		IL_E5:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴽⠿❁⅃㉅", a_));
		IL_17F:;
	}

	// Token: 0x060054B1 RID: 21681 RVA: 0x003527B4 File Offset: 0x003517B4
	private void ᜀ(XmlWriter A_0, XlsWorkbook A_1)
	{
		int a_ = 8;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_17F;
			case 1:
			{
				int count;
				this.ᜀ(A_0, RecordTableEnumerator.b("䘽", a_), RecordTableEnumerator.b("洽┿⹁⅃╅㱇⽉⡋ᵍ㡏㝑ㅓ≕⭗", a_), count.ToString());
				num = 7;
				continue;
			}
			case 2:
				goto IL_4F;
			case 4:
			{
				int index;
				this.ᜀ(A_0, RecordTableEnumerator.b("䘽", a_), RecordTableEnumerator.b("缽⌿㙁ⵃぅⵇ᥉⑋⭍㕏♑", a_), index.ToString());
				this.ᜀ(A_0, RecordTableEnumerator.b("䘽", a_), RecordTableEnumerator.b("砽⤿ぁ㝃㉅ṇ⍉㽋❍㉏㹑ㅓՕし㽙㥛⩝", a_), A_1.DisplayedTab.ToString());
				num = 0;
				continue;
			}
			case 5:
			{
				if (A_1 == null)
				{
					num = 9;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("䘽", a_), RecordTableEnumerator.b("笽㠿⅁⅃⩅὇╉㹋╍㉏㵑㭓㵕", a_), null);
				int index = A_1.ActiveSheet.Index;
				int count = A_1.WorksheetGroup.Count;
				num = 6;
				continue;
			}
			case 6:
			{
				int index;
				if (index > 0)
				{
					num = 4;
					continue;
				}
				goto IL_17F;
			}
			case 7:
				goto IL_169;
			case 8:
			{
				int count;
				if (count > 1)
				{
					num = 1;
					continue;
				}
				goto IL_1D5;
			}
			case 9:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_1BB;
				}
				break;
			}
			IL_41:
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			num = 5;
			continue;
			goto IL_41;
			IL_17F:
			num = 8;
		}
		IL_4F:
		throw new ArgumentNullException(RecordTableEnumerator.b("䤽㈿⭁ぃ⍅㩇", a_));
		IL_169:
		goto IL_1D5;
		IL_1BB:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("尽⼿ⵁ⽃", a_));
		IL_1D5:
		if (true)
		{
		}
		A_0.WriteEndElement();
	}

	// Token: 0x060054B2 RID: 21682 RVA: 0x003529A4 File Offset: 0x003519A4
	private void ᜁ(XmlWriter A_0, IWorkbook A_1)
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
		throw new NotImplementedException();
	}

	// Token: 0x060054B3 RID: 21683 RVA: 0x003529E4 File Offset: 0x003519E4
	private void ᜀ(XmlWriter A_0, IWorkbook A_1)
	{
		int a_ = 3;
		int num;
		List<spr\u192F> a_2;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_20B:
			if (A_1 == null)
			{
				num = 2;
			}
			else
			{
				XlsWorkbook xlsWorkbook = (XlsWorkbook)A_1;
				this.\u17F2.Clear();
				A_0.WriteStartElement(RecordTableEnumerator.b("游吺似吾⍀ⱂ⩄ⱆ", a_), null);
				A_0.WriteAttributeString(RecordTableEnumerator.b("䄸嘺儼儾㉀", a_), null, null, RecordTableEnumerator.b("䰸䤺匼Ծ㉀⁂ⵄ≆⑈⩊㹌扎㱐㩒㙔╖㙘⡚㉜㥞ᕠ乢٤ࡦѨ兪ɬ८ᝰᩲᙴቶ䍸ࡺർൾﮎ", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("䄸嘺儼儾㉀", a_), RecordTableEnumerator.b("䨸䠺", a_), null, RecordTableEnumerator.b("䰸䤺匼Ծ㉀⁂ⵄ≆⑈⩊㹌扎㱐㩒㙔╖㙘⡚㉜㥞ᕠ乢٤ࡦѨ兪ɬ८ᝰᩲᙴቶ䍸ࡺർൾﮎ", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("䄸嘺儼儾㉀", a_), RecordTableEnumerator.b("䄸", a_), null, RecordTableEnumerator.b("䰸䤺匼Ծ㉀⁂ⵄ≆⑈⩊㹌扎㱐㩒㙔╖㙘⡚㉜㥞ᕠ乢٤ࡦѨ兪ɬ८ᝰᩲᙴቶ䍸Ṻռ᱾", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("䄸嘺儼儾㉀", a_), RecordTableEnumerator.b("嘸", a_), null, RecordTableEnumerator.b("䰸䤺匼Ծ㉀⁂ⵄ≆⑈⩊㹌扎㱐㩒㙔╖㙘⡚㉜㥞ᕠ乢٤ࡦѨ兪ɬ८ᝰᩲᙴቶ䍸ᑺ᭼᥾", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("䄸嘺儼儾㉀", a_), RecordTableEnumerator.b("儸伺值匾", a_), null, RecordTableEnumerator.b("儸伺䤼伾筀求橄う㹈㱊捌㡎扐絒㩔╖㹘瑚ड़൞习ㅢ⁤⑦䑨ͪᥬɮᵰ䝲䕴", a_));
				a_2 = this.ᜀ(A_1.Worksheets);
				if (true)
				{
				}
				num = 4;
			}
			break;
		case 1:
			goto IL_29;
		default:
			goto IL_29;
		}
		for (;;)
		{
			IL_39:
			switch (num)
			{
			case 0:
				this.ᜀ(A_0, ((XlsWorkbook)A_1).InnerExtFormats, a_2);
				num = 6;
				continue;
			case 2:
				goto IL_21C;
			case 3:
				goto IL_20B;
			case 4:
				if (A_1.Styles.Count > 0)
				{
					num = 0;
					continue;
				}
				goto IL_21E;
			case 5:
				goto IL_69;
			case 6:
				goto IL_1FE;
			}
			if (A_0 == null)
			{
				num = 5;
			}
			else
			{
				num = 3;
			}
		}
		IL_69:
		throw new ArgumentNullException(RecordTableEnumerator.b("丸䤺吼䬾⑀ㅂ", a_));
		IL_1FE:
		goto IL_21E;
		IL_21C:
		throw new ArgumentNullException(RecordTableEnumerator.b("嬸吺刼吾", a_));
		IL_21E:
		this.ᜀ(A_0, (XlsWorkbook)A_1);
		this.ᜀ(A_0, A_1.Names, false);
		this.ᜀ(A_0, A_1.Worksheets);
		this.\u17F2.Clear();
		A_0.WriteEndElement();
		return;
		IL_29:
		if (false)
		{
		}
		num = 1;
		goto IL_39;
	}

	// Token: 0x060054B4 RID: 21684 RVA: 0x00352C48 File Offset: 0x00351C48
	public void ᜂ(XmlWriter A_0, IWorkbook A_1)
	{
		int a_ = 13;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_80:
			if (A_1 != null)
			{
				A_0.WriteRaw(RecordTableEnumerator.b("罂穄㽆⑈❊浌㥎㑐⅒♔㹖㙘㕚恜絞偠䵢啤䕦器啪", a_));
				A_0.WriteRaw(RecordTableEnumerator.b("罂穄⩆㩈⑊恌⹎⅐⍒㥔㹖㩘㩚⥜㙞๠ൢ䕤ᝦ᭨Ѫ੬ٮᕰ乲坴㉶Ÿ᡺᡼፾꾀킂ﾊ꾌낎꾐", a_));
				this.ᜀ(A_0, A_1);
				return;
			}
			num = 1;
			break;
		default:
			if (false)
			{
			}
			num = 2;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_62;
			case 1:
				goto IL_8B;
			case 2:
				if (true)
				{
				}
				break;
			case 3:
				goto IL_80;
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
		IL_62:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑂㝄⹆㵈⹊㽌", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("⅂⩄⡆≈", a_));
	}

	// Token: 0x060054B5 RID: 21685 RVA: 0x00352D28 File Offset: 0x00351D28
	private StringBuilder ᜁ()
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
		return this.ᜀ(ref this.\u17F3);
	}

	// Token: 0x060054B6 RID: 21686 RVA: 0x00352D70 File Offset: 0x00351D70
	private StringBuilder ᜀ()
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
		return this.ᜀ(ref this.\u17F4);
	}

	// Token: 0x060054B7 RID: 21687 RVA: 0x00352DB8 File Offset: 0x00351DB8
	private StringBuilder ᜀ(ref StringBuilder A_0)
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
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					A_0 = new StringBuilder();
					num = 2;
					continue;
				case 1:
					goto IL_64;
				case 2:
					goto IL_7D;
				}
				if (A_0 == null)
				{
					num = 0;
				}
				else
				{
					A_0.Length = 0;
					num = 1;
				}
			}
			IL_7D:
			break;
		}
		}
		IL_64:
		return A_0;
	}

	// Token: 0x060054B8 RID: 21688 RVA: 0x00352E48 File Offset: 0x00351E48
	private string ᜀ(Color A_0)
	{
		int a_ = 3;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		string str = RecordTableEnumerator.b("ᨸ", a_);
		return str + (A_0.ToArgb() & 16777215).ToString(RecordTableEnumerator.b("愸ഺ", a_));
	}

	// Token: 0x060054B9 RID: 21689 RVA: 0x00352EC8 File Offset: 0x00351EC8
	private void ᜁ(string A_0, string A_1, StringBuilder A_2, StringBuilder A_3)
	{
		int a_ = 14;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_80:
			if (A_3 != null)
			{
				A_2.Append(A_0);
				A_3.Insert(0, A_1);
				return;
			}
			num = 1;
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
				goto IL_8C;
			case 2:
				goto IL_80;
			case 3:
				goto IL_5A;
			}
			if (A_2 == null)
			{
				num = 3;
			}
			else
			{
				num = 2;
			}
		}
		IL_5A:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("♃㍅ⅇ♉⡋⭍≏ő⁓㝕⩗⹙", a_));
		IL_8C:
		throw new ArgumentNullException(RecordTableEnumerator.b("♃㍅ⅇ♉⡋⭍≏ᝑ㩓㉕", a_));
	}

	// Token: 0x060054BA RID: 21690 RVA: 0x00352F8C File Offset: 0x00351F8C
	private void ᜀ(string A_0, string A_1, StringBuilder A_2, StringBuilder A_3)
	{
		int a_ = 1;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_78:
			if (A_3 != null)
			{
				A_2.Append(RecordTableEnumerator.b("᜶", a_));
				A_2.Append(A_0);
				A_2.Append(RecordTableEnumerator.b("ਸ਼ᬸ", a_));
				A_2.Append(A_1);
				A_2.Append(RecordTableEnumerator.b("ᔶᤸ", a_));
				return;
			}
			if (true)
			{
			}
			num = 3;
			break;
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
				goto IL_78;
			case 2:
				goto IL_5A;
			case 3:
				goto IL_8C;
			}
			if (A_2 == null)
			{
				num = 2;
			}
			else
			{
				num = 0;
			}
		}
		IL_5A:
		throw new ArgumentNullException(RecordTableEnumerator.b("唶䰸刺儼嬾⑀ㅂᙄ㍆⡈㥊㥌", a_));
		IL_8C:
		throw new ArgumentNullException(RecordTableEnumerator.b("唶䰸刺儼嬾⑀ㅂD⥆ⵈ", a_));
	}

	// Token: 0x060054BB RID: 21691 RVA: 0x0035308C File Offset: 0x0035208C
	private string ᜀ(IConditionalFormat A_0)
	{
		int a_ = 15;
		int num = 8;
		for (;;)
		{
			string text;
			string text2;
			string str;
			switch (num)
			{
			case 0:
				goto IL_DD;
			case 1:
				goto IL_54B;
			case 2:
				text = RecordTableEnumerator.b("牄睆祈", a_);
				goto IL_35E;
			case 3:
				text2 += this.ᜀ(RecordTableEnumerator.b("㝄⹆⹈⍊㥌", a_), A_0.RightBorderColor, A_0.RightBorderStyle);
				num = 18;
				continue;
			case 4:
				goto IL_525;
			case 5:
				if (A_0.IsBorderFormatPresent)
				{
					num = 28;
					continue;
				}
				return text2;
			case 6:
				num = 34;
				continue;
			case 7:
				goto IL_1F4;
			case 9:
				if (A_0.IsRightBorderModified)
				{
					num = 3;
					continue;
				}
				return text2;
			case 10:
				if (A_0.IsBottomBorderModified)
				{
					num = 19;
					continue;
				}
				goto IL_1F4;
			case 11:
				text2 += this.ᜀ(RecordTableEnumerator.b("ㅄ⡆㥈", a_), A_0.TopBorderColor, A_0.TopBorderStyle);
				num = 1;
				continue;
			case 12:
				str = this.ᜀ(A_0.FontColor);
				text2 = text2 + RecordTableEnumerator.b("♄⡆╈⑊㽌畎", a_) + str + RecordTableEnumerator.b("繄", a_);
				num = 21;
				continue;
			case 13:
				if (A_0.IsItalic)
				{
					num = 36;
					continue;
				}
				goto IL_435;
			case 14:
				if (A_0.IsLeftBorderModified)
				{
					num = 31;
					continue;
				}
				goto IL_177;
			case 15:
				if (A_0.IsFontColorPresent)
				{
					num = 12;
					continue;
				}
				goto IL_151;
			case 16:
				num = 30;
				continue;
			case 17:
				goto IL_177;
			case 18:
				return text2;
			case 19:
				text2 += this.ᜀ(RecordTableEnumerator.b("❄⡆㵈㽊≌≎", a_), A_0.BottomBorderColor, A_0.BottomBorderStyle);
				num = 7;
				continue;
			case 20:
				goto IL_435;
			case 21:
				goto IL_151;
			case 22:
				goto IL_490;
			case 23:
				if (A_0.IsPatternFormatPresent)
				{
					num = 6;
					continue;
				}
				goto IL_3E0;
			case 24:
				if (A_0.IsStrikeThrough)
				{
					num = 35;
					continue;
				}
				goto IL_525;
			case 25:
				if (A_0.IsTopBorderModified)
				{
					num = 11;
					continue;
				}
				goto IL_54B;
			case 26:
				if (!A_0.IsBold)
				{
					num = 16;
					continue;
				}
				num = 2;
				continue;
			case 27:
				str = this.ᜀ(A_0.BackColor);
				text2 = text2 + RecordTableEnumerator.b("❄♆⩈⁊⩌㵎㹐♒㭔㍖捘", a_) + str + RecordTableEnumerator.b("繄", a_);
				num = 22;
				continue;
			case 28:
				goto IL_EA;
			case 29:
				goto IL_3E0;
			case 30:
				text = RecordTableEnumerator.b("煄睆祈", a_);
				goto IL_35E;
			case 31:
				text2 += this.ᜀ(RecordTableEnumerator.b("⥄≆⽈㽊", a_), A_0.LeftBorderColor, A_0.LeftBorderStyle);
				num = 17;
				continue;
			case 32:
				num = 15;
				continue;
			case 33:
				if (A_0.IsFontFormatPresent)
				{
					num = 32;
					continue;
				}
				goto IL_525;
			case 34:
				if (A_0.IsBackgroundColorPresent)
				{
					num = 27;
					continue;
				}
				goto IL_490;
			case 35:
				text2 += RecordTableEnumerator.b("ㅄ≆ㅈ㽊恌⍎㡐㵒ご穖ⵘ㍚⽜ぞᑠѢ൤嵦ᩨɪͬ࡮ᵰᙲ乴", a_);
				num = 4;
				continue;
			case 36:
				text2 += RecordTableEnumerator.b("⍄⡆❈㽊恌㱎═⩒㥔㉖捘㉚⥜㹞ൠ੢٤屦", a_);
				num = 20;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_EA:
				num = 25;
				continue;
			default:
				if (false)
				{
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				text2 = "";
				num = 33;
				continue;
			}
			IL_151:
			num = 26;
			continue;
			IL_177:
			num = 10;
			continue;
			IL_1F4:
			num = 9;
			continue;
			IL_35E:
			str = text;
			text2 = text2 + RecordTableEnumerator.b("⍄⡆❈㽊恌㡎㑐㩒㉔㽖ⵘ慚", a_) + str + RecordTableEnumerator.b("繄", a_);
			num = 13;
			continue;
			IL_3E0:
			num = 5;
			continue;
			IL_435:
			str = A_0.Underline.ToString();
			text2 = text2 + RecordTableEnumerator.b("ㅄ≆ㅈ㽊恌㩎㽐㝒ご╖㕘㉚㍜㩞䱠ၢᅤṦը๪坬", a_) + str + RecordTableEnumerator.b("繄", a_);
			num = 24;
			continue;
			IL_490:
			int fillPattern = (int)A_0.FillPattern;
			str = this.៩[fillPattern] + RecordTableEnumerator.b("敄", a_) + this.ᜀ(A_0.Color);
			text2 = text2 + RecordTableEnumerator.b("⡄㑆♈晊㵌⹎═❒ご╖㝘慚", a_) + str + RecordTableEnumerator.b("繄", a_);
			num = 29;
			continue;
			IL_525:
			num = 23;
			continue;
			IL_54B:
			num = 14;
		}
		IL_DD:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("♄⡆❈⽊", a_));
	}

	// Token: 0x060054BC RID: 21692 RVA: 0x0035360C File Offset: 0x0035260C
	private string ᜀ(string A_0, Color A_1, LineStyleType A_2)
	{
		int a_ = 11;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		string str = RecordTableEnumerator.b("⍀ⱂ㝄⍆ⱈ㥊恌", a_);
		str = str + A_0 + RecordTableEnumerator.b("筀", a_);
		str = str + RecordTableEnumerator.b("慀", a_) + this.\u17EA[(int)A_2];
		return str + RecordTableEnumerator.b("慀", a_) + this.ᜀ(A_1) + RecordTableEnumerator.b("穀", a_);
	}

	// Token: 0x060054BD RID: 21693 RVA: 0x003536C0 File Offset: 0x003526C0
	private string ᜀ(HorizontalAlignType A_0)
	{
		int a_ = 19;
		if (A_0 == HorizontalAlignType.General)
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
				break;
			}
			return RecordTableEnumerator.b("ࡈ㹊㥌⁎㱐㉒⅔㹖㩘", a_);
		}
		return A_0.ToString();
	}

	// Token: 0x060054BE RID: 21694 RVA: 0x00353728 File Offset: 0x00352728
	private string ᜀ(VerticalAlignType A_0)
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
		return A_0.ToString();
	}

	// Token: 0x060054BF RID: 21695 RVA: 0x00353770 File Offset: 0x00352770
	private string ᜀ(IFont A_0)
	{
		int a_ = 16;
		string result;
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
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.IsSuperscript)
					{
						num = 4;
						continue;
					}
					return result;
				case 2:
					result = RecordTableEnumerator.b("ᕅ㵇⡉㽋ⵍ≏㭑⑓≕", a_);
					num = 6;
					continue;
				case 3:
					goto IL_D0;
				case 4:
					result = RecordTableEnumerator.b("ᕅ㵇㩉⥋㱍⍏ㅑ♓㽕⡗⹙", a_);
					num = 3;
					continue;
				case 5:
					if (A_0.IsSubscript)
					{
						num = 2;
						continue;
					}
					goto IL_6C;
				case 6:
					goto IL_6C;
				case 7:
					goto IL_6A;
				}
				if (A_0 == null)
				{
					num = 7;
					continue;
				}
				result = RecordTableEnumerator.b("ࡅ❇⑉⥋", a_);
				num = 5;
				continue;
				IL_6C:
				num = 0;
			}
			IL_6A:
			throw new ArgumentNullException(RecordTableEnumerator.b("⁅❇⑉㡋", a_));
			IL_D0:
			if (true)
			{
			}
			break;
		}
		}
		return result;
	}

	// Token: 0x060054C0 RID: 21696 RVA: 0x00353890 File Offset: 0x00352890
	private string ᜀ(IAutoFilterCondition A_0)
	{
		int a_ = 8;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.DataType == FilterDataType.String)
				{
					num = 10;
					continue;
				}
				num = 2;
				continue;
			case 1:
				goto IL_126;
			case 2:
				if (A_0.DataType == FilterDataType.FloatingPoint)
				{
					num = 11;
					continue;
				}
				num = 5;
				continue;
			case 3:
				goto IL_BC;
			case 4:
				if (!A_0.Boolean)
				{
					num = 1;
					continue;
				}
				goto IL_128;
			case 5:
				if (A_0.DataType != FilterDataType.ErrorCode)
				{
					num = 7;
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
					num = 3;
					continue;
				}
				break;
			case 7:
				if (A_0.DataType == FilterDataType.Boolean)
				{
					num = 9;
					continue;
				}
				goto IL_188;
			case 8:
				goto IL_54;
			case 9:
				num = 4;
				continue;
			case 10:
				goto IL_E3;
			case 11:
				goto IL_186;
			}
			if (A_0 == null)
			{
				num = 8;
			}
			else
			{
				num = 0;
			}
		}
		IL_54:
		throw new ArgumentNullException(RecordTableEnumerator.b("崽⼿ⱁ⁃", a_));
		IL_BC:
		return A_0.ErrorCode.ToString();
		IL_E3:
		return A_0.String;
		IL_126:
		return RecordTableEnumerator.b("฽", a_);
		IL_128:
		if (true)
		{
		}
		return RecordTableEnumerator.b("༽", a_);
		IL_186:
		return A_0.Double.ToString();
		IL_188:
		throw new ArgumentException(RecordTableEnumerator.b("欽⸿⍁㝃㕅ⅇⵉ≋⭍㑏牑㝓㥕㙗㹙㕛⩝ཟౡգ੥䡧ṩᕫṭᕯ", a_));
	}

	// Token: 0x060054C1 RID: 21697 RVA: 0x00353A38 File Offset: 0x00352A38
	private string ᜀ(XlsCellRecordCollection A_0, long A_1, out XmlSerializationCellType A_2)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			double numberWithoutFormula;
			int num;
			DateTime dateTime;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_170:
				if (numberWithoutFormula != -1.7976931348623157E+308)
				{
					num = 8;
				}
				else
				{
					dateTime = A_0.GetDateTime(A_1);
					num = 1;
				}
				break;
			default:
				if (false)
				{
				}
				num = 3;
				break;
			}
			string error;
			string text;
			for (;;)
			{
				switch (num)
				{
				case 0:
					A_2 = XmlSerializationCellType.Boolean;
					num = 11;
					continue;
				case 1:
					if (dateTime != DateTime.MinValue)
					{
						num = 7;
						continue;
					}
					error = A_0.GetError(A_1);
					num = 2;
					continue;
				case 2:
					if (error != null)
					{
						num = 4;
						continue;
					}
					num = 9;
					continue;
				case 4:
					goto IL_1D7;
				case 5:
					if (text != null)
					{
						num = 6;
						continue;
					}
					numberWithoutFormula = A_0.GetNumberWithoutFormula(A_1);
					num = 10;
					continue;
				case 6:
					goto IL_10A;
				case 7:
					goto IL_14D;
				case 8:
					goto IL_188;
				case 9:
				{
					bool flag;
					if (A_0.GetBool(A_1, out flag))
					{
						if (true)
						{
						}
						num = 0;
						continue;
					}
					goto IL_1FA;
				}
				case 10:
					goto IL_170;
				case 11:
				{
					bool flag;
					if (!flag)
					{
						num = 12;
						continue;
					}
					goto IL_111;
				}
				case 12:
					goto IL_1AC;
				case 13:
					goto IL_89;
				}
				if (A_0 == null)
				{
					num = 13;
				}
				else
				{
					text = A_0.GetText(A_1);
					num = 5;
				}
			}
			IL_89:
			throw new ArgumentNullException(RecordTableEnumerator.b("⭇⽉⁋≍", a_));
			IL_10A:
			A_2 = XmlSerializationCellType.String;
			return text;
			IL_111:
			return RecordTableEnumerator.b("祇", a_);
			IL_14D:
			A_2 = XmlSerializationCellType.DateTime;
			return XmlConvert.ToString(dateTime, RecordTableEnumerator.b("ㅇ㍉㕋㝍絏ὑᥓ筕㱗㹙࡛ᙝ⡟塡ॣ୥剧ᥩὫ", a_));
			IL_188:
			A_2 = XmlSerializationCellType.Number;
			return XmlConvert.ToString(numberWithoutFormula);
			IL_1AC:
			return RecordTableEnumerator.b("硇", a_);
			IL_1D7:
			A_2 = XmlSerializationCellType.Error;
			return error;
			IL_1FA:
			throw new ApplicationException(RecordTableEnumerator.b("େ⽉⁋≍灏㙑㭓╕㙗絙⡛繝͟ൡ੣ብ१ͩɫ乭ٯ፱ᡳ͵ᵷ", a_));
		}
		}
	}

	// Token: 0x060054C2 RID: 21698 RVA: 0x00353C54 File Offset: 0x00352C54
	private void ᜀ(XmlWriter A_0, string A_1, string A_2)
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
		this.ᜀ(A_0, A_1, A_2, null);
	}

	// Token: 0x060054C3 RID: 21699 RVA: 0x00353C9C File Offset: 0x00352C9C
	private void ᜀ(XmlWriter A_0, string A_1, string A_2, string A_3)
	{
		int a_ = 6;
		int num = 6;
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
					goto IL_12D;
				case 1:
					goto IL_184;
				case 2:
					goto IL_74;
				case 3:
					goto IL_14D;
				case 4:
					num = 12;
					continue;
				case 5:
					num = 8;
					continue;
				case 7:
					if (A_3 != null)
					{
						num = 10;
						continue;
					}
					goto IL_186;
				case 8:
					if (A_1.Length == 0)
					{
						num = 0;
						continue;
					}
					A_0.WriteStartElement(A_1, A_2, null);
					num = 7;
					continue;
				case 9:
					if (A_1 != null)
					{
						num = 5;
						continue;
					}
					goto IL_79;
				case 10:
					A_0.WriteString(A_3);
					if (true)
					{
					}
					num = 3;
					continue;
				case 11:
					goto IL_B3;
				case 12:
					if (A_2.Length == 0)
					{
						num = 1;
						continue;
					}
					num = 9;
					continue;
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				num = 11;
				continue;
			}
			IL_B3:
			if (A_2 == null)
			{
				goto IL_14F;
			}
			num = 4;
		}
		IL_74:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬻䰽⤿㙁⅃㑅", a_));
		IL_79:
		throw new ArgumentNullException(RecordTableEnumerator.b("伻䨽㈿ቁ㙃⍅⹇⍉㑋", a_));
		IL_12D:
		goto IL_79;
		IL_14D:
		goto IL_186;
		IL_14F:
		throw new ArgumentNullException(RecordTableEnumerator.b("伻䨽㈿ు╃⭅ⵇ", a_));
		IL_184:
		goto IL_14F;
		IL_186:
		A_0.WriteEndElement();
	}

	// Token: 0x060054C4 RID: 21700 RVA: 0x00353E38 File Offset: 0x00352E38
	private void ᜀ(XmlWriter A_0, List<spr\u192F> A_1)
	{
		int a_ = 4;
		if (true)
		{
		}
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				int num2 = 0;
				int count = A_1.Count;
				num = 7;
				continue;
			}
			case 1:
			{
				int num2;
				int count;
				if (num2 >= count)
				{
					num = 6;
					continue;
				}
				this.ᜄ(A_0, A_1[num2]);
				num2++;
				num = 3;
				continue;
			}
			case 2:
				goto IL_F0;
			case 3:
				goto IL_BB;
			case 4:
				goto IL_4F;
			case 6:
				return;
			case 7:
				goto IL_BB;
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			num = 0;
			continue;
			IL_BB:
			num = 1;
		}
		for (;;)
		{
			IL_4F:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_97;
			}
		}
		IL_97:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䴹主圽㐿❁㙃", a_));
		IL_F0:
		throw new ArgumentNullException(RecordTableEnumerator.b("嘹唻䴽㐿", a_));
	}

	// Token: 0x060054C5 RID: 21701 RVA: 0x00353F50 File Offset: 0x00352F50
	private List<spr\u192F> ᜀ(IWorksheets A_0)
	{
		int a_ = 10;
		switch (0)
		{
		default:
			for (;;)
			{
				List<spr\u192F> list = new List<spr\u192F>();
				int num = 6;
				for (;;)
				{
					if (true)
					{
					}
					int num2;
					IList<spr\u192F> list2;
					switch (num)
					{
					case 0:
						goto IL_154;
					case 1:
						goto IL_78;
					case 2:
						return list;
					case 3:
						goto IL_154;
					case 4:
					{
						int count;
						if (num2 >= count)
						{
							num = 2;
							continue;
						}
						XlsWorksheet xlsWorksheet = (XlsWorksheet)A_0[num2];
						spr\u1FBC spr_u1FBC = xlsWorksheet.MergeCells;
						list2 = spr_u1FBC.ᜁ();
						int num3 = 0;
						int count2 = list2.Count;
						num = 5;
						continue;
					}
					case 5:
						goto IL_7D;
					case 6:
					{
						if (A_0 == null)
						{
							num = 1;
							continue;
						}
						num2 = 0;
						int count = A_0.Count;
						num = 0;
						continue;
					}
					case 7:
						goto IL_7D;
					case 8:
					{
						int num3;
						int count2;
						if (num3 < count2)
						{
							spr\u192F spr_u192F = list2[num3];
							spr\u1FBC spr_u1FBC;
							Rectangle rectangle = spr_u1FBC.ᜂ(num3);
							long a_2 = sprṔ.ᜀ(rectangle.X + 1, rectangle.Y + 1);
							XlsWorksheet xlsWorksheet;
							long key = sprỉ.ᜀ(xlsWorksheet.Index, a_2);
							int num4 = 5000 + this.\u17F2.Count;
							spr_u192F.ᜃ((int)((ushort)num4));
							this.\u17F2.Add(key, num4);
							num3++;
							num = 7;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_B4;
						default:
							if (false)
							{
							}
							num = 9;
							continue;
						}
						break;
					}
					case 9:
						goto IL_B4;
					}
					break;
					IL_7D:
					num = 8;
					continue;
					IL_B4:
					list.AddRange(list2);
					num2++;
					num = 3;
					continue;
					IL_154:
					num = 4;
				}
			}
			IL_78:
			throw new ArgumentNullException(RecordTableEnumerator.b("㌿⩁⅃⍅㱇㥉", a_));
		}
	}

	// Token: 0x060054C6 RID: 21702 RVA: 0x0035413C File Offset: 0x0035313C
	private string ᜀ(string A_0)
	{
		int a_ = 18;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return A_0;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					if (!A_0.EndsWith(RecordTableEnumerator.b("歇ᡉोࡍ", a_)))
					{
						num = 0;
						continue;
					}
					goto IL_C2;
				}
				break;
			case 3:
				if (A_0.Length == 0)
				{
					num = 5;
					continue;
				}
				num = 1;
				continue;
			case 4:
				num = 3;
				continue;
			case 5:
				goto IL_BE;
			}
			if (A_0 == null)
			{
				goto IL_8C;
			}
			num = 4;
		}
		return A_0;
		IL_8C:
		throw new ArgumentNullException(RecordTableEnumerator.b("็╉㹋⍍╏㹑㕓", a_));
		IL_BE:
		goto IL_8C;
		IL_C2:
		if (true)
		{
		}
		return RecordTableEnumerator.b("畇楉ṋ୍ᙏ獑", a_);
	}

	// Token: 0x060054C7 RID: 21703 RVA: 0x00354224 File Offset: 0x00353224
	// Note: this type is marked as 'beforefieldinit'.
	static sprỉ()
	{
		int a_ = 1;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		sprỉ.\u17EB = new string[]
		{
			"",
			"",
			"",
			"",
			"",
			RecordTableEnumerator.b("猶倸娺娼倾⽀≂⥄୆ⱈⵊ㥌", a_),
			RecordTableEnumerator.b("猶倸娺娼倾⽀≂⥄ᕆ⁈ⱊ╌㭎", a_),
			RecordTableEnumerator.b("笶尸崺䤼", a_),
			RecordTableEnumerator.b("挶嘸䬺", a_),
			RecordTableEnumerator.b("甶嘸伺䤼倾ⱀ", a_),
			RecordTableEnumerator.b("收倸尺唼䬾", a_)
		};
		sprỉ.\u17EC = new string[]
		{
			RecordTableEnumerator.b("礶嘸唺堼", a_),
			RecordTableEnumerator.b("ضᤸ砺刼儾㕀⩂⭄㉆♈㹊㹌", a_),
			RecordTableEnumerator.b("Զᤸ砺刼儾㕀⩂⭄㉆♈㹊㹌", a_),
			RecordTableEnumerator.b("ضᤸ缺尼䰾⥀", a_),
			RecordTableEnumerator.b("ضᤸ缺刼䬾", a_),
			RecordTableEnumerator.b("жᤸ砺刼儾㕀⩂⭄㉆♈㹊㹌", a_),
			RecordTableEnumerator.b("жᤸ缺刼䨾⍀⽂⁄", a_),
			RecordTableEnumerator.b("琶嘸唺䤼嘾⽀㙂⩄㉆㩈", a_),
			RecordTableEnumerator.b("Զᤸ缺尼䰾⥀", a_),
			RecordTableEnumerator.b("ضᤸ缺尼䰾⥀݂⩄㍆", a_),
			RecordTableEnumerator.b("Զᤸ缺尼䰾⥀݂⩄㍆", a_),
			RecordTableEnumerator.b("ضᤸ缺尼䰾⥀݂⩄㍆ൈ⑊㥌", a_),
			RecordTableEnumerator.b("Զᤸ缺尼䰾⥀݂⩄㍆ൈ⑊㥌", a_),
			RecordTableEnumerator.b("Զᤸ栺儼帾⽀㝂ń♆㩈⍊ौ⁎═", a_)
		};
		sprỉ.\u17ED = new string[]
		{
			RecordTableEnumerator.b("縶圸欺儼帾≀♂", a_),
			RecordTableEnumerator.b("礶嘸砺刼刾ⱀ♂⭄㍆㩈", a_),
			RecordTableEnumerator.b("搶儸帺堼䬾рⵂ⅄", a_)
		};
		sprỉ.\u17EE = new string[]
		{
			RecordTableEnumerator.b("夶嘸唺堼", a_),
			RecordTableEnumerator.b("甶唸娺匼吾", a_),
			RecordTableEnumerator.b("猶堸䠺唼", a_),
			RecordTableEnumerator.b("礶砸", a_)
		};
		sprỉ.\u17EF = new string[]
		{
			RecordTableEnumerator.b("制䬸䤺刼䴾", a_),
			RecordTableEnumerator.b("搶儸帺堼䬾ी⩂⅄⍆ⱈ╊", a_),
			RecordTableEnumerator.b("搶儸帺堼䬾ᝀ♂㝄㹆ň≊⥌⭎㑐㵒", a_)
		};
		sprỉ.\u17F0 = new string[]
		{
			RecordTableEnumerator.b("礶嘸唺堼", a_),
			RecordTableEnumerator.b("搶嘸场吼嬾", a_),
			RecordTableEnumerator.b("瀶䬸娺䐼ਾ煀", a_),
			RecordTableEnumerator.b("瀶䬸娺䐼࠾瑀", a_),
			RecordTableEnumerator.b("瀶䬸娺䐼ാ瑀", a_),
			RecordTableEnumerator.b("缶嘸䤺䜼氾㕀ㅂⱄ㝆ⱈ", a_),
			RecordTableEnumerator.b("愶尸䤺䤼氾㕀ㅂⱄ㝆ⱈ", a_),
			RecordTableEnumerator.b("收尸䴺堼䴾㉀♂ń⹆⡈ⱊṌ㭎⍐㩒╔㉖", a_),
			RecordTableEnumerator.b("猶倸娺娼氾㕀ㅂⱄ㝆ⱈ", a_),
			RecordTableEnumerator.b("猶倸娺娼簾㍀ⱂ㙄㑆", a_),
			RecordTableEnumerator.b("挶儸刺帼吾Հ⩂⑄⁆ੈ㥊≌㱎≐", a_),
			RecordTableEnumerator.b("挶儸刺匼眾⹀ㅂ㽄ᑆ㵈㥊⑌㽎㑐", a_),
			RecordTableEnumerator.b("挶儸刺匼椾⑀ㅂㅄᑆ㵈㥊⑌㽎㑐", a_),
			RecordTableEnumerator.b("挶儸刺匼派⑀㕂⁄㕆㩈⹊ौ♎ぐ㑒ٔ⍖⭘㉚ⵜ㩞", a_),
			RecordTableEnumerator.b("挶儸刺匼笾⡀≂≄ᑆ㵈㥊⑌㽎㑐", a_),
			RecordTableEnumerator.b("挶儸刺匼眾⹀ㅂ㽄ц㭈⑊㹌㱎", a_),
			RecordTableEnumerator.b("挶儸刺匼笾⡀≂≄ц㭈⑊㹌㱎", a_),
			RecordTableEnumerator.b("瀶䬸娺䐼฾獀療", a_),
			RecordTableEnumerator.b("瀶䬸娺䐼༾着煂灄", a_)
		};
	}

	// Token: 0x040027CE RID: 10190
	public const string ᜀ = "<?xml version=\"1.0\"?>";

	// Token: 0x040027CF RID: 10191
	public const string ᜁ = "<?mso-application progid=\"Excel.Sheet\"?>";

	// Token: 0x040027D0 RID: 10192
	internal const string ᜂ = "urn:schemas-microsoft-com:office:office";

	// Token: 0x040027D1 RID: 10193
	internal const string ᜃ = "urn:schemas-microsoft-com:office:excel";

	// Token: 0x040027D2 RID: 10194
	internal const string ᜄ = "urn:schemas-microsoft-com:office:spreadsheet";

	// Token: 0x040027D3 RID: 10195
	internal const string ᜅ = "http://www.w3.org/TR/REC-html40";

	// Token: 0x040027D4 RID: 10196
	internal const string ᜆ = "ss";

	// Token: 0x040027D5 RID: 10197
	internal const string ᜇ = "html";

	// Token: 0x040027D6 RID: 10198
	internal const string ᜈ = "o";

	// Token: 0x040027D7 RID: 10199
	internal const string ᜉ = "x";

	// Token: 0x040027D8 RID: 10200
	internal const string ᜊ = "xmlns:";

	// Token: 0x040027D9 RID: 10201
	internal const string ᜋ = "xmlns";

	// Token: 0x040027DA RID: 10202
	internal const string ᜌ = "urlset";

	// Token: 0x040027DB RID: 10203
	public const string \u170D = "Workbook";

	// Token: 0x040027DC RID: 10204
	public const string ᜎ = "Worksheet";

	// Token: 0x040027DD RID: 10205
	public const string ᜏ = "Name";

	// Token: 0x040027DE RID: 10206
	public const string ᜐ = "Table";

	// Token: 0x040027DF RID: 10207
	public const string ᜑ = "Row";

	// Token: 0x040027E0 RID: 10208
	public const string \u1712 = "Cell";

	// Token: 0x040027E1 RID: 10209
	public const string \u1713 = "Data";

	// Token: 0x040027E2 RID: 10210
	public const string \u1714 = "Names";

	// Token: 0x040027E3 RID: 10211
	public const string \u1715 = "NamedRange";

	// Token: 0x040027E4 RID: 10212
	public const string \u1716 = "Styles";

	// Token: 0x040027E5 RID: 10213
	public const string \u1717 = "Style";

	// Token: 0x040027E6 RID: 10214
	public const string \u1718 = "Font";

	// Token: 0x040027E7 RID: 10215
	public const string \u1719 = "Protection";

	// Token: 0x040027E8 RID: 10216
	public const string \u171A = "Alignment";

	// Token: 0x040027E9 RID: 10217
	public const string \u171B = "NumberFormat";

	// Token: 0x040027EA RID: 10218
	public const string \u171C = "Interior";

	// Token: 0x040027EB RID: 10219
	public const string \u171D = "Borders";

	// Token: 0x040027EC RID: 10220
	public const string \u171E = "Border";

	// Token: 0x040027ED RID: 10221
	internal const string \u171F = "AutoFilter";

	// Token: 0x040027EE RID: 10222
	internal const string ᜠ = "AutoFilterColumn";

	// Token: 0x040027EF RID: 10223
	internal const string ᜡ = "AutoFilterAnd";

	// Token: 0x040027F0 RID: 10224
	internal const string ᜢ = "AutoFilterCondition";

	// Token: 0x040027F1 RID: 10225
	internal const string ᜣ = "AutoFilterOr";

	// Token: 0x040027F2 RID: 10226
	public const string ᜤ = "Comment";

	// Token: 0x040027F3 RID: 10227
	internal const string ᜥ = "<B>";

	// Token: 0x040027F4 RID: 10228
	internal const string ᜦ = "</B>";

	// Token: 0x040027F5 RID: 10229
	internal const string ᜧ = "<I>";

	// Token: 0x040027F6 RID: 10230
	internal const string ᜨ = "</I>";

	// Token: 0x040027F7 RID: 10231
	internal const string ᜩ = "<U>";

	// Token: 0x040027F8 RID: 10232
	internal const string ᜪ = "</U>";

	// Token: 0x040027F9 RID: 10233
	internal const string ᜫ = "<S>";

	// Token: 0x040027FA RID: 10234
	internal const string ᜬ = "</S>";

	// Token: 0x040027FB RID: 10235
	internal const string ᜭ = "<Sub>";

	// Token: 0x040027FC RID: 10236
	internal const string ᜮ = "</Sub>";

	// Token: 0x040027FD RID: 10237
	internal const string ᜯ = "<Sup>";

	// Token: 0x040027FE RID: 10238
	internal const string ᜰ = "</Sup>";

	// Token: 0x040027FF RID: 10239
	internal const string ᜱ = "</Font>";

	// Token: 0x04002800 RID: 10240
	internal const string \u1732 = "<Font";

	// Token: 0x04002801 RID: 10241
	public const string \u1733 = "Span";

	// Token: 0x04002802 RID: 10242
	public const string \u1734 = "Column";

	// Token: 0x04002803 RID: 10243
	internal const string \u1735 = "ConditionalFormatting";

	// Token: 0x04002804 RID: 10244
	internal const string \u1736 = "Condition";

	// Token: 0x04002805 RID: 10245
	internal const string \u1737 = "Qualifier";

	// Token: 0x04002806 RID: 10246
	internal const string \u1738 = "Value1";

	// Token: 0x04002807 RID: 10247
	internal const string \u1739 = "Value2";

	// Token: 0x04002808 RID: 10248
	internal const string \u173A = "WorksheetOptions";

	// Token: 0x04002809 RID: 10249
	internal const string \u173B = "DisplayPageBreak";

	// Token: 0x0400280A RID: 10250
	internal const string \u173C = "ShowPageBreakZoom";

	// Token: 0x0400280B RID: 10251
	internal const string \u173D = "PageBreakZoom";

	// Token: 0x0400280C RID: 10252
	internal const string \u173E = "PageBreaks";

	// Token: 0x0400280D RID: 10253
	internal const string \u173F = "ColBreaks";

	// Token: 0x0400280E RID: 10254
	internal const string ᝀ = "ColBreak";

	// Token: 0x0400280F RID: 10255
	internal const string ᝁ = "Column";

	// Token: 0x04002810 RID: 10256
	internal const string ᝂ = "RowStart";

	// Token: 0x04002811 RID: 10257
	internal const string ᝃ = "RowEnd";

	// Token: 0x04002812 RID: 10258
	internal const string ᝄ = "RowBreaks";

	// Token: 0x04002813 RID: 10259
	internal const string ᝅ = "RowBreak";

	// Token: 0x04002814 RID: 10260
	internal const string ᝆ = "Row";

	// Token: 0x04002815 RID: 10261
	internal const string ᝇ = "ColStart";

	// Token: 0x04002816 RID: 10262
	internal const string ᝈ = "ColEnd";

	// Token: 0x04002817 RID: 10263
	internal const string ᝉ = "PageSetup";

	// Token: 0x04002818 RID: 10264
	internal const string ᝊ = "Footer";

	// Token: 0x04002819 RID: 10265
	internal const string ᝋ = "Header";

	// Token: 0x0400281A RID: 10266
	internal const string ᝌ = "Layout";

	// Token: 0x0400281B RID: 10267
	internal const string ᝍ = "PageMargins";

	// Token: 0x0400281C RID: 10268
	internal const string ᝎ = "Print";

	// Token: 0x0400281D RID: 10269
	internal const string ᝏ = "CommentsLayout";

	// Token: 0x0400281E RID: 10270
	internal const string ᝐ = "PrintErrors";

	// Token: 0x0400281F RID: 10271
	internal const string ᝑ = "FitToPage";

	// Token: 0x04002820 RID: 10272
	internal const string \u1752 = "LeftToRight";

	// Token: 0x04002821 RID: 10273
	internal const string \u1753 = "ActivePane";

	// Token: 0x04002822 RID: 10274
	public const string \u1754 = "TopRowVisible";

	// Token: 0x04002823 RID: 10275
	internal const string \u1755 = "SplitHorizontal";

	// Token: 0x04002824 RID: 10276
	internal const string \u1756 = "SplitVertical";

	// Token: 0x04002825 RID: 10277
	internal const string \u1757 = "TopRowBottomPane";

	// Token: 0x04002826 RID: 10278
	internal const string \u1758 = "LeftColumnRightPane";

	// Token: 0x04002827 RID: 10279
	internal const string \u1759 = "FreezePanes";

	// Token: 0x04002828 RID: 10280
	internal const string \u175A = "FrozenNoSplit";

	// Token: 0x04002829 RID: 10281
	internal const string \u175B = "Panes";

	// Token: 0x0400282A RID: 10282
	internal const string \u175C = "Pane";

	// Token: 0x0400282B RID: 10283
	internal const string \u175D = "Number";

	// Token: 0x0400282C RID: 10284
	internal const string \u175E = "ActiveCol";

	// Token: 0x0400282D RID: 10285
	internal const string \u175F = "ActiveRow";

	// Token: 0x0400282E RID: 10286
	internal const string ᝠ = "TabColorIndex";

	// Token: 0x0400282F RID: 10287
	internal const string ᝡ = "Zoom";

	// Token: 0x04002830 RID: 10288
	internal const string ᝢ = "DoNotDisplayGridlines";

	// Token: 0x04002831 RID: 10289
	internal const string ᝣ = "Visible";

	// Token: 0x04002832 RID: 10290
	internal const string ᝤ = "DoNotDisplayHeadings";

	// Token: 0x04002833 RID: 10291
	internal const string ᝥ = "ExcelWorkbook";

	// Token: 0x04002834 RID: 10292
	internal const string ᝦ = "ActiveSheet";

	// Token: 0x04002835 RID: 10293
	internal const string ᝧ = "Selected";

	// Token: 0x04002836 RID: 10294
	internal const string ᝨ = "SelectedSheets";

	// Token: 0x04002837 RID: 10295
	public const string ᝩ = "RightToLeft";

	// Token: 0x04002838 RID: 10296
	public const string ᝪ = "Index";

	// Token: 0x04002839 RID: 10297
	public const string ᝫ = "Type";

	// Token: 0x0400283A RID: 10298
	private const string ᝬ = "Ticked";

	// Token: 0x0400283B RID: 10299
	public const string \u176D = "Formula";

	// Token: 0x0400283C RID: 10300
	public const string ᝮ = "RefersTo";

	// Token: 0x0400283D RID: 10301
	public const string ᝯ = "ID";

	// Token: 0x0400283E RID: 10302
	public const string ᝰ = "Parent";

	// Token: 0x0400283F RID: 10303
	public const string \u1771 = "Bold";

	// Token: 0x04002840 RID: 10304
	public const string \u1772 = "FontName";

	// Token: 0x04002841 RID: 10305
	public const string \u1773 = "Color";

	// Token: 0x04002842 RID: 10306
	public const string \u1774 = "Italic";

	// Token: 0x04002843 RID: 10307
	public const string \u1775 = "Outline";

	// Token: 0x04002844 RID: 10308
	public const string \u1776 = "Shadow";

	// Token: 0x04002845 RID: 10309
	public const string \u1777 = "Size";

	// Token: 0x04002846 RID: 10310
	public const string \u1778 = "StrikeThrough";

	// Token: 0x04002847 RID: 10311
	public const string \u1779 = "Underline";

	// Token: 0x04002848 RID: 10312
	public const string \u177A = "Protected";

	// Token: 0x04002849 RID: 10313
	public const string \u177B = "HideFormula";

	// Token: 0x0400284A RID: 10314
	public const string \u177C = "Horizontal";

	// Token: 0x0400284B RID: 10315
	public const string \u177D = "Indent";

	// Token: 0x0400284C RID: 10316
	public const string \u177E = "ReadingOrder";

	// Token: 0x0400284D RID: 10317
	public const string \u177F = "Rotate";

	// Token: 0x0400284E RID: 10318
	public const string ក = "ShrinkToFit";

	// Token: 0x0400284F RID: 10319
	public const string ខ = "Vertical";

	// Token: 0x04002850 RID: 10320
	public const string គ = "VerticalText";

	// Token: 0x04002851 RID: 10321
	public const string ឃ = "WrapText";

	// Token: 0x04002852 RID: 10322
	public const string ង = "Format";

	// Token: 0x04002853 RID: 10323
	public const string ច = "PatternColor";

	// Token: 0x04002854 RID: 10324
	public const string ឆ = "Pattern";

	// Token: 0x04002855 RID: 10325
	public const string ជ = "Position";

	// Token: 0x04002856 RID: 10326
	private const string ឈ = "Range";

	// Token: 0x04002857 RID: 10327
	private const string ញ = "Operator";

	// Token: 0x04002858 RID: 10328
	private const string ដ = "Value";

	// Token: 0x04002859 RID: 10329
	public const string ឋ = "Author";

	// Token: 0x0400285A RID: 10330
	public const string ឌ = "ShowAlways";

	// Token: 0x0400285B RID: 10331
	public const string ឍ = "DefaultColumnWidth";

	// Token: 0x0400285C RID: 10332
	public const string ណ = "DefaultRowHeight";

	// Token: 0x0400285D RID: 10333
	public const string ត = "Width";

	// Token: 0x0400285E RID: 10334
	public const string ថ = "Hidden";

	// Token: 0x0400285F RID: 10335
	public const string ទ = "StyleID";

	// Token: 0x04002860 RID: 10336
	public const string ធ = "AutoFitWidth";

	// Token: 0x04002861 RID: 10337
	public const string ន = "AutoFitHeight";

	// Token: 0x04002862 RID: 10338
	public const string ប = "Height";

	// Token: 0x04002863 RID: 10339
	public const string ផ = "Face";

	// Token: 0x04002864 RID: 10340
	public const string ព = "LineStyle";

	// Token: 0x04002865 RID: 10341
	public const string ភ = "Weight";

	// Token: 0x04002866 RID: 10342
	public const string ម = "VerticalAlign";

	// Token: 0x04002867 RID: 10343
	public const string យ = "MergeAcross";

	// Token: 0x04002868 RID: 10344
	public const string រ = "MergeDown";

	// Token: 0x04002869 RID: 10345
	internal const string ល = "HRefScreenTip";

	// Token: 0x0400286A RID: 10346
	internal const string វ = "HRef";

	// Token: 0x0400286B RID: 10347
	internal const string ឝ = "Margin";

	// Token: 0x0400286C RID: 10348
	internal const string ឞ = "Top";

	// Token: 0x0400286D RID: 10349
	internal const string ស = "Right";

	// Token: 0x0400286E RID: 10350
	internal const string ហ = "Left";

	// Token: 0x0400286F RID: 10351
	internal const string ឡ = "Bottom";

	// Token: 0x04002870 RID: 10352
	internal const string អ = "CenterHorizontal";

	// Token: 0x04002871 RID: 10353
	internal const string ឣ = "CenterVertical";

	// Token: 0x04002872 RID: 10354
	internal const string ឤ = "Orientation";

	// Token: 0x04002873 RID: 10355
	internal const string ឥ = "StartPageNumber";

	// Token: 0x04002874 RID: 10356
	internal const string ឦ = "NumberofCopies";

	// Token: 0x04002875 RID: 10357
	public const int ឧ = 1;

	// Token: 0x04002876 RID: 10358
	internal const string ឨ = "HorizontalResolution";

	// Token: 0x04002877 RID: 10359
	internal const string ឩ = "PaperSizeIndex";

	// Token: 0x04002878 RID: 10360
	internal const string ឪ = "Scale";

	// Token: 0x04002879 RID: 10361
	internal const string ឫ = "FitWidth";

	// Token: 0x0400287A RID: 10362
	internal const string ឬ = "FitHeight";

	// Token: 0x0400287B RID: 10363
	internal const string ឭ = "Gridlines";

	// Token: 0x0400287C RID: 10364
	internal const string ឮ = "BlackAndWhite";

	// Token: 0x0400287D RID: 10365
	internal const string ឯ = "DraftQuality";

	// Token: 0x0400287E RID: 10366
	internal const string ឰ = "RowColHeadings";

	// Token: 0x0400287F RID: 10367
	private const string ឱ = ":";

	// Token: 0x04002880 RID: 10368
	private const string ឲ = ";";

	// Token: 0x04002881 RID: 10369
	private const string ឳ = "color";

	// Token: 0x04002882 RID: 10370
	private const string \u17B4 = "font-style";

	// Token: 0x04002883 RID: 10371
	private const string \u17B5 = "font-weight";

	// Token: 0x04002884 RID: 10372
	private const string \u17B6 = "700";

	// Token: 0x04002885 RID: 10373
	private const string \u17B7 = "400";

	// Token: 0x04002886 RID: 10374
	private const string \u17B8 = "font-style:italic;";

	// Token: 0x04002887 RID: 10375
	private const string \u17B9 = "text-line-through:single;";

	// Token: 0x04002888 RID: 10376
	private const string \u17BA = "text-underline-style";

	// Token: 0x04002889 RID: 10377
	private const string \u17BB = "background";

	// Token: 0x0400288A RID: 10378
	private const string \u17BC = "mso-pattern";

	// Token: 0x0400288B RID: 10379
	private const string \u17BD = "border-";

	// Token: 0x0400288C RID: 10380
	private const string \u17BE = "Arial";

	// Token: 0x0400288D RID: 10381
	private const string \u17BF = "None";

	// Token: 0x0400288E RID: 10382
	public const int \u17C0 = 8;

	// Token: 0x0400288F RID: 10383
	private const int \u17C1 = 5;

	// Token: 0x04002890 RID: 10384
	private const int \u17C2 = 6;

	// Token: 0x04002891 RID: 10385
	public const int \u17C3 = 0;

	// Token: 0x04002892 RID: 10386
	public const int \u17C4 = 90;

	// Token: 0x04002893 RID: 10387
	private const int \u17C5 = 10;

	// Token: 0x04002894 RID: 10388
	public const int \u17C6 = 255;

	// Token: 0x04002895 RID: 10389
	private const int \u17C7 = 5;

	// Token: 0x04002896 RID: 10390
	public const string \u17C8 = "Default";

	// Token: 0x04002897 RID: 10391
	private const string \u17C9 = "s";

	// Token: 0x04002898 RID: 10392
	private const string \u17CA = "None";

	// Token: 0x04002899 RID: 10393
	private const string \u17CB = "Subscript";

	// Token: 0x0400289A RID: 10394
	private const string \u17CC = "Superscript";

	// Token: 0x0400289B RID: 10395
	private const double \u17CD = 0.5;

	// Token: 0x0400289C RID: 10396
	private const int \u17CE = 100;

	// Token: 0x0400289D RID: 10397
	private const int \u17CF = 1;

	// Token: 0x0400289E RID: 10398
	private const int \u17D0 = 100;

	// Token: 0x0400289F RID: 10399
	private const string \u17D1 = "1";

	// Token: 0x040028A0 RID: 10400
	private const string \u17D2 = "0";

	// Token: 0x040028A1 RID: 10401
	private const string \u17D3 = "All";

	// Token: 0x040028A2 RID: 10402
	public const string \u17D4 = "#";

	// Token: 0x040028A3 RID: 10403
	private const string \u17D5 = "Bottom";

	// Token: 0x040028A4 RID: 10404
	private const string \u17D6 = "Top";

	// Token: 0x040028A5 RID: 10405
	private const string \u17D7 = "Percent";

	// Token: 0x040028A6 RID: 10406
	private const string \u17D8 = "Blanks";

	// Token: 0x040028A7 RID: 10407
	private const string \u17D9 = "Custom";

	// Token: 0x040028A8 RID: 10408
	private const string \u17DA = "NonBlanks";

	// Token: 0x040028A9 RID: 10409
	private const double \u17DB = 48.0;

	// Token: 0x040028AA RID: 10410
	internal const double ៜ = 12.75;

	// Token: 0x040028AB RID: 10411
	public const double \u17DD = 256.0;

	// Token: 0x040028AC RID: 10412
	public const double \u17DE = 20.0;

	// Token: 0x040028AD RID: 10413
	private const string \u17DF = "yyyy-MM-ddTHH:mm:ss";

	// Token: 0x040028AE RID: 10414
	private const int ០ = 5000;

	// Token: 0x040028AF RID: 10415
	private const string ១ = "&#10;";

	// Token: 0x040028B0 RID: 10416
	public const string ២ = "#REF";

	// Token: 0x040028B1 RID: 10417
	public const string ៣ = "#REF!";

	// Token: 0x040028B2 RID: 10418
	public const string ៤ = "=#REF!";

	// Token: 0x040028B3 RID: 10419
	public const int ៥ = 256;

	// Token: 0x040028B4 RID: 10420
	public const int ៦ = 0;

	// Token: 0x040028B5 RID: 10421
	public const long ៧ = 10000000000L;

	// Token: 0x040028B6 RID: 10422
	public const string ៨ = "FirstVisibleSheet";

	// Token: 0x040028B7 RID: 10423
	private readonly string[] ៩;

	// Token: 0x040028B8 RID: 10424
	private readonly string[] \u17EA;

	// Token: 0x040028B9 RID: 10425
	public static readonly string[] \u17EB;

	// Token: 0x040028BA RID: 10426
	public static readonly string[] \u17EC;

	// Token: 0x040028BB RID: 10427
	internal static readonly string[] \u17ED;

	// Token: 0x040028BC RID: 10428
	internal static readonly string[] \u17EE;

	// Token: 0x040028BD RID: 10429
	internal static readonly string[] \u17EF;

	// Token: 0x040028BE RID: 10430
	public static readonly string[] \u17F0;

	// Token: 0x040028BF RID: 10431
	private readonly string[] \u17F1;

	// Token: 0x040028C0 RID: 10432
	private Dictionary<long, int> \u17F2;

	// Token: 0x040028C1 RID: 10433
	private StringBuilder \u17F3;

	// Token: 0x040028C2 RID: 10434
	private StringBuilder \u17F4;
}
