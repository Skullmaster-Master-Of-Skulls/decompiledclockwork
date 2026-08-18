using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x020005A5 RID: 1445
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class RefreshEventArgs : EventArgs
	{
		// Token: 0x06003607 RID: 13831 RVA: 0x000EC651 File Offset: 0x000EA851
		public RefreshEventArgs(object componentChanged)
		{
			this.componentChanged = componentChanged;
			this.typeChanged = componentChanged.GetType();
		}

		// Token: 0x06003608 RID: 13832 RVA: 0x000EC66C File Offset: 0x000EA86C
		public RefreshEventArgs(Type typeChanged)
		{
			this.typeChanged = typeChanged;
		}

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x06003609 RID: 13833 RVA: 0x000EC67B File Offset: 0x000EA87B
		public object ComponentChanged
		{
			get
			{
				return this.componentChanged;
			}
		}

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x0600360A RID: 13834 RVA: 0x000EC683 File Offset: 0x000EA883
		public Type TypeChanged
		{
			get
			{
				return this.typeChanged;
			}
		}

		// Token: 0x04002A94 RID: 10900
		private object componentChanged;

		// Token: 0x04002A95 RID: 10901
		private Type typeChanged;
	}
}
