using System;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Updates;

namespace TechnoPro.Common.DAO
{
	// Token: 0x0200000B RID: 11
	public interface IFileStorageDAO
	{
		// Token: 0x0600000F RID: 15
		FileStructure LoadFile(FileType fileType, int addrSize, string clientVersion);

		// Token: 0x06000010 RID: 16
		void SaveFile(FileStructure fs);

		// Token: 0x06000011 RID: 17
		FileVersionResp GetFileVersion(FileType fileType, int addrSize);
	}
}
