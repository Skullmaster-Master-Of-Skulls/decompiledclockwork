using System;
using System.Collections.Generic;

namespace Telerik.Charting
{
	// Token: 0x02001739 RID: 5945
	internal class PopularCollection : List<Popular>
	{
		// Token: 0x0600E78F RID: 59279 RVA: 0x0033CA0C File Offset: 0x0033AC0C
		internal PopularCollection CopyPopList()
		{
			PopularCollection popularCollection = new PopularCollection();
			foreach (Popular popular in this)
			{
				Popular item = new Popular(popular.Value, popular.Number, popular.X, popular.YPositive, popular.YNegative);
				popularCollection.Add(item);
			}
			return popularCollection;
		}

		// Token: 0x0600E790 RID: 59280 RVA: 0x0033CA88 File Offset: 0x0033AC88
		internal static PopularCollection GetPopularValues(Chart chart)
		{
			PopularCollection popularCollection = new PopularCollection();
			List<float> list = new List<float>();
			List<float> list2 = new List<float>();
			List<float> list3 = new List<float>();
			List<float> list4 = new List<float>();
			ChartSeriesCollection series = chart.Series;
			for (int i = 0; i < series.Count; i++)
			{
				ChartSeries chartSeries = series[i];
				ChartSeriesType type = chartSeries.Type;
				bool flag = type == ChartSeriesType.StackedBar;
				bool flag2 = type == ChartSeriesType.StackedBar100;
				bool flag3 = type == ChartSeriesType.Bar;
				bool flag4 = type == ChartSeriesType.Gantt;
				bool flag5 = type == ChartSeriesType.CandleStick;
				if (flag3 || flag4 || flag || flag2 || flag5)
				{
					foreach (ChartSeriesItem chartSeriesItem in chartSeries.Items)
					{
						float xvalue = chartSeriesItem.GetXValue();
						if (list2.Contains(xvalue))
						{
							int count = list.Count;
							int j = 0;
							while (j < count)
							{
								if (list[j] == xvalue)
								{
									if ((flag && list3.Contains(xvalue)) || (flag2 && list4.Contains(xvalue)))
									{
										break;
									}
									List<float> list5;
									int index;
									(list5 = list)[index = j + 1] = list5[index] + 1f;
									if (flag)
									{
										list3.Add(xvalue);
									}
									if (flag2)
									{
										list4.Add(xvalue);
										break;
									}
									break;
								}
								else
								{
									j += 2;
								}
							}
						}
						else
						{
							list.Add(xvalue);
							list.Add(1f);
							list2.Add(xvalue);
							if (flag)
							{
								list3.Add(xvalue);
							}
							if (flag2)
							{
								list4.Add(xvalue);
							}
						}
					}
				}
			}
			int count2 = list.Count;
			for (int k = 1; k < count2; k += 2)
			{
				float coordinate = chart.PlotArea.XAxis.GetCoordinate((double)list[k - 1]);
				popularCollection.Add(new Popular(list[k - 1], (int)list[k], coordinate));
			}
			return popularCollection;
		}

		// Token: 0x0600E791 RID: 59281 RVA: 0x0033CC8C File Offset: 0x0033AE8C
		internal int Popularity(float val)
		{
			int count = base.Count;
			for (int i = 0; i < count; i++)
			{
				if (base[i].Value == val)
				{
					return base[i].Number;
				}
			}
			return -1;
		}

		// Token: 0x0600E792 RID: 59282 RVA: 0x0033CCCC File Offset: 0x0033AECC
		internal int GetPopularityIndex(float val)
		{
			int num = 0;
			foreach (Popular popular in this)
			{
				if (popular.Value == val)
				{
					return num;
				}
				num++;
			}
			return -1;
		}
	}
}
