using System;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files.FileUpload;
using TechnoPro.ClockWorkServer.Contracts.DTO.TempFiles;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.TempFiles;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.TempFiles
{
	// Token: 0x0200000C RID: 12
	public class TempFileRestClientManager : BearerTokenRestProxy<ITempFileClientManager>, ITempFileClientManager, IWebService
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00002FE4 File Offset: 0x000011E4
		public TempFileRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002FEE File Offset: 0x000011EE
		public TempFileRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002FF9 File Offset: 0x000011F9
		public void DeleteOldTempFiles()
		{
			base.Delete("tempfile/deleteold");
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003008 File Offset: 0x00001208
		public int AddNewTempFile(TempFileContextDTO context, BinaryFileDTO fileToUpload)
		{
			AddNewTempFileReq addNewTempFileReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AddNewTempFileReq>();
			addNewTempFileReq.Context = context;
			addNewTempFileReq.FileToUpload = fileToUpload;
			return base.Post<AddNewTempFileReq, int>(addNewTempFileReq, "tempfile");
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000303C File Offset: 0x0000123C
		public async Task<int> AddNewTempFileAsync(TempFileContextDTO context, BinaryFileDTO fileToUpload)
		{
			AddNewTempFileReq addNewTempFileReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AddNewTempFileReq>();
			addNewTempFileReq.Context = context;
			addNewTempFileReq.FileToUpload = fileToUpload;
			return await this.PostAsync<AddNewTempFileReq, int>(addNewTempFileReq, "tempfile").ConfigureAwait(false);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003091 File Offset: 0x00001291
		public BinaryFileDTO DownloadTempFile(TempFileContextDTO context, int tempFileId)
		{
			return base.Get<BinaryFileDTO>(string.Format("tempfile/tempfileid/{0}?usage={1}&groupid={2}", tempFileId, context.Usage, context.GroupId), true);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000030BC File Offset: 0x000012BC
		public async Task<BinaryFileDTO> DownloadTempFileAsync(TempFileContextDTO context, int tempFileId)
		{
			return await this.GetAsync<BinaryFileDTO>(string.Format("tempfile/tempfileid/{0}?usage={1}&groupid={2}", tempFileId, context.Usage, context.GroupId), true).ConfigureAwait(false);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003111 File Offset: 0x00001311
		public void DeleteTempFiles(TempFileContextDTO context)
		{
			base.Delete(string.Format("tempfile?usage={0}&groupid={1}", context.Usage, context.GroupId));
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002BEE File Offset: 0x00000DEE
		public void DeleteTempFile(TempFileContextDTO context, int tempFileId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003134 File Offset: 0x00001334
		public async Task DeleteTempFileAsync(TempFileContextDTO context, int tempFileId)
		{
			await this.DeleteAsync(string.Format("tempfile/tempfileid/{0}?usage={1}&groupid={2}", tempFileId, context.Usage, context.GroupId)).ConfigureAwait(false);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000318C File Offset: 0x0000138C
		public int[] CopyTempFilesToInstructorExamUploadAndDeleteTempFile(TempFileContextDTO context, int examId, int whoEntered, string description)
		{
			CopyTempFilesToInstructorExamUploadAndDeleteTempFileReq copyTempFilesToInstructorExamUploadAndDeleteTempFileReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CopyTempFilesToInstructorExamUploadAndDeleteTempFileReq>();
			copyTempFilesToInstructorExamUploadAndDeleteTempFileReq.Context = context;
			copyTempFilesToInstructorExamUploadAndDeleteTempFileReq.ExamId = examId;
			copyTempFilesToInstructorExamUploadAndDeleteTempFileReq.WhoEnteredPersonId = whoEntered;
			copyTempFilesToInstructorExamUploadAndDeleteTempFileReq.Description = description;
			return base.Post<CopyTempFilesToInstructorExamUploadAndDeleteTempFileReq, int[]>(copyTempFilesToInstructorExamUploadAndDeleteTempFileReq, "tempfile/copytempfilestoinstructorexamuploadanddeletetempfile");
		}
	}
}
