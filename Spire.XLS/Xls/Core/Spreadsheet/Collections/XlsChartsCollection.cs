using System;
using System.Collections.Generic;
using Spire.Xls.Core.Spreadsheet.Charts;
using Spire.Xls.Core.Spreadsheet.Security;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x0200001D RID: 29
	public class XlsChartsCollection : CollectionExtended<IChart>, ICharts
	{
		// Token: 0x06000238 RID: 568 RVA: 0x00013F24 File Offset: 0x00012F24
		internal XlsChartsCollection(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜀ();
			this.ᜂ.Objects.TabSheetMoved += this.ᜀ;
		}

		// Token: 0x170000E9 RID: 233
		public IChart this[string name]
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
				return this.ᜁ[name];
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00013FB4 File Offset: 0x00012FB4
		public IChart Add()
		{
			int a_ = 0;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return this.Add(new ChartSheet((spr\u2158)base.ReservedHandle, this)
			{
				Name = CollectionExtended<IChart>.GenerateDefaultName(base.List, RecordTableEnumerator.b("电倷嬹主䨽", a_))
			});
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00014030 File Offset: 0x00013030
		public IChart Add(string name)
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
			return this.Add(new ChartSheet((spr\u2158)base.ReservedHandle, this)
			{
				Name = name
			});
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0001408C File Offset: 0x0001308C
		public IChart Remove(string name)
		{
			int a_ = 12;
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
						goto IL_A6;
					default:
						goto IL_8C;
					}
					break;
				case 2:
					goto IL_A6;
				case 3:
					if (this.ᜂ.ObjectCount == 1)
					{
						if (true)
						{
						}
						num = 0;
						continue;
					}
					goto IL_44;
				}
				if (this.ᜁ.ContainsKey(name))
				{
					num = 2;
					continue;
				}
				goto IL_D4;
				IL_A6:
				num = 3;
			}
			IL_44:
			IChart chart = this.ᜁ[name];
			base.Remove(chart);
			this.ᜂ.Objects.RemoveAt(((spr\u252A)chart).get_RealIndex());
			return chart;
			IL_8C:
			if (false)
			{
			}
			throw new ArgumentException(RecordTableEnumerator.b("แ╃㕅㱇橉㭋⅍≏㥑❓㹕㵗㽙⡛繝͟͡੣ࡥݧṩ䱫౭ᕯ剱ٳ፵ᕷᕹ੻᭽ꊁ겋揄뒓ﲝ쾟춡쾣袥", a_));
			IL_D4:
			return null;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00014178 File Offset: 0x00013178
		public new IChart Add(IChart chartToAdd)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.AddInternal(chartToAdd);
			this.ᜂ.Objects.ᜀ((spr\u252A)chartToAdd);
			return chartToAdd;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000141D4 File Offset: 0x000131D4
		internal new IChart ᜀ(sprἛ A_0)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return this.ᜀ(A_0, ExcelParseOptions.Default, false, null, null);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0001421C File Offset: 0x0001321C
		internal new IChart ᜀ(sprἛ A_0, ExcelParseOptions A_1, bool A_2, Dictionary<int, int> A_3, IDecryptor A_4)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			XlsChart chartToAdd = new XlsChart((spr\u2158)base.ReservedHandle, this, A_0, A_1, A_2, A_3, A_4);
			return this.Add(chartToAdd);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00014278 File Offset: 0x00013278
		private new void ᜀ()
		{
			int a_ = 9;
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
				object obj = base.FindParent(typeof(XlsWorkbook));
				if (obj != null)
				{
					this.ᜂ = (XlsWorkbook)obj;
					return;
				}
				break;
			}
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("漾⁀ㅂ⁄⥆㵈歊≌ⵎ㭐㙒㙔⍖祘㡚㱜ㅞའౢᅤ䝦୨๪䵬८Ṱٲ᭴፶坸", a_));
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000142F4 File Offset: 0x000132F4
		public void Move(int oldIndex, int newIndex)
		{
			int a_ = 4;
			int num = 9;
			for (;;)
			{
				int count;
				switch (num)
				{
				case 0:
					goto IL_BE;
				case 1:
					return;
				case 2:
					if (oldIndex >= count)
					{
						num = 3;
						continue;
					}
					num = 7;
					continue;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_BE;
					default:
						goto IL_10D;
					}
					break;
				case 4:
					if (newIndex >= count)
					{
						num = 6;
						continue;
					}
					goto IL_122;
				case 5:
					num = 2;
					continue;
				case 6:
					goto IL_D8;
				case 7:
					if (true)
					{
					}
					if (newIndex >= 0)
					{
						num = 0;
						continue;
					}
					goto IL_A9;
				case 8:
					if (oldIndex >= 0)
					{
						num = 5;
						continue;
					}
					goto IL_6D;
				}
				if (oldIndex == newIndex)
				{
					num = 1;
					continue;
				}
				count = base.InnerList.Count;
				num = 8;
				continue;
				IL_BE:
				num = 4;
			}
			return;
			IL_6D:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("唹倻娽िⱁ⁃⍅ぇ", a_));
			IL_A9:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("吹夻䤽िⱁ⁃⍅ぇ", a_));
			IL_D8:
			goto IL_A9;
			IL_10D:
			if (false)
			{
			}
			goto IL_6D;
			IL_122:
			XlsChart item = base[oldIndex] as XlsChart;
			base.InnerList.RemoveAt(oldIndex);
			base.InnerList.Insert(newIndex, item);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0001444C File Offset: 0x0001344C
		private new void ᜀ(object A_0, XlsEventArgs A_1)
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
			CollectionExtended<IChart>.ChangeName(this.ᜁ, A_1);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00014494 File Offset: 0x00013494
		protected override void OnClear()
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
			base.OnClear();
			this.ᜁ.Clear();
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000144E0 File Offset: 0x000134E0
		private new void ᜀ(XlsChart A_0, int A_1)
		{
			int a_ = 6;
			switch (0)
			{
			default:
			{
				int num = 0;
				for (;;)
				{
					int realIndex;
					int num2;
					int num3;
					ITabSheet tabSheet;
					int num4;
					ITabSheets tabSheets;
					switch (num)
					{
					case 1:
						num2 = realIndex - 1;
						num3 = -1;
						num = 9;
						continue;
					case 2:
						if (A_1 < realIndex)
						{
							goto IL_1B9;
						}
						goto IL_195;
					case 3:
						goto IL_1C7;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1B9;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							goto IL_A3;
						}
						break;
					case 5:
					{
						ITabSheet tabSheet2;
						tabSheet = tabSheet2;
						num = 14;
						continue;
					}
					case 6:
						goto IL_F2;
					case 7:
						goto IL_190;
					case 8:
						num2 = realIndex + 1;
						num3 = 1;
						num = 4;
						continue;
					case 9:
						goto IL_A3;
					case 10:
						goto IL_8A;
					case 11:
					{
						XlsChart xlsChart = (XlsChart)tabSheet;
						int index = A_0.Index;
						int index2 = xlsChart.Index;
						this.ᜀ(index, index2);
						num = 7;
						continue;
					}
					case 12:
					{
						if (num4 > A_1)
						{
							num = 6;
							continue;
						}
						ITabSheet tabSheet2 = tabSheets[num4];
						num = 17;
						continue;
					}
					case 13:
						if (tabSheet != null)
						{
							num = 11;
							continue;
						}
						return;
					case 14:
						goto IL_F2;
					case 15:
						if (A_1 > realIndex)
						{
							num = 8;
							continue;
						}
						num = 2;
						continue;
					case 16:
						goto IL_1C7;
					case 17:
					{
						ITabSheet tabSheet2;
						if (tabSheet2 is XlsChart)
						{
							num = 5;
							continue;
						}
						num4 += num3;
						num = 3;
						continue;
					}
					}
					if (A_0 == null)
					{
						num = 10;
						continue;
					}
					realIndex = A_0.RealIndex;
					num3 = 0;
					num2 = -1;
					tabSheets = this.ᜂ.TabSheets;
					num = 15;
					continue;
					IL_A3:
					tabSheet = null;
					num4 = num2;
					num = 16;
					continue;
					IL_F2:
					num = 13;
					continue;
					IL_1B9:
					num = 1;
					continue;
					IL_1C7:
					num = 12;
				}
				IL_8A:
				throw new ArgumentNullException(RecordTableEnumerator.b("弻嘽ℿぁぃ", a_));
				IL_190:
				return;
				IL_195:
				throw new NotImplementedException(RecordTableEnumerator.b("缻嘽ℿぁぃ晅㽇⭉㽋⁍睏♑瑓㭕㝗ⱙ㥛㩝䁟͡ၣ䙥१٩k", a_));
			}
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00014738 File Offset: 0x00013738
		private new void ᜀ(int A_0, int A_1)
		{
			int a_ = 2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				switch (0)
				{
				default:
				{
					int num = 4;
					for (;;)
					{
						int count;
						switch (num)
						{
						case 0:
							num = 2;
							continue;
						case 1:
							goto IL_1C4;
						case 2:
							if (A_0 >= count)
							{
								num = 1;
								continue;
							}
							num = 13;
							continue;
						case 3:
							goto IL_10A;
						case 5:
							goto IL_175;
						case 6:
						{
							if (A_1 >= count)
							{
								num = 3;
								continue;
							}
							if (true)
							{
							}
							XlsChart xlsChart = base[A_0] as XlsChart;
							base.InnerList.RemoveAt(A_0);
							base.InnerList.Insert(A_1, xlsChart);
							int num2 = Math.Min(A_1, A_0);
							int num3 = Math.Max(A_1, A_0);
							int num4 = num2;
							num = 5;
							continue;
						}
						case 7:
							return;
						case 8:
							num = 6;
							continue;
						case 9:
						{
							int num3;
							int num4;
							if (num4 > num3)
							{
								num = 7;
								continue;
							}
							XlsChart xlsChart = base[num4] as XlsChart;
							xlsChart.Index = num4;
							num4++;
							num = 12;
							continue;
						}
						case 10:
							return;
						case 11:
							if (A_0 >= 0)
							{
								num = 0;
								continue;
							}
							goto IL_194;
						case 12:
							goto IL_175;
						case 13:
							if (A_1 >= 0)
							{
								num = 8;
								continue;
							}
							goto IL_161;
						}
						if (A_0 == A_1)
						{
							num = 10;
							continue;
						}
						count = base.InnerList.Count;
						num = 11;
						continue;
						IL_175:
						num = 9;
					}
					IL_10A:
					IL_161:
					throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("儷琹夻䤽िⱁ⁃⍅ぇ", a_));
					IL_194:
					throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("儷甹倻娽िⱁ⁃⍅ぇ", a_));
					IL_1C4:
					goto IL_194;
				}
				}
				break;
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0001492C File Offset: 0x0001392C
		public void AddInternal(IChart chartToAdd)
		{
			int a_ = 4;
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					num = 2;
					continue;
				case 1:
					if (chartToAdd.Name != null)
					{
						num = 0;
						continue;
					}
					goto IL_C3;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8F;
					default:
						if (false)
						{
						}
						if (chartToAdd.Name.Length == 0)
						{
							num = 6;
							continue;
						}
						goto IL_EF;
					}
					break;
				case 3:
					goto IL_40;
				case 4:
					goto IL_ED;
				case 6:
					goto IL_C3;
				}
				if (chartToAdd == null)
				{
					num = 3;
					continue;
				}
				IL_8F:
				num = 1;
				continue;
				IL_C3:
				chartToAdd.Name = CollectionExtended<IChart>.GenerateDefaultName(base.List, RecordTableEnumerator.b("礹吻弽㈿㙁", a_));
				num = 4;
			}
			IL_40:
			throw new ArgumentNullException(RecordTableEnumerator.b("夹吻弽㈿㙁၃⥅े⹉⡋", a_));
			IL_ED:
			IL_EF:
			this.ᜁ.Add(chartToAdd.Name, chartToAdd);
			XlsChart xlsChart = chartToAdd as XlsChart;
			xlsChart.Index = base.Count;
			xlsChart.NameChanged += this.ᜀ;
			base.Add(chartToAdd);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00014A68 File Offset: 0x00013A68
		private new void ᜀ(object A_0, TabSheetMovedEventArgs A_1)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					ITabSheets tabSheets = (ITabSheets)A_0;
					int newIndex = A_1.NewIndex;
					XlsChart xlsChart = tabSheets[newIndex] as XlsChart;
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
						int num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								int oldIndex = A_1.OldIndex;
								this.ᜀ(xlsChart, oldIndex);
								num = 1;
								continue;
							}
							case 1:
								return;
							case 2:
								if (xlsChart != null)
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
				return;
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00014B18 File Offset: 0x00013B18
		internal new void ᜀ(IChart A_0)
		{
			int a_ = 1;
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_3A;
			}
			if (false)
			{
			}
			if (A_0 != null)
			{
				XlsChart xlsChart = (A_0 as XlsChart).Clone(null, this, null);
				xlsChart.\u1739();
				xlsChart.Name = CollectionExtended<object>.GenerateDefaultName(this.ᜂ.Objects, A_0.Name);
				xlsChart.ᜠ = null;
				this.ᜂ.InnerCharts.Add(xlsChart);
				return;
			}
			IL_3A:
			throw new ArgumentNullException(RecordTableEnumerator.b("吶儸娺似䬾ᕀⱂل⡆㥈㉊", a_));
		}

		// Token: 0x04000063 RID: 99
		internal new const string ᜀ = "Chart";

		// Token: 0x04000064 RID: 100
		private string[] \u2460\u0092\u009E\u008B;

		// Token: 0x04000065 RID: 101
		private new Dictionary<string, IChart> ᜁ = new Dictionary<string, IChart>(StringComparer.CurrentCultureIgnoreCase);

		// Token: 0x04000066 RID: 102
		private long[] \u25D8\u0095\u0090\u00A6;

		// Token: 0x04000067 RID: 103
		private byte \u2460\u00A7\u008D\u009F;

		// Token: 0x04000068 RID: 104
		private long \u2460\u00A6\u008E\u00A5;

		// Token: 0x04000069 RID: 105
		private float \u2460\u009C\u00A4\u0082;

		// Token: 0x0400006A RID: 106
		private new XlsWorkbook ᜂ;
	}
}
