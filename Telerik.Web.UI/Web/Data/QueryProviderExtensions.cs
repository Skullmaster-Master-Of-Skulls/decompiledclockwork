using System;
using System.Linq;

namespace Telerik.Web.Data
{
	// Token: 0x02001BA6 RID: 7078
	internal static class QueryProviderExtensions
	{
		// Token: 0x060111ED RID: 70125 RVA: 0x003C67FB File Offset: 0x003C49FB
		public static bool IsEntityFrameworkProvider(this IQueryProvider provider)
		{
			return provider.GetType().FullName == "System.Data.Objects.ELinq.ObjectQueryProvider";
		}

		// Token: 0x060111EE RID: 70126 RVA: 0x003C6812 File Offset: 0x003C4A12
		public static bool IsLinqToObjectsProvider(this IQueryProvider provider)
		{
			return provider.GetType().FullName.Contains("EnumerableQuery");
		}
	}
}
