using System;
using System.Runtime.CompilerServices;
using System.Web.Mvc;
using Microsoft.CSharp.RuntimeBinder;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Infrastructure;
using TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Models.ConfidentialityAgreement;
using TechnoPro.ClockWorkWeb.Binders;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Core.ConfidentialityAgreement;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.ConfidentialityAgreement;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Controllers
{
	// Token: 0x0200018A RID: 394
	[NoCache]
	[ClockWorkRegisteredStudentRequired]
	[AlternateFormatAccommodationRequired]
	public class StudentConfidentialityAgreementController : Controller
	{
		// Token: 0x06000B9C RID: 2972 RVA: 0x0004ADB4 File Offset: 0x00048FB4
		public ActionResult Index([ModelBinder(typeof(LogonStudentModelBinder))] PersonBaseDTO student, string returnUrl)
		{
			if (StudentConfidentialityAgreementController.<>o__0.<>p__0 == null)
			{
				StudentConfidentialityAgreementController.<>o__0.<>p__0 = CallSite<Func<CallSite, object, eClockWorkWebMenu, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SelectedMenu", typeof(StudentConfidentialityAgreementController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			StudentConfidentialityAgreementController.<>o__0.<>p__0.Target(StudentConfidentialityAgreementController.<>o__0.<>p__0, base.ViewBag, eClockWorkWebMenu.AlternateFormat_Home);
			IStudentConfidentialityAgreementWebClientManager studentConfidentialityAgreementWebClientManager = new StudentConfidentialityAgreementWebClientManager(eClockWorkModules.Alternate_Format);
			string studentConfidentialityAgreementText = studentConfidentialityAgreementWebClientManager.GetStudentConfidentialityAgreementText(student.PersonId);
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			string settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.ALTERNATEFORMAT_StudentConfidentialityAgreementPageTitleText);
			string settingValue2 = webSettingsClientManager.GetSettingValue<string>(Setting.ALTERNATEFORMAT_StudentConfidentialityAgreementPageText);
			StudentConfidentialityAgreementViewModel model = new StudentConfidentialityAgreementViewModel
			{
				ConfidentialityAgreementText = studentConfidentialityAgreementText,
				Student = student,
				ReturnUrl = returnUrl,
				PageTitle = settingValue,
				PageDescription = settingValue2
			};
			return base.View("SignStudentConfidentialityAgreement", model);
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0004AE98 File Offset: 0x00049098
		[HttpPost]
		public ActionResult SignConfidentialityAgreement([ModelBinder(typeof(LogonStudentModelBinder))] PersonBaseDTO student, string returnUrl)
		{
			if (StudentConfidentialityAgreementController.<>o__1.<>p__0 == null)
			{
				StudentConfidentialityAgreementController.<>o__1.<>p__0 = CallSite<Func<CallSite, object, eClockWorkWebMenu, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SelectedMenu", typeof(StudentConfidentialityAgreementController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			StudentConfidentialityAgreementController.<>o__1.<>p__0.Target(StudentConfidentialityAgreementController.<>o__1.<>p__0, base.ViewBag, eClockWorkWebMenu.AlternateFormat_Home);
			IStudentConfidentialityAgreementWebClientManager studentConfidentialityAgreementWebClientManager = new StudentConfidentialityAgreementWebClientManager(eClockWorkModules.Alternate_Format);
			studentConfidentialityAgreementWebClientManager.RecordSignedConfidentialityAgreement(student.PersonId);
			string url = "";
			bool flag = !string.IsNullOrEmpty(returnUrl);
			if (flag)
			{
				url = base.Server.UrlDecode(returnUrl);
			}
			bool flag2 = base.Url.IsLocalUrl(url);
			ActionResult result;
			if (flag2)
			{
				result = this.Redirect(url);
			}
			else
			{
				result = base.RedirectToAction("Index", "AlternateFormatHome");
			}
			return result;
		}
	}
}
