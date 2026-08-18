using System;

namespace NLog.Internal
{
	// Token: 0x020000A4 RID: 164
	internal static class ParameterUtils
	{
		// Token: 0x06000531 RID: 1329 RVA: 0x0000B7CC File Offset: 0x000099CC
		public static void AssertNotNull(object value, string parameterName)
		{
			if (value == null)
			{
				throw new ArgumentNullException(parameterName);
			}
		}
	}
}
