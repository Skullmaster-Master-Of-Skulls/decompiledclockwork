using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.FTP;

namespace TechnoPro.Common.DAO.FTP
{
	// Token: 0x02000071 RID: 113
	public interface IFtpDAO
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x060002B5 RID: 693
		// (set) Token: 0x060002B6 RID: 694
		FtpConnectionInfo ConnectionInfo { get; set; }

		// Token: 0x060002B7 RID: 695
		bool DownloadFile(string remoteFilename, string localFilename);

		// Token: 0x060002B8 RID: 696
		bool UploadFile(string localFilename, string remoteFilename);

		// Token: 0x060002B9 RID: 697
		bool AppendAllLines(string remoteFilename, IEnumerable<string> lines);

		// Token: 0x060002BA RID: 698
		bool AppendAllText(string remoteFilename, string text);

		// Token: 0x060002BB RID: 699
		bool CreateDirectory(string remoteFolder);

		// Token: 0x060002BC RID: 700
		string ReadAllText(string remoteFilename);

		// Token: 0x060002BD RID: 701
		bool Exists(string remotePath);

		// Token: 0x060002BE RID: 702
		bool FileExists(string remotePath, string remoteFilename);

		// Token: 0x060002BF RID: 703
		IList<FtpFileInfo> GetFilesInfo();
	}
}
