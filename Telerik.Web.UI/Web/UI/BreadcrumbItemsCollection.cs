using System;
using System.Collections;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200000F RID: 15
	[ParseChildren(typeof(BreadcrumbItem))]
	public class BreadcrumbItemsCollection : StronglyTypedStateManagedCollection<BreadcrumbItem>
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000109 RID: 265 RVA: 0x0000391D File Offset: 0x00001B1D
		internal IList ItemsList
		{
			get
			{
				return base.List;
			}
		}
	}
}
