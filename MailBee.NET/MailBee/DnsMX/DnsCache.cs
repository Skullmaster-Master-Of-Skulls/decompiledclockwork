using System;
using a.g;

namespace MailBee.DnsMX
{
	// Token: 0x0200056E RID: 1390
	public class DnsCache
	{
		// Token: 0x06002E25 RID: 11813 RVA: 0x000DE51A File Offset: 0x000DD51A
		private DnsCache()
		{
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06002E26 RID: 11814 RVA: 0x000DE522 File Offset: 0x000DD522
		// (set) Token: 0x06002E27 RID: 11815 RVA: 0x000DE529 File Offset: 0x000DD529
		public static int Timeout
		{
			get
			{
				return DnsCache.a;
			}
			set
			{
				if (value < 1)
				{
					throw new MailBeeInvalidArgumentException(23);
				}
				DnsCache.a = value;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06002E28 RID: 11816 RVA: 0x000DE53D File Offset: 0x000DD53D
		// (set) Token: 0x06002E29 RID: 11817 RVA: 0x000DE544 File Offset: 0x000DD544
		public static bool Enabled
		{
			get
			{
				return DnsCache.b;
			}
			set
			{
				DnsCache.b = value;
			}
		}

		// Token: 0x06002E2A RID: 11818 RVA: 0x000DE54C File Offset: 0x000DD54C
		public static void Clear()
		{
			object obj = DnsCache.c.a();
			lock (obj)
			{
				DnsCache.c.Clear();
			}
			obj = DnsCache.d.a();
			lock (obj)
			{
				DnsCache.d.Clear();
			}
			obj = DnsCache.e.a();
			lock (obj)
			{
				DnsCache.e.Clear();
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06002E2B RID: 11819 RVA: 0x000DE604 File Offset: 0x000DD604
		public static int Count
		{
			get
			{
				return DnsCache.c.Count + DnsCache.d.Count + DnsCache.e.Count;
			}
		}

		// Token: 0x06002E2C RID: 11820 RVA: 0x000DE626 File Offset: 0x000DD626
		internal static j a(h A_0)
		{
			if (A_0 == h.l)
			{
				return DnsCache.e;
			}
			if (A_0 != h.p)
			{
				return DnsCache.c;
			}
			return DnsCache.d;
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06002E2D RID: 11821 RVA: 0x000DE645 File Offset: 0x000DD645
		// (set) Token: 0x06002E2E RID: 11822 RVA: 0x000DE64C File Offset: 0x000DD64C
		public static int SmtpMXMaxFailureCount
		{
			get
			{
				return DnsCache.f;
			}
			set
			{
				if (value < 1)
				{
					throw new MailBeeInvalidArgumentException(23);
				}
				DnsCache.f = value;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06002E2F RID: 11823 RVA: 0x000DE660 File Offset: 0x000DD660
		// (set) Token: 0x06002E30 RID: 11824 RVA: 0x000DE667 File Offset: 0x000DD667
		public static int SmtpMXNextAttemptInterval
		{
			get
			{
				return DnsCache.g;
			}
			set
			{
				if (value < 0)
				{
					throw new MailBeeInvalidArgumentException(23);
				}
				DnsCache.g = value;
			}
		}

		// Token: 0x04001FC4 RID: 8132
		private static int a = 90;

		// Token: 0x04001FC5 RID: 8133
		private static bool b = true;

		// Token: 0x04001FC6 RID: 8134
		private static j c = new j();

		// Token: 0x04001FC7 RID: 8135
		private static j d = new j();

		// Token: 0x04001FC8 RID: 8136
		private static j e = new j();

		// Token: 0x04001FC9 RID: 8137
		private static int f = 1;

		// Token: 0x04001FCA RID: 8138
		private static int g = 1000;
	}
}
