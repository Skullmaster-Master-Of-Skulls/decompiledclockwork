using System;
using System.Net.Http;
using System.Web.Http.Hosting;

namespace System.Web.Http.WebHost
{
	// Token: 0x02000013 RID: 19
	public class WebHostBufferPolicySelector : IHostBufferPolicySelector
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00003867 File Offset: 0x00001A67
		public virtual bool UseBufferedInputStream(object hostContext)
		{
			if (hostContext == null)
			{
				throw Error.ArgumentNull("hostContext");
			}
			return true;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003878 File Offset: 0x00001A78
		public virtual bool UseBufferedOutputStream(HttpResponseMessage response)
		{
			if (response == null)
			{
				throw Error.ArgumentNull("response");
			}
			HttpContent content = response.Content;
			if (content != null)
			{
				long? contentLength = content.Headers.ContentLength;
				return (contentLength == null || contentLength.Value < 0L) && !(content is StreamContent) && !(content is PushStreamContent);
			}
			return false;
		}
	}
}
