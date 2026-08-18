using System;
using System.Collections.Generic;
using System.Drawing;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;

namespace Spire.Xls.Core.Spreadsheet.Charts
{
	// Token: 0x0200019B RID: 411
	public class XlsChartBorder : XlsObject, IChartBorder, ICloneParent
	{
		// Token: 0x060014A0 RID: 5280 RVA: 0x000C4F08 File Offset: 0x000C3F08
		internal XlsChartBorder(spr\u1DF5 A_0, object A_1)
		{
			this.ᜁ = (spr\u22F3)spr\u175E.ᜀ(TBIFFRecord.ChartLineFormat);
			base..ctor(A_0, A_1);
			this.ᜁ = (spr\u22F3)spr\u175E.ᜀ(TBIFFRecord.ChartLineFormat);
			this.Fill = new XlsShapeFill(A_0, A_1);
			this.ᜁ();
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x000C4F5C File Offset: 0x000C3F5C
		internal XlsChartBorder(spr\u1DF5 A_0, object A_1, spr\u22F3 A_2)
		{
			int a_ = 11;
			this.ᜁ = (spr\u22F3)spr\u175E.ᜀ(TBIFFRecord.ChartLineFormat);
			base..ctor(A_0, A_1);
			if (A_2 == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("ⵀ⩂⭄≆", a_));
			}
			this.ᜁ = A_2;
			this.ᜁ();
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x000C4FB8 File Offset: 0x000C3FB8
		internal XlsChartBorder(spr\u1DF5 A_0, object A_1, IList<BiffRecordRaw> A_2, ref int A_3)
		{
			this.ᜁ = (spr\u22F3)spr\u175E.ᜀ(TBIFFRecord.ChartLineFormat);
			base..ctor(A_0, A_1);
			this.ᜀ(A_2, ref A_3);
			this.ᜁ();
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x000C4FF4 File Offset: 0x000C3FF4
		internal void ᜀ(IList<BiffRecordRaw> A_0, ref int A_1)
		{
			int a_ = 16;
			while (A_0 != null)
			{
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
					BiffRecordRaw biffRecordRaw = A_0[A_1];
					biffRecordRaw.CheckTypeCode(TBIFFRecord.ChartLineFormat);
					this.ᜁ = (spr\u22F3)biffRecordRaw;
					A_1++;
					return;
				}
				}
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("≅⥇㹉ⵋ", a_));
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x000C5078 File Offset: 0x000C4078
		internal void ᜀ(IList<IRecordStorage> A_0)
		{
			int a_ = 4;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_38;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						this.ᜀ();
						A_0.Add((IRecordStorage)this.ᜁ.Clone());
						num = 4;
						continue;
					}
					break;
				case 3:
					if (this.ᜁ != null)
					{
						num = 2;
						continue;
					}
					return;
				case 4:
					return;
				}
				if (A_0 == null)
				{
					num = 1;
				}
				else
				{
					num = 3;
				}
			}
			IL_38:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䠹夻崽⼿ぁ⁃㕅", a_));
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x000C5148 File Offset: 0x000C4148
		private void ᜁ()
		{
			int a_ = 6;
			this.ᜂ = (XlsWorkbook)base.FindParent(typeof(XlsWorkbook));
			this.ᜃ = (XlsChartSerieDataFormat)base.FindParent(typeof(XlsChartSerieDataFormat));
			if (this.ᜂ == null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_8B;
				}
				if (false)
				{
				}
				if (true)
				{
				}
				throw new ApplicationException(RecordTableEnumerator.b("氻弽㈿❁⩃㉅桇╉⹋⑍㕏ㅑ⁓癕㭗㭙㉛そཟᙡ䑣ѥ൧䩩੫ŭկᱱၳ塵", a_));
			}
			IL_8B:
			this.ᜄ = new OColor((ExcelColors)this.ᜁ.ᜂ());
			this.ᜄ.AfterChange += this.ᜀ;
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x000C5210 File Offset: 0x000C4210
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
			this.ᜁ.ᜀ((ushort)this.ᜄ.ᜂ(this.ᜂ));
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x060014A7 RID: 5287 RVA: 0x000C5268 File Offset: 0x000C4268
		// (set) Token: 0x060014A8 RID: 5288 RVA: 0x000C52B4 File Offset: 0x000C42B4
		public Color Color
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
				return this.ᜄ.ᜁ(this.ᜂ);
			}
			set
			{
				int num = 8;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 6;
						continue;
					case 1:
						if (this.ᜃ != null)
						{
							num = 4;
							continue;
						}
						return;
					case 2:
						if (this.UseDefaultFormat)
						{
							num = 3;
							continue;
						}
						return;
					case 3:
						goto IL_4C;
					case 4:
						this.ᜃ.ClearOnPropertyChange();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							num = 7;
							continue;
						}
						break;
					case 5:
						num = 2;
						continue;
					case 6:
						if (value.ToArgb() == this.ᜄ.Value)
						{
							num = 5;
							continue;
						}
						goto IL_4C;
					case 7:
						return;
					}
					if (this.ᜄ.ColorType == ColorType.RGB)
					{
						num = 0;
						continue;
					}
					IL_4C:
					this.UseDefaultFormat = false;
					this.ᜄ.ᜀ(value, this.ᜂ);
					this.ᜁ.ᜂ(false);
					if (true)
					{
					}
					num = 1;
				}
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x000C53E8 File Offset: 0x000C43E8
		// (set) Token: 0x060014AA RID: 5290 RVA: 0x000C5430 File Offset: 0x000C4430
		public ChartLinePatternType Pattern
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
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_B5;
					case 2:
						if (value == this.Pattern)
						{
							num = 4;
							continue;
						}
						goto IL_66;
					case 3:
						return;
					case 4:
						num = 8;
						continue;
					case 5:
						if (this.ᜃ != null)
						{
							num = 9;
							continue;
						}
						goto IL_E8;
					case 6:
						goto IL_66;
					case 7:
						goto IL_E8;
					case 8:
						if (this.UseDefaultFormat)
						{
							num = 6;
							continue;
						}
						return;
					case 9:
						this.ᜃ.ClearOnPropertyChange();
						num = 7;
						continue;
					}
					if (value != this.Pattern)
					{
						num = 1;
						continue;
					}
					break;
					IL_66:
					this.ᜁ.ᜀ(value);
					this.UseDefaultFormat = false;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_B5:
						if (true)
						{
						}
						num = 2;
						continue;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					IL_E8:
					this.HasLineProperties = true;
					num = 3;
				}
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x060014AB RID: 5291 RVA: 0x000C555C File Offset: 0x000C455C
		// (set) Token: 0x060014AC RID: 5292 RVA: 0x000C55A4 File Offset: 0x000C45A4
		public ChartLineWeightType Weight
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
				return this.ᜁ.ᜅ();
			}
			set
			{
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						if (true)
						{
						}
						num = 3;
						continue;
					case 2:
						if (this.ᜃ != null)
						{
							num = 6;
							continue;
						}
						return;
					case 3:
						if (this.UseDefaultFormat)
						{
							goto IL_C1;
						}
						return;
					case 4:
						goto IL_57;
					case 6:
						this.ᜃ.ClearOnPropertyChange();
						num = 0;
						continue;
					}
					if (value == this.Weight)
					{
						num = 1;
						continue;
					}
					IL_57:
					this.ᜁ.ᜀ(value);
					this.UseDefaultFormat = false;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_C1:
						num = 4;
						break;
					default:
						if (false)
						{
						}
						num = 2;
						break;
					}
				}
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x060014AD RID: 5293 RVA: 0x000C568C File Offset: 0x000C468C
		// (set) Token: 0x060014AE RID: 5294 RVA: 0x000C56D0 File Offset: 0x000C46D0
		internal spr\u1C26 Fill
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
				return this.ᜆ;
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
				this.ᜆ = value;
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x060014AF RID: 5295 RVA: 0x000C5714 File Offset: 0x000C4714
		internal bool HasGradientFill
		{
			get
			{
				if (this.ᜆ != null)
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
						return this.ᜆ.FillType == ShapeFillType.Gradient;
					}
				}
				return false;
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x060014B0 RID: 5296 RVA: 0x000C576C File Offset: 0x000C476C
		// (set) Token: 0x060014B1 RID: 5297 RVA: 0x000C57B0 File Offset: 0x000C47B0
		internal bool HasLineProperties
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
				return this.ᜉ;
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
				this.ᜉ = value;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x060014B2 RID: 5298 RVA: 0x000C57F4 File Offset: 0x000C47F4
		// (set) Token: 0x060014B3 RID: 5299 RVA: 0x000C5838 File Offset: 0x000C4838
		internal XLSXBorderJoinType JoinType
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
				return this.ᜇ;
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
				this.ᜇ = value;
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x060014B4 RID: 5300 RVA: 0x000C587C File Offset: 0x000C487C
		// (set) Token: 0x060014B5 RID: 5301 RVA: 0x000C58C4 File Offset: 0x000C48C4
		public bool UseDefaultFormat
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
				return this.ᜁ.ᜉ();
			}
			set
			{
				int num = 16;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (!this.ᜃ.ParentXlsChart.TypeChanging)
						{
							num = 15;
							continue;
						}
						goto IL_1DD;
					case 1:
						goto IL_6D;
					case 2:
						goto IL_6D;
					case 3:
					{
						int num2;
						if (num2 != -1)
						{
							num = 9;
							continue;
						}
						goto IL_6D;
					}
					case 4:
						if (this.ᜃ != null)
						{
							num = 12;
							continue;
						}
						goto IL_6D;
					case 5:
						goto IL_188;
					case 6:
						this.ᜁ.ᜀ(ChartLineWeightType.Hairline);
						this.ᜁ.ᜀ(ChartLinePatternType.Solid);
						this.UseDefaultLineColor = true;
						num = 1;
						continue;
					case 7:
						if (!this.ᜃ.ParentXlsChart.TypeChanging)
						{
							num = 14;
							continue;
						}
						goto IL_6D;
					case 8:
						if (this.ᜃ != null)
						{
							num = 13;
							continue;
						}
						goto IL_1DD;
					case 9:
					{
						int num2;
						this.ᜁ.ᜀ((ushort)num2);
						this.ᜁ.ᜂ(false);
						num = 2;
						continue;
					}
					case 10:
						if (value)
						{
							num = 6;
							continue;
						}
						num = 4;
						continue;
					case 11:
						this.ᜁ.ᜀ(value);
						num = 10;
						continue;
					case 12:
						num = 7;
						continue;
					case 13:
						num = 0;
						continue;
					case 14:
					{
						int num2 = this.ᜃ.UpdateLineColor();
						num = 3;
						continue;
					}
					case 15:
						this.ᜃ.ClearOnPropertyChange();
						num = 5;
						continue;
					}
					if (this.UseDefaultFormat != value)
					{
						num = 11;
						continue;
					}
					break;
					IL_6D:
					num = 8;
				}
				IL_188:
				IL_1DD:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_188;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					return;
				}
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x060014B6 RID: 5302 RVA: 0x000C5AD4 File Offset: 0x000C4AD4
		// (set) Token: 0x060014B7 RID: 5303 RVA: 0x000C5B1C File Offset: 0x000C4B1C
		public bool DrawTickLabels
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
				this.ᜁ.ᜁ(value);
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x000C5B64 File Offset: 0x000C4B64
		// (set) Token: 0x060014B9 RID: 5305 RVA: 0x000C5BAC File Offset: 0x000C4BAC
		public bool UseDefaultLineColor
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
				for (;;)
				{
					this.ᜁ.ᜂ(value);
					int num = 2;
					for (;;)
					{
						if (true)
						{
						}
						switch (num)
						{
						case 0:
							return;
						case 1:
							this.ᜃ.ClearOnPropertyChange();
							num = 0;
							continue;
						case 2:
							if (value)
							{
								num = 5;
								continue;
							}
							goto IL_5E;
						case 3:
							if (this.ᜃ == null)
							{
								return;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_A4;
							default:
								if (false)
								{
								}
								num = 1;
								continue;
							}
							break;
						case 4:
							goto IL_5E;
						case 5:
							this.ᜁ.ᜀ(77);
							goto IL_A4;
						}
						break;
						IL_5E:
						num = 3;
						continue;
						IL_A4:
						num = 4;
					}
				}
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x060014BA RID: 5306 RVA: 0x000C5C74 File Offset: 0x000C4C74
		// (set) Token: 0x060014BB RID: 5307 RVA: 0x000C5CC0 File Offset: 0x000C4CC0
		public ExcelColors KnownColor
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
				return this.ᜄ.ᜂ(this.ᜂ);
			}
			set
			{
				int num = 7;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜃ.ClearOnPropertyChange();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_114;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 1:
						if (this.ᜃ != null)
						{
							num = 0;
							continue;
						}
						goto IL_114;
					case 2:
						if (this.UseDefaultFormat)
						{
							num = 8;
							continue;
						}
						goto IL_114;
					case 3:
						goto IL_112;
					case 4:
						num = 6;
						continue;
					case 5:
						num = 2;
						continue;
					case 6:
						if (this.KnownColor == value)
						{
							num = 5;
							continue;
						}
						goto IL_4B;
					case 8:
						goto IL_4B;
					}
					if (this.ᜄ.ColorType == ColorType.Known)
					{
						num = 4;
						continue;
					}
					IL_4B:
					value = XlsChartFrameFormat.ᜀ(value);
					this.UseDefaultFormat = false;
					this.ᜄ.SetKnownColor(value);
					this.ᜁ.ᜂ(false);
					num = 1;
				}
				IL_112:
				IL_114:
				if (true)
				{
				}
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x000C5DEC File Offset: 0x000C4DEC
		internal OColor OColor
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
				return this.ᜄ;
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x060014BD RID: 5309 RVA: 0x000C5E30 File Offset: 0x000C4E30
		// (set) Token: 0x060014BE RID: 5310 RVA: 0x000C5E74 File Offset: 0x000C4E74
		public double Transparency
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
				return this.ᜅ;
			}
			set
			{
				int a_ = 3;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (value > 1.0)
						{
							num = 3;
							continue;
						}
						goto IL_95;
					case 2:
						if (true)
						{
						}
						num = 0;
						continue;
					case 3:
						goto IL_93;
					}
					if (value < 0.0)
					{
						break;
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
						num = 2;
						break;
					}
				}
				IL_5B:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("洸䤺尼儾㉀㍂⑄㕆ⱈ╊⹌㙎煐㩒♔睖㙘⹚⥜罞๠բ䕤ᕦࡨժ੬੮", a_));
				IL_93:
				goto IL_5B;
				IL_95:
				this.ᜅ = value;
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x060014BF RID: 5311 RVA: 0x000C5F28 File Offset: 0x000C4F28
		// (set) Token: 0x060014C0 RID: 5312 RVA: 0x000C5F6C File Offset: 0x000C4F6C
		internal string LineWeightString
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
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜈ = value;
			}
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x000C5FB0 File Offset: 0x000C4FB0
		public XlsChartBorder Clone(object parent)
		{
			int a_ = 10;
			if (true)
			{
			}
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
			XlsChartBorder xlsChartBorder = (XlsChartBorder)base.MemberwiseClone();
			xlsChartBorder.ᜁ = (spr\u22F3)spr\u1CD3.ᜀ(this.ᜁ);
			xlsChartBorder.SetParent(parent);
			xlsChartBorder.ᜁ();
			xlsChartBorder.ᜄ = this.ᜄ.ᜀ();
			return xlsChartBorder;
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x000C6050 File Offset: 0x000C5050
		internal void ᜃ()
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
			this.UseDefaultLineColor = false;
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x000C6094 File Offset: 0x000C5094
		object ICloneParent.Clone(object parent)
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
			return this.Clone(parent);
		}

		// Token: 0x04000EF7 RID: 3831
		private const ExcelColors ᜀ = (ExcelColors)77;

		// Token: 0x04000EF8 RID: 3832
		private spr\u22F3 ᜁ;

		// Token: 0x04000EF9 RID: 3833
		private XlsWorkbook ᜂ;

		// Token: 0x04000EFA RID: 3834
		private XlsChartSerieDataFormat ᜃ;

		// Token: 0x04000EFB RID: 3835
		private OColor ᜄ;

		// Token: 0x04000EFC RID: 3836
		private double ᜅ;

		// Token: 0x04000EFD RID: 3837
		private long \u25D9\u00AD\u0091\u008D;

		// Token: 0x04000EFE RID: 3838
		private spr\u1C26 ᜆ;

		// Token: 0x04000EFF RID: 3839
		private XLSXBorderJoinType ᜇ;

		// Token: 0x04000F00 RID: 3840
		private string ᜈ;

		// Token: 0x04000F01 RID: 3841
		private bool ᜉ;
	}
}
