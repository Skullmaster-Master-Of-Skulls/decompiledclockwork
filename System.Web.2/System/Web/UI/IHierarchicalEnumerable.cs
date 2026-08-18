using System;
using System.Collections;

namespace System.Web.UI
{
	// Token: 0x020002A7 RID: 679
	public interface IHierarchicalEnumerable : IEnumerable
	{
		// Token: 0x06001F9B RID: 8091
		IHierarchyData GetHierarchyData(object enumeratedItem);
	}
}
