using System;

namespace NLog.Internal
{
	// Token: 0x0200008F RID: 143
	internal class FileCharacteristics
	{
		// Token: 0x060004A4 RID: 1188 RVA: 0x0000A524 File Offset: 0x00008724
		public FileCharacteristics(DateTime creationTimeUtc, DateTime lastWriteTimeUtc, long fileLength)
		{
			this.CreationTimeUtc = creationTimeUtc;
			this.LastWriteTimeUtc = lastWriteTimeUtc;
			this.FileLength = fileLength;
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x0000A541 File Offset: 0x00008741
		// (set) Token: 0x060004A6 RID: 1190 RVA: 0x0000A549 File Offset: 0x00008749
		public DateTime CreationTimeUtc { get; private set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0000A552 File Offset: 0x00008752
		// (set) Token: 0x060004A8 RID: 1192 RVA: 0x0000A55A File Offset: 0x0000875A
		public DateTime LastWriteTimeUtc { get; private set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x0000A563 File Offset: 0x00008763
		// (set) Token: 0x060004AA RID: 1194 RVA: 0x0000A56B File Offset: 0x0000876B
		public long FileLength { get; private set; }
	}
}
