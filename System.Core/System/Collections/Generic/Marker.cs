using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x0200009B RID: 155
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	internal struct Marker
	{
		// Token: 0x06000434 RID: 1076 RVA: 0x0000BFDB File Offset: 0x0000A1DB
		public Marker(int count, int index)
		{
			this.Count = count;
			this.Index = index;
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x0000BFEB File Offset: 0x0000A1EB
		public int Count { get; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0000BFF3 File Offset: 0x0000A1F3
		public int Index { get; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0000BFFB File Offset: 0x0000A1FB
		private string DebuggerDisplay
		{
			get
			{
				return string.Format("{0}: {1}, {2}: {3}", new object[]
				{
					"Index",
					this.Index,
					"Count",
					this.Count
				});
			}
		}
	}
}
