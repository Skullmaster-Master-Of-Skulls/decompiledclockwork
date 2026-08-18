using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ClockWorkWebAPI;
using Databases;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.ClientManager.Core.LookupCourses;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Notetaking;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.Notetaking;
using TechnoPro.Common.UI.Web.Entity.AuthenticationAuthorization;

namespace TechnoPro.ClockWorkWeb.user.NotetakingNotetakers
{
	// Token: 0x020000A9 RID: 169
	public class user_NotetakingNotetakers_NotesNotetaker : Page
	{
		// Token: 0x0600053A RID: 1338 RVA: 0x00026198 File Offset: 0x00024398
		private static int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetNotetakerId(null);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x000261B8 File Offset: 0x000243B8
		protected void Page_Load(object sender, EventArgs e)
		{
			this.lbl_DownloadLectureNotesInfo.Text = new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_DownloadLectureNotesInfo);
			int pid = user_NotetakingNotetakers_NotesNotetaker.GetPid();
			bool flag = pid <= 0;
			if (flag)
			{
				ClockWorkIdentity currentClockWorkIdentity = WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetCurrentClockWorkIdentity(this.Page);
				bool flag2 = currentClockWorkIdentity != null;
				if (flag2)
				{
					base.Response.Redirect("NotetakerAppNew.aspx", true);
				}
			}
			int intFromUrlParameter = NavigatorClientManager.CurrentInstance.GetIntFromUrlParameter(base.Request.QueryString["lucid"] ?? "");
			bool flag3 = intFromUrlParameter < 1;
			if (flag3)
			{
				base.Response.Redirect("NotetakerApp.aspx", true);
			}
			else
			{
				bool flag4 = !this.Page.IsPostBack;
				if (flag4)
				{
					this.lucidVal.Value = intFromUrlParameter.ToString();
					string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_UploadLectureNotesMessage);
					bool flag5 = settingValue.Length > 0;
					if (flag5)
					{
						this.lbl_submitNotesMsg.Text = settingValue;
					}
					else
					{
						this.p_submitNotesMessage.Visible = false;
					}
					ILookupCourseClientManager lookupCourseClientManager = new LookupCourseClientManager();
					LookupCourseDTO course = lookupCourseClientManager.LoadCourseByLuCourseId(intFromUrlParameter);
					this.lbl_course.Text = course.GetCourseDescription();
				}
			}
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x000262FC File Offset: 0x000244FC
		protected void btn_download_click(object sender, EventArgs e)
		{
			string value = this.fileIdVal.Value;
			this.fileIdVal.Value = "";
			int num;
			bool flag = !string.IsNullOrEmpty(value) && int.TryParse(value, out num) && num > 0;
			if (flag)
			{
				INotetakingWebClientManager notetakingWebClientManager = new NotetakingWebClientManager();
				bool flag2 = notetakingWebClientManager.DownloadLectureNoteToBrowser(num);
				bool flag3 = !flag2;
				if (flag3)
				{
					this.lbl_fileUploadError.Text = "Something went wrong downloading the file; please try it again.";
					this.p_fileUploadError.Visible = true;
				}
			}
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00026380 File Offset: 0x00024580
		protected void btn_upload_Click(object sender, EventArgs e)
		{
			string text = (this.datepicker.Value ?? "").Trim();
			DateTime dateTime;
			bool flag = text.Length < 1 || !DateTime.TryParse(text, out dateTime) || dateTime == DateTime.MinValue;
			if (flag)
			{
				this.lbl_fileUploadError.Text = "Please provide a lecture date for the file you want to upload. Nothing was done.";
				this.p_fileUploadError.Visible = true;
			}
			else
			{
				HttpFileCollection files = base.Request.Files;
				List<HttpPostedFile> list = new List<HttpPostedFile>();
				string[] allowedFileTypes = this.GetAllowedFileTypes();
				for (int i = 0; i < files.Count; i++)
				{
					HttpPostedFile httpPostedFile = files[i];
					bool flag2 = httpPostedFile.ContentLength > 0;
					if (flag2)
					{
						string fileName = Path.GetFileName((httpPostedFile.FileName ?? "").Trim());
						int num = fileName.LastIndexOf(".");
						string text2 = (num > 0) ? fileName.Substring(num).ToLower() : "";
						bool flag3 = text2.Length > 0 && allowedFileTypes.Contains(text2);
						if (flag3)
						{
							list.Add(httpPostedFile);
						}
					}
				}
				bool flag4 = list.Count < 1 && this.txt_description.Text.Trim().Length < 1;
				if (flag4)
				{
					this.lbl_fileUploadError.Text = "Please select a file to upload and/or provide a comment. Nothing was done.";
					this.p_fileUploadError.Visible = true;
				}
				else
				{
					int pid = user_NotetakingNotetakers_NotesNotetaker.GetPid();
					bool flag5 = pid <= 0;
					if (flag5)
					{
						base.Response.Redirect("notetakerapp.aspx", true);
					}
					int intFromUrlParameter = NavigatorClientManager.CurrentInstance.GetIntFromUrlParameter(base.Request.QueryString["lucid"] ?? "");
					int num2 = pid;
					INotetakingWebClientManager notetakingWebClientManager = new NotetakingWebClientManager();
					bool flag6 = list.Count > 0;
					if (flag6)
					{
						int num3 = 0;
						foreach (HttpPostedFile httpPostedFile2 in list)
						{
							Exception ex;
							bool flag7 = notetakingWebClientManager.UploadLectureNote(httpPostedFile2.InputStream, httpPostedFile2.ContentLength, httpPostedFile2.FileName, this.txt_description.Text, num2, intFromUrlParameter, dateTime, true, out ex);
							bool flag8 = flag7;
							if (!flag8)
							{
								this.lbl_fileUploadError.Text = string.Format("Something went wrong uploading a file ({0}); please try it again.", ((ex != null) ? ex.Message : null) ?? "?");
								this.p_fileUploadError.Visible = true;
								return;
							}
							num3++;
						}
					}
					else
					{
						this.UploadFile(0, null, num2, intFromUrlParameter, dateTime, this.txt_description.Text, "");
					}
					notetakingWebClientManager.NotifyStudentsNewLectureNotesHaveBeenUploaded(num2, intFromUrlParameter, dateTime);
					this.lbl_fileUploadError.Text = "File(s) successfully uploaded.";
					this.p_fileUploadError.Visible = true;
					base.Response.Redirect("NotesNotetaker.aspx?success=1&lucid=" + base.Request.QueryString["lucid"], true);
				}
			}
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x000266C0 File Offset: 0x000248C0
		private string[] GetAllowedFileTypes()
		{
			return ".ppt,.pdf,.doc,.docx,.txt,.rtf,.html,.zip,.xls,.xlsx,.pptx,.jpg,.jpeg,.bmp,.gif,.png,.rar,.tif,.tiff".ToLower().Split(new char[]
			{
				','
			});
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x000266EC File Offset: 0x000248EC
		public string GetAllowedFileTypesForJavascript()
		{
			string[] value = (from g in this.GetAllowedFileTypes()
			select "'" + g.Substring(1) + "'").ToArray<string>();
			return "[" + string.Join(",", value) + "]";
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00026748 File Offset: 0x00024948
		private void UploadFile(int sizeInBytes, byte[] fileData, int spid, int lucid, DateTime lectureDate, string comment, string filename)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@docName", DbType.String, filename),
				clockWork.GetParameter("@numPages", DbType.Int32, 1),
				clockWork.GetParameter("@sizeInBytes", DbType.Int32, sizeInBytes),
				clockWork.GetParameter("@dateCreated", DbType.DateTime, DateTime.Now),
				clockWork.GetParameter("@binaryData", DbType.Binary, (fileData == null || fileData.Length < 1) ? DBNull.Value : fileData),
				clockWork.GetParameter("@NotetakerID", DbType.Int32, spid),
				clockWork.GetParameter("@LUCourseId", DbType.Int32, lucid),
				clockWork.GetParameter("@notes", DbType.String, comment),
				clockWork.GetParameter("@lectureDate", DbType.DateTime, lectureDate),
				clockWork.GetParameter("@issamplenotes", DbType.Boolean, false)
			};
			clockWork.ExecuteNonQuery(ClockWorkWebAPI.QueryStorage.QS_INSERT_Notes, parameters);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00026854 File Offset: 0x00024A54
		[WebMethod]
		public static IList<LectureNoteWrapper> GetAvailableLectureNotes(int lucid)
		{
			List<LectureNoteWrapper> list = new List<LectureNoteWrapper>();
			int pid = user_NotetakingNotetakers_NotesNotetaker.GetPid();
			bool flag = lucid < 1 || pid < 1;
			IList<LectureNoteWrapper> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork);
				DbParameter[] parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@spid", DbType.Int32, pid),
					databaseLayer.GetParameter("@lucid", DbType.Int32, lucid)
				};
				DataTable dataTable = databaseLayer.ExecuteQuery(ClockWorkWebAPI.QueryStorage.QS_Select_NotetakerForCourse, parameters);
				bool flag2 = dataTable.Rows.Count < 1;
				if (flag2)
				{
					result = list;
				}
				else
				{
					parameters = new DbParameter[]
					{
						databaseLayer.GetParameter("@spid", DbType.Int32, pid),
						databaseLayer.GetParameter("@lucid", DbType.Int32, lucid)
					};
					DataTable dataTable2 = databaseLayer.ExecuteQuery(ClockWorkWebAPI.QueryStorage.QS_Select_Notes, parameters);
					list.AddRange(from DataRow dr in dataTable2.Rows
					select new LectureNoteWrapper((dr["notetakerdocumentid"] is DBNull) ? 0 : ((int)dr["notetakerdocumentid"]), (dr["lucourseid"] is DBNull) ? 0 : ((int)dr["lucourseid"]), dr["notes"].ToString().Trim(), (dr["lecturedate"] is DBNull) ? DateTime.MinValue : ((DateTime)dr["lecturedate"]), (dr["datecreated"] is DBNull) ? DateTime.MinValue : ((DateTime)dr["datecreated"]), dr["docname"].ToString().Trim()));
					list.Sort((LectureNoteWrapper g1, LectureNoteWrapper g2) => g2.LectureDate.CompareTo(g1.LectureDate));
					result = list;
				}
			}
			return result;
		}

