using System;

namespace Ionic.Zip
{
	// Token: 0x02000009 RID: 9
	public class ZipProgressEventArgs : EventArgs
	{
		// Token: 0x06000018 RID: 24 RVA: 0x0000210E File Offset: 0x0000030E
		internal ZipProgressEventArgs()
		{
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002116 File Offset: 0x00000316
		internal ZipProgressEventArgs(string archiveName, ZipProgressEventType flavor)
		{
			this._archiveName = archiveName;
			this._flavor = flavor;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000212C File Offset: 0x0000032C
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002134 File Offset: 0x00000334
		public int EntriesTotal
		{
			get
			{
				return this._entriesTotal;
			}
			set
			{
				this._entriesTotal = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000213D File Offset: 0x0000033D
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002145 File Offset: 0x00000345
		public ZipEntry CurrentEntry
		{
			get
			{
				return this._latestEntry;
			}
			set
			{
				this._latestEntry = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000214E File Offset: 0x0000034E
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002156 File Offset: 0x00000356
		public bool Cancel
		{
			get
			{
				return this._cancel;
			}
			set
			{
				this._cancel = (this._cancel || value);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002172 File Offset: 0x00000372
		public ZipProgressEventType EventType
		{
			get
			{
				return this._flavor;
			}
			set
			{
				this._flavor = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000217B File Offset: 0x0000037B
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002183 File Offset: 0x00000383
		public string ArchiveName
		{
			get
			{
				return this._archiveName;
			}
			set
			{
				this._archiveName = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000218C File Offset: 0x0000038C
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002194 File Offset: 0x00000394
		public long BytesTransferred
		{
			get
			{
				return this._bytesTransferred;
			}
			set
			{
				this._bytesTransferred = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000219D File Offset: 0x0000039D
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000021A5 File Offset: 0x000003A5
		public long TotalBytesToTransfer
		{
			get
			{
				return this._totalBytesToTransfer;
			}
			set
			{
				this._totalBytesToTransfer = value;
			}
		}

		// Token: 0x04000020 RID: 32
		private int _entriesTotal;

		// Token: 0x04000021 RID: 33
		private bool _cancel;

		// Token: 0x04000022 RID: 34
		private ZipEntry _latestEntry;

		// Token: 0x04000023 RID: 35
		private ZipProgressEventType _flavor;

		// Token: 0x04000024 RID: 36
		private string _archiveName;

		// Token: 0x04000025 RID: 37
		private long _bytesTransferred;

		// Token: 0x04000026 RID: 38
		private long _totalBytesToTransfer;
	}
}
