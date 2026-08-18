using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000518 RID: 1304
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class ComponentEditor
	{
		// Token: 0x06003173 RID: 12659 RVA: 0x000DF9B8 File Offset: 0x000DDBB8
		public bool EditComponent(object component)
		{
			return this.EditComponent(null, component);
		}

		// Token: 0x06003174 RID: 12660
		public abstract bool EditComponent(ITypeDescriptorContext context, object component);
	}
}
