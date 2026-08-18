using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000556 RID: 1366
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class HandledEventArgs : EventArgs
	{
		// Token: 0x0600335F RID: 13151 RVA: 0x000E40A2 File Offset: 0x000E22A2
		public HandledEventArgs() : this(false)
		{
		}

		// Token: 0x06003360 RID: 13152 RVA: 0x000E40AB File Offset: 0x000E22AB
		public HandledEventArgs(bool defaultHandledValue)
		{
			this.handled = defaultHandledValue;
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x06003361 RID: 13153 RVA: 0x000E40BA File Offset: 0x000E22BA
		// (set) Token: 0x06003362 RID: 13154 RVA: 0x000E40C2 File Offset: 0x000E22C2
		public bool Handled
		{
			get
			{
				return this.handled;
			}
			set
			{
				this.handled = value;
			}
		}

		// Token: 0x040029C0 RID: 10688
		private bool handled;
	}
}
