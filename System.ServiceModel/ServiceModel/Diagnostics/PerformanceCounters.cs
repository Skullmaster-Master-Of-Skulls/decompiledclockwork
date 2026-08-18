using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime;
using System.Security;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A8A RID: 2698
	internal static class PerformanceCounters
	{
		// Token: 0x1700195C RID: 6492
		// (get) Token: 0x06006A7F RID: 27263 RVA: 0x0018D0AC File Offset: 0x0018B2AC
		// (set) Token: 0x06006A80 RID: 27264 RVA: 0x0018D0B3 File Offset: 0x0018B2B3
		internal static PerformanceCounterScope Scope
		{
			get
			{
				return PerformanceCounters.scope;
			}
			set
			{
				PerformanceCounters.scope = value;
			}
		}

		// Token: 0x1700195D RID: 6493
		// (get) Token: 0x06006A81 RID: 27265 RVA: 0x0018D0BB File Offset: 0x0018B2BB
		internal static bool PerformanceCountersEnabled
		{
			get
			{
				return PerformanceCounters.scope != PerformanceCounterScope.Off && PerformanceCounters.scope != PerformanceCounterScope.Default;
			}
		}

		// Token: 0x1700195E RID: 6494
		// (get) Token: 0x06006A82 RID: 27266 RVA: 0x0018D0D1 File Offset: 0x0018B2D1
		internal static bool MinimalPerformanceCountersEnabled
		{
			get
			{
				return PerformanceCounters.scope == PerformanceCounterScope.Default;
			}
		}

		// Token: 0x06006A83 RID: 27267 RVA: 0x0018D0DC File Offset: 0x0018B2DC
		static PerformanceCounters()
		{
			PerformanceCounterScope performanceCounterScope = PerformanceCounters.GetPerformanceCountersFromConfig();
			if (performanceCounterScope != PerformanceCounterScope.Off)
			{
				try
				{
					if (performanceCounterScope == PerformanceCounterScope.Default)
					{
						performanceCounterScope = (OSEnvironmentHelper.IsVistaOrGreater ? PerformanceCounterScope.ServiceOnly : PerformanceCounterScope.Off);
					}
					PerformanceCounters.scope = performanceCounterScope;
					return;
				}
				catch (SecurityException exception)
				{
					PerformanceCounters.scope = PerformanceCounterScope.Off;
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Warning);
					if (DiagnosticUtility.ShouldTraceWarning)
					{
						TraceUtility.TraceEvent(TraceEventType.Warning, 524344, SR.GetString("PartialTrustPerformanceCountersNotEnabled"));
					}
					return;
				}
			}
			PerformanceCounters.scope = PerformanceCounterScope.Off;
		}

		// Token: 0x06006A84 RID: 27268 RVA: 0x0018D17C File Offset: 0x0018B37C
		[SecuritySafeCritical]
		private static PerformanceCounterScope GetPerformanceCountersFromConfig()
		{
			return DiagnosticSection.UnsafeGetSection().PerformanceCounters;
		}

		// Token: 0x06006A85 RID: 27269 RVA: 0x0018D188 File Offset: 0x0018B388
		internal static PerformanceCounter GetOperationPerformanceCounter(string perfCounterName, string instanceName)
		{
			return PerformanceCounters.GetPerformanceCounter("ServiceModelOperation 4.0.0.0", perfCounterName, instanceName, PerformanceCounterInstanceLifetime.Process);
		}

		// Token: 0x06006A86 RID: 27270 RVA: 0x0018D197 File Offset: 0x0018B397
		internal static PerformanceCounter GetEndpointPerformanceCounter(string perfCounterName, string instanceName)
		{
			return PerformanceCounters.GetPerformanceCounter("ServiceModelEndpoint 4.0.0.0", perfCounterName, instanceName, PerformanceCounterInstanceLifetime.Process);
		}

		// Token: 0x06006A87 RID: 27271 RVA: 0x0018D1A6 File Offset: 0x0018B3A6
		internal static PerformanceCounter GetServicePerformanceCounter(string perfCounterName, string instanceName)
		{
			return PerformanceCounters.GetPerformanceCounter("ServiceModelService 4.0.0.0", perfCounterName, instanceName, PerformanceCounterInstanceLifetime.Process);
		}

		// Token: 0x06006A88 RID: 27272 RVA: 0x0018D1B5 File Offset: 0x0018B3B5
		internal static PerformanceCounter GetDefaultPerformanceCounter(string perfCounterName, string instanceName)
		{
			return PerformanceCounters.GetPerformanceCounter("ServiceModelService 4.0.0.0", perfCounterName, instanceName, PerformanceCounterInstanceLifetime.Global);
		}

		// Token: 0x06006A89 RID: 27273 RVA: 0x0018D1C4 File Offset: 0x0018B3C4
		internal static PerformanceCounter GetPerformanceCounter(string categoryName, string perfCounterName, string instanceName, PerformanceCounterInstanceLifetime instanceLifetime)
		{
			PerformanceCounter result = null;
			if (PerformanceCounters.PerformanceCountersEnabled || PerformanceCounters.MinimalPerformanceCountersEnabled)
			{
				result = PerformanceCounters.GetPerformanceCounterInternal(categoryName, perfCounterName, instanceName, instanceLifetime);
			}
			return result;
		}

		// Token: 0x06006A8A RID: 27274 RVA: 0x0018D1EC File Offset: 0x0018B3EC
		internal static PerformanceCounter GetPerformanceCounterInternal(string categoryName, string perfCounterName, string instanceName, PerformanceCounterInstanceLifetime instanceLifetime)
		{
			PerformanceCounter performanceCounter = null;
			try
			{
				performanceCounter = new PerformanceCounter();
				performanceCounter.CategoryName = categoryName;
				performanceCounter.CounterName = perfCounterName;
				performanceCounter.InstanceName = instanceName;
				performanceCounter.ReadOnly = false;
				performanceCounter.InstanceLifetime = instanceLifetime;
				try
				{
					long rawValue = performanceCounter.RawValue;
				}
				catch (InvalidOperationException)
				{
					performanceCounter = null;
					throw;
				}
				catch (SecurityException inner)
				{
					PerformanceCounters.scope = PerformanceCounterScope.Off;
					DiagnosticUtility.TraceHandledException(new SecurityException(SR.GetString("PartialTrustPerformanceCountersNotEnabled"), inner), TraceEventType.Warning);
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityException(SR.GetString("PartialTrustPerformanceCountersNotEnabled")));
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				if (performanceCounter != null)
				{
					if (!performanceCounter.ReadOnly)
					{
						try
						{
							performanceCounter.RemoveInstance();
						}
						catch (Exception exception)
						{
							if (Fx.IsFatal(exception))
							{
								throw;
							}
						}
					}
					performanceCounter = null;
				}
				bool flag = true;
				if (categoryName == "ServiceModelService 4.0.0.0")
				{
					if (!PerformanceCounters.serviceOOM)
					{
						PerformanceCounters.serviceOOM = true;
					}
					else
					{
						flag = false;
					}
				}
				else if (categoryName == "ServiceModelOperation 4.0.0.0")
				{
					if (!PerformanceCounters.operationOOM)
					{
						PerformanceCounters.operationOOM = true;
					}
					else
					{
						flag = false;
					}
				}
				else if (categoryName == "ServiceModelEndpoint 4.0.0.0")
				{
					if (!PerformanceCounters.endpointOOM)
					{
						PerformanceCounters.endpointOOM = true;
					}
					else
					{
						flag = false;
					}
				}
				if (flag)
				{
					DiagnosticUtility.EventLog.LogEvent(TraceEventType.Error, 8, 3221356554U, new string[]
					{
						categoryName,
						perfCounterName,
						ex.ToString()
					});
				}
			}
			return performanceCounter;
		}

		// Token: 0x1700195F RID: 6495
		// (get) Token: 0x06006A8B RID: 27275 RVA: 0x0018D364 File Offset: 0x0018B564
		internal static Dictionary<string, ServiceModelPerformanceCounters> PerformanceCountersForEndpoint
		{
			get
			{
				if (PerformanceCounters.performanceCounters == null)
				{
					object obj = PerformanceCounters.perfCounterDictionarySyncObject;
					lock (obj)
					{
						if (PerformanceCounters.performanceCounters == null)
						{
							PerformanceCounters.performanceCounters = new Dictionary<string, ServiceModelPerformanceCounters>();
						}
					}
				}
				return PerformanceCounters.performanceCounters;
			}
		}

		// Token: 0x17001960 RID: 6496
		// (get) Token: 0x06006A8C RID: 27276 RVA: 0x0018D3BC File Offset: 0x0018B5BC
		internal static List<ServiceModelPerformanceCounters> PerformanceCountersForEndpointList
		{
			get
			{
				if (PerformanceCounters.performanceCountersList == null)
				{
					object obj = PerformanceCounters.perfCounterDictionarySyncObject;
					lock (obj)
					{
						if (PerformanceCounters.performanceCountersList == null)
						{
							PerformanceCounters.performanceCountersList = new List<ServiceModelPerformanceCounters>();
						}
					}
				}
				return PerformanceCounters.performanceCountersList;
			}
		}

		// Token: 0x17001961 RID: 6497
		// (get) Token: 0x06006A8D RID: 27277 RVA: 0x0018D414 File Offset: 0x0018B614
		internal static Dictionary<string, ServiceModelPerformanceCountersEntry> PerformanceCountersForBaseUri
		{
			get
			{
				if (PerformanceCounters.performanceCountersBaseUri == null)
				{
					object obj = PerformanceCounters.perfCounterDictionarySyncObject;
					lock (obj)
					{
						if (PerformanceCounters.performanceCountersBaseUri == null)
						{
							PerformanceCounters.performanceCountersBaseUri = new Dictionary<string, ServiceModelPerformanceCountersEntry>();
						}
					}
				}
				return PerformanceCounters.performanceCountersBaseUri;
			}
		}

		// Token: 0x06006A8E RID: 27278 RVA: 0x0018D46C File Offset: 0x0018B66C
		internal static void AddPerformanceCountersForEndpoint(ServiceHostBase serviceHost, ContractDescription contractDescription, EndpointDispatcher endpointDispatcher)
		{
			bool performanceCountersEnabled = PerformanceCounters.PerformanceCountersEnabled;
			bool minimalPerformanceCountersEnabled = PerformanceCounters.MinimalPerformanceCountersEnabled;
			if ((performanceCountersEnabled || minimalPerformanceCountersEnabled) && endpointDispatcher.SetPerfCounterId())
			{
				object obj = PerformanceCounters.perfCounterDictionarySyncObject;
				ServiceModelPerformanceCounters serviceModelPerformanceCounters;
				lock (obj)
				{
					if (!PerformanceCounters.PerformanceCountersForEndpoint.TryGetValue(endpointDispatcher.PerfCounterId, out serviceModelPerformanceCounters))
					{
						serviceModelPerformanceCounters = new ServiceModelPerformanceCounters(serviceHost, contractDescription, endpointDispatcher);
						if (!serviceModelPerformanceCounters.Initialized)
						{
							return;
						}
						PerformanceCounters.PerformanceCountersForEndpoint.Add(endpointDispatcher.PerfCounterId, serviceModelPerformanceCounters);
						int num = PerformanceCounters.PerformanceCountersForEndpointList.FindIndex((ServiceModelPerformanceCounters c) => c == null);
						if (num >= 0)
						{
							PerformanceCounters.PerformanceCountersForEndpointList[num] = serviceModelPerformanceCounters;
						}
						else
						{
							PerformanceCounters.PerformanceCountersForEndpointList.Add(serviceModelPerformanceCounters);
							num = PerformanceCounters.PerformanceCountersForEndpointList.Count - 1;
						}
						endpointDispatcher.PerfCounterInstanceId = num;
					}
				}
				object obj2 = PerformanceCounters.perfCounterDictionarySyncObject;
				lock (obj2)
				{
					ServiceModelPerformanceCountersEntry serviceModelPerformanceCountersEntry;
					if (!PerformanceCounters.PerformanceCountersForBaseUri.TryGetValue(endpointDispatcher.PerfCounterBaseId, out serviceModelPerformanceCountersEntry))
					{
						if (performanceCountersEnabled)
						{
							serviceModelPerformanceCountersEntry = new ServiceModelPerformanceCountersEntry(serviceHost.Counters);
						}
						else if (minimalPerformanceCountersEnabled)
						{
							serviceModelPerformanceCountersEntry = new ServiceModelPerformanceCountersEntry(serviceHost.DefaultCounters);
						}
						PerformanceCounters.PerformanceCountersForBaseUri.Add(endpointDispatcher.PerfCounterBaseId, serviceModelPerformanceCountersEntry);
					}
					serviceModelPerformanceCountersEntry.Add(serviceModelPerformanceCounters);
				}
			}
		}

		// Token: 0x06006A8F RID: 27279 RVA: 0x0018D5E0 File Offset: 0x0018B7E0
		internal static void ReleasePerformanceCountersForEndpoint(string id, string baseId)
		{
			if (PerformanceCounters.PerformanceCountersEnabled)
			{
				object obj = PerformanceCounters.perfCounterDictionarySyncObject;
				lock (obj)
				{
					ServiceModelPerformanceCounters item;
					if (!string.IsNullOrEmpty(id) && PerformanceCounters.PerformanceCountersForEndpoint.TryGetValue(id, out item))
					{
						PerformanceCounters.PerformanceCountersForEndpoint.Remove(id);
						int index = PerformanceCounters.PerformanceCountersForEndpointList.IndexOf(item);
						PerformanceCounters.PerformanceCountersForEndpointList[index] = null;
					}
					if (!string.IsNullOrEmpty(baseId))
					{
						PerformanceCounters.PerformanceCountersForBaseUri.Remove(baseId);
					}
				}
			}
		}

		// Token: 0x06006A90 RID: 27280 RVA: 0x0018D670 File Offset: 0x0018B870
		internal static void ReleasePerformanceCounter(ref PerformanceCounter counter)
		{
			if (counter != null)
			{
				try
				{
					counter.RemoveInstance();
					counter = null;
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x06006A91 RID: 27281 RVA: 0x0018D6AC File Offset: 0x0018B8AC
		internal static void TxFlowed(EndpointDispatcher el, string operation)
		{
			if (el != null)
			{
				ServicePerformanceCountersBase servicePerformanceCounters = PerformanceCounters.GetServicePerformanceCounters(el.PerfCounterInstanceId);
				if (servicePerformanceCounters != null)
				{
					servicePerformanceCounters.TxFlowed();
				}
				if (PerformanceCounters.Scope == PerformanceCounterScope.All)
				{
					OperationPerformanceCountersBase operationPerformanceCounters = PerformanceCounters.GetOperationPerformanceCounters(el.PerfCounterInstanceId, operation);
					if (operationPerformanceCounters != null)
					{
						operationPerformanceCounters.TxFlowed();
					}
					EndpointPerformanceCountersBase endpointPerformanceCounters = PerformanceCounters.GetEndpointPerformanceCounters(el.PerfCounterInstanceId);
					if (servicePerformanceCounters != null)
					{
						endpointPerformanceCounters.TxFlowed();
					}
				}
			}
		}

		// Token: 0x06006A92 RID: 27282 RVA: 0x0018D704 File Offset: 0x0018B904
		internal static void TxAborted(EndpointDispatcher el, long count)
		{
			if (PerformanceCounters.PerformanceCountersEnabled && el != null)
			{
				ServicePerformanceCountersBase servicePerformanceCounters = PerformanceCounters.GetServicePerformanceCounters(el.PerfCounterInstanceId);
				if (servicePerformanceCounters != null)
				{
					servicePerformanceCounters.TxAborted(count);
				}
			}
		}

		// Token: 0x06006A93 RID: 27283 RVA: 0x0018D734 File Offset: 0x0018B934
		internal static void TxCommitted(EndpointDispatcher el, long count)
		{
			if (PerformanceCounters.PerformanceCountersEnabled && el != null)
			{
				ServicePerformanceCountersBase servicePerformanceCounters = PerformanceCounters.GetServicePerformanceCounters(el.PerfCounterInstanceId);
				if (servicePerformanceCounters != null)
				{
					servicePerformanceCounters.TxCommitted(count);
				}
			}
		}

		// Token: 0x06006A94 RID: 27284 RVA: 0x0018D764 File Offset: 0x0018B964
		internal static void TxInDoubt(EndpointDispatcher el, long count)
		{
			if (PerformanceCounters.PerformanceCountersEnabled && el != null)
			{
				ServicePerformanceCountersBase servicePerformanceCounters = PerformanceCounters.GetServicePerformanceCounters(el.PerfCounterInstanceId);
				if (servicePerformanceCounters != null)
				{
					servicePerformanceCounters.TxInDoubt(count);
				}
			}
		}

		// Token: 0x06006A95 RID: 27285 RVA: 0x0018D794 File Offset: 0x0018B994
		internal static void MethodCalled(string operationName)
		{
			EndpointDispatcher endpointDispatcher = PerformanceCounters.GetEndpointDispatcher();
			if (endpointDispatcher != null)
			{
				if (PerformanceCounters.Scope == PerformanceCounterScope.All)
				{
					string perfCounterId = endpointDispatcher.PerfCounterId;
					OperationPerformanceCountersBase operationPerformanceCounters = PerformanceCounters.GetOperationPerformanceCounters(endpointDispatcher.PerfCounterInstanceId, operationName);
					if (operationPerformanceCounters != null)
					{
						operationPerformanceCounters.MethodCalled();
					}
					EndpointPerformanceCountersBase endpointPerformanceCounters = PerformanceCounters.GetEndpointPerformanceCounters(endpointDispatcher.PerfCounterInstanceId);
					if (endpointPerformanceCounters != null)
					{
						endpointPerformanceCounters.MethodCalled();
					}
				}
				ServicePerformanceCountersBase servicePerformanceCounters = PerformanceCounters.GetServicePerformanceCounters(endpointDispatcher.PerfCounterInstanceId);
				if (servicePerformanceCounters != null)
				{
					servicePerformanceCounters.MethodCalled();
				}
			}
		}

		// Token: 0x06006A96 RID: 27286 RVA: 0x0018D7FC File Offset: 0x0018B9FC
		internal static void MethodReturnedSuccess(string operationName)
		{
			PerformanceCounters.MethodReturnedSuccess(operationName, -1L);
		}

		// Token: 0x06006A97 RID: 27287 RVA: 0x0018D808 File Offset: 0x0018BA08
		internal static void MethodReturnedSuccess(string operationName, long time)
		{
			EndpointDispatcher endpointDispatcher = PerformanceCounters.GetEndpointDispatcher();
			if (endpointDispatcher != null)
			{
				if (PerformanceCounters.Scope == PerformanceCounterScope.All)
				{
					string perfCounterId = endpointDispatcher.PerfCounterId;
					OperationPerformanceCountersBase operationPerformanceCounters = PerformanceCounters.GetOperationPerformanceCounters(endpointDispatcher.PerfCounterInstanceId, operationName);
					if (operationPerformanceCounters != null)
					{
						operationPerformanceCounters.MethodReturnedSuccess();
						if (time > 0L)
						{
							operationPerformanceCounters.SaveCallDuration(time);
						}
					}
					EndpointPerformanceCountersBase endpointPerformanceCounters = PerformanceCounters.GetEndpointPerformanceCounters(endpointDispatcher.PerfCounterInstanceId);
					if (endpointPerformanceCounters != null)
					{
						endpointPerformanceCounters.MethodReturnedSuccess();
						if (time > 0L)
						{
							endpointPerformanceCounters.SaveCallDuration(time);
						}
					}
				}
				ServicePerformanceCountersBase servicePerformanceCounters = PerformanceCounters.GetServicePerformanceCounters(endpointDispatcher.PerfCounterInstanceId);
				if (servicePerformanceCounters != null)
				{
					servicePerformanceCounters.MethodReturnedSuccess();
					if (time > 0L)
					{
						servicePerformanceCounters.SaveCallDuration(time);
					}
				}
			}
		}

		// Token: 0x06006A98 RID: 27288 RVA: 0x0018D895 File Offset: 0x0018BA95
		internal static void MethodReturnedFault(string operationName)
		{
			PerformanceCounters.MethodReturnedFault(operationName, -1L);
		}

		// Token: 0x06006A99 RID: 27289 RVA: 0x0018D8A0 File Offset: 0x0018BAA0
		internal static void MethodReturnedFault(string operationName, long time)
		{
			EndpointDispatcher endpointDispatcher = PerformanceCounters.GetEndpointDispatcher();
			if (endpointDispatcher != null)
			{
				if (PerformanceCounters.Scope == PerformanceCounterScope.All)
				{
					string perfCounterId = endpointDispatcher.PerfCounterId;
					OperationPerformanceCountersBase operationPerformanceCounters = PerformanceCounters.GetOperationPerformanceCounters(endpointDispatcher.PerfCounterInstanceId, operationName);
					if (operationPerformanceCounters != null)
					{
						operationPerformanceCounters.MethodReturnedFault();
						if (time > 0L)
						{
							operationPerformanceCounters.SaveCallDuration(time);
						}
					}
					EndpointPerformanceCountersBase endpointPerformanceCounters = PerformanceCounters.GetEndpointPerformanceCounters(endpointDispatcher.PerfCounterInstanceId);
					if (endpointPerformanceCounters != null)
					{
						endpointPerformanceCounters.MethodReturnedFault();
						if (time > 0L)
						{
							endpointPerformanceCounters.SaveCallDuration(time);
						}
					}
				}
				ServicePerformanceCountersBase servicePerformanceCounters = PerformanceCounters.GetServicePerformanceCounters(endpointDispatcher.PerfCounterInstanceId);
				if (servicePerformanceCounters != null)
				{
					servicePerformanceCounters.MethodReturnedFault();
					if (time > 0L)
					{
						servicePerformanceCounters.SaveCallDuration(time);
					}
				}
			}
		}

		// Token: 0x06006A9A RID: 27290 RVA: 0x0018D92D File Offset: 0x0018BB2D
		internal static void MethodReturnedError(string operationName)
		{
			PerformanceCounters.MethodReturnedError(operationName, -1L);
		}

		// Token: 0x06006A9B RID: 27291 RVA: 0x0018D938 File Offset: 0x0018BB38
		internal static void MethodReturnedError(string operationName, long time)
		{
			EndpointDispatcher endpointDispatcher = PerformanceCounters.GetEndpointDispatcher();
			if (endpointDispatcher != null)
			{
				if (PerformanceCounters.Scope == PerformanceCounterScope.All)
				{
					string perfCounterId = endpointDispatcher.PerfCounterId;
					OperationPerformanceCountersBase operationPerformanceCounters = PerformanceCounters.GetOperationPerformanceCounters(endpointDispatcher.PerfCounterInstanceId, operationName);
					if (operationPerformanceCounters != null)
					{
						operationPerformanceCounters.MethodReturnedError();
						if (time > 0L)
						{
							operationPerformanceCounters.SaveCallDuration(time);
						}
					}
					EndpointPerformanceCountersBase endpointPerformanceCounters = PerformanceCounters.GetEndpointPerformanceCounters(endpointDispatcher.PerfCounterInstanceId);
					if (endpointPerformanceCounters != null)
					{
						endpointPerformanceCounters.MethodReturnedError();
						if (time > 0L)
						{
							endpointPerformanceCounters.SaveCallDuration(time);
						}
					}
				}
				ServicePerformanceCountersBase servicePerformanceCounters = PerformanceCounters.GetServicePerformanceCounters(endpointDispatcher.PerfCounterInstanceId);
				if (servicePerformanceCounters != null)
				{
					servicePerformanceCounters.MethodReturnedError();
					if (time > 0L)
					{
						servicePerformanceCounters.SaveCallDuration(time);
					}
				}
			}
		}

		// Token: 0x06006A9C RID: 27292 RVA: 0x0018D9C8 File Offset: 0x0018BBC8
		private static void InvokeMethod(object o, string methodName)
		{
			MethodInfo method = o.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);
			method.Invoke(o, null);
		}

		// Token: 0x06006A9D RID: 27293 RVA: 0x0018D9F0 File Offset: 0x0018BBF0
		private static void CallOnAllCounters(string methodName, Message message, Uri listenUri, bool includeOperations)
		{
			if (message != null && message.Headers != null && null != message.Headers.To && null != listenUri)
			{
				string uri = listenUri.AbsoluteUri.ToUpperInvariant();
				ServiceModelPerformanceCountersEntry serviceModelPerformanceCountersBaseUri = PerformanceCounters.GetServiceModelPerformanceCountersBaseUri(uri);
				if (serviceModelPerformanceCountersBaseUri != null)
				{
					PerformanceCounters.InvokeMethod(serviceModelPerformanceCountersBaseUri.ServicePerformanceCounters, methodName);
					if (PerformanceCounters.Scope == PerformanceCounterScope.All)
					{
						List<ServiceModelPerformanceCounters> counterList = serviceModelPerformanceCountersBaseUri.CounterList;
						foreach (ServiceModelPerformanceCounters serviceModelPerformanceCounters in counterList)
						{
							if (serviceModelPerformanceCounters.EndpointPerformanceCounters != null)
							{
								PerformanceCounters.InvokeMethod(serviceModelPerformanceCounters.EndpointPerformanceCounters, methodName);
							}
							if (includeOperations)
							{
								OperationPerformanceCountersBase operationPerformanceCountersFromMessage = serviceModelPerformanceCounters.GetOperationPerformanceCountersFromMessage(message);
								if (operationPerformanceCountersFromMessage != null)
								{
									PerformanceCounters.InvokeMethod(operationPerformanceCountersFromMessage, methodName);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06006A9E RID: 27294 RVA: 0x0018DACC File Offset: 0x0018BCCC
		internal static void AuthenticationFailed(Message message, Uri listenUri)
		{
			PerformanceCounters.CallOnAllCounters("AuthenticationFailed", message, listenUri, true);
		}

		// Token: 0x06006A9F RID: 27295 RVA: 0x0018DADC File Offset: 0x0018BCDC
		internal static void AuthorizationFailed(string operationName)
		{
			EndpointDispatcher endpointDispatcher = PerformanceCounters.GetEndpointDispatcher();
			if (endpointDispatcher != null)
			{
				string perfCounterId = endpointDispatcher.PerfCounterId;
				if (PerformanceCounters.Scope == PerformanceCounterScope.All)
				{
					OperationPerformanceCountersBase operationPerformanceCounters = PerformanceCounters.GetOperationPerformanceCounters(endpointDispatcher.PerfCounterInstanceId, operationName);
					if (operationPerformanceCounters != null)
					{
						operationPerformanceCounters.AuthorizationFailed();
					}
					EndpointPerformanceCountersBase endpointPerformanceCounters = PerformanceCounters.GetEndpointPerformanceCounters(endpointDispatcher.PerfCounterInstanceId);
					if (endpointPerformanceCounters != null)
					{
						endpointPerformanceCounters.AuthorizationFailed();
					}
				}
				ServicePerformanceCountersBase servicePerformanceCounters = PerformanceCounters.GetServicePerformanceCounters(endpointDispatcher.PerfCounterInstanceId);
				if (servicePerformanceCounters != null)
				{
					servicePerformanceCounters.AuthorizationFailed();
				}
			}
		}

		// Token: 0x06006AA0 RID: 27296 RVA: 0x0018DB44 File Offset: 0x0018BD44
		internal static void SessionFaulted(string uri)
		{
			ServiceModelPerformanceCountersEntry serviceModelPerformanceCountersBaseUri = PerformanceCounters.GetServiceModelPerformanceCountersBaseUri(uri);
			if (serviceModelPerformanceCountersBaseUri != null)
			{
				serviceModelPerformanceCountersBaseUri.ServicePerformanceCounters.SessionFaulted();
				if (PerformanceCounters.Scope == PerformanceCounterScope.All)
				{
					List<ServiceModelPerformanceCounters> counterList = serviceModelPerformanceCountersBaseUri.CounterList;
					foreach (ServiceModelPerformanceCounters serviceModelPerformanceCounters in counterList)
					{
						if (serviceModelPerformanceCounters.EndpointPerformanceCounters != null)
						{
							serviceModelPerformanceCounters.EndpointPerformanceCounters.SessionFaulted();
						}
					}
				}
			}
		}

		// Token: 0x06006AA1 RID: 27297 RVA: 0x0018DBC4 File Offset: 0x0018BDC4
		internal static void MessageDropped(string uri)
		{
			ServiceModelPerformanceCountersEntry serviceModelPerformanceCountersBaseUri = PerformanceCounters.GetServiceModelPerformanceCountersBaseUri(uri);
			if (serviceModelPerformanceCountersBaseUri != null)
			{
				serviceModelPerformanceCountersBaseUri.ServicePerformanceCounters.MessageDropped();
				if (PerformanceCounters.Scope == PerformanceCounterScope.All)
				{
					List<ServiceModelPerformanceCounters> counterList = serviceModelPerformanceCountersBaseUri.CounterList;
					foreach (ServiceModelPerformanceCounters serviceModelPerformanceCounters in counterList)
					{
						if (serviceModelPerformanceCounters.EndpointPerformanceCounters != null)
						{
							serviceModelPerformanceCounters.EndpointPerformanceCounters.MessageDropped();
						}
					}
				}
			}
		}

		// Token: 0x06006AA2 RID: 27298 RVA: 0x0018DC44 File Offset: 0x0018BE44
		internal static void MsmqDroppedMessage(string uri)
		{
			if (PerformanceCounters.Scope == PerformanceCounterScope.All)
			{
				ServiceModelPerformanceCountersEntry serviceModelPerformanceCountersBaseUri = PerformanceCounters.GetServiceModelPerformanceCountersBaseUri(uri);
				if (serviceModelPerformanceCountersBaseUri != null)
				{
					serviceModelPerformanceCountersBaseUri.ServicePerformanceCounters.MsmqDroppedMessage();
				}
			}
		}

		// Token: 0x06006AA3 RID: 27299 RVA: 0x0018DC70 File Offset: 0x0018BE70
		internal static void MsmqPoisonMessage(string uri)
		{
			if (PerformanceCounters.Scope == PerformanceCounterScope.All)
			{
				ServiceModelPerformanceCountersEntry serviceModelPerformanceCountersBaseUri = PerformanceCounters.GetServiceModelPerformanceCountersBaseUri(uri);
				if (serviceModelPerformanceCountersBaseUri != null)
				{
					serviceModelPerformanceCountersBaseUri.ServicePerformanceCounters.MsmqPoisonMessage();
				}
			}
		}

		// Token: 0x06006AA4 RID: 27300 RVA: 0x0018DC9C File Offset: 0x0018BE9C
		internal static void MsmqRejectedMessage(string uri)
		{
			if (PerformanceCounters.Scope == PerformanceCounterScope.All)
			{
				ServiceModelPerformanceCountersEntry serviceModelPerformanceCountersBaseUri = PerformanceCounters.GetServiceModelPerformanceCountersBaseUri(uri);
				if (serviceModelPerformanceCountersBaseUri != null)
				{
					serviceModelPerformanceCountersBaseUri.ServicePerformanceCounters.MsmqRejectedMessage();
				}
			}
		}

		// Token: 0x06006AA5 RID: 27301 RVA: 0x0018DCC8 File Offset: 0x0018BEC8
		internal static EndpointDispatcher GetEndpointDispatcher()
		{
			EndpointDispatcher result = null;
			OperationContext operationContext = OperationContext.Current;
			if (operationContext != null && operationContext.InternalServiceChannel != null)
			{
				result = operationContext.EndpointDispatcher;
			}
			return result;
		}

		// Token: 0x06006AA6 RID: 27302 RVA: 0x0018DCF0 File Offset: 0x0018BEF0
		private static ServiceModelPerformanceCounters GetServiceModelPerformanceCounters(int perfCounterInstanceId)
		{
			if (PerformanceCounters.PerformanceCountersForEndpointList.Count == 0)
			{
				return null;
			}
			return PerformanceCounters.PerformanceCountersForEndpointList[perfCounterInstanceId];
		}

		// Token: 0x06006AA7 RID: 27303 RVA: 0x0018DD0C File Offset: 0x0018BF0C
		private static ServiceModelPerformanceCountersEntry GetServiceModelPerformanceCountersBaseUri(string uri)
		{
			ServiceModelPerformanceCountersEntry result = null;
			if (!string.IsNullOrEmpty(uri))
			{
				PerformanceCounters.PerformanceCountersForBaseUri.TryGetValue(uri, out result);
			}
			return result;
		}

		// Token: 0x06006AA8 RID: 27304 RVA: 0x0018DD34 File Offset: 0x0018BF34
		private static OperationPerformanceCountersBase GetOperationPerformanceCounters(int perfCounterInstanceId, string operation)
		{
			ServiceModelPerformanceCounters serviceModelPerformanceCounters = PerformanceCounters.GetServiceModelPerformanceCounters(perfCounterInstanceId);
			if (serviceModelPerformanceCounters != null)
			{
				return serviceModelPerformanceCounters.GetOperationPerformanceCounters(operation);
			}
			return null;
		}

		// Token: 0x06006AA9 RID: 27305 RVA: 0x0018DD54 File Offset: 0x0018BF54
		private static EndpointPerformanceCountersBase GetEndpointPerformanceCounters(int perfCounterInstanceId)
		{
			ServiceModelPerformanceCounters serviceModelPerformanceCounters = PerformanceCounters.GetServiceModelPerformanceCounters(perfCounterInstanceId);
			if (serviceModelPerformanceCounters != null)
			{
				return serviceModelPerformanceCounters.EndpointPerformanceCounters;
			}
			return null;
		}

		// Token: 0x06006AAA RID: 27306 RVA: 0x0018DD74 File Offset: 0x0018BF74
		private static ServicePerformanceCountersBase GetServicePerformanceCounters(int perfCounterInstanceId)
		{
			ServiceModelPerformanceCounters serviceModelPerformanceCounters = PerformanceCounters.GetServiceModelPerformanceCounters(perfCounterInstanceId);
			if (serviceModelPerformanceCounters != null)
			{
				return serviceModelPerformanceCounters.ServicePerformanceCounters;
			}
			return null;
		}

		// Token: 0x06006AAB RID: 27307 RVA: 0x0018DD94 File Offset: 0x0018BF94
		internal static void TracePerformanceCounterUpdateFailure(string instanceName, string perfCounterName)
		{
			if (DiagnosticUtility.ShouldTraceError)
			{
				TraceUtility.TraceEvent(TraceEventType.Error, 524346, SR.GetString("TraceCodePerformanceCountersFailedDuringUpdate", new object[]
				{
					perfCounterName + "::" + instanceName
				}));
			}
		}

		// Token: 0x04003CB6 RID: 15542
		private static PerformanceCounterScope scope;

		// Token: 0x04003CB7 RID: 15543
		private static object perfCounterDictionarySyncObject = new object();

		// Token: 0x04003CB8 RID: 15544
		internal const int MaxInstanceNameLength = 127;

		// Token: 0x04003CB9 RID: 15545
		private static bool serviceOOM = false;

		// Token: 0x04003CBA RID: 15546
		private static bool endpointOOM = false;

		// Token: 0x04003CBB RID: 15547
		private static bool operationOOM = false;

		// Token: 0x04003CBC RID: 15548
		private static Dictionary<string, ServiceModelPerformanceCounters> performanceCounters = null;

		// Token: 0x04003CBD RID: 15549
		private static Dictionary<string, ServiceModelPerformanceCountersEntry> performanceCountersBaseUri = null;

		// Token: 0x04003CBE RID: 15550
		private static List<ServiceModelPerformanceCounters> performanceCountersList = null;
	}
}
