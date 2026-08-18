using System;
using System.ServiceModel.Description;

namespace WCFExtras.Utils
{
	// Token: 0x02000006 RID: 6
	internal static class Extensions
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002A00 File Offset: 0x00000C00
		public static string GetHeaderType(this MessageHeaderDescription header)
		{
			return (string)ReflectionUtils.GetValue(header, "BaseType");
		}
	}
}
