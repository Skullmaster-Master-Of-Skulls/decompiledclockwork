using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkController;
using ClockWorkLogger;
using ClockWorkWebAPI;
using ClockWorkWebAPI.AuthenticationAuthorization;
using ClockWorkWebAPIWeb;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.Core.Notetaking;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.ClientManager.ICore.Notetaking;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Authentication;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Email;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.MailMerging;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.MailMerging;
using TechnoPro.Common.UI.Web.Entity.AuthenticationAuthorization;
using TechnoPro.Common.UI.Web.Entity.Modules;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.NotetakingStudents
{
	// Token: 0x0200008E RID: 142
	public class user_NotetakingStudents_ChooseNotetaker : Page
	{
		// Token: 0x060004AF RID: 1199 RVA: 0x0002143C File Offset: 0x0001F63C
		private int GetPid()
		{
			IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
			ClockWorkIdentity currentClockWorkIdentity_LoginIfNecessary = webAuthenticationAuthorizationWebClientManager.GetCurrentClockWorkIdentity_LoginIfNecessary(this.Page, ClockWorkWebAPI.AuthenticationAuthorization.GroupMembership.student, true);
			return (currentClockWorkIdentity_LoginIfNecessary != null) ? currentClockWorkIdentity_LoginIfNecessary.PersonId : 0;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00021474 File Offset: 0x0001F674
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				int pid = this.GetPid();
				bool flag2 = pid < 1;
				if (flag2)
				{
					NavigatorClientManager.CurrentInstance.NotAllowed(Setting.GENERAL_ErrorMessage_NotAClockWorkStudent, this.Page);
				}
				else
				{
					int num = NavigatorClientManager.CurrentInstance.ConvertUrlStringToIntParameter(base.Request.QueryString["lucid"] ?? "");
					bool flag3 = num < 1;
					if (flag3)
					{
						base.Response.Redirect("courses.aspx", true);
					}
					else
					{
						string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_ChooseNotetakerInfoStarNote);
						bool flag4 = settingValue.Length > 0;
						if (flag4)
						{
							this.lbl_starNote.Text = settingValue;
							this.lblstarNote2.Visible = false;
							this.img_star.Visible = false;
						}
						this.lbl_ChooseNotetakerInfo.Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_ChooseNotetakerInfo);
						this.lblTitle.Text = "Select a note taker for " + NavigatorClientManager.CurrentInstance.GetStringFromUrlParameter("cd");
					}
				}
			}
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00021599 File Offset: 0x0001F799
		protected void btn_back_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("courses.aspx");
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x000215B0 File Offset: 0x0001F7B0
		protected void gv_courses_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.NOTETAKINGB_AllowStudentsToSeeNotetakerContactInfoAndName);
			int pid = this.GetPid();
			bool flag = pid < 1;
			if (flag)
			{
				NavigatorClientManager.CurrentInstance.NotAllowed(Setting.GENERAL_ErrorMessage_NotAClockWorkStudent, this.Page);
			}
			else
			{
				int num = NavigatorClientManager.CurrentInstance.ConvertUrlStringToIntParameter(base.Request.QueryString["lucid"] ?? "");
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				int settingValue2 = webSettingsClientManager.GetSettingValue<int>(Setting.NOTETAKINGB_EquivalentCourseStoredProcedureNumber);
				DbParameter[] parameters = new DbParameter[]
				{
					clockWork.GetParameter("@lucid", DbType.Int32, num)
				};
				string query = (settingValue2 > 0) ? ClockWorkWebAPI.QueryStorage.QS_Select_PotentialNotetakers_With_Upload_Count.Replace("equivalentcourses1", "equivalentcourses" + settingValue2.ToString()) : ClockWorkWebAPI.QueryStorage.QS_Select_PotentialNotetakers_With_Upload_Count.Replace("equivalentcourses1", "equivalentcourses");
				DataTable dataTable = clockWork.ExecuteQuery(query, parameters);
				dataTable = encryption.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
				{
					"firstname",
					"lastname",
					"student_no"
				});
				foreach (object obj in dataTable.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					dataColumn.ReadOnly = false;
				}
				int settingValue3 = webSettingsClientManager.GetSettingValue<int>(Setting.NOTETAKINGB_NotetakersMinSampleNotesUploadCount);
				bool flag2 = settingValue3 > 0;
				if (flag2)
				{
					DataTable dataTable2 = dataTable.Clone();
					DataRow[] array = dataTable.Select("NumNotes>=" + settingValue3.ToString());
					foreach (DataRow row in array)
					{
						dataTable2.ImportRow(row);
					}
					dataTable = dataTable2;
				}
				DataTable dataTable3 = dataTable.Clone();
				int k;
				for (int j = 0; j < dataTable.Rows.Count; j = k)
				{
					DataRow dataRow = dataTable.Rows[j];
					int num2 = (int)dataRow["serviceproviderid"];
					int num3 = (int)dataRow["lucourseid"];
					bool flag3 = settingValue;
					if (flag3)
					{
						dataRow["firstname"] = dataRow["firstname"];
						dataRow["lastname"] = dataRow["lastname"];
					}
					else
					{
						dataRow["firstname"] = "Notetaker";
						dataRow["lastname"] = num2.ToString();
					}
					k = j + 1;
					List<int> list = new List<int>();
					bool flag4 = dataRow["personid"] != DBNull.Value;
					if (flag4)
					{
						int item = (int)dataRow["personid"];
						bool flag5 = !list.Contains(item);
						if (flag5)
						{
							list.Add(item);
						}
					}
					while (k < dataTable.Rows.Count)
					{
						DataRow dataRow2 = dataTable.Rows[k];
						int num4 = (int)dataRow2["serviceproviderid"];
						int num5 = (int)dataRow2["lucourseid"];
						bool flag6 = num4 != num2 || num5 != num3;
						if (flag6)
						{
							break;
						}
						dataRow2["firstname"] = "Notetaker";
						dataRow2["lastname"] = num2.ToString();
						bool flag7 = dataRow2["personid"] != DBNull.Value;
						if (flag7)
						{
							int item2 = (int)dataRow2["personid"];
							bool flag8 = !list.Contains(item2);
							if (flag8)
							{
								list.Add(item2);
							}
						}
						k++;
					}
					dataRow["activenotetakerothercourse"] = list.Count;
					dataTable3.ImportRow(dataRow);
				}
				bool settingValue4 = new WebSettingsClientManager().GetSettingValue<bool>(Setting.NOTETAKINGB_Dont_Allow_Students_To_Pick_Different_Notetakers_For_the_Same_Class);
				bool flag9 = settingValue4;
				if (flag9)
				{
					DataTable dataTable4 = dataTable3.Clone();
					foreach (object obj2 in dataTable3.Rows)
					{
						DataRow dataRow3 = (DataRow)obj2;
						bool flag10 = Convert.ToBoolean(dataRow3["activenotetakerothercourse"]);
						bool flag11 = flag10;
						if (flag11)
						{
							dataTable4.ImportRow(dataRow3);
						}
					}
					bool flag12 = dataTable4.Rows.Count > 0;
					if (flag12)
					{
						dataTable3 = dataTable4;
						bool flag13 = dataTable3.Rows.Count > 1;
						if (flag13)
						{
							CWLogger.Logger.Info("ChooseNotetaker.aspx:DontAllowStudentsToChooseDifferentNotetakersForSameClass is enabled but notetaker list > 1 (pid:{0}:lucid={1}:notetakercount={2})", pid.ToString(), num.ToString(), dataTable3.Rows.Count.ToString());
						}
					}
				}
				this.gv_courses.DataSource = dataTable3;
			}
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00021ADC File Offset: 0x0001FCDC
		public string GetCommandArgument(object spid, object lucid, object courseDescription, object notetakerFirst, object notetakerLast)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append((spid == null) ? "" : spid.ToString());
			stringBuilder.Append(",");
			stringBuilder.Append((lucid == null) ? "" : lucid.ToString());
			stringBuilder.Append(",");
			string text = (courseDescription == null) ? "" : courseDescription.ToString();
			stringBuilder.Append(text.Replace(',', '.'));
			stringBuilder.Append(",");
			string text2 = ((notetakerFirst == null) ? "" : notetakerFirst.ToString()) + " " + ((notetakerLast == null) ? "" : notetakerLast.ToString());
			stringBuilder.Append(text2.Replace(',', '.'));
			return stringBuilder.ToString();
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00021BB0 File Offset: 0x0001FDB0
		protected void gv_courses_ItemCommand(object source, GridCommandEventArgs e)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			int pid = this.GetPid();
			bool flag = pid < 1;
			if (flag)
			{
				NavigatorClientManager.CurrentInstance.NotAllowed(Setting.GENERAL_ErrorMessage_NotAClockWorkStudent, this.Page);
			}
			else
			{
				int num = NavigatorClientManager.CurrentInstance.ConvertUrlStringToIntParameter(base.Request.QueryString["lucid"] ?? "");
				object commandArgument = e.CommandArgument;
				bool flag2 = commandArgument != null;
				int num2;
				int num3;
				string text2;
				string text3;
				if (flag2)
				{
					string text = commandArgument.ToString().Trim();
					bool flag3 = text.Length > 0;
					if (flag3)
					{
						string[] array = text.Split(new char[]
						{
							','
						});
						try
						{
							num2 = int.Parse(array[0]);
							num3 = int.Parse(array[1]);
							bool flag4 = array.Length > 2;
							if (flag4)
							{
								text2 = array[2];
								bool flag5 = array.Length > 3;
								if (flag5)
								{
									text3 = array[3];
									for (int i = 4; i < array.Length; i++)
									{
										text3 += array[i];
									}
								}
								else
								{
									text3 = "";
								}
							}
							else
							{
								text2 = "";
								text3 = "";
							}
						}
						catch
						{
							num2 = 0;
							num3 = 0;
							text2 = "";
							text3 = "";
						}
					}
					else
					{
						num2 = 0;
						num3 = 0;
						text2 = "";
						text3 = "";
					}
				}
				else
				{
					num2 = 0;
					num3 = 0;
					text2 = "";
					text3 = "";
				}
				bool flag6 = e.CommandName.CompareTo("view") == 0;
				if (flag6)
				{
					base.Response.Redirect(string.Concat(new string[]
					{
						"SampleNotesStudent.aspx?lucid2=",
						NavigatorClientManager.CurrentInstance.ConvertIntParameterToUrlString(num3) + "&lucid=",
						NavigatorClientManager.CurrentInstance.ConvertIntParameterToUrlString(num) + "&spid=",
						NavigatorClientManager.CurrentInstance.ConvertIntParameterToUrlString(num2) + "&cd=",
						text2,
						"&nn=",
						ClockWorkWebCore.EncodeUrlVariable(text3, true, encryption)
					}));
				}
				else
				{
					bool flag7 = e.CommandName.CompareTo("Select") == 0;
					if (flag7)
					{
						INotetakingClientManager notetakingClientManager = new NotetakingClientManager();
						bool flag8 = notetakingClientManager.AssignNotetaker(pid, num, num2, num3);
						bool flag9 = flag8;
						if (flag9)
						{
							this.SendEmailToNotetaker(num2, text2, pid, num);
						}
						this.Session["msgcode"] = "selectednotetaker";
						base.Response.Redirect("courses.aspx");
					}
				}
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00021E64 File Offset: 0x00020064
		private void SendEmailToNotetaker(int spid, string courseDescription, int pid, int lucid)
		{
			ServiceProvider serviceProvider = ServiceProvider.LoadServiceProvider(spid);
			StringDictionary stringDictionary = new StringDictionary();
			bool flag = serviceProvider != null;
			if (flag)
			{
				stringDictionary.Add("email", serviceProvider.Email);
				stringDictionary.Add("firstname", serviceProvider.FirstName);
				stringDictionary.Add("lastname", serviceProvider.LastName);
				stringDictionary.Add("student_no", serviceProvider.Student_no);
			}
			stringDictionary.Add("course", courseDescription);
			IMailMergeCodes mailMergeCodes = new MailMergeCodes();
			stringDictionary.Add("from", mailMergeCodes.GetDefaultFromAddress(eWebModule.Notetaking));
			stringDictionary.Add("signature", mailMergeCodes.GetDefaultSignature(eWebModule.Notetaking));
			IEmailClientManager emailClientManager = new EmailClientManager();
			MailMergeContextDTO mailMergeContext = new MailMergeContextDTO
			{
				PersonId = pid,
				LuCourseId = lucid,
				ServiceProviderId = spid
			};
			emailClientManager.SendEmail(Setting.NOTETAKINGB_Email_SelectedAsNotetaker, mailMergeContext, stringDictionary.InsertBaseUserMailMergeValues(), "NotetakingStudents_ChooseNotetaker");
		}

		// Token: 0x04000287 RID: 647
		protected ScriptManager bbb;

		// Token: 0x04000288 RID: 648
		protected Panel p_title;

		// Token: 0x04000289 RID: 649
		protected Label lblTitle;

		// Token: 0x0400028A RID: 650
		protected Panel p_intro;

		// Token: 0x0400028B RID: 651
		protected Label lbl_ChooseNotetakerInfo;

		// Token: 0x0400028C RID: 652
		protected RadGrid gv_courses;

		// Token: 0x0400028D RID: 653
		protected Label lbl_starNote;

		// Token: 0x0400028E RID: 654
		protected Image img_star;

		// Token: 0x0400028F RID: 655
		protected Label lblstarNote2;

		// Token: 0x04000290 RID: 656
		protected Button btn_back;
	}
}
