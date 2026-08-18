using System;
using System.Diagnostics.PerformanceData;
using System.Security;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A79 RID: 2681
	internal sealed class EndpointPerformanceCountersV2 : EndpointPerformanceCountersBase
	{
		// Token: 0x060069CF RID: 27087 RVA: 0x0018A194 File Offset: 0x00188394
		internal EndpointPerformanceCountersV2(string service, string contract, string uri) : base(service, contract, uri)
		{
			EndpointPerformanceCountersV2.EnsureCounterSet();
			this.endpointCounterSetInstance = EndpointPerformanceCountersV2.CreateCounterSetInstance(this.InstanceName);
			this.counters = new CounterData[19];
			for (int i = 0; i < 19; i++)
			{
				this.counters[i] = this.endpointCounterSetInstance.Counters[i];
				this.counters[i].Value = 0L;
			}
		}

		// Token: 0x060069D0 RID: 27088 RVA: 0x0018A204 File Offset: 0x00188404
		internal static void EnsureCounterSet()
		{
			if (EndpointPerformanceCountersV2.endpointCounterSet == null)
			{
				object obj = EndpointPerformanceCountersV2.syncRoot;
				lock (obj)
				{
					if (EndpointPerformanceCountersV2.endpointCounterSet == null)
					{
						CounterSet counterSet = EndpointPerformanceCountersV2.CreateCounterSet();
						counterSet.AddCounter(0, CounterType.RawData32, EndpointPerformanceCountersBase.perfCounterNames[0]);
						counterSet.AddCounter(1, CounterType.RateOfCountPerSecond32, EndpointPerformanceCountersBase.perfCounterNames[1]);
						counterSet.AddCounter(2, CounterType.RawData32, EndpointPerformanceCountersBase.perfCounterNames[2]);
						counterSet.AddCounter(3, CounterType.RawData32, EndpointPerformanceCountersBase.perfCounterNames[3]);
						counterSet.AddCounter(4, CounterType.RateOfCountPerSecond32, EndpointPerformanceCountersBase.perfCounterNames[4]);
						counterSet.AddCounter(5, CounterType.RawData32, EndpointPerformanceCountersBase.perfCounterNames[5]);
						counterSet.AddCounter(6, CounterType.RateOfCountPerSecond32, EndpointPerformanceCountersBase.perfCounterNames[6]);
						counterSet.AddCounter(8, CounterType.AverageBase, EndpointPerformanceCountersBase.perfCounterNames[8]);
						counterSet.AddCounter(7, CounterType.AverageTimer32, EndpointPerformanceCountersBase.perfCounterNames[7]);
						counterSet.AddCounter(9, CounterType.RawData32, EndpointPerformanceCountersBase.perfCounterNames[9]);
						counterSet.AddCounter(10, CounterType.RateOfCountPerSecond32, EndpointPerformanceCountersBase.perfCounterNames[10]);
						counterSet.AddCounter(11, CounterType.RawData32, EndpointPerformanceCountersBase.perfCounterNames[11]);
						counterSet.AddCounter(12, CounterType.RateOfCountPerSecond32, EndpointPerformanceCountersBase.perfCounterNames[12]);
						counterSet.AddCounter(13, CounterType.RawData32, EndpointPerformanceCountersBase.perfCounterNames[13]);
						counterSet.AddCounter(14, CounterType.RateOfCountPerSecond32, EndpointPerformanceCountersBase.perfCounterNames[14]);
						counterSet.AddCounter(15, CounterType.RawData32, EndpointPerformanceCountersBase.perfCounterNames[15]);
						counterSet.AddCounter(16, CounterType.RateOfCountPerSecond32, EndpointPerformanceCountersBase.perfCounterNames[16]);
						counterSet.AddCounter(17, CounterType.RawData32, EndpointPerformanceCountersBase.perfCounterNames[17]);
						counterSet.AddCounter(18, CounterType.RateOfCountPerSecond32, EndpointPerformanceCountersBase.perfCounterNames[18]);
						EndpointPerformanceCountersV2.endpointCounterSet = counterSet;
					}
				}
			}
		}

		// Token: 0x060069D1 RID: 27089 RVA: 0x0018A3EC File Offset: 0x001885EC
		[SecuritySafeCritical]
		private static CounterSet CreateCounterSet()
		{
			return new CounterSet(EndpointPerformanceCountersV2.serviceModelProviderId, EndpointPerformanceCountersV2.endpointCounterSetId, CounterSetInstanceType.Multiple);
		}

		// Token: 0x060069D2 RID: 27090 RVA: 0x0018A3FE File Offset: 0x001885FE
		[SecuritySafeCritical]
		private static CounterSetInstance CreateCounterSetInstance(string name)
		{
			return EndpointPerformanceCountersV2.counterSetInstanceCache.Get(name) ?? EndpointPerformanceCountersV2.endpointCounterSet.CreateCounterSetInstance(name);
		}

		// Token: 0x060069D3 RID: 27091 RVA: 0x0018A41C File Offset: 0x0018861C
		internal override void MethodCalled()
		{
			this.counters[0].Increment();
			this.counters[1].Increment();
			this.counters[2].Increment();
		}

		// Token: 0x060069D4 RID: 27092 RVA: 0x0018A445 File Offset: 0x00188645
		internal override void MethodReturnedSuccess()
		{
			this.counters[2].Decrement();
		}

		// Token: 0x060069D5 RID: 27093 RVA: 0x0018A454 File Offset: 0x00188654
		internal override void MethodReturnedError()
		{
			this.counters[3].Increment();
			this.counters[4].Increment();
			this.counters[2].Decrement();
		}

		// Token: 0x060069D6 RID: 27094 RVA: 0x0018A47D File Offset: 0x0018867D
		internal override void MethodReturnedFault()
		{
			this.counters[5].Increment();
			this.counters[6].Increment();
			this.counters[2].Decrement();
		}

		// Token: 0x060069D7 RID: 27095 RVA: 0x0018A4A6 File Offset: 0x001886A6
		internal override void SaveCallDuration(long time)
		{
			this.counters[7].IncrementBy(time);
			this.counters[8].Increment();
		}

		// Token: 0x060069D8 RID: 27096 RVA: 0x0018A4C3 File Offset: 0x001886C3
		internal override void AuthenticationFailed()
		{
			this.counters[9].Increment();
			this.counters[10].Increment();
		}

		// Token: 0x060069D9 RID: 27097 RVA: 0x0018A4E1 File Offset: 0x001886E1
		internal override void AuthorizationFailed()
		{
			this.counters[11].Increment();
			this.counters[12].Increment();
		}

		// Token: 0x060069DA RID: 27098 RVA: 0x0018A4FF File Offset: 0x001886FF
		internal override void SessionFaulted()
		{
			this.counters[13].Increment();
			this.counters[14].Increment();
		}

		// Token: 0x060069DB RID: 27099 RVA: 0x0018A51D File Offset: 0x0018871D
		internal override void MessageDropped()
		{
			this.counters[15].Increment();
			this.counters[16].Increment();
		}

		// Token: 0x060069DC RID: 27100 RVA: 0x0018A53B File Offset: 0x0018873B
		internal override void TxFlowed()
		{
			this.counters[17].Increment();
			this.counters[18].Increment();
		}

		// Token: 0x17001935 RID: 6453
		// (get) Token: 0x060069DD RID: 27101 RVA: 0x0018A559 File Offset: 0x00188759
		internal override bool Initialized
		{
			get
			{
				return this.endpointCounterSetInstance != null;
			}
		}

		// Token: 0x060069DE RID: 27102 RVA: 0x0018A564 File Offset: 0x00188764
		internal void DeleteInstance()
		{
			if (this.endpointCounterSetInstance != null)
			{
				this.endpointCounterSetInstance.Dispose();
				this.endpointCounterSetInstance = null;
			}
		}

		// Token: 0x060069DF RID: 27103 RVA: 0x0018A580 File Offset: 0x00188780
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && PerformanceCounters.PerformanceCountersEnabled && this.endpointCounterSetInstance != null)
				{
					EndpointPerformanceCountersV2.counterSetInstanceCache.Add(this.InstanceName, this.endpointCounterSetInstance);
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060069E0 RID: 27104 RVA: 0x0018A5D0 File Offset: 0x001887D0
		internal static void CleanupCache()
		{
			EndpointPerformanceCountersV2.counterSetInstanceCache.Cleanup();
		}

		// Token: 0x04003C58 RID: 15448
		private static object syncRoot = new object();

		// Token: 0x04003C59 RID: 15449
		private static Guid serviceModelProviderId = new Guid("{890c10c3-8c2a-4fe3-a36a-9eca153d47cb}");

		// Token: 0x04003C5A RID: 15450
		private static Guid endpointCounterSetId = new Guid("{16dcff2c-91a3-4e6a-8135-0a9e6681c1b5}");

		// Token: 0x04003C5B RID: 15451
		private static readonly PerformanceCountersBase.CounterSetInstanceCache counterSetInstanceCache = new PerformanceCountersBase.CounterSetInstanceCache();

		// Token: 0x04003C5C RID: 15452
		private static volatile CounterSet endpointCounterSet;

		// Token: 0x04003C5D RID: 15453
		private CounterSetInstance endpointCounterSetInstance;

		// Token: 0x04003C5E RID: 15454
		private CounterData[] counters;
	}
}
