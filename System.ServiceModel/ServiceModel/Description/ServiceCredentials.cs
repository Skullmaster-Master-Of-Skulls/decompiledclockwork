using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Configuration;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Description
{
	// Token: 0x020003C2 RID: 962
	public class ServiceCredentials : SecurityCredentialsManager, IServiceBehavior
	{
		// Token: 0x060023FC RID: 9212 RVA: 0x00082ED8 File Offset: 0x000810D8
		public ServiceCredentials()
		{
			this.userName = new UserNamePasswordServiceCredential();
			this.clientCertificate = new X509CertificateInitiatorServiceCredential();
			this.serviceCertificate = new X509CertificateRecipientServiceCredential();
			this.windows = new WindowsServiceCredential();
			this.issuedToken = new IssuedTokenServiceCredential();
			this.peer = new PeerCredential();
			this.secureConversation = new SecureConversationServiceCredential();
			this.exceptionMapper = new ExceptionMapper();
			this.UseIdentityConfiguration = false;
		}

		// Token: 0x060023FD RID: 9213 RVA: 0x00082F54 File Offset: 0x00081154
		protected ServiceCredentials(ServiceCredentials other)
		{
			if (other == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("other");
			}
			this.userName = new UserNamePasswordServiceCredential(other.userName);
			this.clientCertificate = new X509CertificateInitiatorServiceCredential(other.clientCertificate);
			this.serviceCertificate = new X509CertificateRecipientServiceCredential(other.serviceCertificate);
			this.windows = new WindowsServiceCredential(other.windows);
			this.issuedToken = new IssuedTokenServiceCredential(other.issuedToken);
			this.peer = new PeerCredential(other.peer);
			this.secureConversation = new SecureConversationServiceCredential(other.secureConversation);
			this.identityConfiguration = other.identityConfiguration;
			this.saveBootstrapTokenInSession = other.saveBootstrapTokenInSession;
			this.exceptionMapper = other.exceptionMapper;
			this.UseIdentityConfiguration = other.useIdentityConfiguration;
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x060023FE RID: 9214 RVA: 0x00083028 File Offset: 0x00081228
		public UserNamePasswordServiceCredential UserNameAuthentication
		{
			get
			{
				return this.userName;
			}
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x060023FF RID: 9215 RVA: 0x00083030 File Offset: 0x00081230
		public X509CertificateInitiatorServiceCredential ClientCertificate
		{
			get
			{
				return this.clientCertificate;
			}
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06002400 RID: 9216 RVA: 0x00083038 File Offset: 0x00081238
		public X509CertificateRecipientServiceCredential ServiceCertificate
		{
			get
			{
				return this.serviceCertificate;
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06002401 RID: 9217 RVA: 0x00083040 File Offset: 0x00081240
		public WindowsServiceCredential WindowsAuthentication
		{
			get
			{
				return this.windows;
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06002402 RID: 9218 RVA: 0x00083048 File Offset: 0x00081248
		public IssuedTokenServiceCredential IssuedTokenAuthentication
		{
			get
			{
				return this.issuedToken;
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06002403 RID: 9219 RVA: 0x00083050 File Offset: 0x00081250
		public PeerCredential Peer
		{
			get
			{
				return this.peer;
			}
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06002404 RID: 9220 RVA: 0x00083058 File Offset: 0x00081258
		public SecureConversationServiceCredential SecureConversationAuthentication
		{
			get
			{
				return this.secureConversation;
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06002405 RID: 9221 RVA: 0x00083060 File Offset: 0x00081260
		// (set) Token: 0x06002406 RID: 9222 RVA: 0x00083068 File Offset: 0x00081268
		public ExceptionMapper ExceptionMapper
		{
			get
			{
				return this.exceptionMapper;
			}
			set
			{
				this.ThrowIfImmutable();
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.exceptionMapper = value;
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06002407 RID: 9223 RVA: 0x0008308A File Offset: 0x0008128A
		// (set) Token: 0x06002408 RID: 9224 RVA: 0x00083092 File Offset: 0x00081292
		public IdentityConfiguration IdentityConfiguration
		{
			get
			{
				return this.identityConfiguration;
			}
			set
			{
				this.ThrowIfImmutable();
				this.identityConfiguration = value;
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06002409 RID: 9225 RVA: 0x000830A1 File Offset: 0x000812A1
		// (set) Token: 0x0600240A RID: 9226 RVA: 0x000830A9 File Offset: 0x000812A9
		public bool UseIdentityConfiguration
		{
			get
			{
				return this.useIdentityConfiguration;
			}
			set
			{
				this.ThrowIfImmutable();
				this.useIdentityConfiguration = value;
				if (this.identityConfiguration == null && this.useIdentityConfiguration)
				{
					this.identityConfiguration = new IdentityConfiguration();
				}
			}
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x000830D3 File Offset: 0x000812D3
		internal static ServiceCredentials CreateDefaultCredentials()
		{
			return new ServiceCredentials();
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x000830DA File Offset: 0x000812DA
		public override SecurityTokenManager CreateSecurityTokenManager()
		{
			if (this.useIdentityConfiguration)
			{
				return new FederatedSecurityTokenManager(this.Clone());
			}
			return new ServiceCredentialsSecurityTokenManager(this.Clone());
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x000830FB File Offset: 0x000812FB
		protected virtual ServiceCredentials CloneCore()
		{
			return new ServiceCredentials(this);
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x00083104 File Offset: 0x00081304
		public ServiceCredentials Clone()
		{
			ServiceCredentials serviceCredentials = this.CloneCore();
			if (serviceCredentials == null || serviceCredentials.GetType() != base.GetType())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException(SR.GetString("CloneNotImplementedCorrectly", new object[]
				{
					base.GetType(),
					(serviceCredentials != null) ? serviceCredentials.ToString() : "null"
				})));
			}
			return serviceCredentials;
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x0008316B File Offset: 0x0008136B
		void IServiceBehavior.Validate(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
			if (this.UseIdentityConfiguration)
			{
				this.ConfigureServiceHost(serviceHostBase);
			}
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x0008317C File Offset: 0x0008137C
		private void ConfigureServiceHost(ServiceHostBase serviceHost)
		{
			if (serviceHost == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serviceHost");
			}
			if (serviceHost.State != CommunicationState.Created && serviceHost.State != CommunicationState.Opening)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperInvalidOperation(SR.GetString("ID4041", new object[]
				{
					serviceHost
				}));
			}
			if (this.ServiceCertificate != null)
			{
				X509Certificate2 certificate = this.ServiceCertificate.Certificate;
				if (certificate != null)
				{
					this.IdentityConfiguration.ServiceCertificate = certificate;
				}
			}
			if (this.IssuedTokenAuthentication != null && this.IssuedTokenAuthentication.KnownCertificates != null && this.IssuedTokenAuthentication.KnownCertificates.Count > 0)
			{
				this.IdentityConfiguration.KnownIssuerCertificates = new List<X509Certificate2>(this.IssuedTokenAuthentication.KnownCertificates);
			}
			if (!this.IdentityConfiguration.IsInitialized)
			{
				this.IdentityConfiguration.Initialize();
			}
			if (serviceHost.Authorization.ServiceAuthorizationManager == null)
			{
				serviceHost.Authorization.ServiceAuthorizationManager = new IdentityModelServiceAuthorizationManager();
			}
			else if (!(serviceHost.Authorization.ServiceAuthorizationManager is IdentityModelServiceAuthorizationManager))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4039")));
			}
			if (this.IdentityConfiguration.SecurityTokenHandlers[typeof(SecurityContextSecurityToken)] != null && serviceHost.Credentials.SecureConversationAuthentication.SecurityStateEncoder == null)
			{
				serviceHost.Credentials.SecureConversationAuthentication.SecurityStateEncoder = new NoOpSecurityStateEncoder();
			}
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x000832D8 File Offset: 0x000814D8
		void IServiceBehavior.AddBindingParameters(ServiceDescription description, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection parameters)
		{
			if (parameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parameters");
			}
			SecurityCredentialsManager securityCredentialsManager = parameters.Find<SecurityCredentialsManager>();
			if (securityCredentialsManager != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MultipleSecurityCredentialsManagersInServiceBindingParameters", new object[]
				{
					securityCredentialsManager
				})));
			}
			parameters.Add(this);
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x00083330 File Offset: 0x00081530
		void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
			for (int i = 0; i < serviceHostBase.ChannelDispatchers.Count; i++)
			{
				ChannelDispatcher channelDispatcher = serviceHostBase.ChannelDispatchers[i] as ChannelDispatcher;
				if (channelDispatcher != null && !ServiceMetadataBehavior.IsHttpGetMetadataDispatcher(description, channelDispatcher))
				{
					foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
					{
						DispatchRuntime dispatchRuntime = endpointDispatcher.DispatchRuntime;
						dispatchRuntime.RequireClaimsPrincipalOnOperationContext = this.useIdentityConfiguration;
					}
				}
			}
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x000833C0 File Offset: 0x000815C0
		internal void MakeReadOnly()
		{
			this.isReadOnly = true;
			this.ClientCertificate.MakeReadOnly();
			this.IssuedTokenAuthentication.MakeReadOnly();
			this.Peer.MakeReadOnly();
			this.SecureConversationAuthentication.MakeReadOnly();
			this.ServiceCertificate.MakeReadOnly();
			this.UserNameAuthentication.MakeReadOnly();
			this.WindowsAuthentication.MakeReadOnly();
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x00083421 File Offset: 0x00081621
		private void ThrowIfImmutable()
		{
			if (this.isReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
		}

		// Token: 0x04002041 RID: 8257
		private UserNamePasswordServiceCredential userName;

		// Token: 0x04002042 RID: 8258
		private X509CertificateInitiatorServiceCredential clientCertificate;

		// Token: 0x04002043 RID: 8259
		private X509CertificateRecipientServiceCredential serviceCertificate;

		// Token: 0x04002044 RID: 8260
		private WindowsServiceCredential windows;

		// Token: 0x04002045 RID: 8261
		private IssuedTokenServiceCredential issuedToken;

		// Token: 0x04002046 RID: 8262
		private PeerCredential peer;

		// Token: 0x04002047 RID: 8263
		private SecureConversationServiceCredential secureConversation;

		// Token: 0x04002048 RID: 8264
		private bool useIdentityConfiguration;

		// Token: 0x04002049 RID: 8265
		private bool isReadOnly;

		// Token: 0x0400204A RID: 8266
		private bool saveBootstrapTokenInSession = true;

		// Token: 0x0400204B RID: 8267
		private IdentityConfiguration identityConfiguration;

		// Token: 0x0400204C RID: 8268
		private ExceptionMapper exceptionMapper;
	}
}
