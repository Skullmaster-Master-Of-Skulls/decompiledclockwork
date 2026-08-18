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
	// Token: 0x0200059E RID: 1438
	internal class SslState
	{
		// Token: 0x06002C63 RID: 11363 RVA: 0x000BF26C File Offset: 0x000BE26C
		internal SslState(Stream innerStream, bool isHTTP) : this(innerStream, null, null)
		{
			this._ForceBufferingLastHandshakePayload = isHTTP;
		}

		// Token: 0x06002C64 RID: 11364 RVA: 0x000BF27E File Offset: 0x000BE27E
		internal SslState(Stream innerStream, RemoteCertValidationCallback certValidationCallback, LocalCertSelectionCallback certSelectionCallback)
		{
			this._InnerStream = innerStream;
			this._Reader = new FixedSizeReader(innerStream);
			this._CertValidationDelegate = certValidationCallback;
			this._CertSelectionDelegate = certSelectionCallback;
		}

		// Token: 0x06002C65 RID: 11365 RVA: 0x000BF2A8 File Offset: 0x000BE2A8
		internal void ValidateCreateContext(bool isServer, string targetHost, SslProtocols enabledSslProtocols, X509Certificate serverCertificate, X509CertificateCollection clientCertificates, bool remoteCertRequired, bool checkCertRevocationStatus)
		{
			this.ValidateCreateContext(isServer, targetHost, enabledSslProtocols, serverCertificate, clientCertificates, remoteCertRequired, checkCertRevocationStatus, !isServer);
		}

		// Token: 0x06002C66 RID: 11366 RVA: 0x000BF2CC File Offset: 0x000BE2CC
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
				enabledSslProtocols &= (SslProtocols)1073743189;
				if (serverCertificate == null)
				{
					throw new ArgumentNullException("serverCertificate");
				}
			}
			else
			{
				enabledSslProtocols &= (SslProtocols)(-2147480918);
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
				this._Context = new SecureChannel(targetHost, isServer, (SchProtocols)enabledSslProtocols, serverCertificate, clientCertificates, remoteCertRequired, checkCertName, checkCertRevocationStatus, this._CertSelectionDelegate);
			}
			catch (Win32Exception innerException)
			{
				throw new AuthenticationException(SR.GetString("net_auth_SSPI"), innerException);
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06002C67 RID: 11367 RVA: 0x000BF418 File Offset: 0x000BE418
		internal bool IsAuthenticated
		{
			get
			{
				return this._Context != null && this._Context.IsValidContext && this._Exception == null && this.HandshakeCompleted;
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06002C68 RID: 11368 RVA: 0x000BF43F File Offset: 0x000BE43F
		internal bool IsMutuallyAuthenticated
		{
			get
			{
				return this.IsAuthenticated && (this.Context.IsServer ? this.Context.LocalServerCertificate : this.Context.LocalClientCertificate) != null && this.Context.IsRemoteCertificateAvailable;
			}
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06002C69 RID: 11369 RVA: 0x000BF47D File Offset: 0x000BE47D
		internal bool RemoteCertRequired
		{
			get
			{
				return this.Context == null || this.Context.RemoteCertRequired;
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06002C6A RID: 11370 RVA: 0x000BF494 File Offset: 0x000BE494
		internal bool IsServer
		{
			get
			{
				return this.Context != null && this.Context.IsServer;
			}
		}

		// Token: 0x06002C6B RID: 11371 RVA: 0x000BF4AB File Offset: 0x000BE4AB
		internal void SetCertValidationDelegate(RemoteCertValidationCallback certValidationCallback)
		{
			this._CertValidationDelegate = certValidationCallback;
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06002C6C RID: 11372 RVA: 0x000BF4B4 File Offset: 0x000BE4B4
		internal X509Certificate LocalCertificate
		{
			get
			{
				this.CheckThrow(true);
				return this.InternalLocalCertificate;
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06002C6D RID: 11373 RVA: 0x000BF4C3 File Offset: 0x000BE4C3
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

		// Token: 0x06002C6E RID: 11374 RVA: 0x000BF4E9 File Offset: 0x000BE4E9
		internal ChannelBinding GetChannelBinding(ChannelBindingKind kind)
		{
			if (this.Context != null)
			{
				return this.Context.GetChannelBinding(kind);
			}
			return null;
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06002C6F RID: 11375 RVA: 0x000BF501 File Offset: 0x000BE501
		internal bool CheckCertRevocationStatus
		{
			get
			{
				return this.Context != null && this.Context.CheckCertRevocationStatus;
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06002C70 RID: 11376 RVA: 0x000BF518 File Offset: 0x000BE518
		internal SecurityStatus LastSecurityStatus
		{
			get
			{
				return this._SecurityStatus;
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06002C71 RID: 11377 RVA: 0x000BF520 File Offset: 0x000BE520
		internal bool IsCertValidationFailed
		{
			get
			{
				return this._CertValidationFailed;
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06002C72 RID: 11378 RVA: 0x000BF528 File Offset: 0x000BE528
		internal bool DataAvailable
		{
			get
			{
				return this.IsAuthenticated && (this.SecureStream.DataAvailable || this._QueuedReadCount != 0);
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06002C73 RID: 11379 RVA: 0x000BF550 File Offset: 0x000BE550
		internal CipherAlgorithmType CipherAlgorithm
		{
			get
			{
				this.CheckThrow(true);
				SslConnectionInfo connectionInfo = this.Context.ConnectionInfo;
				if (connectionInfo == null)
				{
					return CipherAlgorithmType.None;
				}
				return (CipherAlgorithmType)connectionInfo.DataCipherAlg;
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06002C74 RID: 11380 RVA: 0x000BF57C File Offset: 0x000BE57C
		internal int CipherStrength
		{
			get
			{
				this.CheckThrow(true);
				SslConnectionInfo connectionInfo = this.Context.ConnectionInfo;
				if (connectionInfo == null)
				{
					return 0;
				}
				return connectionInfo.DataKeySize;
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06002C75 RID: 11381 RVA: 0x000BF5A8 File Offset: 0x000BE5A8
		internal HashAlgorithmType HashAlgorithm
		{
			get
			{
				this.CheckThrow(true);
				SslConnectionInfo connectionInfo = this.Context.ConnectionInfo;
				if (connectionInfo == null)
				{
					return HashAlgorithmType.None;
				}
				return (HashAlgorithmType)connectionInfo.DataHashAlg;
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06002C76 RID: 11382 RVA: 0x000BF5D4 File Offset: 0x000BE5D4
		internal int HashStrength
		{
			get
			{
				this.CheckThrow(true);
				SslConnectionInfo connectionInfo = this.Context.ConnectionInfo;
				if (connectionInfo == null)
				{
					return 0;
				}
				return connectionInfo.DataHashKeySize;
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06002C77 RID: 11383 RVA: 0x000BF600 File Offset: 0x000BE600
		internal ExchangeAlgorithmType KeyExchangeAlgorithm
		{
			get
			{
				this.CheckThrow(true);
				SslConnectionInfo connectionInfo = this.Context.ConnectionInfo;
				if (connectionInfo == null)
				{
					return ExchangeAlgorithmType.None;
				}
				return (ExchangeAlgorithmType)connectionInfo.KeyExchangeAlg;
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06002C78 RID: 11384 RVA: 0x000BF62C File Offset: 0x000BE62C
		internal int KeyExchangeStrength
		{
			get
			{
				this.CheckThrow(true);
				SslConnectionInfo connectionInfo = this.Context.ConnectionInfo;
				if (connectionInfo == null)
				{
					return 0;
				}
				return connectionInfo.KeyExchKeySize;
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06002C79 RID: 11385 RVA: 0x000BF658 File Offset: 0x000BE658
		internal SslProtocols SslProtocol
		{
			get
			{
				this.CheckThrow(true);
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
				if ((sslProtocols & (SslProtocols)768) != SslProtocols.None)
				{
					sslProtocols |= (SslProtocols)768;
				}
				if ((sslProtocols & (SslProtocols)3072) != SslProtocols.None)
				{
					sslProtocols |= (SslProtocols)3072;
				}
				return sslProtocols;
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06002C7A RID: 11386 RVA: 0x000BF6CE File Offset: 0x000BE6CE
		internal Stream InnerStream
		{
			get
			{
				return this._InnerStream;
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06002C7B RID: 11387 RVA: 0x000BF6D6 File Offset: 0x000BE6D6
		internal _SslStream SecureStream
		{
			get
			{
				this.CheckThrow(true);
				if (this._SecureStream == null)
				{
					Interlocked.CompareExchange<_SslStream>(ref this._SecureStream, new _SslStream(this), null);
				}
				return this._SecureStream;
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06002C7C RID: 11388 RVA: 0x000BF700 File Offset: 0x000BE700
		internal int HeaderSize
		{
			get
			{
				return this.Context.HeaderSize;
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06002C7D RID: 11389 RVA: 0x000BF70D File Offset: 0x000BE70D
		internal int MaxDataSize
		{
			get
			{
				return this.Context.MaxDataSize;
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06002C7E RID: 11390 RVA: 0x000BF71A File Offset: 0x000BE71A
		internal byte[] LastPayload
		{
			get
			{
				return this._LastPayload;
			}
		}

		// Token: 0x06002C7F RID: 11391 RVA: 0x000BF722 File Offset: 0x000BE722
		internal void LastPayloadConsumed()
		{
			this._LastPayload = null;
		}

		// Token: 0x06002C80 RID: 11392 RVA: 0x000BF72B File Offset: 0x000BE72B
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

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06002C81 RID: 11393 RVA: 0x000BF75D File Offset: 0x000BE75D
		private bool HandshakeCompleted
		{
			get
			{
				return this._HandshakeCompleted;
			}
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06002C82 RID: 11394 RVA: 0x000BF765 File Offset: 0x000BE765
		private SecureChannel Context
		{
			get
			{
				return this._Context;
			}
		}

		// Token: 0x06002C83 RID: 11395 RVA: 0x000BF76D File Offset: 0x000BE76D
		internal void CheckThrow(bool authSucessCheck)
		{
			if (this._Exception != null)
			{
				throw this._Exception;
			}
			if (authSucessCheck && !this.IsAuthenticated)
			{
				throw new InvalidOperationException(SR.GetString("net_auth_noauth"));
			}
		}

		// Token: 0x06002C84 RID: 11396 RVA: 0x000BF799 File Offset: 0x000BE799
		internal void Flush()
		{
			this.InnerStream.Flush();
		}

		// Token: 0x06002C85 RID: 11397 RVA: 0x000BF7A6 File Offset: 0x000BE7A6
		internal void Close()
		{
			this._Exception = new ObjectDisposedException("SslStream");
			if (this.Context != null)
			{
				this.Context.Close();
			}
		}

		// Token: 0x06002C86 RID: 11398 RVA: 0x000BF7CB File Offset: 0x000BE7CB
		internal SecurityStatus EncryptData(byte[] buffer, int offset, int count, ref byte[] outBuffer, out int outSize)
		{
			this.CheckThrow(true);
			return this.Context.Encrypt(buffer, offset, count, ref outBuffer, out outSize);
		}

		// Token: 0x06002C87 RID: 11399 RVA: 0x000BF7E6 File Offset: 0x000BE7E6
		internal SecurityStatus DecryptData(byte[] buffer, ref int offset, ref int count)
		{
			this.CheckThrow(true);
			return this.PrivateDecryptData(buffer, ref offset, ref count);
		}

		// Token: 0x06002C88 RID: 11400 RVA: 0x000BF7F8 File Offset: 0x000BE7F8
		private SecurityStatus PrivateDecryptData(byte[] buffer, ref int offset, ref int count)
		{
			return this.Context.Decrypt(buffer, ref offset, ref count);
		}

		// Token: 0x06002C89 RID: 11401 RVA: 0x000BF808 File Offset: 0x000BE808
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

		// Token: 0x06002C8A RID: 11402 RVA: 0x000BF8C4 File Offset: 0x000BE8C4
		internal int CheckOldKeyDecryptedData(byte[] buffer, int offset, int count)
		{
			this.CheckThrow(true);
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

		// Token: 0x06002C8B RID: 11403 RVA: 0x000BF938 File Offset: 0x000BE938
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
				this.CheckThrow(false);
				AsyncProtocolRequest asyncProtocolRequest = null;
				if (lazyResult != null)
				{
					asyncProtocolRequest = new AsyncProtocolRequest(lazyResult);
					asyncProtocolRequest.Buffer = null;
				}
				this._CachedSession = SslState.CachedSessionStatus.Unknown;
				this.ForceAuthentication(this.Context.IsServer, null, asyncProtocolRequest);
			}
			finally
			{
				if (lazyResult == null || this._Exception != null)
				{
					this._NestedAuth = 0;
				}
			}
		}

		// Token: 0x06002C8C RID: 11404 RVA: 0x000BF9E0 File Offset: 0x000BE9E0
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
			});
		}

		// Token: 0x06002C8D RID: 11405 RVA: 0x000BFA50 File Offset: 0x000BEA50
		private void ForceAuthentication(bool receiveFirst, byte[] buffer, AsyncProtocolRequest asyncRequest)
		{
			if (this.CheckEnqueueHandshake(buffer, asyncRequest))
			{
				return;
			}
			this._Framing = SslState.Framing.None;
			try
			{
				if (receiveFirst)
				{
					this.StartReceiveBlob(buffer, asyncRequest);
				}
				else
				{
					this.StartSendBlob(buffer, (buffer == null) ? 0 : buffer.Length, asyncRequest);
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
			catch
			{
				this._Framing = SslState.Framing.None;
				this._HandshakeCompleted = false;
				throw this.SetException(new Exception(SR.GetString("net_nonClsCompliantException")));
			}
			finally
			{
				if (this._Exception != null)
				{
					this.FinishHandshake(null, null);
				}
			}
		}

		// Token: 0x06002C8E RID: 11406 RVA: 0x000BFB14 File Offset: 0x000BEB14
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
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x000BFB9C File Offset: 0x000BEB9C
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

		// Token: 0x06002C90 RID: 11408 RVA: 0x000BFBD8 File Offset: 0x000BEBD8
		private void StartSendBlob(byte[] incoming, int count, AsyncProtocolRequest asyncRequest)
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
				if (protocolToken.Done && this._ForceBufferingLastHandshakePayload && this.InnerStream.GetType() == typeof(NetworkStream) && !this._PendingReHandshake && !this.CheckWin9xCachedSession())
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

		// Token: 0x06002C91 RID: 11409 RVA: 0x000BFD00 File Offset: 0x000BED00
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
			if (this.CheckWin9xCachedSession())
			{
				this._PendingReHandshake = true;
				this.Win9xSessionRestarted();
				this.ForceAuthentication(false, null, asyncRequest);
				return;
			}
			if (!this.CompleteHandshake())
			{
				this.StartSendAuthResetSignal(null, asyncRequest, new AuthenticationException(SR.GetString("net_ssl_io_cert_validation"), null));
				return;
			}
			this.FinishHandshake(null, asyncRequest);
		}

		// Token: 0x06002C92 RID: 11410 RVA: 0x000BFD9C File Offset: 0x000BED9C
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

		// Token: 0x06002C93 RID: 11411 RVA: 0x000BFE24 File Offset: 0x000BEE24
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

		// Token: 0x06002C94 RID: 11412 RVA: 0x000BFEE8 File Offset: 0x000BEEE8
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
			this.StartSendBlob(buffer, count, asyncRequest);
		}

		// Token: 0x06002C95 RID: 11413 RVA: 0x000BFF94 File Offset: 0x000BEF94
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

		// Token: 0x06002C96 RID: 11414 RVA: 0x000C0008 File Offset: 0x000BF008
		private bool CheckWin9xCachedSession()
		{
			if (ComNetOS.IsWin9x && this._CachedSession == SslState.CachedSessionStatus.IsCached && this.Context.IsServer && this.Context.RemoteCertRequired)
			{
				X509Certificate2 x509Certificate = null;
				try
				{
					X509Certificate2Collection x509Certificate2Collection;
					x509Certificate = this.Context.GetRemoteCertificate(out x509Certificate2Collection);
					if (x509Certificate == null)
					{
						return true;
					}
				}
				finally
				{
					if (x509Certificate != null)
					{
						x509Certificate.Reset();
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06002C97 RID: 11415 RVA: 0x000C0078 File Offset: 0x000BF078
		private void Win9xSessionRestarted()
		{
			this._CachedSession = SslState.CachedSessionStatus.Renegotiated;
		}

		// Token: 0x06002C98 RID: 11416 RVA: 0x000C0081 File Offset: 0x000BF081
		private bool CompleteHandshake()
		{
			this.Context.ProcessHandshakeSuccess();
			if (!this.Context.VerifyRemoteCertificate(this._CertValidationDelegate))
			{
				this._HandshakeCompleted = false;
				this._CertValidationFailed = true;
				return false;
			}
			this._CertValidationFailed = false;
			this._HandshakeCompleted = true;
			return true;
		}

		// Token: 0x06002C99 RID: 11417 RVA: 0x000C00C0 File Offset: 0x000BF0C0
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
			catch
			{
				if (asyncProtocolRequest.IsUserCompleted)
				{
					throw;
				}
				sslState.FinishHandshake(new Exception(SR.GetString("net_nonClsCompliantException")), asyncProtocolRequest);
			}
		}

		// Token: 0x06002C9A RID: 11418 RVA: 0x000C0174 File Offset: 0x000BF174
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
			catch
			{
				if (asyncRequest.IsUserCompleted)
				{
					throw;
				}
				sslState.FinishHandshake(new Exception(SR.GetString("net_nonClsCompliantException")), asyncRequest);
			}
		}

		// Token: 0x06002C9B RID: 11419 RVA: 0x000C01F8 File Offset: 0x000BF1F8
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
			catch
			{
				if (asyncRequest.IsUserCompleted)
				{
					throw;
				}
				sslState.FinishHandshake(new Exception(SR.GetString("net_nonClsCompliantException")), asyncRequest);
			}
		}

		// Token: 0x06002C9C RID: 11420 RVA: 0x000C0290 File Offset: 0x000BF290
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

		// Token: 0x06002C9D RID: 11421 RVA: 0x000C0318 File Offset: 0x000BF318
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

		// Token: 0x06002C9E RID: 11422 RVA: 0x000C039C File Offset: 0x000BF39C
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

		// Token: 0x06002C9F RID: 11423 RVA: 0x000C0468 File Offset: 0x000BF468
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

		// Token: 0x06002CA0 RID: 11424 RVA: 0x000C04F8 File Offset: 0x000BF4F8
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
				if (this._LockWriteState == 1)
				{
					this.CheckThrow(true);
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
			this.CheckThrow(true);
			return false;
		}

		// Token: 0x06002CA1 RID: 11425 RVA: 0x000C05A8 File Offset: 0x000BF5A8
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

		// Token: 0x06002CA2 RID: 11426 RVA: 0x000C0640 File Offset: 0x000BF640
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
							goto IL_5F;
						}
					}
					return false;
				}
				int num3 = Interlocked.Exchange(ref this._LockWriteState, 2);
				if (num3 != 1)
				{
					return false;
				}
				IL_5F:
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

		// Token: 0x06002CA3 RID: 11427 RVA: 0x000C06F8 File Offset: 0x000BF6F8
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

		// Token: 0x06002CA4 RID: 11428 RVA: 0x000C07B4 File Offset: 0x000BF7B4
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

		// Token: 0x06002CA5 RID: 11429 RVA: 0x000C07E8 File Offset: 0x000BF7E8
		private SslState.Framing DetectFraming(byte[] bytes, int length)
		{
			int num = -1;
			if (bytes[0] == 22 || bytes[0] == 23)
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

		// Token: 0x06002CA6 RID: 11430 RVA: 0x000C08A4 File Offset: 0x000BF8A4
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

		// Token: 0x06002CA7 RID: 11431 RVA: 0x000C0940 File Offset: 0x000BF940
		private void AsyncResumeHandshake(object state)
		{
			AsyncProtocolRequest asyncProtocolRequest = state as AsyncProtocolRequest;
			this.ForceAuthentication(this.Context.IsServer, asyncProtocolRequest.Buffer, asyncProtocolRequest);
		}

		// Token: 0x06002CA8 RID: 11432 RVA: 0x000C096C File Offset: 0x000BF96C
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
			catch
			{
				if (asyncProtocolRequest.IsUserCompleted)
				{
					throw;
				}
				this.FinishHandshake(new Exception(SR.GetString("net_nonClsCompliantException")), asyncProtocolRequest);
			}
		}

		// Token: 0x06002CA9 RID: 11433 RVA: 0x000C0A10 File Offset: 0x000BFA10
		private void CompleteRequestWaitCallback(object state)
		{
			AsyncProtocolRequest asyncProtocolRequest = (AsyncProtocolRequest)state;
			if (asyncProtocolRequest.MustCompleteSynchronously)
			{
				throw new InternalException();
			}
			asyncProtocolRequest.CompleteRequest(0);
		}

		// Token: 0x06002CAA RID: 11434 RVA: 0x000C0A3C File Offset: 0x000BFA3C
		private void RehandshakeCompleteCallback(IAsyncResult result)
		{
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)result;
			Exception ex = lazyAsyncResult.InternalWaitForCompletion() as Exception;
			if (ex != null)
			{
				this.FinishHandshake(ex, null);
			}
		}

		// Token: 0x04002A30 RID: 10800
		private const int _ConstMaxQueuedReadBytes = 131072;

		// Token: 0x04002A31 RID: 10801
		private const int LockNone = 0;

		// Token: 0x04002A32 RID: 10802
		private const int LockWrite = 1;

		// Token: 0x04002A33 RID: 10803
		private const int LockHandshake = 2;

		// Token: 0x04002A34 RID: 10804
		private const int LockPendingWrite = 3;

		// Token: 0x04002A35 RID: 10805
		private const int LockPendingHandshake = 4;

		// Token: 0x04002A36 RID: 10806
		private const int LockRead = 5;

		// Token: 0x04002A37 RID: 10807
		private const int LockPendingRead = 6;

		// Token: 0x04002A38 RID: 10808
		private static int UniqueNameInteger = 123;

		// Token: 0x04002A39 RID: 10809
		private static AsyncProtocolCallback _PartialFrameCallback = new AsyncProtocolCallback(SslState.PartialFrameCallback);

		// Token: 0x04002A3A RID: 10810
		private static AsyncProtocolCallback _ReadFrameCallback = new AsyncProtocolCallback(SslState.ReadFrameCallback);

		// Token: 0x04002A3B RID: 10811
		private static AsyncCallback _WriteCallback = new AsyncCallback(SslState.WriteCallback);

		// Token: 0x04002A3C RID: 10812
		private RemoteCertValidationCallback _CertValidationDelegate;

		// Token: 0x04002A3D RID: 10813
		private LocalCertSelectionCallback _CertSelectionDelegate;

		// Token: 0x04002A3E RID: 10814
		private bool _CanRetryAuthentication;

		// Token: 0x04002A3F RID: 10815
		private Stream _InnerStream;

		// Token: 0x04002A40 RID: 10816
		private _SslStream _SecureStream;

		// Token: 0x04002A41 RID: 10817
		private FixedSizeReader _Reader;

		// Token: 0x04002A42 RID: 10818
		private int _NestedAuth;

		// Token: 0x04002A43 RID: 10819
		private SecureChannel _Context;

		// Token: 0x04002A44 RID: 10820
		private bool _HandshakeCompleted;

		// Token: 0x04002A45 RID: 10821
		private bool _CertValidationFailed;

		// Token: 0x04002A46 RID: 10822
		private SecurityStatus _SecurityStatus;

		// Token: 0x04002A47 RID: 10823
		private Exception _Exception;

		// Token: 0x04002A48 RID: 10824
		private SslState.CachedSessionStatus _CachedSession;

		// Token: 0x04002A49 RID: 10825
		private byte[] _QueuedReadData;

		// Token: 0x04002A4A RID: 10826
		private int _QueuedReadCount;

		// Token: 0x04002A4B RID: 10827
		private bool _PendingReHandshake;

		// Token: 0x04002A4C RID: 10828
		private int _LockWriteState;

		// Token: 0x04002A4D RID: 10829
		private object _QueuedWriteStateRequest;

		// Token: 0x04002A4E RID: 10830
		private int _LockReadState;

		// Token: 0x04002A4F RID: 10831
		private object _QueuedReadStateRequest;

		// Token: 0x04002A50 RID: 10832
		private bool _ForceBufferingLastHandshakePayload;

		// Token: 0x04002A51 RID: 10833
		private byte[] _LastPayload;

		// Token: 0x04002A52 RID: 10834
		private SslState.Framing _Framing;

		// Token: 0x0200059F RID: 1439
		private enum CachedSessionStatus : byte
		{
			// Token: 0x04002A54 RID: 10836
			Unknown,
			// Token: 0x04002A55 RID: 10837
			IsNotCached,
			// Token: 0x04002A56 RID: 10838
			IsCached,
			// Token: 0x04002A57 RID: 10839
			Renegotiated
		}

		// Token: 0x020005A0 RID: 1440
		private enum Framing
		{
			// Token: 0x04002A59 RID: 10841
			None,
			// Token: 0x04002A5A RID: 10842
			BeforeSSL3,
			// Token: 0x04002A5B RID: 10843
			SinceSSL3,
			// Token: 0x04002A5C RID: 10844
			Unified,
			// Token: 0x04002A5D RID: 10845
			Invalid
		}

		// Token: 0x020005A1 RID: 1441
		private enum FrameType : byte
		{
			// Token: 0x04002A5F RID: 10847
			ChangeCipherSpec = 20,
			// Token: 0x04002A60 RID: 10848
			Alert,
			// Token: 0x04002A61 RID: 10849
			Handshake,
			// Token: 0x04002A62 RID: 10850
			AppData
		}
	}
}
