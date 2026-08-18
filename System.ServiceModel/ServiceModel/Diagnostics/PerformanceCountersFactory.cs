using System;
using System.Diagnostics;
using System.Runtime;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A89 RID: 2697
	internal static class PerformanceCountersFactory
	{
		// Token: 0x06006A7A RID: 27258 RVA: 0x0018CDD8 File Offset: 0x0018AFD8
		internal static ServicePerformanceCountersBase CreateServiceCounters(ServiceHostBase serviceHost)
		{
			if (!PerformanceCountersFactory.CheckPermissions())
			{
				return null;
			}
			if (OSEnvironmentHelper.IsVistaOrGreater)
			{
				try
				{
					PerformanceCountersFactory.EnsureCategoriesExistIfNeeded();
					ServicePerformanceCountersV2 result = new ServicePerformanceCountersV2(serviceHost);
					EndpointPerformanceCountersV2.EnsureCounterSet();
					OperationPerformanceCountersV2.EnsureCounterSet();
					return result;
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					PerformanceCounters.Scope = PerformanceCounterScope.Off;
					if (DiagnosticUtility.ShouldTraceError)
					{
						TraceUtility.TraceEvent(TraceEventType.Error, 524347, SR.GetString("TraceCodePerformanceCountersFailedForService"), null, exception);
					}
					return null;
				}
			}
			return new ServicePerformanceCounters(serviceHost);
		}

		// Token: 0x06006A7B RID: 27259 RVA: 0x0018CE5C File Offset: 0x0018B05C
		internal static EndpointPerformanceCountersBase CreateEndpointCounters(string service, string contract, string uri)
		{
			if (!PerformanceCountersFactory.CheckPermissions())
			{
				return null;
			}
			if (OSEnvironmentHelper.IsVistaOrGreater)
			{
				try
				{
					PerformanceCountersFactory.EnsureCategoriesExistIfNeeded();
					return new EndpointPerformanceCountersV2(service, contract, uri);
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					PerformanceCounters.Scope = PerformanceCounterScope.Off;
					if (DiagnosticUtility.ShouldTraceError)
					{
						TraceUtility.TraceEvent(TraceEventType.Error, 524347, SR.GetString("TraceCodePerformanceCountersFailedForService"), null, exception);
					}
					return null;
				}
			}
			return new EndpointPerformanceCounters(service, contract, uri);
		}

		// Token: 0x06006A7C RID: 27260 RVA: 0x0018CED8 File Offset: 0x0018B0D8
		internal static OperationPerformanceCountersBase CreateOperationCounters(string service, string contract, string operationName, string uri)
		{
			if (!PerformanceCountersFactory.CheckPermissions())
			{
				return null;
			}
			if (OSEnvironmentHelper.IsVistaOrGreater)
			{
				try
				{
					PerformanceCountersFactory.EnsureCategoriesExistIfNeeded();
					return new OperationPerformanceCountersV2(service, contract, operationName, uri);
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					PerformanceCounters.Scope = PerformanceCounterScope.Off;
					if (DiagnosticUtility.ShouldTraceError)
					{
						TraceUtility.TraceEvent(TraceEventType.Error, 524347, SR.GetString("TraceCodePerformanceCountersFailedForService"), null, exception);
					}
					return null;
				}
			}
			return new OperationPerformanceCounters(service, contract, operationName, uri);
		}

		// Token: 0x06006A7D RID: 27261 RVA: 0x0018CF54 File Offset: 0x0018B154
		private static bool CheckPermissions()
		{
			if (PartialTrustHelpers.AppDomainFullyTrusted)
			{
				return true;
			}
			PerformanceCounters.Scope = PerformanceCounterScope.Off;
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, 524347, SR.GetString("PartialTrustPerformanceCountersNotEnabled"));
			}
			return false;
		}

		// Token: 0x06006A7E RID: 27262 RVA: 0x0018CF84 File Offset: 0x0018B184
		private static void EnsureCategoriesExistIfNeeded()
		{
			if (PerformanceCountersFactory.categoriesExist || !ServiceModelAppSettings.EnsureUniquePerformanceCounterInstanceNames)
			{
				return;
			}
			OperationPerformanceCountersV2 operationPerformanceCountersV = null;
			EndpointPerformanceCountersV2 endpointPerformanceCountersV = null;
			ServicePerformanceCountersV2 servicePerformanceCountersV = null;
			try
			{
				if (PerformanceCounterCategory.Exists("ServiceModelOperation 4.0.0.0") && PerformanceCounterCategory.Exists("ServiceModelEndpoint 4.0.0.0") && PerformanceCounterCategory.Exists("ServiceModelService 4.0.0.0"))
				{
					PerformanceCountersFactory.categoriesExist = true;
				}
				else
				{
					ServiceHost serviceHost = new ServiceHost(typeof(object), new Uri[]
					{
						new Uri("http://_WCF_Admin")
					});
					operationPerformanceCountersV = new OperationPerformanceCountersV2("_WCF_Admin", "_WCF_Admin", "_WCF_Admin", "_WCF_Admin");
					endpointPerformanceCountersV = new EndpointPerformanceCountersV2("_WCF_Admin", "_WCF_Admin", "_WCF_Admin");
					servicePerformanceCountersV = new ServicePerformanceCountersV2(serviceHost);
					PerformanceCounter.CloseSharedResources();
					PerformanceCounterCategory.Exists("_WCF_Admin");
				}
			}
			catch (UnauthorizedAccessException)
			{
				if (DiagnosticUtility.ShouldTraceWarning)
				{
					TraceUtility.TraceEvent(TraceEventType.Warning, 524347, SR.GetString("EnsureCategoriesExistFailedPermission"));
				}
			}
			catch
			{
			}
			finally
			{
				if (operationPerformanceCountersV != null)
				{
					operationPerformanceCountersV.DeleteInstance();
				}
				if (endpointPerformanceCountersV != null)
				{
					endpointPerformanceCountersV.DeleteInstance();
				}
				if (servicePerformanceCountersV != null)
				{
					servicePerformanceCountersV.DeleteInstance();
				}
				PerformanceCountersFactory.categoriesExist = true;
			}
		}

		// Token: 0x04003CB5 RID: 15541
		private static bool categoriesExist;
	}
}
