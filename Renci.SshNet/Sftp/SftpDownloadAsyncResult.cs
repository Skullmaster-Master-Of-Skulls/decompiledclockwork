using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Sftp
{
	// Token: 0x02000033 RID: 51
	public class SftpDownloadAsyncResult : AsyncResult
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000EA5F File Offset: 0x0000CC5F
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x0000EA67 File Offset: 0x0000CC67
		public bool IsDownloadCanceled { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0000EA70 File Offset: 0x0000CC70
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x0000EA78 File Offset: 0x0000CC78
		public ulong DownloadedBytes { get; private set; }

		// Token: 0x060003E5 RID: 997 RVA: 0x0000EA81 File Offset: 0x0000CC81
		public SftpDownloadAsyncResult(AsyncCallback asyncCallback, object state) : base(asyncCallback, state)
		{
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000EA8B File Offset: 0x0000CC8B
		internal void Update(ulong downloadedBytes)
		{
			this.DownloadedBytes = downloadedBytes;
		}
	}
}
