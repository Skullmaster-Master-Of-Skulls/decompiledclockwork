using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	// Token: 0x020005D2 RID: 1490
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class ComponentRenameEventArgs : EventArgs
	{
		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x06003781 RID: 14209 RVA: 0x000F0844 File Offset: 0x000EEA44
		public object Component
		{
			get
			{
				return this.component;
			}
		}

		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x06003782 RID: 14210 RVA: 0x000F084C File Offset: 0x000EEA4C
		public virtual string OldName
		{
			get
			{
				return this.oldName;
			}
		}

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x06003783 RID: 14211 RVA: 0x000F0854 File Offset: 0x000EEA54
		public virtual string NewName
		{
			get
			{
				return this.newName;
			}
		}

		// Token: 0x06003784 RID: 14212 RVA: 0x000F085C File Offset: 0x000EEA5C
		public ComponentRenameEventArgs(object component, string oldName, string newName)
		{
			this.oldName = oldName;
			this.newName = newName;
			this.component = component;
		}

		// Token: 0x04002AF9 RID: 11001
		private object component;

		// Token: 0x04002AFA RID: 11002
		private string oldName;

		// Token: 0x04002AFB RID: 11003
		private string newName;
	}
}
