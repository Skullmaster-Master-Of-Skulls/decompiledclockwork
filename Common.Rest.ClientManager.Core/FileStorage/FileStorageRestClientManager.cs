using System;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.FileStorage;
using TechnoPro.Common.Core.Mappers.Files;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.FileStorage;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.FileStorage
{
	// Token: 0x0200004C RID: 76
	public class FileStorageRestClientManager : BearerTokenRestProxy<IFileStorageClientManager>, IFileStorageClientManager, IWebService
	{
		// Token: 0x060002E2 RID: 738 RVA: 0x00008BEC File Offset: 0x00006DEC
		public FileStorageRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00008BF6 File Offset: 0x00006DF6
		public FileStorageRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00008C04 File Offset: 0x00006E04
		public BinaryFileDTO GetFile(string filename, string serverFolder)
		{
			GetFileReq getFileReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetFileReq>();
			getFileReq.Filename = filename;
			getFileReq.ServerFolderPath = serverFolder;
			getFileReq.SpecialFolder = eServerStorageSpecialFolders.None;
			return base.Post<GetFileReq, BinaryFileDTO>(getFileReq, "filestorage/getfile");
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00008C40 File Offset: 0x00006E40
		public BinaryFileDTO GetFile(string filename, eServerStorageSpecialFolders specialFolder)
		{
			GetFileReq getFileReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetFileReq>();
			getFileReq.Filename = filename;
			getFileReq.SpecialFolder = specialFolder;
			getFileReq.ServerFolderPath = null;
			return base.Post<GetFileReq, BinaryFileDTO>(getFileReq, "filestorage/getfile");
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00008C79 File Offset: 0x00006E79
		public void SaveFile(FileStructure file)
		{
			base.Post<FileStructureDTO>(file.ToDTO(), "filestorage");
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00008C8C File Offset: 0x00006E8C
		public async Task<BinaryFileDTO> GetFileAsync(string filename, string serverFolder)
		{
			GetFileReq getFileReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetFileReq>();
			getFileReq.Filename = filename;
			getFileReq.ServerFolderPath = serverFolder;
			getFileReq.SpecialFolder = eServerStorageSpecialFolders.None;
			return await this.PostAsync<GetFileReq, BinaryFileDTO>(getFileReq, "filestorage/getfile");
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00008CE4 File Offset: 0x00006EE4
		public async Task<BinaryFileDTO> GetFileAsync(string filename, eServerStorageSpecialFolders specialFolder)
		{
			GetFileReq getFileReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetFileReq>();
			getFileReq.Filename = filename;
			getFileReq.SpecialFolder = specialFolder;
			getFileReq.ServerFolderPath = null;
			return await this.PostAsync<GetFileReq, BinaryFileDTO>(getFileReq, "filestorage/getfile");
		}
	}
}
