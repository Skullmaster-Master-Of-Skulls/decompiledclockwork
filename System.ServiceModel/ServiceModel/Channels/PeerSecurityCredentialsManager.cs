using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A32 RID: 2610
	internal class PeerSecurityCredentialsManager : SecurityCredentialsManager, IEndpointBehavior, IServiceBehavior
	{
		// Token: 0x0600679B RID: 26523 RVA: 0x0018352D File Offset: 0x0018172D
		public PeerSecurityCredentialsManager(SecurityTokenManager manager, PeerAuthenticationMode mode, bool messageAuth)
		{
			this.manager = manager;
			this.mode = mode;
			this.messageAuth = messageAuth;
		}

		// Token: 0x0600679C RID: 26524 RVA: 0x00183551 File Offset: 0x00181751
		public PeerSecurityCredentialsManager(PeerCredential credential, PeerAuthenticationMode mode, bool messageAuth)
		{
			this.credential = credential;
			this.mode = mode;
			this.messageAuth = messageAuth;
		}

		// Token: 0x170018D1 RID: 6353
		// (get) Token: 0x0600679D RID: 26525 RVA: 0x00183575 File Offset: 0x00181775
		// (set) Token: 0x0600679E RID: 26526 RVA: 0x0018357D File Offset: 0x0018177D
		public PeerSecurityManager Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		// Token: 0x0600679F RID: 26527 RVA: 0x00183588 File Offset: 0x00181788
		public override SecurityTokenManager CreateSecurityTokenManager()
		{
			if (this.manager != null)
			{
				return new PeerSecurityCredentialsManager.PeerClientSecurityTokenManager(this.parent, this.manager, this.mode, this.messageAuth);
			}
			return new PeerSecurityCredentialsManager.PeerClientSecurityTokenManager(this.parent, this.credential, this.mode, this.messageAuth);
		}

		// Token: 0x060067A0 RID: 26528 RVA: 0x001835D8 File Offset: 0x001817D8
		public PeerSecurityCredentialsManager()
		{
		}

		// Token: 0x060067A1 RID: 26529 RVA: 0x001835E8 File Offset: 0x001817E8
		public PeerSecurityCredentialsManager CloneForTransport()
		{
			PeerSecurityCredentialsManager peerSecurityCredentialsManager = new PeerSecurityCredentialsManager();
			if (this.credential != null)
			{
				peerSecurityCredentialsManager.credential = new PeerCredential(this.credential);
			}
			peerSecurityCredentialsManager.mode = this.mode;
			peerSecurityCredentialsManager.messageAuth = this.messageAuth;
			peerSecurityCredentialsManager.manager = this.manager;
			peerSecurityCredentialsManager.parent = this.parent;
			return peerSecurityCredentialsManager;
		}

		// Token: 0x170018D2 RID: 6354
		// (get) Token: 0x060067A2 RID: 26530 RVA: 0x00183645 File Offset: 0x00181845
		internal PeerCredential Credential
		{
			get
			{
				return this.credential;
			}
		}

		// Token: 0x170018D3 RID: 6355
		// (get) Token: 0x060067A3 RID: 26531 RVA: 0x00183650 File Offset: 0x00181850
		internal string Password
		{
			get
			{
				if (this.credential != null)
				{
					return this.credential.MeshPassword;
				}
				ServiceModelSecurityTokenRequirement tokenRequirement = PeerSecurityCredentialsManager.PeerClientSecurityTokenManager.CreateRequirement(SecurityTokenTypes.UserName);
				UserNameSecurityTokenProvider userNameSecurityTokenProvider = this.manager.CreateSecurityTokenProvider(tokenRequirement) as UserNameSecurityTokenProvider;
				if (userNameSecurityTokenProvider == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("TokenProvider");
				}
				UserNameSecurityToken userNameSecurityToken = userNameSecurityTokenProvider.GetToken(ServiceDefaults.SendTimeout) as UserNameSecurityToken;
				if (userNameSecurityToken == null || string.IsNullOrEmpty(userNameSecurityToken.Password))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("password");
				}
				return userNameSecurityToken.Password;
			}
		}

		// Token: 0x170018D4 RID: 6356
		// (get) Token: 0x060067A4 RID: 26532 RVA: 0x001836D8 File Offset: 0x001818D8
		internal X509Certificate2 Certificate
		{
			get
			{
				X509Certificate2 x509Certificate;
				if (this.mode == PeerAuthenticationMode.Password && this.ssl != null)
				{
					x509Certificate = this.ssl.GetX509Certificate();
				}
				if (this.credential != null)
				{
					x509Certificate = this.credential.Certificate;
				}
				else
				{
					ServiceModelSecurityTokenRequirement tokenRequirement = PeerSecurityCredentialsManager.PeerClientSecurityTokenManager.CreateRequirement(SecurityTokenTypes.X509Certificate);
					X509SecurityTokenProvider x509SecurityTokenProvider = this.manager.CreateSecurityTokenProvider(tokenRequirement) as X509SecurityTokenProvider;
					if (x509SecurityTokenProvider == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("TokenProvider");
					}
					X509SecurityToken x509SecurityToken = x509SecurityTokenProvider.GetToken(ServiceDefaults.SendTimeout) as X509SecurityToken;
					if (x509SecurityToken == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("token");
					}
					x509Certificate = x509SecurityToken.Certificate;
				}
				if (x509Certificate == null && this.mode == PeerAuthenticationMode.Password)
				{
					this.ssl = this.parent.GetCertificate();
					x509Certificate = this.ssl.GetX509Certificate();
				}
				return x509Certificate;
			}
		}

		// Token: 0x060067A5 RID: 26533 RVA: 0x0018379F File Offset: 0x0018199F
		void IEndpointBehavior.Validate(ServiceEndpoint serviceEndpoint)
		{
		}

		// Token: 0x060067A6 RID: 26534 RVA: 0x001837A1 File Offset: 0x001819A1
		void IEndpointBehavior.AddBindingParameters(ServiceEndpoint serviceEndpoint, BindingParameterCollection bindingParameters)
		{
			if (bindingParameters != null)
			{
				bindingParameters.Add(this);
			}
		}

		// Token: 0x060067A7 RID: 26535 RVA: 0x001837AD File Offset: 0x001819AD
		void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint, EndpointDispatcher endpointDispatcher)
		{
		}

		// Token: 0x060067A8 RID: 26536 RVA: 0x001837AF File Offset: 0x001819AF
		void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint serviceEndpoint, ClientRuntime behavior)
		{
		}

		// Token: 0x060067A9 RID: 26537 RVA: 0x001837B1 File Offset: 0x001819B1
		void IServiceBehavior.Validate(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
		}

		// Token: 0x060067AA RID: 26538 RVA: 0x001837B3 File Offset: 0x001819B3
		void IServiceBehavior.AddBindingParameters(ServiceDescription description, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection parameters)
		{
			if (parameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parameters");
			}
			parameters.Add(this);
		}

		// Token: 0x060067AB RID: 26539 RVA: 0x001837D1 File Offset: 0x001819D1
		void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
		}

		// Token: 0x060067AC RID: 26540 RVA: 0x001837D4 File Offset: 0x001819D4
		public override bool Equals(object other)
		{
			PeerSecurityCredentialsManager peerSecurityCredentialsManager = other as PeerSecurityCredentialsManager;
			if (peerSecurityCredentialsManager == null)
			{
				return false;
			}
			if (this.credential != null)
			{
				return this.credential.Equals(peerSecurityCredentialsManager.credential, this.mode, this.messageAuth);
			}
			return this.manager.Equals(peerSecurityCredentialsManager.manager);
		}

		// Token: 0x060067AD RID: 26541 RVA: 0x00183824 File Offset: 0x00181A24
		public void CheckIfCompatible(PeerSecurityCredentialsManager that)
		{
			if (that == null)
			{
				PeerExceptionHelper.ThrowInvalidOperation_PeerConflictingPeerNodeSettings(PeerBindingPropertyNames.Credentials);
			}
			if (this.mode == PeerAuthenticationMode.None)
			{
				return;
			}
			if (this.mode == PeerAuthenticationMode.Password && this.Password != that.Password)
			{
				PeerExceptionHelper.ThrowInvalidOperation_PeerConflictingPeerNodeSettings(PeerBindingPropertyNames.Password);
			}
			if (!this.Certificate.Equals(that.Certificate))
			{
				PeerExceptionHelper.ThrowInvalidOperation_PeerConflictingPeerNodeSettings(PeerBindingPropertyNames.Certificate);
			}
		}

		// Token: 0x060067AE RID: 26542 RVA: 0x0018388A File Offset: 0x00181A8A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04003B80 RID: 15232
		private SecurityTokenManager manager;

		// Token: 0x04003B81 RID: 15233
		private PeerCredential credential;

		// Token: 0x04003B82 RID: 15234
		private bool messageAuth;

		// Token: 0x04003B83 RID: 15235
		private PeerAuthenticationMode mode = PeerAuthenticationMode.Password;

		// Token: 0x04003B84 RID: 15236
		private SelfSignedCertificate ssl;

		// Token: 0x04003B85 RID: 15237
		private PeerSecurityManager parent;

		// Token: 0x02000E71 RID: 3697
		public class PeerClientSecurityTokenManager : SecurityTokenManager
		{
			// Token: 0x060083D4 RID: 33748 RVA: 0x001E7AAE File Offset: 0x001E5CAE
			public PeerClientSecurityTokenManager(PeerSecurityManager parent, PeerCredential credential, PeerAuthenticationMode mode, bool messageAuth)
			{
				this.credential = credential;
				this.mode = mode;
				this.messageAuth = messageAuth;
				this.parent = parent;
			}

			// Token: 0x060083D5 RID: 33749 RVA: 0x001E7AD3 File Offset: 0x001E5CD3
			public PeerClientSecurityTokenManager(PeerSecurityManager parent, SecurityTokenManager manager, PeerAuthenticationMode mode, bool messageAuth)
			{
				this.delegateManager = manager;
				this.mode = mode;
				this.messageAuth = messageAuth;
				this.parent = parent;
			}

			// Token: 0x060083D6 RID: 33750 RVA: 0x001E7AF8 File Offset: 0x001E5CF8
			internal static ServiceModelSecurityTokenRequirement CreateRequirement(string tokenType)
			{
				return PeerSecurityCredentialsManager.PeerClientSecurityTokenManager.CreateRequirement(tokenType, false);
			}

			// Token: 0x060083D7 RID: 33751 RVA: 0x001E7B04 File Offset: 0x001E5D04
			internal static ServiceModelSecurityTokenRequirement CreateRequirement(string tokenType, bool forMessageValidation)
			{
				InitiatorServiceModelSecurityTokenRequirement initiatorServiceModelSecurityTokenRequirement = new InitiatorServiceModelSecurityTokenRequirement();
				initiatorServiceModelSecurityTokenRequirement.TokenType = tokenType;
				initiatorServiceModelSecurityTokenRequirement.TransportScheme = "net.p2p";
				if (forMessageValidation)
				{
					initiatorServiceModelSecurityTokenRequirement.Properties[SecurityTokenRequirement.PeerAuthenticationMode] = SecurityMode.Message;
				}
				else
				{
					initiatorServiceModelSecurityTokenRequirement.Properties[SecurityTokenRequirement.PeerAuthenticationMode] = SecurityMode.Transport;
				}
				return initiatorServiceModelSecurityTokenRequirement;
			}

			// Token: 0x060083D8 RID: 33752 RVA: 0x001E7B5C File Offset: 0x001E5D5C
			private UserNameSecurityTokenProvider GetPasswordTokenProvider()
			{
				if (this.delegateManager == null)
				{
					return new UserNameSecurityTokenProvider(string.Empty, this.credential.MeshPassword);
				}
				ServiceModelSecurityTokenRequirement serviceModelSecurityTokenRequirement = PeerSecurityCredentialsManager.PeerClientSecurityTokenManager.CreateRequirement(SecurityTokenTypes.UserName);
				UserNameSecurityTokenProvider userNameSecurityTokenProvider = this.delegateManager.CreateSecurityTokenProvider(serviceModelSecurityTokenRequirement) as UserNameSecurityTokenProvider;
				if (userNameSecurityTokenProvider == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SecurityTokenManagerCannotCreateProviderForRequirement", new object[]
					{
						serviceModelSecurityTokenRequirement
					})));
				}
				return userNameSecurityTokenProvider;
			}

			// Token: 0x060083D9 RID: 33753 RVA: 0x001E7BCC File Offset: 0x001E5DCC
			public override SecurityTokenSerializer CreateSecurityTokenSerializer(SecurityTokenVersion version)
			{
				if (this.delegateManager != null)
				{
					return this.delegateManager.CreateSecurityTokenSerializer(version);
				}
				MessageSecurityTokenVersion messageSecurityTokenVersion = version as MessageSecurityTokenVersion;
				if (messageSecurityTokenVersion != null)
				{
					return new WSSecurityTokenSerializer(messageSecurityTokenVersion.SecurityVersion, messageSecurityTokenVersion.TrustVersion, messageSecurityTokenVersion.SecureConversationVersion, messageSecurityTokenVersion.EmitBspRequiredAttributes, null, null, null);
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SecurityTokenManagerCannotCreateSerializerForVersion", new object[]
				{
					version
				})));
			}

			// Token: 0x060083DA RID: 33754 RVA: 0x001E7C3C File Offset: 0x001E5E3C
			public override SecurityTokenProvider CreateSecurityTokenProvider(SecurityTokenRequirement tokenRequirement)
			{
				ServiceModelSecurityTokenRequirement serviceModelSecurityTokenRequirement = tokenRequirement as ServiceModelSecurityTokenRequirement;
				if (serviceModelSecurityTokenRequirement == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenRequirement");
				}
				if (this.IsX509TokenRequirement(serviceModelSecurityTokenRequirement))
				{
					if (this.IsForConnectionValidator(serviceModelSecurityTokenRequirement))
					{
						SecurityTokenProvider securityTokenProvider = null;
						if (this.ssc != null)
						{
							securityTokenProvider = new X509SecurityTokenProvider(this.ssc.GetX509Certificate());
						}
						else if (this.delegateManager != null)
						{
							serviceModelSecurityTokenRequirement.Properties[SecurityTokenRequirement.PeerAuthenticationMode] = SecurityMode.Transport;
							serviceModelSecurityTokenRequirement.TransportScheme = "net.p2p";
							securityTokenProvider = this.delegateManager.CreateSecurityTokenProvider(tokenRequirement);
						}
						else if (this.credential.Certificate != null)
						{
							securityTokenProvider = new X509SecurityTokenProvider(this.credential.Certificate);
						}
						if (securityTokenProvider == null && this.mode == PeerAuthenticationMode.Password)
						{
							this.ssc = this.parent.GetCertificate();
							securityTokenProvider = new X509SecurityTokenProvider(this.ssc.GetX509Certificate());
						}
						return securityTokenProvider;
					}
					if (this.delegateManager != null)
					{
						serviceModelSecurityTokenRequirement.TransportScheme = "net.p2p";
						serviceModelSecurityTokenRequirement.Properties[SecurityTokenRequirement.PeerAuthenticationMode] = SecurityMode.Message;
						return this.delegateManager.CreateSecurityTokenProvider(tokenRequirement);
					}
					X509CertificateValidator validator;
					if (!this.credential.MessageSenderAuthentication.TryGetCertificateValidator(out validator))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("TokenType");
					}
					return new PeerX509TokenProvider(validator, this.credential.Certificate);
				}
				else
				{
					if (this.IsPasswordTokenRequirement(serviceModelSecurityTokenRequirement))
					{
						return this.GetPasswordTokenProvider();
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("TokenType");
				}
			}

			// Token: 0x060083DB RID: 33755 RVA: 0x001E7DAC File Offset: 0x001E5FAC
			private bool IsPasswordTokenRequirement(ServiceModelSecurityTokenRequirement requirement)
			{
				return requirement != null && requirement.TokenType == SecurityTokenTypes.UserName;
			}

			// Token: 0x060083DC RID: 33756 RVA: 0x001E7DC3 File Offset: 0x001E5FC3
			private bool IsX509TokenRequirement(ServiceModelSecurityTokenRequirement requirement)
			{
				return requirement != null && requirement.TokenType == SecurityTokenTypes.X509Certificate;
			}

			// Token: 0x060083DD RID: 33757 RVA: 0x001E7DDA File Offset: 0x001E5FDA
			private bool IsForConnectionValidator(ServiceModelSecurityTokenRequirement requirement)
			{
				return requirement.TransportScheme == "net.tcp" && requirement.SecurityBindingElement == null && requirement.MessageSecurityVersion == null;
			}

			// Token: 0x060083DE RID: 33758 RVA: 0x001E7E04 File Offset: 0x001E6004
			public override SecurityTokenAuthenticator CreateSecurityTokenAuthenticator(SecurityTokenRequirement tokenRequirement, out SecurityTokenResolver outOfBandTokenResolver)
			{
				ServiceModelSecurityTokenRequirement serviceModelSecurityTokenRequirement = tokenRequirement as ServiceModelSecurityTokenRequirement;
				outOfBandTokenResolver = null;
				if (serviceModelSecurityTokenRequirement == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenRequirement");
				}
				if (!this.IsX509TokenRequirement(serviceModelSecurityTokenRequirement))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("tokenRequirement");
				}
				if (this.mode == PeerAuthenticationMode.Password && this.IsForConnectionValidator(serviceModelSecurityTokenRequirement))
				{
					return new X509SecurityTokenAuthenticator(X509CertificateValidator.None);
				}
				if (this.delegateManager != null)
				{
					if (this.IsForConnectionValidator(serviceModelSecurityTokenRequirement))
					{
						serviceModelSecurityTokenRequirement.TransportScheme = "net.p2p";
						serviceModelSecurityTokenRequirement.Properties[SecurityTokenRequirement.PeerAuthenticationMode] = SecurityMode.Transport;
					}
					else
					{
						serviceModelSecurityTokenRequirement.TransportScheme = "net.p2p";
						serviceModelSecurityTokenRequirement.Properties[SecurityTokenRequirement.PeerAuthenticationMode] = SecurityMode.Message;
					}
					return this.delegateManager.CreateSecurityTokenAuthenticator(tokenRequirement, out outOfBandTokenResolver);
				}
				X509CertificateValidator validator = null;
				if (this.IsForConnectionValidator(serviceModelSecurityTokenRequirement))
				{
					if (this.mode == PeerAuthenticationMode.MutualCertificate)
					{
						if (!this.credential.PeerAuthentication.TryGetCertificateValidator(out validator))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SecurityTokenManagerCannotCreateProviderForRequirement", new object[]
							{
								serviceModelSecurityTokenRequirement
							})));
						}
					}
					else
					{
						validator = X509CertificateValidator.None;
					}
				}
				else if (!this.credential.MessageSenderAuthentication.TryGetCertificateValidator(out validator))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SecurityTokenManagerCannotCreateProviderForRequirement", new object[]
					{
						serviceModelSecurityTokenRequirement
					})));
				}
				return new X509SecurityTokenAuthenticator(validator);
			}

			// Token: 0x060083DF RID: 33759 RVA: 0x001E7F60 File Offset: 0x001E6160
			public override bool Equals(object other)
			{
				PeerSecurityCredentialsManager.PeerClientSecurityTokenManager peerClientSecurityTokenManager = other as PeerSecurityCredentialsManager.PeerClientSecurityTokenManager;
				if (peerClientSecurityTokenManager == null)
				{
					return false;
				}
				if (this.credential != null)
				{
					return peerClientSecurityTokenManager.credential != null && this.credential.Equals(peerClientSecurityTokenManager.credential, this.mode, this.messageAuth);
				}
				return this.delegateManager.Equals(peerClientSecurityTokenManager.delegateManager);
			}

			// Token: 0x060083E0 RID: 33760 RVA: 0x001E7FBD File Offset: 0x001E61BD
			internal bool HasCompatibleMessageSecuritySettings(PeerSecurityCredentialsManager.PeerClientSecurityTokenManager that)
			{
				if (this.credential != null)
				{
					return that.credential != null && this.credential.Equals(that.credential);
				}
				return this.delegateManager.Equals(that.delegateManager);
			}

			// Token: 0x060083E1 RID: 33761 RVA: 0x001E7FF4 File Offset: 0x001E61F4
			public override int GetHashCode()
			{
				if (this.credential != null)
				{
					return this.credential.GetHashCode();
				}
				if (this.delegateManager != null)
				{
					return this.delegateManager.GetHashCode();
				}
				return 0;
			}

			// Token: 0x04004B15 RID: 19221
			private SecurityTokenManager delegateManager;

			// Token: 0x04004B16 RID: 19222
			private PeerCredential credential;

			// Token: 0x04004B17 RID: 19223
			private PeerAuthenticationMode mode;

			// Token: 0x04004B18 RID: 19224
			private bool messageAuth;

			// Token: 0x04004B19 RID: 19225
			private SelfSignedCertificate ssc;

			// Token: 0x04004B1A RID: 19226
			private PeerSecurityManager parent;
		}
	}
}
