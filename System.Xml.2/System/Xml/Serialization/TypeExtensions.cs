using System;
using System.Reflection;

namespace System.Xml.Serialization
{
	// Token: 0x020001C9 RID: 457
	internal static class TypeExtensions
	{
		// Token: 0x06001F1F RID: 7967 RVA: 0x000A9194 File Offset: 0x000A7394
		public static bool TryConvertTo(this Type targetType, object data, out object returnValue)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			returnValue = null;
			if (data == null)
			{
				return !targetType.IsValueType;
			}
			Type type = data.GetType();
			if (targetType == type || targetType.IsAssignableFrom(type))
			{
				returnValue = data;
				return true;
			}
			MethodInfo[] methods = targetType.GetMethods(BindingFlags.Static | BindingFlags.Public);
			foreach (MethodInfo methodInfo in methods)
			{
				if (methodInfo.Name == "op_Implicit" && methodInfo.ReturnType != null && targetType.IsAssignableFrom(methodInfo.ReturnType))
				{
					ParameterInfo[] parameters = methodInfo.GetParameters();
					if (parameters != null && parameters.Length == 1 && parameters[0].ParameterType.IsAssignableFrom(type))
					{
						returnValue = methodInfo.Invoke(null, new object[]
						{
							data
						});
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04000D04 RID: 3332
		private const string ImplicitCastOperatorName = "op_Implicit";
	}
}
