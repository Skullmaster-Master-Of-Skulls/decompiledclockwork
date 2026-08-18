using System;
using System.Globalization;

namespace System.Net.Http.Headers
{
	// Token: 0x02000025 RID: 37
	internal class ByteArrayHeaderParser : HttpHeaderParser
	{
		// Token: 0x060001A7 RID: 423 RVA: 0x00007081 File Offset: 0x00005281
		private ByteArrayHeaderParser() : base(false)
		{
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000708A File Offset: 0x0000528A
		public override string ToString(object value)
		{
			return Convert.ToBase64String((byte[])value);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00007098 File Offset: 0x00005298
		public override bool TryParseValue(string value, object storeValue, ref int index, out object parsedValue)
		{
			parsedValue = null;
			if (string.IsNullOrEmpty(value) || index == value.Length)
			{
				return false;
			}
			string text = value;
			if (index > 0)
			{
				text = value.Substring(index);
			}
			try
			{
				parsedValue = Convert.FromBase64String(text);
				index = value.Length;
				return true;
			}
			catch (FormatException ex)
			{
				if (Logging.On)
				{
					Logging.PrintError(Logging.Http, string.Format(CultureInfo.InvariantCulture, SR.net_http_parser_invalid_base64_string, new object[]
					{
						text,
						ex.Message
					}));
				}
			}
			return false;
		}

		// Token: 0x040000D7 RID: 215
		internal static readonly ByteArrayHeaderParser Parser = new ByteArrayHeaderParser();
	}
}
