using System;

namespace System.Net.Http.Headers
{
	// Token: 0x0200002A RID: 42
	internal class DateHeaderParser : HttpHeaderParser
	{
		// Token: 0x0600021B RID: 539 RVA: 0x000090D7 File Offset: 0x000072D7
		private DateHeaderParser() : base(false)
		{
		}

		// Token: 0x0600021C RID: 540 RVA: 0x000090E0 File Offset: 0x000072E0
		public override string ToString(object value)
		{
			return HttpRuleParser.DateToString((DateTimeOffset)value);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000090F0 File Offset: 0x000072F0
		public override bool TryParseValue(string value, object storeValue, ref int index, out object parsedValue)
		{
			parsedValue = null;
			if (string.IsNullOrEmpty(value) || index == value.Length)
			{
				return false;
			}
			string input = value;
			if (index > 0)
			{
				input = value.Substring(index);
			}
			DateTimeOffset dateTimeOffset;
			if (!HttpRuleParser.TryStringToDate(input, out dateTimeOffset))
			{
				return false;
			}
			index = value.Length;
			parsedValue = dateTimeOffset;
			return true;
		}

		// Token: 0x04000104 RID: 260
		internal static readonly DateHeaderParser Parser = new DateHeaderParser();
	}
}
