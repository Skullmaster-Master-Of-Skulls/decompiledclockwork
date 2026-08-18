using System;
using System.ServiceModel;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Attributes;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200008A RID: 138
	[ServiceContract(Namespace = "http://tpro.ca", Name = "LargeFileStreamingService")]
	[StreamingService]
	[NoSslCertificate]
	public interface ILargeFileStreaming : IService
	{
		// Token: 0x060003BF RID: 959
		[OperationContract]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		StreamingFileDTO DownloadLargeFile(DownloadLargeFileMessageReq request);

		// Token: 0x060003C0 RID: 960
		[OperationContract(Name = "DownloadLargeFileAsync")]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		Task<StreamingFileDTO> DownloadLargeFileAsync(DownloadLargeFileMessageReq request);

		// Token: 0x060003C1 RID: 961
		[OperationContract]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		UploadLargeFileResp UploadLargeFile(StreamingFileDTO file);

		// Token: 0x060003C2 RID: 962
		[OperationContract(Name = "UploadLargeFileAsync")]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		Task<UploadLargeFileResp> UploadLargeFileAsync(StreamingFileDTO request);

		// Token: 0x060003C3 RID: 963
		[OperationContract]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		StreamingFileDTO DownloadLargeTempFile(DownloadLargeFileMessageReq request);

		// Token: 0x060003C4 RID: 964
		[OperationContract(Name = "DownloadLargeTempFileAsync")]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		Task<StreamingFileDTO> DownloadLargeTempFileAsync(DownloadLargeFileMessageReq request);

		// Token: 0x060003C5 RID: 965
		[OperationContract]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		UploadLargeFileResp UploadLargeTempFile(StreamingFileDTO request);

		// Token: 0x060003C6 RID: 966
		[OperationContract(Name = "UploadLargeTempFileAsync")]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		Task<UploadLargeFileResp> UploadLargeTempFileAsync(StreamingFileDTO request);
	}
}
