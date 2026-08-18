using System;
using System.Collections.Generic;
using System.Xml;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Interfaces;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Charts;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.XmlReaders;
using Spire.Xls.Core.Spreadsheet.XmlSerialization;

// Token: 0x02000584 RID: 1412
internal class sprῐ
{
	// Token: 0x06005567 RID: 21863 RVA: 0x00367EE4 File Offset: 0x00366EE4
	internal sprῐ()
	{
		int a_ = 16;
		base..ctor();
		if (sprῐ.ᜂ.Count == 0)
		{
			sprῐ.ᜂ.Add(RecordTableEnumerator.b("⹅ⅇⵉ⑋", a_), TickLabelPositionType.TickLabelPositionHigh);
			sprῐ.ᜂ.Add(RecordTableEnumerator.b("⩅❇㵉", a_), TickLabelPositionType.TickLabelPositionLow);
			sprῐ.ᜂ.Add(RecordTableEnumerator.b("⡅ⵇ㉉㡋ᩍ㽏", a_), TickLabelPositionType.TickLabelPositionNextToAxis);
			sprῐ.ᜂ.Add(RecordTableEnumerator.b("⡅❇⑉⥋", a_), TickLabelPositionType.TickLabelPositionNone);
			sprῐ.ᜃ.Add(RecordTableEnumerator.b("⡅❇⑉⥋", a_), TickMarkType.TickMarkNone);
			sprῐ.ᜃ.Add(RecordTableEnumerator.b("⽅♇", a_), TickMarkType.TickMarkInside);
			sprῐ.ᜃ.Add(RecordTableEnumerator.b("⥅㵇㹉", a_), TickMarkType.TickMarkOutside);
			sprῐ.ᜃ.Add(RecordTableEnumerator.b("╅㩇╉㽋㵍", a_), TickMarkType.TickMarkCross);
		}
	}

	// Token: 0x06005568 RID: 21864 RVA: 0x00367FDC File Offset: 0x00366FDC
	internal sprῐ(XlsWorkbook A_0) : this()
	{
		this.ᜄ = A_0;
		spr\u1AA0.ᜀ(A_0);
	}

