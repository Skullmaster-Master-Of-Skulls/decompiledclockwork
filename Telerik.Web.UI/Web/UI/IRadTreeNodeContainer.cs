using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200129A RID: 4762
	public interface IRadTreeNodeContainer
	{
		// Token: 0x17004012 RID: 16402
		// (get) Token: 0x0600C665 RID: 50789
		IRadTreeNodeContainer Owner { get; }

		// Token: 0x17004013 RID: 16403
		// (get) Token: 0x0600C666 RID: 50790
		RadTreeNodeCollection Nodes { get; }
	}
}
