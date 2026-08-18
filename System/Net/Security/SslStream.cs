using System;
using System.IO;
using System.Security.Authentication;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Net.Security
{
	// Token: 0x02000598 RID: 1432
	public class SslStream : AuthenticatedStream
	{
		// Token: 0x06002BF5 RID: 11253 RVA: 0x000BDB34 File Offset: 0x000BCB34
		public SslStream(Stream innerStream) : this(innerStream, false, null, null)
		{
		}

		// Token: 0x06002BF6 RID: 11254 RVA: 0x000BDB40 File Offset: 0x000BCB40
		public SslStream(Stream innerStream, bool leaveInnerStreamOpen) : this(innerStream, leaveInnerStreamOpen, null, null)
		{
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x000BDB4C File Offset: 0x000BCB4C
		public SslStream(Stream innerStream, bool leaveInnerStreamOpen, RemoteCertificateValidationCallback userCertificateValidationCallback) : this(innerStream, leaveInnerStreamOpen, userCertificateValidationCallback, null)
		{
		}

		// Token: 0x06002BF8 RID: 11256 RVA: 0x000BDB58 File Offset: 0x000BCB58
		public SslStream(Stream innerStream, bool leaveInnerStreamOpen, RemoteCertificateValidationCallback userCertificateValidationCallback, LocalCertificateSelectionCallback userCertificateSelectionCallback) : base(innerStream, leaveInnerStreamOpen)
		{
			this._userCertificateValidationCallback = userCertificateValidationCallback;
			this._userCertificateSelectionCallback = userCertificateSelectionCallback;
			RemoteCertValidationCallback certValidationCallback = new RemoteCertValidationCallback(this.userCertValidationCallbackWrapper);
			LocalCertSelectionCallback certSelectionCallback = (userCertificateSelectionCallback == null) ? null : new LocalCertSelectionCallback(this.userCertSelectionCallbackWrapper);
			this._SslState = new SslState(innerStream, certValidationCallback, certSelectionCallback);
		}

		// Token: 0x06002BF9 RID: 11257 RVA: 0x000BDBAC File Offset: 0x000BCBAC
		private bool userCertValidationCallbackWrapper(string hostName, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			this.m_RemoteCertificateOrBytes = ((certificate == null) ? null : certificate.GetRawCertData());
			if (this._userCertificateValidationCallback == null)
			{
				if (!this._SslState.RemoteCertRequired)
				{
					sslPolicyErrors &= ~SslPolicyErrors.RemoteCertificateNotAvailable;
				}
				return sslPolicyErrors == SslPolicyErrors.None;
			}
			return this._userCertificateValidationCallback(this, certificate, chain, sslPolicyErrors);
		}

		// Token: 0x06002BFA RID: 11258 RVA: 0x000BDBFD File Offset: 0x000BCBFD
		private X509Certificate userCertSelectionCallbackWrapper(string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers)
		{
			return this._userCertificateSelectionCallback(this, targetHost, localCertificates, remoteCertificate, acceptableIssuers);
		}

		// Token: 0x06002BFB RID: 11259 RVA: 0x000BDC10 File Offset: 0x000BCC10
		public virtual void AuthenticateAsClient(string targetHost)
		{
			this.AuthenticateAsClient(targetHost, new X509CertificateCollection(), ServicePointManager.DefaultSslProtocols, false);
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x000BDC24 File Offset: 0x000BCC24
		public virtual void AuthenticateAsClient(string targetHost, X509CertificateCollection clientCertificates, SslProtocols enabledSslProtocols, bool checkCertificateRevocation)
		{
			this._SslState.ValidateCreateContext(false, targetHost, enabledSslProtocols, null, clientCertificates, true, checkCertificateRevocation);
			this._SslState.ProcessAuthentication(null);
		}

		// Token: 0x06002BFD RID: 11261 RVA: 0x000BDC45 File Offset: 0x000BCC45
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsClient(string targetHost, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsClient(targetHost, new X509CertificateCollection(), ServicePointManager.DefaultSslProtocols, false, asyncCallback, asyncState);
		}

		// Token: 0x06002BFE RID: 11262 RVA: 0x000BDC5C File Offset: 0x000BCC5C
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsClient(string targetHost, X509CertificateCollection clientCertificates, SslProtocols enabledSslProtocols, bool checkCertificateRevocation, AsyncCallback asyncCallback, object asyncState)
		{
			this._SslState.ValidateCreateContext(false, targetHost, enabledSslProtocols, null, clientCertificates, true, checkCertificateRevocation);
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(this._SslState, asyncState, asyncCallback);
			this._SslState.ProcessAuthentication(lazyAsyncResult);
			return lazyAsyncResult;
		}

		// Token: 0x06002BFF RID: 11263 RVA: 0x000BDC99 File Offset: 0x000BCC99
		public virtual void EndAuthenticateAsClient(IAsyncResult asyncResult)
		{
			this._SslState.EndProcessAuthentication(asyncResult);
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x000BDCA7 File Offset: 0x000BCCA7
		public virtual void AuthenticateAsServer(X509Certificate serverCertificate)
		{
			this.AuthenticateAsServer(serverCertificate, false, ServicePointManager.DefaultSslProtocols, false);
		}

		// Token: 0x06002C01 RID: 11265 RVA: 0x000BDCB7 File Offset: 0x000BCCB7
		public virtual void AuthenticateAsServer(X509Certificate serverCertificate, bool clientCertificateRequired, SslProtocols enabledSslProtocols, bool checkCertificateRevocation)
		{
			if (!ComNetOS.IsWin2K)
			{
				throw new PlatformNotSupportedException(SR.GetString("Win2000Required"));
			}
			this._SslState.ValidateCreateContext(true, string.Empty, enabledSslProtocols, serverCertificate, null, clientCertificateRequired, checkCertificateRevocation);
			this._SslState.ProcessAuthentication(null);
		}

		// Token: 0x06002C02 RID: 11266 RVA: 0x000BDCF3 File Offset: 0x000BCCF3
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsServer(X509Certificate serverCertificate, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsServer(serverCertificate, false, ServicePointManager.DefaultSslProtocols, false, asyncCallback, asyncState);
		}

		// Token: 0x06002C03 RID: 11267 RVA: 0x000BDD08 File Offset: 0x000BCD08
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsServer(X509Certificate serverCertificate, bool clientCertificateRequired, SslProtocols enabledSslProtocols, bool checkCertificateRevocation, AsyncCallback asyncCallback, object asyncState)
		{
			if (!ComNetOS.IsWin2K)
			{
				throw new PlatformNotSupportedException(SR.GetString("Win2000Required"));
			}
			this._SslState.ValidateCreateContext(true, string.Empty, enabledSslProtocols, serverCertificate, null, clientCertificateRequired, checkCertificateRevocation);
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(this._SslState, asyncState, asyncCallback);
			this._SslState.ProcessAuthentication(lazyAsyncResult);
			return lazyAsyncResult;
		}

		// Token: 0x06002C04 RID: 11268 RVA: 0x000BDD60 File Offset: 0x000BCD60
		public virtual void EndAuthenticateAsServer(IAsyncResult asyncResult)
		{
			this._SslState.EndProcessAuthentication(asyncResult);
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06002C05 RID: 11269 RVA: 0x000BDD6E File Offset: 0x000BCD6E
		public TransportContext TransportContext
		{
			get
			{
				return new SslStreamContext(this);
			}
		}

		// Token: 0x06002C06 RID: 11270 RVA: 0x000BDD76 File Offset: 0x000BCD76
		internal ChannelBinding GetChannelBinding(ChannelBindingKind kind)
		{
			return this._SslState.GetChannelBinding(kind);
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06002C07 RID: 11271 RVA: 0x000BDD84 File Offset: 0x000BCD84
		public override bool IsAuthenticated
		{
			get
			{
				return this._SslState.IsAuthenticated;
			}
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06002C08 RID: 11272 RVA: 0x000BDD91 File Offset: 0x000BCD91
		public override bool IsMutuallyAuthenticated
		{
			get
			{
				return this._SslState.IsMutuallyAuthenticated;
			}
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06002C09 RID: 11273 RVA: 0x000BDD9E File Offset: 0x000BCD9E
		public override bool IsEncrypted
		{
			get
			{
				return this.IsAuthenticated;
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06002C0A RID: 11274 RVA: 0x000BDDA6 File Offset: 0x000BCDA6
		public override bool IsSigned
		{
			get
			{
				return this.IsAuthenticated;
			}
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06002C0B RID: 11275 RVA: 0x000BDDAE File Offset: 0x000BCDAE
		public override bool IsServer
		{
			get
			{
				return this._SslState.IsServer;
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06002C0C RID: 11276 RVA: 0x000BDDBB File Offset: 0x000BCDBB
		public virtual SslProtocols SslProtocol
		{
			get
			{
				return this._SslState.SslProtocol;
			}
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06002C0D RID: 11277 RVA: 0x000BDDC8 File Offset: 0x000BCDC8
		public virtual bool CheckCertRevocationStatus
		{
			get
			{
				return this._SslState.CheckCertRevocationStatus;
			}
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06002C0E RID: 11278 RVA: 0x000BDDD5 File Offset: 0x000BCDD5
		public virtual X509Certificate LocalCertificate
		{
			get
			{
				return this._SslState.LocalCertificate;
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06002C0F RID: 11279 RVA: 0x000BDDE4 File Offset: 0x000BCDE4
		public virtual X509Certificate RemoteCertificate
		{
			get
			{
				this._SslState.CheckThrow(true);
				object remoteCertificateOrBytes = this.m_RemoteCertificateOrBytes;
				if (remoteCertificateOrBytes != null && remoteCertificateOrBytes.GetType() == typeof(byte[]))
				{
					return (X509Certificate)(this.m_RemoteCertificateOrBytes = new X509Certificate((byte[])remoteCertificateOrBytes));
				}
				return remoteCertificateOrBytes as X509Certificate;
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06002C10 RID: 11280 RVA: 0x000BDE39 File Offset: 0x000BCE39
		public virtual CipherAlgorithmType CipherAlgorithm
		{
			get
			{
				return this._SslState.CipherAlgorithm;
			}
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06002C11 RID: 11281 RVA: 0x000BDE46 File Offset: 0x000BCE46
		public virtual int CipherStrength
		{
			get
			{
				return this._SslState.CipherStrength;
			}
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06002C12 RID: 11282 RVA: 0x000BDE53 File Offset: 0x000BCE53
		public virtual HashAlgorithmType HashAlgorithm
		{
			get
			{
				return this._SslState.HashAlgorithm;
			}
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06002C13 RID: 11283 RVA: 0x000BDE60 File Offset: 0x000BCE60
		public virtual int HashStrength
		{
			get
			{
				return this._SslState.HashStrength;
			}
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06002C14 RID: 11284 RVA: 0x000BDE6D File Offset: 0x000BCE6D
		public virtual ExchangeAlgorithmType KeyExchangeAlgorithm
		{
			get
			{
				return this._SslState.KeyExchangeAlgorithm;
			}
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06002C15 RID: 11285 RVA: 0x000BDE7A File Offset: 0x000BCE7A
		public virtual int KeyExchangeStrength
		{
			get
			{
				return this._SslState.KeyExchangeStrength;
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06002C16 RID: 11286 RVA: 0x000BDE87 File Offset: 0x000BCE87
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06002C17 RID: 11287 RVA: 0x000BDE8A File Offset: 0x000BCE8A
		public override bool CanRead
		{
			get
			{
				return this._SslState.IsAuthenticated && base.InnerStream.CanRead;
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06002C18 RID: 11288 RVA: 0x000BDEA6 File Offset: 0x000BCEA6
		public override bool CanTimeout
		{
			get
			{
				return base.InnerStream.CanTimeout;
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06002C19 RID: 11289 RVA: 0x000BDEB3 File Offset: 0x000BCEB3
		public override bool CanWrite
		{
			get
			{
				return this._SslState.IsAuthenticated && base.InnerStream.CanWrite;
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06002C1A RID: 11290 RVA: 0x000BDECF File Offset: 0x000BCECF
		// (set) Token: 0x06002C1B RID: 11291 RVA: 0x000BDEDC File Offset: 0x000BCEDC
		public override int ReadTimeout
		{
			get
			{
				return base.InnerStream.ReadTimeout;
			}
			set
			{
				base.InnerStream.ReadTimeout = value;
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06002C1C RID: 11292 RVA: 0x000BDEEA File Offset: 0x000BCEEA
		// (set) Token: 0x06002C1D RID: 11293 RVA: 0x000BDEF7 File Offset: 0x000BCEF7
		public override int WriteTimeout
		{
			get
			{
				return base.InnerStream.WriteTimeout;
			}
			set
			{
				base.InnerStream.WriteTimeout = value;
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06002C1E RID: 11294 RVA: 0x000BDF05 File Offset: 0x000BCF05
		public override long Length
		{
			get
			{
				return base.InnerStream.Length;
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06002C1F RID: 11295 RVA: 0x000BDF12 File Offset: 0x000BCF12
		// (set) Token: 0x06002C20 RID: 11296 RVA: 0x000BDF1F File Offset: 0x000BCF1F
		public override long Position
		{
			get
			{
				return base.InnerStream.Position;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("net_noseek"));
			}
		}

		// Token: 0x06002C21 RID: 11297 RVA: 0x000BDF30 File Offset: 0x000BCF30
		public override void SetLength(long value)
		{
			base.InnerStream.SetLength(value);
		}

		// Token: 0x06002C22 RID: 11298 RVA: 0x000BDF3E File Offset: 0x000BCF3E
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06002C23 RID: 11299 RVA: 0x000BDF4F File Offset: 0x000BCF4F
		public override void Flush()
		{
			this._SslState.Flush();
		}

		// Token: 0x06002C24 RID: 11300 RVA: 0x000BDF5C File Offset: 0x000BCF5C
		protected override void Dispose(bool disposing)
		{
			try
			{
				this._SslState.Close();
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06002C25 RID: 11301 RVA: 0x000BDF90 File Offset: 0x000BCF90
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this._SslState.SecureStream.Read(buffer, offset, count);
		}

		// Token: 0x06002C26 RID: 11302 RVA: 0x000BDFA5 File Offset: 0x000BCFA5
		public void Write(byte[] buffer)
		{
			this._SslState.SecureStream.Write(buffer, 0, buffer.Length);
		}

		// Token: 0x06002C27 RID: 11303 RVA: 0x000BDFBC File Offset: 0x000BCFBC
		public override void Write(byte[] buffer, int offset, int count)
		{
			this._SslState.SecureStream.Write(buffer, offset, count);
		}

		// Token: 0x06002C28 RID: 11304 RVA: 0x000BDFD1 File Offset: 0x000BCFD1
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			return this._SslState.SecureStream.BeginRead(buffer, offset, count, asyncCallback, asyncState);
		}

		// Token: 0x06002C29 RID: 11305 RVA: 0x000BDFEA File Offset: 0x000BCFEA
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this._SslState.SecureStream.EndRead(asyncResult);
		}

		// Token: 0x06002C2A RID: 11306 RVA: 0x000BDFFD File Offset: 0x000BCFFD
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			return this._SslState.SecureStream.BeginWrite(buffer, offset, count, asyncCallback, asyncState);
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x000BE016 File Offset: 0x000BD016
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this._SslState.SecureStream.EndWrite(asyncResult);
		}

		// Token: 0x04002A05 RID: 10757
		private SslState _SslState;

		// Token: 0x04002A06 RID: 10758
		private RemoteCertificateValidationCallback _userCertificateValidationCallback;

		// Token: 0x04002A07 RID: 10759
		private LocalCertificateSelectionCallback _userCertificateSelectionCallback;

		// Token: 0x04002A08 RID: 10760
		private object m_RemoteCertificateOrBytes;
	}
}
