using System;
using System.Diagnostics.Tracing;
using System.Runtime;
using System.ServiceModel.Description;

namespace System.ServiceModel
{
	// Token: 0x020000A3 RID: 163
	internal class TelemetryTraceLogging
	{
		// Token: 0x060002B7 RID: 695 RVA: 0x000106F8 File Offset: 0x0000E8F8
		public static void LogSeriveKPIData(ServiceDescription description)
		{
			try
			{
				TelemetryHelper telemetryHelper = new TelemetryHelper();
				if (TelemetryTraceLogging.logger != null && TelemetryTraceLogging.logger.IsEnabled())
				{
					TelemetryTraceLogging.logger.Write<ServiceKPI>(TelemetryTraceLogging.wcfHostTypeWithVersions, TelemetryEventSource.MeasuresOptions(), new ServiceKPI
					{
						ServiceId = telemetryHelper.GetServiceId(description),
						HostType = telemetryHelper.GetHostType(),
						EndpointsV2 = telemetryHelper.GetEndpoints(description),
						Version = telemetryHelper.GetAssemblyVersion()
					});
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
			}
		}

		// Token: 0x0400093F RID: 2367
		private static readonly string wcfHostTypeWithVersions = "WCFServiceKPI";

		// Token: 0x04000940 RID: 2368
		private static readonly string wcfproviderName = "Microsoft.DOTNET.WCF.ServiceModel";

		// Token: 0x04000941 RID: 2369
		private static EventSource logger = new TelemetryEventSource(TelemetryTraceLogging.wcfproviderName);
	}
}
