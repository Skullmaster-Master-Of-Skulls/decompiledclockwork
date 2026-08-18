using System;
using System.Globalization;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	// Token: 0x02000014 RID: 20
	[__DynamicallyInvokable]
	public abstract class HttpContent : IDisposable
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00005322 File Offset: 0x00003522
		[__DynamicallyInvokable]
		public HttpContentHeaders Headers
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.headers == null)
				{
					this.headers = new HttpContentHeaders(new Func<long?>(this.GetComputedOrBufferLength));
				}
				return this.headers;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00005349 File Offset: 0x00003549
		private bool IsBuffered
		{
			get
			{
				return this.bufferedContent != null;
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005354 File Offset: 0x00003554
		[__DynamicallyInvokable]
		protected HttpContent()
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Http, this, ".ctor", null);
			}
			this.canCalculateLength = true;
			if (Logging.On)
			{
				Logging.Exit(Logging.Http, this, ".ctor", null);
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005394 File Offset: 0x00003594
		[__DynamicallyInvokable]
		public Task<string> ReadAsStringAsync()
		{
			this.CheckDisposed();
			TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
			this.LoadIntoBufferAsync().ContinueWithStandard(delegate(Task task)
			{
				if (HttpUtilities.HandleFaultsAndCancelation<string>(task, tcs))
				{
					return;
				}
				if (this.bufferedContent.Length == 0L)
				{
					tcs.TrySetResult(string.Empty);
					return;
				}
				Encoding encoding = null;
				int num = -1;
				byte[] buffer = this.bufferedContent.GetBuffer();
				int num2 = (int)this.bufferedContent.Length;
				if (this.Headers.ContentType != null && this.Headers.ContentType.CharSet != null)
				{
					try
					{
						encoding = Encoding.GetEncoding(this.Headers.ContentType.CharSet);
					}
					catch (ArgumentException innerException)
					{
						tcs.TrySetException(new InvalidOperationException(SR.net_http_content_invalid_charset, innerException));
						return;
					}
				}
				if (encoding == null)
				{
					foreach (Encoding encoding2 in HttpContent.EncodingsWithBom)
					{
						byte[] preamble = encoding2.GetPreamble();
						if (HttpContent.ByteArrayHasPrefix(buffer, num2, preamble))
						{
							encoding = encoding2;
							num = preamble.Length;
							break;
						}
					}
				}
				encoding = (encoding ?? HttpContent.DefaultStringEncoding);
				if (num == -1)
				{
					byte[] preamble2 = encoding.GetPreamble();
					if (HttpContent.ByteArrayHasPrefix(buffer, num2, preamble2))
					{
						num = preamble2.Length;
					}
					else
					{
						num = 0;
					}
				}
				try
				{
					string @string = encoding.GetString(buffer, num, num2 - num);
					tcs.TrySetResult(@string);
				}
				catch (Exception exception)
				{
					tcs.TrySetException(exception);
				}
			});
			return tcs.Task;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000053E4 File Offset: 0x000035E4
		[__DynamicallyInvokable]
		public Task<byte[]> ReadAsByteArrayAsync()
		{
			this.CheckDisposed();
			TaskCompletionSource<byte[]> tcs = new TaskCompletionSource<byte[]>();
			this.LoadIntoBufferAsync().ContinueWithStandard(delegate(Task task)
			{
				if (!HttpUtilities.HandleFaultsAndCancelation<byte[]>(task, tcs))
				{
					tcs.TrySetResult(this.bufferedContent.ToArray());
				}
			});
			return tcs.Task;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005434 File Offset: 0x00003634
		[__DynamicallyInvokable]
		public Task<Stream> ReadAsStreamAsync()
		{
			this.CheckDisposed();
			TaskCompletionSource<Stream> tcs = new TaskCompletionSource<Stream>();
			if (this.contentReadStream == null && this.IsBuffered)
			{
				this.contentReadStream = new MemoryStream(this.bufferedContent.GetBuffer(), 0, (int)this.bufferedContent.Length, false, false);
			}
			if (this.contentReadStream != null)
			{
				tcs.TrySetResult(this.contentReadStream);
				return tcs.Task;
			}
			this.CreateContentReadStreamAsync().ContinueWithStandard(delegate(Task<Stream> task)
			{
				if (!HttpUtilities.HandleFaultsAndCancelation<Stream>(task, tcs))
				{
					this.contentReadStream = task.Result;
					tcs.TrySetResult(this.contentReadStream);
				}
			});
			return tcs.Task;
		}

		// Token: 0x0600010C RID: 268
		[__DynamicallyInvokable]
		protected abstract Task SerializeToStreamAsync(Stream stream, TransportContext context);

		// Token: 0x0600010D RID: 269 RVA: 0x000054E0 File Offset: 0x000036E0
		[__DynamicallyInvokable]
		public Task CopyToAsync(Stream stream, TransportContext context)
		{
			this.CheckDisposed();
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
			try
			{
				Task task;
				if (this.IsBuffered)
				{
					task = Task.Factory.FromAsync<byte[], int, int>(new Func<byte[], int, int, AsyncCallback, object, IAsyncResult>(stream.BeginWrite), new Action<IAsyncResult>(stream.EndWrite), this.bufferedContent.GetBuffer(), 0, (int)this.bufferedContent.Length, null);
				}
				else
				{
					task = this.SerializeToStreamAsync(stream, context);
					this.CheckTaskNotNull(task);
				}
				task.ContinueWithStandard(delegate(Task copyTask)
				{
					if (copyTask.IsFaulted)
					{
						tcs.TrySetException(HttpContent.GetStreamCopyException(copyTask.Exception.GetBaseException()));
						return;
					}
					if (copyTask.IsCanceled)
					{
						tcs.TrySetCanceled();
						return;
					}
					tcs.TrySetResult(null);
				});
			}
			catch (IOException originalException)
			{
				tcs.TrySetException(HttpContent.GetStreamCopyException(originalException));
			}
			catch (ObjectDisposedException originalException2)
			{
				tcs.TrySetException(HttpContent.GetStreamCopyException(originalException2));
			}
			return tcs.Task;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000055D4 File Offset: 0x000037D4
		[__DynamicallyInvokable]
		public Task CopyToAsync(Stream stream)
		{
			return this.CopyToAsync(stream, null);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000055DE File Offset: 0x000037DE
		internal void CopyTo(Stream stream)
		{
			this.CopyToAsync(stream).Wait();
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000055EC File Offset: 0x000037EC
		[__DynamicallyInvokable]
		public Task LoadIntoBufferAsync()
		{
			return this.LoadIntoBufferAsync(2147483647L);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000055FC File Offset: 0x000037FC
		[__DynamicallyInvokable]
		public Task LoadIntoBufferAsync(long maxBufferSize)
		{
			this.CheckDisposed();
			if (maxBufferSize > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("maxBufferSize", maxBufferSize, string.Format(CultureInfo.InvariantCulture, SR.net_http_content_buffersize_limit, new object[]
				{
					2147483647L
				}));
			}
			if (this.IsBuffered)
			{
				return HttpContent.CreateCompletedTask();
			}
			TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
			Exception exception = null;
			MemoryStream tempBuffer = this.CreateMemoryStream(maxBufferSize, out exception);
			if (tempBuffer == null)
			{
				tcs.TrySetException(exception);
			}
			else
			{
				try
				{
					Task task = this.SerializeToStreamAsync(tempBuffer, null);
					this.CheckTaskNotNull(task);
					task.ContinueWithStandard(delegate(Task copyTask)
					{
						try
						{
							if (copyTask.IsFaulted)
							{
								tempBuffer.Dispose();
								tcs.TrySetException(HttpContent.GetStreamCopyException(copyTask.Exception.GetBaseException()));
							}
							else if (copyTask.IsCanceled)
							{
								tempBuffer.Dispose();
								tcs.TrySetCanceled();
							}
							else
							{
								tempBuffer.Seek(0L, SeekOrigin.Begin);
								this.bufferedContent = tempBuffer;
								tcs.TrySetResult(null);
							}
						}
						catch (Exception ex)
						{
							tcs.TrySetException(ex);
							if (Logging.On)
							{
								Logging.Exception(Logging.Http, this, "LoadIntoBufferAsync", ex);
							}
						}
					});
				}
				catch (IOException originalException)
				{
					tcs.TrySetException(HttpContent.GetStreamCopyException(originalException));
				}
				catch (ObjectDisposedException originalException2)
				{
					tcs.TrySetException(HttpContent.GetStreamCopyException(originalException2));
				}
			}
			return tcs.Task;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005718 File Offset: 0x00003918
		[__DynamicallyInvokable]
		protected virtual Task<Stream> CreateContentReadStreamAsync()
		{
			TaskCompletionSource<Stream> tcs = new TaskCompletionSource<Stream>();
			this.LoadIntoBufferAsync().ContinueWithStandard(delegate(Task task)
			{
				if (!HttpUtilities.HandleFaultsAndCancelation<Stream>(task, tcs))
				{
					tcs.TrySetResult(this.bufferedContent);
				}
			});
			return tcs.Task;
		}

		// Token: 0x06000113 RID: 275
		[__DynamicallyInvokable]
		protected internal abstract bool TryComputeLength(out long length);

		// Token: 0x06000114 RID: 276 RVA: 0x00005760 File Offset: 0x00003960
		private long? GetComputedOrBufferLength()
		{
			this.CheckDisposed();
			if (this.IsBuffered)
			{
				return new long?(this.bufferedContent.Length);
			}
			if (this.canCalculateLength)
			{
				long value = 0L;
				if (this.TryComputeLength(out value))
				{
					return new long?(value);
				}
				this.canCalculateLength = false;
			}
			return null;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000057B8 File Offset: 0x000039B8
		private MemoryStream CreateMemoryStream(long maxBufferSize, out Exception error)
		{
			error = null;
			long? contentLength = this.Headers.ContentLength;
			if (contentLength == null)
			{
				return new HttpContent.LimitMemoryStream((int)maxBufferSize, 0);
			}
			long? num = contentLength;
			if (num.GetValueOrDefault() > maxBufferSize & num != null)
			{
				error = new HttpRequestException(string.Format(CultureInfo.InvariantCulture, SR.net_http_content_buffersize_exceeded, new object[]
				{
					maxBufferSize
				}));
				return null;
			}
			return new HttpContent.LimitMemoryStream((int)maxBufferSize, (int)contentLength.Value);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005834 File Offset: 0x00003A34
		[__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this.disposed)
			{
				this.disposed = true;
				if (this.contentReadStream != null)
				{
					this.contentReadStream.Dispose();
				}
				if (this.IsBuffered)
				{
					this.bufferedContent.Dispose();
				}
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000586E File Offset: 0x00003A6E
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000587D File Offset: 0x00003A7D
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005898 File Offset: 0x00003A98
		private void CheckTaskNotNull(Task task)
		{
			if (task == null)
			{
				if (Logging.On)
				{
					Logging.PrintError(Logging.Http, string.Format(CultureInfo.InvariantCulture, SR.net_http_log_content_no_task_returned_copytoasync, new object[]
					{
						base.GetType().FullName
					}));
				}
				throw new InvalidOperationException(SR.net_http_content_no_task_returned);
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000058E8 File Offset: 0x00003AE8
		private static Task CreateCompletedTask()
		{
			TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>();
			bool flag = taskCompletionSource.TrySetResult(null);
			return taskCompletionSource.Task;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000590C File Offset: 0x00003B0C
		private static Exception GetStreamCopyException(Exception originalException)
		{
			Exception ex = originalException;
			if (ex is IOException || ex is ObjectDisposedException)
			{
				ex = new HttpRequestException(SR.net_http_content_stream_copy_error, ex);
			}
			return ex;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005938 File Offset: 0x00003B38
		private static bool ByteArrayHasPrefix(byte[] byteArray, int dataLength, byte[] prefix)
		{
			if (prefix == null || byteArray == null || prefix.Length > dataLength || prefix.Length == 0)
			{
				return false;
			}
			for (int i = 0; i < prefix.Length; i++)
			{
				if (prefix[i] != byteArray[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400009C RID: 156
		private HttpContentHeaders headers;

		// Token: 0x0400009D RID: 157
		private MemoryStream bufferedContent;

		// Token: 0x0400009E RID: 158
		private bool disposed;

		// Token: 0x0400009F RID: 159
		private Stream contentReadStream;

		// Token: 0x040000A0 RID: 160
		private bool canCalculateLength;

		// Token: 0x040000A1 RID: 161
		internal const long MaxBufferSize = 2147483647L;

		// Token: 0x040000A2 RID: 162
		internal static readonly Encoding DefaultStringEncoding = Encoding.UTF8;

		// Token: 0x040000A3 RID: 163
		private static Encoding[] EncodingsWithBom = new Encoding[]
		{
			Encoding.UTF8,
			Encoding.UTF32,
			Encoding.Unicode,
			Encoding.BigEndianUnicode
		};

		// Token: 0x02000059 RID: 89
		private class LimitMemoryStream : MemoryStream
		{
			// Token: 0x06000437 RID: 1079 RVA: 0x0000FBAC File Offset: 0x0000DDAC
			public LimitMemoryStream(int maxSize, int capacity) : base(capacity)
			{
				this.maxSize = maxSize;
			}

			// Token: 0x06000438 RID: 1080 RVA: 0x0000FBBC File Offset: 0x0000DDBC
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				this.CheckSize(count);
				return base.BeginWrite(buffer, offset, count, callback, state);
			}

			// Token: 0x06000439 RID: 1081 RVA: 0x0000FBD2 File Offset: 0x0000DDD2
			public override void Write(byte[] buffer, int offset, int count)
			{
				this.CheckSize(count);
				base.Write(buffer, offset, count);
			}

			// Token: 0x0600043A RID: 1082 RVA: 0x0000FBE4 File Offset: 0x0000DDE4
			public override void WriteByte(byte value)
			{
				this.CheckSize(1);
				base.WriteByte(value);
			}

			// Token: 0x0600043B RID: 1083 RVA: 0x0000FBF4 File Offset: 0x0000DDF4
			public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			{
				this.CheckSize(count);
				return base.WriteAsync(buffer, offset, count, cancellationToken);
			}

			// Token: 0x0600043C RID: 1084 RVA: 0x0000FC08 File Offset: 0x0000DE08
			private void CheckSize(int countToAdd)
			{
				if ((long)this.maxSize - this.Length < (long)countToAdd)
				{
					throw new HttpRequestException(string.Format(CultureInfo.InvariantCulture, SR.net_http_content_buffersize_exceeded, new object[]
					{
						this.maxSize
					}));
				}
			}

			// Token: 0x040001B0 RID: 432
			private int maxSize;
		}
	}
}
