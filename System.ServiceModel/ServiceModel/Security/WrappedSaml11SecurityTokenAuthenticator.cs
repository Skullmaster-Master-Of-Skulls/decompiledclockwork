using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Claims;

namespace System.ServiceModel.Security
{
	// Token: 0x02000360 RID: 864
	internal class WrappedSaml11SecurityTokenAuthenticator : SamlSecurityTokenAuthenticator
	{
		// Token: 0x06001FB8 RID: 8120 RVA: 0x00076EAC File Offset: 0x000750AC
		public WrappedSaml11SecurityTokenAuthenticator(SamlSecurityTokenHandler saml11SecurityTokenHandler, ExceptionMapper exceptionMapper) : base(new List<SecurityTokenAuthenticator>())
		{
			if (saml11SecurityTokenHandler == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("wrappedSaml11SecurityTokenHandler");
			}
			if (exceptionMapper == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("exceptionMapper");
			}
			this._wrappedSaml11SecurityTokenHandler = saml11SecurityTokenHandler;
			this._exceptionMapper = exceptionMapper;
		}

		// Token: 0x06001FB9 RID: 8121 RVA: 0x00076EF8 File Offset: 0x000750F8
		protected override ReadOnlyCollection<IAuthorizationPolicy> ValidateTokenCore(SecurityToken token)
		{
			IEnumerable<ClaimsIdentity> identityCollection = null;
			try
			{
				identityCollection = this._wrappedSaml11SecurityTokenHandler.ValidateToken(token);
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

		// Token: 0x04001EF4 RID: 7924
		private SamlSecurityTokenHandler _wrappedSaml11SecurityTokenHandler;

		// Token: 0x04001EF5 RID: 7925
		private ExceptionMapper _exceptionMapper;
	}
}
