using System;
using System.Runtime;
using System.Security.Authentication.ExtendedProtection;
using System.ServiceModel.Security;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200081E RID: 2078
	internal class StreamedFramingRequestChannel : RequestChannel
	{
		// Token: 0x06004DA6 RID: 19878 RVA: 0x0011BAD0 File Offset: 0x00119CD0
		public StreamedFramingRequestChannel(ChannelManagerBase factory, IConnectionOrientedTransportChannelFactorySettings settings, EndpointAddress remoteAddresss, Uri via, IConnectionInitiator connectionInitiator, ConnectionPool connectionPool) : base(factory, remoteAddresss, via, settings.ManualAddressing)
		{
			this.settings = settings;
			this.connectionInitiator = connectionInitiator;
			this.connectionPool = connectionPool;
			this.messageEncoder = settings.MessageEncoderFactory.Encoder;
			this.upgrade = settings.Upgrade;
		}

		// Token: 0x17001377 RID: 4983
		// (get) Token: 0x06004DA7 RID: 19879 RVA: 0x0011BB21 File Offset: 0x00119D21
		private byte[] Preamble
		{
			get
			{
				return this.startBytes;
			}
		}

		// Token: 0x06004DA8 RID: 19880 RVA: 0x0011BB29 File Offset: 0x00119D29
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06004DA9 RID: 19881 RVA: 0x0011BB32 File Offset: 0x00119D32
		protected override void OnEndOpen(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x06004DAA RID: 19882 RVA: 0x0011BB3A File Offset: 0x00119D3A
		protected override void OnOpen(TimeSpan timeout)
		{
		}

		// Token: 0x06004DAB RID: 19883 RVA: 0x0011BB3C File Offset: 0x00119D3C
		protected override void OnOpened()
		{
			EncodedVia via = new EncodedVia(base.Via.AbsoluteUri);
			EncodedContentType contentType = EncodedContentType.Create(this.settings.MessageEncoderFactory.Encoder.ContentType);
			int num = ClientSingletonEncoder.ModeBytes.Length + ClientSingletonEncoder.CalcStartSize(via, contentType);
			int num2 = 0;
			if (this.upgrade == null)
			{
				num2 = num;
				num += SessionEncoder.PreambleEndBytes.Length;
			}
			this.startBytes = DiagnosticUtility.Utility.AllocateByteArray(num);
			Buffer.BlockCopy(ClientSingletonEncoder.ModeBytes, 0, this.startBytes, 0, ClientSingletonEncoder.ModeBytes.Length);
			ClientSingletonEncoder.EncodeStart(this.startBytes, ClientSingletonEncoder.ModeBytes.Length, via, contentType);
			if (num2 > 0)
			{
				Buffer.BlockCopy(ClientSingletonEncoder.PreambleEndBytes, 0, this.startBytes, num2, ClientSingletonEncoder.PreambleEndBytes.Length);
			}
			base.OnOpened();
		}

		// Token: 0x06004DAC RID: 19884 RVA: 0x0011BBFC File Offset: 0x00119DFC
		protected override IAsyncRequest CreateAsyncRequest(Message message, AsyncCallback callback, object state)
		{
			return new StreamedFramingRequestChannel.StreamedFramingAsyncRequest(this, callback, state);
		}

		// Token: 0x06004DAD RID: 19885 RVA: 0x0011BC06 File Offset: 0x00119E06
		protected override IRequest CreateRequest(Message message)
		{
			return new StreamedFramingRequestChannel.StreamedFramingRequest(this);
		}

		// Token: 0x06004DAE RID: 19886 RVA: 0x0011BC10 File Offset: 0x00119E10
		private IConnection SendPreamble(IConnection connection, ref TimeoutHelper timeoutHelper, ClientFramingDecoder decoder, out SecurityMessageProperty remoteSecurity)
		{
			connection.Write(this.Preamble, 0, this.Preamble.Length, true, timeoutHelper.RemainingTime());
			if (this.upgrade != null)
			{
				IStreamUpgradeChannelBindingProvider property = this.upgrade.GetProperty<IStreamUpgradeChannelBindingProvider>();
				StreamUpgradeInitiator upgradeInitiator = this.upgrade.CreateUpgradeInitiator(base.RemoteAddress, base.Via);
				if (!ConnectionUpgradeHelper.InitiateUpgrade(upgradeInitiator, ref connection, decoder, this, ref timeoutHelper))
				{
					ConnectionUpgradeHelper.DecodeFramingFault(decoder, connection, base.Via, this.messageEncoder.ContentType, ref timeoutHelper);
				}
				if (property != null && property.IsChannelBindingSupportEnabled)
				{
					this.channelBindingToken = property.GetChannelBinding(upgradeInitiator, ChannelBindingKind.Endpoint);
				}
				remoteSecurity = StreamSecurityUpgradeInitiator.GetRemoteSecurity(upgradeInitiator);
				connection.Write(ClientSingletonEncoder.PreambleEndBytes, 0, ClientSingletonEncoder.PreambleEndBytes.Length, true, timeoutHelper.RemainingTime());
			}
			else
			{
				remoteSecurity = null;
			}
			byte[] array = new byte[1];
			int count = connection.Read(array, 0, array.Length, timeoutHelper.RemainingTime());
			if (!ConnectionUpgradeHelper.ValidatePreambleResponse(array, count, decoder, base.Via))
			{
				ConnectionUpgradeHelper.DecodeFramingFault(decoder, connection, base.Via, this.messageEncoder.ContentType, ref timeoutHelper);
			}
			return connection;
		}

		// Token: 0x06004DAF RID: 19887 RVA: 0x0011BD14 File Offset: 0x00119F14
		protected override void OnClose(TimeSpan timeout)
		{
			base.WaitForPendingRequests(timeout);
		}

		// Token: 0x06004DB0 RID: 19888 RVA: 0x0011BD1D File Offset: 0x00119F1D
		protected override void OnClosed()
		{
			base.OnClosed();
			ChannelBindingUtility.Dispose(ref this.channelBindingToken);
		}

		// Token: 0x06004DB1 RID: 19889 RVA: 0x0011BD30 File Offset: 0x00119F30
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return base.BeginWaitForPendingRequests(timeout, callback, state);
		}

		// Token: 0x06004DB2 RID: 19890 RVA: 0x0011BD3B File Offset: 0x00119F3B
		protected override void OnEndClose(IAsyncResult result)
		{
			base.EndWaitForPendingRequests(result);
		}

		// Token: 0x04003094 RID: 12436
		private IConnectionInitiator connectionInitiator;

		// Token: 0x04003095 RID: 12437
		private ConnectionPool connectionPool;

		// Token: 0x04003096 RID: 12438
		private MessageEncoder messageEncoder;

		// Token: 0x04003097 RID: 12439
		private IConnectionOrientedTransportFactorySettings settings;

		// Token: 0x04003098 RID: 12440
		private byte[] startBytes;

		// Token: 0x04003099 RID: 12441
		private StreamUpgradeProvider upgrade;

		// Token: 0x0400309A RID: 12442
		private ChannelBinding channelBindingToken;

		// Token: 0x02000D1C RID: 3356
		internal class StreamedConnectionPoolHelper : ConnectionPoolHelper
		{
			// Token: 0x06007B82 RID: 31618 RVA: 0x001CCED4 File Offset: 0x001CB0D4
			public StreamedConnectionPoolHelper(StreamedFramingRequestChannel channel) : base(channel.connectionPool, channel.connectionInitiator, channel.Via)
			{
				this.channel = channel;
			}

			// Token: 0x17001BCE RID: 7118
			// (get) Token: 0x06007B83 RID: 31619 RVA: 0x001CCEF5 File Offset: 0x001CB0F5
			public ClientSingletonDecoder Decoder
			{
				get
				{
					return this.decoder;
				}
			}

			// Token: 0x17001BCF RID: 7119
			// (get) Token: 0x06007B84 RID: 31620 RVA: 0x001CCEFD File Offset: 0x001CB0FD
			public SecurityMessageProperty RemoteSecurity
			{
				get
				{
					return this.remoteSecurity;
				}
			}

			// Token: 0x06007B85 RID: 31621 RVA: 0x001CCF05 File Offset: 0x001CB105
			protected override TimeoutException CreateNewConnectionTimeoutException(TimeSpan timeout, TimeoutException innerException)
			{
				return new TimeoutException(SR.GetString("RequestTimedOutEstablishingTransportSession", new object[]
				{
					timeout,
					this.channel.Via.AbsoluteUri
				}), innerException);
			}

			// Token: 0x06007B86 RID: 31622 RVA: 0x001CCF39 File Offset: 0x001CB139
			protected override IConnection AcceptPooledConnection(IConnection connection, ref TimeoutHelper timeoutHelper)
			{
				this.decoder = new ClientSingletonDecoder(0L);
				return this.channel.SendPreamble(connection, ref timeoutHelper, this.decoder, out this.remoteSecurity);
			}

			// Token: 0x06007B87 RID: 31623 RVA: 0x001CCF61 File Offset: 0x001CB161
			protected override IAsyncResult BeginAcceptPooledConnection(IConnection connection, ref TimeoutHelper timeoutHelper, AsyncCallback callback, object state)
			{
				this.decoder = new ClientSingletonDecoder(0L);
				return new StreamedFramingRequestChannel.StreamedConnectionPoolHelper.SendPreambleAsyncResult(this.channel, connection, ref timeoutHelper, this.decoder, callback, state);
			}

			// Token: 0x06007B88 RID: 31624 RVA: 0x001CCF86 File Offset: 0x001CB186
			protected override IConnection EndAcceptPooledConnection(IAsyncResult result)
			{
				return StreamedFramingRequestChannel.StreamedConnectionPoolHelper.SendPreambleAsyncResult.End(result, out this.remoteSecurity);
			}

			// Token: 0x040046E4 RID: 18148
			private StreamedFramingRequestChannel channel;

			// Token: 0x040046E5 RID: 18149
			private ClientSingletonDecoder decoder;

			// Token: 0x040046E6 RID: 18150
			private SecurityMessageProperty remoteSecurity;

			// Token: 0x02000F4A RID: 3914
			private class SendPreambleAsyncResult : AsyncResult
			{
				// Token: 0x060086E3 RID: 34531 RVA: 0x001F4074 File Offset: 0x001F2274
				public SendPreambleAsyncResult(StreamedFramingRequestChannel channel, IConnection connection, ref TimeoutHelper timeoutHelper, ClientFramingDecoder decoder, AsyncCallback callback, object state) : base(callback, state)
				{
					this.channel = channel;
					this.connection = connection;
					this.timeoutHelper = timeoutHelper;
					this.decoder = decoder;
					if (connection.BeginWrite(channel.Preamble, 0, channel.Preamble.Length, true, timeoutHelper.RemainingTime(), StreamedFramingRequestChannel.StreamedConnectionPoolHelper.SendPreambleAsyncResult.onWritePreamble, this) == AsyncCompletionResult.Queued)
					{
						return;
					}
					if (this.HandleWritePreamble())
					{
						base.Complete(true);
					}
				}

				// Token: 0x060086E4 RID: 34532 RVA: 0x001F40E4 File Offset: 0x001F22E4
				public static IConnection End(IAsyncResult result, out SecurityMessageProperty remoteSecurity)
				{
					StreamedFramingRequestChannel.StreamedConnectionPoolHelper.SendPreambleAsyncResult sendPreambleAsyncResult = AsyncResult.End<StreamedFramingRequestChannel.StreamedConnectionPoolHelper.SendPreambleAsyncResult>(result);
					remoteSecurity = sendPreambleAsyncResult.remoteSecurity;
					return sendPreambleAsyncResult.connection;
				}

				// Token: 0x060086E5 RID: 34533 RVA: 0x001F4108 File Offset: 0x001F2308
				private bool HandleWritePreamble()
				{
					this.connection.EndWrite();
					if (this.channel.upgrade == null)
					{
						return this.ReadPreambleAck();
					}
					this.channelBindingProvider = this.channel.upgrade.GetProperty<IStreamUpgradeChannelBindingProvider>();
					this.upgradeInitiator = this.channel.upgrade.CreateUpgradeInitiator(this.channel.RemoteAddress, this.channel.Via);
					if (StreamedFramingRequestChannel.StreamedConnectionPoolHelper.SendPreambleAsyncResult.onUpgrade == null)
					{
						StreamedFramingRequestChannel.StreamedConnectionPoolHelper.SendPreambleAsyncResult.onUpgrade = Fx.ThunkCallback(new AsyncCallback(StreamedFramingRequestChannel.StreamedConnectionPoolHelper.SendPreambleAsyncResult.OnUpgrade));
					}
					IAsyncResult asyncResult = ConnectionUpgradeHelper.BeginInitiateUpgrade(this.channel.settings, this.channel.RemoteAddress, this.connection, this.decoder, this.upgradeInitiator, this.channel.messageEncoder.ContentType, null, this.timeoutHelper, StreamedFramingRequestChannel.StreamedConnectionPoolHelper.SendPreambleAsyncResult.onUpgrade, this);
					return asyncResult.CompletedSynchronously && this.HandleUpgrade(asyncResult);
				}

				// Token: 0x060086E6 RID: 34534 RVA: 0x001F41F0 File Offset: 0x001F23F0
				private bool HandleUpgrade(IAsyncResult result)
				{
					this.connection = ConnectionUpgradeHelper.EndInitiateUpgrade(result);
					if (this.channelBindingProvider != null && this.channelBindingProvider.IsChannelBindingSupportEnabled)
					{
						this.channel.channelBindingToken = this.channelBindingProvider.GetChannelBinding(this.upgradeInitiator, ChannelBindingKind.Endpoint);
					}
					this.remoteSecurity = StreamSecurityUpgradeInitiator.GetRemoteSecurity(this.upgradeInitiator);
					this.upgradeInitiator = null;
					if (StreamedFramingRequestChannel.StreamedConnectionPoolHelper.SendPreambleAsyncResult.onWritePreambleEnd == null)
					{
						StreamedFramingRequestChannel.StreamedConnectionPoolHelper.SendPreambleAsyncResult.onWritePreambleEnd = Fx.ThunkCallback(new WaitCallback(StreamedFramingRequestChannel.StreamedConnectionPoolHelper.SendPreambleAsyncResult.OnWritePreambleEnd));
					}
					if (this.connection.BeginWrite(ClientSingletonEncoder.PreambleEndBytes, 0, ClientSingletonEncoder.PreambleEndBytes.Length, true, this.timeoutHelper.RemainingTime(), StreamedFramingRequestChannel.StreamedConnectionPoolHelper.SendPreambleAsyncResult.onWritePreambleEnd, this) == AsyncCompletionResult.Queued)
					{
						return false;
					}
					this.connection.EndWrite();
					return this.ReadPreambleAck();
				}

				// Token: 0x060086E7 RID: 34535 RVA: 0x001F42B4 File Offset: 0x001F24B4
				private bool ReadPreambleAck()
				{
					return this.connection.BeginRead(0, 1, this.timeoutHelper.RemainingTime(), StreamedFramingRequestChannel.StreamedConnectionPoolHelper.SendPreambleAsyncResult.onReadPreambleAck, this) != AsyncCompletionResult.Queued && this.HandlePreambleAck();
				}

				// Token: 0x060086E8 RID: 34536 RVA: 0x001F42EC File Offset: 0x001F24EC
				private bool HandlePreambleAck()
				{
					int count = this.connection.EndRead();
					if (ConnectionUpgradeHelper.ValidatePreambleResponse(this.connection.AsyncReadBuffer, count, this.decoder, this.channel.Via))
					{
						return true;
					}
					if (StreamedFramingRequestChannel.StreamedConnectionPoolHelper.SendPreambleAsyncResult.onFailedUpgrade == null)
					{
						StreamedFramingRequestChannel.StreamedConnectionPoolHelper.SendPreambleAsyncResult.onFailedUpgrade = Fx.ThunkCallback(new AsyncCallback(StreamedFramingRequestChannel.StreamedConnectionPoolHelper.SendPreambleAsyncResult.OnFailedUpgrade));
					}
					IAsyncResult asyncResult = ConnectionUpgradeHelper.BeginDecodeFramingFault(this.decoder, this.connection, this.channel.Via, this.channel.messageEncoder.ContentType, ref this.timeoutHelper, StreamedFramingRequestChannel.StreamedConnectionPoolHelper.SendPreambleAsyncResult.onFailedUpgrade, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return false;
					}
					ConnectionUpgradeHelper.EndDecodeFramingFault(asyncResult);
					return true;
				}

				// Token: 0x060086E9 RID: 34537 RVA: 0x001F4394 File Offset: 0x001F2594
				private static void OnWritePreamble(object asyncState)
				{
					StreamedFramingRequestChannel.StreamedConnectionPoolHelper.SendPreambleAsyncResult sendPreambleAsyncResult = (StreamedFramingRequestChannel.StreamedConnectionPoolHelper.SendPreambleAsyncResult)asyncState;
					Exception exception = null;
					bool flag;
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

				// Token: 0x060086EA RID: 34538 RVA: 0x001F43E0 File Offset: 0x001F25E0
				private static void OnWritePreambleEnd(object asyncState)
				{
					StreamedFramingRequestChannel.StreamedConnectionPoolHelper.SendPreambleAsyncResult sendPreambleAsyncResult = (StreamedFramingRequestChannel.StreamedConnectionPoolHelper.SendPreambleAsyncResult)asyncState;
					Exception exception = null;
					bool flag;
					try
					{
						sendPreambleAsyncResult.connection.EndWrite();
						flag = sendPreambleAsyncResult.ReadPreambleAck();
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

				// Token: 0x060086EB RID: 34539 RVA: 0x001F4438 File Offset: 0x001F2638
				private static void OnReadPreambleAck(object state)
				{
					StreamedFramingRequestChannel.StreamedConnectionPoolHelper.SendPreambleAsyncResult sendPreambleAsyncResult = (StreamedFramingRequestChannel.StreamedConnectionPoolHelper.SendPreambleAsyncResult)state;
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

				// Token: 0x060086EC RID: 34540 RVA: 0x001F4484 File Offset: 0x001F2684
				private static void OnUpgrade(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					StreamedFramingRequestChannel.StreamedConnectionPoolHelper.SendPreambleAsyncResult sendPreambleAsyncResult = (StreamedFramingRequestChannel.StreamedConnectionPoolHelper.SendPreambleAsyncResult)result.AsyncState;
					Exception exception = null;
					bool flag;
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

				// Token: 0x060086ED RID: 34541 RVA: 0x001F44E0 File Offset: 0x001F26E0
				private static void OnFailedUpgrade(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					StreamedFramingRequestChannel.StreamedConnectionPoolHelper.SendPreambleAsyncResult sendPreambleAsyncResult = (StreamedFramingRequestChannel.StreamedConnectionPoolHelper.SendPreambleAsyncResult)result.AsyncState;
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
					sendPreambleAsyncResult.Complete(false, exception);
				}

				// Token: 0x04004E6D RID: 20077
				private StreamedFramingRequestChannel channel;

				// Token: 0x04004E6E RID: 20078
				private IConnection connection;

				// Token: 0x04004E6F RID: 20079
				private ClientFramingDecoder decoder;

				// Token: 0x04004E70 RID: 20080
				private StreamUpgradeInitiator upgradeInitiator;

				// Token: 0x04004E71 RID: 20081
				private SecurityMessageProperty remoteSecurity;

				// Token: 0x04004E72 RID: 20082
				private TimeoutHelper timeoutHelper;

				// Token: 0x04004E73 RID: 20083
				private static WaitCallback onWritePreamble = Fx.ThunkCallback(new WaitCallback(StreamedFramingRequestChannel.StreamedConnectionPoolHelper.SendPreambleAsyncResult.OnWritePreamble));

				// Token: 0x04004E74 RID: 20084
				private static WaitCallback onWritePreambleEnd;

				// Token: 0x04004E75 RID: 20085
				private static WaitCallback onReadPreambleAck = new WaitCallback(StreamedFramingRequestChannel.StreamedConnectionPoolHelper.SendPreambleAsyncResult.OnReadPreambleAck);

				// Token: 0x04004E76 RID: 20086
				private static AsyncCallback onUpgrade;

				// Token: 0x04004E77 RID: 20087
				private static AsyncCallback onFailedUpgrade;

				// Token: 0x04004E78 RID: 20088
				private IStreamUpgradeChannelBindingProvider channelBindingProvider;
			}
		}

		// Token: 0x02000D1D RID: 3357
		private class ClientSingletonConnectionReader : SingletonConnectionReader
		{
			// Token: 0x06007B89 RID: 31625 RVA: 0x001CCF94 File Offset: 0x001CB194
			public ClientSingletonConnectionReader(IConnection connection, StreamedFramingRequestChannel.StreamedConnectionPoolHelper connectionPoolHelper, IConnectionOrientedTransportFactorySettings settings) : base(connection, 0, 0, connectionPoolHelper.RemoteSecurity, settings, null)
			{
				this.connectionPoolHelper = connectionPoolHelper;
			}

			// Token: 0x17001BD0 RID: 7120
			// (get) Token: 0x06007B8A RID: 31626 RVA: 0x001CCFAE File Offset: 0x001CB1AE
			protected override long StreamPosition
			{
				get
				{
					return this.connectionPoolHelper.Decoder.StreamPosition;
				}
			}

			// Token: 0x06007B8B RID: 31627 RVA: 0x001CCFC0 File Offset: 0x001CB1C0
			protected override bool DecodeBytes(byte[] buffer, ref int offset, ref int size, ref bool isAtEof)
			{
				while (size > 0)
				{
					int num = this.connectionPoolHelper.Decoder.Decode(buffer, offset, size);
					if (num > 0)
					{
						offset += num;
						size -= num;
					}
					ClientFramingDecoderState currentState = this.connectionPoolHelper.Decoder.CurrentState;
					if (currentState == ClientFramingDecoderState.EnvelopeStart)
					{
						return true;
					}
					if (currentState == ClientFramingDecoderState.End)
					{
						isAtEof = true;
						return false;
					}
				}
				return false;
			}

			// Token: 0x06007B8C RID: 31628 RVA: 0x001CD020 File Offset: 0x001CB220
			protected override void OnClose(TimeSpan timeout)
			{
				this.connectionPoolHelper.Close(timeout);
			}

			// Token: 0x040046E7 RID: 18151
			private StreamedFramingRequestChannel.StreamedConnectionPoolHelper connectionPoolHelper;
		}

		// Token: 0x02000D1E RID: 3358
		private class StreamedFramingRequest : IRequest, IRequestBase
		{
			// Token: 0x06007B8D RID: 31629 RVA: 0x001CD02E File Offset: 0x001CB22E
			public StreamedFramingRequest(StreamedFramingRequestChannel channel)
			{
				this.channel = channel;
				this.connectionPoolHelper = new StreamedFramingRequestChannel.StreamedConnectionPoolHelper(channel);
			}

			// Token: 0x06007B8E RID: 31630 RVA: 0x001CD04C File Offset: 0x001CB24C
			public void SendRequest(Message message, TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				try
				{
					this.connection = this.connectionPoolHelper.EstablishConnection(timeoutHelper.RemainingTime());
					ChannelBindingUtility.TryAddToMessage(this.channel.channelBindingToken, message, false);
					bool flag = false;
					try
					{
						StreamingConnectionHelper.WriteMessage(message, this.connection, true, this.channel.settings, ref timeoutHelper);
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
				catch (TimeoutException innerException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException(SR.GetString("TimeoutOnRequest", new object[]
					{
						timeout
					}), innerException));
				}
			}

			// Token: 0x06007B8F RID: 31631 RVA: 0x001CD104 File Offset: 0x001CB304
			public Message WaitForReply(TimeSpan timeout)
			{
				StreamedFramingRequestChannel.ClientSingletonConnectionReader clientSingletonConnectionReader = new StreamedFramingRequestChannel.ClientSingletonConnectionReader(this.connection, this.connectionPoolHelper, this.channel.settings);
				clientSingletonConnectionReader.DoneSending(TimeSpan.Zero);
				Message message = clientSingletonConnectionReader.Receive(timeout);
				if (message != null)
				{
					ChannelBindingUtility.TryAddToMessage(this.channel.channelBindingToken, message, false);
				}
				return message;
			}

			// Token: 0x06007B90 RID: 31632 RVA: 0x001CD157 File Offset: 0x001CB357
			private void Cleanup()
			{
				this.connectionPoolHelper.Abort();
			}

			// Token: 0x06007B91 RID: 31633 RVA: 0x001CD164 File Offset: 0x001CB364
			public void Abort(RequestChannel requestChannel)
			{
				this.Cleanup();
			}

			// Token: 0x06007B92 RID: 31634 RVA: 0x001CD16C File Offset: 0x001CB36C
			public void Fault(RequestChannel requestChannel)
			{
				this.Cleanup();
			}

			// Token: 0x06007B93 RID: 31635 RVA: 0x001CD174 File Offset: 0x001CB374
			public void OnReleaseRequest()
			{
			}

			// Token: 0x040046E8 RID: 18152
			private StreamedFramingRequestChannel channel;

			// Token: 0x040046E9 RID: 18153
			private StreamedFramingRequestChannel.StreamedConnectionPoolHelper connectionPoolHelper;

			// Token: 0x040046EA RID: 18154
			private IConnection connection;
		}

		// Token: 0x02000D1F RID: 3359
		private class StreamedFramingAsyncRequest : AsyncResult, IAsyncRequest, IAsyncResult, IRequestBase
		{
			// Token: 0x06007B94 RID: 31636 RVA: 0x001CD176 File Offset: 0x001CB376
			public StreamedFramingAsyncRequest(StreamedFramingRequestChannel channel, AsyncCallback callback, object state) : base(callback, state)
			{
				this.channel = channel;
				this.connectionPoolHelper = new StreamedFramingRequestChannel.StreamedConnectionPoolHelper(channel);
			}

			// Token: 0x06007B95 RID: 31637 RVA: 0x001CD194 File Offset: 0x001CB394
			public void BeginSendRequest(Message message, TimeSpan timeout)
			{
				this.timeoutHelper = new TimeoutHelper(timeout);
				this.message = message;
				bool flag = false;
				bool flag2 = false;
				try
				{
					try
					{
						IAsyncResult asyncResult = this.connectionPoolHelper.BeginEstablishConnection(this.timeoutHelper.RemainingTime(), StreamedFramingRequestChannel.StreamedFramingAsyncRequest.onEstablishConnection, this);
						if (asyncResult.CompletedSynchronously)
						{
							flag = this.HandleEstablishConnection(asyncResult);
						}
					}
					catch (TimeoutException innerException)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException(SR.GetString("TimeoutOnRequest", new object[]
						{
							timeout
						}), innerException));
					}
					flag2 = true;
				}
				finally
				{
					if (!flag2)
					{
						this.Cleanup();
					}
				}
				if (flag)
				{
					base.Complete(true);
				}
			}

			// Token: 0x06007B96 RID: 31638 RVA: 0x001CD248 File Offset: 0x001CB448
			private bool HandleEstablishConnection(IAsyncResult result)
			{
				this.connection = this.connectionPoolHelper.EndEstablishConnection(result);
				ChannelBindingUtility.TryAddToMessage(this.channel.channelBindingToken, this.message, false);
				IAsyncResult asyncResult = StreamingConnectionHelper.BeginWriteMessage(this.message, this.connection, true, this.channel.settings, ref this.timeoutHelper, StreamedFramingRequestChannel.StreamedFramingAsyncRequest.onWriteMessage, this);
				return asyncResult.CompletedSynchronously && this.HandleWriteMessage(asyncResult);
			}

			// Token: 0x06007B97 RID: 31639 RVA: 0x001CD2BC File Offset: 0x001CB4BC
			public Message End()
			{
				try
				{
					AsyncResult.End<StreamedFramingRequestChannel.StreamedFramingAsyncRequest>(this);
				}
				catch (TimeoutException innerException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException(SR.GetString("TimeoutOnRequest", new object[]
					{
						this.timeoutHelper.OriginalTimeout
					}), innerException));
				}
				return this.replyMessage;
			}

			// Token: 0x06007B98 RID: 31640 RVA: 0x001CD320 File Offset: 0x001CB520
			public void Abort(RequestChannel requestChannel)
			{
				this.Cleanup();
			}

			// Token: 0x06007B99 RID: 31641 RVA: 0x001CD328 File Offset: 0x001CB528
			public void Fault(RequestChannel requestChannel)
			{
				this.Cleanup();
			}

			// Token: 0x06007B9A RID: 31642 RVA: 0x001CD330 File Offset: 0x001CB530
			private void Cleanup()
			{
				this.connectionPoolHelper.Abort();
			}

			// Token: 0x06007B9B RID: 31643 RVA: 0x001CD340 File Offset: 0x001CB540
			private bool HandleWriteMessage(IAsyncResult result)
			{
				StreamingConnectionHelper.EndWriteMessage(result);
				this.connectionReader = new StreamedFramingRequestChannel.ClientSingletonConnectionReader(this.connection, this.connectionPoolHelper, this.channel.settings);
				this.connectionReader.DoneSending(TimeSpan.Zero);
				IAsyncResult asyncResult = this.connectionReader.BeginReceive(this.timeoutHelper.RemainingTime(), StreamedFramingRequestChannel.StreamedFramingAsyncRequest.onReceiveReply, this);
				return asyncResult.CompletedSynchronously && this.CompleteReceiveReply(asyncResult);
			}

			// Token: 0x06007B9C RID: 31644 RVA: 0x001CD3B3 File Offset: 0x001CB5B3
			private bool CompleteReceiveReply(IAsyncResult result)
			{
				this.replyMessage = this.connectionReader.EndReceive(result);
				if (this.replyMessage != null)
				{
					ChannelBindingUtility.TryAddToMessage(this.channel.channelBindingToken, this.replyMessage, false);
				}
				return true;
			}

			// Token: 0x06007B9D RID: 31645 RVA: 0x001CD3E8 File Offset: 0x001CB5E8
			private static void OnEstablishConnection(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				StreamedFramingRequestChannel.StreamedFramingAsyncRequest streamedFramingAsyncRequest = (StreamedFramingRequestChannel.StreamedFramingAsyncRequest)result.AsyncState;
				Exception exception = null;
				bool flag = true;
				bool flag2;
				try
				{
					flag2 = streamedFramingAsyncRequest.HandleEstablishConnection(result);
					flag = false;
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag2 = true;
					exception = ex;
				}
				finally
				{
					if (flag)
					{
						streamedFramingAsyncRequest.Cleanup();
					}
				}
				if (flag2)
				{
					streamedFramingAsyncRequest.Complete(false, exception);
				}
			}

			// Token: 0x06007B9E RID: 31646 RVA: 0x001CD460 File Offset: 0x001CB660
			private static void OnWriteMessage(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				StreamedFramingRequestChannel.StreamedFramingAsyncRequest streamedFramingAsyncRequest = (StreamedFramingRequestChannel.StreamedFramingAsyncRequest)result.AsyncState;
				Exception exception = null;
				bool flag = true;
				bool flag2;
				try
				{
					flag2 = streamedFramingAsyncRequest.HandleWriteMessage(result);
					flag = false;
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag2 = true;
					exception = ex;
				}
				finally
				{
					if (flag)
					{
						streamedFramingAsyncRequest.Cleanup();
					}
				}
				if (flag2)
				{
					streamedFramingAsyncRequest.Complete(false, exception);
				}
			}

			// Token: 0x06007B9F RID: 31647 RVA: 0x001CD4D8 File Offset: 0x001CB6D8
			private static void OnReceiveReply(IAsyncResult result)
			{
				StreamedFramingRequestChannel.StreamedFramingAsyncRequest streamedFramingAsyncRequest = (StreamedFramingRequestChannel.StreamedFramingAsyncRequest)result.AsyncState;
				Exception exception = null;
				bool flag = true;
				bool flag2;
				try
				{
					flag2 = streamedFramingAsyncRequest.CompleteReceiveReply(result);
					flag = false;
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag2 = true;
					exception = ex;
				}
				finally
				{
					if (flag)
					{
						streamedFramingAsyncRequest.Cleanup();
					}
				}
				if (flag2)
				{
					streamedFramingAsyncRequest.Complete(false, exception);
				}
			}

			// Token: 0x06007BA0 RID: 31648 RVA: 0x001CD548 File Offset: 0x001CB748
			public void OnReleaseRequest()
			{
			}

			// Token: 0x040046EB RID: 18155
			private StreamedFramingRequestChannel channel;

			// Token: 0x040046EC RID: 18156
			private IConnection connection;

			// Token: 0x040046ED RID: 18157
			private StreamedFramingRequestChannel.StreamedConnectionPoolHelper connectionPoolHelper;

			// Token: 0x040046EE RID: 18158
			private Message message;

			// Token: 0x040046EF RID: 18159
			private Message replyMessage;

			// Token: 0x040046F0 RID: 18160
			private TimeoutHelper timeoutHelper;

			// Token: 0x040046F1 RID: 18161
			private static AsyncCallback onEstablishConnection = Fx.ThunkCallback(new AsyncCallback(StreamedFramingRequestChannel.StreamedFramingAsyncRequest.OnEstablishConnection));

			// Token: 0x040046F2 RID: 18162
			private static AsyncCallback onWriteMessage = Fx.ThunkCallback(new AsyncCallback(StreamedFramingRequestChannel.StreamedFramingAsyncRequest.OnWriteMessage));

			// Token: 0x040046F3 RID: 18163
			private static AsyncCallback onReceiveReply = Fx.ThunkCallback(new AsyncCallback(StreamedFramingRequestChannel.StreamedFramingAsyncRequest.OnReceiveReply));

			// Token: 0x040046F4 RID: 18164
			private StreamedFramingRequestChannel.ClientSingletonConnectionReader connectionReader;
		}
	}
}
