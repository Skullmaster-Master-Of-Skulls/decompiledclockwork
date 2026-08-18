using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.AlternativeFormat;
using TechnoPro.Common.DAO.Impl.AlternativeFormat;
using TechnoPro.Common.ICore.AlternativeFormat;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Public.Entities.FileStorage;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Core.AlternativeFormat
{
	// Token: 0x02000157 RID: 343
	public class MediaContentFileManager : IMediaContentFileManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000F39 RID: 3897 RVA: 0x00071B3D File Offset: 0x0006FD3D
		// (set) Token: 0x06000F3A RID: 3898 RVA: 0x00071B45 File Offset: 0x0006FD45
		private IMediaContentFileDAO MediaContentFileDAO { get; set; }

		// Token: 0x06000F3B RID: 3899 RVA: 0x00071B4E File Offset: 0x0006FD4E
		public MediaContentFileManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.MediaContentFileDAO = new MediaContentFileDAO(opContext);
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000F3C RID: 3900 RVA: 0x00071B6D File Offset: 0x0006FD6D
		// (set) Token: 0x06000F3D RID: 3901 RVA: 0x00071B75 File Offset: 0x0006FD75
		public OperationContext OpContext { get; set; }

		// Token: 0x06000F3E RID: 3902 RVA: 0x00071B80 File Offset: 0x0006FD80
		public MediaContentFileWithoutData CreateMediaContentFileInfo(MediaContentFileWithoutData fileInfo)
		{
			return this.MediaContentFileDAO.CreateMediaContentFileInfo(fileInfo);
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x00071BA0 File Offset: 0x0006FDA0
		[DebuggerStepThrough]
		public Task<MediaContentFileWithoutData> CreateMediaContentFileInfoAsync(MediaContentFileWithoutData fileInfo)
		{
			MediaContentFileManager.<CreateMediaContentFileInfoAsync>d__10 <CreateMediaContentFileInfoAsync>d__ = new MediaContentFileManager.<CreateMediaContentFileInfoAsync>d__10();
			<CreateMediaContentFileInfoAsync>d__.<>t__builder = AsyncTaskMethodBuilder<MediaContentFileWithoutData>.Create();
			<CreateMediaContentFileInfoAsync>d__.<>4__this = this;
			<CreateMediaContentFileInfoAsync>d__.fileInfo = fileInfo;
			<CreateMediaContentFileInfoAsync>d__.<>1__state = -1;
			<CreateMediaContentFileInfoAsync>d__.<>t__builder.Start<MediaContentFileManager.<CreateMediaContentFileInfoAsync>d__10>(ref <CreateMediaContentFileInfoAsync>d__);
			return <CreateMediaContentFileInfoAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x00071BEC File Offset: 0x0006FDEC
		public IList<MediaContentFileWithoutData> LoadMediaContentFileByContent(Guid mediaContentId, int studentId = 0)
		{
			return this.MediaContentFileDAO.LoadMediaContentFileByContent(mediaContentId, studentId);
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x00071C0C File Offset: 0x0006FE0C
		public IList<StudentMediaContentFileWithProofOfPurchaseInfo> LoadMediaContentFileByStudentId(int studentId)
		{
			return this.MediaContentFileDAO.LoadMediaContentFileByStudentId(studentId);
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x00071C2C File Offset: 0x0006FE2C
		[DebuggerStepThrough]
		public Task<IList<StudentMediaContentFileWithProofOfPurchaseInfo>> LoadAvailableMediaContentFileByStudentIdAsync(int studentId, DateTime startDate, DateTime endDate)
		{
			MediaContentFileManager.<LoadAvailableMediaContentFileByStudentIdAsync>d__13 <LoadAvailableMediaContentFileByStudentIdAsync>d__ = new MediaContentFileManager.<LoadAvailableMediaContentFileByStudentIdAsync>d__13();
			<LoadAvailableMediaContentFileByStudentIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<StudentMediaContentFileWithProofOfPurchaseInfo>>.Create();
			<LoadAvailableMediaContentFileByStudentIdAsync>d__.<>4__this = this;
			<LoadAvailableMediaContentFileByStudentIdAsync>d__.studentId = studentId;
			<LoadAvailableMediaContentFileByStudentIdAsync>d__.startDate = startDate;
			<LoadAvailableMediaContentFileByStudentIdAsync>d__.endDate = endDate;
			<LoadAvailableMediaContentFileByStudentIdAsync>d__.<>1__state = -1;
			<LoadAvailableMediaContentFileByStudentIdAsync>d__.<>t__builder.Start<MediaContentFileManager.<LoadAvailableMediaContentFileByStudentIdAsync>d__13>(ref <LoadAvailableMediaContentFileByStudentIdAsync>d__);
			return <LoadAvailableMediaContentFileByStudentIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x00071C85 File Offset: 0x0006FE85
		public void UpdateMediaContentFileWithoutData(MediaContentFileWithoutData mediaContentFile)
		{
			this.MediaContentFileDAO.UpdateMediaContentFileWithoutData(mediaContentFile);
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x00071C98 File Offset: 0x0006FE98
		[DebuggerStepThrough]
		public Task DeleteMediaContentFileAsync(FileIdentifier fileId)
		{
			MediaContentFileManager.<DeleteMediaContentFileAsync>d__15 <DeleteMediaContentFileAsync>d__ = new MediaContentFileManager.<DeleteMediaContentFileAsync>d__15();
			<DeleteMediaContentFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DeleteMediaContentFileAsync>d__.<>4__this = this;
			<DeleteMediaContentFileAsync>d__.fileId = fileId;
			<DeleteMediaContentFileAsync>d__.<>1__state = -1;
			<DeleteMediaContentFileAsync>d__.<>t__builder.Start<MediaContentFileManager.<DeleteMediaContentFileAsync>d__15>(ref <DeleteMediaContentFileAsync>d__);
			return <DeleteMediaContentFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x00071CE4 File Offset: 0x0006FEE4
		public IList<MediaContentFileWithoutData> LoadMediaContentFileByMediaContentPerFormatId(int mediaContentPerFormatId, int studentId = 0)
		{
			return this.MediaContentFileDAO.LoadMediaContentFileByMediaContentPerFormatId(mediaContentPerFormatId, studentId);
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x00071D04 File Offset: 0x0006FF04
		[DebuggerStepThrough]
		public Task<IList<MediaContentFileWithoutData>> LoadMediaContentFileByMediaContentPerFormatIdAsync(int mediaContentPerFormatId, int studentId = 0)
		{
			MediaContentFileManager.<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__17 <LoadMediaContentFileByMediaContentPerFormatIdAsync>d__ = new MediaContentFileManager.<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__17();
			<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<MediaContentFileWithoutData>>.Create();
			<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__.<>4__this = this;
			<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__.mediaContentPerFormatId = mediaContentPerFormatId;
			<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__.studentId = studentId;
			<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__.<>1__state = -1;
			<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__.<>t__builder.Start<MediaContentFileManager.<LoadMediaContentFileByMediaContentPerFormatIdAsync>d__17>(ref <LoadMediaContentFileByMediaContentPerFormatIdAsync>d__);
			return <LoadMediaContentFileByMediaContentPerFormatIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x00071D58 File Offset: 0x0006FF58
		public IList<MediaContentFileWithoutData> LoadMediaContentFileByMediaContentPerFormatId(Guid mediaContentId, MediaContentFormat mediaContentFormat, int studentId = 0)
		{
			return this.MediaContentFileDAO.LoadMediaContentFileByMediaContentPerFormatId(mediaContentId, mediaContentFormat, studentId);
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x00071D78 File Offset: 0x0006FF78
		public int GetCountAvailableAlternateFormatFiles(int mediaContentPerFormatId, int studentId = 0)
		{
			return this.MediaContentFileDAO.GetCountAvailableAlternateFormatFiles(mediaContentPerFormatId, studentId);
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x00071D98 File Offset: 0x0006FF98
		public IList<MediaContentFileWithoutData> GetMediaContentFileMatching(string searchText, int lucourseid = 0)
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			return oldUserSettingManager.GetSettingValue_Bool(this.OpContext.WhoAmI, eSettingCode.SETTING_AlternateFormat_UserDefinedEquivalentCoursesFunction) ? this.MediaContentFileDAO.GetMediaContentFileMatchingUsingUserDefinedEquivalentCoursesAlt(searchText, lucourseid) : this.MediaContentFileDAO.GetMediaContentFileMatchingUsingEquivalentCoursesAlt(searchText, lucourseid);
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x00071DEC File Offset: 0x0006FFEC
		[DebuggerStepThrough]
		public Task<IList<StudentMediaContentFileWithProofOfPurchaseInfo>> LoadAvailableMediaContentFileByStudentAndMediaContentAsync(int studentId, Guid mediaContentId, DateTime startDate, DateTime endDate)
		{
			MediaContentFileManager.<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__21 <LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__ = new MediaContentFileManager.<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__21();
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<StudentMediaContentFileWithProofOfPurchaseInfo>>.Create();
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.<>4__this = this;
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.studentId = studentId;
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.mediaContentId = mediaContentId;
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.startDate = startDate;
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.endDate = endDate;
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.<>1__state = -1;
			<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.<>t__builder.Start<MediaContentFileManager.<LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__21>(ref <LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__);
			return <LoadAvailableMediaContentFileByStudentAndMediaContentAsync>d__.<>t__builder.Task;
		}
	}
}
