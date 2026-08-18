using System;
using System.Diagnostics;
using System.Net;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Security.Authentication.ExtendedProtection;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security;
using System.Threading;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000814 RID: 2068
	internal class ServerSessionPreambleConnectionReader : InitialServerConnectionReader
	{
		// Token: 0x06004D3F RID: 19775 RVA: 0x00119F20 File Offset: 0x00118120
		public ServerSessionPreambleConnectionReader(IConnection connection, Action connectionDequeuedCallback, long streamPosition, int offset, int size, TransportSettingsCallback transportSettingsCallback, ConnectionClosedCallback closedCallback, ServerSessionPreambleCallback callback) : base(connection, closedCallback)
		{
			this.rawConnection = connection;
			this.decoder = new ServerSessionDecoder(streamPosition, base.MaxViaSize, base.MaxContentTypeSize);
			this.offset = offset;
			this.size = size;
			this.transportSettingsCallback = transportSettingsCallback;
			this.callback = callback;
			base.ConnectionDequeuedCallback = connectionDequeuedCallback;
		}

		// Token: 0x17001360 RID: 4960
		// (get) Token: 0x06004D40 RID: 19776 RVA: 0x00119F7C File Offset: 0x0011817C
		public int BufferOffset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x17001361 RID: 4961
		// (get) Token: 0x06004D41 RID: 19777 RVA: 0x00119F84 File Offset: 0x00118184
		public int BufferSize
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x17001362 RID: 4962
		// (get) Token: 0x06004D42 RID: 19778 RVA: 0x00119F8C File Offset: 0x0011818C
		public ServerSessionDecoder Decoder
		{
			get
			{
				return this.decoder;
			}
		}

		// Token: 0x17001363 RID: 4963
		// (get) Token: 0x06004D43 RID: 19779 RVA: 0x00119F94 File Offset: 0x00118194
		public IConnection RawConnection
		{
			get
			{
				return this.rawConnection;
			}
		}

		// Token: 0x17001364 RID: 4964
		// (get) Token: 0x06004D44 RID: 19780 RVA: 0x00119F9C File Offset: 0x0011819C
		public Uri Via
		{
			get
			{
				return this.via;
			}
		}

		// Token: 0x06004D45 RID: 19781 RVA: 0x00119FA4 File Offset: 0x001181A4
		private TimeSpan GetRemainingTimeout()
		{
			return this.receiveTimeoutHelper.RemainingTime();
		}

		// Token: 0x06004D46 RID: 19782 RVA: 0x00119FB4 File Offset: 0x001181B4
		private static void ReadCallback(object state)
		{
			ServerSessionPreambleConnectionReader serverSessionPreambleConnectionReader = (ServerSessionPreambleConnectionReader)state;
			bool flag = false;
			try
			{
				serverSessionPreambleConnectionReader.GetReadResult();
				serverSessionPreambleConnectionReader.ContinueReading();
				flag = true;
			}
			catch (CommunicationException exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
			}
			catch (TimeoutException ex)
			{
				if (TD.ReceiveTimeoutIsEnabled())
				{
					TD.ReceiveTimeout(ex.Message);
				}
				DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
			}
			catch (Exception exception2)
			{
				if (Fx.IsFatal(exception2))
				{
					throw;
				}
				if (!ExceptionHandler.HandleTransportExceptionHelper(exception2))
				{
					throw;
				}
			}
			finally
			{
				if (!flag)
				{
					serverSessionPreambleConnectionReader.Abort();
				}
			}
		}

		// Token: 0x06004D47 RID: 19783 RVA: 0x0011A058 File Offset: 0x00118258
		private void GetReadResult()
		{
			this.offset = 0;
			this.size = base.Connection.EndRead();
			if (this.size == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.decoder.CreatePrematureEOFException());
			}
		}

		// Token: 0x06004D48 RID: 19784 RVA: 0x0011A090 File Offset: 0x00118290
		private void ContinueReading()
		{
			bool flag = false;
			try
			{
				do
				{
					if (this.size == 0)
					{
						if (ServerSessionPreambleConnectionReader.readCallback == null)
						{
							ServerSessionPreambleConnectionReader.readCallback = new WaitCallback(ServerSessionPreambleConnectionReader.ReadCallback);
						}
						if (base.Connection.BeginRead(0, this.connectionBuffer.Length, this.GetRemainingTimeout(), ServerSessionPreambleConnectionReader.readCallback, this) == AsyncCompletionResult.Queued)
						{
							goto IL_F5;
						}
						this.GetReadResult();
					}
					int num = this.decoder.Decode(this.connectionBuffer, this.offset, this.size);
					if (num > 0)
					{
						this.offset += num;
						this.size -= num;
					}
				}
				while (this.decoder.CurrentState != ServerSessionDecoder.State.PreUpgradeStart);
				if (ServerSessionPreambleConnectionReader.onValidate == null)
				{
					ServerSessionPreambleConnectionReader.onValidate = Fx.ThunkCallback(new AsyncCallback(ServerSessionPreambleConnectionReader.OnValidate));
				}
				this.via = this.decoder.Via;
				IAsyncResult asyncResult = base.Connection.BeginValidate(this.via, ServerSessionPreambleConnectionReader.onValidate, this);
				if (asyncResult.CompletedSynchronously && !this.VerifyValidationResult(asyncResult))
				{
					return;
				}
				IL_F5:
				flag = true;
			}
			catch (CommunicationException exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
			}
			catch (TimeoutException ex)
			{
				if (TD.ReceiveTimeoutIsEnabled())
				{
					TD.ReceiveTimeout(ex.Message);
				}
				DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
			}
			catch (Exception exception2)
			{
				if (Fx.IsFatal(exception2))
				{
					throw;
				}
				if (!ExceptionHandler.HandleTransportExceptionHelper(exception2))
				{
					throw;
				}
			}
			finally
			{
				if (!flag)
				{
					base.Abort();
				}
			}
		}

		// Token: 0x06004D49 RID: 19785 RVA: 0x0011A248 File Offset: 0x00118448
		private bool VerifyValidationResult(IAsyncResult result)
		{
			return base.Connection.EndValidate(result) && this.ContinuePostValidationProcessing();
		}

		// Token: 0x06004D4A RID: 19786 RVA: 0x0011A260 File Offset: 0x00118460
		private static void OnValidate(IAsyncResult result)
		{
			bool flag = false;
			ServerSessionPreambleConnectionReader serverSessionPreambleConnectionReader = (ServerSessionPreambleConnectionReader)result.AsyncState;
			try
			{
				if (result.CompletedSynchronously || serverSessionPreambleConnectionReader.VerifyValidationResult(result))
				{
					flag = true;
				}
			}
			catch (CommunicationException exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
			}
			catch (TimeoutException ex)
			{
				if (TD.ReceiveTimeoutIsEnabled())
				{
					TD.ReceiveTimeout(ex.Message);
				}
				DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
			}
			catch (Exception exception2)
			{
				if (Fx.IsFatal(exception2))
				{
					throw;
				}
				DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
			}
			finally
			{
				if (!flag)
				{
					serverSessionPreambleConnectionReader.Abort();
				}
			}
		}

		// Token: 0x06004D4B RID: 19787 RVA: 0x0011A310 File Offset: 0x00118510
		private bool ContinuePostValidationProcessing()
		{
			if (this.viaDelegate != null)
			{
				try
				{
					this.viaDelegate(this.via);
				}
				catch (ServiceActivationException exception)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
					this.SendFault("http://schemas.microsoft.com/ws/2006/05/framing/faults/ServiceActivationFailed");
					return true;
				}
			}
			this.settings = this.transportSettingsCallback(this.via);
			if (this.settings == null)
			{
				EndpointNotFoundException exception2 = new EndpointNotFoundException(SR.GetString("EndpointNotFound", new object[]
				{
					this.decoder.Via
				}));
				DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
				this.SendFault("http://schemas.microsoft.com/ws/2006/05/framing/faults/EndpointNotFound");
				return false;
			}
			this.callback(this);
			return true;
		}

		// Token: 0x06004D4C RID: 19788 RVA: 0x0011A3C8 File Offset: 0x001185C8
		public void SendFault(string faultString)
		{
			InitialServerConnectionReader.SendFault(base.Connection, faultString, this.connectionBuffer, this.GetRemainingTimeout(), 65536);
			base.Close(this.GetRemainingTimeout());
		}

		// Token: 0x06004D4D RID: 19789 RVA: 0x0011A3F3 File Offset: 0x001185F3
		public void StartReading(Action<Uri> viaDelegate, TimeSpan receiveTimeout)
		{
			this.viaDelegate = viaDelegate;
			this.receiveTimeoutHelper = new TimeoutHelper(receiveTimeout);
			this.connectionBuffer = base.Connection.AsyncReadBuffer;
			this.ContinueReading();
		}

		// Token: 0x06004D4E RID: 19790 RVA: 0x0011A41F File Offset: 0x0011861F
		public IDuplexSessionChannel CreateDuplexSessionChannel(ConnectionOrientedTransportChannelListener channelListener, EndpointAddress localAddress, bool exposeConnectionProperty, ConnectionDemuxer connectionDemuxer)
		{
			return new ServerSessionPreambleConnectionReader.ServerFramingDuplexSessionChannel(channelListener, this, localAddress, exposeConnectionProperty, connectionDemuxer);
		}

		// Token: 0x0400304F RID: 12367
		private ServerSessionDecoder decoder;

		// Token: 0x04003050 RID: 12368
		private byte[] connectionBuffer;

		// Token: 0x04003051 RID: 12369
		private int offset;

		// Token: 0x04003052 RID: 12370
		private int size;

		// Token: 0x04003053 RID: 12371
		private TransportSettingsCallback transportSettingsCallback;

		// Token: 0x04003054 RID: 12372
		private ServerSessionPreambleCallback callback;

		// Token: 0x04003055 RID: 12373
		private static WaitCallback readCallback;

		// Token: 0x04003056 RID: 12374
		private IConnectionOrientedTransportFactorySettings settings;

		// Token: 0x04003057 RID: 12375
		private Uri via;

		// Token: 0x04003058 RID: 12376
		private Action<Uri> viaDelegate;

		// Token: 0x04003059 RID: 12377
		private TimeoutHelper receiveTimeoutHelper;

		// Token: 0x0400305A RID: 12378
		private IConnection rawConnection;

		// Token: 0x0400305B RID: 12379
		private static AsyncCallback onValidate;

		// Token: 0x02000D15 RID: 3349
		private class ServerFramingDuplexSessionChannel : FramingDuplexSessionChannel
		{
			// Token: 0x06007B37 RID: 31543 RVA: 0x001CB348 File Offset: 0x001C9548
			public ServerFramingDuplexSessionChannel(ConnectionOrientedTransportChannelListener channelListener, ServerSessionPreambleConnectionReader preambleReader, EndpointAddress localAddress, bool exposeConnectionProperty, ConnectionDemuxer connectionDemuxer) : base(channelListener, localAddress, preambleReader.Via, exposeConnectionProperty)
			{
				this.channelListener = channelListener;
				this.connectionDemuxer = connectionDemuxer;
				base.Connection = preambleReader.Connection;
				this.decoder = preambleReader.Decoder;
				this.connectionBuffer = preambleReader.connectionBuffer;
				this.offset = preambleReader.BufferOffset;
				this.size = preambleReader.BufferSize;
				this.rawConnection = preambleReader.RawConnection;
				StreamUpgradeProvider upgrade = channelListener.Upgrade;
				if (upgrade != null)
				{
					this.channelBindingProvider = upgrade.GetProperty<IStreamUpgradeChannelBindingProvider>();
					this.upgradeAcceptor = upgrade.CreateUpgradeAcceptor();
				}
			}

			// Token: 0x06007B38 RID: 31544 RVA: 0x001CB3E0 File Offset: 0x001C95E0
			protected override void ReturnConnectionIfNecessary(bool abort, TimeSpan timeout)
			{
				IConnection connection = null;
				if (this.sessionReader != null)
				{
					object thisLock = base.ThisLock;
					lock (thisLock)
					{
						connection = this.sessionReader.GetRawConnection();
					}
				}
				if (connection != null)
				{
					if (abort)
					{
						connection.Abort();
					}
					else
					{
						this.connectionDemuxer.ReuseConnection(connection, timeout);
					}
					this.connectionDemuxer = null;
				}
			}

			// Token: 0x06007B39 RID: 31545 RVA: 0x001CB454 File Offset: 0x001C9654
			public override T GetProperty<T>()
			{
				if (typeof(T) == typeof(IChannelBindingProvider))
				{
					return (T)((object)this.channelBindingProvider);
				}
				return base.GetProperty<T>();
			}

			// Token: 0x06007B3A RID: 31546 RVA: 0x001CB483 File Offset: 0x001C9683
			protected override void PrepareMessage(Message message)
			{
				this.channelListener.RaiseMessageReceived();
				base.PrepareMessage(message);
			}

			// Token: 0x06007B3B RID: 31547 RVA: 0x001CB498 File Offset: 0x001C9698
			protected override void OnOpen(TimeSpan timeout)
			{
				bool flag = false;
				try
				{
					TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
					this.ValidateContentType(ref timeoutHelper);
					for (;;)
					{
						if (this.size == 0)
						{
							this.offset = 0;
							this.size = base.Connection.Read(this.connectionBuffer, 0, this.connectionBuffer.Length, timeoutHelper.RemainingTime());
							if (this.size == 0)
							{
								break;
							}
						}
						do
						{
							this.DecodeBytes();
							ServerSessionDecoder.State currentState = this.decoder.CurrentState;
							if (currentState == ServerSessionDecoder.State.UpgradeRequest)
							{
								this.ProcessUpgradeRequest(ref timeoutHelper);
								base.Connection.Write(ServerSessionEncoder.UpgradeResponseBytes, 0, ServerSessionEncoder.UpgradeResponseBytes.Length, true, timeoutHelper.RemainingTime());
								IConnection connection = base.Connection;
								if (this.size > 0)
								{
									connection = new PreReadConnection(connection, this.connectionBuffer, this.offset, this.size);
								}
								try
								{
									base.Connection = InitialServerConnectionReader.UpgradeConnection(connection, this.upgradeAcceptor, timeoutHelper.RemainingTime(), this);
									if (this.channelBindingProvider != null && this.channelBindingProvider.IsChannelBindingSupportEnabled)
									{
										base.SetChannelBinding(this.channelBindingProvider.GetChannelBinding(this.upgradeAcceptor, ChannelBindingKind.Endpoint));
									}
									this.connectionBuffer = base.Connection.AsyncReadBuffer;
									goto IL_188;
								}
								catch (Exception exception)
								{
									if (Fx.IsFatal(exception))
									{
										throw;
									}
									this.WriteAuditFailure(this.upgradeAcceptor as StreamSecurityUpgradeAcceptor, exception);
									throw;
								}
								goto IL_158;
							}
							if (currentState == ServerSessionDecoder.State.Start)
							{
								goto IL_158;
							}
							IL_188:;
						}
						while (this.size != 0);
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.decoder.CreatePrematureEOFException());
					IL_158:
					this.SetupSecurityIfNecessary();
					base.Connection.Write(ServerSessionEncoder.AckResponseBytes, 0, ServerSessionEncoder.AckResponseBytes.Length, true, timeoutHelper.RemainingTime());
					this.SetupSessionReader();
					flag = true;
				}
				finally
				{
					if (!flag)
					{
						base.Connection.Abort();
					}
				}
			}

			// Token: 0x06007B3C RID: 31548 RVA: 0x001CB680 File Offset: 0x001C9880
			private void AcceptUpgradedConnection(IConnection upgradedConnection)
			{
				base.Connection = upgradedConnection;
				if (this.channelBindingProvider != null && this.channelBindingProvider.IsChannelBindingSupportEnabled)
				{
					base.SetChannelBinding(this.channelBindingProvider.GetChannelBinding(this.upgradeAcceptor, ChannelBindingKind.Endpoint));
				}
				this.connectionBuffer = base.Connection.AsyncReadBuffer;
			}

			// Token: 0x06007B3D RID: 31549 RVA: 0x001CB6D4 File Offset: 0x001C98D4
			private void ValidateContentType(ref TimeoutHelper timeoutHelper)
			{
				base.MessageEncoder = this.channelListener.MessageEncoderFactory.CreateSessionEncoder();
				if (!base.MessageEncoder.IsContentTypeSupported(this.decoder.ContentType))
				{
					this.SendFault("http://schemas.microsoft.com/ws/2006/05/framing/faults/ContentTypeInvalid", ref timeoutHelper);
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("ContentTypeMismatch", new object[]
					{
						this.decoder.ContentType,
						base.MessageEncoder.ContentType
					})));
				}
				ICompressedMessageEncoder compressedMessageEncoder = base.MessageEncoder as ICompressedMessageEncoder;
				if (compressedMessageEncoder != null && compressedMessageEncoder.CompressionEnabled)
				{
					compressedMessageEncoder.SetSessionContentType(this.decoder.ContentType);
				}
			}

			// Token: 0x06007B3E RID: 31550 RVA: 0x001CB780 File Offset: 0x001C9980
			private void DecodeBytes()
			{
				int num = this.decoder.Decode(this.connectionBuffer, this.offset, this.size);
				if (num > 0)
				{
					this.offset += num;
					this.size -= num;
				}
			}

			// Token: 0x06007B3F RID: 31551 RVA: 0x001CB7CC File Offset: 0x001C99CC
			private void ProcessUpgradeRequest(ref TimeoutHelper timeoutHelper)
			{
				if (this.upgradeAcceptor == null)
				{
					this.SendFault("http://schemas.microsoft.com/ws/2006/05/framing/faults/UpgradeInvalid", ref timeoutHelper);
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("UpgradeRequestToNonupgradableService", new object[]
					{
						this.decoder.Upgrade
					})));
				}
				if (!this.upgradeAcceptor.CanUpgrade(this.decoder.Upgrade))
				{
					this.SendFault("http://schemas.microsoft.com/ws/2006/05/framing/faults/UpgradeInvalid", ref timeoutHelper);
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("UpgradeProtocolNotSupported", new object[]
					{
						this.decoder.Upgrade
					})));
				}
			}

			// Token: 0x06007B40 RID: 31552 RVA: 0x001CB86D File Offset: 0x001C9A6D
			private void SendFault(string faultString, ref TimeoutHelper timeoutHelper)
			{
				InitialServerConnectionReader.SendFault(base.Connection, faultString, this.connectionBuffer, timeoutHelper.RemainingTime(), 65536);
			}

			// Token: 0x06007B41 RID: 31553 RVA: 0x001CB88C File Offset: 0x001C9A8C
			private void SetupSecurityIfNecessary()
			{
				StreamSecurityUpgradeAcceptor streamSecurityUpgradeAcceptor = this.upgradeAcceptor as StreamSecurityUpgradeAcceptor;
				if (streamSecurityUpgradeAcceptor != null)
				{
					base.RemoteSecurity = streamSecurityUpgradeAcceptor.GetRemoteSecurity();
					if (base.RemoteSecurity == null)
					{
						Exception exception = new ProtocolException(SR.GetString("RemoteSecurityNotNegotiatedOnStreamUpgrade", new object[]
						{
							this.Via
						}));
						this.WriteAuditFailure(streamSecurityUpgradeAcceptor, exception);
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
					}
					this.WriteAuditEvent(streamSecurityUpgradeAcceptor, AuditLevel.Success, null);
				}
			}

			// Token: 0x06007B42 RID: 31554 RVA: 0x001CB8F8 File Offset: 0x001C9AF8
			private void SetupSessionReader()
			{
				this.sessionReader = new ServerSessionPreambleConnectionReader.ServerFramingDuplexSessionChannel.ServerSessionConnectionReader(this);
				base.SetMessageSource(this.sessionReader);
			}

			// Token: 0x06007B43 RID: 31555 RVA: 0x001CB912 File Offset: 0x001C9B12
			protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new ServerSessionPreambleConnectionReader.ServerFramingDuplexSessionChannel.OpenAsyncResult(this, timeout, callback, state);
			}

			// Token: 0x06007B44 RID: 31556 RVA: 0x001CB91D File Offset: 0x001C9B1D
			protected override void OnEndOpen(IAsyncResult result)
			{
				ServerSessionPreambleConnectionReader.ServerFramingDuplexSessionChannel.OpenAsyncResult.End(result);
			}

			// Token: 0x06007B45 RID: 31557 RVA: 0x001CB928 File Offset: 0x001C9B28
			private void WriteAuditFailure(StreamSecurityUpgradeAcceptor securityUpgradeAcceptor, Exception exception)
			{
				try
				{
					this.WriteAuditEvent(securityUpgradeAcceptor, AuditLevel.Failure, exception);
				}
				catch (Exception exception2)
				{
					if (Fx.IsFatal(exception2))
					{
						throw;
					}
					DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Error);
				}
			}

			// Token: 0x06007B46 RID: 31558 RVA: 0x001CB964 File Offset: 0x001C9B64
			private void WriteAuditEvent(StreamSecurityUpgradeAcceptor securityUpgradeAcceptor, AuditLevel auditLevel, Exception exception)
			{
				if ((this.channelListener.AuditBehavior.MessageAuthenticationAuditLevel & auditLevel) != auditLevel)
				{
					return;
				}
				if (securityUpgradeAcceptor == null)
				{
					return;
				}
				string clientIdentity = string.Empty;
				SecurityMessageProperty remoteSecurity = securityUpgradeAcceptor.GetRemoteSecurity();
				if (remoteSecurity != null)
				{
					clientIdentity = ServerSessionPreambleConnectionReader.ServerFramingDuplexSessionChannel.GetIdentityNameFromContext(remoteSecurity);
				}
				ServiceSecurityAuditBehavior auditBehavior = this.channelListener.AuditBehavior;
				if (auditLevel == AuditLevel.Success)
				{
					SecurityAuditHelper.WriteTransportAuthenticationSuccessEvent(auditBehavior.AuditLogLocation, auditBehavior.SuppressAuditFailure, null, base.LocalVia, clientIdentity);
					return;
				}
				SecurityAuditHelper.WriteTransportAuthenticationFailureEvent(auditBehavior.AuditLogLocation, auditBehavior.SuppressAuditFailure, null, base.LocalVia, clientIdentity, exception);
			}

			// Token: 0x06007B47 RID: 31559 RVA: 0x001CB9E6 File Offset: 0x001C9BE6
			[MethodImpl(MethodImplOptions.NoInlining)]
			private static string GetIdentityNameFromContext(SecurityMessageProperty clientSecurity)
			{
				return SecurityUtils.GetIdentityNamesFromContext(clientSecurity.ServiceSecurityContext.AuthorizationContext);
			}

			// Token: 0x040046B4 RID: 18100
			private ConnectionOrientedTransportChannelListener channelListener;

			// Token: 0x040046B5 RID: 18101
			private ConnectionDemuxer connectionDemuxer;

			// Token: 0x040046B6 RID: 18102
			private ServerSessionPreambleConnectionReader.ServerFramingDuplexSessionChannel.ServerSessionConnectionReader sessionReader;

			// Token: 0x040046B7 RID: 18103
			private ServerSessionDecoder decoder;

			// Token: 0x040046B8 RID: 18104
			private IConnection rawConnection;

			// Token: 0x040046B9 RID: 18105
			private byte[] connectionBuffer;

			// Token: 0x040046BA RID: 18106
			private int offset;

			// Token: 0x040046BB RID: 18107
			private int size;

			// Token: 0x040046BC RID: 18108
			private StreamUpgradeAcceptor upgradeAcceptor;

			// Token: 0x040046BD RID: 18109
			private IStreamUpgradeChannelBindingProvider channelBindingProvider;

			// Token: 0x02000F46 RID: 3910
			private class OpenAsyncResult : AsyncResult
			{
				// Token: 0x060086D1 RID: 34513 RVA: 0x001F3810 File Offset: 0x001F1A10
				public OpenAsyncResult(ServerSessionPreambleConnectionReader.ServerFramingDuplexSessionChannel channel, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
				{
					this.channel = channel;
					this.timeoutHelper = new TimeoutHelper(timeout);
					bool flag = false;
					bool flag2 = false;
					try
					{
						channel.ValidateContentType(ref this.timeoutHelper);
						flag = this.ContinueReading();
						flag2 = true;
					}
					finally
					{
						if (!flag2)
						{
							this.CleanupOnError();
						}
					}
					if (flag)
					{
						base.Complete(true);
					}
				}

				// Token: 0x060086D2 RID: 34514 RVA: 0x001F3878 File Offset: 0x001F1A78
				public static void End(IAsyncResult result)
				{
					AsyncResult.End<ServerSessionPreambleConnectionReader.ServerFramingDuplexSessionChannel.OpenAsyncResult>(result);
				}

				// Token: 0x060086D3 RID: 34515 RVA: 0x001F3881 File Offset: 0x001F1A81
				private void CleanupOnError()
				{
					this.channel.Connection.Abort();
				}

				// Token: 0x060086D4 RID: 34516 RVA: 0x001F3894 File Offset: 0x001F1A94
				private bool ContinueReading()
				{
					for (;;)
					{
						if (this.channel.size == 0)
						{
							if (ServerSessionPreambleConnectionReader.ServerFramingDuplexSessionChannel.OpenAsyncResult.readCallback == null)
							{
								ServerSessionPreambleConnectionReader.ServerFramingDuplexSessionChannel.OpenAsyncResult.readCallback = new WaitCallback(ServerSessionPreambleConnectionReader.ServerFramingDuplexSessionChannel.OpenAsyncResult.ReadCallback);
							}
							if (this.channel.Connection.BeginRead(0, this.channel.connectionBuffer.Length, this.timeoutHelper.RemainingTime(), ServerSessionPreambleConnectionReader.ServerFramingDuplexSessionChannel.OpenAsyncResult.readCallback, this) == AsyncCompletionResult.Queued)
							{
								break;
							}
							this.GetReadResult();
						}
						do
						{
							this.channel.DecodeBytes();
							ServerSessionDecoder.State currentState = this.channel.decoder.CurrentState;
							if (currentState != ServerSessionDecoder.State.UpgradeRequest)
							{
								if (currentState == ServerSessionDecoder.State.Start)
								{
									goto IL_F5;
								}
							}
							else
							{
								this.channel.ProcessUpgradeRequest(ref this.timeoutHelper);
								if (ServerSessionPreambleConnectionReader.ServerFramingDuplexSessionChannel.OpenAsyncResult.onWriteUpgradeResponse == null)
								{
									ServerSessionPreambleConnectionReader.ServerFramingDuplexSessionChannel.OpenAsyncResult.onWriteUpgradeResponse = Fx.ThunkCallback(new WaitCallback(ServerSessionPreambleConnectionReader.ServerFramingDuplexSessionChannel.OpenAsyncResult.OnWriteUpgradeResponse));
								}
								if (this.channel.Connection.BeginWrite(ServerSessionEncoder.UpgradeResponseBytes, 0, ServerSessionEncoder.UpgradeResponseBytes.Length, true, this.timeoutHelper.RemainingTime(), ServerSessionPreambleConnectionReader.ServerFramingDuplexSessionChannel.OpenAsyncResult.onWriteUpgradeResponse, this) == AsyncCompletionResult.Queued)
								{
									return false;
								}
								if (!this.HandleWriteUpgradeResponseComplete())
								{
									return false;
								}
							}
						}
						while (this.channel.size != 0);
					}
					return false;
					IL_F5:
					this.channel.SetupSecurityIfNecessary();
					if (ServerSessionPreambleConnectionReader.ServerFramingDuplexSessionChannel.OpenAsyncResult.onWriteAckResponse == null)
					{
						ServerSessionPreambleConnectionReader.ServerFramingDuplexSessionChannel.OpenAsyncResult.onWriteAckResponse = Fx.ThunkCallback(new WaitCallback(ServerSessionPreambleConnectionReader.ServerFramingDuplexSessionChannel.OpenAsyncResult.OnWriteAckResponse));
					}
					return this.channel.Connection.BeginWrite(ServerSessionEncoder.AckResponseBytes, 0, ServerSessionEncoder.AckResponseBytes.Length, true, this.timeoutHelper.RemainingTime(), ServerSessionPreambleConnectionReader.ServerFramingDuplexSessionChannel.OpenAsyncResult.onWriteAckResponse, this) != AsyncCompletionResult.Queued && this.HandleWriteAckComplete();
				}

				// Token: 0x060086D5 RID: 34517 RVA: 0x001F3A10 File Offset: 0x001F1C10
				private void GetReadResult()
				{
					this.channel.offset = 0;
					this.channel.size = this.channel.Connection.EndRead();
					if (this.channel.size == 0)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.channel.decoder.CreatePrematureEOFException());
					}
				}

				// Token: 0x060086D6 RID: 34518 RVA: 0x001F3A6C File Offset: 0x001F1C6C
				private bool HandleWriteUpgradeResponseComplete()
				{
					this.channel.Connection.EndWrite();
					IConnection connection = this.channel.Connection;
					if (this.channel.size > 0)
					{
						connection = new PreReadConnection(connection, this.channel.connectionBuffer, this.channel.offset, this.channel.size);
					}
					if (ServerSessionPreambleConnectionReader.ServerFramingDuplexSessionChannel.OpenAsyncResult.onUpgradeConnection == null)
					{
						ServerSessionPreambleConnectionReader.ServerFramingDuplexSessionChannel.OpenAsyncResult.onUpgradeConnection = Fx.ThunkCallback(new AsyncCallback(ServerSessionPreambleConnectionReader.ServerFramingDuplexSessionChannel.OpenAsyncResult.OnUpgradeConnection));
					}
					bool result;
					try
					{
						IAsyncResult asyncResult = InitialServerConnectionReader.BeginUpgradeConnection(connection, this.channel.upgradeAcceptor, this.channel, this.timeoutHelper.RemainingTime(), ServerSessionPreambleConnectionReader.ServerFramingDuplexSessionChannel.OpenAsyncResult.onUpgradeConnection, this);
						if (!asyncResult.CompletedSynchronously)
						{
							result = false;
						}
						else
						{
							result = this.HandleUpgradeConnectionComplete(asyncResult);
						}
					}
					catch (Exception exception)
					{
						if (Fx.IsFatal(exception))
						{
							throw;
						}
						this.channel.WriteAuditFailure(this.channel.upgradeAcceptor as StreamSecurityUpgradeAcceptor, exception);
						throw;
					}
					return result;
				}

				// Token: 0x060086D7 RID: 34519 RVA: 0x001F3B64 File Offset: 0x001F1D64
				private bool HandleUpgradeConnectionComplete(IAsyncResult result)
				{
					this.channel.AcceptUpgradedConnection(InitialServerConnectionReader.EndUpgradeConnection(result));
					return true;
				}

				// Token: 0x060086D8 RID: 34520 RVA: 0x001F3B78 File Offset: 0x001F1D78
				private bool HandleWriteAckComplete()
				{
					this.channel.Connection.EndWrite();
					this.channel.SetupSessionReader();
					return true;
				}

				// Token: 0x060086D9 RID: 34521 RVA: 0x001F3B98 File Offset: 0x001F1D98
				private static void ReadCallback(object state)
				{
					ServerSessionPreambleConnectionReader.ServerFramingDuplexSessionChannel.OpenAsyncResult openAsyncResult = (ServerSessionPreambleConnectionReader.ServerFramingDuplexSessionChannel.OpenAsyncResult)state;
					bool flag = false;
					Exception exception = null;
					try
					{
						openAsyncResult.GetReadResult();
						flag = openAsyncResult.ContinueReading();
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						flag = true;
						exception = ex;
						openAsyncResult.CleanupOnError();
					}
					if (flag)
					{
						openAsyncResult.Complete(false, exception);
					}
				}

				// Token: 0x060086DA RID: 34522 RVA: 0x001F3BF4 File Offset: 0x001F1DF4
				private static void OnWriteUpgradeResponse(object asyncState)
				{
					ServerSessionPreambleConnectionReader.ServerFramingDuplexSessionChannel.OpenAsyncResult openAsyncResult = (ServerSessionPreambleConnectionReader.ServerFramingDuplexSessionChannel.OpenAsyncResult)asyncState;
					bool flag = false;
					Exception exception = null;
					try
					{
						flag = openAsyncResult.HandleWriteUpgradeResponseComplete();
						if (flag)
						{
							flag = openAsyncResult.ContinueReading();
						}
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						exception = ex;
						flag = true;
						openAsyncResult.CleanupOnError();
						openAsyncResult.channel.WriteAuditFailure(openAsyncResult.channel.upgradeAcceptor as StreamSecurityUpgradeAcceptor, ex);
					}
					if (flag)
					{
						openAsyncResult.Complete(false, exception);
					}
				}

				// Token: 0x060086DB RID: 34523 RVA: 0x001F3C70 File Offset: 0x001F1E70
				private static void OnUpgradeConnection(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					ServerSessionPreambleConnectionReader.ServerFramingDuplexSessionChannel.OpenAsyncResult openAsyncResult = (ServerSessionPreambleConnectionReader.ServerFramingDuplexSessionChannel.OpenAsyncResult)result.AsyncState;
					bool flag = false;
					Exception exception = null;
					try
					{
						flag = openAsyncResult.HandleUpgradeConnectionComplete(result);
						if (flag)
						{
							flag = openAsyncResult.ContinueReading();
						}
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						exception = ex;
						flag = true;
						openAsyncResult.CleanupOnError();
						openAsyncResult.channel.WriteAuditFailure(openAsyncResult.channel.upgradeAcceptor as StreamSecurityUpgradeAcceptor, ex);
					}
					if (flag)
					{
						openAsyncResult.Complete(false, exception);
					}
				}

				// Token: 0x060086DC RID: 34524 RVA: 0x001F3CF8 File Offset: 0x001F1EF8
				private static void OnWriteAckResponse(object asyncState)
				{
					ServerSessionPreambleConnectionReader.ServerFramingDuplexSessionChannel.OpenAsyncResult openAsyncResult = (ServerSessionPreambleConnectionReader.ServerFramingDuplexSessionChannel.OpenAsyncResult)asyncState;
					bool flag = false;
					Exception exception = null;
					try
					{
						flag = openAsyncResult.HandleWriteAckComplete();
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						exception = ex;
						flag = true;
						openAsyncResult.CleanupOnError();
					}
					if (flag)
					{
						openAsyncResult.Complete(false, exception);
					}
				}

				// Token: 0x04004E55 RID: 20053
				private ServerSessionPreambleConnectionReader.ServerFramingDuplexSessionChannel channel;

				// Token: 0x04004E56 RID: 20054
				private TimeoutHelper timeoutHelper;

				// Token: 0x04004E57 RID: 20055
				private static WaitCallback readCallback;

				// Token: 0x04004E58 RID: 20056
				private static WaitCallback onWriteAckResponse;

				// Token: 0x04004E59 RID: 20057
				private static WaitCallback onWriteUpgradeResponse;

				// Token: 0x04004E5A RID: 20058
				private static AsyncCallback onUpgradeConnection;
			}

			// Token: 0x02000F47 RID: 3911
			private class ServerSessionConnectionReader : SessionConnectionReader
			{
				// Token: 0x060086DD RID: 34525 RVA: 0x001F3D4C File Offset: 0x001F1F4C
				public ServerSessionConnectionReader(ServerSessionPreambleConnectionReader.ServerFramingDuplexSessionChannel channel) : base(channel.Connection, channel.rawConnection, channel.offset, channel.size, channel.RemoteSecurity)
				{
					this.decoder = channel.decoder;
					this.contentType = this.decoder.ContentType;
					this.maxBufferSize = channel.channelListener.MaxBufferSize;
					this.bufferManager = channel.channelListener.BufferManager;
					this.messageEncoder = channel.MessageEncoder;
					this.rawConnection = channel.rawConnection;
				}

				// Token: 0x060086DE RID: 34526 RVA: 0x001F3DD4 File Offset: 0x001F1FD4
				protected override void EnsureDecoderAtEof()
				{
					if (this.decoder.CurrentState != ServerSessionDecoder.State.End && this.decoder.CurrentState != ServerSessionDecoder.State.EnvelopeEnd)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.decoder.CreatePrematureEOFException());
					}
				}

				// Token: 0x060086DF RID: 34527 RVA: 0x001F3E0C File Offset: 0x001F200C
				protected override Message DecodeMessage(byte[] buffer, ref int offset, ref int size, ref bool isAtEof, TimeSpan timeout)
				{
					while (!isAtEof && size > 0)
					{
						int num = this.decoder.Decode(buffer, offset, size);
						if (num > 0)
						{
							if (base.EnvelopeBuffer != null)
							{
								if (buffer != base.EnvelopeBuffer)
								{
									Buffer.BlockCopy(buffer, offset, base.EnvelopeBuffer, base.EnvelopeOffset, num);
								}
								base.EnvelopeOffset += num;
							}
							offset += num;
							size -= num;
						}
						switch (this.decoder.CurrentState)
						{
						case ServerSessionDecoder.State.EnvelopeStart:
						{
							int envelopeSize = this.decoder.EnvelopeSize;
							if (envelopeSize > this.maxBufferSize)
							{
								base.SendFault("http://schemas.microsoft.com/ws/2006/05/framing/faults/MaxMessageSizeExceededFault", timeout);
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(MaxMessageSizeStream.CreateMaxReceivedMessageSizeExceededException((long)this.maxBufferSize));
							}
							base.EnvelopeBuffer = this.bufferManager.TakeBuffer(envelopeSize);
							base.EnvelopeOffset = 0;
							base.EnvelopeSize = envelopeSize;
							continue;
						}
						case ServerSessionDecoder.State.ReadingEnvelopeBytes:
						case ServerSessionDecoder.State.ReadingEndRecord:
							continue;
						case ServerSessionDecoder.State.EnvelopeEnd:
							if (base.EnvelopeBuffer != null)
							{
								using (ServiceModelActivity serviceModelActivity = DiagnosticUtility.ShouldUseActivity ? ServiceModelActivity.CreateBoundedActivity(true) : null)
								{
									if (DiagnosticUtility.ShouldUseActivity)
									{
										ServiceModelActivity.Start(serviceModelActivity, SR.GetString("ActivityProcessingMessage", new object[]
										{
											TraceUtility.RetrieveMessageNumber()
										}), ActivityType.ProcessMessage);
									}
									Message message = null;
									try
									{
										message = this.messageEncoder.ReadMessage(new ArraySegment<byte>(base.EnvelopeBuffer, 0, base.EnvelopeSize), this.bufferManager, this.contentType);
									}
									catch (XmlException innerException)
									{
										throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("MessageXmlProtocolError"), innerException));
									}
									if (DiagnosticUtility.ShouldUseActivity)
									{
										TraceUtility.TransferFromTransport(message);
									}
									base.EnvelopeBuffer = null;
									return message;
								}
								break;
							}
							continue;
						case ServerSessionDecoder.State.End:
							break;
						default:
							continue;
						}
						isAtEof = true;
					}
					return null;
				}

				// Token: 0x060086E0 RID: 34528 RVA: 0x001F3FEC File Offset: 0x001F21EC
				protected override void PrepareMessage(Message message)
				{
					base.PrepareMessage(message);
					IPEndPoint remoteIPEndPoint = this.rawConnection.RemoteIPEndPoint;
					if (remoteIPEndPoint != null)
					{
						RemoteEndpointMessageProperty property = new RemoteEndpointMessageProperty(remoteIPEndPoint);
						message.Properties.Add(RemoteEndpointMessageProperty.Name, property);
					}
				}

				// Token: 0x04004E5B RID: 20059
				private ServerSessionDecoder decoder;

				// Token: 0x04004E5C RID: 20060
				private int maxBufferSize;

				// Token: 0x04004E5D RID: 20061
				private BufferManager bufferManager;

				// Token: 0x04004E5E RID: 20062
				private MessageEncoder messageEncoder;

				// Token: 0x04004E5F RID: 20063
				private string contentType;

				// Token: 0x04004E60 RID: 20064
				private IConnection rawConnection;
			}
		}
	}
}
