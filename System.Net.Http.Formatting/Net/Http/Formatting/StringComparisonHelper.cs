using System;
using System.Web.Http;

namespace System.Net.Http.Formatting
{
	// Token: 0x0200004E RID: 78
	internal static class StringComparisonHelper
	{
		// Token: 0x060002D5 RID: 725 RVA: 0x0000A88B File Offset: 0x00008A8B
		public static bool IsDefined(StringComparison value)
		{
			return value == StringComparison.CurrentCulture || value == StringComparison.CurrentCultureIgnoreCase || value == StringComparison.InvariantCulture || value == StringComparison.InvariantCultureIgnoreCase || value == StringComparison.Ordinal || value == StringComparison.OrdinalIgnoreCase;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000A8A6 File Offset: 0x00008AA6
		public static void Validate(StringComparison value, string parameterName)
		{
			if (!StringComparisonHelper.IsDefined(value))
			{
				throw Error.InvalidEnumArgument(parameterName, (int)value, typeof(StringComparison));
			}
		}
	}
}
