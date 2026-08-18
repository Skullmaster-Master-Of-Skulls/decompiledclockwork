using System;
using System.Globalization;
using System.Net.Http.Properties;
using System.Text;
using System.Web.Http;

namespace System.Net.Http.Formatting.Parsers
{
	// Token: 0x02000060 RID: 96
	internal class HttpStatusLineParser
	{
		// Token: 0x06000360 RID: 864 RVA: 0x0000DC34 File Offset: 0x0000BE34
		public HttpStatusLineParser(HttpUnsortedResponse httpResponse, int maxStatusLineSize)
		{
			if (maxStatusLineSize < 15)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("maxStatusLineSize", maxStatusLineSize, 15);
			}
			if (httpResponse == null)
			{
				throw Error.ArgumentNull("httpResponse");
			}
			this._httpResponse = httpResponse;
			this._maximumHeaderLength = maxStatusLineSize;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000DC90 File Offset: 0x0000BE90
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
				result = HttpStatusLineParser.ParseStatusLine(buffer, bytesReady, ref bytesConsumed, ref this._statusLineState, this._maximumHeaderLength, ref this._totalBytesConsumed, this._currentToken, this._httpResponse);
			}
			catch (Exception)
			{
				result = ParserState.Invalid;
			}
			return result;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000DCF4 File Offset: 0x0000BEF4
		private static ParserState ParseStatusLine(byte[] buffer, int bytesReady, ref int bytesConsumed, ref HttpStatusLineParser.HttpStatusLineState statusLineState, int maximumHeaderLength, ref int totalBytesConsumed, StringBuilder currentToken, HttpUnsortedResponse httpResponse)
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
			switch (statusLineState)
			{
			case HttpStatusLineParser.HttpStatusLineState.BeforeVersionNumbers:
			{
				num3 = bytesConsumed;
				while (buffer[bytesConsumed] != 47)
				{
					if (buffer[bytesConsumed] < 33 || buffer[bytesConsumed] > 122)
					{
						result = ParserState.Invalid;
						goto IL_3F5;
					}
					if (++bytesConsumed == num2)
					{
						string @string = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
						currentToken.Append(@string);
						goto IL_3F5;
					}
				}
				if (bytesConsumed > num3)
				{
					string string2 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
					currentToken.Append(string2);
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
				statusLineState = HttpStatusLineParser.HttpStatusLineState.MajorVersionNumber;
				if (++bytesConsumed == num2)
				{
					goto IL_3F5;
				}
				break;
			}
			case HttpStatusLineParser.HttpStatusLineState.MajorVersionNumber:
				break;
			case HttpStatusLineParser.HttpStatusLineState.MinorVersionNumber:
				goto IL_1AD;
			case HttpStatusLineParser.HttpStatusLineState.StatusCode:
				goto IL_250;
			case HttpStatusLineParser.HttpStatusLineState.ReasonPhrase:
				goto IL_349;
			case HttpStatusLineParser.HttpStatusLineState.AfterCarriageReturn:
				goto IL_3E1;
			default:
				goto IL_3F5;
			}
			num3 = bytesConsumed;
			while (buffer[bytesConsumed] != 46)
			{
				if (buffer[bytesConsumed] < 48 || buffer[bytesConsumed] > 57)
				{
					result = ParserState.Invalid;
					goto IL_3F5;
				}
				if (++bytesConsumed == num2)
				{
					string string3 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
					currentToken.Append(string3);
					goto IL_3F5;
				}
			}
			if (bytesConsumed > num3)
			{
				string string4 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
				currentToken.Append(string4);
			}
			currentToken.Append('.');
			statusLineState = HttpStatusLineParser.HttpStatusLineState.MinorVersionNumber;
			if (++bytesConsumed == num2)
			{
				goto IL_3F5;
			}
			IL_1AD:
			num3 = bytesConsumed;
			while (buffer[bytesConsumed] != 32)
			{
				if (buffer[bytesConsumed] < 48 || buffer[bytesConsumed] > 57)
				{
					result = ParserState.Invalid;
					goto IL_3F5;
				}
				if (++bytesConsumed == num2)
				{
					string string5 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
					currentToken.Append(string5);
					goto IL_3F5;
				}
			}
			if (bytesConsumed > num3)
			{
				string string6 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
				currentToken.Append(string6);
			}
			httpResponse.Version = Version.Parse(currentToken.ToString());
			currentToken.Clear();
			statusLineState = HttpStatusLineParser.HttpStatusLineState.StatusCode;
			if (++bytesConsumed == num2)
			{
				goto IL_3F5;
			}
			IL_250:
			num3 = bytesConsumed;
			while (buffer[bytesConsumed] != 32)
			{
				if (buffer[bytesConsumed] < 48 || buffer[bytesConsumed] > 57)
				{
					result = ParserState.Invalid;
					goto IL_3F5;
				}
				if (++bytesConsumed == num2)
				{
					string string7 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
					currentToken.Append(string7);
					goto IL_3F5;
				}
			}
			if (bytesConsumed > num3)
			{
				string string8 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
				currentToken.Append(string8);
			}
			int num4 = int.Parse(currentToken.ToString(), CultureInfo.InvariantCulture);
			if (num4 < 100 || num4 > 1000)
			{
				throw new FormatException(Error.Format(Resources.HttpInvalidStatusCode, new object[]
				{
					num4,
					100,
					1000
				}));
			}
			httpResponse.StatusCode = (HttpStatusCode)num4;
			currentToken.Clear();
			statusLineState = HttpStatusLineParser.HttpStatusLineState.ReasonPhrase;
			if (++bytesConsumed == num2)
			{
				goto IL_3F5;
			}
			IL_349:
			num3 = bytesConsumed;
			while (buffer[bytesConsumed] != 13)
			{
				if (buffer[bytesConsumed] < 32 || buffer[bytesConsumed] > 122)
				{
					result = ParserState.Invalid;
					goto IL_3F5;
				}
				if (++bytesConsumed == num2)
				{
					string string9 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
					currentToken.Append(string9);
					goto IL_3F5;
				}
			}
			if (bytesConsumed > num3)
			{
				string string10 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
				currentToken.Append(string10);
			}
			httpResponse.ReasonPhrase = currentToken.ToString();
			currentToken.Clear();
			statusLineState = HttpStatusLineParser.HttpStatusLineState.AfterCarriageReturn;
			if (++bytesConsumed == num2)
			{
				goto IL_3F5;
			}
			IL_3E1:
			if (buffer[bytesConsumed] != 10)
			{
				result = ParserState.Invalid;
			}
			else
			{
				result = ParserState.Done;
				bytesConsumed++;
			}
			IL_3F5:
			totalBytesConsumed += bytesConsumed - num;
			return result;
		}

