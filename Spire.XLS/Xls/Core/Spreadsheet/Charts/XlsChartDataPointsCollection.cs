using System;
using System.Collections;
using System.Collections.Generic;
using Spire.Xls.Charts;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Charts
{
	// Token: 0x02000197 RID: 407
	public class XlsChartDataPointsCollection : XlsObject, IChartDataPoints
	{
		// Token: 0x0600143A RID: 5178 RVA: 0x000C2668 File Offset: 0x000C1668
		internal XlsChartDataPointsCollection(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜀ = new ChartDataPoint((spr\u2158)base.ReservedHandle, this, 65535);
			this.ᜀ();
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x000C26AC File Offset: 0x000C16AC
		private void ᜀ()
		{
			int a_ = 18;
			this.ᜂ = (base.FindParent(typeof(XlsChartSerie)) as XlsChartSerie);
			if (this.ᜂ == null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("େ⭉≋楍⑏牑㉓㽕㙗㹙籛⹝şၡţࡥᱧ䩩Ὣ୭ɯ᭱ᅳյ噷", a_));
			}
		}

		// Token: 0x1700073F RID: 1855
		public IChartDataPoint this[int index]
		{
			get
			{
				int a_ = 2;
				switch (0)
				{
				default:
				{
					int num = 13;
					XlsChartDataPoint xlsChartDataPoint;
					for (;;)
					{
						XlsChartSerieDataFormat dataFormatOrNull;
						switch (num)
						{
						case 0:
							goto IL_166;
						case 1:
							if (this.ᜁ.ContainsKey(index))
							{
								num = 14;
								continue;
							}
							xlsChartDataPoint = new ChartDataPoint((spr\u2158)base.ReservedHandle, this, index);
							this.Add(xlsChartDataPoint);
							num = 7;
							continue;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1E7;
							default:
							{
								if (false)
								{
								}
								int pointNumber;
								if (index >= pointNumber)
								{
									num = 10;
									continue;
								}
								goto IL_AD;
							}
							}
							break;
						case 3:
							xlsChartDataPoint.CloneDataFormat(dataFormatOrNull);
							num = 0;
							continue;
						case 4:
						{
							int pointNumber = this.ᜂ.PointNumber;
							num = 2;
							continue;
						}
						case 5:
							if (dataFormatOrNull.IsFormatted)
							{
								num = 3;
								continue;
							}
							return xlsChartDataPoint;
						case 6:
							if (!this.IsLoading)
							{
								num = 4;
								continue;
							}
							goto IL_AD;
						case 7:
							goto IL_1C8;
						case 8:
							goto IL_136;
						case 9:
							if (index == 65535)
							{
								num = 8;
								continue;
							}
							num = 6;
							continue;
						case 10:
							goto IL_1B2;
						case 11:
							goto IL_7D;
						case 12:
							if (true)
							{
							}
							num = 5;
							continue;
						case 14:
							xlsChartDataPoint = this.ᜁ[index];
							num = 16;
							continue;
						case 15:
							goto IL_1E7;
						case 16:
							goto IL_1C8;
						}
						if (index < 0)
						{
							num = 11;
							continue;
						}
						num = 9;
						continue;
						IL_AD:
						num = 1;
						continue;
						IL_1C8:
						XlsChartDataPoint xlsChartDataPoint2 = (XlsChartDataPoint)this.DefaultDataPoint;
						dataFormatOrNull = xlsChartDataPoint2.DataFormatOrNull;
						num = 15;
						continue;
						IL_1E7:
						if (dataFormatOrNull == null)
						{
							return xlsChartDataPoint;
						}
						num = 12;
					}
					IL_7D:
					throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("儷吹堻嬽㠿", a_));
					IL_136:
					return this.DefaultDataPoint;
					IL_166:
					return xlsChartDataPoint;
					IL_1B2:
					throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("儷吹堻嬽㠿", a_));
				}
				}
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x0600143D RID: 5181 RVA: 0x000C2974 File Offset: 0x000C1974
		public IChartDataPoint DefaultDataPoint
		{
			get
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						if (!this.ᜂ.InnerXlsChart.TypeChanging)
						{
							num = 1;
							continue;
						}
						goto IL_C5;
					case 1:
					{
						XlsChartFormat commonSerieFormat = this.ᜂ.GetCommonSerieFormat();
						this.ᜀ.CloneDataFormat(commonSerieFormat.DataFormatOrNull);
						num = 3;
						continue;
					}
					case 2:
						goto IL_93;
					case 3:
						goto IL_91;
					}
					if (this.ᜂ.InnerXlsChart.Loading)
					{
						break;
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
					IL_93:
					num = 0;
				}
				IL_91:
				IL_C5:
				return this.ᜀ;
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x000C2A4C File Offset: 0x000C1A4C
		internal bool IsLoading
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
				return this.ᜂ.InnerWorkbook.Loading;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x0600143F RID: 5183 RVA: 0x000C2A98 File Offset: 0x000C1A98
		public XlsChartSerieDataFormat DefaultPointFormat
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
				return this.ᜀ.DataFormatOrNull;
			}
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x000C2AE0 File Offset: 0x000C1AE0
		internal void ᜁ(RecordArrayList A_0)
		{
			for (;;)
			{
				Dictionary<int, XlsChartDataPoint>.ValueCollection.Enumerator enumerator = this.ᜁ.Values.GetEnumerator();
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						try
						{
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_E3;
								case 1:
									if (enumerator.MoveNext())
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
											XlsChartDataPoint xlsChartDataPoint = enumerator.Current;
											xlsChartDataPoint.ᜀ(A_0);
											num = 4;
											continue;
										}
										}
									}
									num = 3;
									continue;
								case 3:
									num = 0;
									continue;
								}
								IL_C0:
								num = 1;
								continue;
								goto IL_C0;
							}
							IL_E3:
							goto IL_45;
						}
						finally
						{
							((IDisposable)enumerator).Dispose();
						}
						goto IL_F6;
						IL_45:
						num = 3;
						continue;
					case 1:
						goto IL_F6;
					case 2:
						return;
					case 3:
						if (this.ᜀ != null)
						{
							num = 1;
							continue;
						}
						return;
					}
					break;
					IL_F6:
					this.ᜀ.ᜀ(A_0);
					num = 2;
				}
			}
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x000C2C0C File Offset: 0x000C1C0C
		internal void ᜀ(RecordArrayList A_0)
		{
			for (;;)
			{
				Dictionary<int, XlsChartDataPoint>.ValueCollection.Enumerator enumerator = this.ᜁ.Values.GetEnumerator();
				if (true)
				{
				}
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_F6;
					case 1:
						try
						{
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
									num = 4;
									continue;
								case 3:
									if (enumerator.MoveNext())
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
											XlsChartDataPoint xlsChartDataPoint = enumerator.Current;
											xlsChartDataPoint.ᜁ(A_0);
											num = 1;
											continue;
										}
										}
									}
									num = 0;
									continue;
								case 4:
									goto IL_E3;
								}
								IL_C0:
								num = 3;
								continue;
								goto IL_C0;
							}
							IL_E3:
							goto IL_45;
						}
						finally
						{
							((IDisposable)enumerator).Dispose();
						}
						goto IL_F6;
						IL_45:
						num = 2;
						continue;
					case 2:
						if (this.ᜀ != null)
						{
							num = 0;
							continue;
						}
						return;
					case 3:
						return;
					}
					break;
					IL_F6:
					this.ᜀ.ᜁ(A_0);
					num = 3;
				}
			}
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x000C2D38 File Offset: 0x000C1D38
		public object Clone(object parent, XlsWorkbook book, Dictionary<int, int> fontIndexes, Dictionary<string, string> dicNewSheetNames)
		{
			switch (0)
			{
			default:
			{
				XlsChartDataPointsCollection xlsChartDataPointsCollection;
				for (;;)
				{
					xlsChartDataPointsCollection = (XlsChartDataPointsCollection)base.MemberwiseClone();
					xlsChartDataPointsCollection.SetParent(parent);
					xlsChartDataPointsCollection.ᜀ();
					int count = this.ᜁ.Count;
					xlsChartDataPointsCollection.ᜁ = new Dictionary<int, XlsChartDataPoint>(count);
					int num = 0;
					for (;;)
					{
						Dictionary<int, XlsChartDataPoint>.ValueCollection.Enumerator enumerator;
						switch (num)
						{
						case 0:
							if (this.ᜀ != null)
							{
								num = 3;
								continue;
							}
							goto IL_189;
						case 1:
							goto IL_189;
						case 2:
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
									case 2:
										num = 4;
										continue;
									case 3:
									{
										if (!enumerator.MoveNext())
										{
											num = 2;
											continue;
										}
										XlsChartDataPoint xlsChartDataPoint = enumerator.Current;
										XlsChartDataPoint point = (XlsChartDataPoint)xlsChartDataPoint.ᜀ(xlsChartDataPointsCollection, fontIndexes, dicNewSheetNames);
										xlsChartDataPointsCollection.Add(point);
										goto IL_102;
									}
									case 4:
										goto IL_119;
									}
									IL_AA:
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										IL_102:
										num = 1;
										continue;
									default:
										if (false)
										{
										}
										num = 3;
										continue;
									}
									goto IL_AA;
								}
								IL_119:
								return xlsChartDataPointsCollection;
							}
							finally
							{
								((IDisposable)enumerator).Dispose();
							}
							goto IL_12C;
						case 3:
							xlsChartDataPointsCollection.ᜀ = (XlsChartDataPoint)this.ᜀ.ᜀ(xlsChartDataPointsCollection, fontIndexes, dicNewSheetNames);
							num = 1;
							continue;
						case 4:
							if (count > 0)
							{
								num = 5;
								continue;
							}
							return xlsChartDataPointsCollection;
						case 5:
							goto IL_12C;
						}
						break;
						IL_12C:
						enumerator = this.ᜁ.Values.GetEnumerator();
						num = 2;
						continue;
						IL_189:
						num = 4;
					}
				}
				return xlsChartDataPointsCollection;
			}
			}
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x000C2F00 File Offset: 0x000C1F00
		public void Add(XlsChartDataPoint point)
		{
			int a_ = 1;
			if (point == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("䜶嘸刺匼䬾", a_));
				}
			}
			int index = point.Index;
			this.ᜁ[index] = point;
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x000C2F74 File Offset: 0x000C1F74
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
			this.ᜁ.Clear();
			this.ᜀ = new ChartDataPoint((spr\u2158)base.ReservedHandle, this, 65535);
			this.ᜀ.DataFormat.BarType = BaseFormatType.Rectangle;
			this.ᜀ.DataFormat.BarTopType = TopFormatType.Straight;
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x000C2FF8 File Offset: 0x000C1FF8
		public void UpdateSerieIndex()
		{
			if (true)
			{
			}
			int index = this.ᜂ.Index;
			this.ᜀ.UpdateSerieIndex();
			using (Dictionary<int, XlsChartDataPoint>.ValueCollection.Enumerator enumerator = this.ᜁ.Values.GetEnumerator())
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_AF;
					case 3:
						if (enumerator.MoveNext())
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
								XlsChartDataPoint xlsChartDataPoint = enumerator.Current;
								xlsChartDataPoint.UpdateSerieIndex();
								num = 2;
								continue;
							}
							}
						}
						num = 4;
						continue;
					case 4:
						num = 0;
						continue;
					}
					IL_8C:
					num = 3;
					continue;
					goto IL_8C;
				}
				IL_AF:;
			}
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x000C30E0 File Offset: 0x000C20E0
		public void ClearDataFormats(XlsChartSerieDataFormat format)
		{
			for (;;)
			{
				this.ᜀ.ClearDataFormats(format);
				int num = 0;
				for (;;)
				{
					Dictionary<int, XlsChartDataPoint>.ValueCollection.Enumerator enumerator;
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						if (this.ᜁ.Count == 0)
						{
							num = 1;
							continue;
						}
						goto IL_E5;
					case 1:
						return;
					case 2:
						goto IL_57;
						try
						{
							for (;;)
							{
								IL_57:
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
										num = 4;
										continue;
									case 3:
									{
										if (!enumerator.MoveNext())
										{
											num = 0;
											continue;
										}
										XlsChartDataPoint xlsChartDataPoint = enumerator.Current;
										xlsChartDataPoint.ClearDataFormats(format);
										num = 1;
										continue;
									}
									case 4:
										goto IL_D5;
									}
									IL_96:
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_57;
									default:
										if (false)
										{
										}
										num = 3;
										continue;
									}
									goto IL_96;
								}
							}
							IL_D5:
							return;
						}
						finally
						{
							((IDisposable)enumerator).Dispose();
						}
						goto IL_E5;
					}
					break;
					IL_E5:
					enumerator = this.ᜁ.Values.GetEnumerator();
					num = 2;
				}
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x000C3204 File Offset: 0x000C2204
		internal int DeninedDPCount
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
				return this.ᜁ.Count;
			}
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x000C324C File Offset: 0x000C224C
		public IEnumerator GetEnumerator()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return this.ᜁ.Values.GetEnumerator();
		}

		// Token: 0x04000ED7 RID: 3799
		private byte \u2593\u008B\u00A7\u008F;

		// Token: 0x04000ED8 RID: 3800
		private XlsChartDataPoint ᜀ;

		// Token: 0x04000ED9 RID: 3801
		private bool \u2460\u0090\u00B0\u0085;

		// Token: 0x04000EDA RID: 3802
		private int[] \u2609\u0097\u0083\u0089;

		// Token: 0x04000EDB RID: 3803
		private Dictionary<int, XlsChartDataPoint> ᜁ = new Dictionary<int, XlsChartDataPoint>();

		// Token: 0x04000EDC RID: 3804
		private float[] \u25D9\u00AE\u0083\u008A;

		// Token: 0x04000EDD RID: 3805
		private float[] \u2593\u009D\u00AE\u00AD;

		// Token: 0x04000EDE RID: 3806
		private long[] \u2593\u00A1\u00AF\u009B;

		// Token: 0x04000EDF RID: 3807
		private XlsChartSerie ᜂ;
	}
}
