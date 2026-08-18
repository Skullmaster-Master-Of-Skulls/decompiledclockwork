using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkController;
using ClockWorkWebAPI;
using ClockWorkWebAPI.Templates;
using ClockWorkWebAPIWeb;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync.Student;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.ClientManager.Core.DataSync;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.DataSync;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.Web.Entity.AuthenticationAuthorization;

namespace TechnoPro.ClockWorkWeb.user.workshop2
{
	// Token: 0x02000024 RID: 36
	public class user_workshop2_Newuser : Page
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x00005D24 File Offset: 0x00003F24
		private void Page_Init(object sender, EventArgs e)
		{
			int settingValue = new WebSettingsClientManager().GetSettingValue<int>(Setting.WORKSHOPS_registrationScreenNum);
			this.AddWizardControls(settingValue);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00005D4C File Offset: 0x00003F4C
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00005D70 File Offset: 0x00003F70
		protected void Page_Load(object sender, EventArgs e)
		{
			int pid = this.GetPid();
			bool flag = pid > 0;
			if (flag)
			{
				base.Response.Redirect("workshops.aspx", true);
			}
			else
			{
				bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.WORKSHOPS_allowNonClockWorkStudentsToRegister);
				bool flag2 = !settingValue;
				if (flag2)
				{
					base.Response.Redirect("Message.aspx?msgcode=notallowed");
				}
				else
				{
					string authenticatedUsername = WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetAuthenticatedUsername(this.Page);
					ClockWorkIdentity currentClockWorkIdentity = WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetCurrentClockWorkIdentity(this);
					string text = (((currentClockWorkIdentity != null) ? currentClockWorkIdentity.StudentNumber : null) ?? "").Trim();
					bool flag3 = authenticatedUsername.Length < 1;
					if (flag3)
					{
						NavigatorClientManager.CurrentInstance.SetReturnUrl();
						string loginUrl = ClockWorkWebAPI.Core.GetLoginUrl();
						base.Response.Redirect(loginUrl, true);
					}
					else
					{
						bool flag4 = !this.Page.IsPostBack;
						if (flag4)
						{
							this.lbl_confidentiality.Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.APPOINTMENTBOOKING_confidentialityAgreement);
							WebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
							IDataSyncClientManager dataSyncClientManager = new DataSyncClientManager();
							StudentDataSyncPreviewDataDTO studentPreviewDataByStudentNumberOrUsername = dataSyncClientManager.GetStudentPreviewDataByStudentNumberOrUsername(authenticatedUsername, text);
							string text2 = (((studentPreviewDataByStudentNumberOrUsername != null) ? studentPreviewDataByStudentNumberOrUsername.StudentNumber : null) ?? "").Trim();
							bool flag5 = text2.Length > 0;
							if (flag5)
							{
								text = text2;
							}
							string text3 = authenticatedUsername;
							bool flag6 = text.Length < 1;
							if (flag6)
							{
								bool settingValue2 = webSettingsClientManager.GetSettingValue<bool>(Setting.NOTETAKINGB_UsernameIsActuallyStudentNumber);
								bool flag7 = settingValue2;
								if (flag7)
								{
									text = text3;
								}
							}
							string text4 = (((studentPreviewDataByStudentNumberOrUsername != null) ? studentPreviewDataByStudentNumberOrUsername.Email : null) ?? "").Trim();
							bool flag8 = text4.Length < 1;
							if (flag8)
							{
								string settingValue3 = webSettingsClientManager.GetSettingValue<string>(Setting.LOGIN_UsernameType);
								bool flag9 = settingValue3.CompareTo("email") == 0;
								if (flag9)
								{
									string text5 = (webSettingsClientManager.GetSettingValue<string>(Setting.GENERAL_EmailSuffix) ?? "").Trim();
									bool flag10 = text5.Length > 0;
									if (flag10)
									{
										text4 = text3 + text5;
									}
								}
							}
							string text6 = (((studentPreviewDataByStudentNumberOrUsername != null) ? studentPreviewDataByStudentNumberOrUsername.FirstName : null) ?? "").Trim();
							string text7 = (((studentPreviewDataByStudentNumberOrUsername != null) ? studentPreviewDataByStudentNumberOrUsername.LastName : null) ?? "").Trim();
							bool flag11 = text6.Length < 1;
							if (flag11)
							{
								object obj = this.Session["fn"];
								text6 = ((obj is string) ? (((string)obj) ?? "").Trim() : "");
							}
							bool flag12 = text7.Length < 1;
							if (flag12)
							{
								object obj2 = this.Session["ln"];
								text7 = ((obj2 is string) ? (((string)obj2) ?? "").Trim() : "");
							}
							bool flag13 = text.Length > 0;
							if (flag13)
							{
								this.txt_student_no.Text = text.Trim();
								this.txt_student_no.ReadOnly = true;
							}
							bool flag14 = text4.Length > 0;
							if (flag14)
							{
								this.txt_email.Text = text4.Trim();
								this.txt_email.ReadOnly = true;
							}
							bool flag15 = text6.Length > 0;
							if (flag15)
							{
								this.txt_fn.Text = text6;
								this.txt_fn.ReadOnly = true;
							}
							bool flag16 = text7.Length > 0;
							if (flag16)
							{
								this.txt_ln.Text = text7;
								this.txt_ln.ReadOnly = true;
							}
						}
					}
				}
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00006118 File Offset: 0x00004318
		private void AddWizardControls(int screenNum)
		{
			bool flag = screenNum > 0;
			if (flag)
			{
				string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.GENERAL_EmailCid);
				DynamicControlLayoutHelper dynamicControlLayoutHelper = new DynamicControlLayoutHelper();
				DynamicScreenLayout.ControlsToScreen(ref dynamicControlLayoutHelper, base.Cache, screenNum, this.p_data, null, false, false, settingValue);
			}
			else
			{
				this.p_data.Visible = false;
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00006170 File Offset: 0x00004370
		private static List<int> IntListFromString(string commaSeparatedNumbers)
		{
			List<int> list = new List<int>();
			bool flag = commaSeparatedNumbers == null;
			List<int> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				string[] array = commaSeparatedNumbers.Split(new char[]
				{
					','
				});
				foreach (string text in array)
				{
					string text2 = text.Trim();
					bool flag2 = !string.IsNullOrEmpty(text2);
					if (flag2)
					{
						int item;
						bool flag3 = int.TryParse(text2, out item);
						if (flag3)
						{
							list.Add(item);
						}
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000061FC File Offset: 0x000043FC
		public void btn_submit_click(object sender, EventArgs e)
		{
			bool flag = !this.chk_iagree.Checked;
			if (flag)
			{
				this.lbl_iacceptrequired.Visible = true;
				this.lbl_iacceptrequired.Focus();
			}
			else
			{
				DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
				IEncryption encryption = clockWork.Encryption;
				string text = this.txt_student_no.Text.Trim();
				string text2 = this.txt_fn.Text.Trim();
				string text3 = this.txt_ln.Text.Trim();
				string text4 = this.txt_email.Text.Trim();
				string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.GENERAL_EmailSuffix);
				bool flag2 = text.Length > 0 && text2.Length > 0 && text3.Length > 0 && text4.Length > 0;
				if (flag2)
				{
					string settingValue2 = new WebSettingsClientManager().GetSettingValue<string>(Setting.GENERAL_EmailCid);
					ClockWorkIdentity currentClockWorkIdentity = WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetCurrentClockWorkIdentity(this);
					string str = (((currentClockWorkIdentity != null) ? currentClockWorkIdentity.UserName : null) ?? "").Trim();
					string settingValue3 = new WebSettingsClientManager().GetSettingValue<string>(Setting.LOGIN_UsernameType);
					bool flag3 = settingValue3.CompareTo("email") == 0;
					if (flag3)
					{
						text4 = str + settingValue;
					}
					string text5 = "1";
					int settingValue4 = new WebSettingsClientManager().GetSettingValue<int>(Setting.APPOINTMENTBOOKING_clientGid);
					bool flag4 = settingValue4 > 0;
					if (flag4)
					{
						text5 = text5 + "," + settingValue4.ToString();
					}
					List<int> list = user_workshop2_Newuser.IntListFromString(text5);
					int num = ClockWorkController.Student.CreateUser(text, text2, "", text3, list.ToArray());
					bool flag5 = num > 0;
					if (flag5)
					{
						bool settingValue5 = new WebSettingsClientManager().GetSettingValue<bool>(Setting.GENERAL_EmailEncrypted);
						byte[] value = ClockWorkWebAPI.Core.StringToBytes(text4, settingValue5, encryption);
						int num2;
						bool flag6 = !int.TryParse(settingValue2, out num2);
						if (flag6)
						{
							num2 = 0;
						}
						bool flag7 = num2 > 0;
						if (flag7)
						{
							string query = "INSERT INTO otherinfops (screennum,personid,controlid,controlvalue) VALUES (0,@pid,@cid,@val)";
							DbParameter[] parameters = new DbParameter[]
							{
								clockWork.GetParameter("@pid", DbType.Int32, num),
								clockWork.GetParameter("@cid", DbType.Int32, num2),
								clockWork.GetParameter("@val", DbType.Binary, value)
							};
							clockWork.ExecuteNonQuery(query, parameters);
						}
						int settingValue6 = new WebSettingsClientManager().GetSettingValue<int>(Setting.APPOINTMENTBOOKING_registrationIAgreeTableCid);
						bool flag8 = settingValue6 > 0;
						if (flag8)
						{
							DynamicScreenLayout.AddRowToDynamicTablePS(num, settingValue6, new string[]
							{
								"True",
								DateTime.Now.ToString("yyyy-MM-dd")
							});
						}
						int settingValue7 = new WebSettingsClientManager().GetSettingValue<int>(Setting.APPOINTMENTBOOKING_registrationScreenNum);
						DynamicScreenLayout.SaveDynamicData(ScreenType.ScreenType_PerStudent, num, settingValue7, base.Cache, this.p_data, settingValue2);
						string settingValue8 = new WebSettingsClientManager().GetSettingValue<string>(Setting.APPOINTMENTBOOKING_email_registration);
						EmailTemplate emailTemplate = ClockWorkWebCore.CreateEmailTemplate(settingValue8, base.Cache, "EN");
						bool flag9 = emailTemplate != null;
						if (flag9)
						{
							NameObjectPairCollection nameObjectPairCollection = new NameObjectPairCollection();
							DateTime now = DateTime.Now;
							bool flag10 = now.Month < 5;
							string val;
							if (flag10)
							{
								val = (now.Year - 1).ToString().Substring(2) + "." + now.Year.ToString();
							}
							else
							{
								val = now.Year.ToString().Substring(2) + "." + (now.Year + 1).ToString();
							}
							nameObjectPairCollection.Add("#<schoolyear>#", val);
							nameObjectPairCollection.Add("#<email>#", this.txt_email.Text);
							nameObjectPairCollection.Add("#<firstname>#", this.txt_fn.Text);
							nameObjectPairCollection.Add("#<lastname>#", this.txt_ln.Text);
							nameObjectPairCollection.Add("#<student_no>#", this.txt_student_no.Text);
							string subject;
							string body;
							emailTemplate.MailMerge(nameObjectPairCollection, out subject, out body);
							string emailAddress = (emailTemplate.To.ToLower().Trim().CompareTo("#<email>#") == 0) ? this.txt_email.Text : emailTemplate.To;
							IEmailClientManager emailClientManager = new EmailClientManager();
							IEmailClientManager emailClientManager2 = emailClientManager;
							TPMailMessageDTO tpmailMessageDTO = new TPMailMessageDTO();
							tpmailMessageDTO.From = new TPMailAddressDTO
							{
								EmailAddress = emailTemplate.From
							};
							tpmailMessageDTO.To = new List<TPMailAddressDTO>
							{
								new TPMailAddressDTO
								{
									EmailAddress = emailAddress
								}
							};
							List<TPMailAddressDTO> cc;
							if (emailTemplate.CcArray == null || emailTemplate.CcArray.Length == 0)
							{
								cc = null;
							}
							else
							{
								cc = (from g in emailTemplate.CcArray
								select new TPMailAddressDTO
								{
									EmailAddress = g
								}).ToList<TPMailAddressDTO>();
							}
							tpmailMessageDTO.Cc = cc;
							List<TPMailAddressDTO> bcc;
							if (emailTemplate.BccArray == null || emailTemplate.BccArray.Length == 0)
							{
								bcc = null;
							}
							else
							{
								bcc = (from g in emailTemplate.BccArray
								select new TPMailAddressDTO
								{
									EmailAddress = g
								}).ToList<TPMailAddressDTO>();
							}
							tpmailMessageDTO.Bcc = bcc;
							tpmailMessageDTO.Subject = subject;
							tpmailMessageDTO.Body = body;
							emailClientManager2.SendEmail(tpmailMessageDTO, "Workshop:NewUser");
						}
						WebAuthenticationAuthorizationWebClientManager.CurrentInstance.Logout(true);
					}
					else
					{
						this.p_msg.Visible = true;
						this.lbl_msg.Text = "Error creating new user.  Nothing was done.";
					}
				}
				else
				{
					this.p_msg.Visible = true;
					this.lbl_msg.Text = "Please fill in all required fields in order to continue...";
				}
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004233 File Offset: 0x00002433
		public void btn_cancel_click(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.Logout();
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00006775 File Offset: 0x00004975
		protected void btn_next_Click(object sender, EventArgs e)
		{
			this.p_page1.Visible = false;
			this.p_page2.Visible = true;
		}

		// Token: 0x04000088 RID: 136
		protected Label lbl_title;

		// Token: 0x04000089 RID: 137
		protected Label lbl_sub;

		// Token: 0x0400008A RID: 138
		protected Label lbl_iacceptrequired;

		// Token: 0x0400008B RID: 139
		protected Panel p_msg;

		// Token: 0x0400008C RID: 140
		protected Label lbl_msg;

		// Token: 0x0400008D RID: 141
		protected Panel p_page1;

		// Token: 0x0400008E RID: 142
		protected Panel p_name;

		// Token: 0x0400008F RID: 143
		protected Label lbl_student_no;

		// Token: 0x04000090 RID: 144
		protected TextBox txt_student_no;

		// Token: 0x04000091 RID: 145
		protected RequiredFieldValidator val_sn;

		// Token: 0x04000092 RID: 146
		protected Label Label1;

		// Token: 0x04000093 RID: 147
		protected TextBox txt_fn;

		// Token: 0x04000094 RID: 148
		protected RequiredFieldValidator RequiredFieldValidator1;

		// Token: 0x04000095 RID: 149
		protected Label Label2;

		// Token: 0x04000096 RID: 150
		protected TextBox txt_ln;

		// Token: 0x04000097 RID: 151
		protected RequiredFieldValidator RequiredFieldValidator2;

		// Token: 0x04000098 RID: 152
		protected Label Label3;

		// Token: 0x04000099 RID: 153
		protected TextBox txt_email;

		// Token: 0x0400009A RID: 154
		protected RequiredFieldValidator RequiredFieldValidator3;

		// Token: 0x0400009B RID: 155
		protected Panel p_data;

		// Token: 0x0400009C RID: 156
		protected Button btn_next;

		// Token: 0x0400009D RID: 157
		protected Button btn_cancel2;

		// Token: 0x0400009E RID: 158
		protected Panel p_page2;

		// Token: 0x0400009F RID: 159
		protected Label lbl_confidentiality;

		// Token: 0x040000A0 RID: 160
		protected CheckBox chk_iagree;

		// Token: 0x040000A1 RID: 161
		protected Button btn_submit;

		// Token: 0x040000A2 RID: 162
		protected Button btn_cancel;
	}
}
