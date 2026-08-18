using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200029F RID: 671
	[__DynamicallyInvokable]
	public abstract class IPAddressInformation
	{
		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001909 RID: 6409
		[__DynamicallyInvokable]
		public abstract IPAddress Address { [__DynamicallyInvokable] get; }

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x0600190A RID: 6410
		[__DynamicallyInvokable]
		public abstract bool IsDnsEligible { [__DynamicallyInvokable] get; }

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x0600190B RID: 6411
		[__DynamicallyInvokable]
		public abstract bool IsTransient { [__DynamicallyInvokable] get; }

		// Token: 0x0600190C RID: 6412 RVA: 0x0007DF21 File Offset: 0x0007C121
		[__DynamicallyInvokable]
		protected IPAddressInformation()
		{
		}
	}
}
