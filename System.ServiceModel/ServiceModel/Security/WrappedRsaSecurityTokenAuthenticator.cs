using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Claims;

namespace System.ServiceModel.Security
{
	// Token: 0x0200035F RID: 863
	internal class WrappedRsaSecurityTokenAuthenticator : RsaSecurityTokenAuthenticator
	{
		// Token: 0x06001FB6 RID: 8118 RVA: 0x00076E14 File Offset: 0x00075014
		public WrappedRsaSecurityTokenAuthenticator(RsaSecurityTokenHandler wrappedRsaSecurityTokenHandler, ExceptionMapper exceptionMapper)
		{
			if (wrappedRsaSecurityTokenHandler == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("wrappedRsaSecurityTokenHandler");
			}
			if (exceptionMapper == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("exceptionMapper");
			}
			this._wrappedRsaSecurityTokenHandler = wrappedRsaSecurityTokenHandler;
			this._exceptionMapper = exceptionMapper;
		}

		// Token: 0x06001FB7 RID: 8119 RVA: 0x00076E50 File Offset: 0x00075050
		protected override ReadOnlyCollection<IAuthorizationPolicy> ValidateTokenCore(SecurityToken token)
		{
			IEnumerable<ClaimsIdentity> identityCollection = null;
			try
			{
				identityCollection = this._wrappedRsaSecurityTokenHandler.ValidateToken(token);
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

		// Token: 0x04001EF2 RID: 7922
		private RsaSecurityTokenHandler _wrappedRsaSecurityTokenHandler;

		// Token: 0x04001EF3 RID: 7923
		private ExceptionMapper _exceptionMapper;
	}
}
