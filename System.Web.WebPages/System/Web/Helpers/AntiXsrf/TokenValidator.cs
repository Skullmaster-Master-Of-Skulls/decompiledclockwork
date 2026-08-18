using System;
using System.Globalization;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.WebPages.Resources;

namespace System.Web.Helpers.AntiXsrf
{
	// Token: 0x02000025 RID: 37
	internal sealed class TokenValidator : ITokenValidator
	{
		// Token: 0x0600010D RID: 269 RVA: 0x0000458C File Offset: 0x0000278C
		internal TokenValidator(IAntiForgeryConfig config, IClaimUidExtractor claimUidExtractor)
		{
			this._config = config;
			this._claimUidExtractor = claimUidExtractor;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000045A4 File Offset: 0x000027A4
		public AntiForgeryToken GenerateCookieToken()
		{
			return new AntiForgeryToken
			{
				IsSessionToken = true
			};
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000045C0 File Offset: 0x000027C0
		public AntiForgeryToken GenerateFormToken(HttpContextBase httpContext, IIdentity identity, AntiForgeryToken cookieToken)
		{
			AntiForgeryToken antiForgeryToken = new AntiForgeryToken
			{
				SecurityToken = cookieToken.SecurityToken,
				IsSessionToken = false
			};
			bool flag = false;
			if (identity != null && identity.IsAuthenticated)
			{
				if (!this._config.SuppressIdentityHeuristicChecks)
				{
					flag = true;
				}
				antiForgeryToken.ClaimUid = this._claimUidExtractor.ExtractClaimUid(identity);
				if (antiForgeryToken.ClaimUid == null)
				{
					antiForgeryToken.Username = identity.Name;
				}
			}
			if (this._config.AdditionalDataProvider != null)
			{
				antiForgeryToken.AdditionalData = this._config.AdditionalDataProvider.GetAdditionalData(httpContext);
			}
			if (flag && string.IsNullOrEmpty(antiForgeryToken.Username) && antiForgeryToken.ClaimUid == null && string.IsNullOrEmpty(antiForgeryToken.AdditionalData))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, WebPageResources.TokenValidator_AuthenticatedUserWithoutUsername, new object[]
				{
					identity.GetType()
				}));
			}
			return antiForgeryToken;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000469A File Offset: 0x0000289A
		public bool IsCookieTokenValid(AntiForgeryToken cookieToken)
		{
			return cookieToken != null && cookieToken.IsSessionToken;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000046A8 File Offset: 0x000028A8
		public void ValidateTokens(HttpContextBase httpContext, IIdentity identity, AntiForgeryToken sessionToken, AntiForgeryToken fieldToken)
		{
			if (sessionToken == null)
			{
				throw HttpAntiForgeryException.CreateCookieMissingException(this._config.CookieName);
			}
			if (fieldToken == null)
			{
				throw HttpAntiForgeryException.CreateFormFieldMissingException(this._config.FormFieldName);
			}
			if (!sessionToken.IsSessionToken || fieldToken.IsSessionToken)
			{
				throw HttpAntiForgeryException.CreateTokensSwappedException(this._config.CookieName, this._config.FormFieldName);
			}
			if (!object.Equals(sessionToken.SecurityToken, fieldToken.SecurityToken))
			{
				throw HttpAntiForgeryException.CreateSecurityTokenMismatchException();
			}
			string text = string.Empty;
			BinaryBlob binaryBlob = null;
			if (identity != null && identity.IsAuthenticated)
			{
				binaryBlob = this._claimUidExtractor.ExtractClaimUid(identity);
				if (binaryBlob == null)
				{
					text = (identity.Name ?? string.Empty);
				}
			}
			bool flag = text.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || text.StartsWith("https://", StringComparison.OrdinalIgnoreCase);
			if (!string.Equals(fieldToken.Username, text, flag ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase))
			{
				throw HttpAntiForgeryException.CreateUsernameMismatchException(fieldToken.Username, text);
			}
			if (!object.Equals(fieldToken.ClaimUid, binaryBlob))
			{
				throw HttpAntiForgeryException.CreateClaimUidMismatchException();
			}
			if (this._config.AdditionalDataProvider != null && !this._config.AdditionalDataProvider.ValidateAdditionalData(httpContext, fieldToken.AdditionalData))
			{
				throw HttpAntiForgeryException.CreateAdditionalDataCheckFailedException();
			}
		}

		// Token: 0x04000057 RID: 87
		private readonly IClaimUidExtractor _claimUidExtractor;

		// Token: 0x04000058 RID: 88
		private readonly IAntiForgeryConfig _config;
	}
}
