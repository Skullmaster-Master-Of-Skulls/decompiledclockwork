using System;
using System.Collections;

namespace log4net.Repository.Hierarchy
{
	// Token: 0x020000D0 RID: 208
	internal sealed class ProvisionNode : ArrayList
	{
		// Token: 0x0600063C RID: 1596 RVA: 0x00012F89 File Offset: 0x00011189
		internal ProvisionNode(Logger log)
		{
			this.Add(log);
		}
	}
}
