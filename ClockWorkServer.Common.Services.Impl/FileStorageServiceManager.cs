using System;
using System.IO;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage;
using TechnoPro.ClockWorkServer.Core.Impl;
using TechnoPro.Common.Core.FileStorages;
using TechnoPro.Common.Core.Mappers.Files;
using TechnoPro.Common.ICore;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.FileStorage;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000047 RID: 71
	public class FileStorageServiceManager : IFileStorage, IService
	{
		// Token: 0x060002AF RID: 687 RVA: 0x0000D714 File Offset: 0x0000B914
		public GetFileResp GetFile(GetFileReq request)
		{
			FileStorageOperationContext operationContext = request.GetOperationContext<FileStorageOperationContext>();
			operationContext.ServerFileStorageFolder = ObjectFactory.Resolve<ServerExecutingContext>().GetServerFileSystemStorageFolder();
			IFileSystemStorageManager fileSystemStorageManager = new FileSystemStorageManager(operationContext);
			BinaryFile binaryFile = (request.SpecialFolder != eServerStorageSpecialFolders.None) ? fileSystemStorageManager.GetFile(request.Filename, request.SpecialFolder) : fileSystemStorageManager.GetFile(Path.Combine(request.ServerFolderPath, request.Filename));
			return new GetFileResp
			{
				File = binaryFile.ToDTO()
			};
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000D78C File Offset: 0x0000B98C
		public SaveFileResp SaveFile(SaveFileReq request)
		{
			FileStorageOperationContext operationContext = request.GetOperationContext<FileStorageOperationContext>();
			operationContext.ServerFileStorageFolder = ObjectFactory.Resolve<ServerExecutingContext>().GetServerFileSystemStorageFolder();
			IFileSystemStorageManager fileSystemStorageManager = new FileSystemStorageManager(operationContext);
			fileSystemStorageManager.SaveFile(request.File.ToDomainObject());
			return new SaveFileResp();
		}
	}
}
