using System;
using System.Globalization;

namespace System.Net.Http.Headers
{
	// Token: 0x02000035 RID: 53
	internal class Int32NumberHeaderParser : BaseHeaderParser
	{
		// Token: 0x0600031C RID: 796 RVA: 0x0000C3F0 File Offset: 0x0000A5F0
		private Int32NumberHeaderParser() : base(false)
		{
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000C3FC File Offset: 0x0000A5FC
		public override string ToString(object value)
		{
			return ((int)value).ToString(NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000C41C File Offset: 0x0000A61C
		protected override int GetParsedValueLength(string value, int startIndex, object storeValue, out object parsedValue)
		{
			parsedValue = null;
			int numberLength = HttpRuleParser.GetNumberLength(value, startIndex, false);
			if (numberLength == 0 || numberLength > 10)
			{
				return 0;
			}
			int num = 0;
			if (!HeaderUtilities.TryParseInt32(value.Substring(startIndex, numberLength), out num))
			{
				return 0;
			}
			parsedValue = num;
			return numberLength;
		}

		// Token: 0x04000158 RID: 344
		internal static readonly Int32NumberHeaderParser Parser = new Int32NumberHeaderParser();
	}
}
