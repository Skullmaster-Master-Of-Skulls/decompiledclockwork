using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPI;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Email;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.MailMerging;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.MailMerging;
using TechnoPro.Common.UI.Web.Entity.Modules;

namespace TechnoPro.ClockWorkWeb.user.NotetakingNotetakers
{
	// Token: 0x020000A2 RID: 162
	public class user_NotetakingNotetakers_DontRequireNotetaker : Page
	{
		// Token: 0x06000522 RID: 1314 RVA: 0x000258A8 File Offset: 0x00023AA8
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetNotetakerId(this.Page);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x000258CC File Offset: 0x00023ACC
		protected void Page_Load(object sender, EventArgs e)
		{
			int pid = this.GetPid();
			bool flag = pid < 1;
			if (flag)
			{
				NavigatorClientManager.CurrentInstance.NotAllowed(Setting.GENERAL_ErrorMessage_NotAClockWorkStudent, this.Page);
			}
			else
			{
				DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
				bool flag2 = !this.Page.IsPostBack;
				if (flag2)
				{
					int intFromUrlParameter = NavigatorClientManager.CurrentInstance.GetIntFromUrlParameter(base.Request.QueryString["lucid"] ?? "");
					string stringFromUrlParameter = NavigatorClientManager.CurrentInstance.GetStringFromUrlParameter("cd");
					this.lbl_course.Text = stringFromUrlParameter;
					this.lbl_course2.Text = stringFromUrlParameter;
					DbParameter[] parameters = new DbParameter[]
					{
						clockWork.GetParameter("@id", DbType.Int32, pid),
						clockWork.GetParameter("@lucid", DbType.Int32, intFromUrlParameter)
					};
					DataTable dataTable = clockWork.ExecuteQuery(ClockWorkWebAPI.QueryStorage.QS_Select_NumberProvidingFor, parameters);
					int num = (dataTable.Rows.Count > 0) ? ((int)dataTable.Rows[0][0]) : 0;
					this.p_special.Visible = (num > 0);
				}
			}
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x000259FC File Offset: 0x00023BFC
		protected void btn_accept1_Click(object sender, EventArgs e)
		{
			int pid = this.GetPid();
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			bool flag = pid < 1;
			if (flag)
			{
				NavigatorClientManager.CurrentInstance.NotAllowed(Setting.GENERAL_ErrorMessage_NotAClockWorkStudent, this.Page);
			}
			else
			{
				int intFromUrlParameter = NavigatorClientManager.CurrentInstance.GetIntFromUrlParameter(base.Request.QueryString["lucid"] ?? "");
				string stringFromUrlParameter = NavigatorClientManager.CurrentInstance.GetStringFromUrlParameter("cd");
				DbParameter[] parameters = new DbParameter[]
				{
					clockWork.GetParameter("@id", DbType.Int32, pid),
					clockWork.GetParameter("@lucid", DbType.Int32, intFromUrlParameter)
				};
				DataTable dataTable = clockWork.ExecuteQuery("SELECT    sr.serviceproviderrequestid,p.firstname,p.lastname,p.student_no,c.email\r\nFROM        serviceproviderrequests sr LEFT JOIN people p ON p.personid=sr.personid \r\n            LEFT JOIN common c ON c.personid=sr.personid\r\nWHERE sr.isactive=1 AND sr.serviceproviderid=@id AND lucourseid IN (SELECT DISTINCT luc2.lucourseid FROM lucourses luc LEFT JOIN lucourses luc2 ON luc2.subjectid=luc.subjectid AND luc2.course=luc.course WHERE luc.lucourseid=@lucid)", parameters);
				dataTable = encryption.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
				{
					"firstname",
					"lastname",
					"student_no",
					"email"
				});
				parameters = new DbParameter[]
				{
					clockWork.GetParameter("@id", DbType.Int32, pid),
					clockWork.GetParameter("@lucid", DbType.Int32, intFromUrlParameter),
					clockWork.GetParameter("@sptype", DbType.Int32, 128)
				};
				clockWork.ExecuteNonQuery(ClockWorkWebAPI.QueryStorage.QS_UPDATE_RemoveServiceProviderFromAllRequests, parameters);
				parameters = new DbParameter[]
				{
					clockWork.GetParameter("@id", DbType.Int32, pid),
					clockWork.GetParameter("@lucid", DbType.Int32, intFromUrlParameter),
					clockWork.GetParameter("@sptype", DbType.Int32, 128)
				};
				clockWork.ExecuteNonQuery(ClockWorkWebAPI.QueryStorage.QS_UPDATE_RemoveServiceProviderAvailability, parameters);
				parameters = new DbParameter[]
				{
					clockWork.GetParameter("@id", DbType.Int32, pid),
					clockWork.GetParameter("@lucid", DbType.Int32, intFromUrlParameter),
					clockWork.GetParameter("@note", DbType.Binary, encryption.Encrypt(this.txt_why.Text))
				};
				clockWork.ExecuteNonQuery(ClockWorkWebAPI.QueryStorage.QS_UPDATE_UpdateServiceProviderCancelDate, parameters);
				IEmailClientManager emailClientManager = new EmailClientManager();
				MailMergeContextDTO mailMergeContext = new MailMergeContextDTO
				{
					ServiceProviderId = pid,
					LuCourseId = intFromUrlParameter
				};
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					StringDictionary stringDictionary = new StringDictionary
					{
						{
							"course",
							stringFromUrlParameter
						},
						{
							"email",
							dataRow["email"].ToString()
						},
						{
							"firstname",
							dataRow["firstname"].ToString()
						},
						{
							"lastname",
							dataRow["lastname"].ToString()
						},
						{
							"student_no",
							dataRow["student_no"].ToString()
						},
						{
							"whycancelled",
							this.txt_why.Text.Trim()
						}
					};
					IMailMergeCodes mailMergeCodes = new MailMergeCodes();
					stringDictionary.Add("from", mailMergeCodes.GetDefaultFromAddress(eWebModule.Notetaking));
					stringDictionary.Add("signature", mailMergeCodes.GetDefaultSignature(eWebModule.Notetaking));
					emailClientManager.SendEmail(Setting.NOTETAKINGB_Email_NotetakerNotAvailable, mailMergeContext, stringDictionary.InsertBaseUserMailMergeValues(), "NotetakingNotetakers_DontRequireNotetaker");
				}
				bool flag2 = dataTable.Rows.Count > 0;
				if (flag2)
				{
					string value = string.Join(", ", (from DataRow dr in dataTable.Rows
					select dr["student_no"].ToString().Trim()).ToArray<string>());
					string value2 = string.Join("<br />", dataTable.Rows.Cast<DataRow>().Select(delegate(DataRow dr)
					{
						string separator = " . ";
						string[] array = new string[2];
						array[0] = string.Join(", ", (from g in new string[]
						{
							dr["lastname"].ToString().Trim(),
							dr["firstname"].ToString().Trim()
						}
						where g.Length > 0
						select g).ToArray<string>());
						array[1] = dr["student_no"].ToString().Trim();
						return string.Join(separator, array.ToArray<string>());
					}).ToArray<string>());
					string value3 = string.Join(", ", dataTable.Rows.Cast<DataRow>().Select(delegate(DataRow dr)
					{
						string separator = " . ";
						string[] array = new string[2];
						array[0] = string.Join(", ", (from g in new string[]
						{
							dr["lastname"].ToString().Trim(),
							dr["firstname"].ToString().Trim()
						}
						where g.Length > 0
						select g).ToArray<string>());
						array[1] = dr["student_no"].ToString().Trim();
						return string.Join(separator, array.ToArray<string>());
					}).ToArray<string>());
					StringDictionary stringDictionary2 = new StringDictionary
					{
						{
							"course",
							stringFromUrlParameter
						},
						{
							"students",
							value
						},
						{
							"studentnames",
							value2
						},
						{
							"studentnamesline",
							value3
						},
						{
							"whycancelled",
							this.txt_why.Text.Trim()
						}
					};
					IMailMergeCodes mailMergeCodes2 = new MailMergeCodes();
					stringDictionary2.Add("from", mailMergeCodes2.GetDefaultFromAddress(eWebModule.Notetaking));
					stringDictionary2.Add("signature", mailMergeCodes2.GetDefaultSignature(eWebModule.Notetaking));
					emailClientManager.SendEmail(Setting.NOTETAKINGB_Email_NotetakerNotAvailable_ForStaff, mailMergeContext, stringDictionary2.InsertBaseUserMailMergeValues(), "NotetakingNotetakers_DontRequireNotetaker2");
				}
				this.Session["msgcode"] = "dontrequirenotetaker";
				this.Session["msgcodedesc"] = "1";
				base.Response.Redirect("notetakerapp.aspx", true);
			}
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00025F44 File Offset: 0x00024144
		protected void btn_cancel1_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("notetakerapp.aspx");
		}

