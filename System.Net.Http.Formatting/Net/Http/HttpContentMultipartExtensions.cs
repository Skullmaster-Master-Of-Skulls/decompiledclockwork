using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Http.Formatting.Parsers;
using System.Net.Http.Properties;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000056 RID: 86
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HttpContentMultipartExtensions
	{
		// Token: 0x06000330 RID: 816 RVA: 0x0000C335 File Offset: 0x0000A535
		public static bool IsMimeMultipartContent(this HttpContent content)
		{
			if (content == null)
			{
				throw Error.ArgumentNull("content");
			}
			return MimeMultipartBodyPartParser.IsMimeMultipartContent(content);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000C34C File Offset: 0x0000A54C
		public static bool IsMimeMultipartContent(this HttpContent content, string subtype)
		{
			if (string.IsNullOrWhiteSpace(subtype))
			{
				throw Error.ArgumentNull("subtype");
			}
			return content.IsMimeMultipartContent() && content.Headers.ContentType.MediaType.Equals("multipart/" + subtype, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000C39A File Offset: 0x0000A59A
		public static Task<MultipartMemoryStreamProvider> ReadAsMultipartAsync(this HttpContent content)
		{
			return content.ReadAsMultipartAsync(new MultipartMemoryStreamProvider(), 32768);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000C3AC File Offset: 0x0000A5AC
		public static Task<MultipartMemoryStreamProvider> ReadAsMultipartAsync(this HttpContent content, CancellationToken cancellationToken)
		{
			return content.ReadAsMultipartAsync(new MultipartMemoryStreamProvider(), 32768, cancellationToken);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000C3BF File Offset: 0x0000A5BF
		public static Task<T> ReadAsMultipartAsync<T>(this HttpContent content, T streamProvider) where T : MultipartStreamProvider
		{
			return content.ReadAsMultipartAsync(streamProvider, 32768);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000C3CD File Offset: 0x0000A5CD
		public static Task<T> ReadAsMultipartAsync<T>(this HttpContent content, T streamProvider, CancellationToken cancellationToken) where T : MultipartStreamProvider
		{
			return content.ReadAsMultipartAsync(streamProvider, 32768, cancellationToken);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000C3DC File Offset: 0x0000A5DC
		public static Task<T> ReadAsMultipartAsync<T>(this HttpContent content, T streamProvider, int bufferSize) where T : MultipartStreamProvider
		{
			return content.ReadAsMultipartAsync(streamProvider, bufferSize, CancellationToken.None);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000C708 File Offset: 0x0000A908
		public static async Task<T> ReadAsMultipartAsync<T>(this HttpContent content, T streamProvider, int bufferSize, CancellationToken cancellationToken) where T : MultipartStreamProvider
		{
			if (content == null)
			{
				throw Error.ArgumentNull("content");
			}
			if (streamProvider == null)
			{
				throw Error.ArgumentNull("streamProvider");
			}
			if (bufferSize < 256)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("bufferSize", bufferSize, 256);
			}
			Stream stream;
			try
			{
				stream = await content.ReadAsStreamAsync();
			}
			catch (Exception innerException)
			{
				throw new IOException(Resources.ReadAsMimeMultipartErrorReading, innerException);
			}
			T result;
			using (MimeMultipartBodyPartParser parser = new MimeMultipartBodyPartParser(content, streamProvider))
			{
				byte[] data = new byte[bufferSize];
				HttpContentMultipartExtensions.MultipartAsyncContext context = new HttpContentMultipartExtensions.MultipartAsyncContext(stream, parser, data, streamProvider.Contents);
				await HttpContentMultipartExtensions.MultipartReadAsync(context, cancellationToken);
				await streamProvider.ExecutePostProcessingAsync(cancellationToken);
				result = streamProvider;
			}
			return result;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000CAA0 File Offset: 0x0000ACA0
		private static async Task MultipartReadAsync(HttpContentMultipartExtensions.MultipartAsyncContext context, CancellationToken cancellationToken)
		{
			for (;;)
			{
				int bytesRead;
				try
				{
					bytesRead = await context.ContentStream.ReadAsync(context.Data, 0, context.Data.Length, cancellationToken);
				}
				catch (Exception innerException)
				{
					throw new IOException(Resources.ReadAsMimeMultipartErrorReading, innerException);
				}
				IEnumerable<MimeBodyPart> parts = context.MimeParser.ParseBuffer(context.Data, bytesRead);
				using (IEnumerator<MimeBodyPart> enumerator = parts.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						MimeBodyPart part = enumerator.Current;
						foreach (ArraySegment<byte> segment in part.Segments)
						{
							try
							{
								await part.WriteSegment(segment, cancellationToken);
							}
							catch (Exception innerException2)
							{
								part.Dispose();
								throw new IOException(Resources.ReadAsMimeMultipartErrorWriting, innerException2);
							}
						}
						if (HttpContentMultipartExtensions.CheckIsFinalPart(part, context.Result))
						{
							return;
						}
					}
					continue;
				}
				break;
			}
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000CAF0 File Offset: 0x0000ACF0
		private static bool CheckIsFinalPart(MimeBodyPart part, ICollection<HttpContent> result)
		{
			if (part.IsComplete)
			{
				HttpContent completedHttpContent = part.GetCompletedHttpContent();
				if (completedHttpContent != null)
				{
					result.Add(completedHttpContent);
				}
				bool isFinal = part.IsFinal;
				part.Dispose();
				return isFinal;
			}
			return false;
		}

		// Token: 0x040000E3 RID: 227
		private const int MinBufferSize = 256;

		// Token: 0x040000E4 RID: 228
		private const int DefaultBufferSize = 32768;

		// Token: 0x02000057 RID: 87
		private class MultipartAsyncContext
		{
			// Token: 0x0600033A RID: 826 RVA: 0x0000CB26 File Offset: 0x0000AD26
			public MultipartAsyncContext(Stream contentStream, MimeMultipartBodyPartParser mimeParser, byte[] data, ICollection<HttpContent> result)
			{
				this.ContentStream = contentStream;
				this.Result = result;
				this.MimeParser = mimeParser;
				this.Data = data;
			}

			// Token: 0x170000C1 RID: 193
			// (get) Token: 0x0600033B RID: 827 RVA: 0x0000CB4B File Offset: 0x0000AD4B
			// (set) Token: 0x0600033C RID: 828 RVA: 0x0000CB53 File Offset: 0x0000AD53
			public Stream ContentStream { get; private set; }

			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x0600033D RID: 829 RVA: 0x0000CB5C File Offset: 0x0000AD5C
			// (set) Token: 0x0600033E RID: 830 RVA: 0x0000CB64 File Offset: 0x0000AD64
			public ICollection<HttpContent> Result { get; private set; }

			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x0600033F RID: 831 RVA: 0x0000CB6D File Offset: 0x0000AD6D
			// (set) Token: 0x06000340 RID: 832 RVA: 0x0000CB75 File Offset: 0x0000AD75
			public byte[] Data { get; private set; }

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x06000341 RID: 833 RVA: 0x0000CB7E File Offset: 0x0000AD7E
			// (set) Token: 0x06000342 RID: 834 RVA: 0x0000CB86 File Offset: 0x0000AD86
			public MimeMultipartBodyPartParser MimeParser { get; private set; }
		}
	}
}
