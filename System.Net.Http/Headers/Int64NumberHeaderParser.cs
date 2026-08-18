using System;
using System.Globalization;

namespace System.Net.Http.Headers
{
	// Token: 0x02000036 RID: 54
	internal class Int64NumberHeaderParser : BaseHeaderParser
	{
		// Token: 0x06000320 RID: 800 RVA: 0x0000C46B File Offset: 0x0000A66B
		private Int64NumberHeaderParser() : base(false)
		{
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000C474 File Offset: 0x0000A674
		public override string ToString(object value)
		{
			return ((long)value).ToString(NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000C494 File Offset: 0x0000A694
		protected override int GetParsedValueLength(string value, int startIndex, object storeValue, out object parsedValue)
		{
			parsedValue = null;
			int numberLength = HttpRuleParser.GetNumberLength(value, startIndex, false);
			if (numberLength == 0 || numberLength > 19)
			{
				return 0;
			}
			long num = 0L;
			if (!HeaderUtilities.TryParseInt64(value.Substring(startIndex, numberLength), out num))
			{
				return 0;
			}
			parsedValue = num;
			return numberLength;
		}

		// Token: 0x04000159 RID: 345
		internal static readonly Int64NumberHeaderParser Parser = new Int64NumberHeaderParser();
	}
}
