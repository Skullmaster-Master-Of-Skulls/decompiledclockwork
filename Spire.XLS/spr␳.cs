using System;
using System.Collections.Generic;
using System.Xml;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Charts;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.XmlSerialization;

// Token: 0x0200041B RID: 1051
internal class spr\u2433
{
	// Token: 0x06003EAD RID: 16045 RVA: 0x0022BA94 File Offset: 0x0022AA94
	static spr\u2433()
	{
		int a_ = 12;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u2433.ᜁ = new Dictionary<TickLabelPositionType, string>(4);
		spr\u2433.ᜂ = new Dictionary<TickMarkType, string>(4);
		spr\u2433.ᜁ.Add(TickLabelPositionType.TickLabelPositionHigh, RecordTableEnumerator.b("⩁ⵃⅅ⁇", a_));
		spr\u2433.ᜁ.Add(TickLabelPositionType.TickLabelPositionLow, RecordTableEnumerator.b("⹁⭃ㅅ", a_));
		spr\u2433.ᜁ.Add(TickLabelPositionType.TickLabelPositionNextToAxis, RecordTableEnumerator.b("ⱁ⅃㹅㱇ṉ⍋", a_));
		spr\u2433.ᜁ.Add(TickLabelPositionType.TickLabelPositionNone, RecordTableEnumerator.b("ⱁ⭃⡅ⵇ", a_));
		spr\u2433.ᜂ.Add(TickMarkType.TickMarkNone, RecordTableEnumerator.b("ⱁ⭃⡅ⵇ", a_));
		spr\u2433.ᜂ.Add(TickMarkType.TickMarkInside, RecordTableEnumerator.b("⭁⩃", a_));
		spr\u2433.ᜂ.Add(TickMarkType.TickMarkOutside, RecordTableEnumerator.b("ⵁㅃ㉅", a_));
		spr\u2433.ᜂ.Add(TickMarkType.TickMarkCross, RecordTableEnumerator.b("⅁㙃⥅㭇㥉", a_));
	}

