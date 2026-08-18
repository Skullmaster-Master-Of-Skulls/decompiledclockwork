using System;
using System.Collections.Generic;
using System.Drawing;
using Spire.Xls.Charts;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Charts
{
	// Token: 0x020001A9 RID: 425
	public class XlsChartFormat : XlsObject, IChartFormat, ICloneParent
	{
		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06001642 RID: 5698 RVA: 0x000D6DC4 File Offset: 0x000D5DC4
		// (set) Token: 0x06001643 RID: 5699 RVA: 0x000D6E08 File Offset: 0x000D5E08
		public bool IsVeryColor
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
				return this.IsVaryColor;
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
				this.IsVaryColor = value;
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06001644 RID: 5700 RVA: 0x000D6E4C File Offset: 0x000D5E4C
		// (set) Token: 0x06001645 RID: 5701 RVA: 0x000D6E94 File Offset: 0x000D5E94
		public bool IsVaryColor
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
				return this.ChartChartFormatRecord.ᜂ();
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
				this.ChartChartFormatRecord.ᜀ(value);
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x000D6EDC File Offset: 0x000D5EDC
		internal IChartSerieDataFormat SerieDataFormat
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
				return this.DataFormat;
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06001647 RID: 5703 RVA: 0x000D6F20 File Offset: 0x000D5F20
		// (set) Token: 0x06001648 RID: 5704 RVA: 0x000D6F94 File Offset: 0x000D5F94
		public int Overlap
		{
			get
			{
				int a_ = 5;
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
					if (!this.\u171B.IsChart3D)
					{
						return this.BarRecord.ᜅ();
					}
					break;
				}
				throw new NotSupportedException(RecordTableEnumerator.b("琺䬼娾㍀⽂⑄㝆楈⡊ⱌⅎ㽐㱒⅔睖㭘㹚絜ⱞᑠ።ᕤࡦ᭨Ὢ࡬୮兰ᕲᩴն奸䡺᥼彾ﶈꖊ", a_));
			}
			set
			{
				int a_ = 13;
				int num = 7;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
						if (value >= -100)
						{
							num = 3;
							continue;
						}
						goto IL_D5;
					case 2:
						if (true)
						{
						}
						if (value > 100)
						{
							num = 6;
							continue;
						}
						goto IL_11D;
					case 3:
						goto IL_5D;
					case 4:
						if (this.\u171B.IsChart3D)
						{
							num = 8;
							continue;
						}
						goto IL_7F;
					case 5:
						if (!this.\u171B.ParentWorkbook.Loading)
						{
							num = 0;
							continue;
						}
						goto IL_5D;
					case 6:
						goto IL_7D;
					case 8:
						goto IL_D3;
					case 9:
						num = 4;
						continue;
					}
					if (!this.\u171B.ParentWorkbook.Loading)
					{
						num = 9;
						continue;
					}
					goto IL_7F;
					IL_5D:
					num = 2;
					continue;
					IL_7F:
					num = 5;
				}
				IL_7D:
				goto IL_D5;
				IL_D3:
				throw new NotSupportedException(RecordTableEnumerator.b("ూ㍄≆㭈❊ⱌ㽎煐げ㑔㥖㝘㑚⥜罞͠٢䕤ᑦᱨ᭪ᵬnͰݲၴ፶奸ᵺቼൾꆀ낂Ꞇﶎ붒", a_));
				IL_D5:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㕂⑄⭆㱈⹊", a_));
				IL_11D:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_D5;
				default:
					if (false)
					{
					}
					this.BarRecord.ᜀ(value);
					return;
				}
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06001649 RID: 5705 RVA: 0x000D70E8 File Offset: 0x000D60E8
		// (set) Token: 0x0600164A RID: 5706 RVA: 0x000D7150 File Offset: 0x000D6150
		public int GapWidth
		{
			get
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
					if (this.\u1712.TypeCode == TBIFFRecord.ChartBar)
					{
						return (int)this.BarRecord.ᜄ();
					}
					break;
				}
				if (true)
				{
				}
				return (int)this.BoppopRecord.ᜈ();
			}
			set
			{
				int a_ = 5;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						num = 3;
						continue;
					case 2:
						num = 7;
						continue;
					case 3:
						if (value >= 0)
						{
							num = 4;
							continue;
						}
						goto IL_122;
					case 4:
						goto IL_B2;
					case 5:
						goto IL_120;
					case 6:
						goto IL_D0;
					case 7:
						if (value > 200)
						{
							num = 5;
							continue;
						}
						goto IL_136;
					case 8:
						if (value >= 5)
						{
							num = 2;
							continue;
						}
						goto IL_D2;
					case 9:
						if (value > 500)
						{
							num = 6;
							continue;
						}
						goto IL_A4;
					}
					if (this.\u1712.TypeCode != TBIFFRecord.ChartBar)
					{
						num = 8;
						continue;
					}
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
						num = 1;
						continue;
					}
					IL_B2:
					num = 9;
				}
				IL_A4:
				this.BarRecord.ᜀ((ushort)value);
				return;
				IL_D0:
				goto IL_122;
				IL_D2:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("簺尼伾ᙀ⩂⅄㍆ⅈ", a_));
				IL_120:
				goto IL_D2;
				IL_122:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("簺尼伾ᙀ⩂⅄㍆ⅈ", a_));
				IL_136:
				this.BoppopRecord.ᜂ((ushort)value);
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x000D72A0 File Offset: 0x000D62A0
		// (set) Token: 0x0600164C RID: 5708 RVA: 0x000D72E8 File Offset: 0x000D62E8
		public bool IsHorizontalBar
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
				return this.BarRecord.ᜆ();
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
				this.BarRecord.ᜀ(value);
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x000D7330 File Offset: 0x000D6330
		// (set) Token: 0x0600164E RID: 5710 RVA: 0x000D7378 File Offset: 0x000D6378
		public bool StackValuesBar
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
				return this.BarRecord.ᜁ();
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
				this.BarRecord.ᜂ(value);
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x000D73C0 File Offset: 0x000D63C0
		// (set) Token: 0x06001650 RID: 5712 RVA: 0x000D7408 File Offset: 0x000D6408
		public bool ShowAsPercentsBar
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
				return this.BarRecord.ᜂ();
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
				this.BarRecord.ᜃ(value);
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06001651 RID: 5713 RVA: 0x000D7450 File Offset: 0x000D6450
		// (set) Token: 0x06001652 RID: 5714 RVA: 0x000D7498 File Offset: 0x000D6498
		public bool HasShadowBar
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
				return this.BarRecord.ᜀ();
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
				this.BarRecord.ᜁ(value);
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06001653 RID: 5715 RVA: 0x000D74E0 File Offset: 0x000D64E0
		// (set) Token: 0x06001654 RID: 5716 RVA: 0x000D7528 File Offset: 0x000D6528
		public bool StackValuesLine
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
				return this.LineRecord.ᜀ();
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
				this.LineRecord.ᜁ(value);
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06001655 RID: 5717 RVA: 0x000D7570 File Offset: 0x000D6570
		// (set) Token: 0x06001656 RID: 5718 RVA: 0x000D75B8 File Offset: 0x000D65B8
		public bool ShowAsPercentsLine
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
				return this.LineRecord.ᜁ();
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
				this.LineRecord.ᜂ(value);
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06001657 RID: 5719 RVA: 0x000D7600 File Offset: 0x000D6600
		// (set) Token: 0x06001658 RID: 5720 RVA: 0x000D7648 File Offset: 0x000D6648
		public bool HasShadowLine
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
				return this.LineRecord.ᜂ();
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
				this.LineRecord.ᜀ(value);
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06001659 RID: 5721 RVA: 0x000D7690 File Offset: 0x000D6690
		// (set) Token: 0x0600165A RID: 5722 RVA: 0x000D76D8 File Offset: 0x000D66D8
		public int FirstSliceAngle
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
				return (int)this.PieRecord.ᜁ();
			}
			set
			{
				int a_ = 2;
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
							goto IL_79;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 1:
						goto IL_89;
					case 2:
						goto IL_79;
					case 3:
						num = 2;
						continue;
					}
					if (value >= 0)
					{
						num = 3;
						continue;
					}
					break;
					IL_79:
					if (value <= 360)
					{
						goto IL_8B;
					}
					num = 1;
				}
				IL_5D:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("欷丹崻䰽㐿́⩃ⅅ⑇⽉", a_));
				IL_89:
				goto IL_5D;
				IL_8B:
				if (true)
				{
				}
				this.PieRecord.ᜂ((ushort)value);
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x0600165B RID: 5723 RVA: 0x000D7788 File Offset: 0x000D6788
		// (set) Token: 0x0600165C RID: 5724 RVA: 0x000D77D0 File Offset: 0x000D67D0
		public int DoughnutHoleSize
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
				return (int)this.PieRecord.ᜆ();
			}
			set
			{
				int a_ = 9;
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_E1;
					case 1:
					{
						ExcelChartType chartType = this.\u171B.ChartType;
						num = 2;
						continue;
					}
					case 2:
					{
						ExcelChartType chartType;
						if (chartType != ExcelChartType.Doughnut)
						{
							num = 4;
							continue;
						}
						goto IL_137;
					}
					case 3:
						num = 9;
						continue;
					case 4:
						goto IL_81;
					case 6:
					{
						ExcelChartType chartType;
						if (chartType != ExcelChartType.DoughnutExploded)
						{
							num = 7;
							continue;
						}
						goto IL_137;
					}
					case 7:
						goto IL_9C;
					case 8:
						if (!this.\u171B.TypeChanging)
						{
							num = 1;
							continue;
						}
						goto IL_137;
					case 9:
						if (value > 90)
						{
							num = 0;
							continue;
						}
						num = 8;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_81:
						num = 6;
						break;
					default:
						if (false)
						{
						}
						if (value < 10)
						{
							goto IL_F7;
						}
						if (true)
						{
						}
						num = 3;
						break;
					}
				}
				IL_9C:
				throw new NotSupportedException(RecordTableEnumerator.b("笾⹀㙂≄⽆❈㹊㥌ݎ㹐㽒ごіじ⅚㡜罞ɠɢ୤०٨Ὢ䵬൮ᑰ卲ٴɶॸ୺ቼൾꞆﾌ꾎ﮒﲔ릘ﺞ펠힢薤펦킨\udbaa좬膮", a_));
				IL_E1:
				IL_F7:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("笾⹀ⵂい㍆ň⑊⅌⩎ɐ㩒⽔㉖", a_));
				IL_137:
				this.PieRecord.ᜁ((ushort)value);
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x0600165D RID: 5725 RVA: 0x000D7924 File Offset: 0x000D6924
		// (set) Token: 0x0600165E RID: 5726 RVA: 0x000D796C File Offset: 0x000D696C
		public bool HasShadowPie
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
				return this.PieRecord.ᜀ();
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
				this.PieRecord.ᜁ(value);
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x0600165F RID: 5727 RVA: 0x000D79B4 File Offset: 0x000D69B4
		// (set) Token: 0x06001660 RID: 5728 RVA: 0x000D7A0C File Offset: 0x000D6A0C
		public bool IsShowLeaderLines
		{
			get
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
					if (this.\u1712 is spr\u2156)
					{
						return false;
					}
					break;
				}
				return this.PieRecord.ᜅ();
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
				this.PieRecord.ᜀ(value);
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06001661 RID: 5729 RVA: 0x000D7A54 File Offset: 0x000D6A54
		// (set) Token: 0x06001662 RID: 5730 RVA: 0x000D7A9C File Offset: 0x000D6A9C
		public int BubbleScale
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
				return (int)this.ScatterRecord.ᜅ();
			}
			set
			{
				int a_ = 16;
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (!this.\u171B.TypeChanging)
						{
							num = 4;
							continue;
						}
						goto IL_147;
					case 1:
						goto IL_E3;
					case 2:
					{
						ExcelChartType chartType;
						if (chartType != ExcelChartType.Bubble)
						{
							num = 3;
							continue;
						}
						goto IL_147;
					}
					case 3:
						goto IL_80;
					case 4:
					{
						ExcelChartType chartType = this.\u171B.ChartType;
						num = 2;
						continue;
					}
					case 6:
					{
						ExcelChartType chartType;
						if (chartType != ExcelChartType.Bubble3D)
						{
							num = 7;
							continue;
						}
						goto IL_147;
					}
					case 7:
						goto IL_9B;
					case 8:
						if (value > 300)
						{
							num = 1;
							continue;
						}
						num = 0;
						continue;
					case 9:
						num = 8;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_80:
						num = 6;
						break;
					default:
						if (false)
						{
						}
						if (value < 0)
						{
							goto IL_F9;
						}
						if (true)
						{
						}
						num = 9;
						break;
					}
				}
				IL_9B:
				throw new NotSupportedException(RecordTableEnumerator.b("х㵇⡉⹋≍㕏ő㝓㝕㑗㽙籛㵝şౡ੣॥ᱧ䩩๫୭偯űųٵࡷᕹ๻੽ꒃ겋揄ﮑ뚕ﮗﶛ풟芡킣\udfa5\ud8a7쾩芫", a_));
				IL_E3:
				IL_F9:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("х㵇⡉⁋⭍͏㭑⹓㍕ୗ㥙㵛㉝՟", a_), RecordTableEnumerator.b("၅⥇♉㥋⭍灏㽑⅓╕ⱗ穙㹛㭝䁟աᙣͥ१ṩ५ᱭ偯ٱᱳ᝵ᙷ婹ٻ᭽ꒃ겋뚕ﶛ肟醡钣隥蚧", a_));
				IL_147:
				this.ScatterRecord.ᜁ((ushort)value);
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06001663 RID: 5731 RVA: 0x000D7C00 File Offset: 0x000D6C00
		// (set) Token: 0x06001664 RID: 5732 RVA: 0x000D7C48 File Offset: 0x000D6C48
		public BubbleSizeType SizeRepresents
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
				return this.ScatterRecord.ᜇ();
			}
			set
			{
				int a_ = 5;
				int num = 3;
				for (;;)
				{
					ExcelChartType chartType;
					switch (num)
					{
					case 0:
						goto IL_67;
					case 1:
						num = 0;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_67;
						default:
							if (false)
							{
							}
							if (chartType != ExcelChartType.Bubble)
							{
								num = 1;
								continue;
							}
							goto IL_C0;
						}
						break;
					case 4:
						chartType = this.\u171B.ChartType;
						num = 2;
						continue;
					case 5:
						goto IL_74;
					}
					if (!this.\u171B.TypeChanging)
					{
						num = 4;
						continue;
					}
					goto IL_C0;
					IL_67:
					if (chartType == ExcelChartType.Bubble3D)
					{
						goto IL_C0;
					}
					num = 5;
				}
				IL_74:
				if (true)
				{
				}
				throw new NotSupportedException(RecordTableEnumerator.b("栺吼䔾⑀ᅂ⁄㝆㭈⹊㹌⩎㽐❒♔睖㩘㩚㍜ㅞ๠ᝢ䕤զ౨䭪ṬᩮŰͲᩴն൸Ṻ᥼彾Ꞇﶈﲎ놐ﶔ붜\ud8a0펢삤覦", a_));
				IL_C0:
				this.ScatterRecord.ᜀ(value);
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06001665 RID: 5733 RVA: 0x000D7D2C File Offset: 0x000D6D2C
		// (set) Token: 0x06001666 RID: 5734 RVA: 0x000D7D74 File Offset: 0x000D6D74
		public bool IsBubbles
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
				return this.ScatterRecord.ᜄ();
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
				this.ScatterRecord.ᜁ(value);
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06001667 RID: 5735 RVA: 0x000D7DBC File Offset: 0x000D6DBC
		// (set) Token: 0x06001668 RID: 5736 RVA: 0x000D7E04 File Offset: 0x000D6E04
		public bool ShowNegativeBubbles
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
				return this.ScatterRecord.ᜁ();
			}
			set
			{
				int a_ = 11;
				for (;;)
				{
					IL_3D:
					if (true)
					{
					}
					ExcelChartType chartType = this.\u171B.ChartType;
					int num = 2;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3D;
						default:
							if (false)
							{
							}
							switch (num)
							{
							case 0:
								num = 3;
								continue;
							case 1:
								goto IL_9E;
							case 2:
								if (chartType != ExcelChartType.Bubble)
								{
									num = 0;
									continue;
								}
								goto IL_A0;
							case 3:
								if (chartType != ExcelChartType.Bubble3D)
								{
									num = 1;
									continue;
								}
								goto IL_A0;
							}
							goto IL_3D;
						}
					}
				}
				IL_9E:
				throw new NotSupportedException(RecordTableEnumerator.b("ቀ⭂⩄う݈⹊⩌⹎═㩒⍔㉖᭘⹚㽜㵞ൠ٢ᙤ䝦੨੪ͬ佮ὰᱲŴ坶᭸Ṻ嵼౾ﮈﾊ놐杖릘튠莢욤쾦좨\ud9aa\ud9ac辮얰쪲어튶鞸閺", a_));
				IL_A0:
				this.ScatterRecord.ᜀ(value);
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06001669 RID: 5737 RVA: 0x000D7EC0 File Offset: 0x000D6EC0
		// (set) Token: 0x0600166A RID: 5738 RVA: 0x000D7F08 File Offset: 0x000D6F08
		public bool HasShadowScatter
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
				return this.ScatterRecord.ᜀ();
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
				this.ScatterRecord.ᜂ(value);
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x000D7F50 File Offset: 0x000D6F50
		// (set) Token: 0x0600166C RID: 5740 RVA: 0x000D7F98 File Offset: 0x000D6F98
		public bool IsStacked
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
				return this.AreaRecord.ᜁ();
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
				this.AreaRecord.ᜃ(value);
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x0600166D RID: 5741 RVA: 0x000D7FE0 File Offset: 0x000D6FE0
		// (set) Token: 0x0600166E RID: 5742 RVA: 0x000D8028 File Offset: 0x000D7028
		public bool IsCategoryBrokenDown
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
				return this.AreaRecord.ᜂ();
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
				this.AreaRecord.ᜂ(value);
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x0600166F RID: 5743 RVA: 0x000D8070 File Offset: 0x000D7070
		// (set) Token: 0x06001670 RID: 5744 RVA: 0x000D80B8 File Offset: 0x000D70B8
		public bool IsAreaShadowed
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
				return this.AreaRecord.ᜃ();
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
				this.AreaRecord.ᜁ(value);
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06001671 RID: 5745 RVA: 0x000D8100 File Offset: 0x000D7100
		// (set) Token: 0x06001672 RID: 5746 RVA: 0x000D8148 File Offset: 0x000D7148
		public bool IsFillSurface
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
				return this.SurfaceRecord.ᜄ();
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
				this.SurfaceRecord.ᜀ(value);
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06001673 RID: 5747 RVA: 0x000D8190 File Offset: 0x000D7190
		// (set) Token: 0x06001674 RID: 5748 RVA: 0x000D81D8 File Offset: 0x000D71D8
		public bool Is3DPhongShade
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
				return this.SurfaceRecord.ᜀ();
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
				this.SurfaceRecord.ᜁ(value);
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06001675 RID: 5749 RVA: 0x000D8220 File Offset: 0x000D7220
		// (set) Token: 0x06001676 RID: 5750 RVA: 0x000D8268 File Offset: 0x000D7268
		public bool HasShadowRadar
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
				return this.RadarRecord.ᜀ();
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
				this.RadarRecord.ᜁ(value);
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06001677 RID: 5751 RVA: 0x000D82B0 File Offset: 0x000D72B0
		// (set) Token: 0x06001678 RID: 5752 RVA: 0x000D8318 File Offset: 0x000D7318
		public bool HasRadarAxisLabels
		{
			get
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
					if (this.\u1712.TypeCode != TBIFFRecord.ChartRadarArea)
					{
						return this.RadarAreaRecord.ᜁ();
					}
					break;
				}
				if (true)
				{
				}
				return this.RadarRecord.ᜅ();
			}
			set
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
					if (this.\u1712.TypeCode == TBIFFRecord.ChartRadarArea)
					{
						if (true)
						{
						}
						this.RadarAreaRecord.ᜀ(value);
						return;
					}
					break;
				}
				this.RadarRecord.ᜀ(value);
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06001679 RID: 5753 RVA: 0x000D8380 File Offset: 0x000D7380
		// (set) Token: 0x0600167A RID: 5754 RVA: 0x000D83C8 File Offset: 0x000D73C8
		public ChartPieType PieChartType
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
				return this.BoppopRecord.ᜆ();
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
				this.BoppopRecord.ᜀ(value);
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x000D8410 File Offset: 0x000D7410
		// (set) Token: 0x0600167C RID: 5756 RVA: 0x000D8458 File Offset: 0x000D7458
		public bool UseDefaultSplitValue
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
				return this.BoppopRecord.ᜂ();
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
				this.BoppopRecord.ᜁ(value);
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x0600167D RID: 5757 RVA: 0x000D84A0 File Offset: 0x000D74A0
		// (set) Token: 0x0600167E RID: 5758 RVA: 0x000D84E8 File Offset: 0x000D74E8
		public SplitType SplitType
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
				return this.BoppopRecord.ᜃ();
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
				this.BoppopRecord.ᜀ(value);
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x0600167F RID: 5759 RVA: 0x000D8530 File Offset: 0x000D7530
		// (set) Token: 0x06001680 RID: 5760 RVA: 0x000D8578 File Offset: 0x000D7578
		public int SplitValue
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
				return (int)this.BoppopRecord.ᜄ();
			}
			set
			{
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
							continue;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 1:
						goto IL_64;
					case 2:
						if (true)
						{
						}
						this.BoppopRecord.ᜃ((ushort)value);
						num = 3;
						continue;
					case 3:
						goto IL_8D;
					}
					if (this.SplitType == SplitType.Percent)
					{
						num = 2;
					}
					else
					{
						this.BoppopRecord.ᜁ((ushort)value);
						num = 1;
					}
				}
				IL_64:
				IL_8D:
				this.UseDefaultSplitValue = false;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06001681 RID: 5761 RVA: 0x000D861C File Offset: 0x000D761C
		// (set) Token: 0x06001682 RID: 5762 RVA: 0x000D8664 File Offset: 0x000D7664
		public int SplitPercent
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
				return (int)this.BoppopRecord.ᜇ();
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
				this.BoppopRecord.ᜃ((ushort)value);
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06001683 RID: 5763 RVA: 0x000D86AC File Offset: 0x000D76AC
		// (set) Token: 0x06001684 RID: 5764 RVA: 0x000D86F4 File Offset: 0x000D76F4
		public int PieSecondSize
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
				return (int)this.BoppopRecord.ᜀ();
			}
			set
			{
				int a_ = 19;
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
						goto IL_91;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_81;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 3:
						goto IL_81;
					}
					if (value >= 5)
					{
						num = 0;
						continue;
					}
					break;
					IL_81:
					if (value <= 200)
					{
						goto IL_93;
					}
					num = 1;
				}
				IL_65:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("᥈≊⡌ᱎ㑐げ㩔㥖㵘࡚㑜╞Ѡ", a_));
				IL_91:
				goto IL_65;
				IL_93:
				this.BoppopRecord.ᜀ((ushort)value);
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06001685 RID: 5765 RVA: 0x000D87A4 File Offset: 0x000D77A4
		// (set) Token: 0x06001686 RID: 5766 RVA: 0x000D87EC File Offset: 0x000D77EC
		public int Gap
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
				return (int)this.BoppopRecord.ᜈ();
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
				this.BoppopRecord.ᜂ((ushort)value);
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06001687 RID: 5767 RVA: 0x000D8834 File Offset: 0x000D7834
		// (set) Token: 0x06001688 RID: 5768 RVA: 0x000D887C File Offset: 0x000D787C
		public int NumSplitValue
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
				return this.BoppopRecord.ᜅ();
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
				this.BoppopRecord.ᜀ(value);
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06001689 RID: 5769 RVA: 0x000D88C4 File Offset: 0x000D78C4
		// (set) Token: 0x0600168A RID: 5770 RVA: 0x000D890C File Offset: 0x000D790C
		public bool HasShadowBoppop
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
				return this.BoppopRecord.ᜁ();
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
				this.BoppopRecord.ᜀ(value);
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x0600168B RID: 5771 RVA: 0x000D8954 File Offset: 0x000D7954
		// (set) Token: 0x0600168C RID: 5772 RVA: 0x000D899C File Offset: 0x000D799C
		public bool IsSeriesName
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
				return this.DataLabelsRecord.ᜂ();
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
				this.DataLabelsRecord.ᜃ(value);
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x0600168D RID: 5773 RVA: 0x000D89E4 File Offset: 0x000D79E4
		// (set) Token: 0x0600168E RID: 5774 RVA: 0x000D8A2C File Offset: 0x000D7A2C
		public bool IsCategoryName
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
				return this.DataLabelsRecord.ᜀ();
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
				this.DataLabelsRecord.ᜄ(value);
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x0600168F RID: 5775 RVA: 0x000D8A74 File Offset: 0x000D7A74
		// (set) Token: 0x06001690 RID: 5776 RVA: 0x000D8ABC File Offset: 0x000D7ABC
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
				return this.DataLabelsRecord.ᜄ();
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
				this.DataLabelsRecord.ᜁ(value);
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06001691 RID: 5777 RVA: 0x000D8B04 File Offset: 0x000D7B04
		// (set) Token: 0x06001692 RID: 5778 RVA: 0x000D8B4C File Offset: 0x000D7B4C
		public bool IsPercentage
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
				return this.DataLabelsRecord.ᜆ();
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
				this.DataLabelsRecord.ᜀ(value);
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06001693 RID: 5779 RVA: 0x000D8B94 File Offset: 0x000D7B94
		// (set) Token: 0x06001694 RID: 5780 RVA: 0x000D8BDC File Offset: 0x000D7BDC
		public bool IsBubbleSize
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
				return this.DataLabelsRecord.ᜁ();
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
				this.DataLabelsRecord.ᜂ(value);
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06001695 RID: 5781 RVA: 0x000D8C24 File Offset: 0x000D7C24
		public int DelimiterLength
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
				return this.DataLabelsRecord.ᜇ();
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06001696 RID: 5782 RVA: 0x000D8C6C File Offset: 0x000D7C6C
		// (set) Token: 0x06001697 RID: 5783 RVA: 0x000D8CB4 File Offset: 0x000D7CB4
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
				return this.DataLabelsRecord.ᜃ();
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
				this.DataLabelsRecord.ᜀ(value);
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x000D8CFC File Offset: 0x000D7CFC
		// (set) Token: 0x06001699 RID: 5785 RVA: 0x000D8D98 File Offset: 0x000D7D98
		public DropLineStyleType LineStyle
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_5B;
					case 1:
						if (true)
						{
						}
						this.\u1716 = (spr\u233F)spr\u175E.ᜀ(TBIFFRecord.ChartChartLine);
						num = 0;
						continue;
					}
					IL_1C:
					if (spr\u233F.ᜁ(this.\u1716, null))
					{
						num = 1;
						continue;
					}
					IL_5B:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C;
					default:
						goto IL_71;
					}
				}
				IL_71:
				if (false)
				{
				}
				return this.\u1716.ᜀ();
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.\u1716 = (spr\u233F)spr\u175E.ᜀ(TBIFFRecord.ChartChartLine);
						num = 2;
						continue;
					case 2:
						goto IL_5B;
					}
					IL_1C:
					if (true)
					{
					}
					if (spr\u233F.ᜁ(this.\u1716, null))
					{
						num = 1;
						continue;
					}
					IL_5B:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C;
					default:
						goto IL_71;
					}
				}
				IL_71:
				if (false)
				{
				}
				this.\u1716.ᜀ(value);
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x0600169A RID: 5786 RVA: 0x000D8E34 File Offset: 0x000D7E34
		public IChartDropBar FirstDropBar
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.\u1718 = new ChartDropBar((spr\u2158)base.ReservedHandle, this);
						num = 2;
						continue;
					case 2:
						goto IL_4F;
					}
					IL_1C:
					if (this.\u1718 == null)
					{
						num = 1;
						continue;
					}
					IL_4F:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C;
					default:
						goto IL_65;
					}
				}
				IL_65:
				if (true)
				{
				}
				if (false)
				{
				}
				return this.\u1718;
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x0600169B RID: 5787 RVA: 0x000D8EC4 File Offset: 0x000D7EC4
		public IChartDropBar SecondDropBar
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						break;
					case 1:
						goto IL_57;
					case 2:
						this.\u1719 = new ChartDropBar((spr\u2158)base.ReservedHandle, this);
						num = 1;
						continue;
					}
					IL_24:
					if (this.\u1719 == null)
					{
						num = 2;
						continue;
					}
					IL_57:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_24;
					default:
						goto IL_6D;
					}
				}
				IL_6D:
				if (false)
				{
				}
				return this.\u1719;
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x0600169C RID: 5788 RVA: 0x000D8F54 File Offset: 0x000D7F54
		public bool IsDefaultRotation
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
				return this.Chart3DRecord.ᜆ();
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x0600169D RID: 5789 RVA: 0x000D8F9C File Offset: 0x000D7F9C
		public IChartBorder PieSeriesLine
		{
			get
			{
				int a_ = 2;
				int num = 1;
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
							if (true)
							{
							}
							if (false)
							{
							}
							this.\u171D = new ChartBorder((spr\u2158)base.ReservedHandle, this);
							num = 3;
							continue;
						}
						break;
					case 2:
						if (this.\u171D == null)
						{
							num = 0;
							continue;
						}
						goto IL_CF;
					case 3:
						goto IL_99;
					case 4:
						goto IL_47;
					}
					if (this.\u1712.TypeCode != TBIFFRecord.ChartBoppop)
					{
						num = 4;
					}
					else
					{
						num = 2;
					}
				}
				IL_47:
				throw new ArgumentNullException(RecordTableEnumerator.b("样匹夻洽┿ぁⵃ⍅㭇ى╋⁍㕏牑㝓㝕㙗㑙㍛⩝䁟aţ䙥᭧ὩᱫṭὯqs፵ᱷ婹᩻ᅽꊁꪉ뚕ﮝ躟", a_));
				IL_99:
				IL_CF:
				return this.\u171D;
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x0600169E RID: 5790 RVA: 0x000D9080 File Offset: 0x000D8080
		public bool IsDefaultElevation
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
				return this.Chart3DRecord.ᜂ();
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x0600169F RID: 5791 RVA: 0x000D90C8 File Offset: 0x000D80C8
		// (set) Token: 0x060016A0 RID: 5792 RVA: 0x000D9110 File Offset: 0x000D8110
		public int Rotation
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
				return (int)this.Chart3DRecord.ᜌ();
			}
			set
			{
				int a_ = 19;
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
							goto IL_91;
						case 2:
							if (value > 360)
							{
								if (true)
								{
								}
								num = 0;
								continue;
							}
							goto IL_93;
						case 3:
							num = 2;
							continue;
						}
						if (value < 0)
						{
							goto IL_5D;
						}
						break;
					}
					num = 3;
				}
				IL_5D:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ᭈ⑊㥌⹎═㩒㩔㥖", a_));
				IL_91:
				goto IL_5D;
				IL_93:
				this.Chart3DRecord.ᜄ((ushort)value);
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x000D91C0 File Offset: 0x000D81C0
		// (set) Token: 0x060016A2 RID: 5794 RVA: 0x000D9208 File Offset: 0x000D8208
		public int Elevation
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
				return (int)this.Chart3DRecord.ᜃ();
			}
			set
			{
				int a_ = 15;
				int num = 3;
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
							goto IL_8F;
						case 1:
							if (true)
							{
							}
							if (value > 90)
							{
								num = 0;
								continue;
							}
							goto IL_91;
						case 2:
							num = 1;
							continue;
						}
						if (value < -90)
						{
							goto IL_5E;
						}
						break;
					}
					num = 2;
				}
				IL_5E:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("D⭆ⱈ㵊ⱌ㭎㡐㱒㭔", a_));
				IL_8F:
				goto IL_5E;
				IL_91:
				this.Chart3DRecord.ᜀ((short)value);
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x060016A3 RID: 5795 RVA: 0x000D92B4 File Offset: 0x000D82B4
		// (set) Token: 0x060016A4 RID: 5796 RVA: 0x000D92FC File Offset: 0x000D82FC
		public int Perspective
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
				return (int)this.Chart3DRecord.ᜅ();
			}
			set
			{
				int a_ = 2;
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
							goto IL_8E;
						case 2:
							num = 3;
							continue;
						case 3:
							if (value > 100)
							{
								if (true)
								{
								}
								num = 0;
								continue;
							}
							goto IL_90;
						}
						if (value < 0)
						{
							goto IL_5D;
						}
						break;
					}
					num = 2;
				}
				IL_5D:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("紷嘹夻䠽ℿ㙁ⵃ⥅♇", a_));
				IL_8E:
				goto IL_5D;
				IL_90:
				this.Chart3DRecord.ᜀ((ushort)value);
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x060016A5 RID: 5797 RVA: 0x000D93A8 File Offset: 0x000D83A8
		// (set) Token: 0x060016A6 RID: 5798 RVA: 0x000D93F0 File Offset: 0x000D83F0
		public int HeightPercent
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
				return (int)this.Chart3DRecord.ᜀ();
			}
			set
			{
				int a_ = 1;
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
						if (true)
						{
						}
						switch (num)
						{
						case 1:
							num = 2;
							continue;
						case 2:
							if (value > 500)
							{
								num = 3;
								continue;
							}
							goto IL_93;
						case 3:
							goto IL_91;
						}
						if (value < 5)
						{
							goto IL_65;
						}
						break;
					}
					num = 1;
				}
				IL_65:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("父唸帺䬼帾㕀⩂⩄⥆", a_));
				IL_91:
				goto IL_65;
				IL_93:
				this.Chart3DRecord.ᜃ((ushort)value);
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x060016A7 RID: 5799 RVA: 0x000D94A0 File Offset: 0x000D84A0
		// (set) Token: 0x060016A8 RID: 5800 RVA: 0x000D94E8 File Offset: 0x000D84E8
		public int DepthPercent
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
				return (int)this.Chart3DRecord.ᜈ();
			}
			set
			{
				int a_ = 12;
				int num = 3;
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
							if (value > 2000)
							{
								num = 2;
								continue;
							}
							goto IL_94;
						case 1:
							num = 0;
							continue;
						case 2:
							goto IL_92;
						}
						if (true)
						{
						}
						if (value < 20)
						{
							goto IL_66;
						}
						break;
					}
					num = 1;
				}
				IL_66:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ف⅃㙅㱇≉᱋⭍≏ㅑㅓ㡕ⱗ", a_));
				IL_92:
				goto IL_66;
				IL_94:
				this.Chart3DRecord.ᜁ((ushort)value);
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x060016A9 RID: 5801 RVA: 0x000D9598 File Offset: 0x000D8598
		// (set) Token: 0x060016AA RID: 5802 RVA: 0x000D95E0 File Offset: 0x000D85E0
		public int GapDepth
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
				return (int)this.Chart3DRecord.ᜄ();
			}
			set
			{
				int a_ = 3;
				int num = 3;
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
						case 0:
							if (value > 500)
							{
								num = 1;
								continue;
							}
							goto IL_93;
						case 1:
							goto IL_91;
						case 2:
							num = 0;
							continue;
						}
						if (value < 0)
						{
							goto IL_65;
						}
						break;
					}
					num = 2;
				}
				IL_65:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("縸娺䴼笾⑀㍂ㅄ⽆", a_));
				IL_91:
				goto IL_65;
				IL_93:
				this.Chart3DRecord.ᜂ((ushort)value);
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x060016AB RID: 5803 RVA: 0x000D9690 File Offset: 0x000D8690
		// (set) Token: 0x060016AC RID: 5804 RVA: 0x000D96DC File Offset: 0x000D86DC
		public bool RightAngleAxes
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
				return !this.Chart3DRecord.ᜊ();
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
				this.Chart3DRecord.ᜀ(!value);
			}
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x000D9728 File Offset: 0x000D8728
		// (set) Token: 0x060016AE RID: 5806 RVA: 0x000D9770 File Offset: 0x000D8770
		public bool IsClustered
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
				return this.Chart3DRecord.ᜋ();
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
				this.Chart3DRecord.ᜅ(value);
			}
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x000D97B8 File Offset: 0x000D87B8
		// (set) Token: 0x060016B0 RID: 5808 RVA: 0x000D9800 File Offset: 0x000D8800
		public bool AutoScaling
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
				return this.Chart3DRecord.ᜉ();
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
				this.Chart3DRecord.ᜁ(value);
			}
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x060016B1 RID: 5809 RVA: 0x000D9848 File Offset: 0x000D8848
		// (set) Token: 0x060016B2 RID: 5810 RVA: 0x000D9890 File Offset: 0x000D8890
		public bool WallsAndGridlines2D
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
				return this.Chart3DRecord.ᜁ();
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
				this.Chart3DRecord.ᜂ(value);
			}
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x060016B3 RID: 5811 RVA: 0x000D98D8 File Offset: 0x000D88D8
		private spr\u204B BarRecord
		{
			get
			{
				int a_ = 13;
				if (this.\u1712.TypeCode != TBIFFRecord.ChartBar)
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
					throw new NotSupportedException(RecordTableEnumerator.b("ᝂⵄ⹆㩈歊㵌㵎㹐⍒ご╖ⵘ≚絜㙞በ䍢୤ࡦᵨ䭪ṬᩮŰᱲݴͶᱸὺ嵼ᙾꎂﮈ力뎒ﾖ뾞햠\udaa2햤슦螨", a_));
				}
				return this.\u1712 as spr\u204B;
			}
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x060016B4 RID: 5812 RVA: 0x000D9950 File Offset: 0x000D8950
		private sprᯙ LineRecord
		{
			get
			{
				int a_ = 13;
				if (this.\u1712.TypeCode != TBIFFRecord.ChartLine)
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
					throw new NotSupportedException(RecordTableEnumerator.b("ᝂⵄ⹆㩈歊㵌㵎㹐⍒ご╖ⵘ≚絜㙞በ䍢୤ࡦᵨ䭪ṬᩮŰᱲݴͶᱸὺ嵼ᙾꎂﮈ力뎒ﾖ뾞햠\udaa2햤슦螨", a_));
				}
				return this.\u1712 as sprᯙ;
			}
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x060016B5 RID: 5813 RVA: 0x000D99C8 File Offset: 0x000D89C8
		private spr\u1B77 PieRecord
		{
			get
			{
				int a_ = 17;
				if (this.\u1712.TypeCode != TBIFFRecord.ChartPie)
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
					throw new NotSupportedException(RecordTableEnumerator.b("ፆⅈ≊㹌潎⅐⅒㩔❖㱘⥚⥜♞䅠੢ᙤ䝦ݨѪᥬ佮ɰٲմᡶ୸ེ᡼᭾ꆀꞆﺊﾌﶎﶒ랖滛ﲜ햠莢톤\udea6\ud9a8캪莬", a_));
				}
				if (true)
				{
				}
				return this.\u1712 as spr\u1B77;
			}
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x060016B6 RID: 5814 RVA: 0x000D9A40 File Offset: 0x000D8A40
		private spr\u1AB2 ScatterRecord
		{
			get
			{
				int a_ = 11;
				if (this.\u1712.TypeCode != TBIFFRecord.ChartScatter)
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
						break;
					}
					throw new NotSupportedException(RecordTableEnumerator.b("ᕀ⭂ⱄ㑆楈㭊㽌⁎⅐㙒❔⍖⁘筚㑜ⱞ䅠ൢ੤፦䥨ᡪᡬὮṰŲŴቶᵸ孺ᑼᅾꆀﮈﮎ놐ﶔ붜\ud8a0펢삤覦", a_));
				}
				return this.\u1712 as spr\u1AB2;
			}
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x060016B7 RID: 5815 RVA: 0x000D9AB8 File Offset: 0x000D8AB8
		private spr\u1D5A AreaRecord
		{
			get
			{
				int a_ = 3;
				if (this.\u1712.TypeCode != TBIFFRecord.ChartArea)
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
					if (true)
					{
					}
					throw new NotSupportedException(RecordTableEnumerator.b("洸区吼䰾慀㍂㝄⡆㥈⹊㽌㭎⡐獒㱔⑖祘㕚㉜⭞䅠ၢၤᝦ٨ᥪᥬ੮ᕰ卲ᱴ᥶奸᡺ࡼൾꦈ떔놞", a_));
				}
				return this.\u1712 as spr\u1D5A;
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x060016B8 RID: 5816 RVA: 0x000D9B30 File Offset: 0x000D8B30
		private sprᨺ SurfaceRecord
		{
			get
			{
				int a_ = 6;
				if (this.\u1712.TypeCode != TBIFFRecord.ChartSurface)
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
					if (true)
					{
					}
					throw new NotSupportedException(RecordTableEnumerator.b("栻嘽⤿ㅁ摃㙅㩇╉㱋⭍≏♑ⵓ癕ㅗ⥙籛そཟᙡ䑣ᕥᵧᩩͫᱭѯ᝱ၳ噵ᅷᑹ屻ᵽﺉ겋뢗얟財", a_));
				}
				return this.\u1712 as sprᨺ;
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x060016B9 RID: 5817 RVA: 0x000D9BA8 File Offset: 0x000D8BA8
		private sprẨ RadarRecord
		{
			get
			{
				int a_ = 0;
				if (this.\u1712.TypeCode != TBIFFRecord.ChartRadar)
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
						break;
					}
					throw new NotSupportedException(RecordTableEnumerator.b("戵倷匹伻ḽ〿ぁ⭃㙅ⵇ㡉㡋㝍灏㭑❓癕㙗㕙⡛繝፟ᝡᑣ॥ᩧṩ५੭偯᭱ᩳ噵᭷ཹ๻౽ꚅﲍ늑ﾙ늛", a_));
				}
				return this.\u1712 as sprẨ;
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x060016BA RID: 5818 RVA: 0x000D9C20 File Offset: 0x000D8C20
		private sprᶗ RadarAreaRecord
		{
			get
			{
				int a_ = 1;
				if (this.\u1712.TypeCode != TBIFFRecord.ChartRadarArea)
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
					throw new NotSupportedException(RecordTableEnumerator.b("挶儸刺丼Ἶㅀㅂ⩄㝆ⱈ㥊㥌㙎煐㩒♔睖㝘㑚⥜罞በᙢᕤࡦ᭨Ὢ࡬୮兰ᩲ᭴坶᩸๺ོൾꞆﶎ뎒ﺚ뎜", a_));
				}
				if (true)
				{
				}
				return this.\u1712 as sprᶗ;
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x060016BB RID: 5819 RVA: 0x000D9C98 File Offset: 0x000D8C98
		private spr\u2156 BoppopRecord
		{
			get
			{
				int a_ = 7;
				if (this.\u1712.TypeCode != TBIFFRecord.ChartBoppop)
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
					if (true)
					{
					}
					throw new NotSupportedException(RecordTableEnumerator.b("椼圾⡀あ敄㝆㭈⑊㵌⩎⍐❒ⱔ睖じ⡚絜ㅞ๠ᝢ䕤ᑦᱨ᭪ɬᵮհᙲᅴ坶ၸᕺ嵼᱾ﾊ권戀릘쒠趢", a_));
				}
				return this.\u1712 as spr\u2156;
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x060016BC RID: 5820 RVA: 0x000D9D10 File Offset: 0x000D8D10
		private sprᨻ DataLabelsRecord
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_53;
					case 2:
						this.\u1715 = (sprᨻ)spr\u175E.ᜀ(TBIFFRecord.ChartDataLabels);
						num = 1;
						continue;
					}
					IL_1C:
					if (sprᨻ.ᜁ(this.\u1715, null))
					{
						num = 2;
						continue;
					}
					IL_53:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C;
					default:
						goto IL_69;
					}
				}
				IL_69:
				if (true)
				{
				}
				if (false)
				{
				}
				return this.\u1715;
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x060016BD RID: 5821 RVA: 0x000D9DA4 File Offset: 0x000D8DA4
		private XlsChartSerieDataFormat DataFormat
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.\u1717 = new ChartSerieDataFormat((spr\u2158)base.ReservedHandle, this);
						num = 2;
						continue;
					case 2:
						if (true)
						{
						}
						goto IL_57;
					}
					IL_1C:
					if (this.\u1717 == null)
					{
						num = 0;
						continue;
					}
					IL_57:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C;
					default:
						goto IL_6D;
					}
				}
				IL_6D:
				if (false)
				{
				}
				return this.\u1717;
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x060016BE RID: 5822 RVA: 0x000D9E34 File Offset: 0x000D8E34
		private spr᪘ ChartChartFormatRecord
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_55;
					case 1:
						this.ᜑ = (spr᪘)spr\u175E.ᜀ(TBIFFRecord.ChartChartFormat);
						num = 0;
						continue;
					}
					IL_1C:
					if (true)
					{
					}
					if (this.ᜑ == null)
					{
						num = 1;
						continue;
					}
					IL_55:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C;
					default:
						goto IL_6B;
					}
				}
				IL_6B:
				if (false)
				{
				}
				return this.ᜑ;
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x060016BF RID: 5823 RVA: 0x000D9EC4 File Offset: 0x000D8EC4
		private spr\u2272 Chart3DRecord
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.\u1713 = (spr\u2272)spr\u175E.ᜀ(TBIFFRecord.Chart3D);
						num = 2;
						continue;
					case 1:
						if (true)
						{
						}
						break;
					case 2:
						goto IL_5B;
					}
					IL_24:
					if (spr\u2272.ᜁ(this.\u1713, null))
					{
						num = 0;
						continue;
					}
					IL_5B:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_24;
					default:
						goto IL_71;
					}
				}
				IL_71:
				if (false)
				{
				}
				return this.\u1713;
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x060016C0 RID: 5824 RVA: 0x000D9F58 File Offset: 0x000D8F58
		public bool IsPrimaryAxis
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
				return this.\u171C.ᜁ();
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x060016C1 RID: 5825 RVA: 0x000D9FA0 File Offset: 0x000D8FA0
		public bool IsChartChartLine
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
				return spr\u233F.ᜀ(this.\u1716, null);
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x060016C2 RID: 5826 RVA: 0x000D9FE8 File Offset: 0x000D8FE8
		public bool IsChartLineFormat
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
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x060016C3 RID: 5827 RVA: 0x000DA030 File Offset: 0x000D9030
		public bool IsDropBar
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
				return this.\u1718 != null;
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x060016C4 RID: 5828 RVA: 0x000DA078 File Offset: 0x000D9078
		internal BiffRecordRaw SerieFormat
		{
			get
			{
				int a_ = 0;
				if (this.\u1712 == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("嬵朷䤹夻䰽⤿❁Ƀ⥅㩇❉ⵋ㩍", a_));
				}
				return this.\u1712;
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x060016C5 RID: 5829 RVA: 0x000DA0E0 File Offset: 0x000D90E0
		// (set) Token: 0x060016C6 RID: 5830 RVA: 0x000DA128 File Offset: 0x000D9128
		public int DrawingZOrder
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
				return (int)this.ChartChartFormatRecord.ᜆ();
			}
			set
			{
				for (;;)
				{
					int num = (int)this.ChartChartFormatRecord.ᜆ();
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							this.ChartChartFormatRecord.ᜀ((ushort)value);
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_28;
							default:
								if (false)
								{
								}
								num2 = 2;
								continue;
							}
							break;
						case 1:
							goto IL_28;
						case 2:
							return;
						}
						break;
						IL_28:
						if (num == value)
						{
							return;
						}
						num2 = 0;
					}
				}
			}
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x060016C7 RID: 5831 RVA: 0x000DA1B0 File Offset: 0x000D91B0
		internal TBIFFRecord FormatRecordType
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
				return this.\u1712.TypeCode;
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x060016C8 RID: 5832 RVA: 0x000DA1F8 File Offset: 0x000D91F8
		public bool Is3D
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
				return spr\u2272.ᜀ(this.\u1713, null);
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x060016C9 RID: 5833 RVA: 0x000DA240 File Offset: 0x000D9240
		public XlsChartSerieDataFormat DataFormatOrNull
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
				return this.\u1717;
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x060016CA RID: 5834 RVA: 0x000DA284 File Offset: 0x000D9284
		public bool IsMarker
		{
			get
			{
				if (this.\u1717 != null)
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
						return this.\u1717.IsMarker;
					}
				}
				return true;
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x060016CB RID: 5835 RVA: 0x000DA2D8 File Offset: 0x000D92D8
		public bool IsLine
		{
			get
			{
				if (this.\u1717 != null)
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
						return this.\u1717.HasBorderLine;
					}
				}
				return true;
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x060016CC RID: 5836 RVA: 0x000DA32C File Offset: 0x000D932C
		public bool IsSmoothed
		{
			get
			{
				if (this.\u1717 != null)
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
						return this.\u1717.IsSmoothed;
					}
				}
				return false;
			}
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x000DA380 File Offset: 0x000D9380
		internal XlsChartFormat(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.\u1715();
			this.ᜑ = (spr᪘)spr\u175E.ᜀ(TBIFFRecord.ChartChartFormat);
			this.\u1712 = spr\u175E.ᜀ(TBIFFRecord.ChartBar);
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x000DA3C0 File Offset: 0x000D93C0
		internal void \u1715()
		{
			int a_ = 10;
			this.\u171C = (sprᾹ)base.FindParent(typeof(sprᾹ));
			if (this.\u171C == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("ဿ⍁㙃⍅♇㹉汋⅍㉏㡑ㅓ㕕ⱗ穙㽛㽝๟ౡୣብ䡧ࡩ५乭ᙯᵱųᡵᱷ呹", a_));
				}
			}
			this.\u171B = this.\u171C.ᜅ;
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x000DA450 File Offset: 0x000D9450
		internal void ᜀ(IList<BiffRecordRaw> A_0, ref int A_1)
		{
			int a_ = 13;
			switch (0)
			{
			default:
			{
				int num = 28;
				for (;;)
				{
					int num2;
					int num3;
					BiffRecordRaw biffRecordRaw;
					switch (num)
					{
					case 0:
					{
						TBIFFRecord typeCode;
						switch (typeCode)
						{
						case (TBIFFRecord)2128:
						case TBIFFRecord.ChartWrapper:
						case (TBIFFRecord)2130:
						case (TBIFFRecord)2131:
						case TBIFFRecord.ChartBegDispUnit:
						case TBIFFRecord.ChartEndDispUnit:
							goto IL_604;
						default:
							num = 6;
							continue;
						}
						break;
					}
					case 1:
						goto IL_604;
					case 2:
						num = 9;
						continue;
					case 3:
					{
						TBIFFRecord typeCode;
						if (typeCode != TBIFFRecord.ChartBoppop)
						{
							num = 17;
							continue;
						}
						goto IL_5EC;
					}
					case 4:
						goto IL_1E9;
					case 5:
					{
						TBIFFRecord typeCode;
						if (typeCode <= TBIFFRecord.ChartLineFormat)
						{
							num = 26;
							continue;
						}
						num = 23;
						continue;
					}
					case 6:
						num = 30;
						continue;
					case 7:
						goto IL_604;
					case 8:
						goto IL_604;
					case 9:
					{
						TBIFFRecord typeCode;
						switch (typeCode)
						{
						case TBIFFRecord.Begin:
							A_1 = BiffRecordRaw.SkipBeginEndBlock(A_0, A_1) - 1;
							num = 1;
							continue;
						case TBIFFRecord.End:
							num2--;
							num = 19;
							continue;
						default:
							num = 31;
							continue;
						}
						break;
					}
					case 10:
					{
						if (num3 > 1)
						{
							num = 20;
							continue;
						}
						XlsChartDropBar xlsChartDropBar = new ChartDropBar((spr\u2158)base.ReservedHandle, this);
						xlsChartDropBar.ᜀ(A_0, ref A_1);
						num = 18;
						continue;
					}
					case 11:
						goto IL_604;
					case 12:
					{
						TBIFFRecord typeCode;
						switch (typeCode)
						{
						case TBIFFRecord.ChartLegend:
							this.\u171B.ᜊ(A_0, ref A_1);
							A_1--;
							num = 24;
							continue;
						case TBIFFRecord.ChartSeriesList:
							this.\u171A = (spr\u2274)biffRecordRaw;
							num = 41;
							continue;
						case TBIFFRecord.ChartBar:
						case TBIFFRecord.ChartLine:
						case TBIFFRecord.ChartPie:
						case TBIFFRecord.ChartArea:
						case TBIFFRecord.ChartScatter:
							goto IL_5EC;
						case TBIFFRecord.ChartChartLine:
							this.\u1716 = (spr\u233F)biffRecordRaw;
							num = 4;
							continue;
						case TBIFFRecord.ChartAxis:
						case TBIFFRecord.ChartTick:
						case TBIFFRecord.ChartValueRange:
						case TBIFFRecord.ChartCatserRange:
						case TBIFFRecord.ChartAxisLineFormat:
							goto IL_604;
						case TBIFFRecord.ChartFormatLink:
							this.\u1714 = (spr\u1CE1)biffRecordRaw;
							num = 22;
							continue;
						default:
							num = 2;
							continue;
						}
						break;
					}
					case 13:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1E9;
						default:
							if (false)
							{
							}
							num = 8;
							continue;
						}
						break;
					case 14:
						goto IL_604;
					case 15:
						goto IL_2AC;
					case 16:
					{
						XlsChartDropBar xlsChartDropBar;
						this.\u1718 = xlsChartDropBar;
						num = 40;
						continue;
					}
					case 17:
						num = 14;
						continue;
					case 18:
					{
						if (num3 == 0)
						{
							num = 16;
							continue;
						}
						XlsChartDropBar xlsChartDropBar;
						this.\u1719 = xlsChartDropBar;
						num = 29;
						continue;
					}
					case 19:
						goto IL_604;
					case 20:
						goto IL_3B5;
					case 21:
						num = 3;
						continue;
					case 22:
						goto IL_604;
					case 23:
					{
						TBIFFRecord typeCode;
						if (typeCode <= TBIFFRecord.End)
						{
							num = 42;
							continue;
						}
						num = 37;
						continue;
					}
					case 24:
						goto IL_604;
					case 25:
						goto IL_2AC;
					case 26:
						num = 0;
						continue;
					case 27:
					{
						TBIFFRecord typeCode;
						switch (typeCode)
						{
						case TBIFFRecord.ChartDataFormat:
							this.\u1717 = new ChartSerieDataFormat((spr\u2158)base.ReservedHandle, this);
							A_1 = this.\u1717.ᜀ(A_0, A_1) - 1;
							num = 11;
							continue;
						case TBIFFRecord.ChartLineFormat:
							this.\u171D = new ChartBorder((spr\u2158)base.ReservedHandle, this, (spr\u22F3)biffRecordRaw);
							num = 7;
							continue;
						default:
							num = 13;
							continue;
						}
						break;
					}
					case 29:
						goto IL_381;
					case 30:
					{
						TBIFFRecord typeCode;
						switch (typeCode)
						{
						case (TBIFFRecord)2154:
							goto IL_604;
						case TBIFFRecord.ChartDataLabels:
							this.\u1715 = (sprᨻ)biffRecordRaw;
							num = 36;
							continue;
						default:
							num = 33;
							continue;
						}
						break;
					}
					case 31:
						num = 32;
						continue;
					case 32:
						goto IL_604;
					case 33:
						num = 27;
						continue;
					case 34:
						goto IL_FA;
					case 35:
					{
						if (num2 == 0)
						{
							num = 38;
							continue;
						}
						biffRecordRaw = A_0[A_1];
						TBIFFRecord typeCode = biffRecordRaw.TypeCode;
						num = 5;
						continue;
					}
					case 36:
						goto IL_604;
					case 37:
					{
						TBIFFRecord typeCode;
						switch (typeCode)
						{
						case TBIFFRecord.Chart3D:
							this.\u1713 = (spr\u2272)biffRecordRaw;
							num = 44;
							continue;
						case (TBIFFRecord)4155:
						case TBIFFRecord.ChartPicf:
							goto IL_604;
						case TBIFFRecord.ChartDropBar:
							num = 10;
							continue;
						case TBIFFRecord.ChartRadar:
						case TBIFFRecord.ChartSurface:
						case TBIFFRecord.ChartRadarArea:
							goto IL_5EC;
						default:
							num = 21;
							continue;
						}
						break;
					}
					case 38:
						return;
					case 39:
						goto IL_604;
					case 40:
						goto IL_381;
					case 41:
						goto IL_604;
					case 42:
						num = 12;
						continue;
					case 43:
						goto IL_604;
					case 44:
						goto IL_604;
					}
					if (true)
					{
					}
					if (A_0 == null)
					{
						num = 34;
						continue;
					}
					biffRecordRaw = A_0[A_1];
					biffRecordRaw.CheckTypeCode(TBIFFRecord.ChartChartFormat);
					this.ᜑ = (spr᪘)A_0[A_1];
					A_1++;
					biffRecordRaw = A_0[A_1];
					biffRecordRaw.CheckTypeCode(TBIFFRecord.Begin);
					A_1++;
					num2 = 1;
					num3 = 0;
					num = 15;
					continue;
					IL_2AC:
					num = 35;
					continue;
					IL_381:
					num3++;
					num = 39;
					continue;
					IL_5EC:
					this.\u1712 = biffRecordRaw;
					num = 43;
					continue;
					IL_604:
					A_1++;
					num = 25;
					continue;
					IL_1E9:
					goto IL_604;
				}
				IL_FA:
				throw new ArgumentNullException(RecordTableEnumerator.b("❂⑄㍆⡈", a_));
				IL_3B5:
				throw new spr\u2313(RecordTableEnumerator.b("݂い㝆╈≊⹌⹎═㙒ㅔ睖㩘㍚㱜ⵞᕠ䍢Ťᕦ٨᭪䵬൮ၰŲ啴ᕶᱸ孺᭼ၾꦆ", a_));
			}
			}
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x000DAA8C File Offset: 0x000D9A8C
		public void SerializeDataToList(RecordArrayList records)
		{
			int a_ = 13;
			int num = 22;
			for (;;)
			{
				switch (num)
				{
				case 0:
					records.ᜀ((BiffRecordRaw)this.\u1714.Clone());
					num = 14;
					continue;
				case 1:
					goto IL_268;
				case 2:
					records.ᜀ((spr\u2272)this.\u1713.Clone());
					num = 1;
					continue;
				case 3:
					goto IL_263;
				case 4:
					if (sprᨻ.ᜀ(this.\u1715, null))
					{
						num = 9;
						continue;
					}
					goto IL_404;
				case 5:
					if (spr\u2272.ᜀ(this.\u1713, null))
					{
						num = 2;
						continue;
					}
					goto IL_268;
				case 6:
					if (spr\u233F.ᜀ(this.\u1716, null))
					{
						num = 21;
						continue;
					}
					goto IL_18C;
				case 7:
					if (this.\u1714 != null)
					{
						num = 0;
						continue;
					}
					goto IL_29F;
				case 8:
					if (this.\u1719 != null)
					{
						num = 31;
						continue;
					}
					goto IL_327;
				case 9:
					records.ᜀ((sprᨻ)this.\u1715.Clone());
					num = 3;
					continue;
				case 10:
					goto IL_D9;
				case 11:
					goto IL_AD;
				case 12:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3E8;
					default:
						if (false)
						{
						}
						goto IL_1FC;
					}
					break;
				case 13:
					this.\u171B.ᜋ(records);
					num = 25;
					continue;
				case 14:
					goto IL_29F;
				case 15:
					goto IL_327;
				case 16:
					if (this.\u1717 != null)
					{
						num = 18;
						continue;
					}
					goto IL_1B7;
				case 17:
					if (this.\u1718 != null)
					{
						num = 28;
						continue;
					}
					goto IL_D9;
				case 18:
					this.\u1717.SerializeDataToList(records);
					num = 23;
					continue;
				case 19:
					this.\u171D.ᜀ(records);
					num = 24;
					continue;
				case 20:
					if (this.\u171D != null)
					{
						if (true)
						{
						}
						num = 19;
						continue;
					}
					goto IL_142;
				case 21:
					records.ᜀ((BiffRecordRaw)this.\u1716.Clone());
					num = 30;
					continue;
				case 23:
					goto IL_1B7;
				case 24:
					goto IL_142;
				case 25:
					goto IL_353;
				case 26:
					records.ᜀ((BiffRecordRaw)this.\u171A.Clone());
					num = 12;
					continue;
				case 27:
					if (spr\u2274.ᜀ(this.\u171A, null))
					{
						num = 26;
						continue;
					}
					goto IL_1FC;
				case 28:
					this.\u1718.SerializeDataToList(records);
					num = 10;
					continue;
				case 29:
					if (this.DrawingZOrder == 0)
					{
						num = 13;
						continue;
					}
					goto IL_353;
				case 30:
					goto IL_18C;
				case 31:
					goto IL_3E8;
				}
				if (records == null)
				{
					num = 11;
					continue;
				}
				records.ᜀ((BiffRecordRaw)this.ᜑ.Clone());
				records.ᜀ(spr\u175E.ᜀ(TBIFFRecord.Begin));
				records.ᜀ((BiffRecordRaw)this.\u1712.Clone());
				num = 7;
				continue;
				IL_D9:
				num = 8;
				continue;
				IL_142:
				num = 16;
				continue;
				IL_18C:
				num = 20;
				continue;
				IL_1B7:
				num = 4;
				continue;
				IL_1FC:
				num = 5;
				continue;
				IL_268:
				num = 29;
				continue;
				IL_29F:
				num = 27;
				continue;
				IL_327:
				num = 6;
				continue;
				IL_353:
				num = 17;
				continue;
				IL_3E8:
				this.\u1719.SerializeDataToList(records);
				num = 15;
			}
			IL_AD:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄⑆♈㥊⥌㱎", a_));
			IL_263:
			IL_404:
			records.ᜀ(spr\u175E.ᜀ(TBIFFRecord.End));
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x000DAEB0 File Offset: 0x000D9EB0
		internal static string ᜉ(ExcelChartType A_0)
		{
			int a_ = 18;
			int num = 22;
			for (;;)
			{
				string text;
				switch (num)
				{
				case 0:
					if (text.StartsWith(RecordTableEnumerator.b("े㡉⥋⽍", a_)))
					{
						num = 30;
						continue;
					}
					goto IL_3F6;
				case 1:
					goto IL_527;
				case 2:
					if (text.StartsWith(RecordTableEnumerator.b("େ㍉⁋❍㹏㙑ㅓ⑕", a_)))
					{
						num = 16;
						continue;
					}
					goto IL_193;
				case 3:
					if (text.StartsWith(RecordTableEnumerator.b("ੇ⭉㹋", a_)))
					{
						num = 33;
						continue;
					}
					goto IL_31F;
				case 4:
					text = RecordTableEnumerator.b("ч⍉≋⭍", a_);
					num = 31;
					continue;
				case 5:
					goto IL_45B;
				case 6:
					if (text.StartsWith(RecordTableEnumerator.b("ч⍉≋⭍", a_)))
					{
						num = 4;
						continue;
					}
					goto IL_26A;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1F6;
					default:
						if (false)
						{
						}
						text = RecordTableEnumerator.b("ᭇ㽉㹋⡍ㅏㅑㅓ", a_);
						num = 5;
						continue;
					}
					break;
				case 8:
					if (text.StartsWith(RecordTableEnumerator.b("ᡇ㍉㹋⽍㵏㭑こ", a_)))
					{
						num = 34;
						continue;
					}
					goto IL_5D9;
				case 9:
					goto IL_F0;
				case 10:
					if (text.StartsWith(RecordTableEnumerator.b("େ╉≋⭍", a_)))
					{
						num = 11;
						continue;
					}
					goto IL_29E;
				case 11:
					text = RecordTableEnumerator.b("େ╉≋⭍", a_);
					num = 29;
					continue;
				case 12:
					goto IL_31F;
				case 13:
					goto IL_EB;
				case 14:
					goto IL_124;
				case 15:
					text = RecordTableEnumerator.b("େ╉⁋㭍㵏㱑", a_);
					num = 27;
					continue;
				case 16:
					text = RecordTableEnumerator.b("େ㍉⁋❍㹏㙑ㅓ⑕", a_);
					num = 45;
					continue;
				case 17:
					if (text.StartsWith(RecordTableEnumerator.b("ᭇ㹉⍋ⵍ㭏", a_)))
					{
						num = 38;
						continue;
					}
					goto IL_3C2;
				case 18:
					if (text.StartsWith(RecordTableEnumerator.b("ᭇ㽉㹋⡍ㅏㅑㅓ", a_)))
					{
						num = 7;
						continue;
					}
					goto IL_45B;
				case 19:
					text = RecordTableEnumerator.b("େ╉⅋ⱍ㥏㱑㕓≕ㅗ㕙㉛", a_);
					num = 43;
					continue;
				case 20:
					text = RecordTableEnumerator.b("ే╉㥋⥍㡏㱑⅓≕", a_);
					num = 1;
					continue;
				case 21:
					if (true)
					{
					}
					goto IL_5D9;
				case 23:
					if (text.StartsWith(RecordTableEnumerator.b("େ╉⅋ⱍ㥏㱑㕓≕ㅗ㕙㉛", a_)))
					{
						num = 19;
						continue;
					}
					return text;
				case 24:
					text = RecordTableEnumerator.b("ᡇ⍉⥋", a_);
					num = 39;
					continue;
				case 25:
					if (text.StartsWith(RecordTableEnumerator.b("ᭇ⥉ⵋ㩍⑏㝑♓", a_)))
					{
						num = 42;
						continue;
					}
					goto IL_42A;
				case 26:
					goto IL_3F6;
				case 27:
					goto IL_1C7;
				case 28:
					text = RecordTableEnumerator.b("ੇ㽉⹋ⱍ㱏㝑", a_);
					num = 9;
					continue;
				case 29:
					goto IL_29E;
				case 30:
					text = RecordTableEnumerator.b("े㡉⥋⽍", a_);
					num = 26;
					continue;
				case 31:
					goto IL_26A;
				case 32:
					if (text.StartsWith(RecordTableEnumerator.b("ᩇ⭉⡋⽍≏", a_)))
					{
						num = 40;
						continue;
					}
					goto IL_124;
				case 33:
					goto IL_1F6;
				case 34:
					text = RecordTableEnumerator.b("ᡇ㍉㹋⽍㵏㭑こ", a_);
					num = 21;
					continue;
				case 35:
					goto IL_42A;
				case 36:
					goto IL_3C2;
				case 37:
					if (text.StartsWith(RecordTableEnumerator.b("ᡇ⍉⥋", a_)))
					{
						num = 24;
						continue;
					}
					goto IL_36F;
				case 38:
					text = RecordTableEnumerator.b("ᭇ㹉⍋ⵍ㭏", a_);
					num = 36;
					continue;
				case 39:
					goto IL_36F;
				case 40:
					text = RecordTableEnumerator.b("ᩇ⭉⡋⽍≏", a_);
					num = 14;
					continue;
				case 41:
					if (text.StartsWith(RecordTableEnumerator.b("ੇ㽉⹋ⱍ㱏㝑", a_)))
					{
						num = 28;
						continue;
					}
					goto IL_F0;
				case 42:
					text = RecordTableEnumerator.b("ᭇ⥉ⵋ㩍⑏㝑♓", a_);
					num = 35;
					continue;
				case 43:
					return text;
				case 44:
					if (text.StartsWith(RecordTableEnumerator.b("େ╉⁋㭍㵏㱑", a_)))
					{
						num = 15;
						continue;
					}
					goto IL_1C7;
				case 45:
					goto IL_193;
				case 46:
					if (text.StartsWith(RecordTableEnumerator.b("ే╉㥋⥍㡏㱑⅓≕", a_)))
					{
						num = 20;
						continue;
					}
					goto IL_527;
				}
				if (A_0 == ExcelChartType.PieOfPie)
				{
					num = 13;
					continue;
				}
				text = A_0.ToString();
				num = 44;
				continue;
				IL_F0:
				num = 17;
				continue;
				IL_124:
				num = 18;
				continue;
				IL_193:
				num = 10;
				continue;
				IL_1C7:
				num = 3;
				continue;
				IL_1F6:
				text = RecordTableEnumerator.b("ੇ⭉㹋", a_);
				num = 12;
				continue;
				IL_26A:
				num = 37;
				continue;
				IL_29E:
				num = 8;
				continue;
				IL_31F:
				num = 6;
				continue;
				IL_36F:
				num = 25;
				continue;
				IL_3C2:
				num = 2;
				continue;
				IL_3F6:
				num = 46;
				continue;
				IL_42A:
				num = 0;
				continue;
				IL_45B:
				num = 41;
				continue;
				IL_527:
				num = 32;
				continue;
				IL_5D9:
				num = 23;
			}
			IL_EB:
			return RecordTableEnumerator.b("ᡇ⍉⥋", a_);
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x000DB4C8 File Offset: 0x000DA4C8
		internal void ᜃ(ExcelChartType A_0, bool A_1)
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
			this.ᜂ(A_0, A_1);
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x000DB50C File Offset: 0x000DA50C
		private void ᜁ()
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
			this.\u1713 = null;
			this.\u1716 = null;
			this.\u171D = null;
			this.\u1717 = null;
			this.\u1715 = null;
			this.\u1718 = null;
			this.\u1719 = null;
			this.\u171A = null;
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x000DB580 File Offset: 0x000DA580
		private void ᜀ()
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
			ExcelChartType destinationType = this.\u171B.DestinationType;
			this.\u171B.DestinationType = ExcelChartType.Line;
			this.\u1712 = spr\u175E.ᜀ(TBIFFRecord.ChartLine);
			this.\u171D = new ChartBorder((spr\u2158)base.ReservedHandle, this);
			this.\u171D.Weight = ChartLineWeightType.Hairline;
			this.\u171D.KnownColor = (ExcelColors)79;
			this.LineStyle = DropLineStyleType.HiLow;
			IChartBorder lineProperties = this.SerieDataFormat.LineProperties;
			lineProperties.Weight = ChartLineWeightType.Hairline;
			lineProperties.Pattern = ChartLinePatternType.None;
			lineProperties.KnownColor = (ExcelColors)79;
			this.\u1717.SeriesNumber = 65533;
			this.\u1717.MarkerStyle = ChartMarkerType.None;
			this.\u1717.MarkerForegroundKnownColor = (ExcelColors)77;
			this.\u1717.MarkerBackgroundKnownColor = (ExcelColors)77;
			this.\u1717.IsAutoMarker = false;
			this.\u171B.PrimaryCategoryAxis.AxisBetweenCategories = true;
			IChartGridLine majorGridLines = this.\u171B.PrimaryValueAxis.MajorGridLines;
			this.\u171B.DestinationType = destinationType;
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x000DB6B8 File Offset: 0x000DA6B8
		internal void \u1713()
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
			ExcelChartType destinationType = this.\u171B.DestinationType;
			this.\u171B.DestinationType = ExcelChartType.Line;
			this.ᜀ();
			XlsChartDataPoint xlsChartDataPoint = (XlsChartDataPoint)this.\u171B.Series[2].DataPoints.DefaultDataPoint;
			xlsChartDataPoint.ChangeChartStockHigh_Low_CloseType();
			this.\u171B.DestinationType = destinationType;
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x000DB748 File Offset: 0x000DA748
		internal void ᜏ()
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
			this.ᜀ();
			this.FirstDropBar.GapWidth = 150;
			IChartBorder lineProperties = this.\u1718.LineProperties;
			lineProperties.Pattern = ChartLinePatternType.Solid;
			lineProperties.Weight = ChartLineWeightType.Hairline;
			IChartInterior interior = this.\u1718.Interior;
			interior.Pattern = ExcelPatternType.Solid;
			interior.BackgroundKnownColor = (ExcelColors)79;
			interior.ForegroundKnownColor = ExcelColors.WhiteCustom;
			interior.BackgroundKnownColor = ExcelColors.Color0;
			this.\u1719 = this.\u1718.Clone(this);
			interior = this.\u1719.Interior;
			interior.ForegroundKnownColor = ExcelColors.Color0;
			interior.BackgroundKnownColor = ExcelColors.WhiteCustom;
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x000DB810 File Offset: 0x000DA810
		internal void \u1712()
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
			this.\u1712 = spr\u175E.ᜀ(TBIFFRecord.ChartBar);
			this.IsVaryColor = false;
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x000DB864 File Offset: 0x000DA864
		internal void \u1716()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					this.ᜀ();
					ushort[] a_ = new ushort[]
					{
						1,
						2,
						3,
						4
					};
					this.\u171A = (spr\u2274)spr\u175E.ᜀ(TBIFFRecord.ChartSeriesList);
					this.\u171A.ᜀ(a_);
					this.\u171B.SecondaryParentAxis.ᜁ(true);
					int num = 1;
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_12F;
						case 1:
							goto IL_12F;
						case 2:
							if (num == 3)
							{
								num2 = 5;
								continue;
							}
							goto IL_8D;
						case 3:
							return;
						case 4:
							goto IL_8D;
						case 5:
						{
							for (;;)
							{
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									goto IL_B8;
								}
							}
							IL_B8:
							if (false)
							{
							}
							XlsChartSerie xlsChartSerie;
							XlsChartDataPoint xlsChartDataPoint = (XlsChartDataPoint)xlsChartSerie.DataPoints.DefaultDataPoint;
							xlsChartDataPoint.ChangeChartStockVolume_High_Low_CloseType();
							num2 = 4;
							continue;
						}
						case 6:
						{
							if (num >= 4)
							{
								if (true)
								{
								}
								num2 = 3;
								continue;
							}
							XlsChartSerie xlsChartSerie = (XlsChartSerie)this.\u171B.Series[num];
							xlsChartSerie.ChartGroup = 1;
							num2 = 2;
							continue;
						}
						}
						break;
						IL_8D:
						num++;
						num2 = 0;
						continue;
						IL_12F:
						num2 = 6;
					}
				}
				return;
			}
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x000DB9C8 File Offset: 0x000DA9C8
		internal void \u1717()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					this.ᜏ();
					this.FirstDropBar.GapWidth = 100;
					this.SecondDropBar.GapWidth = 100;
					IChartInterior interior = this.\u1718.Interior;
					interior.ForegroundKnownColor = ExcelColors.WhiteCustom;
					interior.BackgroundKnownColor = ExcelColors.Color0;
					ExcelChartType destinationType = this.\u171B.DestinationType;
					this.\u171B.DestinationType = ExcelChartType.Line;
					((XlsChartSerieDataFormat)this.SerieDataFormat).SeriesNumber = 65533;
					this.\u171B.DestinationType = destinationType;
					this.SecondDropBar.Interior.BackgroundColor = Color.FromArgb(0, 255, 255, 255);
					this.\u171A = (spr\u2274)spr\u175E.ᜀ(TBIFFRecord.ChartSeriesList);
					ushort[] a_ = new ushort[]
					{
						1,
						2,
						3,
						4,
						5
					};
					this.\u171A.ᜀ(a_);
					int num = 1;
					int num2 = 2;
					for (;;)
					{
						switch (num2)
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
								goto IL_10C;
							}
							break;
						case 2:
							if (true)
							{
							}
							goto IL_10C;
						case 3:
						{
							if (num >= 5)
							{
								num2 = 0;
								continue;
							}
							XlsChartSerie xlsChartSerie = (XlsChartSerie)this.\u171B.Series[num];
							xlsChartSerie.ChartGroup = 1;
							num++;
							num2 = 1;
							continue;
						}
						}
						break;
						IL_10C:
						num2 = 3;
					}
				}
				return;
			}
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x000DBB58 File Offset: 0x000DAB58
		internal void ᜂ(ExcelChartType A_0, bool A_1)
		{
			int a_ = 6;
			if (true)
			{
			}
			for (;;)
			{
				int num = 3;
				for (;;)
				{
					string a;
					switch (num)
					{
					case 0:
						goto IL_318;
					case 1:
						num = 15;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_27B;
						default:
							if (false)
							{
							}
							this.\u171B.PrimaryCategoryAxis.AxisBetweenCategories = (!(a == RecordTableEnumerator.b("紻䰽┿⍁", a_)) && !(a == RecordTableEnumerator.b("漻䬽㈿⑁╃╅ⵇ", a_)));
							num = 5;
							continue;
						}
						break;
					case 3:
						switch (A_0)
						{
						case ExcelChartType.ColumnClustered:
						case ExcelChartType.Column3DClustered:
						case ExcelChartType.Column3D:
						case ExcelChartType.BarClustered:
						case ExcelChartType.Bar3DClustered:
							this.ᜆ(A_0);
							num = 6;
							continue;
						case ExcelChartType.ColumnStacked:
						case ExcelChartType.Column100PercentStacked:
						case ExcelChartType.Column3DStacked:
						case ExcelChartType.Column3D100PercentStacked:
						case ExcelChartType.BarStacked:
						case ExcelChartType.Bar100PercentStacked:
						case ExcelChartType.Bar3DStacked:
						case ExcelChartType.Bar3D100PercentStacked:
							this.ᜅ(A_0);
							num = 10;
							continue;
						case ExcelChartType.Line:
						case ExcelChartType.LineStacked:
						case ExcelChartType.Line100PercentStacked:
						case ExcelChartType.LineMarkers:
						case ExcelChartType.LineMarkersStacked:
						case ExcelChartType.LineMarkers100PercentStacked:
						case ExcelChartType.Line3D:
							this.ᜄ(A_0);
							num = 7;
							continue;
						case ExcelChartType.Pie:
						case ExcelChartType.Pie3D:
						case ExcelChartType.PieOfPie:
						case ExcelChartType.PieExploded:
						case ExcelChartType.Pie3DExploded:
						case ExcelChartType.PieBar:
							this.ᜃ(A_0);
							num = 9;
							continue;
						case ExcelChartType.ScatterMarkers:
						case ExcelChartType.ScatterSmoothedLineMarkers:
						case ExcelChartType.ScatterSmoothedLine:
						case ExcelChartType.ScatterLineMarkers:
						case ExcelChartType.ScatterLine:
							this.ᜁ(A_0);
							num = 4;
							continue;
						case ExcelChartType.Area:
						case ExcelChartType.AreaStacked:
						case ExcelChartType.Area100PercentStacked:
						case ExcelChartType.Area3D:
						case ExcelChartType.Area3DStacked:
						case ExcelChartType.Area3D100PercentStacked:
							this.ᜂ(A_0);
							num = 0;
							continue;
						case ExcelChartType.Doughnut:
						case ExcelChartType.DoughnutExploded:
							this.ᜈ(A_0);
							num = 11;
							continue;
						case ExcelChartType.Radar:
						case ExcelChartType.RadarMarkers:
						case ExcelChartType.RadarFilled:
							this.ᜇ(A_0);
							num = 16;
							continue;
						case ExcelChartType.Surface3D:
						case ExcelChartType.Surface3DNoColor:
						case ExcelChartType.SurfaceContour:
						case ExcelChartType.SurfaceContourNoColor:
							this.ᜀ(A_0, A_1);
							num = 14;
							continue;
						case ExcelChartType.Bubble:
						case ExcelChartType.Bubble3D:
							this.ᜁ(A_0, A_1);
							num = 17;
							continue;
						case ExcelChartType.StockHighLowClose:
						case ExcelChartType.StockOpenHighLowClose:
						case ExcelChartType.StockVolumeHighLowClose:
						case ExcelChartType.StockVolumeOpenHighLowClose:
							goto IL_2D3;
						case ExcelChartType.CylinderClustered:
						case ExcelChartType.CylinderStacked:
						case ExcelChartType.Cylinder100PercentStacked:
						case ExcelChartType.CylinderBarClustered:
						case ExcelChartType.CylinderBarStacked:
						case ExcelChartType.CylinderBar100PercentStacked:
						case ExcelChartType.Cylinder3DClustered:
						case ExcelChartType.ConeClustered:
						case ExcelChartType.ConeStacked:
						case ExcelChartType.Cone100PercentStacked:
						case ExcelChartType.ConeBarClustered:
						case ExcelChartType.ConeBarStacked:
						case ExcelChartType.ConeBar100PercentStacked:
						case ExcelChartType.Cone3DClustered:
						case ExcelChartType.PyramidClustered:
						case ExcelChartType.PyramidStacked:
						case ExcelChartType.Pyramid100PercentStacked:
						case ExcelChartType.PyramidBarClustered:
						case ExcelChartType.PyramidBarStacked:
						case ExcelChartType.PyramidBar100PercentStacked:
						case ExcelChartType.Pyramid3DClustered:
							this.ᜀ(A_0);
							num = 13;
							continue;
						default:
							num = 1;
							continue;
						}
						break;
					case 4:
						goto IL_318;
					case 5:
						return;
					case 6:
						goto IL_318;
					case 7:
						goto IL_318;
					case 8:
						if (!this.\u171B.ParentWorkbook.Loading)
						{
							num = 12;
							continue;
						}
						return;
					case 9:
						goto IL_318;
					case 10:
						goto IL_318;
					case 11:
						goto IL_27B;
					case 12:
						num = 2;
						continue;
					case 13:
						goto IL_318;
					case 14:
						goto IL_318;
					case 15:
						goto IL_25D;
					case 16:
						goto IL_318;
					case 17:
						goto IL_318;
					}
					break;
					IL_318:
					a = XlsChartFormat.ᜉ(A_0);
					num = 8;
					continue;
					IL_27B:
					goto IL_318;
				}
			}
			IL_25D:
			IL_2D3:
			throw new NotSupportedException(RecordTableEnumerator.b("缻嘽ℿⱁ⍃⍅桇㥉⥋㱍㥏㝑瑓≕⅗⩙㥛繝ٟ͡ൣ੥൧๩䉫", a_));
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x000DBED8 File Offset: 0x000DAED8
		private void ᜈ(ExcelChartType A_0)
		{
			for (;;)
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_74:
					if (true)
					{
					}
					this.SerieDataFormat.Percent = 25;
					num = 1;
					break;
				default:
					if (false)
					{
					}
					this.ᜁ();
					this.\u1712 = (spr\u1B77)spr\u175E.ᜀ(TBIFFRecord.ChartPie);
					this.ᜑ.ᜀ(true);
					this.DoughnutHoleSize = 50;
					num = 2;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_74;
					case 1:
						return;
					case 2:
						if (A_0 == ExcelChartType.DoughnutExploded)
						{
							num = 0;
							continue;
						}
						return;
					}
					break;
				}
			}
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x000DBF88 File Offset: 0x000DAF88
		private void ᜁ(ExcelChartType A_0, bool A_1)
		{
			int a_ = 11;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					this.ᜀ(this.\u171B.Series);
					num = 7;
					continue;
				case 2:
					if (!A_1)
					{
						num = 1;
						continue;
					}
					return;
				case 3:
					goto IL_114;
				case 4:
					if (true)
					{
					}
					num = 9;
					continue;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					case 1:
						goto IL_E8;
					default:
						goto IL_E8;
					}
					IL_154:
					num = 8;
					continue;
					IL_E8:
					if (false)
					{
					}
					this.SerieDataFormat.Is3DBubbles = true;
					goto IL_154;
				case 6:
					if (A_0 == ExcelChartType.Bubble3D)
					{
						num = 5;
						continue;
					}
					goto IL_61;
				case 7:
					return;
				case 8:
					goto IL_61;
				case 9:
					if (!A_1)
					{
						num = 3;
						continue;
					}
					goto IL_7C;
				}
				if (this.\u171B.Series.Count < 2)
				{
					num = 4;
					continue;
				}
				goto IL_7C;
				IL_61:
				num = 2;
				continue;
				IL_7C:
				this.ᜁ();
				this.\u1712 = (spr\u1AB2)spr\u175E.ᜀ(TBIFFRecord.ChartScatter);
				this.SizeRepresents = BubbleSizeType.Area;
				this.BubbleScale = 100;
				this.IsBubbles = true;
				num = 6;
			}
			IL_114:
			throw new ArgumentException(RecordTableEnumerator.b("ɀ⭂⑄⥆⹈⹊浌ⱎ㥐㉒❔⍖祘⽚⑜⽞Ѡ䍢ͤ٦hݪ࡬୮彰", a_));
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x000DC0FC File Offset: 0x000DB0FC
		private void ᜀ(IChartSeries A_0)
		{
			int a_ = 13;
			switch (0)
			{
			default:
			{
				int num = 8;
				for (;;)
				{
					int num2;
					XlsChartSeries xlsChartSeries;
					int count;
					int num3;
					object[] array;
					int num5;
					int num6;
					switch (num)
					{
					case 0:
						goto IL_9A;
					case 1:
					{
						if (num2 >= xlsChartSeries.Count - 1)
						{
							num = 9;
							continue;
						}
						XlsChartSerie xlsChartSerie = (XlsChartSerie)xlsChartSeries[num2];
						IChartSerie chartSerie = xlsChartSeries[num2 + 1];
						xlsChartSerie.Bubbles = chartSerie.Values;
						xlsChartSerie.Index = num2;
						xlsChartSerie.Number = num2;
						xlsChartSeries.RemoveAt(num2 + 1);
						num2--;
						num2 += 2;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_130;
						default:
							if (false)
							{
							}
							num = 11;
							continue;
						}
						break;
					}
					case 2:
						num = 15;
						continue;
					case 3:
						if (count % 2 != 0)
						{
							num = 16;
							continue;
						}
						return;
					case 4:
					{
						IXLSRange values;
						num3 = Math.Max(values.LastRow - values.Row + 1, values.LastColumn - values.Column + 1);
						goto IL_20E;
					}
					case 5:
						return;
					case 6:
					{
						int num4 = xlsChartSeries.Count - 1;
						XlsChartSerie xlsChartSerie2 = (XlsChartSerie)xlsChartSeries[xlsChartSeries.Count - 1];
						xlsChartSerie2.EnteredDirectlyBubbles = array;
						xlsChartSerie2.Index = num4;
						xlsChartSerie2.Number = num4;
						num = 5;
						continue;
					}
					case 7:
						goto IL_84;
					case 9:
						goto IL_130;
					case 10:
					{
						IChartSerie chartSerie2;
						if (chartSerie2.Values == null)
						{
							num = 2;
							continue;
						}
						num = 4;
						continue;
					}
					case 11:
						goto IL_9A;
					case 12:
						if (num5 >= num6)
						{
							num = 6;
							continue;
						}
						array[num5] = 0;
						num5++;
						num = 14;
						continue;
					case 13:
						goto IL_297;
					case 14:
						goto IL_297;
					case 15:
					{
						IChartSerie chartSerie2;
						num3 = chartSerie2.EnteredDirectlyValues.Length;
						goto IL_20E;
					}
					case 16:
					{
						IChartSerie chartSerie2 = xlsChartSeries[0];
						IXLSRange values = chartSerie2.Values;
						num = 10;
						continue;
					}
					}
					if (true)
					{
					}
					if (A_0 == null)
					{
						num = 7;
						continue;
					}
					xlsChartSeries = (XlsChartSeries)A_0;
					count = this.\u171B.Series.Count;
					num2 = 0;
					num = 0;
					continue;
					IL_9A:
					num = 1;
					continue;
					IL_130:
					num = 3;
					continue;
					IL_20E:
					num6 = num3;
					array = new object[num6];
					num5 = 0;
					num = 13;
					continue;
					IL_297:
					num = 12;
				}
				IL_84:
				throw new ArgumentNullException(RecordTableEnumerator.b("あ⁄㕆⁈⹊㹌", a_));
			}
			}
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x000DC3C8 File Offset: 0x000DB3C8
		private void ᜀ(ExcelChartType A_0, bool A_1)
		{
			int a_ = 8;
			int num = 12;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_FA;
				case 1:
					num = 6;
					continue;
				case 2:
					num = 9;
					continue;
				case 3:
					if (A_0 != ExcelChartType.Surface3D)
					{
						num = 11;
						continue;
					}
					goto IL_AA;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_67;
					default:
						if (false)
						{
						}
						if (A_0 != ExcelChartType.SurfaceContourNoColor)
						{
							num = 1;
							continue;
						}
						goto IL_D2;
					}
					break;
				case 5:
					if (A_0 == ExcelChartType.SurfaceContour)
					{
						num = 7;
						continue;
					}
					goto IL_74;
				case 6:
					if (A_0 == ExcelChartType.SurfaceContour)
					{
						num = 13;
						continue;
					}
					return;
				case 7:
					goto IL_AA;
				case 8:
					goto IL_120;
				case 9:
					if (true)
					{
					}
					if (!A_1)
					{
						num = 8;
						continue;
					}
					goto IL_16C;
				case 10:
					goto IL_74;
				case 11:
					num = 5;
					continue;
				case 13:
					goto IL_D2;
				}
				goto IL_51;
				IL_67:
				num = 2;
				continue;
				IL_51:
				if (this.\u171B.Series.Count < 2)
				{
					goto IL_67;
				}
				goto IL_16C;
				IL_74:
				num = 4;
				continue;
				IL_AA:
				this.IsFillSurface = true;
				num = 10;
				continue;
				IL_D2:
				this.Rotation = 0;
				this.Elevation = 90;
				this.Perspective = 0;
				this.IsVaryColor = false;
				num = 0;
				continue;
				IL_16C:
				this.ᜁ();
				this.\u1712 = (sprᨺ)spr\u175E.ᜀ(TBIFFRecord.ChartSurface);
				this.RightAngleAxes = false;
				num = 3;
			}
			IL_FA:
			return;
			IL_120:
			throw new ArgumentException(RecordTableEnumerator.b("紽⠿⍁㙃㉅桇㥉⑋⅍╏㹑こ癕㩗㽙籛㵝ཟౡၣݥŧѩ䱫ͭὯqᅳ噵౷ቹᵻၽꁿ낁ꒃ벑", a_));
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x000DC584 File Offset: 0x000DB584
		private void ᜇ(ExcelChartType A_0)
		{
			for (;;)
			{
				this.ᜁ();
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						if (A_0 == ExcelChartType.RadarFilled)
						{
							num = 3;
							continue;
						}
						if (true)
						{
						}
						sprẨ sprẨ = (sprẨ)spr\u175E.ᜀ(TBIFFRecord.ChartRadar);
						sprẨ.ᜀ(true);
						this.\u1712 = sprẨ;
						num = 2;
						continue;
					}
					case 1:
						goto IL_82;
					case 2:
						if (A_0 == ExcelChartType.Radar)
						{
							num = 4;
							continue;
						}
						return;
					case 3:
						goto IL_37;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_CA;
						default:
							if (false)
							{
							}
							this.HasRadarAxisLabels = true;
							((XlsChartSerieDataFormat)this.SerieDataFormat).ChangeRadarDataFormat(A_0);
							num = 1;
							continue;
						}
						break;
					}
					break;
				}
			}
			IL_37:
			goto IL_CA;
			IL_82:
			return;
			IL_CA:
			sprᶗ sprᶗ = (sprᶗ)spr\u175E.ᜀ(TBIFFRecord.ChartRadarArea);
			sprᶗ.ᜀ(true);
			this.\u1712 = sprᶗ;
			this.IsCategoryName = true;
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x000DC684 File Offset: 0x000DB684
		private void ᜆ(ExcelChartType A_0)
		{
			int a_ = 1;
			for (;;)
			{
				this.ᜁ();
				this.\u1712 = (spr\u204B)spr\u175E.ᜀ(TBIFFRecord.ChartBar);
				int num = 6;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						num = 2;
						continue;
					case 2:
						if (A_0 == ExcelChartType.Bar3DClustered)
						{
							num = 3;
							continue;
						}
						goto IL_B1;
					case 3:
						goto IL_104;
					case 4:
					{
						if (A_0 == ExcelChartType.Column3D)
						{
							num = 8;
							continue;
						}
						string text = A_0.ToString();
						num = 7;
						continue;
					}
					case 5:
						goto IL_B1;
					case 6:
						if (A_0 != ExcelChartType.Column3DClustered)
						{
							num = 1;
							continue;
						}
						goto IL_122;
					case 7:
					{
						string text;
						if (text.StartsWith(RecordTableEnumerator.b("甶堸䤺", a_)))
						{
							num = 9;
							continue;
						}
						return;
					}
					case 8:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_104;
						default:
							goto IL_E1;
						}
						break;
					case 9:
						this.IsHorizontalBar = true;
						num = 0;
						continue;
					}
					break;
					IL_B1:
					num = 4;
					continue;
					IL_122:
					this.IsClustered = true;
					num = 5;
					continue;
					IL_104:
					if (true)
					{
					}
					goto IL_122;
				}
			}
			IL_E1:
			if (false)
			{
			}
			this.RightAngleAxes = false;
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x000DC7D4 File Offset: 0x000DB7D4
		private void ᜅ(ExcelChartType A_0)
		{
			for (;;)
			{
				this.ᜆ(A_0);
				this.StackValuesBar = true;
				this.BarRecord.ᜀ(-65436);
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 2;
						continue;
					case 1:
						if (A_0 != ExcelChartType.Column100PercentStacked)
						{
							num = 0;
							continue;
						}
						goto IL_E5;
					case 2:
						if (A_0 == ExcelChartType.Bar100PercentStacked)
						{
							num = 3;
							continue;
						}
						num = 6;
						continue;
					case 3:
						goto IL_C5;
					case 4:
						return;
					case 5:
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
					case 6:
						switch (A_0)
						{
						case ExcelChartType.Column3DStacked:
							goto IL_C7;
						case ExcelChartType.Column3D100PercentStacked:
							goto IL_119;
						default:
							num = 5;
							continue;
						}
						break;
					case 7:
						switch (A_0)
						{
						case ExcelChartType.Bar3DStacked:
							goto IL_C7;
						case ExcelChartType.Bar3D100PercentStacked:
							goto IL_119;
						default:
							num = 4;
							continue;
						}
						break;
					}
					break;
				}
			}
			IL_C5:
			goto IL_E5;
			IL_C7:
			if (true)
			{
			}
			this.\u1713 = (spr\u2272)spr\u175E.ᜀ(TBIFFRecord.Chart3D);
			return;
			IL_E5:
			this.ShowAsPercentsBar = true;
			return;
			IL_119:
			this.\u1713 = (spr\u2272)spr\u175E.ᜀ(TBIFFRecord.Chart3D);
			this.ShowAsPercentsBar = true;
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x000DC918 File Offset: 0x000DB918
		private void ᜄ(ExcelChartType A_0)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_C5:
				num = 4;
				break;
			default:
				if (false)
				{
				}
				num = 21;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜁ();
					num = 25;
					continue;
				case 1:
					goto IL_128;
				case 2:
					goto IL_281;
				case 3:
					if (A_0 == ExcelChartType.Line3D)
					{
						num = 11;
						continue;
					}
					num = 9;
					continue;
				case 4:
					if (A_0 != ExcelChartType.Line)
					{
						num = 24;
						continue;
					}
					goto IL_128;
				case 5:
					goto IL_21E;
				case 6:
					return;
				case 7:
					if (A_0 != ExcelChartType.LineMarkers100PercentStacked)
					{
						num = 23;
						continue;
					}
					goto IL_2A1;
				case 8:
					num = 13;
					continue;
				case 9:
					if (A_0 != ExcelChartType.LineMarkersStacked)
					{
						num = 8;
						continue;
					}
					goto IL_1B8;
				case 10:
					if (A_0 == ExcelChartType.Line100PercentStacked)
					{
						num = 1;
						continue;
					}
					goto IL_198;
				case 11:
					goto IL_123;
				case 12:
					if (A_0 == ExcelChartType.Area3D)
					{
						num = 22;
						continue;
					}
					return;
				case 13:
					if (A_0 == ExcelChartType.LineStacked)
					{
						num = 17;
						continue;
					}
					goto IL_281;
				case 14:
					if (A_0 == ExcelChartType.LineMarkers)
					{
						num = 6;
						continue;
					}
					num = 3;
					continue;
				case 15:
					if (A_0 != ExcelChartType.LineStacked)
					{
						num = 18;
						continue;
					}
					goto IL_128;
				case 16:
					goto IL_2BA;
				case 17:
					goto IL_1B8;
				case 18:
					num = 10;
					continue;
				case 19:
					goto IL_198;
				case 20:
					goto IL_2A1;
				case 22:
					this.RightAngleAxes = false;
					num = 5;
					continue;
				case 23:
					num = 26;
					continue;
				case 24:
					num = 15;
					continue;
				case 25:
					goto IL_146;
				case 26:
					if (A_0 == ExcelChartType.Line100PercentStacked)
					{
						num = 20;
						continue;
					}
					goto IL_C5;
				}
				if (true)
				{
				}
				if (!this.\u171B.ParentWorkbook.Loading)
				{
					num = 0;
					continue;
				}
				goto IL_146;
				IL_128:
				((XlsChartSerieDataFormat)this.SerieDataFormat).ChangeLineDataFormat(A_0);
				num = 19;
				continue;
				IL_146:
				this.\u1712 = (sprᯙ)spr\u175E.ᜀ(TBIFFRecord.ChartLine);
				num = 14;
				continue;
				IL_198:
				num = 12;
				continue;
				IL_1B8:
				this.StackValuesLine = true;
				num = 2;
				continue;
				IL_281:
				num = 7;
				continue;
				IL_2A1:
				this.StackValuesLine = true;
				this.ShowAsPercentsLine = true;
				num = 16;
			}
			IL_123:
			this.\u1713 = (spr\u2272)spr\u175E.ᜀ(TBIFFRecord.Chart3D);
			this.RightAngleAxes = false;
			return;
			IL_21E:
			return;
			IL_2BA:
			goto IL_C5;
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x000DCBE4 File Offset: 0x000DBBE4
		private void ᜃ(ExcelChartType A_0)
		{
			for (;;)
			{
				this.ᜁ();
				this.IsVaryColor = true;
				this.\u1712 = (spr\u1B77)spr\u175E.ᜀ(TBIFFRecord.ChartPie);
				int num = 13;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (A_0 == ExcelChartType.PieOfPie)
						{
							num = 15;
							continue;
						}
						return;
					case 1:
						num = 8;
						continue;
					case 2:
						num = 4;
						continue;
					case 3:
						if (A_0 != ExcelChartType.PieExploded)
						{
							num = 2;
							continue;
						}
						goto IL_B5;
					case 4:
						if (A_0 == ExcelChartType.Pie3DExploded)
						{
							num = 7;
							continue;
						}
						goto IL_1AF;
					case 5:
						this.PieChartType = ChartPieType.Pie;
						goto IL_1D9;
					case 6:
						num = 0;
						continue;
					case 7:
						goto IL_B5;
					case 8:
						if (A_0 != ExcelChartType.Pie3DExploded)
						{
							goto IL_206;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1D9;
						default:
							if (false)
							{
							}
							num = 16;
							continue;
						}
						break;
					case 9:
						if (A_0 != ExcelChartType.PieBar)
						{
							num = 6;
							continue;
						}
						goto IL_F7;
					case 10:
						goto IL_206;
					case 11:
						if (A_0 == ExcelChartType.PieOfPie)
						{
							num = 5;
							continue;
						}
						return;
					case 12:
						return;
					case 13:
						if (A_0 != ExcelChartType.Pie3D)
						{
							num = 1;
							continue;
						}
						goto IL_D2;
					case 14:
						goto IL_1AF;
					case 15:
						goto IL_F7;
					case 16:
						goto IL_D2;
					}
					break;
					IL_B5:
					this.SerieDataFormat.Percent = 25;
					num = 14;
					continue;
					IL_D2:
					this.\u1713 = (spr\u2272)spr\u175E.ᜀ(TBIFFRecord.Chart3D);
					num = 10;
					continue;
					IL_F7:
					if (true)
					{
					}
					this.\u1712 = (spr\u2156)spr\u175E.ᜀ(TBIFFRecord.ChartBoppop);
					this.UseDefaultSplitValue = true;
					this.PieChartType = ChartPieType.Bar;
					this.PieSecondSize = 75;
					this.Gap = 100;
					this.LineStyle = DropLineStyleType.Series;
					this.\u171D = new ChartBorder((spr\u2158)base.ReservedHandle, this);
					num = 11;
					continue;
					IL_1AF:
					num = 9;
					continue;
					IL_1D9:
					num = 12;
					continue;
					IL_206:
					num = 3;
				}
			}
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x000DCE18 File Offset: 0x000DBE18
		private void ᜂ(ExcelChartType A_0)
		{
			for (;;)
			{
				this.ᜁ();
				this.\u1712 = (spr\u1D5A)spr\u175E.ᜀ(TBIFFRecord.ChartArea);
				int num = 16;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						num = 11;
						continue;
					case 1:
						return;
					case 2:
						goto IL_15E;
					case 3:
						num = 12;
						continue;
					case 4:
						if (A_0 == ExcelChartType.Area3D100PercentStacked)
						{
							goto IL_102;
						}
						return;
					case 5:
						if (A_0 != ExcelChartType.AreaStacked)
						{
							num = 0;
							continue;
						}
						goto IL_12C;
					case 6:
						num = 15;
						continue;
					case 7:
						goto IL_12C;
					case 8:
						goto IL_8F;
					case 9:
						goto IL_143;
					case 10:
						if (A_0 != ExcelChartType.Area100PercentStacked)
						{
							num = 14;
							continue;
						}
						goto IL_143;
					case 11:
						if (A_0 == ExcelChartType.Area3DStacked)
						{
							num = 7;
							continue;
						}
						goto IL_8F;
					case 12:
						if (A_0 == ExcelChartType.Area3D100PercentStacked)
						{
							num = 13;
							continue;
						}
						goto IL_15E;
					case 13:
						goto IL_17E;
					case 14:
						num = 4;
						continue;
					case 15:
						if (A_0 != ExcelChartType.Area3DStacked)
						{
							num = 3;
							continue;
						}
						goto IL_17E;
					case 16:
						if (A_0 != ExcelChartType.Area3D)
						{
							num = 6;
							continue;
						}
						goto IL_17E;
					}
					break;
					IL_8F:
					num = 10;
					continue;
					IL_102:
					num = 9;
					continue;
					IL_17E:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_102;
					}
					if (false)
					{
					}
					this.\u1713 = (spr\u2272)spr\u175E.ᜀ(TBIFFRecord.Chart3D);
					num = 2;
					continue;
					IL_12C:
					this.IsStacked = true;
					num = 8;
					continue;
					IL_143:
					this.IsStacked = true;
					this.IsCategoryBrokenDown = true;
					num = 1;
					continue;
					IL_15E:
					num = 5;
				}
			}
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x000DCFEC File Offset: 0x000DBFEC
		private void ᜁ(ExcelChartType A_0)
		{
			for (;;)
			{
				this.ᜁ();
				this.\u1712 = (spr\u1AB2)spr\u175E.ᜀ(TBIFFRecord.ChartScatter);
				this.SizeRepresents = BubbleSizeType.Area;
				this.BubbleScale = 100;
				if (A_0 == ExcelChartType.ScatterLineMarkers)
				{
					break;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_48;
				}
			}
			return;
			IL_48:
			if (false)
			{
			}
			if (true)
			{
			}
			((XlsChartSerieDataFormat)this.SerieDataFormat).ChangeScatterDataFormat(A_0);
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x000DD06C File Offset: 0x000DC06C
		private void ᜀ(ExcelChartType A_0)
		{
			int a_ = 8;
			BaseFormatType baseFormatType;
			TopFormatType topFormatType;
			for (;;)
			{
				switch (0)
				{
				default:
					for (;;)
					{
						int num = 10;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_1D9;
							case 1:
								goto IL_1D9;
							case 2:
								num = 0;
								continue;
							case 3:
								num = 9;
								continue;
							case 4:
								goto IL_1D9;
							case 5:
								goto IL_1D9;
							case 6:
								goto IL_1D9;
							case 7:
								goto IL_1D9;
							case 8:
								goto IL_1D4;
							case 9:
								goto IL_EF;
							case 10:
								switch (A_0)
								{
								case ExcelChartType.CylinderClustered:
								case ExcelChartType.ConeClustered:
								case ExcelChartType.PyramidClustered:
									this.ᜆ(ExcelChartType.Column3DClustered);
									num = 5;
									continue;
								case ExcelChartType.CylinderStacked:
								case ExcelChartType.ConeStacked:
								case ExcelChartType.PyramidStacked:
									this.ᜅ(ExcelChartType.Column3DStacked);
									num = 4;
									continue;
								case ExcelChartType.Cylinder100PercentStacked:
								case ExcelChartType.Cone100PercentStacked:
								case ExcelChartType.Pyramid100PercentStacked:
									this.ᜅ(ExcelChartType.Column3D100PercentStacked);
									num = 6;
									continue;
								case ExcelChartType.CylinderBarClustered:
								case ExcelChartType.ConeBarClustered:
								case ExcelChartType.PyramidBarClustered:
									this.ᜆ(ExcelChartType.Bar3DClustered);
									num = 7;
									continue;
								case ExcelChartType.CylinderBarStacked:
								case ExcelChartType.ConeBarStacked:
								case ExcelChartType.PyramidBarStacked:
									this.ᜅ(ExcelChartType.Bar3DStacked);
									num = 14;
									continue;
								case ExcelChartType.CylinderBar100PercentStacked:
								case ExcelChartType.ConeBar100PercentStacked:
								case ExcelChartType.PyramidBar100PercentStacked:
									this.ᜅ(ExcelChartType.Bar3D100PercentStacked);
									if (true)
									{
									}
									num = 13;
									continue;
								case ExcelChartType.Cylinder3DClustered:
								case ExcelChartType.Cone3DClustered:
								case ExcelChartType.Pyramid3DClustered:
									this.ᜆ(ExcelChartType.Column3D);
									num = 1;
									continue;
								default:
									num = 2;
									continue;
								}
								break;
							case 11:
								switch (A_0)
								{
								case ExcelChartType.CylinderClustered:
								case ExcelChartType.CylinderStacked:
								case ExcelChartType.Cylinder100PercentStacked:
								case ExcelChartType.CylinderBarClustered:
								case ExcelChartType.CylinderBarStacked:
								case ExcelChartType.CylinderBar100PercentStacked:
								case ExcelChartType.Cylinder3DClustered:
									baseFormatType = BaseFormatType.Circle;
									topFormatType = TopFormatType.Straight;
									num = 8;
									continue;
								case ExcelChartType.ConeClustered:
								case ExcelChartType.ConeStacked:
								case ExcelChartType.Cone100PercentStacked:
								case ExcelChartType.ConeBarClustered:
								case ExcelChartType.ConeBarStacked:
								case ExcelChartType.ConeBar100PercentStacked:
								case ExcelChartType.Cone3DClustered:
									baseFormatType = BaseFormatType.Circle;
									topFormatType = TopFormatType.Sharp;
									num = 15;
									continue;
								case ExcelChartType.PyramidClustered:
								case ExcelChartType.PyramidStacked:
								case ExcelChartType.Pyramid100PercentStacked:
								case ExcelChartType.PyramidBarClustered:
								case ExcelChartType.PyramidBarStacked:
								case ExcelChartType.PyramidBar100PercentStacked:
								case ExcelChartType.Pyramid3DClustered:
									baseFormatType = BaseFormatType.Rectangle;
									topFormatType = TopFormatType.Sharp;
									num = 12;
									continue;
								default:
									num = 3;
									continue;
								}
								break;
							case 12:
								goto IL_14D;
							case 13:
								goto IL_1D9;
							case 14:
								goto IL_1D9;
							case 15:
								goto IL_178;
							}
							break;
							IL_1D9:
							num = 11;
						}
					}
					IL_EF:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_105;
					}
					break;
				}
			}
			IL_105:
			if (false)
			{
			}
			throw new ArgumentException(RecordTableEnumerator.b("䨽㤿㉁⅃", a_));
			IL_14D:
			IL_178:
			IL_1D4:
			this.\u171B.Series.ᜀ(baseFormatType, topFormatType);
			this.SerieDataFormat.BarType = baseFormatType;
			this.SerieDataFormat.BarTopType = topFormatType;
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x000DD33C File Offset: 0x000DC33C
		public object Clone(object parent)
		{
			XlsChartFormat xlsChartFormat;
			for (;;)
			{
				xlsChartFormat = (XlsChartFormat)base.MemberwiseClone();
				xlsChartFormat.SetParent(parent);
				xlsChartFormat.\u1715();
				xlsChartFormat.ᜑ = (spr᪘)spr\u1CD3.ᜀ(this.ᜑ);
				xlsChartFormat.\u1712 = (BiffRecordRaw)spr\u1CD3.ᜀ(this.\u1712);
				xlsChartFormat.\u1713 = (spr\u2272)spr\u1CD3.ᜀ(this.\u1713);
				xlsChartFormat.\u1714 = (spr\u1CE1)spr\u1CD3.ᜀ(this.\u1714);
				xlsChartFormat.\u1715 = (sprᨻ)spr\u1CD3.ᜀ(this.\u1715);
				xlsChartFormat.\u1716 = (spr\u233F)spr\u1CD3.ᜀ(this.\u1716);
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_1E5;
					case 1:
						xlsChartFormat.\u1717 = this.\u1717.Clone(xlsChartFormat);
						num = 9;
						continue;
					case 2:
						if (this.\u1717 != null)
						{
							num = 1;
							continue;
						}
						return xlsChartFormat;
					case 3:
						goto IL_E0;
					case 4:
						xlsChartFormat.\u1719 = this.\u1719.Clone(xlsChartFormat);
						num = 0;
						continue;
					case 5:
						goto IL_122;
					case 6:
						if (this.\u1718 != null)
						{
							num = 11;
							continue;
						}
						goto IL_122;
					case 7:
						if (true)
						{
						}
						xlsChartFormat.\u171D = this.\u171D.Clone(xlsChartFormat);
						num = 8;
						continue;
					case 8:
						goto IL_145;
					case 9:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_E0;
						default:
							goto IL_1B1;
						}
						break;
					case 10:
						if (this.\u1719 != null)
						{
							num = 4;
							continue;
						}
						goto IL_1E5;
					case 11:
						xlsChartFormat.\u1718 = this.\u1718.Clone(xlsChartFormat);
						num = 5;
						continue;
					}
					break;
					IL_E0:
					if (this.\u171D != null)
					{
						num = 7;
						continue;
					}
					goto IL_145;
					IL_122:
					num = 10;
					continue;
					IL_145:
					xlsChartFormat.\u171A = (spr\u2274)spr\u1CD3.ᜀ(this.\u171A);
					num = 6;
					continue;
					IL_1E5:
					num = 2;
				}
			}
			IL_1B1:
			if (false)
			{
			}
			return xlsChartFormat;
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x000DD574 File Offset: 0x000DC574
		public static bool operator ==(XlsChartFormat format1, XlsChartFormat format2)
		{
			for (;;)
			{
				IL_00:
				switch (0)
				{
				default:
				{
					int num = 17;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (spr\u2274.ᜁ(format1.\u171A, format2.\u171A))
							{
								num = 7;
								continue;
							}
							return false;
						case 1:
							if (format1.ᜑ.ᜀ(format2.ᜑ))
							{
								num = 6;
								continue;
							}
							return false;
						case 2:
							return true;
						case 3:
						{
							if (format1.\u1712.TypeCode != format2.\u1712.TypeCode)
							{
								num = 4;
								continue;
							}
							int storeSize = format1.\u1712.GetStoreSize(ExcelVersion.Version97to2003);
							int storeSize2 = format1.\u1712.GetStoreSize(ExcelVersion.Version97to2003);
							num = 10;
							continue;
						}
						case 4:
							return false;
						case 5:
							if (true)
							{
							}
							num = 1;
							continue;
						case 6:
							num = 14;
							continue;
						case 7:
							num = 9;
							continue;
						case 8:
							num = 18;
							continue;
						case 9:
							if (spr\u233F.ᜁ(format1.\u1716, format2.\u1716))
							{
								num = 13;
								continue;
							}
							return false;
						case 10:
						{
							int storeSize;
							int storeSize2;
							if (storeSize != storeSize2)
							{
								num = 20;
								continue;
							}
							spr\u24E5 spr_u24E = new spr\u24E5(new byte[storeSize]);
							format1.\u1712.InfillInternalData(spr_u24E, 0, ExcelVersion.Version97to2003);
							spr\u24E5 spr_u24E2 = new spr\u24E5(new byte[storeSize2]);
							format2.\u1712.InfillInternalData(spr_u24E2, 0, ExcelVersion.Version97to2003);
							num = 12;
							continue;
						}
						case 11:
							if (!object.Equals(format1, null))
							{
								num = 19;
								continue;
							}
							return false;
						case 12:
						{
							spr\u24E5 spr_u24E;
							spr\u24E5 spr_u24E2;
							if (BiffRecordRaw.CompareArrays(spr_u24E.ᜅ(), spr_u24E2.ᜅ()))
							{
								num = 5;
								continue;
							}
							return false;
						}
						case 13:
							goto IL_203;
						case 14:
							if (!spr\u2272.ᜁ(format1.\u1713, format2.\u1713))
							{
								return false;
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
								num = 21;
								continue;
							}
							break;
						case 15:
							if (object.Equals(format2, null))
							{
								num = 16;
								continue;
							}
							num = 3;
							continue;
						case 16:
							goto IL_1D0;
						case 18:
							if (object.Equals(format2, null))
							{
								num = 2;
								continue;
							}
							goto IL_CB;
						case 19:
							num = 15;
							continue;
						case 20:
							return false;
						case 21:
							num = 0;
							continue;
						}
						if (object.Equals(format1, null))
						{
							num = 8;
							continue;
						}
						IL_CB:
						num = 11;
					}
					break;
				}
				}
			}
			IL_1D0:
			return false;
			IL_203:
			return sprᨻ.ᜁ(format1.\u1715, format2.\u1715);
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x000DD874 File Offset: 0x000DC874
		public static bool operator !=(XlsChartFormat format1, XlsChartFormat format2)
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
			return !(format1 == format2);
		}

		// Token: 0x04000F67 RID: 3943
		internal const int ᜀ = -65436;

		// Token: 0x04000F68 RID: 3944
		private const int ᜁ = 65533;

		// Token: 0x04000F69 RID: 3945
		internal const string ᜂ = "Column";

		// Token: 0x04000F6A RID: 3946
		private long[] \u2460\u0080\u008A\u00A8;

		// Token: 0x04000F6B RID: 3947
		internal const string ᜃ = "Bar";

		// Token: 0x04000F6C RID: 3948
		internal const string ᜄ = "Line";

		// Token: 0x04000F6D RID: 3949
		internal const string ᜅ = "Pie";

		// Token: 0x04000F6E RID: 3950
		internal const string ᜆ = "Scatter";

		// Token: 0x04000F6F RID: 3951
		internal const string ᜇ = "Area";

		// Token: 0x04000F70 RID: 3952
		internal const string ᜈ = "Doughnut";

		// Token: 0x04000F71 RID: 3953
		internal const string ᜉ = "Radar";

		// Token: 0x04000F72 RID: 3954
		internal const string ᜊ = "Surface";

		// Token: 0x04000F73 RID: 3955
		internal const string ᜋ = "Bubble";

		// Token: 0x04000F74 RID: 3956
		internal const string ᜌ = "Stock";

		// Token: 0x04000F75 RID: 3957
		internal const string \u170D = "Cylinder";

		// Token: 0x04000F76 RID: 3958
		internal const string ᜎ = "Cone";

		// Token: 0x04000F77 RID: 3959
		private string[] \u2609\u0081\u0094\u00A8;

		// Token: 0x04000F78 RID: 3960
		internal const string ᜏ = "Pyramid";

		// Token: 0x04000F79 RID: 3961
		internal const string ᜐ = "Combination";

		// Token: 0x04000F7A RID: 3962
		private spr᪘ ᜑ;

		// Token: 0x04000F7B RID: 3963
		private BiffRecordRaw \u1712;

		// Token: 0x04000F7C RID: 3964
		private spr\u2272 \u1713;

		// Token: 0x04000F7D RID: 3965
		private spr\u1CE1 \u1714;

		// Token: 0x04000F7E RID: 3966
		private sprᨻ \u1715;

		// Token: 0x04000F7F RID: 3967
		private spr\u233F \u1716;

		// Token: 0x04000F80 RID: 3968
		private XlsChartSerieDataFormat \u1717;

		// Token: 0x04000F81 RID: 3969
		private XlsChartDropBar \u1718;

		// Token: 0x04000F82 RID: 3970
		private XlsChartDropBar \u1719;

		// Token: 0x04000F83 RID: 3971
		private spr\u2274 \u171A;

		// Token: 0x04000F84 RID: 3972
		private XlsChart \u171B;

		// Token: 0x04000F85 RID: 3973
		private sprᾹ \u171C;

		// Token: 0x04000F86 RID: 3974
		private XlsChartBorder \u171D;
	}
}
