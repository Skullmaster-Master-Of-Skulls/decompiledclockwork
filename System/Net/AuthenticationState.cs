using System;
using System.Net.Security;

namespace System.Net
{
	// Token: 0x020004AE RID: 1198
	internal class AuthenticationState
	{
		// Token: 0x060024E8 RID: 9448 RVA: 0x000920AC File Offset: 0x000910AC
		internal NTAuthentication GetSecurityContext(IAuthenticationModule module)
		{
			if (module != this.Module)
			{
				return null;
			}
			return this.SecurityContext;
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x000920BF File Offset: 0x000910BF
		internal void SetSecurityContext(NTAuthentication securityContext, IAuthenticationModule module)
		{
			this.SecurityContext = securityContext;
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x060024EA RID: 9450 RVA: 0x000920C8 File Offset: 0x000910C8
		// (set) Token: 0x060024EB RID: 9451 RVA: 0x000920D0 File Offset: 0x000910D0
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

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x060024EC RID: 9452 RVA: 0x000920D9 File Offset: 0x000910D9
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

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x060024ED RID: 9453 RVA: 0x000920E8 File Offset: 0x000910E8
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

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x060024EE RID: 9454 RVA: 0x000920FD File Offset: 0x000910FD
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

		// Token: 0x060024EF RID: 9455 RVA: 0x00092112 File Offset: 0x00091112
		internal AuthenticationState(bool isProxyAuth)
		{
			this.IsProxyAuth = isProxyAuth;
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x00092124 File Offset: 0x00091124
		private void PrepareState(HttpWebRequest httpWebRequest)
		{
			Uri uri = this.IsProxyAuth ? httpWebRequest.ServicePoint.InternalAddress : httpWebRequest.Address;
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

		// Token: 0x060024F1 RID: 9457 RVA: 0x000921B4 File Offset: 0x000911B4
		internal string GetComputeSpn(HttpWebRequest httpWebRequest)
		{
			if (this.ChallengedSpn != null)
			{
				return this.ChallengedSpn;
			}
			string canonicalKey = httpWebRequest.ChallengedUri.GetParts(UriComponents.Scheme | UriComponents.Host | UriComponents.Port | UriComponents.Path, UriFormat.SafeUnescaped);
			string text = AuthenticationManager.SpnDictionary.InternalGet(canonicalKey);
			if (text == null)
			{
				if (!this.IsProxyAuth && httpWebRequest.ServicePoint.InternalProxyServicePoint)
				{
					text = httpWebRequest.ChallengedUri.Host;
					if (httpWebRequest.ChallengedUri.HostNameType == UriHostNameType.IPv6 || httpWebRequest.ChallengedUri.HostNameType == UriHostNameType.IPv4 || text.IndexOf('.') != -1)
					{
						goto IL_9F;
					}
					try
					{
						text = Dns.InternalGetHostByName(text).HostName;
						goto IL_9F;
					}
					catch (Exception exception)
					{
						if (NclUtilities.IsFatal(exception))
						{
							throw;
						}
						goto IL_9F;
					}
				}
				text = httpWebRequest.ServicePoint.Hostname;
				IL_9F:
				text = "HTTP/" + text;
				canonicalKey = httpWebRequest.ChallengedUri.GetParts(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped) + "/";
				AuthenticationManager.SpnDictionary.InternalSet(canonicalKey, text);
			}
			return this.ChallengedSpn = text;
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x000922AC File Offset: 0x000912AC
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
					catch (Exception)
					{
						this.ClearSession(httpWebRequest);
					}
					catch
					{
						this.ClearSession(httpWebRequest);
					}
				}
			}
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x0009233C File Offset: 0x0009133C
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
			catch (Exception)
			{
				this.Authorization = null;
				this.ClearSession(httpWebRequest);
				throw;
			}
			catch
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

		// Token: 0x060024F4 RID: 9460 RVA: 0x00092484 File Offset: 0x00091484
		internal void ClearAuthReq(HttpWebRequest httpWebRequest)
		{
			this.TriedPreAuth = false;
			this.Authorization = null;
			this.UniqueGroupId = null;
			httpWebRequest.Headers.Remove(this.AuthorizationHeader);
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x000924AC File Offset: 0x000914AC
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
						catch (Exception)
						{
							this.ClearSession(httpWebRequest);
							if (httpWebRequest.AuthenticationLevel == AuthenticationLevel.MutualAuthRequired && (httpWebRequest.CurrentAuthenticationState == null || httpWebRequest.CurrentAuthenticationState.Authorization == null || !httpWebRequest.CurrentAuthenticationState.Authorization.MutuallyAuthenticated))
							{
								throw;
							}
						}
						catch
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
				if (this.Module != null && this.Authorization.Complete && this.Module.CanPreAuthenticate && httpWebRequest.ResponseStatusCode != this.StatusCodeMatch)
				{
					AuthenticationManager.BindModule(this.ChallengedUri, this.Authorization, this.Module);
				}
			}
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x000925F8 File Offset: 0x000915F8
		internal void ClearSession()
		{
			if (this.SecurityContext != null)
			{
				this.SecurityContext.CloseContext();
				this.SecurityContext = null;
			}
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x00092614 File Offset: 0x00091614
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
				catch
				{
				}
			}
		}

		// Token: 0x040024E4 RID: 9444
		private bool TriedPreAuth;

		// Token: 0x040024E5 RID: 9445
		internal Authorization Authorization;

		// Token: 0x040024E6 RID: 9446
		internal IAuthenticationModule Module;

		// Token: 0x040024E7 RID: 9447
		internal string UniqueGroupId;

		// Token: 0x040024E8 RID: 9448
		private bool IsProxyAuth;

		// Token: 0x040024E9 RID: 9449
		internal Uri ChallengedUri;

		// Token: 0x040024EA RID: 9450
		private string ChallengedSpn;

		// Token: 0x040024EB RID: 9451
		private NTAuthentication SecurityContext;

		// Token: 0x040024EC RID: 9452
		private TransportContext _TransportContext;
	}
}
