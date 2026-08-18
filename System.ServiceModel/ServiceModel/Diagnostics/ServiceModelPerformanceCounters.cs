using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A9E RID: 2718
	internal class ServiceModelPerformanceCounters
	{
		// Token: 0x06006BA6 RID: 27558 RVA: 0x00190940 File Offset: 0x0018EB40
		internal ServiceModelPerformanceCounters(ServiceHostBase serviceHost, ContractDescription contractDescription, EndpointDispatcher endpointDispatcher)
		{
			this.perfCounterId = endpointDispatcher.PerfCounterId;
			if (PerformanceCounters.Scope == PerformanceCounterScope.All)
			{
				this.operationPerfCounters = new Dictionary<string, OperationPerformanceCountersBase>(contractDescription.Operations.Count);
				this.actionToOperation = new SortedList<string, string>(contractDescription.Operations.Count);
				foreach (OperationDescription operationDescription in contractDescription.Operations)
				{
					if (operationDescription.Messages[0].Action != null && !this.actionToOperation.Keys.Contains(operationDescription.Messages[0].Action))
					{
						this.actionToOperation.Add(operationDescription.Messages[0].Action, operationDescription.Name);
					}
					OperationPerformanceCountersBase operationPerformanceCountersBase;
					if (!this.operationPerfCounters.TryGetValue(operationDescription.Name, out operationPerformanceCountersBase))
					{
						OperationPerformanceCountersBase operationPerformanceCountersBase2 = PerformanceCountersFactory.CreateOperationCounters(serviceHost.Description.Name, contractDescription.Name, operationDescription.Name, endpointDispatcher.PerfCounterBaseId);
						if (operationPerformanceCountersBase2 == null || !operationPerformanceCountersBase2.Initialized)
						{
							this.initialized = false;
							return;
						}
						this.operationPerfCounters.Add(operationDescription.Name, operationPerformanceCountersBase2);
					}
				}
				EndpointPerformanceCountersBase endpointPerformanceCountersBase = PerformanceCountersFactory.CreateEndpointCounters(serviceHost.Description.Name, contractDescription.Name, endpointDispatcher.PerfCounterBaseId);
				if (endpointPerformanceCountersBase != null && endpointPerformanceCountersBase.Initialized)
				{
					this.endpointPerfCounters = endpointPerformanceCountersBase;
				}
			}
			if (PerformanceCounters.PerformanceCountersEnabled)
			{
				this.servicePerfCounters = serviceHost.Counters;
			}
			if (PerformanceCounters.MinimalPerformanceCountersEnabled)
			{
				this.defaultPerfCounters = serviceHost.DefaultCounters;
			}
			this.initialized = true;
		}

		// Token: 0x06006BA7 RID: 27559 RVA: 0x00190AF0 File Offset: 0x0018ECF0
		internal OperationPerformanceCountersBase GetOperationPerformanceCountersFromMessage(Message message)
		{
			string operation;
			if (this.actionToOperation.TryGetValue(message.Headers.Action, out operation))
			{
				return this.GetOperationPerformanceCounters(operation);
			}
			return null;
		}

		// Token: 0x06006BA8 RID: 27560 RVA: 0x00190B20 File Offset: 0x0018ED20
		internal OperationPerformanceCountersBase GetOperationPerformanceCounters(string operation)
		{
			Dictionary<string, OperationPerformanceCountersBase> dictionary = this.operationPerfCounters;
			OperationPerformanceCountersBase result;
			if (dictionary != null && dictionary.TryGetValue(operation, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x17001981 RID: 6529
		// (get) Token: 0x06006BA9 RID: 27561 RVA: 0x00190B45 File Offset: 0x0018ED45
		internal bool Initialized
		{
			get
			{
				return this.initialized;
			}
		}

		// Token: 0x17001982 RID: 6530
		// (get) Token: 0x06006BAA RID: 27562 RVA: 0x00190B4D File Offset: 0x0018ED4D
		internal EndpointPerformanceCountersBase EndpointPerformanceCounters
		{
			get
			{
				return this.endpointPerfCounters;
			}
		}

		// Token: 0x17001983 RID: 6531
		// (get) Token: 0x06006BAB RID: 27563 RVA: 0x00190B55 File Offset: 0x0018ED55
		internal ServicePerformanceCountersBase ServicePerformanceCounters
		{
			get
			{
				return this.servicePerfCounters;
			}
		}

		// Token: 0x17001984 RID: 6532
		// (get) Token: 0x06006BAC RID: 27564 RVA: 0x00190B5D File Offset: 0x0018ED5D
		internal DefaultPerformanceCounters DefaultPerformanceCounters
		{
			get
			{
				return this.defaultPerfCounters;
			}
		}

		// Token: 0x17001985 RID: 6533
		// (get) Token: 0x06006BAD RID: 27565 RVA: 0x00190B65 File Offset: 0x0018ED65
		internal string PerfCounterId
		{
			get
			{
				return this.perfCounterId;
			}
		}

		// Token: 0x04003CF6 RID: 15606
		private Dictionary<string, OperationPerformanceCountersBase> operationPerfCounters;

		// Token: 0x04003CF7 RID: 15607
		private SortedList<string, string> actionToOperation;

		// Token: 0x04003CF8 RID: 15608
		private EndpointPerformanceCountersBase endpointPerfCounters;

		// Token: 0x04003CF9 RID: 15609
		private ServicePerformanceCountersBase servicePerfCounters;

		// Token: 0x04003CFA RID: 15610
		private DefaultPerformanceCounters defaultPerfCounters;

		// Token: 0x04003CFB RID: 15611
		private bool initialized;

		// Token: 0x04003CFC RID: 15612
		private string perfCounterId;
	}
}
