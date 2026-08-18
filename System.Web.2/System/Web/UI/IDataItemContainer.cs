using System;

namespace System.Web.UI
{
	// Token: 0x0200029F RID: 671
	public interface IDataItemContainer : INamingContainer
	{
		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06001F84 RID: 8068
		object DataItem { get; }

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06001F85 RID: 8069
		int DataItemIndex { get; }

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06001F86 RID: 8070
		int DisplayIndex { get; }
	}
}
