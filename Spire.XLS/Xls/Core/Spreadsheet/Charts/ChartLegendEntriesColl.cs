using System;
using System.Collections.Generic;
using Spire.Xls.Charts;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Charts
{
	// Token: 0x020001D3 RID: 467
	public class ChartLegendEntriesColl : XlsObject, IChartLegendEntries
	{
		// Token: 0x06001A16 RID: 6678 RVA: 0x000EC200 File Offset: 0x000EB200
		internal ChartLegendEntriesColl(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜀ();
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06001A17 RID: 6679 RVA: 0x000EC228 File Offset: 0x000EB228
		public int Count
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
						XlsChartSeries xlsChartSeries = this.ᜁ.Series;
						string value = XlsChartFormat.ᜉ(this.ᜁ.ChartType);
						bool flag = Array.IndexOf<string>(XlsChart.ᜥ, value) == -1;
						result = 0;
						int num = 4;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_C1;
							case 1:
							{
								int num2;
								result = num2;
								num = 5;
								continue;
							}
							case 2:
							{
								int num3;
								int count;
								if (num3 >= count)
								{
									num = 1;
									continue;
								}
								IChartSerie chartSerie = xlsChartSeries[num3];
								int num2;
								num2 += chartSerie.TrendLines.Count;
								num3++;
								num = 0;
								continue;
							}
							case 3:
								goto IL_C1;
							case 4:
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
									if (!flag)
									{
										XlsChartSerie xlsChartSerie = (XlsChartSerie)xlsChartSeries[0];
										result = xlsChartSerie.PointNumber;
										num = 6;
										continue;
									}
									break;
								}
								num = 7;
								continue;
							case 5:
								return result;
							case 6:
								return result;
							case 7:
							{
								int num2 = xlsChartSeries.Count;
								int num3 = 0;
								int count = xlsChartSeries.Count;
								num = 3;
								continue;
							}
							}
							break;
							IL_C1:
							num = 2;
						}
					}
					return result;
				}
				}
			}
		}

		// Token: 0x170009B7 RID: 2487
		public IChartLegendEntry this[int iIndex]
		{
			get
			{
				int a_ = 10;
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						num = 1;
						continue;
					case 1:
						if (iIndex >= this.Count)
						{
							num = 3;
							continue;
						}
						goto IL_50;
					case 2:
						if (this.ᜀ.ContainsKey(iIndex))
						{
							num = 4;
							continue;
						}
						goto IL_D8;
					case 3:
						goto IL_AD;
					case 4:
						goto IL_6E;
					}
					if (!this.ᜁ.Loading)
					{
						num = 0;
						continue;
					}
					IL_50:
					num = 2;
				}
				for (;;)
				{
					IL_6E:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_C5;
					}
				}
				IL_C5:
				if (false)
				{
				}
				return this.ᜀ[iIndex];
				IL_AD:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⤿ⱁ⁃⍅ぇ", a_));
				IL_D8:
				return this.Add(iIndex);
			}
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x000EC47C File Offset: 0x000EB47C
		private void ᜀ()
		{
			int a_ = 4;
			this.ᜁ = (XlsChart)base.FindParent(typeof(XlsChart));
			if (this.ᜁ != null)
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
					return;
				}
			}
			throw new ApplicationException(RecordTableEnumerator.b("樹崻䰽┿ⱁぃ晅❇⡉♋⭍㍏♑瑓㕕㥗㑙㉛ㅝᑟ䉡٣ͥ䡧౩ͫ᭭ṯᙱ婳", a_));
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x000EC4FC File Offset: 0x000EB4FC
		public XlsChartLegendEntry Add(int iIndex)
		{
			if (!this.ᜀ.ContainsKey(iIndex))
			{
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
					ChartLegendEntry entry = new ChartLegendEntry((spr\u2158)base.ReservedHandle, this, iIndex);
					return this.Add(iIndex, entry);
				}
				}
			}
			return this.ᜀ[iIndex];
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x000EC570 File Offset: 0x000EB570
		public XlsChartLegendEntry Add(int iIndex, XlsChartLegendEntry entry)
		{
			int a_ = 12;
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_128;
				case 1:
					goto IL_193;
				case 2:
					goto IL_FA;
				case 3:
					goto IL_159;
				case 5:
					if (iIndex >= this.Count)
					{
						num = 0;
						continue;
					}
					goto IL_17A;
				case 6:
					this.ᜀ[iIndex] = entry;
					num = 2;
					continue;
				case 7:
					num = 5;
					continue;
				case 8:
					entry.LegendEntityIndex = iIndex;
					num = 12;
					continue;
				case 9:
				{
					ExcelChartType chartType = this.ᜁ.ChartType;
					string value = XlsChartFormat.ᜉ(chartType);
					num = 13;
					continue;
				}
				case 10:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_13C;
					default:
						if (false)
						{
						}
						if (!this.ᜁ.Loading)
						{
							num = 9;
							continue;
						}
						goto IL_72;
					}
					break;
				case 11:
					if (this.ᜀ.ContainsKey(iIndex))
					{
						num = 6;
						continue;
					}
					this.ᜀ.Add(iIndex, entry);
					num = 3;
					continue;
				case 12:
					goto IL_13C;
				case 13:
				{
					string value;
					if (Array.IndexOf<string>(XlsChart.ᜥ, value) != -1)
					{
						num = 8;
						continue;
					}
					goto IL_72;
				}
				case 14:
					if (entry == null)
					{
						num = 1;
						continue;
					}
					entry.Index = iIndex;
					num = 10;
					continue;
				}
				if (!this.ᜁ.Loading)
				{
					num = 7;
					continue;
				}
				goto IL_17A;
				IL_72:
				num = 11;
				continue;
				IL_13C:
				goto IL_72;
				IL_17A:
				num = 14;
			}
			IL_FA:
			return entry;
			IL_128:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⭁⩃≅ⵇ㉉", a_));
			IL_159:
			return entry;
			IL_193:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("❁⩃㉅㩇㍉", a_));
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x000EC764 File Offset: 0x000EB764
		public bool Contains(int iIndex)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return this.ᜀ.ContainsKey(iIndex);
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x000EC7AC File Offset: 0x000EB7AC
		public bool CanDelete(int iIndex)
		{
			int num = 9;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
				{
					int count;
					if (num2 >= count)
					{
						num = 3;
						continue;
					}
					num = 6;
					continue;
				}
				case 1:
				{
					XlsChartLegendEntry xlsChartLegendEntry;
					if (this.ᜀ.TryGetValue(num2, out xlsChartLegendEntry))
					{
						num = 8;
						continue;
					}
					goto IL_DC;
				}
				case 2:
					goto IL_BE;
				case 3:
					return false;
				case 4:
				{
					XlsChartLegendEntry xlsChartLegendEntry = null;
					num = 1;
					continue;
				}
				case 5:
					return true;
				case 6:
					if (num2 != iIndex)
					{
						num = 4;
						continue;
					}
					goto IL_DC;
				case 7:
				{
					XlsChartLegendEntry xlsChartLegendEntry;
					if (!xlsChartLegendEntry.IsDeleted)
					{
						num = 11;
						continue;
					}
					goto IL_DC;
				}
				case 8:
					goto IL_83;
				case 10:
					goto IL_BE;
				case 11:
					goto IL_A1;
				}
				if (this.ᜀ.Count == this.Count)
				{
					num2 = 0;
					int count = this.ᜀ.Count;
					num = 2;
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
					num = 5;
					continue;
				}
				IL_83:
				num = 7;
				continue;
				IL_BE:
				num = 0;
				continue;
				IL_DC:
				num2++;
				num = 10;
			}
			return true;
			IL_A1:
			if (true)
			{
			}
			return true;
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x000EC8FC File Offset: 0x000EB8FC
		public void Remove(int iIndex)
		{
			int a_ = 12;
			switch (0)
			{
			default:
				for (;;)
				{
					int count = this.Count;
					string value = XlsChartFormat.ᜉ(this.ᜁ.ChartType);
					int num = 0;
					for (;;)
					{
						int num2;
						switch (num)
						{
						case 0:
							if (Array.IndexOf<string>(XlsChart.ᜥ, value) != -1)
							{
								num = 6;
								continue;
							}
							num = 5;
							continue;
						case 1:
							goto IL_18B;
						case 2:
							if (true)
							{
							}
							goto IL_130;
						case 3:
							if (iIndex >= count)
							{
								num = 4;
								continue;
							}
							num = 7;
							continue;
						case 4:
							goto IL_1F3;
						case 5:
							if (iIndex >= 0)
							{
								num = 8;
								continue;
							}
							goto IL_207;
						case 6:
							return;
						case 7:
							if (this.ᜀ.ContainsKey(iIndex))
							{
								num = 14;
								continue;
							}
							goto IL_1F5;
						case 8:
							num = 3;
							continue;
						case 9:
							if (this.ᜀ.ContainsKey(num2))
							{
								num = 11;
								continue;
							}
							goto IL_130;
						case 10:
							return;
						case 11:
						{
							XlsChartLegendEntry xlsChartLegendEntry = this.ᜀ[num2];
							xlsChartLegendEntry.Index = num2 - 1;
							this.ᜀ.Add(num2 - 1, xlsChartLegendEntry);
							this.ᜀ.Remove(num2);
							num = 2;
							continue;
						}
						case 12:
							if (num2 >= count)
							{
								num = 10;
								continue;
							}
							num = 9;
							continue;
						case 13:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1F3;
							default:
								if (false)
								{
								}
								goto IL_1F5;
							}
							break;
						case 14:
							this.ᜀ.Remove(iIndex);
							num = 13;
							continue;
						case 15:
							goto IL_18B;
						}
						break;
						IL_130:
						num2++;
						num = 15;
						continue;
						IL_18B:
						num = 12;
						continue;
						IL_1F5:
						num2 = iIndex + 1;
						num = 1;
					}
				}
				return;
				IL_1F3:
				IL_207:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⭁⩃≅ⵇ㉉", a_));
			}
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x000ECB24 File Offset: 0x000EBB24
		public ChartLegendEntriesColl Clone(object parent, Dictionary<int, int> dicIndexes, Dictionary<string, string> dicNewSheetNames)
		{
			int a_ = 18;
			switch (0)
			{
			default:
			{
				int num = 3;
				ChartLegendEntriesColl chartLegendEntriesColl;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_17A;
					case 1:
						goto IL_195;
					case 2:
						goto IL_54;
					case 4:
					{
						int count;
						if (count == 0)
						{
							num = 0;
							continue;
						}
						Dictionary<int, XlsChartLegendEntry>.Enumerator enumerator = this.ᜀ.GetEnumerator();
						num = 1;
						continue;
					}
					}
					if (true)
					{
					}
					if (parent == null)
					{
						num = 2;
					}
					else
					{
						chartLegendEntriesColl = (ChartLegendEntriesColl)base.MemberwiseClone();
						chartLegendEntriesColl.SetParent(parent);
						chartLegendEntriesColl.ᜀ();
						int count = this.ᜀ.Count;
						chartLegendEntriesColl.ᜀ = new Dictionary<int, XlsChartLegendEntry>(count);
						num = 4;
					}
				}
				IL_54:
				throw new ArgumentNullException(RecordTableEnumerator.b("㡇⭉㹋⭍㹏♑", a_));
				IL_17A:
				return chartLegendEntriesColl;
				IL_195:
				try
				{
					num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 3;
							continue;
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
								break;
							}
							break;
						case 3:
							goto IL_FB;
						case 4:
						{
							Dictionary<int, XlsChartLegendEntry>.Enumerator enumerator;
							if (!enumerator.MoveNext())
							{
								num = 0;
								continue;
							}
							KeyValuePair<int, XlsChartLegendEntry> keyValuePair = enumerator.Current;
							XlsChartLegendEntry xlsChartLegendEntry = keyValuePair.Value;
							xlsChartLegendEntry = xlsChartLegendEntry.Clone(chartLegendEntriesColl, dicIndexes, dicNewSheetNames);
							chartLegendEntriesColl.ᜀ.Add(keyValuePair.Key, xlsChartLegendEntry);
							num = 1;
							continue;
						}
						}
						IL_9D:
						num = 4;
						continue;
						goto IL_9D;
					}
					IL_FB:
					return chartLegendEntriesColl;
				}
				finally
				{
					Dictionary<int, XlsChartLegendEntry>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				return chartLegendEntriesColl;
			}
			}
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x000ECCDC File Offset: 0x000EBCDC
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
			this.ᜀ.Clear();
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x000ECD24 File Offset: 0x000EBD24
		public void UpdateEntries(int entryIndex, int value)
		{
			for (;;)
			{
				IL_2C:
				int count = this.Count;
				int num = this.Count - 1;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_F6:
					num2 = 2;
					break;
				case 1:
					goto IL_5C;
				default:
					goto IL_5C;
				}
				for (;;)
				{
					IL_02:
					if (true)
					{
					}
					switch (num2)
					{
					case 0:
					{
						XlsChartLegendEntry xlsChartLegendEntry = this.ᜀ[num];
						xlsChartLegendEntry.Index += value;
						this.ᜀ.Add(xlsChartLegendEntry.Index, xlsChartLegendEntry);
						this.ᜀ.Remove(num);
						num2 = 5;
						continue;
					}
					case 1:
						goto IL_E7;
					case 2:
						return;
					case 3:
						if (this.ᜀ.ContainsKey(num))
						{
							num2 = 0;
							continue;
						}
						goto IL_6C;
					case 4:
						if (num < entryIndex)
						{
							goto IL_F6;
						}
						num2 = 3;
						continue;
					case 5:
						goto IL_6C;
					case 6:
						goto IL_E7;
					}
					goto IL_2C;
					IL_6C:
					num--;
					num2 = 6;
					continue;
					IL_E7:
					num2 = 4;
				}
				IL_5C:
				if (false)
				{
				}
				num2 = 1;
				goto IL_02;
			}
		}

		// Token: 0x04000FFF RID: 4095
		private Dictionary<int, XlsChartLegendEntry> ᜀ = new Dictionary<int, XlsChartLegendEntry>();

		// Token: 0x04001000 RID: 4096
		private long[] \u25D9\u00A8\u009B\u00AC;

		// Token: 0x04001001 RID: 4097
		private long[] \u25D8\u009A\u008B\u009D;

		// Token: 0x04001002 RID: 4098
		private int[] \u25D8\u00AD\u009D\u008F;

		// Token: 0x04001003 RID: 4099
		private bool[] \u25D9\u0099\u009E\u0093;

		// Token: 0x04001004 RID: 4100
		private XlsChart ᜁ;
	}
}
