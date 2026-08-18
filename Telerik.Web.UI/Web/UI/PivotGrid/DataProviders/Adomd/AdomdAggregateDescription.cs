using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.DataProviders.Adomd
{
	// Token: 0x02000D5D RID: 3421
	[DataContract]
	public sealed class AdomdAggregateDescription : OlapAggregateDescription
	{
		// Token: 0x06007FA6 RID: 32678 RVA: 0x001D2C3F File Offset: 0x001D0E3F
		protected override Cloneable CreateInstanceCore()
		{
			return new AdomdAggregateDescription();
		}
	}
}
