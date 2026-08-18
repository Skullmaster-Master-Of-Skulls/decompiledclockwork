using System;
using System.IO;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.Http.Internal;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000022 RID: 34
	public class PushStreamContent : HttpContent
	{
		// Token: 0x06000115 RID: 277 RVA: 0x000051B8 File Offset: 0x000033B8
		public PushStreamContent(Action<Stream, HttpContent, TransportContext> onStreamAvailable) : this(PushStreamContent.Taskify(onStreamAvailable), null)
		{
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000051C7 File Offset: 0x000033C7
		public PushStreamContent(Func<Stream, HttpContent, TransportContext, Task> onStreamAvailable) : this(onStreamAvailable, null)
		{
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000051D1 File Offset: 0x000033D1
		public PushStreamContent(Action<Stream, HttpContent, TransportContext> onStreamAvailable, string mediaType) : this(PushStreamContent.Taskify(onStreamAvailable), new MediaTypeHeaderValue(mediaType))
		{
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000051E5 File Offset: 0x000033E5
		public PushStreamContent(Func<Stream, HttpContent, TransportContext, Task> onStreamAvailable, string mediaType) : this(onStreamAvailable, new MediaTypeHeaderValue(mediaType))
		{
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000051F4 File Offset: 0x000033F4
		public PushStreamContent(Action<Stream, HttpContent, TransportContext> onStreamAvailable, MediaTypeHeaderValue mediaType) : this(PushStreamContent.Taskify(onStreamAvailable), mediaType)
		{
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005203 File Offset: 0x00003403
		public PushStreamContent(Func<Stream, HttpContent, TransportContext, Task> onStreamAvailable, MediaTypeHeaderValue mediaType)
		{
			if (onStreamAvailable == null)
			{
				throw Error.ArgumentNull("onStreamAvailable");
			}
			this._onStreamAvailable = onStreamAvailable;
			base.Headers.ContentType = (mediaType ?? MediaTypeConstants.ApplicationOctetStreamMediaType);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005254 File Offset: 0x00003454
		private static Func<Stream, HttpContent, TransportContext, Task> Taskify(Action<Stream, HttpContent, TransportContext> onStreamAvailable)
		{
			if (onStreamAvailable == null)
			{
				throw Error.ArgumentNull("onStreamAvailable");
			}
			return delegate(Stream stream, HttpContent content, TransportContext transportContext)
			{
				onStreamAvailable(stream, content, transportContext);
				return TaskHelpers.Completed();
			};
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000541C File Offset: 0x0000361C
		protected override async Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
			TaskCompletionSource<bool> serializeToStreamTask = new TaskCompletionSource<bool>();
			Stream wrappedStream = new PushStreamContent.CompleteTaskOnCloseStream(stream, serializeToStreamTask);
			await this._onStreamAvailable(wrappedStream, this, context);
			await serializeToStreamTask.Task;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00005472 File Offset: 0x00003672
		protected override bool TryComputeLength(out long length)
		{
			length = -1L;
			return false;
		}

		// Token: 0x0400004B RID: 75
		private readonly Func<Stream, HttpContent, TransportContext, Task> _onStreamAvailable;

		// Token: 0x02000023 RID: 35
		internal class CompleteTaskOnCloseStream : DelegatingStream
		{
			// Token: 0x0600011E RID: 286 RVA: 0x00005479 File Offset: 0x00003679
			public CompleteTaskOnCloseStream(Stream innerStream, TaskCompletionSource<bool> serializeToStreamTask) : base(innerStream)
			{
				this._serializeToStreamTask = serializeToStreamTask;
			}

			// Token: 0x0600011F RID: 287 RVA: 0x00005489 File Offset: 0x00003689
			public override void Close()
			{
				this._serializeToStreamTask.TrySetResult(true);
			}

			// Token: 0x0400004C RID: 76
			private TaskCompletionSource<bool> _serializeToStreamTask;
		}
	}
}
