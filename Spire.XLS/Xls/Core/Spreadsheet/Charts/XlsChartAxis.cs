using System;
using System.Collections.Generic;
using Spire.Xls.Charts;
using Spire.Xls.Core.Interfaces;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Charts
{
	// Token: 0x020001BE RID: 446
	public abstract class XlsChartAxis : XlsObject, IChartAxis
	{
		// Token: 0x060018A3 RID: 6307 RVA: 0x000E9440 File Offset: 0x000E8440
		internal XlsChartAxis(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.InitializeVariables();
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x000E9464 File Offset: 0x000E8464
		internal XlsChartAxis(spr\u1DF5 A_0, object A_1, AxisType A_2) : this(A_0, A_1, A_2, true)
		{
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x000E947C File Offset: 0x000E847C
		internal XlsChartAxis(spr\u1DF5 A_0, object A_1, AxisType A_2, bool A_3) : base(A_0, A_1)
		{
			this.ᜁ = A_2;
			this.ᜂ = A_3;
			this.InitializeVariables();
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x000E94B0 File Offset: 0x000E84B0
		internal XlsChartAxis(spr\u1DF5 A_0, object A_1, IList<BiffRecordRaw> A_2, ref int A_3) : this(A_0, A_1, A_2, ref A_3, true)
		{
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x000E94CC File Offset: 0x000E84CC
		internal XlsChartAxis(spr\u1DF5 A_0, object A_1, IList<BiffRecordRaw> A_2, ref int A_3, bool A_4) : this(A_0, A_1)
		{
			this.Parse(A_2, ref A_3, A_4);
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x000E94EC File Offset: 0x000E84EC
		private void ᜁ()
		{
			int a_ = 6;
			this.ᜌ = (sprᾹ)base.FindParent(typeof(sprᾹ));
			if (this.ᜌ != null)
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
			throw new ArgumentNullException(RecordTableEnumerator.b("氻弽㈿❁⩃㉅桇╉⹋⑍㕏ㅑ⁓癕㭗㭙㉛そཟᙡ䑣ѥ൧䩩੫ŭկᱱၳ塵", a_));
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x060018A9 RID: 6313 RVA: 0x000E956C File Offset: 0x000E856C
		// (set) Token: 0x060018AA RID: 6314 RVA: 0x000E95B0 File Offset: 0x000E85B0
		public AxisType AxisType
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
				this.ᜁ = value;
			}
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x060018AB RID: 6315 RVA: 0x000E95F4 File Offset: 0x000E85F4
		// (set) Token: 0x060018AC RID: 6316 RVA: 0x000E9638 File Offset: 0x000E8638
		public bool IsPrimary
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
				this.ᜂ = value;
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x060018AD RID: 6317 RVA: 0x000E967C File Offset: 0x000E867C
		// (set) Token: 0x060018AE RID: 6318 RVA: 0x000E96D0 File Offset: 0x000E86D0
		public string Title
		{
			get
			{
				if (this.ᜃ != null)
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
						return this.ᜃ.Text;
					}
				}
				if (true)
				{
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
				this.TitleArea.Text = value;
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x060018AF RID: 6319 RVA: 0x000E9718 File Offset: 0x000E8718
		// (set) Token: 0x060018B0 RID: 6320 RVA: 0x000E97E8 File Offset: 0x000E87E8
		public int TextRotationAngle
		{
			get
			{
				for (;;)
				{
					ExcelVersion version = this.ParentWorkbook.Version;
					int num = 0;
					for (;;)
					{
						bool flag;
						bool flag2;
						switch (num)
						{
						case 0:
							if (version != ExcelVersion.Version2007)
							{
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_B1;
								}
								if (false)
								{
								}
								num = 4;
								continue;
							}
							num = 5;
							continue;
						case 1:
							goto IL_A4;
						case 2:
							if (!flag)
							{
								num = 1;
								continue;
							}
							goto IL_B4;
						case 3:
							flag2 = (version == ExcelVersion.Version2010);
							goto IL_8A;
						case 4:
							num = 3;
							continue;
						case 5:
							goto IL_B1;
						}
						break;
						IL_8A:
						flag = flag2;
						num = 2;
						continue;
						IL_B1:
						flag2 = true;
						goto IL_8A;
					}
				}
				IL_A4:
				if (true)
				{
				}
				return (int)this.ᜄ.ᜏ();
				IL_B4:
				return (int)(-(int)this.ᜄ.ᜏ());
			}
			set
			{
				int a_ = 0;
				bool flag2;
				for (;;)
				{
					IL_09:
					int num = 4;
					for (;;)
					{
						bool flag;
						switch (num)
						{
						case 0:
							num = 3;
							continue;
						case 1:
							goto IL_E1;
						case 2:
							flag = true;
							goto IL_D5;
						case 3:
						{
							if (value > 90)
							{
								num = 7;
								continue;
							}
							ExcelVersion version = this.ParentWorkbook.Version;
							if (true)
							{
							}
							num = 5;
							continue;
						}
						case 5:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_09;
							default:
							{
								if (false)
								{
								}
								ExcelVersion version;
								if (version != ExcelVersion.Version2007)
								{
									num = 6;
									continue;
								}
								num = 2;
								continue;
							}
							}
							break;
						case 6:
							num = 8;
							continue;
						case 7:
							goto IL_10A;
						case 8:
						{
							ExcelVersion version;
							flag = (version == ExcelVersion.Version2010);
							goto IL_D5;
						}
						}
						if (value >= -90)
						{
							num = 0;
							continue;
						}
						goto IL_A2;
						IL_D5:
						flag2 = flag;
						num = 1;
					}
				}
				IL_A2:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䀵夷嘹䤻嬽", a_), RecordTableEnumerator.b("怵夷嘹䤻嬽怿⅁╃⡅♇╉㡋湍㉏㝑瑓㩕㵗⥙⽛繝䵟孡呣䙥१ѩ࡫乭ᝯqᅳ᝵౷ό๻幽ꢇ뎉벋", a_));
				IL_E1:
				this.ᜄ.ᜀ((short)(flag2 ? (-(short)value) : value));
				this.ᜄ.ᜂ(false);
				return;
				IL_10A:
				goto IL_A2;
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x060018B1 RID: 6321 RVA: 0x000E9928 File Offset: 0x000E8928
		public bool IsAutoTextRotation
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
				return this.ᜄ.ᜀ();
			}
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x060018B2 RID: 6322 RVA: 0x000E9970 File Offset: 0x000E8970
		public IChartTextArea TitleArea
		{
			get
			{
				if (true)
				{
				}
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
							break;
						default:
							goto IL_9B;
						}
						break;
					case 2:
						this.ᜃ = new ChartTextArea((spr\u2158)base.ReservedHandle, this, this.TextLinkType);
						this.ᜃ.IsBold = true;
						this.ᜃ.Size = 10.0;
						num = 0;
						continue;
					}
					IL_2E:
					if (this.ᜃ == null)
					{
						num = 2;
						continue;
					}
					goto IL_A3;
					goto IL_2E;
				}
				IL_9B:
				if (false)
				{
				}
				IL_A3:
				return this.ᜃ;
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x060018B3 RID: 6323 RVA: 0x000E9A28 File Offset: 0x000E8A28
		// (set) Token: 0x060018B4 RID: 6324 RVA: 0x000E9B04 File Offset: 0x000E8B04
		public IFont Font
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
						goto IL_C4;
					case 3:
						this.IsDefaultTextSettings = false;
						num = 0;
						continue;
					case 4:
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_C4;
						}
						if (true)
						{
						}
						if (false)
						{
						}
						XlsFont a_ = (XlsFont)this.ParentWorkbook.InnerFonts[0];
						this.ᜇ = new ExcelFontWrapper(a_);
						num = 1;
						continue;
					}
					case 5:
						if (!this.IsChartFont)
						{
							num = 3;
							continue;
						}
						goto IL_C6;
					}
					if (this.ᜇ == null)
					{
						num = 4;
						continue;
					}
					IL_79:
					num = 5;
					continue;
					IL_C4:
					goto IL_79;
				}
				IL_77:
				IL_C6:
				return this.ᜇ;
			}
			set
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_BB;
					case 1:
						if (!this.IsChartFont)
						{
							num = 0;
							continue;
						}
						goto IL_6C;
					case 2:
						return;
					case 3:
						goto IL_6C;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_BB;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					}
					if (value != this.ᜇ)
					{
						if (true)
						{
						}
						num = 5;
						continue;
					}
					break;
					IL_6C:
					this.ᜇ = (FontWrapper)value;
					this.\u171B = false;
					num = 2;
					continue;
					IL_BB:
					this.IsDefaultTextSettings = false;
					num = 3;
				}
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x060018B5 RID: 6325 RVA: 0x000E9BD0 File Offset: 0x000E8BD0
		// (set) Token: 0x060018B6 RID: 6326 RVA: 0x000E9C14 File Offset: 0x000E8C14
		internal bool IsChartFont
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
				return this.\u171B;
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
				this.\u171B = value;
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x060018B7 RID: 6327 RVA: 0x000E9C58 File Offset: 0x000E8C58
		public IChartGridLine MajorGridLines
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
				return this.ᜈ;
			}
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x060018B8 RID: 6328 RVA: 0x000E9C9C File Offset: 0x000E8C9C
		public IChartGridLine MinorGridLines
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
				return this.ᜉ;
			}
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x060018B9 RID: 6329 RVA: 0x000E9CE0 File Offset: 0x000E8CE0
		// (set) Token: 0x060018BA RID: 6330 RVA: 0x000E9D24 File Offset: 0x000E8D24
		public bool HasMinorGridLines
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
				int a_ = 8;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						XlsChart xlsChart;
						if (xlsChart.TypeChanging)
						{
							goto IL_A2;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_C5;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					}
					case 1:
						num = 7;
						continue;
					case 3:
						goto IL_C5;
					case 4:
						num = 3;
						continue;
					case 5:
						this.ᜉ = (value ? new ChartGridLine((spr\u2158)base.ReservedHandle, this, AxisLineIdentifierType.MinorGridLine) : null);
						num = 8;
						continue;
					case 6:
					{
						XlsChart xlsChart = this.ᜌ.ᜇ();
						num = 0;
						continue;
					}
					case 7:
					{
						XlsChart xlsChart;
						if (!xlsChart.CheckForSupportGridLine())
						{
							num = 4;
							continue;
						}
						goto IL_A2;
					}
					case 8:
						goto IL_100;
					}
					if (value != this.HasMinorGridLines)
					{
						num = 6;
						continue;
					}
					goto IL_122;
					IL_A2:
					this.ᜋ = value;
					num = 5;
				}
				IL_C5:
				throw new ApplicationException(RecordTableEnumerator.b("礽㈿⭁⁃⩅ⅇ⑉⥋㵍灏ㅑ㕓㡕㙗㕙⡛繝ɟݡ䑣ᕥᵧᩩᱫŭɯٱᅳት塷ᱹ፻౽ꁿꢇ뒓鍊낝", a_));
				IL_100:
				IL_122:
				if (true)
				{
				}
			}
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x060018BB RID: 6331 RVA: 0x000E9E5C File Offset: 0x000E8E5C
		// (set) Token: 0x060018BC RID: 6332 RVA: 0x000E9EA0 File Offset: 0x000E8EA0
		public bool HasMajorGridLines
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
				int a_ = 6;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
					{
						XlsChart xlsChart;
						if (xlsChart.TypeChanging)
						{
							goto IL_A2;
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
							num = 8;
							continue;
						}
						break;
					}
					case 2:
						if (true)
						{
						}
						num = 3;
						continue;
					case 3:
						goto IL_CD;
					case 4:
						this.ᜈ = (value ? new ChartGridLine((spr\u2158)base.ReservedHandle, this, AxisLineIdentifierType.MajorGridLine) : null);
						num = 5;
						continue;
					case 5:
						return;
					case 6:
					{
						XlsChart xlsChart;
						if (!xlsChart.CheckForSupportGridLine())
						{
							num = 2;
							continue;
						}
						goto IL_A2;
					}
					case 7:
					{
						XlsChart xlsChart = this.ᜌ.ᜇ();
						num = 1;
						continue;
					}
					case 8:
						num = 6;
						continue;
					}
					if (value != this.HasMajorGridLines)
					{
						num = 7;
						continue;
					}
					return;
					IL_A2:
					this.ᜊ = value;
					num = 4;
				}
				IL_CD:
				throw new ApplicationException(RecordTableEnumerator.b("笻䰽⤿♁⡃⽅♇⽉㽋湍㍏㍑㩓㡕㝗⹙籛㱝՟䉡ᝣ፥ᡧᩩͫᱭѯ᝱ၳ噵ṷᕹ๻幽ꚅﲍ늑ﾙ늛", a_));
			}
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x060018BD RID: 6333 RVA: 0x000E9FD8 File Offset: 0x000E8FD8
		internal sprᾹ ParentAxis
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
				return this.ᜌ;
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x060018BE RID: 6334 RVA: 0x000EA01C File Offset: 0x000E901C
		// (set) Token: 0x060018BF RID: 6335 RVA: 0x000EA060 File Offset: 0x000E9060
		public int NumberFormatIndex
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
				this.\u170D = value;
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x060018C0 RID: 6336 RVA: 0x000EA0A4 File Offset: 0x000E90A4
		// (set) Token: 0x060018C1 RID: 6337 RVA: 0x000EA16C File Offset: 0x000E916C
		public string NumberFormat
		{
			get
			{
				int a_;
				spr\u21FF spr_u21FF;
				for (;;)
				{
					a_ = this.\u170D;
					spr_u21FF = this.ParentWorkbook.InnerFormats;
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_72;
						case 1:
							goto IL_4A;
						case 2:
							if (this.\u170D != -1)
							{
								num = 0;
								continue;
							}
							goto IL_4A;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_72;
							default:
								goto IL_6A;
							}
							break;
						case 4:
							if (!spr_u21FF.ᜀ(this.\u170D))
							{
								num = 1;
								continue;
							}
							goto IL_A2;
						}
						break;
						IL_4A:
						a_ = 0;
						num = 3;
						continue;
						IL_72:
						num = 4;
					}
				}
				IL_6A:
				if (false)
				{
				}
				IL_A2:
				if (true)
				{
				}
				sprᤅ sprᤅ = spr_u21FF.ᜁ(a_);
				return sprᤅ.ᜂ();
			}
			set
			{
				int a_ = 2;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_99;
					case 1:
						goto IL_34;
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
							if (value.Length == 0)
							{
								num = 0;
								continue;
							}
							goto IL_B7;
						}
						break;
					}
					IL_29:
					if (value == null)
					{
						num = 1;
						continue;
					}
					num = 2;
					continue;
					goto IL_29;
				}
				IL_34:
				throw new ArgumentNullException(RecordTableEnumerator.b("瘷伹儻尽┿ぁɃ⥅㩇❉ⵋ㩍", a_));
				IL_99:
				if (true)
				{
				}
				throw new ArgumentException(RecordTableEnumerator.b("欷丹主圽⸿╁摃╅⥇⑉≋⅍⑏牑㙓㍕硗㽙ㅛ⹝ᑟ᭡䩣", a_), RecordTableEnumerator.b("瘷伹儻尽┿ぁɃ⥅㩇❉ⵋ㩍", a_));
				IL_B7:
				this.\u170D = this.ParentWorkbook.InnerFormats.ᜉ(value);
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x060018C2 RID: 6338 RVA: 0x000EA248 File Offset: 0x000E9248
		// (set) Token: 0x060018C3 RID: 6339 RVA: 0x000EA290 File Offset: 0x000E9290
		public TickMarkType MinorTickMark
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
				return this.ᜄ.ᜅ();
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
				this.ᜄ.ᜀ(value);
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x060018C4 RID: 6340 RVA: 0x000EA2D8 File Offset: 0x000E92D8
		// (set) Token: 0x060018C5 RID: 6341 RVA: 0x000EA320 File Offset: 0x000E9320
		public TickMarkType MajorTickMark
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
				return this.ᜄ.ᜇ();
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
				this.ᜄ.ᜁ(value);
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x060018C6 RID: 6342 RVA: 0x000EA368 File Offset: 0x000E9368
		public ChartBorder Border
		{
			get
			{
				if (true)
				{
				}
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_40;
						default:
							goto IL_75;
						}
						break;
					case 2:
						goto IL_40;
					}
					if (this.ᜎ == null)
					{
						num = 2;
						continue;
					}
					goto IL_7D;
					IL_40:
					this.ᜎ = new ChartBorder((spr\u2158)base.ReservedHandle, this);
					num = 1;
				}
				IL_75:
				if (false)
				{
				}
				IL_7D:
				return (ChartBorder)this.ᜎ;
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x060018C7 RID: 6343 RVA: 0x000EA400 File Offset: 0x000E9400
		// (set) Token: 0x060018C8 RID: 6344 RVA: 0x000EA448 File Offset: 0x000E9448
		public TickLabelPositionType TickLabelPosition
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
				return this.ᜄ.ᜊ();
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
				this.ᜄ.ᜀ(value);
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x060018C9 RID: 6345 RVA: 0x000EA490 File Offset: 0x000E9490
		// (set) Token: 0x060018CA RID: 6346 RVA: 0x000EA4D4 File Offset: 0x000E94D4
		public bool Visible
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
				return !this.Deleted;
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							this.Border.Pattern = (value ? ChartLinePatternType.Solid : ChartLinePatternType.None);
							this.Deleted = !value;
							num = 3;
							continue;
						}
						break;
					case 3:
						return;
					}
					if (value == this.Visible)
					{
						break;
					}
					num = 0;
				}
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x060018CB RID: 6347 RVA: 0x000EA578 File Offset: 0x000E9578
		// (set) Token: 0x060018CC RID: 6348 RVA: 0x000EA5BC File Offset: 0x000E95BC
		public AxisTextDirectionType Alignment
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

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x060018CD RID: 6349 RVA: 0x000EA600 File Offset: 0x000E9600
		// (set) Token: 0x060018CE RID: 6350 RVA: 0x000EA644 File Offset: 0x000E9644
		public bool IsReversed
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
				return this.IsReverseOrder;
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
				this.IsReverseOrder = value;
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x060018CF RID: 6351
		// (set) Token: 0x060018D0 RID: 6352
		public abstract bool IsReverseOrder { get; set; }

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x060018D1 RID: 6353 RVA: 0x000EA688 File Offset: 0x000E9688
		// (set) Token: 0x060018D2 RID: 6354 RVA: 0x000EA6CC File Offset: 0x000E96CC
		public int AxisId
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
			internal set
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

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x060018D3 RID: 6355 RVA: 0x000EA710 File Offset: 0x000E9710
		// (set) Token: 0x060018D4 RID: 6356 RVA: 0x000EA754 File Offset: 0x000E9754
		internal sprό ChartTick
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
			set
			{
				int a_ = 0;
				while (value == null)
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
						throw new ArgumentNullException(RecordTableEnumerator.b("䀵夷嘹䤻嬽", a_));
					}
				}
				this.ᜄ = value;
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x060018D5 RID: 6357 RVA: 0x000EA7B8 File Offset: 0x000E97B8
		internal XlsChart ParentXlsChart
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
				return this.ᜌ.ᜅ;
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x060018D6 RID: 6358 RVA: 0x000EA800 File Offset: 0x000E9800
		// (set) Token: 0x060018D7 RID: 6359 RVA: 0x000EA844 File Offset: 0x000E9844
		public bool Deleted
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
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜑ = value;
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x060018D8 RID: 6360 RVA: 0x000EA888 File Offset: 0x000E9888
		// (set) Token: 0x060018D9 RID: 6361 RVA: 0x000EA8CC File Offset: 0x000E98CC
		public bool AutoTickLabelSpacing
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
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.\u1712 = value;
			}
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x060018DA RID: 6362 RVA: 0x000EA910 File Offset: 0x000E9910
		// (set) Token: 0x060018DB RID: 6363 RVA: 0x000EA954 File Offset: 0x000E9954
		public bool AutoTickMarkSpacing
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
				return this.\u1713;
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
				this.\u1713 = value;
			}
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x060018DC RID: 6364 RVA: 0x000EA998 File Offset: 0x000E9998
		public ChartShadow Shadow
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_38;
						default:
							goto IL_68;
						}
						break;
					case 2:
						goto IL_38;
					}
					if (this.ᜅ == null)
					{
						num = 2;
						continue;
					}
					goto IL_78;
					IL_38:
					this.ᜅ = new ChartShadow(base.AppImplementation, this);
					num = 1;
				}
				IL_68:
				if (false)
				{
				}
				if (true)
				{
				}
				IL_78:
				return this.ᜅ;
			}
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x060018DD RID: 6365 RVA: 0x000EAA24 File Offset: 0x000E9A24
		public IShadow ShadowProperties
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
				return this.Shadow;
			}
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x060018DE RID: 6366 RVA: 0x000EAA68 File Offset: 0x000E9A68
		// (set) Token: 0x060018DF RID: 6367 RVA: 0x000EAAB0 File Offset: 0x000E9AB0
		internal bool HasShadowProperties
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
				return this.ᜅ != null;
			}
			set
			{
				while (value)
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
						ChartShadow shadow = this.Shadow;
						return;
					}
					}
				}
				if (true)
				{
				}
				this.ᜅ = null;
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x060018E0 RID: 6368 RVA: 0x000EAB00 File Offset: 0x000E9B00
		public IFormat3D Chart3DOptions
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_38;
						default:
							goto IL_70;
						}
						break;
					case 2:
						goto IL_38;
					}
					if (this.\u1715 == null)
					{
						num = 2;
						continue;
					}
					goto IL_78;
					IL_38:
					this.\u1715 = new Format3D(base.AppImplementation, this);
					if (true)
					{
					}
					num = 1;
				}
				IL_70:
				if (false)
				{
				}
				IL_78:
				return this.\u1715;
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x060018E1 RID: 6369 RVA: 0x000EAB8C File Offset: 0x000E9B8C
		public IFormat3D Chart3DProperties
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
				return this.Chart3DOptions;
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x060018E2 RID: 6370 RVA: 0x000EABD0 File Offset: 0x000E9BD0
		// (set) Token: 0x060018E3 RID: 6371 RVA: 0x000EAC18 File Offset: 0x000E9C18
		public bool Has3dProperties
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
				return this.\u1715 != null;
			}
			internal set
			{
				for (;;)
				{
					if (true)
					{
					}
					if (!value)
					{
						goto IL_3B;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_2D;
					}
				}
				IL_2D:
				if (false)
				{
				}
				IFormat3D chart3DOptions = this.Chart3DOptions;
				return;
				IL_3B:
				this.\u1715 = null;
			}
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x060018E4 RID: 6372 RVA: 0x000EAC68 File Offset: 0x000E9C68
		// (set) Token: 0x060018E5 RID: 6373 RVA: 0x000EACAC File Offset: 0x000E9CAC
		internal ChartAxisPos? AxisPosition
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
				return this.\u1716;
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
				this.\u1716 = value;
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x060018E6 RID: 6374 RVA: 0x000EACF0 File Offset: 0x000E9CF0
		// (set) Token: 0x060018E7 RID: 6375 RVA: 0x000EAD34 File Offset: 0x000E9D34
		internal bool IsSourceLinked
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
				return this.\u1717;
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
				this.\u1717 = value;
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x060018E8 RID: 6376 RVA: 0x000EAD78 File Offset: 0x000E9D78
		public IChartFrameFormat FrameFormat
		{
			get
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
							goto IL_38;
						default:
							goto IL_5C;
						}
						break;
					case 2:
						goto IL_38;
					}
					if (this.\u1719 == null)
					{
						num = 2;
						continue;
					}
					goto IL_6C;
					IL_38:
					this.InitFrameFormat();
					num = 0;
				}
				IL_5C:
				if (true)
				{
				}
				if (false)
				{
				}
				IL_6C:
				return this.\u1719;
			}
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x060018E9 RID: 6377 RVA: 0x000EADF8 File Offset: 0x000E9DF8
		public bool HasAxisTitle
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
				return this.ᜃ != null;
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x060018EA RID: 6378 RVA: 0x000EAE40 File Offset: 0x000E9E40
		// (set) Token: 0x060018EB RID: 6379 RVA: 0x000EAE84 File Offset: 0x000E9E84
		public ChartParagraphType ParagraphType
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
				return this.\u1718;
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
				this.\u1718 = value;
			}
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x060018EC RID: 6380 RVA: 0x000EAEC8 File Offset: 0x000E9EC8
		// (set) Token: 0x060018ED RID: 6381 RVA: 0x000EAF0C File Offset: 0x000E9F0C
		internal bool IsDefaultTextSettings
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
				return this.\u171A;
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
				this.\u171A = value;
			}
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x060018EE RID: 6382
		protected abstract ObjectTextLinkType TextLinkType { get; }

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x060018EF RID: 6383 RVA: 0x000EAF50 File Offset: 0x000E9F50
		protected XlsWorkbook ParentWorkbook
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
				return this.ᜌ.ᜅ.InnerWorkbook;
			}
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x000EAF9C File Offset: 0x000E9F9C
		internal virtual void Parse(IList<BiffRecordRaw> data, ref int iPos, bool isPrimary)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					this.ᜂ = isPrimary;
					BiffRecordRaw biffRecordRaw = data[iPos];
					biffRecordRaw.CheckTypeCode(TBIFFRecord.ChartAxis);
					spr\u2426 spr_u = (spr\u2426)biffRecordRaw;
					iPos++;
					biffRecordRaw = data[iPos];
					biffRecordRaw.CheckTypeCode(TBIFFRecord.Begin);
					int num = 5;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							iPos++;
							spr\u2426.ChartAxisType chartAxisType = spr_u.ᜃ();
							num = 12;
							continue;
						}
						case 1:
							goto IL_205;
						case 2:
							goto IL_2B8;
						case 3:
							num = 9;
							continue;
						case 4:
							goto IL_C0;
						case 5:
							IL_BB:
							goto IL_2B8;
						case 6:
							goto IL_C0;
						case 7:
							goto IL_159;
						case 8:
						{
							TBIFFRecord typeCode;
							if (typeCode != TBIFFRecord.ChartFontx)
							{
								num = 11;
								continue;
							}
							spr\u2241 a_ = (spr\u2241)data[iPos];
							this.ᜀ(a_);
							iPos++;
							num = 4;
							continue;
						}
						case 9:
						{
							TBIFFRecord typeCode;
							if (typeCode != TBIFFRecord.ChartAxisLineFormat)
							{
								num = 15;
								continue;
							}
							this.ᜀ(data, ref iPos);
							num = 6;
							continue;
						}
						case 10:
						{
							TBIFFRecord typeCode;
							if (typeCode != TBIFFRecord.ChartTick)
							{
								num = 3;
								continue;
							}
							this.ᜀ((sprό)data[iPos]);
							iPos++;
							num = 18;
							continue;
						}
						case 11:
							num = 20;
							continue;
						case 12:
						{
							spr\u2426.ChartAxisType chartAxisType;
							switch (chartAxisType)
							{
							case spr\u2426.ChartAxisType.CategoryAxis:
								goto IL_179;
							case spr\u2426.ChartAxisType.ValueAxis:
								goto IL_20A;
							case spr\u2426.ChartAxisType.SeriesAxis:
								goto IL_311;
							default:
								num = 1;
								continue;
							}
							break;
						}
						case 13:
						{
							TBIFFRecord typeCode;
							if (typeCode <= TBIFFRecord.ChartAxisLineFormat)
							{
								num = 22;
								continue;
							}
							num = 8;
							continue;
						}
						case 14:
							goto IL_159;
						case 15:
							num = 14;
							continue;
						case 16:
						{
							if (biffRecordRaw.TypeCode == TBIFFRecord.End)
							{
								num = 0;
								continue;
							}
							TBIFFRecord typeCode = biffRecordRaw.TypeCode;
							num = 13;
							continue;
						}
						case 17:
							num = 7;
							continue;
						case 18:
							goto IL_C0;
						case 19:
							goto IL_C0;
						case 20:
						{
							TBIFFRecord typeCode;
							if (typeCode != TBIFFRecord.ChartIfmt)
							{
								num = 17;
								continue;
							}
							this.ᜀ(biffRecordRaw as sprᴏ);
							iPos++;
							num = 21;
							continue;
						}
						case 21:
							goto IL_C0;
						case 22:
							num = 10;
							continue;
						}
						break;
						IL_C0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_BB;
						default:
							if (false)
							{
							}
							biffRecordRaw = data[iPos];
							num = 2;
							continue;
						}
						IL_159:
						this.ParseData(biffRecordRaw, data, ref iPos);
						iPos++;
						num = 19;
						continue;
						IL_2B8:
						num = 16;
					}
				}
				IL_179:
				this.ᜁ = AxisType.Category;
				return;
				IL_205:
				if (true)
				{
				}
				return;
				IL_20A:
				this.ᜁ = AxisType.Value;
				return;
				IL_311:
				this.ᜁ = AxisType.Serie;
				return;
			}
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x000EB2CC File Offset: 0x000EA2CC
		private void ᜀ(IList<BiffRecordRaw> A_0, ref int A_1)
		{
			int a_ = 8;
			int num = 3;
			for (;;)
			{
				AxisLineIdentifierType axisLineIdentifierType;
				switch (num)
				{
				case 0:
					if (A_0[A_1].TypeCode == TBIFFRecord.ChartLineFormat)
					{
						goto IL_6A;
					}
					return;
				case 1:
					goto IL_180;
				case 2:
					goto IL_40;
				case 4:
					goto IL_72;
				case 5:
					switch (axisLineIdentifierType)
					{
					case AxisLineIdentifierType.AxisLineItself:
						A_1++;
						num = 0;
						continue;
					case AxisLineIdentifierType.MajorGridLine:
						goto IL_154;
					case AxisLineIdentifierType.MinorGridLine:
						goto IL_77;
					case AxisLineIdentifierType.WallsOrFloor:
						goto IL_137;
					default:
						num = 6;
						continue;
					}
					break;
				case 6:
					num = 1;
					continue;
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				spr\u231E spr_u231E = (spr\u231E)A_0[A_1];
				axisLineIdentifierType = spr_u231E.ᜀ();
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
					num = 5;
					continue;
				}
				IL_6A:
				num = 4;
			}
			IL_40:
			throw new ArgumentNullException(RecordTableEnumerator.b("娽ℿ㙁╃", a_));
			IL_72:
			this.ᜎ = new ChartBorder((spr\u2158)base.ReservedHandle, this, A_0, ref A_1);
			return;
			IL_77:
			this.ᜋ = true;
			this.ᜉ = new ChartGridLine((spr\u2158)base.ReservedHandle, this, A_0, ref A_1);
			return;
			IL_137:
			this.ParseWallsOrFloor(A_0, ref A_1);
			return;
			IL_154:
			this.ᜊ = true;
			this.ᜈ = new ChartGridLine((spr\u2158)base.ReservedHandle, this, A_0, ref A_1);
			return;
			IL_180:
			throw new NotSupportedException(RecordTableEnumerator.b("欽⸿⥁⩃⥅㽇⑉汋≍㥏㱑ㅓ癕ㅗ㑙㡛㭝๟ᙡൣeŧཀྵṫ䁭", a_));
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x000EB45C File Offset: 0x000EA45C
		internal void ᜀ(spr\u2241 A_0)
		{
			int a_ = 13;
			while (A_0 != null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				if (true)
				{
				}
				if (false)
				{
				}
				int index = (int)A_0.ᜀ();
				XlsWorkbook parentWorkbook = this.ParentWorkbook;
				XlsFont font = (XlsFont)parentWorkbook.InnerFonts[index];
				this.ᜇ = new FontWrapper(font);
				return;
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("╂⩄⥆㵈㍊", a_));
		}

		// Token: 0x060018F3 RID: 6387
		internal abstract void ParseWallsOrFloor(IList<BiffRecordRaw> data, ref int iPos);

		// Token: 0x060018F4 RID: 6388 RVA: 0x000EB4E8 File Offset: 0x000EA4E8
		internal void ᜀ(sprᴏ A_0)
		{
			int a_ = 4;
			for (;;)
			{
				if (true)
				{
				}
				if (A_0 == null)
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
			throw new ArgumentNullException(RecordTableEnumerator.b("䠹夻崽⼿ぁ⁃", a_));
			IL_4A:
			if (false)
			{
			}
			this.NumberFormatIndex = (int)A_0.ᜀ();
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x000EB554 File Offset: 0x000EA554
		[CLSCompliant(false)]
		internal virtual void ParseData(BiffRecordRaw record, IList<BiffRecordRaw> data, ref int iPos)
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

		// Token: 0x060018F6 RID: 6390 RVA: 0x000EB590 File Offset: 0x000EA590
		private void ᜀ(sprό A_0)
		{
			int a_ = 16;
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_40;
				case 1:
					goto IL_C1;
				case 2:
					if (A_0.ᜈ())
					{
						num = 6;
						continue;
					}
					goto IL_C1;
				case 3:
					goto IL_BF;
				case 5:
					if (A_0.ᜋ())
					{
						num = 3;
						continue;
					}
					num = 2;
					continue;
				case 6:
					this.ᜏ = AxisTextDirectionType.RightToLeft;
					num = 1;
					continue;
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				this.ᜄ = A_0;
				this.ᜏ = AxisTextDirectionType.Context;
				IL_A1:
				num = 5;
				continue;
				IL_C1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A1;
				default:
					goto IL_D7;
				}
			}
			IL_40:
			throw new ArgumentNullException(RecordTableEnumerator.b("╅⁇⭉㹋㩍я㭑㝓㵕", a_));
			IL_BF:
			this.ᜏ = AxisTextDirectionType.LeftToRight;
			return;
			IL_D7:
			if (false)
			{
			}
			if (true)
			{
			}
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x000EB684 File Offset: 0x000EA684
		internal virtual void SerializeDataToList(RecordArrayList records)
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

		// Token: 0x060018F8 RID: 6392 RVA: 0x000EB6C4 File Offset: 0x000EA6C4
		internal void ᜇ(RecordArrayList A_0)
		{
			int a_ = 3;
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_5C;
				case 1:
					if (this.ᜃ != null)
					{
						num = 3;
						continue;
					}
					return;
				case 2:
					return;
				case 3:
					this.ᜃ.SerializeDataToList(A_0);
					num = 2;
					continue;
				}
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
					if (A_0 == null)
					{
						num = 0;
					}
					else
					{
						num = 1;
					}
					break;
				}
			}
			IL_5C:
			throw new ArgumentNullException(RecordTableEnumerator.b("䬸帺帼倾㍀❂㙄", a_));
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x000EB780 File Offset: 0x000EA780
		internal void ᜃ(RecordArrayList A_0)
		{
			int a_ = 1;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					if (this.ᜇ != null)
					{
						num = 2;
						continue;
					}
					return;
				case 2:
				{
					spr\u2241 spr_u = (spr\u2241)spr\u175E.ᜀ(TBIFFRecord.ChartFontx);
					spr_u.ᜀ((ushort)this.ᜇ.Index);
					A_0.ᜀ(spr_u);
					num = 3;
					continue;
				}
				case 3:
					return;
				case 4:
					goto IL_5C;
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
					if (A_0 == null)
					{
						if (true)
						{
						}
						num = 4;
					}
					else
					{
						num = 1;
					}
					break;
				}
			}
			IL_5C:
			throw new ArgumentNullException(RecordTableEnumerator.b("䔶尸堺刼䴾╀あ", a_));
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x000EB860 File Offset: 0x000EA860
		internal void ᜄ(RecordArrayList A_0)
		{
			int a_ = 11;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_46;
				case 2:
					return;
				case 3:
					this.ᜉ.SerializeDataToList(A_0);
					num = 2;
					continue;
				case 4:
					goto IL_44;
				case 5:
					this.ᜈ.SerializeDataToList(A_0);
					num = 0;
					continue;
				case 6:
					if (this.ᜈ != null)
					{
						num = 5;
						continue;
					}
					goto IL_46;
				case 7:
					if (this.ᜉ != null)
					{
						goto IL_61;
					}
					return;
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_61;
				default:
					if (false)
					{
					}
					num = 6;
					continue;
				}
				IL_46:
				if (true)
				{
				}
				num = 7;
				continue;
				IL_61:
				num = 3;
			}
			IL_44:
			throw new ArgumentNullException(RecordTableEnumerator.b("㍀♂♄⡆㭈⽊㹌", a_));
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x000EB968 File Offset: 0x000EA968
		internal void ᜂ(RecordArrayList A_0)
		{
			int a_ = 4;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
				{
					if (true)
					{
					}
					sprᴏ sprᴏ = (sprᴏ)spr\u175E.ᜀ(TBIFFRecord.ChartIfmt);
					sprᴏ.ᜀ((ushort)this.NumberFormatIndex);
					A_0.ᜀ(sprᴏ);
					num = 0;
					continue;
				}
				case 2:
					goto IL_54;
				case 4:
					if (this.NumberFormatIndex != -1)
					{
						num = 1;
						continue;
					}
					return;
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
					if (A_0 == null)
					{
						num = 2;
					}
					else
					{
						num = 4;
					}
					break;
				}
			}
			IL_54:
			throw new ArgumentNullException(RecordTableEnumerator.b("䠹夻崽⼿ぁ⁃㕅", a_));
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x000EBA44 File Offset: 0x000EAA44
		[CLSCompliant(false)]
		internal void ᜅ(RecordArrayList A_0)
		{
			int a_ = 1;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜎ == null)
					{
						goto IL_4F;
					}
					goto IL_93;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4F;
					default:
						goto IL_6D;
					}
					break;
				case 3:
					goto IL_34;
				}
				if (A_0 == null)
				{
					num = 3;
					continue;
				}
				if (true)
				{
				}
				num = 0;
				continue;
				IL_4F:
				num = 2;
			}
			IL_34:
			throw new ArgumentNullException(RecordTableEnumerator.b("䔶尸堺刼䴾╀あ", a_));
			IL_6D:
			if (false)
			{
			}
			return;
			IL_93:
			spr\u231E spr_u231E = (spr\u231E)spr\u175E.ᜀ(TBIFFRecord.ChartAxisLineFormat);
			spr_u231E.ᜀ(AxisLineIdentifierType.AxisLineItself);
			A_0.ᜀ(spr_u231E);
			this.ᜎ.ᜀ(A_0);
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x000EBB10 File Offset: 0x000EAB10
		[CLSCompliant(false)]
		internal void ᜆ(RecordArrayList A_0)
		{
			int a_ = 5;
			int num = 3;
			sprό sprό;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					sprό.ᜀ(true);
					num = 1;
					continue;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						goto IL_9E;
					}
					break;
				case 2:
					goto IL_4F;
				case 4:
					if (this.Alignment == AxisTextDirectionType.RightToLeft)
					{
						num = 5;
						continue;
					}
					goto IL_114;
				case 5:
					sprό.ᜁ(true);
					num = 7;
					continue;
				case 6:
					if (this.Alignment == AxisTextDirectionType.LeftToRight)
					{
						num = 0;
						continue;
					}
					num = 4;
					continue;
				case 7:
					goto IL_CC;
				}
				if (A_0 == null)
				{
					num = 2;
				}
				else
				{
					sprό = (sprό)this.ᜄ.Clone();
					sprό.ᜀ(false);
					sprό.ᜁ(false);
					num = 6;
				}
			}
			IL_4F:
			throw new ArgumentNullException(RecordTableEnumerator.b("䤺堼尾⹀ㅂ⅄㑆", a_));
			IL_9E:
			if (false)
			{
			}
			IL_CC:
			IL_114:
			A_0.ᜀ(sprό);
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x000EBC3C File Offset: 0x000EAC3C
		protected virtual void InitializeVariables()
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
			this.\u1718 = ChartParagraphType.None;
			this.ᜀ();
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x000EBC8C File Offset: 0x000EAC8C
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
			this.ᜄ = (sprό)spr\u175E.ᜀ(TBIFFRecord.ChartTick);
			this.ᜄ.ᜁ(TickMarkType.TickMarkOutside);
			this.ᜄ.ᜀ(TickLabelPositionType.TickLabelPositionNextToAxis);
			this.ᜄ.ᜄ(true);
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x000EBD00 File Offset: 0x000EAD00
		protected internal void SetTitleArea(XlsChartTextArea titleArea)
		{
			int a_ = 13;
			while (titleArea != null)
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
					this.ᜃ = titleArea;
					return;
				}
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㝂ⱄ㍆╈⹊ౌ㵎㑐㉒", a_));
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x000EBD64 File Offset: 0x000EAD64
		public virtual XlsChartAxis Clone(object parent, Dictionary<int, int> dicFontIndexes, Dictionary<string, string> dicNewSheetNames)
		{
			XlsChartAxis xlsChartAxis;
			for (;;)
			{
				xlsChartAxis = (XlsChartAxis)base.MemberwiseClone();
				xlsChartAxis.SetParent(parent);
				xlsChartAxis.ᜁ();
				xlsChartAxis.NumberFormat = this.NumberFormat;
				xlsChartAxis.ᜁ = this.ᜁ;
				xlsChartAxis.m_bIsDisposed = this.m_bIsDisposed;
				xlsChartAxis.ᜆ = this.ᜆ;
				xlsChartAxis.ᜂ = this.ᜂ;
				int num = 12;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜎ != null)
						{
							num = 13;
							continue;
						}
						goto IL_264;
					case 1:
						if (this.ᜃ != null)
						{
							num = 8;
							continue;
						}
						goto IL_107;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_150;
						default:
							if (false)
							{
							}
							if (this.ᜉ != null)
							{
								num = 3;
								continue;
							}
							goto IL_150;
						}
						break;
					case 3:
						xlsChartAxis.ᜉ = (XlsChartGridLine)this.ᜉ.Clone(xlsChartAxis);
						num = 11;
						continue;
					case 4:
						xlsChartAxis.ᜈ = (XlsChartGridLine)this.ᜈ.Clone(xlsChartAxis);
						num = 15;
						continue;
					case 5:
						if (this.ᜇ != null)
						{
							num = 7;
							continue;
						}
						return xlsChartAxis;
					case 6:
						goto IL_12A;
					case 7:
						xlsChartAxis.ᜇ = this.ᜇ.Clone(xlsChartAxis.ParentWorkbook, this, dicFontIndexes);
						num = 10;
						continue;
					case 8:
						xlsChartAxis.ᜃ = (XlsChartTextArea)this.ᜃ.Clone(xlsChartAxis, dicFontIndexes, dicNewSheetNames);
						num = 17;
						continue;
					case 9:
						goto IL_264;
					case 10:
						return xlsChartAxis;
					case 11:
						goto IL_150;
					case 12:
						if (this.ᜄ != null)
						{
							num = 14;
							continue;
						}
						goto IL_12A;
					case 13:
						xlsChartAxis.ᜎ = this.ᜎ.Clone(xlsChartAxis);
						num = 9;
						continue;
					case 14:
						if (true)
						{
						}
						xlsChartAxis.ᜄ = (sprό)this.ᜄ.Clone();
						num = 6;
						continue;
					case 15:
						goto IL_C8;
					case 16:
						if (this.ᜈ != null)
						{
							num = 4;
							continue;
						}
						goto IL_C8;
					case 17:
						goto IL_107;
					}
					break;
					IL_C8:
					num = 2;
					continue;
					IL_107:
					num = 16;
					continue;
					IL_12A:
					num = 0;
					continue;
					IL_150:
					num = 5;
					continue;
					IL_264:
					num = 1;
				}
			}
			return xlsChartAxis;
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x000EBFFC File Offset: 0x000EAFFC
		public void SetTitle(XlsChartTextArea text)
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
			this.ᜃ = text;
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x000EC040 File Offset: 0x000EB040
		public void UpdateTickRecord(TickLabelPositionType value)
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
			this.ᜄ.ᜀ(value);
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x000EC088 File Offset: 0x000EB088
		public void MarkUsedReferences(bool[] usedItems)
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
			this.ᜃ.ᜀ(usedItems);
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x000EC0D0 File Offset: 0x000EB0D0
		public void UpdateReferenceIndexes(int[] arrUpdatedIndexes)
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
			this.ᜃ.ᜀ(arrUpdatedIndexes);
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x000EC118 File Offset: 0x000EB118
		protected void InitFrameFormat()
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
			this.\u1719 = this.CreateFrameFormat();
			sprᳫ sprᳫ = this.\u1719.FrameRecord;
			sprᳫ.ᜁ(true);
			this.\u1719.Border.Pattern = ChartLinePatternType.None;
			this.\u1719.Border.UseDefaultFormat = false;
			this.\u1719.Interior.UseDefaultFormat = false;
			this.\u1719.Interior.Pattern = ExcelPatternType.None;
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x000EC1B8 File Offset: 0x000EB1B8
		protected virtual XlsChartFrameFormat CreateFrameFormat()
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
			return new XlsChartFrameFormat(base.AppImplementation, this);
		}

		// Token: 0x04000FE0 RID: 4064
		protected const int DEF_NUMBER_FORMAT_INDEX = -1;

		// Token: 0x04000FE1 RID: 4065
		private const int ᜀ = 0;

		// Token: 0x04000FE2 RID: 4066
		private AxisType ᜁ;

		// Token: 0x04000FE3 RID: 4067
		private bool ᜂ;

		// Token: 0x04000FE4 RID: 4068
		private XlsChartTextArea ᜃ;

		// Token: 0x04000FE5 RID: 4069
		private sprό ᜄ;

		// Token: 0x04000FE6 RID: 4070
		private ChartShadow ᜅ;

		// Token: 0x04000FE7 RID: 4071
		private bool ᜆ;

		// Token: 0x04000FE8 RID: 4072
		private long \u2593\u00AE\u00AF\u0086;

		// Token: 0x04000FE9 RID: 4073
		private FontWrapper ᜇ;

		// Token: 0x04000FEA RID: 4074
		private XlsChartGridLine ᜈ;

		// Token: 0x04000FEB RID: 4075
		private XlsChartGridLine ᜉ;

		// Token: 0x04000FEC RID: 4076
		private bool ᜊ;

		// Token: 0x04000FED RID: 4077
		private bool ᜋ;

		// Token: 0x04000FEE RID: 4078
		private bool \u25D9\u00A2\u0088\u009B;

		// Token: 0x04000FEF RID: 4079
		private sprᾹ ᜌ;

		// Token: 0x04000FF0 RID: 4080
		private int \u170D = -1;

		// Token: 0x04000FF1 RID: 4081
		private XlsChartBorder ᜎ;

		// Token: 0x04000FF2 RID: 4082
		private AxisTextDirectionType ᜏ;

		// Token: 0x04000FF3 RID: 4083
		private int ᜐ;

		// Token: 0x04000FF4 RID: 4084
		private bool ᜑ;

		// Token: 0x04000FF5 RID: 4085
		private bool \u1712;

		// Token: 0x04000FF6 RID: 4086
		private bool \u1713;

		// Token: 0x04000FF7 RID: 4087
		internal string \u1714;

		// Token: 0x04000FF8 RID: 4088
		private Format3D \u1715;

		// Token: 0x04000FF9 RID: 4089
		private ChartAxisPos? \u1716;

		// Token: 0x04000FFA RID: 4090
		private bool \u1717;

		// Token: 0x04000FFB RID: 4091
		private ChartParagraphType \u1718;

		// Token: 0x04000FFC RID: 4092
		private XlsChartFrameFormat \u1719;

		// Token: 0x04000FFD RID: 4093
		private bool \u171A;

		// Token: 0x04000FFE RID: 4094
		private bool \u171B;
	}
}
