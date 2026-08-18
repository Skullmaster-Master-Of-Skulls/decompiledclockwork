using System;

namespace System.Reflection.Internal
{
	// Token: 0x02000085 RID: 133
	internal static class LightUpHelper
	{
		// Token: 0x06000340 RID: 832 RVA: 0x000081CC File Offset: 0x000063CC
		internal static MethodInfo GetMethod(Type type, string name, params Type[] parameterTypes)
		{
			MethodInfo result;
			try
			{
				result = type.GetRuntimeMethod(name, parameterTypes);
			}
			catch (AmbiguousMatchException)
			{
				result = null;
			}
			return result;
		}
	}
}
