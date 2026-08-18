using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.Diagnostics;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.Threading;
using System.Transactions;
using System.Xml;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000552 RID: 1362
	internal class ChannelHandler
	{
		// Token: 0x06003431 RID: 13361 RVA: 0x000C8FEC File Offset: 0x000C71EC
		internal ChannelHandler(MessageVersion messageVersion, IChannelBinder binder, ServiceChannel channel)
		{
			ClientRuntime clientRuntime = channel.ClientRuntime;
			this.messageVersion = messageVersion;
			this.isManualAddressing = clientRuntime.ManualAddressing;
			this.binder = binder;
			this.channel = channel;
			this.isConcurrent = true;
			this.duplexBinder = (binder as DuplexChannelBinder);
			this.hasSession = binder.HasSession;
			this.isCallback = true;
			DispatchRuntime dispatchRuntime = clientRuntime.DispatchRuntime;
			if (dispatchRuntime == null)
			{
				this.receiver = new ErrorHandlingReceiver(binder, null);
			}
			else
			{
				this.receiver = new ErrorHandlingReceiver(binder, dispatchRuntime.ChannelDispatcher);
			}
			this.requestInfo = new ChannelHandler.RequestInfo(this);
		}

		// Token: 0x06003432 RID: 13362 RVA: 0x000C9084 File Offset: 0x000C7284
		internal ChannelHandler(MessageVersion messageVersion, IChannelBinder binder, ServiceThrottle throttle, ListenerHandler listener, bool wasChannelThrottled, WrappedTransaction acceptTransaction, ServiceChannel.SessionIdleManager idleManager)
		{
			ChannelDispatcher channelDispatcher = listener.ChannelDispatcher;
			this.messageVersion = messageVersion;
			this.isManualAddressing = channelDispatcher.ManualAddressing;
			this.binder = binder;
			this.throttle = throttle;
			this.listener = listener;
			this.wasChannelThrottled = wasChannelThrottled;
			this.host = listener.Host;
			this.receiveSynchronously = channelDispatcher.ReceiveSynchronously;
			this.sendAsynchronously = channelDispatcher.SendAsynchronously;
			this.duplexBinder = (binder as DuplexChannelBinder);
			this.hasSession = binder.HasSession;
			this.isConcurrent = ConcurrencyBehavior.IsConcurrent(channelDispatcher, this.hasSession);
			if (channelDispatcher.MaxPendingReceives > 1)
			{
				this.binder = new MultipleReceiveBinder(this.binder, channelDispatcher.MaxPendingReceives, !this.isConcurrent);
			}
			if (channelDispatcher.BufferedReceiveEnabled)
			{
				this.binder = new BufferedReceiveBinder(this.binder);
			}
			this.receiver = new ErrorHandlingReceiver(this.binder, channelDispatcher);
			this.idleManager = idleManager;
			if (channelDispatcher.IsTransactedReceive && !channelDispatcher.ReceiveContextEnabled)
			{
				this.receiveSynchronously = true;
				this.receiveWithTransaction = true;
				if (channelDispatcher.MaxTransactedBatchSize > 0)
				{
					int maxConcurrentBatches = 1;
					if (throttle != null && throttle.MaxConcurrentCalls > 1)
					{
						maxConcurrentBatches = throttle.MaxConcurrentCalls;
						foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
						{
							if (ConcurrencyMode.Multiple != endpointDispatcher.DispatchRuntime.ConcurrencyMode)
							{
								maxConcurrentBatches = 1;
								break;
							}
						}
					}
					this.sharedTransactedBatchContext = new SharedTransactedBatchContext(this, channelDispatcher, maxConcurrentBatches);
					this.isMainTransactedBatchHandler = true;
					this.throttle = null;
				}
			}
			else if (channelDispatcher.IsTransactedReceive && channelDispatcher.ReceiveContextEnabled && channelDispatcher.MaxTransactedBatchSize > 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("IncompatibleBehaviors")));
			}
			if (this.binder.HasSession)
			{
				this.sessionOpenNotification = this.binder.Channel.GetProperty<SessionOpenNotification>();
				this.needToCreateSessionOpenNotificationMessage = (this.sessionOpenNotification != null && this.sessionOpenNotification.IsEnabled);
			}
			this.acceptTransaction = acceptTransaction;
			this.requestInfo = new ChannelHandler.RequestInfo(this);
			if (this.listener.State == CommunicationState.Opened)
			{
				this.listener.ChannelDispatcher.Channels.IncrementActivityCount();
				this.incrementedActivityCountInConstructor = true;
			}
		}

		// Token: 0x06003433 RID: 13363 RVA: 0x000C92DC File Offset: 0x000C74DC
		internal ChannelHandler(ChannelHandler handler, TransactedBatchContext context)
		{
			this.messageVersion = handler.messageVersion;
			this.isManualAddressing = handler.isManualAddressing;
			this.binder = handler.binder;
			this.listener = handler.listener;
			this.wasChannelThrottled = handler.wasChannelThrottled;
			this.host = handler.host;
			this.receiveSynchronously = true;
			this.receiveWithTransaction = true;
			this.duplexBinder = handler.duplexBinder;
			this.hasSession = handler.hasSession;
			this.isConcurrent = handler.isConcurrent;
			this.receiver = handler.receiver;
			this.sharedTransactedBatchContext = context.Shared;
			this.transactedBatchContext = context;
			this.requestInfo = new ChannelHandler.RequestInfo(this);
			this.sendAsynchronously = handler.sendAsynchronously;
			this.sessionOpenNotification = handler.sessionOpenNotification;
			this.needToCreateSessionOpenNotificationMessage = handler.needToCreateSessionOpenNotificationMessage;
			this.shouldRejectMessageWithOnOpenActionHeader = handler.shouldRejectMessageWithOnOpenActionHeader;
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x06003434 RID: 13364 RVA: 0x000C93C4 File Offset: 0x000C75C4
		internal IChannelBinder Binder
		{
			get
			{
				return this.binder;
			}
		}

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x06003435 RID: 13365 RVA: 0x000C93CC File Offset: 0x000C75CC
		internal ServiceChannel Channel
		{
			get
			{
				return this.channel;
			}
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06003436 RID: 13366 RVA: 0x000C93D4 File Offset: 0x000C75D4
		internal bool HasRegisterBeenCalled
		{
			get
			{
				return this.hasRegisterBeenCalled;
			}
		}

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x06003437 RID: 13367 RVA: 0x000C93DC File Offset: 0x000C75DC
		internal InstanceContext InstanceContext
		{
			get
			{
				if (this.channel == null)
				{
					return null;
				}
				return this.channel.InstanceContext;
			}
		}

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06003438 RID: 13368 RVA: 0x000C93F3 File Offset: 0x000C75F3
		// (set) Token: 0x06003439 RID: 13369 RVA: 0x000C93FB File Offset: 0x000C75FB
		internal ServiceThrottle InstanceContextServiceThrottle
		{
			get
			{
				return this.instanceContextThrottle;
			}
			set
			{
				this.instanceContextThrottle = value;
			}
		}

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x0600343A RID: 13370 RVA: 0x000C9404 File Offset: 0x000C7604
		private bool IsOpen
		{
			get
			{
				return this.binder.Channel.State == CommunicationState.Opened;
			}
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x0600343B RID: 13371 RVA: 0x000C941C File Offset: 0x000C761C
		private EndpointAddress LocalAddress
		{
			get
			{
				if (this.binder != null)
				{
					IInputChannel inputChannel = this.binder.Channel as IInputChannel;
					if (inputChannel != null)
					{
						return inputChannel.LocalAddress;
					}
					IReplyChannel replyChannel = this.binder.Channel as IReplyChannel;
					if (replyChannel != null)
					{
						return replyChannel.LocalAddress;
					}
				}
				return null;
			}
		}

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x0600343C RID: 13372 RVA: 0x000C9468 File Offset: 0x000C7668
		private object ThisLock
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x0600343D RID: 13373 RVA: 0x000C946B File Offset: 0x000C766B
		private EventTraceActivity EventTraceActivity
		{
			get
			{
				if (this.eventTraceActivity == null)
				{
					this.eventTraceActivity = new EventTraceActivity(false);
				}
				return this.eventTraceActivity;
			}
		}

		// Token: 0x0600343E RID: 13374 RVA: 0x000C9487 File Offset: 0x000C7687
		internal static void Register(ChannelHandler handler)
		{
			handler.Register();
		}

		// Token: 0x0600343F RID: 13375 RVA: 0x000C9490 File Offset: 0x000C7690
		internal static void Register(ChannelHandler handler, RequestContext request)
		{
			BufferedReceiveBinder bufferedReceiveBinder = handler.Binder as BufferedReceiveBinder;
			bufferedReceiveBinder.InjectRequest(request);
			handler.Register();
		}

		// Token: 0x06003440 RID: 13376 RVA: 0x000C94B6 File Offset: 0x000C76B6
		private void Register()
		{
			this.hasRegisterBeenCalled = true;
			if (this.binder.Channel.State == CommunicationState.Created)
			{
				ActionItem.Schedule(ChannelHandler.openAndEnsurePump, this);
				return;
			}
			this.EnsurePump();
		}

		// Token: 0x06003441 RID: 13377 RVA: 0x000C94E4 File Offset: 0x000C76E4
		private void AsyncMessagePump()
		{
			IAsyncResult asyncResult = this.BeginTryReceive();
			if (asyncResult != null && asyncResult.CompletedSynchronously)
			{
				this.AsyncMessagePump(asyncResult);
			}
		}

		// Token: 0x06003442 RID: 13378 RVA: 0x000C950C File Offset: 0x000C770C
		private void AsyncMessagePump(IAsyncResult result)
		{
			if (TD.ChannelReceiveStopIsEnabled())
			{
				TD.ChannelReceiveStop(this.EventTraceActivity, this.GetHashCode());
			}
			for (;;)
			{
				RequestContext request;
				if (this.EndTryReceive(result, out request))
				{
					if (!this.HandleRequest(request, null) || !this.TryAcquirePump())
					{
						return;
					}
					result = this.BeginTryReceive();
					if (result == null || !result.CompletedSynchronously)
					{
						return;
					}
				}
				else
				{
					result = this.BeginTryReceive();
					if (result == null || !result.CompletedSynchronously)
					{
						break;
					}
				}
			}
		}

		// Token: 0x06003443 RID: 13379 RVA: 0x000C9578 File Offset: 0x000C7778
		private IAsyncResult BeginTryReceive()
		{
			this.requestInfo.Cleanup();
			if (TD.ChannelReceiveStartIsEnabled())
			{
				TD.ChannelReceiveStart(this.EventTraceActivity, this.GetHashCode());
			}
			this.shouldRejectMessageWithOnOpenActionHeader = !this.needToCreateSessionOpenNotificationMessage;
			if (this.needToCreateSessionOpenNotificationMessage)
			{
				return new CompletedAsyncResult(ChannelHandler.onAsyncReceiveComplete, this);
			}
			return this.receiver.BeginTryReceive(TimeSpan.MaxValue, ChannelHandler.onAsyncReceiveComplete, this);
		}

		// Token: 0x06003444 RID: 13380 RVA: 0x000C95E4 File Offset: 0x000C77E4
		private bool DispatchAndReleasePump(RequestContext request, bool cleanThread, OperationContext currentOperationContext)
		{
			ServiceChannel serviceChannel = this.requestInfo.Channel;
			EndpointDispatcher endpoint = this.requestInfo.Endpoint;
			bool flag = false;
			bool result;
			try
			{
				DispatchRuntime dispatchRuntime = this.requestInfo.DispatchRuntime;
				if (serviceChannel == null || dispatchRuntime == null)
				{
					result = true;
				}
				else
				{
					MessageBuffer messageBuffer = null;
					EventTraceActivity eventTraceActivity = this.TraceDispatchMessageStart(request.RequestMessage);
					AspNetEnvironment.Current.PrepareMessageForDispatch(request.RequestMessage);
					Message message;
					if (dispatchRuntime.PreserveMessage)
					{
						object obj = null;
						if (request.RequestMessage.Properties.TryGetValue("_RequestMessageBuffer_", out obj))
						{
							messageBuffer = (MessageBuffer)obj;
							message = messageBuffer.CreateMessage();
						}
						else
						{
							messageBuffer = request.RequestMessage.CreateBufferedCopy(int.MaxValue);
							message = messageBuffer.CreateMessage();
						}
					}
					else
					{
						message = request.RequestMessage;
					}
					DispatchOperationRuntime operation = dispatchRuntime.GetOperation(ref message);
					if (operation == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "No DispatchOperationRuntime found to process message.", new object[0])));
					}
					if (this.shouldRejectMessageWithOnOpenActionHeader && message.Headers.Action == "http://schemas.microsoft.com/2011/02/session/onopen")
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxNoEndpointMatchingAddressForConnectionOpeningMessage", new object[]
						{
							message.Headers.Action,
							"Open"
						})));
					}
					if (MessageLogger.LoggingEnabled)
					{
						MessageLogger.LogMessage(ref message, (operation.IsOneWay ? MessageLoggingSource.ServiceLevelReceiveDatagram : MessageLoggingSource.ServiceLevelReceiveRequest) | MessageLoggingSource.LastChance);
					}
					if (operation.IsTerminating && this.hasSession)
					{
						this.isChannelTerminated = true;
					}
					bool isOperationContextSet;
					if (currentOperationContext != null)
					{
						isOperationContextSet = true;
						currentOperationContext.ReInit(request, message, serviceChannel);
					}
					else
					{
						isOperationContextSet = false;
						currentOperationContext = new OperationContext(request, message, serviceChannel, this.host);
					}
					if (dispatchRuntime.PreserveMessage)
					{
						currentOperationContext.IncomingMessageProperties.Add("_RequestMessageBuffer_", messageBuffer);
					}
					if (currentOperationContext.EndpointDispatcher == null && this.listener != null)
					{
						currentOperationContext.EndpointDispatcher = endpoint;
					}
					MessageRpc messageRpc = new MessageRpc(request, message, operation, serviceChannel, this.host, this, cleanThread, currentOperationContext, this.requestInfo.ExistingInstanceContext, eventTraceActivity);
					TraceUtility.MessageFlowAtMessageReceived(message, currentOperationContext, eventTraceActivity, true);
					messageRpc.TransactedBatchContext = this.transactedBatchContext;
					this.requestInfo.ChannelHandlerOwnsCallThrottle = false;
					messageRpc.MessageRpcOwnsInstanceContextThrottle = this.requestInfo.ChannelHandlerOwnsInstanceContextThrottle;
					this.requestInfo.ChannelHandlerOwnsInstanceContextThrottle = false;
					this.ReleasePump();
					flag = true;
					result = operation.Parent.Dispatch(ref messageRpc, isOperationContextSet);
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				result = this.HandleError(ex, request, serviceChannel);
			}
			finally
			{
				if (!flag)
				{
					this.ReleasePump();
				}
			}
			return result;
		}

		// Token: 0x06003445 RID: 13381 RVA: 0x000C989C File Offset: 0x000C7A9C
		internal void DispatchDone()
		{
			if (this.throttle != null)
			{
				this.throttle.DeactivateCall();
			}
		}

		// Token: 0x06003446 RID: 13382 RVA: 0x000C98B4 File Offset: 0x000C7AB4
		private RequestContext GetSessionOpenNotificationRequestContext()
		{
			Message message = Message.CreateMessage(this.Binder.Channel.GetProperty<MessageVersion>(), "http://schemas.microsoft.com/2011/02/session/onopen");
			message.Headers.To = this.LocalAddress.Uri;
			this.sessionOpenNotification.UpdateMessageProperties(message.Properties);
			return this.Binder.CreateRequestContext(message);
		}

		// Token: 0x06003447 RID: 13383 RVA: 0x000C9910 File Offset: 0x000C7B10
		private bool EndTryReceive(IAsyncResult result, out RequestContext requestContext)
		{
			bool flag;
			if (this.needToCreateSessionOpenNotificationMessage)
			{
				this.needToCreateSessionOpenNotificationMessage = false;
				CompletedAsyncResult.End(result);
				requestContext = this.GetSessionOpenNotificationRequestContext();
				flag = true;
			}
			else
			{
				flag = this.receiver.EndTryReceive(result, out requestContext);
			}
			if (flag)
			{
				this.HandleReceiveComplete(requestContext);
			}
			return flag;
		}

		// Token: 0x06003448 RID: 13384 RVA: 0x000C9958 File Offset: 0x000C7B58
		private void EnsureChannelAndEndpoint(RequestContext request)
		{
			this.requestInfo.Channel = this.channel;
			if (this.requestInfo.Channel == null)
			{
				bool flag;
				if (this.hasSession)
				{
					this.requestInfo.Channel = this.GetSessionChannel(request.RequestMessage, out this.requestInfo.Endpoint, out flag);
				}
				else
				{
					this.requestInfo.Channel = this.GetDatagramChannel(request.RequestMessage, out this.requestInfo.Endpoint, out flag);
				}
				if (this.requestInfo.Channel == null)
				{
					this.host.RaiseUnknownMessageReceived(request.RequestMessage);
					if (flag)
					{
						this.ReplyContractFilterDidNotMatch(request);
					}
					else
					{
						this.ReplyAddressFilterDidNotMatch(request);
					}
				}
			}
			else
			{
				this.requestInfo.Endpoint = this.requestInfo.Channel.EndpointDispatcher;
				if (this.InstanceContextServiceThrottle != null && this.requestInfo.Channel.InstanceContextServiceThrottle == null)
				{
					this.requestInfo.Channel.InstanceContextServiceThrottle = this.InstanceContextServiceThrottle;
				}
			}
			this.requestInfo.EndpointLookupDone = true;
			if (this.requestInfo.Channel == null)
			{
				TraceUtility.TraceDroppedMessage(request.RequestMessage, this.requestInfo.Endpoint);
				request.Close();
				return;
			}
			if (this.requestInfo.Channel.HasSession || this.isCallback)
			{
				this.requestInfo.DispatchRuntime = this.requestInfo.Channel.DispatchRuntime;
				return;
			}
			this.requestInfo.DispatchRuntime = this.requestInfo.Endpoint.DispatchRuntime;
		}

		// Token: 0x06003449 RID: 13385 RVA: 0x000C9AE0 File Offset: 0x000C7CE0
		private void EnsurePump()
		{
			if (this.sharedTransactedBatchContext == null || this.isMainTransactedBatchHandler)
			{
				if (this.TryAcquirePump())
				{
					if (this.receiveSynchronously)
					{
						ActionItem.Schedule(ChannelHandler.onStartSyncMessagePump, this);
						return;
					}
					if (!Thread.CurrentThread.IsThreadPoolThread)
					{
						ActionItem.Schedule(ChannelHandler.onStartAsyncMessagePump, this);
						return;
					}
					IAsyncResult asyncResult = this.BeginTryReceive();
					if (asyncResult != null && asyncResult.CompletedSynchronously)
					{
						ActionItem.Schedule(ChannelHandler.onContinueAsyncReceive, asyncResult);
						return;
					}
				}
			}
			else
			{
				ActionItem.Schedule(ChannelHandler.onStartSingleTransactedBatch, this);
			}
		}

		// Token: 0x0600344A RID: 13386 RVA: 0x000C9B5C File Offset: 0x000C7D5C
		private ServiceChannel GetDatagramChannel(Message message, out EndpointDispatcher endpoint, out bool addressMatched)
		{
			addressMatched = false;
			endpoint = this.GetEndpointDispatcher(message, out addressMatched);
			if (endpoint == null)
			{
				return null;
			}
			if (endpoint.DatagramChannel == null)
			{
				object thisLock = this.listener.ThisLock;
				lock (thisLock)
				{
					if (endpoint.DatagramChannel == null)
					{
						endpoint.DatagramChannel = new ServiceChannel(this.binder, endpoint, this.listener.ChannelDispatcher, this.idleManager);
						this.InitializeServiceChannel(endpoint.DatagramChannel);
					}
				}
			}
			return endpoint.DatagramChannel;
		}

		// Token: 0x0600344B RID: 13387 RVA: 0x000C9BFC File Offset: 0x000C7DFC
		private EndpointDispatcher GetEndpointDispatcher(Message message, out bool addressMatched)
		{
			return this.listener.Endpoints.Lookup(message, out addressMatched);
		}

		// Token: 0x0600344C RID: 13388 RVA: 0x000C9C10 File Offset: 0x000C7E10
		private ServiceChannel GetSessionChannel(Message message, out EndpointDispatcher endpoint, out bool addressMatched)
		{
			addressMatched = false;
			if (this.channel == null)
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					if (this.channel == null)
					{
						endpoint = this.GetEndpointDispatcher(message, out addressMatched);
						if (endpoint != null)
						{
							this.channel = new ServiceChannel(this.binder, endpoint, this.listener.ChannelDispatcher, this.idleManager);
							this.InitializeServiceChannel(this.channel);
						}
					}
				}
			}
			if (this.channel == null)
			{
				endpoint = null;
			}
			else
			{
				endpoint = this.channel.EndpointDispatcher;
			}
			return this.channel;
		}

		// Token: 0x0600344D RID: 13389 RVA: 0x000C9CBC File Offset: 0x000C7EBC
		private void InitializeServiceChannel(ServiceChannel channel)
		{
			if (this.wasChannelThrottled)
			{
				if (channel.Aborted && this.throttle != null)
				{
					this.throttle.DeactivateChannel();
				}
				channel.ServiceThrottle = this.throttle;
			}
			if (this.InstanceContextServiceThrottle != null)
			{
				channel.InstanceContextServiceThrottle = this.InstanceContextServiceThrottle;
			}
			ClientRuntime clientRuntime = channel.ClientRuntime;
			if (clientRuntime != null)
			{
				Type contractClientType = clientRuntime.ContractClientType;
				Type callbackClientType = clientRuntime.CallbackClientType;
				if (contractClientType != null)
				{
					channel.Proxy = ServiceChannelFactory.CreateProxy(contractClientType, callbackClientType, MessageDirection.Output, channel);
				}
			}
			if (this.listener != null)
			{
				this.listener.ChannelDispatcher.InitializeChannel((IClientChannel)channel.Proxy);
			}
			((ICommunicationObject)channel).Open();
		}

		// Token: 0x0600344E RID: 13390 RVA: 0x000C9D68 File Offset: 0x000C7F68
		private void ProvideFault(Exception e, ref ErrorHandlerFaultInfo faultInfo)
		{
			if (this.listener != null)
			{
				this.listener.ChannelDispatcher.ProvideFault(e, (this.requestInfo.Channel == null) ? this.binder.Channel.GetProperty<FaultConverter>() : this.requestInfo.Channel.GetProperty<FaultConverter>(), ref faultInfo);
				return;
			}
			if (this.channel != null)
			{
				DispatchRuntime callbackDispatchRuntime = this.channel.ClientRuntime.CallbackDispatchRuntime;
				callbackDispatchRuntime.ChannelDispatcher.ProvideFault(e, this.channel.GetProperty<FaultConverter>(), ref faultInfo);
			}
		}

		// Token: 0x0600344F RID: 13391 RVA: 0x000C9DF0 File Offset: 0x000C7FF0
		internal bool HandleError(Exception e)
		{
			ErrorHandlerFaultInfo errorHandlerFaultInfo = default(ErrorHandlerFaultInfo);
			return this.HandleError(e, ref errorHandlerFaultInfo);
		}

		// Token: 0x06003450 RID: 13392 RVA: 0x000C9E10 File Offset: 0x000C8010
		private bool HandleError(Exception e, ref ErrorHandlerFaultInfo faultInfo)
		{
			if (e == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString(SR.GetString("SFxNonExceptionThrown"))));
			}
			if (this.listener != null)
			{
				return this.listener.ChannelDispatcher.HandleError(e, ref faultInfo);
			}
			return this.channel != null && this.channel.ClientRuntime.CallbackDispatchRuntime.ChannelDispatcher.HandleError(e, ref faultInfo);
		}

		// Token: 0x06003451 RID: 13393 RVA: 0x000C9E80 File Offset: 0x000C8080
		private bool HandleError(Exception e, RequestContext request, ServiceChannel channel)
		{
			ErrorHandlerFaultInfo errorHandlerFaultInfo = new ErrorHandlerFaultInfo(this.messageVersion.Addressing.DefaultFaultAction);
			bool flag;
			bool flag2;
			this.ProvideFaultAndReplyFailure(request, e, ref errorHandlerFaultInfo, out flag, out flag2);
			return !flag2 && this.HandleErrorContinuation(e, request, channel, ref errorHandlerFaultInfo, flag);
		}

		// Token: 0x06003452 RID: 13394 RVA: 0x000C9EC4 File Offset: 0x000C80C4
		private bool HandleErrorContinuation(Exception e, RequestContext request, ServiceChannel channel, ref ErrorHandlerFaultInfo faultInfo, bool replied)
		{
			if (replied)
			{
				try
				{
					request.Close();
					goto IL_27;
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					this.HandleError(ex);
					goto IL_27;
				}
			}
			request.Abort();
			IL_27:
			if (!this.HandleError(e, ref faultInfo) && this.hasSession)
			{
				if (channel != null)
				{
					if (replied)
					{
						TimeoutHelper timeoutHelper = new TimeoutHelper(ChannelHandler.CloseAfterFaultTimeout);
						try
						{
							channel.Close(timeoutHelper.RemainingTime());
						}
						catch (Exception ex2)
						{
							if (Fx.IsFatal(ex2))
							{
								throw;
							}
							this.HandleError(ex2);
						}
						try
						{
							this.binder.CloseAfterFault(timeoutHelper.RemainingTime());
							return true;
						}
						catch (Exception ex3)
						{
							if (Fx.IsFatal(ex3))
							{
								throw;
							}
							this.HandleError(ex3);
							return true;
						}
					}
					channel.Abort();
					this.binder.Abort();
				}
				else
				{
					if (replied)
					{
						try
						{
							this.binder.CloseAfterFault(ChannelHandler.CloseAfterFaultTimeout);
							return true;
						}
						catch (Exception ex4)
						{
							if (Fx.IsFatal(ex4))
							{
								throw;
							}
							this.HandleError(ex4);
							return true;
						}
					}
					this.binder.Abort();
				}
			}
			return true;
		}

		// Token: 0x06003453 RID: 13395 RVA: 0x000C9FF4 File Offset: 0x000C81F4
		private void HandleReceiveComplete(RequestContext context)
		{
			try
			{
				if (this.channel != null)
				{
					this.channel.HandleReceiveComplete(context);
				}
				else if (context == null && this.hasSession)
				{
					object thisLock = this.ThisLock;
					bool flag2;
					lock (thisLock)
					{
						flag2 = !this.doneReceiving;
						this.doneReceiving = true;
					}
					if (flag2)
					{
						this.receiver.Close();
						if (this.idleManager != null)
						{
							this.idleManager.CancelTimer();
						}
						ServiceThrottle serviceThrottle = this.throttle;
						if (serviceThrottle != null)
						{
							serviceThrottle.DeactivateChannel();
						}
					}
				}
			}
			finally
			{
				if (context == null && this.incrementedActivityCountInConstructor)
				{
					this.listener.ChannelDispatcher.Channels.DecrementActivityCount();
				}
			}
		}

		// Token: 0x06003454 RID: 13396 RVA: 0x000CA0C4 File Offset: 0x000C82C4
		private bool HandleRequest(RequestContext request, OperationContext currentOperationContext)
		{
			if (request == null)
			{
				return false;
			}
			ServiceModelActivity activity = DiagnosticUtility.ShouldUseActivity ? TraceUtility.ExtractActivity(request) : null;
			using (ServiceModelActivity.BoundOperation(activity))
			{
				if (this.HandleRequestAsReply(request))
				{
					this.ReleasePump();
					return true;
				}
				if (this.isChannelTerminated)
				{
					this.ReleasePump();
					this.ReplyChannelTerminated(request);
					return true;
				}
				RequestContext requestContext = this.requestInfo.RequestContext;
				this.requestInfo.RequestContext = request;
				if (!this.TryAcquireCallThrottle(request))
				{
					if (DS.ServiceThrottleIsEnabled())
					{
						DS.CallThrottleWaiting(request.RequestMessage);
					}
					return false;
				}
				bool channelHandlerOwnsCallThrottle = this.requestInfo.ChannelHandlerOwnsCallThrottle;
				this.requestInfo.ChannelHandlerOwnsCallThrottle = true;
				if (!this.TryRetrievingInstanceContext(request))
				{
					return true;
				}
				this.requestInfo.Channel.CompletedIOOperation();
				if (!this.TryAcquireThrottle(request, this.requestInfo.ExistingInstanceContext == null))
				{
					return false;
				}
				bool channelHandlerOwnsInstanceContextThrottle = this.requestInfo.ChannelHandlerOwnsInstanceContextThrottle;
				this.requestInfo.ChannelHandlerOwnsInstanceContextThrottle = (this.requestInfo.ExistingInstanceContext == null);
				if (!this.DispatchAndReleasePump(request, true, currentOperationContext))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003455 RID: 13397 RVA: 0x000CA200 File Offset: 0x000C8400
		private bool HandleRequestAsReply(RequestContext request)
		{
			return this.duplexBinder != null && this.duplexBinder.HandleRequestAsReply(request.RequestMessage);
		}

		// Token: 0x06003456 RID: 13398 RVA: 0x000CA220 File Offset: 0x000C8420
		private static void OnStartAsyncMessagePump(object state)
		{
			((ChannelHandler)state).AsyncMessagePump();
		}

		// Token: 0x06003457 RID: 13399 RVA: 0x000CA230 File Offset: 0x000C8430
		private static void OnStartSyncMessagePump(object state)
		{
			ChannelHandler channelHandler = state as ChannelHandler;
			if (TD.ChannelReceiveStopIsEnabled())
			{
				TD.ChannelReceiveStop(channelHandler.EventTraceActivity, state.GetHashCode());
			}
			if (channelHandler.receiveWithTransaction)
			{
				channelHandler.SyncTransactionalMessagePump();
				return;
			}
			channelHandler.SyncMessagePump();
		}

		// Token: 0x06003458 RID: 13400 RVA: 0x000CA274 File Offset: 0x000C8474
		private static void OnStartSingleTransactedBatch(object state)
		{
			ChannelHandler channelHandler = state as ChannelHandler;
			channelHandler.TransactedBatchLoop();
		}

		// Token: 0x06003459 RID: 13401 RVA: 0x000CA28F File Offset: 0x000C848F
		private static void OnAsyncReceiveComplete(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				((ChannelHandler)result.AsyncState).AsyncMessagePump(result);
			}
		}

		// Token: 0x0600345A RID: 13402 RVA: 0x000CA2AC File Offset: 0x000C84AC
		private static void OnContinueAsyncReceive(object state)
		{
			IAsyncResult asyncResult = (IAsyncResult)state;
			((ChannelHandler)asyncResult.AsyncState).AsyncMessagePump(asyncResult);
		}

		// Token: 0x0600345B RID: 13403 RVA: 0x000CA2D1 File Offset: 0x000C84D1
		private static void OpenAndEnsurePump(object state)
		{
			((ChannelHandler)state).OpenAndEnsurePump();
		}

		// Token: 0x0600345C RID: 13404 RVA: 0x000CA2E0 File Offset: 0x000C84E0
		private void OpenAndEnsurePump()
		{
			Exception ex = null;
			try
			{
				this.binder.Channel.Open();
			}
			catch (Exception ex2)
			{
				if (Fx.IsFatal(ex2))
				{
					throw;
				}
				ex = ex2;
			}
			if (ex != null)
			{
				if (DiagnosticUtility.ShouldTraceWarning)
				{
					TraceUtility.TraceEvent(TraceEventType.Warning, 524351, SR.GetString("TraceCodeFailedToOpenIncomingChannel"));
				}
				ServiceChannel.SessionIdleManager sessionIdleManager = this.idleManager;
				if (sessionIdleManager != null)
				{
					sessionIdleManager.CancelTimer();
				}
				if (this.throttle != null && this.hasSession)
				{
					this.throttle.DeactivateChannel();
				}
				bool flag = this.HandleError(ex);
				if (this.incrementedActivityCountInConstructor)
				{
					this.listener.ChannelDispatcher.Channels.DecrementActivityCount();
				}
				if (!flag)
				{
					this.binder.Channel.Abort();
					return;
				}
			}
			else
			{
				this.EnsurePump();
			}
		}

		// Token: 0x0600345D RID: 13405 RVA: 0x000CA3AC File Offset: 0x000C85AC
		private bool TryReceive(TimeSpan timeout, out RequestContext requestContext)
		{
			this.shouldRejectMessageWithOnOpenActionHeader = !this.needToCreateSessionOpenNotificationMessage;
			bool flag;
			if (this.needToCreateSessionOpenNotificationMessage)
			{
				this.needToCreateSessionOpenNotificationMessage = false;
				requestContext = this.GetSessionOpenNotificationRequestContext();
				flag = true;
			}
			else
			{
				flag = this.receiver.TryReceive(timeout, out requestContext);
			}
			if (flag)
			{
				this.HandleReceiveComplete(requestContext);
			}
			return flag;
		}

		// Token: 0x0600345E RID: 13406 RVA: 0x000CA400 File Offset: 0x000C8600
		private void ReplyAddressFilterDidNotMatch(RequestContext request)
		{
			FaultCode code = FaultCode.CreateSenderFaultCode("DestinationUnreachable", this.messageVersion.Addressing.Namespace);
			string @string = SR.GetString("SFxNoEndpointMatchingAddress", new object[]
			{
				request.RequestMessage.Headers.To
			});
			this.ReplyFailure(request, code, @string);
		}

		// Token: 0x0600345F RID: 13407 RVA: 0x000CA458 File Offset: 0x000C8658
		private void ReplyContractFilterDidNotMatch(RequestContext request)
		{
			AddressingVersion addressing = this.messageVersion.Addressing;
			if (addressing != AddressingVersion.None && request.RequestMessage.Headers.Action == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageHeaderException(SR.GetString("SFxMissingActionHeader", new object[]
				{
					addressing.Namespace
				}), "Action", addressing.Namespace));
			}
			FaultCode code = FaultCode.CreateSenderFaultCode("ActionNotSupported", this.messageVersion.Addressing.Namespace);
			string @string = SR.GetString("SFxNoEndpointMatchingContract", new object[]
			{
				request.RequestMessage.Headers.Action
			});
			this.ReplyFailure(request, code, @string, this.messageVersion.Addressing.FaultAction);
		}

		// Token: 0x06003460 RID: 13408 RVA: 0x000CA518 File Offset: 0x000C8718
		private void ReplyChannelTerminated(RequestContext request)
		{
			FaultCode faultCode = FaultCode.CreateSenderFaultCode("SessionTerminated", "http://schemas.microsoft.com/net/2005/12/windowscommunicationfoundation/dispatcher");
			string @string = SR.GetString("SFxChannelTerminated0");
			string action = "http://schemas.microsoft.com/net/2005/12/windowscommunicationfoundation/dispatcher/fault";
			Message fault = Message.CreateMessage(this.messageVersion, faultCode, @string, action);
			this.ReplyFailure(request, fault, action, @string, faultCode);
		}

		// Token: 0x06003461 RID: 13409 RVA: 0x000CA560 File Offset: 0x000C8760
		private void ReplyFailure(RequestContext request, FaultCode code, string reason)
		{
			string defaultFaultAction = this.messageVersion.Addressing.DefaultFaultAction;
			this.ReplyFailure(request, code, reason, defaultFaultAction);
		}

		// Token: 0x06003462 RID: 13410 RVA: 0x000CA588 File Offset: 0x000C8788
		private void ReplyFailure(RequestContext request, FaultCode code, string reason, string action)
		{
			Message fault = Message.CreateMessage(this.messageVersion, code, reason, action);
			this.ReplyFailure(request, fault, action, reason, code);
		}

		// Token: 0x06003463 RID: 13411 RVA: 0x000CA5B4 File Offset: 0x000C87B4
		private void ReplyFailure(RequestContext request, Message fault, string action, string reason, FaultCode code)
		{
			FaultException ex = new FaultException(reason, code);
			ErrorBehavior.ThrowAndCatch(ex);
			ErrorHandlerFaultInfo errorHandlerFaultInfo = new ErrorHandlerFaultInfo(action);
			errorHandlerFaultInfo.Fault = fault;
			bool flag;
			bool flag2;
			this.ProvideFaultAndReplyFailure(request, ex, ref errorHandlerFaultInfo, out flag, out flag2);
			this.HandleError(ex, ref errorHandlerFaultInfo);
		}

		// Token: 0x06003464 RID: 13412 RVA: 0x000CA5FC File Offset: 0x000C87FC
		private void ProvideFaultAndReplyFailure(RequestContext request, Exception exception, ref ErrorHandlerFaultInfo faultInfo, out bool replied, out bool replySentAsync)
		{
			replied = false;
			replySentAsync = false;
			bool flag = false;
			try
			{
				flag = request.RequestMessage.IsFault;
			}
			catch (Exception exception2)
			{
				if (Fx.IsFatal(exception2))
				{
					throw;
				}
			}
			bool flag2 = false;
			if (this.listener != null)
			{
				flag2 = this.listener.ChannelDispatcher.EnableFaults;
			}
			else if (this.channel != null && this.channel.IsClient)
			{
				flag2 = this.channel.ClientRuntime.EnableFaults;
			}
			if (!flag && flag2)
			{
				this.ProvideFault(exception, ref faultInfo);
				if (faultInfo.Fault != null)
				{
					Message fault = faultInfo.Fault;
					try
					{
						try
						{
							if (this.PrepareReply(request, fault))
							{
								if (this.sendAsynchronously)
								{
									ChannelHandler.ContinuationState continuationState = new ChannelHandler.ContinuationState
									{
										ChannelHandler = this,
										Channel = this.channel,
										Exception = exception,
										FaultInfo = faultInfo,
										Request = request,
										Reply = fault
									};
									IAsyncResult asyncResult = request.BeginReply(fault, ChannelHandler.onAsyncReplyComplete, continuationState);
									if (asyncResult.CompletedSynchronously)
									{
										ChannelHandler.AsyncReplyComplete(asyncResult, continuationState);
										replied = true;
									}
									else
									{
										replySentAsync = true;
									}
								}
								else
								{
									request.Reply(fault);
									replied = true;
								}
							}
						}
						finally
						{
							if (!replySentAsync)
							{
								fault.Close();
							}
						}
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						this.HandleError(ex);
					}
				}
			}
		}

		// Token: 0x06003465 RID: 13413 RVA: 0x000CA780 File Offset: 0x000C8980
		private bool PrepareReply(RequestContext request, Message reply)
		{
			if (this.replied == request)
			{
				return false;
			}
			this.replied = request;
			bool flag = true;
			Message message = null;
			try
			{
				message = request.RequestMessage;
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
			}
			if (message != null)
			{
				UniqueId uniqueId = null;
				try
				{
					uniqueId = message.Headers.MessageId;
				}
				catch (MessageHeaderException)
				{
				}
				if (uniqueId != null && !this.isManualAddressing)
				{
					RequestReplyCorrelator.PrepareReply(reply, uniqueId);
				}
				if (!this.hasSession && !this.isManualAddressing)
				{
					try
					{
						flag = RequestReplyCorrelator.AddressReply(reply, message);
					}
					catch (MessageHeaderException)
					{
					}
				}
			}
			return this.IsOpen && flag;
		}

		// Token: 0x06003466 RID: 13414 RVA: 0x000CA830 File Offset: 0x000C8A30
		private static void AsyncReplyComplete(IAsyncResult result, ChannelHandler.ContinuationState state)
		{
			try
			{
				state.Request.EndReply(result);
			}
			catch (Exception ex)
			{
				DiagnosticUtility.TraceHandledException(ex, TraceEventType.Error);
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				state.ChannelHandler.HandleError(ex);
			}
			try
			{
				state.Reply.Close();
			}
			catch (Exception ex2)
			{
				DiagnosticUtility.TraceHandledException(ex2, TraceEventType.Error);
				if (Fx.IsFatal(ex2))
				{
					throw;
				}
				state.ChannelHandler.HandleError(ex2);
			}
			try
			{
				state.ChannelHandler.HandleErrorContinuation(state.Exception, state.Request, state.Channel, ref state.FaultInfo, true);
			}
			catch (Exception ex3)
			{
				DiagnosticUtility.TraceHandledException(ex3, TraceEventType.Error);
				if (Fx.IsFatal(ex3))
				{
					throw;
				}
				state.ChannelHandler.HandleError(ex3);
			}
			state.ChannelHandler.EnsurePump();
		}

		// Token: 0x06003467 RID: 13415 RVA: 0x000CA918 File Offset: 0x000C8B18
		private static void OnAsyncReplyComplete(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			try
			{
				ChannelHandler.ContinuationState state = (ChannelHandler.ContinuationState)result.AsyncState;
				ChannelHandler.AsyncReplyComplete(result, state);
			}
			catch (Exception exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Error);
				if (Fx.IsFatal(exception))
				{
					throw;
				}
			}
		}

		// Token: 0x06003468 RID: 13416 RVA: 0x000CA968 File Offset: 0x000C8B68
		private void ReleasePump()
		{
			if (this.isConcurrent)
			{
				Interlocked.Exchange(ref this.isPumpAcquired, 0);
			}
		}

		// Token: 0x06003469 RID: 13417 RVA: 0x000CA980 File Offset: 0x000C8B80
		private void SyncMessagePump()
		{
			OperationContext value = OperationContext.Current;
			try
			{
				OperationContext operationContext = new OperationContext(this.host);
				OperationContext.Current = operationContext;
				for (;;)
				{
					this.requestInfo.Cleanup();
					RequestContext request;
					while (!this.TryReceive(TimeSpan.MaxValue, out request))
					{
					}
					if (!this.HandleRequest(request, operationContext))
					{
						break;
					}
					if (!this.TryAcquirePump())
					{
						break;
					}
					operationContext.Recycle();
				}
			}
			finally
			{
				OperationContext.Current = value;
			}
		}

		// Token: 0x0600346A RID: 13418 RVA: 0x000CA9F4 File Offset: 0x000C8BF4
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SyncTransactionalMessagePump()
		{
			bool flag;
			do
			{
				if (this.sharedTransactedBatchContext == null)
				{
					flag = this.TransactedLoop();
				}
				else
				{
					flag = this.TransactedBatchLoop();
				}
			}
			while (flag);
		}

		// Token: 0x0600346B RID: 13419 RVA: 0x000CAA1C File Offset: 0x000C8C1C
		private bool TransactedLoop()
		{
			try
			{
				this.receiver.WaitForMessage();
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				if (!this.HandleError(ex))
				{
					throw;
				}
			}
			Transaction transaction = this.CreateOrGetAttachedTransaction();
			OperationContext value = OperationContext.Current;
			bool result;
			try
			{
				OperationContext operationContext = new OperationContext(this.host);
				OperationContext.Current = operationContext;
				for (;;)
				{
					this.requestInfo.Cleanup();
					RequestContext requestContext;
					if (!this.TryTransactionalReceive(transaction, out requestContext))
					{
						break;
					}
					if (requestContext == null)
					{
						goto Block_8;
					}
					TransactionMessageProperty.Set(transaction, requestContext.RequestMessage);
					if (!this.HandleRequest(requestContext, operationContext))
					{
						goto Block_9;
					}
					if (!this.TryAcquirePump())
					{
						goto Block_10;
					}
					transaction = this.CreateOrGetAttachedTransaction();
					operationContext.Recycle();
				}
				return this.IsOpen;
				Block_8:
				return false;
				Block_9:
				return false;
				Block_10:
				result = false;
			}
			finally
			{
				OperationContext.Current = value;
			}
			return result;
		}

		// Token: 0x0600346C RID: 13420 RVA: 0x000CAAFC File Offset: 0x000C8CFC
		private bool TransactedBatchLoop()
		{
			if (this.transactedBatchContext != null)
			{
				if (this.transactedBatchContext.InDispatch)
				{
					this.transactedBatchContext.ForceRollback();
					this.transactedBatchContext.InDispatch = false;
				}
				if (!this.transactedBatchContext.IsActive)
				{
					if (!this.isMainTransactedBatchHandler)
					{
						return false;
					}
					this.transactedBatchContext = null;
				}
			}
			if (this.transactedBatchContext == null)
			{
				try
				{
					this.receiver.WaitForMessage();
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					if (!this.HandleError(ex))
					{
						throw;
					}
				}
				this.transactedBatchContext = this.sharedTransactedBatchContext.CreateTransactedBatchContext();
			}
			OperationContext value = OperationContext.Current;
			try
			{
				OperationContext operationContext = new OperationContext(this.host);
				OperationContext.Current = operationContext;
				while (this.transactedBatchContext.IsActive)
				{
					this.requestInfo.Cleanup();
					RequestContext requestContext;
					if (!this.TryTransactionalReceive(this.transactedBatchContext.Transaction, out requestContext))
					{
						if (this.IsOpen)
						{
							this.transactedBatchContext.ForceCommit();
							return true;
						}
						this.transactedBatchContext.ForceRollback();
						return false;
					}
					else
					{
						if (requestContext == null)
						{
							this.transactedBatchContext.ForceRollback();
							return false;
						}
						TransactionMessageProperty.Set(this.transactedBatchContext.Transaction, requestContext.RequestMessage);
						this.transactedBatchContext.InDispatch = true;
						if (!this.HandleRequest(requestContext, operationContext))
						{
							return false;
						}
						if (this.transactedBatchContext.InDispatch)
						{
							this.transactedBatchContext.ForceRollback();
							this.transactedBatchContext.InDispatch = false;
							return true;
						}
						if (!this.TryAcquirePump())
						{
							return false;
						}
						operationContext.Recycle();
					}
				}
			}
			finally
			{
				OperationContext.Current = value;
			}
			return true;
		}

		// Token: 0x0600346D RID: 13421 RVA: 0x000CACBC File Offset: 0x000C8EBC
		private Transaction CreateOrGetAttachedTransaction()
		{
			if (this.acceptTransaction != null)
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					if (this.acceptTransaction != null)
					{
						Transaction transaction = this.acceptTransaction.Transaction;
						this.acceptTransaction = null;
						return transaction;
					}
				}
			}
			if (this.InstanceContext != null && this.InstanceContext.HasTransaction)
			{
				return this.InstanceContext.Transaction.Attached;
			}
			return TransactionBehavior.CreateTransaction(this.listener.ChannelDispatcher.TransactionIsolationLevel, TransactionBehavior.NormalizeTimeout(this.listener.ChannelDispatcher.TransactionTimeout));
		}

		// Token: 0x0600346E RID: 13422 RVA: 0x000CAD70 File Offset: 0x000C8F70
		private bool TryTransactionalReceive(Transaction tx, out RequestContext request)
		{
			request = null;
			bool flag = false;
			try
			{
				using (TransactionScope transactionScope = new TransactionScope(tx))
				{
					if (this.sharedTransactedBatchContext != null)
					{
						object receiveLock = this.sharedTransactedBatchContext.ReceiveLock;
						lock (receiveLock)
						{
							if (this.transactedBatchContext.AboutToExpire)
							{
								return false;
							}
							flag = this.receiver.TryReceive(TimeSpan.Zero, out request);
							goto IL_9D;
						}
					}
					TimeSpan timeout = TimeoutHelper.Min(this.listener.ChannelDispatcher.TransactionTimeout, this.listener.ChannelDispatcher.DefaultCommunicationTimeouts.ReceiveTimeout);
					flag = this.receiver.TryReceive(TransactionBehavior.NormalizeTimeout(timeout), out request);
					IL_9D:
					transactionScope.Complete();
				}
				if (flag)
				{
					this.HandleReceiveComplete(request);
				}
			}
			catch (ObjectDisposedException e)
			{
				this.HandleError(e);
				request = null;
				return false;
			}
			catch (TransactionException e2)
			{
				this.HandleError(e2);
				request = null;
				return false;
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				if (!this.HandleError(ex))
				{
					throw;
				}
			}
			return flag;
		}

		// Token: 0x0600346F RID: 13423 RVA: 0x000CAEC0 File Offset: 0x000C90C0
		internal void ThrottleAcquiredForCall()
		{
			RequestContext requestContext = this.requestWaitingForThrottle;
			if (DS.ServiceThrottleIsEnabled())
			{
				DS.CallThrottleAcquired(requestContext.RequestMessage);
			}
			this.requestWaitingForThrottle = null;
			bool channelHandlerOwnsCallThrottle = this.requestInfo.ChannelHandlerOwnsCallThrottle;
			this.requestInfo.ChannelHandlerOwnsCallThrottle = true;
			if (!this.TryRetrievingInstanceContext(requestContext))
			{
				this.EnsurePump();
				return;
			}
			this.requestInfo.Channel.CompletedIOOperation();
			if (this.TryAcquireThrottle(requestContext, this.requestInfo.ExistingInstanceContext == null))
			{
				bool channelHandlerOwnsInstanceContextThrottle = this.requestInfo.ChannelHandlerOwnsInstanceContextThrottle;
				this.requestInfo.ChannelHandlerOwnsInstanceContextThrottle = (this.requestInfo.ExistingInstanceContext == null);
				if (this.DispatchAndReleasePump(requestContext, false, null))
				{
					this.EnsurePump();
				}
			}
		}

		// Token: 0x06003470 RID: 13424 RVA: 0x000CAF74 File Offset: 0x000C9174
		private bool TryRetrievingInstanceContext(RequestContext request)
		{
			bool result;
			try
			{
				result = this.TryRetrievingInstanceContextCore(request);
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Error);
				try
				{
					request.Close();
				}
				catch (Exception exception2)
				{
					if (Fx.IsFatal(exception2))
					{
						throw;
					}
					request.Abort();
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06003471 RID: 13425 RVA: 0x000CAFDC File Offset: 0x000C91DC
		private bool TryRetrievingInstanceContextCore(RequestContext request)
		{
			bool flag = true;
			try
			{
				if (!this.requestInfo.EndpointLookupDone)
				{
					this.EnsureChannelAndEndpoint(request);
				}
				if (this.requestInfo.Channel == null)
				{
					return false;
				}
				if (this.requestInfo.DispatchRuntime != null)
				{
					IContextChannel contextChannel = this.requestInfo.Channel.Proxy as IContextChannel;
					try
					{
						this.requestInfo.ExistingInstanceContext = this.requestInfo.DispatchRuntime.InstanceContextProvider.GetExistingInstanceContext(request.RequestMessage, contextChannel);
						flag = false;
						goto IL_C2;
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						this.requestInfo.Channel = null;
						this.HandleError(ex, request, this.channel);
						return false;
					}
					goto IL_A2;
					IL_C2:
					return true;
				}
				IL_A2:
				TraceUtility.TraceDroppedMessage(request.RequestMessage, this.requestInfo.Endpoint);
				request.Close();
				return false;
			}
			catch (Exception ex2)
			{
				if (Fx.IsFatal(ex2))
				{
					throw;
				}
				this.HandleError(ex2, request, this.channel);
				return false;
			}
			finally
			{
				if (flag)
				{
					this.ReleasePump();
				}
			}
			return true;
		}

		// Token: 0x06003472 RID: 13426 RVA: 0x000CB104 File Offset: 0x000C9304
		internal void ThrottleAcquired()
		{
			RequestContext requestContext = this.requestWaitingForThrottle;
			if (DS.ServiceThrottleIsEnabled())
			{
				DS.InstanceThrottleAcquired(requestContext.RequestMessage);
			}
			this.requestWaitingForThrottle = null;
			bool channelHandlerOwnsInstanceContextThrottle = this.requestInfo.ChannelHandlerOwnsInstanceContextThrottle;
			this.requestInfo.ChannelHandlerOwnsInstanceContextThrottle = (this.requestInfo.ExistingInstanceContext == null);
			if (this.DispatchAndReleasePump(requestContext, false, null))
			{
				this.EnsurePump();
			}
		}

		// Token: 0x06003473 RID: 13427 RVA: 0x000CB168 File Offset: 0x000C9368
		private bool TryAcquireThrottle(RequestContext request, bool acquireInstanceContextThrottle)
		{
			ServiceThrottle serviceThrottle = this.throttle;
			if (serviceThrottle == null || !serviceThrottle.IsActive)
			{
				return true;
			}
			this.requestWaitingForThrottle = request;
			if (serviceThrottle.AcquireInstanceContextAndDynamic(this, acquireInstanceContextThrottle))
			{
				this.requestWaitingForThrottle = null;
				return true;
			}
			if (DS.ServiceThrottleIsEnabled())
			{
				DS.InstanceThrottleWaiting(request.RequestMessage);
			}
			return false;
		}

		// Token: 0x06003474 RID: 13428 RVA: 0x000CB1B8 File Offset: 0x000C93B8
		private bool TryAcquireCallThrottle(RequestContext request)
		{
			ServiceThrottle serviceThrottle = this.throttle;
			if (serviceThrottle == null || !serviceThrottle.IsActive)
			{
				return true;
			}
			this.requestWaitingForThrottle = request;
			if (serviceThrottle.AcquireCall(this))
			{
				this.requestWaitingForThrottle = null;
				return true;
			}
			return false;
		}

		// Token: 0x06003475 RID: 13429 RVA: 0x000CB1F3 File Offset: 0x000C93F3
		private bool TryAcquirePump()
		{
			return !this.isConcurrent || Interlocked.CompareExchange(ref this.isPumpAcquired, 1, 0) == 0;
		}

		// Token: 0x06003476 RID: 13430 RVA: 0x000CB210 File Offset: 0x000C9410
		private EventTraceActivity TraceDispatchMessageStart(Message message)
		{
			if (FxTrace.Trace.IsEnd2EndActivityTracingEnabled && message != null)
			{
				EventTraceActivity result = EventTraceActivityHelper.TryExtractActivity(message);
				if (TD.DispatchMessageStartIsEnabled())
				{
					TD.DispatchMessageStart(result);
				}
				return result;
			}
			return null;
		}

		// Token: 0x040027CC RID: 10188
		public static readonly TimeSpan CloseAfterFaultTimeout = TimeSpan.FromSeconds(10.0);

		// Token: 0x040027CD RID: 10189
		public const string MessageBufferPropertyName = "_RequestMessageBuffer_";

		// Token: 0x040027CE RID: 10190
		private readonly IChannelBinder binder;

		// Token: 0x040027CF RID: 10191
		private readonly DuplexChannelBinder duplexBinder;

		// Token: 0x040027D0 RID: 10192
		private readonly ServiceHostBase host;

		// Token: 0x040027D1 RID: 10193
		private readonly bool incrementedActivityCountInConstructor;

		// Token: 0x040027D2 RID: 10194
		private readonly bool isCallback;

		// Token: 0x040027D3 RID: 10195
		private readonly ListenerHandler listener;

		// Token: 0x040027D4 RID: 10196
		private readonly ServiceThrottle throttle;

		// Token: 0x040027D5 RID: 10197
		private readonly bool wasChannelThrottled;

		// Token: 0x040027D6 RID: 10198
		private readonly ServiceChannel.SessionIdleManager idleManager;

		// Token: 0x040027D7 RID: 10199
		private readonly bool sendAsynchronously;

		// Token: 0x040027D8 RID: 10200
		private static AsyncCallback onAsyncReplyComplete = Fx.ThunkCallback(new AsyncCallback(ChannelHandler.OnAsyncReplyComplete));

		// Token: 0x040027D9 RID: 10201
		private static AsyncCallback onAsyncReceiveComplete = Fx.ThunkCallback(new AsyncCallback(ChannelHandler.OnAsyncReceiveComplete));

		// Token: 0x040027DA RID: 10202
		private static Action<object> onContinueAsyncReceive = new Action<object>(ChannelHandler.OnContinueAsyncReceive);

		// Token: 0x040027DB RID: 10203
		private static Action<object> onStartSyncMessagePump = new Action<object>(ChannelHandler.OnStartSyncMessagePump);

		// Token: 0x040027DC RID: 10204
		private static Action<object> onStartAsyncMessagePump = new Action<object>(ChannelHandler.OnStartAsyncMessagePump);

		// Token: 0x040027DD RID: 10205
		private static Action<object> onStartSingleTransactedBatch = new Action<object>(ChannelHandler.OnStartSingleTransactedBatch);

		// Token: 0x040027DE RID: 10206
		private static Action<object> openAndEnsurePump = new Action<object>(ChannelHandler.OpenAndEnsurePump);

		// Token: 0x040027DF RID: 10207
		private ChannelHandler.RequestInfo requestInfo;

		// Token: 0x040027E0 RID: 10208
		private ServiceChannel channel;

		// Token: 0x040027E1 RID: 10209
		private bool doneReceiving;

		// Token: 0x040027E2 RID: 10210
		private bool hasRegisterBeenCalled;

		// Token: 0x040027E3 RID: 10211
		private bool hasSession;

		// Token: 0x040027E4 RID: 10212
		private int isPumpAcquired;

		// Token: 0x040027E5 RID: 10213
		private bool isChannelTerminated;

		// Token: 0x040027E6 RID: 10214
		private bool isConcurrent;

		// Token: 0x040027E7 RID: 10215
		private bool isManualAddressing;

		// Token: 0x040027E8 RID: 10216
		private MessageVersion messageVersion;

		// Token: 0x040027E9 RID: 10217
		private ErrorHandlingReceiver receiver;

		// Token: 0x040027EA RID: 10218
		private bool receiveSynchronously;

		// Token: 0x040027EB RID: 10219
		private bool receiveWithTransaction;

		// Token: 0x040027EC RID: 10220
		private RequestContext replied;

		// Token: 0x040027ED RID: 10221
		private RequestContext requestWaitingForThrottle;

		// Token: 0x040027EE RID: 10222
		private WrappedTransaction acceptTransaction;

		// Token: 0x040027EF RID: 10223
		private ServiceThrottle instanceContextThrottle;

		// Token: 0x040027F0 RID: 10224
		private SharedTransactedBatchContext sharedTransactedBatchContext;

		// Token: 0x040027F1 RID: 10225
		private TransactedBatchContext transactedBatchContext;

		// Token: 0x040027F2 RID: 10226
		private bool isMainTransactedBatchHandler;

		// Token: 0x040027F3 RID: 10227
		private EventTraceActivity eventTraceActivity;

		// Token: 0x040027F4 RID: 10228
		private SessionOpenNotification sessionOpenNotification;

		// Token: 0x040027F5 RID: 10229
		private bool needToCreateSessionOpenNotificationMessage;

		// Token: 0x040027F6 RID: 10230
		private bool shouldRejectMessageWithOnOpenActionHeader;

		// Token: 0x02000C74 RID: 3188
		private struct RequestInfo
		{
			// Token: 0x0600781C RID: 30748 RVA: 0x001C12CC File Offset: 0x001BF4CC
			public RequestInfo(ChannelHandler channelHandler)
			{
				this.Endpoint = null;
				this.ExistingInstanceContext = null;
				this.Channel = null;
				this.EndpointLookupDone = false;
				this.DispatchRuntime = null;
				this.RequestContext = null;
				this.ChannelHandler = channelHandler;
				this.ChannelHandlerOwnsCallThrottle = false;
				this.ChannelHandlerOwnsInstanceContextThrottle = false;
			}

			// Token: 0x0600781D RID: 30749 RVA: 0x001C1318 File Offset: 0x001BF518
			public void Cleanup()
			{
				if (this.ChannelHandlerOwnsInstanceContextThrottle)
				{
					this.ChannelHandler.throttle.DeactivateInstanceContext();
					this.ChannelHandlerOwnsInstanceContextThrottle = false;
				}
				this.Endpoint = null;
				this.ExistingInstanceContext = null;
				this.Channel = null;
				this.EndpointLookupDone = false;
				this.RequestContext = null;
				if (this.ChannelHandlerOwnsCallThrottle)
				{
					this.ChannelHandler.DispatchDone();
					this.ChannelHandlerOwnsCallThrottle = false;
				}
			}

			// Token: 0x04004483 RID: 17539
			public EndpointDispatcher Endpoint;

			// Token: 0x04004484 RID: 17540
			public InstanceContext ExistingInstanceContext;

			// Token: 0x04004485 RID: 17541
			public ServiceChannel Channel;

			// Token: 0x04004486 RID: 17542
			public bool EndpointLookupDone;

			// Token: 0x04004487 RID: 17543
			public DispatchRuntime DispatchRuntime;

			// Token: 0x04004488 RID: 17544
			public RequestContext RequestContext;

			// Token: 0x04004489 RID: 17545
			public ChannelHandler ChannelHandler;

			// Token: 0x0400448A RID: 17546
			public bool ChannelHandlerOwnsCallThrottle;

			// Token: 0x0400448B RID: 17547
			public bool ChannelHandlerOwnsInstanceContextThrottle;
		}

		// Token: 0x02000C75 RID: 3189
		private struct ContinuationState
		{
			// Token: 0x0400448C RID: 17548
			public ChannelHandler ChannelHandler;

			// Token: 0x0400448D RID: 17549
			public Exception Exception;

			// Token: 0x0400448E RID: 17550
			public RequestContext Request;

			// Token: 0x0400448F RID: 17551
			public Message Reply;

			// Token: 0x04004490 RID: 17552
			public ServiceChannel Channel;

			// Token: 0x04004491 RID: 17553
			public ErrorHandlerFaultInfo FaultInfo;
		}
	}
}
