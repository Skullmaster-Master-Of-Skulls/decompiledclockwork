using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002AB RID: 683
	[__DynamicallyInvokable]
	public class UnicastIPAddressInformationCollection : ICollection<UnicastIPAddressInformation>, IEnumerable<UnicastIPAddressInformation>, IEnumerable
	{
		// Token: 0x06001977 RID: 6519 RVA: 0x0007E059 File Offset: 0x0007C259
		[__DynamicallyInvokable]
		protected internal UnicastIPAddressInformationCollection()
		{
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x0007E06C File Offset: 0x0007C26C
		[__DynamicallyInvokable]
		public virtual void CopyTo(UnicastIPAddressInformation[] array, int offset)
		{
			this.addresses.CopyTo(array, offset);
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001979 RID: 6521 RVA: 0x0007E07B File Offset: 0x0007C27B
		[__DynamicallyInvokable]
		public virtual int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this.addresses.Count;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x0600197A RID: 6522 RVA: 0x0007E088 File Offset: 0x0007C288
		[__DynamicallyInvokable]
		public virtual bool IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return true;
			}
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x0007E08B File Offset: 0x0007C28B
		[__DynamicallyInvokable]
		public virtual void Add(UnicastIPAddressInformation address)
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x0007E09C File Offset: 0x0007C29C
		internal void InternalAdd(UnicastIPAddressInformation address)
		{
			this.addresses.Add(address);
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x0007E0AA File Offset: 0x0007C2AA
		[__DynamicallyInvokable]
		public virtual bool Contains(UnicastIPAddressInformation address)
		{
			return this.addresses.Contains(address);
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x0007E0B8 File Offset: 0x0007C2B8
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x0007E0C0 File Offset: 0x0007C2C0
		[__DynamicallyInvokable]
		public virtual IEnumerator<UnicastIPAddressInformation> GetEnumerator()
		{
			return this.addresses.GetEnumerator();
		}

		// Token: 0x170005A7 RID: 1447
		[__DynamicallyInvokable]
		public virtual UnicastIPAddressInformation this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				return this.addresses[index];
			}
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x0007E0DB File Offset: 0x0007C2DB
		[__DynamicallyInvokable]
		public virtual bool Remove(UnicastIPAddressInformation address)
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x0007E0EC File Offset: 0x0007C2EC
		[__DynamicallyInvokable]
		public virtual void Clear()
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x04001910 RID: 6416
		private Collection<UnicastIPAddressInformation> addresses = new Collection<UnicastIPAddressInformation>();
	}
}
