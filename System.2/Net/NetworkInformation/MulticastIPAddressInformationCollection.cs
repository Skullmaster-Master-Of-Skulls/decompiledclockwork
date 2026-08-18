using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002AD RID: 685
	[__DynamicallyInvokable]
	public class MulticastIPAddressInformationCollection : ICollection<MulticastIPAddressInformation>, IEnumerable<MulticastIPAddressInformation>, IEnumerable
	{
		// Token: 0x0600198A RID: 6538 RVA: 0x0007E105 File Offset: 0x0007C305
		[__DynamicallyInvokable]
		protected internal MulticastIPAddressInformationCollection()
		{
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x0007E118 File Offset: 0x0007C318
		[__DynamicallyInvokable]
		public virtual void CopyTo(MulticastIPAddressInformation[] array, int offset)
		{
			this.addresses.CopyTo(array, offset);
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x0600198C RID: 6540 RVA: 0x0007E127 File Offset: 0x0007C327
		[__DynamicallyInvokable]
		public virtual int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this.addresses.Count;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x0600198D RID: 6541 RVA: 0x0007E134 File Offset: 0x0007C334
		[__DynamicallyInvokable]
		public virtual bool IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return true;
			}
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x0007E137 File Offset: 0x0007C337
		[__DynamicallyInvokable]
		public virtual void Add(MulticastIPAddressInformation address)
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x0007E148 File Offset: 0x0007C348
		internal void InternalAdd(MulticastIPAddressInformation address)
		{
			this.addresses.Add(address);
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x0007E156 File Offset: 0x0007C356
		[__DynamicallyInvokable]
		public virtual bool Contains(MulticastIPAddressInformation address)
		{
			return this.addresses.Contains(address);
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x0007E164 File Offset: 0x0007C364
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x0007E16C File Offset: 0x0007C36C
		[__DynamicallyInvokable]
		public virtual IEnumerator<MulticastIPAddressInformation> GetEnumerator()
		{
			return this.addresses.GetEnumerator();
		}

		// Token: 0x170005B0 RID: 1456
		[__DynamicallyInvokable]
		public virtual MulticastIPAddressInformation this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				return this.addresses[index];
			}
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x0007E187 File Offset: 0x0007C387
		[__DynamicallyInvokable]
		public virtual bool Remove(MulticastIPAddressInformation address)
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x0007E198 File Offset: 0x0007C398
		[__DynamicallyInvokable]
		public virtual void Clear()
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x04001911 RID: 6417
		private Collection<MulticastIPAddressInformation> addresses = new Collection<MulticastIPAddressInformation>();
	}
}
