using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using Spire.Xls.Charts;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Charts
{
	// Token: 0x020001A7 RID: 423
	public class XlsChartSerie : XlsObject, IChartSerie, spr\u252A, sprṨ
	{
		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060015B6 RID: 5558 RVA: 0x000CCBF0 File Offset: 0x000CBBF0
		// (remove) Token: 0x060015B7 RID: 5559 RVA: 0x000CCC88 File Offset: 0x000CBC88
		public event XlsEventHandler ValueRangeChanged
		{
			add
			{
				for (;;)
				{
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
						{
							if (true)
							{
							}
							if (false)
							{
							}
							XlsEventHandler xlsEventHandler = this.ᜅ;
							int num = 0;
							for (;;)
							{
								XlsEventHandler xlsEventHandler2;
								switch (num)
								{
								case 0:
									goto IL_49;
								case 1:
									if (xlsEventHandler == xlsEventHandler2)
									{
										num = 2;
										continue;
									}
									goto IL_49;
								case 2:
									return;
								}
								break;
								IL_49:
								xlsEventHandler2 = xlsEventHandler;
								XlsEventHandler value2 = (XlsEventHandler)Delegate.Combine(xlsEventHandler2, value);
								xlsEventHandler = Interlocked.CompareExchange<XlsEventHandler>(ref this.ᜅ, value2, xlsEventHandler2);
								num = 1;
							}
							break;
						}
						}
					}
				}
			}
			remove
			{
				for (;;)
				{
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
						{
							if (true)
							{
							}
							if (false)
							{
							}
							XlsEventHandler xlsEventHandler = this.ᜅ;
							int num = 2;
							for (;;)
							{
								XlsEventHandler xlsEventHandler2;
								switch (num)
								{
								case 0:
									return;
								case 1:
									if (xlsEventHandler == xlsEventHandler2)
									{
										num = 0;
										continue;
									}
									goto IL_49;
								case 2:
									goto IL_49;
								}
								break;
								IL_49:
								xlsEventHandler2 = xlsEventHandler;
								XlsEventHandler value2 = (XlsEventHandler)Delegate.Remove(xlsEventHandler2, value);
								xlsEventHandler = Interlocked.CompareExchange<XlsEventHandler>(ref this.ᜅ, value2, xlsEventHandler2);
								num = 1;
							}
							break;
						}
						}
					}
				}
			}
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x000CCD20 File Offset: 0x000CBD20
		internal XlsChartSerie(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜆ();
			this.ᜄ();
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x000CCD80 File Offset: 0x000CBD80
		internal XlsChartSerie(spr\u1DF5 A_0, object A_1, IList<BiffRecordRaw> A_2, ref int A_3) : this(A_0, A_1)
		{
			this.ᜃ(A_2, ref A_3);
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x060015BA RID: 5562 RVA: 0x000CCDA0 File Offset: 0x000CBDA0
		// (set) Token: 0x060015BB RID: 5563 RVA: 0x000CCDE4 File Offset: 0x000CBDE4
		public string Name
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
				return this.ᜁ();
			}
			set
			{
				int a_ = 13;
				int num = 7;
				for (;;)
				{
					string a;
					switch (num)
					{
					case 0:
						return;
					case 1:
						if (this.ᜏ.ChartTitle.Length == 0)
						{
							num = 8;
							continue;
						}
						return;
					case 2:
						if (value[0] == '=')
						{
							num = 15;
							continue;
						}
						goto IL_1C3;
					case 3:
						num = 12;
						continue;
					case 4:
						goto IL_1C3;
					case 5:
						num = 9;
						continue;
					case 6:
						num = 23;
						continue;
					case 8:
						goto IL_16B;
					case 9:
						if (this.ᜐ.Count != 1)
						{
							num = 14;
							continue;
						}
						goto IL_1A2;
					case 10:
						if (!(a == RecordTableEnumerator.b("ፂⱄ≆", a_)))
						{
							num = 3;
							continue;
						}
						goto IL_1A2;
					case 11:
						goto IL_148;
					case 12:
						if (a == RecordTableEnumerator.b("݂⩄㉆⹈⍊⍌㩎═", a_))
						{
							num = 11;
							continue;
						}
						return;
					case 13:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_148;
						default:
							if (false)
							{
							}
							if (this.ᜏ.ChartTitle != null)
							{
								num = 22;
								continue;
							}
							goto IL_16B;
						}
						break;
					case 14:
						num = 10;
						continue;
					case 15:
						this.ᜊ = this.ᜀ();
						num = 4;
						continue;
					case 16:
						this.ᜁ(value);
						this.ᜉ = value;
						this.\u1712 = false;
						num = 18;
						continue;
					case 17:
						if (!this.\u1712)
						{
							num = 5;
							continue;
						}
						return;
					case 18:
						if (value != null)
						{
							num = 6;
							continue;
						}
						goto IL_1C3;
					case 19:
						if (!this.ᜏ.Loading)
						{
							num = 20;
							continue;
						}
						return;
					case 20:
						num = 13;
						continue;
					case 21:
						num = 2;
						continue;
					case 22:
						num = 1;
						continue;
					case 23:
						if (value.Length > 0)
						{
							num = 21;
							continue;
						}
						goto IL_1C3;
					}
					if (this.ᜉ != value)
					{
						num = 16;
						continue;
					}
					break;
					IL_16B:
					a = XlsChartFormat.ᜉ(this.ᜏ.ChartType);
					num = 17;
					continue;
					IL_1A2:
					this.ᜏ.ChartTitle = this.Name;
					num = 0;
					continue;
					IL_148:
					goto IL_1A2;
					IL_1C3:
					num = 19;
				}
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x060015BC RID: 5564 RVA: 0x000CD0B8 File Offset: 0x000CC0B8
		public CellRange NamedRange
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
				this.ᜁ();
				return this.\u171A as CellRange;
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x060015BD RID: 5565 RVA: 0x000CD108 File Offset: 0x000CC108
		// (set) Token: 0x060015BE RID: 5566 RVA: 0x000CD14C File Offset: 0x000CC14C
		public IXLSRange Values
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
				return this.ᜆ;
			}
			set
			{
				int a_ = 7;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						return;
					case 2:
					{
						this.\u1714.Clear();
						XlsEventArgs a_2 = new XlsEventArgs(this.ᜆ, value, RecordTableEnumerator.b("欼帾ⵀ㙂⁄ᕆ⡈╊⩌⩎", a_));
						this.ᜆ = value;
						this.ᜀ(a_2);
						num = 1;
						continue;
					}
					}
					IL_25:
					if (true)
					{
					}
					if (this.ᜆ == value)
					{
						break;
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
						num = 2;
						continue;
					}
					goto IL_25;
				}
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x060015BF RID: 5567 RVA: 0x000CD200 File Offset: 0x000CC200
		// (set) Token: 0x060015C0 RID: 5568 RVA: 0x000CD244 File Offset: 0x000CC244
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
				return this.ᜇ;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.\u1715.Clear();
						this.ᜇ = value;
						this.ᜃ();
						num = 2;
						continue;
					case 2:
						return;
					}
					IL_1C:
					if (true)
					{
					}
					if (this.ᜇ == value)
					{
						break;
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
					goto IL_1C;
				}
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x060015C1 RID: 5569 RVA: 0x000CD2D0 File Offset: 0x000CC2D0
		// (set) Token: 0x060015C2 RID: 5570 RVA: 0x000CD314 File Offset: 0x000CC314
		public IXLSRange Bubbles
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
				return this.ᜈ;
			}
			set
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
						return;
					case 1:
						this.\u1716.Clear();
						this.ᜈ = value;
						this.ᜂ();
						num = 0;
						continue;
					}
					IL_24:
					if (this.ᜈ == value)
					{
						break;
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
					goto IL_24;
				}
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x060015C3 RID: 5571 RVA: 0x000CD3A0 File Offset: 0x000CC3A0
		// (set) Token: 0x060015C4 RID: 5572 RVA: 0x000CD3E4 File Offset: 0x000CC3E4
		public int RealIndex
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
				return this.Index;
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
				this.Index = value;
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x060015C5 RID: 5573 RVA: 0x000CD428 File Offset: 0x000CC428
		public IChartDataPoints DataPoints
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.m_dataPoints = new ChartDataPointsCollection((spr\u2158)base.ReservedHandle, this);
						num = 2;
						continue;
					case 2:
						goto IL_71;
					}
					IL_1C:
					if (true)
					{
					}
					if (this.m_dataPoints != null)
					{
						break;
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
					goto IL_1C;
				}
				IL_71:
				return this.m_dataPoints;
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x060015C6 RID: 5574 RVA: 0x000CD4B8 File Offset: 0x000CC4B8
		public IChartSerieDataFormat Format
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
				return this.m_dataPoints.DefaultDataPoint.DataFormat;
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x060015C7 RID: 5575 RVA: 0x000CD504 File Offset: 0x000CC504
		// (set) Token: 0x060015C8 RID: 5576 RVA: 0x000CD548 File Offset: 0x000CC548
		public ExcelChartType SerieType
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
				return this.ᜋ();
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
				this.ᜁ(value, false);
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x060015C9 RID: 5577 RVA: 0x000CD58C File Offset: 0x000CC58C
		// (set) Token: 0x060015CA RID: 5578 RVA: 0x000CD5D4 File Offset: 0x000CC5D4
		public bool UsePrimaryAxis
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
				XlsChartFormat commonSerieFormat = this.GetCommonSerieFormat();
				return commonSerieFormat.IsPrimaryAxis;
			}
			set
			{
				int a_ = 17;
				int num = 7;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_83;
					case 1:
						this.ᜀ(value);
						this.ᜏ.IsManuallyFormatted = true;
						num = 0;
						continue;
					case 2:
						if (value != this.UsePrimaryAxis)
						{
							num = 1;
							continue;
						}
						goto IL_83;
					case 3:
						if (value)
						{
							goto IL_111;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_E5;
						default:
							if (false)
							{
							}
							num = 8;
							continue;
						}
						break;
					case 4:
						if (!this.ᜐ.ᜈ())
						{
							num = 10;
							continue;
						}
						goto IL_159;
					case 5:
						goto IL_FB;
					case 6:
						goto IL_60;
					case 8:
						this.ᜏ.SecondaryParentAxis.ᜁ(true);
						num = 9;
						continue;
					case 9:
						goto IL_111;
					case 10:
						goto IL_E5;
					}
					if (Array.IndexOf<ExcelChartType>(XlsChart.ᜫ, this.SerieType) == -1)
					{
						num = 6;
						continue;
					}
					num = 2;
					continue;
					IL_83:
					num = 3;
					continue;
					IL_E5:
					this.ᜏ.ᜣ();
					num = 5;
					continue;
					IL_111:
					num = 4;
				}
				IL_60:
				throw new NotSupportedException(RecordTableEnumerator.b("ቆ㩈⹊ᵌ㵎㡐㹒㑔╖⁘ᩚ╜㙞በ䍢٤٦ݨժɬ᭮兰ᅲၴ坶੸๺ർཾꮊ뎒ﾖ붜쒠톢첤슦覨\udfaa풬\udfae풰", a_));
				IL_FB:
				IL_159:
				if (true)
				{
				}
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x060015CB RID: 5579 RVA: 0x000CD744 File Offset: 0x000CC744
		// (set) Token: 0x060015CC RID: 5580 RVA: 0x000CD7D0 File Offset: 0x000CC7D0
		public object[] EnteredDirectlyValues
		{
			get
			{
				int num = 2;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						this.\u1717 = this.ᜁ(this.\u1714);
						num = 1;
						continue;
					case 1:
						goto IL_6C;
					}
					IL_24:
					if (this.\u1717 != null)
					{
						break;
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
					goto IL_24;
				}
				IL_6C:
				return this.\u1717;
			}
			set
			{
				int a_ = 6;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (value.Length == 0)
						{
							num = 1;
							continue;
						}
						goto IL_8F;
					case 1:
						goto IL_8D;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_8F;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					}
					if (value == null)
					{
						break;
					}
					num = 2;
				}
				IL_5C:
				if (true)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("䨻弽ⰿ㝁⅃", a_));
				IL_8D:
				goto IL_5C;
				IL_8F:
				bool a_2 = this.ᜀ(value);
				this.\u1714 = this.ᜀ(a_2, value);
				this.\u1717 = value;
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x060015CD RID: 5581 RVA: 0x000CD88C File Offset: 0x000CC88C
		// (set) Token: 0x060015CE RID: 5582 RVA: 0x000CD918 File Offset: 0x000CC918
		public object[] EnteredDirectlyCategoryLabels
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.\u1718 = this.ᜁ(this.\u1715);
						if (true)
						{
						}
						num = 2;
						continue;
					case 2:
						goto IL_6C;
					}
					IL_1C:
					if (this.\u1718 != null)
					{
						break;
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
					goto IL_1C;
				}
				IL_6C:
				return this.\u1718;
			}
			set
			{
				int a_ = 19;
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
							goto IL_8F;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 1:
						goto IL_8D;
					case 2:
						if (true)
						{
						}
						break;
					case 3:
						if (value.Length == 0)
						{
							num = 1;
							continue;
						}
						goto IL_8F;
					}
					if (value == null)
					{
						break;
					}
					num = 0;
				}
				IL_64:
				throw new ArgumentNullException(RecordTableEnumerator.b("㽈⩊⅌㩎㑐", a_));
				IL_8D:
				goto IL_64;
				IL_8F:
				bool a_2 = this.ᜀ(value);
				this.\u1715 = this.ᜀ(a_2, value);
				this.\u1718 = value;
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x060015CF RID: 5583 RVA: 0x000CD9D4 File Offset: 0x000CC9D4
		// (set) Token: 0x060015D0 RID: 5584 RVA: 0x000CDA60 File Offset: 0x000CCA60
		public object[] EnteredDirectlyBubbles
		{
			get
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
						this.\u1719 = this.ᜁ(this.\u1716);
						num = 2;
						continue;
					case 2:
						goto IL_6C;
					}
					IL_1C:
					if (this.\u1719 != null)
					{
						break;
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
					goto IL_1C;
				}
				IL_6C:
				return this.\u1719;
			}
			set
			{
				int a_ = 0;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_85;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_87;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 3:
						if (value.Length == 0)
						{
							num = 0;
							continue;
						}
						goto IL_87;
					}
					if (value == null)
					{
						break;
					}
					num = 1;
				}
				IL_5C:
				throw new ArgumentNullException(RecordTableEnumerator.b("䀵夷嘹䤻嬽", a_));
				IL_85:
				goto IL_5C;
				IL_87:
				if (true)
				{
				}
				this.Bubbles = null;
				bool a_2 = this.ᜀ(value);
				this.\u1716 = this.ᜀ(a_2, value);
				this.\u1719 = value;
			}
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x060015D1 RID: 5585 RVA: 0x000CDB20 File Offset: 0x000CCB20
		public IChartErrorBars ErrorBarsY
		{
			get
			{
				int a_ = 3;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					if (this.\u171C == null)
					{
						throw new ApplicationException(RecordTableEnumerator.b("永䠺堼Ἶी≂㙄Ɇ㭈㥊≌㵎ፐ㉒❔⑖X筚ⵜⵞ๠።dᕦᵨቪ䵬᭮Ṱ卲ᙴնᱸ᩺ॼ᩾ꆀ力권릖", a_));
					}
					break;
				}
				if (true)
				{
				}
				return this.\u171C;
			}
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x060015D2 RID: 5586 RVA: 0x000CDB88 File Offset: 0x000CCB88
		// (set) Token: 0x060015D3 RID: 5587 RVA: 0x000CDBD0 File Offset: 0x000CCBD0
		public bool HasErrorBarsY
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
				return this.\u171C != null;
			}
			set
			{
				int a_ = 14;
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
							goto IL_F6;
						default:
							goto IL_A8;
						}
						break;
					case 1:
						if (!this.ᜏ.IsChart3D)
						{
							num = 5;
							continue;
						}
						goto IL_5E;
					case 2:
						return;
					case 4:
						num = 9;
						continue;
					case 5:
						num = 7;
						continue;
					case 6:
						this.\u171C = new spr\u237B((spr\u2158)base.ReservedHandle, this, true);
						num = 2;
						continue;
					case 7:
					{
						string value2;
						if (Array.IndexOf<string>(XlsChart.DEF_SUPPORT_ERROR_BARS, value2) == -1)
						{
							num = 10;
							continue;
						}
						num = 8;
						continue;
					}
					case 8:
						if (true)
						{
						}
						if (this.\u171C == null)
						{
							num = 6;
							continue;
						}
						return;
					case 9:
					{
						if (!value)
						{
							goto IL_F6;
						}
						string value2 = XlsChartFormat.ᜉ(this.SerieType);
						num = 1;
						continue;
					}
					case 10:
						goto IL_90;
					}
					if (this.HasErrorBarsY != value)
					{
						num = 4;
						continue;
					}
					return;
					IL_F6:
					num = 0;
				}
				IL_5E:
				throw new NotSupportedException(RecordTableEnumerator.b("݃㍅㩇㡉⥋⁍⑏牑❓㍕⩗㍙㥛繝џൡţᕥ٧թᡫ乭ͯݱѳٵ᝷ࡹࡻ幽\ud97fꊁ慎ﺋ꺍뚗", a_));
				IL_90:
				goto IL_5E;
				IL_A8:
				if (false)
				{
				}
				this.\u171C = null;
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x000CDD40 File Offset: 0x000CCD40
		public IChartErrorBars ErrorBarsX
		{
			get
			{
				int a_ = 13;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					if (this.\u171D == null)
					{
						if (true)
						{
						}
						throw new ApplicationException(RecordTableEnumerator.b("ᙂ㙄≆楈͊ⱌ㱎ᑐ⅒❔㡖⭘ᥚ㱜ⵞበ㭢䕤ᝦ᭨Ѫᵬ੮Ͱݲ౴坶൸ᑺ嵼᱾ꮊﶎﲒ랖ﮘ漢辠", a_));
					}
					break;
				}
				return this.\u171D;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x060015D5 RID: 5589 RVA: 0x000CDDA8 File Offset: 0x000CCDA8
		// (set) Token: 0x060015D6 RID: 5590 RVA: 0x000CDDF0 File Offset: 0x000CCDF0
		public bool HasErrorBarsX
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
				return this.\u171D != null;
			}
			set
			{
				int a_ = 14;
				int num = 9;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_F6;
					case 1:
						num = 7;
						continue;
					case 2:
						if (this.\u171D == null)
						{
							num = 8;
							continue;
						}
						return;
					case 3:
						if (XlsChartFormat.ᜉ(this.SerieType) != RecordTableEnumerator.b("ك㍅⩇⡉⁋⭍", a_))
						{
							num = 5;
							continue;
						}
						goto IL_116;
					case 4:
						num = 3;
						continue;
					case 5:
						goto IL_AD;
					case 6:
						goto IL_114;
					case 7:
						if (!value)
						{
							num = 6;
							continue;
						}
						num = 10;
						continue;
					case 8:
						this.\u171D = new spr\u237B((spr\u2158)base.ReservedHandle, this, false);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_116;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					case 9:
						if (true)
						{
						}
						break;
					case 10:
						if (XlsChartFormat.ᜉ(this.SerieType) != RecordTableEnumerator.b("ᝃ╅⥇㹉㡋⭍≏", a_))
						{
							num = 4;
							continue;
						}
						goto IL_116;
					}
					if (this.HasErrorBarsX != value)
					{
						num = 1;
						continue;
					}
					return;
					IL_116:
					num = 2;
				}
				IL_AD:
				throw new NotSupportedException(RecordTableEnumerator.b("݃㍅㩇㡉⥋⁍⑏牑❓㍕⩗㍙㥛繝џൡţᕥ٧թᡫ乭ͯݱѳٵ᝷ࡹࡻ幽\ud87fꊁ慎ﺋ꺍뚗", a_));
				IL_F6:
				return;
				IL_114:
				this.\u171D = null;
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x000CDF7C File Offset: 0x000CCF7C
		public IChartTrendLines TrendLines
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
				return this.\u171E;
			}
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x000CDFC0 File Offset: 0x000CCFC0
		public IChartErrorBars ErrorBar(bool bIsY)
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
			return this.ErrorBar(bIsY, ErrorBarIncludeType.Both);
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x000CE004 File Offset: 0x000CD004
		public IChartErrorBars ErrorBar(bool bIsY, ErrorBarIncludeType include)
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
			return this.ErrorBar(bIsY, include, ErrorBarType.Fixed);
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x000CE048 File Offset: 0x000CD048
		public IChartErrorBars ErrorBar(bool bIsY, ErrorBarIncludeType include, ErrorBarType type)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 2;
					continue;
				case 2:
					goto IL_65;
				case 3:
					goto IL_35;
				}
				if (!bIsY)
				{
					num = 0;
				}
				else
				{
					num = 3;
				}
			}
			IL_35:
			if (true)
			{
			}
			double num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_65:
				num2 = (double)1;
				break;
			default:
				if (false)
				{
				}
				num2 = (double)10;
				break;
			}
			double numberValue = num2;
			return this.ErrorBar(bIsY, include, type, numberValue);
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x000CE0D4 File Offset: 0x000CD0D4
		public IChartErrorBars ErrorBar(bool bIsY, ErrorBarIncludeType include, ErrorBarType type, double numberValue)
		{
			int a_ = 6;
			int num = 3;
			spr\u237B spr_u237B;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.HasErrorBarsY = true;
					spr_u237B = this.\u171C;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7B;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 1:
					goto IL_45;
				case 2:
					goto IL_79;
				case 4:
					goto IL_C9;
				case 5:
					if (bIsY)
					{
						num = 0;
						continue;
					}
					this.HasErrorBarsX = true;
					spr_u237B = this.\u171D;
					num = 4;
					continue;
				}
				if (true)
				{
				}
				if (type == ErrorBarType.Custom)
				{
					num = 1;
					continue;
				}
				IL_7B:
				spr_u237B = null;
				num = 5;
			}
			IL_45:
			throw new ArgumentException(RecordTableEnumerator.b("稻儽㈿扁㝃⍅㱇㥉汋ⵍ╏⅑⁓㥕㕗穙⡛❝ၟݡ䑣፥᭧ཀྵ䱫཭ṯᵱsṵᵷࡹ屻ᅽ꺍ﶏﺕﺙ", a_));
			IL_79:
			IL_C9:
			spr_u237B.ᜀ(type);
			spr_u237B.ᜁ(include);
			spr_u237B.ᜀ(numberValue);
			spr_u237B.ᜁ().UseDefaultFormat = true;
			spr_u237B.ᜁ(true);
			return spr_u237B;
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x000CE1D8 File Offset: 0x000CD1D8
		public IChartErrorBars ErrorBar(bool bIsY, IXLSRange plusRange, IXLSRange minusRange)
		{
			int a_ = 11;
			spr\u237B spr_u237B;
			for (;;)
			{
				bool flag = plusRange != null;
				bool flag2 = minusRange != null;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_15A;
					case 1:
						spr_u237B.ᜂ(plusRange);
						num = 11;
						continue;
					case 2:
						num = 12;
						continue;
					case 3:
						if (!flag)
						{
							num = 2;
							continue;
						}
						goto IL_186;
					case 4:
						goto IL_120;
					case 5:
						goto IL_11E;
					case 6:
						this.HasErrorBarsY = true;
						spr_u237B = this.\u171C;
						spr_u237B.ᜀ(10.0);
						num = 13;
						continue;
					case 7:
						if (flag2)
						{
							if (true)
							{
							}
							num = 8;
							continue;
						}
						goto IL_1A6;
					case 8:
						spr_u237B.ᜁ(minusRange);
						num = 0;
						continue;
					case 9:
						if (flag)
						{
							num = 1;
							continue;
						}
						goto IL_C3;
					case 10:
						if (bIsY)
						{
							num = 6;
							continue;
						}
						goto IL_85;
					case 11:
						goto IL_C3;
					case 12:
						if (!flag2)
						{
							num = 5;
							continue;
						}
						goto IL_186;
					case 13:
						goto IL_120;
					}
					break;
					IL_85:
					this.HasErrorBarsX = true;
					spr_u237B = this.\u171D;
					spr_u237B.ᜀ(1.0);
					num = 4;
					continue;
					IL_C3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_85;
					default:
						if (false)
						{
						}
						num = 7;
						continue;
					}
					IL_120:
					num = 9;
					continue;
					IL_186:
					spr_u237B = null;
					num = 10;
				}
			}
			IL_11E:
			throw new ArgumentException(RecordTableEnumerator.b("ᅀ⽂い㑆楈㥊ⱌⅎ㙐㙒畔㙖㝘㽚絜㉞ࡠൢၤᑦ䥨ᥪ౬Ůᙰᙲ啴ᙶ୸Ṻ嵼ᅾꞆﮈﮔﲘ떚", a_));
			IL_15A:
			IL_1A6:
			spr_u237B.ᜁ().UseDefaultFormat = true;
			spr_u237B.ᜁ(true);
			return spr_u237B;
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x000CE3A0 File Offset: 0x000CD3A0
		private void ᜃ(IList<BiffRecordRaw> A_0, ref int A_1)
		{
			int a_ = 18;
			int num = 8;
			for (;;)
			{
				BiffRecordRaw biffRecordRaw;
				switch (num)
				{
				case 0:
					goto IL_155;
				case 1:
				{
					TBIFFRecord typeCode;
					if (typeCode > TBIFFRecord.ChartSeriesText)
					{
						num = 7;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_16E;
					default:
						if (false)
						{
						}
						num = 25;
						continue;
					}
					break;
				}
				case 2:
					goto IL_21D;
				case 3:
				{
					TBIFFRecord typeCode;
					if (typeCode == TBIFFRecord.ChartAI)
					{
						num = 19;
						continue;
					}
					goto IL_1E1;
				}
				case 4:
					goto IL_2C8;
				case 5:
					num = 16;
					continue;
				case 6:
					goto IL_155;
				case 7:
				{
					TBIFFRecord typeCode;
					switch (typeCode)
					{
					case TBIFFRecord.ChartLegendxn:
						this.ᜀ(A_0, ref A_1);
						num = 14;
						continue;
					case TBIFFRecord.ChartShtprops:
						goto IL_1E1;
					case TBIFFRecord.ChartSertocrt:
						this.ᜁ(A_0, ref A_1);
						num = 0;
						continue;
					default:
						num = 12;
						continue;
					}
					break;
				}
				case 8:
					if (true)
					{
					}
					break;
				case 9:
				{
					TBIFFRecord typeCode;
					if (typeCode != TBIFFRecord.ChartSeriesText)
					{
						num = 5;
						continue;
					}
					num = 17;
					continue;
				}
				case 10:
					num = 9;
					continue;
				case 11:
					goto IL_119;
				case 12:
					goto IL_16E;
				case 13:
					goto IL_155;
				case 14:
					goto IL_155;
				case 15:
					goto IL_1F7;
				case 16:
					goto IL_1E1;
				case 17:
					if (this.ᜐ.Count == 0)
					{
						num = 22;
						continue;
					}
					goto IL_119;
				case 18:
					goto IL_155;
				case 19:
					this.ᜂ(A_0, ref A_1);
					num = 6;
					continue;
				case 20:
				{
					if (biffRecordRaw.TypeCode == TBIFFRecord.End)
					{
						num = 2;
						continue;
					}
					TBIFFRecord typeCode = biffRecordRaw.TypeCode;
					num = 1;
					continue;
				}
				case 21:
					goto IL_155;
				case 22:
					this.\u171B = ((spr\u1D35)biffRecordRaw).ᜁ();
					num = 11;
					continue;
				case 23:
					if (biffRecordRaw.TypeCode != TBIFFRecord.Begin)
					{
						num = 4;
						continue;
					}
					A_1++;
					this.ᜋ.Clear();
					this.m_dataPoints.Clear();
					biffRecordRaw = A_0[A_1];
					num = 15;
					continue;
				case 24:
					goto IL_A2;
				case 25:
					num = 27;
					continue;
				case 26:
					goto IL_1F7;
				case 27:
				{
					TBIFFRecord typeCode;
					if (typeCode != TBIFFRecord.ChartDataFormat)
					{
						num = 10;
						continue;
					}
					XlsChartSerieDataFormat xlsChartSerieDataFormat = new ChartSerieDataFormat((spr\u2158)base.ReservedHandle, this);
					A_1 = xlsChartSerieDataFormat.ᜀ(A_0, A_1);
					xlsChartSerieDataFormat.DataFormat.ᜅ();
					this.ᜀ(xlsChartSerieDataFormat);
					num = 21;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 24;
					continue;
				}
				biffRecordRaw = A_0[A_1];
				biffRecordRaw.CheckTypeCode(TBIFFRecord.ChartSeries);
				this.ᜀ((sprḠ)A_0[A_1]);
				A_1++;
				biffRecordRaw = A_0[A_1];
				num = 23;
				continue;
				IL_119:
				A_1++;
				num = 13;
				continue;
				IL_155:
				biffRecordRaw = A_0[A_1];
				num = 26;
				continue;
				IL_16E:
				num = 3;
				continue;
				IL_1E1:
				A_1++;
				num = 18;
				continue;
				IL_1F7:
				num = 20;
			}
			IL_A2:
			throw new ArgumentNullException(RecordTableEnumerator.b("ⱇ⭉㡋⽍", a_));
			IL_21D:
			this.ᜐ.TrendIndex++;
			A_1++;
			this.Reparse();
			return;
			IL_2C8:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ੇ⽉⭋❍㹏牑♓㍕㭗㕙⹛㩝䁟šգࡥ٧թᡫ乭ቯ᝱味ၵ᝷ཹቻ᩽깿", a_));
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x000CE76C File Offset: 0x000CD76C
		private void ᜀ(sprḠ A_0)
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
			this.ᜎ = A_0;
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x000CE7B0 File Offset: 0x000CD7B0
		private void ᜂ(IList<BiffRecordRaw> A_0, ref int A_1)
		{
			int a_ = 3;
			int num = 2;
			sprᢀ sprᢀ;
			for (;;)
			{
				BiffRecordRaw biffRecordRaw;
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6E;
					default:
						if (false)
						{
						}
						this.ᜀ(sprᢀ, A_0, ref A_1);
						num = 4;
						continue;
					}
					break;
				case 1:
					if (biffRecordRaw.TypeCode != TBIFFRecord.ChartAI)
					{
						num = 3;
						continue;
					}
					sprᢀ = (sprᢀ)biffRecordRaw;
					A_1++;
					goto IL_6E;
				case 3:
					goto IL_CF;
				case 4:
					goto IL_101;
				case 5:
					if (sprᢀ.ᜄ() == sprᢀ.LinkIndex.LinkToTitleOrText)
					{
						if (true)
						{
						}
						num = 0;
						continue;
					}
					goto IL_153;
				case 6:
					if (this.ᜋ.ContainsKey(sprᢀ.ᜄ()))
					{
						num = 7;
						continue;
					}
					num = 5;
					continue;
				case 7:
					goto IL_A1;
				case 8:
					goto IL_48;
				}
				if (A_0 == null)
				{
					num = 8;
					continue;
				}
				biffRecordRaw = A_0[A_1];
				num = 1;
				continue;
				IL_6E:
				num = 6;
			}
			IL_48:
			throw new ArgumentNullException(RecordTableEnumerator.b("崸娺䤼帾", a_));
			IL_A1:
			throw new ArgumentException(RecordTableEnumerator.b("紸为䴼匾⡀⁂⑄㍆ⱈ⽊浌๎ᡐ獒❔㉖㩘㑚⽜㭞䅠ୢѤᑦ䥨४࡬੮ὰ卲፴ᡶ౸ᕺ᥼兾", a_));
			IL_CF:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("稸区尼䴾㕀ɂౄ杆㭈⹊⹌⁎⍐㝒畔㑖㡘㕚㍜ぞᕠ䍢ݤɦ䥨൪ɬᩮὰᝲ孴", a_));
			IL_101:
			IL_153:
			this.ᜋ.Add(sprᢀ.ᜄ(), sprᢀ);
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x000CE924 File Offset: 0x000CD924
		private void ᜁ(IList<BiffRecordRaw> A_0, ref int A_1)
		{
			int a_ = 15;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_3C;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			if (A_0 != null)
			{
				BiffRecordRaw biffRecordRaw = A_0[A_1];
				biffRecordRaw.CheckTypeCode(TBIFFRecord.ChartSertocrt);
				this.ᜌ = (int)((sprὈ)biffRecordRaw).ᜁ();
				A_1++;
				return;
			}
			IL_3C:
			throw new ArgumentNullException(RecordTableEnumerator.b("⅄♆㵈⩊", a_));
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x000CE9AC File Offset: 0x000CD9AC
		private void ᜀ(sprᢀ A_0, IList<BiffRecordRaw> A_1, ref int A_2)
		{
			int a_ = 11;
			int num = 9;
			for (;;)
			{
				BiffRecordRaw biffRecordRaw;
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_17F;
					default:
						if (false)
						{
						}
						if (A_0.ᜁ() == sprᢀ.ReferenceType.Worksheet)
						{
							num = 3;
							continue;
						}
						return;
					}
					break;
				case 1:
					goto IL_17F;
				case 2:
					if (A_0.ᜁ() == sprᢀ.ReferenceType.NotUsed)
					{
						num = 5;
						continue;
					}
					goto IL_5A;
				case 3:
				{
					Ptg[] a_2 = A_0.ᜆ();
					this.ᜉ = RecordTableEnumerator.b("籀", a_) + this.\u170D.FormulaUtil.ᜁ(a_2);
					this.ᜊ = a_2;
					this.\u1712 = false;
					num = 8;
					continue;
				}
				case 4:
					num = 2;
					continue;
				case 5:
					goto IL_150;
				case 6:
					if (true)
					{
					}
					goto IL_5A;
				case 7:
					if (biffRecordRaw.TypeCode == TBIFFRecord.ChartSeriesText)
					{
						num = 1;
						continue;
					}
					goto IL_5A;
				case 8:
					return;
				}
				if (A_0.ᜁ() != sprᢀ.ReferenceType.EnteredDirectly)
				{
					num = 4;
					continue;
				}
				goto IL_150;
				IL_5A:
				num = 0;
				continue;
				IL_150:
				biffRecordRaw = A_1[A_2];
				num = 7;
				continue;
				IL_17F:
				this.ᜉ = ((spr\u1D35)biffRecordRaw).ᜁ();
				this.\u171B = this.ᜉ;
				this.\u1712 = false;
				A_2++;
				num = 6;
			}
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x000CEB40 File Offset: 0x000CDB40
		private void ᜀ(IList<BiffRecordRaw> A_0, ref int A_1)
		{
			int a_ = 3;
			int num = 4;
			XlsChartLegendEntry xlsChartLegendEntry;
			ChartLegendEntriesColl chartLegendEntriesColl;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 3;
					continue;
				case 1:
					goto IL_3C;
				case 2:
					goto IL_100;
				case 3:
					goto IL_6C;
				case 5:
					if (xlsChartLegendEntry.LegendEntityIndex != 65535)
					{
						num = 0;
						continue;
					}
					num = 2;
					continue;
				}
				if (A_0 == null)
				{
					num = 1;
				}
				else
				{
					if (true)
					{
					}
					this.ᜏ.HasLegend = true;
					chartLegendEntriesColl = (ChartLegendEntriesColl)this.ᜏ.Legend.LegendEntries;
					xlsChartLegendEntry = new ChartLegendEntry((spr\u2158)base.ReservedHandle, chartLegendEntriesColl, 0);
					xlsChartLegendEntry.ᜀ(A_0, ref A_1);
					num = 5;
				}
			}
			IL_3C:
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_100:
				num2 = this.ᜐ.Count;
				goto IL_10D;
			default:
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("崸娺䤼帾", a_));
			}
			IL_6C:
			num2 = xlsChartLegendEntry.LegendEntityIndex;
			IL_10D:
			int iIndex = num2;
			chartLegendEntriesColl.Add(iIndex, xlsChartLegendEntry);
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x000CEC64 File Offset: 0x000CDC64
		public void ParseErrorBars(IList data)
		{
			int a_ = 14;
			int num = 0;
			spr\u237B spr_u237B;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_81;
				case 2:
					for (;;)
					{
						if (true)
						{
						}
						if (spr_u237B.ᜌ())
						{
							break;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_AD;
						}
					}
					num = 1;
					continue;
				case 3:
					goto IL_34;
				}
				if (data == null)
				{
					num = 3;
				}
				else
				{
					spr_u237B = new spr\u237B((spr\u2158)base.ReservedHandle, this, data);
					num = 2;
				}
			}
			IL_34:
			throw new ArgumentNullException(RecordTableEnumerator.b("⁃❅㱇⭉", a_));
			IL_81:
			this.ᜀ(spr_u237B, ref this.\u171C);
			return;
			IL_AD:
			if (false)
			{
			}
			this.ᜀ(spr_u237B, ref this.\u171D);
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x000CED34 File Offset: 0x000CDD34
		private void ᜆ()
		{
			int a_ = 1;
			object obj;
			for (;;)
			{
				obj = base.FindParent(typeof(XlsWorkbook));
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_85;
					case 1:
						goto IL_77;
					case 2:
						goto IL_50;
					case 3:
						goto IL_12A;
					case 4:
						if (obj != null)
						{
							this.ᜏ = (XlsChart)obj;
							obj = base.FindParent(typeof(XlsChartSeries));
							num = 1;
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
							num = 3;
							continue;
						}
						break;
					case 5:
						if (obj == null)
						{
							num = 2;
							continue;
						}
						this.\u170D = (XlsWorkbook)obj;
						obj = base.FindParent(typeof(XlsChart));
						num = 4;
						continue;
					}
					break;
					IL_77:
					if (obj != null)
					{
						goto IL_12F;
					}
					num = 0;
				}
			}
			IL_50:
			throw new ArgumentNullException(RecordTableEnumerator.b("朶堸䤺堼儾㕀捂⩄╆⍈⹊⹌㭎煐げ㑔㥖㝘㑚⥜罞͠٢䕤Ŧ٨Ṫͬ୮彰", a_));
			IL_85:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("朶堸䤺堼儾㕀捂⩄╆⍈⹊⹌㭎煐げ㑔㥖㝘㑚⥜罞͠٢䕤Ŧ٨Ṫͬ୮彰", a_));
			IL_12A:
			throw new ArgumentNullException(RecordTableEnumerator.b("朶堸䤺堼儾㕀捂⩄╆⍈⹊⹌㭎煐げ㑔㥖㝘㑚⥜罞͠٢䕤Ŧ٨Ṫͬ୮彰", a_));
			IL_12F:
			this.ᜐ = (XlsChartSeries)obj;
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x000CEE7C File Offset: 0x000CDE7C
		private void ᜅ()
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
			sprᢀ sprᢀ = (sprᢀ)spr\u175E.ᜀ(TBIFFRecord.ChartAI);
			sprᢀ.ᜀ(sprᢀ.LinkIndex.LinkToTitleOrText);
			sprᢀ.ᜀ(sprᢀ.ReferenceType.NotUsed);
			this.ᜋ.Add(sprᢀ.ᜄ(), sprᢀ);
			sprᢀ = (sprᢀ)sprᢀ.Clone();
			sprᢀ.ᜀ(sprᢀ.LinkIndex.LinkToCategories);
			sprᢀ.ᜀ(sprᢀ.ReferenceType.DefaultCategories);
			this.ᜋ.Add(sprᢀ.ᜄ(), sprᢀ);
			sprᢀ = (sprᢀ)sprᢀ.Clone();
			sprᢀ.ᜀ(sprᢀ.LinkIndex.LinkToValues);
			sprᢀ.ᜀ(sprᢀ.ReferenceType.NotUsed);
			this.ᜋ.Add(sprᢀ.ᜄ(), sprᢀ);
			sprᢀ = (sprᢀ)sprᢀ.Clone();
			sprᢀ.ᜀ(sprᢀ.LinkIndex.LinkToBubbles);
			sprᢀ.ᜀ(sprᢀ.ReferenceType.NotUsed);
			this.ᜋ.Add(sprᢀ.ᜄ(), sprᢀ);
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x000CEF6C File Offset: 0x000CDF6C
		private void ᜄ()
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
			this.ᜅ();
			this.ᜎ = (sprḠ)spr\u175E.ᜀ(TBIFFRecord.ChartSeries);
			XlsChartSerieDataFormat xlsChartSerieDataFormat = new ChartSerieDataFormat((spr\u2158)base.ReservedHandle, this);
			xlsChartSerieDataFormat.DataFormat.ᜂ(ushort.MaxValue);
			this.ᜀ(xlsChartSerieDataFormat);
			this.\u171E = new spr\u2457((spr\u2158)base.ReservedHandle, this);
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x000CF004 File Offset: 0x000CE004
		private void ᜀ(XlsChartSerieDataFormat A_0)
		{
			int a_ = 14;
			if (A_0 == null)
			{
				for (;;)
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
						goto IL_36;
					}
				}
				IL_36:
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("⁃❅㱇⭉ੋ⅍≏㽑㕓≕", a_));
			}
			int index = (int)A_0.DataFormat.ᜅ();
			XlsChartDataPoint xlsChartDataPoint = (XlsChartDataPoint)this.DataPoints[index];
			xlsChartDataPoint.InnerDataFormat = A_0;
			A_0.SetParent(xlsChartDataPoint);
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x000CF090 File Offset: 0x000CE090
		internal spr\u25C6 \u170D()
		{
			spr\u25C6 spr_u25C;
			for (;;)
			{
				spr_u25C = (spr\u25C6)spr\u175E.ᜀ(TBIFFRecord.Chart3DDataFormat);
				ExcelChartType chartType = this.ᜏ.ChartType;
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return spr_u25C;
					case 1:
						return spr_u25C;
					case 2:
						return spr_u25C;
					case 3:
						return spr_u25C;
					case 4:
						num = 1;
						continue;
					case 5:
						switch (chartType)
						{
						case ExcelChartType.CylinderClustered:
						case ExcelChartType.CylinderStacked:
						case ExcelChartType.Cylinder100PercentStacked:
						case ExcelChartType.CylinderBarClustered:
						case ExcelChartType.CylinderBarStacked:
						case ExcelChartType.CylinderBar100PercentStacked:
						case ExcelChartType.Cylinder3DClustered:
							spr_u25C.ᜀ(BaseFormatType.Circle);
							spr_u25C.ᜀ(TopFormatType.Straight);
							num = 7;
							continue;
						case ExcelChartType.ConeClustered:
						case ExcelChartType.ConeStacked:
						case ExcelChartType.ConeBarClustered:
						case ExcelChartType.ConeBarStacked:
						case ExcelChartType.Cone3DClustered:
							spr_u25C.ᜀ(BaseFormatType.Circle);
							spr_u25C.ᜀ(TopFormatType.Sharp);
							num = 2;
							continue;
						case ExcelChartType.Cone100PercentStacked:
						case ExcelChartType.ConeBar100PercentStacked:
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
								spr_u25C.ᜀ(BaseFormatType.Circle);
								spr_u25C.ᜀ(TopFormatType.Trunc);
								num = 3;
								continue;
							}
							break;
						case ExcelChartType.PyramidClustered:
						case ExcelChartType.PyramidStacked:
						case ExcelChartType.PyramidBarClustered:
						case ExcelChartType.PyramidBarStacked:
						case ExcelChartType.Pyramid3DClustered:
							break;
						case ExcelChartType.Pyramid100PercentStacked:
						case ExcelChartType.PyramidBar100PercentStacked:
							spr_u25C.ᜀ(BaseFormatType.Rectangle);
							spr_u25C.ᜀ(TopFormatType.Trunc);
							num = 0;
							continue;
						default:
							num = 4;
							continue;
						}
						spr_u25C.ᜀ(BaseFormatType.Rectangle);
						spr_u25C.ᜀ(TopFormatType.Sharp);
						num = 6;
						continue;
					case 6:
						return spr_u25C;
					case 7:
						return spr_u25C;
					}
					break;
				}
			}
			return spr_u25C;
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x000CF21C File Offset: 0x000CE21C
		private void ᜁ(string A_0)
		{
			int a_ = 0;
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_148;
				case 1:
					num = 5;
					continue;
				case 2:
					if (A_0.Length > 0)
					{
						num = 8;
						continue;
					}
					goto IL_1A8;
				case 3:
					if (this.\u171A.Row != this.\u171A.LastRow)
					{
						num = 1;
						continue;
					}
					goto IL_1A8;
				case 4:
					num = 2;
					continue;
				case 5:
					if (this.\u171A.Column != this.\u171A.LastColumn)
					{
						if (true)
						{
						}
						num = 0;
						continue;
					}
					goto IL_1A8;
				case 6:
					try
					{
						Ptg[] array = this.\u170D.FormulaUtil.ᜃ(A_0);
						sprỜ sprỜ = (sprỜ)array[0];
						this.\u171A = sprỜ.ᜀ(this.\u170D, null);
						goto IL_58;
					}
					catch
					{
						throw new ArgumentException(RecordTableEnumerator.b("缵嘷䰹崻刽⤿♁摃⁅❇㡉⅋㭍㱏㍑瑓╕ⱗ⡙㕛そݟ", a_));
					}
					goto IL_192;
					IL_58:
					num = 3;
					continue;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C7;
					default:
						if (false)
						{
						}
						num = 10;
						continue;
					}
					break;
				case 9:
					goto IL_192;
				case 10:
					if (A_0[0] == '=')
					{
						goto IL_C7;
					}
					goto IL_1A8;
				}
				if (A_0 != null)
				{
					num = 4;
					continue;
				}
				goto IL_1A8;
				IL_C7:
				num = 9;
				continue;
				IL_192:
				A_0 = A_0.Substring(1);
				num = 6;
			}
			IL_148:
			throw new NotSupportedException(RecordTableEnumerator.b("搵崷尹夻䰽ℿⱁ❃⍅桇❉㥋㵍⑏牑㙓㍕硗㭙籛ⵝय़ౡͣ੥൧䩩ཫ୭ᱯṱ塳噵੷ᕹ୻剽ꁿꚅﮍﶏﲑ몓", a_));
			IL_1A8:
			this.\u171A = null;
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x000CF3E8 File Offset: 0x000CE3E8
		private void ᜀ(XlsEventArgs A_0)
		{
			for (;;)
			{
				IL_14:
				this.ᜀ(this.ᜋ[sprᢀ.LinkIndex.LinkToValues], this.ᜆ, sprᢀ.ReferenceType.EnteredDirectly);
				for (;;)
				{
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (this.ᜅ != null)
							{
								num = 2;
								continue;
							}
							return;
						case 1:
							goto IL_5C;
						case 2:
							this.ᜅ(this, A_0);
							num = 1;
							continue;
						}
						goto IL_14;
					}
					IL_5C:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_72;
					}
				}
			}
			IL_72:
			if (false)
			{
			}
			if (true)
			{
			}
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x000CF484 File Offset: 0x000CE484
		private void ᜃ()
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
			this.ᜀ(this.ᜋ[sprᢀ.LinkIndex.LinkToCategories], this.ᜇ, sprᢀ.ReferenceType.DefaultCategories);
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x000CF4D8 File Offset: 0x000CE4D8
		private void ᜂ()
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
			this.ᜀ(this.ᜋ[sprᢀ.LinkIndex.LinkToBubbles], this.ᜈ, sprᢀ.ReferenceType.DefaultCategories);
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x000CF52C File Offset: 0x000CE52C
		public XlsChartSerie Clone(object parent, Dictionary<string, string> newNames, Dictionary<int, int> fontIndexes)
		{
			XlsChartSerie xlsChartSerie;
			for (;;)
			{
				xlsChartSerie = new ChartSerie((spr\u2158)base.ReservedHandle, parent);
				xlsChartSerie.\u1712 = this.\u1712;
				xlsChartSerie.m_bIsDisposed = this.m_bIsDisposed;
				xlsChartSerie.ᜋ = new Dictionary<sprᢀ.LinkIndex, sprᢀ>();
				xlsChartSerie.ᜅ();
				IXLSRange ixlsrange = this.GetSerieNameRange();
				int num = 15;
				for (;;)
				{
					switch (num)
					{
					case 0:
						ixlsrange = ((ICombinedRange)this.ᜆ).Clone(xlsChartSerie, newNames, xlsChartSerie.\u170D);
						xlsChartSerie.Values = ixlsrange;
						num = 16;
						continue;
					case 1:
						xlsChartSerie.\u171C = this.\u171C.ᜀ(xlsChartSerie, newNames);
						num = 21;
						continue;
					case 2:
						xlsChartSerie.\u171A = ((ICombinedRange)this.\u171A).Clone(xlsChartSerie, newNames, xlsChartSerie.\u170D);
						num = 5;
						continue;
					case 3:
						if (this.m_dataPoints != null)
						{
							num = 12;
							continue;
						}
						goto IL_26B;
					case 4:
						if (this.ᜆ != null)
						{
							num = 0;
							continue;
						}
						goto IL_21F;
					case 5:
						goto IL_1A8;
					case 6:
						if (this.\u171C != null)
						{
							num = 1;
							continue;
						}
						goto IL_39D;
					case 7:
						goto IL_26B;
					case 8:
						xlsChartSerie.CategoryLabels = ((ICombinedRange)this.ᜇ).Clone(xlsChartSerie, newNames, xlsChartSerie.\u170D);
						num = 18;
						continue;
					case 9:
						goto IL_185;
					case 10:
						if (this.\u171A != null)
						{
							num = 2;
							continue;
						}
						goto IL_1A8;
					case 11:
						xlsChartSerie.\u171D = this.\u171D.ᜀ(xlsChartSerie, newNames);
						num = 19;
						continue;
					case 12:
						xlsChartSerie.m_dataPoints = (XlsChartDataPointsCollection)this.m_dataPoints.Clone(xlsChartSerie, xlsChartSerie.\u170D, fontIndexes, newNames);
						num = 7;
						continue;
					case 13:
						xlsChartSerie.\u171A = ((ICombinedRange)this.\u171A).Clone(xlsChartSerie, newNames, xlsChartSerie.\u170D);
						num = 9;
						continue;
					case 14:
						if (this.\u171D != null)
						{
							num = 11;
							continue;
						}
						goto IL_245;
					case 15:
						if (ixlsrange != null)
						{
							num = 13;
							continue;
						}
						goto IL_185;
					case 16:
						goto IL_21F;
					case 17:
						goto IL_338;
					case 18:
						goto IL_377;
					case 19:
						goto IL_245;
					case 20:
						xlsChartSerie.Bubbles = ((ICombinedRange)this.ᜈ).Clone(xlsChartSerie, newNames, xlsChartSerie.\u170D);
						num = 17;
						continue;
					case 21:
						goto IL_1EC;
					case 22:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_377;
						default:
							if (false)
							{
							}
							if (this.ᜇ != null)
							{
								num = 8;
								continue;
							}
							goto IL_377;
						}
						break;
					case 23:
						if (this.ᜈ != null)
						{
							num = 20;
							continue;
						}
						goto IL_338;
					}
					break;
					IL_185:
					num = 4;
					continue;
					IL_1A8:
					num = 3;
					continue;
					IL_21F:
					num = 23;
					continue;
					IL_245:
					num = 6;
					continue;
					IL_26B:
					xlsChartSerie.ᜌ = this.ᜌ;
					xlsChartSerie.ᜑ = this.ᜑ;
					xlsChartSerie.ᜎ = (sprḠ)this.ᜎ.Clone();
					xlsChartSerie.ᜉ = this.ᜉ;
					xlsChartSerie.\u171E = this.\u171E.ᜀ(xlsChartSerie, fontIndexes, newNames);
					num = 14;
					continue;
					IL_338:
					num = 22;
					continue;
					IL_377:
					num = 10;
				}
			}
			IL_1EC:
			IL_39D:
			if (true)
			{
			}
			return xlsChartSerie;
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x000CF8E0 File Offset: 0x000CE8E0
		private string ᜀ(string A_0)
		{
			int a_ = 19;
			if (A_0 == null)
			{
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_2E;
					}
				}
				IL_2E:
				if (true)
				{
				}
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("⡈⽊⥌㵎㑐⁒♔", a_));
			}
			int num = A_0.IndexOf(RecordTableEnumerator.b("湈橊", a_));
			return A_0.Substring(1, num - 1);
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x000CF95C File Offset: 0x000CE95C
		private void ᜀ(bool A_0)
		{
			int a_ = 17;
			switch (0)
			{
			default:
			{
				int num = 25;
				for (;;)
				{
					int num2;
					int num3;
					int num4;
					int chartGroup;
					int num5;
					XlsChartFormat commonSerieFormat;
					bool flag;
					switch (num)
					{
					case 0:
						this.ChartGroup = num2;
						num = 21;
						continue;
					case 1:
						if (num3 != num4)
						{
							num = 26;
							continue;
						}
						num = 9;
						continue;
					case 2:
						num = 22;
						continue;
					case 3:
						goto IL_213;
					case 4:
						num = 6;
						continue;
					case 5:
						if (Array.IndexOf<ExcelChartType>(XlsChart.ᜭ, this.SerieType) != -1)
						{
							num = 3;
							continue;
						}
						goto IL_153;
					case 6:
						if (this.ᜐ.Count == 1)
						{
							num = 19;
							continue;
						}
						goto IL_28E;
					case 7:
						goto IL_348;
					case 8:
						num = 5;
						continue;
					case 9:
						if (!A_0)
						{
							num = 2;
							continue;
						}
						goto IL_348;
					case 10:
						num5 = chartGroup + 1;
						goto IL_2B8;
					case 11:
						if (A_0)
						{
							num = 8;
							continue;
						}
						goto IL_153;
					case 12:
						if (num3 == 1)
						{
							num = 17;
							continue;
						}
						goto IL_19A;
					case 13:
						if (!A_0)
						{
							num = 15;
							continue;
						}
						num = 24;
						continue;
					case 14:
					{
						ExcelChartType serieType;
						this.SerieType = serieType;
						num = 16;
						continue;
					}
					case 15:
						num = 10;
						continue;
					case 16:
						goto IL_EF;
					case 17:
						this.ᜏ.PrimaryParentAxis.ᜆ().Remove(commonSerieFormat);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_295;
						default:
							if (false)
							{
							}
							num = 18;
							continue;
						}
						break;
					case 18:
						goto IL_19A;
					case 19:
						goto IL_289;
					case 20:
					{
						ExcelChartType serieType;
						if (Array.IndexOf<ExcelChartType>(XlsChart.ᜰ, serieType) != -1)
						{
							num = 14;
							continue;
						}
						return;
					}
					case 21:
						goto IL_22B;
					case 22:
						if (this.ᜏ.SecondaryFormats.Count == 0)
						{
							num = 7;
							continue;
						}
						goto IL_108;
					case 23:
						if (flag)
						{
							num = 0;
							continue;
						}
						return;
					case 24:
						if (true)
						{
						}
						num5 = chartGroup - 1;
						goto IL_2B8;
					case 26:
					{
						ExcelChartType serieType = this.SerieType;
						this.ChartGroup = num2;
						num = 12;
						continue;
					}
					}
					if (!A_0)
					{
						num = 4;
						continue;
					}
					goto IL_28E;
					IL_153:
					flag = (num3 != 1);
					this.ᜏ.PrimaryParentAxis.ᜆ().ChangeShallowAxis(A_0, chartGroup, flag, num2);
					num = 23;
					continue;
					IL_19A:
					num = 20;
					continue;
					IL_295:
					num = 13;
					continue;
					IL_28E:
					chartGroup = this.ChartGroup;
					goto IL_295;
					IL_2B8:
					num2 = num5;
					num3 = this.ᜐ.ᜅ(chartGroup);
					num4 = this.ᜐ.ᜀ(this.SerieType);
					commonSerieFormat = this.GetCommonSerieFormat();
					num = 1;
					continue;
					IL_348:
					num = 11;
				}
				IL_EF:
				return;
				IL_108:
				throw new ApplicationException(RecordTableEnumerator.b("ц⡈╊橌㭎煐⁒ご⍖祘㡚⡜ⵞ፠٢୤፦䥨ᡪ࡬ᵮᡰᙲ啴Ͷᙸ孺๼᩾力꾎ﲔ", a_));
				IL_213:
				goto IL_108;
				IL_22B:
				return;
				IL_289:
				throw new ArgumentException(RecordTableEnumerator.b("цⅈ⩊⍌⡎㑐獒㑔⽖じ⡚絜㥞`੢।ɦ൨䕪", a_));
			}
			}
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x000CFCE0 File Offset: 0x000CECE0
		internal void ᜀ(int A_0, spr\u23A5 A_1)
		{
			int a_ = 10;
			int num = 7;
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
					num = 5;
					continue;
				case 1:
					goto IL_D0;
				case 2:
					if (this.ᜈ == null)
					{
						num = 13;
						continue;
					}
					return;
				case 3:
					num = 9;
					continue;
				case 4:
					if (this.ᜆ == null)
					{
						num = 6;
						continue;
					}
					return;
				case 5:
					switch (A_0)
					{
					case 1:
						if (true)
						{
						}
						num = 4;
						continue;
					case 2:
						num = 14;
						continue;
					case 3:
						num = 2;
						continue;
					default:
						num = 11;
						continue;
					}
					break;
				case 6:
					goto IL_B2;
				case 8:
					goto IL_84;
				case 9:
					if (A_0 < 1)
					{
						num = 10;
						continue;
					}
					goto IL_B4;
				case 10:
					goto IL_1AD;
				case 11:
					return;
				case 12:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B4;
					default:
						goto IL_15A;
					}
					break;
				case 13:
					this.\u1716.Add(A_1 as BiffRecordRaw);
					num = 12;
					continue;
				case 14:
					if (this.ᜇ == null)
					{
						num = 8;
						continue;
					}
					return;
				}
				if (A_0 <= 3)
				{
					num = 3;
					continue;
				}
				goto IL_102;
				IL_B4:
				num = 0;
			}
			IL_84:
			this.\u1715.Add(A_1 as BiffRecordRaw);
			return;
			IL_B2:
			this.\u1714.Add(A_1 as BiffRecordRaw);
			return;
			IL_D0:
			throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁❃⥅㩇⹉", a_));
			IL_102:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㌿⭁ൃ⡅ⱇ⽉㑋", a_));
			IL_15A:
			if (false)
			{
			}
			return;
			IL_1AD:
			goto IL_102;
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x000CFEC8 File Offset: 0x000CEEC8
		internal List<BiffRecordRaw> ᜀ(int A_0)
		{
			int a_ = 17;
			int num = 13;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_74;
				case 1:
				{
					List<BiffRecordRaw> list;
					if (list != null)
					{
						num = 11;
						continue;
					}
					goto IL_127;
				}
				case 2:
					goto IL_74;
				case 3:
					goto IL_10E;
				case 4:
				{
					List<BiffRecordRaw> list;
					if (list.Count <= 0)
					{
						num = 9;
						continue;
					}
					return list;
				}
				case 5:
					goto IL_74;
				case 6:
				{
					if (A_0 < 1)
					{
						num = 3;
						continue;
					}
					List<BiffRecordRaw> list = null;
					num = 10;
					continue;
				}
				case 7:
					num = 8;
					continue;
				case 8:
					goto IL_74;
				case 9:
					goto IL_F2;
				case 10:
					switch (A_0)
					{
					case 1:
					{
						List<BiffRecordRaw> list = this.\u1714;
						num = 2;
						continue;
					}
					case 2:
					{
						List<BiffRecordRaw> list = this.\u1715;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num = 5;
							continue;
						}
						break;
					}
					case 3:
					{
						List<BiffRecordRaw> list = this.\u1716;
						break;
					}
					default:
						num = 7;
						continue;
					}
					num = 0;
					continue;
				case 11:
					num = 4;
					continue;
				case 12:
					num = 6;
					continue;
				case 13:
					if (true)
					{
					}
					break;
				}
				if (A_0 <= 3)
				{
					num = 12;
					continue;
				}
				break;
				IL_74:
				num = 1;
			}
			IL_BC:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㑆⁈Ɋ⍌⭎㑐⭒", a_));
			IL_F2:
			goto IL_127;
			IL_10E:
			goto IL_BC;
			IL_127:
			return null;
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x000D0058 File Offset: 0x000CF058
		internal object[] ᜁ(List<BiffRecordRaw> A_0)
		{
			int a_ = 12;
			switch (0)
			{
			default:
			{
				int num = 6;
				List<object> list;
				for (;;)
				{
					int num2;
					int count;
					object item;
					TBIFFRecord typeCode;
					switch (num)
					{
					case 0:
						goto IL_B5;
					case 1:
						num = 4;
						continue;
					case 2:
						goto IL_69;
					case 3:
						goto IL_103;
					case 4:
						goto IL_B5;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_187;
						default:
						{
							if (false)
							{
							}
							if (num2 >= count)
							{
								num = 11;
								continue;
							}
							if (true)
							{
							}
							BiffRecordRaw biffRecordRaw = A_0[num2];
							item = null;
							typeCode = biffRecordRaw.TypeCode;
							num = 10;
							continue;
						}
						}
						break;
					case 7:
						goto IL_103;
					case 8:
						goto IL_B0;
					case 9:
						goto IL_B5;
					case 10:
						goto IL_187;
					case 11:
						goto IL_13B;
					case 12:
						if (count == 0)
						{
							num = 8;
							continue;
						}
						list = new List<object>(count);
						num2 = 0;
						num = 7;
						continue;
					}
					if (A_0 == null)
					{
						num = 2;
						continue;
					}
					count = A_0.Count;
					num = 12;
					continue;
					IL_187:
					switch (typeCode)
					{
					case TBIFFRecord.Number:
					{
						BiffRecordRaw biffRecordRaw;
						item = ((spr\u19FF)biffRecordRaw).ᜅ();
						num = 0;
						continue;
					}
					case TBIFFRecord.Label:
					{
						BiffRecordRaw biffRecordRaw;
						item = ((spr\u2170)biffRecordRaw).ᜁ();
						num = 9;
						continue;
					}
					default:
						num = 1;
						continue;
					}
					IL_B5:
					list.Add(item);
					num2++;
					num = 3;
					continue;
					IL_103:
					num = 5;
				}
				IL_69:
				throw new ArgumentNullException(RecordTableEnumerator.b("⍁㙃㑅⥇㍉", a_));
				IL_B0:
				return null;
				IL_13B:
				return list.ToArray();
			}
			}
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x000D0218 File Offset: 0x000CF218
		private bool ᜀ(object[] A_0)
		{
			int a_ = 0;
			int num = 4;
			for (;;)
			{
				int num2;
				int num3;
				switch (num)
				{
				case 0:
					if (num2 >= num3)
					{
						goto IL_90;
					}
					num = 5;
					continue;
				case 1:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_90;
					default:
						goto IL_CE;
					}
					break;
				case 2:
					goto IL_44;
				case 3:
					goto IL_84;
				case 5:
					if (A_0[num2] is string)
					{
						num = 1;
						continue;
					}
					num2++;
					num = 3;
					continue;
				case 6:
					return true;
				case 7:
					goto IL_84;
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				num2 = 0;
				num3 = A_0.Length;
				num = 7;
				continue;
				IL_84:
				num = 0;
				continue;
				IL_90:
				num = 6;
			}
			IL_44:
			throw new ArgumentNullException(RecordTableEnumerator.b("圵䨷䠹崻䜽", a_));
			IL_CE:
			if (false)
			{
			}
			return false;
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x000D0308 File Offset: 0x000CF308
		private List<BiffRecordRaw> ᜀ(bool A_0, object[] A_1)
		{
			int a_ = 7;
			int num2;
			for (;;)
			{
				IL_09:
				switch (0)
				{
				default:
				{
					int num = 8;
					for (;;)
					{
						object obj;
						spr\u23A5 spr_u23A;
						object obj2;
						int num3;
						List<BiffRecordRaw> list;
						switch (num)
						{
						case 0:
						{
							if (!(obj is IConvertible))
							{
								num = 13;
								continue;
							}
							spr\u2170 spr_u;
							spr_u.ᜀ(Convert.ToString(obj));
							spr_u23A = spr_u;
							num = 4;
							continue;
						}
						case 1:
							num = 7;
							continue;
						case 2:
							if (!(bool)obj)
							{
								num = 1;
								continue;
							}
							num = 5;
							continue;
						case 3:
							goto IL_2D1;
						case 4:
							goto IL_278;
						case 5:
							obj2 = RecordTableEnumerator.b("椼派ᑀق", a_);
							goto IL_269;
						case 6:
							num = 15;
							continue;
						case 7:
							obj2 = RecordTableEnumerator.b("笼績ീ၂D", a_);
							goto IL_269;
						case 8:
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
								break;
							}
							break;
						case 9:
							goto IL_CA;
						case 10:
							goto IL_231;
						case 11:
							if (obj is bool)
							{
								num = 6;
								continue;
							}
							goto IL_2AE;
						case 12:
							if (num2 >= num3)
							{
								num = 21;
								continue;
							}
							obj = A_1[num2];
							num = 14;
							continue;
						case 13:
							goto IL_393;
						case 14:
							if (obj == null)
							{
								num = 17;
								continue;
							}
							num = 11;
							continue;
						case 15:
							if (!A_0)
							{
								num = 19;
								continue;
							}
							goto IL_2AE;
						case 16:
							goto IL_2D1;
						case 17:
							goto IL_1FB;
						case 18:
						{
							spr\u19FF spr_u19FF = (spr\u19FF)spr\u175E.ᜀ(TBIFFRecord.Number);
							num = 24;
							continue;
						}
						case 19:
							num = 2;
							continue;
						case 20:
							goto IL_2AE;
						case 21:
							return list;
						case 22:
							goto IL_278;
						case 23:
						{
							if (A_0)
							{
								num = 18;
								continue;
							}
							spr\u2170 spr_u = (spr\u2170)spr\u175E.ᜀ(TBIFFRecord.Label);
							num = 0;
							continue;
						}
						case 24:
						{
							if (!(obj is IConvertible))
							{
								num = 10;
								continue;
							}
							spr\u19FF spr_u19FF;
							spr_u19FF.ᜀ(Convert.ToDouble(obj));
							spr_u23A = spr_u19FF;
							num = 22;
							continue;
						}
						}
						if (A_1 == null)
						{
							num = 9;
							continue;
						}
						num3 = A_1.Length;
						list = new List<BiffRecordRaw>();
						num2 = 0;
						num = 16;
						continue;
						IL_269:
						obj = obj2;
						num = 20;
						continue;
						IL_278:
						spr_u23A.ᜄ((int)((ushort)this.Index));
						spr_u23A.ᜃ((int)((ushort)num2));
						list.Add(spr_u23A as BiffRecordRaw);
						num2++;
						num = 3;
						continue;
						IL_2AE:
						num = 23;
						continue;
						IL_2D1:
						num = 12;
					}
					break;
				}
				}
			}
			IL_CA:
			throw new ArgumentNullException(RecordTableEnumerator.b("䬼帾ⵀ㙂⁄㑆", a_));
			IL_1FB:
			throw new ApplicationException(RecordTableEnumerator.b("猼䨾ⵀ⽂敄㕆ⱈⵊ⡌㵎ぐ㵒㙔㉖祘ⵚ㱜㍞ᑠ٢䕤๦ݨ䭪᭬๮ᵰٲၴѶ奸ོ᩺ൾ廒ꖄﶈꮊ", a_) + num2.ToString() + RecordTableEnumerator.b("ᴼ伾⹀あⱄ㍆⁈⑊⍌", a_));
			IL_231:
			throw new ApplicationException(RecordTableEnumerator.b("缼帾╀捂㍄♆╈㹊⡌潎㡐㵒畔⅖㡘㝚⡜㩞በ䍢Ѥᕦ᭨੪ᑬ佮ၰݲ啴", a_) + num2.ToString() + RecordTableEnumerator.b("ᴼ伾⹀あⱄ㍆⁈⑊⍌", a_));
			IL_393:
			throw new ApplicationException(RecordTableEnumerator.b("缼帾╀捂㍄♆╈㹊⡌潎㡐㵒畔⅖㡘㝚⡜㩞በ䍢Ѥᕦ᭨੪ᑬ佮ၰݲ啴", a_) + num2.ToString() + RecordTableEnumerator.b("ᴼ伾⹀あⱄ㍆⁈⑊⍌", a_));
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x000D06AC File Offset: 0x000CF6AC
		private void ᜀ(List<BiffRecordRaw> A_0)
		{
			int a_ = 12;
			switch (0)
			{
			default:
			{
				int num = 3;
				for (;;)
				{
					int num2;
					int count;
					int index;
					switch (num)
					{
					case 0:
						return;
					case 1:
						goto IL_C9;
					case 2:
					{
						if (num2 >= count)
						{
							num = 0;
							continue;
						}
						spr\u23A5 spr_u23A = (spr\u23A5)A_0[num2];
						spr_u23A.ᜄ((int)((ushort)index));
						num2++;
						num = 4;
						continue;
					}
					case 4:
						goto IL_C9;
					case 5:
						goto IL_4D;
					}
					if (A_0 == null)
					{
						num = 5;
						continue;
					}
					index = this.Index;
					num2 = 0;
					count = A_0.Count;
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
						num = 1;
						continue;
					}
					IL_C9:
					num = 2;
				}
				IL_4D:
				throw new ArgumentNullException(RecordTableEnumerator.b("⍁㙃㑅⥇㍉ᡋ⅍Տ≑こ㝕ⱗ㽙", a_));
			}
			}
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x000D07B0 File Offset: 0x000CF7B0
		private string ᜁ()
		{
			int a_ = 8;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_71:
				if (this.ᜉ == null)
				{
					goto IL_147;
				}
				num = 10;
				break;
			default:
				if (false)
				{
				}
				switch (0)
				{
				default:
					num = 4;
					break;
				}
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_CA;
				case 1:
					num = 9;
					continue;
				case 2:
					try
					{
						sprỜ sprỜ = (sprỜ)this.ᜊ[0];
						this.\u171A = sprỜ.ᜀ(this.\u170D, null);
						goto IL_10B;
					}
					catch
					{
						return this.ᜉ = RecordTableEnumerator.b("ᴽሿ݁Ƀ杅", a_);
					}
					goto IL_194;
					IL_10B:
					num = 6;
					continue;
				case 3:
					if (this.ᜉ.Length != 0)
					{
						num = 1;
						continue;
					}
					goto IL_147;
				case 5:
					goto IL_1B7;
				case 6:
					if (this.\u171A == null)
					{
						num = 7;
						continue;
					}
					goto IL_12D;
				case 7:
					goto IL_12B;
				case 8:
					if (this.\u171A != null)
					{
						num = 5;
						continue;
					}
					this.ᜉ.Substring(1);
					num = 2;
					continue;
				case 9:
					if (this.ᜉ[0] != '=')
					{
						num = 0;
						continue;
					}
					goto IL_194;
				case 10:
					num = 3;
					continue;
				}
				break;
				IL_194:
				num = 8;
			}
			goto IL_71;
			IL_CA:
			goto IL_147;
			IL_12B:
			return this.ᜉ = RecordTableEnumerator.b("ᴽሿ݁Ƀ杅", a_);
			IL_12D:
			return this.ᜀ(this.\u171A);
			IL_1B7:
			return this.ᜀ(this.\u171A);
			IL_147:
			if (true)
			{
			}
			return this.ᜉ;
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x000D0994 File Offset: 0x000CF994
		private string ᜀ(IXLSRange A_0)
		{
			int a_ = 3;
			switch (0)
			{
			default:
			{
				string text;
				for (;;)
				{
					IL_17:
					int num = 12;
					for (;;)
					{
						int num2;
						int lastColumn;
						int num3;
						switch (num)
						{
						case 0:
							goto IL_F9;
						case 1:
							goto IL_11D;
						case 2:
						{
							string value;
							if (value.Length > 0)
							{
								num = 13;
								continue;
							}
							goto IL_A2;
						}
						case 3:
							num2++;
							num = 7;
							continue;
						case 4:
							goto IL_8B;
						case 5:
						{
							string value;
							if (value != null)
							{
								num = 10;
								continue;
							}
							goto IL_A2;
						}
						case 6:
							num = 14;
							continue;
						case 7:
							goto IL_18D;
						case 8:
						{
							if (num2 > lastColumn)
							{
								num = 6;
								continue;
							}
							num3 = A_0.Row;
							int lastRow = A_0.LastRow;
							num = 1;
							continue;
						}
						case 9:
							goto IL_11D;
						case 10:
							num = 2;
							continue;
						case 11:
						{
							int lastRow;
							if (num3 > lastRow)
							{
								num = 3;
								continue;
							}
							string value = A_0[num3, num2].Value;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_17;
							default:
								if (false)
								{
								}
								num = 5;
								continue;
							}
							break;
						}
						case 13:
						{
							string value;
							text = text + value + RecordTableEnumerator.b("ᤸ", a_);
							num = 15;
							continue;
						}
						case 14:
							if (!(text == ""))
							{
								num = 0;
								continue;
							}
							goto IL_228;
						case 15:
							goto IL_A2;
						case 16:
							goto IL_18D;
						}
						if (true)
						{
						}
						if (A_0 == null)
						{
							num = 4;
							continue;
						}
						text = "";
						num2 = A_0.Column;
						lastColumn = A_0.LastColumn;
						num = 16;
						continue;
						IL_A2:
						num3++;
						num = 9;
						continue;
						IL_11D:
						num = 11;
						continue;
						IL_18D:
						num = 8;
					}
				}
				IL_8B:
				throw new ArgumentNullException(RecordTableEnumerator.b("䬸娺匼堾⑀", a_));
				IL_F9:
				return text.Substring(0, text.Length - 1);
				IL_228:
				return "";
			}
			}
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x000D0BD0 File Offset: 0x000CFBD0
		public void SetDefaultName(string defaultName)
		{
			int a_ = 12;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					if (true)
					{
					}
					if (defaultName.Length == 0)
					{
						num = 2;
						continue;
					}
					goto IL_92;
				case 2:
					goto IL_40;
				case 3:
					IL_34:
					num = 1;
					continue;
				}
				if (defaultName != null)
				{
					num = 3;
					continue;
				}
				IL_40:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_34;
				default:
					goto IL_56;
				}
			}
			IL_56:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅁぃ㑅ه⭉⅋⭍", a_));
			IL_92:
			this.ᜉ = defaultName;
			this.\u1712 = true;
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x000D0C80 File Offset: 0x000CFC80
		public IXLSRange GetSerieNameRange()
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
			string name = this.Name;
			return this.\u171A;
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x000D0CC8 File Offset: 0x000CFCC8
		internal ExcelChartType ᜋ()
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
			string value = this.ᜎ();
			return (ExcelChartType)Enum.Parse(typeof(ExcelChartType), value, true);
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x000D0D20 File Offset: 0x000CFD20
		internal string ᜇ()
		{
			int a_ = 4;
			XlsChartFormat commonSerieFormat;
			for (;;)
			{
				XlsChartFormatCollection primaryFormats = this.ᜏ.PrimaryFormats;
				int num = 14;
				for (;;)
				{
					TBIFFRecord tbiffrecord;
					switch (num)
					{
					case 0:
						num = 20;
						continue;
					case 1:
						num = 17;
						continue;
					case 2:
						goto IL_210;
					case 3:
						switch (tbiffrecord)
						{
						case TBIFFRecord.ChartRadar:
						case TBIFFRecord.ChartRadarArea:
							goto IL_178;
						case TBIFFRecord.ChartSurface:
							goto IL_1A6;
						default:
							num = 10;
							continue;
						}
						break;
					case 4:
						if (commonSerieFormat.DataFormatOrNull != null)
						{
							num = 1;
							continue;
						}
						goto IL_155;
					case 5:
						goto IL_173;
					case 6:
						goto IL_279;
					case 7:
						num = 13;
						continue;
					case 8:
						if (commonSerieFormat.DoughnutHoleSize == 0)
						{
							if (true)
							{
							}
							num = 6;
							continue;
						}
						goto IL_187;
					case 9:
						goto IL_155;
					case 10:
						num = 12;
						continue;
					case 11:
						goto IL_1A1;
					case 12:
						if (tbiffrecord != TBIFFRecord.ChartBoppop)
						{
							num = 18;
							continue;
						}
						goto IL_1B5;
					case 13:
						if (!commonSerieFormat.DataFormatOrNull.Is3DBubbles)
						{
							num = 9;
							continue;
						}
						goto IL_FD;
					case 14:
						if (this.ᜏ.Loading)
						{
							num = 0;
							continue;
						}
						goto IL_10C;
					case 15:
						if (!commonSerieFormat.IsBubbles)
						{
							num = 5;
							continue;
						}
						goto IL_FD;
					case 16:
						num = 3;
						continue;
					case 17:
						if (commonSerieFormat.DataFormatOrNull.SerieFormatOrNull != null)
						{
							num = 7;
							continue;
						}
						goto IL_155;
					case 18:
						num = 11;
						continue;
					case 19:
						switch (tbiffrecord)
						{
						case TBIFFRecord.ChartBar:
							goto IL_248;
						case TBIFFRecord.ChartLine:
							goto IL_1C4;
						case TBIFFRecord.ChartPie:
							num = 8;
							continue;
						case TBIFFRecord.ChartArea:
							goto IL_C8;
						case TBIFFRecord.ChartScatter:
							num = 4;
							continue;
						default:
							num = 16;
							continue;
						}
						break;
					case 20:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_C8;
						default:
							if (false)
							{
							}
							if (primaryFormats.Count == 0)
							{
								num = 2;
								continue;
							}
							goto IL_10C;
						}
						break;
					}
					break;
					IL_10C:
					commonSerieFormat = this.GetCommonSerieFormat();
					tbiffrecord = commonSerieFormat.FormatRecordType;
					num = 19;
					continue;
					IL_155:
					num = 15;
				}
			}
			IL_C8:
			return RecordTableEnumerator.b("笹主嬽ℿ", a_);
			IL_FD:
			return RecordTableEnumerator.b("砹䤻尽∿⹁⅃", a_);
			IL_173:
			return RecordTableEnumerator.b("椹弻弽㐿㙁⅃㑅", a_);
			IL_178:
			return RecordTableEnumerator.b("根崻娽ℿぁ", a_);
			IL_187:
			return RecordTableEnumerator.b("縹医䬽✿⩁⩃㍅㱇", a_);
			IL_1A1:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("礹崻倽朿㙁摃≅ⵇ㹉⥋ⵍ⑏牑❓㍕⩗㍙㥛繝ᑟ᭡ᑣͥ䙧", a_));
			IL_1A6:
			return RecordTableEnumerator.b("椹䤻䰽☿⍁❃⍅", a_);
			IL_1B5:
			return RecordTableEnumerator.b("樹唻嬽", a_);
			IL_1C4:
			return RecordTableEnumerator.b("瘹唻倽┿", a_);
			IL_210:
			return RecordTableEnumerator.b("礹医刽㔿⽁⩃", a_);
			IL_248:
			return this.ᜇ(commonSerieFormat);
			IL_279:
			return RecordTableEnumerator.b("樹唻嬽", a_);
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x000D1038 File Offset: 0x000D0038
		internal string ᜎ()
		{
			int a_ = 5;
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
				XlsChartFormat commonSerieFormat;
				for (;;)
				{
					XlsChartFormatCollection primaryFormats = this.ᜏ.PrimaryFormats;
					int num = 1;
					for (;;)
					{
						TBIFFRecord tbiffrecord;
						switch (num)
						{
						case 0:
							switch (tbiffrecord)
							{
							case TBIFFRecord.ChartBar:
								goto IL_115;
							case TBIFFRecord.ChartLine:
								goto IL_1BD;
							case TBIFFRecord.ChartPie:
								goto IL_99;
							case TBIFFRecord.ChartArea:
								goto IL_FB;
							case TBIFFRecord.ChartScatter:
								goto IL_91;
							default:
								num = 7;
								continue;
							}
							break;
						case 1:
							if (this.ᜏ.Loading)
							{
								num = 4;
								continue;
							}
							goto IL_A1;
						case 2:
							switch (tbiffrecord)
							{
							case TBIFFRecord.ChartRadar:
								goto IL_E7;
							case TBIFFRecord.ChartSurface:
								goto IL_15A;
							case TBIFFRecord.ChartRadarArea:
								goto IL_14D;
							default:
								num = 10;
								continue;
							}
							break;
						case 3:
							goto IL_1BB;
						case 4:
							num = 6;
							continue;
						case 5:
							num = 3;
							continue;
						case 6:
							if (primaryFormats.Count == 0)
							{
								num = 9;
								continue;
							}
							goto IL_A1;
						case 7:
							num = 2;
							continue;
						case 8:
							if (tbiffrecord != TBIFFRecord.ChartBoppop)
							{
								if (true)
								{
								}
								num = 5;
								continue;
							}
							goto IL_103;
						case 9:
							goto IL_1AB;
						case 10:
							num = 8;
							continue;
						}
						break;
						IL_A1:
						commonSerieFormat = this.GetCommonSerieFormat();
						tbiffrecord = commonSerieFormat.FormatRecordType;
						num = 0;
					}
				}
				IL_91:
				return this.ᜀ(commonSerieFormat);
				IL_99:
				return this.ᜆ(commonSerieFormat);
				IL_E7:
				return this.ᜂ(commonSerieFormat);
				IL_FB:
				return this.ᜅ(commonSerieFormat);
				IL_103:
				return this.ᜃ(commonSerieFormat).ToString();
				IL_115:
				return this.ᜈ(commonSerieFormat);
				IL_15A:
				return this.ᜄ(commonSerieFormat);
				IL_1AB:
				return ExcelChartType.ColumnClustered.ToString();
				IL_1BB:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("砺尼儾晀㝂敄⍆ⱈ㽊⡌ⱎ═獒♔㉖⭘㉚㡜罞ᕠᩢᕤɦ䝨", a_));
				IL_1BD:
				return this.ᜁ(commonSerieFormat);
			}
			}
			IL_14D:
			return ExcelChartType.RadarFilled.ToString();
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x000D1220 File Offset: 0x000D0220
		private string ᜈ(XlsChartFormat A_0)
		{
			int a_ = 12;
			string text;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_33D:
				text += RecordTableEnumerator.b("獁瑃癅ᡇ⽉㹋ⵍ㕏㱑⁓", a_);
				num = 27;
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
				bool flag;
				bool flag2;
				switch (num)
				{
				case 0:
					goto IL_249;
				case 1:
					if (text.IndexOf(RecordTableEnumerator.b("Ł⭃⡅ⵇ", a_)) == -1)
					{
						num = 16;
						continue;
					}
					goto IL_2A5;
				case 2:
					goto IL_26B;
				case 3:
					text += RecordTableEnumerator.b("煁C", a_);
					num = 32;
					continue;
				case 4:
					if (A_0.ShowAsPercentsBar)
					{
						num = 29;
						continue;
					}
					goto IL_E8;
				case 6:
					num = 30;
					continue;
				case 7:
					if (!A_0.RightAngleAxes)
					{
						num = 17;
						continue;
					}
					goto IL_24B;
				case 8:
					if (!A_0.IsClustered)
					{
						num = 25;
						continue;
					}
					goto IL_3A9;
				case 9:
					goto IL_1CA;
				case 10:
					if (!A_0.StackValuesBar)
					{
						num = 3;
						continue;
					}
					goto IL_3A9;
				case 11:
					if (A_0.FormatRecordType != TBIFFRecord.ChartBar)
					{
						num = 23;
						continue;
					}
					text = string.Empty;
					text = this.ᜇ(A_0);
					num = 1;
					continue;
				case 12:
					text += RecordTableEnumerator.b("ᅁぃ❅⭇ⅉ⥋⩍", a_);
					num = 9;
					continue;
				case 13:
					if (flag)
					{
						num = 34;
						continue;
					}
					goto IL_3A9;
				case 14:
					if (!A_0.IsClustered)
					{
						num = 0;
						continue;
					}
					goto IL_24B;
				case 15:
					flag2 = true;
					goto IL_2F3;
				case 16:
					num = 19;
					continue;
				case 17:
					num = 14;
					continue;
				case 18:
					if (A_0.StackValuesBar)
					{
						num = 12;
						continue;
					}
					num = 20;
					continue;
				case 19:
					if (text.IndexOf(RecordTableEnumerator.b("Ł㵃⩅ⅇ⑉⡋⭍≏", a_)) == -1)
					{
						num = 6;
						continue;
					}
					goto IL_2A5;
				case 20:
					if (text == RecordTableEnumerator.b("Ł⭃⩅㵇❉≋", a_))
					{
						num = 26;
						continue;
					}
					goto IL_24B;
				case 21:
					if (A_0.Is3D)
					{
						num = 28;
						continue;
					}
					goto IL_36C;
				case 22:
					goto IL_36C;
				case 23:
					goto IL_3F5;
				case 24:
					num = 7;
					continue;
				case 25:
					num = 10;
					continue;
				case 26:
					num = 33;
					continue;
				case 27:
					goto IL_E8;
				case 28:
					num = 35;
					continue;
				case 29:
					goto IL_3CA;
				case 30:
					flag2 = (text.IndexOf(RecordTableEnumerator.b("ቁ㵃㑅⥇❉╋⩍", a_)) != -1);
					goto IL_2F3;
				case 31:
					text += RecordTableEnumerator.b("煁C", a_);
					num = 22;
					continue;
				case 32:
					goto IL_3A9;
				case 33:
					if (A_0.Is3D)
					{
						num = 24;
						continue;
					}
					goto IL_24B;
				case 34:
					num = 8;
					continue;
				case 35:
					if (!flag)
					{
						num = 31;
						continue;
					}
					goto IL_36C;
				case 36:
					goto IL_E3;
				}
				if (A_0 == null)
				{
					num = 36;
					continue;
				}
				num = 11;
				continue;
				IL_E8:
				num = 18;
				continue;
				IL_24B:
				text += RecordTableEnumerator.b("Ł⡃㍅㭇㹉⥋㱍㕏㙑", a_);
				num = 2;
				continue;
				IL_2A5:
				num = 15;
				continue;
				IL_2F3:
				flag = flag2;
				num = 21;
				continue;
				IL_36C:
				num = 13;
				continue;
				IL_3A9:
				num = 4;
			}
			IL_E3:
			throw new ArgumentNullException(RecordTableEnumerator.b("⑁⭃㑅╇⭉㡋", a_));
			IL_1CA:
			return text;
			IL_249:
			return ExcelChartType.Column3D.ToString();
			IL_26B:
			return text;
			IL_3CA:
			goto IL_33D;
			IL_3F5:
			if (true)
			{
			}
			throw new ArgumentException(RecordTableEnumerator.b("⑁⭃㑅╇⭉㡋", a_));
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x000D1680 File Offset: 0x000D0680
		private string ᜇ(XlsChartFormat A_0)
		{
			int a_ = 10;
			int num = 20;
			string text3;
			for (;;)
			{
				string text4;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_365;
				default:
				{
					if (false)
					{
					}
					string text;
					string text2;
					switch (num)
					{
					case 0:
					{
						XlsChartSerieDataFormat dataFormatOrNull;
						if (dataFormatOrNull.BarTopType != TopFormatType.Straight)
						{
							num = 14;
							continue;
						}
						num = 31;
						continue;
					}
					case 1:
						text = RecordTableEnumerator.b("̿ⵁ⩃⍅", a_);
						goto IL_1E2;
					case 2:
						if (!A_0.IsHorizontalBar)
						{
							num = 13;
							continue;
						}
						num = 4;
						continue;
					case 3:
						num = 19;
						continue;
					case 4:
						text2 = RecordTableEnumerator.b("ȿ⍁㙃", a_);
						goto IL_2B6;
					case 5:
						text3 += RecordTableEnumerator.b("ȿ⍁㙃", a_);
						num = 10;
						continue;
					case 6:
						num = 0;
						continue;
					case 7:
						goto IL_2B4;
					case 8:
						if (A_0.DataFormatOrNull != null)
						{
							num = 18;
							continue;
						}
						goto IL_F9;
					case 9:
						num = 15;
						continue;
					case 10:
						goto IL_226;
					case 11:
					{
						XlsChartSerieDataFormat dataFormatOrNull;
						if (dataFormatOrNull.BarTopType == TopFormatType.Straight)
						{
							num = 3;
							continue;
						}
						text3 = RecordTableEnumerator.b("ဿ㭁㙃❅╇⍉⡋", a_);
						num = 17;
						continue;
					}
					case 12:
						text3 += RecordTableEnumerator.b("ȿ⍁㙃", a_);
						num = 24;
						continue;
					case 13:
						num = 30;
						continue;
					case 14:
						num = 1;
						continue;
					case 15:
						text4 = RecordTableEnumerator.b("̿ⵁ⡃㍅╇⑉", a_);
						goto IL_393;
					case 16:
					{
						XlsChartSerieDataFormat dataFormatOrNull = A_0.DataFormatOrNull;
						num = 27;
						continue;
					}
					case 17:
						if (A_0.IsHorizontalBar)
						{
							num = 5;
							continue;
						}
						return text3;
					case 18:
						num = 29;
						continue;
					case 19:
						if (!A_0.IsHorizontalBar)
						{
							num = 9;
							continue;
						}
						num = 25;
						continue;
					case 21:
						if (A_0.IsHorizontalBar)
						{
							num = 12;
							continue;
						}
						return text3;
					case 22:
						if (A_0.FormatRecordType != TBIFFRecord.ChartBar)
						{
							num = 7;
							continue;
						}
						text3 = null;
						num = 8;
						continue;
					case 23:
						return text3;
					case 24:
						goto IL_253;
					case 25:
						goto IL_365;
					case 26:
						goto IL_CF;
					case 27:
					{
						XlsChartSerieDataFormat dataFormatOrNull;
						if (dataFormatOrNull.BarType == BaseFormatType.Circle)
						{
							num = 6;
							continue;
						}
						num = 11;
						continue;
					}
					case 28:
						return text3;
					case 29:
						if (A_0.DataFormatOrNull.Serie3DdDataFormatOrNull != null)
						{
							num = 16;
							continue;
						}
						goto IL_F9;
					case 30:
						text2 = RecordTableEnumerator.b("̿ⵁ⡃㍅╇⑉", a_);
						goto IL_2B6;
					case 31:
						text = RecordTableEnumerator.b("̿㭁⡃⽅♇⹉⥋㱍", a_);
						goto IL_1E2;
					}
					if (A_0 == null)
					{
						num = 26;
						continue;
					}
					num = 22;
					continue;
					IL_F9:
					num = 2;
					continue;
					IL_1E2:
					text3 = text;
					num = 21;
					continue;
					IL_2B6:
					text3 = text2;
					num = 23;
					continue;
				}
				}
				IL_393:
				text3 = text4;
				num = 28;
				continue;
				IL_365:
				text4 = RecordTableEnumerator.b("ȿ⍁㙃", a_);
				goto IL_393;
			}
			IL_CF:
			throw new ArgumentNullException(RecordTableEnumerator.b("☿ⵁ㙃⭅⥇㹉", a_));
			IL_226:
			if (true)
			{
			}
			IL_253:
			return text3;
			IL_2B4:
			throw new ArgumentException(RecordTableEnumerator.b("☿ⵁ㙃⭅⥇㹉", a_));
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x000D1A30 File Offset: 0x000D0A30
		private string ᜆ(XlsChartFormat A_0)
		{
			int a_ = 14;
			int num = 2;
			string text;
			for (;;)
			{
				string text2;
				switch (num)
				{
				case 0:
					text += RecordTableEnumerator.b("Ń㹅㡇♉⍋⩍㕏㙑", a_);
					num = 5;
					continue;
				case 1:
					num = 17;
					continue;
				case 3:
					num = 8;
					continue;
				case 4:
					goto IL_1E5;
				case 5:
					goto IL_FA;
				case 6:
					if (A_0.DoughnutHoleSize == 0)
					{
						num = 3;
						continue;
					}
					num = 9;
					continue;
				case 7:
					if (A_0.FormatRecordType != TBIFFRecord.ChartPie)
					{
						if (true)
						{
						}
						num = 4;
						continue;
					}
					num = 6;
					continue;
				case 8:
					text2 = RecordTableEnumerator.b("ᑃ⽅ⵇ", a_);
					goto IL_12C;
				case 9:
					text2 = RecordTableEnumerator.b("C⥅㵇ⵉ⑋⁍╏♑", a_);
					goto IL_12C;
				case 10:
					goto IL_187;
				case 11:
					goto IL_75;
				case 12:
					if (A_0.Is3D)
					{
						num = 16;
						continue;
					}
					goto IL_187;
				case 13:
					if (A_0.DataFormatOrNull != null)
					{
						num = 15;
						continue;
					}
					return text;
				case 14:
				{
					XlsChartSerieDataFormat dataFormatOrNull;
					if (dataFormatOrNull.PieFormatOrNull != null)
					{
						num = 1;
						continue;
					}
					return text;
				}
				case 15:
				{
					XlsChartSerieDataFormat dataFormatOrNull = A_0.DataFormatOrNull;
					num = 14;
					continue;
				}
				case 16:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return text;
					default:
						if (false)
						{
						}
						text += RecordTableEnumerator.b("睃Ʌ", a_);
						num = 10;
						continue;
					}
					break;
				case 17:
				{
					XlsChartSerieDataFormat dataFormatOrNull;
					if (dataFormatOrNull.Percent > 0)
					{
						num = 0;
						continue;
					}
					return text;
				}
				}
				if (A_0 == null)
				{
					num = 11;
					continue;
				}
				num = 7;
				continue;
				IL_12C:
				text = text2;
				num = 12;
				continue;
				IL_187:
				num = 13;
			}
			IL_75:
			throw new ArgumentNullException(RecordTableEnumerator.b("≃⥅㩇❉ⵋ㩍", a_));
			IL_FA:
			return text;
			IL_1E5:
			throw new ArgumentException(RecordTableEnumerator.b("≃⥅㩇❉ⵋ㩍", a_));
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x000D1C74 File Offset: 0x000D0C74
		private string ᜅ(XlsChartFormat A_0)
		{
			int a_ = 11;
			if (true)
			{
			}
			int num = 1;
			string text;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_CB;
				case 2:
					goto IL_7E;
				case 3:
					if (A_0.Is3D)
					{
						num = 10;
						continue;
					}
					goto IL_1E1;
				case 4:
					if (A_0.FormatRecordType != TBIFFRecord.ChartArea)
					{
						num = 16;
						continue;
					}
					num = 3;
					continue;
				case 5:
					goto IL_17B;
				case 6:
					goto IL_158;
				case 7:
					if (A_0.IsCategoryBrokenDown)
					{
						num = 15;
						continue;
					}
					goto IL_CB;
				case 8:
					if (A_0.IsStacked)
					{
						num = 11;
						continue;
					}
					return text;
				case 9:
					if (!A_0.IsStacked)
					{
						num = 5;
						continue;
					}
					goto IL_1E1;
				case 10:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						num = 13;
						continue;
					}
					break;
				case 11:
					text += RecordTableEnumerator.b("ቀ㝂⑄⑆≈⹊⥌", a_);
					num = 6;
					continue;
				case 12:
					if (A_0.Is3D)
					{
						num = 17;
						continue;
					}
					goto IL_83;
				case 13:
					if (!A_0.RightAngleAxes)
					{
						num = 18;
						continue;
					}
					goto IL_1E1;
				case 14:
					goto IL_83;
				case 15:
					text += RecordTableEnumerator.b("灀獂畄ᝆⱈ㥊⹌⩎㽐❒", a_);
					num = 0;
					continue;
				case 16:
					goto IL_111;
				case 17:
					text += RecordTableEnumerator.b("牀݂", a_);
					num = 14;
					continue;
				case 18:
					num = 9;
					continue;
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				num = 4;
				continue;
				IL_83:
				num = 7;
				continue;
				IL_CB:
				num = 8;
				continue;
				IL_1E1:
				text = RecordTableEnumerator.b("@ㅂ⁄♆", a_);
				num = 12;
			}
			IL_7E:
			throw new ArgumentNullException(RecordTableEnumerator.b("❀ⱂ㝄⩆⡈㽊", a_));
			IL_111:
			throw new ArgumentException(RecordTableEnumerator.b("❀ⱂ㝄⩆⡈㽊", a_));
			IL_158:
			return text;
			IL_17B:
			return ExcelChartType.Area3D.ToString();
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x000D1ED0 File Offset: 0x000D0ED0
		private string ᜄ(XlsChartFormat A_0)
		{
			int a_ = 14;
			int num = 6;
			for (;;)
			{
				string text;
				switch (num)
				{
				case 0:
					num = 10;
					continue;
				case 1:
					goto IL_D8;
				case 2:
					if (A_0.FormatRecordType != TBIFFRecord.ChartSurface)
					{
						num = 1;
						continue;
					}
					text = RecordTableEnumerator.b("ᝃ㍅㩇ⱉⵋⵍ㕏", a_);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A5;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				case 3:
					text += RecordTableEnumerator.b("੃⥅େ╉⁋⅍≏", a_);
					num = 8;
					continue;
				case 4:
					if (A_0.Rotation == 0)
					{
						num = 0;
						continue;
					}
					goto IL_73;
				case 5:
					if (A_0.Perspective == 0)
					{
						goto IL_A5;
					}
					goto IL_73;
				case 7:
					goto IL_DD;
				case 8:
					return text;
				case 9:
					if (!A_0.IsFillSurface)
					{
						num = 3;
						continue;
					}
					return text;
				case 10:
					if (A_0.Elevation == 90)
					{
						num = 11;
						continue;
					}
					goto IL_73;
				case 11:
					num = 5;
					continue;
				case 12:
					text += RecordTableEnumerator.b("݃⥅♇㹉⍋㭍≏", a_);
					num = 14;
					continue;
				case 13:
					goto IL_6E;
				case 14:
					goto IL_DD;
				}
				if (true)
				{
				}
				if (A_0 == null)
				{
					num = 13;
					continue;
				}
				num = 2;
				continue;
				IL_73:
				text += RecordTableEnumerator.b("睃Ʌ", a_);
				num = 7;
				continue;
				IL_A5:
				num = 12;
				continue;
				IL_DD:
				num = 9;
			}
			IL_6E:
			throw new ArgumentNullException(RecordTableEnumerator.b("≃⥅㩇❉ⵋ㩍", a_));
			IL_D8:
			throw new ArgumentException(RecordTableEnumerator.b("≃⥅㩇❉ⵋ㩍", a_));
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x000D20D0 File Offset: 0x000D10D0
		private ExcelChartType ᜃ(XlsChartFormat A_0)
		{
			int a_ = 2;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					ChartPieType pieChartType;
					switch (pieChartType)
					{
					case ChartPieType.Normal:
						return ExcelChartType.Pie;
					case ChartPieType.Pie:
						return ExcelChartType.PieOfPie;
					case ChartPieType.Bar:
						return ExcelChartType.PieBar;
					default:
						num = 1;
						continue;
					}
					break;
				}
				case 1:
					num = 2;
					continue;
				case 2:
					goto IL_53;
				case 4:
					goto IL_91;
				case 5:
					goto IL_46;
				case 6:
				{
					if (A_0.FormatRecordType != TBIFFRecord.ChartBoppop)
					{
						num = 4;
						continue;
					}
					ChartPieType pieChartType = A_0.PieChartType;
					num = 0;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 5;
				}
				else
				{
					num = 6;
				}
			}
			IL_46:
			throw new ArgumentNullException(RecordTableEnumerator.b("帷唹主匽ℿ㙁", a_));
			IL_53:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_EB:
				throw new ArgumentException(RecordTableEnumerator.b("帷唹主匽ℿ㙁", a_));
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				throw new ApplicationException(RecordTableEnumerator.b("洷吹圻倽⼿㕁⩃晅㭇⽉㹋❍㕏牑⁓⽕⡗㽙牛", a_));
			}
			IL_91:
			goto IL_EB;
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x000D21F8 File Offset: 0x000D11F8
		private string ᜂ(XlsChartFormat A_0)
		{
			int a_ = 15;
			int num = 6;
			for (;;)
			{
				XlsChartSerieDataFormat defaultPointFormat;
				switch (num)
				{
				case 0:
				{
					if (A_0.FormatRecordType != TBIFFRecord.ChartRadar)
					{
						num = 11;
						continue;
					}
					string text = RecordTableEnumerator.b("ᝄ♆ⵈ⩊㽌", a_);
					num = 4;
					continue;
				}
				case 1:
				{
					string text = RecordTableEnumerator.b("ᝄ♆ⵈ⩊㽌", a_);
					num = 5;
					continue;
				}
				case 2:
				{
					string text;
					return text;
				}
				case 3:
					goto IL_6A;
				case 4:
					if (!A_0.IsMarker)
					{
						goto IL_11F;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A1;
					default:
						if (false)
						{
						}
						num = 12;
						continue;
					}
					break;
				case 5:
				{
					if (defaultPointFormat.IsMarker)
					{
						num = 10;
						continue;
					}
					string text;
					return text;
				}
				case 7:
				{
					if (defaultPointFormat.HasBorder)
					{
						num = 1;
						continue;
					}
					string text;
					return text;
				}
				case 8:
					goto IL_11F;
				case 9:
					num = 7;
					continue;
				case 10:
				{
					string text;
					text += RecordTableEnumerator.b("ࡄ♆㭈⁊⡌㵎≐", a_);
					num = 2;
					continue;
				}
				case 11:
					goto IL_D4;
				case 12:
				{
					string text;
					text += RecordTableEnumerator.b("ࡄ♆㭈⁊⡌㵎≐", a_);
					goto IL_A1;
				}
				case 13:
				{
					if (defaultPointFormat != null)
					{
						num = 9;
						continue;
					}
					string text;
					return text;
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
				num = 0;
				continue;
				IL_A1:
				num = 8;
				continue;
				IL_11F:
				defaultPointFormat = ((XlsChartDataPointsCollection)this.DataPoints).DefaultPointFormat;
				num = 13;
			}
			IL_6A:
			throw new ArgumentNullException(RecordTableEnumerator.b("⍄⡆㭈♊ⱌ㭎", a_));
			IL_D4:
			throw new ArgumentException(RecordTableEnumerator.b("⍄⡆㭈♊ⱌ㭎", a_));
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x000D23E8 File Offset: 0x000D13E8
		private string ᜁ(XlsChartFormat A_0)
		{
			int a_ = 13;
			int num = 6;
			for (;;)
			{
				string text;
				switch (num)
				{
				case 0:
					goto IL_1E1;
				case 1:
					num = 20;
					continue;
				case 2:
					goto IL_12E;
				case 3:
				{
					bool flag = true;
					num = 12;
					continue;
				}
				case 4:
					goto IL_B4;
				case 5:
					goto IL_2D9;
				case 7:
					goto IL_178;
				case 8:
					if (!A_0.IsMarker)
					{
						if (true)
						{
						}
						num = 5;
						continue;
					}
					goto IL_17D;
				case 9:
				{
					bool flag;
					if (flag)
					{
						num = 1;
						continue;
					}
					goto IL_1E1;
				}
				case 10:
				{
					bool flag;
					if (!flag)
					{
						num = 13;
						continue;
					}
					goto IL_2D9;
				}
				case 11:
					text += RecordTableEnumerator.b("၂ㅄ♆⩈⁊⡌⭎", a_);
					goto IL_219;
				case 12:
					goto IL_2AC;
				case 13:
					num = 8;
					continue;
				case 14:
					if (A_0.StackValuesLine)
					{
						num = 11;
						continue;
					}
					return text;
				case 15:
					goto IL_19F;
				case 16:
					if (A_0.FormatRecordType != TBIFFRecord.ChartLine)
					{
						num = 7;
						continue;
					}
					num = 17;
					continue;
				case 17:
				{
					if (A_0.Is3D)
					{
						num = 2;
						continue;
					}
					bool flag = false;
					XlsChartSerieDataFormat defaultPointFormat = ((XlsChartDataPointsCollection)this.DataPoints).DefaultPointFormat;
					num = 19;
					continue;
				}
				case 18:
					if (A_0.ShowAsPercentsLine)
					{
						num = 22;
						continue;
					}
					goto IL_19F;
				case 19:
				{
					XlsChartSerieDataFormat defaultPointFormat;
					if (defaultPointFormat != null)
					{
						num = 25;
						continue;
					}
					goto IL_2AC;
				}
				case 20:
				{
					XlsChartSerieDataFormat defaultPointFormat;
					if (defaultPointFormat.IsMarker)
					{
						num = 23;
						continue;
					}
					goto IL_1E1;
				}
				case 21:
					return text;
				case 22:
					text += RecordTableEnumerator.b("牂畄睆᥈⹊㽌ⱎ㑐㵒⅔", a_);
					num = 15;
					continue;
				case 23:
					goto IL_17D;
				case 24:
				{
					XlsChartSerieDataFormat defaultPointFormat;
					if (defaultPointFormat.HasBorder)
					{
						num = 3;
						continue;
					}
					goto IL_2AC;
				}
				case 25:
					num = 24;
					continue;
				}
				if (!(A_0 == null))
				{
					num = 16;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_219;
				default:
					if (false)
					{
					}
					num = 4;
					continue;
				}
				IL_17D:
				text += RecordTableEnumerator.b("โ⑄㕆≈⹊㽌㱎", a_);
				num = 0;
				continue;
				IL_19F:
				num = 14;
				continue;
				IL_1E1:
				num = 18;
				continue;
				IL_219:
				num = 21;
				continue;
				IL_2AC:
				text = RecordTableEnumerator.b("གⱄ⥆ⱈ", a_);
				num = 10;
				continue;
				IL_2D9:
				num = 9;
			}
			IL_B4:
			throw new ArgumentNullException(RecordTableEnumerator.b("╂⩄㕆⑈⩊㥌", a_));
			IL_12E:
			return ExcelChartType.Line3D.ToString();
			IL_178:
			throw new ArgumentException(RecordTableEnumerator.b("╂⩄㕆⑈⩊㥌", a_));
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x000D26F0 File Offset: 0x000D16F0
		private string ᜀ(XlsChartFormat A_0)
		{
			int a_ = 4;
			switch (0)
			{
			default:
			{
				int num = 41;
				string text;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_179;
					case 1:
						goto IL_4AF;
					case 2:
						num = 31;
						continue;
					case 3:
						if (A_0.IsSmoothed)
						{
							num = 30;
							continue;
						}
						goto IL_133;
					case 4:
						num = 10;
						continue;
					case 5:
						goto IL_45C;
					case 6:
						goto IL_39C;
					case 7:
						goto IL_37C;
					case 8:
					{
						XlsChartSerieDataFormat dataFormatOrNull;
						if (dataFormatOrNull.SerieFormatOrNull != null)
						{
							num = 16;
							continue;
						}
						goto IL_40C;
					}
					case 9:
						goto IL_288;
					case 10:
					{
						XlsChartSerieDataFormat defaultPointFormat;
						if (!defaultPointFormat.HasBorderLine)
						{
							num = 1;
							continue;
						}
						goto IL_10D;
					}
					case 11:
					{
						if (A_0.IsBubbles)
						{
							num = 35;
							continue;
						}
						XlsChartSerieDataFormat defaultPointFormat = ((XlsChartDataPointsCollection)this.DataPoints).DefaultPointFormat;
						bool hasBorder = defaultPointFormat.HasBorder;
						text = RecordTableEnumerator.b("椹弻弽㐿㙁⅃㑅", a_);
						if (true)
						{
						}
						num = 13;
						continue;
					}
					case 12:
						goto IL_461;
					case 13:
					{
						bool hasBorder;
						if (hasBorder)
						{
							num = 28;
							continue;
						}
						goto IL_39C;
					}
					case 14:
						goto IL_17E;
					case 15:
					{
						XlsChartSerieDataFormat dataFormatOrNull;
						if (dataFormatOrNull.Is3DBubbles)
						{
							num = 0;
							continue;
						}
						goto IL_40C;
					}
					case 16:
						num = 15;
						continue;
					case 17:
					{
						XlsChartSerieDataFormat defaultPointFormat;
						if (!defaultPointFormat.IsSmoothed)
						{
							num = 6;
							continue;
						}
						goto IL_217;
					}
					case 18:
						if (A_0.DataFormatOrNull != null)
						{
							num = 23;
							continue;
						}
						goto IL_40C;
					case 19:
						text = ExcelChartType.ScatterLine.ToString();
						num = 9;
						continue;
					case 20:
					{
						bool hasBorder;
						if (!hasBorder)
						{
							num = 2;
							continue;
						}
						goto IL_37C;
					}
					case 21:
						goto IL_37C;
					case 22:
						goto IL_1E4;
					case 23:
					{
						XlsChartSerieDataFormat dataFormatOrNull = A_0.DataFormatOrNull;
						num = 8;
						continue;
					}
					case 24:
						num = 34;
						continue;
					case 25:
						goto IL_10D;
					case 26:
						if (text == RecordTableEnumerator.b("椹弻弽㐿㙁⅃㑅", a_))
						{
							num = 19;
							continue;
						}
						return text;
					case 27:
						if (A_0.FormatRecordType != TBIFFRecord.ChartScatter)
						{
							num = 5;
							continue;
						}
						num = 18;
						continue;
					case 28:
						num = 17;
						continue;
					case 29:
					{
						bool hasBorder;
						if (hasBorder)
						{
							num = 4;
							continue;
						}
						goto IL_4AF;
					}
					case 30:
						goto IL_217;
					case 31:
						if (A_0.IsLine)
						{
							num = 25;
							continue;
						}
						goto IL_37C;
					case 32:
						goto IL_108;
					case 33:
						if (A_0.IsMarker)
						{
							num = 12;
							continue;
						}
						goto IL_1E4;
					case 34:
					{
						XlsChartSerieDataFormat defaultPointFormat;
						if (!defaultPointFormat.IsMarker)
						{
							num = 14;
							continue;
						}
						goto IL_461;
					}
					case 35:
						goto IL_42F;
					case 36:
					{
						bool hasBorder;
						if (!hasBorder)
						{
							num = 39;
							continue;
						}
						goto IL_133;
					}
					case 37:
						num = 33;
						continue;
					case 38:
					{
						bool hasBorder;
						if (!hasBorder)
						{
							num = 37;
							continue;
						}
						goto IL_1E4;
					}
					case 39:
						num = 3;
						continue;
					case 40:
					{
						bool hasBorder;
						if (hasBorder)
						{
							num = 24;
							continue;
						}
						goto IL_17E;
					}
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_19E;
					default:
						if (false)
						{
						}
						if (A_0 == null)
						{
							num = 32;
							continue;
						}
						num = 27;
						continue;
					}
					IL_10D:
					text += RecordTableEnumerator.b("瘹唻倽┿", a_);
					num = 7;
					continue;
					IL_133:
					num = 29;
					continue;
					IL_17E:
					num = 38;
					continue;
					IL_1E4:
					num = 26;
					continue;
					IL_217:
					text += RecordTableEnumerator.b("椹儻儽⼿㙁ⱃ⍅ⱇى╋⁍㕏", a_);
					num = 21;
					continue;
					IL_37C:
					num = 40;
					continue;
					IL_39C:
					num = 36;
					continue;
					IL_40C:
					num = 11;
					continue;
					IL_461:
					text += RecordTableEnumerator.b("眹崻䰽⬿❁㙃㕅", a_);
					num = 22;
					continue;
					IL_4AF:
					num = 20;
				}
				IL_108:
				throw new ArgumentNullException(RecordTableEnumerator.b("尹医䰽ⴿ⍁ぃ", a_));
				IL_179:
				return ExcelChartType.Bubble3D.ToString();
				IL_19E:
				throw new ArgumentException(RecordTableEnumerator.b("尹医䰽ⴿ⍁ぃ", a_));
				IL_288:
				return text;
				IL_42F:
				return ExcelChartType.Bubble.ToString();
				IL_45C:
				goto IL_19E;
			}
			}
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x000D2BD0 File Offset: 0x000D1BD0
		internal void ᜁ(ExcelChartType A_0, bool A_1)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_41;
				case 1:
					goto IL_43;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_41;
					default:
						if (false)
						{
						}
						if (this.SerieType != A_0)
						{
							num = 1;
							continue;
						}
						return;
					}
					break;
				case 4:
					return;
				}
				if (!this.\u170D.Loading)
				{
					if (true)
					{
					}
					num = 0;
					continue;
				}
				goto IL_43;
				IL_41:
				num = 2;
				continue;
				IL_43:
				this.ᜏ.TypeChanging = true;
				this.ᜀ(A_0, A_1);
				this.\u1713 = A_0;
				this.ᜏ.TypeChanging = false;
				num = 4;
			}
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x000D2C98 File Offset: 0x000D1C98
		private void ᜀ(ExcelChartType A_0, bool A_1)
		{
			int a_ = 14;
			for (;;)
			{
				this.m_dataPoints.Clear();
				this.ᜏ.HasDataTable = false;
				this.HasErrorBarsX = false;
				this.HasErrorBarsY = false;
				this.\u171E.Clear();
				ExcelChartType chartType = this.ᜏ.ChartType;
				int num = 9;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜏ.PrimaryParentAxis.ᜄ();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_14F;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num = 14;
							continue;
						}
						break;
					case 1:
						if (this.ᜏ.Series.Count == 1)
						{
							num = 11;
							continue;
						}
						num = 17;
						continue;
					case 2:
						if (Array.IndexOf<ExcelChartType>(XlsChart.ᜬ, A_0) != -1)
						{
							num = 0;
							continue;
						}
						goto IL_2B2;
					case 3:
						num = 16;
						continue;
					case 4:
					{
						XlsChartFormat commonSerieFormat;
						if (this.ᜀ(commonSerieFormat, A_0))
						{
							num = 7;
							continue;
						}
						num = 19;
						continue;
					}
					case 5:
						if (Array.IndexOf<ExcelChartType>(XlsChart.ᜫ, chartType) != -1)
						{
							num = 18;
							continue;
						}
						goto IL_215;
					case 6:
						goto IL_293;
					case 7:
						return;
					case 8:
						if (chartType == ExcelChartType.Bubble)
						{
							num = 20;
							continue;
						}
						num = 2;
						continue;
					case 9:
						if (Array.IndexOf<ExcelChartType>(XlsChart.ᜮ, chartType) != -1)
						{
							num = 15;
							continue;
						}
						goto IL_14F;
					case 10:
						num = 8;
						continue;
					case 11:
						goto IL_136;
					case 12:
						num = 13;
						continue;
					case 13:
						if (A_0 != ExcelChartType.Bubble3D)
						{
							num = 3;
							continue;
						}
						goto IL_13B;
					case 14:
						goto IL_1D7;
					case 15:
						goto IL_C8;
					case 16:
						if (chartType != ExcelChartType.Bubble3D)
						{
							num = 10;
							continue;
						}
						goto IL_13B;
					case 17:
					{
						if (Array.IndexOf<ExcelChartType>(XlsChart.ᜫ, A_0) == -1)
						{
							num = 6;
							continue;
						}
						XlsChartFormat commonSerieFormat = this.GetCommonSerieFormat();
						num = 4;
						continue;
					}
					case 18:
						num = 1;
						continue;
					case 19:
						if (A_0 != ExcelChartType.Bubble)
						{
							num = 12;
							continue;
						}
						goto IL_13B;
					case 20:
						goto IL_267;
					}
					break;
					IL_14F:
					num = 5;
				}
			}
			IL_C8:
			this.ᜀ(A_0);
			return;
			IL_136:
			goto IL_215;
			IL_13B:
			throw new ArgumentException(RecordTableEnumerator.b("݃⹅⥇⑉⭋⭍灏⅑ㅓ⑕ㅗ㽙籛⩝ᥟቡţ䙥๧୩իɭᕯᙱ婳", a_));
			IL_1D7:
			goto IL_2B2;
			IL_215:
			this.ᜏ.ᜁ(A_0, A_1);
			return;
			IL_267:
			goto IL_13B;
			IL_293:
			throw new ArgumentException(RecordTableEnumerator.b("݃⹅⥇⑉⭋⭍灏⅑ㅓ⑕ㅗ㽙籛⩝ᥟቡţ䙥๧୩իɭᕯᙱ婳", a_));
			IL_2B2:
			this.ᜁ(A_0);
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x000D2F60 File Offset: 0x000D1F60
		private void ᜁ(ExcelChartType A_0)
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
			this.ᜏ.IsManuallyFormatted = true;
			XlsChartFormat xlsChartFormat = this.ᜏ.PrimaryParentAxis.ᜆ().ᜀ(A_0, this.SerieType, base.ReservedHandle, this.ᜏ, this);
			this.ChartGroup = xlsChartFormat.DrawingZOrder;
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x000D2FE0 File Offset: 0x000D1FE0
		private bool ᜀ(XlsChartFormat A_0, ExcelChartType A_1)
		{
			int a_ = 10;
			switch (0)
			{
			default:
			{
				int num = 6;
				string text2;
				XlsChartDataPoint xlsChartDataPoint;
				XlsChartSerieDataFormat dataFormatOrNull;
				for (;;)
				{
					string text;
					switch (num)
					{
					case 0:
						num = 12;
						continue;
					case 1:
						if (text != text2)
						{
							num = 2;
							continue;
						}
						goto IL_F8;
					case 2:
						return false;
					case 3:
						if (text == RecordTableEnumerator.b("ሿ⍁⁃❅㩇", a_))
						{
							num = 17;
							continue;
						}
						goto IL_1C8;
					case 4:
						goto IL_26D;
					case 5:
						num = 9;
						continue;
					case 7:
						num = 18;
						continue;
					case 8:
						goto IL_1A2;
					case 9:
					{
						string a;
						if (!(a == RecordTableEnumerator.b("ȿ㝁♃⑅⑇⽉", a_)))
						{
							num = 11;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_F8;
						default:
							goto IL_225;
						}
						break;
					}
					case 10:
						goto IL_AB;
					case 11:
						num = 14;
						continue;
					case 12:
						if (A_1 != ExcelChartType.RadarFilled)
						{
							num = 4;
							continue;
						}
						goto IL_1C8;
					case 13:
						num = 8;
						continue;
					case 14:
					{
						string a;
						if (!(a == RecordTableEnumerator.b("ి⭁⩃⍅", a_)))
						{
							num = 13;
							continue;
						}
						goto IL_152;
					}
					case 15:
					{
						string a;
						if ((a = text) != null)
						{
							num = 7;
							continue;
						}
						goto IL_1A2;
					}
					case 16:
						if (this.SerieType != ExcelChartType.RadarFilled)
						{
							num = 0;
							continue;
						}
						goto IL_1C8;
					case 17:
						num = 16;
						continue;
					case 18:
					{
						string a;
						if (!(a == RecordTableEnumerator.b("ጿ⅁╃㉅㱇⽉㹋", a_)))
						{
							num = 5;
							continue;
						}
						goto IL_199;
					}
					case 19:
						if (this.SerieType != A_1)
						{
							num = 21;
							continue;
						}
						return true;
					case 20:
						goto IL_248;
					case 21:
						A_0.ᜂ(A_1, false);
						num = 20;
						continue;
					}
					if (true)
					{
					}
					if (A_0 == null)
					{
						num = 10;
						continue;
					}
					text = XlsChartFormat.ᜉ(this.SerieType);
					text2 = XlsChartFormat.ᜉ(A_1);
					num = 1;
					continue;
					IL_F8:
					text2 = A_1.ToString();
					xlsChartDataPoint = (XlsChartDataPoint)this.m_dataPoints.DefaultDataPoint;
					dataFormatOrNull = xlsChartDataPoint.DataFormatOrNull;
					num = 3;
					continue;
					IL_1A2:
					num = 19;
					continue;
					IL_1C8:
					num = 15;
				}
				IL_AB:
				throw new ArgumentNullException(RecordTableEnumerator.b("☿ⵁ㙃⭅⥇㹉", a_));
				IL_152:
				this.ᜀ(A_0, A_1, text2);
				return true;
				IL_199:
				dataFormatOrNull.ChangeScatterDataFormat(A_1);
				return true;
				IL_225:
				if (false)
				{
				}
				xlsChartDataPoint.ChangeIntimateBuble(A_1);
				return true;
				IL_248:
				return true;
				IL_26D:
				dataFormatOrNull.ChangeRadarDataFormat(A_1);
				return true;
			}
			}
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x000D32EC File Offset: 0x000D22EC
		private void ᜀ(XlsChartFormat A_0, ExcelChartType A_1, string A_2)
		{
			int a_ = 1;
			bool flag = A_2.IndexOf(RecordTableEnumerator.b("稶堸䤺嘼娾㍀あ", a_)) != -1;
			if (A_0.IsMarker == flag)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_5D;
				}
				if (true)
				{
				}
				if (false)
				{
				}
				IL_5D:
				A_0.ᜂ(A_1, false);
				return;
			}
			Dictionary<ExcelChartType, ExcelChartType> dictionary = new Dictionary<ExcelChartType, ExcelChartType>(7);
			this.ᜀ(dictionary);
			A_0.ᜂ(dictionary[A_1], false);
			XlsChartDataPoint xlsChartDataPoint = (XlsChartDataPoint)this.DataPoints.DefaultDataPoint;
			((XlsChartSerieDataFormat)xlsChartDataPoint.DataFormat).ChangeLineDataFormat(A_1);
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x000D33A0 File Offset: 0x000D23A0
		private void ᜀ(Dictionary<ExcelChartType, ExcelChartType> A_0)
		{
			int a_ = 13;
			if (true)
			{
			}
			if (A_0 == null)
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
				throw new ArgumentNullException(RecordTableEnumerator.b("⭂⑄㑆ⅈ", a_));
			}
			A_0.Add(ExcelChartType.Line, ExcelChartType.LineMarkers);
			A_0.Add(ExcelChartType.LineStacked, ExcelChartType.LineMarkersStacked);
			A_0.Add(ExcelChartType.Line100PercentStacked, ExcelChartType.LineMarkers100PercentStacked);
			A_0.Add(ExcelChartType.LineMarkersStacked, ExcelChartType.LineStacked);
			A_0.Add(ExcelChartType.LineMarkers100PercentStacked, ExcelChartType.Line100PercentStacked);
			A_0.Add(ExcelChartType.LineMarkers, ExcelChartType.Line);
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x000D343C File Offset: 0x000D243C
		private void ᜀ(ExcelChartType A_0)
		{
			int a_ = 12;
			switch (0)
			{
			default:
			{
				XlsChartFormat xlsChartFormat;
				XlsChartFormatCollection xlsChartFormatCollection;
				for (;;)
				{
					xlsChartFormat = this.ᜀ(A_0, this.UsePrimaryAxis, true);
					XlsChartFormat commonSerieFormat = this.GetCommonSerieFormat();
					int drawingZOrder = commonSerieFormat.DrawingZOrder;
					int num = 19;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							bool flag;
							if (flag)
							{
								num = 15;
								continue;
							}
							goto IL_319;
						}
						case 1:
						{
							bool flag2;
							if (!flag2)
							{
								xlsChartFormatCollection = this.ᜏ.PrimaryFormats;
								num = 16;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_27C;
							default:
								if (false)
								{
								}
								num = 9;
								continue;
							}
							break;
						}
						case 2:
							this.ᜏ.PrimaryParentAxis.ᜆ().Remove(commonSerieFormat);
							num = 3;
							continue;
						case 3:
							goto IL_14B;
						case 4:
						{
							bool flag2;
							if (flag2)
							{
								num = 17;
								continue;
							}
							goto IL_B3;
						}
						case 5:
							goto IL_11A;
						case 6:
							this.ChartGroup = xlsChartFormat.DrawingZOrder;
							num = 12;
							continue;
						case 7:
							goto IL_269;
						case 8:
							goto IL_317;
						case 9:
							xlsChartFormatCollection = this.ᜏ.SecondaryFormats;
							this.ᜏ.SecondaryParentAxis.ᜁ(true);
							num = 7;
							continue;
						case 10:
							this.ChartGroup = ((this.ChartGroup == 0) ? 1 : 0);
							this.ᜏ.PrimaryParentAxis.ᜆ().Remove(commonSerieFormat);
							num = 8;
							continue;
						case 11:
							goto IL_2BB;
						case 12:
							if (this.ᜐ.ᜅ(drawingZOrder) == 0)
							{
								num = 2;
								continue;
							}
							goto IL_14B;
						case 13:
							if (this.ᜐ.ᜀ(A_0, this.UsePrimaryAxis) == this.ᜐ.Count)
							{
								num = 14;
								continue;
							}
							return;
						case 14:
							this.ᜏ.ChartType = this.SerieType;
							num = 5;
							continue;
						case 15:
							num = 10;
							continue;
						case 16:
							goto IL_269;
						case 17:
							num = 18;
							continue;
						case 18:
						{
							int count;
							if (count != 0)
							{
								num = 11;
								continue;
							}
							goto IL_B3;
						}
						case 19:
						{
							if (xlsChartFormat != null)
							{
								num = 6;
								continue;
							}
							bool flag = this.ᜐ.ᜅ(drawingZOrder) == 1;
							bool flag2 = Array.IndexOf<ExcelChartType>(XlsChart.ᜭ, A_0) != -1;
							int count = this.ᜏ.SecondaryFormats.Count;
							num = 4;
							continue;
						}
						}
						break;
						IL_B3:
						num = 1;
						continue;
						IL_14B:
						this.ᜀ(xlsChartFormat, A_0);
						num = 13;
						continue;
						IL_27C:
						num = 0;
						continue;
						IL_269:
						xlsChartFormat = new ChartFormat((spr\u2158)base.ReservedHandle, xlsChartFormatCollection);
						goto IL_27C;
					}
				}
				IL_11A:
				if (true)
				{
				}
				return;
				IL_2BB:
				throw new ArgumentException(RecordTableEnumerator.b("Łⱃ❅♇ⵉ⥋湍⍏㝑♓㽕㵗穙⡛❝ၟݡ䑣e१ͩk୭ᑯ山", a_));
				IL_317:
				IL_319:
				xlsChartFormat.ᜂ(A_0, false);
				xlsChartFormat.DrawingZOrder = this.ᜐ.FindOrderByType(A_0);
				xlsChartFormatCollection.Add(xlsChartFormat);
				this.ChartGroup = xlsChartFormat.DrawingZOrder;
				return;
			}
			}
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x000D3794 File Offset: 0x000D2794
		internal XlsChartFormat ᜀ(ExcelChartType A_0, bool A_1, bool A_2)
		{
			switch (0)
			{
			default:
			{
				XlsChartFormat result;
				for (;;)
				{
					string a = XlsChartFormat.ᜉ(A_0);
					List<int> list = new List<int>(6);
					result = null;
					int num = 0;
					int count = this.ᜐ.Count;
					int num2 = 12;
					for (;;)
					{
						int chartGroup;
						switch (num2)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_A5;
							default:
							{
								if (false)
								{
								}
								XlsChartSerie xlsChartSerie;
								result = xlsChartSerie.GetCommonSerieFormat();
								num2 = 5;
								continue;
							}
							}
							break;
						case 1:
							goto IL_7D;
						case 2:
							if (A_2)
							{
								goto IL_A5;
							}
							return result;
						case 3:
							goto IL_E4;
						case 4:
							return result;
						case 5:
							if (A_2)
							{
								num2 = 8;
								continue;
							}
							goto IL_93;
						case 6:
						{
							XlsChartSerie xlsChartSerie;
							if (A_1 != xlsChartSerie.UsePrimaryAxis)
							{
								num2 = 11;
								continue;
							}
							return result;
						}
						case 7:
						{
							XlsChartSerie xlsChartSerie;
							if (a == XlsChartFormat.ᜉ(xlsChartSerie.SerieType))
							{
								num2 = 0;
								continue;
							}
							goto IL_7D;
						}
						case 8:
							num2 = 6;
							continue;
						case 9:
							if (!list.Contains(chartGroup))
							{
								num2 = 10;
								continue;
							}
							goto IL_151;
						case 10:
							num2 = 7;
							continue;
						case 11:
							goto IL_93;
						case 12:
							goto IL_E4;
						case 13:
						{
							if (num >= count)
							{
								num2 = 4;
								continue;
							}
							XlsChartSerie xlsChartSerie = (XlsChartSerie)this.ᜐ[num];
							chartGroup = xlsChartSerie.ChartGroup;
							if (true)
							{
							}
							num2 = 9;
							continue;
						}
						case 14:
							goto IL_151;
						}
						break;
						IL_7D:
						list.Add(chartGroup);
						num2 = 14;
						continue;
						IL_93:
						num2 = 2;
						continue;
						IL_A5:
						num2 = 1;
						continue;
						IL_E4:
						num2 = 13;
						continue;
						IL_151:
						num++;
						num2 = 3;
					}
				}
				return result;
			}
			}
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x000D397C File Offset: 0x000D297C
		public void UpdateFormula(int currentIndex, int srcIndex, Rectangle srcRect, int destIndex, Rectangle destRect)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_121;
				case 1:
					if (this.ᜈ != null)
					{
						num = 4;
						continue;
					}
					return;
				case 2:
					goto IL_53;
				case 4:
				{
					sprᢀ a_ = this.ᜋ[sprᢀ.LinkIndex.LinkToBubbles];
					this.Bubbles = this.ᜀ(a_, currentIndex, srcIndex, srcRect, destIndex, destRect);
					num = 6;
					continue;
				}
				case 5:
				{
					sprᢀ a_ = this.ᜋ[sprᢀ.LinkIndex.LinkToCategories];
					this.CategoryLabels = this.ᜀ(a_, currentIndex, srcIndex, srcRect, destIndex, destRect);
					if (true)
					{
					}
					num = 2;
					continue;
				}
				case 6:
					return;
				case 7:
				{
					sprᢀ a_ = this.ᜋ[sprᢀ.LinkIndex.LinkToValues];
					this.Values = this.ᜀ(a_, currentIndex, srcIndex, srcRect, destIndex, destRect);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_121;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				}
				case 8:
					if (this.ᜇ != null)
					{
						num = 5;
						continue;
					}
					goto IL_53;
				}
				if (this.ᜆ != null)
				{
					num = 7;
					continue;
				}
				goto IL_121;
				IL_53:
				num = 1;
				continue;
				IL_121:
				num = 8;
			}
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x000D3AD0 File Offset: 0x000D2AD0
		private IXLSRange ᜀ(sprᢀ A_0, int A_1, int A_2, Rectangle A_3, int A_4, Rectangle A_5)
		{
			switch (0)
			{
			default:
			{
				int num = 0;
				List<Ptg> list;
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
						if (true)
						{
						}
						switch (num)
						{
						case 1:
						{
							int num2;
							int num3;
							if (num2 >= num3)
							{
								num = 4;
								continue;
							}
							Ptg[] array;
							Ptg a_ = array[num2];
							Ptg[] collection = this.ᜀ(a_, A_1, A_2, A_3, A_4, A_5);
							list.AddRange(collection);
							num2++;
							num = 2;
							continue;
						}
						case 2:
							goto IL_C8;
						case 3:
							goto IL_C8;
						case 4:
							goto IL_E4;
						case 5:
							goto IL_72;
						}
						goto IL_66;
						IL_C8:
						num = 1;
						continue;
						IL_66:
						if (A_0 != null)
						{
							Ptg[] array = A_0.ᜆ();
							list = new List<Ptg>();
							int num2 = 0;
							int num3 = array.Length;
							num = 3;
							continue;
						}
						break;
					}
					num = 5;
				}
				IL_72:
				return null;
				IL_E4:
				A_0.ᜀ(list.ToArray());
				return this.ᜀ(A_0);
			}
			}
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x000D3BD8 File Offset: 0x000D2BD8
		private Ptg[] ᜀ(Ptg A_0, int A_1, int A_2, Rectangle A_3, int A_4, Rectangle A_5)
		{
			int a_ = 1;
			switch (0)
			{
			default:
			{
				int num = 10;
				Rectangle rectangle;
				int num2;
				for (;;)
				{
					spr\u21F8 spr_u21F;
					sprẄ sprẄ;
					switch (num)
					{
					case 0:
						if (spr_u21F == null)
						{
							num = 12;
							continue;
						}
						rectangle = spr_u21F.ᜀ();
						num = 14;
						continue;
					case 1:
						goto IL_131;
					case 2:
						if (num2 != A_2)
						{
							num = 16;
							continue;
						}
						goto IL_22D;
					case 3:
						if (sprẄ == null)
						{
							num = 11;
							continue;
						}
						num2 = (int)sprẄ.ᜁ();
						num = 2;
						continue;
					case 4:
						if (UtilityMethods.ᜀ(A_3, rectangle))
						{
							num = 19;
							continue;
						}
						goto IL_2B2;
					case 5:
						num = 20;
						continue;
					case 6:
						goto IL_1F3;
					case 7:
						goto IL_1E2;
					case 8:
						goto IL_98;
					case 9:
						goto IL_204;
					case 11:
						goto IL_29D;
					case 12:
						goto IL_252;
					case 13:
						num = 4;
						continue;
					case 14:
						if (num2 == A_2)
						{
							num = 5;
							continue;
						}
						goto IL_2B2;
					case 15:
						if (num2 == A_4)
						{
							num = 23;
							continue;
						}
						goto IL_326;
					case 16:
						num = 6;
						continue;
					case 17:
						if (UtilityMethods.ᜀ(A_5, rectangle))
						{
							num = 22;
							continue;
						}
						goto IL_326;
					case 18:
						if (true)
						{
						}
						if (A_2 == A_4)
						{
							num = 13;
							continue;
						}
						goto IL_2B2;
					case 19:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1F3;
						default:
							goto IL_181;
						}
						break;
					case 20:
						if (A_3.Contains(rectangle))
						{
							num = 7;
							continue;
						}
						num = 18;
						continue;
					case 21:
						if (A_5.Contains(rectangle))
						{
							num = 1;
							continue;
						}
						num = 17;
						continue;
					case 22:
						goto IL_1BB;
					case 23:
						num = 21;
						continue;
					}
					if (A_0 == null)
					{
						num = 8;
						continue;
					}
					sprẄ = (A_0 as sprẄ);
					num = 3;
					continue;
					IL_1F3:
					if (num2 == A_4)
					{
						num = 9;
						continue;
					}
					IL_22D:
					spr_u21F = (A_0 as spr\u21F8);
					num = 0;
					continue;
					IL_2B2:
					num = 15;
				}
				IL_98:
				throw new ArgumentNullException(RecordTableEnumerator.b("䌶嘸债堼儾", a_));
				IL_131:
				return new Ptg[0];
				IL_181:
				if (false)
				{
				}
				Ptg ptg = this.ᜁ(A_0, A_2, rectangle, A_3, A_5);
				return new Ptg[]
				{
					ptg
				};
				IL_1BB:
				Ptg ptg2 = this.ᜀ(A_0, num2, rectangle, A_3, A_5);
				return new Ptg[]
				{
					ptg2
				};
				IL_1E2:
				bool flag;
				return new Ptg[]
				{
					A_0.Offset(A_1, -1, -1, A_2, A_3, A_4, A_5, out flag, this.\u170D)
				};
				IL_204:
				return new Ptg[]
				{
					A_0
				};
				IL_252:
				return new Ptg[]
				{
					A_0
				};
				IL_29D:
				return new Ptg[]
				{
					A_0
				};
				IL_326:
				return new Ptg[]
				{
					A_0
				};
			}
			}
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x000D3F1C File Offset: 0x000D2F1C
		private Ptg ᜁ(Ptg A_0, int A_1, Rectangle A_2, Rectangle A_3, Rectangle A_4)
		{
			switch (0)
			{
			default:
			{
				int num3;
				int num5;
				int num6;
				int num7;
				for (;;)
				{
					bool flag = UtilityMethods.ᜀ(A_3, A_2.Left, A_2.Top);
					bool flag2 = UtilityMethods.ᜀ(A_3, A_2.Right, A_2.Bottom);
					if (true)
					{
					}
					int num = 12;
					for (;;)
					{
						int num2;
						int num4;
						switch (num)
						{
						case 0:
							if (num2 == 0)
							{
								num = 8;
								continue;
							}
							goto IL_329;
						case 1:
							if (A_2.Height == 0)
							{
								goto IL_183;
							}
							goto IL_329;
						case 2:
							num = 19;
							continue;
						case 3:
							if (!flag2)
							{
								num = 9;
								continue;
							}
							goto IL_14B;
						case 4:
							if (flag)
							{
								num = 13;
								continue;
							}
							num3 = Math.Min(A_2.Right + num4, A_2.Left);
							num5 = Math.Max(A_2.Right + num4, A_3.Left - 1);
							num = 18;
							continue;
						case 5:
							num = 3;
							continue;
						case 6:
							if (flag)
							{
								num = 14;
								continue;
							}
							num6 = Math.Min(A_2.Bottom + num2, A_2.Top);
							num7 = Math.Max(A_2.Bottom + num2, A_3.Top - 1);
							num = 15;
							continue;
						case 7:
							num = 0;
							continue;
						case 8:
							num = 4;
							continue;
						case 9:
							return A_0;
						case 10:
							if (A_2.Width == 0)
							{
								num = 2;
								continue;
							}
							return A_0;
						case 11:
							goto IL_1D0;
						case 12:
							if (!flag)
							{
								num = 5;
								continue;
							}
							goto IL_14B;
						case 13:
							num3 = Math.Min(A_2.Left + num4, A_3.Right + 1);
							num5 = Math.Max(A_2.Left + num4, A_2.Right);
							num = 16;
							continue;
						case 14:
							num6 = Math.Min(A_2.Top + num2, A_3.Bottom + 1);
							num7 = Math.Max(A_2.Top + num2, A_2.Bottom);
							num = 11;
							continue;
						case 15:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_183;
							default:
								goto IL_31E;
							}
							break;
						case 16:
							goto IL_3D1;
						case 17:
							num = 6;
							continue;
						case 18:
							goto IL_2A4;
						case 19:
							if (num4 == 0)
							{
								num = 17;
								continue;
							}
							return A_0;
						}
						break;
						IL_14B:
						num4 = A_4.Left - A_3.Left;
						num2 = A_4.Top - A_3.Top;
						num = 1;
						continue;
						IL_183:
						num = 7;
						continue;
						IL_329:
						num = 10;
					}
				}
				IL_E1:
				return FormulaUtil.ᜀ(A_0.TokenCode, new object[]
				{
					A_1,
					A_2.Top,
					num3,
					A_2.Bottom,
					num5,
					0,
					0
				});
				IL_1D0:
				IL_1FE:
				return FormulaUtil.ᜀ(A_0.TokenCode, new object[]
				{
					A_1,
					num6,
					A_2.Left,
					num7,
					A_2.Right,
					0,
					0
				});
				IL_2A4:
				goto IL_E1;
				IL_31E:
				if (false)
				{
				}
				goto IL_1FE;
				IL_3D1:
				goto IL_E1;
			}
			}
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x000D4300 File Offset: 0x000D3300
		private Ptg ᜀ(Ptg A_0, int A_1, Rectangle A_2, Rectangle A_3, Rectangle A_4)
		{
			int a_ = 16;
			switch (0)
			{
			default:
			{
				int num = 7;
				int num2;
				int num3;
				int num4;
				int num5;
				for (;;)
				{
					bool flag;
					bool flag2;
					switch (num)
					{
					case 0:
						goto IL_12D;
					case 1:
						if (!flag)
						{
							num = 12;
							continue;
						}
						goto IL_1B5;
					case 2:
						num = 16;
						continue;
					case 3:
						if (!flag2)
						{
							num = 4;
							continue;
						}
						goto IL_1B5;
					case 4:
						goto IL_179;
					case 5:
						goto IL_80;
					case 6:
						goto IL_1B0;
					case 8:
						num2 = A_4.Right + 1;
						num = 0;
						continue;
					case 9:
						if (A_2.Left == A_2.Right)
						{
							num = 10;
							continue;
						}
						num = 11;
						continue;
					case 10:
						num = 15;
						continue;
					case 11:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_179;
						default:
							if (false)
							{
							}
							if (A_2.Top == A_2.Bottom)
							{
								num = 2;
								continue;
							}
							goto IL_268;
						}
						break;
					case 12:
						return A_0;
					case 13:
						goto IL_194;
					case 14:
						num3 = A_4.Bottom + 1;
						num = 17;
						continue;
					case 15:
						if (flag2)
						{
							if (true)
							{
							}
							num = 14;
							continue;
						}
						num4 = A_4.Top - 1;
						num = 13;
						continue;
					case 16:
						if (flag2)
						{
							num = 8;
							continue;
						}
						num5 = A_4.Left - 1;
						num = 6;
						continue;
					case 17:
						goto IL_EE;
					}
					if (A_0 == null)
					{
						num = 5;
						continue;
					}
					flag2 = UtilityMethods.ᜀ(A_4, A_2.Left, A_2.Top);
					flag = UtilityMethods.ᜀ(A_4, A_2.Right, A_2.Bottom);
					num = 3;
					continue;
					IL_179:
					num = 1;
					continue;
					IL_1B5:
					num3 = A_2.Top;
					num4 = A_2.Bottom;
					num2 = A_2.Left;
					num5 = A_2.Right;
					num = 9;
				}
				IL_80:
				throw new ArgumentNullException(RecordTableEnumerator.b("㉅❇ⅉ⥋⁍", a_));
				IL_EE:
				IL_12D:
				IL_194:
				IL_1B0:
				IL_268:
				return FormulaUtil.ᜀ(A_0.TokenCode, new object[]
				{
					A_1,
					num3,
					num2,
					num4,
					num5,
					0,
					0
				});
			}
			}
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x000D45D4 File Offset: 0x000D35D4
		private void ᜀ(spr\u237B A_0, ref spr\u237B A_1)
		{
			int a_ = 14;
			int num = 20;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 12;
					continue;
				case 1:
					if (A_0.ᜂ() == ErrorBarIncludeType.Minus)
					{
						num = 8;
						continue;
					}
					goto IL_1A9;
				case 2:
					goto IL_218;
				case 3:
					if (A_0.ᜅ() == ErrorBarType.Custom)
					{
						num = 0;
						continue;
					}
					A_1.ᜁ(ErrorBarIncludeType.Both);
					num = 13;
					continue;
				case 4:
					goto IL_122;
				case 5:
					if (A_1.ᜂ() == ErrorBarIncludeType.Minus)
					{
						num = 4;
						continue;
					}
					return;
				case 6:
					if (A_1.ᜂ() != ErrorBarIncludeType.Plus)
					{
						num = 14;
						continue;
					}
					goto IL_1A0;
				case 7:
					goto IL_83;
				case 8:
					num = 6;
					continue;
				case 9:
					goto IL_AD;
				case 10:
				{
					IXLSRange ixlsrange;
					A_1.ᜂ(ixlsrange);
					num = 16;
					continue;
				}
				case 11:
				{
					IXLSRange ixlsrange2;
					A_1.ᜁ(ixlsrange2);
					num = 9;
					continue;
				}
				case 12:
				{
					if (A_1.ᜅ() != ErrorBarType.Custom)
					{
						num = 2;
						continue;
					}
					IXLSRange ixlsrange = A_0.ᜉ();
					IXLSRange ixlsrange2 = A_0.ᜐ();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_218;
					default:
						if (false)
						{
						}
						num = 19;
						continue;
					}
					break;
				}
				case 13:
					goto IL_17F;
				case 14:
					goto IL_1A9;
				case 15:
					if (A_0.ᜂ() == ErrorBarIncludeType.Plus)
					{
						num = 22;
						continue;
					}
					return;
				case 16:
					goto IL_140;
				case 17:
				{
					IXLSRange ixlsrange;
					if (ixlsrange != null)
					{
						num = 10;
						continue;
					}
					goto IL_140;
				}
				case 18:
					if (A_1 == null)
					{
						num = 21;
						continue;
					}
					num = 3;
					continue;
				case 19:
				{
					IXLSRange ixlsrange2;
					if (ixlsrange2 != null)
					{
						num = 11;
						continue;
					}
					goto IL_AD;
				}
				case 21:
					goto IL_13E;
				case 22:
					num = 5;
					continue;
				}
				if (A_0 == null)
				{
					num = 7;
					continue;
				}
				num = 18;
				continue;
				IL_AD:
				num = 17;
				continue;
				IL_140:
				if (true)
				{
				}
				num = 1;
				continue;
				IL_1A9:
				num = 15;
			}
			IL_83:
			throw new ArgumentNullException(RecordTableEnumerator.b("♃❅㩇", a_));
			IL_122:
			goto IL_1A0;
			IL_13E:
			A_1 = A_0;
			return;
			IL_17F:
			return;
			IL_1A0:
			A_1.ᜁ(ErrorBarIncludeType.Both);
			return;
			IL_218:
			throw new ApplicationException(RecordTableEnumerator.b("݃❅♇⑉⍋㩍灏≑㕓⑕⭗㽙籛㭝቟ၡୣᑥ䡧ࡩ൫ᱭͯ山味㉵ᅷᱹ᩻᭽ꢇﺉﺍ몓", a_));
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x000D4860 File Offset: 0x000D3860
		public XlsChartFormat GetCommonSerieFormat()
		{
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_48;
			}
			if (false)
			{
			}
			if (!this.ᜏ.SecondaryFormats.ContainsIndex(this.ᜌ))
			{
				return this.ᜏ.PrimaryFormats[this.ᜌ];
			}
			IL_48:
			return this.ᜏ.SecondaryFormats[this.ᜌ];
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x000D48E4 File Offset: 0x000D38E4
		internal void ᜀ(bool[] A_0)
		{
			for (;;)
			{
				Dictionary<sprᢀ.LinkIndex, sprᢀ>.ValueCollection.Enumerator enumerator = this.ᜋ.Values.GetEnumerator();
				int num = 7;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_145;
					case 1:
						if (this.\u171E != null)
						{
							num = 2;
							continue;
						}
						return;
					case 2:
						this.\u171E.ᜀ(A_0);
						num = 5;
						continue;
					case 3:
						this.\u171C.ᜀ(A_0);
						num = 0;
						continue;
					case 4:
						if (this.\u171D != null)
						{
							num = 6;
							continue;
						}
						goto IL_67;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_67;
						default:
							goto IL_BB;
						}
						break;
					case 6:
						this.\u171D.ᜀ(A_0);
						num = 8;
						continue;
					case 7:
						try
						{
							num = 1;
							for (;;)
							{
								switch (num)
								{
								case 2:
									num = 4;
									continue;
								case 3:
								{
									if (!enumerator.MoveNext())
									{
										num = 2;
										continue;
									}
									sprᢀ sprᢀ = enumerator.Current;
									FormulaUtil.ᜀ(sprᢀ.ᜆ(), A_0);
									num = 0;
									continue;
								}
								case 4:
									goto IL_135;
								}
								IL_112:
								num = 3;
								continue;
								goto IL_112;
							}
							IL_135:
							goto IL_184;
						}
						finally
						{
							((IDisposable)enumerator).Dispose();
						}
						goto IL_145;
						IL_184:
						num = 4;
						continue;
					case 8:
						goto IL_67;
					case 9:
						if (this.\u171C != null)
						{
							num = 3;
							continue;
						}
						goto IL_145;
					}
					break;
					IL_67:
					num = 9;
					continue;
					IL_145:
					num = 1;
				}
			}
			IL_BB:
			if (true)
			{
			}
			if (false)
			{
			}
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x000D4AA8 File Offset: 0x000D3AA8
		internal void ᜀ(int[] A_0)
		{
			for (;;)
			{
				Dictionary<sprᢀ.LinkIndex, sprᢀ>.ValueCollection.Enumerator enumerator = this.ᜋ.Values.GetEnumerator();
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_162;
					case 1:
						this.\u171D.ᜀ(A_0);
						num = 9;
						continue;
					case 2:
						if (this.\u171E != null)
						{
							num = 4;
							continue;
						}
						return;
					case 3:
						try
						{
							num = 3;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_152;
								case 2:
								{
									if (!enumerator.MoveNext())
									{
										num = 6;
										continue;
									}
									sprᢀ sprᢀ = enumerator.Current;
									Ptg[] a_ = sprᢀ.ᜆ();
									num = 5;
									continue;
								}
								case 4:
								{
									sprᢀ sprᢀ;
									Ptg[] a_;
									sprᢀ.ᜀ(a_);
									num = 1;
									continue;
								}
								case 5:
								{
									Ptg[] a_;
									if (FormulaUtil.ᜀ(a_, A_0))
									{
										num = 4;
										continue;
									}
									break;
								}
								case 6:
									num = 0;
									continue;
								}
								IL_11B:
								num = 2;
								continue;
								goto IL_11B;
							}
							IL_152:
							goto IL_1A1;
						}
						finally
						{
							((IDisposable)enumerator).Dispose();
						}
						goto IL_162;
						IL_1A1:
						if (true)
						{
						}
						num = 7;
						continue;
					case 4:
						this.\u171E.ᜀ(A_0);
						num = 5;
						continue;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_64;
						default:
							goto IL_B8;
						}
						break;
					case 6:
						this.\u171C.ᜀ(A_0);
						num = 0;
						continue;
					case 7:
						if (this.\u171D != null)
						{
							num = 1;
							continue;
						}
						goto IL_64;
					case 8:
						if (this.\u171C != null)
						{
							num = 6;
							continue;
						}
						goto IL_162;
					case 9:
						goto IL_64;
					}
					break;
					IL_64:
					num = 8;
					continue;
					IL_162:
					num = 2;
				}
			}
			IL_B8:
			if (false)
			{
			}
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x000D4C94 File Offset: 0x000D3C94
		public void SerializeDataToList(RecordArrayList records)
		{
			int a_ = 2;
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.\u1714.Count != 0)
					{
						num = 6;
						continue;
					}
					goto IL_247;
				case 1:
					if (this.\u1716.Count != 0)
					{
						goto IL_A5;
					}
					goto IL_6D;
				case 2:
					if (this.HasErrorBarsX)
					{
						num = 3;
						continue;
					}
					goto IL_272;
				case 3:
					if (true)
					{
					}
					this.\u171D.ᜀ(this.ᜐ.TrendErrorList);
					num = 14;
					continue;
				case 4:
					goto IL_148;
				case 5:
					goto IL_68;
				case 6:
					this.ᜀ(this.\u1714);
					num = 10;
					continue;
				case 7:
					this.ᜀ(this.\u1716);
					num = 15;
					continue;
				case 8:
					goto IL_8D;
				case 10:
					goto IL_247;
				case 11:
					this.ᜀ(this.\u1715);
					num = 8;
					continue;
				case 12:
					if (this.\u1715.Count != 0)
					{
						num = 11;
						continue;
					}
					goto IL_8D;
				case 13:
					if (this.HasErrorBarsY)
					{
						num = 16;
						continue;
					}
					goto IL_148;
				case 14:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A5;
					default:
						goto IL_1E1;
					}
					break;
				case 15:
					goto IL_6D;
				case 16:
					this.\u171C.ᜀ(this.ᜐ.TrendErrorList);
					num = 4;
					continue;
				}
				if (records == null)
				{
					num = 5;
					continue;
				}
				this.ᜂ(records);
				records.ᜀ(spr\u175E.ᜀ(TBIFFRecord.Begin));
				this.ᜃ(records);
				this.m_dataPoints.ᜀ(records);
				sprὈ sprὈ = (sprὈ)spr\u175E.ᜀ(TBIFFRecord.ChartSertocrt);
				sprὈ.ᜀ((ushort)this.ChartGroup);
				records.ᜀ(sprὈ);
				this.ᜁ(records);
				records.ᜀ(spr\u175E.ᜀ(TBIFFRecord.End));
				num = 0;
				continue;
				IL_6D:
				num = 13;
				continue;
				IL_8D:
				num = 1;
				continue;
				IL_A5:
				num = 7;
				continue;
				IL_148:
				num = 2;
				continue;
				IL_247:
				num = 12;
			}
			IL_68:
			throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹弻儽㈿♁㝃", a_));
			IL_1E1:
			if (false)
			{
			}
			IL_272:
			this.\u171E.ᜀ(this.ᜐ.TrendErrorList);
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x000D4F2C File Offset: 0x000D3F2C
		private void ᜃ(RecordArrayList A_0)
		{
			int a_ = 3;
			int num = 13;
			sprᢀ sprᢀ;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.\u1714.Count != 0)
					{
						num = 6;
						continue;
					}
					goto IL_16B;
				case 1:
					goto IL_E6;
				case 2:
					goto IL_1E4;
				case 3:
					if (this.ᜆ != null)
					{
						num = 10;
						continue;
					}
					goto IL_1E6;
				case 4:
					if (this.\u1716.Count != 0)
					{
						num = 9;
						continue;
					}
					goto IL_225;
				case 5:
					sprᢀ.ᜀ(null);
					sprᢀ.ᜀ(sprᢀ.ReferenceType.EnteredDirectly);
					num = 1;
					continue;
				case 6:
					sprᢀ.ᜀ(null);
					sprᢀ.ᜀ(sprᢀ.ReferenceType.EnteredDirectly);
					num = 11;
					continue;
				case 7:
					goto IL_1E6;
				case 8:
					goto IL_5C;
				case 9:
					if (true)
					{
					}
					sprᢀ.ᜀ(null);
					sprᢀ.ᜀ(sprᢀ.ReferenceType.EnteredDirectly);
					num = 2;
					continue;
				case 10:
					sprᢀ.ᜁ(2);
					num = 7;
					continue;
				case 11:
					goto IL_16B;
				case 12:
					if (this.\u1715.Count != 0)
					{
						goto IL_1B3;
					}
					goto IL_E6;
				}
				if (A_0 == null)
				{
					num = 8;
					continue;
				}
				this.ᜀ(A_0);
				sprᢀ = this.ᜋ[sprᢀ.LinkIndex.LinkToValues];
				this.ᜀ(sprᢀ, this.ᜆ, sprᢀ.ReferenceType.EnteredDirectly);
				num = 3;
				continue;
				IL_E6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_1B3:
					num = 5;
					continue;
				default:
					if (false)
					{
					}
					A_0.ᜀ((BiffRecordRaw)sprᢀ.Clone());
					sprᢀ = this.ᜋ[sprᢀ.LinkIndex.LinkToBubbles];
					this.ᜀ(sprᢀ, this.ᜈ, sprᢀ.ReferenceType.EnteredDirectly);
					num = 4;
					continue;
				}
				IL_16B:
				A_0.ᜀ((BiffRecordRaw)sprᢀ.Clone());
				sprᢀ = this.ᜋ[sprᢀ.LinkIndex.LinkToCategories];
				this.ᜀ(sprᢀ, this.ᜇ, sprᢀ.ReferenceType.DefaultCategories);
				num = 12;
				continue;
				IL_1E6:
				num = 0;
			}
			IL_5C:
			throw new ArgumentNullException(RecordTableEnumerator.b("䬸帺帼倾㍀❂㙄", a_));
			IL_1E4:
			IL_225:
			A_0.ᜀ((BiffRecordRaw)sprᢀ.Clone());
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x000D5170 File Offset: 0x000D4170
		private void ᜀ(sprᢀ A_0, IXLSRange A_1, sprᢀ.ReferenceType A_2)
		{
			int a_ = 19;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_DD;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4C;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						if (A_1.GetType() == typeof(XlsRange))
						{
							num = 4;
							continue;
						}
						goto IL_FD;
					}
					break;
				case 2:
					if (A_1 == null)
					{
						num = 5;
						continue;
					}
					A_0.ᜀ(sprᢀ.ReferenceType.Worksheet);
					goto IL_4C;
				case 4:
					((XlsRange)A_1).ᜀ(this.\u170D);
					num = 0;
					continue;
				case 5:
					goto IL_FB;
				case 6:
					goto IL_43;
				}
				if (A_0 == null)
				{
					num = 6;
					continue;
				}
				num = 2;
				continue;
				IL_4C:
				num = 1;
			}
			IL_43:
			throw new ArgumentNullException(RecordTableEnumerator.b("㭈⹊⹌⁎⍐㝒", a_));
			IL_DD:
			goto IL_FD;
			IL_FB:
			A_0.ᜀ(A_2);
			A_0.ᜀ(null);
			return;
			IL_FD:
			A_0.ᜀ(((spr\u1A8B)A_1).ᜀ());
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x000D528C File Offset: 0x000D428C
		private void ᜂ(RecordArrayList A_0)
		{
			int a_ = 2;
			int num = 10;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜎ.ᜁ((this.ᜈ != null) ? ((ushort)this.ᜈ.Count) : 0);
					num = 2;
					continue;
				case 1:
					if (this.\u1715.Count == 0)
					{
						num = 9;
						continue;
					}
					this.ᜎ.ᜂ((ushort)this.\u1715.Count);
					num = 19;
					continue;
				case 2:
					goto IL_2AB;
				case 3:
					goto IL_24A;
				case 4:
					this.ᜎ.ᜁ(sprḠ.DataType.Text);
					num = 17;
					continue;
				case 5:
					goto IL_182;
				case 6:
					goto IL_32D;
				case 7:
					goto IL_24A;
				case 8:
					this.ᜎ.ᜂ(sprḠ.DataType.Text);
					num = 3;
					continue;
				case 9:
					num = 18;
					continue;
				case 11:
					if (this.\u1716[0] is spr\u2170)
					{
						num = 12;
						continue;
					}
					goto IL_32F;
				case 12:
					this.ᜎ.ᜀ(sprḠ.DataType.Text);
					num = 6;
					continue;
				case 13:
					this.ᜎ.ᜀ((this.ᜆ != null) ? ((ushort)this.ᜆ.Count) : 0);
					num = 5;
					continue;
				case 14:
					if (this.\u1716.Count == 0)
					{
						num = 20;
						continue;
					}
					if (true)
					{
					}
					this.ᜎ.ᜁ((ushort)this.\u1716.Count);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2AB;
					default:
						if (false)
						{
						}
						num = 11;
						continue;
					}
					break;
				case 15:
					goto IL_80;
				case 16:
					num = 13;
					continue;
				case 17:
					goto IL_182;
				case 18:
					this.ᜎ.ᜂ((this.ᜇ != null) ? ((ushort)this.ᜇ.Count) : this.ᜎ.ᜀ());
					num = 7;
					continue;
				case 19:
					if (this.\u1715[0] is spr\u2170)
					{
						num = 8;
						continue;
					}
					goto IL_24A;
				case 20:
					num = 0;
					continue;
				case 21:
					if (this.\u1714[0] is spr\u2170)
					{
						num = 4;
						continue;
					}
					goto IL_182;
				case 22:
					if (this.\u1714.Count == 0)
					{
						num = 16;
						continue;
					}
					this.ᜎ.ᜀ((ushort)this.\u1714.Count);
					num = 21;
					continue;
				}
				if (A_0 == null)
				{
					num = 15;
					continue;
				}
				this.ᜎ.ᜂ(sprḠ.DataType.Numeric);
				this.ᜎ.ᜁ(sprḠ.DataType.Numeric);
				this.ᜎ.ᜀ(sprḠ.DataType.Numeric);
				num = 22;
				continue;
				IL_182:
				num = 1;
				continue;
				IL_24A:
				num = 14;
			}
			IL_80:
			throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹弻儽㈿♁㝃", a_));
			IL_2AB:
			IL_32D:
			IL_32F:
			this.ᜈ();
			A_0.ᜀ((BiffRecordRaw)this.ᜎ.Clone());
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x000D55E8 File Offset: 0x000D45E8
		internal void ᜈ()
		{
			int a_ = 15;
			int num = 7;
			for (;;)
			{
				int num2;
				ExcelVersion version;
				int num3;
				switch (num)
				{
				case 0:
					if (num2 > 32000)
					{
						num = 3;
						continue;
					}
					return;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_144;
					default:
						if (false)
						{
						}
						if (version != ExcelVersion.Version97to2003)
						{
							num = 13;
							continue;
						}
						goto IL_86;
					}
					break;
				case 2:
					goto IL_144;
				case 3:
					goto IL_12D;
				case 4:
					num3 = (int)((ushort)this.ᜇ.Count);
					goto IL_1E1;
				case 5:
					if (this.ᜏ.IsChart3D)
					{
						num = 10;
						continue;
					}
					goto IL_10C;
				case 6:
					goto IL_AB;
				case 8:
					if (version == ExcelVersion.Version2007)
					{
						num = 12;
						continue;
					}
					return;
				case 9:
					goto IL_10C;
				case 10:
					num = 16;
					continue;
				case 11:
					goto IL_AB;
				case 12:
					goto IL_86;
				case 13:
					num = 8;
					continue;
				case 14:
					num = 15;
					continue;
				case 15:
					num3 = (int)this.ᜎ.ᜀ();
					goto IL_1E1;
				case 16:
					if (num2 <= 4000)
					{
						num = 9;
						continue;
					}
					goto IL_179;
				case 17:
					num = 2;
					continue;
				}
				if (true)
				{
				}
				if (this.\u1715.Count == 0)
				{
					num = 17;
					continue;
				}
				num2 = this.\u1715.Count;
				num = 11;
				continue;
				IL_86:
				num = 5;
				continue;
				IL_AB:
				version = this.\u170D.Version;
				num = 1;
				continue;
				IL_10C:
				num = 0;
				continue;
				IL_144:
				if (this.ᜇ == null)
				{
					num = 14;
					continue;
				}
				num = 4;
				continue;
				IL_1E1:
				num2 = num3;
				num = 6;
			}
			IL_12D:
			IL_179:
			throw new ApplicationException(RecordTableEnumerator.b("ᅄ⽆ⱈ歊⁌⹎⥐㩒㡔≖㑘筚㍜⩞ౠŢdᕦ䥨Ѫ୬佮ᕰቲŴᙶ奸୺ቼᙾꞆ꾎떔ﺚ列뾞좠춢薤욦覨쾪첬\udbae킰鎲운튶쮸튺\ud8bc첾ꗂ꫄뗆꫊﷎闐뛔뿖룘꧚꧜￞裠郢엤퓦\udbe8\udbea\uddec\udfee\uddf0폲鏴飶诸\udbfa鳼\udffe㈀䜂┄搆愈樊缌笎ㄐ稒昔㜖ⴘ⬚ⴜ⼞༠眢䨤ܦ尨堪䠬༮尰尲䜴制ᤸ强尼䬾⁀捂㕄⡆⁈╊㥌㱎結獒ⱔ㡖ⱘ筚㍜㩞Ѡݢ䕤ѦŨ੪ͬ࡮ᑰ卲ၴྲྀ᩸Ṻᅼ彾꾎ﲒ떔솖ﲘ캠춢薤閦馨骪鶬膮", a_));
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x000D57E8 File Offset: 0x000D47E8
		[CLSCompliant(false)]
		internal void ᜄ(RecordArrayList A_0)
		{
			int a_ = 6;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					this.m_dataPoints.ᜁ(A_0);
					num = 3;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8E;
					default:
						if (false)
						{
						}
						if (this.m_dataPoints != null)
						{
							num = 1;
							continue;
						}
						return;
					}
					break;
				case 3:
					goto IL_56;
				case 4:
					goto IL_38;
				}
				if (A_0 == null)
				{
					num = 4;
				}
				else
				{
					num = 2;
				}
			}
			IL_38:
			if (true)
			{
			}
			goto IL_8E;
			IL_56:
			return;
			IL_8E:
			throw new ArgumentNullException(RecordTableEnumerator.b("主嬽⌿ⵁ㙃≅㭇", a_));
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x000D58A4 File Offset: 0x000D48A4
		private void ᜁ(RecordArrayList A_0)
		{
			int a_ = 15;
			switch (0)
			{
			default:
			{
				int num = 0;
				ChartLegendEntriesColl chartLegendEntriesColl;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 1:
					{
						int count;
						if (num2 >= count)
						{
							num = 11;
							continue;
						}
						num = 3;
						continue;
					}
					case 2:
						goto IL_C4;
					case 3:
						if (chartLegendEntriesColl.Contains(num2))
						{
							num = 7;
							continue;
						}
						goto IL_C4;
					case 4:
					{
						num2 = 0;
						int count = chartLegendEntriesColl.Count;
						num = 10;
						continue;
					}
					case 5:
						if (chartLegendEntriesColl.Contains(this.Index))
						{
							num = 14;
							continue;
						}
						return;
					case 6:
					{
						if (!this.ᜏ.HasLegend)
						{
							num = 8;
							continue;
						}
						string value = XlsChartFormat.ᜉ(this.ᜏ.ChartType);
						chartLegendEntriesColl = (ChartLegendEntriesColl)this.ᜏ.Legend.LegendEntries;
						num = 13;
						continue;
					}
					case 7:
					{
						XlsChartLegendEntry xlsChartLegendEntry = (XlsChartLegendEntry)chartLegendEntriesColl[num2];
						xlsChartLegendEntry.ᜀ(A_0);
						num = 2;
						continue;
					}
					case 8:
						return;
					case 9:
						goto IL_79;
					case 10:
						goto IL_118;
					case 11:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_AA;
						}
						goto Block_6;
					case 12:
						goto IL_118;
					case 13:
					{
						string value;
						if (Array.IndexOf<string>(XlsChart.ᜥ, value) == -1)
						{
							num = 16;
							continue;
						}
						num = 15;
						continue;
					}
					case 14:
						goto IL_194;
					case 15:
						goto IL_AA;
					case 16:
						num = 5;
						continue;
					}
					if (A_0 == null)
					{
						num = 9;
						continue;
					}
					num = 6;
					continue;
					IL_AA:
					if (this.Index == 0)
					{
						num = 4;
						continue;
					}
					return;
					IL_C4:
					num2++;
					num = 12;
					continue;
					IL_118:
					num = 1;
				}
				IL_79:
				throw new ArgumentNullException(RecordTableEnumerator.b("㝄≆⩈⑊㽌⭎≐", a_));
				Block_6:
				if (false)
				{
				}
				return;
				IL_194:
				XlsChartLegendEntry xlsChartLegendEntry2 = (XlsChartLegendEntry)chartLegendEntriesColl[this.Index];
				xlsChartLegendEntry2.ᜀ(A_0);
				return;
			}
			}
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x000D5AF4 File Offset: 0x000D4AF4
		private void ᜀ(RecordArrayList A_0)
		{
			int a_ = 8;
			int num = 6;
			for (;;)
			{
				string name;
				sprᢀ sprᢀ;
				switch (num)
				{
				case 0:
				{
					spr\u1D35 spr_u1D;
					spr_u1D.ᜀ((name == null) ? "" : name);
					A_0.ᜀ(spr_u1D);
					num = 16;
					continue;
				}
				case 1:
					if (this.\u171A != null)
					{
						num = 11;
						continue;
					}
					goto IL_1FE;
				case 2:
					goto IL_1E7;
				case 3:
					if (this.ᜉ != null)
					{
						num = 4;
						continue;
					}
					goto IL_21C;
				case 4:
					num = 13;
					continue;
				case 5:
					if (this.ᜉ[0] != '=')
					{
						num = 14;
						continue;
					}
					this.ᜀ();
					sprᢀ.ᜀ(((spr\u1A8B)this.\u171A).ᜀ());
					sprᢀ.ᜀ(sprᢀ.ReferenceType.Worksheet);
					num = 8;
					continue;
				case 7:
					if (true)
					{
					}
					goto IL_21C;
				case 8:
					goto IL_15C;
				case 9:
					num = 2;
					continue;
				case 10:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_21C;
					default:
					{
						if (false)
						{
						}
						spr\u1D35 spr_u1D = (spr\u1D35)spr\u175E.ᜀ(TBIFFRecord.ChartSeriesText);
						num = 0;
						continue;
					}
					}
					break;
				case 11:
					goto IL_1B1;
				case 12:
					if (!this.\u1712)
					{
						num = 10;
						continue;
					}
					return;
				case 13:
					if (this.ᜉ.Length == 0)
					{
						num = 7;
						continue;
					}
					goto IL_1B1;
				case 14:
					goto IL_1FE;
				case 15:
					goto IL_15C;
				case 16:
					goto IL_1AC;
				}
				if (A_0 == null)
				{
					num = 9;
					continue;
				}
				name = this.Name;
				sprᢀ = this.ᜋ[sprᢀ.LinkIndex.LinkToTitleOrText];
				num = 3;
				continue;
				IL_15C:
				A_0.ᜀ((BiffRecordRaw)sprᢀ.Clone());
				num = 12;
				continue;
				IL_1B1:
				num = 5;
				continue;
				IL_1FE:
				sprᢀ.ᜀ(null);
				sprᢀ.ᜀ(sprᢀ.ReferenceType.EnteredDirectly);
				num = 15;
				continue;
				IL_21C:
				num = 1;
			}
			IL_1AC:
			return;
			IL_1E7:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰽┿⅁⭃㑅ⱇ㥉", a_));
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x000D5D40 File Offset: 0x000D4D40
		private Ptg[] ᜀ()
		{
			int num = 0;
			Ptg[] result;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return result;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				case 2:
					num = 4;
					continue;
				case 3:
					if (this.ᜉ[0] == '=')
					{
						num = 6;
						continue;
					}
					goto IL_E3;
				case 4:
					if (this.ᜉ != null)
					{
						num = 1;
						continue;
					}
					goto IL_E3;
				case 5:
					return result;
				case 6:
				{
					string a_ = UtilityMethods.ᜀ(this.ᜉ);
					result = this.\u170D.FormulaUtil.ᜃ(a_);
					num = 7;
					continue;
				}
				case 7:
					return result;
				}
				if (this.\u171A == null)
				{
					num = 2;
					continue;
				}
				IL_E3:
				result = ((spr\u1A8B)this.\u171A).ᜀ();
				num = 5;
			}
			return result;
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06001620 RID: 5664 RVA: 0x000D5E50 File Offset: 0x000D4E50
		// (remove) Token: 0x06001621 RID: 5665 RVA: 0x000D5EE8 File Offset: 0x000D4EE8
		public event XlsEventHandler NameChanged
		{
			add
			{
				for (;;)
				{
					if (true)
					{
					}
					XlsEventHandler xlsEventHandler = this.ᜢ;
					int num = 1;
					for (;;)
					{
						XlsEventHandler xlsEventHandler2;
						switch (num)
						{
						case 0:
							return;
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
								goto IL_49;
							}
							break;
						case 2:
							if (xlsEventHandler == xlsEventHandler2)
							{
								num = 0;
								continue;
							}
							goto IL_49;
						}
						break;
						IL_49:
						xlsEventHandler2 = xlsEventHandler;
						XlsEventHandler value2 = (XlsEventHandler)Delegate.Combine(xlsEventHandler2, value);
						xlsEventHandler = Interlocked.CompareExchange<XlsEventHandler>(ref this.ᜢ, value2, xlsEventHandler2);
						num = 2;
					}
				}
			}
			remove
			{
				for (;;)
				{
					XlsEventHandler xlsEventHandler = this.ᜢ;
					int num = 0;
					for (;;)
					{
						if (true)
						{
						}
						XlsEventHandler xlsEventHandler2;
						switch (num)
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
								goto IL_49;
							}
							break;
						case 1:
							if (xlsEventHandler == xlsEventHandler2)
							{
								num = 2;
								continue;
							}
							goto IL_49;
						case 2:
							return;
						}
						break;
						IL_49:
						xlsEventHandler2 = xlsEventHandler;
						XlsEventHandler value2 = (XlsEventHandler)Delegate.Remove(xlsEventHandler2, value);
						xlsEventHandler = Interlocked.CompareExchange<XlsEventHandler>(ref this.ᜢ, value2, xlsEventHandler2);
						num = 1;
					}
				}
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06001622 RID: 5666 RVA: 0x000D5F80 File Offset: 0x000D4F80
		// (set) Token: 0x06001623 RID: 5667 RVA: 0x000D5FC4 File Offset: 0x000D4FC4
		public int Index
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
				return this.ᜑ;
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
				this.ᜑ = value;
				this.m_dataPoints.UpdateSerieIndex();
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06001624 RID: 5668 RVA: 0x000D6014 File Offset: 0x000D5014
		// (set) Token: 0x06001625 RID: 5669 RVA: 0x000D6058 File Offset: 0x000D5058
		public int Number
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
				return this.Index;
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
				this.Index = value;
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06001626 RID: 5670 RVA: 0x000D609C File Offset: 0x000D509C
		// (set) Token: 0x06001627 RID: 5671 RVA: 0x000D60E0 File Offset: 0x000D50E0
		public int ChartGroup
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
				return this.ᜌ;
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
				this.ᜌ = value;
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06001628 RID: 5672 RVA: 0x000D6124 File Offset: 0x000D5124
		protected internal XlsChart InnerXlsChart
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
				return this.ᜏ;
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06001629 RID: 5673 RVA: 0x000D6168 File Offset: 0x000D5168
		// (set) Token: 0x0600162A RID: 5674 RVA: 0x000D61AC File Offset: 0x000D51AC
		public bool IsDefaultName
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
				return this.\u1712;
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
				this.\u1712 = value;
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x0600162B RID: 5675 RVA: 0x000D61F0 File Offset: 0x000D51F0
		public int PointNumber
		{
			get
			{
				int a_ = 17;
				for (;;)
				{
					IL_3D:
					string b = XlsChartFormat.ᜉ(this.ᜏ.ChartType);
					int num = 2;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return 65532;
						default:
							if (false)
							{
							}
							switch (num)
							{
							case 0:
								if (this.ᜆ == null)
								{
									num = 3;
									continue;
								}
								goto IL_A5;
							case 1:
								return 65532;
							case 2:
								if (RecordTableEnumerator.b("ᑆ㱈㥊⭌⹎㉐㙒", a_) == b)
								{
									num = 1;
									continue;
								}
								num = 0;
								continue;
							case 3:
								return 0;
							}
							goto IL_3D;
						}
					}
				}
				return 65532;
				IL_A5:
				if (true)
				{
				}
				return ((ICombinedRange)this.ᜆ).CellsCount;
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x0600162C RID: 5676 RVA: 0x000D62BC File Offset: 0x000D52BC
		protected internal XlsWorkbook InnerWorkbook
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
				return this.\u170D;
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x0600162D RID: 5677 RVA: 0x000D6300 File Offset: 0x000D5300
		public string StartType
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
				return XlsChartFormat.ᜉ(this.SerieType);
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x0600162E RID: 5678 RVA: 0x000D6348 File Offset: 0x000D5348
		public string ParseSerieNotDefaultText
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
				return this.\u171B;
			}
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x0600162F RID: 5679 RVA: 0x000D638C File Offset: 0x000D538C
		public XlsChartSeries ParentSeries
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
				return this.ᜐ;
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06001630 RID: 5680 RVA: 0x000D63D0 File Offset: 0x000D53D0
		public XlsChart ParentChart
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
				return this.ᜏ;
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06001631 RID: 5681 RVA: 0x000D6414 File Offset: 0x000D5414
		internal XlsWorkbook ParentBook
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
				return this.\u170D;
			}
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06001632 RID: 5682 RVA: 0x000D6458 File Offset: 0x000D5458
		internal bool IsPie
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
				return XlsChart.GetIsChartPie(this.SerieType);
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06001633 RID: 5683 RVA: 0x000D64A0 File Offset: 0x000D54A0
		public string NameOrFormula
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜉ[0] == '=')
						{
							num = 5;
							continue;
						}
						goto IL_D8;
					case 2:
						goto IL_D6;
					case 3:
						goto IL_60;
					case 4:
						if (this.ᜊ == null)
						{
							num = 3;
							continue;
						}
						goto IL_E6;
					case 5:
						num = 4;
						continue;
					case 6:
						num = 7;
						continue;
					case 7:
						if (true)
						{
						}
						if (this.ᜉ.Length > 0)
						{
							num = 0;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_E6;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					}
					if (this.ᜉ == null)
					{
						goto IL_DF;
					}
					num = 6;
				}
				IL_60:
				goto IL_D8;
				IL_D6:
				goto IL_DF;
				IL_D8:
				return this.ᜉ;
				IL_DF:
				return this.ᜉ;
				IL_E6:
				return '=' + this.\u170D.FormulaUtil.ᜁ(this.ᜊ);
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06001634 RID: 5684 RVA: 0x000D65B8 File Offset: 0x000D55B8
		// (set) Token: 0x06001635 RID: 5685 RVA: 0x000D65FC File Offset: 0x000D55FC
		public bool? InvertNegaColor
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
				return this.\u171F;
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
				this.\u171F = value;
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06001636 RID: 5686 RVA: 0x000D6640 File Offset: 0x000D5640
		// (set) Token: 0x06001637 RID: 5687 RVA: 0x000D6684 File Offset: 0x000D5684
		internal string StrRefFormula
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
				return this.ᜠ;
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
				this.ᜠ = value;
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x000D66C8 File Offset: 0x000D56C8
		// (set) Token: 0x06001639 RID: 5689 RVA: 0x000D670C File Offset: 0x000D570C
		internal string NumRefFormula
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
				return this.ᜡ;
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
				this.ᜡ = value;
			}
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x000D6750 File Offset: 0x000D5750
		public void Reparse()
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
			sprᢀ a_ = this.ᜋ[sprᢀ.LinkIndex.LinkToValues];
			this.ᜆ = this.ᜀ(a_);
			a_ = this.ᜋ[sprᢀ.LinkIndex.LinkToBubbles];
			this.ᜈ = this.ᜀ(a_);
			a_ = this.ᜋ[sprᢀ.LinkIndex.LinkToCategories];
			this.ᜇ = this.ᜀ(a_);
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x000D67DC File Offset: 0x000D57DC
		internal IXLSRange ᜀ(sprᢀ A_0)
		{
			int a_ = 13;
			switch (0)
			{
			default:
			{
				int num = 20;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
					{
						IXLSRanges ixlsranges;
						if (ixlsranges != null)
						{
							num = 22;
							continue;
						}
						goto IL_BB;
					}
					case 1:
						num = 16;
						continue;
					case 2:
					{
						IXLSRange ixlsrange;
						IWorksheet worksheet = ixlsrange.Worksheet;
						num = 25;
						continue;
					}
					case 3:
						goto IL_2D9;
					case 4:
					{
						IXLSRange ixlsrange;
						IXLSRanges ixlsranges = ((XlsWorksheet)ixlsrange.Worksheet).ᜮ();
						num = 21;
						continue;
					}
					case 5:
						goto IL_1E1;
					case 6:
					{
						IXLSRanges ixlsranges;
						if (ixlsranges != null)
						{
							num = 9;
							continue;
						}
						goto IL_208;
					}
					case 7:
						if (A_0.ᜆ() != null)
						{
							num = 1;
							continue;
						}
						goto IL_107;
					case 8:
						if (A_0.ᜁ() != sprᢀ.ReferenceType.Worksheet)
						{
							num = 11;
							continue;
						}
						goto IL_26A;
					case 9:
						num = 26;
						continue;
					case 10:
					{
						IXLSRange ixlsrange;
						if (ixlsrange != null)
						{
							num = 18;
							continue;
						}
						goto IL_BB;
					}
					case 11:
						num = 28;
						continue;
					case 12:
						num = 6;
						continue;
					case 13:
					{
						Ptg ptg;
						if (ptg is sprỜ)
						{
							num = 15;
							continue;
						}
						goto IL_BB;
					}
					case 14:
					{
						int num3;
						if (num2 >= num3)
						{
							num = 12;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1CA;
						default:
						{
							if (false)
							{
							}
							Ptg ptg = A_0.ᜆ()[num2];
							num = 13;
							continue;
						}
						}
						break;
					}
					case 15:
					{
						if (true)
						{
						}
						Ptg ptg;
						IXLSRange ixlsrange = this.ᜀ(ptg);
						num = 10;
						continue;
					}
					case 16:
					{
						if (A_0.ᜆ().Length <= 1)
						{
							num = 24;
							continue;
						}
						IXLSRanges ixlsranges = null;
						num2 = 0;
						int num3 = A_0.ᜆ().Length;
						num = 23;
						continue;
					}
					case 17:
					{
						IXLSRanges ixlsranges;
						if (ixlsranges == null)
						{
							num = 2;
							continue;
						}
						goto IL_2FD;
					}
					case 18:
						num = 17;
						continue;
					case 19:
					{
						IXLSRanges ixlsranges;
						return ixlsranges;
					}
					case 21:
						goto IL_2FD;
					case 22:
					{
						IXLSRanges ixlsranges;
						IXLSRange ixlsrange;
						((XlsRangesCollection)ixlsranges).Add(ixlsrange);
						num = 29;
						continue;
					}
					case 23:
						goto IL_2D9;
					case 24:
						goto IL_107;
					case 25:
					{
						IWorksheet worksheet;
						if (worksheet != null)
						{
							num = 4;
							continue;
						}
						goto IL_2FD;
					}
					case 26:
					{
						IXLSRanges ixlsranges;
						if (ixlsranges.Count != 0)
						{
							num = 19;
							continue;
						}
						goto IL_208;
					}
					case 27:
						goto IL_B6;
					case 28:
						goto IL_1CA;
					case 29:
						goto IL_BB;
					}
					if (A_0 == null)
					{
						num = 27;
						continue;
					}
					num = 7;
					continue;
					IL_BB:
					num2++;
					num = 3;
					continue;
					IL_107:
					num = 8;
					continue;
					IL_1CA:
					if (A_0.ᜆ() != null)
					{
						num = 5;
						continue;
					}
					goto IL_35B;
					IL_2D9:
					num = 14;
					continue;
					IL_2FD:
					num = 0;
				}
				IL_B6:
				throw new ArgumentNullException(RecordTableEnumerator.b("⁂ⵄ♆㭈㽊ౌ♎", a_));
				IL_1E1:
				goto IL_26A;
				IL_208:
				return null;
				IL_26A:
				Ptg a_2 = A_0.ᜆ()[0];
				return this.ᜀ(a_2);
				IL_35B:
				return null;
			}
			}
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x000D6B48 File Offset: 0x000D5B48
		private IXLSRange ᜀ(Ptg A_0)
		{
			int a_ = 2;
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.ToString(this.\u170D.FormulaUtil, 0, 0, false).IndexOf(RecordTableEnumerator.b("ᬷ根礻砽", a_)) != -1)
					{
						num = 3;
						continue;
					}
					goto IL_120;
				case 1:
					num = 0;
					continue;
				case 2:
					goto IL_DF;
				case 3:
					goto IL_8D;
				case 4:
					if (!(A_0 is sprỜ))
					{
						if (true)
						{
						}
						num = 2;
						continue;
					}
					goto IL_E1;
				case 6:
					goto IL_4E;
				case 7:
					if (A_0.IsOperation)
					{
						goto IL_120;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E1;
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
					num = 6;
					continue;
				}
				num = 4;
				continue;
				IL_E1:
				num = 7;
			}
			IL_4E:
			throw new ArgumentNullException(RecordTableEnumerator.b("嬷伹主䰽┿ⱁぃᙅ㱇ⵉ", a_));
			IL_8D:
			return null;
			IL_DF:
			throw new spr\u2313(RecordTableEnumerator.b("嬷伹主䰽┿ⱁぃᙅ㱇ⵉ", a_));
			IL_120:
			sprỜ sprỜ = (sprỜ)A_0;
			return sprỜ.ᜀ(this.\u170D, null);
		}

		// Token: 0x04000F42 RID: 3906
		internal const int ᜀ = 65535;

		// Token: 0x04000F43 RID: 3907
		private const int ᜁ = 65532;

		// Token: 0x04000F44 RID: 3908
		internal const int ᜂ = -1;

		// Token: 0x04000F45 RID: 3909
		internal const string ᜃ = "TRUE";

		// Token: 0x04000F46 RID: 3910
		internal const string ᜄ = "FALSE";

		// Token: 0x04000F47 RID: 3911
		private XlsEventHandler ᜅ;

		// Token: 0x04000F48 RID: 3912
		private IXLSRange ᜆ;

		// Token: 0x04000F49 RID: 3913
		private IXLSRange ᜇ;

		// Token: 0x04000F4A RID: 3914
		private IXLSRange ᜈ;

		// Token: 0x04000F4B RID: 3915
		private string ᜉ;

		// Token: 0x04000F4C RID: 3916
		private Ptg[] ᜊ;

		// Token: 0x04000F4D RID: 3917
		private Dictionary<sprᢀ.LinkIndex, sprᢀ> ᜋ = new Dictionary<sprᢀ.LinkIndex, sprᢀ>();

		// Token: 0x04000F4E RID: 3918
		private int ᜌ;

		// Token: 0x04000F4F RID: 3919
		private XlsWorkbook \u170D;

		// Token: 0x04000F50 RID: 3920
		private sprḠ ᜎ;

		// Token: 0x04000F51 RID: 3921
		private XlsChart ᜏ;

		// Token: 0x04000F52 RID: 3922
		private XlsChartSeries ᜐ;

		// Token: 0x04000F53 RID: 3923
		private int ᜑ;

		// Token: 0x04000F54 RID: 3924
		private bool \u1712 = true;

		// Token: 0x04000F55 RID: 3925
		protected internal XlsChartDataPointsCollection m_dataPoints;

		// Token: 0x04000F56 RID: 3926
		private ExcelChartType \u1713;

		// Token: 0x04000F57 RID: 3927
		private List<BiffRecordRaw> \u1714 = new List<BiffRecordRaw>();

		// Token: 0x04000F58 RID: 3928
		private List<BiffRecordRaw> \u1715 = new List<BiffRecordRaw>();

		// Token: 0x04000F59 RID: 3929
		private List<BiffRecordRaw> \u1716 = new List<BiffRecordRaw>();

		// Token: 0x04000F5A RID: 3930
		private float \u2460\u00AC\u0090ª;

		// Token: 0x04000F5B RID: 3931
		private object[] \u1717;

		// Token: 0x04000F5C RID: 3932
		private object[] \u1718;

		// Token: 0x04000F5D RID: 3933
		private object[] \u1719;

		// Token: 0x04000F5E RID: 3934
		private IXLSRange \u171A;

		// Token: 0x04000F5F RID: 3935
		private string \u171B;

		// Token: 0x04000F60 RID: 3936
		private spr\u237B \u171C;

		// Token: 0x04000F61 RID: 3937
		private spr\u237B \u171D;

		// Token: 0x04000F62 RID: 3938
		private spr\u2457 \u171E;

		// Token: 0x04000F63 RID: 3939
		private bool? \u171F = null;

		// Token: 0x04000F64 RID: 3940
		private string ᜠ;

		// Token: 0x04000F65 RID: 3941
		private string ᜡ;

		// Token: 0x04000F66 RID: 3942
		private XlsEventHandler ᜢ;
	}
}
