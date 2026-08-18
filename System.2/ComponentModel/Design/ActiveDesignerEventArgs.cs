using System;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	// Token: 0x020005C8 RID: 1480
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class ActiveDesignerEventArgs : EventArgs
	{
		// Token: 0x06003758 RID: 14168 RVA: 0x000F067C File Offset: 0x000EE87C
		public ActiveDesignerEventArgs(IDesignerHost oldDesigner, IDesignerHost newDesigner)
		{
			this.oldDesigner = oldDesigner;
			this.newDesigner = newDesigner;
		}

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x06003759 RID: 14169 RVA: 0x000F0692 File Offset: 0x000EE892
		public IDesignerHost OldDesigner
		{
			get
			{
				return this.oldDesigner;
			}
		}

		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x0600375A RID: 14170 RVA: 0x000F069A File Offset: 0x000EE89A
		public IDesignerHost NewDesigner
		{
			get
			{
				return this.newDesigner;
			}
		}

		// Token: 0x04002AED RID: 10989
		private readonly IDesignerHost oldDesigner;

		// Token: 0x04002AEE RID: 10990
		private readonly IDesignerHost newDesigner;
	}
}
