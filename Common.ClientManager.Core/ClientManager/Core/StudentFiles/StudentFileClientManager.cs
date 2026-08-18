using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.StudentFiles;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.StudentFiles
{
	// Token: 0x02000015 RID: 21
	public class StudentFileClientManager : IStudentFileClientManager, IWebService
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00004A20 File Offset: 0x00002C20
		public BinaryFileDTO LoadFileFromDynamicFileDescription(int studentPersonId, DynamicFileDescriptionDTO fileDescription)
		{
			LoadFileFromDynamicFileDescriptionReq loadFileFromDynamicFileDescriptionReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadFileFromDynamicFileDescriptionReq>();
			loadFileFromDynamicFileDescriptionReq.StudentPersonId = studentPersonId;
			loadFileFromDynamicFileDescriptionReq.DynamicFileDescription = fileDescription;
			return ClientServiceFactory.GetClientInstance<IStudentFile>().LoadFileFromDynamicFileDescription(loadFileFromDynamicFileDescriptionReq).File;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004A60 File Offset: 0x00002C60
		[DebuggerStepThrough]
		public Task<BinaryFileDTO> LoadFileFromDynamicFileDescriptionAsync(int studentPersonId, DynamicFileDescriptionDTO fileDescription)
		{
			StudentFileClientManager.<LoadFileFromDynamicFileDescriptionAsync>d__1 <LoadFileFromDynamicFileDescriptionAsync>d__ = new StudentFileClientManager.<LoadFileFromDynamicFileDescriptionAsync>d__1();
			<LoadFileFromDynamicFileDescriptionAsync>d__.<>t__builder = AsyncTaskMethodBuilder<BinaryFileDTO>.Create();
			<LoadFileFromDynamicFileDescriptionAsync>d__.<>4__this = this;
			<LoadFileFromDynamicFileDescriptionAsync>d__.studentPersonId = studentPersonId;
			<LoadFileFromDynamicFileDescriptionAsync>d__.fileDescription = fileDescription;
			<LoadFileFromDynamicFileDescriptionAsync>d__.<>1__state = -1;
			<LoadFileFromDynamicFileDescriptionAsync>d__.<>t__builder.Start<StudentFileClientManager.<LoadFileFromDynamicFileDescriptionAsync>d__1>(ref <LoadFileFromDynamicFileDescriptionAsync>d__);
			return <LoadFileFromDynamicFileDescriptionAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004AB4 File Offset: 0x00002CB4
		public StudentFileCategoryFileDescriptionsWithColDataDTO[] LoadStudentFileDescriptions(int studentPesonId)
		{
			LoadStudentFileDescriptionsReq loadStudentFileDescriptionsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadStudentFileDescriptionsReq>();
			loadStudentFileDescriptionsReq.StudentPersonId = studentPesonId;
			return ClientServiceFactory.GetClientInstance<IStudentFile>().LoadStudentFileDescriptions(loadStudentFileDescriptionsReq).StudentFileCategoriesWithFileDescriptions;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004AEC File Offset: 0x00002CEC
		[DebuggerStepThrough]
		public Task<StudentFileCategoryFileDescriptionsWithColDataDTO[]> LoadStudentFileDescriptionsAsync(int studentPersonId)
		{
			StudentFileClientManager.<LoadStudentFileDescriptionsAsync>d__3 <LoadStudentFileDescriptionsAsync>d__ = new StudentFileClientManager.<LoadStudentFileDescriptionsAsync>d__3();
			<LoadStudentFileDescriptionsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<StudentFileCategoryFileDescriptionsWithColDataDTO[]>.Create();
			<LoadStudentFileDescriptionsAsync>d__.<>4__this = this;
			<LoadStudentFileDescriptionsAsync>d__.studentPersonId = studentPersonId;
			<LoadStudentFileDescriptionsAsync>d__.<>1__state = -1;
			<LoadStudentFileDescriptionsAsync>d__.<>t__builder.Start<StudentFileClientManager.<LoadStudentFileDescriptionsAsync>d__3>(ref <LoadStudentFileDescriptionsAsync>d__);
			return <LoadStudentFileDescriptionsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004B38 File Offset: 0x00002D38
		[DebuggerStepThrough]
		public Task<int> UploadStudentFileAsync(string StudentComment, BinaryFileDTO File)
		{
			StudentFileClientManager.<UploadStudentFileAsync>d__4 <UploadStudentFileAsync>d__ = new StudentFileClientManager.<UploadStudentFileAsync>d__4();
			<UploadStudentFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<UploadStudentFileAsync>d__.<>4__this = this;
			<UploadStudentFileAsync>d__.StudentComment = StudentComment;
			<UploadStudentFileAsync>d__.File = File;
			<UploadStudentFileAsync>d__.<>1__state = -1;
			<UploadStudentFileAsync>d__.<>t__builder.Start<StudentFileClientManager.<UploadStudentFileAsync>d__4>(ref <UploadStudentFileAsync>d__);
			return <UploadStudentFileAsync>d__.<>t__builder.Task;
		}
	}
}
