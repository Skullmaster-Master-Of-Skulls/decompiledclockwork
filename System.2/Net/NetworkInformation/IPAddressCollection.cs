using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002AE RID: 686
	[__DynamicallyInvokable]
	public class IPAddressCollection : ICollection<IPAddress>, IEnumerable<IPAddress>, IEnumerable
	{
		// Token: 0x06001996 RID: 6550 RVA: 0x0007E1A9 File Offset: 0x0007C3A9
		[__DynamicallyInvokable]
		protected internal IPAddressCollection()
		{
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x0007E1BC File Offset: 0x0007C3BC
		[__DynamicallyInvokable]
		public virtual void CopyTo(IPAddress[] array, int offset)
		{
			this.addresses.CopyTo(array, offset);
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001998 RID: 6552 RVA: 0x0007E1CB File Offset: 0x0007C3CB
		[__DynamicallyInvokable]
		public virtual int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this.addresses.Count;
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001999 RID: 6553 RVA: 0x0007E1D8 File Offset: 0x0007C3D8
		[__DynamicallyInvokable]
		public virtual bool IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return true;
			}
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x0007E1DB File Offset: 0x0007C3DB
		[__DynamicallyInvokable]
		public virtual void Add(IPAddress address)
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x0007E1EC File Offset: 0x0007C3EC
		internal void InternalAdd(IPAddress address)
		{
			this.addresses.Add(address);
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x0007E1FA File Offset: 0x0007C3FA
		[__DynamicallyInvokable]
		public virtual bool Contains(IPAddress address)
		{
			return this.addresses.Contains(address);
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x0007E208 File Offset: 0x0007C408
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x0007E210 File Offset: 0x0007C410
		[__DynamicallyInvokable]
		public virtual IEnumerator<IPAddress> GetEnumerator()
		{
			return this.addresses.GetEnumerator();
		}

		// Token: 0x170005B3 RID: 1459
		[__DynamicallyInvokable]
		public virtual IPAddress this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				return this.addresses[index];
			}
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x0007E22B File Offset: 0x0007C42B
		[__DynamicallyInvokable]
		public virtual bool Remove(IPAddress address)
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x0007E23C File Offset: 0x0007C43C
		[__DynamicallyInvokable]
		public virtual void Clear()
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x04001912 RID: 6418
		private Collection<IPAddress> addresses = new Collection<IPAddress>();
	}
}
