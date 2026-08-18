using System;
using System.IO;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;

namespace System.Net.Security
{
	// Token: 0x0200058D RID: 1421
	public class NegotiateStream : AuthenticatedStream
	{
		// Token: 0x06002BA1 RID: 11169 RVA: 0x000BCCBA File Offset: 0x000BBCBA
		public NegotiateStream(Stream innerStream) : this(innerStream, false)
		{
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x000BCCC4 File Offset: 0x000BBCC4
		public NegotiateStream(Stream innerStream, bool leaveInnerStreamOpen) : base(innerStream, leaveInnerStreamOpen)
		{
			this._NegoState = new NegoState(innerStream, leaveInnerStreamOpen);
			this._Package = NegoState.DefaultPackage;
			this.InitializeStreamPart();
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x000BCCEC File Offset: 0x000BBCEC
		public virtual void AuthenticateAsClient()
		{
			this.AuthenticateAsClient((NetworkCredential)CredentialCache.DefaultCredentials, null, string.Empty, ProtectionLevel.EncryptAndSign, TokenImpersonationLevel.Identification);
		}

		// Token: 0x06002BA4 RID: 11172 RVA: 0x000BCD06 File Offset: 0x000BBD06
		public virtual void AuthenticateAsClient(NetworkCredential credential, string targetName)
		{
			this.AuthenticateAsClient(credential, null, targetName, ProtectionLevel.EncryptAndSign, TokenImpersonationLevel.Identification);
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x000BCD13 File Offset: 0x000BBD13
		public virtual void AuthenticateAsClient(NetworkCredential credential, ChannelBinding binding, string targetName)
		{
			this.AuthenticateAsClient(credential, binding, targetName, ProtectionLevel.EncryptAndSign, TokenImpersonationLevel.Identification);
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x000BCD20 File Offset: 0x000BBD20
		public virtual void AuthenticateAsClient(NetworkCredential credential, string targetName, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel allowedImpersonationLevel)
		{
			this.AuthenticateAsClient(credential, null, targetName, requiredProtectionLevel, allowedImpersonationLevel);
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x000BCD2E File Offset: 0x000BBD2E
		public virtual void AuthenticateAsClient(NetworkCredential credential, ChannelBinding binding, string targetName, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel allowedImpersonationLevel)
		{
			this._NegoState.ValidateCreateContext(this._Package, false, credential, targetName, binding, requiredProtectionLevel, allowedImpersonationLevel);
			this._NegoState.ProcessAuthentication(null);
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x000BCD55 File Offset: 0x000BBD55
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsClient(AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsClient((NetworkCredential)CredentialCache.DefaultCredentials, null, string.Empty, ProtectionLevel.EncryptAndSign, TokenImpersonationLevel.Identification, asyncCallback, asyncState);
		}

		// Token: 0x06002BA9 RID: 11177 RVA: 0x000BCD71 File Offset: 0x000BBD71
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsClient(NetworkCredential credential, string targetName, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsClient(credential, null, targetName, ProtectionLevel.EncryptAndSign, TokenImpersonationLevel.Identification, asyncCallback, asyncState);
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x000BCD81 File Offset: 0x000BBD81
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsClient(NetworkCredential credential, ChannelBinding binding, string targetName, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsClient(credential, binding, targetName, ProtectionLevel.EncryptAndSign, TokenImpersonationLevel.Identification, asyncCallback, asyncState);
		}

		// Token: 0x06002BAB RID: 11179 RVA: 0x000BCD92 File Offset: 0x000BBD92
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsClient(NetworkCredential credential, string targetName, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel allowedImpersonationLevel, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsClient(credential, null, targetName, requiredProtectionLevel, allowedImpersonationLevel, asyncCallback, asyncState);
		}

		// Token: 0x06002BAC RID: 11180 RVA: 0x000BCDA4 File Offset: 0x000BBDA4
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsClient(NetworkCredential credential, ChannelBinding binding, string targetName, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel allowedImpersonationLevel, AsyncCallback asyncCallback, object asyncState)
		{
			this._NegoState.ValidateCreateContext(this._Package, false, credential, targetName, binding, requiredProtectionLevel, allowedImpersonationLevel);
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(this._NegoState, asyncState, asyncCallback);
			this._NegoState.ProcessAuthentication(lazyAsyncResult);
			return lazyAsyncResult;
		}

		// Token: 0x06002BAD RID: 11181 RVA: 0x000BCDE7 File Offset: 0x000BBDE7
		public virtual void EndAuthenticateAsClient(IAsyncResult asyncResult)
		{
			this._NegoState.EndProcessAuthentication(asyncResult);
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x000BCDF5 File Offset: 0x000BBDF5
		public virtual void AuthenticateAsServer()
		{
			this.AuthenticateAsServer((NetworkCredential)CredentialCache.DefaultCredentials, null, ProtectionLevel.EncryptAndSign, TokenImpersonationLevel.Identification);
		}

		// Token: 0x06002BAF RID: 11183 RVA: 0x000BCE0A File Offset: 0x000BBE0A
		public virtual void AuthenticateAsServer(ExtendedProtectionPolicy policy)
		{
			this.AuthenticateAsServer((NetworkCredential)CredentialCache.DefaultCredentials, policy, ProtectionLevel.EncryptAndSign, TokenImpersonationLevel.Identification);
		}

		// Token: 0x06002BB0 RID: 11184 RVA: 0x000BCE1F File Offset: 0x000BBE1F
		public virtual void AuthenticateAsServer(NetworkCredential credential, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel requiredImpersonationLevel)
		{
			this.AuthenticateAsServer(credential, null, requiredProtectionLevel, requiredImpersonationLevel);
		}

		// Token: 0x06002BB1 RID: 11185 RVA: 0x000BCE2B File Offset: 0x000BBE2B
		public virtual void AuthenticateAsServer(NetworkCredential credential, ExtendedProtectionPolicy policy, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel requiredImpersonationLevel)
		{
			if (!ComNetOS.IsWin2K)
			{
				throw new PlatformNotSupportedException(SR.GetString("Win2000Required"));
			}
			this._NegoState.ValidateCreateContext(this._Package, credential, string.Empty, policy, requiredProtectionLevel, requiredImpersonationLevel);
			this._NegoState.ProcessAuthentication(null);
		}

		// Token: 0x06002BB2 RID: 11186 RVA: 0x000BCE6B File Offset: 0x000BBE6B
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsServer(AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsServer((NetworkCredential)CredentialCache.DefaultCredentials, null, ProtectionLevel.EncryptAndSign, TokenImpersonationLevel.Identification, asyncCallback, asyncState);
		}

		// Token: 0x06002BB3 RID: 11187 RVA: 0x000BCE82 File Offset: 0x000BBE82
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsServer(ExtendedProtectionPolicy policy, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsServer((NetworkCredential)CredentialCache.DefaultCredentials, policy, ProtectionLevel.EncryptAndSign, TokenImpersonationLevel.Identification, asyncCallback, asyncState);
		}

		// Token: 0x06002BB4 RID: 11188 RVA: 0x000BCE99 File Offset: 0x000BBE99
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsServer(NetworkCredential credential, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel requiredImpersonationLevel, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsServer(credential, null, requiredProtectionLevel, requiredImpersonationLevel, asyncCallback, asyncState);
		}

		// Token: 0x06002BB5 RID: 11189 RVA: 0x000BCEAC File Offset: 0x000BBEAC
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsServer(NetworkCredential credential, ExtendedProtectionPolicy policy, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel requiredImpersonationLevel, AsyncCallback asyncCallback, object asyncState)
		{
			if (!ComNetOS.IsWin2K)
			{
				throw new PlatformNotSupportedException(SR.GetString("Win2000Required"));
			}
			this._NegoState.ValidateCreateContext(this._Package, credential, string.Empty, policy, requiredProtectionLevel, requiredImpersonationLevel);
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(this._NegoState, asyncState, asyncCallback);
			this._NegoState.ProcessAuthentication(lazyAsyncResult);
			return lazyAsyncResult;
		}

		// Token: 0x06002BB6 RID: 11190 RVA: 0x000BCF08 File Offset: 0x000BBF08
		public virtual void EndAuthenticateAsServer(IAsyncResult asyncResult)
		{
			this._NegoState.EndProcessAuthentication(asyncResult);
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06002BB7 RID: 11191 RVA: 0x000BCF16 File Offset: 0x000BBF16
		public override bool IsAuthenticated
		{
			get
			{
				return this._NegoState.IsAuthenticated;
			}
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06002BB8 RID: 11192 RVA: 0x000BCF23 File Offset: 0x000BBF23
		public override bool IsMutuallyAuthenticated
		{
			get
			{
				return this._NegoState.IsMutuallyAuthenticated;
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06002BB9 RID: 11193 RVA: 0x000BCF30 File Offset: 0x000BBF30
		public override bool IsEncrypted
		{
			get
			{
				return this._NegoState.IsEncrypted;
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06002BBA RID: 11194 RVA: 0x000BCF3D File Offset: 0x000BBF3D
		public override bool IsSigned
		{
			get
			{
				return this._NegoState.IsSigned;
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06002BBB RID: 11195 RVA: 0x000BCF4A File Offset: 0x000BBF4A
		public override bool IsServer
		{
			get
			{
				return this._NegoState.IsServer;
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06002BBC RID: 11196 RVA: 0x000BCF57 File Offset: 0x000BBF57
		public virtual TokenImpersonationLevel ImpersonationLevel
		{
			get
			{
				return this._NegoState.AllowedImpersonation;
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06002BBD RID: 11197 RVA: 0x000BCF64 File Offset: 0x000BBF64
		public virtual IIdentity RemoteIdentity
		{
			get
			{
				new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
				if (this._RemoteIdentity == null)
				{
					this._RemoteIdentity = this._NegoState.GetIdentity();
				}
				return this._RemoteIdentity;
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06002BBE RID: 11198 RVA: 0x000BCF90 File Offset: 0x000BBF90
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06002BBF RID: 11199 RVA: 0x000BCF93 File Offset: 0x000BBF93
		public override bool CanRead
		{
			get
			{
				return this.IsAuthenticated && base.InnerStream.CanRead;
			}
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06002BC0 RID: 11200 RVA: 0x000BCFAA File Offset: 0x000BBFAA
		public override bool CanTimeout
		{
			get
			{
				return base.InnerStream.CanTimeout;
			}
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06002BC1 RID: 11201 RVA: 0x000BCFB7 File Offset: 0x000BBFB7
		public override bool CanWrite
		{
			get
			{
				return this.IsAuthenticated && base.InnerStream.CanWrite;
			}
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06002BC2 RID: 11202 RVA: 0x000BCFCE File Offset: 0x000BBFCE
		// (set) Token: 0x06002BC3 RID: 11203 RVA: 0x000BCFDB File Offset: 0x000BBFDB
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

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06002BC4 RID: 11204 RVA: 0x000BCFE9 File Offset: 0x000BBFE9
		// (set) Token: 0x06002BC5 RID: 11205 RVA: 0x000BCFF6 File Offset: 0x000BBFF6
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

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06002BC6 RID: 11206 RVA: 0x000BD004 File Offset: 0x000BC004
		public override long Length
		{
			get
			{
				return base.InnerStream.Length;
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06002BC7 RID: 11207 RVA: 0x000BD011 File Offset: 0x000BC011
		// (set) Token: 0x06002BC8 RID: 11208 RVA: 0x000BD01E File Offset: 0x000BC01E
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

		// Token: 0x06002BC9 RID: 11209 RVA: 0x000BD02F File Offset: 0x000BC02F
		public override void SetLength(long value)
		{
			base.InnerStream.SetLength(value);
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x000BD03D File Offset: 0x000BC03D
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06002BCB RID: 11211 RVA: 0x000BD04E File Offset: 0x000BC04E
		public override void Flush()
		{
			base.InnerStream.Flush();
		}

		// Token: 0x06002BCC RID: 11212 RVA: 0x000BD05C File Offset: 0x000BC05C
		protected override void Dispose(bool disposing)
		{
			try
			{
				this._NegoState.Close();
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x000BD090 File Offset: 0x000BC090
		public override int Read(byte[] buffer, int offset, int count)
		{
			this._NegoState.CheckThrow(true);
			if (!this._NegoState.CanGetSecureStream)
			{
				return base.InnerStream.Read(buffer, offset, count);
			}
			return this.ProcessRead(buffer, offset, count, null);
		}

		// Token: 0x06002BCE RID: 11214 RVA: 0x000BD0C4 File Offset: 0x000BC0C4
		public override void Write(byte[] buffer, int offset, int count)
		{
			this._NegoState.CheckThrow(true);
			if (!this._NegoState.CanGetSecureStream)
			{
				base.InnerStream.Write(buffer, offset, count);
				return;
			}
			this.ProcessWrite(buffer, offset, count, null);
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x000BD0F8 File Offset: 0x000BC0F8
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			this._NegoState.CheckThrow(true);
			if (!this._NegoState.CanGetSecureStream)
			{
				return base.InnerStream.BeginRead(buffer, offset, count, asyncCallback, asyncState);
			}
			BufferAsyncResult bufferAsyncResult = new BufferAsyncResult(this, buffer, offset, count, asyncState, asyncCallback);
			AsyncProtocolRequest asyncRequest = new AsyncProtocolRequest(bufferAsyncResult);
			this.ProcessRead(buffer, offset, count, asyncRequest);
			return bufferAsyncResult;
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x000BD154 File Offset: 0x000BC154
		public override int EndRead(IAsyncResult asyncResult)
		{
			this._NegoState.CheckThrow(true);
			if (!this._NegoState.CanGetSecureStream)
			{
				return base.InnerStream.EndRead(asyncResult);
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			BufferAsyncResult bufferAsyncResult = asyncResult as BufferAsyncResult;
			if (bufferAsyncResult == null)
			{
				throw new ArgumentException(SR.GetString("net_io_async_result", new object[]
				{
					asyncResult.GetType().FullName
				}), "asyncResult");
			}
			if (Interlocked.Exchange(ref this._NestedRead, 0) == 0)
			{
				throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[]
				{
					"EndRead"
				}));
			}
			bufferAsyncResult.InternalWaitForCompletion();
			if (!(bufferAsyncResult.Result is Exception))
			{
				return (int)bufferAsyncResult.Result;
			}
			if (bufferAsyncResult.Result is IOException)
			{
				throw (Exception)bufferAsyncResult.Result;
			}
			throw new IOException(SR.GetString("net_io_write"), (Exception)bufferAsyncResult.Result);
		}

		// Token: 0x06002BD1 RID: 11217 RVA: 0x000BD24C File Offset: 0x000BC24C
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			this._NegoState.CheckThrow(true);
			if (!this._NegoState.CanGetSecureStream)
			{
				return base.InnerStream.BeginWrite(buffer, offset, count, asyncCallback, asyncState);
			}
			BufferAsyncResult bufferAsyncResult = new BufferAsyncResult(this, buffer, offset, count, true, asyncState, asyncCallback);
			AsyncProtocolRequest asyncRequest = new AsyncProtocolRequest(bufferAsyncResult);
			this.ProcessWrite(buffer, offset, count, asyncRequest);
			return bufferAsyncResult;
		}

		// Token: 0x06002BD2 RID: 11218 RVA: 0x000BD2A8 File Offset: 0x000BC2A8
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this._NegoState.CheckThrow(true);
			if (!this._NegoState.CanGetSecureStream)
			{
				base.InnerStream.EndWrite(asyncResult);
				return;
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			BufferAsyncResult bufferAsyncResult = asyncResult as BufferAsyncResult;
			if (bufferAsyncResult == null)
			{
				throw new ArgumentException(SR.GetString("net_io_async_result", new object[]
				{
					asyncResult.GetType().FullName
				}), "asyncResult");
			}
			if (Interlocked.Exchange(ref this._NestedWrite, 0) == 0)
			{
				throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[]
				{
					"EndWrite"
				}));
			}
			bufferAsyncResult.InternalWaitForCompletion();
			if (!(bufferAsyncResult.Result is Exception))
			{
				return;
			}
			if (bufferAsyncResult.Result is IOException)
			{
				throw (Exception)bufferAsyncResult.Result;
			}
			throw new IOException(SR.GetString("net_io_write"), (Exception)bufferAsyncResult.Result);
		}

		// Token: 0x06002BD3 RID: 11219 RVA: 0x000BD394 File Offset: 0x000BC394
		private void InitializeStreamPart()
		{
			this._ReadHeader = new byte[4];
			this._FrameReader = new FixedSizeReader(base.InnerStream);
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06002BD4 RID: 11220 RVA: 0x000BD3B3 File Offset: 0x000BC3B3
		private byte[] InternalBuffer
		{
			get
			{
				return this._InternalBuffer;
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06002BD5 RID: 11221 RVA: 0x000BD3BB File Offset: 0x000BC3BB
		private int InternalOffset
		{
			get
			{
				return this._InternalOffset;
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06002BD6 RID: 11222 RVA: 0x000BD3C3 File Offset: 0x000BC3C3
		private int InternalBufferCount
		{
			get
			{
				return this._InternalBufferCount;
			}
		}

		// Token: 0x06002BD7 RID: 11223 RVA: 0x000BD3CB File Offset: 0x000BC3CB
		private void DecrementInternalBufferCount(int decrCount)
		{
			this._InternalOffset += decrCount;
			this._InternalBufferCount -= decrCount;
		}

		// Token: 0x06002BD8 RID: 11224 RVA: 0x000BD3E9 File Offset: 0x000BC3E9
		private void EnsureInternalBufferSize(int bytes)
		{
			this._InternalBufferCount = bytes;
			this._InternalOffset = 0;
			if (this.InternalBuffer == null || this.InternalBuffer.Length < bytes)
			{
				this._InternalBuffer = new byte[bytes];
			}
		}

		// Token: 0x06002BD9 RID: 11225 RVA: 0x000BD418 File Offset: 0x000BC418
		private void AdjustInternalBufferOffsetSize(int bytes, int offset)
		{
			this._InternalBufferCount = bytes;
			this._InternalOffset = offset;
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x000BD428 File Offset: 0x000BC428
		private void ValidateParameters(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException(SR.GetString("net_offset_plus_count"));
			}
		}

		// Token: 0x06002BDB RID: 11227 RVA: 0x000BD47C File Offset: 0x000BC47C
		private void ProcessWrite(byte[] buffer, int offset, int count, AsyncProtocolRequest asyncRequest)
		{
			this.ValidateParameters(buffer, offset, count);
			if (Interlocked.Exchange(ref this._NestedWrite, 1) == 1)
			{
				throw new NotSupportedException(SR.GetString("net_io_invalidnestedcall", new object[]
				{
					(asyncRequest != null) ? "BeginWrite" : "Write",
					"write"
				}));
			}
			bool flag = false;
			try
			{
				this.StartWriting(buffer, offset, count, asyncRequest);
			}
			catch (Exception ex)
			{
				flag = true;
				if (ex is IOException)
				{
					throw;
				}
				throw new IOException(SR.GetString("net_io_write"), ex);
			}
			catch
			{
				flag = true;
				throw new IOException(SR.GetString("net_io_write"), new Exception(SR.GetString("net_nonClsCompliantException")));
			}
			finally
			{
				if (asyncRequest == null || flag)
				{
					this._NestedWrite = 0;
				}
			}
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x000BD55C File Offset: 0x000BC55C
		private void StartWriting(byte[] buffer, int offset, int count, AsyncProtocolRequest asyncRequest)
		{
			if (count >= 0)
			{
				byte[] buffer2 = null;
				for (;;)
				{
					int num = Math.Min(count, 64512);
					int count2;
					try
					{
						count2 = this._NegoState.EncryptData(buffer, offset, num, ref buffer2);
					}
					catch (Exception innerException)
					{
						throw new IOException(SR.GetString("net_io_encrypt"), innerException);
					}
					catch
					{
						throw new IOException(SR.GetString("net_io_encrypt"), new Exception(SR.GetString("net_nonClsCompliantException")));
					}
					if (asyncRequest != null)
					{
						asyncRequest.SetNextRequest(buffer, offset + num, count - num, null);
						IAsyncResult asyncResult = base.InnerStream.BeginWrite(buffer2, 0, count2, NegotiateStream._WriteCallback, asyncRequest);
						if (!asyncResult.CompletedSynchronously)
						{
							break;
						}
						base.InnerStream.EndWrite(asyncResult);
					}
					else
					{
						base.InnerStream.Write(buffer2, 0, count2);
					}
					offset += num;
					count -= num;
					if (count == 0)
					{
						goto IL_BB;
					}
				}
				return;
			}
			IL_BB:
			if (asyncRequest != null)
			{
				asyncRequest.CompleteUser();
			}
		}

		// Token: 0x06002BDD RID: 11229 RVA: 0x000BD64C File Offset: 0x000BC64C
		private int ProcessRead(byte[] buffer, int offset, int count, AsyncProtocolRequest asyncRequest)
		{
			this.ValidateParameters(buffer, offset, count);
			if (Interlocked.Exchange(ref this._NestedRead, 1) == 1)
			{
				throw new NotSupportedException(SR.GetString("net_io_invalidnestedcall", new object[]
				{
					(asyncRequest != null) ? "BeginRead" : "Read",
					"read"
				}));
			}
			bool flag = false;
			int result;
			try
			{
				if (this.InternalBufferCount != 0)
				{
					int num = (this.InternalBufferCount > count) ? count : this.InternalBufferCount;
					if (num != 0)
					{
						Buffer.BlockCopy(this.InternalBuffer, this.InternalOffset, buffer, offset, num);
						this.DecrementInternalBufferCount(num);
					}
					if (asyncRequest != null)
					{
						asyncRequest.CompleteUser(num);
					}
					result = num;
				}
				else
				{
					result = this.StartReading(buffer, offset, count, asyncRequest);
				}
			}
			catch (Exception ex)
			{
				flag = true;
				if (ex is IOException)
				{
					throw;
				}
				throw new IOException(SR.GetString("net_io_read"), ex);
			}
			catch
			{
				flag = true;
				throw new IOException(SR.GetString("net_io_read"), new Exception(SR.GetString("net_nonClsCompliantException")));
			}
			finally
			{
				if (asyncRequest == null || flag)
				{
					this._NestedRead = 0;
				}
			}
			return result;
		}

		// Token: 0x06002BDE RID: 11230 RVA: 0x000BD780 File Offset: 0x000BC780
		private int StartReading(byte[] buffer, int offset, int count, AsyncProtocolRequest asyncRequest)
		{
			int result;
			while ((result = this.StartFrameHeader(buffer, offset, count, asyncRequest)) == -1)
			{
			}
			return result;
		}

		// Token: 0x06002BDF RID: 11231 RVA: 0x000BD7A0 File Offset: 0x000BC7A0
		private int StartFrameHeader(byte[] buffer, int offset, int count, AsyncProtocolRequest asyncRequest)
		{
			int readBytes;
			if (asyncRequest != null)
			{
				asyncRequest.SetNextRequest(this._ReadHeader, 0, this._ReadHeader.Length, NegotiateStream._ReadCallback);
				this._FrameReader.AsyncReadPacket(asyncRequest);
				if (!asyncRequest.MustCompleteSynchronously)
				{
					return 0;
				}
				readBytes = asyncRequest.Result;
			}
			else
			{
				readBytes = this._FrameReader.ReadPacket(this._ReadHeader, 0, this._ReadHeader.Length);
			}
			return this.StartFrameBody(readBytes, buffer, offset, count, asyncRequest);
		}

		// Token: 0x06002BE0 RID: 11232 RVA: 0x000BD818 File Offset: 0x000BC818
		private int StartFrameBody(int readBytes, byte[] buffer, int offset, int count, AsyncProtocolRequest asyncRequest)
		{
			if (readBytes == 0)
			{
				if (asyncRequest != null)
				{
					asyncRequest.CompleteUser(0);
				}
				return 0;
			}
			readBytes = (int)this._ReadHeader[3];
			readBytes = (readBytes << 8 | (int)this._ReadHeader[2]);
			readBytes = (readBytes << 8 | (int)this._ReadHeader[1]);
			readBytes = (readBytes << 8 | (int)this._ReadHeader[0]);
			if (readBytes <= 4 || readBytes > 65536)
			{
				throw new IOException(SR.GetString("net_frame_read_size"));
			}
			this.EnsureInternalBufferSize(readBytes);
			if (asyncRequest != null)
			{
				asyncRequest.SetNextRequest(this.InternalBuffer, 0, readBytes, NegotiateStream._ReadCallback);
				this._FrameReader.AsyncReadPacket(asyncRequest);
				if (!asyncRequest.MustCompleteSynchronously)
				{
					return 0;
				}
				readBytes = asyncRequest.Result;
			}
			else
			{
				readBytes = this._FrameReader.ReadPacket(this.InternalBuffer, 0, readBytes);
			}
			return this.ProcessFrameBody(readBytes, buffer, offset, count, asyncRequest);
		}

		// Token: 0x06002BE1 RID: 11233 RVA: 0x000BD8F0 File Offset: 0x000BC8F0
		private int ProcessFrameBody(int readBytes, byte[] buffer, int offset, int count, AsyncProtocolRequest asyncRequest)
		{
			if (readBytes == 0)
			{
				throw new IOException(SR.GetString("net_io_eof"));
			}
			int offset2;
			readBytes = this._NegoState.DecryptData(this.InternalBuffer, 0, readBytes, out offset2);
			this.AdjustInternalBufferOffsetSize(readBytes, offset2);
			if (readBytes == 0 && count != 0)
			{
				return -1;
			}
			if (readBytes > count)
			{
				readBytes = count;
			}
			Buffer.BlockCopy(this.InternalBuffer, this.InternalOffset, buffer, offset, readBytes);
			this.DecrementInternalBufferCount(readBytes);
			if (asyncRequest != null)
			{
				asyncRequest.CompleteUser(readBytes);
			}
			return readBytes;
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x000BD970 File Offset: 0x000BC970
		private static void WriteCallback(IAsyncResult transportResult)
		{
			if (transportResult.CompletedSynchronously)
			{
				return;
			}
			AsyncProtocolRequest asyncProtocolRequest = (AsyncProtocolRequest)transportResult.AsyncState;
			try
			{
				NegotiateStream negotiateStream = (NegotiateStream)asyncProtocolRequest.AsyncObject;
				negotiateStream.InnerStream.EndWrite(transportResult);
				if (asyncProtocolRequest.Count == 0)
				{
					asyncProtocolRequest.Count = -1;
				}
				negotiateStream.StartWriting(asyncProtocolRequest.Buffer, asyncProtocolRequest.Offset, asyncProtocolRequest.Count, asyncProtocolRequest);
			}
			catch (Exception e)
			{
				if (asyncProtocolRequest.IsUserCompleted)
				{
					throw;
				}
				asyncProtocolRequest.CompleteWithError(e);
			}
			catch
			{
				if (asyncProtocolRequest.IsUserCompleted)
				{
					throw;
				}
				asyncProtocolRequest.CompleteWithError(new Exception(SR.GetString("net_nonClsCompliantException")));
			}
		}

		// Token: 0x06002BE3 RID: 11235 RVA: 0x000BDA28 File Offset: 0x000BCA28
		private static void ReadCallback(AsyncProtocolRequest asyncRequest)
		{
			try
			{
				NegotiateStream negotiateStream = (NegotiateStream)asyncRequest.AsyncObject;
				BufferAsyncResult bufferAsyncResult = (BufferAsyncResult)asyncRequest.UserAsyncResult;
				if (asyncRequest.Buffer == negotiateStream._ReadHeader)
				{
					negotiateStream.StartFrameBody(asyncRequest.Result, bufferAsyncResult.Buffer, bufferAsyncResult.Offset, bufferAsyncResult.Count, asyncRequest);
				}
				else if (-1 == negotiateStream.ProcessFrameBody(asyncRequest.Result, bufferAsyncResult.Buffer, bufferAsyncResult.Offset, bufferAsyncResult.Count, asyncRequest))
				{
					negotiateStream.StartReading(bufferAsyncResult.Buffer, bufferAsyncResult.Offset, bufferAsyncResult.Count, asyncRequest);
				}
			}
			catch (Exception e)
			{
				if (asyncRequest.IsUserCompleted)
				{
					throw;
				}
				asyncRequest.CompleteWithError(e);
			}
			catch
			{
				if (asyncRequest.IsUserCompleted)
				{
					throw;
				}
				asyncRequest.CompleteWithError(new Exception(SR.GetString("net_nonClsCompliantException")));
			}
		}

		// Token: 0x040029D9 RID: 10713
		private NegoState _NegoState;

		// Token: 0x040029DA RID: 10714
		private string _Package;

		// Token: 0x040029DB RID: 10715
		private IIdentity _RemoteIdentity;

		// Token: 0x040029DC RID: 10716
		private static AsyncCallback _WriteCallback = new AsyncCallback(NegotiateStream.WriteCallback);

		// Token: 0x040029DD RID: 10717
		private static AsyncProtocolCallback _ReadCallback = new AsyncProtocolCallback(NegotiateStream.ReadCallback);

		// Token: 0x040029DE RID: 10718
		private int _NestedWrite;

		// Token: 0x040029DF RID: 10719
		private int _NestedRead;

		// Token: 0x040029E0 RID: 10720
		private byte[] _ReadHeader;

		// Token: 0x040029E1 RID: 10721
		private byte[] _InternalBuffer;

		// Token: 0x040029E2 RID: 10722
		private int _InternalOffset;

		// Token: 0x040029E3 RID: 10723
		private int _InternalBufferCount;

		// Token: 0x040029E4 RID: 10724
		private FixedSizeReader _FrameReader;
	}
}
