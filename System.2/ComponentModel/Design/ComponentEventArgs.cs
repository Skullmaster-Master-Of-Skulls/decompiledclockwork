using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	// Token: 0x020005D0 RID: 1488
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class ComponentEventArgs : EventArgs
	{
		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x0600377B RID: 14203 RVA: 0x000F082D File Offset: 0x000EEA2D
		public virtual IComponent Component
		{
			get
			{
				return this.component;
			}
		}

		// Token: 0x0600377C RID: 14204 RVA: 0x000F0835 File Offset: 0x000EEA35
		public ComponentEventArgs(IComponent component)
		{
			this.component = component;
		}

		// Token: 0x04002AF8 RID: 11000
		private IComponent component;
	}
}
