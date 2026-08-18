using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200056D RID: 1389
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class InstanceCreationEditor
	{
		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x060033C4 RID: 13252 RVA: 0x000E41D0 File Offset: 0x000E23D0
		public virtual string Text
		{
			get
			{
				return SR.GetString("InstanceCreationEditorDefaultText");
			}
		}

		// Token: 0x060033C5 RID: 13253
		public abstract object CreateInstance(ITypeDescriptorContext context, Type instanceType);
	}
}
