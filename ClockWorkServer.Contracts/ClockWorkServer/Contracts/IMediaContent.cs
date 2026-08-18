using System;
using System.ServiceModel;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000007 RID: 7
	[ServiceContract(Name = "MediaContentService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IMediaContent : IService
	{
		// Token: 0x0600000A RID: 10
		[OperationContract(Name = "GetMediaContentMatchingAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<GetMediaContentMatchingResp> GetMediaContentMatchingAsync(GetMediaContentMatchingReq request);

		// Token: 0x0600000B RID: 11
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetMediaContentMatchingResp GetMediaContentMatching(GetMediaContentMatchingReq request);

		// Token: 0x0600000C RID: 12
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadMediaContentByIdResp LoadMediaContentById(LoadMediaContentByIdReq request);

		// Token: 0x0600000D RID: 13
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadMediaContentByIdentifierResp LoadMediaContentByIdentifier(LoadMediaContentByIdentifierReq request);

		// Token: 0x0600000E RID: 14
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadMediaContentByISBNResp LoadMediaContentByISBN(LoadMediaContentByISBNReq request);

		// Token: 0x0600000F RID: 15
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadMediaContentByCourseResp LoadMediaContentByCourse(LoadMediaContentByCourseReq request);

		// Token: 0x06000010 RID: 16
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadMediaContentByPublisherResp LoadMediaContentByPublisher(LoadMediaContentByPublisherReq request);

		// Token: 0x06000011 RID: 17
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadMediaContentByCategoryResp LoadMediaContentByCategory(LoadMediaContentByCategoryReq request);

		// Token: 0x06000012 RID: 18
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateMediaContentResp CreateMediaContent(CreateMediaContentReq request);

		// Token: 0x06000013 RID: 19
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateMediaContentResp UpdateMediaContent(UpdateMediaContentReq request);

		// Token: 0x06000014 RID: 20
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DeleteMediaContentResp DeleteMediaContent(DeleteMediaContentReq request);

		// Token: 0x06000015 RID: 21
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetAllMediaContentWithFormatsResp GetAllMediaContent(GetAllMediaContentWithFormatsReq request);

		// Token: 0x06000016 RID: 22
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetMediaContentPerFormatInfoByIdResp GetMediaContentPerFormatInfoById(GetMediaContentPerFormatInfoByIdReq request);

		// Token: 0x06000017 RID: 23
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadMediaContentPerFormatInfoByMediaContentResp LoadMediaContentPerFormatInfoByMediaContent(LoadMediaContentPerFormatInfoByMediaContentReq request);

		// Token: 0x06000018 RID: 24
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetMediaContentPerFormatStatusResp GetMediaContentPerFormatStatus(GetMediaContentPerFormatStatusReq request);

		// Token: 0x06000019 RID: 25
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetMediaContentPerFormatStatusListResp GetMediaContentPerFormatStatusList(GetMediaContentPerFormatStatusListReq request);

		// Token: 0x0600001A RID: 26
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetMediaContentCoursesResp GetMediaContentCourses(GetMediaContentCoursesReq request);

		// Token: 0x0600001B RID: 27
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetMediaContentThumbnailResp GetMediaContentThumbnail(GetMediaContentThumbnailReq request);

		// Token: 0x0600001C RID: 28
		[OperationContract(Name = "GetMediaContentThumbnailBytesAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<GetMediaContentThumbnailBytesResp> GetMediaContentThumbnailBytesAsync(GetMediaContentThumbnailBytesReq request);

		// Token: 0x0600001D RID: 29
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetMediaContentThumbnailBytesResp GetMediaContentThumbnailBytes(GetMediaContentThumbnailBytesReq request);

		// Token: 0x0600001E RID: 30
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		SetMediaContentThumbnailResp SetMediaContentThumbnail(SetMediaContentThumbnailReq request);

		// Token: 0x0600001F RID: 31
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetMediaContentCoverImageResp GetMediaContentCoverImage(GetMediaContentCoverImageReq request);

		// Token: 0x06000020 RID: 32
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetMediaContentCoverImageBytesResp GetMediaContentCoverImageBytes(GetMediaContentCoverImageBytesReq request);

		// Token: 0x06000021 RID: 33
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		SetMediaContentCoverResp SetMediaContentCover(SetMediaContentCoverReq request);
	}
}