	// Token: 0x06003EAE RID: 16046 RVA: 0x0022BBB8 File Offset: 0x0022ABB8
	public void ᜀ(XmlWriter A_0, IChartAxis A_1, RelationsCollection A_2)
	{
		int a_ = 3;
		int num = 8;
		XlsChartCategoryAxis xlsChartCategoryAxis;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_144;
				default:
					goto IL_E7;
				}
				break;
			case 1:
				num = 0;
				continue;
			case 2:
			{
				if (A_1 == null)
				{
					num = 6;
					continue;
				}
				AxisType axisType = A_1.AxisType;
				num = 9;
				continue;
			}
			case 3:
				goto IL_77;
			case 4:
				if (!xlsChartCategoryAxis.IsChartBubbleOrScatter)
				{
					num = 10;
					continue;
				}
				goto IL_110;
			case 5:
				if (xlsChartCategoryAxis.CategoryType == CategoryType.Time)
				{
					num = 3;
					continue;
				}
				goto IL_55;
			case 6:
				goto IL_AD;
			case 7:
				goto IL_50;
			case 9:
			{
				AxisType axisType;
				switch (axisType)
				{
				case AxisType.Category:
					xlsChartCategoryAxis = (XlsChartCategoryAxis)A_1;
					num = 4;
					continue;
				case AxisType.Value:
					goto IL_82;
				case AxisType.Serie:
					goto IL_B8;
				default:
					num = 1;
					continue;
				}
				break;
			}
			case 10:
				goto IL_144;
			}
			if (A_0 == null)
			{
				num = 7;
				continue;
			}
			num = 2;
			continue;
			IL_144:
			num = 5;
		}
		IL_50:
		throw new ArgumentNullException(RecordTableEnumerator.b("丸䤺吼䬾⑀ㅂ", a_));
		IL_55:
		this.ᜀ(A_0, xlsChartCategoryAxis);
		return;
		IL_77:
		this.ᜁ(A_0, xlsChartCategoryAxis);
		return;
		IL_82:
		this.ᜁ(A_0, (XlsChartValueAxis)A_1, A_2);
		return;
		IL_AD:
		if (true)
		{
		}
		return;
		IL_B8:
		this.ᜀ(A_0, (XlsChartSeriesAxis)A_1);
		return;
		IL_E7:
		if (false)
		{
		}
		throw new NotSupportedException();
		IL_110:
		this.ᜁ(A_0, (XlsChartValueAxis)A_1, A_2);
	}

	// Token: 0x06003EAF RID: 16047 RVA: 0x0022BD48 File Offset: 0x0022AD48
	private void ᜁ(XmlWriter A_0, XlsChartCategoryAxis A_1)
	{
		int a_ = 4;
		for (;;)
		{
			IL_09:
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!A_1.BaseUnitIsAuto)
					{
						num = 1;
						continue;
					}
					goto IL_10D;
				case 1:
				{
					string a_2 = this.ᜀ(A_1.BaseUnit);
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("堹崻䴽┿ᙁⵃ⭅ⵇὉ≋❍⑏", a_), a_2);
					num = 9;
					continue;
				}
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					default:
						if (false)
						{
						}
						if (!A_1.IsAutoMajor)
						{
							num = 3;
							continue;
						}
						goto IL_D6;
					}
					break;
				case 3:
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("圹崻吽⼿ぁᅃ⡅ⅇ㹉", a_), XmlConvert.ToString(A_1.MajorUnit));
					num = 11;
					continue;
				case 4:
					if (!A_1.IsAutoMinor)
					{
						num = 12;
						continue;
					}
					goto IL_213;
				case 5:
					if (A_1 == null)
					{
						num = 7;
						continue;
					}
					if (true)
					{
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("帹崻䨽┿́㱃", a_), RecordTableEnumerator.b("刹䠻䨽〿硁歃楅㭇⥉⑋⭍㵏㍑❓硕㝗⩙㥛そᡟཡࡣeݧᡩū཭ѯű婳᥵੷ᵹ卻᩽뾏ꂑ꒓ꚕ꺗떙ﾛ솟킡킣", a_));
					this.ᜇ(A_0, A_1);
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("嘹帻刽༿⑁≃㕅ⵇ㹉", a_), A_1.Offset.ToString());
					num = 0;
					continue;
				case 6:
					goto IL_180;
				case 7:
					goto IL_D1;
				case 9:
					goto IL_10D;
				case 10:
					goto IL_58;
				case 11:
					goto IL_D6;
				case 12:
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("圹唻倽⼿ぁᅃ⡅ⅇ㹉", a_), XmlConvert.ToString(A_1.MinorUnit));
					num = 6;
					continue;
				}
				if (A_0 == null)
				{
					num = 10;
					continue;
				}
				num = 5;
				continue;
				IL_D6:
				num = 4;
				continue;
				IL_10D:
				num = 2;
			}
		}
		IL_58:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴹主圽㐿❁㙃", a_));
		IL_D1:
		throw new ArgumentNullException(RecordTableEnumerator.b("嬹䐻圽㌿", a_));
		IL_180:
		IL_213:
		string a_3 = this.ᜀ(A_1.MajorUnitScale);
		spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("圹崻吽⼿ぁ၃⽅╇⽉᥋⁍㥏♑", a_), a_3);
		a_3 = this.ᜀ(A_1.MinorUnitScale);
		spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("圹唻倽⼿ぁ၃⽅╇⽉᥋⁍㥏♑", a_), a_3);
		A_0.WriteEndElement();
	}

	// Token: 0x06003EB0 RID: 16048 RVA: 0x0022BFB4 File Offset: 0x0022AFB4
	private string ᜀ(ChartBaseUnitType A_0)
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
		return A_0.ToString().ToLower() + 's';
	}

	// Token: 0x06003EB1 RID: 16049 RVA: 0x0022C00C File Offset: 0x0022B00C
	private void ᜀ(XmlWriter A_0, XlsChartCategoryAxis A_1)
	{
		int a_ = 1;
		int num = 14;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 11;
				continue;
			case 1:
				goto IL_92;
			case 2:
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("䌶倸堺嘼猾⍀⽂ᙄⱆ⁈㭊", a_), A_1.TickLabelSpacing.ToString());
				num = 12;
				continue;
			case 3:
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("嘶䰸伺刼", a_), true);
				num = 1;
				continue;
			case 4:
				if (!A_1.IsAutoMajor)
				{
					num = 2;
					continue;
				}
				goto IL_72;
			case 5:
				goto IL_182;
			case 6:
				if (A_1 == null)
				{
					num = 10;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("吶堸伺簼䜾", a_), RecordTableEnumerator.b("弶䴸伺䴼Ծ湀求㙄⑆ⅈ⹊⁌⹎≐絒㩔❖㱘㕚╜㉞ൠբ੤ᕦѨ੪ᥬᱮ彰ᱲݴၶ噸ὺོṾꊌ붎ꆐꎒꎔ뢖滛ﲜ햠", a_));
				this.ᜇ(A_0, A_1);
				num = 8;
				continue;
			case 7:
				goto IL_1AF;
			case 8:
				if (A_1.CategoryType == CategoryType.Automatic)
				{
					num = 3;
					continue;
				}
				goto IL_92;
			case 9:
				if (A_1.IsAutoMinor)
				{
					num = 0;
					continue;
				}
				goto IL_182;
			case 10:
				goto IL_10C;
			case 11:
				if (!A_1.AutoTickMarkSpacing)
				{
					num = 5;
					continue;
				}
				goto IL_23B;
			case 12:
				if (true)
				{
				}
				goto IL_72;
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
			num = 9;
			continue;
			IL_92:
			spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("嬶嬸场爼夾❀あ⁄㍆", a_), A_1.Offset.ToString());
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_182:
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("䌶倸堺嘼爾⁀ㅂ⹄ᑆ≈≊㵌", a_), A_1.TickMarkSpacing.ToString());
				num = 7;
				break;
			default:
				if (false)
				{
				}
				num = 4;
				break;
			}
		}
		IL_6D:
		throw new ArgumentNullException(RecordTableEnumerator.b("䀶䬸刺䤼娾㍀", a_));
		IL_10C:
		throw new ArgumentNullException(RecordTableEnumerator.b("嘶䄸刺丼", a_));
		IL_1AF:
		IL_23B:
		A_0.WriteEndElement();
	}

	// Token: 0x06003EB2 RID: 16050 RVA: 0x0022C25C File Offset: 0x0022B25C
	private void ᜁ(XmlWriter A_0, XlsChartValueAxis A_1, RelationsCollection A_2)
	{
		int a_ = 5;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_263:
			num = 6;
			break;
		default:
			if (false)
			{
			}
			num = 13;
			break;
		}
		for (;;)
		{
			IChartCategoryAxis chartCategoryAxis;
			string text;
			IChartCategoryAxis chartCategoryAxis2;
			switch (num)
			{
			case 0:
				goto IL_C4;
			case 1:
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("嘺吼儾⹀ㅂ၄⥆⁈㽊", a_), XmlConvert.ToString(A_1.MinorUnit));
				num = 5;
				continue;
			case 2:
				num = 8;
				continue;
			case 3:
				goto IL_92;
			case 4:
				if (!A_1.IsPrimary)
				{
					num = 2;
					continue;
				}
				num = 7;
				continue;
			case 5:
				goto IL_21A;
			case 6:
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("嘺尼唾⹀ㅂ၄⥆⁈㽊", a_), XmlConvert.ToString(A_1.MajorUnit));
				num = 14;
				continue;
			case 7:
			{
				XlsChart xlsChart;
				chartCategoryAxis = xlsChart.PrimaryCategoryAxis;
				goto IL_133;
			}
			case 8:
			{
				if (true)
				{
				}
				XlsChart xlsChart;
				chartCategoryAxis = xlsChart.SecondaryCategoryAxis;
				goto IL_133;
			}
			case 9:
				if (!A_1.IsAutoMinor)
				{
					num = 1;
					continue;
				}
				goto IL_273;
			case 10:
				num = 11;
				continue;
			case 11:
				text = RecordTableEnumerator.b("嘺吼嬾ɀ≂ㅄ", a_);
				goto IL_237;
			case 12:
			{
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("䴺尼匾@㭂", a_), RecordTableEnumerator.b("区䤼䬾ㅀ祂橄框㩈⡊╌⩎㱐㉒♔祖㙘⭚㡜ㅞᥠ๢।Ŧ٨ᥪl๮հr孴ᡶ୸ᱺ剼᭾뺐ꆒꖔꞖ꾘뒚ﺜ삠톢톤", a_));
				this.ᜇ(A_0, A_1);
				XlsChart xlsChart = A_1.ParentXlsChart;
				num = 4;
				continue;
			}
			case 14:
				goto IL_E1;
			case 15:
				if (!chartCategoryAxis2.AxisBetweenCategories)
				{
					num = 10;
					continue;
				}
				num = 17;
				continue;
			case 16:
				if (!A_1.IsAutoMajor)
				{
					goto IL_263;
				}
				goto IL_E1;
			case 17:
				text = RecordTableEnumerator.b("夺堼䬾㙀♂⁄⥆", a_);
				goto IL_237;
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			num = 12;
			continue;
			IL_E1:
			num = 9;
			continue;
			IL_133:
			chartCategoryAxis2 = chartCategoryAxis;
			num = 15;
			continue;
			IL_237:
			string a_2 = text;
			spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("堺似倾㉀あ݄≆㵈㱊⡌⩎㽐", a_), a_2);
			num = 16;
		}
		IL_92:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰺似嘾㕀♂㝄", a_));
		IL_C4:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴺尼匾㑀♂ф㽆⁈㡊", a_));
		IL_21A:
		IL_273:
		this.ᜀ(A_0, A_1, A_2);
		A_0.WriteEndElement();
	}

	// Token: 0x06003EB3 RID: 16051 RVA: 0x0022C4EC File Offset: 0x0022B4EC
	private void ᜀ(XmlWriter A_0, XlsChartSeriesAxis A_1)
	{
		int a_ = 17;
		int num = 8;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!A_1.AutoTickMarkSpacing)
				{
					num = 4;
					continue;
				}
				goto IL_190;
			case 1:
				if (!A_1.AutoTickLabelSpacing)
				{
					num = 9;
					continue;
				}
				goto IL_15C;
			case 2:
				goto IL_146;
			case 3:
				goto IL_15C;
			case 4:
				goto IL_119;
			case 5:
				if (A_1 == null)
				{
					num = 7;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("㑆ⱈ㥊ౌ㝎", a_), RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒♔㑖ㅘ㹚ぜ㹞በ䵢੤ᝦ౨ժᕬɮᵰᕲᩴնᑸ᩺ॼ౾꾀Ꚉﾌ朗ﮔ늜궞醠鎢鎤袦쪨쎪첬\uddae얰", a_));
				this.ᜇ(A_0, A_1);
				num = 1;
				continue;
			case 6:
				goto IL_75;
			case 7:
				goto IL_117;
			case 9:
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("㍆⁈⡊♌͎㍐㽒ٔ㱖じ⭚", a_), A_1.TickLabelSpacing.ToString());
				num = 3;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_119:
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("㍆⁈⡊♌Ɏぐ⅒㹔і㉘㉚ⵜ", a_), A_1.TickMarkSpacing.ToString());
				num = 2;
				continue;
			default:
				if (false)
				{
				}
				if (A_0 == null)
				{
					num = 6;
					continue;
				}
				num = 5;
				continue;
			}
			IL_15C:
			num = 0;
		}
		IL_75:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("う㭈≊㥌⩎⍐", a_));
		IL_117:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑆ⱈ㥊⑌⩎≐ቒⵔ㹖⩘", a_));
		IL_146:
		IL_190:
		A_0.WriteEndElement();
	}

	// Token: 0x06003EB4 RID: 16052 RVA: 0x0022C690 File Offset: 0x0022B690
	private void ᜇ(XmlWriter A_0, XlsChartAxis A_1)
	{
		int a_ = 17;
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				XlsChartValueAxis xlsChartValueAxis;
				string a_2;
				string a_4;
				switch (num)
				{
				case 0:
					if (xlsChartValueAxis.IsMaxCross)
					{
						num = 8;
						continue;
					}
					num = 12;
					continue;
				case 1:
					goto IL_E7;
				case 2:
					goto IL_B9;
				case 3:
					goto IL_E7;
				case 5:
					if (A_1.ParagraphType == ChartParagraphType.Default)
					{
						num = 16;
						continue;
					}
					goto IL_1E3;
				case 6:
					if (true)
					{
					}
					num = 15;
					continue;
				case 7:
					spr\u1CFF.ᜀ(A_0, A_1.FrameFormat, A_1.ParentXlsChart, false);
					num = 3;
					continue;
				case 8:
					a_2 = RecordTableEnumerator.b("⩆⡈㍊", a_);
					num = 28;
					continue;
				case 9:
					if (A_1.\u1714 != null)
					{
						num = 21;
						continue;
					}
					return;
				case 10:
				{
					XlsChart xlsChart;
					RelationsCollection a_3;
					spr\u1CFF.ᜀ(A_0, A_1.TitleArea, xlsChart.ParentWorkbook, a_3, 10.0);
					num = 23;
					continue;
				}
				case 11:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_BE;
					default:
						if (false)
						{
						}
						if (A_1.FrameFormat.HasInterior)
						{
							num = 6;
							continue;
						}
						goto IL_49B;
					}
					break;
				case 12:
					if (!xlsChartValueAxis.IsAutoCross)
					{
						num = 19;
						continue;
					}
					goto IL_239;
				case 13:
					goto IL_338;
				case 14:
					goto IL_1E3;
				case 15:
					if (A_1.FrameFormat.Interior.Pattern != ExcelPatternType.None)
					{
						num = 7;
						continue;
					}
					goto IL_49B;
				case 16:
					this.ᜀ(A_0, A_1);
					num = 14;
					continue;
				case 17:
				{
					if (A_1 == null)
					{
						num = 20;
						continue;
					}
					XlsChart xlsChart = A_1.ParentXlsChart;
					sprᡟ sprᡟ = xlsChart.DataHolder;
					sprវ sprវ = sprᡟ.ᜋ();
					RelationsCollection a_3 = xlsChart.Relations;
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("♆ㅈɊ⥌", a_), A_1.AxisId.ToString());
					this.ᜁ(A_0, A_1);
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("⍆ⱈ❊⡌㭎㑐", a_), A_1.Deleted);
					this.ᜂ(A_0, A_1);
					this.ᜃ(A_0, A_1);
					num = 26;
					continue;
				}
				case 18:
					goto IL_239;
				case 19:
					a_2 = XmlConvert.ToString(xlsChartValueAxis.CrossesAt);
					a_4 = RecordTableEnumerator.b("⑆㭈⑊㹌㱎㑐⁒ᑔ⍖", a_);
					num = 18;
					continue;
				case 20:
					goto IL_438;
				case 21:
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("⭆⭈❊ౌ⍎㙐㵒", a_), A_1.\u1714);
					num = 13;
					continue;
				case 22:
					if (xlsChartValueAxis != null)
					{
						num = 25;
						continue;
					}
					goto IL_239;
				case 23:
					goto IL_38B;
				case 24:
				{
					A_0.WriteStartElement(RecordTableEnumerator.b("㑆㥈ᭊ㽌", a_), RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒♔㑖ㅘ㹚ぜ㹞በ䵢੤ᝦ౨ժᕬɮᵰᕲᩴնᑸ᩺ॼ౾꾀Ꚉﾌ朗ﮔ늜궞醠鎢鎤袦쪨쎪첬\uddae얰", a_));
					sprវ sprវ;
					spr\u1CFF.ᜀ(A_0, A_1.Border, sprវ.\u171C());
					A_0.WriteEndElement();
					num = 1;
					continue;
				}
				case 25:
					goto IL_BE;
				case 26:
					if (A_1.HasAxisTitle)
					{
						num = 10;
						continue;
					}
					goto IL_38B;
				case 27:
					if (A_1.FrameFormat.HasLineProperties)
					{
						num = 24;
						continue;
					}
					goto IL_E7;
				case 28:
					goto IL_239;
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				num = 17;
				continue;
				IL_BE:
				num = 0;
				continue;
				IL_E7:
				num = 5;
				continue;
				IL_1E3:
				this.ᜆ(A_0, A_1);
				a_2 = RecordTableEnumerator.b("♆㱈㽊≌ᕎ㑐⅒㩔", a_);
				a_4 = RecordTableEnumerator.b("⑆㭈⑊㹌㱎㑐⁒", a_);
				xlsChartValueAxis = (spr\u2433.ᜀ(A_1) as XlsChartValueAxis);
				num = 22;
				continue;
				IL_239:
				spr\u1CFF.ᜀ(A_0, a_4, a_2);
				num = 9;
				continue;
				IL_38B:
				this.ᜄ(A_0, A_1);
				this.ᜀ(A_0, RecordTableEnumerator.b("⩆⡈⅊≌㵎Ր㩒㙔㱖ᑘ㩚⽜㑞", a_), A_1.MajorTickMark);
				this.ᜀ(A_0, RecordTableEnumerator.b("⩆⁈╊≌㵎Ր㩒㙔㱖ᑘ㩚⽜㑞", a_), A_1.MinorTickMark);
				this.ᜅ(A_0, A_1);
				num = 11;
				continue;
				IL_49B:
				num = 27;
			}
			IL_B9:
			throw new ArgumentNullException(RecordTableEnumerator.b("う㭈≊㥌⩎⍐", a_));
			IL_338:
			return;
			IL_438:
			throw new ArgumentNullException(RecordTableEnumerator.b("♆ㅈ≊㹌", a_));
		}
		}
	}

	// Token: 0x06003EB5 RID: 16053 RVA: 0x0022CB68 File Offset: 0x0022BB68
	public static IChartAxis ᜀ(XlsChartAxis A_0)
	{
		IChartAxis result;
		for (;;)
		{
			for (;;)
			{
				result = null;
				int num = 11;
				for (;;)
				{
					IChartAxis chartAxis;
					IChartAxis chartAxis2;
					switch (num)
					{
					case 0:
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
							XlsChart xlsChart;
							chartAxis = xlsChart.PrimaryValueAxis;
							goto IL_165;
						}
						}
						break;
					case 1:
						num = 6;
						continue;
					case 2:
						num = 9;
						continue;
					case 3:
						if (!A_0.IsPrimary)
						{
							num = 5;
							continue;
						}
						num = 0;
						continue;
					case 4:
						if (!A_0.IsPrimary)
						{
							if (true)
							{
							}
							num = 2;
							continue;
						}
						num = 14;
						continue;
					case 5:
						num = 8;
						continue;
					case 6:
						return result;
					case 7:
						return result;
					case 8:
					{
						XlsChart xlsChart;
						chartAxis = xlsChart.SecondaryValueAxis;
						goto IL_165;
					}
					case 9:
					{
						XlsChart xlsChart;
						chartAxis2 = xlsChart.SecondaryCategoryAxis;
						goto IL_173;
					}
					case 10:
					{
						XlsChart xlsChart = A_0.ParentXlsChart;
						AxisType axisType = A_0.AxisType;
						num = 13;
						continue;
					}
					case 11:
						if (A_0 != null)
						{
							num = 10;
							continue;
						}
						return result;
					case 12:
						return result;
					case 13:
					{
						AxisType axisType;
						switch (axisType)
						{
						case AxisType.Category:
							num = 3;
							continue;
						case AxisType.Value:
							num = 4;
							continue;
						default:
							num = 1;
							continue;
						}
						break;
					}
					case 14:
					{
						XlsChart xlsChart;
						chartAxis2 = xlsChart.PrimaryCategoryAxis;
						goto IL_173;
					}
					}
					break;
					IL_165:
					result = chartAxis;
					num = 7;
					continue;
					IL_173:
					result = chartAxis2;
					num = 12;
				}
			}
		}
		return result;
	}

	// Token: 0x06003EB6 RID: 16054 RVA: 0x0022CCF8 File Offset: 0x0022BCF8
	private void ᜆ(XmlWriter A_0, XlsChartAxis A_1)
	{
		int a_ = 2;
		switch (0)
		{
		default:
		{
			int num = 6;
			XlsChartAxis xlsChartAxis;
			for (;;)
			{
				IChartCategoryAxis chartCategoryAxis;
				IChartValueAxis chartValueAxis;
				switch (num)
				{
				case 0:
				{
					XlsChart xlsChart;
					chartCategoryAxis = xlsChart.PrimaryCategoryAxis;
					goto IL_F3;
				}
				case 1:
				{
					XlsChart xlsChart;
					chartCategoryAxis = xlsChart.SecondaryCategoryAxis;
					goto IL_F3;
				}
				case 2:
				{
					AxisType axisType;
					switch (axisType)
					{
					case AxisType.Category:
						num = 4;
						continue;
					case AxisType.Value:
						num = 11;
						continue;
					case AxisType.Serie:
					{
						XlsChart xlsChart;
						xlsChartAxis = (XlsChartAxis)xlsChart.PrimaryValueAxis;
						num = 9;
						continue;
					}
					default:
						num = 12;
						continue;
					}
					break;
				}
				case 3:
					goto IL_105;
				case 4:
				{
					bool isPrimary;
					if (!isPrimary)
					{
						num = 8;
						continue;
					}
					num = 7;
					continue;
				}
				case 5:
					goto IL_80;
				case 7:
				{
					if (true)
					{
					}
					XlsChart xlsChart;
					chartValueAxis = xlsChart.PrimaryValueAxis;
					goto IL_206;
				}
				case 8:
					num = 13;
					continue;
				case 9:
					goto IL_204;
				case 10:
					num = 1;
					continue;
				case 11:
				{
					bool isPrimary;
					if (!isPrimary)
					{
						num = 10;
						continue;
					}
					num = 0;
					continue;
				}
				case 12:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_71;
					}
					if (false)
					{
					}
					num = 15;
					continue;
				case 13:
				{
					XlsChart xlsChart;
					chartValueAxis = xlsChart.SecondaryValueAxis;
					goto IL_206;
				}
				case 14:
				{
					if (A_1 == null)
					{
						num = 17;
						continue;
					}
					XlsChart xlsChart = A_1.ParentXlsChart;
					bool isPrimary = A_1.IsPrimary;
					AxisType axisType = A_1.AxisType;
					num = 2;
					continue;
				}
				case 15:
					goto IL_B7;
				case 16:
					goto IL_218;
				case 17:
					goto IL_125;
				}
				IL_71:
				if (A_0 == null)
				{
					num = 5;
					continue;
				}
				num = 14;
				continue;
				IL_F3:
				xlsChartAxis = (XlsChartAxis)chartCategoryAxis;
				num = 3;
				continue;
				IL_206:
				xlsChartAxis = (XlsChartAxis)chartValueAxis;
				num = 16;
			}
			IL_80:
			throw new ArgumentNullException(RecordTableEnumerator.b("伷䠹唻䨽┿ぁ", a_));
			IL_B7:
			throw new InvalidOperationException();
			IL_105:
			goto IL_231;
			IL_125:
			throw new ArgumentNullException(RecordTableEnumerator.b("夷䈹唻䴽", a_));
			IL_204:
			IL_218:
			IL_231:
			spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("嬷䠹医䴽㌿́㱃", a_), xlsChartAxis.AxisId.ToString());
			return;
		}
		}
	}

	// Token: 0x06003EB7 RID: 16055 RVA: 0x0022CF5C File Offset: 0x0022BF5C
	private void ᜀ(XmlWriter A_0, string A_1, TickMarkType A_2)
	{
		int a_ = 19;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_89;
			case 1:
				if (A_1.Length == 0)
				{
					num = 0;
					continue;
				}
				goto IL_C3;
			case 2:
				if (A_1 != null)
				{
					num = 3;
					continue;
				}
				goto IL_89;
			case 3:
				num = 1;
				continue;
			case 4:
				if (true)
				{
				}
				break;
			case 5:
				goto IL_44;
			}
			if (A_0 == null)
			{
				num = 5;
				continue;
			}
			num = 2;
			continue;
			IL_89:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_9F;
			}
		}
		IL_44:
		throw new ArgumentNullException(RecordTableEnumerator.b("㹈㥊⑌㭎㑐⅒", a_));
		IL_9F:
		if (false)
		{
		}
		throw new ArgumentException(RecordTableEnumerator.b("㵈⩊⩌Ŏぐ㹒ご", a_));
		IL_C3:
		string a_2 = spr\u2433.ᜂ[A_2];
		spr\u1CFF.ᜀ(A_0, A_1, a_2);
	}

	// Token: 0x06003EB8 RID: 16056 RVA: 0x0022D040 File Offset: 0x0022C040
	private void ᜅ(XmlWriter A_0, XlsChartAxis A_1)
	{
		int a_ = 7;
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
					break;
				default:
					goto IL_54;
				}
				break;
			case 1:
				goto IL_3C;
			case 3:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				goto IL_A1;
			}
			IL_29:
			if (A_0 == null)
			{
				if (true)
				{
				}
				num = 1;
				continue;
			}
			num = 3;
			continue;
			goto IL_29;
		}
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨼䴾⡀㝂⁄㕆", a_));
		IL_54:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("尼䜾⡀あ", a_));
		IL_A1:
		string a_2 = spr\u2433.ᜁ[A_1.TickLabelPosition];
		spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("䤼嘾≀⡂ॄ╆╈ᭊ≌㱎", a_), a_2);
	}

	// Token: 0x06003EB9 RID: 16057 RVA: 0x0022D114 File Offset: 0x0022C114
	private void ᜄ(XmlWriter A_0, XlsChartAxis A_1)
	{
		int a_ = 4;
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
					break;
				default:
					goto IL_54;
				}
				break;
			case 2:
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				goto IL_A1;
			case 3:
				goto IL_3C;
			}
			IL_29:
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
			goto IL_29;
		}
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴹主圽㐿❁㙃", a_));
		IL_54:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("嬹䐻圽㌿", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("吹䤻匽ؿ⽁ぃ", a_), RecordTableEnumerator.b("刹䠻䨽〿硁歃楅㭇⥉⑋⭍㵏㍑❓硕㝗⩙㥛そᡟཡࡣeݧᡩū཭ѯű婳᥵੷ᵹ卻᩽뾏ꂑ꒓ꚕ꺗떙ﾛ솟킡킣", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("尹医䰽ⴿ⍁ぃՅ❇⹉⥋", a_), A_1.NumberFormat);
		bool a_2 = A_1.IsSourceLinked;
		spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("䤹医䬽㈿⅁⅃੅ⅇ⑉❋⭍㑏", a_), a_2, false);
		A_0.WriteEndElement();
	}

	// Token: 0x06003EBA RID: 16058 RVA: 0x0022D224 File Offset: 0x0022C224
	private void ᜃ(XmlWriter A_0, XlsChartAxis A_1)
	{
		int a_ = 17;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1.HasMinorGridLines)
				{
					num = 6;
					continue;
				}
				return;
			case 1:
			{
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				XlsWorkbook parentWorkbook = A_1.ParentXlsChart.ParentWorkbook;
				num = 8;
				continue;
			}
			case 2:
				goto IL_F3;
			case 4:
				return;
			case 5:
				goto IL_59;
			case 6:
			{
				XlsWorkbook parentWorkbook;
				this.ᜀ(A_0, A_1.MinorGridLines, RecordTableEnumerator.b("⩆⁈╊≌㵎ᙐ⅒㱔㍖㕘㉚㍜㩞በ", a_), parentWorkbook);
				num = 4;
				continue;
			}
			case 7:
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
					XlsWorkbook parentWorkbook;
					this.ᜀ(A_0, A_1.MajorGridLines, RecordTableEnumerator.b("⩆⡈⅊≌㵎ᙐ⅒㱔㍖㕘㉚㍜㩞በ", a_), parentWorkbook);
					num = 9;
					continue;
				}
				}
				break;
			case 8:
				if (A_1.HasMajorGridLines)
				{
					num = 7;
					continue;
				}
				goto IL_132;
			case 9:
				goto IL_132;
			}
			IL_4B:
			if (A_0 == null)
			{
				num = 5;
				continue;
			}
			num = 1;
			continue;
			goto IL_4B;
			IL_132:
			num = 0;
		}
		IL_59:
		throw new ArgumentNullException(RecordTableEnumerator.b("う㭈≊㥌⩎⍐", a_));
		IL_F3:
		throw new ArgumentNullException(RecordTableEnumerator.b("♆ㅈ≊㹌", a_));
	}

	// Token: 0x06003EBB RID: 16059 RVA: 0x0022D398 File Offset: 0x0022C398
	private void ᜀ(XmlWriter A_0, IChartGridLine A_1, string A_2, IWorkbook A_3)
	{
		int a_ = 17;
		int num = 9;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 12;
				continue;
			case 1:
			{
				A_0.WriteStartElement(RecordTableEnumerator.b("㑆㥈ᭊ㽌", a_), RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒♔㑖ㅘ㹚ぜ㹞በ䵢੤ᝦ౨ժᕬɮᵰᕲᩴնᑸ᩺ॼ౾꾀Ꚉﾌ朗ﮔ늜궞醠鎢鎤袦쪨쎪첬\uddae얰", a_));
				IChartBorder border;
				spr\u1CFF.ᜀ(A_0, border, A_3);
				A_0.WriteEndElement();
				num = 3;
				continue;
			}
			case 2:
				num = 8;
				continue;
			case 3:
				goto IL_18A;
			case 4:
				if (A_1 == null)
				{
					num = 10;
					continue;
				}
				num = 11;
				continue;
			case 5:
			{
				IChartBorder border;
				if (border != null)
				{
					num = 0;
					continue;
				}
				goto IL_1BE;
			}
			case 6:
				goto IL_D6;
			case 7:
				goto IL_5B;
			case 8:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_FE;
				default:
				{
					if (false)
					{
					}
					if (A_2.Length == 0)
					{
						if (true)
						{
						}
						num = 6;
						continue;
					}
					A_0.WriteStartElement(A_2, RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒♔㑖ㅘ㹚ぜ㹞በ䵢੤ᝦ౨ժᕬɮᵰᕲᩴնᑸ᩺ॼ౾꾀Ꚉﾌ朗ﮔ늜궞醠鎢鎤袦쪨쎪첬\uddae얰", a_));
					IChartBorder border = A_1.Border;
					num = 5;
					continue;
				}
				}
				break;
			case 10:
				goto IL_FE;
			case 11:
				if (A_2 != null)
				{
					num = 2;
					continue;
				}
				goto IL_13A;
			case 12:
			{
				IChartBorder border;
				if (!border.UseDefaultFormat)
				{
					num = 1;
					continue;
				}
				goto IL_1BE;
			}
			}
			if (A_0 == null)
			{
				num = 7;
			}
			else
			{
				num = 4;
			}
		}
		IL_5B:
		throw new ArgumentNullException(RecordTableEnumerator.b("う㭈≊㥌⩎⍐", a_));
		IL_D6:
		goto IL_13A;
		IL_FE:
		throw new ArgumentNullException(RecordTableEnumerator.b("⁆㭈≊⥌͎㡐㵒ご⑖", a_));
		IL_13A:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㍆⡈ⱊ͌⹎㱐㙒", a_));
		IL_18A:
		IL_1BE:
		A_0.WriteEndElement();
	}

	// Token: 0x06003EBC RID: 16060 RVA: 0x0022D56C File Offset: 0x0022C56C
	private void ᜂ(XmlWriter A_0, XlsChartAxis A_1)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 11;
			for (;;)
			{
				string text;
				string text2;
				string text3;
				string text4;
				string text5;
				switch (num)
				{
				case 0:
				{
					if (A_1 == null)
					{
						num = 2;
						continue;
					}
					ChartAxisPos? chartAxisPos = A_1.AxisPosition;
					num = 13;
					continue;
				}
				case 1:
					text = RecordTableEnumerator.b("㉅", a_);
					goto IL_183;
				case 2:
					goto IL_E2;
				case 3:
					num = 7;
					continue;
				case 4:
				{
					AxisType axisType = A_1.AxisType;
					num = 18;
					continue;
				}
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_30B;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 6:
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("❅ぇᩉ⍋㵍", a_), text2);
					num = 15;
					continue;
				case 7:
					goto IL_27F;
				case 8:
					if (!A_1.IsPrimary)
					{
						num = 25;
						continue;
					}
					num = 19;
					continue;
				case 9:
					goto IL_27F;
				case 10:
					if (!A_1.IsPrimary)
					{
						num = 5;
						continue;
					}
					num = 27;
					continue;
				case 12:
					goto IL_27F;
				case 13:
				{
					ChartAxisPos? chartAxisPos;
					if (chartAxisPos == null)
					{
						num = 22;
						continue;
					}
					ChartAxisPos? chartAxisPos2 = A_1.AxisPosition;
					num = 14;
					continue;
				}
				case 14:
				{
					ChartAxisPos? chartAxisPos2;
					text3 = chartAxisPos2.ToString();
					goto IL_19F;
				}
				case 15:
					return;
				case 16:
					goto IL_27F;
				case 17:
					if (text2 != null)
					{
						num = 6;
						continue;
					}
					return;
				case 18:
				{
					AxisType axisType;
					switch (axisType)
					{
					case AxisType.Category:
						num = 28;
						continue;
					case AxisType.Value:
						num = 8;
						continue;
					case AxisType.Serie:
						num = 10;
						continue;
					}
					goto IL_30B;
				}
				case 19:
					text4 = RecordTableEnumerator.b("⩅", a_);
					goto IL_344;
				case 20:
					text4 = RecordTableEnumerator.b("㑅", a_);
					goto IL_344;
				case 21:
					text5 = RecordTableEnumerator.b("⑅", a_);
					goto IL_356;
				case 22:
					num = 30;
					continue;
				case 23:
					text5 = RecordTableEnumerator.b("㉅", a_);
					goto IL_356;
				case 24:
					num = 23;
					continue;
				case 25:
					num = 20;
					continue;
				case 26:
					if (text2 == null)
					{
						num = 4;
						continue;
					}
					goto IL_27F;
				case 27:
					text = RecordTableEnumerator.b("⑅", a_);
					goto IL_183;
				case 28:
					if (!A_1.IsPrimary)
					{
						num = 24;
						continue;
					}
					num = 21;
					continue;
				case 29:
					goto IL_BF;
				case 30:
					text3 = null;
					goto IL_19F;
				}
				if (true)
				{
				}
				if (A_0 == null)
				{
					num = 29;
					continue;
				}
				num = 0;
				continue;
				IL_183:
				text2 = text;
				num = 9;
				continue;
				IL_19F:
				text2 = text3;
				num = 26;
				continue;
				IL_27F:
				num = 17;
				continue;
				IL_30B:
				num = 3;
				continue;
				IL_344:
				text2 = text4;
				num = 12;
				continue;
				IL_356:
				text2 = text5;
				num = 16;
			}
			IL_BF:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅅ㩇⍉㡋⭍≏", a_));
			IL_E2:
			throw new ArgumentNullException(RecordTableEnumerator.b("ぅ⥇♉㥋⭍ᅏ⩑㵓╕", a_));
		}
		}
	}

	// Token: 0x06003EBD RID: 16061 RVA: 0x0022D928 File Offset: 0x0022C928
	private void ᜁ(XmlWriter A_0, XlsChartAxis A_1)
	{
		int a_ = 1;
		int num = 19;
		for (;;)
		{
			string text;
			switch (num)
			{
			case 0:
			{
				if (true)
				{
				}
				XlsChartValueAxis xlsChartValueAxis;
				if (!xlsChartValueAxis.IsAutoMax)
				{
					num = 1;
					continue;
				}
				goto IL_12E;
			}
			case 1:
			{
				XlsChartValueAxis xlsChartValueAxis;
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("娶堸䌺", a_), XmlConvert.ToString(xlsChartValueAxis.MaxValue));
				num = 8;
				continue;
			}
			case 2:
				goto IL_91;
			case 3:
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("嬶嘸尺缼帾㉀♂", a_), XmlConvert.ToString(10));
				num = 2;
				continue;
			case 4:
				goto IL_7B;
			case 5:
				text = RecordTableEnumerator.b("娶堸䌺瀼嘾⽀", a_);
				goto IL_286;
			case 6:
				goto IL_1AA;
			case 7:
			{
				XlsChartValueAxis xlsChartValueAxis;
				spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("娶倸唺", a_), XmlConvert.ToString(xlsChartValueAxis.MinValue));
				num = 6;
				continue;
			}
			case 8:
				goto IL_12E;
			case 9:
				num = 11;
				continue;
			case 10:
			{
				XlsChartValueAxis xlsChartValueAxis;
				if (xlsChartValueAxis != null)
				{
					num = 16;
					continue;
				}
				goto IL_91;
			}
			case 11:
				text = RecordTableEnumerator.b("娶倸唺瀼帾㥀", a_);
				goto IL_286;
			case 12:
				if (!A_1.IsReversed)
				{
					num = 9;
					continue;
				}
				num = 5;
				continue;
			case 13:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2AA;
				default:
					if (false)
					{
					}
					num = 0;
					continue;
				}
				break;
			case 14:
			{
				XlsChartValueAxis xlsChartValueAxis;
				if (xlsChartValueAxis != null)
				{
					goto IL_2AA;
				}
				goto IL_2BA;
			}
			case 15:
				goto IL_17E;
			case 16:
				num = 17;
				continue;
			case 17:
			{
				XlsChartValueAxis xlsChartValueAxis;
				if (xlsChartValueAxis.IsLogScale)
				{
					num = 3;
					continue;
				}
				goto IL_91;
			}
			case 18:
			{
				if (A_1 == null)
				{
					num = 15;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("䐶娸娺儼嘾⽀⑂", a_), RecordTableEnumerator.b("弶䴸伺䴼Ծ湀求㙄⑆ⅈ⹊⁌⹎≐絒㩔❖㱘㕚╜㉞ൠբ੤ᕦѨ੪ᥬᱮ彰ᱲݴၶ噸ὺོṾꊌ붎ꆐꎒꎔ뢖滛ﲜ햠", a_));
				XlsChartValueAxis xlsChartValueAxis = A_1 as XlsChartValueAxis;
				num = 10;
				continue;
			}
			case 20:
			{
				XlsChartValueAxis xlsChartValueAxis;
				if (!xlsChartValueAxis.IsAutoMin)
				{
					num = 7;
					continue;
				}
				goto IL_2BA;
			}
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			num = 18;
			continue;
			IL_91:
			num = 12;
			continue;
			IL_12E:
			num = 20;
			continue;
			IL_286:
			string a_2 = text;
			spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("堶䬸刺堼儾㕀≂ㅄ⹆♈╊", a_), a_2);
			num = 14;
			continue;
			IL_2AA:
			num = 13;
		}
		IL_7B:
		throw new ArgumentNullException(RecordTableEnumerator.b("䀶䬸刺䤼娾㍀", a_));
		IL_17E:
		throw new ArgumentNullException(RecordTableEnumerator.b("嘶䄸刺丼", a_));
		IL_1AA:
		IL_2BA:
		A_0.WriteEndElement();
	}

	// Token: 0x06003EBE RID: 16062 RVA: 0x0022DBF8 File Offset: 0x0022CBF8
	private void ᜀ(XmlWriter A_0, XlsChartValueAxis A_1, RelationsCollection A_2)
	{
		int a_ = 11;
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				ChartDisplayUnitType displayUnit;
				bool hasDisplayUnitLabel;
				switch (num)
				{
				case 0:
				{
					string a_2 = ((XLSXChartDisplayUnit)displayUnit).ToString();
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("⍀㙂ⱄ⭆㵈Ɋ⍌ᩎ㽐㩒⅔", a_), a_2);
					num = 11;
					continue;
				}
				case 1:
					goto IL_189;
				case 3:
					goto IL_70;
				case 4:
					goto IL_195;
				case 5:
					goto IL_1BE;
				case 6:
				{
					XlsChart xlsChart = A_1.ParentXlsChart;
					XlsWorkbook parentWorkbook = xlsChart.ParentWorkbook;
					XlsChartTextArea xlsChartTextArea = A_1.DisplayUnitLabel as XlsChartTextArea;
					XlsChartTextArea xlsChartTextArea2 = xlsChartTextArea;
					A_0.WriteStartElement(RecordTableEnumerator.b("╀⩂㙄㝆᱈╊⑌㭎≐ὒ㝔㭖", a_), RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂ﲊﺒ璉뢖ꮘꮚ궜ꦞ躠삢춤욦\udba8\udfaa", a_));
					spr\u1CFF.ᜀ(A_0, xlsChartTextArea.FrameFormat, xlsChart, false);
					num = 8;
					continue;
				}
				case 7:
				{
					XlsWorkbook parentWorkbook;
					XlsChartTextArea xlsChartTextArea;
					this.ᜀ(A_0, parentWorkbook, xlsChartTextArea, false, 0);
					num = 9;
					continue;
				}
				case 8:
				{
					XlsChartTextArea xlsChartTextArea2;
					if (xlsChartTextArea2.ParagraphType == ChartParagraphType.Default)
					{
						num = 7;
						continue;
					}
					goto IL_1AC;
				}
				case 9:
					goto IL_1AC;
				case 10:
					if (displayUnit == ChartDisplayUnitType.None)
					{
						num = 12;
						continue;
					}
					hasDisplayUnitLabel = A_1.HasDisplayUnitLabel;
					A_0.WriteStartElement(RecordTableEnumerator.b("╀⩂㙄㝆᱈╊⑌㭎≐", a_), RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂ﲊﺒ璉뢖ꮘꮚ궜ꦞ躠삢춤욦\udba8\udfaa", a_));
					num = 13;
					continue;
				case 11:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_195;
					default:
						if (false)
						{
						}
						goto IL_189;
					}
					break;
				case 12:
					return;
				case 13:
				{
					if (displayUnit != ChartDisplayUnitType.Custom)
					{
						num = 0;
						continue;
					}
					double displayUnitCustom = A_1.DisplayUnitCustom;
					string a_2 = XmlConvert.ToString(displayUnitCustom);
					spr\u1CFF.ᜀ(A_0, RecordTableEnumerator.b("≀㙂㙄㍆᱈╊⑌㭎", a_), a_2);
					num = 1;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 3;
					continue;
				}
				if (true)
				{
				}
				displayUnit = A_1.DisplayUnit;
				num = 10;
				continue;
				IL_189:
				num = 4;
				continue;
				IL_195:
				if (hasDisplayUnitLabel)
				{
					num = 6;
					continue;
				}
				goto IL_260;
				IL_1AC:
				A_0.WriteEndElement();
				num = 5;
			}
			IL_70:
			throw new ArgumentNullException(RecordTableEnumerator.b("㙀ㅂⱄ㍆ⱈ㥊", a_));
			IL_1BE:
			IL_260:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06003EBF RID: 16063 RVA: 0x0022DE6C File Offset: 0x0022CE6C
	private void ᜀ(XmlWriter A_0, XlsChartAxis A_1)
	{
		int a_ = 10;
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
				break;
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("ℿ㩁ⵃ㕅", a_));
		}
		if (true)
		{
		}
		this.ᜀ(A_0, A_1.ParentXlsChart.ParentWorkbook, A_1.Font, A_1.IsAutoTextRotation, A_1.TextRotationAngle);
	}

	// Token: 0x06003EC0 RID: 16064 RVA: 0x0022DEF0 File Offset: 0x0022CEF0
	private void ᜀ(XmlWriter A_0, IWorkbook A_1, IFont A_2, bool A_3, int A_4)
	{
		int a_ = 10;
		int num = 2;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				goto IL_66;
			case 1:
				if (!A_3)
				{
					num = 3;
					continue;
				}
				goto IL_113;
			case 3:
			{
				int num2 = A_4 * 60000;
				A_0.WriteAttributeString(RecordTableEnumerator.b("㈿ⵁぃ", a_), num2.ToString());
				goto IL_8F;
			}
			case 4:
				goto IL_9A;
			}
			if (A_0 != null)
			{
				A_0.WriteStartElement(RecordTableEnumerator.b("㐿㩁ᑃ㑅", a_), RecordTableEnumerator.b("⠿㙁ぃ㙅片敉捋㵍㍏㩑ㅓ㭕㥗⥙牛ㅝၟݡ੣ṥէ٩੫ŭɯάᕳɵ୷呹፻౽궁ﶉﾑ릕ꪗꪙ겛ꢝ辟송첣장\udaa7\udea9", a_));
				A_0.WriteStartElement(RecordTableEnumerator.b("∿ⵁ⁃㽅ᡇ㡉", a_), RecordTableEnumerator.b("⠿㙁ぃ㙅片敉捋㵍㍏㩑ㅓ㭕㥗⥙牛ㅝၟݡ੣ṥէ٩੫ŭɯάᕳɵ୷呹፻౽궁ﶉﾑ릕ꪗꪙ겛ꢝ辟쾡얣쾥욧", a_));
				num = 1;
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
			IL_8F:
			num = 4;
		}
		IL_66:
		throw new ArgumentNullException(RecordTableEnumerator.b("㜿ぁⵃ㉅ⵇ㡉", a_));
		IL_9A:
		IL_113:
		A_0.WriteEndElement();
		A_0.WriteStartElement(RecordTableEnumerator.b("〿", a_), RecordTableEnumerator.b("⠿㙁ぃ㙅片敉捋㵍㍏㩑ㅓ㭕㥗⥙牛ㅝၟݡ੣ṥէ٩੫ŭɯάᕳɵ୷呹፻౽궁ﶉﾑ릕ꪗꪙ겛ꢝ辟쾡얣쾥욧", a_));
		A_0.WriteStartElement(RecordTableEnumerator.b("〿ቁ㙃", a_), RecordTableEnumerator.b("⠿㙁ぃ㙅片敉捋㵍㍏㩑ㅓ㭕㥗⥙牛ㅝၟݡ੣ṥէ٩੫ŭɯάᕳɵ୷呹፻౽궁ﶉﾑ릕ꪗꪙ겛ꢝ辟쾡얣쾥욧", a_));
		spr\u1CFF.ᜀ(A_0, A_2, RecordTableEnumerator.b("␿❁≃ᑅᡇ㡉", a_), A_1, 10.0);
		A_0.WriteEndElement();
		A_0.WriteEndElement();
		A_0.WriteEndElement();
	}

	// Token: 0x04001C85 RID: 7301
	internal const int ᜀ = 60000;

	// Token: 0x04001C86 RID: 7302
	private static Dictionary<TickLabelPositionType, string> ᜁ;

	// Token: 0x04001C87 RID: 7303
	private static Dictionary<TickMarkType, string> ᜂ;
}
