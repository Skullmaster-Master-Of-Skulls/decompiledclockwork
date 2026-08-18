using System;

namespace System.Threading
{
	// Token: 0x0200008F RID: 143
	internal class ReaderWriterCount
	{
		// Token: 0x040004A5 RID: 1189
		public long lockID;

		// Token: 0x040004A6 RID: 1190
		public int readercount;

		// Token: 0x040004A7 RID: 1191
		public int writercount;

		// Token: 0x040004A8 RID: 1192
		public int upgradecount;

		// Token: 0x040004A9 RID: 1193
		public ReaderWriterCount next;
	}
}
