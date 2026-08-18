using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPI;
using ClockWorkWebAPI.AuthenticationAuthorization;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync.Notetaking;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.ClientManager.Core.Notetaking;
using TechnoPro.Common.ClientManager.ICore.Notetaking;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Authentication;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Notetaking;
using TechnoPro.Common.UI.ClientManager.Web.Core.Notetaking;
using TechnoPro.Common.UI.Web.Entity.AuthenticationAuthorization;
using TechnoPro.Common.UI.Web.Entity.Notetaking;

namespace TechnoPro.ClockWorkWeb.user.NotetakingNotetakers
{
	// Token: 0x0200009E RID: 158
	public class user_NotetakingNotetakers_AddCourse : Page
	{
		// Token: 0x0600050B RID: 1291 RVA: 0x00024B78 File Offset: 0x00022D78
		protected void Page_Load(object sender, EventArgs e)
		{
			this.p_topmsg.Visible = false;
			int pid = this.GetPid();
			bool flag = pid <= 0;
			if (flag)
			{
				IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
				ClockWorkIdentity currentClockWorkIdentity_LoginIfNecessary = webAuthenticationAuthorizationWebClientManager.GetCurrentClockWorkIdentity_LoginIfNecessary(this.Page, ClockWorkWebAPI.AuthenticationAuthorization.GroupMembership.notetakers, false);
				bool flag2 = currentClockWorkIdentity_LoginIfNecessary != null;
				if (flag2)
				{
					base.Response.Redirect((currentClockWorkIdentity_LoginIfNecessary != null) ? "NotetakerAppNew.aspx" : "default.aspx", true);
				}
			}
			bool flag3 = !this.Page.IsPostBack;
			if (flag3)
			{
				DateTime now = DateTime.Now.Date;
				INotetakingClientManager notetakingClientManager = new NotetakingClientManager();
				List<LookupCourseBaseDTO> existingCourses = (from g in notetakingClientManager.LoadNotetakerAvailableCourses(pid, now, now.AddYears(1))
				where g.EndDate >= now
				select g).ToList<LookupCourseBaseDTO>();
				this.ShowDataSyncCoursesNotSelectedYet(existingCourses);
			}
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00024C60 File Offset: 0x00022E60
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetNotetakerId(this.Page);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00024C84 File Offset: 0x00022E84
		private void NoCoursesAreAvailable()
		{
			this.btn_newcourse.Enabled = false;
			this.btn_newcourseall.Enabled = false;
			this.lbl_topmsg.Text = "There are no courses available for you to add at this time.";
			this.p_topmsg.Visible = true;
			this.lbl_newcourses.Visible = false;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00024CD8 File Offset: 0x00022ED8
		private bool AreStringsEqual(string s1, string s2)
		{
			string text = (s1 ?? "").Trim();
			string value = (s2 ?? "").Trim();
			return text.Equals(value, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00024D14 File Offset: 0x00022F14
		private bool AreCoursesEqual(DataSyncExternalCourseDTO course1, LookupCourseBaseDTO course2)
		{
			bool flag = !this.AreStringsEqual(course1.Subject, (course2.Subject == null) ? "" : course2.Subject.SubjectDescription);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = !this.AreStringsEqual(course1.Course, course2.Course);
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = !this.AreStringsEqual(course1.Section, course2.Section);
					if (flag3)
					{
						result = false;
					}
					else
					{
						bool flag4 = !this.AreStringsEqual(course1.Campus, course2.Campus);
						if (flag4)
						{
							result = false;
						}
						else
						{
							bool flag5 = !this.AreStringsEqual(course1.TimeOfDay, course2.TimeOfDay);
							result = !flag5;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00024DD0 File Offset: 0x00022FD0
		private void ShowDataSyncCoursesNotSelectedYet(List<LookupCourseBaseDTO> existingCourses)
		{
			INotetakingClientDataSyncWebClientManager notetakingClientDataSyncWebClientManager = new NotetakingClientDataSyncWebClientManager();
			GetNotetakerInfoAndCoursesInfo getNotetakerInfoAndCoursesInfo;
			NotetakerWithExternalCoursesDTO notetakerAndCourseInfo = notetakingClientDataSyncWebClientManager.GetNotetakerAndCourseInfo(false, this.Page, out getNotetakerInfoAndCoursesInfo);
			IList<DataSyncExternalCourseDTO> list = (notetakerAndCourseInfo == null || notetakerAndCourseInfo.ExternalCourses == null) ? null : notetakerAndCourseInfo.ExternalCourses;
			bool flag = list == null || list.Count < 1;
			if (flag)
			{
				this.NoCoursesAreAvailable();
			}
			else
			{
				List<DataSyncExternalCourseDTO> list2 = (from g in list
				where !existingCourses.Any((LookupCourseBaseDTO h) => this.AreCoursesEqual(g, h))
				select g).ToList<DataSyncExternalCourseDTO>();
				this.chks_courses.Items.Clear();
				bool flag2 = list2.Count < 1;
				if (flag2)
				{
					this.NoCoursesAreAvailable();
				}
				else
				{
					List<string> list3 = new List<string>();
					DateTime date = DateTime.Now.Date;
					foreach (DataSyncExternalCourseDTO dataSyncExternalCourseDTO in list2)
					{
						bool flag3 = dataSyncExternalCourseDTO.EndDate >= date;
						if (flag3)
						{
							string externalCourseUniqueId = this.GetExternalCourseUniqueId(dataSyncExternalCourseDTO);
							bool flag4 = externalCourseUniqueId.Length <= 0 || list3.Contains(externalCourseUniqueId);
							if (!flag4)
							{
								ListItem item = new ListItem(externalCourseUniqueId, externalCourseUniqueId);
								this.chks_courses.Items.Add(item);
								list3.Add(externalCourseUniqueId);
							}
						}
					}
					bool flag5 = this.chks_courses.Items.Count < 1;
					if (flag5)
					{
						this.NoCoursesAreAvailable();
					}
				}
			}
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00024F68 File Offset: 0x00023168
		private string GetExternalCourseUniqueId(DataSyncExternalCourseDTO course)
		{
			bool flag = course != null;
			string result;
			if (flag)
			{
				result = string.Concat(new string[]
				{
					course.Subject,
					" ",
					course.Course,
					" ",
					course.Section,
					" ",
					course.TimeOfDay
				});
			}
			else
			{
				result = "?";
			}
			return result;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00024FD4 File Offset: 0x000231D4
		protected void btn_newcourseall_Click(object sender, EventArgs e)
		{
			int pid = this.GetPid();
			int num = Notetakerb.CreateServiceProviderApplication(pid, 128);
			INotetakingClientDataSyncWebClientManager notetakingClientDataSyncWebClientManager = new NotetakingClientDataSyncWebClientManager();
			GetNotetakerInfoAndCoursesInfo getNotetakerInfoAndCoursesInfo;
			NotetakerWithExternalCoursesDTO notetakerAndCourseInfo = notetakingClientDataSyncWebClientManager.GetNotetakerAndCourseInfo(false, this.Page, out getNotetakerInfoAndCoursesInfo);
			IList<DataSyncExternalCourseDTO> extCourses = (notetakerAndCourseInfo == null || notetakerAndCourseInfo.ExternalCourses == null) ? null : notetakerAndCourseInfo.ExternalCourses;
			List<string> list = new List<string>();
			foreach (object obj in this.chks_courses.Items)
			{
				ListItem listItem = (ListItem)obj;
				list.Add(listItem.Value);
			}
			this.NewCourses(pid, list, extCourses);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0002509C File Offset: 0x0002329C
		private void NewCourses(int nid, IList<string> courseVals, IList<DataSyncExternalCourseDTO> extCourses)
		{
			bool flag = extCourses == null;
			if (!flag)
			{
				List<DataSyncExternalCourseDTO> list = new List<DataSyncExternalCourseDTO>();
				foreach (DataSyncExternalCourseDTO dataSyncExternalCourseDTO in extCourses)
				{
					string externalCourseUniqueId = this.GetExternalCourseUniqueId(dataSyncExternalCourseDTO);
					bool flag2 = courseVals.Contains(externalCourseUniqueId);
					if (flag2)
					{
						list.Add(dataSyncExternalCourseDTO);
					}
				}
				bool flag3 = list.Count < 1;
				if (!flag3)
				{
					INotetakingClientManager notetakingClientManager = new NotetakingClientManager();
					notetakingClientManager.AddPotentialCoursesForNotetaker(nid, list);
					base.Response.Redirect("notetakerapp.aspx", true);
				}
			}
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0002514C File Offset: 0x0002334C
		protected void btn_newcourse_Click(object sender, EventArgs e)
		{
			int pid = this.GetPid();
			int num = Notetakerb.CreateServiceProviderApplication(pid, 128);
			INotetakingClientDataSyncWebClientManager notetakingClientDataSyncWebClientManager = new NotetakingClientDataSyncWebClientManager();
			GetNotetakerInfoAndCoursesInfo getNotetakerInfoAndCoursesInfo;
			NotetakerWithExternalCoursesDTO notetakerAndCourseInfo = notetakingClientDataSyncWebClientManager.GetNotetakerAndCourseInfo(false, this.Page, out getNotetakerInfoAndCoursesInfo);
			IList<DataSyncExternalCourseDTO> extCourses = (notetakerAndCourseInfo == null || notetakerAndCourseInfo.ExternalCourses == null) ? null : notetakerAndCourseInfo.ExternalCourses;
			List<string> list = new List<string>();
			foreach (object obj in this.chks_courses.Items)
			{
				ListItem listItem = (ListItem)obj;
				bool selected = listItem.Selected;
				if (selected)
				{
					list.Add(listItem.Value);
				}
			}
			this.NewCourses(pid, list, extCourses);
		}

		// Token: 0x040002EF RID: 751
		protected ScriptManager bbb;

		// Token: 0x040002F0 RID: 752
		protected Label lblTitle;

		// Token: 0x040002F1 RID: 753
		protected Panel p_topmsg;

		// Token: 0x040002F2 RID: 754
		protected Image img_topmsg;

		// Token: 0x040002F3 RID: 755
		protected Label lbl_topmsg;

		// Token: 0x040002F4 RID: 756
		protected Panel p_newcourses;

		// Token: 0x040002F5 RID: 757
		protected Label lbl_newcourses;

		// Token: 0x040002F6 RID: 758
		protected CheckBoxList chks_courses;

		// Token: 0x040002F7 RID: 759
		protected Button btn_cancel;

		// Token: 0x040002F8 RID: 760
		protected Button btn_newcourseall;

		// Token: 0x040002F9 RID: 761
		protected Button btn_newcourse;
	}
}
