using System;
using System.Security;
using a.g;

namespace MailBee.DnsMX
{
	// Token: 0x02000579 RID: 1401
	[Serializable]
	public class DnsServerCollection : SortableByPriorityCollection
	{
		// Token: 0x170005C1 RID: 1473
		public DnsServer this[int index]
		{
			get
			{
				return (DnsServer)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06002E65 RID: 11877 RVA: 0x000DEA53 File Offset: 0x000DDA53
		public void Add(DnsServer server)
		{
			base.List.Add(server);
			base.SortByPriority();
		}

		// Token: 0x06002E66 RID: 11878 RVA: 0x000DEA68 File Offset: 0x000DDA68
		public DnsServer Add(string host)
		{
			DnsServer dnsServer = new DnsServer(host);
			base.List.Add(dnsServer);
			base.SortByPriority();
			return dnsServer;
		}

		// Token: 0x06002E67 RID: 11879 RVA: 0x000DEA90 File Offset: 0x000DDA90
		public DnsServer Add(string host, int priority)
		{
			DnsServer dnsServer = new DnsServer(host, priority);
			base.List.Add(dnsServer);
			base.SortByPriority();
			return dnsServer;
		}

		// Token: 0x06002E68 RID: 11880 RVA: 0x000DEAB9 File Offset: 0x000DDAB9
		public void Remove(DnsServer server)
		{
			base.List.Remove(server);
		}

		// Token: 0x06002E69 RID: 11881 RVA: 0x000DEAC7 File Offset: 0x000DDAC7
		public bool Autodetect()
		{
			return this.Autodetect(DnsAutodetectOptions.ConfigFiles | DnsAutodetectOptions.NetInterface | DnsAutodetectOptions.Registry | DnsAutodetectOptions.Wmi);
		}

		// Token: 0x06002E6A RID: 11882 RVA: 0x000DEAD4 File Offset: 0x000DDAD4
		public bool Autodetect(DnsAutodetectOptions options)
		{
			int num = 0;
			bool a_ = (options & DnsAutodetectOptions.AllowIPv6Servers) == DnsAutodetectOptions.None;
			base.Clear();
			if ((options & DnsAutodetectOptions.ConfigFiles) > DnsAutodetectOptions.None)
			{
				num = e.d(this, a_);
				if (Global.DnsServers.Count > 0)
				{
					foreach (object obj in Global.DnsServers)
					{
						DnsServer server = (DnsServer)obj;
						this.Add(server);
						num++;
					}
				}
			}
			if (num == 0 && (options & DnsAutodetectOptions.NetInterface) > DnsAutodetectOptions.None)
			{
				try
				{
					num = e.a(this, a_);
				}
				catch (SecurityException)
				{
				}
			}
			if (num == 0 && (options & DnsAutodetectOptions.Wmi) > DnsAutodetectOptions.None)
			{
				try
				{
					num = e.b(this, a_);
				}
				catch (SecurityException)
				{
				}
			}
			if (num == 0 && (options & DnsAutodetectOptions.Registry) > DnsAutodetectOptions.None)
			{
				num = e.c(this, a_);
			}
			if (num == 0 && (options & DnsAutodetectOptions.RootServers) > DnsAutodetectOptions.None)
			{
				num = e.a(this);
			}
			return num > 0;
		}
	}
}
