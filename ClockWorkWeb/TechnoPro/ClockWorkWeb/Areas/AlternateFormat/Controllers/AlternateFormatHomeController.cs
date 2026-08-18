using System;
using System.Runtime.CompilerServices;
using System.Web.Mvc;
using Microsoft.CSharp.RuntimeBinder;
using TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Infrastructure;
using TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Models;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Controllers
{
	// Token: 0x02000186 RID: 390
	[NoCache]
	[AllowAnonymous]
	public class AlternateFormatHomeController : Controller
	{
		// Token: 0x06000B80 RID: 2944 RVA: 0x00049AC4 File Offset: 0x00047CC4
		public ActionResult Index()
		{
			return base.RedirectToAction("Home");
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00049AE4 File Offset: 0x00047CE4
		public ActionResult Home()
		{
			if (AlternateFormatHomeController.<>o__1.<>p__0 == null)
			{
				AlternateFormatHomeController.<>o__1.<>p__0 = CallSite<Func<CallSite, object, eClockWorkWebMenu, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SelectedMenu", typeof(AlternateFormatHomeController), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			AlternateFormatHomeController.<>o__1.<>p__0.Target(AlternateFormatHomeController.<>o__1.<>p__0, base.ViewBag, eClockWorkWebMenu.AlternateFormat_Home);
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			string settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.ALTERNATEFORMAT_WelcomePageTitleText);
			string settingValue2 = webSettingsClientManager.GetSettingValue<string>(Setting.ALTERNATEFORMAT_WelcomePageText);
			string errorMessage = (string)base.TempData["message"];
			return base.View(new AlternateFormatHomeViewModel
			{
				PageTitle = settingValue,
				PageDescription = settingValue2,
				ErrorMessage = errorMessage
			});
		}
	}
}
