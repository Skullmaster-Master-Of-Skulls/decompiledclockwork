using System;
using System.Web.Http;

namespace System.Net.Http.Formatting.Parsers
{
	// Token: 0x0200005A RID: 90
	internal class HttpRequestHeaderParser
	{
		// Token: 0x06000357 RID: 855 RVA: 0x0000D58F File Offset: 0x0000B78F
		public HttpRequestHeaderParser(HttpUnsortedRequest httpRequest) : this(httpRequest, 2048, 16384)
		{
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000D5A4 File Offset: 0x0000B7A4
		public HttpRequestHeaderParser(HttpUnsortedRequest httpRequest, int maxRequestLineSize, int maxHeaderSize)
		{
			if (httpRequest == null)
			{
				throw Error.ArgumentNull("httpRequest");
			}
			this._httpRequest = httpRequest;
			this._requestLineParser = new HttpRequestLineParser(this._httpRequest, maxRequestLineSize);
			this._headerParser = new InternetMessageFormatHeaderParser(this._httpRequest.HttpHeaders, maxHeaderSize);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000D5F8 File Offset: 0x0000B7F8
		public ParserState ParseBuffer(byte[] buffer, int bytesReady, ref int bytesConsumed)
		{
			if (buffer == null)
			{
				throw Error.ArgumentNull("buffer");
			}
			ParserState result = ParserState.NeedMoreData;
			ParserState parserState = ParserState.NeedMoreData;
			switch (this._requestStatus)
			{
			case HttpRequestHeaderParser.HttpRequestState.RequestLine:
				try
				{
					parserState = this._requestLineParser.ParseBuffer(buffer, bytesReady, ref bytesConsumed);
				}
				catch (Exception)
				{
					parserState = ParserState.Invalid;
				}
				if (parserState == ParserState.Done)
				{
					this._requestStatus = HttpRequestHeaderParser.HttpRequestState.RequestHeaders;
					parserState = ParserState.NeedMoreData;
				}
				else
				{
					if (parserState != ParserState.NeedMoreData)
					{
						return parserState;
					}
					return result;
				}
				break;
			case HttpRequestHeaderParser.HttpRequestState.RequestHeaders:
				break;
			default:
				return result;
			}
			if (bytesConsumed < bytesReady)
			{
				try
				{
					parserState = this._headerParser.ParseBuffer(buffer, bytesReady, ref bytesConsumed);
				}
				catch (Exception)
				{
					parserState = ParserState.Invalid;
				}
				if (parserState == ParserState.Done)
				{
					result = parserState;
				}
				else if (parserState != ParserState.NeedMoreData)
				{
					result = parserState;
				}
			}
			return result;
		}

		// Token: 0x040000FA RID: 250
		internal const int DefaultMaxRequestLineSize = 2048;

		// Token: 0x040000FB RID: 251
		internal const int DefaultMaxHeaderSize = 16384;

		// Token: 0x040000FC RID: 252
		private HttpUnsortedRequest _httpRequest;

		// Token: 0x040000FD RID: 253
		private HttpRequestHeaderParser.HttpRequestState _requestStatus;

		// Token: 0x040000FE RID: 254
		private HttpRequestLineParser _requestLineParser;

		// Token: 0x040000FF RID: 255
		private InternetMessageFormatHeaderParser _headerParser;

		// Token: 0x0200005B RID: 91
		private enum HttpRequestState
		{
			// Token: 0x04000101 RID: 257
			RequestLine,
			// Token: 0x04000102 RID: 258
			RequestHeaders
		}
	}
}
