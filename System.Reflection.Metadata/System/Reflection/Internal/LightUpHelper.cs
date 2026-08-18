using System;
using System.IO;

namespace System.Reflection.Internal
{
	// Token: 0x02000163 RID: 355
	internal static class LightUpHelper
	{
		// Token: 0x06000B00 RID: 2816 RVA: 0x0001F600 File Offset: 0x0001D800
		internal static Type GetType(string typeName, params string[] assemblyNames)
		{
			foreach (string str in assemblyNames)
			{
				Type type = null;
				try
				{
					type = Type.GetType(typeName + "," + str, false);
				}
				catch (IOException)
				{
				}
				if (type != null)
				{
					return type;
				}
			}
			return null;
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0001F654 File Offset: 0x0001D854
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
