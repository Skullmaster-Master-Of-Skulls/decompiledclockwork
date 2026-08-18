using System;
using System.Data;
using System.Data.Common;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPI;
using ClockWorkWebAPIWeb;
using Databases;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.user.instructor
{
	// Token: 0x020000CD RID: 205
	public class user_instructor_ChangePwd : Page
	{
		// Token: 0x060005EC RID: 1516 RVA: 0x0002BC60 File Offset: 0x00029E60
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetInstructorId(this.Page);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0002BC84 File Offset: 0x00029E84
		protected void Page_Load(object sender, EventArgs e)
		{
			ClockWorkWebCore.WarnUserBeforeSessionTimeout(this.Session, this.Page, base.GetType());
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				object obj = this.Context.Items["token"];
				bool flag2 = obj != null && obj is string;
				if (flag2)
				{
					string text = (string)obj;
				}
				int pid = this.GetPid();
				bool flag3 = pid <= 0;
				if (flag3)
				{
					base.Response.Redirect("Message.aspx?msgcode=notallowed", true);
				}
			}
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0002BD1C File Offset: 0x00029F1C
		private void ShowMessage(string msg)
		{
			this.lbl_msg.Text = msg;
			this.p_msg.Visible = true;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0002BD3C File Offset: 0x00029F3C
		protected void btn_submit_Click(object sender, EventArgs e)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			string text = this.txt_oldpwd.Text;
			string text2 = this.txt_newpwd.Text;
			string text3 = this.txt_newpwd2.Text;
			bool flag = text2.CompareTo(text3) != 0;
			if (flag)
			{
				this.ShowMessage("Your new passwords do not match!  Please re-enter them and try again.");
			}
			else
			{
				bool flag2 = text2.Length < 1;
				if (flag2)
				{
					this.ShowMessage("Please enter a new password in order to continue.");
				}
				else
				{
					bool flag3 = text.Length < 1;
					if (flag3)
					{
						this.ShowMessage("Please enter your old password in order to continue.");
					}
					else
					{
						string value = this.Session["username"].ToString();
						DbParameter[] array = new DbParameter[]
						{
							clockWork.Parameter
						};
						array[0].ParameterName = "@email";
						array[0].DbType = DbType.String;
						array[0].Value = value;
						DataTable dataTable = clockWork.ExecuteQuery(ClockWorkWebAPI.QueryStorage.QS_Select_InstructorByEmail2, array);
						bool flag4 = dataTable.Rows.Count > 0;
						if (flag4)
						{
							DataRow dataRow = dataTable.Rows[0];
							string strB = FormsAuthentication.HashPasswordForStoringInConfigFile(text, "sha1");
							string text4 = (dataRow["password"] == DBNull.Value) ? "" : ((string)dataRow["password"]);
							bool flag5 = text4.CompareTo(strB) == 0;
							if (flag5)
							{
								string value2 = FormsAuthentication.HashPasswordForStoringInConfigFile(text2, "sha1");
								array = new DbParameter[2];
								array[0] = clockWork.Parameter;
								array[0].ParameterName = "@email";
								array[0].DbType = DbType.String;
								array[0].Value = value;
								array[1] = clockWork.Parameter;
								array[1].ParameterName = "@pwd";
								array[1].DbType = DbType.String;
								array[1].Value = value2;
								clockWork.ExecuteNonQuery(ClockWorkWebAPI.QueryStorage.QS_UPDATE_UpdateInstructorPassword, array);
								this.ShowMessage("Password changed successfully. <a href='default.aspx'>Click here to return to the home page.</a>");
							}
							else
							{
								this.ShowMessage("Invalid old password. Nothing was done - please re-enter your old password and try again.");
							}
						}
						else
						{
							this.ShowMessage("Invalid old password.  Nothing was done.");
						}
					}
				}
			}
		}

		// Token: 0x04000444 RID: 1092
		protected Panel p_msg;

		// Token: 0x04000445 RID: 1093
		protected Label lbl_msg;

		// Token: 0x04000446 RID: 1094
		protected Panel p_changepwd;

		// Token: 0x04000447 RID: 1095
		protected Panel p_token;

		// Token: 0x04000448 RID: 1096
		protected Label lbl_token;

		// Token: 0x04000449 RID: 1097
		protected Label lbl_oldpwd;

		// Token: 0x0400044A RID: 1098
		protected TextBox txt_oldpwd;

		// Token: 0x0400044B RID: 1099
		protected Label lbl_newpwd;

		// Token: 0x0400044C RID: 1100
		protected TextBox txt_newpwd;

		// Token: 0x0400044D RID: 1101
		protected Label Label1;

		// Token: 0x0400044E RID: 1102
		protected TextBox txt_newpwd2;

		// Token: 0x0400044F RID: 1103
		protected Button btn_submit;
	}
}
