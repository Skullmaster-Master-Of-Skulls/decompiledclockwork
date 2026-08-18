using System;
using System.Web.Http;

namespace System.Net.Http.Formatting.Parsers
{
	// Token: 0x0200005E RID: 94
	internal class HttpResponseHeaderParser
	{
		// Token: 0x0600035D RID: 861 RVA: 0x0000DB23 File Offset: 0x0000BD23
		public HttpResponseHeaderParser(HttpUnsortedResponse httpResponse) : this(httpResponse, 2048, 16384)
		{
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000DB38 File Offset: 0x0000BD38
		public HttpResponseHeaderParser(HttpUnsortedResponse httpResponse, int maxResponseLineSize, int maxHeaderSize)
		{
			if (httpResponse == null)
			{
				throw Error.ArgumentNull("httpResponse");
			}
			this._httpResponse = httpResponse;
			this._statusLineParser = new HttpStatusLineParser(this._httpResponse, maxResponseLineSize);
			this._headerParser = new InternetMessageFormatHeaderParser(this._httpResponse.HttpHeaders, maxHeaderSize);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000DB8C File Offset: 0x0000BD8C
		public ParserState ParseBuffer(byte[] buffer, int bytesReady, ref int bytesConsumed)
		{
			if (buffer == null)
			{
				throw Error.ArgumentNull("buffer");
			}
			ParserState result = ParserState.NeedMoreData;
			ParserState parserState = ParserState.NeedMoreData;
			switch (this._responseStatus)
			{
			case HttpResponseHeaderParser.HttpResponseState.StatusLine:
				try
				{
					parserState = this._statusLineParser.ParseBuffer(buffer, bytesReady, ref bytesConsumed);
				}
				catch (Exception)
				{
					parserState = ParserState.Invalid;
				}
				if (parserState == ParserState.Done)
				{
					this._responseStatus = HttpResponseHeaderParser.HttpResponseState.ResponseHeaders;
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
			case HttpResponseHeaderParser.HttpResponseState.ResponseHeaders:
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

		// Token: 0x04000111 RID: 273
		internal const int DefaultMaxStatusLineSize = 2048;

		// Token: 0x04000112 RID: 274
		internal const int DefaultMaxHeaderSize = 16384;

		// Token: 0x04000113 RID: 275
		private HttpUnsortedResponse _httpResponse;

		// Token: 0x04000114 RID: 276
		private HttpResponseHeaderParser.HttpResponseState _responseStatus;

		// Token: 0x04000115 RID: 277
		private HttpStatusLineParser _statusLineParser;

		// Token: 0x04000116 RID: 278
		private InternetMessageFormatHeaderParser _headerParser;

		// Token: 0x0200005F RID: 95
		private enum HttpResponseState
		{
			// Token: 0x04000118 RID: 280
			StatusLine,
			// Token: 0x04000119 RID: 281
			ResponseHeaders
		}
	}
}
