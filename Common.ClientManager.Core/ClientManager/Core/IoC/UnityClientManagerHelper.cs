using System;
using System.Collections.Generic;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.IoC
{
	// Token: 0x0200004E RID: 78
	public static class UnityClientManagerHelper
	{
		// Token: 0x060002B3 RID: 691 RVA: 0x0000C2F4 File Offset: 0x0000A4F4
		public static void Configure(params string[] extraAssemblyNames)
		{
			string[] array = new string[]
			{
				"Common.Core.dll",
				"Common.ClientManager.Core.dll",
				"Common.DAO.Impl.dll",
				"Common.Public.dll",
				"Common.ClientManager.ClientCaching.dll"
			};
			bool flag = extraAssemblyNames != null;
			if (flag)
			{
				List<string> list = new List<string>(array);
				list.AddRange(extraAssemblyNames);
				ObjectFactory.Configure(list.ToArray());
			}
			else
			{
				ObjectFactory.Configure(array);
			}
		}
	}
}
