using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using Spire.Xls.Charts;
using Spire.Xls.Core.Interfaces;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet.Charts;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlReaders;
using Spire.Xls.Core.Spreadsheet.XmlReaders.Shapes;

namespace Spire.Xls.Core.Spreadsheet.XmlSerialization.Charts
{
	// Token: 0x02000218 RID: 536
	public class ChartParser
	{
		// Token: 0x06001F02 RID: 7938 RVA: 0x001064E8 File Offset: 0x001054E8
		public ChartParser(XlsWorkbook book)
		{
			this.ᜁ = book;
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x00106504 File Offset: 0x00105504
		public void ParseChart(XmlReader reader, XlsChart chart, RelationsCollection relations)
		{
			int a_ = 12;
			switch (0)
			{
			default:
			{
				int num = 16;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_5AD;
					case 1:
					{
						string localName;
						int num2;
						if (spr\u22D2.\u1736.TryGetValue(localName, out num2))
						{
							num = 20;
							continue;
						}
						goto IL_22F;
					}
					case 2:
						goto IL_5AD;
					case 3:
						num = 24;
						continue;
					case 4:
						goto IL_5AD;
					case 5:
					{
						int num2;
						switch (num2)
						{
						case 0:
							this.ᜁ(reader, chart, relations);
							num = 27;
							continue;
						case 1:
							chart.HasPlotArea = true;
							chart.PlotArea.IsBorderCornersRound = spr\u1AA0.ᜃ(reader);
							num = 13;
							continue;
						case 2:
						{
							IChartFrameFormat chartArea;
							spr\u1772 a_2 = new spr\u1A7B(chartArea.Border as XlsChartBorder, chartArea.Interior as XlsChartInterior, chartArea.Fill as spr\u1C26, chartArea.Shadow, chartArea.Format3D);
							spr\u1AA0.ᜀ(reader, a_2, chart.ParentWorkbook.DataHolder, relations);
							num = 15;
							continue;
						}
						case 3:
							chart.Style = spr\u1AA0.ᜂ(reader);
							num = 23;
							continue;
						case 4:
							this.ᜂ(reader, chart, relations);
							num = 36;
							continue;
						case 5:
							this.ᜄ(reader, chart);
							num = 21;
							continue;
						case 6:
							this.ᜃ(reader, chart, relations);
							num = 30;
							continue;
						case 7:
							this.ᜇ(reader, chart);
							num = 0;
							continue;
						case 8:
							chart.AlternateContent = ShapeParser.ReadNodeAsStream(reader);
							num = 17;
							continue;
						case 9:
							this.ᜊ(reader, chart);
							num = 2;
							continue;
						default:
							num = 3;
							continue;
						}
						break;
					}
					case 6:
						if (!(reader.LocalName != RecordTableEnumerator.b("⅁ⱃ❅㩇㹉Ὃ㹍ㅏㅑㅓ", a_)))
						{
							num = 31;
							continue;
						}
						num = 26;
						continue;
					case 7:
						goto IL_5AD;
					case 8:
						num = 25;
						continue;
					case 9:
						spr\u22D2.\u1736 = new Dictionary<string, int>(10)
						{
							{
								RecordTableEnumerator.b("⅁ⱃ❅㩇㹉", a_),
								0
							},
							{
								RecordTableEnumerator.b("ぁ⭃㍅♇⹉⥋⩍ፏ㵑♓㡕㵗⡙⽛", a_),
								1
							},
							{
								RecordTableEnumerator.b("ㅁ㑃ᙅ㩇", a_),
								2
							},
							{
								RecordTableEnumerator.b("ㅁぃ㽅⑇⽉", a_),
								3
							},
							{
								RecordTableEnumerator.b("㝁㝃⍅㩇᥉⑋⽍⁏㝑❓", a_),
								4
							},
							{
								RecordTableEnumerator.b("㉁ⵃぅ❇㹉Ὃ⅍╏⁑㝓㍕", a_),
								5
							},
							{
								RecordTableEnumerator.b("㉁㙃⽅♇㹉Ὃ⭍⑏♑㵓㡕㽗⥙", a_),
								6
							},
							{
								RecordTableEnumerator.b("❁㱃㉅ч㥉㡋", a_),
								7
							},
							{
								RecordTableEnumerator.b("́⡃㉅ⵇ㡉≋⽍⑏㝑ᝓ㥕㙗⹙㥛そᑟ", a_),
								8
							},
							{
								RecordTableEnumerator.b("㙁㱃ᙅ㩇", a_),
								9
							}
						};
						num = 35;
						continue;
					case 10:
						num = 19;
						continue;
					case 11:
						goto IL_D6;
					case 12:
						goto IL_39C;
					case 13:
						goto IL_5AD;
					case 14:
						if (spr\u22D2.\u1736 == null)
						{
							num = 9;
							continue;
						}
						goto IL_423;
					case 15:
						goto IL_5AD;
					case 17:
						goto IL_5AD;
					case 18:
						if (reader.NodeType != XmlNodeType.EndElement)
						{
							num = 22;
							continue;
						}
						goto IL_5EE;
					case 19:
					{
						if (reader.LocalName != RecordTableEnumerator.b("⅁ⱃ❅㩇㹉Ὃ㹍ㅏㅑㅓ", a_))
						{
							num = 32;
							continue;
						}
						reader.Read();
						IChartFrameFormat chartArea = chart.ChartArea;
						chartArea.Interior.UseDefaultFormat = true;
						chartArea.Border.UseDefaultFormat = true;
						num = 4;
						continue;
					}
					case 20:
						num = 5;
						continue;
					case 21:
						goto IL_5AD;
					case 22:
						num = 6;
						continue;
					case 23:
						goto IL_5AD;
					case 24:
						goto IL_22F;
					case 25:
					{
						string localName;
						if ((localName = reader.LocalName) != null)
						{
							num = 28;
							continue;
						}
						goto IL_22F;
					}
					case 26:
						if (reader.NodeType == XmlNodeType.Element)
						{
							num = 8;
							continue;
						}
						if (true)
						{
						}
						reader.Skip();
						num = 7;
						continue;
					case 27:
						goto IL_5AD;
					case 28:
						num = 14;
						continue;
					case 29:
						goto IL_5AD;
					case 30:
						goto IL_5AD;
					case 31:
						goto IL_27D;
					case 32:
						goto IL_153;
					case 33:
						if (reader.NodeType == XmlNodeType.Element)
						{
							num = 10;
							continue;
						}
						reader.Read();
						num = 12;
						continue;
					case 34:
						goto IL_5A8;
					case 35:
						goto IL_423;
					case 36:
						goto IL_5AD;
					case 37:
						if (chart == null)
						{
							num = 34;
							continue;
						}
						goto IL_39C;
					}
					if (reader == null)
					{
						num = 11;
						continue;
					}
					num = 37;
					continue;
					IL_22F:
					reader.Read();
					num = 29;
					continue;
					IL_39C:
					num = 33;
					continue;
					IL_423:
					num = 1;
					continue;
					IL_5AD:
					num = 18;
				}
				IL_D6:
				throw new ArgumentNullException(RecordTableEnumerator.b("ぁ⅃❅ⱇ⽉㹋", a_));
				IL_153:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_D6;
				default:
					if (false)
					{
					}
					throw new XmlException(RecordTableEnumerator.b("ᝁ⩃⍅ぇ㩉⥋ⵍ⑏㝑こ癕⁗㝙せ繝ᑟͣ͡䡥", a_));
				}
				IL_27D:
				goto IL_5EE;
				IL_5A8:
				throw new ArgumentNullException(RecordTableEnumerator.b("⅁ⱃ❅㩇㹉", a_));
				IL_5EE:
				chart.DetectIsInRowOnParsing();
				reader.Read();
				return;
			}
			}
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x00106B0C File Offset: 0x00105B0C
		private void ᜊ(XmlReader A_0, XlsChart A_1)
		{
			int a_ = 17;
			int num = 0;
			XmlReader xmlReader;
			for (;;)
			{
				switch (num)
				{
				case 1:
					xmlReader.Read();
					num = 24;
					continue;
				case 2:
					goto IL_E8;
				case 3:
					goto IL_1BA;
				case 4:
					if (A_1 == null)
					{
						num = 2;
						continue;
					}
					A_1.DefaultTextProperty = ShapeParser.ReadNodeAsStream(A_0);
					A_1.DefaultTextProperty.Position = 0L;
					xmlReader = UtilityMethods.ᜀ(A_1.DefaultTextProperty);
					goto IL_21E;
				case 5:
				{
					if (true)
					{
					}
					string localName;
					if (!(localName == RecordTableEnumerator.b("㝆", a_)))
					{
						num = 9;
						continue;
					}
					this.ᜈ(xmlReader, A_1);
					num = 6;
					continue;
				}
				case 6:
					goto IL_1BA;
				case 7:
					if (!xmlReader.IsEmptyElement)
					{
						num = 1;
						continue;
					}
					goto IL_326;
				case 8:
					goto IL_1DA;
				case 9:
					num = 12;
					continue;
				case 10:
					if (xmlReader.LocalName != RecordTableEnumerator.b("㍆ㅈᭊ㽌", a_))
					{
						num = 18;
						continue;
					}
					num = 7;
					continue;
				case 11:
					goto IL_2A1;
				case 12:
					goto IL_19D;
				case 13:
					if (xmlReader.NodeType == XmlNodeType.Element)
					{
						num = 16;
						continue;
					}
					xmlReader.Skip();
					num = 3;
					continue;
				case 14:
					num = 21;
					continue;
				case 15:
					goto IL_1BA;
				case 16:
					num = 20;
					continue;
				case 17:
					goto IL_1BA;
				case 18:
					goto IL_24F;
				case 19:
					num = 5;
					continue;
				case 20:
				{
					string localName;
					if ((localName = xmlReader.LocalName) != null)
					{
						num = 14;
						continue;
					}
					goto IL_19D;
				}
				case 21:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("╆♈⽊㑌὎⍐", a_)))
					{
						num = 19;
						continue;
					}
					this.ᜉ(xmlReader, A_1);
					num = 17;
					continue;
				}
				case 22:
					if (!(A_0.LocalName != RecordTableEnumerator.b("㍆ㅈᭊ㽌", a_)))
					{
						num = 4;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_21E;
					default:
						if (false)
						{
						}
						num = 11;
						continue;
					}
					break;
				case 23:
					goto IL_8F;
				case 24:
					goto IL_1BA;
				case 25:
					if (xmlReader.NodeType == XmlNodeType.EndElement)
					{
						num = 8;
						continue;
					}
					num = 13;
					continue;
				}
				if (A_0 == null)
				{
					num = 23;
					continue;
				}
				num = 22;
				continue;
				IL_19D:
				xmlReader.Skip();
				num = 15;
				continue;
				IL_1BA:
				num = 25;
				continue;
				IL_21E:
				num = 10;
			}
			IL_8F:
			throw new ArgumentNullException(RecordTableEnumerator.b("㕆ⱈ⩊⥌⩎⍐", a_));
			IL_E8:
			throw new ArgumentNullException(RecordTableEnumerator.b("⑆ⅈ⩊㽌㭎", a_));
			IL_1DA:
			goto IL_326;
			IL_24F:
			throw new XmlException();
			IL_2A1:
			throw new XmlException(RecordTableEnumerator.b("ቆ❈⹊㕌㽎㑐げ⅔㉖㵘筚⥜㹞٠䍢୤٦Ѩ๪", a_));
			IL_326:
			xmlReader.Read();
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x00106E48 File Offset: 0x00105E48
		private void ᜉ(XmlReader A_0, XlsChart A_1)
		{
			int a_ = 7;
			int num = 3;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_9B;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_58;
					case 1:
						if (A_0.LocalName != RecordTableEnumerator.b("弼倾╀㩂ᕄ㕆", a_))
						{
							num = 2;
							continue;
						}
						goto IL_E2;
					case 2:
						goto IL_85;
					case 4:
						goto IL_E0;
					case 5:
						if (true)
						{
						}
						if (A_1 == null)
						{
							num = 4;
							continue;
						}
						num = 1;
						continue;
					}
					if (A_0 == null)
					{
						num = 0;
					}
					else
					{
						num = 5;
					}
					break;
				}
			}
			IL_58:
			goto IL_9B;
			IL_85:
			throw new XmlException();
			IL_9B:
			throw new ArgumentNullException(RecordTableEnumerator.b("似娾⁀❂⁄㕆", a_));
			IL_E0:
			throw new ArgumentNullException(RecordTableEnumerator.b("帼圾⁀ㅂㅄ", a_));
			IL_E2:
			A_0.MoveToElement();
			A_0.Skip();
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x00106F44 File Offset: 0x00105F44
		private void ᜈ(XmlReader A_0, XlsChart A_1)
		{
			int a_ = 3;
			for (;;)
			{
				sprᡟ sprᡟ = A_1.DataHolder;
				sprវ sprវ = sprᡟ.ᜋ();
				int num = 7;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						goto IL_C9;
					case 2:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_C9;
						default:
						{
							if (false)
							{
							}
							TextSettings a_2 = spr\u1AA0.ᜃ(A_0, sprវ.\u1718());
							spr\u1AA0.ᜀ(A_1.Font, a_2);
							this.ᜀ(A_1);
							num = 1;
							continue;
						}
						}
						break;
					case 3:
						if (A_0.NodeType == XmlNodeType.EndElement)
						{
							num = 0;
							continue;
						}
						goto IL_CB;
					case 4:
						goto IL_55;
					case 5:
						num = 3;
						continue;
					case 6:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 10;
							continue;
						}
						goto IL_156;
					case 7:
						goto IL_55;
					case 8:
						if (A_0.LocalName == RecordTableEnumerator.b("䤸", a_))
						{
							num = 5;
							continue;
						}
						goto IL_CB;
					case 9:
						if (A_0.LocalName == RecordTableEnumerator.b("崸帺嬼派ᅀㅂ", a_))
						{
							num = 2;
							continue;
						}
						goto IL_156;
					case 10:
						num = 9;
						continue;
					}
					break;
					IL_55:
					num = 8;
					continue;
					IL_CB:
					num = 6;
					continue;
					IL_156:
					A_0.Read();
					num = 4;
					continue;
					IL_C9:
					goto IL_55;
				}
			}
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x001070C0 File Offset: 0x001060C0
		private void ᜀ(XlsChart A_0)
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
				using (List<XlsChartAxis>.Enumerator enumerator = new List<XlsChartAxis>
				{
					(XlsChartAxis)A_0.PrimaryCategoryAxis,
					(XlsChartAxis)A_0.PrimaryValueAxis,
					(XlsChartAxis)A_0.SecondaryValueAxis,
					(XlsChartAxis)A_0.SecondaryCategoryAxis
				}.GetEnumerator())
				{
					int num = 8;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							XlsChartAxis xlsChartAxis;
							if (xlsChartAxis.IsDefaultTextSettings)
							{
								num = 5;
								continue;
							}
							break;
						}
						case 1:
						{
							if (!enumerator.MoveNext())
							{
								num = 2;
								continue;
							}
							XlsChartAxis xlsChartAxis = enumerator.Current;
							num = 7;
							continue;
						}
						case 2:
							num = 3;
							continue;
						case 3:
							goto IL_149;
						case 4:
							num = 0;
							continue;
						case 5:
						{
							XlsChartAxis xlsChartAxis;
							xlsChartAxis.IsChartFont = true;
							xlsChartAxis.Font = (IFont)A_0.Font.Clone(A_0);
							xlsChartAxis.IsChartFont = false;
							num = 6;
							continue;
						}
						case 7:
						{
							if (true)
							{
							}
							XlsChartAxis xlsChartAxis;
							if (xlsChartAxis != null)
							{
								num = 4;
								continue;
							}
							break;
						}
						}
						IL_D4:
						num = 1;
						continue;
						goto IL_D4;
					}
					IL_149:;
				}
				break;
			}
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x00107238 File Offset: 0x00106238
		private void ᜇ(XmlReader A_0, XlsChart A_1)
		{
			int a_ = 16;
			int num = 15;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_1 == null)
					{
						goto IL_BC;
					}
					num = 4;
					continue;
				case 1:
					num = 14;
					continue;
				case 2:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 1;
						continue;
					}
					goto IL_123;
				case 3:
					goto IL_6E;
				case 4:
					if (A_0.IsEmptyElement)
					{
						num = 9;
						continue;
					}
					A_0.Read();
					num = 5;
					continue;
				case 5:
					if (true)
					{
					}
					goto IL_123;
				case 6:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 7;
						continue;
					}
					num = 2;
					continue;
				case 7:
					goto IL_143;
				case 8:
					this.ᜆ(A_0, A_1);
					num = 13;
					continue;
				case 9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_BC;
					default:
						goto IL_15E;
					}
					break;
				case 10:
					num = 12;
					continue;
				case 11:
					goto IL_C7;
				case 12:
				{
					string localName;
					if (localName == RecordTableEnumerator.b("⍅ぇ㹉", a_))
					{
						num = 8;
						continue;
					}
					goto IL_123;
				}
				case 13:
					goto IL_123;
				case 14:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 10;
						continue;
					}
					goto IL_123;
				}
				}
				if (A_0 == null)
				{
					num = 3;
					continue;
				}
				num = 0;
				continue;
				IL_BC:
				num = 11;
				continue;
				IL_123:
				num = 6;
			}
			IL_6E:
			throw new ArgumentNullException(RecordTableEnumerator.b("㑅ⵇ⭉⡋⭍≏", a_));
			IL_C7:
			throw new ArgumentNullException(RecordTableEnumerator.b("╅⁇⭉㹋㩍", a_));
			IL_143:
			A_0.Read();
			return;
			IL_15E:
			if (false)
			{
			}
			A_0.Read();
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x00107414 File Offset: 0x00106414
		private void ᜆ(XmlReader A_0, XlsChart A_1)
		{
			int a_ = 7;
			int num = 2;
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
						goto IL_EB;
					case 1:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 11;
							continue;
						}
						goto IL_150;
					case 3:
						if (A_1 == null)
						{
							num = 0;
							continue;
						}
						A_0.Read();
						num = 10;
						continue;
					case 4:
						goto IL_78;
					case 5:
						num = 7;
						continue;
					case 6:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num = 5;
							continue;
						}
						goto IL_150;
					}
					case 7:
					{
						string localName;
						if (localName == RecordTableEnumerator.b("䴼嘾㝀ⱂㅄࡆ㥈㽊⑌⁎㽐⁒", a_))
						{
							num = 12;
							continue;
						}
						goto IL_150;
					}
					case 8:
						goto IL_173;
					case 9:
						goto IL_15B;
					case 10:
						goto IL_150;
					case 11:
						num = 6;
						continue;
					case 12:
						this.ᜅ(A_0, A_1);
						num = 13;
						continue;
					case 13:
						if (true)
						{
						}
						goto IL_150;
					}
					if (A_0 == null)
					{
						num = 4;
						continue;
					}
					num = 3;
					continue;
					IL_150:
					num = 9;
					continue;
				}
				IL_15B:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 8;
				}
				else
				{
					num = 1;
				}
			}
			IL_78:
			throw new ArgumentNullException(RecordTableEnumerator.b("似娾⁀❂⁄㕆", a_));
			IL_EB:
			throw new ArgumentNullException(RecordTableEnumerator.b("帼圾⁀ㅂㅄ", a_));
			IL_173:
			A_0.Read();
		}

		// Token: 0x06001F0A RID: 7946 RVA: 0x001075C8 File Offset: 0x001065C8
		private void ᜅ(XmlReader A_0, XlsChart A_1)
		{
			int a_ = 14;
			int num = 37;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_161;
				case 1:
					A_1.DisplayLegendFieldButtons = true;
					num = 43;
					continue;
				case 2:
					A_1.ShowReportFilterFieldButtons = true;
					num = 21;
					continue;
				case 3:
					num = 7;
					continue;
				case 4:
					if (A_0.LocalName == RecordTableEnumerator.b("㑃⽅㹇╉㡋ō⁏♑㵓㥕㙗⥙", a_))
					{
						num = 42;
						continue;
					}
					return;
				case 5:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 41;
						continue;
					}
					A_0.Read();
					num = 25;
					continue;
				case 6:
					goto IL_1BF;
				case 7:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("⁃㑅❇㩉ᙋ⅍㹏㝑ၓ㝕ⱗ㭙", a_)))
					{
						goto IL_319;
					}
					num = 17;
					continue;
				}
				case 8:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 31;
						continue;
					}
					goto IL_161;
				}
				case 9:
					goto IL_1BF;
				case 10:
					goto IL_1BF;
				case 11:
					num = 0;
					continue;
				case 12:
					goto IL_1BF;
				case 13:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 19;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_319;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 14:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㉃❅⑇", a_)))
					{
						num = 2;
						continue;
					}
					goto IL_1BF;
				case 15:
					num = 29;
					continue;
				case 16:
					if (A_0.LocalName != RecordTableEnumerator.b("㑃⽅㹇╉㡋ō⁏♑㵓㥕㙗⥙", a_))
					{
						num = 22;
						continue;
					}
					A_0.Read();
					num = 10;
					continue;
				case 17:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㉃❅⑇", a_)))
					{
						num = 36;
						continue;
					}
					goto IL_1BF;
				case 18:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("⁃㑅❇㩉ᙋ⅍㹏㝑ᝓ㝕ⱗ㽙㭛ㅝ቟ୡţᕥ", a_)))
					{
						num = 3;
						continue;
					}
					num = 23;
					continue;
				}
				case 19:
					num = 4;
					continue;
				case 20:
					goto IL_251;
				case 21:
					goto IL_1BF;
				case 22:
					goto IL_1A6;
				case 23:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㉃❅⑇", a_)))
					{
						num = 35;
						continue;
					}
					goto IL_1BF;
				case 24:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㉃❅⑇", a_)))
					{
						num = 40;
						continue;
					}
					goto IL_1BF;
				case 25:
					goto IL_1BF;
				case 26:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㉃❅⑇", a_)))
					{
						num = 1;
						continue;
					}
					goto IL_1BF;
				case 27:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("⁃㑅❇㩉ᙋ⅍㹏㝑❓Uㅗ⥙㕛㱝౟ݡ", a_)))
					{
						num = 11;
						continue;
					}
					num = 24;
					continue;
				}
				case 28:
					goto IL_4B6;
				case 29:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("⁃㑅❇㩉ᙋ⅍㹏㝑ቓ㽕㑗⹙㥛ⱝ", a_)))
					{
						num = 38;
						continue;
					}
					num = 14;
					continue;
				}
				case 30:
					goto IL_1BF;
				case 31:
					num = 18;
					continue;
				case 32:
					goto IL_DD;
				case 33:
					num = 27;
					continue;
				case 34:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("⁃㑅❇㩉ᙋ⅍㹏㝑ݓ㍕⩗㍙㥛ⵝ", a_)))
					{
						num = 33;
						continue;
					}
					num = 26;
					continue;
				}
				case 35:
					A_1.DisplayAxisFieldButtons = true;
					num = 9;
					continue;
				case 36:
					A_1.DisplayValueFieldButtons = true;
					num = 6;
					continue;
				case 38:
					num = 34;
					continue;
				case 39:
					if (A_1 == null)
					{
						num = 28;
						continue;
					}
					num = 16;
					continue;
				case 40:
					A_1.DisplayEntireFieldButtons = true;
					num = 12;
					continue;
				case 41:
					num = 8;
					continue;
				case 42:
					A_0.Read();
					num = 20;
					continue;
				case 43:
					goto IL_1BF;
				}
				if (A_0 == null)
				{
					num = 32;
					continue;
				}
				num = 39;
				continue;
				IL_161:
				A_0.Read();
				num = 30;
				continue;
				IL_1BF:
				if (true)
				{
				}
				num = 13;
				continue;
				IL_319:
				num = 15;
			}
			IL_DD:
			throw new ArgumentNullException(RecordTableEnumerator.b("㙃⍅⥇⹉⥋㱍", a_));
			IL_1A6:
			throw new XmlException(RecordTableEnumerator.b("ᅃ⡅ⵇ㉉㱋⭍㍏♑ㅓ㉕硗⹙㵛㥝䁟ౡգ୥൧", a_));
			IL_251:
			return;
			IL_4B6:
			throw new ArgumentNullException(RecordTableEnumerator.b("❃⹅⥇㡉㡋", a_));
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x00107AFC File Offset: 0x00106AFC
		private void ᜃ(XmlReader A_0, XlsChart A_1, RelationsCollection A_2)
		{
			int a_ = 15;
			int num = 6;
			for (;;)
			{
				string localName;
				switch (num)
				{
				case 0:
					goto IL_149;
				case 1:
				{
					if (!(localName == RecordTableEnumerator.b("㕄♆⹈⹊L⹎⍐㑒㱔㥖⩘", a_)))
					{
						num = 8;
						continue;
					}
					IPageSetupBase pageSetup;
					spr\u1A61 a_2;
					spr\u2306.ᜀ(A_0, pageSetup, a_2);
					num = 12;
					continue;
				}
				case 2:
					if (A_0.IsEmptyElement)
					{
						return;
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
						num = 15;
						continue;
					}
					break;
				case 3:
					goto IL_149;
				case 4:
					goto IL_149;
				case 5:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 19;
						continue;
					}
					A_0.Skip();
					num = 32;
					continue;
				case 7:
					goto IL_109;
				case 8:
					num = 27;
					continue;
				case 9:
					goto IL_149;
				case 10:
					num = 24;
					continue;
				case 11:
					num = 20;
					continue;
				case 12:
					goto IL_149;
				case 13:
					if ((localName = A_0.LocalName) != null)
					{
						num = 25;
						continue;
					}
					goto IL_109;
				case 14:
					goto IL_2AF;
				case 15:
				{
					A_0.Read();
					IPageSetupBase pageSetup = A_1.PageSetup;
					num = 33;
					continue;
				}
				case 16:
					A_0.Read();
					num = 21;
					continue;
				case 17:
				{
					if (A_0.LocalName != RecordTableEnumerator.b("㕄㕆⁈╊㥌ᱎ㑐❒⅔㹖㝘㱚⹜", a_))
					{
						num = 14;
						continue;
					}
					spr\u1A61 a_2 = new spr\u1A61();
					num = 2;
					continue;
				}
				case 18:
					num = 7;
					continue;
				case 19:
					num = 13;
					continue;
				case 20:
					goto IL_DA;
				case 21:
					goto IL_21C;
				case 22:
				{
					if (true)
					{
					}
					if (!(localName == RecordTableEnumerator.b("㕄㕆⁈╊㥌N⅐❒㱔㡖㝘⡚", a_)))
					{
						num = 29;
						continue;
					}
					IPageSetupBase pageSetup;
					spr\u2306.ᜀ(A_0, pageSetup);
					num = 9;
					continue;
				}
				case 23:
					goto IL_39D;
				case 24:
				{
					if (!(localName == RecordTableEnumerator.b("ⵄ≆⡈⽊⡌㵎ᝐ㱒㩔⍖㱘⥚", a_)))
					{
						num = 11;
						continue;
					}
					IPageSetupBase pageSetup;
					spr\u2306.ᜀ(A_0, (XlsPageSetupBase)pageSetup);
					num = 31;
					continue;
				}
				case 25:
					num = 22;
					continue;
				case 26:
					goto IL_B5;
				case 27:
				{
					if (!(localName == RecordTableEnumerator.b("㕄♆⹈⹊Ṍ⩎═♒╔", a_)))
					{
						num = 10;
						continue;
					}
					IPageSetupBase pageSetup;
					spr\u2306.ᜁ(A_0, (XlsPageSetupBase)pageSetup);
					num = 0;
					continue;
				}
				case 28:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 16;
						continue;
					}
					num = 5;
					continue;
				case 29:
					num = 1;
					continue;
				case 30:
					if (A_1 == null)
					{
						num = 23;
						continue;
					}
					num = 17;
					continue;
				case 31:
					goto IL_149;
				case 32:
					goto IL_149;
				case 33:
					goto IL_149;
				}
				if (A_0 == null)
				{
					num = 26;
					continue;
				}
				num = 30;
				continue;
				IL_DA:
				if (!(localName == RecordTableEnumerator.b("⥄≆⹈⩊⹌㙎ᕐ⅒㑔⁖じ㕚㩜᝞❠", a_)))
				{
					num = 18;
					continue;
				}
				spr\u2306.ᜀ(A_0, A_1, A_2);
				num = 3;
				continue;
				IL_109:
				A_0.Skip();
				num = 4;
				continue;
				IL_149:
				num = 28;
			}
			IL_B5:
			throw new ArgumentNullException(RecordTableEnumerator.b("㝄≆⡈⽊⡌㵎", a_));
			IL_21C:
			return;
			IL_2AF:
			throw new XmlException();
			IL_39D:
			throw new ArgumentNullException(RecordTableEnumerator.b("♄⽆⡈㥊㥌", a_));
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x00107EFC File Offset: 0x00106EFC
		private void ᜄ(XmlReader A_0, XlsChart A_1)
		{
			int a_ = 0;
			int num = 13;
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
					num = 7;
					continue;
				case 1:
					goto IL_167;
				case 2:
					if (A_0.LocalName != RecordTableEnumerator.b("䘵儷䰹医䨽ጿⵁㅃ㑅⭇⽉", a_))
					{
						num = 16;
						continue;
					}
					A_0.Read();
					num = 17;
					continue;
				case 3:
					goto IL_7F;
				case 4:
					goto IL_167;
				case 5:
					if (A_1 == null)
					{
						num = 14;
						continue;
					}
					num = 2;
					continue;
				case 6:
					goto IL_18A;
				case 7:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 15;
						continue;
					}
					A_0.Skip();
					num = 4;
					continue;
				case 8:
					goto IL_167;
				case 9:
				{
					string text;
					if (text != null)
					{
						num = 11;
						continue;
					}
					goto IL_167;
				}
				case 10:
				{
					string localName;
					if (localName == RecordTableEnumerator.b("堵夷圹夻", a_))
					{
						goto IL_223;
					}
					goto IL_FC;
				}
				case 11:
					A_1.DisplayEntireFieldButtons = false;
					A_1.DisplayAxisFieldButtons = false;
					A_1.DisplayLegendFieldButtons = false;
					A_1.ShowReportFilterFieldButtons = false;
					A_1.DisplayValueFieldButtons = false;
					num = 1;
					continue;
				case 12:
				{
					string text = A_0.ReadElementContentAsString();
					A_1.PivotTable = this.ᜀ(A_1.Workbook, text);
					A_1.PreservedPivotSource = text;
					num = 9;
					continue;
				}
				case 14:
					goto IL_12B;
				case 15:
					num = 18;
					continue;
				case 16:
					goto IL_1E7;
				case 17:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_223;
					default:
						if (false)
						{
						}
						goto IL_167;
					}
					break;
				case 18:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 19;
						continue;
					}
					goto IL_FC;
				}
				case 19:
					num = 10;
					continue;
				}
				if (true)
				{
				}
				if (A_0 == null)
				{
					num = 3;
					continue;
				}
				num = 5;
				continue;
				IL_FC:
				A_0.Skip();
				num = 8;
				continue;
				IL_167:
				num = 0;
				continue;
				IL_223:
				num = 12;
			}
			IL_7F:
			throw new ArgumentNullException(RecordTableEnumerator.b("䐵崷嬹堻嬽㈿", a_));
			IL_12B:
			throw new ArgumentNullException(RecordTableEnumerator.b("唵倷嬹主䨽", a_));
			IL_18A:
			A_0.Read();
			return;
			IL_1E7:
			throw new XmlException(RecordTableEnumerator.b("挵嘷弹䐻丽┿⅁ぃ⍅ⱇ橉㑋⍍㱏牑⁓㝕㽗", a_));
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x001081A0 File Offset: 0x001071A0
		private PivotTable ᜀ(IWorkbook A_0, string A_1)
		{
			switch (0)
			{
			default:
			{
				string name;
				int num3;
				for (;;)
				{
					IL_4D:
					if (true)
					{
					}
					int num = A_1.LastIndexOf('!');
					int num2 = 2;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4D;
						}
						if (false)
						{
						}
						switch (num2)
						{
						case 0:
							goto IL_B1;
						case 1:
							goto IL_74;
						case 2:
							if (num < 0)
							{
								num2 = 1;
								continue;
							}
							name = A_1.Substring(num + 1);
							A_1 = A_1.Substring(0, num);
							num3 = A_1.IndexOf(']');
							num2 = 3;
							continue;
						case 3:
							if (num3 < 0)
							{
								num2 = 0;
								continue;
							}
							goto IL_B5;
						}
						break;
					}
				}
				IL_74:
				return null;
				IL_B1:
				return null;
				IL_B5:
				string sheetName = A_1.Substring(num3 + 1);
				return (PivotTable)A_0.Worksheets[sheetName].PivotTables[name];
			}
			}
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x00108288 File Offset: 0x00107288
		private void ᜂ(XmlReader A_0, XlsChart A_1, RelationsCollection A_2)
		{
			int a_ = 1;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			A_0.MoveToAttribute(RecordTableEnumerator.b("帶崸", a_), RecordTableEnumerator.b("弶䴸伺䴼Ծ湀求㙄⑆ⅈ⹊⁌⹎≐絒㩔❖㱘㕚╜㉞ൠբ੤ᕦѨ੪ᥬᱮ彰ᱲݴၶ噸ᑺ᭼᥾쎆ﶒ뢖ꮘꮚ궜ꦞ躠톢삤쮦좨\udfaa쒬삮\udfb0삲\uddb4\udeb6즸좺", a_));
			string value = A_0.Value;
			Dictionary<string, object> a_2 = new Dictionary<string, object>();
			sprᦨ a_3 = A_2[value];
			A_1.DataHolder.ᜀ(A_1, a_3, a_2);
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x00108314 File Offset: 0x00107314
		private void ᜁ(XmlReader A_0, XlsChart A_1, RelationsCollection A_2)
		{
			int a_ = 19;
			switch (0)
			{
			default:
			{
				int num = 7;
				spr\u2272 a_2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_1CA;
					case 1:
						goto IL_1CA;
					case 2:
					{
						if (A_0.LocalName != RecordTableEnumerator.b("⩈⍊ⱌ㵎═", a_))
						{
							num = 20;
							continue;
						}
						if (true)
						{
						}
						A_0.Read();
						sprᡟ sprᡟ = A_1.DataHolder;
						sprវ sprវ = sprᡟ.ᜋ();
						a_2 = null;
						num = 19;
						continue;
					}
					case 3:
						goto IL_BA;
					case 4:
						num = 11;
						continue;
					case 5:
						if (A_1 == null)
						{
							num = 21;
							continue;
						}
						num = 2;
						continue;
					case 6:
						goto IL_1CA;
					case 8:
						goto IL_1CA;
					case 9:
						goto IL_1EF;
					case 10:
						goto IL_44E;
					case 11:
					{
						string localName;
						if ((localName = A_0.LocalName) == null)
						{
							goto IL_1B6;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_44E;
						default:
							if (false)
							{
							}
							num = 17;
							continue;
						}
						break;
					}
					case 12:
						if (A_0.NodeType == XmlNodeType.EndElement)
						{
							num = 9;
							continue;
						}
						num = 14;
						continue;
					case 13:
						goto IL_1B6;
					case 14:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 4;
							continue;
						}
						A_0.Skip();
						num = 1;
						continue;
					case 15:
					{
						int num2;
						switch (num2)
						{
						case 0:
							a_2 = this.ᜂ(A_0, A_1);
							num = 26;
							continue;
						case 1:
						{
							sprវ sprវ;
							this.ᜂ(A_0, A_1, A_2, sprវ.\u1718());
							num = 10;
							continue;
						}
						case 2:
							A_1.HasLegend = true;
							this.ᜀ(A_0, A_1.Legend, A_1, A_2);
							num = 8;
							continue;
						case 3:
						{
							sprវ sprវ;
							this.ᜀ(A_0, A_1.Floor, sprវ, A_2);
							num = 28;
							continue;
						}
						case 4:
						{
							sprវ sprវ;
							this.ᜀ(A_0, A_1.Walls, sprវ, A_2);
							num = 25;
							continue;
						}
						case 5:
						{
							sprវ sprវ;
							this.ᜀ(A_0, A_1.Walls, sprវ, A_2);
							num = 23;
							continue;
						}
						case 6:
						{
							sprᮟ a_3 = A_1.ChartTitleArea as sprᮟ;
							spr\u1AA0.ᜀ(this.ᜁ);
							sprវ sprវ;
							spr\u1AA0.ᜀ(A_0, a_3, sprវ, A_2, new float?(18f));
							num = 0;
							continue;
						}
						case 7:
							this.ᜃ(A_0, A_1);
							num = 24;
							continue;
						default:
							num = 30;
							continue;
						}
						break;
					}
					case 16:
					{
						string localName;
						int num2;
						if (spr\u22D2.\u1737.TryGetValue(localName, out num2))
						{
							num = 18;
							continue;
						}
						goto IL_1B6;
					}
					case 17:
						num = 27;
						continue;
					case 18:
						num = 15;
						continue;
					case 19:
						goto IL_1CA;
					case 20:
						goto IL_28D;
					case 21:
						goto IL_471;
					case 22:
						spr\u22D2.\u1737 = new Dictionary<string, int>(8)
						{
							{
								RecordTableEnumerator.b("㽈≊⡌㡎扐ᝒ", a_),
								0
							},
							{
								RecordTableEnumerator.b("㥈❊≌㭎ၐ⅒ご㙖", a_),
								1
							},
							{
								RecordTableEnumerator.b("╈⹊⩌⩎㽐㝒", a_),
								2
							},
							{
								RecordTableEnumerator.b("⽈❊≌⁎⍐", a_),
								3
							},
							{
								RecordTableEnumerator.b("㩈≊⥌⩎ِ㉒㥔㭖", a_),
								4
							},
							{
								RecordTableEnumerator.b("⭈⩊⹌⑎ِ㉒㥔㭖", a_),
								5
							},
							{
								RecordTableEnumerator.b("㵈≊㥌⍎㑐", a_),
								6
							},
							{
								RecordTableEnumerator.b("㥈≊㭌⁎═ᕒ㡔⍖⩘", a_),
								7
							}
						};
						num = 29;
						continue;
					case 23:
						goto IL_1CA;
					case 24:
						goto IL_1CA;
					case 25:
						goto IL_1CA;
					case 26:
						goto IL_1CA;
					case 27:
						if (spr\u22D2.\u1737 == null)
						{
							num = 22;
							continue;
						}
						goto IL_33B;
					case 28:
						goto IL_1CA;
					case 29:
						goto IL_33B;
					case 30:
						num = 13;
						continue;
					}
					if (A_0 == null)
					{
						num = 3;
						continue;
					}
					num = 5;
					continue;
					IL_1B6:
					A_0.Skip();
					num = 6;
					continue;
					IL_1CA:
					num = 12;
					continue;
					IL_33B:
					num = 16;
					continue;
					IL_44E:
					goto IL_1CA;
				}
				IL_BA:
				throw new ArgumentNullException(RecordTableEnumerator.b("㭈⹊ⱌ⭎㑐⅒", a_));
				IL_1EF:
				A_0.Read();
				this.ᜀ(A_1, a_2);
				return;
				IL_28D:
				throw new XmlException(RecordTableEnumerator.b("᱈╊⡌㝎⅐㙒㙔⍖㱘㽚絜❞ౠར䕤፦ࡨ౪䍬", a_));
				IL_471:
				throw new ArgumentNullException(RecordTableEnumerator.b("⩈⍊ⱌ㵎═", a_));
			}
			}
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x0010880C File Offset: 0x0010780C
		private void ᜃ(XmlReader A_0, XlsChart A_1)
		{
			int a_ = 12;
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
						goto IL_40;
					default:
						if (false)
						{
						}
						if (A_1 == null)
						{
							num = 1;
							continue;
						}
						goto IL_A1;
					}
					break;
				case 1:
					goto IL_8B;
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
			throw new ArgumentNullException(RecordTableEnumerator.b("ぁ⅃❅ⱇ⽉㹋", a_));
			IL_40:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("⅁ⱃ❅㩇㹉", a_));
			IL_8B:
			goto IL_40;
			IL_A1:
			A_1.PivotFormatsStream = ShapeParser.ReadNodeAsStream(A_0);
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x001088C8 File Offset: 0x001078C8
		private void ᜀ(XlsChart A_0, spr\u2272 A_1)
		{
			int a_ = 17;
			int num = 4;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_107;
				case 1:
					A_0.Elevation = (int)A_1.ᜃ();
					num = 0;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_11F;
					default:
						if (false)
						{
						}
						if (!A_1.ᜂ())
						{
							num = 1;
							continue;
						}
						goto IL_107;
					}
					break;
				case 3:
					if (!spr\u2272.ᜁ(A_1, null))
					{
						num = 11;
						continue;
					}
					return;
				case 5:
					if (A_0.Series.Count == 0)
					{
						num = 8;
						continue;
					}
					num = 2;
					continue;
				case 6:
					goto IL_EE;
				case 7:
					if (!A_1.ᜆ())
					{
						num = 10;
						continue;
					}
					goto IL_172;
				case 8:
					goto IL_170;
				case 9:
					goto IL_5C;
				case 10:
					A_0.Rotation = (int)A_1.ᜌ();
					num = 6;
					continue;
				case 11:
					num = 5;
					continue;
				}
				if (A_0 == null)
				{
					num = 9;
					continue;
				}
				num = 3;
				continue;
				IL_11F:
				num = 7;
				continue;
				IL_107:
				A_0.AutoScaling = A_1.ᜉ();
				A_0.HeightPercent = (int)A_1.ᜀ();
				goto IL_11F;
			}
			IL_5C:
			throw new ArgumentNullException(RecordTableEnumerator.b("⑆ⅈ⩊㽌㭎", a_));
			IL_EE:
			goto IL_172;
			IL_170:
			return;
			IL_172:
			A_0.DepthPercent = (int)A_1.ᜈ();
			A_0.RightAngleAxes = A_1.ᜊ();
			A_0.Perspective = (int)A_1.ᜅ();
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x00108A6C File Offset: 0x00107A6C
		private void ᜀ(XmlReader A_0, IChartLegend A_1, XlsChart A_2, RelationsCollection A_3)
		{
			int a_ = 3;
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
						goto IL_23B;
					case 1:
						goto IL_19A;
					case 2:
						goto IL_23B;
					case 3:
						goto IL_BA;
					case 4:
					{
						if (A_0.LocalName != RecordTableEnumerator.b("唸帺娼娾⽀❂", a_))
						{
							num = 23;
							continue;
						}
						bool isEmptyElement = A_0.IsEmptyElement;
						A_0.Read();
						spr\u2306 a_2 = A_2.ParentWorkbook.DataHolder.\u1718();
						A_1.TextArea.FontName = RecordTableEnumerator.b("稸娺儼嘾⍀ㅂⱄ", a_);
						num = 15;
						continue;
					}
					case 5:
						if (true)
						{
						}
						if (spr\u22D2.\u1738 == null)
						{
							num = 25;
							continue;
						}
						goto IL_19A;
					case 6:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 30;
							continue;
						}
						A_0.Skip();
						num = 28;
						continue;
					case 7:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num = 12;
							continue;
						}
						goto IL_227;
					}
					case 8:
					{
						int num2;
						switch (num2)
						{
						case 0:
						{
							string value = spr\u1AA0.ᜄ(A_0);
							A_1.Position = (LegendPositionType)((XLSXLegendPosition)Enum.Parse(typeof(XLSXLegendPosition), value, false));
							num = 24;
							continue;
						}
						case 1:
						{
							spr\u2306 a_2;
							this.ᜀ(A_0, A_1, a_2);
							num = 0;
							continue;
						}
						case 2:
						{
							sprᮟ a_3 = A_1.TextArea as sprᮟ;
							spr\u2306 a_2;
							this.ᜀ(A_0, a_3, a_2);
							num = 27;
							continue;
						}
						case 3:
						{
							IChartFrameFormat frameFormat = (A_1 as XlsChartLegend).FrameFormat;
							spr\u1A7B a_4 = new spr\u1A7B(frameFormat.Border as XlsChartBorder, frameFormat.Interior as XlsChartInterior, frameFormat.Fill as spr\u1C26, frameFormat.Shadow, frameFormat.Format3D);
							sprវ a_5 = A_2.ParentWorkbook.DataHolder;
							spr\u1AA0.ᜀ(A_0, a_4, a_5, A_3);
							num = 17;
							continue;
						}
						case 4:
						{
							Stream a_6 = ShapeParser.ReadNodeAsStream(A_0);
							(A_1 as XlsChartLegend).LayoutStream = a_6;
							num = 2;
							continue;
						}
						case 5:
							A_1.IncludeInLayout = !spr\u1AA0.ᜃ(A_0);
							num = 9;
							continue;
						default:
							num = 19;
							continue;
						}
						break;
					}
					case 9:
						goto IL_23B;
					case 11:
						goto IL_23B;
					case 12:
						num = 5;
						continue;
					case 13:
						num = 8;
						continue;
					case 14:
						goto IL_260;
					case 15:
					{
						bool isEmptyElement;
						if (!isEmptyElement)
						{
							num = 21;
							continue;
						}
						goto IL_507;
					}
					case 16:
						if (A_0.NodeType == XmlNodeType.EndElement)
						{
							num = 14;
							continue;
						}
						num = 6;
						continue;
					case 17:
						goto IL_23B;
					case 18:
					{
						string localName;
						int num2;
						if (spr\u22D2.\u1738.TryGetValue(localName, out num2))
						{
							num = 13;
							continue;
						}
						goto IL_227;
					}
					case 19:
						num = 29;
						continue;
					case 20:
						goto IL_492;
					case 21:
						num = 26;
						continue;
					case 22:
						if (A_1 == null)
						{
							num = 20;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_195;
						default:
							if (false)
							{
							}
							num = 4;
							continue;
						}
						break;
					case 23:
						goto IL_300;
					case 24:
						goto IL_23B;
					case 25:
						spr\u22D2.\u1738 = new Dictionary<string, int>(6)
						{
							{
								RecordTableEnumerator.b("唸帺娼娾⽀❂ᕄ⡆㩈", a_),
								0
							},
							{
								RecordTableEnumerator.b("唸帺娼娾⽀❂D⥆㵈㥊㑌", a_),
								1
							},
							{
								RecordTableEnumerator.b("䴸䌺洼䴾", a_),
								2
							},
							{
								RecordTableEnumerator.b("䨸䬺洼䴾", a_),
								3
							},
							{
								RecordTableEnumerator.b("唸娺䐼倾㑀㝂", a_),
								4
							},
							{
								RecordTableEnumerator.b("嘸䴺堼䴾ⵀ≂㱄", a_),
								5
							}
						};
						num = 1;
						continue;
					case 26:
						goto IL_23B;
					case 27:
						goto IL_23B;
					case 28:
						goto IL_195;
					case 29:
						goto IL_227;
					case 30:
						num = 7;
						continue;
					}
					if (A_0 == null)
					{
						num = 3;
						continue;
					}
					num = 22;
					continue;
					IL_19A:
					num = 18;
					continue;
					IL_227:
					A_0.Skip();
					num = 11;
					continue;
					IL_23B:
					num = 16;
					continue;
					IL_195:
					goto IL_23B;
				}
				IL_BA:
				throw new ArgumentNullException(RecordTableEnumerator.b("䬸帺尼嬾⑀ㅂ", a_));
				IL_260:
				goto IL_507;
				IL_300:
				throw new XmlException(RecordTableEnumerator.b("永唺堼䜾ㅀ♂♄㍆ⱈ⽊浌㝎㱐㽒畔⍖㡘㱚獜", a_));
				IL_492:
				throw new ArgumentNullException(RecordTableEnumerator.b("唸帺娼娾⽀❂", a_));
				IL_507:
				A_0.Read();
				return;
			}
			}
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x00108F88 File Offset: 0x00107F88
		private void ᜀ(XmlReader A_0, IChartLegend A_1, spr\u2306 A_2)
		{
			int a_ = 10;
			switch (0)
			{
			default:
			{
				int num = 3;
				for (;;)
				{
					int iIndex;
					switch (num)
					{
					case 0:
						goto IL_232;
					case 1:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 18;
							continue;
						}
						A_0.Skip();
						num = 5;
						continue;
					case 2:
						goto IL_232;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2EF;
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
							num = 13;
							continue;
						}
						num = 21;
						continue;
					case 5:
						goto IL_232;
					case 6:
						if (A_0.NodeType == XmlNodeType.EndElement)
						{
							num = 23;
							continue;
						}
						num = 1;
						continue;
					case 7:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("␿❁⡃⍅㱇⽉", a_)))
						{
							num = 14;
							continue;
						}
						goto IL_2EF;
					}
					case 8:
						num = 17;
						continue;
					case 9:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("㐿㩁ᑃ㑅", a_)))
						{
							num = 22;
							continue;
						}
						sprᮟ a_2 = A_1.LegendEntries[iIndex].TextArea as sprᮟ;
						this.ᜀ(A_0, a_2, A_2);
						num = 0;
						continue;
					}
					case 10:
						goto IL_232;
					case 11:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num = 8;
							continue;
						}
						goto IL_14E;
					}
					case 12:
						goto IL_B8;
					case 13:
						goto IL_192;
					case 14:
						num = 9;
						continue;
					case 15:
						goto IL_232;
					case 16:
						num = 7;
						continue;
					case 17:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("⤿♁㱃", a_)))
						{
							num = 16;
							continue;
						}
						string s = spr\u1AA0.ᜄ(A_0);
						iIndex = int.Parse(s);
						num = 24;
						continue;
					}
					case 18:
						num = 11;
						continue;
					case 19:
						goto IL_14E;
					case 20:
						goto IL_149;
					case 21:
						if (A_0.LocalName != RecordTableEnumerator.b("ⰿ❁⍃⍅♇⹉ो⁍⑏⁑ⵓ", a_))
						{
							num = 20;
							continue;
						}
						A_0.Read();
						iIndex = 0;
						num = 10;
						continue;
					case 22:
						num = 19;
						continue;
					case 23:
						goto IL_257;
					case 24:
						goto IL_232;
					}
					if (A_0 == null)
					{
						num = 12;
						continue;
					}
					num = 4;
					continue;
					IL_14E:
					A_0.Skip();
					if (true)
					{
					}
					num = 15;
					continue;
					IL_232:
					num = 6;
					continue;
					IL_2EF:
					bool isDeleted = spr\u1AA0.ᜃ(A_0);
					A_1.LegendEntries[iIndex].IsDeleted = isDeleted;
					num = 2;
				}
				IL_B8:
				throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁╃≅ⵇ㡉", a_));
				IL_149:
				throw new XmlException(RecordTableEnumerator.b("ᔿⱁ⅃㹅㡇⽉⽋㩍㕏㙑瑓⹕㕗㙙籛⩝şա䩣", a_));
				IL_192:
				throw new ArgumentNullException(RecordTableEnumerator.b("ⰿ❁⍃⍅♇⹉", a_));
				IL_257:
				A_0.Read();
				return;
			}
			}
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x001092EC File Offset: 0x001082EC
		private spr\u2272 ᜂ(XmlReader A_0, XlsChart A_1)
		{
			int a_ = 3;
			switch (0)
			{
			default:
			{
				int num = 21;
				spr\u2272 spr_u;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (A_1 == null)
						{
							num = 16;
							continue;
						}
						num = 20;
						continue;
					case 1:
						goto IL_1E7;
					case 2:
						goto IL_1E7;
					case 3:
						goto IL_179;
					case 4:
						goto IL_1E7;
					case 5:
						goto IL_AF;
					case 6:
						num = 28;
						continue;
					case 7:
						goto IL_20C;
					case 8:
						num = 15;
						continue;
					case 9:
						goto IL_1E7;
					case 10:
						goto IL_1E7;
					case 11:
						goto IL_1E7;
					case 12:
						num = 18;
						continue;
					case 13:
						goto IL_1F3;
					case 14:
					{
						int num2;
						switch (num2)
						{
						case 0:
						{
							string s = spr\u1AA0.ᜄ(A_0);
							spr_u.ᜀ(short.Parse(s));
							num = 9;
							continue;
						}
						case 1:
						{
							spr_u.ᜁ(false);
							string s2 = spr\u1AA0.ᜄ(A_0);
							spr_u.ᜃ(ushort.Parse(s2));
							num = 27;
							continue;
						}
						case 2:
						{
							string s3 = spr\u1AA0.ᜄ(A_0);
							spr_u.ᜄ(ushort.Parse(s3));
							num = 1;
							continue;
						}
						case 3:
						{
							string s4 = spr\u1AA0.ᜄ(A_0);
							spr_u.ᜁ(ushort.Parse(s4));
							num = 23;
							continue;
						}
						case 4:
						{
							string s5 = spr\u1AA0.ᜄ(A_0);
							spr_u.ᜀ(XmlConvert.ToBoolean(s5));
							num = 11;
							continue;
						}
						case 5:
						{
							string s6 = spr\u1AA0.ᜄ(A_0);
							spr_u.ᜀ((ushort)(int.Parse(s6) / 2));
							num = 2;
							continue;
						}
						default:
							num = 8;
							continue;
						}
						break;
					}
					case 15:
						goto IL_1D3;
					case 16:
						goto IL_466;
					case 17:
					{
						int num2;
						string localName;
						if (spr\u22D2.\u1739.TryGetValue(localName, out num2))
						{
							num = 25;
							continue;
						}
						goto IL_1D3;
					}
					case 18:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num = 6;
							continue;
						}
						goto IL_1D3;
					}
					case 19:
						if (A_0.NodeType != XmlNodeType.Element)
						{
							A_0.Skip();
							num = 4;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1F3;
						default:
							if (false)
							{
							}
							num = 12;
							continue;
						}
						break;
					case 20:
						if (A_0.LocalName != RecordTableEnumerator.b("伸刺堼䠾牀݂", a_))
						{
							num = 22;
							continue;
						}
						A_0.Read();
						spr_u = (spr\u2272)spr\u175E.ᜀ(TBIFFRecord.Chart3D);
						num = 24;
						continue;
					case 22:
						goto IL_28E;
					case 23:
						goto IL_1E7;
					case 24:
						goto IL_1E7;
					case 25:
						num = 14;
						continue;
					case 26:
						spr\u22D2.\u1739 = new Dictionary<string, int>(6)
						{
							{
								RecordTableEnumerator.b("䬸吺䤼朾", a_),
								0
							},
							{
								RecordTableEnumerator.b("儸欺堼䴾≀♂⭄㍆", a_),
								1
							},
							{
								RecordTableEnumerator.b("䬸吺䤼显", a_),
								2
							},
							{
								RecordTableEnumerator.b("崸帺䴼䬾⥀ፂ⁄㕆⩈⹊⍌㭎", a_),
								3
							},
							{
								RecordTableEnumerator.b("䬸稺匼堾@㭂", a_),
								4
							},
							{
								RecordTableEnumerator.b("䤸帺似䰾ㅀ♂♄㍆⁈㵊⡌", a_),
								5
							}
						};
						num = 3;
						continue;
					case 27:
						goto IL_1E7;
					case 28:
						if (spr\u22D2.\u1739 == null)
						{
							num = 26;
							continue;
						}
						goto IL_179;
					}
					if (A_0 == null)
					{
						num = 5;
						continue;
					}
					num = 0;
					continue;
					IL_179:
					num = 17;
					continue;
					IL_1D3:
					A_0.Skip();
					num = 10;
					continue;
					IL_1E7:
					num = 13;
					continue;
					IL_1F3:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 7;
					}
					else
					{
						num = 19;
					}
				}
				IL_AF:
				throw new ArgumentNullException(RecordTableEnumerator.b("䬸帺尼嬾⑀ㅂ", a_));
				IL_20C:
				A_0.Read();
				return spr_u;
				IL_28E:
				throw new XmlException(RecordTableEnumerator.b("永唺堼䜾ㅀ♂♄㍆ⱈ⽊浌㝎㱐㽒畔⍖㡘㱚獜", a_));
				IL_466:
				if (true)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("娸区尼䴾㕀", a_));
			}
			}
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x00109774 File Offset: 0x00108774
		private void ᜆ(XmlReader A_0, XlsChartSerie A_1, RelationsCollection A_2)
		{
			int a_ = 5;
			switch (0)
			{
			default:
			{
				int num = 4;
				IChartErrorBars chartErrorBars;
				for (;;)
				{
					string value;
					object[] a_2;
					spr\u237B spr_u237B;
					switch (num)
					{
					case 0:
						if (!((spr\u237B)chartErrorBars).ᜎ())
						{
							num = 39;
							continue;
						}
						goto IL_182;
					case 1:
						num = 5;
						continue;
					case 2:
						spr\u22D2.\u173A = new Dictionary<string, int>(8)
						{
							{
								RecordTableEnumerator.b("帺似䴾Հ⩂㝄", a_),
								0
							},
							{
								RecordTableEnumerator.b("帺似䴾̀≂㝄ፆえ㭊⡌", a_),
								1
							},
							{
								RecordTableEnumerator.b("帺似䴾ᝀ≂⥄ፆえ㭊⡌", a_),
								2
							},
							{
								RecordTableEnumerator.b("唺刼稾⽀❂ل♆㥈", a_),
								3
							},
							{
								RecordTableEnumerator.b("䬺儼䨾㉀", a_),
								4
							},
							{
								RecordTableEnumerator.b("嘺吼儾㑀あ", a_),
								5
							},
							{
								RecordTableEnumerator.b("䴺尼匾", a_),
								6
							},
							{
								RecordTableEnumerator.b("䠺䴼漾㍀", a_),
								7
							}
						};
						num = 29;
						continue;
					case 3:
						goto IL_292;
					case 5:
					{
						int num2;
						switch (num2)
						{
						case 0:
						{
							string a = spr\u1AA0.ᜄ(A_0);
							num = 40;
							continue;
						}
						case 1:
							value = spr\u1AA0.ᜄ(A_0);
							num = 31;
							continue;
						case 2:
						{
							string value2 = spr\u1AA0.ᜄ(A_0);
							XLSXErrorBarType type = (XLSXErrorBarType)Enum.Parse(typeof(XLSXErrorBarType), value2, false);
							chartErrorBars.Type = (ErrorBarType)type;
							num = 3;
							continue;
						}
						case 3:
							chartErrorBars.HasCap = !spr\u1AA0.ᜃ(A_0);
							num = 10;
							continue;
						case 4:
						{
							XlsWorkbook xlsWorkbook;
							IXLSRange plusRange = this.ᜀ(A_0, xlsWorkbook, out a_2, chartErrorBars);
							num = 23;
							continue;
						}
						case 5:
						{
							XlsWorkbook xlsWorkbook;
							IXLSRange minusRange = this.ᜀ(A_0, xlsWorkbook, out a_2, chartErrorBars);
							num = 0;
							continue;
						}
						case 6:
							chartErrorBars.NumberValue = spr\u1AA0.ᜁ(A_0);
							num = 14;
							continue;
						case 7:
						{
							spr\u1A7B a_3 = new spr\u1A7B(chartErrorBars.Border as XlsChartBorder, null, null, chartErrorBars.Shadow as ChartShadow, chartErrorBars.Chart3DOptions as Format3D);
							sprវ a_4;
							spr\u1AA0.ᜀ(A_0, a_3, a_4, A_2);
							num = 37;
							continue;
						}
						default:
							num = 17;
							continue;
						}
						break;
					}
					case 6:
						goto IL_292;
					case 7:
						goto IL_608;
					case 8:
						goto IL_F8;
					case 9:
						num = 38;
						continue;
					case 10:
						goto IL_292;
					case 11:
						goto IL_210;
					case 12:
						goto IL_636;
					case 13:
						A_1.HasErrorBarsY = true;
						chartErrorBars = A_1.ErrorBarsY;
						num = 7;
						continue;
					case 14:
						goto IL_292;
					case 15:
						goto IL_2B7;
					case 16:
					{
						IXLSRange plusRange;
						chartErrorBars.PlusRange = plusRange;
						num = 42;
						continue;
					}
					case 17:
						num = 11;
						continue;
					case 18:
						if (A_1 == null)
						{
							num = 19;
							continue;
						}
						num = 21;
						continue;
					case 19:
						goto IL_606;
					case 20:
						goto IL_292;
					case 21:
					{
						if (A_0.LocalName != RecordTableEnumerator.b("帺似䴾̀≂㝄㑆", a_))
						{
							num = 27;
							continue;
						}
						A_0.Read();
						chartErrorBars = null;
						XlsWorkbook xlsWorkbook = A_1.ParentBook;
						sprវ a_4 = xlsWorkbook.DataHolder;
						a_2 = null;
						spr_u237B = null;
						num = 6;
						continue;
					}
					case 22:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num = 9;
							continue;
						}
						goto IL_210;
					}
					case 23:
						if (!((spr\u237B)chartErrorBars).ᜎ())
						{
							num = 16;
							continue;
						}
						goto IL_472;
					case 24:
						A_1.HasErrorBarsX = true;
						chartErrorBars = A_1.ErrorBarsX;
						num = 25;
						continue;
					case 25:
						goto IL_636;
					case 26:
						goto IL_182;
					case 27:
						goto IL_1D1;
					case 28:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 33;
							continue;
						}
						A_0.Skip();
						num = 20;
						continue;
					case 29:
						goto IL_4F5;
					case 30:
						goto IL_292;
					case 31:
						if (chartErrorBars == null)
						{
							num = 13;
							continue;
						}
						goto IL_608;
					case 32:
						goto IL_292;
					case 33:
						num = 22;
						continue;
					case 34:
					{
						int num2;
						string localName;
						if (spr\u22D2.\u173A.TryGetValue(localName, out num2))
						{
							num = 1;
							continue;
						}
						goto IL_210;
					}
					case 35:
						if (A_0.NodeType == XmlNodeType.EndElement)
						{
							num = 15;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_625;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num = 28;
							continue;
						}
						break;
					case 36:
						goto IL_292;
					case 37:
						goto IL_292;
					case 38:
						if (spr\u22D2.\u173A == null)
						{
							num = 2;
							continue;
						}
						goto IL_4F5;
					case 39:
					{
						IXLSRange minusRange;
						chartErrorBars.MinusRange = minusRange;
						num = 26;
						continue;
					}
					case 40:
					{
						string a;
						if (a == RecordTableEnumerator.b("䌺", a_))
						{
							num = 24;
							continue;
						}
						A_1.HasErrorBarsY = true;
						chartErrorBars = A_1.ErrorBarsY;
						num = 12;
						continue;
					}
					case 41:
						goto IL_292;
					case 42:
						goto IL_472;
					case 43:
						goto IL_292;
					}
					if (A_0 == null)
					{
						num = 8;
						continue;
					}
					num = 18;
					continue;
					IL_182:
					spr_u237B.ᜀ(a_2);
					num = 41;
					continue;
					IL_210:
					A_0.Skip();
					num = 36;
					continue;
					IL_292:
					num = 35;
					continue;
					IL_472:
					spr_u237B.ᜁ(a_2);
					num = 32;
					continue;
					IL_4F5:
					num = 34;
					continue;
					IL_625:
					num = 30;
					continue;
					IL_608:
					chartErrorBars.Include = (ErrorBarIncludeType)Enum.Parse(typeof(ErrorBarIncludeType), value, true);
					goto IL_625;
					IL_636:
					spr_u237B = (chartErrorBars as spr\u237B);
					num = 43;
				}
				IL_F8:
				throw new ArgumentNullException(RecordTableEnumerator.b("䤺堼帾╀♂㝄", a_));
				IL_1D1:
				throw new XmlException(RecordTableEnumerator.b("渺匼娾㥀㍂⁄⑆㵈⹊⥌潎⥐㹒㥔睖ⵘ㩚㩜煞", a_));
				IL_2B7:
				this.ᜀ(chartErrorBars);
				A_0.Read();
				return;
				IL_606:
				throw new ArgumentNullException(RecordTableEnumerator.b("䠺堼䴾⡀♂㙄", a_));
			}
			}
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x00109E30 File Offset: 0x00108E30
		private void ᜀ(IChartErrorBars A_0)
		{
			int a_ = 18;
			int num = 12;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_1CB;
				case 1:
					if (A_0.PlusRange != null)
					{
						goto IL_168;
					}
					goto IL_124;
				case 2:
					goto IL_148;
				case 3:
					return;
				case 4:
					if (((spr\u237B)A_0).ᜎ())
					{
						num = 11;
						continue;
					}
					goto IL_101;
				case 5:
					A_0.Include = ((A_0.MinusRange == null) ? ErrorBarIncludeType.Plus : ErrorBarIncludeType.Minus);
					num = 2;
					continue;
				case 6:
					if (A_0.MinusRange != null)
					{
						num = 9;
						continue;
					}
					goto IL_E4;
				case 7:
					if (A_0.MinusRange != null)
					{
						num = 8;
						continue;
					}
					goto IL_E4;
				case 8:
					num = 6;
					continue;
				case 9:
					goto IL_E2;
				case 10:
					goto IL_101;
				case 11:
					num = 13;
					continue;
				case 13:
					if (!((spr\u237B)A_0).\u170D())
					{
						num = 10;
						continue;
					}
					return;
				case 14:
					num = 1;
					continue;
				case 15:
					num = 7;
					continue;
				case 16:
					if (A_0.MinusRange == null)
					{
						num = 14;
						continue;
					}
					goto IL_1CB;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_168:
					num = 0;
					continue;
				}
				if (false)
				{
				}
				if (A_0.Type != ErrorBarType.Custom)
				{
					num = 3;
					continue;
				}
				num = 4;
				continue;
				IL_E4:
				num = 5;
				continue;
				IL_101:
				num = 16;
				continue;
				IL_1CB:
				((spr\u237B)A_0).ᜋ();
				num = 15;
			}
			return;
			IL_E2:
			A_0.Include = ErrorBarIncludeType.Both;
			return;
			IL_124:
			throw new NotSupportedException(RecordTableEnumerator.b("େ㽉㽋㩍㽏㽑瑓⁕㥗㙙⥛㭝䁟šգࡥ䡧ࡩ५乭ͯ᝱s噵᝷ᑹၻݽꁿꚅﮍ늑ﶙ鍊뺝쾟킡蒣횥쒧\udfa9\udfab躭슯펱\udab3통\uddb7骹햻춽뇁ꇃ닅", a_));
			IL_148:
			if (true)
			{
			}
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x0010A024 File Offset: 0x00109024
		private IXLSRange ᜀ(XmlReader A_0, IWorkbook A_1, out object[] A_2, IChartErrorBars A_3)
		{
			int a_ = 5;
			switch (0)
			{
			default:
			{
				int num = 4;
				string text;
				for (;;)
				{
					bool flag;
					switch (num)
					{
					case 0:
						goto IL_E3;
					case 1:
						if (A_0.LocalName == RecordTableEnumerator.b("唺䠼刾ፀ♂⍄", a_))
						{
							num = 2;
							continue;
						}
						goto IL_2C4;
					case 2:
						text = this.ᜀ(A_0, out A_2);
						num = 14;
						continue;
					case 3:
						if (flag)
						{
							num = 13;
							continue;
						}
						((spr\u237B)A_3).ᜂ(true);
						num = 19;
						continue;
					case 5:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 8;
							continue;
						}
						goto IL_2C4;
					case 6:
						A_0.Read();
						num = 21;
						continue;
					case 7:
						goto IL_21E;
					case 8:
						if (true)
						{
						}
						num = 1;
						continue;
					case 9:
						if (A_0.LocalName == RecordTableEnumerator.b("唺䠼刾ീ⩂ㅄ", a_))
						{
							num = 20;
							continue;
						}
						goto IL_E3;
					case 10:
						if (A_0.LocalName == RecordTableEnumerator.b("䬺儼䨾㉀", a_))
						{
							num = 18;
							continue;
						}
						goto IL_1DC;
					case 11:
						goto IL_90;
					case 12:
						goto IL_223;
					case 13:
						((spr\u237B)A_3).ᜄ(true);
						num = 12;
						continue;
					case 14:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_145;
						default:
							if (false)
							{
							}
							goto IL_E3;
						}
						break;
					case 15:
						if (A_0.NodeType == XmlNodeType.EndElement)
						{
							num = 6;
							continue;
						}
						num = 5;
						continue;
					case 16:
						goto IL_E3;
					case 17:
						goto IL_1DC;
					case 18:
						goto IL_145;
					case 19:
						goto IL_223;
					case 20:
						num = 3;
						continue;
					case 21:
						if (text != null)
						{
							num = 7;
							continue;
						}
						goto IL_2FF;
					}
					if (A_0 == null)
					{
						num = 11;
						continue;
					}
					flag = false;
					num = 10;
					continue;
					IL_E3:
					num = 15;
					continue;
					IL_145:
					flag = true;
					num = 17;
					continue;
					IL_1DC:
					A_0.Read();
					text = null;
					A_2 = null;
					num = 0;
					continue;
					IL_223:
					string empty = string.Empty;
					A_2 = this.ᜁ(A_0);
					num = 16;
					continue;
					IL_2C4:
					num = 9;
				}
				IL_90:
				throw new ArgumentNullException(RecordTableEnumerator.b("䤺堼帾╀♂㝄", a_));
				IL_21E:
				XlsWorkbook xlsWorkbook = A_1 as XlsWorkbook;
				FormulaUtil formulaUtil = xlsWorkbook.DataHolder.\u1718().ᜀ();
				Ptg[] array = formulaUtil.ᜃ(text);
				sprỜ sprỜ = array[0] as sprỜ;
				return sprỜ.ᜀ(A_1, A_1.Worksheets[0]);
				IL_2FF:
				return null;
			}
			}
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x0010A334 File Offset: 0x00109334
		private void ᜅ(XmlReader A_0, XlsChartSerie A_1, RelationsCollection A_2)
		{
			int a_ = 10;
			int num = 7;
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
						num = 5;
						continue;
					}
					goto IL_AF;
				case 1:
					goto IL_AF;
				case 2:
					return;
				case 3:
					goto IL_61;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_69;
					}
					if (false)
					{
					}
					if (!(A_0.LocalName == RecordTableEnumerator.b("㐿ぁ⅃⡅ⱇ♉╋⁍㕏", a_)))
					{
						num = 2;
						continue;
					}
					this.ᜄ(A_0, A_1, A_2);
					num = 3;
					continue;
				case 5:
					goto IL_AA;
				case 6:
					goto IL_61;
				case 8:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 1;
						continue;
					}
					A_0.Read();
					num = 6;
					continue;
				case 9:
					goto IL_5C;
				case 10:
					goto IL_69;
				case 11:
					num = 8;
					continue;
				}
				if (A_0 == null)
				{
					num = 9;
					continue;
				}
				num = 0;
				continue;
				IL_61:
				num = 10;
				continue;
				IL_69:
				if (A_0.NodeType != XmlNodeType.EndElement)
				{
					num = 11;
					continue;
				}
				IL_AF:
				num = 4;
			}
			IL_5C:
			throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁╃≅ⵇ㡉", a_));
			IL_AA:
			throw new ArgumentNullException(RecordTableEnumerator.b("㌿❁㙃⽅ⵇ㥉", a_));
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x0010A4B4 File Offset: 0x001094B4
		private void ᜄ(XmlReader A_0, XlsChartSerie A_1, RelationsCollection A_2)
		{
			int a_ = 6;
			for (;;)
			{
				IL_09:
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
							string localName;
							if ((localName = A_0.LocalName) != null)
							{
								num = 19;
								continue;
							}
							goto IL_545;
						}
						case 1:
						{
							int num2;
							switch (num2)
							{
							case 0:
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
									sprᴌ sprᴌ;
									sprᴌ.ᜀ(A_0.ReadElementContentAsString());
									num = 3;
									continue;
								}
								}
								break;
							case 1:
							{
								sprᴌ sprᴌ;
								spr\u1A7B a_2 = new spr\u1A7B(sprᴌ.ᜄ() as XlsChartBorder, null, null, sprᴌ.\u1713() as ChartShadow, sprᴌ.ᜋ() as Format3D);
								sprវ a_3;
								spr\u1AA0.ᜀ(A_0, a_2, a_3, A_2);
								num = 13;
								continue;
							}
							case 2:
							{
								string value = spr\u1AA0.ᜄ(A_0);
								XLSXTrendlineType a_4 = (XLSXTrendlineType)Enum.Parse(typeof(XLSXTrendlineType), value, false);
								sprᴌ sprᴌ;
								sprᴌ.ᜁ((TrendLineType)a_4);
								num = 16;
								continue;
							}
							case 3:
							case 4:
							{
								sprᴌ sprᴌ;
								sprᴌ.ᜂ(spr\u1AA0.ᜂ(A_0));
								num = 12;
								continue;
							}
							case 5:
							{
								sprᴌ sprᴌ;
								sprᴌ.ᜂ(spr\u1AA0.ᜁ(A_0));
								num = 11;
								continue;
							}
							case 6:
							{
								sprᴌ sprᴌ;
								sprᴌ.ᜁ(spr\u1AA0.ᜁ(A_0));
								num = 10;
								continue;
							}
							case 7:
							{
								sprᴌ sprᴌ;
								sprᴌ.ᜃ(spr\u1AA0.ᜁ(A_0));
								num = 32;
								continue;
							}
							case 8:
							{
								sprᴌ sprᴌ;
								sprᴌ.ᜁ(spr\u1AA0.ᜃ(A_0));
								num = 22;
								continue;
							}
							case 9:
							{
								sprᴌ sprᴌ;
								sprᴌ.ᜅ(spr\u1AA0.ᜃ(A_0));
								num = 26;
								continue;
							}
							case 10:
							{
								sprᴌ sprᴌ;
								this.ᜀ(A_0, sprᴌ);
								num = 24;
								continue;
							}
							default:
								num = 6;
								continue;
							}
							break;
						}
						case 2:
							if (A_1 == null)
							{
								num = 20;
								continue;
							}
							num = 8;
							continue;
						case 3:
							goto IL_240;
						case 4:
							goto IL_240;
						case 5:
							num = 0;
							continue;
						case 6:
							num = 30;
							continue;
						case 7:
							goto IL_C2;
						case 8:
						{
							if (A_0.LocalName != RecordTableEnumerator.b("䠻䰽┿ⱁ⁃⩅ⅇ⑉⥋", a_))
							{
								num = 17;
								continue;
							}
							A_0.Read();
							sprᴌ sprᴌ = A_1.TrendLines.Add() as sprᴌ;
							sprវ a_3 = A_1.ParentBook.DataHolder;
							num = 27;
							continue;
						}
						case 9:
							goto IL_265;
						case 10:
							goto IL_240;
						case 11:
							goto IL_240;
						case 12:
							goto IL_240;
						case 13:
							goto IL_240;
						case 15:
							if (spr\u22D2.\u173B == null)
							{
								num = 28;
								continue;
							}
							goto IL_485;
						case 16:
							goto IL_240;
						case 17:
							goto IL_3D0;
						case 18:
							if (A_0.NodeType == XmlNodeType.Element)
							{
								num = 5;
								continue;
							}
							A_0.Skip();
							num = 21;
							continue;
						case 19:
							num = 15;
							continue;
						case 20:
							goto IL_540;
						case 21:
							goto IL_240;
						case 22:
							goto IL_240;
						case 23:
							if (A_0.NodeType == XmlNodeType.EndElement)
							{
								num = 9;
								continue;
							}
							num = 18;
							continue;
						case 24:
							goto IL_240;
						case 25:
						{
							string localName;
							int num2;
							if (spr\u22D2.\u173B.TryGetValue(localName, out num2))
							{
								num = 29;
								continue;
							}
							goto IL_545;
						}
						case 26:
							goto IL_240;
						case 27:
							goto IL_240;
						case 28:
							spr\u22D2.\u173B = new Dictionary<string, int>(11)
							{
								{
									RecordTableEnumerator.b("刻弽ⴿ❁", a_),
									0
								},
								{
									RecordTableEnumerator.b("伻丽ဿぁ", a_),
									1
								},
								{
									RecordTableEnumerator.b("䠻䰽┿ⱁ⁃⩅ⅇ⑉⥋ᩍ⥏≑ㅓ", a_),
									2
								},
								{
									RecordTableEnumerator.b("医䰽␿❁㙃", a_),
									3
								},
								{
									RecordTableEnumerator.b("䰻嬽㈿⭁⭃≅", a_),
									4
								},
								{
									RecordTableEnumerator.b("娻儽㈿㕁╃㑅ⱇ", a_),
									5
								},
								{
									RecordTableEnumerator.b("帻弽⌿⥁㍃❅㩇⹉", a_),
									6
								},
								{
									RecordTableEnumerator.b("唻倽㐿❁㙃╅ⵇ㩉㡋", a_),
									7
								},
								{
									RecordTableEnumerator.b("堻圽㌿㉁ᙃᕅ㥇㡉", a_),
									8
								},
								{
									RecordTableEnumerator.b("堻圽㌿㉁Ń㝅", a_),
									9
								},
								{
									RecordTableEnumerator.b("䠻䰽┿ⱁ⁃⩅ⅇ⑉⥋ɍ㉏㹑", a_),
									10
								}
							};
							num = 31;
							continue;
						case 29:
							if (true)
							{
							}
							num = 1;
							continue;
						case 30:
							goto IL_545;
						case 31:
							goto IL_485;
						case 32:
							goto IL_240;
						}
						if (A_0 == null)
						{
							num = 7;
							continue;
						}
						num = 2;
						continue;
						IL_240:
						num = 23;
						continue;
						IL_485:
						num = 25;
						continue;
						IL_545:
						A_0.Skip();
						num = 4;
					}
					break;
				}
				}
			}
			IL_C2:
			throw new ArgumentNullException(RecordTableEnumerator.b("主嬽ℿ♁⅃㑅", a_));
			IL_265:
			A_0.Read();
			return;
			IL_3D0:
			throw new XmlException(RecordTableEnumerator.b("椻倽┿㩁㑃⍅⭇㹉⥋⩍灏⩑㥓㩕硗⹙㵛㥝也", a_));
			IL_540:
			throw new ArgumentNullException(RecordTableEnumerator.b("伻嬽㈿⭁⅃㕅", a_));
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x0010AA54 File Offset: 0x00109A54
		private void ᜀ(XmlReader A_0, IChartTrendLine A_1)
		{
			int a_ = 3;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_110;
				case 1:
					if (A_0.LocalName != RecordTableEnumerator.b("䴸䤺堼儾╀⽂ⱄ⥆ⱈ݊⽌⍎", a_))
					{
						num = 2;
						continue;
					}
					A_0.Read();
					num = 9;
					continue;
				case 2:
					goto IL_166;
				case 4:
					if (A_1 == null)
					{
						num = 6;
						continue;
					}
					num = 1;
					continue;
				case 5:
					goto IL_130;
				case 6:
					goto IL_9A;
				case 7:
					goto IL_110;
				case 8:
					A_0.Skip();
					num = 0;
					continue;
				case 9:
					goto IL_110;
				case 10:
					goto IL_58;
				case 11:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 8;
						continue;
					}
					A_0.Skip();
					num = 7;
					continue;
				case 12:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 5;
						continue;
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
						num = 11;
						continue;
					}
					break;
				}
				if (A_0 == null)
				{
					num = 10;
					continue;
				}
				IL_81:
				num = 4;
				continue;
				IL_110:
				num = 12;
			}
			IL_58:
			throw new ArgumentNullException(RecordTableEnumerator.b("䬸帺尼嬾⑀ㅂ", a_));
			IL_9A:
			throw new ArgumentNullException(RecordTableEnumerator.b("䴸䤺堼儾╀⽂ⱄ⥆ⱈ", a_));
			IL_130:
			A_0.Read();
			return;
			IL_166:
			if (true)
			{
			}
			throw new XmlException(RecordTableEnumerator.b("永唺堼䜾ㅀ♂♄㍆ⱈ⽊浌㝎㱐㽒畔⍖㡘㱚獜", a_));
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x0010AC00 File Offset: 0x00109C00
		private void ᜀ(XmlReader A_0, IChartWallOrFloor A_1, sprវ A_2, RelationsCollection A_3)
		{
			int a_ = 16;
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!A_0.IsEmptyElement)
					{
						num = 6;
						continue;
					}
					goto IL_2C6;
				case 1:
					num = 3;
					continue;
				case 2:
					goto IL_162;
				case 3:
					goto IL_127;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2C1;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 7;
							continue;
						}
						A_0.Skip();
						num = 20;
						continue;
					}
					break;
				case 6:
					A_0.Read();
					num = 9;
					continue;
				case 7:
					num = 19;
					continue;
				case 8:
					goto IL_153;
				case 9:
					goto IL_162;
				case 10:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("㉅⁇⍉⽋╍㹏㝑❓╕", a_)))
					{
						num = 1;
						continue;
					}
					string s = spr\u1AA0.ᜄ(A_0);
					((XlsChartWallOrFloor)A_1).Thickness = int.Parse(s);
					num = 12;
					continue;
				}
				case 11:
					goto IL_162;
				case 12:
					goto IL_162;
				case 13:
					goto IL_7F;
				case 14:
					goto IL_185;
				case 15:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 14;
						continue;
					}
					num = 4;
					continue;
				case 16:
					num = 18;
					continue;
				case 17:
					if (A_1 == null)
					{
						num = 8;
						continue;
					}
					((XlsChartWallOrFloor)A_1).HasShapeProperties = false;
					num = 0;
					continue;
				case 18:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("㕅㡇ᩉ㹋", a_)))
					{
						num = 21;
						continue;
					}
					((XlsChartWallOrFloor)A_1).HasShapeProperties = true;
					spr\u1772 a_2 = new spr\u1A7B(A_1.LineProperties, A_1.Interior as XlsChartInterior, A_1.Fill as spr\u1C26, A_1.Shadow, A_1.Format3D);
					spr\u1AA0.ᜀ(A_0, a_2, A_2, A_3);
					num = 11;
					continue;
				}
				case 19:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 16;
						continue;
					}
					goto IL_127;
				}
				case 20:
					goto IL_162;
				case 21:
					goto IL_2C1;
				}
				if (A_0 == null)
				{
					num = 13;
					continue;
				}
				num = 17;
				continue;
				IL_127:
				A_0.Skip();
				num = 2;
				continue;
				IL_162:
				num = 15;
				continue;
				IL_2C1:
				num = 10;
			}
			IL_7F:
			throw new ArgumentNullException(RecordTableEnumerator.b("㑅ⵇ⭉⡋⭍≏", a_));
			IL_153:
			throw new ArgumentNullException(RecordTableEnumerator.b("㕅㵇㡉⩋⽍㍏㝑", a_));
			IL_185:
			IL_2C6:
			A_0.Read();
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x0010AEDC File Offset: 0x00109EDC
		private void ᜂ(XmlReader A_0, XlsChart A_1, RelationsCollection A_2, spr\u2306 A_3)
		{
			int a_ = 5;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_6A;
				case 1:
					if (A_1 == null)
					{
						num = 2;
						continue;
					}
					num = 0;
					continue;
				case 2:
					goto IL_F1;
				case 4:
					goto IL_8D;
				case 5:
					goto IL_60;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_6A:
					if (!(A_0.LocalName != RecordTableEnumerator.b("䬺儼倾㕀ɂ㝄≆⡈", a_)))
					{
						goto IL_F3;
					}
					num = 4;
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					if (A_0 == null)
					{
						num = 5;
					}
					else
					{
						num = 1;
					}
					break;
				}
			}
			IL_60:
			throw new ArgumentNullException(RecordTableEnumerator.b("䤺堼帾╀♂㝄", a_));
			IL_8D:
			throw new XmlException(RecordTableEnumerator.b("渺匼娾㥀㍂⁄⑆㵈⹊⥌潎⥐㹒㥔睖ⵘ㩚㩜", a_));
			IL_F1:
			throw new ArgumentNullException(RecordTableEnumerator.b("堺唼帾㍀㝂", a_));
			IL_F3:
			Stream stream = ShapeParser.ReadNodeAsStream(A_0);
			stream.Position = 0L;
			A_0 = UtilityMethods.ᜀ(stream);
			A_0.Read();
			this.ᜀ(A_0, A_1, A_2, A_3);
			stream.Position = 0L;
			A_0 = UtilityMethods.ᜀ(stream);
			A_0.Read();
			this.ᜁ(A_0, A_1, A_2, A_3);
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x0010B028 File Offset: 0x0010A028
		private void ᜁ(XmlReader A_0, XlsChart A_1, RelationsCollection A_2, spr\u2306 A_3)
		{
			int a_ = 3;
			switch (0)
			{
			default:
			{
				Dictionary<int, int> dictionary;
				for (;;)
				{
					IChartFrameFormat plotArea = A_1.PlotArea;
					sprវ a_2 = A_1.DataHolder.ᜋ();
					dictionary = new Dictionary<int, int>();
					int num = 21;
					for (;;)
					{
						bool isBorderCornersRound;
						bool flag;
						switch (num)
						{
						case 0:
							num = 24;
							continue;
						case 1:
							goto IL_716;
						case 2:
							goto IL_716;
						case 3:
							num = 39;
							continue;
						case 4:
							num = 8;
							continue;
						case 5:
							num = 41;
							continue;
						case 6:
							spr\u22D2.\u173C = new Dictionary<string, int>(20)
							{
								{
									RecordTableEnumerator.b("唸娺䐼倾㑀㝂", a_),
									0
								},
								{
									RecordTableEnumerator.b("崸漺尼崾ⵀ♂", a_),
									1
								},
								{
									RecordTableEnumerator.b("䨸䬺洼䴾", a_),
									2
								},
								{
									RecordTableEnumerator.b("嬸娺似簾⥀≂㝄㍆", a_),
									3
								},
								{
									RecordTableEnumerator.b("嬸娺似ాՀBⵄ♆㭈㽊", a_),
									4
								},
								{
									RecordTableEnumerator.b("堸䤺堼帾ɀ⭂⑄㕆㵈", a_),
									5
								},
								{
									RecordTableEnumerator.b("堸䤺堼帾牀݂ل⽆⡈㥊㥌", a_),
									6
								},
								{
									RecordTableEnumerator.b("唸刺匼娾ɀ⭂⑄㕆㵈", a_),
									7
								},
								{
									RecordTableEnumerator.b("唸刺匼娾牀݂ل⽆⡈㥊㥌", a_),
									8
								},
								{
									RecordTableEnumerator.b("嬸为弼崾ⵀ♂ل⽆⡈㥊㥌", a_),
									9
								},
								{
									RecordTableEnumerator.b("嬸为弼崾ⵀ♂癄͆", a_),
									10
								},
								{
									RecordTableEnumerator.b("䨸为似夾⁀⁂⁄цⅈ⩊㽌㭎", a_),
									11
								},
								{
									RecordTableEnumerator.b("䨸为似夾⁀⁂⁄瑆ൈࡊ╌⹎⍐❒", a_),
									12
								},
								{
									RecordTableEnumerator.b("䬸娺夼帾㍀Bⵄ♆㭈㽊", a_),
									13
								},
								{
									RecordTableEnumerator.b("䨸堺尼䬾㕀♂㝄цⅈ⩊㽌㭎", a_),
									14
								},
								{
									RecordTableEnumerator.b("䤸刺堼簾⥀≂㝄㍆", a_),
									15
								},
								{
									RecordTableEnumerator.b("䤸刺堼ాՀBⵄ♆㭈㽊", a_),
									16
								},
								{
									RecordTableEnumerator.b("崸吺䠼堾⥀ⵂい㍆ੈ⍊ⱌ㵎═", a_),
									17
								},
								{
									RecordTableEnumerator.b("嘸崺洼嘾⑀Bⵄ♆㭈㽊", a_),
									18
								},
								{
									RecordTableEnumerator.b("䨸伺刼尾⩀Bⵄ♆㭈㽊", a_),
									19
								}
							};
							num = 20;
							continue;
						case 7:
							num = 22;
							continue;
						case 8:
							if (spr\u22D2.\u173C == null)
							{
								num = 6;
								continue;
							}
							goto IL_4D4;
						case 9:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_462;
							default:
								if (false)
								{
								}
								num = 29;
								continue;
							}
							break;
						case 10:
							if (A_0.NodeType == XmlNodeType.Element)
							{
								num = 3;
								continue;
							}
							A_0.Skip();
							num = 23;
							continue;
						case 11:
							goto IL_716;
						case 12:
							goto IL_716;
						case 13:
							goto IL_716;
						case 14:
							goto IL_716;
						case 15:
							goto IL_716;
						case 16:
							goto IL_716;
						case 17:
							goto IL_45D;
						case 18:
							if (A_0.NodeType != XmlNodeType.EndElement)
							{
								num = 7;
								continue;
							}
							goto IL_755;
						case 19:
							goto IL_716;
						case 20:
							goto IL_4D4;
						case 21:
							if (A_1.PlotArea == null)
							{
								num = 9;
								continue;
							}
							num = 27;
							continue;
						case 22:
							if (A_0.NodeType == XmlNodeType.None)
							{
								num = 17;
								continue;
							}
							num = 10;
							continue;
						case 23:
							goto IL_716;
						case 24:
						{
							int num2;
							switch (num2)
							{
							case 0:
								A_1.InnerPlotArea.LayoutStream = ShapeParser.ReadNodeAsStream(A_0);
								num = 19;
								continue;
							case 1:
								this.ᜀ(A_0, A_1);
								num = 33;
								continue;
							case 2:
							{
								A_1.HasPlotArea = true;
								plotArea = A_1.PlotArea;
								A_1.PlotArea.IsBorderCornersRound = isBorderCornersRound;
								spr\u1A7B a_3 = new spr\u1A7B(plotArea.Border as XlsChartBorder, plotArea.Interior as XlsChartInterior, plotArea.Fill as spr\u1C26, plotArea.Shadow, plotArea.Format3D);
								spr\u1AA0.ᜀ(A_0, a_3, a_2, A_2);
								num = 12;
								continue;
							}
							case 3:
								goto IL_462;
							case 4:
								this.ᜈ(A_0, A_1, A_2, dictionary);
								num = 14;
								continue;
							case 5:
								this.ᜆ(A_0, A_1, A_2, dictionary);
								num = 11;
								continue;
							case 6:
								this.ᜇ(A_0, A_1, A_2, dictionary);
								num = 35;
								continue;
							case 7:
								this.ᜃ(A_0, A_1, A_2, dictionary, A_3);
								num = 34;
								continue;
							case 8:
								this.ᜄ(A_0, A_1, A_2, dictionary, A_3);
								num = 16;
								continue;
							case 9:
							case 10:
								this.ᜅ(A_0, A_1, A_2, dictionary);
								num = 1;
								continue;
							case 11:
								this.ᜄ(A_0, A_1, A_2, dictionary);
								num = 28;
								continue;
							case 12:
								this.ᜄ(A_0, A_1, A_2, dictionary);
								num = 32;
								continue;
							case 13:
								this.ᜂ(A_0, A_1, A_2, dictionary, A_3);
								num = 31;
								continue;
							case 14:
								this.ᜁ(A_0, A_1, A_2, dictionary, A_3);
								num = 15;
								continue;
							case 15:
								this.ᜃ(A_0, A_1, A_2, dictionary);
								num = 25;
								continue;
							case 16:
								this.ᜂ(A_0, A_1, A_2, dictionary);
								num = 30;
								continue;
							case 17:
								if (true)
								{
								}
								this.ᜀ(A_0, A_1, A_2, dictionary);
								num = 38;
								continue;
							case 18:
								this.ᜁ(A_0, A_1, A_2, dictionary);
								num = 37;
								continue;
							case 19:
								this.ᜀ(A_0, A_1, A_2, dictionary, A_3);
								num = 2;
								continue;
							default:
								num = 5;
								continue;
							}
							break;
						}
						case 25:
							goto IL_716;
						case 26:
							goto IL_716;
						case 27:
							flag = A_1.PlotArea.IsBorderCornersRound;
							goto IL_696;
						case 28:
							goto IL_716;
						case 29:
							flag = false;
							goto IL_696;
						case 30:
							goto IL_716;
						case 31:
							goto IL_716;
						case 32:
							goto IL_716;
						case 33:
							goto IL_716;
						case 34:
							goto IL_716;
						case 35:
							goto IL_716;
						case 36:
							goto IL_716;
						case 37:
							goto IL_716;
						case 38:
							goto IL_716;
						case 39:
						{
							string localName;
							if ((localName = A_0.LocalName) != null)
							{
								num = 4;
								continue;
							}
							goto IL_517;
						}
						case 40:
						{
							int num2;
							string localName;
							if (spr\u22D2.\u173C.TryGetValue(localName, out num2))
							{
								num = 0;
								continue;
							}
							goto IL_517;
						}
						case 41:
							goto IL_517;
						}
						break;
						IL_462:
						this.ᜉ(A_0, A_1, A_2, dictionary);
						num = 36;
						continue;
						IL_4D4:
						num = 40;
						continue;
						IL_517:
						A_0.Skip();
						num = 26;
						continue;
						IL_696:
						isBorderCornersRound = flag;
						A_1.HasPlotArea = false;
						num = 13;
						continue;
						IL_716:
						num = 18;
					}
				}
				IL_45D:
				IL_755:
				XlsChartSeries xlsChartSeries = A_1.Series;
				xlsChartSeries.ᜀ(dictionary);
				return;
			}
			}
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x0010B79C File Offset: 0x0010A79C
		private void ᜀ(XmlReader A_0, XlsChart A_1, RelationsCollection A_2, spr\u2306 A_3)
		{
			int a_ = 6;
			switch (0)
			{
			default:
				for (;;)
				{
					sprῐ sprῐ = new sprῐ(this.ᜁ);
					IChartFrameFormat plotArea = A_1.PlotArea;
					A_1.DataHolder.ᜋ();
					new Dictionary<int, int>();
					int num = 0;
					ExcelChartType a_2 = A_1.ChartType;
					int num2 = 21;
					for (;;)
					{
						XlsChartSeriesAxis xlsChartSeriesAxis;
						IChartCategoryAxis chartCategoryAxis;
						IChartValueAxis chartValueAxis;
						IChartCategoryAxis chartCategoryAxis2;
						switch (num2)
						{
						case 0:
							if (xlsChartSeriesAxis == null)
							{
								num2 = 26;
								continue;
							}
							goto IL_FA;
						case 1:
							return;
						case 2:
							goto IL_42C;
						case 3:
							goto IL_FA;
						case 4:
							if (A_0.NodeType == XmlNodeType.Element)
							{
								num2 = 8;
								continue;
							}
							A_0.Skip();
							num2 = 9;
							continue;
						case 5:
							num2 = 13;
							continue;
						case 6:
							num2 = 10;
							continue;
						case 7:
							chartCategoryAxis = A_1.SecondaryCategoryAxis;
							goto IL_295;
						case 8:
							num2 = 27;
							continue;
						case 9:
							goto IL_42C;
						case 10:
							chartValueAxis = A_1.SecondaryValueAxis;
							goto IL_35A;
						case 11:
							num2 = 7;
							continue;
						case 12:
							if (true)
							{
							}
							goto IL_3EA;
						case 13:
							chartCategoryAxis2 = A_1.SecondaryCategoryAxis;
							goto IL_513;
						case 14:
							goto IL_42C;
						case 15:
							if (A_0.NodeType != XmlNodeType.EndElement)
							{
								num2 = 4;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_372;
							default:
								if (false)
								{
								}
								num2 = 1;
								continue;
							}
							break;
						case 16:
							num2 = 19;
							continue;
						case 17:
							spr\u22D2.\u173D = new Dictionary<string, int>(6)
							{
								{
									RecordTableEnumerator.b("䨻弽ⰿ́㱃", a_),
									0
								},
								{
									RecordTableEnumerator.b("伻嬽㈿́㱃", a_),
									1
								},
								{
									RecordTableEnumerator.b("弻弽㐿́㱃", a_),
									2
								},
								{
									RecordTableEnumerator.b("堻弽㐿❁Ճ㹅", a_),
									3
								},
								{
									RecordTableEnumerator.b("帻䬽∿⁁⡃⍅େ≉ⵋ㱍⑏", a_),
									4
								},
								{
									RecordTableEnumerator.b("伻崽ℿ㙁ぃ⍅㩇ॉ⑋⽍≏♑", a_),
									5
								}
							};
							num2 = 37;
							continue;
						case 18:
						{
							int num3;
							switch (num3)
							{
							case 0:
							{
								bool flag = num <= 1;
								A_1.CreateNecessaryAxes(flag);
								num2 = 30;
								continue;
							}
							case 1:
								A_1.CreateNecessaryAxes(true);
								xlsChartSeriesAxis = (XlsChartSeriesAxis)A_1.PrimarySerieAxis;
								num2 = 0;
								continue;
							case 2:
							{
								bool flag = num <= 1;
								A_1.CreateNecessaryAxes(flag);
								num2 = 28;
								continue;
							}
							case 3:
							{
								bool flag = num <= 1;
								A_1.CreateNecessaryAxes(flag);
								num2 = 22;
								continue;
							}
							case 4:
								a_2 = ExcelChartType.Bubble;
								A_0.Skip();
								num2 = 34;
								continue;
							case 5:
								a_2 = ExcelChartType.ScatterMarkers;
								A_0.Skip();
								num2 = 20;
								continue;
							default:
								num2 = 32;
								continue;
							}
							break;
						}
						case 19:
							if (spr\u22D2.\u173D == null)
							{
								num2 = 17;
								continue;
							}
							goto IL_1B6;
						case 20:
							goto IL_42C;
						case 21:
							goto IL_42C;
						case 22:
						{
							bool flag;
							if (!flag)
							{
								num2 = 11;
								continue;
							}
							num2 = 29;
							continue;
						}
						case 23:
							goto IL_42C;
						case 24:
							goto IL_42C;
						case 25:
						{
							int num3;
							string localName;
							if (spr\u22D2.\u173D.TryGetValue(localName, out num3))
							{
								num2 = 36;
								continue;
							}
							goto IL_3EA;
						}
						case 26:
							xlsChartSeriesAxis = A_1.ᜦ();
							num2 = 3;
							continue;
						case 27:
						{
							string localName;
							if ((localName = A_0.LocalName) != null)
							{
								num2 = 16;
								continue;
							}
							goto IL_3EA;
						}
						case 28:
							if (num > 1)
							{
								num2 = 5;
								continue;
							}
							num2 = 35;
							continue;
						case 29:
							chartCategoryAxis = A_1.PrimaryCategoryAxis;
							goto IL_295;
						case 30:
						{
							bool flag;
							if (!flag)
							{
								num2 = 6;
								continue;
							}
							num2 = 31;
							continue;
						}
						case 31:
							chartValueAxis = A_1.PrimaryValueAxis;
							goto IL_35A;
						case 32:
							num2 = 12;
							continue;
						case 33:
							goto IL_42C;
						case 34:
							goto IL_42C;
						case 35:
							chartCategoryAxis2 = A_1.PrimaryCategoryAxis;
							goto IL_513;
						case 36:
							num2 = 18;
							continue;
						case 37:
							goto IL_1B6;
						}
						break;
						IL_FA:
						sprῐ.ᜀ(A_0, xlsChartSeriesAxis, A_2, a_2, A_3);
						num2 = 33;
						continue;
						IL_1B6:
						num2 = 25;
						continue;
						IL_295:
						XlsChartCategoryAxis a_3 = (XlsChartCategoryAxis)chartCategoryAxis;
						sprῐ.ᜁ(A_0, a_3, A_2, a_2, A_3);
						num++;
						num2 = 2;
						continue;
						IL_372:
						num2 = 14;
						continue;
						IL_35A:
						XlsChartValueAxis a_4 = (XlsChartValueAxis)chartValueAxis;
						sprῐ.ᜀ(A_0, a_4, A_2, a_2, A_3);
						num++;
						goto IL_372;
						IL_3EA:
						A_0.Skip();
						num2 = 23;
						continue;
						IL_42C:
						num2 = 15;
						continue;
						IL_513:
						XlsChartCategoryAxis a_5 = (XlsChartCategoryAxis)chartCategoryAxis2;
						sprῐ.ᜀ(A_0, a_5, A_2, a_2, A_3);
						num++;
						num2 = 24;
					}
				}
				return;
			}
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x0010BCE8 File Offset: 0x0010ACE8
		private void ᜉ(XmlReader A_0, XlsChart A_1, RelationsCollection A_2, Dictionary<int, int> A_3)
		{
			int a_ = 15;
			switch (0)
			{
			default:
			{
				int num = 14;
				for (;;)
				{
					bool flag;
					IChartFormat chartFormat;
					IChartFormat chartFormat2;
					switch (num)
					{
					case 0:
					{
						int? num2;
						if (num2.GetValueOrDefault() == 100)
						{
							num = 34;
							continue;
						}
						num = 35;
						continue;
					}
					case 1:
					{
						IChartSerie chartSerie;
						if (chartSerie != null)
						{
							num = 39;
							continue;
						}
						goto IL_EB;
					}
					case 2:
					{
						if (A_0.LocalName != RecordTableEnumerator.b("❄♆㭈ࡊ╌⹎⍐❒", a_))
						{
							num = 30;
							continue;
						}
						A_0.Read();
						List<XlsChartSerie> list = new List<XlsChartSerie>();
						string text = null;
						IChartSerie chartSerie = this.ᜀ(A_0, A_1, A_2, false, list, out text);
						int? num3 = null;
						int? num4 = null;
						num = 27;
						continue;
					}
					case 3:
						num = 11;
						continue;
					case 4:
						goto IL_344;
					case 5:
						num = 1;
						continue;
					case 6:
					{
						int? num2;
						flag = (num2 != null);
						goto IL_4F3;
					}
					case 7:
					{
						int? num4 = new int?(-65436);
						num = 12;
						continue;
					}
					case 8:
						goto IL_248;
					case 9:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("⑄㽆H⽊", a_)))
						{
							num = 32;
							continue;
						}
						List<XlsChartSerie> list;
						this.ᜀ(A_0, list, A_3);
						num = 10;
						continue;
					}
					case 10:
						goto IL_344;
					case 11:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("≄♆㥈᱊⑌⭎═㭒", a_)))
						{
							num = 15;
							continue;
						}
						string s = spr\u1AA0.ᜄ(A_0);
						int? num3 = new int?(int.Parse(s));
						num = 21;
						continue;
					}
					case 12:
						goto IL_344;
					case 13:
						goto IL_4D2;
					case 15:
						num = 33;
						continue;
					case 16:
						chartFormat = null;
						goto IL_203;
					case 17:
					{
						IChartSerie chartSerie;
						if (chartSerie == null)
						{
							num = 19;
							continue;
						}
						num = 40;
						continue;
					}
					case 18:
						if (A_0.NodeType == XmlNodeType.EndElement)
						{
							num = 28;
							continue;
						}
						num = 25;
						continue;
					case 19:
						if (true)
						{
						}
						num = 16;
						continue;
					case 20:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4AF;
						default:
						{
							if (false)
							{
							}
							string localName;
							if ((localName = A_0.LocalName) != null)
							{
								num = 3;
								continue;
							}
							goto IL_192;
						}
						}
						break;
					case 21:
						goto IL_344;
					case 22:
					{
						int? num4;
						chartFormat2.Overlap = num4.Value;
						num = 8;
						continue;
					}
					case 23:
						if (A_1 == null)
						{
							num = 13;
							continue;
						}
						num = 2;
						continue;
					case 24:
					{
						int? num3;
						chartFormat2.GapWidth = num3.Value;
						num = 37;
						continue;
					}
					case 25:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 5;
							continue;
						}
						goto IL_EB;
					case 26:
						goto IL_E6;
					case 27:
						goto IL_344;
					case 28:
						num = 17;
						continue;
					case 29:
					{
						int? num4;
						if (num4 != null)
						{
							num = 22;
							continue;
						}
						goto IL_52F;
					}
					case 30:
						goto IL_18D;
					case 31:
					{
						int? num3;
						if (num3 != null)
						{
							num = 24;
							continue;
						}
						goto IL_509;
					}
					case 32:
						num = 36;
						continue;
					case 33:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("⩄ㅆⱈ㥊⅌⹎⅐", a_)))
						{
							num = 38;
							continue;
						}
						string s2 = spr\u1AA0.ᜄ(A_0);
						int? num4 = new int?(int.Parse(s2));
						int? num2 = num4;
						num = 0;
						continue;
					}
					case 34:
						goto IL_4AF;
					case 35:
						flag = false;
						goto IL_4F3;
					case 36:
						goto IL_192;
					case 37:
						goto IL_509;
					case 38:
						num = 9;
						continue;
					case 39:
						num = 20;
						continue;
					case 40:
					{
						IChartSerie chartSerie;
						chartFormat = chartSerie.Format.Options;
						goto IL_203;
					}
					case 41:
						goto IL_344;
					}
					if (A_0 == null)
					{
						num = 26;
						continue;
					}
					num = 23;
					continue;
					IL_EB:
					A_0.Skip();
					num = 4;
					continue;
					IL_192:
					A_0.Skip();
					num = 41;
					continue;
					IL_203:
					chartFormat2 = chartFormat;
					num = 31;
					continue;
					IL_344:
					num = 18;
					continue;
					IL_4F3:
					if (flag)
					{
						num = 7;
						continue;
					}
					goto IL_344;
					IL_4AF:
					num = 6;
					continue;
					IL_509:
					num = 29;
				}
				IL_E6:
				throw new ArgumentNullException(RecordTableEnumerator.b("㝄≆⡈⽊⡌㵎", a_));
				IL_18D:
				throw new XmlException(RecordTableEnumerator.b("၄⥆ⱈ㍊㵌⩎㉐❒ご㍖祘⍚ぜ㍞䅠ᝢѤf䝨", a_));
				IL_248:
				goto IL_52F;
				IL_4D2:
				throw new ArgumentNullException(RecordTableEnumerator.b("♄⽆⡈㥊㥌", a_));
				IL_52F:
				A_0.Read();
				return;
			}
			}
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x0010C22C File Offset: 0x0010B22C
		private bool ᜀ(XmlReader A_0, List<XlsChartSerie> A_1, Dictionary<int, int> A_2)
		{
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				int num;
				int num2;
				int num3;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
				{
					IL_8E:
					XlsChartSerie xlsChartSerie = A_1[num];
					int index = xlsChartSerie.Index;
					A_2[index] = num2;
					num++;
					num3 = 9;
					break;
				}
				default:
					if (false)
					{
					}
					goto IL_63;
				}
				int count;
				bool result;
				for (;;)
				{
					IL_34:
					bool flag;
					switch (num3)
					{
					case 0:
						num3 = 3;
						continue;
					case 1:
					{
						int axisId;
						if (num2 != axisId)
						{
							num3 = 0;
							continue;
						}
						num3 = 4;
						continue;
					}
					case 2:
						if (count > 0)
						{
							num3 = 6;
							continue;
						}
						goto IL_179;
					case 3:
					{
						int axisId2;
						flag = (num2 != axisId2);
						goto IL_167;
					}
					case 4:
						flag = false;
						goto IL_167;
					case 5:
						goto IL_14F;
					case 6:
					{
						XlsChartSerie xlsChartSerie2 = A_1[0];
						XlsChart parentChart = xlsChartSerie2.ParentChart;
						int axisId = (parentChart.PrimaryValueAxis as XlsChartAxis).AxisId;
						int axisId2 = (parentChart.PrimaryCategoryAxis as XlsChartAxis).AxisId;
						num3 = 1;
						continue;
					}
					case 7:
						goto IL_12F;
					case 8:
						if (num >= count)
						{
							num3 = 5;
							continue;
						}
						goto IL_8E;
					case 9:
						goto IL_12F;
					}
					goto IL_63;
					IL_12F:
					num3 = 8;
					continue;
					IL_167:
					result = flag;
					num = 0;
					num3 = 7;
				}
				IL_14F:
				IL_179:
				A_1.Clear();
				return result;
				IL_63:
				num2 = spr\u1AA0.ᜂ(A_0);
				count = A_1.Count;
				result = false;
				num3 = 2;
				goto IL_34;
			}
			}
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x0010C3BC File Offset: 0x0010B3BC
		private void ᜈ(XmlReader A_0, XlsChart A_1, RelationsCollection A_2, Dictionary<int, int> A_3)
		{
			int a_ = 6;
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
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("嬻弽〿ف⅃㙅㱇≉", a_)))
						{
							num = 24;
							continue;
						}
						A_1.GapDepth = spr\u1AA0.ᜂ(A_0);
						num = 32;
						continue;
					}
					case 1:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("嬻弽〿ᕁⵃ≅㱇≉", a_)))
						{
							num = 25;
							continue;
						}
						string s = spr\u1AA0.ᜄ(A_0);
						int? num2 = new int?(int.Parse(s));
						num = 10;
						continue;
					}
					case 2:
					{
						int? num2;
						if (num2 != null)
						{
							num = 34;
							continue;
						}
						goto IL_479;
					}
					case 3:
						if (A_1 == null)
						{
							num = 28;
							continue;
						}
						num = 13;
						continue;
					case 5:
						goto IL_332;
					case 6:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("伻嘽ℿ㉁⅃", a_)))
						{
							num = 22;
							continue;
						}
						XlsChartSerie xlsChartSerie;
						this.ᜄ(A_0, xlsChartSerie);
						num = 29;
						continue;
					}
					case 7:
						goto IL_220;
					case 8:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 9;
							continue;
						}
						goto IL_137;
					case 9:
						if (true)
						{
						}
						num = 23;
						continue;
					case 10:
						goto IL_220;
					case 11:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("崻䘽ि♁", a_)))
						{
							num = 33;
							continue;
						}
						List<XlsChartSerie> list;
						this.ᜀ(A_0, list, A_3);
						num = 30;
						continue;
					}
					case 12:
						num = 2;
						continue;
					case 13:
					{
						if (A_0.LocalName != RecordTableEnumerator.b("帻弽㈿煁CՅ⁇⭉㹋㩍", a_))
						{
							num = 26;
							continue;
						}
						A_0.Read();
						List<XlsChartSerie> list = new List<XlsChartSerie>();
						string text = null;
						XlsChartSerie xlsChartSerie = this.ᜀ(A_0, A_1, A_2, true, list, out text);
						int? num2 = null;
						num = 15;
						continue;
					}
					case 14:
						num = 1;
						continue;
					case 15:
					{
						string text;
						if (text != null)
						{
							num = 19;
							continue;
						}
						goto IL_220;
					}
					case 16:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num = 14;
							continue;
						}
						goto IL_332;
					}
					case 17:
						num = 16;
						continue;
					case 18:
						goto IL_271;
					case 19:
					{
						XlsChartSerie xlsChartSerie;
						string text;
						this.ᜀ(text, xlsChartSerie);
						num = 20;
						continue;
					}
					case 20:
						goto IL_220;
					case 21:
						goto IL_220;
					case 22:
						num = 11;
						continue;
					case 23:
					{
						XlsChartSerie xlsChartSerie;
						if (xlsChartSerie != null)
						{
							num = 17;
							continue;
						}
						goto IL_137;
					}
					case 24:
						num = 6;
						continue;
					case 25:
						num = 0;
						continue;
					case 26:
						goto IL_2FC;
					case 27:
						if (A_0.NodeType == XmlNodeType.EndElement)
						{
							num = 12;
							continue;
						}
						num = 8;
						continue;
					case 28:
						goto IL_405;
					case 29:
						goto IL_220;
					case 30:
						goto IL_220;
					case 31:
						goto IL_CA;
					case 32:
						goto IL_220;
					case 33:
						num = 5;
						continue;
					case 34:
					{
						XlsChartSerie xlsChartSerie;
						IChartFormat options = xlsChartSerie.Format.Options;
						int? num2;
						options.GapWidth = num2.Value;
						num = 18;
						continue;
					}
					}
					if (A_0 == null)
					{
						num = 31;
						continue;
					}
					num = 3;
					continue;
					IL_137:
					A_0.Skip();
					num = 21;
					continue;
					IL_220:
					num = 27;
					continue;
					IL_332:
					A_0.Skip();
					num = 7;
				}
				IL_CA:
				throw new ArgumentNullException(RecordTableEnumerator.b("主嬽ℿ♁⅃㑅", a_));
				IL_123:
				throw new XmlException(RecordTableEnumerator.b("椻倽┿㩁㑃⍅⭇㹉⥋⩍灏⩑㥓㩕硗⹙㵛㥝也", a_));
				IL_271:
				goto IL_479;
				IL_2FC:
				goto IL_123;
				IL_405:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_123;
				default:
					if (false)
					{
					}
					throw new ArgumentNullException(RecordTableEnumerator.b("弻嘽ℿぁぃ", a_));
				}
				IL_479:
				A_0.Read();
				return;
			}
			}
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x0010C84C File Offset: 0x0010B84C
		private void ᜄ(XmlReader A_0, XlsChartSerie A_1)
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
					goto IL_A1;
				case 1:
					goto IL_8B;
				case 2:
					goto IL_62;
				}
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_62;
				default:
					if (false)
					{
					}
					if (A_0 == null)
					{
						num = 2;
					}
					else
					{
						num = 0;
					}
					break;
				}
			}
			IL_62:
			throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹崻娽┿ぁ", a_));
			IL_8B:
			throw new ArgumentNullException(RecordTableEnumerator.b("帷匹主䴽㐿ᅁ⅃㑅ⅇ⽉㽋", a_));
			IL_A1:
			string a_2 = spr\u1AA0.ᜄ(A_0);
			this.ᜀ(a_2, A_1);
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x0010C90C File Offset: 0x0010B90C
		private void ᜀ(string A_0, XlsChartSerie A_1)
		{
			int a_ = 3;
			int num = 9;
			IChartSerieDataFormat format;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_BC;
				case 1:
					num = 13;
					continue;
				case 2:
				{
					int num2;
					switch (num2)
					{
					case 0:
						goto IL_BE;
					case 1:
						goto IL_61;
					case 2:
						goto IL_16C;
					case 3:
						goto IL_91;
					case 4:
						goto IL_15D;
					case 5:
						goto IL_CD;
					default:
						num = 1;
						continue;
					}
					break;
				}
				case 3:
					if (A_0 != null)
					{
						num = 7;
						continue;
					}
					goto IL_27E;
				case 4:
					goto IL_DC;
				case 5:
					if (A_1 == null)
					{
						num = 0;
						continue;
					}
					A_1.GetCommonSerieFormat();
					format = A_1.Format;
					num = 3;
					continue;
				case 6:
				{
					int num2;
					if (!spr\u22D2.\u173E.TryGetValue(A_0, out num2))
					{
						goto IL_27E;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_DC;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 11;
						continue;
					}
					break;
				}
				case 7:
					num = 10;
					continue;
				case 8:
					spr\u22D2.\u173E = new Dictionary<string, int>(6)
					{
						{
							RecordTableEnumerator.b("娸吺匼娾", a_),
							0
						},
						{
							RecordTableEnumerator.b("䤸䈺似帾ⱀ⩂⅄", a_),
							1
						},
						{
							RecordTableEnumerator.b("娸吺匼娾ᕀⱂࡄ♆ㅈ", a_),
							2
						},
						{
							RecordTableEnumerator.b("䤸䈺似帾ⱀ⩂⅄ፆ♈يⱌ㝎", a_),
							3
						},
						{
							RecordTableEnumerator.b("娸䈺儼嘾⽀❂⁄㕆", a_),
							4
						},
						{
							RecordTableEnumerator.b("嬸吺䔼", a_),
							5
						}
					};
					num = 4;
					continue;
				case 10:
					if (spr\u22D2.\u173E == null)
					{
						num = 8;
						continue;
					}
					goto IL_DC;
				case 11:
					num = 2;
					continue;
				case 12:
					return;
				case 13:
					goto IL_78;
				}
				if (A_0 == null)
				{
					num = 12;
					continue;
				}
				num = 5;
				continue;
				IL_DC:
				num = 6;
			}
			return;
			IL_61:
			format.BarTopType = TopFormatType.Sharp;
			format.BarType = BaseFormatType.Rectangle;
			return;
			IL_78:
			goto IL_27E;
			IL_91:
			format.BarTopType = TopFormatType.Trunc;
			format.BarType = BaseFormatType.Rectangle;
			return;
			IL_BC:
			throw new ArgumentNullException(RecordTableEnumerator.b("弸刺似䰾㕀၂⁄㕆⁈⹊㹌", a_));
			IL_BE:
			format.BarTopType = TopFormatType.Sharp;
			format.BarType = BaseFormatType.Circle;
			return;
			IL_CD:
			format.BarTopType = TopFormatType.Straight;
			format.BarType = BaseFormatType.Rectangle;
			return;
			IL_15D:
			format.BarTopType = TopFormatType.Straight;
			format.BarType = BaseFormatType.Circle;
			return;
			IL_16C:
			format.BarTopType = TopFormatType.Trunc;
			format.BarType = BaseFormatType.Circle;
			return;
			IL_27E:
			throw new XmlException();
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x0010CB9C File Offset: 0x0010BB9C
		private XlsChartSerie ᜀ(XmlReader A_0, XlsChart A_1, RelationsCollection A_2, bool A_3, List<XlsChartSerie> A_4, out string A_5)
		{
			int a_ = 2;
			switch (0)
			{
			default:
			{
				int num = 6;
				XlsChartSerie result;
				for (;;)
				{
					XmlReader xmlReader;
					XmlWriter xmlWriter;
					bool flag;
					switch (num)
					{
					case 0:
						goto IL_3A8;
					case 1:
						goto IL_3A8;
					case 2:
					{
						string text;
						string text2;
						A_1.PivotChartType = this.ᜀ(text, text2, A_5, A_3);
						num = 9;
						continue;
					}
					case 3:
						goto IL_18C;
					case 4:
					{
						string localName;
						int num2;
						if (spr\u22D2.\u173F.TryGetValue(localName, out num2))
						{
							num = 10;
							continue;
						}
						goto IL_179;
					}
					case 5:
					{
						xmlReader.Read();
						string text;
						string text2;
						this.ᜀ(xmlReader, text, text2, A_5, A_3, A_4, A_1, A_2, ref result);
						num = 3;
						continue;
					}
					case 7:
						num = 12;
						continue;
					case 8:
						goto IL_3A8;
					case 9:
						goto IL_24F;
					case 10:
						num = 30;
						continue;
					case 11:
						goto IL_46E;
					case 12:
						goto IL_179;
					case 13:
						if (A_1.HasPivotTable)
						{
							num = 2;
							continue;
						}
						return result;
					case 14:
						if (true)
						{
						}
						num = 23;
						continue;
					case 15:
						goto IL_3A8;
					case 16:
					{
						xmlWriter.WriteEndElement();
						xmlWriter.Flush();
						MemoryStream memoryStream;
						memoryStream.Position = 0L;
						xmlReader = UtilityMethods.ᜀ(memoryStream);
						num = 29;
						continue;
					}
					case 17:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							goto IL_1F1;
						}
						goto IL_179;
					}
					case 18:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 24;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1F1;
						default:
							if (false)
							{
							}
							A_0.Skip();
							num = 22;
							continue;
						}
						break;
					case 19:
						spr\u22D2.\u173F = new Dictionary<string, int>(6)
						{
							{
								RecordTableEnumerator.b("娷嬹主稽⤿ぁ", a_),
								0
							},
							{
								RecordTableEnumerator.b("強䠹医䬽〿⭁⩃ⅅ", a_),
								1
							},
							{
								RecordTableEnumerator.b("丷嬹主䜽̿ⵁ⡃⥅㩇㥉", a_),
								2
							},
							{
								RecordTableEnumerator.b("䬷刹崻丽┿", a_),
								3
							},
							{
								RecordTableEnumerator.b("䬷弹主", a_),
								4
							},
							{
								RecordTableEnumerator.b("尷瘹帻刽㌿", a_),
								5
							}
						};
						num = 28;
						continue;
					case 20:
						goto IL_3A8;
					case 21:
						goto IL_3A8;
					case 22:
						goto IL_3A8;
					case 23:
						if (spr\u22D2.\u173F == null)
						{
							num = 19;
							continue;
						}
						goto IL_254;
					case 24:
						num = 17;
						continue;
					case 25:
						if (!flag)
						{
							num = 16;
							continue;
						}
						num = 18;
						continue;
					case 26:
						goto IL_3A8;
					case 27:
					{
						if (A_1 == null)
						{
							num = 11;
							continue;
						}
						flag = true;
						result = null;
						string text = null;
						string text2 = null;
						A_5 = null;
						MemoryStream memoryStream = new MemoryStream();
						xmlWriter = UtilityMethods.ᜀ(memoryStream, Encoding.UTF8);
						xmlWriter.WriteStartElement(RecordTableEnumerator.b("䨷唹医䨽", a_));
						num = 1;
						continue;
					}
					case 28:
						goto IL_254;
					case 29:
						if (!xmlReader.IsEmptyElement)
						{
							num = 5;
							continue;
						}
						goto IL_18C;
					case 30:
					{
						int num2;
						switch (num2)
						{
						case 0:
						{
							string text = spr\u1AA0.ᜄ(A_0);
							num = 26;
							continue;
						}
						case 1:
						{
							string text2 = spr\u1AA0.ᜄ(A_0);
							num = 21;
							continue;
						}
						case 2:
							spr\u1AA0.ᜃ(A_0);
							num = 20;
							continue;
						case 3:
							A_5 = spr\u1AA0.ᜄ(A_0);
							num = 8;
							continue;
						case 4:
							xmlWriter.WriteNode(A_0, false);
							num = 0;
							continue;
						case 5:
							A_0.Skip();
							num = 32;
							continue;
						default:
							num = 7;
							continue;
						}
						break;
					}
					case 31:
						goto IL_C2;
					case 32:
						goto IL_3A8;
					}
					if (A_0 == null)
					{
						num = 31;
						continue;
					}
					num = 27;
					continue;
					IL_179:
					flag = false;
					num = 15;
					continue;
					IL_18C:
					xmlReader.Close();
					xmlWriter.Close();
					num = 13;
					continue;
					IL_1F1:
					num = 14;
					continue;
					IL_254:
					num = 4;
					continue;
					IL_3A8:
					num = 25;
				}
				IL_C2:
				throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹崻娽┿ぁ", a_));
				IL_24F:
				return result;
				IL_46E:
				throw new ArgumentNullException(RecordTableEnumerator.b("嬷刹崻䰽㐿", a_));
			}
			}
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x0010D068 File Offset: 0x0010C068
		private void ᜀ(XmlReader A_0, string A_1, string A_2, string A_3, bool A_4, List<XlsChartSerie> A_5, XlsChart A_6, RelationsCollection A_7, ref XlsChartSerie A_8)
		{
			int a_ = 3;
			int num = 3;
			for (;;)
			{
				XlsChartSerie xlsChartSerie;
				switch (num)
				{
				case 0:
					if (A_8 == null)
					{
						num = 11;
						continue;
					}
					goto IL_55;
				case 1:
					goto IL_55;
				case 5:
					if (A_0.LocalName == RecordTableEnumerator.b("䨸帺似", a_))
					{
						num = 9;
						continue;
					}
					goto IL_112;
				case 6:
					goto IL_F9;
				case 7:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 10;
						continue;
					}
					num = 6;
					continue;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_F9;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 9:
				{
					ExcelChartType a_2 = this.ᜀ(A_1, A_2, A_3, A_4);
					xlsChartSerie = this.ᜂ(A_0, A_6, a_2, A_7);
					num = 0;
					continue;
				}
				case 10:
					return;
				case 11:
					A_8 = xlsChartSerie;
					num = 1;
					continue;
				}
				goto IL_53;
				IL_55:
				A_5.Add(xlsChartSerie);
				if (true)
				{
				}
				num = 2;
				continue;
				IL_CC:
				num = 7;
				continue;
				IL_53:
				goto IL_CC;
				IL_F9:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 8;
					continue;
				}
				IL_112:
				A_0.Read();
				num = 4;
			}
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x0010D1D8 File Offset: 0x0010C1D8
		private ExcelChartType ᜀ(string A_0, string A_1, bool A_2, string A_3)
		{
			int a_ = 2;
			string text;
			for (;;)
			{
				text = null;
				int num = 11;
				for (;;)
				{
					string text2;
					string text3;
					switch (num)
					{
					case 0:
						goto IL_14B;
					case 1:
						num = 2;
						continue;
					case 2:
						if (!(A_1 == RecordTableEnumerator.b("䠷弹主崽┿ⱁぃᕅ㱇⭉⽋╍㕏㙑", a_)))
						{
							num = 6;
							continue;
						}
						text += RecordTableEnumerator.b("षਹ఻渽┿ぁ❃⍅♇㹉Ὃ㩍ㅏㅑ㽓㍕㱗", a_);
						num = 9;
						continue;
					case 3:
						goto IL_1D4;
					case 4:
						if (!(A_0 == RecordTableEnumerator.b("娷嬹主", a_)))
						{
							num = 13;
							continue;
						}
						num = 16;
						continue;
					case 5:
						if (!(A_3 == RecordTableEnumerator.b("娷唹䐻", a_)))
						{
							num = 10;
							continue;
						}
						num = 22;
						continue;
					case 6:
						num = 20;
						continue;
					case 7:
						text2 = RecordTableEnumerator.b("笷唹刻嬽", a_);
						goto IL_248;
					case 8:
						goto IL_27A;
					case 9:
						goto IL_193;
					case 10:
						num = 7;
						continue;
					case 11:
						if (!A_2)
						{
							num = 23;
							continue;
						}
						num = 5;
						continue;
					case 12:
						text3 = RecordTableEnumerator.b("笷唹倻䬽ⴿⱁ", a_);
						goto IL_1FE;
					case 13:
						num = 12;
						continue;
					case 14:
						num = 0;
						continue;
					case 15:
						if (A_1 != null)
						{
							num = 28;
							continue;
						}
						goto IL_14B;
					case 16:
						text3 = RecordTableEnumerator.b("稷嬹主", a_);
						goto IL_1FE;
					case 17:
						goto IL_27A;
					case 18:
						goto IL_1F9;
					case 19:
						if (A_0 == RecordTableEnumerator.b("娷嬹主", a_))
						{
							num = 21;
							continue;
						}
						goto IL_27A;
					case 20:
						if (!(A_1 == RecordTableEnumerator.b("䬷丹崻崽⬿❁⁃", a_)))
						{
							num = 14;
							continue;
						}
						text += RecordTableEnumerator.b("欷丹崻崽⬿❁⁃", a_);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1EE;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 21:
						text += RecordTableEnumerator.b("稷嬹主", a_);
						num = 17;
						continue;
					case 22:
						text2 = RecordTableEnumerator.b("笷唹倻䬽ⴿⱁ", a_);
						goto IL_248;
					case 23:
						num = 4;
						continue;
					case 24:
						goto IL_2DB;
					case 25:
						goto IL_387;
					case 26:
						text += RecordTableEnumerator.b("ଷ縹缻刽㔿ㅁぃ⍅㩇⽉⡋", a_);
						goto IL_1EE;
					case 27:
						if (A_2)
						{
							num = 26;
							continue;
						}
						text += RecordTableEnumerator.b("笷嘹䤻䴽㐿❁㙃⍅ⱇ", a_);
						num = 25;
						continue;
					case 28:
						num = 29;
						continue;
					case 29:
						if (!(A_1 == RecordTableEnumerator.b("嬷嘹䤻䴽㐿❁㙃⍅ⱇ", a_)))
						{
							num = 1;
							continue;
						}
						text += RecordTableEnumerator.b("笷嘹䤻䴽㐿❁㙃⍅ⱇ", a_);
						num = 24;
						continue;
					}
					break;
					IL_14B:
					num = 27;
					continue;
					IL_1EE:
					num = 18;
					continue;
					IL_1FE:
					text = text3;
					if (true)
					{
					}
					num = 8;
					continue;
					IL_248:
					text = text2;
					num = 19;
					continue;
					IL_27A:
					num = 15;
				}
			}
			IL_193:
			IL_1D4:
			IL_1F9:
			IL_2DB:
			IL_387:
			return (ExcelChartType)Enum.Parse(typeof(ExcelChartType), text, false);
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x0010D5BC File Offset: 0x0010C5BC
		private ExcelChartType ᜀ(string A_0, string A_1, string A_2, bool A_3)
		{
			int a_ = 7;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_3CE:
				if (A_2 == null)
				{
					num = 40;
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
				switch (0)
				{
				default:
					goto IL_15B;
				}
				break;
			}
			string[] array;
			string text2;
			for (;;)
			{
				IL_40:
				string text;
				string text3;
				switch (num)
				{
				case 0:
					goto IL_566;
				case 1:
					if (A_1 != null)
					{
						num = 44;
						continue;
					}
					goto IL_6C4;
				case 2:
					text = A_2;
					goto IL_518;
				case 3:
					text = A_2 + RecordTableEnumerator.b("戼紾⁀ㅂ", a_);
					goto IL_518;
				case 4:
					if (!(A_0 == RecordTableEnumerator.b("弼帾㍀", a_)))
					{
						num = 65;
						continue;
					}
					num = 39;
					continue;
				case 5:
					goto IL_458;
				case 6:
					goto IL_201;
				case 7:
					if (!(A_1 == RecordTableEnumerator.b("帼匾㑀あㅄ≆㭈⹊⥌", a_)))
					{
						num = 58;
						continue;
					}
					num = 16;
					continue;
				case 8:
					num = 14;
					continue;
				case 9:
					if (!(A_1 == RecordTableEnumerator.b("䴼娾㍀⁂⁄⥆㵈ᡊ㥌⹎㉐㡒ご㍖", a_)))
					{
						num = 24;
						continue;
					}
					num = 48;
					continue;
				case 10:
					if (A_2 != null)
					{
						num = 46;
						continue;
					}
					num = 15;
					continue;
				case 11:
					goto IL_566;
				case 12:
					num = 51;
					continue;
				case 13:
					if (A_3)
					{
						num = 57;
						continue;
					}
					goto IL_2A2;
				case 14:
					if (Array.IndexOf<string>(array, A_2) == -1)
					{
						num = 60;
						continue;
					}
					goto IL_540;
				case 15:
					if (!(A_0 == RecordTableEnumerator.b("弼帾㍀", a_)))
					{
						num = 31;
						continue;
					}
					num = 50;
					continue;
				case 16:
					if (A_3)
					{
						num = 67;
						continue;
					}
					goto IL_1E0;
				case 17:
					goto IL_6AE;
				case 18:
					num = 26;
					continue;
				case 19:
					if (A_3)
					{
						num = 43;
						continue;
					}
					goto IL_27C;
				case 20:
					if (A_2 != null)
					{
						num = 55;
						continue;
					}
					goto IL_2C8;
				case 21:
					goto IL_7C2;
				case 22:
					text2 += RecordTableEnumerator.b("฼笾ɀ⽂い㑆㵈⹊㽌⩎㕐", a_);
					num = 42;
					continue;
				case 23:
					text3 = RecordTableEnumerator.b("縼倾ⵀ㙂⡄⥆", a_);
					goto IL_766;
				case 24:
					num = 25;
					continue;
				case 25:
					if (!(A_1 == RecordTableEnumerator.b("丼䬾⁀⁂⹄≆ⵈ", a_)))
					{
						num = 18;
						continue;
					}
					num = 13;
					continue;
				case 26:
					goto IL_6C4;
				case 27:
					if (Array.IndexOf<string>(array, A_2) == -1)
					{
						num = 34;
						continue;
					}
					goto IL_2A2;
				case 28:
					goto IL_561;
				case 29:
					if (A_2 != null)
					{
						num = 35;
						continue;
					}
					goto IL_437;
				case 30:
					text = RecordTableEnumerator.b("缼帾㍀", a_);
					goto IL_518;
				case 31:
					num = 59;
					continue;
				case 32:
					if (Array.IndexOf<string>(array, A_2) == -1)
					{
						num = 21;
						continue;
					}
					goto IL_27C;
				case 33:
					if (Array.IndexOf<string>(array, A_2) == -1)
					{
						num = 56;
						continue;
					}
					goto IL_566;
				case 34:
					goto IL_437;
				case 35:
					num = 27;
					continue;
				case 36:
					if (Array.IndexOf<string>(array, A_2) == -1)
					{
						num = 66;
						continue;
					}
					goto IL_1E0;
				case 37:
					if (A_2 != null)
					{
						num = 52;
						continue;
					}
					goto IL_7C2;
				case 38:
					goto IL_7E3;
				case 39:
					text3 = RecordTableEnumerator.b("缼帾㍀", a_);
					goto IL_766;
				case 40:
					goto IL_206;
				case 41:
					if (!(A_1 == RecordTableEnumerator.b("丼䬾⁀ⵂ⅄♆㭈⽊", a_)))
					{
						num = 47;
						continue;
					}
					num = 19;
					continue;
				case 42:
					goto IL_73E;
				case 43:
					num = 37;
					continue;
				case 44:
					num = 7;
					continue;
				case 45:
					num = 3;
					continue;
				case 46:
					num = 33;
					continue;
				case 47:
					num = 9;
					continue;
				case 48:
					if (A_3)
					{
						num = 12;
						continue;
					}
					goto IL_540;
				case 49:
					if (A_3)
					{
						num = 22;
						continue;
					}
					text2 += RecordTableEnumerator.b("縼匾㑀あㅄ≆㭈⹊⥌", a_);
					num = 61;
					continue;
				case 50:
					if (A_2 != null)
					{
						num = 45;
						continue;
					}
					num = 30;
					continue;
				case 51:
					if (A_2 != null)
					{
						num = 8;
						continue;
					}
					goto IL_68D;
				case 52:
					num = 32;
					continue;
				case 53:
					text = RecordTableEnumerator.b("縼倾ⵀ㙂⡄⥆", a_);
					goto IL_518;
				case 54:
					num = 62;
					continue;
				case 55:
					num = 36;
					continue;
				case 56:
					num = 4;
					continue;
				case 57:
					num = 29;
					continue;
				case 58:
					num = 41;
					continue;
				case 59:
					if (A_0 == RecordTableEnumerator.b("帼倾ⵀ", a_))
					{
						num = 54;
						continue;
					}
					goto IL_206;
				case 60:
					goto IL_68D;
				case 61:
					goto IL_35C;
				case 62:
					goto IL_3CE;
				case 63:
					goto IL_2C3;
				case 64:
					goto IL_2E9;
				case 65:
					num = 23;
					continue;
				case 66:
					goto IL_2C8;
				case 67:
					num = 20;
					continue;
				case 68:
					goto IL_29D;
				}
				goto IL_15B;
				IL_1E0:
				text2 += RecordTableEnumerator.b("縼匾㑀あㅄ≆㭈⹊⥌", a_);
				num = 6;
				continue;
				IL_206:
				num = 53;
				continue;
				IL_27C:
				text2 += RecordTableEnumerator.b("฼笾ɀ⽂い㑆㵈⹊㽌⩎㕐", a_);
				num = 68;
				continue;
				IL_2A2:
				text2 += RecordTableEnumerator.b("渼䬾⁀⁂⹄≆ⵈ", a_);
				num = 63;
				continue;
				IL_2C8:
				text2 += RecordTableEnumerator.b("฼笾ɀ⽂い㑆㵈⹊㽌⩎㕐", a_);
				num = 64;
				continue;
				IL_437:
				text2 += RecordTableEnumerator.b("฼笾ቀ㝂⑄⑆≈⹊⥌", a_);
				num = 5;
				continue;
				IL_518:
				text2 = text;
				num = 11;
				continue;
				IL_540:
				text2 += RecordTableEnumerator.b("఼༾煀ፂ⁄㕆⩈⹊⍌㭎ɐ❒㑔㑖㉘㹚㥜", a_);
				num = 28;
				continue;
				IL_566:
				num = 1;
				continue;
				IL_68D:
				text2 += RecordTableEnumerator.b("฼笾灀獂畄ᝆⱈ㥊⹌⩎㽐❒ٔ⍖㡘㡚㙜㩞ՠ", a_);
				num = 17;
				continue;
				IL_6C4:
				num = 49;
				continue;
				IL_766:
				text2 = text3;
				num = 0;
				continue;
				IL_7C2:
				text2 += RecordTableEnumerator.b("฼笾", a_);
				num = 38;
			}
			IL_201:
			IL_29D:
			IL_2C3:
			IL_2E9:
			IL_35C:
			IL_458:
			IL_561:
			IL_6AE:
			IL_73E:
			IL_7E3:
			return (ExcelChartType)Enum.Parse(typeof(ExcelChartType), text2, true);
			IL_15B:
			text2 = null;
			array = new string[]
			{
				RecordTableEnumerator.b("縼倾⽀♂", a_),
				RecordTableEnumerator.b("縼䘾ⵀ⩂⭄⍆ⱈ㥊", a_),
				RecordTableEnumerator.b("洼䘾㍀≂⡄⹆ⵈ", a_)
			};
			num = 10;
			goto IL_40;
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x0010DDEC File Offset: 0x0010CDEC
		private ExcelChartType ᜁ(string A_0, bool A_1)
		{
			int a_ = 11;
			string text;
			for (;;)
			{
				text = RecordTableEnumerator.b("@ㅂ⁄♆", a_);
				int num = 8;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_82;
					case 1:
						text += RecordTableEnumerator.b("牀݂", a_);
						num = 7;
						continue;
					case 2:
						goto IL_154;
					case 3:
						if (!(A_0 == RecordTableEnumerator.b("≀⽂い㑆㵈⹊㽌⩎㕐", a_)))
						{
							num = 6;
							continue;
						}
						goto IL_1C4;
					case 4:
						goto IL_100;
					case 5:
						num = 3;
						continue;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_154;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 7:
						goto IL_BD;
					case 8:
						if (A_1)
						{
							num = 1;
							continue;
						}
						goto IL_BD;
					case 9:
						goto IL_147;
					case 10:
						if (A_0 != null)
						{
							num = 5;
							continue;
						}
						goto IL_1C4;
					case 11:
						if (true)
						{
						}
						num = 0;
						continue;
					case 12:
						if (!(A_0 == RecordTableEnumerator.b("㉀㝂⑄⑆≈⹊⥌", a_)))
						{
							num = 11;
							continue;
						}
						text += RecordTableEnumerator.b("ቀ㝂⑄⑆≈⹊⥌", a_);
						num = 4;
						continue;
					case 13:
						num = 12;
						continue;
					}
					break;
					IL_BD:
					num = 10;
					continue;
					IL_154:
					if (!(A_0 == RecordTableEnumerator.b("ㅀ♂㝄⑆ⱈ╊㥌ᱎ═㉒㙔㱖㱘㽚", a_)))
					{
						num = 13;
					}
					else
					{
						text += RecordTableEnumerator.b("灀獂畄ᝆⱈ㥊⹌⩎㽐❒ٔ⍖㡘㡚㙜㩞ՠ", a_);
						num = 9;
					}
				}
			}
			IL_82:
			IL_100:
			IL_147:
			IL_1C4:
			return (ExcelChartType)Enum.Parse(typeof(ExcelChartType), text, false);
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x0010DFD8 File Offset: 0x0010CFD8
		private ExcelChartType ᜀ(string A_0, bool A_1)
		{
			int a_ = 13;
			ExcelChartType result;
			for (;;)
			{
				string text = RecordTableEnumerator.b("གⱄ⥆ⱈ", a_);
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_176:
					if (true)
					{
					}
					if (!(A_0 == RecordTableEnumerator.b("あㅄ♆⩈⁊⡌⭎", a_)))
					{
						num = 9;
					}
					else
					{
						text += RecordTableEnumerator.b("၂ㅄ♆⩈⁊⡌⭎", a_);
						num = 5;
					}
					break;
				default:
					if (false)
					{
					}
					num = 7;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_13A;
					case 1:
						num = 11;
						continue;
					case 2:
						goto IL_176;
					case 3:
						num = 10;
						continue;
					case 4:
						num = 2;
						continue;
					case 5:
						goto IL_13A;
					case 6:
						goto IL_13A;
					case 7:
						if (!A_1)
						{
							num = 3;
							continue;
						}
						result = ExcelChartType.Line3D;
						num = 12;
						continue;
					case 8:
						return result;
					case 9:
						num = 6;
						continue;
					case 10:
						if (A_0 != null)
						{
							num = 1;
							continue;
						}
						goto IL_13A;
					case 11:
						if (!(A_0 == RecordTableEnumerator.b("㍂⁄㕆⩈⹊⍌㭎ɐ❒㑔㑖㉘㹚㥜", a_)))
						{
							num = 4;
							continue;
						}
						text += RecordTableEnumerator.b("牂畄睆᥈⹊㽌ⱎ㑐㵒⅔іⵘ㩚㹜㑞Ѡݢ", a_);
						num = 0;
						continue;
					case 12:
						return result;
					}
					break;
					IL_13A:
					result = (ExcelChartType)Enum.Parse(typeof(ExcelChartType), text, false);
					num = 8;
				}
			}
			return result;
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x0010E18C File Offset: 0x0010D18C
		private void ᜇ(XmlReader A_0, XlsChart A_1, RelationsCollection A_2, Dictionary<int, int> A_3)
		{
			int a_ = 14;
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_13F;
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
						break;
					}
					num = 14;
					continue;
				case 2:
					goto IL_162;
				case 3:
					goto IL_109;
				case 4:
				{
					if (true)
					{
					}
					List<XlsChartSerie> list;
					this.ᜀ(A_0, list, A_3);
					num = 0;
					continue;
				}
				case 5:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 1;
						continue;
					}
					goto IL_1C0;
				}
				case 6:
					goto IL_13F;
				case 7:
					if (A_1 == null)
					{
						num = 3;
						continue;
					}
					num = 9;
					continue;
				case 9:
				{
					if (A_0.LocalName != RecordTableEnumerator.b("╃㑅ⵇ⭉罋੍ፏ㩑㕓⑕ⱗ", a_))
					{
						num = 10;
						continue;
					}
					A_0.Read();
					List<XlsChartSerie> list = new List<XlsChartSerie>();
					this.ᜀ(A_0, A_1, true, A_2, list, true);
					num = 16;
					continue;
				}
				case 10:
					goto IL_1BE;
				case 11:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 2;
						continue;
					}
					num = 13;
					continue;
				case 12:
					goto IL_13F;
				case 13:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 17;
						continue;
					}
					A_0.Read();
					num = 6;
					continue;
				case 14:
				{
					string localName;
					if (localName == RecordTableEnumerator.b("╃㹅Ň⹉", a_))
					{
						num = 4;
						continue;
					}
					goto IL_1C0;
				}
				case 15:
					goto IL_6F;
				case 16:
					goto IL_13F;
				case 17:
					num = 5;
					continue;
				}
				if (A_0 == null)
				{
					num = 15;
					continue;
				}
				num = 7;
				continue;
				IL_13F:
				num = 11;
				continue;
				IL_1C0:
				A_0.Skip();
				num = 12;
			}
			IL_6F:
			throw new ArgumentNullException(RecordTableEnumerator.b("㙃⍅⥇⹉⥋㱍", a_));
			IL_109:
			throw new ArgumentNullException(RecordTableEnumerator.b("❃⹅⥇㡉㡋", a_));
			IL_162:
			A_0.Read();
			return;
			IL_1BE:
			throw new XmlException(RecordTableEnumerator.b("ᅃ⡅ⵇ㉉㱋⭍㍏♑ㅓ㉕硗≙ㅛ㉝䁟ᙡգť䙧", a_));
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x0010E3E0 File Offset: 0x0010D3E0
		private void ᜆ(XmlReader A_0, XlsChart A_1, RelationsCollection A_2, Dictionary<int, int> A_3)
		{
			int a_ = 5;
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_13B;
				case 1:
					goto IL_191;
				case 2:
					goto IL_13B;
				case 3:
				{
					List<XlsChartSerie> list;
					bool flag = this.ᜀ(A_0, list, A_3);
					num = 7;
					continue;
				}
				case 4:
					if (A_1 == null)
					{
						num = 5;
						continue;
					}
					num = 6;
					continue;
				case 5:
					goto IL_D1;
				case 6:
				{
					if (A_0.LocalName != RecordTableEnumerator.b("娺似娾⁀Bⵄ♆㭈㽊", a_))
					{
						num = 1;
						continue;
					}
					A_0.Read();
					List<XlsChartSerie> list = new List<XlsChartSerie>();
					bool flag = this.ᜀ(A_1, ref A_0);
					this.ᜀ(A_0, A_1, false, A_2, list, !flag);
					num = 2;
					continue;
				}
				case 7:
					goto IL_13B;
				case 9:
					return;
				case 10:
					if (A_0.LocalName == RecordTableEnumerator.b("娺䔼瘾╀", a_))
					{
						num = 3;
						continue;
					}
					A_0.Skip();
					num = 0;
					continue;
				case 11:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 9;
						continue;
					}
					if (true)
					{
					}
					num = 10;
					continue;
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
				IL_13B:
				num = 11;
			}
			IL_58:
			throw new ArgumentNullException(RecordTableEnumerator.b("䤺堼帾╀♂㝄", a_));
			IL_D1:
			throw new ArgumentNullException(RecordTableEnumerator.b("堺唼帾㍀㝂", a_));
			IL_191:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			default:
				if (false)
				{
				}
				throw new XmlException(RecordTableEnumerator.b("渺匼娾㥀㍂⁄⑆㵈⹊⥌潎⥐㹒㥔睖ⵘ㩚㩜煞", a_));
			}
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x0010E5C4 File Offset: 0x0010D5C4
		private bool ᜀ(XlsChart A_0, ref XmlReader A_1)
		{
			int a_ = 16;
			switch (0)
			{
			default:
			{
				MemoryStream memoryStream;
				XmlWriter xmlWriter;
				bool result;
				for (;;)
				{
					memoryStream = new MemoryStream();
					xmlWriter = UtilityMethods.ᜀ(memoryStream, Encoding.UTF8);
					xmlWriter.WriteStartElement(RecordTableEnumerator.b("㑅❇╉㡋", a_));
					result = false;
					int num = 10;
					for (;;)
					{
						int num2;
						bool flag;
						string localName;
						switch (num)
						{
						case 0:
							goto IL_A3;
						case 1:
							if (A_1.LocalName == RecordTableEnumerator.b("❅ぇ͉⡋", a_))
							{
								num = 13;
								continue;
							}
							num = 5;
							continue;
						case 2:
							goto IL_A3;
						case 3:
						{
							int axisId;
							if (num2 != axisId)
							{
								num = 9;
								continue;
							}
							num = 12;
							continue;
						}
						case 4:
							goto IL_C9;
						case 5:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_215;
							default:
								if (false)
								{
								}
								if (A_1.NodeType == XmlNodeType.Element)
								{
									num = 6;
									continue;
								}
								A_1.Skip();
								num = 2;
								continue;
							}
							break;
						case 6:
							goto IL_215;
						case 7:
						{
							int axisId2;
							flag = (num2 != axisId2);
							goto IL_190;
						}
						case 8:
							goto IL_A3;
						case 9:
							if (true)
							{
							}
							num = 7;
							continue;
						case 10:
							goto IL_A3;
						case 11:
							if (A_1.NodeType == XmlNodeType.EndElement)
							{
								num = 4;
								continue;
							}
							num = 1;
							continue;
						case 12:
							flag = false;
							goto IL_190;
						case 13:
						{
							localName = A_1.LocalName;
							num2 = spr\u1AA0.ᜂ(A_1);
							int axisId = (A_0.PrimaryValueAxis as XlsChartAxis).AxisId;
							int axisId2 = (A_0.PrimaryCategoryAxis as XlsChartAxis).AxisId;
							num = 3;
							continue;
						}
						}
						break;
						IL_A3:
						num = 11;
						continue;
						IL_190:
						result = flag;
						spr\u1CFF.ᜀ(xmlWriter, localName, num2.ToString());
						num = 0;
						continue;
						IL_215:
						xmlWriter.WriteNode(A_1, false);
						num = 8;
					}
				}
				IL_C9:
				xmlWriter.WriteEndElement();
				xmlWriter.Flush();
				A_1.Read();
				memoryStream.Position = 0L;
				A_1 = UtilityMethods.ᜀ(memoryStream);
				A_1.Read();
				return result;
			}
			}
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x0010E818 File Offset: 0x0010D818
		private void ᜀ(XmlReader A_0, XlsChart A_1, bool A_2, RelationsCollection A_3, List<XlsChartSerie> A_4, bool A_5)
		{
			int a_ = 17;
			switch (0)
			{
			default:
			{
				int num = 13;
				for (;;)
				{
					bool flag;
					switch (num)
					{
					case 0:
						num = 7;
						continue;
					case 1:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 5;
							continue;
						}
						goto IL_119;
					case 2:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("ㅆ⡈㥊㑌౎㹐㽒㩔╖⩘", a_)))
						{
							num = 24;
							continue;
						}
						spr\u1AA0.ᜃ(A_0);
						num = 18;
						continue;
					}
					case 3:
						if (A_0.NodeType != XmlNodeType.EndElement)
						{
							num = 14;
							continue;
						}
						goto IL_181;
					case 4:
						goto IL_181;
					case 5:
						num = 32;
						continue;
					case 6:
						goto IL_3EB;
					case 7:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("⁆㭈⑊㡌㽎㡐㵒㉔", a_)))
						{
							num = 11;
							continue;
						}
						string a_2 = spr\u1AA0.ᜄ(A_0);
						num = 16;
						continue;
					}
					case 8:
						goto IL_119;
					case 9:
						goto IL_119;
					case 10:
					{
						string a_2;
						A_1.PivotChartType = this.ᜁ(a_2, A_2);
						num = 20;
						continue;
					}
					case 11:
						num = 2;
						continue;
					case 12:
						goto IL_119;
					case 14:
						num = 15;
						continue;
					case 15:
						if (!flag)
						{
							num = 4;
							continue;
						}
						num = 1;
						continue;
					case 16:
						goto IL_119;
					case 17:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("⍆Ո⥊⅌㱎", a_)))
						{
							num = 28;
							continue;
						}
						A_0.Skip();
						num = 12;
						continue;
					}
					case 18:
						goto IL_119;
					case 19:
						goto IL_119;
					case 20:
						goto IL_252;
					case 21:
						goto IL_119;
					case 22:
						goto IL_C2;
					case 23:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("⍆㭈⑊㵌͎㡐㵒ご⑖", a_)))
						{
							num = 30;
							continue;
						}
						A_0.Skip();
						num = 21;
						continue;
					}
					case 24:
						num = 31;
						continue;
					case 25:
					{
						if (A_1 == null)
						{
							num = 6;
							continue;
						}
						flag = true;
						string a_2 = null;
						num = 19;
						continue;
					}
					case 26:
						if (A_1.HasPivotTable)
						{
							num = 10;
							continue;
						}
						return;
					case 27:
						goto IL_346;
					case 28:
						num = 23;
						continue;
					case 29:
						num = 17;
						continue;
					case 30:
						num = 27;
						continue;
					case 31:
					{
						string localName;
						if (localName == RecordTableEnumerator.b("㑆ⱈ㥊", a_))
						{
							string a_2;
							ExcelChartType a_3 = this.ᜁ(a_2, A_2);
							XlsChartSerie item = this.ᜀ(A_0, A_1, a_3, A_3, A_5);
							A_4.Add(item);
							num = 9;
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
							if (true)
							{
							}
							num = 29;
							continue;
						}
						break;
					}
					case 32:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							goto IL_1FA;
						}
						goto IL_346;
					}
					}
					if (A_0 == null)
					{
						num = 22;
						continue;
					}
					num = 25;
					continue;
					IL_119:
					num = 3;
					continue;
					IL_181:
					num = 26;
					continue;
					IL_1FA:
					num = 0;
					continue;
					IL_346:
					flag = false;
					num = 8;
				}
				IL_C2:
				throw new ArgumentNullException(RecordTableEnumerator.b("㕆ⱈ⩊⥌⩎⍐", a_));
				IL_252:
				return;
				IL_3EB:
				throw new ArgumentNullException(RecordTableEnumerator.b("⑆ⅈ⩊㽌㭎", a_));
			}
			}
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x0010EC18 File Offset: 0x0010DC18
		private XlsChartSerie ᜀ(XmlReader A_0, XlsChart A_1, bool A_2, RelationsCollection A_3, List<XlsChartSerie> A_4, spr\u2306 A_5)
		{
			int a_ = 2;
			switch (0)
			{
			default:
			{
				int num = 25;
				XlsChartSerie xlsChartSerie;
				for (;;)
				{
					bool flag;
					switch (num)
					{
					case 0:
						if (A_0.NodeType != XmlNodeType.EndElement)
						{
							num = 7;
							continue;
						}
						goto IL_189;
					case 1:
						goto IL_3F6;
					case 2:
						num = 11;
						continue;
					case 3:
					{
						XlsChartSerie xlsChartSerie2;
						xlsChartSerie = xlsChartSerie2;
						num = 16;
						continue;
					}
					case 4:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("䬷弹主", a_)))
						{
							num = 6;
							continue;
						}
						string a_3;
						ExcelChartType a_2 = this.ᜀ(a_3, A_2);
						XlsChartSerie xlsChartSerie2 = this.ᜂ(A_0, A_1, a_2, A_3, A_5);
						A_4.Add(xlsChartSerie2);
						num = 19;
						continue;
					}
					case 5:
						goto IL_22F;
					case 6:
						num = 27;
						continue;
					case 7:
						num = 12;
						continue;
					case 8:
						goto IL_301;
					case 9:
					{
						string a_3;
						A_1.PivotChartType = this.ᜀ(a_3, A_2);
						if (true)
						{
						}
						num = 5;
						continue;
					}
					case 10:
						num = 15;
						continue;
					case 11:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("丷嬹主䜽̿ⵁ⡃⥅㩇㥉", a_)))
						{
							num = 26;
							continue;
						}
						spr\u1AA0.ᜃ(A_0);
						num = 30;
						continue;
					}
					case 12:
						if (!flag)
						{
							num = 34;
							continue;
						}
						num = 14;
						continue;
					case 13:
						goto IL_121;
					case 14:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 17;
							continue;
						}
						goto IL_121;
					case 15:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("尷䠹医丽ి⭁⩃⍅㭇", a_)))
						{
							num = 23;
							continue;
						}
						A_1.DropLinesStream = ShapeParser.ReadNodeAsStream(A_0, true);
						num = 33;
						continue;
					}
					case 16:
						goto IL_121;
					case 17:
						num = 20;
						continue;
					case 18:
						goto IL_121;
					case 19:
						if (xlsChartSerie == null)
						{
							num = 3;
							continue;
						}
						goto IL_121;
					case 20:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num = 22;
							continue;
						}
						goto IL_301;
					}
					case 21:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("強䠹医䬽〿⭁⩃ⅅ", a_)))
						{
							num = 2;
							continue;
						}
						string a_3 = spr\u1AA0.ᜄ(A_0);
						num = 32;
						continue;
					}
					case 22:
						num = 21;
						continue;
					case 23:
						num = 8;
						continue;
					case 24:
					{
						if (A_1 == null)
						{
							num = 1;
							continue;
						}
						flag = true;
						string a_3 = null;
						xlsChartSerie = null;
						num = 28;
						continue;
					}
					case 26:
						num = 4;
						continue;
					case 27:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("尷瘹帻刽㌿", a_)))
						{
							num = 10;
							continue;
						}
						A_0.Skip();
						num = 18;
						continue;
					}
					case 28:
						goto IL_121;
					case 29:
						goto IL_CA;
					case 30:
						goto IL_121;
					case 31:
						if (A_1.HasPivotTable)
						{
							num = 9;
							continue;
						}
						return xlsChartSerie;
					case 32:
						goto IL_121;
					case 33:
						goto IL_121;
					case 34:
						goto IL_189;
					}
					if (A_0 == null)
					{
						num = 29;
						continue;
					}
					num = 24;
					continue;
					IL_121:
					num = 0;
					continue;
					IL_189:
					num = 31;
					continue;
					IL_301:
					flag = false;
					num = 13;
				}
				IL_CA:
				throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹崻娽┿ぁ", a_));
				IL_22F:
				return xlsChartSerie;
				IL_3F6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					throw new ArgumentNullException(RecordTableEnumerator.b("嬷刹崻䰽㐿", a_));
				}
				return xlsChartSerie;
			}
			}
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x0010F04C File Offset: 0x0010E04C
		private void ᜄ(XmlReader A_0, XlsChart A_1, RelationsCollection A_2, Dictionary<int, int> A_3, spr\u2306 A_4)
		{
			int a_ = 5;
			int num = 5;
			for (;;)
			{
				List<XlsChartSerie> list;
				switch (num)
				{
				case 0:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 6;
						continue;
					}
					num = 1;
					continue;
				case 1:
					if (A_0.LocalName == RecordTableEnumerator.b("娺䔼瘾╀", a_))
					{
						num = 12;
						continue;
					}
					A_0.Skip();
					num = 10;
					continue;
				case 2:
					if (A_1 == null)
					{
						if (true)
						{
						}
						num = 11;
						continue;
					}
					num = 7;
					continue;
				case 3:
					goto IL_186;
				case 4:
					goto IL_130;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_15D;
					default:
						goto IL_1C6;
					}
					break;
				case 7:
					goto IL_15D;
				case 8:
					goto IL_58;
				case 9:
					goto IL_130;
				case 10:
					goto IL_130;
				case 11:
					goto IL_CE;
				case 12:
					this.ᜀ(A_0, list, A_3);
					num = 9;
					continue;
				}
				if (A_0 == null)
				{
					num = 8;
					continue;
				}
				num = 2;
				continue;
				IL_15D:
				if (A_0.LocalName != RecordTableEnumerator.b("场吼儾⑀灂ńцⅈ⩊㽌㭎", a_))
				{
					num = 3;
					continue;
				}
				A_0.Read();
				list = new List<XlsChartSerie>();
				this.ᜀ(A_0, A_1, true, A_2, list, A_4);
				num = 4;
				continue;
				IL_130:
				num = 0;
			}
			IL_58:
			throw new ArgumentNullException(RecordTableEnumerator.b("䤺堼帾╀♂㝄", a_));
			IL_CE:
			throw new ArgumentNullException(RecordTableEnumerator.b("堺唼帾㍀㝂", a_));
			IL_186:
			throw new XmlException(RecordTableEnumerator.b("渺匼娾㥀㍂⁄⑆㵈⹊⥌潎⥐㹒㥔睖ⵘ㩚㩜煞", a_));
			IL_1C6:
			if (false)
			{
			}
			A_0.Read();
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x0010F22C File Offset: 0x0010E22C
		private void ᜃ(XmlReader A_0, XlsChart A_1, RelationsCollection A_2, Dictionary<int, int> A_3, spr\u2306 A_4)
		{
			int a_ = 18;
			switch (0)
			{
			default:
			{
				int num = 18;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 9;
						continue;
					case 1:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("⁇⍉K⅍❏ṑ㵓㡕㵗⥙", a_)))
						{
							num = 12;
							continue;
						}
						XlsChartSerie xlsChartSerie;
						this.ᜃ(A_0, xlsChartSerie);
						num = 7;
						continue;
					}
					case 2:
						num = 19;
						continue;
					case 3:
						goto IL_247;
					case 4:
						goto IL_26C;
					case 5:
						num = 11;
						continue;
					case 6:
						goto IL_247;
					case 7:
						goto IL_247;
					case 8:
					{
						bool flag;
						if (flag)
						{
							num = 25;
							continue;
						}
						goto IL_247;
					}
					case 9:
					{
						XlsChartSerie xlsChartSerie;
						if (xlsChartSerie != null)
						{
							num = 2;
							continue;
						}
						goto IL_1B7;
					}
					case 10:
						goto IL_247;
					case 11:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("╇⭉㹋╍㕏⁑", a_)))
						{
							num = 28;
							continue;
						}
						bool flag = spr\u1AA0.ᜃ(A_0);
						num = 8;
						continue;
					}
					case 12:
						num = 29;
						continue;
					case 13:
						num = 22;
						continue;
					case 14:
						num = 17;
						continue;
					case 15:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 0;
							continue;
						}
						goto IL_1B7;
					case 16:
						goto IL_304;
					case 17:
						goto IL_233;
					case 19:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num = 13;
							continue;
						}
						goto IL_233;
					}
					case 20:
						goto IL_247;
					case 21:
						goto IL_B6;
					case 22:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("㵇㩉ࡋ⅍❏㱑ᙓ㝕⩗⥙", a_)))
						{
							num = 5;
							continue;
						}
						XlsChartSerie xlsChartSerie;
						this.ᜁ(A_0, xlsChartSerie, A_2);
						num = 6;
						continue;
					}
					case 23:
						if (A_0.NodeType == XmlNodeType.EndElement)
						{
							num = 4;
							continue;
						}
						num = 15;
						continue;
					case 24:
						goto IL_247;
					case 25:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_102;
						default:
						{
							if (false)
							{
							}
							XlsChartSerie xlsChartSerie;
							XlsChartSerieDataFormat xlsChartSerieDataFormat = (XlsChartSerieDataFormat)xlsChartSerie.Format;
							xlsChartSerieDataFormat.MarkerFormat;
							num = 20;
							continue;
						}
						}
						break;
					case 26:
						goto IL_247;
					case 27:
					{
						if (A_1 == null)
						{
							num = 16;
							continue;
						}
						A_0.Read();
						List<XlsChartSerie> list = new List<XlsChartSerie>();
						XlsChartSerie xlsChartSerie = this.ᜀ(A_0, A_1, false, A_2, list, A_4);
						bool flag = false;
						num = 24;
						continue;
					}
					case 28:
						num = 1;
						continue;
					case 29:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("⥇㉉Ջ⩍", a_)))
						{
							goto IL_102;
						}
						List<XlsChartSerie> list;
						this.ᜀ(A_0, list, A_3);
						num = 3;
						continue;
					}
					}
					if (A_0 == null)
					{
						num = 21;
						continue;
					}
					num = 27;
					continue;
					IL_102:
					num = 14;
					continue;
					IL_1B7:
					if (true)
					{
					}
					A_0.Skip();
					num = 10;
					continue;
					IL_233:
					A_0.Skip();
					num = 26;
					continue;
					IL_247:
					num = 23;
				}
				IL_B6:
				throw new ArgumentNullException(RecordTableEnumerator.b("㩇⽉ⵋ⩍㕏⁑", a_));
				IL_26C:
				A_0.Read();
				return;
				IL_304:
				throw new ArgumentNullException(RecordTableEnumerator.b("⭇≉ⵋ㱍⑏", a_));
			}
			}
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x0010F5F0 File Offset: 0x0010E5F0
		private void ᜅ(XmlReader A_0, XlsChart A_1, RelationsCollection A_2, Dictionary<int, int> A_3)
		{
			int a_ = 5;
			switch (0)
			{
			default:
			{
				int num = 6;
				for (;;)
				{
					List<XlsChartSerie> list;
					switch (num)
					{
					case 0:
						goto IL_327;
					case 1:
						num = 3;
						continue;
					case 2:
					{
						string localName;
						int num2;
						if (spr\u22D2.ᝀ.TryGetValue(localName, out num2))
						{
							num = 22;
							continue;
						}
						goto IL_1B8;
					}
					case 3:
						if (spr\u22D2.ᝀ == null)
						{
							num = 9;
							continue;
						}
						goto IL_2F5;
					case 4:
						goto IL_2F5;
					case 5:
						goto IL_34C;
					case 7:
						num = 23;
						continue;
					case 8:
						goto IL_327;
					case 9:
						spr\u22D2.ᝀ = new Dictionary<string, int>(6)
						{
							{
								RecordTableEnumerator.b("䴺尼䴾㡀B⩄⭆♈㥊㹌", a_),
								0
							},
							{
								RecordTableEnumerator.b("䠺堼䴾", a_),
								1
							},
							{
								RecordTableEnumerator.b("夺䠼崾⍀⽂⁄ᑆ⩈⩊⅌⩎", a_),
								2
							},
							{
								RecordTableEnumerator.b("䠺唼倾㙀ൂ⁄⁆ୈ㹊⽌ⵎ㵐㙒♔", a_),
								3
							},
							{
								RecordTableEnumerator.b("䠺吼䔾⑀ᅂ⁄㝆㭈⹊㹌⩎㽐❒♔", a_),
								4
							},
							{
								RecordTableEnumerator.b("娺䔼瘾╀", a_),
								5
							}
						};
						num = 4;
						continue;
					case 10:
						if (A_0.NodeType == XmlNodeType.EndElement)
						{
							num = 5;
							continue;
						}
						num = 29;
						continue;
					case 11:
						goto IL_327;
					case 12:
						goto IL_327;
					case 13:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num = 1;
							continue;
						}
						goto IL_1B8;
					}
					case 14:
						goto IL_327;
					case 15:
						goto IL_327;
					case 16:
						num = 13;
						continue;
					case 17:
						goto IL_327;
					case 18:
						goto IL_BA;
					case 19:
						goto IL_2B0;
					case 20:
						goto IL_327;
					case 21:
						goto IL_327;
					case 22:
						num = 30;
						continue;
					case 23:
						goto IL_1B8;
					case 24:
						goto IL_3D1;
					case 25:
					{
						if (A_0.LocalName != RecordTableEnumerator.b("夺䠼崾⍀⽂⁄цⅈ⩊㽌㭎", a_))
						{
							num = 24;
							continue;
						}
						A_0.Read();
						XlsChartSerie xlsChartSerie = null;
						list = new List<XlsChartSerie>();
						num = 11;
						continue;
					}
					case 26:
						num = 19;
						continue;
					case 27:
						if (A_1 == null)
						{
							num = 26;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_12A;
						default:
							if (false)
							{
							}
							num = 25;
							continue;
						}
						break;
					case 28:
					{
						IChartFormat options;
						string a;
						options.SizeRepresents = ((a == RecordTableEnumerator.b("娺似娾⁀", a_)) ? BubbleSizeType.Area : BubbleSizeType.Width);
						num = 0;
						continue;
					}
					case 29:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 16;
							continue;
						}
						A_0.Skip();
						num = 17;
						continue;
					case 30:
					{
						int num2;
						switch (num2)
						{
						case 0:
							spr\u1AA0.ᜃ(A_0);
							num = 12;
							continue;
						case 1:
						{
							XlsChartSerie xlsChartSerie = this.ᜀ(A_0, A_1, A_2);
							list.Add(xlsChartSerie);
							num = 14;
							continue;
						}
						case 2:
						{
							int bubbleScale = spr\u1AA0.ᜂ(A_0);
							XlsChartSerie xlsChartSerie;
							IChartFormat options = xlsChartSerie.Format.Options;
							options.BubbleScale = bubbleScale;
							num = 15;
							continue;
						}
						case 3:
						{
							bool showNegativeBubbles = spr\u1AA0.ᜃ(A_0);
							XlsChartSerie xlsChartSerie;
							IChartFormat options = xlsChartSerie.Format.Options;
							options.ShowNegativeBubbles = showNegativeBubbles;
							num = 8;
							continue;
						}
						case 4:
						{
							string a = spr\u1AA0.ᜄ(A_0);
							XlsChartSerie xlsChartSerie;
							IChartFormat options = xlsChartSerie.Format.Options;
							num = 28;
							continue;
						}
						case 5:
							goto IL_12A;
						default:
							num = 7;
							continue;
						}
						break;
					}
					}
					if (A_0 == null)
					{
						num = 18;
						continue;
					}
					num = 27;
					continue;
					IL_12A:
					if (true)
					{
					}
					this.ᜀ(A_0, list, A_3);
					num = 20;
					continue;
					IL_1B8:
					A_0.Skip();
					num = 21;
					continue;
					IL_2F5:
					num = 2;
					continue;
					IL_327:
					num = 10;
				}
				IL_BA:
				throw new ArgumentNullException(RecordTableEnumerator.b("䤺堼帾╀♂㝄", a_));
				IL_2B0:
				throw new ArgumentNullException(RecordTableEnumerator.b("堺唼帾㍀㝂", a_));
				IL_34C:
				A_0.Read();
				return;
				IL_3D1:
				throw new XmlException(RecordTableEnumerator.b("渺匼娾㥀㍂⁄⑆㵈⹊⥌潎⥐㹒㥔睖ⵘ㩚㩜煞", a_));
			}
			}
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x0010FAAC File Offset: 0x0010EAAC
		private void ᜄ(XmlReader A_0, XlsChart A_1, RelationsCollection A_2, Dictionary<int, int> A_3)
		{
			int a_ = 13;
			int num = 0;
			for (;;)
			{
				bool a_2;
				switch (num)
				{
				case 1:
					goto IL_12E;
				case 2:
					if (A_0.LocalName == RecordTableEnumerator.b("≂㵄ๆⵈ", a_))
					{
						num = 12;
						continue;
					}
					A_0.Skip();
					num = 1;
					continue;
				case 3:
					goto IL_107;
				case 4:
					goto IL_6B;
				case 5:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 13;
						continue;
					}
					num = 2;
					continue;
				case 6:
					if (A_0.LocalName == RecordTableEnumerator.b("あい㕆⽈⩊⹌⩎ቐ㭒㑔╖ⵘ", a_))
					{
						num = 15;
						continue;
					}
					num = 11;
					continue;
				case 7:
					goto IL_109;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_9E;
					default:
						if (false)
						{
						}
						a_2 = true;
						num = 10;
						continue;
					}
					break;
				case 9:
					goto IL_12E;
				case 10:
					goto IL_109;
				case 11:
					if (A_0.LocalName == RecordTableEnumerator.b("あい㕆⽈⩊⹌⩎扐ᝒᙔ㽖㡘⥚⥜", a_))
					{
						num = 8;
						continue;
					}
					goto IL_1F6;
				case 12:
					goto IL_9E;
				case 13:
					goto IL_151;
				case 14:
					goto IL_12E;
				case 15:
					a_2 = false;
					num = 7;
					continue;
				case 16:
					if (true)
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
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				num = 16;
				continue;
				IL_9E:
				List<XlsChartSerie> list;
				this.ᜀ(A_0, list, A_3);
				num = 14;
				continue;
				IL_109:
				A_0.Read();
				list = new List<XlsChartSerie>();
				this.ᜀ(A_0, A_1, a_2, A_2, list);
				num = 9;
				continue;
				IL_12E:
				num = 5;
			}
			IL_6B:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄♆ⵈ⹊㽌", a_));
			IL_107:
			throw new ArgumentNullException(RecordTableEnumerator.b("⁂ⵄ♆㭈㽊", a_));
			IL_151:
			A_0.Skip();
			return;
			IL_1F6:
			throw new XmlException(RecordTableEnumerator.b("ᙂ⭄≆ㅈ㭊⡌ⱎ═㙒ㅔ睖⅘㙚ㅜ罞ᕠɢɤ䥦", a_));
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x0010FCF0 File Offset: 0x0010ECF0
		private void ᜀ(XmlReader A_0, XlsChart A_1, bool A_2, RelationsCollection A_3, List<XlsChartSerie> A_4)
		{
			int a_ = 11;
			switch (0)
			{
			default:
			{
				int num = 13;
				for (;;)
				{
					bool flag;
					bool a_2;
					switch (num)
					{
					case 0:
						goto IL_15E;
					case 1:
						goto IL_28A;
					case 2:
						if (A_1 == null)
						{
							num = 1;
							continue;
						}
						flag = true;
						a_2 = false;
						num = 14;
						continue;
					case 3:
						num = 20;
						continue;
					case 4:
						A_1.PivotChartType = this.ᜀ(a_2, A_2);
						num = 7;
						continue;
					case 5:
						if (A_1.HasPivotTable)
						{
							num = 4;
							continue;
						}
						return;
					case 6:
						goto IL_10D;
					case 7:
						goto IL_1F7;
					case 8:
						num = 23;
						continue;
					case 9:
						num = 25;
						continue;
					case 10:
						goto IL_B5;
					case 11:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("㉀♂㝄", a_)))
						{
							num = 3;
							continue;
						}
						ExcelChartType a_3 = this.ᜀ(a_2, A_2);
						XlsChartSerie item = this.ᜁ(A_0, A_1, a_3, A_3);
						A_4.Add(item);
						num = 15;
						continue;
					}
					case 12:
						num = 22;
						continue;
					case 14:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_CA;
						default:
							if (false)
							{
							}
							goto IL_10D;
						}
						break;
					case 15:
						goto IL_10D;
					case 16:
						goto IL_10D;
					case 17:
						goto IL_BA;
					case 18:
						goto IL_10D;
					case 19:
						goto IL_10D;
					case 20:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("⍀≂⭄⍆཈♊㥌㱎", a_)))
						{
							num = 26;
							continue;
						}
						this.ᜁ(A_0, A_1);
						num = 16;
						continue;
					}
					case 21:
						if (A_0.NodeType != XmlNodeType.EndElement)
						{
							num = 9;
							continue;
						}
						goto IL_15E;
					case 22:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num = 8;
							continue;
						}
						goto IL_BA;
					}
					case 23:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("㙀⩂㝄≆⽈㥊ⱌ≎㑐", a_)))
						{
							num = 24;
							continue;
						}
						goto IL_CA;
					}
					case 24:
						num = 11;
						continue;
					case 25:
						if (!flag)
						{
							num = 0;
							continue;
						}
						num = 27;
						continue;
					case 26:
						num = 17;
						continue;
					case 27:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 12;
							continue;
						}
						A_0.Skip();
						num = 19;
						continue;
					}
					if (A_0 == null)
					{
						num = 10;
						continue;
					}
					num = 2;
					continue;
					IL_BA:
					flag = false;
					num = 18;
					continue;
					IL_CA:
					a_2 = spr\u1AA0.ᜃ(A_0);
					num = 6;
					continue;
					IL_10D:
					num = 21;
					continue;
					IL_15E:
					num = 5;
				}
				IL_B5:
				throw new ArgumentNullException(RecordTableEnumerator.b("㍀♂⑄⍆ⱈ㥊", a_));
				IL_1F7:
				return;
				IL_28A:
				if (true)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("≀⭂⑄㕆㵈", a_));
			}
			}
		}

		// Token: 0x06001F34 RID: 7988 RVA: 0x00110044 File Offset: 0x0010F044
		private void ᜁ(XmlReader A_0, XlsChart A_1)
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
			A_1.PreservedBandFormats = ShapeParser.ReadNodeAsStream(A_0);
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x0011008C File Offset: 0x0010F08C
		private ExcelChartType ᜀ(bool A_0, bool A_1)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_8A:
				if (true)
				{
				}
				num = 2;
				break;
			default:
				if (false)
				{
				}
				num = 7;
				break;
			}
			ExcelChartType result;
			for (;;)
			{
				ExcelChartType excelChartType;
				ExcelChartType excelChartType2;
				switch (num)
				{
				case 0:
					excelChartType = ExcelChartType.Surface3DNoColor;
					goto IL_D6;
				case 1:
					goto IL_AA;
				case 2:
					excelChartType = ExcelChartType.SurfaceContourNoColor;
					goto IL_D6;
				case 3:
					num = 11;
					continue;
				case 4:
					excelChartType2 = ExcelChartType.Surface3D;
					goto IL_9E;
				case 5:
					num = 10;
					continue;
				case 6:
					return result;
				case 8:
					goto IL_C5;
				case 9:
					if (!A_1)
					{
						num = 5;
						continue;
					}
					num = 4;
					continue;
				case 10:
					excelChartType2 = ExcelChartType.SurfaceContour;
					goto IL_9E;
				case 11:
					if (!A_1)
					{
						num = 8;
						continue;
					}
					num = 0;
					continue;
				}
				if (A_0)
				{
					num = 3;
					continue;
				}
				num = 9;
				continue;
				IL_9E:
				result = excelChartType2;
				num = 1;
				continue;
				IL_D6:
				result = excelChartType;
				num = 6;
			}
			IL_AA:
			return result;
			IL_C5:
			goto IL_8A;
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x00110198 File Offset: 0x0010F198
		private void ᜂ(XmlReader A_0, XlsChart A_1, RelationsCollection A_2, Dictionary<int, int> A_3, spr\u2306 A_4)
		{
			int a_ = 10;
			switch (0)
			{
			default:
			{
				int num = 32;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 6;
						continue;
					case 1:
					{
						ExcelChartType excelChartType;
						A_1.PivotChartType = excelChartType;
						num = 27;
						continue;
					}
					case 2:
						if (true)
						{
						}
						goto IL_33C;
					case 3:
						num = 8;
						continue;
					case 4:
						goto IL_223;
					case 5:
						num = 12;
						continue;
					case 6:
						if (A_1.HasPivotTable)
						{
							num = 1;
							continue;
						}
						goto IL_445;
					case 7:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("㘿⍁㙃㽅େ╉⁋⅍≏⅑", a_)))
						{
							num = 3;
							continue;
						}
						bool flag = spr\u1AA0.ᜃ(A_0);
						num = 10;
						continue;
					}
					case 8:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("㌿❁㙃", a_)))
						{
							num = 23;
							continue;
						}
						ExcelChartType excelChartType;
						XlsChartSerie xlsChartSerie = this.ᜀ(A_0, A_1, excelChartType, A_2, A_4);
						List<XlsChartSerie> list;
						list.Add(xlsChartSerie);
						num = 15;
						continue;
					}
					case 9:
					{
						XlsChartSerie xlsChartSerie;
						xlsChartSerie.Format.Options.IsVaryColor = true;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3CE;
						default:
							if (false)
							{
							}
							num = 16;
							continue;
						}
						break;
					}
					case 10:
						goto IL_223;
					case 11:
						num = 7;
						continue;
					case 12:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num = 31;
							continue;
						}
						goto IL_33C;
					}
					case 13:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 5;
							continue;
						}
						A_0.Skip();
						num = 17;
						continue;
					case 14:
						num = 2;
						continue;
					case 15:
					{
						bool flag;
						if (flag)
						{
							num = 9;
							continue;
						}
						goto IL_223;
					}
					case 16:
						goto IL_223;
					case 17:
						goto IL_223;
					case 18:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("ℿ㩁ൃ≅", a_)))
						{
							num = 14;
							continue;
						}
						List<XlsChartSerie> list;
						this.ᜀ(A_0, list, A_3);
						num = 28;
						continue;
					}
					case 19:
						goto IL_223;
					case 20:
						goto IL_C2;
					case 21:
					{
						if (A_0.LocalName != RecordTableEnumerator.b("㈿⍁⁃❅㩇ॉ⑋⽍≏♑", a_))
						{
							num = 24;
							continue;
						}
						A_0.Read();
						ExcelChartType excelChartType = ExcelChartType.Radar;
						bool flag = false;
						List<XlsChartSerie> list = new List<XlsChartSerie>();
						num = 19;
						continue;
					}
					case 22:
						goto IL_223;
					case 23:
						goto IL_3CE;
					case 24:
						goto IL_2E6;
					case 25:
						if (A_0.NodeType == XmlNodeType.EndElement)
						{
							num = 0;
							continue;
						}
						num = 13;
						continue;
					case 26:
						goto IL_424;
					case 27:
						goto IL_260;
					case 28:
						goto IL_223;
					case 29:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("㈿⍁⁃❅㩇᥉㡋㝍㱏㝑", a_)))
						{
							num = 11;
							continue;
						}
						string value = spr\u1AA0.ᜄ(A_0);
						XLSXRadarStyle xlsxradarStyle = (XLSXRadarStyle)Enum.Parse(typeof(XLSXRadarStyle), value, true);
						ExcelChartType excelChartType = (ExcelChartType)xlsxradarStyle;
						num = 22;
						continue;
					}
					case 30:
						if (A_1 == null)
						{
							num = 26;
							continue;
						}
						num = 21;
						continue;
					case 31:
						num = 29;
						continue;
					}
					if (A_0 == null)
					{
						num = 20;
						continue;
					}
					num = 30;
					continue;
					IL_223:
					num = 25;
					continue;
					IL_33C:
					A_0.Skip();
					num = 4;
					continue;
					IL_3CE:
					num = 18;
				}
				IL_C2:
				throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁╃≅ⵇ㡉", a_));
				IL_260:
				goto IL_445;
				IL_2E6:
				throw new XmlException(RecordTableEnumerator.b("ᔿⱁ⅃㹅㡇⽉⽋㩍㕏㙑瑓⹕㕗㙙籛⩝şա䩣", a_));
				IL_424:
				throw new ArgumentNullException(RecordTableEnumerator.b("⌿⩁╃㑅㱇", a_));
				IL_445:
				A_0.Read();
				return;
			}
			}
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x001105F4 File Offset: 0x0010F5F4
		private void ᜁ(XmlReader A_0, XlsChart A_1, RelationsCollection A_2, Dictionary<int, int> A_3, spr\u2306 A_4)
		{
			int a_ = 3;
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
						if (A_0.LocalName != RecordTableEnumerator.b("䨸堺尼䬾㕀♂㝄цⅈ⩊㽌㭎", a_))
						{
							num = 13;
							continue;
						}
						A_0.Read();
						ExcelChartType a_2 = ExcelChartType.ScatterMarkers;
						bool flag = false;
						XlsChartSerie xlsChartSerie = null;
						List<XlsChartSerie> list = new List<XlsChartSerie>();
						num = 32;
						continue;
					}
					case 1:
						num = 3;
						continue;
					case 2:
						goto IL_238;
					case 3:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num = 17;
							continue;
						}
						goto IL_224;
					}
					case 4:
						goto IL_238;
					case 5:
						num = 26;
						continue;
					case 6:
						goto IL_25D;
					case 7:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 1;
							continue;
						}
						A_0.Skip();
						num = 29;
						continue;
					case 8:
						num = 21;
						continue;
					case 9:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("䰸䬺礼倾㙀ⵂ݄♆㭈㡊", a_)))
						{
							num = 5;
							continue;
						}
						XlsChartSerie xlsChartSerie;
						this.ᜁ(A_0, xlsChartSerie, A_2);
						num = 11;
						continue;
					}
					case 10:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("伸娺似䘾ɀⱂ⥄⡆㭈㡊", a_)))
						{
							num = 8;
							continue;
						}
						bool flag = spr\u1AA0.ᜃ(A_0);
						num = 30;
						continue;
					}
					case 11:
						goto IL_238;
					case 12:
						goto IL_238;
					case 13:
						goto IL_2E3;
					case 15:
						goto IL_238;
					case 16:
						if (A_0.NodeType == XmlNodeType.EndElement)
						{
							num = 6;
							continue;
						}
						num = 7;
						continue;
					case 17:
						num = 22;
						continue;
					case 18:
						goto IL_422;
					case 19:
						if (A_1 == null)
						{
							num = 18;
							continue;
						}
						num = 0;
						continue;
					case 20:
						num = 31;
						continue;
					case 21:
					{
						string localName;
						if (localName == RecordTableEnumerator.b("䨸帺似", a_))
						{
							ExcelChartType a_2;
							XlsChartSerie xlsChartSerie = this.ᜁ(A_0, A_1, a_2, A_2, A_4);
							List<XlsChartSerie> list;
							list.Add(xlsChartSerie);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								num = 28;
								continue;
							}
						}
						num = 24;
						continue;
					}
					case 22:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("䨸堺尼䬾㕀♂㝄ᑆ㵈㉊⅌⩎", a_)))
						{
							num = 27;
							continue;
						}
						string value = spr\u1AA0.ᜄ(A_0);
						XLSXScatterStyle xlsxscatterStyle = (XLSXScatterStyle)Enum.Parse(typeof(XLSXScatterStyle), value, false);
						ExcelChartType a_2 = (ExcelChartType)xlsxscatterStyle;
						num = 12;
						continue;
					}
					case 23:
						goto IL_C2;
					case 24:
						num = 9;
						continue;
					case 25:
					{
						XlsChartSerie xlsChartSerie;
						xlsChartSerie.Format.Options.IsVaryColor = true;
						num = 15;
						continue;
					}
					case 26:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("堸䌺琼嬾", a_)))
						{
							num = 20;
							continue;
						}
						List<XlsChartSerie> list;
						this.ᜀ(A_0, list, A_3);
						num = 4;
						continue;
					}
					case 27:
						num = 10;
						continue;
					case 28:
					{
						bool flag;
						if (flag)
						{
							num = 25;
							continue;
						}
						goto IL_238;
					}
					case 29:
						goto IL_238;
					case 30:
						goto IL_238;
					case 31:
						goto IL_224;
					case 32:
						goto IL_238;
					}
					if (A_0 == null)
					{
						num = 23;
						continue;
					}
					num = 19;
					continue;
					IL_224:
					A_0.Skip();
					num = 2;
					continue;
					IL_238:
					num = 16;
				}
				IL_C2:
				throw new ArgumentNullException(RecordTableEnumerator.b("䬸帺尼嬾⑀ㅂ", a_));
				IL_25D:
				if (true)
				{
				}
				A_0.Read();
				return;
				IL_2E3:
				throw new XmlException(RecordTableEnumerator.b("永唺堼䜾ㅀ♂♄㍆ⱈ⽊浌㝎㱐㽒畔⍖㡘㱚獜", a_));
				IL_422:
				throw new ArgumentNullException(RecordTableEnumerator.b("娸区尼䴾㕀", a_));
			}
			}
		}

		// Token: 0x06001F38 RID: 7992 RVA: 0x00110A5C File Offset: 0x0010FA5C
		private void ᜃ(XmlReader A_0, XlsChart A_1, RelationsCollection A_2, Dictionary<int, int> A_3)
		{
			int a_ = 2;
			for (;;)
			{
				IL_09:
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
						{
							string localName;
							if (!(localName == RecordTableEnumerator.b("帷匹主䴽㐿ᅁ⡃⽅⭇⽉ോ⁍㝏", a_)))
							{
								num = 16;
								continue;
							}
							XlsChartSerie xlsChartSerie;
							IChartFormat options = xlsChartSerie.Format.Options;
							options.FirstSliceAngle = spr\u1AA0.ᜂ(A_0);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_09;
							default:
								if (false)
								{
								}
								num = 5;
								continue;
							}
							break;
						}
						case 1:
						{
							if (A_0.LocalName != RecordTableEnumerator.b("䠷匹夻紽⠿⍁㙃㉅", a_))
							{
								num = 12;
								continue;
							}
							A_0.Read();
							List<XlsChartSerie> list = new List<XlsChartSerie>();
							XlsChartSerie xlsChartSerie = this.ᜀ(A_0, A_1, ExcelChartType.Pie, A_2, list);
							num = 18;
							continue;
						}
						case 3:
						{
							string localName;
							if (!(localName == RecordTableEnumerator.b("夷䈹画娽", a_)))
							{
								num = 10;
								continue;
							}
							List<XlsChartSerie> list;
							this.ᜀ(A_0, list, A_3);
							num = 7;
							continue;
						}
						case 4:
							num = 0;
							continue;
						case 5:
							goto IL_160;
						case 6:
							goto IL_1EE;
						case 7:
							goto IL_160;
						case 8:
							goto IL_8B;
						case 9:
							goto IL_11C;
						case 10:
							num = 6;
							continue;
						case 11:
							goto IL_160;
						case 12:
							goto IL_1EC;
						case 13:
							if (A_1 == null)
							{
								num = 9;
								continue;
							}
							num = 1;
							continue;
						case 14:
							if (A_0.NodeType == XmlNodeType.EndElement)
							{
								num = 15;
								continue;
							}
							num = 17;
							continue;
						case 15:
							goto IL_182;
						case 16:
							num = 3;
							continue;
						case 17:
						{
							if (true)
							{
							}
							string localName;
							if ((localName = A_0.LocalName) != null)
							{
								num = 4;
								continue;
							}
							goto IL_1EE;
						}
						case 18:
							goto IL_160;
						}
						if (A_0 == null)
						{
							num = 8;
							continue;
						}
						num = 13;
						continue;
						IL_160:
						num = 14;
						continue;
						IL_1EE:
						A_0.Skip();
						num = 11;
					}
					break;
				}
				}
			}
			IL_8B:
			throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹崻娽┿ぁ", a_));
			IL_11C:
			throw new ArgumentNullException(RecordTableEnumerator.b("嬷刹崻䰽㐿", a_));
			IL_182:
			A_0.Read();
			return;
			IL_1EC:
			throw new XmlException(RecordTableEnumerator.b("洷吹夻䘽〿❁❃㉅ⵇ⹉汋㙍㵏㹑瑓≕㥗㵙牛", a_));
		}

		// Token: 0x06001F39 RID: 7993 RVA: 0x00110D00 File Offset: 0x0010FD00
		private void ᜂ(XmlReader A_0, XlsChart A_1, RelationsCollection A_2, Dictionary<int, int> A_3)
		{
			int a_ = 18;
			int num = 11;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_125;
				case 1:
					goto IL_125;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_50;
					default:
						goto IL_15B;
					}
					break;
				case 3:
					if (A_0.LocalName == RecordTableEnumerator.b("⥇㉉Ջ⩍", a_))
					{
						num = 10;
						continue;
					}
					A_0.Skip();
					if (true)
					{
					}
					num = 1;
					continue;
				case 4:
				{
					if (A_0.LocalName != RecordTableEnumerator.b("㡇⍉⥋絍ᑏᅑ㱓㝕⩗⹙", a_))
					{
						num = 9;
						continue;
					}
					A_0.Read();
					List<XlsChartSerie> list = new List<XlsChartSerie>();
					this.ᜀ(A_0, A_1, ExcelChartType.Pie3D, A_2, list);
					num = 12;
					continue;
				}
				case 5:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 2;
						continue;
					}
					num = 3;
					continue;
				case 6:
					if (A_1 == null)
					{
						num = 8;
						continue;
					}
					num = 4;
					continue;
				case 7:
					goto IL_58;
				case 8:
					goto IL_BB;
				case 9:
					goto IL_197;
				case 10:
				{
					List<XlsChartSerie> list;
					this.ᜀ(A_0, list, A_3);
					num = 0;
					continue;
				}
				case 12:
					goto IL_125;
				}
				goto IL_4D;
				IL_50:
				num = 7;
				continue;
				IL_4D:
				if (A_0 == null)
				{
					goto IL_50;
				}
				num = 6;
				continue;
				IL_125:
				num = 5;
			}
			IL_58:
			throw new ArgumentNullException(RecordTableEnumerator.b("㩇⽉ⵋ⩍㕏⁑", a_));
			IL_BB:
			throw new ArgumentNullException(RecordTableEnumerator.b("⭇≉ⵋ㱍⑏", a_));
			IL_15B:
			if (false)
			{
			}
			A_0.Read();
			return;
			IL_197:
			throw new XmlException(RecordTableEnumerator.b("ᵇ⑉⥋㙍⁏㝑㝓≕㵗㹙籛♝ൟ๡䑣ብ१൩䉫", a_));
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x00110EE0 File Offset: 0x0010FEE0
		private void ᜁ(XmlReader A_0, XlsChart A_1, RelationsCollection A_2, Dictionary<int, int> A_3)
		{
			int a_ = 2;
			switch (0)
			{
			default:
			{
				int num = 21;
				for (;;)
				{
					ExcelChartType excelChartType;
					ExcelChartType excelChartType2;
					switch (num)
					{
					case 0:
						if (A_1.HasPivotTable)
						{
							num = 5;
							continue;
						}
						goto IL_642;
					case 1:
					{
						int? num2;
						if (num2 != null)
						{
							num = 15;
							continue;
						}
						goto IL_5CA;
					}
					case 2:
						goto IL_22C;
					case 3:
						goto IL_5FF;
					case 4:
						goto IL_22C;
					case 5:
						A_1.PivotChartType = excelChartType;
						num = 3;
						continue;
					case 6:
						num = 31;
						continue;
					case 7:
						num = 39;
						continue;
					case 8:
					{
						string a;
						if (!(a == RecordTableEnumerator.b("䠷匹夻", a_)))
						{
							num = 25;
							continue;
						}
						num = 10;
						continue;
					}
					case 9:
					{
						if (true)
						{
						}
						XlsChartSerie xlsChartSerie;
						if (xlsChartSerie != null)
						{
							num = 36;
							continue;
						}
						goto IL_3A3;
					}
					case 10:
						excelChartType2 = ExcelChartType.PieOfPie;
						goto IL_53F;
					case 11:
						num = 1;
						continue;
					case 12:
						excelChartType2 = ExcelChartType.PieBar;
						goto IL_53F;
					case 13:
						if (A_1 == null)
						{
							num = 32;
							continue;
						}
						num = 28;
						continue;
					case 14:
						goto IL_22C;
					case 15:
					{
						int? num2;
						XlsChartSerie xlsChartSerie;
						xlsChartSerie.Format.Options.GapWidth = num2.Value;
						num = 23;
						continue;
					}
					case 16:
						num = 9;
						continue;
					case 17:
						goto IL_40C;
					case 18:
						goto IL_22C;
					case 19:
						goto IL_22C;
					case 20:
						goto IL_4F4;
					case 22:
						num = 17;
						continue;
					case 23:
						goto IL_5CA;
					case 24:
						if (A_0.NodeType == XmlNodeType.EndElement)
						{
							num = 11;
							continue;
						}
						num = 30;
						continue;
					case 25:
						num = 12;
						continue;
					case 26:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_392;
						default:
							if (false)
							{
							}
							goto IL_371;
						}
						break;
					case 27:
						goto IL_22C;
					case 28:
					{
						if (A_0.LocalName != RecordTableEnumerator.b("圷尹氻圽┿Łⱃ❅㩇㹉", a_))
						{
							num = 20;
							continue;
						}
						A_0.Read();
						excelChartType = ExcelChartType.PieOfPie;
						XlsChartSerie xlsChartSerie = null;
						List<XlsChartSerie> list = new List<XlsChartSerie>();
						int? num2 = null;
						num = 34;
						continue;
					}
					case 29:
						goto IL_22C;
					case 30:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 16;
							continue;
						}
						goto IL_3A3;
					case 31:
						if (spr\u22D2.ᝁ == null)
						{
							num = 37;
							continue;
						}
						goto IL_371;
					case 32:
						goto IL_61F;
					case 33:
						goto IL_22C;
					case 34:
						goto IL_22C;
					case 35:
						goto IL_22C;
					case 36:
						num = 41;
						continue;
					case 37:
						spr\u22D2.ᝁ = new Dictionary<string, int>(10)
						{
							{
								RecordTableEnumerator.b("圷尹氻圽┿ᙁ㵃㙅ⵇ", a_),
								0
							},
							{
								RecordTableEnumerator.b("丷嬹主䜽̿ⵁ⡃⥅㩇㥉", a_),
								1
							},
							{
								RecordTableEnumerator.b("䬷弹主", a_),
								2
							},
							{
								RecordTableEnumerator.b("尷瘹帻刽㌿", a_),
								3
							},
							{
								RecordTableEnumerator.b("強嬹䰻椽⤿♁ぃ⹅", a_),
								4
							},
							{
								RecordTableEnumerator.b("䬷䨹倻圽㐿ᙁ㵃㙅ⵇ", a_),
								5
							},
							{
								RecordTableEnumerator.b("䬷䨹倻圽㐿ቁ⭃㕅", a_),
								6
							},
							{
								RecordTableEnumerator.b("䬷弹弻儽⸿♁ᑃ⽅ⵇ᥉╋㑍㕏", a_),
								7
							},
							{
								RecordTableEnumerator.b("夷䈹画娽", a_),
								8
							},
							{
								RecordTableEnumerator.b("䬷弹主爽⤿ⱁ⅃㕅", a_),
								9
							}
						};
						num = 26;
						continue;
					case 38:
					{
						string localName;
						int num3;
						if (spr\u22D2.ᝁ.TryGetValue(localName, out num3))
						{
							goto IL_392;
						}
						goto IL_40C;
					}
					case 39:
					{
						int num3;
						switch (num3)
						{
						case 0:
						{
							string a = spr\u1AA0.ᜄ(A_0);
							num = 8;
							continue;
						}
						case 1:
						case 2:
						case 3:
						{
							List<XlsChartSerie> list;
							XlsChartSerie xlsChartSerie = this.ᜀ(A_0, A_1, excelChartType, A_2, list);
							num = 18;
							continue;
						}
						case 4:
						{
							int? num2 = new int?(spr\u1AA0.ᜂ(A_0));
							num = 19;
							continue;
						}
						case 5:
						{
							string value = spr\u1AA0.ᜄ(A_0);
							XLSXSplitType splitType = (XLSXSplitType)Enum.Parse(typeof(XLSXSplitType), value, true);
							XlsChartSerie xlsChartSerie;
							IChartFormat options = xlsChartSerie.Format.Options;
							options.SplitType = (SplitType)splitType;
							num = 4;
							continue;
						}
						case 6:
						{
							XlsChartSerie xlsChartSerie;
							IChartFormat options = xlsChartSerie.Format.Options;
							options.SplitValue = spr\u1AA0.ᜂ(A_0);
							num = 2;
							continue;
						}
						case 7:
						{
							XlsChartSerie xlsChartSerie;
							IChartFormat options = xlsChartSerie.Format.Options;
							options.PieSecondSize = spr\u1AA0.ᜂ(A_0);
							num = 14;
							continue;
						}
						case 8:
						{
							List<XlsChartSerie> list;
							this.ᜀ(A_0, list, A_3);
							num = 29;
							continue;
						}
						case 9:
							goto IL_40C;
						default:
							num = 22;
							continue;
						}
						break;
					}
					case 40:
						goto IL_F0;
					case 41:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num = 6;
							continue;
						}
						goto IL_40C;
					}
					}
					if (A_0 == null)
					{
						num = 40;
						continue;
					}
					num = 13;
					continue;
					IL_22C:
					num = 24;
					continue;
					IL_371:
					num = 38;
					continue;
					IL_392:
					num = 7;
					continue;
					IL_3A3:
					A_0.Skip();
					num = 27;
					continue;
					IL_40C:
					A_0.Skip();
					num = 33;
					continue;
					IL_53F:
					excelChartType = excelChartType2;
					num = 35;
					continue;
					IL_5CA:
					num = 0;
				}
				IL_F0:
				throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹崻娽┿ぁ", a_));
				IL_4F4:
				throw new XmlException(RecordTableEnumerator.b("洷吹夻䘽〿❁❃㉅ⵇ⹉汋㙍㵏㹑瑓≕㥗㵙牛", a_));
				IL_5FF:
				goto IL_642;
				IL_61F:
				throw new ArgumentNullException(RecordTableEnumerator.b("嬷刹崻䰽㐿", a_));
				IL_642:
				A_0.Read();
				return;
			}
			}
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x00111538 File Offset: 0x00110538
		private void ᜀ(XmlReader A_0, XlsChart A_1, RelationsCollection A_2, Dictionary<int, int> A_3, spr\u2306 A_4)
		{
			int a_ = 10;
			int num = 25;
			for (;;)
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
					switch (num)
					{
					case 0:
						num = 8;
						continue;
					case 1:
						goto IL_2A3;
					case 2:
						goto IL_1F7;
					case 3:
						goto IL_20A;
					case 4:
						goto IL_20A;
					case 5:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("ℿ㩁ൃ≅", a_)))
						{
							num = 9;
							continue;
						}
						List<XlsChartSerie> list;
						this.ᜀ(A_0, list, A_3);
						num = 12;
						continue;
					}
					case 6:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("㌿❁㙃", a_)))
						{
							num = 20;
							continue;
						}
						XlsChartSerie xlsChartSerie = this.ᜂ(A_0, A_1, ExcelChartType.Line, A_2, A_4);
						List<XlsChartSerie> list;
						list.Add(xlsChartSerie);
						num = 4;
						continue;
					}
					case 7:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 0;
							continue;
						}
						A_0.Skip();
						num = 19;
						continue;
					case 8:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num = 27;
							continue;
						}
						goto IL_1F7;
					}
					case 9:
						num = 2;
						continue;
					case 10:
						if (A_1 == null)
						{
							num = 1;
							continue;
						}
						num = 11;
						continue;
					case 11:
					{
						if (A_0.LocalName != RecordTableEnumerator.b("㌿㙁⭃╅⍇ॉ⑋⽍≏♑", a_))
						{
							num = 18;
							continue;
						}
						A_0.Read();
						XlsChartSerie xlsChartSerie = null;
						List<XlsChartSerie> list = new List<XlsChartSerie>();
						num = 24;
						continue;
					}
					case 12:
						goto IL_20A;
					case 13:
						goto IL_C1;
					case 14:
						goto IL_20A;
					case 15:
						num = 5;
						continue;
					case 16:
						if (A_0.NodeType == XmlNodeType.EndElement)
						{
							num = 26;
							continue;
						}
						num = 7;
						continue;
					case 17:
						goto IL_20A;
					case 18:
						goto IL_146;
					case 19:
						goto IL_20A;
					case 20:
						num = 23;
						continue;
					case 21:
						num = 22;
						continue;
					case 22:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("㔿㉁C⥅㽇⑉๋⽍≏⅑", a_)))
						{
							num = 15;
							continue;
						}
						XlsChartSerie xlsChartSerie;
						this.ᜁ(A_0, xlsChartSerie, A_2);
						num = 14;
						continue;
					}
					case 23:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("⠿⭁ࡃ⥅㽇ى╋⁍㕏⅑", a_)))
						{
							num = 21;
							continue;
						}
						XlsChartSerie xlsChartSerie;
						this.ᜃ(A_0, xlsChartSerie);
						num = 3;
						continue;
					}
					case 24:
						goto IL_282;
					case 26:
						goto IL_22D;
					case 27:
						num = 6;
						continue;
					}
					if (A_0 == null)
					{
						num = 13;
						continue;
					}
					num = 10;
					continue;
					IL_1F7:
					A_0.Skip();
					num = 17;
					continue;
				}
				IL_20A:
				num = 16;
				continue;
				IL_282:
				goto IL_20A;
			}
			IL_C1:
			throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁╃≅ⵇ㡉", a_));
			IL_146:
			throw new XmlException(RecordTableEnumerator.b("ᔿⱁ⅃㹅㡇⽉⽋㩍㕏㙑瑓⹕㕗㙙籛⩝şա䩣", a_));
			IL_22D:
			A_0.Read();
			return;
			IL_2A3:
			throw new ArgumentNullException(RecordTableEnumerator.b("⌿⩁╃㑅㱇", a_));
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x001118B4 File Offset: 0x001108B4
		private void ᜃ(XmlReader A_0, XlsChartSerie A_1)
		{
			int a_ = 4;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_83;
				case 2:
					goto IL_34;
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
							num = 1;
							continue;
						}
						goto IL_A1;
					}
					break;
				}
				IL_29:
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				num = 3;
				continue;
				goto IL_29;
			}
			IL_34:
			throw new ArgumentNullException(RecordTableEnumerator.b("䠹夻弽␿❁㙃", a_));
			IL_83:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䤹夻䰽⤿❁㝃", a_));
			IL_A1:
			XlsChart parentChart = A_1.ParentChart;
			XlsChartFormat xlsChartFormat = parentChart.PrimaryFormats[A_1.ChartGroup];
			xlsChartFormat.LineStyle = DropLineStyleType.HiLow;
			A_0.Skip();
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x00111988 File Offset: 0x00110988
		private void ᜀ(XmlReader A_0, XlsChart A_1, RelationsCollection A_2, Dictionary<int, int> A_3)
		{
			int a_ = 7;
			switch (0)
			{
			default:
			{
				int num = 18;
				for (;;)
				{
					IChartFormat chartFormat;
					IChartFormat chartFormat2;
					switch (num)
					{
					case 0:
						goto IL_3C6;
					case 1:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("嬼嘾㍀あㅄᑆ╈≊⹌⩎ၐ㵒㉔", a_)))
						{
							num = 30;
							continue;
						}
						chartFormat.FirstSliceAngle = spr\u1AA0.ᜂ(A_0);
						num = 0;
						continue;
					}
					case 2:
						goto IL_418;
					case 3:
						num = 9;
						continue;
					case 4:
						if (A_1 == null)
						{
							num = 6;
							continue;
						}
						num = 11;
						continue;
					case 5:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("唼倾ⵀ♂ᙄ⹆㍈⹊", a_)))
						{
							num = 3;
							continue;
						}
						chartFormat.DoughnutHoleSize = spr\u1AA0.ᜂ(A_0);
						if (true)
						{
						}
						num = 19;
						continue;
					}
					case 6:
						goto IL_33B;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3C4;
						default:
							if (false)
							{
							}
							goto IL_3C6;
						}
						break;
					case 8:
						if (A_0.NodeType == XmlNodeType.EndElement)
						{
							num = 14;
							continue;
						}
						num = 28;
						continue;
					case 9:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("尼䜾ࡀ❂", a_)))
						{
							num = 13;
							continue;
						}
						List<XlsChartSerie> list;
						this.ᜀ(A_0, list, A_3);
						num = 33;
						continue;
					}
					case 10:
						goto IL_D8;
					case 11:
					{
						if (A_0.LocalName != RecordTableEnumerator.b("夼倾㑀⑂ⵄ⥆㱈㽊์❎ぐ⅒⅔", a_))
						{
							num = 24;
							continue;
						}
						A_0.Read();
						List<XlsChartSerie> list = new List<XlsChartSerie>();
						chartFormat = null;
						XlsChartSerie xlsChartSerie = this.ᜀ(A_0, A_1, ExcelChartType.Doughnut, A_2, list);
						num = 27;
						continue;
					}
					case 12:
						if (A_1.HasPivotTable)
						{
							num = 15;
							continue;
						}
						goto IL_441;
					case 13:
						num = 21;
						continue;
					case 14:
						num = 32;
						continue;
					case 15:
						A_1.PivotChartType = ExcelChartType.Doughnut;
						num = 2;
						continue;
					case 16:
						num = 35;
						continue;
					case 17:
						num = 12;
						continue;
					case 19:
						goto IL_3C4;
					case 20:
						num = 34;
						continue;
					case 21:
						goto IL_3F0;
					case 22:
						goto IL_3C6;
					case 23:
						if (A_0.NodeType == XmlNodeType.EndElement)
						{
							num = 17;
							continue;
						}
						num = 26;
						continue;
					case 24:
						goto IL_158;
					case 25:
						num = 8;
						continue;
					case 26:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 16;
							continue;
						}
						goto IL_2C5;
					case 27:
					{
						XlsChartSerie xlsChartSerie;
						if (xlsChartSerie != null)
						{
							num = 25;
							continue;
						}
						goto IL_3C6;
					}
					case 28:
					{
						XlsChartSerie xlsChartSerie;
						chartFormat2 = xlsChartSerie.Format.Options;
						goto IL_1AC;
					}
					case 29:
						num = 1;
						continue;
					case 30:
						num = 5;
						continue;
					case 31:
						goto IL_3C6;
					case 32:
						chartFormat2 = null;
						goto IL_1AC;
					case 33:
						goto IL_3C6;
					case 34:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num = 29;
							continue;
						}
						goto IL_3F0;
					}
					case 35:
					{
						XlsChartSerie xlsChartSerie;
						if (xlsChartSerie != null)
						{
							num = 20;
							continue;
						}
						goto IL_2C5;
					}
					}
					if (A_0 == null)
					{
						num = 10;
						continue;
					}
					num = 4;
					continue;
					IL_1AC:
					chartFormat = chartFormat2;
					num = 31;
					continue;
					IL_2C5:
					A_0.Skip();
					num = 7;
					continue;
					IL_3C6:
					num = 23;
					continue;
					IL_3C4:
					goto IL_3C6;
					IL_3F0:
					A_0.Skip();
					num = 22;
				}
				IL_D8:
				throw new ArgumentNullException(RecordTableEnumerator.b("似娾⁀❂⁄㕆", a_));
				IL_158:
				throw new XmlException(RecordTableEnumerator.b("格儾⑀㭂㕄≆⩈㽊⡌⭎煐⭒㡔㭖祘⽚㱜㡞你", a_));
				IL_33B:
				throw new ArgumentNullException(RecordTableEnumerator.b("帼圾⁀ㅂㅄ", a_));
				IL_418:
				IL_441:
				A_0.Read();
				return;
			}
			}
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x00111DE0 File Offset: 0x00110DE0
		private XlsChartSerie ᜀ(XmlReader A_0, XlsChart A_1, ExcelChartType A_2, RelationsCollection A_3, List<XlsChartSerie> A_4)
		{
			int a_ = 18;
			switch (0)
			{
			default:
			{
				int num = 18;
				XlsChartSerie xlsChartSerie;
				for (;;)
				{
					bool flag;
					switch (num)
					{
					case 0:
						goto IL_1F3;
					case 1:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 22;
							continue;
						}
						A_0.Skip();
						num = 4;
						continue;
					case 2:
						goto IL_110;
					case 3:
						goto IL_CF;
					case 4:
						goto IL_110;
					case 5:
						goto IL_284;
					case 6:
						goto IL_110;
					case 7:
						goto IL_110;
					case 8:
						if (A_0.NodeType != XmlNodeType.EndElement)
						{
							num = 27;
							continue;
						}
						goto IL_161;
					case 9:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("㭇⽉㹋", a_)))
						{
							num = 10;
							continue;
						}
						xlsChartSerie = this.ᜀ(A_0, A_1, A_2, A_3);
						bool isVaryColor;
						xlsChartSerie.Format.Options.IsVaryColor = isVaryColor;
						A_4.Add(xlsChartSerie);
						num = 14;
						continue;
					}
					case 10:
						num = 19;
						continue;
					case 11:
					{
						if (A_1 == null)
						{
							num = 5;
							continue;
						}
						flag = true;
						xlsChartSerie = null;
						bool isVaryColor = false;
						num = 2;
						continue;
					}
					case 12:
						if (A_1.HasPivotTable)
						{
							num = 17;
							continue;
						}
						return xlsChartSerie;
					case 13:
						num = 24;
						continue;
					case 14:
						goto IL_110;
					case 15:
						if (!flag)
						{
							num = 23;
							continue;
						}
						num = 1;
						continue;
					case 16:
						num = 9;
						continue;
					case 17:
						A_1.PivotChartType = A_2;
						num = 0;
						continue;
					case 18:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_23F;
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
					case 19:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("ⱇى⹋≍⍏", a_)))
						{
							num = 13;
							continue;
						}
						this.ᜂ(A_0, xlsChartSerie);
						num = 7;
						continue;
					}
					case 20:
						num = 25;
						continue;
					case 21:
						goto IL_110;
					case 22:
						goto IL_23F;
					case 23:
						goto IL_161;
					case 24:
						goto IL_D4;
					case 25:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("㹇⭉㹋㝍ፏ㵑㡓㥕⩗⥙", a_)))
						{
							num = 16;
							continue;
						}
						bool isVaryColor = spr\u1AA0.ᜃ(A_0);
						num = 6;
						continue;
					}
					case 26:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num = 20;
							continue;
						}
						goto IL_D4;
					}
					case 27:
						num = 15;
						continue;
					}
					if (A_0 == null)
					{
						num = 3;
						continue;
					}
					num = 11;
					continue;
					IL_D4:
					flag = false;
					num = 21;
					continue;
					IL_110:
					num = 8;
					continue;
					IL_161:
					num = 12;
					continue;
					IL_23F:
					num = 26;
				}
				IL_CF:
				throw new ArgumentNullException(RecordTableEnumerator.b("㩇⽉ⵋ⩍㕏⁑", a_));
				IL_1F3:
				return xlsChartSerie;
				IL_284:
				throw new ArgumentNullException(RecordTableEnumerator.b("⭇≉ⵋ㱍⑏", a_));
			}
			}
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x00112134 File Offset: 0x00111134
		private void ᜂ(XmlReader A_0, XlsChartSerie A_1)
		{
			int a_ = 13;
			switch (0)
			{
			default:
			{
				int num = 16;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_139;
					case 1:
						if (A_1 == null)
						{
							num = 12;
							continue;
						}
						num = 14;
						continue;
					case 2:
						goto IL_7D;
					case 3:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 11;
							continue;
						}
						A_0.Skip();
						num = 0;
						continue;
					case 4:
						this.ᜁ(A_0, A_1);
						num = 9;
						continue;
					case 5:
						num = 10;
						continue;
					case 6:
						goto IL_139;
					case 7:
						goto IL_1B3;
					case 8:
						goto IL_139;
					case 9:
						goto IL_139;
					case 10:
					{
						string localName;
						if (localName == RecordTableEnumerator.b("❂ॄ╆╈", a_))
						{
							num = 4;
							continue;
						}
						goto IL_1B8;
					}
					case 11:
						num = 13;
						continue;
					case 12:
						goto IL_101;
					case 13:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num = 5;
							continue;
						}
						goto IL_1B8;
					}
					case 14:
						if (A_0.LocalName != RecordTableEnumerator.b("❂ॄ╆╈㡊", a_))
						{
							num = 7;
							continue;
						}
						A_0.Read();
						num = 8;
						continue;
					case 15:
						goto IL_166;
					case 17:
						if (true)
						{
						}
						if (A_0.NodeType == XmlNodeType.EndElement)
						{
							goto IL_15A;
						}
						num = 3;
						continue;
					}
					if (A_0 == null)
					{
						num = 2;
						continue;
					}
					num = 1;
					continue;
					IL_139:
					num = 17;
					continue;
					IL_15A:
					num = 15;
					continue;
					IL_1B8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_15A;
					default:
					{
						if (false)
						{
						}
						IChartDataLabels dataLabels = A_1.DataPoints.DefaultDataPoint.DataLabels;
						sprវ sprវ = A_1.ParentBook.DataHolder;
						spr\u2306 a_2 = sprវ.\u1718();
						this.ᜀ(A_0, dataLabels, a_2, sprវ);
						num = 6;
						break;
					}
					}
				}
				IL_7D:
				throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄♆ⵈ⹊㽌", a_));
				IL_101:
				throw new ArgumentNullException(RecordTableEnumerator.b("あ⁄㕆⁈⹊㹌", a_));
				IL_166:
				A_0.Read();
				return;
				IL_1B3:
				throw new XmlException(RecordTableEnumerator.b("ᙂ⭄≆ㅈ㭊⡌ⱎ═㙒ㅔ睖⅘㙚ㅜ罞ᕠɢɤ䥦", a_));
			}
			}
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x001123B4 File Offset: 0x001113B4
		private void ᜁ(XmlReader A_0, XlsChartSerie A_1)
		{
			int a_ = 0;
			switch (0)
			{
			default:
			{
				int num = 2;
				for (;;)
				{
					IChartDataLabels chartDataLabels;
					switch (num)
					{
					case 0:
						num = 15;
						continue;
					case 1:
						num = 5;
						continue;
					case 3:
						goto IL_228;
					case 4:
						goto IL_228;
					case 5:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("張尷䈹", a_)))
						{
							num = 24;
							continue;
						}
						int index = spr\u1AA0.ᜂ(A_0);
						chartDataLabels = A_1.DataPoints[index].DataLabels;
						num = 3;
						continue;
					}
					case 6:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("娵夷䌹医䬽㐿", a_)))
						{
							num = 8;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_28C;
						default:
						{
							if (false)
							{
							}
							Stream layoutStream = ShapeParser.ReadNodeAsStream(A_0);
							(chartDataLabels as XlsChartDataLabels).LayoutStream = layoutStream;
							num = 7;
							continue;
						}
						}
						break;
					}
					case 7:
						goto IL_228;
					case 8:
						num = 18;
						continue;
					case 9:
						goto IL_13D;
					case 10:
						goto IL_138;
					case 11:
						goto IL_228;
					case 12:
						goto IL_228;
					case 13:
						if (A_1 == null)
						{
							num = 19;
							continue;
						}
						num = 14;
						continue;
					case 14:
						if (A_0.LocalName != RecordTableEnumerator.b("刵琷堹倻", a_))
						{
							num = 10;
							continue;
						}
						A_0.Read();
						chartDataLabels = null;
						num = 11;
						continue;
					case 15:
					{
						if (true)
						{
						}
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num = 1;
							continue;
						}
						goto IL_13D;
					}
					case 16:
						goto IL_9C;
					case 17:
						goto IL_228;
					case 18:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("刵崷嘹夻䨽┿", a_)))
						{
							num = 23;
							continue;
						}
						bool a_2 = spr\u1AA0.ᜃ(A_0);
						(chartDataLabels as XlsChartDataLabels).IsDelete = a_2;
						num = 17;
						continue;
					}
					case 19:
						goto IL_18B;
					case 20:
						if (A_0.NodeType == XmlNodeType.EndElement)
						{
							num = 21;
							continue;
						}
						num = 22;
						continue;
					case 21:
						goto IL_24D;
					case 22:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 0;
							continue;
						}
						A_0.Skip();
						num = 12;
						continue;
					case 23:
						num = 9;
						continue;
					case 24:
						num = 6;
						continue;
					}
					if (A_0 == null)
					{
						num = 16;
						continue;
					}
					num = 13;
					continue;
					IL_13D:
					sprវ sprវ = A_1.ParentBook.DataHolder;
					spr\u2306 a_3 = sprវ.\u1718();
					this.ᜀ(A_0, chartDataLabels, a_3, sprវ);
					num = 4;
					continue;
					IL_228:
					num = 20;
				}
				IL_9C:
				throw new ArgumentNullException(RecordTableEnumerator.b("䐵崷嬹堻嬽㈿", a_));
				IL_138:
				throw new XmlException(RecordTableEnumerator.b("挵嘷弹䐻丽┿⅁⅃≅桇㉉⅋≍灏♑㕓ㅕ癗", a_));
				IL_18B:
				goto IL_28C;
				IL_24D:
				A_0.Read();
				return;
				IL_28C:
				throw new ArgumentNullException(RecordTableEnumerator.b("䔵崷䠹唻嬽㌿", a_));
			}
			}
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x0011272C File Offset: 0x0011172C
		private void ᜀ(XmlReader A_0, IChartDataLabels A_1, spr\u2306 A_2, sprវ A_3)
		{
			int a_ = 19;
			switch (0)
			{
			default:
			{
				int num = 57;
				for (;;)
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
						switch (num)
						{
						case 0:
							goto IL_2A7;
						case 1:
							goto IL_2A7;
						case 2:
							goto IL_2A7;
						case 3:
							num = 43;
							continue;
						case 4:
							goto IL_2A7;
						case 5:
							goto IL_2A7;
						case 6:
							goto IL_2A7;
						case 7:
							goto IL_2A7;
						case 8:
						{
							IChartDataPoint chartDataPoint;
							XlsChartTextArea textArea = (chartDataPoint.DataLabels as XlsChartDataLabels).TextArea;
							ChartDataPointsCollection chartDataPointsCollection = (ChartDataPointsCollection)chartDataPoint.Parent;
							IEnumerator enumerator = chartDataPointsCollection.GetEnumerator();
							num = 31;
							continue;
						}
						case 9:
							goto IL_2A7;
						case 10:
							num = 53;
							continue;
						case 11:
							A_1.HasValue = spr\u1AA0.ᜃ(A_0);
							num = 9;
							continue;
						case 12:
							if (true)
							{
							}
							A_1.HasPercentage = spr\u1AA0.ᜃ(A_0);
							num = 48;
							continue;
						case 13:
							num = 40;
							continue;
						case 14:
							if (!A_1.HasValue)
							{
								num = 11;
								continue;
							}
							A_0.Skip();
							num = 7;
							continue;
						case 15:
						{
							IChartDataPoint chartDataPoint;
							if ((chartDataPoint.DataLabels as XlsChartDataLabels).TextArea.Text != null)
							{
								num = 8;
								continue;
							}
							goto IL_2A7;
						}
						case 16:
						{
							string text;
							if (text != "")
							{
								num = 55;
								continue;
							}
							goto IL_2A7;
						}
						case 17:
							A_1.HasCategoryName = spr\u1AA0.ᜃ(A_0);
							num = 4;
							continue;
						case 18:
							goto IL_70C;
						case 19:
							spr\u22D2.ᝂ = new Dictionary<string, int>(11)
							{
								{
									RecordTableEnumerator.b("ⵈ݊⽌⍎Ő㱒♔", a_),
									0
								},
								{
									RecordTableEnumerator.b("㩈⍊≌㡎ᵐ㙒㉔㉖㝘㽚ᙜ㩞ᡠ", a_),
									1
								},
								{
									RecordTableEnumerator.b("㩈⍊≌㡎ᵐ㙒㑔㍖㱘⥚ᅜ㙞འ٢ᙤ", a_),
									2
								},
								{
									RecordTableEnumerator.b("㩈⍊≌㡎ݐ㉒㥔", a_),
									3
								},
								{
									RecordTableEnumerator.b("㩈⍊≌㡎ቐ㉒⅔ᥖ㡘㙚㡜", a_),
									4
								},
								{
									RecordTableEnumerator.b("㩈⍊≌㡎Ő㙒❔㑖㱘㕚⥜", a_),
									5
								},
								{
									RecordTableEnumerator.b("㩈⍊≌㡎ፐ♒㝔㕖㕘㹚๜㙞᭠٢", a_),
									6
								},
								{
									RecordTableEnumerator.b("㩈⍊≌㡎ɐ㙒❔ᥖ㡘㙚㡜", a_),
									7
								},
								{
									RecordTableEnumerator.b("㩈⹊㵌⹎⍐㉒⅔㡖⭘", a_),
									8
								},
								{
									RecordTableEnumerator.b("㵈㍊ᵌ㵎", a_),
									9
								},
								{
									RecordTableEnumerator.b("❈㹊⁌ॎ㱐❒", a_),
									10
								}
							};
							num = 32;
							continue;
						case 20:
						{
							bool flag;
							A_1.ShowLeaderLines = flag;
							num = 21;
							continue;
						}
						case 21:
							goto IL_2A7;
						case 22:
						{
							if (A_1 == null)
							{
								num = 50;
								continue;
							}
							sprᮟ sprᮟ = A_1 as sprᮟ;
							sprᮟ.Size = 10.0;
							num = 24;
							continue;
						}
						case 23:
							goto IL_2A7;
						case 24:
							goto IL_2A7;
						case 25:
							if (!A_1.HasPercentage)
							{
								num = 12;
								continue;
							}
							A_0.Skip();
							num = 2;
							continue;
						case 26:
							A_1.HasLegendKey = spr\u1AA0.ᜃ(A_0);
							num = 42;
							continue;
						case 27:
							goto IL_2A7;
						case 28:
							goto IL_2A7;
						case 29:
							goto IL_2A7;
						case 30:
							return;
						case 31:
							try
							{
								num = 3;
								for (;;)
								{
									switch (num)
									{
									case 0:
									{
										IEnumerator enumerator;
										if (!enumerator.MoveNext())
										{
											num = 2;
											continue;
										}
										XlsChartDataPoint xlsChartDataPoint = (XlsChartDataPoint)enumerator.Current;
										num = 1;
										continue;
									}
									case 1:
									{
										XlsChartDataPoint xlsChartDataPoint;
										if (xlsChartDataPoint.HasDataLabels)
										{
											num = 6;
											continue;
										}
										break;
									}
									case 2:
										num = 4;
										continue;
									case 4:
										goto IL_8B1;
									case 5:
									{
										XlsChartDataPoint xlsChartDataPoint;
										if ((xlsChartDataPoint.DataLabels as XlsChartDataLabels).ParagraphType != ChartParagraphType.Default)
										{
											num = 8;
											continue;
										}
										break;
									}
									case 6:
										num = 5;
										continue;
									case 8:
									{
										XlsChartTextArea textArea;
										XlsChartDataPoint xlsChartDataPoint;
										(xlsChartDataPoint.DataLabels as XlsChartDataLabels).TextArea = textArea;
										num = 7;
										continue;
									}
									}
									IL_839:
									num = 0;
									continue;
									goto IL_839;
								}
								IL_8B1:
								goto IL_2A7;
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
											goto IL_8FE;
										case 1:
											goto IL_8FC;
										case 2:
											disposable.Dispose();
											num = 1;
											continue;
										}
										break;
									}
								}
								IL_8FC:
								IL_8FE:;
							}
							goto IL_8FF;
						case 32:
							goto IL_189;
						case 33:
							goto IL_142;
						case 34:
						{
							IChartDataPoint chartDataPoint;
							if (chartDataPoint.IsDefault)
							{
								goto IL_6E4;
							}
							goto IL_2A7;
						}
						case 35:
							if (A_0.NodeType == XmlNodeType.EndElement)
							{
								num = 30;
								continue;
							}
							num = 49;
							continue;
						case 36:
							if (!A_1.HasBubbleSize)
							{
								num = 41;
								continue;
							}
							A_0.Skip();
							num = 27;
							continue;
						case 37:
							goto IL_2A7;
						case 38:
							if (!A_1.HasLegendKey)
							{
								num = 26;
								continue;
							}
							A_0.Skip();
							num = 6;
							continue;
						case 39:
							goto IL_2A7;
						case 40:
						{
							int num2;
							switch (num2)
							{
							case 0:
							{
								string value = spr\u1AA0.ᜄ(A_0);
								XLSXDataLabelPos position = (XLSXDataLabelPos)Enum.Parse(typeof(XLSXDataLabelPos), value, true);
								A_1.Position = (DataLabelPositionType)position;
								num = 37;
								continue;
							}
							case 1:
								num = 38;
								continue;
							case 2:
							{
								bool flag = spr\u1AA0.ᜃ(A_0);
								num = 51;
								continue;
							}
							case 3:
								num = 14;
								continue;
							case 4:
								num = 56;
								continue;
							case 5:
								num = 25;
								continue;
							case 6:
								num = 36;
								continue;
							case 7:
								num = 46;
								continue;
							case 8:
								A_1.Delimiter = A_0.ReadElementContentAsString();
								num = 39;
								continue;
							case 9:
							{
								sprᮟ a_2 = A_1 as sprᮟ;
								this.ᜀ(A_0, a_2, A_2);
								IChartDataPoint chartDataPoint = (IChartDataPoint)(A_1 as XlsChartDataLabels).Parent;
								num = 34;
								continue;
							}
							case 10:
							{
								string text = spr\u1AA0.ᜀ(A_0);
								num = 16;
								continue;
							}
							default:
								num = 52;
								continue;
							}
							break;
						}
						case 41:
							goto IL_8FF;
						case 42:
							goto IL_2A7;
						case 43:
						{
							string localName;
							if ((localName = A_0.LocalName) != null)
							{
								num = 10;
								continue;
							}
							goto IL_70C;
						}
						case 44:
						{
							int num2;
							string localName;
							if (spr\u22D2.ᝂ.TryGetValue(localName, out num2))
							{
								num = 13;
								continue;
							}
							goto IL_70C;
						}
						case 45:
							num = 15;
							continue;
						case 46:
							if (!A_1.HasSeriesName)
							{
								num = 54;
								continue;
							}
							A_0.Skip();
							num = 0;
							continue;
						case 47:
							goto IL_2A7;
						case 48:
							goto IL_2A7;
						case 49:
							if (A_0.NodeType == XmlNodeType.Element)
							{
								num = 3;
								continue;
							}
							A_0.Skip();
							num = 5;
							continue;
						case 50:
							goto IL_667;
						case 51:
						{
							bool flag;
							if (flag)
							{
								num = 20;
								continue;
							}
							goto IL_2A7;
						}
						case 52:
							num = 18;
							continue;
						case 53:
							if (spr\u22D2.ᝂ == null)
							{
								num = 19;
								continue;
							}
							goto IL_189;
						case 54:
							A_1.HasSeriesName = spr\u1AA0.ᜃ(A_0);
							num = 47;
							continue;
						case 55:
						{
							string text;
							(A_1 as XlsChartDataLabels).NumberFormat = text;
							num = 28;
							continue;
						}
						case 56:
							if (!A_1.HasCategoryName)
							{
								num = 17;
								continue;
							}
							A_0.Skip();
							num = 29;
							continue;
						}
						if (A_0 == null)
						{
							num = 33;
							continue;
						}
						num = 22;
						continue;
						IL_189:
						num = 44;
						continue;
						IL_2A7:
						num = 35;
						continue;
						IL_70C:
						RelationsCollection a_3 = new RelationsCollection();
						spr\u1AA0.ᜀ(A_0, A_1 as sprᮟ, a_3, A_3, new float?(10f));
						num = 23;
						continue;
						IL_8FF:
						A_1.HasBubbleSize = spr\u1AA0.ᜃ(A_0);
						num = 1;
						continue;
					}
					}
					IL_6E4:
					num = 45;
				}
				IL_142:
				throw new ArgumentNullException(RecordTableEnumerator.b("㭈⹊ⱌ⭎㑐⅒", a_));
				IL_667:
				throw new ArgumentNullException(RecordTableEnumerator.b("ⵈ⩊㥌⹎ᵐ㉒㝔㉖㕘⡚", a_));
			}
			}
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x00113094 File Offset: 0x00112094
		private XlsChartSerie ᜂ(XmlReader A_0, XlsChart A_1, ExcelChartType A_2, RelationsCollection A_3)
		{
			int a_ = 19;
			switch (0)
			{
			default:
			{
				int num = 32;
				XlsChartSerie xlsChartSerie;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_203;
					case 1:
						if (spr\u22D2.ᝃ == null)
						{
							num = 29;
							continue;
						}
						goto IL_22D;
					case 2:
						goto IL_203;
					case 3:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num = 9;
							continue;
						}
						goto IL_1EF;
					}
					case 4:
						goto IL_228;
					case 5:
						goto IL_203;
					case 6:
						goto IL_203;
					case 7:
						goto IL_203;
					case 8:
						num = 16;
						continue;
					case 9:
						num = 1;
						continue;
					case 10:
						goto IL_203;
					case 11:
						if (A_0.NodeType == XmlNodeType.EndElement)
						{
							num = 4;
							continue;
						}
						num = 25;
						continue;
					case 12:
						goto IL_22D;
					case 13:
					{
						object[] array;
						if (array != null)
						{
							num = 22;
							continue;
						}
						goto IL_203;
					}
					case 14:
					{
						object[] array;
						xlsChartSerie.EnteredDirectlyCategoryLabels = array;
						num = 26;
						continue;
					}
					case 15:
						goto IL_203;
					case 16:
						goto IL_1EF;
					case 17:
					{
						string localName;
						int num2;
						if (spr\u22D2.ᝃ.TryGetValue(localName, out num2))
						{
							num = 24;
							continue;
						}
						goto IL_1EF;
					}
					case 18:
						if (A_1 == null)
						{
							num = 31;
							continue;
						}
						xlsChartSerie = (XlsChartSerie)A_1.Series.Add(A_2);
						this.ᜃ(A_0, xlsChartSerie, A_3);
						num = 0;
						continue;
					case 19:
					{
						object[] array;
						if (array != null)
						{
							num = 14;
							continue;
						}
						goto IL_203;
					}
					case 20:
						goto IL_203;
					case 21:
						goto IL_C2;
					case 22:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_228;
						default:
							if (false)
							{
							}
							num = 27;
							continue;
						}
						break;
					case 23:
					{
						object[] array;
						xlsChartSerie.EnteredDirectlyValues = array;
						num = 10;
						continue;
					}
					case 24:
						num = 28;
						continue;
					case 25:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 30;
							continue;
						}
						A_0.Skip();
						num = 15;
						continue;
					case 26:
						goto IL_203;
					case 27:
						if (xlsChartSerie.Values == null)
						{
							num = 23;
							continue;
						}
						goto IL_203;
					case 28:
					{
						int num2;
						switch (num2)
						{
						case 0:
							this.ᜂ(A_0, xlsChartSerie);
							num = 6;
							continue;
						case 1:
							this.ᜅ(A_0, xlsChartSerie, A_3);
							num = 20;
							continue;
						case 2:
							this.ᜆ(A_0, xlsChartSerie, A_3);
							num = 5;
							continue;
						case 3:
						{
							object[] array;
							xlsChartSerie.CategoryLabels = this.ᜀ(A_0, xlsChartSerie, out array);
							if (true)
							{
							}
							num = 19;
							continue;
						}
						case 4:
						{
							object[] array;
							xlsChartSerie.Values = this.ᜀ(A_0, xlsChartSerie, out array);
							num = 13;
							continue;
						}
						case 5:
							this.ᜂ(A_0, xlsChartSerie, A_3);
							num = 7;
							continue;
						default:
							num = 8;
							continue;
						}
						break;
					}
					case 29:
						spr\u22D2.ᝃ = new Dictionary<string, int>(6)
						{
							{
								RecordTableEnumerator.b("ⵈ݊⽌⍎≐", a_),
								0
							},
							{
								RecordTableEnumerator.b("㵈㥊⡌ⅎ㕐㽒㱔㥖㱘", a_),
								1
							},
							{
								RecordTableEnumerator.b("ⱈ㥊㽌ൎぐ⅒♔", a_),
								2
							},
							{
								RecordTableEnumerator.b("⩈⩊㥌", a_),
								3
							},
							{
								RecordTableEnumerator.b("㽈⩊⅌", a_),
								4
							},
							{
								RecordTableEnumerator.b("ⵈᭊ㥌", a_),
								5
							}
						};
						num = 12;
						continue;
					case 30:
						num = 3;
						continue;
					case 31:
						goto IL_440;
					}
					if (A_0 == null)
					{
						num = 21;
						continue;
					}
					num = 18;
					continue;
					IL_1EF:
					A_0.Skip();
					num = 2;
					continue;
					IL_203:
					num = 11;
					continue;
					IL_22D:
					num = 17;
				}
				IL_C2:
				throw new ArgumentNullException(RecordTableEnumerator.b("㭈⹊ⱌ⭎㑐⅒", a_));
				IL_228:
				A_0.Read();
				return xlsChartSerie;
				IL_440:
				throw new ArgumentNullException(RecordTableEnumerator.b("⩈⍊ⱌ㵎═", a_));
			}
			}
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x00113518 File Offset: 0x00112518
		private XlsChartSerie ᜁ(XmlReader A_0, XlsChart A_1, ExcelChartType A_2, RelationsCollection A_3)
		{
			int a_ = 14;
			int num = 8;
			XlsChartSerie xlsChartSerie;
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
					goto IL_12C;
				}
				case 1:
					goto IL_223;
				case 2:
					goto IL_223;
				case 3:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 16;
						continue;
					}
					A_0.Skip();
					num = 2;
					continue;
				case 4:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 22;
						continue;
					}
					num = 3;
					continue;
				case 5:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("㉃❅⑇", a_)))
					{
						num = 25;
						continue;
					}
					object[] array;
					xlsChartSerie.Values = this.ᜀ(A_0, xlsChartSerie, out array);
					num = 21;
					continue;
				}
				case 6:
				{
					object[] array;
					if (array != null)
					{
						num = 23;
						continue;
					}
					goto IL_223;
				}
				case 7:
					IL_C0:
					num = 13;
					continue;
				case 9:
					goto IL_17F;
				case 10:
				{
					object[] array;
					xlsChartSerie.EnteredDirectlyCategoryLabels = array;
					num = 24;
					continue;
				}
				case 11:
					if (A_1 == null)
					{
						if (true)
						{
						}
						num = 9;
						continue;
					}
					xlsChartSerie = (XlsChartSerie)A_1.Series.Add(A_2);
					this.ᜃ(A_0, xlsChartSerie, A_3);
					num = 1;
					continue;
				case 12:
					goto IL_12C;
				case 13:
					if (xlsChartSerie.Values == null)
					{
						num = 10;
						continue;
					}
					goto IL_223;
				case 14:
					goto IL_8F;
				case 15:
					goto IL_223;
				case 16:
					num = 0;
					continue;
				case 17:
					num = 20;
					continue;
				case 18:
					num = 5;
					continue;
				case 19:
					goto IL_223;
				case 20:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("❃❅㱇", a_)))
					{
						num = 18;
						continue;
					}
					object[] array;
					xlsChartSerie.CategoryLabels = this.ᜀ(A_0, xlsChartSerie, out array);
					num = 6;
					continue;
				}
				case 21:
				{
					object[] array;
					if (array != null)
					{
						num = 7;
						continue;
					}
					goto IL_223;
				}
				case 22:
					goto IL_246;
				case 23:
				{
					object[] array;
					xlsChartSerie.EnteredDirectlyCategoryLabels = array;
					num = 15;
					continue;
				}
				case 24:
					goto IL_223;
				case 25:
					num = 12;
					continue;
				}
				if (A_0 == null)
				{
					num = 14;
					continue;
				}
				num = 11;
				continue;
				IL_12C:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_C0;
				default:
					if (false)
					{
					}
					A_0.Skip();
					num = 19;
					continue;
				}
				IL_223:
				num = 4;
			}
			IL_8F:
			throw new ArgumentNullException(RecordTableEnumerator.b("㙃⍅⥇⹉⥋㱍", a_));
			IL_17F:
			throw new ArgumentNullException(RecordTableEnumerator.b("❃⹅⥇㡉㡋", a_));
			IL_246:
			A_0.Read();
			return xlsChartSerie;
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x00113820 File Offset: 0x00112820
		private XlsChartSerie ᜀ(XmlReader A_0, XlsChart A_1, ExcelChartType A_2, RelationsCollection A_3)
		{
			int a_ = 2;
			switch (0)
			{
			default:
			{
				int num = 22;
				XlsChartSerie xlsChartSerie;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_223;
					case 1:
					{
						object[] array;
						if (array != null)
						{
							num = 20;
							continue;
						}
						goto IL_223;
					}
					case 2:
					{
						int num2;
						switch (num2)
						{
						case 0:
							xlsChartSerie.Format.Percent = spr\u1AA0.ᜂ(A_0);
							num = 27;
							continue;
						case 1:
							this.ᜂ(A_0, xlsChartSerie);
							num = 30;
							continue;
						case 2:
							this.ᜅ(A_0, xlsChartSerie, A_3);
							num = 7;
							continue;
						case 3:
							this.ᜆ(A_0, xlsChartSerie, A_3);
							num = 21;
							continue;
						case 4:
						{
							object[] array;
							xlsChartSerie.CategoryLabels = this.ᜀ(A_0, xlsChartSerie, out array);
							num = 1;
							continue;
						}
						case 5:
						{
							object[] array;
							xlsChartSerie.Values = this.ᜀ(A_0, xlsChartSerie, out array);
							num = 10;
							continue;
						}
						case 6:
							this.ᜂ(A_0, xlsChartSerie, A_3);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								num = 32;
								continue;
							}
							break;
						default:
							num = 3;
							continue;
						}
						break;
					}
					case 3:
						num = 28;
						continue;
					case 4:
						num = 29;
						continue;
					case 5:
						goto IL_223;
					case 6:
						goto IL_24D;
					case 7:
						goto IL_223;
					case 8:
						num = 31;
						continue;
					case 9:
						goto IL_248;
					case 10:
					{
						object[] array;
						if (array != null)
						{
							num = 8;
							continue;
						}
						goto IL_223;
					}
					case 11:
						if (A_1 == null)
						{
							num = 14;
							continue;
						}
						xlsChartSerie = (XlsChartSerie)A_1.Series.Add(A_2);
						this.ᜃ(A_0, xlsChartSerie, A_3);
						num = 23;
						continue;
					case 12:
						goto IL_223;
					case 13:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num = 4;
							continue;
						}
						goto IL_192;
					}
					case 14:
						goto IL_45F;
					case 15:
						if (A_0.NodeType == XmlNodeType.EndElement)
						{
							num = 9;
							continue;
						}
						num = 18;
						continue;
					case 16:
						spr\u22D2.ᝄ = new Dictionary<string, int>(7)
						{
							{
								RecordTableEnumerator.b("崷䈹䰻刽⼿ㅁⵃ⥅♇", a_),
								0
							},
							{
								RecordTableEnumerator.b("尷瘹帻刽㌿", a_),
								1
							},
							{
								RecordTableEnumerator.b("䰷䠹夻倽␿⹁ⵃ⡅ⵇ", a_),
								2
							},
							{
								RecordTableEnumerator.b("崷䠹主簽ℿぁ㝃", a_),
								3
							},
							{
								RecordTableEnumerator.b("嬷嬹䠻", a_),
								4
							},
							{
								RecordTableEnumerator.b("丷嬹倻", a_),
								5
							},
							{
								RecordTableEnumerator.b("尷樹䠻", a_),
								6
							}
						};
						num = 6;
						continue;
					case 17:
					{
						object[] array;
						xlsChartSerie.EnteredDirectlyCategoryLabels = array;
						num = 5;
						continue;
					}
					case 18:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 19;
							continue;
						}
						goto IL_223;
					case 19:
						num = 13;
						continue;
					case 20:
					{
						object[] array;
						xlsChartSerie.EnteredDirectlyCategoryLabels = array;
						num = 0;
						continue;
					}
					case 21:
						goto IL_223;
					case 23:
						goto IL_223;
					case 24:
						num = 2;
						continue;
					case 25:
						goto IL_D4;
					case 26:
					{
						int num2;
						string localName;
						if (spr\u22D2.ᝄ.TryGetValue(localName, out num2))
						{
							num = 24;
							continue;
						}
						goto IL_192;
					}
					case 27:
						goto IL_223;
					case 28:
						goto IL_192;
					case 29:
						if (spr\u22D2.ᝄ == null)
						{
							num = 16;
							continue;
						}
						goto IL_24D;
					case 30:
						goto IL_223;
					case 31:
						if (xlsChartSerie.Values == null)
						{
							num = 17;
							continue;
						}
						goto IL_223;
					case 32:
						goto IL_223;
					}
					if (true)
					{
					}
					if (A_0 == null)
					{
						num = 25;
						continue;
					}
					num = 11;
					continue;
					IL_192:
					A_0.Skip();
					num = 12;
					continue;
					IL_223:
					num = 15;
					continue;
					IL_24D:
					num = 26;
				}
				IL_D4:
				throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹崻娽┿ぁ", a_));
				IL_248:
				A_0.Read();
				return xlsChartSerie;
				IL_45F:
				throw new ArgumentNullException(RecordTableEnumerator.b("嬷刹崻䰽㐿", a_));
			}
			}
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x00113CCC File Offset: 0x00112CCC
		private XlsChartSerie ᜂ(XmlReader A_0, XlsChart A_1, ExcelChartType A_2, RelationsCollection A_3, spr\u2306 A_4)
		{
			int a_ = 18;
			switch (0)
			{
			default:
			{
				int num = 30;
				XlsChartSerie xlsChartSerie;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_21A;
					case 1:
						if (spr\u22D2.ᝅ == null)
						{
							num = 33;
							continue;
						}
						goto IL_2DB;
					case 2:
						num = 15;
						continue;
					case 3:
						goto IL_2DB;
					case 4:
						num = 31;
						continue;
					case 5:
					{
						int num2;
						switch (num2)
						{
						case 0:
						{
							spr\u1A7B a_2 = new spr\u1A7B(xlsChartSerie.Format.LineProperties, null, null, xlsChartSerie.Format.Shadow, xlsChartSerie.Format.Format3D);
							sprវ a_3;
							spr\u1AA0.ᜀ(A_0, a_2, a_3, A_3);
							num = 8;
							continue;
						}
						case 1:
							this.ᜀ(A_0, xlsChartSerie, A_4);
							num = 12;
							continue;
						case 2:
							this.ᜂ(A_0, xlsChartSerie);
							num = 11;
							continue;
						case 3:
							this.ᜅ(A_0, xlsChartSerie, A_3);
							num = 34;
							continue;
						case 4:
							this.ᜆ(A_0, xlsChartSerie, A_3);
							num = 28;
							continue;
						case 5:
						{
							object[] array;
							xlsChartSerie.CategoryLabels = this.ᜀ(A_0, xlsChartSerie, out array);
							num = 9;
							continue;
						}
						case 6:
						{
							object[] array;
							xlsChartSerie.Values = this.ᜀ(A_0, xlsChartSerie, out array);
							num = 21;
							continue;
						}
						case 7:
							this.ᜂ(A_0, xlsChartSerie, A_3);
							num = 18;
							continue;
						default:
							num = 4;
							continue;
						}
						break;
					}
					case 6:
					{
						object[] array;
						xlsChartSerie.EnteredDirectlyCategoryLabels = array;
						num = 10;
						continue;
					}
					case 7:
						goto IL_1F5;
					case 8:
						if (true)
						{
						}
						goto IL_1F5;
					case 9:
					{
						object[] array;
						if (array != null)
						{
							num = 6;
							continue;
						}
						goto IL_1F5;
					}
					case 10:
						goto IL_1F5;
					case 11:
						goto IL_1F5;
					case 12:
						goto IL_1F5;
					case 13:
						goto IL_CA;
					case 14:
						goto IL_4D9;
					case 15:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num = 32;
							continue;
						}
						goto IL_30D;
					}
					case 16:
					{
						object[] array;
						xlsChartSerie.EnteredDirectlyCategoryLabels = array;
						num = 17;
						continue;
					}
					case 17:
						goto IL_1F5;
					case 18:
						goto IL_1F5;
					case 19:
						if (xlsChartSerie.Values == null)
						{
							num = 16;
							continue;
						}
						goto IL_1F5;
					case 20:
					{
						if (A_1 == null)
						{
							num = 14;
							continue;
						}
						xlsChartSerie = (XlsChartSerie)A_1.Series.Add(A_2);
						this.ᜃ(A_0, xlsChartSerie, A_3);
						sprវ a_3 = xlsChartSerie.ParentBook.DataHolder;
						num = 26;
						continue;
					}
					case 21:
					{
						object[] array;
						if (array != null)
						{
							num = 22;
							continue;
						}
						goto IL_1F5;
					}
					case 22:
						num = 19;
						continue;
					case 23:
						if (A_0.NodeType == XmlNodeType.EndElement)
						{
							num = 0;
							continue;
						}
						num = 25;
						continue;
					case 24:
						goto IL_1F5;
					case 25:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 2;
							continue;
						}
						A_0.Skip();
						num = 24;
						continue;
					case 26:
						goto IL_1F5;
					case 27:
					{
						int num2;
						string localName;
						if (spr\u22D2.ᝅ.TryGetValue(localName, out num2))
						{
							num = 29;
							continue;
						}
						goto IL_30D;
					}
					case 28:
						goto IL_1F5;
					case 29:
						num = 5;
						continue;
					case 31:
						goto IL_30D;
					case 32:
						num = 1;
						continue;
					case 33:
						spr\u22D2.ᝅ = new Dictionary<string, int>(8)
						{
							{
								RecordTableEnumerator.b("㭇㩉᱋㱍", a_),
								0
							},
							{
								RecordTableEnumerator.b("╇⭉㹋╍㕏⁑", a_),
								1
							},
							{
								RecordTableEnumerator.b("ⱇى⹋≍⍏", a_),
								2
							},
							{
								RecordTableEnumerator.b("㱇㡉⥋⁍㑏㹑㵓㡕㵗", a_),
								3
							},
							{
								RecordTableEnumerator.b("ⵇ㡉㹋్ㅏ⁑❓", a_),
								4
							},
							{
								RecordTableEnumerator.b("⭇⭉㡋", a_),
								5
							},
							{
								RecordTableEnumerator.b("㹇⭉⁋", a_),
								6
							},
							{
								RecordTableEnumerator.b("ⱇᩉ㡋", a_),
								7
							}
						};
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_CA;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 34:
						goto IL_1F5;
					}
					if (A_0 == null)
					{
						num = 13;
						continue;
					}
					num = 20;
					continue;
					IL_1F5:
					num = 23;
					continue;
					IL_2DB:
					num = 27;
					continue;
					IL_30D:
					A_0.Skip();
					num = 7;
				}
				IL_CA:
				throw new ArgumentNullException(RecordTableEnumerator.b("㩇⽉ⵋ⩍㕏⁑", a_));
				IL_21A:
				A_0.Read();
				return xlsChartSerie;
				IL_4D9:
				throw new ArgumentNullException(RecordTableEnumerator.b("⭇≉ⵋ㱍⑏", a_));
			}
			}
		}

		// Token: 0x06001F46 RID: 8006 RVA: 0x00114204 File Offset: 0x00113204
		private XlsChartSerie ᜁ(XmlReader A_0, XlsChart A_1, ExcelChartType A_2, RelationsCollection A_3, spr\u2306 A_4)
		{
			int a_ = 17;
			switch (0)
			{
			default:
			{
				int num = 32;
				XlsChartSerie xlsChartSerie;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_231;
					case 1:
					{
						object[] array;
						if (array != null)
						{
							num = 9;
							continue;
						}
						goto IL_231;
					}
					case 2:
						num = 21;
						continue;
					case 3:
						goto IL_231;
					case 4:
						goto IL_231;
					case 5:
						if (A_1 == null)
						{
							num = 27;
							continue;
						}
						xlsChartSerie = (XlsChartSerie)A_1.Series.Add(A_2);
						this.ᜃ(A_0, xlsChartSerie, A_3);
						num = 29;
						continue;
					case 6:
						goto IL_231;
					case 7:
						goto IL_EC;
					case 8:
						goto IL_231;
					case 9:
					{
						object[] array;
						xlsChartSerie.EnteredDirectlyCategoryLabels = array;
						num = 28;
						continue;
					}
					case 10:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 11;
							continue;
						}
						A_0.Skip();
						num = 8;
						continue;
					case 11:
						num = 33;
						continue;
					case 12:
						goto IL_30F;
					case 13:
						goto IL_231;
					case 14:
					{
						object[] array;
						if (array != null)
						{
							num = 22;
							continue;
						}
						goto IL_231;
					}
					case 15:
					{
						string localName;
						int num2;
						if (spr\u22D2.ᝆ.TryGetValue(localName, out num2))
						{
							num = 2;
							continue;
						}
						goto IL_341;
					}
					case 16:
						if (A_0.NodeType == XmlNodeType.EndElement)
						{
							num = 17;
							continue;
						}
						num = 10;
						continue;
					case 17:
						goto IL_256;
					case 18:
						goto IL_231;
					case 19:
						spr\u22D2.ᝆ = new Dictionary<string, int>(9)
						{
							{
								RecordTableEnumerator.b("㑆㥈ᭊ㽌", a_),
								0
							},
							{
								RecordTableEnumerator.b("⩆⡈㥊♌⩎⍐", a_),
								1
							},
							{
								RecordTableEnumerator.b("⍆Ո⥊⅌㱎", a_),
								2
							},
							{
								RecordTableEnumerator.b("㍆㭈⹊⍌⭎㵐㩒㭔㉖", a_),
								3
							},
							{
								RecordTableEnumerator.b("≆㭈㥊ཌ⹎⍐⁒", a_),
								4
							},
							{
								RecordTableEnumerator.b("㽆Ὀ⩊⅌", a_),
								5
							},
							{
								RecordTableEnumerator.b("㹆Ὀ⩊⅌", a_),
								6
							},
							{
								RecordTableEnumerator.b("㑆⑈⑊≌㭎㥐", a_),
								7
							},
							{
								RecordTableEnumerator.b("⍆᥈㽊", a_),
								8
							}
						};
						num = 12;
						continue;
					case 20:
						num = 31;
						continue;
					case 21:
					{
						int num2;
						switch (num2)
						{
						case 0:
						{
							XlsChartSerieDataFormat a_2 = (XlsChartSerieDataFormat)xlsChartSerie.Format;
							sprវ a_3 = xlsChartSerie.ParentBook.DataHolder;
							spr\u230D a_4 = new spr\u230D(a_2);
							spr\u1AA0.ᜀ(A_0, a_4, a_3, A_3);
							num = 4;
							continue;
						}
						case 1:
							this.ᜀ(A_0, xlsChartSerie, A_4);
							num = 6;
							continue;
						case 2:
							this.ᜂ(A_0, xlsChartSerie);
							num = 25;
							continue;
						case 3:
							this.ᜅ(A_0, xlsChartSerie, A_3);
							num = 0;
							continue;
						case 4:
							this.ᜆ(A_0, xlsChartSerie, A_3);
							num = 30;
							continue;
						case 5:
						{
							object[] array;
							xlsChartSerie.CategoryLabels = this.ᜀ(A_0, xlsChartSerie, out array);
							num = 1;
							continue;
						}
						case 6:
						{
							object[] array;
							xlsChartSerie.Values = this.ᜀ(A_0, xlsChartSerie, out array);
							num = 14;
							continue;
						}
						case 7:
						{
							bool isSmoothedLine = spr\u1AA0.ᜃ(A_0);
							XlsChartSerieDataFormat xlsChartSerieDataFormat = (XlsChartSerieDataFormat)xlsChartSerie.Format;
							xlsChartSerieDataFormat.IsSmoothedLine = isSmoothedLine;
							num = 13;
							continue;
						}
						case 8:
							this.ᜂ(A_0, xlsChartSerie, A_3);
							num = 18;
							continue;
						default:
							num = 26;
							continue;
						}
						break;
					}
					case 22:
					{
						object[] array;
						xlsChartSerie.EnteredDirectlyValues = array;
						num = 24;
						continue;
					}
					case 23:
						goto IL_341;
					case 24:
						goto IL_231;
					case 25:
						goto IL_231;
					case 26:
						if (true)
						{
						}
						num = 23;
						continue;
					case 27:
						goto IL_4F5;
					case 28:
						goto IL_231;
					case 29:
						goto IL_231;
					case 30:
						goto IL_231;
					case 31:
						if (spr\u22D2.ᝆ == null)
						{
							num = 19;
							continue;
						}
						goto IL_30F;
					case 33:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num = 20;
							continue;
						}
						goto IL_341;
					}
					}
					if (A_0 != null)
					{
						num = 5;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_EC;
					default:
						if (false)
						{
						}
						num = 7;
						continue;
					}
					IL_231:
					num = 16;
					continue;
					IL_30F:
					num = 15;
					continue;
					IL_341:
					A_0.Skip();
					num = 3;
				}
				IL_EC:
				throw new ArgumentNullException(RecordTableEnumerator.b("㕆ⱈ⩊⥌⩎⍐", a_));
				IL_256:
				A_0.Read();
				return xlsChartSerie;
				IL_4F5:
				throw new ArgumentNullException(RecordTableEnumerator.b("⑆ⅈ⩊㽌㭎", a_));
			}
			}
		}

		// Token: 0x06001F47 RID: 8007 RVA: 0x00114748 File Offset: 0x00113748
		private XlsChartSerie ᜀ(XmlReader A_0, XlsChart A_1, ExcelChartType A_2, RelationsCollection A_3, spr\u2306 A_4)
		{
			int a_ = 9;
			int num = 22;
			XlsChartSerie xlsChartSerie;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 25;
					continue;
				case 1:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 7;
						continue;
					}
					num = 3;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_137;
					default:
						if (false)
						{
						}
						num = 23;
						continue;
					}
					break;
				case 3:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 20;
						continue;
					}
					A_0.Skip();
					num = 10;
					continue;
				case 4:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("嬾ീ⅂⥄㑆", a_)))
					{
						num = 8;
						continue;
					}
					this.ᜂ(A_0, xlsChartSerie);
					num = 5;
					continue;
				}
				case 5:
					goto IL_1DC;
				case 6:
					goto IL_1DC;
				case 7:
					goto IL_1FF;
				case 8:
					num = 32;
					continue;
				case 9:
					num = 13;
					continue;
				case 10:
					goto IL_1DC;
				case 11:
					goto IL_1DC;
				case 12:
				{
					object[] array;
					if (array != null)
					{
						num = 19;
						continue;
					}
					goto IL_1DC;
				}
				case 13:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("嬾ᅀ㝂", a_)))
					{
						num = 0;
						continue;
					}
					goto IL_137;
				}
				case 14:
					if (A_1 == null)
					{
						num = 30;
						continue;
					}
					xlsChartSerie = (XlsChartSerie)A_1.Series.Add(A_2);
					this.ᜃ(A_0, xlsChartSerie, A_3);
					num = 6;
					continue;
				case 15:
					if (xlsChartSerie.Values == null)
					{
						num = 31;
						continue;
					}
					goto IL_1DC;
				case 16:
					num = 4;
					continue;
				case 17:
					goto IL_1DC;
				case 18:
					num = 34;
					continue;
				case 19:
				{
					object[] array;
					xlsChartSerie.EnteredDirectlyCategoryLabels = array;
					if (true)
					{
					}
					num = 11;
					continue;
				}
				case 20:
					num = 33;
					continue;
				case 21:
					goto IL_B9;
				case 23:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("䤾⁀⽂", a_)))
					{
						num = 9;
						continue;
					}
					object[] array;
					xlsChartSerie.Values = this.ᜀ(A_0, xlsChartSerie, out array);
					num = 27;
					continue;
				}
				case 24:
					goto IL_1DC;
				case 25:
					goto IL_1C9;
				case 26:
					num = 15;
					continue;
				case 27:
				{
					object[] array;
					if (array != null)
					{
						num = 26;
						continue;
					}
					goto IL_1DC;
				}
				case 28:
					goto IL_1DC;
				case 29:
					goto IL_1DC;
				case 30:
					goto IL_3DA;
				case 31:
				{
					object[] array;
					xlsChartSerie.EnteredDirectlyValues = array;
					num = 24;
					continue;
				}
				case 32:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("尾⁀㝂", a_)))
					{
						num = 2;
						continue;
					}
					object[] array;
					xlsChartSerie.CategoryLabels = this.ᜀ(A_0, xlsChartSerie, out array);
					num = 12;
					continue;
				}
				case 33:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 18;
						continue;
					}
					goto IL_1C9;
				}
				case 34:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("刾⁀ㅂ⹄≆㭈", a_)))
					{
						num = 16;
						continue;
					}
					this.ᜀ(A_0, xlsChartSerie, A_4);
					num = 17;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 21;
					continue;
				}
				num = 14;
				continue;
				IL_137:
				this.ᜂ(A_0, xlsChartSerie, A_3);
				num = 28;
				continue;
				IL_1C9:
				A_0.Skip();
				num = 29;
				continue;
				IL_1DC:
				num = 1;
			}
			IL_B9:
			throw new ArgumentNullException(RecordTableEnumerator.b("䴾⑀≂⅄≆㭈", a_));
			IL_1FF:
			A_0.Read();
			return xlsChartSerie;
			IL_3DA:
			throw new ArgumentNullException(RecordTableEnumerator.b("尾⥀≂㝄㍆", a_));
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x00114B64 File Offset: 0x00113B64
		private XlsChartSerie ᜀ(XmlReader A_0, XlsChart A_1, RelationsCollection A_2)
		{
			int a_ = 14;
			switch (0)
			{
			default:
			{
				int num = 15;
				XlsChartSerie xlsChartSerie;
				for (;;)
				{
					string localName;
					int num2;
					switch (num)
					{
					case 0:
						if (A_1 == null)
						{
							num = 9;
							continue;
						}
						num = 17;
						continue;
					case 1:
						goto IL_23E;
					case 2:
						goto IL_23E;
					case 3:
						goto IL_23E;
					case 4:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 30;
							continue;
						}
						A_0.Skip();
						num = 1;
						continue;
					case 5:
						goto IL_23E;
					case 6:
						goto IL_263;
					case 7:
					{
						object[] array;
						if (array != null)
						{
							num = 18;
							continue;
						}
						goto IL_23E;
					}
					case 8:
					{
						object[] array;
						xlsChartSerie.EnteredDirectlyCategoryLabels = array;
						num = 31;
						continue;
					}
					case 9:
						goto IL_524;
					case 10:
						goto IL_23E;
					case 11:
						num = 37;
						continue;
					case 12:
						goto IL_1D0;
					case 13:
						goto IL_18E;
					case 14:
						goto IL_268;
					case 16:
					{
						object[] array;
						if (array != null)
						{
							num = 20;
							continue;
						}
						goto IL_23E;
					}
					case 17:
						if (A_0.LocalName != RecordTableEnumerator.b("㝃⍅㩇", a_))
						{
							num = 13;
							continue;
						}
						xlsChartSerie = (XlsChartSerie)A_1.Series.Add(ExcelChartType.Bubble);
						this.ᜃ(A_0, xlsChartSerie, A_2);
						num = 3;
						continue;
					case 18:
					{
						object[] array;
						xlsChartSerie.EnteredDirectlyCategoryLabels = array;
						num = 10;
						continue;
					}
					case 19:
						goto IL_DA;
					case 20:
					{
						object[] array;
						xlsChartSerie.EnteredDirectlyCategoryLabels = array;
						num = 29;
						continue;
					}
					case 21:
						if (A_0.NodeType == XmlNodeType.EndElement)
						{
							num = 6;
							continue;
						}
						num = 4;
						continue;
					case 22:
						goto IL_23E;
					case 23:
						goto IL_23E;
					case 24:
						num = 26;
						continue;
					case 25:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_274;
						default:
							if (false)
							{
							}
							num = 12;
							continue;
						}
						break;
					case 26:
						if (spr\u22D2.ᝇ == null)
						{
							num = 28;
							continue;
						}
						goto IL_268;
					case 27:
						goto IL_23E;
					case 28:
						if (true)
						{
						}
						spr\u22D2.ᝇ = new Dictionary<string, int>(8)
						{
							{
								RecordTableEnumerator.b("⁃੅⩇♉㽋", a_),
								0
							},
							{
								RecordTableEnumerator.b("ぃ㑅ⵇ⑉⡋≍㥏㱑ㅓ", a_),
								1
							},
							{
								RecordTableEnumerator.b("⅃㑅㩇ࡉⵋ㱍⍏", a_),
								2
							},
							{
								RecordTableEnumerator.b("㱃၅⥇♉", a_),
								3
							},
							{
								RecordTableEnumerator.b("㵃၅⥇♉", a_),
								4
							},
							{
								RecordTableEnumerator.b("♃㍅⩇⡉⁋⭍͏㭑⹓㍕", a_),
								5
							},
							{
								RecordTableEnumerator.b("♃㍅⩇⡉⁋⭍捏ᙑ", a_),
								6
							},
							{
								RecordTableEnumerator.b("⁃ᙅ㱇", a_),
								7
							}
						};
						num = 14;
						continue;
					case 29:
						goto IL_23E;
					case 30:
						num = 35;
						continue;
					case 31:
						goto IL_23E;
					case 32:
						xlsChartSerie.Format.Is3DBubbles = true;
						num = 33;
						continue;
					case 33:
						goto IL_23E;
					case 34:
						goto IL_274;
					case 35:
						if ((localName = A_0.LocalName) != null)
						{
							num = 24;
							continue;
						}
						goto IL_1D0;
					case 36:
					{
						object[] array;
						if (array != null)
						{
							num = 8;
							continue;
						}
						goto IL_23E;
					}
					case 37:
						switch (num2)
						{
						case 0:
							this.ᜂ(A_0, xlsChartSerie);
							num = 27;
							continue;
						case 1:
							this.ᜅ(A_0, xlsChartSerie, A_2);
							num = 5;
							continue;
						case 2:
							this.ᜆ(A_0, xlsChartSerie, A_2);
							num = 2;
							continue;
						case 3:
						{
							object[] array;
							xlsChartSerie.CategoryLabels = this.ᜀ(A_0, xlsChartSerie, out array);
							num = 36;
							continue;
						}
						case 4:
						{
							object[] array;
							xlsChartSerie.Values = this.ᜀ(A_0, xlsChartSerie, out array);
							num = 16;
							continue;
						}
						case 5:
						{
							object[] array;
							xlsChartSerie.Bubbles = this.ᜀ(A_0, xlsChartSerie, out array);
							num = 7;
							continue;
						}
						case 6:
						{
							bool flag = spr\u1AA0.ᜃ(A_0);
							num = 38;
							continue;
						}
						case 7:
							this.ᜂ(A_0, xlsChartSerie, A_2);
							num = 22;
							continue;
						default:
							num = 25;
							continue;
						}
						break;
					case 38:
					{
						bool flag;
						if (flag)
						{
							num = 32;
							continue;
						}
						goto IL_23E;
					}
					}
					if (A_0 == null)
					{
						num = 19;
						continue;
					}
					num = 0;
					continue;
					IL_1D0:
					A_0.Skip();
					num = 23;
					continue;
					IL_274:
					if (spr\u22D2.ᝇ.TryGetValue(localName, out num2))
					{
						num = 11;
						continue;
					}
					goto IL_1D0;
					IL_23E:
					num = 21;
					continue;
					IL_268:
					num = 34;
				}
				IL_DA:
				throw new ArgumentNullException(RecordTableEnumerator.b("㙃⍅⥇⹉⥋㱍", a_));
				IL_18E:
				throw new XmlException(RecordTableEnumerator.b("ᅃ⡅ⵇ㉉㱋⭍㍏♑ㅓ㉕硗≙ㅛ㉝䁟ᙡգť䙧", a_));
				IL_263:
				A_0.Read();
				return xlsChartSerie;
				IL_524:
				throw new ArgumentNullException(RecordTableEnumerator.b("❃⹅⥇㡉㡋", a_));
			}
			}
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x001150EC File Offset: 0x001140EC
		private XlsChartSerie ᜀ(XmlReader A_0, XlsChart A_1, ExcelChartType A_2, RelationsCollection A_3, bool A_4)
		{
			int a_ = 16;
			switch (0)
			{
			default:
			{
				int num = 1;
				XlsChartSerie xlsChartSerie;
				for (;;)
				{
					ExcelChartType excelChartType;
					switch (num)
					{
					case 0:
					{
						object[] array;
						if (array != null)
						{
							num = 19;
							continue;
						}
						goto IL_279;
					}
					case 2:
						goto IL_2E0;
					case 3:
						num = 4;
						continue;
					case 4:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num = 12;
							continue;
						}
						goto IL_203;
					}
					case 5:
						goto IL_279;
					case 6:
						goto IL_203;
					case 7:
					{
						object[] array;
						xlsChartSerie.EnteredDirectlyCategoryLabels = array;
						num = 38;
						continue;
					}
					case 8:
						goto IL_217;
					case 9:
						num = 37;
						continue;
					case 10:
						goto IL_EE;
					case 11:
						if (excelChartType != A_2)
						{
							num = 25;
							continue;
						}
						goto IL_45C;
					case 12:
						num = 26;
						continue;
					case 13:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 3;
							continue;
						}
						A_0.Skip();
						num = 34;
						continue;
					case 14:
						goto IL_3EC;
					case 15:
						goto IL_279;
					case 16:
						goto IL_279;
					case 17:
						if (excelChartType != A_2)
						{
							num = 21;
							continue;
						}
						goto IL_2E0;
					case 18:
						if (!A_4)
						{
							num = 20;
							continue;
						}
						goto IL_3EC;
					case 19:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_250;
						default:
						{
							if (false)
							{
							}
							object[] array;
							xlsChartSerie.EnteredDirectlyCategoryLabels = array;
							num = 42;
							continue;
						}
						}
						break;
					case 20:
						xlsChartSerie.UsePrimaryAxis = A_4;
						goto IL_250;
					case 21:
						num = 27;
						continue;
					case 22:
						num = 6;
						continue;
					case 23:
						goto IL_279;
					case 24:
					{
						object[] array;
						if (array != null)
						{
							num = 9;
							continue;
						}
						goto IL_279;
					}
					case 25:
						xlsChartSerie.SerieType = A_2;
						num = 41;
						continue;
					case 26:
						if (spr\u22D2.ᝈ == null)
						{
							num = 33;
							continue;
						}
						goto IL_217;
					case 27:
						if (excelChartType == ExcelChartType.CombinationChart)
						{
							num = 2;
							continue;
						}
						goto IL_162;
					case 28:
						goto IL_29E;
					case 29:
						goto IL_279;
					case 30:
						goto IL_162;
					case 31:
						goto IL_531;
					case 32:
					{
						string localName;
						int num2;
						if (spr\u22D2.ᝈ.TryGetValue(localName, out num2))
						{
							num = 36;
							continue;
						}
						goto IL_203;
					}
					case 33:
						spr\u22D2.ᝈ = new Dictionary<string, int>(6)
						{
							{
								RecordTableEnumerator.b("≅ч⡉⁋㵍", a_),
								0
							},
							{
								RecordTableEnumerator.b("㉅㩇⽉≋⩍㱏㭑㩓㍕", a_),
								1
							},
							{
								RecordTableEnumerator.b("⍅㩇㡉๋⽍≏⅑", a_),
								2
							},
							{
								RecordTableEnumerator.b("╅⥇㹉", a_),
								3
							},
							{
								RecordTableEnumerator.b("ぅ⥇♉", a_),
								4
							},
							{
								RecordTableEnumerator.b("≅ᡇ㹉", a_),
								5
							}
						};
						num = 8;
						continue;
					case 34:
						goto IL_279;
					case 35:
						if (A_0.NodeType == XmlNodeType.EndElement)
						{
							num = 28;
							continue;
						}
						num = 13;
						continue;
					case 36:
						num = 40;
						continue;
					case 37:
						if (xlsChartSerie.Values == null)
						{
							num = 7;
							continue;
						}
						goto IL_279;
					case 38:
						goto IL_279;
					case 39:
						if (A_1 == null)
						{
							num = 31;
							continue;
						}
						excelChartType = A_1.ChartType;
						num = 17;
						continue;
					case 40:
					{
						int num2;
						switch (num2)
						{
						case 0:
							this.ᜂ(A_0, xlsChartSerie);
							num = 16;
							continue;
						case 1:
							this.ᜅ(A_0, xlsChartSerie, A_3);
							num = 5;
							continue;
						case 2:
							this.ᜆ(A_0, xlsChartSerie, A_3);
							num = 23;
							continue;
						case 3:
						{
							object[] array;
							xlsChartSerie.CategoryLabels = this.ᜀ(A_0, xlsChartSerie, out array);
							num = 0;
							continue;
						}
						case 4:
						{
							object[] array;
							xlsChartSerie.Values = this.ᜀ(A_0, xlsChartSerie, out array);
							if (true)
							{
							}
							num = 24;
							continue;
						}
						case 5:
							this.ᜂ(A_0, xlsChartSerie, A_3);
							num = 15;
							continue;
						default:
							num = 22;
							continue;
						}
						break;
					}
					case 41:
						goto IL_45C;
					case 42:
						goto IL_279;
					case 43:
						goto IL_279;
					}
					if (A_0 == null)
					{
						num = 10;
						continue;
					}
					num = 39;
					continue;
					IL_162:
					xlsChartSerie = (XlsChartSerie)A_1.Series.Add(excelChartType);
					num = 18;
					continue;
					IL_203:
					A_0.Skip();
					num = 43;
					continue;
					IL_217:
					num = 32;
					continue;
					IL_250:
					num = 14;
					continue;
					IL_279:
					num = 35;
					continue;
					IL_2E0:
					excelChartType = A_2;
					num = 30;
					continue;
					IL_3EC:
					num = 11;
					continue;
					IL_45C:
					this.ᜃ(A_0, xlsChartSerie, A_3);
					num = 29;
				}
				IL_EE:
				throw new ArgumentNullException(RecordTableEnumerator.b("㑅ⵇ⭉⡋⭍≏", a_));
				IL_29E:
				A_0.Read();
				return xlsChartSerie;
				IL_531:
				throw new ArgumentNullException(RecordTableEnumerator.b("╅⁇⭉㹋㩍", a_));
			}
			}
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x0011567C File Offset: 0x0011467C
		private void ᜃ(XmlReader A_0, XlsChartSerie A_1, RelationsCollection A_2)
		{
			int a_ = 19;
			if (true)
			{
			}
			int num = 32;
			for (;;)
			{
				bool flag;
				switch (num)
				{
				case 0:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("⁈⽊㕌", a_)))
					{
						num = 2;
						continue;
					}
					string s = spr\u1AA0.ᜄ(A_0);
					A_1.Index = int.Parse(s);
					num = 11;
					continue;
				}
				case 1:
					goto IL_151;
				case 2:
					num = 30;
					continue;
				case 3:
					num = 19;
					continue;
				case 4:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("㵈㍊", a_)))
					{
						num = 18;
						continue;
					}
					this.ᜀ(A_0, A_1);
					num = 1;
					continue;
				}
				case 5:
					goto IL_1AF;
				case 6:
					A_1.InvertNegaColor = new bool?(XmlConvert.ToBoolean(A_0.Value));
					num = 26;
					continue;
				case 7:
					num = 34;
					continue;
				case 8:
					if (A_1 == null)
					{
						num = 14;
						continue;
					}
					num = 22;
					continue;
				case 9:
					goto IL_1FF;
				case 10:
					goto IL_C1;
				case 11:
					goto IL_151;
				case 12:
					goto IL_2A4;
				case 13:
					num = 4;
					continue;
				case 14:
					goto IL_3AC;
				case 15:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 3;
						continue;
					}
					A_0.Skip();
					num = 29;
					continue;
				case 16:
					num = 0;
					continue;
				case 17:
					goto IL_151;
				case 18:
					num = 20;
					continue;
				case 19:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 16;
						continue;
					}
					goto IL_FC;
				}
				case 20:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("㩈㭊ᵌ㵎", a_)))
					{
						num = 25;
						continue;
					}
					this.ᜀ(A_0, A_1, A_2);
					num = 28;
					continue;
				}
				case 21:
					goto IL_FC;
				case 22:
					if (A_0.LocalName != RecordTableEnumerator.b("㩈⹊㽌", a_))
					{
						num = 12;
						continue;
					}
					A_0.Read();
					flag = true;
					num = 17;
					continue;
				case 23:
					goto IL_151;
				case 24:
					goto IL_151;
				case 25:
					num = 27;
					continue;
				case 26:
					goto IL_151;
				case 27:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("⁈╊㭌⩎⍐❒᱔ㅖ᝘㹚㩜㹞ᕠ੢፤ɦ", a_)))
					{
						num = 5;
						continue;
					}
					num = 31;
					continue;
				}
				case 28:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1AF;
					default:
						if (false)
						{
						}
						goto IL_151;
					}
					break;
				case 29:
					goto IL_151;
				case 30:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("♈㥊⥌⩎⍐", a_)))
					{
						num = 13;
						continue;
					}
					spr\u1AA0.ᜄ(A_0);
					num = 24;
					continue;
				}
				case 31:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("㽈⩊⅌", a_)))
					{
						num = 6;
						continue;
					}
					goto IL_151;
				case 33:
					if (A_0.NodeType != XmlNodeType.EndElement)
					{
						num = 7;
						continue;
					}
					return;
				case 34:
					if (!flag)
					{
						num = 9;
						continue;
					}
					num = 15;
					continue;
				}
				if (A_0 == null)
				{
					num = 10;
					continue;
				}
				num = 8;
				continue;
				IL_FC:
				flag = false;
				num = 23;
				continue;
				IL_151:
				num = 33;
				continue;
				IL_1AF:
				num = 21;
			}
			IL_C1:
			throw new ArgumentNullException(RecordTableEnumerator.b("㭈⹊ⱌ⭎㑐⅒", a_));
			IL_1FF:
			return;
			IL_2A4:
			throw new XmlException(RecordTableEnumerator.b("᱈╊⡌㝎⅐㙒㙔⍖㱘㽚絜❞ౠར䕤፦ࡨ౪䍬", a_));
			IL_3AC:
			throw new ArgumentNullException(RecordTableEnumerator.b("㩈⹊㽌♎㑐⁒", a_));
		}

		// Token: 0x06001F4B RID: 8011 RVA: 0x00115AA4 File Offset: 0x00114AA4
		private void ᜀ(XmlReader A_0, XlsChartSerie A_1)
		{
			int a_ = 9;
			int num = 24;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!A_0.IsEmptyElement)
					{
						num = 12;
						continue;
					}
					goto IL_304;
				case 1:
					goto IL_10A;
				case 2:
					num = 15;
					continue;
				case 3:
					goto IL_105;
				case 4:
					goto IL_20D;
				case 5:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 21;
						continue;
					}
					goto IL_10A;
				}
				case 6:
					goto IL_9A;
				case 7:
					if (A_0.LocalName != RecordTableEnumerator.b("䬾㥀", a_))
					{
						num = 3;
						continue;
					}
					num = 0;
					continue;
				case 8:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("䤾", a_)))
					{
						num = 2;
						continue;
					}
					A_1.Name = A_0.ReadElementContentAsString();
					num = 13;
					continue;
				}
				case 9:
					goto IL_20D;
				case 10:
					if (A_1.IsDefaultName)
					{
						num = 22;
						continue;
					}
					goto IL_195;
				case 11:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 16;
						continue;
					}
					goto IL_195;
				case 12:
					A_0.Read();
					num = 4;
					continue;
				case 13:
					goto IL_20D;
				case 14:
					num = 1;
					continue;
				case 15:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("䰾㕀ㅂᝄ≆⽈", a_)))
					{
						num = 14;
						continue;
					}
					A_1.Name = RecordTableEnumerator.b("Ⱦ", a_) + this.ᜃ(A_0);
					num = 9;
					continue;
				}
				case 16:
					num = 5;
					continue;
				case 17:
					goto IL_20D;
				case 18:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 19;
						continue;
					}
					num = 10;
					continue;
				case 19:
					goto IL_22D;
				case 20:
					goto IL_155;
				case 21:
					num = 8;
					continue;
				case 22:
					num = 11;
					continue;
				case 23:
					goto IL_20D;
				case 25:
					if (A_1 == null)
					{
						num = 20;
						continue;
					}
					num = 7;
					continue;
				}
				if (A_0 == null)
				{
					if (true)
					{
					}
					num = 6;
					continue;
				}
				num = 25;
				continue;
				IL_10A:
				A_0.Skip();
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_25A;
				default:
					if (false)
					{
					}
					num = 23;
					continue;
				}
				IL_195:
				A_0.Skip();
				num = 17;
				continue;
				IL_20D:
				num = 18;
			}
			IL_9A:
			throw new ArgumentNullException(RecordTableEnumerator.b("䴾⑀≂⅄≆㭈", a_));
			IL_105:
			throw new XmlException(RecordTableEnumerator.b("樾⽀♂㵄㝆ⱈ⡊㥌⩎㕐獒ⵔ㩖㕘筚⥜㹞٠䵢", a_));
			IL_155:
			goto IL_25A;
			IL_22D:
			goto IL_304;
			IL_25A:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰾⑀ㅂⱄ≆㩈", a_));
			IL_304:
			A_0.Read();
		}

		// Token: 0x06001F4C RID: 8012 RVA: 0x00115DBC File Offset: 0x00114DBC
		private void ᜂ(XmlReader A_0, XlsChartSerie A_1, RelationsCollection A_2)
		{
			int a_ = 11;
			switch (0)
			{
			default:
			{
				int num = 18;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_AF;
					case 1:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("ⱀ≂㝄ⱆⱈ㥊", a_)))
						{
							num = 5;
							continue;
						}
						IChartDataPoint chartDataPoint;
						XlsChartSerieDataFormat a_2 = chartDataPoint.DataFormat as XlsChartSerieDataFormat;
						sprវ sprវ;
						this.ᜁ(A_0, a_2, sprវ.\u1718());
						num = 9;
						continue;
					}
					case 2:
						goto IL_212;
					case 3:
						num = 26;
						continue;
					case 4:
						num = 1;
						continue;
					case 5:
						num = 15;
						continue;
					case 6:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 20;
							continue;
						}
						A_0.Read();
						num = 10;
						continue;
					case 7:
						goto IL_1ED;
					case 8:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("㉀㍂ᕄ㕆", a_)))
						{
							num = 4;
							continue;
						}
						num = 23;
						continue;
					}
					case 9:
						goto IL_1ED;
					case 10:
						goto IL_1ED;
					case 11:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num = 3;
							continue;
						}
						goto IL_1D9;
					}
					case 12:
						goto IL_1ED;
					case 13:
					{
						sprវ sprវ = A_1.ParentChart.ParentWorkbook.DataHolder;
						A_0.Read();
						IChartDataPoint chartDataPoint = null;
						num = 12;
						continue;
					}
					case 14:
						if (A_0.NodeType == XmlNodeType.EndElement)
						{
							num = 2;
							continue;
						}
						num = 6;
						continue;
					case 15:
						goto IL_1D9;
					case 16:
						goto IL_1ED;
					case 17:
						goto IL_1ED;
					case 19:
						if (A_0.LocalName != RecordTableEnumerator.b("╀ፂㅄ", a_))
						{
							num = 24;
							continue;
						}
						num = 25;
						continue;
					case 20:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3CA;
						default:
							if (false)
							{
							}
							num = 11;
							continue;
						}
						break;
					case 21:
						goto IL_3E8;
					case 22:
						if (A_1 == null)
						{
							num = 21;
							continue;
						}
						num = 19;
						continue;
					case 23:
					{
						IChartDataPoint chartDataPoint;
						if (chartDataPoint == null)
						{
							num = 28;
							continue;
						}
						XlsChartSerieDataFormat xlsChartSerieDataFormat = chartDataPoint.DataFormat as XlsChartSerieDataFormat;
						xlsChartSerieDataFormat.HasLineProperties = true;
						xlsChartSerieDataFormat.HasInterior = true;
						spr\u1A7B a_3 = new spr\u1A7B(xlsChartSerieDataFormat.LineProperties, xlsChartSerieDataFormat.Interior as XlsChartInterior, xlsChartSerieDataFormat.Fill as spr\u1C26, xlsChartSerieDataFormat.Shadow, xlsChartSerieDataFormat.Format3D);
						sprវ sprវ;
						spr\u1AA0.ᜀ(A_0, a_3, sprវ, A_2);
						num = 17;
						continue;
					}
					case 24:
						goto IL_287;
					case 25:
						if (!A_0.IsEmptyElement)
						{
							num = 13;
							continue;
						}
						goto IL_3FE;
					case 26:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("⡀❂㵄", a_)))
						{
							num = 27;
							continue;
						}
						string s = spr\u1AA0.ᜄ(A_0);
						int index = int.Parse(s);
						IChartDataPoint chartDataPoint = A_1.DataPoints[index];
						num = 7;
						continue;
					}
					case 27:
						num = 8;
						continue;
					case 28:
						goto IL_D2;
					}
					if (A_0 == null)
					{
						num = 0;
						continue;
					}
					goto IL_3CA;
					IL_1D9:
					A_0.Skip();
					num = 16;
					continue;
					IL_1ED:
					num = 14;
					continue;
					IL_3CA:
					num = 22;
				}
				IL_AF:
				throw new ArgumentNullException(RecordTableEnumerator.b("㍀♂⑄⍆ⱈ㥊", a_));
				IL_D2:
				throw new XmlException();
				IL_212:
				goto IL_3FE;
				IL_287:
				throw new XmlException();
				IL_3E8:
				throw new ArgumentNullException(RecordTableEnumerator.b("㉀♂㝄⹆ⱈ㡊", a_));
				IL_3FE:
				if (true)
				{
				}
				A_0.Read();
				return;
			}
			}
		}

		// Token: 0x06001F4D RID: 8013 RVA: 0x001161D8 File Offset: 0x001151D8
		private IXLSRange ᜀ(XmlReader A_0, XlsChartSerie A_1, out object[] A_2)
		{
			int a_ = 12;
			switch (0)
			{
			default:
			{
				int num = 28;
				IXLSRange ixlsrange;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_4D2;
					case 1:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("ㅁぃ㑅ᩇ⽉⩋", a_)))
						{
							num = 13;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_578;
						default:
						{
							if (false)
							{
							}
							bool a_2 = true;
							string text = this.ᜃ(A_0);
							num = 40;
							continue;
						}
						}
						break;
					}
					case 2:
						num = 42;
						continue;
					case 3:
						goto IL_4D2;
					case 4:
						if (ixlsrange != null)
						{
							num = 9;
							continue;
						}
						return ixlsrange;
					case 5:
					{
						string text;
						if (text.StartsWith(RecordTableEnumerator.b("橁", a_)))
						{
							num = 49;
							continue;
						}
						goto IL_6C4;
					}
					case 6:
						goto IL_6C4;
					case 7:
						goto IL_4D2;
					case 8:
					{
						XlsWorkbook xlsWorkbook = A_1.ParentBook;
						FormulaUtil formulaUtil = xlsWorkbook.DataHolder.\u1718().ᜀ();
						string text;
						Ptg[] array = formulaUtil.ᜃ(text);
						sprỜ sprỜ = array[0] as sprỜ;
						ixlsrange = sprỜ.ᜀ(xlsWorkbook, xlsWorkbook.Worksheets[0]);
						goto IL_578;
					}
					case 9:
						num = 51;
						continue;
					case 10:
						num = 5;
						continue;
					case 11:
					{
						string text;
						if (text.Split(new char[]
						{
							','
						}).Length > 1)
						{
							num = 45;
							continue;
						}
						goto IL_4D2;
					}
					case 12:
					{
						string text;
						A_1.StrRefFormula = text;
						num = 3;
						continue;
					}
					case 13:
						num = 20;
						continue;
					case 14:
						if (ixlsrange is XlsRange)
						{
							num = 35;
							continue;
						}
						num = 31;
						continue;
					case 15:
						A_0.Read();
						num = 30;
						continue;
					case 16:
						goto IL_4D2;
					case 17:
						goto IL_634;
					case 18:
						num = 32;
						continue;
					case 19:
					{
						string text;
						if (text.EndsWith(RecordTableEnumerator.b("歁", a_)))
						{
							num = 23;
							continue;
						}
						goto IL_6C4;
					}
					case 20:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("⽁ㅃ⩅㱇⍉K㡍㱏ő⁓⑕੗㽙㩛", a_)))
						{
							num = 22;
							continue;
						}
						bool a_3 = true;
						string text = this.ᜂ(A_0);
						num = 0;
						continue;
					}
					case 21:
					{
						string text;
						if (text != null)
						{
							num = 8;
							continue;
						}
						return ixlsrange;
					}
					case 22:
						num = 29;
						continue;
					case 23:
					{
						string text = text.Substring(1, text.Length - 2);
						num = 6;
						continue;
					}
					case 24:
						goto IL_4D2;
					case 25:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num = 2;
							continue;
						}
						goto IL_264;
					}
					case 26:
						goto IL_4D2;
					case 27:
					{
						if (A_1 == null)
						{
							num = 17;
							continue;
						}
						A_0.Read();
						string text = null;
						A_2 = null;
						bool a_4 = false;
						bool a_2 = false;
						bool a_3 = false;
						num = 43;
						continue;
					}
					case 29:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("ⱁㅃ⭅ч⍉㡋", a_)))
						{
							num = 18;
							continue;
						}
						A_2 = this.ᜁ(A_0);
						num = 24;
						continue;
					}
					case 30:
					{
						string text;
						if (text != null)
						{
							num = 10;
							continue;
						}
						goto IL_6C4;
					}
					case 31:
						if (ixlsrange is XlsName)
						{
							num = 34;
							continue;
						}
						return ixlsrange;
					case 32:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("ㅁぃ㑅ч⍉㡋", a_)))
						{
							num = 39;
							continue;
						}
						if (true)
						{
						}
						A_2 = this.ᜁ(A_0);
						num = 7;
						continue;
					}
					case 33:
						goto IL_25F;
					case 34:
					{
						bool a_4;
						(ixlsrange as XlsName).IsNumReference = a_4;
						bool a_2;
						(ixlsrange as XlsName).IsStringReference = a_2;
						bool a_3;
						(ixlsrange as XlsName).IsMultiReference = a_3;
						num = 38;
						continue;
					}
					case 35:
					{
						bool a_4;
						(ixlsrange as XlsRange).IsNumReference = a_4;
						bool a_2;
						(ixlsrange as XlsRange).IsStringReference = a_2;
						bool a_3;
						(ixlsrange as XlsRange).IsMultiReference = a_3;
						num = 33;
						continue;
					}
					case 36:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 50;
							continue;
						}
						A_0.Skip();
						num = 26;
						continue;
					case 37:
						goto IL_112;
					case 38:
						goto IL_31D;
					case 39:
						num = 46;
						continue;
					case 40:
					{
						string text;
						if (text.Split(new char[]
						{
							','
						}).Length > 1)
						{
							num = 12;
							continue;
						}
						goto IL_4D2;
					}
					case 41:
						if (A_0.NodeType == XmlNodeType.EndElement)
						{
							num = 15;
							continue;
						}
						num = 36;
						continue;
					case 42:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("ⱁㅃ⭅ᩇ⽉⩋", a_)))
						{
							num = 48;
							continue;
						}
						bool a_4 = true;
						string text = this.ᜀ(A_0, out A_2);
						num = 11;
						continue;
					}
					case 43:
						goto IL_4D2;
					case 44:
						goto IL_2E5;
					case 45:
					{
						string text;
						A_1.NumRefFormula = text;
						num = 47;
						continue;
					}
					case 46:
						goto IL_264;
					case 47:
						goto IL_4D2;
					case 48:
						num = 1;
						continue;
					case 49:
						num = 19;
						continue;
					case 50:
						num = 25;
						continue;
					case 51:
						if (ixlsrange is spr\u20A6)
						{
							num = 52;
							continue;
						}
						num = 14;
						continue;
					case 52:
					{
						bool a_4;
						(ixlsrange as spr\u20A6).ᜀ(a_4);
						bool a_2;
						(ixlsrange as spr\u20A6).ᜁ(a_2);
						bool a_3;
						(ixlsrange as spr\u20A6).ᜂ(a_3);
						num = 44;
						continue;
					}
					}
					if (A_0 == null)
					{
						num = 37;
						continue;
					}
					num = 27;
					continue;
					IL_264:
					A_0.Skip();
					num = 16;
					continue;
					IL_4D2:
					num = 41;
					continue;
					IL_578:
					num = 4;
					continue;
					IL_6C4:
					IWorksheet worksheet = A_1.ParentBook.Worksheets[0];
					ixlsrange = null;
					num = 21;
				}
				IL_112:
				throw new ArgumentNullException(RecordTableEnumerator.b("ぁ⅃❅ⱇ⽉㹋", a_));
				IL_25F:
				IL_2E5:
				IL_31D:
				return ixlsrange;
				IL_634:
				throw new ArgumentNullException(RecordTableEnumerator.b("ㅁ⅃㑅ⅇ⽉㽋", a_));
			}
			}
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x001168E4 File Offset: 0x001158E4
		private string ᜀ(XmlReader A_0, out object[] A_1)
		{
			int a_ = 8;
			int num = 12;
			string result;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_131;
				case 1:
					goto IL_D4;
				case 2:
					goto IL_131;
				case 3:
					goto IL_131;
				case 4:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("倽㔿⽁݃❅⭇≉⥋", a_)))
					{
						num = 14;
						continue;
					}
					A_1 = this.ᜁ(A_0);
					num = 20;
					continue;
				}
				case 5:
					num = 4;
					continue;
				case 6:
					num = 19;
					continue;
				case 7:
					goto IL_11B;
				case 8:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 9;
						continue;
					}
					num = 10;
					continue;
				case 9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						goto IL_16A;
					}
					break;
				case 10:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 6;
						continue;
					}
					A_0.Skip();
					num = 2;
					continue;
				case 11:
					A_1 = null;
					num = 3;
					continue;
				case 13:
					goto IL_7C;
				case 14:
					num = 1;
					continue;
				case 15:
					num = 21;
					continue;
				case 16:
					if (A_0.LocalName != RecordTableEnumerator.b("倽㔿⽁ᙃ⍅⹇", a_))
					{
						num = 7;
						continue;
					}
					A_0.Read();
					result = null;
					A_1 = null;
					num = 18;
					continue;
				case 17:
					goto IL_131;
				case 18:
					goto IL_131;
				case 19:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 15;
						continue;
					}
					goto IL_D4;
				}
				case 20:
					if (A_1.Length == 0)
					{
						num = 11;
						continue;
					}
					goto IL_131;
				case 21:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("堽", a_)))
					{
						num = 5;
						continue;
					}
					result = A_0.ReadElementContentAsString();
					num = 0;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 13;
					continue;
				}
				num = 16;
				continue;
				IL_D4:
				A_0.Skip();
				num = 17;
				continue;
				IL_131:
				num = 8;
			}
			IL_7C:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰽┿⍁⁃⍅㩇", a_));
			IL_11B:
			if (true)
			{
			}
			throw new XmlException(RecordTableEnumerator.b("欽⸿❁㱃㙅ⵇ⥉㡋⭍㑏牑ⱓ㭕㑗穙⡛㽝ݟ䱡", a_));
			IL_16A:
			if (false)
			{
			}
			A_0.Read();
			return result;
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x00116B80 File Offset: 0x00115B80
		private string ᜃ(XmlReader A_0)
		{
			int a_ = 12;
			int num = 0;
			string result;
			for (;;)
			{
				switch (num)
				{
				case 1:
					num = 11;
					continue;
				case 2:
					goto IL_D3;
				case 3:
					goto IL_156;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_69;
					default:
						if (false)
						{
						}
						goto IL_136;
					}
					break;
				case 5:
					num = 8;
					continue;
				case 6:
					goto IL_64;
				case 7:
					goto IL_69;
				case 8:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 1;
						continue;
					}
					goto IL_107;
				}
				case 9:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 5;
						continue;
					}
					A_0.Skip();
					num = 10;
					continue;
				case 10:
					goto IL_136;
				case 11:
				{
					string localName;
					if (localName == RecordTableEnumerator.b("⑁", a_))
					{
						num = 7;
						continue;
					}
					goto IL_107;
				}
				case 12:
					goto IL_136;
				case 13:
					if (A_0.LocalName != RecordTableEnumerator.b("ㅁぃ㑅ᩇ⽉⩋", a_))
					{
						num = 2;
						continue;
					}
					A_0.Read();
					result = null;
					num = 15;
					continue;
				case 14:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 3;
						continue;
					}
					num = 9;
					continue;
				case 15:
					goto IL_136;
				}
				if (A_0 == null)
				{
					num = 6;
					continue;
				}
				num = 13;
				continue;
				IL_69:
				result = A_0.ReadElementContentAsString();
				num = 12;
				continue;
				IL_107:
				A_0.Skip();
				num = 4;
				continue;
				IL_136:
				num = 14;
			}
			IL_64:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("ぁ⅃❅ⱇ⽉㹋", a_));
			IL_D3:
			throw new XmlException(RecordTableEnumerator.b("ᝁ⩃⍅ぇ㩉⥋ⵍ⑏㝑こ癕⁗㝙せ繝ᑟͣ͡䡥", a_));
			IL_156:
			A_0.Read();
			return result;
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x00116D84 File Offset: 0x00115D84
		private string ᜂ(XmlReader A_0)
		{
			int a_ = 3;
			int num = 3;
			string result;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_69;
				case 1:
					goto IL_DB;
				case 2:
					goto IL_156;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_69;
					default:
						if (false)
						{
						}
						goto IL_136;
					}
					break;
				case 5:
					if (A_0.LocalName != RecordTableEnumerator.b("吸为儼䬾⡀ག㍄⭆ᩈ㽊㽌ᵎ㑐㕒", a_))
					{
						num = 1;
						continue;
					}
					A_0.Read();
					result = null;
					num = 8;
					continue;
				case 6:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 2;
						continue;
					}
					num = 11;
					continue;
				case 7:
					goto IL_136;
				case 8:
					goto IL_136;
				case 9:
				{
					string localName;
					if (localName == RecordTableEnumerator.b("弸", a_))
					{
						num = 0;
						continue;
					}
					goto IL_107;
				}
				case 10:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						if (true)
						{
						}
						num = 15;
						continue;
					}
					goto IL_107;
				}
				case 11:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 13;
						continue;
					}
					A_0.Skip();
					num = 12;
					continue;
				case 12:
					goto IL_136;
				case 13:
					num = 10;
					continue;
				case 14:
					goto IL_64;
				case 15:
					num = 9;
					continue;
				}
				if (A_0 == null)
				{
					num = 14;
					continue;
				}
				num = 5;
				continue;
				IL_69:
				result = A_0.ReadElementContentAsString();
				num = 7;
				continue;
				IL_107:
				A_0.Skip();
				num = 4;
				continue;
				IL_136:
				num = 6;
			}
			IL_64:
			throw new ArgumentNullException(RecordTableEnumerator.b("䬸帺尼嬾⑀ㅂ", a_));
			IL_DB:
			throw new XmlException(RecordTableEnumerator.b("永唺堼䜾ㅀ♂♄㍆ⱈ⽊浌㝎㱐㽒畔⍖㡘㱚獜", a_));
			IL_156:
			A_0.Read();
			return result;
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x00116F88 File Offset: 0x00115F88
		private void ᜀ(XmlReader A_0, XlsChartSerie A_1, spr\u2306 A_2)
		{
			int a_ = 17;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_6F;
				case 1:
					if (A_0.LocalName != RecordTableEnumerator.b("⩆⡈㥊♌⩎⍐", a_))
					{
						num = 0;
						continue;
					}
					goto IL_F9;
				case 2:
					goto IL_F7;
				case 4:
					goto IL_3F;
				case 5:
					if (A_1 == null)
					{
						num = 2;
						continue;
					}
					num = 1;
					continue;
				}
				if (A_0 == null)
				{
					num = 4;
				}
				else
				{
					num = 5;
				}
			}
			IL_3F:
			goto IL_A9;
			IL_6F:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_A9:
				throw new ArgumentNullException(RecordTableEnumerator.b("㕆ⱈ⩊⥌⩎⍐", a_));
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				throw new XmlException(RecordTableEnumerator.b("ቆ❈⹊㕌㽎㑐げ⅔㉖㵘筚╜㉞ൠ䍢ᅤ٦๨䕪", a_));
			}
			IL_F7:
			throw new ArgumentNullException(RecordTableEnumerator.b("㑆ⱈ㥊⑌⩎≐", a_));
			IL_F9:
			IChartSerieDataFormat format = A_1.Format;
			this.ᜁ(A_0, format, A_2);
		}

		// Token: 0x06001F52 RID: 8018 RVA: 0x001170A0 File Offset: 0x001160A0
		private void ᜁ(XmlReader A_0, IChartSerieDataFormat A_1, spr\u2306 A_2)
		{
			int a_ = 17;
			switch (0)
			{
			default:
			{
				bool a_2;
				for (;;)
				{
					a_2 = true;
					int num = 13;
					for (;;)
					{
						string localName;
						switch (num)
						{
						case 0:
							goto IL_19D;
						case 1:
						{
							if (!(localName == RecordTableEnumerator.b("㑆え♊⽌⁎㵐", a_)))
							{
								num = 2;
								continue;
							}
							string value = spr\u1AA0.ᜄ(A_0);
							XLSXChartMarkerType markerStyle = (XLSXChartMarkerType)Enum.Parse(typeof(XLSXChartMarkerType), value, false);
							A_1.MarkerStyle = (ChartMarkerType)markerStyle;
							num = 20;
							continue;
						}
						case 2:
							num = 10;
							continue;
						case 3:
							goto IL_167;
						case 4:
							goto IL_167;
						case 5:
							if (true)
							{
							}
							num = 8;
							continue;
						case 6:
							goto IL_167;
						case 7:
							goto IL_1F7;
						case 8:
							if ((localName = A_0.LocalName) != null)
							{
								num = 9;
								continue;
							}
							goto IL_1F7;
						case 9:
							num = 1;
							continue;
						case 10:
							if (!(localName == RecordTableEnumerator.b("㑆⁈ㅊ⡌", a_)))
							{
								num = 16;
								continue;
							}
							A_1.MarkerSize = spr\u1AA0.ᜂ(A_0);
							num = 15;
							continue;
						case 11:
							if (A_0.NodeType == XmlNodeType.Element)
							{
								num = 5;
								continue;
							}
							A_0.Skip();
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_19D;
							default:
								if (false)
								{
								}
								num = 4;
								continue;
							}
							break;
						case 12:
							num = 7;
							continue;
						case 13:
							if (!A_0.IsEmptyElement)
							{
								num = 17;
								continue;
							}
							goto IL_29E;
						case 14:
							if (A_0.NodeType == XmlNodeType.EndElement)
							{
								num = 18;
								continue;
							}
							num = 11;
							continue;
						case 15:
							goto IL_167;
						case 16:
							num = 0;
							continue;
						case 17:
							A_0.Read();
							num = 3;
							continue;
						case 18:
							goto IL_18C;
						case 19:
							goto IL_167;
						case 20:
							goto IL_167;
						}
						break;
						IL_19D:
						if (!(localName == RecordTableEnumerator.b("㑆㥈ᭊ㽌", a_)))
						{
							num = 12;
							continue;
						}
						a_2 = false;
						this.ᜀ(A_0, A_1, A_2);
						num = 19;
						continue;
						IL_167:
						num = 14;
						continue;
						IL_1F7:
						A_0.Skip();
						num = 6;
					}
				}
				IL_18C:
				IL_29E:
				((XlsChartSerieDataFormat)A_1).MarkerFormat.ᜁ(a_2);
				A_0.Read();
				return;
			}
			}
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x00117364 File Offset: 0x00116364
		private void ᜀ(XmlReader A_0, IChartSerieDataFormat A_1, spr\u2306 A_2)
		{
			int a_ = 0;
			switch (0)
			{
			default:
				for (;;)
				{
					string localName = A_0.LocalName;
					int num = 7;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_22F;
						case 1:
							goto IL_22F;
						case 2:
							goto IL_22F;
						case 3:
							num = 6;
							continue;
						case 4:
							num = 17;
							continue;
						case 5:
						{
							string localName2;
							if (!(localName2 == RecordTableEnumerator.b("儵䨷嬹堻砽⤿⹁⡃", a_)))
							{
								num = 3;
								continue;
							}
							GradientStops gradientStops = spr\u1AA0.ᜂ(A_0, A_2);
							XlsChartSerieDataFormat xlsChartSerieDataFormat;
							sprᣐ sprᣐ = xlsChartSerieDataFormat.MarkerFormat;
							sprᣐ.ᜁ(false);
							sprᣐ.ᜀ(false);
							sprᣐ.ᜂ(false);
							xlsChartSerieDataFormat.MarkerBackColorObject.ᜀ(gradientStops[0].OColor, true);
							xlsChartSerieDataFormat.MarkerGradient = gradientStops;
							goto IL_36E;
						}
						case 6:
						{
							string localName2;
							if (!(localName2 == RecordTableEnumerator.b("娵嘷", a_)))
							{
								num = 20;
								continue;
							}
							XlsChartSerieDataFormat xlsChartSerieDataFormat;
							xlsChartSerieDataFormat.MarkerFormat.ᜂ(!this.ᜀ(A_0, xlsChartSerieDataFormat.MarkerForeColorObject, A_2, xlsChartSerieDataFormat));
							num = 0;
							continue;
						}
						case 7:
							if (true)
							{
							}
							if (!A_0.IsEmptyElement)
							{
								num = 15;
								continue;
							}
							goto IL_37F;
						case 8:
						{
							string localName2;
							if (!(localName2 == RecordTableEnumerator.b("䔵圷嘹唻娽ؿ⭁⡃⩅", a_)))
							{
								num = 21;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_36E;
							default:
							{
								if (false)
								{
								}
								XlsChartSerieDataFormat xlsChartSerieDataFormat;
								spr\u1AA0.ᜀ(A_0, A_2, xlsChartSerieDataFormat.MarkerBackgroundColor);
								num = 23;
								continue;
							}
							}
							break;
						}
						case 9:
							goto IL_22F;
						case 10:
							if (A_0.NodeType == XmlNodeType.Element)
							{
								num = 4;
								continue;
							}
							A_0.Skip();
							num = 16;
							continue;
						case 11:
							num = 8;
							continue;
						case 12:
							goto IL_259;
						case 13:
							num = 22;
							continue;
						case 14:
						{
							string localName2;
							if (!(localName2 == RecordTableEnumerator.b("堵圷簹唻刽ⰿ", a_)))
							{
								num = 24;
								continue;
							}
							XlsChartSerieDataFormat xlsChartSerieDataFormat;
							xlsChartSerieDataFormat.MarkerFormat.ᜀ(true);
							A_0.Read();
							num = 2;
							continue;
						}
						case 15:
						{
							A_0.Read();
							XlsChartSerieDataFormat xlsChartSerieDataFormat = A_1 as XlsChartSerieDataFormat;
							num = 1;
							continue;
						}
						case 16:
							goto IL_22F;
						case 17:
						{
							string localName2;
							if ((localName2 = A_0.LocalName) != null)
							{
								num = 11;
								continue;
							}
							goto IL_259;
						}
						case 18:
							if (A_0.NodeType != XmlNodeType.EndElement)
							{
								num = 13;
								continue;
							}
							goto IL_37F;
						case 19:
							goto IL_22F;
						case 20:
							num = 14;
							continue;
						case 21:
							num = 5;
							continue;
						case 22:
							if (!(A_0.LocalName != localName))
							{
								num = 25;
								continue;
							}
							num = 10;
							continue;
						case 23:
							goto IL_22F;
						case 24:
							num = 12;
							continue;
						case 25:
							goto IL_1EE;
						}
						break;
						IL_22F:
						num = 18;
						continue;
						IL_259:
						A_0.Skip();
						num = 19;
						continue;
						IL_36E:
						num = 9;
					}
				}
				IL_1EE:
				IL_37F:
				A_0.Read();
				return;
			}
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x001176F8 File Offset: 0x001166F8
		private bool ᜀ(XmlReader A_0, OColor A_1, spr\u2306 A_2, XlsChartSerieDataFormat A_3)
		{
			int a_ = 9;
			switch (0)
			{
			default:
			{
				bool result;
				for (;;)
				{
					result = false;
					int num = 100000;
					Stream stream = ShapeParser.ReadNodeAsStream(A_0);
					stream.Position = 0L;
					A_0 = UtilityMethods.ᜀ(stream);
					A_3.MarkerLineStream = stream;
					int num2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_16B:
						A_0.Skip();
						num2 = 12;
						break;
					default:
						if (false)
						{
						}
						num2 = 13;
						break;
					}
					for (;;)
					{
						switch (num2)
						{
						case 0:
							num2 = 4;
							continue;
						case 1:
							A_0.Read();
							num2 = 3;
							continue;
						case 2:
							goto IL_151;
						case 3:
							goto IL_12F;
						case 4:
						{
							string localName;
							if ((localName = A_0.LocalName) != null)
							{
								num2 = 6;
								continue;
							}
							goto IL_C3;
						}
						case 5:
							spr\u1AA0.ᜀ(A_0, A_2, A_1, out num);
							A_3.MarkerTransparencyValue = (double)((float)num / 100000f);
							result = true;
							num2 = 7;
							continue;
						case 6:
							num2 = 11;
							continue;
						case 7:
							goto IL_12F;
						case 8:
							if (A_0.NodeType == XmlNodeType.Element)
							{
								num2 = 0;
								continue;
							}
							goto IL_16B;
						case 9:
							goto IL_12F;
						case 10:
							if (A_0.NodeType == XmlNodeType.EndElement)
							{
								num2 = 2;
								continue;
							}
							num2 = 8;
							continue;
						case 11:
						{
							string localName;
							if (localName == RecordTableEnumerator.b("䰾⹀⽂ⱄ⍆཈≊⅌⍎", a_))
							{
								num2 = 5;
								continue;
							}
							goto IL_C3;
						}
						case 12:
							goto IL_12F;
						case 13:
							if (!A_0.IsEmptyElement)
							{
								num2 = 1;
								continue;
							}
							goto IL_1E6;
						}
						break;
						IL_C3:
						A_0.Skip();
						if (true)
						{
						}
						num2 = 9;
						continue;
						IL_12F:
						num2 = 10;
					}
				}
				IL_151:
				IL_1E6:
				A_0.Read();
				return result;
			}
			}
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x001178F4 File Offset: 0x001168F4
		private void ᜁ(XmlReader A_0, XlsChartSerie A_1, RelationsCollection A_2)
		{
			int a_ = 14;
			switch (0)
			{
			default:
			{
				int num = 6;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 3;
						continue;
					case 1:
						goto IL_212;
					case 2:
						num = 15;
						continue;
					case 3:
						goto IL_133;
					case 4:
						goto IL_212;
					case 5:
						num = 21;
						continue;
					case 6:
						if (true)
						{
						}
						break;
					case 7:
						if (A_0.NodeType == XmlNodeType.EndElement)
						{
							num = 17;
							continue;
						}
						num = 13;
						continue;
					case 8:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num = 11;
							continue;
						}
						goto IL_133;
					}
					case 9:
						if (A_1 == null)
						{
							num = 10;
							continue;
						}
						num = 16;
						continue;
					case 10:
						goto IL_181;
					case 11:
						num = 23;
						continue;
					case 12:
						goto IL_A4;
					case 13:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 19;
							continue;
						}
						A_0.Skip();
						num = 18;
						continue;
					case 14:
						goto IL_12E;
					case 15:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("ㅃ㙅ੇ⭉㹋㵍", a_)))
						{
							num = 5;
							continue;
						}
						XlsChartFormat xlsChartFormat;
						IChartDropBar firstDropBar = xlsChartFormat.FirstDropBar;
						sprវ a_2;
						this.ᜀ(A_0, firstDropBar, a_2, A_2);
						goto IL_2FB;
					}
					case 16:
					{
						if (A_0.LocalName != RecordTableEnumerator.b("ㅃ㙅ే╉㭋⁍቏㍑♓╕", a_))
						{
							num = 14;
							continue;
						}
						XlsChartFormat xlsChartFormat = (XlsChartFormat)A_1.Format.Options;
						sprវ a_2 = A_1.ParentBook.DataHolder;
						A_0.Read();
						num = 24;
						continue;
					}
					case 17:
						goto IL_237;
					case 18:
						goto IL_212;
					case 19:
						num = 8;
						continue;
					case 20:
						goto IL_212;
					case 21:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("⁃⥅㽇⑉๋⽍≏⅑", a_)))
						{
							num = 0;
							continue;
						}
						XlsChartFormat xlsChartFormat;
						IChartDropBar secondDropBar = xlsChartFormat.SecondDropBar;
						sprវ a_2;
						this.ᜀ(A_0, secondDropBar, a_2, A_2);
						num = 4;
						continue;
					}
					case 22:
						goto IL_212;
					case 23:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("⍃❅㡇ᵉ╋⩍⑏㩑", a_)))
						{
							num = 2;
							continue;
						}
						spr\u1AA0.ᜂ(A_0);
						num = 22;
						continue;
					}
					case 24:
						goto IL_212;
					}
					if (A_0 == null)
					{
						num = 12;
						continue;
					}
					num = 9;
					continue;
					IL_133:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_2FB:
						num = 1;
						continue;
					default:
						if (false)
						{
						}
						A_0.Skip();
						num = 20;
						continue;
					}
					IL_212:
					num = 7;
				}
				IL_A4:
				throw new ArgumentNullException(RecordTableEnumerator.b("㙃⍅⥇⹉⥋㱍", a_));
				IL_12E:
				throw new XmlException(RecordTableEnumerator.b("ᅃ⡅ⵇ㉉㱋⭍㍏♑ㅓ㉕硗≙ㅛ㉝䁟ᙡգť䙧", a_));
				IL_181:
				throw new ArgumentNullException(RecordTableEnumerator.b("㝃⍅㩇⍉⥋㵍", a_));
				IL_237:
				A_0.Read();
				return;
			}
			}
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x00117C58 File Offset: 0x00116C58
		private void ᜀ(XmlReader A_0, IChartDropBar A_1, sprវ A_2, RelationsCollection A_3)
		{
			int a_ = 1;
			int num = 14;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						if (true)
						{
						}
						num = 10;
						continue;
					}
					goto IL_104;
				case 1:
					goto IL_FF;
				case 2:
					goto IL_17D;
				case 3:
					goto IL_B5;
				case 4:
					if (A_1 == null)
					{
						num = 1;
						continue;
					}
					num = 12;
					continue;
				case 5:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 7;
						continue;
					}
					num = 0;
					continue;
				case 6:
					A_0.Read();
					num = 2;
					continue;
				case 7:
					goto IL_1A0;
				case 8:
					goto IL_17D;
				case 9:
				{
					spr\u1A7B a_2 = new spr\u1A7B(A_1.LineProperties, A_1.Interior as XlsChartInterior, A_1.Fill as spr\u1C26, A_1.Shadow, A_1.Format3D);
					spr\u1AA0.ᜀ(A_0, a_2, A_2, A_3);
					num = 3;
					continue;
				}
				case 10:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B5;
					default:
						if (false)
						{
						}
						num = 11;
						continue;
					}
					break;
				case 11:
					if (A_0.LocalName == RecordTableEnumerator.b("䐶䤸欺似", a_))
					{
						num = 9;
						continue;
					}
					goto IL_104;
				case 12:
					if (!A_0.IsEmptyElement)
					{
						num = 6;
						continue;
					}
					goto IL_1EA;
				case 13:
					goto IL_63;
				}
				if (A_0 == null)
				{
					num = 13;
					continue;
				}
				num = 4;
				continue;
				IL_104:
				A_0.Skip();
				num = 8;
				continue;
				IL_17D:
				num = 5;
				continue;
				IL_B5:
				goto IL_17D;
			}
			IL_63:
			throw new ArgumentNullException(RecordTableEnumerator.b("䔶尸娺夼娾㍀", a_));
			IL_FF:
			throw new ArgumentNullException(RecordTableEnumerator.b("匶䬸吺䴼紾⁀ㅂ", a_));
			IL_1A0:
			IL_1EA:
			A_0.Read();
		}

		// Token: 0x06001F57 RID: 8023 RVA: 0x00117E58 File Offset: 0x00116E58
		private void ᜀ(XmlReader A_0, XlsChart A_1)
		{
			int a_ = 19;
			switch (0)
			{
			default:
			{
				int num = 24;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_23D;
					case 1:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("㩈⍊≌㡎ݐ㙒❔⍖᭘㑚⽜㭞Ѡᅢ", a_)))
						{
							num = 11;
							continue;
						}
						IChartDataTable dataTable;
						dataTable.HasVertBorder = spr\u1AA0.ᜃ(A_0);
						num = 17;
						continue;
					}
					case 2:
						if (!A_0.IsEmptyElement)
						{
							num = 22;
							continue;
						}
						goto IL_447;
					case 3:
						if (A_0.NodeType == XmlNodeType.EndElement)
						{
							num = 18;
							continue;
						}
						num = 13;
						continue;
					case 4:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("㩈⍊≌㡎ᩐ㙒ⱔ⑖", a_)))
						{
							num = 30;
							continue;
						}
						IChartDataTable dataTable;
						dataTable.ShowSeriesKeys = spr\u1AA0.ᜃ(A_0);
						num = 12;
						continue;
					}
					case 5:
						goto IL_425;
					case 6:
						goto IL_23D;
					case 7:
					{
						if (true)
						{
						}
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num = 25;
							continue;
						}
						goto IL_229;
					}
					case 8:
						goto IL_C2;
					case 9:
						num = 7;
						continue;
					case 10:
						goto IL_23D;
					case 11:
						num = 20;
						continue;
					case 12:
						goto IL_23D;
					case 13:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 9;
							continue;
						}
						A_0.Skip();
						num = 0;
						continue;
					case 14:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("㵈㍊ᵌ㵎", a_)))
						{
							num = 31;
							continue;
						}
						IChartDataTable dataTable;
						sprᮟ textArea = dataTable.TextArea;
						spr\u2306 a_2;
						this.ᜀ(A_0, textArea, a_2);
						num = 29;
						continue;
					}
					case 15:
						goto IL_229;
					case 16:
						num = 4;
						continue;
					case 17:
						goto IL_23D;
					case 18:
						goto IL_26C;
					case 19:
						num = 1;
						continue;
					case 20:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("㩈⍊≌㡎Ṑ♒⅔㭖じ㕚㡜", a_)))
						{
							num = 16;
							continue;
						}
						IChartDataTable dataTable;
						dataTable.HasBorders = spr\u1AA0.ᜃ(A_0);
						num = 32;
						continue;
					}
					case 21:
					{
						string localName;
						if (!(localName == RecordTableEnumerator.b("㩈⍊≌㡎ᥐ㱒❔ⵖ᭘㑚⽜㭞Ѡᅢ", a_)))
						{
							num = 19;
							continue;
						}
						IChartDataTable dataTable;
						dataTable.HasHorzBorder = spr\u1AA0.ᜃ(A_0);
						num = 27;
						continue;
					}
					case 22:
					{
						A_0.Read();
						IChartDataTable dataTable = A_1.DataTable;
						num = 10;
						continue;
					}
					case 23:
						if (A_0.LocalName != RecordTableEnumerator.b("ⵈὊⱌⵎ㵐㙒", a_))
						{
							num = 26;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_229;
						default:
						{
							if (false)
							{
							}
							spr\u2306 a_2 = A_1.ParentWorkbook.DataHolder.\u1718();
							A_1.HasDataTable = true;
							num = 2;
							continue;
						}
						}
						break;
					case 25:
						num = 21;
						continue;
					case 26:
						goto IL_2ED;
					case 27:
						goto IL_23D;
					case 28:
						if (A_1 == null)
						{
							num = 5;
							continue;
						}
						num = 23;
						continue;
					case 29:
						goto IL_23D;
					case 30:
						num = 14;
						continue;
					case 31:
						num = 15;
						continue;
					case 32:
						goto IL_23D;
					}
					if (A_0 == null)
					{
						num = 8;
						continue;
					}
					num = 28;
					continue;
					IL_229:
					A_0.Skip();
					num = 6;
					continue;
					IL_23D:
					num = 3;
				}
				IL_C2:
				throw new ArgumentNullException(RecordTableEnumerator.b("㭈⹊ⱌ⭎㑐⅒", a_));
				IL_26C:
				goto IL_447;
				IL_2ED:
				throw new XmlException(RecordTableEnumerator.b("᱈╊⡌㝎⅐㙒㙔⍖㱘㽚絜❞ౠར䕤፦ࡨ౪䍬", a_));
				IL_425:
				throw new ArgumentNullException(RecordTableEnumerator.b("⩈⍊ⱌ㵎═", a_));
				IL_447:
				A_0.Read();
				return;
			}
			}
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x001182B4 File Offset: 0x001172B4
		private void ᜀ(XmlReader A_0, XlsChartSerie A_1, RelationsCollection A_2)
		{
			int a_ = 15;
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
					num = 4;
					continue;
				case 2:
					goto IL_F7;
				case 3:
					goto IL_6F;
				case 4:
					if (A_0.LocalName != RecordTableEnumerator.b("㙄㝆᥈㥊", a_))
					{
						num = 3;
						continue;
					}
					goto IL_F9;
				case 5:
					goto IL_3F;
				}
				if (A_0 == null)
				{
					num = 5;
				}
				else
				{
					num = 0;
				}
			}
			IL_3F:
			goto IL_A9;
			IL_6F:
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_A9:
				throw new ArgumentNullException(RecordTableEnumerator.b("㝄≆⡈⽊⡌㵎", a_));
			default:
				if (false)
				{
				}
				throw new XmlException(RecordTableEnumerator.b("၄⥆ⱈ㍊㵌⩎㉐❒ご㍖祘⍚ぜ㍞䅠ᝢѤf", a_));
			}
			IL_F7:
			throw new ArgumentNullException(RecordTableEnumerator.b("㙄≆㭈≊⡌㱎", a_));
			IL_F9:
			XlsChartSerieDataFormat a_2 = (XlsChartSerieDataFormat)A_1.Format;
			sprវ a_3 = A_1.ParentChart.DataHolder.ᜋ();
			spr\u230D a_4 = new spr\u230D(a_2);
			spr\u1AA0.ᜀ(A_0, a_4, a_3, A_2);
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x001183E8 File Offset: 0x001173E8
		private void ᜀ(XmlReader A_0, sprᮟ A_1, spr\u2306 A_2)
		{
			int a_ = 19;
			int num = 14;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 18;
					continue;
				case 1:
					if (A_0.LocalName == RecordTableEnumerator.b("ⵈ⹊⭌ᵎŐ⅒", a_))
					{
						num = 10;
						continue;
					}
					goto IL_299;
				case 2:
					goto IL_77;
				case 3:
					goto IL_122;
				case 4:
					if (!(A_0.LocalName != RecordTableEnumerator.b("ⵈ⹊⭌ᵎŐ⅒", a_)))
					{
						num = 13;
						continue;
					}
					A_0.Read();
					num = 8;
					continue;
				case 5:
					goto IL_104;
				case 6:
					goto IL_18B;
				case 7:
					if (A_1 == null)
					{
						num = 3;
						continue;
					}
					num = 16;
					continue;
				case 8:
					goto IL_AF;
				case 9:
					goto IL_AF;
				case 10:
					spr\u1AA0.ᜀ(A_0, A_1, A_2, null);
					num = 5;
					continue;
				case 11:
					if (!(A_0.LocalName != RecordTableEnumerator.b("㵈㍊ᵌ㵎", a_)))
					{
						num = 6;
						continue;
					}
					A_0.Read();
					num = 12;
					continue;
				case 12:
					goto IL_15A;
				case 13:
					goto IL_124;
				case 15:
					if (A_0.NodeType != XmlNodeType.EndElement)
					{
						num = 0;
						continue;
					}
					goto IL_124;
				case 16:
					if (A_0.LocalName != RecordTableEnumerator.b("㵈㍊ᵌ㵎", a_))
					{
						num = 19;
						continue;
					}
					A_0.Read();
					A_1.ᜀ(ChartParagraphType.Default);
					num = 9;
					continue;
				case 17:
					num = 4;
					continue;
				case 18:
					if (A_0.LocalName != RecordTableEnumerator.b("㵈㍊ᵌ㵎", a_))
					{
						num = 17;
						continue;
					}
					goto IL_124;
				case 19:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_104;
					default:
						goto IL_24D;
					}
					break;
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				num = 7;
				continue;
				IL_AF:
				num = 15;
				continue;
				IL_124:
				num = 1;
				continue;
				IL_15A:
				num = 11;
				continue;
				IL_104:
				goto IL_15A;
			}
			IL_77:
			throw new ArgumentNullException(RecordTableEnumerator.b("㭈⹊ⱌ⭎㑐⅒", a_));
			IL_122:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㵈⹊㕌㭎ᝐ㱒❔㩖㡘⽚⥜㙞འѢ", a_));
			IL_18B:
			goto IL_299;
			IL_24D:
			if (false)
			{
			}
			throw new XmlException(RecordTableEnumerator.b("᱈╊⡌㝎⅐㙒㙔⍖㱘㽚絜❞ౠར䕤፦ࡨ౪", a_));
			IL_299:
			A_0.Read();
		}

		// Token: 0x06001F5A RID: 8026 RVA: 0x00118698 File Offset: 0x00117698
		private object[] ᜁ(XmlReader A_0)
		{
			int a_ = 0;
			int num = 9;
			List<object> list;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 8;
						continue;
					}
					goto IL_13A;
				case 1:
					this.ᜀ(A_0, list);
					num = 4;
					continue;
				case 2:
					goto IL_60;
				case 3:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 10;
						continue;
					}
					A_0.Skip();
					num = 12;
					continue;
				case 4:
					goto IL_13A;
				case 5:
					goto IL_13A;
				case 6:
					goto IL_15A;
				case 7:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 11;
						continue;
					}
					goto IL_65;
				}
				case 8:
					goto IL_F9;
				case 10:
					num = 7;
					continue;
				case 11:
					num = 14;
					continue;
				case 12:
					goto IL_13A;
				case 13:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 6;
						continue;
					}
					num = 3;
					continue;
				case 14:
				{
					string localName;
					if (localName == RecordTableEnumerator.b("䘵䰷", a_))
					{
						num = 1;
						continue;
					}
					goto IL_65;
				}
				}
				IL_55:
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				A_0.Read();
				list = new List<object>();
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
				goto IL_55;
				IL_65:
				A_0.Skip();
				num = 5;
				continue;
				IL_13A:
				num = 13;
			}
			IL_60:
			throw new ArgumentNullException(RecordTableEnumerator.b("䐵崷嬹堻嬽㈿", a_));
			IL_F9:
			if (true)
			{
			}
			return list.ToArray();
			IL_15A:
			A_0.Read();
			return list.ToArray();
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x00118868 File Offset: 0x00117868
		private object ᜀ(XmlReader A_0)
		{
			int a_ = 3;
			switch (0)
			{
			default:
			{
				int num = 1;
				for (;;)
				{
					string text;
					NumberStyles style;
					switch (num)
					{
					case 0:
					{
						double num2;
						if (double.TryParse(text, style, CultureInfo.InvariantCulture, out num2))
						{
							goto IL_B2;
						}
						object result = text;
						num = 3;
						continue;
					}
					case 1:
						if (true)
						{
						}
						break;
					case 2:
					{
						object result;
						return result;
					}
					case 3:
					{
						object result;
						return result;
					}
					case 4:
						goto IL_55;
					case 5:
					{
						double num2;
						object result = num2;
						num = 2;
						continue;
					}
					}
					if (A_0 == null)
					{
						num = 4;
						continue;
					}
					text = A_0.ReadElementContentAsString();
					style = NumberStyles.Number;
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
					IL_B2:
					num = 5;
				}
				IL_55:
				throw new ArgumentNullException(RecordTableEnumerator.b("䬸帺尼嬾⑀ㅂ", a_));
			}
			}
		}

		// Token: 0x06001F5C RID: 8028 RVA: 0x0011895C File Offset: 0x0011795C
		private void ᜀ(XmlReader A_0, List<object> A_1)
		{
			int a_ = 1;
			int num = 22;
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
					num = 1;
					continue;
				case 1:
					if (A_0.LocalName != RecordTableEnumerator.b("䜶䴸", a_))
					{
						num = 10;
						continue;
					}
					num = 8;
					continue;
				case 2:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 7;
						continue;
					}
					goto IL_110;
				}
				case 3:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 5;
						continue;
					}
					A_0.Skip();
					num = 19;
					continue;
				case 4:
					goto IL_17F;
				case 5:
					num = 2;
					continue;
				case 6:
					goto IL_1A7;
				case 7:
					num = 17;
					continue;
				case 8:
					if (A_0.MoveToAttribute(RecordTableEnumerator.b("帶崸䌺", a_)))
					{
						num = 14;
						continue;
					}
					goto IL_1A7;
				case 9:
					goto IL_158;
				case 10:
					goto IL_10B;
				case 11:
					A_1.Add(this.ᜀ(A_0));
					num = 20;
					continue;
				case 12:
					goto IL_17F;
				case 13:
					A_0.Read();
					goto IL_BD;
				case 14:
					XmlConvert.ToInt32(A_0.Value);
					num = 6;
					continue;
				case 15:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 21;
						continue;
					}
					if (true)
					{
					}
					num = 3;
					continue;
				case 16:
					goto IL_83;
				case 17:
				{
					string localName;
					if (localName == RecordTableEnumerator.b("䄶", a_))
					{
						num = 11;
						continue;
					}
					goto IL_110;
				}
				case 18:
					if (!A_0.IsEmptyElement)
					{
						num = 13;
						continue;
					}
					goto IL_2A7;
				case 19:
					goto IL_17F;
				case 20:
					goto IL_17F;
				case 21:
					goto IL_1A2;
				}
				if (A_0 == null)
				{
					num = 16;
					continue;
				}
				num = 0;
				continue;
				IL_BD:
				num = 12;
				continue;
				IL_110:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_BD;
				default:
					if (false)
					{
					}
					A_0.Skip();
					num = 4;
					continue;
				}
				IL_17F:
				num = 15;
				continue;
				IL_1A7:
				num = 18;
			}
			IL_83:
			throw new ArgumentNullException(RecordTableEnumerator.b("䔶尸娺夼娾㍀", a_));
			IL_10B:
			throw new XmlException();
			IL_158:
			throw new ArgumentNullException(RecordTableEnumerator.b("嬶倸䠺䤼", a_));
			IL_1A2:
			IL_2A7:
			A_0.Read();
		}

		// Token: 0x040010E9 RID: 4329
		private string \u25D8\u0082\u00A5\u008C;

		// Token: 0x040010EA RID: 4330
		internal const float ᜀ = 18f;

		// Token: 0x040010EB RID: 4331
		private int[] \u25D9\u00A6\u0088\u009B;

		// Token: 0x040010EC RID: 4332
		private long \u25D9\u00AD\u008C\u0092;

		// Token: 0x040010ED RID: 4333
		private XlsWorkbook ᜁ;
	}
}
