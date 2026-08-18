using System;

namespace Telerik.Web.Extensions
{
	// Token: 0x02000152 RID: 338
	public static class StringExtensions
	{
		// Token: 0x06000D6E RID: 3438 RVA: 0x000317D7 File Offset: 0x0002F9D7
		public static bool IsEmptySerializedObject(this string str)
		{
			return str.Equals("{}", StringComparison.InvariantCultureIgnoreCase);
		}
	}
}
