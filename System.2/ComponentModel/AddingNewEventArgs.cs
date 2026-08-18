using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200050C RID: 1292
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class AddingNewEventArgs : EventArgs
	{
		// Token: 0x0600310C RID: 12556 RVA: 0x000DEC9E File Offset: 0x000DCE9E
		public AddingNewEventArgs()
		{
		}

		// Token: 0x0600310D RID: 12557 RVA: 0x000DECA6 File Offset: 0x000DCEA6
		public AddingNewEventArgs(object newObject)
		{
			this.newObject = newObject;
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x0600310E RID: 12558 RVA: 0x000DECB5 File Offset: 0x000DCEB5
		// (set) Token: 0x0600310F RID: 12559 RVA: 0x000DECBD File Offset: 0x000DCEBD
		public object NewObject
		{
			get
			{
				return this.newObject;
			}
			set
			{
				this.newObject = value;
			}
		}

		// Token: 0x04002908 RID: 10504
		private object newObject;
	}
}
