using System;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A86 RID: 2694
	internal abstract class OperationPerformanceCountersBase : PerformanceCountersBase
	{
		// Token: 0x06006A45 RID: 27205 RVA: 0x0018C52E File Offset: 0x0018A72E
		internal OperationPerformanceCountersBase(string service, string contract, string operationName, string uri)
		{
			this.operationName = operationName;
			this.instanceName = OperationPerformanceCountersBase.CreateFriendlyInstanceName(service, contract, operationName, uri);
		}

		// Token: 0x06006A46 RID: 27206 RVA: 0x0018C54D File Offset: 0x0018A74D
		private static string GetFullInstanceName(string service, string contract, string operation, string uri)
		{
			return string.Format("{0}.{1}.{2}@{3}", new object[]
			{
				service,
				contract,
				operation,
				uri
			});
		}

		// Token: 0x06006A47 RID: 27207 RVA: 0x0018C570 File Offset: 0x0018A770
		private static string GetShortInstanceName(string service, string contract, string operation, string uri)
		{
			int num = service.Length + contract.Length + operation.Length + uri.Length + 3;
			if (num > 64)
			{
				OperationPerformanceCountersBase.truncOptions compressionTasks = OperationPerformanceCountersBase.GetCompressionTasks(num, service.Length, contract.Length, operation.Length, uri.Length);
				if ((compressionTasks & OperationPerformanceCountersBase.truncOptions.service7) > OperationPerformanceCountersBase.truncOptions.NoBits)
				{
					int num2 = 7;
					service = PerformanceCountersBase.GetHashedString(service, num2 - 2, service.Length - num2 + 2, true);
				}
				if ((compressionTasks & OperationPerformanceCountersBase.truncOptions.contract7) > OperationPerformanceCountersBase.truncOptions.NoBits)
				{
					int num2 = 7;
					contract = PerformanceCountersBase.GetHashedString(contract, num2 - 2, contract.Length - num2 + 2, true);
				}
				if ((compressionTasks & OperationPerformanceCountersBase.truncOptions.operation15) > OperationPerformanceCountersBase.truncOptions.NoBits)
				{
					int num2 = 15;
					operation = PerformanceCountersBase.GetHashedString(operation, num2 - 2, operation.Length - num2 + 2, true);
				}
				if ((compressionTasks & OperationPerformanceCountersBase.truncOptions.uri32) > OperationPerformanceCountersBase.truncOptions.NoBits)
				{
					int num2 = 32;
					uri = PerformanceCountersBase.GetHashedString(uri, 0, uri.Length - num2 + 2, false);
				}
			}
			return string.Concat(new string[]
			{
				service,
				".",
				contract,
				".",
				operation,
				"@",
				uri.Replace('/', '|')
			});
		}

		// Token: 0x06006A48 RID: 27208 RVA: 0x0018C678 File Offset: 0x0018A878
		internal static string CreateFriendlyInstanceName(string service, string contract, string operation, string uri)
		{
			string shortInstanceName = OperationPerformanceCountersBase.GetShortInstanceName(service, contract, operation, uri);
			if (!ServiceModelAppSettings.EnsureUniquePerformanceCounterInstanceNames)
			{
				return shortInstanceName;
			}
			string fullInstanceName = OperationPerformanceCountersBase.GetFullInstanceName(service, contract, operation, uri);
			return PerformanceCountersBase.EnsureUniqueInstanceName("ServiceModelOperation 4.0.0.0", shortInstanceName, fullInstanceName);
		}

		// Token: 0x06006A49 RID: 27209 RVA: 0x0018C6B0 File Offset: 0x0018A8B0
		internal static string GetFriendlyInstanceName(string service, string contract, string operation, string uri)
		{
			string shortInstanceName = OperationPerformanceCountersBase.GetShortInstanceName(service, contract, operation, uri);
			if (!ServiceModelAppSettings.EnsureUniquePerformanceCounterInstanceNames)
			{
				return shortInstanceName;
			}
			string fullInstanceName = OperationPerformanceCountersBase.GetFullInstanceName(service, contract, operation, uri);
			return PerformanceCountersBase.GetUniqueInstanceName("ServiceModelOperation 4.0.0.0", shortInstanceName, fullInstanceName);
		}

		// Token: 0x06006A4A RID: 27210 RVA: 0x0018C6E8 File Offset: 0x0018A8E8
		private static OperationPerformanceCountersBase.truncOptions GetCompressionTasks(int totalLen, int serviceLen, int contractLen, int operationLen, int uriLen)
		{
			OperationPerformanceCountersBase.truncOptions truncOptions = OperationPerformanceCountersBase.truncOptions.NoBits;
			if (totalLen > 64)
			{
				int num = totalLen;
				if (num > 64 && serviceLen > 8)
				{
					truncOptions |= OperationPerformanceCountersBase.truncOptions.service7;
					num -= serviceLen - 7;
				}
				if (num > 64 && contractLen > 7)
				{
					truncOptions |= OperationPerformanceCountersBase.truncOptions.contract7;
					num -= contractLen - 7;
				}
				if (num > 64 && operationLen > 15)
				{
					truncOptions |= OperationPerformanceCountersBase.truncOptions.operation15;
					num -= operationLen - 15;
				}
				if (num > 64 && uriLen > 32)
				{
					truncOptions |= OperationPerformanceCountersBase.truncOptions.uri32;
				}
			}
			return truncOptions;
		}

		// Token: 0x17001954 RID: 6484
		// (get) Token: 0x06006A4B RID: 27211 RVA: 0x0018C749 File Offset: 0x0018A949
		internal override string InstanceName
		{
			get
			{
				return this.instanceName;
			}
		}

		// Token: 0x17001955 RID: 6485
		// (get) Token: 0x06006A4C RID: 27212 RVA: 0x0018C751 File Offset: 0x0018A951
		internal string OperationName
		{
			get
			{
				return this.operationName;
			}
		}

		// Token: 0x17001956 RID: 6486
		// (get) Token: 0x06006A4D RID: 27213 RVA: 0x0018C759 File Offset: 0x0018A959
		internal override string[] CounterNames
		{
			get
			{
				return OperationPerformanceCountersBase.perfCounterNames;
			}
		}

		// Token: 0x17001957 RID: 6487
		// (get) Token: 0x06006A4E RID: 27214 RVA: 0x0018C760 File Offset: 0x0018A960
		internal override int PerfCounterStart
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001958 RID: 6488
		// (get) Token: 0x06006A4F RID: 27215 RVA: 0x0018C763 File Offset: 0x0018A963
		internal override int PerfCounterEnd
		{
			get
			{
				return 15;
			}
		}

		// Token: 0x06006A50 RID: 27216
		internal abstract void MethodCalled();

		// Token: 0x06006A51 RID: 27217
		internal abstract void MethodReturnedSuccess();

		// Token: 0x06006A52 RID: 27218
		internal abstract void MethodReturnedError();

		// Token: 0x06006A53 RID: 27219
		internal abstract void MethodReturnedFault();

		// Token: 0x06006A54 RID: 27220
		internal abstract void SaveCallDuration(long time);

		// Token: 0x06006A55 RID: 27221
		internal abstract void AuthenticationFailed();

		// Token: 0x06006A56 RID: 27222
		internal abstract void AuthorizationFailed();

		// Token: 0x06006A57 RID: 27223
		internal abstract void TxFlowed();

		// Token: 0x04003CA8 RID: 15528
		protected string instanceName;

		// Token: 0x04003CA9 RID: 15529
		protected string operationName;

		// Token: 0x04003CAA RID: 15530
		protected static readonly string[] perfCounterNames = new string[]
		{
			"Calls",
			"Calls Per Second",
			"Calls Outstanding",
			"Calls Failed",
			"Call Failed Per Second",
			"Calls Faulted",
			"Calls Faulted Per Second",
			"Calls Duration",
			"Calls Duration Base",
			"Security Validation and Authentication Failures",
			"Security Validation and Authentication Failures Per Second",
			"Security Calls Not Authorized",
			"Security Calls Not Authorized Per Second",
			"Transactions Flowed",
			"Transactions Flowed Per Second"
		};

		// Token: 0x04003CAB RID: 15531
		private const int maxCounterLength = 64;

		// Token: 0x04003CAC RID: 15532
		private const int hashLength = 2;

		// Token: 0x02000EA7 RID: 3751
		protected enum PerfCounters
		{
			// Token: 0x04004C23 RID: 19491
			Calls,
			// Token: 0x04004C24 RID: 19492
			CallsPerSecond,
			// Token: 0x04004C25 RID: 19493
			CallsOutstanding,
			// Token: 0x04004C26 RID: 19494
			CallsFailed,
			// Token: 0x04004C27 RID: 19495
			CallsFailedPerSecond,
			// Token: 0x04004C28 RID: 19496
			CallsFaulted,
			// Token: 0x04004C29 RID: 19497
			CallsFaultedPerSecond,
			// Token: 0x04004C2A RID: 19498
			CallDuration,
			// Token: 0x04004C2B RID: 19499
			CallDurationBase,
			// Token: 0x04004C2C RID: 19500
			SecurityValidationAuthenticationFailures,
			// Token: 0x04004C2D RID: 19501
			SecurityValidationAuthenticationFailuresPerSecond,
			// Token: 0x04004C2E RID: 19502
			CallsNotAuthorized,
			// Token: 0x04004C2F RID: 19503
			CallsNotAuthorizedPerSecond,
			// Token: 0x04004C30 RID: 19504
			TxFlowed,
			// Token: 0x04004C31 RID: 19505
			TxFlowedPerSecond,
			// Token: 0x04004C32 RID: 19506
			TotalCounters
		}

		// Token: 0x02000EA8 RID: 3752
		[Flags]
		private enum truncOptions : uint
		{
			// Token: 0x04004C34 RID: 19508
			NoBits = 0U,
			// Token: 0x04004C35 RID: 19509
			service7 = 1U,
			// Token: 0x04004C36 RID: 19510
			contract7 = 2U,
			// Token: 0x04004C37 RID: 19511
			operation15 = 4U,
			// Token: 0x04004C38 RID: 19512
			uri32 = 8U
		}
	}
}
