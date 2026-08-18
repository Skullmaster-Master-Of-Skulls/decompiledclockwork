using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Charting
{
	// Token: 0x0200174D RID: 5965
	internal class SeriesCollectionEditor : CollectionEditor
	{
		// Token: 0x0600E8CC RID: 59596 RVA: 0x003445FC File Offset: 0x003427FC
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public SeriesCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x0600E8CD RID: 59597 RVA: 0x00344608 File Offset: 0x00342808
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider isp, object value)
		{
			this.chartComponent = (IChartComponent)context.Instance;
			return base.EditValue(context, isp, value);
		}

		// Token: 0x0600E8CE RID: 59598 RVA: 0x00344634 File Offset: 0x00342834
		protected override object CreateInstance(Type itemType)
		{
			int num = 1;
			bool flag;
			do
			{
				flag = true;
				foreach (ChartSeries chartSeries in this.chartComponent.Chart.Series)
				{
					if (string.Equals(chartSeries.Name, "Series " + num))
					{
						flag = false;
						num++;
						break;
					}
				}
			}
			while (!flag);
			return new ChartSeries("Series " + num, this.chartComponent.Chart.DefaultType, this.chartComponent.Chart.Series);
		}

		// Token: 0x040042F4 RID: 17140
		private IChartComponent chartComponent;
	}
}
