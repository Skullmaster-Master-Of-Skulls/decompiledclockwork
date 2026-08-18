using System;
using System.Net.Http.Properties;
using System.Text;
using System.Web.Http;

namespace System.Net.Http.Formatting.Parsers
{
	// Token: 0x0200005C RID: 92
	internal class HttpRequestLineParser
	{
		// Token: 0x0600035A RID: 858 RVA: 0x0000D6A0 File Offset: 0x0000B8A0
		public HttpRequestLineParser(HttpUnsortedRequest httpRequest, int maxRequestLineSize)
		{
			if (maxRequestLineSize < 14)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("maxRequestLineSize", maxRequestLineSize, 14);
			}
			if (httpRequest == null)
			{
				throw Error.ArgumentNull("httpRequest");
			}
			this._httpRequest = httpRequest;
			this._maximumHeaderLength = maxRequestLineSize;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000D6FC File Offset: 0x0000B8FC
		public ParserState ParseBuffer(byte[] buffer, int bytesReady, ref int bytesConsumed)
		{
			if (buffer == null)
			{
				throw Error.ArgumentNull("buffer");
			}
			ParserState result = ParserState.NeedMoreData;
			if (bytesConsumed >= bytesReady)
			{
				return result;
			}
			try
			{
				result = HttpRequestLineParser.ParseRequestLine(buffer, bytesReady, ref bytesConsumed, ref this._requestLineState, this._maximumHeaderLength, ref this._totalBytesConsumed, this._currentToken, this._httpRequest);
			}
			catch (Exception)
			{
				result = ParserState.Invalid;
			}
			return result;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000D760 File Offset: 0x0000B960
		private static ParserState ParseRequestLine(byte[] buffer, int bytesReady, ref int bytesConsumed, ref HttpRequestLineParser.HttpRequestLineState requestLineState, int maximumHeaderLength, ref int totalBytesConsumed, StringBuilder currentToken, HttpUnsortedRequest httpRequest)
		{
			int num = bytesConsumed;
			ParserState result = ParserState.DataTooBig;
			int num2 = (maximumHeaderLength <= 0) ? int.MaxValue : (maximumHeaderLength - totalBytesConsumed + bytesConsumed);
			if (bytesReady < num2)
			{
				result = ParserState.NeedMoreData;
				num2 = bytesReady;
			}
			int num3;
			switch (requestLineState)
			{
			case HttpRequestLineParser.HttpRequestLineState.RequestMethod:
				num3 = bytesConsumed;
				while (buffer[bytesConsumed] != 32)
				{
					if (buffer[bytesConsumed] < 33 || buffer[bytesConsumed] > 122)
					{
						result = ParserState.Invalid;
						goto IL_3AB;
					}
					if (++bytesConsumed == num2)
					{
						string @string = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
						currentToken.Append(@string);
						goto IL_3AB;
					}
				}
				if (bytesConsumed > num3)
				{
					string string2 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
					currentToken.Append(string2);
				}
				httpRequest.Method = new HttpMethod(currentToken.ToString());
				currentToken.Clear();
				requestLineState = HttpRequestLineParser.HttpRequestLineState.RequestUri;
				if (++bytesConsumed == num2)
				{
					goto IL_3AB;
				}
				break;
			case HttpRequestLineParser.HttpRequestLineState.RequestUri:
				break;
			case HttpRequestLineParser.HttpRequestLineState.BeforeVersionNumbers:
				goto IL_198;
			case HttpRequestLineParser.HttpRequestLineState.MajorVersionNumber:
				goto IL_268;
			case HttpRequestLineParser.HttpRequestLineState.MinorVersionNumber:
				goto IL_2FA;
			case HttpRequestLineParser.HttpRequestLineState.AfterCarriageReturn:
				goto IL_397;
			default:
				goto IL_3AB;
			}
			num3 = bytesConsumed;
			while (buffer[bytesConsumed] != 32)
			{
				if (buffer[bytesConsumed] == 13)
				{
					result = ParserState.Invalid;
					goto IL_3AB;
				}
				if (++bytesConsumed == num2)
				{
					string string3 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
					currentToken.Append(string3);
					goto IL_3AB;
				}
			}
			if (bytesConsumed > num3)
			{
				string string4 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
				currentToken.Append(string4);
			}
			if (currentToken.Length == 0)
			{
				throw new FormatException(Resources.HttpMessageParserEmptyUri);
			}
			httpRequest.RequestUri = currentToken.ToString();
			currentToken.Clear();
			requestLineState = HttpRequestLineParser.HttpRequestLineState.BeforeVersionNumbers;
			if (++bytesConsumed == num2)
			{
				goto IL_3AB;
			}
			IL_198:
			num3 = bytesConsumed;
			while (buffer[bytesConsumed] != 47)
			{
				if (buffer[bytesConsumed] < 33 || buffer[bytesConsumed] > 122)
				{
					result = ParserState.Invalid;
					goto IL_3AB;
				}
				if (++bytesConsumed == num2)
				{
					string string5 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
					currentToken.Append(string5);
					goto IL_3AB;
				}
			}
			if (bytesConsumed > num3)
			{
				string string6 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
				currentToken.Append(string6);
			}
			string text = currentToken.ToString();
			if (string.CompareOrdinal("HTTP", text) != 0)
			{
				throw new FormatException(Error.Format(Resources.HttpInvalidVersion, new object[]
				{
					text,
					"HTTP"
				}));
			}
			currentToken.Clear();
			requestLineState = HttpRequestLineParser.HttpRequestLineState.MajorVersionNumber;
			if (++bytesConsumed == num2)
			{
				goto IL_3AB;
			}
			IL_268:
			num3 = bytesConsumed;
			while (buffer[bytesConsumed] != 46)
			{
				if (buffer[bytesConsumed] < 48 || buffer[bytesConsumed] > 57)
				{
					result = ParserState.Invalid;
					goto IL_3AB;
				}
				if (++bytesConsumed == num2)
				{
					string string7 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
					currentToken.Append(string7);
					goto IL_3AB;
				}
			}
			if (bytesConsumed > num3)
			{
				string string8 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
				currentToken.Append(string8);
			}
			currentToken.Append('.');
			requestLineState = HttpRequestLineParser.HttpRequestLineState.MinorVersionNumber;
			if (++bytesConsumed == num2)
			{
				goto IL_3AB;
			}
			IL_2FA:
			num3 = bytesConsumed;
			while (buffer[bytesConsumed] != 13)
			{
				if (buffer[bytesConsumed] < 48 || buffer[bytesConsumed] > 57)
				{
					result = ParserState.Invalid;
					goto IL_3AB;
				}
				if (++bytesConsumed == num2)
				{
					string string9 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
					currentToken.Append(string9);
					goto IL_3AB;
				}
			}
			if (bytesConsumed > num3)
			{
				string string10 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
				currentToken.Append(string10);
			}
			httpRequest.Version = Version.Parse(currentToken.ToString());
			currentToken.Clear();
			requestLineState = HttpRequestLineParser.HttpRequestLineState.AfterCarriageReturn;
			if (++bytesConsumed == num2)
			{
				goto IL_3AB;
			}
			IL_397:
			if (buffer[bytesConsumed] != 10)
			{
				result = ParserState.Invalid;
			}
			else
			{
				result = ParserState.Done;
				bytesConsumed++;
			}
			IL_3AB:
			totalBytesConsumed += bytesConsumed - num;
			return result;
		}

		// Token: 0x04000103 RID: 259
		internal const int MinRequestLineSize = 14;

		// Token: 0x04000104 RID: 260
		private const int DefaultTokenAllocation = 2048;

		// Token: 0x04000105 RID: 261
		private int _totalBytesConsumed;

		// Token: 0x04000106 RID: 262
		private int _maximumHeaderLength;

		// Token: 0x04000107 RID: 263
		private HttpRequestLineParser.HttpRequestLineState _requestLineState;

		// Token: 0x04000108 RID: 264
		private HttpUnsortedRequest _httpRequest;

		// Token: 0x04000109 RID: 265
		private StringBuilder _currentToken = new StringBuilder(2048);

		// Token: 0x0200005D RID: 93
		private enum HttpRequestLineState
		{
			// Token: 0x0400010B RID: 267
			RequestMethod,
			// Token: 0x0400010C RID: 268
			RequestUri,
			// Token: 0x0400010D RID: 269
			BeforeVersionNumbers,
			// Token: 0x0400010E RID: 270
			MajorVersionNumber,
			// Token: 0x0400010F RID: 271
			MinorVersionNumber,
			// Token: 0x04000110 RID: 272
			AfterCarriageReturn
		}
	}
}
