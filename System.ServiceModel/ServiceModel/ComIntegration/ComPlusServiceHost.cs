using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000210 RID: 528
	internal abstract class ComPlusServiceHost : ServiceHostBase
	{
		// Token: 0x06001024 RID: 4132 RVA: 0x000398AF File Offset: 0x00037AAF
		protected void Initialize(Guid clsid, ServiceElement service, ComCatalogObject applicationObject, ComCatalogObject classObject, HostingMode hostingMode)
		{
			this.VerifyFunctionality();
			this.info = new ServiceInfo(clsid, service, applicationObject, classObject, hostingMode);
			base.InitializeDescription(new UriSchemeKeyedCollection(new Uri[0]));
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x000398DA File Offset: 0x00037ADA
		protected override void ApplyConfiguration()
		{
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x000398DC File Offset: 0x00037ADC
		protected override ServiceDescription CreateDescription(out IDictionary<string, ContractDescription> implementedContracts)
		{
			ServiceDescription result;
			try
			{
				ComPlusServiceLoader comPlusServiceLoader = new ComPlusServiceLoader(this.info);
				ServiceDescription serviceDescription = comPlusServiceLoader.Load(this);
				implementedContracts = null;
				result = serviceDescription;
			}
			catch (Exception ex)
			{
				DiagnosticUtility.EventLog.LogEvent(TraceEventType.Error, 10, 3221356566U, new string[]
				{
					this.info.AppID.ToString(),
					this.info.Clsid.ToString(),
					ex.ToString()
				});
				throw;
			}
			return result;
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x00039974 File Offset: 0x00037B74
		protected override void InitializeRuntime()
		{
			ComPlusServiceHostTrace.Trace(TraceEventType.Information, 327681, "TraceCodeComIntegrationServiceHostStartingService", this.info);
			try
			{
				DispatcherBuilder dispatcherBuilder = new DispatcherBuilder();
				dispatcherBuilder.InitializeServiceHost(base.Description, this);
			}
			catch (Exception ex)
			{
				if (DiagnosticUtility.ShouldTraceError)
				{
					DiagnosticUtility.EventLog.LogEvent(TraceEventType.Error, 10, 3221356566U, new string[]
					{
						this.info.AppID.ToString(),
						this.info.Clsid.ToString(),
						ex.ToString()
					});
				}
				throw;
			}
			ComPlusServiceHostTrace.Trace(TraceEventType.Verbose, 327684, "TraceCodeComIntegrationServiceHostStartedServiceDetails", this.info, base.Description);
			ComPlusServiceHostTrace.Trace(TraceEventType.Information, 327682, "TraceCodeComIntegrationServiceHostStartedService", this.info);
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x00039A54 File Offset: 0x00037C54
		protected override void OnClose(TimeSpan timeout)
		{
			ComPlusServiceHostTrace.Trace(TraceEventType.Information, 327686, "TraceCodeComIntegrationServiceHostStoppingService", this.info);
			base.OnClose(timeout);
			ComPlusServiceHostTrace.Trace(TraceEventType.Information, 327687, "TraceCodeComIntegrationServiceHostStoppedService", this.info);
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x00039A8C File Offset: 0x00037C8C
		protected void VerifyFunctionality()
		{
			object obj = new CServiceConfig();
			if (!(obj is IServiceSysTxnConfig))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.QFENotPresent());
			}
		}

		// Token: 0x04001859 RID: 6233
		private ServiceInfo info;
	}
}
