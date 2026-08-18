using System;
using System.IO;
using System.Threading.Tasks;

namespace System.Net.Http.Handlers
{
	// Token: 0x02000027 RID: 39
	internal class ProgressContent : HttpContent
	{
		// Token: 0x0600012A RID: 298 RVA: 0x000056E5 File Offset: 0x000038E5
		public ProgressContent(HttpContent innerContent, ProgressMessageHandler handler, HttpRequestMessage request)
		{
			this._innerContent = innerContent;
			this._handler = handler;
			this._request = request;
			innerContent.Headers.CopyTo(base.Headers);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005714 File Offset: 0x00003914
		protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
			ProgressStream stream2 = new ProgressStream(stream, this._handler, this._request, null);
			return this._innerContent.CopyToAsync(stream2);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00005744 File Offset: 0x00003944
		protected override bool TryComputeLength(out long length)
		{
			long? contentLength = this._innerContent.Headers.ContentLength;
			if (contentLength != null)
			{
				length = contentLength.Value;
				return true;
			}
			length = -1L;
			return false;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000577B File Offset: 0x0000397B
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				this._innerContent.Dispose();
			}
		}

		// Token: 0x0400004F RID: 79
		private readonly HttpContent _innerContent;

		// Token: 0x04000050 RID: 80
		private readonly ProgressMessageHandler _handler;

		// Token: 0x04000051 RID: 81
		private readonly HttpRequestMessage _request;
	}
}
