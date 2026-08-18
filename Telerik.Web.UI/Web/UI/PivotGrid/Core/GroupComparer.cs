using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x020006E2 RID: 1762
	[DataContract]
	public abstract class GroupComparer : SettingsNode
	{
		// Token: 0x06003EE5 RID: 16101
		public abstract int CompareGroups(IAggregateResultProvider results, IGroup left, IGroup right, PivotAxis axis);
	}
}
