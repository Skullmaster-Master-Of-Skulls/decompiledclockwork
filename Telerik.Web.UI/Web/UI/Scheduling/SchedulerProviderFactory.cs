using System;
using System.Web.Configuration;

namespace Telerik.Web.UI.Scheduling
{
	// Token: 0x020012DE RID: 4830
	internal static class SchedulerProviderFactory
	{
		// Token: 0x0600CAC3 RID: 51907 RVA: 0x002D45BF File Offset: 0x002D27BF
		public static SchedulerProviderBase GetProvider(RadScheduler owner, string name)
		{
			if (name == "Integrated")
			{
				return new DataSourceViewSchedulerProvider(owner);
			}
			return SchedulerProviderFactory.GetProvider(name);
		}

		// Token: 0x0600CAC4 RID: 51908 RVA: 0x002D45DC File Offset: 0x002D27DC
		public static SchedulerProviderBase GetProvider(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("Name cannot be null or empty.", "name");
			}
			SchedulerProviderFactory.LoadProviders();
			SchedulerProviderBase result;
			lock (SchedulerProviderFactory._lock)
			{
				SchedulerProviderBase schedulerProviderBase = SchedulerProviderFactory._appointmentProviders[name];
				if (schedulerProviderBase == null)
				{
					throw new ArgumentException("Provider '" + name + "' has not been declared in web.config.");
				}
				result = schedulerProviderBase.Synchronized();
			}
			return result;
		}

		// Token: 0x0600CAC5 RID: 51909 RVA: 0x002D4660 File Offset: 0x002D2860
		private static void LoadProviders()
		{
			if (SchedulerProviderFactory._appointmentProviders == null)
			{
				lock (SchedulerProviderFactory._lock)
				{
					if (SchedulerProviderFactory._appointmentProviders == null)
					{
						SchedulerProviderFactory._appointmentProviders = new SchedulerProviderCollection();
						RadSchedulerConfigurationSection radSchedulerConfigurationSection = (RadSchedulerConfigurationSection)WebConfigurationManager.GetSection("telerik.web.ui/radScheduler");
						if (radSchedulerConfigurationSection != null)
						{
							ProvidersHelper.InstantiateProviders(radSchedulerConfigurationSection.AppointmentProviders, SchedulerProviderFactory._appointmentProviders, typeof(SchedulerProviderBase));
						}
					}
				}
			}
		}

		// Token: 0x0400353A RID: 13626
		private static SchedulerProviderCollection _appointmentProviders;

		// Token: 0x0400353B RID: 13627
		private static readonly object _lock = new object();
	}
}
