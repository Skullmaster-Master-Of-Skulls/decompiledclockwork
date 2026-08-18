using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000012 RID: 18
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HttpContentFormDataExtensions
	{
		// Token: 0x06000091 RID: 145 RVA: 0x00003FE8 File Offset: 0x000021E8
		public static bool IsFormData(this HttpContent content)
		{
			if (content == null)
			{
				throw Error.ArgumentNull("content");
			}
			MediaTypeHeaderValue contentType = content.Headers.ContentType;
			return contentType != null && string.Equals("application/x-www-form-urlencoded", contentType.MediaType, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004025 File Offset: 0x00002225
		public static Task<NameValueCollection> ReadAsFormDataAsync(this HttpContent content)
		{
			return content.ReadAsFormDataAsync(CancellationToken.None);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004034 File Offset: 0x00002234
		public static Task<NameValueCollection> ReadAsFormDataAsync(this HttpContent content, CancellationToken cancellationToken)
		{
			if (content == null)
			{
				throw Error.ArgumentNull("content");
			}
			MediaTypeFormatter[] formatters = new MediaTypeFormatter[]
			{
				new FormUrlEncodedMediaTypeFormatter()
			};
			return HttpContentFormDataExtensions.ReadAsAsyncCore(content, formatters, cancellationToken);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004168 File Offset: 0x00002368
		private static async Task<NameValueCollection> ReadAsAsyncCore(HttpContent content, MediaTypeFormatter[] formatters, CancellationToken cancellationToken)
		{
			FormDataCollection formData = await content.ReadAsAsync(formatters, cancellationToken);
			return (formData == null) ? null : formData.ReadAsNameValueCollection();
		}

		// Token: 0x0400002C RID: 44
		private const string ApplicationFormUrlEncoded = "application/x-www-form-urlencoded";
	}
}
