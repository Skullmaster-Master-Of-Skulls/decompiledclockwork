using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TechnoPro.Common.Unity.Adapters
{
	// Token: 0x02000013 RID: 19
	public static class TypesAdapter
	{
		// Token: 0x0600005D RID: 93 RVA: 0x0000388C File Offset: 0x00001A8C
		public static IEnumerable<Type> GetTypesSafely(this Assembly assembly)
		{
			IEnumerable<Type> result;
			try
			{
				result = assembly.GetTypes();
			}
			catch (ReflectionTypeLoadException ex)
			{
				result = from x in ex.Types
				where x != null
				select x;
			}
			return result;
		}
	}
}
