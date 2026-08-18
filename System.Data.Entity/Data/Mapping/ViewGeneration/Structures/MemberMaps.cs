using System;

namespace System.Data.Mapping.ViewGeneration.Structures
{
	// Token: 0x020002AF RID: 687
	internal class MemberMaps
	{
		// Token: 0x060028E0 RID: 10464 RVA: 0x0009E5C0 File Offset: 0x0009C7C0
		internal MemberMaps(ViewTarget viewTarget, MemberProjectionIndex projectedSlotMap, MemberDomainMap queryDomainMap, MemberDomainMap updateDomainMap)
		{
			this.m_projectedSlotMap = projectedSlotMap;
			this.m_queryDomainMap = queryDomainMap;
			this.m_updateDomainMap = updateDomainMap;
			this.m_viewTarget = viewTarget;
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x060028E1 RID: 10465 RVA: 0x0009E5E5 File Offset: 0x0009C7E5
		internal MemberProjectionIndex ProjectedSlotMap
		{
			get
			{
				return this.m_projectedSlotMap;
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x060028E2 RID: 10466 RVA: 0x0009E5ED File Offset: 0x0009C7ED
		internal MemberDomainMap QueryDomainMap
		{
			get
			{
				return this.m_queryDomainMap;
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x060028E3 RID: 10467 RVA: 0x0009E5F5 File Offset: 0x0009C7F5
		internal MemberDomainMap UpdateDomainMap
		{
			get
			{
				return this.m_updateDomainMap;
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x060028E4 RID: 10468 RVA: 0x0009E5FD File Offset: 0x0009C7FD
		internal MemberDomainMap RightDomainMap
		{
			get
			{
				if (this.m_viewTarget != ViewTarget.QueryView)
				{
					return this.m_queryDomainMap;
				}
				return this.m_updateDomainMap;
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x060028E5 RID: 10469 RVA: 0x0009E614 File Offset: 0x0009C814
		internal MemberDomainMap LeftDomainMap
		{
			get
			{
				if (this.m_viewTarget != ViewTarget.QueryView)
				{
					return this.m_updateDomainMap;
				}
				return this.m_queryDomainMap;
			}
		}

		// Token: 0x04001270 RID: 4720
		private MemberProjectionIndex m_projectedSlotMap;

		// Token: 0x04001271 RID: 4721
		private MemberDomainMap m_queryDomainMap;

		// Token: 0x04001272 RID: 4722
		private MemberDomainMap m_updateDomainMap;

		// Token: 0x04001273 RID: 4723
		private ViewTarget m_viewTarget;
	}
}
