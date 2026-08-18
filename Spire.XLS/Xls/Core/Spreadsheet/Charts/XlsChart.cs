using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Spire.Xls.Charts;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Security;
using Spire.Xls.Core.Spreadsheet.XmlSerialization;

namespace Spire.Xls.Core.Spreadsheet.Charts
{
	// Token: 0x02000063 RID: 99
	public class XlsChart : XlsWorksheetBase, IChart, spr\u252A, spr\u1D46, ICloneParent
	{
		// Token: 0x060009FA RID: 2554 RVA: 0x000613E0 File Offset: 0x000603E0
		internal XlsChart(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.\u1717();
			this.ᝇ = (base.FindParent(typeof(XlsWorksheetBase), true) is XlsWorksheetBase);
			if (!this.m_book.Loading)
			{
				this.\u1716();
			}
			XlsChartFormat xlsChartFormat = new ChartFormat((spr\u2158)base.ReservedHandle, this.PrimaryFormats);
			this.PrimaryFormats.Add(xlsChartFormat, true);
			xlsChartFormat.ᜃ(ExcelChartType.ColumnClustered, false);
			if (!this.m_book.Loading)
			{
				this.HasLegend = true;
				this.PrimaryValueAxis.HasMajorGridLines = true;
				this.\u1755 = (spr\u2140)spr\u175E.ᜀ(TBIFFRecord.ChartShtprops);
				this.\u175F = new ChartWallOrFloor((spr\u2158)A_0, this, true);
				this.ᝠ = new ChartWallOrFloor((spr\u2158)A_0, this, false);
				this.ᝡ = new ChartPlotArea((spr\u2158)A_0, this, this.ChartType);
			}
			else if (this.m_book.Version != ExcelVersion.Version97to2003)
			{
				this.\u1755 = (spr\u2140)spr\u175E.ᜀ(TBIFFRecord.ChartShtprops);
			}
			this.ᜂ();
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00061550 File Offset: 0x00060550
		internal XlsChart(spr\u1DF5 A_0, object A_1, sprἛ A_2, ExcelParseOptions A_3, bool A_4, Dictionary<int, int> A_5, IDecryptor A_6) : base(A_0, A_1, A_2, A_3, A_4, A_5, A_6)
		{
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x000615B0 File Offset: 0x000605B0
		internal XlsChart(spr\u1DF5 A_0, object A_1, IList A_2, ref int A_3, ExcelParseOptions A_4) : this(A_0, A_1)
		{
			this.ᜎ.Clear();
			this.m_parseOptions = A_4;
			this.ᜀ(A_2, ref A_3, this.ᜎ, A_4);
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x000615EC File Offset: 0x000605EC
		private void \u1717()
		{
			int a_ = 12;
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
				this.m_book = (base.FindParent(typeof(XlsWorkbook)) as XlsWorkbook);
				if (this.m_book != null)
				{
					return;
				}
				break;
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("ቁ╃㑅ⵇ⑉㡋湍㽏け㹓㍕㭗⹙籛㵝şౡ੣॥ᱧ䩩๫୭偯ᑱ᭳͵ᙷṹ剻", a_));
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0006166C File Offset: 0x0006066C
		private void \u1716()
		{
			for (;;)
			{
				for (;;)
				{
					this.m_title = new ChartTextArea((spr\u2158)base.AppImplementation, this, ObjectTextLinkType.Chart);
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (this.m_book.InnerFonts.Count >= 506)
							{
								goto IL_C6;
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
								num = 4;
								continue;
							}
							break;
						case 1:
							if (this.m_book.Version == ExcelVersion.Version97to2003)
							{
								if (true)
								{
								}
								num = 3;
								continue;
							}
							goto IL_65;
						case 2:
							goto IL_79;
						case 3:
							num = 0;
							continue;
						case 4:
							goto IL_65;
						}
						break;
						IL_65:
						this.m_title.IsBold = true;
						num = 2;
					}
				}
			}
			IL_79:
			IL_C6:
			this.m_title.FrameFormat.Interior.UseDefaultFormat = true;
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00061758 File Offset: 0x00060758
		public override void Parse()
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
			this.KeepRecord = true;
			base.Parse();
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x000617A0 File Offset: 0x000607A0
		private new void ᜀ(IList A_0, ref int A_1, ExcelParseOptions A_2)
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
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x000617DC File Offset: 0x000607DC
		protected internal override void ParseData(Dictionary<int, int> dictUpdatedSSTIndexes)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					this.\u1715();
					int num = 7;
					for (;;)
					{
						Dictionary<int, int> dictionary;
						switch (num)
						{
						case 0:
							if (this.ᜠ == null)
							{
								num = 5;
								continue;
							}
							this.ᜠ.ᜁ(this);
							num = 22;
							continue;
						case 1:
						{
							BiffRecordRaw biffRecordRaw;
							if (biffRecordRaw.TypeCode == TBIFFRecord.BOF)
							{
								num = 19;
								continue;
							}
							num = 10;
							continue;
						}
						case 2:
						{
							bool flag = true;
							num = 14;
							continue;
						}
						case 3:
							goto IL_233;
						case 4:
							return;
						case 5:
						{
							this.ᝥ = new List<BiffRecordRaw>();
							int num2 = 0;
							bool flag = false;
							BiffRecordRaw biffRecordRaw = this.ᜎ[num2];
							int num3 = 0;
							dictionary = new Dictionary<int, int>();
							num = 18;
							continue;
						}
						case 6:
							goto IL_217;
						case 7:
							if ((this.m_parseOptions & ExcelParseOptions.DoNotParseCharts) != ExcelParseOptions.Default)
							{
								goto IL_8F;
							}
							num = 0;
							continue;
						case 8:
						{
							int num3;
							if (num3 == 1)
							{
								num = 16;
								continue;
							}
							int num2;
							num2++;
							num = 13;
							continue;
						}
						case 9:
						{
							int num2;
							if (num2 < this.ᜎ.Count)
							{
								num = 12;
								continue;
							}
							goto IL_217;
						}
						case 10:
						{
							BiffRecordRaw biffRecordRaw;
							if (biffRecordRaw.TypeCode == TBIFFRecord.EOF)
							{
								num = 21;
								continue;
							}
							num = 8;
							continue;
						}
						case 11:
							goto IL_231;
						case 12:
							num = 17;
							continue;
						case 13:
							goto IL_233;
						case 14:
							goto IL_233;
						case 15:
							goto IL_233;
						case 16:
						{
							BiffRecordRaw biffRecordRaw;
							int num2;
							this.ᜀ(biffRecordRaw, ref num2, dictionary);
							num = 15;
							continue;
						}
						case 17:
						{
							bool flag;
							if (flag)
							{
								num = 6;
								continue;
							}
							int num2;
							BiffRecordRaw biffRecordRaw = this.ᜎ[num2];
							num = 1;
							continue;
						}
						case 18:
							goto IL_233;
						case 19:
						{
							if (true)
							{
							}
							int num3;
							num3++;
							int num2;
							num2++;
							num = 3;
							continue;
						}
						case 20:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_8F;
							default:
							{
								if (false)
								{
								}
								int num3;
								if (num3 == 0)
								{
									num = 2;
									continue;
								}
								goto IL_233;
							}
							}
							break;
						case 21:
						{
							int num3;
							num3--;
							int num2;
							num2++;
							num = 20;
							continue;
						}
						case 22:
							goto IL_192;
						}
						break;
						IL_8F:
						num = 4;
						continue;
						IL_217:
						base.PrepareProtection();
						this.ᜀ(dictionary);
						num = 11;
						continue;
						IL_233:
						num = 9;
					}
				}
				return;
				IL_192:
				IL_231:
				base.IsParsed = true;
				return;
			}
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x00061AA0 File Offset: 0x00060AA0
		private new void ᜀ(Dictionary<int, int> A_0)
		{
			for (;;)
			{
				IL_00:
				switch (0)
				{
				default:
				{
					int num = 2;
					for (;;)
					{
						if (true)
						{
						}
						Dictionary<int, List<BiffRecordRaw>>.Enumerator enumerator;
						switch (num)
						{
						case 0:
							num = 1;
							continue;
						case 1:
							if (this.ᝫ.Count > 0)
							{
								num = 3;
								continue;
							}
							goto IL_1A0;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								break;
							}
							break;
						case 3:
							goto IL_150;
						case 4:
							try
							{
								num = 7;
								for (;;)
								{
									int num2;
									KeyValuePair<int, List<BiffRecordRaw>> keyValuePair;
									switch (num)
									{
									case 0:
										num = 3;
										continue;
									case 1:
										num2 = A_0[num2];
										num = 6;
										continue;
									case 2:
										if (!enumerator.MoveNext())
										{
											num = 0;
											continue;
										}
										keyValuePair = enumerator.Current;
										num2 = keyValuePair.Key;
										num = 5;
										continue;
									case 3:
										goto IL_140;
									case 5:
										if (A_0.ContainsKey(num2))
										{
											num = 1;
											continue;
										}
										goto IL_EF;
									case 6:
										goto IL_EF;
									}
									IL_D2:
									num = 2;
									continue;
									goto IL_D2;
									IL_EF:
									List<BiffRecordRaw> value = keyValuePair.Value;
									XlsChartSerie xlsChartSerie = (XlsChartSerie)this.\u1753[num2];
									xlsChartSerie.ParseErrorBars(value);
									num = 4;
								}
								IL_140:
								goto IL_1A0;
							}
							finally
							{
								((IDisposable)enumerator).Dispose();
							}
							goto IL_150;
						}
						if (this.ᝫ != null)
						{
							num = 0;
							continue;
						}
						goto IL_1A0;
						IL_150:
						enumerator = this.ᝫ.GetEnumerator();
						num = 4;
					}
					break;
				}
				}
			}
			IL_1A0:
			this.ᝫ = null;
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00061C64 File Offset: 0x00060C64
		private new void ᜀ(BiffRecordRaw A_0, ref int A_1, Dictionary<int, int> A_2)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					TBIFFRecord typeCode = A_0.TypeCode;
					int num = 25;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 20;
							continue;
						case 1:
							num = 31;
							continue;
						case 2:
							num = 23;
							continue;
						case 3:
							num = 30;
							continue;
						case 4:
							goto IL_31D;
						case 5:
							if (typeCode != TBIFFRecord.ChartSiIndex)
							{
								num = 26;
								continue;
							}
							goto IL_303;
						case 6:
							if (typeCode != TBIFFRecord.ObjectProtect)
							{
								num = 28;
								continue;
							}
							goto IL_491;
						case 7:
							num = 4;
							continue;
						case 8:
							num = 11;
							continue;
						case 9:
							num = 24;
							continue;
						case 10:
							num = 16;
							continue;
						case 11:
							switch (typeCode)
							{
							case (TBIFFRecord)2136:
							case (TBIFFRecord)2137:
								goto IL_187;
							default:
								num = 7;
								continue;
							}
							break;
						case 12:
							num = 13;
							continue;
						case 13:
							if (typeCode != TBIFFRecord.Dimensions)
							{
								num = 32;
								continue;
							}
							goto IL_420;
						case 14:
							if (typeCode != TBIFFRecord.ChartChart)
							{
								num = 1;
								continue;
							}
							goto IL_D1;
						case 15:
							if (typeCode != TBIFFRecord.ScenProtect)
							{
								num = 3;
								continue;
							}
							goto IL_218;
						case 16:
							if (typeCode != (TBIFFRecord)2128)
							{
								num = 8;
								continue;
							}
							goto IL_187;
						case 17:
							if (true)
							{
							}
							num = 5;
							continue;
						case 18:
							goto IL_EC;
						case 19:
							num = 6;
							continue;
						case 20:
							switch (typeCode)
							{
							case TBIFFRecord.Protect:
								goto IL_205;
							case TBIFFRecord.Password:
								goto IL_19A;
							case TBIFFRecord.Header:
								goto IL_472;
							default:
								num = 19;
								continue;
							}
							break;
						case 21:
							if (typeCode <= (TBIFFRecord)2137)
							{
								num = 29;
								continue;
							}
							num = 14;
							continue;
						case 22:
							if (typeCode != TBIFFRecord.WindowZoom)
							{
								num = 2;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_C0;
							default:
								goto IL_12F;
							}
							break;
						case 23:
							goto IL_2B5;
						case 24:
							if (typeCode <= TBIFFRecord.WindowZoom)
							{
								num = 0;
								continue;
							}
							num = 15;
							continue;
						case 25:
							if (typeCode <= TBIFFRecord.Dimensions)
							{
								goto IL_C0;
							}
							num = 21;
							continue;
						case 26:
							num = 18;
							continue;
						case 27:
							if (typeCode != TBIFFRecord.WindowTwo)
							{
								num = 10;
								continue;
							}
							goto IL_3A2;
						case 28:
							num = 22;
							continue;
						case 29:
							num = 27;
							continue;
						case 30:
							if (typeCode != TBIFFRecord.CodeName)
							{
								num = 12;
								continue;
							}
							goto IL_2DF;
						case 31:
							if (typeCode != TBIFFRecord.ChartFbi)
							{
								num = 17;
								continue;
							}
							goto IL_34A;
						case 32:
							num = 33;
							continue;
						case 33:
							goto IL_3CE;
						}
						break;
						IL_C0:
						num = 9;
					}
				}
				IL_D1:
				this.ᜁ(this.ᜎ, ref A_1, A_2);
				return;
				IL_EC:
				goto IL_4A4;
				IL_12F:
				if (false)
				{
				}
				this.ParseWindowZoom((spr\u1CF7)this.ᜎ[A_1++]);
				return;
				IL_187:
				this.ᝥ.Add(A_0);
				A_1++;
				return;
				IL_19A:
				base.ᜀ((spr\u24C3)A_0);
				A_1++;
				return;
				IL_205:
				base.ᜀ((spr\u1AE8)A_0);
				A_1++;
				return;
				IL_218:
				base.ᜀ((sprℷ)A_0);
				A_1++;
				return;
				IL_2B5:
				goto IL_4A4;
				IL_2DF:
				this.m_strCodeName = ((spr\u2384)this.ᜎ[A_1]).ᜀ();
				A_1++;
				return;
				IL_303:
				this.ᜉ(this.ᜎ, ref A_1);
				return;
				IL_31D:
				goto IL_4A4;
				IL_34A:
				this.ᜀ(this.ᜎ, ref A_1);
				return;
				IL_3A2:
				this.ParseWindowTwo((sprṫ)this.ᜎ[A_1++]);
				return;
				IL_3CE:
				goto IL_4A4;
				IL_420:
				this.ParseDimensions((spr\u203C)this.ᜎ[A_1++]);
				return;
				IL_472:
				this.ᝍ = new ChartPageSetup((spr\u2158)base.ReservedHandle, this, this.ᜎ, ref A_1);
				return;
				IL_491:
				base.ᜀ((spr\u17CF)A_0);
				A_1++;
				return;
				IL_4A4:
				A_1++;
				return;
			}
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0006211C File Offset: 0x0006111C
		private new void ᜀ(IList A_0, ref int A_1, List<BiffRecordRaw> A_2, ExcelParseOptions A_3)
		{
			int a_ = 5;
			switch (0)
			{
			default:
			{
				int num = 5;
				for (;;)
				{
					BiffRecordRaw biffRecordRaw;
					switch (num)
					{
					case 0:
					{
						bool flag;
						if (flag)
						{
							num = 20;
							continue;
						}
						goto IL_33F;
					}
					case 1:
						if (A_1 >= 0)
						{
							num = 13;
							continue;
						}
						goto IL_E8;
					case 2:
						goto IL_33D;
					case 3:
						goto IL_184;
					case 4:
					{
						if (biffRecordRaw.TypeCode != TBIFFRecord.BOF)
						{
							num = 14;
							continue;
						}
						A_2.Add(biffRecordRaw);
						A_1++;
						biffRecordRaw = (BiffRecordRaw)A_0[A_1];
						bool flag = false;
						goto IL_29C;
					}
					case 6:
					{
						bool flag;
						if (flag)
						{
							num = 15;
							continue;
						}
						goto IL_243;
					}
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_29C;
						default:
							if (false)
							{
							}
							goto IL_243;
						}
						break;
					case 8:
					{
						XlsWorkbook xlsWorkbook;
						if (xlsWorkbook == null)
						{
							num = 2;
							continue;
						}
						XlsFont xlsFont = (XlsFont)xlsWorkbook.InnerFonts[0];
						xlsFont.Record;
						num = 7;
						continue;
					}
					case 9:
						goto IL_AB;
					case 10:
						if (A_1 >= A_0.Count)
						{
							num = 23;
							continue;
						}
						biffRecordRaw = (BiffRecordRaw)A_0[A_1];
						num = 4;
						continue;
					case 11:
						this.m_iMsoStartIndex = A_2.Count - 1;
						num = 3;
						continue;
					case 12:
						if (biffRecordRaw.TypeCode == TBIFFRecord.MSODrawing)
						{
							num = 16;
							continue;
						}
						goto IL_184;
					case 13:
						num = 10;
						continue;
					case 14:
						goto IL_21F;
					case 15:
					{
						XlsWorkbook xlsWorkbook = base.FindParent(typeof(XlsWorkbook)) as XlsWorkbook;
						num = 8;
						continue;
					}
					case 16:
						num = 18;
						continue;
					case 17:
						goto IL_27A;
					case 18:
						if (this.m_iMsoStartIndex < 0)
						{
							num = 11;
							continue;
						}
						goto IL_184;
					case 19:
						if (biffRecordRaw.TypeCode != TBIFFRecord.ChartFbi)
						{
							num = 25;
							continue;
						}
						goto IL_33F;
					case 20:
						num = 19;
						continue;
					case 21:
						goto IL_33F;
					case 22:
						goto IL_243;
					case 23:
						goto IL_155;
					case 24:
					{
						bool flag;
						if (flag)
						{
							num = 27;
							continue;
						}
						goto IL_B0;
					}
					case 25:
						goto IL_B0;
					case 26:
						if (true)
						{
						}
						if (biffRecordRaw.TypeCode == TBIFFRecord.EOF)
						{
							num = 17;
							continue;
						}
						num = 24;
						continue;
					case 27:
						num = 0;
						continue;
					}
					if (A_0 == null)
					{
						num = 9;
						continue;
					}
					num = 1;
					continue;
					IL_B0:
					A_2.Add(biffRecordRaw);
					num = 21;
					continue;
					IL_184:
					A_1++;
					biffRecordRaw = (BiffRecordRaw)A_0[A_1];
					num = 22;
					continue;
					IL_243:
					num = 26;
					continue;
					IL_29C:
					num = 6;
					continue;
					IL_33F:
					num = 12;
				}
				IL_AB:
				throw new ArgumentNullException(RecordTableEnumerator.b("强尼䬾⁀", a_));
				IL_E8:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䬺刼䰾", a_), RecordTableEnumerator.b("欺刼䰾⡀㝂ⱄ⡆❈歊⅌⩎≐⁒畔⍖ㅘ㩚㍜罞兠䍢੤ᕦ䥨౪Ὤ੮ၰݲၴն奸ེᕼṾꎂﲈ歷ꆎ", a_));
				IL_155:
				goto IL_E8;
				IL_21F:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("渺匼娾㥀㍂⁄⑆㵈⹊⥌潎㍐㱒㍔睖⭘㹚㹜ぞ፠ݢ䭤䥦", a_));
				IL_27A:
				A_2.Add((BiffRecordRaw)A_0[A_1]);
				A_1++;
				return;
				IL_33D:
				throw new ArgumentNullException(RecordTableEnumerator.b("欺尼䴾⑀ⵂㅄ杆♈⥊❌⩎㉐❒畔㑖㡘㕚㍜ぞᕠ䍢ݤɦ䥨൪ɬᩮὰᝲ孴", a_));
			}
			}
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x000624E4 File Offset: 0x000614E4
		private new void ᜀ(IList A_0, ref int A_1)
		{
			int a_ = 16;
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
			for (;;)
			{
				if (true)
				{
				}
				BiffRecordRaw biffRecordRaw = (BiffRecordRaw)A_0[A_1];
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_7E;
					case 1:
						if (biffRecordRaw.TypeCode != TBIFFRecord.ChartFbi)
						{
							num = 2;
							continue;
						}
						this.\u1752.Add((spr\u1F17)biffRecordRaw);
						A_1++;
						biffRecordRaw = (BiffRecordRaw)A_0[A_1];
						num = 4;
						continue;
					case 2:
						return;
					case 3:
						if (biffRecordRaw.TypeCode != TBIFFRecord.ChartFbi)
						{
							num = 0;
							continue;
						}
						goto IL_80;
					case 4:
						goto IL_80;
					}
					break;
					IL_80:
					num = 1;
				}
			}
			IL_7E:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("Յ⁇⭉㹋㩍ᙏけ㵓癕⩗㽙㽛ㅝ቟١䑣ե१ѩɫŭѯ剱ᙳ፵塷ᱹ፻୽ꪃ", a_));
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x000625D8 File Offset: 0x000615D8
		private void ᜁ(IList<BiffRecordRaw> A_0, ref int A_1, Dictionary<int, int> A_2)
		{
			int a_ = 18;
			switch (0)
			{
			default:
			{
				int num = 32;
				for (;;)
				{
					BiffRecordRaw biffRecordRaw;
					int num2;
					int num3;
					List<XlsChartTextArea> list;
					List<XlsChartTextArea> list2;
					int num4;
					int num5;
					switch (num)
					{
					case 0:
						num = 36;
						continue;
					case 1:
						num = 33;
						continue;
					case 2:
					{
						TBIFFRecord typeCode;
						if (typeCode != TBIFFRecord.ChartWrapper)
						{
							num = 5;
							continue;
						}
						spr\u23F0 spr_u23F = (spr\u23F0)biffRecordRaw;
						num = 27;
						continue;
					}
					case 3:
						num = 34;
						continue;
					case 4:
					{
						TBIFFRecord typeCode;
						if (typeCode != TBIFFRecord.ChartSeries)
						{
							num = 1;
							continue;
						}
						this.ᜀ(A_0, ref A_1, A_2);
						num = 13;
						continue;
					}
					case 5:
						num = 38;
						continue;
					case 6:
						goto IL_5DF;
					case 7:
						goto IL_5DF;
					case 8:
					{
						TBIFFRecord typeCode;
						if (typeCode <= TBIFFRecord.ChartText)
						{
							num = 58;
							continue;
						}
						num = 53;
						continue;
					}
					case 9:
					{
						if (num2 >= num3)
						{
							num = 47;
							continue;
						}
						XlsChartTextArea area = list[num2];
						this.\u1753.AssignTrendDataLabel(area);
						num2++;
						num = 22;
						continue;
					}
					case 10:
						goto IL_5DF;
					case 11:
						goto IL_5DF;
					case 12:
						if (this.\u1755 == null)
						{
							num = 43;
							continue;
						}
						goto IL_269;
					case 13:
						goto IL_5DF;
					case 14:
						num = 2;
						continue;
					case 15:
						goto IL_465;
					case 16:
						goto IL_5DF;
					case 17:
						goto IL_14A;
					case 18:
						goto IL_702;
					case 19:
						goto IL_7C5;
					case 20:
						goto IL_5DF;
					case 21:
						goto IL_5DF;
					case 22:
						goto IL_7C5;
					case 23:
						goto IL_5DF;
					case 24:
						biffRecordRaw = A_0[A_1];
						num = 15;
						continue;
					case 25:
						list = list2;
						num = 48;
						continue;
					case 26:
						num = 57;
						continue;
					case 27:
					{
						spr\u23F0 spr_u23F;
						if (spr_u23F.ᜀ().TypeCode == TBIFFRecord.ChartText)
						{
							num = 46;
							continue;
						}
						A_1++;
						num = 10;
						continue;
					}
					case 28:
						goto IL_7E7;
					case 29:
						num = 63;
						continue;
					case 30:
						num = 62;
						continue;
					case 31:
						goto IL_5DF;
					case 33:
					{
						TBIFFRecord typeCode;
						switch (typeCode)
						{
						case TBIFFRecord.ChartDefaultText:
							this.ᜇ(A_0, ref A_1);
							num = 31;
							continue;
						case TBIFFRecord.ChartText:
							goto IL_5AA;
						default:
							num = 0;
							continue;
						}
						break;
					}
					case 34:
					{
						TBIFFRecord typeCode;
						switch (typeCode)
						{
						case TBIFFRecord.ChartDat:
							this.ᜂ(A_0, ref A_1);
							num = 55;
							continue;
						case TBIFFRecord.ChartPlotGrowth:
							this.ᜀ((sprᥦ)A_0[A_1++]);
							num = 6;
							continue;
						default:
							num = 59;
							continue;
						}
						break;
					}
					case 35:
						num4 = list.Count;
						goto IL_6A7;
					case 36:
						goto IL_7E7;
					case 37:
						goto IL_5DF;
					case 38:
						if (true)
						{
						}
						goto IL_7E7;
					case 39:
						if (list == null)
						{
							num = 64;
							continue;
						}
						num = 35;
						continue;
					case 40:
						goto IL_269;
					case 41:
						goto IL_5DF;
					case 42:
					{
						TBIFFRecord typeCode;
						switch (typeCode)
						{
						case TBIFFRecord.ChartShtprops:
							this.ᜈ(A_0, ref A_1);
							num = 44;
							continue;
						case TBIFFRecord.ChartSertocrt:
							goto IL_7E7;
						case TBIFFRecord.ChartAxesUsed:
							this.ᜄ(A_0, ref A_1);
							num = 21;
							continue;
						default:
							num = 3;
							continue;
						}
						break;
					}
					case 43:
						this.\u1755 = (spr\u2140)spr\u175E.ᜀ(TBIFFRecord.ChartShtprops);
						num = 40;
						continue;
					case 44:
						goto IL_5DF;
					case 45:
						if (biffRecordRaw.TypeCode == TBIFFRecord.End)
						{
							num = 30;
							continue;
						}
						goto IL_65A;
					case 46:
					{
						XlsChartWrappedTextArea a_2 = new XlsChartWrappedTextArea(base.ReservedHandle, this, A_0, ref A_1);
						this.ᜀ(a_2, A_2);
						num = 41;
						continue;
					}
					case 47:
						return;
					case 48:
						goto IL_5DF;
					case 49:
						goto IL_5DF;
					case 50:
						goto IL_465;
					case 51:
						if (A_1 != num3)
						{
							num = 24;
							continue;
						}
						goto IL_702;
					case 52:
						goto IL_7E7;
					case 53:
					{
						TBIFFRecord typeCode;
						if (typeCode <= TBIFFRecord.ChartAxisParent)
						{
							num = 29;
							continue;
						}
						num = 42;
						continue;
					}
					case 54:
					{
						TBIFFRecord typeCode;
						if (typeCode <= TBIFFRecord.ChartWrapper)
						{
							num = 26;
							continue;
						}
						num = 4;
						continue;
					}
					case 55:
						goto IL_5DF;
					case 56:
						num = 61;
						continue;
					case 57:
					{
						TBIFFRecord typeCode;
						if (typeCode != TBIFFRecord.WindowZoom)
						{
							num = 14;
							continue;
						}
						this.ᝦ = (spr\u1CF7)biffRecordRaw;
						A_1++;
						num = 49;
						continue;
					}
					case 58:
						num = 54;
						continue;
					case 59:
						num = 28;
						continue;
					case 60:
						num = 52;
						continue;
					case 61:
					{
						TBIFFRecord typeCode;
						if (typeCode != TBIFFRecord.ChartAxisParent)
						{
							num = 60;
							continue;
						}
						this.ᜃ(A_0, ref A_1);
						num = 23;
						continue;
					}
					case 62:
						if (num5 == 0)
						{
							num = 18;
							continue;
						}
						goto IL_65A;
					case 63:
					{
						TBIFFRecord typeCode;
						switch (typeCode)
						{
						case TBIFFRecord.ChartFrame:
							this.InnerXlsChartArea.ᜀ(A_0, ref A_1);
							num = 37;
							continue;
						case TBIFFRecord.Begin:
							num5++;
							A_1++;
							num = 11;
							continue;
						case TBIFFRecord.End:
							num5--;
							A_1++;
							num = 16;
							continue;
						default:
							num = 56;
							continue;
						}
						break;
					}
					case 64:
						num = 65;
						continue;
					case 65:
						num4 = 0;
						goto IL_6A7;
					case 66:
						if (list == null)
						{
							num = 25;
							continue;
						}
						list.AddRange(list2);
						num = 20;
						continue;
					}
					if (A_0 == null)
					{
						num = 17;
						continue;
					}
					biffRecordRaw = A_0[A_1];
					biffRecordRaw.CheckTypeCode(TBIFFRecord.ChartChart);
					this.ᜀ((sprὬ)biffRecordRaw);
					A_1++;
					biffRecordRaw = A_0[A_1];
					biffRecordRaw.CheckTypeCode(TBIFFRecord.Begin);
					A_1++;
					num5 = 0;
					num3 = A_0.Count;
					this.\u1753.TrendIndex = 0;
					biffRecordRaw = A_0[A_1];
					list = null;
					num = 50;
					continue;
					IL_269:
					this.UpdateChartTitle();
					this.DetectIsInRowOnParsing();
					this.ChangePrimaryAxis(true);
					this.DetectChartType();
					this.\u1714();
					num = 39;
					continue;
					IL_465:
					num = 45;
					continue;
					IL_5AA:
					XlsChartTextArea a_3 = this.ᜅ(A_0, ref A_1);
					list2 = this.ᜀ(a_3, A_2);
					num = 66;
					continue;
					IL_65A:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5AA;
					default:
					{
						if (false)
						{
						}
						TBIFFRecord typeCode = biffRecordRaw.TypeCode;
						num = 8;
						continue;
					}
					}
					IL_5DF:
					num = 51;
					continue;
					IL_6A7:
					num3 = num4;
					num2 = 0;
					num = 19;
					continue;
					IL_702:
					A_1++;
					num = 12;
					continue;
					IL_7C5:
					num = 9;
					continue;
					IL_7E7:
					A_1++;
					num = 7;
				}
				IL_14A:
				throw new ArgumentNullException(RecordTableEnumerator.b("ⱇ⭉㡋⽍", a_));
			}
			}
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00062DFC File Offset: 0x00061DFC
		private new void ᜀ(sprὬ A_0)
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
			this.XPos = XlsChart.ᜀ(A_0.ᜅ());
			this.YPos = XlsChart.ᜀ(A_0.ᜄ());
			this.Width = XlsChart.ᜀ(A_0.ᜃ());
			this.Height = XlsChart.ᜀ(A_0.ᜀ());
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00062E7C File Offset: 0x00061E7C
		private new void ᜀ(sprᥦ A_0)
		{
			int a_ = 8;
			while (A_0 == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("丽ⰿⵁぃŅ㩇╉㭋㩍㡏", a_));
				}
			}
			this.\u1756 = A_0;
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00062EE0 File Offset: 0x00061EE0
		private void ᜉ(IList<BiffRecordRaw> A_0, ref int A_1)
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
			this.\u1753.ᜀ(A_0, ref A_1);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00062F28 File Offset: 0x00061F28
		private new void ᜀ(IList<BiffRecordRaw> A_0, ref int A_1, Dictionary<int, int> A_2)
		{
			int a_ = 5;
			switch (0)
			{
			default:
			{
				int num = 0;
				List<BiffRecordRaw> list;
				int num3;
				XlsChartSerie xlsChartSerie;
				for (;;)
				{
					int num2;
					int count;
					bool flag;
					switch (num)
					{
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1CB;
						default:
							goto IL_B4;
						}
						break;
					case 2:
						if (num2 >= count)
						{
							num = 8;
							continue;
						}
						this.ᝤ.Add(list[num2]);
						num2++;
						num = 17;
						continue;
					case 3:
						goto IL_155;
					case 4:
						if (this.ᜀ(A_0, list, ref A_1, ref num3, ref flag))
						{
							num = 3;
							continue;
						}
						num = 10;
						continue;
					case 5:
						num3 = A_2[num3];
						num = 15;
						continue;
					case 6:
						goto IL_99;
					case 7:
						if (this.ᝫ == null)
						{
							num = 9;
							continue;
						}
						goto IL_15A;
					case 8:
						return;
					case 9:
						this.ᝫ = new Dictionary<int, List<BiffRecordRaw>>();
						num = 6;
						continue;
					case 10:
						goto IL_1CB;
					case 11:
						goto IL_80;
					case 12:
						num = 13;
						continue;
					case 13:
						if (A_2.ContainsKey(num3))
						{
							num = 5;
							continue;
						}
						goto IL_18B;
					case 14:
						if (xlsChartSerie != null)
						{
							num = 1;
							continue;
						}
						num = 7;
						continue;
					case 15:
						goto IL_18B;
					case 16:
						goto IL_168;
					case 17:
						goto IL_168;
					}
					if (A_0 == null)
					{
						num = 11;
						continue;
					}
					list = new List<BiffRecordRaw>();
					num3 = 0;
					flag = false;
					num = 4;
					continue;
					IL_1CB:
					if (flag)
					{
						num = 12;
						continue;
					}
					num2 = 0;
					count = list.Count;
					if (true)
					{
					}
					num = 16;
					continue;
					IL_168:
					num = 2;
					continue;
					IL_18B:
					xlsChartSerie = (XlsChartSerie)this.\u1753[num3];
					num = 14;
				}
				IL_80:
				throw new ArgumentNullException(RecordTableEnumerator.b("强尼䬾⁀", a_));
				IL_99:
				goto IL_15A;
				IL_B4:
				if (false)
				{
				}
				xlsChartSerie.ParseErrorBars(list);
				return;
				IL_155:
				int num4 = 0;
				ChartSerie serieToAdd = new ChartSerie((spr\u2158)base.ReservedHandle, this.\u1753, list, ref num4);
				this.\u1753.Add(serieToAdd);
				A_2[num3] = this.\u1753.Count - 1;
				return;
				IL_15A:
				this.ᝫ.Add(num3, list);
				return;
			}
			}
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x000631C4 File Offset: 0x000621C4
		private void ᜈ(IList<BiffRecordRaw> A_0, ref int A_1)
		{
			int a_ = 19;
			while (A_0 == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("ⵈ⩊㥌⹎", a_));
				}
			}
			BiffRecordRaw biffRecordRaw = A_0[A_1];
			biffRecordRaw.CheckTypeCode(TBIFFRecord.ChartShtprops);
			this.\u1755 = (spr\u2140)biffRecordRaw.Clone();
			A_1++;
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0006324C File Offset: 0x0006224C
		private void ᜇ(IList<BiffRecordRaw> A_0, ref int A_1)
		{
			int a_ = 11;
			for (;;)
			{
				if (true)
				{
				}
				if (A_0 != null)
				{
					goto IL_50;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_2C;
				}
			}
			IL_2C:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("╀≂ㅄ♆", a_));
			IL_50:
			BiffRecordRaw biffRecordRaw = A_0[A_1];
			biffRecordRaw.CheckTypeCode(TBIFFRecord.ChartDefaultText);
			spr\u2350 spr_u = (spr\u2350)biffRecordRaw.Clone();
			int key = (int)spr_u.ᜀ();
			A_1++;
			List<BiffRecordRaw> value = this.ᜆ(A_0, ref A_1);
			this.\u175A[key] = value;
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x000632EC File Offset: 0x000622EC
		private List<BiffRecordRaw> ᜆ(IList<BiffRecordRaw> A_0, ref int A_1)
		{
			int a_ = 0;
			int num = 6;
			List<BiffRecordRaw> list;
			for (;;)
			{
				BiffRecordRaw biffRecordRaw;
				switch (num)
				{
				case 0:
					if (A_1 > A_0.Count)
					{
						num = 7;
						continue;
					}
					biffRecordRaw = A_0[A_1];
					biffRecordRaw.CheckTypeCode(TBIFFRecord.ChartText);
					list = new List<BiffRecordRaw>();
					list.Add(biffRecordRaw);
					A_1++;
					biffRecordRaw = A_0[A_1];
					biffRecordRaw.CheckTypeCode(TBIFFRecord.Begin);
					list.Add(biffRecordRaw);
					num = 1;
					continue;
				case 1:
					goto IL_B6;
				case 2:
					if (biffRecordRaw.TypeCode == TBIFFRecord.End)
					{
						num = 5;
						continue;
					}
					goto IL_F1;
				case 3:
					if (A_1 >= 0)
					{
						num = 4;
						continue;
					}
					goto IL_15E;
				case 4:
					num = 0;
					continue;
				case 5:
					goto IL_12A;
				case 7:
					goto IL_D8;
				case 8:
					goto IL_67;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_B6:
					break;
				default:
					if (false)
					{
					}
					if (A_0 == null)
					{
						num = 8;
						continue;
					}
					if (true)
					{
					}
					num = 3;
					continue;
				}
				IL_F1:
				A_1++;
				biffRecordRaw = A_0[A_1];
				list.Add(biffRecordRaw);
				num = 2;
			}
			IL_67:
			throw new ArgumentNullException(RecordTableEnumerator.b("刵夷丹崻", a_));
			IL_D8:
			goto IL_15E;
			IL_12A:
			A_1++;
			return list;
			IL_15E:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䘵圷䤹", a_), RecordTableEnumerator.b("昵圷䤹᰻刽┿ㅁ㝃晅㱇≉ⵋ⁍灏扑瑓㥕⩗穙㭛ⱝ՟͡ၣͥᩧ䩩ᡫ٭ᅯᱱ味᩵ᵷᑹ᭻੽", a_));
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00063480 File Offset: 0x00062480
		private new XlsChartTextArea ᜅ(IList<BiffRecordRaw> A_0, ref int A_1)
		{
			int a_ = 12;
			if (A_0 == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("♁╃㉅⥇", a_));
				}
			}
			BiffRecordRaw biffRecordRaw = A_0[A_1];
			biffRecordRaw.CheckTypeCode(TBIFFRecord.ChartText);
			return new ChartTextArea((spr\u2158)base.ReservedHandle, this, A_0, ref A_1);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00063504 File Offset: 0x00062504
		private new void ᜄ(IList<BiffRecordRaw> A_0, ref int A_1)
		{
			int a_ = 5;
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
					throw new ArgumentNullException(RecordTableEnumerator.b("强尼䬾⁀", a_));
				}
			}
			if (true)
			{
			}
			BiffRecordRaw biffRecordRaw = A_0[A_1];
			biffRecordRaw.CheckTypeCode(TBIFFRecord.ChartAxesUsed);
			this.\u175B.ᜆ().PrimaryFormats.Clear();
			this.\u175B.ᜆ().SecondaryFormats.Clear();
			A_1++;
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x000635A8 File Offset: 0x000625A8
		private new void ᜃ(IList<BiffRecordRaw> A_0, ref int A_1)
		{
			int a_ = 2;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
				{
					ushort num2;
					switch (num2)
					{
					case 0:
						goto IL_68;
					case 1:
						goto IL_94;
					default:
						num = 2;
						continue;
					}
					break;
				}
				case 2:
					num = 3;
					continue;
				case 3:
					goto IL_7E;
				case 4:
					goto IL_5C;
				}
				if (true)
				{
				}
				if (A_0 == null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7E;
					default:
						if (false)
						{
						}
						num = 4;
						break;
					}
				}
				else
				{
					BiffRecordRaw biffRecordRaw = A_0[A_1];
					biffRecordRaw.CheckTypeCode(TBIFFRecord.ChartAxisParent);
					sprᶓ sprᶓ = (sprᶓ)biffRecordRaw.Clone();
					ushort num2 = sprᶓ.ᜃ();
					num = 1;
				}
			}
			IL_5C:
			throw new ArgumentNullException(RecordTableEnumerator.b("尷嬹䠻弽", a_));
			IL_68:
			this.\u175B.ᜃ(A_0, ref A_1);
			return;
			IL_7E:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("礷䈹夻䴽怿⭁⩃≅ⵇ㉉汋⍍╏⅑⁓癕㩗㽙籛湝䁟ൡᙣ䙥奧䑩", a_));
			IL_94:
			this.\u175C.ᜃ(A_0, ref A_1);
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x000636B8 File Offset: 0x000626B8
		private new void ᜂ(IList<BiffRecordRaw> A_0, ref int A_1)
		{
			int a_ = 12;
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
					throw new ArgumentNullException(RecordTableEnumerator.b("♁╃㉅⥇", a_));
				}
			}
			BiffRecordRaw biffRecordRaw = A_0[A_1];
			biffRecordRaw.CheckTypeCode(TBIFFRecord.ChartDat);
			this.ᝌ = true;
			this.\u1754 = new ChartDataTableXls(base.ReservedHandle, this, A_0, ref A_1);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00063744 File Offset: 0x00062744
		private new List<XlsChartTextArea> ᜀ(XlsChartTextArea A_0, Dictionary<int, int> A_1)
		{
			int a_ = 18;
			switch (0)
			{
			default:
			{
				int num = 4;
				List<XlsChartTextArea> list;
				for (;;)
				{
					ObjectTextLinkType objectTextLinkType;
					spr\u20F4 spr_u20F;
					switch (num)
					{
					case 0:
						return list;
					case 1:
						list = new List<XlsChartTextArea>();
						num = 6;
						continue;
					case 2:
					{
						int num2 = A_1[num2];
						num = 16;
						continue;
					}
					case 3:
						if (list == null)
						{
							num = 1;
							continue;
						}
						goto IL_216;
					case 5:
						goto IL_8C;
					case 6:
						goto IL_216;
					case 7:
					{
						int num2;
						if (num2 >= this.\u1753.Count)
						{
							num = 18;
							continue;
						}
						XlsChartSerie xlsChartSerie = (XlsChartSerie)this.\u1753[num2];
						int index;
						XlsChartDataPoint xlsChartDataPoint = (XlsChartDataPoint)xlsChartSerie.DataPoints[index];
						xlsChartDataPoint.SetDataLabels(A_0);
						num = 8;
						continue;
					}
					case 8:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_159;
						default:
							goto IL_1DA;
						}
						break;
					case 9:
						return list;
					case 10:
						return list;
					case 11:
						goto IL_D4;
					case 12:
						switch (objectTextLinkType)
						{
						case ObjectTextLinkType.Chart:
							this.m_title = A_0;
							num = 9;
							continue;
						case ObjectTextLinkType.YAxis:
							this.ValueAxisTitle = A_0.Text;
							num = 0;
							continue;
						case ObjectTextLinkType.XAxis:
							this.CategoryAxisTitle = A_0.Text;
							num = 19;
							continue;
						case ObjectTextLinkType.DataLabel:
						{
							int num2 = (int)spr_u20F.ᜁ();
							int index = (int)spr_u20F.ᜄ();
							num = 14;
							continue;
						}
						case (ObjectTextLinkType)5:
						case (ObjectTextLinkType)6:
							return list;
						case ObjectTextLinkType.ZAxis:
							this.SeriesAxisTitle = A_0.Text;
							num = 11;
							continue;
						default:
							num = 13;
							continue;
						}
						break;
					case 13:
						if (true)
						{
						}
						num = 15;
						continue;
					case 14:
					{
						int num2;
						if (A_1.ContainsKey(num2))
						{
							num = 2;
							continue;
						}
						goto IL_91;
					}
					case 15:
						return list;
					case 16:
						goto IL_91;
					case 17:
						goto IL_159;
					case 18:
						num = 3;
						continue;
					case 19:
						return list;
					case 20:
						goto IL_16B;
					}
					if (A_0 == null)
					{
						num = 5;
						continue;
					}
					spr_u20F = A_0.ObjectLink;
					num = 17;
					continue;
					IL_91:
					num = 7;
					continue;
					IL_159:
					if (spr_u20F == null)
					{
						num = 20;
						continue;
					}
					list = null;
					objectTextLinkType = spr_u20F.ᜃ();
					num = 12;
					continue;
					IL_216:
					list.Add(A_0);
					num = 10;
				}
				IL_8C:
				throw new ArgumentNullException(RecordTableEnumerator.b("㱇⽉㑋㩍ᅏ⁑ㅓ㝕", a_));
				IL_D4:
				return list;
				IL_16B:
				throw new ArgumentNullException(RecordTableEnumerator.b("❇⡉♋⭍㍏♑ᡓ㽕㙗ㅙ", a_));
				IL_1DA:
				if (false)
				{
				}
				return list;
			}
			}
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00063A3C File Offset: 0x00062A3C
		private void ᜁ(IList<BiffRecordRaw> A_0, ref int A_1)
		{
			int a_ = 12;
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
					throw new ArgumentNullException(RecordTableEnumerator.b("♁╃㉅⥇", a_));
				}
			}
			BiffRecordRaw biffRecordRaw = A_0[A_1];
			biffRecordRaw.CheckTypeCode(TBIFFRecord.ChartPlotArea);
			A_1++;
			biffRecordRaw = A_0[A_1];
			biffRecordRaw.CheckTypeCode(TBIFFRecord.ChartFrame);
			this.InnerPlotArea.ᜀ(A_0, ref A_1);
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00063AD4 File Offset: 0x00062AD4
		private new void ᜀ(IList<BiffRecordRaw> A_0, ref int A_1)
		{
			int a_ = 9;
			int num = 11;
			for (;;)
			{
				int num2;
				BiffRecordRaw biffRecordRaw;
				switch (num)
				{
				case 0:
					num2++;
					A_1++;
					num = 7;
					continue;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_72;
					default:
						goto IL_129;
					}
					break;
				case 2:
					num = 12;
					continue;
				case 3:
					goto IL_C2;
				case 4:
					if (biffRecordRaw.TypeCode == TBIFFRecord.Begin)
					{
						num = 0;
						continue;
					}
					return;
				case 5:
				{
					if (num2 == 0)
					{
						num = 1;
						continue;
					}
					if (true)
					{
					}
					biffRecordRaw = A_0[A_1];
					TBIFFRecord typeCode = biffRecordRaw.TypeCode;
					num = 6;
					continue;
				}
				case 6:
				{
					TBIFFRecord typeCode;
					switch (typeCode)
					{
					case TBIFFRecord.Begin:
						num2++;
						num = 3;
						continue;
					case TBIFFRecord.End:
						num2--;
						num = 8;
						continue;
					default:
						num = 2;
						continue;
					}
					break;
				}
				case 7:
					goto IL_FA;
				case 8:
					goto IL_C2;
				case 9:
					goto IL_FA;
				case 10:
					goto IL_58;
				case 12:
					goto IL_C2;
				}
				if (A_0 == null)
				{
					num = 10;
					continue;
				}
				IL_72:
				biffRecordRaw = A_0[A_1];
				biffRecordRaw.CheckTypeCode(TBIFFRecord.ChartFrame);
				A_1++;
				num2 = 0;
				biffRecordRaw = A_0[A_1];
				num = 4;
				continue;
				IL_C2:
				A_1++;
				num = 9;
				continue;
				IL_FA:
				num = 5;
			}
			IL_58:
			throw new ArgumentNullException(RecordTableEnumerator.b("嬾⁀㝂⑄", a_));
			IL_129:
			if (false)
			{
			}
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x00063C80 File Offset: 0x00062C80
		public void DetectChartType()
		{
			if (this.Series.Count == 0)
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
					this.ᝈ = ExcelChartType.ColumnClustered;
					return;
				}
			}
			if (true)
			{
			}
			this.ᝈ = this.\u175B.ᜆ().DetectChartType(this.\u1753);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00063CF0 File Offset: 0x00062CF0
		private void \u1715()
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
			this.\u1759 = null;
			this.\u1758 = null;
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00063D3C File Offset: 0x00062D3C
		private new IFont ᜀ(spr\u2241 A_0)
		{
			int a_ = 13;
			if (A_0 == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("╂⩄⥆㵈㍊", a_));
				}
			}
			int index = (int)A_0.ᜀ();
			XlsFont font = this.m_book.InnerFonts[index] as XlsFont;
			return new FontWrapper(font);
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00063DC0 File Offset: 0x00062DC0
		internal void ᜊ(IList<BiffRecordRaw> A_0, ref int A_1)
		{
			int a_ = 17;
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
					throw new ArgumentException(RecordTableEnumerator.b("⍆⡈㽊ⱌ", a_));
				}
			}
			this.HasLegend = true;
			this.\u175D.ᜀ(A_0, ref A_1);
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00063E34 File Offset: 0x00062E34
		private new bool ᜀ(IList<BiffRecordRaw> A_0, IList A_1, ref int A_2, ref int A_3, ref bool A_4)
		{
			int a_ = 1;
			switch (0)
			{
			default:
			{
				int num = 21;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_194;
					case 1:
					{
						TBIFFRecord typeCode;
						if (typeCode != TBIFFRecord.ChartDataFormat)
						{
							num = 8;
							continue;
						}
						BiffRecordRaw biffRecordRaw;
						sprᲡ sprᲡ = (sprᲡ)biffRecordRaw;
						A_3 = (int)sprᲡ.ᜀ();
						num = 22;
						continue;
					}
					case 2:
						goto IL_194;
					case 3:
						num = 6;
						continue;
					case 4:
					{
						bool result;
						return result;
					}
					case 5:
						goto IL_A0;
					case 6:
					{
						TBIFFRecord typeCode;
						if (typeCode != TBIFFRecord.ChartSerAuxErrBar)
						{
							num = 7;
							continue;
						}
						bool result = false;
						A_4 = true;
						num = 9;
						continue;
					}
					case 7:
						goto IL_D8;
					case 8:
						num = 19;
						continue;
					case 9:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D8;
						default:
							if (false)
							{
							}
							goto IL_194;
						}
						break;
					case 10:
						goto IL_237;
					case 11:
						num = 1;
						continue;
					case 12:
						goto IL_194;
					case 13:
					{
						int num2;
						if (num2 <= 0)
						{
							num = 4;
							continue;
						}
						BiffRecordRaw biffRecordRaw = A_0[A_2];
						A_1.Add(biffRecordRaw);
						TBIFFRecord typeCode = biffRecordRaw.TypeCode;
						num = 20;
						continue;
					}
					case 14:
						goto IL_237;
					case 15:
						goto IL_2E2;
					case 16:
						goto IL_194;
					case 17:
						goto IL_194;
					case 18:
						goto IL_194;
					case 19:
					{
						TBIFFRecord typeCode;
						switch (typeCode)
						{
						case TBIFFRecord.Begin:
						{
							int num2;
							num2++;
							num = 16;
							continue;
						}
						case TBIFFRecord.End:
						{
							int num2;
							num2--;
							num = 18;
							continue;
						}
						default:
							num = 23;
							continue;
						}
						break;
					}
					case 20:
					{
						TBIFFRecord typeCode;
						if (typeCode <= TBIFFRecord.End)
						{
							num = 11;
							continue;
						}
						num = 24;
						continue;
					}
					case 22:
						goto IL_194;
					case 23:
						if (true)
						{
						}
						num = 17;
						continue;
					case 24:
					{
						TBIFFRecord typeCode;
						switch (typeCode)
						{
						case TBIFFRecord.ChartSerParent:
						{
							BiffRecordRaw biffRecordRaw;
							A_3 = (int)(((sprᴀ)biffRecordRaw).ᜁ() - 1);
							num = 12;
							continue;
						}
						case TBIFFRecord.ChartSerAuxTrend:
						{
							bool result = false;
							num = 0;
							continue;
						}
						default:
							num = 3;
							continue;
						}
						break;
					}
					case 25:
					{
						if (A_1 == null)
						{
							num = 15;
							continue;
						}
						bool result = true;
						A_4 = false;
						BiffRecordRaw biffRecordRaw = A_0[A_2];
						biffRecordRaw.CheckTypeCode(TBIFFRecord.ChartSeries);
						A_2++;
						A_1.Add(biffRecordRaw);
						biffRecordRaw = A_0[A_2];
						biffRecordRaw.CheckTypeCode(TBIFFRecord.Begin);
						A_2++;
						A_1.Add(biffRecordRaw);
						int num2 = 1;
						num = 14;
						continue;
					}
					}
					if (A_0 == null)
					{
						num = 5;
						continue;
					}
					num = 25;
					continue;
					IL_D8:
					num = 2;
					continue;
					IL_194:
					A_2++;
					num = 10;
					continue;
					IL_237:
					num = 13;
				}
				IL_A0:
				throw new ArgumentNullException(RecordTableEnumerator.b("嬶倸䠺䤼", a_));
				IL_2E2:
				throw new ArgumentNullException(RecordTableEnumerator.b("弶嘸场夼娾㍀", a_));
			}
			}
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0006419C File Offset: 0x0006319C
		private void \u1714()
		{
			switch (0)
			{
			default:
			{
				int num = 4;
				for (;;)
				{
					int num2;
					int count;
					ChartLegendEntriesColl chartLegendEntriesColl;
					ChartLegendEntriesColl chartLegendEntriesColl2;
					switch (num)
					{
					case 0:
						if (num2 < count)
						{
							int num3 = this.ᜀ(this.ᝤ, num2);
							XlsChartSerie xlsChartSerie = (XlsChartSerie)this.\u1753[num3];
							spr\u2457 spr_u = (spr\u2457)xlsChartSerie.TrendLines;
							XlsChartLegendEntry xlsChartLegendEntry;
							sprᴌ a_ = new sprᴌ((spr\u2158)base.ReservedHandle, spr_u, this.ᝤ, ref num2, ref xlsChartLegendEntry);
							spr_u.ᜀ(a_);
							num = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1B7;
						default:
							if (false)
							{
							}
							num = 12;
							continue;
						}
						break;
					case 1:
						goto IL_1B7;
					case 2:
						goto IL_F1;
					case 3:
						chartLegendEntriesColl = (ChartLegendEntriesColl)this.\u175D.LegendEntries;
						goto IL_1CB;
					case 5:
					{
						int num3;
						int legendEntryOffset = this.\u1753.GetLegendEntryOffset(num3);
						chartLegendEntriesColl2.UpdateEntries(legendEntryOffset, 1);
						XlsChartLegendEntry xlsChartLegendEntry;
						chartLegendEntriesColl2.Add(legendEntryOffset, xlsChartLegendEntry);
						num = 11;
						continue;
					}
					case 6:
					{
						XlsChartLegendEntry xlsChartLegendEntry;
						if (xlsChartLegendEntry != null)
						{
							num = 5;
							continue;
						}
						goto IL_143;
					}
					case 7:
						num = 10;
						continue;
					case 8:
						goto IL_F1;
					case 9:
						num = 6;
						continue;
					case 10:
						chartLegendEntriesColl = null;
						goto IL_1CB;
					case 11:
						goto IL_143;
					case 12:
						return;
					}
					if (!this.HasLegend)
					{
						if (true)
						{
						}
						num = 7;
						continue;
					}
					num = 3;
					continue;
					IL_F1:
					num = 0;
					continue;
					IL_143:
					num2++;
					num = 2;
					continue;
					IL_1B7:
					if (chartLegendEntriesColl2 != null)
					{
						num = 9;
						continue;
					}
					goto IL_143;
					IL_1CB:
					chartLegendEntriesColl2 = chartLegendEntriesColl;
					num2 = 0;
					count = this.ᝤ.Count;
					num = 8;
				}
				return;
			}
			}
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x00064394 File Offset: 0x00063394
		private new int ᜀ(List<BiffRecordRaw> A_0, int A_1)
		{
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				int result;
				for (;;)
				{
					result = -1;
					int count = A_0.Count;
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							return result;
						case 1:
						{
							BiffRecordRaw biffRecordRaw;
							if (biffRecordRaw.TypeCode != TBIFFRecord.ChartSerParent)
							{
								A_1++;
								num = 4;
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
								num = 5;
								continue;
							}
							break;
						}
						case 2:
							goto IL_CC;
						case 3:
							return result;
						case 4:
							goto IL_CC;
						case 5:
						{
							BiffRecordRaw biffRecordRaw;
							sprᴀ sprᴀ = (sprᴀ)biffRecordRaw;
							result = (int)(sprᴀ.ᜁ() - 1);
							goto IL_6F;
						}
						case 6:
						{
							if (A_1 >= count)
							{
								num = 0;
								continue;
							}
							BiffRecordRaw biffRecordRaw = A_0[A_1];
							num = 1;
							continue;
						}
						}
						break;
						IL_6F:
						num = 3;
						continue;
						IL_CC:
						num = 6;
					}
				}
				return result;
			}
			}
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0006448C File Offset: 0x0006348C
		public override void SerializeDataToList(RecordArrayList records)
		{
			int a_ = 13;
			int num = 7;
			for (;;)
			{
				spr\u203C spr_u203C;
				IXLSRange ixlsrange;
				IXLSRange ixlsrange2;
				switch (num)
				{
				case 0:
					if (this.ᜎ.Count > 0)
					{
						num = 2;
						continue;
					}
					records.ᜀ(this.\u171E);
					this.SerializeHeaderFooterPictures(records);
					this.ᝍ.SerializeDataToList(records);
					this.ᜊ(records);
					num = 15;
					continue;
				case 1:
					goto IL_1E2;
				case 2:
					goto IL_EC;
				case 3:
					num = 17;
					continue;
				case 4:
					goto IL_17C;
				case 5:
					goto IL_17C;
				case 6:
					if (!this.ᝇ)
					{
						num = 12;
						continue;
					}
					goto IL_319;
				case 8:
					spr_u203C.ᜀ((ixlsrange != null) ? this.\u1753[0].Values.Cells.Length : 0);
					records.ᜀ(spr_u203C);
					this.ᜅ(records);
					num = 6;
					continue;
				case 9:
					if (this.\u1753.Count <= 0)
					{
						num = 18;
						continue;
					}
					goto IL_276;
				case 10:
					goto IL_78;
				case 11:
					records.ᜀ(spr\u175E.ᜀ(TBIFFRecord.Protect));
					num = 4;
					continue;
				case 12:
					this.SerializeWindowTwo(records);
					base.ᜐ(records);
					base.ᜑ(records);
					num = 13;
					continue;
				case 13:
					goto IL_317;
				case 14:
					if (this.ᝥ != null)
					{
						num = 3;
						continue;
					}
					goto IL_1E2;
				case 15:
					if (this.ᝇ)
					{
						num = 11;
						continue;
					}
					this.SerializeProtection(records, true);
					num = 5;
					continue;
				case 16:
					ixlsrange2 = null;
					goto IL_294;
				case 17:
					if (this.ᝥ.Count > 0)
					{
						num = 19;
						continue;
					}
					goto IL_1E2;
				case 18:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_276;
					default:
						if (false)
						{
						}
						num = 16;
						continue;
					}
					break;
				case 19:
					records.AddRange(this.ᝥ);
					num = 1;
					continue;
				case 20:
					ixlsrange2 = this.\u1753[0].Values;
					goto IL_294;
				}
				if (records == null)
				{
					num = 10;
					continue;
				}
				int count = this.ᜎ.Count;
				this.\u171E.ᜀ(sprḯ.TType.TYPE_CHART);
				this.\u171E.ᜀ(base.FindParent(typeof(XlsWorksheet)) != null);
				num = 0;
				continue;
				IL_17C:
				this.SerializeMsoDrawings(records);
				num = 14;
				continue;
				IL_1E2:
				records.ᜀ(spr\u175E.ᜀ(TBIFFRecord.ChartUnits));
				this.ᜉ(records);
				spr_u203C = (spr\u203C)spr\u175E.ᜀ(TBIFFRecord.Dimensions);
				spr_u203C.ᜀ((ushort)(this.\u1753.TrendErrorBarIndex + 1));
				num = 9;
				continue;
				IL_276:
				num = 20;
				continue;
				IL_294:
				ixlsrange = ixlsrange2;
				num = 8;
			}
			IL_78:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄⑆♈㥊⥌㱎", a_));
			IL_EC:
			records.AddList(this.ᜎ);
			return;
			IL_317:
			IL_319:
			this.SerializeMacrosSupport(records);
			records.ᜀ(spr\u175E.ᜀ(TBIFFRecord.EOF));
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x000647C8 File Offset: 0x000637C8
		private void ᜊ(RecordArrayList A_0)
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
			A_0.AddList(this.\u1752);
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x00064810 File Offset: 0x00063810
		private void ᜉ(RecordArrayList A_0)
		{
			for (;;)
			{
				sprὬ sprὬ = (sprὬ)spr\u175E.ᜀ(TBIFFRecord.ChartChart);
				sprὬ.ᜁ(XlsChart.ᜀ(this.XPos));
				sprὬ.ᜀ(XlsChart.ᜀ(this.YPos));
				sprὬ.ᜂ(XlsChart.ᜀ(this.Width));
				sprὬ.ᜃ(XlsChart.ᜀ(this.Height));
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						sprὬ.ᜂ(48027384);
						sprὬ.ᜃ(29506896);
						num = 5;
						continue;
					case 1:
						goto IL_183;
					case 2:
					{
						if (this.ᝦ != null)
						{
							num = 6;
							continue;
						}
						spr\u1CF7 spr_u1CF = (spr\u1CF7)spr\u175E.ᜀ(TBIFFRecord.WindowZoom);
						spr_u1CF.ᜀ(1);
						spr_u1CF.ᜁ(1);
						A_0.ᜀ(spr_u1CF);
						num = 12;
						continue;
					}
					case 3:
						if (sprὬ.ᜃ() == 0)
						{
							num = 0;
							continue;
						}
						goto IL_109;
					case 4:
						this.\u1758.ᜀ(A_0);
						num = 10;
						continue;
					case 5:
						goto IL_109;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_20F;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							A_0.ᜀ(this.ᝦ);
							num = 9;
							continue;
						}
						break;
					case 7:
						if (this.HasTitle)
						{
							num = 8;
							continue;
						}
						goto IL_267;
					case 8:
						goto IL_20F;
					case 9:
						goto IL_CF;
					case 10:
						goto IL_1B8;
					case 11:
						if (this.\u1758 != null)
						{
							num = 4;
							continue;
						}
						goto IL_1B8;
					case 12:
						goto IL_CF;
					}
					break;
					IL_CF:
					A_0.ᜀ((BiffRecordRaw)this.PlotGrowth.Clone());
					num = 11;
					continue;
					IL_109:
					A_0.ᜀ(sprὬ);
					A_0.ᜀ(spr\u175E.ᜀ(TBIFFRecord.Begin));
					num = 2;
					continue;
					IL_1B8:
					this.\u1753.SerializeDataToList(A_0);
					this.ᜆ(A_0);
					this.ᜈ(A_0);
					this.ᜇ(A_0);
					A_0.ᜀ(this.\u1753.TrendLabels);
					this.ᜂ(A_0);
					num = 7;
					continue;
					IL_20F:
					this.m_title.SerializeDataToList(A_0);
					num = 1;
				}
			}
			IL_183:
			IL_267:
			this.ᜁ(A_0);
			A_0.ᜀ(spr\u175E.ᜀ(TBIFFRecord.End));
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x00064A9C File Offset: 0x00063A9C
		private void ᜈ(RecordArrayList A_0)
		{
			int a_ = 1;
			switch (0)
			{
			default:
			{
				int num = 1;
				for (;;)
				{
					int num2;
					int count;
					switch (num)
					{
					case 0:
						goto IL_102;
					case 2:
					{
						if (num2 >= count)
						{
							num = 3;
							continue;
						}
						int key = this.\u175A.GetKey(num2);
						List<BiffRecordRaw> byIndex = this.\u175A.GetByIndex(num2);
						spr\u2350 spr_u = (spr\u2350)spr\u175E.ᜀ(TBIFFRecord.ChartDefaultText);
						spr_u.ᜀ((spr\u2350.TextDefaults)key);
						A_0.ᜀ(spr_u);
						A_0.AddList(byIndex);
						num2++;
						num = 5;
						continue;
					}
					case 3:
						return;
					case 4:
						goto IL_4D;
					case 5:
						goto IL_102;
					}
					if (A_0 == null)
					{
						num = 4;
						continue;
					}
					num2 = 0;
					count = this.\u175A.Count;
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_EE;
					}
					if (false)
					{
					}
					num = 0;
					continue;
					IL_102:
					num = 2;
				}
				IL_4D:
				IL_EE:
				throw new ArgumentNullException(RecordTableEnumerator.b("䔶尸堺刼䴾╀あ", a_));
			}
			}
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x00064BCC File Offset: 0x00063BCC
		private void ᜇ(RecordArrayList A_0)
		{
			for (;;)
			{
				if (true)
				{
				}
				sprỴ sprỴ = (sprỴ)spr\u175E.ᜀ(TBIFFRecord.ChartAxesUsed);
				int count = this.SecondaryFormats.Count;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (count > 0)
						{
							num = 3;
							continue;
						}
						return;
					case 1:
						sprỴ.ᜀ((count > 0) ? 2 : 1);
						A_0.ᜀ(sprỴ);
						this.\u175B.ᜀ(A_0);
						goto IL_9A;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_9A;
						default:
							goto IL_78;
						}
						break;
					case 3:
						this.\u175C.ᜀ(A_0);
						num = 2;
						continue;
					}
					break;
					IL_9A:
					num = 0;
				}
			}
			IL_78:
			if (false)
			{
			}
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x00064C9C File Offset: 0x00063C9C
		private void ᜆ(RecordArrayList A_0)
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
			A_0.ᜀ((BiffRecordRaw)this.\u1755.Clone());
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x00064CF0 File Offset: 0x00063CF0
		private new void ᜅ(RecordArrayList A_0)
		{
			int a_ = 0;
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
				throw new ArgumentNullException(RecordTableEnumerator.b("䐵崷夹医䰽␿ㅁ", a_));
			}
			spr\u220C spr_u220C = (spr\u220C)spr\u175E.ᜀ(TBIFFRecord.ChartSiIndex);
			spr_u220C.ᜀ(2);
			A_0.ᜀ(spr_u220C);
			this.ᜀ(A_0, 2);
			this.ᜄ(A_0);
			spr_u220C = (spr\u220C)spr_u220C.Clone();
			spr_u220C.ᜀ(1);
			A_0.ᜀ(spr_u220C);
			this.ᜀ(A_0, 1);
			spr_u220C = (spr\u220C)spr_u220C.Clone();
			spr_u220C.ᜀ(3);
			A_0.ᜀ(spr_u220C);
			this.ᜀ(A_0, 3);
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x00064DC4 File Offset: 0x00063DC4
		private new void ᜄ(RecordArrayList A_0)
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

		// Token: 0x06000A24 RID: 2596 RVA: 0x00064E00 File Offset: 0x00063E00
		private new void ᜃ(RecordArrayList A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					spr\u19FF spr_u19FF = (spr\u19FF)spr\u175E.ᜀ(TBIFFRecord.Number);
					int num = 0;
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_B4;
						case 1:
							goto IL_63;
						case 2:
							goto IL_B4;
						case 3:
						{
							if (num >= this.Series.Count)
							{
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_B2;
								}
								if (false)
								{
								}
								num2 = 5;
								continue;
							}
							XlsChartSerie xlsChartSerie = (XlsChartSerie)this.Series[num];
							int num3 = 0;
							CellRange[] cells = xlsChartSerie.Values.Cells;
							int num4 = 0;
							num2 = 6;
							continue;
						}
						case 4:
							num++;
							num2 = 0;
							continue;
						case 5:
							return;
						case 6:
							goto IL_B2;
						case 7:
						{
							CellRange[] cells;
							int num4;
							if (num4 >= cells.Length)
							{
								num2 = 4;
								continue;
							}
							IXLSRange ixlsrange = cells[num4];
							spr_u19FF = (spr\u19FF)spr_u19FF.Clone();
							int num3;
							spr_u19FF.ᜇ((int)((ushort)num3));
							spr_u19FF.ᜆ((int)((ushort)num));
							spr_u19FF.ᜀ(ixlsrange.NumberValue);
							spr_u19FF.ᜁ(0);
							A_0.ᜀ(spr_u19FF);
							num3++;
							num4++;
							if (true)
							{
							}
							num2 = 1;
							continue;
						}
						}
						break;
						IL_63:
						num2 = 7;
						continue;
						IL_B2:
						goto IL_63;
						IL_B4:
						num2 = 3;
					}
				}
				return;
			}
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x00064F74 File Offset: 0x00063F74
		private new void ᜂ(RecordArrayList A_0)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.\u1754.SerializeDataToList(A_0);
					num = 1;
					continue;
				case 1:
					return;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_52;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						break;
					}
					break;
				}
				goto IL_4A;
				IL_52:
				num = 0;
				continue;
				IL_4A:
				if (this.HasDataTable)
				{
					goto IL_52;
				}
				break;
			}
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x00064FF4 File Offset: 0x00063FF4
		private void ᜁ(RecordArrayList A_0)
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
			this.\u1753.ᜀ(A_0);
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0006503C File Offset: 0x0006403C
		private new void ᜀ(RecordArrayList A_0)
		{
			int num = 2;
			for (;;)
			{
				spr\u2274 spr_u;
				switch (num)
				{
				case 0:
					spr_u.ᜀ(new ushort[]
					{
						1,
						2,
						3,
						4,
						5
					});
					num = 1;
					continue;
				case 1:
					goto IL_CD;
				case 3:
					if (this.ChartType == ExcelChartType.StockVolumeHighLowClose)
					{
						if (true)
						{
						}
						num = 7;
						continue;
					}
					num = 6;
					continue;
				case 4:
					spr_u = (spr\u2274)spr\u175E.ᜀ(TBIFFRecord.ChartSeriesList);
					num = 3;
					continue;
				case 5:
					goto IL_E0;
				case 6:
					if (this.ChartType == ExcelChartType.StockVolumeOpenHighLowClose)
					{
						num = 0;
						continue;
					}
					goto IL_CD;
				case 7:
					spr_u.ᜀ(new ushort[]
					{
						1,
						2,
						3,
						4
					});
					num = 8;
					continue;
				case 8:
					goto IL_CD;
				}
				if (this.IsChartVolume)
				{
					num = 4;
					continue;
				}
				return;
				IL_CD:
				A_0.ᜀ(spr_u);
				num = 5;
			}
			IL_E0:
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
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0006516C File Offset: 0x0006416C
		private new void ᜀ(RecordArrayList A_0, int A_1)
		{
			int a_ = 4;
			int num = 10;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_E0;
				case 1:
				{
					if (A_1 < 1)
					{
						num = 6;
						continue;
					}
					List<BiffRecordRaw> list = this.\u1753.ᜄ(A_1);
					num = 5;
					continue;
				}
				case 2:
					goto IL_50;
				case 3:
					if (true)
					{
					}
					goto IL_121;
				case 4:
					if (A_1 <= 3)
					{
						num = 3;
						continue;
					}
					goto IL_96;
				case 5:
				{
					List<BiffRecordRaw> list;
					if (list != null)
					{
						num = 8;
						continue;
					}
					return;
				}
				case 6:
					goto IL_13E;
				case 7:
				{
					List<BiffRecordRaw> list;
					if (list.Count > 0)
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
						goto IL_121;
					default:
						if (false)
						{
						}
						num = 7;
						continue;
					}
					break;
				case 9:
				{
					List<BiffRecordRaw> list;
					A_0.AddList(list);
					num = 0;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				num = 4;
				continue;
				IL_121:
				num = 1;
			}
			IL_50:
			throw new ArgumentNullException(RecordTableEnumerator.b("䠹夻崽⼿ぁ⁃㕅", a_));
			IL_96:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䤹唻眽⸿♁⅃㹅", a_));
			IL_E0:
			return;
			IL_13E:
			goto IL_96;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x000652BC File Offset: 0x000642BC
		internal void ᜋ(RecordArrayList A_0)
		{
			int num = 1;
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
						goto IL_52;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 2:
					this.\u175D.SerializeDataToList(A_0);
					num = 0;
					continue;
				}
				goto IL_38;
				IL_52:
				num = 2;
				continue;
				IL_38:
				if (true)
				{
				}
				if (this.\u175D != null)
				{
					goto IL_52;
				}
				break;
			}
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0006533C File Offset: 0x0006433C
		internal void ᜌ(RecordArrayList A_0)
		{
			int a_ = 0;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_86;
				case 2:
					return;
				case 3:
					goto IL_38;
				case 4:
					if (true)
					{
					}
					this.\u175F.SerializeDataToList(A_0);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_86;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				}
				if (A_0 == null)
				{
					num = 3;
					continue;
				}
				num = 0;
				continue;
				IL_86:
				if (this.\u175F == null)
				{
					return;
				}
				num = 4;
			}
			IL_38:
			throw new ArgumentNullException(RecordTableEnumerator.b("䐵崷夹医䰽␿ㅁ", a_));
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x000653F8 File Offset: 0x000643F8
		internal void \u170D(RecordArrayList A_0)
		{
			int a_ = 6;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					goto IL_38;
				case 2:
					goto IL_86;
				case 4:
					if (true)
					{
					}
					this.ᝠ.SerializeDataToList(A_0);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_86;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				}
				if (A_0 == null)
				{
					num = 1;
					continue;
				}
				num = 2;
				continue;
				IL_86:
				if (this.ᝠ == null)
				{
					return;
				}
				num = 4;
			}
			IL_38:
			throw new ArgumentNullException(RecordTableEnumerator.b("主嬽⌿ⵁ㙃≅㭇", a_));
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x000654B4 File Offset: 0x000644B4
		internal new void ᜎ(RecordArrayList A_0)
		{
			int a_ = 5;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_86;
				case 1:
					goto IL_40;
				case 3:
					this.ᝡ.ᜀ(A_0);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_86;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				case 4:
					return;
				}
				if (true)
				{
				}
				if (A_0 == null)
				{
					num = 1;
					continue;
				}
				num = 0;
				continue;
				IL_86:
				if (this.ᝡ == null)
				{
					return;
				}
				num = 3;
			}
			IL_40:
			throw new ArgumentNullException(RecordTableEnumerator.b("䤺堼尾⹀ㅂ⅄㑆", a_));
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x00065570 File Offset: 0x00064570
		// (set) Token: 0x06000A2E RID: 2606 RVA: 0x000655B8 File Offset: 0x000645B8
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
				return this.XlsChartFormat.Rotation;
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
				this.XlsChartFormat.Rotation = value;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x00065600 File Offset: 0x00064600
		// (set) Token: 0x06000A30 RID: 2608 RVA: 0x00065648 File Offset: 0x00064648
		public int Elevation
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
				return this.XlsChartFormat.Elevation;
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
				this.XlsChartFormat.Elevation = value;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x00065690 File Offset: 0x00064690
		// (set) Token: 0x06000A32 RID: 2610 RVA: 0x000656D8 File Offset: 0x000646D8
		public int Perspective
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
				return this.XlsChartFormat.Perspective;
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
				this.XlsChartFormat.Perspective = value;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x00065720 File Offset: 0x00064720
		// (set) Token: 0x06000A34 RID: 2612 RVA: 0x00065764 File Offset: 0x00064764
		public ExcelChartType PivotChartType
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
				return this.ᝉ;
			}
			set
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_4B;
					case 1:
						this.ChartType = value;
						num = 8;
						continue;
					case 2:
						this.ᝮ = null;
						num = 4;
						continue;
					case 4:
						return;
					case 5:
					{
						bool hasPivotTable;
						if (!hasPivotTable)
						{
							num = 2;
							continue;
						}
						return;
					}
					case 6:
					{
						bool hasPivotTable;
						if (!hasPivotTable)
						{
							num = 7;
							continue;
						}
						goto IL_4B;
					}
					case 7:
						IL_102:
						this.ᝮ = string.Empty;
						if (true)
						{
						}
						num = 0;
						continue;
					case 8:
						goto IL_BC;
					}
					if (this.Series.Count != 0)
					{
						num = 1;
						continue;
					}
					goto IL_BC;
					IL_4B:
					this.CreateNecessaryAxes(true);
					num = 5;
					continue;
					IL_BC:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_102;
					default:
					{
						if (false)
						{
						}
						this.ᝉ = value;
						bool hasPivotTable = this.HasPivotTable;
						num = 6;
						break;
					}
					}
				}
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x00065878 File Offset: 0x00064878
		// (set) Token: 0x06000A36 RID: 2614 RVA: 0x000658BC File Offset: 0x000648BC
		public PivotTable PivotTable
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
				return this.\u176D;
			}
			set
			{
				int a_ = 19;
				if (true)
				{
				}
				if (Array.IndexOf<ExcelChartType>(XlsChart.ᜦ, this.PivotChartType) != -1)
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
					throw new NotSupportedException(RecordTableEnumerator.b("᥈≊㭌⁎═ၒ㵔㙖⭘⽚ड़♞ᅠ٢", a_));
				}
				this.ᝮ = null;
				this.\u176D = value;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x00065938 File Offset: 0x00064938
		// (set) Token: 0x06000A38 RID: 2616 RVA: 0x0006597C File Offset: 0x0006497C
		internal string PreservedPivotSource
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
				return this.ᝮ;
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
				this.ᝮ = value;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x000659C0 File Offset: 0x000649C0
		public bool HasPivotTable
		{
			get
			{
				if (true)
				{
				}
				if (this.PivotTable == null)
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
						return this.PreservedPivotSource != null;
					}
				}
				return true;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x00065A14 File Offset: 0x00064A14
		// (set) Token: 0x06000A3B RID: 2619 RVA: 0x00065A58 File Offset: 0x00064A58
		public bool DisplayEntireFieldButtons
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
				return this.ᝯ;
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
				this.ᝯ = value;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x00065A9C File Offset: 0x00064A9C
		// (set) Token: 0x06000A3D RID: 2621 RVA: 0x00065AE0 File Offset: 0x00064AE0
		public bool DisplayValueFieldButtons
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
				return this.\u1771;
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
				this.\u1771 = value;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x00065B24 File Offset: 0x00064B24
		// (set) Token: 0x06000A3F RID: 2623 RVA: 0x00065B68 File Offset: 0x00064B68
		public bool DisplayAxisFieldButtons
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
				return this.ᝰ;
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
				this.ᝰ = value;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x00065BAC File Offset: 0x00064BAC
		// (set) Token: 0x06000A41 RID: 2625 RVA: 0x00065BF0 File Offset: 0x00064BF0
		public bool DisplayLegendFieldButtons
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
				return this.\u1772;
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
				this.\u1772 = value;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x00065C34 File Offset: 0x00064C34
		// (set) Token: 0x06000A43 RID: 2627 RVA: 0x00065C78 File Offset: 0x00064C78
		public bool ShowReportFilterFieldButtons
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
				return this.\u1773;
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
				this.\u1773 = value;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x00065CBC File Offset: 0x00064CBC
		// (set) Token: 0x06000A45 RID: 2629 RVA: 0x00065D04 File Offset: 0x00064D04
		public int HeightPercent
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
				return this.XlsChartFormat.HeightPercent;
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
				this.XlsChartFormat.HeightPercent = value;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x00065D4C File Offset: 0x00064D4C
		// (set) Token: 0x06000A47 RID: 2631 RVA: 0x00065D94 File Offset: 0x00064D94
		public int DepthPercent
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
				return this.XlsChartFormat.DepthPercent;
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
				this.XlsChartFormat.DepthPercent = value;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x00065DDC File Offset: 0x00064DDC
		// (set) Token: 0x06000A49 RID: 2633 RVA: 0x00065E24 File Offset: 0x00064E24
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
				return this.XlsChartFormat.GapDepth;
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
				this.XlsChartFormat.GapDepth = value;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x00065E6C File Offset: 0x00064E6C
		// (set) Token: 0x06000A4B RID: 2635 RVA: 0x00065EB4 File Offset: 0x00064EB4
		public bool RightAngleAxes
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
				return this.XlsChartFormat.RightAngleAxes;
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
				this.XlsChartFormat.RightAngleAxes = value;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x00065EFC File Offset: 0x00064EFC
		// (set) Token: 0x06000A4D RID: 2637 RVA: 0x00065F44 File Offset: 0x00064F44
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
				return this.XlsChartFormat.AutoScaling;
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
				this.XlsChartFormat.AutoScaling = value;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x00065F8C File Offset: 0x00064F8C
		// (set) Token: 0x06000A4F RID: 2639 RVA: 0x00065FD4 File Offset: 0x00064FD4
		public bool WallsAndGridlines2D
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
				return this.XlsChartFormat.WallsAndGridlines2D;
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
				this.XlsChartFormat.WallsAndGridlines2D = value;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x0006601C File Offset: 0x0006501C
		// (set) Token: 0x06000A51 RID: 2641 RVA: 0x00066064 File Offset: 0x00065064
		public ExcelChartType ChartType
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
				this.DetectChartType();
				return this.ᝈ;
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
				this.ᜁ(value, false);
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x000660A8 File Offset: 0x000650A8
		// (set) Token: 0x06000A53 RID: 2643 RVA: 0x00066130 File Offset: 0x00065130
		public IXLSRange DataRange
		{
			get
			{
				int num = 1;
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
							goto IL_72;
						default:
							if (false)
							{
							}
							this.ᝊ = this.ᜀ();
							num = 2;
							continue;
						}
						break;
					case 2:
						goto IL_70;
					}
					if (this.ᝊ != null)
					{
						break;
					}
					num = 0;
				}
				IL_70:
				IL_72:
				return this.ᝊ;
			}
			set
			{
				int num = 6;
				for (;;)
				{
					XlsChartSerie xlsChartSerie;
					switch (num)
					{
					case 0:
					{
						ExcelChartType chartType = this.ChartType;
						this.ᝊ = value;
						this.ᜂ(chartType);
						xlsChartSerie = (XlsChartSerie)this.\u1753[0];
						num = 3;
						continue;
					}
					case 1:
						num = 4;
						continue;
					case 2:
						IL_99:
						goto IL_42;
					case 3:
						if (xlsChartSerie.NumRefFormula == null)
						{
							num = 1;
							continue;
						}
						goto IL_42;
					case 4:
						if (xlsChartSerie.StrRefFormula != null)
						{
							num = 2;
							continue;
						}
						return;
					case 5:
						return;
					}
					if (this.ᝊ != value)
					{
						num = 0;
						continue;
					}
					break;
					IL_42:
					xlsChartSerie.NumRefFormula = null;
					xlsChartSerie.StrRefFormula = null;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_99;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 5;
						break;
					}
				}
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x0006622C File Offset: 0x0006522C
		// (set) Token: 0x06000A55 RID: 2645 RVA: 0x00066270 File Offset: 0x00065270
		public bool SeriesDataFromRange
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
				return this.ᝋ;
			}
			set
			{
				int a_ = 11;
				for (;;)
				{
					int count = this.\u1753.Count;
					int num = 5;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_CF;
						case 1:
							return;
						case 2:
							this.ᜇ();
							if (true)
							{
							}
							num = 1;
							continue;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_CF;
							}
							if (false)
							{
							}
							this.ᝋ = value;
							num = 4;
							continue;
						case 4:
							if (count != 0)
							{
								num = 2;
								continue;
							}
							return;
						case 5:
							if (this.DataRange == null)
							{
								num = 7;
								continue;
							}
							goto IL_A3;
						case 6:
							if (this.ᝋ != value)
							{
								num = 3;
								continue;
							}
							return;
						case 7:
							num = 0;
							continue;
						case 8:
							goto IL_DD;
						}
						break;
						IL_A3:
						num = 6;
						continue;
						IL_CF:
						if (count == 0)
						{
							goto IL_A3;
						}
						num = 8;
					}
				}
				IL_DD:
				throw new ApplicationException(RecordTableEnumerator.b("Հ≂ㅄ♆楈㥊ⱌⅎ㙐㙒畔㑖㡘㕚㍜ぞᕠ䍢ݤɦ䥨൪ɬᩮὰᝲ孴", a_));
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x0006638C File Offset: 0x0006538C
		// (set) Token: 0x06000A57 RID: 2647 RVA: 0x000663D4 File Offset: 0x000653D4
		public string ChartTitle
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
				return this.ChartTitleArea.Text;
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
				this.ChartTitleArea.Text = value;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x0006641C File Offset: 0x0006541C
		protected internal IChartTextArea ChartTitleArea
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6A;
					case 2:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6C;
						default:
							if (false)
							{
							}
							this.\u1716();
							num = 0;
							continue;
						}
						break;
					}
					if (this.m_title != null)
					{
						break;
					}
					num = 2;
				}
				IL_6A:
				IL_6C:
				return this.m_title;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x0006649C File Offset: 0x0006549C
		public IFont ChartTitleFont
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
				return this.ChartTitleArea;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x000664E0 File Offset: 0x000654E0
		// (set) Token: 0x06000A5B RID: 2651 RVA: 0x00066528 File Offset: 0x00065528
		public string CategoryAxisTitle
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
				return this.PrimaryCategoryAxis.Title;
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
				this.PrimaryCategoryAxis.Title = value;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x00066570 File Offset: 0x00065570
		// (set) Token: 0x06000A5D RID: 2653 RVA: 0x000665B8 File Offset: 0x000655B8
		public string ValueAxisTitle
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
				return this.PrimaryValueAxis.Title;
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
				this.PrimaryValueAxis.Title = value;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x00066600 File Offset: 0x00065600
		// (set) Token: 0x06000A5F RID: 2655 RVA: 0x00066648 File Offset: 0x00065648
		public string SecondaryCategoryAxisTitle
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
				return this.SecondaryCategoryAxis.Title;
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
				this.SecondaryCategoryAxis.Title = value;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x00066690 File Offset: 0x00065690
		// (set) Token: 0x06000A61 RID: 2657 RVA: 0x000666D8 File Offset: 0x000656D8
		public string SecondaryValueAxisTitle
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
				return this.SecondaryValueAxis.Title;
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
				this.SecondaryValueAxis.Title = value;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x00066720 File Offset: 0x00065720
		// (set) Token: 0x06000A63 RID: 2659 RVA: 0x00066768 File Offset: 0x00065768
		public string SeriesAxisTitle
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
				return this.PrimarySerieAxis.Title;
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
				this.PrimarySerieAxis.Title = value;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x000667B0 File Offset: 0x000657B0
		public IChartCategoryAxis PrimaryCategoryAxis
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
				return this.\u175B.ᜂ();
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x000667F8 File Offset: 0x000657F8
		public IChartValueAxis PrimaryValueAxis
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
				return this.\u175B.ᜃ();
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x00066840 File Offset: 0x00065840
		public IChartSeriesAxis PrimarySerieAxis
		{
			get
			{
				int a_ = 16;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (!this.Loading)
						{
							num = 2;
							continue;
						}
						goto IL_97;
					case 1:
						if (true)
						{
						}
						break;
					case 2:
						goto IL_95;
					case 3:
						goto IL_67;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_67:
						num = 0;
						break;
					default:
						if (false)
						{
						}
						if (this.IsSeriesAxisAvail)
						{
							goto IL_97;
						}
						num = 3;
						break;
					}
				}
				IL_95:
				throw new NotSupportedException(RecordTableEnumerator.b("ᕅⵇ㡉╋⭍⍏牑㕓⹕ㅗ⥙籛㩝ཟݡᝣࡥݧṩ䱫୭࡯᭱ݳɵ塷፹ቻ幽꺍晴몙킟잡誣", a_));
				IL_97:
				return this.\u175B.ᜅ();
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x000668F0 File Offset: 0x000658F0
		public IChartCategoryAxis SecondaryCategoryAxis
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
				return this.\u175C.ᜂ();
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000A68 RID: 2664 RVA: 0x00066938 File Offset: 0x00065938
		public IChartValueAxis SecondaryValueAxis
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
				return this.\u175C.ᜃ();
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x00066980 File Offset: 0x00065980
		public IChartPageSetup PageSetup
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
				return this.ᝍ;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x000669C4 File Offset: 0x000659C4
		// (set) Token: 0x06000A6B RID: 2667 RVA: 0x00066A08 File Offset: 0x00065A08
		public double XPos
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
				return this.ᝎ;
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
				this.ᝎ = value;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x00066A4C File Offset: 0x00065A4C
		// (set) Token: 0x06000A6D RID: 2669 RVA: 0x00066A90 File Offset: 0x00065A90
		public double YPos
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
				return this.ᝏ;
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
				this.ᝏ = value;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x00066AD4 File Offset: 0x00065AD4
		// (set) Token: 0x06000A6F RID: 2671 RVA: 0x00066B18 File Offset: 0x00065B18
		public double Width
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
				return this.ᝐ;
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
				this.ᝐ = value;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x00066B5C File Offset: 0x00065B5C
		// (set) Token: 0x06000A71 RID: 2673 RVA: 0x00066BA0 File Offset: 0x00065BA0
		public double Height
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
				return this.ᝑ;
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
				this.ᝑ = value;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x00066BE4 File Offset: 0x00065BE4
		internal XlsChartSeries Series
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
				return this.\u1753;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x00066C28 File Offset: 0x00065C28
		protected internal XlsChartFormatCollection PrimaryFormats
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
				return this.\u175B.ᜊ();
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x00066C70 File Offset: 0x00065C70
		protected internal XlsChartFormatCollection SecondaryFormats
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
				return this.\u175C.ᜊ();
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x00066CB8 File Offset: 0x00065CB8
		public IChartFrameFormat ChartArea
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
							goto IL_87;
						default:
							if (false)
							{
							}
							this.\u1758 = new ChartArea((spr\u2158)base.ReservedHandle, this);
							this.\u1758.Interior.ForegroundKnownColor = ExcelColors.WhiteCustom;
							num = 2;
							continue;
						}
						break;
					case 2:
						goto IL_85;
					}
					if (this.\u1758 != null)
					{
						break;
					}
					num = 0;
				}
				IL_85:
				IL_87:
				if (true)
				{
				}
				return this.\u1758;
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x00066D5C File Offset: 0x00065D5C
		// (set) Token: 0x06000A77 RID: 2679 RVA: 0x00066DA4 File Offset: 0x00065DA4
		public bool HasChartArea
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
				return this.\u1758 != null;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					IL_0A:
					switch (num)
					{
					case 1:
						this.\u1758 = (value ? new XlsChartFrameFormat(base.AppImplementation, this) : null);
						num = 3;
						continue;
					case 2:
						num = 1;
						continue;
					case 3:
						goto IL_85;
					}
					while (value != this.HasChartArea)
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
							num = 2;
							goto IL_0A;
						}
					}
					return;
				}
				IL_85:
				if (true)
				{
				}
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x00066E40 File Offset: 0x00065E40
		// (set) Token: 0x06000A79 RID: 2681 RVA: 0x00066E88 File Offset: 0x00065E88
		public bool HasPlotArea
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
				return this.ᝡ != null;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					IL_0A:
					switch (num)
					{
					case 1:
						return;
					case 2:
						num = 3;
						continue;
					case 3:
						this.ᝡ = (value ? new ChartPlotArea((spr\u2158)base.ReservedHandle, this, this.ChartType) : null);
						num = 1;
						continue;
					}
					while (value != this.HasPlotArea)
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
							num = 2;
							goto IL_0A;
						}
					}
					break;
				}
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x00066F34 File Offset: 0x00065F34
		// (set) Token: 0x06000A7B RID: 2683 RVA: 0x00066F78 File Offset: 0x00065F78
		public IChartFrameFormat PlotArea
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
				return this.ᝡ;
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
				this.ᝡ = (XlsChartPlotArea)value;
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x00066FC0 File Offset: 0x00065FC0
		internal sprᾹ PrimaryParentAxis
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
				return this.\u175B;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x00067004 File Offset: 0x00066004
		internal sprᾹ SecondaryParentAxis
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
				return this.\u175C;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x00067048 File Offset: 0x00066048
		// (set) Token: 0x06000A7F RID: 2687 RVA: 0x00067154 File Offset: 0x00066154
		public IChartWallOrFloor Walls
		{
			get
			{
				int a_ = 7;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 6;
						continue;
					case 1:
						if (this.\u175F == null)
						{
							if (true)
							{
							}
							num = 5;
							continue;
						}
						goto IL_EE;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_A0;
						default:
							goto IL_C1;
						}
						break;
					case 4:
						goto IL_EC;
					case 5:
						this.\u175F = new ChartWallOrFloor((spr\u2158)base.ReservedHandle, this, true);
						num = 4;
						continue;
					case 6:
						if (Array.IndexOf<ExcelChartType>(XlsChart.ᜨ, this.ChartType) == -1)
						{
							goto IL_A0;
						}
						goto IL_4C;
					}
					if (!this.m_book.Loading)
					{
						num = 0;
						continue;
					}
					IL_4C:
					num = 1;
					continue;
					IL_A0:
					num = 2;
				}
				IL_C1:
				if (false)
				{
				}
				throw new ApplicationException(RecordTableEnumerator.b("樼帾ⵀ⽂㙄杆⩈⩊⍌ⅎ㹐❒畔㕖㱘筚⹜⩞ᅠ።੤ᕦᵨ๪६佮ᝰᱲݴ坶൸፺᡼彾ﶈꮊ歷", a_));
				IL_EC:
				IL_EE:
				return this.\u175F;
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
				this.\u175F = (XlsChartWallOrFloor)value;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x0006719C File Offset: 0x0006619C
		// (set) Token: 0x06000A81 RID: 2689 RVA: 0x0006729C File Offset: 0x0006629C
		public IChartWallOrFloor Floor
		{
			get
			{
				int a_ = 4;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᝠ == null)
						{
							num = 5;
							continue;
						}
						goto IL_E3;
					case 1:
						num = 6;
						continue;
					case 3:
						goto IL_E1;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_8D;
						default:
							goto IL_AE;
						}
						break;
					case 5:
						this.ᝠ = new ChartWallOrFloor((spr\u2158)base.ReservedHandle, this, false);
						num = 3;
						continue;
					case 6:
						if (!this.SupportWallsAndFloor)
						{
							goto IL_8D;
						}
						goto IL_4C;
					}
					if (!this.m_book.Loading)
					{
						num = 1;
						continue;
					}
					IL_4C:
					num = 0;
					continue;
					IL_8D:
					num = 4;
				}
				IL_AE:
				if (true)
				{
				}
				if (false)
				{
				}
				throw new ApplicationException(RecordTableEnumerator.b("簹倻儽⼿ぁ摃╅⥇⑉≋⅍⑏牑㙓㍕硗⥙⥛⹝ၟൡᙣብ൧๩䱫࡭Ὧq味ɵၷό屻ᵽꢇﺉﺍ", a_));
				IL_E1:
				IL_E3:
				return this.ᝠ;
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
				this.ᝠ = (XlsChartWallOrFloor)value;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x000672E4 File Offset: 0x000662E4
		public IChartDataTable DataTable
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
				return this.\u1754;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x00067328 File Offset: 0x00066328
		// (set) Token: 0x06000A84 RID: 2692 RVA: 0x0006736C File Offset: 0x0006636C
		public bool HasDataTable
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
				return this.ᝌ;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (value)
						{
							num = 6;
							continue;
						}
						goto IL_67;
					case 2:
						num = 0;
						continue;
					case 3:
						return;
					case 4:
						goto IL_67;
					case 5:
						this.\u1754 = (value ? new ChartDataTableXls(base.AppImplementation, this) : null);
						if (true)
						{
						}
						num = 3;
						continue;
					case 6:
						this.ᜑ();
						num = 4;
						continue;
					}
					goto IL_2C;
					for (;;)
					{
						IL_67:
						this.ᝌ = value;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_84;
						}
					}
					IL_84:
					if (false)
					{
					}
					num = 5;
					continue;
					IL_2C:
					if (this.ᝌ == value)
					{
						break;
					}
					num = 2;
				}
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x0006744C File Offset: 0x0006644C
		public IChartLegend Legend
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
				return this.\u175D;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x00067490 File Offset: 0x00066490
		// (set) Token: 0x06000A87 RID: 2695 RVA: 0x000674D4 File Offset: 0x000664D4
		public bool HasLegend
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
				return this.\u175E;
			}
			set
			{
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
							break;
						default:
							if (false)
							{
							}
							this.\u175E = value;
							break;
						}
						num = 3;
						continue;
					case 3:
						this.\u175D = (value ? new ChartLegend((spr\u2158)base.ReservedHandle, this) : null);
						if (true)
						{
						}
						num = 0;
						continue;
					}
					if (this.\u175E == value)
					{
						break;
					}
					num = 1;
				}
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000A88 RID: 2696 RVA: 0x00067580 File Offset: 0x00066580
		// (set) Token: 0x06000A89 RID: 2697 RVA: 0x000675C8 File Offset: 0x000665C8
		public ChartPlotEmptyType DisplayBlanksAs
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
				return this.\u1755.ᜀ();
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
				this.\u1755.ᜀ(value);
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000A8A RID: 2698 RVA: 0x00067610 File Offset: 0x00066610
		// (set) Token: 0x06000A8B RID: 2699 RVA: 0x00067658 File Offset: 0x00066658
		public bool PlotVisibleOnly
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
				return this.\u1755.ᜇ();
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
				this.\u1755.ᜄ(value);
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x000676A0 File Offset: 0x000666A0
		// (set) Token: 0x06000A8D RID: 2701 RVA: 0x000676F8 File Offset: 0x000666F8
		public bool SizeWithWindow
		{
			get
			{
				if (this.ᝇ)
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
						return true;
					}
				}
				if (true)
				{
				}
				return !this.\u1755.ᜅ();
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6B;
					case 2:
						for (;;)
						{
							this.\u1755.ᜂ(!value);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_5D;
							}
						}
						IL_5D:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					if (this.ᝇ)
					{
						return;
					}
					num = 2;
				}
				IL_6B:
				if (true)
				{
				}
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x0006777C File Offset: 0x0006677C
		public bool SupportWallsAndFloor
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
				return Array.IndexOf<ExcelChartType>(XlsChart.ᜨ, this.ChartType) >= 0;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x000677D0 File Offset: 0x000667D0
		public override bool ProtectDrawingObjects
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
				return (this.InnerProtection & SheetProtectionType.Objects) != SheetProtectionType.None;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x0006781C File Offset: 0x0006681C
		public override bool ProtectScenarios
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
				return (this.InnerProtection & SheetProtectionType.Scenarios) != SheetProtectionType.None;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x00067868 File Offset: 0x00066868
		public override SheetProtectionType Protection
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
				return base.Protection & ~SheetProtectionType.Scenarios;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x000678AC File Offset: 0x000668AC
		// (set) Token: 0x06000A93 RID: 2707 RVA: 0x000678F0 File Offset: 0x000668F0
		public override ExcelColors TabKnownColor
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
				return base.TabKnownColor;
			}
			set
			{
				if (!this.ᝇ)
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
						base.TabKnownColor = value;
						return;
					}
				}
				if (true)
				{
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x00067944 File Offset: 0x00066944
		public bool IsCategoryAxisAvail
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
				return Array.IndexOf<ExcelChartType>(XlsChart.\u1738, this.ChartType) == -1;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x00067994 File Offset: 0x00066994
		public bool IsValueAxisAvail
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
				return Array.IndexOf<ExcelChartType>(XlsChart.\u1738, this.ChartType) == -1;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x000679E4 File Offset: 0x000669E4
		public bool IsSeriesAxisAvail
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
							goto IL_32;
						default:
							goto IL_5A;
						}
						break;
					case 2:
						num = 3;
						continue;
					case 3:
						goto IL_78;
					}
					if (!this.HasPivotTable)
					{
						num = 2;
						continue;
					}
					IL_32:
					num = 1;
				}
				IL_5A:
				if (true)
				{
				}
				if (false)
				{
				}
				ExcelChartType excelChartType = this.PivotChartType;
				goto IL_80;
				IL_78:
				excelChartType = this.ChartType;
				IL_80:
				ExcelChartType value = excelChartType;
				return Array.IndexOf<ExcelChartType>(XlsChart.DEF_SUPPORT_SERIES_AXIS, value) != -1;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x00067A84 File Offset: 0x00066A84
		public bool IsStacked
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
				return XlsChart.ᜃ(this.ChartType);
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x00067ACC File Offset: 0x00066ACC
		public bool IsChart_100
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
				return XlsChart.ᜄ(this.ChartType);
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x00067B14 File Offset: 0x00066B14
		public bool IsChart3D
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
				return Array.IndexOf<ExcelChartType>(XlsChart.\u1735, this.ChartType) != -1;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x00067B68 File Offset: 0x00066B68
		public bool IsPivot3DChart
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
				return Array.IndexOf<ExcelChartType>(XlsChart.\u1732, this.PivotChartType) != -1;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x00067BBC File Offset: 0x00066BBC
		public bool IsChartLine
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
				return Array.IndexOf<ExcelChartType>(XlsChart.\u1736, this.ChartType) != -1;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x00067C10 File Offset: 0x00066C10
		public bool NeedDataFormat
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 13;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_11C;
						default:
							if (false)
							{
							}
							if (this.ChartType != ExcelChartType.Bubble3D)
							{
								num = 11;
								continue;
							}
							goto IL_E3;
						}
						break;
					case 3:
						if (!this.IsChartStock)
						{
							num = 9;
							continue;
						}
						goto IL_E3;
					case 4:
						if (!this.IsChartLine)
						{
							num = 8;
							continue;
						}
						goto IL_E3;
					case 5:
						goto IL_11C;
					case 6:
						num = 3;
						continue;
					case 7:
						if (!this.IsChartExploded)
						{
							num = 0;
							continue;
						}
						goto IL_E3;
					case 8:
						num = 7;
						continue;
					case 9:
						num = 1;
						continue;
					case 10:
						goto IL_13B;
					case 11:
						num = 5;
						continue;
					case 12:
						num = 4;
						continue;
					case 13:
						if (!this.IsChartScatter)
						{
							num = 6;
							continue;
						}
						goto IL_E3;
					}
					if (true)
					{
					}
					if (!this.IsChart3D)
					{
						num = 12;
						continue;
					}
					break;
					IL_11C:
					if (this.ChartType != ExcelChartType.Radar)
					{
						return false;
					}
					num = 10;
				}
				IL_E3:
				return this.ChartType != ExcelChartType.SurfaceContourNoColor;
				IL_13B:
				goto IL_E3;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x00067D80 File Offset: 0x00066D80
		public bool NeedMarkerFormat
		{
			get
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
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (!this.IsChartCone)
							{
								num = 1;
								continue;
							}
							return true;
						case 1:
							goto IL_7F;
						case 2:
							num = 0;
							continue;
						}
						if (true)
						{
						}
						if (this.IsChartPyramid)
						{
							break;
						}
						num = 2;
					}
					return true;
				}
				}
				IL_7F:
				return this.IsChartCylinder;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x00067E10 File Offset: 0x00066E10
		public bool IsChartBar
		{
			get
			{
				int a_ = 8;
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ChartType.ToString().IndexOf(RecordTableEnumerator.b("簽ℿぁ", a_)) != -1;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x00067E80 File Offset: 0x00066E80
		public bool IsChartPyramid
		{
			get
			{
				int a_ = 14;
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ChartType.ToString().IndexOf(RecordTableEnumerator.b("ᑃ㽅㩇⭉⅋❍㑏", a_)) != -1;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x00067EF0 File Offset: 0x00066EF0
		public bool IsChartCone
		{
			get
			{
				int a_ = 11;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ChartType.ToString().IndexOf(RecordTableEnumerator.b("ɀⱂ⭄≆", a_)) != -1;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x00067F60 File Offset: 0x00066F60
		public bool IsChartCylinder
		{
			get
			{
				int a_ = 9;
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ChartType.ToString().IndexOf(RecordTableEnumerator.b("簾㡀⽂ⱄ⥆ⵈ⹊㽌", a_)) != -1;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x00067FD0 File Offset: 0x00066FD0
		public bool IsChartBubble
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
				return Array.IndexOf<ExcelChartType>(XlsChart.\u1737, this.ChartType) != -1;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x00068024 File Offset: 0x00067024
		public bool IsChartDoughnut
		{
			get
			{
				int a_ = 6;
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ChartType.ToString().IndexOf(RecordTableEnumerator.b("砻儽㔿╁ⱃ⡅㵇㹉", a_)) != -1;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x00068094 File Offset: 0x00067094
		public bool IsChartVeryColor
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
				return Array.IndexOf<ExcelChartType>(XlsChart.\u1739, this.ChartType) != -1;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x000680E8 File Offset: 0x000670E8
		public bool IsChartExploded
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
				return Array.IndexOf<ExcelChartType>(XlsChart.\u173A, this.ChartType) != -1;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x0006813C File Offset: 0x0006713C
		public bool IsSeriesLines
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
				return this.CanChartHaveSeriesLines;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x00068180 File Offset: 0x00067180
		public bool CanChartHaveSeriesLines
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
				return Array.IndexOf<ExcelChartType>(XlsChart.\u173B, this.ChartType) != -1;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x000681D4 File Offset: 0x000671D4
		public bool IsChartScatter
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
				return Array.IndexOf<ExcelChartType>(XlsChart.\u173C, this.ChartType) != -1;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x00068228 File Offset: 0x00067228
		public ChartLinePatternType DefaultLinePattern
		{
			get
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_74;
				default:
				{
					if (false)
					{
					}
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_74;
						case 1:
							num = 2;
							continue;
						case 2:
							if (this.IsChartStock)
							{
								num = 0;
								continue;
							}
							return ChartLinePatternType.Solid;
						}
						if (this.ChartType == ExcelChartType.ScatterMarkers)
						{
							return ChartLinePatternType.None;
						}
						num = 1;
					}
					return ChartLinePatternType.Solid;
				}
				}
				return ChartLinePatternType.None;
				IL_74:
				if (true)
				{
				}
				return ChartLinePatternType.None;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x000682B4 File Offset: 0x000672B4
		public bool IsChartSmoothedLine
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
				return Array.IndexOf<ExcelChartType>(XlsChart.\u173D, this.ChartType) != -1;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x00068308 File Offset: 0x00067308
		public bool IsChartStock
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
				return Array.IndexOf<ExcelChartType>(XlsChart.\u173E, this.ChartType) != -1;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x0006835C File Offset: 0x0006735C
		public bool NeedDropBar
		{
			get
			{
				if (this.ChartType != ExcelChartType.StockOpenHighLowClose)
				{
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_22;
						}
					}
					IL_22:
					if (true)
					{
					}
					if (false)
					{
					}
					return this.ChartType == ExcelChartType.StockVolumeOpenHighLowClose;
				}
				return true;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000AAD RID: 2733 RVA: 0x000683B0 File Offset: 0x000673B0
		public bool IsChartVolume
		{
			get
			{
				if (this.ChartType != ExcelChartType.StockVolumeHighLowClose)
				{
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_22;
						}
					}
					IL_22:
					if (true)
					{
					}
					if (false)
					{
					}
					return this.ChartType == ExcelChartType.StockVolumeOpenHighLowClose;
				}
				return true;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x00068404 File Offset: 0x00067404
		public bool IsPerspective
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
				return Array.IndexOf<ExcelChartType>(XlsChart.\u173F, this.ChartType) != -1;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x00068458 File Offset: 0x00067458
		public bool IsClustered
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
				return XlsChart.ᜅ(this.ChartType);
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x000684A0 File Offset: 0x000674A0
		public bool NoPlotArea
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (!this.IsChartPie)
						{
							num = 5;
							continue;
						}
						return true;
					case 2:
						goto IL_8E;
					case 3:
						if (this.IsChartDoughnut)
						{
							return true;
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
							num = 2;
							continue;
						}
						break;
					case 4:
						num = 0;
						continue;
					case 5:
						num = 3;
						continue;
					}
					IL_28:
					if (true)
					{
					}
					if (!this.IsChartRadar)
					{
						num = 4;
						continue;
					}
					return true;
					goto IL_28;
				}
				IL_8E:
				return this.ChartType == ExcelChartType.SurfaceContourNoColor;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x00068560 File Offset: 0x00067560
		public bool IsChartRadar
		{
			get
			{
				int a_ = 1;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ChartType.ToString().StartsWith(RecordTableEnumerator.b("收堸强尼䴾", a_));
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x000685C8 File Offset: 0x000675C8
		public bool IsChartPie
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
				return XlsChart.GetIsChartPie(this.ChartType);
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x00068610 File Offset: 0x00067610
		public bool IsChartWalls
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

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x0006864C File Offset: 0x0006764C
		public bool IsChartFloor
		{
			get
			{
				if (ExcelChartType.SurfaceContourNoColor != this.ChartType)
				{
					for (;;)
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
							goto IL_2A;
						}
					}
					IL_2A:
					if (false)
					{
					}
					return ExcelChartType.SurfaceContour == this.ChartType;
				}
				return true;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x000686A0 File Offset: 0x000676A0
		internal List<int> SerializedAxisIds
		{
			get
			{
				for (;;)
				{
					if (true)
					{
					}
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							this.\u1779 = new List<int>();
							num = 1;
							continue;
						case 1:
							goto IL_53;
						}
						if (this.\u1779 != null)
						{
							goto IL_71;
						}
						num = 0;
					}
					IL_53:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_69;
					}
				}
				IL_69:
				if (false)
				{
				}
				IL_71:
				return this.\u1779;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x00068724 File Offset: 0x00067724
		public bool IsSecondaryCategoryAxisAvail
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
				return this.SecondaryCategoryAxis != null;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x0006876C File Offset: 0x0006776C
		public bool IsSecondaryValueAxisAvail
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
				return this.SecondaryValueAxis != null;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x000687B4 File Offset: 0x000677B4
		// (set) Token: 0x06000AB9 RID: 2745 RVA: 0x00068844 File Offset: 0x00067844
		public bool IsSecondaryAxes
		{
			get
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
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_77;
						case 1:
							if (!this.IsSecondaryCategoryAxisAvail)
							{
								num = 0;
								continue;
							}
							goto IL_79;
						case 2:
							num = 1;
							continue;
						}
						if (this.IsSecondaryValueAxisAvail)
						{
							break;
						}
						num = 2;
					}
					IL_79:
					if (true)
					{
					}
					return true;
				}
				}
				IL_77:
				return this.ᝆ;
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
				this.ᝆ = value;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x00068888 File Offset: 0x00067888
		public bool IsSpecialDataLabels
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
				return Array.IndexOf<ExcelChartType>(XlsChart.ᝄ, this.ChartType) != -1;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x000688DC File Offset: 0x000678DC
		public bool CanChartPercentageLabel
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
				return Array.IndexOf<ExcelChartType>(XlsChart.ᝅ, this.ChartType) != -1;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x00068930 File Offset: 0x00067930
		public bool CanChartBubbleLabel
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
				return this.IsChartBubble;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x00068974 File Offset: 0x00067974
		// (set) Token: 0x06000ABE RID: 2750 RVA: 0x000689BC File Offset: 0x000679BC
		public bool IsManuallyFormatted
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
				return this.\u1755.ᜃ();
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
				this.\u1755.ᜃ(value);
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x00068A04 File Offset: 0x00067A04
		private sprᥦ PlotGrowth
		{
			get
			{
				for (;;)
				{
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_55;
						case 2:
							this.\u1756 = (sprᥦ)spr\u175E.ᜀ(TBIFFRecord.ChartPlotGrowth);
							num = 0;
							continue;
						}
						if (this.\u1756 != null)
						{
							goto IL_7B;
						}
						num = 2;
					}
					IL_55:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_6B;
					}
				}
				IL_6B:
				if (true)
				{
				}
				if (false)
				{
				}
				IL_7B:
				return this.\u1756;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x00068A94 File Offset: 0x00067A94
		private spr\u23BE PlotAreaBoundingBox
		{
			get
			{
				for (;;)
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
							this.\u1757 = (spr\u23BE)spr\u175E.ᜀ(TBIFFRecord.ChartPos);
							this.\u1757.ᜀ(2);
							this.\u1757.ᜁ(2);
							num = 2;
							continue;
						case 2:
							goto IL_75;
						}
						if (this.\u1757 != null)
						{
							goto IL_93;
						}
						num = 0;
					}
					IL_75:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_8B;
					}
				}
				IL_8B:
				if (false)
				{
				}
				IL_93:
				return this.\u1757;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x00068B3C File Offset: 0x00067B3C
		protected internal XlsWorkbook InnerWorkbook
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
				return this.m_book;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x00068B80 File Offset: 0x00067B80
		protected internal XlsChartFrameFormat InnerXlsChartArea
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
				return this.ChartArea as XlsChartFrameFormat;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x00068BC8 File Offset: 0x00067BC8
		protected internal XlsChartFrameFormat InnerPlotArea
		{
			get
			{
				for (;;)
				{
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_5A;
						case 1:
							this.\u1759 = new XlsChartFrameFormat(base.ReservedHandle, this);
							num = 0;
							continue;
						case 2:
							if (true)
							{
							}
							break;
						}
						if (this.\u1759 != null)
						{
							goto IL_78;
						}
						num = 1;
					}
					IL_5A:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_70;
					}
				}
				IL_70:
				if (false)
				{
				}
				IL_78:
				return this.\u1759;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x00068C54 File Offset: 0x00067C54
		public string ChartStartType
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
				return XlsChartFormat.ᜉ(this.ChartType);
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x00068C9C File Offset: 0x00067C9C
		internal override XlsPageSetupBase PageSetupBase
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
				return this.ᝍ;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x00068CE0 File Offset: 0x00067CE0
		internal spr\u2140 ChartProperties
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
				return this.\u1755;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x00068D24 File Offset: 0x00067D24
		public bool Loading
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
				return this.m_book.Loading;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x00068D6C File Offset: 0x00067D6C
		internal XlsChartFormat XlsChartFormat
		{
			get
			{
				int a_ = 1;
				if (this.\u1753.Count == 0)
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
					if (true)
					{
					}
					if (false)
					{
					}
					throw new ApplicationException(RecordTableEnumerator.b("琶儸娺似䬾慀╂⩄㕆⑈⩊㥌潎㉐㉒㭔㥖㙘⽚絜㵞Ѡ䍢ͤࡦᱨժ६䅮", a_));
				}
				XlsChartSerie xlsChartSerie = (XlsChartSerie)this.\u1753[0];
				return xlsChartSerie.GetCommonSerieFormat();
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x00068DEC File Offset: 0x00067DEC
		// (set) Token: 0x06000ACA RID: 2762 RVA: 0x00068E30 File Offset: 0x00067E30
		public bool TypeChanging
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
				return this.ᝢ;
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
				this.ᝢ = value;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x00068E74 File Offset: 0x00067E74
		// (set) Token: 0x06000ACC RID: 2764 RVA: 0x00068EB8 File Offset: 0x00067EB8
		public ExcelChartType DestinationType
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
				return this.ᝣ;
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
				this.ᝣ = value;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x00068EFC File Offset: 0x00067EFC
		internal RelationsCollection Relations
		{
			get
			{
				for (;;)
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
							this.ᝧ = new RelationsCollection();
							num = 1;
							continue;
						case 1:
							goto IL_53;
						}
						if (this.ᝧ != null)
						{
							goto IL_71;
						}
						num = 0;
					}
					IL_53:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_69;
					}
				}
				IL_69:
				if (false)
				{
				}
				IL_71:
				return this.ᝧ;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x00068F80 File Offset: 0x00067F80
		// (set) Token: 0x06000ACF RID: 2767 RVA: 0x00068FC4 File Offset: 0x00067FC4
		public int Style
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
				return this.ᝨ;
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
				this.ᝨ = value;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x00069008 File Offset: 0x00068008
		public bool HasFloor
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
				return this.ᝠ != null;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x00069050 File Offset: 0x00068050
		public bool HasWalls
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
				return this.\u175F != null;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x00069098 File Offset: 0x00068098
		// (set) Token: 0x06000AD3 RID: 2771 RVA: 0x000690DC File Offset: 0x000680DC
		public Stream PivotFormatsStream
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
				return this.ᝩ;
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
				this.ᝩ = value;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x00069120 File Offset: 0x00068120
		// (set) Token: 0x06000AD5 RID: 2773 RVA: 0x00069164 File Offset: 0x00068164
		public bool ZoomToFit
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
				return this.SizeWithWindow;
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
				this.SizeWithWindow = value;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x000691A8 File Offset: 0x000681A8
		protected override SheetProtectionType DefaultProtectionOptions
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
				return SheetProtectionType.Objects | SheetProtectionType.Scenarios | SheetProtectionType.Content;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x000691E8 File Offset: 0x000681E8
		public bool IsEmbeded
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
				return this.ᝇ;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x0006922C File Offset: 0x0006822C
		public int DefaultTextIndex
		{
			get
			{
				switch (0)
				{
				default:
				{
					int result;
					for (;;)
					{
						result = 0;
						int num = 2;
						for (;;)
						{
							List<BiffRecordRaw>.Enumerator enumerator;
							List<BiffRecordRaw> byIndex;
							switch (num)
							{
							case 0:
								try
								{
									num = 5;
									for (;;)
									{
										BiffRecordRaw biffRecordRaw;
										switch (num)
										{
										case 0:
											if (!enumerator.MoveNext())
											{
												num = 3;
												continue;
											}
											biffRecordRaw = enumerator.Current;
											num = 6;
											continue;
										case 1:
											goto IL_100;
										case 2:
											goto IL_10C;
										case 3:
											goto IL_100;
										case 4:
											goto IL_C0;
										case 5:
											switch ((1 == 1) ? 1 : 0)
											{
											case 0:
											case 2:
												goto IL_C0;
											default:
												if (false)
												{
												}
												break;
											}
											break;
										case 6:
											if (biffRecordRaw.TypeCode == TBIFFRecord.ChartFontx)
											{
												num = 4;
												continue;
											}
											break;
										}
										IL_A3:
										num = 0;
										continue;
										goto IL_A3;
										IL_C0:
										result = (int)((spr\u2241)biffRecordRaw).ᜀ();
										num = 1;
										continue;
										IL_100:
										num = 2;
									}
									IL_10C:
									return result;
								}
								finally
								{
									((IDisposable)enumerator).Dispose();
								}
								goto IL_11F;
							case 1:
								byIndex = this.\u175A.GetByIndex(0);
								num = 5;
								continue;
							case 2:
								if (this.\u175A != null)
								{
									num = 4;
									continue;
								}
								return result;
							case 3:
								if (this.\u175A.Count > 0)
								{
									num = 1;
									continue;
								}
								return result;
							case 4:
								num = 3;
								continue;
							case 5:
								if (true)
								{
								}
								if (byIndex != null)
								{
									num = 6;
									continue;
								}
								return result;
							case 6:
								goto IL_11F;
							}
							break;
							IL_11F:
							enumerator = byIndex.GetEnumerator();
							num = 0;
						}
					}
					return result;
				}
				}
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x000693E8 File Offset: 0x000683E8
		// (set) Token: 0x06000ADA RID: 2778 RVA: 0x0006942C File Offset: 0x0006842C
		internal Stream PreservedBandFormats
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
				return this.ᝬ;
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
				this.ᝬ = value;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x00069470 File Offset: 0x00068470
		internal bool HasTitle
		{
			get
			{
				bool result;
				for (;;)
				{
					result = false;
					int num = 9;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 7;
							continue;
						case 1:
							IL_129:
							num = 4;
							continue;
						case 2:
							goto IL_DB;
						case 3:
							if (this.m_title.FontIndex != 0)
							{
								num = 1;
								continue;
							}
							goto IL_DB;
						case 4:
							if (this.m_title.TextRecord.ᜉ())
							{
								if (true)
								{
								}
								num = 10;
								continue;
							}
							goto IL_DB;
						case 5:
							num = 3;
							continue;
						case 6:
							if (!this.m_title.TextRecord.ᜄ())
							{
								num = 2;
								continue;
							}
							return result;
						case 7:
							if (this.m_title.Text == null)
							{
								num = 5;
								continue;
							}
							goto IL_DB;
						case 8:
							return result;
						case 9:
							if (this.m_title != null)
							{
								num = 0;
								continue;
							}
							return result;
						case 10:
							num = 6;
							continue;
						}
						break;
						IL_DB:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_129;
						default:
							if (false)
							{
							}
							result = true;
							num = 8;
							break;
						}
					}
				}
				return result;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x000695AC File Offset: 0x000685AC
		// (set) Token: 0x06000ADD RID: 2781 RVA: 0x000695F0 File Offset: 0x000685F0
		internal Stream AlternateContent
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
				return this.\u1774;
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
				this.\u1774 = value;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x00069634 File Offset: 0x00068634
		public bool HasChartTitle
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
				return this.m_title != null;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x0006967C File Offset: 0x0006867C
		// (set) Token: 0x06000AE0 RID: 2784 RVA: 0x000696C0 File Offset: 0x000686C0
		internal Stream DefaultTextProperty
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
				return this.\u1776;
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
				this.\u1776 = value;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x00069704 File Offset: 0x00068704
		public FontWrapper Font
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_87;
					case 2:
					{
						if (true)
						{
						}
						XlsFont font = (XlsFont)base.ParentWorkbook.InnerFonts[0];
						this.\u1777 = new FontWrapper(font);
						num = 1;
						continue;
					}
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
						if (this.\u1777 != null)
						{
							goto IL_89;
						}
						break;
					}
					num = 2;
				}
				IL_87:
				IL_89:
				return this.\u1777;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x000697A0 File Offset: 0x000687A0
		// (set) Token: 0x06000AE3 RID: 2787 RVA: 0x000697E4 File Offset: 0x000687E4
		internal bool? HasAutoTitle
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
				return this.\u1778;
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
				this.\u1778 = value;
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x00069828 File Offset: 0x00068828
		// (set) Token: 0x06000AE5 RID: 2789 RVA: 0x0006986C File Offset: 0x0006886C
		internal Stream DropLinesStream
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
				return this.\u177A;
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
				this.\u177A = value;
			}
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x000698B0 File Offset: 0x000688B0
		internal new static bool ᜅ(ExcelChartType A_0)
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
			return Array.IndexOf<ExcelChartType>(XlsChart.ᝀ, A_0) >= 0;
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x000698FC File Offset: 0x000688FC
		internal new static bool ᜄ(ExcelChartType A_0)
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
			return Array.IndexOf<ExcelChartType>(XlsChart.\u1733, A_0) >= 0;
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x00069948 File Offset: 0x00068948
		internal new static bool ᜃ(ExcelChartType A_0)
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
			return Array.IndexOf<ExcelChartType>(XlsChart.\u1734, A_0) >= 0;
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x00069994 File Offset: 0x00068994
		public static bool GetIsChartPie(ExcelChartType chartType)
		{
			int a_ = 1;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return chartType.ToString().StartsWith(RecordTableEnumerator.b("朶倸帺", a_));
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x000699F8 File Offset: 0x000689F8
		public void CreateNecessaryAxes(bool bPrimary)
		{
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.\u175B.ᜃ() == null)
					{
						num = 3;
						continue;
					}
					goto IL_233;
				case 1:
					return;
				case 2:
					if (this.\u175C.ᜂ() == null)
					{
						num = 15;
						continue;
					}
					return;
				case 3:
					this.\u175B.ᜀ(new ChartValueAxis((spr\u2158)base.AppImplementation, this.\u175B, AxisType.Value));
					num = 17;
					continue;
				case 4:
					if (this.IsCategoryAxisAvail)
					{
						num = 8;
						continue;
					}
					goto IL_263;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_276;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 6:
					num = 0;
					continue;
				case 7:
					if (this.\u175B.ᜂ() == null)
					{
						num = 11;
						continue;
					}
					goto IL_263;
				case 8:
					if (true)
					{
					}
					num = 7;
					continue;
				case 9:
					goto IL_263;
				case 10:
					num = 18;
					continue;
				case 11:
					this.\u175B.ᜀ(new ChartCategoryAxis((spr\u2158)base.AppImplementation, this.\u175B, AxisType.Category));
					num = 9;
					continue;
				case 12:
					if (this.IsSeriesAxisAvail)
					{
						num = 10;
						continue;
					}
					return;
				case 13:
					goto IL_AE;
				case 14:
					num = 4;
					continue;
				case 15:
					this.\u175C.ᜀ(new ChartCategoryAxis((spr\u2158)base.AppImplementation, this.\u175C, AxisType.Category, false));
					this.\u175C.ᜀ(new ChartValueAxis((spr\u2158)base.AppImplementation, this.\u175C, AxisType.Value, false));
					num = 1;
					continue;
				case 16:
					if (this.IsValueAxisAvail)
					{
						goto IL_276;
					}
					goto IL_233;
				case 17:
					goto IL_233;
				case 18:
					if (this.\u175B.ᜅ() == null)
					{
						num = 13;
						continue;
					}
					return;
				}
				if (bPrimary)
				{
					num = 14;
					continue;
				}
				num = 2;
				continue;
				IL_233:
				num = 12;
				continue;
				IL_263:
				num = 16;
				continue;
				IL_276:
				num = 6;
			}
			IL_AE:
			this.\u175B.ᜀ(new ChartSeriesAxis((spr\u2158)base.AppImplementation, this.\u175B, AxisType.Serie));
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00069C8C File Offset: 0x00068C8C
		protected override void InitializeCollections()
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
			base.InitializeCollections();
			this.\u1752 = new List<spr\u1F17>();
			this.\u1753 = new ChartSeries((spr\u2158)base.ReservedHandle, this);
			this.ᝍ = new ChartPageSetup((spr\u2158)base.ReservedHandle, this);
			this.\u175B = new spr\u21CD((spr\u2158)base.ReservedHandle, this);
			this.\u175C = new spr\u21CD((spr\u2158)base.ReservedHandle, this, false);
			this.\u175B.ᜉ();
			this.\u175C.ᜁ(false);
			this.ᜐ();
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00069D54 File Offset: 0x00068D54
		private void ᜑ()
		{
			switch (0)
			{
			default:
			{
				int num = 1;
				for (;;)
				{
					int num2;
					int count;
					switch (num)
					{
					case 0:
						goto IL_C7;
					case 2:
						goto IL_C7;
					case 3:
						return;
					case 4:
						goto IL_55;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							if (num2 < count)
							{
								XlsChartSerie xlsChartSerie = this.Series[num2] as XlsChartSerie;
								string startType = XlsChartFormat.ᜉ(xlsChartSerie.SerieType);
								XlsChart.CheckDataTablePossibility(startType, true);
								num2++;
								num = 2;
								continue;
							}
							break;
						}
						num = 3;
						continue;
					}
					if (this.ChartType != ExcelChartType.CombinationChart)
					{
						num = 4;
						continue;
					}
					num2 = 0;
					count = this.Series.Count;
					num = 0;
					continue;
					IL_C7:
					num = 5;
				}
				IL_55:
				if (true)
				{
				}
				string startType2 = XlsChartFormat.ᜉ(this.ChartType);
				XlsChart.CheckDataTablePossibility(startType2, true);
				return;
			}
			}
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00069E68 File Offset: 0x00068E68
		public static bool CheckDataTablePossibility(string startType, bool bThrowException)
		{
			int a_ = 2;
			for (;;)
			{
				bool flag = Array.IndexOf<string>(XlsChart.ᜧ, startType) != -1;
				if (true)
				{
				}
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (bThrowException)
						{
							num = 3;
							continue;
						}
						return flag;
					case 1:
						if (!flag)
						{
							num = 2;
							continue;
						}
						return flag;
					case 2:
						for (;;)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_84;
							}
						}
						IL_84:
						if (false)
						{
						}
						num = 0;
						continue;
					case 3:
						goto IL_A3;
					}
					break;
				}
			}
			IL_A3:
			throw new NotSupportedException(RecordTableEnumerator.b("簷嬹䠻弽怿㙁╃⑅⑇⽉汋⩍㽏㝑❓癕㙗㕙⡛繝፟ᝡᑣ॥ᩧṩ५੭偯᭱ᩳ噵౷ቹᕻൽꁿ慎ﺉ겋揄", a_));
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00069F1C File Offset: 0x00068F1C
		private void ᜐ()
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
			List<BiffRecordRaw> list = new List<BiffRecordRaw>();
			spr\u20B6 spr_u20B = (spr\u20B6)spr\u175E.ᜀ(TBIFFRecord.ChartText);
			spr_u20B.ᜁ(true);
			spr_u20B.ᜉ(true);
			spr_u20B.ᜀ(ChartHorzAlignmentType.Center);
			spr_u20B.ᜀ(ChartVertAlignmentType.Center);
			list.Add(spr_u20B);
			list.Add(spr\u175E.ᜀ(TBIFFRecord.Begin));
			spr\u23BE spr_u23BE = (spr\u23BE)spr\u175E.ᜀ(TBIFFRecord.ChartPos);
			spr_u23BE.ᜁ(2);
			spr_u23BE.ᜀ(2);
			list.Add(spr_u23BE);
			spr\u2241 spr_u = (spr\u2241)spr\u175E.ᜀ(TBIFFRecord.ChartFontx);
			list.Add(spr_u);
			sprᢀ sprᢀ = (sprᢀ)spr\u175E.ᜀ(TBIFFRecord.ChartAI);
			sprᢀ.ᜀ(sprᢀ.ReferenceType.EnteredDirectly);
			list.Add(sprᢀ);
			list.Add(spr\u175E.ᜀ(TBIFFRecord.End));
			this.\u175A.Add(2, list);
			list = new List<BiffRecordRaw>();
			list.Add((BiffRecordRaw)spr_u20B.Clone());
			list.Add(spr\u175E.ᜀ(TBIFFRecord.Begin));
			list.Add((BiffRecordRaw)spr_u23BE.Clone());
			spr_u = (spr\u2241)spr_u.Clone();
			list.Add(spr_u);
			list.Add((BiffRecordRaw)sprᢀ.Clone());
			list.Add(spr\u175E.ᜀ(TBIFFRecord.End));
			this.\u175A.Add(3, list);
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0006A094 File Offset: 0x00069094
		private new void ᜂ(ExcelChartType A_0)
		{
			int a_ = 17;
			switch (0)
			{
			default:
			{
				int num = 8;
				IXLSRange a_2;
				int a_3;
				IXLSRange ixlsrange2;
				for (;;)
				{
					IXLSRange ixlsrange;
					int num2;
					switch (num)
					{
					case 0:
						if (!this.ᝋ)
						{
							num = 1;
							continue;
						}
						num = 9;
						continue;
					case 1:
						num = 12;
						continue;
					case 2:
						if (!this.ᜀ(a_2, A_0))
						{
							num = 5;
							continue;
						}
						this.PrimaryCategoryAxis.CategoryLabels = ixlsrange;
						a_3 = 0;
						num = 3;
						continue;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D8;
						default:
							if (false)
							{
							}
							if (ixlsrange2 != null)
							{
								num = 4;
								continue;
							}
							goto IL_1CF;
						}
						break;
					case 4:
						num = 7;
						continue;
					case 5:
						goto IL_FD;
					case 6:
						goto IL_16F;
					case 7:
						if (ixlsrange != null)
						{
							num = 10;
							continue;
						}
						goto IL_1CF;
					case 9:
						num2 = ixlsrange.LastRow - ixlsrange.Row + 1;
						goto IL_162;
					case 10:
						num = 0;
						continue;
					case 11:
						goto IL_6E;
					case 12:
						num2 = ixlsrange.LastColumn - ixlsrange.Column + 1;
						goto IL_162;
					}
					if (this.ᝊ == null)
					{
						num = 11;
						continue;
					}
					ixlsrange2 = this.ᜀ(this.ᝊ, this.ᝋ, out a_2);
					ixlsrange = this.ᜀ(a_2, !this.ᝋ, out a_2);
					IL_D8:
					num = 2;
					continue;
					IL_162:
					a_3 = num2;
					num = 6;
				}
				IL_6E:
				if (true)
				{
				}
				this.\u1753.Clear();
				return;
				IL_FD:
				throw new ApplicationException(RecordTableEnumerator.b("͆⡈㽊ⱌ潎⍐㉒㭔ざ㱘筚⹜㩞ᕠ䍢ͤ٦hݪ࡬୮彰", a_));
				IL_16F:
				IL_1CF:
				this.ᜀ(a_2, ixlsrange2, XlsChartFormat.ᜉ(A_0), a_3);
				return;
			}
			}
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0006A280 File Offset: 0x00069280
		private void ᜏ()
		{
			for (;;)
			{
				int num = this.ᝊ.Row;
				int lastRow = this.ᝊ.LastRow;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_3A;
					case 1:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_42;
						default:
							goto IL_76;
						}
						break;
					case 2:
						goto IL_42;
					case 3:
						goto IL_3A;
					}
					break;
					IL_3A:
					num2 = 2;
					continue;
					IL_42:
					if (num > lastRow)
					{
						num2 = 1;
					}
					else
					{
						XlsChartSerie xlsChartSerie = (XlsChartSerie)this.Series.Add();
						xlsChartSerie.Values = this.ᝊ[num, this.ᝊ.Column, num, this.ᝊ.LastColumn];
						xlsChartSerie.ValueRangeChanged += this.ᜀ;
						num++;
						num2 = 0;
					}
				}
			}
			IL_76:
			if (false)
			{
			}
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0006A36C File Offset: 0x0006936C
		private void \u170D()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					int row = this.ᝊ.Row;
					int lastRow = this.ᝊ.LastRow;
					int lastColumn = this.ᝊ.LastColumn;
					int num = this.ᝊ.Column;
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_6C;
							default:
								goto IL_A0;
							}
							break;
						case 1:
						{
							if (num > lastColumn)
							{
								if (true)
								{
								}
								num2 = 0;
								continue;
							}
							XlsChartSerie xlsChartSerie = (XlsChartSerie)this.Series.Add();
							xlsChartSerie.Values = this.ᝊ[row, num, lastRow, num];
							xlsChartSerie.ValueRangeChanged += this.ᜀ;
							num++;
							num2 = 3;
							continue;
						}
						case 2:
							goto IL_6C;
						case 3:
							goto IL_6C;
						}
						break;
						IL_6C:
						num2 = 1;
					}
				}
				IL_A0:
				if (false)
				{
				}
				return;
			}
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0006A474 File Offset: 0x00069474
		private void ᜌ()
		{
			if (true)
			{
			}
			switch (0)
			{
			default:
				for (;;)
				{
					for (;;)
					{
						int lastRow = this.ᝊ.LastRow;
						int lastColumn = this.ᝊ.LastColumn;
						int column = this.ᝊ.Column;
						int num = this.ᝊ.Row;
						int num2 = 1;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_74;
							case 1:
								goto IL_74;
							case 2:
								goto IL_8A;
							case 3:
							{
								if (num > lastRow)
								{
									num2 = 2;
									continue;
								}
								XlsChartSerie xlsChartSerie = (XlsChartSerie)this.Series.Add();
								xlsChartSerie.Bubbles = this.ᝊ[num, column, num, lastColumn];
								xlsChartSerie.Values = this.ᝊ[num + 1, column, num + 1, lastColumn];
								xlsChartSerie.ValueRangeChanged += this.ᜀ;
								num += 2;
								num2 = 0;
								continue;
							}
							}
							break;
							IL_74:
							num2 = 3;
						}
					}
					IL_8A:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_A0;
					}
				}
				IL_A0:
				if (false)
				{
				}
				return;
			}
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0006A594 File Offset: 0x00069594
		private void ᜋ()
		{
			if (true)
			{
			}
			switch (0)
			{
			default:
				for (;;)
				{
					for (;;)
					{
						int row = this.ᝊ.Row;
						int lastRow = this.ᝊ.LastRow;
						int lastColumn = this.ᝊ.LastColumn;
						int num = this.ᝊ.Column;
						int num2 = 2;
						for (;;)
						{
							switch (num2)
							{
							case 0:
							{
								if (num > lastColumn)
								{
									num2 = 3;
									continue;
								}
								XlsChartSerie xlsChartSerie = (XlsChartSerie)this.Series.Add();
								xlsChartSerie.Bubbles = this.ᝊ[row, num, lastRow, num];
								xlsChartSerie.Values = this.ᝊ[row, num + 1, lastRow, num + 1];
								xlsChartSerie.ValueRangeChanged += this.ᜀ;
								num++;
								num2 = 1;
								continue;
							}
							case 1:
								goto IL_74;
							case 2:
								goto IL_74;
							case 3:
								goto IL_8A;
							}
							break;
							IL_74:
							num2 = 0;
						}
					}
					IL_8A:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_A0;
					}
				}
				IL_A0:
				if (false)
				{
				}
				return;
			}
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0006A6B4 File Offset: 0x000696B4
		private void ᜊ()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					for (;;)
					{
						int row = this.ᝊ.Row;
						int column = this.ᝊ.Column;
						int lastRow = this.ᝊ.LastRow;
						int lastColumn = this.ᝊ.LastColumn;
						int num = row + 1;
						int num2 = 2;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_88;
							case 1:
								goto IL_71;
							case 2:
								goto IL_71;
							case 3:
							{
								if (num > lastRow)
								{
									num2 = 0;
									continue;
								}
								XlsChartSerie xlsChartSerie = (XlsChartSerie)this.Series.Add();
								xlsChartSerie.CategoryLabels = this.ᝊ[row, column, row, lastColumn];
								xlsChartSerie.Values = this.ᝊ[num, column, num, lastColumn];
								xlsChartSerie.ValueRangeChanged += this.ᜀ;
								num += 2;
								if (true)
								{
								}
								num2 = 1;
								continue;
							}
							}
							break;
							IL_71:
							num2 = 3;
						}
					}
					IL_88:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_9E;
					}
				}
				IL_9E:
				if (false)
				{
				}
				return;
			}
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0006A7DC File Offset: 0x000697DC
		private void ᜉ()
		{
			if (true)
			{
			}
			switch (0)
			{
			default:
				for (;;)
				{
					for (;;)
					{
						int row = this.ᝊ.Row;
						int column = this.ᝊ.Column;
						int lastRow = this.ᝊ.LastRow;
						int lastColumn = this.ᝊ.LastColumn;
						int num = column + 1;
						int num2 = 1;
						for (;;)
						{
							switch (num2)
							{
							case 0:
							{
								if (num > lastColumn)
								{
									num2 = 3;
									continue;
								}
								XlsChartSerie xlsChartSerie = (XlsChartSerie)this.Series.Add();
								xlsChartSerie.CategoryLabels = this.ᝊ[row, column, lastRow, column];
								xlsChartSerie.Values = this.ᝊ[row, num, lastRow, num];
								xlsChartSerie.ValueRangeChanged += this.ᜀ;
								num++;
								num2 = 2;
								continue;
							}
							case 1:
								goto IL_79;
							case 2:
								goto IL_79;
							case 3:
								goto IL_90;
							}
							break;
							IL_79:
							num2 = 0;
						}
					}
					IL_90:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_A6;
					}
				}
				IL_A6:
				if (false)
				{
				}
				return;
			}
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0006A904 File Offset: 0x00069904
		private new void ᜄ(int A_0)
		{
			int a_ = 7;
			switch (0)
			{
			default:
			{
				XlsChartSerie xlsChartSerie;
				for (;;)
				{
					int row;
					int column;
					int lastRow;
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
						row = this.ᝊ.Row;
						column = this.ᝊ.Column;
						lastRow = this.ᝊ.LastRow;
						int lastColumn = this.ᝊ.LastColumn;
						break;
					}
					}
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_B6;
						case 1:
							goto IL_149;
						case 2:
						{
							int num2;
							if (num2 >= A_0)
							{
								num = 5;
								continue;
							}
							xlsChartSerie = (XlsChartSerie)this.Series.Add();
							xlsChartSerie.Values = this.ᝊ[row + num2, column, lastRow + num2, column];
							xlsChartSerie.ValueRangeChanged += this.ᜀ;
							num2++;
							num = 1;
							continue;
						}
						case 3:
						{
							if (lastRow - row != A_0 - 1)
							{
								num = 0;
								continue;
							}
							int num2 = 0;
							num = 4;
							continue;
						}
						case 4:
							goto IL_149;
						case 5:
							goto IL_169;
						}
						break;
						IL_149:
						num = 2;
					}
				}
				IL_B6:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("椼圾⑀ㅂ⁄杆㩈⍊≌㩎㵐㝒畔㕖㱘筚", a_) + A_0.ToString() + RecordTableEnumerator.b("ᴼ嬾⁀㝂⑄杆㭈⑊㩌㱎煐❒㩔睖ⵘ㍚㡜罞ɠୢѤᕦᵨ䭪ᥬ᙮Űᙲ孴", a_));
				IL_169:
				xlsChartSerie = (XlsChartSerie)this.Series[A_0 - 1];
				this.ᜀ(xlsChartSerie);
				return;
			}
			}
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0006AA98 File Offset: 0x00069A98
		private new void ᜃ(int A_0)
		{
			int a_ = 9;
			switch (0)
			{
			default:
			{
				XlsChartSerie xlsChartSerie;
				for (;;)
				{
					int row;
					int column;
					int lastRow;
					int lastColumn;
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
						row = this.ᝊ.Row;
						column = this.ᝊ.Column;
						lastRow = this.ᝊ.LastRow;
						lastColumn = this.ᝊ.LastColumn;
						break;
					}
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_B6;
						case 1:
							goto IL_14B;
						case 2:
							goto IL_16B;
						case 3:
							goto IL_14B;
						case 4:
						{
							if (lastColumn - column != A_0 - 1)
							{
								num = 0;
								continue;
							}
							int num2 = 0;
							num = 3;
							continue;
						}
						case 5:
						{
							int num2;
							if (num2 >= A_0)
							{
								num = 2;
								continue;
							}
							xlsChartSerie = (XlsChartSerie)this.Series.Add();
							xlsChartSerie.Values = this.ᝊ[row, column + num2, lastRow, column + num2];
							xlsChartSerie.ValueRangeChanged += this.ᜀ;
							num2++;
							num = 1;
							continue;
						}
						}
						break;
						IL_14B:
						num = 5;
					}
				}
				IL_B6:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("款⥀♂㝄≆楈㡊╌⁎⑐㽒ㅔ睖㭘㹚絜", a_) + A_0 + RecordTableEnumerator.b("Ἶ╀≂ㅄ♆楈⡊≌⍎⑐㹒㭔⑖祘⽚㉜罞ᕠୢd䝦੨ͪ౬ᵮհ卲Ŵ๶ॸṺ卼", a_));
				IL_16B:
				xlsChartSerie = (XlsChartSerie)this.Series[A_0 - 1];
				this.ᜀ(xlsChartSerie);
				return;
			}
			}
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0006AC30 File Offset: 0x00069C30
		private new void ᜂ(int A_0)
		{
			int a_ = 15;
			switch (0)
			{
			default:
				for (;;)
				{
					int row = this.ᝊ.Row;
					int column = this.ᝊ.Column;
					int lastRow = this.ᝊ.LastRow;
					int lastColumn = this.ᝊ.LastColumn;
					int num = 2;
					for (;;)
					{
						XlsChartSerie xlsChartSerie;
						int num2;
						switch (num)
						{
						case 0:
							goto IL_91;
						case 1:
							goto IL_1EB;
						case 2:
							if (lastRow - row != A_0 - 1)
							{
								num = 0;
								continue;
							}
							xlsChartSerie = (XlsChartSerie)this.\u1753.Add();
							xlsChartSerie.Values = this.ᝊ[row, column, lastRow, column];
							xlsChartSerie.Number = A_0 - 1;
							xlsChartSerie.ValueRangeChanged += this.ᜀ;
							num2 = 1;
							num = 3;
							continue;
						case 3:
							goto IL_1EB;
						case 4:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_D6;
							default:
								if (false)
								{
								}
								if (A_0 == 4)
								{
									num = 6;
									continue;
								}
								goto IL_210;
							}
							break;
						case 5:
							goto IL_197;
						case 6:
							this.ᜀ((XlsChartSerie)this.\u1753[3]);
							num = 5;
							continue;
						case 7:
							if (num2 >= A_0)
							{
								num = 8;
								continue;
							}
							goto IL_D6;
						case 8:
							num = 4;
							continue;
						}
						break;
						IL_D6:
						xlsChartSerie = (XlsChartSerie)this.\u1753.Add();
						xlsChartSerie.Values = this.ᝊ[row + num2, column, lastRow + num2, column];
						xlsChartSerie.Number = num2 - 1;
						xlsChartSerie.ChartGroup = 1;
						xlsChartSerie.ValueRangeChanged += this.ᜀ;
						num2++;
						num = 1;
						continue;
						IL_1EB:
						num = 7;
					}
				}
				IL_91:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ᅄ⽆ⱈ㥊⡌潎≐㭒㩔≖㕘㽚絜㵞Ѡ䍢", a_) + A_0.ToString() + RecordTableEnumerator.b("敄⍆⡈㽊ⱌ潎⍐㱒≔⑖祘⽚㉜罞ᕠୢd䝦੨ͪ౬ᵮհ卲Ŵ๶ॸṺ卼", a_));
				IL_197:
				IL_210:
				if (true)
				{
				}
				this.ᜈ();
				return;
			}
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0006AE5C File Offset: 0x00069E5C
		private void ᜁ(int A_0)
		{
			int a_ = 19;
			switch (0)
			{
			default:
				for (;;)
				{
					if (true)
					{
					}
					int row = this.ᝊ.Row;
					int column = this.ᝊ.Column;
					int lastRow = this.ᝊ.LastRow;
					int lastColumn = this.ᝊ.LastColumn;
					int num = 6;
					for (;;)
					{
						int num2;
						XlsChartSerie xlsChartSerie;
						switch (num)
						{
						case 0:
							goto IL_1A3;
						case 1:
							goto IL_1FB;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_E8;
							default:
								if (false)
								{
								}
								if (A_0 == 4)
								{
									num = 7;
									continue;
								}
								goto IL_220;
							}
							break;
						case 3:
							if (num2 >= A_0)
							{
								num = 4;
								continue;
							}
							goto IL_E8;
						case 4:
							num = 2;
							continue;
						case 5:
							goto IL_99;
						case 6:
							if (lastColumn - column != A_0 - 1)
							{
								num = 5;
								continue;
							}
							xlsChartSerie = (XlsChartSerie)this.\u1753.Add();
							xlsChartSerie.Values = this.ᝊ[row, column, lastRow, column];
							xlsChartSerie.Number = A_0 - 1;
							xlsChartSerie.ValueRangeChanged += this.ᜀ;
							num2 = 1;
							num = 1;
							continue;
						case 7:
							this.ᜀ((XlsChartSerie)this.\u1753[3]);
							num = 0;
							continue;
						case 8:
							goto IL_1FB;
						}
						break;
						IL_E8:
						xlsChartSerie = (XlsChartSerie)this.\u1753.Add();
						xlsChartSerie.Values = this.ᝊ[row, column + num2, lastRow, column + num2];
						xlsChartSerie.Number = num2 - 1;
						xlsChartSerie.ChartGroup = 1;
						xlsChartSerie.ValueRangeChanged += this.ᜀ;
						num2++;
						num = 8;
						continue;
						IL_1FB:
						num = 3;
					}
				}
				IL_99:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ᵈ⍊⡌㵎㑐獒♔㽖㙘⹚ㅜ㭞䅠Ţd䝦", a_) + A_0 + RecordTableEnumerator.b("楈⽊ⱌ㭎ぐ獒㙔㡖㕘⹚ぜㅞበ䍢ᅤࡦ䥨Ὢլ੮兰ၲᵴᙶ୸ེ嵼୾ꦆ", a_));
				IL_1A3:
				IL_220:
				this.ᜈ();
				return;
			}
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0006B090 File Offset: 0x0006A090
		private void ᜈ()
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
			this.SecondaryCategoryAxis.IsMaxCross = true;
			XlsChartCategoryAxis xlsChartCategoryAxis = (XlsChartCategoryAxis)this.SecondaryCategoryAxis;
			this.\u1755.ᜃ(true);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0006B0F0 File Offset: 0x0006A0F0
		private new void ᜀ(XlsChartSerie A_0)
		{
			int a_ = 0;
			if (true)
			{
			}
			if (A_0 != null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_16;
				}
				if (false)
				{
				}
				XlsChartDataPoint xlsChartDataPoint = (XlsChartDataPoint)A_0.DataPoints.DefaultDataPoint;
				XlsChartSerieDataFormat xlsChartSerieDataFormat = (XlsChartSerieDataFormat)xlsChartDataPoint.DataFormat;
				IChartBorder lineProperties = xlsChartSerieDataFormat.LineProperties;
				lineProperties.Pattern = ChartLinePatternType.None;
				lineProperties.Weight = ChartLineWeightType.Hairline;
				xlsChartSerieDataFormat.PieFormat.ᜀ(0);
				sprᣐ sprᣐ = xlsChartSerieDataFormat.MarkerFormat;
				sprᣐ.ᜀ(ChartMarkerType.DowJones);
				sprᣐ.ᜀ(60);
				return;
			}
			IL_16:
			throw new ArgumentNullException(RecordTableEnumerator.b("䔵崷䠹唻嬽", a_));
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0006B1A8 File Offset: 0x0006A1A8
		private void ᜇ()
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_44;
				case 1:
					IL_08:
					break;
				case 2:
					this.ᜂ(this.ChartType);
					num = 0;
					continue;
				}
				if (this.ᝊ != null)
				{
					num = 2;
					continue;
				}
				IL_44:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_08;
				default:
					goto IL_64;
				}
			}
			IL_64:
			if (true)
			{
			}
			if (false)
			{
			}
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0006B228 File Offset: 0x0006A228
		private void ᜆ()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					XlsChartSeries u = this.\u1753;
					this.\u1753 = new ChartSeries((spr\u2158)base.ReservedHandle, this.\u1753.Parent);
					int num = 0;
					int count = u.Count;
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							IXLSRange bubbles;
							if (bubbles != null)
							{
								if (true)
								{
								}
								num2 = 6;
								continue;
							}
							goto IL_72;
						}
						case 1:
							IL_6D:
							goto IL_122;
						case 2:
							goto IL_122;
						case 3:
							return;
						case 4:
							goto IL_72;
						case 5:
						{
							if (num >= count)
							{
								num2 = 3;
								continue;
							}
							XlsChartSerie xlsChartSerie = (XlsChartSerie)this.\u1753.Add();
							XlsChartSerie xlsChartSerie2 = (XlsChartSerie)u[num];
							xlsChartSerie.Values = xlsChartSerie2.Values;
							IXLSRange bubbles = xlsChartSerie2.Bubbles;
							num2 = 0;
							continue;
						}
						case 6:
						{
							XlsChartSerie xlsChartSerie3 = new ChartSerie((spr\u2158)base.ReservedHandle, this.\u1753);
							IXLSRange bubbles;
							xlsChartSerie3.Values = bubbles;
							this.\u1753.Add(xlsChartSerie3);
							num2 = 4;
							continue;
						}
						}
						break;
						IL_72:
						num++;
						num2 = 2;
						continue;
						IL_122:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6D;
						}
						if (false)
						{
						}
						num2 = 5;
					}
				}
				return;
			}
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0006B3A0 File Offset: 0x0006A3A0
		private new void ᜀ(ExcelChartType A_0, bool A_1)
		{
			int a_ = 19;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_4F;
				case 2:
					this.ᜆ();
					num = 0;
					continue;
				case 3:
					goto IL_4D;
				case 4:
					goto IL_8B;
				case 5:
					if (Array.IndexOf<ExcelChartType>(XlsChart.\u173E, A_0) != -1)
					{
						num = 4;
						continue;
					}
					goto IL_12D;
				case 6:
					if (this.ChartStartType == RecordTableEnumerator.b("ୈ㹊⽌ⵎ㵐㙒", a_))
					{
						num = 2;
						continue;
					}
					goto IL_4F;
				}
				if (A_0 == ExcelChartType.CombinationChart)
				{
					if (true)
					{
					}
					num = 3;
					continue;
				}
				this.HasDataTable = false;
				this.\u1753.ᜄ();
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_4D;
				}
				if (false)
				{
				}
				num = 6;
				continue;
				IL_4F:
				this.\u175B.ᜆ().Clear();
				this.\u1753.ᜉ();
				num = 5;
			}
			IL_4D:
			throw new ArgumentException(RecordTableEnumerator.b("ੈ⍊ⱌⅎ㙐㙒畔㑖ㅘ㩚⽜⭞䅠ᝢᱤᝦ౨䭪୬๮ᡰὲၴ፶坸", a_));
			IL_8B:
			this.ᜀ(A_0);
			return;
			IL_12D:
			XlsChartFormat xlsChartFormat = new ChartFormat((spr\u2158)base.ReservedHandle, this.PrimaryFormats);
			this.PrimaryFormats.Add(xlsChartFormat, false);
			xlsChartFormat.ᜃ(A_0, A_1);
			this.ᜁ(A_0);
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0006B510 File Offset: 0x0006A510
		private new void ᜅ()
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
			((XlsChartAxis)this.PrimaryCategoryAxis).UpdateTickRecord(TickLabelPositionType.TickLabelPositionLow);
			((XlsChartAxis)this.PrimaryValueAxis).UpdateTickRecord(TickLabelPositionType.TickLabelPositionNone);
			((XlsChartAxis)this.PrimarySerieAxis).UpdateTickRecord(TickLabelPositionType.TickLabelPositionLow);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0006B580 File Offset: 0x0006A580
		private new void ᜄ()
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
			((XlsChartAxis)this.PrimaryCategoryAxis).UpdateTickRecord(TickLabelPositionType.TickLabelPositionNextToAxis);
			((XlsChartAxis)this.PrimaryValueAxis).UpdateTickRecord(TickLabelPositionType.TickLabelPositionNextToAxis);
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0006B5E0 File Offset: 0x0006A5E0
		private void ᜁ(ExcelChartType A_0)
		{
			switch (0)
			{
			default:
			{
				int num = 1;
				for (;;)
				{
					bool flag;
					XlsChartAxis xlsChartAxis2;
					switch (num)
					{
					case 0:
					{
						XlsChartAxis xlsChartAxis = (XlsChartAxis)this.PrimaryCategoryAxis;
						xlsChartAxis.UpdateTickRecord(TickLabelPositionType.TickLabelPositionLow);
						num = 6;
						continue;
					}
					case 2:
						return;
					case 3:
						if (Array.IndexOf<ExcelChartType>(XlsChart.\u1735, this.ChartType) != -1)
						{
							num = 0;
							continue;
						}
						goto IL_1BF;
					case 4:
						goto IL_D4;
					case 5:
						goto IL_37A;
					case 6:
						goto IL_1BF;
					case 7:
						num = 22;
						continue;
					case 8:
						this.\u175E = true;
						num = 16;
						continue;
					case 9:
						if (A_0 == ExcelChartType.SurfaceContourNoColor)
						{
							num = 14;
							continue;
						}
						num = 21;
						continue;
					case 10:
						goto IL_D4;
					case 11:
						num = 13;
						continue;
					case 12:
						if (flag)
						{
							num = 26;
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
							this.\u175B.ᜀ(null);
							num = 4;
							continue;
						}
						break;
					case 13:
						if (A_0 != ExcelChartType.RadarFilled)
						{
							num = 7;
							continue;
						}
						goto IL_17F;
					case 14:
						goto IL_154;
					case 15:
						goto IL_13A;
					case 16:
						goto IL_2A2;
					case 17:
					{
						if (true)
						{
						}
						XlsChartSeriesAxis xlsChartSeriesAxis;
						this.\u175B.ᜀ(xlsChartSeriesAxis = new ChartSeriesAxis((spr\u2158)base.ReservedHandle, this.\u175B, AxisType.Serie));
						xlsChartAxis2 = xlsChartSeriesAxis;
						num = 5;
						continue;
					}
					case 18:
						if (A_0 == ExcelChartType.SurfaceContourNoColor)
						{
							num = 24;
							continue;
						}
						return;
					case 19:
						if (!this.\u175E)
						{
							num = 8;
							continue;
						}
						goto IL_2A2;
					case 20:
						num = 9;
						continue;
					case 21:
						if (A_0 != ExcelChartType.Radar)
						{
							num = 11;
							continue;
						}
						goto IL_17F;
					case 22:
					{
						if (A_0 == ExcelChartType.RadarMarkers)
						{
							num = 15;
							continue;
						}
						XlsChartAxis xlsChartAxis3 = (XlsChartAxis)this.PrimaryValueAxis;
						xlsChartAxis3.UpdateTickRecord(TickLabelPositionType.TickLabelPositionLow);
						num = 2;
						continue;
					}
					case 23:
						if (xlsChartAxis2 == null)
						{
							num = 17;
							continue;
						}
						goto IL_37A;
					case 24:
						goto IL_17A;
					case 25:
						num = 19;
						continue;
					case 26:
						xlsChartAxis2 = this.\u175B.ᜅ();
						num = 23;
						continue;
					case 27:
						if (A_0 != ExcelChartType.SurfaceContour)
						{
							num = 20;
							continue;
						}
						goto IL_154;
					}
					IL_90:
					if (!this.m_book.Loading)
					{
						num = 25;
						continue;
					}
					return;
					goto IL_90;
					IL_D4:
					num = 27;
					continue;
					IL_154:
					this.ᜅ();
					num = 18;
					continue;
					IL_1BF:
					flag = (Array.IndexOf<ExcelChartType>(XlsChart.DEF_SUPPORT_SERIES_AXIS, A_0) != -1);
					num = 12;
					continue;
					IL_2A2:
					this.\u175D = new ChartLegend((spr\u2158)base.ReservedHandle, this);
					this.\u175B.ᜄ();
					this.SetToDefaultGridlines(A_0);
					this.\u175F = new ChartWallOrFloor((spr\u2158)base.ReservedHandle, this, true);
					this.ᝠ = new ChartWallOrFloor((spr\u2158)base.ReservedHandle, this, false);
					this.ᝡ = new ChartPlotArea((spr\u2158)base.ReservedHandle, this, A_0);
					num = 3;
					continue;
					IL_37A:
					xlsChartAxis2.UpdateTickRecord(TickLabelPositionType.TickLabelPositionLow);
					num = 10;
				}
				IL_13A:
				goto IL_17F;
				IL_17A:
				this.\u175F.Interior.Pattern = ExcelPatternType.None;
				this.ᝠ.Interior.Pattern = ExcelPatternType.None;
				return;
				IL_17F:
				this.ᜄ();
				return;
			}
			}
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0006B9C8 File Offset: 0x0006A9C8
		private new void ᜀ(ExcelChartType A_0)
		{
			int a_ = 12;
			for (;;)
			{
				int count = this.\u1753.Count;
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_145;
					case 1:
						goto IL_280;
					case 2:
						if (count != 3)
						{
							num = 0;
							continue;
						}
						goto IL_206;
					case 3:
						goto IL_24E;
					case 4:
						goto IL_270;
					case 5:
						switch (A_0)
						{
						case ExcelChartType.StockHighLowClose:
							num = 2;
							continue;
						case ExcelChartType.StockOpenHighLowClose:
							num = 10;
							continue;
						case ExcelChartType.StockVolumeHighLowClose:
							num = 8;
							continue;
						case ExcelChartType.StockVolumeOpenHighLowClose:
							num = 7;
							continue;
						}
						goto IL_6C;
					case 6:
						goto IL_2B4;
					case 7:
						if (count != 5)
						{
							num = 3;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6C;
						default:
							goto IL_171;
						}
						break;
					case 8:
						if (count != 4)
						{
							num = 4;
							continue;
						}
						goto IL_79;
					case 9:
						num = 1;
						continue;
					case 10:
						if (count != 4)
						{
							num = 6;
							continue;
						}
						goto IL_D5;
					}
					break;
					IL_6C:
					num = 9;
				}
			}
			IL_79:
			XlsChartFormat xlsChartFormat = new ChartFormat((spr\u2158)base.ReservedHandle, this.PrimaryFormats);
			this.PrimaryFormats.Add(xlsChartFormat);
			xlsChartFormat.\u1712();
			xlsChartFormat = new ChartFormat((spr\u2158)base.ReservedHandle, this.SecondaryFormats);
			xlsChartFormat.DrawingZOrder = 1;
			this.SecondaryFormats.Add(xlsChartFormat);
			xlsChartFormat.\u1716();
			return;
			IL_D5:
			xlsChartFormat = new ChartFormat((spr\u2158)base.ReservedHandle, this.PrimaryFormats);
			this.PrimaryFormats.Add(xlsChartFormat);
			xlsChartFormat.ᜏ();
			return;
			IL_145:
			throw new ArgumentException(RecordTableEnumerator.b("ᙁⱃ⍅桇㥉⥋㱍㥏㝑❓癕㭗㕙⥛そᑟ䉡ୣe䡧३ѫ཭ɯٱ味ɵŷ੹᥻幽겋늑ﮙ뺝鎟財", a_));
			IL_171:
			if (false)
			{
			}
			this.IsManuallyFormatted = true;
			this.\u175C.ᜁ(true);
			xlsChartFormat = new ChartFormat((spr\u2158)base.ReservedHandle, this.PrimaryFormats);
			this.PrimaryFormats.Add(xlsChartFormat);
			xlsChartFormat.\u1712();
			xlsChartFormat = new ChartFormat((spr\u2158)base.ReservedHandle, this.SecondaryFormats);
			xlsChartFormat.DrawingZOrder = 1;
			this.SecondaryFormats.Add(xlsChartFormat);
			xlsChartFormat.\u1717();
			this.SecondaryCategoryAxis.IsMaxCross = true;
			return;
			IL_206:
			xlsChartFormat = new ChartFormat((spr\u2158)base.ReservedHandle, this.PrimaryFormats);
			this.PrimaryFormats.Add(xlsChartFormat);
			xlsChartFormat.\u1713();
			return;
			IL_24E:
			throw new ArgumentException(RecordTableEnumerator.b("ᙁⱃ⍅桇㥉⥋㱍㥏㝑❓癕㭗㕙⥛そᑟ䉡ୣe䡧३ѫ཭ɯٱ味ɵŷ੹᥻幽겋늑ﮙ뺝閟財", a_));
			IL_270:
			throw new ArgumentException(RecordTableEnumerator.b("ᙁⱃ⍅桇㥉⥋㱍㥏㝑❓癕㭗㕙⥛そᑟ䉡ୣe䡧३ѫ཭ɯٱ味ɵŷ੹᥻幽겋늑ﮙ뺝钟財", a_));
			IL_280:
			if (true)
			{
			}
			throw new ArgumentException(RecordTableEnumerator.b("㙁㵃㙅ⵇ", a_));
			IL_2B4:
			throw new ArgumentException(RecordTableEnumerator.b("ᙁⱃ⍅桇㥉⥋㱍㥏㝑❓癕㭗㕙⥛そᑟ䉡ୣe䡧३ѫ཭ɯٱ味ɵŷ੹᥻幽겋늑ﮙ뺝钟財", a_));
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0006BCA4 File Offset: 0x0006ACA4
		private new spr\u2272 ᜃ()
		{
			spr\u2272 spr_u;
			for (;;)
			{
				spr_u = (spr\u2272)spr\u175E.ᜀ(TBIFFRecord.Chart3D);
				spr_u.ᜀ(this.IsPerspective);
				spr_u.ᜅ(this.IsClustered);
				ExcelChartType chartType = this.ChartType;
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_120;
					case 1:
						return spr_u;
					case 2:
						return spr_u;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_120;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 4:
						return spr_u;
					case 5:
						if (chartType != ExcelChartType.Pie3D)
						{
							num = 7;
							continue;
						}
						goto IL_F3;
					case 6:
						if (chartType != ExcelChartType.Pie3DExploded)
						{
							num = 8;
							continue;
						}
						goto IL_F3;
					case 7:
						num = 6;
						continue;
					case 8:
						num = 0;
						continue;
					}
					break;
					IL_120:
					switch (chartType)
					{
					case ExcelChartType.SurfaceContour:
					case ExcelChartType.SurfaceContourNoColor:
						if (true)
						{
						}
						spr_u.ᜄ(0);
						spr_u.ᜀ(90);
						spr_u.ᜀ(0);
						num = 4;
						continue;
					default:
						num = 3;
						continue;
					}
					IL_F3:
					spr_u.ᜄ(0);
					spr_u.ᜁ(false);
					spr_u.ᜂ(false);
					num = 1;
				}
			}
			return spr_u;
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0006BDF4 File Offset: 0x0006ADF4
		private new void ᜀ(object A_0, XlsEventArgs A_1)
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
			this.ᝊ = null;
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0006BE38 File Offset: 0x0006AE38
		private new void ᜂ()
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
			this.\u1758 = new ChartArea((spr\u2158)base.ReservedHandle, this);
			this.\u1758.Interior.ForegroundKnownColor = ExcelColors.WhiteCustom;
			this.\u1759 = new XlsChartFrameFormat(base.ReservedHandle, this, true, false, true);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0006BEB4 File Offset: 0x0006AEB4
		public void RemoveFormat(IChartFormat formatToRemove)
		{
			int a_ = 8;
			while (formatToRemove == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("堽⼿ぁ⥃❅㱇ṉ⍋ᱍ㕏㽑㭓⁕㵗", a_));
				}
			}
			this.\u175B.ᜆ().Remove((XlsChartFormat)formatToRemove);
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0006BF28 File Offset: 0x0006AF28
		public void UpdateChartTitle()
		{
			int a_ = 18;
			switch (0)
			{
			default:
			{
				int num = 13;
				for (;;)
				{
					string a;
					string text;
					int count;
					XlsChartSerie xlsChartSerie;
					XlsChartTextArea xlsChartTextArea;
					switch (num)
					{
					case 0:
						if (!(a == RecordTableEnumerator.b("ᡇ⍉⥋", a_)))
						{
							num = 16;
							continue;
						}
						goto IL_122;
					case 1:
						goto IL_AC;
					case 2:
						num = 0;
						continue;
					case 3:
						return;
					case 4:
						text = this.ᜁ();
						goto IL_1B0;
					case 5:
						if (count == 1)
						{
							num = 20;
							continue;
						}
						num = 4;
						continue;
					case 6:
						if (a == RecordTableEnumerator.b("ే╉㥋⥍㡏㱑⅓≕", a_))
						{
							num = 14;
							continue;
						}
						return;
					case 7:
						num = 5;
						continue;
					case 8:
						xlsChartSerie = (XlsChartSerie)this.Series[0];
						num = 15;
						continue;
					case 9:
						text = null;
						goto IL_1B0;
					case 10:
						if (xlsChartTextArea.TextRecord.ᜄ())
						{
							num = 18;
							continue;
						}
						return;
					case 11:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_AC;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							if (count != 1)
							{
								num = 2;
								continue;
							}
							goto IL_122;
						}
						break;
					case 12:
						return;
					case 14:
						goto IL_122;
					case 15:
						if (!xlsChartSerie.IsDefaultName)
						{
							num = 7;
							continue;
						}
						return;
					case 16:
						num = 6;
						continue;
					case 17:
						if (this.ChartTitle == null)
						{
							num = 19;
							continue;
						}
						return;
					case 18:
						count = this.Series.Count;
						num = 1;
						continue;
					case 19:
						num = 10;
						continue;
					case 20:
						num = 9;
						continue;
					}
					if (this.m_title == null)
					{
						num = 12;
						continue;
					}
					xlsChartTextArea = (this.ChartTitleArea as XlsChartTextArea);
					num = 17;
					continue;
					IL_AC:
					if (count > 0)
					{
						num = 8;
						continue;
					}
					return;
					IL_122:
					string parseSerieNotDefaultText = xlsChartSerie.ParseSerieNotDefaultText;
					this.ChartTitle = parseSerieNotDefaultText;
					num = 3;
					continue;
					IL_1B0:
					a = text;
					num = 11;
				}
				return;
			}
			}
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0006C1AC File Offset: 0x0006B1AC
		private string ᜁ()
		{
			switch (0)
			{
			default:
			{
				int num = 5;
				for (;;)
				{
					string text;
					int num2;
					int count;
					switch (num)
					{
					case 0:
					{
						XlsChartSerie xlsChartSerie;
						if (xlsChartSerie.ᜇ() != text)
						{
							num = 1;
							continue;
						}
						num2++;
						num = 8;
						continue;
					}
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_FB;
						default:
							if (false)
							{
							}
							text = null;
							num = 6;
							continue;
						}
						break;
					case 2:
					{
						if (num2 >= count)
						{
							num = 4;
							continue;
						}
						XlsChartSerie xlsChartSerie = this.\u1753[num2] as XlsChartSerie;
						num = 0;
						continue;
					}
					case 3:
						goto IL_5A;
					case 4:
						return text;
					case 6:
						return text;
					case 7:
						goto IL_FB;
					case 8:
						goto IL_A4;
					}
					if (this.\u1753.Count == 0)
					{
						num = 3;
						continue;
					}
					text = (this.\u1753[0] as XlsChartSerie).ᜇ();
					num2 = 1;
					count = this.\u1753.Count;
					num = 7;
					continue;
					IL_A4:
					if (true)
					{
					}
					num = 2;
					continue;
					IL_FB:
					goto IL_A4;
				}
				IL_5A:
				return null;
			}
			}
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0006C2F0 File Offset: 0x0006B2F0
		private new IXLSRange ᜀ()
		{
			switch (0)
			{
			default:
			{
				int num = 16;
				IWorksheet worksheet;
				Rectangle rectangle;
				for (;;)
				{
					IXLSRange serieNameRange;
					IXLSRange bubbles;
					string name;
					IXLSRange values;
					switch (num)
					{
					case 0:
					{
						IXLSRange ixlsrange;
						if (ixlsrange != null)
						{
							num = 2;
							continue;
						}
						goto IL_153;
					}
					case 1:
					{
						IXLSRange ixlsrange2;
						if (ixlsrange2 == null)
						{
							num = 13;
							continue;
						}
						num = 6;
						continue;
					}
					case 2:
						num = 19;
						continue;
					case 3:
						goto IL_22D;
					case 4:
						goto IL_89;
					case 5:
						goto IL_BB;
					case 6:
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
							IXLSRange ixlsrange;
							if (!this.ᜀ(serieNameRange, bubbles, worksheet, name, out ixlsrange))
							{
								num = 11;
								continue;
							}
							IXLSRange ixlsrange2;
							rectangle = sprṔ.ᜀ(ixlsrange2, true);
							num = 0;
							continue;
						}
						}
						break;
					case 7:
						num = 12;
						continue;
					case 8:
						goto IL_262;
					case 9:
					{
						IXLSRange categoryLabels;
						if (name != categoryLabels.Worksheet.Name)
						{
							num = 8;
							continue;
						}
						goto IL_264;
					}
					case 10:
					{
						if (values == null)
						{
							num = 15;
							continue;
						}
						IXLSRange ixlsrange2 = this.ᜀ(values, bubbles, worksheet, name);
						IXLSRange categoryLabels = this.PrimaryCategoryAxis.CategoryLabels;
						if (true)
						{
						}
						num = 18;
						continue;
					}
					case 11:
						goto IL_107;
					case 12:
					{
						IXLSRange categoryLabels;
						if (!this.ᜀ(categoryLabels, ref rectangle, this.ᝋ))
						{
							num = 3;
							continue;
						}
						goto IL_294;
					}
					case 13:
						goto IL_28D;
					case 14:
						num = 9;
						continue;
					case 15:
						goto IL_1CD;
					case 17:
					{
						IXLSRange categoryLabels;
						if (categoryLabels != null)
						{
							num = 7;
							continue;
						}
						goto IL_294;
					}
					case 18:
					{
						IXLSRange categoryLabels;
						if (categoryLabels != null)
						{
							num = 14;
							continue;
						}
						goto IL_264;
					}
					case 19:
					{
						IXLSRange ixlsrange;
						if (!this.ᜀ(ixlsrange, ref rectangle, !this.ᝋ))
						{
							num = 5;
							continue;
						}
						goto IL_153;
					}
					}
					if (this.\u1753.Count == 0)
					{
						num = 4;
						continue;
					}
					XlsChartSerie xlsChartSerie = (XlsChartSerie)this.Series[0];
					values = xlsChartSerie.Values;
					serieNameRange = xlsChartSerie.GetSerieNameRange();
					bubbles = xlsChartSerie.Bubbles;
					worksheet = values.Worksheet;
					name = worksheet.Name;
					num = 10;
					continue;
					IL_153:
					num = 17;
					continue;
					IL_264:
					num = 1;
				}
				IL_89:
				return null;
				IL_BB:
				return null;
				IL_107:
				return null;
				IL_1CD:
				return null;
				IL_22D:
				return null;
				IL_262:
				return null;
				IL_28D:
				return null;
				IL_294:
				return worksheet[rectangle.Top, rectangle.Left, rectangle.Bottom, rectangle.Right];
			}
			}
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0006C5B4 File Offset: 0x0006B5B4
		private new bool ᜀ(IXLSRange A_0)
		{
			while (A_0 == null)
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
					return true;
				}
			}
			int num = A_0.LastRow - A_0.Row;
			int num2 = A_0.LastColumn - A_0.Column;
			return num <= num2;
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0006C61C File Offset: 0x0006B61C
		private new IXLSRange ᜀ(IXLSRange A_0, bool A_1, out IXLSRange A_2)
		{
			int a_ = 5;
			switch (0)
			{
			default:
			{
				int num = 10;
				IXLSRange ixlsrange4;
				for (;;)
				{
					int num2;
					int num3;
					IXLSRange ixlsrange;
					bool flag;
					bool flag2;
					int num4;
					int num5;
					int num6;
					int num7;
					int num8;
					IXLSRange ixlsrange2;
					int num9;
					IXLSRange ixlsrange3;
					int num10;
					int num11;
					switch (num)
					{
					case 0:
						ixlsrange = A_0[num2, num3];
						goto IL_393;
					case 1:
						num = 3;
						continue;
					case 2:
						goto IL_1F9;
					case 3:
						ixlsrange = A_0[num3, num2];
						goto IL_393;
					case 4:
						goto IL_345;
					case 5:
						flag = true;
						goto IL_3E4;
					case 6:
						goto IL_30C;
					case 7:
						goto IL_159;
					case 8:
						if (flag2)
						{
							num = 38;
							continue;
						}
						num = 27;
						continue;
					case 9:
						if (num4 == -1)
						{
							num = 7;
							continue;
						}
						num = 21;
						continue;
					case 11:
						goto IL_E6;
					case 12:
						if (num3 < num5)
						{
							num = 29;
							continue;
						}
						goto IL_13C;
					case 13:
						num6 = A_0.Column;
						goto IL_421;
					case 14:
						if (!A_1)
						{
							num = 32;
							continue;
						}
						num = 28;
						continue;
					case 15:
						num6 = A_0.Row;
						goto IL_421;
					case 16:
						ixlsrange2 = A_0[num7, num8, num4, num2];
						goto IL_1EB;
					case 17:
						num9 = A_0.LastColumn;
						goto IL_296;
					case 18:
						flag = ixlsrange3.IsBlank;
						goto IL_3E4;
					case 19:
						goto IL_30C;
					case 20:
						if (!A_1)
						{
							num = 30;
							continue;
						}
						num = 15;
						continue;
					case 21:
						if (!A_1)
						{
							num = 41;
							continue;
						}
						num = 26;
						continue;
					case 22:
						num = 31;
						continue;
					case 23:
						if (!ixlsrange3.HasNumber)
						{
							num = 36;
							continue;
						}
						num = 5;
						continue;
					case 24:
						if (A_1)
						{
							num = 40;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3B9;
						default:
							if (false)
							{
							}
							num = 39;
							continue;
						}
						break;
					case 25:
						if (!A_1)
						{
							num = 22;
							continue;
						}
						num = 34;
						continue;
					case 26:
						ixlsrange2 = A_0[num8, num7, num2, num4];
						goto IL_1EB;
					case 27:
						if (!A_1)
						{
							num = 1;
							continue;
						}
						num = 0;
						continue;
					case 28:
						num10 = A_0.Column;
						goto IL_46B;
					case 29:
						num = 8;
						continue;
					case 30:
						num = 13;
						continue;
					case 31:
						num11 = A_0.LastRow;
						goto IL_27E;
					case 32:
						num = 35;
						continue;
					case 33:
						num4 = num3;
						num = 4;
						continue;
					case 34:
						num11 = A_0.LastColumn;
						goto IL_27E;
					case 35:
						num10 = A_0.Row;
						goto IL_46B;
					case 36:
						goto IL_3B9;
					case 37:
						if (!flag2)
						{
							num = 33;
							continue;
						}
						goto IL_345;
					case 38:
						goto IL_13C;
					case 39:
						num = 17;
						continue;
					case 40:
						num9 = A_0.LastRow;
						goto IL_296;
					case 41:
						if (true)
						{
						}
						num = 16;
						continue;
					}
					if (A_0 == null)
					{
						num = 11;
						continue;
					}
					num = 20;
					continue;
					IL_13C:
					num = 9;
					continue;
					IL_1EB:
					ixlsrange4 = ixlsrange2;
					num = 2;
					continue;
					IL_27E:
					num5 = num11;
					num4 = -1;
					flag2 = false;
					num3 = num7;
					num = 19;
					continue;
					IL_296:
					num2 = num9;
					num = 14;
					continue;
					IL_30C:
					num = 12;
					continue;
					IL_345:
					num3++;
					num = 6;
					continue;
					IL_393:
					ixlsrange3 = ixlsrange;
					num = 23;
					continue;
					IL_3B9:
					num = 18;
					continue;
					IL_3E4:
					flag2 = flag;
					num = 37;
					continue;
					IL_421:
					num8 = num6;
					num = 24;
					continue;
					IL_46B:
					num7 = num10;
					num = 25;
				}
				IL_E6:
				throw new ArgumentNullException(RecordTableEnumerator.b("䤺尼儾♀♂", a_));
				IL_159:
				A_2 = A_0;
				return null;
				IL_1F9:
				A_2 = (A_1 ? A_0[A_0.Row, ixlsrange4.LastColumn + 1, A_0.LastRow, A_0.LastColumn] : A_0[ixlsrange4.LastRow + 1, A_0.Column, A_0.LastRow, A_0.LastColumn]);
				return ixlsrange4;
			}
			}
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0006CAD0 File Offset: 0x0006BAD0
		private new bool ᜀ(IXLSRange A_0, ExcelChartType A_1)
		{
			int a_ = 3;
			switch (0)
			{
			default:
			{
				int num = 17;
				for (;;)
				{
					int num2;
					bool flag;
					int num3;
					int num4;
					int num5;
					string a;
					int num6;
					int count;
					int num7;
					int num8;
					switch (num)
					{
					case 0:
						return true;
					case 1:
						goto IL_1A0;
					case 2:
						if (num2 != 5)
						{
							num = 41;
							continue;
						}
						goto IL_26B;
					case 3:
						if (flag)
						{
							num = 5;
							continue;
						}
						this.\u1753.Add();
						num = 19;
						continue;
					case 4:
						goto IL_F2;
					case 5:
						this.\u1753.RemoveAt(num3 - num4 + num5 - 1);
						num = 20;
						continue;
					case 6:
						goto IL_2F3;
					case 7:
						if (num2 != 3)
						{
							num = 12;
							continue;
						}
						goto IL_445;
					case 8:
						if (a == RecordTableEnumerator.b("笸为弼崾ⵀ♂", a_))
						{
							num = 24;
							continue;
						}
						goto IL_2F3;
					case 9:
						num = 7;
						continue;
					case 10:
						if (true)
						{
						}
						num = 29;
						continue;
					case 11:
						if (A_1 != ExcelChartType.StockOpenHighLowClose)
						{
							num = 40;
							continue;
						}
						return false;
					case 12:
						return false;
					case 13:
						if (!flag)
						{
							num = 16;
							continue;
						}
						num = 31;
						continue;
					case 14:
						if (!(a == RecordTableEnumerator.b("笸为弼崾ⵀ♂", a_)))
						{
							num = 10;
							continue;
						}
						return false;
					case 15:
						if (A_1 == ExcelChartType.StockHighLowClose)
						{
							num = 9;
							continue;
						}
						goto IL_445;
					case 16:
						num = 43;
						continue;
					case 18:
						goto IL_1A0;
					case 19:
						goto IL_20B;
					case 20:
						goto IL_20B;
					case 21:
						num = 2;
						continue;
					case 22:
						goto IL_1AC;
					case 23:
						if (num2 < 2)
						{
							num = 36;
							continue;
						}
						goto IL_2D1;
					case 24:
						num2 = num2 / 2 + num2 % 2;
						num = 6;
						continue;
					case 25:
						if (!flag)
						{
							num = 37;
							continue;
						}
						num = 30;
						continue;
					case 26:
						num = 32;
						continue;
					case 27:
						num6 = num2;
						goto IL_1F7;
					case 28:
						if (A_1 == ExcelChartType.StockVolumeOpenHighLowClose)
						{
							num = 21;
							continue;
						}
						goto IL_26B;
					case 29:
						if (a == RecordTableEnumerator.b("樸为似夾⁀⁂⁄", a_))
						{
							num = 35;
							continue;
						}
						goto IL_2D1;
					case 30:
						num6 = count;
						goto IL_1F7;
					case 31:
						num7 = num2;
						goto IL_3DF;
					case 32:
						num8 = A_0.LastColumn - A_0.Column + 1;
						goto IL_21F;
					case 33:
						if (num2 != 4)
						{
							num = 38;
							continue;
						}
						goto IL_109;
					case 34:
						num8 = A_0.LastRow - A_0.Row + 1;
						goto IL_21F;
					case 35:
						goto IL_2CC;
					case 36:
						num = 14;
						continue;
					case 37:
						num = 27;
						continue;
					case 38:
						goto IL_3DA;
					case 39:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1AC;
						default:
							if (false)
							{
							}
							num = 33;
							continue;
						}
						break;
					case 40:
						num = 44;
						continue;
					case 41:
						return false;
					case 42:
						if (!this.ᝋ)
						{
							num = 26;
							continue;
						}
						num = 34;
						continue;
					case 43:
						num7 = count;
						goto IL_3DF;
					case 44:
						if (A_1 == ExcelChartType.StockVolumeHighLowClose)
						{
							num = 39;
							continue;
						}
						goto IL_109;
					}
					if (A_0 == null)
					{
						num = 4;
						continue;
					}
					a = XlsChartFormat.ᜉ(A_1);
					num = 42;
					continue;
					IL_109:
					num = 28;
					continue;
					IL_1A0:
					num = 22;
					continue;
					IL_1AC:
					if (num4 >= num3)
					{
						num = 0;
						continue;
					}
					num = 3;
					continue;
					IL_1F7:
					num3 = num6;
					num4 = num5;
					num = 18;
					continue;
					IL_20B:
					num4++;
					num = 1;
					continue;
					IL_21F:
					num2 = num8;
					num = 23;
					continue;
					IL_26B:
					num = 8;
					continue;
					IL_2D1:
					num = 15;
					continue;
					IL_2F3:
					count = this.\u1753.Count;
					flag = (count > num2);
					num = 13;
					continue;
					IL_3DF:
					num5 = num7;
					num = 25;
					continue;
					IL_445:
					num = 11;
				}
				IL_F2:
				throw new ArgumentNullException(RecordTableEnumerator.b("䨸帺似嘾⑀ᕂ⑄⭆㱈⹊", a_));
				IL_2CC:
				return false;
				IL_3DA:
				return false;
			}
			}
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0006CFAC File Offset: 0x0006BFAC
		private new void ᜀ(IXLSRange A_0, IXLSRange A_1, string A_2, int A_3)
		{
			int a_ = 10;
			switch (0)
			{
			default:
				for (;;)
				{
					int num = 0;
					int count = this.\u1753.Count;
					int num2 = 5;
					for (;;)
					{
						int num3;
						string rangeGlobalAddress;
						IXLSRange ixlsrange;
						IXLSRange ixlsrange2;
						switch (num2)
						{
						case 0:
							num2 = 4;
							continue;
						case 1:
							if (num >= count)
							{
								num2 = 17;
								continue;
							}
							num2 = 19;
							continue;
						case 2:
							rangeGlobalAddress = A_0[num3 + num, A_1.Column, num3 + num, A_1.LastColumn].RangeGlobalAddress;
							goto IL_316;
						case 3:
							num2 = 12;
							continue;
						case 4:
							ixlsrange = A_0[A_0.Row, A_0.Column + num, A_0.LastRow, A_0.Column + num];
							goto IL_2E2;
						case 5:
							goto IL_8B;
						case 6:
							if (num % 2 == 1)
							{
								num2 = 7;
								continue;
							}
							goto IL_175;
						case 7:
						{
							IChartSerie chartSerie = this.Series[num - 1];
							chartSerie.Bubbles = ixlsrange2;
							num2 = 8;
							continue;
						}
						case 8:
							goto IL_207;
						case 9:
							if (!this.ᝋ)
							{
								num2 = 3;
								continue;
							}
							num2 = 2;
							continue;
						case 10:
							ixlsrange = A_0[A_0.Row + num, A_0.Column, A_0.Row + num, A_0.LastColumn];
							goto IL_2E2;
						case 11:
							goto IL_207;
						case 12:
							rangeGlobalAddress = A_0[A_1.Row, num3 + num, A_1.LastRow, num3 + num].RangeGlobalAddress;
							goto IL_316;
						case 13:
							if (A_2 == RecordTableEnumerator.b("ȿ㝁♃⑅⑇⽉", a_))
							{
								num2 = 15;
								continue;
							}
							goto IL_175;
						case 14:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_8B;
							default:
								if (false)
								{
								}
								num2 = 18;
								continue;
							}
							break;
						case 15:
							if (true)
							{
							}
							num2 = 6;
							continue;
						case 16:
							goto IL_90;
						case 17:
							return;
						case 18:
							num3 += (this.ᝋ ? A_1.Row : A_1.Column);
							num2 = 9;
							continue;
						case 19:
							if (!this.ᝋ)
							{
								num2 = 0;
								continue;
							}
							num2 = 10;
							continue;
						case 20:
							if (A_1 != null)
							{
								num2 = 14;
								continue;
							}
							goto IL_207;
						}
						break;
						IL_90:
						num2 = 1;
						continue;
						IL_8B:
						goto IL_90;
						IL_175:
						XlsChartSerie xlsChartSerie = (XlsChartSerie)this.Series[num];
						xlsChartSerie.Bubbles = null;
						xlsChartSerie.SetDefaultName(this.\u1753.ᜆ(num));
						num3 = A_3;
						xlsChartSerie.Values = ixlsrange2;
						num2 = 20;
						continue;
						IL_207:
						num++;
						num2 = 16;
						continue;
						IL_2E2:
						ixlsrange2 = ixlsrange;
						num2 = 13;
						continue;
						IL_316:
						string str = rangeGlobalAddress;
						xlsChartSerie.Name = RecordTableEnumerator.b("紿", a_) + str;
						num2 = 11;
					}
				}
				return;
			}
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0006D300 File Offset: 0x0006C300
		private new bool ᜀ(Rectangle A_0, IXLSRange A_1, int A_2, string A_3)
		{
			int a_ = 2;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_1.LastRow == A_0.Bottom)
					{
						num = 17;
						continue;
					}
					goto IL_95;
				case 2:
					num = 16;
					continue;
				case 3:
					num = 4;
					continue;
				case 4:
					if (A_3.Length == 0)
					{
						num = 8;
						continue;
					}
					num = 24;
					continue;
				case 5:
					if (A_1.LastRow == A_0.Bottom + A_2)
					{
						num = 23;
						continue;
					}
					goto IL_20B;
				case 6:
					if (A_1.Row == A_0.Top)
					{
						num = 12;
						continue;
					}
					goto IL_95;
				case 7:
					goto IL_16F;
				case 8:
					goto IL_23D;
				case 9:
					goto IL_216;
				case 10:
					if (A_1.Column == A_0.Left + A_2)
					{
						num = 2;
						continue;
					}
					goto IL_95;
				case 11:
					num = 5;
					continue;
				case 12:
					if (true)
					{
					}
					num = 0;
					continue;
				case 13:
					return false;
				case 14:
					if (A_1.Row == A_0.Top + A_2)
					{
						num = 11;
						continue;
					}
					goto IL_20B;
				case 15:
					if (A_1.Worksheet.Name != A_3)
					{
						num = 18;
						continue;
					}
					num = 19;
					continue;
				case 16:
					goto IL_2C9;
				case 17:
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_BC;
						}
					}
					IL_BC:
					if (false)
					{
					}
					num = 10;
					continue;
				case 18:
					return false;
				case 19:
					if (!this.ᝋ)
					{
						num = 22;
						continue;
					}
					num = 14;
					continue;
				case 20:
					if (A_1.Column == A_0.Left)
					{
						num = 21;
						continue;
					}
					goto IL_20B;
				case 21:
					num = 7;
					continue;
				case 22:
					num = 6;
					continue;
				case 23:
					num = 20;
					continue;
				case 24:
					if (A_1 == null)
					{
						num = 13;
						continue;
					}
					num = 15;
					continue;
				case 25:
					goto IL_A0;
				}
				if (A_3 != null)
				{
					num = 3;
					continue;
				}
				goto IL_124;
				IL_95:
				num = 25;
				continue;
				IL_20B:
				num = 9;
			}
			IL_A0:
			return false;
			IL_124:
			throw new ArgumentNullException(RecordTableEnumerator.b("䬷丹主洽⠿❁⅃㉅ه⭉⅋⭍", a_));
			IL_16F:
			return A_1.LastColumn == A_0.Right;
			IL_216:
			return false;
			IL_23D:
			goto IL_124;
			IL_2C9:
			return A_1.LastColumn == A_0.Right + A_2;
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0006D5EC File Offset: 0x0006C5EC
		private new IXLSRange ᜀ(IXLSRange A_0, IXLSRange A_1, IWorksheet A_2, string A_3)
		{
			int a_ = 5;
			switch (0)
			{
			default:
			{
				int num = 17;
				Rectangle a_2;
				int num3;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						goto IL_33C;
					case 1:
						goto IL_341;
					case 2:
					{
						int count;
						if (count == 1)
						{
							num = 24;
							continue;
						}
						goto IL_341;
					}
					case 3:
						if (!this.ᜀ(a_2, A_0, num2 * 2 + num2, A_3))
						{
							num = 0;
							continue;
						}
						goto IL_CB;
					case 4:
						goto IL_38C;
					case 5:
					{
						bool flag;
						if (flag)
						{
							num = 12;
							continue;
						}
						goto IL_1D3;
					}
					case 6:
						goto IL_1D3;
					case 7:
						goto IL_C6;
					case 8:
						goto IL_3CC;
					case 9:
						goto IL_42C;
					case 10:
					{
						int count;
						if (num2 >= count)
						{
							num = 18;
							continue;
						}
						XlsChartSerie xlsChartSerie = (XlsChartSerie)this.Series[num2];
						A_0 = xlsChartSerie.Values;
						num = 15;
						continue;
					}
					case 11:
						num = 16;
						continue;
					case 12:
					{
						XlsChartSerie xlsChartSerie;
						A_1 = xlsChartSerie.Bubbles;
						num = 30;
						continue;
					}
					case 13:
					{
						bool flag;
						if (flag)
						{
							num = 26;
							continue;
						}
						goto IL_341;
					}
					case 14:
						goto IL_1AA;
					case 15:
					{
						bool flag;
						if (!this.ᜀ(a_2, A_0, flag ? (num2 * 2) : num2, A_3))
						{
							num = 20;
							continue;
						}
						num = 5;
						continue;
					}
					case 16:
						if (!this.ᜀ(a_2, A_0, num2 * 2 + num2, A_3))
						{
							num = 9;
							continue;
						}
						num3++;
						num = 6;
						continue;
					case 18:
						num = 27;
						continue;
					case 19:
						num = 28;
						continue;
					case 20:
						goto IL_3A9;
					case 21:
						if (A_1 != null)
						{
							num = 11;
							continue;
						}
						goto IL_1D3;
					case 22:
					{
						if (A_2 == null)
						{
							num = 8;
							continue;
						}
						a_2 = sprṔ.ᜀ(A_0, true);
						bool flag = this.ChartStartType == RecordTableEnumerator.b("示䠼崾⍀⽂⁄", a_);
						int count = this.Series.Count;
						num3 = 0;
						num = 13;
						continue;
					}
					case 23:
						num = 3;
						continue;
					case 24:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_38C;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num3++;
							num = 1;
							continue;
						}
						break;
					case 25:
					{
						int count;
						if (count == num2)
						{
							num = 33;
							continue;
						}
						goto IL_1D3;
					}
					case 26:
						num = 32;
						continue;
					case 27:
						if (!this.ᝋ)
						{
							num = 4;
							continue;
						}
						goto IL_431;
					case 28:
						if (!this.ᜀ(a_2, A_1, 1, A_3))
						{
							num = 31;
							continue;
						}
						num = 2;
						continue;
					case 29:
						goto IL_1AA;
					case 30:
					{
						int count;
						if (count - num2 > 1)
						{
							num = 23;
							continue;
						}
						goto IL_CB;
					}
					case 31:
						goto IL_13B;
					case 32:
						if (A_1 != null)
						{
							num = 19;
							continue;
						}
						goto IL_341;
					case 33:
						num = 21;
						continue;
					}
					if (A_0 == null)
					{
						num = 7;
						continue;
					}
					num = 22;
					continue;
					IL_CB:
					num = 25;
					continue;
					IL_1AA:
					num = 10;
					continue;
					IL_1D3:
					num2++;
					num = 29;
					continue;
					IL_341:
					num2 = 1;
					num = 14;
				}
				IL_C6:
				throw new ArgumentNullException(RecordTableEnumerator.b("场尼䰾㕀ᅂ⑄⥆⹈⹊", a_));
				IL_13B:
				return null;
				IL_33C:
				return null;
				IL_38C:
				return A_2[a_2.Top, a_2.Left, A_0.LastRow, A_0.LastColumn + num3];
				IL_3A9:
				return null;
				IL_3CC:
				throw new ArgumentNullException(RecordTableEnumerator.b("䠺唼娾⑀㝂", a_));
				IL_42C:
				return null;
				IL_431:
				return A_2[a_2.Top, a_2.Left, A_0.LastRow + num3, A_0.LastColumn];
			}
			}
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0006DA4C File Offset: 0x0006CA4C
		private new bool ᜀ(IXLSRange A_0, IXLSRange A_1, IWorksheet A_2, string A_3, out IXLSRange A_4)
		{
			int a_ = 19;
			switch (0)
			{
			default:
			{
				Rectangle a_2;
				int num;
				for (;;)
				{
					bool flag = A_0 == null;
					a_2 = new Rectangle(0, 0, 0, 0);
					bool flag2 = this.ChartStartType == RecordTableEnumerator.b("ୈ㹊⽌ⵎ㵐㙒", a_);
					A_4 = null;
					int count = this.Series.Count;
					num = 0;
					int num2 = 7;
					for (;;)
					{
						int num3;
						switch (num2)
						{
						case 0:
							num++;
							num2 = 3;
							continue;
						case 1:
							a_2 = sprṔ.ᜀ(A_0, true);
							num2 = 16;
							continue;
						case 2:
						{
							if (num3 >= count)
							{
								num2 = 14;
								continue;
							}
							XlsChartSerie xlsChartSerie = (XlsChartSerie)this.Series[num3];
							A_0 = xlsChartSerie.GetSerieNameRange();
							goto IL_1EC;
						}
						case 3:
							goto IL_100;
						case 4:
							num2 = 25;
							continue;
						case 5:
							if (count == 1)
							{
								num2 = 0;
								continue;
							}
							goto IL_100;
						case 6:
							if (flag != (A_0 == null))
							{
								if (true)
								{
								}
								num2 = 8;
								continue;
							}
							num2 = 21;
							continue;
						case 7:
							if (flag2)
							{
								num2 = 19;
								continue;
							}
							goto IL_100;
						case 8:
							return false;
						case 9:
							if (!flag)
							{
								num2 = 1;
								continue;
							}
							goto IL_268;
						case 10:
							num2 = 24;
							continue;
						case 11:
							if (A_1 != null)
							{
								num2 = 31;
								continue;
							}
							goto IL_100;
						case 12:
							if (!this.ᜀ(a_2, A_0, flag2 ? (num3 * 2) : num3, A_3))
							{
								num2 = 18;
								continue;
							}
							goto IL_3AC;
						case 13:
							goto IL_231;
						case 14:
							num2 = 23;
							continue;
						case 15:
							return true;
						case 16:
							goto IL_268;
						case 17:
							goto IL_2CF;
						case 18:
							return false;
						case 19:
							num2 = 11;
							continue;
						case 20:
							goto IL_279;
						case 21:
							if (flag2)
							{
								num2 = 4;
								continue;
							}
							goto IL_279;
						case 22:
							num2 = 12;
							continue;
						case 23:
							if (flag)
							{
								num2 = 15;
								continue;
							}
							num2 = 13;
							continue;
						case 24:
						{
							XlsChartSerie xlsChartSerie;
							if (xlsChartSerie.Bubbles != null)
							{
								num2 = 27;
								continue;
							}
							goto IL_279;
						}
						case 25:
							if (count - num3 > 1)
							{
								num2 = 10;
								continue;
							}
							goto IL_279;
						case 26:
							goto IL_2CF;
						case 27:
							num++;
							num2 = 20;
							continue;
						case 28:
							return false;
						case 29:
							if (A_0 != null)
							{
								num2 = 22;
								continue;
							}
							num2 = 30;
							continue;
						case 30:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1EC;
							default:
							{
								if (false)
								{
								}
								XlsChartSerie xlsChartSerie;
								if (!xlsChartSerie.IsDefaultName)
								{
									num2 = 28;
									continue;
								}
								goto IL_3AC;
							}
							}
							break;
						case 31:
							num2 = 5;
							continue;
						}
						break;
						IL_100:
						num2 = 9;
						continue;
						IL_1EC:
						num2 = 6;
						continue;
						IL_268:
						num3 = 1;
						num2 = 26;
						continue;
						IL_279:
						num2 = 29;
						continue;
						IL_2CF:
						num2 = 2;
						continue;
						IL_3AC:
						num3++;
						num2 = 17;
					}
				}
				return false;
				IL_231:
				A_4 = (this.ᝋ ? A_2[a_2.Top, a_2.Left, A_0.LastRow + num, A_0.LastColumn] : A_2[a_2.Top, a_2.Left, A_0.LastRow, A_0.LastColumn + num]);
				return true;
			}
			}
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0006DE58 File Offset: 0x0006CE58
		private new bool ᜀ(IXLSRange A_0, ref Rectangle A_1, bool A_2)
		{
			int a_ = 17;
			int num = 4;
			for (;;)
			{
				bool flag;
				bool flag2;
				switch (num)
				{
				case 0:
					A_1.Y = A_0.Row;
					A_1.Height++;
					num = 13;
					continue;
				case 1:
					goto IL_77;
				case 2:
					if (!A_2)
					{
						num = 16;
						continue;
					}
					num = 3;
					continue;
				case 3:
					if (A_1.Right == A_0.LastColumn)
					{
						num = 17;
						continue;
					}
					num = 18;
					continue;
				case 5:
					goto IL_F1;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_F1;
					default:
						if (false)
						{
						}
						if (A_0 != null)
						{
							num = 12;
							continue;
						}
						return true;
					}
					break;
				case 7:
					flag = (A_1.Left == A_0.Column + 1);
					goto IL_120;
				case 8:
					return false;
				case 9:
					flag = (A_1.Top == A_0.LastRow + 1);
					goto IL_120;
				case 10:
					return true;
				case 11:
					if (!flag2)
					{
						num = 8;
						continue;
					}
					num = 19;
					continue;
				case 12:
					num = 2;
					continue;
				case 13:
					goto IL_DF;
				case 14:
					if (A_1.Bottom == A_0.LastRow)
					{
						num = 15;
						continue;
					}
					num = 5;
					continue;
				case 15:
					num = 7;
					continue;
				case 16:
					num = 14;
					continue;
				case 17:
					num = 9;
					continue;
				case 18:
					flag = false;
					goto IL_120;
				case 19:
					if (A_2)
					{
						num = 0;
						continue;
					}
					A_1.X = A_0.Column;
					A_1.Width++;
					num = 10;
					continue;
				}
				if (A_0 == null)
				{
					num = 1;
					continue;
				}
				if (true)
				{
				}
				num = 6;
				continue;
				IL_120:
				flag2 = flag;
				num = 11;
				continue;
				IL_F1:
				flag = false;
				goto IL_120;
			}
			IL_77:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅆ⡈❊㡌⩎≐", a_));
			IL_DF:
			return true;
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0006E09C File Offset: 0x0006D09C
		public void DetectIsInRowOnParsing()
		{
			IXLSRange values;
			for (;;)
			{
				IL_18:
				int count = this.\u1753.Count;
				for (;;)
				{
					IL_24:
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_8F;
						case 1:
							if (values == null)
							{
								num = 0;
								continue;
							}
							goto IL_A1;
						case 2:
							goto IL_41;
						case 3:
							if (count == 0)
							{
								num = 2;
								continue;
							}
							values = this.\u1753[0].Values;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_24;
							default:
								if (false)
								{
								}
								num = 1;
								continue;
							}
							break;
						}
						goto IL_18;
					}
				}
			}
			IL_41:
			if (true)
			{
			}
			this.ᝋ = false;
			return;
			IL_8F:
			this.ᝋ = false;
			return;
			IL_A1:
			this.ᝋ = this.ᜀ(values);
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0006E158 File Offset: 0x0006D158
		internal void ᜁ(ExcelChartType A_0, bool A_1)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					for (;;)
					{
						this.TypeChanging = true;
						this.DestinationType = A_0;
						this.ᜀ(A_0, A_1);
						this.ᝈ = A_0;
						this.TypeChanging = false;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						}
						break;
					}
					IL_73:
					if (false)
					{
					}
					num = 1;
					continue;
					goto IL_73;
				case 1:
					goto IL_81;
				}
				if (this.ChartType == A_0)
				{
					break;
				}
				num = 0;
			}
			IL_81:
			if (true)
			{
			}
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0006E1F0 File Offset: 0x0006D1F0
		protected override SheetProtectionType PrepareProtectionOptions(SheetProtectionType options)
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
			return options |= SheetProtectionType.Scenarios;
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0006E234 File Offset: 0x0006D234
		public override object Clone(object parent)
		{
			XlsChart xlsChart;
			for (;;)
			{
				xlsChart = this.Clone(null, parent, null);
				if (true)
				{
				}
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						xlsChart.ParentWorkbook.InnerCharts.AddInternal(xlsChart);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return xlsChart;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 1:
						if (!this.ᝇ)
						{
							num = 0;
							continue;
						}
						return xlsChart;
					case 2:
						return xlsChart;
					}
					break;
				}
			}
			return xlsChart;
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0006E2C4 File Offset: 0x0006D2C4
		public XlsChart Clone(Dictionary<string, string> hashNewNames, object parent, Dictionary<int, int> dicFontIndexes)
		{
			XlsChart xlsChart;
			for (;;)
			{
				IL_B3:
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_35D:
					goto IL_444;
				default:
					if (false)
					{
					}
					xlsChart = (XlsChart)base.Clone(parent);
					xlsChart.SetParent(parent);
					xlsChart.FindParents();
					xlsChart.\u1752 = spr\u1CD3.ᜀ<spr\u1F17>(this.\u1752);
					num = 1;
					break;
				}
				for (;;)
				{
					IL_05:
					switch (num)
					{
					case 0:
						goto IL_4EF;
					case 1:
						if (this.\u1752 != null)
						{
							num = 39;
							continue;
						}
						goto IL_490;
					case 2:
						xlsChart.\u175A = this.\u175A.CloneAll();
						xlsChart.UpdateChartFontXIndexes(dicFontIndexes);
						num = 20;
						continue;
					case 3:
						goto IL_1BA;
					case 4:
						if (this.ᝍ != null)
						{
							num = 16;
							continue;
						}
						goto IL_4B6;
					case 5:
						if (this.\u1754 != null)
						{
							num = 10;
							continue;
						}
						goto IL_444;
					case 6:
						goto IL_362;
					case 7:
						if (this.ᜎ != null)
						{
							num = 27;
							continue;
						}
						goto IL_1BA;
					case 8:
						xlsChart.\u175D = this.\u175D.Clone(xlsChart, dicFontIndexes, hashNewNames);
						num = 9;
						continue;
					case 9:
						return xlsChart;
					case 10:
						xlsChart.\u1754 = this.\u1754.Clone(xlsChart);
						num = 36;
						continue;
					case 11:
						xlsChart.ᝡ = (XlsChartPlotArea)this.ᝡ.Clone(xlsChart);
						num = 18;
						continue;
					case 12:
						goto IL_1DD;
					case 13:
						xlsChart.\u1758 = this.\u1758.Clone(xlsChart);
						num = 23;
						continue;
					case 14:
						if (this.\u175B != null)
						{
							num = 15;
							continue;
						}
						goto IL_274;
					case 15:
						xlsChart.\u175B = this.\u175B.ᜀ(xlsChart, dicFontIndexes, hashNewNames);
						num = 32;
						continue;
					case 16:
						xlsChart.ᝍ = this.ᝍ.Clone(xlsChart);
						num = 30;
						continue;
					case 17:
						if (this.\u1758 != null)
						{
							num = 13;
							continue;
						}
						goto IL_3B6;
					case 18:
						goto IL_31D;
					case 19:
						xlsChart.m_title = (XlsChartTextArea)this.m_title.Clone(xlsChart, dicFontIndexes, hashNewNames);
						if (true)
						{
						}
						num = 6;
						continue;
					case 20:
						goto IL_227;
					case 21:
						goto IL_46A;
					case 22:
						xlsChart.\u1755 = (spr\u2140)this.\u1755.Clone();
						num = 37;
						continue;
					case 23:
						goto IL_3B6;
					case 24:
						if (this.\u1755 != null)
						{
							num = 22;
							continue;
						}
						goto IL_170;
					case 25:
						if (this.\u1759 != null)
						{
							num = 34;
							continue;
						}
						goto IL_4EF;
					case 26:
						if (this.\u175D != null)
						{
							num = 8;
							continue;
						}
						return xlsChart;
					case 27:
						xlsChart.ᜎ = spr\u1CD3.ᜀ(this.ᜎ);
						num = 3;
						continue;
					case 28:
						if (this.\u175A != null)
						{
							num = 2;
							continue;
						}
						goto IL_227;
					case 29:
						goto IL_490;
					case 30:
						goto IL_4B6;
					case 31:
						xlsChart.\u1753 = this.\u1753.Clone(xlsChart, hashNewNames, dicFontIndexes);
						num = 12;
						continue;
					case 32:
						goto IL_274;
					case 33:
						if (this.\u175C != null)
						{
							num = 38;
							continue;
						}
						goto IL_46A;
					case 34:
						xlsChart.\u1759 = this.\u1759.Clone(xlsChart);
						num = 0;
						continue;
					case 35:
						if (this.ᝡ != null)
						{
							num = 11;
							continue;
						}
						goto IL_31D;
					case 36:
						goto IL_35D;
					case 37:
						goto IL_170;
					case 38:
						xlsChart.\u175C = this.\u175C.ᜀ(xlsChart, dicFontIndexes, hashNewNames);
						num = 21;
						continue;
					case 39:
						xlsChart.ᜀ(dicFontIndexes);
						num = 29;
						continue;
					case 40:
						if (this.\u1753 != null)
						{
							num = 31;
							continue;
						}
						goto IL_1DD;
					case 41:
						if (this.m_title != null)
						{
							num = 19;
							continue;
						}
						goto IL_362;
					}
					goto IL_B3;
					IL_170:
					num = 35;
					continue;
					IL_1BA:
					num = 24;
					continue;
					IL_1DD:
					num = 41;
					continue;
					IL_227:
					num = 4;
					continue;
					IL_274:
					num = 33;
					continue;
					IL_31D:
					num = 5;
					continue;
					IL_362:
					num = 14;
					continue;
					IL_3B6:
					num = 28;
					continue;
					IL_46A:
					num = 26;
					continue;
					IL_490:
					num = 7;
					continue;
					IL_4B6:
					this.\u1757 = (spr\u23BE)spr\u1CD3.ᜀ(this.\u1757);
					num = 25;
					continue;
					IL_4EF:
					this.\u1756 = (sprᥦ)spr\u1CD3.ᜀ(this.\u1756);
					num = 40;
				}
				IL_444:
				num = 17;
				goto IL_05;
			}
			return xlsChart;
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0006E800 File Offset: 0x0006D800
		public void ChangePrimaryAxis(bool isParsing)
		{
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 2;
					continue;
				case 1:
					if (!this.PrimaryFormats.NeedSecondaryAxis)
					{
						num = 3;
						continue;
					}
					goto IL_D3;
				case 2:
					if (this.SecondaryFormats.Count == 0)
					{
						return;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D3;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 3:
					return;
				case 4:
					num = 1;
					continue;
				case 5:
					num = 7;
					continue;
				case 7:
					if (this.PrimaryFormats.Count <= 1)
					{
						num = 4;
						continue;
					}
					return;
				}
				if (!isParsing)
				{
					goto IL_D3;
				}
				if (true)
				{
				}
				num = 0;
			}
			return;
			IL_D3:
			this.PrimaryParentAxis.ᜆ().ChangeCollections();
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0006E8F0 File Offset: 0x0006D8F0
		internal new void ᜀ(IDictionary A_0)
		{
			switch (0)
			{
			default:
			{
				int num = 1;
				for (;;)
				{
					int num2;
					int count;
					switch (num)
					{
					case 0:
						if (this.\u1752 == null)
						{
							num = 6;
							continue;
						}
						num2 = 0;
						count = this.\u1752.Count;
						num = 7;
						continue;
					case 2:
						goto IL_108;
					case 3:
						return;
					case 4:
						goto IL_FC;
					case 5:
						if (true)
						{
						}
						num = 0;
						continue;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_108;
						default:
							goto IL_EA;
						}
						break;
					case 7:
						goto IL_FC;
					}
					if (A_0 != null)
					{
						num = 5;
						continue;
					}
					return;
					IL_108:
					if (num2 >= count)
					{
						num = 3;
						continue;
					}
					spr\u1F17 spr_u1F = this.\u1752[num2];
					int num3 = (int)spr_u1F.ᜃ();
					int num4 = (int)A_0[num3];
					spr_u1F.ᜃ((ushort)num4);
					num2++;
					num = 4;
					continue;
					IL_FC:
					num = 2;
				}
				IL_EA:
				if (false)
				{
				}
				return;
			}
			}
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0006EA1C File Offset: 0x0006DA1C
		public void UpdateChartFontXIndexes(IDictionary dicFontIndexes)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					IL_0E:
					int num = 15;
					for (;;)
					{
						int num3;
						int num4;
						switch (num)
						{
						case 0:
							return;
						case 1:
						{
							int num2;
							if (dicFontIndexes.Contains(num2))
							{
								num = 11;
								continue;
							}
							goto IL_80;
						}
						case 2:
							goto IL_1A7;
						case 3:
						{
							int count;
							if (num3 >= count)
							{
								num = 4;
								continue;
							}
							List<BiffRecordRaw> byIndex;
							spr\u2241 spr_u = byIndex[num3] as spr\u2241;
							num = 6;
							continue;
						}
						case 4:
							goto IL_EA;
						case 5:
							goto IL_80;
						case 6:
						{
							spr\u2241 spr_u;
							if (spr_u != null)
							{
								num = 16;
								continue;
							}
							goto IL_80;
						}
						case 7:
						{
							if (this.\u175A == null)
							{
								num = 2;
								continue;
							}
							num4 = 0;
							int count2 = this.\u175A.Count;
							num = 13;
							continue;
						}
						case 8:
						{
							num3 = 0;
							List<BiffRecordRaw> byIndex;
							int count = byIndex.Count;
							num = 17;
							continue;
						}
						case 9:
						{
							int count2;
							if (num4 < count2)
							{
								List<BiffRecordRaw> byIndex = this.\u175A.GetByIndex(num4);
								num = 12;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_0E;
							default:
								if (false)
								{
								}
								num = 0;
								continue;
							}
							break;
						}
						case 10:
							goto IL_1AC;
						case 11:
						{
							int num2;
							int num5 = (int)dicFontIndexes[num2];
							spr\u2241 spr_u;
							spr_u.ᜀ((ushort)num5);
							num = 5;
							continue;
						}
						case 12:
						{
							if (true)
							{
							}
							List<BiffRecordRaw> byIndex;
							if (byIndex != null)
							{
								num = 8;
								continue;
							}
							goto IL_EA;
						}
						case 13:
							goto IL_12C;
						case 14:
							num = 7;
							continue;
						case 16:
						{
							spr\u2241 spr_u;
							int num2 = (int)spr_u.ᜀ();
							num = 1;
							continue;
						}
						case 17:
							goto IL_1AC;
						case 18:
							goto IL_12C;
						}
						if (dicFontIndexes != null)
						{
							num = 14;
							continue;
						}
						return;
						IL_80:
						num3++;
						num = 10;
						continue;
						IL_EA:
						num4++;
						num = 18;
						continue;
						IL_12C:
						num = 9;
						continue;
						IL_1AC:
						num = 3;
					}
				}
				return;
				IL_1A7:
				return;
			}
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0006EC64 File Offset: 0x0006DC64
		public bool CheckForSupportGridLine()
		{
			switch (0)
			{
			default:
			{
				ExcelChartType chartType;
				for (;;)
				{
					if (true)
					{
					}
					int num;
					int num2;
					XlsChartSeries u;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
					{
						IL_DD:
						IChartSerie chartSerie;
						if (Array.IndexOf<ExcelChartType>(XlsChart.ᜬ, chartSerie.SerieType) != -1)
						{
							num = 4;
						}
						else
						{
							num2++;
							num = 3;
						}
						break;
					}
					default:
						if (false)
						{
						}
						chartType = this.ChartType;
						u = this.\u1753;
						num = 1;
						break;
					}
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_8F;
						case 1:
							if (chartType == ExcelChartType.CombinationChart)
							{
								num = 2;
								continue;
							}
							goto IL_114;
						case 2:
						{
							num2 = 0;
							int count = u.Count;
							num = 0;
							continue;
						}
						case 3:
							goto IL_8F;
						case 4:
							return false;
						case 5:
						{
							int count;
							if (num2 >= count)
							{
								num = 6;
								continue;
							}
							IChartSerie chartSerie = u[num2];
							num = 7;
							continue;
						}
						case 6:
							return true;
						case 7:
							goto IL_DD;
						}
						break;
						IL_8F:
						num = 5;
					}
				}
				return true;
				IL_114:
				return Array.IndexOf<ExcelChartType>(XlsChart.ᜬ, chartType) == -1;
			}
			}
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0006ED94 File Offset: 0x0006DD94
		public void SetToDefaultGridlines(ExcelChartType type)
		{
			for (;;)
			{
				IChartAxis chartAxis = this.\u175B.ᜅ();
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_11B;
					case 1:
						if (chartAxis != null)
						{
							num = 2;
							continue;
						}
						goto IL_77;
					case 2:
						if (true)
						{
						}
						chartAxis.HasMinorGridLines = false;
						chartAxis.HasMajorGridLines = false;
						num = 9;
						continue;
					case 3:
						goto IL_BD;
					case 4:
						chartAxis.HasMinorGridLines = false;
						chartAxis.HasMajorGridLines = false;
						num = 3;
						continue;
					case 5:
						if (chartAxis != null)
						{
							num = 4;
							continue;
						}
						goto IL_BD;
					case 6:
						goto IL_EC;
					case 7:
						if (Array.IndexOf<ExcelChartType>(XlsChart.ᜬ, type) == -1)
						{
							num = 6;
							continue;
						}
						return;
					case 8:
						if (chartAxis != null)
						{
							num = 10;
							continue;
						}
						goto IL_11B;
					case 9:
						goto IL_77;
					case 10:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_EC;
						default:
							if (false)
							{
							}
							chartAxis.HasMinorGridLines = false;
							chartAxis.HasMajorGridLines = false;
							num = 0;
							continue;
						}
						break;
					case 11:
						return;
					}
					break;
					IL_77:
					chartAxis = this.\u175B.ᜃ();
					num = 8;
					continue;
					IL_BD:
					chartAxis = this.\u175B.ᜂ();
					num = 1;
					continue;
					IL_EC:
					chartAxis.HasMajorGridLines = true;
					num = 11;
					continue;
					IL_11B:
					num = 7;
				}
			}
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0006EF0C File Offset: 0x0006DF0C
		public override void UpdateFormula(int iCurIndex, int iSourceIndex, Rectangle sourceRect, int iDestIndex, Rectangle destRect)
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
			this.\u1753.UpdateFormula(iCurIndex, iSourceIndex, sourceRect, iDestIndex, destRect);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0006EF5C File Offset: 0x0006DF5C
		public override void MarkUsedReferences(bool[] usedItems)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.m_title.ᜀ(usedItems);
					num = 8;
					continue;
				case 1:
					goto IL_93;
				case 2:
					this.\u175B.ᜀ(usedItems);
					num = 7;
					continue;
				case 4:
					goto IL_EF;
				case 5:
					if (this.\u175B != null)
					{
						num = 2;
						continue;
					}
					goto IL_6E;
				case 6:
					this.\u1753.ᜀ(usedItems);
					num = 1;
					continue;
				case 7:
					goto IL_6E;
				case 8:
					return;
				case 9:
					IL_91:
					this.\u175C.ᜀ(usedItems);
					num = 4;
					continue;
				case 10:
					if (true)
					{
					}
					if (this.\u175C != null)
					{
						num = 9;
						continue;
					}
					goto IL_EF;
				case 11:
					if (this.m_title != null)
					{
						num = 0;
						continue;
					}
					return;
				}
				if (this.\u1753 != null)
				{
					num = 6;
					continue;
				}
				goto IL_93;
				IL_6E:
				num = 10;
				continue;
				IL_EF:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_91;
				default:
					if (false)
					{
					}
					num = 11;
					continue;
				}
				IL_93:
				num = 5;
			}
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0006F0B0 File Offset: 0x0006E0B0
		public override void UpdateReferenceIndexes(int[] arrUpdatedIndexes)
		{
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.\u175B != null)
					{
						num = 6;
						continue;
					}
					goto IL_6E;
				case 1:
					if (this.\u175C != null)
					{
						num = 11;
						continue;
					}
					goto IL_EC;
				case 2:
					goto IL_90;
				case 3:
					this.m_title.ᜀ(arrUpdatedIndexes);
					num = 9;
					continue;
				case 4:
					goto IL_EC;
				case 5:
					this.\u1753.ᜀ(arrUpdatedIndexes);
					num = 2;
					continue;
				case 6:
					this.\u175B.ᜀ(arrUpdatedIndexes);
					num = 10;
					continue;
				case 7:
					if (this.m_title != null)
					{
						num = 3;
						continue;
					}
					return;
				case 9:
					return;
				case 10:
					goto IL_6E;
				case 11:
					IL_86:
					if (true)
					{
					}
					this.\u175C.ᜀ(arrUpdatedIndexes);
					num = 4;
					continue;
				}
				if (this.\u1753 != null)
				{
					num = 5;
					continue;
				}
				goto IL_90;
				IL_6E:
				num = 1;
				continue;
				IL_EC:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_86;
				default:
					if (false)
					{
					}
					num = 7;
					continue;
				}
				IL_90:
				num = 0;
			}
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0006F204 File Offset: 0x0006E204
		internal void ᜣ()
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.\u175C.ᜀ(false);
					num = 5;
					continue;
				case 1:
					goto IL_45;
				case 2:
					this.\u175C.ᜀ(true);
					num = 1;
					continue;
				case 3:
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
						if (this.IsCategoryAxisAvail)
						{
							num = 2;
							continue;
						}
						goto IL_45;
					}
					break;
				case 5:
					return;
				case 6:
					num = 3;
					continue;
				case 7:
					if (this.IsValueAxisAvail)
					{
						num = 0;
						continue;
					}
					return;
				}
				if (this.IsSecondaryAxes)
				{
					num = 6;
					continue;
				}
				break;
				IL_45:
				num = 7;
			}
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0006F2EC File Offset: 0x0006E2EC
		internal new static int ᜀ(double A_0)
		{
			ushort num;
			double num2;
			for (;;)
			{
				switch (0)
				{
				default:
					for (;;)
					{
						num = (ushort)A_0;
						num2 = (A_0 - (double)num) * 100000.0;
						int num3 = 0;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								if (num2 > 65535.0)
								{
									num3 = 1;
									continue;
								}
								goto IL_9A;
							case 1:
								num2 /= 10.0;
								num3 = 2;
								continue;
							case 2:
								goto IL_6A;
							}
							break;
						}
					}
					IL_6A:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_80;
					}
					break;
				}
			}
			IL_80:
			if (false)
			{
			}
			if (true)
			{
			}
			IL_9A:
			byte[] bytes = BitConverter.GetBytes(num);
			byte[] bytes2 = BitConverter.GetBytes((ushort)num2);
			byte[] array = new byte[4];
			bytes.CopyTo(array, 2);
			bytes2.CopyTo(array, 0);
			return BitConverter.ToInt32(array, 0);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0006F3C4 File Offset: 0x0006E3C4
		internal new static double ᜀ(int A_0)
		{
			int num;
			int value;
			for (;;)
			{
				IL_00:
				switch (0)
				{
				default:
					for (;;)
					{
						if (true)
						{
						}
						byte[] bytes = BitConverter.GetBytes(A_0);
						num = (int)BitConverter.ToUInt16(bytes, 0);
						value = (int)BitConverter.ToUInt16(bytes, 2);
						int num2 = 0;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								if (num == 0)
								{
									num2 = 1;
									continue;
								}
								num2 = 3;
								continue;
							case 1:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_00;
								default:
									if (false)
									{
									}
									num2 = 2;
									continue;
								}
								break;
							case 2:
								goto IL_A4;
							case 3:
								goto IL_66;
							}
							break;
						}
					}
					break;
				}
			}
			IL_66:
			int num3 = (int)Math.Log10((double)num) + 1;
			goto IL_A7;
			IL_A4:
			num3 = 0;
			IL_A7:
			int num4 = num3;
			double num5 = (double)Math.Abs(value) + (double)num / Math.Pow(10.0, (double)num4);
			return num5 * (double)Math.Sign(value);
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0006F4A4 File Offset: 0x0006E4A4
		internal XlsChartSeriesAxis ᜦ()
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
			XlsChartSeriesAxis result;
			this.\u175B.ᜀ(result = new ChartSeriesAxis((spr\u2158)base.AppImplementation, this.\u175B, AxisType.Serie));
			return result;
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0006F504 File Offset: 0x0006E504
		// Note: this type is marked as 'beforefieldinit'.
		static XlsChart()
		{
			int a_ = 2;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			XlsChart.ᜥ = new string[]
			{
				RecordTableEnumerator.b("样匹夻", a_),
				RecordTableEnumerator.b("簷唹䤻夽⠿ⱁㅃ㉅", a_),
				RecordTableEnumerator.b("欷伹主堽ℿ⅁⅃", a_)
			};
			XlsChart.DEF_SUPPORT_SERIES_AXIS = new ExcelChartType[]
			{
				ExcelChartType.Surface3D,
				ExcelChartType.SurfaceContour,
				ExcelChartType.SurfaceContourNoColor,
				ExcelChartType.Surface3DNoColor,
				ExcelChartType.Column3D,
				ExcelChartType.Line3D,
				ExcelChartType.Area3D,
				ExcelChartType.Pyramid3DClustered,
				ExcelChartType.Cone3DClustered,
				ExcelChartType.Cylinder3DClustered
			};
			XlsChart.ᜦ = new ExcelChartType[]
			{
				ExcelChartType.ScatterLine,
				ExcelChartType.ScatterLineMarkers,
				ExcelChartType.ScatterMarkers,
				ExcelChartType.ScatterSmoothedLine,
				ExcelChartType.ScatterSmoothedLineMarkers,
				ExcelChartType.StockHighLowClose,
				ExcelChartType.StockOpenHighLowClose,
				ExcelChartType.StockVolumeHighLowClose,
				ExcelChartType.StockVolumeOpenHighLowClose,
				ExcelChartType.Bubble,
				ExcelChartType.Bubble3D
			};
			XlsChart.ᜧ = new string[]
			{
				RecordTableEnumerator.b("笷唹倻䬽ⴿⱁ", a_),
				RecordTableEnumerator.b("稷嬹主", a_),
				RecordTableEnumerator.b("琷匹刻嬽", a_),
				RecordTableEnumerator.b("礷䠹夻弽", a_),
				RecordTableEnumerator.b("笷䌹倻圽⸿♁⅃㑅", a_),
				RecordTableEnumerator.b("笷唹刻嬽", a_),
				RecordTableEnumerator.b("样䌹主弽ⴿ⭁⁃", a_),
				RecordTableEnumerator.b("欷丹医崽⬿", a_)
			};
			XlsChart.DEF_SUPPORT_ERROR_BARS = new string[]
			{
				RecordTableEnumerator.b("笷唹倻䬽ⴿⱁ", a_),
				RecordTableEnumerator.b("稷嬹主", a_),
				RecordTableEnumerator.b("琷匹刻嬽", a_),
				RecordTableEnumerator.b("礷䠹夻弽", a_),
				RecordTableEnumerator.b("欷夹崻䨽㐿❁㙃", a_),
				RecordTableEnumerator.b("稷伹帻尽ⰿ❁", a_)
			};
			XlsChart.DEF_SUPPORT_TREND_LINES = new ExcelChartType[]
			{
				ExcelChartType.ColumnClustered,
				ExcelChartType.BarClustered,
				ExcelChartType.Line,
				ExcelChartType.LineMarkers,
				ExcelChartType.ScatterLine,
				ExcelChartType.ScatterLineMarkers,
				ExcelChartType.ScatterMarkers,
				ExcelChartType.ScatterSmoothedLine,
				ExcelChartType.ScatterSmoothedLineMarkers,
				ExcelChartType.StockHighLowClose,
				ExcelChartType.StockOpenHighLowClose,
				ExcelChartType.StockVolumeHighLowClose,
				ExcelChartType.StockVolumeOpenHighLowClose,
				ExcelChartType.Area,
				ExcelChartType.Bubble,
				ExcelChartType.Bubble3D
			};
			XlsChart.ᜨ = new ExcelChartType[]
			{
				ExcelChartType.Column3D,
				ExcelChartType.Column3DClustered,
				ExcelChartType.Column3D100PercentStacked,
				ExcelChartType.Column3DStacked,
				ExcelChartType.Bar3DClustered,
				ExcelChartType.Bar3DStacked,
				ExcelChartType.Bar3D100PercentStacked,
				ExcelChartType.Line3D,
				ExcelChartType.Area3D,
				ExcelChartType.Area3DStacked,
				ExcelChartType.Area3D100PercentStacked,
				ExcelChartType.CylinderClustered,
				ExcelChartType.CylinderStacked,
				ExcelChartType.Cylinder100PercentStacked,
				ExcelChartType.CylinderBarClustered,
				ExcelChartType.CylinderBarStacked,
				ExcelChartType.CylinderBar100PercentStacked,
				ExcelChartType.Cylinder3DClustered,
				ExcelChartType.ConeClustered,
				ExcelChartType.ConeStacked,
				ExcelChartType.Cone100PercentStacked,
				ExcelChartType.ConeBarClustered,
				ExcelChartType.ConeBarStacked,
				ExcelChartType.ConeBar100PercentStacked,
				ExcelChartType.Cone3DClustered,
				ExcelChartType.PyramidClustered,
				ExcelChartType.PyramidStacked,
				ExcelChartType.Pyramid100PercentStacked,
				ExcelChartType.PyramidBarClustered,
				ExcelChartType.PyramidBarStacked,
				ExcelChartType.PyramidBar100PercentStacked,
				ExcelChartType.Pyramid3DClustered,
				ExcelChartType.Surface3D,
				ExcelChartType.Surface3DNoColor
			};
			XlsChart.ᜩ = new AxisType[]
			{
				AxisType.Category,
				AxisType.Value
			};
			XlsChart.ᜪ = new ExcelChartType[]
			{
				ExcelChartType.ScatterMarkers,
				ExcelChartType.ScatterSmoothedLineMarkers,
				ExcelChartType.ScatterSmoothedLine,
				ExcelChartType.ScatterLineMarkers,
				ExcelChartType.ScatterLine,
				ExcelChartType.Doughnut,
				ExcelChartType.DoughnutExploded,
				ExcelChartType.Radar,
				ExcelChartType.RadarMarkers,
				ExcelChartType.RadarFilled,
				ExcelChartType.Bubble,
				ExcelChartType.Bubble3D,
				ExcelChartType.StockHighLowClose,
				ExcelChartType.StockOpenHighLowClose,
				ExcelChartType.StockVolumeHighLowClose,
				ExcelChartType.StockVolumeOpenHighLowClose,
				ExcelChartType.CombinationChart
			};
			XlsChart.ᜫ = new ExcelChartType[]
			{
				ExcelChartType.ColumnClustered,
				ExcelChartType.ColumnStacked,
				ExcelChartType.Column100PercentStacked,
				ExcelChartType.BarClustered,
				ExcelChartType.BarStacked,
				ExcelChartType.Bar100PercentStacked,
				ExcelChartType.Line,
				ExcelChartType.LineStacked,
				ExcelChartType.Line100PercentStacked,
				ExcelChartType.LineMarkers,
				ExcelChartType.LineMarkersStacked,
				ExcelChartType.LineMarkers100PercentStacked,
				ExcelChartType.Pie,
				ExcelChartType.PieOfPie,
				ExcelChartType.PieExploded,
				ExcelChartType.PieBar,
				ExcelChartType.ScatterMarkers,
				ExcelChartType.ScatterSmoothedLineMarkers,
				ExcelChartType.ScatterSmoothedLine,
				ExcelChartType.ScatterLineMarkers,
				ExcelChartType.ScatterLine,
				ExcelChartType.Area,
				ExcelChartType.AreaStacked,
				ExcelChartType.Area100PercentStacked,
				ExcelChartType.Doughnut,
				ExcelChartType.DoughnutExploded,
				ExcelChartType.Radar,
				ExcelChartType.RadarMarkers,
				ExcelChartType.RadarFilled,
				ExcelChartType.Bubble,
				ExcelChartType.Bubble3D
			};
			XlsChart.ᜬ = new ExcelChartType[]
			{
				ExcelChartType.Doughnut,
				ExcelChartType.DoughnutExploded,
				ExcelChartType.PieBar,
				ExcelChartType.PieExploded,
				ExcelChartType.PieOfPie,
				ExcelChartType.Pie,
				ExcelChartType.Pie3D,
				ExcelChartType.Pie3DExploded
			};
			XlsChart.ᜭ = new ExcelChartType[]
			{
				ExcelChartType.Doughnut,
				ExcelChartType.DoughnutExploded,
				ExcelChartType.PieBar,
				ExcelChartType.PieExploded,
				ExcelChartType.PieOfPie,
				ExcelChartType.Pie,
				ExcelChartType.Radar,
				ExcelChartType.RadarMarkers,
				ExcelChartType.RadarFilled,
				ExcelChartType.BarClustered,
				ExcelChartType.BarStacked,
				ExcelChartType.Bar100PercentStacked
			};
			XlsChart.ᜮ = new ExcelChartType[]
			{
				ExcelChartType.StockHighLowClose,
				ExcelChartType.StockOpenHighLowClose,
				ExcelChartType.StockVolumeHighLowClose,
				ExcelChartType.StockVolumeOpenHighLowClose,
				ExcelChartType.CombinationChart
			};
			XlsChart.ᜯ = new string[]
			{
				RecordTableEnumerator.b("样匹夻", a_),
				RecordTableEnumerator.b("簷唹䤻夽⠿ⱁㅃ㉅", a_),
				RecordTableEnumerator.b("樷嬹堻弽㈿", a_),
				RecordTableEnumerator.b("礷䠹夻弽", a_),
				RecordTableEnumerator.b("笷唹倻䬽ⴿⱁ", a_),
				RecordTableEnumerator.b("稷嬹主", a_),
				RecordTableEnumerator.b("琷匹刻嬽", a_),
				RecordTableEnumerator.b("欷夹崻䨽㐿❁㙃", a_)
			};
			XlsChart.ᜰ = new ExcelChartType[]
			{
				ExcelChartType.Radar,
				ExcelChartType.RadarMarkers,
				ExcelChartType.RadarFilled,
				ExcelChartType.ScatterMarkers,
				ExcelChartType.ScatterSmoothedLineMarkers,
				ExcelChartType.ScatterSmoothedLine,
				ExcelChartType.ScatterLineMarkers,
				ExcelChartType.ScatterLine,
				ExcelChartType.Line,
				ExcelChartType.LineStacked,
				ExcelChartType.Line100PercentStacked,
				ExcelChartType.LineMarkers,
				ExcelChartType.LineMarkersStacked,
				ExcelChartType.LineMarkers100PercentStacked,
				ExcelChartType.Bubble,
				ExcelChartType.Bubble3D
			};
			XlsChart.ᜱ = new ExcelChartType[]
			{
				ExcelChartType.Doughnut,
				ExcelChartType.DoughnutExploded,
				ExcelChartType.PieBar,
				ExcelChartType.PieExploded,
				ExcelChartType.PieOfPie,
				ExcelChartType.Pie,
				ExcelChartType.Pie3D,
				ExcelChartType.Pie3DExploded,
				ExcelChartType.Radar,
				ExcelChartType.RadarMarkers,
				ExcelChartType.RadarFilled,
				ExcelChartType.SurfaceContour,
				ExcelChartType.SurfaceContourNoColor
			};
			XlsChart.\u1732 = new ExcelChartType[]
			{
				ExcelChartType.Surface3D,
				ExcelChartType.SurfaceContour,
				ExcelChartType.Surface3DNoColor,
				ExcelChartType.SurfaceContourNoColor
			};
			XlsChart.\u1733 = new ExcelChartType[]
			{
				ExcelChartType.Column100PercentStacked,
				ExcelChartType.Column3D100PercentStacked,
				ExcelChartType.Bar100PercentStacked,
				ExcelChartType.Bar3D100PercentStacked,
				ExcelChartType.Line100PercentStacked,
				ExcelChartType.LineMarkers100PercentStacked,
				ExcelChartType.Area100PercentStacked,
				ExcelChartType.Area3D100PercentStacked,
				ExcelChartType.Cylinder100PercentStacked,
				ExcelChartType.CylinderBar100PercentStacked,
				ExcelChartType.Cone100PercentStacked,
				ExcelChartType.ConeBar100PercentStacked,
				ExcelChartType.Pyramid100PercentStacked,
				ExcelChartType.PyramidBar100PercentStacked
			};
			XlsChart.\u1734 = new ExcelChartType[]
			{
				ExcelChartType.ColumnStacked,
				ExcelChartType.Column3DStacked,
				ExcelChartType.BarStacked,
				ExcelChartType.Bar3DStacked,
				ExcelChartType.LineStacked,
				ExcelChartType.LineMarkersStacked,
				ExcelChartType.AreaStacked,
				ExcelChartType.Area3DStacked,
				ExcelChartType.CylinderStacked,
				ExcelChartType.CylinderBarStacked,
				ExcelChartType.ConeStacked,
				ExcelChartType.ConeBarStacked,
				ExcelChartType.PyramidStacked,
				ExcelChartType.PyramidBarStacked,
				ExcelChartType.Column100PercentStacked,
				ExcelChartType.Column3D100PercentStacked,
				ExcelChartType.Bar100PercentStacked,
				ExcelChartType.Bar3D100PercentStacked,
				ExcelChartType.Line100PercentStacked,
				ExcelChartType.LineMarkers100PercentStacked,
				ExcelChartType.Area100PercentStacked,
				ExcelChartType.Area3D100PercentStacked,
				ExcelChartType.Cylinder100PercentStacked,
				ExcelChartType.CylinderBar100PercentStacked,
				ExcelChartType.Cone100PercentStacked,
				ExcelChartType.ConeBar100PercentStacked,
				ExcelChartType.Pyramid100PercentStacked,
				ExcelChartType.PyramidBar100PercentStacked
			};
			XlsChart.\u1735 = new ExcelChartType[]
			{
				ExcelChartType.Column3DClustered,
				ExcelChartType.Column3DStacked,
				ExcelChartType.Column3D100PercentStacked,
				ExcelChartType.Column3D,
				ExcelChartType.Bar3DClustered,
				ExcelChartType.Bar3DStacked,
				ExcelChartType.Bar3D100PercentStacked,
				ExcelChartType.Line3D,
				ExcelChartType.Pie3D,
				ExcelChartType.Pie3DExploded,
				ExcelChartType.Area3D,
				ExcelChartType.Area3DStacked,
				ExcelChartType.Area3D100PercentStacked,
				ExcelChartType.Surface3D,
				ExcelChartType.Surface3DNoColor,
				ExcelChartType.SurfaceContour,
				ExcelChartType.SurfaceContourNoColor,
				ExcelChartType.CylinderClustered,
				ExcelChartType.CylinderStacked,
				ExcelChartType.Cylinder100PercentStacked,
				ExcelChartType.CylinderBarClustered,
				ExcelChartType.CylinderBarStacked,
				ExcelChartType.CylinderBar100PercentStacked,
				ExcelChartType.Cylinder3DClustered,
				ExcelChartType.ConeClustered,
				ExcelChartType.ConeStacked,
				ExcelChartType.Cone100PercentStacked,
				ExcelChartType.ConeBarClustered,
				ExcelChartType.ConeBarStacked,
				ExcelChartType.ConeBar100PercentStacked,
				ExcelChartType.Cone3DClustered,
				ExcelChartType.PyramidClustered,
				ExcelChartType.PyramidStacked,
				ExcelChartType.Pyramid100PercentStacked,
				ExcelChartType.PyramidBarClustered,
				ExcelChartType.PyramidBarStacked,
				ExcelChartType.PyramidBar100PercentStacked,
				ExcelChartType.Pyramid3DClustered
			};
			XlsChart.\u1736 = new ExcelChartType[]
			{
				ExcelChartType.Line,
				ExcelChartType.Line3D,
				ExcelChartType.LineMarkers,
				ExcelChartType.LineMarkersStacked,
				ExcelChartType.LineMarkers100PercentStacked,
				ExcelChartType.LineStacked,
				ExcelChartType.Line100PercentStacked
			};
			XlsChart.\u1737 = new ExcelChartType[]
			{
				ExcelChartType.Bubble,
				ExcelChartType.Bubble3D
			};
			XlsChart.\u1738 = new ExcelChartType[]
			{
				ExcelChartType.Doughnut,
				ExcelChartType.DoughnutExploded,
				ExcelChartType.Pie,
				ExcelChartType.Pie3D,
				ExcelChartType.PieBar,
				ExcelChartType.PieExploded,
				ExcelChartType.Pie3DExploded,
				ExcelChartType.PieOfPie
			};
			XlsChart.\u1739 = new ExcelChartType[]
			{
				ExcelChartType.Doughnut,
				ExcelChartType.DoughnutExploded,
				ExcelChartType.Pie,
				ExcelChartType.Pie3D,
				ExcelChartType.PieBar,
				ExcelChartType.PieExploded,
				ExcelChartType.Pie3DExploded,
				ExcelChartType.PieOfPie
			};
			XlsChart.\u173A = new ExcelChartType[]
			{
				ExcelChartType.DoughnutExploded,
				ExcelChartType.PieExploded,
				ExcelChartType.Pie3DExploded
			};
			XlsChart.\u173B = new ExcelChartType[]
			{
				ExcelChartType.PieOfPie,
				ExcelChartType.PieBar
			};
			XlsChart.\u173C = new ExcelChartType[]
			{
				ExcelChartType.ScatterMarkers,
				ExcelChartType.ScatterLineMarkers,
				ExcelChartType.ScatterLine,
				ExcelChartType.ScatterSmoothedLineMarkers,
				ExcelChartType.ScatterSmoothedLine
			};
			XlsChart.\u173D = new ExcelChartType[]
			{
				ExcelChartType.ScatterSmoothedLineMarkers,
				ExcelChartType.ScatterSmoothedLine
			};
			XlsChart.\u173E = new ExcelChartType[]
			{
				ExcelChartType.StockHighLowClose,
				ExcelChartType.StockOpenHighLowClose,
				ExcelChartType.StockVolumeHighLowClose,
				ExcelChartType.StockVolumeOpenHighLowClose
			};
			XlsChart.\u173F = new ExcelChartType[]
			{
				ExcelChartType.Area3D,
				ExcelChartType.Column3D,
				ExcelChartType.Cone3DClustered,
				ExcelChartType.Cylinder3DClustered,
				ExcelChartType.Line3D,
				ExcelChartType.Pyramid3DClustered,
				ExcelChartType.Surface3D,
				ExcelChartType.Surface3DNoColor,
				ExcelChartType.SurfaceContour,
				ExcelChartType.SurfaceContourNoColor
			};
			XlsChart.ᝀ = new ExcelChartType[]
			{
				ExcelChartType.BarClustered,
				ExcelChartType.Bar3DClustered,
				ExcelChartType.ColumnClustered,
				ExcelChartType.Column3DClustered,
				ExcelChartType.ConeClustered,
				ExcelChartType.ConeBarClustered,
				ExcelChartType.CylinderClustered,
				ExcelChartType.CylinderBarClustered,
				ExcelChartType.PyramidClustered,
				ExcelChartType.PyramidBarClustered
			};
			XlsChart.ᝁ = new ExcelChartType[]
			{
				ExcelChartType.ColumnClustered,
				ExcelChartType.ScatterLineMarkers,
				ExcelChartType.ScatterSmoothedLine,
				ExcelChartType.ScatterSmoothedLineMarkers,
				ExcelChartType.LineMarkers100PercentStacked,
				ExcelChartType.StockVolumeOpenHighLowClose,
				ExcelChartType.LineMarkersStacked,
				ExcelChartType.StockVolumeHighLowClose,
				ExcelChartType.Column100PercentStacked,
				ExcelChartType.StockOpenHighLowClose,
				ExcelChartType.ScatterLineMarkers,
				ExcelChartType.Area100PercentStacked,
				ExcelChartType.Line100PercentStacked,
				ExcelChartType.LineMarkers,
				ExcelChartType.StockHighLowClose,
				ExcelChartType.ColumnStacked,
				ExcelChartType.BarClustered,
				ExcelChartType.Bar100PercentStacked,
				ExcelChartType.AreaStacked,
				ExcelChartType.LineStacked,
				ExcelChartType.BarStacked,
				ExcelChartType.Bubble3D,
				ExcelChartType.ScatterMarkers,
				ExcelChartType.Bubble,
				ExcelChartType.Area,
				ExcelChartType.Line
			};
			XlsChart.ᝂ = new LegendPositionType[]
			{
				LegendPositionType.Right,
				LegendPositionType.Corner,
				LegendPositionType.Left
			};
			byte[][] array = new byte[14][];
			array[0] = new byte[]
			{
				80,
				8,
				0,
				0,
				10,
				10,
				3,
				0,
				80,
				8,
				90,
				8,
				97,
				8,
				97,
				8,
				106,
				8,
				107,
				8
			};
			array[1] = new byte[]
			{
				82,
				8,
				0,
				0,
				13,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			};
			byte[][] array2 = array;
			int num = 2;
			byte[] array3 = new byte[12];
			array3[0] = 82;
			array3[1] = 8;
			array2[num] = array3;
			array[3] = new byte[]
			{
				82,
				8,
				0,
				0,
				5,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			};
			byte[][] array4 = array;
			int num2 = 4;
			byte[] array5 = new byte[12];
			array5[0] = 106;
			array5[1] = 8;
			array4[num2] = array5;
			array[5] = new byte[]
			{
				84,
				8,
				0,
				0,
				18,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			};
			array[6] = new byte[]
			{
				81,
				8,
				0,
				0,
				36,
				16,
				2,
				0,
				0,
				0,
				0,
				0
			};
			array[7] = new byte[]
			{
				81,
				8,
				0,
				0,
				37,
				16,
				32,
				0,
				2,
				2,
				1,
				0,
				0,
				0,
				0,
				0,
				169,
				254,
				byte.MaxValue,
				byte.MaxValue,
				187,
				254,
				byte.MaxValue,
				byte.MaxValue,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				177,
				0,
				77,
				0,
				80,
				40,
				0,
				0
			};
			array[8] = new byte[]
			{
				81,
				8,
				0,
				0,
				51,
				16,
				0,
				0,
				0,
				0,
				0,
				0
			};
			array[9] = new byte[]
			{
				81,
				8,
				0,
				0,
				79,
				16,
				20,
				0,
				2,
				0,
				2,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			};
			array[10] = new byte[]
			{
				81,
				8,
				0,
				0,
				81,
				16,
				8,
				0,
				0,
				1,
				0,
				0,
				0,
				0,
				0,
				0
			};
			array[11] = new byte[]
			{
				81,
				8,
				0,
				0,
				39,
				16,
				6,
				0,
				4,
				0,
				0,
				0,
				0,
				0
			};
			array[12] = new byte[]
			{
				81,
				8,
				0,
				0,
				52,
				16,
				0,
				0,
				0,
				0,
				0,
				0
			};
			array[13] = new byte[]
			{
				85,
				8,
				0,
				0,
				18,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			};
			XlsChart.ᝃ = array;
			XlsChart.ᝄ = new ExcelChartType[]
			{
				ExcelChartType.RadarFilled,
				ExcelChartType.Area,
				ExcelChartType.Area3D,
				ExcelChartType.AreaStacked,
				ExcelChartType.Area100PercentStacked,
				ExcelChartType.Area3D100PercentStacked,
				ExcelChartType.Area3DStacked
			};
			XlsChart.ᝅ = new ExcelChartType[]
			{
				ExcelChartType.Pie,
				ExcelChartType.Pie3D,
				ExcelChartType.PieBar,
				ExcelChartType.PieExploded,
				ExcelChartType.Pie3DExploded,
				ExcelChartType.PieOfPie,
				ExcelChartType.Doughnut,
				ExcelChartType.DoughnutExploded
			};
		}

		// Token: 0x040001D7 RID: 471
		internal new const string ᜀ = "Serie1";

		// Token: 0x040001D8 RID: 472
		internal new const ExcelChartType ᜁ = ExcelChartType.ColumnClustered;

		// Token: 0x040001D9 RID: 473
		internal new const string ᜂ = "3D";

		// Token: 0x040001DA RID: 474
		internal new const string ᜃ = "Bar";

		// Token: 0x040001DB RID: 475
		internal new const string ᜄ = "Clustered";

		// Token: 0x040001DC RID: 476
		internal new const string ᜅ = "Contour";

		// Token: 0x040001DD RID: 477
		internal const string ᜆ = "Exploded";

		// Token: 0x040001DE RID: 478
		internal const string ᜇ = "Line";

		// Token: 0x040001DF RID: 479
		internal const string ᜈ = "Markers";

		// Token: 0x040001E0 RID: 480
		internal const string ᜉ = "NoColor";

		// Token: 0x040001E1 RID: 481
		internal const string ᜊ = "100Percent";

		// Token: 0x040001E2 RID: 482
		internal const string ᜋ = "SmoothedLine";

		// Token: 0x040001E3 RID: 483
		internal const string ᜌ = "Stacked";

		// Token: 0x040001E4 RID: 484
		internal const string \u170D = "Area";

		// Token: 0x040001E5 RID: 485
		internal new const string ᜎ = "Bar";

		// Token: 0x040001E6 RID: 486
		internal new const string ᜏ = "Bubble";

		// Token: 0x040001E7 RID: 487
		internal new const string ᜐ = "Column";

		// Token: 0x040001E8 RID: 488
		internal new const string ᜑ = "Cone";

		// Token: 0x040001E9 RID: 489
		internal const string \u1712 = "Cylinder";

		// Token: 0x040001EA RID: 490
		internal const string \u1713 = "Doughnut";

		// Token: 0x040001EB RID: 491
		internal const string \u1714 = "Line";

		// Token: 0x040001EC RID: 492
		internal const string \u1715 = "Pie";

		// Token: 0x040001ED RID: 493
		internal const string \u1716 = "Pyramid";

		// Token: 0x040001EE RID: 494
		internal const string \u1717 = "Radar";

		// Token: 0x040001EF RID: 495
		internal const string \u1718 = "Scatter";

		// Token: 0x040001F0 RID: 496
		internal const string \u1719 = "Surface";

		// Token: 0x040001F1 RID: 497
		internal const string \u171A = "Stock";

		// Token: 0x040001F2 RID: 498
		private const int \u171B = 0;

		// Token: 0x040001F3 RID: 499
		internal const int \u171C = 1;

		// Token: 0x040001F4 RID: 500
		internal const int \u171D = 2;

		// Token: 0x040001F5 RID: 501
		internal new const int \u171E = 3;

		// Token: 0x040001F6 RID: 502
		private const int \u171F = 1;

		// Token: 0x040001F7 RID: 503
		private new const int ᜠ = 506;

		// Token: 0x040001F8 RID: 504
		internal const int ᜡ = 328;

		// Token: 0x040001F9 RID: 505
		internal const int ᜢ = 243;

		// Token: 0x040001FA RID: 506
		internal const int ᜣ = 3125;

		// Token: 0x040001FB RID: 507
		internal const int ᜤ = 3283;

		// Token: 0x040001FC RID: 508
		internal static readonly string[] ᜥ;

		// Token: 0x040001FD RID: 509
		public static readonly ExcelChartType[] DEF_SUPPORT_SERIES_AXIS;

		// Token: 0x040001FE RID: 510
		internal static readonly ExcelChartType[] ᜦ;

		// Token: 0x040001FF RID: 511
		internal static readonly string[] ᜧ;

		// Token: 0x04000200 RID: 512
		public static readonly string[] DEF_SUPPORT_ERROR_BARS;

		// Token: 0x04000201 RID: 513
		public static readonly ExcelChartType[] DEF_SUPPORT_TREND_LINES;

		// Token: 0x04000202 RID: 514
		internal static readonly ExcelChartType[] ᜨ;

		// Token: 0x04000203 RID: 515
		private static readonly AxisType[] ᜩ;

		// Token: 0x04000204 RID: 516
		internal static readonly ExcelChartType[] ᜪ;

		// Token: 0x04000205 RID: 517
		internal static readonly ExcelChartType[] ᜫ;

		// Token: 0x04000206 RID: 518
		internal static readonly ExcelChartType[] ᜬ;

		// Token: 0x04000207 RID: 519
		internal static readonly ExcelChartType[] ᜭ;

		// Token: 0x04000208 RID: 520
		internal static readonly ExcelChartType[] ᜮ;

		// Token: 0x04000209 RID: 521
		internal static readonly string[] ᜯ;

		// Token: 0x0400020A RID: 522
		internal static readonly ExcelChartType[] ᜰ;

		// Token: 0x0400020B RID: 523
		internal static readonly ExcelChartType[] ᜱ;

		// Token: 0x0400020C RID: 524
		internal static readonly ExcelChartType[] \u1732;

		// Token: 0x0400020D RID: 525
		internal static readonly ExcelChartType[] \u1733;

		// Token: 0x0400020E RID: 526
		internal static readonly ExcelChartType[] \u1734;

		// Token: 0x0400020F RID: 527
		internal static readonly ExcelChartType[] \u1735;

		// Token: 0x04000210 RID: 528
		internal static readonly ExcelChartType[] \u1736;

		// Token: 0x04000211 RID: 529
		internal static readonly ExcelChartType[] \u1737;

		// Token: 0x04000212 RID: 530
		internal static readonly ExcelChartType[] \u1738;

		// Token: 0x04000213 RID: 531
		internal new static readonly ExcelChartType[] \u1739;

		// Token: 0x04000214 RID: 532
		internal static readonly ExcelChartType[] \u173A;

		// Token: 0x04000215 RID: 533
		private static readonly ExcelChartType[] \u173B;

		// Token: 0x04000216 RID: 534
		internal static readonly ExcelChartType[] \u173C;

		// Token: 0x04000217 RID: 535
		internal static readonly ExcelChartType[] \u173D;

		// Token: 0x04000218 RID: 536
		internal static readonly ExcelChartType[] \u173E;

		// Token: 0x04000219 RID: 537
		internal static readonly ExcelChartType[] \u173F;

		// Token: 0x0400021A RID: 538
		internal static readonly ExcelChartType[] ᝀ;

		// Token: 0x0400021B RID: 539
		internal static readonly ExcelChartType[] ᝁ;

		// Token: 0x0400021C RID: 540
		private byte[] \u25D8\u0099\u00A4\u00AD;

		// Token: 0x0400021D RID: 541
		internal static readonly LegendPositionType[] ᝂ;

		// Token: 0x0400021E RID: 542
		private static readonly byte[][] ᝃ;

		// Token: 0x0400021F RID: 543
		internal static readonly ExcelChartType[] ᝄ;

		// Token: 0x04000220 RID: 544
		internal static readonly ExcelChartType[] ᝅ;

		// Token: 0x04000221 RID: 545
		private bool ᝆ;

		// Token: 0x04000222 RID: 546
		private bool ᝇ;

		// Token: 0x04000223 RID: 547
		private ExcelChartType ᝈ;

		// Token: 0x04000224 RID: 548
		private ExcelChartType ᝉ;

		// Token: 0x04000225 RID: 549
		private IXLSRange ᝊ;

		// Token: 0x04000226 RID: 550
		private bool ᝋ = true;

		// Token: 0x04000227 RID: 551
		private bool ᝌ;

		// Token: 0x04000228 RID: 552
		private XlsChartPageSetup ᝍ;

		// Token: 0x04000229 RID: 553
		private double ᝎ;

		// Token: 0x0400022A RID: 554
		private double ᝏ;

		// Token: 0x0400022B RID: 555
		private double ᝐ;

		// Token: 0x0400022C RID: 556
		private double ᝑ;

		// Token: 0x0400022D RID: 557
		private List<spr\u1F17> \u1752;

		// Token: 0x0400022E RID: 558
		private XlsChartSeries \u1753;

		// Token: 0x0400022F RID: 559
		private ChartDataTableXls \u1754;

		// Token: 0x04000230 RID: 560
		private spr\u2140 \u1755;

		// Token: 0x04000231 RID: 561
		private sprᥦ \u1756;

		// Token: 0x04000232 RID: 562
		private spr\u23BE \u1757;

		// Token: 0x04000233 RID: 563
		private XlsChartFrameFormat \u1758;

		// Token: 0x04000234 RID: 564
		private XlsChartFrameFormat \u1759;

		// Token: 0x04000235 RID: 565
		private TypedSortedListEx<int, List<BiffRecordRaw>> \u175A = new TypedSortedListEx<int, List<BiffRecordRaw>>();

		// Token: 0x04000236 RID: 566
		protected internal XlsChartTextArea m_title;

		// Token: 0x04000237 RID: 567
		private sprᾹ \u175B;

		// Token: 0x04000238 RID: 568
		private sprᾹ \u175C;

		// Token: 0x04000239 RID: 569
		private XlsChartLegend \u175D;

		// Token: 0x0400023A RID: 570
		private bool \u175E;

		// Token: 0x0400023B RID: 571
		private XlsChartWallOrFloor \u175F;

		// Token: 0x0400023C RID: 572
		private XlsChartWallOrFloor ᝠ;

		// Token: 0x0400023D RID: 573
		private XlsChartPlotArea ᝡ;

		// Token: 0x0400023E RID: 574
		private bool ᝢ;

		// Token: 0x0400023F RID: 575
		private ExcelChartType ᝣ;

		// Token: 0x04000240 RID: 576
		private List<BiffRecordRaw> ᝤ = new List<BiffRecordRaw>();

		// Token: 0x04000241 RID: 577
		private List<BiffRecordRaw> ᝥ;

		// Token: 0x04000242 RID: 578
		private spr\u1CF7 ᝦ;

		// Token: 0x04000243 RID: 579
		private RelationsCollection ᝧ;

		// Token: 0x04000244 RID: 580
		private int ᝨ;

		// Token: 0x04000245 RID: 581
		private Stream ᝩ;

		// Token: 0x04000246 RID: 582
		private bool ᝪ;

		// Token: 0x04000247 RID: 583
		private Dictionary<int, List<BiffRecordRaw>> ᝫ;

		// Token: 0x04000248 RID: 584
		private Stream ᝬ;

		// Token: 0x04000249 RID: 585
		private PivotTable \u176D;

		// Token: 0x0400024A RID: 586
		private string ᝮ;

		// Token: 0x0400024B RID: 587
		private bool ᝯ = true;

		// Token: 0x0400024C RID: 588
		private bool ᝰ = true;

		// Token: 0x0400024D RID: 589
		private bool \u1771 = true;

		// Token: 0x0400024E RID: 590
		private bool \u1772 = true;

		// Token: 0x0400024F RID: 591
		private bool \u1773 = true;

		// Token: 0x04000250 RID: 592
		private Stream \u1774;

		// Token: 0x04000251 RID: 593
		private bool \u1775;

		// Token: 0x04000252 RID: 594
		private Stream \u1776;

		// Token: 0x04000253 RID: 595
		private FontWrapper \u1777;

		// Token: 0x04000254 RID: 596
		private bool? \u1778;

		// Token: 0x04000255 RID: 597
		private List<int> \u1779;

		// Token: 0x04000256 RID: 598
		private Stream \u177A;
	}
}
