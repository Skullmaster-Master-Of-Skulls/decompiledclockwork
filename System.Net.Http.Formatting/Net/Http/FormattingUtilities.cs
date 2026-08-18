using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Xml;
using Newtonsoft.Json.Linq;

namespace System.Net.Http
{
	// Token: 0x0200003C RID: 60
	internal static class FormattingUtilities
	{
		// Token: 0x0600022C RID: 556 RVA: 0x000085D0 File Offset: 0x000067D0
		public static bool IsJTokenType(Type type)
		{
			return typeof(JToken).IsAssignableFrom(type);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000085E4 File Offset: 0x000067E4
		public static HttpContentHeaders CreateEmptyContentHeaders()
		{
			HttpContent httpContent = null;
			HttpContentHeaders httpContentHeaders = null;
			try
			{
				httpContent = new StringContent(string.Empty);
				httpContentHeaders = httpContent.Headers;
				httpContentHeaders.Clear();
			}
			finally
			{
				if (httpContent != null)
				{
					httpContent.Dispose();
				}
			}
			return httpContentHeaders;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000862C File Offset: 0x0000682C
		public static XmlDictionaryReaderQuotas CreateDefaultReaderQuotas()
		{
			return new XmlDictionaryReaderQuotas
			{
				MaxArrayLength = int.MaxValue,
				MaxBytesPerRead = int.MaxValue,
				MaxDepth = 256,
				MaxNameTableCharCount = int.MaxValue,
				MaxStringContentLength = int.MaxValue
			};
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00008678 File Offset: 0x00006878
		public static string UnquoteToken(string token)
		{
			if (string.IsNullOrWhiteSpace(token))
			{
				return token;
			}
			if (token.StartsWith("\"", StringComparison.Ordinal) && token.EndsWith("\"", StringComparison.Ordinal) && token.Length > 1)
			{
				return token.Substring(1, token.Length - 2);
			}
			return token;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000086C8 File Offset: 0x000068C8
		public static bool ValidateHeaderToken(string token)
		{
			if (token == null)
			{
				return false;
			}
			foreach (char c in token)
			{
				if (c < '!' || c > '~' || "()<>@,;:\\\"/[]?={}".IndexOf(c) != -1)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00008714 File Offset: 0x00006914
		public static string DateToString(DateTimeOffset dateTime)
		{
			return dateTime.ToUniversalTime().ToString("r", CultureInfo.InvariantCulture);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000873A File Offset: 0x0000693A
		public static bool TryParseDate(string input, out DateTimeOffset result)
		{
			return DateTimeOffset.TryParseExact(input, FormattingUtilities.dateFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.AllowInnerWhite | DateTimeStyles.AssumeUniversal, out result);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000874F File Offset: 0x0000694F
		public static bool TryParseInt32(string value, out int result)
		{
			return int.TryParse(value, NumberStyles.None, NumberFormatInfo.InvariantInfo, out result);
		}

		// Token: 0x04000086 RID: 134
		private const string NonTokenChars = "()<>@,;:\\\"/[]?={}";

		// Token: 0x04000087 RID: 135
		public const double Match = 1.0;

		// Token: 0x04000088 RID: 136
		public const double NoMatch = 0.0;

		// Token: 0x04000089 RID: 137
		public const int DefaultMaxDepth = 256;

		// Token: 0x0400008A RID: 138
		public const int DefaultMinDepth = 1;

		// Token: 0x0400008B RID: 139
		public const string HttpRequestedWithHeader = "x-requested-with";

		// Token: 0x0400008C RID: 140
		public const string HttpRequestedWithHeaderValue = "XMLHttpRequest";

		// Token: 0x0400008D RID: 141
		public const string HttpHostHeader = "Host";

		// Token: 0x0400008E RID: 142
		public const string HttpVersionToken = "HTTP";

		// Token: 0x0400008F RID: 143
		private static readonly string[] dateFormats = new string[]
		{
			"ddd, d MMM yyyy H:m:s 'GMT'",
			"ddd, d MMM yyyy H:m:s",
			"d MMM yyyy H:m:s 'GMT'",
			"d MMM yyyy H:m:s",
			"ddd, d MMM yy H:m:s 'GMT'",
			"ddd, d MMM yy H:m:s",
			"d MMM yy H:m:s 'GMT'",
			"d MMM yy H:m:s",
			"dddd, d'-'MMM'-'yy H:m:s 'GMT'",
			"dddd, d'-'MMM'-'yy H:m:s",
			"ddd MMM d H:m:s yyyy",
			"ddd, d MMM yyyy H:m:s zzz",
			"ddd, d MMM yyyy H:m:s",
			"d MMM yyyy H:m:s zzz",
			"d MMM yyyy H:m:s"
		};

		// Token: 0x04000090 RID: 144
		public static readonly Type HttpRequestMessageType = typeof(HttpRequestMessage);

		// Token: 0x04000091 RID: 145
		public static readonly Type HttpResponseMessageType = typeof(HttpResponseMessage);

		// Token: 0x04000092 RID: 146
		public static readonly Type HttpContentType = typeof(HttpContent);

		// Token: 0x04000093 RID: 147
		public static readonly Type DelegatingEnumerableGenericType = typeof(DelegatingEnumerable<>);

		// Token: 0x04000094 RID: 148
		public static readonly Type EnumerableInterfaceGenericType = typeof(IEnumerable<>);

		// Token: 0x04000095 RID: 149
		public static readonly Type QueryableInterfaceGenericType = typeof(IQueryable<>);

		// Token: 0x04000096 RID: 150
		public static readonly XsdDataContractExporter XsdDataContractExporter = new XsdDataContractExporter();
	}
}
