using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.AdomdClient;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.DataProviders.Adomd
{
	// Token: 0x02000D60 RID: 3424
	internal class AdomdMemberInfo : IOlapMember, IOlapElement
	{
		// Token: 0x170028A1 RID: 10401
		// (get) Token: 0x06007FB5 RID: 32693 RVA: 0x001D2D83 File Offset: 0x001D0F83
		// (set) Token: 0x06007FB6 RID: 32694 RVA: 0x001D2D8B File Offset: 0x001D0F8B
		public string UniqueName { get; private set; }

		// Token: 0x170028A2 RID: 10402
		// (get) Token: 0x06007FB7 RID: 32695 RVA: 0x001D2D94 File Offset: 0x001D0F94
		// (set) Token: 0x06007FB8 RID: 32696 RVA: 0x001D2D9C File Offset: 0x001D0F9C
		public string Caption { get; private set; }

		// Token: 0x170028A3 RID: 10403
		// (get) Token: 0x06007FB9 RID: 32697 RVA: 0x001D2DA5 File Offset: 0x001D0FA5
		// (set) Token: 0x06007FBA RID: 32698 RVA: 0x001D2DAD File Offset: 0x001D0FAD
		public string HierarchyName { get; private set; }

		// Token: 0x170028A4 RID: 10404
		// (get) Token: 0x06007FBB RID: 32699 RVA: 0x001D2DB6 File Offset: 0x001D0FB6
		// (set) Token: 0x06007FBC RID: 32700 RVA: 0x001D2DBE File Offset: 0x001D0FBE
		public int LevelNumber { get; private set; }

		// Token: 0x170028A5 RID: 10405
		// (get) Token: 0x06007FBD RID: 32701 RVA: 0x001D2DC7 File Offset: 0x001D0FC7
		// (set) Token: 0x06007FBE RID: 32702 RVA: 0x001D2DCF File Offset: 0x001D0FCF
		public string LevelName { get; private set; }

		// Token: 0x170028A6 RID: 10406
		// (get) Token: 0x06007FBF RID: 32703 RVA: 0x001D2DD8 File Offset: 0x001D0FD8
		// (set) Token: 0x06007FC0 RID: 32704 RVA: 0x001D2DE0 File Offset: 0x001D0FE0
		public IList<string> SortKeys { get; private set; }

		// Token: 0x06007FC1 RID: 32705 RVA: 0x001D2DEC File Offset: 0x001D0FEC
		public static AdomdMemberInfo FromAdomdMember(Member memberElement)
		{
			AdomdMemberInfo adomdMemberInfo = new AdomdMemberInfo();
			adomdMemberInfo.Caption = memberElement.Caption;
			adomdMemberInfo.UniqueName = memberElement.UniqueName;
			adomdMemberInfo.LevelName = memberElement.LevelName;
			adomdMemberInfo.LevelNumber = memberElement.LevelDepth;
			adomdMemberInfo.HierarchyName = (string)memberElement.MemberProperties["HIERARCHY_UNIQUE_NAME"].Value;
			MemberPropertyCollection.Enumerator enumerator = memberElement.MemberProperties.GetEnumerator();
			List<string> list = new List<string>();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Name.StartsWith("KEY", StringComparison.Ordinal))
				{
					object value = enumerator.Current.Value;
					if (value != null)
					{
						string item = enumerator.Current.Value.ToString();
						list.Add(item);
					}
				}
			}
			if (list.Count > 0)
			{
				adomdMemberInfo.SortKeys = list;
			}
			return adomdMemberInfo;
		}
	}
}
