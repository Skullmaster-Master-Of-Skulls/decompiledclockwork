using System;
using System.Web.Mvc;
using TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Infrastructure;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Controllers
{
	// Token: 0x02000188 RID: 392
	[NoCache]
	public class MessageHandlerController : Controller
	{
		// Token: 0x06000B8A RID: 2954 RVA: 0x0004A108 File Offset: 0x00048308
		public ActionResult NotRegisteredClockWorkStudent()
		{
			base.TempData["message"] = "You are not registered with us at the current time.  Please contact us for more information about registration.";
			return base.RedirectToAction("Index", "AlternateFormatHome");
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0004A140 File Offset: 0x00048340
		public ActionResult StudentWithoutAlternateFormatAccommodations()
		{
			base.TempData["message"] = "You are not authorized to use the Alternate Format system at this time.  Please contact us for more information.";
			return base.RedirectToAction("Index", "AlternateFormatHome");
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0004A178 File Offset: 0x00048378
		public ActionResult NotLicenseModule(Group group)
		{
			base.TempData["message"] = string.Format("{0} module does not have a valid license.  Please contact us for more information.", group);
			return base.RedirectToAction("Index", "AlternateFormatHome");
		}
	}
}
