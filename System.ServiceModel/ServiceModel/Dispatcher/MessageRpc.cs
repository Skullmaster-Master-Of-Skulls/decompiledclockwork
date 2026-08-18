using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.Diagnostics;
using System.Security;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.Xml;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200058D RID: 1421
	internal struct MessageRpc
	{
		// Token: 0x060036B5 RID: 14005 RVA: 0x000D29F0 File Offset: 0x000D0BF0
		internal MessageRpc(RequestContext requestContext, Message request, DispatchOperationRuntime operation, ServiceChannel channel, ServiceHostBase host, ChannelHandler channelHandler, bool cleanThread, OperationContext operationContext, InstanceContext instanceContext, EventTraceActivity eventTraceActivity)
		{
			this.Activity = null;
			this.EventTraceActivity = eventTraceActivity;
			this.AsyncResult = null;
			this.CanSendReply = true;
			this.Channel = channel;
			this.channelHandler = channelHandler;
			this.Correlation = EmptyArray.Allocate(operation.Parent.CorrelationCount);
			this.CorrelationCallback = null;
			this.DidDeserializeRequestBody = false;
			this.TransactionMessageProperty = null;
			this.TransactedBatchContext = null;
			this.Error = null;
			this.ErrorProcessor = null;
			this.FaultInfo = new ErrorHandlerFaultInfo(request.Version.Addressing.DefaultFaultAction);
			this.HasSecurityContext = false;
			this.Host = host;
			this.Instance = null;
			this.MessageRpcOwnsInstanceContextThrottle = false;
			this.NextProcessor = null;
			this.NotUnderstoodHeaders = null;
			this.Operation = operation;
			this.OperationContext = operationContext;
			this.paused = false;
			this.ParametersDisposed = false;
			this.ReceiveContext = null;
			this.Request = request;
			this.RequestContext = requestContext;
			this.RequestContextThrewOnReply = false;
			this.SuccessfullySendReply = false;
			this.RequestVersion = request.Version;
			this.Reply = null;
			this.ReplyTimeoutHelper = default(TimeoutHelper);
			this.SecurityContext = null;
			this.InstanceContext = instanceContext;
			this.SuccessfullyBoundInstance = false;
			this.SuccessfullyIncrementedActivity = false;
			this.SuccessfullyLockedInstance = false;
			this.switchedThreads = !cleanThread;
			this.transaction = null;
			this.InputParameters = null;
			this.OutputParameters = null;
			this.ReturnParameter = null;
			this.isInstanceContextSingleton = InstanceContextProviderBase.IsProviderSingleton(this.Channel.DispatchRuntime.InstanceContextProvider);
			this.invokeContinueGate = null;
			if (!operation.IsOneWay && !operation.Parent.ManualAddressing)
			{
				this.RequestID = request.Headers.MessageId;
				this.ReplyToInfo = new RequestReplyCorrelator.ReplyToInfo(request);
			}
			else
			{
				this.RequestID = null;
				this.ReplyToInfo = default(RequestReplyCorrelator.ReplyToInfo);
			}
			this.HostingProperty = AspNetEnvironment.Current.GetHostingProperty(request, true);
			if (DiagnosticUtility.ShouldUseActivity)
			{
				this.Activity = TraceUtility.ExtractActivity(this.Request);
			}
			if (DiagnosticUtility.ShouldUseActivity || TraceUtility.ShouldPropagateActivity)
			{
				this.ResponseActivityId = ActivityIdHeader.ExtractActivityId(this.Request);
			}
			else
			{
				this.ResponseActivityId = Guid.Empty;
			}
			this.InvokeNotification = new MessageRpcInvokeNotification(this.Activity, this.channelHandler);
			if (this.EventTraceActivity == null && FxTrace.Trace.IsEnd2EndActivityTracingEnabled && this.Request != null)
			{
				this.EventTraceActivity = EventTraceActivityHelper.TryExtractActivity(this.Request, true);
			}
		}

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x060036B6 RID: 14006 RVA: 0x000D2C5E File Offset: 0x000D0E5E
		internal bool FinalizeCorrelationImplicitly
		{
			get
			{
				return this.CorrelationCallback != null && this.CorrelationCallback.IsFullyDefined;
			}
		}

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x060036B7 RID: 14007 RVA: 0x000D2C75 File Offset: 0x000D0E75
		internal bool IsPaused
		{
			get
			{
				return this.paused;
			}
		}

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x060036B8 RID: 14008 RVA: 0x000D2C7D File Offset: 0x000D0E7D
		internal bool SwitchedThreads
		{
			get
			{
				return this.switchedThreads;
			}
		}

		// Token: 0x17000D02 RID: 3330
		// (set) Token: 0x060036B9 RID: 14009 RVA: 0x000D2C85 File Offset: 0x000D0E85
		internal bool IsInstanceContextSingleton
		{
			set
			{
				this.isInstanceContextSingleton = value;
			}
		}

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x060036BA RID: 14010 RVA: 0x000D2C8E File Offset: 0x000D0E8E
		internal TransactionRpcFacet Transaction
		{
			get
			{
				if (this.transaction == null)
				{
					this.transaction = new TransactionRpcFacet(ref this);
				}
				return this.transaction;
			}
		}

		// Token: 0x060036BB RID: 14011 RVA: 0x000D2CAA File Offset: 0x000D0EAA
		internal void Abort()
		{
			this.AbortRequestContext();
			this.AbortChannel();
			this.AbortInstanceContext();
		}

		// Token: 0x060036BC RID: 14012 RVA: 0x000D2CC0 File Offset: 0x000D0EC0
		private void AbortRequestContext(RequestContext requestContext)
		{
			try
			{
				requestContext.Abort();
				ReceiveContextRPCFacet receiveContext = this.ReceiveContext;
				if (receiveContext != null)
				{
					this.ReceiveContext = null;
					IAsyncResult asyncResult = receiveContext.BeginAbandon(TimeSpan.MaxValue, MessageRpc.handleEndAbandon, new MessageRpc.CallbackState
					{
						ReceiveContext = receiveContext,
						ChannelHandler = this.channelHandler
					});
					if (asyncResult.CompletedSynchronously)
					{
						receiveContext.EndAbandon(asyncResult);
					}
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				this.channelHandler.HandleError(ex);
			}
		}

		// Token: 0x060036BD RID: 14013 RVA: 0x000D2D48 File Offset: 0x000D0F48
		internal void AbortRequestContext()
		{
			if (this.OperationContext.RequestContext != null)
			{
				this.AbortRequestContext(this.OperationContext.RequestContext);
			}
			if (this.RequestContext != null && this.RequestContext != this.OperationContext.RequestContext)
			{
				this.AbortRequestContext(this.RequestContext);
			}
			this.TraceCallDurationInDispatcherIfNecessary(false);
		}

		// Token: 0x060036BE RID: 14014 RVA: 0x000D2DA1 File Offset: 0x000D0FA1
		private void TraceCallDurationInDispatcherIfNecessary(bool requestContextWasClosedSuccessfully)
		{
			if (TD.DispatchFailedIsEnabled())
			{
				if (requestContextWasClosedSuccessfully)
				{
					TD.DispatchSuccessful(this.EventTraceActivity, this.Operation.Name);
					return;
				}
				TD.DispatchFailed(this.EventTraceActivity, this.Operation.Name);
			}
		}

		// Token: 0x060036BF RID: 14015 RVA: 0x000D2DDC File Offset: 0x000D0FDC
		internal void CloseRequestContext()
		{
			if (this.OperationContext.RequestContext != null)
			{
				this.DisposeRequestContext(this.OperationContext.RequestContext);
			}
			if (this.RequestContext != null && this.RequestContext != this.OperationContext.RequestContext)
			{
				this.DisposeRequestContext(this.RequestContext);
			}
			this.TraceCallDurationInDispatcherIfNecessary(true);
		}

		// Token: 0x060036C0 RID: 14016 RVA: 0x000D2E38 File Offset: 0x000D1038
		private void DisposeRequestContext(RequestContext context)
		{
			try
			{
				context.Close();
				ReceiveContextRPCFacet receiveContext = this.ReceiveContext;
				if (receiveContext != null)
				{
					this.ReceiveContext = null;
					IAsyncResult asyncResult = receiveContext.BeginComplete(TimeSpan.MaxValue, null, this.channelHandler, MessageRpc.handleEndComplete, new MessageRpc.CallbackState
					{
						ChannelHandler = this.channelHandler,
						ReceiveContext = receiveContext
					});
					if (asyncResult.CompletedSynchronously)
					{
						receiveContext.EndComplete(asyncResult);
					}
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				this.AbortRequestContext(context);
				this.channelHandler.HandleError(ex);
			}
		}

		// Token: 0x060036C1 RID: 14017 RVA: 0x000D2ED0 File Offset: 0x000D10D0
		private static void HandleEndAbandon(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			MessageRpc.CallbackState callbackState = (MessageRpc.CallbackState)result.AsyncState;
			try
			{
				callbackState.ReceiveContext.EndAbandon(result);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				callbackState.ChannelHandler.HandleError(ex);
			}
		}

		// Token: 0x060036C2 RID: 14018 RVA: 0x000D2F2C File Offset: 0x000D112C
		private static void HandleEndComplete(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			MessageRpc.CallbackState callbackState = (MessageRpc.CallbackState)result.AsyncState;
			try
			{
				callbackState.ReceiveContext.EndComplete(result);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				callbackState.ChannelHandler.HandleError(ex);
			}
		}

		// Token: 0x060036C3 RID: 14019 RVA: 0x000D2F88 File Offset: 0x000D1188
		internal void AbortChannel()
		{
			if (this.Channel != null && this.Channel.HasSession)
			{
				try
				{
					this.Channel.Abort();
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					this.channelHandler.HandleError(ex);
				}
			}
		}

		// Token: 0x060036C4 RID: 14020 RVA: 0x000D2FE4 File Offset: 0x000D11E4
		internal void CloseChannel()
		{
			if (this.Channel != null && this.Channel.HasSession)
			{
				try
				{
					this.Channel.Close(ChannelHandler.CloseAfterFaultTimeout);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					this.channelHandler.HandleError(ex);
				}
			}
		}

		// Token: 0x060036C5 RID: 14021 RVA: 0x000D3044 File Offset: 0x000D1244
		internal void AbortInstanceContext()
		{
			if (this.InstanceContext != null && !this.isInstanceContextSingleton)
			{
				try
				{
					this.InstanceContext.Abort();
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					this.channelHandler.HandleError(ex);
				}
			}
		}

		// Token: 0x060036C6 RID: 14022 RVA: 0x000D3098 File Offset: 0x000D1298
		internal void EnsureReceive()
		{
			using (ServiceModelActivity.BoundOperation(this.Activity))
			{
				ChannelHandler.Register(this.channelHandler);
			}
		}

		// Token: 0x060036C7 RID: 14023 RVA: 0x000D30D8 File Offset: 0x000D12D8
		private bool ProcessError(Exception e)
		{
			MessageRpcProcessor errorProcessor = this.ErrorProcessor;
			bool result;
			try
			{
				Type type = e.GetType();
				if (type.IsAssignableFrom(typeof(FaultException)))
				{
					DiagnosticUtility.TraceHandledException(e, TraceEventType.Information);
				}
				else
				{
					DiagnosticUtility.TraceHandledException(e, TraceEventType.Error);
				}
				if (TraceUtility.MessageFlowTracingOnly)
				{
					TraceUtility.SetActivityId(this.Request.Properties);
					if (Guid.Empty == DiagnosticTraceBase.ActivityId)
					{
						Guid guid = TraceUtility.ExtractActivityId(this.Request);
						if (Guid.Empty != guid)
						{
							DiagnosticTraceBase.ActivityId = guid;
						}
					}
				}
				this.Error = e;
				if (this.ErrorProcessor != null)
				{
					this.ErrorProcessor(ref this);
				}
				result = (this.Error == null);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				result = (errorProcessor != this.ErrorProcessor && this.ProcessError(ex));
			}
			return result;
		}

		// Token: 0x060036C8 RID: 14024 RVA: 0x000D31C0 File Offset: 0x000D13C0
		internal void DisposeParameters(bool excludeInput)
		{
			if (this.Operation.DisposeParameters)
			{
				this.DisposeParametersCore(excludeInput);
			}
		}

		// Token: 0x060036C9 RID: 14025 RVA: 0x000D31D8 File Offset: 0x000D13D8
		internal void DisposeParametersCore(bool excludeInput)
		{
			if (!this.ParametersDisposed)
			{
				if (!excludeInput)
				{
					this.DisposeParameterList(this.InputParameters);
				}
				this.DisposeParameterList(this.OutputParameters);
				IDisposable disposable = this.ReturnParameter as IDisposable;
				if (disposable != null)
				{
					try
					{
						disposable.Dispose();
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						this.channelHandler.HandleError(ex);
					}
				}
				this.ParametersDisposed = true;
			}
		}

		// Token: 0x060036CA RID: 14026 RVA: 0x000D3250 File Offset: 0x000D1450
		private void DisposeParameterList(object[] parameters)
		{
			if (parameters != null)
			{
				foreach (object obj in parameters)
				{
					IDisposable disposable = obj as IDisposable;
					if (disposable != null)
					{
						try
						{
							disposable.Dispose();
						}
						catch (Exception ex)
						{
							if (Fx.IsFatal(ex))
							{
								throw;
							}
							this.channelHandler.HandleError(ex);
						}
					}
				}
			}
		}

		// Token: 0x060036CB RID: 14027 RVA: 0x000D32B8 File Offset: 0x000D14B8
		internal IResumeMessageRpc Pause()
		{
			MessageRpc.Wrapper result = new MessageRpc.Wrapper(ref this);
			this.paused = true;
			return result;
		}

		// Token: 0x060036CC RID: 14028 RVA: 0x000D32D4 File Offset: 0x000D14D4
		[SecurityCritical]
		private IDisposable ApplyHostingIntegrationContext()
		{
			if (this.HostingProperty != null)
			{
				return this.ApplyHostingIntegrationContextNoInline();
			}
			return null;
		}

		// Token: 0x060036CD RID: 14029 RVA: 0x000D32E6 File Offset: 0x000D14E6
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private IDisposable ApplyHostingIntegrationContextNoInline()
		{
			return this.HostingProperty.ApplyIntegrationContext();
		}

		// Token: 0x060036CE RID: 14030 RVA: 0x000D32F4 File Offset: 0x000D14F4
		[SecuritySafeCritical]
		internal bool Process(bool isOperationContextSet)
		{
			bool result;
			using (ServiceModelActivity.BoundOperation(this.Activity))
			{
				bool flag = true;
				if (this.NextProcessor != null)
				{
					MessageRpcProcessor nextProcessor = this.NextProcessor;
					this.NextProcessor = null;
					OperationContext.Holder holder;
					OperationContext context;
					if (!isOperationContextSet)
					{
						holder = OperationContext.CurrentHolder;
						context = holder.Context;
					}
					else
					{
						holder = null;
						context = null;
					}
					this.IncrementBusyCount();
					IDisposable disposable = this.ApplyHostingIntegrationContext();
					try
					{
						if (!isOperationContextSet)
						{
							holder.Context = this.OperationContext;
						}
						nextProcessor(ref this);
						if (!this.paused)
						{
							this.OperationContext.SetClientReply(null, false);
						}
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						if (!this.ProcessError(ex) && this.FaultInfo.Fault == null)
						{
							this.Abort();
						}
					}
					finally
					{
						try
						{
							this.DecrementBusyCount();
							if (disposable != null)
							{
								disposable.Dispose();
							}
							if (!isOperationContextSet)
							{
								holder.Context = context;
							}
							flag = !this.paused;
							if (flag)
							{
								this.channelHandler.DispatchDone();
								this.OperationContext.ClearClientReplyNoThrow();
							}
						}
						catch (Exception ex2)
						{
							if (Fx.IsFatal(ex2))
							{
								throw;
							}
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperFatal(ex2.Message, ex2);
						}
					}
				}
				result = flag;
			}
			return result;
		}

		// Token: 0x060036CF RID: 14031 RVA: 0x000D3488 File Offset: 0x000D1688
		internal void UnPause()
		{
			this.paused = false;
			this.DecrementBusyCount();
		}

		// Token: 0x060036D0 RID: 14032 RVA: 0x000D3497 File Offset: 0x000D1697
		internal bool UnlockInvokeContinueGate(out IAsyncResult result)
		{
			return this.invokeContinueGate.Unlock(out result);
		}

		// Token: 0x060036D1 RID: 14033 RVA: 0x000D34A5 File Offset: 0x000D16A5
		internal void PrepareInvokeContinueGate()
		{
			this.invokeContinueGate = new SignalGate<IAsyncResult>();
		}

		// Token: 0x060036D2 RID: 14034 RVA: 0x000D34B4 File Offset: 0x000D16B4
		private void IncrementBusyCount()
		{
			if (this.Host != null)
			{
				this.Host.IncrementBusyCount();
				if (AspNetEnvironment.Current.TraceIncrementBusyCountIsEnabled())
				{
					AspNetEnvironment.Current.TraceIncrementBusyCount(SR.GetString("ServiceBusyCountTrace", new object[]
					{
						this.Operation.Action
					}));
				}
			}
		}

		// Token: 0x060036D3 RID: 14035 RVA: 0x000D3508 File Offset: 0x000D1708
		private void DecrementBusyCount()
		{
			if (this.Host != null)
			{
				this.Host.DecrementBusyCount();
				if (AspNetEnvironment.Current.TraceDecrementBusyCountIsEnabled())
				{
					AspNetEnvironment.Current.TraceDecrementBusyCount(SR.GetString("ServiceBusyCountTrace", new object[]
					{
						this.Operation.Action
					}));
				}
			}
		}

		// Token: 0x040028C1 RID: 10433
		internal readonly ServiceChannel Channel;

		// Token: 0x040028C2 RID: 10434
		internal readonly ChannelHandler channelHandler;

		// Token: 0x040028C3 RID: 10435
		internal readonly object[] Correlation;

		// Token: 0x040028C4 RID: 10436
		internal readonly ServiceHostBase Host;

		// Token: 0x040028C5 RID: 10437
		internal readonly OperationContext OperationContext;

		// Token: 0x040028C6 RID: 10438
		internal ServiceModelActivity Activity;

		// Token: 0x040028C7 RID: 10439
		internal Guid ResponseActivityId;

		// Token: 0x040028C8 RID: 10440
		internal IAsyncResult AsyncResult;

		// Token: 0x040028C9 RID: 10441
		internal bool CanSendReply;

		// Token: 0x040028CA RID: 10442
		internal bool SuccessfullySendReply;

		// Token: 0x040028CB RID: 10443
		internal CorrelationCallbackMessageProperty CorrelationCallback;

		// Token: 0x040028CC RID: 10444
		internal object[] InputParameters;

		// Token: 0x040028CD RID: 10445
		internal object[] OutputParameters;

		// Token: 0x040028CE RID: 10446
		internal object ReturnParameter;

		// Token: 0x040028CF RID: 10447
		internal bool ParametersDisposed;

		// Token: 0x040028D0 RID: 10448
		internal bool DidDeserializeRequestBody;

		// Token: 0x040028D1 RID: 10449
		internal TransactionMessageProperty TransactionMessageProperty;

		// Token: 0x040028D2 RID: 10450
		internal TransactedBatchContext TransactedBatchContext;

		// Token: 0x040028D3 RID: 10451
		internal Exception Error;

		// Token: 0x040028D4 RID: 10452
		internal MessageRpcProcessor ErrorProcessor;

		// Token: 0x040028D5 RID: 10453
		internal ErrorHandlerFaultInfo FaultInfo;

		// Token: 0x040028D6 RID: 10454
		internal bool HasSecurityContext;

		// Token: 0x040028D7 RID: 10455
		internal object Instance;

		// Token: 0x040028D8 RID: 10456
		internal bool MessageRpcOwnsInstanceContextThrottle;

		// Token: 0x040028D9 RID: 10457
		internal MessageRpcProcessor NextProcessor;

		// Token: 0x040028DA RID: 10458
		internal Collection<MessageHeaderInfo> NotUnderstoodHeaders;

		// Token: 0x040028DB RID: 10459
		internal DispatchOperationRuntime Operation;

		// Token: 0x040028DC RID: 10460
		internal Message Request;

		// Token: 0x040028DD RID: 10461
		internal RequestContext RequestContext;

		// Token: 0x040028DE RID: 10462
		internal bool RequestContextThrewOnReply;

		// Token: 0x040028DF RID: 10463
		internal UniqueId RequestID;

		// Token: 0x040028E0 RID: 10464
		internal Message Reply;

		// Token: 0x040028E1 RID: 10465
		internal TimeoutHelper ReplyTimeoutHelper;

		// Token: 0x040028E2 RID: 10466
		internal RequestReplyCorrelator.ReplyToInfo ReplyToInfo;

		// Token: 0x040028E3 RID: 10467
		internal MessageVersion RequestVersion;

		// Token: 0x040028E4 RID: 10468
		internal ServiceSecurityContext SecurityContext;

		// Token: 0x040028E5 RID: 10469
		internal InstanceContext InstanceContext;

		// Token: 0x040028E6 RID: 10470
		internal bool SuccessfullyBoundInstance;

		// Token: 0x040028E7 RID: 10471
		internal bool SuccessfullyIncrementedActivity;

		// Token: 0x040028E8 RID: 10472
		internal bool SuccessfullyLockedInstance;

		// Token: 0x040028E9 RID: 10473
		internal ReceiveContextRPCFacet ReceiveContext;

		// Token: 0x040028EA RID: 10474
		internal TransactionRpcFacet transaction;

		// Token: 0x040028EB RID: 10475
		internal IAspNetMessageProperty HostingProperty;

		// Token: 0x040028EC RID: 10476
		internal MessageRpcInvokeNotification InvokeNotification;

		// Token: 0x040028ED RID: 10477
		internal EventTraceActivity EventTraceActivity;

		// Token: 0x040028EE RID: 10478
		private static AsyncCallback handleEndComplete = Fx.ThunkCallback(new AsyncCallback(MessageRpc.HandleEndComplete));

		// Token: 0x040028EF RID: 10479
		private static AsyncCallback handleEndAbandon = Fx.ThunkCallback(new AsyncCallback(MessageRpc.HandleEndAbandon));

		// Token: 0x040028F0 RID: 10480
		private bool paused;

		// Token: 0x040028F1 RID: 10481
		private bool switchedThreads;

		// Token: 0x040028F2 RID: 10482
		private bool isInstanceContextSingleton;

		// Token: 0x040028F3 RID: 10483
		private SignalGate<IAsyncResult> invokeContinueGate;

		// Token: 0x02000C94 RID: 3220
		private class CallbackState
		{
			// Token: 0x17001B77 RID: 7031
			// (get) Token: 0x060078DF RID: 30943 RVA: 0x001C3462 File Offset: 0x001C1662
			// (set) Token: 0x060078E0 RID: 30944 RVA: 0x001C346A File Offset: 0x001C166A
			public ReceiveContextRPCFacet ReceiveContext { get; set; }

			// Token: 0x17001B78 RID: 7032
			// (get) Token: 0x060078E1 RID: 30945 RVA: 0x001C3473 File Offset: 0x001C1673
			// (set) Token: 0x060078E2 RID: 30946 RVA: 0x001C347B File Offset: 0x001C167B
			public ChannelHandler ChannelHandler { get; set; }
		}

		// Token: 0x02000C95 RID: 3221
		private class Wrapper : IResumeMessageRpc
		{
			// Token: 0x060078E4 RID: 30948 RVA: 0x001C348C File Offset: 0x001C168C
			internal Wrapper(ref MessageRpc rpc)
			{
				this.rpc = rpc;
				MessageRpcProcessor nextProcessor = rpc.NextProcessor;
				this.rpc.IncrementBusyCount();
			}

			// Token: 0x060078E5 RID: 30949 RVA: 0x001C34B2 File Offset: 0x001C16B2
			public InstanceContext GetMessageInstanceContext()
			{
				return this.rpc.InstanceContext;
			}

			// Token: 0x060078E6 RID: 30950 RVA: 0x001C34C0 File Offset: 0x001C16C0
			public void Resume(out bool alreadyResumedNoLock)
			{
				try
				{
					alreadyResumedNoLock = this.alreadyResumed;
					this.alreadyResumed = true;
					this.rpc.switchedThreads = true;
					if (this.rpc.Process(false) && !this.rpc.InvokeNotification.DidInvokerEnsurePump)
					{
						this.rpc.EnsureReceive();
					}
				}
				finally
				{
					this.rpc.DecrementBusyCount();
				}
			}

			// Token: 0x060078E7 RID: 30951 RVA: 0x001C3534 File Offset: 0x001C1734
			public void Resume(IAsyncResult result)
			{
				this.rpc.AsyncResult = result;
				this.Resume();
			}

			// Token: 0x060078E8 RID: 30952 RVA: 0x001C3548 File Offset: 0x001C1748
			public void Resume(object instance)
			{
				this.rpc.Instance = instance;
				this.Resume();
			}

			// Token: 0x060078E9 RID: 30953 RVA: 0x001C355C File Offset: 0x001C175C
			public void Resume()
			{
				using (ServiceModelActivity.BoundOperation(this.rpc.Activity, true))
				{
					bool flag;
					this.Resume(out flag);
					if (flag)
					{
						string @string = SR.GetString("SFxMultipleCallbackFromAsyncOperation", new object[]
						{
							this.rpc.Operation.Name
						});
						Exception exception = new InvalidOperationException(@string);
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
					}
				}
			}

			// Token: 0x060078EA RID: 30954 RVA: 0x001C35DC File Offset: 0x001C17DC
			public void SignalConditionalResume(IAsyncResult result)
			{
				if (this.rpc.invokeContinueGate.Signal(result))
				{
					this.rpc.AsyncResult = result;
					this.Resume();
				}
			}

			// Token: 0x040044D8 RID: 17624
			private MessageRpc rpc;

			// Token: 0x040044D9 RID: 17625
			private bool alreadyResumed;
		}
	}
}
