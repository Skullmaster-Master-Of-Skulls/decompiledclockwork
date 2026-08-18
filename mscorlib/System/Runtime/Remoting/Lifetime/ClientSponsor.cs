using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x020006BE RID: 1726
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public class ClientSponsor : MarshalByRefObject, ISponsor
	{
		// Token: 0x06003E15 RID: 15893 RVA: 0x000D43A4 File Offset: 0x000D33A4
		public ClientSponsor()
		{
		}

		// Token: 0x06003E16 RID: 15894 RVA: 0x000D43CD File Offset: 0x000D33CD
		public ClientSponsor(TimeSpan renewalTime)
		{
			this.m_renewalTime = renewalTime;
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06003E17 RID: 15895 RVA: 0x000D43FD File Offset: 0x000D33FD
		// (set) Token: 0x06003E18 RID: 15896 RVA: 0x000D4405 File Offset: 0x000D3405
		public TimeSpan RenewalTime
		{
			get
			{
				return this.m_renewalTime;
			}
			set
			{
				this.m_renewalTime = value;
			}
		}

		// Token: 0x06003E19 RID: 15897 RVA: 0x000D4410 File Offset: 0x000D3410
		public bool Register(MarshalByRefObject obj)
		{
			ILease lease = (ILease)obj.GetLifetimeService();
			if (lease == null)
			{
				return false;
			}
			lease.Register(this);
			lock (this.sponsorTable)
			{
				this.sponsorTable[obj] = lease;
			}
			return true;
		}

		// Token: 0x06003E1A RID: 15898 RVA: 0x000D446C File Offset: 0x000D346C
		public void Unregister(MarshalByRefObject obj)
		{
			ILease lease = null;
			lock (this.sponsorTable)
			{
				lease = (ILease)this.sponsorTable[obj];
			}
			if (lease != null)
			{
				lease.Unregister(this);
			}
		}

		// Token: 0x06003E1B RID: 15899 RVA: 0x000D44C0 File Offset: 0x000D34C0
		public TimeSpan Renewal(ILease lease)
		{
			return this.m_renewalTime;
		}

		// Token: 0x06003E1C RID: 15900 RVA: 0x000D44C8 File Offset: 0x000D34C8
		public void Close()
		{
			lock (this.sponsorTable)
			{
				IDictionaryEnumerator enumerator = this.sponsorTable.GetEnumerator();
				while (enumerator.MoveNext())
				{
					((ILease)enumerator.Value).Unregister(this);
				}
				this.sponsorTable.Clear();
			}
		}

		// Token: 0x06003E1D RID: 15901 RVA: 0x000D4530 File Offset: 0x000D3530
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x06003E1E RID: 15902 RVA: 0x000D4534 File Offset: 0x000D3534
		~ClientSponsor()
		{
		}

		// Token: 0x04001FA8 RID: 8104
		private Hashtable sponsorTable = new Hashtable(10);

		// Token: 0x04001FA9 RID: 8105
		private TimeSpan m_renewalTime = TimeSpan.FromMinutes(2.0);
	}
}
