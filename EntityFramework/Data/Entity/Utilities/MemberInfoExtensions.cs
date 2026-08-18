using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace System.Data.Entity.Utilities
{
	// Token: 0x02000009 RID: 9
	internal static class MemberInfoExtensions
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00003894 File Offset: 0x00001A94
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public static object GetValue(this MemberInfo memberInfo)
		{
			PropertyInfo propertyInfo = memberInfo as PropertyInfo;
			if (!(propertyInfo != null))
			{
				return ((FieldInfo)memberInfo).GetValue(null);
			}
			return propertyInfo.GetValue(null, null);
		}
	}
}
