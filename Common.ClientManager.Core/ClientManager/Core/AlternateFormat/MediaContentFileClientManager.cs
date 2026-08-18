using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AlternateFormat;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.AlternateFormat
{
	// Token: 0x0200009E RID: 158
	public class MediaContentFileClientManager : IMediaContentFileClientManager, IWebService
	{
		// Token: 0x060005EA RID: 1514 RVA: 0x0001A308 File Offset: 0x00018508
		public MediaContentFileWithoutDataDTO CreateMediaContentFileInfo(MediaContentFileWithoutDataDTO fileInfo)
		{
			CreateMediaContentFileInfoReq createMediaContentFileInfoReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateMediaContentFileInfoReq>();
			createMediaContentFileInfoReq.MediaContentFile = fileInfo;
			return ClientServiceFactory.GetClientInstance<IMediaContentFile>().CreateMediaContentFileInfo(createMediaContentFileInfoReq).MediaContentFile;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001A340 File Offset: 0x00018540
		[DebuggerStepThrough]
		public Task<MediaContentFileWithoutDataDTO> CreateMediaContentFileInfoAsync(MediaContentFileWithoutDataDTO fileInfo)
		{
			MediaContentFileClientManager.<CreateMediaContentFileInfoAsync>d__1 <CreateMediaContentFileInfoAsync>d__ = new MediaContentFileClientManager.<CreateMediaContentFileInfoAsync>d__1();
			<CreateMediaContentFileInfoAsync>d__.<>t__builder = AsyncTaskMethodBuilder<MediaContentFileWithoutDataDTO>.Create();
			<CreateMediaContentFileInfoAsync>d__.<>4__this = this;
			<CreateMediaContentFileInfoAsync>d__.fileInfo = fileInfo;
			<CreateMediaContentFileInfoAsync>d__.<>1__state = -1;
			<CreateMediaContentFileInfoAsync>d__.<>t__builder.Start<MediaContentFileClientManager.<CreateMediaContentFileInfoAsync>d__1>(ref <CreateMediaContentFileInfoAsync>d__);
			return <CreateMediaContentFileInfoAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0001A38C File Offset: 0x0001858C
		public IList<MediaContentFileWithoutDataDTO> LoadMediaContentFileByContent(Guid mediaContentId, int studentId = 0)
		{
			LoadMediaContentFileByContentReq loadMediaContentFileByContentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadMediaContentFileByContentReq>();
			loadMediaContentFileByContentReq.MediaContentID = mediaContentId;
			loadMediaContentFileByContentReq.StudentId = studentId;
			return ClientServiceFactory.GetClientInstance<IMediaContentFile>().LoadMediaContentFileByContent(loadMediaContentFileByContentReq).MediaContentFiles;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0001A3CC File Offset: 0x000185CC
		public IList<StudentMediaContentFileWithProofOfPurchaseInfoDTO> LoadMediaContentFileByStudentId(int studentId)
		{
			LoadMediaContentFileByStudentIdReq loadMediaContentFileByStudentIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadMediaContentFileByStudentIdReq>();
			loadMediaContentFileByStudentIdReq.StudentId = studentId;
			return ClientServiceFactory.GetClientInstance<IMediaContentFile>().LoadMediaContentFileByStudentId(loadMediaContentFileByStudentIdReq).MediaContentFiles;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001A404 File Offset: 0x00018604
		public void UpdateMediaContentFileWithoutData(MediaContentFileWithoutDataDTO mediaContentFile)
		{
			UpdateMediaContentFileWithoutDataReq updateMediaContentFileWithoutDataReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateMediaContentFileWithoutDataReq>();
			updateMediaContentFileWithoutDataReq.MediaContentFile = mediaContentFile;
			ClientServiceFactory.GetClientInstance<IMediaContentFile>().UpdateMediaContentFileWithoutData(updateMediaContentFileWithoutDataReq);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001A434 File Offset: 0x00018634
		[DebuggerStepThrough]
		public Task DeleteMediaContentFileAsync(FileIdentifierDTO mediaContentFileId)
		{
			MediaContentFileClientManager.<DeleteMediaContentFileAsync>d__5 <DeleteMediaContentFileAsync>d__ = new MediaContentFileClientManager.<DeleteMediaContentFileAsync>d__5();
			<DeleteMediaContentFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DeleteMediaContentFileAsync>d__.<>4__this = this;
			<DeleteMediaContentFileAsync>d__.mediaContentFileId = mediaContentFileId;
			<DeleteMediaContentFileAsync>d__.<>1__state = -1;
			<DeleteMediaContentFileAsync>d__.<>t__builder.Start<MediaContentFileClientManager.<DeleteMediaContentFileAsync>d__5>(ref <DeleteMediaContentFileAsync>d__);
			return <DeleteMediaContentFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0001A480 File Offset: 0x00018680
		public IList<MediaContentFileWithoutDataDTO> GetMediaContentFileMatching(string searchText, int lucourseid = 0)
		{
			GetMediaContentFileMatchingReq getMediaContentFileMatchingReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetMediaContentFileMatchingReq>();
			getMediaContentFileMatchingReq.SearchText = searchText;
			getMediaContentFileMatchingReq.LuCourseid = lucourseid;
			return ClientServiceFactory.GetClientInstance<IMediaContentFile>().GetMediaContentFileMatching(getMediaContentFileMatchingReq).MediaContentFileList;
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0001A4C0 File Offset: 0x000186C0
		public IList<MediaContentFileWithoutDataDTO> LoadMediaContentFileByMediaContentPerFormatId(int mediaContentPerFormatId, int studentId = 0)
		{
			LoadMediaContentFileByMediaContentPerFormatIdReq loadMediaContentFileByMediaContentPerFormatIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadMediaContentFileByMediaContentPerFormatIdReq>();
			loadMediaContentFileByMediaContentPerFormatIdReq.MediaContentPerFormatId = mediaContentPerFormatId;
			loadMediaContentFileByMediaContentPerFormatIdReq.StudentId = studentId;
			return ClientServiceFactory.GetClientInstance<IMediaContentFile>().LoadMediaContentFileByMediaContentPerFormatId(loadMediaContentFileByMediaContentPerFormatIdReq).MediaContentFileList;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0001A500 File Offset: 0x00018700
		[DebuggerStepThrough]
		public Task<IList<MediaContentFileWithoutDataDTO>> LoadMediaContentFileByMediaContentPerFormatIdAsync(int mediaContentPerFormatId, int studentId = 0)
		{
			MediaContentFileClientManager.<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__8 <LoadMediaContentFileByMediaContentPerFormatIdAsync>d__ = new MediaContentFileClientManager.<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__8();
			<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<MediaContentFileWithoutDataDTO>>.Create();
			<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__.<>4__this = this;
			<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__.mediaContentPerFormatId = mediaContentPerFormatId;
			<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__.studentId = studentId;
			<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__.<>1__state = -1;
			<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__.<>t__builder.Start<MediaContentFileClientManager.<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__8>(ref <LoadMediaContentFileByMediaContentPerFormatIdAsync>d__);
			return <LoadMediaContentFileByMediaContentPerFormatIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0001A554 File Offset: 0x00018754
		public IList<MediaContentFileWithoutDataDTO> LoadMediaContentFileByMediaContentPerFormatId(Guid mediaContentId, MediaContentFormat mediaContentFormat, int studentId = 0)
		{
			LoadMediaContentFileByMediaContentAndFormatReq loadMediaContentFileByMediaContentAndFormatReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadMediaContentFileByMediaContentAndFormatReq>();
			loadMediaContentFileByMediaContentAndFormatReq.MediaContentId = mediaContentId;
			loadMediaContentFileByMediaContentAndFormatReq.MediaContentFormat = mediaContentFormat;
			loadMediaContentFileByMediaContentAndFormatReq.StudentId = studentId;
			return ClientServiceFactory.GetClientInstance<IMediaContentFile>().LoadMediaContentFileByMediaContentAndFormat(loadMediaContentFileByMediaContentAndFormatReq).MediaContentFileList;
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0001A59C File Offset: 0x0001879C
		[DebuggerStepThrough]
		public Task<IList<StudentMediaContentFileWithProofOfPurchaseInfoDTO>> LoadAvailableMediaContentFileByStudentIdAsync(int studentId, DateTime startDate, DateTime endDate)
		{
			MediaContentFileClientManager.<LoadAvailableMediaContentFileByStudentIdAsync>d__10 <LoadAvailableMediaContentFileByStudentIdAsync>d__ = new MediaContentFileClientManager.<LoadAvailableMediaContentFileByStudentIdAsync>d__10();
			<LoadAvailableMediaContentFileByStudentIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<StudentMediaContentFileWithProofOfPurchaseInfoDTO>>.Create();
			<LoadAvailableMediaContentFileByStudentIdAsync>d__.<>4__this = this;
			<LoadAvailableMediaContentFileByStudentIdAsync>d__.studentId = studentId;
			<LoadAvailableMediaContentFileByStudentIdAsync>d__.startDate = startDate;
			<LoadAvailableMediaContentFileByStudentIdAsync>d__.endDate = endDate;
			<LoadAvailableMediaContentFileByStudentIdAsync>d__.<>1__state = -1;
			<LoadAvailableMediaContentFileByStudentIdAsync>d__.<>t__builder.Start<MediaContentFileClientManager.<LoadAvailableMediaContentFileByStudentIdAsync>d__10>(ref <LoadAvailableMediaContentFileByStudentIdAsync>d__);
			return <LoadAvailableMediaContentFileByStudentIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0001A5F8 File Offset: 0x000187F8
		[DebuggerStepThrough]
		public Task<IList<StudentMediaContentFileWithProofOfPurchaseInfoDTO>> LoadAvailableMediaContentFileByStudentAndMediaContentAsync(int studentId, Guid mediaContentId, DateTime startDate, DateTime endDate)
		{
			MediaContentFileClientManager.<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__11 <LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__ = new MediaContentFileClientManager.<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__11();
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<StudentMediaContentFileWithProofOfPurchaseInfoDTO>>.Create();
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.<>4__this = this;
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.studentId = studentId;
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.mediaContentId = mediaContentId;
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.startDate = startDate;
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.endDate = endDate;
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.<>1__state = -1;
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.<>t__builder.Start<MediaContentFileClientManager.<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__11>(ref <LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__);
			return <LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.<>t__builder.Task;
		}
	}
}
