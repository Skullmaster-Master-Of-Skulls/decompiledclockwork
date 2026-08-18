using System;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Updates;

namespace TechnoPro.Common.ICore
{
	// Token: 0x02000007 RID: 7
	public interface IFileStorageManager
	{
		// Token: 0x06000037 RID: 55
		FileStructure LoadFile(string fileType, int addrSize, string clientVersion, string customerId);

		// Token: 0x06000038 RID: 56
		FileStructure LoadFile(string fileType, eAddressSize addrSize, string clientVersion, string customerId);

		// Token: 0x06000039 RID: 57
		void SaveFile(FileStructure fs);

		// Token: 0x0600003A RID: 58
		FileVersionResp GetFileVersion(string fileType, int addrSize);
	}
}
