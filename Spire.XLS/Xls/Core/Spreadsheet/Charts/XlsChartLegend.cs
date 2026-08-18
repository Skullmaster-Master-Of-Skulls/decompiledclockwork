using System;
using System.Collections.Generic;
using System.IO;
using Spire.Xls.Charts;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Charts
{
	// Token: 0x020001A3 RID: 419
	public class XlsChartLegend : XlsObject, IChartLegend
	{
		// Token: 0x0600150B RID: 5387 RVA: 0x000C7D64 File Offset: 0x000C6D64
		internal XlsChartLegend(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜁ = (spr\u1A8D)spr\u175E.ᜀ(TBIFFRecord.ChartLegend);
			this.ᜂ = (spr\u23BE)spr\u175E.ᜀ(TBIFFRecord.ChartPos);
			this.ᜂ.ᜁ(5);
			this.ᜃ = new ChartTextArea((spr\u2158)A_0, this);
			this.ᜇ = new ChartLegendEntriesColl(A_0, A_1);
			this.ᜄ();
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x000C7DDC File Offset: 0x000C6DDC
		private void ᜄ()
		{
			int a_ = 1;
			this.ᜆ = (XlsChart)base.FindParent(typeof(XlsChart));
			if (this.ᜆ != null)
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
			throw new ApplicationException(RecordTableEnumerator.b("朶堸䤺堼儾㕀捂⩄╆⍈⹊⹌㭎煐げ㑔㥖㝘㑚⥜罞͠٢䕤Ŧ٨Ṫͬ୮彰", a_));
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x000C7E5C File Offset: 0x000C6E5C
		internal void ᜀ(IList<BiffRecordRaw> A_0, ref int A_1)
		{
			int a_ = 17;
			int num = 4;
			for (;;)
			{
				int num2;
				BiffRecordRaw biffRecordRaw;
				switch (num)
				{
				case 0:
					goto IL_87;
				case 1:
					return;
				case 2:
					goto IL_236;
				case 3:
				{
					TBIFFRecord typeCode;
					if (typeCode != TBIFFRecord.ChartText)
					{
						num = 16;
						continue;
					}
					this.ᜃ = new ChartTextArea((spr\u2158)base.ReservedHandle, this);
					A_1 = this.ᜃ.ᜀ(A_0, A_1) - 1;
					num = 22;
					continue;
				}
				case 5:
					goto IL_1A1;
				case 6:
				{
					TBIFFRecord typeCode;
					switch (typeCode)
					{
					case TBIFFRecord.ChartFrame:
						this.ᜄ = new XlsChartFrameFormat(base.ReservedHandle, this, false);
						this.ᜄ.ᜀ(A_0, ref A_1);
						A_1--;
						num = 14;
						continue;
					case TBIFFRecord.Begin:
						A_1 = BiffRecordRaw.SkipBeginEndBlock(A_0, A_1);
						num = 18;
						continue;
					case TBIFFRecord.End:
						num2--;
						num = 5;
						continue;
					default:
						num = 19;
						continue;
					}
					break;
				}
				case 7:
					num = 8;
					continue;
				case 8:
				{
					TBIFFRecord typeCode;
					if (typeCode != TBIFFRecord.ChartTextPropsStream)
					{
						num = 10;
						continue;
					}
					if (true)
					{
					}
					this.ᜊ = (sprᱬ)A_0[A_1];
					num = 21;
					continue;
				}
				case 9:
					goto IL_1A1;
				case 10:
					num = 3;
					continue;
				case 11:
					goto IL_236;
				case 12:
				{
					TBIFFRecord typeCode;
					if (typeCode != TBIFFRecord.ChartPos)
					{
						num = 23;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_26C;
					default:
						if (false)
						{
						}
						this.ᜂ = (spr\u23BE)biffRecordRaw;
						num = 13;
						continue;
					}
					break;
				}
				case 13:
					goto IL_1A1;
				case 14:
					goto IL_1A1;
				case 15:
				{
					if (num2 == 0)
					{
						num = 1;
						continue;
					}
					biffRecordRaw = A_0[A_1];
					TBIFFRecord typeCode = biffRecordRaw.TypeCode;
					num = 20;
					continue;
				}
				case 16:
					num = 17;
					continue;
				case 17:
					goto IL_26C;
				case 18:
					goto IL_1A1;
				case 19:
					num = 12;
					continue;
				case 20:
				{
					TBIFFRecord typeCode;
					if (typeCode <= TBIFFRecord.ChartText)
					{
						num = 7;
						continue;
					}
					num = 6;
					continue;
				}
				case 21:
					goto IL_1A1;
				case 22:
					goto IL_1A1;
				case 23:
					num = 9;
					continue;
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				biffRecordRaw = A_0[A_1];
				biffRecordRaw.CheckTypeCode(TBIFFRecord.ChartLegend);
				this.ᜁ = (spr\u1A8D)A_0[A_1];
				A_1 += 2;
				num2 = 1;
				num = 11;
				continue;
				IL_1A1:
				A_1++;
				num = 2;
				continue;
				IL_26C:
				goto IL_1A1;
				IL_236:
				num = 15;
			}
			IL_87:
			throw new ArgumentNullException(RecordTableEnumerator.b("⍆⡈㽊ⱌ", a_));
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x000C8178 File Offset: 0x000C7178
		public void SerializeDataToList(RecordArrayList records)
		{
			int a_ = 7;
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_54;
					default:
						if (false)
						{
						}
						records.ᜀ((spr\u23BE)this.ᜂ.Clone());
						num = 11;
						continue;
					}
					break;
				case 1:
					this.ᜄ.ᜀ(records);
					num = 10;
					continue;
				case 2:
					goto IL_17A;
				case 3:
					goto IL_5C;
				case 4:
					if (this.ᜂ != null)
					{
						num = 0;
						continue;
					}
					goto IL_17C;
				case 5:
					if (this.ᜊ != null)
					{
						num = 9;
						continue;
					}
					goto IL_1CB;
				case 6:
					goto IL_13F;
				case 8:
					if (this.ᜄ != null)
					{
						num = 1;
						continue;
					}
					goto IL_FE;
				case 9:
					records.ᜀ(this.ᜊ);
					num = 2;
					continue;
				case 10:
					goto IL_FE;
				case 11:
					goto IL_17C;
				case 12:
					this.ᜃ.ᜀ(records, true);
					num = 6;
					continue;
				case 13:
					if (this.ᜃ != null)
					{
						num = 12;
						continue;
					}
					goto IL_13F;
				}
				goto IL_51;
				IL_54:
				num = 3;
				continue;
				IL_51:
				if (records == null)
				{
					goto IL_54;
				}
				records.ᜀ((spr\u1A8D)this.ᜁ.Clone());
				records.ᜀ(spr\u175E.ᜀ(TBIFFRecord.Begin));
				num = 4;
				continue;
				IL_FE:
				num = 5;
				continue;
				IL_13F:
				num = 8;
				continue;
				IL_17C:
				if (true)
				{
				}
				num = 13;
			}
			IL_5C:
			throw new ArgumentNullException(RecordTableEnumerator.b("似娾≀ⱂ㝄⍆㩈", a_));
			IL_17A:
			IL_1CB:
			records.ᜀ(spr\u175E.ᜀ(TBIFFRecord.End));
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x0600150F RID: 5391 RVA: 0x000C8364 File Offset: 0x000C7364
		protected internal IChartFrameFormat FrameFormat
		{
			get
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_71;
					case 1:
					{
						ExcelVersion version;
						if (version == ExcelVersion.Version2010)
						{
							num = 6;
							continue;
						}
						goto IL_F4;
					}
					case 2:
					{
						this.ᜄ = new XlsChartFrameFormat(base.ReservedHandle, this, true, false, true);
						this.ᜄ.Interior.UseDefaultFormat = true;
						ExcelVersion version = this.ᜆ.Workbook.Version;
						num = 4;
						continue;
					}
					case 4:
					{
						if (true)
						{
						}
						ExcelVersion version;
						if (version != ExcelVersion.Version2007)
						{
							goto IL_E7;
						}
						goto IL_5D;
					}
					case 5:
						num = 1;
						continue;
					case 6:
						goto IL_5D;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_E7:
						num = 5;
						continue;
					default:
						if (false)
						{
						}
						if (this.ᜄ == null)
						{
							num = 2;
							continue;
						}
						goto IL_F4;
					}
					IL_5D:
					this.ᜄ.HasLineProperties = false;
					num = 0;
				}
				IL_71:
				IL_F4:
				return this.ᜄ;
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06001510 RID: 5392 RVA: 0x000C846C File Offset: 0x000C746C
		public IChartTextArea TextArea
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_2E;
					case 2:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2E;
						default:
							goto IL_75;
						}
						break;
					}
					if (this.ᜃ == null)
					{
						num = 1;
						continue;
					}
					goto IL_7D;
					IL_2E:
					this.ᜃ = new ChartTextArea((spr\u2158)base.ReservedHandle, this);
					num = 2;
				}
				IL_75:
				if (false)
				{
				}
				IL_7D:
				return this.ᜃ;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06001511 RID: 5393 RVA: 0x000C84FC File Offset: 0x000C74FC
		// (set) Token: 0x06001512 RID: 5394 RVA: 0x000C8540 File Offset: 0x000C7540
		public bool IncludeInLayout
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
				int a_ = 15;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6C;
						default:
							if (false)
							{
							}
							if (value != this.ᜅ)
							{
								num = 3;
								continue;
							}
							return;
						}
						break;
					case 3:
						this.ᜅ = value;
						num = 0;
						continue;
					case 4:
						goto IL_59;
					}
					if (this.ᜆ.Workbook.Version == ExcelVersion.Version97to2003)
					{
						if (true)
						{
						}
						num = 4;
						continue;
					}
					IL_6C:
					num = 1;
				}
				IL_59:
				throw new ArgumentException(RecordTableEnumerator.b("ౄ⥆⩈❊㡌⭎㑐ᩒ㭔᭖㡘≚㉜⩞ᕠ䍢ᕤᕦ٨᭪࡬ᵮհੲ啴ᑶᡸᕺ嵼ᅾꖄꮊﺌ搜ﮞ膠쪢쮤螦쪨\udeaa\udfac\uddae풰\uddb2솴鞶\udcb8쎺\udebc\udabe귀도ꋆ믈룊꓌ꃎ뿐﷒", a_));
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06001513 RID: 5395 RVA: 0x000C8608 File Offset: 0x000C7608
		// (set) Token: 0x06001514 RID: 5396 RVA: 0x000C8650 File Offset: 0x000C7650
		public int X
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
				return this.LegendRecord.ᜋ();
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
				this.ᜀ();
				this.LegendRecord.ᜁ(value);
				this.PositionRecord.ᜃ(value);
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06001515 RID: 5397 RVA: 0x000C86AC File Offset: 0x000C76AC
		// (set) Token: 0x06001516 RID: 5398 RVA: 0x000C86F4 File Offset: 0x000C76F4
		public int Y
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
				return this.LegendRecord.ᜊ();
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
				this.ᜀ();
				this.LegendRecord.ᜀ(value);
				this.PositionRecord.ᜂ(value);
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06001517 RID: 5399 RVA: 0x000C8750 File Offset: 0x000C7750
		// (set) Token: 0x06001518 RID: 5400 RVA: 0x000C8798 File Offset: 0x000C7798
		public LegendPositionType Position
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
				return this.LegendRecord.ᜇ();
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_30;
					case 2:
						goto IL_6D;
					}
					if (value == LegendPositionType.NotDocked)
					{
						if (true)
						{
						}
						num = 0;
					}
					else
					{
						this.ᜁ();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_32;
						default:
							if (false)
							{
							}
							num = 2;
							break;
						}
					}
				}
				IL_30:
				IL_32:
				this.ᜀ();
				return;
				IL_6D:
				this.IsVerticalLegend = (value != LegendPositionType.Bottom && value != LegendPositionType.Top);
				this.LegendRecord.ᜀ(value);
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06001519 RID: 5401 RVA: 0x000C8838 File Offset: 0x000C7838
		// (set) Token: 0x0600151A RID: 5402 RVA: 0x000C8880 File Offset: 0x000C7880
		public bool IsVerticalLegend
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
				return this.LegendRecord.ᜆ();
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
				this.LegendRecord.ᜂ(value);
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x0600151B RID: 5403 RVA: 0x000C88C8 File Offset: 0x000C78C8
		public IChartLegendEntries LegendEntries
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
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x0600151C RID: 5404 RVA: 0x000C890C File Offset: 0x000C790C
		// (set) Token: 0x0600151D RID: 5405 RVA: 0x000C8954 File Offset: 0x000C7954
		public int Width
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
				return this.LegendRecord.ᜉ();
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
				this.LegendRecord.ᜂ(value);
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x0600151E RID: 5406 RVA: 0x000C899C File Offset: 0x000C799C
		// (set) Token: 0x0600151F RID: 5407 RVA: 0x000C89E4 File Offset: 0x000C79E4
		public int Height
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
				return this.LegendRecord.ᜂ();
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
				this.LegendRecord.ᜃ(value);
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x000C8A2C File Offset: 0x000C7A2C
		// (set) Token: 0x06001521 RID: 5409 RVA: 0x000C8A74 File Offset: 0x000C7A74
		public bool HasDataTable
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
				return this.LegendRecord.ᜃ();
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
				this.LegendRecord.ᜅ(value);
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06001522 RID: 5410 RVA: 0x000C8ABC File Offset: 0x000C7ABC
		// (set) Token: 0x06001523 RID: 5411 RVA: 0x000C8B04 File Offset: 0x000C7B04
		public LegendSpacingType Spacing
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
				return this.LegendRecord.ᜅ();
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
				this.LegendRecord.ᜀ(value);
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06001524 RID: 5412 RVA: 0x000C8B4C File Offset: 0x000C7B4C
		// (set) Token: 0x06001525 RID: 5413 RVA: 0x000C8B94 File Offset: 0x000C7B94
		public bool AutoPosition
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
				return this.LegendRecord.ᜄ();
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
				this.LegendRecord.ᜁ(value);
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06001526 RID: 5414 RVA: 0x000C8BDC File Offset: 0x000C7BDC
		// (set) Token: 0x06001527 RID: 5415 RVA: 0x000C8C24 File Offset: 0x000C7C24
		public bool AutoSeries
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
				return this.LegendRecord.ᜈ();
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
				this.LegendRecord.ᜀ(value);
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06001528 RID: 5416 RVA: 0x000C8C6C File Offset: 0x000C7C6C
		// (set) Token: 0x06001529 RID: 5417 RVA: 0x000C8CB4 File Offset: 0x000C7CB4
		public bool AutoPositionX
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
				return this.LegendRecord.ᜁ();
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
				this.LegendRecord.ᜄ(value);
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x0600152A RID: 5418 RVA: 0x000C8CFC File Offset: 0x000C7CFC
		// (set) Token: 0x0600152B RID: 5419 RVA: 0x000C8D44 File Offset: 0x000C7D44
		public bool AutoPositionY
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
				return this.LegendRecord.ᜀ();
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
				this.LegendRecord.ᜃ(value);
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x0600152C RID: 5420 RVA: 0x000C8D8C File Offset: 0x000C7D8C
		// (set) Token: 0x0600152D RID: 5421 RVA: 0x000C8DD0 File Offset: 0x000C7DD0
		internal Stream LayoutStream
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
				return this.ᜈ;
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
				this.ᜈ = value;
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x000C8E14 File Offset: 0x000C7E14
		// (set) Token: 0x0600152F RID: 5423 RVA: 0x000C8E58 File Offset: 0x000C7E58
		internal ChartParagraphType ParagraphType
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
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜉ = value;
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x000C8E9C File Offset: 0x000C7E9C
		private spr\u1A8D LegendRecord
		{
			get
			{
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
							goto IL_36;
						default:
							goto IL_73;
						}
						break;
					case 1:
						if (true)
						{
						}
						goto IL_36;
					}
					if (this.ᜁ == null)
					{
						num = 1;
						continue;
					}
					goto IL_7B;
					IL_36:
					this.ᜁ = (spr\u1A8D)spr\u175E.ᜀ(TBIFFRecord.ChartLegend);
					num = 0;
				}
				IL_73:
				if (false)
				{
				}
				IL_7B:
				return this.ᜁ;
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06001531 RID: 5425 RVA: 0x000C8F2C File Offset: 0x000C7F2C
		private spr\u23BE PositionRecord
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
							goto IL_36;
						default:
							goto IL_73;
						}
						break;
					case 2:
						goto IL_36;
					}
					if (this.ᜂ == null)
					{
						num = 2;
						continue;
					}
					goto IL_7B;
					IL_36:
					this.ᜂ = (spr\u23BE)spr\u175E.ᜀ(TBIFFRecord.ChartPos);
					num = 1;
				}
				IL_73:
				if (false)
				{
				}
				IL_7B:
				return this.ᜂ;
			}
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x000C8FBC File Offset: 0x000C7FBC
		public XlsChartLegend Clone(object parent, Dictionary<int, int> dicFontIndexes, Dictionary<string, string> dicNewSheetNames)
		{
			XlsChartLegend xlsChartLegend;
			for (;;)
			{
				for (;;)
				{
					xlsChartLegend = (XlsChartLegend)base.MemberwiseClone();
					xlsChartLegend.SetParent(parent);
					xlsChartLegend.ᜄ();
					xlsChartLegend.ᜁ = (spr\u1A8D)spr\u1CD3.ᜀ(this.ᜁ);
					xlsChartLegend.ᜂ = (spr\u23BE)spr\u1CD3.ᜀ(this.ᜂ);
					xlsChartLegend.ᜇ = this.ᜇ.Clone(xlsChartLegend, dicFontIndexes, dicNewSheetNames);
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (this.ᜄ != null)
							{
								num = 4;
								continue;
							}
							goto IL_E0;
						case 1:
							xlsChartLegend.ᜃ = (XlsChartTextArea)this.ᜃ.Clone(xlsChartLegend, dicFontIndexes, dicNewSheetNames);
							num = 2;
							continue;
						case 2:
							return xlsChartLegend;
						case 3:
							goto IL_E0;
						case 4:
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
								xlsChartLegend.ᜄ = this.ᜄ.Clone(xlsChartLegend);
								num = 3;
								continue;
							}
							break;
						case 5:
							if (this.ᜃ != null)
							{
								num = 1;
								continue;
							}
							return xlsChartLegend;
						}
						break;
						IL_E0:
						num = 5;
					}
				}
			}
			return xlsChartLegend;
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x000C90F4 File Offset: 0x000C80F4
		public void Clear()
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
			this.ᜄ.Clear();
			this.ᜇ.Clear();
			this.ᜂ = (spr\u23BE)spr\u175E.ᜀ(TBIFFRecord.ChartPos);
			this.ᜁ = (spr\u1A8D)spr\u175E.ᜀ(TBIFFRecord.ChartLegend);
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x000C9170 File Offset: 0x000C8170
		public void Delete()
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
			this.ᜆ.HasLegend = false;
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x000C91B8 File Offset: 0x000C81B8
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
			this.AutoPosition = true;
			this.AutoPositionX = true;
			this.AutoPositionY = true;
			this.LegendRecord.ᜁ(0);
			this.LegendRecord.ᜀ(0);
			this.LegendRecord.ᜂ(0);
			this.LegendRecord.ᜃ(0);
			this.PositionRecord.ᜃ(0);
			this.PositionRecord.ᜀ(0);
			this.PositionRecord.ᜂ(0);
			this.PositionRecord.ᜁ(0);
			this.ᜆ.ChartProperties.ᜀ(false);
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x000C927C File Offset: 0x000C827C
		private void ᜀ()
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
			this.AutoPosition = false;
			this.AutoPositionX = false;
			this.AutoPositionY = false;
			this.LegendRecord.ᜀ(LegendPositionType.NotDocked);
			this.ᜆ.ChartProperties.ᜀ(true);
		}

		// Token: 0x04000F1E RID: 3870
		private long \u2609\u009A\u00AE\u008B;

		// Token: 0x04000F1F RID: 3871
		private const int ᜀ = 5;

		// Token: 0x04000F20 RID: 3872
		private spr\u1A8D ᜁ;

		// Token: 0x04000F21 RID: 3873
		private int[] \u25D8ª\u00AE\u00A3;

		// Token: 0x04000F22 RID: 3874
		private int[] \u25D8\u009D\u0091\u0082;

		// Token: 0x04000F23 RID: 3875
		private spr\u23BE ᜂ;

		// Token: 0x04000F24 RID: 3876
		private XlsChartTextArea ᜃ;

		// Token: 0x04000F25 RID: 3877
		private XlsChartFrameFormat ᜄ;

		// Token: 0x04000F26 RID: 3878
		private bool ᜅ = true;

		// Token: 0x04000F27 RID: 3879
		private XlsChart ᜆ;

		// Token: 0x04000F28 RID: 3880
		private ChartLegendEntriesColl ᜇ;

		// Token: 0x04000F29 RID: 3881
		private Stream ᜈ;

		// Token: 0x04000F2A RID: 3882
		private string \u25D9\u0080\u0094\u009A;

		// Token: 0x04000F2B RID: 3883
		private ChartParagraphType ᜉ;

		// Token: 0x04000F2C RID: 3884
		private float[] \u2609\u00A3\u00A8\u007F;

		// Token: 0x04000F2D RID: 3885
		private sprᱬ ᜊ;
	}
}
