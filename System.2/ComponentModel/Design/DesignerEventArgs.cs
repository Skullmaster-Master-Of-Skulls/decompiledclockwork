using System;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	// Token: 0x020005DE RID: 1502
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class DesignerEventArgs : EventArgs
	{
		// Token: 0x060037D3 RID: 14291 RVA: 0x000F1252 File Offset: 0x000EF452
		public DesignerEventArgs(IDesignerHost host)
		{
			this.host = host;
		}

		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x060037D4 RID: 14292 RVA: 0x000F1261 File Offset: 0x000EF461
		public IDesignerHost Designer
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x04002B08 RID: 11016
		private readonly IDesignerHost host;
	}
}
