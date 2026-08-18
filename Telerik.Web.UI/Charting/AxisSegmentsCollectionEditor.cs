using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Charting
{
	// Token: 0x02001749 RID: 5961
	[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
	internal class AxisSegmentsCollectionEditor : CollectionEditor
	{
		// Token: 0x0600E8BC RID: 59580 RVA: 0x0034414B File Offset: 0x0034234B
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public AxisSegmentsCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x0600E8BD RID: 59581 RVA: 0x00344154 File Offset: 0x00342354
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			this.parentObject = (ScaleBreak)context.Instance;
			return base.EditValue(context, provider, value);
		}

		// Token: 0x0600E8BE RID: 59582 RVA: 0x00344180 File Offset: 0x00342380
		protected override object CreateInstance(Type itemType)
		{
			if (!this.parentObject.Enabled || this.parentObject.Parent.AutoScale)
			{
				throw new ChartException("Manual segments for ScaleBreaks may be created only when YAxis.AutoScale = false, and YAxis.ScaleBreaks.Enabled = true.");
			}
			int num = 1;
			bool flag;
			do
			{
				flag = true;
				foreach (AxisSegment axisSegment in this.parentObject.Segments)
				{
					if (string.Equals(axisSegment.Name, "Segment " + num))
					{
						flag = false;
						num++;
						break;
					}
				}
			}
			while (!flag);
			return new AxisSegment("Segment " + num);
		}

		// Token: 0x040042EF RID: 17135
		private ScaleBreak parentObject;
	}
}
