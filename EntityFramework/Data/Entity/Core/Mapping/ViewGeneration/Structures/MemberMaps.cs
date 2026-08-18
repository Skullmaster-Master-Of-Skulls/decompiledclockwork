using System;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x0200047C RID: 1148
	internal class MemberMaps
	{
		// Token: 0x06002A51 RID: 10833 RVA: 0x000CC531 File Offset: 0x000CA731
		internal MemberMaps(ViewTarget viewTarget, MemberProjectionIndex projectedSlotMap, MemberDomainMap queryDomainMap, MemberDomainMap updateDomainMap)
		{
			this.m_projectedSlotMap = projectedSlotMap;
			this.m_queryDomainMap = queryDomainMap;
			this.m_updateDomainMap = updateDomainMap;
			this.m_viewTarget = viewTarget;
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06002A52 RID: 10834 RVA: 0x000CC556 File Offset: 0x000CA756
		internal MemberProjectionIndex ProjectedSlotMap
		{
			get
			{
				return this.m_projectedSlotMap;
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06002A53 RID: 10835 RVA: 0x000CC55E File Offset: 0x000CA75E
		internal MemberDomainMap QueryDomainMap
		{
			get
			{
				return this.m_queryDomainMap;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06002A54 RID: 10836 RVA: 0x000CC566 File Offset: 0x000CA766
		internal MemberDomainMap UpdateDomainMap
		{
			get
			{
				return this.m_updateDomainMap;
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06002A55 RID: 10837 RVA: 0x000CC56E File Offset: 0x000CA76E
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

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06002A56 RID: 10838 RVA: 0x000CC585 File Offset: 0x000CA785
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

		// Token: 0x04000FA7 RID: 4007
		private readonly MemberProjectionIndex m_projectedSlotMap;

		// Token: 0x04000FA8 RID: 4008
		private readonly MemberDomainMap m_queryDomainMap;

		// Token: 0x04000FA9 RID: 4009
		private readonly MemberDomainMap m_updateDomainMap;

		// Token: 0x04000FAA RID: 4010
		private readonly ViewTarget m_viewTarget;
	}
}
