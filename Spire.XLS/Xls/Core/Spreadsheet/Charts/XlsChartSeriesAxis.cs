using System;
using System.Collections.Generic;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Charts
{
	// Token: 0x02000187 RID: 391
	public class XlsChartSeriesAxis : XlsChartAxis, IChartSeriesAxis, sprᦳ
	{
		// Token: 0x06001314 RID: 4884 RVA: 0x000BAB20 File Offset: 0x000B9B20
		internal XlsChartSeriesAxis(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			base.AxisId = 63149376;
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x000BAB40 File Offset: 0x000B9B40
		internal XlsChartSeriesAxis(spr\u1DF5 A_0, object A_1, AxisType A_2) : this(A_0, A_1, A_2, true)
		{
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x000BAB58 File Offset: 0x000B9B58
		internal XlsChartSeriesAxis(spr\u1DF5 A_0, object A_1, AxisType A_2, bool A_3) : base(A_0, A_1, A_2, A_3)
		{
			base.AxisId = 63149376;
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x000BAB7C File Offset: 0x000B9B7C
		internal XlsChartSeriesAxis(spr\u1DF5 A_0, object A_1, IList<BiffRecordRaw> A_2, ref int A_3) : this(A_0, A_1, A_2, ref A_3, true)
		{
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x000BAB98 File Offset: 0x000B9B98
		internal XlsChartSeriesAxis(spr\u1DF5 A_0, object A_1, IList<BiffRecordRaw> A_2, ref int A_3, bool A_4) : base(A_0, A_1, A_2, ref A_3, A_4)
		{
			base.AxisId = 63149376;
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001319 RID: 4889 RVA: 0x000BABC0 File Offset: 0x000B9BC0
		// (set) Token: 0x0600131A RID: 4890 RVA: 0x000BAC04 File Offset: 0x000B9C04
		public int LabelsFrequency
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

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x0600131B RID: 4891 RVA: 0x000BAC48 File Offset: 0x000B9C48
		// (set) Token: 0x0600131C RID: 4892 RVA: 0x000BAC90 File Offset: 0x000B9C90
		public int TickLabelSpacing
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
				return (int)this.ᜁ.ᜃ();
			}
			set
			{
				int a_ = 13;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (value > 31999)
						{
							if (true)
							{
							}
							num = 3;
							continue;
						}
						goto IL_89;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_89;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					case 3:
						goto IL_87;
					}
					if (value < 0)
					{
						break;
					}
					num = 1;
				}
				IL_37:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ᕂ⑄⭆㱈⹊浌≎⑐⁒⅔睖㭘㹚絜㡞፠٢Ѥ፦䥨Ὢլ੮ὰ卲䕴坶ᡸᕺ᥼彾ꦈﾊﾐ뎒ꚔꚖꂘꊚ꒜놞", a_));
				IL_87:
				goto IL_37;
				IL_89:
				this.ᜁ.ᜂ((ushort)value);
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x0600131D RID: 4893 RVA: 0x000BAD40 File Offset: 0x000B9D40
		// (set) Token: 0x0600131E RID: 4894 RVA: 0x000BAD84 File Offset: 0x000B9D84
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
				if (true)
				{
				}
				if (false)
				{
				}
				this.TickMarkSpacing = value;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x000BADC8 File Offset: 0x000B9DC8
		// (set) Token: 0x06001320 RID: 4896 RVA: 0x000BAE10 File Offset: 0x000B9E10
		public int TickMarkSpacing
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
				return (int)this.ᜁ.ᜅ();
			}
			set
			{
				int a_ = 4;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_87;
					case 2:
						if (value > 31999)
						{
							num = 1;
							continue;
						}
						goto IL_89;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_89;
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
					}
					if (value < 0)
					{
						break;
					}
					num = 3;
				}
				IL_37:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("氹崻刽㔿❁摃⭅㵇㥉㡋湍㉏㝑瑓ㅕ⩗㽙㵛⩝䁟ᙡౣͥ٧䩩屫乭ᅯᱱၳ噵ᑷόཻൽꁿꪉ뾋뾍ꦏꮑ궓뢕", a_));
				IL_87:
				goto IL_37;
				IL_89:
				this.ᜁ.ᜀ((ushort)value);
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001321 RID: 4897 RVA: 0x000BAEC0 File Offset: 0x000B9EC0
		// (set) Token: 0x06001322 RID: 4898 RVA: 0x000BAF08 File Offset: 0x000B9F08
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
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜁ.ᜀ(value);
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06001323 RID: 4899 RVA: 0x000BAF50 File Offset: 0x000B9F50
		protected override ObjectTextLinkType TextLinkType
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
				return ObjectTextLinkType.ZAxis;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001324 RID: 4900 RVA: 0x000BAF8C File Offset: 0x000B9F8C
		// (set) Token: 0x06001325 RID: 4901 RVA: 0x000BAFD4 File Offset: 0x000B9FD4
		public int CrossesAt
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
				return (int)this.ᜁ.ᜄ();
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
				this.ᜁ.ᜁ((ushort)value);
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x000BB01C File Offset: 0x000BA01C
		// (set) Token: 0x06001327 RID: 4903 RVA: 0x000BB064 File Offset: 0x000BA064
		public bool IsBetween
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
				return this.ᜁ.ᜂ();
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
				this.ᜁ.ᜂ(value);
			}
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x000BB0AC File Offset: 0x000BA0AC
		private void ᜀ(BiffRecordRaw A_0)
		{
			int a_ = 19;
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
				throw new ArgumentNullException(RecordTableEnumerator.b("㭈⹊⹌⁎⍐㝒", a_));
			}
			A_0.CheckTypeCode(TBIFFRecord.ChartCatserRange);
			this.ᜁ = (spr\u248C)A_0;
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x000BB120 File Offset: 0x000BA120
		internal override void ParseWallsOrFloor(IList<BiffRecordRaw> data, ref int iPos)
		{
			int a_ = 12;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			throw new NotSupportedException(RecordTableEnumerator.b("ᕁ╃⩅⑇㥉汋⅍≏牑㉓㩕㝗㕙⹛繝͟͡੣ࡥݧṩ䱫౭ᕯ剱ታ᥵൷ᑹ᡻幽ꒃ낏ﾕ", a_));
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x000BB178 File Offset: 0x000BA178
		internal override void ParseData(BiffRecordRaw record, IList<BiffRecordRaw> data, ref int iPos)
		{
			int a_ = 14;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_65;
				case 2:
				{
					TBIFFRecord typeCode;
					if (typeCode != TBIFFRecord.ChartCatserRange)
					{
						num = 1;
						continue;
					}
					goto IL_D1;
				}
				case 3:
					goto IL_CF;
				case 4:
					goto IL_3C;
				case 5:
				{
					if (record == null)
					{
						num = 3;
						continue;
					}
					TBIFFRecord typeCode = record.TypeCode;
					if (true)
					{
					}
					num = 2;
					continue;
				}
				}
				if (data == null)
				{
					num = 4;
				}
				else
				{
					num = 5;
				}
			}
			IL_3C:
			throw new ArgumentNullException(RecordTableEnumerator.b("⁃❅㱇⭉", a_));
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
				return;
			}
			IL_CF:
			throw new ArgumentNullException(RecordTableEnumerator.b("㙃⍅⭇╉㹋⩍", a_));
			IL_D1:
			this.ᜀ(record);
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x000BB260 File Offset: 0x000BA260
		internal override void SerializeDataToList(RecordArrayList records)
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
			spr\u2426 spr_u = (spr\u2426)spr\u175E.ᜀ(TBIFFRecord.ChartAxis);
			spr_u.ᜀ(spr\u2426.ChartAxisType.SeriesAxis);
			records.ᜀ(spr_u);
			records.ᜀ(spr\u175E.ᜀ(TBIFFRecord.Begin));
			records.ᜀ((BiffRecordRaw)this.ᜁ.Clone());
			base.ᜆ(records);
			base.ᜂ(records);
			base.ᜃ(records);
			base.ᜅ(records);
			base.ᜄ(records);
			records.ᜀ(spr\u175E.ᜀ(TBIFFRecord.End));
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x000BB318 File Offset: 0x000BA318
		protected override void InitializeVariables()
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
			this.ᜁ = (spr\u248C)spr\u175E.ᜀ(TBIFFRecord.ChartCatserRange);
			base.InitializeVariables();
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x000BB370 File Offset: 0x000BA370
		public override XlsChartAxis Clone(object parent, Dictionary<int, int> fontIndexes, Dictionary<string, string> dicNewSheetNames)
		{
			XlsChartSeriesAxis xlsChartSeriesAxis;
			for (;;)
			{
				xlsChartSeriesAxis = (XlsChartSeriesAxis)base.Clone(parent, fontIndexes, dicNewSheetNames);
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						IL_3B:
						xlsChartSeriesAxis.ᜁ = (spr\u248C)this.ᜁ.Clone();
						if (true)
						{
						}
						num = 2;
						continue;
					case 1:
						if (this.ᜁ != null)
						{
							num = 0;
							continue;
						}
						goto IL_6F;
					case 2:
						goto IL_6F;
					}
					break;
					IL_6F:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3B;
					default:
						goto IL_85;
					}
				}
			}
			IL_85:
			if (false)
			{
			}
			return xlsChartSeriesAxis;
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x0600132E RID: 4910 RVA: 0x000BB40C File Offset: 0x000BA40C
		// (set) Token: 0x0600132F RID: 4911 RVA: 0x000BB44C File Offset: 0x000BA44C
		public bool IsLogScale
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
				throw new NotImplementedException();
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06001330 RID: 4912 RVA: 0x000BB48C File Offset: 0x000BA48C
		// (set) Token: 0x06001331 RID: 4913 RVA: 0x000BB4CC File Offset: 0x000BA4CC
		public double MaxValue
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
				throw new NotImplementedException();
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06001332 RID: 4914 RVA: 0x000BB50C File Offset: 0x000BA50C
		// (set) Token: 0x06001333 RID: 4915 RVA: 0x000BB54C File Offset: 0x000BA54C
		public double MinValue
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
				throw new NotImplementedException();
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x04000E92 RID: 3730
		private new const int ᜀ = 31999;

		// Token: 0x04000E93 RID: 3731
		private bool[] \u2460\u00AF\u0083\u009B;

		// Token: 0x04000E94 RID: 3732
		private spr\u248C ᜁ;
	}
}
