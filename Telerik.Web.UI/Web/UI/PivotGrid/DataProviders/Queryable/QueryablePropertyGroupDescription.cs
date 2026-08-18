using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Queryable;

namespace Telerik.Web.UI.PivotGrid.DataProviders.Queryable
{
	// Token: 0x02000D66 RID: 3430
	[DataContract]
	public sealed class QueryablePropertyGroupDescription : QueryablePropertyGroupDescriptionBase
	{
		// Token: 0x06007FF8 RID: 32760 RVA: 0x001D45F1 File Offset: 0x001D27F1
		protected override Cloneable CreateInstanceCore()
		{
			return new QueryablePropertyGroupDescription();
		}
	}
}
