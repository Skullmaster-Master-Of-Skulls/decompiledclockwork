using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Claims;

namespace System.ServiceModel.Security
{
	// Token: 0x02000361 RID: 865
	internal class WrappedSaml2SecurityTokenAuthenticator : SecurityTokenAuthenticator
	{
		// Token: 0x06001FBA RID: 8122 RVA: 0x00076F54 File Offset: 0x00075154
		public WrappedSaml2SecurityTokenAuthenticator(Saml2SecurityTokenHandler saml2SecurityTokenHandler, ExceptionMapper exceptionMapper)
		{
			if (saml2SecurityTokenHandler == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("wrappedSaml2SecurityTokenHandler");
			}
			if (exceptionMapper == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("exceptionMapper");
			}
			this._wrappedSaml2SecurityTokenHandler = saml2SecurityTokenHandler;
			this._exceptionMapper = exceptionMapper;
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x00076F90 File Offset: 0x00075190
		protected override bool CanValidateTokenCore(SecurityToken token)
		{
			return token is Saml2SecurityToken && this._wrappedSaml2SecurityTokenHandler.CanValidateToken;
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x00076FA8 File Offset: 0x000751A8
		protected override ReadOnlyCollection<IAuthorizationPolicy> ValidateTokenCore(SecurityToken token)
		{
			IEnumerable<ClaimsIdentity> identityCollection = null;
			try
			{
				identityCollection = this._wrappedSaml2SecurityTokenHandler.ValidateToken(token);
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

		// Token: 0x04001EF6 RID: 7926
		private Saml2SecurityTokenHandler _wrappedSaml2SecurityTokenHandler;

		// Token: 0x04001EF7 RID: 7927
		private ExceptionMapper _exceptionMapper;
	}
}
