using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Net;
using System.Net.Security;
using System.Runtime;
using System.Security.Principal;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000839 RID: 2105
	internal static class TransportSecurityHelpers
	{
		// Token: 0x06004E95 RID: 20117 RVA: 0x0011E95C File Offset: 0x0011CB5C
		public static IAsyncResult BeginGetSspiCredential(SecurityTokenProviderContainer tokenProvider, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new TransportSecurityHelpers.GetSspiCredentialAsyncResult(tokenProvider.TokenProvider as SspiSecurityTokenProvider, timeout, callback, state);
		}

		// Token: 0x06004E96 RID: 20118 RVA: 0x0011E971 File Offset: 0x0011CB71
		public static IAsyncResult BeginGetSspiCredential(SecurityTokenProvider tokenProvider, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new TransportSecurityHelpers.GetSspiCredentialAsyncResult((SspiSecurityTokenProvider)tokenProvider, timeout, callback, state);
		}

		// Token: 0x06004E97 RID: 20119 RVA: 0x0011E981 File Offset: 0x0011CB81
		public static NetworkCredential EndGetSspiCredential(IAsyncResult result, out TokenImpersonationLevel impersonationLevel, out AuthenticationLevel authenticationLevel)
		{
			return TransportSecurityHelpers.GetSspiCredentialAsyncResult.End(result, out impersonationLevel, out authenticationLevel);
		}

		// Token: 0x06004E98 RID: 20120 RVA: 0x0011E98B File Offset: 0x0011CB8B
		public static NetworkCredential EndGetSspiCredential(IAsyncResult result, out TokenImpersonationLevel impersonationLevel, out bool allowNtlm)
		{
			return TransportSecurityHelpers.GetSspiCredentialAsyncResult.End(result, out impersonationLevel, out allowNtlm);
		}

		// Token: 0x06004E99 RID: 20121 RVA: 0x0011E998 File Offset: 0x0011CB98
		public static NetworkCredential GetSspiCredential(SecurityTokenProviderContainer tokenProvider, TimeSpan timeout, out TokenImpersonationLevel impersonationLevel, out AuthenticationLevel authenticationLevel)
		{
			bool flag;
			bool flag2;
			NetworkCredential sspiCredential = TransportSecurityHelpers.GetSspiCredential(tokenProvider.TokenProvider as SspiSecurityTokenProvider, timeout, out flag, out impersonationLevel, out flag2);
			authenticationLevel = (flag2 ? AuthenticationLevel.MutualAuthRequested : AuthenticationLevel.MutualAuthRequired);
			return sspiCredential;
		}

		// Token: 0x06004E9A RID: 20122 RVA: 0x0011E9C8 File Offset: 0x0011CBC8
		public static NetworkCredential GetSspiCredential(SspiSecurityTokenProvider tokenProvider, TimeSpan timeout, out TokenImpersonationLevel impersonationLevel, out bool allowNtlm)
		{
			bool flag;
			return TransportSecurityHelpers.GetSspiCredential(tokenProvider, timeout, out flag, out impersonationLevel, out allowNtlm);
		}

		// Token: 0x06004E9B RID: 20123 RVA: 0x0011E9E0 File Offset: 0x0011CBE0
		public static NetworkCredential GetSspiCredential(SecurityTokenManager credentialProvider, SecurityTokenRequirement sspiTokenRequirement, TimeSpan timeout, out bool extractGroupsForWindowsAccounts)
		{
			extractGroupsForWindowsAccounts = true;
			NetworkCredential result = null;
			if (credentialProvider != null)
			{
				SecurityTokenProvider securityTokenProvider = credentialProvider.CreateSecurityTokenProvider(sspiTokenRequirement);
				if (securityTokenProvider != null)
				{
					TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
					SecurityUtils.OpenTokenProviderIfRequired(securityTokenProvider, timeoutHelper.RemainingTime());
					bool flag = false;
					try
					{
						TokenImpersonationLevel tokenImpersonationLevel;
						bool flag2;
						result = TransportSecurityHelpers.GetSspiCredential((SspiSecurityTokenProvider)securityTokenProvider, timeoutHelper.RemainingTime(), out extractGroupsForWindowsAccounts, out tokenImpersonationLevel, out flag2);
						flag = true;
					}
					finally
					{
						if (!flag)
						{
							SecurityUtils.AbortTokenProviderIfRequired(securityTokenProvider);
						}
					}
					SecurityUtils.CloseTokenProviderIfRequired(securityTokenProvider, timeoutHelper.RemainingTime());
				}
			}
			return result;
		}

		// Token: 0x06004E9C RID: 20124 RVA: 0x0011EA5C File Offset: 0x0011CC5C
		private static NetworkCredential GetSspiCredential(SspiSecurityTokenProvider tokenProvider, TimeSpan timeout, out bool extractGroupsForWindowsAccounts, out TokenImpersonationLevel impersonationLevel, out bool allowNtlm)
		{
			NetworkCredential networkCredential = null;
			extractGroupsForWindowsAccounts = true;
			impersonationLevel = TokenImpersonationLevel.Identification;
			allowNtlm = true;
			if (tokenProvider != null)
			{
				SspiSecurityToken token = TransportSecurityHelpers.GetToken<SspiSecurityToken>(tokenProvider, timeout);
				if (token != null)
				{
					extractGroupsForWindowsAccounts = token.ExtractGroupsForWindowsAccounts;
					impersonationLevel = token.ImpersonationLevel;
					allowNtlm = token.AllowNtlm;
					if (token.NetworkCredential != null)
					{
						networkCredential = token.NetworkCredential;
						SecurityUtils.FixNetworkCredential(ref networkCredential);
					}
				}
			}
			if (networkCredential == null)
			{
				networkCredential = CredentialCache.DefaultNetworkCredentials;
			}
			return networkCredential;
		}

		// Token: 0x06004E9D RID: 20125 RVA: 0x0011EABC File Offset: 0x0011CCBC
		public static SecurityTokenRequirement CreateSspiTokenRequirement(string transportScheme, Uri listenUri)
		{
			return new RecipientServiceModelSecurityTokenRequirement
			{
				TransportScheme = transportScheme,
				RequireCryptographicToken = false,
				ListenUri = listenUri,
				TokenType = ServiceModelSecurityTokenTypes.SspiCredential
			};
		}

		// Token: 0x06004E9E RID: 20126 RVA: 0x0011EAF0 File Offset: 0x0011CCF0
		private static SecurityTokenRequirement CreateSspiTokenRequirement(EndpointAddress target, Uri via, string transportScheme)
		{
			return new InitiatorServiceModelSecurityTokenRequirement
			{
				TokenType = ServiceModelSecurityTokenTypes.SspiCredential,
				RequireCryptographicToken = false,
				TransportScheme = transportScheme,
				TargetAddress = target,
				Via = via
			};
		}

		// Token: 0x06004E9F RID: 20127 RVA: 0x0011EB2C File Offset: 0x0011CD2C
		public static SspiSecurityTokenProvider GetSspiTokenProvider(SecurityTokenManager tokenManager, EndpointAddress target, Uri via, string transportScheme, AuthenticationSchemes authenticationScheme, ChannelParameterCollection channelParameters)
		{
			if (tokenManager != null)
			{
				SecurityTokenRequirement securityTokenRequirement = TransportSecurityHelpers.CreateSspiTokenRequirement(target, via, transportScheme);
				securityTokenRequirement.Properties[ServiceModelSecurityTokenRequirement.HttpAuthenticationSchemeProperty] = authenticationScheme;
				if (channelParameters != null)
				{
					securityTokenRequirement.Properties[ServiceModelSecurityTokenRequirement.ChannelParametersCollectionProperty] = channelParameters;
				}
				return tokenManager.CreateSecurityTokenProvider(securityTokenRequirement) as SspiSecurityTokenProvider;
			}
			return null;
		}

		// Token: 0x06004EA0 RID: 20128 RVA: 0x0011EB84 File Offset: 0x0011CD84
		public static SspiSecurityTokenProvider GetSspiTokenProvider(SecurityTokenManager tokenManager, EndpointAddress target, Uri via, string transportScheme, out IdentityVerifier identityVerifier)
		{
			identityVerifier = null;
			if (tokenManager != null)
			{
				SspiSecurityTokenProvider sspiSecurityTokenProvider = tokenManager.CreateSecurityTokenProvider(TransportSecurityHelpers.CreateSspiTokenRequirement(target, via, transportScheme)) as SspiSecurityTokenProvider;
				if (sspiSecurityTokenProvider != null)
				{
					identityVerifier = IdentityVerifier.CreateDefault();
				}
				return sspiSecurityTokenProvider;
			}
			return null;
		}

		// Token: 0x06004EA1 RID: 20129 RVA: 0x0011EBBC File Offset: 0x0011CDBC
		public static SecurityTokenProvider GetDigestTokenProvider(SecurityTokenManager tokenManager, EndpointAddress target, Uri via, string transportScheme, AuthenticationSchemes authenticationScheme, ChannelParameterCollection channelParameters)
		{
			if (tokenManager != null)
			{
				InitiatorServiceModelSecurityTokenRequirement initiatorServiceModelSecurityTokenRequirement = new InitiatorServiceModelSecurityTokenRequirement();
				initiatorServiceModelSecurityTokenRequirement.TokenType = ServiceModelSecurityTokenTypes.SspiCredential;
				initiatorServiceModelSecurityTokenRequirement.TargetAddress = target;
				initiatorServiceModelSecurityTokenRequirement.Via = via;
				initiatorServiceModelSecurityTokenRequirement.RequireCryptographicToken = false;
				initiatorServiceModelSecurityTokenRequirement.TransportScheme = transportScheme;
				initiatorServiceModelSecurityTokenRequirement.Properties[ServiceModelSecurityTokenRequirement.HttpAuthenticationSchemeProperty] = authenticationScheme;
				if (channelParameters != null)
				{
					initiatorServiceModelSecurityTokenRequirement.Properties[ServiceModelSecurityTokenRequirement.ChannelParametersCollectionProperty] = channelParameters;
				}
				return tokenManager.CreateSecurityTokenProvider(initiatorServiceModelSecurityTokenRequirement) as SspiSecurityTokenProvider;
			}
			return null;
		}

		// Token: 0x06004EA2 RID: 20130 RVA: 0x0011EC34 File Offset: 0x0011CE34
		public static SecurityTokenAuthenticator GetCertificateTokenAuthenticator(SecurityTokenManager tokenManager, string transportScheme, Uri listenUri)
		{
			SecurityTokenResolver securityTokenResolver;
			return tokenManager.CreateSecurityTokenAuthenticator(new RecipientServiceModelSecurityTokenRequirement
			{
				TokenType = SecurityTokenTypes.X509Certificate,
				RequireCryptographicToken = true,
				KeyUsage = SecurityKeyUsage.Signature,
				TransportScheme = transportScheme,
				ListenUri = listenUri
			}, out securityTokenResolver);
		}

		// Token: 0x06004EA3 RID: 20131 RVA: 0x0011EC78 File Offset: 0x0011CE78
		public static SecurityTokenProvider GetCertificateTokenProvider(SecurityTokenManager tokenManager, EndpointAddress target, Uri via, string transportScheme, ChannelParameterCollection channelParameters)
		{
			if (tokenManager != null)
			{
				InitiatorServiceModelSecurityTokenRequirement initiatorServiceModelSecurityTokenRequirement = new InitiatorServiceModelSecurityTokenRequirement();
				initiatorServiceModelSecurityTokenRequirement.TokenType = SecurityTokenTypes.X509Certificate;
				initiatorServiceModelSecurityTokenRequirement.TargetAddress = target;
				initiatorServiceModelSecurityTokenRequirement.Via = via;
				initiatorServiceModelSecurityTokenRequirement.RequireCryptographicToken = false;
				initiatorServiceModelSecurityTokenRequirement.TransportScheme = transportScheme;
				if (channelParameters != null)
				{
					initiatorServiceModelSecurityTokenRequirement.Properties[ServiceModelSecurityTokenRequirement.ChannelParametersCollectionProperty] = channelParameters;
				}
				return tokenManager.CreateSecurityTokenProvider(initiatorServiceModelSecurityTokenRequirement);
			}
			return null;
		}

		// Token: 0x06004EA4 RID: 20132 RVA: 0x0011ECD4 File Offset: 0x0011CED4
		private static T GetToken<T>(SecurityTokenProvider tokenProvider, TimeSpan timeout) where T : SecurityToken
		{
			SecurityToken token = tokenProvider.GetToken(timeout);
			if (token != null && !(token is T))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("InvalidTokenProvided", new object[]
				{
					tokenProvider.GetType(),
					typeof(T)
				})));
			}
			return token as T;
		}

		// Token: 0x06004EA5 RID: 20133 RVA: 0x0011ED35 File Offset: 0x0011CF35
		public static IAsyncResult BeginGetUserNameCredential(SecurityTokenProviderContainer tokenProvider, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new TransportSecurityHelpers.GetUserNameCredentialAsyncResult(tokenProvider, timeout, callback, state);
		}

		// Token: 0x06004EA6 RID: 20134 RVA: 0x0011ED40 File Offset: 0x0011CF40
		public static NetworkCredential EndGetUserNameCredential(IAsyncResult result)
		{
			return TransportSecurityHelpers.GetUserNameCredentialAsyncResult.End(result);
		}

		// Token: 0x06004EA7 RID: 20135 RVA: 0x0011ED48 File Offset: 0x0011CF48
		public static NetworkCredential GetUserNameCredential(SecurityTokenProviderContainer tokenProvider, TimeSpan timeout)
		{
			NetworkCredential networkCredential = null;
			if (tokenProvider != null && tokenProvider.TokenProvider != null)
			{
				UserNameSecurityToken token = TransportSecurityHelpers.GetToken<UserNameSecurityToken>(tokenProvider.TokenProvider, timeout);
				if (token != null)
				{
					SecurityUtils.PrepareNetworkCredential();
					networkCredential = new NetworkCredential(token.UserName, token.Password);
				}
			}
			if (networkCredential == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("NoUserNameTokenProvided")));
			}
			return networkCredential;
		}

		// Token: 0x06004EA8 RID: 20136 RVA: 0x0011EDA8 File Offset: 0x0011CFA8
		private static InitiatorServiceModelSecurityTokenRequirement CreateUserNameTokenRequirement(EndpointAddress target, Uri via, string transportScheme)
		{
			return new InitiatorServiceModelSecurityTokenRequirement
			{
				RequireCryptographicToken = false,
				TokenType = SecurityTokenTypes.UserName,
				TargetAddress = target,
				Via = via,
				TransportScheme = transportScheme
			};
		}

		// Token: 0x06004EA9 RID: 20137 RVA: 0x0011EDE4 File Offset: 0x0011CFE4
		public static SecurityTokenProvider GetUserNameTokenProvider(SecurityTokenManager tokenManager, EndpointAddress target, Uri via, string transportScheme, AuthenticationSchemes authenticationScheme, ChannelParameterCollection channelParameters)
		{
			SecurityTokenProvider result = null;
			if (tokenManager != null)
			{
				SecurityTokenRequirement securityTokenRequirement = TransportSecurityHelpers.CreateUserNameTokenRequirement(target, via, transportScheme);
				securityTokenRequirement.Properties[ServiceModelSecurityTokenRequirement.HttpAuthenticationSchemeProperty] = authenticationScheme;
				if (channelParameters != null)
				{
					securityTokenRequirement.Properties[ServiceModelSecurityTokenRequirement.ChannelParametersCollectionProperty] = channelParameters;
				}
				result = tokenManager.CreateSecurityTokenProvider(securityTokenRequirement);
			}
			return result;
		}

		// Token: 0x06004EAA RID: 20138 RVA: 0x0011EE38 File Offset: 0x0011D038
		public static Uri GetListenUri(Uri baseAddress, string relativeAddress)
		{
			Uri result = baseAddress;
			if (!string.IsNullOrEmpty(relativeAddress))
			{
				if (!baseAddress.AbsolutePath.EndsWith("/", StringComparison.Ordinal))
				{
					UriBuilder uriBuilder = new UriBuilder(baseAddress);
					TcpChannelListener.FixIpv6Hostname(uriBuilder, baseAddress);
					uriBuilder.Path += "/";
					baseAddress = uriBuilder.Uri;
				}
				result = new Uri(baseAddress, relativeAddress);
			}
			return result;
		}

		// Token: 0x02000D2D RID: 3373
		private class GetUserNameCredentialAsyncResult : AsyncResult
		{
			// Token: 0x06007BFC RID: 31740 RVA: 0x001CF4C0 File Offset: 0x001CD6C0
			public GetUserNameCredentialAsyncResult(SecurityTokenProviderContainer tokenProvider, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				if (tokenProvider == null || tokenProvider.TokenProvider == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("NoUserNameTokenProvided")));
				}
				this.tokenProvider = tokenProvider.TokenProvider;
				IAsyncResult asyncResult = this.tokenProvider.BeginGetToken(timeout, TransportSecurityHelpers.GetUserNameCredentialAsyncResult.onGetToken, this);
				if (asyncResult.CompletedSynchronously)
				{
					this.CompleteGetToken(asyncResult);
					base.Complete(true);
				}
			}

			// Token: 0x06007BFD RID: 31741 RVA: 0x001CF530 File Offset: 0x001CD730
			private void CompleteGetToken(IAsyncResult result)
			{
				UserNameSecurityToken userNameSecurityToken = (UserNameSecurityToken)this.tokenProvider.EndGetToken(result);
				if (userNameSecurityToken != null)
				{
					SecurityUtils.PrepareNetworkCredential();
					this.credential = new NetworkCredential(userNameSecurityToken.UserName, userNameSecurityToken.Password);
					return;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("NoUserNameTokenProvided")));
			}

			// Token: 0x06007BFE RID: 31742 RVA: 0x001CF588 File Offset: 0x001CD788
			private static void OnGetToken(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				TransportSecurityHelpers.GetUserNameCredentialAsyncResult getUserNameCredentialAsyncResult = (TransportSecurityHelpers.GetUserNameCredentialAsyncResult)result.AsyncState;
				Exception exception = null;
				try
				{
					getUserNameCredentialAsyncResult.CompleteGetToken(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				getUserNameCredentialAsyncResult.Complete(false, exception);
			}

			// Token: 0x06007BFF RID: 31743 RVA: 0x001CF5DC File Offset: 0x001CD7DC
			public static NetworkCredential End(IAsyncResult result)
			{
				TransportSecurityHelpers.GetUserNameCredentialAsyncResult getUserNameCredentialAsyncResult = AsyncResult.End<TransportSecurityHelpers.GetUserNameCredentialAsyncResult>(result);
				return getUserNameCredentialAsyncResult.credential;
			}

			// Token: 0x04004732 RID: 18226
			private NetworkCredential credential;

			// Token: 0x04004733 RID: 18227
			private static AsyncCallback onGetToken = Fx.ThunkCallback(new AsyncCallback(TransportSecurityHelpers.GetUserNameCredentialAsyncResult.OnGetToken));

			// Token: 0x04004734 RID: 18228
			private SecurityTokenProvider tokenProvider;
		}

		// Token: 0x02000D2E RID: 3374
		private class GetSspiCredentialAsyncResult : AsyncResult
		{
			// Token: 0x06007C01 RID: 31745 RVA: 0x001CF610 File Offset: 0x001CD810
			public GetSspiCredentialAsyncResult(SspiSecurityTokenProvider credentialProvider, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.allowNtlm = true;
				this.impersonationLevel = TokenImpersonationLevel.Identification;
				if (credentialProvider == null)
				{
					this.EnsureCredentialInitialized();
					base.Complete(true);
					return;
				}
				this.credentialProvider = credentialProvider;
				if (TransportSecurityHelpers.GetSspiCredentialAsyncResult.onGetToken == null)
				{
					TransportSecurityHelpers.GetSspiCredentialAsyncResult.onGetToken = Fx.ThunkCallback(new AsyncCallback(TransportSecurityHelpers.GetSspiCredentialAsyncResult.OnGetToken));
				}
				IAsyncResult asyncResult = credentialProvider.BeginGetToken(timeout, TransportSecurityHelpers.GetSspiCredentialAsyncResult.onGetToken, this);
				if (asyncResult.CompletedSynchronously)
				{
					this.CompleteGetToken(asyncResult);
					base.Complete(true);
				}
			}

			// Token: 0x06007C02 RID: 31746 RVA: 0x001CF690 File Offset: 0x001CD890
			private void CompleteGetToken(IAsyncResult result)
			{
				SspiSecurityToken sspiSecurityToken = (SspiSecurityToken)this.credentialProvider.EndGetToken(result);
				if (sspiSecurityToken != null)
				{
					this.impersonationLevel = sspiSecurityToken.ImpersonationLevel;
					this.allowNtlm = sspiSecurityToken.AllowNtlm;
					if (sspiSecurityToken.NetworkCredential != null)
					{
						this.credential = sspiSecurityToken.NetworkCredential;
						SecurityUtils.FixNetworkCredential(ref this.credential);
					}
				}
				this.EnsureCredentialInitialized();
			}

			// Token: 0x06007C03 RID: 31747 RVA: 0x001CF6EF File Offset: 0x001CD8EF
			private void EnsureCredentialInitialized()
			{
				if (this.credential == null)
				{
					this.credential = CredentialCache.DefaultNetworkCredentials;
				}
			}

			// Token: 0x06007C04 RID: 31748 RVA: 0x001CF704 File Offset: 0x001CD904
			private static void OnGetToken(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				TransportSecurityHelpers.GetSspiCredentialAsyncResult getSspiCredentialAsyncResult = (TransportSecurityHelpers.GetSspiCredentialAsyncResult)result.AsyncState;
				Exception exception = null;
				try
				{
					getSspiCredentialAsyncResult.CompleteGetToken(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				getSspiCredentialAsyncResult.Complete(false, exception);
			}

			// Token: 0x06007C05 RID: 31749 RVA: 0x001CF758 File Offset: 0x001CD958
			public static NetworkCredential End(IAsyncResult result, out TokenImpersonationLevel impersonationLevel, out AuthenticationLevel authenticationLevel)
			{
				TransportSecurityHelpers.GetSspiCredentialAsyncResult getSspiCredentialAsyncResult = AsyncResult.End<TransportSecurityHelpers.GetSspiCredentialAsyncResult>(result);
				impersonationLevel = getSspiCredentialAsyncResult.impersonationLevel;
				authenticationLevel = (getSspiCredentialAsyncResult.allowNtlm ? AuthenticationLevel.MutualAuthRequested : AuthenticationLevel.MutualAuthRequired);
				return getSspiCredentialAsyncResult.credential;
			}

			// Token: 0x06007C06 RID: 31750 RVA: 0x001CF788 File Offset: 0x001CD988
			public static NetworkCredential End(IAsyncResult result, out TokenImpersonationLevel impersonationLevel, out bool allowNtlm)
			{
				TransportSecurityHelpers.GetSspiCredentialAsyncResult getSspiCredentialAsyncResult = AsyncResult.End<TransportSecurityHelpers.GetSspiCredentialAsyncResult>(result);
				impersonationLevel = getSspiCredentialAsyncResult.impersonationLevel;
				allowNtlm = getSspiCredentialAsyncResult.allowNtlm;
				return getSspiCredentialAsyncResult.credential;
			}

			// Token: 0x04004735 RID: 18229
			private bool allowNtlm;

			// Token: 0x04004736 RID: 18230
			private NetworkCredential credential;

			// Token: 0x04004737 RID: 18231
			private TokenImpersonationLevel impersonationLevel;

			// Token: 0x04004738 RID: 18232
			private static AsyncCallback onGetToken;

			// Token: 0x04004739 RID: 18233
			private SspiSecurityTokenProvider credentialProvider;
		}
	}
}
