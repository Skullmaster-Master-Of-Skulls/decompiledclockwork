using System;
using System.ComponentModel;
using System.Web.Helpers.AntiXsrf;
using System.Web.Helpers.Claims;
using System.Web.Mvc;
using System.Web.WebPages.Resources;

namespace System.Web.Helpers
{
	// Token: 0x02000038 RID: 56
	public static class AntiForgery
	{
		// Token: 0x06000188 RID: 392 RVA: 0x00005568 File Offset: 0x00003768
		private static AntiForgeryWorker CreateSingletonAntiForgeryWorker()
		{
			IAntiForgeryConfig config = new AntiForgeryConfigWrapper();
			IAntiForgeryTokenSerializer serializer = new AntiForgeryTokenSerializer(MachineKey45CryptoSystem.Instance);
			ITokenStore tokenStore = new AntiForgeryTokenStore(config, serializer);
			IClaimUidExtractor claimUidExtractor = new ClaimUidExtractor(config, ClaimsIdentityConverter.Default);
			ITokenValidator validator = new TokenValidator(config, claimUidExtractor);
			return new AntiForgeryWorker(serializer, config, tokenStore, validator);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000055B0 File Offset: 0x000037B0
		public static HtmlString GetHtml()
		{
			if (HttpContext.Current == null)
			{
				throw new ArgumentException(WebPageResources.HttpContextUnavailable);
			}
			TagBuilder formInputElement = AntiForgery._worker.GetFormInputElement(new HttpContextWrapper(HttpContext.Current));
			return formInputElement.ToHtmlString(TagRenderMode.SelfClosing);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000055EB File Offset: 0x000037EB
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static void GetTokens(string oldCookieToken, out string newCookieToken, out string formToken)
		{
			if (HttpContext.Current == null)
			{
				throw new ArgumentException(WebPageResources.HttpContextUnavailable);
			}
			AntiForgery._worker.GetTokens(new HttpContextWrapper(HttpContext.Current), oldCookieToken, out newCookieToken, out formToken);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00005618 File Offset: 0x00003818
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This method is deprecated. Use the GetHtml() method instead. To specify a custom domain for the generated cookie, use the <httpCookies> configuration element. To specify custom data to be embedded within the token, use the static AntiForgeryConfig.AdditionalDataProvider property.", true)]
		public static HtmlString GetHtml(HttpContextBase httpContext, string salt, string domain, string path)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			if (!string.IsNullOrEmpty(salt) || !string.IsNullOrEmpty(domain) || !string.IsNullOrEmpty(path))
			{
				throw new NotSupportedException("This method is deprecated. Use the GetHtml() method instead. To specify a custom domain for the generated cookie, use the <httpCookies> configuration element. To specify custom data to be embedded within the token, use the static AntiForgeryConfig.AdditionalDataProvider property.");
			}
			TagBuilder formInputElement = AntiForgery._worker.GetFormInputElement(httpContext);
			return formInputElement.ToHtmlString(TagRenderMode.SelfClosing);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00005669 File Offset: 0x00003869
		public static void Validate()
		{
			if (HttpContext.Current == null)
			{
				throw new ArgumentException(WebPageResources.HttpContextUnavailable);
			}
			AntiForgery._worker.Validate(new HttpContextWrapper(HttpContext.Current));
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00005691 File Offset: 0x00003891
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static void Validate(string cookieToken, string formToken)
		{
			if (HttpContext.Current == null)
			{
				throw new ArgumentException(WebPageResources.HttpContextUnavailable);
			}
			AntiForgery._worker.Validate(new HttpContextWrapper(HttpContext.Current), cookieToken, formToken);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000056BB File Offset: 0x000038BB
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This method is deprecated. Use the Validate() method instead.", true)]
		public static void Validate(HttpContextBase httpContext, string salt)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			if (!string.IsNullOrEmpty(salt))
			{
				throw new NotSupportedException("This method is deprecated. Use the Validate() method instead.");
			}
			AntiForgery._worker.Validate(httpContext);
		}

		// Token: 0x04000080 RID: 128
		private static readonly AntiForgeryWorker _worker = AntiForgery.CreateSingletonAntiForgeryWorker();
	}
}
