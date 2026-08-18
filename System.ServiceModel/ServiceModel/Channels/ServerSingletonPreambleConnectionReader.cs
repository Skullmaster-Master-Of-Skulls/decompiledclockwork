using System;
using System.Diagnostics;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Security.Authentication.ExtendedProtection;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200081A RID: 2074
	internal class ServerSingletonPreambleConnectionReader : InitialServerConnectionReader
	{
		// Token: 0x06004D73 RID: 19827 RVA: 0x0011AD80 File Offset: 0x00118F80
		public ServerSingletonPreambleConnectionReader(IConnection connection, Action connectionDequeuedCallback, long streamPosition, int offset, int size, TransportSettingsCallback transportSettingsCallback, ConnectionClosedCallback closedCallback, ServerSingletonPreambleCallback callback) : base(connection, closedCallback)
		{
			this.decoder = new ServerSingletonDecoder(streamPosition, base.MaxViaSize, base.MaxContentTypeSize);
			this.offset = offset;
			this.size = size;
			this.callback = callback;
			this.transportSettingsCallback = transportSettingsCallback;
			this.rawConnection = connection;
			base.ConnectionDequeuedCallback = connectionDequeuedCallback;
		}

		// Token: 0x17001369 RID: 4969
		// (get) Token: 0x06004D74 RID: 19828 RVA: 0x0011ADDC File Offset: 0x00118FDC
		public ChannelBinding ChannelBinding
		{
			get
			{
				return this.channelBindingToken;
			}
		}

		// Token: 0x1700136A RID: 4970
		// (get) Token: 0x06004D75 RID: 19829 RVA: 0x0011ADE4 File Offset: 0x00118FE4
		public int BufferOffset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x1700136B RID: 4971
		// (get) Token: 0x06004D76 RID: 19830 RVA: 0x0011ADEC File Offset: 0x00118FEC
		public int BufferSize
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x1700136C RID: 4972
		// (get) Token: 0x06004D77 RID: 19831 RVA: 0x0011ADF4 File Offset: 0x00118FF4
		public ServerSingletonDecoder Decoder
		{
			get
			{
				return this.decoder;
			}
		}

		// Token: 0x1700136D RID: 4973
		// (get) Token: 0x06004D78 RID: 19832 RVA: 0x0011ADFC File Offset: 0x00118FFC
		public IConnection RawConnection
		{
			get
			{
				return this.rawConnection;
			}
		}

		// Token: 0x1700136E RID: 4974
		// (get) Token: 0x06004D79 RID: 19833 RVA: 0x0011AE04 File Offset: 0x00119004
		public Uri Via
		{
			get
			{
				return this.via;
			}
		}

		// Token: 0x1700136F RID: 4975
		// (get) Token: 0x06004D7A RID: 19834 RVA: 0x0011AE0C File Offset: 0x0011900C
		public IConnectionOrientedTransportFactorySettings TransportSettings
		{
			get
			{
				return this.transportSettings;
			}
		}

		// Token: 0x17001370 RID: 4976
		// (get) Token: 0x06004D7B RID: 19835 RVA: 0x0011AE14 File Offset: 0x00119014
		public SecurityMessageProperty Security
		{
			get
			{
				return this.security;
			}
		}

		// Token: 0x06004D7C RID: 19836 RVA: 0x0011AE1C File Offset: 0x0011901C
		private TimeSpan GetRemainingTimeout()
		{
			return this.receiveTimeoutHelper.RemainingTime();
		}

		// Token: 0x06004D7D RID: 19837 RVA: 0x0011AE2C File Offset: 0x0011902C
		private void ReadAndDispatch()
		{
			bool flag = false;
			try
			{
				while ((this.size > 0 || !this.isReadPending) && !base.IsClosed)
				{
					if (this.size == 0)
					{
						this.isReadPending = true;
						if (this.onAsyncReadComplete == null)
						{
							this.onAsyncReadComplete = new WaitCallback(this.OnAsyncReadComplete);
						}
						if (base.Connection.BeginRead(0, this.connectionBuffer.Length, this.GetRemainingTimeout(), this.onAsyncReadComplete, null) == AsyncCompletionResult.Queued)
						{
							break;
						}
						this.HandleReadComplete();
					}
					int num = this.decoder.Decode(this.connectionBuffer, this.offset, this.size);
					if (num > 0)
					{
						this.offset += num;
						this.size -= num;
					}
					if (this.decoder.CurrentState == ServerSingletonDecoder.State.PreUpgradeStart)
					{
						if (ServerSingletonPreambleConnectionReader.onValidate == null)
						{
							ServerSingletonPreambleConnectionReader.onValidate = Fx.ThunkCallback(new AsyncCallback(ServerSingletonPreambleConnectionReader.OnValidate));
						}
						this.via = this.decoder.Via;
						IAsyncResult asyncResult = base.Connection.BeginValidate(this.via, ServerSingletonPreambleConnectionReader.onValidate, this);
						if (asyncResult.CompletedSynchronously && !this.VerifyValidationResult(asyncResult))
						{
							return;
						}
						break;
					}
				}
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

		// Token: 0x06004D7E RID: 19838 RVA: 0x0011B00C File Offset: 0x0011920C
		private bool VerifyValidationResult(IAsyncResult result)
		{
			return base.Connection.EndValidate(result) && this.ContinuePostValidationProcessing();
		}

		// Token: 0x06004D7F RID: 19839 RVA: 0x0011B024 File Offset: 0x00119224
		private static void OnValidate(IAsyncResult result)
		{
			bool flag = false;
			ServerSingletonPreambleConnectionReader serverSingletonPreambleConnectionReader = (ServerSingletonPreambleConnectionReader)result.AsyncState;
			try
			{
				if (result.CompletedSynchronously || serverSingletonPreambleConnectionReader.VerifyValidationResult(result))
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
					serverSingletonPreambleConnectionReader.Abort();
				}
			}
		}

		// Token: 0x06004D80 RID: 19840 RVA: 0x0011B0D4 File Offset: 0x001192D4
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
			this.transportSettings = this.transportSettingsCallback(this.via);
			if (this.transportSettings == null)
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

		// Token: 0x06004D81 RID: 19841 RVA: 0x0011B18C File Offset: 0x0011938C
		public void SendFault(string faultString)
		{
			this.SendFault(faultString, ref this.receiveTimeoutHelper);
		}

		// Token: 0x06004D82 RID: 19842 RVA: 0x0011B19B File Offset: 0x0011939B
		private void SendFault(string faultString, ref TimeoutHelper timeoutHelper)
		{
			InitialServerConnectionReader.SendFault(base.Connection, faultString, this.connectionBuffer, timeoutHelper.RemainingTime(), 65536);
		}

		// Token: 0x06004D83 RID: 19843 RVA: 0x0011B1BA File Offset: 0x001193BA
		public IAsyncResult BeginCompletePreamble(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult(timeout, this, callback, state);
		}

		// Token: 0x06004D84 RID: 19844 RVA: 0x0011B1C5 File Offset: 0x001193C5
		public IConnection EndCompletePreamble(IAsyncResult result)
		{
			return TypedAsyncResult<IConnection>.End(result);
		}

		// Token: 0x06004D85 RID: 19845 RVA: 0x0011B1D0 File Offset: 0x001193D0
		private void SetupSecurityIfNecessary(StreamUpgradeAcceptor upgradeAcceptor)
		{
			StreamSecurityUpgradeAcceptor streamSecurityUpgradeAcceptor = upgradeAcceptor as StreamSecurityUpgradeAcceptor;
			if (streamSecurityUpgradeAcceptor != null)
			{
				this.security = streamSecurityUpgradeAcceptor.GetRemoteSecurity();
				if (this.security == null)
				{
					Exception exception = new ProtocolException(SR.GetString("RemoteSecurityNotNegotiatedOnStreamUpgrade", new object[]
					{
						this.Via
					}));
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
				}
				this.WriteAuditEvent(streamSecurityUpgradeAcceptor, AuditLevel.Success, null);
			}
		}

		// Token: 0x06004D86 RID: 19846 RVA: 0x0011B230 File Offset: 0x00119430
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

		// Token: 0x06004D87 RID: 19847 RVA: 0x0011B26C File Offset: 0x0011946C
		private void WriteAuditEvent(StreamSecurityUpgradeAcceptor securityUpgradeAcceptor, AuditLevel auditLevel, Exception exception)
		{
			if ((this.transportSettings.AuditBehavior.MessageAuthenticationAuditLevel & auditLevel) != auditLevel)
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
				clientIdentity = ServerSingletonPreambleConnectionReader.GetIdentityNameFromContext(remoteSecurity);
			}
			ServiceSecurityAuditBehavior auditBehavior = this.transportSettings.AuditBehavior;
			if (auditLevel == AuditLevel.Success)
			{
				SecurityAuditHelper.WriteTransportAuthenticationSuccessEvent(auditBehavior.AuditLogLocation, auditBehavior.SuppressAuditFailure, null, this.Via, clientIdentity);
				return;
			}
			SecurityAuditHelper.WriteTransportAuthenticationFailureEvent(auditBehavior.AuditLogLocation, auditBehavior.SuppressAuditFailure, null, this.Via, clientIdentity, exception);
		}

		// Token: 0x06004D88 RID: 19848 RVA: 0x0011B2EE File Offset: 0x001194EE
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static string GetIdentityNameFromContext(SecurityMessageProperty clientSecurity)
		{
			return SecurityUtils.GetIdentityNamesFromContext(clientSecurity.ServiceSecurityContext.AuthorizationContext);
		}

		// Token: 0x06004D89 RID: 19849 RVA: 0x0011B300 File Offset: 0x00119500
		private void HandleReadComplete()
		{
			this.offset = 0;
			this.size = base.Connection.EndRead();
			this.isReadPending = false;
			if (this.size == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.decoder.CreatePrematureEOFException());
			}
		}

		// Token: 0x06004D8A RID: 19850 RVA: 0x0011B340 File Offset: 0x00119540
		private void OnAsyncReadComplete(object state)
		{
			bool flag = false;
			try
			{
				this.HandleReadComplete();
				this.ReadAndDispatch();
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

		// Token: 0x06004D8B RID: 19851 RVA: 0x0011B3DC File Offset: 0x001195DC
		public void StartReading(Action<Uri> viaDelegate, TimeSpan timeout)
		{
			this.viaDelegate = viaDelegate;
			this.receiveTimeoutHelper = new TimeoutHelper(timeout);
			this.connectionBuffer = base.Connection.AsyncReadBuffer;
			this.ReadAndDispatch();
		}

		// Token: 0x04003073 RID: 12403
		private ServerSingletonDecoder decoder;

		// Token: 0x04003074 RID: 12404
		private ServerSingletonPreambleCallback callback;

		// Token: 0x04003075 RID: 12405
		private WaitCallback onAsyncReadComplete;

		// Token: 0x04003076 RID: 12406
		private IConnectionOrientedTransportFactorySettings transportSettings;

		// Token: 0x04003077 RID: 12407
		private TransportSettingsCallback transportSettingsCallback;

		// Token: 0x04003078 RID: 12408
		private SecurityMessageProperty security;

		// Token: 0x04003079 RID: 12409
		private Uri via;

		// Token: 0x0400307A RID: 12410
		private IConnection rawConnection;

		// Token: 0x0400307B RID: 12411
		private byte[] connectionBuffer;

		// Token: 0x0400307C RID: 12412
		private bool isReadPending;

		// Token: 0x0400307D RID: 12413
		private int offset;

		// Token: 0x0400307E RID: 12414
		private int size;

		// Token: 0x0400307F RID: 12415
		private TimeoutHelper receiveTimeoutHelper;

		// Token: 0x04003080 RID: 12416
		private Action<Uri> viaDelegate;

		// Token: 0x04003081 RID: 12417
		private ChannelBinding channelBindingToken;

		// Token: 0x04003082 RID: 12418
		private static AsyncCallback onValidate;

		// Token: 0x02000D16 RID: 3350
		private class CompletePreambleAsyncResult : TypedAsyncResult<IConnection>
		{
			// Token: 0x06007B48 RID: 31560 RVA: 0x001CB9F8 File Offset: 0x001C9BF8
			public CompletePreambleAsyncResult(TimeSpan timeout, ServerSingletonPreambleConnectionReader parent, AsyncCallback callback, object state) : base(callback, state)
			{
				this.timeoutHelper = new TimeoutHelper(timeout);
				this.parent = parent;
				this.Initialize();
				if (this.ContinueWork(null))
				{
					base.Complete(this.currentConnection, true);
				}
			}

			// Token: 0x17001BC9 RID: 7113
			// (get) Token: 0x06007B49 RID: 31561 RVA: 0x001CBA32 File Offset: 0x001C9C32
			// (set) Token: 0x06007B4A RID: 31562 RVA: 0x001CBA3F File Offset: 0x001C9C3F
			private byte[] ConnectionBuffer
			{
				get
				{
					return this.parent.connectionBuffer;
				}
				set
				{
					this.parent.connectionBuffer = value;
				}
			}

			// Token: 0x17001BCA RID: 7114
			// (get) Token: 0x06007B4B RID: 31563 RVA: 0x001CBA4D File Offset: 0x001C9C4D
			// (set) Token: 0x06007B4C RID: 31564 RVA: 0x001CBA5A File Offset: 0x001C9C5A
			private int Offset
			{
				get
				{
					return this.parent.offset;
				}
				set
				{
					this.parent.offset = value;
				}
			}

			// Token: 0x17001BCB RID: 7115
			// (get) Token: 0x06007B4D RID: 31565 RVA: 0x001CBA68 File Offset: 0x001C9C68
			// (set) Token: 0x06007B4E RID: 31566 RVA: 0x001CBA75 File Offset: 0x001C9C75
			private int Size
			{
				get
				{
					return this.parent.size;
				}
				set
				{
					this.parent.size = value;
				}
			}

			// Token: 0x17001BCC RID: 7116
			// (get) Token: 0x06007B4F RID: 31567 RVA: 0x001CBA83 File Offset: 0x001C9C83
			private bool CanReadAndDecode
			{
				get
				{
					return this.upgradeState == ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.None || this.upgradeState == ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.UpgradeComplete;
				}
			}

			// Token: 0x17001BCD RID: 7117
			// (get) Token: 0x06007B50 RID: 31568 RVA: 0x001CBA98 File Offset: 0x001C9C98
			private ServerSingletonDecoder Decoder
			{
				get
				{
					return this.parent.decoder;
				}
			}

			// Token: 0x06007B51 RID: 31569 RVA: 0x001CBAA8 File Offset: 0x001C9CA8
			private void Initialize()
			{
				if (!this.parent.transportSettings.MessageEncoderFactory.Encoder.IsContentTypeSupported(this.Decoder.ContentType))
				{
					this.SendFault("http://schemas.microsoft.com/ws/2006/05/framing/faults/ContentTypeInvalid", ref this.timeoutHelper);
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("ContentTypeMismatch", new object[]
					{
						this.Decoder.ContentType,
						this.parent.transportSettings.MessageEncoderFactory.Encoder.ContentType
					})));
				}
				this.upgrade = this.parent.transportSettings.Upgrade;
				if (this.upgrade != null)
				{
					this.channelBindingProvider = this.upgrade.GetProperty<IStreamUpgradeChannelBindingProvider>();
					this.upgradeAcceptor = this.upgrade.CreateUpgradeAcceptor();
				}
				this.currentConnection = this.parent.Connection;
			}

			// Token: 0x06007B52 RID: 31570 RVA: 0x001CBB89 File Offset: 0x001C9D89
			private void SendFault(string faultString, ref TimeoutHelper timeoutHelper)
			{
				this.parent.SendFault(faultString, ref timeoutHelper);
			}

			// Token: 0x06007B53 RID: 31571 RVA: 0x001CBB98 File Offset: 0x001C9D98
			private bool BeginRead()
			{
				this.Offset = 0;
				return this.currentConnection.BeginRead(0, this.ConnectionBuffer.Length, this.timeoutHelper.RemainingTime(), ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.onReadCompleted, this) == AsyncCompletionResult.Completed;
			}

			// Token: 0x06007B54 RID: 31572 RVA: 0x001CBBC9 File Offset: 0x001C9DC9
			private void EndRead()
			{
				this.Size = this.currentConnection.EndRead();
				if (this.Size == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.Decoder.CreatePrematureEOFException());
				}
			}

			// Token: 0x06007B55 RID: 31573 RVA: 0x001CBBFC File Offset: 0x001C9DFC
			private bool ContinueWork(IAsyncResult upgradeAsyncResult)
			{
				if (upgradeAsyncResult != null)
				{
					Fx.AssertAndThrow(this.upgradeState == ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.EndUpgrade, "upgradeAsyncResult should only be passed in from OnUpgradeComplete callback");
				}
				for (;;)
				{
					if (this.Size == 0 && this.CanReadAndDecode)
					{
						if (!this.BeginRead())
						{
							return false;
						}
						this.EndRead();
					}
					do
					{
						if (this.CanReadAndDecode)
						{
							int num = this.Decoder.Decode(this.ConnectionBuffer, this.Offset, this.Size);
							if (num > 0)
							{
								this.Offset += num;
								this.Size -= num;
							}
						}
						ServerSingletonDecoder.State currentState = this.Decoder.CurrentState;
						if (currentState != ServerSingletonDecoder.State.UpgradeRequest)
						{
							if (currentState == ServerSingletonDecoder.State.Start)
							{
								goto IL_27D;
							}
						}
						else
						{
							switch (this.upgradeState)
							{
							case ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.None:
								this.ChangeUpgradeState(ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.VerifyingUpgradeRequest);
								goto IL_2E8;
							case ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.VerifyingUpgradeRequest:
								if (this.upgradeAcceptor == null)
								{
									goto Block_10;
								}
								if (!this.upgradeAcceptor.CanUpgrade(this.Decoder.Upgrade))
								{
									goto Block_11;
								}
								this.ChangeUpgradeState(ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.WritingUpgradeAck);
								if (this.currentConnection.BeginWrite(ServerSingletonEncoder.UpgradeResponseBytes, 0, ServerSingletonEncoder.UpgradeResponseBytes.Length, true, this.timeoutHelper.RemainingTime(), ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.onWriteCompleted, this) == AsyncCompletionResult.Queued)
								{
									return false;
								}
								this.currentConnection.EndWrite();
								this.ChangeUpgradeState(ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.UpgradeAckSent);
								goto IL_2E8;
							case ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.WritingUpgradeAck:
								goto IL_2E8;
							case ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.UpgradeAckSent:
							{
								IConnection innerConnection = this.currentConnection;
								if (this.Size > 0)
								{
									innerConnection = new PreReadConnection(innerConnection, this.ConnectionBuffer, this.Offset, this.Size);
								}
								this.ChangeUpgradeState(ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.BeginUpgrade);
								goto IL_2E8;
							}
							case ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.BeginUpgrade:
								try
								{
									if (!this.BeginUpgrade(out upgradeAsyncResult))
									{
										return false;
									}
									this.ChangeUpgradeState(ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.EndUpgrade);
									goto IL_2E8;
								}
								catch (Exception exception)
								{
									if (Fx.IsFatal(exception))
									{
										throw;
									}
									this.parent.WriteAuditFailure(this.upgradeAcceptor as StreamSecurityUpgradeAcceptor, exception);
									throw;
								}
								break;
							case ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.EndUpgrade:
								break;
							case ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.UpgradeComplete:
								goto IL_274;
							default:
								goto IL_2E8;
							}
							try
							{
								this.EndUpgrade(upgradeAsyncResult);
								this.ChangeUpgradeState(ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.UpgradeComplete);
								goto IL_2E8;
							}
							catch (Exception exception2)
							{
								if (Fx.IsFatal(exception2))
								{
									throw;
								}
								this.parent.WriteAuditFailure(this.upgradeAcceptor as StreamSecurityUpgradeAcceptor, exception2);
								throw;
							}
							IL_274:
							this.ChangeUpgradeState(ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.VerifyingUpgradeRequest);
						}
						IL_2E8:;
					}
					while (this.Size != 0);
				}
				Block_10:
				this.SendFault("http://schemas.microsoft.com/ws/2006/05/framing/faults/UpgradeInvalid", ref this.timeoutHelper);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("UpgradeRequestToNonupgradableService", new object[]
				{
					this.Decoder.Upgrade
				})));
				Block_11:
				this.SendFault("http://schemas.microsoft.com/ws/2006/05/framing/faults/UpgradeInvalid", ref this.timeoutHelper);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("UpgradeProtocolNotSupported", new object[]
				{
					this.Decoder.Upgrade
				})));
				IL_27D:
				this.parent.SetupSecurityIfNecessary(this.upgradeAcceptor);
				if (this.upgradeState == ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.UpgradeComplete || this.upgradeState == ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.None)
				{
					this.ChangeUpgradeState(ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.WritingPreambleEnd);
					if (this.currentConnection.BeginWrite(ServerSessionEncoder.AckResponseBytes, 0, ServerSessionEncoder.AckResponseBytes.Length, true, this.timeoutHelper.RemainingTime(), ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.onWriteCompleted, this) == AsyncCompletionResult.Queued)
					{
						return false;
					}
					this.currentConnection.EndWrite();
					this.ChangeUpgradeState(ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.PreambleEndSent);
				}
				return true;
			}

			// Token: 0x06007B56 RID: 31574 RVA: 0x001CBF24 File Offset: 0x001CA124
			private bool BeginUpgrade(out IAsyncResult upgradeAsyncResult)
			{
				upgradeAsyncResult = InitialServerConnectionReader.BeginUpgradeConnection(this.currentConnection, this.upgradeAcceptor, this.parent.transportSettings, this.timeoutHelper.RemainingTime(), ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.onUpgradeComplete, this);
				if (!upgradeAsyncResult.CompletedSynchronously)
				{
					upgradeAsyncResult = null;
					return false;
				}
				return true;
			}

			// Token: 0x06007B57 RID: 31575 RVA: 0x001CBF64 File Offset: 0x001CA164
			private void EndUpgrade(IAsyncResult upgradeAsyncResult)
			{
				this.currentConnection = InitialServerConnectionReader.EndUpgradeConnection(upgradeAsyncResult);
				this.ConnectionBuffer = this.currentConnection.AsyncReadBuffer;
				if (this.channelBindingProvider != null && this.channelBindingProvider.IsChannelBindingSupportEnabled && this.parent.channelBindingToken == null)
				{
					this.parent.channelBindingToken = this.channelBindingProvider.GetChannelBinding(this.upgradeAcceptor, ChannelBindingKind.Endpoint);
				}
			}

			// Token: 0x06007B58 RID: 31576 RVA: 0x001CBFD0 File Offset: 0x001CA1D0
			private void ChangeUpgradeState(ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState newState)
			{
				switch (newState)
				{
				case ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.None:
					throw Fx.AssertAndThrow("Invalid State Transition: currentState=" + this.upgradeState.ToString() + ", newState=" + newState.ToString());
				case ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.VerifyingUpgradeRequest:
					if (this.upgradeState != ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.None && this.upgradeState != ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.UpgradeComplete)
					{
						throw Fx.AssertAndThrow("Invalid State Transition: currentState=" + this.upgradeState.ToString() + ", newState=" + newState.ToString());
					}
					break;
				case ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.WritingUpgradeAck:
					if (this.upgradeState != ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.VerifyingUpgradeRequest)
					{
						throw Fx.AssertAndThrow("Invalid State Transition: currentState=" + this.upgradeState.ToString() + ", newState=" + newState.ToString());
					}
					break;
				case ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.UpgradeAckSent:
					if (this.upgradeState != ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.WritingUpgradeAck)
					{
						throw Fx.AssertAndThrow("Invalid State Transition: currentState=" + this.upgradeState.ToString() + ", newState=" + newState.ToString());
					}
					break;
				case ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.BeginUpgrade:
					if (this.upgradeState != ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.UpgradeAckSent)
					{
						throw Fx.AssertAndThrow("Invalid State Transition: currentState=" + this.upgradeState.ToString() + ", newState=" + newState.ToString());
					}
					break;
				case ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.EndUpgrade:
					if (this.upgradeState != ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.BeginUpgrade)
					{
						throw Fx.AssertAndThrow("Invalid State Transition: currentState=" + this.upgradeState.ToString() + ", newState=" + newState.ToString());
					}
					break;
				case ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.UpgradeComplete:
					if (this.upgradeState != ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.EndUpgrade)
					{
						throw Fx.AssertAndThrow("Invalid State Transition: currentState=" + this.upgradeState.ToString() + ", newState=" + newState.ToString());
					}
					break;
				case ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.WritingPreambleEnd:
					if (this.upgradeState != ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.None && this.upgradeState != ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.UpgradeComplete)
					{
						throw Fx.AssertAndThrow("Invalid State Transition: currentState=" + this.upgradeState.ToString() + ", newState=" + newState.ToString());
					}
					break;
				case ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.PreambleEndSent:
					if (this.upgradeState != ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.WritingPreambleEnd)
					{
						throw Fx.AssertAndThrow("Invalid State Transition: currentState=" + this.upgradeState.ToString() + ", newState=" + newState.ToString());
					}
					break;
				default:
					throw Fx.AssertAndThrow("Unexpected Upgrade State: " + newState.ToString());
				}
				this.upgradeState = newState;
			}

			// Token: 0x06007B59 RID: 31577 RVA: 0x001CC270 File Offset: 0x001CA470
			private static void OnReadCompleted(object state)
			{
				ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult completePreambleAsyncResult = (ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult)state;
				Exception ex = null;
				bool flag = false;
				try
				{
					completePreambleAsyncResult.EndRead();
					flag = completePreambleAsyncResult.ContinueWork(null);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					ex = ex2;
					flag = true;
				}
				if (flag)
				{
					if (ex != null)
					{
						completePreambleAsyncResult.Complete(false, ex);
						return;
					}
					completePreambleAsyncResult.Complete(completePreambleAsyncResult.currentConnection, false);
				}
			}

			// Token: 0x06007B5A RID: 31578 RVA: 0x001CC2D8 File Offset: 0x001CA4D8
			private static void OnWriteCompleted(object state)
			{
				ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult completePreambleAsyncResult = (ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult)state;
				Exception ex = null;
				bool flag = false;
				try
				{
					completePreambleAsyncResult.currentConnection.EndWrite();
					ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState upgradeState = completePreambleAsyncResult.upgradeState;
					if (upgradeState != ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.WritingUpgradeAck)
					{
						if (upgradeState == ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.WritingPreambleEnd)
						{
							completePreambleAsyncResult.ChangeUpgradeState(ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.PreambleEndSent);
						}
					}
					else
					{
						completePreambleAsyncResult.ChangeUpgradeState(ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.UpgradeAckSent);
					}
					flag = completePreambleAsyncResult.ContinueWork(null);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					ex = ex2;
					flag = true;
				}
				if (flag)
				{
					if (ex != null)
					{
						completePreambleAsyncResult.Complete(false, ex);
						return;
					}
					completePreambleAsyncResult.Complete(completePreambleAsyncResult.currentConnection, false);
				}
			}

			// Token: 0x06007B5B RID: 31579 RVA: 0x001CC368 File Offset: 0x001CA568
			private static void OnUpgradeComplete(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult completePreambleAsyncResult = (ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult)result.AsyncState;
				Exception ex = null;
				bool flag = false;
				try
				{
					completePreambleAsyncResult.ChangeUpgradeState(ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState.EndUpgrade);
					flag = completePreambleAsyncResult.ContinueWork(result);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					ex = ex2;
					flag = true;
				}
				if (flag)
				{
					if (ex != null)
					{
						completePreambleAsyncResult.Complete(false, ex);
						return;
					}
					completePreambleAsyncResult.Complete(completePreambleAsyncResult.currentConnection, false);
				}
			}

			// Token: 0x040046BE RID: 18110
			private static WaitCallback onReadCompleted = new WaitCallback(ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.OnReadCompleted);

			// Token: 0x040046BF RID: 18111
			private static WaitCallback onWriteCompleted = new WaitCallback(ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.OnWriteCompleted);

			// Token: 0x040046C0 RID: 18112
			private static AsyncCallback onUpgradeComplete = Fx.ThunkCallback(new AsyncCallback(ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.OnUpgradeComplete));

			// Token: 0x040046C1 RID: 18113
			private TimeoutHelper timeoutHelper;

			// Token: 0x040046C2 RID: 18114
			private ServerSingletonPreambleConnectionReader parent;

			// Token: 0x040046C3 RID: 18115
			private StreamUpgradeAcceptor upgradeAcceptor;

			// Token: 0x040046C4 RID: 18116
			private StreamUpgradeProvider upgrade;

			// Token: 0x040046C5 RID: 18117
			private IStreamUpgradeChannelBindingProvider channelBindingProvider;

			// Token: 0x040046C6 RID: 18118
			private IConnection currentConnection;

			// Token: 0x040046C7 RID: 18119
			private ServerSingletonPreambleConnectionReader.CompletePreambleAsyncResult.UpgradeState upgradeState;

			// Token: 0x02000F48 RID: 3912
			private enum UpgradeState
			{
				// Token: 0x04004E62 RID: 20066
				None,
				// Token: 0x04004E63 RID: 20067
				VerifyingUpgradeRequest,
				// Token: 0x04004E64 RID: 20068
				WritingUpgradeAck,
				// Token: 0x04004E65 RID: 20069
				UpgradeAckSent,
				// Token: 0x04004E66 RID: 20070
				BeginUpgrade,
				// Token: 0x04004E67 RID: 20071
				EndUpgrade,
				// Token: 0x04004E68 RID: 20072
				UpgradeComplete,
				// Token: 0x04004E69 RID: 20073
				WritingPreambleEnd,
				// Token: 0x04004E6A RID: 20074
				PreambleEndSent
			}
		}
	}
}
