using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.IO;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security;

namespace System.ServiceModel.Description
{
	// Token: 0x020003BF RID: 959
	[__DynamicallyInvokable]
	public class ClientCredentials : SecurityCredentialsManager, IEndpointBehavior
	{
		// Token: 0x060023E0 RID: 9184 RVA: 0x000828BC File Offset: 0x00080ABC
		[__DynamicallyInvokable]
		public ClientCredentials()
		{
			this.supportInteractive = true;
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x000828D8 File Offset: 0x00080AD8
		[__DynamicallyInvokable]
		protected ClientCredentials(ClientCredentials other)
		{
			if (other == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("other");
			}
			if (other.userName != null)
			{
				this.userName = new UserNamePasswordClientCredential(other.userName);
			}
			if (other.clientCertificate != null)
			{
				this.clientCertificate = new X509CertificateInitiatorClientCredential(other.clientCertificate);
			}
			if (other.serviceCertificate != null)
			{
				this.serviceCertificate = new X509CertificateRecipientClientCredential(other.serviceCertificate);
			}
			if (other.windows != null)
			{
				this.windows = new WindowsClientCredential(other.windows);
			}
			if (other.httpDigest != null)
			{
				this.httpDigest = new HttpDigestClientCredential(other.httpDigest);
			}
			if (other.issuedToken != null)
			{
				this.issuedToken = new IssuedTokenClientCredential(other.issuedToken);
			}
			if (other.peer != null)
			{
				this.peer = new PeerCredential(other.peer);
			}
			this.getInfoCardTokenCallback = other.getInfoCardTokenCallback;
			this.supportInteractive = other.supportInteractive;
			this.securityTokenHandlerCollectionManager = other.securityTokenHandlerCollectionManager;
			this.useIdentityConfiguration = other.useIdentityConfiguration;
			this.isReadOnly = other.isReadOnly;
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x060023E2 RID: 9186 RVA: 0x000829F4 File Offset: 0x00080BF4
		internal GetInfoCardTokenCallback GetInfoCardTokenCallback
		{
			get
			{
				if (this.getInfoCardTokenCallback == null)
				{
					GetInfoCardTokenCallback getInfoCardTokenCallback = new GetInfoCardTokenCallback(this.GetInfoCardSecurityToken);
					this.getInfoCardTokenCallback = getInfoCardTokenCallback;
				}
				return this.getInfoCardTokenCallback;
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x060023E3 RID: 9187 RVA: 0x00082A24 File Offset: 0x00080C24
		public IssuedTokenClientCredential IssuedToken
		{
			get
			{
				if (this.issuedToken == null)
				{
					this.issuedToken = new IssuedTokenClientCredential();
					if (this.isReadOnly)
					{
						this.issuedToken.MakeReadOnly();
					}
				}
				return this.issuedToken;
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x060023E4 RID: 9188 RVA: 0x00082A52 File Offset: 0x00080C52
		[__DynamicallyInvokable]
		public UserNamePasswordClientCredential UserName
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.userName == null)
				{
					this.userName = new UserNamePasswordClientCredential();
					if (this.isReadOnly)
					{
						this.userName.MakeReadOnly();
					}
				}
				return this.userName;
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x060023E5 RID: 9189 RVA: 0x00082A80 File Offset: 0x00080C80
		public X509CertificateInitiatorClientCredential ClientCertificate
		{
			get
			{
				if (this.clientCertificate == null)
				{
					this.clientCertificate = new X509CertificateInitiatorClientCredential();
					if (this.isReadOnly)
					{
						this.clientCertificate.MakeReadOnly();
					}
				}
				return this.clientCertificate;
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x060023E6 RID: 9190 RVA: 0x00082AAE File Offset: 0x00080CAE
		public X509CertificateRecipientClientCredential ServiceCertificate
		{
			get
			{
				if (this.serviceCertificate == null)
				{
					this.serviceCertificate = new X509CertificateRecipientClientCredential();
					if (this.isReadOnly)
					{
						this.serviceCertificate.MakeReadOnly();
					}
				}
				return this.serviceCertificate;
			}
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x060023E7 RID: 9191 RVA: 0x00082ADC File Offset: 0x00080CDC
		[__DynamicallyInvokable]
		public WindowsClientCredential Windows
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.windows == null)
				{
					this.windows = new WindowsClientCredential();
					if (this.isReadOnly)
					{
						this.windows.MakeReadOnly();
					}
				}
				return this.windows;
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x060023E8 RID: 9192 RVA: 0x00082B0A File Offset: 0x00080D0A
		[__DynamicallyInvokable]
		public HttpDigestClientCredential HttpDigest
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.httpDigest == null)
				{
					this.httpDigest = new HttpDigestClientCredential();
					if (this.isReadOnly)
					{
						this.httpDigest.MakeReadOnly();
					}
				}
				return this.httpDigest;
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x060023E9 RID: 9193 RVA: 0x00082B38 File Offset: 0x00080D38
		public PeerCredential Peer
		{
			get
			{
				if (this.peer == null)
				{
					this.peer = new PeerCredential();
					if (this.isReadOnly)
					{
						this.peer.MakeReadOnly();
					}
				}
				return this.peer;
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x060023EA RID: 9194 RVA: 0x00082B68 File Offset: 0x00080D68
		// (set) Token: 0x060023EB RID: 9195 RVA: 0x00082BC4 File Offset: 0x00080DC4
		public SecurityTokenHandlerCollectionManager SecurityTokenHandlerCollectionManager
		{
			get
			{
				if (this.securityTokenHandlerCollectionManager == null)
				{
					object obj = this.handlerCollectionLock;
					lock (obj)
					{
						if (this.securityTokenHandlerCollectionManager == null)
						{
							this.securityTokenHandlerCollectionManager = SecurityTokenHandlerCollectionManager.CreateDefaultSecurityTokenHandlerCollectionManager();
						}
					}
				}
				return this.securityTokenHandlerCollectionManager;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				this.securityTokenHandlerCollectionManager = value;
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x060023EC RID: 9196 RVA: 0x00082BEF File Offset: 0x00080DEF
		// (set) Token: 0x060023ED RID: 9197 RVA: 0x00082BF7 File Offset: 0x00080DF7
		public bool UseIdentityConfiguration
		{
			get
			{
				return this.useIdentityConfiguration;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				this.useIdentityConfiguration = value;
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x060023EE RID: 9198 RVA: 0x00082C22 File Offset: 0x00080E22
		// (set) Token: 0x060023EF RID: 9199 RVA: 0x00082C2A File Offset: 0x00080E2A
		public bool SupportInteractive
		{
			get
			{
				return this.supportInteractive;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
				}
				this.supportInteractive = value;
			}
		}

		// Token: 0x060023F0 RID: 9200 RVA: 0x00082C55 File Offset: 0x00080E55
		internal static ClientCredentials CreateDefaultCredentials()
		{
			return new ClientCredentials();
		}

		// Token: 0x060023F1 RID: 9201 RVA: 0x00082C5C File Offset: 0x00080E5C
		public override SecurityTokenManager CreateSecurityTokenManager()
		{
			return new ClientCredentialsSecurityTokenManager(this.Clone());
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x00082C69 File Offset: 0x00080E69
		[__DynamicallyInvokable]
		protected virtual ClientCredentials CloneCore()
		{
			return new ClientCredentials(this);
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x00082C74 File Offset: 0x00080E74
		[__DynamicallyInvokable]
		public ClientCredentials Clone()
		{
			ClientCredentials clientCredentials = this.CloneCore();
			if (clientCredentials == null || clientCredentials.GetType() != base.GetType())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException(SR.GetString("CloneNotImplementedCorrectly", new object[]
				{
					base.GetType(),
					(clientCredentials != null) ? clientCredentials.ToString() : "null"
				})));
			}
			return clientCredentials;
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x00082CDB File Offset: 0x00080EDB
		[__DynamicallyInvokable]
		void IEndpointBehavior.Validate(ServiceEndpoint serviceEndpoint)
		{
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x00082CE0 File Offset: 0x00080EE0
		[__DynamicallyInvokable]
		void IEndpointBehavior.AddBindingParameters(ServiceEndpoint serviceEndpoint, BindingParameterCollection bindingParameters)
		{
			if (bindingParameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("bindingParameters");
			}
			SecurityCredentialsManager securityCredentialsManager = bindingParameters.Find<SecurityCredentialsManager>();
			if (securityCredentialsManager != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MultipleSecurityCredentialsManagersInChannelBindingParameters", new object[]
				{
					securityCredentialsManager
				})));
			}
			bindingParameters.Add(this);
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x00082D35 File Offset: 0x00080F35
		[__DynamicallyInvokable]
		void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint, EndpointDispatcher endpointDispatcher)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFXEndpointBehaviorUsedOnWrongSide", new object[]
			{
				typeof(ClientCredentials).Name
			})));
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x00082D68 File Offset: 0x00080F68
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void AddInteractiveInitializers(ServiceEndpoint serviceEndpoint, ClientRuntime behavior)
		{
			CardSpacePolicyElement[] array;
			Uri uri;
			if (InfoCardHelper.IsInfocardRequired(serviceEndpoint.Binding, this, this.CreateSecurityTokenManager(), EndpointAddress.AnonymousAddress, out array, out uri))
			{
				behavior.InteractiveChannelInitializers.Add(new InfocardInteractiveChannelInitializer(this, serviceEndpoint.Binding));
			}
		}

		// Token: 0x060023F8 RID: 9208 RVA: 0x00082DAC File Offset: 0x00080FAC
		[__DynamicallyInvokable]
		public virtual void ApplyClientBehavior(ServiceEndpoint serviceEndpoint, ClientRuntime behavior)
		{
			if (serviceEndpoint == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serviceEndpoint");
			}
			if (serviceEndpoint.Binding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serviceEndpoint.Binding");
			}
			if (serviceEndpoint.Binding.CreateBindingElements().Find<SecurityBindingElement>() == null)
			{
				return;
			}
			try
			{
				this.AddInteractiveInitializers(serviceEndpoint, behavior);
			}
			catch (FileNotFoundException)
			{
			}
		}

		// Token: 0x060023F9 RID: 9209 RVA: 0x00082E18 File Offset: 0x00081018
		internal void MakeReadOnly()
		{
			this.isReadOnly = true;
			if (this.clientCertificate != null)
			{
				this.clientCertificate.MakeReadOnly();
			}
			if (this.serviceCertificate != null)
			{
				this.serviceCertificate.MakeReadOnly();
			}
			if (this.userName != null)
			{
				this.userName.MakeReadOnly();
			}
			if (this.windows != null)
			{
				this.windows.MakeReadOnly();
			}
			if (this.httpDigest != null)
			{
				this.httpDigest.MakeReadOnly();
			}
			if (this.issuedToken != null)
			{
				this.issuedToken.MakeReadOnly();
			}
			if (this.peer != null)
			{
				this.peer.MakeReadOnly();
			}
		}

		// Token: 0x060023FA RID: 9210 RVA: 0x00082EB1 File Offset: 0x000810B1
		protected internal virtual SecurityToken GetInfoCardSecurityToken(bool requiresInfoCard, CardSpacePolicyElement[] chain, SecurityTokenSerializer tokenSerializer)
		{
			if (!requiresInfoCard)
			{
				return null;
			}
			return CardSpaceSelector.GetToken(chain, tokenSerializer);
		}

		// Token: 0x0400202D RID: 8237
		internal const bool SupportInteractiveDefault = true;

		// Token: 0x0400202E RID: 8238
		private UserNamePasswordClientCredential userName;

		// Token: 0x0400202F RID: 8239
		private X509CertificateInitiatorClientCredential clientCertificate;

		// Token: 0x04002030 RID: 8240
		private X509CertificateRecipientClientCredential serviceCertificate;

		// Token: 0x04002031 RID: 8241
		private WindowsClientCredential windows;

		// Token: 0x04002032 RID: 8242
		private HttpDigestClientCredential httpDigest;

		// Token: 0x04002033 RID: 8243
		private IssuedTokenClientCredential issuedToken;

		// Token: 0x04002034 RID: 8244
		private PeerCredential peer;

		// Token: 0x04002035 RID: 8245
		private bool supportInteractive;

		// Token: 0x04002036 RID: 8246
		private bool isReadOnly;

		// Token: 0x04002037 RID: 8247
		private GetInfoCardTokenCallback getInfoCardTokenCallback;

		// Token: 0x04002038 RID: 8248
		private bool useIdentityConfiguration;

		// Token: 0x04002039 RID: 8249
		private SecurityTokenHandlerCollectionManager securityTokenHandlerCollectionManager;

		// Token: 0x0400203A RID: 8250
		private object handlerCollectionLock = new object();
	}
}
