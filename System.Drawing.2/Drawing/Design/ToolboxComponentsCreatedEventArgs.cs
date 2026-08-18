using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Drawing.Design
{
	// Token: 0x0200007B RID: 123
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public class ToolboxComponentsCreatedEventArgs : EventArgs
	{
		// Token: 0x06000869 RID: 2153 RVA: 0x00020E01 File Offset: 0x0001F001
		public ToolboxComponentsCreatedEventArgs(IComponent[] components)
		{
			this.comps = components;
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x00020E10 File Offset: 0x0001F010
		public IComponent[] Components
		{
			get
			{
				return (IComponent[])this.comps.Clone();
			}
		}

		// Token: 0x0400070C RID: 1804
		private readonly IComponent[] comps;
	}
}