		// Token: 0x04000303 RID: 771
		private const string QS_Select_ANotetakersStudents = "SELECT    sr.serviceproviderrequestid,p.firstname,p.lastname,p.student_no,c.email\r\nFROM        serviceproviderrequests sr LEFT JOIN people p ON p.personid=sr.personid \r\n            LEFT JOIN common c ON c.personid=sr.personid\r\nWHERE sr.isactive=1 AND sr.serviceproviderid=@id AND lucourseid IN (SELECT DISTINCT luc2.lucourseid FROM lucourses luc LEFT JOIN lucourses luc2 ON luc2.subjectid=luc.subjectid AND luc2.course=luc.course WHERE luc.lucourseid=@lucid)";

		// Token: 0x04000304 RID: 772
		protected ScriptManager bbb;

		// Token: 0x04000305 RID: 773
		protected Panel p_title;

		// Token: 0x04000306 RID: 774
		protected Label lblTitle;

		// Token: 0x04000307 RID: 775
		protected Label lbl_course;

		// Token: 0x04000308 RID: 776
		protected Panel p_regular;

		// Token: 0x04000309 RID: 777
		protected Panel p_special;

		// Token: 0x0400030A RID: 778
		protected Label lbl_specialnote;

		// Token: 0x0400030B RID: 779
		protected Label lbl_msgregular;

		// Token: 0x0400030C RID: 780
		protected Label lbl_course2;

		// Token: 0x0400030D RID: 781
		protected Label lbl_why;

		// Token: 0x0400030E RID: 782
		protected TextBox txt_why;

		// Token: 0x0400030F RID: 783
		protected Button btn_cancel1;

		// Token: 0x04000310 RID: 784
		protected Button btn_accept1;
	}
}
