using System;
using System.Collections.Generic;
using System.Drawing;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Charts
{
	// Token: 0x0200018F RID: 399
	public class XlsChartInterior : XlsObject, IChartInterior, ICloneParent
	{
		// Token: 0x060013D2 RID: 5074 RVA: 0x000BFA20 File Offset: 0x000BEA20
		static XlsChartInterior()
		{
			for (;;)
			{
				IL_18:
				XlsChartInterior.ᜅ = new Dictionary<ExcelPatternType, GradientPatternType>(18);
				XlsChartInterior.ᜅ.Add(ExcelPatternType.Percent50, GradientPatternType.Pat50Percent);
				XlsChartInterior.ᜅ.Add(ExcelPatternType.Percent70, GradientPatternType.Pat70Percent);
				XlsChartInterior.ᜅ.Add(ExcelPatternType.Percent25, GradientPatternType.Pat25Percent);
				XlsChartInterior.ᜅ.Add(ExcelPatternType.Percent60, GradientPatternType.Pat30Percent);
				XlsChartInterior.ᜅ.Add(ExcelPatternType.Percent10, GradientPatternType.Pat20Percent);
				XlsChartInterior.ᜅ.Add(ExcelPatternType.Percent05, GradientPatternType.Pat10Percent);
				int num = 5;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_E7:
					goto IL_A3;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num2 = 1;
					break;
				}
				for (;;)
				{
					IL_02:
					switch (num2)
					{
					case 0:
						return;
					case 1:
						goto IL_A1;
					case 2:
						if (num >= 16)
						{
							num2 = 0;
							continue;
						}
						XlsChartInterior.ᜅ.Add((ExcelPatternType)num, num + GradientPatternType.Pat60Percent);
						num++;
						num2 = 3;
						continue;
					case 3:
						goto IL_E7;
					}
					goto IL_18;
				}
				IL_A1:
				IL_A3:
				num2 = 2;
				goto IL_02;
			}
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x000BFB18 File Offset: 0x000BEB18
		internal XlsChartInterior(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜀ = (sprᨓ)spr\u175E.ᜀ(TBIFFRecord.ChartAreaFormat);
			this.ᜂ();
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x000BFB48 File Offset: 0x000BEB48
		internal XlsChartInterior(spr\u1DF5 A_0, object A_1, sprᨓ A_2)
		{
			int a_ = 4;
			base..ctor(A_0, A_1);
			if (A_2 == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("嬹主嬽ℿ", a_));
			}
			this.ᜀ = A_2;
			this.ᜂ();
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x000BFB8C File Offset: 0x000BEB8C
		internal XlsChartInterior(spr\u1DF5 A_0, object A_1, IList<BiffRecordRaw> A_2, ref int A_3) : base(A_0, A_1)
		{
			this.ᜀ(A_2, ref A_3);
			this.ᜂ();
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x000BFBB0 File Offset: 0x000BEBB0
		private void ᜂ()
		{
			int a_ = 0;
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
				this.ᜁ = (XlsWorkbook)base.FindParent(typeof(XlsWorkbook));
				this.ᜂ = (base.FindParent(typeof(XlsChartSerieDataFormat)) as XlsChartSerieDataFormat);
				if (this.ᜁ == null)
				{
					throw new ApplicationException(RecordTableEnumerator.b("昵夷䠹夻倽㐿扁⭃⑅≇⽉⽋㩍灏ㅑ㕓㡕㙗㕙⡛繝ɟݡ䑣eݧὩɫ੭幯", a_));
				}
				break;
			}
			this.ᜃ = new OColor(this.ᜀ.ᜄ());
			this.ᜃ.AfterChange += this.ᜁ;
			this.ᜄ = new OColor(this.ᜀ.ᜂ());
			this.ᜄ.AfterChange += this.ᜀ;
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x000BFCA4 File Offset: 0x000BECA4
		internal void ᜀ(IList<BiffRecordRaw> A_0, ref int A_1)
		{
			int a_ = 8;
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
					if (true)
					{
					}
					throw new ArgumentNullException(RecordTableEnumerator.b("娽ℿ㙁╃", a_));
				}
			}
			BiffRecordRaw biffRecordRaw = A_0[A_1];
			biffRecordRaw.CheckTypeCode(TBIFFRecord.ChartAreaFormat);
			this.ᜀ = (sprᨓ)biffRecordRaw;
			A_1++;
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x000BFD28 File Offset: 0x000BED28
		internal void ᜀ(IList<IRecordStorage> A_0)
		{
			int a_ = 0;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_98:
				if (true)
				{
				}
				A_0.Add((BiffRecordRaw)this.ᜀ.Clone());
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
					return;
				case 1:
					if (this.ᜀ != null)
					{
						num = 4;
						continue;
					}
					return;
				case 3:
					goto IL_5E;
				case 4:
					goto IL_98;
				}
				if (A_0 == null)
				{
					num = 3;
				}
				else
				{
					num = 1;
				}
			}
			IL_5E:
			throw new ArgumentNullException(RecordTableEnumerator.b("䐵崷夹医䰽␿ㅁ", a_));
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x000BFDEC File Offset: 0x000BEDEC
		public OColor ForegroundColorObject
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
				return this.ᜃ;
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x060013DA RID: 5082 RVA: 0x000BFE30 File Offset: 0x000BEE30
		public OColor BackgroundColorObject
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
				return this.ᜄ;
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x060013DB RID: 5083 RVA: 0x000BFE74 File Offset: 0x000BEE74
		// (set) Token: 0x060013DC RID: 5084 RVA: 0x000BFEC0 File Offset: 0x000BEEC0
		public Color ForegroundColor
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
				return this.ᜃ.ᜁ(this.ᜁ);
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
				this.ᜃ.ᜀ(value, this.ᜁ);
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x060013DD RID: 5085 RVA: 0x000BFF10 File Offset: 0x000BEF10
		// (set) Token: 0x060013DE RID: 5086 RVA: 0x000BFF5C File Offset: 0x000BEF5C
		public Color BackgroundColor
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
				return this.ᜄ.ᜁ(this.ᜁ);
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
				this.ᜄ.ᜀ(value, this.ᜁ);
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x000BFFAC File Offset: 0x000BEFAC
		// (set) Token: 0x060013E0 RID: 5088 RVA: 0x000C0000 File Offset: 0x000BF000
		public ExcelPatternType Pattern
		{
			get
			{
				if (true)
				{
				}
				if (!this.UseDefaultFormat)
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
						return this.ᜀ.ᜁ();
					}
				}
				return ExcelPatternType.Solid;
			}
			set
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
					{
						IShapeFill shapeFill = (base.Parent as spr\u218E).ᜈ();
						num = 8;
						continue;
					}
					case 2:
						num = 4;
						continue;
					case 4:
						if (this.Pattern > ExcelPatternType.Solid)
						{
							num = 6;
							continue;
						}
						goto IL_D5;
					case 5:
						goto IL_D5;
					case 6:
					{
						IShapeFill shapeFill;
						shapeFill.Solid();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_C8;
						default:
							if (false)
							{
							}
							num = 5;
							continue;
						}
						break;
					}
					case 7:
						if (true)
						{
						}
						goto IL_D5;
					case 8:
					{
						if (value < ExcelPatternType.Percent50)
						{
							goto IL_C8;
						}
						IShapeFill shapeFill;
						shapeFill.Patterned(XlsChartInterior.ᜅ[value]);
						num = 7;
						continue;
					}
					}
					if (this.Pattern != value)
					{
						num = 1;
						continue;
					}
					break;
					IL_C8:
					num = 2;
					continue;
					IL_D5:
					this.UseDefaultFormat = false;
					this.ᜀ.ᜀ(value);
					num = 0;
				}
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x000C0128 File Offset: 0x000BF128
		// (set) Token: 0x060013E2 RID: 5090 RVA: 0x000C0174 File Offset: 0x000BF174
		public ExcelColors ForegroundKnownColor
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
				return this.ᜃ.ᜂ(this.ᜁ);
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
				this.ᜃ.SetKnownColor(value);
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x060013E3 RID: 5091 RVA: 0x000C01BC File Offset: 0x000BF1BC
		// (set) Token: 0x060013E4 RID: 5092 RVA: 0x000C0208 File Offset: 0x000BF208
		public ExcelColors BackgroundKnownColor
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
				return this.ᜄ.ᜂ(this.ᜁ);
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
				this.ᜄ.SetKnownColor(value);
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x060013E5 RID: 5093 RVA: 0x000C0250 File Offset: 0x000BF250
		// (set) Token: 0x060013E6 RID: 5094 RVA: 0x000C0298 File Offset: 0x000BF298
		public bool UseDefaultFormat
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
				return this.ᜀ.ᜅ();
			}
			set
			{
				int num = 4;
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
							this.ᜀ.ᜀ(ExcelPatternType.Solid);
							goto IL_7C;
						case 1:
							if (!value)
							{
								num = 6;
								continue;
							}
							return;
						case 2:
							return;
						case 3:
							this.ᜀ.ᜁ(value);
							num = 1;
							continue;
						case 5:
							if (this.ᜀ.ᜁ() == ExcelPatternType.None)
							{
								num = 0;
								continue;
							}
							return;
						case 6:
							num = 5;
							continue;
						}
						if (value != this.UseDefaultFormat)
						{
							if (true)
							{
							}
							num = 3;
							continue;
						}
						return;
					}
					IL_7C:
					num = 2;
				}
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x060013E7 RID: 5095 RVA: 0x000C0378 File Offset: 0x000BF378
		// (set) Token: 0x060013E8 RID: 5096 RVA: 0x000C03C0 File Offset: 0x000BF3C0
		public bool SwapColorsOnNegative
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
				return this.ᜀ.ᜀ();
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
				this.ᜀ.ᜀ(value);
			}
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x000C0408 File Offset: 0x000BF408
		private void ᜁ()
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
			this.ᜀ.ᜁ(this.ForegroundKnownColor);
			this.ᜀ.ᜀ(this.ForegroundColor.ToArgb() & 16777215);
			this.UseDefaultFormat = false;
			(base.Parent as spr\u218E).ᜁ(true);
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x000C048C File Offset: 0x000BF48C
		private void ᜀ()
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
			this.ᜀ.ᜀ(this.BackgroundKnownColor);
			this.ᜀ.ᜀ(this.BackgroundColor);
			this.UseDefaultFormat = false;
			(base.Parent as spr\u218E).ᜁ(true);
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x000C0504 File Offset: 0x000BF504
		public void InitForFrameFormat(bool bIsAutoSize, bool bIs3DChart, bool bIsInteriorGray)
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
			this.InitForFrameFormat(bIsAutoSize, bIs3DChart, bIsInteriorGray, false);
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x000C054C File Offset: 0x000BF54C
		public void InitForFrameFormat(bool bIsAutoSize, bool bIs3DChart, bool bIsInteriorGray, bool bIsGray50)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_D6:
				num = 3;
				break;
			default:
				if (false)
				{
				}
				goto IL_42;
			}
			for (;;)
			{
				IL_28:
				switch (num)
				{
				case 0:
					this.ᜀ.ᜀ(bIsAutoSize ? ((ExcelColors)79) : ((ExcelColors)77));
					num = 2;
					continue;
				case 1:
					this.ᜀ.ᜁ(bIsInteriorGray ? ExcelColors.Gray25Percent : ExcelColors.White);
					num = 0;
					continue;
				case 2:
					if (bIsGray50)
					{
						if (true)
						{
						}
						num = 4;
						continue;
					}
					return;
				case 3:
					return;
				case 4:
					goto IL_C7;
				}
				goto IL_42;
			}
			IL_C7:
			this.ᜀ.ᜁ(ExcelColors.Gray50Percent);
			goto IL_D6;
			IL_42:
			this.ᜀ.ᜀ(ExcelPatternType.Solid);
			this.ᜀ.ᜁ(bIs3DChart);
			this.ᜀ.ᜀ(false);
			num = 1;
			goto IL_28;
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x000C063C File Offset: 0x000BF63C
		public XlsChartInterior Clone(object parent)
		{
			int a_ = 10;
			if (parent == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("〿⍁㙃⍅♇㹉", a_));
				}
			}
			if (true)
			{
			}
			XlsChartInterior xlsChartInterior = (XlsChartInterior)base.MemberwiseClone();
			xlsChartInterior.ᜀ = (sprᨓ)spr\u1CD3.ᜀ(this.ᜀ);
			xlsChartInterior.SetParent(parent);
			xlsChartInterior.ᜂ();
			return xlsChartInterior;
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x000C06CC File Offset: 0x000BF6CC
		object ICloneParent.Clone(object parent)
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
			return this.Clone(parent);
		}

		// Token: 0x04000EB4 RID: 3764
		private sprᨓ ᜀ;

		// Token: 0x04000EB5 RID: 3765
		private XlsWorkbook ᜁ;

		// Token: 0x04000EB6 RID: 3766
		private XlsChartSerieDataFormat ᜂ;

		// Token: 0x04000EB7 RID: 3767
		private OColor ᜃ;

		// Token: 0x04000EB8 RID: 3768
		private OColor ᜄ;

		// Token: 0x04000EB9 RID: 3769
		private byte[] \u2460\u0097\u0085\u0094;

		// Token: 0x04000EBA RID: 3770
		private long[] \u2460\u00A8\u0092\u00A9;

		// Token: 0x04000EBB RID: 3771
		private string \u25D8\u0096\u00A3\u0094;

		// Token: 0x04000EBC RID: 3772
		private int[] \u2593\u0096\u00AF\u0088;

		// Token: 0x04000EBD RID: 3773
		private long[] \u2460\u0097\u009F\u0085;

		// Token: 0x04000EBE RID: 3774
		private static Dictionary<ExcelPatternType, GradientPatternType> ᜅ;
	}
}
