using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.CSharp.RuntimeBinder;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkWeb.Adapters;
using TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Infrastructure;
using TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Models;
using TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Models.StudentRequests;
using TechnoPro.ClockWorkWeb.Binders;
using TechnoPro.ClockWorkWeb.Models;
using TechnoPro.ClockWorkWeb.Models.LookupCourses;
using TechnoPro.Common.ClientManager.Core.AlternateFormat;
using TechnoPro.Common.ClientManager.Core.LookupCourses;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.AlternateFormat;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.AlternativeFormat.Adapters;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.LookupCourses;
using TechnoPro.Common.UI.ClientManager.Web.Core.LookupCourses;
using TechnoPro.Common.UI.Web.Entity;
using TechnoPro.Common.UI.Web.Entity.AlternateFormat;
using TechnoPro.Common.UI.Web.Entity.AlternateFormat.Adapters;
using TechnoPro.Common.UI.Web.Entity.CourseRegistrations;
using TechnoPro.Common.UI.Web.Entity.LookupCourses;
using TechnoPro.Common.UI.Web.Mappers.AlternateFormat;
using TechnoPro.Common.UI.Web.Mappers.LookupCourses;

namespace TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Controllers
{
	// Token: 0x0200018C RID: 396
	[NoCache]
	[ClockWorkRegisteredStudentRequired]
	[AlternateFormatAccommodationRequired]
	[AlternateFormatConfidentialityAgreementRequired]
	public class StudentRequestsController : Controller
	{
		// Token: 0x06000BA4 RID: 2980 RVA: 0x0004B164 File Offset: 0x00049364
		public ActionResult NewRequest()
		{
			return base.RedirectToAction("RequestsByCourse");
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0004B184 File Offset: 0x00049384
		public ActionResult RequestsByCourse()
		{
			if (StudentRequestsController.<>o__2.<>p__0 == null)
			{
				StudentRequestsController.<>o__2.<>p__0 = CallSite<Func<CallSite, object, eClockWorkWebMenu, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SelectedMenu", typeof(StudentRequestsController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			StudentRequestsController.<>o__2.<>p__0.Target(StudentRequestsController.<>o__2.<>p__0, base.ViewBag, eClockWorkWebMenu.AlternateFormat_NewRequest);
			if (StudentRequestsController.<>o__2.<>p__1 == null)
			{
				StudentRequestsController.<>o__2.<>p__1 = CallSite<Func<CallSite, object, eClockWorkWebMenuActions, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SelectedMenuAction", typeof(StudentRequestsController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			StudentRequestsController.<>o__2.<>p__1.Target(StudentRequestsController.<>o__2.<>p__1, base.ViewBag, eClockWorkWebMenuActions.AlternateFormat_NewRequest_By_Course);
			TechnoPro.Common.UI.ClientManager.Web.Core.LookupCourses.ISessionClientManager sessionClientManager = new TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.LookupCourses.SessionClientManager();
			List<SessionView> sessions = sessionClientManager.GetSessions(TermChooserAvailableSessionMode.CurrentTermAndNextTerm);
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			string settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.ALTERNATEFORMAT_StudentRequestByCoursePageTitleText);
			string settingValue2 = webSettingsClientManager.GetSettingValue<string>(Setting.ALTERNATEFORMAT_StudentRequestByCoursePageText);
			RequestByCourseViewModel model = new RequestByCourseViewModel
			{
				PageTitle = settingValue,
				PageDescription = settingValue2,
				Courses = new CoursesByAcademicTermViewModel
				{
					SessionList = sessions
				}
			};
			return base.View("RequestsByCourse", model);
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0004B2BC File Offset: 0x000494BC
		[HttpPost]
		public JsonResult GetCoursesByTerm([ModelBinder(typeof(LogonStudentModelBinder))] PersonBaseDTO student, string selectedSessionId)
		{
			TechnoPro.Common.UI.ClientManager.Web.Core.LookupCourses.ISessionClientManager sessionClientManager = new TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.LookupCourses.SessionClientManager();
			SessionView session = sessionClientManager.GetSession(selectedSessionId);
			bool flag = session == null;
			JsonResult result;
			if (flag)
			{
				result = null;
			}
			else
			{
				ILookupCourseClientManager lookupCourseClientManager = new LookupCourseClientManager();
				IList<CourseRegistrationDTO> source = lookupCourseClientManager.LoadStudentsCourses(session.ToDTO(), student.PersonId);
				SelectList data = new SelectList(source.ToList<CourseRegistrationDTO>().ConvertAll<CourseRegistrationView>((CourseRegistrationDTO c) => new CourseRegistrationView(c)), "LuCourseId", "CourseDescription", 0);
				result = base.Json(data);
			}
			return result;
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0004B350 File Offset: 0x00049550
		[HttpGet]
		public ViewResult CreateNewContent()
		{
			if (StudentRequestsController.<>o__4.<>p__0 == null)
			{
				StudentRequestsController.<>o__4.<>p__0 = CallSite<Func<CallSite, object, eClockWorkWebMenu, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SelectedMenu", typeof(StudentRequestsController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			StudentRequestsController.<>o__4.<>p__0.Target(StudentRequestsController.<>o__4.<>p__0, base.ViewBag, eClockWorkWebMenu.AlternateFormat_NewRequest);
			if (StudentRequestsController.<>o__4.<>p__1 == null)
			{
				StudentRequestsController.<>o__4.<>p__1 = CallSite<Func<CallSite, object, eClockWorkWebMenuActions, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SelectedMenuAction", typeof(StudentRequestsController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			StudentRequestsController.<>o__4.<>p__1.Target(StudentRequestsController.<>o__4.<>p__1, base.ViewBag, eClockWorkWebMenuActions.AlternateFormat_NewRequest_By_Searching);
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			string settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.ALTERNATEFORMAT_NewMediaContentRequestPageTitleText);
			string settingValue2 = webSettingsClientManager.GetSettingValue<string>(Setting.ALTERNATEFORMAT_NewMediaContentRequestPageText);
			IMediaPublisherClientManager mediaPublisherClientManager = new MediaPublisherClientManager();
			IList<MediaPublisherDTO> publishers = mediaPublisherClientManager.LoadAllPublishers();
			WebMediaContentRequest model = new WebMediaContentRequest
			{
				MediaContentUniqueId = Guid.NewGuid(),
				Title = string.Empty,
				Authors = string.Empty,
				ISBN = string.Empty,
				PublisherId = 0,
				Publishers = publishers,
				PageTitle = settingValue,
				PageDescription = settingValue2
			};
			return base.View("CreateNewContentRequest", model);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0004B4B4 File Offset: 0x000496B4
		[HttpPost]
		public ActionResult CreateNewContent(PendingRequestsCart cart, WebMediaContentRequest content)
		{
			if (StudentRequestsController.<>o__5.<>p__0 == null)
			{
				StudentRequestsController.<>o__5.<>p__0 = CallSite<Func<CallSite, object, eClockWorkWebMenu, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SelectedMenu", typeof(StudentRequestsController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			StudentRequestsController.<>o__5.<>p__0.Target(StudentRequestsController.<>o__5.<>p__0, base.ViewBag, eClockWorkWebMenu.AlternateFormat_NewRequest);
			if (StudentRequestsController.<>o__5.<>p__1 == null)
			{
				StudentRequestsController.<>o__5.<>p__1 = CallSite<Func<CallSite, object, eClockWorkWebMenuActions, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SelectedMenuAction", typeof(StudentRequestsController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			StudentRequestsController.<>o__5.<>p__1.Target(StudentRequestsController.<>o__5.<>p__1, base.ViewBag, eClockWorkWebMenuActions.AlternateFormat_NewRequest_By_Searching);
			MediaPublisherDTO mediaPublisherDTO = null;
			bool flag = content.PublisherId > 0;
			if (flag)
			{
				IMediaPublisherClientManager mediaPublisherClientManager = new MediaPublisherClientManager();
				mediaPublisherDTO = mediaPublisherClientManager.LoadPublisherById(content.PublisherId);
			}
			MediaContentWebView mediaContentWebView = new MediaContentWebView
			{
				Identifier = new MediaContentIdentifierDTO
				{
					ISBN = content.ISBN,
					MediaContentUniqueId = new Guid?(content.MediaContentUniqueId)
				},
				ShortTitle = content.Title,
				Authors = content.Authors,
				Edition = content.Edition,
				ISBN = content.ISBN,
				PublisherId = ((mediaPublisherDTO != null) ? mediaPublisherDTO.PublisherId : 0),
				Publisher = ((mediaPublisherDTO != null) ? mediaPublisherDTO.Name : string.Empty),
				PublisherEmail = ((mediaPublisherDTO != null) ? mediaPublisherDTO.Email : string.Empty),
				PublisherAddress = ((mediaPublisherDTO != null) ? mediaPublisherDTO.Address : string.Empty),
				PublisherFax = ((mediaPublisherDTO != null) ? mediaPublisherDTO.Fax : string.Empty),
				PublisherPhone = ((mediaPublisherDTO != null) ? mediaPublisherDTO.Phone : string.Empty),
				PublisherWebsite = ((mediaPublisherDTO != null) ? mediaPublisherDTO.Website : string.Empty),
				ProofOfPurchaseRequired = true,
				IsANewUserCreatedMediaContent = true
			};
			bool flag2 = !cart.Contains(mediaContentWebView.Id);
			if (flag2)
			{
				cart.AddRequest(mediaContentWebView, null);
			}
			if (StudentRequestsController.<>o__5.<>p__2 == null)
			{
				StudentRequestsController.<>o__5.<>p__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "MediaContentAddedMessage", typeof(StudentRequestsController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			StudentRequestsController.<>o__5.<>p__2.Target(StudentRequestsController.<>o__5.<>p__2, base.ViewBag, string.Format("Your Media Content '<b>{0}{1}</b>' was successfully added to your pending requests list", mediaContentWebView.ShortTitle ?? string.Empty, string.IsNullOrEmpty(mediaContentWebView.ISBN) ? string.Empty : string.Format("({0})", mediaContentWebView.ISBN)));
			return base.RedirectToAction("Index", "PendingRequestsCart");
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0004B78C File Offset: 0x0004998C
		public ActionResult List()
		{
			if (StudentRequestsController.<>o__6.<>p__0 == null)
			{
				StudentRequestsController.<>o__6.<>p__0 = CallSite<Func<CallSite, object, eClockWorkWebMenu, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SelectedMenu", typeof(StudentRequestsController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			StudentRequestsController.<>o__6.<>p__0.Target(StudentRequestsController.<>o__6.<>p__0, base.ViewBag, eClockWorkWebMenu.AlternateFormat_MyRequests);
			TechnoPro.Common.UI.ClientManager.Web.Core.LookupCourses.ISessionClientManager sessionClientManager = new TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.LookupCourses.SessionClientManager();
			List<SessionView> sessions = sessionClientManager.GetSessions(TermChooserAvailableSessionMode.PreviousTermAndCurrentTermAndNextTerm);
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			string settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.ALTERNATEFORMAT_StudentRequestsPageTitleText);
			string settingValue2 = webSettingsClientManager.GetSettingValue<string>(Setting.ALTERNATEFORMAT_StudentRequestsPageText);
			StudentRequestListViewModel model = new StudentRequestListViewModel
			{
				PageTitle = settingValue,
				PageDescription = settingValue2,
				Session = new SessionViewModel
				{
					SessionList = sessions
				}
			};
			return base.View("MyRequestList", model);
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0004B864 File Offset: 0x00049A64
		public async Task<PartialViewResult> GetStudentRequestListAsync([ModelBinder(typeof(LogonStudentModelBinder))] PersonBaseDTO student, string selectedTermId, int page = 1)
		{
			if (StudentRequestsController.<>o__7.<>p__0 == null)
			{
				StudentRequestsController.<>o__7.<>p__0 = CallSite<Func<CallSite, object, eClockWorkWebMenu, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SelectedMenu", typeof(StudentRequestsController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			StudentRequestsController.<>o__7.<>p__0.Target(StudentRequestsController.<>o__7.<>p__0, this.ViewBag, eClockWorkWebMenu.AlternateFormat_MyRequests);
			if (StudentRequestsController.<>o__7.<>p__1 == null)
			{
				StudentRequestsController.<>o__7.<>p__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SelectedTermId", typeof(StudentRequestsController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			StudentRequestsController.<>o__7.<>p__1.Target(StudentRequestsController.<>o__7.<>p__1, this.ViewBag, selectedTermId);
			if (StudentRequestsController.<>o__7.<>p__2 == null)
			{
				StudentRequestsController.<>o__7.<>p__2 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "CurrentPage", typeof(StudentRequestsController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			StudentRequestsController.<>o__7.<>p__2.Target(StudentRequestsController.<>o__7.<>p__2, this.ViewBag, page);
			bool flag = string.IsNullOrEmpty(selectedTermId);
			PartialViewResult result2;
			if (flag)
			{
				result2 = null;
			}
			else
			{
				TechnoPro.Common.UI.ClientManager.Web.Core.LookupCourses.ISessionClientManager sessionClientManager = new TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.LookupCourses.SessionClientManager();
				SessionView session = sessionClientManager.GetSession(selectedTermId);
				IStudentMediaRequestClientManager studentMediaRequestWebClientManager = new StudentMediaRequestClientManager();
				IList<MediaContentRequestedInfoExtendedDTO> list = await studentMediaRequestWebClientManager.LoadAllStudentMediaRequestByStudentAndDatesAsync(student.PersonId, session.StartDate, session.EndDate);
				IList<MediaContentRequestedInfoExtendedDTO> studentRequestList = list;
				list = null;
				List<MediaContentRequestedListViewModel> list2;
				if (studentRequestList == null)
				{
					list2 = null;
				}
				else
				{
					list2 = (from r in studentRequestList
					group r by r.ContentDetailRequested.MediaContent.MediaContentUniqueId).Select(delegate(IGrouping<Guid, MediaContentRequestedInfoExtendedDTO> g)
					{
						MediaContentRequestedListViewModel mediaContentRequestedListViewModel = new MediaContentRequestedListViewModel();
						mediaContentRequestedListViewModel.MediaContentDetail = g.First<MediaContentRequestedInfoExtendedDTO>().ContentDetailRequested;
						mediaContentRequestedListViewModel.StudentRequestList = (from g1 in g
						group g1 by g1.ContentDetailRequested.MediaContentPerFormatId into g2
						select g2.First<MediaContentRequestedInfoExtendedDTO>() into x
						select new StudentRequestWebView(x)).ToList<StudentRequestWebView>();
						mediaContentRequestedListViewModel.ProofOfPurchaseId = g.First<MediaContentRequestedInfoExtendedDTO>().ProofOfPurchaseId;
						return mediaContentRequestedListViewModel;
					}).ToList<MediaContentRequestedListViewModel>();
				}
				List<MediaContentRequestedListViewModel> contentList = list2;
				StudentRequestsByTermViewModel studentRequestsByTermViewModel = new StudentRequestsByTermViewModel();
				IList<MediaContentRequestedListViewModel> studentRequestList2;
				if (contentList == null)
				{
					studentRequestList2 = null;
				}
				else
				{
					studentRequestList2 = (from f in contentList
					orderby f.MediaContentDetail.MediaContent.ShortTitle
					select f).Skip((page - 1) * this.PageSizeForStudentRequests).Take(this.PageSizeForStudentRequests).ToList<MediaContentRequestedListViewModel>();
				}
				studentRequestsByTermViewModel.StudentRequestList = studentRequestList2;
				studentRequestsByTermViewModel.PagingInfo = new PagingInfo
				{
					CurrentPage = page,
					ItemsPerPage = this.PageSizeForStudentRequests,
					TotalItems = ((contentList != null) ? contentList.Count : 0)
				};
				studentRequestsByTermViewModel.SelectedTermId = selectedTermId;
				StudentRequestsByTermViewModel result = studentRequestsByTermViewModel;
				result2 = this.PartialView("StudentRequestsByTerm", result);
			}
			return result2;
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0004B8C0 File Offset: 0x00049AC0
		public PartialViewResult GetStudentRequestToolStrip(string mediaContentUniqueId, string mediaContentTitle, int proofOfPurchaseId, bool proofOfPurchaseRequired)
		{
			return this.PartialView("StudentRequestsToolStrip", new StudentRequestsToolStripViewModel
			{
				MediaContentUniqueId = mediaContentUniqueId,
				MediaContentTitle = mediaContentTitle,
				ProofOfPurchaseRequired = proofOfPurchaseRequired,
				ProofOfPurchaseId = proofOfPurchaseId
			});
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0004B904 File Offset: 0x00049B04
		public async Task<PartialViewResult> CancelRequestAsync([ModelBinder(typeof(LogonStudentModelBinder))] PersonBaseDTO student, int requestId, string selectedTermId, int page = 1)
		{
			IStudentMediaRequestClientManager studentMediaRequestWebClientManager = new StudentMediaRequestClientManager();
			studentMediaRequestWebClientManager.DeleteStudentContentMediaRequestInfo(requestId);
			return await this.GetStudentRequestListAsync(student, selectedTermId, page);
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0004B968 File Offset: 0x00049B68
		public async Task<PartialViewResult> CancelAllRequestAsync([ModelBinder(typeof(LogonStudentModelBinder))] PersonBaseDTO student, IList<int> requestIdList, string selectedTermId, int page = 1)
		{
			IStudentMediaRequestClientManager studentMediaRequestWebClientManager = new StudentMediaRequestClientManager();
			foreach (int requestId in requestIdList)
			{
				studentMediaRequestWebClientManager.DeleteStudentContentMediaRequestInfo(requestId);
			}
			IEnumerator<int> enumerator = null;
			return await this.GetStudentRequestListAsync(student, selectedTermId, page);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0004B9CC File Offset: 0x00049BCC
		public ActionResult RequestBySearching()
		{
			if (StudentRequestsController.<>o__11.<>p__0 == null)
			{
				StudentRequestsController.<>o__11.<>p__0 = CallSite<Func<CallSite, object, eClockWorkWebMenu, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SelectedMenu", typeof(StudentRequestsController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			StudentRequestsController.<>o__11.<>p__0.Target(StudentRequestsController.<>o__11.<>p__0, base.ViewBag, eClockWorkWebMenu.AlternateFormat_NewRequest);
			if (StudentRequestsController.<>o__11.<>p__1 == null)
			{
				StudentRequestsController.<>o__11.<>p__1 = CallSite<Func<CallSite, object, eClockWorkWebMenuActions, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SelectedMenuAction", typeof(StudentRequestsController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			StudentRequestsController.<>o__11.<>p__1.Target(StudentRequestsController.<>o__11.<>p__1, base.ViewBag, eClockWorkWebMenuActions.AlternateFormat_NewRequest_By_Searching);
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			string settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.ALTERNATEFORMAT_StudentRequestBySearchingPageTitleText);
			string settingValue2 = webSettingsClientManager.GetSettingValue<string>(Setting.ALTERNATEFORMAT_StudentRequestBySearchingPageText);
			RequestsBySearchingViewModel model = new RequestsBySearchingViewModel
			{
				PageTitle = settingValue,
				PageDescription = settingValue2,
				SearchText = string.Empty
			};
			return base.View("RequestBySearching", model);
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0004BAE8 File Offset: 0x00049CE8
		public async Task<FileContentResult> DownloadProofOfPurchase([ModelBinder(typeof(PersonBaseModelBinder))] PersonBaseDTO student, int proofOfPurchaseId, string bookTitle)
		{
			bool flag = proofOfPurchaseId == 0;
			FileContentResult result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IStudentMediaRequestClientManager studentMediaRequestClientManager = new StudentMediaRequestClientManager();
				ProofOfPurchaseInfoDTO proofOfPurchaseInfoDTO = await studentMediaRequestClientManager.DownloadProofOfPurchaseAsync(proofOfPurchaseId);
				ProofOfPurchaseInfoDTO pop = proofOfPurchaseInfoDTO;
				proofOfPurchaseInfoDTO = null;
				result = new FileContentResult(pop.ProofOfPurchaseReceipt, "application/octet-stream")
				{
					FileDownloadName = bookTitle.DisplayMediaContentTitle(50) + "- receipt - " + student.GetName() + pop.Extension
				};
			}
			return result;
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0004BB44 File Offset: 0x00049D44
		public ActionResult UploadProofOfPurchase([ModelBinder(typeof(PersonBaseModelBinder))] PersonBaseDTO student, UploadProofOfPurchaseViewModel model)
		{
			if (StudentRequestsController.<>o__13.<>p__0 == null)
			{
				StudentRequestsController.<>o__13.<>p__0 = CallSite<Func<CallSite, object, eClockWorkWebMenu, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SelectedMenu", typeof(StudentRequestsController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			StudentRequestsController.<>o__13.<>p__0.Target(StudentRequestsController.<>o__13.<>p__0, base.ViewBag, eClockWorkWebMenu.AlternateFormat_MyRequests);
			IMediaContentClientManager mediaContentClientManager = new MediaContentClientManager();
			model.MediaContent = mediaContentClientManager.LoadMediaContentByIdentifier(model.MediaContent.Identifier).ToWebView();
			model.PageTitle = "Upload proof of purchase receipt";
			model.PageDescription = "Please upload a proof of purchase receipt for the following book";
			model.Student = student;
			return base.View("UploadProofOfPurchase", model);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0004BC04 File Offset: 0x00049E04
		[HttpPost]
		public async Task<JsonResult> UploadProofOfPurchaseByIdAsync(PersonBaseDTO student, string id)
		{
			bool flag = string.IsNullOrEmpty(id);
			JsonResult result;
			if (flag)
			{
				result = this.ThrowJsonError("Empty book id", "");
			}
			else
			{
				bool flag2 = this.Request.Files.Count == 0;
				if (flag2)
				{
					result = this.ThrowJsonError("There is not file to upload", id);
				}
				else
				{
					HttpPostedFileBase hpf = this.Request.Files[0];
					bool flag3 = hpf == null || hpf.ContentLength == 0;
					if (flag3)
					{
						result = this.ThrowJsonError("Zero file size", id);
					}
					else
					{
						bool flag4 = !hpf.ValidateReceiptFormat();
						if (flag4)
						{
							result = this.ThrowJsonError(string.Format("File MIME content type '{0}' is not supported. Supported file types are: {1}", hpf.ContentType, HttpPostedFileBaseAdapter.ReceiptSupportedFiles.CommaSeparatedValues<string>()), id);
						}
						else
						{
							byte[] bytes = hpf.SaveAsBytes();
							bool flag5 = bytes != null;
							if (flag5)
							{
								ProofOfPurchaseInfoDTO proofOfPurchase = new ProofOfPurchaseInfoDTO
								{
									MediaContentUniqueId = new Guid(id),
									StudentPersonId = student.PersonId,
									Notes = string.Format("Proof of purchase was upload online by the student on '{0}'", DateTime.Now.ToString("MMM d, yyyy hh:mm tt")),
									ProofOfPurchaseReceipt = bytes,
									Filename = hpf.FileName,
									Extension = Path.GetExtension(hpf.FileName)
								};
								IStudentMediaRequestClientManager studentMediaRequestWebClientManager = new StudentMediaRequestClientManager();
								ProofOfPurchaseInfoDTO proofOfPurchaseInfoDTO = proofOfPurchase;
								int proofOfPurchaseId = await studentMediaRequestWebClientManager.UploadProofOfPurchaseAsync(proofOfPurchase);
								proofOfPurchaseInfoDTO.ProofOfPurchaseId = proofOfPurchaseId;
								proofOfPurchaseInfoDTO = null;
								result = this.Json(new
								{
									mediaContentUniqueId = id,
									proofOfPurchaseId = proofOfPurchase.ProofOfPurchaseId,
									mediaContentTitle = proofOfPurchase.Filename,
									proofOfPurchaseRequired = true,
									name = hpf.FileName,
									length = hpf.ContentLength,
									type = hpf.ContentType
								});
							}
							else
							{
								result = this.ThrowJsonError("Image is not in the correct format", id);
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0004BC5C File Offset: 0x00049E5C
		[HttpPost]
		public ActionResult UploadProofOfPurchase([ModelBinder(typeof(PersonBaseModelBinder))] PersonBaseDTO student, UploadProofOfPurchaseFileViewModel fileModel)
		{
			fileModel.Student = student;
			bool flag = base.ModelState.IsValid && fileModel.File != null && fileModel.File.ContentLength > 0;
			ActionResult result;
			if (flag)
			{
				bool flag2 = !fileModel.File.ValidateReceiptFormat();
				if (flag2)
				{
					base.ModelState.AddModelError("Receipt", string.Format("File MIME content type '{0}' is not supported. Supported file types are: {1}", fileModel.File.ContentType, HttpPostedFileBaseAdapter.ReceiptSupportedFiles.CommaSeparatedValues<string>()));
					result = base.View("UploadProofOfPurchase", fileModel);
				}
				else
				{
					byte[] array = fileModel.File.SaveAsBytes();
					bool flag3 = array == null;
					if (flag3)
					{
						base.ModelState.AddModelError("Receipt", "Receipt image file is null or invalid.");
						result = base.View("UploadProofOfPurchase", fileModel);
					}
					else
					{
						ProofOfPurchaseInfoDTO proofOfPurchaseInfoDTO = new ProofOfPurchaseInfoDTO
						{
							MediaContentUniqueId = fileModel.MediaContent.Identifier.MediaContentUniqueId.Value,
							StudentPersonId = fileModel.Student.PersonId,
							Notes = string.Format("Proof of purchase was upload online by the student on '{0}'", DateTime.Now.ToString("MMM d, yyyy hh:mm tt")),
							ProofOfPurchaseReceipt = array,
							Filename = fileModel.File.FileName,
							Extension = Path.GetExtension(fileModel.File.FileName)
						};
						IStudentMediaRequestClientManager studentMediaRequestClientManager = new StudentMediaRequestClientManager();
						proofOfPurchaseInfoDTO.ProofOfPurchaseId = studentMediaRequestClientManager.UploadProofOfPurchase(proofOfPurchaseInfoDTO);
						fileModel.PageTitle = "Proof of purchase upload confirmation";
						fileModel.PageDescription = string.Format("Proof of purchase for your media content request <b>{0}</b><i>{1}</i> has been uploaded successfully", fileModel.MediaContent.ShortTitle ?? "", string.IsNullOrEmpty(fileModel.MediaContent.ISBN) ? "" : string.Format(" ({0})", fileModel.MediaContent.ISBN.DisplayISBNFormat()));
						result = base.View("UploadProofOfPurchaseConfirmation", fileModel);
					}
				}
			}
			else
			{
				result = base.View("UploadProofOfPurchase", fileModel);
			}
			return result;
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0004BE60 File Offset: 0x0004A060
		private JsonResult ThrowJsonError(string message, string id)
		{
			base.Response.StatusCode = 400;
			base.Response.StatusDescription = string.Format("{0}:{1}", id, message);
			return base.Json(new
			{
				message,
				id
			}, JsonRequestBehavior.AllowGet);
		}

		// Token: 0x04000864 RID: 2148
		public int PageSizeForStudentRequests = 5;
	}
}
