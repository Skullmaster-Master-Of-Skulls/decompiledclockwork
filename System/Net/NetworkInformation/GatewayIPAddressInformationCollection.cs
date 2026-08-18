using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Net.NetworkInformation
{
	// Token: 0x020005EA RID: 1514
	public class GatewayIPAddressInformationCollection : ICollection<GatewayIPAddressInformation>, IEnumerable<GatewayIPAddressInformation>, IEnumerable
	{
		// Token: 0x06002FBD RID: 12221 RVA: 0x000CF287 File Offset: 0x000CE287
		protected internal GatewayIPAddressInformationCollection()
		{
		}

		// Token: 0x06002FBE RID: 12222 RVA: 0x000CF29A File Offset: 0x000CE29A
		public virtual void CopyTo(GatewayIPAddressInformation[] array, int offset)
		{
			this.addresses.CopyTo(array, offset);
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06002FBF RID: 12223 RVA: 0x000CF2A9 File Offset: 0x000CE2A9
		public virtual int Count
		{
			get
			{
				return this.addresses.Count;
			}
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06002FC0 RID: 12224 RVA: 0x000CF2B6 File Offset: 0x000CE2B6
		public virtual bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000A66 RID: 2662
		public virtual GatewayIPAddressInformation this[int index]
		{
			get
			{
				return this.addresses[index];
			}
		}

		// Token: 0x06002FC2 RID: 12226 RVA: 0x000CF2C7 File Offset: 0x000CE2C7
		public virtual void Add(GatewayIPAddressInformation address)
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x06002FC3 RID: 12227 RVA: 0x000CF2D8 File Offset: 0x000CE2D8
		internal void InternalAdd(GatewayIPAddressInformation address)
		{
			this.addresses.Add(address);
		}

		// Token: 0x06002FC4 RID: 12228 RVA: 0x000CF2E6 File Offset: 0x000CE2E6
		public virtual bool Contains(GatewayIPAddressInformation address)
		{
			return this.addresses.Contains(address);
		}

		// Token: 0x06002FC5 RID: 12229 RVA: 0x000CF2F4 File Offset: 0x000CE2F4
		public virtual IEnumerator<GatewayIPAddressInformation> GetEnumerator()
		{
			return this.addresses.GetEnumerator();
		}

		// Token: 0x06002FC6 RID: 12230 RVA: 0x000CF301 File Offset: 0x000CE301
		IEnumerator IEnumerable.GetEnumerator()
		{
			return null;
		}

		// Token: 0x06002FC7 RID: 12231 RVA: 0x000CF304 File Offset: 0x000CE304
		public virtual bool Remove(GatewayIPAddressInformation address)
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x000CF315 File Offset: 0x000CE315
		public virtual void Clear()
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x04002CC7 RID: 11463
		private Collection<GatewayIPAddressInformation> addresses = new Collection<GatewayIPAddressInformation>();
	}
}
