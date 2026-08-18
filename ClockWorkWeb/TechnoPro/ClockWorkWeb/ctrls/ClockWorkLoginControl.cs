using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkLogger;
using ClockWorkWebAPIWeb;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.Authentication;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.Core.LookupCourses;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;
using TechnoPro.Common.Public.Entities.Authentication.Authorization;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Authentication;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Email;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;

namespace TechnoPro.ClockWorkWeb.ctrls
{
	// Token: 0x02000121 RID: 289
	public class ClockWorkLoginControl : UserControl
	{
		// Token: 0x0600084C RID: 2124 RVA: 0x0003BA7B File Offset: 0x00039C7B
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			Page page = this.Page;
			if (page != null)
			{
				page.RegisterRequiresViewStateEncryption();
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x0003BA98 File Offset: 0x00039C98
		// (set) Token: 0x0600084E RID: 2126 RVA: 0x0003BAB5 File Offset: 0x00039CB5
		public string LoginFormType
		{
			get
			{
				return this.hv_LoginFormType.Value;
			}
			set
			{
				this.hv_LoginFormType.Value = value;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x0003BAC8 File Offset: 0x00039CC8
		private bool enableLoginProblems
		{
			get
			{
				return this.LoginFormType.Equals("instructor") && new WebSettingsClientManager().GetSettingValue<bool>(Setting.INSTRUCTOR_LoginProblems_Enabled);
			}
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0003BB00 File Offset: 0x00039D00
		protected void btn_loginProblems_Click(object sender, EventArgs e)
		{
			bool flag = !this.LoginFormType.Equals("instructor");
			if (!flag)
			{
				string lastAttemptedUsername = this.LastAttemptedUsername;
				string returnUrl = ClockWorkWebCore.GetReturnUrl(base.Session);
				int luCourseId = this.GetLuCourseId(returnUrl);
				int pid = this.GetPid(returnUrl);
				ILookupCourseClientManager lookupCourseClientManager = new LookupCourseClientManager();
				LookupCourseDTO lookupCourseDTO = lookupCourseClientManager.LoadCourseByLuCourseId(luCourseId);
				string value = (lookupCourseDTO == null) ? returnUrl : lookupCourseDTO.GetCourseDescriptionShort();
				StringDictionary stringDictionary = new StringDictionary();
				stringDictionary.Add("course", value);
				stringDictionary.Add("username", lastAttemptedUsername);
				IEmailClientManager emailClientManager = new EmailClientManager();
				MailMergeContextDTO mailMergeContext = new MailMergeContextDTO();
				emailClientManager.SendEmail(Setting.INSTRUCTOR_LoginProblems_Email, mailMergeContext, stringDictionary.InsertBaseUserMailMergeValues(), "LoginControl");
				this.p_loginProblems.Visible = false;
				this.ShowMessage("Thank you for your submission.");
			}
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0003BBD4 File Offset: 0x00039DD4
		private int GetLuCourseId(string str)
		{
			return ClockWorkLoginControl.GetUrlVariableInt2(str, "lucid", 1, true, DatabaseLayerFactory.ClockWork.Encryption);
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0003BC00 File Offset: 0x00039E00
		private int GetPid(string str)
		{
			return ClockWorkLoginControl.GetUrlVariableInt2(str, "pid", 0, true, DatabaseLayerFactory.ClockWork.Encryption);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0003BC2C File Offset: 0x00039E2C
		public static int GetUrlVariableInt2(string str, string varName, int combinedIndex, bool encrypted, IEncryption tripleDES)
		{
			int num = ClockWorkLoginControl.GetUrlVariableInt(str, varName, encrypted, tripleDES);
			bool flag = num <= 0;
			if (flag)
			{
				string[] array = str.Split(new char[]
				{
					','
				});
				bool flag2 = combinedIndex < array.Length;
				if (flag2)
				{
					str = ClockWorkWebCore.DecodeUrlVariable(array[combinedIndex], encrypted, tripleDES);
				}
				else
				{
					bool flag3 = array.Length != 0;
					if (flag3)
					{
						str = ClockWorkWebCore.DecodeUrlVariable(array[0], encrypted, tripleDES);
					}
					else
					{
						str = "0";
					}
				}
				try
				{
					num = int.Parse(str);
				}
				catch
				{
					num = 0;
				}
			}
			return num;
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0003BCCC File Offset: 0x00039ECC
		public static int GetUrlVariableInt(string str, string varName, bool encrypted, IEncryption tripleDES)
		{
			string s = ClockWorkWebCore.DecodeUrlVariable(str, encrypted, tripleDES);
			int result;
			bool flag = !int.TryParse(s, out result);
			if (flag)
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x0003BCFC File Offset: 0x00039EFC
		// (set) Token: 0x06000856 RID: 2134 RVA: 0x0003BD14 File Offset: 0x00039F14
		public bool OverrideTryToLoginRightAway
		{
			get
			{
				return this.overrideTryToLoginRightAway;
			}
			set
			{
				this.overrideTryToLoginRightAway = value;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x0003BD20 File Offset: 0x00039F20
		// (set) Token: 0x06000858 RID: 2136 RVA: 0x0003BD38 File Offset: 0x00039F38
		public string GroupsToAuthenticate
		{
			get
			{
				return this.groupsToAuthenticate;
			}
			set
			{
				this.groupsToAuthenticate = value;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x0003BD44 File Offset: 0x00039F44
		// (set) Token: 0x0600085A RID: 2138 RVA: 0x0003BD5C File Offset: 0x00039F5C
		public string PasswordLabel
		{
			get
			{
				return this.passwordLabel;
			}
			set
			{
				this.passwordLabel = value;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x0003BD68 File Offset: 0x00039F68
		public string Username
		{
			get
			{
				return this.Login1.UserName;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x0003BD88 File Offset: 0x00039F88
		// (set) Token: 0x0600085D RID: 2141 RVA: 0x0003BDA0 File Offset: 0x00039FA0
		public string TitleText
		{
			get
			{
				return this.titleText;
			}
			set
			{
				this.titleText = value;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x0003BDAC File Offset: 0x00039FAC
		// (set) Token: 0x0600085F RID: 2143 RVA: 0x0003BDC4 File Offset: 0x00039FC4
		public string InstructionText
		{
			get
			{
				return this.instructionText;
			}
			set
			{
				this.instructionText = value;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x0003BDD0 File Offset: 0x00039FD0
		// (set) Token: 0x06000861 RID: 2145 RVA: 0x0003BDE8 File Offset: 0x00039FE8
		public string InstructionText2
		{
			get
			{
				return this.instructionText2;
			}
			set
			{
				this.instructionText2 = value;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x0003BDF4 File Offset: 0x00039FF4
		// (set) Token: 0x06000863 RID: 2147 RVA: 0x0003BE0C File Offset: 0x0003A00C
		public string UsernameLabel
		{
			get
			{
				return this.usernameLabel;
			}
			set
			{
				this.usernameLabel = value;
			}
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0003BE18 File Offset: 0x0003A018
		private void ShowMessage(string msg)
		{
			this.p_err.Visible = true;
			bool flag = msg.Length > 0;
			if (flag)
			{
				this.lbl_err.Text = msg;
			}
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0003BE50 File Offset: 0x0003A050
		protected override void OnPreRender(EventArgs e)
		{
			bool flag = this.titleText != null;
			if (flag)
			{
				Control control = this.Login1.FindControl("lbl_actualLoginTitle");
				bool flag2 = control != null;
				if (flag2)
				{
					((Label)control).Text = this.titleText;
				}
			}
			bool flag3 = this.instructionText != null;
			if (flag3)
			{
				Control control = this.Login1.FindControl("lbl_actualLoginInstruction");
				bool flag4 = control != null;
				if (flag4)
				{
					((Label)control).Text = this.instructionText;
				}
			}
			bool flag5 = this.instructionText2 != null;
			if (flag5)
			{
				this.lbl_msg.Text = this.instructionText2;
			}
			bool flag6 = this.usernameLabel != null;
			if (flag6)
			{
				Control control = this.Login1.FindControl("UserName");
				bool flag7 = control != null;
				if (flag7)
				{
					((TextBox)control).Attributes["placeholder"] = this.usernameLabel;
				}
			}
			bool flag8 = this.passwordLabel != null;
			if (flag8)
			{
				Control control = this.Login1.FindControl("Password");
				bool flag9 = control != null;
				if (flag9)
				{
					((TextBox)control).Attributes["placeholder"] = this.passwordLabel;
				}
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000866 RID: 2150 RVA: 0x0003BF87 File Offset: 0x0003A187
		// (set) Token: 0x06000867 RID: 2151 RVA: 0x0003BF8F File Offset: 0x0003A18F
		public bool OverrideExternalCollectCredentialsUrl { get; set; }

		// Token: 0x06000868 RID: 2152 RVA: 0x0003BF98 File Offset: 0x0003A198
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.MyInit();
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0003BFAC File Offset: 0x0003A1AC
		private void MyInit()
		{
			Button button = (Button)this.Login1.FindControl("LoginButton");
			TextBox textBox = (TextBox)this.Login1.FindControl("Password");
			bool flag = button != null && textBox != null;
			if (flag)
			{
				string arg = string.Format("txt_pwd.value.replace( '{0}','{1}' ).replace( '{2}', '{3}').replace( '{4}', '{5}').replace( '{6}', '{7}').replace( '{8}', '{9}')", new object[]
				{
					"&",
					"&amp;",
					"<",
					"&lt;",
					">",
					"&gt;",
					"\"",
					"&quot;",
					"\\'",
					"&apos;"
				});
				button.OnClientClick = string.Format("var txt_pwd=document.getElementById('{0}'); txt_pwd.value = {1};", textBox.ClientID, arg);
			}
			string value = base.Request.QueryString["failed"];
			bool flag2 = !string.IsNullOrEmpty(value);
			if (flag2)
			{
				this.ShowMessage("");
			}
			bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.LOGIN_LoginFirstWithoutCredenntials);
			bool flag3 = settingValue && !this.overrideTryToLoginRightAway;
			if (flag3)
			{
				NavigatorClientManager.CurrentInstance.SetReturnUrl();
				this.TryToAuthenticate(WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetEnvironmentVariables());
			}
			IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
			bool flag4;
			string text = webAuthenticationAuthorizationWebClientManager.GetLoginPageUrl(out flag4);
			bool flag5 = !flag4 && !this.OverrideExternalCollectCredentialsUrl;
			if (flag5)
			{
				NavigatorClientManager.CurrentInstance.SetReturnUrl();
				text = text.Replace("\n", "");
				base.Response.Redirect(text, false);
			}
			else
			{
				ClockWorkWebCore.SetFocus(this.Login1.FindControl("UserName"));
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x0003C158 File Offset: 0x0003A358
		// (set) Token: 0x0600086B RID: 2155 RVA: 0x0003C1BF File Offset: 0x0003A3BF
		public string FailureText
		{
			get
			{
				string text = this.Login1.FailureText;
				bool flag = text.StartsWith("<div class='alert alert-danger' style='margin-top: 10px; margin-bottom: 0; padding: 5px'>");
				if (flag)
				{
					text = text.Substring("<div class='alert alert-danger' style='margin-top: 10px; margin-bottom: 0; padding: 5px'>".Length);
				}
				bool flag2 = text.EndsWith("</div>");
				if (flag2)
				{
					text = text.Substring(0, text.Length - "</div>".Length);
				}
				return text;
			}
			set
			{
				this.Login1.FailureText = "<div class='alert alert-danger' style='margin-top: 10px; margin-bottom: 0; padding: 5px'>" + value + "</div>";
			}
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0003C1DE File Offset: 0x0003A3DE
		protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
		{
			this.TryToAuthenticate();
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x0003C1E8 File Offset: 0x0003A3E8
		public string LastAttemptedUsername
		{
			get
			{
				object obj = base.Session["lastattemptedusername"];
				bool flag = obj != null;
				string result;
				if (flag)
				{
					result = (string)obj;
				}
				else
				{
					result = "";
				}
				return result;
			}
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0003C224 File Offset: 0x0003A424
		private IList<eAuthorizationContextItemType> ConvertLegacyGroupsToAuthenticate(string groupsToAuthenticate)
		{
			List<eAuthorizationContextItemType> list = new List<eAuthorizationContextItemType>();
			string[] array = (groupsToAuthenticate == null) ? new string[0] : groupsToAuthenticate.Split(new char[]
			{
				','
			});
			foreach (string text in array)
			{
				eAuthorizationContextItemType eAuthorizationContextItemType = eAuthorizationContextItemType.Unknown;
				string a = text;
				if (!(a == "staff"))
				{
					if (!(a == "student"))
					{
						if (!(a == "instructors"))
						{
							if (!(a == "altcontact"))
							{
								if (!(a == "notetakers"))
								{
									if (a == "tutors")
									{
										eAuthorizationContextItemType = eAuthorizationContextItemType.Tutors;
									}
								}
								else
								{
									eAuthorizationContextItemType = eAuthorizationContextItemType.Notetaking;
								}
							}
							else
							{
								eAuthorizationContextItemType = eAuthorizationContextItemType.AlternateContact;
							}
						}
						else
						{
							eAuthorizationContextItemType = eAuthorizationContextItemType.Instructor;
						}
					}
					else
					{
						eAuthorizationContextItemType = eAuthorizationContextItemType.Student;
					}
				}
				else
				{
					eAuthorizationContextItemType = eAuthorizationContextItemType.Staff;
				}
				bool flag = eAuthorizationContextItemType != eAuthorizationContextItemType.Unknown && !list.Contains(eAuthorizationContextItemType);
				if (flag)
				{
					list.Add(eAuthorizationContextItemType);
				}
			}
			return list;
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0003C318 File Offset: 0x0003A518
		private void TryToAuthenticate()
		{
			this.TryToAuthenticate(null);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0003C324 File Offset: 0x0003A524
		private bool TryToAuthenticateStaff(string username, string pwd, AuthenticationArgsDTO args)
		{
			CWLogger.Logger.Info("Starting try to authenticate staff '" + username + "'...");
			IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
			AuthenticationAndAuthorizationResultDTO authenticationAndAuthorizationResultDTO;
			if ((authenticationAndAuthorizationResultDTO = webAuthenticationAuthorizationWebClientManager.TryToAuthenticateStaff(username, pwd, args ?? new AuthenticationArgsDTO(), false)) == null)
			{
				(authenticationAndAuthorizationResultDTO = new AuthenticationAndAuthorizationResultDTO()).PassedAuthentication = false;
			}
			AuthenticationAndAuthorizationResultDTO authenticationAndAuthorizationResultDTO2 = authenticationAndAuthorizationResultDTO;
			return authenticationAndAuthorizationResultDTO2.PassedAuthentication;
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0003C384 File Offset: 0x0003A584
		private void TryToAuthenticate(AuthenticationArgsDTO args)
		{
			string text = this.Login1.UserName.ToLower().Trim();
			string text2 = WebUtility.HtmlDecode(this.Login1.Password);
			base.Session["lastattemptedusername"] = this.Login1.UserName;
			bool flag = false;
			bool flag2 = this.groupsToAuthenticate == "staff";
			if (flag2)
			{
				flag = this.TryToAuthenticateStaff(text, text2, args);
				bool flag3 = flag;
				if (flag3)
				{
					NavigatorClientManager.CurrentInstance.GotoLastReturnUrl(null, "default.aspx");
				}
			}
			bool flag4 = !flag;
			if (flag4)
			{
				CWLogger.Logger.Info("Starting try to authenticate '" + text + "'...");
				IList<eAuthorizationContextItemType> list = this.ConvertLegacyGroupsToAuthenticate(this.groupsToAuthenticate);
				AuthenticationArgsDTO authenticationArgs = args ?? new AuthenticationArgsDTO();
				IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
				AuthenticationAndAuthorizationResultDTO authenticationAndAuthorizationResultDTO;
				if ((authenticationAndAuthorizationResultDTO = webAuthenticationAuthorizationWebClientManager.TryToAuthenticateUser(this.Page, text, text2, authenticationArgs, list, true)) == null)
				{
					(authenticationAndAuthorizationResultDTO = new AuthenticationAndAuthorizationResultDTO()).PassedAuthentication = false;
				}
				AuthenticationAndAuthorizationResultDTO authenticationAndAuthorizationResultDTO2 = authenticationAndAuthorizationResultDTO;
				bool passedAuthentication = authenticationAndAuthorizationResultDTO2.PassedAuthentication;
				if (passedAuthentication)
				{
					NavigatorClientManager.CurrentInstance.GotoLastReturnUrl(null, "default.aspx");
				}
				else
				{
					this.ShowMessage("");
					bool enableLoginProblems = this.enableLoginProblems;
					if (enableLoginProblems)
					{
						this.p_loginProblems.Visible = true;
						bool flag5 = this.LoginFormType.Equals("instructor");
						if (flag5)
						{
							this.lbl_loginProblems.Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.INSTRUCTOR_LoginProblems_Message);
						}
					}
					ClockWorkWebCore.ClearAuthenticationInformationFromSession(base.Session);
				}
			}
		}

		// Token: 0x04000661 RID: 1633
		protected Panel p_err;

		// Token: 0x04000662 RID: 1634
		protected Label lbl_err;

		// Token: 0x04000663 RID: 1635
		protected Panel pnlLogin;

		// Token: 0x04000664 RID: 1636
		protected Login Login1;

		// Token: 0x04000665 RID: 1637
		protected Label lbl_msg;

		// Token: 0x04000666 RID: 1638
		protected Panel p_loginProblems;

		// Token: 0x04000667 RID: 1639
		protected Label lbl_loginProblems;

		// Token: 0x04000668 RID: 1640
		protected Button btn_loginProblems;

		// Token: 0x04000669 RID: 1641
		protected HiddenField hv_LoginFormType;

		// Token: 0x0400066A RID: 1642
		private string groupsToAuthenticate = "";

		// Token: 0x0400066B RID: 1643
		private bool overrideTryToLoginRightAway = false;

		// Token: 0x0400066C RID: 1644
		private string titleText = null;

		// Token: 0x0400066D RID: 1645
		private string instructionText = null;

		// Token: 0x0400066E RID: 1646
		private string instructionText2 = null;

		// Token: 0x0400066F RID: 1647
		private string usernameLabel = null;

		// Token: 0x04000670 RID: 1648
		private string passwordLabel = null;

		// Token: 0x04000672 RID: 1650
		private const string _failureTextDivPre = "<div class='alert alert-danger' style='margin-top: 10px; margin-bottom: 0; padding: 5px'>";

		// Token: 0x04000673 RID: 1651
		private const string _failureTextDivPost = "</div>";
	}
}
