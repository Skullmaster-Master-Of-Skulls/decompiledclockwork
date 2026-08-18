using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x02000606 RID: 1542
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class DesignerLoader
	{
		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x060038AC RID: 14508 RVA: 0x000F1F02 File Offset: 0x000F0102
		public virtual bool Loading
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060038AD RID: 14509
		public abstract void BeginLoad(IDesignerLoaderHost host);

		// Token: 0x060038AE RID: 14510
		public abstract void Dispose();

		// Token: 0x060038AF RID: 14511 RVA: 0x000F1F05 File Offset: 0x000F0105
		public virtual void Flush()
		{
		}
	}
}
