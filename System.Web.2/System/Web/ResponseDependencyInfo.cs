using System;

namespace System.Web
{
	// Token: 0x020000B3 RID: 179
	internal class ResponseDependencyInfo
	{
		// Token: 0x06000BF1 RID: 3057 RVA: 0x0001F68C File Offset: 0x0001D88C
		internal ResponseDependencyInfo(string[] items, DateTime utcDate)
		{
			this.items = items;
			this.utcDate = utcDate;
		}

		// Token: 0x0400046F RID: 1135
		internal readonly string[] items;

		// Token: 0x04000470 RID: 1136
		internal readonly DateTime utcDate;
	}
}
