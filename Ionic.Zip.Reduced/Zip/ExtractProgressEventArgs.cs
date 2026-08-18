using System;

namespace Ionic.Zip
{
	// Token: 0x0200000D RID: 13
	public class ExtractProgressEventArgs : ZipProgressEventArgs
	{
		// Token: 0x0600003B RID: 59 RVA: 0x0000236B File Offset: 0x0000056B
		internal ExtractProgressEventArgs(string archiveName, bool before, int entriesTotal, int entriesExtracted, ZipEntry entry, string extractLocation) : base(archiveName, before ? ZipProgressEventType.Extracting_BeforeExtractEntry : ZipProgressEventType.Extracting_AfterExtractEntry)
		{
			base.EntriesTotal = entriesTotal;
			base.CurrentEntry = entry;
			this._entriesExtracted = entriesExtracted;
			this._target = extractLocation;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000239C File Offset: 0x0000059C
		internal ExtractProgressEventArgs(string archiveName, ZipProgressEventType flavor) : base(archiveName, flavor)
		{
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000023A6 File Offset: 0x000005A6
		internal ExtractProgressEventArgs()
		{
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000023B0 File Offset: 0x000005B0
		internal static ExtractProgressEventArgs BeforeExtractEntry(string archiveName, ZipEntry entry, string extractLocation)
		{
			return new ExtractProgressEventArgs
			{
				ArchiveName = archiveName,
				EventType = ZipProgressEventType.Extracting_BeforeExtractEntry,
				CurrentEntry = entry,
				_target = extractLocation
			};
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000023E4 File Offset: 0x000005E4
		internal static ExtractProgressEventArgs ExtractExisting(string archiveName, ZipEntry entry, string extractLocation)
		{
			return new ExtractProgressEventArgs
			{
				ArchiveName = archiveName,
				EventType = ZipProgressEventType.Extracting_ExtractEntryWouldOverwrite,
				CurrentEntry = entry,
				_target = extractLocation
			};
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002418 File Offset: 0x00000618
		internal static ExtractProgressEventArgs AfterExtractEntry(string archiveName, ZipEntry entry, string extractLocation)
		{
			return new ExtractProgressEventArgs
			{
				ArchiveName = archiveName,
				EventType = ZipProgressEventType.Extracting_AfterExtractEntry,
				CurrentEntry = entry,
				_target = extractLocation
			};
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000244C File Offset: 0x0000064C
		internal static ExtractProgressEventArgs ExtractAllStarted(string archiveName, string extractLocation)
		{
			return new ExtractProgressEventArgs(archiveName, ZipProgressEventType.Extracting_BeforeExtractAll)
			{
				_target = extractLocation
			};
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000246C File Offset: 0x0000066C
		internal static ExtractProgressEventArgs ExtractAllCompleted(string archiveName, string extractLocation)
		{
			return new ExtractProgressEventArgs(archiveName, ZipProgressEventType.Extracting_AfterExtractAll)
			{
				_target = extractLocation
			};
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000248C File Offset: 0x0000068C
		internal static ExtractProgressEventArgs ByteUpdate(string archiveName, ZipEntry entry, long bytesWritten, long totalBytes)
		{
			return new ExtractProgressEventArgs(archiveName, ZipProgressEventType.Extracting_EntryBytesWritten)
			{
				ArchiveName = archiveName,
				CurrentEntry = entry,
				BytesTransferred = bytesWritten,
				TotalBytesToTransfer = totalBytes
			};
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000024BF File Offset: 0x000006BF
		public int EntriesExtracted
		{
			get
			{
				return this._entriesExtracted;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000024C7 File Offset: 0x000006C7
		public string ExtractLocation
		{
			get
			{
				return this._target;
			}
		}

		// Token: 0x04000028 RID: 40
		private int _entriesExtracted;

		// Token: 0x04000029 RID: 41
		private string _target;
	}
}
