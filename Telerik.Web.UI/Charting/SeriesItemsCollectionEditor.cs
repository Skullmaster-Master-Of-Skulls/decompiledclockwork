using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Charting
{
	// Token: 0x0200174E RID: 5966
	internal class SeriesItemsCollectionEditor : CollectionEditor
	{
		// Token: 0x0600E8CF RID: 59599 RVA: 0x003446E8 File Offset: 0x003428E8
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public SeriesItemsCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x0600E8D0 RID: 59600 RVA: 0x003446F4 File Offset: 0x003428F4
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider isp, object value)
		{
			this.chartSeries = (ChartSeries)context.Instance;
			return base.EditValue(context, isp, value);
		}

		// Token: 0x0600E8D1 RID: 59601 RVA: 0x00344720 File Offset: 0x00342920
		protected override object CreateInstance(Type itemType)
		{
			int num = 1;
			bool flag;
			do
			{
				flag = true;
				foreach (ChartSeriesItem chartSeriesItem in this.chartSeries.Items)
				{
					if (string.Equals(chartSeriesItem.Name, "Item " + num))
					{
						flag = false;
						num++;
						break;
					}
				}
			}
			while (!flag);
			return new ChartSeriesItem(this.chartSeries)
			{
				Name = "Item " + num,
				PointAppearance = 
				{
					Chart = this.chartSeries.Chart
				},
				Label = 
				{
					Appearance = 
					{
						Chart = this.chartSeries.Chart
					}
				}
			};
		}

		// Token: 0x040042F5 RID: 17141
		private ChartSeries chartSeries;
	}
}
