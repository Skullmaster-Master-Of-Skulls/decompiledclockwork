using System;

namespace System.IdentityModel
{
	// Token: 0x020000AD RID: 173
	internal static class StringUtil
	{
		// Token: 0x06000547 RID: 1351 RVA: 0x000141C0 File Offset: 0x000123C0
		public static string OptimizeString(string value)
		{
			if (value != null)
			{
				string text = string.IsInterned(value);
				if (text != null)
				{
					return text;
				}
			}
			return value;
		}
	}
}
