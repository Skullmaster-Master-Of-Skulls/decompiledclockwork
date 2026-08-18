using System;
using System.Collections;

namespace OracleInternal.Common
{
	// Token: 0x02000088 RID: 136
	internal static class ODTSettings
	{
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000692 RID: 1682 RVA: 0x0003A5A0 File Offset: 0x000387A0
		// (remove) Token: 0x06000693 RID: 1683 RVA: 0x0003A5D4 File Offset: 0x000387D4
		public static event ODTSettings.EdmInUseEvent m_edmInUseEvent;

		// Token: 0x06000694 RID: 1684 RVA: 0x0003A608 File Offset: 0x00038808
		public static void FireEdmInUseEvent()
		{
			if (ODTSettings.m_edmInUseEvent != null)
			{
				ODTSettings.m_edmInUseEvent("ODP_EdmInUseEvent");
			}
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0003A620 File Offset: 0x00038820
		public static bool EdmEventHasSubscribers()
		{
			return ODTSettings.m_edmInUseEvent != null;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0003A62C File Offset: 0x0003882C
		public static void SetSchemaFilter(Hashtable schemaFilterHashtable)
		{
			if (schemaFilterHashtable != null)
			{
				ODTSettings.m_schemaFilterHashtable = schemaFilterHashtable;
			}
		}

		// Token: 0x0400079E RID: 1950
		public static bool m_bUse12cTypes;

		// Token: 0x0400079F RID: 1951
		public static bool m_bUse32DataTypes;

		// Token: 0x040007A0 RID: 1952
		public static bool m_bUseLongIdentifiers;

		// Token: 0x040007A1 RID: 1953
		public static Hashtable m_schemaFilterHashtable = null;

		// Token: 0x02000089 RID: 137
		// (Invoke) Token: 0x06000699 RID: 1689
		public delegate void EdmInUseEvent(object sender);
	}
}
