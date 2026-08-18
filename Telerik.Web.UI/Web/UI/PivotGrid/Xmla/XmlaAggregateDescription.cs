using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D72 RID: 3442
	[DataContract]
	public sealed class XmlaAggregateDescription : OlapAggregateDescription
	{
		// Token: 0x06008096 RID: 32918 RVA: 0x001D6B41 File Offset: 0x001D4D41
		protected override Cloneable CreateInstanceCore()
		{
			return new XmlaAggregateDescription();
		}
	}
}
