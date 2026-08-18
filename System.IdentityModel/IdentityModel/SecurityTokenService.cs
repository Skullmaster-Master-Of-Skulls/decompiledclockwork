using System;
using System.IdentityModel.Configuration;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.Runtime;
using System.Security.Claims;

namespace System.IdentityModel
{
	// Token: 0x02000075 RID: 117
	public abstract class SecurityTokenService
	{
		// Token: 0x060003CC RID: 972 RVA: 0x0000DF48 File Offset: 0x0000C148
		protected SecurityTokenService(SecurityTokenServiceConfiguration securityTokenServiceConfiguration)
		{
			if (securityTokenServiceConfiguration == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("securityTokenServiceConfiguration");
			}
			this._securityTokenServiceConfiguration = securityTokenServiceConfiguration;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000DF6A File Offset: 0x0000C16A
		public virtual IAsyncResult BeginCancel(ClaimsPrincipal principal, RequestSecurityToken request, AsyncCallback callback, object state)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidRequestException(SR.GetString("ID3141", new object[]
			{
				(request != null) ? request.RequestType : "Cancel"
			})));
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000DF9E File Offset: 0x0000C19E
		protected virtual IAsyncResult BeginGetScope(ClaimsPrincipal principal, RequestSecurityToken request, AsyncCallback callback, object state)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException(SR.GetString("ID2081")));
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000DFBC File Offset: 0x0000C1BC
		public virtual IAsyncResult BeginIssue(ClaimsPrincipal principal, RequestSecurityToken request, AsyncCallback callback, object state)
		{
			if (request == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("request");
			}
			this._principal = principal;
			this._request = request;
			this.ValidateRequest(request);
			SecurityTokenService.FederatedAsyncState federatedAsyncState = new SecurityTokenService.FederatedAsyncState(request, principal, new TypedAsyncResult<RequestSecurityTokenResponse>(callback, state));
			this.BeginGetScope(principal, request, new AsyncCallback(this.OnGetScopeComplete), federatedAsyncState);
			return federatedAsyncState.Result;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000E01D File Offset: 0x0000C21D
		public virtual IAsyncResult BeginRenew(ClaimsPrincipal principal, RequestSecurityToken request, AsyncCallback callback, object state)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidRequestException(SR.GetString("ID3141", new object[]
			{
				(request != null && request.RequestType != null) ? request.RequestType : "Renew"
			})));
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000E059 File Offset: 0x0000C259
		public virtual IAsyncResult BeginValidate(ClaimsPrincipal principal, RequestSecurityToken request, AsyncCallback callback, object state)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidRequestException(SR.GetString("ID3141", new object[]
			{
				(request != null && request.RequestType != null) ? request.RequestType : "Validate"
			})));
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000E095 File Offset: 0x0000C295
		public virtual RequestSecurityTokenResponse Cancel(ClaimsPrincipal principal, RequestSecurityToken request)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidRequestException(SR.GetString("ID3141", new object[]
			{
				(request != null && request.RequestType != null) ? request.RequestType : "Cancel"
			})));
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000E0D4 File Offset: 0x0000C2D4
		protected virtual SecurityTokenDescriptor CreateSecurityTokenDescriptor(RequestSecurityToken request, Scope scope)
		{
			if (request == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("request");
			}
			if (scope == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("scope");
			}
			SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor();
			securityTokenDescriptor.AppliesToAddress = scope.AppliesToAddress;
			securityTokenDescriptor.ReplyToAddress = scope.ReplyToAddress;
			securityTokenDescriptor.SigningCredentials = scope.SigningCredentials;
			if (securityTokenDescriptor.SigningCredentials == null)
			{
				securityTokenDescriptor.SigningCredentials = this.SecurityTokenServiceConfiguration.SigningCredentials;
			}
			if (scope.EncryptingCredentials != null && scope.EncryptingCredentials.SecurityKey is AsymmetricSecurityKey && (request.EncryptionAlgorithm == null || request.EncryptionAlgorithm == "http://www.w3.org/2001/04/xmlenc#aes256-cbc") && (request.SecondaryParameters == null || request.SecondaryParameters.EncryptionAlgorithm == null || request.SecondaryParameters.EncryptionAlgorithm == "http://www.w3.org/2001/04/xmlenc#aes256-cbc"))
			{
				securityTokenDescriptor.EncryptingCredentials = new EncryptedKeyEncryptingCredentials(scope.EncryptingCredentials, 256, "http://www.w3.org/2001/04/xmlenc#aes256-cbc");
			}
			return securityTokenDescriptor;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000E1C6 File Offset: 0x0000C3C6
		protected virtual string GetIssuerName()
		{
			return this.SecurityTokenServiceConfiguration.TokenIssuerName;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000E1D4 File Offset: 0x0000C3D4
		private string GetValidIssuerName()
		{
			string issuerName = this.GetIssuerName();
			if (string.IsNullOrEmpty(issuerName))
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID2083"));
			}
			return issuerName;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000E204 File Offset: 0x0000C404
		protected virtual ProofDescriptor GetProofToken(RequestSecurityToken request, Scope scope)
		{
			if (request == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("request");
			}
			if (scope == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("scope");
			}
			EncryptingCredentials requestorProofEncryptingCredentials = this.GetRequestorProofEncryptingCredentials(request);
			if (scope.EncryptingCredentials != null && !(scope.EncryptingCredentials.SecurityKey is AsymmetricSecurityKey))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4179")));
			}
			EncryptingCredentials encryptingCredentials = scope.EncryptingCredentials;
			string x = string.IsNullOrEmpty(request.KeyType) ? "http://schemas.microsoft.com/idfx/keytype/symmetric" : request.KeyType;
			ProofDescriptor result = null;
			if (StringComparer.Ordinal.Equals(x, "http://schemas.microsoft.com/idfx/keytype/asymmetric"))
			{
				if (request.UseKey == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidRequestException(SR.GetString("ID3091")));
				}
				result = new AsymmetricProofDescriptor(request.UseKey.SecurityKeyIdentifier);
			}
			else if (StringComparer.Ordinal.Equals(x, "http://schemas.microsoft.com/idfx/keytype/symmetric"))
			{
				if (request.ComputedKeyAlgorithm != null && !StringComparer.Ordinal.Equals(request.ComputedKeyAlgorithm, "http://schemas.microsoft.com/idfx/computedkeyalgorithm/psha1"))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new RequestFailedException(SR.GetString("ID2011", new object[]
					{
						request.ComputedKeyAlgorithm
					})));
				}
				if (encryptingCredentials == null && scope.SymmetricKeyEncryptionRequired)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new RequestFailedException(SR.GetString("ID4007")));
				}
				if (request.KeySizeInBits == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new RequestFailedException(SR.GetString("ID2059")));
				}
				if (request.Entropy != null)
				{
					result = new SymmetricProofDescriptor(request.KeySizeInBits.Value, encryptingCredentials, requestorProofEncryptingCredentials, request.Entropy.GetKeyBytes(), request.EncryptWith);
				}
				else
				{
					result = new SymmetricProofDescriptor(request.KeySizeInBits.Value, encryptingCredentials, requestorProofEncryptingCredentials, request.EncryptWith);
				}
			}
			else
			{
				StringComparer.Ordinal.Equals(x, "http://schemas.microsoft.com/idfx/keytype/bearer");
			}
			return result;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000E3F4 File Offset: 0x0000C5F4
		protected virtual EncryptingCredentials GetRequestorProofEncryptingCredentials(RequestSecurityToken request)
		{
			if (request == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("request");
			}
			if (request.ProofEncryption == null)
			{
				return null;
			}
			X509SecurityToken x509SecurityToken = request.ProofEncryption.GetSecurityToken() as X509SecurityToken;
			if (x509SecurityToken != null)
			{
				return new X509EncryptingCredentials(x509SecurityToken);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new RequestFailedException(SR.GetString("ID2084", new object[]
			{
				request.ProofEncryption.GetSecurityToken()
			})));
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000E468 File Offset: 0x0000C668
		protected virtual Lifetime GetTokenLifetime(Lifetime requestLifetime)
		{
			DateTime dateTime;
			DateTime expires;
			if (requestLifetime == null)
			{
				dateTime = DateTime.UtcNow;
				expires = DateTimeUtil.Add(dateTime, this._securityTokenServiceConfiguration.DefaultTokenLifetime);
			}
			else
			{
				if (requestLifetime.Created != null)
				{
					dateTime = requestLifetime.Created.Value;
				}
				else
				{
					dateTime = DateTime.UtcNow;
				}
				if (requestLifetime.Expires != null)
				{
					expires = requestLifetime.Expires.Value;
				}
				else
				{
					expires = DateTimeUtil.Add(dateTime, this._securityTokenServiceConfiguration.DefaultTokenLifetime);
				}
			}
			this.VerifyComputedLifetime(dateTime, expires);
			return new Lifetime(dateTime, expires);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000E4FC File Offset: 0x0000C6FC
		private void VerifyComputedLifetime(DateTime created, DateTime expires)
		{
			DateTime utcNow = DateTime.UtcNow;
			if (DateTimeUtil.Add(DateTimeUtil.ToUniversalTime(expires), this._securityTokenServiceConfiguration.MaxClockSkew) < utcNow)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidRequestException(SR.GetString("ID2075", new object[]
				{
					created,
					expires,
					utcNow
				})));
			}
			if (DateTimeUtil.ToUniversalTime(created) > DateTimeUtil.Add(utcNow + TimeSpan.FromDays(1.0), this._securityTokenServiceConfiguration.MaxClockSkew))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidRequestException(SR.GetString("ID2076", new object[]
				{
					created,
					expires,
					utcNow
				})));
			}
			if (expires <= created)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidRequestException(SR.GetString("ID2077", new object[]
				{
					created,
					expires
				})));
			}
			if (expires - created > this._securityTokenServiceConfiguration.MaximumTokenLifetime)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidRequestException(SR.GetString("ID2078", new object[]
				{
					created,
					expires,
					this._securityTokenServiceConfiguration.MaximumTokenLifetime
				})));
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000E670 File Offset: 0x0000C870
		protected virtual RequestSecurityTokenResponse GetResponse(RequestSecurityToken request, SecurityTokenDescriptor tokenDescriptor)
		{
			if (tokenDescriptor != null)
			{
				RequestSecurityTokenResponse requestSecurityTokenResponse = new RequestSecurityTokenResponse(request);
				tokenDescriptor.ApplyTo(requestSecurityTokenResponse);
				if (request.ReplyTo != null)
				{
					requestSecurityTokenResponse.ReplyTo = tokenDescriptor.ReplyToAddress;
				}
				if (!string.IsNullOrEmpty(tokenDescriptor.AppliesToAddress))
				{
					requestSecurityTokenResponse.AppliesTo = new EndpointReference(tokenDescriptor.AppliesToAddress);
				}
				return requestSecurityTokenResponse;
			}
			return null;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000E6C3 File Offset: 0x0000C8C3
		public virtual RequestSecurityTokenResponse EndCancel(IAsyncResult result)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidRequestException(SR.GetString("ID3141", new object[]
			{
				"Cancel"
			})));
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000DF9E File Offset: 0x0000C19E
		protected virtual Scope EndGetScope(IAsyncResult result)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException(SR.GetString("ID2081")));
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000E6EC File Offset: 0x0000C8EC
		public virtual RequestSecurityTokenResponse EndIssue(IAsyncResult result)
		{
			if (result == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("result");
			}
			if (!(result is TypedAsyncResult<RequestSecurityTokenResponse>))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID2012", new object[]
				{
					typeof(TypedAsyncResult<RequestSecurityTokenResponse>),
					result.GetType()
				})));
			}
			return TypedAsyncResult<RequestSecurityTokenResponse>.End(result);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000E750 File Offset: 0x0000C950
		public virtual RequestSecurityTokenResponse EndRenew(IAsyncResult result)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidRequestException(SR.GetString("ID3141", new object[]
			{
				"Renew"
			})));
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000E779 File Offset: 0x0000C979
		public virtual RequestSecurityTokenResponse EndValidate(IAsyncResult result)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidRequestException(SR.GetString("ID3141", new object[]
			{
				"Validate"
			})));
		}

		// Token: 0x060003E0 RID: 992
		protected abstract Scope GetScope(ClaimsPrincipal principal, RequestSecurityToken request);

		// Token: 0x060003E1 RID: 993
		protected abstract ClaimsIdentity GetOutputClaimsIdentity(ClaimsPrincipal principal, RequestSecurityToken request, Scope scope);

		// Token: 0x060003E2 RID: 994 RVA: 0x0000DF9E File Offset: 0x0000C19E
		protected virtual IAsyncResult BeginGetOutputClaimsIdentity(ClaimsPrincipal principal, RequestSecurityToken request, Scope scope, AsyncCallback callback, object state)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException(SR.GetString("ID2081")));
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000DF9E File Offset: 0x0000C19E
		protected virtual ClaimsIdentity EndGetOutputClaimsIdentity(IAsyncResult result)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException(SR.GetString("ID2081")));
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000E7A4 File Offset: 0x0000C9A4
		public virtual RequestSecurityTokenResponse Issue(ClaimsPrincipal principal, RequestSecurityToken request)
		{
			if (request == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("request");
			}
			this._principal = principal;
			this._request = request;
			this.ValidateRequest(request);
			Scope scope = this.GetScope(principal, request);
			if (scope == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID2013"));
			}
			this.Scope = scope;
			this.SecurityTokenDescriptor = this.CreateSecurityTokenDescriptor(request, scope);
			if (this.SecurityTokenDescriptor == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID2003"));
			}
			if (this.SecurityTokenDescriptor.SigningCredentials == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID2079"));
			}
			if (this.Scope.TokenEncryptionRequired && this.SecurityTokenDescriptor.EncryptingCredentials == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4184"));
			}
			SecurityTokenHandler securityTokenHandler = this.GetSecurityTokenHandler(request.TokenType);
			if (securityTokenHandler == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("ID4010", new object[]
				{
					request.TokenType
				})));
			}
			this._tokenDescriptor.TokenIssuerName = this.GetValidIssuerName();
			this._tokenDescriptor.Lifetime = this.GetTokenLifetime(request.Lifetime);
			this._tokenDescriptor.Proof = this.GetProofToken(request, scope);
			this._tokenDescriptor.Subject = this.GetOutputClaimsIdentity(principal, request, scope);
			if (!string.IsNullOrEmpty(request.TokenType))
			{
				this._tokenDescriptor.TokenType = request.TokenType;
			}
			else
			{
				string[] tokenTypeIdentifiers = securityTokenHandler.GetTokenTypeIdentifiers();
				if (tokenTypeIdentifiers == null || tokenTypeIdentifiers.Length == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4264", new object[]
					{
						request.TokenType
					})));
				}
				this._tokenDescriptor.TokenType = tokenTypeIdentifiers[0];
			}
			this._tokenDescriptor.Token = securityTokenHandler.CreateToken(this._tokenDescriptor);
			this._tokenDescriptor.AttachedReference = securityTokenHandler.CreateSecurityTokenReference(this._tokenDescriptor.Token, true);
			this._tokenDescriptor.UnattachedReference = securityTokenHandler.CreateSecurityTokenReference(this._tokenDescriptor.Token, false);
			return this.GetResponse(request, this._tokenDescriptor);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000E9B8 File Offset: 0x0000CBB8
		protected virtual SecurityTokenHandler GetSecurityTokenHandler(string requestedTokenType)
		{
			string tokenTypeIdentifier = string.IsNullOrEmpty(requestedTokenType) ? this._securityTokenServiceConfiguration.DefaultTokenType : requestedTokenType;
			return this._securityTokenServiceConfiguration.SecurityTokenHandlers[tokenTypeIdentifier];
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000E9F0 File Offset: 0x0000CBF0
		private void OnGetScopeComplete(IAsyncResult result)
		{
			if (result == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("result");
			}
			SecurityTokenService.FederatedAsyncState federatedAsyncState = result.AsyncState as SecurityTokenService.FederatedAsyncState;
			if (federatedAsyncState == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID2001")));
			}
			Exception ex = null;
			TypedAsyncResult<RequestSecurityTokenResponse> typedAsyncResult = federatedAsyncState.Result as TypedAsyncResult<RequestSecurityTokenResponse>;
			if (typedAsyncResult == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID2004", new object[]
				{
					typeof(TypedAsyncResult<RequestSecurityTokenResponse>),
					federatedAsyncState.Result.GetType()
				})));
			}
			RequestSecurityToken request = federatedAsyncState.Request;
			try
			{
				Scope scope = this.EndGetScope(result);
				if (scope == null)
				{
					throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID2013"));
				}
				this.Scope = scope;
				this.SecurityTokenDescriptor = this.CreateSecurityTokenDescriptor(request, this.Scope);
				if (this.SecurityTokenDescriptor == null)
				{
					throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID2003"));
				}
				if (this.SecurityTokenDescriptor.SigningCredentials == null)
				{
					throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID2079"));
				}
				if (this.Scope.TokenEncryptionRequired && this.SecurityTokenDescriptor.EncryptingCredentials == null)
				{
					throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4184"));
				}
				SecurityTokenHandler securityTokenHandler = this.GetSecurityTokenHandler((request == null) ? null : request.TokenType);
				if (securityTokenHandler == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("ID4010", new object[]
					{
						(request == null) ? string.Empty : request.TokenType
					})));
				}
				federatedAsyncState.SecurityTokenHandler = securityTokenHandler;
				this._tokenDescriptor.TokenIssuerName = this.GetValidIssuerName();
				this._tokenDescriptor.Lifetime = this.GetTokenLifetime((request == null) ? null : request.Lifetime);
				this._tokenDescriptor.Proof = this.GetProofToken(request, this.Scope);
				this.BeginGetOutputClaimsIdentity(federatedAsyncState.ClaimsPrincipal, federatedAsyncState.Request, scope, new AsyncCallback(this.OnGetOutputClaimsIdentityComplete), federatedAsyncState);
			}
			catch (Exception ex2)
			{
				if (Fx.IsFatal(ex2))
				{
					throw;
				}
				ex = ex2;
			}
			if (ex != null)
			{
				typedAsyncResult.Complete(null, result.CompletedSynchronously, ex);
			}
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000EC20 File Offset: 0x0000CE20
		private void OnGetOutputClaimsIdentityComplete(IAsyncResult result)
		{
			if (result == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("result");
			}
			SecurityTokenService.FederatedAsyncState federatedAsyncState = result.AsyncState as SecurityTokenService.FederatedAsyncState;
			if (federatedAsyncState == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID2001")));
			}
			SecurityTokenHandler securityTokenHandler = federatedAsyncState.SecurityTokenHandler;
			if (securityTokenHandler == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID2016")));
			}
			Exception exception = null;
			RequestSecurityToken request = federatedAsyncState.Request;
			RequestSecurityTokenResponse result2 = null;
			TypedAsyncResult<RequestSecurityTokenResponse> typedAsyncResult = federatedAsyncState.Result as TypedAsyncResult<RequestSecurityTokenResponse>;
			if (typedAsyncResult == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID2004", new object[]
				{
					typeof(TypedAsyncResult<RequestSecurityTokenResponse>),
					federatedAsyncState.Result.GetType()
				})));
			}
			try
			{
				if (this._tokenDescriptor == null)
				{
					throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID2003"));
				}
				this._tokenDescriptor.Subject = this.EndGetOutputClaimsIdentity(result);
				if (!string.IsNullOrEmpty(request.TokenType))
				{
					this._tokenDescriptor.TokenType = request.TokenType;
				}
				else
				{
					string[] tokenTypeIdentifiers = securityTokenHandler.GetTokenTypeIdentifiers();
					if (tokenTypeIdentifiers == null || tokenTypeIdentifiers.Length == 0)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4264", new object[]
						{
							request.TokenType
						})));
					}
					this._tokenDescriptor.TokenType = tokenTypeIdentifiers[0];
				}
				this._tokenDescriptor.Token = securityTokenHandler.CreateToken(this._tokenDescriptor);
				this._tokenDescriptor.AttachedReference = securityTokenHandler.CreateSecurityTokenReference(this._tokenDescriptor.Token, true);
				this._tokenDescriptor.UnattachedReference = securityTokenHandler.CreateSecurityTokenReference(this._tokenDescriptor.Token, false);
				result2 = this.GetResponse(request, this._tokenDescriptor);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				exception = ex;
			}
			typedAsyncResult.Complete(result2, typedAsyncResult.CompletedSynchronously, exception);
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0000EE10 File Offset: 0x0000D010
		public SecurityTokenServiceConfiguration SecurityTokenServiceConfiguration
		{
			get
			{
				return this._securityTokenServiceConfiguration;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000EE18 File Offset: 0x0000D018
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x0000EE20 File Offset: 0x0000D020
		public ClaimsPrincipal Principal
		{
			get
			{
				return this._principal;
			}
			set
			{
				this._principal = value;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000EE29 File Offset: 0x0000D029
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x0000EE31 File Offset: 0x0000D031
		public RequestSecurityToken Request
		{
			get
			{
				return this._request;
			}
			set
			{
				this._request = value;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000EE3A File Offset: 0x0000D03A
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x0000EE42 File Offset: 0x0000D042
		public Scope Scope { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000EE4B File Offset: 0x0000D04B
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x0000EE53 File Offset: 0x0000D053
		protected SecurityTokenDescriptor SecurityTokenDescriptor
		{
			get
			{
				return this._tokenDescriptor;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this._tokenDescriptor = value;
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000E01D File Offset: 0x0000C21D
		public virtual RequestSecurityTokenResponse Renew(ClaimsPrincipal principal, RequestSecurityToken request)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidRequestException(SR.GetString("ID3141", new object[]
			{
				(request != null && request.RequestType != null) ? request.RequestType : "Renew"
			})));
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000E059 File Offset: 0x0000C259
		public virtual RequestSecurityTokenResponse Validate(ClaimsPrincipal principal, RequestSecurityToken request)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidRequestException(SR.GetString("ID3141", new object[]
			{
				(request != null && request.RequestType != null) ? request.RequestType : "Validate"
			})));
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000EE70 File Offset: 0x0000D070
		protected virtual void ValidateRequest(RequestSecurityToken request)
		{
			if (request == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidRequestException(SR.GetString("ID2051")));
			}
			if (request.RequestType != null && request.RequestType != "http://schemas.microsoft.com/idfx/requesttype/issue")
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidRequestException(SR.GetString("ID2052")));
			}
			if (request.KeyType != null && !SecurityTokenService.IsKnownType(request.KeyType))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidRequestException(SR.GetString("ID2053")));
			}
			if (StringComparer.Ordinal.Equals(request.KeyType, "http://schemas.microsoft.com/idfx/keytype/bearer") && request.KeySizeInBits != null && request.KeySizeInBits.Value != 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidRequestException(SR.GetString("ID2050")));
			}
			if (this.GetSecurityTokenHandler(request.TokenType) == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new UnsupportedTokenTypeBadRequestException(request.TokenType));
			}
			request.KeyType = (string.IsNullOrEmpty(request.KeyType) ? "http://schemas.microsoft.com/idfx/keytype/symmetric" : request.KeyType);
			if (StringComparer.Ordinal.Equals(request.KeyType, "http://schemas.microsoft.com/idfx/keytype/symmetric"))
			{
				if (request.KeySizeInBits != null)
				{
					if (request.KeySizeInBits.Value > this._securityTokenServiceConfiguration.DefaultMaxSymmetricKeySizeInBits)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidRequestException(SR.GetString("ID2056", new object[]
						{
							request.KeySizeInBits.Value,
							this._securityTokenServiceConfiguration.DefaultMaxSymmetricKeySizeInBits
						})));
					}
				}
				else
				{
					request.KeySizeInBits = new int?(this._securityTokenServiceConfiguration.DefaultSymmetricKeySizeInBits);
				}
			}
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000F036 File Offset: 0x0000D236
		private static bool IsKnownType(string keyType)
		{
			return StringComparer.Ordinal.Equals(keyType, "http://schemas.microsoft.com/idfx/keytype/symmetric") || StringComparer.Ordinal.Equals(keyType, "http://schemas.microsoft.com/idfx/keytype/asymmetric") || StringComparer.Ordinal.Equals(keyType, "http://schemas.microsoft.com/idfx/keytype/bearer");
		}

		// Token: 0x04000376 RID: 886
		private SecurityTokenServiceConfiguration _securityTokenServiceConfiguration;

		// Token: 0x04000377 RID: 887
		private ClaimsPrincipal _principal;

		// Token: 0x04000378 RID: 888
		private RequestSecurityToken _request;

		// Token: 0x04000379 RID: 889
		private SecurityTokenDescriptor _tokenDescriptor;

		// Token: 0x02000239 RID: 569
		protected class FederatedAsyncState
		{
			// Token: 0x06001214 RID: 4628 RVA: 0x0004F834 File Offset: 0x0004DA34
			public FederatedAsyncState(SecurityTokenService.FederatedAsyncState federatedAsyncState)
			{
				if (federatedAsyncState == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("FederatedAsyncState");
				}
				this._request = federatedAsyncState.Request;
				this._claimsPrincipal = federatedAsyncState.ClaimsPrincipal;
				this._securityTokenHandler = federatedAsyncState.SecurityTokenHandler;
				this._result = federatedAsyncState.Result;
			}

			// Token: 0x06001215 RID: 4629 RVA: 0x0004F88C File Offset: 0x0004DA8C
			public FederatedAsyncState(RequestSecurityToken request, ClaimsPrincipal principal, IAsyncResult result)
			{
				if (request == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("request");
				}
				if (result == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("result");
				}
				this._request = request;
				this._claimsPrincipal = principal;
				this._result = result;
			}

			// Token: 0x17000502 RID: 1282
			// (get) Token: 0x06001216 RID: 4630 RVA: 0x0004F8DA File Offset: 0x0004DADA
			public RequestSecurityToken Request
			{
				get
				{
					return this._request;
				}
			}

			// Token: 0x17000503 RID: 1283
			// (get) Token: 0x06001217 RID: 4631 RVA: 0x0004F8E2 File Offset: 0x0004DAE2
			public ClaimsPrincipal ClaimsPrincipal
			{
				get
				{
					return this._claimsPrincipal;
				}
			}

			// Token: 0x17000504 RID: 1284
			// (get) Token: 0x06001218 RID: 4632 RVA: 0x0004F8EA File Offset: 0x0004DAEA
			// (set) Token: 0x06001219 RID: 4633 RVA: 0x0004F8F2 File Offset: 0x0004DAF2
			public SecurityTokenHandler SecurityTokenHandler
			{
				get
				{
					return this._securityTokenHandler;
				}
				set
				{
					this._securityTokenHandler = value;
				}
			}

			// Token: 0x17000505 RID: 1285
			// (get) Token: 0x0600121A RID: 4634 RVA: 0x0004F8FB File Offset: 0x0004DAFB
			public IAsyncResult Result
			{
				get
				{
					return this._result;
				}
			}

			// Token: 0x04000F58 RID: 3928
			private RequestSecurityToken _request;

			// Token: 0x04000F59 RID: 3929
			private ClaimsPrincipal _claimsPrincipal;

			// Token: 0x04000F5A RID: 3930
			private SecurityTokenHandler _securityTokenHandler;

			// Token: 0x04000F5B RID: 3931
			private IAsyncResult _result;
		}
	}
}
