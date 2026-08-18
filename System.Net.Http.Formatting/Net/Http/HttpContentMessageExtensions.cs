using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting.Parsers;
using System.Net.Http.Headers;
using System.Net.Http.Properties;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000055 RID: 85
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HttpContentMessageExtensions
	{
		// Token: 0x0600031A RID: 794 RVA: 0x0000B864 File Offset: 0x00009A64
		public static bool IsHttpRequestMessageContent(this HttpContent content)
		{
			if (content == null)
			{
				throw Error.ArgumentNull("content");
			}
			bool result;
			try
			{
				result = HttpMessageContent.ValidateHttpMessageContent(content, true, false);
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000B8A0 File Offset: 0x00009AA0
		public static bool IsHttpResponseMessageContent(this HttpContent content)
		{
			if (content == null)
			{
				throw Error.ArgumentNull("content");
			}
			bool result;
			try
			{
				result = HttpMessageContent.ValidateHttpMessageContent(content, false, false);
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000B8DC File Offset: 0x00009ADC
		public static Task<HttpRequestMessage> ReadAsHttpRequestMessageAsync(this HttpContent content)
		{
			return content.ReadAsHttpRequestMessageAsync("http", 32768);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000B8EE File Offset: 0x00009AEE
		public static Task<HttpRequestMessage> ReadAsHttpRequestMessageAsync(this HttpContent content, CancellationToken cancellationToken)
		{
			return content.ReadAsHttpRequestMessageAsync("http", 32768, cancellationToken);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000B901 File Offset: 0x00009B01
		public static Task<HttpRequestMessage> ReadAsHttpRequestMessageAsync(this HttpContent content, string uriScheme)
		{
			return content.ReadAsHttpRequestMessageAsync(uriScheme, 32768);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000B90F File Offset: 0x00009B0F
		public static Task<HttpRequestMessage> ReadAsHttpRequestMessageAsync(this HttpContent content, string uriScheme, CancellationToken cancellationToken)
		{
			return content.ReadAsHttpRequestMessageAsync(uriScheme, 32768, cancellationToken);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000B91E File Offset: 0x00009B1E
		public static Task<HttpRequestMessage> ReadAsHttpRequestMessageAsync(this HttpContent content, string uriScheme, int bufferSize)
		{
			return content.ReadAsHttpRequestMessageAsync(uriScheme, bufferSize, 16384);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000B92D File Offset: 0x00009B2D
		public static Task<HttpRequestMessage> ReadAsHttpRequestMessageAsync(this HttpContent content, string uriScheme, int bufferSize, CancellationToken cancellationToken)
		{
			return content.ReadAsHttpRequestMessageAsync(uriScheme, bufferSize, 16384, cancellationToken);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000B93D File Offset: 0x00009B3D
		public static Task<HttpRequestMessage> ReadAsHttpRequestMessageAsync(this HttpContent content, string uriScheme, int bufferSize, int maxHeaderSize)
		{
			return content.ReadAsHttpRequestMessageAsync(uriScheme, bufferSize, maxHeaderSize, CancellationToken.None);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000B950 File Offset: 0x00009B50
		public static Task<HttpRequestMessage> ReadAsHttpRequestMessageAsync(this HttpContent content, string uriScheme, int bufferSize, int maxHeaderSize, CancellationToken cancellationToken)
		{
			if (content == null)
			{
				throw Error.ArgumentNull("content");
			}
			if (uriScheme == null)
			{
				throw Error.ArgumentNull("uriScheme");
			}
			if (!Uri.CheckSchemeName(uriScheme))
			{
				throw Error.Argument("uriScheme", Resources.HttpMessageParserInvalidUriScheme, new object[]
				{
					uriScheme,
					typeof(Uri).Name
				});
			}
			if (bufferSize < 256)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("bufferSize", bufferSize, 256);
			}
			if (maxHeaderSize < 2)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("maxHeaderSize", maxHeaderSize, 2);
			}
			HttpMessageContent.ValidateHttpMessageContent(content, true, true);
			return content.ReadAsHttpRequestMessageAsyncCore(uriScheme, bufferSize, maxHeaderSize, cancellationToken);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000BCCC File Offset: 0x00009ECC
		private static async Task<HttpRequestMessage> ReadAsHttpRequestMessageAsyncCore(this HttpContent content, string uriScheme, int bufferSize, int maxHeaderSize, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			Stream stream = await content.ReadAsStreamAsync();
			HttpUnsortedRequest httpRequest = new HttpUnsortedRequest();
			HttpRequestHeaderParser parser = new HttpRequestHeaderParser(httpRequest, 2048, maxHeaderSize);
			byte[] buffer = new byte[bufferSize];
			int bytesRead = 0;
			int headerConsumed = 0;
			for (;;)
			{
				try
				{
					bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
				}
				catch (Exception innerException)
				{
					throw new IOException(Resources.HttpMessageErrorReading, innerException);
				}
				ParserState parseStatus;
				try
				{
					parseStatus = parser.ParseBuffer(buffer, bytesRead, ref headerConsumed);
				}
				catch (Exception)
				{
					parseStatus = ParserState.Invalid;
				}
				if (parseStatus == ParserState.Done)
				{
					break;
				}
				if (parseStatus != ParserState.NeedMoreData)
				{
					goto Block_4;
				}
				if (bytesRead == 0)
				{
					goto Block_5;
				}
			}
			return HttpContentMessageExtensions.CreateHttpRequestMessage(uriScheme, httpRequest, stream, bytesRead - headerConsumed);
			Block_4:
			throw Error.InvalidOperation(Resources.HttpMessageParserError, new object[]
			{
				headerConsumed,
				buffer
			});
			Block_5:
			throw new IOException(Resources.ReadAsHttpMessageUnexpectedTermination);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000BD33 File Offset: 0x00009F33
		public static Task<HttpResponseMessage> ReadAsHttpResponseMessageAsync(this HttpContent content)
		{
			return content.ReadAsHttpResponseMessageAsync(32768);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000BD40 File Offset: 0x00009F40
		public static Task<HttpResponseMessage> ReadAsHttpResponseMessageAsync(this HttpContent content, CancellationToken cancellationToken)
		{
			return content.ReadAsHttpResponseMessageAsync(32768, cancellationToken);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000BD4E File Offset: 0x00009F4E
		public static Task<HttpResponseMessage> ReadAsHttpResponseMessageAsync(this HttpContent content, int bufferSize)
		{
			return content.ReadAsHttpResponseMessageAsync(bufferSize, 16384);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000BD5C File Offset: 0x00009F5C
		public static Task<HttpResponseMessage> ReadAsHttpResponseMessageAsync(this HttpContent content, int bufferSize, CancellationToken cancellationToken)
		{
			return content.ReadAsHttpResponseMessageAsync(bufferSize, 16384, cancellationToken);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000BD6B File Offset: 0x00009F6B
		public static Task<HttpResponseMessage> ReadAsHttpResponseMessageAsync(this HttpContent content, int bufferSize, int maxHeaderSize)
		{
			return content.ReadAsHttpResponseMessageAsync(bufferSize, maxHeaderSize, CancellationToken.None);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000BD7C File Offset: 0x00009F7C
		public static Task<HttpResponseMessage> ReadAsHttpResponseMessageAsync(this HttpContent content, int bufferSize, int maxHeaderSize, CancellationToken cancellationToken)
		{
			if (content == null)
			{
				throw Error.ArgumentNull("content");
			}
			if (bufferSize < 256)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("bufferSize", bufferSize, 256);
			}
			if (maxHeaderSize < 2)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("maxHeaderSize", maxHeaderSize, 2);
			}
			HttpMessageContent.ValidateHttpMessageContent(content, false, true);
			return content.ReadAsHttpResponseMessageAsyncCore(bufferSize, maxHeaderSize, cancellationToken);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000C0AC File Offset: 0x0000A2AC
		private static async Task<HttpResponseMessage> ReadAsHttpResponseMessageAsyncCore(this HttpContent content, int bufferSize, int maxHeaderSize, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			Stream stream = await content.ReadAsStreamAsync();
			HttpUnsortedResponse httpResponse = new HttpUnsortedResponse();
			HttpResponseHeaderParser parser = new HttpResponseHeaderParser(httpResponse, 2048, maxHeaderSize);
			byte[] buffer = new byte[bufferSize];
			int bytesRead = 0;
			int headerConsumed = 0;
			for (;;)
			{
				try
				{
					bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
				}
				catch (Exception innerException)
				{
					throw new IOException(Resources.HttpMessageErrorReading, innerException);
				}
				ParserState parseStatus;
				try
				{
					parseStatus = parser.ParseBuffer(buffer, bytesRead, ref headerConsumed);
				}
				catch (Exception)
				{
					parseStatus = ParserState.Invalid;
				}
				if (parseStatus == ParserState.Done)
				{
					break;
				}
				if (parseStatus != ParserState.NeedMoreData)
				{
					goto Block_4;
				}
				if (bytesRead == 0)
				{
					goto Block_5;
				}
			}
			return HttpContentMessageExtensions.CreateHttpResponseMessage(httpResponse, stream, bytesRead - headerConsumed);
			Block_4:
			throw Error.InvalidOperation(Resources.HttpMessageParserError, new object[]
			{
				headerConsumed,
				buffer
			});
			Block_5:
			throw new IOException(Resources.ReadAsHttpMessageUnexpectedTermination);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000C10C File Offset: 0x0000A30C
		private static Uri CreateRequestUri(string uriScheme, HttpUnsortedRequest httpRequest)
		{
			IEnumerable<string> source;
			if (!httpRequest.HttpHeaders.TryGetValues("Host", out source))
			{
				throw Error.InvalidOperation(Resources.HttpMessageParserInvalidHostCount, new object[]
				{
					"Host",
					0
				});
			}
			int num = source.Count<string>();
			if (num != 1)
			{
				throw Error.InvalidOperation(Resources.HttpMessageParserInvalidHostCount, new object[]
				{
					"Host",
					num
				});
			}
			string uriString = string.Format(CultureInfo.InvariantCulture, "{0}://{1}{2}", new object[]
			{
				uriScheme,
				source.ElementAt(0),
				httpRequest.RequestUri
			});
			return new Uri(uriString);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000C1C0 File Offset: 0x0000A3C0
		private static HttpContent CreateHeaderFields(HttpHeaders source, HttpHeaders destination, Stream contentStream, int rewind)
		{
			HttpContentHeaders httpContentHeaders = null;
			HttpContent httpContent = null;
			foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair in source)
			{
				if (!destination.TryAddWithoutValidation(keyValuePair.Key, keyValuePair.Value))
				{
					if (httpContentHeaders == null)
					{
						httpContentHeaders = FormattingUtilities.CreateEmptyContentHeaders();
					}
					httpContentHeaders.TryAddWithoutValidation(keyValuePair.Key, keyValuePair.Value);
				}
			}
			if (httpContentHeaders != null)
			{
				if (!contentStream.CanSeek)
				{
					throw Error.InvalidOperation(Resources.HttpMessageContentStreamMustBeSeekable, new object[]
					{
						"ContentReadStream",
						FormattingUtilities.HttpResponseMessageType.Name
					});
				}
				contentStream.Seek((long)(-(long)rewind), SeekOrigin.Current);
				httpContent = new StreamContent(contentStream);
				httpContentHeaders.CopyTo(httpContent.Headers);
			}
			return httpContent;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000C290 File Offset: 0x0000A490
		private static HttpRequestMessage CreateHttpRequestMessage(string uriScheme, HttpUnsortedRequest httpRequest, Stream contentStream, int rewind)
		{
			HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
			httpRequestMessage.Method = httpRequest.Method;
			httpRequestMessage.RequestUri = HttpContentMessageExtensions.CreateRequestUri(uriScheme, httpRequest);
			httpRequestMessage.Version = httpRequest.Version;
			httpRequestMessage.Content = HttpContentMessageExtensions.CreateHeaderFields(httpRequest.HttpHeaders, httpRequestMessage.Headers, contentStream, rewind);
			return httpRequestMessage;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000C2E4 File Offset: 0x0000A4E4
		private static HttpResponseMessage CreateHttpResponseMessage(HttpUnsortedResponse httpResponse, Stream contentStream, int rewind)
		{
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
			httpResponseMessage.Version = httpResponse.Version;
			httpResponseMessage.StatusCode = httpResponse.StatusCode;
			httpResponseMessage.ReasonPhrase = httpResponse.ReasonPhrase;
			httpResponseMessage.Content = HttpContentMessageExtensions.CreateHeaderFields(httpResponse.HttpHeaders, httpResponseMessage.Headers, contentStream, rewind);
			return httpResponseMessage;
		}

		// Token: 0x040000E1 RID: 225
		private const int MinBufferSize = 256;

		// Token: 0x040000E2 RID: 226
		private const int DefaultBufferSize = 32768;
	}
}
