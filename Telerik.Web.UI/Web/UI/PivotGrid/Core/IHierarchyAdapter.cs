using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000D48 RID: 3400
	public interface IHierarchyAdapter
	{
		// Token: 0x06007E8D RID: 32397
		IEnumerable<object> GetItems(object item);

		// Token: 0x06007E8E RID: 32398
		object GetItemAt(object item, int index);
	}
}
