using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Net.NetworkInformation
{
	// Token: 0x020005DB RID: 1499
	public class IPAddressInformationCollection : ICollection<IPAddressInformation>, IEnumerable<IPAddressInformation>, IEnumerable
	{
		// Token: 0x06002F36 RID: 12086 RVA: 0x000CEF9E File Offset: 0x000CDF9E
		internal IPAddressInformationCollection()
		{
		}

		// Token: 0x06002F37 RID: 12087 RVA: 0x000CEFB1 File Offset: 0x000CDFB1
		public virtual void CopyTo(IPAddressInformation[] array, int offset)
		{
			this.addresses.CopyTo(array, offset);
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06002F38 RID: 12088 RVA: 0x000CEFC0 File Offset: 0x000CDFC0
		public virtual int Count
		{
			get
			{
				return this.addresses.Count;
			}
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06002F39 RID: 12089 RVA: 0x000CEFCD File Offset: 0x000CDFCD
		public virtual bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002F3A RID: 12090 RVA: 0x000CEFD0 File Offset: 0x000CDFD0
		public virtual void Add(IPAddressInformation address)
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x06002F3B RID: 12091 RVA: 0x000CEFE1 File Offset: 0x000CDFE1
		internal void InternalAdd(IPAddressInformation address)
		{
			this.addresses.Add(address);
		}

		// Token: 0x06002F3C RID: 12092 RVA: 0x000CEFEF File Offset: 0x000CDFEF
		public virtual bool Contains(IPAddressInformation address)
		{
			return this.addresses.Contains(address);
		}

		// Token: 0x06002F3D RID: 12093 RVA: 0x000CEFFD File Offset: 0x000CDFFD
		IEnumerator IEnumerable.GetEnumerator()
		{
			return null;
		}

		// Token: 0x06002F3E RID: 12094 RVA: 0x000CF000 File Offset: 0x000CE000
		public virtual IEnumerator<IPAddressInformation> GetEnumerator()
		{
			return this.addresses.GetEnumerator();
		}

		// Token: 0x17000A1A RID: 2586
		public virtual IPAddressInformation this[int index]
		{
			get
			{
				return this.addresses[index];
			}
		}

		// Token: 0x06002F40 RID: 12096 RVA: 0x000CF01B File Offset: 0x000CE01B
		public virtual bool Remove(IPAddressInformation address)
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x06002F41 RID: 12097 RVA: 0x000CF02C File Offset: 0x000CE02C
		public virtual void Clear()
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x04002C8C RID: 11404
		private Collection<IPAddressInformation> addresses = new Collection<IPAddressInformation>();
	}
}
