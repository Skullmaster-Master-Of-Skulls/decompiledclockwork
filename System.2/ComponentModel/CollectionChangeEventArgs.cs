using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000526 RID: 1318
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class CollectionChangeEventArgs : EventArgs
	{
		// Token: 0x060031FA RID: 12794 RVA: 0x000E08E4 File Offset: 0x000DEAE4
		public CollectionChangeEventArgs(CollectionChangeAction action, object element)
		{
			this.action = action;
			this.element = element;
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x060031FB RID: 12795 RVA: 0x000E08FA File Offset: 0x000DEAFA
		public virtual CollectionChangeAction Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x060031FC RID: 12796 RVA: 0x000E0902 File Offset: 0x000DEB02
		public virtual object Element
		{
			get
			{
				return this.element;
			}
		}

		// Token: 0x0400295A RID: 10586
		private CollectionChangeAction action;

		// Token: 0x0400295B RID: 10587
		private object element;
	}
}
