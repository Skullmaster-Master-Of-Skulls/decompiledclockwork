using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Net.NetworkInformation
{
	// Token: 0x020005E7 RID: 1511
	public class IPAddressCollection : ICollection<IPAddress>, IEnumerable<IPAddress>, IEnumerable
	{
		// Token: 0x06002FAD RID: 12205 RVA: 0x000CF1C9 File Offset: 0x000CE1C9
		protected internal IPAddressCollection()
		{
		}

		// Token: 0x06002FAE RID: 12206 RVA: 0x000CF1DC File Offset: 0x000CE1DC
		public virtual void CopyTo(IPAddress[] array, int offset)
		{
			this.addresses.CopyTo(array, offset);
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06002FAF RID: 12207 RVA: 0x000CF1EB File Offset: 0x000CE1EB
		public virtual int Count
		{
			get
			{
				return this.addresses.Count;
			}
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06002FB0 RID: 12208 RVA: 0x000CF1F8 File Offset: 0x000CE1F8
		public virtual bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002FB1 RID: 12209 RVA: 0x000CF1FB File Offset: 0x000CE1FB
		public virtual void Add(IPAddress address)
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x06002FB2 RID: 12210 RVA: 0x000CF20C File Offset: 0x000CE20C
		internal void InternalAdd(IPAddress address)
		{
			this.addresses.Add(address);
		}

		// Token: 0x06002FB3 RID: 12211 RVA: 0x000CF21A File Offset: 0x000CE21A
		public virtual bool Contains(IPAddress address)
		{
			return this.addresses.Contains(address);
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x000CF228 File Offset: 0x000CE228
		IEnumerator IEnumerable.GetEnumerator()
		{
			return null;
		}

		// Token: 0x06002FB5 RID: 12213 RVA: 0x000CF22B File Offset: 0x000CE22B
		public virtual IEnumerator<IPAddress> GetEnumerator()
		{
			return this.addresses.GetEnumerator();
		}

		// Token: 0x17000A61 RID: 2657
		public virtual IPAddress this[int index]
		{
			get
			{
				return this.addresses[index];
			}
		}

		// Token: 0x06002FB7 RID: 12215 RVA: 0x000CF246 File Offset: 0x000CE246
		public virtual bool Remove(IPAddress address)
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x000CF257 File Offset: 0x000CE257
		public virtual void Clear()
		{
			throw new NotSupportedException(SR.GetString("net_collection_readonly"));
		}

		// Token: 0x04002CC5 RID: 11461
		private Collection<IPAddress> addresses = new Collection<IPAddress>();
	}
}
