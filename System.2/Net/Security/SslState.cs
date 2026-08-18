using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace System.Net.Security
{
	// Token: 0x02000360 RID: 864
	internal class SslState
	{
		// Token: 0x06001F60 RID: 8032 RVA: 0x000923EB File Offset: 0x000905EB
		internal SslState(Stream innerStream, bool isHTTP, EncryptionPolicy encryptionPolicy) : this(innerStream, null, null, encryptionPolicy)
		{
			this._ForceBufferingLastHandshakePayload = isHTTP;
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x000923FE File Offset: 0x000905FE
		internal SslState(Stream innerStream, RemoteCertValidationCallback certValidationCallback, LocalCertSelectionCallback certSelectionCallback, EncryptionPolicy encryptionPolicy)
		{
			this._InnerStream = innerStream;
			this._Reader = new FixedSizeReader(innerStream);
			this._CertValidationDelegate = certValidationCallback;
			this._CertSelectionDelegate = certSelectionCallback;
			this._EncryptionPolicy = encryptionPolicy;
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x00092430 File Offset: 0x00090630
		internal void ValidateCreateContext(bool isServer, string targetHost, SslProtocols enabledSslProtocols, X509Certificate serverCertificate, X509CertificateCollection clientCertificates, bool remoteCertRequired, bool checkCertRevocationStatus)
		{
			this.ValidateCreateContext(isServer, targetHost, enabledSslProtocols, serverCertificate, clientCertificates, remoteCertRequired, checkCertRevocationStatus, !isServer);
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x00092454 File Offset: 0x00090654
		internal void ValidateCreateContext(bool isServer, string targetHost, SslProtocols enabledSslProtocols, X509Certificate serverCertificate, X509CertificateCollection clientCertificates, bool remoteCertRequired, bool checkCertRevocationStatus, bool checkCertName)
		{
			if (this._Exception != null && !this._CanRetryAuthentication)
			{
				throw this._Exception;
			}
			if (this.Context != null && this.Context.IsValidContext)
			{
				throw new InvalidOperationException(SR.GetString("net_auth_reauth"));
			}
			if (this.Context != null && this.IsServer != isServer)
			{
				throw new InvalidOperationException(SR.GetString("net_auth_client_server"));
			}
			if (targetHost == null)
			{
				throw new ArgumentNullException("targetHost");
			}
			if (isServer)
			{
				enabledSslProtocols &= (SslProtocols)1073747285;
				if (serverCertificate == null)
				{
					throw new ArgumentNullException("serverCertificate");
				}
			}
			else
			{
				enabledSslProtocols &= (SslProtocols)(-2147472726);
			}
			if (ServicePointManager.DisableSystemDefaultTlsVersions && enabledSslProtocols == SslProtocols.None)
			{
				throw new ArgumentException(SR.GetString("net_invalid_enum", new object[]
				{
					"SslProtocolType"
				}), "sslProtocolType");
			}
			if (clientCertificates == null)
			{
				clientCertificates = new X509CertificateCollection();
			}
			if (targetHost.Length == 0)
			{
				targetHost = "?" + Interlocked.Increment(ref SslState.UniqueNameInteger).ToString(NumberFormatInfo.InvariantInfo);
			}
			this._Exception = null;
			try
			{
				this._Context = new SecureChannel(targetHost, isServer, (SchProtocols)enabledSslProtocols, serverCertificate, clientCertificates, remoteCertRequired, checkCertName, checkCertRevocationStatus, this._EncryptionPolicy, this._CertSelectionDelegate);
			}
			catch (Win32Exception innerException)
			{
				throw new AuthenticationException(SR.GetString("net_auth_SSPI"), innerException);
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06001F64 RID: 8036 RVA: 0x000925A4 File Offset: 0x000907A4
		internal bool IsAuthenticated
		{
			get
			{
				return this._Context != null && this._Context.IsValidContext && this._Exception == null && this.HandshakeCompleted;
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06001F65 RID: 8037 RVA: 0x000925CB File Offset: 0x000907CB
		internal bool IsMutuallyAuthenticated
		{
			get
			{
				return this.IsAuthenticated && (this.Context.IsServer ? this.Context.LocalServerCertificate : this.Context.LocalClientCertificate) != null && this.Context.IsRemoteCertificateAvailable;
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06001F66 RID: 8038 RVA: 0x00092609 File Offset: 0x00090809
		internal bool RemoteCertRequired
		{
			get
			{
				return this.Context == null || this.Context.RemoteCertRequired;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06001F67 RID: 8039 RVA: 0x00092620 File Offset: 0x00090820
		internal bool IsServer
		{
			get
			{
				return this.Context != null && this.Context.IsServer;
			}
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x00092637 File Offset: 0x00090837
		internal void SetCertValidationDelegate(RemoteCertValidationCallback certValidationCallback)
		{
			this._CertValidationDelegate = certValidationCallback;
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06001F69 RID: 8041 RVA: 0x00092640 File Offset: 0x00090840
		internal X509Certificate LocalCertificate
		{
			get
			{
				this.CheckThrow(true, false);
				return this.InternalLocalCertificate;
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06001F6A RID: 8042 RVA: 0x00092650 File Offset: 0x00090850
		internal X509Certificate InternalLocalCertificate
		{
			get
			{
				if (!this.Context.IsServer)
				{
					return this.Context.LocalClientCertificate;
				}
				return this.Context.LocalServerCertificate;
			}
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x00092676 File Offset: 0x00090876
		internal ChannelBinding GetChannelBinding(ChannelBindingKind kind)
		{
			if (this.Context != null)
			{
				return this.Context.GetChannelBinding(kind);
			}
			return null;
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06001F6C RID: 8044 RVA: 0x0009268E File Offset: 0x0009088E
		internal bool CheckCertRevocationStatus
		{
			get
			{
				return this.Context != null && this.Context.CheckCertRevocationStatus;
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06001F6D RID: 8045 RVA: 0x000926A5 File Offset: 0x000908A5
		internal SecurityStatus LastSecurityStatus
		{
			get
			{
				return this._SecurityStatus;
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06001F6E RID: 8046 RVA: 0x000926AD File Offset: 0x000908AD
		internal bool IsCertValidationFailed
		{
			get
			{
				return this._CertValidationFailed;
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06001F6F RID: 8047 RVA: 0x000926B5 File Offset: 0x000908B5
		internal bool IsShutdown
		{
			get
			{
				return this._Shutdown;
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06001F70 RID: 8048 RVA: 0x000926BD File Offset: 0x000908BD
		internal bool DataAvailable
		{
			get
			{
				return this.IsAuthenticated && (this.SecureStream.DataAvailable || this._QueuedReadCount != 0);
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06001F71 RID: 8049 RVA: 0x000926E4 File Offset: 0x000908E4
		internal CipherAlgorithmType CipherAlgorithm
		{
			get
			{
				this.CheckThrow(true, false);
				SslConnectionInfo connectionInfo = this.Context.ConnectionInfo;
				if (connectionInfo == null)
				{
					return CipherAlgorithmType.None;
				}
				return (CipherAlgorithmType)connectionInfo.DataCipherAlg;
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06001F72 RID: 8050 RVA: 0x00092710 File Offset: 0x00090910
		internal int CipherStrength
		{
			get
			{
				this.CheckThrow(true, false);
				SslConnectionInfo connectionInfo = this.Context.ConnectionInfo;
				if (connectionInfo == null)
				{
					return 0;
				}
				return connectionInfo.DataKeySize;
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06001F73 RID: 8051 RVA: 0x0009273C File Offset: 0x0009093C
		internal HashAlgorithmType HashAlgorithm
		{
			get
			{
				this.CheckThrow(true, false);
				SslConnectionInfo connectionInfo = this.Context.ConnectionInfo;
				if (connectionInfo == null)
				{
					return HashAlgorithmType.None;
				}
				return (HashAlgorithmType)connectionInfo.DataHashAlg;
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06001F74 RID: 8052 RVA: 0x00092768 File Offset: 0x00090968
		internal int HashStrength
		{
			get
			{
				this.CheckThrow(true, false);
				SslConnectionInfo connectionInfo = this.Context.ConnectionInfo;
				if (connectionInfo == null)
				{
					return 0;
				}
				return connectionInfo.DataHashKeySize;
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06001F75 RID: 8053 RVA: 0x00092794 File Offset: 0x00090994
		internal ExchangeAlgorithmType KeyExchangeAlgorithm
		{
			get
			{
				this.CheckThrow(true, false);
				SslConnectionInfo connectionInfo = this.Context.ConnectionInfo;
				if (connectionInfo == null)
				{
					return ExchangeAlgorithmType.None;
				}
				return (ExchangeAlgorithmType)connectionInfo.KeyExchangeAlg;
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06001F76 RID: 8054 RVA: 0x000927C0 File Offset: 0x000909C0
		internal int KeyExchangeStrength
		{
			get
			{
				this.CheckThrow(true, false);
				SslConnectionInfo connectionInfo = this.Context.ConnectionInfo;
				if (connectionInfo == null)
				{
					return 0;
				}
				return connectionInfo.KeyExchKeySize;
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06001F77 RID: 8055 RVA: 0x000927EC File Offset: 0x000909EC
		internal SslProtocols SslProtocol
		{
			get
			{
				this.CheckThrow(true, false);
				SslConnectionInfo connectionInfo = this.Context.ConnectionInfo;
				if (connectionInfo == null)
				{
					return SslProtocols.None;
				}
				SslProtocols sslProtocols = (SslProtocols)connectionInfo.Protocol;
				if ((sslProtocols & SslProtocols.Ssl2) != SslProtocols.None)
				{
					sslProtocols |= SslProtocols.Ssl2;
				}
				if ((sslProtocols & SslProtocols.Ssl3) != SslProtocols.None)
				{
					sslProtocols |= SslProtocols.Ssl3;
				}
				if ((sslProtocols & SslProtocols.Tls) != SslProtocols.None)
				{
					sslProtocols |= SslProtocols.Tls;
				}
				if ((sslProtocols & SslProtocols.Tls11) != SslProtocols.None)
				{
					sslProtocols |= SslProtocols.Tls11;
				}
				if ((sslProtocols & SslProtocols.Tls12) != SslProtocols.None)
				{
					sslProtocols |= SslProtocols.Tls12;
				}
				if ((sslProtocols & SslProtocols.Tls13) != SslProtocols.None)
				{
					sslProtocols |= SslProtocols.Tls13;
				}
				return sslProtocols;
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06001F78 RID: 8056 RVA: 0x00092874 File Offset: 0x00090A74
		internal Stream InnerStream
		{
			get
			{
				return this._InnerStream;
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06001F79 RID: 8057 RVA: 0x0009287C File Offset: 0x00090A7C
		internal _SslStream SecureStream
		{
			get
			{
				this.CheckThrow(true, false);
				if (this._SecureStream == null)
				{
					Interlocked.CompareExchange<_SslStream>(ref this._SecureStream, new _SslStream(this), null);
				}
				return this._SecureStream;
			}
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06001F7A RID: 8058 RVA: 0x000928A7 File Offset: 0x00090AA7
		internal int HeaderSize
		{
			get
			{
				return this.Context.HeaderSize;
			}
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06001F7B RID: 8059 RVA: 0x000928B4 File Offset: 0x00090AB4
		internal int MaxDataSize
		{
			get
			{
				return this.Context.MaxDataSize;
			}
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06001F7C RID: 8060 RVA: 0x000928C1 File Offset: 0x00090AC1
		internal byte[] LastPayload
		{
			get
			{
				return this._LastPayload;
			}
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x000928C9 File Offset: 0x00090AC9
		internal void LastPayloadConsumed()
		{
			this._LastPayload = null;
		}

		// Token: 0x06001F7E RID: 8062 RVA: 0x000928D2 File Offset: 0x00090AD2
		private Exception SetException(Exception e)
		{
			if (this._Exception == null)
			{
				this._Exception = e;
			}
			if (this._Exception != null && this.Context != null)
			{
				this.Context.Close();
			}
			return this._Exception;
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06001F7F RID: 8063 RVA: 0x00092904 File Offset: 0x00090B04
		private bool HandshakeCompleted
		{
			get
			{
				return this._HandshakeCompleted;
			}
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06001F80 RID: 8064 RVA: 0x0009290C File Offset: 0x00090B0C
		private SecureChannel Context
		{
			get
			{
				return this._Context;
			}
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x00092914 File Offset: 0x00090B14
		internal void CheckThrow(bool authSuccessCheck, bool shutdownCheck = false)
		{
			if (this._Exception != null)
			{
				throw this._Exception;
			}
			if (authSuccessCheck && !this.IsAuthenticated)
			{
				throw new InvalidOperationException(SR.GetString("net_auth_noauth"));
			}
			if (shutdownCheck && this._Shutdown && !LocalAppContextSwitches.DontEnableTlsAlerts)
			{
				throw new InvalidOperationException("net_ssl_io_already_shutdown");
			}
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x00092968 File Offset: 0x00090B68
		internal void Flush()
		{
			this.InnerStream.Flush();
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x00092975 File Offset: 0x00090B75
		internal void Close()
		{
			this._Exception = new ObjectDisposedException("SslStream");
			if (this.Context != null)
			{
				this.Context.Close();
			}
		}

		// Token: 0x06001F84 RID: 8068 RVA: 0x0009299A File Offset: 0x00090B9A
		internal SecurityStatus EncryptData(byte[] buffer, int offset, int count, ref byte[] outBuffer, out int outSize)
		{
			this.CheckThrow(true, false);
			return this.Context.Encrypt(buffer, offset, count, ref outBuffer, out outSize);
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x000929B6 File Offset: 0x00090BB6
		internal SecurityStatus DecryptData(byte[] buffer, ref int offset, ref int count)
		{
			this.CheckThrow(true, false);
			return this.PrivateDecryptData(buffer, ref offset, ref count);
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x000929C9 File Offset: 0x00090BC9
		private SecurityStatus PrivateDecryptData(byte[] buffer, ref int offset, ref int count)
		{
			return this.Context.Decrypt(buffer, ref offset, ref count);
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x000929DC File Offset: 0x00090BDC
		private Exception EnqueueOldKeyDecryptedData(byte[] buffer, int offset, int count)
		{
			lock (this)
			{
				if (this._QueuedReadCount + count > 131072)
				{
					return new IOException(SR.GetString("net_auth_ignored_reauth", new object[]
					{
						131072.ToString(NumberFormatInfo.CurrentInfo)
					}));
				}
				if (count != 0)
				{
					this._QueuedReadData = SslState.EnsureBufferSize(this._QueuedReadData, this._QueuedReadCount, this._QueuedReadCount + count);
					Buffer.BlockCopy(buffer, offset, this._QueuedReadData, this._QueuedReadCount, count);
					this._QueuedReadCount += count;
					this.FinishHandshakeRead(2);
				}
			}
			return null;
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x00092A9C File Offset: 0x00090C9C
		internal int CheckOldKeyDecryptedData(byte[] buffer, int offset, int count)
		{
			this.CheckThrow(true, false);
			if (this._QueuedReadData != null)
			{
				int num = Math.Min(this._QueuedReadCount, count);
				Buffer.BlockCopy(this._QueuedReadData, 0, buffer, offset, num);
				this._QueuedReadCount -= num;
				if (this._QueuedReadCount == 0)
				{
					this._QueuedReadData = null;
				}
				else
				{
					Buffer.BlockCopy(this._QueuedReadData, num, this._QueuedReadData, 0, this._QueuedReadCount);
				}
				return num;
			}
			return -1;
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x00092B10 File Offset: 0x00090D10
		internal void ProcessAuthentication(LazyAsyncResult lazyResult)
		{
			if (Interlocked.Exchange(ref this._NestedAuth, 1) == 1)
			{
				throw new InvalidOperationException(SR.GetString("net_io_invalidnestedcall", new object[]
				{
					(lazyResult == null) ? "BeginAuthenticate" : "Authenticate",
					"authenticate"
				}));
			}
			try
			{
				this.CheckThrow(false, false);
				AsyncProtocolRequest asyncProtocolRequest = null;
				if (lazyResult != null)
				{
					asyncProtocolRequest = new AsyncProtocolRequest(lazyResult);
					asyncProtocolRequest.Buffer = null;
				}
				this._CachedSession = SslState.CachedSessionStatus.Unknown;
				this.ForceAuthentication(this.Context.IsServer, null, asyncProtocolRequest, false);
				if (lazyResult == null && Logging.On)
				{
					Logging.PrintInfo(Logging.Web, SR.GetString("net_log_sspi_selected_cipher_suite", new object[]
					{
						"ProcessAuthentication",
						this.SslProtocol,
						this.CipherAlgorithm,
						this.CipherStrength,
						this.HashAlgorithm,
						this.HashStrength,
						this.KeyExchangeAlgorithm,
						this.KeyExchangeStrength
					}));
				}
			}
			catch (Exception)
			{
				this._NestedAuth = 0;
				throw;
			}
			finally
			{
				if (lazyResult == null)
				{
					this._NestedAuth = 0;
				}
			}
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x00092C5C File Offset: 0x00090E5C
		internal void ReplyOnReAuthentication(byte[] buffer)
		{
			lock (this)
			{
				this._LockReadState = 2;
				if (this._PendingReHandshake)
				{
					this.FinishRead(buffer);
					return;
				}
			}
			this.ForceAuthentication(false, buffer, new AsyncProtocolRequest(new LazyAsyncResult(this, null, new AsyncCallback(this.RehandshakeCompleteCallback)))
			{
				Buffer = buffer
			}, true);
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x00092CD4 File Offset: 0x00090ED4
		private void ForceAuthentication(bool receiveFirst, byte[] buffer, AsyncProtocolRequest asyncRequest, bool renegotiation = false)
		{
			if (this.CheckEnqueueHandshake(buffer, asyncRequest))
			{
				return;
			}
			SslConnectionInfo connectionInfo = this.Context.ConnectionInfo;
			if (connectionInfo == null || (connectionInfo.Protocol & 12288) == 0)
			{
				this._Framing = SslState.Framing.None;
			}
			try
			{
				if (receiveFirst)
				{
					this.StartReceiveBlob(buffer, asyncRequest);
				}
				else
				{
					this.StartSendBlob(buffer, (buffer == null) ? 0 : buffer.Length, asyncRequest, renegotiation);
				}
			}
			catch (Exception ex)
			{
				this._Framing = SslState.Framing.None;
				this._HandshakeCompleted = false;
				if (this.SetException(ex) == ex)
				{
					throw;
				}
				throw this._Exception;
			}
			finally
			{
				if (this._Exception != null)
				{
					this.FinishHandshake(null, null);
				}
			}
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x00092D84 File Offset: 0x00090F84
		internal void EndProcessAuthentication(IAsyncResult result)
		{
			if (result == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			LazyAsyncResult lazyAsyncResult = result as LazyAsyncResult;
			if (lazyAsyncResult == null)
			{
				throw new ArgumentException(SR.GetString("net_io_async_result", new object[]
				{
					result.GetType().FullName
				}), "asyncResult");
			}
			if (Interlocked.Exchange(ref this._NestedAuth, 0) == 0)
			{
				throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[]
				{
					"EndAuthenticate"
				}));
			}
			this.InternalEndProcessAuthentication(lazyAsyncResult);
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, SR.GetString("net_log_sspi_selected_cipher_suite", new object[]
				{
					"EndProcessAuthentication",
					this.SslProtocol,
					this.CipherAlgorithm,
					this.CipherStrength,
					this.HashAlgorithm,
					this.HashStrength,
					this.KeyExchangeAlgorithm,
					this.KeyExchangeStrength
				}));
			}
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x00092E94 File Offset: 0x00091094
		internal void InternalEndProcessAuthentication(LazyAsyncResult lazyResult)
		{
			lazyResult.InternalWaitForCompletion();
			Exception ex = lazyResult.Result as Exception;
			if (ex != null)
			{
				this._Framing = SslState.Framing.None;
				this._HandshakeCompleted = false;
				throw this.SetException(ex);
			}
		}

		// Token: 0x06001F8E RID: 8078 RVA: 0x00092ED0 File Offset: 0x000910D0
		private void StartSendBlob(byte[] incoming, int count, AsyncProtocolRequest asyncRequest, bool renegotiation = false)
		{
			ProtocolToken protocolToken = this.Context.NextMessage(incoming, 0, count);
			this._SecurityStatus = protocolToken.Status;
			if (protocolToken.Size != 0)
			{
				if (this.Context.IsServer && this._CachedSession == SslState.CachedSessionStatus.Unknown)
				{
					this._CachedSession = ((protocolToken.Size < 200) ? SslState.CachedSessionStatus.IsCached : SslState.CachedSessionStatus.IsNotCached);
				}
				if (this._Framing == SslState.Framing.Unified)
				{
					this._Framing = this.DetectFraming(protocolToken.Payload, protocolToken.Payload.Length);
				}
				SslConnectionInfo connectionInfo = this.Context.ConnectionInfo;
				bool flag = renegotiation && !this.Context.IsServer && connectionInfo != null && (connectionInfo.Protocol & 12288) != 0;
				if (protocolToken.Done && this._ForceBufferingLastHandshakePayload && this.InnerStream.GetType() == typeof(NetworkStream) && !this._PendingReHandshake && !flag)
				{
					this._LastPayload = protocolToken.Payload;
				}
				else if (asyncRequest == null)
				{
					this.InnerStream.Write(protocolToken.Payload, 0, protocolToken.Size);
				}
				else
				{
					asyncRequest.AsyncState = protocolToken;
					IAsyncResult asyncResult = this.InnerStream.BeginWrite(protocolToken.Payload, 0, protocolToken.Size, SslState._WriteCallback, asyncRequest);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					this.InnerStream.EndWrite(asyncResult);
				}
			}
			this.CheckCompletionBeforeNextReceive(protocolToken, asyncRequest);
		}

		// Token: 0x06001F8F RID: 8079 RVA: 0x0009302C File Offset: 0x0009122C
		private void CheckCompletionBeforeNextReceive(ProtocolToken message, AsyncProtocolRequest asyncRequest)
		{
			if (message.Failed)
			{
				this.StartSendAuthResetSignal(null, asyncRequest, new AuthenticationException(SR.GetString("net_auth_SSPI"), message.GetException()));
				return;
			}
			if (!message.Done || this._PendingReHandshake)
			{
				this.StartReceiveBlob(message.Payload, asyncRequest);
				return;
			}
			ProtocolToken message2 = null;
			if (!this.CompleteHandshake(ref message2))
			{
				this.StartSendAuthResetSignal(message2, asyncRequest, new AuthenticationException(SR.GetString("net_ssl_io_cert_validation"), null));
				return;
			}
			this.FinishHandshake(null, asyncRequest);
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x000930AC File Offset: 0x000912AC
		private void StartReceiveBlob(byte[] buffer, AsyncProtocolRequest asyncRequest)
		{
			if (this._PendingReHandshake)
			{
				if (this.CheckEnqueueHandshakeRead(ref buffer, asyncRequest))
				{
					return;
				}
				if (!this._PendingReHandshake)
				{
					this.ProcessReceivedBlob(buffer, buffer.Length, asyncRequest);
					return;
				}
			}
			buffer = SslState.EnsureBufferSize(buffer, 0, 5);
			int readBytes;
			if (asyncRequest == null)
			{
				readBytes = this._Reader.ReadPacket(buffer, 0, 5);
			}
			else
			{
				asyncRequest.SetNextRequest(buffer, 0, 5, SslState._PartialFrameCallback);
				this._Reader.AsyncReadPacket(asyncRequest);
				if (!asyncRequest.MustCompleteSynchronously)
				{
					return;
				}
				readBytes = asyncRequest.Result;
			}
			this.StartReadFrame(buffer, readBytes, asyncRequest);
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x00093134 File Offset: 0x00091334
		private void StartReadFrame(byte[] buffer, int readBytes, AsyncProtocolRequest asyncRequest)
		{
			if (readBytes == 0)
			{
				throw new IOException(SR.GetString("net_auth_eof"));
			}
			if (this._Framing == SslState.Framing.None)
			{
				this._Framing = this.DetectFraming(buffer, readBytes);
			}
			int num = this.GetRemainingFrameSize(buffer, readBytes);
			if (num < 0)
			{
				throw new IOException(SR.GetString("net_ssl_io_frame"));
			}
			if (num == 0)
			{
				throw new AuthenticationException(SR.GetString("net_auth_eof"), null);
			}
			buffer = SslState.EnsureBufferSize(buffer, readBytes, readBytes + num);
			if (asyncRequest == null)
			{
				num = this._Reader.ReadPacket(buffer, readBytes, num);
			}
			else
			{
				asyncRequest.SetNextRequest(buffer, readBytes, num, SslState._ReadFrameCallback);
				this._Reader.AsyncReadPacket(asyncRequest);
				if (!asyncRequest.MustCompleteSynchronously)
				{
					return;
				}
				num = asyncRequest.Result;
				if (num == 0)
				{
					readBytes = 0;
				}
			}
			this.ProcessReceivedBlob(buffer, readBytes + num, asyncRequest);
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x000931F8 File Offset: 0x000913F8
		private void ProcessReceivedBlob(byte[] buffer, int count, AsyncProtocolRequest asyncRequest)
		{
			if (count == 0)
			{
				throw new AuthenticationException(SR.GetString("net_auth_eof"), null);
			}
			if (this._PendingReHandshake)
			{
				int num = 0;
				SecurityStatus securityStatus = this.PrivateDecryptData(buffer, ref num, ref count);
				if (securityStatus == SecurityStatus.OK)
				{
					Exception ex = this.EnqueueOldKeyDecryptedData(buffer, num, count);
					if (ex != null)
					{
						this.StartSendAuthResetSignal(null, asyncRequest, ex);
						return;
					}
					this._Framing = SslState.Framing.None;
					this.StartReceiveBlob(buffer, asyncRequest);
					return;
				}
				else
				{
					if (securityStatus != SecurityStatus.Renegotiate)
					{
						ProtocolToken protocolToken = new ProtocolToken(null, securityStatus);
						this.StartSendAuthResetSignal(null, asyncRequest, new AuthenticationException(SR.GetString("net_auth_SSPI"), protocolToken.GetException()));
						return;
					}
					this._PendingReHandshake = false;
					if (num != 0)
					{
						Buffer.BlockCopy(buffer, num, buffer, 0, count);
					}
				}
			}
			this.StartSendBlob(buffer, count, asyncRequest, false);
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x000932A8 File Offset: 0x000914A8
		private void StartSendAuthResetSignal(ProtocolToken message, AsyncProtocolRequest asyncRequest, Exception exception)
		{
			if (message == null || message.Size == 0)
			{
				throw exception;
			}
			if (asyncRequest == null)
			{
				this.InnerStream.Write(message.Payload, 0, message.Size);
			}
			else
			{
				asyncRequest.AsyncState = exception;
				IAsyncResult asyncResult = this.InnerStream.BeginWrite(message.Payload, 0, message.Size, SslState._WriteCallback, asyncRequest);
				if (!asyncResult.CompletedSynchronously)
				{
					return;
				}
				this.InnerStream.EndWrite(asyncResult);
			}
			throw exception;
		}

		// Token: 0x06001F94 RID: 8084 RVA: 0x0009331B File Offset: 0x0009151B
		private bool CompleteHandshake(ref ProtocolToken alertToken)
		{
			this.Context.ProcessHandshakeSuccess();
			if (!this.Context.VerifyRemoteCertificate(this._CertValidationDelegate, ref alertToken))
			{
				this._HandshakeCompleted = false;
				this._CertValidationFailed = true;
				return false;
			}
			this._CertValidationFailed = false;
			this._HandshakeCompleted = true;
			return true;
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x0009335C File Offset: 0x0009155C
		private static void WriteCallback(IAsyncResult transportResult)
		{
			if (transportResult.CompletedSynchronously)
			{
				return;
			}
			AsyncProtocolRequest asyncProtocolRequest = (AsyncProtocolRequest)transportResult.AsyncState;
			SslState sslState = (SslState)asyncProtocolRequest.AsyncObject;
			try
			{
				sslState.InnerStream.EndWrite(transportResult);
				object asyncState = asyncProtocolRequest.AsyncState;
				Exception ex = asyncState as Exception;
				if (ex != null)
				{
					throw ex;
				}
				sslState.CheckCompletionBeforeNextReceive((ProtocolToken)asyncState, asyncProtocolRequest);
			}
			catch (Exception e)
			{
				if (asyncProtocolRequest.IsUserCompleted)
				{
					throw;
				}
				sslState.FinishHandshake(e, asyncProtocolRequest);
			}
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x000933E0 File Offset: 0x000915E0
		private static void PartialFrameCallback(AsyncProtocolRequest asyncRequest)
		{
			SslState sslState = (SslState)asyncRequest.AsyncObject;
			try
			{
				sslState.StartReadFrame(asyncRequest.Buffer, asyncRequest.Result, asyncRequest);
			}
			catch (Exception e)
			{
				if (asyncRequest.IsUserCompleted)
				{
					throw;
				}
				sslState.FinishHandshake(e, asyncRequest);
			}
		}

		// Token: 0x06001F97 RID: 8087 RVA: 0x00093434 File Offset: 0x00091634
		private static void ReadFrameCallback(AsyncProtocolRequest asyncRequest)
		{
			SslState sslState = (SslState)asyncRequest.AsyncObject;
			try
			{
				if (asyncRequest.Result == 0)
				{
					asyncRequest.Offset = 0;
				}
				sslState.ProcessReceivedBlob(asyncRequest.Buffer, asyncRequest.Offset + asyncRequest.Result, asyncRequest);
			}
			catch (Exception e)
			{
				if (asyncRequest.IsUserCompleted)
				{
					throw;
				}
				sslState.FinishHandshake(e, asyncRequest);
			}
		}

		// Token: 0x06001F98 RID: 8088 RVA: 0x000934A0 File Offset: 0x000916A0
		private bool CheckEnqueueHandshakeRead(ref byte[] buffer, AsyncProtocolRequest request)
		{
			LazyAsyncResult lazyAsyncResult = null;
			lock (this)
			{
				if (this._LockReadState == 6)
				{
					return false;
				}
				int num = Interlocked.Exchange(ref this._LockReadState, 2);
				if (num != 5)
				{
					return false;
				}
				if (request != null)
				{
					this._QueuedReadStateRequest = request;
					return true;
				}
				lazyAsyncResult = new LazyAsyncResult(null, null, null);
				this._QueuedReadStateRequest = lazyAsyncResult;
			}
			lazyAsyncResult.InternalWaitForCompletion();
			buffer = (byte[])lazyAsyncResult.Result;
			return false;
		}

		// Token: 0x06001F99 RID: 8089 RVA: 0x00093534 File Offset: 0x00091734
		private void FinishHandshakeRead(int newState)
		{
			lock (this)
			{
				int num = Interlocked.Exchange(ref this._LockReadState, newState);
				if (num == 6)
				{
					this._LockReadState = 5;
					object queuedReadStateRequest = this._QueuedReadStateRequest;
					if (queuedReadStateRequest != null)
					{
						this._QueuedReadStateRequest = null;
						if (queuedReadStateRequest is LazyAsyncResult)
						{
							((LazyAsyncResult)queuedReadStateRequest).InvokeCallback();
						}
						else
						{
							ThreadPool.QueueUserWorkItem(new WaitCallback(this.CompleteRequestWaitCallback), queuedReadStateRequest);
						}
					}
				}
			}
		}

		// Token: 0x06001F9A RID: 8090 RVA: 0x000935C0 File Offset: 0x000917C0
		internal int CheckEnqueueRead(byte[] buffer, int offset, int count, AsyncProtocolRequest request)
		{
			int num = Interlocked.CompareExchange(ref this._LockReadState, 5, 0);
			if (num != 2)
			{
				return this.CheckOldKeyDecryptedData(buffer, offset, count);
			}
			LazyAsyncResult lazyAsyncResult = null;
			lock (this)
			{
				int num2 = this.CheckOldKeyDecryptedData(buffer, offset, count);
				if (num2 != -1)
				{
					return num2;
				}
				if (this._LockReadState != 2)
				{
					this._LockReadState = 5;
					return -1;
				}
				this._LockReadState = 6;
				if (request != null)
				{
					this._QueuedReadStateRequest = request;
					return 0;
				}
				lazyAsyncResult = new LazyAsyncResult(null, null, null);
				this._QueuedReadStateRequest = lazyAsyncResult;
			}
			lazyAsyncResult.InternalWaitForCompletion();
			int result;
			lock (this)
			{
				result = this.CheckOldKeyDecryptedData(buffer, offset, count);
			}
			return result;
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x000936A4 File Offset: 0x000918A4
		internal void FinishRead(byte[] renegotiateBuffer)
		{
			int num = Interlocked.CompareExchange(ref this._LockReadState, 0, 5);
			if (num != 2)
			{
				return;
			}
			lock (this)
			{
				LazyAsyncResult lazyAsyncResult = this._QueuedReadStateRequest as LazyAsyncResult;
				if (lazyAsyncResult != null)
				{
					this._QueuedReadStateRequest = null;
					lazyAsyncResult.InvokeCallback(renegotiateBuffer);
				}
				else
				{
					AsyncProtocolRequest asyncProtocolRequest = (AsyncProtocolRequest)this._QueuedReadStateRequest;
					asyncProtocolRequest.Buffer = renegotiateBuffer;
					this._QueuedReadStateRequest = null;
					ThreadPool.QueueUserWorkItem(new WaitCallback(this.AsyncResumeHandshakeRead), asyncProtocolRequest);
				}
			}
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x0009373C File Offset: 0x0009193C
		internal bool CheckEnqueueWrite(AsyncProtocolRequest asyncRequest)
		{
			bool disableHandshakeLockFix = ServicePointManager.DisableHandshakeLockFix;
			if (disableHandshakeLockFix)
			{
				this._QueuedWriteStateRequest = null;
			}
			int num = Interlocked.CompareExchange(ref this._LockWriteState, 1, 0);
			if (disableHandshakeLockFix)
			{
				if (num != 2)
				{
					return false;
				}
			}
			else if (num != 2 && num != 4)
			{
				return false;
			}
			LazyAsyncResult lazyAsyncResult = null;
			lock (this)
			{
				if (this._LockWriteState != 2)
				{
					this.CheckThrow(true, false);
					return false;
				}
				this._LockWriteState = 3;
				if (asyncRequest != null)
				{
					this._QueuedWriteStateRequest = asyncRequest;
					return true;
				}
				lazyAsyncResult = new LazyAsyncResult(null, null, null);
				this._QueuedWriteStateRequest = lazyAsyncResult;
			}
			lazyAsyncResult.InternalWaitForCompletion();
			this.CheckThrow(true, false);
			return false;
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x000937F8 File Offset: 0x000919F8
		internal void FinishWrite()
		{
			int num = Interlocked.CompareExchange(ref this._LockWriteState, 0, 1);
			if (ServicePointManager.DisableHandshakeLockFix)
			{
				if (num != 2)
				{
					return;
				}
			}
			else if (num != 4)
			{
				return;
			}
			lock (this)
			{
				if (!ServicePointManager.DisableHandshakeLockFix)
				{
					this._LockWriteState = 2;
				}
				object queuedWriteStateRequest = this._QueuedWriteStateRequest;
				if (queuedWriteStateRequest != null)
				{
					this._QueuedWriteStateRequest = null;
					if (queuedWriteStateRequest is LazyAsyncResult)
					{
						((LazyAsyncResult)queuedWriteStateRequest).InvokeCallback();
					}
					else
					{
						ThreadPool.QueueUserWorkItem(new WaitCallback(this.AsyncResumeHandshake), queuedWriteStateRequest);
					}
				}
			}
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x00093898 File Offset: 0x00091A98
		internal IAsyncResult BeginShutdown(AsyncCallback asyncCallback, object asyncState)
		{
			this.CheckThrow(true, true);
			ProtocolToken protocolToken = this.Context.CreateShutdownToken();
			return this.InnerStream.BeginWrite(protocolToken.Payload, 0, protocolToken.Payload.Length, asyncCallback, asyncState);
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x000938D5 File Offset: 0x00091AD5
		internal void EndShutdown(IAsyncResult result)
		{
			this.CheckThrow(true, true);
			this.InnerStream.EndWrite(result);
			this._Shutdown = true;
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x000938F4 File Offset: 0x00091AF4
		private bool CheckEnqueueHandshake(byte[] buffer, AsyncProtocolRequest asyncRequest)
		{
			LazyAsyncResult lazyAsyncResult = null;
			lock (this)
			{
				if (this._LockWriteState == 3)
				{
					return false;
				}
				if (!ServicePointManager.DisableHandshakeLockFix)
				{
					for (;;)
					{
						int num = Interlocked.CompareExchange(ref this._LockWriteState, 2, 0);
						if (num != 1)
						{
							break;
						}
						int num2 = Interlocked.CompareExchange(ref this._LockWriteState, 4, num);
						if (num2 == num)
						{
							goto IL_6A;
						}
					}
					return false;
				}
				int num3 = Interlocked.Exchange(ref this._LockWriteState, 2);
				if (num3 != 1)
				{
					return false;
				}
				IL_6A:
				if (asyncRequest != null)
				{
					asyncRequest.Buffer = buffer;
					this._QueuedWriteStateRequest = asyncRequest;
					return true;
				}
				lazyAsyncResult = new LazyAsyncResult(null, null, null);
				this._QueuedWriteStateRequest = lazyAsyncResult;
			}
			lazyAsyncResult.InternalWaitForCompletion();
			return false;
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x000939B8 File Offset: 0x00091BB8
		private void FinishHandshake(Exception e, AsyncProtocolRequest asyncRequest)
		{
			try
			{
				lock (this)
				{
					if (e != null)
					{
						this.SetException(e);
					}
					this.FinishHandshakeRead(0);
					int num = Interlocked.CompareExchange(ref this._LockWriteState, 0, 2);
					if (num == 3)
					{
						this._LockWriteState = 1;
						object queuedWriteStateRequest = this._QueuedWriteStateRequest;
						if (queuedWriteStateRequest != null)
						{
							this._QueuedWriteStateRequest = null;
							if (queuedWriteStateRequest is LazyAsyncResult)
							{
								((LazyAsyncResult)queuedWriteStateRequest).InvokeCallback();
							}
							else
							{
								ThreadPool.QueueUserWorkItem(new WaitCallback(this.CompleteRequestWaitCallback), queuedWriteStateRequest);
							}
						}
					}
				}
			}
			finally
			{
				if (asyncRequest != null)
				{
					if (e != null)
					{
						asyncRequest.CompleteWithError(e);
					}
					else
					{
						asyncRequest.CompleteUser();
					}
				}
			}
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x00093A78 File Offset: 0x00091C78
		private static byte[] EnsureBufferSize(byte[] buffer, int copyCount, int size)
		{
			if (buffer == null || buffer.Length < size)
			{
				byte[] array = buffer;
				buffer = new byte[size];
				if (array != null && copyCount != 0)
				{
					Buffer.BlockCopy(array, 0, buffer, 0, copyCount);
				}
			}
			return buffer;
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x00093AAC File Offset: 0x00091CAC
		private SslState.Framing DetectFraming(byte[] bytes, int length)
		{
			int num = -1;
			if (bytes[0] == 22 || bytes[0] == 23 || bytes[0] == 21)
			{
				if (length < 3)
				{
					return SslState.Framing.Invalid;
				}
				num = ((int)bytes[1] << 8 | (int)bytes[2]);
				if (num < 768 || num >= 1280)
				{
					return SslState.Framing.Invalid;
				}
				return SslState.Framing.SinceSSL3;
			}
			else
			{
				if (length < 3)
				{
					return SslState.Framing.Invalid;
				}
				if (bytes[2] > 8)
				{
					return SslState.Framing.Invalid;
				}
				if (bytes[2] == 1)
				{
					if (length >= 5)
					{
						num = ((int)bytes[3] << 8 | (int)bytes[4]);
					}
				}
				else if (bytes[2] == 4 && length >= 7)
				{
					num = ((int)bytes[5] << 8 | (int)bytes[6]);
				}
				if (num != -1)
				{
					if (this._Framing == SslState.Framing.None)
					{
						if (num != 2 && (num < 512 || num >= 1280))
						{
							return SslState.Framing.Invalid;
						}
					}
					else if (num != 2)
					{
						return SslState.Framing.Invalid;
					}
				}
				if (!this.Context.IsServer || this._Framing == SslState.Framing.Unified)
				{
					return SslState.Framing.BeforeSSL3;
				}
				return SslState.Framing.Unified;
			}
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x00093B70 File Offset: 0x00091D70
		internal int GetRemainingFrameSize(byte[] buffer, int dataSize)
		{
			int num = -1;
			switch (this._Framing)
			{
			case SslState.Framing.BeforeSSL3:
			case SslState.Framing.Unified:
				if (dataSize < 2)
				{
					throw new IOException(SR.GetString("net_ssl_io_frame"));
				}
				if ((buffer[0] & 128) != 0)
				{
					num = ((int)(buffer[0] & 127) << 8 | (int)buffer[1]) + 2;
					num -= dataSize;
				}
				else
				{
					num = ((int)(buffer[0] & 63) << 8 | (int)buffer[1]) + 3;
					num -= dataSize;
				}
				break;
			case SslState.Framing.SinceSSL3:
				if (dataSize < 5)
				{
					throw new IOException(SR.GetString("net_ssl_io_frame"));
				}
				num = ((int)buffer[3] << 8 | (int)buffer[4]) + 5;
				num -= dataSize;
				break;
			}
			return num;
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x00093C0C File Offset: 0x00091E0C
		private void AsyncResumeHandshake(object state)
		{
			AsyncProtocolRequest asyncProtocolRequest = state as AsyncProtocolRequest;
			try
			{
				this.ForceAuthentication(this.Context.IsServer, asyncProtocolRequest.Buffer, asyncProtocolRequest, false);
			}
			catch (Exception e)
			{
				asyncProtocolRequest.CompleteWithError(e);
			}
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x00093C58 File Offset: 0x00091E58
		private void AsyncResumeHandshakeRead(object state)
		{
			AsyncProtocolRequest asyncProtocolRequest = (AsyncProtocolRequest)state;
			try
			{
				if (this._PendingReHandshake)
				{
					this.StartReceiveBlob(asyncProtocolRequest.Buffer, asyncProtocolRequest);
				}
				else
				{
					this.ProcessReceivedBlob(asyncProtocolRequest.Buffer, (asyncProtocolRequest.Buffer == null) ? 0 : asyncProtocolRequest.Buffer.Length, asyncProtocolRequest);
				}
			}
			catch (Exception e)
			{
				if (asyncProtocolRequest.IsUserCompleted)
				{
					throw;
				}
				this.FinishHandshake(e, asyncProtocolRequest);
			}
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x00093CCC File Offset: 0x00091ECC
		private void CompleteRequestWaitCallback(object state)
		{
			AsyncProtocolRequest asyncProtocolRequest = (AsyncProtocolRequest)state;
			if (asyncProtocolRequest.MustCompleteSynchronously)
			{
				throw new InternalException();
			}
			asyncProtocolRequest.CompleteRequest(0);
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x00093CF8 File Offset: 0x00091EF8
		private void RehandshakeCompleteCallback(IAsyncResult result)
		{
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)result;
			Exception ex = lazyAsyncResult.InternalWaitForCompletion() as Exception;
			if (ex != null)
			{
				this.FinishHandshake(ex, null);
			}
		}

		// Token: 0x04001D27 RID: 7463
		private static int UniqueNameInteger = 123;

		// Token: 0x04001D28 RID: 7464
		private static AsyncProtocolCallback _PartialFrameCallback = new AsyncProtocolCallback(SslState.PartialFrameCallback);

		// Token: 0x04001D29 RID: 7465
		private static AsyncProtocolCallback _ReadFrameCallback = new AsyncProtocolCallback(SslState.ReadFrameCallback);

		// Token: 0x04001D2A RID: 7466
		private static AsyncCallback _WriteCallback = new AsyncCallback(SslState.WriteCallback);

		// Token: 0x04001D2B RID: 7467
		private RemoteCertValidationCallback _CertValidationDelegate;

		// Token: 0x04001D2C RID: 7468
		private LocalCertSelectionCallback _CertSelectionDelegate;

		// Token: 0x04001D2D RID: 7469
		private bool _CanRetryAuthentication;

		// Token: 0x04001D2E RID: 7470
		private Stream _InnerStream;

		// Token: 0x04001D2F RID: 7471
		private _SslStream _SecureStream;

		// Token: 0x04001D30 RID: 7472
		private FixedSizeReader _Reader;

		// Token: 0x04001D31 RID: 7473
		private int _NestedAuth;

		// Token: 0x04001D32 RID: 7474
		private SecureChannel _Context;

		// Token: 0x04001D33 RID: 7475
		private bool _HandshakeCompleted;

		// Token: 0x04001D34 RID: 7476
		private bool _CertValidationFailed;

		// Token: 0x04001D35 RID: 7477
		private bool _Shutdown;

		// Token: 0x04001D36 RID: 7478
		private SecurityStatus _SecurityStatus;

		// Token: 0x04001D37 RID: 7479
		private Exception _Exception;

		// Token: 0x04001D38 RID: 7480
		private SslState.CachedSessionStatus _CachedSession;

		// Token: 0x04001D39 RID: 7481
		private byte[] _QueuedReadData;

		// Token: 0x04001D3A RID: 7482
		private int _QueuedReadCount;

		// Token: 0x04001D3B RID: 7483
		private bool _PendingReHandshake;

		// Token: 0x04001D3C RID: 7484
		private const int _ConstMaxQueuedReadBytes = 131072;

		// Token: 0x04001D3D RID: 7485
		private const int LockNone = 0;

		// Token: 0x04001D3E RID: 7486
		private const int LockWrite = 1;

		// Token: 0x04001D3F RID: 7487
		private const int LockHandshake = 2;

		// Token: 0x04001D40 RID: 7488
		private const int LockPendingWrite = 3;

		// Token: 0x04001D41 RID: 7489
		private const int LockPendingHandshake = 4;

		// Token: 0x04001D42 RID: 7490
		private const int LockRead = 5;

		// Token: 0x04001D43 RID: 7491
		private const int LockPendingRead = 6;

		// Token: 0x04001D44 RID: 7492
		private int _LockWriteState;

		// Token: 0x04001D45 RID: 7493
		private object _QueuedWriteStateRequest;

		// Token: 0x04001D46 RID: 7494
		private int _LockReadState;

		// Token: 0x04001D47 RID: 7495
		private object _QueuedReadStateRequest;

		// Token: 0x04001D48 RID: 7496
		private bool _ForceBufferingLastHandshakePayload;

		// Token: 0x04001D49 RID: 7497
		private byte[] _LastPayload;

		// Token: 0x04001D4A RID: 7498
		private readonly EncryptionPolicy _EncryptionPolicy;

		// Token: 0x04001D4B RID: 7499
		private SslState.Framing _Framing;

		// Token: 0x020007D3 RID: 2003
		private enum CachedSessionStatus : byte
		{
			// Token: 0x040034A4 RID: 13476
			Unknown,
			// Token: 0x040034A5 RID: 13477
			IsNotCached,
			// Token: 0x040034A6 RID: 13478
			IsCached,
			// Token: 0x040034A7 RID: 13479
			Renegotiated
		}

		// Token: 0x020007D4 RID: 2004
		private enum Framing
		{
			// Token: 0x040034A9 RID: 13481
			None,
			// Token: 0x040034AA RID: 13482
			BeforeSSL3,
			// Token: 0x040034AB RID: 13483
			SinceSSL3,
			// Token: 0x040034AC RID: 13484
			Unified,
			// Token: 0x040034AD RID: 13485
			Invalid
		}

		// Token: 0x020007D5 RID: 2005
		private enum FrameType : byte
		{
			// Token: 0x040034AF RID: 13487
			ChangeCipherSpec = 20,
			// Token: 0x040034B0 RID: 13488
			Alert,
			// Token: 0x040034B1 RID: 13489
			Handshake,
			// Token: 0x040034B2 RID: 13490
			AppData
		}
	}
}
