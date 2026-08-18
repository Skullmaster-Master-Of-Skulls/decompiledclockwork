using System;
using System.Collections.Generic;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A9F RID: 2719
	internal class ServiceModelPerformanceCountersEntry
	{
		// Token: 0x06006BAE RID: 27566 RVA: 0x00190B6D File Offset: 0x0018ED6D
		public ServiceModelPerformanceCountersEntry(ServicePerformanceCountersBase serviceCounters)
		{
			this.servicePerformanceCounters = serviceCounters;
			this.performanceCounters = new List<ServiceModelPerformanceCounters>();
		}

		// Token: 0x06006BAF RID: 27567 RVA: 0x00190B87 File Offset: 0x0018ED87
		public ServiceModelPerformanceCountersEntry(DefaultPerformanceCounters defaultServiceCounters)
		{
			this.defaultPerformanceCounters = defaultServiceCounters;
			this.performanceCounters = new List<ServiceModelPerformanceCounters>();
		}

		// Token: 0x06006BB0 RID: 27568 RVA: 0x00190BA1 File Offset: 0x0018EDA1
		public void Add(ServiceModelPerformanceCounters counters)
		{
			this.performanceCounters.Add(counters);
		}

		// Token: 0x06006BB1 RID: 27569 RVA: 0x00190BB0 File Offset: 0x0018EDB0
		public void Remove(string id)
		{
			for (int i = 0; i < this.performanceCounters.Count; i++)
			{
				if (this.performanceCounters[i].PerfCounterId.Equals(id))
				{
					this.performanceCounters.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x06006BB2 RID: 27570 RVA: 0x00190BF9 File Offset: 0x0018EDF9
		public void Clear()
		{
			this.performanceCounters.Clear();
		}

		// Token: 0x17001986 RID: 6534
		// (get) Token: 0x06006BB3 RID: 27571 RVA: 0x00190C06 File Offset: 0x0018EE06
		// (set) Token: 0x06006BB4 RID: 27572 RVA: 0x00190C0E File Offset: 0x0018EE0E
		public ServicePerformanceCountersBase ServicePerformanceCounters
		{
			get
			{
				return this.servicePerformanceCounters;
			}
			set
			{
				this.servicePerformanceCounters = value;
			}
		}

		// Token: 0x17001987 RID: 6535
		// (get) Token: 0x06006BB5 RID: 27573 RVA: 0x00190C17 File Offset: 0x0018EE17
		// (set) Token: 0x06006BB6 RID: 27574 RVA: 0x00190C1F File Offset: 0x0018EE1F
		public DefaultPerformanceCounters DefaultPerformanceCounters
		{
			get
			{
				return this.defaultPerformanceCounters;
			}
			set
			{
				this.defaultPerformanceCounters = value;
			}
		}

		// Token: 0x17001988 RID: 6536
		// (get) Token: 0x06006BB7 RID: 27575 RVA: 0x00190C28 File Offset: 0x0018EE28
		public List<ServiceModelPerformanceCounters> CounterList
		{
			get
			{
				return this.performanceCounters;
			}
		}

		// Token: 0x04003CFD RID: 15613
		private ServicePerformanceCountersBase servicePerformanceCounters;

		// Token: 0x04003CFE RID: 15614
		private DefaultPerformanceCounters defaultPerformanceCounters;

		// Token: 0x04003CFF RID: 15615
		private List<ServiceModelPerformanceCounters> performanceCounters;
	}
}
