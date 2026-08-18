using System;
using System.Data.Spatial.Internal;

namespace System.Data.Spatial
{
	// Token: 0x020002DC RID: 732
	internal static class ExtensionMethods
	{
		// Token: 0x06002C40 RID: 11328 RVA: 0x000A859D File Offset: 0x000A679D
		internal static void CheckNull<T>(this T value, string argumentName) where T : class
		{
			if (value == null)
			{
				throw SpatialExceptions.ArgumentNull(argumentName);
			}
		}
	}
}