	// Token: 0x06005569 RID: 21865 RVA: 0x00367FFC File Offset: 0x00366FFC
	public void ᜁ(XmlReader A_0, XlsChartCategoryAxis A_1, RelationsCollection A_2, ExcelChartType A_3, spr\u2306 A_4)
	{
		int a_ = 8;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_3F;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_D1;
				default:
					goto IL_B7;
				}
				break;
			case 3:
				if (A_0.LocalName != RecordTableEnumerator.b("娽ℿ㙁⅃݅ぇ", a_))
				{
					num = 2;
					continue;
				}
				goto IL_EF;
			case 4:
				goto IL_ED;
			case 5:
				if (A_1 == null)
				{
					num = 4;
					continue;
				}
				num = 3;
				continue;
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			IL_D1:
			num = 5;
		}
		IL_3F:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䰽┿⍁⁃⍅㩇", a_));
		IL_B7:
		if (false)
		{
		}
		throw new XmlException(RecordTableEnumerator.b("欽⸿❁㱃㙅ⵇ⥉㡋⭍㑏牑ⱓ㭕㑗穙⡛㽝ݟ䱡", a_));
		IL_ED:
		throw new ArgumentNullException(RecordTableEnumerator.b("弽㠿⭁㝃", a_));
		IL_EF:
		A_0.Read();
		A_1.CategoryType = CategoryType.Time;
		this.ᜀ(A_0, A_1, A_2, A_3, A_4, new sprῐ.ᜀ(this.ᜂ));
		A_0.Read();
	}

	// Token: 0x0600556A RID: 21866 RVA: 0x00368130 File Offset: 0x00367130
	public void ᜀ(XmlReader A_0, XlsChartCategoryAxis A_1, RelationsCollection A_2, ExcelChartType A_3, spr\u2306 A_4)
	{
		int a_ = 13;
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
					goto IL_D1;
				default:
					goto IL_B7;
				}
				break;
			case 2:
				goto IL_3F;
			case 3:
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
			case 4:
				if (A_0.LocalName != RecordTableEnumerator.b("⁂⑄㍆ࡈ㍊", a_))
				{
					num = 1;
					continue;
				}
				goto IL_EF;
			case 5:
				goto IL_ED;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			IL_D1:
			num = 3;
		}
		IL_3F:
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄♆ⵈ⹊㽌", a_));
		IL_B7:
		if (false)
		{
		}
		throw new XmlException(RecordTableEnumerator.b("ㅂ⁄♆ⵈ⹊㽌", a_));
		IL_ED:
		throw new ArgumentNullException(RecordTableEnumerator.b("≂㵄⹆㩈", a_));
		IL_EF:
		A_0.Read();
		A_1.IsAutoMajor = true;
		A_1.IsAutoMinor = true;
		A_1.CategoryType = CategoryType.Category;
		this.ᜀ(A_0, A_1, A_2, A_3, A_4, new sprῐ.ᜀ(this.ᜃ));
		A_0.Read();
	}

	// Token: 0x0600556B RID: 21867 RVA: 0x00368274 File Offset: 0x00367274
	public void ᜀ(XmlReader A_0, XlsChartValueAxis A_1, RelationsCollection A_2, ExcelChartType A_3, spr\u2306 A_4)
	{
		int a_ = 14;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_92;
			case 2:
				if (true)
				{
				}
				if (A_1 == null)
				{
					goto IL_E9;
				}
				num = 3;
				continue;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_E9;
				default:
					if (false)
					{
					}
					if (A_0.LocalName != RecordTableEnumerator.b("㉃❅⑇୉㑋", a_))
					{
						num = 0;
						continue;
					}
					goto IL_F6;
				}
				break;
			case 4:
				goto IL_3F;
			case 5:
				goto IL_F4;
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			num = 2;
			continue;
			IL_E9:
			num = 5;
		}
		IL_3F:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙃⍅⥇⹉⥋㱍", a_));
		IL_92:
		throw new XmlException(RecordTableEnumerator.b("ᅃ⡅ⵇ㉉㱋⭍㍏♑ㅓ㉕硗≙ㅛ㉝䁟ᙡգť䙧", a_));
		IL_F4:
		throw new ArgumentNullException(RecordTableEnumerator.b("㉃❅⑇㽉⥋ཌྷ⡏㭑❓", a_));
		IL_F6:
		A_0.Read();
		this.ᜀ(A_0, A_1, A_2, A_3, A_4, new sprῐ.ᜀ(this.ᜁ));
		A_0.Read();
	}

	// Token: 0x0600556C RID: 21868 RVA: 0x003683A0 File Offset: 0x003673A0
	public void ᜀ(XmlReader A_0, XlsChartSeriesAxis A_1, RelationsCollection A_2, ExcelChartType A_3, spr\u2306 A_4)
	{
		int a_ = 8;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				if (A_1 == null)
				{
					goto IL_E9;
				}
				num = 4;
				continue;
			case 1:
				goto IL_F4;
			case 2:
				goto IL_92;
			case 3:
				goto IL_3F;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_E9;
				default:
					if (false)
					{
					}
					if (A_0.LocalName != RecordTableEnumerator.b("䴽┿ぁՃ㹅", a_))
					{
						num = 2;
						continue;
					}
					goto IL_F6;
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
			IL_E9:
			num = 1;
		}
		IL_3F:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰽┿⍁⁃⍅㩇", a_));
		IL_92:
		throw new XmlException(RecordTableEnumerator.b("欽⸿❁㱃㙅ⵇ⥉㡋⭍㑏牑ⱓ㭕㑗穙⡛㽝ݟ䱡", a_));
		IL_F4:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴽┿ぁⵃ⍅㭇୉㑋❍⍏", a_));
		IL_F6:
		A_0.Read();
		A_1.AutoTickLabelSpacing = true;
		A_1.AutoTickMarkSpacing = true;
		this.ᜀ(A_0, A_1, A_2, A_3, A_4, new sprῐ.ᜀ(this.ᜀ));
		A_0.Read();
	}

	// Token: 0x0600556D RID: 21869 RVA: 0x003684D8 File Offset: 0x003674D8
	private void ᜀ(XmlReader A_0, XlsChartAxis A_1, RelationsCollection A_2, ExcelChartType A_3, spr\u2306 A_4, sprῐ.ᜀ A_5)
	{
		int a_ = 11;
		switch (0)
		{
		default:
		{
			int num = 11;
			for (;;)
			{
				IChartCategoryAxis chartCategoryAxis;
				int a_4;
				switch (num)
				{
				case 0:
					goto IL_2AE;
				case 1:
					goto IL_2AE;
				case 2:
					goto IL_2AE;
				case 3:
					num = 46;
					continue;
				case 4:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 32;
						continue;
					}
					goto IL_1E9;
				}
				case 5:
					num = 51;
					continue;
				case 6:
				{
					spr\u20A3 spr_u20A;
					if (spr_u20A != null)
					{
						num = 27;
						continue;
					}
					return;
				}
				case 7:
					goto IL_244;
				case 8:
					goto IL_32F;
				case 9:
					num = 12;
					continue;
				case 10:
					goto IL_2AE;
				case 12:
					goto IL_1E9;
				case 13:
					chartCategoryAxis = A_1.ParentXlsChart.SecondaryCategoryAxis;
					goto IL_7CB;
				case 14:
					num = 42;
					continue;
				case 15:
					goto IL_2AE;
				case 16:
				{
					if (A_1 == null)
					{
						num = 40;
						continue;
					}
					XlsChart xlsChart = A_1.ParentXlsChart;
					sprᡟ sprᡟ = xlsChart.DataHolder;
					sprវ a_2 = sprᡟ.ᜋ();
					RelationsCollection a_3 = xlsChart.Relations;
					bool flag = true;
					spr\u20A3 spr_u20A = null;
					A_1.Visible = true;
					a_4 = -1;
					bool? flag2 = null;
					A_1.Visible = true;
					A_1.Font.FontName = RecordTableEnumerator.b("ɀ≂⥄⹆⭈㥊⑌", a_);
					A_1.Font.Size = 10.0;
					num = 34;
					continue;
				}
				case 17:
					goto IL_2AE;
				case 18:
					goto IL_2AE;
				case 19:
				{
					bool? flag2;
					A_1.Deleted = !flag2.Value;
					num = 22;
					continue;
				}
				case 20:
					if (A_3 != ExcelChartType.ScatterMarkers)
					{
						num = 5;
						continue;
					}
					goto IL_8BC;
				case 21:
					if (A_0.NodeType != XmlNodeType.EndElement)
					{
						num = 3;
						continue;
					}
					goto IL_244;
				case 22:
					goto IL_898;
				case 23:
					goto IL_2AE;
				case 24:
					goto IL_132;
				case 25:
					goto IL_2FF;
				case 26:
				{
					int num2;
					switch (num2)
					{
					case 0:
						a_4 = spr\u1AA0.ᜂ(A_0);
						num = 23;
						continue;
					case 1:
					{
						spr\u20A3 spr_u20A = this.ᜀ(A_0);
						num = 52;
						continue;
					}
					case 2:
					{
						bool? flag2 = new bool?(!spr\u1AA0.ᜃ(A_0));
						num = 1;
						continue;
					}
					case 3:
					{
						string text = this.ᜀ(A_0, A_1);
						A_1.AxisPosition = new ChartAxisPos?((ChartAxisPos)Enum.Parse(typeof(ChartAxisPos), text, false));
						num = 20;
						continue;
					}
					case 4:
					{
						A_1.HasMajorGridLines = true;
						sprវ a_2;
						this.ᜀ(A_0, A_1.MajorGridLines, a_2, A_2);
						num = 53;
						continue;
					}
					case 5:
					{
						if (true)
						{
						}
						A_1.HasMinorGridLines = true;
						sprវ a_2;
						this.ᜀ(A_0, A_1.MinorGridLines, a_2, A_2);
						num = 17;
						continue;
					}
					case 6:
					{
						sprᮟ a_5 = A_1.TitleArea as sprᮟ;
						sprវ a_2;
						RelationsCollection a_3;
						spr\u1AA0.ᜀ(A_0, a_5, a_2, a_3);
						num = 59;
						continue;
					}
					case 7:
						this.ᜁ(A_0, A_1);
						num = 28;
						continue;
					case 8:
						A_1.MajorTickMark = this.ᜁ(A_0);
						num = 58;
						continue;
					case 9:
						A_1.MinorTickMark = this.ᜁ(A_0);
						num = 37;
						continue;
					case 10:
						this.ᜂ(A_0, A_1);
						num = 10;
						continue;
					case 11:
						this.ᜃ(A_0, A_1);
						num = 15;
						continue;
					case 12:
						this.ᜄ(A_0, A_1);
						num = 36;
						continue;
					case 13:
					{
						XlsChartValueAxis xlsChartValueAxis = spr\u2433.ᜀ(A_1) as XlsChartValueAxis;
						num = 43;
						continue;
					}
					case 14:
					{
						XlsChartInterior xlsChartInterior = A_1.FrameFormat.Interior as XlsChartInterior;
						xlsChartInterior.UseDefaultFormat = false;
						spr\u2436 a_6 = A_1.FrameFormat.Fill as spr\u2436;
						spr\u1772 a_7 = new spr\u1A7B(A_1.FrameFormat.Border as XlsChartBorder, xlsChartInterior, a_6, A_1.ShadowProperties as ChartShadow, A_1.FrameFormat.Format3D);
						sprវ a_2;
						spr\u1AA0.ᜀ(A_0, a_7, a_2, A_2);
						num = 44;
						continue;
					}
					case 15:
						A_1.ParagraphType = ChartParagraphType.Default;
						this.ᜁ(A_0, A_1, A_4);
						num = 18;
						continue;
					case 16:
						A_1.\u1714 = spr\u1AA0.ᜄ(A_0);
						num = 2;
						continue;
					default:
						num = 9;
						continue;
					}
					break;
				}
				case 27:
				{
					spr\u20A3 spr_u20A;
					spr_u20A.ᜀ(A_1 as sprᦳ);
					num = 54;
					continue;
				}
				case 28:
					goto IL_2AE;
				case 29:
					goto IL_2AE;
				case 30:
					num = 4;
					continue;
				case 31:
					goto IL_2AE;
				case 32:
					num = 39;
					continue;
				case 33:
				{
					XlsChartValueAxis xlsChartValueAxis;
					xlsChartValueAxis.CrossesAt = spr\u1AA0.ᜁ(A_0);
					num = 50;
					continue;
				}
				case 34:
					goto IL_2AE;
				case 35:
					num = 26;
					continue;
				case 36:
					goto IL_2AE;
				case 37:
					goto IL_2AE;
				case 38:
					goto IL_2AE;
				case 39:
					if (spr\u22D2.ឨ == null)
					{
						num = 60;
						continue;
					}
					goto IL_32F;
				case 40:
					goto IL_75C;
				case 41:
					goto IL_2F3;
				case 42:
				{
					string text;
					if (text == RecordTableEnumerator.b("㕀", a_))
					{
						num = 41;
						continue;
					}
					goto IL_2AE;
				}
				case 43:
				{
					XlsChartValueAxis xlsChartValueAxis;
					if (xlsChartValueAxis != null)
					{
						num = 33;
						continue;
					}
					(A_1 as XlsChartSeriesAxis).CrossesAt = spr\u1AA0.ᜂ(A_0);
					num = 38;
					continue;
				}
				case 44:
					goto IL_2AE;
				case 45:
				{
					string localName;
					int num2;
					if (spr\u22D2.ឨ.TryGetValue(localName, out num2))
					{
						num = 35;
						continue;
					}
					goto IL_1E9;
				}
				case 46:
				{
					bool flag;
					if (!flag)
					{
						num = 7;
						continue;
					}
					num = 57;
					continue;
				}
				case 47:
					goto IL_8BC;
				case 48:
				{
					string text;
					if (!(text == RecordTableEnumerator.b("⍀", a_)))
					{
						num = 14;
						continue;
					}
					goto IL_2F3;
				}
				case 49:
					num = 13;
					continue;
				case 50:
					goto IL_2AE;
				case 51:
					if (A_3 == ExcelChartType.Bubble)
					{
						num = 47;
						continue;
					}
					goto IL_2AE;
				case 52:
					goto IL_2AE;
				case 53:
					goto IL_2AE;
				case 54:
					goto IL_2EE;
				case 55:
					chartCategoryAxis = A_1.ParentXlsChart.PrimaryCategoryAxis;
					goto IL_7CB;
				case 56:
				{
					bool? flag2;
					if (flag2 != null)
					{
						num = 19;
						continue;
					}
					goto IL_898;
				}
				case 57:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 30;
						continue;
					}
					A_0.Skip();
					num = 31;
					continue;
				case 58:
					goto IL_2AE;
				case 59:
					goto IL_2AE;
				case 60:
					spr\u22D2.ឨ = new Dictionary<string, int>(17)
					{
						{
							RecordTableEnumerator.b("⁀㭂ౄ⍆", a_),
							0
						},
						{
							RecordTableEnumerator.b("㉀⁂⑄⭆⁈╊⩌", a_),
							1
						},
						{
							RecordTableEnumerator.b("╀♂⥄≆㵈⹊", a_),
							2
						},
						{
							RecordTableEnumerator.b("⁀㭂ᕄ⡆㩈", a_),
							3
						},
						{
							RecordTableEnumerator.b("ⱀ≂⽄⡆㭈ొ㽌♎㕐㽒㱔㥖㱘⡚", a_),
							4
						},
						{
							RecordTableEnumerator.b("ⱀ⩂⭄⡆㭈ొ㽌♎㕐㽒㱔㥖㱘⡚", a_),
							5
						},
						{
							RecordTableEnumerator.b("㕀⩂ㅄ⭆ⱈ", a_),
							6
						},
						{
							RecordTableEnumerator.b("⽀㙂⡄ņ⑈㽊", a_),
							7
						},
						{
							RecordTableEnumerator.b("ⱀ≂⽄⡆㭈Ὂ⑌ⱎ㩐Ṓ㑔╖㉘", a_),
							8
						},
						{
							RecordTableEnumerator.b("ⱀ⩂⭄⡆㭈Ὂ⑌ⱎ㩐Ṓ㑔╖㉘", a_),
							9
						},
						{
							RecordTableEnumerator.b("㕀⩂♄ⱆՈ⥊⅌὎㹐⁒", a_),
							10
						},
						{
							RecordTableEnumerator.b("≀ㅂ⩄㑆㩈੊㕌", a_),
							11
						},
						{
							RecordTableEnumerator.b("≀ㅂ⩄㑆㩈⹊㹌", a_),
							12
						},
						{
							RecordTableEnumerator.b("≀ㅂ⩄㑆㩈⹊㹌๎═", a_),
							13
						},
						{
							RecordTableEnumerator.b("㉀㍂ᕄ㕆", a_),
							14
						},
						{
							RecordTableEnumerator.b("㕀㭂ᕄ㕆", a_),
							15
						},
						{
							RecordTableEnumerator.b("ⵀ⅂⥄ن╈ⱊ⍌", a_),
							16
						}
					};
					num = 8;
					continue;
				}
				if (A_0 == null)
				{
					num = 24;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2FF;
				default:
					if (false)
					{
					}
					num = 16;
					continue;
				}
				IL_1E9:
				A_0.Skip();
				num = 29;
				continue;
				IL_244:
				A_1.AxisId = a_4;
				num = 56;
				continue;
				IL_2AE:
				num = 21;
				continue;
				IL_2F3:
				num = 25;
				continue;
				IL_2FF:
				if (!A_1.IsPrimary)
				{
					num = 49;
					continue;
				}
				num = 55;
				continue;
				IL_32F:
				num = 45;
				continue;
				IL_7CB:
				A_1 = (chartCategoryAxis as XlsChartValueAxis);
				A_1.Visible = true;
				num = 0;
				continue;
				IL_898:
				num = 6;
				continue;
				IL_8BC:
				num = 48;
			}
			IL_132:
			throw new ArgumentNullException(RecordTableEnumerator.b("㍀♂⑄⍆ⱈ㥊", a_));
			IL_2EE:
			return;
			IL_75C:
			throw new ArgumentNullException(RecordTableEnumerator.b("⁀㭂ⱄ㑆", a_));
		}
		}
	}

	// Token: 0x0600556E RID: 21870 RVA: 0x00368EC0 File Offset: 0x00367EC0
	private void ᜁ(XmlReader A_0, XlsChartAxis A_1, spr\u2306 A_2)
	{
		int a_ = 14;
		int num = 14;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 21;
				continue;
			case 1:
				goto IL_1F3;
			case 2:
				goto IL_1D3;
			case 3:
				if (!A_0.IsEmptyElement)
				{
					num = 22;
					continue;
				}
				goto IL_2CA;
			case 4:
				goto IL_1D3;
			case 5:
				num = 19;
				continue;
			case 6:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 1;
					continue;
				}
				num = 20;
				continue;
			case 7:
				goto IL_166;
			case 8:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("♃⥅ⱇ㍉᱋㱍", a_)))
				{
					num = 0;
					continue;
				}
				this.ᜅ(A_0, A_1);
				num = 18;
				continue;
			}
			case 9:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_158;
				default:
					if (false)
					{
					}
					num = 8;
					continue;
				}
				break;
			case 10:
				num = 11;
				continue;
			case 11:
				goto IL_125;
			case 12:
				goto IL_120;
			case 13:
				goto IL_1D3;
			case 15:
				if (A_0.LocalName != RecordTableEnumerator.b("ぃ㹅ᡇ㡉", a_))
				{
					num = 12;
					continue;
				}
				A_1.Font.Size = 10.0;
				num = 3;
				continue;
			case 16:
				goto IL_1D3;
			case 17:
				goto IL_158;
			case 18:
				goto IL_1D3;
			case 19:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 9;
					continue;
				}
				goto IL_125;
			}
			case 20:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 5;
					continue;
				}
				A_0.Skip();
				num = 2;
				continue;
			case 21:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("㑃", a_)))
				{
					num = 10;
					continue;
				}
				this.ᜀ(A_0, A_1, A_2);
				num = 13;
				continue;
			}
			case 22:
				A_0.Read();
				num = 16;
				continue;
			case 23:
				goto IL_87;
			}
			if (A_0 == null)
			{
				num = 23;
				continue;
			}
			num = 17;
			continue;
			IL_158:
			if (A_1 == null)
			{
				num = 7;
				continue;
			}
			num = 15;
			continue;
			IL_125:
			A_0.Skip();
			if (true)
			{
			}
			num = 4;
			continue;
			IL_1D3:
			num = 6;
		}
		IL_87:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙃⍅⥇⹉⥋㱍", a_));
		IL_120:
		throw new XmlException();
		IL_166:
		throw new ArgumentNullException(RecordTableEnumerator.b("╃㹅ⅇ㥉", a_));
		IL_1F3:
		IL_2CA:
		A_0.Read();
	}

	// Token: 0x0600556F RID: 21871 RVA: 0x003691A0 File Offset: 0x003681A0
	private void ᜀ(XmlReader A_0, XlsChartAxis A_1, spr\u2306 A_2)
	{
		int a_ = 3;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 1:
				num = 2;
				continue;
			case 2:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 9;
					continue;
				}
				goto IL_AC;
			case 3:
				if (A_0.LocalName == RecordTableEnumerator.b("崸帺嬼派ᅀㅂ", a_))
				{
					num = 8;
					continue;
				}
				goto IL_14B;
			case 5:
				IL_11:
				break;
			case 6:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 7;
					continue;
				}
				goto IL_14B;
			case 7:
				num = 3;
				continue;
			case 8:
			{
				if (true)
				{
				}
				TextSettings a_2 = spr\u1AA0.ᜀ(A_0, A_2, new float?(10f));
				spr\u1AA0.ᜀ(A_1.Font as IInternalFont, a_2);
				num = 0;
				continue;
			}
			case 9:
				goto IL_109;
			case 10:
				if (A_0.LocalName == RecordTableEnumerator.b("䤸", a_))
				{
					num = 1;
					continue;
				}
				goto IL_AC;
			}
			IL_47:
			num = 10;
			continue;
			goto IL_47;
			IL_AC:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_11;
			default:
				if (false)
				{
				}
				num = 6;
				continue;
			}
			IL_14B:
			A_0.Read();
			num = 4;
		}
		IL_109:
		A_0.Read();
	}

	// Token: 0x06005570 RID: 21872 RVA: 0x00369318 File Offset: 0x00368318
	private void ᜅ(XmlReader A_0, XlsChartAxis A_1)
	{
		int a_ = 6;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_C2;
			case 1:
				goto IL_93;
			case 2:
				goto IL_7E;
			case 3:
				if (A_0.LocalName != RecordTableEnumerator.b("帻儽␿㭁ᑃ㑅", a_))
				{
					num = 2;
					continue;
				}
				num = 4;
				continue;
			case 4:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("主儽㐿", a_)))
				{
					num = 8;
					continue;
				}
				goto IL_143;
			case 6:
				goto IL_88;
			case 7:
				goto IL_48;
			case 8:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_88;
				default:
					if (false)
					{
					}
					A_1.TextRotationAngle = XmlConvert.ToInt32(A_0.Value) / 60000;
					if (true)
					{
					}
					num = 0;
					continue;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 7;
				continue;
			}
			num = 6;
			continue;
			IL_88:
			if (A_1 == null)
			{
				num = 1;
			}
			else
			{
				num = 3;
			}
		}
		IL_48:
		throw new ArgumentNullException(RecordTableEnumerator.b("主嬽ℿ♁⅃㑅", a_));
		IL_7E:
		throw new XmlException();
		IL_93:
		throw new ArgumentNullException(RecordTableEnumerator.b("崻䘽⤿ㅁ", a_));
		IL_C2:
		IL_143:
		A_0.MoveToElement();
		A_0.Skip();
	}

	// Token: 0x06005571 RID: 21873 RVA: 0x00369478 File Offset: 0x00368478
	private void ᜄ(XmlReader A_0, XlsChartAxis A_1)
	{
		int a_ = 2;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				if (A_0.LocalName != RecordTableEnumerator.b("嬷䠹医䴽㌿❁㝃", a_))
				{
					num = 1;
					continue;
				}
				string a = spr\u1AA0.ᜄ(A_0);
				num = 3;
				continue;
			}
			case 1:
				goto IL_97;
			case 3:
			{
				string a;
				if (a == RecordTableEnumerator.b("唷嬹䐻", a_))
				{
					num = 7;
					continue;
				}
				goto IL_13B;
			}
			case 4:
				goto IL_48;
			case 5:
				goto IL_13B;
			case 6:
				if (A_1 == null)
				{
					num = 8;
					continue;
				}
				num = 0;
				continue;
			case 7:
			{
				IL_125:
				XlsChartValueAxis xlsChartValueAxis = spr\u2433.ᜀ(A_1) as XlsChartValueAxis;
				xlsChartValueAxis.IsMaxCross = true;
				num = 5;
				continue;
			}
			case 8:
				goto IL_BC;
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			num = 6;
			continue;
			IL_13B:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_125;
			default:
				goto IL_151;
			}
		}
		IL_48:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹崻娽┿ぁ", a_));
		IL_97:
		if (true)
		{
		}
		throw new XmlException(RecordTableEnumerator.b("洷吹夻䘽〿❁❃㉅ⵇ⹉汋㙍㵏㹑瑓≕㥗㵙牛", a_));
		IL_BC:
		throw new ArgumentNullException(RecordTableEnumerator.b("夷䈹唻䴽", a_));
		IL_151:
		if (false)
		{
		}
	}

	// Token: 0x06005572 RID: 21874 RVA: 0x003695DC File Offset: 0x003685DC
	private void ᜃ(XmlReader A_0, XlsChartAxis A_1)
	{
		int a_ = 3;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.LocalName != RecordTableEnumerator.b("娸䤺刼䰾㉀ɂ㵄", a_))
				{
					num = 2;
					continue;
				}
				goto IL_F0;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_E0;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 2:
				goto IL_8F;
			case 3:
				goto IL_62;
			case 4:
				goto IL_EE;
			case 5:
				goto IL_E0;
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			if (true)
			{
			}
			num = 5;
			continue;
			IL_E0:
			if (A_1 == null)
			{
				num = 4;
			}
			else
			{
				num = 0;
			}
		}
		IL_62:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬸帺尼嬾⑀ㅂ", a_));
		IL_8F:
		throw new XmlException(RecordTableEnumerator.b("永唺堼䜾ㅀ♂♄㍆ⱈ⽊浌㝎㱐㽒畔⍖㡘㱚獜", a_));
		IL_EE:
		throw new ArgumentNullException(RecordTableEnumerator.b("堸䌺吼䰾", a_));
		IL_F0:
		A_0.Skip();
	}

	// Token: 0x06005573 RID: 21875 RVA: 0x003696E0 File Offset: 0x003686E0
	private TickMarkType ᜁ(XmlReader A_0)
	{
		int a_ = 3;
		for (;;)
		{
			if (true)
			{
			}
			if (A_0 != null)
			{
				goto IL_50;
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
		IL_36:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䬸帺尼嬾⑀ㅂ", a_));
		IL_50:
		string key = spr\u1AA0.ᜄ(A_0);
		return sprῐ.ᜃ[key];
	}

	// Token: 0x06005574 RID: 21876 RVA: 0x00369750 File Offset: 0x00368750
	private void ᜂ(XmlReader A_0, XlsChartAxis A_1)
	{
		int a_ = 6;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_E0;
			case 1:
				if (A_0.LocalName != RecordTableEnumerator.b("䠻圽⌿⥁ࡃ⑅⑇ᩉ⍋㵍", a_))
				{
					num = 3;
					continue;
				}
				goto IL_F0;
			case 2:
				goto IL_EE;
			case 3:
				goto IL_97;
			case 4:
				goto IL_6A;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_E0;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			num = 0;
			continue;
			IL_E0:
			if (A_1 == null)
			{
				num = 2;
			}
			else
			{
				num = 1;
			}
		}
		IL_6A:
		throw new ArgumentNullException(RecordTableEnumerator.b("主嬽ℿ♁⅃㑅", a_));
		IL_97:
		throw new XmlException(RecordTableEnumerator.b("椻倽┿㩁㑃⍅⭇㹉⥋⩍灏⩑㥓㩕硗⹙㵛㥝也", a_));
		IL_EE:
		throw new ArgumentNullException(RecordTableEnumerator.b("崻䘽⤿ㅁ", a_));
		IL_F0:
		string key = spr\u1AA0.ᜄ(A_0);
		A_1.TickLabelPosition = sprῐ.ᜂ[key];
	}

	// Token: 0x06005575 RID: 21877 RVA: 0x00369868 File Offset: 0x00368868
	private void ᜁ(XmlReader A_0, XlsChartAxis A_1)
	{
		int a_ = 14;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_D2;
			case 1:
				goto IL_164;
			case 2:
				if (true)
				{
				}
				A_1.NumberFormat = A_0.Value;
				num = 0;
				continue;
			case 3:
				goto IL_CD;
			case 4:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("≃⥅㩇❉ⵋ㩍ፏ㵑こ㍕", a_)))
				{
					goto IL_138;
				}
				goto IL_D2;
			case 6:
				A_1.IsSourceLinked = XmlConvert.ToBoolean(A_0.Value);
				num = 1;
				continue;
			case 7:
				goto IL_197;
			case 8:
				if (A_0.LocalName != RecordTableEnumerator.b("⩃㍅╇౉⅋㩍", a_))
				{
					num = 7;
					continue;
				}
				num = 4;
				continue;
			case 9:
				if (A_0.MoveToAttribute(RecordTableEnumerator.b("㝃⥅㵇㡉⽋⭍ᱏ㭑㩓㵕㵗㹙", a_)))
				{
					num = 6;
					continue;
				}
				goto IL_1B0;
			case 10:
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				num = 8;
				continue;
			case 11:
				goto IL_70;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_138:
				num = 2;
				continue;
			default:
				if (false)
				{
				}
				if (A_0 == null)
				{
					num = 11;
					continue;
				}
				num = 10;
				continue;
			}
			IL_D2:
			num = 9;
		}
		IL_70:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙃⍅⥇⹉⥋㱍", a_));
		IL_CD:
		throw new ArgumentNullException(RecordTableEnumerator.b("╃㹅ⅇ㥉", a_));
		IL_164:
		goto IL_1B0;
		IL_197:
		throw new XmlException(RecordTableEnumerator.b("ᅃ⡅ⵇ㉉㱋⭍㍏♑ㅓ㉕硗≙ㅛ㉝䁟ᙡգť䙧", a_));
		IL_1B0:
		A_0.Read();
	}

	// Token: 0x06005576 RID: 21878 RVA: 0x00369A2C File Offset: 0x00368A2C
	private void ᜀ(XmlReader A_0, IChartGridLine A_1, sprវ A_2, RelationsCollection A_3)
	{
		int a_ = 3;
		int num = 2;
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
				goto IL_1D0;
			case 1:
				goto IL_163;
			case 3:
				goto IL_163;
			case 4:
			{
				spr\u1A7B a_2 = new spr\u1A7B(A_1.Border, null, null, A_1.Shadow, A_1.Format3D);
				spr\u1AA0.ᜀ(A_0, a_2, A_2, A_3);
				num = 13;
				continue;
			}
			case 5:
				goto IL_186;
			case 6:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 7;
					continue;
				}
				goto IL_E0;
			case 7:
				if (true)
				{
				}
				num = 14;
				continue;
			case 8:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 5;
					continue;
				}
				num = 6;
				continue;
			case 9:
				goto IL_60;
			case 10:
				if (A_1 == null)
				{
					num = 11;
					continue;
				}
				num = 0;
				continue;
			case 11:
				goto IL_DB;
			case 12:
				A_0.Read();
				num = 3;
				continue;
			case 13:
				goto IL_163;
			case 14:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1A8;
				default:
					if (false)
					{
					}
					if (A_0.LocalName == RecordTableEnumerator.b("䨸䬺洼䴾", a_))
					{
						num = 4;
						continue;
					}
					goto IL_E0;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 9;
				continue;
			}
			num = 10;
			continue;
			IL_E0:
			A_0.Skip();
			num = 1;
			continue;
			IL_163:
			num = 8;
		}
		IL_60:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬸帺尼嬾⑀ㅂ", a_));
		IL_DB:
		goto IL_1A8;
		IL_186:
		goto IL_1D0;
		IL_1A8:
		throw new ArgumentNullException(RecordTableEnumerator.b("常䤺吼嬾ീ⩂⭄≆㩈", a_));
		IL_1D0:
		A_0.Read();
	}

	// Token: 0x06005577 RID: 21879 RVA: 0x00369C10 File Offset: 0x00368C10
	private string ᜀ(XmlReader A_0, XlsChartAxis A_1)
	{
		int a_ = 14;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_6E:
			num = 0;
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
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				goto IL_A1;
			case 1:
				goto IL_8B;
			case 3:
				goto IL_58;
			}
			if (true)
			{
			}
			if (A_0 != null)
			{
				goto IL_6E;
			}
			num = 3;
		}
		IL_58:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙃⍅⥇⹉⥋㱍", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("㉃❅⑇㽉⥋ཌྷ⡏㭑❓", a_));
		IL_A1:
		return spr\u1AA0.ᜄ(A_0);
	}

	// Token: 0x06005578 RID: 21880 RVA: 0x00369CC8 File Offset: 0x00368CC8
	private spr\u20A3 ᜀ(XmlReader A_0)
	{
		int a_ = 10;
		int num = 27;
		spr\u20A3 spr_u20A;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("⼿ぁⵃ⍅♇㹉ⵋ㩍㥏㵑㩓", a_)))
				{
					num = 26;
					continue;
				}
				string a = spr\u1AA0.ᜄ(A_0);
				num = 2;
				continue;
			}
			case 1:
				goto IL_207;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_E0;
				default:
				{
					if (false)
					{
					}
					string a;
					if (a == RecordTableEnumerator.b("ⴿ⍁㱃୅ⅇ⑉", a_))
					{
						num = 3;
						continue;
					}
					goto IL_207;
				}
				}
				break;
			case 3:
				spr_u20A.ᜁ = new bool?(true);
				num = 24;
				continue;
			case 4:
				goto IL_2CD;
			case 5:
				goto IL_207;
			case 6:
				goto IL_207;
			case 7:
				goto IL_207;
			case 8:
				num = 0;
				continue;
			case 9:
				num = 25;
				continue;
			case 10:
				goto IL_207;
			case 11:
				num = 13;
				continue;
			case 12:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("ⰿⵁ⍃х⥇㥉⥋", a_)))
				{
					num = 8;
					continue;
				}
				A_0.Read();
				spr_u20A.ᜀ = new bool?(true);
				num = 15;
				continue;
			}
			case 13:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("ⴿ⭁⩃", a_)))
				{
					num = 9;
					continue;
				}
				spr_u20A.ᜃ = new double?(spr\u1AA0.ᜁ(A_0));
				num = 10;
				continue;
			}
			case 14:
				num = 12;
				continue;
			case 15:
				goto IL_207;
			case 16:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("ⴿ⍁㱃", a_)))
				{
					num = 11;
					continue;
				}
				spr_u20A.ᜂ = new double?(spr\u1AA0.ᜁ(A_0));
				num = 5;
				continue;
			}
			case 17:
				if (A_0.LocalName != RecordTableEnumerator.b("㌿⅁╃⩅ⅇ⑉⭋", a_))
				{
					num = 4;
					continue;
				}
				A_0.Read();
				spr_u20A = new spr\u20A3();
				goto IL_E0;
			case 18:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 14;
					continue;
				}
				goto IL_1F4;
			}
			case 19:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 20;
					continue;
				}
				num = 22;
				continue;
			case 20:
				goto IL_22A;
			case 21:
				goto IL_A2;
			case 22:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 23;
					continue;
				}
				A_0.Skip();
				num = 1;
				continue;
			case 23:
				num = 18;
				continue;
			case 24:
				goto IL_207;
			case 25:
				goto IL_1F4;
			case 26:
				num = 16;
				continue;
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 21;
				continue;
			}
			num = 17;
			continue;
			IL_E0:
			num = 6;
			continue;
			IL_1F4:
			A_0.Skip();
			num = 7;
			continue;
			IL_207:
			num = 19;
		}
		IL_A2:
		throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁╃≅ⵇ㡉", a_));
		IL_22A:
		A_0.Read();
		return spr_u20A;
		IL_2CD:
		throw new XmlException(RecordTableEnumerator.b("ᔿⱁ⅃㹅㡇⽉⽋㩍㕏㙑瑓⹕㕗㙙籛⩝şա䩣", a_));
	}

	// Token: 0x06005579 RID: 21881 RVA: 0x0036A058 File Offset: 0x00369058
	private void ᜀ(XmlReader A_0, XlsChartValueAxis A_1, RelationsCollection A_2)
	{
		int a_ = 7;
		int num = 20;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_87;
			case 1:
				if (A_0.LocalName != RecordTableEnumerator.b("夼嘾㉀㍂၄⥆⁈㽊㹌", a_))
				{
					num = 19;
					continue;
				}
				num = 4;
				continue;
			case 2:
				num = 15;
				continue;
			case 3:
				num = 16;
				continue;
			case 4:
				if (!A_0.IsEmptyElement)
				{
					num = 11;
					continue;
				}
				goto IL_2D7;
			case 5:
				goto IL_106;
			case 6:
				goto IL_1D0;
			case 7:
				num = 5;
				continue;
			case 8:
				goto IL_1F0;
			case 9:
				goto IL_1D0;
			case 10:
				goto IL_135;
			case 11:
				if (true)
				{
				}
				A_0.Read();
				num = 13;
				continue;
			case 12:
				if (A_1 == null)
				{
					num = 10;
					continue;
				}
				num = 1;
				continue;
			case 13:
				goto IL_1D0;
			case 14:
				goto IL_1D0;
			case 15:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("夼嘾㉀㍂၄⥆⁈㽊㹌͎㍐㽒", a_)))
				{
					num = 7;
					continue;
				}
				A_1.HasDisplayUnitLabel = true;
				sprᮟ a_2 = A_1.DisplayUnitLabel as sprᮟ;
				sprវ a_3 = A_1.ParentXlsChart.ParentWorkbook.DataHolder;
				spr\u1AA0.ᜀ(A_0, a_2, a_3, A_2);
				num = 14;
				continue;
			}
			case 16:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 23;
					continue;
				}
				goto IL_106;
			}
			case 17:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 3;
					continue;
				}
				A_0.Skip();
				num = 22;
				continue;
			case 18:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 8;
					continue;
				}
				num = 17;
				continue;
			case 19:
				goto IL_101;
			case 21:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("弼䨾⡀⽂ㅄๆ❈Ṋ⍌♎═", a_)))
				{
					num = 2;
					continue;
				}
				this.ᜀ(A_0, A_1);
				num = 6;
				continue;
			}
			case 22:
				goto IL_1D0;
			case 23:
				num = 21;
				continue;
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			num = 12;
			continue;
			IL_106:
			A_0.Skip();
			num = 9;
			continue;
			IL_1D0:
			num = 18;
		}
		IL_87:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_1F0:
			goto IL_2D7;
		default:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("似娾⁀❂⁄㕆", a_));
		}
		IL_101:
		throw new XmlException();
		IL_135:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬼帾ⵀ㙂⁄نㅈ≊㹌", a_));
		IL_2D7:
		A_0.Read();
	}

	// Token: 0x0600557A RID: 21882 RVA: 0x0036A344 File Offset: 0x00369344
	private void ᜀ(XmlReader A_0, XlsChartValueAxis A_1)
	{
		int a_ = 19;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_6E:
			num = 1;
			break;
		default:
			if (false)
			{
			}
			num = 3;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_58;
			case 1:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				goto IL_A1;
			case 2:
				goto IL_8B;
			}
			if (true)
			{
			}
			if (A_0 != null)
			{
				goto IL_6E;
			}
			num = 0;
		}
		IL_58:
		throw new ArgumentNullException(RecordTableEnumerator.b("㭈⹊ⱌ⭎㑐⅒", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("㽈⩊⅌㩎㑐ቒⵔ㹖⩘", a_));
		IL_A1:
		string value = spr\u1AA0.ᜄ(A_0);
		Excel2007ChartDisplayUnit displayUnit = (Excel2007ChartDisplayUnit)Enum.Parse(typeof(Excel2007ChartDisplayUnit), value, false);
		A_1.DisplayUnit = (ChartDisplayUnitType)displayUnit;
	}

	// Token: 0x0600557B RID: 21883 RVA: 0x0036A418 File Offset: 0x00369418
	private void ᜃ(XmlReader A_0, XlsChartAxis A_1, RelationsCollection A_2)
	{
		int a_ = 17;
		int num = 1;
		XlsChartCategoryAxis xlsChartCategoryAxis;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				string localName;
				int num2;
				if (spr\u22D2.ឩ.TryGetValue(localName, out num2))
				{
					num = 6;
					continue;
				}
				goto IL_158;
			}
			case 2:
				if (spr\u22D2.ឩ == null)
				{
					num = 7;
					continue;
				}
				goto IL_122;
			case 3:
			{
				int num2;
				switch (num2)
				{
				case 0:
					goto IL_14B;
				case 1:
					goto IL_9F;
				case 2:
					xlsChartCategoryAxis.TickMarkSpacing = spr\u1AA0.ᜂ(A_0);
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
				case 3:
					num = 13;
					continue;
				case 4:
					xlsChartCategoryAxis.MajorUnit = spr\u1AA0.ᜁ(A_0);
					num = 5;
					continue;
				case 5:
					goto IL_E6;
				}
				num = 11;
				continue;
			}
			case 4:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 9;
					continue;
				}
				goto IL_158;
			}
			case 5:
				return;
			case 6:
				num = 3;
				continue;
			case 7:
				spr\u22D2.ឩ = new Dictionary<string, int>(6)
				{
					{
						RecordTableEnumerator.b("⭆⭈❊Ɍ⥎㝐⁒ご⍖", a_),
						0
					},
					{
						RecordTableEnumerator.b("㍆⁈⡊♌͎㍐㽒ٔ㱖じ⭚", a_),
						1
					},
					{
						RecordTableEnumerator.b("㍆⁈⡊♌Ɏぐ⅒㹔і㉘㉚ⵜ", a_),
						2
					},
					{
						RecordTableEnumerator.b("♆㱈㽊≌", a_),
						3
					},
					{
						RecordTableEnumerator.b("⩆⡈⅊≌㵎ѐ㵒㱔⍖", a_),
						4
					},
					{
						RecordTableEnumerator.b("⩆⁈╊≌㵎ѐ㵒㱔⍖", a_),
						5
					}
				};
				num = 14;
				continue;
			case 8:
				xlsChartCategoryAxis = (A_1 as XlsChartCategoryAxis);
				num = 4;
				continue;
			case 9:
				num = 2;
				continue;
			case 10:
				return;
			case 11:
				num = 12;
				continue;
			case 12:
				goto IL_275;
			case 13:
				goto IL_76;
			case 14:
				goto IL_122;
			}
			if (A_0.NodeType == XmlNodeType.Element)
			{
				num = 8;
				continue;
			}
			goto IL_27A;
			IL_122:
			num = 0;
		}
		IL_76:
		xlsChartCategoryAxis.CategoryType = (spr\u1AA0.ᜃ(A_0) ? CategoryType.Automatic : CategoryType.Category);
		return;
		IL_9F:
		xlsChartCategoryAxis.TickLabelSpacing = spr\u1AA0.ᜂ(A_0);
		return;
		IL_E6:
		xlsChartCategoryAxis.MinorUnit = spr\u1AA0.ᜁ(A_0);
		return;
		IL_14B:
		xlsChartCategoryAxis.Offset = spr\u1AA0.ᜂ(A_0);
		return;
		IL_158:
		A_0.Skip();
		return;
		IL_275:
		goto IL_158;
		IL_27A:
		if (true)
		{
		}
		A_0.Skip();
	}

	// Token: 0x0600557C RID: 21884 RVA: 0x0036A6B0 File Offset: 0x003696B0
	private void ᜂ(XmlReader A_0, XlsChartAxis A_1, RelationsCollection A_2)
	{
		int a_ = 8;
		int num = 11;
		XlsChartCategoryAxis xlsChartCategoryAxis;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				xlsChartCategoryAxis = (A_1 as XlsChartCategoryAxis);
				num = 13;
				continue;
			case 1:
				num = 7;
				continue;
			case 2:
				num = 6;
				continue;
			case 3:
				goto IL_151;
			case 4:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("匽⤿ⱁ⭃㑅ᵇ⑉╋㩍", a_)))
				{
					num = 2;
					continue;
				}
				goto IL_76;
			}
			case 5:
				num = 12;
				continue;
			case 6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_151;
				default:
				{
					if (false)
					{
					}
					string localName;
					if (!(localName == RecordTableEnumerator.b("匽⤿ⱁ⭃㑅᱇⍉⅋⭍Տ㱑㵓≕", a_)))
					{
						num = 14;
						continue;
					}
					goto IL_182;
				}
				}
				break;
			case 7:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("刽∿⹁ୃ⁅⹇㥉⥋㩍", a_)))
				{
					num = 5;
					continue;
				}
				goto IL_19C;
			}
			case 8:
				goto IL_8B;
			case 9:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("匽ℿ⡁⭃㑅᱇⍉⅋⭍Տ㱑㵓≕", a_)))
				{
					num = 10;
					continue;
				}
				goto IL_C1;
			}
			case 10:
				num = 8;
				continue;
			case 12:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("匽ℿ⡁⭃㑅ᵇ⑉╋㩍", a_)))
				{
					num = 3;
					continue;
				}
				goto IL_10C;
			}
			case 13:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 1;
					continue;
				}
				goto IL_119;
			}
			case 14:
				num = 9;
				continue;
			}
			if (A_0.NodeType == XmlNodeType.Element)
			{
				num = 0;
				continue;
			}
			goto IL_232;
			IL_151:
			num = 4;
		}
		IL_76:
		xlsChartCategoryAxis.MinorUnit = spr\u1AA0.ᜁ(A_0);
		return;
		IL_8B:
		goto IL_119;
		IL_C1:
		string a_2 = spr\u1AA0.ᜄ(A_0);
		((XlsChartCategoryAxis)A_1).MajorUnitScale = this.ᜂ(a_2);
		return;
		IL_10C:
		xlsChartCategoryAxis.MajorUnit = spr\u1AA0.ᜁ(A_0);
		return;
		IL_119:
		A_0.Skip();
		return;
		IL_182:
		a_2 = spr\u1AA0.ᜄ(A_0);
		((XlsChartCategoryAxis)A_1).MinorUnitScale = this.ᜂ(a_2);
		return;
		IL_19C:
		xlsChartCategoryAxis.Offset = spr\u1AA0.ᜂ(A_0);
		return;
		IL_232:
		A_0.Skip();
	}

	// Token: 0x0600557D RID: 21885 RVA: 0x0036A8F8 File Offset: 0x003698F8
	private void ᜁ(XmlReader A_0, XlsChartAxis A_1, RelationsCollection A_2)
	{
		int a_ = 2;
		switch (0)
		{
		default:
		{
			int num = 0;
			XlsChartValueAxis xlsChartValueAxis;
			string a;
			XlsChart xlsChart;
			for (;;)
			{
				switch (num)
				{
				case 1:
					num = 15;
					continue;
				case 2:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 9;
						continue;
					}
					goto IL_1AF;
				}
				case 3:
					goto IL_133;
				case 4:
					num = 14;
					continue;
				case 5:
					xlsChartValueAxis = (A_1 as XlsChartValueAxis);
					num = 2;
					continue;
				case 6:
					num = 3;
					continue;
				case 7:
					if (!xlsChartValueAxis.IsPrimary)
					{
						num = 4;
						continue;
					}
					num = 16;
					continue;
				case 8:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("尷匹伻丽ᔿⱁⵃ㉅㭇", a_)))
					{
						num = 6;
						continue;
					}
					goto IL_135;
				}
				case 9:
					num = 11;
					continue;
				case 10:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("唷匹刻儽㈿ᝁ⩃⽅㱇", a_)))
					{
						num = 12;
						continue;
					}
					goto IL_11A;
				}
				case 11:
				{
					if (true)
					{
					}
					string localName;
					if (!(localName == RecordTableEnumerator.b("嬷䠹医䴽㌿A⅃㉅㽇⽉⥋⁍", a_)))
					{
						num = 1;
						continue;
					}
					a = spr\u1AA0.ᜄ(A_0);
					xlsChart = xlsChartValueAxis.ParentXlsChart;
					num = 7;
					continue;
				}
				case 12:
					num = 8;
					continue;
				case 13:
					num = 10;
					continue;
				case 14:
					goto IL_1C2;
				case 15:
				{
					string localName;
					if (!(localName == RecordTableEnumerator.b("唷嬹嘻儽㈿ᝁ⩃⽅㱇", a_)))
					{
						num = 13;
						continue;
					}
					goto IL_204;
				}
				case 16:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6D;
					}
					goto Block_8;
				}
				IL_6D:
				if (A_0.NodeType != XmlNodeType.Element)
				{
					goto IL_267;
				}
				num = 5;
			}
			IL_11A:
			xlsChartValueAxis.ᜁ(spr\u1AA0.ᜁ(A_0));
			return;
			IL_133:
			goto IL_1AF;
			IL_135:
			this.ᜀ(A_0, xlsChartValueAxis, A_2);
			return;
			IL_1AF:
			A_0.Skip();
			return;
			IL_1C2:
			IChartCategoryAxis chartCategoryAxis = xlsChart.SecondaryCategoryAxis;
			goto IL_24B;
			IL_204:
			xlsChartValueAxis.ᜀ(spr\u1AA0.ᜁ(A_0));
			return;
			Block_8:
			if (false)
			{
			}
			chartCategoryAxis = xlsChart.PrimaryCategoryAxis;
			IL_24B:
			IChartCategoryAxis chartCategoryAxis2 = chartCategoryAxis;
			chartCategoryAxis2.AxisBetweenCategories = (a == RecordTableEnumerator.b("娷弹䠻䤽┿❁⩃", a_));
			return;
			IL_267:
			A_0.Skip();
			return;
		}
		}
	}

	// Token: 0x0600557E RID: 21886 RVA: 0x0036AB74 File Offset: 0x00369B74
	private void ᜀ(XmlReader A_0, XlsChartAxis A_1, RelationsCollection A_2)
	{
		int a_ = 10;
		int num = 2;
		XlsChartSeriesAxis xlsChartSeriesAxis;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 7;
					continue;
				}
				goto IL_92;
			}
			case 1:
				goto IL_92;
			case 3:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("㐿⭁❃ⵅՇ⭉㹋╍͏㥑㵓♕", a_)))
				{
					num = 5;
					continue;
				}
				goto IL_E8;
			}
			case 4:
				IL_90:
				if (true)
				{
				}
				num = 3;
				continue;
			case 5:
				num = 1;
				continue;
			case 6:
			{
				string localName;
				if (!(localName == RecordTableEnumerator.b("㐿⭁❃ⵅч⡉⁋ᵍ㭏㭑⑓", a_)))
				{
					num = 4;
					continue;
				}
				goto IL_53;
			}
			case 7:
				num = 6;
				continue;
			case 8:
				xlsChartSeriesAxis = (A_1 as XlsChartSeriesAxis);
				num = 0;
				continue;
			}
			if (A_0.NodeType == XmlNodeType.Element)
			{
				num = 8;
				continue;
			}
			goto IL_12E;
			IL_92:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_90;
			default:
				goto IL_A8;
			}
		}
		IL_53:
		xlsChartSeriesAxis.TickLabelSpacing = spr\u1AA0.ᜂ(A_0);
		return;
		IL_A8:
		if (false)
		{
		}
		A_0.Skip();
		return;
		IL_E8:
		xlsChartSeriesAxis.TickMarkSpacing = spr\u1AA0.ᜂ(A_0);
		return;
		IL_12E:
		A_0.Skip();
	}

	// Token: 0x0600557F RID: 21887 RVA: 0x0036ACB8 File Offset: 0x00369CB8
	private ChartBaseUnitType ᜂ(string A_0)
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
		A_0 = this.ᜁ(A_0);
		return (ChartBaseUnitType)Enum.Parse(typeof(ChartBaseUnitType), A_0, false);
	}

	// Token: 0x06005580 RID: 21888 RVA: 0x0036AD14 File Offset: 0x00369D14
	private string ᜁ(string A_0)
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
		A_0 = this.ᜀ(A_0);
		char c = A_0[0];
		return char.ToUpper(c) + A_0.Substring(1);
	}

	// Token: 0x06005581 RID: 21889 RVA: 0x0036AD78 File Offset: 0x00369D78
	private string ᜀ(string A_0)
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
		return A_0.Substring(0, A_0.Length - 1);
	}

	// Token: 0x06005582 RID: 21890 RVA: 0x0036ADC4 File Offset: 0x00369DC4
	// Note: this type is marked as 'beforefieldinit'.
	static sprῐ()
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
		sprῐ.ᜂ = new Dictionary<string, TickLabelPositionType>(4);
		sprῐ.ᜃ = new Dictionary<string, TickMarkType>(4);
	}

	// Token: 0x04002909 RID: 10505
	public const string ᜀ = "Calibri";

	// Token: 0x0400290A RID: 10506
	public const float ᜁ = 10f;

	// Token: 0x0400290B RID: 10507
	private static Dictionary<string, TickLabelPositionType> ᜂ;

	// Token: 0x0400290C RID: 10508
	private static Dictionary<string, TickMarkType> ᜃ;

	// Token: 0x0400290D RID: 10509
	private XlsWorkbook ᜄ;

	// Token: 0x02000585 RID: 1413
	// (Invoke) Token: 0x06005584 RID: 21892
	private delegate void ᜀ(XmlReader A_0, XlsChartAxis A_1, RelationsCollection A_2);
}
