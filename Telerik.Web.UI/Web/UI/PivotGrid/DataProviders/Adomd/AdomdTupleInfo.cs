using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AnalysisServices.AdomdClient;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.DataProviders.Adomd
{
	// Token: 0x02000D61 RID: 3425
	internal class AdomdTupleInfo : IOlapTuple
	{
		// Token: 0x06007FC3 RID: 32707 RVA: 0x001D2ECB File Offset: 0x001D10CB
		public AdomdTupleInfo()
		{
			this.members = new List<AdomdMemberInfo>();
		}

		// Token: 0x170028A7 RID: 10407
		// (get) Token: 0x06007FC4 RID: 32708 RVA: 0x001D2EDE File Offset: 0x001D10DE
		public IEnumerable<AdomdMemberInfo> Members
		{
			get
			{
				return this.members;
			}
		}

		// Token: 0x170028A8 RID: 10408
		// (get) Token: 0x06007FC5 RID: 32709 RVA: 0x001D2EE6 File Offset: 0x001D10E6
		IEnumerable IOlapTuple.Members
		{
			get
			{
				return this.members;
			}
		}

		// Token: 0x06007FC6 RID: 32710 RVA: 0x001D2EF0 File Offset: 0x001D10F0
		public static AdomdTupleInfo FromAdomdTuple(Tuple tupleElement)
		{
			AdomdTupleInfo adomdTupleInfo = new AdomdTupleInfo();
			foreach (Member memberElement in tupleElement.Members)
			{
				AdomdMemberInfo item = AdomdMemberInfo.FromAdomdMember(memberElement);
				adomdTupleInfo.members.Add(item);
			}
			return adomdTupleInfo;
		}

		// Token: 0x06007FC7 RID: 32711 RVA: 0x001D2F38 File Offset: 0x001D1138
		public static IEnumerable<AdomdTupleInfo> FromAdomdAxis(Axis axisElement)
		{
			List<AdomdTupleInfo> list = new List<AdomdTupleInfo>();
			foreach (Tuple tupleElement in axisElement.Set.Tuples)
			{
				AdomdTupleInfo item = AdomdTupleInfo.FromAdomdTuple(tupleElement);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x04002329 RID: 9001
		private List<AdomdMemberInfo> members;
	}
}
