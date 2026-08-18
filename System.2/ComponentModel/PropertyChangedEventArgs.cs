using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000598 RID: 1432
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class PropertyChangedEventArgs : EventArgs
	{
		// Token: 0x0600352E RID: 13614 RVA: 0x000E7D4E File Offset: 0x000E5F4E
		[__DynamicallyInvokable]
		public PropertyChangedEventArgs(string propertyName)
		{
			this.propertyName = propertyName;
		}

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x0600352F RID: 13615 RVA: 0x000E7D5D File Offset: 0x000E5F5D
		[__DynamicallyInvokable]
		public virtual string PropertyName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x04002A42 RID: 10818
		private readonly string propertyName;
	}
}
