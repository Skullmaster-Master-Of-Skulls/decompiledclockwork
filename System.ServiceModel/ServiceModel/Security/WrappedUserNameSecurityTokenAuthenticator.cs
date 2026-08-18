using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Claims;

namespace System.ServiceModel.Security
{
	// Token: 0x02000366 RID: 870
	internal class WrappedUserNameSecurityTokenAuthenticator : UserNameSecurityTokenAuthenticator
	{
		// Token: 0x06001FEC RID: 8172 RVA: 0x00077720 File Offset: 0x00075920
		public WrappedUserNameSecurityTokenAuthenticator(UserNameSecurityTokenHandler wrappedUserNameSecurityTokenHandler, ExceptionMapper exceptionMapper)
		{
			if (wrappedUserNameSecurityTokenHandler == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("wrappedUserNameSecurityTokenHandler");
			}
			if (exceptionMapper == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("exceptionMapper");
			}
			this._wrappedUserNameSecurityTokenHandler = wrappedUserNameSecurityTokenHandler;
			this._exceptionMapper = exceptionMapper;
		}

		// Token: 0x06001FED RID: 8173 RVA: 0x0007775C File Offset: 0x0007595C
		protected override ReadOnlyCollection<IAuthorizationPolicy> ValidateTokenCore(SecurityToken token)
		{
			ReadOnlyCollection<ClaimsIdentity> identityCollection = null;
			try
			{
				identityCollection = this._wrappedUserNameSecurityTokenHandler.ValidateToken(token);
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

		// Token: 0x06001FEE RID: 8174 RVA: 0x000777B8 File Offset: 0x000759B8
		protected override ReadOnlyCollection<IAuthorizationPolicy> ValidateUserNamePasswordCore(string userName, string password)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException(SR.GetString("ID4008", new object[]
			{
				"WrappedUserNameSecurityTokenAuthenticator",
				"ValidateUserNamePasswordCore"
			})));
		}

		// Token: 0x04001F01 RID: 7937
		private UserNameSecurityTokenHandler _wrappedUserNameSecurityTokenHandler;

		// Token: 0x04001F02 RID: 7938
		private ExceptionMapper _exceptionMapper;
	}
}
