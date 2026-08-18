using System;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.Security;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Principal;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Security;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007E8 RID: 2024
	internal class ClientFramingDuplexSessionChannel : FramingDuplexSessionChannel
	{
		// Token: 0x06004C95 RID: 19605 RVA: 0x0011751C File Offset: 0x0011571C
		public ClientFramingDuplexSessionChannel(ChannelManagerBase factory, IConnectionOrientedTransportChannelFactorySettings settings, EndpointAddress remoteAddresss, Uri via, IConnectionInitiator connectionInitiator, ConnectionPool connectionPool, bool exposeConnectionProperty, bool flowIdentity) : base(factory, settings, remoteAddresss, via, exposeConnectionProperty)
		{
			this.settings = settings;
			base.MessageEncoder = settings.MessageEncoderFactory.CreateSessionEncoder();
			this.upgrade = settings.Upgrade;
			this.flowIdentity = flowIdentity;
			this.connectionPoolHelper = new ClientFramingDuplexSessionChannel.DuplexConnectionPoolHelper(this, connectionPool, connectionInitiator);
		}

		// Token: 0x06004C96 RID: 19606 RVA: 0x00117574 File Offset: 0x00115774
		private ArraySegment<byte> CreatePreamble()
		{
			EncodedVia via = new EncodedVia(this.Via.AbsoluteUri);
			EncodedContentType contentType = EncodedContentType.Create(base.MessageEncoder.ContentType);
			int num = ClientDuplexEncoder.ModeBytes.Length + SessionEncoder.CalcStartSize(via, contentType);
			int num2 = 0;
			if (this.upgrade == null)
			{
				num2 = num;
				num += SessionEncoder.PreambleEndBytes.Length;
			}
			byte[] array = DiagnosticUtility.Utility.AllocateByteArray(num);
			Buffer.BlockCopy(ClientDuplexEncoder.ModeBytes, 0, array, 0, ClientDuplexEncoder.ModeBytes.Length);
			SessionEncoder.EncodeStart(array, ClientDuplexEncoder.ModeBytes.Length, via, contentType);
			if (num2 > 0)
			{
				Buffer.BlockCopy(SessionEncoder.PreambleEndBytes, 0, array, num2, SessionEncoder.PreambleEndBytes.Length);
			}
			return new ArraySegment<byte>(array, 0, num);
		}

		// Token: 0x06004C97 RID: 19607 RVA: 0x0011761D File Offset: 0x0011581D
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ClientFramingDuplexSessionChannel.OpenAsyncResult(this, timeout, callback, state);
		}

		// Token: 0x06004C98 RID: 19608 RVA: 0x00117628 File Offset: 0x00115828
		protected override void OnEndOpen(IAsyncResult result)
		{
			ClientFramingDuplexSessionChannel.OpenAsyncResult.End(result);
		}

		// Token: 0x06004C99 RID: 19609 RVA: 0x00117630 File Offset: 0x00115830
		public override T GetProperty<T>()
		{
			T property = base.GetProperty<T>();
			if (property == null && this.upgrade != null)
			{
				property = this.upgrade.GetProperty<T>();
			}
			return property;
		}

		// Token: 0x06004C9A RID: 19610 RVA: 0x00117664 File Offset: 0x00115864
		private IConnection SendPreamble(IConnection connection, ArraySegment<byte> preamble, ref TimeoutHelper timeoutHelper)
		{
			if (TD.ClientSendPreambleStartIsEnabled())
			{
				TD.ClientSendPreambleStart(base.EventTraceActivity);
			}
			this.decoder = new ClientDuplexDecoder(0L);
			byte[] array = new byte[1];
			connection.Write(preamble.Array, preamble.Offset, preamble.Count, true, timeoutHelper.RemainingTime());
			if (this.upgrade != null)
			{
				IStreamUpgradeChannelBindingProvider property = this.upgrade.GetProperty<IStreamUpgradeChannelBindingProvider>();
				StreamUpgradeInitiator streamUpgradeInitiator = this.upgrade.CreateUpgradeInitiator(this.RemoteAddress, this.Via);
				streamUpgradeInitiator.Open(timeoutHelper.RemainingTime());
				if (!ConnectionUpgradeHelper.InitiateUpgrade(streamUpgradeInitiator, ref connection, this.decoder, this, ref timeoutHelper))
				{
					ConnectionUpgradeHelper.DecodeFramingFault(this.decoder, connection, this.Via, base.MessageEncoder.ContentType, ref timeoutHelper);
				}
				if (property != null && property.IsChannelBindingSupportEnabled)
				{
					base.SetChannelBinding(property.GetChannelBinding(streamUpgradeInitiator, ChannelBindingKind.Endpoint));
				}
				this.SetRemoteSecurity(streamUpgradeInitiator);
				streamUpgradeInitiator.Close(timeoutHelper.RemainingTime());
				connection.Write(SessionEncoder.PreambleEndBytes, 0, SessionEncoder.PreambleEndBytes.Length, true, timeoutHelper.RemainingTime());
			}
			int count = connection.Read(array, 0, array.Length, timeoutHelper.RemainingTime());
			if (!ConnectionUpgradeHelper.ValidatePreambleResponse(array, count, this.decoder, this.Via))
			{
				ConnectionUpgradeHelper.DecodeFramingFault(this.decoder, connection, this.Via, base.MessageEncoder.ContentType, ref timeoutHelper);
			}
			if (TD.ClientSendPreambleStopIsEnabled())
			{
				TD.ClientSendPreambleStop(base.EventTraceActivity);
			}
			return connection;
		}

		// Token: 0x06004C9B RID: 19611 RVA: 0x001177C3 File Offset: 0x001159C3
		private IAsyncResult BeginSendPreamble(IConnection connection, ArraySegment<byte> preamble, ref TimeoutHelper timeoutHelper, AsyncCallback callback, object state)
		{
			return new ClientFramingDuplexSessionChannel.SendPreambleAsyncResult(this, connection, preamble, this.flowIdentity, ref timeoutHelper, callback, state);
		}

		// Token: 0x06004C9C RID: 19612 RVA: 0x001177D8 File Offset: 0x001159D8
		private IConnection EndSendPreamble(IAsyncResult result)
		{
			return ClientFramingDuplexSessionChannel.SendPreambleAsyncResult.End(result);
		}

		// Token: 0x06004C9D RID: 19613 RVA: 0x001177E0 File Offset: 0x001159E0
		protected override void OnOpen(TimeSpan timeout)
		{
			IConnection connection;
			try
			{
				connection = this.connectionPoolHelper.EstablishConnection(timeout);
			}
			catch (TimeoutException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException(SR.GetString("TimeoutOnOpen", new object[]
				{
					timeout
				}), innerException));
			}
			bool flag = false;
			try
			{
				this.AcceptConnection(connection);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					this.connectionPoolHelper.Abort();
				}
			}
		}

		// Token: 0x06004C9E RID: 19614 RVA: 0x00117860 File Offset: 0x00115A60
		protected override void ReturnConnectionIfNecessary(bool abort, TimeSpan timeout)
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (abort)
				{
					this.connectionPoolHelper.Abort();
				}
				else
				{
					this.connectionPoolHelper.Close(timeout);
				}
			}
		}

		// Token: 0x06004C9F RID: 19615 RVA: 0x001178B8 File Offset: 0x00115AB8
		private void AcceptConnection(IConnection connection)
		{
			base.SetMessageSource(new ClientDuplexConnectionReader(this, connection, this.decoder, this.settings, base.MessageEncoder));
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (base.State != CommunicationState.Opening)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationObjectAbortedException(SR.GetString("DuplexChannelAbortedDuringOpen", new object[]
					{
						this.Via
					})));
				}
				base.Connection = connection;
			}
		}

		// Token: 0x06004CA0 RID: 19616 RVA: 0x0011794C File Offset: 0x00115B4C
		private void SetRemoteSecurity(StreamUpgradeInitiator upgradeInitiator)
		{
			base.RemoteSecurity = StreamSecurityUpgradeInitiator.GetRemoteSecurity(upgradeInitiator);
		}

		// Token: 0x06004CA1 RID: 19617 RVA: 0x0011795A File Offset: 0x00115B5A
		protected override void PrepareMessage(Message message)
		{
			base.PrepareMessage(message);
			if (base.RemoteSecurity != null)
			{
				message.Properties.Security = (SecurityMessageProperty)base.RemoteSecurity.CreateCopy();
			}
		}

		// Token: 0x04002FB8 RID: 12216
		private IConnectionOrientedTransportChannelFactorySettings settings;

		// Token: 0x04002FB9 RID: 12217
		private ClientDuplexDecoder decoder;

		// Token: 0x04002FBA RID: 12218
		private StreamUpgradeProvider upgrade;

		// Token: 0x04002FBB RID: 12219
		private ConnectionPoolHelper connectionPoolHelper;

		// Token: 0x04002FBC RID: 12220
		private bool flowIdentity;

		// Token: 0x02000D09 RID: 3337
		private class DuplexConnectionPoolHelper : ConnectionPoolHelper
		{
			// Token: 0x06007B06 RID: 31494 RVA: 0x001CA3C8 File Offset: 0x001C85C8
			public DuplexConnectionPoolHelper(ClientFramingDuplexSessionChannel channel, ConnectionPool connectionPool, IConnectionInitiator connectionInitiator) : base(connectionPool, connectionInitiator, channel.Via)
			{
				this.channel = channel;
				this.preamble = channel.CreatePreamble();
			}

			// Token: 0x06007B07 RID: 31495 RVA: 0x001CA3EB File Offset: 0x001C85EB
			protected override TimeoutException CreateNewConnectionTimeoutException(TimeSpan timeout, TimeoutException innerException)
			{
				return new TimeoutException(SR.GetString("OpenTimedOutEstablishingTransportSession", new object[]
				{
					timeout,
					this.channel.Via.AbsoluteUri
				}), innerException);
			}

			// Token: 0x06007B08 RID: 31496 RVA: 0x001CA41F File Offset: 0x001C861F
			protected override IAsyncResult BeginAcceptPooledConnection(IConnection connection, ref TimeoutHelper timeoutHelper, AsyncCallback callback, object state)
			{
				return this.channel.BeginSendPreamble(connection, this.preamble, ref timeoutHelper, callback, state);
			}

			// Token: 0x06007B09 RID: 31497 RVA: 0x001CA437 File Offset: 0x001C8637
			protected override IConnection EndAcceptPooledConnection(IAsyncResult result)
			{
				return this.channel.EndSendPreamble(result);
			}

			// Token: 0x06007B0A RID: 31498 RVA: 0x001CA445 File Offset: 0x001C8645
			protected override IConnection AcceptPooledConnection(IConnection connection, ref TimeoutHelper timeoutHelper)
			{
				return this.channel.SendPreamble(connection, this.preamble, ref timeoutHelper);
			}

			// Token: 0x0400464F RID: 17999
			private ClientFramingDuplexSessionChannel channel;

			// Token: 0x04004650 RID: 18000
			private ArraySegment<byte> preamble;
		}

		// Token: 0x02000D0A RID: 3338
		private class SendPreambleAsyncResult : AsyncResult
		{
			// Token: 0x06007B0B RID: 31499 RVA: 0x001CA45C File Offset: 0x001C865C
			public SendPreambleAsyncResult(ClientFramingDuplexSessionChannel channel, IConnection connection, ArraySegment<byte> preamble, bool flowIdentity, ref TimeoutHelper timeoutHelper, AsyncCallback callback, object state) : base(callback, state)
			{
				this.channel = channel;
				this.timeoutHelper = timeoutHelper;
				this.connection = connection;
				if (TD.ClientSendPreambleStartIsEnabled())
				{
					TD.ClientSendPreambleStart(this.EventTraceActivity);
				}
				if (flowIdentity && !SecurityContext.IsWindowsIdentityFlowSuppressed())
				{
					this.identityToImpersonate = WindowsIdentity.GetCurrent(true);
				}
				channel.decoder = new ClientDuplexDecoder(0L);
				if (connection.BeginWrite(preamble.Array, preamble.Offset, preamble.Count, true, timeoutHelper.RemainingTime(), ClientFramingDuplexSessionChannel.SendPreambleAsyncResult.onWritePreamble, this) == AsyncCompletionResult.Queued)
				{
					return;
				}
				if (this.HandleWritePreamble())
				{
					base.Complete(true);
				}
			}

			// Token: 0x17001BC8 RID: 7112
			// (get) Token: 0x06007B0C RID: 31500 RVA: 0x001CA501 File Offset: 0x001C8701
			private EventTraceActivity EventTraceActivity
			{
				get
				{
					if (this.eventTraceActivity == null)
					{
						this.eventTraceActivity = EventTraceActivity.GetFromThreadOrCreate(false);
					}
					return this.eventTraceActivity;
				}
			}

			// Token: 0x06007B0D RID: 31501 RVA: 0x001CA520 File Offset: 0x001C8720
			public static IConnection End(IAsyncResult result)
			{
				ClientFramingDuplexSessionChannel.SendPreambleAsyncResult sendPreambleAsyncResult = AsyncResult.End<ClientFramingDuplexSessionChannel.SendPreambleAsyncResult>(result);
				return sendPreambleAsyncResult.connection;
			}

			// Token: 0x06007B0E RID: 31502 RVA: 0x001CA53C File Offset: 0x001C873C
			private bool HandleWritePreamble()
			{
				this.connection.EndWrite();
				if (TD.ClientSendPreambleStopIsEnabled())
				{
					TD.ClientSendPreambleStop(this.EventTraceActivity);
				}
				if (this.channel.upgrade != null)
				{
					this.channelBindingProvider = this.channel.upgrade.GetProperty<IStreamUpgradeChannelBindingProvider>();
					this.upgradeInitiator = this.channel.upgrade.CreateUpgradeInitiator(this.channel.RemoteAddress, this.channel.Via);
					if (ClientFramingDuplexSessionChannel.SendPreambleAsyncResult.onUpgradeInitiatorOpen == null)
					{
						ClientFramingDuplexSessionChannel.SendPreambleAsyncResult.onUpgradeInitiatorOpen = Fx.ThunkCallback(new AsyncCallback(ClientFramingDuplexSessionChannel.SendPreambleAsyncResult.OnUpgradeInitiatorOpen));
					}
					IAsyncResult asyncResult = this.upgradeInitiator.BeginOpen(this.timeoutHelper.RemainingTime(), ClientFramingDuplexSessionChannel.SendPreambleAsyncResult.onUpgradeInitiatorOpen, this);
					return asyncResult.CompletedSynchronously && this.HandleInitiatorOpen(asyncResult);
				}
				return this.ReadAck();
			}

			// Token: 0x06007B0F RID: 31503 RVA: 0x001CA60C File Offset: 0x001C880C
			private bool HandleInitiatorOpen(IAsyncResult result)
			{
				this.upgradeInitiator.EndOpen(result);
				if (ClientFramingDuplexSessionChannel.SendPreambleAsyncResult.onUpgrade == null)
				{
					ClientFramingDuplexSessionChannel.SendPreambleAsyncResult.onUpgrade = Fx.ThunkCallback(new AsyncCallback(ClientFramingDuplexSessionChannel.SendPreambleAsyncResult.OnUpgrade));
				}
				IAsyncResult asyncResult = ConnectionUpgradeHelper.BeginInitiateUpgrade(this.channel, this.channel.RemoteAddress, this.connection, this.channel.decoder, this.upgradeInitiator, this.channel.MessageEncoder.ContentType, this.identityToImpersonate, this.timeoutHelper, ClientFramingDuplexSessionChannel.SendPreambleAsyncResult.onUpgrade, this);
				return asyncResult.CompletedSynchronously && this.HandleUpgrade(asyncResult);
			}

			// Token: 0x06007B10 RID: 31504 RVA: 0x001CA6A4 File Offset: 0x001C88A4
			private bool HandleUpgrade(IAsyncResult result)
			{
				this.connection = ConnectionUpgradeHelper.EndInitiateUpgrade(result);
				if (this.channelBindingProvider != null && this.channelBindingProvider.IsChannelBindingSupportEnabled)
				{
					this.channel.SetChannelBinding(this.channelBindingProvider.GetChannelBinding(this.upgradeInitiator, ChannelBindingKind.Endpoint));
				}
				this.channel.SetRemoteSecurity(this.upgradeInitiator);
				if (ClientFramingDuplexSessionChannel.SendPreambleAsyncResult.onUpgradeInitiatorClose == null)
				{
					ClientFramingDuplexSessionChannel.SendPreambleAsyncResult.onUpgradeInitiatorClose = Fx.ThunkCallback(new AsyncCallback(ClientFramingDuplexSessionChannel.SendPreambleAsyncResult.OnUpgradeInitiatorClose));
				}
				IAsyncResult asyncResult = this.upgradeInitiator.BeginClose(this.timeoutHelper.RemainingTime(), ClientFramingDuplexSessionChannel.SendPreambleAsyncResult.onUpgradeInitiatorClose, this);
				return asyncResult.CompletedSynchronously && this.HandleInitiatorClose(asyncResult);
			}

			// Token: 0x06007B11 RID: 31505 RVA: 0x001CA74C File Offset: 0x001C894C
			private bool HandleInitiatorClose(IAsyncResult result)
			{
				this.upgradeInitiator.EndClose(result);
				this.upgradeInitiator = null;
				if (ClientFramingDuplexSessionChannel.SendPreambleAsyncResult.onWritePreambleEnd == null)
				{
					ClientFramingDuplexSessionChannel.SendPreambleAsyncResult.onWritePreambleEnd = Fx.ThunkCallback(new WaitCallback(ClientFramingDuplexSessionChannel.SendPreambleAsyncResult.OnWritePreambleEnd));
				}
				if (this.connection.BeginWrite(SessionEncoder.PreambleEndBytes, 0, SessionEncoder.PreambleEndBytes.Length, true, this.timeoutHelper.RemainingTime(), ClientFramingDuplexSessionChannel.SendPreambleAsyncResult.onWritePreambleEnd, this) == AsyncCompletionResult.Queued)
				{
					return false;
				}
				this.connection.EndWrite();
				return this.ReadAck();
			}

			// Token: 0x06007B12 RID: 31506 RVA: 0x001CA7CC File Offset: 0x001C89CC
			private bool ReadAck()
			{
				return this.connection.BeginRead(0, 1, this.timeoutHelper.RemainingTime(), ClientFramingDuplexSessionChannel.SendPreambleAsyncResult.onReadPreambleAck, this) != AsyncCompletionResult.Queued && this.HandlePreambleAck();
			}

			// Token: 0x06007B13 RID: 31507 RVA: 0x001CA804 File Offset: 0x001C8A04
			private bool HandlePreambleAck()
			{
				int count = this.connection.EndRead();
				if (ConnectionUpgradeHelper.ValidatePreambleResponse(this.connection.AsyncReadBuffer, count, this.channel.decoder, this.channel.Via))
				{
					return true;
				}
				IAsyncResult asyncResult = ConnectionUpgradeHelper.BeginDecodeFramingFault(this.channel.decoder, this.connection, this.channel.Via, this.channel.MessageEncoder.ContentType, ref this.timeoutHelper, Fx.ThunkCallback(new AsyncCallback(this.OnFailedPreamble)), this);
				if (!asyncResult.CompletedSynchronously)
				{
					return false;
				}
				ConnectionUpgradeHelper.EndDecodeFramingFault(asyncResult);
				return true;
			}

			// Token: 0x06007B14 RID: 31508 RVA: 0x001CA8A4 File Offset: 0x001C8AA4
			private static void OnWritePreamble(object asyncState)
			{
				ClientFramingDuplexSessionChannel.SendPreambleAsyncResult sendPreambleAsyncResult = (ClientFramingDuplexSessionChannel.SendPreambleAsyncResult)asyncState;
				Exception exception = null;
				bool flag = false;
				try
				{
					flag = sendPreambleAsyncResult.HandleWritePreamble();
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag = true;
					exception = ex;
				}
				if (flag)
				{
					sendPreambleAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x06007B15 RID: 31509 RVA: 0x001CA8F4 File Offset: 0x001C8AF4
			private static void OnReadPreambleAck(object state)
			{
				ClientFramingDuplexSessionChannel.SendPreambleAsyncResult sendPreambleAsyncResult = (ClientFramingDuplexSessionChannel.SendPreambleAsyncResult)state;
				Exception exception = null;
				bool flag;
				try
				{
					flag = sendPreambleAsyncResult.HandlePreambleAck();
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag = true;
					exception = ex;
				}
				if (flag)
				{
					sendPreambleAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x06007B16 RID: 31510 RVA: 0x001CA940 File Offset: 0x001C8B40
			private static void OnUpgradeInitiatorOpen(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				ClientFramingDuplexSessionChannel.SendPreambleAsyncResult sendPreambleAsyncResult = (ClientFramingDuplexSessionChannel.SendPreambleAsyncResult)result.AsyncState;
				bool flag = false;
				Exception exception = null;
				try
				{
					flag = sendPreambleAsyncResult.HandleInitiatorOpen(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag = true;
					exception = ex;
				}
				if (flag)
				{
					sendPreambleAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x06007B17 RID: 31511 RVA: 0x001CA99C File Offset: 0x001C8B9C
			private static void OnUpgrade(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				ClientFramingDuplexSessionChannel.SendPreambleAsyncResult sendPreambleAsyncResult = (ClientFramingDuplexSessionChannel.SendPreambleAsyncResult)result.AsyncState;
				bool flag = false;
				Exception exception = null;
				try
				{
					flag = sendPreambleAsyncResult.HandleUpgrade(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag = true;
					exception = ex;
				}
				if (flag)
				{
					sendPreambleAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x06007B18 RID: 31512 RVA: 0x001CA9F8 File Offset: 0x001C8BF8
			private static void OnUpgradeInitiatorClose(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				ClientFramingDuplexSessionChannel.SendPreambleAsyncResult sendPreambleAsyncResult = (ClientFramingDuplexSessionChannel.SendPreambleAsyncResult)result.AsyncState;
				bool flag = false;
				Exception exception = null;
				try
				{
					flag = sendPreambleAsyncResult.HandleInitiatorClose(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag = true;
					exception = ex;
				}
				if (flag)
				{
					sendPreambleAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x06007B19 RID: 31513 RVA: 0x001CAA54 File Offset: 0x001C8C54
			private static void OnWritePreambleEnd(object asyncState)
			{
				ClientFramingDuplexSessionChannel.SendPreambleAsyncResult sendPreambleAsyncResult = (ClientFramingDuplexSessionChannel.SendPreambleAsyncResult)asyncState;
				Exception exception = null;
				bool flag = false;
				try
				{
					sendPreambleAsyncResult.connection.EndWrite();
					flag = sendPreambleAsyncResult.ReadAck();
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag = true;
					exception = ex;
				}
				if (flag)
				{
					sendPreambleAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x06007B1A RID: 31514 RVA: 0x001CAAAC File Offset: 0x001C8CAC
			private void OnFailedPreamble(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				Exception exception = null;
				try
				{
					ConnectionUpgradeHelper.EndDecodeFramingFault(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				base.Complete(false, exception);
			}

			// Token: 0x04004651 RID: 18001
			private ClientFramingDuplexSessionChannel channel;

			// Token: 0x04004652 RID: 18002
			private IConnection connection;

			// Token: 0x04004653 RID: 18003
			private TimeoutHelper timeoutHelper;

			// Token: 0x04004654 RID: 18004
			private StreamUpgradeInitiator upgradeInitiator;

			// Token: 0x04004655 RID: 18005
			private IStreamUpgradeChannelBindingProvider channelBindingProvider;

			// Token: 0x04004656 RID: 18006
			private static WaitCallback onReadPreambleAck = new WaitCallback(ClientFramingDuplexSessionChannel.SendPreambleAsyncResult.OnReadPreambleAck);

			// Token: 0x04004657 RID: 18007
			private static WaitCallback onWritePreamble = Fx.ThunkCallback(new WaitCallback(ClientFramingDuplexSessionChannel.SendPreambleAsyncResult.OnWritePreamble));

			// Token: 0x04004658 RID: 18008
			private static WaitCallback onWritePreambleEnd;

			// Token: 0x04004659 RID: 18009
			private static AsyncCallback onUpgrade;

			// Token: 0x0400465A RID: 18010
			private static AsyncCallback onUpgradeInitiatorOpen;

			// Token: 0x0400465B RID: 18011
			private static AsyncCallback onUpgradeInitiatorClose;

			// Token: 0x0400465C RID: 18012
			private WindowsIdentity identityToImpersonate;

			// Token: 0x0400465D RID: 18013
			private EventTraceActivity eventTraceActivity;
		}

		// Token: 0x02000D0B RID: 3339
		private class OpenAsyncResult : AsyncResult
		{
			// Token: 0x06007B1C RID: 31516 RVA: 0x001CAB20 File Offset: 0x001C8D20
			public OpenAsyncResult(ClientFramingDuplexSessionChannel duplexChannel, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.timeoutHelper = new TimeoutHelper(timeout);
				this.duplexChannel = duplexChannel;
				IAsyncResult asyncResult;
				try
				{
					asyncResult = duplexChannel.connectionPoolHelper.BeginEstablishConnection(this.timeoutHelper.RemainingTime(), ClientFramingDuplexSessionChannel.OpenAsyncResult.onEstablishConnection, this);
				}
				catch (TimeoutException innerException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException(SR.GetString("TimeoutOnOpen", new object[]
					{
						timeout
					}), innerException));
				}
				if (!asyncResult.CompletedSynchronously)
				{
					return;
				}
				if (this.HandleEstablishConnection(asyncResult))
				{
					base.Complete(true);
				}
			}

			// Token: 0x06007B1D RID: 31517 RVA: 0x001CABBC File Offset: 0x001C8DBC
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<ClientFramingDuplexSessionChannel.OpenAsyncResult>(result);
			}

			// Token: 0x06007B1E RID: 31518 RVA: 0x001CABC8 File Offset: 0x001C8DC8
			private bool HandleEstablishConnection(IAsyncResult result)
			{
				IConnection connection;
				try
				{
					connection = this.duplexChannel.connectionPoolHelper.EndEstablishConnection(result);
				}
				catch (TimeoutException innerException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException(SR.GetString("TimeoutOnOpen", new object[]
					{
						this.timeoutHelper.OriginalTimeout
					}), innerException));
				}
				this.duplexChannel.AcceptConnection(connection);
				return true;
			}

			// Token: 0x06007B1F RID: 31519 RVA: 0x001CAC3C File Offset: 0x001C8E3C
			private static void OnEstablishConnection(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				ClientFramingDuplexSessionChannel.OpenAsyncResult openAsyncResult = (ClientFramingDuplexSessionChannel.OpenAsyncResult)result.AsyncState;
				Exception exception = null;
				bool flag;
				try
				{
					flag = openAsyncResult.HandleEstablishConnection(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag = true;
					exception = ex;
				}
				if (flag)
				{
					openAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x0400465E RID: 18014
			private static AsyncCallback onEstablishConnection = Fx.ThunkCallback(new AsyncCallback(ClientFramingDuplexSessionChannel.OpenAsyncResult.OnEstablishConnection));

			// Token: 0x0400465F RID: 18015
			private ClientFramingDuplexSessionChannel duplexChannel;

			// Token: 0x04004660 RID: 18016
			private TimeoutHelper timeoutHelper;
		}
	}
}
