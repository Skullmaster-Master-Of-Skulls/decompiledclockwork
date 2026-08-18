using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x020006EC RID: 1772
	[DataContract]
	public abstract class FilterDescription : DescriptionBase
	{
		// Token: 0x06003F07 RID: 16135 RVA: 0x000C88CA File Offset: 0x000C6ACA
		internal virtual bool RequiresRefreshForDistinct()
		{
			return true;
		}
	}
}
