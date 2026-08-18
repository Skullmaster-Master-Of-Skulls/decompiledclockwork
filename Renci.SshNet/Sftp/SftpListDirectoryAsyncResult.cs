using System;
using System.Collections.Generic;
using Renci.SshNet.Common;

namespace Renci.SshNet.Sftp
{
	// Token: 0x02000038 RID: 56
	public class SftpListDirectoryAsyncResult : AsyncResult<IEnumerable<SftpFile>>
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x000106C8 File Offset: 0x0000E8C8
		// (set) Token: 0x0600048A RID: 1162 RVA: 0x000106D0 File Offset: 0x0000E8D0
		public int FilesRead { get; private set; }

		// Token: 0x0600048B RID: 1163 RVA: 0x000106D9 File Offset: 0x0000E8D9
		public SftpListDirectoryAsyncResult(AsyncCallback asyncCallback, object state) : base(asyncCallback, state)
		{
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x000106E3 File Offset: 0x0000E8E3
		internal void Update(int filesRead)
		{
			this.FilesRead = filesRead;
		}
	}
}
