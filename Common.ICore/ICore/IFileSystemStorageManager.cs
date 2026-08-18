using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.FileStorage;
using TechnoPro.Common.Public.Entities.OperationContexts;

namespace TechnoPro.Common.ICore
{
	// Token: 0x02000008 RID: 8
	public interface IFileSystemStorageManager : IFileStorageManager, IBaseOperationContext<FileStorageOperationContext>
	{
		// Token: 0x0600003B RID: 59
		BinaryFile GetFile(string file);

		// Token: 0x0600003C RID: 60
		BinaryFile GetFile(string filename, eServerStorageSpecialFolders specialFolder);
	}
}
