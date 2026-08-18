using System;
using TechnoPro.Common.Public.Entities.FTP;

namespace TechnoPro.Common.DAO.Impl.Adapters
{
	// Token: 0x0200017E RID: 382
	public static class FtpFileInfoAdapter
	{
		// Token: 0x06000B63 RID: 2915 RVA: 0x00078F64 File Offset: 0x00077164
		public static bool IsPublicFolder(this FtpFileInfo ftpFileInfo)
		{
			return ftpFileInfo.Folder.Equals("Public");
		}
	}
}
