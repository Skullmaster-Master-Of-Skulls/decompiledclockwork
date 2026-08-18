using System;
using System.Collections.Generic;
using System.Reflection;

namespace TechnoPro.Common.Unity.Adapters
{
	// Token: 0x02000011 RID: 17
	public static class AssemblyAdapter
	{
		// Token: 0x0600005B RID: 91 RVA: 0x000036C0 File Offset: 0x000018C0
		public static IList<Assembly> LoadAssemblySafely(this string[] assemblyPath)
		{
			List<Assembly> list = new List<Assembly>();
			int i = 0;
			while (i < assemblyPath.Length)
			{
				string assemblyFile = assemblyPath[i];
				try
				{
					Assembly assembly = Assembly.Load(AssemblyName.GetAssemblyName(assemblyFile));
					bool flag = assembly != null;
					if (flag)
					{
						list.Add(assembly);
					}
				}
				catch
				{
				}
				IL_40:
				i++;
				continue;
				goto IL_40;
			}
			return list;
		}
	}
}
