using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime;
using System.Threading;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200091A RID: 2330
	internal abstract class ReliableChannelListener<TChannel, TReliableChannel, TInnerChannel> : ReliableChannelListenerBase<TChannel> where TChannel : class, IChannel where TReliableChannel : class, IChannel where TInnerChannel : class, IChannel
	{
		// Token: 0x06005952 RID: 22866 RVA: 0x001468FC File Offset: 0x00144AFC
		protected ReliableChannelListener(ReliableSessionBindingElement binding, BindingContext context) : base(binding, context.Binding)
		{
			this.typedListener = context.BuildInnerChannelListener<TInnerChannel>();
			this.inputQueueChannelAcceptor = new InputQueueChannelAcceptor<TChannel>(this);
			base.Acceptor = this.inputQueueChannelAcceptor;
		}

		// Token: 0x170015CC RID: 5580
		// (get) Token: 0x06005953 RID: 22867 RVA: 0x0014692F File Offset: 0x00144B2F
		// (set) Token: 0x06005954 RID: 22868 RVA: 0x00146937 File Offset: 0x00144B37
		internal override IChannelListener InnerChannelListener
		{
			get
			{
				return this.typedListener;
			}
			set
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException());
			}
		}

		// Token: 0x06005955 RID: 22869 RVA: 0x00146948 File Offset: 0x00144B48
		private IServerReliableChannelBinder CreateBinder(TInnerChannel channel, EndpointAddress localAddress, EndpointAddress remoteAddress)
		{
			return ServerReliableChannelBinder<TInnerChannel>.CreateBinder(channel, localAddress, remoteAddress, TolerateFaultsMode.IfNotSecuritySession, this.DefaultCloseTimeout, this.DefaultSendTimeout);
		}

		// Token: 0x06005956 RID: 22870
		protected abstract TReliableChannel CreateChannel(UniqueId id, CreateSequenceInfo createSequenceInfo, IServerReliableChannelBinder binder);

		// Token: 0x06005957 RID: 22871 RVA: 0x0014695F File Offset: 0x00144B5F
		protected void Dispatch()
		{
			this.inputQueueChannelAcceptor.Dispatch();
		}

		// Token: 0x06005958 RID: 22872 RVA: 0x0014696C File Offset: 0x00144B6C
		protected virtual void OnInnerChannelAccepted(TInnerChannel channel)
		{
		}

		// Token: 0x06005959 RID: 22873 RVA: 0x0014696E File Offset: 0x00144B6E
		protected bool EnqueueWithoutDispatch(TChannel channel)
		{
			return this.inputQueueChannelAcceptor.EnqueueWithoutDispatch(channel, null);
		}

		// Token: 0x0600595A RID: 22874 RVA: 0x00146980 File Offset: 0x00144B80
		protected TReliableChannel GetChannel(WsrmMessageInfo info, out UniqueId id)
		{
			id = WsrmUtilities.GetInputId(info);
			object thisLock = base.ThisLock;
			TReliableChannel result;
			lock (thisLock)
			{
				TReliableChannel treliableChannel = default(TReliableChannel);
				if ((id == null || !this.channelsByInput.TryGetValue(id, out treliableChannel)) && this.Duplex)
				{
					UniqueId outputId = WsrmUtilities.GetOutputId(base.ReliableMessagingVersion, info);
					if (outputId != null)
					{
						id = outputId;
						this.channelsByOutput.TryGetValue(id, out treliableChannel);
					}
				}
				result = treliableChannel;
			}
			return result;
		}

		// Token: 0x0600595B RID: 22875 RVA: 0x00146A1C File Offset: 0x00144C1C
		private void HandleAcceptComplete(TInnerChannel channel)
		{
			if (channel == null)
			{
				return;
			}
			try
			{
				this.OnInnerChannelAccepted(channel);
				channel.Open();
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Error);
				channel.Abort();
				return;
			}
			this.ProcessChannel(channel);
		}

		// Token: 0x0600595C RID: 22876 RVA: 0x00146A80 File Offset: 0x00144C80
		protected bool HandleException(Exception e, ICommunicationObject o)
		{
			if ((e is CommunicationException || e is TimeoutException) && o.State == CommunicationState.Opened)
			{
				DiagnosticUtility.TraceHandledException(e, TraceEventType.Warning);
				return true;
			}
			DiagnosticUtility.TraceHandledException(e, TraceEventType.Error);
			return false;
		}

		// Token: 0x0600595D RID: 22877 RVA: 0x00146AAC File Offset: 0x00144CAC
		protected override bool HasChannels()
		{
			return this.channelsByInput != null && this.channelsByInput.Count > 0;
		}

		// Token: 0x0600595E RID: 22878 RVA: 0x00146AC6 File Offset: 0x00144CC6
		private bool IsExpectedException(Exception e)
		{
			return !(e is ProtocolException) && e is CommunicationException;
		}

		// Token: 0x0600595F RID: 22879 RVA: 0x00146ADB File Offset: 0x00144CDB
		protected override bool IsLastChannel(UniqueId inputId)
		{
			return this.channelsByInput.Count == 1 && this.channelsByInput.ContainsKey(inputId);
		}

		// Token: 0x06005960 RID: 22880 RVA: 0x00146AFC File Offset: 0x00144CFC
		private void OnAcceptCompleted(IAsyncResult result)
		{
			TInnerChannel tinnerChannel = default(TInnerChannel);
			Exception ex = null;
			Exception ex2 = null;
			try
			{
				tinnerChannel = this.typedListener.EndAcceptChannel(result);
			}
			catch (Exception ex3)
			{
				if (Fx.IsFatal(ex3))
				{
					throw;
				}
				if (this.IsExpectedException(ex3))
				{
					ex = ex3;
				}
				else
				{
					ex2 = ex3;
				}
			}
			if (tinnerChannel != null)
			{
				this.HandleAcceptComplete(tinnerChannel);
				this.StartAccepting();
				return;
			}
			if (ex2 != null)
			{
				base.Fault(ex2);
				return;
			}
			if (ex != null && this.typedListener.State == CommunicationState.Opened)
			{
				DiagnosticUtility.TraceHandledException(ex, TraceEventType.Warning);
				this.StartAccepting();
				return;
			}
			if (this.typedListener.State == CommunicationState.Faulted)
			{
				base.Fault(ex);
			}
		}

		// Token: 0x06005961 RID: 22881 RVA: 0x00146BA8 File Offset: 0x00144DA8
		private static void OnAcceptCompletedStatic(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				ReliableChannelListener<TChannel, TReliableChannel, TInnerChannel> reliableChannelListener = (ReliableChannelListener<TChannel, TReliableChannel, TInnerChannel>)result.AsyncState;
				try
				{
					reliableChannelListener.OnAcceptCompleted(result);
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					reliableChannelListener.Fault(exception);
				}
			}
		}

		// Token: 0x06005962 RID: 22882 RVA: 0x00146BF8 File Offset: 0x00144DF8
		protected override void OnFaulted()
		{
			this.typedListener.Abort();
			this.inputQueueChannelAcceptor.FaultQueue();
			base.OnFaulted();
		}

		// Token: 0x06005963 RID: 22883 RVA: 0x00146C18 File Offset: 0x00144E18
		protected override void OnOpened()
		{
			base.OnOpened();
			this.channelsByInput = new Dictionary<UniqueId, TReliableChannel>();
			if (this.Duplex)
			{
				this.channelsByOutput = new Dictionary<UniqueId, TReliableChannel>();
			}
			if (Thread.CurrentThread.IsThreadPoolThread)
			{
				try
				{
					this.StartAccepting();
					return;
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					base.Fault(exception);
					return;
				}
			}
			ActionItem.Schedule(new Action<object>(ReliableChannelListener<TChannel, TReliableChannel, TInnerChannel>.StartAccepting), this);
		}

		// Token: 0x06005964 RID: 22884 RVA: 0x00146C94 File Offset: 0x00144E94
		protected TReliableChannel ProcessCreateSequence(WsrmMessageInfo info, TInnerChannel channel, out bool dispatch, out bool newChannel)
		{
			dispatch = false;
			newChannel = false;
			CreateSequenceInfo createSequenceInfo = info.CreateSequenceInfo;
			EndpointAddress localAddress;
			TReliableChannel result;
			if (!WsrmUtilities.ValidateCreateSequence<TChannel>(info, this, channel, out localAddress))
			{
				result = default(TReliableChannel);
				return result;
			}
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				TReliableChannel treliableChannel = default(TReliableChannel);
				if (createSequenceInfo.OfferIdentifier != null && this.Duplex && this.channelsByOutput.TryGetValue(createSequenceInfo.OfferIdentifier, out treliableChannel))
				{
					result = treliableChannel;
				}
				else if (!base.IsAccepting)
				{
					info.FaultReply = WsrmUtilities.CreateEndpointNotFoundFault(base.MessageVersion, SR.GetString("RMEndpointNotFoundReason", new object[]
					{
						this.Uri
					}));
					result = default(TReliableChannel);
				}
				else if (this.inputQueueChannelAcceptor.PendingCount >= base.MaxPendingChannels)
				{
					info.FaultReply = WsrmUtilities.CreateCSRefusedServerTooBusyFault(base.MessageVersion, base.ReliableMessagingVersion, SR.GetString("ServerTooBusy", new object[]
					{
						this.Uri
					}));
					result = default(TReliableChannel);
				}
				else
				{
					UniqueId uniqueId = WsrmUtilities.NextSequenceId();
					treliableChannel = this.CreateChannel(uniqueId, createSequenceInfo, this.CreateBinder(channel, localAddress, createSequenceInfo.ReplyTo));
					this.channelsByInput.Add(uniqueId, treliableChannel);
					if (this.Duplex)
					{
						this.channelsByOutput.Add(createSequenceInfo.OfferIdentifier, treliableChannel);
					}
					dispatch = this.EnqueueWithoutDispatch((TChannel)((object)treliableChannel));
					newChannel = true;
					result = treliableChannel;
				}
			}
			return result;
		}

		// Token: 0x06005965 RID: 22885
		protected abstract void ProcessChannel(TInnerChannel channel);

		// Token: 0x06005966 RID: 22886 RVA: 0x00146E38 File Offset: 0x00145038
		protected override void RemoveChannel(UniqueId inputId, UniqueId outputId)
		{
			this.channelsByInput.Remove(inputId);
			if (this.Duplex)
			{
				this.channelsByOutput.Remove(outputId);
			}
		}

		// Token: 0x06005967 RID: 22887 RVA: 0x00146E5C File Offset: 0x0014505C
		private void StartAccepting()
		{
			Exception exception = null;
			Exception ex = null;
			while (this.typedListener.State == CommunicationState.Opened)
			{
				TInnerChannel tinnerChannel = default(TInnerChannel);
				exception = null;
				ex = null;
				try
				{
					IAsyncResult asyncResult = this.typedListener.BeginAcceptChannel(TimeSpan.MaxValue, ReliableChannelListener<TChannel, TReliableChannel, TInnerChannel>.onAcceptCompleted, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					tinnerChannel = this.typedListener.EndAcceptChannel(asyncResult);
					if (tinnerChannel == null)
					{
						break;
					}
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					if (this.IsExpectedException(ex2))
					{
						DiagnosticUtility.TraceHandledException(ex2, TraceEventType.Warning);
						exception = ex2;
						continue;
					}
					ex = ex2;
					break;
				}
				this.HandleAcceptComplete(tinnerChannel);
			}
			if (ex != null)
			{
				base.Fault(ex);
				return;
			}
			if (this.typedListener.State == CommunicationState.Faulted)
			{
				base.Fault(exception);
				return;
			}
		}

		// Token: 0x06005968 RID: 22888 RVA: 0x00146F28 File Offset: 0x00145128
		private static void StartAccepting(object state)
		{
			ReliableChannelListener<TChannel, TReliableChannel, TInnerChannel> reliableChannelListener = (ReliableChannelListener<TChannel, TReliableChannel, TInnerChannel>)state;
			try
			{
				reliableChannelListener.StartAccepting();
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				reliableChannelListener.Fault(exception);
			}
		}

		// Token: 0x04003660 RID: 13920
		private Dictionary<UniqueId, TReliableChannel> channelsByInput;

		// Token: 0x04003661 RID: 13921
		private Dictionary<UniqueId, TReliableChannel> channelsByOutput;

		// Token: 0x04003662 RID: 13922
		private InputQueueChannelAcceptor<TChannel> inputQueueChannelAcceptor;

		// Token: 0x04003663 RID: 13923
		private static AsyncCallback onAcceptCompleted = Fx.ThunkCallback(new AsyncCallback(ReliableChannelListener<TChannel, TReliableChannel, TInnerChannel>.OnAcceptCompletedStatic));

		// Token: 0x04003664 RID: 13924
		private IChannelListener<TInnerChannel> typedListener;
	}
}
