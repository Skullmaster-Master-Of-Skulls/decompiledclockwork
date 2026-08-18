using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ClockWorkLogger;
using Microsoft.CSharp.RuntimeBinder;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.ClockWorkWeb.Areas.Loa.Models;
using TechnoPro.ClockWorkWeb.Binders;
using TechnoPro.Common.ClientManager.Core.MailMerging;
using TechnoPro.Common.ClientManager.Core.StudentAccommodationRequests;
using TechnoPro.Common.ClientManager.ICore.MailMerging;
using TechnoPro.Common.ClientManager.ICore.StudentAccommodationRequests;

namespace TechnoPro.ClockWorkWeb.Areas.Loa.Controllers
{
	// Token: 0x02000161 RID: 353
	public class LoaLogicController : Controller
	{
		// Token: 0x06000AA9 RID: 2729 RVA: 0x00048CF8 File Offset: 0x00046EF8
		public ActionResult StudentLetters()
		{
			string text = base.Request.QueryString["hashstr"];
			string text2 = base.Request.QueryString["plainstr"];
			string text3 = base.Request.QueryString["status"] ?? "";
			bool flag = text.Length > 1;
			if (flag)
			{
				text = text.Substring(0, text.Length - 1);
			}
			bool flag2 = text2.Length > 1;
			if (flag2)
			{
				text2 = text2.Substring(0, text2.Length - 1);
			}
			CWLogger.Logger.Debug("LoaLogicController:StudentLetters:h={0}:p={1}", text ?? "NULL", text2 ?? "NULL");
			StudentCourseForLogicEmailRulesViewModel model = null;
			try
			{
				string @string = Encoding.UTF8.GetString(Convert.FromBase64String(text));
				string string2 = Encoding.UTF8.GetString(Convert.FromBase64String(text2));
				ISelfRegClientManager selfRegClientManager = new SelfRegClientManager();
				AllowedStudentCourseRegistrationsForCustomEmailLogicDTO coursesAllowedBySelfRegCustomLogicRulesToViewLoaFor = selfRegClientManager.GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaFor(@string, string2);
				model = new StudentCourseForLogicEmailRulesViewModel
				{
					AllowedCourses = ((coursesAllowedBySelfRegCustomLogicRulesToViewLoaFor != null) ? coursesAllowedBySelfRegCustomLogicRulesToViewLoaFor.CourseRegistrations : null),
					Student = ((coursesAllowedBySelfRegCustomLogicRulesToViewLoaFor != null) ? coursesAllowedBySelfRegCustomLogicRulesToViewLoaFor.Student : null),
					PersonIdHash = text,
					PersonIdHashPlain = text2
				};
				if (LoaLogicController.<>o__0.<>p__0 == null)
				{
					LoaLogicController.<>o__0.<>p__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "status", typeof(LoaLogicController), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				LoaLogicController.<>o__0.<>p__0.Target(LoaLogicController.<>o__0.<>p__0, base.ViewBag, text3 ?? "");
			}
			catch (Exception ex)
			{
				model = new StudentCourseForLogicEmailRulesViewModel
				{
					AllowedCourses = new List<CourseRegistrationDTO>(),
					Student = null
				};
				CWLogger.Logger.Error("LoaLogicController:StudentLetters:err={0}", ex.ToString());
			}
			return base.View(model);
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00048EEC File Offset: 0x000470EC
		public async Task<ActionResult> DownloadLetter([ModelBinder(typeof(LogonStudentModelBinder))] PersonBaseDTO loggedInUser, int lucourseid, int personid, string personidhash, string personidhashplain)
		{
			IMailMergingDocClientManager mailMergingDocClientManager = new MailMergingDocClientManager();
			BinaryFileDTO document = mailMergingDocClientManager.GenerateAccommodationLetterForExternalLogicRulesUser(personid, lucourseid);
			BinaryFileDTO binaryFileDTO = document;
			bool flag = ((binaryFileDTO != null) ? binaryFileDTO.ByteArray : null) == null;
			ActionResult result;
			if (flag)
			{
				result = this.RedirectToAction("StudentLetters", new
				{
					hashstr = personidhash,
					plainstr = personidhashplain,
					status = "failed"
				});
			}
			else
			{
				result = this.File(document.ByteArray, "application/octet-stream", document.FileName);
			}
			return result;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00048F58 File Offset: 0x00047158
		public async Task DownloadLetters([ModelBinder(typeof(LogonStudentModelBinder))] PersonBaseDTO loggedInUser, int personid, string personidhash, string personidhashplain)
		{
			IMailMergingDocClientManager mailMergingDocClientManager = new MailMergingDocClientManager();
			string filename = mailMergingDocClientManager.GenerateAllAccommodationLettersForExternalLogicRulesUser(this.Response.OutputStream, personid);
			this.Response.AddHeader("content-disposition", string.Format("attachment;filename=\"{0}\"", filename ?? "file.zip"));
			this.Response.ContentType = "application/x-compressed";
		}
	}
}
