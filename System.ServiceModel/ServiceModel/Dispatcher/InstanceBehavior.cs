using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000545 RID: 1349
	internal class InstanceBehavior
	{
		// Token: 0x06003356 RID: 13142 RVA: 0x000C6428 File Offset: 0x000C4628
		internal InstanceBehavior(DispatchRuntime dispatch, ImmutableDispatchRuntime immutableRuntime)
		{
			this.useSession = dispatch.ChannelDispatcher.Session;
			this.immutableRuntime = immutableRuntime;
			this.host = ((dispatch.ChannelDispatcher == null) ? null : dispatch.ChannelDispatcher.Host);
			this.initializers = EmptyArray<IInstanceContextInitializer>.ToArray(dispatch.InstanceContextInitializers);
			this.provider = dispatch.InstanceProvider;
			this.singleton = dispatch.SingletonInstanceContext;
			this.transactionAutoCompleteOnSessionClose = dispatch.TransactionAutoCompleteOnSessionClose;
			this.releaseServiceInstanceOnTransactionComplete = dispatch.ReleaseServiceInstanceOnTransactionComplete;
			this.isSynchronized = (dispatch.ConcurrencyMode != ConcurrencyMode.Multiple);
			this.instanceContextProvider = dispatch.InstanceContextProvider;
			if (this.provider == null)
			{
				ConstructorInfo constructorInfo = null;
				if (dispatch.Type != null)
				{
					constructorInfo = InstanceBehavior.GetConstructor(dispatch.Type);
				}
				if (this.singleton == null)
				{
					if (dispatch.Type != null && (dispatch.Type.IsAbstract || dispatch.Type.IsInterface))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxServiceTypeNotCreatable")));
					}
					if (constructorInfo == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxNoDefaultConstructor")));
					}
				}
				if (constructorInfo != null && (this.singleton == null || !this.singleton.IsWellKnown))
				{
					InvokerUtil invokerUtil = new InvokerUtil();
					CreateInstanceDelegate creator = invokerUtil.GenerateCreateInstanceDelegate(dispatch.Type, constructorInfo);
					this.provider = new InstanceProvider(creator);
				}
			}
			if (this.singleton != null)
			{
				this.singleton.Behavior = this;
			}
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06003357 RID: 13143 RVA: 0x000C65BB File Offset: 0x000C47BB
		internal bool TransactionAutoCompleteOnSessionClose
		{
			get
			{
				return this.transactionAutoCompleteOnSessionClose;
			}
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06003358 RID: 13144 RVA: 0x000C65C3 File Offset: 0x000C47C3
		internal bool ReleaseServiceInstanceOnTransactionComplete
		{
			get
			{
				return this.releaseServiceInstanceOnTransactionComplete;
			}
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06003359 RID: 13145 RVA: 0x000C65CB File Offset: 0x000C47CB
		internal IInstanceContextProvider InstanceContextProvider
		{
			get
			{
				return this.instanceContextProvider;
			}
		}

		// Token: 0x0600335A RID: 13146 RVA: 0x000C65D4 File Offset: 0x000C47D4
		internal void AfterReply(ref MessageRpc rpc, ErrorBehavior error)
		{
			InstanceContext instanceContext = rpc.InstanceContext;
			if (instanceContext != null)
			{
				try
				{
					if (rpc.Operation.ReleaseInstanceAfterCall)
					{
						if (instanceContext.State == CommunicationState.Opened)
						{
							instanceContext.ReleaseServiceInstance();
						}
					}
					else if (this.releaseServiceInstanceOnTransactionComplete && this.isSynchronized && rpc.transaction != null && (rpc.transaction.IsCompleted || rpc.Error != null))
					{
						if (instanceContext.State == CommunicationState.Opened)
						{
							instanceContext.ReleaseServiceInstance();
						}
						if (DiagnosticUtility.ShouldTraceInformation)
						{
							TraceUtility.TraceEvent(TraceEventType.Information, 917516, SR.GetString("TraceCodeTxReleaseServiceInstanceOnCompletion", new object[]
							{
								"*"
							}));
						}
					}
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					error.HandleError(ex);
				}
				try
				{
					instanceContext.UnbindRpc(ref rpc);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					error.HandleError(ex2);
				}
			}
		}

		// Token: 0x0600335B RID: 13147 RVA: 0x000C66C4 File Offset: 0x000C48C4
		internal bool CanUnload(InstanceContext instanceContext)
		{
			if (InstanceContextProviderBase.IsProviderSingleton(this.instanceContextProvider))
			{
				return false;
			}
			if (InstanceContextProviderBase.IsProviderPerCall(this.instanceContextProvider) || InstanceContextProviderBase.IsProviderSessionful(this.instanceContextProvider))
			{
				return true;
			}
			if (!this.instanceContextProvider.IsIdle(instanceContext))
			{
				this.instanceContextProvider.NotifyIdle(InstanceContext.NotifyIdleCallback, instanceContext);
				return false;
			}
			return true;
		}

		// Token: 0x0600335C RID: 13148 RVA: 0x000C6720 File Offset: 0x000C4920
		internal void EnsureInstanceContext(ref MessageRpc rpc)
		{
			if (rpc.InstanceContext == null)
			{
				rpc.InstanceContext = new InstanceContext(rpc.Host, false);
				rpc.InstanceContext.ServiceThrottle = rpc.channelHandler.InstanceContextServiceThrottle;
				rpc.MessageRpcOwnsInstanceContextThrottle = false;
			}
			rpc.OperationContext.SetInstanceContext(rpc.InstanceContext);
			rpc.InstanceContext.Behavior = this;
			if (rpc.InstanceContext.State == CommunicationState.Created)
			{
				object thisLock = rpc.InstanceContext.ThisLock;
				lock (thisLock)
				{
					if (rpc.InstanceContext.State == CommunicationState.Created)
					{
						rpc.InstanceContext.Open(rpc.Channel.CloseTimeout);
					}
				}
			}
			rpc.InstanceContext.BindRpc(ref rpc);
		}

		// Token: 0x0600335D RID: 13149 RVA: 0x000C67F0 File Offset: 0x000C49F0
		private static ConstructorInfo GetConstructor(Type type)
		{
			return type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
		}

		// Token: 0x0600335E RID: 13150 RVA: 0x000C6804 File Offset: 0x000C4A04
		internal object GetInstance(InstanceContext instanceContext)
		{
			if (this.provider == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxNoDefaultConstructor")));
			}
			bool flag = DS.InstanceProviderIsEnabled();
			Stopwatch stopwatch = null;
			if (flag)
			{
				stopwatch = Stopwatch.StartNew();
			}
			object instance = this.provider.GetInstance(instanceContext);
			if (flag)
			{
				DS.InstanceProviderGet(this.provider.GetType(), instance, stopwatch.Elapsed);
			}
			return instance;
		}

		// Token: 0x0600335F RID: 13151 RVA: 0x000C686C File Offset: 0x000C4A6C
		internal object GetInstance(InstanceContext instanceContext, Message request)
		{
			if (this.provider == null)
			{
				throw TraceUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxNoDefaultConstructor")), request);
			}
			bool flag = DS.InstanceProviderIsEnabled();
			Stopwatch stopwatch = null;
			if (flag)
			{
				stopwatch = Stopwatch.StartNew();
			}
			object instance = this.provider.GetInstance(instanceContext, request);
			if (flag)
			{
				DS.InstanceProviderGet(this.provider.GetType(), instance, stopwatch.Elapsed);
			}
			return instance;
		}

		// Token: 0x06003360 RID: 13152 RVA: 0x000C68D4 File Offset: 0x000C4AD4
		internal void Initialize(InstanceContext instanceContext)
		{
			OperationContext operationContext = OperationContext.Current;
			Message message = (operationContext != null) ? operationContext.IncomingMessage : null;
			if (operationContext != null && operationContext.InternalServiceChannel != null)
			{
				IContextChannel channel = (IContextChannel)operationContext.InternalServiceChannel.Proxy;
				this.instanceContextProvider.InitializeInstanceContext(instanceContext, message, channel);
			}
			for (int i = 0; i < this.initializers.Length; i++)
			{
				this.initializers[i].Initialize(instanceContext, message);
			}
		}

		// Token: 0x06003361 RID: 13153 RVA: 0x000C6940 File Offset: 0x000C4B40
		internal void EnsureServiceInstance(ref MessageRpc rpc)
		{
			if (rpc.Operation.ReleaseInstanceBeforeCall)
			{
				rpc.InstanceContext.ReleaseServiceInstance();
			}
			if (TD.GetServiceInstanceStartIsEnabled())
			{
				TD.GetServiceInstanceStart(rpc.EventTraceActivity);
			}
			rpc.Instance = rpc.InstanceContext.GetServiceInstance(rpc.Request);
			if (TD.GetServiceInstanceStopIsEnabled())
			{
				TD.GetServiceInstanceStop(rpc.EventTraceActivity);
			}
		}

		// Token: 0x06003362 RID: 13154 RVA: 0x000C69A0 File Offset: 0x000C4BA0
		internal void ReleaseInstance(InstanceContext instanceContext, object instance)
		{
			if (this.provider != null)
			{
				try
				{
					bool flag = DS.InstanceProviderIsEnabled();
					Stopwatch stopwatch = null;
					if (flag)
					{
						stopwatch = Stopwatch.StartNew();
					}
					this.provider.ReleaseInstance(instanceContext, instance);
					if (flag)
					{
						DS.InstanceProviderRelease(this.provider.GetType(), instance, stopwatch.Elapsed);
					}
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					this.immutableRuntime.ErrorBehavior.HandleError(ex);
				}
			}
		}

		// Token: 0x0400277C RID: 10108
		private const BindingFlags DefaultBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x0400277D RID: 10109
		private bool useSession;

		// Token: 0x0400277E RID: 10110
		private ServiceHostBase host;

		// Token: 0x0400277F RID: 10111
		private IInstanceContextInitializer[] initializers;

		// Token: 0x04002780 RID: 10112
		private IInstanceContextProvider instanceContextProvider;

		// Token: 0x04002781 RID: 10113
		private IInstanceProvider provider;

		// Token: 0x04002782 RID: 10114
		private InstanceContext singleton;

		// Token: 0x04002783 RID: 10115
		private bool transactionAutoCompleteOnSessionClose;

		// Token: 0x04002784 RID: 10116
		private bool releaseServiceInstanceOnTransactionComplete = true;

		// Token: 0x04002785 RID: 10117
		private bool isSynchronized;

		// Token: 0x04002786 RID: 10118
		private ImmutableDispatchRuntime immutableRuntime;
	}
}
