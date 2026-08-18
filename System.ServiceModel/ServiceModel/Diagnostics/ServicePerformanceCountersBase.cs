using System;
using System.ServiceModel.Activation;
using System.ServiceModel.Administration;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A9A RID: 2714
	internal abstract class ServicePerformanceCountersBase : PerformanceCountersBase
	{
		// Token: 0x06006B40 RID: 27456 RVA: 0x0018FA78 File Offset: 0x0018DC78
		internal ServicePerformanceCountersBase(ServiceHostBase serviceHost)
		{
			this.instanceName = ServicePerformanceCountersBase.CreateFriendlyInstanceName(serviceHost);
		}

		// Token: 0x17001974 RID: 6516
		// (get) Token: 0x06006B41 RID: 27457 RVA: 0x0018FA8C File Offset: 0x0018DC8C
		internal override string InstanceName
		{
			get
			{
				return this.instanceName;
			}
		}

		// Token: 0x17001975 RID: 6517
		// (get) Token: 0x06006B42 RID: 27458 RVA: 0x0018FA94 File Offset: 0x0018DC94
		internal override string[] CounterNames
		{
			get
			{
				return ServicePerformanceCountersBase.perfCounterNames;
			}
		}

		// Token: 0x17001976 RID: 6518
		// (get) Token: 0x06006B43 RID: 27459 RVA: 0x0018FA9B File Offset: 0x0018DC9B
		internal override int PerfCounterStart
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001977 RID: 6519
		// (get) Token: 0x06006B44 RID: 27460 RVA: 0x0018FA9E File Offset: 0x0018DC9E
		internal override int PerfCounterEnd
		{
			get
			{
				return 39;
			}
		}

		// Token: 0x06006B45 RID: 27461 RVA: 0x0018FAA4 File Offset: 0x0018DCA4
		private static string GetServiceUri(ServiceHostBase serviceHost, ServiceInfo serviceInfo)
		{
			string firstAddress;
			if (!ServicePerformanceCountersBase.TryGetFullVirtualPath(serviceHost, out firstAddress))
			{
				firstAddress = serviceInfo.FirstAddress;
			}
			return firstAddress;
		}

		// Token: 0x06006B46 RID: 27462 RVA: 0x0018FAC4 File Offset: 0x0018DCC4
		private static string GetFullInstanceName(ServiceHostBase serviceHost)
		{
			ServiceInfo serviceInfo = new ServiceInfo(serviceHost);
			string serviceName = serviceInfo.ServiceName;
			string serviceUri = ServicePerformanceCountersBase.GetServiceUri(serviceHost, serviceInfo);
			return string.Format("{0}@{1}", serviceName, serviceUri);
		}

		// Token: 0x06006B47 RID: 27463 RVA: 0x0018FAF4 File Offset: 0x0018DCF4
		private static string GetShortInstanceName(ServiceHostBase serviceHost)
		{
			ServiceInfo serviceInfo = new ServiceInfo(serviceHost);
			string text = serviceInfo.ServiceName;
			string text2 = ServicePerformanceCountersBase.GetServiceUri(serviceHost, serviceInfo);
			int num = text.Length + text2.Length + 2;
			if (num > 64)
			{
				ServicePerformanceCountersBase.truncOptions compressionTasks = ServicePerformanceCountersBase.GetCompressionTasks(num, text.Length, text2.Length);
				if ((compressionTasks & ServicePerformanceCountersBase.truncOptions.service32) > ServicePerformanceCountersBase.truncOptions.NoBits)
				{
					int num2 = 32;
					text = PerformanceCountersBase.GetHashedString(text, num2 - 2, text.Length - num2 + 2, true);
				}
				if ((compressionTasks & ServicePerformanceCountersBase.truncOptions.uri31) > ServicePerformanceCountersBase.truncOptions.NoBits)
				{
					int num2 = 31;
					text2 = PerformanceCountersBase.GetHashedString(text2, 0, text2.Length - num2 + 2, false);
				}
			}
			return text + "@" + text2.Replace('/', '|');
		}

		// Token: 0x06006B48 RID: 27464 RVA: 0x0018FB9C File Offset: 0x0018DD9C
		internal static string CreateFriendlyInstanceName(ServiceHostBase serviceHost)
		{
			string shortInstanceName = ServicePerformanceCountersBase.GetShortInstanceName(serviceHost);
			if (!ServiceModelAppSettings.EnsureUniquePerformanceCounterInstanceNames)
			{
				return shortInstanceName;
			}
			string fullInstanceName = ServicePerformanceCountersBase.GetFullInstanceName(serviceHost);
			return PerformanceCountersBase.EnsureUniqueInstanceName("ServiceModelService 4.0.0.0", shortInstanceName, fullInstanceName);
		}

		// Token: 0x06006B49 RID: 27465 RVA: 0x0018FBCC File Offset: 0x0018DDCC
		internal static string GetFriendlyInstanceName(ServiceHostBase serviceHost)
		{
			string shortInstanceName = ServicePerformanceCountersBase.GetShortInstanceName(serviceHost);
			if (!ServiceModelAppSettings.EnsureUniquePerformanceCounterInstanceNames)
			{
				return shortInstanceName;
			}
			string fullInstanceName = ServicePerformanceCountersBase.GetFullInstanceName(serviceHost);
			return PerformanceCountersBase.GetUniqueInstanceName("ServiceModelService 4.0.0.0", shortInstanceName, fullInstanceName);
		}

		// Token: 0x06006B4A RID: 27466 RVA: 0x0018FBFC File Offset: 0x0018DDFC
		private static bool TryGetFullVirtualPath(ServiceHostBase serviceHost, out string uri)
		{
			VirtualPathExtension virtualPathExtension = serviceHost.Extensions.Find<VirtualPathExtension>();
			if (virtualPathExtension == null)
			{
				uri = null;
				return false;
			}
			uri = virtualPathExtension.ApplicationVirtualPath + virtualPathExtension.VirtualPath.ToString().Replace("~", "");
			return uri != null;
		}

		// Token: 0x06006B4B RID: 27467 RVA: 0x0018FC4C File Offset: 0x0018DE4C
		private static ServicePerformanceCountersBase.truncOptions GetCompressionTasks(int totalLen, int serviceLen, int uriLen)
		{
			ServicePerformanceCountersBase.truncOptions truncOptions = ServicePerformanceCountersBase.truncOptions.NoBits;
			if (totalLen > 64)
			{
				int num = totalLen;
				if (num > 64 && serviceLen > 32)
				{
					truncOptions |= ServicePerformanceCountersBase.truncOptions.service32;
					num -= serviceLen - 32;
				}
				if (num > 64 && uriLen > 31)
				{
					truncOptions |= ServicePerformanceCountersBase.truncOptions.uri31;
				}
			}
			return truncOptions;
		}

		// Token: 0x06006B4C RID: 27468
		internal abstract void MethodCalled();

		// Token: 0x06006B4D RID: 27469
		internal abstract void MethodReturnedSuccess();

		// Token: 0x06006B4E RID: 27470
		internal abstract void MethodReturnedError();

		// Token: 0x06006B4F RID: 27471
		internal abstract void MethodReturnedFault();

		// Token: 0x06006B50 RID: 27472
		internal abstract void SaveCallDuration(long time);

		// Token: 0x06006B51 RID: 27473
		internal abstract void AuthenticationFailed();

		// Token: 0x06006B52 RID: 27474
		internal abstract void AuthorizationFailed();

		// Token: 0x06006B53 RID: 27475
		internal abstract void ServiceInstanceCreated();

		// Token: 0x06006B54 RID: 27476
		internal abstract void ServiceInstanceRemoved();

		// Token: 0x06006B55 RID: 27477
		internal abstract void SessionFaulted();

		// Token: 0x06006B56 RID: 27478
		internal abstract void MessageDropped();

		// Token: 0x06006B57 RID: 27479
		internal abstract void TxCommitted(long count);

		// Token: 0x06006B58 RID: 27480
		internal abstract void TxInDoubt(long count);

		// Token: 0x06006B59 RID: 27481
		internal abstract void TxAborted(long count);

		// Token: 0x06006B5A RID: 27482
		internal abstract void TxFlowed();

		// Token: 0x06006B5B RID: 27483
		internal abstract void MsmqDroppedMessage();

		// Token: 0x06006B5C RID: 27484
		internal abstract void MsmqPoisonMessage();

		// Token: 0x06006B5D RID: 27485
		internal abstract void MsmqRejectedMessage();

		// Token: 0x06006B5E RID: 27486
		internal abstract void IncrementThrottlePercent(int counterIndex);

		// Token: 0x06006B5F RID: 27487
		internal abstract void SetThrottleBase(int counterIndex, long denominator);

		// Token: 0x06006B60 RID: 27488
		internal abstract void DecrementThrottlePercent(int counterIndex);

		// Token: 0x04003CE5 RID: 15589
		private string instanceName;

		// Token: 0x04003CE6 RID: 15590
		protected static readonly string[] perfCounterNames = new string[]
		{
			"Calls",
			"Calls Per Second",
			"Calls Outstanding",
			"Calls Failed",
			"Calls Failed Per Second",
			"Calls Faulted",
			"Calls Faulted Per Second",
			"Calls Duration",
			"Calls Duration Base",
			"Security Validation and Authentication Failures",
			"Security Validation and Authentication Failures Per Second",
			"Security Calls Not Authorized",
			"Security Calls Not Authorized Per Second",
			"Instances",
			"Instances Created Per Second",
			"Reliable Messaging Sessions Faulted",
			"Reliable Messaging Sessions Faulted Per Second",
			"Reliable Messaging Messages Dropped",
			"Reliable Messaging Messages Dropped Per Second",
			"Transactions Flowed",
			"Transactions Flowed Per Second",
			"Transacted Operations Committed",
			"Transacted Operations Committed Per Second",
			"Transacted Operations Aborted",
			"Transacted Operations Aborted Per Second",
			"Transacted Operations In Doubt",
			"Transacted Operations In Doubt Per Second",
			"Queued Poison Messages",
			"Queued Poison Messages Per Second",
			"Queued Messages Rejected",
			"Queued Messages Rejected Per Second",
			"Queued Messages Dropped",
			"Queued Messages Dropped Per Second",
			"Percent Of Max Concurrent Calls",
			"Percent Of Max Concurrent Calls Base",
			"Percent Of Max Concurrent Instances",
			"Percent Of Max Concurrent Instances Base",
			"Percent Of Max Concurrent Sessions",
			"Percent Of Max Concurrent Sessions Base"
		};

		// Token: 0x04003CE7 RID: 15591
		private const int maxCounterLength = 64;

		// Token: 0x04003CE8 RID: 15592
		private const int hashLength = 2;

		// Token: 0x02000EC0 RID: 3776
		internal enum PerfCounters
		{
			// Token: 0x04004C7A RID: 19578
			Calls,
			// Token: 0x04004C7B RID: 19579
			CallsPerSecond,
			// Token: 0x04004C7C RID: 19580
			CallsOutstanding,
			// Token: 0x04004C7D RID: 19581
			CallsFailed,
			// Token: 0x04004C7E RID: 19582
			CallsFailedPerSecond,
			// Token: 0x04004C7F RID: 19583
			CallsFaulted,
			// Token: 0x04004C80 RID: 19584
			CallsFaultedPerSecond,
			// Token: 0x04004C81 RID: 19585
			CallDuration,
			// Token: 0x04004C82 RID: 19586
			CallDurationBase,
			// Token: 0x04004C83 RID: 19587
			SecurityValidationAuthenticationFailures,
			// Token: 0x04004C84 RID: 19588
			SecurityValidationAuthenticationFailuresPerSecond,
			// Token: 0x04004C85 RID: 19589
			CallsNotAuthorized,
			// Token: 0x04004C86 RID: 19590
			CallsNotAuthorizedPerSecond,
			// Token: 0x04004C87 RID: 19591
			Instances,
			// Token: 0x04004C88 RID: 19592
			InstancesRate,
			// Token: 0x04004C89 RID: 19593
			RMSessionsFaulted,
			// Token: 0x04004C8A RID: 19594
			RMSessionsFaultedPerSecond,
			// Token: 0x04004C8B RID: 19595
			RMMessagesDropped,
			// Token: 0x04004C8C RID: 19596
			RMMessagesDroppedPerSecond,
			// Token: 0x04004C8D RID: 19597
			TxFlowed,
			// Token: 0x04004C8E RID: 19598
			TxFlowedPerSecond,
			// Token: 0x04004C8F RID: 19599
			TxCommitted,
			// Token: 0x04004C90 RID: 19600
			TxCommittedPerSecond,
			// Token: 0x04004C91 RID: 19601
			TxAborted,
			// Token: 0x04004C92 RID: 19602
			TxAbortedPerSecond,
			// Token: 0x04004C93 RID: 19603
			TxInDoubt,
			// Token: 0x04004C94 RID: 19604
			TxInDoubtPerSecond,
			// Token: 0x04004C95 RID: 19605
			MsmqPoisonMessages,
			// Token: 0x04004C96 RID: 19606
			MsmqPoisonMessagesPerSecond,
			// Token: 0x04004C97 RID: 19607
			MsmqRejectedMessages,
			// Token: 0x04004C98 RID: 19608
			MsmqRejectedMessagesPerSecond,
			// Token: 0x04004C99 RID: 19609
			MsmqDroppedMessages,
			// Token: 0x04004C9A RID: 19610
			MsmqDroppedMessagesPerSecond,
			// Token: 0x04004C9B RID: 19611
			CallsPercentMaxCalls,
			// Token: 0x04004C9C RID: 19612
			CallsPercentMaxCallsBase,
			// Token: 0x04004C9D RID: 19613
			InstancesPercentMaxInstances,
			// Token: 0x04004C9E RID: 19614
			InstancesPercentMaxInstancesBase,
			// Token: 0x04004C9F RID: 19615
			SessionsPercentMaxSessions,
			// Token: 0x04004CA0 RID: 19616
			SessionsPercentMaxSessionsBase,
			// Token: 0x04004CA1 RID: 19617
			TotalCounters
		}

		// Token: 0x02000EC1 RID: 3777
		[Flags]
		private enum truncOptions : uint
		{
			// Token: 0x04004CA3 RID: 19619
			NoBits = 0U,
			// Token: 0x04004CA4 RID: 19620
			service32 = 1U,
			// Token: 0x04004CA5 RID: 19621
			uri31 = 4U
		}
	}
}
