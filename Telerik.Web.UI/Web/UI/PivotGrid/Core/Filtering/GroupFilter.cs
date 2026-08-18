using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core.Filtering
{
	// Token: 0x020006DE RID: 1758
	[DataContract]
	public abstract class GroupFilter : SettingsNode, IDescriptionsReferencing
	{
		// Token: 0x06003EC8 RID: 16072 RVA: 0x000C80A5 File Offset: 0x000C62A5
		internal GroupFilter()
		{
		}

		// Token: 0x06003EC9 RID: 16073 RVA: 0x000C80AD File Offset: 0x000C62AD
		internal virtual bool TrackDescriptions(IDescriptionIndexMap map)
		{
			return true;
		}

		// Token: 0x06003ECA RID: 16074 RVA: 0x000C80B0 File Offset: 0x000C62B0
		bool IDescriptionsReferencing.TrackDescriptions(IDescriptionIndexMap map)
		{
			return this.TrackDescriptions(map);
		}
	}
}
