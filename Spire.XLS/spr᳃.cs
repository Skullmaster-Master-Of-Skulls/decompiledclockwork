using System;
using System.Collections.Generic;
using System.Xml;
using Spire.Xls;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200049C RID: 1180
internal class spr\u1CC3 : spr\u1B7A
{
	// Token: 0x060048FD RID: 18685 RVA: 0x002C5984 File Offset: 0x002C4984
	public override ExcelVersion ᜀ()
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
		return ExcelVersion.Version2010;
	}

	// Token: 0x060048FE RID: 18686 RVA: 0x002C59C0 File Offset: 0x002C49C0
	public spr\u1CC3(XlsWorkbook A_0) : base(A_0)
	{
	}

	// Token: 0x060048FF RID: 18687 RVA: 0x002C59D4 File Offset: 0x002C49D4
	protected override void ᜁ(XmlWriter A_0, XlsWorksheet A_1)
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
		this.ᜀ(A_0, A_1);
	}

	// Token: 0x06004900 RID: 18688 RVA: 0x002C5A18 File Offset: 0x002C4A18
	public void ᜀ(XmlWriter A_0, XlsWorksheet A_1)
	{
		int a_ = 17;
		int num = 4;
		for (;;)
		{
			List<ISparklineGroup>.Enumerator enumerator;
			switch (num)
			{
			case 0:
				goto IL_43;
			case 1:
				goto IL_168;
			case 2:
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				goto IL_204;
			case 3:
				goto IL_20F;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_20F;
				default:
					goto IL_240;
				}
				break;
			case 6:
				try
				{
					num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 1;
							continue;
						case 1:
							goto IL_1F4;
						case 2:
						{
							if (!enumerator.MoveNext())
							{
								num = 0;
								continue;
							}
							SparklineGroup a_2 = (SparklineGroup)enumerator.Current;
							this.ᜀ(A_0, A_1, a_2);
							num = 3;
							continue;
						}
						}
						IL_1D1:
						num = 2;
						continue;
						goto IL_1D1;
					}
					IL_1F4:
					goto IL_267;
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
				goto IL_204;
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			num = 2;
			continue;
			IL_20F:
			if (A_1.SparklineGroups.Count == 0)
			{
				num = 5;
				continue;
			}
			A_0.WriteStartElement(RecordTableEnumerator.b("≆ㅈ㽊Ō㱎═", a_), RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒♔㑖ㅘ㹚ぜ㹞በ䵢੤ᝦ౨ժᕬɮᵰᕲᩴնᑸ᩺ॼ౾꾀Ꚉﶌﶎﺚ철쾢誤閦馨鮪鮬肮\udcb0튲\udcb4\ud9b6", a_));
			A_0.WriteStartElement(RecordTableEnumerator.b("≆ㅈ㽊", a_), RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒♔㑖ㅘ㹚ぜ㹞በ䵢੤ᝦ౨ժᕬɮᵰᕲᩴնᑸ᩺ॼ౾꾀Ꚉﶌﶎﺚ철쾢誤閦馨鮪鮬肮\udcb0튲\udcb4\ud9b6", a_));
			A_0.WriteAttributeString(RecordTableEnumerator.b("㉆㭈≊", a_), RecordTableEnumerator.b("㱆祈繊์祎慐晒晔扖瑘橚᭜湞坠乢兤Ŧ൨奪䁬⵮䝰䁲䙴婶㽸佺㭼䱾란얂떄얆뾈뾊좌뾎", a_));
			A_0.WriteAttributeString(RecordTableEnumerator.b("㽆⑈❊⍌㱎", a_), RecordTableEnumerator.b("㽆硈罊", a_), null, RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒♔㑖ㅘ㹚ぜ㹞በ䵢ࡤ๦੨ᥪɬᱮṰᕲŴ奶᩸ᑺၼ偾ꊌﲎﶘ爵쒠힢좤쮦蚨馪鶬龮袰鲲貴颶풸\udaba풼톾", a_));
			A_0.WriteStartElement(RecordTableEnumerator.b("㑆㥈⩊㽌⑎㵐㩒㭔㉖Ṙ⥚㉜⩞ᅠၢ", a_), RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒♔㑖ㅘ㹚ぜ㹞በ䵢ࡤ๦੨ᥪɬᱮṰᕲŴ奶᩸ᑺၼ偾ꊌﲎﶘ爵쒠힢좤쮦蚨馪鶬龮袰鲲貴颶풸\udaba풼톾", a_));
			A_0.WriteAttributeString(RecordTableEnumerator.b("㽆⑈❊⍌㱎", a_), RecordTableEnumerator.b("㽆⑈", a_), null, RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒♔㑖ㅘ㹚ぜ㹞በ䵢ࡤ๦੨ᥪɬᱮṰᕲŴ奶᩸ᑺၼ偾ꊌﮖ뚘ꦚ궜꾞鞠貢좤욦삨얪", a_));
			enumerator = A_1.SparklineGroups.GetEnumerator();
			num = 6;
			continue;
			IL_204:
			num = 3;
		}
		IL_43:
		throw new ArgumentNullException(RecordTableEnumerator.b("う㭈≊㥌⩎⍐", a_));
		IL_168:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑆ⅈ⹊⡌㭎", a_));
		IL_240:
		if (true)
		{
		}
		if (false)
		{
		}
		return;
		IL_267:
		A_0.WriteEndElement();
		A_0.WriteEndElement();
		A_0.WriteEndElement();
	}

	// Token: 0x06004901 RID: 18689 RVA: 0x002C5CB0 File Offset: 0x002C4CB0
	private void ᜀ(XmlWriter A_0, XlsWorksheet A_1, SparklineGroup A_2)
	{
		int a_ = 7;
		switch (0)
		{
		default:
		{
			int num = 33;
			for (;;)
			{
				List<ISparklines>.Enumerator enumerator;
				OColor ocolor;
				SparklineType sparklineType;
				SparklineEmptyCells emptyCellsType;
				switch (num)
				{
				case 0:
					goto IL_472;
				case 1:
					num = 7;
					continue;
				case 2:
					goto IL_8F1;
				case 3:
					if (A_2.VerticalAxisMinimum != null)
					{
						num = 15;
						continue;
					}
					goto IL_472;
				case 4:
					A_0.WriteAttributeString(RecordTableEnumerator.b("夼帾㕀♂ф㽆⁈㡊", a_), RecordTableEnumerator.b("఼", a_));
					num = 54;
					continue;
				case 5:
					goto IL_8F1;
				case 6:
					A_0.WriteStartElement(RecordTableEnumerator.b("嬼", a_), RecordTableEnumerator.b("唼䬾㕀㍂罄框晈㡊⹌❎㑐㹒㑔⑖睘㙚㑜㱞፠ౢᙤࡦཨὪ䍬౮ṰṲ婴ᡶὸᵺᑼ᱾겂ﾆꂎꎐꎒꖔꆖ뚘ﲜ쾠", a_));
					A_0.WriteString(A_2.HorizontalDateAxisRange.RangeAddress.Replace(RecordTableEnumerator.b("ᨼ", a_), ""));
					A_0.WriteEndElement();
					num = 18;
					continue;
				case 7:
					if (A_2.SparklineType == SparklineType.Line)
					{
						num = 50;
						continue;
					}
					goto IL_D84;
				case 8:
					num = 69;
					continue;
				case 9:
					if (A_2.ShowLowPoint)
					{
						num = 57;
						continue;
					}
					goto IL_EED;
				case 10:
					if (A_2.VerticalAxisMaximum.ᜁ() == SpartlineVerticalAxisType.Custom)
					{
						num = 11;
						continue;
					}
					goto IL_A01;
				case 11:
					A_0.WriteAttributeString(RecordTableEnumerator.b("值帾⽀㙂⑄⭆ш⩊㕌", a_), A_2.VerticalAxisMaximum.ᜀ().ToString());
					num = 25;
					continue;
				case 12:
					if (A_2.VerticalAxisMaximum != null)
					{
						num = 84;
						continue;
					}
					goto IL_A01;
				case 13:
					A_0.WriteAttributeString(RecordTableEnumerator.b("嬼嘾㍀あㅄ", a_), RecordTableEnumerator.b("఼", a_));
					num = 64;
					continue;
				case 14:
					if (A_2.ShowHorizontalAxis)
					{
						num = 17;
						continue;
					}
					goto IL_F84;
				case 15:
					num = 76;
					continue;
				case 16:
					goto IL_D84;
				case 17:
					A_0.WriteAttributeString(RecordTableEnumerator.b("夼嘾㉀㍂⥄♆えፊౌ㝎㡐⁒", a_), RecordTableEnumerator.b("఼", a_));
					num = 86;
					continue;
				case 18:
					goto IL_F48;
				case 19:
					goto IL_651;
				case 20:
					if (A_2.SparklineType == SparklineType.Line)
					{
						num = 63;
						continue;
					}
					goto IL_B2B;
				case 21:
					goto IL_6DF;
				case 22:
					goto IL_436;
				case 23:
					A_0.WriteAttributeString(RecordTableEnumerator.b("夼嘾㉀㍂⥄♆え͊⑌⭎㕐㙒㭔", a_), RecordTableEnumerator.b("఼", a_));
					num = 27;
					continue;
				case 24:
					if (A_2.HorizontalDateAxisRange != null)
					{
						num = 6;
						continue;
					}
					goto IL_F48;
				case 25:
					goto IL_A01;
				case 26:
					if (A_2.ShowNegativePoint)
					{
						num = 75;
						continue;
					}
					goto IL_6DF;
				case 27:
					goto IL_19F;
				case 28:
					if (A_2.HorizontalDateAxisRange != null)
					{
						num = 4;
						continue;
					}
					goto IL_9CC;
				case 29:
					num = 68;
					continue;
				case 30:
				{
					SpartlineVerticalAxisType spartlineVerticalAxisType;
					switch (spartlineVerticalAxisType)
					{
					case SpartlineVerticalAxisType.Automatic:
						goto IL_651;
					case SpartlineVerticalAxisType.Same:
						A_0.WriteAttributeString(RecordTableEnumerator.b("值帾㥀ɂ㵄⹆㩈Ὂ㑌㽎㑐", a_), RecordTableEnumerator.b("娼䴾⹀㙂㕄", a_));
						num = 46;
						continue;
					case SpartlineVerticalAxisType.Custom:
						A_0.WriteAttributeString(RecordTableEnumerator.b("值帾㥀ɂ㵄⹆㩈Ὂ㑌㽎㑐", a_), RecordTableEnumerator.b("帼䨾㉀㝂⩄⩆", a_));
						num = 19;
						continue;
					default:
						num = 37;
						continue;
					}
					break;
				}
				case 31:
					num = 28;
					continue;
				case 32:
					if (A_2 == null)
					{
						num = 85;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("丼伾⁀ㅂ⹄⭆⁈╊⡌ࡎ⍐㱒⁔❖", a_), RecordTableEnumerator.b("唼䬾㕀㍂罄框晈㡊⹌❎㑐㹒㑔⑖睘㙚㑜㱞፠ౢᙤࡦཨὪ䍬౮ṰṲ婴ᡶὸᵺᑼ᱾겂ﮈﮒ낞鎠鎢閤麦蚨銪芬슮킰\udab2\udbb4", a_));
					num = 12;
					continue;
				case 34:
					A_0.WriteAttributeString(RecordTableEnumerator.b("儼帾㉀㝂", a_), RecordTableEnumerator.b("఼", a_));
					num = 36;
					continue;
				case 35:
					A_0.WriteAttributeString(RecordTableEnumerator.b("儼嘾⽀♂ቄ≆⁈ⱊ╌㭎", a_), XmlConvert.ToString(A_2.LineWeight));
					num = 81;
					continue;
				case 36:
					goto IL_D5C;
				case 37:
					num = 67;
					continue;
				case 38:
					A_0.WriteAttributeString(RecordTableEnumerator.b("夼嘾㉀㍂⥄♆え๊⁌㽎═⩒ᙔ㉖㕘㝚⹜Ṟበ", a_), RecordTableEnumerator.b("丼伾⁀ⵂ", a_));
					num = 59;
					continue;
				case 39:
					if (A_2.PlotRightToLeft)
					{
						num = 60;
						continue;
					}
					goto IL_4F9;
				case 40:
					if (A_2.IsDisplayHidden)
					{
						num = 23;
						continue;
					}
					goto IL_19F;
				case 41:
					if (A_2.VerticalAxisMinimum != null)
					{
						num = 53;
						continue;
					}
					goto IL_8F1;
				case 42:
					goto IL_B2B;
				case 43:
					if (A_2.ShowLastPoint)
					{
						num = 34;
						continue;
					}
					goto IL_D5C;
				case 44:
					A_0.WriteAttributeString(RecordTableEnumerator.b("唼嘾♀⭂", a_), RecordTableEnumerator.b("఼", a_));
					num = 73;
					continue;
				case 45:
					try
					{
						num = 3;
						for (;;)
						{
							switch (num)
							{
							case 1:
								goto IL_423;
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
								SparklineCollection a_2 = (SparklineCollection)enumerator.Current;
								this.ᜀ(A_0, A_1, a_2);
								num = 0;
								continue;
							}
							}
							IL_3DC:
							num = 4;
							continue;
							goto IL_3DC;
						}
						IL_423:
						goto IL_FAC;
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
					goto IL_436;
				case 46:
					goto IL_651;
				case 47:
					goto IL_7CD;
				case 48:
					if (A_2.IsHorizontalDateAxis)
					{
						num = 31;
						continue;
					}
					goto IL_9CC;
				case 49:
					if (A_2.VerticalAxisMaximum != null)
					{
						num = 58;
						continue;
					}
					goto IL_651;
				case 50:
					A_0.WriteAttributeString(RecordTableEnumerator.b("值帾㍀⡂⁄㕆㩈", a_), RecordTableEnumerator.b("఼", a_));
					num = 16;
					continue;
				case 51:
					goto IL_965;
				case 52:
					goto IL_4F9;
				case 53:
				{
					SpartlineVerticalAxisType spartlineVerticalAxisType2 = A_2.VerticalAxisMinimum.ᜁ();
					num = 61;
					continue;
				}
				case 54:
					goto IL_9CC;
				case 55:
					goto IL_19A;
				case 56:
					if (A_1 == null)
					{
						num = 51;
						continue;
					}
					num = 32;
					continue;
				case 57:
					A_0.WriteAttributeString(RecordTableEnumerator.b("儼倾㙀", a_), RecordTableEnumerator.b("఼", a_));
					num = 79;
					continue;
				case 58:
				{
					SpartlineVerticalAxisType spartlineVerticalAxisType = A_2.VerticalAxisMaximum.ᜁ();
					num = 30;
					continue;
				}
				case 59:
					goto IL_436;
				case 60:
					A_0.WriteAttributeString(RecordTableEnumerator.b("似嘾♀⭂ㅄፆ♈݊⡌⥎═", a_), RecordTableEnumerator.b("఼", a_));
					num = 52;
					continue;
				case 61:
				{
					SpartlineVerticalAxisType spartlineVerticalAxisType2;
					switch (spartlineVerticalAxisType2)
					{
					case SpartlineVerticalAxisType.Automatic:
						goto IL_8F1;
					case SpartlineVerticalAxisType.Same:
						A_0.WriteAttributeString(RecordTableEnumerator.b("值嘾⽀ɂ㵄⹆㩈Ὂ㑌㽎㑐", a_), RecordTableEnumerator.b("娼䴾⹀㙂㕄", a_));
						num = 2;
						continue;
					case SpartlineVerticalAxisType.Custom:
						A_0.WriteAttributeString(RecordTableEnumerator.b("值嘾⽀ɂ㵄⹆㩈Ὂ㑌㽎㑐", a_), RecordTableEnumerator.b("帼䨾㉀㝂⩄⩆", a_));
						num = 5;
						continue;
					default:
						num = 29;
						continue;
					}
					break;
				}
				case 62:
					goto IL_436;
				case 63:
					A_0.WriteStartElement(RecordTableEnumerator.b("帼倾ⵀⱂ㝄੆⡈㥊♌⩎⍐⁒", a_), RecordTableEnumerator.b("唼䬾㕀㍂罄框晈㡊⹌❎㑐㹒㑔⑖睘㙚㑜㱞፠ౢᙤࡦཨὪ䍬౮ṰṲ婴ᡶὸᵺᑼ᱾겂ﮈﮒ낞鎠鎢閤麦蚨銪芬슮킰\udab2\udbb4", a_));
					ocolor = new OColor(A_2.MarkersColor);
					A_0.WriteAttributeString(RecordTableEnumerator.b("似堾⍀", a_), ocolor.Value.ToString(RecordTableEnumerator.b("攼ा", a_)));
					A_0.WriteEndElement();
					num = 42;
					continue;
				case 64:
					goto IL_1C7;
				case 65:
					A_0.WriteAttributeString(RecordTableEnumerator.b("值帾⽀㙂⑄⭆ш≊⍌", a_), A_2.VerticalAxisMinimum.ᜀ().ToString());
					num = 0;
					continue;
				case 66:
					goto IL_7CD;
				case 67:
					goto IL_651;
				case 68:
					goto IL_8F1;
				case 69:
					goto IL_436;
				case 70:
					if (A_2.ShowFirstPoint)
					{
						num = 13;
						continue;
					}
					goto IL_1C7;
				case 71:
					switch (sparklineType)
					{
					case SparklineType.Stacked:
						A_0.WriteAttributeString(RecordTableEnumerator.b("䤼䘾ㅀ♂", a_), RecordTableEnumerator.b("丼䬾⁀⁂⹄≆ⵈ", a_));
						num = 47;
						continue;
					case SparklineType.Column:
						A_0.WriteAttributeString(RecordTableEnumerator.b("䤼䘾ㅀ♂", a_), RecordTableEnumerator.b("帼倾ⵀ㙂⡄⥆", a_));
						num = 66;
						continue;
					case SparklineType.Line:
						goto IL_7CD;
					default:
						num = 82;
						continue;
					}
					break;
				case 72:
					switch (emptyCellsType)
					{
					case SparklineEmptyCells.Gaps:
						A_0.WriteAttributeString(RecordTableEnumerator.b("夼嘾㉀㍂⥄♆え๊⁌㽎═⩒ᙔ㉖㕘㝚⹜Ṟበ", a_), RecordTableEnumerator.b("娼帾ㅀ", a_));
						num = 62;
						continue;
					case SparklineEmptyCells.Zero:
						goto IL_436;
					case SparklineEmptyCells.Line:
						num = 83;
						continue;
					default:
						num = 8;
						continue;
					}
					break;
				case 73:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_742;
					default:
						if (false)
						{
						}
						goto IL_847;
					}
					break;
				case 74:
					if (A_2.SparklineType == SparklineType.Line)
					{
						num = 35;
						continue;
					}
					goto IL_73A;
				case 75:
					A_0.WriteAttributeString(RecordTableEnumerator.b("匼娾♀≂ㅄ⹆㽈⹊", a_), RecordTableEnumerator.b("఼", a_));
					num = 21;
					continue;
				case 76:
					if (A_2.VerticalAxisMinimum.ᜁ() == SpartlineVerticalAxisType.Custom)
					{
						num = 65;
						continue;
					}
					goto IL_472;
				case 77:
					goto IL_7CD;
				case 78:
					if (A_2.ShowMarkers)
					{
						num = 1;
						continue;
					}
					goto IL_D84;
				case 79:
					goto IL_EED;
				case 80:
					if (A_2.ShowHighPoint)
					{
						num = 44;
						continue;
					}
					goto IL_847;
				case 81:
					goto IL_73A;
				case 82:
					num = 77;
					continue;
				case 83:
					if (A_2.SparklineType == SparklineType.Line)
					{
						num = 38;
						continue;
					}
					A_0.WriteAttributeString(RecordTableEnumerator.b("夼嘾㉀㍂⥄♆え๊⁌㽎═⩒ᙔ㉖㕘㝚⹜Ṟበ", a_), RecordTableEnumerator.b("䜼娾㍀ⱂ", a_));
					num = 22;
					continue;
				case 84:
					if (true)
					{
					}
					num = 10;
					continue;
				case 85:
					goto IL_F7F;
				case 86:
					goto IL_F84;
				}
				if (A_0 == null)
				{
					num = 55;
					continue;
				}
				num = 56;
				continue;
				IL_19F:
				num = 49;
				continue;
				IL_1C7:
				num = 43;
				continue;
				IL_436:
				num = 78;
				continue;
				IL_472:
				num = 74;
				continue;
				IL_4F9:
				A_0.WriteStartElement(RecordTableEnumerator.b("帼倾ⵀⱂ㝄ᑆⱈ㥊⑌⩎≐", a_), RecordTableEnumerator.b("唼䬾㕀㍂罄框晈㡊⹌❎㑐㹒㑔⑖睘㙚㑜㱞፠ౢᙤࡦཨὪ䍬౮ṰṲ婴ᡶὸᵺᑼ᱾겂ﮈﮒ낞鎠鎢閤麦蚨銪芬슮킰\udab2\udbb4", a_));
				ocolor = new OColor(A_2.SparklineColor);
				A_0.WriteAttributeString(RecordTableEnumerator.b("似堾⍀", a_), ocolor.Value.ToString(RecordTableEnumerator.b("攼ा", a_)));
				A_0.WriteEndElement();
				A_0.WriteStartElement(RecordTableEnumerator.b("帼倾ⵀⱂ㝄ॆⱈⱊⱌ㭎㡐╒ご", a_), RecordTableEnumerator.b("唼䬾㕀㍂罄框晈㡊⹌❎㑐㹒㑔⑖睘㙚㑜㱞፠ౢᙤࡦཨὪ䍬౮ṰṲ婴ᡶὸᵺᑼ᱾겂ﮈﮒ낞鎠鎢閤麦蚨銪芬슮킰\udab2\udbb4", a_));
				ocolor = new OColor(A_2.NegativePointColor);
				A_0.WriteAttributeString(RecordTableEnumerator.b("似堾⍀", a_), ocolor.Value.ToString(RecordTableEnumerator.b("攼ा", a_)));
				A_0.WriteEndElement();
				A_0.WriteStartElement(RecordTableEnumerator.b("帼倾ⵀⱂ㝄نㅈ≊㹌", a_), RecordTableEnumerator.b("唼䬾㕀㍂罄框晈㡊⹌❎㑐㹒㑔⑖睘㙚㑜㱞፠ౢᙤࡦཨὪ䍬౮ṰṲ婴ᡶὸᵺᑼ᱾겂ﮈﮒ낞鎠鎢閤麦蚨銪芬슮킰\udab2\udbb4", a_));
				ocolor = new OColor(A_2.HorizontalAxisColor);
				A_0.WriteAttributeString(RecordTableEnumerator.b("似堾⍀", a_), ocolor.Value.ToString(RecordTableEnumerator.b("攼ा", a_)));
				A_0.WriteEndElement();
				num = 20;
				continue;
				IL_651:
				num = 41;
				continue;
				IL_6DF:
				num = 14;
				continue;
				IL_742:
				num = 71;
				continue;
				IL_73A:
				sparklineType = A_2.SparklineType;
				goto IL_742;
				IL_7CD:
				num = 48;
				continue;
				IL_847:
				num = 9;
				continue;
				IL_8F1:
				num = 39;
				continue;
				IL_9CC:
				emptyCellsType = A_2.EmptyCellsType;
				num = 72;
				continue;
				IL_A01:
				num = 3;
				continue;
				IL_B2B:
				A_0.WriteStartElement(RecordTableEnumerator.b("帼倾ⵀⱂ㝄ņ⁈㥊㹌㭎", a_), RecordTableEnumerator.b("唼䬾㕀㍂罄框晈㡊⹌❎㑐㹒㑔⑖睘㙚㑜㱞፠ౢᙤࡦཨὪ䍬౮ṰṲ婴ᡶὸᵺᑼ᱾겂ﮈﮒ낞鎠鎢閤麦蚨銪芬슮킰\udab2\udbb4", a_));
				ocolor = new OColor(A_2.FirstPointColor);
				A_0.WriteAttributeString(RecordTableEnumerator.b("似堾⍀", a_), ocolor.Value.ToString(RecordTableEnumerator.b("攼ा", a_)));
				A_0.WriteEndElement();
				A_0.WriteStartElement(RecordTableEnumerator.b("帼倾ⵀⱂ㝄୆⡈㡊㥌", a_), RecordTableEnumerator.b("唼䬾㕀㍂罄框晈㡊⹌❎㑐㹒㑔⑖睘㙚㑜㱞፠ౢᙤࡦཨὪ䍬౮ṰṲ婴ᡶὸᵺᑼ᱾겂ﮈﮒ낞鎠鎢閤麦蚨銪芬슮킰\udab2\udbb4", a_));
				ocolor = new OColor(A_2.LastPointColor);
				A_0.WriteAttributeString(RecordTableEnumerator.b("似堾⍀", a_), ocolor.Value.ToString(RecordTableEnumerator.b("攼ा", a_)));
				A_0.WriteEndElement();
				A_0.WriteStartElement(RecordTableEnumerator.b("帼倾ⵀⱂ㝄ཆ⁈ⱊ╌", a_), RecordTableEnumerator.b("唼䬾㕀㍂罄框晈㡊⹌❎㑐㹒㑔⑖睘㙚㑜㱞፠ౢᙤࡦཨὪ䍬౮ṰṲ婴ᡶὸᵺᑼ᱾겂ﮈﮒ낞鎠鎢閤麦蚨銪芬슮킰\udab2\udbb4", a_));
				ocolor = new OColor(A_2.HighPointColor);
				A_0.WriteAttributeString(RecordTableEnumerator.b("似堾⍀", a_), ocolor.Value.ToString(RecordTableEnumerator.b("攼ा", a_)));
				A_0.WriteEndElement();
				A_0.WriteStartElement(RecordTableEnumerator.b("帼倾ⵀⱂ㝄୆♈㱊", a_), RecordTableEnumerator.b("唼䬾㕀㍂罄框晈㡊⹌❎㑐㹒㑔⑖睘㙚㑜㱞፠ౢᙤࡦཨὪ䍬౮ṰṲ婴ᡶὸᵺᑼ᱾겂ﮈﮒ낞鎠鎢閤麦蚨銪芬슮킰\udab2\udbb4", a_));
				ocolor = new OColor(A_2.LastPointColor);
				A_0.WriteAttributeString(RecordTableEnumerator.b("似堾⍀", a_), ocolor.Value.ToString(RecordTableEnumerator.b("攼ा", a_)));
				A_0.WriteEndElement();
				num = 24;
				continue;
				IL_D5C:
				num = 26;
				continue;
				IL_D84:
				num = 80;
				continue;
				IL_EED:
				num = 70;
				continue;
				IL_F48:
				enumerator = A_2.GetEnumerator();
				num = 45;
				continue;
				IL_F84:
				num = 40;
			}
			IL_19A:
			throw new ArgumentNullException(RecordTableEnumerator.b("䨼䴾⡀㝂⁄㕆", a_));
			IL_965:
			throw new ArgumentNullException(RecordTableEnumerator.b("丼圾⑀♂ㅄ", a_));
			IL_F7F:
			throw new ArgumentNullException(RecordTableEnumerator.b("渼伾⁀ㅂ⹄⭆⁈╊⡌ࡎ⍐㱒⁔❖", a_));
			IL_FAC:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06004902 RID: 18690 RVA: 0x002C6C80 File Offset: 0x002C5C80
	private void ᜀ(XmlWriter A_0, XlsWorksheet A_1, SparklineCollection A_2)
	{
		int a_ = 0;
		int num = 4;
		for (;;)
		{
			List<ISparkline>.Enumerator enumerator;
			switch (num)
			{
			case 0:
				try
				{
					num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_12E;
						case 1:
						{
							if (!enumerator.MoveNext())
							{
								num = 4;
								continue;
							}
							Sparkline a_2 = (Sparkline)enumerator.Current;
							this.ᜀ(A_0, A_1, a_2);
							num = 2;
							continue;
						}
						case 4:
							num = 0;
							continue;
						}
						IL_10B:
						num = 1;
						continue;
						goto IL_10B;
					}
					IL_12E:
					goto IL_197;
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
				goto IL_13E;
			case 1:
				goto IL_40;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_151;
				default:
					goto IL_178;
				}
				break;
			case 3:
				goto IL_A2;
			case 5:
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				goto IL_13E;
			case 6:
				goto IL_151;
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			num = 5;
			continue;
			IL_151:
			if (A_2 == null)
			{
				num = 2;
				continue;
			}
			A_0.WriteStartElement(RecordTableEnumerator.b("䔵䠷嬹主唽ⰿ⭁⩃⍅㭇", a_), RecordTableEnumerator.b("帵䰷丹䰻н漿流㝃╅⁇⽉⅋⽍⍏籑㥓㽕㭗⡙㍛ⵝཟѡၣ䡥୧թū䅭Ὧᑱታή᭷ό卻ൽ黎煉歹랗ꢙ겛꺝馟趡鶣覥얧쮩얫삭", a_));
			enumerator = A_2.GetEnumerator();
			num = 0;
			continue;
			IL_13E:
			if (true)
			{
			}
			num = 6;
		}
		IL_40:
		throw new ArgumentNullException(RecordTableEnumerator.b("䄵䨷匹䠻嬽㈿", a_));
		IL_A2:
		throw new ArgumentNullException(RecordTableEnumerator.b("䔵倷弹夻䨽", a_));
		IL_178:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("攵䠷嬹主刽⤿ⱁ⅃㕅", a_));
		IL_197:
		A_0.WriteEndElement();
	}

	// Token: 0x06004903 RID: 18691 RVA: 0x002C6E3C File Offset: 0x002C5E3C
	private void ᜀ(XmlWriter A_0, XlsWorksheet A_1, Sparkline A_2)
	{
		int a_ = 11;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_D6;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_D6;
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
				num = 3;
				continue;
			case 3:
				if (A_2 == null)
				{
					num = 4;
					continue;
				}
				goto IL_D8;
			case 4:
				goto IL_75;
			case 5:
				goto IL_60;
			}
			if (true)
			{
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
		IL_60:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙀ㅂⱄ㍆ⱈ㥊", a_));
		IL_75:
		throw new ArgumentNullException(RecordTableEnumerator.b("㉀㍂⑄㕆≈❊⑌ⅎ㑐", a_));
		IL_D6:
		throw new ArgumentNullException(RecordTableEnumerator.b("㉀⭂⁄≆㵈", a_));
		IL_D8:
		A_0.WriteStartElement(RecordTableEnumerator.b("㉀㍂⑄㕆≈❊⑌ⅎ㑐", a_), RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜㉞ࡠbᝤࡦᩨѪ୬᭮彰ၲᩴ᩶噸ᑺ᭼᥾ꢆ愈ﮊﾌﾖﲘﺚ춠貢鞤鞦馨銪芬隮麰\udeb2풴\udeb6ힸ", a_));
		A_0.WriteStartElement(RecordTableEnumerator.b("❀", a_), RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜㉞ࡠbᝤࡦᩨѪ୬᭮彰ၲᩴ᩶噸ᑺ᭼᥾ꢆ﶐벒ꞔꞖꦘ궚늜삠쪢쮤", a_));
		A_0.WriteString(A_2.DataRange.RangeGlobalAddress);
		A_0.WriteEndElement();
		A_0.WriteStartElement(RecordTableEnumerator.b("㉀㉂㝄≆⽈", a_), RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜㉞ࡠbᝤࡦᩨѪ୬᭮彰ၲᩴ᩶噸ᑺ᭼᥾ꢆ﶐벒ꞔꞖꦘ궚늜삠쪢쮤", a_));
		A_0.WriteString(A_2.RefRange.RangeAddressLocal);
		A_0.WriteEndElement();
		A_0.WriteEndElement();
	}

	// Token: 0x06004904 RID: 18692 RVA: 0x002C6FBC File Offset: 0x002C5FBC
	protected override void ᜀ(XmlWriter A_0)
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
		spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("瘶䤸䬺欼娾㍀あⱄ⡆❈", a_), RecordTableEnumerator.b("ضസᔺ഼ా煀獂", a_), null);
	}

	// Token: 0x04002124 RID: 8484
	private new const string ᜀ = "14.0300";
}
