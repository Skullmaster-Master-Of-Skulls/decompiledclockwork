using System;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A77 RID: 2679
	internal abstract class EndpointPerformanceCountersBase : PerformanceCountersBase
	{
		// Token: 0x060069A8 RID: 27048 RVA: 0x00189D05 File Offset: 0x00187F05
		internal EndpointPerformanceCountersBase(string service, string contract, string uri)
		{
			this.instanceName = EndpointPerformanceCountersBase.CreateFriendlyInstanceName(service, contract, uri);
		}

		// Token: 0x060069A9 RID: 27049 RVA: 0x00189D1B File Offset: 0x00187F1B
		private static string GetFullInstanceName(string service, string contract, string uri)
		{
			return string.Format("{0}.{1}@{2}", service, contract, uri);
		}

		// Token: 0x060069AA RID: 27050 RVA: 0x00189D2C File Offset: 0x00187F2C
		private static string GetShortInstanceName(string service, string contract, string uri)
		{
			int num = service.Length + contract.Length + uri.Length + 2;
			if (num > 64)
			{
				EndpointPerformanceCountersBase.truncOptions compressionTasks = EndpointPerformanceCountersBase.GetCompressionTasks(num, service.Length, contract.Length, uri.Length);
				if ((compressionTasks & EndpointPerformanceCountersBase.truncOptions.service15) > EndpointPerformanceCountersBase.truncOptions.NoBits)
				{
					int num2 = 15;
					service = PerformanceCountersBase.GetHashedString(service, num2 - 2, service.Length - num2 + 2, true);
				}
				if ((compressionTasks & EndpointPerformanceCountersBase.truncOptions.contract16) > EndpointPerformanceCountersBase.truncOptions.NoBits)
				{
					int num2 = 16;
					contract = PerformanceCountersBase.GetHashedString(contract, num2 - 2, contract.Length - num2 + 2, true);
				}
				if ((compressionTasks & EndpointPerformanceCountersBase.truncOptions.uri31) > EndpointPerformanceCountersBase.truncOptions.NoBits)
				{
					int num2 = 31;
					uri = PerformanceCountersBase.GetHashedString(uri, 0, uri.Length - num2 + 2, false);
				}
			}
			return string.Concat(new string[]
			{
				service,
				".",
				contract,
				"@",
				uri.Replace('/', '|')
			});
		}

		// Token: 0x060069AB RID: 27051 RVA: 0x00189DFC File Offset: 0x00187FFC
		internal static string CreateFriendlyInstanceName(string service, string contract, string uri)
		{
			string shortInstanceName = EndpointPerformanceCountersBase.GetShortInstanceName(service, contract, uri);
			if (!ServiceModelAppSettings.EnsureUniquePerformanceCounterInstanceNames)
			{
				return shortInstanceName;
			}
			string fullInstanceName = EndpointPerformanceCountersBase.GetFullInstanceName(service, contract, uri);
			return PerformanceCountersBase.EnsureUniqueInstanceName("ServiceModelEndpoint 4.0.0.0", shortInstanceName, fullInstanceName);
		}

		// Token: 0x060069AC RID: 27052 RVA: 0x00189E30 File Offset: 0x00188030
		internal static string GetFriendlyInstanceName(string service, string contract, string uri)
		{
			string shortInstanceName = EndpointPerformanceCountersBase.GetShortInstanceName(service, contract, uri);
			if (!ServiceModelAppSettings.EnsureUniquePerformanceCounterInstanceNames)
			{
				return shortInstanceName;
			}
			string fullInstanceName = EndpointPerformanceCountersBase.GetFullInstanceName(service, contract, uri);
			return PerformanceCountersBase.GetUniqueInstanceName("ServiceModelEndpoint 4.0.0.0", shortInstanceName, fullInstanceName);
		}

		// Token: 0x060069AD RID: 27053 RVA: 0x00189E64 File Offset: 0x00188064
		private static EndpointPerformanceCountersBase.truncOptions GetCompressionTasks(int totalLen, int serviceLen, int contractLen, int uriLen)
		{
			EndpointPerformanceCountersBase.truncOptions truncOptions = EndpointPerformanceCountersBase.truncOptions.NoBits;
			if (totalLen > 64)
			{
				int num = totalLen;
				if (num > 64 && serviceLen > 15)
				{
					truncOptions |= EndpointPerformanceCountersBase.truncOptions.service15;
					num -= serviceLen - 15;
				}
				if (num > 64 && contractLen > 16)
				{
					truncOptions |= EndpointPerformanceCountersBase.truncOptions.contract16;
					num -= contractLen - 16;
				}
				if (num > 64 && uriLen > 31)
				{
					truncOptions |= EndpointPerformanceCountersBase.truncOptions.uri31;
				}
			}
			return truncOptions;
		}

		// Token: 0x1700192F RID: 6447
		// (get) Token: 0x060069AE RID: 27054 RVA: 0x00189EB3 File Offset: 0x001880B3
		internal override string InstanceName
		{
			get
			{
				return this.instanceName;
			}
		}

		// Token: 0x17001930 RID: 6448
		// (get) Token: 0x060069AF RID: 27055 RVA: 0x00189EBB File Offset: 0x001880BB
		internal override string[] CounterNames
		{
			get
			{
				return EndpointPerformanceCountersBase.perfCounterNames;
			}
		}

		// Token: 0x17001931 RID: 6449
		// (get) Token: 0x060069B0 RID: 27056 RVA: 0x00189EC2 File Offset: 0x001880C2
		internal override int PerfCounterStart
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001932 RID: 6450
		// (get) Token: 0x060069B1 RID: 27057 RVA: 0x00189EC5 File Offset: 0x001880C5
		internal override int PerfCounterEnd
		{
			get
			{
				return 19;
			}
		}

		// Token: 0x060069B2 RID: 27058
		internal abstract void MethodCalled();

		// Token: 0x060069B3 RID: 27059
		internal abstract void MethodReturnedSuccess();

		// Token: 0x060069B4 RID: 27060
		internal abstract void MethodReturnedError();

		// Token: 0x060069B5 RID: 27061
		internal abstract void MethodReturnedFault();

		// Token: 0x060069B6 RID: 27062
		internal abstract void SaveCallDuration(long time);

		// Token: 0x060069B7 RID: 27063
		internal abstract void AuthenticationFailed();

		// Token: 0x060069B8 RID: 27064
		internal abstract void AuthorizationFailed();

		// Token: 0x060069B9 RID: 27065
		internal abstract void SessionFaulted();

		// Token: 0x060069BA RID: 27066
		internal abstract void MessageDropped();

		// Token: 0x060069BB RID: 27067
		internal abstract void TxFlowed();

		// Token: 0x04003C53 RID: 15443
		protected string instanceName;

		// Token: 0x04003C54 RID: 15444
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
			"Reliable Messaging Sessions Faulted",
			"Reliable Messaging Sessions Faulted Per Second",
			"Reliable Messaging Messages Dropped",
			"Reliable Messaging Messages Dropped Per Second",
			"Transactions Flowed",
			"Transactions Flowed Per Second"
		};

		// Token: 0x04003C55 RID: 15445
		private const int maxCounterLength = 64;

		// Token: 0x04003C56 RID: 15446
		private const int hashLength = 2;

		// Token: 0x02000EA5 RID: 3749
		protected enum PerfCounters
		{
			// Token: 0x04004C09 RID: 19465
			Calls,
			// Token: 0x04004C0A RID: 19466
			CallsPerSecond,
			// Token: 0x04004C0B RID: 19467
			CallsOutstanding,
			// Token: 0x04004C0C RID: 19468
			CallsFailed,
			// Token: 0x04004C0D RID: 19469
			CallsFailedPerSecond,
			// Token: 0x04004C0E RID: 19470
			CallsFaulted,
			// Token: 0x04004C0F RID: 19471
			CallsFaultedPerSecond,
			// Token: 0x04004C10 RID: 19472
			CallDuration,
			// Token: 0x04004C11 RID: 19473
			CallDurationBase,
			// Token: 0x04004C12 RID: 19474
			SecurityValidationAuthenticationFailures,
			// Token: 0x04004C13 RID: 19475
			SecurityValidationAuthenticationFailuresPerSecond,
			// Token: 0x04004C14 RID: 19476
			CallsNotAuthorized,
			// Token: 0x04004C15 RID: 19477
			CallsNotAuthorizedPerSecond,
			// Token: 0x04004C16 RID: 19478
			RMSessionsFaulted,
			// Token: 0x04004C17 RID: 19479
			RMSessionsFaultedPerSecond,
			// Token: 0x04004C18 RID: 19480
			RMMessagesDropped,
			// Token: 0x04004C19 RID: 19481
			RMMessagesDroppedPerSecond,
			// Token: 0x04004C1A RID: 19482
			TxFlowed,
			// Token: 0x04004C1B RID: 19483
			TxFlowedPerSecond,
			// Token: 0x04004C1C RID: 19484
			TotalCounters
		}

		// Token: 0x02000EA6 RID: 3750
		[Flags]
		private enum truncOptions : uint
		{
			// Token: 0x04004C1E RID: 19486
			NoBits = 0U,
			// Token: 0x04004C1F RID: 19487
			service15 = 1U,
			// Token: 0x04004C20 RID: 19488
			contract16 = 2U,
			// Token: 0x04004C21 RID: 19489
			uri31 = 4U
		}
	}
}
