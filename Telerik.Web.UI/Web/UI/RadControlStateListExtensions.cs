using System;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Web.UI
{
	// Token: 0x02000C1F RID: 3103
	public static class RadControlStateListExtensions
	{
		// Token: 0x06007610 RID: 30224 RVA: 0x001B69C4 File Offset: 0x001B4BC4
		public static RadControlState FindByUniqueId(this List<RadControlState> rcs, string uniqueId)
		{
			return rcs.FirstOrDefault((RadControlState cs) => cs.UniqueId == uniqueId);
		}

		// Token: 0x06007611 RID: 30225 RVA: 0x001B6A0C File Offset: 0x001B4C0C
		public static RadControlState FindByUniqueKey(this List<RadControlState> rcs, string uniqueKey)
		{
			return rcs.FirstOrDefault((RadControlState cs) => cs.UniqueKey == uniqueKey);
		}

		// Token: 0x06007612 RID: 30226 RVA: 0x001B6A54 File Offset: 0x001B4C54
		public static bool RemoveByUniqueId(this List<RadControlState> rcs, string uniqueId)
		{
			RadControlState radControlState = rcs.FirstOrDefault((RadControlState cs) => cs.UniqueId == uniqueId);
			return radControlState != null && rcs.Remove(radControlState);
		}

		// Token: 0x06007613 RID: 30227 RVA: 0x001B6AA8 File Offset: 0x001B4CA8
		public static bool RemoveByUniqueKey(this List<RadControlState> rcs, string uniqueKey)
		{
			RadControlState radControlState = rcs.FirstOrDefault((RadControlState cs) => cs.UniqueKey == uniqueKey);
			return radControlState != null && rcs.Remove(radControlState);
		}
	}
}
