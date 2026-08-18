using System;
using System.Runtime;
using System.ServiceModel.Diagnostics.Application;
using System.Threading;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200092A RID: 2346
	internal class ClientReliableDuplexSessionChannel : ReliableDuplexSessionChannel
	{
		// Token: 0x06005A09 RID: 23049 RVA: 0x00149DE0 File Offset: 0x00147FE0
		public ClientReliableDuplexSessionChannel(ChannelManagerBase factory, IReliableFactorySettings settings, IReliableChannelBinder binder, FaultHelper faultHelper, LateBoundChannelParameterCollection channelParameters, UniqueId inputID) : base(factory, settings, binder)
		{
			this.clientSession = new ClientReliableDuplexSessionChannel.DuplexClientReliableSession(this, settings, faultHelper, inputID);
			this.clientSession.PollingCallback = new ClientReliableSession.PollingHandler(this.PollingCallback);
			base.SetSession(this.clientSession);
			this.channelParameters = channelParameters;
			channelParameters.SetChannel(this);
			((IClientReliableChannelBinder)binder).ConnectionLost += this.OnConnectionLost;
		}

		// Token: 0x06005A0A RID: 23050 RVA: 0x00149E51 File Offset: 0x00148051
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(ChannelParameterCollection))
			{
				return (T)((object)this.channelParameters);
			}
			return base.GetProperty<T>();
		}

		// Token: 0x06005A0B RID: 23051 RVA: 0x00149E80 File Offset: 0x00148080
		private void HandleReconnectComplete(IAsyncResult result)
		{
			bool flag = true;
			try
			{
				base.Binder.EndSend(result);
				flag = false;
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (base.Binder.Connected)
					{
						this.clientSession.ResumePolling(base.OutputConnection.Strategy.QuotaRemaining == 0);
					}
					else
					{
						this.WaitForReconnect();
					}
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				if (!flag)
				{
					throw;
				}
				this.WaitForReconnect();
			}
		}

		// Token: 0x06005A0C RID: 23052 RVA: 0x00149F28 File Offset: 0x00148128
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.closeTimeoutHelper = new TimeoutHelper(timeout);
			this.closing = true;
			return base.OnBeginClose(timeout, callback, state);
		}

		// Token: 0x06005A0D RID: 23053 RVA: 0x00149F46 File Offset: 0x00148146
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ReliableChannelOpenAsyncResult(base.Binder, base.ReliableSession, timeout, callback, state);
		}

		// Token: 0x06005A0E RID: 23054 RVA: 0x00149F5C File Offset: 0x0014815C
		protected override void OnClose(TimeSpan timeout)
		{
			this.closeTimeoutHelper = new TimeoutHelper(timeout);
			this.closing = true;
			base.OnClose(timeout);
		}

		// Token: 0x06005A0F RID: 23055 RVA: 0x00149F78 File Offset: 0x00148178
		private void OnConnectionLost(object sender, EventArgs args)
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if ((base.State == CommunicationState.Opened || base.State == CommunicationState.Closing) && !base.Binder.Connected && this.clientSession.StopPolling())
				{
					if (TD.ClientReliableSessionReconnectIsEnabled())
					{
						TD.ClientReliableSessionReconnect(this.clientSession.Id);
					}
					this.Reconnect();
				}
			}
		}

		// Token: 0x06005A10 RID: 23056 RVA: 0x00149FFC File Offset: 0x001481FC
		protected override void OnEndOpen(IAsyncResult result)
		{
			ReliableChannelOpenAsyncResult.End(result);
		}

		// Token: 0x06005A11 RID: 23057 RVA: 0x0014A004 File Offset: 0x00148204
		protected override void OnOpen(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			bool flag = true;
			try
			{
				base.Binder.Open(timeoutHelper.RemainingTime());
				base.ReliableSession.Open(timeoutHelper.RemainingTime());
				flag = false;
			}
			finally
			{
				if (flag)
				{
					base.Binder.Close(timeoutHelper.RemainingTime());
				}
			}
		}

		// Token: 0x06005A12 RID: 23058 RVA: 0x0014A06C File Offset: 0x0014826C
		protected override void OnOpened()
		{
			base.OnOpened();
			base.SetConnections();
			if (Thread.CurrentThread.IsThreadPoolThread)
			{
				try
				{
					base.StartReceiving(false);
					return;
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					base.ReliableSession.OnUnknownException(ex);
					return;
				}
			}
			ActionItem.Schedule(new Action<object>(ClientReliableDuplexSessionChannel.StartReceivingStatic), this);
		}

		// Token: 0x06005A13 RID: 23059 RVA: 0x0014A0D8 File Offset: 0x001482D8
		private static void OnReconnectComplete(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			ClientReliableDuplexSessionChannel clientReliableDuplexSessionChannel = (ClientReliableDuplexSessionChannel)result.AsyncState;
			clientReliableDuplexSessionChannel.HandleReconnectComplete(result);
		}

		// Token: 0x06005A14 RID: 23060 RVA: 0x0014A104 File Offset: 0x00148304
		private static void OnReconnectTimerElapsed(object state)
		{
			ClientReliableDuplexSessionChannel clientReliableDuplexSessionChannel = (ClientReliableDuplexSessionChannel)state;
			object thisLock = clientReliableDuplexSessionChannel.ThisLock;
			lock (thisLock)
			{
				if ((clientReliableDuplexSessionChannel.State == CommunicationState.Opened || clientReliableDuplexSessionChannel.State == CommunicationState.Closing) && !clientReliableDuplexSessionChannel.Binder.Connected)
				{
					clientReliableDuplexSessionChannel.Reconnect();
				}
				else
				{
					clientReliableDuplexSessionChannel.clientSession.ResumePolling(clientReliableDuplexSessionChannel.OutputConnection.Strategy.QuotaRemaining == 0);
				}
			}
		}

		// Token: 0x06005A15 RID: 23061 RVA: 0x0014A18C File Offset: 0x0014838C
		protected override void OnRemoteActivity()
		{
			base.ReliableSession.OnRemoteActivity(base.OutputConnection.Strategy.QuotaRemaining == 0);
		}

		// Token: 0x06005A16 RID: 23062 RVA: 0x0014A1AC File Offset: 0x001483AC
		private void PollingCallback()
		{
			using (Message message = WsrmUtilities.CreateAckRequestedMessage(base.Settings.MessageVersion, base.Settings.ReliableMessagingVersion, base.ReliableSession.OutputID))
			{
				base.Binder.Send(message, base.DefaultSendTimeout);
			}
		}

		// Token: 0x06005A17 RID: 23063 RVA: 0x0014A210 File Offset: 0x00148410
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
			base.ProcessDuplexMessage(info);
		}

		// Token: 0x06005A18 RID: 23064 RVA: 0x0014A23C File Offset: 0x0014843C
		private static void StartReceivingStatic(object state)
		{
			ClientReliableDuplexSessionChannel clientReliableDuplexSessionChannel = (ClientReliableDuplexSessionChannel)state;
			try
			{
				clientReliableDuplexSessionChannel.StartReceiving(true);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				clientReliableDuplexSessionChannel.ReliableSession.OnUnknownException(ex);
			}
		}

		// Token: 0x06005A19 RID: 23065 RVA: 0x0014A284 File Offset: 0x00148484
		private void Reconnect()
		{
			bool flag = true;
			try
			{
				Message message = WsrmUtilities.CreateAckRequestedMessage(base.Settings.MessageVersion, base.Settings.ReliableMessagingVersion, base.ReliableSession.OutputID);
				TimeSpan timeout = this.closing ? this.closeTimeoutHelper.RemainingTime() : this.DefaultCloseTimeout;
				IAsyncResult asyncResult = base.Binder.BeginSend(message, timeout, ClientReliableDuplexSessionChannel.onReconnectComplete, this);
				flag = false;
				if (asyncResult.CompletedSynchronously)
				{
					this.HandleReconnectComplete(asyncResult);
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				if (!flag)
				{
					throw;
				}
				this.WaitForReconnect();
			}
		}

		// Token: 0x06005A1A RID: 23066 RVA: 0x0014A32C File Offset: 0x0014852C
		private void WaitForReconnect()
		{
			TimeSpan timeFromNow;
			if (this.closing)
			{
				timeFromNow = TimeoutHelper.Divide(this.closeTimeoutHelper.RemainingTime(), 2);
			}
			else
			{
				timeFromNow = TimeoutHelper.Divide(base.DefaultSendTimeout, 2);
			}
			IOThreadTimer iothreadTimer = new IOThreadTimer(ClientReliableDuplexSessionChannel.onReconnectTimerElapsed, this, false);
			iothreadTimer.Set(timeFromNow);
		}

		// Token: 0x0400367C RID: 13948
		private ChannelParameterCollection channelParameters;

		// Token: 0x0400367D RID: 13949
		private ClientReliableDuplexSessionChannel.DuplexClientReliableSession clientSession;

		// Token: 0x0400367E RID: 13950
		private TimeoutHelper closeTimeoutHelper;

		// Token: 0x0400367F RID: 13951
		private bool closing;

		// Token: 0x04003680 RID: 13952
		private static AsyncCallback onReconnectComplete = Fx.ThunkCallback(new AsyncCallback(ClientReliableDuplexSessionChannel.OnReconnectComplete));

		// Token: 0x04003681 RID: 13953
		private static Action<object> onReconnectTimerElapsed = new Action<object>(ClientReliableDuplexSessionChannel.OnReconnectTimerElapsed);

		// Token: 0x02000DC6 RID: 3526
		private class DuplexClientReliableSession : ClientReliableSession, IDuplexSession, IInputSession, ISession, IOutputSession
		{
			// Token: 0x06007FF0 RID: 32752 RVA: 0x001DBF6C File Offset: 0x001DA16C
			public DuplexClientReliableSession(ClientReliableDuplexSessionChannel channel, IReliableFactorySettings settings, FaultHelper helper, UniqueId inputID) : base(channel, settings, (IClientReliableChannelBinder)channel.Binder, helper, inputID)
			{
				this.channel = channel;
			}

			// Token: 0x06007FF1 RID: 32753 RVA: 0x001DBF8B File Offset: 0x001DA18B
			public IAsyncResult BeginCloseOutputSession(AsyncCallback callback, object state)
			{
				return this.BeginCloseOutputSession(this.channel.DefaultCloseTimeout, callback, state);
			}

			// Token: 0x06007FF2 RID: 32754 RVA: 0x001DBFA0 File Offset: 0x001DA1A0
			public IAsyncResult BeginCloseOutputSession(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.channel.OnBeginCloseOutputSession(timeout, callback, state);
			}

			// Token: 0x06007FF3 RID: 32755 RVA: 0x001DBFB0 File Offset: 0x001DA1B0
			public void EndCloseOutputSession(IAsyncResult result)
			{
				this.channel.OnEndCloseOutputSession(result);
			}

			// Token: 0x06007FF4 RID: 32756 RVA: 0x001DBFBE File Offset: 0x001DA1BE
			public void CloseOutputSession()
			{
				this.CloseOutputSession(this.channel.DefaultCloseTimeout);
			}

			// Token: 0x06007FF5 RID: 32757 RVA: 0x001DBFD1 File Offset: 0x001DA1D1
			public void CloseOutputSession(TimeSpan timeout)
			{
				this.channel.OnCloseOutputSession(timeout);
			}

			// Token: 0x04004922 RID: 18722
			private ClientReliableDuplexSessionChannel channel;
		}
	}
}
