using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core.Filtering
{
	// Token: 0x02000CCB RID: 3275
	[DataContract]
	public abstract class SiblingGroupsFilter : GroupFilter
	{
		// Token: 0x06007A84 RID: 31364
		protected internal abstract ICollection<IGroup> Filter(IReadOnlyList<IGroup> groups, IAggregateResultProvider results, PivotAxis axis, int level);
	}
}
