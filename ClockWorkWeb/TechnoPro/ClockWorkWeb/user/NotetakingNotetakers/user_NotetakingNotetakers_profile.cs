using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using ClockWorkLogger;
using ClockWorkWebAPI;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.NotetakingNotetakers
{
	// Token: 0x020000B1 RID: 177
	public class user_NotetakingNotetakers_profile : Page
	{
		// Token: 0x06000581 RID: 1409 RVA: 0x000288B0 File Offset: 0x00026AB0
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = this.Page.Master != null && this.Page.Master is IClockWorkMasterPage;
			if (flag)
			{
				((IClockWorkMasterPage)this.Page.Master).SetCurrentPage(eClockWorkWebPage.NotetakingNotetakers_Profile);
			}
			int pid = this.GetPid();
			bool flag2 = pid < 1;
			if (flag2)
			{
				CWLogger.Logger.Info("Notetaking:NotetakerApp.aspx:msg=Student is logged in as '{0}', but does not have a notetaker id.  Sending them to the new notetaker application page...", "");
				base.Response.Redirect("NotetakerAppNew.aspx", true);
			}
			bool flag3 = !this.Page.IsPostBack;
			if (flag3)
			{
				this.lbl_mailingAddressIntro.Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_NotetakerApplicationAddressIntro);
				this.lbl_emailIntro.Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_EmailIntro);
				bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.NOTETAKINGB_NotetakerMandatoryEmail1);
				bool settingValue2 = new WebSettingsClientManager().GetSettingValue<bool>(Setting.NOTETAKINGB_NotetakerMandatoryEmail2);
				bool settingValue3 = new WebSettingsClientManager().GetSettingValue<bool>(Setting.NOTETAKINGB_NotetakerMandatoryPhone1);
				bool settingValue4 = new WebSettingsClientManager().GetSettingValue<bool>(Setting.NOTETAKINGB_NotetakerMandatoryPhone2);
				bool settingValue5 = new WebSettingsClientManager().GetSettingValue<bool>(Setting.NOTETAKINGB_NotetakerMandatoryAddress1);
				bool settingValue6 = new WebSettingsClientManager().GetSettingValue<bool>(Setting.NOTETAKINGB_NotetakerMandatoryAddress2);
				foreach (var <>f__AnonymousType in from g in new <>f__AnonymousType15<bool, RequiredFieldValidator, ValidatorCalloutExtender>[]
				{
					new
					{
						IsRequired = settingValue,
						val1 = this.val_email1,
						val2 = this.val_ajax_email1
					},
					new
					{
						IsRequired = settingValue2,
						val1 = this.val_email2,
						val2 = this.val_ajax_email2
					},
					new
					{
						IsRequired = settingValue3,
						val1 = this.val_phone1,
						val2 = this.val_ajax_phone1
					},
					new
					{
						IsRequired = settingValue4,
						val1 = this.val_phone2,
						val2 = this.val_ajax_phone2
					},
					new
					{
						IsRequired = settingValue5,
						val1 = this.val_address1,
						val2 = this.val_ajax_address1
					},
					new
					{
						IsRequired = settingValue6,
						val1 = this.val_address2,
						val2 = this.val_ajax_address2
					}
				}
				where g.IsRequired
				select g)
				{
					<>f__AnonymousType.val1.Enabled = true;
					<>f__AnonymousType.val2.Enabled = true;
				}
				Notetakerb notetakerb = new Notetakerb(pid);
				bool flag4 = notetakerb.NotetakerId > 0;
				if (flag4)
				{
					this.txt_firstName.Text = notetakerb.FirstName;
					this.txt_lastname.Text = notetakerb.LastName;
					this.txt_student_no.Text = notetakerb.Student_no;
					this.txt_email.Text = notetakerb.Email;
					this.txt_address.Text = notetakerb.Address;
					this.txt_phoneHome.Text = notetakerb.PhoneHome;
					this.txt_phoneCell.Text = notetakerb.PhoneCell;
					this.txt_email2.Text = notetakerb.Email2;
					this.txt_perm.Text = notetakerb.Address2;
					this.chk_mailing.Checked = notetakerb.AddressActive;
					this.chk_perm.Checked = notetakerb.Address2Active;
				}
				TextBox[] array = new TextBox[]
				{
					this.txt_firstName,
					this.txt_lastname,
					this.txt_student_no,
					this.txt_email
				};
				foreach (TextBox textBox in array)
				{
					bool flag5 = !string.IsNullOrEmpty(textBox.Text);
					if (flag5)
					{
						textBox.ReadOnly = true;
					}
				}
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00028C48 File Offset: 0x00026E48
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetNotetakerId(this.Page);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00028C6C File Offset: 0x00026E6C
		protected void btn_updateProfile_Click(object sender, EventArgs e)
		{
			int pid = this.GetPid();
			bool flag = pid <= 0;
			if (!flag)
			{
				Notetakerb.UpdateNotetakerAccount(pid, this.txt_firstName.Text, "", this.txt_lastname.Text, this.txt_student_no.Text, this.txt_email.Text, this.txt_email2.Text, this.txt_address.Text, this.txt_perm.Text, this.txt_phoneHome.Text, this.txt_phoneCell.Text, this.chk_mailing.Checked, this.chk_perm.Checked);
				this.Session["msgcode"] = "accountupdated";
				this.Session["mscodedesc"] = "";
			}
			this.ShowMessage();
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00028D50 File Offset: 0x00026F50
		private void ShowMessage()
		{
			object obj = this.Session["msgcode"];
			bool flag = obj == null;
			if (!flag)
			{
				string text = (string)obj;
				object obj2 = this.Session["msgcodedesc"];
				string a = text;
				if (a == "accountupdated")
				{
					this.lbl_topmsg.Text = "Your profile was successfully updated.";
					this.p_topmsg.Visible = true;
				}
				this.Session["msgcode"] = null;
				this.Session["msgcodedesc"] = null;
			}
		}

		// Token: 0x0400039E RID: 926
		protected ScriptManager bbb;

		// Token: 0x0400039F RID: 927
		protected Label lblTitle;

		// Token: 0x040003A0 RID: 928
		protected Panel p_topmsg;

		// Token: 0x040003A1 RID: 929
		protected Image img_topmsg;

		// Token: 0x040003A2 RID: 930
		protected Label lbl_topmsg;

		// Token: 0x040003A3 RID: 931
		protected Panel pProfile;

		// Token: 0x040003A4 RID: 932
		protected Label lbl_fn;

		// Token: 0x040003A5 RID: 933
		protected TextBox txt_firstName;

		// Token: 0x040003A6 RID: 934
		protected RequiredFieldValidator val_firstName;

		// Token: 0x040003A7 RID: 935
		protected ValidatorCalloutExtender val_ajax_firstname;

		// Token: 0x040003A8 RID: 936
		protected Label Label1;

		// Token: 0x040003A9 RID: 937
		protected TextBox txt_lastname;

		// Token: 0x040003AA RID: 938
		protected RequiredFieldValidator val_lastname;

		// Token: 0x040003AB RID: 939
		protected ValidatorCalloutExtender val_ajax_lastname;

		// Token: 0x040003AC RID: 940
		protected Label Label2;

		// Token: 0x040003AD RID: 941
		protected TextBox txt_student_no;

		// Token: 0x040003AE RID: 942
		protected RequiredFieldValidator val_student_no;

		// Token: 0x040003AF RID: 943
		protected ValidatorCalloutExtender val_ajax_student_no;

		// Token: 0x040003B0 RID: 944
		protected Label Label3;

		// Token: 0x040003B1 RID: 945
		protected TextBox txt_email;

		// Token: 0x040003B2 RID: 946
		protected RequiredFieldValidator val_email1;

		// Token: 0x040003B3 RID: 947
		protected ValidatorCalloutExtender val_ajax_email1;

		// Token: 0x040003B4 RID: 948
		protected Label Label4;

		// Token: 0x040003B5 RID: 949
		protected TextBox txt_email2;

		// Token: 0x040003B6 RID: 950
		protected RequiredFieldValidator val_email2;

		// Token: 0x040003B7 RID: 951
		protected ValidatorCalloutExtender val_ajax_email2;

		// Token: 0x040003B8 RID: 952
		protected Label lbl_emailIntro;

		// Token: 0x040003B9 RID: 953
		protected CheckBox chk_mailing;

		// Token: 0x040003BA RID: 954
		protected Label Label7;

		// Token: 0x040003BB RID: 955
		protected TextBox txt_address;

		// Token: 0x040003BC RID: 956
		protected RequiredFieldValidator val_address1;

		// Token: 0x040003BD RID: 957
		protected ValidatorCalloutExtender val_ajax_address1;

		// Token: 0x040003BE RID: 958
		protected CheckBox chk_perm;

		// Token: 0x040003BF RID: 959
		protected Label Label8;

		// Token: 0x040003C0 RID: 960
		protected TextBox txt_perm;

		// Token: 0x040003C1 RID: 961
		protected RequiredFieldValidator val_address2;

		// Token: 0x040003C2 RID: 962
		protected ValidatorCalloutExtender val_ajax_address2;

		// Token: 0x040003C3 RID: 963
		protected Label lbl_mailingAddressIntro;

		// Token: 0x040003C4 RID: 964
		protected Label Label5;

		// Token: 0x040003C5 RID: 965
		protected TextBox txt_phoneHome;

		// Token: 0x040003C6 RID: 966
		protected RequiredFieldValidator val_phone1;

		// Token: 0x040003C7 RID: 967
		protected ValidatorCalloutExtender val_ajax_phone1;

		// Token: 0x040003C8 RID: 968
		protected Label Label6;

		// Token: 0x040003C9 RID: 969
		protected TextBox txt_phoneCell;

		// Token: 0x040003CA RID: 970
		protected RequiredFieldValidator val_phone2;

		// Token: 0x040003CB RID: 971
		protected ValidatorCalloutExtender val_ajax_phone2;

		// Token: 0x040003CC RID: 972
		protected Button btn_updateProfile;

		// Token: 0x040003CD RID: 973
		protected Panel p_additionalInfo;

		// Token: 0x040003CE RID: 974
		protected Label lbl_additionalInfo;
	}
}
