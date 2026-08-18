using System;
using System.Collections.Generic;
using Spire.Xls.Charts;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Charts
{
	// Token: 0x020001AD RID: 429
	public class XlsChartCategoryAxis : XlsChartValueAxis, IChartCategoryAxis
	{
		// Token: 0x0600170F RID: 5903 RVA: 0x000DEB7C File Offset: 0x000DDB7C
		internal XlsChartCategoryAxis(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			base.AxisId = (base.IsPrimary ? 59983360 : 62908672);
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x000DEBD4 File Offset: 0x000DDBD4
		internal XlsChartCategoryAxis(spr\u1DF5 A_0, object A_1, AxisType A_2) : this(A_0, A_1, A_2, true)
		{
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x000DEBEC File Offset: 0x000DDBEC
		internal XlsChartCategoryAxis(spr\u1DF5 A_0, object A_1, AxisType A_2, bool A_3) : base(A_0, A_1, A_2, A_3)
		{
			base.AxisId = (base.IsPrimary ? 59983360 : 62908672);
			if (!base.IsPrimary)
			{
				base.Visible = false;
			}
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x000DEC5C File Offset: 0x000DDC5C
		internal XlsChartCategoryAxis(spr\u1DF5 A_0, object A_1, IList<BiffRecordRaw> A_2, ref int A_3) : this(A_0, A_1, A_2, ref A_3, true)
		{
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x000DEC78 File Offset: 0x000DDC78
		internal XlsChartCategoryAxis(spr\u1DF5 A_0, object A_1, IList<BiffRecordRaw> A_2, ref int A_3, bool A_4) : base(A_0, A_1, A_2, ref A_3, A_4)
		{
			base.AxisId = (base.IsPrimary ? 59983360 : 62908672);
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06001714 RID: 5908 RVA: 0x000DECD4 File Offset: 0x000DDCD4
		// (set) Token: 0x06001715 RID: 5909 RVA: 0x000DED18 File Offset: 0x000DDD18
		public override bool IsMaxCross
		{
			get
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
				return this.ᜈ;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_85;
					case 2:
						base.IsMaxCross = value;
						num = 0;
						continue;
					case 3:
						goto IL_74;
					}
					if (this.IsChartBubbleOrScatter)
					{
						if (true)
						{
						}
						num = 2;
					}
					else
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
							this.ᜄ.ᜁ(value);
							num = 3;
							break;
						}
					}
				}
				IL_74:
				IL_85:
				this.ᜈ = value;
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06001716 RID: 5910 RVA: 0x000DEDB4 File Offset: 0x000DDDB4
		// (set) Token: 0x06001717 RID: 5911 RVA: 0x000DEE0C File Offset: 0x000DDE0C
		public double CrossingPoint
		{
			get
			{
				if (!this.IsChartBubbleOrScatter)
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
						if (true)
						{
						}
						return (double)this.ᜄ.ᜄ();
					}
				}
				return base.CrossesAt;
			}
			set
			{
				int a_ = 19;
				int num = 7;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_75;
					case 1:
						if (value < 1.0)
						{
							goto IL_4B;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_75;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 2:
						num = 0;
						continue;
					case 3:
						if (!base.ParentWorkbook.Loading)
						{
							num = 5;
							continue;
						}
						goto IL_FC;
					case 4:
						goto IL_4B;
					case 5:
						goto IL_6B;
					case 6:
						goto IL_49;
					}
					if (this.IsChartBubbleOrScatter)
					{
						num = 6;
						continue;
					}
					num = 1;
					continue;
					IL_4B:
					num = 3;
					continue;
					IL_75:
					if (value <= 31999.0)
					{
						goto IL_FC;
					}
					num = 4;
				}
				IL_49:
				base.CrossesAt = value;
				return;
				IL_6B:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("཈⑊㽌潎㉐♒❔╖㱘㕚⥜罞ɠୢѤᕦᵨ䭪ᥬ᙮Űᙲ啴Ŷᡸ᝺ᑼ᭾ꆀﾌ꾎ﲐ릘連뾞쎠욢톤킦첨캪쎬辮肰鎲솴\ud8b6馸袺貼蚾", a_));
				IL_FC:
				this.ᜄ.ᜁ((ushort)value);
				this.IsAutoCross = false;
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06001718 RID: 5912 RVA: 0x000DEF2C File Offset: 0x000DDF2C
		// (set) Token: 0x06001719 RID: 5913 RVA: 0x000DEF70 File Offset: 0x000DDF70
		public int LabelFrequency
		{
			get
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
				return this.TickLabelSpacing;
			}
			set
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
				this.TickLabelSpacing = value;
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x0600171A RID: 5914 RVA: 0x000DEFB4 File Offset: 0x000DDFB4
		// (set) Token: 0x0600171B RID: 5915 RVA: 0x000DEFFC File Offset: 0x000DDFFC
		public int TickLabelSpacing
		{
			get
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
				return (int)this.ᜄ.ᜃ();
			}
			set
			{
				int a_ = 15;
				if (value < 0)
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
						throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ᅄ⹆⩈⁊Ō⹎㍐㙒㥔і⥘㩚㹜㙞འѢ", a_), RecordTableEnumerator.b("ᅄ⹆⩈⁊Ō⹎㍐㙒㥔і⥘㩚㹜㙞འѢ䕤Ѧࡨժͬnհ卲᝴ቶ奸᝺᡼౾ꎂ권뾎", a_));
					}
				}
				this.ᜄ.ᜂ((ushort)value);
				base.AutoTickLabelSpacing = false;
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x0600171C RID: 5916 RVA: 0x000DF07C File Offset: 0x000DE07C
		// (set) Token: 0x0600171D RID: 5917 RVA: 0x000DF0C0 File Offset: 0x000DE0C0
		public int TickMarksFrequency
		{
			get
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
				return this.TickMarkSpacing;
			}
			set
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
				this.TickMarkSpacing = value;
				base.AutoTickMarkSpacing = false;
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x0600171E RID: 5918 RVA: 0x000DF10C File Offset: 0x000DE10C
		// (set) Token: 0x0600171F RID: 5919 RVA: 0x000DF154 File Offset: 0x000DE154
		public int TickMarkSpacing
		{
			get
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
				return (int)this.ᜄ.ᜅ();
			}
			set
			{
				int a_ = 19;
				if (value < 0)
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
						throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㽈⩊⅌㩎㑐", a_), RecordTableEnumerator.b("Ὀ⩊⅌㩎㑐獒㙔㙖㝘㕚㉜⭞䅠Ţd䝦ը๪Ṭᱮ兰ݲᵴᙶ᝸孺䵼", a_));
					}
				}
				base.AutoTickMarkSpacing = false;
				this.ᜄ.ᜀ((ushort)value);
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06001720 RID: 5920 RVA: 0x000DF1D4 File Offset: 0x000DE1D4
		protected override ObjectTextLinkType TextLinkType
		{
			get
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
				return ObjectTextLinkType.XAxis;
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06001721 RID: 5921 RVA: 0x000DF210 File Offset: 0x000DE210
		// (set) Token: 0x06001722 RID: 5922 RVA: 0x000DF258 File Offset: 0x000DE258
		public bool AxisBetweenCategories
		{
			get
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
				return this.CatserRecord.ᜂ();
			}
			set
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
				this.CatserRecord.ᜂ(value);
			}
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06001723 RID: 5923 RVA: 0x000DF2A0 File Offset: 0x000DE2A0
		// (set) Token: 0x06001724 RID: 5924 RVA: 0x000DF2F8 File Offset: 0x000DE2F8
		public override bool IsReverseOrder
		{
			get
			{
				if (true)
				{
				}
				if (this.IsChartBubbleOrScatter)
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
						return base.IsReverseOrder;
					}
				}
				return this.CatserRecord.ᜀ();
			}
			set
			{
				if (this.IsChartBubbleOrScatter)
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
						if (true)
						{
						}
						base.IsReverseOrder = value;
						return;
					}
				}
				this.CatserRecord.ᜀ(value);
				base.IsReverseOrder = value;
			}
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06001725 RID: 5925 RVA: 0x000DF358 File Offset: 0x000DE358
		// (set) Token: 0x06001726 RID: 5926 RVA: 0x000DF3AC File Offset: 0x000DE3AC
		public IXLSRange CategoryLabels
		{
			get
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
				return base.ParentXlsChart.Series[0].CategoryLabels;
			}
			set
			{
				for (;;)
				{
					XlsChartSeries xlsChartSeries = base.ParentXlsChart.Series;
					int num = 0;
					int count = xlsChartSeries.Count;
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_37;
						case 1:
							goto IL_71;
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
								if (num >= count)
								{
									num2 = 1;
									continue;
								}
								xlsChartSeries[num].CategoryLabels = value;
								num++;
								num2 = 0;
								continue;
							}
							break;
						case 3:
							goto IL_37;
						}
						break;
						IL_37:
						num2 = 2;
					}
				}
				IL_71:
				if (true)
				{
				}
			}
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x000DF454 File Offset: 0x000DE454
		// (set) Token: 0x06001728 RID: 5928 RVA: 0x000DF4A8 File Offset: 0x000DE4A8
		public object[] EnteredDirectlyCategoryLabels
		{
			get
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
				return base.ParentXlsChart.Series[0].EnteredDirectlyCategoryLabels;
			}
			set
			{
				for (;;)
				{
					XlsChartSeries xlsChartSeries = base.ParentXlsChart.Series;
					int num = 0;
					int count = xlsChartSeries.Count;
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
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
								if (num >= count)
								{
									num2 = 1;
									continue;
								}
								xlsChartSeries[num].EnteredDirectlyCategoryLabels = value;
								num++;
								num2 = 3;
								continue;
							}
							break;
						case 1:
							return;
						case 2:
							goto IL_37;
						case 3:
							goto IL_37;
						}
						break;
						IL_37:
						num2 = 0;
					}
				}
			}
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06001729 RID: 5929 RVA: 0x000DF550 File Offset: 0x000DE550
		// (set) Token: 0x0600172A RID: 5930 RVA: 0x000DF594 File Offset: 0x000DE594
		public CategoryType CategoryType
		{
			get
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
				return this.ᜆ;
			}
			set
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
				this.ᜆ = value;
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x0600172B RID: 5931 RVA: 0x000DF5D8 File Offset: 0x000DE5D8
		// (set) Token: 0x0600172C RID: 5932 RVA: 0x000DF61C File Offset: 0x000DE61C
		public int Offset
		{
			get
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
				return this.ᜇ;
			}
			set
			{
				int a_ = 12;
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
							continue;
						default:
							if (false)
							{
							}
							if (value > 1000)
							{
								num = 2;
								continue;
							}
							goto IL_81;
						}
						break;
					case 1:
						num = 0;
						continue;
					case 2:
						goto IL_7F;
					}
					if (value < 0)
					{
						break;
					}
					num = 1;
				}
				IL_37:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ᙁⱃ⍅桇㱉ⵋ≍╏㝑瑓㕕㥗㑙籛㱝՟䉡ɣᑥݧݩ䱫幭偯ٱᱳѵ᝷ཹ᭻ᙽꁿ뎁뒃뚅뢇ꒉ", a_));
				IL_7F:
				goto IL_37;
				IL_81:
				if (true)
				{
				}
				this.ᜇ = value;
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x0600172D RID: 5933 RVA: 0x000DF6C4 File Offset: 0x000DE6C4
		// (set) Token: 0x0600172E RID: 5934 RVA: 0x000DF710 File Offset: 0x000DE710
		public ChartBaseUnitType BaseUnit
		{
			get
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
				this.ᜂ();
				return this.ᜅ.ᜅ();
			}
			set
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
				this.ᜂ();
				this.ᜅ.ᜁ(value);
				this.ᜅ.ᜀ(false);
			}
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x0600172F RID: 5935 RVA: 0x000DF76C File Offset: 0x000DE76C
		// (set) Token: 0x06001730 RID: 5936 RVA: 0x000DF7B8 File Offset: 0x000DE7B8
		public bool BaseUnitIsAuto
		{
			get
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
				this.ᜂ();
				return this.ᜅ.ᜎ();
			}
			set
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
				this.ᜂ();
				this.ᜅ.ᜀ(value);
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06001731 RID: 5937 RVA: 0x000DF808 File Offset: 0x000DE808
		// (set) Token: 0x06001732 RID: 5938 RVA: 0x000DF8A4 File Offset: 0x000DE8A4
		public override bool IsAutoMajor
		{
			get
			{
				int num = 1;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_54;
					case 2:
						goto IL_38;
					case 3:
						if (this.IsCategoryType)
						{
							num = 0;
							continue;
						}
						goto IL_79;
					}
					if (this.IsChartBubbleOrScatter)
					{
						num = 2;
					}
					else
					{
						num = 3;
					}
				}
				IL_38:
				goto IL_72;
				IL_54:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_72:
					return base.IsAutoMajor;
				default:
					if (false)
					{
					}
					return true;
				}
				IL_79:
				return this.ᜅ.ᜏ();
			}
			set
			{
				int a_ = 4;
				int num = 9;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_11;
					default:
						if (false)
						{
						}
						switch (num)
						{
						case 0:
							num = 1;
							continue;
						case 1:
							if (value != this.IsAutoMajor)
							{
								num = 10;
								continue;
							}
							goto IL_159;
						case 2:
							num = 7;
							continue;
						case 3:
							if (!base.ParentWorkbook.Loading)
							{
								num = 2;
								continue;
							}
							goto IL_159;
						case 4:
							num = 6;
							continue;
						case 5:
							num = 3;
							continue;
						case 6:
							if (!base.ParentWorkbook.IsCreated)
							{
								num = 0;
								continue;
							}
							goto IL_159;
						case 7:
							if (!base.ParentWorkbook.IsLoaded)
							{
								num = 4;
								continue;
							}
							goto IL_159;
						case 8:
							num = 11;
							continue;
						case 9:
							goto IL_11;
						case 10:
							goto IL_AB;
						case 11:
							if (!this.IsCategoryType)
							{
								num = 5;
								continue;
							}
							goto IL_159;
						}
						break;
					}
					IL_77:
					if (!this.IsChartBubbleOrScatter)
					{
						num = 8;
						continue;
					}
					goto IL_159;
					IL_11:
					if (true)
					{
					}
					goto IL_77;
				}
				IL_AB:
				throw new NotSupportedException(RecordTableEnumerator.b("渹吻圽㌿扁㑃㑅❇㩉⥋㱍⑏⭑瑓㽕⭗穙㉛ㅝᑟ䉡ᝣ፥ᡧᩩͫᱭѯ᝱ၳ噵ṷᕹ๻幽ꚅﾉﺋﲍﲑ뚕ﮗﶛ풟芡킣\udfa5\ud8a7쾩", a_));
				IL_159:
				base.IsAutoMajor = value;
				this.ᜅ.ᜆ(value);
			}
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06001733 RID: 5939 RVA: 0x000DFA20 File Offset: 0x000DEA20
		// (set) Token: 0x06001734 RID: 5940 RVA: 0x000DFABC File Offset: 0x000DEABC
		public override bool IsAutoMinor
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_4C;
					case 2:
						if (this.IsCategoryType)
						{
							num = 1;
							continue;
						}
						goto IL_79;
					case 3:
						goto IL_30;
					}
					if (this.IsChartBubbleOrScatter)
					{
						num = 3;
					}
					else
					{
						num = 2;
					}
				}
				IL_30:
				goto IL_72;
				IL_4C:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_72:
					return base.IsAutoMinor;
				default:
					if (false)
					{
					}
					return true;
				}
				IL_79:
				return this.ᜅ.ᜆ();
			}
			set
			{
				int a_ = 10;
				int num = 9;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (!base.ParentWorkbook.IsLoaded)
						{
							num = 6;
							continue;
						}
						goto IL_123;
					case 1:
						num = 8;
						continue;
					case 2:
						num = 0;
						continue;
					case 3:
						goto IL_8C;
					case 4:
						goto IL_A3;
					case 5:
						if (!this.IsCategoryType)
						{
							num = 1;
							continue;
						}
						goto IL_123;
					case 6:
						num = 3;
						continue;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_8C;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num = 5;
							continue;
						}
						break;
					case 8:
						if (!base.ParentWorkbook.Loading)
						{
							num = 2;
							continue;
						}
						goto IL_123;
					}
					if (!this.IsChartBubbleOrScatter)
					{
						num = 7;
						continue;
					}
					goto IL_123;
					IL_8C:
					if (value == this.IsAutoMinor)
					{
						goto IL_123;
					}
					num = 4;
				}
				IL_A3:
				throw new NotSupportedException(RecordTableEnumerator.b("ᐿ⩁ⵃ㕅桇㩉㹋⅍⁏㝑♓≕⅗穙㕛ⵝ䁟ౡୣብ䡧ᥩᥫṭoᵱٳɵᵷṹ屻᡽ꒃ겋벛ﶝ좟쎡횣튥袧\udea9햫\udead햯", a_));
				IL_123:
				base.IsAutoMinor = value;
				this.ᜅ.ᜇ(value);
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06001735 RID: 5941 RVA: 0x000DFC00 File Offset: 0x000DEC00
		// (set) Token: 0x06001736 RID: 5942 RVA: 0x000DFC58 File Offset: 0x000DEC58
		public override bool IsAutoCross
		{
			get
			{
				if (this.IsChartBubbleOrScatter)
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
						return base.IsAutoCross;
					}
				}
				return this.ᜅ.ᜈ();
			}
			set
			{
				for (;;)
				{
					base.IsAutoCross = value;
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							return;
						case 1:
							if (!this.IsChartBubbleOrScatter)
							{
								num = 2;
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
								if (true)
								{
								}
								if (false)
								{
								}
								this.ᜅ.ᜂ(value);
								base.IsAutoCross = value;
								break;
							}
							num = 0;
							continue;
						}
						break;
					}
				}
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06001737 RID: 5943 RVA: 0x000DFCE8 File Offset: 0x000DECE8
		// (set) Token: 0x06001738 RID: 5944 RVA: 0x000DFD84 File Offset: 0x000DED84
		public override bool IsAutoMax
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.IsCategoryType)
						{
							num = 2;
							continue;
						}
						goto IL_79;
					case 2:
						goto IL_54;
					case 3:
						goto IL_38;
					}
					if (true)
					{
					}
					if (this.IsChartBubbleOrScatter)
					{
						num = 3;
					}
					else
					{
						num = 0;
					}
				}
				IL_38:
				goto IL_72;
				IL_54:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_72:
					return base.IsAutoMax;
				default:
					if (false)
					{
					}
					return true;
				}
				IL_79:
				return this.ᜅ.ᜐ();
			}
			set
			{
				int a_ = 15;
				for (;;)
				{
					base.SetAutoMax(false, value);
					this.ᜅ.ᜅ(value);
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 1;
							continue;
						case 1:
							if (!this.IsCategoryType)
							{
								return;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_A2;
							default:
								if (false)
								{
								}
								num = 3;
								continue;
							}
							break;
						case 2:
							if (!this.IsChartBubbleOrScatter)
							{
								num = 0;
								continue;
							}
							return;
						case 3:
							goto IL_A2;
						}
						break;
					}
				}
				IL_A2:
				if (true)
				{
				}
				throw new NotSupportedException(RecordTableEnumerator.b("ᅄ⽆⁈㡊浌㽎⍐㱒╔㉖⭘⽚⑜罞ࡠၢ䕤०٨Ὢ䵬ᱮѰͲմᡶ୸ེ᡼᭾ꆀꦈﾊ놐ﺚ膠삢춤욦\udba8\udfaa趬\udbae좰쎲킴", a_));
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06001739 RID: 5945 RVA: 0x000DFE40 File Offset: 0x000DEE40
		// (set) Token: 0x0600173A RID: 5946 RVA: 0x000DFEDC File Offset: 0x000DEEDC
		public override bool IsAutoMin
		{
			get
			{
				if (true)
				{
				}
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_38;
					case 2:
						goto IL_54;
					case 3:
						if (this.IsCategoryType)
						{
							num = 2;
							continue;
						}
						goto IL_79;
					}
					if (this.IsChartBubbleOrScatter)
					{
						num = 0;
					}
					else
					{
						num = 3;
					}
				}
				IL_38:
				goto IL_72;
				IL_54:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_72:
					return base.IsAutoMin;
				default:
					if (false)
					{
					}
					return true;
				}
				IL_79:
				return this.ᜅ.ᜄ();
			}
			set
			{
				int a_ = 0;
				for (;;)
				{
					base.SetAutoMin(false, value);
					this.ᜅ.ᜁ(value);
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_A2;
						case 1:
							if (true)
							{
							}
							if (!this.IsChartBubbleOrScatter)
							{
								num = 3;
								continue;
							}
							return;
						case 2:
							if (!this.IsCategoryType)
							{
								return;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_A2;
							default:
								if (false)
								{
								}
								num = 0;
								continue;
							}
							break;
						case 3:
							num = 2;
							continue;
						}
						break;
					}
				}
				IL_A2:
				throw new NotSupportedException(RecordTableEnumerator.b("戵倷匹伻ḽ〿ぁ⭃㙅ⵇ㡉㡋㝍灏㭑❓癕㙗㕙⡛繝፟ᝡᑣᙥݧᡩᡫ୭ᑯ剱ታ᥵੷婹ࡻᙽꊁ慎늑ﺕ聯뺝풟\udba1풣쎥", a_));
			}
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x0600173B RID: 5947 RVA: 0x000DFF98 File Offset: 0x000DEF98
		// (set) Token: 0x0600173C RID: 5948 RVA: 0x000E0050 File Offset: 0x000DF050
		public override double MajorUnit
		{
			get
			{
				int a_ = 11;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_39;
					case 1:
						if (this.IsCategoryType)
						{
							num = 2;
							continue;
						}
						goto IL_94;
					case 2:
						goto IL_67;
					}
					if (this.IsChartBubbleOrScatter)
					{
						num = 0;
					}
					else
					{
						num = 1;
					}
				}
				IL_39:
				goto IL_85;
				IL_67:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_85:
					if (true)
					{
					}
					return base.MajorUnit;
				default:
					if (false)
					{
					}
					throw new NotSupportedException(RecordTableEnumerator.b("ᕀ⭂ⱄ㑆楈㭊㽌⁎⅐㙒❔⍖⁘筚㑜ⱞ䅠ൢ੤፦䥨ᡪᡬὮŰᱲݴͶᱸὺ嵼᥾ꖄ권붜ﲞ즠슢힤펦覨\udfaa풬\udfae풰", a_));
				}
				IL_94:
				return (double)this.ᜅ.ᜁ();
			}
			set
			{
				int a_ = 6;
				int num = 7;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_87;
					case 1:
						if (!this.IsAutoMajor)
						{
							num = 8;
							continue;
						}
						goto IL_163;
					case 2:
						if (value >= 1.0)
						{
							num = 6;
							continue;
						}
						goto IL_103;
					case 3:
						if (this.IsCategoryType)
						{
							num = 9;
							continue;
						}
						num = 2;
						continue;
					case 4:
						if (value > this.MajorUnit)
						{
							num = 11;
							continue;
						}
						goto IL_163;
					case 5:
						goto IL_126;
					case 6:
						num = 1;
						continue;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_E5;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 8:
						num = 4;
						continue;
					case 9:
						goto IL_E5;
					case 10:
						if (!base.ParentWorkbook.Loading)
						{
							num = 5;
							continue;
						}
						goto IL_163;
					case 11:
						goto IL_103;
					}
					if (this.IsChartBubbleOrScatter)
					{
						num = 0;
						continue;
					}
					num = 3;
					continue;
					IL_103:
					num = 10;
				}
				IL_87:
				base.MajorUnit = value;
				return;
				IL_E5:
				throw new NotSupportedException(RecordTableEnumerator.b("栻嘽⤿ㅁ摃㙅㩇╉㱋⭍≏♑ⵓ癕ㅗ⥙籛そཟᙡ䑣ᕥᵧᩩᱫŭɯٱᅳት塷ᱹ፻౽ꁿꢇ曆ﲍ望뢗蓮ﾝ튟횡蒣튥톧\udaa9즫", a_));
				IL_126:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("焻弽⨿ⵁ㙃ፅ♇⍉㡋", a_));
				IL_163:
				this.ᜅ.ᜀ((ushort)value);
				this.ᜅ.ᜆ(false);
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x0600173D RID: 5949 RVA: 0x000E01DC File Offset: 0x000DF1DC
		// (set) Token: 0x0600173E RID: 5950 RVA: 0x000E0294 File Offset: 0x000DF294
		public override double MinorUnit
		{
			get
			{
				int a_ = 17;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6F;
					case 2:
						if (true)
						{
						}
						if (this.IsCategoryType)
						{
							num = 0;
							continue;
						}
						goto IL_94;
					case 3:
						goto IL_39;
					}
					if (this.IsChartBubbleOrScatter)
					{
						num = 3;
					}
					else
					{
						num = 2;
					}
				}
				IL_39:
				goto IL_8D;
				IL_6F:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_8D:
					return base.MinorUnit;
				default:
					if (false)
					{
					}
					throw new NotSupportedException(RecordTableEnumerator.b("ፆⅈ≊㹌潎⅐⅒㩔❖㱘⥚⥜♞䅠੢ᙤ䝦ݨѪᥬ佮ɰٲմݶᙸॺॼ᩾ꎂﮈꮊ歷뎒햠莢욤쾦좨\ud9aa\ud9ac辮얰쪲어튶", a_));
				}
				IL_94:
				return (double)this.ᜅ.\u170D();
			}
			set
			{
				int a_ = 18;
				int num = 8;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.IsCategoryType)
						{
							num = 6;
							continue;
						}
						num = 5;
						continue;
					case 1:
						num = 7;
						continue;
					case 2:
						if (true)
						{
						}
						num = 4;
						continue;
					case 3:
						goto IL_77;
					case 4:
						if (!this.IsAutoMajor)
						{
							num = 1;
							continue;
						}
						goto IL_12D;
					case 5:
						if (value >= 1.0)
						{
							num = 2;
							continue;
						}
						goto IL_DC;
					case 6:
						goto IL_DA;
					case 7:
						if (value > this.MajorUnit)
						{
							goto IL_10C;
						}
						goto IL_12D;
					case 8:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_10C;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 9:
						goto IL_117;
					}
					if (this.IsChartBubbleOrScatter)
					{
						num = 3;
						continue;
					}
					num = 0;
					continue;
					IL_10C:
					num = 9;
				}
				IL_77:
				base.MinorUnit = value;
				return;
				IL_DA:
				throw new NotSupportedException(RecordTableEnumerator.b("᱇≉╋㵍灏≑♓㥕⡗㽙⹛⩝ᥟ䉡ൣᕥ䡧ѩͫᩭ偯űųٵࡷᕹ๻੽ꒃ겋揄뒓ﮝ캟횡蒣얥삧쮩\udeab\udaad邯욱춳욵\uddb7", a_));
				IL_DC:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("Շ⍉≋⅍≏ݑ㩓㽕ⱗ", a_));
				IL_117:
				goto IL_DC;
				IL_12D:
				this.ᜅ.ᜁ((ushort)value);
				this.ᜅ.ᜇ(false);
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x0600173F RID: 5951 RVA: 0x000E03E8 File Offset: 0x000DF3E8
		// (set) Token: 0x06001740 RID: 5952 RVA: 0x000E0434 File Offset: 0x000DF434
		public ChartBaseUnitType MajorUnitScale
		{
			get
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
				this.ᜂ();
				return this.ᜅ.ᜌ();
			}
			set
			{
				int a_ = 9;
				for (;;)
				{
					this.ᜂ();
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_95;
						case 1:
							if (!this.IsAutoMinor)
							{
								num = 3;
								continue;
							}
							goto IL_97;
						case 2:
							if (value >= this.MinorUnitScale)
							{
								goto IL_97;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_95;
							default:
								if (false)
								{
								}
								num = 0;
								continue;
							}
							break;
						case 3:
							num = 2;
							continue;
						}
						break;
					}
				}
				IL_95:
				if (true)
				{
				}
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("款⥀⩂㙄杆㥈㥊≌㽎㑐⅒⅔⹖祘㉚⹜罞འౢᅤ䝦ᩨṪᵬὮṰŲŴቶᵸ孺᭼ၾꎂꮊ搜練뮚ﺜ삠톢톤螦\udda8튪\uddac쪮", a_));
				IL_97:
				this.ᜅ.ᜂ(value);
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06001741 RID: 5953 RVA: 0x000E04F0 File Offset: 0x000DF4F0
		// (set) Token: 0x06001742 RID: 5954 RVA: 0x000E053C File Offset: 0x000DF53C
		public ChartBaseUnitType MinorUnitScale
		{
			get
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
				this.ᜂ();
				return this.ᜅ.ᜂ();
			}
			set
			{
				int a_ = 16;
				for (;;)
				{
					this.ᜂ();
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 1;
							continue;
						case 1:
							if (this.MajorUnitScale >= value)
							{
								goto IL_97;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_95;
							default:
								if (false)
								{
								}
								num = 3;
								continue;
							}
							break;
						case 2:
							if (!this.IsAutoMajor)
							{
								if (true)
								{
								}
								num = 0;
								continue;
							}
							goto IL_97;
						case 3:
							goto IL_95;
						}
						break;
					}
				}
				IL_95:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ቅ⁇⍉㽋湍⁏⁑㭓♕㵗⡙⡛❝䁟ୡᝣ䙥٧թᡫ乭ͯݱѳٵ᝷ࡹࡻ᭽ꊁ慎ꪉ늑鍊풟芡잣캥즧\ud8a9\ud8ab躭쒯쮱쒳펵", a_));
				IL_97:
				this.ᜅ.ᜀ(value);
			}
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x000E05F8 File Offset: 0x000DF5F8
		internal override void ParseData(BiffRecordRaw record, IList<BiffRecordRaw> data, ref int iPos)
		{
			int a_ = 3;
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_56;
				case 1:
					goto IL_BB;
				case 2:
					goto IL_120;
				case 3:
				{
					TBIFFRecord typeCode;
					switch (typeCode)
					{
					case TBIFFRecord.ChartValueRange:
					case TBIFFRecord.ChartCatserRange:
						goto IL_125;
					default:
						num = 8;
						continue;
					}
					break;
				}
				case 4:
				{
					if (data == null)
					{
						num = 1;
						continue;
					}
					TBIFFRecord typeCode = record.TypeCode;
					num = 5;
					continue;
				}
				case 5:
				{
					TBIFFRecord typeCode;
					if (typeCode != TBIFFRecord.ChartAxisOffset)
					{
						num = 9;
						continue;
					}
					goto IL_C0;
				}
				case 6:
				{
					TBIFFRecord typeCode;
					if (typeCode == TBIFFRecord.ChartAxcext)
					{
						num = 2;
						continue;
					}
					goto IL_16E;
				}
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					break;
				case 9:
					num = 3;
					continue;
				}
				IL_41:
				if (record == null)
				{
					num = 0;
					continue;
				}
				num = 4;
				continue;
				goto IL_41;
			}
			IL_56:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䬸帺帼倾㍀❂", a_));
			IL_BB:
			throw new ArgumentNullException(RecordTableEnumerator.b("崸娺䤼帾", a_));
			IL_C0:
			this.ᜇ = ((spr\u201D)record).ᜁ();
			return;
			IL_120:
			this.ᜅ = (sprᮔ)record;
			this.ᜀ(this.ᜅ);
			return;
			IL_125:
			this.ParseMaxCross(record);
			return;
			IL_16E:
			base.ParseData(record, data, ref iPos);
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x000E077C File Offset: 0x000DF77C
		internal override void ParseMaxCross(BiffRecordRaw record)
		{
			int a_ = 13;
			int num = 1;
			for (;;)
			{
				TBIFFRecord typeCode;
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					num = 4;
					continue;
				case 2:
					switch (typeCode)
					{
					case TBIFFRecord.ChartValueRange:
						goto IL_6D;
					case TBIFFRecord.ChartCatserRange:
						goto IL_3A;
					default:
						num = 0;
						continue;
					}
					break;
				case 3:
					goto IL_38;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_81;
					}
					goto Block_3;
				}
				if (record == null)
				{
					num = 3;
					continue;
				}
				typeCode = record.TypeCode;
				IL_81:
				num = 2;
			}
			IL_38:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄⑆♈㥊⥌", a_));
			IL_3A:
			this.ᜄ = (spr\u248C)record;
			return;
			IL_6D:
			base.ChartValueRange = (spr\u21D5)record;
			return;
			Block_3:
			if (false)
			{
			}
			throw new ApplicationException(RecordTableEnumerator.b("ᙂ⭄ⱆ❈⑊㩌ⅎ煐⅒ご㑖㙘⥚㥜罞ᕠᩢᕤɦ", a_));
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x000E086C File Offset: 0x000DF86C
		internal override void ParseWallsOrFloor(IList<BiffRecordRaw> data, ref int iPos)
		{
			int a_ = 2;
			if (data == null)
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
					if (true)
					{
					}
					throw new ArgumentNullException(RecordTableEnumerator.b("尷嬹䠻弽", a_));
				}
			}
			base.ParentXlsChart.Walls = new ChartWallOrFloor((spr\u2158)base.ReservedHandle, base.ParentXlsChart, true, data, ref iPos);
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x000E08F0 File Offset: 0x000DF8F0
		private void ᜀ(sprᮔ A_0)
		{
			int a_ = 14;
			if (true)
			{
			}
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_5E;
				case 2:
					goto IL_A9;
				case 3:
					goto IL_40;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5E;
					default:
						goto IL_86;
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
				IL_5E:
				if (A_0.ᜊ())
				{
					num = 4;
				}
				else
				{
					num = 2;
				}
			}
			IL_40:
			throw new ArgumentNullException(RecordTableEnumerator.b("㙃⍅⭇╉㹋⩍", a_));
			IL_86:
			if (false)
			{
			}
			this.ᜆ = CategoryType.Automatic;
			return;
			IL_A9:
			this.ᜆ = (A_0.ᜋ() ? CategoryType.Time : CategoryType.Category);
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x000E09BC File Offset: 0x000DF9BC
		[CLSCompliant(false)]
		internal override void SerializeDataToList(RecordArrayList records)
		{
			int a_ = 2;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_5F;
				case 2:
					if (this.IsChartBubbleOrScatter)
					{
						num = 0;
						continue;
					}
					goto IL_91;
				case 3:
					goto IL_34;
				}
				if (records == null)
				{
					num = 3;
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
			goto IL_7D;
			IL_5F:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_7D:
				throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹弻儽㈿♁㝃", a_));
			default:
				if (false)
				{
				}
				base.ᜀ(records, spr\u2426.ChartAxisType.CategoryAxis);
				return;
			}
			IL_91:
			this.ᜁ(records);
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x000E0A6C File Offset: 0x000DFA6C
		private void ᜁ(RecordArrayList A_0)
		{
			int a_ = 15;
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (base.IsPrimary)
					{
						num = 2;
						continue;
					}
					goto IL_1CE;
				case 1:
					goto IL_10D;
				case 2:
					base.ᜄ(A_0);
					this.SerializeWallsOrFloor(A_0);
					num = 1;
					continue;
				case 3:
				{
					spr\u201D spr_u201D = (spr\u201D)spr\u175E.ᜀ(TBIFFRecord.ChartAxisOffset);
					spr_u201D.ᜀ(this.Offset);
					A_0.ᜀ(spr_u201D);
					num = 6;
					continue;
				}
				case 4:
					if (this.ᜇ != 100)
					{
						num = 3;
						continue;
					}
					goto IL_77;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8C;
					default:
						goto IL_6F;
					}
					break;
				case 6:
					goto IL_77;
				}
				if (A_0 == null)
				{
					if (true)
					{
					}
					num = 5;
					continue;
				}
				spr\u2426 spr_u = (spr\u2426)spr\u175E.ᜀ(TBIFFRecord.ChartAxis);
				spr_u.ᜀ(spr\u2426.ChartAxisType.CategoryAxis);
				A_0.ᜀ(spr_u);
				A_0.ᜀ(spr\u175E.ᜀ(TBIFFRecord.Begin));
				A_0.ᜀ((BiffRecordRaw)this.ᜄ.Clone());
				this.ᜀ(A_0);
				base.ᜂ(A_0);
				sprᱬ sprᱬ = new sprᱬ();
				sprᱬ.ᜀ(2134);
				sprᱬ.ᜀ = new byte[]
				{
					86,
					8,
					0,
					0,
					100,
					0,
					2,
					0,
					236,
					43,
					0,
					0
				};
				sprᱬ.Length = sprᱬ.ᜀ.Length;
				A_0.ᜀ(sprᱬ);
				num = 4;
				continue;
				IL_8C:
				num = 0;
				continue;
				IL_77:
				base.ᜆ(A_0);
				base.ᜃ(A_0);
				base.ᜅ(A_0);
				goto IL_8C;
			}
			IL_6F:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㝄≆⩈⑊㽌⭎≐", a_));
			IL_10D:
			IL_1CE:
			A_0.ᜀ(spr\u175E.ᜀ(TBIFFRecord.End));
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x000E0C58 File Offset: 0x000DFC58
		internal override void SerializeWallsOrFloor(RecordArrayList records)
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
			base.ParentXlsChart.ᜌ(records);
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x000E0CA0 File Offset: 0x000DFCA0
		private void ᜀ(RecordArrayList A_0)
		{
			int a_ = 16;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_A7;
				case 2:
					goto IL_34;
				case 3:
					goto IL_78;
				}
				if (A_0 == null)
				{
					num = 2;
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
				IL_78:
				this.ᜅ.ᜄ(this.ᜆ == CategoryType.Automatic);
				if (true)
				{
				}
				num = 0;
			}
			IL_34:
			throw new ArgumentNullException(RecordTableEnumerator.b("㑅ⵇ⥉⍋㱍㑏⅑", a_));
			IL_A7:
			this.ᜅ.ᜃ(this.ᜆ == CategoryType.Time);
			A_0.ᜀ(this.ᜅ);
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x000E0D80 File Offset: 0x000DFD80
		protected override void InitializeVariables()
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
			this.ᜄ = (spr\u248C)spr\u175E.ᜀ(TBIFFRecord.ChartCatserRange);
			base.ChartValueRange = (spr\u21D5)spr\u175E.ᜀ(TBIFFRecord.ChartValueRange);
			base.InitializeVariables();
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x000E0DEC File Offset: 0x000DFDEC
		protected override bool CheckValueRangeRecord(bool throwException)
		{
			int a_ = 4;
			for (;;)
			{
				bool flag = this.IsChartBubbleOrScatter;
				if (true)
				{
				}
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 3;
						continue;
					case 1:
						goto IL_95;
					case 2:
						if (throwException)
						{
							num = 0;
							continue;
						}
						return flag;
					case 3:
						if (flag)
						{
							return flag;
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
							num = 1;
							continue;
						}
						break;
					}
					break;
				}
			}
			IL_95:
			throw new NotSupportedException(RecordTableEnumerator.b("渹吻圽㌿扁㑃㑅❇㩉⥋㱍⑏⭑瑓㽕⭗穙㉛ㅝᑟ䉡ᝣ፥ᡧᩩͫᱭѯ᝱ၳ噵ṷᕹ๻幽ꚅﾉﺋﲍﲑ뚕ﮗﶛ풟芡킣\udfa5\ud8a7쾩", a_));
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x000E0E94 File Offset: 0x000DFE94
		public override XlsChartAxis Clone(object parent, Dictionary<int, int> dicFontIndexes, Dictionary<string, string> dicNewSheetNames)
		{
			if (true)
			{
			}
			XlsChartCategoryAxis xlsChartCategoryAxis;
			for (;;)
			{
				xlsChartCategoryAxis = (XlsChartCategoryAxis)base.Clone(parent, dicFontIndexes, dicNewSheetNames);
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
							goto IL_4F;
						default:
							if (false)
							{
							}
							xlsChartCategoryAxis.ᜅ = (sprᮔ)this.ᜅ.Clone();
							num = 2;
							continue;
						}
						break;
					case 1:
						goto IL_90;
					case 2:
						return xlsChartCategoryAxis;
					case 3:
						if (this.ᜄ != null)
						{
							num = 5;
							continue;
						}
						goto IL_90;
					case 4:
						if (this.ᜅ != null)
						{
							num = 0;
							continue;
						}
						return xlsChartCategoryAxis;
					case 5:
						goto IL_4F;
					}
					break;
					IL_4F:
					xlsChartCategoryAxis.ᜄ = (spr\u248C)this.ᜄ.Clone();
					num = 1;
					continue;
					IL_90:
					num = 4;
				}
			}
			return xlsChartCategoryAxis;
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x000E0F80 File Offset: 0x000DFF80
		private string ᜃ()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					XlsChartSeries xlsChartSeries = base.ParentXlsChart.Series;
					int num = 6;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_60;
						case 1:
							goto IL_D9;
						case 2:
							goto IL_C6;
						case 3:
						{
							string startType;
							string startType2;
							if (startType != startType2)
							{
								num = 2;
								continue;
							}
							int num2;
							num2++;
							num = 1;
							continue;
						}
						case 4:
						{
							string startType2;
							return startType2;
						}
						case 5:
						{
							int num2;
							int count;
							if (num2 >= count)
							{
								num = 4;
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
								XlsChartSerie xlsChartSerie = (XlsChartSerie)xlsChartSeries[num2];
								string startType = xlsChartSerie.StartType;
								num = 3;
								continue;
							}
							}
							break;
						}
						case 6:
						{
							if (xlsChartSeries.Count == 0)
							{
								num = 0;
								continue;
							}
							string startType2 = (xlsChartSeries[0] as XlsChartSerie).StartType;
							int num2 = 1;
							int count = xlsChartSeries.Count;
							if (true)
							{
							}
							num = 7;
							continue;
						}
						case 7:
							goto IL_D9;
						}
						break;
						IL_D9:
						num = 5;
					}
				}
				IL_60:
				return XlsChartFormat.ᜉ(base.ParentXlsChart.ChartType);
				IL_C6:
				return XlsChartFormat.ᜉ(ExcelChartType.CombinationChart);
			}
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x000E10CC File Offset: 0x000E00CC
		private void ᜂ()
		{
			int a_ = 11;
			int num = 1;
			for (;;)
			{
				IL_13:
				switch (num)
				{
				case 0:
					goto IL_98;
				case 2:
					while (this.IsChartBubbleOrScatter)
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
							if (true)
							{
							}
							num = 0;
							goto IL_13;
						}
					}
					return;
				case 3:
					num = 2;
					continue;
				}
				if (this.IsCategoryType)
				{
					break;
				}
				num = 3;
			}
			IL_3B:
			throw new NotSupportedException(RecordTableEnumerator.b("ɀ㙂㝄㕆ⱈ╊㥌潎㉐㭒㑔╖ⵘ筚㥜ぞѠၢ୤ࡦᵨ䭪ṬᩮŰͲᩴն൸孺ॼ᝾ꖄﮈﶌ릖", a_));
			IL_98:
			goto IL_3B;
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06001750 RID: 5968 RVA: 0x000E1174 File Offset: 0x000E0174
		private spr\u248C CatserRecord
		{
			get
			{
				if (true)
				{
				}
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_79;
					case 1:
						goto IL_5C;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_5C;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					}
					if (this.ᜄ == null)
					{
						num = 1;
						continue;
					}
					break;
					IL_5C:
					this.ᜄ = (spr\u248C)spr\u175E.ᜀ(TBIFFRecord.ChartCatserRange);
					num = 0;
				}
				IL_79:
				return this.ᜄ;
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06001751 RID: 5969 RVA: 0x000E1204 File Offset: 0x000E0204
		internal bool IsChartBubbleOrScatter
		{
			get
			{
				int a_ = 2;
				string a = this.ᜃ();
				if (!(a == RecordTableEnumerator.b("稷伹帻尽ⰿ❁", a_)))
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
					return a == RecordTableEnumerator.b("欷夹崻䨽㐿❁㙃", a_);
				}
				return true;
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06001752 RID: 5970 RVA: 0x000E1280 File Offset: 0x000E0280
		private bool IsCategoryType
		{
			get
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
				return this.ᜆ == CategoryType.Category;
			}
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x000E12C4 File Offset: 0x000E02C4
		// Note: this type is marked as 'beforefieldinit'.
		static XlsChartCategoryAxis()
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
			XlsChartCategoryAxis.ᜃ = new DateTime(1900, 1, 1);
		}

		// Token: 0x04000F90 RID: 3984
		private new const string ᜀ = "This property is not supported for the current chart type";

		// Token: 0x04000F91 RID: 3985
		private new const int ᜁ = 100;

		// Token: 0x04000F92 RID: 3986
		private byte \u25D8\u0087\u008D\u0086;

		// Token: 0x04000F93 RID: 3987
		private bool \u2609\u00A1\u008A\u0094;

		// Token: 0x04000F94 RID: 3988
		private new const int ᜂ = 12;

		// Token: 0x04000F95 RID: 3989
		private new static readonly DateTime ᜃ;

		// Token: 0x04000F96 RID: 3990
		private new spr\u248C ᜄ;

		// Token: 0x04000F97 RID: 3991
		private new sprᮔ ᜅ = (sprᮔ)spr\u175E.ᜀ(TBIFFRecord.ChartAxcext);

		// Token: 0x04000F98 RID: 3992
		private new CategoryType ᜆ = CategoryType.Automatic;

		// Token: 0x04000F99 RID: 3993
		private new int ᜇ = 100;

		// Token: 0x04000F9A RID: 3994
		private bool \u25D9\u00A3\u0084\u0098;

		// Token: 0x04000F9B RID: 3995
		private bool ᜈ;
	}
}
