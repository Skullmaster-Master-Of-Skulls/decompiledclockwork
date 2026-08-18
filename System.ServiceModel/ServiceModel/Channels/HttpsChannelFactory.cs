using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Net;
using System.Net.Security;
using System.Runtime;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;
using System.Text;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200086F RID: 2159
	internal class HttpsChannelFactory<TChannel> : HttpChannelFactory<TChannel>
	{
		// Token: 0x060051A7 RID: 20903 RVA: 0x0012C4A4 File Offset: 0x0012A6A4
		internal HttpsChannelFactory(HttpsTransportBindingElement httpsBindingElement, BindingContext context) : base(httpsBindingElement, context)
		{
			this.requireClientCertificate = httpsBindingElement.RequireClientCertificate;
			this.channelBindingProvider = new ChannelBindingProviderHelper();
			ClientCredentials clientCredentials = context.BindingParameters.Find<ClientCredentials>();
			if (clientCredentials != null && clientCredentials.ServiceCertificate.SslCertificateAuthentication != null)
			{
				this.sslCertificateValidator = clientCredentials.ServiceCertificate.SslCertificateAuthentication.GetCertificateValidator();
				this.remoteCertificateValidationCallback = new RemoteCertificateValidationCallback(this.RemoteCertificateValidationCallback);
			}
		}

		// Token: 0x17001436 RID: 5174
		// (get) Token: 0x060051A8 RID: 20904 RVA: 0x0012C514 File Offset: 0x0012A714
		public override string Scheme
		{
			get
			{
				return Uri.UriSchemeHttps;
			}
		}

		// Token: 0x17001437 RID: 5175
		// (get) Token: 0x060051A9 RID: 20905 RVA: 0x0012C51B File Offset: 0x0012A71B
		public bool RequireClientCertificate
		{
			get
			{
				return this.requireClientCertificate;
			}
		}

		// Token: 0x17001438 RID: 5176
		// (get) Token: 0x060051AA RID: 20906 RVA: 0x0012C523 File Offset: 0x0012A723
		public override bool IsChannelBindingSupportEnabled
		{
			get
			{
				return this.channelBindingProvider.IsChannelBindingSupportEnabled;
			}
		}

		// Token: 0x060051AB RID: 20907 RVA: 0x0012C530 File Offset: 0x0012A730
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(IChannelBindingProvider))
			{
				return (T)((object)this.channelBindingProvider);
			}
			return base.GetProperty<T>();
		}

		// Token: 0x060051AC RID: 20908 RVA: 0x0012C560 File Offset: 0x0012A760
		internal override SecurityMessageProperty CreateReplySecurityProperty(HttpWebRequest request, HttpWebResponse response)
		{
			X509Certificate certificate = request.ServicePoint.Certificate;
			SecurityMessageProperty securityMessageProperty;
			if (certificate != null)
			{
				X509Certificate2 certificate2 = new X509Certificate2(certificate);
				SecurityToken token = new X509SecurityToken(certificate2, false);
				ReadOnlyCollection<IAuthorizationPolicy> readOnlyCollection = SecurityUtils.NonValidatingX509Authenticator.ValidateToken(token);
				securityMessageProperty = new SecurityMessageProperty();
				securityMessageProperty.TransportToken = new SecurityTokenSpecification(token, readOnlyCollection);
				securityMessageProperty.ServiceSecurityContext = new ServiceSecurityContext(readOnlyCollection);
			}
			else
			{
				securityMessageProperty = base.CreateReplySecurityProperty(request, response);
			}
			return securityMessageProperty;
		}

		// Token: 0x060051AD RID: 20909 RVA: 0x0012C5C8 File Offset: 0x0012A7C8
		protected override void ValidateCreateChannelParameters(EndpointAddress remoteAddress, Uri via)
		{
			if (remoteAddress.Identity != null)
			{
				X509CertificateEndpointIdentity x509CertificateEndpointIdentity = remoteAddress.Identity as X509CertificateEndpointIdentity;
				if (x509CertificateEndpointIdentity != null && x509CertificateEndpointIdentity.Certificates.Count > 1)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("remoteAddress", SR.GetString("HttpsIdentityMultipleCerts", new object[]
					{
						remoteAddress.Uri
					}));
				}
				EndpointIdentity identity = remoteAddress.Identity;
				bool flag = x509CertificateEndpointIdentity != null || ClaimTypes.Spn.Equals(identity.IdentityClaim.ClaimType) || ClaimTypes.Upn.Equals(identity.IdentityClaim.ClaimType) || ClaimTypes.Dns.Equals(identity.IdentityClaim.ClaimType);
				if (!HttpChannelFactory<TChannel>.IsWindowsAuth(base.AuthenticationScheme) && !flag)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("remoteAddress", SR.GetString("HttpsExplicitIdentity"));
				}
			}
			base.ValidateCreateChannelParameters(remoteAddress, via);
		}

		// Token: 0x060051AE RID: 20910 RVA: 0x0012C6AC File Offset: 0x0012A8AC
		protected override TChannel OnCreateChannelCore(EndpointAddress address, Uri via)
		{
			this.ValidateCreateChannelParameters(address, via);
			base.ValidateWebSocketTransportUsage();
			if (typeof(TChannel) == typeof(IRequestChannel))
			{
				return (TChannel)((object)new HttpsChannelFactory<TChannel>.HttpsRequestChannel((HttpsChannelFactory<IRequestChannel>)this, address, via, base.ManualAddressing));
			}
			return (TChannel)((object)new ClientWebSocketTransportDuplexSessionChannel((HttpChannelFactory<IDuplexSessionChannel>)this, base.ClientWebSocketFactory, address, via, base.WebSocketBufferPool));
		}

		// Token: 0x060051AF RID: 20911 RVA: 0x0012C719 File Offset: 0x0012A919
		protected override bool IsSecurityTokenManagerRequired()
		{
			return this.requireClientCertificate || base.IsSecurityTokenManagerRequired();
		}

		// Token: 0x060051B0 RID: 20912 RVA: 0x0012C72C File Offset: 0x0012A92C
		protected override string OnGetConnectionGroupPrefix(HttpWebRequest httpWebRequest, SecurityTokenContainer clientCertificateToken)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string arg = "\0";
			if (this.RequireClientCertificate)
			{
				HttpsChannelFactory<TChannel>.SetCertificate(httpWebRequest, clientCertificateToken);
				X509CertificateCollection clientCertificates = httpWebRequest.ClientCertificates;
				for (int i = 0; i < clientCertificates.Count; i++)
				{
					stringBuilder.AppendFormat("{0}{1}", clientCertificates[i].GetCertHashString(), arg);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060051B1 RID: 20913 RVA: 0x0012C78B File Offset: 0x0012A98B
		private void OnOpenCore()
		{
			if (this.requireClientCertificate && base.SecurityTokenManager == null)
			{
				throw Fx.AssertAndThrow("HttpsChannelFactory: SecurityTokenManager is null on open.");
			}
		}

		// Token: 0x060051B2 RID: 20914 RVA: 0x0012C7A8 File Offset: 0x0012A9A8
		protected override void OnEndOpen(IAsyncResult result)
		{
			base.OnEndOpen(result);
			this.OnOpenCore();
		}

		// Token: 0x060051B3 RID: 20915 RVA: 0x0012C7B7 File Offset: 0x0012A9B7
		protected override void OnOpen(TimeSpan timeout)
		{
			base.OnOpen(timeout);
			this.OnOpenCore();
		}

		// Token: 0x060051B4 RID: 20916 RVA: 0x0012C7C8 File Offset: 0x0012A9C8
		internal SecurityTokenProvider CreateAndOpenCertificateTokenProvider(EndpointAddress target, Uri via, ChannelParameterCollection channelParameters, TimeSpan timeout)
		{
			if (!this.RequireClientCertificate)
			{
				return null;
			}
			SecurityTokenProvider certificateTokenProvider = TransportSecurityHelpers.GetCertificateTokenProvider(base.SecurityTokenManager, target, via, this.Scheme, channelParameters);
			SecurityUtils.OpenTokenProviderIfRequired(certificateTokenProvider, timeout);
			return certificateTokenProvider;
		}

		// Token: 0x060051B5 RID: 20917 RVA: 0x0012C800 File Offset: 0x0012AA00
		private static void SetCertificate(HttpWebRequest request, SecurityTokenContainer clientCertificateToken)
		{
			if (clientCertificateToken != null)
			{
				X509SecurityToken x509SecurityToken = (X509SecurityToken)clientCertificateToken.Token;
				request.ClientCertificates.Add(x509SecurityToken.Certificate);
			}
		}

		// Token: 0x060051B6 RID: 20918 RVA: 0x0012C830 File Offset: 0x0012AA30
		internal SecurityTokenContainer GetCertificateSecurityToken(SecurityTokenProvider certificateProvider, EndpointAddress to, Uri via, ChannelParameterCollection channelParameters, ref TimeoutHelper timeoutHelper)
		{
			SecurityToken securityToken = null;
			SecurityTokenContainer result = null;
			SecurityTokenProvider securityTokenProvider;
			if (base.ManualAddressing && this.RequireClientCertificate)
			{
				securityTokenProvider = this.CreateAndOpenCertificateTokenProvider(to, via, channelParameters, timeoutHelper.RemainingTime());
			}
			else
			{
				securityTokenProvider = certificateProvider;
			}
			if (securityTokenProvider != null)
			{
				securityToken = securityTokenProvider.GetToken(timeoutHelper.RemainingTime());
			}
			if (base.ManualAddressing && this.RequireClientCertificate)
			{
				SecurityUtils.AbortTokenProviderIfRequired(securityTokenProvider);
			}
			if (securityToken != null)
			{
				result = new SecurityTokenContainer(securityToken);
			}
			return result;
		}

		// Token: 0x060051B7 RID: 20919 RVA: 0x0012C899 File Offset: 0x0012AA99
		private void AddServerCertMappingOrSetRemoteCertificateValidationCallback(HttpWebRequest request, EndpointAddress to)
		{
			if (this.sslCertificateValidator != null)
			{
				request.ServerCertificateValidationCallback = this.remoteCertificateValidationCallback;
				return;
			}
			HttpTransportSecurityHelpers.AddServerCertMapping(request, to);
		}

		// Token: 0x060051B8 RID: 20920 RVA: 0x0012C8B8 File Offset: 0x0012AAB8
		private bool RemoteCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			bool result;
			try
			{
				this.sslCertificateValidator.Validate(new X509Certificate2(certificate));
				result = true;
			}
			catch (SecurityTokenValidationException exception)
			{
				FxTrace.Exception.AsInformation(exception);
				result = false;
			}
			catch (Exception exception2)
			{
				if (Fx.IsFatal(exception2))
				{
					throw;
				}
				FxTrace.Exception.AsWarning(exception2);
				result = false;
			}
			return result;
		}

		// Token: 0x0400321B RID: 12827
		private bool requireClientCertificate;

		// Token: 0x0400321C RID: 12828
		private IChannelBindingProvider channelBindingProvider;

		// Token: 0x0400321D RID: 12829
		private RemoteCertificateValidationCallback remoteCertificateValidationCallback;

		// Token: 0x0400321E RID: 12830
		private X509CertificateValidator sslCertificateValidator;

		// Token: 0x02000D55 RID: 3413
		private class HttpsRequestChannel : HttpChannelFactory<TChannel>.HttpRequestChannel
		{
			// Token: 0x06007D15 RID: 32021 RVA: 0x001D3D40 File Offset: 0x001D1F40
			public HttpsRequestChannel(HttpsChannelFactory<IRequestChannel> factory, EndpointAddress to, Uri via, bool manualAddressing) : base(factory, to, via, manualAddressing)
			{
				this.factory = factory;
			}

			// Token: 0x17001BFD RID: 7165
			// (get) Token: 0x06007D16 RID: 32022 RVA: 0x001D3D54 File Offset: 0x001D1F54
			public new HttpsChannelFactory<IRequestChannel> Factory
			{
				get
				{
					return this.factory;
				}
			}

			// Token: 0x06007D17 RID: 32023 RVA: 0x001D3D5C File Offset: 0x001D1F5C
			private void CreateAndOpenTokenProvider(TimeSpan timeout)
			{
				if (!base.ManualAddressing && this.Factory.RequireClientCertificate)
				{
					this.certificateProvider = this.Factory.CreateAndOpenCertificateTokenProvider(base.RemoteAddress, base.Via, base.ChannelParameters, timeout);
				}
			}

			// Token: 0x06007D18 RID: 32024 RVA: 0x001D3D97 File Offset: 0x001D1F97
			private void CloseTokenProvider(TimeSpan timeout)
			{
				if (this.certificateProvider != null)
				{
					SecurityUtils.CloseTokenProviderIfRequired(this.certificateProvider, timeout);
				}
			}

			// Token: 0x06007D19 RID: 32025 RVA: 0x001D3DAD File Offset: 0x001D1FAD
			private void AbortTokenProvider()
			{
				if (this.certificateProvider != null)
				{
					SecurityUtils.AbortTokenProviderIfRequired(this.certificateProvider);
				}
			}

			// Token: 0x06007D1A RID: 32026 RVA: 0x001D3DC4 File Offset: 0x001D1FC4
			protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				this.CreateAndOpenTokenProvider(timeoutHelper.RemainingTime());
				return base.OnBeginOpen(timeoutHelper.RemainingTime(), callback, state);
			}

			// Token: 0x06007D1B RID: 32027 RVA: 0x001D3DF8 File Offset: 0x001D1FF8
			protected override void OnOpen(TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				this.CreateAndOpenTokenProvider(timeoutHelper.RemainingTime());
				base.OnOpen(timeoutHelper.RemainingTime());
			}

			// Token: 0x06007D1C RID: 32028 RVA: 0x001D3E27 File Offset: 0x001D2027
			protected override void OnAbort()
			{
				this.AbortTokenProvider();
				base.OnAbort();
			}

			// Token: 0x06007D1D RID: 32029 RVA: 0x001D3E38 File Offset: 0x001D2038
			protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				this.CloseTokenProvider(timeoutHelper.RemainingTime());
				return base.OnBeginClose(timeoutHelper.RemainingTime(), callback, state);
			}

			// Token: 0x06007D1E RID: 32030 RVA: 0x001D3E6C File Offset: 0x001D206C
			protected override void OnClose(TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				this.CloseTokenProvider(timeoutHelper.RemainingTime());
				base.OnClose(timeoutHelper.RemainingTime());
			}

			// Token: 0x06007D1F RID: 32031 RVA: 0x001D3E9B File Offset: 0x001D209B
			public IAsyncResult BeginBaseGetWebRequest(EndpointAddress to, Uri via, SecurityTokenContainer clientCertificateToken, ref TimeoutHelper timeoutHelper, AsyncCallback callback, object state)
			{
				return base.BeginGetWebRequest(to, via, clientCertificateToken, ref timeoutHelper, callback, state);
			}

			// Token: 0x06007D20 RID: 32032 RVA: 0x001D3EAC File Offset: 0x001D20AC
			public HttpWebRequest EndBaseGetWebRequest(IAsyncResult result)
			{
				return base.EndGetWebRequest(result);
			}

			// Token: 0x06007D21 RID: 32033 RVA: 0x001D3EB8 File Offset: 0x001D20B8
			public override HttpWebRequest GetWebRequest(EndpointAddress to, Uri via, ref TimeoutHelper timeoutHelper)
			{
				SecurityTokenContainer certificateSecurityToken = this.Factory.GetCertificateSecurityToken(this.certificateProvider, to, via, base.ChannelParameters, ref timeoutHelper);
				HttpWebRequest webRequest = base.GetWebRequest(to, via, certificateSecurityToken, ref timeoutHelper);
				this.factory.AddServerCertMappingOrSetRemoteCertificateValidationCallback(webRequest, to);
				return webRequest;
			}

			// Token: 0x06007D22 RID: 32034 RVA: 0x001D3EF9 File Offset: 0x001D20F9
			public override IAsyncResult BeginGetWebRequest(EndpointAddress to, Uri via, ref TimeoutHelper timeoutHelper, AsyncCallback callback, object state)
			{
				return new HttpsChannelFactory<TChannel>.HttpsRequestChannel.GetWebRequestAsyncResult(this, to, via, ref timeoutHelper, callback, state);
			}

			// Token: 0x06007D23 RID: 32035 RVA: 0x001D3F08 File Offset: 0x001D2108
			public override HttpWebRequest EndGetWebRequest(IAsyncResult result)
			{
				return HttpsChannelFactory<TChannel>.HttpsRequestChannel.GetWebRequestAsyncResult.End(result);
			}

			// Token: 0x06007D24 RID: 32036 RVA: 0x001D3F10 File Offset: 0x001D2110
			public override bool WillGetWebRequestCompleteSynchronously()
			{
				return base.WillGetWebRequestCompleteSynchronously() && this.certificateProvider == null && !this.Factory.ManualAddressing;
			}

			// Token: 0x06007D25 RID: 32037 RVA: 0x001D3F34 File Offset: 0x001D2134
			internal override void OnWebRequestCompleted(HttpWebRequest request)
			{
				HttpTransportSecurityHelpers.RemoveServerCertMapping(request);
			}

			// Token: 0x040047DA RID: 18394
			private SecurityTokenProvider certificateProvider;

			// Token: 0x040047DB RID: 18395
			private HttpsChannelFactory<IRequestChannel> factory;

			// Token: 0x02000F5E RID: 3934
			private class GetWebRequestAsyncResult : AsyncResult
			{
				// Token: 0x06008773 RID: 34675 RVA: 0x001F6C64 File Offset: 0x001F4E64
				public GetWebRequestAsyncResult(HttpsChannelFactory<TChannel>.HttpsRequestChannel httpsChannel, EndpointAddress to, Uri via, ref TimeoutHelper timeoutHelper, AsyncCallback callback, object state) : base(callback, state)
				{
					this.httpsChannel = httpsChannel;
					this.to = to;
					this.via = via;
					this.timeoutHelper = timeoutHelper;
					this.factory = httpsChannel.Factory;
					this.certificateProvider = httpsChannel.certificateProvider;
					if (this.factory.ManualAddressing && this.factory.RequireClientCertificate)
					{
						this.certificateProvider = this.factory.CreateAndOpenCertificateTokenProvider(to, via, httpsChannel.ChannelParameters, timeoutHelper.RemainingTime());
					}
					if (!this.GetToken())
					{
						return;
					}
					if (!this.GetWebRequest())
					{
						return;
					}
					base.Complete(true);
				}

				// Token: 0x06008774 RID: 34676 RVA: 0x001F6D08 File Offset: 0x001F4F08
				private bool GetWebRequest()
				{
					IAsyncResult asyncResult = this.httpsChannel.BeginBaseGetWebRequest(this.to, this.via, this.tokenContainer, ref this.timeoutHelper, HttpsChannelFactory<TChannel>.HttpsRequestChannel.GetWebRequestAsyncResult.onGetBaseWebRequestCallback, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return false;
					}
					this.request = this.httpsChannel.EndBaseGetWebRequest(asyncResult);
					this.factory.AddServerCertMappingOrSetRemoteCertificateValidationCallback(this.request, this.to);
					return true;
				}

				// Token: 0x06008775 RID: 34677 RVA: 0x001F6D74 File Offset: 0x001F4F74
				private bool GetToken()
				{
					if (this.certificateProvider != null)
					{
						if (HttpsChannelFactory<TChannel>.HttpsRequestChannel.GetWebRequestAsyncResult.onGetTokenCallback == null)
						{
							HttpsChannelFactory<TChannel>.HttpsRequestChannel.GetWebRequestAsyncResult.onGetTokenCallback = Fx.ThunkCallback(new AsyncCallback(HttpsChannelFactory<TChannel>.HttpsRequestChannel.GetWebRequestAsyncResult.OnGetTokenCallback));
						}
						IAsyncResult asyncResult = this.certificateProvider.BeginGetToken(this.timeoutHelper.RemainingTime(), HttpsChannelFactory<TChannel>.HttpsRequestChannel.GetWebRequestAsyncResult.onGetTokenCallback, this);
						if (!asyncResult.CompletedSynchronously)
						{
							return false;
						}
						this.OnGetToken(asyncResult);
					}
					return true;
				}

				// Token: 0x06008776 RID: 34678 RVA: 0x001F6DD8 File Offset: 0x001F4FD8
				private static void OnGetBaseWebRequestCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					HttpsChannelFactory<TChannel>.HttpsRequestChannel.GetWebRequestAsyncResult getWebRequestAsyncResult = (HttpsChannelFactory<TChannel>.HttpsRequestChannel.GetWebRequestAsyncResult)result.AsyncState;
					Exception exception = null;
					try
					{
						getWebRequestAsyncResult.request = getWebRequestAsyncResult.httpsChannel.EndBaseGetWebRequest(result);
						getWebRequestAsyncResult.factory.AddServerCertMappingOrSetRemoteCertificateValidationCallback(getWebRequestAsyncResult.request, getWebRequestAsyncResult.to);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						exception = ex;
					}
					getWebRequestAsyncResult.Complete(false, exception);
				}

				// Token: 0x06008777 RID: 34679 RVA: 0x001F6E50 File Offset: 0x001F5050
				private static void OnGetTokenCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					HttpsChannelFactory<TChannel>.HttpsRequestChannel.GetWebRequestAsyncResult getWebRequestAsyncResult = (HttpsChannelFactory<TChannel>.HttpsRequestChannel.GetWebRequestAsyncResult)result.AsyncState;
					Exception exception = null;
					bool flag;
					try
					{
						getWebRequestAsyncResult.OnGetToken(result);
						flag = getWebRequestAsyncResult.GetWebRequest();
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
						getWebRequestAsyncResult.Complete(false, exception);
					}
				}

				// Token: 0x06008778 RID: 34680 RVA: 0x001F6EB0 File Offset: 0x001F50B0
				private void OnGetToken(IAsyncResult result)
				{
					SecurityToken securityToken = this.certificateProvider.EndGetToken(result);
					if (securityToken != null)
					{
						this.tokenContainer = new SecurityTokenContainer(securityToken);
					}
					this.CloseCertificateProviderIfRequired();
				}

				// Token: 0x06008779 RID: 34681 RVA: 0x001F6EDF File Offset: 0x001F50DF
				private void CloseCertificateProviderIfRequired()
				{
					if (this.factory.ManualAddressing && this.certificateProvider != null)
					{
						SecurityUtils.AbortTokenProviderIfRequired(this.certificateProvider);
					}
				}

				// Token: 0x0600877A RID: 34682 RVA: 0x001F6F04 File Offset: 0x001F5104
				public static HttpWebRequest End(IAsyncResult result)
				{
					HttpsChannelFactory<TChannel>.HttpsRequestChannel.GetWebRequestAsyncResult getWebRequestAsyncResult = AsyncResult.End<HttpsChannelFactory<TChannel>.HttpsRequestChannel.GetWebRequestAsyncResult>(result);
					return getWebRequestAsyncResult.request;
				}

				// Token: 0x04004ED6 RID: 20182
				private SecurityTokenProvider certificateProvider;

				// Token: 0x04004ED7 RID: 20183
				private HttpsChannelFactory<IRequestChannel> factory;

				// Token: 0x04004ED8 RID: 20184
				private HttpsChannelFactory<TChannel>.HttpsRequestChannel httpsChannel;

				// Token: 0x04004ED9 RID: 20185
				private HttpWebRequest request;

				// Token: 0x04004EDA RID: 20186
				private EndpointAddress to;

				// Token: 0x04004EDB RID: 20187
				private Uri via;

				// Token: 0x04004EDC RID: 20188
				private TimeoutHelper timeoutHelper;

				// Token: 0x04004EDD RID: 20189
				private SecurityTokenContainer tokenContainer;

				// Token: 0x04004EDE RID: 20190
				private static AsyncCallback onGetBaseWebRequestCallback = Fx.ThunkCallback(new AsyncCallback(HttpsChannelFactory<TChannel>.HttpsRequestChannel.GetWebRequestAsyncResult.OnGetBaseWebRequestCallback));

				// Token: 0x04004EDF RID: 20191
				private static AsyncCallback onGetTokenCallback;
			}
		}
	}
}
