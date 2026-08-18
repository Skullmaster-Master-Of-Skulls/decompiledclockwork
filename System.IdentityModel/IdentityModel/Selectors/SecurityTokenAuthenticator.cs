using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Diagnostics.Application;
using System.IdentityModel.Policy;
using System.IdentityModel.Tokens;
using System.Runtime.Diagnostics;

namespace System.IdentityModel.Selectors
{
	// Token: 0x020001A5 RID: 421
	public abstract class SecurityTokenAuthenticator
	{
		// Token: 0x06000DAB RID: 3499 RVA: 0x0003F484 File Offset: 0x0003D684
		public bool CanValidateToken(SecurityToken token)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			return this.CanValidateTokenCore(token);
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x0003F4A0 File Offset: 0x0003D6A0
		public ReadOnlyCollection<IAuthorizationPolicy> ValidateToken(SecurityToken token)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			if (!this.CanValidateToken(token))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenValidationException(SR.GetString("CannotValidateSecurityTokenType", new object[]
				{
					this,
					token.GetType()
				})));
			}
			EventTraceActivity eventTraceActivity = null;
			string text = null;
			if (TD.TokenValidationStartedIsEnabled())
			{
				eventTraceActivity = (eventTraceActivity ?? EventTraceActivity.GetFromThreadOrCreate(false));
				text = (text ?? token.GetType().ToString());
				TD.TokenValidationStarted(eventTraceActivity, text, token.Id);
			}
			ReadOnlyCollection<IAuthorizationPolicy> readOnlyCollection = this.ValidateTokenCore(token);
			if (readOnlyCollection == null)
			{
				string @string = SR.GetString("CannotValidateSecurityTokenType", new object[]
				{
					this,
					token.GetType()
				});
				if (TD.TokenValidationFailureIsEnabled())
				{
					eventTraceActivity = (eventTraceActivity ?? EventTraceActivity.GetFromThreadOrCreate(false));
					text = (text ?? token.GetType().ToString());
					TD.TokenValidationFailure(eventTraceActivity, text, token.Id, @string);
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenValidationException(@string));
			}
			if (TD.TokenValidationSuccessIsEnabled())
			{
				eventTraceActivity = (eventTraceActivity ?? EventTraceActivity.GetFromThreadOrCreate(false));
				text = (text ?? token.GetType().ToString());
				TD.TokenValidationSuccess(eventTraceActivity, text, token.Id);
			}
			return readOnlyCollection;
		}

		// Token: 0x06000DAD RID: 3501
		protected abstract bool CanValidateTokenCore(SecurityToken token);

		// Token: 0x06000DAE RID: 3502
		protected abstract ReadOnlyCollection<IAuthorizationPolicy> ValidateTokenCore(SecurityToken token);
	}
}
