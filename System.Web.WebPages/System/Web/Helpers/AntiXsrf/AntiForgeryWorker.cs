using System;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.WebPages.Resources;

namespace System.Web.Helpers.AntiXsrf
{
	// Token: 0x0200002D RID: 45
	internal sealed class AntiForgeryWorker
	{
		// Token: 0x0600013A RID: 314 RVA: 0x00004B34 File Offset: 0x00002D34
		internal AntiForgeryWorker(IAntiForgeryTokenSerializer serializer, IAntiForgeryConfig config, ITokenStore tokenStore, ITokenValidator validator)
		{
			this._serializer = serializer;
			this._config = config;
			this._tokenStore = tokenStore;
			this._validator = validator;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00004B59 File Offset: 0x00002D59
		private void CheckSSLConfig(HttpContextBase httpContext)
		{
			if (this._config.RequireSSL && !httpContext.Request.IsSecureConnection)
			{
				throw new InvalidOperationException(WebPageResources.AntiForgeryWorker_RequireSSL);
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00004B80 File Offset: 0x00002D80
		private AntiForgeryToken DeserializeToken(string serializedToken)
		{
			if (string.IsNullOrEmpty(serializedToken))
			{
				return null;
			}
			return this._serializer.Deserialize(serializedToken);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00004B98 File Offset: 0x00002D98
		private AntiForgeryToken DeserializeTokenNoThrow(string serializedToken)
		{
			AntiForgeryToken result;
			try
			{
				result = this.DeserializeToken(serializedToken);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004BC8 File Offset: 0x00002DC8
		private static IIdentity ExtractIdentity(HttpContextBase httpContext)
		{
			if (httpContext != null)
			{
				IPrincipal user = httpContext.User;
				if (user != null)
				{
					return user.Identity;
				}
			}
			return null;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004BEC File Offset: 0x00002DEC
		private AntiForgeryToken GetCookieTokenNoThrow(HttpContextBase httpContext)
		{
			AntiForgeryToken result;
			try
			{
				result = this._tokenStore.GetCookieToken(httpContext);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00004C20 File Offset: 0x00002E20
		public TagBuilder GetFormInputElement(HttpContextBase httpContext)
		{
			this.CheckSSLConfig(httpContext);
			AntiForgeryToken cookieTokenNoThrow = this.GetCookieTokenNoThrow(httpContext);
			AntiForgeryToken antiForgeryToken;
			AntiForgeryToken token;
			this.GetTokens(httpContext, cookieTokenNoThrow, out antiForgeryToken, out token);
			if (antiForgeryToken != null)
			{
				this._tokenStore.SaveCookieToken(httpContext, antiForgeryToken);
			}
			if (!this._config.SuppressXFrameOptionsHeader)
			{
				httpContext.Response.AddHeader("X-Frame-Options", "SAMEORIGIN");
			}
			TagBuilder tagBuilder = new TagBuilder("input");
			tagBuilder.Attributes["type"] = "hidden";
			tagBuilder.Attributes["name"] = this._config.FormFieldName;
			tagBuilder.Attributes["value"] = this._serializer.Serialize(token);
			return tagBuilder;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004CD4 File Offset: 0x00002ED4
		public void GetTokens(HttpContextBase httpContext, string serializedOldCookieToken, out string serializedNewCookieToken, out string serializedFormToken)
		{
			this.CheckSSLConfig(httpContext);
			AntiForgeryToken oldCookieToken = this.DeserializeTokenNoThrow(serializedOldCookieToken);
			AntiForgeryToken token;
			AntiForgeryToken token2;
			this.GetTokens(httpContext, oldCookieToken, out token, out token2);
			serializedNewCookieToken = this.Serialize(token);
			serializedFormToken = this.Serialize(token2);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004D10 File Offset: 0x00002F10
		private void GetTokens(HttpContextBase httpContext, AntiForgeryToken oldCookieToken, out AntiForgeryToken newCookieToken, out AntiForgeryToken formToken)
		{
			newCookieToken = null;
			if (!this._validator.IsCookieTokenValid(oldCookieToken))
			{
				AntiForgeryToken antiForgeryToken;
				newCookieToken = (antiForgeryToken = this._validator.GenerateCookieToken());
				oldCookieToken = antiForgeryToken;
			}
			formToken = this._validator.GenerateFormToken(httpContext, AntiForgeryWorker.ExtractIdentity(httpContext), oldCookieToken);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00004D56 File Offset: 0x00002F56
		private string Serialize(AntiForgeryToken token)
		{
			if (token == null)
			{
				return null;
			}
			return this._serializer.Serialize(token);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004D6C File Offset: 0x00002F6C
		public void Validate(HttpContextBase httpContext)
		{
			this.CheckSSLConfig(httpContext);
			AntiForgeryToken cookieToken = this._tokenStore.GetCookieToken(httpContext);
			AntiForgeryToken formToken = this._tokenStore.GetFormToken(httpContext);
			this._validator.ValidateTokens(httpContext, AntiForgeryWorker.ExtractIdentity(httpContext), cookieToken, formToken);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00004DB0 File Offset: 0x00002FB0
		public void Validate(HttpContextBase httpContext, string cookieToken, string formToken)
		{
			this.CheckSSLConfig(httpContext);
			AntiForgeryToken cookieToken2 = this.DeserializeToken(cookieToken);
			AntiForgeryToken formToken2 = this.DeserializeToken(formToken);
			this._validator.ValidateTokens(httpContext, AntiForgeryWorker.ExtractIdentity(httpContext), cookieToken2, formToken2);
		}

		// Token: 0x04000062 RID: 98
		private readonly IAntiForgeryConfig _config;

		// Token: 0x04000063 RID: 99
		private readonly IAntiForgeryTokenSerializer _serializer;

		// Token: 0x04000064 RID: 100
		private readonly ITokenStore _tokenStore;

		// Token: 0x04000065 RID: 101
		private readonly ITokenValidator _validator;
	}
}
