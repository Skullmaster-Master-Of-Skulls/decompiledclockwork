using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http.Handlers
{
	// Token: 0x02000028 RID: 40
	public class ProgressMessageHandler : DelegatingHandler
	{
		// Token: 0x0600012E RID: 302 RVA: 0x00005792 File Offset: 0x00003992
		public ProgressMessageHandler()
		{
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000579A File Offset: 0x0000399A
		public ProgressMessageHandler(HttpMessageHandler innerHandler) : base(innerHandler)
		{
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000130 RID: 304 RVA: 0x000057A4 File Offset: 0x000039A4
		// (remove) Token: 0x06000131 RID: 305 RVA: 0x000057DC File Offset: 0x000039DC
		public event EventHandler<HttpProgressEventArgs> HttpSendProgress;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000132 RID: 306 RVA: 0x00005814 File Offset: 0x00003A14
		// (remove) Token: 0x06000133 RID: 307 RVA: 0x0000584C File Offset: 0x00003A4C
		public event EventHandler<HttpProgressEventArgs> HttpReceiveProgress;

		// Token: 0x06000134 RID: 308 RVA: 0x00005A4C File Offset: 0x00003C4C
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			this.AddRequestProgress(request);
			HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
			if (this.HttpReceiveProgress != null && response != null && response.Content != null)
			{
				cancellationToken.ThrowIfCancellationRequested();
				await this.AddResponseProgressAsync(request, response);
			}
			return response;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005AA2 File Offset: 0x00003CA2
		protected internal virtual void OnHttpRequestProgress(HttpRequestMessage request, HttpProgressEventArgs e)
		{
			if (this.HttpSendProgress != null)
			{
				this.HttpSendProgress(request, e);
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00005AB9 File Offset: 0x00003CB9
		protected internal virtual void OnHttpResponseProgress(HttpRequestMessage request, HttpProgressEventArgs e)
		{
			if (this.HttpReceiveProgress != null)
			{
				this.HttpReceiveProgress(request, e);
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00005AD0 File Offset: 0x00003CD0
		private void AddRequestProgress(HttpRequestMessage request)
		{
			if (this.HttpSendProgress != null && request != null && request.Content != null)
			{
				HttpContent content = new ProgressContent(request.Content, this, request);
				request.Content = content;
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005C58 File Offset: 0x00003E58
		private async Task<HttpResponseMessage> AddResponseProgressAsync(HttpRequestMessage request, HttpResponseMessage response)
		{
			Stream stream = await response.Content.ReadAsStreamAsync();
			ProgressStream progressStream = new ProgressStream(stream, this, request, response);
			HttpContent progressContent = new StreamContent(progressStream);
			response.Content.Headers.CopyTo(progressContent.Headers);
			response.Content = progressContent;
			return response;
		}
	}
}
