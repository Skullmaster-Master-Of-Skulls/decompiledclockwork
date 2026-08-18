using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Net.Http.Headers;
using System.Net.Http.Properties;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000015 RID: 21
	internal static class MultipartFormDataStreamProviderHelper
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x000042D0 File Offset: 0x000024D0
		public static bool IsFileContent(HttpContent parent, HttpContentHeaders headers)
		{
			if (parent == null)
			{
				throw Error.ArgumentNull("parent");
			}
			if (headers == null)
			{
				throw Error.ArgumentNull("headers");
			}
			ContentDispositionHeaderValue contentDisposition = headers.ContentDisposition;
			if (contentDisposition == null)
			{
				throw Error.InvalidOperation(Resources.MultipartFormDataStreamProviderNoContentDisposition, new object[]
				{
					"Content-Disposition"
				});
			}
			return !string.IsNullOrEmpty(contentDisposition.FileName);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000044FC File Offset: 0x000026FC
		public static async Task ReadFormDataAsync(Collection<HttpContent> contents, NameValueCollection formData, CancellationToken cancellationToken)
		{
			foreach (HttpContent content in contents)
			{
				ContentDispositionHeaderValue contentDisposition = content.Headers.ContentDisposition;
				if (string.IsNullOrEmpty(contentDisposition.FileName))
				{
					string formFieldName = FormattingUtilities.UnquoteToken(contentDisposition.Name) ?? string.Empty;
					cancellationToken.ThrowIfCancellationRequested();
					string formFieldValue = await content.ReadAsStringAsync();
					formData.Add(formFieldName, formFieldValue);
				}
			}
		}
	}
}
