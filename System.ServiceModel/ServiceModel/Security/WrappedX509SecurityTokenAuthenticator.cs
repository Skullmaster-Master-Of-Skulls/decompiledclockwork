using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Claims;

namespace System.ServiceModel.Security
{
	// Token: 0x02000367 RID: 871
	internal class WrappedX509SecurityTokenAuthenticator : X509SecurityTokenAuthenticator
	{
		// Token: 0x06001FEF RID: 8175 RVA: 0x000777EC File Offset: 0x000759EC
		public WrappedX509SecurityTokenAuthenticator(X509SecurityTokenHandler wrappedX509SecurityTokenHandler, ExceptionMapper exceptionMapper) : base(X509CertificateValidator.None, WrappedX509SecurityTokenAuthenticator.GetMapToWindowsSetting(wrappedX509SecurityTokenHandler), true)
		{
			if (wrappedX509SecurityTokenHandler == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("wrappedX509SecurityTokenHandler");
			}
			if (exceptionMapper == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("exceptionMapper");
			}
			this._wrappedX509SecurityTokenHandler = wrappedX509SecurityTokenHandler;
			this._exceptionMapper = exceptionMapper;
		}

		// Token: 0x06001FF0 RID: 8176 RVA: 0x00077840 File Offset: 0x00075A40
		protected override ReadOnlyCollection<IAuthorizationPolicy> ValidateTokenCore(SecurityToken token)
		{
			ReadOnlyCollection<ClaimsIdentity> readOnlyCollection = null;
			try
			{
				readOnlyCollection = this._wrappedX509SecurityTokenHandler.ValidateToken(token);
			}
			catch (Exception ex)
			{
				if (!this._exceptionMapper.HandleSecurityTokenProcessingException(ex))
				{
					throw;
				}
			}
			bool flag = SecurityTokenHandlerConfiguration.DefaultSaveBootstrapContext;
			if (this._wrappedX509SecurityTokenHandler.Configuration != null)
			{
				flag = this._wrappedX509SecurityTokenHandler.Configuration.SaveBootstrapContext;
			}
			if (flag)
			{
				X509SecurityToken x509SecurityToken = token as X509SecurityToken;
				SecurityToken token2;
				if (x509SecurityToken != null)
				{
					token2 = new X509SecurityToken(x509SecurityToken.Certificate);
				}
				else
				{
					token2 = token;
				}
				BootstrapContext bootstrapContext = new BootstrapContext(token2, this._wrappedX509SecurityTokenHandler);
				foreach (ClaimsIdentity claimsIdentity in readOnlyCollection)
				{
					claimsIdentity.BootstrapContext = bootstrapContext;
				}
			}
			return new List<IAuthorizationPolicy>(1)
			{
				new AuthorizationPolicy(readOnlyCollection)
			}.AsReadOnly();
		}

		// Token: 0x06001FF1 RID: 8177 RVA: 0x00077930 File Offset: 0x00075B30
		private static bool GetMapToWindowsSetting(X509SecurityTokenHandler securityTokenHandler)
		{
			if (securityTokenHandler == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("securityTokenHandler");
			}
			return securityTokenHandler.MapToWindows;
		}

		// Token: 0x04001F03 RID: 7939
		private X509SecurityTokenHandler _wrappedX509SecurityTokenHandler;

		// Token: 0x04001F04 RID: 7940
		private ExceptionMapper _exceptionMapper;
	}
}
