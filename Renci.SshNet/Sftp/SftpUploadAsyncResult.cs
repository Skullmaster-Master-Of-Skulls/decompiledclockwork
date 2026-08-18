using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Sftp
{
	// Token: 0x0200003D RID: 61
	public class SftpUploadAsyncResult : AsyncResult
	{
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x00011F7C File Offset: 0x0001017C
		// (set) Token: 0x060004C5 RID: 1221 RVA: 0x00011F84 File Offset: 0x00010184
		public bool IsUploadCanceled { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00011F8D File Offset: 0x0001018D
		// (set) Token: 0x060004C7 RID: 1223 RVA: 0x00011F95 File Offset: 0x00010195
		public ulong UploadedBytes { get; private set; }

		// Token: 0x060004C8 RID: 1224 RVA: 0x0000EA81 File Offset: 0x0000CC81
		public SftpUploadAsyncResult(AsyncCallback asyncCallback, object state) : base(asyncCallback, state)
		{
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00011F9E File Offset: 0x0001019E
		internal void Update(ulong uploadedBytes)
		{
			this.UploadedBytes = uploadedBytes;
		}
	}
}
