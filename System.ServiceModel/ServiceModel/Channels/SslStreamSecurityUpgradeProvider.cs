using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Runtime;
using System.Security.Authentication;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200082A RID: 2090
	internal class SslStreamSecurityUpgradeProvider : StreamSecurityUpgradeProvider, IStreamUpgradeChannelBindingProvider, IChannelBindingProvider
	{
		// Token: 0x06004E0B RID: 19979 RVA: 0x0011D2C1 File Offset: 0x0011B4C1
		private SslStreamSecurityUpgradeProvider(IDefaultCommunicationTimeouts timeouts, SecurityTokenManager clientSecurityTokenManager, bool requireClientCertificate, string scheme, IdentityVerifier identityVerifier, SslProtocols sslProtocols) : base(timeouts)
		{
			this.identityVerifier = identityVerifier;
			this.scheme = scheme;
			this.clientSecurityTokenManager = clientSecurityTokenManager;
			this.requireClientCertificate = requireClientCertificate;
			this.sslProtocols = sslProtocols;
		}

		// Token: 0x06004E0C RID: 19980 RVA: 0x0011D2F0 File Offset: 0x0011B4F0
		private SslStreamSecurityUpgradeProvider(IDefaultCommunicationTimeouts timeouts, SecurityTokenProvider serverTokenProvider, bool requireClientCertificate, SecurityTokenAuthenticator clientCertificateAuthenticator, string scheme, IdentityVerifier identityVerifier, SslProtocols sslProtocols) : base(timeouts)
		{
			this.serverTokenProvider = serverTokenProvider;
			this.requireClientCertificate = requireClientCertificate;
			this.clientCertificateAuthenticator = clientCertificateAuthenticator;
			this.identityVerifier = identityVerifier;
			this.scheme = scheme;
			this.sslProtocols = sslProtocols;
		}

		// Token: 0x06004E0D RID: 19981 RVA: 0x0011D328 File Offset: 0x0011B528
		public static SslStreamSecurityUpgradeProvider CreateClientProvider(SslStreamSecurityBindingElement bindingElement, BindingContext context)
		{
			SecurityCredentialsManager securityCredentialsManager = context.BindingParameters.Find<SecurityCredentialsManager>();
			if (securityCredentialsManager == null)
			{
				securityCredentialsManager = ClientCredentials.CreateDefaultCredentials();
			}
			SecurityTokenManager securityTokenManager = securityCredentialsManager.CreateSecurityTokenManager();
			return new SslStreamSecurityUpgradeProvider(context.Binding, securityTokenManager, bindingElement.RequireClientCertificate, context.Binding.Scheme, bindingElement.IdentityVerifier, bindingElement.SslProtocols);
		}

		// Token: 0x06004E0E RID: 19982 RVA: 0x0011D37C File Offset: 0x0011B57C
		public static SslStreamSecurityUpgradeProvider CreateServerProvider(SslStreamSecurityBindingElement bindingElement, BindingContext context)
		{
			SecurityCredentialsManager securityCredentialsManager = context.BindingParameters.Find<SecurityCredentialsManager>();
			if (securityCredentialsManager == null)
			{
				securityCredentialsManager = ServiceCredentials.CreateDefaultCredentials();
			}
			Uri listenUri = TransportSecurityHelpers.GetListenUri(context.ListenUriBaseAddress, context.ListenUriRelativeAddress);
			SecurityTokenManager securityTokenManager = securityCredentialsManager.CreateSecurityTokenManager();
			RecipientServiceModelSecurityTokenRequirement recipientServiceModelSecurityTokenRequirement = new RecipientServiceModelSecurityTokenRequirement();
			recipientServiceModelSecurityTokenRequirement.TokenType = SecurityTokenTypes.X509Certificate;
			recipientServiceModelSecurityTokenRequirement.RequireCryptographicToken = true;
			recipientServiceModelSecurityTokenRequirement.KeyUsage = SecurityKeyUsage.Exchange;
			recipientServiceModelSecurityTokenRequirement.TransportScheme = context.Binding.Scheme;
			recipientServiceModelSecurityTokenRequirement.ListenUri = listenUri;
			SecurityTokenProvider securityTokenProvider = securityTokenManager.CreateSecurityTokenProvider(recipientServiceModelSecurityTokenRequirement);
			if (securityTokenProvider == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ClientCredentialsUnableToCreateLocalTokenProvider", new object[]
				{
					recipientServiceModelSecurityTokenRequirement
				})));
			}
			SecurityTokenAuthenticator certificateTokenAuthenticator = TransportSecurityHelpers.GetCertificateTokenAuthenticator(securityTokenManager, context.Binding.Scheme, listenUri);
			return new SslStreamSecurityUpgradeProvider(context.Binding, securityTokenProvider, bindingElement.RequireClientCertificate, certificateTokenAuthenticator, context.Binding.Scheme, bindingElement.IdentityVerifier, bindingElement.SslProtocols);
		}

		// Token: 0x17001383 RID: 4995
		// (get) Token: 0x06004E0F RID: 19983 RVA: 0x0011D45F File Offset: 0x0011B65F
		public override EndpointIdentity Identity
		{
			get
			{
				if (this.identity == null && this.serverCertificate != null)
				{
					this.identity = SecurityUtils.GetServiceCertificateIdentity(this.serverCertificate);
				}
				return this.identity;
			}
		}

		// Token: 0x17001384 RID: 4996
		// (get) Token: 0x06004E10 RID: 19984 RVA: 0x0011D488 File Offset: 0x0011B688
		public IdentityVerifier IdentityVerifier
		{
			get
			{
				return this.identityVerifier;
			}
		}

		// Token: 0x17001385 RID: 4997
		// (get) Token: 0x06004E11 RID: 19985 RVA: 0x0011D490 File Offset: 0x0011B690
		public bool RequireClientCertificate
		{
			get
			{
				return this.requireClientCertificate;
			}
		}

		// Token: 0x17001386 RID: 4998
		// (get) Token: 0x06004E12 RID: 19986 RVA: 0x0011D498 File Offset: 0x0011B698
		public X509Certificate2 ServerCertificate
		{
			get
			{
				return this.serverCertificate;
			}
		}

		// Token: 0x17001387 RID: 4999
		// (get) Token: 0x06004E13 RID: 19987 RVA: 0x0011D4A0 File Offset: 0x0011B6A0
		public SecurityTokenAuthenticator ClientCertificateAuthenticator
		{
			get
			{
				if (this.clientCertificateAuthenticator == null)
				{
					this.clientCertificateAuthenticator = new X509SecurityTokenAuthenticator(X509ClientCertificateAuthentication.DefaultCertificateValidator);
				}
				return this.clientCertificateAuthenticator;
			}
		}

		// Token: 0x17001388 RID: 5000
		// (get) Token: 0x06004E14 RID: 19988 RVA: 0x0011D4C0 File Offset: 0x0011B6C0
		public SecurityTokenManager ClientSecurityTokenManager
		{
			get
			{
				return this.clientSecurityTokenManager;
			}
		}

		// Token: 0x17001389 RID: 5001
		// (get) Token: 0x06004E15 RID: 19989 RVA: 0x0011D4C8 File Offset: 0x0011B6C8
		public string Scheme
		{
			get
			{
				return this.scheme;
			}
		}

		// Token: 0x1700138A RID: 5002
		// (get) Token: 0x06004E16 RID: 19990 RVA: 0x0011D4D0 File Offset: 0x0011B6D0
		public SslProtocols SslProtocols
		{
			get
			{
				return this.sslProtocols;
			}
		}

		// Token: 0x06004E17 RID: 19991 RVA: 0x0011D4D8 File Offset: 0x0011B6D8
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(IChannelBindingProvider) || typeof(T) == typeof(IStreamUpgradeChannelBindingProvider))
			{
				return (T)((object)this);
			}
			return base.GetProperty<T>();
		}

		// Token: 0x06004E18 RID: 19992 RVA: 0x0011D528 File Offset: 0x0011B728
		ChannelBinding IStreamUpgradeChannelBindingProvider.GetChannelBinding(StreamUpgradeInitiator upgradeInitiator, ChannelBindingKind kind)
		{
			if (upgradeInitiator == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("upgradeInitiator");
			}
			SslStreamSecurityUpgradeInitiator sslStreamSecurityUpgradeInitiator = upgradeInitiator as SslStreamSecurityUpgradeInitiator;
			if (sslStreamSecurityUpgradeInitiator == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("upgradeInitiator", SR.GetString("UnsupportedUpgradeInitiator", new object[]
				{
					upgradeInitiator.GetType()
				}));
			}
			if (kind != ChannelBindingKind.Endpoint)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("kind", SR.GetString("StreamUpgradeUnsupportedChannelBindingKind", new object[]
				{
					base.GetType(),
					kind
				}));
			}
			return sslStreamSecurityUpgradeInitiator.ChannelBinding;
		}

		// Token: 0x06004E19 RID: 19993 RVA: 0x0011D5B8 File Offset: 0x0011B7B8
		ChannelBinding IStreamUpgradeChannelBindingProvider.GetChannelBinding(StreamUpgradeAcceptor upgradeAcceptor, ChannelBindingKind kind)
		{
			if (upgradeAcceptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("upgradeAcceptor");
			}
			SslStreamSecurityUpgradeAcceptor sslStreamSecurityUpgradeAcceptor = upgradeAcceptor as SslStreamSecurityUpgradeAcceptor;
			if (sslStreamSecurityUpgradeAcceptor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("upgradeAcceptor", SR.GetString("UnsupportedUpgradeAcceptor", new object[]
				{
					upgradeAcceptor.GetType()
				}));
			}
			if (kind != ChannelBindingKind.Endpoint)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("kind", SR.GetString("StreamUpgradeUnsupportedChannelBindingKind", new object[]
				{
					base.GetType(),
					kind
				}));
			}
			return sslStreamSecurityUpgradeAcceptor.ChannelBinding;
		}

		// Token: 0x06004E1A RID: 19994 RVA: 0x0011D648 File Offset: 0x0011B848
		void IChannelBindingProvider.EnableChannelBindingSupport()
		{
			this.enableChannelBinding = true;
		}

		// Token: 0x1700138B RID: 5003
		// (get) Token: 0x06004E1B RID: 19995 RVA: 0x0011D651 File Offset: 0x0011B851
		bool IChannelBindingProvider.IsChannelBindingSupportEnabled
		{
			get
			{
				return this.enableChannelBinding;
			}
		}

		// Token: 0x06004E1C RID: 19996 RVA: 0x0011D659 File Offset: 0x0011B859
		public override StreamUpgradeAcceptor CreateUpgradeAcceptor()
		{
			base.ThrowIfDisposedOrNotOpen();
			return new SslStreamSecurityUpgradeAcceptor(this);
		}

		// Token: 0x06004E1D RID: 19997 RVA: 0x0011D667 File Offset: 0x0011B867
		public override StreamUpgradeInitiator CreateUpgradeInitiator(EndpointAddress remoteAddress, Uri via)
		{
			base.ThrowIfDisposedOrNotOpen();
			return new SslStreamSecurityUpgradeInitiator(this, remoteAddress, via);
		}

		// Token: 0x06004E1E RID: 19998 RVA: 0x0011D677 File Offset: 0x0011B877
		protected override void OnAbort()
		{
			if (this.clientCertificateAuthenticator != null)
			{
				SecurityUtils.AbortTokenAuthenticatorIfRequired(this.clientCertificateAuthenticator);
			}
			this.CleanupServerCertificate();
		}

		// Token: 0x06004E1F RID: 19999 RVA: 0x0011D692 File Offset: 0x0011B892
		protected override void OnClose(TimeSpan timeout)
		{
			if (this.clientCertificateAuthenticator != null)
			{
				SecurityUtils.CloseTokenAuthenticatorIfRequired(this.clientCertificateAuthenticator, timeout);
			}
			this.CleanupServerCertificate();
		}

		// Token: 0x06004E20 RID: 20000 RVA: 0x0011D6AE File Offset: 0x0011B8AE
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return SecurityUtils.BeginCloseTokenAuthenticatorIfRequired(this.clientCertificateAuthenticator, timeout, callback, state);
		}

		// Token: 0x06004E21 RID: 20001 RVA: 0x0011D6BE File Offset: 0x0011B8BE
		protected override void OnEndClose(IAsyncResult result)
		{
			SecurityUtils.EndCloseTokenAuthenticatorIfRequired(result);
			this.CleanupServerCertificate();
		}

		// Token: 0x06004E22 RID: 20002 RVA: 0x0011D6CC File Offset: 0x0011B8CC
		private void SetupServerCertificate(SecurityToken token)
		{
			X509SecurityToken x509SecurityToken = token as X509SecurityToken;
			if (x509SecurityToken == null)
			{
				SecurityUtils.AbortTokenProviderIfRequired(this.serverTokenProvider);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("InvalidTokenProvided", new object[]
				{
					this.serverTokenProvider.GetType(),
					typeof(X509SecurityToken)
				})));
			}
			this.serverCertificate = new X509Certificate2(x509SecurityToken.Certificate);
		}

		// Token: 0x06004E23 RID: 20003 RVA: 0x0011D73A File Offset: 0x0011B93A
		private void CleanupServerCertificate()
		{
			if (!ServiceModelAppSettings.DeferSslStreamServerCertificateCleanup && this.serverCertificate != null)
			{
				SecurityUtils.ResetCertificate(this.serverCertificate);
				this.serverCertificate = null;
			}
		}

		// Token: 0x06004E24 RID: 20004 RVA: 0x0011D760 File Offset: 0x0011B960
		protected override void OnOpen(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			SecurityUtils.OpenTokenAuthenticatorIfRequired(this.ClientCertificateAuthenticator, timeoutHelper.RemainingTime());
			if (this.serverTokenProvider != null)
			{
				SecurityUtils.OpenTokenProviderIfRequired(this.serverTokenProvider, timeoutHelper.RemainingTime());
				SecurityToken token = this.serverTokenProvider.GetToken(timeout);
				this.SetupServerCertificate(token);
				SecurityUtils.CloseTokenProviderIfRequired(this.serverTokenProvider, timeoutHelper.RemainingTime());
				this.serverTokenProvider = null;
			}
		}

		// Token: 0x06004E25 RID: 20005 RVA: 0x0011D7CE File Offset: 0x0011B9CE
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new SslStreamSecurityUpgradeProvider.OpenAsyncResult(this, timeout, callback, state);
		}

		// Token: 0x06004E26 RID: 20006 RVA: 0x0011D7D9 File Offset: 0x0011B9D9
		protected override void OnEndOpen(IAsyncResult result)
		{
			SslStreamSecurityUpgradeProvider.OpenAsyncResult.End(result);
		}

		// Token: 0x040030C6 RID: 12486
		private SecurityTokenAuthenticator clientCertificateAuthenticator;

		// Token: 0x040030C7 RID: 12487
		private SecurityTokenManager clientSecurityTokenManager;

		// Token: 0x040030C8 RID: 12488
		private SecurityTokenProvider serverTokenProvider;

		// Token: 0x040030C9 RID: 12489
		private EndpointIdentity identity;

		// Token: 0x040030CA RID: 12490
		private IdentityVerifier identityVerifier;

		// Token: 0x040030CB RID: 12491
		private X509Certificate2 serverCertificate;

		// Token: 0x040030CC RID: 12492
		private bool requireClientCertificate;

		// Token: 0x040030CD RID: 12493
		private string scheme;

		// Token: 0x040030CE RID: 12494
		private bool enableChannelBinding;

		// Token: 0x040030CF RID: 12495
		private SslProtocols sslProtocols;

		// Token: 0x02000D28 RID: 3368
		private class OpenAsyncResult : AsyncResult
		{
			// Token: 0x06007BDB RID: 31707 RVA: 0x001CEB0C File Offset: 0x001CCD0C
			public OpenAsyncResult(SslStreamSecurityUpgradeProvider parent, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.parent = parent;
				this.timeoutHelper = new TimeoutHelper(timeout);
				this.onOpenTokenAuthenticator = Fx.ThunkCallback(new AsyncCallback(this.OnOpenTokenAuthenticator));
				IAsyncResult asyncResult = SecurityUtils.BeginOpenTokenAuthenticatorIfRequired(parent.ClientCertificateAuthenticator, this.timeoutHelper.RemainingTime(), this.onOpenTokenAuthenticator, this);
				if (!asyncResult.CompletedSynchronously)
				{
					return;
				}
				if (this.HandleOpenAuthenticatorComplete(asyncResult))
				{
					base.Complete(true);
				}
			}

			// Token: 0x06007BDC RID: 31708 RVA: 0x001CEB83 File Offset: 0x001CCD83
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<SslStreamSecurityUpgradeProvider.OpenAsyncResult>(result);
			}

			// Token: 0x06007BDD RID: 31709 RVA: 0x001CEB8C File Offset: 0x001CCD8C
			private bool HandleOpenAuthenticatorComplete(IAsyncResult result)
			{
				SecurityUtils.EndOpenTokenAuthenticatorIfRequired(result);
				if (this.parent.serverTokenProvider == null)
				{
					return true;
				}
				this.onOpenTokenProvider = Fx.ThunkCallback(new AsyncCallback(this.OnOpenTokenProvider));
				IAsyncResult asyncResult = SecurityUtils.BeginOpenTokenProviderIfRequired(this.parent.serverTokenProvider, this.timeoutHelper.RemainingTime(), this.onOpenTokenProvider, this);
				return asyncResult.CompletedSynchronously && this.HandleOpenTokenProviderComplete(asyncResult);
			}

			// Token: 0x06007BDE RID: 31710 RVA: 0x001CEBFC File Offset: 0x001CCDFC
			private bool HandleOpenTokenProviderComplete(IAsyncResult result)
			{
				SecurityUtils.EndOpenTokenProviderIfRequired(result);
				this.onGetToken = Fx.ThunkCallback(new AsyncCallback(this.OnGetToken));
				IAsyncResult asyncResult = this.parent.serverTokenProvider.BeginGetToken(this.timeoutHelper.RemainingTime(), this.onGetToken, this);
				return asyncResult.CompletedSynchronously && this.HandleGetTokenComplete(asyncResult);
			}

			// Token: 0x06007BDF RID: 31711 RVA: 0x001CEC5C File Offset: 0x001CCE5C
			private bool HandleGetTokenComplete(IAsyncResult result)
			{
				SecurityToken token = this.parent.serverTokenProvider.EndGetToken(result);
				this.parent.SetupServerCertificate(token);
				this.onCloseTokenProvider = Fx.ThunkCallback(new AsyncCallback(this.OnCloseTokenProvider));
				IAsyncResult asyncResult = SecurityUtils.BeginCloseTokenProviderIfRequired(this.parent.serverTokenProvider, this.timeoutHelper.RemainingTime(), this.onCloseTokenProvider, this);
				return asyncResult.CompletedSynchronously && this.HandleCloseTokenProviderComplete(asyncResult);
			}

			// Token: 0x06007BE0 RID: 31712 RVA: 0x001CECD2 File Offset: 0x001CCED2
			private bool HandleCloseTokenProviderComplete(IAsyncResult result)
			{
				SecurityUtils.EndCloseTokenProviderIfRequired(result);
				this.parent.serverTokenProvider = null;
				return true;
			}

			// Token: 0x06007BE1 RID: 31713 RVA: 0x001CECE8 File Offset: 0x001CCEE8
			private void OnOpenTokenAuthenticator(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				Exception exception = null;
				bool flag = false;
				try
				{
					flag = this.HandleOpenAuthenticatorComplete(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag = true;
					exception = ex;
				}
				if (flag)
				{
					base.Complete(false, exception);
				}
			}

			// Token: 0x06007BE2 RID: 31714 RVA: 0x001CED38 File Offset: 0x001CCF38
			private void OnOpenTokenProvider(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				Exception exception = null;
				bool flag = false;
				try
				{
					flag = this.HandleOpenTokenProviderComplete(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag = true;
					exception = ex;
				}
				if (flag)
				{
					base.Complete(false, exception);
				}
			}

			// Token: 0x06007BE3 RID: 31715 RVA: 0x001CED88 File Offset: 0x001CCF88
			private void OnGetToken(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				Exception exception = null;
				bool flag = false;
				try
				{
					flag = this.HandleGetTokenComplete(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag = true;
					exception = ex;
				}
				if (flag)
				{
					base.Complete(false, exception);
				}
			}

			// Token: 0x06007BE4 RID: 31716 RVA: 0x001CEDD8 File Offset: 0x001CCFD8
			private void OnCloseTokenProvider(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				Exception exception = null;
				bool flag = false;
				try
				{
					flag = this.HandleCloseTokenProviderComplete(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag = true;
					exception = ex;
				}
				if (flag)
				{
					base.Complete(false, exception);
				}
			}

			// Token: 0x0400471B RID: 18203
			private SslStreamSecurityUpgradeProvider parent;

			// Token: 0x0400471C RID: 18204
			private TimeoutHelper timeoutHelper;

			// Token: 0x0400471D RID: 18205
			private AsyncCallback onOpenTokenAuthenticator;

			// Token: 0x0400471E RID: 18206
			private AsyncCallback onOpenTokenProvider;

			// Token: 0x0400471F RID: 18207
			private AsyncCallback onGetToken;

			// Token: 0x04004720 RID: 18208
			private AsyncCallback onCloseTokenProvider;
		}
	}
}
