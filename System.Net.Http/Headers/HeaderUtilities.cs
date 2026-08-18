using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mail;
using System.Text;

namespace System.Net.Http.Headers
{
	// Token: 0x0200002D RID: 45
	internal static class HeaderUtilities
	{
		// Token: 0x06000239 RID: 569 RVA: 0x0000975C File Offset: 0x0000795C
		internal static void SetQuality(ICollection<NameValueHeaderValue> parameters, double? value)
		{
			NameValueHeaderValue nameValueHeaderValue = NameValueHeaderValue.Find(parameters, "q");
			if (value != null)
			{
				double? num = value;
				double num2 = 0.0;
				if (!(num.GetValueOrDefault() < num2 & num != null))
				{
					num = value;
					num2 = (double)1;
					if (!(num.GetValueOrDefault() > num2 & num != null))
					{
						string value2 = value.Value.ToString("0.0##", NumberFormatInfo.InvariantInfo);
						if (nameValueHeaderValue != null)
						{
							nameValueHeaderValue.Value = value2;
							return;
						}
						parameters.Add(new NameValueHeaderValue("q", value2));
						return;
					}
				}
				throw new ArgumentOutOfRangeException("value");
			}
			if (nameValueHeaderValue != null)
			{
				parameters.Remove(nameValueHeaderValue);
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00009808 File Offset: 0x00007A08
		internal static double? GetQuality(ICollection<NameValueHeaderValue> parameters)
		{
			NameValueHeaderValue nameValueHeaderValue = NameValueHeaderValue.Find(parameters, "q");
			if (nameValueHeaderValue != null)
			{
				double value = 0.0;
				if (double.TryParse(nameValueHeaderValue.Value, NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo, out value))
				{
					return new double?(value);
				}
				if (Logging.On)
				{
					Logging.PrintError(Logging.Http, string.Format(CultureInfo.InvariantCulture, SR.net_http_log_headers_invalid_quality, new object[]
					{
						nameValueHeaderValue.Value
					}));
				}
			}
			return null;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00009884 File Offset: 0x00007A84
		internal static void CheckValidToken(string value, string parameterName)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentException(SR.net_http_argument_empty_string, parameterName);
			}
			if (HttpRuleParser.GetTokenLength(value, 0) != value.Length)
			{
				throw new FormatException(string.Format(CultureInfo.InvariantCulture, SR.net_http_headers_invalid_value, new object[]
				{
					value
				}));
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x000098D4 File Offset: 0x00007AD4
		internal static void CheckValidComment(string value, string parameterName)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentException(SR.net_http_argument_empty_string, parameterName);
			}
			int num = 0;
			if (HttpRuleParser.GetCommentLength(value, 0, out num) != HttpParseResult.Parsed || num != value.Length)
			{
				throw new FormatException(string.Format(CultureInfo.InvariantCulture, SR.net_http_headers_invalid_value, new object[]
				{
					value
				}));
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000992C File Offset: 0x00007B2C
		internal static void CheckValidQuotedString(string value, string parameterName)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentException(SR.net_http_argument_empty_string, parameterName);
			}
			int num = 0;
			if (HttpRuleParser.GetQuotedStringLength(value, 0, out num) != HttpParseResult.Parsed || num != value.Length)
			{
				throw new FormatException(string.Format(CultureInfo.InvariantCulture, SR.net_http_headers_invalid_value, new object[]
				{
					value
				}));
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00009982 File Offset: 0x00007B82
		internal static bool AreEqualCollections<T>(ICollection<T> x, ICollection<T> y)
		{
			return HeaderUtilities.AreEqualCollections<T>(x, y, null);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000998C File Offset: 0x00007B8C
		internal static bool AreEqualCollections<T>(ICollection<T> x, ICollection<T> y, IEqualityComparer<T> comparer)
		{
			if (x == null)
			{
				return y == null || y.Count == 0;
			}
			if (y == null)
			{
				return x.Count == 0;
			}
			if (x.Count != y.Count)
			{
				return false;
			}
			if (x.Count == 0)
			{
				return true;
			}
			bool[] array = new bool[x.Count];
			foreach (T x2 in x)
			{
				int num = 0;
				bool flag = false;
				foreach (T t in y)
				{
					if (!array[num] && ((comparer == null && x2.Equals(t)) || (comparer != null && comparer.Equals(x2, t))))
					{
						array[num] = true;
						flag = true;
						break;
					}
					num++;
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00009A98 File Offset: 0x00007C98
		internal static int GetNextNonEmptyOrWhitespaceIndex(string input, int startIndex, bool skipEmptyValues, out bool separatorFound)
		{
			separatorFound = false;
			int num = startIndex + HttpRuleParser.GetWhitespaceLength(input, startIndex);
			if (num == input.Length || input[num] != ',')
			{
				return num;
			}
			separatorFound = true;
			num++;
			num += HttpRuleParser.GetWhitespaceLength(input, num);
			if (skipEmptyValues)
			{
				while (num < input.Length && input[num] == ',')
				{
					num++;
					num += HttpRuleParser.GetWhitespaceLength(input, num);
				}
			}
			return num;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00009B04 File Offset: 0x00007D04
		internal static DateTimeOffset? GetDateTimeOffsetValue(string headerName, HttpHeaders store)
		{
			object parsedValues = store.GetParsedValues(headerName);
			if (parsedValues != null)
			{
				return new DateTimeOffset?((DateTimeOffset)parsedValues);
			}
			return null;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00009B34 File Offset: 0x00007D34
		internal static TimeSpan? GetTimeSpanValue(string headerName, HttpHeaders store)
		{
			object parsedValues = store.GetParsedValues(headerName);
			if (parsedValues != null)
			{
				return new TimeSpan?((TimeSpan)parsedValues);
			}
			return null;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00009B61 File Offset: 0x00007D61
		internal static bool TryParseInt32(string value, out int result)
		{
			return int.TryParse(value, NumberStyles.None, NumberFormatInfo.InvariantInfo, out result);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00009B70 File Offset: 0x00007D70
		internal static bool TryParseInt64(string value, out long result)
		{
			return long.TryParse(value, NumberStyles.None, NumberFormatInfo.InvariantInfo, out result);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00009B80 File Offset: 0x00007D80
		internal static string DumpHeaders(params HttpHeaders[] headers)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{\r\n");
			for (int i = 0; i < headers.Length; i++)
			{
				if (headers[i] != null)
				{
					foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair in headers[i])
					{
						foreach (string value in keyValuePair.Value)
						{
							stringBuilder.Append("  ");
							stringBuilder.Append(keyValuePair.Key);
							stringBuilder.Append(": ");
							stringBuilder.Append(value);
							stringBuilder.Append("\r\n");
						}
					}
				}
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00009C78 File Offset: 0x00007E78
		internal static bool IsValidEmailAddress(string value)
		{
			try
			{
				MailAddressParser.ParseAddress(value);
				return true;
			}
			catch (FormatException ex)
			{
				if (Logging.On)
				{
					Logging.PrintError(Logging.Http, string.Format(CultureInfo.InvariantCulture, SR.net_http_log_headers_wrong_email_format, new object[]
					{
						value,
						ex.Message
					}));
				}
			}
			return false;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00009CDC File Offset: 0x00007EDC
		private static void ValidateToken(HttpHeaderValueCollection<string> collection, string value)
		{
			HeaderUtilities.CheckValidToken(value, "item");
		}

		// Token: 0x04000122 RID: 290
		private const string qualityName = "q";

		// Token: 0x04000123 RID: 291
		internal const string ConnectionClose = "close";

		// Token: 0x04000124 RID: 292
		internal static readonly TransferCodingHeaderValue TransferEncodingChunked = new TransferCodingHeaderValue("chunked");

		// Token: 0x04000125 RID: 293
		internal static readonly NameValueWithParametersHeaderValue ExpectContinue = new NameValueWithParametersHeaderValue("100-continue");

		// Token: 0x04000126 RID: 294
		internal const string BytesUnit = "bytes";

		// Token: 0x04000127 RID: 295
		internal static readonly Action<HttpHeaderValueCollection<string>, string> TokenValidator = new Action<HttpHeaderValueCollection<string>, string>(HeaderUtilities.ValidateToken);
	}
}
