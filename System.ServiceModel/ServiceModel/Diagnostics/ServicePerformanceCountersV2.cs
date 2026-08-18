using System;
using System.Diagnostics.PerformanceData;
using System.Security;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A9C RID: 2716
	internal sealed class ServicePerformanceCountersV2 : ServicePerformanceCountersBase
	{
		// Token: 0x06006B80 RID: 27520 RVA: 0x001900BC File Offset: 0x0018E2BC
		internal ServicePerformanceCountersV2(ServiceHostBase serviceHost) : base(serviceHost)
		{
			if (ServicePerformanceCountersV2.serviceCounterSet == null)
			{
				object obj = ServicePerformanceCountersV2.syncRoot;
				lock (obj)
				{
					if (ServicePerformanceCountersV2.serviceCounterSet == null)
					{
						CounterSet counterSet = ServicePerformanceCountersV2.CreateCounterSet();
						counterSet.AddCounter(0, CounterType.RawData32, ServicePerformanceCountersBase.perfCounterNames[0]);
						counterSet.AddCounter(1, CounterType.RateOfCountPerSecond32, ServicePerformanceCountersBase.perfCounterNames[1]);
						counterSet.AddCounter(2, CounterType.RawData32, ServicePerformanceCountersBase.perfCounterNames[2]);
						counterSet.AddCounter(3, CounterType.RawData32, ServicePerformanceCountersBase.perfCounterNames[3]);
						counterSet.AddCounter(4, CounterType.RateOfCountPerSecond32, ServicePerformanceCountersBase.perfCounterNames[4]);
						counterSet.AddCounter(5, CounterType.RawData32, ServicePerformanceCountersBase.perfCounterNames[5]);
						counterSet.AddCounter(6, CounterType.RateOfCountPerSecond32, ServicePerformanceCountersBase.perfCounterNames[6]);
						counterSet.AddCounter(8, CounterType.AverageBase, ServicePerformanceCountersBase.perfCounterNames[8]);
						counterSet.AddCounter(7, CounterType.AverageTimer32, ServicePerformanceCountersBase.perfCounterNames[7]);
						counterSet.AddCounter(9, CounterType.RawData32, ServicePerformanceCountersBase.perfCounterNames[9]);
						counterSet.AddCounter(10, CounterType.RateOfCountPerSecond32, ServicePerformanceCountersBase.perfCounterNames[10]);
						counterSet.AddCounter(11, CounterType.RawData32, ServicePerformanceCountersBase.perfCounterNames[11]);
						counterSet.AddCounter(12, CounterType.RateOfCountPerSecond32, ServicePerformanceCountersBase.perfCounterNames[12]);
						counterSet.AddCounter(13, CounterType.RawData32, ServicePerformanceCountersBase.perfCounterNames[13]);
						counterSet.AddCounter(14, CounterType.RateOfCountPerSecond32, ServicePerformanceCountersBase.perfCounterNames[14]);
						counterSet.AddCounter(15, CounterType.RawData32, ServicePerformanceCountersBase.perfCounterNames[15]);
						counterSet.AddCounter(16, CounterType.RateOfCountPerSecond32, ServicePerformanceCountersBase.perfCounterNames[16]);
						counterSet.AddCounter(17, CounterType.RawData32, ServicePerformanceCountersBase.perfCounterNames[17]);
						counterSet.AddCounter(18, CounterType.RateOfCountPerSecond32, ServicePerformanceCountersBase.perfCounterNames[18]);
						counterSet.AddCounter(19, CounterType.RawData32, ServicePerformanceCountersBase.perfCounterNames[19]);
						counterSet.AddCounter(20, CounterType.RateOfCountPerSecond32, ServicePerformanceCountersBase.perfCounterNames[20]);
						counterSet.AddCounter(21, CounterType.RawData32, ServicePerformanceCountersBase.perfCounterNames[21]);
						counterSet.AddCounter(22, CounterType.RateOfCountPerSecond32, ServicePerformanceCountersBase.perfCounterNames[22]);
						counterSet.AddCounter(23, CounterType.RawData32, ServicePerformanceCountersBase.perfCounterNames[23]);
						counterSet.AddCounter(24, CounterType.RateOfCountPerSecond32, ServicePerformanceCountersBase.perfCounterNames[24]);
						counterSet.AddCounter(25, CounterType.RawData32, ServicePerformanceCountersBase.perfCounterNames[25]);
						counterSet.AddCounter(26, CounterType.RateOfCountPerSecond32, ServicePerformanceCountersBase.perfCounterNames[26]);
						counterSet.AddCounter(27, CounterType.RawData32, ServicePerformanceCountersBase.perfCounterNames[27]);
						counterSet.AddCounter(28, CounterType.RateOfCountPerSecond32, ServicePerformanceCountersBase.perfCounterNames[28]);
						counterSet.AddCounter(29, CounterType.RawData32, ServicePerformanceCountersBase.perfCounterNames[29]);
						counterSet.AddCounter(30, CounterType.RateOfCountPerSecond32, ServicePerformanceCountersBase.perfCounterNames[30]);
						counterSet.AddCounter(31, CounterType.RawData32, ServicePerformanceCountersBase.perfCounterNames[31]);
						counterSet.AddCounter(32, CounterType.RateOfCountPerSecond32, ServicePerformanceCountersBase.perfCounterNames[32]);
						counterSet.AddCounter(33, CounterType.RawFraction32, ServicePerformanceCountersBase.perfCounterNames[33]);
						counterSet.AddCounter(34, CounterType.RawBase32, ServicePerformanceCountersBase.perfCounterNames[34]);
						counterSet.AddCounter(35, CounterType.RawFraction32, ServicePerformanceCountersBase.perfCounterNames[35]);
						counterSet.AddCounter(36, CounterType.RawBase32, ServicePerformanceCountersBase.perfCounterNames[36]);
						counterSet.AddCounter(37, CounterType.RawFraction32, ServicePerformanceCountersBase.perfCounterNames[37]);
						counterSet.AddCounter(38, CounterType.RawBase32, ServicePerformanceCountersBase.perfCounterNames[38]);
						ServicePerformanceCountersV2.serviceCounterSet = counterSet;
					}
				}
			}
			this.serviceCounterSetInstance = ServicePerformanceCountersV2.CreateCounterSetInstance(this.InstanceName);
			this.counters = new CounterData[39];
			for (int i = 0; i < 39; i++)
			{
				this.counters[i] = this.serviceCounterSetInstance.Counters[i];
				this.counters[i].Value = 0L;
			}
		}

		// Token: 0x06006B81 RID: 27521 RVA: 0x001904A4 File Offset: 0x0018E6A4
		[SecuritySafeCritical]
		private static CounterSet CreateCounterSet()
		{
			return new CounterSet(ServicePerformanceCountersV2.serviceModelProviderId, ServicePerformanceCountersV2.serviceCounterSetId, CounterSetInstanceType.Multiple);
		}

		// Token: 0x06006B82 RID: 27522 RVA: 0x001904B6 File Offset: 0x0018E6B6
		[SecuritySafeCritical]
		private static CounterSetInstance CreateCounterSetInstance(string name)
		{
			return ServicePerformanceCountersV2.counterSetInstanceCache.Get(name) ?? ServicePerformanceCountersV2.serviceCounterSet.CreateCounterSetInstance(name);
		}

		// Token: 0x06006B83 RID: 27523 RVA: 0x001904D4 File Offset: 0x0018E6D4
		internal override void MethodCalled()
		{
			this.counters[0].Increment();
			this.counters[1].Increment();
			this.counters[2].Increment();
		}

		// Token: 0x06006B84 RID: 27524 RVA: 0x001904FD File Offset: 0x0018E6FD
		internal override void MethodReturnedSuccess()
		{
			this.counters[2].Decrement();
		}

		// Token: 0x06006B85 RID: 27525 RVA: 0x0019050C File Offset: 0x0018E70C
		internal override void MethodReturnedError()
		{
			this.counters[3].Increment();
			this.counters[4].Increment();
			this.counters[2].Decrement();
		}

		// Token: 0x06006B86 RID: 27526 RVA: 0x00190535 File Offset: 0x0018E735
		internal override void MethodReturnedFault()
		{
			this.counters[5].Increment();
			this.counters[6].Increment();
			this.counters[2].Decrement();
		}

		// Token: 0x06006B87 RID: 27527 RVA: 0x0019055E File Offset: 0x0018E75E
		internal override void SaveCallDuration(long time)
		{
			this.counters[7].IncrementBy(time);
			this.counters[8].Increment();
		}

		// Token: 0x06006B88 RID: 27528 RVA: 0x0019057B File Offset: 0x0018E77B
		internal override void AuthenticationFailed()
		{
			this.counters[9].Increment();
			this.counters[10].Increment();
		}

		// Token: 0x06006B89 RID: 27529 RVA: 0x00190599 File Offset: 0x0018E799
		internal override void AuthorizationFailed()
		{
			this.counters[11].Increment();
			this.counters[12].Increment();
		}

		// Token: 0x06006B8A RID: 27530 RVA: 0x001905B7 File Offset: 0x0018E7B7
		internal override void ServiceInstanceCreated()
		{
			this.counters[13].Increment();
			this.counters[14].Increment();
		}

		// Token: 0x06006B8B RID: 27531 RVA: 0x001905D5 File Offset: 0x0018E7D5
		internal override void ServiceInstanceRemoved()
		{
			this.counters[13].Decrement();
		}

		// Token: 0x06006B8C RID: 27532 RVA: 0x001905E5 File Offset: 0x0018E7E5
		internal override void SessionFaulted()
		{
			this.counters[15].Increment();
			this.counters[16].Increment();
		}

		// Token: 0x06006B8D RID: 27533 RVA: 0x00190603 File Offset: 0x0018E803
		internal override void MessageDropped()
		{
			this.counters[17].Increment();
			this.counters[18].Increment();
		}

		// Token: 0x06006B8E RID: 27534 RVA: 0x00190621 File Offset: 0x0018E821
		internal override void TxCommitted(long count)
		{
			this.counters[21].Increment();
			this.counters[22].Increment();
		}

		// Token: 0x06006B8F RID: 27535 RVA: 0x0019063F File Offset: 0x0018E83F
		internal override void TxInDoubt(long count)
		{
			this.counters[25].Increment();
			this.counters[26].Increment();
		}

		// Token: 0x06006B90 RID: 27536 RVA: 0x0019065D File Offset: 0x0018E85D
		internal override void TxAborted(long count)
		{
			this.counters[23].Increment();
			this.counters[24].Increment();
		}

		// Token: 0x06006B91 RID: 27537 RVA: 0x0019067B File Offset: 0x0018E87B
		internal override void TxFlowed()
		{
			this.counters[19].Increment();
			this.counters[20].Increment();
		}

		// Token: 0x06006B92 RID: 27538 RVA: 0x00190699 File Offset: 0x0018E899
		internal override void MsmqDroppedMessage()
		{
			this.counters[31].Increment();
			this.counters[32].Increment();
		}

		// Token: 0x06006B93 RID: 27539 RVA: 0x001906B7 File Offset: 0x0018E8B7
		internal override void MsmqPoisonMessage()
		{
			this.counters[27].Increment();
			this.counters[28].Increment();
		}

		// Token: 0x06006B94 RID: 27540 RVA: 0x001906D5 File Offset: 0x0018E8D5
		internal override void MsmqRejectedMessage()
		{
			this.counters[29].Increment();
			this.counters[30].Increment();
		}

		// Token: 0x06006B95 RID: 27541 RVA: 0x001906F3 File Offset: 0x0018E8F3
		internal override void IncrementThrottlePercent(int counterIndex)
		{
			this.counters[counterIndex].Increment();
		}

		// Token: 0x06006B96 RID: 27542 RVA: 0x00190702 File Offset: 0x0018E902
		internal override void SetThrottleBase(int counterIndex, long denominator)
		{
			this.counters[counterIndex].Value = denominator;
		}

		// Token: 0x06006B97 RID: 27543 RVA: 0x00190712 File Offset: 0x0018E912
		internal override void DecrementThrottlePercent(int counterIndex)
		{
			this.counters[counterIndex].Decrement();
		}

		// Token: 0x1700197A RID: 6522
		// (get) Token: 0x06006B98 RID: 27544 RVA: 0x00190721 File Offset: 0x0018E921
		internal override bool Initialized
		{
			get
			{
				return this.serviceCounterSetInstance != null;
			}
		}

		// Token: 0x06006B99 RID: 27545 RVA: 0x0019072C File Offset: 0x0018E92C
		internal void DeleteInstance()
		{
			if (this.serviceCounterSetInstance != null)
			{
				this.serviceCounterSetInstance.Dispose();
				this.serviceCounterSetInstance = null;
			}
		}

		// Token: 0x06006B9A RID: 27546 RVA: 0x00190748 File Offset: 0x0018E948
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && PerformanceCounters.PerformanceCountersEnabled && this.serviceCounterSetInstance != null)
				{
					ServicePerformanceCountersV2.counterSetInstanceCache.Cleanup();
					OperationPerformanceCountersV2.CleanupCache();
					EndpointPerformanceCountersV2.CleanupCache();
					ServicePerformanceCountersV2.counterSetInstanceCache.Add(this.InstanceName, this.serviceCounterSetInstance);
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x04003CEA RID: 15594
		private static object syncRoot = new object();

		// Token: 0x04003CEB RID: 15595
		private static Guid serviceModelProviderId = new Guid("{890c10c3-8c2a-4fe3-a36a-9eca153d47cb}");

		// Token: 0x04003CEC RID: 15596
		private static Guid serviceCounterSetId = new Guid("{e829b6db-21ab-453b-83c9-d980ec708edd}");

		// Token: 0x04003CED RID: 15597
		private static readonly PerformanceCountersBase.CounterSetInstanceCache counterSetInstanceCache = new PerformanceCountersBase.CounterSetInstanceCache();

		// Token: 0x04003CEE RID: 15598
		private static volatile CounterSet serviceCounterSet;

		// Token: 0x04003CEF RID: 15599
		private CounterSetInstance serviceCounterSetInstance;

		// Token: 0x04003CF0 RID: 15600
		private CounterData[] counters;
	}
}
