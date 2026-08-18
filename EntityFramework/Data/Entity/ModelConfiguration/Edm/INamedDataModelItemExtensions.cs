using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x0200080A RID: 2058
	internal static class INamedDataModelItemExtensions
	{
		// Token: 0x06005CA4 RID: 23716 RVA: 0x001900EC File Offset: 0x0018E2EC
		public static string UniquifyName(this IEnumerable<INamedDataModelItem> namedDataModelItems, string name)
		{
			return (from i in namedDataModelItems
			select i.Name).Uniquify(name);
		}
	}
}
