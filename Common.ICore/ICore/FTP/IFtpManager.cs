using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.FTP;

namespace TechnoPro.Common.ICore.FTP
{
	// Token: 0x0200008C RID: 140
	public interface IFtpManager
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060003E5 RID: 997
		// (set) Token: 0x060003E6 RID: 998
		FtpConnectionInfo ConnectionInfo { get; set; }

		// Token: 0x060003E7 RID: 999
		bool DownloadFile(string remoteFilename, string localFilename);

		// Token: 0x060003E8 RID: 1000
		bool UploadFile(string localFilename, string remoteFilename);

		// Token: 0x060003E9 RID: 1001
		bool AppendAllLines(string remoteFilename, IEnumerable<string> lines);

		// Token: 0x060003EA RID: 1002
		bool AppendAllText(string remoteFilename, string text);

		// Token: 0x060003EB RID: 1003
		bool CreateDirectory(string remoteFolder);

		// Token: 0x060003EC RID: 1004
		string ReadAllText(string remoteFilename);

		// Token: 0x060003ED RID: 1005
		bool Exists(string remotePath);

		// Token: 0x060003EE RID: 1006
		bool FileExists(string remotePath, string remoteFilename);

		// Token: 0x060003EF RID: 1007
		IList<FtpFileInfo> GetFilesInfo();
	}
}
