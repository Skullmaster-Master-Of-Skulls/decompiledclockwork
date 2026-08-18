using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Spire.Xls.Charts;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Charts
{
	// Token: 0x02000199 RID: 409
	public class XlsChartDataLabels : XlsObject, IChartDataLabels, spr\u1B6D, sprᮟ
	{
		// Token: 0x0600144D RID: 5197 RVA: 0x000C3388 File Offset: 0x000C2388
		internal XlsChartDataLabels(spr\u1DF5 A_0, object A_1, int A_2) : base(A_0, A_1)
		{
			this.ᜁ();
			this.ᜄ = new XlsChartWrappedTextArea(base.ReservedHandle, this);
			this.ᜄ.ObjectLink.ᜀ((ushort)A_2);
			this.ᜄ.TextRecord.ᜁ(true);
			this.ᜄ.ChartAI.ᜀ(sprᢀ.ReferenceType.EnteredDirectly);
			this.ᜈ = ChartParagraphType.None;
			XlsChartSerieDataFormat innerDataFormat = this.ᜅ.InnerDataFormat;
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x000C3400 File Offset: 0x000C2400
		private void ᜁ()
		{
			int a_ = 17;
			for (;;)
			{
				for (;;)
				{
					object[] array = base.FindParents(new Type[]
					{
						typeof(ChartSerie),
						typeof(ChartDataPoint)
					});
					this.ᜃ = (array[0] as XlsChartSerie);
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
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_97;
							case 1:
								if (this.ᜃ == null)
								{
									num = 0;
									continue;
								}
								this.ᜅ = (array[1] as XlsChartDataPoint);
								num = 3;
								continue;
							case 2:
								goto IL_F1;
							case 3:
								if (this.ᜅ == null)
								{
									num = 2;
									continue;
								}
								return;
							}
							break;
						}
						break;
					}
					}
				}
			}
			IL_97:
			throw new ArgumentNullException(RecordTableEnumerator.b("㝆⡈㥊⡌ⅎ═", a_), RecordTableEnumerator.b("ᝆ⡈㥊⡌ⅎ═獒㩔㕖㍘㹚㹜⭞䅠bѤ०ݨѪᥬ佮፰ᙲ啴ᅶᙸ๺፼᭾꾀", a_));
			IL_F1:
			throw new ArgumentNullException(RecordTableEnumerator.b("㝆⡈㥊⡌ⅎ═", a_), RecordTableEnumerator.b("ᝆ⡈㥊⡌ⅎ═獒㩔㕖㍘㹚㹜⭞䅠bѤ०ݨѪᥬ佮፰ᙲ啴ᅶᙸ๺፼᭾꾀", a_));
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x000C3524 File Offset: 0x000C2524
		// (set) Token: 0x06001450 RID: 5200 RVA: 0x000C356C File Offset: 0x000C256C
		public bool HasSeriesName
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
				return this.ᜄ.HasSeriesName;
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
				this.ᜄ.HasSeriesName = value;
				this.ᜊ = true;
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x000C35BC File Offset: 0x000C25BC
		// (set) Token: 0x06001452 RID: 5202 RVA: 0x000C3604 File Offset: 0x000C2604
		public bool HasCategoryName
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
				return this.ᜄ.HasCategoryName;
			}
			set
			{
				for (;;)
				{
					for (;;)
					{
						this.ᜄ.HasCategoryName = value;
						this.ᜋ = true;
						XlsChartSerieDataFormat format = this.Format;
						int num = 0;
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
									if (false)
									{
									}
									if (format != null)
									{
										num = 1;
										continue;
									}
									return;
								}
								break;
							case 1:
								if (true)
								{
								}
								format.AttachedLabel.ᜄ(value);
								num = 2;
								continue;
							case 2:
								return;
							}
							break;
						}
					}
				}
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06001453 RID: 5203 RVA: 0x000C369C File Offset: 0x000C269C
		// (set) Token: 0x06001454 RID: 5204 RVA: 0x000C36E4 File Offset: 0x000C26E4
		public bool HasValue
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
				return this.ᜄ.HasValue;
			}
			set
			{
				for (;;)
				{
					for (;;)
					{
						XlsChartSerieDataFormat format = this.Format;
						this.ᜉ = true;
						if (true)
						{
						}
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_79;
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
									if (format != null)
									{
										num = 2;
										continue;
									}
									goto IL_7B;
								}
								break;
							case 2:
								format.AttachedLabel.ᜃ(value);
								num = 0;
								continue;
							}
							break;
						}
					}
				}
				IL_79:
				IL_7B:
				this.ᜄ.HasValue = value;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x000C3778 File Offset: 0x000C2778
		// (set) Token: 0x06001456 RID: 5206 RVA: 0x000C37C0 File Offset: 0x000C27C0
		public bool HasPercentage
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
				return this.ᜄ.HasPercentage;
			}
			set
			{
				for (;;)
				{
					for (;;)
					{
						if (true)
						{
						}
						this.ᜄ.HasPercentage = value;
						this.ᜌ = true;
						XlsChartSerieDataFormat format = this.Format;
						int num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								format.AttachedLabel.ᜀ(true);
								num = 1;
								continue;
							case 1:
								return;
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
									if (format != null)
									{
										num = 0;
										continue;
									}
									return;
								}
								break;
							}
							break;
						}
					}
				}
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x000C3858 File Offset: 0x000C2858
		// (set) Token: 0x06001458 RID: 5208 RVA: 0x000C38A0 File Offset: 0x000C28A0
		public bool HasBubbleSize
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
				return this.ᜄ.HasBubbleSize;
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
				this.ᜄ.HasBubbleSize = value;
				this.ᜎ = true;
				XlsChartSerieDataFormat format = this.Format;
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x000C38F8 File Offset: 0x000C28F8
		// (set) Token: 0x0600145A RID: 5210 RVA: 0x000C3940 File Offset: 0x000C2940
		public string Delimiter
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
				return this.ᜄ.Delimiter;
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
				this.ᜄ.Delimiter = value;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x0600145B RID: 5211 RVA: 0x000C3988 File Offset: 0x000C2988
		// (set) Token: 0x0600145C RID: 5212 RVA: 0x000C39D0 File Offset: 0x000C29D0
		public bool HasLegendKey
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
				return this.ᜄ.HasLegendKey;
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
				this.ᜄ.HasLegendKey = value;
				this.\u170D = true;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x000C3A20 File Offset: 0x000C2A20
		// (set) Token: 0x0600145E RID: 5214 RVA: 0x000C3A70 File Offset: 0x000C2A70
		public bool ShowLeaderLines
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
				return this.ᜃ.InnerXlsChart.XlsChartFormat.IsShowLeaderLines;
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
				XlsChartFormat xlsChartFormat = this.ᜃ.InnerXlsChart.XlsChartFormat;
				xlsChartFormat.IsShowLeaderLines = value;
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x000C3AC4 File Offset: 0x000C2AC4
		// (set) Token: 0x06001460 RID: 5216 RVA: 0x000C3B0C File Offset: 0x000C2B0C
		public DataLabelPositionType Position
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
				return this.ᜄ.Position;
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
				this.ᜄ.Position = value;
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06001461 RID: 5217 RVA: 0x000C3B54 File Offset: 0x000C2B54
		// (set) Token: 0x06001462 RID: 5218 RVA: 0x000C3B9C File Offset: 0x000C2B9C
		public ChartBackgroundMode BackgroundMode
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
				return this.ᜄ.BackgroundMode;
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
				this.ᜄ.BackgroundMode = value;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06001463 RID: 5219 RVA: 0x000C3BE4 File Offset: 0x000C2BE4
		// (set) Token: 0x06001464 RID: 5220 RVA: 0x000C3C2C File Offset: 0x000C2C2C
		public bool IsAutoMode
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
				return this.ᜄ.IsAutoMode;
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
				this.ᜄ.IsAutoMode = value;
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06001465 RID: 5221 RVA: 0x000C3C74 File Offset: 0x000C2C74
		// (set) Token: 0x06001466 RID: 5222 RVA: 0x000C3CBC File Offset: 0x000C2CBC
		public string Text
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
				return this.ᜄ.Text;
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
				this.ᜄ.Text = value;
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x000C3D04 File Offset: 0x000C2D04
		// (set) Token: 0x06001468 RID: 5224 RVA: 0x000C3D4C File Offset: 0x000C2D4C
		public int TextRotationAngle
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
				return this.ᜄ.TextRotationAngle;
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
				this.ᜄ.TextRotationAngle = value;
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06001469 RID: 5225 RVA: 0x000C3D94 File Offset: 0x000C2D94
		public IChartFrameFormat FrameFormat
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
				return this.ᜄ.FrameFormat;
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x000C3DDC File Offset: 0x000C2DDC
		// (set) Token: 0x0600146B RID: 5227 RVA: 0x000C3E24 File Offset: 0x000C2E24
		public bool IsBold
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
				return this.ᜄ.IsBold;
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
				this.ᜄ.IsBold = value;
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x000C3E6C File Offset: 0x000C2E6C
		// (set) Token: 0x0600146D RID: 5229 RVA: 0x000C3EB4 File Offset: 0x000C2EB4
		public ExcelColors KnownColor
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
				return this.ᜄ.KnownColor;
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
				this.ᜄ.KnownColor = value;
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x0600146E RID: 5230 RVA: 0x000C3EFC File Offset: 0x000C2EFC
		// (set) Token: 0x0600146F RID: 5231 RVA: 0x000C3F44 File Offset: 0x000C2F44
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
				return this.ᜄ.Color;
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
				this.ᜄ.Color = value;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x000C3F8C File Offset: 0x000C2F8C
		// (set) Token: 0x06001471 RID: 5233 RVA: 0x000C3FD4 File Offset: 0x000C2FD4
		public bool IsItalic
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
				return this.ᜄ.IsItalic;
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
				this.ᜄ.IsItalic = value;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x000C401C File Offset: 0x000C301C
		// (set) Token: 0x06001473 RID: 5235 RVA: 0x000C4064 File Offset: 0x000C3064
		protected internal bool MacOSOutlineFont
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
				return this.ᜄ.MacOSOutlineFont;
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
				this.ᜄ.MacOSOutlineFont = value;
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x000C40AC File Offset: 0x000C30AC
		// (set) Token: 0x06001475 RID: 5237 RVA: 0x000C40F4 File Offset: 0x000C30F4
		protected internal bool MacOSShadow
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
				return this.ᜄ.MacOSShadow;
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
				this.ᜄ.MacOSShadow = value;
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x000C413C File Offset: 0x000C313C
		// (set) Token: 0x06001477 RID: 5239 RVA: 0x000C4184 File Offset: 0x000C3184
		public double Size
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
				return this.ᜄ.Size;
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
				this.ᜄ.Size = value;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x000C41CC File Offset: 0x000C31CC
		// (set) Token: 0x06001479 RID: 5241 RVA: 0x000C4214 File Offset: 0x000C3214
		public bool IsStrikethrough
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
				return this.ᜄ.IsStrikethrough;
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
				this.ᜄ.IsStrikethrough = value;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x0600147A RID: 5242 RVA: 0x000C425C File Offset: 0x000C325C
		// (set) Token: 0x0600147B RID: 5243 RVA: 0x000C42A4 File Offset: 0x000C32A4
		public bool IsSubscript
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
				return this.ᜄ.IsSubscript;
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
				this.ᜄ.IsSubscript = value;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x0600147C RID: 5244 RVA: 0x000C42EC File Offset: 0x000C32EC
		// (set) Token: 0x0600147D RID: 5245 RVA: 0x000C4334 File Offset: 0x000C3334
		public bool IsSuperscript
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
				return this.ᜄ.IsSuperscript;
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
				this.ᜄ.IsSuperscript = value;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x0600147E RID: 5246 RVA: 0x000C437C File Offset: 0x000C337C
		// (set) Token: 0x0600147F RID: 5247 RVA: 0x000C43C4 File Offset: 0x000C33C4
		public FontUnderlineType Underline
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
				return this.ᜄ.Underline;
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
				this.ᜄ.Underline = value;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06001480 RID: 5248 RVA: 0x000C440C File Offset: 0x000C340C
		// (set) Token: 0x06001481 RID: 5249 RVA: 0x000C4454 File Offset: 0x000C3454
		public string FontName
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
				return this.ᜄ.FontName;
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
				this.ᜄ.FontName = value;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06001482 RID: 5250 RVA: 0x000C449C File Offset: 0x000C349C
		// (set) Token: 0x06001483 RID: 5251 RVA: 0x000C44E4 File Offset: 0x000C34E4
		public FontVertialAlignmentType VerticalAlignment
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
				return this.ᜄ.VerticalAlignment;
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
				this.ᜄ.VerticalAlignment = value;
			}
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x000C452C File Offset: 0x000C352C
		public Font GenerateNativeFont()
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
			return this.ᜄ.GenerateNativeFont();
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x000C4574 File Offset: 0x000C3574
		public bool IsAutoColor
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
				return this.ᜄ.IsAutoColor;
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06001486 RID: 5254 RVA: 0x000C45BC File Offset: 0x000C35BC
		public XlsFont Font
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
				return this.ᜄ.Font;
			}
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x000C4604 File Offset: 0x000C3604
		[CLSCompliant(false)]
		public void SerializeDataToList(IList<IRecordStorage> records)
		{
			int a_ = 19;
			int num = 8;
			for (;;)
			{
				if (true)
				{
				}
				bool flag;
				bool flag2;
				switch (num)
				{
				case 0:
					this.ᜄ.TextRecord.ᜂ(false);
					num = 9;
					continue;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1A9;
					default:
						if (false)
						{
						}
						if (flag)
						{
							num = 3;
							continue;
						}
						goto IL_1A9;
					}
					break;
				case 2:
					num = 5;
					continue;
				case 3:
					this.ᜄ.TextRecord.ᜂ(true);
					num = 11;
					continue;
				case 4:
					if (flag)
					{
						num = 0;
						continue;
					}
					return;
				case 5:
					flag2 = this.HasCategoryName;
					goto IL_149;
				case 6:
					if (!this.HasValue)
					{
						num = 2;
						continue;
					}
					num = 12;
					continue;
				case 7:
					this.ᜄ.IsShowLabelPercent = (this.ᜃ.IsPie && this.HasPercentage && this.HasCategoryName && !this.HasValue && !this.HasSeriesName);
					num = 13;
					continue;
				case 9:
					return;
				case 10:
					num = 7;
					continue;
				case 11:
					goto IL_1A9;
				case 12:
					flag2 = false;
					goto IL_149;
				case 13:
					goto IL_102;
				case 14:
					goto IL_6C;
				case 15:
					if (this.ᜄ.HasDataLabels)
					{
						num = 10;
						continue;
					}
					goto IL_102;
				}
				if (records == null)
				{
					num = 14;
					continue;
				}
				num = 15;
				continue;
				IL_102:
				this.ᜀ();
				num = 6;
				continue;
				IL_149:
				flag = flag2;
				num = 1;
				continue;
				IL_1A9:
				this.ᜄ.SerializeDataToList(records);
				num = 4;
			}
			IL_6C:
			throw new ArgumentNullException(RecordTableEnumerator.b("㭈⹊⹌⁎⍐㝒♔", a_));
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x000C4800 File Offset: 0x000C3800
		private void ᜀ()
		{
			int a_ = 13;
			spr\u20F4 spr_u20F;
			XlsChartSerie xlsChartSerie;
			for (;;)
			{
				spr_u20F = this.ᜄ.ObjectLink;
				spr_u20F.ᜀ((ushort)this.ᜅ.Index);
				spr_u20F.ᜀ(ObjectTextLinkType.DataLabel);
				xlsChartSerie = (base.FindParent(typeof(XlsChartSerie)) as XlsChartSerie);
				if (xlsChartSerie == null)
				{
					break;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_7D;
				}
			}
			throw new NotImplementedException(RecordTableEnumerator.b("B⑄⥆湈㽊浌⥎㡐㵒ㅔ睖⥘㩚⽜㩞འᝢ䕤ᑦ౨ᥪѬ੮ɰ", a_));
			IL_7D:
			if (false)
			{
			}
			if (true)
			{
			}
			spr_u20F.ᜁ((ushort)xlsChartSerie.Index);
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x000C48A8 File Offset: 0x000C38A8
		// (set) Token: 0x0600148A RID: 5258 RVA: 0x000C48EC File Offset: 0x000C38EC
		public XlsChartTextArea TextArea
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
			set
			{
				int a_ = 6;
				for (;;)
				{
					if (true)
					{
					}
					if (value == null)
					{
						break;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_4A;
					}
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("䨻弽ⰿ㝁⅃", a_));
				IL_4A:
				if (false)
				{
				}
				this.ᜄ = value;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x0600148B RID: 5259 RVA: 0x000C4950 File Offset: 0x000C3950
		public XlsChartSerieDataFormat Format
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
				return this.ᜅ.InnerDataFormat;
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x000C4998 File Offset: 0x000C3998
		// (set) Token: 0x0600148D RID: 5261 RVA: 0x000C49DC File Offset: 0x000C39DC
		public Stream LayoutStream
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

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x000C4A20 File Offset: 0x000C3A20
		// (set) Token: 0x0600148F RID: 5263 RVA: 0x000C4A64 File Offset: 0x000C3A64
		internal bool IsDelete
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
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜇ = value;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x000C4AA8 File Offset: 0x000C3AA8
		public bool HasTextRotation
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
				return this.TextArea.HasTextRotation;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x000C4AF0 File Offset: 0x000C3AF0
		// (set) Token: 0x06001492 RID: 5266 RVA: 0x000C4B88 File Offset: 0x000C3B88
		public ChartParagraphType ParagraphType
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_77;
					case 1:
						for (;;)
						{
							spr\u1AA0.ᜀ(this.TextArea);
							this.ᜈ = this.TextArea.ParagraphType;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_61;
							}
						}
						IL_61:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 0;
						continue;
					}
					if (this.ᜈ == ChartParagraphType.Default)
					{
						break;
					}
					num = 1;
				}
				IL_77:
				return this.ᜈ;
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
				this.ᜈ = value;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x000C4BCC File Offset: 0x000C3BCC
		// (set) Token: 0x06001494 RID: 5268 RVA: 0x000C4C10 File Offset: 0x000C3C10
		public string NumberFormat
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
				return this.ᜏ;
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
				this.ᜏ = value;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x000C4C54 File Offset: 0x000C3C54
		// (set) Token: 0x06001496 RID: 5270 RVA: 0x000C4C98 File Offset: 0x000C3C98
		public bool HasFormula
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
				return this.ᜐ;
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
				this.ᜐ = value;
			}
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x000C4CDC File Offset: 0x000C3CDC
		public void UpdateSerieIndex()
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
			this.ᜄ.UpdateSerieIndex(this.ᜃ.Index);
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x000C4D30 File Offset: 0x000C3D30
		internal object ᜀ(object A_0, Dictionary<int, int> A_1, Dictionary<string, string> A_2)
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
			XlsChartDataLabels xlsChartDataLabels = (XlsChartDataLabels)base.MemberwiseClone();
			xlsChartDataLabels.SetParent(A_0);
			xlsChartDataLabels.ᜁ();
			xlsChartDataLabels.ᜄ = (XlsChartTextArea)this.ᜄ.Clone(xlsChartDataLabels, A_1, A_2);
			return xlsChartDataLabels;
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x000C4DA0 File Offset: 0x000C3DA0
		public void BeginUpdate()
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
			this.ᜄ.BeginUpdate();
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x000C4DE8 File Offset: 0x000C3DE8
		public void EndUpdate()
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
			this.ᜄ.EndUpdate();
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x000C4E30 File Offset: 0x000C3E30
		public OColor OColor
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
				return this.ᜄ.OColor;
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x000C4E78 File Offset: 0x000C3E78
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
				return this.ᜄ.Index;
			}
		}

		// Token: 0x04000EE0 RID: 3808
		internal const string ᜀ = "Tahoma";

		// Token: 0x04000EE1 RID: 3809
		private long[] \u2609\u008F\u00A8\u00AD;

		// Token: 0x04000EE2 RID: 3810
		internal const string ᜁ = "en-US";

		// Token: 0x04000EE3 RID: 3811
		internal const double ᜂ = 10.0;

		// Token: 0x04000EE4 RID: 3812
		private XlsChartSerie ᜃ;

		// Token: 0x04000EE5 RID: 3813
		private float \u25D8\u009C\u00A5\u0080;

		// Token: 0x04000EE6 RID: 3814
		private XlsChartTextArea ᜄ;

		// Token: 0x04000EE7 RID: 3815
		private XlsChartDataPoint ᜅ;

		// Token: 0x04000EE8 RID: 3816
		private long[] \u25D8\u00A8\u00A1\u00B0;

		// Token: 0x04000EE9 RID: 3817
		private Stream ᜆ;

		// Token: 0x04000EEA RID: 3818
		private int \u25D8\u0097\u00AF\u0094;

		// Token: 0x04000EEB RID: 3819
		private string[] \u25D9\u00A5\u0099\u00AE;

		// Token: 0x04000EEC RID: 3820
		private bool ᜇ;

		// Token: 0x04000EED RID: 3821
		private ChartParagraphType ᜈ;

		// Token: 0x04000EEE RID: 3822
		internal bool ᜉ;

		// Token: 0x04000EEF RID: 3823
		internal bool ᜊ;

		// Token: 0x04000EF0 RID: 3824
		internal bool ᜋ;

		// Token: 0x04000EF1 RID: 3825
		private long \u25D9\u00AE\u0086\u0083;

		// Token: 0x04000EF2 RID: 3826
		internal bool ᜌ;

		// Token: 0x04000EF3 RID: 3827
		internal bool \u170D;

		// Token: 0x04000EF4 RID: 3828
		internal bool ᜎ;

		// Token: 0x04000EF5 RID: 3829
		private string ᜏ;

		// Token: 0x04000EF6 RID: 3830
		private bool ᜐ;
	}
}
