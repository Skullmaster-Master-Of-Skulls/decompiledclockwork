using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002A0 RID: 672
	[__DynamicallyInvokable]
	public class IPAddressInformationCollection : ICollection<IPAddressInformation>, IEnumerable<IPAddressInformation>, IEnumerable
	{
		// Token: 0x0600190D RID: 6413 RVA: 0x0007DF29 File Offset: 0x0007C129
		internal IPAddressInformationCollection()
		{
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x0007DF3C File Offset: 0x0007C13C
		[__DynamicallyInvokable]
		public virtual void CopyTo(IPAddressInformation[] array, int offset)
		{
			this.addresses.CopyTo(array, offset);
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x0600190F RID: 6415 RVA: 0x0007DF4B File Offset: 0x0007C14B
		[__DynamicallyInvokable]
		public virtual int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this.addresses.Count;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001910 RID: 6416 RVA: 0x0007DF58 File Offset: 0x0007C158
		[__DynamicallyInvokable]
		public virtual bool IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return true;
			}
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x0007DF5B File Offset: 0x0007C15B
		[__DynamicallyInvokable]
		public virtual void Add(IPAddressInformation address)
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x0007DF6C File Offset: 0x0007C16C
		internal void InternalAdd(IPAddressInformation address)
		{
			this.addresses.Add(address);
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x0007DF7A File Offset: 0x0007C17A
		[__DynamicallyInvokable]
		public virtual bool Contains(IPAddressInformation address)
		{
			return this.addresses.Contains(address);
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x0007DF88 File Offset: 0x0007C188
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x0007DF90 File Offset: 0x0007C190
		[__DynamicallyInvokable]
		public virtual IEnumerator<IPAddressInformation> GetEnumerator()
		{
			return this.addresses.GetEnumerator();
		}

		// Token: 0x1700055F RID: 1375
		[__DynamicallyInvokable]
		public virtual IPAddressInformation this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				return this.addresses[index];
			}
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x0007DFAB File Offset: 0x0007C1AB
		[__DynamicallyInvokable]
		public virtual bool Remove(IPAddressInformation address)
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x0007DFBC File Offset: 0x0007C1BC
		[__DynamicallyInvokable]
		public virtual void Clear()
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x040018D0 RID: 6352
		private Collection<IPAddressInformation> addresses = new Collection<IPAddressInformation>();
	}
}
