using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x02000362 RID: 866
	internal class WrappedSamlSecurityTokenAuthenticator : SecurityTokenAuthenticator
	{
		// Token: 0x06001FBD RID: 8125 RVA: 0x00077004 File Offset: 0x00075204
		public WrappedSamlSecurityTokenAuthenticator(WrappedSaml11SecurityTokenAuthenticator wrappedSaml11SecurityTokenAuthenticator, WrappedSaml2SecurityTokenAuthenticator wrappedSaml2SecurityTokenAuthenticator)
		{
			if (wrappedSaml11SecurityTokenAuthenticator == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("wrappedSaml11SecurityTokenAuthenticator");
			}
			if (wrappedSaml2SecurityTokenAuthenticator == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("wrappedSaml2SecurityTokenAuthenticator");
			}
			this._wrappedSaml11SecurityTokenAuthenticator = wrappedSaml11SecurityTokenAuthenticator;
			this._wrappedSaml2SecurityTokenAuthenticator = wrappedSaml2SecurityTokenAuthenticator;
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x00077040 File Offset: 0x00075240
		protected override bool CanValidateTokenCore(SecurityToken token)
		{
			return this._wrappedSaml11SecurityTokenAuthenticator.CanValidateToken(token) || this._wrappedSaml2SecurityTokenAuthenticator.CanValidateToken(token);
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x00077060 File Offset: 0x00075260
		protected override ReadOnlyCollection<IAuthorizationPolicy> ValidateTokenCore(SecurityToken token)
		{
			if (this._wrappedSaml11SecurityTokenAuthenticator.CanValidateToken(token))
			{
				return this._wrappedSaml11SecurityTokenAuthenticator.ValidateToken(token);
			}
			if (this._wrappedSaml2SecurityTokenAuthenticator.CanValidateToken(token))
			{
				return this._wrappedSaml2SecurityTokenAuthenticator.ValidateToken(token);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("ID4101", new object[]
			{
				token.GetType().ToString()
			})));
		}

		// Token: 0x04001EF8 RID: 7928
		private WrappedSaml11SecurityTokenAuthenticator _wrappedSaml11SecurityTokenAuthenticator;

		// Token: 0x04001EF9 RID: 7929
		private WrappedSaml2SecurityTokenAuthenticator _wrappedSaml2SecurityTokenAuthenticator;
	}
}
