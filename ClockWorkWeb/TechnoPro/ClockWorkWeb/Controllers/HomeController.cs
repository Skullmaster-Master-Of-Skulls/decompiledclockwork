using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TechnoPro.ClockWorkWeb.Areas.AlternateFormat.Infrastructure;

namespace TechnoPro.ClockWorkWeb.Controllers
{
	// Token: 0x02000156 RID: 342
	[NoCache]
	public class HomeController : Controller
	{
		// Token: 0x06000A81 RID: 2689 RVA: 0x000486AC File Offset: 0x000468AC
		[AllowAnonymous]
		public ActionResult Index()
		{
			return this.Redirect("~/custom/misc/home.aspx");
		}

		// Token: 0x04000807 RID: 2055
		private readonly IList<ClockWorkLinkDisplayInfo> clockworkLinkList = new List<ClockWorkLinkDisplayInfo>
		{
			new ClockWorkLinkDisplayInfo
			{
				Title = "Tutoring (tutors)",
				IconClass = "fa fa-user-plus fa-5x",
				TargetUrl = "~/user/TutoringTutors/",
				GroupName = "Students",
				OrderNum = 1
			},
			new ClockWorkLinkDisplayInfo
			{
				Title = "Tutoring (tutors)",
				IconClass = "fa fa-user-plus fa-5x",
				TargetUrl = "~/user/TutoringTutors/",
				GroupName = "Students",
				OrderNum = 2
			},
			new ClockWorkLinkDisplayInfo
			{
				Title = "Tutoring (tutors)",
				IconClass = "fa fa-user-plus fa-5x",
				TargetUrl = "~/user/TutoringTutors/",
				GroupName = "Students",
				OrderNum = 3
			},
			new ClockWorkLinkDisplayInfo
			{
				Title = "Tutoring (tutors)",
				IconClass = "fa fa-user-plus fa-5x",
				TargetUrl = "~/user/TutoringTutors/",
				GroupName = "Students",
				OrderNum = 4
			},
			new ClockWorkLinkDisplayInfo
			{
				Title = "Instructor access",
				IconClass = "fa fa-user-plus fa-5x",
				TargetUrl = "~/user/TutoringTutors/",
				GroupName = "Instructors",
				OrderNum = 1
			},
			new ClockWorkLinkDisplayInfo
			{
				Title = "Instructor access",
				IconClass = "fa fa-user-plus fa-5x",
				TargetUrl = "~/user/TutoringTutors/",
				GroupName = "Instructors",
				OrderNum = 2
			},
			new ClockWorkLinkDisplayInfo
			{
				Title = "Instructor access",
				IconClass = "fa fa-user-plus fa-5x",
				TargetUrl = "~/user/TutoringTutors/",
				GroupName = "Instructors",
				OrderNum = 3
			}
		};
	}
}
