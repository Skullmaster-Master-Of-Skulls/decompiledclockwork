using System;
using System.Collections;
using System.Security.Permissions;
using System.Web.Configuration;

namespace System.Web.Management
{
	// Token: 0x020001A1 RID: 417
	public static class WebEventManager
	{
		// Token: 0x060015F7 RID: 5623 RVA: 0x00043AEC File Offset: 0x00041CEC
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public static void Flush(string providerName)
		{
			HealthMonitoringSectionHelper.ProviderInstances providerInstances = HealthMonitoringManager.ProviderInstances;
			if (providerInstances == null)
			{
				return;
			}
			if (!providerInstances.ContainsKey(providerName))
			{
				throw new ArgumentException(SR.GetString("Health_mon_provider_not_found", new object[]
				{
					providerName
				}));
			}
			using (new ApplicationImpersonationContext())
			{
				providerInstances[providerName].Flush();
			}
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x00043B54 File Offset: 0x00041D54
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public static void Flush()
		{
			HealthMonitoringSectionHelper.ProviderInstances providerInstances = HealthMonitoringManager.ProviderInstances;
			if (providerInstances == null)
			{
				return;
			}
			using (new ApplicationImpersonationContext())
			{
				foreach (object obj in providerInstances)
				{
					WebEventProvider webEventProvider = (WebEventProvider)((DictionaryEntry)obj).Value;
					webEventProvider.Flush();
				}
			}
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x00043BE0 File Offset: 0x00041DE0
		internal static void Shutdown()
		{
			HealthMonitoringSectionHelper.ProviderInstances providerInstances = HealthMonitoringManager.ProviderInstances;
			if (providerInstances == null)
			{
				return;
			}
			foreach (object obj in providerInstances)
			{
				WebEventProvider webEventProvider = (WebEventProvider)((DictionaryEntry)obj).Value;
				webEventProvider.Shutdown();
			}
		}
	}
}
