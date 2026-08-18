using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core.Filtering
{
	// Token: 0x020006DF RID: 1759
	[DataContract]
	public abstract class SingleGroupFilter : GroupFilter
	{
		// Token: 0x06003ECC RID: 16076
		protected internal abstract bool Filter(IGroup group, IAggregateResultProvider results, PivotAxis axis);
	}
}
