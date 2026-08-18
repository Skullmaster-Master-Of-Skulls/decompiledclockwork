using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.ClockWorkWeb.Areas.Loa.Models;
using TechnoPro.Common.ClientManager.Core.MailMerging;
using TechnoPro.Common.ClientManager.Core.StudentAccommodationRequests;
using TechnoPro.Common.ClientManager.ICore.MailMerging;
using TechnoPro.Common.ClientManager.ICore.StudentAccommodationRequests;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.Web;

namespace TechnoPro.ClockWorkWeb.user.misc
{
	// Token: 0x020000C4 RID: 196
	public class StudentLetters : Page
	{
		// Token: 0x060005CC RID: 1484 RVA: 0x0002A9D0 File Offset: 0x00028BD0
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				string text = base.Request.QueryString["hashstr"];
				string text2 = base.Request.QueryString["plainstr"];
				string text3 = base.Request.QueryString["status"] ?? "";
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
						PersonIdHash = HttpUtility.UrlEncode(text),
						PersonIdHashPlain = HttpUtility.UrlEncode(text2)
					};
				}
				catch (Exception ex)
				{
					model = new StudentCourseForLogicEmailRulesViewModel
					{
						AllowedCourses = new List<CourseRegistrationDTO>(),
						Student = null
					};
					CWLogger.Logger.Error("ClockWorkWeb.user.misc:StudentLetters:err={0}", ex.ToString());
				}
				StudentCourseForLogicEmailRulesViewModel model2 = model;
				List<StudentLetters.AllowedCourse> list;
				if (model2 == null)
				{
					list = null;
				}
				else
				{
					IList<CourseRegistrationDTO> allowedCourses = model2.AllowedCourses;
					list = ((allowedCourses != null) ? (from g in allowedCourses
					select new StudentLetters.AllowedCourse
					{
						CourseDescription = g.Course.GetCourseDescription(),
						LuCourseId = g.Course.LuCourseId,
						DownloadArgs = string.Format("{0}~{1}~{2}", model.PersonIdHash, model.PersonIdHashPlain, NavigatorClientManager.CurrentInstance.ConvertIntParameterToUrlString(g.Course.LuCourseId))
					}).ToList<StudentLetters.AllowedCourse>() : null);
				}
				List<StudentLetters.AllowedCourse> dataSource = list ?? new List<StudentLetters.AllowedCourse>();
				this.courseList.DataSource = dataSource;
				this.courseList.DataBind();
				this.btn_downloadAll.CommandArgument = string.Format("{0}~{1}", model.PersonIdHash, model.PersonIdHashPlain);
				Label label = this.lbl_title;
				PersonBaseDTO student = model.Student;
				label.Text = (((student != null) ? student.GetStudentName() : null) ?? "");
			}
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0002ABF8 File Offset: 0x00028DF8
		protected void courseListItemCommand(object sender, CommandEventArgs e)
		{
			bool flag = e.CommandName == "download";
			if (flag)
			{
				string[] array = e.CommandArgument.ToString().Split(new char[]
				{
					'~'
				}).ToArray<string>();
				string str = (array.Length != 0) ? array[0] : "";
				string str2 = (array.Length > 1) ? array[1] : "";
				string urlParameter = (array.Length > 2) ? array[2] : "";
				int lucid = NavigatorClientManager.CurrentInstance.ConvertUrlStringToIntParameter(urlParameter);
				try
				{
					string @string = Encoding.UTF8.GetString(Convert.FromBase64String(HttpUtility.UrlDecode(str)));
					string string2 = Encoding.UTF8.GetString(Convert.FromBase64String(HttpUtility.UrlDecode(str2)));
					ISelfRegClientManager selfRegClientManager = new SelfRegClientManager();
					AllowedStudentCourseRegistrationsForCustomEmailLogicDTO coursesAllowedBySelfRegCustomLogicRulesToViewLoaFor = selfRegClientManager.GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaFor(@string, string2);
					bool flag2 = lucid > 0 && coursesAllowedBySelfRegCustomLogicRulesToViewLoaFor.CourseRegistrations.Any((CourseRegistrationDTO g) => g.Course.LuCourseId == lucid);
					if (flag2)
					{
						IMailMergingDocClientManager mailMergingDocClientManager = new MailMergingDocClientManager();
						IMailMergingDocClientManager mailMergingDocClientManager2 = mailMergingDocClientManager;
						PersonBaseDTO student = coursesAllowedBySelfRegCustomLogicRulesToViewLoaFor.Student;
						BinaryFileDTO binaryFileDTO = mailMergingDocClientManager2.GenerateAccommodationLetterForExternalLogicRulesUser((student != null) ? student.PersonId : 0, lucid);
						IWebFileClientManager webFileClientManager = new WebFileClientManager();
						webFileClientManager.DownloadFile(binaryFileDTO.FileName, binaryFileDTO.ByteArray);
					}
				}
				catch (Exception ex)
				{
					CWLogger.Logger.Error("ClockWorkWeb.user.misc:StudentLetters:courseListItemCommand:err={0}", ex.ToString());
				}
			}
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0002AD74 File Offset: 0x00028F74
		protected void btn_downloadAll_OnClick(object sender, EventArgs ea)
		{
			LinkButton linkButton = (LinkButton)sender;
			string[] array = linkButton.CommandArgument.ToString().Split(new char[]
			{
				'~'
			}).ToArray<string>();
			string str = (array.Length != 0) ? array[0] : "";
			string str2 = (array.Length > 1) ? array[1] : "";
			try
			{
				string @string = Encoding.UTF8.GetString(Convert.FromBase64String(HttpUtility.UrlDecode(str)));
				string string2 = Encoding.UTF8.GetString(Convert.FromBase64String(HttpUtility.UrlDecode(str2)));
				ISelfRegClientManager selfRegClientManager = new SelfRegClientManager();
				AllowedStudentCourseRegistrationsForCustomEmailLogicDTO coursesAllowedBySelfRegCustomLogicRulesToViewLoaFor = selfRegClientManager.GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaFor(@string, string2);
				bool flag = coursesAllowedBySelfRegCustomLogicRulesToViewLoaFor.CourseRegistrations.Count > 0;
				if (flag)
				{
					PersonBaseDTO student = coursesAllowedBySelfRegCustomLogicRulesToViewLoaFor.Student;
					int studentPersonId = (student != null) ? student.PersonId : 0;
					IMailMergingDocClientManager mailMergingDocClientManager = new MailMergingDocClientManager();
					byte[] bytes = null;
					string filename = "";
					MemoryStream memoryStream = null;
					try
					{
						memoryStream = new MemoryStream();
						filename = mailMergingDocClientManager.GenerateAllAccommodationLettersForExternalLogicRulesUser(memoryStream, studentPersonId);
						bytes = memoryStream.ToArray();
					}
					finally
					{
						bool flag2 = memoryStream != null;
						if (flag2)
						{
							memoryStream.Dispose();
						}
					}
					IWebFileClientManager webFileClientManager = new WebFileClientManager();
					webFileClientManager.DownloadFile(filename, bytes);
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("ClockWorkWeb.user.misc:StudentLetters:courseListItemCommand:err={0}", ex.ToString());
			}
		}

		// Token: 0x04000419 RID: 1049
		protected Label lbl_title;

		// Token: 0x0400041A RID: 1050
		protected Repeater courseList;

		// Token: 0x0400041B RID: 1051
		protected LinkButton btn_downloadAll;

		// Token: 0x020001FD RID: 509
		internal class AllowedCourse
		{
			// Token: 0x17000308 RID: 776
			// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x0004F4AD File Offset: 0x0004D6AD
			// (set) Token: 0x06000DA8 RID: 3496 RVA: 0x0004F4B5 File Offset: 0x0004D6B5
			public string CourseDescription { get; set; }

			// Token: 0x17000309 RID: 777
			// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x0004F4BE File Offset: 0x0004D6BE
			// (set) Token: 0x06000DAA RID: 3498 RVA: 0x0004F4C6 File Offset: 0x0004D6C6
			public int LuCourseId { get; set; }

			// Token: 0x1700030A RID: 778
			// (get) Token: 0x06000DAB RID: 3499 RVA: 0x0004F4CF File Offset: 0x0004D6CF
			// (set) Token: 0x06000DAC RID: 3500 RVA: 0x0004F4D7 File Offset: 0x0004D6D7
			public string DownloadArgs { get; set; }
		}
	}
}
