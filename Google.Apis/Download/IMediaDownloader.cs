using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Google.Apis.Download
{
	// Token: 0x0200000D RID: 13
	public interface IMediaDownloader
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000059 RID: 89
		// (remove) Token: 0x0600005A RID: 90
		event Action<IDownloadProgress> ProgressChanged;

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600005B RID: 91
		// (set) Token: 0x0600005C RID: 92
		int ChunkSize { get; set; }

		// Token: 0x0600005D RID: 93
		IDownloadProgress Download(string url, Stream stream);

		// Token: 0x0600005E RID: 94
		Task<IDownloadProgress> DownloadAsync(string url, Stream stream);

		// Token: 0x0600005F RID: 95
		Task<IDownloadProgress> DownloadAsync(string url, Stream stream, CancellationToken cancellationToken);
	}
}
