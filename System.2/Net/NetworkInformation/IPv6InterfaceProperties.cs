using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002B3 RID: 691
	[__DynamicallyInvokable]
	public abstract class IPv6InterfaceProperties
	{
		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060019BB RID: 6587
		[__DynamicallyInvokable]
		public abstract int Index { [__DynamicallyInvokable] get; }

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x060019BC RID: 6588
		[__DynamicallyInvokable]
		public abstract int Mtu { [__DynamicallyInvokable] get; }

		// Token: 0x060019BD RID: 6589 RVA: 0x0007E36C File Offset: 0x0007C56C
		[__DynamicallyInvokable]
		public virtual long GetScopeId(ScopeLevel scopeLevel)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x0007E373 File Offset: 0x0007C573
		[__DynamicallyInvokable]
		protected IPv6InterfaceProperties()
		{
		}
	}
}
