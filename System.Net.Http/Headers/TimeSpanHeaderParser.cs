using System;
using System.Globalization;

namespace System.Net.Http.Headers
{
	// Token: 0x02000045 RID: 69
	internal class TimeSpanHeaderParser : BaseHeaderParser
	{
		// Token: 0x060003CE RID: 974 RVA: 0x0000E658 File Offset: 0x0000C858
		private TimeSpanHeaderParser() : base(false)
		{
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000E664 File Offset: 0x0000C864
		public override string ToString(object value)
		{
			return ((int)((TimeSpan)value).TotalSeconds).ToString(NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000E690 File Offset: 0x0000C890
		protected override int GetParsedValueLength(string value, int startIndex, object storeValue, out object parsedValue)
		{
			parsedValue = null;
			int numberLength = HttpRuleParser.GetNumberLength(value, startIndex, false);
			if (numberLength == 0 || numberLength > 10)
			{
				return 0;
			}
			int seconds = 0;
			if (!HeaderUtilities.TryParseInt32(value.Substring(startIndex, numberLength), out seconds))
			{
				return 0;
			}
			parsedValue = new TimeSpan(0, 0, seconds);
			return numberLength;
		}

		// Token: 0x0400017A RID: 378
		internal static readonly TimeSpanHeaderParser Parser = new TimeSpanHeaderParser();
	}
}
