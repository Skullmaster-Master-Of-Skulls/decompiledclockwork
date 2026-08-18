using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.CSharp.RuntimeBinder;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Infrastructure;
using TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Models;
using TechnoPro.ClockWorkWeb.Binders;
using TechnoPro.ClockWorkWeb.Models;
using TechnoPro.ClockWorkWeb.Models.LookupCourses;
using TechnoPro.Common.ClientManager.Core.AlternateFormat;
using TechnoPro.Common.ClientManager.Core.FileStorage;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.AlternateFormat;
using TechnoPro.Common.ClientManager.ICore.FileStorage;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.FileStorage;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.LookupCourses;
using TechnoPro.Common.UI.ClientManager.Web.Core.LookupCourses;
using TechnoPro.Common.UI.Web.Entity;
using TechnoPro.Common.UI.Web.Entity.AlternateFormat;
using TechnoPro.Common.UI.Web.Entity.LookupCourses;
using TechnoPro.Common.UI.Web.Mappers.AlternateFormat;

namespace TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Controllers
{
	// Token: 0x0200018B RID: 395
	[NoCache]
	[ClockWorkRegisteredStudentRequired]
	[AlternateFormatAccommodationRequired]
	[AlternateFormatConfidentialityAgreementRequired]
	public class StudentFilesController : Controller
	{
		// Token: 0x06000B9F RID: 2975 RVA: 0x0004AF6C File Offset: 0x0004916C
		public ViewResult List()
		{
			if (StudentFilesController.<>o__1.<>p__0 == null)
			{
				StudentFilesController.<>o__1.<>p__0 = CallSite<Func<CallSite, object, eClockWorkWebMenu, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SelectedMenu", typeof(StudentFilesController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			StudentFilesController.<>o__1.<>p__0.Target(StudentFilesController.<>o__1.<>p__0, base.ViewBag, eClockWorkWebMenu.AlternateFormat_MyFiles);
			ISessionClientManager sessionClientManager = new SessionClientManager();
			List<SessionView> sessions = sessionClientManager.GetSessions(TermChooserAvailableSessionMode.PreviousTermAndCurrentTermAndNextTerm);
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			string settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.ALTERNATEFORMAT_StudentFilesPageTitleText);
			string settingValue2 = webSettingsClientManager.GetSettingValue<string>(Setting.ALTERNATEFORMAT_StudentFilesPageText);
			StudentFilesViewModel model = new StudentFilesViewModel
			{
				PageTitle = settingValue,
				PageDescription = settingValue2,
				Session = new SessionViewModel
				{
					SessionList = sessions
				}
			};
			return base.View("MyFiles", model);
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0004B044 File Offset: 0x00049244
		public async Task<PartialViewResult> GetStudentMediaContentFilesAsync([ModelBinder(typeof(LogonStudentModelBinder))] PersonBaseDTO student, string selectedTermId, int page = 1)
		{
			if (StudentFilesController.<>o__2.<>p__0 == null)
			{
				StudentFilesController.<>o__2.<>p__0 = CallSite<Func<CallSite, object, eClockWorkWebMenu, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SelectedMenu", typeof(StudentFilesController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			StudentFilesController.<>o__2.<>p__0.Target(StudentFilesController.<>o__2.<>p__0, this.ViewBag, eClockWorkWebMenu.AlternateFormat_MyFiles);
			if (StudentFilesController.<>o__2.<>p__1 == null)
			{
				StudentFilesController.<>o__2.<>p__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SelectedTermId", typeof(StudentFilesController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			StudentFilesController.<>o__2.<>p__1.Target(StudentFilesController.<>o__2.<>p__1, this.ViewBag, selectedTermId);
			if (StudentFilesController.<>o__2.<>p__2 == null)
			{
				StudentFilesController.<>o__2.<>p__2 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "CurrentPageIndex", typeof(StudentFilesController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			StudentFilesController.<>o__2.<>p__2.Target(StudentFilesController.<>o__2.<>p__2, this.ViewBag, page);
			bool flag = string.IsNullOrEmpty(selectedTermId);
			PartialViewResult result2;
			if (flag)
			{
				result2 = null;
			}
			else
			{
				ISessionClientManager sessionClientManager = new SessionClientManager();
				SessionView session = sessionClientManager.GetSession(selectedTermId);
				IMediaContentFileClientManager mediaContentFileWebClientManager = new MediaContentFileClientManager();
				IList<StudentMediaContentFileWithProofOfPurchaseInfoDTO> list;
				if (session == null)
				{
					list = null;
				}
				else
				{
					IList<StudentMediaContentFileWithProofOfPurchaseInfoDTO> list2 = await mediaContentFileWebClientManager.LoadAvailableMediaContentFileByStudentIdAsync(student.PersonId, session.StartDate, session.EndDate);
					list = list2;
					list2 = null;
				}
				IList<StudentMediaContentFileWithProofOfPurchaseInfoDTO> studentMediaContentFileList = list;
				list = null;
				List<MediaContentFileListViewModel> list3;
				if (session != null)
				{
					list3 = (from f in studentMediaContentFileList
					group f by f.MediaContent.MediaContentUniqueId into g
					select new
					{
						g = g,
						mcf = g.FirstOrDefault((StudentMediaContentFileWithProofOfPurchaseInfoDTO k) => k != null && k.MediaContent != null)
					}).Select(delegate(<>h__TransparentIdentifier0)
					{
						MediaContentFileListViewModel mediaContentFileListViewModel = new MediaContentFileListViewModel();
						StudentMediaContentFileWithProofOfPurchaseInfoDTO mcf = <>h__TransparentIdentifier0.mcf;
						mediaContentFileListViewModel.MediaContent = ((mcf != null) ? mcf.MediaContent.ToWebView() : null);
						mediaContentFileListViewModel.MediaContentFileList = (from x in <>h__TransparentIdentifier0.g
						select new MediaContentFileWithoutDataWebView(x)).ToList<MediaContentFileWithoutDataWebView>();
						StudentMediaContentFileWithProofOfPurchaseInfoDTO mcf2 = <>h__TransparentIdentifier0.mcf;
						mediaContentFileListViewModel.FileStatus = ((mcf2 != null) ? new eStudentMediaContentFileStatus?(mcf2.FileStatus) : null);
						StudentMediaContentFileWithProofOfPurchaseInfoDTO mcf3 = <>h__TransparentIdentifier0.mcf;
						mediaContentFileListViewModel.ProofOfPurchaseId = ((mcf3 != null) ? mcf3.ProofOfPurchaseId : 0);
						return mediaContentFileListViewModel;
					}).ToList<MediaContentFileListViewModel>();
				}
				else
				{
					list3 = null;
				}
				List<MediaContentFileListViewModel> studentFileList = list3;
				StudentMediaContentFilesViewModel studentMediaContentFilesViewModel = new StudentMediaContentFilesViewModel();
				List<MediaContentFileListViewModel> list4 = studentFileList;
				IList<MediaContentFileListViewModel> mediaContentList;
				if (list4 == null)
				{
					mediaContentList = null;
				}
				else
				{
					mediaContentList = (from f in list4
					orderby f.MediaContent.Identifier.MediaContentId
					select f).Skip((page - 1) * this.PageSize).Take(this.PageSize).ToList<MediaContentFileListViewModel>();
				}
				studentMediaContentFilesViewModel.MediaContentList = mediaContentList;
				PagingInfo pagingInfo = new PagingInfo();
				pagingInfo.CurrentPage = page;
				pagingInfo.ItemsPerPage = this.PageSize;
				List<MediaContentFileListViewModel> list5 = studentFileList;
				pagingInfo.TotalItems = ((list5 != null) ? list5.Count : 0);
				studentMediaContentFilesViewModel.PagingInfo = pagingInfo;
				studentMediaContentFilesViewModel.SelectedTermId = selectedTermId;
				StudentMediaContentFilesViewModel result = studentMediaContentFilesViewModel;
				result2 = this.PartialView("GetStudentMediaContentFiles", result);
			}
			return result2;
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0004B0A0 File Offset: 0x000492A0
		public async Task DownloadFilesByContentAndFormat([ModelBinder(typeof(LogonStudentModelBinder))] PersonBaseDTO student, int mediaContentPerFormatId)
		{
			IMediaContentFileClientManager mediaContentFileWebClientManager = new MediaContentFileClientManager();
			IList<MediaContentFileWithoutDataDTO> list = await mediaContentFileWebClientManager.LoadMediaContentFileByMediaContentPerFormatIdAsync(mediaContentPerFormatId, (student != null) ? student.PersonId : 0);
			IList<MediaContentFileWithoutDataDTO> files = list;
			list = null;
			IFilesStorageClientManager filesStorageClientManager = new FilesStorageClientManager();
			this.Response.ContentType = "application/zip";
			this.Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", string.Format("Alternate_format_student_files-{0}.zip", student.GetName()).Replace(' ', '_')));
			await filesStorageClientManager.ZipFilesAsync(new PositionWrapperStream(this.Response.OutputStream), (from f in files
			select new FileIdentifier
			{
				Source = eFileSource.AlternativeFormat_MediaContentFile,
				FileUniqueId = f.MediaContentFileUniqueId,
				LegacyId = f.MediaContentFileId
			}).ToArray<FileIdentifier>());
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0004B0F8 File Offset: 0x000492F8
		public async Task<FileStreamResult> DownloadFiles([ModelBinder(typeof(LogonStudentModelBinder))] PersonBaseDTO student, Guid mediaContentId, string selectedTermId)
		{
			ISessionClientManager sessionClientManager = new SessionClientManager();
			List<SessionView> termList = sessionClientManager.GetSessions(TermChooserAvailableSessionMode.CurrentTermAndNextTerm);
			SessionView selTerm = termList.FirstOrDefault((SessionView t) => t.Id == selectedTermId);
			IMediaContentFileClientManager mediaContentFileWebClientManager = new MediaContentFileClientManager();
			IList<StudentMediaContentFileWithProofOfPurchaseInfoDTO> list = await mediaContentFileWebClientManager.LoadAvailableMediaContentFileByStudentAndMediaContentAsync((student != null) ? student.PersonId : 0, mediaContentId, selTerm.StartDate, selTerm.EndDate);
			IList<StudentMediaContentFileWithProofOfPurchaseInfoDTO> files = list;
			list = null;
			IFilesStorageClientManager filesStorageClientManager = new FilesStorageClientManager();
			Stream stream2 = await filesStorageClientManager.ZipFilesAsync((from f in files
			select new FileIdentifier
			{
				Source = eFileSource.AlternativeFormat_MediaContentFile,
				FileUniqueId = f.MediaContentFileUniqueId,
				LegacyId = f.MediaContentFileId
			}).ToArray<FileIdentifier>());
			Stream stream = stream2;
			stream2 = null;
			stream.Position = 0L;
			return this.File(stream, "application/zip", string.Format("Alternate format student files for {0} - {1}.zip", selTerm.Title, student.GetName()));
		}

		// Token: 0x04000863 RID: 2147
		public int PageSize = 5;
	}
}
