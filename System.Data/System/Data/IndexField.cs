using System;

namespace System.Data
{
	// Token: 0x020000DB RID: 219
	internal struct IndexField
	{
		// Token: 0x06000D0F RID: 3343 RVA: 0x00214648 File Offset: 0x00213A48
		internal IndexField(DataColumn column, bool isDescending)
		{
			this.Column = column;
			this.IsDescending = isDescending;
		}

		// Token: 0x0400090C RID: 2316
		public readonly DataColumn Column;

		// Token: 0x0400090D RID: 2317
		public readonly bool IsDescending;
	}
}
