using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Claims;

namespace System.ServiceModel.Security
{
	// Token: 0x0200035D RID: 861
	internal class SecurityTokenAuthenticatorAdapter : SecurityTokenAuthenticator
	{
		// Token: 0x06001FA0 RID: 8096 RVA: 0x00076788 File Offset: 0x00074988
		public SecurityTokenAuthenticatorAdapter(SecurityTokenHandler securityTokenHandler, ExceptionMapper exceptionMapper)
		{
			if (securityTokenHandler == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("securityTokenHandler");
			}
			if (exceptionMapper == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("exceptionMapper");
			}
			this._securityTokenHandler = securityTokenHandler;
			this._exceptionMapper = exceptionMapper;
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x000767C4 File Offset: 0x000749C4
		protected override bool CanValidateTokenCore(SecurityToken token)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			return token.GetType() == this._securityTokenHandler.TokenType && this._securityTokenHandler.CanValidateToken;
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x00076800 File Offset: 0x00074A00
		protected sealed override ReadOnlyCollection<IAuthorizationPolicy> ValidateTokenCore(SecurityToken token)
		{
			IEnumerable<ClaimsIdentity> identityCollection = null;
			try
			{
				identityCollection = this._securityTokenHandler.ValidateToken(token);
			}
			catch (Exception ex)
			{
				if (!this._exceptionMapper.HandleSecurityTokenProcessingException(ex))
				{
					throw;
				}
			}
			return new List<IAuthorizationPolicy>(1)
			{
				new AuthorizationPolicy(identityCollection)
			}.AsReadOnly();
		}

		// Token: 0x04001EEC RID: 7916
		private SecurityTokenHandler _securityTokenHandler;

		// Token: 0x04001EED RID: 7917
		private ExceptionMapper _exceptionMapper;
	}
}
