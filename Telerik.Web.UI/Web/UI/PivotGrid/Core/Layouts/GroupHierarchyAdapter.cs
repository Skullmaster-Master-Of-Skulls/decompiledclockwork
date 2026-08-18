using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.Layouts
{
	// Token: 0x02000D49 RID: 3401
	internal class GroupHierarchyAdapter : IHierarchyAdapter
	{
		// Token: 0x06007E8F RID: 32399 RVA: 0x001CFB07 File Offset: 0x001CDD07
		IEnumerable<object> IHierarchyAdapter.GetItems(object item)
		{
			return (item as IGroup).Groups;
		}

		// Token: 0x06007E90 RID: 32400 RVA: 0x001CFB14 File Offset: 0x001CDD14
		object IHierarchyAdapter.GetItemAt(object item, int index)
		{
			return (item as IGroup).Groups[index];
		}
	}
}
