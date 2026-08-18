using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.FileStorage;
using TechnoPro.Common.Public.Entities.OperationContexts;

namespace TechnoPro.Common.DAO
{
	// Token: 0x0200000C RID: 12
	public interface IFileSystemStorageDAO : IFileStorageDAO, IBaseOperationContext<FileStorageOperationContext>
	{
		// Token: 0x06000012 RID: 18
		BinaryFile GetFile(string file);

		// Token: 0x06000013 RID: 19
		BinaryFile GetFile(string filename, eServerStorageSpecialFolders specialFolder);
	}
}
