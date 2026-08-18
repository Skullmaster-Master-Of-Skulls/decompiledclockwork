using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Internal;
using System.Net.Http.Properties;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x0200000B RID: 11
	public class ByteRangeStreamContent : HttpContent
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002D35 File Offset: 0x00000F35
		public ByteRangeStreamContent(Stream content, RangeHeaderValue range, string mediaType) : this(content, range, new MediaTypeHeaderValue(mediaType), 4096)
		{
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002D4A File Offset: 0x00000F4A
		public ByteRangeStreamContent(Stream content, RangeHeaderValue range, string mediaType, int bufferSize) : this(content, range, new MediaTypeHeaderValue(mediaType), bufferSize)
		{
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002D5C File Offset: 0x00000F5C
		public ByteRangeStreamContent(Stream content, RangeHeaderValue range, MediaTypeHeaderValue mediaType) : this(content, range, mediaType, 4096)
		{
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002D6C File Offset: 0x00000F6C
		public ByteRangeStreamContent(Stream content, RangeHeaderValue range, MediaTypeHeaderValue mediaType, int bufferSize)
		{
			if (content == null)
			{
				throw Error.ArgumentNull("content");
			}
			if (!content.CanSeek)
			{
				throw Error.Argument("content", Resources.ByteRangeStreamNotSeekable, new object[]
				{
					typeof(ByteRangeStreamContent).Name
				});
			}
			if (range == null)
			{
				throw Error.ArgumentNull("range");
			}
			if (mediaType == null)
			{
				throw Error.ArgumentNull("mediaType");
			}
			if (bufferSize < 1)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("bufferSize", bufferSize, 1);
			}
			if (!range.Unit.Equals("bytes", StringComparison.OrdinalIgnoreCase))
			{
				throw Error.Argument("range", Resources.ByteRangeStreamContentNotBytesRange, new object[]
				{
					range.Unit,
					"bytes"
				});
			}
			try
			{
				if (range.Ranges.Count <= 1)
				{
					if (range.Ranges.Count == 1)
					{
						try
						{
							ByteRangeStream byteRangeStream = new ByteRangeStream(content, range.Ranges.First<RangeItemHeaderValue>());
							this._byteRangeContent = new StreamContent(byteRangeStream, bufferSize);
							this._byteRangeContent.Headers.ContentType = mediaType;
							this._byteRangeContent.Headers.ContentRange = byteRangeStream.ContentRange;
							goto IL_23A;
						}
						catch (ArgumentOutOfRangeException)
						{
							ContentRangeHeaderValue contentRange = new ContentRangeHeaderValue(content.Length);
							string message = Error.Format(Resources.ByteRangeStreamNoOverlap, new object[]
							{
								range.ToString()
							});
							throw new InvalidByteRangeException(contentRange, message);
						}
					}
					throw Error.Argument("range", Resources.ByteRangeStreamContentNoRanges, new object[0]);
				}
				MultipartContent multipartContent = new MultipartContent("byteranges");
				this._byteRangeContent = multipartContent;
				foreach (RangeItemHeaderValue range2 in range.Ranges)
				{
					try
					{
						ByteRangeStream byteRangeStream2 = new ByteRangeStream(content, range2);
						multipartContent.Add(new StreamContent(byteRangeStream2, bufferSize)
						{
							Headers = 
							{
								ContentType = mediaType,
								ContentRange = byteRangeStream2.ContentRange
							}
						});
					}
					catch (ArgumentOutOfRangeException)
					{
					}
				}
				if (!multipartContent.Any<HttpContent>())
				{
					ContentRangeHeaderValue contentRange2 = new ContentRangeHeaderValue(content.Length);
					string message2 = Error.Format(Resources.ByteRangeStreamNoneOverlap, new object[]
					{
						range.ToString()
					});
					throw new InvalidByteRangeException(contentRange2, message2);
				}
				IL_23A:
				this._byteRangeContent.Headers.CopyTo(base.Headers);
				this._content = content;
				this._start = content.Position;
			}
			catch
			{
				if (this._byteRangeContent != null)
				{
					this._byteRangeContent.Dispose();
				}
				throw;
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003058 File Offset: 0x00001258
		protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
			this._content.Position = this._start;
			return this._byteRangeContent.CopyToAsync(stream);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003078 File Offset: 0x00001278
		protected override bool TryComputeLength(out long length)
		{
			long? contentLength = this._byteRangeContent.Headers.ContentLength;
			if (contentLength != null)
			{
				length = contentLength.Value;
				return true;
			}
			length = -1L;
			return false;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000030AF File Offset: 0x000012AF
		protected override void Dispose(bool disposing)
		{
			if (disposing && !this._disposed)
			{
				this._byteRangeContent.Dispose();
				this._content.Dispose();
				this._disposed = true;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0400000D RID: 13
		private const string SupportedRangeUnit = "bytes";

		// Token: 0x0400000E RID: 14
		private const string ByteRangesContentSubtype = "byteranges";

		// Token: 0x0400000F RID: 15
		private const int DefaultBufferSize = 4096;

		// Token: 0x04000010 RID: 16
		private const int MinBufferSize = 1;

		// Token: 0x04000011 RID: 17
		private readonly Stream _content;

		// Token: 0x04000012 RID: 18
		private readonly long _start;

		// Token: 0x04000013 RID: 19
		private readonly HttpContent _byteRangeContent;

		// Token: 0x04000014 RID: 20
		private bool _disposed;
	}
}
