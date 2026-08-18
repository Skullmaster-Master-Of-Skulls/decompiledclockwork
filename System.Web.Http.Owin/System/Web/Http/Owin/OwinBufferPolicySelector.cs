using System;
using System.Net.Http;
using System.Web.Http.Hosting;

namespace System.Web.Http.Owin
{
	// Token: 0x02000012 RID: 18
	public class OwinBufferPolicySelector : IHostBufferPolicySelector
	{
		// Token: 0x06000080 RID: 128 RVA: 0x0000344B File Offset: 0x0000164B
		public bool UseBufferedInputStream(object hostContext)
		{
			return false;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003450 File Offset: 0x00001650
		public bool UseBufferedOutputStream(HttpResponseMessage response)
		{
			if (response == null)
			{
				throw Error.ArgumentNull("response");
			}
			HttpContent content = response.Content;
			if (content == null)
			{
				return false;
			}
			long? contentLength = content.Headers.ContentLength;
			if (contentLength != null && contentLength.Value >= 0L)
			{
				return false;
			}
			bool? transferEncodingChunked = response.Headers.TransferEncodingChunked;
			return (transferEncodingChunked == null || !transferEncodingChunked.Value) && !(content is StreamContent) && !(content is PushStreamContent);
		}
	}
}
