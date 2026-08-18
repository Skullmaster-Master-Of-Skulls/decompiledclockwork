using System;
using System.Collections.ObjectModel;
using System.IdentityModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Net;
using System.Runtime;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x0200030D RID: 781
	internal class TlsnegoTokenProvider : SspiNegotiationTokenProvider
	{
		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001AD0 RID: 6864 RVA: 0x0006475B File Offset: 0x0006295B
		// (set) Token: 0x06001AD1 RID: 6865 RVA: 0x00064763 File Offset: 0x00062963
		public SecurityTokenAuthenticator ServerTokenAuthenticator
		{
			get
			{
				return this.serverTokenAuthenticator;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.serverTokenAuthenticator = value;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001AD2 RID: 6866 RVA: 0x00064777 File Offset: 0x00062977
		// (set) Token: 0x06001AD3 RID: 6867 RVA: 0x0006477F File Offset: 0x0006297F
		public SecurityTokenProvider ClientTokenProvider
		{
			get
			{
				return this.clientTokenProvider;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.clientTokenProvider = value;
			}
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x00064794 File Offset: 0x00062994
		private static X509SecurityToken ValidateToken(SecurityToken token)
		{
			X509SecurityToken x509SecurityToken = token as X509SecurityToken;
			if (x509SecurityToken == null && token != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TokenProviderReturnedBadToken", new object[]
				{
					token.GetType().ToString()
				})));
			}
			return x509SecurityToken;
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x000647E0 File Offset: 0x000629E0
		private SspiNegotiationTokenProviderState CreateTlsSspiState(X509SecurityToken token)
		{
			X509Certificate2 clientCertificate;
			if (token == null)
			{
				clientCertificate = null;
			}
			else
			{
				clientCertificate = token.Certificate;
			}
			TlsSspiNegotiation sspiNegotiation;
			if (LocalAppContextSwitches.DisableUsingServicePointManagerSecurityProtocols)
			{
				sspiNegotiation = new TlsSspiNegotiation(string.Empty, (SchProtocols)160, clientCertificate);
			}
			else
			{
				SchProtocols protocolFlags = (SchProtocols)(ServicePointManager.SecurityProtocol & (SecurityProtocolType)(-2147472726));
				sspiNegotiation = new TlsSspiNegotiation(string.Empty, protocolFlags, clientCertificate);
			}
			return new SspiNegotiationTokenProviderState(sspiNegotiation);
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001AD6 RID: 6870 RVA: 0x00064838 File Offset: 0x00062A38
		public override XmlDictionaryString NegotiationValueType
		{
			get
			{
				if (base.StandardsManager.MessageSecurityVersion.TrustVersion == TrustVersion.WSTrustFeb2005)
				{
					return XD.TrustApr2004Dictionary.TlsnegoValueTypeUri;
				}
				if (base.StandardsManager.MessageSecurityVersion.TrustVersion == TrustVersion.WSTrust13)
				{
					return DXD.TrustDec2005Dictionary.TlsnegoValueTypeUri;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException());
			}
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x00064898 File Offset: 0x00062A98
		protected override bool CreateNegotiationStateCompletesSynchronously(EndpointAddress target, Uri via)
		{
			return this.ClientTokenProvider == null;
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x000648A5 File Offset: 0x00062AA5
		protected override IAsyncResult BeginCreateNegotiationState(EndpointAddress target, Uri via, TimeSpan timeout, AsyncCallback callback, object state)
		{
			base.EnsureEndpointAddressDoesNotRequireEncryption(target);
			if (this.ClientTokenProvider == null)
			{
				return new CompletedAsyncResult<SspiNegotiationTokenProviderState>(this.CreateTlsSspiState(null), callback, state);
			}
			return new TlsnegoTokenProvider.CreateSspiStateAsyncResult(target, via, this, timeout, callback, state);
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x000648D4 File Offset: 0x00062AD4
		protected override SspiNegotiationTokenProviderState EndCreateNegotiationState(IAsyncResult result)
		{
			if (result is CompletedAsyncResult<SspiNegotiationTokenProviderState>)
			{
				return CompletedAsyncResult<SspiNegotiationTokenProviderState>.End(result);
			}
			return TlsnegoTokenProvider.CreateSspiStateAsyncResult.End(result);
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x000648EC File Offset: 0x00062AEC
		protected override SspiNegotiationTokenProviderState CreateNegotiationState(EndpointAddress target, Uri via, TimeSpan timeout)
		{
			base.EnsureEndpointAddressDoesNotRequireEncryption(target);
			X509SecurityToken token;
			if (this.ClientTokenProvider == null)
			{
				token = null;
			}
			else
			{
				SecurityToken token2 = this.ClientTokenProvider.GetToken(timeout);
				token = TlsnegoTokenProvider.ValidateToken(token2);
			}
			return this.CreateTlsSspiState(token);
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x00064928 File Offset: 0x00062B28
		protected override ReadOnlyCollection<IAuthorizationPolicy> ValidateSspiNegotiation(ISspiNegotiation sspiNegotiation)
		{
			TlsSspiNegotiation tlsSspiNegotiation = (TlsSspiNegotiation)sspiNegotiation;
			if (!tlsSspiNegotiation.IsValidContext)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("InvalidSspiNegotiation")));
			}
			X509Certificate2 remoteCertificate = tlsSspiNegotiation.RemoteCertificate;
			if (remoteCertificate == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("ServerCertificateNotProvided")));
			}
			ReadOnlyCollection<IAuthorizationPolicy> result;
			if (this.ServerTokenAuthenticator != null)
			{
				X509SecurityToken token = new X509SecurityToken(remoteCertificate, false);
				result = this.ServerTokenAuthenticator.ValidateToken(token);
			}
			else
			{
				result = EmptyReadOnlyCollection<IAuthorizationPolicy>.Instance;
			}
			return result;
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x000649A8 File Offset: 0x00062BA8
		public override void OnOpen(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (this.ClientTokenProvider != null)
			{
				SecurityUtils.OpenTokenProviderIfRequired(this.ClientTokenProvider, timeoutHelper.RemainingTime());
			}
			if (this.ServerTokenAuthenticator != null)
			{
				SecurityUtils.OpenTokenAuthenticatorIfRequired(this.ServerTokenAuthenticator, timeoutHelper.RemainingTime());
			}
			base.OnOpen(timeoutHelper.RemainingTime());
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x00064A00 File Offset: 0x00062C00
		public override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (this.clientTokenProvider != null)
			{
				SecurityUtils.CloseTokenProviderIfRequired(this.ClientTokenProvider, timeoutHelper.RemainingTime());
				this.clientTokenProvider = null;
			}
			if (this.serverTokenAuthenticator != null)
			{
				SecurityUtils.CloseTokenAuthenticatorIfRequired(this.ServerTokenAuthenticator, timeoutHelper.RemainingTime());
				this.serverTokenAuthenticator = null;
			}
			base.OnClose(timeoutHelper.RemainingTime());
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x00064A64 File Offset: 0x00062C64
		public override void OnAbort()
		{
			if (this.clientTokenProvider != null)
			{
				SecurityUtils.AbortTokenProviderIfRequired(this.ClientTokenProvider);
				this.clientTokenProvider = null;
			}
			if (this.serverTokenAuthenticator != null)
			{
				SecurityUtils.AbortTokenAuthenticatorIfRequired(this.ServerTokenAuthenticator);
				this.serverTokenAuthenticator = null;
			}
			base.OnAbort();
		}

		// Token: 0x04001D39 RID: 7481
		private SecurityTokenAuthenticator serverTokenAuthenticator;

		// Token: 0x04001D3A RID: 7482
		private SecurityTokenProvider clientTokenProvider;

		// Token: 0x02000B6A RID: 2922
		private class CreateSspiStateAsyncResult : AsyncResult
		{
			// Token: 0x06007257 RID: 29271 RVA: 0x001AAEDC File Offset: 0x001A90DC
			public CreateSspiStateAsyncResult(EndpointAddress target, Uri via, TlsnegoTokenProvider tlsTokenProvider, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.tlsTokenProvider = tlsTokenProvider;
				IAsyncResult asyncResult = this.tlsTokenProvider.ClientTokenProvider.BeginGetToken(timeout, TlsnegoTokenProvider.CreateSspiStateAsyncResult.getTokensCallback, this);
				if (!asyncResult.CompletedSynchronously)
				{
					return;
				}
				SecurityToken token = this.tlsTokenProvider.ClientTokenProvider.EndGetToken(asyncResult);
				X509SecurityToken token2 = TlsnegoTokenProvider.ValidateToken(token);
				this.sspiState = this.tlsTokenProvider.CreateTlsSspiState(token2);
				base.Complete(true);
			}

			// Token: 0x06007258 RID: 29272 RVA: 0x001AAF50 File Offset: 0x001A9150
			private static void GetTokensCallback(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				TlsnegoTokenProvider.CreateSspiStateAsyncResult createSspiStateAsyncResult = (TlsnegoTokenProvider.CreateSspiStateAsyncResult)result.AsyncState;
				try
				{
					SecurityToken token = createSspiStateAsyncResult.tlsTokenProvider.ClientTokenProvider.EndGetToken(result);
					X509SecurityToken token2 = TlsnegoTokenProvider.ValidateToken(token);
					createSspiStateAsyncResult.sspiState = createSspiStateAsyncResult.tlsTokenProvider.CreateTlsSspiState(token2);
					createSspiStateAsyncResult.Complete(false);
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					createSspiStateAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x06007259 RID: 29273 RVA: 0x001AAFCC File Offset: 0x001A91CC
			public static SspiNegotiationTokenProviderState End(IAsyncResult result)
			{
				TlsnegoTokenProvider.CreateSspiStateAsyncResult createSspiStateAsyncResult = AsyncResult.End<TlsnegoTokenProvider.CreateSspiStateAsyncResult>(result);
				return createSspiStateAsyncResult.sspiState;
			}

			// Token: 0x040040B4 RID: 16564
			private static readonly AsyncCallback getTokensCallback = Fx.ThunkCallback(new AsyncCallback(TlsnegoTokenProvider.CreateSspiStateAsyncResult.GetTokensCallback));

			// Token: 0x040040B5 RID: 16565
			private TlsnegoTokenProvider tlsTokenProvider;

			// Token: 0x040040B6 RID: 16566
			private SspiNegotiationTokenProviderState sspiState;
		}
	}
}
