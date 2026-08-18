using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.Authentication;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.Core.Authentication;
using TechnoPro.Common.ClientManager.Core.LookupCourses;
using TechnoPro.Common.ClientManager.Core.Notetaking;
using TechnoPro.Common.ClientManager.Core.People;
using TechnoPro.Common.ClientManager.ICore.Authentication;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;
using TechnoPro.Common.ClientManager.ICore.Notetaking;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Authentication;
using TechnoPro.Common.UI.Web.Entity;
using TechnoPro.Common.UI.Web.Entity.AuthenticationAuthorization;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.staff.schedule
{
	// Token: 0x02000106 RID: 262
	public class staff_schedule_LoginOptions : Page
	{
		// Token: 0x060007B7 RID: 1975 RVA: 0x000392EC File Offset: 0x000374EC
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = base.Master != null && base.Master is IClockWorkMasterPage;
			if (flag)
			{
				((IClockWorkMasterPage)base.Master).SetCurrentPage(eClockWorkWebPage.Staff_LoginAsAnotherUser);
			}
			this.CheckUserIsAllowedToBeHere();
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00039334 File Offset: 0x00037534
		private int CheckUserIsAllowedToBeHere()
		{
			IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
			ClockWorkIdentity currentClockWorkIdentity = webAuthenticationAuthorizationWebClientManager.GetCurrentClockWorkIdentity(this.Page);
			int num = (currentClockWorkIdentity == null) ? 0 : currentClockWorkIdentity.PersonId;
			bool flag = num < 1;
			if (flag)
			{
				CWLogger.Logger.Warn("Staff:LoginOptions:Can't access staff page because pid is zero");
				base.Response.Redirect("~/custom/misc/home.aspx?msg=CantAccessStaffPageBecausePidIsZero", true);
			}
			else
			{
				IClockWorkAuthenticationClientManager clockWorkAuthenticationClientManager = new ClockWorkAuthenticationClientManager();
				bool flag2 = clockWorkAuthenticationClientManager.IsUserAdminOrInSettingsListOfStaffPidsAllowedToLoginAsAnother(num);
				bool flag3 = !flag2;
				if (!flag3)
				{
					return num;
				}
				CWLogger.Logger.Warn("Staff:LoginOptions:Not admin or in settings list of pids:pid={0}", num.ToString());
				base.Response.Redirect("~/user/misc/notallowed.aspx?code=notadminorinsettingslistofpids&step=1.3", true);
			}
			return 0;
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x000393E7 File Offset: 0x000375E7
		private static void ResetIdentity(ClockWorkIdentity identity, string username, string snum, int pid, int iid, int nid)
		{
			staff_schedule_LoginOptions.ResetIdentity(identity, username, snum, pid, iid, nid, -1);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x000393FC File Offset: 0x000375FC
		private static void ResetIdentity(ClockWorkIdentity identity, string username, string snum, int pid, int iid, int nid, int altContactId)
		{
			identity.UserName = username;
			identity.StudentNumber = snum;
			identity.PersonId = pid;
			identity.NotetakerId = nid;
			identity.InstructorId = iid;
			identity.AlternateContactId = altContactId;
			IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
			webAuthenticationAuthorizationWebClientManager.SetCurrentClockWorkIdentity(identity);
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0003944C File Offset: 0x0003764C
		protected void btn_login_Click(object sender, EventArgs e)
		{
			CWLogger.Logger.Warn("LoginOptions:PreCheckUserIsAllowed");
			int num = this.CheckUserIsAllowedToBeHere();
			bool flag = num < 1;
			if (!flag)
			{
				CWLogger.Logger.Warn("LoginOptions:PostCheckUserIsAllowed:pid={0}", num.ToString());
				IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
				string text = this.txt_cwUsername.Text;
				string text2 = this.txt_cwPassword.Text;
				ClockWorkIdentity currentClockWorkIdentity = webAuthenticationAuthorizationWebClientManager.GetCurrentClockWorkIdentity(this.Page);
				int num2 = 0;
				try
				{
					AuthenticationAndAuthorizationResultDTO authenticationAndAuthorizationResultDTO = webAuthenticationAuthorizationWebClientManager.TryToAuthenticateStaff(text, text2, new AuthenticationArgsDTO(), true);
					num2 = ((authenticationAndAuthorizationResultDTO != null && authenticationAndAuthorizationResultDTO.PassedAuthentication && authenticationAndAuthorizationResultDTO.ClockWorkUser != null) ? authenticationAndAuthorizationResultDTO.ClockWorkUser.ClockWorkPid : 0);
				}
				finally
				{
					webAuthenticationAuthorizationWebClientManager.SetCurrentClockWorkIdentity(currentClockWorkIdentity);
				}
				bool flag2 = num2 != num;
				if (flag2)
				{
					CWLogger.Logger.Warn("LoginOptions:Failed:username={0}:pidCheck={1}:pid={2}", text ?? "NULL", num2.ToString(), num.ToString());
					this.ShowMessage("Login failed. Please ensure that your password was entered correctly, and that you are using the same *ClockWork* username and password you are logged in as.");
				}
				else
				{
					string text3 = this.txt_snum.Text;
					ClockWorkIdentity currentClockWorkIdentity2 = webAuthenticationAuthorizationWebClientManager.GetCurrentClockWorkIdentity(this.Page);
					string selectedValue = this.rb_loginAs.SelectedValue;
					if (!(selectedValue == "student"))
					{
						if (!(selectedValue == "notetaker"))
						{
							if (!(selectedValue == "instructor"))
							{
								this.ShowMessage("Please select a valid option from the 'Log in as:' list");
							}
							else
							{
								try
								{
									ILookupInstructorClientManager lookupInstructorClientManager = new LookupInstructorClientManager();
									LookupInstructorDTO lookupInstructorDTO;
									if ((lookupInstructorDTO = lookupInstructorClientManager.LoadInstructorByUsername(text3)) == null)
									{
										lookupInstructorDTO = (lookupInstructorClientManager.LoadInstructorByEmail(text3) ?? lookupInstructorClientManager.LoadInstructorByEmployeeId(text3));
									}
									LookupInstructorDTO lookupInstructorDTO2 = lookupInstructorDTO;
									IAlternateContactClientManager alternateContactClientManager = new AlternateContactClientManager();
									AlternateContactDTO alternateContactDTO = alternateContactClientManager.LoadAlternateContactByUsername(text3) ?? alternateContactClientManager.LoadAlternateContactByEmployeeId(text3);
									bool flag3 = lookupInstructorDTO2 != null || alternateContactDTO != null;
									if (flag3)
									{
										staff_schedule_LoginOptions.ResetIdentity(currentClockWorkIdentity2, text3, text3, -1, (lookupInstructorDTO2 != null) ? lookupInstructorDTO2.InstructorId : -1, -1, (alternateContactDTO != null) ? alternateContactDTO.AlternateContactId : -1);
										base.Response.Redirect("~/user/instructor/default.aspx", true);
									}
									else
									{
										this.ShowMessage("Instructor not found by username or email; alt contact not found by username or employeeid");
									}
								}
								catch (Exception ex)
								{
									this.ShowMessage("An error has occurred: " + ex.Message);
								}
							}
						}
						else
						{
							INotetakingClientManager notetakingClientManager = new NotetakingClientManager();
							NotetakerBaseDTO notetakerBaseDTO = notetakingClientManager.LoadNotetakerBaseByUsername(text3);
							bool flag4 = notetakerBaseDTO != null && notetakerBaseDTO.ServiceProviderId > 0;
							if (flag4)
							{
								staff_schedule_LoginOptions.ResetIdentity(currentClockWorkIdentity2, text3, "", -1, -1, notetakerBaseDTO.ServiceProviderId);
								base.Response.Redirect("~/user/notetakingnotetakers/default.aspx", true);
							}
							else
							{
								this.ShowMessage("Notetaker not found by username.");
							}
						}
					}
					else
					{
						IPeopleClientManager peopleClientManager = new PeopleClientManager();
						PersonBaseDTO personBaseDTO = peopleClientManager.LoadPersonByStudentNumber(text3, false);
						bool flag5 = personBaseDTO != null && personBaseDTO.PersonId > 0 && personBaseDTO.CoreGroup == eCoreGroupDTO.Students;
						if (flag5)
						{
							staff_schedule_LoginOptions.ResetIdentity(currentClockWorkIdentity2, text3, text3, personBaseDTO.PersonId, -1, -1);
							base.Response.Redirect("~/custom/misc/home.aspx", true);
						}
						else
						{
							this.ShowMessage("Student not found by student number.");
						}
					}
				}
			}
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00039784 File Offset: 0x00037984
		private void ShowMessage(string msg)
		{
			this.lbl_msg.Text = msg;
			this.p_msg.Visible = true;
		}

		// Token: 0x040005EC RID: 1516
		protected ScriptManager bbb;

		// Token: 0x040005ED RID: 1517
		protected RadScriptBlock RadScriptBlock1;

		// Token: 0x040005EE RID: 1518
		protected Panel p_msg;

		// Token: 0x040005EF RID: 1519
		protected Label lbl_msg;

		// Token: 0x040005F0 RID: 1520
		protected Panel p_main;

		// Token: 0x040005F1 RID: 1521
		protected Label lbl_loginAs;

		// Token: 0x040005F2 RID: 1522
		protected RadioButtonList rb_loginAs;

		// Token: 0x040005F3 RID: 1523
		protected TextBox txt_snum;

		// Token: 0x040005F4 RID: 1524
		protected Label lbl_cwUsername;

		// Token: 0x040005F5 RID: 1525
		protected TextBox txt_cwUsername;

		// Token: 0x040005F6 RID: 1526
		protected Label lbl_cwPassword;

		// Token: 0x040005F7 RID: 1527
		protected TextBox txt_cwPassword;

		// Token: 0x040005F8 RID: 1528
		protected Button btn_login;
	}
}
