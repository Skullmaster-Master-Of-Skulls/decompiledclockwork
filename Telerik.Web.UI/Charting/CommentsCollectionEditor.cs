using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Charting
{
	// Token: 0x02001751 RID: 5969
	internal class CommentsCollectionEditor : CollectionEditor
	{
		// Token: 0x0600E8DC RID: 59612 RVA: 0x003449C5 File Offset: 0x00342BC5
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public CommentsCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x0600E8DD RID: 59613 RVA: 0x003449D0 File Offset: 0x00342BD0
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider isp, object value)
		{
			this.chartComponent = (IChartComponent)context.Instance;
			return base.EditValue(context, isp, value);
		}

		// Token: 0x040042F8 RID: 17144
		private IChartComponent chartComponent;
	}
}
