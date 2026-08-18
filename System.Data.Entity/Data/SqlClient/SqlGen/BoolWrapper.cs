using System;

namespace System.Data.SqlClient.SqlGen
{
	// Token: 0x0200003C RID: 60
	internal class BoolWrapper
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x000177BA File Offset: 0x000159BA
		// (set) Token: 0x06000548 RID: 1352 RVA: 0x000177C2 File Offset: 0x000159C2
		internal bool Value { get; set; }

		// Token: 0x06000549 RID: 1353 RVA: 0x000177CB File Offset: 0x000159CB
		internal BoolWrapper()
		{
			this.Value = false;
		}
	}
}
