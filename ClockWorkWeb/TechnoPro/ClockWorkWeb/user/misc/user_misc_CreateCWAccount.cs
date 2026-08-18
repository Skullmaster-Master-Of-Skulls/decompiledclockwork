using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPI;
using ClockWorkWebAPI.Settings;
using ClockWorkWebAPIWeb;
using EncryptionClassLibrary;
using TechnoPro.Common.Configuration;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;

namespace TechnoPro.ClockWorkWeb.user.misc
{
	// Token: 0x020000B4 RID: 180
	public class user_misc_CreateCWAccount : Page
	{
		// Token: 0x06000594 RID: 1428 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00029BBC File Offset: 0x00027DBC
		private void AddWizardControls(db conn, IEncryption tripleDES, int screenNum)
		{
			bool flag = screenNum > 0;
			if (flag)
			{
				string settingValueString = AppSettingsV2.GetSettingValueString(Setting.GENERAL_EmailCid, conn, base.Cache);
				DynamicControlLayoutHelper dynamicControlLayoutHelper = new DynamicControlLayoutHelper(conn);
				DynamicScreenLayout.ControlsToScreen(ref dynamicControlLayoutHelper, base.Cache, conn, screenNum, this.p_data, null, false, false, settingValueString);
			}
			else
			{
				this.p_data.Visible = false;
			}
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00029C18 File Offset: 0x00027E18
		public void btn_submit_click(object sender, EventArgs e)
		{
			db db = new db(ClockWorkConfigurationManager.GetConnectionStringByNameUsingProtection("clockwork"));
			string text = this.txt_student_no.Text.Trim();
			string text2 = this.txt_fn.Text.Trim();
			string text3 = this.txt_ln.Text.Trim();
			string text4 = this.txt_email.Text.Trim();
			string settingValueString = AppSettingsV2.GetSettingValueString(Setting.GENERAL_EmailSuffix, db, base.Cache);
			bool flag = text.Length > 0 && text2.Length > 0 && text3.Length > 0 && text4.Length > 0;
			if (flag)
			{
				string settingValueString2 = AppSettingsV2.GetSettingValueString(Setting.GENERAL_EmailCid, db, base.Cache);
				string text5 = this.Session["username"].ToString();
				string settingValueString3 = AppSettingsV2.GetSettingValueString(Setting.LOGIN_UsernameType, db, base.Cache);
				bool flag2 = settingValueString3.CompareTo("email") == 0;
				if (flag2)
				{
					text4 = text5 + settingValueString;
				}
				else
				{
					text = text5;
				}
				string settingValueString4 = AppSettingsV2.GetSettingValueString(Setting.CLUBS_userGids, db, base.Cache);
				int num = Student.CreateUser(text, text2, "", text3, settingValueString4, db);
				bool flag3 = num > 0;
				if (flag3)
				{
					bool settingValueBool = AppSettingsV2.GetSettingValueBool(Setting.GENERAL_EmailEncrypted, db, base.Cache);
					byte[] value = ClockWorkWebAPI.Core.StringToBytes(text4, settingValueBool, db.TripleDES);
					db.Da.SelectCommand.CommandText = "INSERT INTO otherinfops (screennum,personid,controlid,controlvalue) VALUES (0,@pid,@cid,@val)";
					db.Da.SelectCommand.Parameters.Clear();
					db.Da.SelectCommand.Parameters.Add("@pid", num);
					db.Da.SelectCommand.Parameters.Add("@cid", int.Parse(settingValueString2));
					db.Da.SelectCommand.Parameters.Add("@val", value);
					db.Da.Fill(new DataTable());
					int settingValueInt = AppSettingsV2.GetSettingValueInt(Setting.CLUBS_userScreenNum, db, base.Cache);
					Exception ex = DynamicScreenLayout.SaveDynamicData(ScreenType.ScreenType_PerStudent, num, settingValueInt, base.Cache, this.p_data, db, settingValueString2);
					NavigatorClientManager.CurrentInstance.GotoLastReturnUrl("/user/misc/", "default.aspx");
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

		// Token: 0x06000597 RID: 1431 RVA: 0x00029EAC File Offset: 0x000280AC
		public void btn_cancel_click(object sender, EventArgs e)
		{
			db db = new db(ClockWorkConfigurationManager.GetConnectionStringByNameUsingProtection("clockwork"));
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.Logout();
		}

		// Token: 0x040003EC RID: 1004
		protected Label lbl_title;

		// Token: 0x040003ED RID: 1005
		protected Label lbl_sub;

		// Token: 0x040003EE RID: 1006
		protected Panel p_msg;

		// Token: 0x040003EF RID: 1007
		protected Label lbl_msg;

		// Token: 0x040003F0 RID: 1008
		protected Panel p_name;

		// Token: 0x040003F1 RID: 1009
		protected Label lbl_student_no;

		// Token: 0x040003F2 RID: 1010
		protected TextBox txt_student_no;

		// Token: 0x040003F3 RID: 1011
		protected RequiredFieldValidator val_sn;

		// Token: 0x040003F4 RID: 1012
		protected Label Label1;

		// Token: 0x040003F5 RID: 1013
		protected TextBox txt_fn;

		// Token: 0x040003F6 RID: 1014
		protected RequiredFieldValidator RequiredFieldValidator1;

		// Token: 0x040003F7 RID: 1015
		protected Label Label2;

		// Token: 0x040003F8 RID: 1016
		protected TextBox txt_ln;

		// Token: 0x040003F9 RID: 1017
		protected RequiredFieldValidator RequiredFieldValidator2;

		// Token: 0x040003FA RID: 1018
		protected Label Label3;

		// Token: 0x040003FB RID: 1019
		protected TextBox txt_email;

		// Token: 0x040003FC RID: 1020
		protected RequiredFieldValidator RequiredFieldValidator3;

		// Token: 0x040003FD RID: 1021
		protected Panel p_data;

		// Token: 0x040003FE RID: 1022
		protected Button btn_submit;

		// Token: 0x040003FF RID: 1023
		protected Button btn_cancel;
	}
}
