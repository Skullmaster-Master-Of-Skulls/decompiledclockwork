using System;
using System.Net;
using a;

namespace MailBee.DnsMX
{
	// Token: 0x02000578 RID: 1400
	[Serializable]
	public class DnsServer : ax
	{
		// Token: 0x06002E4E RID: 11854 RVA: 0x000DE814 File Offset: 0x000DD814
		public DnsServer() : this("127.0.0.1")
		{
		}

		// Token: 0x06002E4F RID: 11855 RVA: 0x000DE821 File Offset: 0x000DD821
		public DnsServer(string host) : this(host, 0)
		{
		}

		// Token: 0x06002E50 RID: 11856 RVA: 0x000DE82C File Offset: 0x000DD82C
		public DnsServer(string host, int priority)
		{
			this.Host = host;
			this.m_failureCount = 0;
			this.m_putAside = false;
			this.m_priority = priority;
			this.m_udpRetryCount = 2;
			this.m_tryTcp = true;
			this.m_udpTimeout = 5000;
			this.m_tcpTimeout = 5000;
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06002E51 RID: 11857 RVA: 0x000DE87F File Offset: 0x000DD87F
		// (set) Token: 0x06002E52 RID: 11858 RVA: 0x000DE88C File Offset: 0x000DD88C
		public string Host
		{
			get
			{
				return this.m_ip.ToString();
			}
			set
			{
				try
				{
					this.m_ip = IPAddress.Parse(value);
				}
				catch (Exception)
				{
					throw new MailBeeInvalidArgumentException(20);
				}
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06002E53 RID: 11859 RVA: 0x000DE8C0 File Offset: 0x000DD8C0
		public IPAddress IP
		{
			get
			{
				return this.m_ip;
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06002E54 RID: 11860 RVA: 0x000DE8C8 File Offset: 0x000DD8C8
		public int FailureCount
		{
			get
			{
				return this.m_failureCount;
			}
		}

		// Token: 0x06002E55 RID: 11861 RVA: 0x000DE8D0 File Offset: 0x000DD8D0
		internal bool b()
		{
			return this.m_failureCount >= Global.DnsMaxFailureCount;
		}

		// Token: 0x06002E56 RID: 11862 RVA: 0x000DE8E2 File Offset: 0x000DD8E2
		internal bool c()
		{
			if (!this.m_putAside)
			{
				return false;
			}
			if (DateTime.Now < this.m_lastFailure.AddMilliseconds((double)Global.DnsNextAttemptInterval))
			{
				return true;
			}
			this.m_putAside = false;
			return true;
		}

		// Token: 0x06002E57 RID: 11863 RVA: 0x000DE915 File Offset: 0x000DD915
		public void Reset()
		{
			this.m_failureCount = 0;
		}

		// Token: 0x06002E58 RID: 11864 RVA: 0x000DE920 File Offset: 0x000DD920
		internal void a()
		{
			lock (this)
			{
				if (this.m_putAside)
				{
					return;
				}
				this.m_failureCount++;
				if (this.m_failureCount < Global.DnsMaxFailureCount)
				{
					this.m_putAside = true;
				}
			}
			this.m_lastFailure = DateTime.Now;
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06002E59 RID: 11865 RVA: 0x000DE98C File Offset: 0x000DD98C
		// (set) Token: 0x06002E5A RID: 11866 RVA: 0x000DE994 File Offset: 0x000DD994
		public int Priority
		{
			get
			{
				return this.m_priority;
			}
			set
			{
				this.m_priority = value;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06002E5B RID: 11867 RVA: 0x000DE99D File Offset: 0x000DD99D
		// (set) Token: 0x06002E5C RID: 11868 RVA: 0x000DE9A5 File Offset: 0x000DD9A5
		public int UdpRetryCount
		{
			get
			{
				return this.m_udpRetryCount;
			}
			set
			{
				if (value < 0 || value > 10)
				{
					throw new MailBeeInvalidArgumentException(23);
				}
				if (!this.m_tryTcp && value == 0)
				{
					throw new MailBeeInvalidStateException(20);
				}
				this.m_udpRetryCount = value;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06002E5D RID: 11869 RVA: 0x000DE9D2 File Offset: 0x000DD9D2
		// (set) Token: 0x06002E5E RID: 11870 RVA: 0x000DE9DA File Offset: 0x000DD9DA
		public int UdpTimeout
		{
			get
			{
				return this.m_udpTimeout;
			}
			set
			{
				if (value < 0)
				{
					throw new MailBeeInvalidArgumentException(23);
				}
				this.m_udpTimeout = value;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06002E5F RID: 11871 RVA: 0x000DE9EF File Offset: 0x000DD9EF
		// (set) Token: 0x06002E60 RID: 11872 RVA: 0x000DE9F7 File Offset: 0x000DD9F7
		public int TcpTimeout
		{
			get
			{
				return this.m_tcpTimeout;
			}
			set
			{
				if (value < 0)
				{
					throw new MailBeeInvalidArgumentException(23);
				}
				this.m_udpTimeout = value;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06002E61 RID: 11873 RVA: 0x000DEA0C File Offset: 0x000DDA0C
		// (set) Token: 0x06002E62 RID: 11874 RVA: 0x000DEA14 File Offset: 0x000DDA14
		public bool TryTcp
		{
			get
			{
				return this.m_tryTcp;
			}
			set
			{
				if (!value && this.m_udpRetryCount < 1)
				{
					throw new MailBeeInvalidStateException(20);
				}
				this.m_tryTcp = value;
			}
		}

		// Token: 0x04001FDA RID: 8154
		private IPAddress m_ip;

		// Token: 0x04001FDB RID: 8155
		private int m_failureCount;

		// Token: 0x04001FDC RID: 8156
		private int m_priority;

		// Token: 0x04001FDD RID: 8157
		private int m_udpRetryCount;

		// Token: 0x04001FDE RID: 8158
		private int m_udpTimeout;

		// Token: 0x04001FDF RID: 8159
		private int m_tcpTimeout;

		// Token: 0x04001FE0 RID: 8160
		private bool m_tryTcp;

		// Token: 0x04001FE1 RID: 8161
		private DateTime m_lastFailure;

		// Token: 0x04001FE2 RID: 8162
		private bool m_putAside;
	}
}
