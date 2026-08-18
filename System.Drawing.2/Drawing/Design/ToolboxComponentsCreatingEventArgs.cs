using System;
using System.ComponentModel.Design;
using System.Security.Permissions;

namespace System.Drawing.Design
{
	// Token: 0x0200007D RID: 125
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public class ToolboxComponentsCreatingEventArgs : EventArgs
	{
		// Token: 0x0600086F RID: 2159 RVA: 0x00020E22 File Offset: 0x0001F022
		public ToolboxComponentsCreatingEventArgs(IDesignerHost host)
		{
			this.host = host;
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x00020E31 File Offset: 0x0001F031
		public IDesignerHost DesignerHost
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x0400070D RID: 1805
		private readonly IDesignerHost host;
	}
}
