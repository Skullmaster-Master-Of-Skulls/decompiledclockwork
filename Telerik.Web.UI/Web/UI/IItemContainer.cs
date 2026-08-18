using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02000062 RID: 98
	internal interface IItemContainer
	{
		// Token: 0x06000381 RID: 897
		IItem CreateItem();

		// Token: 0x06000382 RID: 898
		void RaiseItemDataBound(IItem item);

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000383 RID: 899
		IList Children { get; }
	}
}
