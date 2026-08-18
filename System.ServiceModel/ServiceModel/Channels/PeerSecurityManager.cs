using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Claims;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Runtime;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A31 RID: 2609
	internal class PeerSecurityManager
	{
		// Token: 0x06006778 RID: 26488 RVA: 0x00182913 File Offset: 0x00180B13
		private PeerSecurityManager(PeerAuthenticationMode authMode, bool signing)
		{
			this.authenticationMode = authMode;
			this.enableSigning = signing;
			this.thisLock = new object();
		}

		// Token: 0x170018CB RID: 6347
		// (get) Token: 0x06006779 RID: 26489 RVA: 0x0018293F File Offset: 0x00180B3F
		public PeerAuthenticationMode AuthenticationMode
		{
			get
			{
				return this.authenticationMode;
			}
		}

		// Token: 0x170018CC RID: 6348
		// (get) Token: 0x0600677A RID: 26490 RVA: 0x00182947 File Offset: 0x00180B47
		public string Password
		{
			get
			{
				return this.password;
			}
		}

		// Token: 0x170018CD RID: 6349
		// (get) Token: 0x0600677B RID: 26491 RVA: 0x0018294F File Offset: 0x00180B4F
		public X509Certificate2 SelfCert
		{
			get
			{
				return this.credManager.Certificate;
			}
		}

		// Token: 0x170018CE RID: 6350
		// (get) Token: 0x0600677C RID: 26492 RVA: 0x0018295C File Offset: 0x00180B5C
		public bool MessageAuthentication
		{
			get
			{
				return this.enableSigning;
			}
		}

		// Token: 0x170018CF RID: 6351
		// (get) Token: 0x0600677D RID: 26493 RVA: 0x00182964 File Offset: 0x00180B64
		// (set) Token: 0x0600677E RID: 26494 RVA: 0x0018296C File Offset: 0x00180B6C
		internal string MeshId
		{
			get
			{
				return this.meshId;
			}
			set
			{
				this.meshId = value;
			}
		}

		// Token: 0x0600677F RID: 26495 RVA: 0x00182978 File Offset: 0x00180B78
		internal SelfSignedCertificate GetCertificate()
		{
			if (this.ssc == null)
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					if (this.ssc == null)
					{
						this.ssc = SelfSignedCertificate.Create("CN=" + Guid.NewGuid().ToString(), this.Password);
					}
				}
			}
			return this.ssc;
		}

		// Token: 0x170018D0 RID: 6352
		// (get) Token: 0x06006780 RID: 26496 RVA: 0x00182A00 File Offset: 0x00180C00
		private object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x06006781 RID: 26497 RVA: 0x00182A08 File Offset: 0x00180C08
		private static PeerSecurityCredentialsManager GetCredentialsManager(PeerAuthenticationMode mode, bool signing, BindingContext context)
		{
			if (mode == PeerAuthenticationMode.None && !signing)
			{
				return null;
			}
			ClientCredentials clientCredentials = context.BindingParameters.Find<ClientCredentials>();
			if (clientCredentials != null)
			{
				return new PeerSecurityCredentialsManager(clientCredentials.Peer, mode, signing);
			}
			ServiceCredentials serviceCredentials = context.BindingParameters.Find<ServiceCredentials>();
			if (serviceCredentials != null)
			{
				return new PeerSecurityCredentialsManager(serviceCredentials.Peer, mode, signing);
			}
			SecurityCredentialsManager securityCredentialsManager = context.BindingParameters.Find<SecurityCredentialsManager>();
			if (securityCredentialsManager == null)
			{
				PeerExceptionHelper.ThrowArgument_InsufficientCredentials(PeerPropertyNames.Credentials);
			}
			return new PeerSecurityCredentialsManager(securityCredentialsManager.CreateSecurityTokenManager(), mode, signing);
		}

		// Token: 0x06006782 RID: 26498 RVA: 0x00182A80 File Offset: 0x00180C80
		private static void Convert(PeerSecuritySettings security, out PeerAuthenticationMode authMode, out bool signing)
		{
			authMode = PeerAuthenticationMode.None;
			signing = false;
			if (security.Mode == SecurityMode.Transport || security.Mode == SecurityMode.TransportWithMessageCredential)
			{
				PeerTransportCredentialType credentialType = security.Transport.CredentialType;
				if (credentialType != PeerTransportCredentialType.Password)
				{
					if (credentialType == PeerTransportCredentialType.Certificate)
					{
						authMode = PeerAuthenticationMode.MutualCertificate;
					}
				}
				else
				{
					authMode = PeerAuthenticationMode.Password;
				}
			}
			if (security.Mode == SecurityMode.Message || security.Mode == SecurityMode.TransportWithMessageCredential)
			{
				signing = true;
			}
		}

		// Token: 0x06006783 RID: 26499 RVA: 0x00182AD8 File Offset: 0x00180CD8
		public static PeerSecurityManager Create(PeerSecuritySettings security, BindingContext context, XmlDictionaryReaderQuotas readerQuotas)
		{
			PeerAuthenticationMode peerAuthenticationMode = PeerAuthenticationMode.None;
			bool signMessages = false;
			PeerSecurityManager.Convert(security, out peerAuthenticationMode, out signMessages);
			return PeerSecurityManager.Create(peerAuthenticationMode, signMessages, context, readerQuotas);
		}

		// Token: 0x06006784 RID: 26500 RVA: 0x00182AFC File Offset: 0x00180CFC
		public static PeerSecurityManager Create(PeerAuthenticationMode authenticationMode, bool signMessages, BindingContext context, XmlDictionaryReaderQuotas readerQuotas)
		{
			if (authenticationMode == PeerAuthenticationMode.None && !signMessages)
			{
				return PeerSecurityManager.CreateDummy();
			}
			if (authenticationMode == PeerAuthenticationMode.Password)
			{
				try
				{
					using (new HMACSHA256())
					{
						using (new SHA256Managed())
						{
						}
					}
				}
				catch (InvalidOperationException ex)
				{
					DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
					PeerExceptionHelper.ThrowInvalidOperation_InsufficientCryptoSupport(ex);
				}
			}
			ChannelProtectionRequirements reqs = context.BindingParameters.Find<ChannelProtectionRequirements>();
			PeerSecurityCredentialsManager credentialsManager = PeerSecurityManager.GetCredentialsManager(authenticationMode, signMessages, context);
			if (credentialsManager.Credential != null)
			{
				PeerSecurityManager.ValidateCredentialSettings(authenticationMode, signMessages, credentialsManager.Credential);
			}
			PeerSecurityManager peerSecurityManager = PeerSecurityManager.Create(authenticationMode, signMessages, credentialsManager, reqs, readerQuotas);
			credentialsManager.Parent = peerSecurityManager;
			peerSecurityManager.ApplyAuditBehaviorSettings(context);
			return peerSecurityManager;
		}

		// Token: 0x06006785 RID: 26501 RVA: 0x00182BC4 File Offset: 0x00180DC4
		private static void ValidateCredentialSettings(PeerAuthenticationMode authenticationMode, bool signMessages, PeerCredential credential)
		{
			if (authenticationMode == PeerAuthenticationMode.None && !signMessages)
			{
				return;
			}
			X509CertificateValidator x509CertificateValidator;
			if (authenticationMode != PeerAuthenticationMode.Password)
			{
				if (authenticationMode == PeerAuthenticationMode.MutualCertificate)
				{
					if (credential.Certificate == null)
					{
						PeerExceptionHelper.ThrowArgument_InsufficientCredentials(PeerPropertyNames.Certificate);
					}
					if (!credential.PeerAuthentication.TryGetCertificateValidator(out x509CertificateValidator))
					{
						PeerExceptionHelper.ThrowArgument_InsufficientCredentials(PeerPropertyNames.PeerAuthentication);
					}
				}
			}
			else if (string.IsNullOrEmpty(credential.MeshPassword))
			{
				PeerExceptionHelper.ThrowArgument_InsufficientCredentials(PeerPropertyNames.Password);
			}
			if (signMessages && !credential.MessageSenderAuthentication.TryGetCertificateValidator(out x509CertificateValidator))
			{
				PeerExceptionHelper.ThrowArgument_InsufficientCredentials(PeerPropertyNames.MessageSenderAuthentication);
			}
		}

		// Token: 0x06006786 RID: 26502 RVA: 0x00182C44 File Offset: 0x00180E44
		private void ApplyAuditBehaviorSettings(BindingContext context)
		{
			ServiceSecurityAuditBehavior serviceSecurityAuditBehavior = context.BindingParameters.Find<ServiceSecurityAuditBehavior>();
			if (serviceSecurityAuditBehavior != null)
			{
				this.auditBehavior = serviceSecurityAuditBehavior.Clone();
				return;
			}
			this.auditBehavior = new ServiceSecurityAuditBehavior();
		}

		// Token: 0x06006787 RID: 26503 RVA: 0x00182C78 File Offset: 0x00180E78
		public void ApplyServiceSecurity(ServiceDescription description)
		{
			if (this.AuthenticationMode == PeerAuthenticationMode.None)
			{
				return;
			}
			description.Behaviors.Add(this.credManager.CloneForTransport());
		}

		// Token: 0x06006788 RID: 26504 RVA: 0x00182C9C File Offset: 0x00180E9C
		internal static PeerSecurityManager CreateDummy()
		{
			return new PeerSecurityManager(PeerAuthenticationMode.None, false);
		}

		// Token: 0x06006789 RID: 26505 RVA: 0x00182CB4 File Offset: 0x00180EB4
		public static PeerSecurityManager Create(PeerAuthenticationMode authenticationMode, bool messageAuthentication, PeerSecurityCredentialsManager credman, ChannelProtectionRequirements reqs, XmlDictionaryReaderQuotas readerQuotas)
		{
			X509CertificateValidator x509CertificateValidator = null;
			X509CertificateValidator x509CertificateValidator2 = null;
			PeerCredential credential = credman.Credential;
			if (credential == null && credman == null)
			{
				if (authenticationMode > PeerAuthenticationMode.None || messageAuthentication)
				{
					PeerExceptionHelper.ThrowArgument_InsufficientCredentials(PeerPropertyNames.Credentials);
				}
				return PeerSecurityManager.CreateDummy();
			}
			PeerSecurityManager peerSecurityManager = new PeerSecurityManager(authenticationMode, messageAuthentication);
			peerSecurityManager.credManager = credman;
			peerSecurityManager.password = credman.Password;
			peerSecurityManager.readerQuotas = readerQuotas;
			if (reqs != null)
			{
				peerSecurityManager.protection = new ChannelProtectionRequirements(reqs);
			}
			peerSecurityManager.tokenManager = credman.CreateSecurityTokenManager();
			if (credential == null)
			{
				return peerSecurityManager;
			}
			switch (authenticationMode)
			{
			case PeerAuthenticationMode.Password:
				peerSecurityManager.password = credential.MeshPassword;
				if (string.IsNullOrEmpty(peerSecurityManager.credManager.Password))
				{
					PeerExceptionHelper.ThrowArgument_InsufficientCredentials(PeerPropertyNames.Password);
				}
				x509CertificateValidator = X509CertificateValidator.None;
				break;
			case PeerAuthenticationMode.MutualCertificate:
				if (peerSecurityManager.credManager.Certificate == null)
				{
					PeerExceptionHelper.ThrowArgument_InsufficientCredentials(PeerPropertyNames.Certificate);
				}
				if (!credential.PeerAuthentication.TryGetCertificateValidator(out x509CertificateValidator))
				{
					PeerExceptionHelper.ThrowArgument_InsufficientCredentials(PeerPropertyNames.PeerAuthentication);
				}
				break;
			}
			if (messageAuthentication)
			{
				if (credential.MessageSenderAuthentication != null)
				{
					if (!credential.MessageSenderAuthentication.TryGetCertificateValidator(out x509CertificateValidator2))
					{
						PeerExceptionHelper.ThrowArgument_InsufficientCredentials(PeerPropertyNames.MessageSenderAuthentication);
					}
				}
				else
				{
					PeerExceptionHelper.ThrowArgument_InsufficientCredentials(PeerPropertyNames.MessageSenderAuthentication);
				}
			}
			return peerSecurityManager;
		}

		// Token: 0x0600678A RID: 26506 RVA: 0x00182DD4 File Offset: 0x00180FD4
		private void ApplySigningRequirements(ScopedMessagePartSpecification spec)
		{
			MessagePartSpecification parts = new MessagePartSpecification(new XmlQualifiedName[]
			{
				new XmlQualifiedName("PeerVia", "http://schemas.microsoft.com/net/2006/05/peer"),
				new XmlQualifiedName("FloodMessage", "http://schemas.microsoft.com/net/2006/05/peer"),
				new XmlQualifiedName("PeerTo", "http://schemas.microsoft.com/net/2006/05/peer"),
				new XmlQualifiedName("MessageID", "http://schemas.microsoft.com/net/2006/05/peer")
			});
			foreach (string action in spec.Actions)
			{
				spec.AddParts(parts, action);
			}
			spec.AddParts(parts, "*");
		}

		// Token: 0x0600678B RID: 26507 RVA: 0x00182E84 File Offset: 0x00181084
		public void Open()
		{
			this.CreateSecurityProtocolFactory();
		}

		// Token: 0x0600678C RID: 26508 RVA: 0x00182E8C File Offset: 0x0018108C
		private void CreateSecurityProtocolFactory()
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.securityProtocolFactory == null)
				{
					TimeoutHelper timeoutHelper = new TimeoutHelper(ServiceDefaults.SendTimeout);
					SecurityProtocolFactory forwardProtocolFactory;
					SecurityProtocolFactory reverseProtocolFactory;
					if (!this.enableSigning)
					{
						forwardProtocolFactory = new PeerDoNothingSecurityProtocolFactory();
						reverseProtocolFactory = new PeerDoNothingSecurityProtocolFactory();
					}
					else
					{
						X509Certificate2 certificate = this.credManager.Certificate;
						if (certificate != null)
						{
							SecurityBindingElement securityBindingElement = SecurityBindingElement.CreateCertificateSignatureBindingElement();
							securityBindingElement.ReaderQuotas = this.readerQuotas;
							BindingParameterCollection bindingParameterCollection = new BindingParameterCollection();
							ChannelProtectionRequirements channelProtectionRequirements;
							if (this.protection == null)
							{
								channelProtectionRequirements = new ChannelProtectionRequirements();
							}
							else
							{
								channelProtectionRequirements = new ChannelProtectionRequirements(this.protection);
							}
							this.ApplySigningRequirements(channelProtectionRequirements.IncomingSignatureParts);
							this.ApplySigningRequirements(channelProtectionRequirements.OutgoingSignatureParts);
							bindingParameterCollection.Add(channelProtectionRequirements);
							bindingParameterCollection.Add(this.auditBehavior);
							bindingParameterCollection.Add(this.credManager);
							BindingContext context = new BindingContext(new CustomBinding(new BindingElement[]
							{
								securityBindingElement
							}), bindingParameterCollection);
							forwardProtocolFactory = securityBindingElement.CreateSecurityProtocolFactory<IOutputChannel>(context, this.credManager, false, null);
						}
						else
						{
							forwardProtocolFactory = new PeerDoNothingSecurityProtocolFactory();
						}
						SecurityTokenResolver securityTokenResolver;
						X509SecurityTokenAuthenticator x509SecurityTokenAuthenticator = this.tokenManager.CreateSecurityTokenAuthenticator(PeerSecurityCredentialsManager.PeerClientSecurityTokenManager.CreateRequirement(SecurityTokenTypes.X509Certificate, true), out securityTokenResolver) as X509SecurityTokenAuthenticator;
						if (x509SecurityTokenAuthenticator != null)
						{
							SecurityBindingElement securityBindingElement2 = SecurityBindingElement.CreateCertificateSignatureBindingElement();
							securityBindingElement2.ReaderQuotas = this.readerQuotas;
							BindingParameterCollection bindingParameterCollection2 = new BindingParameterCollection();
							ChannelProtectionRequirements channelProtectionRequirements;
							if (this.protection == null)
							{
								channelProtectionRequirements = new ChannelProtectionRequirements();
							}
							else
							{
								channelProtectionRequirements = new ChannelProtectionRequirements(this.protection);
							}
							this.ApplySigningRequirements(channelProtectionRequirements.IncomingSignatureParts);
							this.ApplySigningRequirements(channelProtectionRequirements.OutgoingSignatureParts);
							bindingParameterCollection2.Add(channelProtectionRequirements);
							bindingParameterCollection2.Add(this.auditBehavior);
							bindingParameterCollection2.Add(this.credManager);
							BindingContext context2 = new BindingContext(new CustomBinding(new BindingElement[]
							{
								securityBindingElement2
							}), bindingParameterCollection2);
							reverseProtocolFactory = securityBindingElement2.CreateSecurityProtocolFactory<IOutputChannel>(context2, this.credManager, true, null);
						}
						else
						{
							reverseProtocolFactory = new PeerDoNothingSecurityProtocolFactory();
						}
					}
					DuplexSecurityProtocolFactory duplexSecurityProtocolFactory = new DuplexSecurityProtocolFactory(forwardProtocolFactory, reverseProtocolFactory);
					duplexSecurityProtocolFactory.Open(true, timeoutHelper.RemainingTime());
					this.securityProtocolFactory = duplexSecurityProtocolFactory;
				}
			}
		}

		// Token: 0x0600678D RID: 26509 RVA: 0x001830A8 File Offset: 0x001812A8
		public SecurityProtocolFactory GetProtocolFactory<TChannel>()
		{
			if (this.securityProtocolFactory == null)
			{
				this.CreateSecurityProtocolFactory();
			}
			if (typeof(TChannel) == typeof(IOutputChannel))
			{
				if (this.enableSigning && this.securityProtocolFactory.ForwardProtocolFactory is PeerDoNothingSecurityProtocolFactory)
				{
					PeerExceptionHelper.ThrowArgument_InsufficientCredentials(PeerPropertyNames.MessageSenderAuthentication);
				}
				return this.securityProtocolFactory.ForwardProtocolFactory;
			}
			if (typeof(TChannel) == typeof(IInputChannel))
			{
				if (this.enableSigning && this.securityProtocolFactory.ReverseProtocolFactory is PeerDoNothingSecurityProtocolFactory)
				{
					PeerExceptionHelper.ThrowArgument_InsufficientCredentials(PeerPropertyNames.MessageSenderAuthentication);
				}
				return this.securityProtocolFactory.ReverseProtocolFactory;
			}
			if (this.enableSigning && (this.securityProtocolFactory.ReverseProtocolFactory is PeerDoNothingSecurityProtocolFactory || this.securityProtocolFactory.ForwardProtocolFactory is PeerDoNothingSecurityProtocolFactory))
			{
				PeerExceptionHelper.ThrowArgument_InsufficientCredentials(PeerPropertyNames.MessageSenderAuthentication);
			}
			return this.securityProtocolFactory;
		}

		// Token: 0x0600678E RID: 26510 RVA: 0x00183198 File Offset: 0x00181398
		public SecurityProtocol CreateSecurityProtocol<TChannel>(EndpointAddress target, TimeSpan timespan)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timespan);
			SecurityProtocolFactory protocolFactory = this.GetProtocolFactory<TChannel>();
			SecurityProtocol securityProtocol = protocolFactory.CreateSecurityProtocol(target, null, null, false, timeoutHelper.RemainingTime());
			if (securityProtocol != null)
			{
				securityProtocol.Open(timeoutHelper.RemainingTime());
			}
			return securityProtocol;
		}

		// Token: 0x0600678F RID: 26511 RVA: 0x001831D8 File Offset: 0x001813D8
		public void CheckIfCompatibleNodeSettings(object other)
		{
			string text = null;
			PeerSecurityManager peerSecurityManager = other as PeerSecurityManager;
			if (peerSecurityManager == null)
			{
				text = PeerBindingPropertyNames.Security;
			}
			else if (this.authenticationMode != peerSecurityManager.authenticationMode)
			{
				text = PeerBindingPropertyNames.SecurityDotMode;
			}
			else
			{
				if (this.authenticationMode == PeerAuthenticationMode.None)
				{
					return;
				}
				if (!this.tokenManager.Equals(peerSecurityManager.tokenManager))
				{
					if (this.credManager != null)
					{
						this.credManager.CheckIfCompatible(peerSecurityManager.credManager);
					}
					else
					{
						text = PeerBindingPropertyNames.Credentials;
					}
				}
			}
			if (text != null)
			{
				PeerExceptionHelper.ThrowInvalidOperation_PeerConflictingPeerNodeSettings(text);
			}
		}

		// Token: 0x06006790 RID: 26512 RVA: 0x00183255 File Offset: 0x00181455
		public bool HasCompatibleMessageSecurity(PeerSecurityManager that)
		{
			return this.MessageAuthentication == that.MessageAuthentication;
		}

		// Token: 0x06006791 RID: 26513 RVA: 0x00183268 File Offset: 0x00181468
		public byte[] GetAuthenticator()
		{
			if (this.authenticationMode != PeerAuthenticationMode.Password)
			{
				return null;
			}
			if (this.authenticatorHash == null)
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					if (this.authenticatorHash == null)
					{
						this.authenticatorHash = PeerSecurityHelpers.ComputeHash(this.credManager.Certificate, this.credManager.Password);
					}
				}
			}
			return this.authenticatorHash;
		}

		// Token: 0x06006792 RID: 26514 RVA: 0x001832EC File Offset: 0x001814EC
		public bool Authenticate(ServiceSecurityContext context, byte[] message)
		{
			if (context == null)
			{
				return this.authenticationMode == PeerAuthenticationMode.None;
			}
			if (this.authenticationMode != PeerAuthenticationMode.Password)
			{
				if (message != null)
				{
					PeerExceptionHelper.ThrowInvalidOperation_UnexpectedSecurityTokensDuringHandshake();
				}
				return true;
			}
			if (context == null)
			{
				throw Fx.AssertAndThrow("No SecurityContext attached in security mode!");
			}
			Claim claim = PeerSecurityManager.FindClaim(context);
			return PeerSecurityHelpers.Authenticate(claim, this.credManager.Password, message);
		}

		// Token: 0x06006793 RID: 26515 RVA: 0x00183344 File Offset: 0x00181544
		public static Claim FindClaim(ServiceSecurityContext context)
		{
			Claim result = null;
			for (int i = 0; i < context.AuthorizationContext.ClaimSets.Count; i++)
			{
				ClaimSet claimSet = context.AuthorizationContext.ClaimSets[i];
				IEnumerator<Claim> enumerator = claimSet.FindClaims(ClaimTypes.Rsa, null).GetEnumerator();
				if (enumerator.MoveNext())
				{
					result = enumerator.Current;
					break;
				}
			}
			return result;
		}

		// Token: 0x06006794 RID: 26516 RVA: 0x001833A4 File Offset: 0x001815A4
		public void ApplyClientSecurity(ChannelFactory<IPeerProxy> factory)
		{
			factory.Endpoint.Behaviors.Remove<ClientCredentials>();
			if (this.authenticationMode != PeerAuthenticationMode.None)
			{
				factory.Endpoint.Behaviors.Add(this.credManager.CloneForTransport());
			}
		}

		// Token: 0x06006795 RID: 26517 RVA: 0x001833DC File Offset: 0x001815DC
		public BindingElement GetSecurityBindingElement()
		{
			SslStreamSecurityBindingElement sslStreamSecurityBindingElement = null;
			if (this.AuthenticationMode != PeerAuthenticationMode.None)
			{
				sslStreamSecurityBindingElement = new SslStreamSecurityBindingElement();
				sslStreamSecurityBindingElement.IdentityVerifier = new PeerIdentityVerifier();
				sslStreamSecurityBindingElement.RequireClientCertificate = true;
			}
			return sslStreamSecurityBindingElement;
		}

		// Token: 0x06006796 RID: 26518 RVA: 0x0018340C File Offset: 0x0018160C
		public PeerHashToken GetSelfToken()
		{
			if (this.authenticationMode != PeerAuthenticationMode.Password)
			{
				throw Fx.AssertAndThrow("unexpected call to GetSelfToken");
			}
			return new PeerHashToken(this.credManager.Certificate, this.credManager.Password);
		}

		// Token: 0x06006797 RID: 26519 RVA: 0x0018343D File Offset: 0x0018163D
		public PeerHashToken GetExpectedTokenForClaim(Claim claim)
		{
			return new PeerHashToken(claim, this.password);
		}

		// Token: 0x06006798 RID: 26520 RVA: 0x0018344C File Offset: 0x0018164C
		public void OnNeighborOpened(object sender, EventArgs args)
		{
			IPeerNeighbor peerNeighbor = sender as IPeerNeighbor;
			EventHandler onNeighborAuthenticated = this.OnNeighborAuthenticated;
			if (onNeighborAuthenticated == null)
			{
				peerNeighbor.Abort(PeerCloseReason.LeavingMesh, PeerCloseInitiator.LocalNode);
				return;
			}
			if (this.authenticationMode == PeerAuthenticationMode.Password)
			{
				if (peerNeighbor.Extensions.Find<PeerChannelAuthenticatorExtension>() != null)
				{
					throw Fx.AssertAndThrow("extension already exists!");
				}
				PeerChannelAuthenticatorExtension peerChannelAuthenticatorExtension = new PeerChannelAuthenticatorExtension(this, onNeighborAuthenticated, args, this.MeshId);
				peerNeighbor.Extensions.Add(peerChannelAuthenticatorExtension);
				if (peerNeighbor.IsInitiator)
				{
					peerChannelAuthenticatorExtension.InitiateHandShake();
					return;
				}
			}
			else
			{
				peerNeighbor.TrySetState(PeerNeighborState.Authenticated);
				onNeighborAuthenticated(sender, args);
			}
		}

		// Token: 0x06006799 RID: 26521 RVA: 0x001834D0 File Offset: 0x001816D0
		public Message ProcessRequest(IPeerNeighbor neighbor, Message request)
		{
			if (this.authenticationMode != PeerAuthenticationMode.Password || request == null)
			{
				this.Abort(neighbor);
				return null;
			}
			PeerChannelAuthenticatorExtension peerChannelAuthenticatorExtension = neighbor.Extensions.Find<PeerChannelAuthenticatorExtension>();
			Claim claim = PeerSecurityManager.FindClaim(ServiceSecurityContext.Current);
			if (peerChannelAuthenticatorExtension == null || claim == null)
			{
				throw Fx.AssertAndThrow("No suitable claim found in the context to do security negotiation!");
			}
			return peerChannelAuthenticatorExtension.ProcessRst(request, claim);
		}

		// Token: 0x0600679A RID: 26522 RVA: 0x00183522 File Offset: 0x00181722
		private void Abort(IPeerNeighbor neighbor)
		{
			neighbor.Abort(PeerCloseReason.AuthenticationFailure, PeerCloseInitiator.LocalNode);
		}

		// Token: 0x04003B72 RID: 15218
		private PeerAuthenticationMode authenticationMode;

		// Token: 0x04003B73 RID: 15219
		private bool enableSigning;

		// Token: 0x04003B74 RID: 15220
		internal string password;

		// Token: 0x04003B75 RID: 15221
		private DuplexSecurityProtocolFactory securityProtocolFactory;

		// Token: 0x04003B76 RID: 15222
		private volatile byte[] authenticatorHash;

		// Token: 0x04003B77 RID: 15223
		private object thisLock;

		// Token: 0x04003B78 RID: 15224
		public EventHandler OnNeighborAuthenticated;

		// Token: 0x04003B79 RID: 15225
		private string meshId = string.Empty;

		// Token: 0x04003B7A RID: 15226
		private ChannelProtectionRequirements protection;

		// Token: 0x04003B7B RID: 15227
		private PeerSecurityCredentialsManager credManager;

		// Token: 0x04003B7C RID: 15228
		private SecurityTokenManager tokenManager;

		// Token: 0x04003B7D RID: 15229
		private volatile SelfSignedCertificate ssc;

		// Token: 0x04003B7E RID: 15230
		private XmlDictionaryReaderQuotas readerQuotas;

		// Token: 0x04003B7F RID: 15231
		private ServiceSecurityAuditBehavior auditBehavior;
	}
}
