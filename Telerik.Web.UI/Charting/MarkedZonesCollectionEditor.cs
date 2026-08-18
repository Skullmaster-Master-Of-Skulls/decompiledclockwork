using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Charting
{
	// Token: 0x02001757 RID: 5975
	internal class MarkedZonesCollectionEditor : CollectionEditor
	{
		// Token: 0x0600E8F4 RID: 59636 RVA: 0x00345310 File Offset: 0x00343510
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public MarkedZonesCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x0600E8F5 RID: 59637 RVA: 0x00345319 File Offset: 0x00343519
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider isp, object value)
		{
			this._plotArea = (ChartPlotArea)context.Instance;
			return base.EditValue(context, isp, value);
		}

		// Token: 0x0600E8F6 RID: 59638 RVA: 0x00345338 File Offset: 0x00343538
		protected override object CreateInstance(Type itemType)
		{
			int num = 1;
			bool flag;
			do
			{
				flag = true;
				foreach (ChartMarkedZone chartMarkedZone in this._plotArea.MarkedZones)
				{
					if (string.Equals(chartMarkedZone.Name, "Marked zone " + num))
					{
						flag = false;
						num++;
						break;
					}
				}
			}
			while (!flag);
			return new ChartMarkedZone("Marked zone " + num);
		}

		// Token: 0x04004304 RID: 17156
		private ChartPlotArea _plotArea;
	}
}
