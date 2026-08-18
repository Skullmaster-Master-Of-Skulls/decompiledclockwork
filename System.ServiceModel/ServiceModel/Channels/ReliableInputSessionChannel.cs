using System;
using System.Collections.Generic;
using System.Runtime;
using System.ServiceModel.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200092C RID: 2348
	internal abstract class ReliableInputSessionChannel : InputChannel, IInputSessionChannel, IInputChannel, IChannel, ICommunicationObject, ISessionChannel<IInputSession>
	{
		// Token: 0x06005A2A RID: 23082 RVA: 0x0014A6F0 File Offset: 0x001488F0
		protected ReliableInputSessionChannel(ReliableChannelListenerBase<IInputSessionChannel> listener, IServerReliableChannelBinder binder, FaultHelper faultHelper, UniqueId inputID) : base(listener, binder.LocalAddress)
		{
			this.binder = binder;
			this.listener = listener;
			this.connection = new ReliableInputConnection();
			this.connection.ReliableMessagingVersion = listener.ReliableMessagingVersion;
			this.session = new ServerReliableSession(this, listener, binder, faultHelper, inputID, null);
			this.session.UnblockChannelCloseCallback = new ChannelReliableSession.UnblockChannelCloseHandler(this.UnblockClose);
			if (listener.Ordered)
			{
				this.deliveryStrategy = new OrderedDeliveryStrategy<Message>(this, listener.MaxTransferWindowSize, false);
			}
			else
			{
				this.deliveryStrategy = new UnorderedDeliveryStrategy<Message>(this, listener.MaxTransferWindowSize);
			}
			this.binder.Faulted += this.OnBinderFaulted;
			this.binder.OnException += this.OnBinderException;
			this.session.Open(TimeSpan.Zero);
			if (PerformanceCounters.PerformanceCountersEnabled)
			{
				this.perfCounterId = this.listener.Uri.ToString().ToUpperInvariant();
			}
		}

		// Token: 0x170015DE RID: 5598
		// (get) Token: 0x06005A2B RID: 23083 RVA: 0x0014A7EC File Offset: 0x001489EC
		// (set) Token: 0x06005A2C RID: 23084 RVA: 0x0014A7F4 File Offset: 0x001489F4
		protected bool AdvertisedZero
		{
			get
			{
				return this.advertisedZero;
			}
			set
			{
				this.advertisedZero = value;
			}
		}

		// Token: 0x170015DF RID: 5599
		// (get) Token: 0x06005A2D RID: 23085 RVA: 0x0014A7FD File Offset: 0x001489FD
		public IServerReliableChannelBinder Binder
		{
			get
			{
				return this.binder;
			}
		}

		// Token: 0x170015E0 RID: 5600
		// (get) Token: 0x06005A2E RID: 23086 RVA: 0x0014A805 File Offset: 0x00148A05
		protected ReliableInputConnection Connection
		{
			get
			{
				return this.connection;
			}
		}

		// Token: 0x170015E1 RID: 5601
		// (get) Token: 0x06005A2F RID: 23087 RVA: 0x0014A80D File Offset: 0x00148A0D
		protected DeliveryStrategy<Message> DeliveryStrategy
		{
			get
			{
				return this.deliveryStrategy;
			}
		}

		// Token: 0x170015E2 RID: 5602
		// (get) Token: 0x06005A30 RID: 23088 RVA: 0x0014A815 File Offset: 0x00148A15
		protected ReliableChannelListenerBase<IInputSessionChannel> Listener
		{
			get
			{
				return this.listener;
			}
		}

		// Token: 0x170015E3 RID: 5603
		// (get) Token: 0x06005A31 RID: 23089 RVA: 0x0014A81D File Offset: 0x00148A1D
		protected ChannelReliableSession ReliableSession
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x170015E4 RID: 5604
		// (get) Token: 0x06005A32 RID: 23090 RVA: 0x0014A825 File Offset: 0x00148A25
		public IInputSession Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x06005A33 RID: 23091 RVA: 0x0014A82D File Offset: 0x00148A2D
		protected virtual void AggregateAsyncCloseOperations(List<OperationWithTimeoutBeginCallback> beginOperations, List<OperationEndCallback> endOperations)
		{
			beginOperations.Add(new OperationWithTimeoutBeginCallback(this.session.BeginClose));
			endOperations.Add(new OperationEndCallback(this.session.EndClose));
		}

		// Token: 0x06005A34 RID: 23092 RVA: 0x0014A860 File Offset: 0x00148A60
		private static void AsyncReceiveCompleteStatic(object state)
		{
			IAsyncResult asyncResult = (IAsyncResult)state;
			ReliableInputSessionChannel reliableInputSessionChannel = (ReliableInputSessionChannel)asyncResult.AsyncState;
			try
			{
				if (reliableInputSessionChannel.HandleReceiveComplete(asyncResult))
				{
					reliableInputSessionChannel.StartReceiving(true);
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				reliableInputSessionChannel.ReliableSession.OnUnknownException(ex);
			}
		}

		// Token: 0x06005A35 RID: 23093 RVA: 0x0014A8BC File Offset: 0x00148ABC
		private static void OnReceiveCompletedStatic(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			ReliableInputSessionChannel reliableInputSessionChannel = (ReliableInputSessionChannel)result.AsyncState;
			try
			{
				if (reliableInputSessionChannel.HandleReceiveComplete(result))
				{
					reliableInputSessionChannel.StartReceiving(true);
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				reliableInputSessionChannel.ReliableSession.OnUnknownException(ex);
			}
		}

		// Token: 0x06005A36 RID: 23094
		protected abstract bool HandleReceiveComplete(IAsyncResult result);

		// Token: 0x06005A37 RID: 23095 RVA: 0x0014A91C File Offset: 0x00148B1C
		protected virtual void AbortGuards()
		{
		}

		// Token: 0x06005A38 RID: 23096 RVA: 0x0014A920 File Offset: 0x00148B20
		protected void AddAcknowledgementHeader(Message message)
		{
			int num = -1;
			if (this.Listener.FlowControlEnabled)
			{
				num = this.Listener.MaxTransferWindowSize - this.deliveryStrategy.EnqueuedCount;
				this.AdvertisedZero = (num == 0);
			}
			WsrmUtilities.AddAcknowledgementHeader(this.listener.ReliableMessagingVersion, message, this.session.InputID, this.connection.Ranges, this.connection.IsLastKnown, num);
		}

		// Token: 0x06005A39 RID: 23097 RVA: 0x0014A991 File Offset: 0x00148B91
		private IAsyncResult BeginCloseBinder(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.binder.BeginClose(timeout, MaskingMode.Handled, callback, state);
		}

		// Token: 0x06005A3A RID: 23098 RVA: 0x0014A9A2 File Offset: 0x00148BA2
		protected virtual IAsyncResult BeginCloseGuards(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06005A3B RID: 23099 RVA: 0x0014A9AB File Offset: 0x00148BAB
		private IAsyncResult BeginUnregisterChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.listener.OnReliableChannelBeginClose(this.ReliableSession.InputID, null, timeout, callback, state);
		}

		// Token: 0x06005A3C RID: 23100 RVA: 0x0014A9C7 File Offset: 0x00148BC7
		protected override void OnClosed()
		{
			base.OnClosed();
			this.binder.Faulted -= this.OnBinderFaulted;
			this.deliveryStrategy.Dispose();
		}

		// Token: 0x06005A3D RID: 23101 RVA: 0x0014A9F1 File Offset: 0x00148BF1
		protected virtual void CloseGuards(TimeSpan timeout)
		{
		}

		// Token: 0x06005A3E RID: 23102 RVA: 0x0014A9F4 File Offset: 0x00148BF4
		protected Message CreateAcknowledgmentMessage()
		{
			int num = -1;
			if (this.Listener.FlowControlEnabled)
			{
				num = this.Listener.MaxTransferWindowSize - this.deliveryStrategy.EnqueuedCount;
				this.AdvertisedZero = (num == 0);
			}
			return WsrmUtilities.CreateAcknowledgmentMessage(this.listener.MessageVersion, this.listener.ReliableMessagingVersion, this.session.InputID, this.connection.Ranges, this.connection.IsLastKnown, num);
		}

		// Token: 0x06005A3F RID: 23103 RVA: 0x0014AA71 File Offset: 0x00148C71
		private void EndCloseBinder(IAsyncResult result)
		{
			this.binder.EndClose(result);
		}

		// Token: 0x06005A40 RID: 23104 RVA: 0x0014AA7F File Offset: 0x00148C7F
		protected virtual void EndCloseGuards(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x06005A41 RID: 23105 RVA: 0x0014AA87 File Offset: 0x00148C87
		private void EndUnregisterChannel(IAsyncResult result)
		{
			this.listener.OnReliableChannelEndClose(result);
		}

		// Token: 0x06005A42 RID: 23106 RVA: 0x0014AA98 File Offset: 0x00148C98
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(IInputSessionChannel))
			{
				return (T)((object)this);
			}
			T property = base.GetProperty<T>();
			if (property != null)
			{
				return property;
			}
			T property2 = this.binder.Channel.GetProperty<T>();
			if (property2 == null && typeof(T) == typeof(FaultConverter))
			{
				return (T)((object)FaultConverter.GetDefaultFaultConverter(this.listener.MessageVersion));
			}
			return property2;
		}

		// Token: 0x06005A43 RID: 23107 RVA: 0x0014AB23 File Offset: 0x00148D23
		protected override void OnAbort()
		{
			this.connection.Abort(this);
			this.AbortGuards();
			this.session.Abort();
			this.listener.OnReliableChannelAbort(this.ReliableSession.InputID, null);
			base.OnAbort();
		}

		// Token: 0x06005A44 RID: 23108 RVA: 0x0014AB60 File Offset: 0x00148D60
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.ThrowIfCloseInvalid();
			OperationWithTimeoutBeginCallback[] beginOperations = new OperationWithTimeoutBeginCallback[]
			{
				new OperationWithTimeoutBeginCallback(this.connection.BeginClose),
				new OperationWithTimeoutBeginCallback(this.session.BeginClose),
				new OperationWithTimeoutBeginCallback(this.BeginCloseGuards),
				new OperationWithTimeoutBeginCallback(this.BeginCloseBinder),
				new OperationWithTimeoutBeginCallback(this.BeginUnregisterChannel),
				new OperationWithTimeoutBeginCallback(base.OnBeginClose)
			};
			OperationEndCallback[] endOperations = new OperationEndCallback[]
			{
				new OperationEndCallback(this.connection.EndClose),
				new OperationEndCallback(this.session.EndClose),
				new OperationEndCallback(this.EndCloseGuards),
				new OperationEndCallback(this.EndCloseBinder),
				new OperationEndCallback(this.EndUnregisterChannel),
				new OperationEndCallback(base.OnEndClose)
			};
			return OperationWithTimeoutComposer.BeginComposeAsyncOperations(timeout, beginOperations, endOperations, callback, state);
		}

		// Token: 0x06005A45 RID: 23109 RVA: 0x0014AC57 File Offset: 0x00148E57
		private void OnBinderException(IReliableChannelBinder sender, Exception exception)
		{
			if (exception is QuotaExceededException)
			{
				this.session.OnLocalFault(exception, SequenceTerminatedFault.CreateQuotaExceededFault(this.session.OutputID), null);
				return;
			}
			base.EnqueueAndDispatch(exception, null, false);
		}

		// Token: 0x06005A46 RID: 23110 RVA: 0x0014AC88 File Offset: 0x00148E88
		private void OnBinderFaulted(IReliableChannelBinder sender, Exception exception)
		{
			this.binder.Abort();
			exception = new CommunicationException(SR.GetString("EarlySecurityFaulted"), exception);
			this.session.OnLocalFault(exception, null, null);
		}

		// Token: 0x06005A47 RID: 23111 RVA: 0x0014ACB8 File Offset: 0x00148EB8
		protected override void OnClose(TimeSpan timeout)
		{
			this.ThrowIfCloseInvalid();
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.connection.Close(timeoutHelper.RemainingTime());
			this.session.Close(timeoutHelper.RemainingTime());
			this.CloseGuards(timeoutHelper.RemainingTime());
			this.binder.Close(timeoutHelper.RemainingTime(), MaskingMode.Handled);
			this.listener.OnReliableChannelClose(this.ReliableSession.InputID, null, timeoutHelper.RemainingTime());
			base.OnClose(timeoutHelper.RemainingTime());
		}

		// Token: 0x06005A48 RID: 23112 RVA: 0x0014AD42 File Offset: 0x00148F42
		protected override void OnEndClose(IAsyncResult result)
		{
			OperationWithTimeoutComposer.EndComposeAsyncOperations(result);
		}

		// Token: 0x06005A49 RID: 23113 RVA: 0x0014AD4A File Offset: 0x00148F4A
		protected override void OnFaulted()
		{
			this.session.OnFaulted();
			this.UnblockClose();
			base.OnFaulted();
			if (PerformanceCounters.PerformanceCountersEnabled)
			{
				PerformanceCounters.SessionFaulted(this.perfCounterId);
			}
		}

		// Token: 0x06005A4A RID: 23114 RVA: 0x0014AD75 File Offset: 0x00148F75
		protected virtual void OnQuotaAvailable()
		{
		}

		// Token: 0x06005A4B RID: 23115 RVA: 0x0014AD77 File Offset: 0x00148F77
		protected void ShutdownCallback(object state)
		{
			base.Shutdown();
		}

		// Token: 0x06005A4C RID: 23116 RVA: 0x0014AD80 File Offset: 0x00148F80
		protected void StartReceiving(bool canBlock)
		{
			IAsyncResult asyncResult;
			for (;;)
			{
				asyncResult = this.Binder.BeginTryReceive(TimeSpan.MaxValue, ReliableInputSessionChannel.onReceiveCompleted, this);
				if (!asyncResult.CompletedSynchronously)
				{
					break;
				}
				if (!canBlock)
				{
					goto Block_1;
				}
				if (!this.HandleReceiveComplete(asyncResult))
				{
					return;
				}
			}
			return;
			Block_1:
			ActionItem.Schedule(ReliableInputSessionChannel.asyncReceiveComplete, asyncResult);
		}

		// Token: 0x06005A4D RID: 23117 RVA: 0x0014ADC8 File Offset: 0x00148FC8
		private void ThrowIfCloseInvalid()
		{
			bool flag = false;
			if (this.listener.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				if (this.DeliveryStrategy.EnqueuedCount > 0 || this.Connection.Ranges.Count > 1)
				{
					flag = true;
				}
			}
			else if (this.listener.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11 && this.DeliveryStrategy.EnqueuedCount > 0)
			{
				flag = true;
			}
			if (flag)
			{
				WsrmFault wsrmFault = SequenceTerminatedFault.CreateProtocolFault(this.session.InputID, SR.GetString("SequenceTerminatedSessionClosedBeforeDone"), SR.GetString("SessionClosedBeforeDone"));
				this.session.OnLocalFault(null, wsrmFault, null);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(wsrmFault.CreateException());
			}
		}

		// Token: 0x06005A4E RID: 23118 RVA: 0x0014AE77 File Offset: 0x00149077
		private void UnblockClose()
		{
			this.connection.Fault(this);
		}

		// Token: 0x04003684 RID: 13956
		private bool advertisedZero;

		// Token: 0x04003685 RID: 13957
		private IServerReliableChannelBinder binder;

		// Token: 0x04003686 RID: 13958
		private ReliableInputConnection connection;

		// Token: 0x04003687 RID: 13959
		private DeliveryStrategy<Message> deliveryStrategy;

		// Token: 0x04003688 RID: 13960
		private ReliableChannelListenerBase<IInputSessionChannel> listener;

		// Token: 0x04003689 RID: 13961
		private ServerReliableSession session;

		// Token: 0x0400368A RID: 13962
		protected string perfCounterId;

		// Token: 0x0400368B RID: 13963
		private static Action<object> asyncReceiveComplete = new Action<object>(ReliableInputSessionChannel.AsyncReceiveCompleteStatic);

		// Token: 0x0400368C RID: 13964
		private static AsyncCallback onReceiveCompleted = Fx.ThunkCallback(new AsyncCallback(ReliableInputSessionChannel.OnReceiveCompletedStatic));
	}
}
