using System;

namespace Ionic.Zip
{
	// Token: 0x0200000A RID: 10
	public class ReadProgressEventArgs : ZipProgressEventArgs
	{
		// Token: 0x06000028 RID: 40 RVA: 0x000021AE File Offset: 0x000003AE
		internal ReadProgressEventArgs()
		{
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000021B6 File Offset: 0x000003B6
		private ReadProgressEventArgs(string archiveName, ZipProgressEventType flavor) : base(archiveName, flavor)
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000021C0 File Offset: 0x000003C0
		internal static ReadProgressEventArgs Before(string archiveName, int entriesTotal)
		{
			return new ReadProgressEventArgs(archiveName, ZipProgressEventType.Reading_BeforeReadEntry)
			{
				EntriesTotal = entriesTotal
			};
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000021E0 File Offset: 0x000003E0
		internal static ReadProgressEventArgs After(string archiveName, ZipEntry entry, int entriesTotal)
		{
			return new ReadProgressEventArgs(archiveName, ZipProgressEventType.Reading_AfterReadEntry)
			{
				EntriesTotal = entriesTotal,
				CurrentEntry = entry
			};
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002204 File Offset: 0x00000404
		internal static ReadProgressEventArgs Started(string archiveName)
		{
			return new ReadProgressEventArgs(archiveName, ZipProgressEventType.Reading_Started);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000221C File Offset: 0x0000041C
		internal static ReadProgressEventArgs ByteUpdate(string archiveName, ZipEntry entry, long bytesXferred, long totalBytes)
		{
			return new ReadProgressEventArgs(archiveName, ZipProgressEventType.Reading_ArchiveBytesRead)
			{
				CurrentEntry = entry,
				BytesTransferred = bytesXferred,
				TotalBytesToTransfer = totalBytes
			};
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002248 File Offset: 0x00000448
		internal static ReadProgressEventArgs Completed(string archiveName)
		{
			return new ReadProgressEventArgs(archiveName, ZipProgressEventType.Reading_Completed);
		}
	}
}
