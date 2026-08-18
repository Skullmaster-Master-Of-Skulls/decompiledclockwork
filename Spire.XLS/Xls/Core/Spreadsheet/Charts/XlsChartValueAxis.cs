using System;
using System.Collections.Generic;
using Spire.Xls.Charts;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Charts
{
	// Token: 0x0200018D RID: 397
	public class XlsChartValueAxis : XlsChartAxis, IChartValueAxis, sprᦳ
	{
		// Token: 0x06001391 RID: 5009 RVA: 0x000BDCF8 File Offset: 0x000BCCF8
		internal XlsChartValueAxis(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x000BDD1C File Offset: 0x000BCD1C
		internal XlsChartValueAxis(spr\u1DF5 A_0, object A_1, AxisType A_2) : this(A_0, A_1, A_2, true)
		{
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x000BDD34 File Offset: 0x000BCD34
		internal XlsChartValueAxis(spr\u1DF5 A_0, object A_1, AxisType A_2, bool A_3) : base(A_0, A_1, A_2, A_3)
		{
			base.AxisId = (base.IsPrimary ? 57253888 : 61870848);
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x000BDD7C File Offset: 0x000BCD7C
		internal XlsChartValueAxis(spr\u1DF5 A_0, object A_1, IList<BiffRecordRaw> A_2, ref int A_3) : this(A_0, A_1, A_2, ref A_3, true)
		{
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x000BDD98 File Offset: 0x000BCD98
		internal XlsChartValueAxis(spr\u1DF5 A_0, object A_1, IList<BiffRecordRaw> A_2, ref int A_3, bool A_4) : base(A_0, A_1, A_2, ref A_3, A_4)
		{
			base.AxisId = (base.IsPrimary ? 57253888 : 61870848);
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001396 RID: 5014 RVA: 0x000BDDE0 File Offset: 0x000BCDE0
		// (set) Token: 0x06001397 RID: 5015 RVA: 0x000BDE30 File Offset: 0x000BCE30
		public virtual double MinValue
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
				this.CheckValueRangeRecord();
				return this.ᜁ.ᜂ();
			}
			set
			{
				int a_ = 11;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (value < this.MaxValue)
						{
							goto IL_9B;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_41;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 2:
						goto IL_41;
					case 3:
						goto IL_99;
					}
					if (!this.IsAutoMax)
					{
						if (true)
						{
						}
						num = 2;
						continue;
					}
					goto IL_9B;
					IL_41:
					num = 0;
				}
				IL_99:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ీ⩂⭄ᅆ⡈❊㡌⩎", a_));
				IL_9B:
				this.CheckValueRangeRecord();
				this.ᜁ.ᜀ(value);
				this.IsAutoMin = false;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001398 RID: 5016 RVA: 0x000BDEF4 File Offset: 0x000BCEF4
		// (set) Token: 0x06001399 RID: 5017 RVA: 0x000BDF44 File Offset: 0x000BCF44
		public virtual double MaxValue
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
				this.CheckValueRangeRecord();
				return this.ᜁ.ᜅ();
			}
			set
			{
				int a_ = 15;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_39;
					case 2:
						if (value > this.MinValue)
						{
							goto IL_9B;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_39;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 3:
						goto IL_99;
					}
					if (!this.IsAutoMin)
					{
						num = 1;
						continue;
					}
					goto IL_9B;
					IL_39:
					num = 2;
				}
				IL_99:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ࡄ♆ㅈᵊⱌ⍎⑐㙒", a_));
				IL_9B:
				this.CheckValueRangeRecord();
				this.ᜁ.ᜂ(value);
				this.IsAutoMax = false;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x0600139A RID: 5018 RVA: 0x000BE008 File Offset: 0x000BD008
		// (set) Token: 0x0600139B RID: 5019 RVA: 0x000BE058 File Offset: 0x000BD058
		public virtual double MajorUnit
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
				this.CheckValueRangeRecord();
				return this.ᜁ.ᜉ();
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
				this.ᜀ(value);
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x0600139C RID: 5020 RVA: 0x000BE09C File Offset: 0x000BD09C
		// (set) Token: 0x0600139D RID: 5021 RVA: 0x000BE0EC File Offset: 0x000BD0EC
		public virtual double MinorUnit
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
				this.CheckValueRangeRecord();
				return this.ᜁ.ᜆ();
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
				this.ᜁ(value);
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x0600139E RID: 5022 RVA: 0x000BE130 File Offset: 0x000BD130
		// (set) Token: 0x0600139F RID: 5023 RVA: 0x000BE174 File Offset: 0x000BD174
		public double CrossValue
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
				return this.CrossesAt;
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
				this.CrossesAt = value;
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x060013A0 RID: 5024 RVA: 0x000BE1B8 File Offset: 0x000BD1B8
		// (set) Token: 0x060013A1 RID: 5025 RVA: 0x000BE200 File Offset: 0x000BD200
		public virtual double CrossesAt
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
				return this.ᜁ.ᜇ();
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
				this.ᜁ.ᜁ(value);
				this.IsAutoCross = false;
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x000BE250 File Offset: 0x000BD250
		// (set) Token: 0x060013A3 RID: 5027 RVA: 0x000BE2A4 File Offset: 0x000BD2A4
		public virtual bool IsAutoMin
		{
			get
			{
				if (!this.CheckValueRangeRecord(false))
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
						return true;
					}
				}
				return this.ᜁ.ᜃ();
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
				this.SetAutoMin(true, value);
			}
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x000BE2E8 File Offset: 0x000BD2E8
		protected void SetAutoMin(bool check, bool value)
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
			this.CheckValueRangeRecord(check);
			this.ᜁ.ᜃ(value);
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x060013A5 RID: 5029 RVA: 0x000BE338 File Offset: 0x000BD338
		// (set) Token: 0x060013A6 RID: 5030 RVA: 0x000BE38C File Offset: 0x000BD38C
		public virtual bool IsAutoMax
		{
			get
			{
				if (true)
				{
				}
				if (!this.CheckValueRangeRecord(false))
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
						return true;
					}
				}
				return this.ᜁ.ᜌ();
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
				this.SetAutoMax(true, value);
			}
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x000BE3D0 File Offset: 0x000BD3D0
		protected void SetAutoMax(bool check, bool value)
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
			this.CheckValueRangeRecord(check);
			this.ᜁ.ᜅ(value);
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x000BE420 File Offset: 0x000BD420
		// (set) Token: 0x060013A9 RID: 5033 RVA: 0x000BE468 File Offset: 0x000BD468
		public virtual bool IsAutoMajor
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
				return this.ᜁ.ᜊ();
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
				this.ᜁ.ᜄ(value);
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x000BE4B0 File Offset: 0x000BD4B0
		// (set) Token: 0x060013AB RID: 5035 RVA: 0x000BE4F8 File Offset: 0x000BD4F8
		public virtual bool IsAutoMinor
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
				return this.ᜁ.ᜁ();
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
				this.ᜁ.ᜇ(value);
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x000BE540 File Offset: 0x000BD540
		// (set) Token: 0x060013AD RID: 5037 RVA: 0x000BE594 File Offset: 0x000BD594
		public virtual bool IsAutoCross
		{
			get
			{
				if (true)
				{
				}
				if (!this.CheckValueRangeRecord(false))
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
						return true;
					}
				}
				return this.ᜁ.ᜎ();
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
				this.CheckValueRangeRecord();
				this.ᜁ.ᜂ(value);
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x060013AE RID: 5038 RVA: 0x000BE5E4 File Offset: 0x000BD5E4
		// (set) Token: 0x060013AF RID: 5039 RVA: 0x000BE638 File Offset: 0x000BD638
		public bool IsLogScale
		{
			get
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return false;
				}
				if (false)
				{
				}
				if (this.CheckValueRangeRecord(false))
				{
					if (true)
					{
					}
					return this.ᜁ.\u170D();
				}
				return false;
			}
			set
			{
				for (;;)
				{
					this.CheckValueRangeRecord();
					this.ᜁ.ᜆ(value);
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 3;
							continue;
						case 1:
							goto IL_158;
						case 2:
							if (value)
							{
								num = 7;
								continue;
							}
							return;
						case 3:
							if (this.ᜁ.ᜅ() < 1.0)
							{
								num = 4;
								continue;
							}
							return;
						case 4:
							this.ᜁ.ᜂ(1.0);
							num = 10;
							continue;
						case 5:
							if (!this.ᜁ.ᜃ())
							{
								num = 9;
								continue;
							}
							goto IL_158;
						case 6:
							if (!this.ᜁ.ᜌ())
							{
								num = 0;
								continue;
							}
							return;
						case 7:
							num = 5;
							continue;
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
								this.ᜁ.ᜀ(1.0);
								num = 1;
								continue;
							}
							break;
						case 9:
							num = 11;
							continue;
						case 10:
							return;
						case 11:
							if (this.ᜁ.ᜂ() < 1.0)
							{
								num = 8;
								continue;
							}
							goto IL_158;
						}
						break;
						IL_158:
						if (true)
						{
						}
						num = 6;
					}
				}
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x060013B0 RID: 5040 RVA: 0x000BE7D0 File Offset: 0x000BD7D0
		// (set) Token: 0x060013B1 RID: 5041 RVA: 0x000BE818 File Offset: 0x000BD818
		public override bool IsReverseOrder
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
				return this.ᜁ.ᜀ();
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
				this.ᜁ.ᜀ(value);
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x000BE860 File Offset: 0x000BD860
		// (set) Token: 0x060013B3 RID: 5043 RVA: 0x000BE8A8 File Offset: 0x000BD8A8
		public virtual bool IsMaxCross
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
				return this.ᜁ.ᜈ();
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
				this.ᜁ.ᜁ(value);
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x060013B4 RID: 5044 RVA: 0x000BE8F0 File Offset: 0x000BD8F0
		// (set) Token: 0x060013B5 RID: 5045 RVA: 0x000BE934 File Offset: 0x000BD934
		internal spr\u21D5 ChartValueRange
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
				return this.ᜁ;
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
				this.ᜁ = value;
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x060013B6 RID: 5046 RVA: 0x000BE978 File Offset: 0x000BD978
		// (set) Token: 0x060013B7 RID: 5047 RVA: 0x000BE9C0 File Offset: 0x000BD9C0
		public double DisplayUnitCustom
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
				this.CheckValueRangeRecord();
				return this.ᜂ;
			}
			set
			{
				int a_ = 18;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_4C;
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.CheckValueRangeRecord();
				if (value > 0.0)
				{
					this.ᜂ = value;
					this.DisplayUnit = ChartDisplayUnitType.Custom;
					return;
				}
				IL_4C:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("᱇≉⥋湍♏㍑㡓⍕㵗穙ㅛ⭝፟ᙡ䑣ѥ൧䩩k཭ɯᕱᅳ噵౷ቹᵻၽꁿꒉ", a_));
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x060013B8 RID: 5048 RVA: 0x000BEA40 File Offset: 0x000BDA40
		// (set) Token: 0x060013B9 RID: 5049 RVA: 0x000BEA88 File Offset: 0x000BDA88
		public ChartDisplayUnitType DisplayUnit
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
				this.CheckValueRangeRecord();
				return this.ᜃ;
			}
			set
			{
				for (;;)
				{
					this.CheckValueRangeRecord();
					this.ᜃ = value;
					int num = 5;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_72;
						case 1:
							if (value < (ChartDisplayUnitType)XlsChartValueAxis.DEF_DISPLAY_UNIT_VALUES.Length)
							{
								num = 7;
								continue;
							}
							goto IL_72;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_A1;
							default:
								goto IL_6A;
							}
							break;
						case 3:
							if (!base.ParentAxis.ᜇ().ParentWorkbook.Loading)
							{
								num = 6;
								continue;
							}
							return;
						case 4:
							return;
						case 5:
							if (true)
							{
							}
							if (value == ChartDisplayUnitType.None)
							{
								num = 2;
								continue;
							}
							num = 1;
							continue;
						case 6:
							this.HasDisplayUnitLabel = true;
							num = 4;
							continue;
						case 7:
							goto IL_A1;
						}
						break;
						IL_72:
						num = 3;
						continue;
						IL_A1:
						this.ᜂ = XlsChartValueAxis.DEF_DISPLAY_UNIT_VALUES[(int)value];
						num = 0;
					}
				}
				IL_6A:
				if (false)
				{
				}
				this.ᜀ = false;
				this.ᜂ = 1.0;
				this.ᜄ = null;
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x060013BA RID: 5050 RVA: 0x000BEBB4 File Offset: 0x000BDBB4
		// (set) Token: 0x060013BB RID: 5051 RVA: 0x000BEBFC File Offset: 0x000BDBFC
		public bool HasDisplayUnitLabel
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
				this.CheckValueRangeRecord();
				return this.ᜀ;
			}
			set
			{
				int a_ = 6;
				if (true)
				{
				}
				for (;;)
				{
					this.CheckValueRangeRecord();
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (this.ᜃ == ChartDisplayUnitType.None)
							{
								num = 5;
								continue;
							}
							num = 1;
							continue;
						case 1:
							if (value)
							{
								num = 2;
								continue;
							}
							goto IL_D2;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_68;
							default:
								if (false)
								{
								}
								num = 4;
								continue;
							}
							break;
						case 3:
							goto IL_D0;
						case 4:
							goto IL_68;
						case 5:
							goto IL_5E;
						case 6:
							this.ᜀ();
							num = 3;
							continue;
						}
						break;
						IL_68:
						if (this.ᜄ != null)
						{
							goto IL_D2;
						}
						num = 6;
					}
				}
				IL_5E:
				throw new NotSupportedException(RecordTableEnumerator.b("砻儽┿ㅁ⩃⥅㱇橉㽋㭍⁏≑㭓⑕ⱗ穙㡛㝝፟ቡࡣݥᅧ䩩ᥫm᥯ٱ味᩵᥷᡹᥻ችꁿꚅ첇ﾋﺍﲏ쎕뺝춡쪣쎥袧잩쎫쪭햯鲱", a_));
				IL_D0:
				IL_D2:
				this.ᜀ = value;
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x060013BC RID: 5052 RVA: 0x000BECE4 File Offset: 0x000BDCE4
		internal IChartTextArea DisplayUnitLabel
		{
			get
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3F;
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.CheckValueRangeRecord();
				if (this.HasDisplayUnitLabel)
				{
					return this.ᜄ;
				}
				IL_3F:
				return null;
			}
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x000BED38 File Offset: 0x000BDD38
		internal virtual void ParseMaxCross(BiffRecordRaw record)
		{
			int a_ = 8;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_34;
			}
			if (false)
			{
			}
			if (record != null)
			{
				if (true)
				{
				}
				record.CheckTypeCode(TBIFFRecord.ChartValueRange);
				this.ᜁ = (spr\u21D5)record;
				return;
			}
			IL_34:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰽┿⅁⭃㑅ⱇ", a_));
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x000BEDAC File Offset: 0x000BDDAC
		[CLSCompliant(false)]
		internal override void ParseData(BiffRecordRaw record, IList<BiffRecordRaw> data, ref int iPos)
		{
			int a_ = 9;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 2:
				{
					TBIFFRecord typeCode;
					if (typeCode != TBIFFRecord.ChartValueRange)
					{
						num = 0;
						continue;
					}
					goto IL_B5;
				}
				case 3:
					num = 2;
					continue;
				case 4:
					num = 7;
					continue;
				case 5:
				{
					TBIFFRecord typeCode;
					if (typeCode != TBIFFRecord.ChartBegDispUnit)
					{
						num = 4;
						continue;
					}
					goto IL_13F;
				}
				case 6:
					goto IL_5E;
				case 7:
				{
					TBIFFRecord typeCode;
					if (typeCode != TBIFFRecord.ChartAxisDisplayUnits)
					{
						num = 3;
						continue;
					}
					goto IL_F4;
				}
				case 8:
					goto IL_B3;
				case 9:
				{
					if (data == null)
					{
						num = 8;
						continue;
					}
					TBIFFRecord typeCode = record.TypeCode;
					num = 5;
					continue;
				}
				}
				if (true)
				{
				}
				if (record == null)
				{
					num = 6;
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
						num = 9;
						break;
					}
				}
			}
			IL_5E:
			throw new ArgumentNullException(RecordTableEnumerator.b("䴾⑀⁂⩄㕆ⵈ", a_));
			IL_B3:
			throw new ArgumentNullException(RecordTableEnumerator.b("嬾⁀㝂⑄", a_));
			IL_B5:
			this.ParseMaxCross(record);
			return;
			IL_F4:
			this.ᜀ((spr\u1AB1)record);
			return;
			IL_13F:
			this.ᜀ(data, ref iPos);
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x000BEF00 File Offset: 0x000BDF00
		internal override void ParseWallsOrFloor(IList<BiffRecordRaw> data, ref int iPos)
		{
			int a_ = 6;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_3C;
			}
			if (false)
			{
			}
			if (true)
			{
			}
			if (data != null)
			{
				base.ParentXlsChart.Floor = new ChartWallOrFloor((spr\u2158)base.ReservedHandle, base.ParentXlsChart, false, data, ref iPos);
				return;
			}
			IL_3C:
			throw new ArgumentNullException(RecordTableEnumerator.b("堻弽㐿⍁", a_));
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x000BEF84 File Offset: 0x000BDF84
		private void ᜀ(spr\u1AB1 A_0)
		{
			int a_ = 16;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_3C;
			}
			if (false)
			{
			}
			if (A_0 != null)
			{
				this.ᜃ = A_0.ᜁ();
				this.ᜂ = A_0.ᜀ();
				this.ᜀ = A_0.ᜅ();
				return;
			}
			if (true)
			{
			}
			IL_3C:
			throw new ArgumentNullException(RecordTableEnumerator.b("㑅ⵇ⥉⍋㱍㑏", a_));
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x000BF008 File Offset: 0x000BE008
		private void ᜀ(IList<BiffRecordRaw> A_0, ref int A_1)
		{
			int a_ = 1;
			int num = 0;
			for (;;)
			{
				BiffRecordRaw biffRecordRaw;
				switch (num)
				{
				case 1:
				{
					TBIFFRecord typeCode;
					if (typeCode == TBIFFRecord.ChartText)
					{
						num = 2;
						continue;
					}
					goto IL_4D;
				}
				case 2:
					this.ᜄ = new XlsChartWrappedTextArea((spr\u2158)base.ReservedHandle, this, A_0, ref A_1);
					A_1--;
					num = 4;
					continue;
				case 3:
					goto IL_C7;
				case 4:
					goto IL_4D;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4D;
					default:
						goto IL_A5;
					}
					break;
				case 6:
				{
					if (biffRecordRaw.TypeCode == TBIFFRecord.ChartEndDispUnit)
					{
						num = 8;
						continue;
					}
					TBIFFRecord typeCode = biffRecordRaw.TypeCode;
					num = 1;
					continue;
				}
				case 7:
					goto IL_C7;
				case 8:
					return;
				}
				if (A_0 == null)
				{
					num = 5;
					continue;
				}
				biffRecordRaw = A_0[A_1];
				biffRecordRaw.CheckTypeCode(TBIFFRecord.ChartBegDispUnit);
				A_1++;
				biffRecordRaw = XlsChartTextArea.ᜀ(A_0[A_1]);
				num = 3;
				continue;
				IL_4D:
				A_1++;
				biffRecordRaw = XlsChartTextArea.ᜀ(A_0[A_1]);
				num = 7;
				continue;
				IL_C7:
				num = 6;
			}
			IL_A5:
			if (true)
			{
			}
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("匶堸伺尼", a_));
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x000BF170 File Offset: 0x000BE170
		internal override void SerializeDataToList(RecordArrayList records)
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
			this.ᜀ(records, spr\u2426.ChartAxisType.ValueAxis);
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x000BF1B4 File Offset: 0x000BE1B4
		internal void ᜀ(RecordArrayList A_0, spr\u2426.ChartAxisType A_1)
		{
			int a_ = 7;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (base.IsPrimary)
					{
						num = 3;
						continue;
					}
					goto IL_127;
				case 2:
					goto IL_5B;
				case 3:
					base.ᜄ(A_0);
					this.SerializeWallsOrFloor(A_0);
					if (true)
					{
					}
					num = 2;
					continue;
				case 4:
					goto IL_38;
				}
				if (A_0 == null)
				{
					num = 4;
				}
				else
				{
					spr\u2426 spr_u = (spr\u2426)spr\u175E.ᜀ(TBIFFRecord.ChartAxis);
					spr_u.ᜀ(A_1);
					A_0.ᜀ(spr_u);
					A_0.ᜀ(spr\u175E.ᜀ(TBIFFRecord.Begin));
					A_0.ᜀ((BiffRecordRaw)this.ᜁ.Clone());
					this.ᜀ(A_0);
					base.ᜂ(A_0);
					base.ᜆ(A_0);
					base.ᜃ(A_0);
					base.ᜅ(A_0);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_38;
					default:
						if (false)
						{
						}
						num = 0;
						break;
					}
				}
			}
			IL_38:
			throw new ArgumentNullException(RecordTableEnumerator.b("似娾≀ⱂ㝄⍆㩈", a_));
			IL_5B:
			IL_127:
			A_0.ᜀ(spr\u175E.ᜀ(TBIFFRecord.End));
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x000BF2FC File Offset: 0x000BE2FC
		internal virtual void SerializeWallsOrFloor(RecordArrayList records)
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
			base.ParentXlsChart.\u170D(records);
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x000BF344 File Offset: 0x000BE344
		private void ᜀ(RecordArrayList A_0)
		{
			int a_ = 12;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_118;
				case 2:
					goto IL_4B;
				case 3:
					return;
				case 4:
					if (this.ᜃ == ChartDisplayUnitType.None)
					{
						num = 3;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_43;
					default:
					{
						if (false)
						{
						}
						spr\u1AB1 spr_u1AB = (spr\u1AB1)spr\u175E.ᜀ(TBIFFRecord.ChartAxisDisplayUnits);
						spr_u1AB.ᜀ(this.ᜀ);
						spr_u1AB.ᜀ(this.ᜂ);
						spr_u1AB.ᜀ(this.ᜃ);
						A_0.ᜀ(spr_u1AB);
						spr\u21C9 spr_u21C = (spr\u21C9)spr\u175E.ᜀ(TBIFFRecord.ChartBegDispUnit);
						spr_u21C.ᜀ(this.ᜀ);
						A_0.ᜀ(spr_u21C);
						num = 6;
						continue;
					}
					}
					break;
				case 5:
					this.ᜄ.SerializeDataToList(A_0);
					num = 1;
					continue;
				case 6:
					if (this.ᜀ)
					{
						num = 5;
						continue;
					}
					goto IL_147;
				}
				goto IL_35;
				IL_43:
				num = 2;
				continue;
				IL_35:
				if (true)
				{
				}
				if (A_0 == null)
				{
					goto IL_43;
				}
				num = 4;
			}
			IL_4B:
			throw new ArgumentNullException(RecordTableEnumerator.b("ぁ⅃╅❇㡉⡋㵍", a_));
			IL_118:
			IL_147:
			spr\u1ABA spr_u1ABA = (spr\u1ABA)spr\u175E.ᜀ(TBIFFRecord.ChartEndDispUnit);
			spr_u1ABA.ᜀ(this.ᜀ);
			A_0.ᜀ(spr_u1ABA);
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x000BF4BC File Offset: 0x000BE4BC
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
			this.ᜁ = (spr\u21D5)spr\u175E.ᜀ(TBIFFRecord.ChartValueRange);
			base.InitializeVariables();
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x000BF514 File Offset: 0x000BE514
		protected bool CheckValueRangeRecord()
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
			return this.CheckValueRangeRecord(!base.ParentWorkbook.Loading && !base.ParentWorkbook.Saving && !base.ParentWorkbook.IsCreated);
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x000BF588 File Offset: 0x000BE588
		protected virtual bool CheckValueRangeRecord(bool throwException)
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
			return true;
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x060013C9 RID: 5065 RVA: 0x000BF5C4 File Offset: 0x000BE5C4
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
				return ObjectTextLinkType.YAxis;
			}
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x000BF600 File Offset: 0x000BE600
		public override XlsChartAxis Clone(object parent, Dictionary<int, int> fontIndexes, Dictionary<string, string> dicNewSheetNames)
		{
			XlsChartValueAxis xlsChartValueAxis;
			for (;;)
			{
				xlsChartValueAxis = (XlsChartValueAxis)base.Clone(parent, fontIndexes, dicNewSheetNames);
				if (true)
				{
				}
				int num = 4;
				for (;;)
				{
					IL_02:
					switch (num)
					{
					case 0:
						goto IL_74;
					case 1:
						xlsChartValueAxis.ᜄ = (XlsChartWrappedTextArea)this.ᜄ.Clone(xlsChartValueAxis, fontIndexes, dicNewSheetNames);
						num = 3;
						continue;
					case 2:
						xlsChartValueAxis.ᜁ = (spr\u21D5)this.ᜁ.Clone();
						num = 0;
						continue;
					case 3:
						return xlsChartValueAxis;
					case 4:
						if (this.ᜁ != null)
						{
							num = 2;
							continue;
						}
						goto IL_74;
					case 5:
						while (this.ᜄ != null)
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
								num = 1;
								goto IL_02;
							}
						}
						return xlsChartValueAxis;
					}
					break;
					IL_74:
					num = 5;
				}
			}
			return xlsChartValueAxis;
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x000BF6E8 File Offset: 0x000BE6E8
		private void ᜀ()
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_B5;
				default:
				{
					if (false)
					{
					}
					this.ᜄ = new XlsChartWrappedTextArea((spr\u2158)base.ReservedHandle, this, ObjectTextLinkType.DisplayUnit);
					this.ᜄ.TextRecord.ᜁ(true);
					this.ᜄ.IsBold = true;
					this.ᜄ.IsAutoMode = true;
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (base.AxisType == AxisType.Value)
							{
								num = 1;
								continue;
							}
							goto IL_B7;
						case 1:
							this.ᜄ.TextRotationAngle = 90;
							num = 2;
							continue;
						case 2:
							goto IL_B5;
						}
						break;
					}
					break;
				}
				}
			}
			IL_B5:
			IL_B7:
			if (true)
			{
			}
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x000BF7B4 File Offset: 0x000BE7B4
		internal void ᜀ(double A_0)
		{
			int a_ = 0;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_45;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_45;
					default:
						goto IL_8A;
					}
					break;
				case 3:
					if (A_0 < this.MinorUnit)
					{
						num = 2;
						continue;
					}
					goto IL_C4;
				case 4:
					if (!this.IsAutoMinor)
					{
						num = 5;
						continue;
					}
					goto IL_C4;
				case 5:
					num = 3;
					continue;
				}
				if (A_0 > 0.0)
				{
					num = 0;
					continue;
				}
				break;
				IL_45:
				num = 4;
			}
			IL_47:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("笵夷倹医䰽ᔿⱁⵃ㉅", a_));
			IL_8A:
			if (true)
			{
			}
			if (false)
			{
			}
			goto IL_47;
			IL_C4:
			this.ᜁ.ᜃ(A_0);
			this.IsAutoMajor = false;
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x000BF898 File Offset: 0x000BE898
		internal void ᜁ(double A_0)
		{
			int a_ = 9;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					if (A_0 > this.ᜁ.ᜉ())
					{
						num = 3;
						continue;
					}
					goto IL_C9;
				case 1:
					num = 0;
					continue;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_45;
					default:
						goto IL_97;
					}
					break;
				case 4:
					if (!this.IsAutoMajor)
					{
						num = 1;
						continue;
					}
					goto IL_C9;
				case 5:
					goto IL_45;
				}
				if (A_0 > 0.0)
				{
					num = 5;
					continue;
				}
				break;
				IL_45:
				num = 4;
			}
			IL_47:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("爾⡀ⵂ⩄㕆᱈╊⑌㭎", a_));
			IL_97:
			if (false)
			{
			}
			goto IL_47;
			IL_C9:
			this.ᜁ.ᜄ(A_0);
			this.IsAutoMinor = false;
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x000BF984 File Offset: 0x000BE984
		// Note: this type is marked as 'beforefieldinit'.
		static XlsChartValueAxis()
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
			XlsChartValueAxis.DEF_DISPLAY_UNIT_VALUES = new double[]
			{
				0.0,
				100.0,
				1000.0,
				10000.0,
				100000.0,
				1000000.0,
				10000000.0,
				100000000.0,
				1000000000.0,
				1000000000000.0
			};
		}

		// Token: 0x04000EAB RID: 3755
		public static readonly double[] DEF_DISPLAY_UNIT_VALUES;

		// Token: 0x04000EAC RID: 3756
		private new bool ᜀ;

		// Token: 0x04000EAD RID: 3757
		private int \u2460\u00A2\u00AE\u0089;

		// Token: 0x04000EAE RID: 3758
		private spr\u21D5 ᜁ;

		// Token: 0x04000EAF RID: 3759
		private new double ᜂ = 1.0;

		// Token: 0x04000EB0 RID: 3760
		private new ChartDisplayUnitType ᜃ;

		// Token: 0x04000EB1 RID: 3761
		private string[] \u25D8\u00A6\u0099\u0085;

		// Token: 0x04000EB2 RID: 3762
		private int[] \u2460\u00A5\u0080\u00A9;

		// Token: 0x04000EB3 RID: 3763
		private new XlsChartWrappedTextArea ᜄ;
	}
}
