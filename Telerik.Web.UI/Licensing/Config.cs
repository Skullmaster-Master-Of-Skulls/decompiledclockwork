using System;
using System.Net;

namespace Telerik.Licensing
{
	// Token: 0x02000433 RID: 1075
	internal class Config
	{
		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x06002695 RID: 9877 RVA: 0x0007E2D1 File Offset: 0x0007C4D1
		// (set) Token: 0x06002696 RID: 9878 RVA: 0x0007E2D9 File Offset: 0x0007C4D9
		public Uri TokenEndpoint { get; set; }

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x06002697 RID: 9879 RVA: 0x0007E2E2 File Offset: 0x0007C4E2
		// (set) Token: 0x06002698 RID: 9880 RVA: 0x0007E2EA File Offset: 0x0007C4EA
		public string ClientId { get; set; }

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x06002699 RID: 9881 RVA: 0x0007E2F3 File Offset: 0x0007C4F3
		// (set) Token: 0x0600269A RID: 9882 RVA: 0x0007E2FB File Offset: 0x0007C4FB
		public string ClientSecret { get; set; }

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x0600269B RID: 9883 RVA: 0x0007E304 File Offset: 0x0007C504
		// (set) Token: 0x0600269C RID: 9884 RVA: 0x0007E30C File Offset: 0x0007C50C
		public Uri MetricsEndpoint { get; set; }

		// Token: 0x0600269D RID: 9885 RVA: 0x0007E315 File Offset: 0x0007C515
		public static Config GetInstance()
		{
			Config.EnsureInitialized();
			return Config.config;
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x0007E324 File Offset: 0x0007C524
		private static void EnsureInitialized()
		{
			if (Config.config == null)
			{
				lock (Config.configLock)
				{
					if (Config.config == null)
					{
						Config.config = new Config
						{
							TokenEndpoint = new Uri("https://identity.telerik.com/v2/oauth/telerik/token"),
							ClientId = "uri:client.licenser",
							ClientSecret = "597f2d644c3ad29c2058fe08e477eeb5",
							MetricsEndpoint = new Uri("https://dle.telerik.com/metrics/v1/events/callhome")
						};
					}
					ServicePoint servicePoint = ServicePointManager.FindServicePoint(Config.config.TokenEndpoint);
					ServicePoint servicePoint2 = ServicePointManager.FindServicePoint(Config.config.MetricsEndpoint);
					servicePoint.UseNagleAlgorithm = false;
					servicePoint.Expect100Continue = false;
					servicePoint2.UseNagleAlgorithm = false;
					servicePoint2.Expect100Continue = false;
				}
			}
		}

		// Token: 0x040009DD RID: 2525
		private static readonly object configLock = new object();

		// Token: 0x040009DE RID: 2526
		private static Config config;

		// Token: 0x02000434 RID: 1076
		internal struct Consts
		{
			// Token: 0x040009E3 RID: 2531
			public const string TokenEndpoint = "https://identity.telerik.com/v2/oauth/telerik/token";

			// Token: 0x040009E4 RID: 2532
			public const string ClientId = "uri:client.licenser";

			// Token: 0x040009E5 RID: 2533
			public const string ClientSecret = "597f2d644c3ad29c2058fe08e477eeb5";

			// Token: 0x040009E6 RID: 2534
			public const string MetricsEndpoint = "https://dle.telerik.com/metrics/v1/events/callhome";
		}
	}
}
