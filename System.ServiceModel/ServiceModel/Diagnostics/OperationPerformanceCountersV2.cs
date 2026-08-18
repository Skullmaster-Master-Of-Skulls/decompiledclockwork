using System;
using System.Diagnostics.PerformanceData;
using System.Security;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A88 RID: 2696
	internal sealed class OperationPerformanceCountersV2 : OperationPerformanceCountersBase
	{
		// Token: 0x06006A69 RID: 27241 RVA: 0x0018C9EC File Offset: 0x0018ABEC
		internal OperationPerformanceCountersV2(string service, string contract, string operationName, string uri) : base(service, contract, operationName, uri)
		{
			OperationPerformanceCountersV2.EnsureCounterSet();
			this.operationCounterSetInstance = OperationPerformanceCountersV2.CreateCounterSetInstance(this.InstanceName);
			this.counters = new CounterData[15];
			for (int i = 0; i < 15; i++)
			{
				this.counters[i] = this.operationCounterSetInstance.Counters[i];
				this.counters[i].Value = 0L;
			}
		}

		// Token: 0x06006A6A RID: 27242 RVA: 0x0018CA5C File Offset: 0x0018AC5C
		internal static void EnsureCounterSet()
		{
			if (OperationPerformanceCountersV2.operationCounterSet == null)
			{
				object obj = OperationPerformanceCountersV2.syncRoot;
				lock (obj)
				{
					if (OperationPerformanceCountersV2.operationCounterSet == null)
					{
						CounterSet counterSet = OperationPerformanceCountersV2.CreateCounterSet();
						counterSet.AddCounter(0, CounterType.RawData32, OperationPerformanceCountersBase.perfCounterNames[0]);
						counterSet.AddCounter(1, CounterType.RateOfCountPerSecond32, OperationPerformanceCountersBase.perfCounterNames[1]);
						counterSet.AddCounter(2, CounterType.RawData32, OperationPerformanceCountersBase.perfCounterNames[2]);
						counterSet.AddCounter(3, CounterType.RawData32, OperationPerformanceCountersBase.perfCounterNames[3]);
						counterSet.AddCounter(4, CounterType.RateOfCountPerSecond32, OperationPerformanceCountersBase.perfCounterNames[4]);
						counterSet.AddCounter(5, CounterType.RawData32, OperationPerformanceCountersBase.perfCounterNames[5]);
						counterSet.AddCounter(6, CounterType.RateOfCountPerSecond32, OperationPerformanceCountersBase.perfCounterNames[6]);
						counterSet.AddCounter(8, CounterType.AverageBase, OperationPerformanceCountersBase.perfCounterNames[8]);
						counterSet.AddCounter(7, CounterType.AverageTimer32, OperationPerformanceCountersBase.perfCounterNames[7]);
						counterSet.AddCounter(9, CounterType.RawData32, OperationPerformanceCountersBase.perfCounterNames[9]);
						counterSet.AddCounter(10, CounterType.RateOfCountPerSecond32, OperationPerformanceCountersBase.perfCounterNames[10]);
						counterSet.AddCounter(11, CounterType.RawData32, OperationPerformanceCountersBase.perfCounterNames[11]);
						counterSet.AddCounter(12, CounterType.RateOfCountPerSecond32, OperationPerformanceCountersBase.perfCounterNames[12]);
						counterSet.AddCounter(13, CounterType.RawData32, OperationPerformanceCountersBase.perfCounterNames[13]);
						counterSet.AddCounter(14, CounterType.RateOfCountPerSecond32, OperationPerformanceCountersBase.perfCounterNames[14]);
						OperationPerformanceCountersV2.operationCounterSet = counterSet;
					}
				}
			}
		}

		// Token: 0x06006A6B RID: 27243 RVA: 0x0018CBF0 File Offset: 0x0018ADF0
		[SecuritySafeCritical]
		private static CounterSet CreateCounterSet()
		{
			return new CounterSet(OperationPerformanceCountersV2.serviceModelProviderId, OperationPerformanceCountersV2.operationCounterSetId, CounterSetInstanceType.Multiple);
		}

		// Token: 0x06006A6C RID: 27244 RVA: 0x0018CC02 File Offset: 0x0018AE02
		[SecuritySafeCritical]
		private static CounterSetInstance CreateCounterSetInstance(string name)
		{
			return OperationPerformanceCountersV2.counterSetInstanceCache.Get(name) ?? OperationPerformanceCountersV2.operationCounterSet.CreateCounterSetInstance(name);
		}

		// Token: 0x06006A6D RID: 27245 RVA: 0x0018CC20 File Offset: 0x0018AE20
		internal override void MethodCalled()
		{
			this.counters[0].Increment();
			this.counters[1].Increment();
			this.counters[2].Increment();
		}

		// Token: 0x06006A6E RID: 27246 RVA: 0x0018CC49 File Offset: 0x0018AE49
		internal override void MethodReturnedSuccess()
		{
			this.counters[2].Decrement();
		}

		// Token: 0x06006A6F RID: 27247 RVA: 0x0018CC58 File Offset: 0x0018AE58
		internal override void MethodReturnedError()
		{
			this.counters[3].Increment();
			this.counters[4].Increment();
			this.counters[2].Decrement();
		}

		// Token: 0x06006A70 RID: 27248 RVA: 0x0018CC81 File Offset: 0x0018AE81
		internal override void MethodReturnedFault()
		{
			this.counters[5].Increment();
			this.counters[6].Increment();
			this.counters[2].Decrement();
		}

		// Token: 0x06006A71 RID: 27249 RVA: 0x0018CCAA File Offset: 0x0018AEAA
		internal override void SaveCallDuration(long time)
		{
			this.counters[7].IncrementBy(time);
			this.counters[8].Increment();
		}

		// Token: 0x06006A72 RID: 27250 RVA: 0x0018CCC7 File Offset: 0x0018AEC7
		internal override void AuthenticationFailed()
		{
			this.counters[9].Increment();
			this.counters[10].Increment();
		}

		// Token: 0x06006A73 RID: 27251 RVA: 0x0018CCE5 File Offset: 0x0018AEE5
		internal override void AuthorizationFailed()
		{
			this.counters[11].Increment();
			this.counters[12].Increment();
		}

		// Token: 0x06006A74 RID: 27252 RVA: 0x0018CD03 File Offset: 0x0018AF03
		internal override void TxFlowed()
		{
			this.counters[13].Increment();
			this.counters[14].Increment();
		}

		// Token: 0x1700195B RID: 6491
		// (get) Token: 0x06006A75 RID: 27253 RVA: 0x0018CD21 File Offset: 0x0018AF21
		internal override bool Initialized
		{
			get
			{
				return this.operationCounterSetInstance != null;
			}
		}

		// Token: 0x06006A76 RID: 27254 RVA: 0x0018CD2C File Offset: 0x0018AF2C
		internal void DeleteInstance()
		{
			if (this.operationCounterSetInstance != null)
			{
				this.operationCounterSetInstance.Dispose();
				this.operationCounterSetInstance = null;
			}
		}

		// Token: 0x06006A77 RID: 27255 RVA: 0x0018CD48 File Offset: 0x0018AF48
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && PerformanceCounters.PerformanceCountersEnabled && this.operationCounterSetInstance != null)
				{
					OperationPerformanceCountersV2.counterSetInstanceCache.Add(this.InstanceName, this.operationCounterSetInstance);
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06006A78 RID: 27256 RVA: 0x0018CD98 File Offset: 0x0018AF98
		internal static void CleanupCache()
		{
			OperationPerformanceCountersV2.counterSetInstanceCache.Cleanup();
		}

		// Token: 0x04003CAE RID: 15534
		private static object syncRoot = new object();

		// Token: 0x04003CAF RID: 15535
		private static Guid serviceModelProviderId = new Guid("{890c10c3-8c2a-4fe3-a36a-9eca153d47cb}");

		// Token: 0x04003CB0 RID: 15536
		private static Guid operationCounterSetId = new Guid("{8ebb0470-da6d-485b-8441-8e06b049157a}");

		// Token: 0x04003CB1 RID: 15537
		private static readonly PerformanceCountersBase.CounterSetInstanceCache counterSetInstanceCache = new PerformanceCountersBase.CounterSetInstanceCache();

		// Token: 0x04003CB2 RID: 15538
		private static volatile CounterSet operationCounterSet;

		// Token: 0x04003CB3 RID: 15539
		private CounterSetInstance operationCounterSetInstance;

		// Token: 0x04003CB4 RID: 15540
		private CounterData[] counters;
	}
}
