using System;

namespace Renci.SshNet.Sftp
{
	// Token: 0x02000037 RID: 55
	public class SftpFileSytemInformation
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x00010596 File Offset: 0x0000E796
		// (set) Token: 0x06000473 RID: 1139 RVA: 0x0001059E File Offset: 0x0000E79E
		public ulong FileSystemBlockSize { get; private set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x000105A7 File Offset: 0x0000E7A7
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x000105AF File Offset: 0x0000E7AF
		public ulong BlockSize { get; private set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x000105B8 File Offset: 0x0000E7B8
		// (set) Token: 0x06000477 RID: 1143 RVA: 0x000105C0 File Offset: 0x0000E7C0
		public ulong TotalBlocks { get; private set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x000105C9 File Offset: 0x0000E7C9
		// (set) Token: 0x06000479 RID: 1145 RVA: 0x000105D1 File Offset: 0x0000E7D1
		public ulong FreeBlocks { get; private set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x000105DA File Offset: 0x0000E7DA
		// (set) Token: 0x0600047B RID: 1147 RVA: 0x000105E2 File Offset: 0x0000E7E2
		public ulong AvailableBlocks { get; private set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x000105EB File Offset: 0x0000E7EB
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x000105F3 File Offset: 0x0000E7F3
		public ulong TotalNodes { get; private set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x000105FC File Offset: 0x0000E7FC
		// (set) Token: 0x0600047F RID: 1151 RVA: 0x00010604 File Offset: 0x0000E804
		public ulong FreeNodes { get; private set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0001060D File Offset: 0x0000E80D
		// (set) Token: 0x06000481 RID: 1153 RVA: 0x00010615 File Offset: 0x0000E815
		public ulong AvailableNodes { get; private set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x0001061E File Offset: 0x0000E81E
		// (set) Token: 0x06000483 RID: 1155 RVA: 0x00010626 File Offset: 0x0000E826
		public ulong Sid { get; private set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x0001062F File Offset: 0x0000E82F
		public bool IsReadOnly
		{
			get
			{
				return (this._flag & 1UL) == 1UL;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0001063E File Offset: 0x0000E83E
		public bool SupportsSetUid
		{
			get
			{
				return (this._flag & 2UL) == 0UL;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x0001064D File Offset: 0x0000E84D
		// (set) Token: 0x06000487 RID: 1159 RVA: 0x00010655 File Offset: 0x0000E855
		public ulong MaxNameLenght { get; private set; }

		// Token: 0x06000488 RID: 1160 RVA: 0x00010660 File Offset: 0x0000E860
		internal SftpFileSytemInformation(ulong bsize, ulong frsize, ulong blocks, ulong bfree, ulong bavail, ulong files, ulong ffree, ulong favail, ulong sid, ulong flag, ulong namemax)
		{
			this.FileSystemBlockSize = bsize;
			this.BlockSize = frsize;
			this.TotalBlocks = blocks;
			this.FreeBlocks = bfree;
			this.AvailableBlocks = bavail;
			this.TotalNodes = files;
			this.FreeNodes = ffree;
			this.AvailableNodes = favail;
			this.Sid = sid;
			this._flag = flag;
			this.MaxNameLenght = namemax;
		}

		// Token: 0x0400017E RID: 382
		internal const ulong SSH_FXE_STATVFS_ST_RDONLY = 1UL;

		// Token: 0x0400017F RID: 383
		internal const ulong SSH_FXE_STATVFS_ST_NOSUID = 2UL;

		// Token: 0x04000180 RID: 384
		private readonly ulong _flag;
	}
}
