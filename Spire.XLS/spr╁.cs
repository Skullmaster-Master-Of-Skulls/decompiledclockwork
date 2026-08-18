using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Charts;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.PivotTables;
using Spire.Xls.Core.Spreadsheet.XmlReaders.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlSerialization;

// Token: 0x02000582 RID: 1410
internal class spr\u2541
{
	// Token: 0x060054F8 RID: 21752 RVA: 0x0035B11C File Offset: 0x0035A11C
	public void ᜁ(XmlWriter A_0, XlsChart A_1, string A_2)
	{
		int a_ = 7;
		switch (0)
		{
		default:
		{
			int num = 15;
			for (;;)
			{
				RelationsCollection relationsCollection;
				bool? flag;
				switch (num)
				{
				case 0:
					goto IL_6B6;
				case 1:
					if (A_1.SupportWallsAndFloor)
					{
						num = 16;
						continue;
					}
					goto IL_295;
				case 2:
					if (A_1.HasWalls)
					{
						num = 10;
						continue;
					}
					goto IL_295;
				case 3:
					this.ᜀ(A_0, A_1.Floor, RecordTableEnumerator.b("嬼匾⹀ⱂ㝄", a_), A_1);
					num = 8;
					continue;
				case 4:
					this.ᜄ(A_0, A_1, relationsCollection);
					num = 44;
					continue;
				case 5:
				{
					IChartFrameFormat chartArea = A_1.ChartArea;
					num = 43;
					continue;
				}
				case 6:
					if (A_1.PlotArea.IsBorderCornersRound)
					{
						num = 50;
						continue;
					}
					goto IL_456;
				case 7:
					if (A_1.IsEmbeded)
					{
						num = 4;
						continue;
					}
					goto IL_7EF;
				case 8:
					goto IL_370;
				case 9:
					A_0.WriteStartElement(RecordTableEnumerator.b("尼䨾㕀ⱂᅄ⹆㵈❊⡌୎㑐㽒ご⍖㱘㽚", a_), RecordTableEnumerator.b("唼䬾㕀㍂罄框晈㡊⹌❎㑐㹒㑔⑖睘㑚ⵜ㩞འ᭢ࡤ୦ཨѪὬɮၰݲٴ奶ᙸॺ᩼偾﶐벒ꞔꞖꦘ궚늜ﲞ즠슢힤펦", a_));
					num = 13;
					continue;
				case 10:
					this.ᜀ(A_0, A_1.Walls, RecordTableEnumerator.b("丼嘾╀♂ቄ♆╈❊", a_), A_1);
					this.ᜀ(A_0, A_1.Walls, RecordTableEnumerator.b("弼帾≀⡂ቄ♆╈❊", a_), A_1);
					num = 11;
					continue;
				case 11:
					goto IL_295;
				case 12:
					if (A_1 == null)
					{
						num = 31;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("帼", a_), RecordTableEnumerator.b("帼圾⁀ㅂㅄᑆ㥈⩊⹌⩎", a_), RecordTableEnumerator.b("唼䬾㕀㍂罄框晈㡊⹌❎㑐㹒㑔⑖睘㑚ⵜ㩞འ᭢ࡤ୦ཨѪὬɮၰݲٴ奶ᙸॺ᩼偾﶐벒ꞔꞖꦘ궚늜ﲞ즠슢힤펦", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("䔼刾ⵀⵂ㙄", a_), RecordTableEnumerator.b("尼", a_), null, RecordTableEnumerator.b("唼䬾㕀㍂罄框晈㡊⹌❎㑐㹒㑔⑖睘㑚ⵜ㩞འ᭢ࡤ୦ཨѪὬɮၰݲٴ奶ᙸॺ᩼偾﶐벒ꞔꞖꦘ궚늜삠쪢쮤", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("䔼刾ⵀⵂ㙄", a_), RecordTableEnumerator.b("似", a_), null, RecordTableEnumerator.b("唼䬾㕀㍂罄框晈㡊⹌❎㑐㹒㑔⑖睘㑚ⵜ㩞འ᭢ࡤ୦ཨѪὬɮၰݲٴ奶ᙸॺ᩼偾즌늜궞醠鎢鎤袦\udba8캪솬캮얰\udab2\udab4\ud9b6쪸펺풼쾾닀", a_));
					num = 29;
					continue;
				case 13:
					A_0.WriteAttributeString(RecordTableEnumerator.b("䬼帾ⵀ", a_), A_1.HasAutoTitle.Value ? RecordTableEnumerator.b("఼", a_) : RecordTableEnumerator.b("഼", a_));
					A_0.WriteEndElement();
					num = 46;
					continue;
				case 14:
					goto IL_5E1;
				case 16:
					num = 22;
					continue;
				case 17:
					goto IL_10A;
				case 18:
					if (A_1.AlternateContent != null)
					{
						num = 37;
						continue;
					}
					goto IL_398;
				case 19:
					if (A_1.Style > 0)
					{
						num = 32;
						continue;
					}
					goto IL_7A3;
				case 20:
					goto IL_1D4;
				case 21:
					if (A_1.HasChartArea)
					{
						num = 5;
						continue;
					}
					goto IL_2EA;
				case 22:
					if (A_1.HasFloor)
					{
						num = 3;
						continue;
					}
					goto IL_370;
				case 23:
					num = 6;
					continue;
				case 24:
					if (A_1.Series.Count <= 0)
					{
						num = 36;
						continue;
					}
					goto IL_6B6;
				case 25:
					if (flag != null)
					{
						num = 9;
						continue;
					}
					goto IL_161;
				case 26:
					if (A_1.HasLegend)
					{
						num = 49;
						continue;
					}
					goto IL_735;
				case 27:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1D4;
					default:
						if (false)
						{
						}
						A_0.WriteElementString(RecordTableEnumerator.b("䴼䴾⹀㝂⁄⑆㵈≊≌ⅎ", a_), RecordTableEnumerator.b("唼䬾㕀㍂罄框晈㡊⹌❎㑐㹒㑔⑖睘㑚ⵜ㩞འ᭢ࡤ୦ཨѪὬɮၰݲٴ奶ᙸॺ᩼偾﶐벒ꞔꞖꦘ궚늜ﲞ즠슢힤펦", a_), string.Empty);
						num = 14;
						continue;
					}
					break;
				case 28:
					if (A_1.InnerProtection != SheetProtectionType.None)
					{
						num = 27;
						continue;
					}
					goto IL_5E1;
				case 29:
					if (A_1.HasPlotArea)
					{
						num = 23;
						continue;
					}
					goto IL_456;
				case 30:
					goto IL_42E;
				case 31:
					goto IL_1CF;
				case 32:
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("丼䬾㡀⽂⁄", a_), A_1.Style.ToString());
					num = 33;
					continue;
				case 33:
					goto IL_7A3;
				case 34:
					goto IL_2EA;
				case 35:
					goto IL_3BE;
				case 36:
					num = 45;
					continue;
				case 37:
					A_1.AlternateContent.Position = 0L;
					ShapeParser.WriteNodeFromStream(A_0, A_1.AlternateContent);
					num = 41;
					continue;
				case 38:
					if (A_1.HasChartTitle)
					{
						num = 48;
						continue;
					}
					goto IL_1D4;
				case 39:
					goto IL_735;
				case 40:
					goto IL_3BE;
				case 41:
					goto IL_398;
				case 42:
				{
					IChartFrameFormat chartArea;
					spr\u1CFF.ᜀ(A_0, chartArea, A_1, chartArea.IsBorderCornersRound);
					num = 34;
					continue;
				}
				case 43:
				{
					IChartFrameFormat chartArea;
					if (chartArea != null)
					{
						num = 42;
						continue;
					}
					goto IL_2EA;
				}
				case 44:
					goto IL_1AC;
				case 45:
					if (!A_1.HasPivotTable)
					{
						num = 0;
						continue;
					}
					this.\u1713(A_0, A_1);
					num = 35;
					continue;
				case 46:
					goto IL_161;
				case 47:
					goto IL_42E;
				case 48:
					spr\u1CFF.ᜀ(A_0, A_1.ChartTitleArea, A_1.ParentWorkbook, relationsCollection, 18.0);
					num = 20;
					continue;
				case 49:
					this.ᜀ(A_0, A_1.Legend, A_1);
					num = 39;
					continue;
				case 50:
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("似倾㑀ⵂ⅄≆ⵈࡊ≌㵎㽐㙒❔⑖", a_), true);
					num = 47;
					continue;
				}
				if (A_0 == null)
				{
					num = 17;
					continue;
				}
				num = 12;
				continue;
				IL_161:
				this.\u1715(A_0, A_1);
				num = 24;
				continue;
				IL_1D4:
				flag = A_1.HasAutoTitle;
				num = 25;
				continue;
				IL_295:
				this.ᜃ(A_0, A_1, relationsCollection);
				if (true)
				{
				}
				num = 26;
				continue;
				IL_2EA:
				this.\u1717(A_0, A_1);
				this.ᜀ(A_0, A_1, A_2);
				this.\u1716(A_0, A_1);
				num = 7;
				continue;
				IL_370:
				num = 2;
				continue;
				IL_398:
				num = 19;
				continue;
				IL_3BE:
				num = 1;
				continue;
				IL_42E:
				num = 18;
				continue;
				IL_456:
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("似倾㑀ⵂ⅄≆ⵈࡊ≌㵎㽐㙒❔⑖", a_), false);
				num = 30;
				continue;
				IL_5E1:
				A_0.WriteStartElement(RecordTableEnumerator.b("帼圾⁀ㅂㅄ", a_), RecordTableEnumerator.b("唼䬾㕀㍂罄框晈㡊⹌❎㑐㹒㑔⑖睘㑚ⵜ㩞འ᭢ࡤ୦ཨѪὬɮၰݲٴ奶ᙸॺ᩼偾﶐벒ꞔꞖꦘ궚늜ﲞ즠슢힤펦", a_));
				sprᡟ sprᡟ = A_1.DataHolder;
				sprᡟ.ᜋ();
				relationsCollection = A_1.Relations;
				num = 38;
				continue;
				IL_6B6:
				this.\u1712(A_0, A_1);
				num = 40;
				continue;
				IL_735:
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("䴼匾⹀㝂ፄ⹆㩈ъ⍌⍎⡐", a_), A_1.PlotVisibleOnly);
				XLSXChartPlotEmpty displayBlanksAs = (XLSXChartPlotEmpty)A_1.DisplayBlanksAs;
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("夼嘾㉀㍂݄⭆⡈╊♌㱎ၐ⁒", a_), displayBlanksAs.ToString());
				A_0.WriteEndElement();
				num = 21;
				continue;
				IL_7A3:
				this.\u1714(A_0, A_1);
				num = 28;
			}
			IL_10A:
			throw new ArgumentNullException(RecordTableEnumerator.b("䨼䴾⡀㝂⁄㕆", a_));
			IL_1AC:
			goto IL_7EF;
			IL_1CF:
			throw new ArgumentNullException(RecordTableEnumerator.b("帼圾⁀ㅂㅄ", a_));
			IL_7EF:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x060054F9 RID: 21753 RVA: 0x0035B920 File Offset: 0x0035A920
	private void \u1717(XmlWriter A_0, XlsChart A_1)
	{
		int a_ = 17;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_43;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_35;
				}
				goto Block_3;
			case 2:
			{
				Stream stream;
				stream.Position = 0L;
				ShapeParser.WriteNodeFromStream(A_0, stream);
				num = 3;
				continue;
			}
			case 3:
				return;
			case 5:
			{
				Stream stream;
				if (stream != null)
				{
					num = 2;
					continue;
				}
				return;
			}
			case 6:
			{
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				Stream stream = A_1.DefaultTextProperty;
				if (true)
				{
				}
				num = 5;
				continue;
			}
			}
			IL_35:
			if (A_0 == null)
			{
				num = 0;
			}
			else
			{
				num = 6;
			}
		}
		IL_43:
		throw new ArgumentNullException(RecordTableEnumerator.b("う㭈≊㥌⩎⍐", a_));
		Block_3:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("⑆ⅈ⩊㽌㭎", a_));
	}

	// Token: 0x060054FA RID: 21754 RVA: 0x0035BA24 File Offset: 0x0035AA24
	private void \u1716(XmlWriter A_0, XlsChart A_1)
	{
		int a_ = 1;
		int num = 19;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1.DisplayEntireFieldButtons)
				{
					num = 1;
					continue;
				}
				goto IL_4BE;
			case 1:
				A_0.WriteStartElement(RecordTableEnumerator.b("吶࠸༺", a_), RecordTableEnumerator.b("匶䬸吺䴼放⹀ⵂ⁄㑆Ὀ≊㹌♎㍐㽒ご", a_), RecordTableEnumerator.b("弶䴸伺䴼Ծ湀求㙄⑆ⅈ⹊⁌⹎≐絒㡔㹖㩘⥚㉜ⱞ๠բᅤ䥦੨Ѫl䁮Ṱᕲ፴Ṷ᩸Ṻ剼᭾ꊌ붎ꆐꎒꊔ뢖ꆘ뒚꾜낞슠쮢쒤햦\udda8", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("䄶堸场", a_), RecordTableEnumerator.b("ض", a_));
				A_0.WriteEndElement();
				num = 12;
				continue;
			case 2:
				if (A_1.DisplayLegendFieldButtons)
				{
					num = 10;
					continue;
				}
				goto IL_32C;
			case 3:
				goto IL_211;
			case 4:
				A_0.WriteStartElement(RecordTableEnumerator.b("吶࠸༺", a_), RecordTableEnumerator.b("匶䬸吺䴼放⹀ⵂ⁄ņ⁈❊㥌⩎⍐", a_), RecordTableEnumerator.b("弶䴸伺䴼Ծ湀求㙄⑆ⅈ⹊⁌⹎≐絒㡔㹖㩘⥚㉜ⱞ๠բᅤ䥦੨Ѫl䁮Ṱᕲ፴Ṷ᩸Ṻ剼᭾ꊌ붎ꆐꎒꊔ뢖ꆘ뒚꾜낞슠쮢쒤햦\udda8", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("䄶堸场", a_), RecordTableEnumerator.b("ض", a_));
				A_0.WriteEndElement();
				num = 11;
				continue;
			case 5:
				goto IL_352;
			case 6:
				if (A_1.DisplayAxisFieldButtons)
				{
					num = 9;
					continue;
				}
				goto IL_216;
			case 7:
				A_0.WriteStartElement(RecordTableEnumerator.b("吶࠸༺", a_), RecordTableEnumerator.b("匶䬸吺䴼放⹀ⵂ⁄͆⡈㽊ⱌ", a_), RecordTableEnumerator.b("弶䴸伺䴼Ծ湀求㙄⑆ⅈ⹊⁌⹎≐絒㡔㹖㩘⥚㉜ⱞ๠բᅤ䥦੨Ѫl䁮Ṱᕲ፴Ṷ᩸Ṻ剼᭾ꊌ붎ꆐꎒꊔ뢖ꆘ뒚꾜낞슠쮢쒤햦\udda8", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("䄶堸场", a_), RecordTableEnumerator.b("ض", a_));
				A_0.WriteEndElement();
				num = 5;
				continue;
			case 8:
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				num = 16;
				continue;
			case 9:
				A_0.WriteStartElement(RecordTableEnumerator.b("吶࠸༺", a_), RecordTableEnumerator.b("匶䬸吺䴼放⹀ⵂ⁄ц⡈㽊⡌⡎㹐⅒㱔㉖⩘", a_), RecordTableEnumerator.b("弶䴸伺䴼Ծ湀求㙄⑆ⅈ⹊⁌⹎≐絒㡔㹖㩘⥚㉜ⱞ๠բᅤ䥦੨Ѫl䁮Ṱᕲ፴Ṷ᩸Ṻ剼᭾ꊌ붎ꆐꎒꊔ뢖ꆘ뒚꾜낞슠쮢쒤햦\udda8", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("䄶堸场", a_), RecordTableEnumerator.b("ض", a_));
				A_0.WriteEndElement();
				goto IL_169;
			case 10:
				A_0.WriteStartElement(RecordTableEnumerator.b("吶࠸༺", a_), RecordTableEnumerator.b("匶䬸吺䴼放⹀ⵂ⁄ᑆⱈ㥊⑌⩎≐", a_), RecordTableEnumerator.b("弶䴸伺䴼Ծ湀求㙄⑆ⅈ⹊⁌⹎≐絒㡔㹖㩘⥚㉜ⱞ๠բᅤ䥦੨Ѫl䁮Ṱᕲ፴Ṷ᩸Ṻ剼᭾ꊌ붎ꆐꎒꊔ뢖ꆘ뒚꾜낞슠쮢쒤햦\udda8", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("䄶堸场", a_), RecordTableEnumerator.b("ض", a_));
				A_0.WriteEndElement();
				num = 18;
				continue;
			case 11:
				goto IL_498;
			case 12:
				goto IL_29F;
			case 13:
				if (A_1.DisplayValueFieldButtons)
				{
					num = 7;
					continue;
				}
				goto IL_352;
			case 14:
				if (A_1.ShowReportFilterFieldButtons)
				{
					num = 4;
					continue;
				}
				goto IL_498;
			case 15:
				goto IL_216;
			case 16:
				if (!A_1.HasPivotTable)
				{
					num = 17;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("吶", a_), RecordTableEnumerator.b("制䄸伺焼䰾㕀", a_), RecordTableEnumerator.b("弶䴸伺䴼Ծ湀求㙄⑆ⅈ⹊⁌⹎≐絒㩔❖㱘㕚╜㉞ൠբ੤ᕦѨ੪ᥬᱮ彰ᱲݴၶ噸ὺོṾꊌ붎ꆐꎒꎔ뢖滛ﲜ햠", a_));
				A_0.WriteStartElement(RecordTableEnumerator.b("吶", a_), RecordTableEnumerator.b("制䄸伺", a_), RecordTableEnumerator.b("弶䴸伺䴼Ծ湀求㙄⑆ⅈ⹊⁌⹎≐絒㩔❖㱘㕚╜㉞ൠբ੤ᕦѨ੪ᥬᱮ彰ᱲݴၶ噸ὺོṾꊌ붎ꆐꎒꎔ뢖滛ﲜ햠", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("䈶䬸刺", a_), RecordTableEnumerator.b("䰶఼ุ̺績牀瑂灄煆摈ࡊ祌ൎ捐繒慔ᑖᡘᡚ灜晞╠啢卤䩦嵨⵪啬⵮㕰䭲䍴䑶乸㽺䱼䥾ﲀ", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("伶吸场匼䰾", a_), RecordTableEnumerator.b("吶࠸༺", a_), null, RecordTableEnumerator.b("弶䴸伺䴼Ծ湀求㙄⑆ⅈ⹊⁌⹎≐絒㡔㹖㩘⥚㉜ⱞ๠բᅤ䥦੨Ѫl䁮Ṱᕲ፴Ṷ᩸Ṻ剼᭾ꊌ붎ꆐꎒꊔ뢖ꆘ뒚꾜낞슠쮢쒤햦\udda8", a_));
				A_0.WriteStartElement(RecordTableEnumerator.b("吶࠸༺", a_), RecordTableEnumerator.b("䜶倸䴺刼䬾เ㍂ㅄ⹆♈╊㹌", a_), RecordTableEnumerator.b("弶䴸伺䴼Ծ湀求㙄⑆ⅈ⹊⁌⹎≐絒㡔㹖㩘⥚㉜ⱞ๠բᅤ䥦੨Ѫl䁮Ṱᕲ፴Ṷ᩸Ṻ剼᭾ꊌ붎ꆐꎒꊔ뢖ꆘ뒚꾜낞슠쮢쒤햦\udda8", a_));
				num = 14;
				continue;
			case 17:
				return;
			case 18:
				goto IL_32C;
			case 20:
				goto IL_A4;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_169:
				num = 15;
				continue;
			}
			if (false)
			{
			}
			if (A_0 == null)
			{
				num = 20;
				continue;
			}
			num = 8;
			continue;
			IL_216:
			num = 13;
			continue;
			IL_32C:
			num = 0;
			continue;
			IL_352:
			num = 2;
			continue;
			IL_498:
			num = 6;
		}
		IL_A4:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䀶䬸刺䤼娾㍀", a_));
		IL_211:
		throw new ArgumentNullException(RecordTableEnumerator.b("吶儸娺似䬾", a_));
		IL_29F:
		IL_4BE:
		A_0.WriteEndElement();
		A_0.WriteEndElement();
		A_0.WriteEndElement();
	}

	// Token: 0x060054FB RID: 21755 RVA: 0x0035BF04 File Offset: 0x0035AF04
	private void \u1715(XmlWriter A_0, XlsChart A_1)
	{
		int a_ = 7;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				goto IL_43;
			case 2:
			{
				Stream pivotFormatsStream;
				if (pivotFormatsStream != null)
				{
					num = 4;
					continue;
				}
				return;
			}
			case 4:
			{
				Stream pivotFormatsStream;
				pivotFormatsStream.Position = 0L;
				ShapeParser.WriteNodeFromStream(A_0, pivotFormatsStream);
				num = 0;
				continue;
			}
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_7A;
				}
				break;
			case 6:
			{
				if (true)
				{
				}
				if (A_0 == null)
				{
					num = 5;
					continue;
				}
				Stream pivotFormatsStream = A_1.PivotFormatsStream;
				num = 2;
				continue;
			}
			}
			if (A_1 == null)
			{
				num = 1;
			}
			else
			{
				num = 6;
			}
		}
		IL_43:
		throw new ArgumentNullException(RecordTableEnumerator.b("帼圾⁀ㅂㅄ", a_));
		IL_7A:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䨼䴾⡀㝂⁄㕆", a_));
	}

	// Token: 0x060054FC RID: 21756 RVA: 0x0035C008 File Offset: 0x0035B008
	private void \u1714(XmlWriter A_0, XlsChart A_1)
	{
		int a_ = 13;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
			{
				string text;
				if (text != null)
				{
					num = 5;
					continue;
				}
				return;
			}
			case 2:
				goto IL_171;
			case 3:
			{
				IPivotTable pivotTable;
				if (pivotTable != null)
				{
					num = 7;
					continue;
				}
				goto IL_171;
			}
			case 4:
			{
				if (A_0 == null)
				{
					goto IL_C5;
				}
				IPivotTable pivotTable = A_1.PivotTable;
				string text = A_1.PreservedPivotSource;
				num = 3;
				continue;
			}
			case 5:
			{
				if (true)
				{
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("⁂", a_), RecordTableEnumerator.b("㍂ⱄㅆ♈㽊Ṍ⁎⑐⅒㙔㉖", a_), RecordTableEnumerator.b("⭂ㅄ㍆㥈煊扌恎≐げ㵔㉖㑘㩚⹜煞๠።d०ᅨ٪Ŭ८ṰŲᡴᙶ൸ࡺ卼ၾꪄﮈ戴ﾐﮖ뚘ꦚ궜꾞鞠貢욤쾦좨\ud9aa\ud9ac", a_));
				A_0.WriteStartElement(RecordTableEnumerator.b("⁂", a_), RecordTableEnumerator.b("ⵂ⑄⩆ⱈ", a_), RecordTableEnumerator.b("⭂ㅄ㍆㥈煊扌恎≐げ㵔㉖㑘㩚⹜煞๠።d०ᅨ٪Ŭ८ṰŲᡴᙶ൸ࡺ卼ၾꪄﮈ戴ﾐﮖ뚘ꦚ궜꾞鞠貢욤쾦좨\ud9aa\ud9ac", a_));
				string text;
				A_0.WriteString(text);
				A_0.WriteEndElement();
				A_0.WriteEndElement();
				num = 9;
				continue;
			}
			case 6:
				goto IL_D0;
			case 7:
			{
				IPivotTable pivotTable;
				string text = spr\u2541.ᜀ(pivotTable);
				num = 2;
				continue;
			}
			case 8:
				goto IL_72;
			case 9:
				return;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_C5:
				num = 6;
				continue;
			default:
				if (false)
				{
				}
				if (A_1 == null)
				{
					num = 8;
					continue;
				}
				num = 4;
				continue;
			}
			IL_171:
			num = 1;
		}
		IL_72:
		throw new ArgumentNullException(RecordTableEnumerator.b("⁂ⵄ♆㭈㽊", a_));
		IL_D0:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑂㝄⹆㵈⹊㽌", a_));
	}

	// Token: 0x060054FD RID: 21757 RVA: 0x0035C1B8 File Offset: 0x0035B1B8
	private static string ᜀ(IPivotTable A_0)
	{
		int a_ = 10;
		while (A_0 == null)
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
				throw new ArgumentNullException(RecordTableEnumerator.b("〿⭁㉃⥅㱇ṉⵋⱍ㱏㝑", a_));
			}
		}
		XlsPivotTable xlsPivotTable = A_0 as XlsPivotTable;
		IWorksheet worksheet = xlsPivotTable.Worksheet;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(RecordTableEnumerator.b("ᬿ牁᥃", a_));
		stringBuilder.Append(worksheet.Name);
		stringBuilder.Append('!');
		stringBuilder.Append(A_0.Name);
		return stringBuilder.ToString();
	}

	// Token: 0x060054FE RID: 21758 RVA: 0x0035C268 File Offset: 0x0035B268
	private void ᜀ(XmlWriter A_0, XlsChart A_1, string A_2)
	{
		int a_ = 0;
		int num = 4;
		RelationsCollection relationsCollection;
		string text;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_103;
			case 1:
				if (A_1.DataHolder.ᜀ(A_1, relationsCollection, text, RecordTableEnumerator.b("圵䠷䨹倻圽⌿⍁ぃ⽅❇⑉捋㡍㹏㙑穓㥕⡗㽙㉛♝ൟ๡ɣ॥ᩧݩ൫ᩭͯ影᭳ၵṷ፹ύ᭽揄뺏劣춟캡誣얥삧쮩\udeab\udaad쎯\udab1햳욵\uddb7즹鞻욽궿껁", a_), RecordTableEnumerator.b("帵䰷丹䰻н漿流㝃╅⁇⽉⅋⽍⍏籑㭓♕㵗㑙⑛㍝౟ѡୣᑥէ୩ᡫᵭ幯ᵱٳᅵ坷ᕹ᩻᡽슅曆ﲑ릕ꪗꪙ겛ꢝ辟킡솣쪥즧\udea9얫솭\udeaf솱\udcb3\udfb5좷즹鎻\uddbdꢿꏁ뛃닅鷇막꧋볍菏뫑뗓ꛕ뷗꧙", a_)))
				{
					num = 0;
					continue;
				}
				goto IL_1EC;
			case 2:
				return;
			case 3:
				goto IL_50;
			case 5:
				if (text == null)
				{
					num = 10;
					continue;
				}
				goto IL_BE;
			case 6:
				goto IL_BE;
			case 7:
				if (A_1 == null)
				{
					num = 9;
					continue;
				}
				num = 8;
				continue;
			case 8:
			{
				if (A_1.Shapes.Count - A_1.VmlShapesCount <= 0)
				{
					num = 2;
					continue;
				}
				sprᡟ sprᡟ = A_1.DataHolder;
				relationsCollection = A_1.Relations;
				text = sprᡟ.ᜊ();
				num = 5;
				continue;
			}
			case 9:
				goto IL_B9;
			case 10:
			{
				if (true)
				{
				}
				sprᡟ sprᡟ;
				sprᡟ.ᜃ(text = relationsCollection.GenerateRelationId());
				relationsCollection[text] = null;
				num = 6;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			num = 7;
			continue;
			IL_BE:
			num = 1;
		}
		IL_50:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return;
		default:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䄵䨷匹䠻嬽㈿", a_));
		}
		IL_B9:
		throw new ArgumentNullException(RecordTableEnumerator.b("唵倷嬹主䨽", a_));
		IL_103:
		A_0.WriteStartElement(RecordTableEnumerator.b("䌵䬷弹主洽⠿⍁㑃⍅㭇", a_), RecordTableEnumerator.b("帵䰷丹䰻н漿流㝃╅⁇⽉⅋⽍⍏籑㭓♕㵗㑙⑛㍝౟ѡୣᑥէ୩ᡫᵭ幯ᵱٳᅵ坷ṹ๻ώꎋ벍ꂏꊑꊓ릕ﮗﶛ풟", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("張尷", a_), RecordTableEnumerator.b("帵䰷丹䰻н漿流㝃╅⁇⽉⅋⽍⍏籑㭓♕㵗㑙⑛㍝౟ѡୣᑥէ୩ᡫᵭ幯ᵱٳᅵ坷ᕹ᩻᡽슅曆ﲑ릕ꪗꪙ겛ꢝ辟킡솣쪥즧\udea9얫솭\udeaf솱\udcb3\udfb5좷즹", a_), text);
		A_0.WriteEndElement();
		return;
		IL_1EC:
		relationsCollection.Remove(text);
	}

	// Token: 0x060054FF RID: 21759 RVA: 0x0035C468 File Offset: 0x0035B468
	private void ᜄ(XmlWriter A_0, XlsChart A_1, RelationsCollection A_2)
	{
		int a_ = 16;
		int num = 3;
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
					num = 1;
					continue;
				}
				goto IL_A1;
			case 1:
				goto IL_6F;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_87;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 2;
			}
			else
			{
				num = 0;
			}
		}
		IL_6F:
		throw new ArgumentNullException(RecordTableEnumerator.b("╅⁇⭉㹋㩍", a_));
		IL_87:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅅ㩇⍉㡋⭍≏", a_));
		IL_A1:
		IChartPageSetup pageSetup = A_1.PageSetup;
		A_0.WriteStartElement(RecordTableEnumerator.b("㙅㩇⍉≋㩍͏㝑⁓≕ㅗ㑙㭛ⵝ", a_), RecordTableEnumerator.b("⹅㱇㹉㱋瑍罏絑❓㕕し㽙ㅛ㽝፟䱡ୣᙥ൧ѩᑫͭᱯᑱ᭳ѵᕷ᭹ࡻൽ깿ꞇﺋﮑ望뎛겝邟銡銣覥쮧슩춫\udcad쒯", a_));
		spr\u171C a_2 = new spr\u1A61();
		spr\u1B7A.ᜄ(A_0, pageSetup, a_2);
		spr\u1B7A.ᜀ(A_0, A_1, a_2, A_2);
		A_1.DataHolder.ᜀ(A_1, A_2);
		A_0.WriteEndElement();
	}

	// Token: 0x06005500 RID: 21760 RVA: 0x0035C56C File Offset: 0x0035B56C
	private void ᜀ(XmlWriter A_0, IChartLegend A_1, XlsChart A_2)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				bool flag;
				int num2;
				int count;
				IChartLegendEntries legendEntries;
				IWorkbook workbook;
				switch (num)
				{
				case 0:
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("倾㝀♂㝄⭆⡈㉊", a_), RecordTableEnumerator.b("฾", a_));
					num = 2;
					continue;
				case 2:
					goto IL_1BF;
				case 3:
					goto IL_341;
				case 4:
				{
					LegendPositionType position;
					XLSXLegendPosition xlsxlegendPosition = (XLSXLegendPosition)position;
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("匾⑀⑂⁄⥆ⵈᭊ≌㱎", a_), xlsxlegendPosition.ToString());
					num = 3;
					continue;
				}
				case 5:
					goto IL_1BF;
				case 6:
					goto IL_36B;
				case 7:
					A_0.WriteStartElement(RecordTableEnumerator.b("匾⁀㩂⩄㉆㵈", a_), RecordTableEnumerator.b("圾㕀㝂㕄絆晈摊㹌ⱎ㥐㙒㡔㙖⩘畚㉜⽞Ѡൢᵤ੦ը൪ɬᵮᱰቲŴѶ坸ᑺོ᡾꺀ﺈﲐﾒ몔ꖖꦘꮚꮜ낞슠쮢쒤햦\udda8", a_));
					A_0.WriteEndElement();
					num = 13;
					continue;
				case 8:
					if (!A_1.IncludeInLayout)
					{
						num = 0;
						continue;
					}
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("倾㝀♂㝄⭆⡈㉊", a_), RecordTableEnumerator.b("༾", a_));
					num = 5;
					continue;
				case 9:
					if (flag)
					{
						num = 22;
						continue;
					}
					goto IL_3C4;
				case 10:
				{
					if (A_2 == null)
					{
						num = 11;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("匾⑀⑂⁄⥆ⵈ", a_), RecordTableEnumerator.b("圾㕀㝂㕄絆晈摊㹌ⱎ㥐㙒㡔㙖⩘畚㉜⽞Ѡൢᵤ੦ը൪ɬᵮᱰቲŴѶ坸ᑺོ᡾꺀ﺈﲐﾒ몔ꖖꦘꮚꮜ낞슠쮢쒤햦\udda8", a_));
					LegendPositionType position = A_1.Position;
					num = 21;
					continue;
				}
				case 11:
					goto IL_10D;
				case 12:
					goto IL_BF;
				case 13:
					goto IL_36B;
				case 14:
					if (A_1 == null)
					{
						num = 23;
						continue;
					}
					num = 10;
					continue;
				case 15:
					goto IL_BF;
				case 16:
				{
					Stream stream = (A_1 as XlsChartLegend).LayoutStream;
					num = 18;
					continue;
				}
				case 17:
					goto IL_29B;
				case 18:
				{
					Stream stream;
					if (stream == null)
					{
						num = 7;
						continue;
					}
					stream.Position = 0L;
					ShapeParser.WriteNodeFromStream(A_0, stream);
					num = 6;
					continue;
				}
				case 19:
					if (num2 >= count)
					{
						num = 16;
						continue;
					}
					this.ᜀ(A_0, legendEntries[num2], num2, workbook);
					num2++;
					num = 12;
					continue;
				case 20:
					goto IL_98;
				case 21:
				{
					LegendPositionType position;
					if (position != LegendPositionType.NotDocked)
					{
						num = 4;
						continue;
					}
					goto IL_341;
				}
				case 22:
					this.ᜀ(A_0, A_1.TextArea, workbook, 10.0);
					num = 17;
					continue;
				case 23:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8F;
					default:
						goto IL_17B;
					}
					break;
				}
				goto IL_89;
				IL_8F:
				num = 20;
				continue;
				IL_89:
				if (A_0 == null)
				{
					goto IL_8F;
				}
				num = 14;
				continue;
				IL_BF:
				num = 19;
				continue;
				IL_1BF:
				spr\u1CFF.ᜀ(A_0, (A_1 as XlsChartLegend).FrameFormat, A_2, false);
				flag = (((sprᮟ)A_1.TextArea).ᜃ() == ChartParagraphType.Default);
				num = 9;
				continue;
				IL_341:
				legendEntries = A_1.LegendEntries;
				workbook = A_2.Workbook;
				num2 = 0;
				count = legendEntries.Count;
				num = 15;
				continue;
				IL_36B:
				num = 8;
			}
			IL_98:
			throw new ArgumentNullException(RecordTableEnumerator.b("䠾㍀⩂ㅄ≆㭈", a_));
			IL_10D:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("尾⥀≂㝄㍆", a_));
			IL_17B:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("匾⑀⑂⁄⥆ⵈ", a_));
			IL_29B:
			IL_3C4:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06005501 RID: 21761 RVA: 0x0035C944 File Offset: 0x0035B944
	private void ᜀ(XmlWriter A_0, IChartLegendEntry A_1, int A_2, IWorkbook A_3)
	{
		int a_ = 6;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_63;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_E5;
				default:
					if (false)
					{
					}
					if (A_1 == null)
					{
						num = 3;
						continue;
					}
					num = 6;
					continue;
				}
				break;
			case 2:
				goto IL_88;
			case 3:
				goto IL_11D;
			case 4:
				num = 13;
				continue;
			case 6:
				if (true)
				{
				}
				if (!A_1.IsDeleted)
				{
					num = 4;
					continue;
				}
				goto IL_88;
			case 7:
				if (A_1.IsFormatted)
				{
					num = 8;
					continue;
				}
				goto IL_180;
			case 8:
				this.ᜀ(A_0, A_1.TextArea, A_3, 10.0);
				num = 14;
				continue;
			case 9:
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("堻嬽ⰿ❁ぃ⍅", a_), true);
				num = 12;
				continue;
			case 10:
				if (A_1.IsDeleted)
				{
					num = 9;
					continue;
				}
				goto IL_68;
			case 11:
				return;
			case 12:
				goto IL_68;
			case 13:
				if (A_1.IsFormatted)
				{
					num = 2;
					continue;
				}
				return;
			case 14:
				goto IL_180;
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			goto IL_E5;
			IL_68:
			num = 7;
			continue;
			IL_88:
			A_0.WriteStartElement(RecordTableEnumerator.b("倻嬽✿❁⩃≅േ⑉㡋㱍⥏", a_), RecordTableEnumerator.b("吻䨽㐿㉁繃楅杇㥉⽋♍㕏㽑㕓╕癗㕙ⱛ㭝๟ᩡॣ੥๧թṫͭᅯٱݳ塵᝷ࡹ᭻兽ﲏ붑ꚓꚕꢗ겙뎛ﶝ좟쎡횣튥", a_));
			spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("唻娽㠿", a_), A_2.ToString());
			num = 10;
			continue;
			IL_E5:
			num = 1;
			continue;
			IL_180:
			A_0.WriteEndElement();
			num = 11;
		}
		IL_63:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬻䰽⤿㙁⅃㑅", a_));
		IL_11D:
		throw new ArgumentNullException(RecordTableEnumerator.b("倻嬽✿❁⩃≅േ⑉㡋㱍⥏", a_));
	}

	// Token: 0x06005502 RID: 21762 RVA: 0x0035CB50 File Offset: 0x0035BB50
	private void \u1713(XmlWriter A_0, XlsChart A_1)
	{
		int a_ = 19;
		int num = 21;
		string a_2;
		string a_3;
		for (;;)
		{
			string text;
			string text2;
			switch (num)
			{
			case 0:
				text = RecordTableEnumerator.b("灈筊", a_);
				goto IL_230;
			case 1:
				if (A_1.PivotChartType != ExcelChartType.Surface3D)
				{
					goto IL_249;
				}
				goto IL_F3;
			case 2:
				goto IL_28A;
			case 3:
				if (A_1.PivotChartType != ExcelChartType.Surface3DNoColor)
				{
					num = 10;
					continue;
				}
				goto IL_29A;
			case 4:
				if (!A_1.IsPivot3DChart)
				{
					num = 11;
					continue;
				}
				num = 19;
				continue;
			case 5:
				num = 17;
				continue;
			case 6:
				num = 3;
				continue;
			case 7:
				if (A_1.PivotChartType != ExcelChartType.Surface3DNoColor)
				{
					num = 8;
					continue;
				}
				goto IL_F3;
			case 8:
				num = 16;
				continue;
			case 9:
				num = 0;
				continue;
			case 10:
				num = 2;
				continue;
			case 11:
				return;
			case 12:
				num = 7;
				continue;
			case 13:
				goto IL_8F;
			case 14:
				if (A_1 == null)
				{
					num = 15;
					continue;
				}
				num = 4;
				continue;
			case 15:
				goto IL_1D8;
			case 16:
				text2 = RecordTableEnumerator.b("祈", a_);
				goto IL_1EE;
			case 17:
				if (A_1.PivotChartType != ExcelChartType.Surface3DNoColor)
				{
					num = 9;
					continue;
				}
				goto IL_176;
			case 18:
				if (A_1.PivotChartType != ExcelChartType.Surface3D)
				{
					num = 6;
					continue;
				}
				goto IL_29A;
			case 19:
				if (A_1.PivotChartType != ExcelChartType.Surface3D)
				{
					num = 5;
					continue;
				}
				goto IL_176;
			case 20:
				goto IL_2A5;
			case 22:
				text = RecordTableEnumerator.b("硈繊", a_);
				goto IL_230;
			case 23:
				text2 = RecordTableEnumerator.b("筈筊", a_);
				goto IL_1EE;
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 13;
				continue;
			}
			num = 14;
			continue;
			IL_F3:
			num = 23;
			continue;
			IL_176:
			num = 22;
			continue;
			IL_1EE:
			a_2 = text2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_249:
				num = 12;
				continue;
			default:
				if (false)
				{
				}
				num = 18;
				continue;
			}
			IL_230:
			a_3 = text;
			num = 1;
			continue;
			IL_29A:
			num = 20;
		}
		IL_8F:
		throw new ArgumentNullException(RecordTableEnumerator.b("㹈㥊⑌㭎㑐⅒", a_));
		IL_1D8:
		throw new ArgumentNullException(RecordTableEnumerator.b("⩈⍊ⱌ㵎═", a_));
		IL_28A:
		string text3 = RecordTableEnumerator.b("祈", a_);
		goto IL_2B5;
		IL_2A5:
		text3 = RecordTableEnumerator.b("穈筊", a_);
		IL_2B5:
		string a_4 = text3;
		A_0.WriteStartElement(RecordTableEnumerator.b("㽈≊⡌㡎扐ᝒ", a_), RecordTableEnumerator.b("ⅈ㽊㥌㽎歐籒穔⑖㩘㍚㡜㉞`ၢ䭤ࡦᥨ๪ͬᝮᱰὲ፴ᡶ୸ᙺᱼ୾궂ꒊﶎﲔ練ﺘ낞鎠鎢閤醦蚨좪얬캮쎰잲", a_));
		spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("㭈⑊㥌ᝎ", a_), a_3);
		spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("㭈⑊㥌ᙎ", a_), a_2);
		spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("㭈੊⍌⡎ၐ⭒", a_), false);
		spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("㥈⹊㽌㱎⅐㙒㙔⍖じⵚ㡜", a_), a_4);
		A_0.WriteEndElement();
	}

	// Token: 0x06005503 RID: 21763 RVA: 0x0035CE90 File Offset: 0x0035BE90
	private void \u1712(XmlWriter A_0, XlsChart A_1)
	{
		int a_ = 0;
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
				{
					if (!A_1.IsChart3D)
					{
						num = 7;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("䀵儷弹䬻ഽп", a_), RecordTableEnumerator.b("帵䰷丹䰻н漿流㝃╅⁇⽉⅋⽍⍏籑㭓♕㵗㑙⑛㍝౟ѡୣᑥէ୩ᡫᵭ幯ᵱٳᅵ坷ṹ๻ώꎋ벍ꂏꊑꊓ릕ﮗﶛ풟", a_));
					XlsChartFormat xlsChartFormat = A_1.XlsChartFormat;
					num = 16;
					continue;
				}
				case 1:
					goto IL_E7;
				case 2:
					goto IL_7C;
				case 3:
					goto IL_15D;
				case 4:
				{
					XlsChartFormat xlsChartFormat;
					if (!xlsChartFormat.IsDefaultRotation)
					{
						num = 6;
						continue;
					}
					goto IL_276;
				}
				case 5:
					goto IL_1B3;
				case 6:
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("䐵圷丹攻", a_), A_1.Rotation.ToString());
					num = 5;
					continue;
				case 7:
					return;
				case 8:
					if (A_1 != null)
					{
						num = 0;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_174;
					default:
						if (false)
						{
						}
						num = 10;
						continue;
					}
					break;
				case 9:
					num = 12;
					continue;
				case 10:
					goto IL_158;
				case 11:
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("䐵圷丹搻", a_), A_1.Elevation.ToString());
					num = 3;
					continue;
				case 12:
					if (!A_1.AutoScaling)
					{
						num = 13;
						continue;
					}
					goto IL_E7;
				case 13:
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("帵样弹主崽┿ⱁぃ", a_), A_1.HeightPercent.ToString());
					num = 1;
					continue;
				case 15:
					if (A_1.RightAngleAxes)
					{
						goto IL_174;
					}
					goto IL_E7;
				case 16:
				{
					XlsChartFormat xlsChartFormat;
					if (!xlsChartFormat.IsDefaultElevation)
					{
						num = 11;
						continue;
					}
					goto IL_15D;
				}
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				num = 8;
				continue;
				IL_E7:
				if (true)
				{
				}
				num = 4;
				continue;
				IL_15D:
				num = 15;
				continue;
				IL_174:
				num = 9;
			}
			IL_7C:
			throw new ArgumentNullException(RecordTableEnumerator.b("䄵䨷匹䠻嬽㈿", a_));
			IL_158:
			throw new ArgumentNullException(RecordTableEnumerator.b("唵倷嬹主䨽", a_));
			IL_1B3:
			IL_276:
			spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("刵崷䨹䠻嘽ဿ❁㙃╅ⵇ⑉㡋", a_), A_1.DepthPercent.ToString());
			spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("䐵礷吹嬻缽㠿", a_), A_1.RightAngleAxes);
			spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("䘵崷䠹伻丽┿⅁ぃ⽅㹇⽉", a_), (A_1.Perspective * 2).ToString());
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06005504 RID: 21764 RVA: 0x0035D17C File Offset: 0x0035C17C
	private void ᜀ(XmlWriter A_0, IChartErrorBars A_1, string A_2, IWorkbook A_3, XlsChartSerie A_4)
	{
		int a_ = 19;
		switch (0)
		{
		default:
		{
			int num = 13;
			for (;;)
			{
				IChartBorder border;
				XLSXErrorBarType type;
				switch (num)
				{
				case 0:
					num = 16;
					continue;
				case 1:
					if (A_1 == null)
					{
						num = 19;
						continue;
					}
					num = 4;
					continue;
				case 2:
					goto IL_C2;
				case 3:
				{
					spr\u237B spr_u237B;
					this.ᜀ(A_0, A_1.MinusRange, spr_u237B.ᜄ(), A_4);
					num = 25;
					continue;
				}
				case 4:
					if (A_2 != null)
					{
						num = 0;
						continue;
					}
					goto IL_1FE;
				case 5:
					num = 32;
					continue;
				case 6:
					if (border != null)
					{
						num = 20;
						continue;
					}
					goto IL_4E6;
				case 7:
					goto IL_111;
				case 8:
					A_0.WriteStartElement(RecordTableEnumerator.b("㩈㭊ᵌ㵎", a_), RecordTableEnumerator.b("ⅈ㽊㥌㽎歐籒穔⑖㩘㍚㡜㉞`ၢ䭤ࡦᥨ๪ͬᝮᱰὲ፴ᡶ୸ᙺᱼ୾궂ꒊﶎﲔ練ﺘ낞鎠鎢閤醦蚨좪얬캮쎰잲", a_));
					spr\u1CFF.ᜀ(A_0, border, A_3);
					A_0.WriteEndElement();
					num = 18;
					continue;
				case 9:
					goto IL_3BE;
				case 10:
				{
					spr\u237B spr_u237B;
					this.ᜀ(A_0, A_1.PlusRange, spr_u237B.ᜋ(), A_4);
					num = 7;
					continue;
				}
				case 11:
				{
					spr\u237B spr_u237B;
					if (!spr_u237B.\u170D())
					{
						num = 3;
						continue;
					}
					this.ᜀ(A_0, spr_u237B.ᜄ(), false);
					num = 23;
					continue;
				}
				case 12:
					if (true)
					{
					}
					num = 22;
					continue;
				case 14:
					num = 31;
					continue;
				case 15:
					goto IL_49F;
				case 16:
				{
					if (A_2.Length == 0)
					{
						num = 24;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("ⱈ㥊㽌ൎぐ⅒♔", a_), RecordTableEnumerator.b("ⅈ㽊㥌㽎歐籒穔⑖㩘㍚㡜㉞`ၢ䭤ࡦᥨ๪ͬᝮᱰὲ፴ᡶ୸ᙺᱼ୾궂ꒊﶎﲔ練ﺘ낞鎠鎢閤醦蚨좪얬캮쎰잲", a_));
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("ⱈ㥊㽌୎㡐⅒", a_), A_2);
					ErrorBarIncludeType include = A_1.Include;
					string a_2 = include.ToString().ToLower();
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("ⱈ㥊㽌ൎぐ⅒Ŕ⹖⥘㹚", a_), a_2);
					spr\u237B spr_u237B = A_1 as spr\u237B;
					type = (XLSXErrorBarType)A_1.Type;
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("ⱈ㥊㽌᥎ぐ㽒Ŕ⹖⥘㹚", a_), type.ToString());
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("❈⑊ࡌⅎ㕐ၒ㑔❖", a_), !A_1.HasCap);
					num = 9;
					continue;
				}
				case 17:
				{
					spr\u237B spr_u237B;
					if (!spr_u237B.ᜎ())
					{
						num = 10;
						continue;
					}
					this.ᜀ(A_0, spr_u237B.ᜋ(), false);
					num = 21;
					continue;
				}
				case 18:
					goto IL_2A5;
				case 19:
					return;
				case 20:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3BE;
					default:
						if (false)
						{
						}
						num = 28;
						continue;
					}
					break;
				case 21:
					goto IL_111;
				case 22:
				{
					ErrorBarIncludeType include;
					if (include == ErrorBarIncludeType.Both)
					{
						num = 30;
						continue;
					}
					goto IL_212;
				}
				case 23:
					goto IL_150;
				case 24:
					goto IL_14B;
				case 25:
					goto IL_150;
				case 26:
				{
					ErrorBarIncludeType include;
					if (include != ErrorBarIncludeType.Minus)
					{
						num = 12;
						continue;
					}
					goto IL_C7;
				}
				case 27:
					goto IL_212;
				case 28:
					if (!border.UseDefaultFormat)
					{
						num = 8;
						continue;
					}
					goto IL_4E6;
				case 29:
					goto IL_439;
				case 30:
					goto IL_C7;
				case 31:
				{
					ErrorBarIncludeType include;
					if (include == ErrorBarIncludeType.Both)
					{
						num = 29;
						continue;
					}
					goto IL_49F;
				}
				case 32:
				{
					ErrorBarIncludeType include;
					if (include != ErrorBarIncludeType.Plus)
					{
						num = 14;
						continue;
					}
					goto IL_439;
				}
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				num = 1;
				continue;
				IL_C7:
				A_0.WriteStartElement(RecordTableEnumerator.b("⑈≊⍌㩎≐", a_), RecordTableEnumerator.b("ⅈ㽊㥌㽎歐籒穔⑖㩘㍚㡜㉞`ၢ䭤ࡦᥨ๪ͬᝮᱰὲ፴ᡶ୸ᙺᱼ୾궂ꒊﶎﲔ練ﺘ낞鎠鎢閤醦蚨좪얬캮쎰잲", a_));
				num = 11;
				continue;
				IL_111:
				A_0.WriteEndElement();
				num = 15;
				continue;
				IL_150:
				A_0.WriteEndElement();
				num = 27;
				continue;
				IL_212:
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("㽈⩊⅌", a_), XmlConvert.ToString(A_1.NumberValue));
				border = A_1.Border;
				num = 6;
				continue;
				IL_3BE:
				if (type == XLSXErrorBarType.cust)
				{
					num = 5;
					continue;
				}
				goto IL_212;
				IL_439:
				A_0.WriteStartElement(RecordTableEnumerator.b("㥈❊㡌㱎", a_), RecordTableEnumerator.b("ⅈ㽊㥌㽎歐籒穔⑖㩘㍚㡜㉞`ၢ䭤ࡦᥨ๪ͬᝮᱰὲ፴ᡶ୸ᙺᱼ୾궂ꒊﶎﲔ練ﺘ낞鎠鎢閤醦蚨좪얬캮쎰잲", a_));
				num = 17;
				continue;
				IL_49F:
				num = 26;
			}
			IL_C2:
			throw new ArgumentNullException(RecordTableEnumerator.b("㹈㥊⑌㭎㑐⅒", a_));
			IL_14B:
			IL_1FE:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⵈ≊㽌⩎㉐❒㱔㡖㝘", a_));
			IL_2A5:
			IL_4E6:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06005505 RID: 21765 RVA: 0x0035D678 File Offset: 0x0035C678
	private void ᜀ(XmlWriter A_0, IChartTrendLines A_1, IWorkbook A_2)
	{
		int a_ = 18;
		int num = 6;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_49;
				default:
					goto IL_BD;
				}
				break;
			case 1:
				goto IL_8D;
			case 2:
			{
				int count;
				if (num2 >= count)
				{
					num = 0;
					continue;
				}
				goto IL_49;
			}
			case 3:
				goto IL_8D;
			case 4:
			{
				if (A_1 == null)
				{
					num = 7;
					continue;
				}
				num2 = 0;
				int count = A_1.Count;
				num = 3;
				continue;
			}
			case 5:
				goto IL_47;
			case 7:
				return;
			}
			if (A_0 == null)
			{
				num = 5;
				continue;
			}
			num = 4;
			continue;
			IL_49:
			this.ᜀ(A_0, A_1[num2], A_2);
			num2++;
			if (true)
			{
			}
			num = 1;
			continue;
			IL_8D:
			num = 2;
		}
		IL_47:
		throw new ArgumentNullException(RecordTableEnumerator.b("㽇㡉╋㩍㕏⁑", a_));
		IL_BD:
		if (false)
		{
		}
	}

	// Token: 0x06005506 RID: 21766 RVA: 0x0035D77C File Offset: 0x0035C77C
	private void ᜀ(XmlWriter A_0, IChartTrendLine A_1, IWorkbook A_2)
	{
		int a_ = 4;
		switch (0)
		{
		default:
		{
			int num = 25;
			for (;;)
			{
				string text;
				IChartBorder border;
				switch (num)
				{
				case 0:
					text = RecordTableEnumerator.b("唹主娽┿ぁ", a_);
					num = 12;
					continue;
				case 1:
					goto IL_1EB;
				case 2:
					A_0.WriteStartElement(RecordTableEnumerator.b("䤹䰻渽㈿", a_), RecordTableEnumerator.b("刹䠻䨽〿硁歃楅㭇⥉⑋⭍㵏㍑❓硕㝗⩙㥛そᡟཡࡣeݧᡩū཭ѯű婳᥵੷ᵹ卻᩽뾏ꂑ꒓ꚕ꺗떙ﾛ솟킡킣", a_));
					spr\u1CFF.ᜀ(A_0, A_1.Border, A_2);
					A_0.WriteEndElement();
					num = 1;
					continue;
				case 3:
					goto IL_45A;
				case 4:
					goto IL_196;
				case 5:
					goto IL_287;
				case 6:
					if (!A_1.InterceptIsAuto)
					{
						num = 28;
						continue;
					}
					goto IL_287;
				case 7:
					if (A_1.DisplayEquation)
					{
						num = 10;
						continue;
					}
					goto IL_4AA;
				case 8:
					if (A_1.Type == TrendLineType.Moving_Average)
					{
						num = 24;
						continue;
					}
					goto IL_196;
				case 9:
					num = 7;
					continue;
				case 10:
					goto IL_2E0;
				case 11:
					if (A_1.Type != TrendLineType.Polynomial)
					{
						num = 8;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_45A;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 12:
					goto IL_196;
				case 13:
					spr\u1CFF.ᜀ(A_0, text, A_1.Order.ToString());
					num = 16;
					continue;
				case 14:
					goto IL_389;
				case 15:
				{
					if (A_1 == null)
					{
						num = 14;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("丹主嬽⸿♁⡃⽅♇⽉", a_), RecordTableEnumerator.b("刹䠻䨽〿硁歃楅㭇⥉⑋⭍㵏㍑❓硕㝗⩙㥛そᡟཡࡣeݧᡩū཭ѯű婳᥵੷ᵹ卻᩽뾏ꂑ꒓ꚕ꺗떙ﾛ솟킡킣", a_));
					string name = A_1.Name;
					num = 27;
					continue;
				}
				case 16:
					goto IL_3D4;
				case 17:
					if (true)
					{
					}
					num = 22;
					continue;
				case 18:
					goto IL_2F9;
				case 19:
					if (border != null)
					{
						num = 26;
						continue;
					}
					goto IL_1EB;
				case 20:
				{
					string name;
					A_0.WriteElementString(RecordTableEnumerator.b("吹崻匽┿", a_), RecordTableEnumerator.b("刹䠻䨽〿硁歃楅㭇⥉⑋⭍㵏㍑❓硕㝗⩙㥛そᡟཡࡣeݧᡩū཭ѯű婳᥵੷ᵹ卻᩽뾏ꂑ꒓ꚕ꺗떙ﾛ솟킡킣", a_), name);
					num = 3;
					continue;
				}
				case 21:
					if (!A_1.DisplayRSquared)
					{
						num = 9;
						continue;
					}
					goto IL_2E0;
				case 22:
					if (!A_1.NameIsAuto)
					{
						num = 20;
						continue;
					}
					goto IL_45A;
				case 23:
					goto IL_BA;
				case 24:
					text = RecordTableEnumerator.b("䨹夻䰽⤿ⵁ⁃", a_);
					num = 4;
					continue;
				case 26:
					num = 30;
					continue;
				case 27:
				{
					string name;
					if (name != null)
					{
						num = 17;
						continue;
					}
					goto IL_45A;
				}
				case 28:
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("匹刻䨽┿ぁ❃⍅㡇㹉", a_), XmlConvert.ToString(A_1.Intercept));
					num = 5;
					continue;
				case 29:
					if (text != null)
					{
						num = 13;
						continue;
					}
					goto IL_3D4;
				case 30:
					if (!border.UseDefaultFormat)
					{
						num = 2;
						continue;
					}
					goto IL_1EB;
				}
				if (A_0 == null)
				{
					num = 23;
					continue;
				}
				num = 15;
				continue;
				IL_196:
				num = 29;
				continue;
				IL_1EB:
				XLSXTrendlineType type = (XLSXTrendlineType)A_1.Type;
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("丹主嬽⸿♁⡃⽅♇⽉ᡋ㝍⁏㝑", a_), type.ToString());
				text = null;
				num = 11;
				continue;
				IL_287:
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("帹唻䴽〿၁ᝃ㝅㩇", a_), A_1.DisplayRSquared);
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("帹唻䴽〿݁㕃", a_), A_1.DisplayEquation);
				num = 21;
				continue;
				IL_2E0:
				this.ᜀ(A_0, A_1.DataLabel);
				num = 18;
				continue;
				IL_3D4:
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("尹医䰽㜿⍁㙃≅", a_), XmlConvert.ToString(A_1.Forward));
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("堹崻崽⬿㕁╃㑅ⱇ", a_), XmlConvert.ToString(A_1.Backward));
				num = 6;
				continue;
				IL_45A:
				border = A_1.Border;
				num = 19;
			}
			IL_BA:
			throw new ArgumentNullException(RecordTableEnumerator.b("䴹主圽㐿❁㙃", a_));
			IL_2F9:
			goto IL_4AA;
			IL_389:
			throw new ArgumentNullException(RecordTableEnumerator.b("丹主嬽⸿♁⡃⽅♇⽉", a_));
			IL_4AA:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06005507 RID: 21767 RVA: 0x0035DC3C File Offset: 0x0035CC3C
	private void ᜀ(XmlWriter A_0, IChartTextArea A_1)
	{
		int a_ = 14;
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
					goto IL_8B;
				case 1:
					if (true)
					{
					}
					break;
				case 2:
					if (A_1 == null)
					{
						num = 0;
						continue;
					}
					goto IL_A1;
				case 3:
					goto IL_62;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 3;
			}
			else
			{
				num = 2;
			}
		}
		IL_62:
		throw new ArgumentNullException(RecordTableEnumerator.b("㍃㑅ⅇ㹉⥋㱍", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("⁃❅㱇⭉K⽍㉏㝑㡓ၕ㝗⡙ㅛ㽝ᑟ", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("ぃ㑅ⵇ⑉⡋≍㥏㱑ㅓᩕ㩗㙙", a_), RecordTableEnumerator.b("ⱃ㉅㱇㩉癋慍罏⅑㝓㹕㵗㝙㵛ⵝ也ൡᑣͥ٧ቩūɭᙯᵱٳ᭵᥷๹ཻ偽ꦅ轢憐ﲑﮕ떙꺛꺝邟钡讣얥삧쮩\udeab\udaad", a_));
		A_0.WriteElementString(RecordTableEnumerator.b("⡃❅ㅇ╉㥋㩍", a_), RecordTableEnumerator.b("ⱃ㉅㱇㩉癋慍罏⅑㝓㹕㵗㝙㵛ⵝ也ൡᑣͥ٧ቩūɭᙯᵱٳ᭵᥷๹ཻ偽ꦅ轢憐ﲑﮕ떙꺛꺝邟钡讣얥삧쮩\udeab\udaad", a_), string.Empty);
		A_0.WriteStartElement(RecordTableEnumerator.b("⩃㍅╇౉⅋㩍", a_), RecordTableEnumerator.b("ⱃ㉅㱇㩉癋慍罏⅑㝓㹕㵗㝙㵛ⵝ也ൡᑣͥ٧ቩūɭᙯᵱٳ᭵᥷๹ཻ偽ꦅ轢憐ﲑﮕ떙꺛꺝邟钡讣얥삧쮩\udeab\udaad", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("≃⥅㩇❉ⵋ㩍ፏ㵑こ㍕", a_), RecordTableEnumerator.b("̓⍅♇⽉㹋⽍㱏", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("㝃⥅㵇㡉⽋⭍ᱏ㭑㩓㵕㵗㹙", a_), RecordTableEnumerator.b("瑃", a_));
		A_0.WriteEndElement();
		A_0.WriteEndElement();
	}

	// Token: 0x06005508 RID: 21768 RVA: 0x0035DDA8 File Offset: 0x0035CDA8
	private void ᜀ(XmlWriter A_0, IChartWallOrFloor A_1, string A_2, XlsChart A_3)
	{
		int a_ = 12;
		int num = 10;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_2 != null)
				{
					if (true)
					{
					}
					num = 9;
					continue;
				}
				goto IL_75;
			case 1:
				if (A_1 == null)
				{
					num = 4;
					continue;
				}
				num = 0;
				continue;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A2;
				default:
				{
					if (false)
					{
					}
					XlsChartWallOrFloor xlsChartWallOrFloor;
					if (xlsChartWallOrFloor.Thickness != -1)
					{
						num = 6;
						continue;
					}
					goto IL_1D6;
				}
				}
				break;
			case 3:
				goto IL_1D1;
			case 4:
				goto IL_A2;
			case 5:
				spr\u1CFF.ᜀ(A_0, A_1, A_3, false);
				num = 7;
				continue;
			case 6:
			{
				XlsChartWallOrFloor xlsChartWallOrFloor;
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("㙁ⱃ⽅⭇ⅉ≋⭍⍏⅑", a_), xlsChartWallOrFloor.Thickness.ToString());
				num = 8;
				continue;
			}
			case 7:
				goto IL_A7;
			case 8:
				goto IL_174;
			case 9:
				num = 11;
				continue;
			case 11:
			{
				if (A_2.Length == 0)
				{
					num = 3;
					continue;
				}
				XlsChartWallOrFloor xlsChartWallOrFloor = (XlsChartWallOrFloor)A_1;
				A_0.WriteStartElement(A_2, RecordTableEnumerator.b("⩁ぃ㉅㡇灉捋慍⍏ㅑ㱓㍕㕗㭙⽛灝ཟቡţࡥၧݩk࡭Ὧqᥳ᝵౷ॹ剻ᅽꮃ慎ﮋﺏ煉歹랗ꢙ겛꺝隟趡잣캥즧\ud8a9\ud8ab", a_));
				num = 13;
				continue;
			}
			case 12:
				goto IL_5C;
			case 13:
			{
				XlsChartWallOrFloor xlsChartWallOrFloor;
				if (xlsChartWallOrFloor.HasShapeProperties)
				{
					num = 5;
					continue;
				}
				goto IL_A7;
			}
			}
			if (A_0 == null)
			{
				num = 12;
				continue;
			}
			num = 1;
			continue;
			IL_A7:
			num = 2;
		}
		IL_5C:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕁㙃⽅㱇⽉㹋", a_));
		IL_75:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⽁╃⽅♇ṉⵋ⥍ṏ㍑㥓㍕", a_));
		IL_A2:
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅁㅃ㑅⹇⭉⽋⭍", a_));
		IL_174:
		goto IL_1D6;
		IL_1D1:
		goto IL_75;
		IL_1D6:
		A_0.WriteEndElement();
	}

	// Token: 0x06005509 RID: 21769 RVA: 0x0035DF94 File Offset: 0x0035CF94
	private void ᜃ(XmlWriter A_0, XlsChart A_1, RelationsCollection A_2)
	{
		int a_ = 16;
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
				switch (num)
				{
				case 1:
					if (num2 == 0)
					{
						num = 19;
						continue;
					}
					goto IL_246;
				case 2:
					goto IL_36A;
				case 3:
				{
					A_0.WriteStartElement(RecordTableEnumerator.b("⑅⥇㡉ཋ♍ㅏ⁑⁓", a_), RecordTableEnumerator.b("⹅㱇㹉㱋瑍罏絑❓㕕し㽙ㅛ㽝፟䱡ୣᙥ൧ѩᑫͭᱯᑱ᭳ѵᕷ᭹ࡻൽ깿ꞇﺋﮑ望뎛겝邟銡銣覥쮧슩춫\udcad쒯", a_));
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("⑅⥇㡉ࡋ❍≏", a_), RecordTableEnumerator.b("╅❇♉", a_));
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("ⅅ㩇╉㥋㹍㥏㱑㍓", a_), RecordTableEnumerator.b("╅⑇㽉㽋㩍㕏⁑ㅓ㉕", a_));
					XlsChartAxis xlsChartAxis = (XlsChartAxis)A_1.PrimaryCategoryAxis;
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("❅ぇ͉⡋", a_), xlsChartAxis.AxisId.ToString());
					A_1.SerializedAxisIds.Add(xlsChartAxis.AxisId);
					xlsChartAxis = (XlsChartAxis)A_1.PrimaryValueAxis;
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("❅ぇ͉⡋", a_), xlsChartAxis.AxisId.ToString());
					A_1.SerializedAxisIds.Add(xlsChartAxis.AxisId);
					A_0.WriteEndElement();
					num = 7;
					continue;
				}
				case 4:
					goto IL_341;
				case 5:
					goto IL_3D2;
				case 6:
					goto IL_3B4;
				case 7:
					goto IL_266;
				case 8:
					if (count == 0)
					{
						num = 13;
						continue;
					}
					goto IL_266;
				case 9:
				{
					IChartFrameFormat plotArea = A_1.PlotArea;
					spr\u1CFF.ᜀ(A_0, plotArea, A_1, plotArea.IsBorderCornersRound);
					num = 2;
					continue;
				}
				case 10:
				{
					Stream stream;
					if (stream == null)
					{
						num = 23;
						continue;
					}
					stream.Position = 0L;
					ShapeParser.WriteNodeFromStream(A_0, stream);
					num = 6;
					continue;
				}
				case 11:
					if (A_1.HasPlotArea)
					{
						num = 9;
						continue;
					}
					goto IL_41E;
				case 12:
					if (!A_1.HasPivotTable)
					{
						num = 3;
						continue;
					}
					goto IL_246;
				case 13:
					num = 15;
					continue;
				case 14:
				{
					if (A_1 == null)
					{
						if (true)
						{
						}
						num = 21;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("㙅⑇╉㡋ཌྷ≏㝑㕓", a_), RecordTableEnumerator.b("⹅㱇㹉㱋瑍罏絑❓㕕し㽙ㅛ㽝፟䱡ୣᙥ൧ѩᑫͭᱯᑱ᭳ѵᕷ᭹ࡻൽ깿ꞇﺋﮑ望뎛겝邟銡銣覥쮧슩춫\udcad쒯", a_));
					Stream stream = A_1.InnerPlotArea.LayoutStream;
					goto IL_1F1;
				}
				case 15:
					if (A_1.HasPivotTable)
					{
						num = 4;
						continue;
					}
					goto IL_266;
				case 16:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1F1;
					default:
						if (false)
						{
						}
						goto IL_3D2;
					}
					break;
				case 17:
					goto IL_98;
				case 18:
					if (num2 == count)
					{
						num = 20;
						continue;
					}
					num2 += this.ᜀ(A_0, A_1, num3);
					num3++;
					num = 16;
					continue;
				case 19:
					num = 12;
					continue;
				case 20:
					num = 1;
					continue;
				case 21:
					goto IL_241;
				case 22:
					goto IL_3B4;
				case 23:
					A_0.WriteElementString(RecordTableEnumerator.b("⩅⥇㍉⍋㭍⑏", a_), RecordTableEnumerator.b("⹅㱇㹉㱋瑍罏絑❓㕕し㽙ㅛ㽝፟䱡ୣᙥ൧ѩᑫͭᱯᑱ᭳ѵᕷ᭹ࡻൽ깿ꞇﺋﮑ望뎛겝邟銡銣覥쮧슩춫\udcad쒯", a_), string.Empty);
					num = 22;
					continue;
				}
				if (A_0 == null)
				{
					num = 17;
					continue;
				}
				num = 14;
				continue;
				IL_1F1:
				num = 10;
				continue;
				IL_246:
				num = 8;
				continue;
				IL_266:
				this.ᜁ(A_0, A_1, A_2);
				this.ᜀ(A_0, A_1);
				num = 11;
				continue;
				IL_3B4:
				count = A_1.Series.Count;
				num2 = 0;
				num3 = 0;
				num = 5;
				continue;
				IL_3D2:
				num = 18;
			}
			IL_98:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅅ㩇⍉㡋⭍≏", a_));
			IL_241:
			throw new ArgumentNullException(RecordTableEnumerator.b("╅⁇⭉㹋㩍", a_));
			IL_341:
			this.ᜂ(A_0, A_1, A_2);
			return;
			IL_36A:
			IL_41E:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x0600550A RID: 21770 RVA: 0x0035E3C8 File Offset: 0x0035D3C8
	private void ᜂ(XmlWriter A_0, XlsChart A_1, RelationsCollection A_2)
	{
		int a_ = 6;
		switch (0)
		{
		default:
			for (;;)
			{
				this.ᜆ(A_0, A_1);
				string text = A_1.PivotChartType.ToString();
				bool flag = text.Contains(RecordTableEnumerator.b("氻圽┿", a_));
				bool flag2 = text.Contains(RecordTableEnumerator.b("砻儽㔿╁ⱃ⡅㵇㹉", a_));
				int num = 10;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_1BC;
					case 1:
						if (!flag2)
						{
							num = 8;
							continue;
						}
						goto IL_1E1;
					case 2:
						num = 12;
						continue;
					case 3:
					{
						IChartFrameFormat plotArea = A_1.PlotArea;
						spr\u1CFF.ᜀ(A_0, plotArea, A_1, plotArea.IsBorderCornersRound);
						num = 0;
						continue;
					}
					case 4:
						if (flag)
						{
							goto IL_1E1;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_216;
						default:
							if (false)
							{
							}
							num = 6;
							continue;
						}
						break;
					case 5:
					{
						XlsChartAxis xlsChartAxis = (XlsChartAxis)A_1.PrimarySerieAxis;
						spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("崻䘽ि♁", a_), xlsChartAxis.AxisId.ToString());
						num = 14;
						continue;
					}
					case 6:
						num = 1;
						continue;
					case 7:
						goto IL_1E1;
					case 8:
						this.ᜀ(A_0, A_1, A_2);
						this.ᜀ(A_0, A_1);
						num = 7;
						continue;
					case 9:
						if (A_1.HasPlotArea)
						{
							num = 3;
							continue;
						}
						goto IL_28F;
					case 10:
						if (!flag)
						{
							num = 2;
							continue;
						}
						goto IL_210;
					case 11:
					{
						XlsChartAxis xlsChartAxis = (XlsChartAxis)A_1.PrimaryCategoryAxis;
						spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("崻䘽ि♁", a_), xlsChartAxis.AxisId.ToString());
						xlsChartAxis = (XlsChartAxis)A_1.PrimaryValueAxis;
						spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("崻䘽ि♁", a_), xlsChartAxis.AxisId.ToString());
						num = 13;
						continue;
					}
					case 12:
						if (!flag2)
						{
							num = 11;
							continue;
						}
						goto IL_210;
					case 13:
						if (text.Contains(RecordTableEnumerator.b("漻䬽㈿⑁╃╅ⵇ", a_)))
						{
							num = 5;
							continue;
						}
						goto IL_210;
					case 14:
						goto IL_210;
					}
					break;
					IL_1E1:
					num = 9;
					continue;
					IL_216:
					num = 4;
					continue;
					IL_210:
					A_0.WriteEndElement();
					goto IL_216;
				}
			}
			IL_1BC:
			IL_28F:
			if (true)
			{
			}
			A_0.WriteEndElement();
			return;
		}
	}

	// Token: 0x0600550B RID: 21771 RVA: 0x0035E674 File Offset: 0x0035D674
	private void ᜑ(XmlWriter A_0, XlsChart A_1)
	{
		int a_ = 14;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_7C;
			case 1:
				goto IL_C2;
			case 2:
				goto IL_EE;
			case 4:
			{
				string text;
				if (!text.Contains(RecordTableEnumerator.b("ك❅㩇", a_)))
				{
					num = 5;
					continue;
				}
				num = 1;
				continue;
			}
			case 5:
				num = 0;
				continue;
			case 6:
			{
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("♃❅㩇ॉ⑋⽍≏♑", a_), RecordTableEnumerator.b("ⱃ㉅㱇㩉癋慍罏⅑㝓㹕㵗㝙㵛ⵝ也ൡᑣͥ٧ቩūɭᙯᵱٳ᭵᥷๹ཻ偽ꦅ轢憐ﲑﮕ떙꺛꺝邟钡讣얥삧쮩\udeab\udaad", a_));
				string text = A_1.PivotChartType.ToString();
				num = 4;
				continue;
			}
			case 7:
				goto IL_72;
			}
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_C2;
			default:
				if (false)
				{
				}
				if (A_0 == null)
				{
					num = 7;
				}
				else
				{
					num = 6;
				}
				break;
			}
		}
		IL_72:
		throw new ArgumentNullException(RecordTableEnumerator.b("㍃㑅ⅇ㹉⥋㱍", a_));
		IL_7C:
		string text2 = RecordTableEnumerator.b("❃⥅⑇", a_);
		goto IL_157;
		IL_C2:
		text2 = RecordTableEnumerator.b("♃❅㩇", a_);
		goto IL_157;
		IL_EE:
		throw new ArgumentNullException(RecordTableEnumerator.b("❃⹅⥇㡉㡋", a_));
		IL_157:
		string a_2 = text2;
		spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("♃❅㩇้╋㱍", a_), a_2);
		this.ᜎ(A_0, A_1);
	}

	// Token: 0x0600550C RID: 21772 RVA: 0x0035E7F8 File Offset: 0x0035D7F8
	private int \u1717(XmlWriter A_0, XlsChart A_1, XlsChartSerie A_2)
	{
		int a_ = 15;
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
			default:
			{
				int num = 1;
				int result;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						if (A_1 == null)
						{
							if (true)
							{
							}
							num = 2;
							continue;
						}
						A_0.WriteStartElement(RecordTableEnumerator.b("❄♆㭈ࡊ╌⹎⍐❒", a_), RecordTableEnumerator.b("ⵄ㍆㵈㭊睌恎繐⁒㙔㽖㱘㙚㱜ⱞ你ౢᕤɦݨ፪lͮᝰᱲݴ᩶ᡸེ๼兾ꢆ力ﶒ殺뒚꾜꾞醠関誤쒦솨쪪\udfac\udbae", a_));
						result = this.\u1713(A_0, A_1, A_2);
						IChartFormat options = A_2.Format.Options;
						int gapWidth = options.GapWidth;
						spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("≄♆㥈᱊⑌⭎═㭒", a_), gapWidth.ToString());
						int num2 = options.Overlap;
						num = 6;
						continue;
					}
					case 2:
						goto IL_141;
					case 3:
					{
						int num2 = 100;
						num = 7;
						continue;
					}
					case 4:
						goto IL_174;
					case 5:
						goto IL_7C;
					case 6:
					{
						int num2;
						if (num2 == -65436)
						{
							num = 3;
							continue;
						}
						goto IL_18A;
					}
					case 7:
						goto IL_18A;
					case 8:
					{
						int num2;
						spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("⩄ㅆⱈ㥊⅌⹎⅐", a_), num2.ToString());
						num = 4;
						continue;
					}
					case 9:
					{
						int num2;
						if (num2 != 0)
						{
							num = 8;
							continue;
						}
						goto IL_1BB;
					}
					}
					if (A_0 == null)
					{
						num = 5;
						continue;
					}
					num = 0;
					continue;
					IL_18A:
					num = 9;
				}
				IL_7C:
				break;
				IL_141:
				throw new ArgumentNullException(RecordTableEnumerator.b("♄⽆⡈㥊㥌", a_));
				IL_174:
				IL_1BB:
				this.\u1716(A_0, A_1, A_2);
				A_0.WriteEndElement();
				return result;
			}
			}
			break;
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㉄㕆⁈㽊⡌㵎", a_));
	}

	// Token: 0x0600550D RID: 21773 RVA: 0x0035E9D0 File Offset: 0x0035D9D0
	private void \u1716(XmlWriter A_0, XlsChart A_1, XlsChartSerie A_2)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			int num = 7;
			for (;;)
			{
				IChartValueAxis chartValueAxis;
				IChartCategoryAxis chartCategoryAxis;
				XlsChartAxis xlsChartAxis;
				switch (num)
				{
				case 0:
					if (A_1 == null)
					{
						num = 10;
						continue;
					}
					num = 15;
					continue;
				case 1:
					num = 6;
					continue;
				case 2:
					chartValueAxis = A_1.PrimaryValueAxis;
					goto IL_DB;
				case 3:
					goto IL_2BE;
				case 4:
					if (A_1.IsSeriesAxisAvail)
					{
						num = 16;
						continue;
					}
					return;
				case 5:
					num = 13;
					continue;
				case 6:
					chartValueAxis = A_1.SecondaryValueAxis;
					goto IL_DB;
				case 8:
					goto IL_84;
				case 9:
				{
					bool usePrimaryAxis;
					if (!usePrimaryAxis)
					{
						num = 5;
						continue;
					}
					goto IL_26A;
				}
				case 10:
					goto IL_170;
				case 11:
				{
					bool usePrimaryAxis;
					if (!usePrimaryAxis)
					{
						num = 1;
						continue;
					}
					num = 2;
					continue;
				}
				case 12:
					goto IL_1C4;
				case 13:
					chartCategoryAxis = A_1.SecondaryCategoryAxis;
					goto IL_29A;
				case 14:
					if (true)
					{
					}
					chartCategoryAxis = A_1.PrimaryCategoryAxis;
					goto IL_29A;
				case 15:
				{
					if (A_2 == null)
					{
						num = 18;
						continue;
					}
					bool usePrimaryAxis = A_2.UsePrimaryAxis;
					num = 9;
					continue;
				}
				case 16:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_26A;
					default:
						if (false)
						{
						}
						xlsChartAxis = (XlsChartAxis)A_1.PrimarySerieAxis;
						spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("堸䌺琼嬾", a_), xlsChartAxis.AxisId.ToString());
						num = 12;
						continue;
					}
					break;
				case 17:
					if (xlsChartAxis == null)
					{
						num = 3;
						continue;
					}
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("堸䌺琼嬾", a_), xlsChartAxis.AxisId.ToString());
					A_1.SerializedAxisIds.Add(xlsChartAxis.AxisId);
					num = 11;
					continue;
				case 18:
					goto IL_1E7;
				}
				if (A_0 == null)
				{
					num = 8;
					continue;
				}
				num = 0;
				continue;
				IL_DB:
				xlsChartAxis = (XlsChartAxis)chartValueAxis;
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("堸䌺琼嬾", a_), xlsChartAxis.AxisId.ToString());
				A_1.SerializedAxisIds.Add(xlsChartAxis.AxisId);
				num = 4;
				continue;
				IL_26A:
				num = 14;
				continue;
				IL_29A:
				xlsChartAxis = (XlsChartAxis)chartCategoryAxis;
				num = 17;
			}
			IL_84:
			throw new ArgumentNullException(RecordTableEnumerator.b("丸䤺吼䬾⑀ㅂ", a_));
			IL_170:
			throw new ArgumentNullException(RecordTableEnumerator.b("娸区尼䴾㕀", a_));
			IL_1C4:
			return;
			IL_1E7:
			throw new ArgumentNullException(RecordTableEnumerator.b("弸刺似䰾㕀၂⁄㕆⁈⹊㹌", a_));
			IL_2BE:
			throw new ArgumentNullException(RecordTableEnumerator.b("堸䌺吼䰾", a_));
		}
		}
	}

	// Token: 0x0600550E RID: 21774 RVA: 0x0035ECC0 File Offset: 0x0035DCC0
	private void ᜐ(XmlWriter A_0, XlsChart A_1)
	{
		int a_ = 14;
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				ExcelChartType pivotChartType;
				BaseFormatType a_2;
				TopFormatType a_3;
				string text2;
				switch (num)
				{
				case 0:
					goto IL_17D;
				case 1:
					switch (pivotChartType)
					{
					case ExcelChartType.CylinderClustered:
					case ExcelChartType.CylinderStacked:
					case ExcelChartType.Cylinder100PercentStacked:
					case ExcelChartType.CylinderBarClustered:
					case ExcelChartType.CylinderBarStacked:
					case ExcelChartType.CylinderBar100PercentStacked:
					case ExcelChartType.Cylinder3DClustered:
						a_2 = BaseFormatType.Circle;
						a_3 = TopFormatType.Straight;
						num = 12;
						continue;
					case ExcelChartType.ConeClustered:
					case ExcelChartType.ConeStacked:
					case ExcelChartType.Cone100PercentStacked:
					case ExcelChartType.ConeBarClustered:
					case ExcelChartType.ConeBarStacked:
					case ExcelChartType.ConeBar100PercentStacked:
					case ExcelChartType.Cone3DClustered:
						a_2 = BaseFormatType.Circle;
						a_3 = TopFormatType.Sharp;
						num = 13;
						continue;
					case ExcelChartType.PyramidClustered:
					case ExcelChartType.PyramidStacked:
					case ExcelChartType.Pyramid100PercentStacked:
					case ExcelChartType.PyramidBarClustered:
					case ExcelChartType.PyramidBarStacked:
					case ExcelChartType.PyramidBar100PercentStacked:
					case ExcelChartType.Pyramid3DClustered:
						a_2 = BaseFormatType.Rectangle;
						a_3 = TopFormatType.Sharp;
						num = 10;
						continue;
					default:
						num = 5;
						continue;
					}
					break;
				case 2:
				{
					IL_16B:
					if (A_1 == null)
					{
						num = 0;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("♃❅㩇祉ࡋ്㡏㍑♓≕", a_), RecordTableEnumerator.b("ⱃ㉅㱇㩉癋慍罏⅑㝓㹕㵗㝙㵛ⵝ也ൡᑣͥ٧ቩūɭᙯᵱٳ᭵᥷๹ཻ偽ꦅ轢憐ﲑﮕ떙꺛꺝邟钡讣얥삧쮩\udeab\udaad", a_));
					string text = A_1.PivotChartType.ToString();
					num = 7;
					continue;
				}
				case 3:
					goto IL_94;
				case 5:
					num = 14;
					continue;
				case 6:
					text2 = RecordTableEnumerator.b("♃❅㩇", a_);
					goto IL_292;
				case 7:
				{
					string text;
					if (!text.Contains(RecordTableEnumerator.b("ك❅㩇", a_)))
					{
						num = 17;
						continue;
					}
					num = 6;
					continue;
				}
				case 8:
					goto IL_2E3;
				case 9:
				{
					string text;
					if (text.Contains(RecordTableEnumerator.b("ᑃ㽅㩇⭉⅋❍㑏", a_)))
					{
						num = 8;
						continue;
					}
					return;
				}
				case 10:
					goto IL_193;
				case 11:
				{
					string text;
					if (!text.Contains(RecordTableEnumerator.b("݃⥅♇⽉", a_)))
					{
						num = 18;
						continue;
					}
					goto IL_2E3;
				}
				case 12:
					goto IL_193;
				case 13:
					goto IL_193;
				case 14:
					goto IL_18E;
				case 15:
					text2 = RecordTableEnumerator.b("❃⥅⑇", a_);
					goto IL_292;
				case 16:
					return;
				case 17:
					num = 15;
					continue;
				case 18:
					num = 20;
					continue;
				case 19:
					num = 9;
					continue;
				case 20:
				{
					string text;
					if (!text.Contains(RecordTableEnumerator.b("݃㽅⑇⍉≋⩍㕏⁑", a_)))
					{
						num = 19;
						continue;
					}
					goto IL_2E3;
				}
				}
				if (true)
				{
				}
				if (A_0 == null)
				{
					num = 3;
					continue;
				}
				num = 2;
				continue;
				IL_193:
				this.ᜀ(A_0, a_2, a_3);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_16B;
				default:
					if (false)
					{
					}
					num = 16;
					continue;
				}
				IL_292:
				string a_4 = text2;
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("♃❅㩇้╋㱍", a_), a_4);
				this.ᜎ(A_0, A_1);
				num = 11;
				continue;
				IL_2E3:
				pivotChartType = A_1.PivotChartType;
				num = 1;
			}
			IL_94:
			throw new ArgumentNullException(RecordTableEnumerator.b("㍃㑅ⅇ㹉⥋㱍", a_));
			IL_17D:
			throw new ArgumentNullException(RecordTableEnumerator.b("❃⹅⥇㡉㡋", a_));
			IL_18E:
			throw new ArgumentException(RecordTableEnumerator.b("ぃ㽅㡇⽉", a_));
		}
		}
	}

	// Token: 0x0600550F RID: 21775 RVA: 0x0035F034 File Offset: 0x0035E034
	private int \u1715(XmlWriter A_0, XlsChart A_1, XlsChartSerie A_2)
	{
		int a_ = 4;
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
					goto IL_5A;
				case 2:
					goto IL_8B;
				case 3:
					if (A_1 == null)
					{
						if (true)
						{
						}
						num = 2;
						continue;
					}
					goto IL_A1;
				}
				break;
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
		IL_5A:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴹主圽㐿❁㙃", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("夹吻弽㈿㙁", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("堹崻䰽猿ف݃⹅⥇㡉㡋", a_), RecordTableEnumerator.b("刹䠻䨽〿硁歃楅㭇⥉⑋⭍㵏㍑❓硕㝗⩙㥛そᡟཡࡣeݧᡩū཭ѯű婳᥵੷ᵹ卻᩽뾏ꂑ꒓ꚕ꺗떙ﾛ솟킡킣", a_));
		int result = this.\u1713(A_0, A_1, A_2);
		IChartFormat options = A_2.Format.Options;
		int gapWidth = options.GapWidth;
		spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("崹崻丽᜿⭁⁃㉅⁇", a_), gapWidth.ToString());
		this.ᜏ(A_0, A_1);
		this.\u1714(A_0, A_1, A_2);
		this.\u1716(A_0, A_1, A_2);
		A_0.WriteEndElement();
		return result;
	}

	// Token: 0x06005510 RID: 21776 RVA: 0x0035F160 File Offset: 0x0035E160
	private void ᜏ(XmlWriter A_0, XlsChart A_1)
	{
		int a_ = 5;
		int num = 0;
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
				case 1:
					if (A_1 == null)
					{
						num = 3;
						continue;
					}
					goto IL_99;
				case 2:
					goto IL_5A;
				case 3:
					goto IL_83;
				}
				break;
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
		IL_5A:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰺似嘾㕀♂㝄", a_));
		IL_83:
		throw new ArgumentNullException(RecordTableEnumerator.b("堺唼帾㍀㝂", a_));
		IL_99:
		if (true)
		{
		}
		spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("尺尼伾Հ♂㕄㍆ⅈ", a_), A_1.GapDepth.ToString());
	}

	// Token: 0x06005511 RID: 21777 RVA: 0x0035F230 File Offset: 0x0035E230
	private void ᜀ(XmlWriter A_0, BaseFormatType A_1, TopFormatType A_2)
	{
		int a_ = 5;
		int num = 17;
		string a_2;
		for (;;)
		{
			string text;
			string text2;
			string text3;
			switch (num)
			{
			case 0:
				num = 1;
				continue;
			case 1:
				text = RecordTableEnumerator.b("䬺䐼䴾⁀⹂ⱄ⍆ᵈ⑊L⹎⥐", a_);
				goto IL_1D0;
			case 2:
				switch (A_2)
				{
				case TopFormatType.Straight:
					num = 11;
					continue;
				case TopFormatType.Sharp:
					num = 19;
					continue;
				case TopFormatType.Trunc:
					num = 15;
					continue;
				default:
					num = 16;
					continue;
				}
				break;
			case 3:
				goto IL_1DC;
			case 4:
				text2 = RecordTableEnumerator.b("堺䐼匾⡀ⵂ⅄≆㭈", a_);
				goto IL_1F9;
			case 5:
				text = RecordTableEnumerator.b("堺刼儾⑀ᝂ⩄੆⡈㍊", a_);
				goto IL_1D0;
			case 6:
				goto IL_77;
			case 7:
				IL_18A:
				text3 = RecordTableEnumerator.b("䬺䐼䴾⁀⹂ⱄ⍆", a_);
				goto IL_ED;
			case 8:
				num = 7;
				continue;
			case 9:
				goto IL_229;
			case 10:
				text2 = RecordTableEnumerator.b("夺刼䜾", a_);
				goto IL_1F9;
			case 11:
				if (A_1 != BaseFormatType.Circle)
				{
					num = 13;
					continue;
				}
				num = 4;
				continue;
			case 12:
				goto IL_127;
			case 13:
				num = 10;
				continue;
			case 14:
				text3 = RecordTableEnumerator.b("堺刼儾⑀", a_);
				goto IL_ED;
			case 15:
				if (A_1 != BaseFormatType.Circle)
				{
					num = 0;
					continue;
				}
				num = 5;
				continue;
			case 16:
				num = 12;
				continue;
			case 18:
				goto IL_F9;
			case 19:
				if (A_1 != BaseFormatType.Circle)
				{
					num = 8;
					continue;
				}
				num = 14;
				continue;
			}
			if (A_0 == null)
			{
				num = 6;
				continue;
			}
			a_2 = null;
			num = 2;
			continue;
			IL_ED:
			a_2 = text3;
			num = 18;
			continue;
			IL_1F9:
			a_2 = text2;
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_18A;
			default:
				if (false)
				{
				}
				num = 9;
				continue;
			}
			IL_1D0:
			a_2 = text;
			num = 3;
		}
		IL_77:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰺似嘾㕀♂㝄", a_));
		IL_F9:
		IL_127:
		IL_1DC:
		IL_229:
		spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("䠺唼帾ㅀ♂", a_), a_2);
	}

	// Token: 0x06005512 RID: 21778 RVA: 0x0035F480 File Offset: 0x0035E480
	private void \u1714(XmlWriter A_0, XlsChart A_1, XlsChartSerie A_2)
	{
		int a_ = 8;
		switch (0)
		{
		default:
		{
			int num = 10;
			string a_2;
			for (;;)
			{
				string text;
				string text2;
				string text3;
				switch (num)
				{
				case 0:
					goto IL_213;
				case 1:
					text = RecordTableEnumerator.b("丽㤿ぁ╃⭅ⅇ⹉", a_);
					goto IL_287;
				case 2:
				{
					if (true)
					{
					}
					TopFormatType topFormatType;
					switch (topFormatType)
					{
					case TopFormatType.Straight:
						num = 13;
						continue;
					case TopFormatType.Sharp:
						num = 9;
						continue;
					case TopFormatType.Trunc:
						num = 18;
						continue;
					default:
						num = 17;
						continue;
					}
					break;
				}
				case 3:
					goto IL_90;
				case 4:
					text2 = RecordTableEnumerator.b("崽㤿⹁ⵃ⡅ⱇ⽉㹋", a_);
					goto IL_206;
				case 5:
					goto IL_1E0;
				case 6:
					text2 = RecordTableEnumerator.b("尽⼿㩁", a_);
					goto IL_206;
				case 7:
					num = 6;
					continue;
				case 8:
					goto IL_178;
				case 9:
				{
					BaseFormatType barType;
					if (barType != BaseFormatType.Circle)
					{
						num = 11;
						continue;
					}
					num = 21;
					continue;
				}
				case 11:
					num = 1;
					continue;
				case 12:
					goto IL_166;
				case 13:
				{
					BaseFormatType barType;
					if (barType != BaseFormatType.Circle)
					{
						num = 7;
						continue;
					}
					num = 4;
					continue;
				}
				case 14:
					goto IL_294;
				case 15:
					text3 = RecordTableEnumerator.b("崽⼿ⱁ⅃ቅ❇݉ⵋ㙍", a_);
					goto IL_16B;
				case 16:
				{
					if (A_1 == null)
					{
						num = 12;
						continue;
					}
					XlsChartSerieDataFormat xlsChartSerieDataFormat = A_2.Format as XlsChartSerieDataFormat;
					TopFormatType barTopType = xlsChartSerieDataFormat.BarTopType;
					BaseFormatType barType = xlsChartSerieDataFormat.BarType;
					a_2 = null;
					TopFormatType topFormatType = barTopType;
					num = 2;
					continue;
				}
				case 17:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2B5;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 18:
				{
					BaseFormatType barType;
					if (barType != BaseFormatType.Circle)
					{
						num = 20;
						continue;
					}
					num = 15;
					continue;
				}
				case 19:
					text3 = RecordTableEnumerator.b("丽㤿ぁ╃⭅ⅇ⹉ᡋ⅍ᵏ㍑ⱓ", a_);
					goto IL_16B;
				case 20:
					num = 19;
					continue;
				case 21:
					text = RecordTableEnumerator.b("崽⼿ⱁ⅃", a_);
					goto IL_287;
				}
				if (A_0 == null)
				{
					num = 3;
					continue;
				}
				num = 16;
				continue;
				IL_16B:
				a_2 = text3;
				num = 8;
				continue;
				IL_206:
				a_2 = text2;
				num = 0;
				continue;
				IL_287:
				a_2 = text;
				num = 14;
			}
			IL_90:
			throw new ArgumentNullException(RecordTableEnumerator.b("䤽㈿⭁ぃ⍅㩇", a_));
			IL_166:
			throw new ArgumentNullException(RecordTableEnumerator.b("崽⠿⍁㙃㉅", a_));
			IL_178:
			IL_1E0:
			IL_213:
			IL_294:
			IL_2B5:
			spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("䴽⠿⍁㑃⍅", a_), a_2);
			return;
		}
		}
	}

	// Token: 0x06005513 RID: 21779 RVA: 0x0035F758 File Offset: 0x0035E758
	private int \u1713(XmlWriter A_0, XlsChart A_1, XlsChartSerie A_2)
	{
		int a_ = 2;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 5;
				continue;
			case 1:
			{
				string text;
				if (!text.Contains(RecordTableEnumerator.b("稷嬹主", a_)))
				{
					num = 0;
					continue;
				}
				num = 7;
				continue;
			}
			case 3:
				goto IL_44;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A4;
				default:
				{
					if (false)
					{
					}
					if (A_1 == null)
					{
						num = 6;
						continue;
					}
					string text = A_2.SerieType.ToString();
					num = 1;
					continue;
				}
				}
				break;
			case 5:
				goto IL_4E;
			case 6:
				goto IL_D9;
			case 7:
				goto IL_91;
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			IL_A4:
			num = 4;
		}
		IL_44:
		throw new ArgumentNullException(RecordTableEnumerator.b("伷䠹唻䨽┿ぁ", a_));
		IL_4E:
		string text2 = RecordTableEnumerator.b("嬷唹倻", a_);
		goto IL_135;
		IL_91:
		text2 = RecordTableEnumerator.b("娷嬹主", a_);
		goto IL_135;
		IL_D9:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("嬷刹崻䰽㐿", a_));
		IL_135:
		string a_2 = text2;
		spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("娷嬹主稽⤿ぁ", a_), a_2);
		this.ᜎ(A_0, A_2);
		this.ᜌ(A_0, A_2);
		return this.ᜀ(A_0, A_1, A_2, new spr\u2541.ᜀ(this.ᜋ));
	}

	// Token: 0x06005514 RID: 21780 RVA: 0x0035F8D8 File Offset: 0x0035E8D8
	private int ᜀ(XmlWriter A_0, XlsChart A_1, XlsChartSerie A_2, spr\u2541.ᜀ A_3)
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			int num = 5;
			for (;;)
			{
				IList<IChartSerie> list;
				int num2;
				int num3;
				int count;
				IList<IChartSerie> list3;
				IChartSeries a_2;
				switch (num)
				{
				case 0:
					if (A_1 == null)
					{
						num = 6;
						continue;
					}
					goto IL_89;
				case 1:
					goto IL_9E;
				case 2:
					if (A_2 == null)
					{
						num = 1;
						continue;
					}
					num = 15;
					continue;
				case 3:
					list = A_1.Series;
					goto IL_224;
				case 4:
				{
					XlsChartSerie xlsChartSerie;
					A_3(A_0, xlsChartSerie);
					num2++;
					num = 18;
					continue;
				}
				case 6:
					goto IL_173;
				case 7:
					goto IL_84;
				case 8:
				{
					IList<IChartSerie> list2;
					if (list2.Count != A_1.Series.Count)
					{
						num = 9;
						continue;
					}
					num = 10;
					continue;
				}
				case 9:
					num = 3;
					continue;
				case 10:
				{
					IList<IChartSerie> list2;
					list = list2;
					goto IL_224;
				}
				case 11:
					goto IL_283;
				case 12:
				{
					if (num3 >= count)
					{
						num = 16;
						continue;
					}
					XlsChartSerie xlsChartSerie = (XlsChartSerie)list3[num3];
					num = 17;
					continue;
				}
				case 13:
					goto IL_10F;
				case 14:
					goto IL_283;
				case 15:
				{
					if (A_3 == null)
					{
						num = 13;
						continue;
					}
					int chartGroup = A_2.ChartGroup;
					IList<IChartSerie> list2 = A_1.Series.AdditionOrder;
					a_2 = A_1.Series;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_89;
					default:
						if (false)
						{
						}
						num = 8;
						continue;
					}
					break;
				}
				case 16:
					return num2;
				case 17:
				{
					XlsChartSerie xlsChartSerie;
					int chartGroup;
					if (xlsChartSerie.ChartGroup == chartGroup)
					{
						num = 4;
						continue;
					}
					goto IL_A3;
				}
				case 18:
					goto IL_A3;
				}
				if (A_0 == null)
				{
					num = 7;
					continue;
				}
				num = 0;
				continue;
				IL_89:
				num = 2;
				continue;
				IL_A3:
				num3++;
				num = 11;
				continue;
				IL_224:
				list3 = list;
				int num4 = this.ᜀ(A_2, list3, a_2);
				A_2 = (list3[num4] as XlsChartSerie);
				A_3(A_0, A_2);
				num2 = 1;
				num3 = num4 + 1;
				count = list3.Count;
				if (true)
				{
				}
				num = 14;
				continue;
				IL_283:
				num = 12;
			}
			IL_84:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰺似嘾㕀♂㝄", a_));
			IL_9E:
			throw new ArgumentNullException(RecordTableEnumerator.b("崺吼䴾㉀㝂ᙄ≆㭈≊⡌㱎", a_));
			IL_10F:
			throw new ArgumentNullException(RecordTableEnumerator.b("䠺堼䴾⡀≂⥄⹆㍈⩊㥌⁎⍐", a_));
			IL_173:
			throw new ArgumentNullException(RecordTableEnumerator.b("堺唼帾㍀㝂", a_));
		}
		}
	}

	// Token: 0x06005515 RID: 21781 RVA: 0x0035FB90 File Offset: 0x0035EB90
	private int ᜀ(XlsChartSerie A_0, IList<IChartSerie> A_1, IChartSeries A_2)
	{
		int result;
		for (;;)
		{
			result = -1;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					if (true)
					{
					}
					if (A_2 == A_1)
					{
						num = 7;
						continue;
					}
					int num2 = 0;
					int count = A_1.Count;
					num = 5;
					continue;
				}
				case 1:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 2;
						continue;
					}
					num = 4;
					continue;
				}
				case 2:
					return result;
				case 3:
				{
					int num2;
					result = num2;
					num = 6;
					continue;
				}
				case 4:
				{
					int num2;
					if ((A_1[num2] as XlsChartSerie).ChartGroup == A_0.ChartGroup)
					{
						num = 3;
						continue;
					}
					num2++;
					num = 8;
					continue;
				}
				case 5:
					goto IL_BF;
				case 6:
					return result;
				case 7:
					result = A_0.Index;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return result;
					default:
						if (false)
						{
						}
						num = 9;
						continue;
					}
					break;
				case 8:
					goto IL_BF;
				case 9:
					return result;
				}
				break;
				IL_BF:
				num = 1;
			}
		}
		return result;
	}

	// Token: 0x06005516 RID: 21782 RVA: 0x0035FCA8 File Offset: 0x0035ECA8
	private void ᜎ(XmlWriter A_0, XlsChartSerie A_1)
	{
		int a_ = 15;
		int num = 6;
		string a_2;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				goto IL_100;
			case 1:
				a_2 = RecordTableEnumerator.b("㙄㍆⡈⡊♌⩎㕐", a_);
				num = 0;
				continue;
			case 2:
				goto IL_64;
			case 3:
				goto IL_1B6;
			case 4:
			{
				ExcelChartType serieType;
				if (!XlsChart.ᜃ(serieType))
				{
					a_2 = RecordTableEnumerator.b("㙄㍆⡈╊⥌⹎⍐㝒", a_);
					num = 12;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_19C;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				break;
			}
			case 5:
			{
				if (A_1 == null)
				{
					num = 7;
					continue;
				}
				ExcelChartType serieType = A_1.SerieType;
				num = 10;
				continue;
			}
			case 7:
				goto IL_E1;
			case 8:
				goto IL_19C;
			case 9:
				goto IL_133;
			case 10:
			{
				ExcelChartType serieType;
				if (XlsChart.ᜅ(serieType))
				{
					num = 8;
					continue;
				}
				num = 13;
				continue;
			}
			case 11:
				a_2 = RecordTableEnumerator.b("㕄≆㭈⡊⡌ⅎ═R⅔㙖㩘ず㡜㭞", a_);
				num = 9;
				continue;
			case 12:
				goto IL_152;
			case 13:
			{
				ExcelChartType serieType;
				if (XlsChart.ᜄ(serieType))
				{
					num = 11;
					continue;
				}
				num = 4;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			num = 5;
			continue;
			IL_19C:
			a_2 = RecordTableEnumerator.b("♄⭆㱈㡊㥌⩎⍐㙒ㅔ", a_);
			num = 3;
		}
		IL_64:
		throw new ArgumentNullException(RecordTableEnumerator.b("㉄㕆⁈㽊⡌㵎", a_));
		IL_E1:
		throw new ArgumentNullException(RecordTableEnumerator.b("⍄⹆㭈㡊㥌ᱎ㑐⅒㱔㉖⩘", a_));
		IL_100:
		IL_133:
		IL_152:
		IL_1B6:
		spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("≄㕆♈㹊㵌♎㽐㑒", a_), a_2);
	}

	// Token: 0x06005517 RID: 21783 RVA: 0x0035FE84 File Offset: 0x0035EE84
	private void ᜎ(XmlWriter A_0, XlsChart A_1)
	{
		int a_ = 1;
		int num = 11;
		string a_2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_19C;
			case 1:
				a_2 = RecordTableEnumerator.b("䜶尸䤺帼娾⽀㝂ᙄ㍆⡈⡊♌⩎㕐", a_);
				if (true)
				{
				}
				num = 9;
				continue;
			case 2:
			{
				ExcelChartType pivotChartType;
				if (XlsChart.ᜄ(pivotChartType))
				{
					num = 1;
					continue;
				}
				num = 3;
				continue;
			}
			case 3:
			{
				ExcelChartType pivotChartType;
				if (!XlsChart.ᜃ(pivotChartType))
				{
					a_2 = RecordTableEnumerator.b("䐶䴸娺匼嬾⁀ㅂ⅄", a_);
					num = 12;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_19C;
				default:
					if (false)
					{
					}
					num = 8;
					continue;
				}
				break;
			}
			case 4:
			{
				ExcelChartType pivotChartType;
				if (XlsChart.ᜅ(pivotChartType))
				{
					num = 0;
					continue;
				}
				num = 2;
				continue;
			}
			case 5:
			{
				if (A_1 == null)
				{
					num = 10;
					continue;
				}
				ExcelChartType pivotChartType = A_1.PivotChartType;
				num = 4;
				continue;
			}
			case 6:
				goto IL_5C;
			case 7:
				goto IL_F8;
			case 8:
				a_2 = RecordTableEnumerator.b("䐶䴸娺帼吾⑀❂", a_);
				num = 7;
				continue;
			case 9:
				goto IL_133;
			case 10:
				goto IL_D9;
			case 12:
				goto IL_152;
			case 13:
				goto IL_1B6;
			}
			if (A_0 == null)
			{
				num = 6;
				continue;
			}
			num = 5;
			continue;
			IL_19C:
			a_2 = RecordTableEnumerator.b("吶唸为丼䬾⑀ㅂ⁄⍆", a_);
			num = 13;
		}
		IL_5C:
		throw new ArgumentNullException(RecordTableEnumerator.b("䀶䬸刺䤼娾㍀", a_));
		IL_D9:
		throw new ArgumentNullException(RecordTableEnumerator.b("儶倸䤺丼䬾ቀ♂㝄⹆ⱈ㡊", a_));
		IL_F8:
		IL_133:
		IL_152:
		IL_1B6:
		spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("倶䬸吺䠼伾⡀ⵂ≄", a_), a_2);
	}

	// Token: 0x06005518 RID: 21784 RVA: 0x00360060 File Offset: 0x0035F060
	private void \u170D(XmlWriter A_0, XlsChart A_1)
	{
		int a_ = 12;
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
					num = 0;
					continue;
				}
				goto IL_A1;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_8D;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					break;
				}
				break;
			case 3:
				goto IL_62;
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
		IL_62:
		goto IL_8D;
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("⅁ⱃ❅㩇㹉", a_));
		IL_8D:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕁㙃⽅㱇⽉㹋", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("⍁㙃⍅⥇祉ࡋ്㡏㍑♓≕", a_), RecordTableEnumerator.b("⩁ぃ㉅㡇灉捋慍⍏ㅑ㱓㍕㕗㭙⽛灝ཟቡţࡥၧݩk࡭Ὧqᥳ᝵౷ॹ剻ᅽꮃ慎ﮋﺏ煉歹랗ꢙ겛꺝隟趡잣캥즧\ud8a9\ud8ab", a_));
		this.ᜎ(A_0, A_1);
	}

	// Token: 0x06005519 RID: 21785 RVA: 0x00360138 File Offset: 0x0035F138
	private int \u1712(XmlWriter A_0, XlsChart A_1, XlsChartSerie A_2)
	{
		int a_ = 18;
		int num = 3;
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
			case 1:
				goto IL_62;
			case 2:
				goto IL_8B;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_8D;
				default:
					if (false)
					{
					}
					break;
				}
				break;
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
				num = 0;
			}
		}
		IL_62:
		goto IL_8D;
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("⭇≉ⵋ㱍⑏", a_));
		IL_8D:
		throw new ArgumentNullException(RecordTableEnumerator.b("㽇㡉╋㩍㕏⁑", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("⥇㡉⥋⽍捏ᙑᝓ㹕㥗⡙⡛", a_), RecordTableEnumerator.b("⁇㹉㡋㹍橏絑筓╕㭗㉙㥛㍝şᅡ䩣॥ᡧཀྵɫ᙭ᵯṱታ᥵੷᝹ᵻ੽겁ꖉﲍﶓﾗ놝銟銡钣邥螧즩쒫쾭슯욱", a_));
		int result = this.ᜐ(A_0, A_1, A_2);
		this.\u1716(A_0, A_1, A_2);
		A_0.WriteEndElement();
		return result;
	}

	// Token: 0x0600551A RID: 21786 RVA: 0x00360224 File Offset: 0x0035F224
	private void ᜌ(XmlWriter A_0, XlsChart A_1)
	{
		int a_ = 9;
		int num = 1;
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
					goto IL_8D;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 2:
				if (A_1 == null)
				{
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
				num = 2;
			}
		}
		IL_50:
		if (true)
		{
		}
		goto IL_8D;
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("尾⥀≂㝄㍆", a_));
		IL_8D:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠾㍀⩂ㅄ≆㭈", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("帾㍀♂⑄цⅈ⩊㽌㭎", a_), RecordTableEnumerator.b("圾㕀㝂㕄絆晈摊㹌ⱎ㥐㙒㡔㙖⩘畚㉜⽞Ѡൢᵤ੦ը൪ɬᵮᱰቲŴѶ坸ᑺོ᡾꺀ﺈﲐﾒ몔ꖖꦘꮚꮜ낞슠쮢쒤햦\udda8", a_));
		this.ᜎ(A_0, A_1);
	}

	// Token: 0x0600551B RID: 21787 RVA: 0x003602FC File Offset: 0x0035F2FC
	private int ᜑ(XmlWriter A_0, XlsChart A_1, XlsChartSerie A_2)
	{
		int a_ = 0;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_62;
			case 1:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				goto IL_A1;
			case 2:
				goto IL_8B;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_8D;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					break;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 0;
			}
			else
			{
				num = 1;
			}
		}
		IL_62:
		goto IL_8D;
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("唵倷嬹主䨽", a_));
		IL_8D:
		throw new ArgumentNullException(RecordTableEnumerator.b("䄵䨷匹䠻嬽㈿", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("圵䨷弹崻紽⠿⍁㙃㉅", a_), RecordTableEnumerator.b("帵䰷丹䰻н漿流㝃╅⁇⽉⅋⽍⍏籑㭓♕㵗㑙⑛㍝౟ѡୣᑥէ୩ᡫᵭ幯ᵱٳᅵ坷ṹ๻ώꎋ벍ꂏꊑꊓ릕ﮗﶛ풟", a_));
		int result = this.ᜐ(A_0, A_1, A_2);
		this.\u1716(A_0, A_1, A_2);
		A_0.WriteEndElement();
		return result;
	}

	// Token: 0x0600551C RID: 21788 RVA: 0x003603E8 File Offset: 0x0035F3E8
	private int ᜐ(XmlWriter A_0, XlsChart A_1, XlsChartSerie A_2)
	{
		int a_ = 18;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_8B;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_8D;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 2:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				goto IL_A1;
			case 3:
				goto IL_62;
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
				num = 2;
			}
		}
		IL_62:
		goto IL_8D;
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("⭇≉ⵋ㱍⑏", a_));
		IL_8D:
		throw new ArgumentNullException(RecordTableEnumerator.b("㽇㡉╋㩍㕏⁑", a_));
		IL_A1:
		this.ᜎ(A_0, A_2);
		this.ᜌ(A_0, A_2);
		return this.ᜀ(A_0, A_1, A_2, new spr\u2541.ᜀ(this.ᜄ));
	}

	// Token: 0x0600551D RID: 21789 RVA: 0x003604C0 File Offset: 0x0035F4C0
	private int ᜏ(XmlWriter A_0, XlsChart A_1, XlsChartSerie A_2)
	{
		int a_ = 15;
		if (true)
		{
		}
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_48;
			case 1:
				if (A_1 == null)
				{
					num = 4;
					continue;
				}
				num = 3;
				continue;
			case 3:
				if (!A_1.IsChartStock)
				{
					num = 6;
					continue;
				}
				goto IL_F0;
			case 4:
				goto IL_C5;
			case 5:
				goto IL_AA;
			case 6:
				this.ᜎ(A_0, A_2);
				this.ᜌ(A_0, A_2);
				num = 5;
				continue;
			}
			if (A_0 == null)
			{
				num = 0;
			}
			else
			{
				num = 1;
			}
		}
		IL_48:
		throw new ArgumentNullException(RecordTableEnumerator.b("㉄㕆⁈㽊⡌㵎", a_));
		IL_AA:
		goto IL_F0;
		IL_C5:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_48;
		default:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("♄⽆⡈㥊㥌", a_));
		}
		IL_F0:
		return this.ᜀ(A_0, A_1, A_2, new spr\u2541.ᜀ(this.ᜈ));
	}

	// Token: 0x0600551E RID: 21790 RVA: 0x003605D4 File Offset: 0x0035F5D4
	private void ᜋ(XmlWriter A_0, XlsChart A_1)
	{
		int a_ = 4;
		int num = 3;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				goto IL_8B;
			case 1:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				goto IL_A1;
			case 2:
				goto IL_62;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_8D;
				default:
					if (false)
					{
					}
					break;
				}
				break;
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
		IL_62:
		goto IL_8D;
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("夹吻弽㈿㙁", a_));
		IL_8D:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴹主圽㐿❁㙃", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("嘹唻倽┿煁CՅ⁇⭉㹋㩍", a_), RecordTableEnumerator.b("刹䠻䨽〿硁歃楅㭇⥉⑋⭍㵏㍑❓硕㝗⩙㥛そᡟཡࡣeݧᡩū཭ѯű婳᥵੷ᵹ卻᩽뾏ꂑ꒓ꚕ꺗떙ﾛ솟킡킣", a_));
		this.ᜎ(A_0, A_1);
		spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("圹崻䰽⬿❁㙃", a_), RecordTableEnumerator.b("ହ", a_));
	}

	// Token: 0x0600551F RID: 21791 RVA: 0x003606D0 File Offset: 0x0035F6D0
	private int ᜎ(XmlWriter A_0, XlsChart A_1, XlsChartSerie A_2)
	{
		int a_ = 19;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_83;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_85;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 2:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				goto IL_A1;
			case 3:
				goto IL_50;
			}
			if (A_0 == null)
			{
				num = 3;
			}
			else
			{
				num = 2;
			}
		}
		IL_50:
		goto IL_85;
		IL_83:
		throw new ArgumentNullException(RecordTableEnumerator.b("⩈⍊ⱌ㵎═", a_));
		IL_85:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㹈㥊⑌㭎㑐⅒", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("╈≊⍌⩎扐ᝒᙔ㽖㡘⥚⥜", a_), RecordTableEnumerator.b("ⅈ㽊㥌㽎歐籒穔⑖㩘㍚㡜㉞`ၢ䭤ࡦᥨ๪ͬᝮᱰὲ፴ᡶ୸ᙺᱼ୾궂ꒊﶎﲔ練ﺘ낞鎠鎢閤醦蚨좪얬캮쎰잲", a_));
		int result = this.ᜏ(A_0, A_1, A_2);
		this.\u1716(A_0, A_1, A_2);
		A_0.WriteEndElement();
		return result;
	}

	// Token: 0x06005520 RID: 21792 RVA: 0x003607BC File Offset: 0x0035F7BC
	private void ᜊ(XmlWriter A_0, XlsChart A_1)
	{
		int a_ = 1;
		int num = 1;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				goto IL_85;
			case 2:
				goto IL_65;
			case 3:
				goto IL_3C;
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
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("䀶䬸刺䤼娾㍀", a_));
		IL_65:
		throw new ArgumentNullException(RecordTableEnumerator.b("吶儸娺似䬾", a_));
		IL_85:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_65;
		default:
		{
			if (false)
			{
			}
			string localName = RecordTableEnumerator.b("嬶倸唺堼簾⥀≂㝄㍆", a_);
			A_0.WriteStartElement(localName, RecordTableEnumerator.b("弶䴸伺䴼Ծ湀求㙄⑆ⅈ⹊⁌⹎≐絒㩔❖㱘㕚╜㉞ൠբ੤ᕦѨ੪ᥬᱮ彰ᱲݴၶ噸ὺོṾꊌ붎ꆐꎒꎔ뢖滛ﲜ햠", a_));
			this.ᜎ(A_0, A_1);
			spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("娶堸䤺嘼娾㍀", a_), RecordTableEnumerator.b("ض", a_));
			return;
		}
		}
	}

	// Token: 0x06005521 RID: 21793 RVA: 0x003608B8 File Offset: 0x0035F8B8
	private int \u170D(XmlWriter A_0, XlsChart A_1, XlsChartSerie A_2)
	{
		int a_ = 2;
		switch (0)
		{
		default:
		{
			int num = 18;
			for (;;)
			{
				XlsChartFormat xlsChartFormat;
				string text;
				XlsChartSerieDataFormat xlsChartSerieDataFormat;
				switch (num)
				{
				case 0:
					if (A_1.DropLinesStream != null)
					{
						num = 7;
						continue;
					}
					goto IL_2B1;
				case 1:
					goto IL_81;
				case 2:
					num = 5;
					continue;
				case 3:
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("唷嬹主唽┿ぁ", a_), RecordTableEnumerator.b("ष", a_));
					num = 4;
					continue;
				case 4:
					goto IL_1B1;
				case 5:
					if (xlsChartFormat.LineStyle == DropLineStyleType.HiLow)
					{
						num = 9;
						continue;
					}
					goto IL_D6;
				case 6:
					goto IL_B8;
				case 7:
					A_1.DropLinesStream.Position = 0L;
					ShapeParser.WriteNodeFromStream(A_0, A_1.DropLinesStream, true);
					num = 14;
					continue;
				case 8:
					text = RecordTableEnumerator.b("吷匹刻嬽̿⩁╃㑅㱇", a_);
					goto IL_133;
				case 9:
					A_0.WriteElementString(RecordTableEnumerator.b("倷匹瀻儽㜿แⵃ⡅ⵇ㥉", a_), RecordTableEnumerator.b("倷丹䠻丽稿流歃㕅⭇≉⥋⍍ㅏ⅑穓㥕⡗㽙㉛♝ൟ๡ɣ॥ᩧݩ൫ᩭͯ山᭳ѵί啹᡻౽ꆍꊏꊑ꒓ꂕ랗蓮ﾝ튟횡", a_), string.Empty);
					num = 13;
					continue;
				case 10:
					if (true)
					{
					}
					text = RecordTableEnumerator.b("䬷丹医崽⬿Łⱃ❅㩇㹉", a_);
					goto IL_133;
				case 11:
					if (xlsChartSerieDataFormat.IsMarker)
					{
						num = 3;
						continue;
					}
					goto IL_1B1;
				case 12:
					if (A_1 == null)
					{
						num = 6;
						continue;
					}
					num = 15;
					continue;
				case 13:
					goto IL_D6;
				case 14:
					goto IL_249;
				case 15:
					if (!A_1.IsChartStock)
					{
						num = 16;
						continue;
					}
					num = 10;
					continue;
				case 16:
					num = 8;
					continue;
				case 17:
					if (xlsChartFormat.IsChartChartLine)
					{
						num = 2;
						continue;
					}
					goto IL_D6;
				}
				if (A_0 == null)
				{
					num = 1;
					continue;
				}
				num = 12;
				continue;
				IL_D6:
				num = 0;
				continue;
				IL_133:
				string localName = text;
				A_0.WriteStartElement(localName, RecordTableEnumerator.b("倷丹䠻丽稿流歃㕅⭇≉⥋⍍ㅏ⅑穓㥕⡗㽙㉛♝ൟ๡ɣ॥ᩧݩ൫ᩭͯ山᭳ѵί啹᡻౽ꆍꊏꊑ꒓ꂕ랗蓮ﾝ튟횡", a_));
				int result = this.ᜏ(A_0, A_1, A_2);
				this.ᜀ(A_0, A_1, A_2);
				xlsChartSerieDataFormat = (XlsChartSerieDataFormat)A_2.Format;
				num = 11;
				continue;
				IL_1B1:
				xlsChartFormat = (XlsChartFormat)A_2.Format.Options;
				num = 17;
			}
			IL_81:
			throw new ArgumentNullException(RecordTableEnumerator.b("伷䠹唻䨽┿ぁ", a_));
			IL_B8:
			throw new ArgumentNullException(RecordTableEnumerator.b("嬷刹崻䰽㐿", a_));
			IL_249:
			IL_2B1:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_B8;
			default:
			{
				if (false)
				{
				}
				this.\u1716(A_0, A_1, A_2);
				A_0.WriteEndElement();
				int result;
				return result;
			}
			}
			break;
		}
		}
	}

	// Token: 0x06005522 RID: 21794 RVA: 0x00360BA4 File Offset: 0x0035FBA4
	private int ᜌ(XmlWriter A_0, XlsChart A_1, XlsChartSerie A_2)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			int num = 2;
			int result;
			for (;;)
			{
				IChartFormat options;
				BubbleSizeType sizeRepresents;
				switch (num)
				{
				case 0:
					goto IL_94;
				case 1:
					goto IL_176;
				case 3:
					num = 10;
					continue;
				case 4:
				{
					int bubbleScale;
					if (bubbleScale != 100)
					{
						num = 12;
						continue;
					}
					goto IL_94;
				}
				case 5:
					if (true)
					{
					}
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("㭇≉⍋㥍ṏ㝑㍓ᑕⵗ㡙㹛㉝՟ᅡ", a_), RecordTableEnumerator.b("祇", a_));
					num = 7;
					continue;
				case 6:
				{
					if (A_1 == null)
					{
						num = 8;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("⩇㽉⹋ⱍ㱏㝑ᝓ㹕㥗⡙⡛", a_), RecordTableEnumerator.b("⁇㹉㡋㹍橏絑筓╕㭗㉙㥛㍝şᅡ䩣॥ᡧཀྵɫ᙭ᵯṱታ᥵੷᝹ᵻ੽겁ꖉﲍﶓﾗ놝銟銡钣邥螧즩쒫쾭슯욱", a_));
					this.ᜌ(A_0, A_2);
					result = this.ᜀ(A_0, A_1, A_2, new spr\u2541.ᜀ(this.ᜅ));
					options = A_2.Format.Options;
					int bubbleScale = options.BubbleScale;
					num = 4;
					continue;
				}
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A5;
					default:
						if (false)
						{
						}
						goto IL_72;
					}
					break;
				case 8:
					goto IL_D1;
				case 9:
					if (sizeRepresents != BubbleSizeType.Area)
					{
						num = 3;
						continue;
					}
					num = 1;
					continue;
				case 10:
					goto IL_E2;
				case 11:
					if (options.ShowNegativeBubbles)
					{
						goto IL_A5;
					}
					goto IL_72;
				case 12:
				{
					int bubbleScale;
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("⩇㽉⹋ⱍ㱏㝑ݓ㕕㥗㙙㥛", a_), bubbleScale.ToString());
					num = 0;
					continue;
				}
				case 13:
					goto IL_6D;
				}
				if (A_0 == null)
				{
					num = 13;
					continue;
				}
				num = 6;
				continue;
				IL_72:
				sizeRepresents = options.SizeRepresents;
				num = 9;
				continue;
				IL_94:
				num = 11;
				continue;
				IL_A5:
				num = 5;
			}
			IL_6D:
			throw new ArgumentNullException(RecordTableEnumerator.b("㽇㡉╋㩍㕏⁑", a_));
			IL_D1:
			throw new ArgumentNullException(RecordTableEnumerator.b("⭇≉ⵋ㱍⑏", a_));
			IL_E2:
			string text = RecordTableEnumerator.b("㽇", a_);
			goto IL_23E;
			IL_176:
			text = RecordTableEnumerator.b("⥇㡉⥋⽍", a_);
			IL_23E:
			string a_2 = text;
			spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("㭇⍉㙋⭍ɏ㝑⑓⑕㵗⥙㥛そᑟᅡ", a_), a_2);
			this.\u1716(A_0, A_1, A_2);
			A_0.WriteEndElement();
			return result;
		}
		}
	}

	// Token: 0x06005523 RID: 21795 RVA: 0x00360E18 File Offset: 0x0035FE18
	private void ᜉ(XmlWriter A_0, XlsChart A_1)
	{
		int a_ = 8;
		int num = 2;
		for (;;)
		{
			bool flag;
			bool flag2;
			switch (num)
			{
			case 0:
			{
				ExcelChartType pivotChartType;
				if (pivotChartType != ExcelChartType.Surface3DNoColor)
				{
					num = 6;
					continue;
				}
				num = 5;
				continue;
			}
			case 1:
				goto IL_B1;
			case 3:
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("䤽⤿ぁ⅃⁅㩇⭉⅋⭍", a_), RecordTableEnumerator.b("༽", a_));
				num = 7;
				continue;
			case 4:
			{
				ExcelChartType pivotChartType;
				flag = (pivotChartType == ExcelChartType.SurfaceContourNoColor);
				goto IL_104;
			}
			case 5:
				flag = true;
				goto IL_104;
			case 6:
				num = 4;
				continue;
			case 7:
				goto IL_E3;
			case 8:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_66;
				}
				break;
			case 9:
			{
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("䴽㔿ぁ≃❅⭇⽉ཋ♍ㅏ⁑⁓", a_), RecordTableEnumerator.b("嘽㐿㙁㑃籅杇敉㽋ⵍ㡏㝑㥓㝕⭗瑙㍛⹝՟ౡᱣ୥ѧ౩ͫᱭᵯ፱sյ噷ᕹ๻᥽꽿ﾇﶏﺑ뮓꒕ꢗꪙꪛ놝쎟쪡얣풥\udca7", a_));
				ExcelChartType pivotChartType = A_1.PivotChartType;
				num = 0;
				continue;
			}
			case 10:
				if (flag2)
				{
					num = 3;
					continue;
				}
				return;
			}
			if (A_0 == null)
			{
				num = 8;
				continue;
			}
			num = 9;
			continue;
			IL_104:
			flag2 = flag;
			num = 10;
		}
		IL_66:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䤽㈿⭁ぃ⍅㩇", a_));
		IL_B1:
		throw new ArgumentNullException(RecordTableEnumerator.b("崽⠿⍁㙃㉅", a_));
		IL_E3:
		if (true)
		{
		}
	}

	// Token: 0x06005524 RID: 21796 RVA: 0x00360FA8 File Offset: 0x0035FFA8
	private int ᜋ(XmlWriter A_0, XlsChart A_1, XlsChartSerie A_2)
	{
		int a_ = 0;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_34;
			case 1:
				goto IL_65;
			case 2:
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				goto IL_85;
			}
			if (A_0 == null)
			{
				num = 0;
			}
			else
			{
				if (true)
				{
				}
				num = 2;
			}
		}
		IL_34:
		throw new ArgumentNullException(RecordTableEnumerator.b("䄵䨷匹䠻嬽㈿", a_));
		IL_65:
		throw new ArgumentNullException(RecordTableEnumerator.b("唵倷嬹主䨽", a_));
		IL_85:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_65;
		default:
		{
			if (false)
			{
			}
			A_0.WriteStartElement(RecordTableEnumerator.b("䔵䴷䠹娻弽⌿❁݃⹅⥇㡉㡋", a_), RecordTableEnumerator.b("帵䰷丹䰻н漿流㝃╅⁇⽉⅋⽍⍏籑㭓♕㵗㑙⑛㍝౟ѡୣᑥէ୩ᡫᵭ幯ᵱٳᅵ坷ṹ๻ώꎋ벍ꂏꊑꊓ릕ﮗﶛ풟", a_));
			int result = this.ᜉ(A_0, A_1, A_2);
			this.\u1716(A_0, A_1, A_2);
			A_0.WriteEndElement();
			return result;
		}
		}
	}

	// Token: 0x06005525 RID: 21797 RVA: 0x00361094 File Offset: 0x00360094
	private void ᜈ(XmlWriter A_0, XlsChart A_1)
	{
		int a_ = 11;
		int num = 5;
		for (;;)
		{
			bool flag;
			bool flag2;
			switch (num)
			{
			case 0:
			{
				ExcelChartType pivotChartType;
				if (pivotChartType != ExcelChartType.Surface3DNoColor)
				{
					num = 6;
					continue;
				}
				num = 7;
				continue;
			}
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_66;
				}
				break;
			case 2:
			{
				if (true)
				{
				}
				ExcelChartType pivotChartType;
				flag = (pivotChartType == ExcelChartType.SurfaceContourNoColor);
				goto IL_104;
			}
			case 3:
				goto IL_B9;
			case 4:
				return;
			case 6:
				num = 2;
				continue;
			case 7:
				flag = true;
				goto IL_104;
			case 8:
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("㙀⩂㝄≆⽈㥊ⱌ≎㑐", a_), RecordTableEnumerator.b("灀", a_));
				num = 4;
				continue;
			case 9:
				if (flag2)
				{
					num = 8;
					continue;
				}
				return;
			case 10:
			{
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("㉀㙂㝄ⅆ⡈⡊⡌籎ᕐၒ㵔㙖⭘⽚", a_), RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂ﲊﺒ璉뢖ꮘꮚ궜ꦞ躠삢춤욦\udba8\udfaa", a_));
				ExcelChartType pivotChartType = A_1.PivotChartType;
				num = 0;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			num = 10;
			continue;
			IL_104:
			flag2 = flag;
			num = 9;
		}
		IL_66:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㙀ㅂⱄ㍆ⱈ㥊", a_));
		IL_B9:
		throw new ArgumentNullException(RecordTableEnumerator.b("≀⭂⑄㕆㵈", a_));
	}

	// Token: 0x06005526 RID: 21798 RVA: 0x00361224 File Offset: 0x00360224
	private int ᜊ(XmlWriter A_0, XlsChart A_1, XlsChartSerie A_2)
	{
		int a_ = 11;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				goto IL_85;
			case 2:
				goto IL_34;
			case 3:
				goto IL_65;
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
		IL_34:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㙀ㅂⱄ㍆ⱈ㥊", a_));
		IL_65:
		throw new ArgumentNullException(RecordTableEnumerator.b("≀⭂⑄㕆㵈", a_));
		IL_85:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_65;
		default:
		{
			if (false)
			{
			}
			A_0.WriteStartElement(RecordTableEnumerator.b("㉀㙂㝄ⅆ⡈⡊⡌籎ᕐၒ㵔㙖⭘⽚", a_), RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂ﲊﺒ璉뢖ꮘꮚ궜ꦞ躠삢춤욦\udba8\udfaa", a_));
			int result = this.ᜉ(A_0, A_1, A_2);
			this.\u1716(A_0, A_1, A_2);
			A_0.WriteEndElement();
			return result;
		}
		}
	}

	// Token: 0x06005527 RID: 21799 RVA: 0x00361310 File Offset: 0x00360310
	private int ᜉ(XmlWriter A_0, XlsChart A_1, XlsChartSerie A_2)
	{
		int a_ = 0;
		int num = 1;
		for (;;)
		{
			bool flag;
			bool flag2;
			switch (num)
			{
			case 0:
				num = 5;
				continue;
			case 2:
				if (flag)
				{
					num = 4;
					continue;
				}
				goto IL_155;
			case 3:
			{
				if (A_1 == null)
				{
					num = 6;
					continue;
				}
				ExcelChartType serieType = A_2.SerieType;
				num = 10;
				continue;
			}
			case 4:
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("䄵儷䠹夻堽㈿⍁⥃⍅", a_), RecordTableEnumerator.b("ܵ", a_));
				num = 9;
				continue;
			case 5:
			{
				ExcelChartType serieType;
				flag2 = (serieType == ExcelChartType.SurfaceContourNoColor);
				goto IL_FB;
			}
			case 6:
				goto IL_B3;
			case 7:
				flag2 = true;
				goto IL_FB;
			case 8:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_6E;
				}
				break;
			case 9:
				goto IL_E5;
			case 10:
			{
				ExcelChartType serieType;
				if (serieType != ExcelChartType.Surface3DNoColor)
				{
					num = 0;
					continue;
				}
				num = 7;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 8;
				continue;
			}
			num = 3;
			continue;
			IL_FB:
			flag = flag2;
			num = 2;
		}
		IL_6E:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䄵䨷匹䠻嬽㈿", a_));
		IL_B3:
		throw new ArgumentNullException(RecordTableEnumerator.b("唵倷嬹主䨽", a_));
		IL_E5:
		IL_155:
		int result = this.ᜀ(A_0, A_1, A_2, new spr\u2541.ᜀ(this.ᜋ));
		this.ᜇ(A_0, A_1);
		return result;
	}

	// Token: 0x06005528 RID: 21800 RVA: 0x00361494 File Offset: 0x00360494
	private void ᜇ(XmlWriter A_0, XlsChart A_1)
	{
		for (;;)
		{
			Stream stream = A_1.PreservedBandFormats;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					num = 2;
					continue;
				case 1:
					return;
				case 2:
					if (stream.Length > 0L)
					{
						num = 4;
						continue;
					}
					return;
				case 3:
					if (stream != null)
					{
						num = 0;
						continue;
					}
					return;
				case 4:
					stream.Position = 0L;
					ShapeParser.WriteNodeFromStream(A_0, stream);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				}
				break;
			}
		}
	}

	// Token: 0x06005529 RID: 21801 RVA: 0x00361544 File Offset: 0x00360544
	private int ᜀ(XmlWriter A_0, XlsChart A_1, int A_2)
	{
		int a_ = 7;
		switch (0)
		{
		default:
		{
			int num = 28;
			int result;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_256;
				case 1:
					goto IL_2B1;
				case 2:
					return result;
				case 3:
					goto IL_165;
				case 4:
					goto IL_2E9;
				case 5:
					goto IL_181;
				case 6:
					goto IL_126;
				case 7:
					goto IL_19D;
				case 8:
				{
					ExcelChartType serieType;
					switch (serieType)
					{
					case ExcelChartType.ColumnClustered:
					case ExcelChartType.ColumnStacked:
					case ExcelChartType.Column100PercentStacked:
					case ExcelChartType.BarClustered:
					case ExcelChartType.BarStacked:
					case ExcelChartType.Bar100PercentStacked:
					{
						XlsChartSerie xlsChartSerie;
						result = this.\u1717(A_0, A_1, xlsChartSerie);
						num = 4;
						continue;
					}
					case ExcelChartType.Column3DClustered:
					case ExcelChartType.Column3DStacked:
					case ExcelChartType.Column3D100PercentStacked:
					case ExcelChartType.Column3D:
					case ExcelChartType.Bar3DClustered:
					case ExcelChartType.Bar3DStacked:
					case ExcelChartType.Bar3D100PercentStacked:
					case ExcelChartType.CylinderClustered:
					case ExcelChartType.CylinderStacked:
					case ExcelChartType.Cylinder100PercentStacked:
					case ExcelChartType.CylinderBarClustered:
					case ExcelChartType.CylinderBarStacked:
					case ExcelChartType.CylinderBar100PercentStacked:
					case ExcelChartType.Cylinder3DClustered:
					case ExcelChartType.ConeClustered:
					case ExcelChartType.ConeStacked:
					case ExcelChartType.Cone100PercentStacked:
					case ExcelChartType.ConeBarClustered:
					case ExcelChartType.ConeBarStacked:
					case ExcelChartType.ConeBar100PercentStacked:
					case ExcelChartType.Cone3DClustered:
					case ExcelChartType.PyramidClustered:
					case ExcelChartType.PyramidStacked:
					case ExcelChartType.Pyramid100PercentStacked:
					case ExcelChartType.PyramidBarClustered:
					case ExcelChartType.PyramidBarStacked:
					case ExcelChartType.PyramidBar100PercentStacked:
					case ExcelChartType.Pyramid3DClustered:
					{
						XlsChartSerie xlsChartSerie;
						result = this.\u1715(A_0, A_1, xlsChartSerie);
						num = 17;
						continue;
					}
					case ExcelChartType.Line:
					case ExcelChartType.LineStacked:
					case ExcelChartType.Line100PercentStacked:
					case ExcelChartType.LineMarkers:
					case ExcelChartType.LineMarkersStacked:
					case ExcelChartType.LineMarkers100PercentStacked:
					{
						XlsChartSerie xlsChartSerie;
						result = this.\u170D(A_0, A_1, xlsChartSerie);
						num = 7;
						continue;
					}
					case ExcelChartType.Line3D:
					{
						XlsChartSerie xlsChartSerie;
						result = this.ᜎ(A_0, A_1, xlsChartSerie);
						num = 29;
						continue;
					}
					case ExcelChartType.Pie:
					case ExcelChartType.PieExploded:
					{
						XlsChartSerie xlsChartSerie;
						result = this.ᜆ(A_0, A_1, xlsChartSerie);
						num = 26;
						continue;
					}
					case ExcelChartType.Pie3D:
					case ExcelChartType.Pie3DExploded:
					{
						XlsChartSerie xlsChartSerie;
						result = this.ᜅ(A_0, A_1, xlsChartSerie);
						num = 12;
						continue;
					}
					case ExcelChartType.PieOfPie:
					case ExcelChartType.PieBar:
					{
						XlsChartSerie xlsChartSerie;
						result = this.ᜄ(A_0, A_1, xlsChartSerie);
						num = 23;
						continue;
					}
					case ExcelChartType.ScatterMarkers:
					case ExcelChartType.ScatterSmoothedLineMarkers:
					case ExcelChartType.ScatterSmoothedLine:
					case ExcelChartType.ScatterLineMarkers:
					case ExcelChartType.ScatterLine:
					{
						XlsChartSerie xlsChartSerie;
						result = this.ᜇ(A_0, A_1, xlsChartSerie);
						num = 13;
						continue;
					}
					case ExcelChartType.Area:
					case ExcelChartType.AreaStacked:
					case ExcelChartType.Area100PercentStacked:
					{
						XlsChartSerie xlsChartSerie;
						result = this.ᜑ(A_0, A_1, xlsChartSerie);
						num = 3;
						continue;
					}
					case ExcelChartType.Area3D:
					case ExcelChartType.Area3DStacked:
					case ExcelChartType.Area3D100PercentStacked:
					{
						XlsChartSerie xlsChartSerie;
						result = this.\u1712(A_0, A_1, xlsChartSerie);
						num = 10;
						continue;
					}
					case ExcelChartType.Doughnut:
					case ExcelChartType.DoughnutExploded:
					{
						XlsChartSerie xlsChartSerie;
						result = this.ᜂ(A_0, A_1, xlsChartSerie);
						num = 1;
						continue;
					}
					case ExcelChartType.Radar:
					case ExcelChartType.RadarMarkers:
					case ExcelChartType.RadarFilled:
					{
						XlsChartSerie xlsChartSerie;
						result = this.ᜈ(A_0, A_1, xlsChartSerie);
						num = 6;
						continue;
					}
					case ExcelChartType.Surface3D:
					case ExcelChartType.Surface3DNoColor:
					{
						XlsChartSerie xlsChartSerie;
						result = this.ᜊ(A_0, A_1, xlsChartSerie);
						num = 14;
						continue;
					}
					case ExcelChartType.SurfaceContour:
					case ExcelChartType.SurfaceContourNoColor:
					{
						XlsChartSerie xlsChartSerie;
						result = this.ᜋ(A_0, A_1, xlsChartSerie);
						num = 5;
						continue;
					}
					case ExcelChartType.Bubble:
					case ExcelChartType.Bubble3D:
					{
						XlsChartSerie xlsChartSerie;
						result = this.ᜌ(A_0, A_1, xlsChartSerie);
						num = 2;
						continue;
					}
					case ExcelChartType.StockHighLowClose:
					case ExcelChartType.StockOpenHighLowClose:
					case ExcelChartType.StockVolumeHighLowClose:
					case ExcelChartType.StockVolumeOpenHighLowClose:
					{
						XlsChartSerie xlsChartSerie;
						result = this.ᜃ(A_0, A_1, xlsChartSerie);
						num = 9;
						continue;
					}
					default:
						num = 11;
						continue;
					}
					break;
				}
				case 9:
					goto IL_1EA;
				case 10:
					goto IL_2CD;
				case 11:
					num = 30;
					continue;
				case 12:
					goto IL_321;
				case 13:
					goto IL_DA;
				case 14:
					goto IL_1CE;
				case 15:
				{
					XlsChartSerie xlsChartSerie2;
					if (xlsChartSerie2.ChartGroup == A_2)
					{
						num = 22;
						continue;
					}
					int num2;
					num2++;
					num = 0;
					continue;
				}
				case 16:
				{
					if (A_1 == null)
					{
						num = 18;
						continue;
					}
					XlsChartSeries xlsChartSeries = A_1.Series;
					XlsChartSerie xlsChartSerie = null;
					int num2 = 0;
					int count = xlsChartSeries.Count;
					num = 20;
					continue;
				}
				case 17:
					goto IL_305;
				case 18:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_272;
					default:
						goto IL_33C;
					}
					break;
				case 19:
					goto IL_BE;
				case 20:
					goto IL_256;
				case 21:
					goto IL_274;
				case 22:
				{
					XlsChartSerie xlsChartSerie2;
					XlsChartSerie xlsChartSerie = xlsChartSerie2;
					num = 21;
					continue;
				}
				case 23:
					return result;
				case 24:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 31;
						continue;
					}
					XlsChartSeries xlsChartSeries;
					XlsChartSerie xlsChartSerie2 = (XlsChartSerie)xlsChartSeries[num2];
					num = 15;
					continue;
				}
				case 25:
				{
					XlsChartSerie xlsChartSerie;
					if (xlsChartSerie != null)
					{
						num = 27;
						continue;
					}
					return result;
				}
				case 26:
					return result;
				case 27:
				{
					XlsChartSerie xlsChartSerie;
					ExcelChartType serieType = xlsChartSerie.SerieType;
					num = 8;
					continue;
				}
				case 29:
					goto IL_F6;
				case 30:
					goto IL_205;
				case 31:
					goto IL_272;
				}
				if (A_0 == null)
				{
					num = 19;
					continue;
				}
				num = 16;
				continue;
				IL_256:
				num = 24;
				continue;
				IL_274:
				result = 0;
				num = 25;
				continue;
				IL_272:
				goto IL_274;
			}
			IL_BE:
			throw new ArgumentNullException(RecordTableEnumerator.b("䨼䴾⡀㝂⁄㕆", a_));
			IL_DA:
			IL_F6:
			IL_126:
			IL_165:
			IL_181:
			IL_19D:
			IL_1CE:
			IL_1EA:
			IL_205:
			IL_2B1:
			IL_2CD:
			IL_2E9:
			IL_305:
			IL_321:
			return result;
			IL_33C:
			if (true)
			{
			}
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("帼圾⁀ㅂㅄ", a_));
		}
		}
	}

	// Token: 0x0600552A RID: 21802 RVA: 0x00361A74 File Offset: 0x00360A74
	private void ᜆ(XmlWriter A_0, XlsChart A_1)
	{
		int a_ = 3;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				ExcelChartType pivotChartType;
				switch (pivotChartType)
				{
				case ExcelChartType.ColumnClustered:
				case ExcelChartType.ColumnStacked:
				case ExcelChartType.Column100PercentStacked:
				case ExcelChartType.BarClustered:
				case ExcelChartType.BarStacked:
				case ExcelChartType.Bar100PercentStacked:
					goto IL_247;
				case ExcelChartType.Column3DClustered:
				case ExcelChartType.Column3DStacked:
				case ExcelChartType.Column3D100PercentStacked:
				case ExcelChartType.Column3D:
				case ExcelChartType.Bar3DClustered:
				case ExcelChartType.Bar3DStacked:
				case ExcelChartType.Bar3D100PercentStacked:
				case ExcelChartType.CylinderClustered:
				case ExcelChartType.CylinderStacked:
				case ExcelChartType.Cylinder100PercentStacked:
				case ExcelChartType.CylinderBarClustered:
				case ExcelChartType.CylinderBarStacked:
				case ExcelChartType.CylinderBar100PercentStacked:
				case ExcelChartType.Cylinder3DClustered:
				case ExcelChartType.ConeClustered:
				case ExcelChartType.ConeStacked:
				case ExcelChartType.Cone100PercentStacked:
				case ExcelChartType.ConeBarClustered:
				case ExcelChartType.ConeBarStacked:
				case ExcelChartType.ConeBar100PercentStacked:
				case ExcelChartType.Cone3DClustered:
				case ExcelChartType.PyramidClustered:
				case ExcelChartType.PyramidStacked:
				case ExcelChartType.Pyramid100PercentStacked:
				case ExcelChartType.PyramidBarClustered:
				case ExcelChartType.PyramidBarStacked:
				case ExcelChartType.PyramidBar100PercentStacked:
				case ExcelChartType.Pyramid3DClustered:
					goto IL_28E;
				case ExcelChartType.Line:
				case ExcelChartType.LineStacked:
				case ExcelChartType.Line100PercentStacked:
				case ExcelChartType.LineMarkers:
				case ExcelChartType.LineMarkersStacked:
				case ExcelChartType.LineMarkers100PercentStacked:
					goto IL_2A9;
				case ExcelChartType.Line3D:
					goto IL_D7;
				case ExcelChartType.Pie:
				case ExcelChartType.PieExploded:
					goto IL_75;
				case ExcelChartType.Pie3D:
				case ExcelChartType.Pie3DExploded:
					goto IL_2A0;
				case ExcelChartType.PieOfPie:
				case ExcelChartType.PieBar:
					goto IL_C5;
				case ExcelChartType.ScatterMarkers:
				case ExcelChartType.ScatterSmoothedLineMarkers:
				case ExcelChartType.ScatterSmoothedLine:
				case ExcelChartType.ScatterLineMarkers:
				case ExcelChartType.ScatterLine:
				case ExcelChartType.Bubble:
				case ExcelChartType.Bubble3D:
				case ExcelChartType.StockHighLowClose:
				case ExcelChartType.StockOpenHighLowClose:
				case ExcelChartType.StockVolumeHighLowClose:
				case ExcelChartType.StockVolumeOpenHighLowClose:
					return;
				case ExcelChartType.Area:
				case ExcelChartType.AreaStacked:
				case ExcelChartType.Area100PercentStacked:
					goto IL_6C;
				case ExcelChartType.Area3D:
				case ExcelChartType.Area3DStacked:
				case ExcelChartType.Area3D100PercentStacked:
					goto IL_297;
				case ExcelChartType.Doughnut:
				case ExcelChartType.DoughnutExploded:
					goto IL_A8;
				case ExcelChartType.Radar:
				case ExcelChartType.RadarMarkers:
				case ExcelChartType.RadarFilled:
					goto IL_229;
				case ExcelChartType.Surface3D:
				case ExcelChartType.Surface3DNoColor:
					goto IL_CE;
				case ExcelChartType.SurfaceContour:
				case ExcelChartType.SurfaceContourNoColor:
					this.ᜉ(A_0, A_1);
					num = 4;
					continue;
				default:
					num = 3;
					continue;
				}
				break;
			}
			case 2:
				goto IL_281;
			case 3:
				return;
			case 4:
				goto IL_245;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_91;
				default:
					goto IL_61;
				}
				break;
			case 6:
			{
				ExcelChartType pivotChartType = A_1.PivotChartType;
				num = 0;
				continue;
			}
			case 7:
				if (A_1.HasPivotTable)
				{
					goto IL_91;
				}
				return;
			case 8:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				num = 7;
				continue;
			}
			if (A_0 == null)
			{
				num = 5;
				continue;
			}
			num = 8;
			continue;
			IL_91:
			num = 6;
		}
		IL_61:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("丸䤺吼䬾⑀ㅂ", a_));
		IL_6C:
		this.ᜌ(A_0, A_1);
		return;
		IL_75:
		this.ᜄ(A_0, A_1);
		return;
		IL_A8:
		this.ᜁ(A_0, A_1);
		return;
		IL_C5:
		this.ᜂ(A_0, A_1);
		return;
		IL_CE:
		this.ᜈ(A_0, A_1);
		return;
		IL_D7:
		this.ᜋ(A_0, A_1);
		return;
		IL_229:
		this.ᜅ(A_0, A_1);
		return;
		IL_245:
		return;
		IL_247:
		this.ᜑ(A_0, A_1);
		return;
		IL_281:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("娸区尼䴾㕀", a_));
		IL_28E:
		this.ᜐ(A_0, A_1);
		return;
		IL_297:
		this.\u170D(A_0, A_1);
		return;
		IL_2A0:
		this.ᜃ(A_0, A_1);
		return;
		IL_2A9:
		this.ᜊ(A_0, A_1);
	}

	// Token: 0x0600552B RID: 21803 RVA: 0x00361D34 File Offset: 0x00360D34
	private void ᜅ(XmlWriter A_0, XlsChart A_1)
	{
		int a_ = 8;
		int num = 2;
		for (;;)
		{
			IL_13:
			switch (num)
			{
			case 0:
				goto IL_8B;
			case 1:
				goto IL_34;
			case 3:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				goto IL_A1;
			}
			while (A_0 != null)
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
					num = 3;
					goto IL_13;
				}
			}
			num = 1;
		}
		IL_34:
		throw new ArgumentNullException(RecordTableEnumerator.b("䤽㈿⭁ぃ⍅㩇", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("崽⠿⍁㙃㉅", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("䰽ℿ♁╃㑅େ≉ⵋ㱍⑏", a_), RecordTableEnumerator.b("嘽㐿㙁㑃籅杇敉㽋ⵍ㡏㝑㥓㝕⭗瑙㍛⹝՟ౡᱣ୥ѧ౩ͫᱭᵯ፱sյ噷ᕹ๻᥽꽿ﾇﶏﺑ뮓꒕ꢗꪙꪛ놝쎟쪡얣풥\udca7", a_));
		XLSXRadarStyle pivotChartType = (XLSXRadarStyle)A_1.PivotChartType;
		spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("䰽ℿ♁╃㑅ᭇ㹉㕋≍㕏", a_), pivotChartType.ToString());
	}

	// Token: 0x0600552C RID: 21804 RVA: 0x00361E2C File Offset: 0x00360E2C
	private int ᜈ(XmlWriter A_0, XlsChart A_1, XlsChartSerie A_2)
	{
		int a_ = 9;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_3C;
			case 1:
				goto IL_8B;
			case 3:
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				goto IL_A1;
			}
			for (;;)
			{
				if (true)
				{
				}
				if (A_0 == null)
				{
					break;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_68;
				}
			}
			num = 0;
			continue;
			IL_68:
			if (false)
			{
			}
			num = 3;
		}
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠾㍀⩂ㅄ≆㭈", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("尾⥀≂㝄㍆", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("䴾⁀❂⑄㕆ੈ⍊ⱌ㵎═", a_), RecordTableEnumerator.b("圾㕀㝂㕄絆晈摊㹌ⱎ㥐㙒㡔㙖⩘畚㉜⽞Ѡൢᵤ੦ը൪ɬᵮᱰቲŴѶ坸ᑺོ᡾꺀ﺈﲐﾒ몔ꖖꦘꮚꮜ낞슠쮢쒤햦\udda8", a_));
		XLSXRadarStyle serieType = (XLSXRadarStyle)A_2.SerieType;
		spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("䴾⁀❂⑄㕆ᩈ㽊㑌⍎㑐", a_), serieType.ToString());
		this.ᜌ(A_0, A_2);
		int result = this.ᜀ(A_0, A_1, A_2, new spr\u2541.ᜀ(this.ᜆ));
		this.\u1716(A_0, A_1, A_2);
		A_0.WriteEndElement();
		return result;
	}

	// Token: 0x0600552D RID: 21805 RVA: 0x00361F50 File Offset: 0x00360F50
	private int ᜇ(XmlWriter A_0, XlsChart A_1, XlsChartSerie A_2)
	{
		int a_ = 6;
		int num = 3;
		for (;;)
		{
			IL_13:
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				goto IL_99;
			case 1:
				goto IL_83;
			case 2:
				goto IL_34;
			}
			while (A_0 != null)
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
					goto IL_13;
				}
			}
			num = 2;
		}
		IL_34:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬻䰽⤿㙁⅃㑅", a_));
		IL_83:
		throw new ArgumentNullException(RecordTableEnumerator.b("弻嘽ℿぁぃ", a_));
		IL_99:
		if (true)
		{
		}
		A_0.WriteStartElement(RecordTableEnumerator.b("伻崽ℿ㙁ぃ⍅㩇ॉ⑋⽍≏♑", a_), RecordTableEnumerator.b("吻䨽㐿㉁繃楅杇㥉⽋♍㕏㽑㕓╕癗㕙ⱛ㭝๟ᩡॣ੥๧թṫͭᅯٱݳ塵᝷ࡹ᭻兽ﲏ붑ꚓꚕꢗ겙뎛ﶝ좟쎡횣튥", a_));
		XLSXScatterStyle serieType = (XLSXScatterStyle)A_2.SerieType;
		spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("伻崽ℿ㙁ぃ⍅㩇᥉㡋㝍㱏㝑", a_), serieType.ToString());
		this.ᜌ(A_0, A_2);
		int result = this.ᜀ(A_0, A_1, A_2, new spr\u2541.ᜀ(this.ᜇ));
		this.ᜀ(A_0, A_1, A_2);
		this.\u1716(A_0, A_1, A_2);
		A_0.WriteEndElement();
		return result;
	}

	// Token: 0x0600552E RID: 21806 RVA: 0x00362080 File Offset: 0x00361080
	private void ᜄ(XmlWriter A_0, XlsChart A_1)
	{
		int a_ = 19;
		int num = 0;
		for (;;)
		{
			IL_13:
			switch (num)
			{
			case 1:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				goto IL_A1;
			case 2:
				goto IL_8B;
			case 3:
				goto IL_34;
			}
			while (A_0 != null)
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
					num = 1;
					goto IL_13;
				}
			}
			num = 3;
		}
		IL_34:
		throw new ArgumentNullException(RecordTableEnumerator.b("㹈㥊⑌㭎㑐⅒", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("⩈⍊ⱌ㵎═", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("㥈≊⡌౎㥐㉒❔⍖", a_), RecordTableEnumerator.b("ⅈ㽊㥌㽎歐籒穔⑖㩘㍚㡜㉞`ၢ䭤ࡦᥨ๪ͬᝮᱰὲ፴ᡶ୸ᙺᱼ୾궂ꒊﶎﲔ練ﺘ낞鎠鎢閤醦蚨좪얬캮쎰잲", a_));
		spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("㽈⩊㽌㙎ቐ㱒㥔㡖⭘⡚", a_), true);
	}

	// Token: 0x0600552F RID: 21807 RVA: 0x00362168 File Offset: 0x00361168
	private int ᜆ(XmlWriter A_0, XlsChart A_1, XlsChartSerie A_2)
	{
		int a_ = 3;
		int num = 1;
		for (;;)
		{
			IL_13:
			switch (num)
			{
			case 0:
				goto IL_8B;
			case 2:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				goto IL_A1;
			case 3:
				goto IL_34;
			}
			while (A_0 != null)
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
					goto IL_13;
				}
			}
			num = 3;
		}
		IL_34:
		throw new ArgumentNullException(RecordTableEnumerator.b("丸䤺吼䬾⑀ㅂ", a_));
		IL_8B:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("娸区尼䴾㕀", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("䤸刺堼簾⥀≂㝄㍆", a_), RecordTableEnumerator.b("儸伺䤼伾筀求橄㑆⩈⍊⡌≎ぐ⁒答㡖⥘㹚㍜❞ౠརͤࡦ᭨٪౬᭮ɰ嵲ᩴնṸ呺᥼ൾꂎꎐꎒꖔꆖ뚘ﺞ펠힢", a_));
		int result = this.ᜁ(A_0, A_1, A_2);
		IChartFormat options = A_2.Format.Options;
		int firstSliceAngle = options.FirstSliceAngle;
		spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("弸刺似䰾㕀၂⥄⹆⩈⹊ౌⅎ㙐", a_), firstSliceAngle.ToString());
		A_0.WriteEndElement();
		return result;
	}

	// Token: 0x06005530 RID: 21808 RVA: 0x00362278 File Offset: 0x00361278
	private void ᜃ(XmlWriter A_0, XlsChart A_1)
	{
		int a_ = 2;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_3C;
			case 2:
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				goto IL_A1;
			case 3:
				goto IL_8B;
			}
			for (;;)
			{
				if (true)
				{
				}
				if (A_0 == null)
				{
					break;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_68;
				}
			}
			num = 1;
			continue;
			IL_68:
			if (false)
			{
			}
			num = 2;
		}
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("伷䠹唻䨽┿ぁ", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("嬷刹崻䰽㐿", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("䠷匹夻ഽпŁⱃ❅㩇㹉", a_), RecordTableEnumerator.b("倷丹䠻丽稿流歃㕅⭇≉⥋⍍ㅏ⅑穓㥕⡗㽙㉛♝ൟ๡ɣ॥ᩧݩ൫ᩭͯ山᭳ѵί啹᡻౽ꆍꊏꊑ꒓ꂕ랗蓮ﾝ튟횡", a_));
		spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("丷嬹主䜽̿ⵁ⡃⥅㩇㥉", a_), true);
	}

	// Token: 0x06005531 RID: 21809 RVA: 0x00362360 File Offset: 0x00361360
	private int ᜅ(XmlWriter A_0, XlsChart A_1, XlsChartSerie A_2)
	{
		int a_ = 16;
		if (true)
		{
		}
		int num = 2;
		for (;;)
		{
			IL_1B:
			switch (num)
			{
			case 0:
				goto IL_3C;
			case 1:
				goto IL_8B;
			case 3:
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				goto IL_A1;
			}
			while (A_0 != null)
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
					goto IL_1B;
				}
			}
			num = 0;
		}
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅅ㩇⍉㡋⭍≏", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("╅⁇⭉㹋㩍", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("㙅ⅇ⽉罋੍ፏ㩑㕓⑕ⱗ", a_), RecordTableEnumerator.b("⹅㱇㹉㱋瑍罏絑❓㕕し㽙ㅛ㽝፟䱡ୣᙥ൧ѩᑫͭᱯᑱ᭳ѵᕷ᭹ࡻൽ깿ꞇﺋﮑ望뎛겝邟銡銣覥쮧슩춫\udcad쒯", a_));
		int result = this.ᜁ(A_0, A_1, A_2);
		A_0.WriteEndElement();
		return result;
	}

	// Token: 0x06005532 RID: 21810 RVA: 0x00362444 File Offset: 0x00361444
	private void ᜂ(XmlWriter A_0, XlsChart A_1)
	{
		int a_ = 2;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("圷尹氻圽┿Łⱃ❅㩇㹉", a_), RecordTableEnumerator.b("倷丹䠻丽稿流歃㕅⭇≉⥋⍍ㅏ⅑穓㥕⡗㽙㉛♝ൟ๡ɣ॥ᩧݩ൫ᩭͯ山᭳ѵί啹᡻౽ꆍꊏꊑ꒓ꂕ랗蓮ﾝ튟횡", a_));
				num = 7;
				continue;
			case 1:
				goto IL_E6;
			case 2:
				goto IL_47;
			case 4:
				goto IL_51;
			case 5:
				num = 4;
				continue;
			case 6:
				goto IL_BD;
			case 7:
				if (true)
				{
				}
				if (A_1.PivotChartType != ExcelChartType.PieOfPie)
				{
					num = 5;
					continue;
				}
				num = 6;
				continue;
			}
			if (A_0 == null)
			{
				num = 2;
			}
			else
			{
				num = 0;
			}
		}
		IL_47:
		throw new ArgumentNullException(RecordTableEnumerator.b("伷䠹唻䨽┿ぁ", a_));
		IL_51:
		string text = RecordTableEnumerator.b("娷嬹主", a_);
		goto IL_13A;
		IL_BD:
		text = RecordTableEnumerator.b("䠷匹夻", a_);
		goto IL_13A;
		IL_E6:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_BD;
		default:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("嬷刹崻䰽㐿", a_));
		}
		IL_13A:
		string a_2 = text;
		spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("圷尹氻圽┿ᙁ㵃㙅ⵇ", a_), a_2);
		spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("丷嬹主䜽̿ⵁ⡃⥅㩇㥉", a_), true);
	}

	// Token: 0x06005533 RID: 21811 RVA: 0x003625B8 File Offset: 0x003615B8
	private int ᜄ(XmlWriter A_0, XlsChart A_1, XlsChartSerie A_2)
	{
		int a_ = 7;
		switch (0)
		{
		default:
		{
			int num = 9;
			IChartFormat options;
			int result;
			for (;;)
			{
				string text;
				int splitValue;
				switch (num)
				{
				case 0:
					goto IL_C6;
				case 1:
					goto IL_61;
				case 2:
					text = RecordTableEnumerator.b("弼帾㍀", a_);
					goto IL_14F;
				case 3:
					if (A_2.SerieType != ExcelChartType.PieOfPie)
					{
						num = 5;
						continue;
					}
					num = 4;
					continue;
				case 4:
					text = RecordTableEnumerator.b("䴼嘾⑀", a_);
					goto IL_14F;
				case 5:
					num = 2;
					continue;
				case 6:
				{
					XLSXSplitType splitType = (XLSXSplitType)options.SplitType;
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("丼伾ⵀ⩂ㅄፆえ㭊⡌", a_), splitType.ToString());
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("丼伾ⵀ⩂ㅄᝆ♈㡊", a_), splitValue.ToString());
					num = 7;
					continue;
				}
				case 7:
					goto IL_11A;
				case 8:
					if (A_1 == null)
					{
						num = 0;
						continue;
					}
					if (true)
					{
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("刼夾ᅀ⩂⁄цⅈ⩊㽌㭎", a_), RecordTableEnumerator.b("唼䬾㕀㍂罄框晈㡊⹌❎㑐㹒㑔⑖睘㑚ⵜ㩞འ᭢ࡤ୦ཨѪὬɮၰݲٴ奶ᙸॺ᩼偾﶐벒ꞔꞖꦘ궚늜ﲞ즠슢힤펦", a_));
					num = 3;
					continue;
				case 10:
					if (splitValue != 0)
					{
						num = 6;
						continue;
					}
					goto IL_22D;
				}
				if (A_0 == null)
				{
					num = 1;
					continue;
				}
				num = 8;
				continue;
				IL_14F:
				string a_2 = text;
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("刼夾ᅀ⩂⁄ፆえ㭊⡌", a_), a_2);
				result = this.ᜁ(A_0, A_1, A_2);
				options = A_2.Format.Options;
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("娼帾ㅀᑂⱄ⍆㵈⍊", a_), options.GapWidth.ToString());
				splitValue = options.SplitValue;
				num = 10;
			}
			IL_61:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_11A:
				goto IL_22D;
			default:
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("䨼䴾⡀㝂⁄㕆", a_));
			}
			IL_C6:
			throw new ArgumentNullException(RecordTableEnumerator.b("帼圾⁀ㅂㅄ", a_));
			IL_22D:
			spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("丼娾≀ⱂ⭄⍆᥈≊⡌ᱎ㡐⥒ご", a_), options.PieSecondSize.ToString());
			A_0.WriteElementString(RecordTableEnumerator.b("丼娾㍀གⱄ⥆ⱈ㡊", a_), RecordTableEnumerator.b("唼䬾㕀㍂罄框晈㡊⹌❎㑐㹒㑔⑖睘㑚ⵜ㩞འ᭢ࡤ୦ཨѪὬɮၰݲٴ奶ᙸॺ᩼偾﶐벒ꞔꞖꦘ궚늜ﲞ즠슢힤펦", a_), string.Empty);
			A_0.WriteEndElement();
			return result;
		}
		}
	}

	// Token: 0x06005534 RID: 21812 RVA: 0x00362844 File Offset: 0x00361844
	private int ᜃ(XmlWriter A_0, XlsChart A_1, XlsChartSerie A_2)
	{
		int a_ = 2;
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			int num = 6;
			XlsChartSeries xlsChartSeries;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					XlsChartFormat xlsChartFormat;
					if (xlsChartFormat.IsChartChartLine)
					{
						num = 7;
						continue;
					}
					goto IL_1FF;
				}
				case 1:
				{
					XlsChartFormat xlsChartFormat;
					if (xlsChartFormat.LineStyle != DropLineStyleType.HiLow)
					{
						goto IL_1FF;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C9;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				}
				case 2:
					goto IL_E9;
				case 3:
					goto IL_71;
				case 4:
					A_0.WriteElementString(RecordTableEnumerator.b("倷匹瀻儽㜿แⵃ⡅ⵇ㥉", a_), RecordTableEnumerator.b("倷丹䠻丽稿流歃㕅⭇≉⥋⍍ㅏ⅑穓㥕⡗㽙㉛♝ൟ๡ɣ॥ᩧݩ൫ᩭͯ山᭳ѵί啹᡻౽ꆍꊏꊑ꒓ꂕ랗蓮ﾝ튟횡", a_), string.Empty);
					num = 10;
					continue;
				case 5:
				{
					if (A_1 == null)
					{
						num = 2;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("䬷丹医崽⬿Łⱃ❅㩇㹉", a_), RecordTableEnumerator.b("倷丹䠻丽稿流歃㕅⭇≉⥋⍍ㅏ⅑穓㥕⡗㽙㉛♝ൟ๡ɣ॥ᩧݩ൫ᩭͯ山᭳ѵί啹᡻౽ꆍꊏꊑ꒓ꂕ랗蓮ﾝ튟횡", a_));
					xlsChartSeries = A_1.Series;
					int num2 = 0;
					int count = xlsChartSeries.Count;
					num = 8;
					continue;
				}
				case 7:
					num = 1;
					continue;
				case 8:
					goto IL_145;
				case 9:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 12;
						continue;
					}
					XlsChartSerie a_2 = (XlsChartSerie)xlsChartSeries[num2];
					this.ᜈ(A_0, a_2);
					num2++;
					num = 11;
					continue;
				}
				case 10:
					goto IL_1A6;
				case 11:
					goto IL_C9;
				case 12:
				{
					XlsChartFormat xlsChartFormat = A_1.PrimaryFormats[0];
					num = 0;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 3;
					continue;
				}
				num = 5;
				continue;
				IL_145:
				num = 9;
				continue;
				IL_C9:
				goto IL_145;
			}
			IL_71:
			throw new ArgumentNullException(RecordTableEnumerator.b("伷䠹唻䨽┿ぁ", a_));
			IL_E9:
			throw new ArgumentNullException(RecordTableEnumerator.b("嬷刹崻䰽㐿", a_));
			IL_1A6:
			IL_1FF:
			this.ᜀ(A_0, A_1, A_2);
			this.\u1716(A_0, A_1, A_2);
			A_0.WriteEndElement();
			return xlsChartSeries.Count;
		}
		}
	}

	// Token: 0x06005535 RID: 21813 RVA: 0x00362A70 File Offset: 0x00361A70
	private void ᜁ(XmlWriter A_0, XlsChart A_1)
	{
		int a_ = 17;
		int num = 2;
		for (;;)
		{
			IL_13:
			switch (num)
			{
			case 0:
				goto IL_8B;
			case 1:
				goto IL_34;
			case 3:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				goto IL_A1;
			}
			while (A_0 != null)
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
					num = 3;
					goto IL_13;
				}
			}
			num = 1;
		}
		IL_34:
		throw new ArgumentNullException(RecordTableEnumerator.b("う㭈≊㥌⩎⍐", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("⑆ⅈ⩊㽌㭎", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("⍆♈㹊⩌❎㽐♒⅔ᑖㅘ㩚⽜⭞", a_), RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒♔㑖ㅘ㹚ぜ㹞በ䵢੤ᝦ౨ժᕬɮᵰᕲᩴնᑸ᩺ॼ౾꾀Ꚉﾌ朗ﮔ늜궞醠鎢鎤袦쪨쎪첬\uddae얰", a_));
		spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("ㅆ⡈㥊㑌౎㹐㽒㩔╖⩘", a_), true);
		spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("⽆♈❊⡌ᱎ㡐⥒ご", a_), RecordTableEnumerator.b("牆祈", a_));
	}

	// Token: 0x06005536 RID: 21814 RVA: 0x00362B78 File Offset: 0x00361B78
	private int ᜂ(XmlWriter A_0, XlsChart A_1, XlsChartSerie A_2)
	{
		int a_ = 10;
		switch (0)
		{
		default:
		{
			int num = 0;
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
					case 1:
						goto IL_6B;
					case 2:
						if (A_1 == null)
						{
							if (true)
							{
							}
							num = 3;
							continue;
						}
						goto IL_B4;
					case 3:
						goto IL_9E;
					}
					break;
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
			IL_6B:
			throw new ArgumentNullException(RecordTableEnumerator.b("㜿ぁⵃ㉅ⵇ㡉", a_));
			IL_9E:
			throw new ArgumentNullException(RecordTableEnumerator.b("⌿⩁╃㑅㱇", a_));
			IL_B4:
			A_0.WriteStartElement(RecordTableEnumerator.b("␿ⵁㅃⅅ⁇⑉㥋㩍ፏ㩑㕓⑕ⱗ", a_), RecordTableEnumerator.b("⠿㙁ぃ㙅片敉捋㵍㍏㩑ㅓ㭕㥗⥙牛ㅝၟݡ੣ṥէ٩੫ŭɯάᕳɵ୷呹፻౽궁ﶉﾑ릕ꪗꪙ겛ꢝ辟송첣장\udaa7\udea9", a_));
			int result = this.ᜁ(A_0, A_1, A_2);
			IChartFormat options = A_2.Format.Options;
			int firstSliceAngle = options.FirstSliceAngle;
			spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("☿⭁㙃㕅㱇᥉⁋❍㍏㝑ᕓ㡕㽗", a_), firstSliceAngle.ToString());
			int doughnutHoleSize = options.DoughnutHoleSize;
			spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("⠿ⵁ⡃⍅ᭇ⍉㙋⭍", a_), doughnutHoleSize.ToString());
			A_0.WriteEndElement();
			return result;
		}
		}
	}

	// Token: 0x06005537 RID: 21815 RVA: 0x00362CBC File Offset: 0x00361CBC
	private int ᜁ(XmlWriter A_0, XlsChart A_1, XlsChartSerie A_2)
	{
		int a_ = 10;
		int num = 2;
		for (;;)
		{
			IL_1B:
			switch (num)
			{
			case 0:
				goto IL_8B;
			case 1:
				goto IL_3C;
			case 2:
				if (true)
				{
				}
				break;
			case 3:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				goto IL_A1;
			}
			while (A_0 != null)
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
					goto IL_1B;
				}
			}
			num = 1;
		}
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("㜿ぁⵃ㉅ⵇ㡉", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("⌿⩁╃㑅㱇", a_));
		IL_A1:
		this.ᜌ(A_0, A_2);
		return this.ᜀ(A_0, A_1, A_2, new spr\u2541.ᜀ(this.ᜊ));
	}

	// Token: 0x06005538 RID: 21816 RVA: 0x00362D88 File Offset: 0x00361D88
	private void ᜀ(XmlWriter A_0, IChartDataLabels A_1, XlsChartSerie A_2)
	{
		int a_ = 10;
		switch (0)
		{
		default:
		{
			int num = 0;
			XlsChart parentChart;
			for (;;)
			{
				XlsChartDataPointsCollection xlsChartDataPointsCollection;
				switch (num)
				{
				case 1:
					goto IL_284;
				case 2:
					try
					{
						num = 8;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								IEnumerator enumerator;
								if (!enumerator.MoveNext())
								{
									num = 6;
									continue;
								}
								XlsChartDataPoint xlsChartDataPoint = (XlsChartDataPoint)enumerator.Current;
								goto IL_D0;
							}
							case 1:
								num = 7;
								continue;
							case 2:
								goto IL_15B;
							case 3:
							{
								XlsChartDataPoint xlsChartDataPoint;
								this.ᜀ(A_0, xlsChartDataPoint.DataLabels, xlsChartDataPoint.Index, parentChart);
								num = 5;
								continue;
							}
							case 4:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_D0;
								default:
								{
									if (false)
									{
									}
									XlsChartDataPoint xlsChartDataPoint;
									if (!xlsChartDataPoint.IsDefault)
									{
										num = 1;
										continue;
									}
									break;
								}
								}
								break;
							case 6:
								num = 2;
								continue;
							case 7:
							{
								XlsChartDataPoint xlsChartDataPoint;
								if (xlsChartDataPoint.HasDataLabels)
								{
									num = 3;
									continue;
								}
								break;
							}
							}
							goto IL_A3;
							IL_D0:
							num = 4;
							continue;
							IL_12D:
							num = 0;
							continue;
							IL_A3:
							goto IL_12D;
						}
						IL_15B:
						goto IL_223;
					}
					finally
					{
						for (;;)
						{
							IEnumerator enumerator;
							IDisposable disposable = enumerator as IDisposable;
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 0:
									if (disposable != null)
									{
										num = 2;
										continue;
									}
									goto IL_1A7;
								case 1:
									goto IL_1A5;
								case 2:
									disposable.Dispose();
									num = 1;
									continue;
								}
								break;
							}
						}
						IL_1A5:
						IL_1A7:;
					}
					goto IL_1A8;
				case 3:
					goto IL_2F6;
				case 4:
					if (xlsChartDataPointsCollection.DeninedDPCount > 0)
					{
						num = 6;
						continue;
					}
					goto IL_223;
				case 5:
					goto IL_68;
				case 6:
				{
					IEnumerator enumerator = xlsChartDataPointsCollection.GetEnumerator();
					num = 2;
					continue;
				}
				case 7:
					if (A_1 == null)
					{
						num = 9;
						continue;
					}
					num = 8;
					continue;
				case 8:
					if (A_2 == null)
					{
						num = 3;
						continue;
					}
					goto IL_1A8;
				case 9:
					goto IL_221;
				case 10:
					if ((A_2.DataPoints.DefaultDataPoint.DataLabels as XlsChartDataLabels).NumberFormat != null)
					{
						num = 11;
						continue;
					}
					goto IL_2F8;
				case 11:
					this.\u170D(A_0, A_2);
					num = 1;
					continue;
				}
				if (A_0 == null)
				{
					num = 5;
					continue;
				}
				num = 7;
				continue;
				IL_1A8:
				A_0.WriteStartElement(RecordTableEnumerator.b("␿แ♃⩅㭇", a_), RecordTableEnumerator.b("⠿㙁ぃ㙅片敉捋㵍㍏㩑ㅓ㭕㥗⥙牛ㅝၟݡ੣ṥէ٩੫ŭɯάᕳɵ୷呹፻౽궁ﶉﾑ릕ꪗꪙ겛ꢝ辟송첣장\udaa7\udea9", a_));
				parentChart = A_2.ParentChart;
				xlsChartDataPointsCollection = (XlsChartDataPointsCollection)A_2.DataPoints;
				num = 4;
				continue;
				IL_223:
				num = 10;
			}
			IL_68:
			throw new ArgumentNullException(RecordTableEnumerator.b("㜿ぁⵃ㉅ⵇ㡉", a_));
			IL_221:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("␿⍁ぃ❅ч⭉⹋⭍㱏⅑", a_));
			IL_284:
			goto IL_2F8;
			IL_2F6:
			throw new ArgumentNullException(RecordTableEnumerator.b("㌿❁㙃⽅ⵇ㥉", a_));
			IL_2F8:
			this.ᜀ(A_0, A_1, parentChart);
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06005539 RID: 21817 RVA: 0x003630AC File Offset: 0x003620AC
	private void \u170D(XmlWriter A_0, XlsChartSerie A_1)
	{
		int a_ = 18;
		int num = 0;
		for (;;)
		{
			IL_13:
			switch (num)
			{
			case 1:
				goto IL_8B;
			case 2:
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				goto IL_A1;
			case 3:
				goto IL_34;
			}
			while (A_0 != null)
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
					goto IL_13;
				}
			}
			num = 3;
		}
		IL_34:
		throw new ArgumentNullException(RecordTableEnumerator.b("㽇㡉╋㩍㕏⁑", a_));
		IL_8B:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㭇⽉㹋❍㕏⅑", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("♇㽉⅋ࡍ㵏♑", a_), RecordTableEnumerator.b("⁇㹉㡋㹍橏絑筓╕㭗㉙㥛㍝şᅡ䩣॥ᡧཀྵɫ᙭ᵯṱታ᥵੷᝹ᵻ੽겁ꖉﲍﶓﾗ놝銟銡钣邥螧즩쒫쾭슯욱", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("⹇╉㹋⍍ㅏ♑ᝓ㥕㱗㽙", a_), (A_1.DataPoints.DefaultDataPoint.DataLabels as XlsChartDataLabels).NumberFormat);
		A_0.WriteEndElement();
	}

	// Token: 0x0600553A RID: 21818 RVA: 0x003631B0 File Offset: 0x003621B0
	private void ᜀ(XmlWriter A_0, IChartDataLabels A_1, int A_2, XlsChart A_3)
	{
		int a_ = 8;
		int num = 8;
		for (;;)
		{
			sprᮟ sprᮟ;
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					goto IL_F4;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("娽ి⁁⡃", a_), RecordTableEnumerator.b("嘽㐿㙁㑃籅杇敉㽋ⵍ㡏㝑㥓㝕⭗瑙㍛⹝՟ౡᱣ୥ѧ౩ͫᱭᵯ፱sյ噷ᕹ๻᥽꽿ﾇﶏﺑ뮓꒕ꢗꪙꪛ놝쎟쪡얣풥\udca7", a_));
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("圽␿㩁", a_), A_2.ToString());
				num = 2;
				continue;
			case 1:
				goto IL_16B;
			case 2:
				if ((A_1 as XlsChartDataLabels).IsDelete)
				{
					num = 3;
					continue;
				}
				goto IL_16B;
			case 3:
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("娽┿⹁⅃㉅ⵇ", a_), true);
				num = 1;
				continue;
			case 4:
				goto IL_FF;
			case 5:
				if (!string.IsNullOrEmpty(sprᮟ.Text))
				{
					num = 6;
					continue;
				}
				goto IL_1AE;
			case 6:
			{
				XlsWorkbook parentWorkbook = A_3.ParentWorkbook;
				spr\u1CFF.ᜁ(A_0, sprᮟ);
				spr\u1CFF.ᜂ(A_0, sprᮟ, parentWorkbook, 10.0);
				num = 7;
				continue;
			}
			case 7:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_F4;
				default:
					goto IL_14F;
				}
				break;
			case 8:
				if (true)
				{
				}
				break;
			case 9:
				goto IL_57;
			}
			if (A_0 == null)
			{
				num = 9;
				continue;
			}
			num = 0;
			continue;
			IL_F4:
			num = 4;
			continue;
			IL_16B:
			sprᮟ = (A_1 as sprᮟ);
			num = 5;
		}
		IL_57:
		throw new ArgumentNullException(RecordTableEnumerator.b("䤽㈿⭁ぃ⍅㩇", a_));
		IL_FF:
		throw new ArgumentNullException(RecordTableEnumerator.b("娽ℿ㙁╃੅⥇⡉⥋≍⍏", a_));
		IL_14F:
		if (false)
		{
		}
		IL_1AE:
		this.ᜀ(A_0, A_1, A_3);
		A_0.WriteEndElement();
	}

	// Token: 0x0600553B RID: 21819 RVA: 0x0036337C File Offset: 0x0036237C
	private void ᜀ(XmlWriter A_0, IChartDataLabels A_1, XlsChart A_2)
	{
		int a_ = 1;
		switch (0)
		{
		default:
		{
			int num = 36;
			for (;;)
			{
				DataLabelPositionType position;
				string delimiter;
				switch (num)
				{
				case 0:
					goto IL_42E;
				case 1:
					num = 14;
					continue;
				case 2:
					goto IL_480;
				case 3:
					goto IL_18D;
				case 4:
					if (!A_1.HasCategoryName)
					{
						num = 1;
						continue;
					}
					goto IL_655;
				case 5:
					num = 17;
					continue;
				case 6:
					if (A_2.DestinationType == ExcelChartType.ScatterLineMarkers)
					{
						num = 39;
						continue;
					}
					goto IL_3DE;
				case 7:
					num = 56;
					continue;
				case 8:
				{
					XLSXDataLabelPos xlsxdataLabelPos = (XLSXDataLabelPos)position;
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("匶甸夺儼漾⹀あ", a_), xlsxdataLabelPos.ToString());
					num = 3;
					continue;
				}
				case 9:
					if (position != DataLabelPositionType.Automatic)
					{
						num = 8;
						continue;
					}
					goto IL_18D;
				case 10:
					if (A_2.DestinationType == ExcelChartType.ScatterLineMarkers)
					{
						num = 21;
						continue;
					}
					goto IL_741;
				case 11:
					goto IL_268;
				case 12:
					num = 16;
					continue;
				case 13:
					if (A_2.DestinationType == ExcelChartType.ScatterLineMarkers)
					{
						num = 41;
						continue;
					}
					goto IL_480;
				case 14:
					if (A_2.DestinationType == ExcelChartType.ScatterLineMarkers)
					{
						num = 27;
						continue;
					}
					goto IL_268;
				case 15:
					goto IL_3B3;
				case 16:
					if (!A_1.HasLegendKey)
					{
						num = 43;
						continue;
					}
					goto IL_3B3;
				case 17:
					if (!A_1.HasPercentage)
					{
						num = 46;
						continue;
					}
					goto IL_5BC;
				case 18:
					goto IL_38C;
				case 19:
					if (A_2.IsChartPie)
					{
						num = 7;
						continue;
					}
					goto IL_6D1;
				case 20:
					num = 50;
					continue;
				case 21:
					goto IL_20D;
				case 22:
					num = 4;
					continue;
				case 23:
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("䐶儸吺䨼猾⑀≂⅄≆㭈݊⑌ⅎ㑐⁒", a_), A_2.XlsChartFormat.IsShowLeaderLines);
					num = 25;
					continue;
				case 24:
					num = 31;
					continue;
				case 25:
					goto IL_6D1;
				case 26:
					if (!(A_1 as XlsChartDataLabels).ᜉ)
					{
						num = 24;
						continue;
					}
					goto IL_31B;
				case 27:
					goto IL_655;
				case 28:
					if (A_2.XlsChartFormat.IsShowLeaderLines)
					{
						num = 23;
						continue;
					}
					goto IL_6D1;
				case 29:
					num = 48;
					continue;
				case 30:
					if (!(A_1 as XlsChartDataLabels).ᜋ)
					{
						num = 22;
						continue;
					}
					goto IL_655;
				case 31:
					if (!A_1.HasValue)
					{
						num = 29;
						continue;
					}
					goto IL_31B;
				case 32:
					goto IL_136;
				case 33:
					num = 28;
					continue;
				case 34:
					if (!(A_1 as XlsChartDataLabels).ᜊ)
					{
						num = 20;
						continue;
					}
					goto IL_1BA;
				case 35:
					goto IL_741;
				case 37:
				{
					if (A_1 == null)
					{
						num = 45;
						continue;
					}
					spr\u1CFF.ᜀ(A_0, (A_1 as XlsChartDataLabels).FrameFormat, A_2, false);
					bool flag = ((XlsChartDataLabels)A_1).ParagraphType == ChartParagraphType.Default;
					num = 60;
					continue;
				}
				case 38:
					if (delimiter != null)
					{
						num = 49;
						continue;
					}
					return;
				case 39:
					goto IL_1BA;
				case 40:
					if (!(A_1 as XlsChartDataLabels).ᜎ)
					{
						num = 55;
						continue;
					}
					goto IL_20D;
				case 41:
					goto IL_5BC;
				case 42:
					num = 10;
					continue;
				case 43:
					num = 58;
					continue;
				case 44:
					goto IL_4AD;
				case 45:
					goto IL_602;
				case 46:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6FB;
					default:
						if (false)
						{
						}
						num = 13;
						continue;
					}
					break;
				case 47:
					if (!(A_1 as XlsChartDataLabels).\u170D)
					{
						num = 12;
						continue;
					}
					goto IL_3B3;
				case 48:
					if (A_2.DestinationType == ExcelChartType.ScatterLineMarkers)
					{
						num = 59;
						continue;
					}
					goto IL_42E;
				case 49:
					A_0.WriteElementString(RecordTableEnumerator.b("䐶尸䬺尼䴾⁀㝂⩄㕆", a_), RecordTableEnumerator.b("弶䴸伺䴼Ծ湀求㙄⑆ⅈ⹊⁌⹎≐絒㩔❖㱘㕚╜㉞ൠբ੤ᕦѨ੪ᥬᱮ彰ᱲݴၶ噸ὺོṾꊌ붎ꆐꎒꎔ뢖滛ﲜ햠", a_), delimiter);
					num = 52;
					continue;
				case 50:
					if (!A_1.HasSeriesName)
					{
						num = 53;
						continue;
					}
					goto IL_1BA;
				case 51:
					if (!A_1.HasBubbleSize)
					{
						num = 42;
						continue;
					}
					goto IL_20D;
				case 52:
					goto IL_316;
				case 53:
					num = 6;
					continue;
				case 54:
					if (!(A_1 as XlsChartDataLabels).ᜌ)
					{
						num = 5;
						continue;
					}
					goto IL_5BC;
				case 55:
					num = 51;
					continue;
				case 56:
					if (A_2.ChartType != ExcelChartType.PieOfPie)
					{
						num = 33;
						continue;
					}
					goto IL_6D1;
				case 57:
					goto IL_3DE;
				case 58:
					if (A_2.DestinationType == ExcelChartType.ScatterLineMarkers)
					{
						num = 15;
						continue;
					}
					goto IL_4AD;
				case 59:
					goto IL_31B;
				case 60:
				{
					bool flag;
					if (flag)
					{
						num = 61;
						continue;
					}
					goto IL_38C;
				}
				case 61:
					this.ᜀ(A_0, A_1, A_2.Workbook, 10.0);
					num = 18;
					continue;
				}
				if (A_0 == null)
				{
					num = 32;
					continue;
				}
				num = 37;
				continue;
				IL_18D:
				num = 47;
				continue;
				IL_1BA:
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("䐶儸吺䨼氾⑀ㅂୄ♆⑈⹊", a_), A_1.HasSeriesName);
				num = 57;
				continue;
				IL_20D:
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("䐶儸吺䨼紾㑀⅂❄⭆ⱈᡊ⑌㕎㑐", a_), A_1.HasBubbleSize);
				num = 35;
				continue;
				IL_268:
				num = 54;
				continue;
				IL_31B:
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("䐶儸吺䨼椾⁀⽂", a_), A_1.HasValue);
				num = 0;
				continue;
				IL_38C:
				position = A_1.Position;
				num = 9;
				continue;
				IL_3B3:
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("䐶儸吺䨼猾⑀⑂⁄⥆ⵈJ⡌㙎", a_), A_1.HasLegendKey);
				num = 44;
				continue;
				IL_3DE:
				num = 19;
				continue;
				IL_42E:
				num = 30;
				continue;
				IL_480:
				num = 40;
				continue;
				IL_4AD:
				num = 26;
				continue;
				IL_5BC:
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("䐶儸吺䨼漾⑀ㅂ♄≆❈㽊", a_), A_1.HasPercentage);
				num = 2;
				continue;
				IL_655:
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("䐶儸吺䨼簾⁀㝂ୄ♆⑈⹊", a_), A_1.HasCategoryName);
				num = 11;
				continue;
				IL_6D1:
				delimiter = A_1.Delimiter;
				num = 38;
				continue;
				IL_741:
				num = 34;
			}
			IL_136:
			throw new ArgumentNullException(RecordTableEnumerator.b("䀶䬸刺䤼娾㍀", a_));
			IL_316:
			return;
			IL_602:
			IL_6FB:
			throw new ArgumentNullException(RecordTableEnumerator.b("匶堸伺尼猾⁀⅂⁄⭆㩈", a_));
		}
		}
	}

	// Token: 0x0600553C RID: 21820 RVA: 0x00363AF8 File Offset: 0x00362AF8
	private void ᜀ(XmlWriter A_0, IChartTextArea A_1, IWorkbook A_2, double A_3)
	{
		int a_ = 9;
		for (;;)
		{
			int num = 5;
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
					goto IL_C5;
				case 1:
					goto IL_C3;
				case 2:
					goto IL_5B;
				case 3:
					if (A_2 == null)
					{
						num = 1;
						continue;
					}
					num = 0;
					continue;
				case 4:
					goto IL_46;
				}
				if (A_0 == null)
				{
					num = 4;
				}
				else
				{
					num = 3;
				}
			}
			IL_C3:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_73;
			}
		}
		IL_46:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠾㍀⩂ㅄ≆㭈", a_));
		IL_5B:
		if (true)
		{
		}
		return;
		IL_73:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("崾⹀ⱂ⹄", a_));
		IL_C5:
		A_0.WriteStartElement(RecordTableEnumerator.b("䬾㥀ፂ㝄", a_), RecordTableEnumerator.b("圾㕀㝂㕄絆晈摊㹌ⱎ㥐㙒㡔㙖⩘畚㉜⽞Ѡൢᵤ੦ը൪ɬᵮᱰቲŴѶ坸ᑺོ᡾꺀ﺈﲐﾒ몔ꖖꦘꮚꮜ낞슠쮢쒤햦\udda8", a_));
		A_0.WriteStartElement(RecordTableEnumerator.b("崾⹀❂㱄ᝆ㭈", a_), RecordTableEnumerator.b("圾㕀㝂㕄絆晈摊㹌ⱎ㥐㙒㡔㙖⩘畚㉜⽞Ѡൢᵤ੦ը൪ɬᵮᱰቲŴѶ坸ᑺོ᡾꺀ﺈﲐﾒ몔ꖖꦘꮚꮜ낞철슢첤즦", a_));
		A_0.WriteEndElement();
		A_0.WriteStartElement(RecordTableEnumerator.b("匾㉀㝂ᙄ㍆え❊⡌", a_), RecordTableEnumerator.b("圾㕀㝂㕄絆晈摊㹌ⱎ㥐㙒㡔㙖⩘畚㉜⽞Ѡൢᵤ੦ը൪ɬᵮᱰቲŴѶ坸ᑺོ᡾꺀ﺈﲐﾒ몔ꖖꦘꮚꮜ낞철슢첤즦", a_));
		A_0.WriteEndElement();
		A_0.WriteStartElement(RecordTableEnumerator.b("伾", a_), RecordTableEnumerator.b("圾㕀㝂㕄絆晈摊㹌ⱎ㥐㙒㡔㙖⩘畚㉜⽞Ѡൢᵤ੦ը൪ɬᵮᱰቲŴѶ坸ᑺོ᡾꺀ﺈﲐﾒ몔ꖖꦘꮚꮜ낞철슢첤즦", a_));
		A_0.WriteStartElement(RecordTableEnumerator.b("伾ᅀㅂ", a_), RecordTableEnumerator.b("圾㕀㝂㕄絆晈摊㹌ⱎ㥐㙒㡔㙖⩘畚㉜⽞Ѡൢᵤ੦ը൪ɬᵮᱰቲŴѶ坸ᑺོ᡾꺀ﺈﲐﾒ몔ꖖꦘꮚꮜ낞철슢첤즦", a_));
		spr\u1CFF.ᜀ(A_0, A_1, RecordTableEnumerator.b("嬾⑀╂ᝄᝆ㭈", a_), A_2, A_3);
		A_0.WriteEndElement();
		A_0.WriteEndElement();
		A_0.WriteEndElement();
	}

	// Token: 0x0600553D RID: 21821 RVA: 0x00363CAC File Offset: 0x00362CAC
	private void ᜌ(XmlWriter A_0, XlsChartSerie A_1)
	{
		int a_ = 4;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_62;
			case 1:
				if (A_1 == null)
				{
					goto IL_83;
				}
				goto IL_A1;
			case 3:
				goto IL_8B;
			}
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_83:
				num = 3;
				break;
			default:
				if (false)
				{
				}
				if (A_0 == null)
				{
					num = 0;
				}
				else
				{
					num = 1;
				}
				break;
			}
		}
		IL_62:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴹主圽㐿❁㙃", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("尹唻䰽㌿㙁ᝃ⍅㩇⍉⥋㵍", a_));
		IL_A1:
		bool isVaryColor = A_1.Format.Options.IsVaryColor;
		spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("䰹崻䰽㤿Ł⭃⩅❇㡉㽋", a_), isVaryColor);
	}

	// Token: 0x0600553E RID: 21822 RVA: 0x00363D80 File Offset: 0x00362D80
	private void ᜋ(XmlWriter A_0, XlsChartSerie A_1)
	{
		int a_ = 16;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if ((A_1.DataPoints.DefaultDataPoint as XlsChartDataPoint).HasDataLabels)
				{
					num = 1;
					continue;
				}
				goto IL_115;
			case 1:
				if (true)
				{
				}
				this.ᜀ(A_0, A_1.DataPoints.DefaultDataPoint.DataLabels, A_1);
				num = 6;
				continue;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					if (A_1 == null)
					{
						num = 4;
						continue;
					}
					this.ᜃ(A_0, A_1);
					num = 0;
					continue;
				}
				break;
			case 4:
				goto IL_110;
			case 5:
				goto IL_43;
			case 6:
				goto IL_D6;
			}
			IL_35:
			if (A_0 == null)
			{
				num = 5;
				continue;
			}
			num = 2;
			continue;
			goto IL_35;
		}
		IL_43:
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅅ㩇⍉㡋⭍≏", a_));
		IL_D6:
		goto IL_115;
		IL_110:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕅ⵇ㡉╋⭍⍏", a_));
		IL_115:
		this.ᜀ(A_0, A_1.TrendLines, A_1.ParentBook);
		this.ᜉ(A_0, A_1);
		this.ᜂ(A_0, A_1);
		this.ᜁ(A_0, A_1);
		A_0.WriteEndElement();
	}

	// Token: 0x0600553F RID: 21823 RVA: 0x00363ED4 File Offset: 0x00362ED4
	private void ᜊ(XmlWriter A_0, XlsChartSerie A_1)
	{
		int a_ = 16;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_56;
			case 1:
				this.ᜀ(A_0, A_1.DataPoints.DefaultDataPoint.DataLabels, A_1);
				num = 9;
				continue;
			case 2:
				goto IL_12D;
			case 3:
			{
				int percent;
				if (percent != 0)
				{
					num = 7;
					continue;
				}
				goto IL_12D;
			}
			case 4:
				if ((A_1.DataPoints.DefaultDataPoint as XlsChartDataPoint).HasDataLabels)
				{
					num = 1;
					continue;
				}
				goto IL_170;
			case 6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_97;
				default:
					goto IL_E4;
				}
				break;
			case 7:
			{
				int percent;
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("⍅ぇ㩉⁋⅍⍏㭑㭓㡕", a_), percent.ToString());
				num = 2;
				continue;
			}
			case 8:
			{
				if (A_1 == null)
				{
					num = 6;
					continue;
				}
				this.ᜃ(A_0, A_1);
				int percent = A_1.Format.Percent;
				goto IL_97;
			}
			case 9:
				goto IL_10F;
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			num = 8;
			continue;
			IL_97:
			num = 3;
			continue;
			IL_12D:
			num = 4;
		}
		IL_56:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅅ㩇⍉㡋⭍≏", a_));
		IL_E4:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㕅ⵇ㡉╋⭍⍏", a_));
		IL_10F:
		IL_170:
		this.ᜀ(A_0, A_1.TrendLines, A_1.ParentBook);
		this.ᜉ(A_0, A_1);
		this.ᜂ(A_0, A_1);
		this.ᜁ(A_0, A_1);
		A_0.WriteEndElement();
	}

	// Token: 0x06005540 RID: 21824 RVA: 0x00364084 File Offset: 0x00363084
	private void ᜉ(XmlWriter A_0, XlsChartSerie A_1)
	{
		int a_ = 11;
		if (true)
		{
		}
		int num = 7;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				if (A_1 == null)
				{
					num = 9;
					continue;
				}
				IWorkbook a_2 = A_1.ParentBook;
				goto IL_94;
			}
			case 1:
				if (A_1.HasErrorBarsX)
				{
					num = 8;
					continue;
				}
				goto IL_12C;
			case 2:
				goto IL_12C;
			case 3:
				goto IL_5E;
			case 4:
			{
				IWorkbook a_2;
				this.ᜀ(A_0, A_1.ErrorBarsY, RecordTableEnumerator.b("㡀", a_), a_2, A_1);
				num = 6;
				continue;
			}
			case 5:
				if (A_1.HasErrorBarsY)
				{
					num = 4;
					continue;
				}
				return;
			case 6:
				return;
			case 8:
			{
				IWorkbook a_2;
				this.ᜀ(A_0, A_1.ErrorBarsX, RecordTableEnumerator.b("㥀", a_), a_2, A_1);
				num = 2;
				continue;
			}
			case 9:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_94;
				default:
					goto IL_E6;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			num = 0;
			continue;
			IL_94:
			num = 1;
			continue;
			IL_12C:
			num = 5;
		}
		IL_5E:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙀ㅂⱄ㍆ⱈ㥊", a_));
		IL_E6:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㉀♂㝄⹆ⱈ㡊", a_));
	}

	// Token: 0x06005541 RID: 21825 RVA: 0x003641F4 File Offset: 0x003631F4
	private void ᜈ(XmlWriter A_0, XlsChartSerie A_1)
	{
		int a_ = 7;
		int num = 1;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				goto IL_D6;
			case 2:
				goto IL_4B;
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
					if (A_1 == null)
					{
						num = 6;
						continue;
					}
					this.ᜃ(A_0, A_1);
					num = 4;
					continue;
				}
				break;
			case 4:
				if ((A_1.DataPoints.DefaultDataPoint as XlsChartDataPoint).HasDataLabels)
				{
					num = 5;
					continue;
				}
				goto IL_115;
			case 5:
				this.ᜀ(A_0, A_1.DataPoints.DefaultDataPoint.DataLabels, A_1);
				num = 0;
				continue;
			case 6:
				goto IL_110;
			}
			IL_3D:
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			num = 3;
			continue;
			goto IL_3D;
		}
		IL_4B:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨼䴾⡀㝂⁄㕆", a_));
		IL_D6:
		goto IL_115;
		IL_110:
		throw new ArgumentNullException(RecordTableEnumerator.b("丼娾㍀⩂⁄㑆", a_));
		IL_115:
		this.ᜀ(A_0, A_1.TrendLines, A_1.ParentBook);
		this.ᜉ(A_0, A_1);
		this.ᜂ(A_0, A_1);
		this.ᜁ(A_0, A_1);
		XlsChartSerieDataFormat xlsChartSerieDataFormat = (XlsChartSerieDataFormat)A_1.Format;
		spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("丼刾⹀ⱂㅄ⽆", a_), xlsChartSerieDataFormat.IsSmoothed);
		A_0.WriteEndElement();
	}

	// Token: 0x06005542 RID: 21826 RVA: 0x00364370 File Offset: 0x00363370
	private void ᜇ(XmlWriter A_0, XlsChartSerie A_1)
	{
		int a_ = 12;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					goto IL_7B;
				}
				goto IL_99;
			case 1:
				goto IL_5A;
			case 3:
				goto IL_83;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_7B:
				num = 3;
				break;
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
					num = 0;
				}
				break;
			}
		}
		IL_5A:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕁㙃⽅㱇⽉㹋", a_));
		IL_83:
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅁ⅃㑅ⅇ⽉㽋", a_));
		IL_99:
		if (true)
		{
		}
		this.ᜃ(A_0, A_1);
		this.ᜀ(A_0, A_1.DataPoints.DefaultDataPoint.DataLabels, A_1);
		this.ᜀ(A_0, A_1.TrendLines, A_1.ParentBook);
		this.ᜉ(A_0, A_1);
		this.ᜁ(A_0, A_1, RecordTableEnumerator.b("㩁ቃ❅⑇", a_));
		this.ᜀ(A_0, A_1, RecordTableEnumerator.b("㭁ቃ❅⑇", a_));
		XlsChartSerieDataFormat xlsChartSerieDataFormat = (XlsChartSerieDataFormat)A_1.Format;
		spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("ㅁ⥃⥅❇㹉⑋", a_), true);
		A_0.WriteEndElement();
	}

	// Token: 0x06005543 RID: 21827 RVA: 0x003644AC File Offset: 0x003634AC
	private void ᜆ(XmlWriter A_0, XlsChartSerie A_1)
	{
		int a_ = 12;
		int num = 2;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				goto IL_110;
			case 1:
				goto IL_D6;
			case 3:
				if ((A_1.DataPoints.DefaultDataPoint as XlsChartDataPoint).HasDataLabels)
				{
					num = 4;
					continue;
				}
				goto IL_115;
			case 4:
				this.ᜀ(A_0, A_1.DataPoints.DefaultDataPoint.DataLabels, A_1);
				num = 1;
				continue;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					if (A_1 == null)
					{
						num = 0;
						continue;
					}
					this.ᜃ(A_0, A_1);
					num = 3;
					continue;
				}
				break;
			case 6:
				goto IL_4B;
			}
			IL_3D:
			if (A_0 == null)
			{
				num = 6;
				continue;
			}
			num = 5;
			continue;
			goto IL_3D;
		}
		IL_4B:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕁㙃⽅㱇⽉㹋", a_));
		IL_D6:
		goto IL_115;
		IL_110:
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅁ⅃㑅ⅇ⽉㽋", a_));
		IL_115:
		this.ᜂ(A_0, A_1);
		this.ᜁ(A_0, A_1);
		A_0.WriteEndElement();
	}

	// Token: 0x06005544 RID: 21828 RVA: 0x003645E4 File Offset: 0x003635E4
	private void ᜅ(XmlWriter A_0, XlsChartSerie A_1)
	{
		int a_ = 4;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_4C;
			case 1:
				if (A_1 == null)
				{
					num = 8;
					continue;
				}
				this.ᜃ(A_0, A_1);
				num = 3;
				continue;
			case 3:
				if ((A_1.DataPoints.DefaultDataPoint as XlsChartDataPoint).HasDataLabels)
				{
					num = 6;
					continue;
				}
				goto IL_13F;
			case 4:
				goto IL_13F;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("堹䤻尽∿⹁⅃畅ే", a_), RecordTableEnumerator.b("ହ", a_));
					num = 7;
					continue;
				}
				break;
			case 6:
				this.ᜀ(A_0, A_1.DataPoints.DefaultDataPoint.DataLabels, A_1);
				if (true)
				{
				}
				num = 4;
				continue;
			case 7:
				goto IL_11C;
			case 8:
				goto IL_CE;
			case 9:
				if (A_1.SerieType == ExcelChartType.Bubble3D)
				{
					num = 5;
					continue;
				}
				goto IL_1E1;
			}
			IL_41:
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			num = 1;
			continue;
			goto IL_41;
			IL_13F:
			this.ᜀ(A_0, A_1.TrendLines, A_1.ParentBook);
			this.ᜉ(A_0, A_1);
			this.ᜁ(A_0, A_1, RecordTableEnumerator.b("䈹樻弽ⰿ", a_));
			this.ᜀ(A_0, A_1, RecordTableEnumerator.b("䌹樻弽ⰿ", a_));
			this.ᜁ(A_0, A_1.Bubbles, A_1.EnteredDirectlyBubbles, RecordTableEnumerator.b("堹䤻尽∿⹁⅃ᕅⅇぉ⥋", a_), A_1);
			num = 9;
		}
		IL_4C:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴹主圽㐿❁㙃", a_));
		IL_CE:
		throw new ArgumentNullException(RecordTableEnumerator.b("䤹夻䰽⤿❁㝃", a_));
		IL_11C:
		IL_1E1:
		A_0.WriteEndElement();
	}

	// Token: 0x06005545 RID: 21829 RVA: 0x003647D8 File Offset: 0x003637D8
	private void ᜄ(XmlWriter A_0, XlsChartSerie A_1)
	{
		int a_ = 6;
		for (;;)
		{
			IL_09:
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_113;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						if ((A_1.DataPoints.DefaultDataPoint as XlsChartDataPoint).HasDataLabels)
						{
							num = 5;
							continue;
						}
						goto IL_115;
					}
					break;
				case 2:
					goto IL_43;
				case 4:
					goto IL_F5;
				case 5:
					this.ᜀ(A_0, A_1.DataPoints.DefaultDataPoint.DataLabels, A_1);
					num = 4;
					continue;
				case 6:
					if (A_1 == null)
					{
						num = 0;
						continue;
					}
					this.ᜃ(A_0, A_1);
					num = 1;
					continue;
				}
				if (A_0 == null)
				{
					num = 2;
				}
				else
				{
					num = 6;
				}
			}
		}
		IL_43:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬻䰽⤿㙁⅃㑅", a_));
		IL_F5:
		goto IL_115;
		IL_113:
		throw new ArgumentNullException(RecordTableEnumerator.b("伻嬽㈿⭁⅃㕅", a_));
		IL_115:
		this.ᜀ(A_0, A_1.TrendLines, A_1.ParentBook);
		this.ᜉ(A_0, A_1);
		this.ᜂ(A_0, A_1);
		this.ᜁ(A_0, A_1);
		A_0.WriteEndElement();
	}

	// Token: 0x06005546 RID: 21830 RVA: 0x0036492C File Offset: 0x0036392C
	private void ᜃ(XmlWriter A_0, XlsChartSerie A_1)
	{
		int a_ = 4;
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				string text;
				bool flag;
				XlsChartDataPointsCollection xlsChartDataPointsCollection;
				XlsChartSerieDataFormat dataFormatOrNull;
				string nameOrFormula;
				switch (num)
				{
				case 1:
					text = RecordTableEnumerator.b("ਹ", a_);
					goto IL_499;
				case 2:
					goto IL_50E;
				case 3:
					goto IL_B4;
				case 4:
					goto IL_14E;
				case 5:
				{
					bool? invertNegaColor;
					if (invertNegaColor != null)
					{
						num = 23;
						continue;
					}
					goto IL_2EB;
				}
				case 6:
					goto IL_AF;
				case 7:
				{
					if (A_1 == null)
					{
						num = 9;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("䤹夻䰽", a_), RecordTableEnumerator.b("刹䠻䨽〿硁歃楅㭇⥉⑋⭍㵏㍑❓硕㝗⩙㥛そᡟཡࡣeݧᡩū཭ѯű婳᥵੷ᵹ卻᩽뾏ꂑ꒓ꚕ꺗떙ﾛ솟킡킣", a_));
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("匹堻䘽", a_), A_1.Number.ToString());
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("唹主娽┿ぁ", a_), A_1.Index.ToString());
					bool? invertNegaColor = A_1.InvertNegaColor;
					num = 5;
					continue;
				}
				case 8:
					try
					{
						num = 6;
						for (;;)
						{
							XlsChartDataPoint xlsChartDataPoint;
							switch (num)
							{
							case 0:
								if (!xlsChartDataPoint.IsDefault)
								{
									num = 5;
									continue;
								}
								break;
							case 2:
								goto IL_29D;
							case 3:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_25F;
								default:
									if (false)
									{
									}
									num = 2;
									continue;
								}
								break;
							case 4:
							{
								IEnumerator enumerator;
								if (!enumerator.MoveNext())
								{
									num = 3;
									continue;
								}
								xlsChartDataPoint = (XlsChartDataPoint)enumerator.Current;
								num = 0;
								continue;
							}
							case 5:
								goto IL_25F;
							}
							IL_219:
							num = 4;
							continue;
							goto IL_219;
							IL_25F:
							this.ᜀ(A_0, xlsChartDataPoint);
							num = 1;
						}
						IL_29D:
						return;
					}
					finally
					{
						for (;;)
						{
							IEnumerator enumerator;
							IDisposable disposable = enumerator as IDisposable;
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 0:
									if (disposable != null)
									{
										num = 1;
										continue;
									}
									goto IL_2EA;
								case 1:
									disposable.Dispose();
									num = 2;
									continue;
								case 2:
									goto IL_2E8;
								}
								break;
							}
						}
						IL_2E8:
						IL_2EA:;
					}
					goto IL_2EB;
				case 9:
					goto IL_509;
				case 10:
				{
					bool? invertNegaColor2;
					flag = (invertNegaColor2 != null);
					goto IL_430;
				}
				case 11:
					goto IL_B4;
				case 12:
					if (xlsChartDataPointsCollection.DeninedDPCount > 0)
					{
						num = 27;
						continue;
					}
					return;
				case 13:
					if (dataFormatOrNull != null)
					{
						num = 19;
						continue;
					}
					goto IL_14E;
				case 14:
					if (nameOrFormula.Length > 0)
					{
						num = 28;
						continue;
					}
					goto IL_1A9;
				case 15:
					if (nameOrFormula[0] == '=')
					{
						num = 22;
						continue;
					}
					goto IL_1A9;
				case 16:
				{
					bool? invertNegaColor2;
					if (invertNegaColor2.GetValueOrDefault())
					{
						num = 25;
						continue;
					}
					num = 26;
					continue;
				}
				case 17:
					nameOrFormula = A_1.NameOrFormula;
					A_0.WriteStartElement(RecordTableEnumerator.b("丹䐻", a_), RecordTableEnumerator.b("刹䠻䨽〿硁歃楅㭇⥉⑋⭍㵏㍑❓硕㝗⩙㥛そᡟཡࡣeݧᡩū཭ѯű婳᥵੷ᵹ卻᩽뾏ꂑ꒓ꚕ꺗떙ﾛ솟킡킣", a_));
					num = 14;
					continue;
				case 18:
					goto IL_2EB;
				case 19:
					spr\u1CFF.ᜀ(A_0, dataFormatOrNull, A_1.ParentChart, false, true);
					num = 4;
					continue;
				case 20:
					text = RecordTableEnumerator.b("ହ", a_);
					goto IL_499;
				case 21:
					if (!A_1.IsDefaultName)
					{
						num = 17;
						continue;
					}
					goto IL_50E;
				case 22:
					this.ᜀ(A_0, nameOrFormula, A_1);
					num = 11;
					continue;
				case 23:
				{
					bool? invertNegaColor2 = A_1.InvertNegaColor;
					num = 16;
					continue;
				}
				case 24:
					num = 1;
					continue;
				case 25:
					num = 10;
					continue;
				case 26:
					flag = false;
					goto IL_430;
				case 27:
				{
					IEnumerator enumerator = xlsChartDataPointsCollection.GetEnumerator();
					num = 8;
					continue;
				}
				case 28:
					num = 15;
					continue;
				}
				if (A_0 == null)
				{
					num = 6;
					continue;
				}
				num = 7;
				continue;
				IL_B4:
				A_0.WriteEndElement();
				num = 2;
				continue;
				IL_14E:
				this.ᜀ(A_0, A_1);
				xlsChartDataPointsCollection = (XlsChartDataPointsCollection)A_1.DataPoints;
				if (true)
				{
				}
				num = 12;
				continue;
				IL_1A9:
				A_0.WriteStartElement(RecordTableEnumerator.b("䰹", a_), RecordTableEnumerator.b("刹䠻䨽〿硁歃楅㭇⥉⑋⭍㵏㍑❓硕㝗⩙㥛そᡟཡࡣeݧᡩū཭ѯű婳᥵੷ᵹ卻᩽뾏ꂑ꒓ꚕ꺗떙ﾛ솟킡킣", a_));
				A_0.WriteString(nameOrFormula);
				A_0.WriteEndElement();
				num = 3;
				continue;
				IL_2EB:
				A_1.ᜈ();
				num = 21;
				continue;
				IL_430:
				if (!flag)
				{
					num = 24;
					continue;
				}
				num = 20;
				continue;
				IL_499:
				string a_2 = text;
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("匹刻䠽┿ぁぃཅ⹇щ⥋⥍ㅏ♑㵓⁕㵗", a_), a_2);
				num = 18;
				continue;
				IL_50E:
				XlsChartDataPoint xlsChartDataPoint2 = (XlsChartDataPoint)A_1.DataPoints.DefaultDataPoint;
				dataFormatOrNull = xlsChartDataPoint2.DataFormatOrNull;
				num = 13;
			}
			IL_AF:
			throw new ArgumentNullException(RecordTableEnumerator.b("䴹主圽㐿❁㙃", a_));
			IL_509:
			throw new ArgumentNullException(RecordTableEnumerator.b("䤹夻䰽⤿❁㝃", a_));
		}
		}
	}

	// Token: 0x06005547 RID: 21831 RVA: 0x00364E94 File Offset: 0x00363E94
	private void ᜀ(XmlWriter A_0, XlsChartDataPoint A_1)
	{
		int a_ = 13;
		int num = 5;
		XlsChartSerieDataFormat dataFormatOrNull;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				num = 2;
				continue;
			case 2:
				if (!dataFormatOrNull.IsFormatted)
				{
					goto IL_59;
				}
				goto IL_FA;
			case 3:
				if (dataFormatOrNull == null)
				{
					return;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_59;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				break;
			case 4:
				goto IL_44;
			case 6:
				if (true)
				{
				}
				if (A_1 == null)
				{
					num = 7;
					continue;
				}
				dataFormatOrNull = A_1.DataFormatOrNull;
				num = 3;
				continue;
			case 7:
				goto IL_AD;
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			num = 6;
			continue;
			IL_59:
			num = 0;
		}
		IL_44:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑂㝄⹆㵈⹊㽌", a_));
		IL_AD:
		throw new ArgumentNullException(RecordTableEnumerator.b("❂⑄㍆⡈ᭊ≌♎㽐❒", a_));
		IL_FA:
		A_0.WriteStartElement(RecordTableEnumerator.b("❂ᕄ㍆", a_), RecordTableEnumerator.b("⭂ㅄ㍆㥈煊扌恎≐げ㵔㉖㑘㩚⹜煞๠።d०ᅨ٪Ŭ८ṰŲᡴᙶ൸ࡺ卼ၾꪄﮈ戴ﾐﮖ뚘ꦚ궜꾞鞠貢욤쾦좨\ud9aa\ud9ac", a_));
		spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("⩂⅄㽆", a_), A_1.Index.ToString());
		spr\u1CFF.ᜀ(A_0, dataFormatOrNull, dataFormatOrNull.ParentXlsChart, false);
		this.ᜀ(A_0, dataFormatOrNull);
		A_0.WriteEndElement();
	}

	// Token: 0x06005548 RID: 21832 RVA: 0x00364FFC File Offset: 0x00363FFC
	private void ᜁ(XmlWriter A_0, XlsChartSerie A_1, string A_2)
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
		this.ᜁ(A_0, A_1.CategoryLabels, A_1.EnteredDirectlyCategoryLabels, A_2, A_1);
	}

	// Token: 0x06005549 RID: 21833 RVA: 0x0036504C File Offset: 0x0036404C
	private void ᜂ(XmlWriter A_0, XlsChartSerie A_1)
	{
		int a_ = 7;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		this.ᜁ(A_0, A_1.CategoryLabels, A_1.EnteredDirectlyCategoryLabels, RecordTableEnumerator.b("帼帾㕀", a_), A_1);
	}

	// Token: 0x0600554A RID: 21834 RVA: 0x003650B4 File Offset: 0x003640B4
	private void ᜁ(XmlWriter A_0, XlsChartSerie A_1)
	{
		int a_ = 8;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		this.ᜁ(A_0, A_1.Values, A_1.EnteredDirectlyValues, RecordTableEnumerator.b("䠽ℿ⹁", a_), A_1);
	}

	// Token: 0x0600554B RID: 21835 RVA: 0x0036511C File Offset: 0x0036411C
	private void ᜀ(XmlWriter A_0, XlsChartSerie A_1, string A_2)
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
		this.ᜁ(A_0, A_1.Values, A_1.EnteredDirectlyValues, A_2, A_1);
	}

	// Token: 0x0600554C RID: 21836 RVA: 0x0036516C File Offset: 0x0036416C
	private void ᜁ(XmlWriter A_0, IXLSRange A_1, object[] A_2, string A_3, XlsChartSerie A_4)
	{
		int a_ = 15;
		int num = 23;
		for (;;)
		{
			bool flag;
			bool flag2;
			bool flag3;
			bool flag4;
			bool flag5;
			switch (num)
			{
			case 0:
				if (A_3 != null)
				{
					num = 58;
					continue;
				}
				goto IL_6DE;
			case 1:
				if (!flag)
				{
					num = 4;
					continue;
				}
				goto IL_43B;
			case 2:
			{
				XlsWorkbook xlsWorkbook;
				if (!xlsWorkbook.IsCreated)
				{
					num = 39;
					continue;
				}
				goto IL_552;
			}
			case 3:
				if (!flag2)
				{
					num = 36;
					continue;
				}
				goto IL_43B;
			case 4:
				this.ᜀ(A_0, A_1, A_2, A_3, A_4);
				num = 15;
				continue;
			case 5:
				num = 59;
				continue;
			case 6:
				if (A_0 == null)
				{
					num = 13;
					continue;
				}
				num = 0;
				continue;
			case 7:
			{
				XlsWorkbook xlsWorkbook;
				if (!xlsWorkbook.IsLoaded)
				{
					num = 26;
					continue;
				}
				goto IL_3D9;
			}
			case 8:
				if (A_1 != null)
				{
					num = 60;
					continue;
				}
				goto IL_2EF;
			case 9:
				flag3 = true;
				goto IL_6A6;
			case 10:
				if (flag)
				{
					num = 65;
					continue;
				}
				goto IL_2EF;
			case 11:
			{
				XlsWorkbook xlsWorkbook = A_1.Worksheet.Workbook as XlsWorkbook;
				num = 64;
				continue;
			}
			case 12:
				if (A_1.Worksheet != null)
				{
					num = 11;
					continue;
				}
				goto IL_59A;
			case 13:
				goto IL_504;
			case 14:
				if (A_1 is XlsRange)
				{
					num = 30;
					continue;
				}
				num = 51;
				continue;
			case 15:
				goto IL_521;
			case 16:
				flag4 = true;
				goto IL_23C;
			case 17:
				goto IL_56A;
			case 18:
				num = 43;
				continue;
			case 19:
				if (flag2)
				{
					num = 55;
					continue;
				}
				goto IL_3FF;
			case 20:
				num = 62;
				continue;
			case 21:
				return;
			case 22:
				goto IL_6D9;
			case 24:
			{
				XlsWorkbook xlsWorkbook;
				if (xlsWorkbook != null)
				{
					num = 35;
					continue;
				}
				num = 27;
				continue;
			}
			case 25:
				goto IL_66F;
			case 26:
				num = 40;
				continue;
			case 27:
				if (A_1 != null)
				{
					num = 20;
					continue;
				}
				goto IL_267;
			case 28:
				num = 19;
				continue;
			case 29:
				num = 46;
				continue;
			case 30:
				num = 68;
				continue;
			case 31:
				num = 12;
				continue;
			case 32:
				goto IL_19B;
			case 33:
				if (A_1 != null)
				{
					num = 28;
					continue;
				}
				goto IL_3FF;
			case 34:
				goto IL_552;
			case 35:
				num = 2;
				continue;
			case 36:
				num = 1;
				continue;
			case 37:
				this.ᜀ(A_0, A_2, false);
				num = 63;
				continue;
			case 38:
				num = 3;
				continue;
			case 39:
				num = 73;
				continue;
			case 40:
			{
				XlsWorkbook xlsWorkbook;
				if (xlsWorkbook.Loading)
				{
					num = 67;
					continue;
				}
				goto IL_7B5;
			}
			case 41:
				goto IL_318;
			case 42:
				if (A_1 != null)
				{
					num = 18;
					continue;
				}
				goto IL_718;
			case 43:
				if (A_3 == RecordTableEnumerator.b("♄♆㵈", a_))
				{
					num = 45;
					continue;
				}
				goto IL_718;
			case 44:
				goto IL_318;
			case 45:
				this.ᜀ(A_0, A_1, A_2);
				num = 25;
				continue;
			case 46:
				flag4 = (A_1 as spr\u20A6).ᜄ();
				goto IL_23C;
			case 47:
				if (A_1 is spr\u20A6)
				{
					num = 5;
					continue;
				}
				num = 14;
				continue;
			case 48:
				if (A_2 == null)
				{
					num = 21;
					continue;
				}
				goto IL_4E8;
			case 49:
				if (A_1 != null)
				{
					num = 38;
					continue;
				}
				goto IL_43B;
			case 50:
				this.ᜁ(A_0, A_1, A_2, A_4);
				num = 22;
				continue;
			case 51:
				if (!(A_1 as XlsRange).IsNumReference)
				{
					num = 75;
					continue;
				}
				num = 66;
				continue;
			case 52:
				num = 48;
				continue;
			case 53:
				if (A_2 != null)
				{
					if (true)
					{
					}
					num = 72;
					continue;
				}
				goto IL_7B5;
			case 54:
				goto IL_630;
			case 55:
				this.ᜁ(A_0, A_1, A_2, A_4);
				num = 71;
				continue;
			case 56:
			{
				if (A_3.Length == 0)
				{
					num = 57;
					continue;
				}
				XlsWorkbook xlsWorkbook = null;
				num = 76;
				continue;
			}
			case 57:
				goto IL_38D;
			case 58:
				num = 56;
				continue;
			case 59:
				if (!(A_1 as spr\u20A6).ᜀ())
				{
					num = 29;
					continue;
				}
				num = 16;
				continue;
			case 60:
				num = 10;
				continue;
			case 61:
				flag3 = (A_1 as XlsRange).IsStringReference;
				goto IL_6A6;
			case 62:
				if (A_3 != RecordTableEnumerator.b("♄♆㵈", a_))
				{
					num = 50;
					continue;
				}
				goto IL_267;
			case 63:
				goto IL_76B;
			case 64:
				goto IL_59A;
			case 65:
				this.ᜀ(A_0, A_1, A_2);
				num = 54;
				continue;
			case 66:
				flag5 = true;
				goto IL_1EC;
			case 67:
				goto IL_3D9;
			case 68:
				if (!(A_1 as XlsRange).IsNumReference)
				{
					num = 74;
					continue;
				}
				num = 9;
				continue;
			case 69:
				if (A_2 != null)
				{
					num = 37;
					continue;
				}
				goto IL_7B5;
			case 70:
				goto IL_318;
			case 71:
				goto IL_436;
			case 72:
				this.ᜀ(A_0, A_2, false);
				num = 77;
				continue;
			case 73:
			{
				XlsWorkbook xlsWorkbook;
				if (xlsWorkbook.IsConverted)
				{
					num = 34;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_19B;
				default:
					if (false)
					{
					}
					num = 7;
					continue;
				}
				break;
			}
			case 74:
				num = 61;
				continue;
			case 75:
				num = 32;
				continue;
			case 76:
				if (A_1 != null)
				{
					num = 31;
					continue;
				}
				goto IL_59A;
			case 77:
				goto IL_617;
			}
			if (A_1 == null)
			{
				num = 52;
				continue;
			}
			goto IL_4E8;
			IL_1EC:
			flag2 = flag5;
			flag = (A_1 as XlsRange).IsMultiReference;
			num = 41;
			continue;
			IL_19B:
			flag5 = (A_1 as XlsName).IsStringReference;
			goto IL_1EC;
			IL_23C:
			flag2 = flag4;
			flag = (A_1 as spr\u20A6).ᝏ();
			num = 44;
			continue;
			IL_267:
			num = 42;
			continue;
			IL_2EF:
			num = 53;
			continue;
			IL_318:
			num = 49;
			continue;
			IL_3D9:
			num = 47;
			continue;
			IL_3FF:
			num = 8;
			continue;
			IL_43B:
			num = 33;
			continue;
			IL_4E8:
			num = 6;
			continue;
			IL_552:
			this.ᜀ(A_0, A_1, A_2, A_3, A_4);
			num = 17;
			continue;
			IL_59A:
			A_0.WriteStartElement(A_3, RecordTableEnumerator.b("ⵄ㍆㵈㭊睌恎繐⁒㙔㽖㱘㙚㱜ⱞ你ౢᕤɦݨ፪lͮᝰᱲݴ᩶ᡸེ๼兾ꢆ力ﶒ殺뒚꾜꾞醠関誤쒦솨쪪\udfac\udbae", a_));
			num = 24;
			continue;
			IL_6A6:
			flag2 = flag3;
			flag = (A_1 as XlsRange).IsMultiReference;
			num = 70;
			continue;
			IL_718:
			num = 69;
		}
		IL_38D:
		goto IL_6DE;
		IL_436:
		goto IL_7B5;
		IL_504:
		throw new ArgumentNullException(RecordTableEnumerator.b("㉄㕆⁈㽊⡌㵎", a_));
		IL_521:
		IL_56A:
		IL_617:
		IL_630:
		IL_66F:
		IL_6D9:
		goto IL_7B5;
		IL_6DE:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ㅄ♆⹈Պⱌ≎㑐", a_));
		IL_76B:
		IL_7B5:
		A_0.WriteEndElement();
	}

	// Token: 0x0600554D RID: 21837 RVA: 0x00365934 File Offset: 0x00364934
	private void ᜀ(XmlWriter A_0, IXLSRange A_1, object[] A_2, string A_3, XlsChartSerie A_4)
	{
		int a_ = 7;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_2 != null)
				{
					num = 5;
					continue;
				}
				return;
			case 1:
				goto IL_10C;
			case 2:
				goto IL_8D;
			case 4:
				if (A_3 == RecordTableEnumerator.b("帼帾㕀", a_))
				{
					num = 2;
					continue;
				}
				goto IL_10E;
			case 5:
				this.ᜀ(A_0, A_2, false);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_137;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					num = 9;
					continue;
				}
				break;
			case 6:
				num = 7;
				continue;
			case 7:
				if (A_3 != RecordTableEnumerator.b("帼帾㕀", a_))
				{
					num = 1;
					continue;
				}
				goto IL_12C;
			case 8:
				num = 4;
				continue;
			case 9:
				goto IL_DD;
			case 10:
				goto IL_137;
			}
			if (A_1 != null)
			{
				num = 6;
				continue;
			}
			goto IL_12C;
			IL_10E:
			num = 0;
			continue;
			IL_137:
			if (A_1 != null)
			{
				num = 8;
				continue;
			}
			goto IL_10E;
			IL_12C:
			num = 10;
		}
		IL_8D:
		this.ᜀ(A_0, A_1, A_4);
		return;
		IL_DD:
		return;
		IL_10C:
		this.ᜁ(A_0, A_1, A_2, A_4);
	}

	// Token: 0x0600554E RID: 21838 RVA: 0x00365A8C File Offset: 0x00364A8C
	private void ᜁ(XmlWriter A_0, IXLSRange A_1, object[] A_2, XlsChartSerie A_3)
	{
		int a_ = 12;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_5C;
			case 2:
				goto IL_5E;
			case 3:
				if (A_1.Worksheet != null)
				{
					num = 4;
					continue;
				}
				goto IL_5E;
			case 4:
			{
				bool flag = this.ᜀ(A_1);
				num = 2;
				continue;
			}
			case 5:
				goto IL_79;
			case 6:
			{
				if (true)
				{
				}
				bool flag;
				if (flag)
				{
					num = 5;
					continue;
				}
				goto IL_E0;
			}
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_79;
			default:
			{
				if (false)
				{
				}
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				bool flag = A_1.HasString;
				num = 3;
				continue;
			}
			}
			IL_5E:
			num = 6;
		}
		IL_5C:
		throw new ArgumentNullException(RecordTableEnumerator.b("ぁ╃⡅⽇⽉", a_));
		IL_79:
		this.ᜀ(A_0, A_1, A_3);
		return;
		IL_E0:
		this.ᜀ(A_0, A_1, A_2, A_3);
	}

	// Token: 0x0600554F RID: 21839 RVA: 0x00365B84 File Offset: 0x00364B84
	private bool ᜀ(IXLSRange A_0)
	{
		for (;;)
		{
			XlsWorkbook xlsWorkbook = A_0.Worksheet.Workbook as XlsWorkbook;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_8A;
				case 1:
					if (!xlsWorkbook.IsCreated)
					{
						num = 6;
						continue;
					}
					goto IL_8C;
				case 2:
					if (A_0 is XlsRange)
					{
						num = 0;
						continue;
					}
					num = 8;
					continue;
				case 3:
					goto IL_E3;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_11C;
					default:
						goto IL_A9;
					}
					break;
				case 5:
					goto IL_11C;
				case 6:
					num = 7;
					continue;
				case 7:
					if (xlsWorkbook.IsConverted)
					{
						num = 3;
						continue;
					}
					num = 5;
					continue;
				case 8:
					if (A_0 is XlsRange)
					{
						num = 4;
						continue;
					}
					return false;
				case 9:
					goto IL_132;
				}
				break;
				IL_11C:
				if (A_0 is spr\u20A6)
				{
					num = 9;
				}
				else
				{
					if (true)
					{
					}
					num = 2;
				}
			}
		}
		IL_8A:
		return (A_0 as XlsRange).IsStringReference;
		IL_8C:
		return A_0.HasString;
		IL_A9:
		if (false)
		{
		}
		return (A_0 as XlsRange).IsStringReference;
		IL_E3:
		goto IL_8C;
		IL_132:
		return (A_0 as spr\u20A6).ᜄ();
	}

	// Token: 0x06005550 RID: 21840 RVA: 0x00365CC8 File Offset: 0x00364CC8
	private void ᜀ(XmlWriter A_0, IXLSRange A_1, object[] A_2, XlsChartSerie A_3)
	{
		int a_ = 18;
		int num = 1;
		for (;;)
		{
			string text;
			string value;
			switch (num)
			{
			case 0:
				if (A_3.NumRefFormula != null)
				{
					num = 13;
					continue;
				}
				goto IL_1F8;
			case 2:
			{
				ICombinedRange combinedRange;
				text = combinedRange.RangeGlobalAddress2007;
				goto IL_117;
			}
			case 3:
				goto IL_77;
			case 4:
			{
				ICombinedRange combinedRange;
				if (combinedRange != null)
				{
					num = 18;
					continue;
				}
				goto IL_133;
			}
			case 5:
			{
				ICombinedRange combinedRange;
				if (combinedRange == null)
				{
					num = 10;
					continue;
				}
				num = 2;
				continue;
			}
			case 6:
				goto IL_1F8;
			case 7:
				goto IL_190;
			case 8:
				A_0.WriteElementString(RecordTableEnumerator.b("♇㽉⅋്ㅏㅑ㱓㍕", a_), RecordTableEnumerator.b("⁇㹉㡋㹍橏絑筓╕㭗㉙㥛㍝şᅡ䩣॥ᡧཀྵɫ᙭ᵯṱታ᥵੷᝹ᵻ੽겁ꖉﲍﶓﾗ놝銟銡钣邥螧즩쒫쾭슯욱", a_), string.Empty);
				num = 14;
				continue;
			case 9:
				goto IL_133;
			case 10:
				num = 12;
				continue;
			case 11:
				if (A_2 == null)
				{
					num = 8;
					continue;
				}
				this.ᜀ(A_0, A_2, true);
				num = 15;
				continue;
			case 12:
				text = A_1.RangeAddressLocal;
				goto IL_117;
			case 13:
				IL_155:
				value = A_3.NumRefFormula;
				num = 6;
				continue;
			case 14:
				goto IL_E7;
			case 15:
				goto IL_1A9;
			case 16:
				A_2 = null;
				num = 9;
				continue;
			case 17:
			{
				ICombinedRange combinedRange;
				if (!(combinedRange is spr\u20A6))
				{
					num = 16;
					continue;
				}
				goto IL_133;
			}
			case 18:
				num = 17;
				continue;
			case 19:
			{
				if (A_1 == null)
				{
					num = 7;
					continue;
				}
				ICombinedRange combinedRange = A_1 as ICombinedRange;
				num = 5;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			num = 19;
			continue;
			IL_117:
			value = text;
			num = 4;
			continue;
			IL_133:
			num = 0;
			continue;
			IL_1F8:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_155;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("♇㽉⅋ᱍ㕏㑑", a_), RecordTableEnumerator.b("⁇㹉㡋㹍橏絑筓╕㭗㉙㥛㍝şᅡ䩣॥ᡧཀྵɫ᙭ᵯṱታ᥵੷᝹ᵻ੽겁ꖉﲍﶓﾗ놝銟銡钣邥螧즩쒫쾭슯욱", a_));
				A_0.WriteElementString(RecordTableEnumerator.b("⹇", a_), RecordTableEnumerator.b("⁇㹉㡋㹍橏絑筓╕㭗㉙㥛㍝şᅡ䩣॥ᡧཀྵɫ᙭ᵯṱታ᥵੷᝹ᵻ੽겁ꖉﲍﶓﾗ놝銟銡钣邥螧즩쒫쾭슯욱", a_), value);
				num = 11;
				break;
			}
		}
		IL_77:
		throw new ArgumentNullException(RecordTableEnumerator.b("㽇㡉╋㩍㕏⁑", a_));
		IL_E7:
		goto IL_282;
		IL_190:
		throw new ArgumentNullException(RecordTableEnumerator.b("㩇⭉≋⥍㕏", a_));
		IL_1A9:
		IL_282:
		A_0.WriteEndElement();
	}

	// Token: 0x06005551 RID: 21841 RVA: 0x00365F60 File Offset: 0x00364F60
	private void ᜀ(XmlWriter A_0, IXLSRange A_1, XlsChartSerie A_2)
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
		this.ᜀ(A_0, ((XlsRange)A_1).RangeGlobalAddress, A_2);
	}

	// Token: 0x06005552 RID: 21842 RVA: 0x00365FB0 File Offset: 0x00364FB0
	private void ᜀ(XmlWriter A_0, string A_1, XlsChartSerie A_2)
	{
		int a_ = 17;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1[0] == '=')
				{
					num = 7;
					continue;
				}
				goto IL_C5;
			case 1:
				goto IL_4C;
			case 2:
				goto IL_C5;
			case 3:
				goto IL_A5;
			case 4:
				goto IL_90;
			case 6:
				if (A_2.StrRefFormula == null)
				{
					goto IL_11D;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_85;
				default:
					if (false)
					{
					}
					num = 9;
					continue;
				}
				break;
			case 7:
				A_1 = UtilityMethods.ᜀ(A_1);
				num = 2;
				continue;
			case 8:
				goto IL_85;
			case 9:
				if (true)
				{
				}
				A_1 = A_2.StrRefFormula;
				num = 3;
				continue;
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			num = 8;
			continue;
			IL_85:
			if (A_1 == null)
			{
				num = 4;
				continue;
			}
			num = 0;
			continue;
			IL_C5:
			num = 6;
		}
		IL_4C:
		throw new ArgumentNullException(RecordTableEnumerator.b("う㭈≊㥌⩎⍐", a_));
		IL_90:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕆⡈╊⩌⩎", a_));
		IL_A5:
		IL_11D:
		A_0.WriteStartElement(RecordTableEnumerator.b("㑆㵈㥊Ὄ⩎㝐", a_), RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒♔㑖ㅘ㹚ぜ㹞በ䵢੤ᝦ౨ժᕬɮᵰᕲᩴնᑸ᩺ॼ౾꾀Ꚉﾌ朗ﮔ늜궞醠鎢鎤袦쪨쎪첬\uddae얰", a_));
		A_0.WriteElementString(RecordTableEnumerator.b("ⅆ", a_), RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒♔㑖ㅘ㹚ぜ㹞በ䵢੤ᝦ౨ժᕬɮᵰᕲᩴնᑸ᩺ॼ౾꾀Ꚉﾌ朗ﮔ늜궞醠鎢鎤袦쪨쎪첬\uddae얰", a_), A_1);
		A_0.WriteElementString(RecordTableEnumerator.b("㑆㵈㥊์⹎㉐㭒ご", a_), RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒♔㑖ㅘ㹚ぜ㹞በ䵢੤ᝦ౨ժᕬɮᵰᕲᩴնᑸ᩺ॼ౾꾀Ꚉﾌ朗ﮔ늜궞醠鎢鎤袦쪨쎪첬\uddae얰", a_), string.Empty);
		A_0.WriteEndElement();
	}

	// Token: 0x06005553 RID: 21843 RVA: 0x0036614C File Offset: 0x0036514C
	private void ᜀ(XmlWriter A_0, IXLSRange A_1, object[] A_2)
	{
		int a_ = 1;
		int num = 9;
		for (;;)
		{
			string text;
			switch (num)
			{
			case 0:
				A_0.WriteElementString(RecordTableEnumerator.b("娶䰸场䤼嘾ീ㕂⥄ᑆ㵈㥊์⹎㉐㭒ご", a_), RecordTableEnumerator.b("弶䴸伺䴼Ծ湀求㙄⑆ⅈ⹊⁌⹎≐絒㩔❖㱘㕚╜㉞ൠբ੤ᕦѨ੪ᥬᱮ彰ᱲݴၶ噸ὺོṾꊌ붎ꆐꎒꎔ뢖滛ﲜ햠", a_), string.Empty);
				num = 10;
				continue;
			case 1:
				text = A_1.RangeGlobalAddress;
				goto IL_152;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_11E;
				default:
					goto IL_F6;
				}
				break;
			case 3:
				goto IL_129;
			case 4:
				if (A_2 == null)
				{
					num = 0;
					continue;
				}
				this.ᜀ(A_0, A_2, true);
				num = 7;
				continue;
			case 5:
			{
				ICombinedRange combinedRange;
				text = combinedRange.RangeGlobalAddress2007;
				goto IL_152;
			}
			case 6:
			{
				if (A_1 == null)
				{
					goto IL_11E;
				}
				ICombinedRange combinedRange = A_1 as ICombinedRange;
				num = 8;
				continue;
			}
			case 7:
				goto IL_AE;
			case 8:
			{
				ICombinedRange combinedRange;
				if (combinedRange == null)
				{
					if (true)
					{
					}
					num = 11;
					continue;
				}
				num = 5;
				continue;
			}
			case 10:
				goto IL_8B;
			case 11:
				num = 1;
				continue;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			num = 6;
			continue;
			IL_11E:
			num = 3;
			continue;
			IL_152:
			string value = text;
			A_0.WriteStartElement(RecordTableEnumerator.b("娶䰸场䤼嘾ീ㕂⥄ᑆ㵈㥊Ὄ⩎㝐", a_), RecordTableEnumerator.b("弶䴸伺䴼Ծ湀求㙄⑆ⅈ⹊⁌⹎≐絒㩔❖㱘㕚╜㉞ൠբ੤ᕦѨ੪ᥬᱮ彰ᱲݴၶ噸ὺོṾꊌ붎ꆐꎒꎔ뢖滛ﲜ햠", a_));
			A_0.WriteElementString(RecordTableEnumerator.b("儶", a_), RecordTableEnumerator.b("弶䴸伺䴼Ծ湀求㙄⑆ⅈ⹊⁌⹎≐絒㩔❖㱘㕚╜㉞ൠբ੤ᕦѨ੪ᥬᱮ彰ᱲݴၶ噸ὺོṾꊌ붎ꆐꎒꎔ뢖滛ﲜ햠", a_), value);
			num = 4;
		}
		IL_8B:
		IL_AE:
		goto IL_1CC;
		IL_F6:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䀶䬸刺䤼娾㍀", a_));
		IL_129:
		throw new ArgumentNullException(RecordTableEnumerator.b("䔶堸唺娼娾", a_));
		IL_1CC:
		A_0.WriteEndElement();
	}

	// Token: 0x06005554 RID: 21844 RVA: 0x0036632C File Offset: 0x0036532C
	private void ᜁ(XmlWriter A_0, XlsChart A_1, RelationsCollection A_2)
	{
		for (;;)
		{
			spr\u2433 spr_u = new spr\u2433();
			int num = 11;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2BA;
					default:
						goto IL_1C7;
					}
					break;
				case 1:
					goto IL_2BA;
				case 2:
					goto IL_1D2;
				case 3:
					num = 10;
					continue;
				case 4:
					if (A_1.IsValueAxisAvail)
					{
						num = 15;
						continue;
					}
					goto IL_1D2;
				case 5:
					goto IL_175;
				case 6:
					if (Array.IndexOf<int>(A_1.SerializedAxisIds.ToArray(), (A_1.PrimaryCategoryAxis as XlsChartAxis).AxisId) >= 0)
					{
						num = 21;
						continue;
					}
					goto IL_111;
				case 7:
					spr_u.ᜀ(A_0, A_1.PrimarySerieAxis, A_2);
					num = 0;
					continue;
				case 8:
					if (A_1.IsSecondaryValueAxisAvail)
					{
						num = 14;
						continue;
					}
					goto IL_137;
				case 9:
					num = 6;
					continue;
				case 10:
					if (Array.IndexOf<int>(A_1.SerializedAxisIds.ToArray(), (A_1.SecondaryCategoryAxis as XlsChartAxis).AxisId) >= 0)
					{
						num = 18;
						continue;
					}
					goto IL_175;
				case 11:
					if (A_1.IsCategoryAxisAvail)
					{
						num = 9;
						continue;
					}
					goto IL_111;
				case 12:
					if (A_1.IsSecondaryCategoryAxisAvail)
					{
						num = 3;
						continue;
					}
					goto IL_175;
				case 13:
					if (Array.IndexOf<int>(A_1.SerializedAxisIds.ToArray(), (A_1.PrimaryValueAxis as XlsChartAxis).AxisId) >= 0)
					{
						num = 1;
						continue;
					}
					goto IL_1D2;
				case 14:
					if (true)
					{
					}
					num = 22;
					continue;
				case 15:
					num = 13;
					continue;
				case 16:
					spr_u.ᜀ(A_0, A_1.SecondaryValueAxis, A_2);
					num = 20;
					continue;
				case 17:
					goto IL_111;
				case 18:
					spr_u.ᜀ(A_0, A_1.SecondaryCategoryAxis, A_2);
					num = 5;
					continue;
				case 19:
					if (A_1.IsSeriesAxisAvail)
					{
						num = 7;
						continue;
					}
					return;
				case 20:
					goto IL_137;
				case 21:
					spr_u.ᜀ(A_0, A_1.PrimaryCategoryAxis, A_2);
					num = 17;
					continue;
				case 22:
					if (Array.IndexOf<int>(A_1.SerializedAxisIds.ToArray(), (A_1.SecondaryValueAxis as XlsChartAxis).AxisId) >= 0)
					{
						num = 16;
						continue;
					}
					goto IL_137;
				}
				break;
				IL_111:
				num = 4;
				continue;
				IL_137:
				num = 19;
				continue;
				IL_175:
				num = 8;
				continue;
				IL_1D2:
				num = 12;
				continue;
				IL_2BA:
				spr_u.ᜀ(A_0, A_1.PrimaryValueAxis, A_2);
				num = 2;
			}
		}
		IL_1C7:
		if (false)
		{
		}
	}

	// Token: 0x06005555 RID: 21845 RVA: 0x00366614 File Offset: 0x00365614
	private void ᜀ(XmlWriter A_0, XlsChart A_1, RelationsCollection A_2)
	{
		for (;;)
		{
			spr\u2433 spr_u = new spr\u2433();
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					spr_u.ᜀ(A_0, A_1.PrimarySerieAxis, A_2);
					num = 6;
					continue;
				case 1:
					spr_u.ᜀ(A_0, A_1.PrimaryCategoryAxis, A_2);
					num = 7;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_45;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						goto IL_4F;
					}
					break;
				case 3:
					if (A_1.IsValueAxisAvail)
					{
						num = 8;
						continue;
					}
					goto IL_4F;
				case 4:
					if (A_1.IsCategoryAxisAvail)
					{
						goto IL_45;
					}
					goto IL_EB;
				case 5:
					if (A_1.IsPivot3DChart)
					{
						num = 0;
						continue;
					}
					return;
				case 6:
					return;
				case 7:
					goto IL_EB;
				case 8:
					spr_u.ᜀ(A_0, A_1.PrimaryValueAxis, A_2);
					num = 2;
					continue;
				}
				break;
				IL_45:
				num = 1;
				continue;
				IL_4F:
				num = 5;
				continue;
				IL_EB:
				num = 3;
			}
		}
	}

	// Token: 0x06005556 RID: 21846 RVA: 0x00366734 File Offset: 0x00365734
	private void ᜀ(XmlWriter A_0, XlsChartSerie A_1)
	{
		int a_ = 16;
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
					goto IL_7B;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 1:
				if (A_1 == null)
				{
					goto IL_7B;
				}
				goto IL_A1;
			case 2:
				goto IL_50;
			case 3:
				goto IL_83;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			num = 1;
			continue;
			IL_7B:
			num = 3;
		}
		IL_50:
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅅ㩇⍉㡋⭍≏", a_));
		IL_83:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㕅ⵇ㡉╋⭍⍏", a_));
		IL_A1:
		XlsChartSerieDataFormat a_2 = (XlsChartSerieDataFormat)A_1.Format;
		this.ᜀ(A_0, a_2);
	}

	// Token: 0x06005557 RID: 21847 RVA: 0x003667F8 File Offset: 0x003657F8
	private void ᜀ(XmlWriter A_0, XlsChartSerieDataFormat A_1)
	{
		int a_ = 4;
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				IWorkbook parentWorkbook;
				double num2;
				switch (num)
				{
				case 0:
					if (!A_1.IsSupportFill)
					{
						num = 4;
						continue;
					}
					num = 28;
					continue;
				case 1:
					goto IL_3D2;
				case 3:
					A_1.MarkerLineStream.Position = 0L;
					ShapeParser.WriteNodeFromStream(A_0, A_1.MarkerLineStream);
					num = 19;
					continue;
				case 4:
					num = 9;
					continue;
				case 5:
					if (!A_1.MarkerFormat.ᜀ())
					{
						num = 33;
						continue;
					}
					goto IL_1FC;
				case 6:
				{
					spr\u208B spr_u208B = new spr\u208B();
					spr_u208B.ᜁ(A_0, A_1.MarkerGradient, parentWorkbook);
					num = 21;
					continue;
				}
				case 7:
					if (!A_1.MarkerFormat.ᜇ())
					{
						num = 29;
						continue;
					}
					goto IL_4B8;
				case 8:
					if (A_1.IsMarker)
					{
						num = 24;
						continue;
					}
					goto IL_28F;
				case 9:
					num2 = 0.0;
					goto IL_25F;
				case 10:
					if (A_1.EffectListStream != null)
					{
						num = 20;
						continue;
					}
					goto IL_FA;
				case 11:
					goto IL_447;
				case 12:
					goto IL_28A;
				case 13:
					if (true)
					{
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("圹崻䰽⬿❁㙃", a_), RecordTableEnumerator.b("刹䠻䨽〿硁歃楅㭇⥉⑋⭍㵏㍑❓硕㝗⩙㥛そᡟཡࡣeݧᡩū཭ѯű婳᥵੷ᵹ卻᩽뾏ꂑ꒓ꚕ꺗떙ﾛ솟킡킣", a_));
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("䤹䔻匽∿ⵁ⡃", a_), XLSXChartMarkerType.none.ToString());
					A_0.WriteEndElement();
					num = 1;
					continue;
				case 14:
					if (A_1.MarkerFormat.ᜀ())
					{
						num = 25;
						continue;
					}
					goto IL_46F;
				case 15:
					if (A_1.IsMarkerSupported)
					{
						num = 13;
						continue;
					}
					return;
				case 16:
					num = 5;
					continue;
				case 17:
					goto IL_FA;
				case 18:
					if (A_1.MarkerLineStream != null)
					{
						num = 3;
						continue;
					}
					spr\u2541.ᜀ(A_0, A_1.MarkerForegroundColor, parentWorkbook, A_1.MarkerFormat.ᜅ(), A_1.MarkerTransparencyValue);
					num = 11;
					continue;
				case 19:
					goto IL_447;
				case 20:
					ShapeParser.WriteNodeFromStream(A_0, A_1.EffectListStream);
					num = 17;
					continue;
				case 21:
					goto IL_46F;
				case 22:
					num = 8;
					continue;
				case 23:
					if (A_1.MarkerGradient != null)
					{
						num = 6;
						continue;
					}
					num = 0;
					continue;
				case 24:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_28A;
					default:
						if (false)
						{
						}
						num = 26;
						continue;
					}
					break;
				case 25:
					A_0.WriteElementString(RecordTableEnumerator.b("吹医砽⤿⹁⡃", a_), RecordTableEnumerator.b("刹䠻䨽〿硁歃楅㭇⥉⑋⭍㵏㍑❓硕㝗⩙㥛そᡟཡࡣeݧᡩū཭ѯű婳᥵੷ᵹ卻᩽뾏ꂑ꒓ꚕ꺗떙ﾝ즟첡", a_), string.Empty);
					num = 31;
					continue;
				case 26:
					if (!A_1.IsAutoMarker)
					{
						num = 30;
						continue;
					}
					return;
				case 27:
					if (A_1.EffectListStream == null)
					{
						num = 16;
						continue;
					}
					goto IL_1FC;
				case 28:
					num2 = A_1.Fill.Transparency;
					goto IL_25F;
				case 29:
					A_0.WriteStartElement(RecordTableEnumerator.b("䤹䰻渽㈿", a_), RecordTableEnumerator.b("刹䠻䨽〿硁歃楅㭇⥉⑋⭍㵏㍑❓硕㝗⩙㥛そᡟཡࡣeݧᡩū཭ѯű婳᥵੷ᵹ卻᩽뾏ꂑ꒓ꚕ꺗떙ﾛ솟킡킣", a_));
					parentWorkbook = A_1.ParentXlsChart.ParentWorkbook;
					num = 27;
					continue;
				case 30:
				{
					A_0.WriteStartElement(RecordTableEnumerator.b("圹崻䰽⬿❁㙃", a_), RecordTableEnumerator.b("刹䠻䨽〿硁歃楅㭇⥉⑋⭍㵏㍑❓硕㝗⩙㥛そᡟཡࡣeݧᡩū཭ѯű婳᥵੷ᵹ卻᩽뾏ꂑ꒓ꚕ꺗떙ﾛ솟킡킣", a_));
					XLSXChartMarkerType markerStyle = (XLSXChartMarkerType)A_1.MarkerStyle;
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("䤹䔻匽∿ⵁ⡃", a_), markerStyle.ToString());
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("䤹唻䐽┿", a_), A_1.MarkerSize.ToString());
					num = 7;
					continue;
				}
				case 31:
					goto IL_46F;
				case 32:
					goto IL_10C;
				case 33:
					num = 23;
					continue;
				}
				if (A_1.IsMarkerSupported)
				{
					num = 22;
					continue;
				}
				goto IL_28F;
				IL_FA:
				A_0.WriteEndElement();
				num = 32;
				continue;
				IL_1FC:
				num = 14;
				continue;
				IL_25F:
				double num3 = num2;
				spr\u1CFF.ᜀ(A_0, A_1.MarkerBackgroundColor, false, parentWorkbook, 1.0 - num3);
				num = 12;
				continue;
				IL_28F:
				num = 15;
				continue;
				IL_447:
				num = 10;
				continue;
				IL_46F:
				num = 18;
				continue;
				IL_28A:
				goto IL_46F;
			}
			IL_10C:
			goto IL_4B8;
			IL_3D2:
			return;
			IL_4B8:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06005558 RID: 21848 RVA: 0x00366D00 File Offset: 0x00365D00
	internal static void ᜀ(XmlWriter A_0, Color A_1, IWorkbook A_2)
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
		spr\u2541.ᜀ(A_0, A_1, A_2, false);
	}

	// Token: 0x06005559 RID: 21849 RVA: 0x00366D44 File Offset: 0x00365D44
	internal static void ᜀ(XmlWriter A_0, Color A_1, IWorkbook A_2, bool A_3, double A_4)
	{
		int a_ = 7;
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
		for (;;)
		{
			A_0.WriteStartElement(RecordTableEnumerator.b("儼儾", a_), RecordTableEnumerator.b("唼䬾㕀㍂罄框晈㡊⹌❎㑐㹒㑔⑖睘㑚ⵜ㩞འ᭢ࡤ୦ཨѪὬɮၰݲٴ奶ᙸॺ᩼偾﶐벒ꞔꞖꦘ궚늜삠쪢쮤", a_));
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_B8;
				case 1:
					if (true)
					{
					}
					if (!A_3)
					{
						num = 3;
						continue;
					}
					A_0.WriteElementString(RecordTableEnumerator.b("匼倾݀⩂⥄⭆", a_), RecordTableEnumerator.b("唼䬾㕀㍂罄框晈㡊⹌❎㑐㹒㑔⑖睘㑚ⵜ㩞འ᭢ࡤ୦ཨѪὬɮၰݲٴ奶ᙸॺ᩼偾﶐벒ꞔꞖꦘ궚늜삠쪢쮤", a_), string.Empty);
					num = 0;
					continue;
				case 2:
					goto IL_D5;
				case 3:
					spr\u1CFF.ᜀ(A_0, A_1, false, A_2, A_4);
					num = 2;
					continue;
				}
				break;
			}
		}
		IL_B8:
		IL_D5:
		A_0.WriteEndElement();
	}

	// Token: 0x0600555A RID: 21850 RVA: 0x00366E30 File Offset: 0x00365E30
	internal static void ᜀ(XmlWriter A_0, Color A_1, IWorkbook A_2, bool A_3)
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
		spr\u2541.ᜀ(A_0, A_1, A_2, A_3, 1.0);
	}

	// Token: 0x0600555B RID: 21851 RVA: 0x00366E80 File Offset: 0x00365E80
	private void ᜀ(XmlWriter A_0, XlsChart A_1, XlsChartSerie A_2)
	{
		int a_ = 2;
		for (;;)
		{
			switch (0)
			{
			default:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_37;
				}
				break;
			}
		}
		IL_37:
		if (true)
		{
		}
		if (false)
		{
		}
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_18E;
			case 2:
				goto IL_16E;
			case 3:
			{
				XlsChartFormat xlsChartFormat;
				if (xlsChartFormat.IsDropBar)
				{
					num = 5;
					continue;
				}
				return;
			}
			case 4:
				goto IL_82;
			case 5:
			{
				XlsChartFormat xlsChartFormat;
				IChartDropBar firstDropBar = xlsChartFormat.FirstDropBar;
				IChartDropBar secondDropBar = xlsChartFormat.SecondDropBar;
				A_0.WriteStartElement(RecordTableEnumerator.b("䴷䨹砻儽㜿ⱁك❅㩇㥉", a_), RecordTableEnumerator.b("倷丹䠻丽稿流歃㕅⭇≉⥋⍍ㅏ⅑穓㥕⡗㽙㉛♝ൟ๡ɣ॥ᩧݩ൫ᩭͯ山᭳ѵί啹᡻౽ꆍꊏꊑ꒓ꂕ랗蓮ﾝ튟횡", a_));
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("強嬹䰻椽⤿♁ぃ⹅", a_), firstDropBar.GapWidth.ToString());
				this.ᜀ(A_0, firstDropBar, RecordTableEnumerator.b("䴷䨹縻弽㈿ㅁ", a_), A_1);
				this.ᜀ(A_0, secondDropBar, RecordTableEnumerator.b("尷唹䬻倽ȿ⍁㙃㕅", a_), A_1);
				A_0.WriteEndElement();
				num = 2;
				continue;
			}
			case 6:
			{
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				XlsChartFormat xlsChartFormat = (XlsChartFormat)A_2.Format.Options;
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
				num = 6;
			}
		}
		IL_82:
		throw new ArgumentNullException(RecordTableEnumerator.b("伷䠹唻䨽┿ぁ", a_));
		IL_16E:
		return;
		IL_18E:
		throw new ArgumentNullException(RecordTableEnumerator.b("嬷刹崻䰽㐿", a_));
	}

	// Token: 0x0600555C RID: 21852 RVA: 0x00367020 File Offset: 0x00366020
	private void ᜀ(XmlWriter A_0, IChartDropBar A_1, string A_2, XlsChart A_3)
	{
		int a_ = 3;
		for (;;)
		{
			IL_09:
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_8F;
				case 1:
					num = 3;
					continue;
				case 2:
					if (A_1 == null)
					{
						num = 6;
						continue;
					}
					num = 7;
					continue;
				case 3:
					if (A_2.Length == 0)
					{
						num = 0;
						continue;
					}
					goto IL_106;
				case 4:
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
				case 5:
					goto IL_6A;
				case 6:
					goto IL_E6;
				case 7:
					if (A_2 != null)
					{
						num = 1;
						continue;
					}
					goto IL_B9;
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
		}
		IL_6A:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("丸䤺吼䬾⑀ㅂ", a_));
		IL_8F:
		IL_B9:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䴸娺娼焾⁀⹂⁄", a_));
		IL_E6:
		throw new ArgumentNullException(RecordTableEnumerator.b("崸䤺刼伾̀≂㝄", a_));
		IL_106:
		A_0.WriteStartElement(A_2, RecordTableEnumerator.b("儸伺䤼伾筀求橄㑆⩈⍊⡌≎ぐ⁒答㡖⥘㹚㍜❞ౠརͤࡦ᭨٪౬᭮ɰ嵲ᩴնṸ呺᥼ൾꂎꎐꎒꖔꆖ뚘ﺞ펠힢", a_));
		spr\u1CFF.ᜀ(A_0, A_1, A_3, false);
		A_0.WriteEndElement();
	}

	// Token: 0x0600555D RID: 21853 RVA: 0x00367158 File Offset: 0x00366158
	internal void ᜃ(XmlWriter A_0, XlsChart A_1, string A_2)
	{
		int a_ = 9;
		for (;;)
		{
			IL_09:
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 4;
					continue;
				case 1:
					if (A_1 == null)
					{
						num = 3;
						continue;
					}
					num = 6;
					continue;
				case 2:
					goto IL_6A;
				case 3:
					goto IL_E6;
				case 4:
					if (A_2.Length == 0)
					{
						num = 7;
						continue;
					}
					goto IL_106;
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
					if (A_2 != null)
					{
						num = 0;
						continue;
					}
					goto IL_B1;
				case 7:
					goto IL_87;
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
		}
		IL_6A:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠾㍀⩂ㅄ≆㭈", a_));
		IL_87:
		IL_B1:
		if (true)
		{
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("嬾㍀≂㉄⹆❈ⱊὌ⩎㵐㉒⅔㹖㙘㕚", a_));
		IL_E6:
		throw new ArgumentNullException(RecordTableEnumerator.b("尾⥀≂㝄㍆", a_));
		IL_106:
		A_0.WriteStartDocument();
		A_0.WriteStartElement(RecordTableEnumerator.b("尾⥀≂㝄㍆㩈⍊⡌⩎═", a_), RecordTableEnumerator.b("圾㕀㝂㕄絆晈摊㹌ⱎ㥐㙒㡔㙖⩘畚㉜⽞Ѡൢᵤ੦ը൪ɬᵮᱰቲŴѶ坸ᑺོ᡾꺀ﲎ戀늜궞醠鎢鎤袦쒨쪪쒬솮", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("䜾ⱀ⽂⭄㑆", a_), RecordTableEnumerator.b("䴾", a_), null, RecordTableEnumerator.b("圾㕀㝂㕄絆晈摊㹌ⱎ㥐㙒㡔㙖⩘畚㉜⽞Ѡൢᵤ੦ը൪ɬᵮᱰቲŴѶ坸ᑺོ᡾꺀쮎ﺐ殺ﲘ낞鎠鎢閤醦蚨\ud9aa좬쎮킰잲\udcb4\ud8b6ힸ좺햼횾뇀냂", a_));
		A_0.WriteElementString(RecordTableEnumerator.b("䰾⥀♂⁄㍆᥈㥊", a_), string.Empty);
		A_0.WriteStartElement(RecordTableEnumerator.b("䰾⥀♂⁄㍆Ὀ≊⡌㡎≐", a_));
		A_0.WriteStartElement(RecordTableEnumerator.b("䰾⥀♂⁄㍆Ὀ≊⡌㡎", a_));
		spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("䔾⹀ⱂ⡄ᑆ⩈⩊⅌⩎", a_), A_1.Zoom, 100);
		A_0.WriteAttributeString(RecordTableEnumerator.b("䠾⹀ㅂ⹄╆♈⑊♌᥎㡐㙒≔Ṗ㵘", a_), RecordTableEnumerator.b("༾", a_));
		spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("䔾⹀ⱂ⡄ፆ♈ൊ⑌㭎", a_), A_1.ZoomToFit, false);
		A_0.WriteEndElement();
		A_0.WriteEndElement();
		XlsWorkbook parentWorkbook = A_1.ParentWorkbook;
		spr\u1B7A spr_u1B7A = parentWorkbook.DataHolder.\u170D();
		spr_u1B7A.ᜁ(A_0, A_1);
		spr\u171C a_2 = new spr\u1CDC();
		spr\u1B7A.ᜂ(A_0, A_1.PageSetup, a_2);
		spr\u1B7A.ᜁ(A_0, A_1.PageSetup, a_2);
		spr\u1B7A.ᜀ(A_0, A_1.PageSetupBase, a_2);
		A_0.WriteStartElement(RecordTableEnumerator.b("嬾㍀≂㉄⹆❈ⱊ", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("嘾╀", a_), RecordTableEnumerator.b("圾㕀㝂㕄絆晈摊㹌ⱎ㥐㙒㡔㙖⩘畚㉜⽞Ѡൢᵤ੦ը൪ɬᵮᱰቲŴѶ坸ᑺོ᡾꺀쮎ﺐ殺ﲘ낞鎠鎢閤醦蚨\ud9aa좬쎮킰잲\udcb4\ud8b6ힸ좺햼횾뇀냂", a_), A_2);
		A_0.WriteEndElement();
		spr_u1B7A.ᜀ(A_0, A_1);
		spr\u1B7A.ᜀ(A_0, A_1, new spr\u1A61(), null);
		A_0.WriteEndElement();
	}

	// Token: 0x0600555E RID: 21854 RVA: 0x0036740C File Offset: 0x0036640C
	public void ᜂ(XmlWriter A_0, XlsChart A_1, string A_2)
	{
		int a_ = 11;
		int width;
		for (;;)
		{
			int num = 3;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					goto IL_202;
				case 1:
					num = 5;
					continue;
				case 2:
					goto IL_82;
				case 4:
					num2 = 8666049;
					goto IL_22C;
				case 5:
					goto IL_6E;
				case 6:
					num2 = (int)A_1.Width;
					goto IL_22C;
				case 7:
					num = 6;
					continue;
				case 8:
					if (A_1.Width > 0.0)
					{
						num = 7;
						continue;
					}
					num = 4;
					continue;
				case 9:
					goto IL_61;
				case 10:
					if (A_1 == null)
					{
						num = 0;
						continue;
					}
					if (true)
					{
					}
					A_0.WriteStartDocument(true);
					A_0.WriteStartElement(RecordTableEnumerator.b("㥀❂㝄", a_), RecordTableEnumerator.b("㙀あń㕆", a_), RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂ﲊﺒ璉뢖ꮘꮚ궜ꦞ躠킢햤햦첨쪪즬\udcae\ud9b0횲킴쎶ﶸ즺\udcbc좾ꣀ귂ꋄ", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("㥀⹂⥄⥆㩈", a_), RecordTableEnumerator.b("⁀", a_), null, RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂ﲊﺒ璉뢖ꮘꮚ궜ꦞ躠캢쒤캦잨", a_));
					A_0.WriteStartElement(RecordTableEnumerator.b("⁀⅂㙄⡆╈㹊㥌⩎ၐ㵒㙔㽖㙘⥚", a_), RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂ﲊﺒ璉뢖ꮘꮚ궜ꦞ躠킢햤햦첨쪪즬\udcae\ud9b0횲킴쎶ﶸ즺\udcbc좾ꣀ귂ꋄ", a_));
					A_0.WriteStartElement(RecordTableEnumerator.b("ㅀⱂ㙄", a_), RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂ﲊﺒ璉뢖ꮘꮚ궜ꦞ躠킢햤햦첨쪪즬\udcae\ud9b0횲킴쎶ﶸ즺\udcbc좾ꣀ귂ꋄ", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("㥀", a_), RecordTableEnumerator.b("煀", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("㡀", a_), RecordTableEnumerator.b("煀", a_));
					A_0.WriteEndElement();
					num = 8;
					continue;
				case 11:
					if (A_1.Height > 0.0)
					{
						num = 1;
						continue;
					}
					num = 2;
					continue;
				}
				if (A_0 == null)
				{
					num = 9;
					continue;
				}
				num = 10;
				continue;
				IL_22C:
				width = num2;
				num = 11;
			}
			IL_82:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_98;
			}
		}
		IL_61:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙀ㅂⱄ㍆ⱈ㥊", a_));
		IL_6E:
		int num3 = (int)A_1.Height;
		goto IL_26E;
		IL_98:
		if (false)
		{
		}
		num3 = 6293304;
		goto IL_26E;
		IL_202:
		throw new ArgumentNullException(RecordTableEnumerator.b("≀⭂⑄㕆㵈", a_));
		IL_26E:
		int height = num3;
		spr\u1B7A.ᜀ(A_0, new Size(width, height));
		A_0.WriteStartElement(RecordTableEnumerator.b("♀ㅂ⑄㝆ⅈ≊⹌ॎ⍐㉒㡔㉖", a_), RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂ﲊﺒ璉뢖ꮘꮚ궜ꦞ躠킢햤햦첨쪪즬\udcae\ud9b0횲킴쎶ﶸ즺\udcbc좾ꣀ귂ꋄ", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("ⱀ≂♄㕆♈", a_), string.Empty);
		A_0.WriteStartElement(RecordTableEnumerator.b("⽀㕂Ʉ㕆⡈㭊╌♎㉐ᕒ❔㙖㑘㹚൜ⵞ", a_), RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂ﲊﺒ璉뢖ꮘꮚ궜ꦞ躠킢햤햦첨쪪즬\udcae\ud9b0횲킴쎶ﶸ즺\udcbc좾ꣀ귂ꋄ", a_));
		A_0.WriteStartElement(RecordTableEnumerator.b("≀ൂ㍄ᝆ㭈", a_), RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂ﲊﺒ璉뢖ꮘꮚ궜ꦞ躠킢햤햦첨쪪즬\udcae\ud9b0횲킴쎶ﶸ즺\udcbc좾ꣀ귂ꋄ", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("⡀❂", a_), RecordTableEnumerator.b("獀", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("⽀≂⡄≆", a_), A_1.Name);
		A_0.WriteEndElement();
		A_0.WriteStartElement(RecordTableEnumerator.b("≀ൂ㍄F㭈⩊㵌❎㡐げፔ╖㡘㙚㡜ཞ፠", a_), RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂ﲊﺒ璉뢖ꮘꮚ궜ꦞ躠킢햤햦첨쪪즬\udcae\ud9b0횲킴쎶ﶸ즺\udcbc좾ꣀ귂ꋄ", a_));
		A_0.WriteStartElement(RecordTableEnumerator.b("♀ㅂ⑄㝆ⅈ≊⹌ॎ⍐㉒㡔㉖ᕘ㑚㹜㑞በ", a_), RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂ﲊﺒ璉뢖ꮘꮚ궜ꦞ躠캢쒤캦잨", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("⽀ⱂɄ㕆㥈", a_), RecordTableEnumerator.b("灀", a_));
		A_0.WriteEndElement();
		A_0.WriteEndElement();
		A_0.WriteEndElement();
		spr\u1A78.ᜀ(A_0, RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂ﲊﺒ璉뢖ꮘꮚ궜ꦞ躠킢햤햦첨쪪즬\udcae\ud9b0횲킴쎶ﶸ즺\udcbc좾ꣀ귂ꋄ", a_), RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂ﲊﺒ璉뢖ꮘꮚ궜ꦞ躠캢쒤캦잨", a_), 0, 0, 0, 0);
		A_0.WriteStartElement(RecordTableEnumerator.b("♀ㅂ⑄㝆ⅈ≊⹌", a_), RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂ﲊﺒ璉뢖ꮘꮚ궜ꦞ躠캢쒤캦잨", a_));
		A_0.WriteStartElement(RecordTableEnumerator.b("♀ㅂ⑄㝆ⅈ≊⹌୎ぐ❒㑔", a_), RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂ﲊﺒ璉뢖ꮘꮚ궜ꦞ躠캢쒤캦잨", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("㑀ㅂⱄ", a_), RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂ﲊﺒ璉뢖ꮘꮚ궜ꦞ躠삢춤욦\udba8\udfaa", a_));
		A_0.WriteStartElement(RecordTableEnumerator.b("≀", a_), RecordTableEnumerator.b("≀⭂⑄㕆㵈", a_), RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂ﲊﺒ璉뢖ꮘꮚ궜ꦞ躠삢춤욦\udba8\udfaa", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("⡀❂", a_), RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂햐ﲒﺚ躠醢閤鞦龨蒪\udfac쪮\uddb0튲솴\udeb6횸햺캼ힾꣀ돂뛄", a_), A_2);
		A_0.WriteEndElement();
		A_0.WriteEndElement();
		A_0.WriteEndElement();
		A_0.WriteEndElement();
		A_0.WriteElementString(RecordTableEnumerator.b("≀⽂ⱄ≆❈㽊ौ⹎═㉒", a_), RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂ﲊﺒ璉뢖ꮘꮚ궜ꦞ躠킢햤햦첨쪪즬\udcae\ud9b0횲킴쎶ﶸ즺\udcbc좾ꣀ귂ꋄ", a_), string.Empty);
		A_0.WriteEndElement();
		A_0.WriteEndElement();
	}

	// Token: 0x0600555F RID: 21855 RVA: 0x003678F8 File Offset: 0x003668F8
	private void ᜀ(XmlWriter A_0, XlsChart A_1)
	{
		int a_ = 9;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1.HasDataTable)
				{
					num = 7;
					continue;
				}
				return;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					goto IL_168;
				}
				break;
			case 3:
				goto IL_163;
			case 4:
			{
				IChartDataTable dataTable;
				XlsWorkbook parentWorkbook = ((dataTable as ChartDataTableXls).Parent as XlsChart).ParentWorkbook;
				this.ᜀ(A_0, dataTable.TextArea, parentWorkbook, 10.0);
				num = 2;
				continue;
			}
			case 5:
				goto IL_57;
			case 6:
			{
				bool flag;
				if (!flag)
				{
					num = 4;
					continue;
				}
				goto IL_168;
			}
			case 7:
			{
				IChartDataTable dataTable = A_1.DataTable;
				A_0.WriteStartElement(RecordTableEnumerator.b("嬾ᕀ≂❄⭆ⱈ", a_), RecordTableEnumerator.b("圾㕀㝂㕄絆晈摊㹌ⱎ㥐㙒㡔㙖⩘畚㉜⽞Ѡൢᵤ੦ը൪ɬᵮᱰቲŴѶ坸ᑺོ᡾꺀ﺈﲐﾒ몔ꖖꦘꮚꮜ낞슠쮢쒤햦\udda8", a_));
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("䰾⥀ⱂ㉄ཆ♈㥊㝌ൎ㹐⅒ㅔ㉖⭘", a_), dataTable.HasHorzBorder);
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("䰾⥀ⱂ㉄ᅆⱈ㥊㥌ൎ㹐⅒ㅔ㉖⭘", a_), dataTable.HasVertBorder);
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("䰾⥀ⱂ㉄ࡆ㱈㽊⅌♎㽐㙒", a_), dataTable.HasBorders);
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("䰾⥀ⱂ㉄ెⱈ㉊㹌", a_), dataTable.ShowSeriesKeys);
				bool flag = ((sprᮟ)dataTable.TextArea).ᜃ() != ChartParagraphType.Default;
				num = 6;
				continue;
			}
			case 8:
				return;
			case 9:
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				num = 0;
				continue;
			}
			IL_41:
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 5;
				continue;
			}
			num = 9;
			continue;
			goto IL_41;
			IL_168:
			A_0.WriteEndElement();
			num = 8;
		}
		IL_57:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠾㍀⩂ㅄ≆㭈", a_));
		IL_163:
		throw new ArgumentNullException(RecordTableEnumerator.b("尾⥀≂㝄㍆", a_));
	}

	// Token: 0x06005560 RID: 21856 RVA: 0x00367B04 File Offset: 0x00366B04
	private void ᜀ(XmlWriter A_0, object[] A_1, bool A_2)
	{
		int a_ = 10;
		int num = 15;
		for (;;)
		{
			int num2;
			int num3;
			string text;
			string text2;
			string localName;
			switch (num)
			{
			case 0:
				goto IL_13B;
			case 1:
				num = 14;
				continue;
			case 2:
				if (num2 >= num3)
				{
					num = 18;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("〿㙁", a_), RecordTableEnumerator.b("⠿㙁ぃ㙅片敉捋㵍㍏㩑ㅓ㭕㥗⥙牛ㅝၟݡ੣ṥէ٩੫ŭɯάᕳɵ୷呹፻౽궁ﶉﾑ릕ꪗꪙ겛ꢝ辟송첣장\udaa7\udea9", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("⤿♁㱃", a_), num2.ToString());
				A_0.WriteStartElement(RecordTableEnumerator.b("㘿", a_), RecordTableEnumerator.b("⠿㙁ぃ㙅片敉捋㵍㍏㩑ㅓ㭕㥗⥙牛ㅝၟݡ੣ṥէ٩੫ŭɯάᕳɵ୷呹፻౽궁ﶉﾑ릕ꪗꪙ겛ꢝ辟송첣장\udaa7\udea9", a_));
				A_0.WriteString(this.ᜀ(A_1[num2]));
				A_0.WriteEndElement();
				A_0.WriteEndElement();
				num2++;
				num = 5;
				continue;
			case 3:
				text = RecordTableEnumerator.b("㌿㙁㙃Յ⥇⥉⑋⭍", a_);
				goto IL_2CA;
			case 4:
				goto IL_77;
			case 5:
				goto IL_15B;
			case 6:
				if (A_2)
				{
					num = 9;
					continue;
				}
				num = 7;
				continue;
			case 7:
				if (!(A_1[0] is string))
				{
					if (true)
					{
					}
					num = 1;
					continue;
				}
				num = 13;
				continue;
			case 8:
				goto IL_DF;
			case 9:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_19A;
				default:
					if (false)
					{
					}
					num = 10;
					continue;
				}
				break;
			case 10:
				if (!(A_1[0] is string))
				{
					num = 12;
					continue;
				}
				num = 3;
				continue;
			case 11:
				goto IL_DF;
			case 12:
				num = 16;
				continue;
			case 13:
				text2 = RecordTableEnumerator.b("㌿㙁㙃੅ⅇ㹉", a_);
				goto IL_249;
			case 14:
				text2 = RecordTableEnumerator.b("⸿㝁⥃੅ⅇ㹉", a_);
				goto IL_249;
			case 16:
				text = RecordTableEnumerator.b("⸿㝁⥃Յ⥇⥉⑋⭍", a_);
				goto IL_2CA;
			case 17:
				goto IL_15B;
			case 18:
				goto IL_175;
			case 19:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				localName = null;
				goto IL_19A;
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			num = 19;
			continue;
			IL_DF:
			A_0.WriteStartElement(localName, RecordTableEnumerator.b("⠿㙁ぃ㙅片敉捋㵍㍏㩑ㅓ㭕㥗⥙牛ㅝၟݡ੣ṥէ٩੫ŭɯάᕳɵ୷呹፻౽궁ﶉﾑ릕ꪗꪙ겛ꢝ辟송첣장\udaa7\udea9", a_));
			num3 = A_1.Length;
			spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("〿㙁݃⥅㵇⑉㡋", a_), num3.ToString());
			num2 = 0;
			num = 17;
			continue;
			IL_15B:
			num = 2;
			continue;
			IL_19A:
			num = 6;
			continue;
			IL_249:
			localName = text2;
			num = 8;
			continue;
			IL_2CA:
			localName = text;
			num = 11;
		}
		IL_77:
		throw new ArgumentNullException(RecordTableEnumerator.b("㜿ぁⵃ㉅ⵇ㡉", a_));
		IL_13B:
		throw new ArgumentNullException(RecordTableEnumerator.b("㘿⍁⡃㍅ⵇ㥉", a_));
		IL_175:
		A_0.WriteEndElement();
	}

	// Token: 0x06005561 RID: 21857 RVA: 0x00367DF4 File Offset: 0x00366DF4
	private string ᜀ(object A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_5C:
			if (A_0 is double)
			{
				num = 3;
			}
			else
			{
				num = 2;
			}
			break;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			goto IL_52;
		}
		string result;
		for (;;)
		{
			IL_30:
			switch (num)
			{
			case 0:
				return result;
			case 1:
				return result;
			case 2:
				if (A_0 is float)
				{
					num = 4;
					continue;
				}
				result = A_0.ToString();
				num = 6;
				continue;
			case 3:
				result = XmlConvert.ToString((double)A_0);
				num = 0;
				continue;
			case 4:
				result = XmlConvert.ToString((float)A_0);
				num = 1;
				continue;
			case 5:
				goto IL_5C;
			case 6:
				return result;
			}
			goto IL_52;
		}
		return result;
		IL_52:
		result = null;
		num = 5;
		goto IL_30;
	}

	// Token: 0x04002907 RID: 10503
	public const int ᜀ = 8666049;

	// Token: 0x04002908 RID: 10504
	public const int ᜁ = 6293304;

	// Token: 0x02000583 RID: 1411
	// (Invoke) Token: 0x06005564 RID: 21860
	private delegate void ᜀ(XmlWriter A_0, XlsChartSerie A_1);
}
