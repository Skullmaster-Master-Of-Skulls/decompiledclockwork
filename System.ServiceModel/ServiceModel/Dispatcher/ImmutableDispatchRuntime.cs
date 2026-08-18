using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.Security;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.Transactions;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000555 RID: 1365
	internal class ImmutableDispatchRuntime
	{
		// Token: 0x060034D3 RID: 13523 RVA: 0x000CC9DC File Offset: 0x000CABDC
		internal ImmutableDispatchRuntime(DispatchRuntime dispatch)
		{
			this.authenticationBehavior = AuthenticationBehavior.TryCreate(dispatch);
			this.authorizationBehavior = AuthorizationBehavior.TryCreate(dispatch);
			this.concurrency = new ConcurrencyBehavior(dispatch);
			this.error = new ErrorBehavior(dispatch.ChannelDispatcher);
			this.enableFaults = dispatch.EnableFaults;
			this.inputSessionShutdownHandlers = EmptyArray<IInputSessionShutdown>.ToArray(dispatch.InputSessionShutdownHandlers);
			this.instance = new InstanceBehavior(dispatch, this);
			this.isOnServer = dispatch.IsOnServer;
			this.manualAddressing = dispatch.ManualAddressing;
			this.messageInspectors = EmptyArray<IDispatchMessageInspector>.ToArray(dispatch.MessageInspectors);
			this.requestReplyCorrelator = new RequestReplyCorrelator();
			this.securityImpersonation = SecurityImpersonationBehavior.CreateIfNecessary(dispatch);
			this.requireClaimsPrincipalOnOperationContext = dispatch.RequireClaimsPrincipalOnOperationContext;
			this.impersonateOnSerializingReply = dispatch.ImpersonateOnSerializingReply;
			this.terminate = TerminatingOperationBehavior.CreateIfNecessary(dispatch);
			this.thread = new ThreadBehavior(dispatch);
			this.validateMustUnderstand = dispatch.ValidateMustUnderstand;
			this.ignoreTransactionFlow = dispatch.IgnoreTransactionMessageProperty;
			this.transaction = TransactionBehavior.CreateIfNeeded(dispatch);
			this.receiveContextEnabledChannel = dispatch.ChannelDispatcher.ReceiveContextEnabled;
			this.sendAsynchronously = dispatch.ChannelDispatcher.SendAsynchronously;
			this.parameterInspectorCorrelationOffset = dispatch.MessageInspectors.Count + dispatch.MaxCallContextInitializers;
			this.correlationCount = this.parameterInspectorCorrelationOffset + dispatch.MaxParameterInspectors;
			DispatchOperationRuntime unhandled = new DispatchOperationRuntime(dispatch.UnhandledDispatchOperation, this);
			if (dispatch.OperationSelector == null)
			{
				ImmutableDispatchRuntime.ActionDemuxer actionDemuxer = new ImmutableDispatchRuntime.ActionDemuxer();
				for (int i = 0; i < dispatch.Operations.Count; i++)
				{
					DispatchOperation dispatchOperation = dispatch.Operations[i];
					DispatchOperationRuntime operation = new DispatchOperationRuntime(dispatchOperation, this);
					actionDemuxer.Add(dispatchOperation.Action, operation);
				}
				actionDemuxer.SetUnhandled(unhandled);
				this.demuxer = actionDemuxer;
			}
			else
			{
				ImmutableDispatchRuntime.CustomDemuxer customDemuxer = new ImmutableDispatchRuntime.CustomDemuxer(dispatch.OperationSelector);
				for (int j = 0; j < dispatch.Operations.Count; j++)
				{
					DispatchOperation dispatchOperation2 = dispatch.Operations[j];
					DispatchOperationRuntime operation2 = new DispatchOperationRuntime(dispatchOperation2, this);
					customDemuxer.Add(dispatchOperation2.Name, operation2);
				}
				customDemuxer.SetUnhandled(unhandled);
				this.demuxer = customDemuxer;
			}
			this.processMessage1 = new MessageRpcProcessor(this.ProcessMessage1);
			this.processMessage11 = new MessageRpcProcessor(this.ProcessMessage11);
			this.processMessage2 = new MessageRpcProcessor(this.ProcessMessage2);
			this.processMessage3 = new MessageRpcProcessor(this.ProcessMessage3);
			this.processMessage31 = new MessageRpcProcessor(this.ProcessMessage31);
			this.processMessage4 = new MessageRpcProcessor(this.ProcessMessage4);
			this.processMessage41 = new MessageRpcProcessor(this.ProcessMessage41);
			this.processMessage5 = new MessageRpcProcessor(this.ProcessMessage5);
			this.processMessage6 = new MessageRpcProcessor(this.ProcessMessage6);
			this.processMessage7 = new MessageRpcProcessor(this.ProcessMessage7);
			this.processMessage8 = new MessageRpcProcessor(this.ProcessMessage8);
			this.processMessage9 = new MessageRpcProcessor(this.ProcessMessage9);
			this.processMessageCleanup = new MessageRpcProcessor(this.ProcessMessageCleanup);
			this.processMessageCleanupError = new MessageRpcProcessor(this.ProcessMessageCleanupError);
		}

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x060034D4 RID: 13524 RVA: 0x000CCCF0 File Offset: 0x000CAEF0
		internal int CallContextCorrelationOffset
		{
			get
			{
				return this.messageInspectors.Length;
			}
		}

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x060034D5 RID: 13525 RVA: 0x000CCCFA File Offset: 0x000CAEFA
		internal int CorrelationCount
		{
			get
			{
				return this.correlationCount;
			}
		}

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x060034D6 RID: 13526 RVA: 0x000CCD02 File Offset: 0x000CAF02
		internal bool EnableFaults
		{
			get
			{
				return this.enableFaults;
			}
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x060034D7 RID: 13527 RVA: 0x000CCD0A File Offset: 0x000CAF0A
		internal InstanceBehavior InstanceBehavior
		{
			get
			{
				return this.instance;
			}
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x060034D8 RID: 13528 RVA: 0x000CCD12 File Offset: 0x000CAF12
		internal bool IsImpersonationEnabledOnSerializingReply
		{
			get
			{
				return this.impersonateOnSerializingReply;
			}
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x060034D9 RID: 13529 RVA: 0x000CCD1A File Offset: 0x000CAF1A
		internal bool RequireClaimsPrincipalOnOperationContext
		{
			get
			{
				return this.requireClaimsPrincipalOnOperationContext;
			}
		}

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x060034DA RID: 13530 RVA: 0x000CCD22 File Offset: 0x000CAF22
		internal bool ManualAddressing
		{
			get
			{
				return this.manualAddressing;
			}
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x060034DB RID: 13531 RVA: 0x000CCD2A File Offset: 0x000CAF2A
		internal int MessageInspectorCorrelationOffset
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x060034DC RID: 13532 RVA: 0x000CCD2D File Offset: 0x000CAF2D
		internal int ParameterInspectorCorrelationOffset
		{
			get
			{
				return this.parameterInspectorCorrelationOffset;
			}
		}

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x060034DD RID: 13533 RVA: 0x000CCD35 File Offset: 0x000CAF35
		internal IRequestReplyCorrelator RequestReplyCorrelator
		{
			get
			{
				return this.requestReplyCorrelator;
			}
		}

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x060034DE RID: 13534 RVA: 0x000CCD3D File Offset: 0x000CAF3D
		internal SecurityImpersonationBehavior SecurityImpersonation
		{
			get
			{
				return this.securityImpersonation;
			}
		}

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x060034DF RID: 13535 RVA: 0x000CCD45 File Offset: 0x000CAF45
		internal bool ValidateMustUnderstand
		{
			get
			{
				return this.validateMustUnderstand;
			}
		}

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x060034E0 RID: 13536 RVA: 0x000CCD4D File Offset: 0x000CAF4D
		internal ErrorBehavior ErrorBehavior
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x060034E1 RID: 13537 RVA: 0x000CCD55 File Offset: 0x000CAF55
		private bool AcquireDynamicInstanceContext(ref MessageRpc rpc)
		{
			return rpc.InstanceContext.QuotaThrottle == null || this.AcquireDynamicInstanceContextCore(ref rpc);
		}

		// Token: 0x060034E2 RID: 13538 RVA: 0x000CCD70 File Offset: 0x000CAF70
		private bool AcquireDynamicInstanceContextCore(ref MessageRpc rpc)
		{
			bool flag = rpc.InstanceContext.QuotaThrottle.Acquire(rpc.Pause());
			if (flag)
			{
				rpc.UnPause();
			}
			return flag;
		}

		// Token: 0x060034E3 RID: 13539 RVA: 0x000CCD9E File Offset: 0x000CAF9E
		internal void AfterReceiveRequest(ref MessageRpc rpc)
		{
			if (this.messageInspectors.Length != 0)
			{
				this.AfterReceiveRequestCore(ref rpc);
			}
		}

		// Token: 0x060034E4 RID: 13540 RVA: 0x000CCDB0 File Offset: 0x000CAFB0
		internal void AfterReceiveRequestCore(ref MessageRpc rpc)
		{
			int messageInspectorCorrelationOffset = this.MessageInspectorCorrelationOffset;
			try
			{
				bool flag = DS.MessageInspectorIsEnabled();
				Stopwatch stopwatch = null;
				if (flag)
				{
					stopwatch = new Stopwatch();
				}
				for (int i = 0; i < this.messageInspectors.Length; i++)
				{
					if (flag)
					{
						stopwatch.Restart();
					}
					rpc.Correlation[messageInspectorCorrelationOffset + i] = this.messageInspectors[i].AfterReceiveRequest(ref rpc.Request, (IClientChannel)rpc.Channel.Proxy, rpc.InstanceContext);
					if (flag)
					{
						DS.DispatchMessageInspectorAfterReceive(this.messageInspectors[i].GetType(), stopwatch.Elapsed);
					}
					if (TD.MessageInspectorAfterReceiveInvokedIsEnabled())
					{
						TD.MessageInspectorAfterReceiveInvoked(rpc.EventTraceActivity, this.messageInspectors[i].GetType().FullName);
					}
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				if (ErrorBehavior.ShouldRethrowExceptionAsIs(ex))
				{
					throw;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(ex);
			}
		}

		// Token: 0x060034E5 RID: 13541 RVA: 0x000CCEA0 File Offset: 0x000CB0A0
		private void BeforeSendReply(ref MessageRpc rpc, ref Exception exception, ref bool thereIsAnUnhandledException)
		{
			if (this.messageInspectors.Length != 0)
			{
				this.BeforeSendReplyCore(ref rpc, ref exception, ref thereIsAnUnhandledException);
			}
		}

		// Token: 0x060034E6 RID: 13542 RVA: 0x000CCEB4 File Offset: 0x000CB0B4
		internal void BeforeSendReplyCore(ref MessageRpc rpc, ref Exception exception, ref bool thereIsAnUnhandledException)
		{
			int messageInspectorCorrelationOffset = this.MessageInspectorCorrelationOffset;
			bool flag = DS.MessageInspectorIsEnabled();
			Stopwatch stopwatch = null;
			if (flag)
			{
				stopwatch = new Stopwatch();
			}
			for (int i = 0; i < this.messageInspectors.Length; i++)
			{
				try
				{
					Message reply = rpc.Reply;
					Message message = reply;
					if (flag)
					{
						stopwatch.Restart();
					}
					this.messageInspectors[i].BeforeSendReply(ref message, rpc.Correlation[messageInspectorCorrelationOffset + i]);
					if (flag)
					{
						DS.DispatchMessageInspectorBeforeSend(this.messageInspectors[i].GetType(), stopwatch.Elapsed);
					}
					if (TD.MessageInspectorBeforeSendInvokedIsEnabled())
					{
						TD.MessageInspectorBeforeSendInvoked(rpc.EventTraceActivity, this.messageInspectors[i].GetType().FullName);
					}
					if (message == null && reply != null)
					{
						string @string = SR.GetString("SFxNullReplyFromExtension2", new object[]
						{
							this.messageInspectors[i].GetType().ToString(),
							rpc.Operation.Name ?? ""
						});
						ErrorBehavior.ThrowAndCatch(new InvalidOperationException(@string));
					}
					rpc.Reply = message;
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					if (!ErrorBehavior.ShouldRethrowExceptionAsIs(ex))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(ex);
					}
					if (exception == null)
					{
						exception = ex;
					}
					thereIsAnUnhandledException = (!this.error.HandleError(ex) | thereIsAnUnhandledException);
				}
			}
		}

		// Token: 0x060034E7 RID: 13543 RVA: 0x000CD010 File Offset: 0x000CB210
		private void FinalizeCorrelation(ref MessageRpc rpc)
		{
			Message reply = rpc.Reply;
			if (reply != null && rpc.Error == null)
			{
				if (rpc.transaction != null && rpc.transaction.Current != null && rpc.transaction.Current.TransactionInformation.Status != TransactionStatus.Active)
				{
					return;
				}
				CorrelationCallbackMessageProperty correlationCallbackMessageProperty;
				if (CorrelationCallbackMessageProperty.TryGet(reply, out correlationCallbackMessageProperty))
				{
					if (correlationCallbackMessageProperty.IsFullyDefined)
					{
						try
						{
							rpc.RequestContextThrewOnReply = true;
							rpc.CorrelationCallback = correlationCallbackMessageProperty;
							rpc.Reply = rpc.CorrelationCallback.FinalizeCorrelation(reply, rpc.ReplyTimeoutHelper.RemainingTime());
							return;
						}
						catch (Exception exception)
						{
							if (Fx.IsFatal(exception))
							{
								throw;
							}
							if (!this.error.HandleError(exception))
							{
								rpc.CorrelationCallback = null;
								rpc.CanSendReply = false;
							}
							return;
						}
					}
					rpc.CorrelationCallback = new ImmutableDispatchRuntime.RpcCorrelationCallbackMessageProperty(correlationCallbackMessageProperty, this, ref rpc);
					reply.Properties[CorrelationCallbackMessageProperty.Name] = rpc.CorrelationCallback;
				}
			}
		}

		// Token: 0x060034E8 RID: 13544 RVA: 0x000CD108 File Offset: 0x000CB308
		private void BeginFinalizeCorrelation(ref MessageRpc rpc)
		{
			Message reply = rpc.Reply;
			if (reply != null && rpc.Error == null)
			{
				if (rpc.transaction != null && rpc.transaction.Current != null && rpc.transaction.Current.TransactionInformation.Status != TransactionStatus.Active)
				{
					return;
				}
				CorrelationCallbackMessageProperty correlationCallbackMessageProperty;
				if (CorrelationCallbackMessageProperty.TryGet(reply, out correlationCallbackMessageProperty))
				{
					if (correlationCallbackMessageProperty.IsFullyDefined)
					{
						bool flag = false;
						try
						{
							rpc.RequestContextThrewOnReply = true;
							rpc.CorrelationCallback = correlationCallbackMessageProperty;
							IResumeMessageRpc state = rpc.Pause();
							rpc.AsyncResult = rpc.CorrelationCallback.BeginFinalizeCorrelation(reply, rpc.ReplyTimeoutHelper.RemainingTime(), ImmutableDispatchRuntime.onFinalizeCorrelationCompleted, state);
							flag = true;
							if (rpc.AsyncResult.CompletedSynchronously)
							{
								rpc.UnPause();
							}
							return;
						}
						catch (Exception exception)
						{
							if (Fx.IsFatal(exception))
							{
								throw;
							}
							if (!this.error.HandleError(exception))
							{
								rpc.CorrelationCallback = null;
								rpc.CanSendReply = false;
							}
							return;
						}
						finally
						{
							if (!flag)
							{
								rpc.UnPause();
							}
						}
					}
					rpc.CorrelationCallback = new ImmutableDispatchRuntime.RpcCorrelationCallbackMessageProperty(correlationCallbackMessageProperty, this, ref rpc);
					reply.Properties[CorrelationCallbackMessageProperty.Name] = rpc.CorrelationCallback;
				}
			}
		}

		// Token: 0x060034E9 RID: 13545 RVA: 0x000CD240 File Offset: 0x000CB440
		private void Reply(ref MessageRpc rpc)
		{
			rpc.RequestContextThrewOnReply = true;
			rpc.SuccessfullySendReply = false;
			try
			{
				rpc.RequestContext.Reply(rpc.Reply, rpc.ReplyTimeoutHelper.RemainingTime());
				rpc.RequestContextThrewOnReply = false;
				rpc.SuccessfullySendReply = true;
				if (TD.DispatchMessageStopIsEnabled())
				{
					TD.DispatchMessageStop(rpc.EventTraceActivity);
				}
			}
			catch (CommunicationException ex)
			{
				this.error.HandleError(ex);
			}
			catch (TimeoutException ex2)
			{
				this.error.HandleError(ex2);
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				if (DiagnosticUtility.ShouldTraceError)
				{
					TraceUtility.TraceEvent(TraceEventType.Error, 524340, SR.GetString("TraceCodeServiceOperationExceptionOnReply"), this, exception);
				}
				if (!this.error.HandleError(exception))
				{
					rpc.RequestContextThrewOnReply = true;
					rpc.CanSendReply = false;
				}
			}
		}

		// Token: 0x060034EA RID: 13546 RVA: 0x000CD328 File Offset: 0x000CB528
		private void BeginReply(ref MessageRpc rpc)
		{
			bool flag = false;
			try
			{
				IResumeMessageRpc state = rpc.Pause();
				rpc.AsyncResult = rpc.RequestContext.BeginReply(rpc.Reply, rpc.ReplyTimeoutHelper.RemainingTime(), ImmutableDispatchRuntime.onReplyCompleted, state);
				flag = true;
				if (rpc.AsyncResult.CompletedSynchronously)
				{
					rpc.UnPause();
				}
			}
			catch (CommunicationException ex)
			{
				this.error.HandleError(ex);
			}
			catch (TimeoutException ex2)
			{
				this.error.HandleError(ex2);
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				if (DiagnosticUtility.ShouldTraceError)
				{
					TraceUtility.TraceEvent(TraceEventType.Error, 524340, SR.GetString("TraceCodeServiceOperationExceptionOnReply"), this, exception);
				}
				if (!this.error.HandleError(exception))
				{
					rpc.RequestContextThrewOnReply = true;
					rpc.CanSendReply = false;
				}
			}
			finally
			{
				if (!flag)
				{
					rpc.UnPause();
				}
			}
		}

		// Token: 0x060034EB RID: 13547 RVA: 0x000CD428 File Offset: 0x000CB628
		internal bool Dispatch(ref MessageRpc rpc, bool isOperationContextSet)
		{
			rpc.ErrorProcessor = this.processMessage8;
			rpc.NextProcessor = this.processMessage1;
			return rpc.Process(isOperationContextSet);
		}

		// Token: 0x060034EC RID: 13548 RVA: 0x000CD44C File Offset: 0x000CB64C
		private void EndFinalizeCorrelation(ref MessageRpc rpc)
		{
			try
			{
				rpc.Reply = rpc.CorrelationCallback.EndFinalizeCorrelation(rpc.AsyncResult);
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				if (!this.error.HandleError(exception))
				{
					rpc.CanSendReply = false;
				}
			}
		}

		// Token: 0x060034ED RID: 13549 RVA: 0x000CD4A4 File Offset: 0x000CB6A4
		private bool EndReply(ref MessageRpc rpc)
		{
			bool result = false;
			try
			{
				rpc.RequestContext.EndReply(rpc.AsyncResult);
				rpc.RequestContextThrewOnReply = false;
				result = true;
				if (TD.DispatchMessageStopIsEnabled())
				{
					TD.DispatchMessageStop(rpc.EventTraceActivity);
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				this.error.HandleError(exception);
			}
			return result;
		}

		// Token: 0x060034EE RID: 13550 RVA: 0x000CD50C File Offset: 0x000CB70C
		internal void InputSessionDoneReceiving(ServiceChannel channel)
		{
			if (this.inputSessionShutdownHandlers.Length != 0)
			{
				this.InputSessionDoneReceivingCore(channel);
			}
		}

		// Token: 0x060034EF RID: 13551 RVA: 0x000CD520 File Offset: 0x000CB720
		private void InputSessionDoneReceivingCore(ServiceChannel channel)
		{
			IDuplexContextChannel duplexContextChannel = channel.Proxy as IDuplexContextChannel;
			if (duplexContextChannel != null)
			{
				IInputSessionShutdown[] array = this.inputSessionShutdownHandlers;
				try
				{
					for (int i = 0; i < array.Length; i++)
					{
						array[i].DoneReceiving(duplexContextChannel);
					}
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					if (!this.error.HandleError(exception))
					{
						duplexContextChannel.Abort();
					}
				}
			}
		}

		// Token: 0x060034F0 RID: 13552 RVA: 0x000CD590 File Offset: 0x000CB790
		internal bool IsConcurrent(ref MessageRpc rpc)
		{
			return this.concurrency.IsConcurrent(ref rpc);
		}

		// Token: 0x060034F1 RID: 13553 RVA: 0x000CD59E File Offset: 0x000CB79E
		internal void InputSessionFaulted(ServiceChannel channel)
		{
			if (this.inputSessionShutdownHandlers.Length != 0)
			{
				this.InputSessionFaultedCore(channel);
			}
		}

		// Token: 0x060034F2 RID: 13554 RVA: 0x000CD5B0 File Offset: 0x000CB7B0
		private void InputSessionFaultedCore(ServiceChannel channel)
		{
			IDuplexContextChannel duplexContextChannel = channel.Proxy as IDuplexContextChannel;
			if (duplexContextChannel != null)
			{
				IInputSessionShutdown[] array = this.inputSessionShutdownHandlers;
				try
				{
					for (int i = 0; i < array.Length; i++)
					{
						array[i].ChannelFaulted(duplexContextChannel);
					}
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					if (!this.error.HandleError(exception))
					{
						duplexContextChannel.Abort();
					}
				}
			}
		}

		// Token: 0x060034F3 RID: 13555 RVA: 0x000CD620 File Offset: 0x000CB820
		internal static void GotDynamicInstanceContext(object state)
		{
			bool flag;
			((IResumeMessageRpc)state).Resume(out flag);
		}

		// Token: 0x060034F4 RID: 13556 RVA: 0x000CD63C File Offset: 0x000CB83C
		private void AddMessageProperties(Message message, OperationContext context, ServiceChannel replyChannel)
		{
			if (context.InternalServiceChannel == replyChannel)
			{
				if (context.HasOutgoingMessageHeaders)
				{
					message.Headers.CopyHeadersFrom(context.OutgoingMessageHeaders);
				}
				if (context.HasOutgoingMessageProperties)
				{
					message.Properties.MergeProperties(context.OutgoingMessageProperties);
				}
			}
		}

		// Token: 0x060034F5 RID: 13557 RVA: 0x000CD67C File Offset: 0x000CB87C
		private static void OnFinalizeCorrelationCompletedCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			IResumeMessageRpc resumeMessageRpc = result.AsyncState as IResumeMessageRpc;
			if (resumeMessageRpc == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SFxInvalidAsyncResultState0"));
			}
			resumeMessageRpc.Resume(result);
		}

		// Token: 0x060034F6 RID: 13558 RVA: 0x000CD6C0 File Offset: 0x000CB8C0
		private static void OnReplyCompletedCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			IResumeMessageRpc resumeMessageRpc = result.AsyncState as IResumeMessageRpc;
			if (resumeMessageRpc == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SFxInvalidAsyncResultState0"));
			}
			resumeMessageRpc.Resume(result);
		}

		// Token: 0x060034F7 RID: 13559 RVA: 0x000CD704 File Offset: 0x000CB904
		private void PrepareReply(ref MessageRpc rpc)
		{
			RequestContext requestContext = rpc.OperationContext.RequestContext;
			Exception ex = null;
			bool flag = false;
			if (!rpc.Operation.IsOneWay)
			{
				if (DiagnosticUtility.ShouldTraceWarning)
				{
					if (rpc.Reply == null && requestContext != null)
					{
						TraceUtility.TraceEvent(TraceEventType.Warning, 524338, SR.GetString("TraceCodeServiceOperationMissingReply", new object[]
						{
							rpc.Operation.Name ?? string.Empty
						}), null, null);
					}
					else if (requestContext == null && rpc.Reply != null)
					{
						TraceUtility.TraceEvent(TraceEventType.Warning, 524339, SR.GetString("TraceCodeServiceOperationMissingReplyContext", new object[]
						{
							rpc.Operation.Name ?? string.Empty
						}), null, null);
					}
				}
				if (requestContext != null && rpc.Reply != null)
				{
					try
					{
						rpc.CanSendReply = this.PrepareAndAddressReply(ref rpc);
					}
					catch (Exception ex2)
					{
						if (Fx.IsFatal(ex2))
						{
							throw;
						}
						flag = (!this.error.HandleError(ex2) || flag);
						ex = ex2;
					}
				}
			}
			this.BeforeSendReply(ref rpc, ref ex, ref flag);
			if (rpc.Operation.IsOneWay)
			{
				rpc.CanSendReply = false;
			}
			if (!rpc.Operation.IsOneWay && requestContext != null && rpc.Reply != null)
			{
				if (ex == null)
				{
					return;
				}
				rpc.Error = ex;
				this.error.ProvideOnlyFaultOfLastResort(ref rpc);
				try
				{
					rpc.CanSendReply = this.PrepareAndAddressReply(ref rpc);
					return;
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					this.error.HandleError(exception);
					return;
				}
			}
			if (ex != null && flag)
			{
				rpc.Abort();
			}
		}

		// Token: 0x060034F8 RID: 13560 RVA: 0x000CD89C File Offset: 0x000CBA9C
		private bool PrepareAndAddressReply(ref MessageRpc rpc)
		{
			bool result = true;
			if (!this.manualAddressing)
			{
				if (rpc.RequestID != null)
				{
					System.ServiceModel.Channels.RequestReplyCorrelator.PrepareReply(rpc.Reply, rpc.RequestID);
				}
				if (!rpc.Channel.HasSession)
				{
					result = System.ServiceModel.Channels.RequestReplyCorrelator.AddressReply(rpc.Reply, rpc.ReplyToInfo);
				}
			}
			this.AddMessageProperties(rpc.Reply, rpc.OperationContext, rpc.Channel);
			if (FxTrace.Trace.IsEnd2EndActivityTracingEnabled && rpc.EventTraceActivity != null)
			{
				rpc.Reply.Properties[EventTraceActivity.Name] = rpc.EventTraceActivity;
			}
			return result;
		}

		// Token: 0x060034F9 RID: 13561 RVA: 0x000CD933 File Offset: 0x000CBB33
		internal DispatchOperationRuntime GetOperation(ref Message message)
		{
			return this.demuxer.GetOperation(ref message);
		}

		// Token: 0x060034FA RID: 13562 RVA: 0x000CD944 File Offset: 0x000CBB44
		internal void ProcessMessage1(ref MessageRpc rpc)
		{
			rpc.NextProcessor = this.processMessage11;
			if (DS.ServiceThrottleIsEnabled())
			{
				DS.Throttled(rpc.Request);
			}
			if (this.receiveContextEnabledChannel)
			{
				ReceiveContextRPCFacet.CreateIfRequired(this, ref rpc);
			}
			if (!rpc.IsPaused)
			{
				this.ProcessMessage11(ref rpc);
				return;
			}
			if (this.isOnServer && DiagnosticUtility.ShouldTraceInformation && !this.didTraceProcessMessage1)
			{
				this.didTraceProcessMessage1 = true;
				TraceUtility.TraceEvent(TraceEventType.Information, 524327, SR.GetString("TraceCodeProcessMessage31Paused", new object[]
				{
					rpc.Channel.DispatchRuntime.EndpointDispatcher.ContractName,
					rpc.Channel.DispatchRuntime.EndpointDispatcher.EndpointAddress
				}));
			}
		}

		// Token: 0x060034FB RID: 13563 RVA: 0x000CD9F8 File Offset: 0x000CBBF8
		internal void ProcessMessage11(ref MessageRpc rpc)
		{
			rpc.NextProcessor = this.processMessage2;
			if (rpc.Operation.IsOneWay)
			{
				rpc.RequestContext.Reply(null);
				rpc.OperationContext.RequestContext = null;
			}
			else
			{
				if (!rpc.Channel.IsReplyChannel && rpc.RequestID == null && rpc.Operation.Action != "*")
				{
					CommunicationException exception = new CommunicationException(SR.GetString("SFxOneWayMessageToTwoWayMethod0"));
					throw TraceUtility.ThrowHelperError(exception, rpc.Request);
				}
				if (!this.manualAddressing)
				{
					EndpointAddress replyTo = rpc.ReplyToInfo.ReplyTo;
					if (replyTo != null && replyTo.IsNone && rpc.Channel.IsReplyChannel)
					{
						CommunicationException exception2 = new CommunicationException(SR.GetString("SFxRequestReplyNone"));
						throw TraceUtility.ThrowHelperError(exception2, rpc.Request);
					}
					if (this.isOnServer)
					{
						EndpointAddress remoteAddress = rpc.Channel.RemoteAddress;
						if (remoteAddress != null && !remoteAddress.IsAnonymous)
						{
							MessageHeaders headers = rpc.Request.Headers;
							Uri uri = remoteAddress.Uri;
							if (replyTo != null && !replyTo.IsAnonymous && uri != replyTo.Uri)
							{
								string @string = SR.GetString("SFxRequestHasInvalidReplyToOnServer", new object[]
								{
									replyTo.Uri,
									uri
								});
								Exception exception3 = new InvalidOperationException(@string);
								throw TraceUtility.ThrowHelperError(exception3, rpc.Request);
							}
							EndpointAddress faultTo = headers.FaultTo;
							if (faultTo != null && !faultTo.IsAnonymous && uri != faultTo.Uri)
							{
								string string2 = SR.GetString("SFxRequestHasInvalidFaultToOnServer", new object[]
								{
									faultTo.Uri,
									uri
								});
								Exception exception4 = new InvalidOperationException(string2);
								throw TraceUtility.ThrowHelperError(exception4, rpc.Request);
							}
							if (rpc.RequestVersion.Addressing == AddressingVersion.WSAddressingAugust2004)
							{
								EndpointAddress from = headers.From;
								if (from != null && !from.IsAnonymous && uri != from.Uri)
								{
									string string3 = SR.GetString("SFxRequestHasInvalidFromOnServer", new object[]
									{
										from.Uri,
										uri
									});
									Exception exception5 = new InvalidOperationException(string3);
									throw TraceUtility.ThrowHelperError(exception5, rpc.Request);
								}
							}
						}
					}
				}
			}
			if (this.concurrency.IsConcurrent(ref rpc))
			{
				rpc.Channel.IncrementActivity();
				rpc.SuccessfullyIncrementedActivity = true;
			}
			if (this.authenticationBehavior != null)
			{
				this.authenticationBehavior.Authenticate(ref rpc);
			}
			if (this.authorizationBehavior != null)
			{
				this.authorizationBehavior.Authorize(ref rpc);
			}
			this.instance.EnsureInstanceContext(ref rpc);
			this.TransferChannelFromPendingList(ref rpc);
			this.AcquireDynamicInstanceContext(ref rpc);
			if (!rpc.IsPaused)
			{
				this.ProcessMessage2(ref rpc);
			}
		}

		// Token: 0x060034FC RID: 13564 RVA: 0x000CDCBC File Offset: 0x000CBEBC
		private void ProcessMessage2(ref MessageRpc rpc)
		{
			rpc.NextProcessor = this.processMessage3;
			this.AfterReceiveRequest(ref rpc);
			if (!this.ignoreTransactionFlow)
			{
				rpc.TransactionMessageProperty = TransactionMessageProperty.TryGet(rpc.Request);
			}
			this.concurrency.LockInstance(ref rpc);
			if (!rpc.IsPaused)
			{
				this.ProcessMessage3(ref rpc);
				return;
			}
			if (this.isOnServer && DiagnosticUtility.ShouldTraceInformation && !this.didTraceProcessMessage2)
			{
				this.didTraceProcessMessage2 = true;
				TraceUtility.TraceEvent(TraceEventType.Information, 524327, SR.GetString("TraceCodeProcessMessage2Paused", new object[]
				{
					rpc.Channel.DispatchRuntime.EndpointDispatcher.ContractName,
					rpc.Channel.DispatchRuntime.EndpointDispatcher.EndpointAddress
				}));
			}
		}

		// Token: 0x060034FD RID: 13565 RVA: 0x000CDD7C File Offset: 0x000CBF7C
		private void ProcessMessage3(ref MessageRpc rpc)
		{
			rpc.NextProcessor = this.processMessage31;
			rpc.SuccessfullyLockedInstance = true;
			if (this.transaction != null)
			{
				this.transaction.ResolveTransaction(ref rpc);
				if (rpc.Operation.TransactionRequired)
				{
					this.transaction.SetCurrent(ref rpc);
				}
			}
			if (!rpc.IsPaused)
			{
				this.ProcessMessage31(ref rpc);
				return;
			}
			if (this.isOnServer && DiagnosticUtility.ShouldTraceInformation && !this.didTraceProcessMessage3)
			{
				this.didTraceProcessMessage3 = true;
				TraceUtility.TraceEvent(TraceEventType.Information, 524327, SR.GetString("TraceCodeProcessMessage3Paused", new object[]
				{
					rpc.Channel.DispatchRuntime.EndpointDispatcher.ContractName,
					rpc.Channel.DispatchRuntime.EndpointDispatcher.EndpointAddress
				}));
			}
		}

		// Token: 0x060034FE RID: 13566 RVA: 0x000CDE44 File Offset: 0x000CC044
		private void ProcessMessage31(ref MessageRpc rpc)
		{
			rpc.NextProcessor = this.processMessage4;
			if (this.transaction != null && rpc.Operation.TransactionRequired)
			{
				ReceiveContextRPCFacet receiveContext = rpc.ReceiveContext;
				if (receiveContext != null)
				{
					rpc.ReceiveContext = null;
					receiveContext.Complete(this, ref rpc, TimeSpan.MaxValue, rpc.Transaction.Current);
				}
			}
			if (!rpc.IsPaused)
			{
				this.ProcessMessage4(ref rpc);
				return;
			}
			if (this.isOnServer && DiagnosticUtility.ShouldTraceInformation && !this.didTraceProcessMessage31)
			{
				this.didTraceProcessMessage31 = true;
				TraceUtility.TraceEvent(TraceEventType.Information, 524327, SR.GetString("TraceCodeProcessMessage31Paused", new object[]
				{
					rpc.Channel.DispatchRuntime.EndpointDispatcher.ContractName,
					rpc.Channel.DispatchRuntime.EndpointDispatcher.EndpointAddress
				}));
			}
		}

		// Token: 0x060034FF RID: 13567 RVA: 0x000CDF14 File Offset: 0x000CC114
		private void ProcessMessage4(ref MessageRpc rpc)
		{
			rpc.NextProcessor = this.processMessage41;
			try
			{
				this.thread.BindThread(ref rpc);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperFatal(ex.Message, ex);
			}
			if (!rpc.IsPaused)
			{
				this.ProcessMessage41(ref rpc);
				return;
			}
			if (this.isOnServer && DiagnosticUtility.ShouldTraceInformation && !this.didTraceProcessMessage4)
			{
				this.didTraceProcessMessage4 = true;
				TraceUtility.TraceEvent(TraceEventType.Information, 524327, SR.GetString("TraceCodeProcessMessage4Paused", new object[]
				{
					rpc.Channel.DispatchRuntime.EndpointDispatcher.ContractName,
					rpc.Channel.DispatchRuntime.EndpointDispatcher.EndpointAddress
				}));
			}
		}

		// Token: 0x06003500 RID: 13568 RVA: 0x000CDFE4 File Offset: 0x000CC1E4
		private void ProcessMessage41(ref MessageRpc rpc)
		{
			rpc.NextProcessor = this.processMessage5;
			if (this.concurrency.IsConcurrent(ref rpc) && !(rpc.Operation.Invoker is IManualConcurrencyOperationInvoker))
			{
				rpc.EnsureReceive();
			}
			this.instance.EnsureServiceInstance(ref rpc);
			if (!rpc.IsPaused)
			{
				this.ProcessMessage5(ref rpc);
				return;
			}
			if (this.isOnServer && DiagnosticUtility.ShouldTraceInformation && !this.didTraceProcessMessage41)
			{
				this.didTraceProcessMessage41 = true;
				TraceUtility.TraceEvent(TraceEventType.Information, 524327, SR.GetString("TraceCodeProcessMessage4Paused", new object[]
				{
					rpc.Channel.DispatchRuntime.EndpointDispatcher.ContractName,
					rpc.Channel.DispatchRuntime.EndpointDispatcher.EndpointAddress
				}));
			}
		}

		// Token: 0x06003501 RID: 13569 RVA: 0x000CE0A8 File Offset: 0x000CC2A8
		private void ProcessMessage5(ref MessageRpc rpc)
		{
			rpc.NextProcessor = this.processMessage6;
			try
			{
				bool flag = false;
				try
				{
					if (!rpc.Operation.IsSynchronous)
					{
						rpc.PrepareInvokeContinueGate();
					}
					if (this.transaction != null)
					{
						this.transaction.InitializeCallContext(ref rpc);
					}
					this.SetActivityIdOnThread(ref rpc);
					rpc.Operation.InvokeBegin(ref rpc);
					flag = true;
				}
				finally
				{
					try
					{
						try
						{
							if (this.transaction != null)
							{
								this.transaction.ClearCallContext(ref rpc);
							}
						}
						finally
						{
							if (!rpc.Operation.IsSynchronous && rpc.IsPaused && rpc.UnlockInvokeContinueGate(out rpc.AsyncResult))
							{
								rpc.UnPause();
							}
						}
					}
					catch (Exception exception)
					{
						if (Fx.IsFatal(exception))
						{
							throw;
						}
						if (flag && (rpc.Operation.IsSynchronous || !rpc.IsPaused))
						{
							throw;
						}
						this.error.HandleError(exception);
					}
				}
			}
			catch
			{
				throw;
			}
			if (!rpc.IsPaused)
			{
				this.ProcessMessage6(ref rpc);
			}
		}

		// Token: 0x06003502 RID: 13570 RVA: 0x000CE1C4 File Offset: 0x000CC3C4
		private void ProcessMessage6(ref MessageRpc rpc)
		{
			rpc.NextProcessor = (rpc.Operation.IsSynchronous ? this.processMessage8 : this.processMessage7);
			try
			{
				this.thread.BindEndThread(ref rpc);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperFatal(ex.Message, ex);
			}
			if (!rpc.IsPaused)
			{
				if (rpc.Operation.IsSynchronous)
				{
					this.ProcessMessage8(ref rpc);
					return;
				}
				this.ProcessMessage7(ref rpc);
			}
		}

		// Token: 0x06003503 RID: 13571 RVA: 0x000CE254 File Offset: 0x000CC454
		private void ProcessMessage7(ref MessageRpc rpc)
		{
			rpc.NextProcessor = null;
			try
			{
				bool flag = false;
				try
				{
					if (this.transaction != null)
					{
						this.transaction.InitializeCallContext(ref rpc);
					}
					rpc.Operation.InvokeEnd(ref rpc);
					flag = true;
				}
				finally
				{
					try
					{
						if (this.transaction != null)
						{
							this.transaction.ClearCallContext(ref rpc);
						}
					}
					catch (Exception exception)
					{
						if (Fx.IsFatal(exception))
						{
							throw;
						}
						if (flag)
						{
							throw;
						}
						this.error.HandleError(exception);
					}
				}
			}
			catch
			{
				throw;
			}
			this.ProcessMessage8(ref rpc);
		}

		// Token: 0x06003504 RID: 13572 RVA: 0x000CE2FC File Offset: 0x000CC4FC
		private void ProcessMessage8(ref MessageRpc rpc)
		{
			rpc.NextProcessor = this.processMessage9;
			try
			{
				this.error.ProvideMessageFault(ref rpc);
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				this.error.HandleError(exception);
			}
			this.PrepareReply(ref rpc);
			if (rpc.CanSendReply)
			{
				rpc.ReplyTimeoutHelper = new TimeoutHelper(rpc.Channel.OperationTimeout);
				if (this.sendAsynchronously)
				{
					this.BeginFinalizeCorrelation(ref rpc);
				}
				else
				{
					this.FinalizeCorrelation(ref rpc);
				}
			}
			if (!rpc.IsPaused)
			{
				this.ProcessMessage9(ref rpc);
			}
		}

		// Token: 0x06003505 RID: 13573 RVA: 0x000CE39C File Offset: 0x000CC59C
		private void ProcessMessage9(ref MessageRpc rpc)
		{
			rpc.NextProcessor = this.processMessageCleanup;
			if (rpc.FinalizeCorrelationImplicitly && this.sendAsynchronously)
			{
				this.EndFinalizeCorrelation(ref rpc);
			}
			if (rpc.CorrelationCallback == null || rpc.FinalizeCorrelationImplicitly)
			{
				this.ResolveTransactionOutcome(ref rpc);
			}
			if (rpc.CanSendReply)
			{
				if (rpc.Reply != null)
				{
					TraceUtility.MessageFlowAtMessageSent(rpc.Reply, rpc.EventTraceActivity);
				}
				if (this.sendAsynchronously)
				{
					this.BeginReply(ref rpc);
				}
				else
				{
					this.Reply(ref rpc);
				}
			}
			if (!rpc.IsPaused)
			{
				this.ProcessMessageCleanup(ref rpc);
			}
		}

		// Token: 0x06003506 RID: 13574 RVA: 0x000CE42C File Offset: 0x000CC62C
		private void ProcessMessageCleanup(ref MessageRpc rpc)
		{
			rpc.ErrorProcessor = this.processMessageCleanupError;
			bool flag = false;
			if (rpc.CanSendReply)
			{
				if (this.sendAsynchronously)
				{
					flag = this.EndReply(ref rpc);
				}
				else
				{
					flag = rpc.SuccessfullySendReply;
				}
			}
			try
			{
				try
				{
					if (rpc.DidDeserializeRequestBody)
					{
						rpc.Request.Close();
					}
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					this.error.HandleError(exception);
				}
				if (rpc.HostingProperty != null)
				{
					try
					{
						rpc.HostingProperty.Close();
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperFatal(ex.Message, ex);
					}
				}
				IManualConcurrencyOperationInvoker manualConcurrencyOperationInvoker = rpc.Operation.Invoker as IManualConcurrencyOperationInvoker;
				rpc.DisposeParameters(manualConcurrencyOperationInvoker != null && manualConcurrencyOperationInvoker.OwnsFormatter);
				if (rpc.FaultInfo.IsConsideredUnhandled)
				{
					if (!flag)
					{
						rpc.AbortRequestContext();
						rpc.AbortChannel();
					}
					else
					{
						rpc.CloseRequestContext();
						rpc.CloseChannel();
					}
					rpc.AbortInstanceContext();
				}
				else if (rpc.RequestContextThrewOnReply)
				{
					rpc.AbortRequestContext();
				}
				else
				{
					rpc.CloseRequestContext();
				}
				if (rpc.Reply != null && rpc.Reply != rpc.ReturnParameter)
				{
					try
					{
						rpc.Reply.Close();
					}
					catch (Exception exception2)
					{
						if (Fx.IsFatal(exception2))
						{
							throw;
						}
						this.error.HandleError(exception2);
					}
				}
				if (rpc.FaultInfo.Fault != null && rpc.FaultInfo.Fault.State != MessageState.Closed)
				{
					try
					{
						rpc.FaultInfo.Fault.Close();
					}
					catch (Exception exception3)
					{
						if (Fx.IsFatal(exception3))
						{
							throw;
						}
						this.error.HandleError(exception3);
					}
				}
				try
				{
					rpc.OperationContext.FireOperationCompleted();
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(ex2);
				}
				this.instance.AfterReply(ref rpc, this.error);
				if (rpc.SuccessfullyLockedInstance)
				{
					try
					{
						this.concurrency.UnlockInstance(ref rpc);
					}
					catch (Exception exception4)
					{
						if (Fx.IsFatal(exception4))
						{
							throw;
						}
						rpc.InstanceContext.FaultInternal();
						this.error.HandleError(exception4);
					}
				}
				if (this.terminate != null)
				{
					try
					{
						this.terminate.AfterReply(ref rpc);
					}
					catch (Exception exception5)
					{
						if (Fx.IsFatal(exception5))
						{
							throw;
						}
						this.error.HandleError(exception5);
					}
				}
				if (rpc.SuccessfullyIncrementedActivity)
				{
					try
					{
						rpc.Channel.DecrementActivity();
					}
					catch (Exception exception6)
					{
						if (Fx.IsFatal(exception6))
						{
							throw;
						}
						this.error.HandleError(exception6);
					}
				}
			}
			finally
			{
				if (rpc.MessageRpcOwnsInstanceContextThrottle && rpc.channelHandler.InstanceContextServiceThrottle != null)
				{
					rpc.channelHandler.InstanceContextServiceThrottle.DeactivateInstanceContext();
				}
				if (rpc.Activity != null && DiagnosticUtility.ShouldUseActivity)
				{
					rpc.Activity.Stop();
				}
			}
			this.error.HandleError(ref rpc);
		}

		// Token: 0x06003507 RID: 13575 RVA: 0x000CE7CC File Offset: 0x000CC9CC
		private void ProcessMessageCleanupError(ref MessageRpc rpc)
		{
			this.error.HandleError(ref rpc);
		}

		// Token: 0x06003508 RID: 13576 RVA: 0x000CE7DC File Offset: 0x000CC9DC
		private void ResolveTransactionOutcome(ref MessageRpc rpc)
		{
			if (this.transaction != null)
			{
				try
				{
					bool flag = rpc.Error != null;
					try
					{
						this.transaction.ResolveOutcome(ref rpc);
					}
					catch (FaultException ex)
					{
						if (rpc.Error == null)
						{
							rpc.Error = ex;
						}
					}
					finally
					{
						if (!flag && rpc.Error != null)
						{
							this.error.ProvideMessageFault(ref rpc);
							this.PrepareAndAddressReply(ref rpc);
						}
					}
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					this.error.HandleError(exception);
				}
			}
		}

		// Token: 0x06003509 RID: 13577 RVA: 0x000CE884 File Offset: 0x000CCA84
		[SecuritySafeCritical]
		private void SetActivityIdOnThread(ref MessageRpc rpc)
		{
			if (FxTrace.Trace.IsEnd2EndActivityTracingEnabled && rpc.EventTraceActivity != null)
			{
				EventTraceActivityHelper.SetOnThread(rpc.EventTraceActivity);
			}
		}

		// Token: 0x0600350A RID: 13578 RVA: 0x000CE8A8 File Offset: 0x000CCAA8
		private void TransferChannelFromPendingList(ref MessageRpc rpc)
		{
			if (rpc.Channel.IsPending)
			{
				rpc.Channel.IsPending = false;
				ChannelDispatcher channelDispatcher = rpc.Channel.ChannelDispatcher;
				IInstanceContextProvider instanceContextProvider = this.instance.InstanceContextProvider;
				if (!InstanceContextProviderBase.IsProviderSessionful(instanceContextProvider) && !InstanceContextProviderBase.IsProviderSingleton(instanceContextProvider))
				{
					IChannel item = rpc.Channel.Proxy as IChannel;
					if (!rpc.InstanceContext.IncomingChannels.Contains(item))
					{
						channelDispatcher.Channels.Add(item);
					}
				}
				channelDispatcher.PendingChannels.Remove(rpc.Channel.Binder.Channel);
			}
		}

		// Token: 0x0400282A RID: 10282
		private readonly AuthenticationBehavior authenticationBehavior;

		// Token: 0x0400282B RID: 10283
		private readonly AuthorizationBehavior authorizationBehavior;

		// Token: 0x0400282C RID: 10284
		private readonly int correlationCount;

		// Token: 0x0400282D RID: 10285
		private readonly ConcurrencyBehavior concurrency;

		// Token: 0x0400282E RID: 10286
		private readonly ImmutableDispatchRuntime.IDemuxer demuxer;

		// Token: 0x0400282F RID: 10287
		private readonly ErrorBehavior error;

		// Token: 0x04002830 RID: 10288
		private readonly bool enableFaults;

		// Token: 0x04002831 RID: 10289
		private readonly bool ignoreTransactionFlow;

		// Token: 0x04002832 RID: 10290
		private readonly bool impersonateOnSerializingReply;

		// Token: 0x04002833 RID: 10291
		private readonly IInputSessionShutdown[] inputSessionShutdownHandlers;

		// Token: 0x04002834 RID: 10292
		private readonly InstanceBehavior instance;

		// Token: 0x04002835 RID: 10293
		private readonly bool isOnServer;

		// Token: 0x04002836 RID: 10294
		private readonly bool manualAddressing;

		// Token: 0x04002837 RID: 10295
		private readonly IDispatchMessageInspector[] messageInspectors;

		// Token: 0x04002838 RID: 10296
		private readonly int parameterInspectorCorrelationOffset;

		// Token: 0x04002839 RID: 10297
		private readonly IRequestReplyCorrelator requestReplyCorrelator;

		// Token: 0x0400283A RID: 10298
		private readonly SecurityImpersonationBehavior securityImpersonation;

		// Token: 0x0400283B RID: 10299
		private readonly TerminatingOperationBehavior terminate;

		// Token: 0x0400283C RID: 10300
		private readonly ThreadBehavior thread;

		// Token: 0x0400283D RID: 10301
		private readonly TransactionBehavior transaction;

		// Token: 0x0400283E RID: 10302
		private readonly bool validateMustUnderstand;

		// Token: 0x0400283F RID: 10303
		private readonly bool receiveContextEnabledChannel;

		// Token: 0x04002840 RID: 10304
		private readonly bool sendAsynchronously;

		// Token: 0x04002841 RID: 10305
		private readonly bool requireClaimsPrincipalOnOperationContext;

		// Token: 0x04002842 RID: 10306
		private readonly MessageRpcProcessor processMessage1;

		// Token: 0x04002843 RID: 10307
		private readonly MessageRpcProcessor processMessage11;

		// Token: 0x04002844 RID: 10308
		private readonly MessageRpcProcessor processMessage2;

		// Token: 0x04002845 RID: 10309
		private readonly MessageRpcProcessor processMessage3;

		// Token: 0x04002846 RID: 10310
		private readonly MessageRpcProcessor processMessage31;

		// Token: 0x04002847 RID: 10311
		private readonly MessageRpcProcessor processMessage4;

		// Token: 0x04002848 RID: 10312
		private readonly MessageRpcProcessor processMessage41;

		// Token: 0x04002849 RID: 10313
		private readonly MessageRpcProcessor processMessage5;

		// Token: 0x0400284A RID: 10314
		private readonly MessageRpcProcessor processMessage6;

		// Token: 0x0400284B RID: 10315
		private readonly MessageRpcProcessor processMessage7;

		// Token: 0x0400284C RID: 10316
		private readonly MessageRpcProcessor processMessage8;

		// Token: 0x0400284D RID: 10317
		private readonly MessageRpcProcessor processMessage9;

		// Token: 0x0400284E RID: 10318
		private readonly MessageRpcProcessor processMessageCleanup;

		// Token: 0x0400284F RID: 10319
		private readonly MessageRpcProcessor processMessageCleanupError;

		// Token: 0x04002850 RID: 10320
		private static AsyncCallback onFinalizeCorrelationCompleted = Fx.ThunkCallback(new AsyncCallback(ImmutableDispatchRuntime.OnFinalizeCorrelationCompletedCallback));

		// Token: 0x04002851 RID: 10321
		private static AsyncCallback onReplyCompleted = Fx.ThunkCallback(new AsyncCallback(ImmutableDispatchRuntime.OnReplyCompletedCallback));

		// Token: 0x04002852 RID: 10322
		private bool didTraceProcessMessage1;

		// Token: 0x04002853 RID: 10323
		private bool didTraceProcessMessage2;

		// Token: 0x04002854 RID: 10324
		private bool didTraceProcessMessage3;

		// Token: 0x04002855 RID: 10325
		private bool didTraceProcessMessage31;

		// Token: 0x04002856 RID: 10326
		private bool didTraceProcessMessage4;

		// Token: 0x04002857 RID: 10327
		private bool didTraceProcessMessage41;

		// Token: 0x02000C76 RID: 3190
		private interface IDemuxer
		{
			// Token: 0x0600781E RID: 30750
			DispatchOperationRuntime GetOperation(ref Message request);
		}

		// Token: 0x02000C77 RID: 3191
		private class ActionDemuxer : ImmutableDispatchRuntime.IDemuxer
		{
			// Token: 0x0600781F RID: 30751 RVA: 0x001C1381 File Offset: 0x001BF581
			internal ActionDemuxer()
			{
				this.map = new HybridDictionary();
			}

			// Token: 0x06007820 RID: 30752 RVA: 0x001C1394 File Offset: 0x001BF594
			internal void Add(string action, DispatchOperationRuntime operation)
			{
				if (this.map.Contains(action))
				{
					DispatchOperationRuntime dispatchOperationRuntime = (DispatchOperationRuntime)this.map[action];
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxActionDemuxerDuplicate", new object[]
					{
						dispatchOperationRuntime.Name,
						operation.Name,
						action
					})));
				}
				this.map.Add(action, operation);
			}

			// Token: 0x06007821 RID: 30753 RVA: 0x001C1404 File Offset: 0x001BF604
			internal void SetUnhandled(DispatchOperationRuntime operation)
			{
				this.unhandled = operation;
			}

			// Token: 0x06007822 RID: 30754 RVA: 0x001C1410 File Offset: 0x001BF610
			public DispatchOperationRuntime GetOperation(ref Message request)
			{
				string text = request.Headers.Action;
				if (text == null)
				{
					text = "*";
				}
				DispatchOperationRuntime dispatchOperationRuntime = (DispatchOperationRuntime)this.map[text];
				if (dispatchOperationRuntime != null)
				{
					return dispatchOperationRuntime;
				}
				return this.unhandled;
			}

			// Token: 0x04004492 RID: 17554
			private HybridDictionary map;

			// Token: 0x04004493 RID: 17555
			private DispatchOperationRuntime unhandled;
		}

		// Token: 0x02000C78 RID: 3192
		private class CustomDemuxer : ImmutableDispatchRuntime.IDemuxer
		{
			// Token: 0x06007823 RID: 30755 RVA: 0x001C1450 File Offset: 0x001BF650
			internal CustomDemuxer(IDispatchOperationSelector selector)
			{
				this.selector = selector;
				this.map = new Dictionary<string, DispatchOperationRuntime>();
			}

			// Token: 0x06007824 RID: 30756 RVA: 0x001C146A File Offset: 0x001BF66A
			internal void Add(string name, DispatchOperationRuntime operation)
			{
				this.map.Add(name, operation);
			}

			// Token: 0x06007825 RID: 30757 RVA: 0x001C1479 File Offset: 0x001BF679
			internal void SetUnhandled(DispatchOperationRuntime operation)
			{
				this.unhandled = operation;
			}

			// Token: 0x06007826 RID: 30758 RVA: 0x001C1484 File Offset: 0x001BF684
			public DispatchOperationRuntime GetOperation(ref Message request)
			{
				bool flag = DS.OperationSelectorIsEnabled();
				Stopwatch stopwatch = null;
				if (flag)
				{
					stopwatch = Stopwatch.StartNew();
				}
				string text = this.selector.SelectOperation(ref request);
				if (flag)
				{
					DS.DispatchSelectOperation(this.selector.GetType(), text, stopwatch.Elapsed);
				}
				DispatchOperationRuntime result = null;
				if (this.map.TryGetValue(text, out result))
				{
					return result;
				}
				return this.unhandled;
			}

			// Token: 0x04004494 RID: 17556
			private Dictionary<string, DispatchOperationRuntime> map;

			// Token: 0x04004495 RID: 17557
			private IDispatchOperationSelector selector;

			// Token: 0x04004496 RID: 17558
			private DispatchOperationRuntime unhandled;
		}

		// Token: 0x02000C79 RID: 3193
		private class RpcCorrelationCallbackMessageProperty : CorrelationCallbackMessageProperty
		{
			// Token: 0x06007827 RID: 30759 RVA: 0x001C14E3 File Offset: 0x001BF6E3
			public RpcCorrelationCallbackMessageProperty(CorrelationCallbackMessageProperty innerCallback, ImmutableDispatchRuntime runtime, ref MessageRpc rpc) : base(innerCallback)
			{
				this.innerCallback = innerCallback;
				this.runtime = runtime;
				this.rpc = rpc;
			}

			// Token: 0x06007828 RID: 30760 RVA: 0x001C1506 File Offset: 0x001BF706
			public RpcCorrelationCallbackMessageProperty(ImmutableDispatchRuntime.RpcCorrelationCallbackMessageProperty rpcCallbackMessageProperty) : base(rpcCallbackMessageProperty)
			{
				this.innerCallback = rpcCallbackMessageProperty.innerCallback;
				this.runtime = rpcCallbackMessageProperty.runtime;
				this.rpc = rpcCallbackMessageProperty.rpc;
			}

			// Token: 0x06007829 RID: 30761 RVA: 0x001C1533 File Offset: 0x001BF733
			public override IMessageProperty CreateCopy()
			{
				return new ImmutableDispatchRuntime.RpcCorrelationCallbackMessageProperty(this);
			}

			// Token: 0x0600782A RID: 30762 RVA: 0x001C153C File Offset: 0x001BF73C
			protected override IAsyncResult OnBeginFinalizeCorrelation(Message message, TimeSpan timeout, AsyncCallback callback, object state)
			{
				bool complete = false;
				this.Enter();
				IAsyncResult result;
				try
				{
					IAsyncResult asyncResult = this.innerCallback.BeginFinalizeCorrelation(message, timeout, callback, state);
					complete = true;
					result = asyncResult;
				}
				finally
				{
					this.Leave(complete);
				}
				return result;
			}

			// Token: 0x0600782B RID: 30763 RVA: 0x001C1584 File Offset: 0x001BF784
			protected override Message OnEndFinalizeCorrelation(IAsyncResult result)
			{
				bool complete = false;
				this.Enter();
				Message result2;
				try
				{
					Message message = this.innerCallback.EndFinalizeCorrelation(result);
					complete = true;
					result2 = message;
				}
				finally
				{
					this.Leave(complete);
					this.CompleteTransaction();
				}
				return result2;
			}

			// Token: 0x0600782C RID: 30764 RVA: 0x001C15CC File Offset: 0x001BF7CC
			protected override Message OnFinalizeCorrelation(Message message, TimeSpan timeout)
			{
				bool complete = false;
				this.Enter();
				Message result;
				try
				{
					Message message2 = this.innerCallback.FinalizeCorrelation(message, timeout);
					complete = true;
					result = message2;
				}
				finally
				{
					this.Leave(complete);
					this.CompleteTransaction();
				}
				return result;
			}

			// Token: 0x0600782D RID: 30765 RVA: 0x001C1614 File Offset: 0x001BF814
			private void CompleteTransaction()
			{
				this.runtime.ResolveTransactionOutcome(ref this.rpc);
			}

			// Token: 0x0600782E RID: 30766 RVA: 0x001C1628 File Offset: 0x001BF828
			private void Enter()
			{
				if (this.rpc.transaction != null && this.rpc.transaction.Current != null)
				{
					this.scope = new TransactionScope(this.rpc.transaction.Current);
				}
			}

			// Token: 0x0600782F RID: 30767 RVA: 0x001C1675 File Offset: 0x001BF875
			private void Leave(bool complete)
			{
				if (this.scope != null)
				{
					if (complete)
					{
						this.scope.Complete();
					}
					this.scope.Dispose();
					this.scope = null;
				}
			}

			// Token: 0x04004497 RID: 17559
			private CorrelationCallbackMessageProperty innerCallback;

			// Token: 0x04004498 RID: 17560
			private MessageRpc rpc;

			// Token: 0x04004499 RID: 17561
			private ImmutableDispatchRuntime runtime;

			// Token: 0x0400449A RID: 17562
			private TransactionScope scope;
		}
	}
}
