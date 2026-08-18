using System;
using System.ServiceModel.Description;

namespace WCFExtrasPlus.Utils
{
	// Token: 0x0200000F RID: 15
	internal static class Extensions
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002F85 File Offset: 0x00001185
		public static string GetHeaderType(this MessageHeaderDescription header)
		{
			return (string)ReflectionUtils.GetValue(header, "BaseType");
		}
	}
}
