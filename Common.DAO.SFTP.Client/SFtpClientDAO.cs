using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClockWorkLogger;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using TechnoPro.Common.DAO.FTP;
using TechnoPro.Common.Public.Entities.FTP;

namespace TechnoPro.Common.DAO.SFTP.Client
{
	// Token: 0x02000002 RID: 2
	public class SFtpClientDAO : IFtpDAO
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public FtpConnectionInfo ConnectionInfo { get; set; }

		// Token: 0x06000003 RID: 3 RVA: 0x00002064 File Offset: 0x00000264
		public bool DownloadFile(string remoteFilename, string localFilename)
		{
			bool result;
			try
			{
				using (SftpClient sftpClient = new SftpClient(this.ConnectionInfo.Host, this.ConnectionInfo.Port, this.ConnectionInfo.Username, this.ConnectionInfo.Password))
				{
					sftpClient.ConnectionInfo.Timeout = TimeSpan.FromMinutes(30.0);
					sftpClient.Connect();
					bool flag = !string.IsNullOrEmpty(this.ConnectionInfo.RemoteDir) && sftpClient.Exists(this.ConnectionInfo.RemoteDir);
					if (flag)
					{
						sftpClient.ChangeDirectory(this.ConnectionInfo.RemoteDir);
					}
					using (FileStream fileStream = new FileStream(localFilename, FileMode.Create, FileAccess.ReadWrite))
					{
						sftpClient.DownloadFile(remoteFilename, fileStream, null);
						sftpClient.Disconnect();
					}
				}
				result = true;
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("SFtpClientDAO::DownloadFile:: Download error: {0}", ex.ToString());
				result = false;
			}
			return result;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002188 File Offset: 0x00000388
		public bool UploadFile(string localFilename, string remoteFilename)
		{
			bool result;
			try
			{
				using (SftpClient sftpClient = new SftpClient(this.ConnectionInfo.Host, this.ConnectionInfo.Port, this.ConnectionInfo.Username, this.ConnectionInfo.Password))
				{
					sftpClient.ConnectionInfo.Timeout = TimeSpan.FromMinutes(30.0);
					sftpClient.Connect();
					bool flag = !string.IsNullOrEmpty(this.ConnectionInfo.RemoteDir) && sftpClient.Exists(this.ConnectionInfo.RemoteDir);
					if (flag)
					{
						sftpClient.ChangeDirectory(this.ConnectionInfo.RemoteDir);
					}
					using (FileStream fileStream = new FileStream(localFilename, FileMode.Open, FileAccess.Read))
					{
						sftpClient.UploadFile(fileStream, remoteFilename, null);
						sftpClient.Disconnect();
					}
				}
				result = true;
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("SFtpClientDAO::UploadFile:: Download error: {0}", ex.ToString());
				result = false;
			}
			return result;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000022AC File Offset: 0x000004AC
		public bool AppendAllLines(string remoteFilename, IEnumerable<string> lines)
		{
			bool result;
			try
			{
				using (SftpClient sftpClient = new SftpClient(this.ConnectionInfo.Host, this.ConnectionInfo.Port, this.ConnectionInfo.Username, this.ConnectionInfo.Password))
				{
					sftpClient.ConnectionInfo.Timeout = TimeSpan.FromMinutes(30.0);
					sftpClient.Connect();
					bool flag = !string.IsNullOrEmpty(this.ConnectionInfo.RemoteDir) && sftpClient.Exists(this.ConnectionInfo.RemoteDir);
					if (flag)
					{
						sftpClient.ChangeDirectory(this.ConnectionInfo.RemoteDir);
					}
					sftpClient.AppendAllLines(remoteFilename, lines);
					sftpClient.Disconnect();
				}
				result = true;
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("SFtpClientDAO::AppendAllLines: {0}", ex.ToString());
				result = false;
			}
			return result;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000023A8 File Offset: 0x000005A8
		public bool AppendAllText(string remoteFilename, string text)
		{
			bool result;
			try
			{
				using (SftpClient sftpClient = new SftpClient(this.ConnectionInfo.Host, this.ConnectionInfo.Port, this.ConnectionInfo.Username, this.ConnectionInfo.Password))
				{
					sftpClient.ConnectionInfo.Timeout = TimeSpan.FromMinutes(30.0);
					sftpClient.Connect();
					bool flag = !string.IsNullOrEmpty(this.ConnectionInfo.RemoteDir) && sftpClient.Exists(this.ConnectionInfo.RemoteDir);
					if (flag)
					{
						sftpClient.ChangeDirectory(this.ConnectionInfo.RemoteDir);
					}
					sftpClient.AppendAllText(remoteFilename, text);
					sftpClient.Disconnect();
				}
				result = true;
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("SFtpClientDAO::AppendAllText: {0}", ex.ToString());
				result = false;
			}
			return result;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000024A4 File Offset: 0x000006A4
		public bool CreateDirectory(string remoteFolder)
		{
			bool result;
			try
			{
				using (SftpClient sftpClient = new SftpClient(this.ConnectionInfo.Host, this.ConnectionInfo.Port, this.ConnectionInfo.Username, this.ConnectionInfo.Password))
				{
					sftpClient.ConnectionInfo.Timeout = TimeSpan.FromMinutes(30.0);
					sftpClient.Connect();
					bool flag = !string.IsNullOrEmpty(this.ConnectionInfo.RemoteDir) && sftpClient.Exists(this.ConnectionInfo.RemoteDir);
					if (flag)
					{
						sftpClient.ChangeDirectory(this.ConnectionInfo.RemoteDir);
					}
					sftpClient.CreateDirectory(remoteFolder);
					sftpClient.Disconnect();
				}
				result = true;
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("SFtpClientDAO::CreateDirectory: {0}", ex.ToString());
				result = false;
			}
			return result;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000025A0 File Offset: 0x000007A0
		public bool Exists(string remotePath)
		{
			bool result;
			try
			{
				bool flag = false;
				using (SftpClient sftpClient = new SftpClient(this.ConnectionInfo.Host, this.ConnectionInfo.Port, this.ConnectionInfo.Username, this.ConnectionInfo.Password))
				{
					sftpClient.ConnectionInfo.Timeout = TimeSpan.FromMinutes(30.0);
					sftpClient.Connect();
					flag = sftpClient.Exists(remotePath);
					sftpClient.Disconnect();
				}
				result = flag;
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("SFtpClientDAO::Exists: {0}", ex.ToString());
				throw;
			}
			return result;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002660 File Offset: 0x00000860
		public bool FileExists(string remotePath, string remoteFilename)
		{
			bool result;
			try
			{
				using (SftpClient sftpClient = new SftpClient(this.ConnectionInfo.Host, this.ConnectionInfo.Port, this.ConnectionInfo.Username, this.ConnectionInfo.Password))
				{
					sftpClient.ConnectionInfo.Timeout = TimeSpan.FromMinutes(30.0);
					sftpClient.Connect();
					IEnumerable<SftpFile> source = sftpClient.ListDirectory(remotePath, null);
					result = source.Any((SftpFile f) => f.Name.Equals(remoteFilename, StringComparison.OrdinalIgnoreCase));
				}
			}
			catch (Exception)
			{
				throw;
			}
			return result;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000271C File Offset: 0x0000091C
		public string ReadAllText(string remoteFilename)
		{
			string result;
			try
			{
				string text;
				using (SftpClient sftpClient = new SftpClient(this.ConnectionInfo.Host, this.ConnectionInfo.Port, this.ConnectionInfo.Username, this.ConnectionInfo.Password))
				{
					sftpClient.ConnectionInfo.Timeout = TimeSpan.FromMinutes(30.0);
					sftpClient.Connect();
					bool flag = !string.IsNullOrEmpty(this.ConnectionInfo.RemoteDir) && sftpClient.Exists(this.ConnectionInfo.RemoteDir);
					if (flag)
					{
						sftpClient.ChangeDirectory(this.ConnectionInfo.RemoteDir);
					}
					text = sftpClient.ReadAllText(remoteFilename);
					sftpClient.Disconnect();
				}
				result = text;
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("SFtpClientDAO::ReadAllText: {0}", ex.ToString());
				throw;
			}
			return result;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002818 File Offset: 0x00000A18
		public IList<FtpFileInfo> GetFilesInfo()
		{
			List<FtpFileInfo> list = new List<FtpFileInfo>();
			try
			{
				CWLogger.Logger.Trace("SFtpClientDAO::GetFilesInfo: Creating sftClient ...");
				using (SftpClient sftpClient = new SftpClient(this.ConnectionInfo.Host, this.ConnectionInfo.Port, this.ConnectionInfo.Username, this.ConnectionInfo.Password))
				{
					sftpClient.ConnectionInfo.Timeout = TimeSpan.FromMinutes(30.0);
					sftpClient.Connect();
					bool flag = sftpClient.Exists(this.ConnectionInfo.RemoteDir);
					if (flag)
					{
						CWLogger.Logger.Trace("SFtpClientDAO::GetFilesInfo: RemoteDir '{0}' exists. Listing directory ...", this.ConnectionInfo.RemoteDir);
						List<SftpFile> list2 = sftpClient.ListDirectory(this.ConnectionInfo.RemoteDir, null).ToList<SftpFile>();
						CWLogger.Logger.Trace("SFtpClientDAO::GetFilesInfo: There are {0} files in '{1}'", list2.Count, this.ConnectionInfo.RemoteDir);
						foreach (SftpFile sftpFile in list2)
						{
							bool isDirectory = sftpFile.IsDirectory;
							if (!isDirectory)
							{
								FtpFileInfo item = new FtpFileInfo
								{
									Filename = sftpFile.Name,
									IsDirectory = false,
									LastModifiedTime = sftpFile.LastWriteTime,
									SizeinBytes = (int)sftpFile.Length
								};
								list.Add(item);
							}
						}
					}
					sftpClient.Disconnect();
					CWLogger.Logger.Trace("SFtpClientDAO::GetFilesInfo: Disconnected");
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("SFtpClientDAO::GetFilesInfo:: {0}", ex.ToString());
				throw;
			}
			return list;
		}
	}
}
