using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Charts
{
	// Token: 0x020001A5 RID: 421
	public class XlsChartTextArea : XlsObject, IChartDataLabels, spr\u1B6D, sprᮟ
	{
		// Token: 0x0600153A RID: 5434 RVA: 0x000C9334 File Offset: 0x000C8334
		[CLSCompliant(false)]
		internal static BiffRecordRaw ᜀ(BiffRecordRaw A_0)
		{
			int a_ = 13;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.TypeCode == TBIFFRecord.ChartWrapper)
					{
						num = 1;
						continue;
					}
					return A_0;
				case 1:
					goto IL_8F;
				case 2:
					goto IL_58;
				}
				if (A_0 == null)
				{
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_58;
					default:
						if (false)
						{
						}
						num = 2;
						break;
					}
				}
				else
				{
					num = 0;
				}
			}
			IL_58:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄⑆♈㥊⥌", a_));
			IL_8F:
			spr\u23F0 spr_u23F = (spr\u23F0)A_0;
			return spr_u23F.ᜀ();
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x000C93E8 File Offset: 0x000C83E8
		internal XlsChartTextArea(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜄ();
			XlsChart xlsChart = (XlsChart)base.FindParent(typeof(XlsChart));
			this.ᜀ(xlsChart.DefaultTextIndex);
			this.ᜁ.ᜄ(true);
			this.ᜁ.ᜉ(false);
			this.ᜁ.ᜁ(false);
			this.ᜁ.ᜈ(true);
			this.ᜁ.ᜀ(ChartHorzAlignmentType.Center);
			this.ᜁ.ᜀ(ChartVertAlignmentType.Center);
			this.ᜅ.ᜀ(ObjectTextLinkType.DataLabel);
			this.ᜇ = (sprᢀ)spr\u175E.ᜀ(TBIFFRecord.ChartAI);
			this.ᜇ.ᜀ(sprᢀ.ReferenceType.EnteredDirectly);
			this.ᜈ = (sprᜰ)spr\u175E.ᜀ(TBIFFRecord.ChartAlruns);
			this.ᜉ = (spr\u23BE)spr\u175E.ᜀ(TBIFFRecord.ChartPos);
			this.ᜉ.ᜁ(2);
			this.ᜉ.ᜀ(2);
			this.ᜌ = ChartParagraphType.None;
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x000C9524 File Offset: 0x000C8524
		internal XlsChartTextArea(spr\u1DF5 A_0, object A_1, ObjectTextLinkType A_2) : this(A_0, A_1)
		{
			this.ᜅ.ᜀ(A_2);
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x000C9548 File Offset: 0x000C8548
		internal XlsChartTextArea(spr\u1DF5 A_0, object A_1, IList<BiffRecordRaw> A_2, ref int A_3) : this(A_0, A_1)
		{
			A_3 = this.ᜀ(A_2, A_3);
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x000C956C File Offset: 0x000C856C
		private void ᜄ()
		{
			int a_ = 18;
			this.ᜂ = (base.FindParent(typeof(XlsWorkbook)) as XlsWorkbook);
			if (this.ᜂ != null)
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
					return;
				}
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("ᡇ⭉㹋⭍㹏♑瑓㥕㩗す㥛㵝ᑟ䉡ݣݥ٧ѩͫᩭ偯ၱᅳ噵ṷᕹॻၽ겁", a_));
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x0600153F RID: 5439 RVA: 0x000C95EC File Offset: 0x000C85EC
		// (set) Token: 0x06001540 RID: 5440 RVA: 0x000C9634 File Offset: 0x000C8634
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
				return this.ᜀ.IsBold;
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
				this.ᜀ.IsBold = value;
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06001541 RID: 5441 RVA: 0x000C967C File Offset: 0x000C867C
		// (set) Token: 0x06001542 RID: 5442 RVA: 0x000C96C4 File Offset: 0x000C86C4
		public ExcelColors KnownColor
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
				return this.ᜀ.KnownColor;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_6A;
					case 2:
						this.ᜁ.ᜀ(value);
						this.ᜀ.KnownColor = value;
						this.ᜁ.ᜈ(false);
						num = 1;
						continue;
					}
					IL_1C:
					if (true)
					{
					}
					if (this.ᜁ.ᜀ() != value)
					{
						num = 2;
						continue;
					}
					IL_6A:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C;
					default:
						goto IL_80;
					}
				}
				IL_80:
				if (false)
				{
				}
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06001543 RID: 5443 RVA: 0x000C9764 File Offset: 0x000C8764
		// (set) Token: 0x06001544 RID: 5444 RVA: 0x000C97AC File Offset: 0x000C87AC
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
				return this.ᜀ.Color;
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
				this.ᜀ.Color = value;
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06001545 RID: 5445 RVA: 0x000C97F4 File Offset: 0x000C87F4
		// (set) Token: 0x06001546 RID: 5446 RVA: 0x000C983C File Offset: 0x000C883C
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
				return this.ᜀ.IsItalic;
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
				this.ᜀ.IsItalic = value;
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06001547 RID: 5447 RVA: 0x000C9884 File Offset: 0x000C8884
		// (set) Token: 0x06001548 RID: 5448 RVA: 0x000C98CC File Offset: 0x000C88CC
		protected internal bool MacOSOutlineFont
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
				return this.ᜀ.MacOSOutlineFont;
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
				this.ᜀ.MacOSOutlineFont = value;
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x000C9914 File Offset: 0x000C8914
		// (set) Token: 0x0600154A RID: 5450 RVA: 0x000C995C File Offset: 0x000C895C
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
				return this.ᜀ.MacOSShadow;
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
				this.ᜀ.MacOSShadow = value;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x0600154B RID: 5451 RVA: 0x000C99A4 File Offset: 0x000C89A4
		// (set) Token: 0x0600154C RID: 5452 RVA: 0x000C99EC File Offset: 0x000C89EC
		public double Size
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
				return this.ᜀ.Size;
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
				this.ᜀ.Size = value;
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x0600154D RID: 5453 RVA: 0x000C9A34 File Offset: 0x000C8A34
		// (set) Token: 0x0600154E RID: 5454 RVA: 0x000C9A7C File Offset: 0x000C8A7C
		public bool IsStrikethrough
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
				return this.ᜀ.IsStrikethrough;
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
				this.ᜀ.IsStrikethrough = value;
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x0600154F RID: 5455 RVA: 0x000C9AC4 File Offset: 0x000C8AC4
		// (set) Token: 0x06001550 RID: 5456 RVA: 0x000C9B0C File Offset: 0x000C8B0C
		public bool IsSubscript
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
				return this.ᜀ.IsSubscript;
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
				this.ᜀ.IsSubscript = value;
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06001551 RID: 5457 RVA: 0x000C9B54 File Offset: 0x000C8B54
		// (set) Token: 0x06001552 RID: 5458 RVA: 0x000C9B9C File Offset: 0x000C8B9C
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
				return this.ᜀ.IsSuperscript;
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
				this.ᜀ.IsSuperscript = value;
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06001553 RID: 5459 RVA: 0x000C9BE4 File Offset: 0x000C8BE4
		// (set) Token: 0x06001554 RID: 5460 RVA: 0x000C9C2C File Offset: 0x000C8C2C
		public FontUnderlineType Underline
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
				return this.ᜀ.Underline;
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
				this.ᜀ.Underline = value;
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06001555 RID: 5461 RVA: 0x000C9C74 File Offset: 0x000C8C74
		// (set) Token: 0x06001556 RID: 5462 RVA: 0x000C9CBC File Offset: 0x000C8CBC
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
				return this.ᜀ.FontName;
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
				this.ᜀ.FontName = value;
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06001557 RID: 5463 RVA: 0x000C9D04 File Offset: 0x000C8D04
		// (set) Token: 0x06001558 RID: 5464 RVA: 0x000C9D4C File Offset: 0x000C8D4C
		public FontVertialAlignmentType VerticalAlignment
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
				return this.ᜀ.VerticalAlignment;
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
				this.ᜀ.VerticalAlignment = value;
			}
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x000C9D94 File Offset: 0x000C8D94
		public Font GenerateNativeFont()
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
			return this.ᜀ.GenerateNativeFont();
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x0600155A RID: 5466 RVA: 0x000C9DDC File Offset: 0x000C8DDC
		// (set) Token: 0x0600155B RID: 5467 RVA: 0x000C9E20 File Offset: 0x000C8E20
		public string Text
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
				return this.ᜄ;
			}
			set
			{
				for (;;)
				{
					this.ᜄ = value;
					int num = 6;
					for (;;)
					{
						if (true)
						{
						}
						switch (num)
						{
						case 0:
							if (this.ᜅ.ᜃ() != ObjectTextLinkType.DisplayUnit)
							{
								goto IL_7F;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_47;
							default:
								if (false)
								{
								}
								num = 7;
								continue;
							}
							break;
						case 1:
							this.ᜈ = null;
							num = 5;
							continue;
						case 2:
							goto IL_7F;
						case 3:
							num = 4;
							continue;
						case 4:
							if (this.ᜈ.Length > 0)
							{
								num = 1;
								continue;
							}
							return;
						case 5:
							return;
						case 6:
							goto IL_47;
						case 7:
							goto IL_112;
						case 8:
							if (this.ᜈ != null)
							{
								num = 3;
								continue;
							}
							return;
						case 9:
							num = 0;
							continue;
						}
						break;
						IL_47:
						if (!this.ᜊ)
						{
							num = 9;
							continue;
						}
						goto IL_112;
						IL_7F:
						this.ᜁ.ᜇ(value == null);
						num = 8;
						continue;
						IL_112:
						this.ᜁ.ᜁ(false);
						num = 2;
					}
				}
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x0600155C RID: 5468 RVA: 0x000C9F5C File Offset: 0x000C8F5C
		public IChartFrameFormat FrameFormat
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						goto IL_46;
					case 1:
						this.InitFrameFormat();
						num = 0;
						continue;
					}
					IL_1C:
					if (this.ᜃ == null)
					{
						num = 1;
						continue;
					}
					IL_46:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C;
					default:
						goto IL_5C;
					}
				}
				IL_5C:
				if (false)
				{
				}
				return this.ᜃ;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x000C9FDC File Offset: 0x000C8FDC
		internal spr\u20F4 ObjectLink
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
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x000CA020 File Offset: 0x000C9020
		// (set) Token: 0x0600155F RID: 5471 RVA: 0x000CA068 File Offset: 0x000C9068
		public int TextRotationAngle
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
				return (int)this.ᜁ.ᜋ();
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
				this.ᜁ.ᜀ((short)value);
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06001560 RID: 5472 RVA: 0x000CA0B0 File Offset: 0x000C90B0
		public bool HasTextRotation
		{
			get
			{
				short? num;
				int? num3;
				for (;;)
				{
					if (true)
					{
					}
					num = this.ᜁ.\u170D();
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_75;
						case 1:
							goto IL_96;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_96;
							default:
								if (false)
								{
								}
								if (num == null)
								{
									num2 = 3;
									continue;
								}
								num2 = 0;
								continue;
							}
							break;
						case 3:
							num3 = null;
							num2 = 1;
							continue;
						}
						break;
					}
				}
				IL_75:
				int? num4 = new int?((int)num.GetValueOrDefault());
				goto IL_99;
				IL_96:
				num4 = num3;
				IL_99:
				int? num5 = num4;
				return num5 != null;
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06001561 RID: 5473 RVA: 0x000CA160 File Offset: 0x000C9160
		internal spr\u20B6 TextRecord
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
				return this.ᜁ;
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06001562 RID: 5474 RVA: 0x000CA1A4 File Offset: 0x000C91A4
		// (set) Token: 0x06001563 RID: 5475 RVA: 0x000CA200 File Offset: 0x000C9200
		public string NumberFormat
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
				int numberFormatIndex = this.NumberFormatIndex;
				sprᤅ sprᤅ = this.ᜂ.InnerFormats.ᜁ(numberFormatIndex);
				return sprᤅ.ᜂ();
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
				int num = this.ᜂ.InnerFormats.ᜉ(value);
				this.ChartAI.ᜁ((ushort)num);
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x000CA25C File Offset: 0x000C925C
		public int NumberFormatIndex
		{
			get
			{
				if (this.ᜇ != null)
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
						return (int)this.ᜇ.ᜃ();
					}
				}
				return 0;
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06001565 RID: 5477 RVA: 0x000CA2B0 File Offset: 0x000C92B0
		internal sprᢀ ChartAI
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
						goto IL_55;
					case 1:
						this.ᜇ = (sprᢀ)spr\u175E.ᜀ(TBIFFRecord.ChartAI);
						num = 0;
						continue;
					}
					IL_24:
					if (this.ᜇ == null)
					{
						num = 1;
						continue;
					}
					IL_55:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_24;
					default:
						goto IL_6B;
					}
				}
				IL_6B:
				if (false)
				{
				}
				return this.ᜇ;
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06001566 RID: 5478 RVA: 0x000CA340 File Offset: 0x000C9340
		internal sprᜰ ChartAlRuns
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
						if (true)
						{
						}
						this.ᜈ = (sprᜰ)spr\u175E.ᜀ(TBIFFRecord.ChartAlruns);
						num = 0;
						continue;
					}
					IL_1C:
					if (this.ᜈ == null)
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
				return this.ᜈ;
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06001567 RID: 5479 RVA: 0x000CA3D0 File Offset: 0x000C93D0
		public bool HasDataLabels
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
				return !sprᨻ.ᜁ(this.ᜆ, null);
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06001568 RID: 5480 RVA: 0x000CA41C File Offset: 0x000C941C
		// (set) Token: 0x06001569 RID: 5481 RVA: 0x000CA464 File Offset: 0x000C9464
		public ChartBackgroundMode BackgroundMode
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
				this.ᜁ.ᜀ(value);
				this.IsAutoMode = false;
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x0600156A RID: 5482 RVA: 0x000CA4B4 File Offset: 0x000C94B4
		// (set) Token: 0x0600156B RID: 5483 RVA: 0x000CA4FC File Offset: 0x000C94FC
		public bool IsAutoMode
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
				return this.ᜁ.ᜄ();
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
				this.ᜁ.ᜄ(value);
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x0600156C RID: 5484 RVA: 0x000CA544 File Offset: 0x000C9544
		// (set) Token: 0x0600156D RID: 5485 RVA: 0x000CA588 File Offset: 0x000C9588
		public bool IsTrend
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
				return this.ᜊ;
			}
			set
			{
				for (;;)
				{
					this.ᜊ = value;
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								return;
							default:
								if (false)
								{
								}
								num = 1;
								continue;
							}
							break;
						case 1:
							if (this.ᜄ.Length == 0)
							{
								num = 2;
								continue;
							}
							return;
						case 2:
							goto IL_6B;
						case 3:
							return;
						case 4:
							if (this.ᜄ != null)
							{
								num = 0;
								continue;
							}
							goto IL_6B;
						}
						break;
						IL_6B:
						this.ᜁ.ᜁ(true);
						num = 3;
					}
				}
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x0600156E RID: 5486 RVA: 0x000CA63C File Offset: 0x000C963C
		// (set) Token: 0x0600156F RID: 5487 RVA: 0x000CA684 File Offset: 0x000C9684
		public bool IsAutoColor
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
				return this.ᜁ.ᜉ();
			}
			internal set
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
				this.ᜁ.ᜈ(value);
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06001570 RID: 5488 RVA: 0x000CA6CC File Offset: 0x000C96CC
		// (set) Token: 0x06001571 RID: 5489 RVA: 0x000CA710 File Offset: 0x000C9710
		public Stream LayoutStream
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
				return this.ᜋ;
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
				this.ᜋ = value;
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06001572 RID: 5490 RVA: 0x000CA754 File Offset: 0x000C9754
		// (set) Token: 0x06001573 RID: 5491 RVA: 0x000CA798 File Offset: 0x000C9798
		internal Stream OverlayStream
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
				this.\u170D = value;
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06001574 RID: 5492 RVA: 0x000CA7DC File Offset: 0x000C97DC
		public XlsWorkbook ParentWorkbook
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
				return this.ᜂ;
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06001575 RID: 5493 RVA: 0x000CA820 File Offset: 0x000C9820
		// (set) Token: 0x06001576 RID: 5494 RVA: 0x000CA864 File Offset: 0x000C9864
		public ChartParagraphType ParagraphType
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
				return this.ᜌ;
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
				this.ᜌ = value;
			}
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x000CA8A8 File Offset: 0x000C98A8
		internal int ᜀ(IList<BiffRecordRaw> A_0, int A_1)
		{
			int a_ = 14;
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
						goto IL_DA;
					default:
					{
						if (false)
						{
						}
						if (true)
						{
						}
						if (A_1 >= A_0.Count)
						{
							num = 4;
							continue;
						}
						this.ᜁ = (spr\u20B6)XlsChartTextArea.ᜀ(A_0[A_1]);
						A_1++;
						BiffRecordRaw biffRecordRaw = XlsChartTextArea.ᜀ(A_0[A_1]);
						A_1++;
						biffRecordRaw.CheckTypeCode(TBIFFRecord.Begin);
						this.ᜆ = null;
						num = 8;
						continue;
					}
					}
					break;
				case 1:
					return A_1;
				case 3:
					if (A_1 >= 0)
					{
						num = 5;
						continue;
					}
					goto IL_9E;
				case 4:
					goto IL_194;
				case 5:
					goto IL_DA;
				case 6:
					goto IL_4C;
				case 7:
					goto IL_DC;
				case 8:
					goto IL_DC;
				case 9:
				{
					BiffRecordRaw biffRecordRaw;
					if (biffRecordRaw.TypeCode == TBIFFRecord.End)
					{
						num = 1;
						continue;
					}
					biffRecordRaw = A_0[A_1];
					biffRecordRaw = XlsChartTextArea.ᜀ(biffRecordRaw);
					A_1++;
					A_1 = this.ᜀ(biffRecordRaw, A_0, A_1);
					num = 7;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 6;
					continue;
				}
				num = 3;
				continue;
				IL_DA:
				num = 0;
				continue;
				IL_DC:
				num = 9;
			}
			IL_4C:
			throw new ArgumentNullException(RecordTableEnumerator.b("⁃❅㱇⭉", a_));
			IL_9E:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ᑃ⥅㭇", a_), RecordTableEnumerator.b("ቃ❅⑇㽉⥋湍㍏㍑㩓㡕㝗⹙籛㱝՟䉡ࡣͥ᭧ᥩ䱫ᩭᡯ፱ᩳ噵䡷婹ᵻၽꊁ늑ﺕ聯벛瞧솟횡얣袥쾩슫즭쒯\udab1", a_));
			IL_194:
			goto IL_9E;
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x000CAA50 File Offset: 0x000C9A50
		private void ᜀ(spr\u2241 A_0)
		{
			int a_ = 11;
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
				if (false)
				{
				}
				if (true)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("❀ⱂ⭄㍆ㅈ", a_));
			}
			this.ᜀ((int)A_0.ᜀ());
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x000CAABC File Offset: 0x000C9ABC
		internal int ᜀ(BiffRecordRaw A_0, IList<BiffRecordRaw> A_1, int A_2)
		{
			int a_ = 9;
			int num = 5;
			for (;;)
			{
				TBIFFRecord typeCode;
				switch (num)
				{
				case 0:
					goto IL_17C;
				case 1:
				{
					if (typeCode != TBIFFRecord.ChartSeriesText)
					{
						num = 6;
						continue;
					}
					spr\u1D35 spr_u1D = (spr\u1D35)A_0;
					this.ᜄ = spr_u1D.ᜁ();
					num = 11;
					continue;
				}
				case 2:
					num = 1;
					continue;
				case 3:
					if (typeCode != TBIFFRecord.ChartFrame)
					{
						num = 18;
						continue;
					}
					A_2--;
					this.InitFrameFormat();
					this.ᜃ.ᜀ(A_1, ref A_2);
					num = 12;
					continue;
				case 4:
					goto IL_2A0;
				case 6:
					num = 23;
					continue;
				case 7:
					goto IL_110;
				case 8:
					goto IL_26F;
				case 9:
					goto IL_204;
				case 10:
					switch (typeCode)
					{
					case TBIFFRecord.ChartPos:
						this.ᜉ = (spr\u23BE)A_0;
						num = 9;
						continue;
					case TBIFFRecord.ChartAlruns:
						this.ᜈ = (sprᜰ)A_0;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_26F;
						default:
							if (false)
							{
							}
							num = 14;
							continue;
						}
						break;
					case TBIFFRecord.ChartAI:
						this.ᜇ = (sprᢀ)A_0;
						num = 21;
						continue;
					default:
						num = 20;
						continue;
					}
					break;
				case 11:
					goto IL_160;
				case 12:
					goto IL_1E8;
				case 13:
					goto IL_C7;
				case 14:
					goto IL_2D8;
				case 15:
					goto IL_8F;
				case 16:
					if (typeCode != TBIFFRecord.ChartDataLabels)
					{
						num = 2;
						continue;
					}
					this.ᜆ = (sprᨻ)A_0;
					num = 0;
					continue;
				case 17:
					if (typeCode <= TBIFFRecord.ChartSeriesText)
					{
						num = 19;
						continue;
					}
					num = 22;
					continue;
				case 18:
					num = 10;
					continue;
				case 19:
					num = 16;
					continue;
				case 20:
					num = 4;
					continue;
				case 21:
					goto IL_AB;
				case 22:
					switch (typeCode)
					{
					case TBIFFRecord.ChartFontx:
						this.ᜀ((spr\u2241)A_0);
						num = 13;
						continue;
					case TBIFFRecord.ChartObjectLink:
						this.ᜅ = (spr\u20F4)A_0;
						num = 7;
						continue;
					default:
						num = 8;
						continue;
					}
					break;
				case 23:
					goto IL_1BF;
				}
				if (true)
				{
				}
				if (A_0 == null)
				{
					num = 15;
					continue;
				}
				typeCode = A_0.TypeCode;
				num = 17;
				continue;
				IL_26F:
				num = 3;
			}
			IL_8F:
			throw new ArgumentNullException(RecordTableEnumerator.b("䴾⑀⁂⩄㕆ⵈ", a_));
			IL_AB:
			IL_C7:
			IL_110:
			IL_160:
			IL_17C:
			IL_1BF:
			IL_1E8:
			IL_204:
			IL_2A0:
			IL_2D8:
			this.ᜀ.OColor.SetKnownColor(this.ᜁ.ᜀ());
			return A_2;
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x000CADC0 File Offset: 0x000C9DC0
		protected virtual bool ShouldSerialize
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 7;
						continue;
					case 2:
						if (true)
						{
						}
						num = 11;
						continue;
					case 3:
						num = 13;
						continue;
					case 4:
						goto IL_15C;
					case 5:
						num = 6;
						continue;
					case 6:
						if (!this.ᜁ.ᜑ())
						{
							num = 0;
							continue;
						}
						return true;
					case 7:
						if (this.ᜁ.ᜉ())
						{
							num = 8;
							continue;
						}
						return true;
					case 8:
						num = 9;
						continue;
					case 9:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_161;
						default:
							if (false)
							{
							}
							if (this.ᜁ.ᜄ())
							{
								num = 4;
								continue;
							}
							return true;
						}
						break;
					case 10:
						if (this.ᜅ.ᜃ() != ObjectTextLinkType.DisplayUnit)
						{
							num = 5;
							continue;
						}
						return true;
					case 11:
						if (!this.ᜊ)
						{
							num = 3;
							continue;
						}
						return true;
					case 12:
						goto IL_161;
					case 13:
						if (this.ᜅ.ᜃ() != ObjectTextLinkType.DataLabel)
						{
							num = 12;
							continue;
						}
						return true;
					}
					if (!this.HasText)
					{
						num = 2;
						continue;
					}
					return true;
					IL_161:
					num = 10;
				}
				IL_15C:
				return !this.ᜁ.ᜏ();
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x0600157B RID: 5499 RVA: 0x000CAF5C File Offset: 0x000C9F5C
		protected internal bool HasText
		{
			get
			{
				if (this.ᜄ != null)
				{
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_2A;
						}
					}
					IL_2A:
					if (false)
					{
					}
					if (true)
					{
					}
					return this.ᜄ.Length > 0;
				}
				return false;
			}
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x0600157C RID: 5500 RVA: 0x000CAFB4 File Offset: 0x000C9FB4
		// (set) Token: 0x0600157D RID: 5501 RVA: 0x000CAFF8 File Offset: 0x000C9FF8
		internal bool IsFormula
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
				return this.ᜎ;
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
				this.ᜎ = value;
			}
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x000CB03C File Offset: 0x000CA03C
		public virtual void SerializeDataToList(IList<IRecordStorage> records)
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
			this.ᜀ(records, false);
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x000CB080 File Offset: 0x000CA080
		internal void ᜀ(IList<IRecordStorage> A_0, bool A_1)
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
			this.ᜀ(A_0, A_1, true);
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x000CB0C4 File Offset: 0x000CA0C4
		internal void ᜀ(IList<IRecordStorage> A_0, bool A_1, bool A_2)
		{
			int a_ = 18;
			int num = 19;
			for (;;)
			{
				bool flag;
				switch (num)
				{
				case 0:
					if (this.ᜄ != null)
					{
						num = 38;
						continue;
					}
					goto IL_320;
				case 1:
					this.SerializeRecord(A_0, this.ᜈ);
					num = 3;
					continue;
				case 2:
				{
					spr\u1D35 spr_u1D = (spr\u1D35)spr\u175E.ᜀ(TBIFFRecord.ChartSeriesText);
					spr_u1D.ᜀ(this.ᜄ);
					this.SerializeRecord(A_0, spr_u1D);
					num = 32;
					continue;
				}
				case 3:
					goto IL_3CA;
				case 4:
					if (flag)
					{
						num = 13;
						continue;
					}
					goto IL_4B2;
				case 5:
					if (this.ᜄ.Length > 0)
					{
						num = 2;
						continue;
					}
					goto IL_320;
				case 6:
					return;
				case 7:
					if (this.ᜉ != null)
					{
						num = 9;
						continue;
					}
					goto IL_295;
				case 8:
					num = 28;
					continue;
				case 9:
					this.SerializeRecord(A_0, this.ᜉ);
					num = 22;
					continue;
				case 10:
					if (!this.ᜊ)
					{
						num = 24;
						continue;
					}
					goto IL_4B2;
				case 11:
					this.UpdateAsTrend();
					num = 30;
					continue;
				case 12:
					this.ᜃ.ᜀ(A_0);
					num = 23;
					continue;
				case 13:
					num = 14;
					continue;
				case 14:
					if (sprᨻ.ᜀ(this.ᜆ, null))
					{
						num = 17;
						continue;
					}
					goto IL_4B2;
				case 15:
					if (this.ᜈ != null)
					{
						num = 34;
						continue;
					}
					goto IL_3CA;
				case 16:
					goto IL_393;
				case 17:
					this.SerializeRecord(A_0, this.ᜆ);
					num = 37;
					continue;
				case 18:
					this.ᜀ(A_0);
					num = 16;
					continue;
				case 20:
					goto IL_CD;
				case 21:
					if (A_2)
					{
						num = 18;
						continue;
					}
					goto IL_393;
				case 22:
					goto IL_295;
				case 23:
					goto IL_245;
				case 24:
					num = 4;
					continue;
				case 25:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_204;
					default:
						if (false)
						{
						}
						if (this.ᜃ != null)
						{
							num = 12;
							continue;
						}
						goto IL_245;
					}
					break;
				case 26:
					if (this.ᜈ.Length >= 3)
					{
						num = 8;
						continue;
					}
					goto IL_3CA;
				case 27:
					if (!flag)
					{
						num = 31;
						continue;
					}
					goto IL_400;
				case 28:
					goto IL_204;
				case 29:
					goto IL_3FB;
				case 30:
					goto IL_11B;
				case 31:
					this.ᜁ.ᜀ(DataLabelPositionType.Automatic);
					num = 36;
					continue;
				case 32:
					goto IL_320;
				case 33:
					if (this.ᜊ)
					{
						num = 11;
						continue;
					}
					goto IL_11B;
				case 34:
					num = 26;
					continue;
				case 35:
					if (!this.ShouldSerialize)
					{
						num = 6;
						continue;
					}
					num = 33;
					continue;
				case 36:
					goto IL_400;
				case 37:
					goto IL_290;
				case 38:
					num = 5;
					continue;
				case 39:
					if (A_1)
					{
						num = 29;
						continue;
					}
					num = 0;
					continue;
				}
				if (A_0 == null)
				{
					num = 20;
					continue;
				}
				num = 35;
				continue;
				IL_11B:
				this.ᜁ.ᜀ(this.ᜀ.OColor.ᜂ(this.ᜂ));
				this.SerializeRecord(A_0, this.ᜁ);
				this.SerializeRecord(A_0, spr\u175E.ᜀ(TBIFFRecord.Begin));
				flag = (this.ᜅ.ᜃ() == ObjectTextLinkType.DataLabel);
				num = 27;
				continue;
				IL_204:
				if (this.ᜈ.Length <= 256)
				{
					num = 1;
					continue;
				}
				goto IL_3CA;
				IL_245:
				this.SerializeRecord(A_0, this.ᜅ);
				num = 10;
				continue;
				IL_295:
				num = 21;
				continue;
				IL_320:
				num = 25;
				continue;
				IL_393:
				num = 15;
				continue;
				IL_3CA:
				if (true)
				{
				}
				this.SerializeRecord(A_0, this.ᜇ);
				num = 39;
				continue;
				IL_400:
				num = 7;
			}
			IL_CD:
			throw new ArgumentNullException(RecordTableEnumerator.b("㩇⽉⽋⅍≏㙑❓", a_));
			IL_290:
			goto IL_4B2;
			IL_3FB:
			this.SerializeRecord(A_0, spr\u175E.ᜀ(TBIFFRecord.End));
			return;
			IL_4B2:
			this.SerializeRecord(A_0, spr\u175E.ᜀ(TBIFFRecord.End));
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x000CB594 File Offset: 0x000CA594
		private void ᜀ(IList<IRecordStorage> A_0)
		{
			int a_ = 2;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_38;
				case 2:
				{
					spr\u2241 spr_u = (spr\u2241)spr\u175E.ᜀ(TBIFFRecord.ChartFontx);
					int num2;
					spr_u.ᜀ((ushort)num2);
					this.SerializeRecord(A_0, spr_u);
					num = 4;
					continue;
				}
				case 3:
				{
					if (true)
					{
					}
					int num2;
					if (num2 > 0)
					{
						num = 2;
						continue;
					}
					return;
				}
				case 4:
					goto IL_65;
				}
				if (A_0 == null)
				{
					num = 1;
				}
				else
				{
					FontWrapper fontWrapper = this.ᜀ;
					int num2 = fontWrapper.Wrapped.Index;
					num = 3;
				}
			}
			IL_38:
			throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹弻儽㈿♁㝃", a_));
			IL_65:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_38;
			default:
				if (false)
				{
				}
				break;
			}
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x000CB67C File Offset: 0x000CA67C
		internal virtual void SerializeRecord(IList<IRecordStorage> records, BiffRecordRaw record)
		{
			int a_ = 19;
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					records.Add((BiffRecordRaw)record.Clone());
					num = 0;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8B;
					default:
						goto IL_56;
					}
					break;
				case 3:
					goto IL_8B;
				}
				if (records == null)
				{
					if (true)
					{
					}
					num = 2;
					continue;
				}
				num = 3;
				continue;
				IL_8B:
				if (record == null)
				{
					return;
				}
				num = 1;
			}
			IL_56:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㭈⹊⹌⁎⍐㝒♔", a_));
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06001583 RID: 5507 RVA: 0x000CB738 File Offset: 0x000CA738
		internal int FontIndex
		{
			get
			{
				if (this.ᜀ == null)
				{
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_2A;
						}
					}
					IL_2A:
					if (true)
					{
					}
					if (false)
					{
					}
					return 0;
				}
				return this.ᜀ.Index;
			}
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x000CB78C File Offset: 0x000CA78C
		protected virtual XlsChartFrameFormat CreateFrameFormat()
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
			return new XlsChartFrameFormat(base.ReservedHandle, this);
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x000CB7D4 File Offset: 0x000CA7D4
		protected void InitFrameFormat()
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
			this.ᜃ = this.CreateFrameFormat();
			sprᳫ sprᳫ = this.ᜃ.FrameRecord;
			sprᳫ.ᜁ(true);
			this.ᜃ.Border.Pattern = ChartLinePatternType.None;
			this.ᜃ.Border.UseDefaultFormat = false;
			this.ᜃ.Interior.UseDefaultFormat = false;
			this.ᜃ.Interior.Pattern = ExcelPatternType.None;
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x000CB874 File Offset: 0x000CA874
		internal void ᜀ(int A_0)
		{
			XlsFont a_;
			for (;;)
			{
				this.ᜁ();
				a_ = (XlsFont)this.ᜂ.InnerFonts[A_0];
				int num = 1;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_79;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_79;
						default:
							if (false)
							{
							}
							if (this.ᜀ == null)
							{
								num = 0;
								continue;
							}
							goto IL_91;
						}
						break;
					case 2:
						goto IL_8F;
					}
					break;
					IL_79:
					this.ᜀ = new FontWrapper();
					num = 2;
				}
			}
			IL_8F:
			IL_91:
			this.ᜀ.Wrapped = a_;
			this.ᜂ();
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x000CB924 File Offset: 0x000CA924
		private void ᜃ()
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_6D;
				case 2:
					this.ᜆ = new sprᨻ();
					num = 1;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6F;
				default:
					if (false)
					{
					}
					if (!sprᨻ.ᜁ(this.ᜆ, null))
					{
						goto IL_6F;
					}
					num = 2;
					break;
				}
			}
			IL_6D:
			IL_6F:
			if (true)
			{
			}
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x000CB9A8 File Offset: 0x000CA9A8
		public object Clone(object parent, Dictionary<int, int> fontIndexes, Dictionary<string, string> dicNewSheetNames)
		{
			switch (0)
			{
			default:
			{
				XlsChartTextArea xlsChartTextArea;
				for (;;)
				{
					xlsChartTextArea = (XlsChartTextArea)base.MemberwiseClone();
					xlsChartTextArea.SetParent(parent);
					xlsChartTextArea.ᜄ();
					xlsChartTextArea.m_bIsDisposed = this.m_bIsDisposed;
					xlsChartTextArea.ᜁ = (spr\u20B6)spr\u1CD3.ᜀ(this.ᜁ);
					int num = 0;
					for (;;)
					{
						int num2;
						spr\u2086 spr_u;
						int num4;
						int num5;
						int num6;
						string text;
						switch (num)
						{
						case 0:
							if (this.ᜀ != null)
							{
								if (true)
								{
								}
								num = 1;
								continue;
							}
							goto IL_23D;
						case 1:
							xlsChartTextArea.ᜀ = this.ᜀ.Clone(xlsChartTextArea.ᜂ, xlsChartTextArea, fontIndexes);
							num = 22;
							continue;
						case 2:
							goto IL_156;
						case 3:
						{
							Ptg[] array;
							num2 = array.Length;
							goto IL_229;
						}
						case 4:
							num = 19;
							continue;
						case 5:
							goto IL_1EB;
						case 6:
						{
							int num3 = (int)spr_u.ᜁ();
							num4 = num3;
							num = 25;
							continue;
						}
						case 7:
							if (this.ᜇ != null)
							{
								num = 24;
								continue;
							}
							goto IL_395;
						case 8:
						{
							if (num5 >= num6)
							{
								num = 26;
								continue;
							}
							Ptg[] array;
							spr_u = (array[num5] as spr\u2086);
							num = 10;
							continue;
						}
						case 9:
							xlsChartTextArea.ᜃ = this.ᜃ.Clone(xlsChartTextArea);
							num = 5;
							continue;
						case 10:
							if (spr_u != null)
							{
								num = 6;
								continue;
							}
							goto IL_1D6;
						case 11:
							if (dicNewSheetNames == null)
							{
								goto IL_EE;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_156;
							default:
								if (false)
								{
								}
								num = 18;
								continue;
							}
							break;
						case 12:
						{
							int num3;
							text = this.ᜂ.GetSheetNameByReference(num3);
							num = 11;
							continue;
						}
						case 13:
							goto IL_1D6;
						case 14:
							if (dicNewSheetNames.ContainsKey(text))
							{
								num = 2;
								continue;
							}
							goto IL_EE;
						case 15:
							goto IL_37A;
						case 16:
						{
							Ptg[] array;
							if (array == null)
							{
								num = 4;
								continue;
							}
							num = 3;
							continue;
						}
						case 17:
							goto IL_32E;
						case 18:
							num = 14;
							continue;
						case 19:
							num2 = 0;
							goto IL_229;
						case 20:
							goto IL_EE;
						case 21:
							goto IL_32E;
						case 22:
							goto IL_23D;
						case 23:
							if (this.ᜃ != null)
							{
								num = 9;
								continue;
							}
							goto IL_1EB;
						case 24:
						{
							xlsChartTextArea.ᜇ = (sprᢀ)this.ᜇ.Clone();
							xlsChartTextArea.NumberFormat = this.NumberFormat;
							Ptg[] array = xlsChartTextArea.ᜇ.ᜆ();
							num = 16;
							continue;
						}
						case 25:
						{
							int num3;
							if (!this.ᜂ.IsExternalReference(num3))
							{
								num = 12;
								continue;
							}
							goto IL_37A;
						}
						case 26:
							goto IL_34A;
						}
						break;
						IL_EE:
						num4 = xlsChartTextArea.ᜂ.AddSheetReference(text);
						num = 15;
						continue;
						IL_156:
						text = dicNewSheetNames[text];
						num = 20;
						continue;
						IL_1D6:
						num5++;
						num = 21;
						continue;
						IL_1EB:
						xlsChartTextArea.ᜅ = (spr\u20F4)spr\u1CD3.ᜀ(this.ᜅ);
						num = 7;
						continue;
						IL_229:
						num6 = num2;
						num5 = 0;
						num = 17;
						continue;
						IL_23D:
						num = 23;
						continue;
						IL_32E:
						num = 8;
						continue;
						IL_37A:
						spr_u.ᜂ((ushort)num4);
						num = 13;
					}
				}
				IL_34A:
				IL_395:
				xlsChartTextArea.ᜄ = this.ᜄ;
				return xlsChartTextArea;
			}
			}
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x000CBD58 File Offset: 0x000CAD58
		public object Clone(object parent)
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
			return this.Clone(parent, null, null);
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x000CBD9C File Offset: 0x000CAD9C
		public void UpdateSerieIndex(int index)
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
			this.ᜅ.ᜁ((ushort)index);
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x000CBDE4 File Offset: 0x000CADE4
		public void UpdateAsTrend()
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
			this.ObjectLink.ᜀ(ushort.MaxValue);
			this.ᜁ.ᜂ(true);
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x000CBE3C File Offset: 0x000CAE3C
		internal void ᜀ(bool[] A_0)
		{
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
						goto IL_2C;
					default:
						goto IL_6F;
					}
					break;
				case 2:
					goto IL_2C;
				}
				if (this.ᜇ != null)
				{
					num = 2;
					continue;
				}
				return;
				IL_2C:
				if (true)
				{
				}
				FormulaUtil.ᜀ(this.ᜇ.ᜆ(), A_0);
				num = 0;
			}
			IL_6F:
			if (false)
			{
			}
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x000CBEC0 File Offset: 0x000CAEC0
		internal void ᜀ(int[] A_0)
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					Ptg[] a_;
					if (FormulaUtil.ᜀ(a_, A_0))
					{
						num = 1;
						continue;
					}
					return;
				}
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_86;
					default:
					{
						if (true)
						{
						}
						if (false)
						{
						}
						Ptg[] a_;
						this.ᜇ.ᜀ(a_);
						num = 2;
						continue;
					}
					}
					break;
				case 2:
					return;
				case 3:
				{
					Ptg[] a_ = this.ᜇ.ᜆ();
					goto IL_86;
				}
				}
				if (this.ᜇ != null)
				{
					num = 3;
					continue;
				}
				break;
				IL_86:
				num = 0;
			}
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x000CBF74 File Offset: 0x000CAF74
		private void ᜂ()
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
					goto IL_34;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_34;
					default:
						goto IL_7A;
					}
					break;
				}
				if (this.ᜀ != null)
				{
					num = 1;
					continue;
				}
				return;
				IL_34:
				this.ᜀ.OColor.AfterChange += this.ᜀ;
				num = 2;
			}
			IL_7A:
			if (false)
			{
			}
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x000CC004 File Offset: 0x000CB004
		private void ᜁ()
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_34;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_34;
					default:
						goto IL_7A;
					}
					break;
				}
				if (true)
				{
				}
				if (this.ᜀ != null)
				{
					num = 0;
					continue;
				}
				return;
				IL_34:
				this.ᜀ.OColor.AfterChange -= this.ᜀ;
				num = 1;
			}
			IL_7A:
			if (false)
			{
			}
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x000CC094 File Offset: 0x000CB094
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
			this.ᜁ.ᜈ(false);
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06001591 RID: 5521 RVA: 0x000CC0DC File Offset: 0x000CB0DC
		// (set) Token: 0x06001592 RID: 5522 RVA: 0x000CC134 File Offset: 0x000CB134
		public bool HasSeriesName
		{
			get
			{
				while (sprᨻ.ᜀ(this.ᜆ, null))
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
						return this.ᜆ.ᜂ();
					}
				}
				if (true)
				{
				}
				return false;
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
				this.ᜆ.ᜃ(value);
			}
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06001593 RID: 5523 RVA: 0x000CC17C File Offset: 0x000CB17C
		// (set) Token: 0x06001594 RID: 5524 RVA: 0x000CC1D4 File Offset: 0x000CB1D4
		public bool HasCategoryName
		{
			get
			{
				while (sprᨻ.ᜀ(this.ᜆ, null))
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
						return this.ᜆ.ᜀ();
					}
				}
				return false;
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
				this.ᜆ.ᜄ(value);
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06001595 RID: 5525 RVA: 0x000CC21C File Offset: 0x000CB21C
		// (set) Token: 0x06001596 RID: 5526 RVA: 0x000CC274 File Offset: 0x000CB274
		public bool HasValue
		{
			get
			{
				while (sprᨻ.ᜀ(this.ᜆ, null))
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
						return this.ᜆ.ᜄ();
					}
				}
				return false;
			}
			set
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
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3A;
						default:
							goto IL_6A;
						}
						break;
					case 2:
						goto IL_3A;
					}
					if (sprᨻ.ᜁ(this.ᜆ, null))
					{
						num = 2;
						continue;
					}
					goto IL_72;
					IL_3A:
					this.ᜃ();
					num = 1;
				}
				IL_6A:
				if (false)
				{
				}
				IL_72:
				this.ᜆ.ᜁ(value);
				this.TextRecord.ᜀ(value);
			}
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06001597 RID: 5527 RVA: 0x000CC30C File Offset: 0x000CB30C
		// (set) Token: 0x06001598 RID: 5528 RVA: 0x000CC364 File Offset: 0x000CB364
		public bool HasPercentage
		{
			get
			{
				while (sprᨻ.ᜀ(this.ᜆ, null))
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
						return this.ᜆ.ᜆ();
					}
				}
				return false;
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
				this.ᜆ.ᜀ(value);
				this.TextRecord.ᜆ(value);
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06001599 RID: 5529 RVA: 0x000CC3B8 File Offset: 0x000CB3B8
		// (set) Token: 0x0600159A RID: 5530 RVA: 0x000CC410 File Offset: 0x000CB410
		public bool HasBubbleSize
		{
			get
			{
				for (;;)
				{
					if (true)
					{
					}
					if (!sprᨻ.ᜀ(this.ᜆ, null))
					{
						break;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_30;
					}
				}
				return false;
				IL_30:
				if (false)
				{
				}
				return this.ᜆ.ᜁ();
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
				this.ᜆ.ᜂ(value);
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x0600159B RID: 5531 RVA: 0x000CC458 File Offset: 0x000CB458
		// (set) Token: 0x0600159C RID: 5532 RVA: 0x000CC4B0 File Offset: 0x000CB4B0
		public bool ShowLeaderLines
		{
			get
			{
				int a_ = 6;
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				throw new NotSupportedException(RecordTableEnumerator.b("瀻圽⸿❁㙃晅⑇⍉≋⭍⍏牑㕓⑕㵗穙㉛ㅝᑟ䉡ᝣ፥ᡧᩩͫᱭѯ᝱ၳ塵", a_));
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
			}
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x0600159D RID: 5533 RVA: 0x000CC4EC File Offset: 0x000CB4EC
		// (set) Token: 0x0600159E RID: 5534 RVA: 0x000CC544 File Offset: 0x000CB544
		public string Delimiter
		{
			get
			{
				while (sprᨻ.ᜀ(this.ᜆ, null))
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
						return this.ᜆ.ᜃ();
					}
				}
				return null;
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
				this.ᜆ.ᜀ(value);
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x0600159F RID: 5535 RVA: 0x000CC58C File Offset: 0x000CB58C
		// (set) Token: 0x060015A0 RID: 5536 RVA: 0x000CC5D4 File Offset: 0x000CB5D4
		public bool HasLegendKey
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
				return this.TextRecord.\u1718();
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
				this.TextRecord.ᜋ(value);
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x060015A1 RID: 5537 RVA: 0x000CC61C File Offset: 0x000CB61C
		// (set) Token: 0x060015A2 RID: 5538 RVA: 0x000CC664 File Offset: 0x000CB664
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
				return this.ᜁ.ᜃ();
			}
			set
			{
				int a_ = 17;
				while (value != DataLabelPositionType.Moved)
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
						this.ᜁ.ᜀ(value);
						return;
					}
				}
				throw new NotSupportedException(RecordTableEnumerator.b("ፆⅈ≊㹌潎㝐㽒㑔ざ祘㽚㉜㩞በൢ੤፦䥨ᡪᡬὮŰᱲݴͶ坸", a_));
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x060015A3 RID: 5539 RVA: 0x000CC6D0 File Offset: 0x000CB6D0
		// (set) Token: 0x060015A4 RID: 5540 RVA: 0x000CC718 File Offset: 0x000CB718
		public bool IsShowLabelPercent
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
				return this.TextRecord.\u171C();
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
				this.TextRecord.ᜅ(value);
			}
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x000CC760 File Offset: 0x000CB760
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
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x000CC79C File Offset: 0x000CB79C
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
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x060015A7 RID: 5543 RVA: 0x000CC7D8 File Offset: 0x000CB7D8
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
				return this.ᜀ.OColor;
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x060015A8 RID: 5544 RVA: 0x000CC820 File Offset: 0x000CB820
		public int Index
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
				return this.ᜀ.Index;
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x060015A9 RID: 5545 RVA: 0x000CC868 File Offset: 0x000CB868
		public XlsFont Font
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
				return this.ᜀ.Font;
			}
		}

		// Token: 0x04000F2E RID: 3886
		private byte \u2593\u008C\u007F\u0083;

		// Token: 0x04000F2F RID: 3887
		private FontWrapper ᜀ;

		// Token: 0x04000F30 RID: 3888
		private spr\u20B6 ᜁ = (spr\u20B6)spr\u175E.ᜀ(TBIFFRecord.ChartText);

		// Token: 0x04000F31 RID: 3889
		private int \u25D9\u0089\u00A7\u00A7;

		// Token: 0x04000F32 RID: 3890
		private XlsWorkbook ᜂ;

		// Token: 0x04000F33 RID: 3891
		private XlsChartFrameFormat ᜃ;

		// Token: 0x04000F34 RID: 3892
		private string ᜄ;

		// Token: 0x04000F35 RID: 3893
		private spr\u20F4 ᜅ = (spr\u20F4)spr\u175E.ᜀ(TBIFFRecord.ChartObjectLink);

		// Token: 0x04000F36 RID: 3894
		private string \u25D9\u00AE\u009D\u009E;

		// Token: 0x04000F37 RID: 3895
		private float[] \u2609\u0090\u007F\u0091;

		// Token: 0x04000F38 RID: 3896
		private int[] \u2609\u008A\u0083\u0087;

		// Token: 0x04000F39 RID: 3897
		private sprᨻ ᜆ = (sprᨻ)spr\u175E.ᜀ(TBIFFRecord.ChartDataLabels);

		// Token: 0x04000F3A RID: 3898
		private sprᢀ ᜇ;

		// Token: 0x04000F3B RID: 3899
		private sprᜰ ᜈ;

		// Token: 0x04000F3C RID: 3900
		private spr\u23BE ᜉ;

		// Token: 0x04000F3D RID: 3901
		private bool ᜊ;

		// Token: 0x04000F3E RID: 3902
		private Stream ᜋ;

		// Token: 0x04000F3F RID: 3903
		private ChartParagraphType ᜌ;

		// Token: 0x04000F40 RID: 3904
		private Stream \u170D;

		// Token: 0x04000F41 RID: 3905
		private bool ᜎ;
	}
}
