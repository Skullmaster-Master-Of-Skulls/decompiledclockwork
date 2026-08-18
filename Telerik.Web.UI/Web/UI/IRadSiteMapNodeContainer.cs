using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000EF7 RID: 3831
	public interface IRadSiteMapNodeContainer
	{
		// Token: 0x17002DFD RID: 11773
		// (get) Token: 0x06009137 RID: 37175
		IRadSiteMapNodeContainer Owner { get; }

		// Token: 0x17002DFE RID: 11774
		// (get) Token: 0x06009138 RID: 37176
		RadSiteMapNodeCollection Nodes { get; }
	}
}
