using System;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000D39 RID: 3385
	internal static class IGroupExtensions
	{
		// Token: 0x06007DD4 RID: 32212 RVA: 0x001CC294 File Offset: 0x001CA494
		public static int GetLevel(IGroup group)
		{
			int num = 0;
			for (IGroup parent = group.Parent; parent != null; parent = parent.Parent)
			{
				num++;
			}
			return num;
		}
	}
}
