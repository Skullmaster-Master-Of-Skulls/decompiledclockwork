using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000530 RID: 1328
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class ContainerFilterService
	{
		// Token: 0x0600323A RID: 12858 RVA: 0x000E157C File Offset: 0x000DF77C
		public virtual ComponentCollection FilterComponents(ComponentCollection components)
		{
			return components;
		}
	}
}
