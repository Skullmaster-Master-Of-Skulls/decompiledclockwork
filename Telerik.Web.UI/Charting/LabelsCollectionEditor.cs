using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x02001756 RID: 5974
	internal class LabelsCollectionEditor : CollectionEditor
	{
		// Token: 0x0600E8F1 RID: 59633 RVA: 0x00345184 File Offset: 0x00343384
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public LabelsCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x0600E8F2 RID: 59634 RVA: 0x00345190 File Offset: 0x00343390
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider isp, object value)
		{
			this.container = (ExtendedLabel)context.Instance;
			ChartLegend chartLegend = this.container as ChartLegend;
			if (chartLegend != null)
			{
				chartLegend.ClearBoundItems(false);
			}
			return base.EditValue(context, isp, value);
		}

		// Token: 0x0600E8F3 RID: 59635 RVA: 0x003451D0 File Offset: 0x003433D0
		protected override object CreateInstance(Type itemType)
		{
			int num = 1;
			bool flag;
			do
			{
				flag = true;
				foreach (LabelItem labelItem in this.container.Items)
				{
					if (string.Equals(labelItem.Name, "Item " + num))
					{
						flag = false;
						num++;
						break;
					}
				}
			}
			while (!flag);
			LabelItem labelItem2 = new LabelItem(this.container.Items);
			labelItem2.appearance = (StyleLabel)this.container.Appearance.ItemAppearance.Clone();
			labelItem2.Marker.appearance = (StyleMarker)this.container.Appearance.ItemMarkerAppearance.Clone();
			labelItem2.TextBlock.appearance = (StyleTextBlock)this.container.Appearance.ItemTextAppearance.Clone();
			labelItem2.Marker.Appearance.styleChart = (labelItem2.Appearance.styleChart = (Chart)this.container.Parent);
			labelItem2.Container = this.container;
			this.container.Add(labelItem2);
			return labelItem2;
		}

		// Token: 0x04004303 RID: 17155
		private ExtendedLabel container;
	}
}
