using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002B1 RID: 689
	[__DynamicallyInvokable]
	public class GatewayIPAddressInformationCollection : ICollection<GatewayIPAddressInformation>, IEnumerable<GatewayIPAddressInformation>, IEnumerable
	{
		// Token: 0x060019A7 RID: 6567 RVA: 0x0007E2C0 File Offset: 0x0007C4C0
		[__DynamicallyInvokable]
		protected internal GatewayIPAddressInformationCollection()
		{
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x0007E2D3 File Offset: 0x0007C4D3
		[__DynamicallyInvokable]
		public virtual void CopyTo(GatewayIPAddressInformation[] array, int offset)
		{
			this.addresses.CopyTo(array, offset);
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x060019A9 RID: 6569 RVA: 0x0007E2E2 File Offset: 0x0007C4E2
		[__DynamicallyInvokable]
		public virtual int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this.addresses.Count;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x060019AA RID: 6570 RVA: 0x0007E2EF File Offset: 0x0007C4EF
		[__DynamicallyInvokable]
		public virtual bool IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return true;
			}
		}

		// Token: 0x170005B8 RID: 1464
		[__DynamicallyInvokable]
		public virtual GatewayIPAddressInformation this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				return this.addresses[index];
			}
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x0007E300 File Offset: 0x0007C500
		[__DynamicallyInvokable]
		public virtual void Add(GatewayIPAddressInformation address)
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x0007E311 File Offset: 0x0007C511
		internal void InternalAdd(GatewayIPAddressInformation address)
		{
			this.addresses.Add(address);
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x0007E31F File Offset: 0x0007C51F
		[__DynamicallyInvokable]
		public virtual bool Contains(GatewayIPAddressInformation address)
		{
			return this.addresses.Contains(address);
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x0007E32D File Offset: 0x0007C52D
		[__DynamicallyInvokable]
		public virtual IEnumerator<GatewayIPAddressInformation> GetEnumerator()
		{
			return this.addresses.GetEnumerator();
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x0007E33A File Offset: 0x0007C53A
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x0007E342 File Offset: 0x0007C542
		[__DynamicallyInvokable]
		public virtual bool Remove(GatewayIPAddressInformation address)
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x0007E353 File Offset: 0x0007C553
		[__DynamicallyInvokable]
		public virtual void Clear()
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x04001914 RID: 6420
		private Collection<GatewayIPAddressInformation> addresses = new Collection<GatewayIPAddressInformation>();
	}
}
