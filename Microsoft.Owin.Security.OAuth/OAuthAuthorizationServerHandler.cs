using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth.Messages;
using Newtonsoft.Json;

namespace Microsoft.Owin.Security.OAuth
{
	// Token: 0x02000009 RID: 9
	internal class OAuthAuthorizationServerHandler : AuthenticationHandler<OAuthAuthorizationServerOptions>
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002651 File Offset: 0x00000851
		public OAuthAuthorizationServerHandler(ILogger logger)
		{
			this._logger = logger;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002660 File Offset: 0x00000860
		protected override Task<AuthenticationTicket> AuthenticateCoreAsync()
		{
			return Task.FromResult<AuthenticationTicket>(null);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000029A4 File Offset: 0x00000BA4
		public override async Task<bool> InvokeAsync()
		{
			OAuthMatchEndpointContext matchRequestContext = new OAuthMatchEndpointContext(base.Context, base.Options);
			if (base.Options.AuthorizeEndpointPath.HasValue && base.Options.AuthorizeEndpointPath == base.Request.Path)
			{
				matchRequestContext.MatchesAuthorizeEndpoint();
			}
			else if (base.Options.TokenEndpointPath.HasValue && base.Options.TokenEndpointPath == base.Request.Path)
			{
				matchRequestContext.MatchesTokenEndpoint();
			}
			await base.Options.Provider.MatchEndpoint(matchRequestContext);
			bool result;
			if (matchRequestContext.IsRequestCompleted)
			{
				result = true;
			}
			else
			{
				if (matchRequestContext.IsAuthorizeEndpoint || matchRequestContext.IsTokenEndpoint)
				{
					if (!base.Options.AllowInsecureHttp && string.Equals(base.Request.Scheme, Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase))
					{
						this._logger.WriteWarning("Authorization server ignoring http request because AllowInsecureHttp is false.", new string[0]);
						return false;
					}
					if (matchRequestContext.IsAuthorizeEndpoint)
					{
						return await this.InvokeAuthorizeEndpointAsync();
					}
					if (matchRequestContext.IsTokenEndpoint)
					{
						await this.InvokeTokenEndpointAsync();
						return true;
					}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002F94 File Offset: 0x00001194
		private async Task<bool> InvokeAuthorizeEndpointAsync()
		{
			AuthorizeEndpointRequest authorizeRequest = new AuthorizeEndpointRequest(base.Request.Query);
			OAuthValidateClientRedirectUriContext clientContext = new OAuthValidateClientRedirectUriContext(base.Context, base.Options, authorizeRequest.ClientId, authorizeRequest.RedirectUri);
			if (!string.IsNullOrEmpty(authorizeRequest.RedirectUri))
			{
				bool acceptableUri = true;
				Uri validatingUri;
				if (!Uri.TryCreate(authorizeRequest.RedirectUri, UriKind.Absolute, out validatingUri))
				{
					acceptableUri = false;
				}
				else if (!string.IsNullOrEmpty(validatingUri.Fragment))
				{
					acceptableUri = false;
				}
				else if (!base.Options.AllowInsecureHttp && string.Equals(validatingUri.Scheme, Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase))
				{
					acceptableUri = false;
				}
				if (!acceptableUri)
				{
					clientContext.SetError("invalid_request");
					return await this.SendErrorRedirectAsync(clientContext, clientContext);
				}
			}
			await base.Options.Provider.ValidateClientRedirectUri(clientContext);
			bool result;
			if (!clientContext.IsValidated)
			{
				this._logger.WriteVerbose("Unable to validate client information");
				result = await this.SendErrorRedirectAsync(clientContext, clientContext);
			}
			else
			{
				OAuthValidateAuthorizeRequestContext validatingContext = new OAuthValidateAuthorizeRequestContext(base.Context, base.Options, authorizeRequest, clientContext);
				if (string.IsNullOrEmpty(authorizeRequest.ResponseType))
				{
					this._logger.WriteVerbose("Authorize endpoint request missing required response_type parameter");
					validatingContext.SetError("invalid_request");
				}
				else if (!authorizeRequest.IsAuthorizationCodeGrantType && !authorizeRequest.IsImplicitGrantType)
				{
					this._logger.WriteVerbose("Authorize endpoint request contains unsupported response_type parameter");
					validatingContext.SetError("unsupported_response_type");
				}
				else
				{
					await base.Options.Provider.ValidateAuthorizeRequest(validatingContext);
				}
				if (!validatingContext.IsValidated)
				{
					result = await this.SendErrorRedirectAsync(clientContext, validatingContext);
				}
				else
				{
					this._clientContext = clientContext;
					this._authorizeEndpointRequest = authorizeRequest;
					OAuthAuthorizeEndpointContext authorizeEndpointContext = new OAuthAuthorizeEndpointContext(base.Context, base.Options, authorizeRequest);
					await base.Options.Provider.AuthorizeEndpoint(authorizeEndpointContext);
					result = authorizeEndpointContext.IsRequestCompleted;
				}
			}
			return result;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003A1C File Offset: 0x00001C1C
		protected override async Task ApplyResponseGrantAsync()
		{
			if (this._clientContext != null && this._authorizeEndpointRequest != null && base.Response.StatusCode == 200)
			{
				AuthenticationResponseGrant signin = base.Helper.LookupSignIn(base.Options.AuthenticationType);
				if (signin != null)
				{
					Dictionary<string, string> returnParameter = new Dictionary<string, string>();
					if (this._authorizeEndpointRequest.IsAuthorizationCodeGrantType)
					{
						DateTimeOffset currentUtc = base.Options.SystemClock.UtcNow;
						signin.Properties.IssuedUtc = new DateTimeOffset?(currentUtc);
						signin.Properties.ExpiresUtc = new DateTimeOffset?(currentUtc.Add(base.Options.AuthorizationCodeExpireTimeSpan));
						signin.Properties.Dictionary["client_id"] = this._authorizeEndpointRequest.ClientId;
						if (!string.IsNullOrEmpty(this._authorizeEndpointRequest.RedirectUri))
						{
							signin.Properties.Dictionary["redirect_uri"] = this._authorizeEndpointRequest.RedirectUri;
						}
						AuthenticationTokenCreateContext context = new AuthenticationTokenCreateContext(base.Context, base.Options.AuthorizationCodeFormat, new AuthenticationTicket(signin.Identity, signin.Properties));
						await base.Options.AuthorizationCodeProvider.CreateAsync(context);
						string code = context.Token;
						if (string.IsNullOrEmpty(code))
						{
							this._logger.WriteError("response_type code requires an Options.AuthorizationCodeProvider implementing a single-use token.");
							OAuthValidateAuthorizeRequestContext errorContext = new OAuthValidateAuthorizeRequestContext(base.Context, base.Options, this._authorizeEndpointRequest, this._clientContext);
							errorContext.SetError("unsupported_response_type");
							await this.SendErrorRedirectAsync(this._clientContext, errorContext);
						}
						else
						{
							OAuthAuthorizationEndpointResponseContext authResponseContext = new OAuthAuthorizationEndpointResponseContext(base.Context, base.Options, new AuthenticationTicket(signin.Identity, signin.Properties), this._authorizeEndpointRequest, null, code);
							await base.Options.Provider.AuthorizationEndpointResponse(authResponseContext);
							foreach (KeyValuePair<string, object> keyValuePair in authResponseContext.AdditionalResponseParameters)
							{
								returnParameter[keyValuePair.Key] = keyValuePair.Value.ToString();
							}
							returnParameter["code"] = code;
							if (!string.IsNullOrEmpty(this._authorizeEndpointRequest.State))
							{
								returnParameter["state"] = this._authorizeEndpointRequest.State;
							}
							string location = string.Empty;
							if (this._authorizeEndpointRequest.IsFormPostResponseMode)
							{
								location = base.Options.FormPostEndpoint.ToString();
								returnParameter["redirect_uri"] = this._clientContext.RedirectUri;
							}
							else
							{
								location = this._clientContext.RedirectUri;
							}
							foreach (string text in returnParameter.Keys)
							{
								location = WebUtilities.AddQueryString(location, text, returnParameter[text]);
							}
							base.Response.Redirect(location);
						}
					}
					else if (this._authorizeEndpointRequest.IsImplicitGrantType)
					{
						string location2 = this._clientContext.RedirectUri;
						DateTimeOffset currentUtc2 = base.Options.SystemClock.UtcNow;
						signin.Properties.IssuedUtc = new DateTimeOffset?(currentUtc2);
						signin.Properties.ExpiresUtc = new DateTimeOffset?(currentUtc2.Add(base.Options.AccessTokenExpireTimeSpan));
						signin.Properties.Dictionary["client_id"] = this._authorizeEndpointRequest.ClientId;
						AuthenticationTokenCreateContext accessTokenContext = new AuthenticationTokenCreateContext(base.Context, base.Options.AccessTokenFormat, new AuthenticationTicket(signin.Identity, signin.Properties));
						await base.Options.AccessTokenProvider.CreateAsync(accessTokenContext);
						string accessToken = accessTokenContext.Token;
						if (string.IsNullOrEmpty(accessToken))
						{
							accessToken = accessTokenContext.SerializeTicket();
						}
						DateTimeOffset? accessTokenExpiresUtc = accessTokenContext.Ticket.Properties.ExpiresUtc;
						OAuthAuthorizationServerHandler.Appender appender = new OAuthAuthorizationServerHandler.Appender(location2, '#');
						appender.Append("access_token", accessToken).Append("token_type", "bearer");
						if (accessTokenExpiresUtc != null)
						{
							appender.Append("expires_in", ((long)((accessTokenExpiresUtc - currentUtc2).Value.TotalSeconds + 0.5)).ToString(CultureInfo.InvariantCulture));
						}
						if (!string.IsNullOrEmpty(this._authorizeEndpointRequest.State))
						{
							appender.Append("state", this._authorizeEndpointRequest.State);
						}
						OAuthAuthorizationEndpointResponseContext authResponseContext2 = new OAuthAuthorizationEndpointResponseContext(base.Context, base.Options, new AuthenticationTicket(signin.Identity, signin.Properties), this._authorizeEndpointRequest, accessToken, null);
						await base.Options.Provider.AuthorizationEndpointResponse(authResponseContext2);
						foreach (KeyValuePair<string, object> keyValuePair2 in authResponseContext2.AdditionalResponseParameters)
						{
							appender.Append(keyValuePair2.Key, keyValuePair2.Value.ToString());
						}
						base.Response.Redirect(appender.ToString());
					}
				}
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000047FC File Offset: 0x000029FC
		private async Task InvokeTokenEndpointAsync()
		{
			DateTimeOffset currentUtc = base.Options.SystemClock.UtcNow;
			currentUtc = currentUtc.Subtract(TimeSpan.FromMilliseconds((double)currentUtc.Millisecond));
			IFormCollection form = await base.Request.ReadFormAsync();
			OAuthValidateClientAuthenticationContext clientContext = new OAuthValidateClientAuthenticationContext(base.Context, base.Options, form);
			await base.Options.Provider.ValidateClientAuthentication(clientContext);
			if (!clientContext.IsValidated)
			{
				this._logger.WriteError("clientID is not valid.");
				if (!clientContext.HasError)
				{
					clientContext.SetError("invalid_client");
				}
				await this.SendErrorAsJsonAsync(clientContext);
			}
			else
			{
				TokenEndpointRequest tokenEndpointRequest = new TokenEndpointRequest(form);
				OAuthValidateTokenRequestContext validatingContext = new OAuthValidateTokenRequestContext(base.Context, base.Options, tokenEndpointRequest, clientContext);
				AuthenticationTicket ticket = null;
				if (tokenEndpointRequest.IsAuthorizationCodeGrantType)
				{
					ticket = await this.InvokeTokenEndpointAuthorizationCodeGrantAsync(validatingContext, currentUtc);
				}
				else if (tokenEndpointRequest.IsResourceOwnerPasswordCredentialsGrantType)
				{
					ticket = await this.InvokeTokenEndpointResourceOwnerPasswordCredentialsGrantAsync(validatingContext, currentUtc);
				}
				else if (tokenEndpointRequest.IsClientCredentialsGrantType)
				{
					ticket = await this.InvokeTokenEndpointClientCredentialsGrantAsync(validatingContext, currentUtc);
				}
				else if (tokenEndpointRequest.IsRefreshTokenGrantType)
				{
					ticket = await this.InvokeTokenEndpointRefreshTokenGrantAsync(validatingContext, currentUtc);
				}
				else if (tokenEndpointRequest.IsCustomExtensionGrantType)
				{
					ticket = await this.InvokeTokenEndpointCustomGrantAsync(validatingContext, currentUtc);
				}
				else
				{
					this._logger.WriteError("grant type is not recognized");
					validatingContext.SetError("unsupported_grant_type");
				}
				if (ticket == null)
				{
					await this.SendErrorAsJsonAsync(validatingContext);
				}
				else
				{
					ticket.Properties.IssuedUtc = new DateTimeOffset?(currentUtc);
					ticket.Properties.ExpiresUtc = new DateTimeOffset?(currentUtc.Add(base.Options.AccessTokenExpireTimeSpan));
					OAuthTokenEndpointContext tokenEndpointContext = new OAuthTokenEndpointContext(base.Context, base.Options, ticket, tokenEndpointRequest);
					await base.Options.Provider.TokenEndpoint(tokenEndpointContext);
					if (tokenEndpointContext.TokenIssued)
					{
						ticket = new AuthenticationTicket(tokenEndpointContext.Identity, tokenEndpointContext.Properties);
						AuthenticationTokenCreateContext accessTokenContext = new AuthenticationTokenCreateContext(base.Context, base.Options.AccessTokenFormat, ticket);
						await base.Options.AccessTokenProvider.CreateAsync(accessTokenContext);
						string accessToken = accessTokenContext.Token;
						if (string.IsNullOrEmpty(accessToken))
						{
							accessToken = accessTokenContext.SerializeTicket();
						}
						DateTimeOffset? accessTokenExpiresUtc = ticket.Properties.ExpiresUtc;
						AuthenticationTokenCreateContext refreshTokenCreateContext = new AuthenticationTokenCreateContext(base.Context, base.Options.RefreshTokenFormat, accessTokenContext.Ticket);
						await base.Options.RefreshTokenProvider.CreateAsync(refreshTokenCreateContext);
						string refreshToken = refreshTokenCreateContext.Token;
						OAuthTokenEndpointResponseContext tokenEndpointResponseContext = new OAuthTokenEndpointResponseContext(base.Context, base.Options, ticket, tokenEndpointRequest, accessToken, tokenEndpointContext.AdditionalResponseParameters);
						await base.Options.Provider.TokenEndpointResponse(tokenEndpointResponseContext);
						MemoryStream memory = new MemoryStream();
						byte[] body;
						using (JsonTextWriter jsonTextWriter = new JsonTextWriter(new StreamWriter(memory)))
						{
							jsonTextWriter.WriteStartObject();
							jsonTextWriter.WritePropertyName("access_token");
							jsonTextWriter.WriteValue(accessToken);
							jsonTextWriter.WritePropertyName("token_type");
							jsonTextWriter.WriteValue("bearer");
							if (accessTokenExpiresUtc != null)
							{
								long num = (long)(accessTokenExpiresUtc - currentUtc).Value.TotalSeconds;
								if (num > 0L)
								{
									jsonTextWriter.WritePropertyName("expires_in");
									jsonTextWriter.WriteValue(num);
								}
							}
							if (!string.IsNullOrEmpty(refreshToken))
							{
								jsonTextWriter.WritePropertyName("refresh_token");
								jsonTextWriter.WriteValue(refreshToken);
							}
							foreach (KeyValuePair<string, object> keyValuePair in tokenEndpointResponseContext.AdditionalResponseParameters)
							{
								jsonTextWriter.WritePropertyName(keyValuePair.Key);
								jsonTextWriter.WriteValue(keyValuePair.Value);
							}
							jsonTextWriter.WriteEndObject();
							jsonTextWriter.Flush();
							body = memory.ToArray();
						}
						base.Response.ContentType = "application/json;charset=UTF-8";
						base.Response.Headers.Set("Cache-Control", "no-cache");
						base.Response.Headers.Set("Pragma", "no-cache");
						base.Response.Headers.Set("Expires", "-1");
						base.Response.ContentLength = new long?((long)memory.ToArray().Length);
						await base.Response.WriteAsync(body, base.Request.CallCancelled);
					}
					else
					{
						this._logger.WriteError("Token was not issued to tokenEndpointContext");
						validatingContext.SetError("invalid_grant");
						await this.SendErrorAsJsonAsync(validatingContext);
					}
				}
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00004C8C File Offset: 0x00002E8C
		private async Task<AuthenticationTicket> InvokeTokenEndpointAuthorizationCodeGrantAsync(OAuthValidateTokenRequestContext validatingContext, DateTimeOffset currentUtc)
		{
			TokenEndpointRequest tokenEndpointRequest = validatingContext.TokenRequest;
			AuthenticationTokenReceiveContext authorizationCodeContext = new AuthenticationTokenReceiveContext(base.Context, base.Options.AuthorizationCodeFormat, tokenEndpointRequest.AuthorizationCodeGrant.Code);
			await base.Options.AuthorizationCodeProvider.ReceiveAsync(authorizationCodeContext);
			AuthenticationTicket ticket = authorizationCodeContext.Ticket;
			AuthenticationTicket result;
			string clientId;
			if (ticket == null)
			{
				this._logger.WriteError("invalid authorization code");
				validatingContext.SetError("invalid_grant");
				result = null;
			}
			else if (ticket.Properties.ExpiresUtc == null || ticket.Properties.ExpiresUtc < currentUtc)
			{
				this._logger.WriteError("expired authorization code");
				validatingContext.SetError("invalid_grant");
				result = null;
			}
			else if (!ticket.Properties.Dictionary.TryGetValue("client_id", out clientId) || !string.Equals(clientId, validatingContext.ClientContext.ClientId, StringComparison.Ordinal))
			{
				this._logger.WriteError("authorization code does not contain matching client_id");
				validatingContext.SetError("invalid_grant");
				result = null;
			}
			else
			{
				string redirectUri;
				if (ticket.Properties.Dictionary.TryGetValue("redirect_uri", out redirectUri))
				{
					ticket.Properties.Dictionary.Remove("redirect_uri");
					if (!string.Equals(redirectUri, tokenEndpointRequest.AuthorizationCodeGrant.RedirectUri, StringComparison.Ordinal))
					{
						this._logger.WriteError("authorization code does not contain matching redirect_uri");
						validatingContext.SetError("invalid_grant");
						return null;
					}
				}
				await base.Options.Provider.ValidateTokenRequest(validatingContext);
				OAuthGrantAuthorizationCodeContext grantContext = new OAuthGrantAuthorizationCodeContext(base.Context, base.Options, ticket);
				if (validatingContext.IsValidated)
				{
					await base.Options.Provider.GrantAuthorizationCode(grantContext);
				}
				result = OAuthAuthorizationServerHandler.ReturnOutcome(validatingContext, grantContext, grantContext.Ticket, "invalid_grant");
			}
			return result;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00004EFC File Offset: 0x000030FC
		private async Task<AuthenticationTicket> InvokeTokenEndpointResourceOwnerPasswordCredentialsGrantAsync(OAuthValidateTokenRequestContext validatingContext, DateTimeOffset currentUtc)
		{
			TokenEndpointRequest tokenEndpointRequest = validatingContext.TokenRequest;
			await base.Options.Provider.ValidateTokenRequest(validatingContext);
			OAuthGrantResourceOwnerCredentialsContext grantContext = new OAuthGrantResourceOwnerCredentialsContext(base.Context, base.Options, validatingContext.ClientContext.ClientId, tokenEndpointRequest.ResourceOwnerPasswordCredentialsGrant.UserName, tokenEndpointRequest.ResourceOwnerPasswordCredentialsGrant.Password, tokenEndpointRequest.ResourceOwnerPasswordCredentialsGrant.Scope);
			if (validatingContext.IsValidated)
			{
				await base.Options.Provider.GrantResourceOwnerCredentials(grantContext);
			}
			return OAuthAuthorizationServerHandler.ReturnOutcome(validatingContext, grantContext, grantContext.Ticket, "invalid_grant");
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00005148 File Offset: 0x00003348
		private async Task<AuthenticationTicket> InvokeTokenEndpointClientCredentialsGrantAsync(OAuthValidateTokenRequestContext validatingContext, DateTimeOffset currentUtc)
		{
			TokenEndpointRequest tokenEndpointRequest = validatingContext.TokenRequest;
			await base.Options.Provider.ValidateTokenRequest(validatingContext);
			AuthenticationTicket result;
			if (!validatingContext.IsValidated)
			{
				result = null;
			}
			else
			{
				OAuthGrantClientCredentialsContext grantContext = new OAuthGrantClientCredentialsContext(base.Context, base.Options, validatingContext.ClientContext.ClientId, tokenEndpointRequest.ClientCredentialsGrant.Scope);
				await base.Options.Provider.GrantClientCredentials(grantContext);
				result = OAuthAuthorizationServerHandler.ReturnOutcome(validatingContext, grantContext, grantContext.Ticket, "unauthorized_client");
			}
			return result;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000054FC File Offset: 0x000036FC
		private async Task<AuthenticationTicket> InvokeTokenEndpointRefreshTokenGrantAsync(OAuthValidateTokenRequestContext validatingContext, DateTimeOffset currentUtc)
		{
			TokenEndpointRequest tokenEndpointRequest = validatingContext.TokenRequest;
			AuthenticationTokenReceiveContext refreshTokenContext = new AuthenticationTokenReceiveContext(base.Context, base.Options.RefreshTokenFormat, tokenEndpointRequest.RefreshTokenGrant.RefreshToken);
			await base.Options.RefreshTokenProvider.ReceiveAsync(refreshTokenContext);
			AuthenticationTicket ticket = refreshTokenContext.Ticket;
			AuthenticationTicket result;
			if (ticket == null)
			{
				this._logger.WriteError("invalid refresh token");
				validatingContext.SetError("invalid_grant");
				result = null;
			}
			else if (ticket.Properties.ExpiresUtc == null || ticket.Properties.ExpiresUtc < currentUtc)
			{
				this._logger.WriteError("expired refresh token");
				validatingContext.SetError("invalid_grant");
				result = null;
			}
			else
			{
				await base.Options.Provider.ValidateTokenRequest(validatingContext);
				OAuthGrantRefreshTokenContext grantContext = new OAuthGrantRefreshTokenContext(base.Context, base.Options, ticket, validatingContext.ClientContext.ClientId);
				if (validatingContext.IsValidated)
				{
					await base.Options.Provider.GrantRefreshToken(grantContext);
				}
				result = OAuthAuthorizationServerHandler.ReturnOutcome(validatingContext, grantContext, grantContext.Ticket, "invalid_grant");
			}
			return result;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00005754 File Offset: 0x00003954
		private async Task<AuthenticationTicket> InvokeTokenEndpointCustomGrantAsync(OAuthValidateTokenRequestContext validatingContext, DateTimeOffset currentUtc)
		{
			TokenEndpointRequest tokenEndpointRequest = validatingContext.TokenRequest;
			await base.Options.Provider.ValidateTokenRequest(validatingContext);
			OAuthGrantCustomExtensionContext grantContext = new OAuthGrantCustomExtensionContext(base.Context, base.Options, validatingContext.ClientContext.ClientId, tokenEndpointRequest.GrantType, tokenEndpointRequest.CustomExtensionGrant.Parameters);
			if (validatingContext.IsValidated)
			{
				await base.Options.Provider.GrantCustomExtension(grantContext);
			}
			return OAuthAuthorizationServerHandler.ReturnOutcome(validatingContext, grantContext, grantContext.Ticket, "unsupported_grant_type");
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000057A4 File Offset: 0x000039A4
		private static AuthenticationTicket ReturnOutcome(OAuthValidateTokenRequestContext validatingContext, BaseValidatingContext<OAuthAuthorizationServerOptions> grantContext, AuthenticationTicket ticket, string defaultError)
		{
			if (!validatingContext.IsValidated)
			{
				return null;
			}
			if (!grantContext.IsValidated)
			{
				if (grantContext.HasError)
				{
					validatingContext.SetError(grantContext.Error, grantContext.ErrorDescription, grantContext.ErrorUri);
				}
				else
				{
					validatingContext.SetError(defaultError);
				}
				return null;
			}
			if (ticket == null)
			{
				validatingContext.SetError(defaultError);
				return null;
			}
			return ticket;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000057FC File Offset: 0x000039FC
		private Task SendErrorAsJsonAsync(BaseValidatingContext<OAuthAuthorizationServerOptions> validatingContext)
		{
			string value = validatingContext.HasError ? validatingContext.Error : "invalid_request";
			string value2 = validatingContext.HasError ? validatingContext.ErrorDescription : null;
			string value3 = validatingContext.HasError ? validatingContext.ErrorUri : null;
			MemoryStream memoryStream = new MemoryStream();
			byte[] array;
			using (JsonTextWriter jsonTextWriter = new JsonTextWriter(new StreamWriter(memoryStream)))
			{
				jsonTextWriter.WriteStartObject();
				jsonTextWriter.WritePropertyName("error");
				jsonTextWriter.WriteValue(value);
				if (!string.IsNullOrEmpty(value2))
				{
					jsonTextWriter.WritePropertyName("error_description");
					jsonTextWriter.WriteValue(value2);
				}
				if (!string.IsNullOrEmpty(value3))
				{
					jsonTextWriter.WritePropertyName("error_uri");
					jsonTextWriter.WriteValue(value3);
				}
				jsonTextWriter.WriteEndObject();
				jsonTextWriter.Flush();
				array = memoryStream.ToArray();
			}
			base.Response.StatusCode = 400;
			base.Response.ContentType = "application/json;charset=UTF-8";
			base.Response.Headers.Set("Cache-Control", "no-cache");
			base.Response.Headers.Set("Pragma", "no-cache");
			base.Response.Headers.Set("Expires", "-1");
			base.Response.Headers.Set("Content-Length", array.Length.ToString(CultureInfo.InvariantCulture));
			return base.Response.WriteAsync(array, base.Request.CallCancelled);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000598C File Offset: 0x00003B8C
		private Task<bool> SendErrorRedirectAsync(OAuthValidateClientRedirectUriContext clientContext, BaseValidatingContext<OAuthAuthorizationServerOptions> validatingContext)
		{
			if (clientContext == null)
			{
				throw new ArgumentNullException("clientContext");
			}
			string text = validatingContext.HasError ? validatingContext.Error : "invalid_request";
			string text2 = validatingContext.HasError ? validatingContext.ErrorDescription : null;
			string text3 = validatingContext.HasError ? validatingContext.ErrorUri : null;
			if (!clientContext.IsValidated)
			{
				return this.SendErrorPageAsync(text, text2, text3);
			}
			string text4 = WebUtilities.AddQueryString(clientContext.RedirectUri, "error", text);
			if (!string.IsNullOrEmpty(text2))
			{
				text4 = WebUtilities.AddQueryString(text4, "error_description", text2);
			}
			if (!string.IsNullOrEmpty(text3))
			{
				text4 = WebUtilities.AddQueryString(text4, "error_uri", text3);
			}
			base.Response.Redirect(text4);
			return Task.FromResult<bool>(true);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00005D0C File Offset: 0x00003F0C
		private async Task<bool> SendErrorPageAsync(string error, string errorDescription, string errorUri)
		{
			base.Response.StatusCode = 400;
			base.Response.Headers.Set("Cache-Control", "no-cache");
			base.Response.Headers.Set("Pragma", "no-cache");
			base.Response.Headers.Set("Expires", "-1");
			bool result;
			if (base.Options.ApplicationCanDisplayErrors)
			{
				base.Context.Set<string>("oauth.Error", error);
				base.Context.Set<string>("oauth.ErrorDescription", errorDescription);
				base.Context.Set<string>("oauth.ErrorUri", errorUri);
				result = false;
			}
			else
			{
				MemoryStream memory = new MemoryStream();
				byte[] body;
				using (StreamWriter streamWriter = new StreamWriter(memory))
				{
					streamWriter.WriteLine("error: {0}", error);
					if (!string.IsNullOrEmpty(errorDescription))
					{
						streamWriter.WriteLine("error_description: {0}", errorDescription);
					}
					if (!string.IsNullOrEmpty(errorUri))
					{
						streamWriter.WriteLine("error_uri: {0}", errorUri);
					}
					streamWriter.Flush();
					body = memory.ToArray();
				}
				base.Response.ContentType = "text/plain;charset=UTF-8";
				base.Response.Headers.Set("Content-Length", body.Length.ToString(CultureInfo.InvariantCulture));
				await base.Response.WriteAsync(body, base.Request.CallCancelled);
				result = true;
			}
			return result;
		}

		// Token: 0x04000018 RID: 24
		private readonly ILogger _logger;

		// Token: 0x04000019 RID: 25
		private AuthorizeEndpointRequest _authorizeEndpointRequest;

		// Token: 0x0400001A RID: 26
		private OAuthValidateClientRedirectUriContext _clientContext;

		// Token: 0x0200000A RID: 10
		private class Appender
		{
			// Token: 0x0600004F RID: 79 RVA: 0x00005D6A File Offset: 0x00003F6A
			public Appender(string value, char delimiter)
			{
				this._sb = new StringBuilder(value);
				this._delimiter = delimiter;
				this._hasDelimiter = (value.IndexOf(delimiter) != -1);
			}

			// Token: 0x06000050 RID: 80 RVA: 0x00005D98 File Offset: 0x00003F98
			public OAuthAuthorizationServerHandler.Appender Append(string name, string value)
			{
				this._sb.Append(this._hasDelimiter ? '&' : this._delimiter).Append(Uri.EscapeDataString(name)).Append('=').Append(Uri.EscapeDataString(value));
				this._hasDelimiter = true;
				return this;
			}

			// Token: 0x06000051 RID: 81 RVA: 0x00005DE8 File Offset: 0x00003FE8
			public override string ToString()
			{
				return this._sb.ToString();
			}

			// Token: 0x0400001B RID: 27
			private readonly char _delimiter;

			// Token: 0x0400001C RID: 28
			private readonly StringBuilder _sb;

			// Token: 0x0400001D RID: 29
			private bool _hasDelimiter;
		}
	}
}
