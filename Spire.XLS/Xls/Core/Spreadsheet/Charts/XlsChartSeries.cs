using System;
using System.Collections.Generic;
using System.Drawing;
using Spire.Xls.Charts;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Charts
{
	// Token: 0x02000181 RID: 385
	public class XlsChartSeries : CollectionExtended<IChartSerie>, IChartSeries, ICloneParent, IList<IChartSerie>
	{
		// Token: 0x06001242 RID: 4674 RVA: 0x000B1854 File Offset: 0x000B0854
		internal XlsChartSeries(spr\u1DF5 A_0, object A_1)
		{
			int a_ = 5;
			this.ᜁ = new List<IRecordStorage>();
			this.ᜂ = new List<IRecordStorage>();
			this.ᜅ = new List<IChartSerie>();
			base..ctor(A_0, A_1);
			this.m_chart = (XlsChart)base.FindParent(typeof(XlsChart));
			if (this.m_chart == null)
			{
				throw new ApplicationException(RecordTableEnumerator.b("欺尼䴾⑀ⵂㅄ杆♈⥊❌⩎㉐❒畔㑖㡘㕚㍜ぞᕠ䍢ݤɦ䥨൪ɬᩮὰᝲ孴", a_));
			}
		}

		// Token: 0x1700066F RID: 1647
		protected internal new IChartSerie this[int index]
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
				return base.List[index];
			}
		}

		// Token: 0x17000670 RID: 1648
		protected internal IChartSerie this[string name]
		{
			get
			{
				IChartSerie chartSerie;
				for (;;)
				{
					int num = 0;
					int count = base.Count;
					int num2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_89:
						if (chartSerie.Name == name)
						{
							num2 = 4;
						}
						else
						{
							num++;
							num2 = 3;
						}
						break;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						num2 = 2;
						break;
					}
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (num >= count)
							{
								num2 = 1;
								continue;
							}
							chartSerie = base.List[num];
							num2 = 5;
							continue;
						case 1:
							goto IL_BE;
						case 2:
							goto IL_A4;
						case 3:
							goto IL_A4;
						case 4:
							return chartSerie;
						case 5:
							goto IL_89;
						}
						break;
						IL_A4:
						num2 = 0;
					}
				}
				return chartSerie;
				IL_BE:
				return null;
			}
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x000B19E4 File Offset: 0x000B09E4
		protected internal IChartSerie Add()
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
			ChartSerie chartSerie = new ChartSerie((spr\u2158)base.ReservedHandle, this);
			chartSerie.SetDefaultName(this.ᜀ());
			chartSerie.IsDefaultName = true;
			return this.Add(chartSerie);
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x000B1A4C File Offset: 0x000B0A4C
		protected internal IChartSerie Add(string name)
		{
			ChartSerie chartSerie;
			for (;;)
			{
				chartSerie = new ChartSerie((spr\u2158)base.ReservedHandle, this);
				chartSerie.Name = name;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.m_chart.ChartTitle = name;
						if (true)
						{
						}
						num = 1;
						continue;
					case 1:
						goto IL_91;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							if (this.m_chart.ChartTitle == null)
							{
								num = 0;
								continue;
							}
							goto IL_93;
						}
						break;
					}
					break;
				}
			}
			IL_91:
			IL_93:
			return this.Add(chartSerie);
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x000B1AF4 File Offset: 0x000B0AF4
		protected internal IChartSerie Add(ExcelChartType serieType)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			XlsChartSerie xlsChartSerie = (XlsChartSerie)this.Add();
			xlsChartSerie.ᜁ(serieType, true);
			return xlsChartSerie;
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x000B1B44 File Offset: 0x000B0B44
		protected internal IChartSerie Add(string name, ExcelChartType serieType)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			IChartSerie chartSerie = this.Add(name);
			this.IsSerieCreating = true;
			chartSerie.SerieType = serieType;
			this.IsSerieCreating = false;
			return chartSerie;
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x000B1BA0 File Offset: 0x000B0BA0
		public new void RemoveAt(int index)
		{
			int a_ = 12;
			switch (0)
			{
			default:
				for (;;)
				{
					int count = base.List.Count;
					int num = 1;
					for (;;)
					{
						bool flag;
						bool flag2;
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1B2;
							default:
								if (false)
								{
								}
								this.m_chart.ᜣ();
								num = 7;
								continue;
							}
							break;
						case 1:
							if (index >= 0)
							{
								num = 14;
								continue;
							}
							goto IL_214;
						case 2:
							if (this.m_chart.HasLegend)
							{
								num = 13;
								continue;
							}
							goto IL_135;
						case 3:
							flag = false;
							goto IL_1D7;
						case 4:
							num = 6;
							continue;
						case 5:
							if (flag2)
							{
								num = 12;
								continue;
							}
							goto IL_19F;
						case 6:
							flag = (count != 1);
							goto IL_1D7;
						case 7:
							goto IL_19A;
						case 8:
						{
							if (index >= count)
							{
								num = 16;
								continue;
							}
							if (true)
							{
							}
							XlsChartSerie xlsChartSerie = (XlsChartSerie)this[index];
							num = 2;
							continue;
						}
						case 9:
							goto IL_135;
						case 10:
						{
							XlsChartSerie xlsChartSerie;
							if (this.ᜅ(xlsChartSerie.ChartGroup) == 0)
							{
								num = 4;
								continue;
							}
							num = 3;
							continue;
						}
						case 11:
							goto IL_1B2;
						case 12:
						{
							XlsChartSerie xlsChartSerie;
							this.m_chart.RemoveFormat(xlsChartSerie.GetCommonSerieFormat());
							num = 15;
							continue;
						}
						case 13:
						{
							ChartLegendEntriesColl chartLegendEntriesColl = (ChartLegendEntriesColl)this.m_chart.Legend.LegendEntries;
							chartLegendEntriesColl.Remove(index);
							num = 9;
							continue;
						}
						case 14:
							num = 8;
							continue;
						case 15:
							goto IL_19F;
						case 16:
							goto IL_C9;
						}
						break;
						IL_135:
						base.RemoveAt(index);
						num = 10;
						continue;
						IL_19F:
						this.UpdateSerieIndexAfterRemove(index);
						num = 11;
						continue;
						IL_1B2:
						if (!this.ᜈ())
						{
							num = 0;
							continue;
						}
						return;
						IL_1D7:
						flag2 = flag;
						num = 5;
					}
				}
				IL_C9:
				goto IL_214;
				IL_19A:
				return;
				IL_214:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⭁⩃≅ⵇ㉉", a_));
			}
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x000B1DD8 File Offset: 0x000B0DD8
		public void Remove(string serieName)
		{
			int a_ = 13;
			int num = 0;
			for (;;)
			{
				int num2;
				int num3;
				switch (num)
				{
				case 1:
					goto IL_4B;
				case 2:
					goto IL_A1;
				case 3:
				{
					XlsChartSerie xlsChartSerie;
					if (xlsChartSerie.Name == serieName)
					{
						num = 4;
						continue;
					}
					goto IL_4D;
				}
				case 4:
				{
					XlsChartSerie xlsChartSerie;
					this.RemoveAt(xlsChartSerie.Index);
					num2--;
					num3--;
					num = 5;
					continue;
				}
				case 5:
					goto IL_4D;
				case 6:
				{
					if (num2 >= num3)
					{
						if (true)
						{
						}
						num = 7;
						continue;
					}
					XlsChartSerie xlsChartSerie = (XlsChartSerie)base.List[num2];
					num = 3;
					continue;
				}
				case 7:
					return;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8D;
					default:
						if (false)
						{
						}
						goto IL_A1;
					}
					break;
				}
				if (serieName == null)
				{
					num = 1;
					continue;
				}
				num2 = 0;
				num3 = base.Count;
				num = 8;
				continue;
				IL_4D:
				num2++;
				num = 2;
				continue;
				IL_A1:
				num = 6;
			}
			IL_4B:
			IL_8D:
			throw new ArgumentException(RecordTableEnumerator.b("あ⁄㕆⁈⹊͌⹎㱐㙒", a_));
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x000B1F0C File Offset: 0x000B0F0C
		internal new void ᜀ(IList<BiffRecordRaw> A_0, ref int A_1)
		{
			int a_ = 14;
			int num = 7;
			for (;;)
			{
				spr\u23A5 spr_u23A;
				BiffRecordRaw biffRecordRaw;
				switch (num)
				{
				case 0:
					goto IL_61;
				case 1:
				{
					int a_2;
					this.ᜀ(a_2, spr_u23A);
					num = 0;
					continue;
				}
				case 2:
					if (biffRecordRaw.TypeCode != TBIFFRecord.Blank)
					{
						num = 10;
						continue;
					}
					goto IL_88;
				case 3:
					goto IL_F3;
				case 4:
					if (biffRecordRaw.TypeCode != TBIFFRecord.Number)
					{
						num = 8;
						continue;
					}
					goto IL_88;
				case 5:
					goto IL_5C;
				case 6:
					if (biffRecordRaw.TypeCode != TBIFFRecord.Label)
					{
						num = 9;
						continue;
					}
					goto IL_88;
				case 8:
					if (true)
					{
					}
					num = 6;
					continue;
				case 9:
					num = 2;
					continue;
				case 10:
					return;
				case 11:
				{
					if (biffRecordRaw.TypeCode != TBIFFRecord.ChartSiIndex)
					{
						num = 3;
						continue;
					}
					int a_2 = (int)((spr\u220C)biffRecordRaw).ᜁ();
					A_1++;
					biffRecordRaw = A_0[A_1];
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						num = 12;
						continue;
					}
					break;
				}
				case 12:
					goto IL_61;
				case 13:
					if (spr_u23A.ᜅ() < base.Count)
					{
						num = 1;
						continue;
					}
					goto IL_61;
				}
				if (A_0 == null)
				{
					num = 5;
					continue;
				}
				biffRecordRaw = A_0[A_1];
				num = 11;
				continue;
				IL_61:
				num = 4;
				continue;
				IL_88:
				spr_u23A = (spr\u23A5)biffRecordRaw;
				A_1++;
				biffRecordRaw = A_0[A_1];
				num = 13;
			}
			IL_5C:
			throw new ArgumentNullException(RecordTableEnumerator.b("⁃❅㱇⭉", a_));
			IL_F3:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("݃⹅⥇㡉㡋ᵍ㥏᭑㩓㉕㵗≙籛ⱝ՟šୣᑥ౧䩩ཫ཭ṯᱱ᭳ɵ塷᡹᥻幽ꒉ", a_));
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x000B20F4 File Offset: 0x000B10F4
		public void SerializeDataToList(RecordArrayList records)
		{
			int a_ = 14;
			int num = 2;
			for (;;)
			{
				IEnumerator<IChartSerie> enumerator;
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					try
					{
						num = 0;
						for (;;)
						{
							switch (num)
							{
							case 1:
								goto IL_A7;
							case 3:
							{
								if (!enumerator.MoveNext())
								{
									num = 4;
									continue;
								}
								XlsChartSerie xlsChartSerie = (XlsChartSerie)enumerator.Current;
								xlsChartSerie.SerializeDataToList(records);
								num = 2;
								continue;
							}
							case 4:
								num = 1;
								continue;
							}
							IL_85:
							num = 3;
							continue;
							goto IL_85;
						}
						IL_A7:
						goto IL_168;
					}
					finally
					{
						num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_FF;
							case 1:
								enumerator.Dispose();
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_101;
								default:
									if (false)
									{
									}
									num = 0;
									continue;
								}
								break;
							}
							if (enumerator == null)
							{
								break;
							}
							num = 1;
						}
						IL_FF:
						IL_101:;
					}
					goto IL_102;
				case 1:
					goto IL_33;
				}
				if (records == null)
				{
					num = 1;
					continue;
				}
				IL_102:
				this.ᜁ.Clear();
				this.ᜂ.Clear();
				this.ᜃ = base.Count;
				this.ᜄ = base.Count;
				enumerator = base.List.GetEnumerator();
				num = 0;
			}
			IL_33:
			throw new ArgumentNullException(RecordTableEnumerator.b("㙃⍅⭇╉㹋⩍⍏", a_));
			IL_168:
			records.ᜀ(this.ᜁ);
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x000B2288 File Offset: 0x000B1288
		[CLSCompliant(false)]
		internal new void ᜀ(RecordArrayList A_0)
		{
			int a_ = 2;
			int num = 4;
			for (;;)
			{
				int num2;
				int count;
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_D0;
					}
					break;
				case 1:
					goto IL_9E;
				case 2:
				{
					if (num2 >= count)
					{
						num = 0;
						continue;
					}
					XlsChartSerie xlsChartSerie = (XlsChartSerie)base.InnerList[num2];
					xlsChartSerie.ᜄ(A_0);
					num2++;
					num = 1;
					continue;
				}
				case 3:
					goto IL_4E;
				case 4:
					if (true)
					{
					}
					break;
				case 5:
					goto IL_9E;
				}
				if (A_0 == null)
				{
					num = 3;
					continue;
				}
				num2 = 0;
				count = base.Count;
				num = 5;
				continue;
				IL_9E:
				num = 2;
			}
			IL_4E:
			throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹弻儽㈿♁㝃", a_));
			IL_D0:
			if (false)
			{
			}
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x000B236C File Offset: 0x000B136C
		public IChartSerie Add(XlsChartSerie serieToAdd)
		{
			int a_ = 5;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					serieToAdd.SetDefaultName(this.ᜀ());
					num = 7;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_57;
					default:
						if (false)
						{
						}
						if (serieToAdd.IsDefaultName)
						{
							num = 0;
							continue;
						}
						goto IL_10B;
					}
					break;
				case 3:
					goto IL_55;
				case 4:
					goto IL_57;
				case 5:
					return serieToAdd;
				case 6:
					return serieToAdd;
				case 7:
					goto IL_10B;
				case 8:
					if (!this.m_chart.ParentWorkbook.Loading)
					{
						num = 4;
						continue;
					}
					this.ᜅ.Add(serieToAdd);
					num = 5;
					continue;
				}
				if (serieToAdd == null)
				{
					num = 3;
					continue;
				}
				num = 2;
				continue;
				IL_57:
				serieToAdd.Number = serieToAdd.Index;
				this.ᜅ.Clear();
				num = 6;
				continue;
				IL_10B:
				base.Add(serieToAdd);
				serieToAdd.Index = base.List.Count - 1;
				num = 8;
			}
			IL_55:
			throw new ArgumentNullException(RecordTableEnumerator.b("䠺堼䴾⡀♂ᅄ⡆ࡈ⽊⥌", a_));
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x000B24D0 File Offset: 0x000B14D0
		protected override void OnClear()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			base.OnClear();
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x000B2514 File Offset: 0x000B1514
		public XlsChartSeries Clone(object parent, Dictionary<string, string> hashNewNames, Dictionary<int, int> fontIndexes)
		{
			switch (0)
			{
			default:
			{
				XlsChartSeries xlsChartSeries;
				for (;;)
				{
					IL_4B:
					xlsChartSeries = new ChartSeries((spr\u2158)base.ReservedHandle, parent);
					int num = 0;
					int count = base.InnerList.Count;
					int num2 = 3;
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
							switch (num2)
							{
							case 0:
								return xlsChartSeries;
							case 1:
							{
								if (num >= count)
								{
									goto IL_83;
								}
								XlsChartSerie xlsChartSerie = base.InnerList[num] as XlsChartSerie;
								XlsChartSerie serieToAdd = xlsChartSerie.Clone(xlsChartSeries, hashNewNames, fontIndexes);
								xlsChartSeries.Add(serieToAdd);
								num++;
								num2 = 2;
								continue;
							}
							case 2:
								goto IL_76;
							case 3:
								goto IL_76;
							}
							goto IL_4B;
							IL_76:
							num2 = 1;
							continue;
						}
						IL_83:
						num2 = 0;
					}
				}
				return xlsChartSeries;
			}
			}
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x000B25F4 File Offset: 0x000B15F4
		public override object Clone(object parent)
		{
			switch (0)
			{
			default:
			{
				XlsChartSeries xlsChartSeries;
				for (;;)
				{
					IL_43:
					xlsChartSeries = new ChartSeries((spr\u2158)base.ReservedHandle, parent);
					List<IChartSerie> innerList = base.InnerList;
					int num = 0;
					int count = innerList.Count;
					if (true)
					{
					}
					int num2 = 1;
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
							switch (num2)
							{
							case 0:
								return xlsChartSeries;
							case 1:
								goto IL_78;
							case 2:
							{
								if (num >= count)
								{
									goto IL_85;
								}
								XlsChartSerie xlsChartSerie = innerList[num] as XlsChartSerie;
								XlsChartSerie serieToAdd = xlsChartSerie.Clone(xlsChartSeries, null, null);
								xlsChartSeries.Add(serieToAdd);
								num++;
								num2 = 3;
								continue;
							}
							case 3:
								goto IL_78;
							}
							goto IL_43;
							IL_78:
							num2 = 2;
							continue;
						}
						IL_85:
						num2 = 0;
					}
				}
				return xlsChartSeries;
			}
			}
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x000B26D4 File Offset: 0x000B16D4
		internal int ᜅ(int A_0)
		{
			int num;
			for (;;)
			{
				num = 0;
				int num2 = 0;
				int count = base.List.Count;
				int num3 = 5;
				for (;;)
				{
					if (true)
					{
					}
					switch (num3)
					{
					case 0:
						return num;
					case 1:
						if (((XlsChartSerie)base.List[num2]).ChartGroup == A_0)
						{
							num3 = 6;
							continue;
						}
						goto IL_46;
					case 2:
						goto IL_46;
					case 3:
						if (num2 >= count)
						{
							num3 = 0;
							continue;
						}
						num3 = 1;
						continue;
					case 4:
						goto IL_B9;
					case 5:
						goto IL_B9;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_B9;
						default:
							if (false)
							{
							}
							num++;
							num3 = 2;
							continue;
						}
						break;
					}
					break;
					IL_46:
					num2++;
					num3 = 4;
					continue;
					IL_B9:
					num3 = 3;
				}
			}
			return num;
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x000B27B8 File Offset: 0x000B17B8
		internal new List<XlsChartSerie> ᜂ(int A_0)
		{
			switch (0)
			{
			default:
			{
				List<XlsChartSerie> list;
				for (;;)
				{
					IL_33:
					list = new List<XlsChartSerie>();
					int num = 0;
					int count = base.List.Count;
					int num2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_6E:
						num++;
						num2 = 3;
						break;
					default:
						if (false)
						{
						}
						num2 = 0;
						break;
					}
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_CE;
						case 1:
						{
							if (num >= count)
							{
								num2 = 6;
								continue;
							}
							XlsChartSerie xlsChartSerie = (XlsChartSerie)base.List[num];
							num2 = 4;
							continue;
						}
						case 2:
						{
							XlsChartSerie xlsChartSerie;
							list.Add(xlsChartSerie);
							num2 = 5;
							continue;
						}
						case 3:
							goto IL_CE;
						case 4:
						{
							XlsChartSerie xlsChartSerie;
							if (xlsChartSerie.ChartGroup == A_0)
							{
								num2 = 2;
								continue;
							}
							goto IL_6E;
						}
						case 5:
							goto IL_8D;
						case 6:
							return list;
						}
						goto IL_33;
						IL_CE:
						if (true)
						{
						}
						num2 = 1;
					}
					IL_8D:
					goto IL_6E;
				}
				return list;
			}
			}
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x000B28BC File Offset: 0x000B18BC
		internal new int ᜀ(ExcelChartType A_0, bool A_1)
		{
			switch (0)
			{
			default:
			{
				int num;
				for (;;)
				{
					num = 0;
					int num2 = 0;
					int count = base.List.Count;
					int num3 = 8;
					for (;;)
					{
						switch (num3)
						{
						case 0:
						{
							XlsChartSerie xlsChartSerie;
							if (xlsChartSerie.UsePrimaryAxis == A_1)
							{
								num3 = 5;
								continue;
							}
							goto IL_60;
						}
						case 1:
						{
							XlsChartSerie xlsChartSerie;
							if (xlsChartSerie.SerieType == A_0)
							{
								num3 = 2;
								continue;
							}
							goto IL_60;
						}
						case 2:
							num3 = 0;
							continue;
						case 3:
							goto IL_BB;
						case 4:
						{
							if (num2 >= count)
							{
								num3 = 7;
								continue;
							}
							XlsChartSerie xlsChartSerie = (XlsChartSerie)base.List[num2];
							num3 = 1;
							continue;
						}
						case 5:
							if (true)
							{
							}
							num++;
							num3 = 6;
							continue;
						case 6:
							goto IL_60;
						case 7:
							return num;
						case 8:
							goto IL_BB;
						}
						break;
						IL_60:
						num2++;
						num3 = 3;
						continue;
						IL_BB:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_60;
						default:
							if (false)
							{
							}
							num3 = 4;
							break;
						}
					}
				}
				return num;
			}
			}
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x000B29E8 File Offset: 0x000B19E8
		internal new int ᜀ(ExcelChartType A_0)
		{
			switch (0)
			{
			default:
			{
				int num;
				for (;;)
				{
					for (;;)
					{
						string b = XlsChartFormat.ᜉ(A_0);
						num = 0;
						int num2 = 0;
						int count = base.List.Count;
						int num3 = 0;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								goto IL_CE;
							case 1:
								goto IL_EA;
							case 2:
								if (num2 >= count)
								{
									num3 = 1;
									continue;
								}
								num3 = 4;
								continue;
							case 3:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (false)
									{
									}
									num++;
									num3 = 5;
									continue;
								}
								break;
							case 4:
								if (((XlsChartSerie)base.List[num2]).StartType == b)
								{
									num3 = 3;
									continue;
								}
								goto IL_5F;
							case 5:
								goto IL_5F;
							case 6:
								goto IL_CE;
							}
							break;
							IL_5F:
							num2++;
							num3 = 6;
							continue;
							IL_CE:
							num3 = 2;
						}
					}
				}
				IL_EA:
				if (true)
				{
				}
				return num;
			}
			}
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x000B2AEC File Offset: 0x000B1AEC
		internal void ᜉ()
		{
			for (;;)
			{
				if (true)
				{
				}
				int num = 0;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return;
					case 1:
						goto IL_2C;
					case 2:
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						}
						if (false)
						{
						}
						if (num >= base.Count)
						{
							num2 = 0;
							continue;
						}
						XlsChartSerie xlsChartSerie = (XlsChartSerie)base.List[num];
						xlsChartSerie.ChartGroup = 0;
						((XlsChartDataPointsCollection)xlsChartSerie.DataPoints).Clear();
						num++;
						num2 = 1;
						continue;
					}
					case 3:
						goto IL_2C;
					}
					break;
					IL_2C:
					num2 = 2;
				}
			}
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x000B2BA0 File Offset: 0x000B1BA0
		public int FindOrderByType(ExcelChartType type)
		{
			int a_ = 6;
			switch (0)
			{
			default:
			{
				int num3;
				for (;;)
				{
					Dictionary<string, object> dictionary = new Dictionary<string, object>();
					Dictionary<int, object> dictionary2 = new Dictionary<int, object>(5);
					int num = 0;
					int num2 = 3;
					for (;;)
					{
						int num4;
						switch (num2)
						{
						case 0:
							return num3;
						case 1:
						{
							string text;
							if (dictionary.ContainsKey(text))
							{
								num2 = 15;
								continue;
							}
							goto IL_FE;
						}
						case 2:
						{
							int chartGroup;
							if (!dictionary2.ContainsKey(chartGroup))
							{
								num2 = 11;
								continue;
							}
							goto IL_1BB;
						}
						case 3:
							goto IL_1D0;
						case 4:
							goto IL_1D0;
						case 5:
						{
							string text2 = XlsChartFormat.ᜉ(type);
							num3 = 0;
							num4 = 0;
							int num5 = XlsChart.ᜯ.Length;
							goto IL_1AD;
						}
						case 6:
						{
							if (true)
							{
							}
							int num5;
							if (num4 >= num5)
							{
								num2 = 12;
								continue;
							}
							string text = XlsChart.ᜯ[num4];
							num2 = 7;
							continue;
						}
						case 7:
						{
							string text;
							string text2;
							if (text2 == text)
							{
								num2 = 0;
								continue;
							}
							num2 = 1;
							continue;
						}
						case 8:
							goto IL_1BB;
						case 9:
							goto IL_169;
						case 10:
							goto IL_FE;
						case 11:
						{
							int chartGroup;
							dictionary2.Add(chartGroup, null);
							XlsChartSerie xlsChartSerie;
							string text2 = XlsChartFormat.ᜉ(xlsChartSerie.SerieType);
							dictionary[text2] = null;
							num2 = 8;
							continue;
						}
						case 12:
							goto IL_192;
						case 13:
							goto IL_169;
						case 14:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1AD;
							default:
							{
								if (false)
								{
								}
								if (num >= base.Count)
								{
									num2 = 5;
									continue;
								}
								XlsChartSerie xlsChartSerie = (XlsChartSerie)base.List[num];
								int chartGroup = xlsChartSerie.ChartGroup;
								num2 = 2;
								continue;
							}
							}
							break;
						case 15:
							num3++;
							num2 = 10;
							continue;
						}
						break;
						IL_FE:
						num4++;
						num2 = 13;
						continue;
						IL_169:
						num2 = 6;
						continue;
						IL_1AD:
						num2 = 9;
						continue;
						IL_1BB:
						num++;
						num2 = 4;
						continue;
						IL_1D0:
						num2 = 14;
					}
				}
				return num3;
				IL_192:
				throw new ApplicationException(RecordTableEnumerator.b("猻䰽␿❁㙃晅⭇⭉≋⁍㽏♑瑓㑕㵗穙㩛ㅝᕟౡc䡥", a_));
			}
			}
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x000B2DE0 File Offset: 0x000B1DE0
		internal new void ᜀ(BaseFormatType A_0, TopFormatType A_1)
		{
			for (;;)
			{
				int num = 0;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_24;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
						{
							if (false)
							{
							}
							if (num >= base.Count)
							{
								num2 = 3;
								continue;
							}
							IChartSerieDataFormat dataFormat = ((XlsChartSerie)base.List[num]).DataPoints.DefaultDataPoint.DataFormat;
							dataFormat.BarType = A_0;
							dataFormat.BarTopType = A_1;
							num++;
							if (true)
							{
							}
							num2 = 2;
							continue;
						}
						}
						break;
					case 2:
						goto IL_24;
					case 3:
						return;
					}
					break;
					IL_24:
					num2 = 1;
				}
			}
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x000B2EA0 File Offset: 0x000B1EA0
		private new void ᜀ(int A_0, spr\u23A5 A_1)
		{
			int a_ = 4;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 5;
					continue;
				case 1:
					goto IL_76;
				case 2:
					if (A_1 != null)
					{
						goto IL_C6;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A5;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 3:
					if (true)
					{
					}
					break;
				case 4:
					goto IL_B0;
				case 5:
					if (A_0 < 1)
					{
						goto IL_A5;
					}
					num = 2;
					continue;
				}
				if (A_0 <= 3)
				{
					num = 0;
					continue;
				}
				goto IL_78;
				IL_A5:
				num = 4;
			}
			IL_76:
			throw new ArgumentNullException(RecordTableEnumerator.b("䠹夻崽⼿ぁ⁃", a_));
			IL_78:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䤹唻眽⸿♁⅃㹅", a_));
			IL_B0:
			goto IL_78;
			IL_C6:
			XlsChartSerie xlsChartSerie = (XlsChartSerie)base.List[A_1.ᜅ()];
			xlsChartSerie.ᜀ(A_0, A_1);
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x000B2F94 File Offset: 0x000B1F94
		internal List<BiffRecordRaw> ᜄ(int A_0)
		{
			int a_ = 18;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_AA:
				num = 17;
				break;
			default:
				if (false)
				{
				}
				switch (0)
				{
				default:
					num = 8;
					break;
				}
				break;
			}
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
				{
					List<BiffRecordRaw> list;
					return list;
				}
				case 1:
					goto IL_1EB;
				case 2:
					goto IL_1B0;
				case 3:
				{
					int count;
					if (num2 >= count)
					{
						num = 16;
						continue;
					}
					List<List<BiffRecordRaw>> list3;
					List<BiffRecordRaw> list2 = list3[num2];
					num = 18;
					continue;
				}
				case 4:
				{
					int num3 = 0;
					num = 6;
					continue;
				}
				case 5:
				{
					List<List<BiffRecordRaw>> list3;
					if (list3 == null)
					{
						num = 13;
						continue;
					}
					if (true)
					{
					}
					int num4 = list3[0].Count;
					int num5 = 1;
					int count2 = list3.Count;
					num = 2;
					continue;
				}
				case 6:
					goto IL_18B;
				case 7:
				{
					List<BiffRecordRaw> list;
					List<BiffRecordRaw> list2;
					int num3;
					list.Add(list2[num3]);
					num = 1;
					continue;
				}
				case 9:
				{
					int num5;
					int count2;
					if (num5 >= count2)
					{
						num = 4;
						continue;
					}
					List<List<BiffRecordRaw>> list3;
					int num4 = Math.Max(num4, list3[num5].Count);
					num5++;
					num = 20;
					continue;
				}
				case 10:
					goto IL_122;
				case 11:
				{
					int num3;
					int num4;
					if (num3 >= num4)
					{
						num = 0;
						continue;
					}
					num2 = 0;
					List<List<BiffRecordRaw>> list3;
					int count = list3.Count;
					num = 10;
					continue;
				}
				case 12:
					goto IL_122;
				case 13:
					goto IL_11D;
				case 14:
					goto IL_18B;
				case 15:
					goto IL_24E;
				case 16:
				{
					int num3;
					num3++;
					num = 14;
					continue;
				}
				case 17:
					num = 19;
					continue;
				case 18:
				{
					List<BiffRecordRaw> list2;
					int num3;
					if (list2.Count > num3)
					{
						num = 7;
						continue;
					}
					goto IL_1EB;
				}
				case 19:
				{
					if (A_0 < 1)
					{
						num = 15;
						continue;
					}
					List<BiffRecordRaw> list = new List<BiffRecordRaw>();
					List<List<BiffRecordRaw>> list3 = this.ᜀ(A_0);
					num = 5;
					continue;
				}
				case 20:
					goto IL_1B0;
				}
				break;
				IL_122:
				num = 3;
				continue;
				IL_18B:
				num = 11;
				continue;
				IL_1B0:
				num = 9;
				continue;
				IL_1EB:
				num2++;
				num = 12;
			}
			if (A_0 <= 3)
			{
				goto IL_AA;
			}
			goto IL_145;
			IL_11D:
			return null;
			IL_145:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("Ň⑉⡋⭍⡏", a_));
			IL_24E:
			goto IL_145;
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x000B3234 File Offset: 0x000B2234
		private new List<List<BiffRecordRaw>> ᜀ(int A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					List<List<BiffRecordRaw>> list = new List<List<BiffRecordRaw>>();
					int num = 0;
					int num2 = 6;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							if (num >= base.Count)
							{
								num2 = 2;
								continue;
							}
							XlsChartSerie xlsChartSerie = (XlsChartSerie)base.List[num];
							List<BiffRecordRaw> list2 = xlsChartSerie.ᜀ(A_0);
							num2 = 5;
							continue;
						}
						case 1:
							goto IL_F1;
						case 2:
							if (true)
							{
							}
							num2 = 8;
							continue;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_F1;
							default:
								goto IL_D6;
							}
							break;
						case 4:
							goto IL_F6;
						case 5:
						{
							List<BiffRecordRaw> list2;
							if (list2 != null)
							{
								num2 = 7;
								continue;
							}
							goto IL_70;
						}
						case 6:
							goto IL_F6;
						case 7:
						{
							List<BiffRecordRaw> list2;
							list.Add(list2);
							num2 = 1;
							continue;
						}
						case 8:
							if (list.Count == 0)
							{
								num2 = 3;
								continue;
							}
							return list;
						}
						break;
						IL_70:
						num++;
						num2 = 4;
						continue;
						IL_F1:
						goto IL_70;
						IL_F6:
						num2 = 0;
					}
				}
				IL_D6:
				if (false)
				{
				}
				return null;
			}
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x000B336C File Offset: 0x000B236C
		protected internal void UpdateSerieIndexAfterRemove(int index)
		{
			int a_ = 9;
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					if (index > base.List.Count)
					{
						num = 4;
						continue;
					}
					int num2 = index;
					int count = base.List.Count;
					num = 3;
					continue;
				}
				case 1:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						if (true)
						{
						}
						num = 2;
						continue;
					}
					XlsChartSerie xlsChartSerie = (XlsChartSerie)base.List[num2];
					xlsChartSerie.Index--;
					num2++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					break;
				}
				case 2:
					goto IL_104;
				case 3:
					goto IL_DF;
				case 4:
					goto IL_DD;
				case 5:
					num = 0;
					continue;
				case 6:
					goto IL_DF;
				}
				if (index >= 0)
				{
					num = 5;
					continue;
				}
				break;
				IL_DF:
				num = 1;
			}
			IL_DD:
			goto IL_106;
			IL_104:
			return;
			IL_106:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("嘾⽀❂⁄㽆", a_));
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x000B3494 File Offset: 0x000B2494
		public ExcelChartType GetTypeByOrder(int order)
		{
			int a_ = 18;
			XlsChartSerie xlsChartSerie;
			for (;;)
			{
				int num = 0;
				int count = base.Count;
				int num2 = 5;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_6C;
					case 1:
						goto IL_A6;
					case 2:
						if (num >= count)
						{
							num2 = 3;
							continue;
						}
						xlsChartSerie = (XlsChartSerie)base.List[num];
						num2 = 4;
						continue;
					case 3:
						goto IL_CC;
					case 4:
						if (xlsChartSerie.ChartGroup == order)
						{
							num2 = 1;
							continue;
						}
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
							num++;
							num2 = 0;
							continue;
						}
						break;
					case 5:
						goto IL_A8;
					}
					break;
					IL_A8:
					num2 = 2;
					continue;
					IL_6C:
					goto IL_A8;
				}
			}
			IL_A6:
			return xlsChartSerie.SerieType;
			IL_CC:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("݇㡉⡋⭍≏", a_));
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x000B3584 File Offset: 0x000B2584
		internal new void ᜀ(XlsChartSerieDataFormat A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					}
					if (false)
					{
					}
					int num = 0;
					int count = base.List.Count;
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							if (num >= count)
							{
								num2 = 1;
								continue;
							}
							XlsChartSerie xlsChartSerie = (XlsChartSerie)base.List[num];
							XlsChartDataPointsCollection xlsChartDataPointsCollection = (XlsChartDataPointsCollection)xlsChartSerie.DataPoints;
							xlsChartDataPointsCollection.ClearDataFormats(A_0);
							num++;
							num2 = 3;
							continue;
						}
						case 1:
							goto IL_7C;
						case 2:
							goto IL_66;
						case 3:
							goto IL_66;
						}
						break;
						IL_66:
						num2 = 0;
					}
				}
				IL_7C:
				if (true)
				{
				}
				return;
			}
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x000B3650 File Offset: 0x000B2650
		internal new string ᜀ()
		{
			int a_ = 0;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return CollectionExtended<IChartSerie>.GenerateDefaultName(base.List, RecordTableEnumerator.b("攵崷䠹唻嬽", a_));
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x000B36B0 File Offset: 0x000B26B0
		internal string ᜆ(int A_0)
		{
			int a_ = 13;
			IList<IChartSerie> list;
			for (;;)
			{
				int count = base.List.Count;
				int num = 10;
				for (;;)
				{
					switch (num)
					{
					case 0:
						list = base.List;
						num = 2;
						continue;
					case 1:
					{
						if (A_0 == base.List.Count)
						{
							num = 0;
							continue;
						}
						list = new List<IChartSerie>(A_0);
						int num2 = 0;
						num = 4;
						continue;
					}
					case 2:
						goto IL_89;
					case 3:
						goto IL_D8;
					case 4:
						goto IL_A2;
					case 5:
						goto IL_A2;
					case 6:
						num = 7;
						continue;
					case 7:
						if (A_0 < 0)
						{
							num = 8;
							continue;
						}
						num = 1;
						continue;
					case 8:
						goto IL_F4;
					case 9:
					{
						int num2;
						if (num2 < A_0)
						{
							list.Add(base.List[num2]);
							num2++;
							num = 5;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D8;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					}
					case 10:
						if (true)
						{
						}
						if (A_0 <= count)
						{
							num = 6;
							continue;
						}
						goto IL_8E;
					}
					break;
					IL_A2:
					num = 9;
				}
			}
			IL_89:
			goto IL_152;
			IL_8E:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⩂ᙄ≆㭈≊⡌َ㽐㝒ご⽖", a_));
			IL_D8:
			goto IL_152;
			IL_F4:
			goto IL_8E;
			IL_152:
			return CollectionExtended<IChartSerie>.GenerateDefaultName(list, RecordTableEnumerator.b("၂⁄㕆⁈⹊", a_));
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x000B3824 File Offset: 0x000B2824
		public void UpdateFormula(int currentIndex, int srcIndex, Rectangle srcRect, int destIndex, Rectangle destRect)
		{
			if (true)
			{
			}
			switch (0)
			{
			default:
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
					{
						if (false)
						{
						}
						List<IChartSerie> innerList = base.InnerList;
						int num = 0;
						int count = innerList.Count;
						int num2 = 2;
						for (;;)
						{
							switch (num2)
							{
							case 0:
							{
								if (num >= count)
								{
									num2 = 1;
									continue;
								}
								XlsChartSerie xlsChartSerie = (XlsChartSerie)innerList[num];
								xlsChartSerie.UpdateFormula(currentIndex, srcIndex, srcRect, destIndex, destRect);
								num++;
								num2 = 3;
								continue;
							}
							case 1:
								return;
							case 2:
								goto IL_70;
							case 3:
								goto IL_70;
							}
							break;
							IL_70:
							num2 = 0;
						}
						break;
					}
					}
				}
				return;
			}
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x000B38E8 File Offset: 0x000B28E8
		public int GetLegendEntryOffset(int iSerIndex)
		{
			int a_ = 2;
			int num = 1;
			int num3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_E2;
				case 2:
					goto IL_68;
				case 3:
				{
					int num2;
					if (num2 >= iSerIndex)
					{
						num = 0;
						continue;
					}
					XlsChartSerie xlsChartSerie = (XlsChartSerie)base.List[num2];
					num3 += xlsChartSerie.TrendLines.Count;
					num2++;
					num = 5;
					continue;
				}
				case 4:
					goto IL_C8;
				case 5:
					goto IL_C8;
				}
				if (iSerIndex < base.Count)
				{
					num3 = 0;
					int num2 = 0;
					if (true)
					{
					}
					num = 4;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					num = 2;
					continue;
				}
				IL_C8:
				num = 3;
			}
			IL_68:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("儷椹夻䰽िⱁ⁃⍅ぇ", a_));
			IL_E2:
			return num3 + base.Count;
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x000B39E4 File Offset: 0x000B29E4
		public void AssignTrendDataLabel(XlsChartTextArea area)
		{
			int a_ = 16;
			switch (0)
			{
			default:
			{
				int num = 4;
				sprᴌ sprᴌ;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_16B;
					case 1:
					{
						int num2;
						int count;
						if (num2 >= count)
						{
							num = 10;
							continue;
						}
						IChartSerie chartSerie = base.List[num2];
						IChartTrendLines trendLines = chartSerie.TrendLines;
						int num3 = 0;
						int count2 = trendLines.Count;
						num = 3;
						continue;
					}
					case 2:
					{
						if (sprᴌ.ᜊ() == (int)area.ObjectLink.ᜁ())
						{
							num = 0;
							continue;
						}
						int num3;
						num3++;
						num = 7;
						continue;
					}
					case 3:
						goto IL_8B;
					case 5:
						goto IL_81;
					case 6:
					{
						int num2;
						num2++;
						num = 8;
						continue;
					}
					case 7:
						goto IL_8B;
					case 8:
						goto IL_112;
					case 9:
						goto IL_112;
					case 10:
						return;
					case 11:
					{
						int num3;
						int count2;
						if (num3 >= count2)
						{
							num = 6;
							continue;
						}
						IChartTrendLines trendLines;
						sprᴌ = (sprᴌ)trendLines[num3];
						num = 2;
						continue;
					}
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_81;
					default:
					{
						if (false)
						{
						}
						if (area == null)
						{
							num = 5;
							continue;
						}
						int num2 = 0;
						int count = base.Count;
						num = 9;
						continue;
					}
					}
					IL_8B:
					num = 11;
					continue;
					IL_112:
					num = 1;
				}
				IL_81:
				if (true)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("❅㩇⽉ⵋ", a_));
				IL_16B:
				sprᴌ.ᜀ(area);
				return;
			}
			}
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x000B3B94 File Offset: 0x000B2B94
		internal void ᜄ()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					}
					if (false)
					{
					}
					int num = 0;
					int count = base.Count;
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							return;
						case 1:
							goto IL_69;
						case 2:
						{
							if (num >= count)
							{
								num2 = 0;
								continue;
							}
							IChartSerie chartSerie = base.List[num];
							IChartTrendLines trendLines = chartSerie.TrendLines;
							chartSerie.HasErrorBarsX = false;
							chartSerie.HasErrorBarsY = false;
							trendLines.Clear();
							num++;
							num2 = 1;
							continue;
						}
						case 3:
							if (true)
							{
							}
							goto IL_69;
						}
						break;
						IL_69:
						num2 = 2;
					}
				}
				return;
			}
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x000B3C5C File Offset: 0x000B2C5C
		internal new void ᜀ(Dictionary<int, int> A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					int count = base.Count;
					int num = 10;
					for (;;)
					{
						List<IChartSerie> innerList;
						int num5;
						SortedList<int, XlsChartSerie> sortedList;
						int num6;
						switch (num)
						{
						case 0:
							goto IL_1B4;
						case 1:
						{
							int num2;
							int num3;
							if (A_0.TryGetValue(num2, out num3))
							{
								num = 11;
								continue;
							}
							goto IL_113;
						}
						case 2:
						{
							int num4;
							if (num4 >= count)
							{
								num = 6;
								continue;
							}
							IList<XlsChartSerie> values;
							XlsChartSerie xlsChartSerie = values[num4];
							innerList[num4] = xlsChartSerie;
							int num2 = xlsChartSerie.Index;
							Dictionary<int, int> dictionary;
							dictionary[num4] = num2;
							xlsChartSerie.Index = num4;
							num4++;
							num = 7;
							continue;
						}
						case 3:
							goto IL_113;
						case 4:
						{
							if (num5 >= count)
							{
								num = 9;
								continue;
							}
							Dictionary<int, int> dictionary;
							int num2 = dictionary[num5];
							num = 1;
							continue;
						}
						case 5:
							goto IL_F2;
						case 6:
							num5 = 0;
							num = 0;
							continue;
						case 7:
							goto IL_1D9;
						case 8:
							if (true)
							{
							}
							num = 18;
							continue;
						case 9:
							return;
						case 10:
							if (count > 1)
							{
								num = 20;
								continue;
							}
							return;
						case 11:
						{
							XlsChartAxis xlsChartAxis = this.m_chart.PrimaryCategoryAxis as XlsChartAxis;
							XlsChartAxis xlsChartAxis2 = this.m_chart.PrimaryValueAxis as XlsChartAxis;
							num = 12;
							continue;
						}
						case 12:
						{
							int num3;
							XlsChartAxis xlsChartAxis;
							if (num3 != xlsChartAxis.AxisId)
							{
								num = 8;
								continue;
							}
							goto IL_113;
						}
						case 13:
						{
							XlsChartSerie xlsChartSerie2 = (XlsChartSerie)innerList[num5];
							xlsChartSerie2.UsePrimaryAxis = false;
							num = 3;
							continue;
						}
						case 14:
						{
							IList<XlsChartSerie> values = sortedList.Values;
							Dictionary<int, int> dictionary = new Dictionary<int, int>();
							int num4 = 0;
							num = 19;
							continue;
						}
						case 15:
							goto IL_FE;
						case 16:
							goto IL_F2;
						case 17:
							goto IL_1B4;
						case 18:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_FE;
							default:
							{
								if (false)
								{
								}
								int num3;
								XlsChartAxis xlsChartAxis2;
								if (num3 != xlsChartAxis2.AxisId)
								{
									num = 13;
									continue;
								}
								goto IL_113;
							}
							}
							break;
						case 19:
							goto IL_1D9;
						case 20:
							innerList = base.InnerList;
							sortedList = new SortedList<int, XlsChartSerie>();
							num6 = 0;
							num = 16;
							continue;
						}
						break;
						IL_F2:
						num = 15;
						continue;
						IL_FE:
						if (num6 >= count)
						{
							num = 14;
							continue;
						}
						XlsChartSerie xlsChartSerie3 = (XlsChartSerie)innerList[num6];
						int index = xlsChartSerie3.Index;
						sortedList.Add(index, xlsChartSerie3);
						num6++;
						num = 5;
						continue;
						IL_113:
						num5++;
						num = 17;
						continue;
						IL_1B4:
						num = 4;
						continue;
						IL_1D9:
						num = 2;
					}
				}
				return;
			}
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x000B3F50 File Offset: 0x000B2F50
		internal new void ᜀ(bool[] A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
					{
						if (false)
						{
						}
						List<IChartSerie> innerList = base.InnerList;
						int num = 0;
						int count = base.Count;
						int num2 = 0;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_70;
							case 1:
								goto IL_70;
							case 2:
								return;
							case 3:
							{
								if (num >= count)
								{
									num2 = 2;
									continue;
								}
								XlsChartSerie xlsChartSerie = (XlsChartSerie)innerList[num];
								xlsChartSerie.ᜀ(A_0);
								num++;
								num2 = 1;
								continue;
							}
							}
							break;
							IL_70:
							num2 = 3;
						}
						break;
					}
					}
				}
				return;
			}
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x000B400C File Offset: 0x000B300C
		internal new void ᜀ(int[] A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					}
					if (false)
					{
					}
					List<IChartSerie> innerList = base.InnerList;
					int num = 0;
					int count = base.Count;
					if (true)
					{
					}
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							return;
						case 1:
						{
							if (num >= count)
							{
								num2 = 0;
								continue;
							}
							XlsChartSerie xlsChartSerie = (XlsChartSerie)innerList[num];
							xlsChartSerie.ᜀ(A_0);
							num++;
							num2 = 3;
							continue;
						}
						case 2:
							goto IL_70;
						case 3:
							goto IL_70;
						}
						break;
						IL_70:
						num2 = 1;
					}
				}
				return;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001268 RID: 4712 RVA: 0x000B40C8 File Offset: 0x000B30C8
		internal List<IChartSerie> AdditionOrder
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
				return this.ᜅ;
			}
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x000B410C File Offset: 0x000B310C
		internal bool ᜈ()
		{
			for (;;)
			{
				int num = 0;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return false;
					case 1:
						if (!this[num].UsePrimaryAxis)
						{
							num2 = 2;
							continue;
						}
						num++;
						num2 = 4;
						continue;
					case 2:
						return true;
					case 3:
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
							goto IL_8D;
						}
						break;
					case 4:
						goto IL_8D;
					case 5:
						if (num >= base.Count)
						{
							num2 = 0;
							continue;
						}
						num2 = 1;
						continue;
					}
					break;
					IL_8D:
					num2 = 5;
				}
			}
			return true;
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x0600126A RID: 4714 RVA: 0x000B41C8 File Offset: 0x000B31C8
		internal IList<IRecordStorage> TrendErrorList
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
				return this.ᜁ;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x0600126B RID: 4715 RVA: 0x000B420C File Offset: 0x000B320C
		// (set) Token: 0x0600126C RID: 4716 RVA: 0x000B4250 File Offset: 0x000B3250
		internal int TrendErrorBarIndex
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
				return this.ᜃ;
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
				this.ᜃ = value;
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x0600126D RID: 4717 RVA: 0x000B4294 File Offset: 0x000B3294
		internal IList<IRecordStorage> TrendLabels
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

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x0600126E RID: 4718 RVA: 0x000B42D8 File Offset: 0x000B32D8
		// (set) Token: 0x0600126F RID: 4719 RVA: 0x000B431C File Offset: 0x000B331C
		internal int TrendIndex
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
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜄ = value;
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001270 RID: 4720 RVA: 0x000B4360 File Offset: 0x000B3360
		// (set) Token: 0x06001271 RID: 4721 RVA: 0x000B43A4 File Offset: 0x000B33A4
		internal bool IsSerieCreating
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

		// Token: 0x04000E53 RID: 3667
		private string \u2593\u0088\u0098\u00AE;

		// Token: 0x04000E54 RID: 3668
		internal new const string ᜀ = "Serie";

		// Token: 0x04000E55 RID: 3669
		protected internal XlsChart m_chart;

		// Token: 0x04000E56 RID: 3670
		private new IList<IRecordStorage> ᜁ;

		// Token: 0x04000E57 RID: 3671
		private new IList<IRecordStorage> ᜂ;

		// Token: 0x04000E58 RID: 3672
		private float[] \u25D8\u007F\u008B\u0090;

		// Token: 0x04000E59 RID: 3673
		private int ᜃ;

		// Token: 0x04000E5A RID: 3674
		private int ᜄ;

		// Token: 0x04000E5B RID: 3675
		private List<IChartSerie> ᜅ;

		// Token: 0x04000E5C RID: 3676
		private bool[] \u2609\u0088\u0084\u0097;

		// Token: 0x04000E5D RID: 3677
		private long[] \u2593\u009B\u00A3\u00AE;

		// Token: 0x04000E5E RID: 3678
		private byte[] \u25D9ª\u009B\u008A;

		// Token: 0x04000E5F RID: 3679
		private bool[] \u2460\u00A2\u00A2\u008D;

		// Token: 0x04000E60 RID: 3680
		private bool ᜆ;
	}
}
