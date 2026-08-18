using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Utilities
{
	// Token: 0x0200000A RID: 10
	internal static class AssemblyExtensions
	{
		// Token: 0x0600006B RID: 107 RVA: 0x000038C6 File Offset: 0x00001AC6
		public static string GetInformationalVersion(this Assembly assembly)
		{
			return assembly.GetCustomAttributes<AssemblyInformationalVersionAttribute>().Single<AssemblyInformationalVersionAttribute>().InformationalVersion;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000038EC File Offset: 0x00001AEC
		public static IEnumerable<Type> GetAccessibleTypes(this Assembly assembly)
		{
			IEnumerable<Type> result;
			try
			{
				result = from t in assembly.DefinedTypes
				select t.AsType();
			}
			catch (ReflectionTypeLoadException ex)
			{
				result = from t in ex.Types
				where t != null
				select t;
			}
			return result;
		}
	}
}
