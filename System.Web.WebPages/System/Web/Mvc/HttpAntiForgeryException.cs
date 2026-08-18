using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Web.WebPages.Resources;

namespace System.Web.Mvc
{
	// Token: 0x02000062 RID: 98
	[TypeForwardedFrom("System.Web.Mvc, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	[Serializable]
	public sealed class HttpAntiForgeryException : HttpException
	{
		// Token: 0x06000266 RID: 614 RVA: 0x000099FA File Offset: 0x00007BFA
		public HttpAntiForgeryException()
		{
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00009A02 File Offset: 0x00007C02
		private HttpAntiForgeryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00009A0C File Offset: 0x00007C0C
		public HttpAntiForgeryException(string message) : base(message)
		{
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00009A15 File Offset: 0x00007C15
		private HttpAntiForgeryException(string message, params object[] args) : this(string.Format(CultureInfo.CurrentCulture, message, args))
		{
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00009A29 File Offset: 0x00007C29
		public HttpAntiForgeryException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00009A33 File Offset: 0x00007C33
		internal static HttpAntiForgeryException CreateAdditionalDataCheckFailedException()
		{
			return new HttpAntiForgeryException(WebPageResources.AntiForgeryToken_AdditionalDataCheckFailed);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00009A3F File Offset: 0x00007C3F
		internal static HttpAntiForgeryException CreateClaimUidMismatchException()
		{
			return new HttpAntiForgeryException(WebPageResources.AntiForgeryToken_ClaimUidMismatch);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00009A4C File Offset: 0x00007C4C
		internal static HttpAntiForgeryException CreateCookieMissingException(string cookieName)
		{
			return new HttpAntiForgeryException(WebPageResources.AntiForgeryToken_CookieMissing, new object[]
			{
				cookieName
			});
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00009A6F File Offset: 0x00007C6F
		internal static HttpAntiForgeryException CreateDeserializationFailedException()
		{
			return new HttpAntiForgeryException(WebPageResources.AntiForgeryToken_DeserializationFailed);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00009A7C File Offset: 0x00007C7C
		internal static HttpAntiForgeryException CreateFormFieldMissingException(string formFieldName)
		{
			return new HttpAntiForgeryException(WebPageResources.AntiForgeryToken_FormFieldMissing, new object[]
			{
				formFieldName
			});
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00009A9F File Offset: 0x00007C9F
		internal static HttpAntiForgeryException CreateSecurityTokenMismatchException()
		{
			return new HttpAntiForgeryException(WebPageResources.AntiForgeryToken_SecurityTokenMismatch);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00009AAC File Offset: 0x00007CAC
		internal static HttpAntiForgeryException CreateTokensSwappedException(string cookieName, string formFieldName)
		{
			return new HttpAntiForgeryException(WebPageResources.AntiForgeryToken_TokensSwapped, new object[]
			{
				cookieName,
				formFieldName
			});
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00009AD4 File Offset: 0x00007CD4
		internal static HttpAntiForgeryException CreateUsernameMismatchException(string usernameInToken, string currentUsername)
		{
			return new HttpAntiForgeryException(WebPageResources.AntiForgeryToken_UsernameMismatch, new object[]
			{
				usernameInToken,
				currentUsername
			});
		}
	}
}
