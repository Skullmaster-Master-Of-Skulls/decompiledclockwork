using System;
using System.Collections.Generic;

namespace WebGrease.Configuration
{
	// Token: 0x0200002B RID: 43
	public class ResourcePivotGroup
	{
		// Token: 0x06000307 RID: 775 RVA: 0x000076A8 File Offset: 0x000058A8
		public ResourcePivotGroup(string key, ResourcePivotApplyMode applyMode, IEnumerable<string> keys)
		{
			this.Key = key;
			this.ApplyMode = applyMode;
			this.Keys = new HashSet<string>(keys);
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000308 RID: 776 RVA: 0x000076CA File Offset: 0x000058CA
		// (set) Token: 0x06000309 RID: 777 RVA: 0x000076D2 File Offset: 0x000058D2
		public ResourcePivotApplyMode ApplyMode { get; private set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600030A RID: 778 RVA: 0x000076DB File Offset: 0x000058DB
		// (set) Token: 0x0600030B RID: 779 RVA: 0x000076E3 File Offset: 0x000058E3
		public string Key { get; private set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600030C RID: 780 RVA: 0x000076EC File Offset: 0x000058EC
		// (set) Token: 0x0600030D RID: 781 RVA: 0x000076F4 File Offset: 0x000058F4
		public HashSet<string> Keys { get; private set; }
	}
}
