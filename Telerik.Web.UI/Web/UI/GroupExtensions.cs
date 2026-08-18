using System;
using System.Collections.Generic;
using Telerik.Web.UI.PivotGrid.Core;

namespace Telerik.Web.UI
{
	// Token: 0x02000E0A RID: 3594
	internal static class GroupExtensions
	{
		// Token: 0x06008537 RID: 34103 RVA: 0x001E64B0 File Offset: 0x001E46B0
		public static object[] GetGroupIndex(this IGroup group)
		{
			List<object> list = new List<object>();
			do
			{
				list.Add(group.Name);
				group = group.Parent;
			}
			while (group != null);
			list.Reverse();
			return list.ToArray();
		}
	}
}
