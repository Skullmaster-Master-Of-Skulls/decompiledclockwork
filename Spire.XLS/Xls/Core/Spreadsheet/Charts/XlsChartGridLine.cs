using System;
using System.Collections.Generic;
using Spire.Xls.Charts;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Charts
{
	// Token: 0x02000195 RID: 405
	public class XlsChartGridLine : XlsObject, IChartGridLine
	{
		// Token: 0x0600141E RID: 5150 RVA: 0x000C1B9C File Offset: 0x000C0B9C
		internal XlsChartGridLine(spr\u1DF5 A_0, object A_1, AxisLineIdentifierType A_2)
		{
			int a_ = 9;
			base..ctor(A_0, A_1);
			if (A_2 != AxisLineIdentifierType.MajorGridLine && A_2 != AxisLineIdentifierType.MinorGridLine)
			{
				throw new ArgumentException(RecordTableEnumerator.b("帾㥀⩂㙄ፆえ㭊⡌", a_));
			}
			this.ᜁ = (spr\u231E)spr\u175E.ᜀ(TBIFFRecord.ChartAxisLineFormat);
			this.ᜅ = new ChartBorder((spr\u2158)A_0, this);
			this.ᜅ.KnownColor = (ExcelColors)77;
			this.ᜅ.Weight = ChartLineWeightType.Hairline;
			this.AxisLineType = A_2;
			this.ᜅ.UseDefaultFormat = true;
			this.ᜀ();
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x000C1C38 File Offset: 0x000C0C38
		internal XlsChartGridLine(spr\u1DF5 A_0, object A_1, IList<BiffRecordRaw> A_2, ref int A_3) : base(A_0, A_1)
		{
			this.Parse(A_2, ref A_3);
			this.ᜀ();
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x000C1C5C File Offset: 0x000C0C5C
		private void ᜀ()
		{
			int a_ = 13;
			this.ᜂ = (XlsChartAxis)XlsObject.FindParent(base.Parent, typeof(XlsChartAxis), true);
			this.ᜄ = (XlsWorkbook)base.FindParent(typeof(XlsWorkbook));
			if (this.ᜄ == null)
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
				throw new ApplicationException(RecordTableEnumerator.b("ፂ⑄㕆ⱈ╊㥌潎㹐ㅒ㽔㉖㩘⽚絜㱞`ൢ୤ࡦᵨ䭪ཬ੮兰ᕲᩴɶ᝸ὺ卼", a_));
			}
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x000C1CFC File Offset: 0x000C0CFC
		internal virtual void Parse(IList<BiffRecordRaw> data, ref int iPos)
		{
			int a_ = 2;
			int num = 2;
			for (;;)
			{
				BiffRecordRaw biffRecordRaw;
				switch (num)
				{
				case 0:
					return;
				case 1:
					goto IL_38;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_30;
					default:
						if (false)
						{
						}
						if (biffRecordRaw.TypeCode == TBIFFRecord.ChartLineFormat)
						{
							num = 4;
							continue;
						}
						return;
					}
					break;
				case 4:
					this.ᜅ = new ChartBorder((spr\u2158)base.ReservedHandle, this, data, ref iPos);
					num = 0;
					continue;
				}
				goto IL_2D;
				IL_30:
				num = 1;
				continue;
				IL_2D:
				if (data == null)
				{
					goto IL_30;
				}
				biffRecordRaw = data[iPos];
				biffRecordRaw.CheckTypeCode(TBIFFRecord.ChartAxisLineFormat);
				this.ᜁ = (spr\u231E)biffRecordRaw;
				iPos++;
				biffRecordRaw = data[iPos];
				num = 3;
			}
			IL_38:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("尷嬹䠻弽", a_));
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x000C1E08 File Offset: 0x000C0E08
		internal virtual void SerializeDataToList(RecordArrayList records)
		{
			int a_ = 19;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜁ == null)
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					goto IL_93;
				case 1:
					goto IL_50;
				case 3:
					return;
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
					if (records == null)
					{
						num = 1;
						continue;
					}
					break;
				}
				num = 0;
			}
			IL_50:
			throw new ArgumentNullException(RecordTableEnumerator.b("㭈⹊⹌⁎⍐㝒♔", a_));
			IL_93:
			records.ᜀ((BiffRecordRaw)this.ᜁ.Clone());
			this.ᜅ.ᜀ(records);
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x000C1ECC File Offset: 0x000C0ECC
		public ChartBorder Border
		{
			get
			{
				for (;;)
				{
					IL_00:
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_7B;
						case 2:
							this.ᜅ = new ChartBorder((spr\u2158)base.ReservedHandle, this);
							if (true)
							{
							}
							num = 0;
							continue;
						}
						if (this.ᜅ != null)
						{
							goto IL_7D;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (false)
							{
							}
							num = 2;
							break;
						}
					}
				}
				IL_7B:
				IL_7D:
				this.ᜅ.HasLineProperties = true;
				return this.ᜅ as ChartBorder;
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06001424 RID: 5156 RVA: 0x000C1F70 File Offset: 0x000C0F70
		public ChartBorder LineProperties
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
				return this.Border;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x000C1FB4 File Offset: 0x000C0FB4
		public bool HasLineProperties
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
				return this.ᜅ != null;
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x000C1FFC File Offset: 0x000C0FFC
		public ChartShadow Shadow
		{
			get
			{
				for (;;)
				{
					IL_00:
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_76;
						case 1:
							this.ᜃ = new ChartShadow(base.AppImplementation, this);
							num = 0;
							continue;
						}
						if (true)
						{
						}
						if (this.ᜃ != null)
						{
							goto IL_78;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (false)
							{
							}
							num = 1;
							break;
						}
					}
				}
				IL_76:
				IL_78:
				return this.ᜃ;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06001427 RID: 5159 RVA: 0x000C2088 File Offset: 0x000C1088
		// (set) Token: 0x06001428 RID: 5160 RVA: 0x000C20D0 File Offset: 0x000C10D0
		public bool HasShadow
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
				return this.ᜃ != null;
			}
			internal set
			{
				if (value)
				{
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_33;
					}
					if (false)
					{
					}
					IL_33:
					ChartShadow shadow = this.Shadow;
					return;
				}
				this.ᜃ = null;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x000C2120 File Offset: 0x000C1120
		public Format3D Format3D
		{
			get
			{
				for (;;)
				{
					IL_00:
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_76;
						case 1:
							this.ᜆ = new Format3D(base.AppImplementation, this);
							if (true)
							{
							}
							num = 0;
							continue;
						}
						if (this.ᜆ != null)
						{
							goto IL_78;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (false)
							{
							}
							num = 1;
							break;
						}
					}
				}
				IL_76:
				IL_78:
				return this.ᜆ;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x000C21AC File Offset: 0x000C11AC
		// (set) Token: 0x0600142B RID: 5163 RVA: 0x000C21F4 File Offset: 0x000C11F4
		public bool HasFormat3D
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
				return this.ᜆ != null;
			}
			internal set
			{
				if (value)
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
					Format3D format3D = this.Format3D;
					return;
				}
				this.ᜆ = null;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x000C2244 File Offset: 0x000C1244
		public virtual bool HasInterior
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
				return false;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x000C2280 File Offset: 0x000C1280
		public virtual IChartInterior Interior
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
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x000C22C0 File Offset: 0x000C12C0
		public virtual IShapeFill Fill
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
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x000C2300 File Offset: 0x000C1300
		public virtual void Delete()
		{
			if (true)
			{
			}
			if (this.ᜁ.ᜀ() == AxisLineIdentifierType.MajorGridLine)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3E;
				}
				if (false)
				{
				}
				IL_3E:
				this.ᜂ.HasMajorGridLines = false;
				return;
			}
			this.ᜂ.HasMinorGridLines = false;
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x000C2364 File Offset: 0x000C1364
		// (set) Token: 0x06001431 RID: 5169 RVA: 0x000C23AC File Offset: 0x000C13AC
		public AxisLineIdentifierType AxisLineType
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

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06001432 RID: 5170 RVA: 0x000C23F4 File Offset: 0x000C13F4
		protected XlsChartAxis ParentAxis
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
				return this.ᜂ;
			}
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x000C2438 File Offset: 0x000C1438
		public virtual object Clone(object parent)
		{
			XlsChartGridLine xlsChartGridLine;
			for (;;)
			{
				if (true)
				{
				}
				xlsChartGridLine = (XlsChartGridLine)base.MemberwiseClone();
				xlsChartGridLine.SetParent(parent);
				xlsChartGridLine.ᜀ();
				xlsChartGridLine.ᜁ = (spr\u231E)spr\u1CD3.ᜀ(this.ᜁ);
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜅ != null)
						{
							goto IL_5B;
						}
						return xlsChartGridLine;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_5B;
						default:
							if (false)
							{
							}
							xlsChartGridLine.ᜅ = this.ᜅ.Clone(xlsChartGridLine);
							num = 2;
							continue;
						}
						break;
					case 2:
						return xlsChartGridLine;
					}
					break;
					IL_5B:
					num = 1;
				}
			}
			return xlsChartGridLine;
		}

		// Token: 0x04000ECD RID: 3789
		private const ExcelColors ᜀ = (ExcelColors)77;

		// Token: 0x04000ECE RID: 3790
		private spr\u231E ᜁ;

		// Token: 0x04000ECF RID: 3791
		private XlsChartAxis ᜂ;

		// Token: 0x04000ED0 RID: 3792
		private bool[] \u2460ª\u0086\u00A7;

		// Token: 0x04000ED1 RID: 3793
		private long[] \u2609\u0086\u0083\u008F;

		// Token: 0x04000ED2 RID: 3794
		private ChartShadow ᜃ;

		// Token: 0x04000ED3 RID: 3795
		internal XlsWorkbook ᜄ;

		// Token: 0x04000ED4 RID: 3796
		private float[] \u2460\u0084\u008D\u00AC;

		// Token: 0x04000ED5 RID: 3797
		private XlsChartBorder ᜅ;

		// Token: 0x04000ED6 RID: 3798
		private Format3D ᜆ;
	}
}
