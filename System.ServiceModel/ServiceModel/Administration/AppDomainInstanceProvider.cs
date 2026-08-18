using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Administration
{
	// Token: 0x02000445 RID: 1093
	internal class AppDomainInstanceProvider : ProviderBase, IWmiProvider
	{
		// Token: 0x06002A8A RID: 10890 RVA: 0x000A43BC File Offset: 0x000A25BC
		void IWmiProvider.EnumInstances(IWmiInstances instances)
		{
			IWmiInstance wmiInstance = instances.NewInstance(null);
			AppDomainInstanceProvider.FillAppDomainInfo(wmiInstance);
			instances.AddInstance(wmiInstance);
		}

		// Token: 0x06002A8B RID: 10891 RVA: 0x000A43E0 File Offset: 0x000A25E0
		bool IWmiProvider.GetInstance(IWmiInstance instance)
		{
			bool result = false;
			if ((int)instance.GetProperty("ProcessId") == AppDomainInfo.Current.ProcessId && string.Equals((string)instance.GetProperty("Name"), AppDomainInfo.Current.Name, StringComparison.Ordinal))
			{
				AppDomainInstanceProvider.FillAppDomainInfo(instance);
				result = true;
			}
			return result;
		}

		// Token: 0x06002A8C RID: 10892 RVA: 0x000A4438 File Offset: 0x000A2638
		internal static string GetReference()
		{
			return string.Format(CultureInfo.InvariantCulture, "AppDomainInfo.AppDomainId={0},Name='{1}',ProcessId={2}", new object[]
			{
				AppDomainInfo.Current.Id,
				AppDomainInfo.Current.Name,
				AppDomainInfo.Current.ProcessId
			});
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x000A448C File Offset: 0x000A268C
		internal static void FillAppDomainInfo(IWmiInstance instance)
		{
			AppDomainInfo appDomainInfo = AppDomainInfo.Current;
			instance.SetProperty("Name", appDomainInfo.Name);
			instance.SetProperty("AppDomainId", appDomainInfo.Id);
			instance.SetProperty("PerformanceCounters", PerformanceCounters.Scope.ToString());
			instance.SetProperty("IsDefault", appDomainInfo.IsDefaultAppDomain);
			instance.SetProperty("ProcessId", appDomainInfo.ProcessId);
			instance.SetProperty("TraceLevel", DiagnosticUtility.Level.ToString());
			instance.SetProperty("LogMalformedMessages", MessageLogger.LogMalformedMessages);
			instance.SetProperty("LogMessagesAtServiceLevel", MessageLogger.LogMessagesAtServiceLevel);
			instance.SetProperty("LogMessagesAtTransportLevel", MessageLogger.LogMessagesAtTransportLevel);
			instance.SetProperty("ServiceConfigPath", AspNetEnvironment.Current.ConfigurationPath);
			AppDomainInstanceProvider.FillListenersInfo(instance);
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x000A4588 File Offset: 0x000A2788
		private static IWmiInstance[] CreateListenersInfo(TraceSource traceSource, IWmiInstance instance)
		{
			IWmiInstance[] array = new IWmiInstance[traceSource.Listeners.Count];
			for (int i = 0; i < traceSource.Listeners.Count; i++)
			{
				TraceListener traceListener = traceSource.Listeners[i];
				IWmiInstance wmiInstance = instance.NewInstance("TraceListener");
				wmiInstance.SetProperty("Name", traceListener.Name);
				List<IWmiInstance> list = new List<IWmiInstance>(1);
				Type type = traceListener.GetType();
				string value = (string)type.InvokeMember("initializeData", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField, null, traceListener, null, CultureInfo.InvariantCulture);
				string[] array2 = (string[])type.InvokeMember("GetSupportedAttributes", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, traceListener, null, CultureInfo.InvariantCulture);
				IWmiInstance wmiInstance2 = instance.NewInstance("TraceListenerArgument");
				wmiInstance2.SetProperty("Name", "initializeData");
				wmiInstance2.SetProperty("Value", value);
				list.Add(wmiInstance2);
				if (array2 != null)
				{
					foreach (string text in array2)
					{
						wmiInstance2 = instance.NewInstance("TraceListenerArgument");
						wmiInstance2.SetProperty("Name", text);
						wmiInstance2.SetProperty("Value", traceListener.Attributes[text]);
						list.Add(wmiInstance2);
					}
				}
				wmiInstance.SetProperty("TraceListenerArguments", list.ToArray());
				array[i] = wmiInstance;
			}
			return array;
		}

		// Token: 0x06002A8F RID: 10895 RVA: 0x000A46E8 File Offset: 0x000A28E8
		private static void FillListenersInfo(IWmiInstance instance)
		{
			TraceSource traceSource = (DiagnosticUtility.DiagnosticTrace == null) ? null : DiagnosticUtility.DiagnosticTrace.TraceSource;
			if (traceSource != null)
			{
				instance.SetProperty("ServiceModelTraceListeners", AppDomainInstanceProvider.CreateListenersInfo(traceSource, instance));
			}
			traceSource = MessageLogger.MessageTraceSource;
			if (traceSource != null)
			{
				instance.SetProperty("MessageLoggingTraceListeners", AppDomainInstanceProvider.CreateListenersInfo(traceSource, instance));
			}
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x000A473C File Offset: 0x000A293C
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		bool IWmiProvider.PutInstance(IWmiInstance instance)
		{
			bool result = false;
			if ((int)instance.GetProperty("ProcessId") == AppDomainInfo.Current.ProcessId && string.Equals((string)instance.GetProperty("Name"), AppDomainInfo.Current.Name, StringComparison.Ordinal))
			{
				try
				{
					SourceLevels sourceLevels = (SourceLevels)Enum.Parse(typeof(SourceLevels), (string)instance.GetProperty("TraceLevel"));
					if (DiagnosticUtility.Level != sourceLevels)
					{
						if (DiagnosticUtility.ShouldTraceVerbose)
						{
							TraceUtility.TraceEvent(TraceEventType.Verbose, 65537, SR.GetString("TraceCodeWmiPut"), new WmiPutTraceRecord("DiagnosticTrace.Level", DiagnosticUtility.Level, sourceLevels), instance, null);
						}
						DiagnosticUtility.Level = sourceLevels;
					}
					bool flag = (bool)instance.GetProperty("LogMalformedMessages");
					if (MessageLogger.LogMalformedMessages != flag)
					{
						if (DiagnosticUtility.ShouldTraceVerbose)
						{
							TraceUtility.TraceEvent(TraceEventType.Verbose, 65537, SR.GetString("TraceCodeWmiPut"), new WmiPutTraceRecord("MessageLogger.LogMalformedMessages", MessageLogger.LogMalformedMessages, flag), instance, null);
						}
						MessageLogger.LogMalformedMessages = flag;
					}
					bool flag2 = (bool)instance.GetProperty("LogMessagesAtServiceLevel");
					if (MessageLogger.LogMessagesAtServiceLevel != flag2)
					{
						if (DiagnosticUtility.ShouldTraceVerbose)
						{
							TraceUtility.TraceEvent(TraceEventType.Verbose, 65537, SR.GetString("TraceCodeWmiPut"), new WmiPutTraceRecord("MessageLogger.LogMessagesAtServiceLevel", MessageLogger.LogMessagesAtServiceLevel, flag2), instance, null);
						}
						MessageLogger.LogMessagesAtServiceLevel = flag2;
					}
					bool flag3 = (bool)instance.GetProperty("LogMessagesAtTransportLevel");
					if (MessageLogger.LogMessagesAtTransportLevel != flag3)
					{
						if (DiagnosticUtility.ShouldTraceVerbose)
						{
							TraceUtility.TraceEvent(TraceEventType.Verbose, 65537, SR.GetString("TraceCodeWmiPut"), new WmiPutTraceRecord("MessageLogger.LogMessagesAtTransportLevel", MessageLogger.LogMessagesAtTransportLevel, flag3), instance, null);
						}
						MessageLogger.LogMessagesAtTransportLevel = flag3;
					}
				}
				catch (ArgumentException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WbemInvalidParameterException());
				}
				result = true;
			}
			return result;
		}
	}
}
