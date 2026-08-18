using System;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x020000E9 RID: 233
	internal static class EntityUtils
	{
		// Token: 0x0600092A RID: 2346 RVA: 0x0006CA44 File Offset: 0x0006AC44
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
