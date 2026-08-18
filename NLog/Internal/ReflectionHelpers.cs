using System;
using System.Collections.Generic;
using System.Reflection;
using NLog.Common;

namespace NLog.Internal
{
	// Token: 0x020000AA RID: 170
	internal static class ReflectionHelpers
	{
		// Token: 0x06000554 RID: 1364 RVA: 0x0000C0AC File Offset: 0x0000A2AC
		public static Type[] SafeGetTypes(this Assembly assembly)
		{
			Type[] result;
			try
			{
				result = assembly.GetTypes();
			}
			catch (ReflectionTypeLoadException ex)
			{
				foreach (Exception ex2 in ex.LoaderExceptions)
				{
					InternalLogger.Warn(ex2, "Type load exception.");
				}
				List<Type> list = new List<Type>();
				foreach (Type type in ex.Types)
				{
					if (type != null)
					{
						list.Add(type);
					}
				}
				result = list.ToArray();
			}
			return result;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0000C144 File Offset: 0x0000A344
		public static bool IsStaticClass(this Type type)
		{
			return type.IsClass && type.IsAbstract && type.IsSealed;
		}
	}
}
