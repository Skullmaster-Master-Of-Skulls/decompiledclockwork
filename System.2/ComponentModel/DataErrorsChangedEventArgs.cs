using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000533 RID: 1331
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class DataErrorsChangedEventArgs : EventArgs
	{
		// Token: 0x06003253 RID: 12883 RVA: 0x000E1A3C File Offset: 0x000DFC3C
		[__DynamicallyInvokable]
		public DataErrorsChangedEventArgs(string propertyName)
		{
			this.propertyName = propertyName;
		}

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06003254 RID: 12884 RVA: 0x000E1A4B File Offset: 0x000DFC4B
		[__DynamicallyInvokable]
		public virtual string PropertyName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x0400296E RID: 10606
		private readonly string propertyName;
	}
}
