using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x0200174A RID: 5962
	internal class ChartAxisItemsCollectionEditor : CollectionEditor
	{
		// Token: 0x0600E8BF RID: 59583 RVA: 0x00344238 File Offset: 0x00342438
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public ChartAxisItemsCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x0600E8C0 RID: 59584 RVA: 0x00344241 File Offset: 0x00342441
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider isp, object value)
		{
			this.chartAxis = (ChartAxis)context.Instance;
			return base.EditValue(context, isp, value);
		}

		// Token: 0x0600E8C1 RID: 59585 RVA: 0x00344260 File Offset: 0x00342460
		protected override object CreateInstance(Type itemType)
		{
			return new ChartAxisItem
			{
				appearance = (StyleLabel)this.chartAxis.Appearance.LabelAppearance.Clone(),
				TextBlock = 
				{
					appearance = (StyleAxisItemText)this.chartAxis.Appearance.TextAppearance.Clone()
				},
				Appearance = 
				{
					styleChart = this.chartAxis.Chart
				},
				Marker = 
				{
					Appearance = 
					{
						styleChart = this.chartAxis.Chart
					}
				}
			};
		}

		// Token: 0x040042F0 RID: 17136
		private ChartAxis chartAxis;
	}
}