		// Token: 0x0400011A RID: 282
		internal const int MinStatusLineSize = 15;

		// Token: 0x0400011B RID: 283
		private const int DefaultTokenAllocation = 2048;

		// Token: 0x0400011C RID: 284
		private const int MaxStatusCode = 1000;

		// Token: 0x0400011D RID: 285
		private int _totalBytesConsumed;

		// Token: 0x0400011E RID: 286
		private int _maximumHeaderLength;

		// Token: 0x0400011F RID: 287
		private HttpStatusLineParser.HttpStatusLineState _statusLineState;

		// Token: 0x04000120 RID: 288
		private HttpUnsortedResponse _httpResponse;

		// Token: 0x04000121 RID: 289
		private StringBuilder _currentToken = new StringBuilder(2048);

		// Token: 0x02000061 RID: 97
		private enum HttpStatusLineState
		{
			// Token: 0x04000123 RID: 291
			BeforeVersionNumbers,
			// Token: 0x04000124 RID: 292
			MajorVersionNumber,
			// Token: 0x04000125 RID: 293
			MinorVersionNumber,
			// Token: 0x04000126 RID: 294
			StatusCode,
			// Token: 0x04000127 RID: 295
			ReasonPhrase,
			// Token: 0x04000128 RID: 296
			AfterCarriageReturn
		}
	}
}
