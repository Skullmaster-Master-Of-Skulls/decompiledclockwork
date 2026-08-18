using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core.Filtering
{
	// Token: 0x020006BC RID: 1724
	[DataContract]
	public abstract class LocalCondition : Condition, ILocalFilterCondition
	{
		// Token: 0x06003DEF RID: 15855
		[SuppressMessage("Microsoft.Security", "CA2119:SealMethodsThatSatisfyPrivateInterfaces", Justification = "It is too early to expose the interface in future but it is well tested and eventually go live.")]
		public abstract bool PassesFilter(object item);
	}
}
