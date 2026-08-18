using System;
using System.Runtime;
using System.ServiceModel.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200092B RID: 2347
	internal sealed class ServerReliableDuplexSessionChannel : ReliableDuplexSessionChannel
	{
		// Token: 0x06005A1C RID: 23068 RVA: 0x0014A3A0 File Offset: 0x001485A0
		public ServerReliableDuplexSessionChannel(ReliableChannelListenerBase<IDuplexSessionChannel> listener, IReliableChannelBinder binder, FaultHelper faultHelper, UniqueId inputID, UniqueId outputID) : base(listener, listener, binder)
		{
			this.listener = listener;
			ServerReliableDuplexSessionChannel.DuplexServerReliableSession duplexServerReliableSession = new ServerReliableDuplexSessionChannel.DuplexServerReliableSession(this, listener, faultHelper, inputID, outputID);
			base.SetSession(duplexServerReliableSession);
			duplexServerReliableSession.Open(TimeSpan.Zero);
			base.SetConnections();
			if (PerformanceCounters.PerformanceCountersEnabled)
			{
				this.perfCounterId = this.listener.Uri.ToString().ToUpperInvariant();
			}
			if (binder.HasSession)
			{
				try
				{
					base.StartReceiving(false);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					base.ReliableSession.OnUnknownException(ex);
				}
			}
		}

		// Token: 0x06005A1D RID: 23069 RVA: 0x0014A440 File Offset: 0x00148640
		private IAsyncResult BeginUnregisterChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.listener.OnReliableChannelBeginClose(base.ReliableSession.InputID, base.ReliableSession.OutputID, timeout, callback, state);
		}

		// Token: 0x06005A1E RID: 23070 RVA: 0x0014A466 File Offset: 0x00148666
		private void EndUnregisterChannel(IAsyncResult result)
		{
			this.listener.OnReliableChannelEndClose(result);
		}

		// Token: 0x06005A1F RID: 23071 RVA: 0x0014A474 File Offset: 0x00148674
		protected override void OnAbort()
		{
			base.OnAbort();
			this.listener.OnReliableChannelAbort(base.ReliableSession.InputID, base.ReliableSession.OutputID);
		}

		// Token: 0x06005A20 RID: 23072 RVA: 0x0014A4A0 File Offset: 0x001486A0
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			OperationWithTimeoutBeginCallback[] beginOperations = new OperationWithTimeoutBeginCallback[]
			{
				new OperationWithTimeoutBeginCallback(base.OnBeginClose),
				new OperationWithTimeoutBeginCallback(this.BeginUnregisterChannel)
			};
			OperationEndCallback[] endOperations = new OperationEndCallback[]
			{
				new OperationEndCallback(base.OnEndClose),
				new OperationEndCallback(this.EndUnregisterChannel)
			};
			return OperationWithTimeoutComposer.BeginComposeAsyncOperations(timeout, beginOperations, endOperations, callback, state);
		}

		// Token: 0x06005A21 RID: 23073 RVA: 0x0014A504 File Offset: 0x00148704
		protected override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.OnClose(timeoutHelper.RemainingTime());
			this.listener.OnReliableChannelClose(base.ReliableSession.InputID, base.ReliableSession.OutputID, timeoutHelper.RemainingTime());
		}

		// Token: 0x06005A22 RID: 23074 RVA: 0x0014A54E File Offset: 0x0014874E
		protected override void OnEndClose(IAsyncResult result)
		{
			OperationWithTimeoutComposer.EndComposeAsyncOperations(result);
		}

		// Token: 0x06005A23 RID: 23075 RVA: 0x0014A556 File Offset: 0x00148756
		protected override void OnOpen(TimeSpan timeout)
		{
		}

		// Token: 0x06005A24 RID: 23076 RVA: 0x0014A558 File Offset: 0x00148758
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06005A25 RID: 23077 RVA: 0x0014A561 File Offset: 0x00148761
		protected override void OnEndOpen(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x06005A26 RID: 23078 RVA: 0x0014A569 File Offset: 0x00148769
		protected override void OnFaulted()
		{
			base.OnFaulted();
			if (PerformanceCounters.PerformanceCountersEnabled)
			{
				PerformanceCounters.SessionFaulted(this.perfCounterId);
			}
		}

		// Token: 0x06005A27 RID: 23079 RVA: 0x0014A583 File Offset: 0x00148783
		protected override void OnMessageDropped()
		{
			if (PerformanceCounters.PerformanceCountersEnabled)
			{
				PerformanceCounters.MessageDropped(this.perfCounterId);
			}
		}

		// Token: 0x06005A28 RID: 23080 RVA: 0x0014A598 File Offset: 0x00148798
		public void ProcessDemuxedMessage(WsrmMessageInfo info)
		{
			try
			{
				this.ProcessMessage(info);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				base.ReliableSession.OnUnknownException(ex);
			}
		}

		// Token: 0x06005A29 RID: 23081 RVA: 0x0014A5D8 File Offset: 0x001487D8
		protected override void ProcessMessage(WsrmMessageInfo info)
		{
			if (!base.ReliableSession.ProcessInfo(info, null))
			{
				return;
			}
			if (!base.ReliableSession.VerifyDuplexProtocolElements(info, null))
			{
				return;
			}
			if (info.CreateSequenceInfo != null)
			{
				EndpointAddress acceptAcksTo;
				if (WsrmUtilities.ValidateCreateSequence<IDuplexSessionChannel>(info, this.listener, base.Binder.Channel, out acceptAcksTo))
				{
					Message message = WsrmUtilities.CreateCreateSequenceResponse(base.Settings.MessageVersion, base.Settings.ReliableMessagingVersion, true, info.CreateSequenceInfo, base.Settings.Ordered, base.ReliableSession.InputID, acceptAcksTo);
					using (info.Message)
					{
						using (message)
						{
							if (((IServerReliableChannelBinder)base.Binder).AddressResponse(info.Message, message))
							{
								base.Binder.Send(message, base.DefaultSendTimeout);
							}
							return;
						}
					}
				}
				base.ReliableSession.OnLocalFault(info.FaultException, info.FaultReply, null);
				return;
			}
			base.ProcessDuplexMessage(info);
		}

		// Token: 0x04003682 RID: 13954
		private ReliableChannelListenerBase<IDuplexSessionChannel> listener;

		// Token: 0x04003683 RID: 13955
		private string perfCounterId;

		// Token: 0x02000DC7 RID: 3527
		private class DuplexServerReliableSession : ServerReliableSession, IDuplexSession, IInputSession, ISession, IOutputSession
		{
			// Token: 0x06007FF6 RID: 32758 RVA: 0x001DBFDF File Offset: 0x001DA1DF
			public DuplexServerReliableSession(ServerReliableDuplexSessionChannel channel, ReliableChannelListenerBase<IDuplexSessionChannel> listener, FaultHelper faultHelper, UniqueId inputID, UniqueId outputID) : base(channel, listener, (IServerReliableChannelBinder)channel.Binder, faultHelper, inputID, outputID)
			{
				this.channel = channel;
			}

			// Token: 0x06007FF7 RID: 32759 RVA: 0x001DC000 File Offset: 0x001DA200
			public IAsyncResult BeginCloseOutputSession(AsyncCallback callback, object state)
			{
				return this.BeginCloseOutputSession(this.channel.DefaultCloseTimeout, callback, state);
			}

			// Token: 0x06007FF8 RID: 32760 RVA: 0x001DC015 File Offset: 0x001DA215
			public IAsyncResult BeginCloseOutputSession(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.channel.OnBeginCloseOutputSession(timeout, callback, state);
			}

			// Token: 0x06007FF9 RID: 32761 RVA: 0x001DC025 File Offset: 0x001DA225
			public void EndCloseOutputSession(IAsyncResult result)
			{
				this.channel.OnEndCloseOutputSession(result);
			}

			// Token: 0x06007FFA RID: 32762 RVA: 0x001DC033 File Offset: 0x001DA233
			public void CloseOutputSession()
			{
				this.CloseOutputSession(this.channel.DefaultCloseTimeout);
			}

			// Token: 0x06007FFB RID: 32763 RVA: 0x001DC046 File Offset: 0x001DA246
			public void CloseOutputSession(TimeSpan timeout)
			{
				this.channel.OnCloseOutputSession(timeout);
			}

			// Token: 0x04004923 RID: 18723
			private ServerReliableDuplexSessionChannel channel;
		}
	}
}
