using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.FTP;
using TechnoPro.Common.DAO.SFTP.Client;
using TechnoPro.Common.ICore.FTP;
using TechnoPro.Common.Public.Entities.FTP;

namespace TechnoPro.Common.Core.Ftp
{
	// Token: 0x02000002 RID: 2
	public class FtpManager : IFtpManager
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		private IFtpDAO FtpDAO { get; set; }

		// Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		public FtpManager()
		{
			this.FtpDAO = new SFtpClientDAO();
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002074 File Offset: 0x00000274
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002081 File Offset: 0x00000281
		public FtpConnectionInfo ConnectionInfo
		{
			get
			{
				return this.FtpDAO.ConnectionInfo;
			}
			set
			{
				this.FtpDAO.ConnectionInfo = value;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000208F File Offset: 0x0000028F
		public bool DownloadFile(string remoteFilename, string localFilename)
		{
			return this.FtpDAO.DownloadFile(remoteFilename, localFilename);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000209E File Offset: 0x0000029E
		public bool UploadFile(string localFilename, string remoteFilename)
		{
			return this.FtpDAO.UploadFile(localFilename, remoteFilename);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020AD File Offset: 0x000002AD
		public bool AppendAllLines(string remoteFilename, IEnumerable<string> lines)
		{
			return this.FtpDAO.AppendAllLines(remoteFilename, lines);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020BC File Offset: 0x000002BC
		public bool AppendAllText(string remoteFilename, string text)
		{
			return this.FtpDAO.AppendAllText(remoteFilename, text);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020CB File Offset: 0x000002CB
		public bool CreateDirectory(string remoteFolder)
		{
			return this.FtpDAO.CreateDirectory(remoteFolder);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020D9 File Offset: 0x000002D9
		public string ReadAllText(string remoteFilename)
		{
			return this.FtpDAO.ReadAllText(remoteFilename);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020E7 File Offset: 0x000002E7
		public bool Exists(string remotePath)
		{
			return this.FtpDAO.Exists(remotePath);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000020F5 File Offset: 0x000002F5
		public bool FileExists(string remotePath, string remoteFilename)
		{
			return this.FtpDAO.FileExists(remotePath, remoteFilename);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002104 File Offset: 0x00000304
		public IList<FtpFileInfo> GetFilesInfo()
		{
			return this.FtpDAO.GetFilesInfo();
		}
	}
}
