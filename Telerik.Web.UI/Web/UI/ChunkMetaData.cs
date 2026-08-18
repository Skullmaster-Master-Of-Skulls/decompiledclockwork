using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000079 RID: 121
	public class ChunkMetaData
	{
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x0000C81B File Offset: 0x0000AA1B
		// (set) Token: 0x060004F1 RID: 1265 RVA: 0x0000C823 File Offset: 0x0000AA23
		public string UploadID
		{
			get
			{
				return this.uploadId;
			}
			set
			{
				this.uploadId = value + ".tmp";
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x0000C836 File Offset: 0x0000AA36
		// (set) Token: 0x060004F3 RID: 1267 RVA: 0x0000C83E File Offset: 0x0000AA3E
		public int ChunkIndex { get; set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0000C847 File Offset: 0x0000AA47
		// (set) Token: 0x060004F5 RID: 1269 RVA: 0x0000C84F File Offset: 0x0000AA4F
		public int TotalChunks { get; set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x0000C858 File Offset: 0x0000AA58
		// (set) Token: 0x060004F7 RID: 1271 RVA: 0x0000C860 File Offset: 0x0000AA60
		public long TotalFileSize { get; set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x0000C869 File Offset: 0x0000AA69
		// (set) Token: 0x060004F9 RID: 1273 RVA: 0x0000C871 File Offset: 0x0000AA71
		public bool IsSingleChunkUpload { get; set; }

		// Token: 0x060004FA RID: 1274 RVA: 0x0000C87A File Offset: 0x0000AA7A
		public ChunkMetaData()
		{
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0000C882 File Offset: 0x0000AA82
		public ChunkMetaData(int chunkIndex, int totalChunks, string guid)
		{
			this.UploadID = guid;
			this.ChunkIndex = chunkIndex;
			this.TotalChunks = totalChunks;
			this.IsSingleChunkUpload = (this.TotalChunks == 1);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000C8AE File Offset: 0x0000AAAE
		public ChunkMetaData(int chunkIndex, int totalChunks, long totalFileSize, string guid) : this(chunkIndex, totalChunks, guid)
		{
			this.TotalFileSize = totalFileSize;
		}

		// Token: 0x040000AC RID: 172
		internal const string Temp_Files_Extension = ".tmp";

		// Token: 0x040000AD RID: 173
		private string uploadId;
	}
}
