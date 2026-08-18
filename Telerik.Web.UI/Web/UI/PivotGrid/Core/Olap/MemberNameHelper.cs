using System;
using System.Globalization;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000CFD RID: 3325
	internal static class MemberNameHelper
	{
		// Token: 0x06007C17 RID: 31767 RVA: 0x001C877A File Offset: 0x001C697A
		public static string GetMemberWithBrackets(string originalMember)
		{
			if (string.IsNullOrEmpty(originalMember))
			{
				return originalMember;
			}
			return MemberNameHelper.PlaceBracketsIfNecessary(originalMember);
		}

		// Token: 0x06007C18 RID: 31768 RVA: 0x001C878C File Offset: 0x001C698C
		private static string PlaceBracketsIfNecessary(string elementName)
		{
			bool flag = elementName.StartsWith("[", StringComparison.OrdinalIgnoreCase) && elementName.EndsWith("]", StringComparison.OrdinalIgnoreCase);
			if (flag)
			{
				return elementName;
			}
			return string.Format(CultureInfo.InvariantCulture, "[{0}]", new object[]
			{
				elementName
			});
		}
	}
}
