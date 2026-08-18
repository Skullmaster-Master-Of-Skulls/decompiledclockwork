using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.CSharp.RuntimeBinder;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Infrastructure;
using TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Models;
using TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Models.MediaContent;
using TechnoPro.ClockWorkWeb.Models;
using TechnoPro.Common.ClientManager.Core.AlternateFormat;
using TechnoPro.Common.ClientManager.ICore.AlternateFormat;
using TechnoPro.Common.UI.Web.Entity;
using TechnoPro.Common.UI.Web.Entity.AlternateFormat;
using TechnoPro.Common.UI.Web.Mappers.AlternateFormat;

namespace TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Controllers
{
	// Token: 0x02000187 RID: 391
	[NoCache]
	[ClockWorkRegisteredStudentRequired]
	[AlternateFormatAccommodationRequired]
	[AlternateFormatConfidentialityAgreementRequired]
	public class MediaContentController : Controller
	{
		// Token: 0x06000B83 RID: 2947 RVA: 0x00049BB0 File Offset: 0x00047DB0
		public PartialViewResult ShowMediaContentThumbnail(MediaContentIdentifierDTO id)
		{
			bool flag = id == null;
			PartialViewResult result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IMediaContentClientManager mediaContentClientManager = new MediaContentClientManager();
				MediaContentDTO mContent = mediaContentClientManager.LoadMediaContentByIdentifier(id);
				result = this.PartialView("MediaContentThumbnail", new MediaContentThumbnailViewModel(mContent));
			}
			return result;
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x00049BF0 File Offset: 0x00047DF0
		public ActionResult GetMediaContentThumbnail(MediaContentIdentifierDTO id)
		{
			bool flag = id == null;
			ActionResult result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IMediaContentClientManager mediaContentClientManager = new MediaContentClientManager();
				byte[] mediaContentThumbnailBytes = mediaContentClientManager.GetMediaContentThumbnailBytes(id);
				result = ((mediaContentThumbnailBytes != null) ? new FileContentResult(mediaContentThumbnailBytes, "image/jpeg") : base.File(base.Server.MapPath("~/img/cover-not-available.png"), "image/png"));
			}
			return result;
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x00049C48 File Offset: 0x00047E48
		public async Task<ActionResult> GetMediaContentThumbnailAsync(MediaContentIdentifierDTO id)
		{
			bool flag = id == null;
			ActionResult result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IMediaContentClientManager mediaContentWebClientManager = new MediaContentClientManager();
				byte[] array = await mediaContentWebClientManager.GetMediaContentThumbnailBytesAsync(id).ConfigureAwait(false);
				byte[] iBytes = array;
				array = null;
				result = ((iBytes != null) ? new FileContentResult(iBytes, "image/jpeg") : this.File(this.Server.MapPath("~/img/cover-not-available.png"), "image/png"));
			}
			return result;
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x00049C98 File Offset: 0x00047E98
		public PartialViewResult MediaContentResultsByCourse(int selectedCourseId = 0, int page = 1)
		{
			if (MediaContentController.<>o__5.<>p__0 == null)
			{
				MediaContentController.<>o__5.<>p__0 = CallSite<Func<CallSite, object, eClockWorkWebMenu, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SelectedMenu", typeof(MediaContentController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			MediaContentController.<>o__5.<>p__0.Target(MediaContentController.<>o__5.<>p__0, base.ViewBag, eClockWorkWebMenu.AlternateFormat_NewRequest);
			if (MediaContentController.<>o__5.<>p__1 == null)
			{
				MediaContentController.<>o__5.<>p__1 = CallSite<Func<CallSite, object, eClockWorkWebMenuActions, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SelectedMenuAction", typeof(MediaContentController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			MediaContentController.<>o__5.<>p__1.Target(MediaContentController.<>o__5.<>p__1, base.ViewBag, eClockWorkWebMenuActions.AlternateFormat_NewRequest_By_Course);
			if (MediaContentController.<>o__5.<>p__2 == null)
			{
				MediaContentController.<>o__5.<>p__2 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SelectedCourseId", typeof(MediaContentController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			MediaContentController.<>o__5.<>p__2.Target(MediaContentController.<>o__5.<>p__2, base.ViewBag, selectedCourseId);
			bool flag = base.TempData.ContainsKey("SelectedCourseId");
			if (flag)
			{
				base.TempData.Remove("SelectedCourseId");
			}
			base.TempData.Add("SelectedCourseId", selectedCourseId);
			bool flag2 = selectedCourseId == 0;
			PartialViewResult result;
			if (flag2)
			{
				result = null;
			}
			else
			{
				IMediaContentClientManager mediaContentClientManager = new MediaContentClientManager();
				IList<MediaContentDTO> list = (selectedCourseId > 0) ? mediaContentClientManager.LoadMediaContentByCourse(selectedCourseId) : null;
				ContentResultsByCourseViewModel contentResultsByCourseViewModel = new ContentResultsByCourseViewModel();
				IList<MediaContentWebView> mediaContentList;
				if (list == null)
				{
					mediaContentList = null;
				}
				else
				{
					mediaContentList = (from m in (from f in list
					orderby f.MediaContentDataID
					select f).Skip((page - 1) * this.PageSizeResultByCourse).Take(this.PageSizeResultByCourse)
					select m.ToWebView()).ToList<MediaContentWebView>();
				}
				contentResultsByCourseViewModel.MediaContentList = mediaContentList;
				contentResultsByCourseViewModel.PagingInfo = new PagingInfo
				{
					CurrentPage = page,
					ItemsPerPage = this.PageSizeResultByCourse,
					TotalItems = ((list != null) ? list.Count : 0)
				};
				contentResultsByCourseViewModel.SelectedCourseId = selectedCourseId;
				ContentResultsByCourseViewModel model = contentResultsByCourseViewModel;
				result = this.PartialView("MediaContentResultsByCourse", model);
			}
			return result;
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00049EEC File Offset: 0x000480EC
		public PartialViewResult SearchingMediaContentResults(string searchText, int page = 1)
		{
			if (MediaContentController.<>o__6.<>p__0 == null)
			{
				MediaContentController.<>o__6.<>p__0 = CallSite<Func<CallSite, object, eClockWorkWebMenu, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SelectedMenu", typeof(MediaContentController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			MediaContentController.<>o__6.<>p__0.Target(MediaContentController.<>o__6.<>p__0, base.ViewBag, eClockWorkWebMenu.AlternateFormat_NewRequest);
			if (MediaContentController.<>o__6.<>p__1 == null)
			{
				MediaContentController.<>o__6.<>p__1 = CallSite<Func<CallSite, object, eClockWorkWebMenuActions, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SelectedMenuAction", typeof(MediaContentController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			MediaContentController.<>o__6.<>p__1.Target(MediaContentController.<>o__6.<>p__1, base.ViewBag, eClockWorkWebMenuActions.AlternateFormat_NewRequest_By_Searching);
			IMediaContentClientManager mediaContentClientManager = new MediaContentClientManager();
			IList<MediaContentDTO> list = string.IsNullOrEmpty(searchText) ? null : mediaContentClientManager.GetMediaContentMatching(searchText, 0);
			SearchingContentResultsViewModel searchingContentResultsViewModel = new SearchingContentResultsViewModel();
			IList<MediaContentWebView> mediaContentList;
			if (list == null)
			{
				mediaContentList = null;
			}
			else
			{
				mediaContentList = (from c in (from f in list
				orderby f.Identifier.MediaContentId descending
				select f).Skip((page - 1) * this.PageSizeResultBySearching).Take(this.PageSizeResultBySearching)
				select c.ToWebView()).ToList<MediaContentWebView>();
			}
			searchingContentResultsViewModel.MediaContentList = mediaContentList;
			searchingContentResultsViewModel.PagingInfo = new PagingInfo
			{
				CurrentPage = page,
				ItemsPerPage = this.PageSizeResultBySearching,
				TotalItems = ((list != null) ? list.Count : 0)
			};
			searchingContentResultsViewModel.SearchText = searchText;
			SearchingContentResultsViewModel model = searchingContentResultsViewModel;
			return this.PartialView("SearchingMediaContentResults", model);
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0004A098 File Offset: 0x00048298
		public async Task<PartialViewResult> SearchingMediaContentResultsAsync(string searchText, int page = 1)
		{
			if (MediaContentController.<>o__7.<>p__0 == null)
			{
				MediaContentController.<>o__7.<>p__0 = CallSite<Func<CallSite, object, eClockWorkWebMenu, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SelectedMenu", typeof(MediaContentController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			MediaContentController.<>o__7.<>p__0.Target(MediaContentController.<>o__7.<>p__0, this.ViewBag, eClockWorkWebMenu.AlternateFormat_NewRequest);
			if (MediaContentController.<>o__7.<>p__1 == null)
			{
				MediaContentController.<>o__7.<>p__1 = CallSite<Func<CallSite, object, eClockWorkWebMenuActions, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SelectedMenuAction", typeof(MediaContentController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			MediaContentController.<>o__7.<>p__1.Target(MediaContentController.<>o__7.<>p__1, this.ViewBag, eClockWorkWebMenuActions.AlternateFormat_NewRequest_By_Searching);
			IMediaContentClientManager mediaContentWebClientManager = new MediaContentClientManager();
			IList<MediaContentDTO> list;
			if (string.IsNullOrEmpty(searchText))
			{
				list = null;
			}
			else
			{
				IList<MediaContentDTO> list2 = await mediaContentWebClientManager.GetMediaContentMatchingAsync(searchText, 0);
				list = list2;
				list2 = null;
			}
			IList<MediaContentDTO> contentList = list;
			list = null;
			SearchingContentResultsViewModel searchingContentResultsViewModel = new SearchingContentResultsViewModel();
			IList<MediaContentWebView> mediaContentList;
			if (contentList == null)
			{
				mediaContentList = null;
			}
			else
			{
				mediaContentList = (from c in (from f in contentList
				orderby f.Identifier.MediaContentId descending
				select f).Skip((page - 1) * this.PageSizeResultBySearching).Take(this.PageSizeResultBySearching)
				select c.ToWebView()).ToList<MediaContentWebView>();
			}
			searchingContentResultsViewModel.MediaContentList = mediaContentList;
			searchingContentResultsViewModel.PagingInfo = new PagingInfo
			{
				CurrentPage = page,
				ItemsPerPage = this.PageSizeResultBySearching,
				TotalItems = ((contentList != null) ? contentList.Count : 0)
			};
			searchingContentResultsViewModel.SearchText = searchText;
			SearchingContentResultsViewModel result = searchingContentResultsViewModel;
			return this.PartialView("SearchingMediaContentResults", result);
		}

		// Token: 0x04000861 RID: 2145
		public int PageSizeResultByCourse = 10;

		// Token: 0x04000862 RID: 2146
		public int PageSizeResultBySearching = 10;
	}
}
