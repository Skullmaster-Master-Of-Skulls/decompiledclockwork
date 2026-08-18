using System;
using Spire.Xls.Core.Spreadsheet.Charts;

namespace Spire.Xls.Charts
{
	// Token: 0x02000180 RID: 384
	public class ChartSeries : XlsChartSeries
	{
		// Token: 0x06001239 RID: 4665 RVA: 0x000B14F0 File Offset: 0x000B04F0
		internal ChartSeries(spr\u2158 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x000B1508 File Offset: 0x000B0508
		public ChartSerie Add(ChartSerie serieToAdd)
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
			return (ChartSerie)base.Add(serieToAdd);
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x000B1550 File Offset: 0x000B0550
		public void ClearDataFormats(ChartSerieDataFormat format)
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
			base.ᜀ(format);
		}

		// Token: 0x1700066D RID: 1645
		public ChartSerie this[int index]
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
				return (ChartSerie)base.List[index];
			}
		}

		// Token: 0x1700066E RID: 1646
		public ChartSerie this[string name]
		{
			get
			{
				ChartSerie chartSerie;
				for (;;)
				{
					IL_20:
					int num = 0;
					int count = base.Count;
					int num2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_57:
						goto IL_A1;
					default:
						if (false)
						{
						}
						num2 = 0;
						break;
					}
					for (;;)
					{
						IL_02:
						switch (num2)
						{
						case 0:
							goto IL_57;
						case 1:
							goto IL_C3;
						case 2:
							if (chartSerie.Name == name)
							{
								num2 = 4;
								continue;
							}
							num++;
							num2 = 5;
							continue;
						case 3:
							if (true)
							{
							}
							if (num >= count)
							{
								num2 = 1;
								continue;
							}
							chartSerie = (ChartSerie)base.List[num];
							num2 = 2;
							continue;
						case 4:
							return chartSerie;
						case 5:
							goto IL_65;
						}
						goto IL_20;
					}
					IL_65:
					IL_A1:
					num2 = 3;
					goto IL_02;
				}
				return chartSerie;
				IL_C3:
				return null;
			}
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x000B16B4 File Offset: 0x000B06B4
		public new ChartSerie Add()
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
			ChartSerie chartSerie = new ChartSerie((spr\u2158)base.ReservedHandle, this);
			chartSerie.SetDefaultName(base.ᜀ());
			chartSerie.IsDefaultName = true;
			return this.Add(chartSerie);
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x000B171C File Offset: 0x000B071C
		public new ChartSerie Add(string name)
		{
			ChartSerie chartSerie;
			for (;;)
			{
				chartSerie = new ChartSerie((spr\u2158)base.ReservedHandle, this);
				chartSerie.Name = name;
				if (true)
				{
				}
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.m_chart.ChartTitle = name;
						num = 2;
						continue;
					case 1:
						if (base.Count == 0)
						{
							num = 0;
							continue;
						}
						goto IL_6F;
					case 2:
						goto IL_6F;
					}
					break;
					IL_6F:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_85;
					}
				}
			}
			IL_85:
			if (false)
			{
			}
			return this.Add(chartSerie);
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x000B17BC File Offset: 0x000B07BC
		public new ChartSerie Add(ExcelChartType serieType)
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
			ChartSerie chartSerie = this.Add();
			chartSerie.SerieType = serieType;
			return chartSerie;
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x000B1808 File Offset: 0x000B0808
		public new ChartSerie Add(string name, ExcelChartType serieType)
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
			ChartSerie chartSerie = this.Add(name);
			chartSerie.SerieType = serieType;
			return chartSerie;
		}
	}
}
