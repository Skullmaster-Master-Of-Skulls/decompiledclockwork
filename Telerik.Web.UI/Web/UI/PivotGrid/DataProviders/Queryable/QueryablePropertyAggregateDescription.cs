using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Queryable;

namespace Telerik.Web.UI.PivotGrid.DataProviders.Queryable
{
	// Token: 0x02000D65 RID: 3429
	[DataContract]
	public sealed class QueryablePropertyAggregateDescription : QueryablePropertyAggregateDescriptionBase
	{
		// Token: 0x06007FF6 RID: 32758 RVA: 0x001D45E2 File Offset: 0x001D27E2
		protected override Cloneable CreateInstanceCore()
		{
			return new QueryablePropertyAggregateDescription();
		}
	}
}
