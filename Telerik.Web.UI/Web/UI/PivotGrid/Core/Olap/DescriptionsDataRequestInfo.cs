using System;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000CF9 RID: 3321
	internal class DescriptionsDataRequestInfo
	{
		// Token: 0x06007BEB RID: 31723 RVA: 0x001C7C27 File Offset: 0x001C5E27
		public DescriptionsDataRequestInfo(object state, OlapElementsFilterCondition filtercondition)
		{
			this.State = state;
			this.FilterCondition = filtercondition;
		}

		// Token: 0x1700279C RID: 10140
		// (get) Token: 0x06007BEC RID: 31724 RVA: 0x001C7C3D File Offset: 0x001C5E3D
		// (set) Token: 0x06007BED RID: 31725 RVA: 0x001C7C45 File Offset: 0x001C5E45
		public object State { get; private set; }

		// Token: 0x1700279D RID: 10141
		// (get) Token: 0x06007BEE RID: 31726 RVA: 0x001C7C4E File Offset: 0x001C5E4E
		// (set) Token: 0x06007BEF RID: 31727 RVA: 0x001C7C56 File Offset: 0x001C5E56
		public OlapElementsFilterCondition FilterCondition { get; private set; }
	}
}
