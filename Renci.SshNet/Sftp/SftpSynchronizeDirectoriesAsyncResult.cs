using System;
using System.Collections.Generic;
using System.IO;
using Renci.SshNet.Common;

namespace Renci.SshNet.Sftp
{
	// Token: 0x0200003C RID: 60
	public class SftpSynchronizeDirectoriesAsyncResult : AsyncResult<IEnumerable<FileInfo>>
	{
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x00011F58 File Offset: 0x00010158
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x00011F60 File Offset: 0x00010160
		public int FilesRead { get; private set; }

		// Token: 0x060004C2 RID: 1218 RVA: 0x00011F69 File Offset: 0x00010169
		public SftpSynchronizeDirectoriesAsyncResult(AsyncCallback asyncCallback, object state) : base(asyncCallback, state)
		{
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00011F73 File Offset: 0x00010173
		internal void Update(int filesRead)
		{
			this.FilesRead = filesRead;
		}
	}
}
