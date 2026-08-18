using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using ClockWorkLogger;
using Microsoft.CSharp.RuntimeBinder;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkWeb.Adapters;
using TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Infrastructure;
using TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Models;
using TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Models.MediaContent;
using TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Models.StudentRequests;
using TechnoPro.Common.ClientManager.Core.AlternateFormat;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.AlternateFormat;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.Web.Entity;
using TechnoPro.Common.UI.Web.Entity.AlternateFormat;
using TechnoPro.Common.UI.Web.Mappers.AlternateFormat;

namespace TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Controllers
{
	// Token: 0x02000189 RID: 393
	[NoCache]
	[ClockWorkRegisteredStudentRequired]
	[AlternateFormatAccommodationRequired]
	[AlternateFormatConfidentialityAgreementRequired]
	public class PendingRequestsCartController : Controller
	{
		// Token: 0x06000B8E RID: 2958 RVA: 0x0004A1BC File Offset: 0x000483BC
		[AllowAnonymous]
		public PartialViewResult SummaryWidget(PendingRequestsCart cart)
		{
			return this.PartialView("PendingRequestsSummaryWidget", cart);
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0004A1DC File Offset: 0x000483DC
		public ActionResult Index(PendingRequestsCart cart)
		{
			if (PendingRequestsCartController.<>o__1.<>p__0 == null)
			{
				PendingRequestsCartController.<>o__1.<>p__0 = CallSite<Func<CallSite, object, eClockWorkWebMenu, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SelectedMenu", typeof(PendingRequestsCartController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			PendingRequestsCartController.<>o__1.<>p__0.Target(PendingRequestsCartController.<>o__1.<>p__0, base.ViewBag, eClockWorkWebMenu.AlternateFormat_NewRequest);
			bool flag = base.Request.IsAjaxRequest();
			ActionResult result;
			if (flag)
			{
				result = base.RedirectToAction("PendingRequestsConfirmation", cart);
			}
			else
			{
				bool flag2 = cart.Count > 0;
				if (flag2)
				{
					if (PendingRequestsCartController.<>o__1.<>p__1 == null)
					{
						PendingRequestsCartController.<>o__1.<>p__1 = CallSite<Func<CallSite, object, eClockWorkWebMenu, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SelectedMenu", typeof(PendingRequestsCartController), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					PendingRequestsCartController.<>o__1.<>p__1.Target(PendingRequestsCartController.<>o__1.<>p__1, base.ViewBag, eClockWorkWebMenu.AlternateFormat_Home);
					PendingRequestsCartIndexViewModel model = new PendingRequestsCartIndexViewModel
					{
						Cart = cart,
						PageTitle = "Student media content requests confirmation",
						PageDescription = "Please review and confirm the following requests before submitting"
					};
					IStudentMediaRequestClientManager studentMediaRequestClientManager = new StudentMediaRequestClientManager();
					using (IEnumerator<StudentRequestWebViewModel> enumerator = cart.Items.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							StudentRequestWebViewModel item = enumerator.Current;
							item.StudentMediaContentSelection = (from i in studentMediaRequestClientManager.GetAllowedMediaContentFormatsForStudentToRequest(cart.Student.PersonId, item.MediaContent.Identifier, item.SelectedCourseId ?? 0)
							select new MediaContentFormatViewModel(i, item.MediaContent.Identifier)).ToList<MediaContentFormatViewModel>();
						}
					}
					result = base.View("Index", model);
				}
				else
				{
					result = base.RedirectToAction("Index", "AlternateFormatHome");
				}
			}
			return result;
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0004A3E4 File Offset: 0x000485E4
		[HttpPost]
		public ActionResult Index_Post(PendingRequestsCart cart)
		{
			if (PendingRequestsCartController.<>o__2.<>p__0 == null)
			{
				PendingRequestsCartController.<>o__2.<>p__0 = CallSite<Func<CallSite, object, eClockWorkWebMenu, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SelectedMenu", typeof(PendingRequestsCartController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			PendingRequestsCartController.<>o__2.<>p__0.Target(PendingRequestsCartController.<>o__2.<>p__0, base.ViewBag, eClockWorkWebMenu.AlternateFormat_NewRequest);
			IList<StudentRequestWebViewModel> items = cart.Items;
			List<MediaContentRequestedInfoDTO> list = new List<MediaContentRequestedInfoDTO>();
			foreach (StudentRequestWebViewModel studentRequestWebViewModel in items)
			{
				MediaContentWebView mediaContent = studentRequestWebViewModel.MediaContent;
				BasicMediaContentDTO mediaContent2 = (mediaContent != null) ? mediaContent.ToBasicDTO() : null;
				ProofOfPurchaseInfoDTO proofOfPurchaseInfoDTO;
				if (studentRequestWebViewModel.ProofOfPurchaseReceipt == null)
				{
					proofOfPurchaseInfoDTO = null;
				}
				else
				{
					ProofOfPurchaseInfoDTO proofOfPurchaseInfoDTO2 = new ProofOfPurchaseInfoDTO();
					proofOfPurchaseInfoDTO2.ProofOfPurchaseReceipt = studentRequestWebViewModel.ProofOfPurchaseReceipt;
					proofOfPurchaseInfoDTO2.Notes = string.Empty;
					proofOfPurchaseInfoDTO2.WhoAcceptedProofOfPurchase = null;
					proofOfPurchaseInfoDTO2.WhenWasAccepted = new DateTime?(DateTime.Today);
					proofOfPurchaseInfoDTO2.Filename = studentRequestWebViewModel.Filename;
					proofOfPurchaseInfoDTO2.Extension = studentRequestWebViewModel.Extension;
					proofOfPurchaseInfoDTO = proofOfPurchaseInfoDTO2;
					PersonBaseDTO student = studentRequestWebViewModel.Student;
					proofOfPurchaseInfoDTO2.StudentPersonId = ((student != null) ? student.PersonId : 0);
				}
				ProofOfPurchaseInfoDTO proofOfPurchaseInfoDTO3 = proofOfPurchaseInfoDTO;
				MediaContentRequestedInfoDTO mediaContentRequestedInfoDTO;
				if (studentRequestWebViewModel.MediaContent == null)
				{
					mediaContentRequestedInfoDTO = null;
				}
				else
				{
					MediaContentRequestedInfoDTO mediaContentRequestedInfoDTO2 = new MediaContentRequestedInfoDTO();
					mediaContentRequestedInfoDTO2.RequestMadeFromStudent = cart.Student;
					mediaContentRequestedInfoDTO2.RequestStatus = MediaRequestStatus.Created;
					mediaContentRequestedInfoDTO2.ProofOfPurchase = proofOfPurchaseInfoDTO3;
					mediaContentRequestedInfoDTO2.ProofOfPurchaseId = ((proofOfPurchaseInfoDTO3 != null) ? proofOfPurchaseInfoDTO3.ProofOfPurchaseId : 0);
					mediaContentRequestedInfoDTO2.CreatedDatetime = DateTime.Now;
					mediaContentRequestedInfoDTO = mediaContentRequestedInfoDTO2;
					MediaContentDetailDTO mediaContentDetailDTO = new MediaContentDetailDTO();
					MediaContentFormat? studentSelectedFormat = studentRequestWebViewModel.StudentSelectedFormat;
					MediaContentFormat? studentPreferredFormat;
					if (studentSelectedFormat == null)
					{
						IList<MediaContentFormatViewModel> studentMediaContentSelection = studentRequestWebViewModel.StudentMediaContentSelection;
						if (studentMediaContentSelection == null)
						{
							studentPreferredFormat = null;
						}
						else
						{
							MediaContentFormatViewModel mediaContentFormatViewModel = studentMediaContentSelection.FirstOrDefault<MediaContentFormatViewModel>();
							studentPreferredFormat = ((mediaContentFormatViewModel != null) ? new MediaContentFormat?(mediaContentFormatViewModel.Format) : null);
						}
					}
					else
					{
						studentPreferredFormat = studentSelectedFormat;
					}
					mediaContentDetailDTO.StudentPreferredFormat = studentPreferredFormat;
					mediaContentDetailDTO.MediaContentPerFormatId = 0;
					mediaContentDetailDTO.MediaContent = mediaContent2;
					mediaContentDetailDTO.MediaContentFormat = MediaContentFormat.UNSPECIFIED;
					mediaContentDetailDTO.IsANewUserCreatedMediaContent = studentRequestWebViewModel.MediaContent.IsANewUserCreatedMediaContent;
					mediaContentRequestedInfoDTO2.ContentDetailRequested = mediaContentDetailDTO;
				}
				MediaContentRequestedInfoDTO mediaContentRequestedInfoDTO3 = mediaContentRequestedInfoDTO;
				bool flag = mediaContentRequestedInfoDTO3 != null;
				if (flag)
				{
					list.Add(mediaContentRequestedInfoDTO3);
				}
			}
			StudentMediaRequestDTO studentMediaRequest = new StudentMediaRequestDTO
			{
				RequestMadeFromStudent = cart.Student,
				CreatedDatetime = DateTime.Now,
				ContentRequestedList = list
			};
			IStudentMediaRequestClientManager studentMediaRequestClientManager = new StudentMediaRequestClientManager();
			StudentMediaRequestDTO studentMediaRequestDTO = studentMediaRequestClientManager.CreateStudentMediaRequest(studentMediaRequest);
			List<MediaContentIdentifierDTO> list2;
			if (studentMediaRequestDTO.ContentRequestedList == null)
			{
				list2 = null;
			}
			else
			{
				list2 = (from r in studentMediaRequestDTO.ContentRequestedList
				where r.MediaContentRequestedInfoID > 0 && r.ContentDetailRequested.MediaContent != null
				select r into c
				select c.ContentDetailRequested.MediaContent.Identifier).ToList<MediaContentIdentifierDTO>();
			}
			List<MediaContentIdentifierDTO> list3 = list2;
			bool flag2 = list3 != null;
			if (flag2)
			{
				cart.RemoveAll(list3);
			}
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			string settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.ALTERNATEFORMAT_RequestsSubmittedThankYouText);
			string settingValue2 = webSettingsClientManager.GetSettingValue<string>(Setting.ALTERNATEFORMAT_RequestsSubmittedThankYouTitleText);
			string settingValue3 = webSettingsClientManager.GetSettingValue<string>(Setting.ALTERNATEFORMAT_RequestsSubmittedFailedText);
			PendingRequestsCartIndexViewModel pendingRequestsCartIndexViewModel = new PendingRequestsCartIndexViewModel();
			pendingRequestsCartIndexViewModel.Cart = cart;
			pendingRequestsCartIndexViewModel.PageTitle = settingValue2;
			string pageDescription;
			if (studentMediaRequestDTO.ContentRequestedList != null)
			{
				if (studentMediaRequestDTO.ContentRequestedList.Any((MediaContentRequestedInfoDTO r) => r.MediaContentRequestedInfoID == 0))
				{
					pageDescription = settingValue3;
					goto IL_355;
				}
			}
			pageDescription = settingValue;
			IL_355:
			pendingRequestsCartIndexViewModel.PageDescription = pageDescription;
			PendingRequestsCartIndexViewModel model = pendingRequestsCartIndexViewModel;
			return base.View("Index", model);
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0004A780 File Offset: 0x00048980
		public PartialViewResult PendingRequestsConfirmation(PendingRequestsCart cart, string returnUrl)
		{
			if (PendingRequestsCartController.<>o__3.<>p__0 == null)
			{
				PendingRequestsCartController.<>o__3.<>p__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ReturnUrl", typeof(PendingRequestsCartController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			PendingRequestsCartController.<>o__3.<>p__0.Target(PendingRequestsCartController.<>o__3.<>p__0, base.ViewBag, returnUrl);
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			if (PendingRequestsCartController.<>o__3.<>p__1 == null)
			{
				PendingRequestsCartController.<>o__3.<>p__1 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AllowStudentsToSelectPreferredFormat", typeof(PendingRequestsCartController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			PendingRequestsCartController.<>o__3.<>p__1.Target(PendingRequestsCartController.<>o__3.<>p__1, base.ViewBag, webSettingsClientManager.GetSettingValue<bool>(Setting.ALTERNATEFORMAT_AllowStudentsToSelectPreferredFormatTypeWhenSubmittingAltFormatRequest));
			return this.PartialView("PendingRequestsConfirmation", cart);
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0004A86C File Offset: 0x00048A6C
		[HttpPost]
		public JsonResult SetStudentPreferredFormat(PendingRequestsCart cart, string mediaContentId, MediaContentFormat format)
		{
			MediaContentIdentifierDTO id = new MediaContentIdentifierDTO(mediaContentId);
			bool flag = cart.Contains(id);
			JsonResult result;
			if (flag)
			{
				cart[id].StudentSelectedFormat = new MediaContentFormat?(format);
				result = base.Json(new
				{
					mediaContentId = mediaContentId,
					format = format,
					result = true
				});
			}
			else
			{
				result = base.Json(new
				{
					mediaContentId = mediaContentId,
					format = format,
					result = false
				});
			}
			return result;
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0004A8C4 File Offset: 0x00048AC4
		[HttpPost]
		public JsonResult UploadProofOfPurchaseById(PendingRequestsCart cart, string identifier)
		{
			bool flag = base.Request.Files.Count == 0;
			JsonResult result;
			if (flag)
			{
				result = this.ThrowJsonError("There is not file to upload", identifier);
			}
			else
			{
				HttpPostedFileBase httpPostedFileBase = base.Request.Files[0];
				bool flag2 = httpPostedFileBase == null || httpPostedFileBase.ContentLength == 0;
				if (flag2)
				{
					result = this.ThrowJsonError("Zero file size", identifier);
				}
				else
				{
					bool flag3 = !httpPostedFileBase.ValidateReceiptFormat();
					if (flag3)
					{
						result = this.ThrowJsonError(string.Format("File MIME content type '{0}' is not supported. Supported file types are: {1}", httpPostedFileBase.ContentType, HttpPostedFileBaseAdapter.ReceiptSupportedFiles.CommaSeparatedValues<string>()), identifier);
					}
					else
					{
						byte[] array = httpPostedFileBase.SaveAsBytes();
						bool flag4 = array != null;
						if (flag4)
						{
							MediaContentIdentifierDTO id = new MediaContentIdentifierDTO(identifier);
							StudentRequestWebViewModel studentRequestWebViewModel = cart[id];
							bool flag5 = studentRequestWebViewModel != null;
							if (flag5)
							{
								studentRequestWebViewModel.ProofOfPurchaseReceipt = array;
								studentRequestWebViewModel.Filename = httpPostedFileBase.FileName;
								studentRequestWebViewModel.Extension = Path.GetExtension(httpPostedFileBase.FileName);
							}
							result = base.Json(new
							{
								identifier = identifier,
								name = httpPostedFileBase.FileName,
								length = httpPostedFileBase.ContentLength,
								type = httpPostedFileBase.ContentType
							});
						}
						else
						{
							result = this.ThrowJsonError("Image is not in the correct format", identifier);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0004A9F8 File Offset: 0x00048BF8
		[HttpPost]
		public ActionResult AddContentToCart(PendingRequestsCart cart, MediaContentIdentifierDTO id)
		{
			bool flag = id != null;
			if (flag)
			{
				bool flag2 = cart.Contains(id);
				if (flag2)
				{
					CWLogger.Logger.Trace("AlternateFormat::PendingRequestsCartController::AddContentToCart: Media content uniqueid='{0}', isbn='{1}', externalid='{2}', externalsourceprovider='{3}' is already in the pending request cart by student '{4}'", new object[]
					{
						id.MediaContentUniqueId.GetValueOrDefault(),
						id.ISBN ?? "NULL",
						id.ExternalId ?? "NULL",
						id.ExternalSourceProvider ?? "NULL",
						cart.Student.PersonId
					});
					return this.ThrowJsonError("Media content is already in your pending request cart", id.ToString());
				}
				IStudentMediaRequestClientManager studentMediaRequestClientManager = new StudentMediaRequestClientManager();
				bool flag3 = studentMediaRequestClientManager.IsMediaContentAlreadyRequested(cart.Student.PersonId, id);
				if (flag3)
				{
					CWLogger.Logger.Warn("AlternateFormat::PendingRequestsCartController::AddContentToCart: Media content uniqueid='{0}', isbn='{1}', externalid='{2}', externalsourceprovider='{3}' was already requested by student '{4}'", new object[]
					{
						id.MediaContentUniqueId.GetValueOrDefault(),
						id.ISBN ?? "NULL",
						id.ExternalId ?? "NULL",
						id.ExternalSourceProvider ?? "NULL",
						cart.Student.PersonId
					});
					return this.ThrowJsonError("Media content already requested", id.ToString());
				}
				IMediaContentClientManager mediaContentClientManager = new MediaContentClientManager();
				MediaContentDTO mediaContentDTO = mediaContentClientManager.LoadMediaContentByIdentifier(id);
				bool flag4 = mediaContentDTO != null;
				if (!flag4)
				{
					CWLogger.Logger.Warn("AlternateFormat::PendingRequestsCartController::AddContentToCart: Media content uniqueid='{0}', isbn='{1}', externalid='{2}', externalsourceprovider='{3}' is not available for student '{4}'", new object[]
					{
						id.MediaContentUniqueId.GetValueOrDefault(),
						id.ISBN ?? "NULL",
						id.ExternalId ?? "NULL",
						id.ExternalSourceProvider ?? "NULL",
						cart.Student.PersonId
					});
					return this.ThrowJsonError("Media content is not currently available, please try again later", id.ToString());
				}
				int? selectedCourseId = base.TempData["SelectedCourseId"] as int?;
				cart.AddRequest(mediaContentDTO.ToWebView(), selectedCourseId);
			}
			return base.RedirectToAction("PendingRequestsSummary", cart);
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0004AC4C File Offset: 0x00048E4C
		public RedirectToRouteResult RemoveFromCart(PendingRequestsCart cart, MediaContentIdentifierDTO id)
		{
			cart.RemoveRequestById(id);
			return base.RedirectToAction("PendingRequestsSummary", cart);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0004AC74 File Offset: 0x00048E74
		public RedirectToRouteResult RemoveFromConfirmation(PendingRequestsCart cart, MediaContentIdentifierDTO id)
		{
			cart.RemoveRequestById(id);
			return base.RedirectToAction("PendingRequestsConfirmation", cart);
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0004AC9C File Offset: 0x00048E9C
		public RedirectToRouteResult RemoveAllFromCart(PendingRequestsCart cart)
		{
			cart.Clear();
			return base.RedirectToAction("PendingRequestsSummary", cart);
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0004ACC4 File Offset: 0x00048EC4
		public ActionResult RemoveAllFromConfirmation(PendingRequestsCart cart)
		{
			cart.Clear();
			return base.RedirectToAction("Index", cart);
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0004ACEC File Offset: 0x00048EEC
		public PartialViewResult PendingRequestsSummary(PendingRequestsCart cart, string msg = null)
		{
			if (PendingRequestsCartController.<>o__11.<>p__0 == null)
			{
				PendingRequestsCartController.<>o__11.<>p__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "MediaContentAddedMessage", typeof(PendingRequestsCartController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			PendingRequestsCartController.<>o__11.<>p__0.Target(PendingRequestsCartController.<>o__11.<>p__0, base.ViewBag, msg);
			return this.PartialView("PendingRequestsSummary", cart);
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0004AD68 File Offset: 0x00048F68
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
	}
}
