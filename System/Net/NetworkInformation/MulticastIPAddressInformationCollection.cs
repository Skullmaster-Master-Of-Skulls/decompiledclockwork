using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Net.NetworkInformation
{
	// Token: 0x020005E6 RID: 1510
	public class MulticastIPAddressInformationCollection : ICollection<MulticastIPAddressInformation>, IEnumerable<MulticastIPAddressInformation>, IEnumerable
	{
		// Token: 0x06002FA1 RID: 12193 RVA: 0x000CF12A File Offset: 0x000CE12A
		protected internal MulticastIPAddressInformationCollection()
		{
		}

		// Token: 0x06002FA2 RID: 12194 RVA: 0x000CF13D File Offset: 0x000CE13D
		public virtual void CopyTo(MulticastIPAddressInformation[] array, int offset)
		{
			this.addresses.CopyTo(array, offset);
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06002FA3 RID: 12195 RVA: 0x000CF14C File Offset: 0x000CE14C
		public virtual int Count
		{
			get
			{
				return this.addresses.Count;
			}
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06002FA4 RID: 12196 RVA: 0x000CF159 File Offset: 0x000CE159
		public virtual bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x000CF15C File Offset: 0x000CE15C
		public virtual void Add(MulticastIPAddressInformation address)
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x06002FA6 RID: 12198 RVA: 0x000CF16D File Offset: 0x000CE16D
		internal void InternalAdd(MulticastIPAddressInformation address)
		{
			this.addresses.Add(address);
		}

		// Token: 0x06002FA7 RID: 12199 RVA: 0x000CF17B File Offset: 0x000CE17B
		public virtual bool Contains(MulticastIPAddressInformation address)
		{
			return this.addresses.Contains(address);
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x000CF189 File Offset: 0x000CE189
		IEnumerator IEnumerable.GetEnumerator()
		{
			return null;
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x000CF18C File Offset: 0x000CE18C
		public virtual IEnumerator<MulticastIPAddressInformation> GetEnumerator()
		{
			return this.addresses.GetEnumerator();
		}

		// Token: 0x17000A5E RID: 2654
		public virtual MulticastIPAddressInformation this[int index]
		{
			get
			{
				return this.addresses[index];
			}
		}

		// Token: 0x06002FAB RID: 12203 RVA: 0x000CF1A7 File Offset: 0x000CE1A7
		public virtual bool Remove(MulticastIPAddressInformation address)
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x06002FAC RID: 12204 RVA: 0x000CF1B8 File Offset: 0x000CE1B8
		public virtual void Clear()
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x04002CC4 RID: 11460
		private Collection<MulticastIPAddressInformation> addresses = new Collection<MulticastIPAddressInformation>();
	}
}