		// Token: 0x04000320 RID: 800
		protected ScriptManager bbb;

		// Token: 0x04000321 RID: 801
		protected HtmlInputHidden lucidVal;

		// Token: 0x04000322 RID: 802
		protected Label lbl_course;

		// Token: 0x04000323 RID: 803
		protected Panel p_fileUploadError;

		// Token: 0x04000324 RID: 804
		protected Label lbl_fileUploadError;

		// Token: 0x04000325 RID: 805
		protected Label lbl_DownloadLectureNotesInfo;

		// Token: 0x04000326 RID: 806
		protected HtmlInputHidden fileIdVal;

		// Token: 0x04000327 RID: 807
		protected Button btn_download;

		// Token: 0x04000328 RID: 808
		protected Panel p_submitNotesMessage;

		// Token: 0x04000329 RID: 809
		protected Label lbl_submitNotes;

		// Token: 0x0400032A RID: 810
		protected HtmlInputText datepicker;

		// Token: 0x0400032B RID: 811
		protected HtmlGenericControl lbl_upload2;

		// Token: 0x0400032C RID: 812
		protected HtmlInputFile file1;

		// Token: 0x0400032D RID: 813
		protected HtmlInputFile file2;

		// Token: 0x0400032E RID: 814
		protected HtmlInputFile file3;

		// Token: 0x0400032F RID: 815
		protected Label lbl_comments;

		// Token: 0x04000330 RID: 816
		protected TextBox txt_description;

		// Token: 0x04000331 RID: 817
		protected Label lbl_submitNotesMsg;

		// Token: 0x04000332 RID: 818
		protected Button btn_upload;
	}
}
