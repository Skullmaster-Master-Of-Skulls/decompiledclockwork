using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200059E RID: 1438
	internal class SingletonInstanceContextProvider : InstanceContextProviderBase
	{
		// Token: 0x060037D2 RID: 14290 RVA: 0x000D6E5A File Offset: 0x000D505A
		internal SingletonInstanceContextProvider(DispatchRuntime dispatchRuntime) : base(dispatchRuntime)
		{
			this.thisLock = new object();
		}

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x060037D3 RID: 14291 RVA: 0x000D6E70 File Offset: 0x000D5070
		internal InstanceContext SingletonInstance
		{
			get
			{
				if (this.singleton == null)
				{
					object obj = this.thisLock;
					lock (obj)
					{
						if (this.singleton == null)
						{
							InstanceContext instanceContext = base.DispatchRuntime.SingletonInstanceContext;
							if (instanceContext == null)
							{
								instanceContext = new InstanceContext(base.DispatchRuntime.ChannelDispatcher.Host, false);
								instanceContext.Open();
							}
							else if (instanceContext.State != CommunicationState.Opened)
							{
								object obj2 = instanceContext.ThisLock;
								lock (obj2)
								{
									if (instanceContext.State != CommunicationState.Opened)
									{
										instanceContext.Open();
									}
								}
							}
							instanceContext.IsUserCreated = false;
							this.singleton = instanceContext;
						}
					}
				}
				return this.singleton;
			}
		}

		// Token: 0x060037D4 RID: 14292 RVA: 0x000D6F44 File Offset: 0x000D5144
		public override InstanceContext GetExistingInstanceContext(Message message, IContextChannel channel)
		{
			ServiceChannel serviceChannelFromProxy = base.GetServiceChannelFromProxy(channel);
			if (serviceChannelFromProxy != null && serviceChannelFromProxy.HasSession)
			{
				this.SingletonInstance.BindIncomingChannel(serviceChannelFromProxy);
			}
			return this.SingletonInstance;
		}

		// Token: 0x060037D5 RID: 14293 RVA: 0x000D6F76 File Offset: 0x000D5176
		public override void InitializeInstanceContext(InstanceContext instanceContext, Message message, IContextChannel channel)
		{
		}

		// Token: 0x060037D6 RID: 14294 RVA: 0x000D6F78 File Offset: 0x000D5178
		public override bool IsIdle(InstanceContext instanceContext)
		{
			return false;
		}

		// Token: 0x060037D7 RID: 14295 RVA: 0x000D6F7B File Offset: 0x000D517B
		public override void NotifyIdle(InstanceContextIdleCallback callback, InstanceContext instanceContext)
		{
		}

		// Token: 0x04002968 RID: 10600
		private InstanceContext singleton;

		// Token: 0x04002969 RID: 10601
		private object thisLock;
	}
}
