using System;
using System.Reflection;
using TechnoPro.Common.ICore.AppointmentSync;
using TechnoPro.Common.Public.Entities.AppointmentSync;

namespace TechnoPro.Common.Core.ApplicationSyncFactories
{
	// Token: 0x02000002 RID: 2
	public static class ApplicationSyncFactory
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static IApplicationSyncFactory GetSyncFactory(SyncOperationContext opContext)
		{
			eApplicationSyncProviderName appSyncProviderName = opContext.AppSyncProviderName;
			eApplicationSyncProviderName eApplicationSyncProviderName = appSyncProviderName;
			IApplicationSyncFactory result;
			if (eApplicationSyncProviderName != eApplicationSyncProviderName.MicrosofExchange)
			{
				if (eApplicationSyncProviderName != eApplicationSyncProviderName.GoogleCalendar)
				{
					result = ApplicationSyncFactory.GetExchangeSyncFactory(opContext);
				}
				else
				{
					result = ApplicationSyncFactory.GetGoogleCalendarSyncFactory(opContext);
				}
			}
			else
			{
				result = ApplicationSyncFactory.GetExchangeSyncFactory(opContext);
			}
			return result;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002090 File Offset: 0x00000290
		private static IApplicationSyncFactory GetGoogleCalendarSyncFactory(SyncOperationContext opContext)
		{
			Assembly assembly = Assembly.Load("Common.Core.GoogleCalendar");
			bool flag = assembly == null;
			IApplicationSyncFactory result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Type type = assembly.GetType("TechnoPro.Common.Core.GoogleCalendar.GoogleCalendarSyncFactory");
				result = (IApplicationSyncFactory)Activator.CreateInstance(type, new object[]
				{
					opContext
				});
			}
			return result;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020E0 File Offset: 0x000002E0
		private static IApplicationSyncFactory GetExchangeSyncFactory(SyncOperationContext opContext)
		{
			Assembly assembly = Assembly.Load("Common.Core.Exchange");
			bool flag = assembly == null;
			IApplicationSyncFactory result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Type type = assembly.GetType("TechnoPro.Common.Core.Exchange.ExchangeSyncFactory");
				result = (IApplicationSyncFactory)Activator.CreateInstance(type, new object[]
				{
					opContext
				});
			}
			return result;
		}
	}
}
