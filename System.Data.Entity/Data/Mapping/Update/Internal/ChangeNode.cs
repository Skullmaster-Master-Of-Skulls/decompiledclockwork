using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;

namespace System.Data.Mapping.Update.Internal
{
	// Token: 0x020002C5 RID: 709
	internal class ChangeNode
	{
		// Token: 0x060029E6 RID: 10726 RVA: 0x000A3F08 File Offset: 0x000A2108
		internal ChangeNode(TypeUsage elementType)
		{
			this.m_elementType = elementType;
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x060029E7 RID: 10727 RVA: 0x000A3F2D File Offset: 0x000A212D
		internal TypeUsage ElementType
		{
			get
			{
				return this.m_elementType;
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x060029E8 RID: 10728 RVA: 0x000A3F35 File Offset: 0x000A2135
		internal List<PropagatorResult> Inserted
		{
			get
			{
				return this.m_inserted;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x060029E9 RID: 10729 RVA: 0x000A3F3D File Offset: 0x000A213D
		internal List<PropagatorResult> Deleted
		{
			get
			{
				return this.m_deleted;
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x060029EA RID: 10730 RVA: 0x000A3F45 File Offset: 0x000A2145
		// (set) Token: 0x060029EB RID: 10731 RVA: 0x000A3F4D File Offset: 0x000A214D
		internal PropagatorResult Placeholder
		{
			get
			{
				return this.m_placeholder;
			}
			set
			{
				this.m_placeholder = value;
			}
		}

		// Token: 0x040012AD RID: 4781
		private TypeUsage m_elementType;

		// Token: 0x040012AE RID: 4782
		private List<PropagatorResult> m_inserted = new List<PropagatorResult>();

		// Token: 0x040012AF RID: 4783
		private List<PropagatorResult> m_deleted = new List<PropagatorResult>();

		// Token: 0x040012B0 RID: 4784
		private PropagatorResult m_placeholder;
	}
}
