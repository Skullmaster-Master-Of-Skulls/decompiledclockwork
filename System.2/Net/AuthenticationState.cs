using System;
using System.Net.Security;

namespace System.Net
{
	// Token: 0x02000190 RID: 400
	internal class AuthenticationState
	{
		// Token: 0x06000F61 RID: 3937 RVA: 0x0004F84E File Offset: 0x0004DA4E
		internal NTAuthentication GetSecurityContext(IAuthenticationModule module)
		{
			if (module != this.Module)
			{
				return null;
			}
			return this.SecurityContext;
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x0004F861 File Offset: 0x0004DA61
		internal void SetSecurityContext(NTAuthentication securityContext, IAuthenticationModule module)
		{
			this.SecurityContext = securityContext;
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000F63 RID: 3939 RVA: 0x0004F86A File Offset: 0x0004DA6A
		// (set) Token: 0x06000F64 RID: 3940 RVA: 0x0004F872 File Offset: 0x0004DA72
		internal TransportContext TransportContext
		{
			get
			{
				return this._TransportContext;
			}
			set
			{
				this._TransportContext = value;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000F65 RID: 3941 RVA: 0x0004F87B File Offset: 0x0004DA7B
		internal HttpResponseHeader AuthenticateHeader
		{
			get
			{
				if (!this.IsProxyAuth)
				{
					return HttpResponseHeader.WwwAuthenticate;
				}
				return HttpResponseHeader.ProxyAuthenticate;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000F66 RID: 3942 RVA: 0x0004F88A File Offset: 0x0004DA8A
		internal string AuthorizationHeader
		{
			get
			{
				if (!this.IsProxyAuth)
				{
					return "Authorization";
				}
				return "Proxy-Authorization";
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000F67 RID: 3943 RVA: 0x0004F89F File Offset: 0x0004DA9F
		internal HttpStatusCode StatusCodeMatch
		{
			get
			{
				if (!this.IsProxyAuth)
				{
					return HttpStatusCode.Unauthorized;
				}
				return HttpStatusCode.ProxyAuthenticationRequired;
			}
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0004F8B4 File Offset: 0x0004DAB4
		internal AuthenticationState(bool isProxyAuth)
		{
			this.IsProxyAuth = isProxyAuth;
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0004F8C4 File Offset: 0x0004DAC4
		private void PrepareState(HttpWebRequest httpWebRequest)
		{
			Uri uri = this.IsProxyAuth ? httpWebRequest.ServicePoint.InternalAddress : httpWebRequest.GetRemoteResourceUri();
			if (this.ChallengedUri != uri)
			{
				if (this.ChallengedUri == null || this.ChallengedUri.Scheme != uri.Scheme || this.ChallengedUri.Host != uri.Host || this.ChallengedUri.Port != uri.Port)
				{
					this.ChallengedSpn = null;
				}
				this.ChallengedUri = uri;
			}
			httpWebRequest.CurrentAuthenticationState = this;
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x0004F954 File Offset: 0x0004DB54
		internal SpnToken GetComputeSpn(HttpWebRequest httpWebRequest)
		{
			if (this.ChallengedSpn != null)
			{
				return this.ChallengedSpn;
			}
			bool flag = true;
			string canonicalKey = httpWebRequest.ChallengedUri.GetParts(UriComponents.Scheme | UriComponents.Host | UriComponents.Port | UriComponents.Path, UriFormat.SafeUnescaped);
			SpnToken spnToken = AuthenticationManager.SpnDictionary.InternalGet(canonicalKey);
			if (spnToken == null || spnToken.Spn == null)
			{
				string text;
				if (!this.IsProxyAuth && (httpWebRequest.ServicePoint.InternalProxyServicePoint || httpWebRequest.UseCustomHost))
				{
					text = httpWebRequest.ChallengedUri.Host;
					if (httpWebRequest.ChallengedUri.HostNameType == UriHostNameType.IPv6 || httpWebRequest.ChallengedUri.HostNameType == UriHostNameType.IPv4 || text.IndexOf('.') != -1)
					{
						goto IL_D1;
					}
					try
					{
						IPHostEntry iphostEntry;
						if (Dns.TryInternalResolve(text, out iphostEntry))
						{
							text = iphostEntry.HostName;
							flag &= iphostEntry.isTrustedHost;
						}
						goto IL_D1;
					}
					catch (Exception exception)
					{
						if (NclUtilities.IsFatal(exception))
						{
							throw;
						}
						goto IL_D1;
					}
				}
				text = httpWebRequest.ServicePoint.Hostname;
				flag &= httpWebRequest.ServicePoint.IsTrustedHost;
				IL_D1:
				string spn = "HTTP/" + text;
				canonicalKey = httpWebRequest.ChallengedUri.GetParts(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped) + "/";
				spnToken = new SpnToken(spn, flag);
				AuthenticationManager.SpnDictionary.InternalSet(canonicalKey, spnToken);
			}
			this.ChallengedSpn = spnToken;
			return this.ChallengedSpn;
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x0004FA8C File Offset: 0x0004DC8C
		internal void PreAuthIfNeeded(HttpWebRequest httpWebRequest, ICredentials authInfo)
		{
			if (!this.TriedPreAuth)
			{
				this.TriedPreAuth = true;
				if (authInfo != null)
				{
					this.PrepareState(httpWebRequest);
					try
					{
						Authorization authorization = AuthenticationManager.PreAuthenticate(httpWebRequest, authInfo);
						if (authorization != null && authorization.Message != null)
						{
							this.UniqueGroupId = authorization.ConnectionGroupId;
							httpWebRequest.Headers.Set(this.AuthorizationHeader, authorization.Message);
						}
					}
					catch (Exception ex)
					{
						this.ClearSession(httpWebRequest);
					}
				}
			}
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x0004FB08 File Offset: 0x0004DD08
		internal bool AttemptAuthenticate(HttpWebRequest httpWebRequest, ICredentials authInfo)
		{
			if (this.Authorization != null && this.Authorization.Complete)
			{
				if (this.IsProxyAuth)
				{
					this.ClearAuthReq(httpWebRequest);
				}
				return false;
			}
			if (authInfo == null)
			{
				return false;
			}
			string text = httpWebRequest.AuthHeader(this.AuthenticateHeader);
			if (text == null)
			{
				if (!this.IsProxyAuth && this.Authorization != null && httpWebRequest.ProxyAuthenticationState.Authorization != null)
				{
					httpWebRequest.Headers.Set(this.AuthorizationHeader, this.Authorization.Message);
				}
				return false;
			}
			this.PrepareState(httpWebRequest);
			try
			{
				this.Authorization = AuthenticationManager.Authenticate(text, httpWebRequest, authInfo);
			}
			catch (Exception ex)
			{
				this.Authorization = null;
				this.ClearSession(httpWebRequest);
				throw;
			}
			if (this.Authorization == null)
			{
				return false;
			}
			if (this.Authorization.Message == null)
			{
				this.Authorization = null;
				return false;
			}
			this.UniqueGroupId = this.Authorization.ConnectionGroupId;
			try
			{
				httpWebRequest.Headers.Set(this.AuthorizationHeader, this.Authorization.Message);
			}
			catch
			{
				this.Authorization = null;
				this.ClearSession(httpWebRequest);
				throw;
			}
			return true;
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x0004FC30 File Offset: 0x0004DE30
		internal void ClearAuthReq(HttpWebRequest httpWebRequest)
		{
			this.TriedPreAuth = false;
			this.Authorization = null;
			this.UniqueGroupId = null;
			httpWebRequest.Headers.Remove(this.AuthorizationHeader);
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x0004FC58 File Offset: 0x0004DE58
		internal void Update(HttpWebRequest httpWebRequest)
		{
			if (this.Authorization != null)
			{
				this.PrepareState(httpWebRequest);
				ISessionAuthenticationModule sessionAuthenticationModule = this.Module as ISessionAuthenticationModule;
				if (sessionAuthenticationModule != null)
				{
					string challenge = httpWebRequest.AuthHeader(this.AuthenticateHeader);
					if (this.IsProxyAuth || httpWebRequest.ResponseStatusCode != HttpStatusCode.ProxyAuthenticationRequired)
					{
						bool complete = true;
						try
						{
							complete = sessionAuthenticationModule.Update(challenge, httpWebRequest);
						}
						catch (Exception ex)
						{
							this.ClearSession(httpWebRequest);
							if (httpWebRequest.AuthenticationLevel == AuthenticationLevel.MutualAuthRequired && (httpWebRequest.CurrentAuthenticationState == null || httpWebRequest.CurrentAuthenticationState.Authorization == null || !httpWebRequest.CurrentAuthenticationState.Authorization.MutuallyAuthenticated))
							{
								throw;
							}
						}
						this.Authorization.SetComplete(complete);
					}
				}
				if (httpWebRequest.PreAuthenticate && this.Module != null && this.Authorization.Complete && this.Module.CanPreAuthenticate && httpWebRequest.ResponseStatusCode != this.StatusCodeMatch)
				{
					AuthenticationManager.BindModule(this.ChallengedUri, this.Authorization, this.Module);
				}
			}
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x0004FD5C File Offset: 0x0004DF5C
		internal void ClearSession()
		{
			if (this.SecurityContext != null)
			{
				this.SecurityContext.CloseContext();
				this.SecurityContext = null;
			}
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x0004FD78 File Offset: 0x0004DF78
		internal void ClearSession(HttpWebRequest httpWebRequest)
		{
			this.PrepareState(httpWebRequest);
			ISessionAuthenticationModule sessionAuthenticationModule = this.Module as ISessionAuthenticationModule;
			this.Module = null;
			if (sessionAuthenticationModule != null)
			{
				try
				{
					sessionAuthenticationModule.ClearSession(httpWebRequest);
				}
				catch (Exception exception)
				{
					if (NclUtilities.IsFatal(exception))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x040012B2 RID: 4786
		private bool TriedPreAuth;

		// Token: 0x040012B3 RID: 4787
		internal Authorization Authorization;

		// Token: 0x040012B4 RID: 4788
		internal IAuthenticationModule Module;

		// Token: 0x040012B5 RID: 4789
		internal string UniqueGroupId;

		// Token: 0x040012B6 RID: 4790
		private bool IsProxyAuth;

		// Token: 0x040012B7 RID: 4791
		internal Uri ChallengedUri;

		// Token: 0x040012B8 RID: 4792
		private SpnToken ChallengedSpn;

		// Token: 0x040012B9 RID: 4793
		private NTAuthentication SecurityContext;

		// Token: 0x040012BA RID: 4794
		private TransportContext _TransportContext;
	}
}
