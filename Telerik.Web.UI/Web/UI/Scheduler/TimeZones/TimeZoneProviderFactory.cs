using System;
using System.Collections.Specialized;

namespace Telerik.Web.UI.Scheduler.TimeZones
{
	// Token: 0x02000E72 RID: 3698
	internal static class TimeZoneProviderFactory
	{
		// Token: 0x06008C42 RID: 35906 RVA: 0x001FD41C File Offset: 0x001FB61C
		public static TimeZoneProviderBase GetProvider(RadScheduler owner, string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("Name cannot be null or empty.", "name");
			}
			TimeZoneProviderFactory.LoadProviders(new NameValueCollection
			{
				{
					"timeZoneId",
					owner.TimeZoneID
				}
			});
			return TimeZoneProviderFactory._timeZoneProviders[name];
		}

		// Token: 0x06008C43 RID: 35907 RVA: 0x001FD46C File Offset: 0x001FB66C
		private static void LoadProviders(NameValueCollection settings)
		{
			lock (TimeZoneProviderFactory._lock)
			{
				TimeZoneProviderFactory._timeZoneProviders = new TimeZoneProviderCollection();
				foreach (Type type in TimeZoneProviderFactory._providers)
				{
					TimeZoneProviderBase timeZoneProviderBase = Activator.CreateInstance(type) as TimeZoneProviderBase;
					if (timeZoneProviderBase != null)
					{
						timeZoneProviderBase.Initialize(type.Name, settings);
						TimeZoneProviderFactory._timeZoneProviders.Add(timeZoneProviderBase);
					}
				}
			}
		}

		// Token: 0x04002765 RID: 10085
		private static TimeZoneProviderCollection _timeZoneProviders;

		// Token: 0x04002766 RID: 10086
		private static readonly object _lock = new object();

		// Token: 0x04002767 RID: 10087
		private static readonly Type[] _providers = new Type[]
		{
			typeof(TimeZoneInfoProvider)
		};
	}
}
