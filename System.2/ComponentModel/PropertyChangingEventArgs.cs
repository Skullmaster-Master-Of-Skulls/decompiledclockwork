using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200059A RID: 1434
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class PropertyChangingEventArgs : EventArgs
	{
		// Token: 0x06003534 RID: 13620 RVA: 0x000E7D65 File Offset: 0x000E5F65
		[__DynamicallyInvokable]
		public PropertyChangingEventArgs(string propertyName)
		{
			this.propertyName = propertyName;
		}

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x06003535 RID: 13621 RVA: 0x000E7D74 File Offset: 0x000E5F74
		[__DynamicallyInvokable]
		public virtual string PropertyName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x04002A43 RID: 10819
		private readonly string propertyName;
	}
}
