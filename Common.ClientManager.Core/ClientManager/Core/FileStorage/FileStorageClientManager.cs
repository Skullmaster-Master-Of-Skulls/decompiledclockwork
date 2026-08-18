using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.FileStorage;
using TechnoPro.Common.Core.Mappers.Files;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.FileStorage;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.FileStorage
{
	// Token: 0x0200005C RID: 92
	public class FileStorageClientManager : IFileStorageClientManager, IWebService
	{
		// Token: 0x06000357 RID: 855 RVA: 0x0000E9EC File Offset: 0x0000CBEC
		public BinaryFileDTO GetFile(string filename, string serverFolder)
		{
			GetFileReq getFileReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetFileReq>();
			getFileReq.Filename = filename;
			getFileReq.ServerFolderPath = serverFolder;
			getFileReq.SpecialFolder = eServerStorageSpecialFolders.None;
			return ClientServiceFactory.GetClientInstance<IFileStorage>().GetFile(getFileReq).File;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000EA34 File Offset: 0x0000CC34
		public BinaryFileDTO GetFile(string filename, eServerStorageSpecialFolders specialFolder)
		{
			GetFileReq getFileReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetFileReq>();
			getFileReq.Filename = filename;
			getFileReq.SpecialFolder = specialFolder;
			getFileReq.ServerFolderPath = null;
			return ClientServiceFactory.GetClientInstance<IFileStorage>().GetFile(getFileReq).File;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000EA7C File Offset: 0x0000CC7C
		public void SaveFile(FileStructure file)
		{
			SaveFileReq saveFileReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveFileReq>();
			saveFileReq.File = file.ToDTO();
			ClientServiceFactory.GetClientInstance<IFileStorage>().SaveFile(saveFileReq);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000EAB0 File Offset: 0x0000CCB0
		[DebuggerStepThrough]
		public Task<BinaryFileDTO> GetFileAsync(string filename, string serverFolder)
		{
			FileStorageClientManager.<GetFileAsync>d__3 <GetFileAsync>d__ = new FileStorageClientManager.<GetFileAsync>d__3();
			<GetFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<BinaryFileDTO>.Create();
			<GetFileAsync>d__.<>4__this = this;
			<GetFileAsync>d__.filename = filename;
			<GetFileAsync>d__.serverFolder = serverFolder;
			<GetFileAsync>d__.<>1__state = -1;
			<GetFileAsync>d__.<>t__builder.Start<FileStorageClientManager.<GetFileAsync>d__3>(ref <GetFileAsync>d__);
			return <GetFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000EB04 File Offset: 0x0000CD04
		[DebuggerStepThrough]
		public Task<BinaryFileDTO> GetFileAsync(string filename, eServerStorageSpecialFolders specialFolder)
		{
			FileStorageClientManager.<GetFileAsync>d__4 <GetFileAsync>d__ = new FileStorageClientManager.<GetFileAsync>d__4();
			<GetFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<BinaryFileDTO>.Create();
			<GetFileAsync>d__.<>4__this = this;
			<GetFileAsync>d__.filename = filename;
			<GetFileAsync>d__.specialFolder = specialFolder;
			<GetFileAsync>d__.<>1__state = -1;
			<GetFileAsync>d__.<>t__builder.Start<FileStorageClientManager.<GetFileAsync>d__4>(ref <GetFileAsync>d__);
			return <GetFileAsync>d__.<>t__builder.Task;
		}
	}
}
