using System;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000114 RID: 276
	internal static class EntityUtils
	{
		// Token: 0x06000AD2 RID: 2770 RVA: 0x0006F469 File Offset: 0x0006E469
		internal static T CheckArgumentNull<T>(T value, string parameterName) where T : class
		{
			if (value == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			return value;
		}
	}
}
