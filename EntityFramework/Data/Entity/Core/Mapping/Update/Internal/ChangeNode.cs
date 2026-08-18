using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020003EA RID: 1002
	internal class ChangeNode
	{
		// Token: 0x06002507 RID: 9479 RVA: 0x000AECF4 File Offset: 0x000ACEF4
		internal ChangeNode(TypeUsage elementType)
		{
			this.m_elementType = elementType;
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06002508 RID: 9480 RVA: 0x000AED19 File Offset: 0x000ACF19
		internal TypeUsage ElementType
		{
			get
			{
				return this.m_elementType;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06002509 RID: 9481 RVA: 0x000AED21 File Offset: 0x000ACF21
		internal List<PropagatorResult> Inserted
		{
			get
			{
				return this.m_inserted;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x0600250A RID: 9482 RVA: 0x000AED29 File Offset: 0x000ACF29
		internal List<PropagatorResult> Deleted
		{
			get
			{
				return this.m_deleted;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x0600250B RID: 9483 RVA: 0x000AED31 File Offset: 0x000ACF31
		// (set) Token: 0x0600250C RID: 9484 RVA: 0x000AED39 File Offset: 0x000ACF39
		internal PropagatorResult Placeholder { get; set; }

		// Token: 0x04000DBC RID: 3516
		private readonly TypeUsage m_elementType;

		// Token: 0x04000DBD RID: 3517
		private readonly List<PropagatorResult> m_inserted = new List<PropagatorResult>();

		// Token: 0x04000DBE RID: 3518
		private readonly List<PropagatorResult> m_deleted = new List<PropagatorResult>();
	}
}
