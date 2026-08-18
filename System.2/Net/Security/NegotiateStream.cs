using System;
using System.IO;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Security
{
	// Token: 0x02000357 RID: 855
	public class NegotiateStream : AuthenticatedStream
	{
		// Token: 0x06001E9D RID: 7837 RVA: 0x0008FFB0 File Offset: 0x0008E1B0
		public NegotiateStream(Stream innerStream) : this(innerStream, false)
		{
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x0008FFBA File Offset: 0x0008E1BA
		public NegotiateStream(Stream innerStream, bool leaveInnerStreamOpen) : base(innerStream, leaveInnerStreamOpen)
		{
			this._NegoState = new NegoState(innerStream, leaveInnerStreamOpen);
			this._Package = NegoState.DefaultPackage;
			this.InitializeStreamPart();
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x0008FFE2 File Offset: 0x0008E1E2
		public virtual void AuthenticateAsClient()
		{
			this.AuthenticateAsClient((NetworkCredential)CredentialCache.DefaultCredentials, null, string.Empty, ProtectionLevel.EncryptAndSign, TokenImpersonationLevel.Identification);
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x0008FFFC File Offset: 0x0008E1FC
		public virtual void AuthenticateAsClient(NetworkCredential credential, string targetName)
		{
			this.AuthenticateAsClient(credential, null, targetName, ProtectionLevel.EncryptAndSign, TokenImpersonationLevel.Identification);
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x00090009 File Offset: 0x0008E209
		public virtual void AuthenticateAsClient(NetworkCredential credential, ChannelBinding binding, string targetName)
		{
			this.AuthenticateAsClient(credential, binding, targetName, ProtectionLevel.EncryptAndSign, TokenImpersonationLevel.Identification);
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x00090016 File Offset: 0x0008E216
		public virtual void AuthenticateAsClient(NetworkCredential credential, string targetName, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel allowedImpersonationLevel)
		{
			this.AuthenticateAsClient(credential, null, targetName, requiredProtectionLevel, allowedImpersonationLevel);
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x00090024 File Offset: 0x0008E224
		public virtual void AuthenticateAsClient(NetworkCredential credential, ChannelBinding binding, string targetName, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel allowedImpersonationLevel)
		{
			this._NegoState.ValidateCreateContext(this._Package, false, credential, targetName, binding, requiredProtectionLevel, allowedImpersonationLevel);
			this._NegoState.ProcessAuthentication(null);
		}

		// Token: 0x06001EA4 RID: 7844 RVA: 0x0009004B File Offset: 0x0008E24B
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsClient(AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsClient((NetworkCredential)CredentialCache.DefaultCredentials, null, string.Empty, ProtectionLevel.EncryptAndSign, TokenImpersonationLevel.Identification, asyncCallback, asyncState);
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x00090067 File Offset: 0x0008E267
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsClient(NetworkCredential credential, string targetName, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsClient(credential, null, targetName, ProtectionLevel.EncryptAndSign, TokenImpersonationLevel.Identification, asyncCallback, asyncState);
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x00090077 File Offset: 0x0008E277
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsClient(NetworkCredential credential, ChannelBinding binding, string targetName, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsClient(credential, binding, targetName, ProtectionLevel.EncryptAndSign, TokenImpersonationLevel.Identification, asyncCallback, asyncState);
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x00090088 File Offset: 0x0008E288
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsClient(NetworkCredential credential, string targetName, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel allowedImpersonationLevel, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsClient(credential, null, targetName, requiredProtectionLevel, allowedImpersonationLevel, asyncCallback, asyncState);
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x0009009C File Offset: 0x0008E29C
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsClient(NetworkCredential credential, ChannelBinding binding, string targetName, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel allowedImpersonationLevel, AsyncCallback asyncCallback, object asyncState)
		{
			this._NegoState.ValidateCreateContext(this._Package, false, credential, targetName, binding, requiredProtectionLevel, allowedImpersonationLevel);
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(this._NegoState, asyncState, asyncCallback);
			this._NegoState.ProcessAuthentication(lazyAsyncResult);
			return lazyAsyncResult;
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x000900DF File Offset: 0x0008E2DF
		public virtual void EndAuthenticateAsClient(IAsyncResult asyncResult)
		{
			this._NegoState.EndProcessAuthentication(asyncResult);
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x000900ED File Offset: 0x0008E2ED
		public virtual void AuthenticateAsServer()
		{
			this.AuthenticateAsServer((NetworkCredential)CredentialCache.DefaultCredentials, null, ProtectionLevel.EncryptAndSign, TokenImpersonationLevel.Identification);
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x00090102 File Offset: 0x0008E302
		public virtual void AuthenticateAsServer(ExtendedProtectionPolicy policy)
		{
			this.AuthenticateAsServer((NetworkCredential)CredentialCache.DefaultCredentials, policy, ProtectionLevel.EncryptAndSign, TokenImpersonationLevel.Identification);
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x00090117 File Offset: 0x0008E317
		public virtual void AuthenticateAsServer(NetworkCredential credential, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel requiredImpersonationLevel)
		{
			this.AuthenticateAsServer(credential, null, requiredProtectionLevel, requiredImpersonationLevel);
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x00090123 File Offset: 0x0008E323
		public virtual void AuthenticateAsServer(NetworkCredential credential, ExtendedProtectionPolicy policy, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel requiredImpersonationLevel)
		{
			this._NegoState.ValidateCreateContext(this._Package, credential, string.Empty, policy, requiredProtectionLevel, requiredImpersonationLevel);
			this._NegoState.ProcessAuthentication(null);
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x0009014C File Offset: 0x0008E34C
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsServer(AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsServer((NetworkCredential)CredentialCache.DefaultCredentials, null, ProtectionLevel.EncryptAndSign, TokenImpersonationLevel.Identification, asyncCallback, asyncState);
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x00090163 File Offset: 0x0008E363
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsServer(ExtendedProtectionPolicy policy, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsServer((NetworkCredential)CredentialCache.DefaultCredentials, policy, ProtectionLevel.EncryptAndSign, TokenImpersonationLevel.Identification, asyncCallback, asyncState);
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x0009017A File Offset: 0x0008E37A
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsServer(NetworkCredential credential, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel requiredImpersonationLevel, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsServer(credential, null, requiredProtectionLevel, requiredImpersonationLevel, asyncCallback, asyncState);
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x0009018C File Offset: 0x0008E38C
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginAuthenticateAsServer(NetworkCredential credential, ExtendedProtectionPolicy policy, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel requiredImpersonationLevel, AsyncCallback asyncCallback, object asyncState)
		{
			this._NegoState.ValidateCreateContext(this._Package, credential, string.Empty, policy, requiredProtectionLevel, requiredImpersonationLevel);
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(this._NegoState, asyncState, asyncCallback);
			this._NegoState.ProcessAuthentication(lazyAsyncResult);
			return lazyAsyncResult;
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x000901D1 File Offset: 0x0008E3D1
		public virtual void EndAuthenticateAsServer(IAsyncResult asyncResult)
		{
			this._NegoState.EndProcessAuthentication(asyncResult);
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x000901DF File Offset: 0x0008E3DF
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task AuthenticateAsClientAsync()
		{
			return Task.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginAuthenticateAsClient), new Action<IAsyncResult>(this.EndAuthenticateAsClient), null);
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x00090206 File Offset: 0x0008E406
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task AuthenticateAsClientAsync(NetworkCredential credential, string targetName)
		{
			return Task.Factory.FromAsync<NetworkCredential, string>(new Func<NetworkCredential, string, AsyncCallback, object, IAsyncResult>(this.BeginAuthenticateAsClient), new Action<IAsyncResult>(this.EndAuthenticateAsClient), credential, targetName, null);
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x00090230 File Offset: 0x0008E430
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task AuthenticateAsClientAsync(NetworkCredential credential, string targetName, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel allowedImpersonationLevel)
		{
			return Task.Factory.FromAsync((AsyncCallback callback, object state) => this.BeginAuthenticateAsClient(credential, targetName, requiredProtectionLevel, allowedImpersonationLevel, callback, state), new Action<IAsyncResult>(this.EndAuthenticateAsClient), null);
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x0009028B File Offset: 0x0008E48B
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task AuthenticateAsClientAsync(NetworkCredential credential, ChannelBinding binding, string targetName)
		{
			return Task.Factory.FromAsync<NetworkCredential, ChannelBinding, string>(new Func<NetworkCredential, ChannelBinding, string, AsyncCallback, object, IAsyncResult>(this.BeginAuthenticateAsClient), new Action<IAsyncResult>(this.EndAuthenticateAsClient), credential, binding, targetName, null);
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x000902B8 File Offset: 0x0008E4B8
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task AuthenticateAsClientAsync(NetworkCredential credential, ChannelBinding binding, string targetName, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel allowedImpersonationLevel)
		{
			return Task.Factory.FromAsync((AsyncCallback callback, object state) => this.BeginAuthenticateAsClient(credential, binding, targetName, requiredProtectionLevel, allowedImpersonationLevel, callback, state), new Action<IAsyncResult>(this.EndAuthenticateAsClient), null);
		}

		// Token: 0x06001EB8 RID: 7864 RVA: 0x0009031B File Offset: 0x0008E51B
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task AuthenticateAsServerAsync()
		{
			return Task.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginAuthenticateAsServer), new Action<IAsyncResult>(this.EndAuthenticateAsServer), null);
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x00090342 File Offset: 0x0008E542
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task AuthenticateAsServerAsync(ExtendedProtectionPolicy policy)
		{
			return Task.Factory.FromAsync<ExtendedProtectionPolicy>(new Func<ExtendedProtectionPolicy, AsyncCallback, object, IAsyncResult>(this.BeginAuthenticateAsServer), new Action<IAsyncResult>(this.EndAuthenticateAsServer), policy, null);
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x0009036A File Offset: 0x0008E56A
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task AuthenticateAsServerAsync(NetworkCredential credential, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel requiredImpersonationLevel)
		{
			return Task.Factory.FromAsync<NetworkCredential, ProtectionLevel, TokenImpersonationLevel>(new Func<NetworkCredential, ProtectionLevel, TokenImpersonationLevel, AsyncCallback, object, IAsyncResult>(this.BeginAuthenticateAsServer), new Action<IAsyncResult>(this.EndAuthenticateAsServer), credential, requiredProtectionLevel, requiredImpersonationLevel, null);
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x00090394 File Offset: 0x0008E594
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task AuthenticateAsServerAsync(NetworkCredential credential, ExtendedProtectionPolicy policy, ProtectionLevel requiredProtectionLevel, TokenImpersonationLevel requiredImpersonationLevel)
		{
			return Task.Factory.FromAsync((AsyncCallback callback, object state) => this.BeginAuthenticateAsServer(credential, policy, requiredProtectionLevel, requiredImpersonationLevel, callback, state), new Action<IAsyncResult>(this.EndAuthenticateAsClient), null);
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06001EBC RID: 7868 RVA: 0x000903EF File Offset: 0x0008E5EF
		public override bool IsAuthenticated
		{
			get
			{
				return this._NegoState.IsAuthenticated;
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06001EBD RID: 7869 RVA: 0x000903FC File Offset: 0x0008E5FC
		public override bool IsMutuallyAuthenticated
		{
			get
			{
				return this._NegoState.IsMutuallyAuthenticated;
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06001EBE RID: 7870 RVA: 0x00090409 File Offset: 0x0008E609
		public override bool IsEncrypted
		{
			get
			{
				return this._NegoState.IsEncrypted;
			}
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06001EBF RID: 7871 RVA: 0x00090416 File Offset: 0x0008E616
		public override bool IsSigned
		{
			get
			{
				return this._NegoState.IsSigned;
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06001EC0 RID: 7872 RVA: 0x00090423 File Offset: 0x0008E623
		public override bool IsServer
		{
			get
			{
				return this._NegoState.IsServer;
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06001EC1 RID: 7873 RVA: 0x00090430 File Offset: 0x0008E630
		public virtual TokenImpersonationLevel ImpersonationLevel
		{
			get
			{
				return this._NegoState.AllowedImpersonation;
			}
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06001EC2 RID: 7874 RVA: 0x0009043D File Offset: 0x0008E63D
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

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06001EC3 RID: 7875 RVA: 0x00090469 File Offset: 0x0008E669
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06001EC4 RID: 7876 RVA: 0x0009046C File Offset: 0x0008E66C
		public override bool CanRead
		{
			get
			{
				return this.IsAuthenticated && base.InnerStream.CanRead;
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06001EC5 RID: 7877 RVA: 0x00090483 File Offset: 0x0008E683
		public override bool CanTimeout
		{
			get
			{
				return base.InnerStream.CanTimeout;
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06001EC6 RID: 7878 RVA: 0x00090490 File Offset: 0x0008E690
		public override bool CanWrite
		{
			get
			{
				return this.IsAuthenticated && base.InnerStream.CanWrite;
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06001EC7 RID: 7879 RVA: 0x000904A7 File Offset: 0x0008E6A7
		// (set) Token: 0x06001EC8 RID: 7880 RVA: 0x000904B4 File Offset: 0x0008E6B4
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

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06001EC9 RID: 7881 RVA: 0x000904C2 File Offset: 0x0008E6C2
		// (set) Token: 0x06001ECA RID: 7882 RVA: 0x000904CF File Offset: 0x0008E6CF
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

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06001ECB RID: 7883 RVA: 0x000904DD File Offset: 0x0008E6DD
		public override long Length
		{
			get
			{
				return base.InnerStream.Length;
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06001ECC RID: 7884 RVA: 0x000904EA File Offset: 0x0008E6EA
		// (set) Token: 0x06001ECD RID: 7885 RVA: 0x000904F7 File Offset: 0x0008E6F7
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

		// Token: 0x06001ECE RID: 7886 RVA: 0x00090508 File Offset: 0x0008E708
		public override void SetLength(long value)
		{
			base.InnerStream.SetLength(value);
		}

		// Token: 0x06001ECF RID: 7887 RVA: 0x00090516 File Offset: 0x0008E716
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06001ED0 RID: 7888 RVA: 0x00090527 File Offset: 0x0008E727
		public override void Flush()
		{
			base.InnerStream.Flush();
		}

		// Token: 0x06001ED1 RID: 7889 RVA: 0x00090534 File Offset: 0x0008E734
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

		// Token: 0x06001ED2 RID: 7890 RVA: 0x00090568 File Offset: 0x0008E768
		public override int Read(byte[] buffer, int offset, int count)
		{
			this._NegoState.CheckThrow(true);
			if (!this._NegoState.CanGetSecureStream)
			{
				return base.InnerStream.Read(buffer, offset, count);
			}
			return this.ProcessRead(buffer, offset, count, null);
		}

		// Token: 0x06001ED3 RID: 7891 RVA: 0x0009059C File Offset: 0x0008E79C
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

		// Token: 0x06001ED4 RID: 7892 RVA: 0x000905D0 File Offset: 0x0008E7D0
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

		// Token: 0x06001ED5 RID: 7893 RVA: 0x0009062C File Offset: 0x0008E82C
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
			throw new IOException(SR.GetString("net_io_read"), (Exception)bufferAsyncResult.Result);
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x00090720 File Offset: 0x0008E920
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

		// Token: 0x06001ED7 RID: 7895 RVA: 0x0009077C File Offset: 0x0008E97C
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

		// Token: 0x06001ED8 RID: 7896 RVA: 0x00090864 File Offset: 0x0008EA64
		private void InitializeStreamPart()
		{
			this._ReadHeader = new byte[4];
			this._FrameReader = new FixedSizeReader(base.InnerStream);
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06001ED9 RID: 7897 RVA: 0x00090883 File Offset: 0x0008EA83
		private byte[] InternalBuffer
		{
			get
			{
				return this._InternalBuffer;
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06001EDA RID: 7898 RVA: 0x0009088B File Offset: 0x0008EA8B
		private int InternalOffset
		{
			get
			{
				return this._InternalOffset;
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06001EDB RID: 7899 RVA: 0x00090893 File Offset: 0x0008EA93
		private int InternalBufferCount
		{
			get
			{
				return this._InternalBufferCount;
			}
		}

		// Token: 0x06001EDC RID: 7900 RVA: 0x0009089B File Offset: 0x0008EA9B
		private void DecrementInternalBufferCount(int decrCount)
		{
			this._InternalOffset += decrCount;
			this._InternalBufferCount -= decrCount;
		}

		// Token: 0x06001EDD RID: 7901 RVA: 0x000908B9 File Offset: 0x0008EAB9
		private void EnsureInternalBufferSize(int bytes)
		{
			this._InternalBufferCount = bytes;
			this._InternalOffset = 0;
			if (this.InternalBuffer == null || this.InternalBuffer.Length < bytes)
			{
				this._InternalBuffer = new byte[bytes];
			}
		}

		// Token: 0x06001EDE RID: 7902 RVA: 0x000908E8 File Offset: 0x0008EAE8
		private void AdjustInternalBufferOffsetSize(int bytes, int offset)
		{
			this._InternalBufferCount = bytes;
			this._InternalOffset = offset;
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x000908F8 File Offset: 0x0008EAF8
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
				throw new ArgumentOutOfRangeException("count", SR.GetString("net_offset_plus_count"));
			}
		}

		// Token: 0x06001EE0 RID: 7904 RVA: 0x00090950 File Offset: 0x0008EB50
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
			finally
			{
				if (asyncRequest == null || flag)
				{
					this._NestedWrite = 0;
				}
			}
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x00090A00 File Offset: 0x0008EC00
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
						goto IL_9B;
					}
				}
				return;
			}
			IL_9B:
			if (asyncRequest != null)
			{
				asyncRequest.CompleteUser();
			}
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x00090AC4 File Offset: 0x0008ECC4
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
			finally
			{
				if (asyncRequest == null || flag)
				{
					this._NestedRead = 0;
				}
			}
			return result;
		}

		// Token: 0x06001EE3 RID: 7907 RVA: 0x00090BC4 File Offset: 0x0008EDC4
		private int StartReading(byte[] buffer, int offset, int count, AsyncProtocolRequest asyncRequest)
		{
			int result;
			while ((result = this.StartFrameHeader(buffer, offset, count, asyncRequest)) == -1)
			{
			}
			return result;
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x00090BE4 File Offset: 0x0008EDE4
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

		// Token: 0x06001EE5 RID: 7909 RVA: 0x00090C5C File Offset: 0x0008EE5C
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

		// Token: 0x06001EE6 RID: 7910 RVA: 0x00090D34 File Offset: 0x0008EF34
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

		// Token: 0x06001EE7 RID: 7911 RVA: 0x00090DB4 File Offset: 0x0008EFB4
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
		}

		// Token: 0x06001EE8 RID: 7912 RVA: 0x00090E3C File Offset: 0x0008F03C
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
		}

		// Token: 0x04001CFB RID: 7419
		private NegoState _NegoState;

		// Token: 0x04001CFC RID: 7420
		private string _Package;

		// Token: 0x04001CFD RID: 7421
		private IIdentity _RemoteIdentity;

		// Token: 0x04001CFE RID: 7422
		private static AsyncCallback _WriteCallback = new AsyncCallback(NegotiateStream.WriteCallback);

		// Token: 0x04001CFF RID: 7423
		private static AsyncProtocolCallback _ReadCallback = new AsyncProtocolCallback(NegotiateStream.ReadCallback);

		// Token: 0x04001D00 RID: 7424
		private int _NestedWrite;

		// Token: 0x04001D01 RID: 7425
		private int _NestedRead;

		// Token: 0x04001D02 RID: 7426
		private byte[] _ReadHeader;

		// Token: 0x04001D03 RID: 7427
		private byte[] _InternalBuffer;

		// Token: 0x04001D04 RID: 7428
		private int _InternalOffset;

		// Token: 0x04001D05 RID: 7429
		private int _InternalBufferCount;

		// Token: 0x04001D06 RID: 7430
		private FixedSizeReader _FrameReader;
	}
}
