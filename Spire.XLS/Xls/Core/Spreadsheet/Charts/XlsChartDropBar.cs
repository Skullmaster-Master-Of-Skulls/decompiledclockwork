using System;
using System.Collections.Generic;
using System.Drawing;
using Spire.Xls.Charts;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Charts
{
	// Token: 0x02000191 RID: 401
	public class XlsChartDropBar : XlsObject, IChartDropBar, spr\u218E
	{
		// Token: 0x060013F0 RID: 5104 RVA: 0x000C0728 File Offset: 0x000BF728
		internal XlsChartDropBar(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜀ();
			this.ᜀ = (sprᡘ)spr\u175E.ᜀ(TBIFFRecord.ChartDropBar);
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x000C0758 File Offset: 0x000BF758
		private void ᜀ()
		{
			int a_ = 10;
			this.ᜃ = (XlsWorkbook)base.FindParent(typeof(XlsWorkbook));
			if (this.ᜃ != null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2E;
				}
				if (false)
				{
				}
				return;
			}
			IL_2E:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("ဿ⍁㙃⍅♇㹉汋⅍㉏㡑ㅓ㕕ⱗ穙㽛㽝๟ౡୣብ䡧ࡩ५乭ᙯᵱųᡵᱷ呹", a_));
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x000C07D8 File Offset: 0x000BF7D8
		internal void ᜀ(IList<BiffRecordRaw> A_0, ref int A_1)
		{
			int a_ = 8;
			int num = 3;
			for (;;)
			{
				BiffRecordRaw biffRecordRaw;
				int num2;
				switch (num)
				{
				case 0:
					goto IL_167;
				case 1:
					num = 12;
					continue;
				case 2:
					goto IL_167;
				case 4:
				{
					if (true)
					{
					}
					TBIFFRecord typeCode;
					if (typeCode != TBIFFRecord.ChartLineFormat)
					{
						num = 14;
						continue;
					}
					this.ᜁ = new ChartBorder((spr\u2158)base.AppImplementation, this, (spr\u22F3)biffRecordRaw);
					num = 6;
					continue;
				}
				case 5:
				{
					TBIFFRecord typeCode;
					if (typeCode != TBIFFRecord.ChartAreaFormat)
					{
						num = 1;
						continue;
					}
					this.ᜂ = new ChartInterior((spr\u2158)base.AppImplementation, this, (sprᨓ)biffRecordRaw);
					num = 8;
					continue;
				}
				case 6:
					goto IL_F0;
				case 7:
					goto IL_F0;
				case 8:
					goto IL_F0;
				case 9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_212;
					default:
						goto IL_19A;
					}
					break;
				case 10:
					goto IL_F0;
				case 11:
					goto IL_6B;
				case 12:
				{
					TBIFFRecord typeCode;
					switch (typeCode)
					{
					case TBIFFRecord.Begin:
						num2++;
						A_1 = BiffRecordRaw.SkipBeginEndBlock(A_0, A_1);
						num = 15;
						continue;
					case TBIFFRecord.End:
						num2--;
						num = 7;
						continue;
					default:
						num = 16;
						continue;
					}
					break;
				}
				case 13:
				{
					if (num2 <= 0)
					{
						num = 9;
						continue;
					}
					biffRecordRaw = A_0[A_1];
					TBIFFRecord typeCode = biffRecordRaw.TypeCode;
					num = 4;
					continue;
				}
				case 14:
					num = 5;
					continue;
				case 15:
					goto IL_F0;
				case 16:
					goto IL_212;
				}
				if (A_0 == null)
				{
					num = 11;
					continue;
				}
				biffRecordRaw = A_0[A_1];
				biffRecordRaw.CheckTypeCode(TBIFFRecord.ChartDropBar);
				this.ᜀ = (sprᡘ)A_0[A_1];
				A_0[A_1 + 1].CheckTypeCode(TBIFFRecord.Begin);
				A_1 += 2;
				num2 = 1;
				num = 2;
				continue;
				IL_F0:
				A_1++;
				num = 0;
				continue;
				IL_167:
				num = 13;
				continue;
				IL_212:
				num = 10;
			}
			IL_6B:
			throw new ArgumentNullException(RecordTableEnumerator.b("娽ℿ㙁╃", a_));
			IL_19A:
			if (false)
			{
			}
			A_1--;
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x000C0A3C File Offset: 0x000BFA3C
		public void SerializeDataToList(RecordArrayList records)
		{
			int a_ = 19;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_54;
				case 1:
					if (this.ᜁ != null)
					{
						num = 11;
						continue;
					}
					goto IL_59;
				case 3:
					if (!this.ᜂ.UseDefaultFormat)
					{
						num = 9;
						continue;
					}
					goto IL_19A;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_160;
					}
					if (false)
					{
					}
					if (this.ᜂ != null)
					{
						num = 6;
						continue;
					}
					goto IL_19A;
				case 5:
					goto IL_D6;
				case 6:
					num = 3;
					continue;
				case 7:
					goto IL_F2;
				case 8:
					if (this.ᜀ == null)
					{
						num = 5;
						continue;
					}
					if (true)
					{
					}
					records.ᜀ((BiffRecordRaw)this.ᜀ.Clone());
					records.ᜀ(spr\u175E.ᜀ(TBIFFRecord.Begin));
					goto IL_160;
				case 9:
					this.ᜂ.ᜀ(records);
					num = 7;
					continue;
				case 10:
					goto IL_59;
				case 11:
					this.ᜁ.ᜀ(records);
					num = 10;
					continue;
				}
				if (records == null)
				{
					num = 0;
					continue;
				}
				num = 8;
				continue;
				IL_59:
				num = 4;
				continue;
				IL_160:
				num = 1;
			}
			IL_54:
			throw new ArgumentException(RecordTableEnumerator.b("㭈⹊⹌⁎⍐㝒♔", a_));
			IL_D6:
			throw new ApplicationException(RecordTableEnumerator.b("ⵈ㥊≌㽎㍐㉒❔", a_));
			IL_F2:
			IL_19A:
			records.ᜀ(spr\u175E.ᜀ(TBIFFRecord.End));
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x060013F4 RID: 5108 RVA: 0x000C0BF4 File Offset: 0x000BFBF4
		public bool HasInterior
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
				return this.ᜂ != null;
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x060013F5 RID: 5109 RVA: 0x000C0C3C File Offset: 0x000BFC3C
		public ChartShadow Shadow
		{
			get
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
							if (true)
							{
							}
							goto IL_5C;
						case 2:
							goto IL_76;
						}
						if (this.ᜆ == null)
						{
							num = 1;
							continue;
						}
						goto IL_78;
					}
					IL_5C:
					this.ᜆ = new ChartShadow(base.AppImplementation, this);
					num = 2;
				}
				IL_76:
				IL_78:
				return this.ᜆ;
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x060013F6 RID: 5110 RVA: 0x000C0CC8 File Offset: 0x000BFCC8
		// (set) Token: 0x060013F7 RID: 5111 RVA: 0x000C0D10 File Offset: 0x000BFD10
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
				return this.ᜆ != null;
			}
			internal set
			{
				if (!value)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_05;
					}
					if (false)
					{
					}
					this.ᜆ = null;
					return;
				}
				IL_05:
				if (true)
				{
				}
				ChartShadow shadow = this.Shadow;
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x060013F8 RID: 5112 RVA: 0x000C0D60 File Offset: 0x000BFD60
		public Format3D Format3D
		{
			get
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
							goto IL_5C;
						case 2:
							goto IL_76;
						}
						if (this.ᜅ == null)
						{
							if (true)
							{
							}
							num = 1;
							continue;
						}
						goto IL_78;
					}
					IL_5C:
					this.ᜅ = new Format3D(base.AppImplementation, this);
					num = 2;
				}
				IL_76:
				IL_78:
				return this.ᜅ;
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x060013F9 RID: 5113 RVA: 0x000C0DEC File Offset: 0x000BFDEC
		// (set) Token: 0x060013FA RID: 5114 RVA: 0x000C0E34 File Offset: 0x000BFE34
		public bool HasFormat3D
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
			internal set
			{
				if (!value)
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
						this.ᜅ = null;
						return;
					}
				}
				Format3D format3D = this.Format3D;
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x060013FB RID: 5115 RVA: 0x000C0E84 File Offset: 0x000BFE84
		public bool HasLineProperties
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
				return this.ᜁ != null;
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x060013FC RID: 5116 RVA: 0x000C0ECC File Offset: 0x000BFECC
		// (set) Token: 0x060013FD RID: 5117 RVA: 0x000C0F14 File Offset: 0x000BFF14
		public int GapWidth
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
				return (int)this.ᜀ.ᜀ();
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
				this.ᜀ.ᜀ((ushort)value);
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x060013FE RID: 5118 RVA: 0x000C0F5C File Offset: 0x000BFF5C
		public IChartInterior Interior
		{
			get
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
						if (true)
						{
						}
						if (false)
						{
						}
						switch (num)
						{
						case 1:
							goto IL_5C;
						case 2:
							goto IL_7B;
						}
						if (this.ᜂ == null)
						{
							num = 1;
							continue;
						}
						goto IL_7D;
					}
					IL_5C:
					this.ᜂ = new ChartInterior((spr\u2158)base.AppImplementation, this);
					num = 2;
				}
				IL_7B:
				IL_7D:
				return this.ᜂ;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x000C0FEC File Offset: 0x000BFFEC
		public ChartBorder LineProperties
		{
			get
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
							goto IL_7B;
						case 2:
							goto IL_5C;
						}
						if (true)
						{
						}
						if (this.ᜁ == null)
						{
							num = 2;
							continue;
						}
						goto IL_7D;
					}
					IL_5C:
					this.ᜁ = new ChartBorder((spr\u2158)base.AppImplementation, this);
					num = 1;
				}
				IL_7B:
				IL_7D:
				return this.ᜁ as ChartBorder;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06001400 RID: 5120 RVA: 0x000C1084 File Offset: 0x000C0084
		public IShapeFill Fill
		{
			get
			{
				if (true)
				{
				}
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
							goto IL_76;
						case 2:
							goto IL_5C;
						}
						if (this.ᜄ == null)
						{
							num = 2;
							continue;
						}
						goto IL_78;
					}
					IL_5C:
					this.ᜄ = new spr\u2436(base.AppImplementation, this);
					num = 0;
				}
				IL_76:
				IL_78:
				return this.ᜄ;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06001401 RID: 5121 RVA: 0x000C1110 File Offset: 0x000C0110
		public OColor ForeGroundColorObject
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
				return (this.Interior as XlsChartInterior).ForegroundColorObject;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06001402 RID: 5122 RVA: 0x000C115C File Offset: 0x000C015C
		// (set) Token: 0x06001403 RID: 5123 RVA: 0x000C11A8 File Offset: 0x000C01A8
		public Color ForeGroundColor
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
				return (this.Interior as XlsChartInterior).ForegroundColor;
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
				(this.Interior as XlsChartInterior).ForegroundColor = value;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06001404 RID: 5124 RVA: 0x000C11F4 File Offset: 0x000C01F4
		// (set) Token: 0x06001405 RID: 5125 RVA: 0x000C1240 File Offset: 0x000C0240
		public ExcelColors ForeGroundKnownColor
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
				return (this.Interior as XlsChartInterior).ForegroundKnownColor;
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
				(this.Interior as XlsChartInterior).ForegroundKnownColor = value;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x000C128C File Offset: 0x000C028C
		// (set) Token: 0x06001407 RID: 5127 RVA: 0x000C12D4 File Offset: 0x000C02D4
		public Color BackGroundColor
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
				return this.Interior.BackgroundColor;
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
				this.Interior.BackgroundColor = value;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x000C131C File Offset: 0x000C031C
		// (set) Token: 0x06001409 RID: 5129 RVA: 0x000C1364 File Offset: 0x000C0364
		public ExcelColors BackGroundKnownColor
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
				return this.Interior.BackgroundKnownColor;
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
				this.Interior.BackgroundKnownColor = value;
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x000C13AC File Offset: 0x000C03AC
		public OColor BackGroundColorObject
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
				return (this.Interior as XlsChartInterior).BackgroundColorObject;
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x000C13F8 File Offset: 0x000C03F8
		// (set) Token: 0x0600140C RID: 5132 RVA: 0x000C1440 File Offset: 0x000C0440
		public ExcelPatternType Pattern
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
				return this.Interior.Pattern;
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
				this.Interior.Pattern = value;
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x000C1488 File Offset: 0x000C0488
		// (set) Token: 0x0600140E RID: 5134 RVA: 0x000C14D0 File Offset: 0x000C04D0
		public bool IsAutomaticFormat
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
				return this.Interior.UseDefaultFormat;
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
				this.Interior.UseDefaultFormat = value;
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x000C1518 File Offset: 0x000C0518
		// (set) Token: 0x06001410 RID: 5136 RVA: 0x000C1564 File Offset: 0x000C0564
		public bool Visible
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
				return this.Interior.Pattern != ExcelPatternType.None;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_A2;
					case 2:
						if (this.Interior.Pattern == ExcelPatternType.None)
						{
							num = 0;
							continue;
						}
						return;
					case 3:
						goto IL_82;
					case 4:
						goto IL_6B;
					}
					if (!value)
					{
						this.Interior.Pattern = ExcelPatternType.None;
						num = 4;
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
					IL_82:
					num = 2;
				}
				IL_6B:
				if (true)
				{
				}
				return;
				IL_A2:
				this.Interior.Pattern = ExcelPatternType.Solid;
			}
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x000C1618 File Offset: 0x000C0618
		public XlsChartDropBar Clone(object parent)
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
			XlsChartDropBar xlsChartDropBar = (XlsChartDropBar)base.MemberwiseClone();
			xlsChartDropBar.SetParent(parent);
			xlsChartDropBar.ᜀ();
			xlsChartDropBar.ᜀ = (sprᡘ)spr\u1CD3.ᜀ(this.ᜀ);
			xlsChartDropBar.ᜁ = (XlsChartBorder)spr\u1CD3.ᜀ(this.ᜁ, this);
			xlsChartDropBar.ᜂ = (XlsChartInterior)spr\u1CD3.ᜀ(this.ᜂ, this);
			return xlsChartDropBar;
		}

		// Token: 0x04000EBF RID: 3775
		private sprᡘ ᜀ;

		// Token: 0x04000EC0 RID: 3776
		private XlsChartBorder ᜁ;

		// Token: 0x04000EC1 RID: 3777
		private XlsChartInterior ᜂ;

		// Token: 0x04000EC2 RID: 3778
		private bool[] \u25D8\u00A0\u0099\u0085;

		// Token: 0x04000EC3 RID: 3779
		private XlsWorkbook ᜃ;

		// Token: 0x04000EC4 RID: 3780
		private spr\u2436 ᜄ;

		// Token: 0x04000EC5 RID: 3781
		private Format3D ᜅ;

		// Token: 0x04000EC6 RID: 3782
		private long[] \u2460\u00A9\u008D\u009F;

		// Token: 0x04000EC7 RID: 3783
		private ChartShadow ᜆ;
	}
}
