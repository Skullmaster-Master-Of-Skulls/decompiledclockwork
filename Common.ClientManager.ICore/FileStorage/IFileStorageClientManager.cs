using System;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.FileStorage;

namespace TechnoPro.Common.ClientManager.ICore.FileStorage
{
	// Token: 0x02000055 RID: 85
	public interface IFileStorageClientManager : IWebService
	{
		// Token: 0x0600028E RID: 654
		BinaryFileDTO GetFile(string filename, string serverFolder);

		// Token: 0x0600028F RID: 655
		BinaryFileDTO GetFile(string filename, eServerStorageSpecialFolders specialFolder);

		// Token: 0x06000290 RID: 656
		void SaveFile(FileStructure file);

		// Token: 0x06000291 RID: 657
		Task<BinaryFileDTO> GetFileAsync(string filename, string serverFolder);

		// Token: 0x06000292 RID: 658
		Task<BinaryFileDTO> GetFileAsync(string filename, eServerStorageSpecialFolders specialFolder);
	}
}
