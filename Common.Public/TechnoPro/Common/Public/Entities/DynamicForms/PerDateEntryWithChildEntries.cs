using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.DynamicForms
{
	// Token: 0x02000358 RID: 856
	public class PerDateEntryWithChildEntries : PerDateEntry
	{
		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x06001A97 RID: 6807 RVA: 0x0001EA00 File Offset: 0x0001CC00
		// (set) Token: 0x06001A98 RID: 6808 RVA: 0x0001EA08 File Offset: 0x0001CC08
		public IList<PerDateEntry> ChildEntries { get; set; }
	}
}
