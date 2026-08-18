using System;
using System.Collections.Generic;
using System.Security;
using System.ServiceModel.Configuration;

namespace System.ServiceModel.Administration
{
	// Token: 0x0200044A RID: 1098
	internal sealed class ManagementExtension
	{
		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06002ABE RID: 10942 RVA: 0x000A6F7D File Offset: 0x000A517D
		internal static bool IsActivated
		{
			get
			{
				return ManagementExtension.activated;
			}
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06002ABF RID: 10943 RVA: 0x000A6F84 File Offset: 0x000A5184
		internal static bool IsEnabled
		{
			get
			{
				return ManagementExtension.isEnabled;
			}
		}

		// Token: 0x06002AC0 RID: 10944 RVA: 0x000A6F8B File Offset: 0x000A518B
		[SecuritySafeCritical]
		private static bool GetIsWmiProviderEnabled()
		{
			return DiagnosticSection.UnsafeGetSection().WmiProviderEnabled;
		}

		// Token: 0x06002AC1 RID: 10945 RVA: 0x000A6F98 File Offset: 0x000A5198
		private static Dictionary<ServiceHostBase, DateTime> GetServices()
		{
			if (ManagementExtension.services == null)
			{
				object obj = ManagementExtension.syncRoot;
				lock (obj)
				{
					if (ManagementExtension.services == null)
					{
						ManagementExtension.services = new Dictionary<ServiceHostBase, DateTime>();
					}
				}
			}
			return ManagementExtension.services;
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06002AC2 RID: 10946 RVA: 0x000A6FF0 File Offset: 0x000A51F0
		internal static ICollection<ServiceHostBase> Services
		{
			get
			{
				return ManagementExtension.GetServices().Keys;
			}
		}

		// Token: 0x06002AC3 RID: 10947 RVA: 0x000A6FFC File Offset: 0x000A51FC
		internal static DateTime GetTimeOpened(ServiceHostBase service)
		{
			return ManagementExtension.GetServices()[service];
		}

		// Token: 0x06002AC4 RID: 10948 RVA: 0x000A7009 File Offset: 0x000A5209
		public static void OnServiceOpened(ServiceHostBase serviceHostBase)
		{
			ManagementExtension.EnsureManagementProvider();
			ManagementExtension.Add(serviceHostBase);
		}

		// Token: 0x06002AC5 RID: 10949 RVA: 0x000A7016 File Offset: 0x000A5216
		public static void OnServiceClosing(ServiceHostBase serviceHostBase)
		{
			ManagementExtension.Remove(serviceHostBase);
		}

		// Token: 0x06002AC6 RID: 10950 RVA: 0x000A7020 File Offset: 0x000A5220
		private static void Add(ServiceHostBase service)
		{
			Dictionary<ServiceHostBase, DateTime> dictionary = ManagementExtension.GetServices();
			Dictionary<ServiceHostBase, DateTime> obj = dictionary;
			lock (obj)
			{
				if (!dictionary.ContainsKey(service))
				{
					dictionary.Add(service, DateTime.Now);
				}
			}
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x000A7070 File Offset: 0x000A5270
		private static void Remove(ServiceHostBase service)
		{
			Dictionary<ServiceHostBase, DateTime> dictionary = ManagementExtension.GetServices();
			Dictionary<ServiceHostBase, DateTime> obj = dictionary;
			lock (obj)
			{
				if (dictionary.ContainsKey(service))
				{
					dictionary.Remove(service);
				}
			}
		}

		// Token: 0x06002AC8 RID: 10952 RVA: 0x000A70BC File Offset: 0x000A52BC
		private static void EnsureManagementProvider()
		{
			if (!ManagementExtension.activated)
			{
				object obj = ManagementExtension.syncRoot;
				lock (obj)
				{
					if (!ManagementExtension.activated)
					{
						ManagementExtension.Activate();
						ManagementExtension.activated = true;
					}
				}
			}
		}

		// Token: 0x06002AC9 RID: 10953 RVA: 0x000A7110 File Offset: 0x000A5310
		private static void Activate()
		{
			WbemProvider wbemProvider = new WbemProvider("root\\ServiceModel", "ServiceModel");
			wbemProvider.Register("AppDomainInfo", new AppDomainInstanceProvider());
			wbemProvider.Register("Service", new ServiceInstanceProvider());
			wbemProvider.Register("Contract", new ContractInstanceProvider());
			wbemProvider.Register("Endpoint", new EndpointInstanceProvider());
			wbemProvider.Register("ServiceAppDomain", new ServiceAppDomainAssociationProvider());
			wbemProvider.Register("ServiceToEndpointAssociation", new ServiceEndpointAssociationProvider());
		}

		// Token: 0x040023FE RID: 9214
		private static Dictionary<ServiceHostBase, DateTime> services;

		// Token: 0x040023FF RID: 9215
		private static bool activated = false;

		// Token: 0x04002400 RID: 9216
		private static object syncRoot = new object();

		// Token: 0x04002401 RID: 9217
		private static bool isEnabled = ManagementExtension.GetIsWmiProviderEnabled();
	}
}
