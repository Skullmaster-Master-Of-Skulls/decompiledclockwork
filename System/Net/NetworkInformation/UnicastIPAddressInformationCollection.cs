using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Net.NetworkInformation
{
	// Token: 0x020005E4 RID: 1508
	public class UnicastIPAddressInformationCollection : ICollection<UnicastIPAddressInformation>, IEnumerable<UnicastIPAddressInformation>, IEnumerable
	{
		// Token: 0x06002F8E RID: 12174 RVA: 0x000CF07E File Offset: 0x000CE07E
		protected internal UnicastIPAddressInformationCollection()
		{
		}

		// Token: 0x06002F8F RID: 12175 RVA: 0x000CF091 File Offset: 0x000CE091
		public virtual void CopyTo(UnicastIPAddressInformation[] array, int offset)
		{
			this.addresses.CopyTo(array, offset);
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06002F90 RID: 12176 RVA: 0x000CF0A0 File Offset: 0x000CE0A0
		public virtual int Count
		{
			get
			{
				return this.addresses.Count;
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06002F91 RID: 12177 RVA: 0x000CF0AD File Offset: 0x000CE0AD
		public virtual bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002F92 RID: 12178 RVA: 0x000CF0B0 File Offset: 0x000CE0B0
		public virtual void Add(UnicastIPAddressInformation address)
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x06002F93 RID: 12179 RVA: 0x000CF0C1 File Offset: 0x000CE0C1
		internal void InternalAdd(UnicastIPAddressInformation address)
		{
			this.addresses.Add(address);
		}

		// Token: 0x06002F94 RID: 12180 RVA: 0x000CF0CF File Offset: 0x000CE0CF
		public virtual bool Contains(UnicastIPAddressInformation address)
		{
			return this.addresses.Contains(address);
		}

		// Token: 0x06002F95 RID: 12181 RVA: 0x000CF0DD File Offset: 0x000CE0DD
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002F96 RID: 12182 RVA: 0x000CF0E5 File Offset: 0x000CE0E5
		public virtual IEnumerator<UnicastIPAddressInformation> GetEnumerator()
		{
			return this.addresses.GetEnumerator();
		}

		// Token: 0x17000A55 RID: 2645
		public virtual UnicastIPAddressInformation this[int index]
		{
			get
			{
				return this.addresses[index];
			}
		}

		// Token: 0x06002F98 RID: 12184 RVA: 0x000CF100 File Offset: 0x000CE100
		public virtual bool Remove(UnicastIPAddressInformation address)
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x06002F99 RID: 12185 RVA: 0x000CF111 File Offset: 0x000CE111
		public virtual void Clear()
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x04002CC3 RID: 11459
		private Collection<UnicastIPAddressInformation> addresses = new Collection<UnicastIPAddressInformation>();
	}
}
