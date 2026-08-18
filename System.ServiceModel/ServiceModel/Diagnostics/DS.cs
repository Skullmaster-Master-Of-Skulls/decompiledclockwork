using System;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A75 RID: 2677
	internal static class DS
	{
		// Token: 0x06006970 RID: 26992 RVA: 0x00189770 File Offset: 0x00187970
		public static bool MessageInspectorIsEnabled()
		{
			return DS.s_dsb.IsEnabled((EventKeywords)1L);
		}

		// Token: 0x06006971 RID: 26993 RVA: 0x0018977E File Offset: 0x0018797E
		public static void DispatchMessageInspectorAfterReceive(Type inspectorType, TimeSpan duration)
		{
			DS.s_dsb.DispatchMessageInspectorAfterReceive(inspectorType.FullName, duration.Ticks);
		}

		// Token: 0x06006972 RID: 26994 RVA: 0x00189797 File Offset: 0x00187997
		public static void DispatchMessageInspectorBeforeSend(Type inspectorType, TimeSpan duration)
		{
			DS.s_dsb.DispatchMessageInspectorBeforeSend(inspectorType.FullName, duration.Ticks);
		}

		// Token: 0x06006973 RID: 26995 RVA: 0x001897B0 File Offset: 0x001879B0
		public static void ClientMessageInspectorAfterReceive(Type inspectorType, TimeSpan duration)
		{
			DS.s_dsb.ClientMessageInspectorAfterReceive(inspectorType.FullName, duration.Ticks);
		}

		// Token: 0x06006974 RID: 26996 RVA: 0x001897C9 File Offset: 0x001879C9
		public static void ClientMessageInspectorBeforeSend(Type inspectorType, TimeSpan duration)
		{
			DS.s_dsb.ClientMessageInspectorBeforeSend(inspectorType.FullName, duration.Ticks);
		}

		// Token: 0x06006975 RID: 26997 RVA: 0x001897E2 File Offset: 0x001879E2
		public static bool ParameterInspectorIsEnabled()
		{
			return DS.s_dsb.IsEnabled((EventKeywords)2L);
		}

		// Token: 0x06006976 RID: 26998 RVA: 0x001897F0 File Offset: 0x001879F0
		public static void ParameterInspectorAfter(Type inspectorType, TimeSpan duration)
		{
			DS.s_dsb.ParameterInspectorAfter(inspectorType.FullName, duration.Ticks);
		}

		// Token: 0x06006977 RID: 26999 RVA: 0x00189809 File Offset: 0x00187A09
		public static void ParameterInspectorBefore(Type inspectorType, TimeSpan duration)
		{
			DS.s_dsb.ParameterInspectorBefore(inspectorType.FullName, duration.Ticks);
		}

		// Token: 0x06006978 RID: 27000 RVA: 0x00189822 File Offset: 0x00187A22
		public static bool MessageFormatterIsEnabled()
		{
			return DS.s_dsb.IsEnabled((EventKeywords)4L);
		}

		// Token: 0x06006979 RID: 27001 RVA: 0x00189830 File Offset: 0x00187A30
		public static void DispatchMessageFormatterDeserialize(Type formatterType, TimeSpan duration)
		{
			DS.s_dsb.DispatchMessageFormatterDeserialize(formatterType.FullName, duration.Ticks);
		}

		// Token: 0x0600697A RID: 27002 RVA: 0x00189849 File Offset: 0x00187A49
		public static void DispatchMessageFormatterSerialize(Type formatterType, TimeSpan duration)
		{
			DS.s_dsb.DispatchMessageFormatterSerialize(formatterType.FullName, duration.Ticks);
		}

		// Token: 0x0600697B RID: 27003 RVA: 0x00189862 File Offset: 0x00187A62
		public static void ClientMessageFormatterDeserialize(Type formatterType, TimeSpan duration)
		{
			DS.s_dsb.ClientMessageFormatterDeserialize(formatterType.FullName, duration.Ticks);
		}

		// Token: 0x0600697C RID: 27004 RVA: 0x0018987B File Offset: 0x00187A7B
		public static void ClientMessageFormatterSerialize(Type formatterType, TimeSpan duration)
		{
			DS.s_dsb.ClientMessageFormatterSerialize(formatterType.FullName, duration.Ticks);
		}

		// Token: 0x0600697D RID: 27005 RVA: 0x00189894 File Offset: 0x00187A94
		public static bool OperationSelectorIsEnabled()
		{
			return DS.s_dsb.IsEnabled((EventKeywords)8L);
		}

		// Token: 0x0600697E RID: 27006 RVA: 0x001898A2 File Offset: 0x00187AA2
		public static void DispatchSelectOperation(Type selectorType, string selectedOperation, TimeSpan duration)
		{
			DS.s_dsb.DispatchSelectOperation(selectorType.FullName, selectedOperation, duration.Ticks);
		}

		// Token: 0x0600697F RID: 27007 RVA: 0x001898BC File Offset: 0x00187ABC
		public static void ClientSelectOperation(Type formatterType, string selectedOperation, TimeSpan duration)
		{
			DS.s_dsb.ClientSelectOperation(formatterType.FullName, selectedOperation, duration.Ticks);
		}

		// Token: 0x06006980 RID: 27008 RVA: 0x001898D6 File Offset: 0x00187AD6
		public static bool OperationInvokerIsEnabled()
		{
			return DS.s_dsb.IsEnabled((EventKeywords)16L);
		}

		// Token: 0x06006981 RID: 27009 RVA: 0x001898E5 File Offset: 0x00187AE5
		public static void InvokeOperationStart(Type invokerType, long timestamp)
		{
			DS.s_dsb.InvokeOperationStart(invokerType.FullName, timestamp);
		}

		// Token: 0x06006982 RID: 27010 RVA: 0x001898F8 File Offset: 0x00187AF8
		public static void InvokeOperationStop(long timestamp)
		{
			DS.s_dsb.InvokeOperationStop(timestamp);
		}

		// Token: 0x06006983 RID: 27011 RVA: 0x00189905 File Offset: 0x00187B05
		public static bool InstanceProviderIsEnabled()
		{
			return DS.s_dsb.IsEnabled((EventKeywords)32L);
		}

		// Token: 0x06006984 RID: 27012 RVA: 0x00189914 File Offset: 0x00187B14
		public static void InstanceProviderGet(Type providerType, object instance, TimeSpan duration)
		{
			DS.s_dsb.InstanceProviderGet(providerType.FullName, RuntimeHelpers.GetHashCode(instance), duration.Ticks);
		}

		// Token: 0x06006985 RID: 27013 RVA: 0x00189933 File Offset: 0x00187B33
		public static void InstanceProviderRelease(Type providerType, object instance, TimeSpan duration)
		{
			DS.s_dsb.InstanceProviderRelease(providerType.FullName, RuntimeHelpers.GetHashCode(instance), duration.Ticks);
		}

		// Token: 0x06006986 RID: 27014 RVA: 0x00189952 File Offset: 0x00187B52
		public static bool ServiceThrottleIsEnabled()
		{
			return DS.s_dsb.IsEnabled((EventKeywords)64L);
		}

		// Token: 0x06006987 RID: 27015 RVA: 0x00189961 File Offset: 0x00187B61
		public static void CallThrottleWaiting(Message requestMessage)
		{
			requestMessage.SetProperty("System.ServiceModel.Diagnostics.DiagnosticSourceBridge.CallThrottleWaitTimestamp", Stopwatch.GetTimestamp());
		}

		// Token: 0x06006988 RID: 27016 RVA: 0x00189978 File Offset: 0x00187B78
		internal static void CallThrottleAcquired(Message requestMessage)
		{
			requestMessage.SetProperty("System.ServiceModel.Diagnostics.DiagnosticSourceBridge.CallThrottleAcquiredTimestamp", Stopwatch.GetTimestamp());
		}

		// Token: 0x06006989 RID: 27017 RVA: 0x00189990 File Offset: 0x00187B90
		public static void Throttled(Message requestMessage)
		{
			bool flag = false;
			object obj;
			object obj2;
			if (requestMessage.GetProperty("System.ServiceModel.Diagnostics.DiagnosticSourceBridge.CallThrottleWaitTimestamp", out obj) && requestMessage.GetProperty("System.ServiceModel.Diagnostics.DiagnosticSourceBridge.CallThrottleAcquiredTimestamp", out obj2))
			{
				flag = true;
				long num = (long)obj2 - (long)obj;
				if (num < 0L)
				{
					num = 0L;
				}
				DS.s_dsb.CallThrottled(num);
			}
			if (requestMessage.GetProperty("System.ServiceModel.Diagnostics.DiagnosticSourceBridge.InstanceThrottleWaitTimestamp", out obj) && requestMessage.GetProperty("System.ServiceModel.Diagnostics.DiagnosticSourceBridge.InstanceThrottleAcquiredTimestamp", out obj2))
			{
				flag = true;
				long num2 = (long)obj2 - (long)obj;
				if (num2 < 0L)
				{
					num2 = 0L;
				}
				DS.s_dsb.InstanceThrottled(num2);
			}
			if (flag)
			{
				requestMessage.Properties.Remove("System.ServiceModel.Diagnostics.DiagnosticSourceBridge.CallThrottleWaitTimestamp");
				requestMessage.Properties.Remove("System.ServiceModel.Diagnostics.DiagnosticSourceBridge.CallThrottleAcquiredTimestamp");
				requestMessage.Properties.Remove("System.ServiceModel.Diagnostics.DiagnosticSourceBridge.InstanceThrottleWaitTimestamp");
				requestMessage.Properties.Remove("System.ServiceModel.Diagnostics.DiagnosticSourceBridge.InstanceThrottleAcquiredTimestamp");
			}
		}

		// Token: 0x0600698A RID: 27018 RVA: 0x00189A6C File Offset: 0x00187C6C
		public static void InstanceThrottleWaiting(Message requestMessage)
		{
			if (requestMessage != null)
			{
				requestMessage.SetProperty("System.ServiceModel.Diagnostics.DiagnosticSourceBridge.InstanceThrottleWaitTimestamp", Stopwatch.GetTimestamp());
			}
		}

		// Token: 0x0600698B RID: 27019 RVA: 0x00189A86 File Offset: 0x00187C86
		internal static void InstanceThrottleAcquired(Message requestMessage)
		{
			if (requestMessage != null)
			{
				requestMessage.SetProperty("System.ServiceModel.Diagnostics.DiagnosticSourceBridge.InstanceThrottleAcquiredTimestamp", Stopwatch.GetTimestamp());
			}
		}

		// Token: 0x0600698C RID: 27020 RVA: 0x00189AA0 File Offset: 0x00187CA0
		public static void InstanceThrottled(Message requestMessage)
		{
			object obj;
			object obj2;
			if (requestMessage.GetProperty("System.ServiceModel.Diagnostics.DiagnosticSourceBridge.InstanceThrottleWaitTimestamp", out obj) && requestMessage.GetProperty("System.ServiceModel.Diagnostics.DiagnosticSourceBridge.InstanceThrottleAcquiredTimestamp", out obj2))
			{
				long num = (long)obj2 - (long)obj;
				if (num < 0L)
				{
					num = 0L;
				}
				DS.s_dsb.InstanceThrottled(num);
			}
			requestMessage.Properties.Remove("System.ServiceModel.Diagnostics.DiagnosticSourceBridge.InstanceThrottleWaitTimestamp");
			requestMessage.Properties.Remove("System.ServiceModel.Diagnostics.DiagnosticSourceBridge.InstanceThrottleAcquiredTimestamp");
		}

		// Token: 0x0600698D RID: 27021 RVA: 0x00189B0E File Offset: 0x00187D0E
		public static bool AuthenticationIsEnabled()
		{
			return DS.s_dsb.IsEnabled((EventKeywords)128L);
		}

		// Token: 0x0600698E RID: 27022 RVA: 0x00189B20 File Offset: 0x00187D20
		internal static void Authentication(Type authenticationManagerType, bool authenticated, TimeSpan duration)
		{
			DS.s_dsb.Authentication(authenticationManagerType.FullName, authenticated, duration.Ticks);
		}

		// Token: 0x0600698F RID: 27023 RVA: 0x00189B3A File Offset: 0x00187D3A
		public static bool AuthorizationIsEnabled()
		{
			return DS.s_dsb.IsEnabled((EventKeywords)256L);
		}

		// Token: 0x06006990 RID: 27024 RVA: 0x00189B4C File Offset: 0x00187D4C
		internal static void Authorization(Type authorizationManagerType, bool authorized, TimeSpan duration)
		{
			DS.s_dsb.Authentication(authorizationManagerType.FullName, authorized, duration.Ticks);
		}

		// Token: 0x04003C4E RID: 15438
		private static readonly DiagnosticSourceBridge s_dsb = new DiagnosticSourceBridge();

		// Token: 0x04003C4F RID: 15439
		private const string CallThrottleWaitTimestampPropertyName = "System.ServiceModel.Diagnostics.DiagnosticSourceBridge.CallThrottleWaitTimestamp";

		// Token: 0x04003C50 RID: 15440
		private const string CallThrottleAcquiredTimestampPropertyName = "System.ServiceModel.Diagnostics.DiagnosticSourceBridge.CallThrottleAcquiredTimestamp";

		// Token: 0x04003C51 RID: 15441
		private const string InstanceThrottleWaitTimestampPropertyName = "System.ServiceModel.Diagnostics.DiagnosticSourceBridge.InstanceThrottleWaitTimestamp";

		// Token: 0x04003C52 RID: 15442
		private const string InstanceThrottleAcquiredTimestampPropertyName = "System.ServiceModel.Diagnostics.DiagnosticSourceBridge.InstanceThrottleAcquiredTimestamp";
	}
}
