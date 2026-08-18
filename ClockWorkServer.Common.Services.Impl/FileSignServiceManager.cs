using System;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Storages;
using TechnoPro.Common.Core.FileStorages;
using TechnoPro.Common.ICore.FileStorages;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000046 RID: 70
	public class FileSignServiceManager : IFileSign, IService
	{
		// Token: 0x060002AB RID: 683 RVA: 0x0000D6A4 File Offset: 0x0000B8A4
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000D6B8 File Offset: 0x0000B8B8
		public DecryptAndVerifyResp DecryptAndVerify(DecryptAndVerifyReq Request)
		{
			IFileSignManager fileSignManager = new FileSignManager();
			byte[] decryptedFile = fileSignManager.DecryptAndVerify(Request.EncryptedFile);
			return new DecryptAndVerifyResp
			{
				DecryptedFile = decryptedFile
			};
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000D6EC File Offset: 0x0000B8EC
		public void DecryptAndVerifyUsingFileSystem(DecryptAndVerifyUsingFileSystemReq Request)
		{
			IFileSignManager fileSignManager = new FileSignManager();
			fileSignManager.DecryptAndVerifyUsingFileSystem(Request.EncryptedFileName, Request.OutputDecryptedFileName);
		}
	}
}
