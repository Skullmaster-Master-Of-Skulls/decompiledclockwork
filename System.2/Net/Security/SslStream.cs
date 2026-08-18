using System;
using System.IO;
using System.Security.Authentication;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace System.Net.Security
{
	// Token: 0x0200035E RID: 862
	public class SslStream : AuthenticatedStream
	{
		// Token: 0x06001EFA RID: 7930 RVA: 0x00090F18 File Offset: 0x0008F118
		public SslStream(Stream innerStream) : this(innerStream, false, null, null)
		{
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x00090F24 File Offset: 0x0008F124
		public SslStream(Stream innerStream, bool leaveInnerStreamOpen) : this(innerStream, leaveInnerStreamOpen, null, null, EncryptionPolicy.RequireEncryption)
		{
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x00090F31 File Offset: 0x0008F131
		public SslStream(Stream innerStream, bool leaveInnerStreamOpen, RemoteCertificateValidationCallback userCertificateValidationCallback) : this(innerStream, leaveInnerStreamOpen, userCertificateValidationCallback, null, EncryptionPolicy.RequireEncryption)
		{
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x00090F3E File Offset: 0x0008F13E
		public SslStream(Stream innerStream, bool leaveInnerStreamOpen, RemoteCertificateValidationCallback userCertificateValidationCallback, LocalCertificateSelectionCallback userCertificateSelectionCallback) : this(innerStream, leaveInnerStreamOpen, userCertificateValidationCallback, userCertificateSelectionCallback, EncryptionPolicy.RequireEncryption)
		{
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x00090F4C File Offset: 0x0008F14C
		public SslStream(Stream innerStream, bool leaveInnerStreamOpen, RemoteCertificateValidationCallback userCertificateValidationCallback, LocalCertificateSelectionCallback userCertificateSelectionCallback, EncryptionPolicy encryptionPolicy) : base(innerStream, leaveInnerStreamOpen)
		{
			if (encryptionPolicy != EncryptionPolicy.RequireEncryption && encryptionPolicy != EncryptionPolicy.AllowNoEncryption && encryptionPolicy != EncryptionPolicy.NoEncryption)
			{
				throw new ArgumentException(SR.GetString("net_invalid_enum", new object[]
				{
					"EncryptionPolicy"
				}), "encryptionPolicy");
			}
			this._userCertificateValidationCallback = userCertificateValidationCallback;
			this._userCertificateSelectionCallback = userCertificateSelectionCallback;
			RemoteCertValidationCallback certValidationCallback = new RemoteCertValidationCallback(this.userCertValidationCallbackWrapper);
			LocalCertSelectionCallback certSelectionCallback = (userCertificateSelectionCallback == null) ? null : new LocalCertSelectionCallback(this.userCertSelectionCallbackWrapper);
			this._SslState = new SslState(innerStream, certValidationCallback, certSelectionCallback, encryptionPolicy);
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x00090FD4 File Offset: 0x0008F1D4
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

		// Token: 0x06001F00 RID: 7936 RVA: 0x00091025 File Offset: 0x0008F225
		private X509Certificate userCertSelectionCallbackWrapper(string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers)
		{
			return this._userCertificateSelectionCallback(this, targetHost, localCertificates, remoteCertificate, acceptableIssuers);
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x00091038 File Offset: 0x0008F238
		public virtual void AuthenticateAsClient(string targetHost)
		{
			this.AuthenticateAsClient(targetHost, new X509CertificateCollection(), ServicePointManager.DefaultSslProtocols, false);
		}

		// Token: 0x06001F02 RID: 7938 RVA: 0x0009104C File Offset: 0x0008F24C
		public virtual void AuthenticateAsClient(string targetHost, X509CertificateCollection clientCertificates, bool checkCertificateRevocation)
		{
			this.AuthenticateAsClient(targetHost, clientCertificates, ServicePointManager.DefaultSslProtocols, !LocalAppContextSwitches.DontCheckCertificateRevocation && checkCertificateRevocation);
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x00091066 File Offset: 0x0008F266
		public virtual void AuthenticateAsClient(string targetHost, X509CertificateCollection clientCertificates, SslProtocols enabledSslProtocols, bool checkCertificateRevocation)
		{
			this._SslState.ValidateCreateContext(false, targetHost, enabledSslProtocols, null, clientCertificates, true, checkCertificateRevocation);
			this._SslState.ProcessAuthentication(null);
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x00091087 File Offset: 0x0008F287
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsClient(string targetHost, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsClient(targetHost, new X509CertificateCollection(), ServicePointManager.DefaultSslProtocols, false, asyncCallback, asyncState);
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x0009109D File Offset: 0x0008F29D
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsClient(string targetHost, X509CertificateCollection clientCertificates, bool checkCertificateRevocation, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsClient(targetHost, clientCertificates, ServicePointManager.DefaultSslProtocols, checkCertificateRevocation, asyncCallback, asyncState);
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x000910B4 File Offset: 0x0008F2B4
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsClient(string targetHost, X509CertificateCollection clientCertificates, SslProtocols enabledSslProtocols, bool checkCertificateRevocation, AsyncCallback asyncCallback, object asyncState)
		{
			this._SslState.ValidateCreateContext(false, targetHost, enabledSslProtocols, null, clientCertificates, true, checkCertificateRevocation);
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(this._SslState, asyncState, asyncCallback);
			this._SslState.ProcessAuthentication(lazyAsyncResult);
			return lazyAsyncResult;
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x000910F1 File Offset: 0x0008F2F1
		public virtual void EndAuthenticateAsClient(IAsyncResult asyncResult)
		{
			this._SslState.EndProcessAuthentication(asyncResult);
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x000910FF File Offset: 0x0008F2FF
		public virtual void AuthenticateAsServer(X509Certificate serverCertificate)
		{
			this.AuthenticateAsServer(serverCertificate, false, ServicePointManager.DefaultSslProtocols, false);
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x0009110F File Offset: 0x0008F30F
		public virtual void AuthenticateAsServer(X509Certificate serverCertificate, bool clientCertificateRequired, bool checkCertificateRevocation)
		{
			this.AuthenticateAsServer(serverCertificate, clientCertificateRequired, ServicePointManager.DefaultSslProtocols, checkCertificateRevocation);
		}

		// Token: 0x06001F0A RID: 7946 RVA: 0x0009111F File Offset: 0x0008F31F
		public virtual void AuthenticateAsServer(X509Certificate serverCertificate, bool clientCertificateRequired, SslProtocols enabledSslProtocols, bool checkCertificateRevocation)
		{
			this._SslState.ValidateCreateContext(true, string.Empty, enabledSslProtocols, serverCertificate, null, clientCertificateRequired, checkCertificateRevocation);
			this._SslState.ProcessAuthentication(null);
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x00091144 File Offset: 0x0008F344
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsServer(X509Certificate serverCertificate, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsServer(serverCertificate, false, ServicePointManager.DefaultSslProtocols, false, asyncCallback, asyncState);
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x00091156 File Offset: 0x0008F356
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsServer(X509Certificate serverCertificate, bool clientCertificateRequired, bool checkCertificateRevocation, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsServer(serverCertificate, clientCertificateRequired, ServicePointManager.DefaultSslProtocols, checkCertificateRevocation, asyncCallback, asyncState);
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x0009116C File Offset: 0x0008F36C
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsServer(X509Certificate serverCertificate, bool clientCertificateRequired, SslProtocols enabledSslProtocols, bool checkCertificateRevocation, AsyncCallback asyncCallback, object asyncState)
		{
			this._SslState.ValidateCreateContext(true, string.Empty, enabledSslProtocols, serverCertificate, null, clientCertificateRequired, checkCertificateRevocation);
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(this._SslState, asyncState, asyncCallback);
			this._SslState.ProcessAuthentication(lazyAsyncResult);
			return lazyAsyncResult;
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x000911AD File Offset: 0x0008F3AD
		public virtual void EndAuthenticateAsServer(IAsyncResult asyncResult)
		{
			this._SslState.EndProcessAuthentication(asyncResult);
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x000911BB File Offset: 0x0008F3BB
		internal virtual IAsyncResult BeginShutdown(AsyncCallback asyncCallback, object asyncState)
		{
			return this._SslState.BeginShutdown(asyncCallback, asyncState);
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x000911CA File Offset: 0x0008F3CA
		internal virtual void EndShutdown(IAsyncResult asyncResult)
		{
			this._SslState.EndShutdown(asyncResult);
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06001F11 RID: 7953 RVA: 0x000911D8 File Offset: 0x0008F3D8
		public TransportContext TransportContext
		{
			get
			{
				return new SslStreamContext(this);
			}
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x000911E0 File Offset: 0x0008F3E0
		internal ChannelBinding GetChannelBinding(ChannelBindingKind kind)
		{
			return this._SslState.GetChannelBinding(kind);
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x000911EE File Offset: 0x0008F3EE
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task AuthenticateAsClientAsync(string targetHost)
		{
			return Task.Factory.FromAsync<string>(new Func<string, AsyncCallback, object, IAsyncResult>(this.BeginAuthenticateAsClient), new Action<IAsyncResult>(this.EndAuthenticateAsClient), targetHost, null);
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x00091216 File Offset: 0x0008F416
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task AuthenticateAsClientAsync(string targetHost, X509CertificateCollection clientCertificates, bool checkCertificateRevocation)
		{
			return this.AuthenticateAsClientAsync(targetHost, clientCertificates, ServicePointManager.DefaultSslProtocols, checkCertificateRevocation);
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x00091228 File Offset: 0x0008F428
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task AuthenticateAsClientAsync(string targetHost, X509CertificateCollection clientCertificates, SslProtocols enabledSslProtocols, bool checkCertificateRevocation)
		{
			return Task.Factory.FromAsync((AsyncCallback callback, object state) => this.BeginAuthenticateAsClient(targetHost, clientCertificates, enabledSslProtocols, checkCertificateRevocation, callback, state), new Action<IAsyncResult>(this.EndAuthenticateAsClient), null);
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x00091283 File Offset: 0x0008F483
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task AuthenticateAsServerAsync(X509Certificate serverCertificate)
		{
			return Task.Factory.FromAsync<X509Certificate>(new Func<X509Certificate, AsyncCallback, object, IAsyncResult>(this.BeginAuthenticateAsServer), new Action<IAsyncResult>(this.EndAuthenticateAsServer), serverCertificate, null);
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x000912AB File Offset: 0x0008F4AB
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task AuthenticateAsServerAsync(X509Certificate serverCertificate, bool clientCertificateRequired, bool checkCertificateRevocation)
		{
			return this.AuthenticateAsServerAsync(serverCertificate, clientCertificateRequired, ServicePointManager.DefaultSslProtocols, checkCertificateRevocation);
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x000912BC File Offset: 0x0008F4BC
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task AuthenticateAsServerAsync(X509Certificate serverCertificate, bool clientCertificateRequired, SslProtocols enabledSslProtocols, bool checkCertificateRevocation)
		{
			return Task.Factory.FromAsync((AsyncCallback callback, object state) => this.BeginAuthenticateAsServer(serverCertificate, clientCertificateRequired, enabledSslProtocols, checkCertificateRevocation, callback, state), new Action<IAsyncResult>(this.EndAuthenticateAsServer), null);
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x00091317 File Offset: 0x0008F517
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task ShutdownAsync()
		{
			return Task.Factory.FromAsync((AsyncCallback callback, object state) => this.BeginShutdown(callback, state), new Action<IAsyncResult>(this.EndShutdown), null);
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06001F1A RID: 7962 RVA: 0x0009133D File Offset: 0x0008F53D
		public override bool IsAuthenticated
		{
			get
			{
				return this._SslState.IsAuthenticated;
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06001F1B RID: 7963 RVA: 0x0009134A File Offset: 0x0008F54A
		public override bool IsMutuallyAuthenticated
		{
			get
			{
				return this._SslState.IsMutuallyAuthenticated;
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06001F1C RID: 7964 RVA: 0x00091357 File Offset: 0x0008F557
		public override bool IsEncrypted
		{
			get
			{
				return this.IsAuthenticated;
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06001F1D RID: 7965 RVA: 0x0009135F File Offset: 0x0008F55F
		public override bool IsSigned
		{
			get
			{
				return this.IsAuthenticated;
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06001F1E RID: 7966 RVA: 0x00091367 File Offset: 0x0008F567
		public override bool IsServer
		{
			get
			{
				return this._SslState.IsServer;
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06001F1F RID: 7967 RVA: 0x00091374 File Offset: 0x0008F574
		public virtual SslProtocols SslProtocol
		{
			get
			{
				return this._SslState.SslProtocol;
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06001F20 RID: 7968 RVA: 0x00091381 File Offset: 0x0008F581
		public virtual bool CheckCertRevocationStatus
		{
			get
			{
				return this._SslState.CheckCertRevocationStatus;
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06001F21 RID: 7969 RVA: 0x0009138E File Offset: 0x0008F58E
		public virtual X509Certificate LocalCertificate
		{
			get
			{
				return this._SslState.LocalCertificate;
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06001F22 RID: 7970 RVA: 0x0009139C File Offset: 0x0008F59C
		public virtual X509Certificate RemoteCertificate
		{
			get
			{
				this._SslState.CheckThrow(true, false);
				object remoteCertificateOrBytes = this.m_RemoteCertificateOrBytes;
				if (remoteCertificateOrBytes != null && remoteCertificateOrBytes.GetType() == typeof(byte[]))
				{
					return (X509Certificate)(this.m_RemoteCertificateOrBytes = new X509Certificate((byte[])remoteCertificateOrBytes));
				}
				return remoteCertificateOrBytes as X509Certificate;
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06001F23 RID: 7971 RVA: 0x000913F7 File Offset: 0x0008F5F7
		public virtual CipherAlgorithmType CipherAlgorithm
		{
			get
			{
				return this._SslState.CipherAlgorithm;
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06001F24 RID: 7972 RVA: 0x00091404 File Offset: 0x0008F604
		public virtual int CipherStrength
		{
			get
			{
				return this._SslState.CipherStrength;
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06001F25 RID: 7973 RVA: 0x00091411 File Offset: 0x0008F611
		public virtual HashAlgorithmType HashAlgorithm
		{
			get
			{
				return this._SslState.HashAlgorithm;
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06001F26 RID: 7974 RVA: 0x0009141E File Offset: 0x0008F61E
		public virtual int HashStrength
		{
			get
			{
				return this._SslState.HashStrength;
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06001F27 RID: 7975 RVA: 0x0009142B File Offset: 0x0008F62B
		public virtual ExchangeAlgorithmType KeyExchangeAlgorithm
		{
			get
			{
				return this._SslState.KeyExchangeAlgorithm;
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06001F28 RID: 7976 RVA: 0x00091438 File Offset: 0x0008F638
		public virtual int KeyExchangeStrength
		{
			get
			{
				return this._SslState.KeyExchangeStrength;
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06001F29 RID: 7977 RVA: 0x00091445 File Offset: 0x0008F645
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06001F2A RID: 7978 RVA: 0x00091448 File Offset: 0x0008F648
		public override bool CanRead
		{
			get
			{
				return this._SslState.IsAuthenticated && base.InnerStream.CanRead;
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06001F2B RID: 7979 RVA: 0x00091464 File Offset: 0x0008F664
		public override bool CanTimeout
		{
			get
			{
				return base.InnerStream.CanTimeout;
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06001F2C RID: 7980 RVA: 0x00091471 File Offset: 0x0008F671
		public override bool CanWrite
		{
			get
			{
				return this._SslState.IsAuthenticated && base.InnerStream.CanWrite && !this._SslState.IsShutdown;
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06001F2D RID: 7981 RVA: 0x0009149D File Offset: 0x0008F69D
		// (set) Token: 0x06001F2E RID: 7982 RVA: 0x000914AA File Offset: 0x0008F6AA
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

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06001F2F RID: 7983 RVA: 0x000914B8 File Offset: 0x0008F6B8
		// (set) Token: 0x06001F30 RID: 7984 RVA: 0x000914C5 File Offset: 0x0008F6C5
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

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06001F31 RID: 7985 RVA: 0x000914D3 File Offset: 0x0008F6D3
		public override long Length
		{
			get
			{
				return base.InnerStream.Length;
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06001F32 RID: 7986 RVA: 0x000914E0 File Offset: 0x0008F6E0
		// (set) Token: 0x06001F33 RID: 7987 RVA: 0x000914ED File Offset: 0x0008F6ED
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

		// Token: 0x06001F34 RID: 7988 RVA: 0x000914FE File Offset: 0x0008F6FE
		public override void SetLength(long value)
		{
			base.InnerStream.SetLength(value);
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x0009150C File Offset: 0x0008F70C
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x0009151D File Offset: 0x0008F71D
		public override void Flush()
		{
			this._SslState.Flush();
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x0009152C File Offset: 0x0008F72C
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

		// Token: 0x06001F38 RID: 7992 RVA: 0x00091560 File Offset: 0x0008F760
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this._SslState.SecureStream.Read(buffer, offset, count);
		}

		// Token: 0x06001F39 RID: 7993 RVA: 0x00091575 File Offset: 0x0008F775
		public void Write(byte[] buffer)
		{
			this._SslState.SecureStream.Write(buffer, 0, buffer.Length);
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x0009158C File Offset: 0x0008F78C
		public override void Write(byte[] buffer, int offset, int count)
		{
			this._SslState.SecureStream.Write(buffer, offset, count);
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x000915A1 File Offset: 0x0008F7A1
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			return this._SslState.SecureStream.BeginRead(buffer, offset, count, asyncCallback, asyncState);
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x000915BA File Offset: 0x0008F7BA
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this._SslState.SecureStream.EndRead(asyncResult);
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x000915CD File Offset: 0x0008F7CD
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			return this._SslState.SecureStream.BeginWrite(buffer, offset, count, asyncCallback, asyncState);
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x000915E6 File Offset: 0x0008F7E6
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this._SslState.SecureStream.EndWrite(asyncResult);
		}

		// Token: 0x04001D10 RID: 7440
		private SslState _SslState;

		// Token: 0x04001D11 RID: 7441
		private RemoteCertificateValidationCallback _userCertificateValidationCallback;

		// Token: 0x04001D12 RID: 7442
		private LocalCertificateSelectionCallback _userCertificateSelectionCallback;

		// Token: 0x04001D13 RID: 7443
		private object m_RemoteCertificateOrBytes;
	}
}
