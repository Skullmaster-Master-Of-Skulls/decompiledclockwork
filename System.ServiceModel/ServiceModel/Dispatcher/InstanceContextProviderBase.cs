using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000581 RID: 1409
	internal abstract class InstanceContextProviderBase : IInstanceContextProvider
	{
		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x06003663 RID: 13923 RVA: 0x000D1B15 File Offset: 0x000CFD15
		public DispatchRuntime DispatchRuntime
		{
			get
			{
				return this.dispatchRuntime;
			}
		}

		// Token: 0x06003664 RID: 13924 RVA: 0x000D1B1D File Offset: 0x000CFD1D
		internal InstanceContextProviderBase(DispatchRuntime dispatchRuntime)
		{
			this.dispatchRuntime = dispatchRuntime;
		}

		// Token: 0x06003665 RID: 13925 RVA: 0x000D1B2C File Offset: 0x000CFD2C
		internal static bool IsProviderSingleton(IInstanceContextProvider provider)
		{
			return provider is SingletonInstanceContextProvider;
		}

		// Token: 0x06003666 RID: 13926 RVA: 0x000D1B37 File Offset: 0x000CFD37
		internal static bool IsProviderSessionful(IInstanceContextProvider provider)
		{
			return provider is PerSessionInstanceContextProvider;
		}

		// Token: 0x06003667 RID: 13927 RVA: 0x000D1B42 File Offset: 0x000CFD42
		internal static IInstanceContextProvider GetProviderForMode(InstanceContextMode instanceMode, DispatchRuntime runtime)
		{
			switch (instanceMode)
			{
			case InstanceContextMode.PerSession:
				return new PerSessionInstanceContextProvider(runtime);
			case InstanceContextMode.PerCall:
				return new PerCallInstanceContextProvider(runtime);
			case InstanceContextMode.Single:
				return new SingletonInstanceContextProvider(runtime);
			default:
				DiagnosticUtility.FailFast("InstanceContextProviderBase.GetProviderForMode: default");
				return null;
			}
		}

		// Token: 0x06003668 RID: 13928 RVA: 0x000D1B79 File Offset: 0x000CFD79
		internal static bool IsProviderPerCall(IInstanceContextProvider provider)
		{
			return provider is PerCallInstanceContextProvider;
		}

		// Token: 0x06003669 RID: 13929 RVA: 0x000D1B84 File Offset: 0x000CFD84
		internal ServiceChannel GetServiceChannelFromProxy(IContextChannel channel)
		{
			ServiceChannel serviceChannel = channel as ServiceChannel;
			if (serviceChannel == null)
			{
				serviceChannel = ServiceChannelFactory.GetServiceChannel(channel);
			}
			return serviceChannel;
		}

		// Token: 0x0600366A RID: 13930 RVA: 0x000D1BA3 File Offset: 0x000CFDA3
		public virtual InstanceContext GetExistingInstanceContext(Message message, IContextChannel channel)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x0600366B RID: 13931 RVA: 0x000D1BB4 File Offset: 0x000CFDB4
		public virtual void InitializeInstanceContext(InstanceContext instanceContext, Message message, IContextChannel channel)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x0600366C RID: 13932 RVA: 0x000D1BC5 File Offset: 0x000CFDC5
		public virtual bool IsIdle(InstanceContext instanceContext)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x0600366D RID: 13933 RVA: 0x000D1BD6 File Offset: 0x000CFDD6
		public virtual void NotifyIdle(InstanceContextIdleCallback callback, InstanceContext instanceContext)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x040028AD RID: 10413
		private DispatchRuntime dispatchRuntime;
	}
}
