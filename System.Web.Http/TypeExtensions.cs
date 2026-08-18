using System;
using System.ComponentModel;

namespace System
{
	// Token: 0x0200001C RID: 28
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal static class TypeExtensions
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x00004678 File Offset: 0x00002878
		public static bool IsNullable(this Type type)
		{
			return !type.IsValueType || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
		}
	}
}
