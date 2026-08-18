using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ClockWorkWebAPI;
using ClockWorkWebAPI.AuthenticationAuthorization;
using ClockWorkWebAPIWeb;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.Core.LookupCourses;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Authentication;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Email;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.MailMerging;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Notetaking;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.MailMerging;
using TechnoPro.Common.UI.ClientManager.Web.Core.Notetaking;
using TechnoPro.Common.UI.Web.Entity.AuthenticationAuthorization;
using TechnoPro.Common.UI.Web.Entity.Modules;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.user.NotetakingNotetakers
{
	// Token: 0x020000B2 RID: 178
	public class user_NotetakingNotetakers_SampleNotesNotetaker : Page
	{
		// Token: 0x06000586 RID: 1414 RVA: 0x00028DE8 File Offset: 0x00026FE8
		private int LookupNotetakerPid()
		{
			IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
			ClockWorkIdentity currentClockWorkIdentity_LoginIfNecessary = webAuthenticationAuthorizationWebClientManager.GetCurrentClockWorkIdentity_LoginIfNecessary(this.Page, ClockWorkWebAPI.AuthenticationAuthorization.GroupMembership.notetakers, true);
			return (currentClockWorkIdentity_LoginIfNecessary != null) ? currentClockWorkIdentity_LoginIfNecessary.NotetakerId : 0;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00028E24 File Offset: 0x00027024
		private string[] GetAllowedFileTypes()
		{
			return ".ppt,.pdf,.doc,.docx,.txt,.rtf,.html,.zip,.xls,.xlsx,.pptx,.jpg,.jpeg,.bmp,.gif,.png,.rar,.tif,.tiff".ToLower().Split(new char[]
			{
				','
			});
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00028E50 File Offset: 0x00027050
		public string GetAllowedFileTypesForJavascript()
		{
			string[] value = (from g in this.GetAllowedFileTypes()
			select "'" + g.Substring(1) + "'").ToArray<string>();
			return "[" + string.Join(",", value) + "]";
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00028EAC File Offset: 0x000270AC
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				int num = this.LookupNotetakerPid();
				bool flag2 = num <= 0;
				if (flag2)
				{
					base.Response.Redirect("notetakerapp.aspx");
				}
				else
				{
					string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_sampleNotesAdditionalInfoNotetaker);
					bool flag3 = settingValue.Length > 0;
					if (flag3)
					{
						this.lbl_additionalInfo.Text = settingValue;
					}
					else
					{
						this.p_additionalInfo.Visible = false;
					}
					int intFromUrlParameter = NavigatorClientManager.CurrentInstance.GetIntFromUrlParameter(base.Request.QueryString["lucid"] ?? "");
					ILookupCourseClientManager lookupCourseClientManager = new LookupCourseClientManager();
					LookupCourseDTO lookupCourseDTO = lookupCourseClientManager.LoadCourseByLuCourseId(intFromUrlParameter);
					this.lblSampleNotesCourse.Text = " for " + ((lookupCourseDTO == null) ? "??" : lookupCourseDTO.GetCourseDescription());
					ClockWorkWebAPI.AuthenticationAuthorization.UserInfo userInfo = ClockWorkWebCore.GetUserInfo(this.Session);
					bool flag4 = userInfo != null;
					if (flag4)
					{
						this.lblSampleNotesNotetaker.Text = userInfo.DisplayName;
					}
					string settingValue2 = new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_SampleNotesWording);
					string text = (settingValue2.Length > 0) ? (char.ToUpper(settingValue2[0]).ToString() + settingValue2.Substring(1)) : "";
					this.lblTitle.Text = text;
					string settingValue3 = new WebSettingsClientManager().GetSettingValue<string>(Setting.NOTETAKINGB_sampleNotesUploadInstructions);
					this.lbl_intro.Text = ((settingValue3.Length > 0) ? settingValue3 : this.lbl_intro.Text.Replace("Sample notes", text));
					this.p_fileupload.GroupingText = "Submit " + settingValue2;
					this.gv_courses.Columns[0].HeaderText = text + " (click to download)";
				}
			}
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00029098 File Offset: 0x00027298
		protected void gv_course_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			int num = this.LookupNotetakerPid();
			bool flag = num <= 0;
			if (flag)
			{
				base.Response.Redirect("notetakerapp.aspx");
			}
			int intFromUrlParameter = NavigatorClientManager.CurrentInstance.GetIntFromUrlParameter(base.Request.QueryString["lucid"] ?? "");
			int num2 = num;
			DateTime dateTime;
			DateTime dateTime2;
			ClockWorkWebAPI.Core.GetTermStartEndDates(out dateTime, out dateTime2);
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@lucid2", DbType.Int32, intFromUrlParameter),
				clockWork.GetParameter("@spid", DbType.Int32, num2),
				clockWork.GetParameter("@sdate", DbType.DateTime, dateTime),
				clockWork.GetParameter("@edate", DbType.DateTime, dateTime2)
			};
			int settingValue = new WebSettingsClientManager().GetSettingValue<int>(Setting.NOTETAKINGB_NotetakersMaxSampleNotesUploadCount);
			string query = string.Format(this.QS_Select_Notes2, settingValue);
			DataTable dataTable = clockWork.ExecuteQuery(query, parameters);
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				dataRow["description"] = Notetakingb.GetNotesFilename(dataRow);
			}
			this.gv_courses.DataSource = dataTable;
			bool flag2 = settingValue > 0 && dataTable.Rows.Count >= settingValue;
			if (flag2)
			{
				this.p_fileupload.Enabled = false;
				this.btn_upload.Visible = false;
				this.btn_backToChooseNotetaker.Visible = false;
				this.btn_return.Visible = false;
				this.lbl_selectFile.Text = "<br />You are only allowed to upload a maximum of " + settingValue.ToString() + " sample note(s).  You may replace one of your sample notes by first removing it and then adding a new one.";
				this.uploadDiv.Visible = false;
			}
			else
			{
				this.p_fileupload.Enabled = true;
				int num3 = settingValue - dataTable.Rows.Count;
				bool flag3 = num3 < 3;
				if (flag3)
				{
					this.grpFile3.Visible = false;
				}
				bool flag4 = num3 < 2;
				if (flag4)
				{
					this.grpFile2.Visible = false;
				}
			}
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x000292E4 File Offset: 0x000274E4
		protected void btn_upload_Click(object sender, EventArgs e)
		{
			int num = this.LookupNotetakerPid();
			int intFromUrlParameter = NavigatorClientManager.CurrentInstance.GetIntFromUrlParameter(base.Request.QueryString["lucid"] ?? "");
			int num2 = num;
			bool flag = num < 1 || intFromUrlParameter < 1;
			if (flag)
			{
				base.Response.Redirect("notetakerapp.aspx");
			}
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			this.p_fileUploadError.Visible = false;
			string[] allowedFileTypes = this.GetAllowedFileTypes();
			List<user_NotetakingNotetakers_SampleNotesNotetaker.LectureNoteWithDate> list = new List<user_NotetakingNotetakers_SampleNotesNotetaker.LectureNoteWithDate>();
			HttpFileCollection files = base.Request.Files;
			string[] allKeys = files.AllKeys;
			for (int i = 0; i < allKeys.Length; i++)
			{
				string text = allKeys[i];
				HttpPostedFile httpPostedFile = files[i];
				bool flag2 = httpPostedFile.ContentLength < 1;
				if (!flag2)
				{
					string text2 = (httpPostedFile.FileName ?? "").Trim();
					int num3 = text2.LastIndexOf(".");
					string ext = (num3 > 0) ? text2.Substring(num3) : "";
					bool flag3 = ext.Length < 1 || !allowedFileTypes.Any((string g) => g.Equals(ext, StringComparison.OrdinalIgnoreCase));
					if (!flag3)
					{
						bool flag4 = text.Contains("1");
						string text3;
						if (flag4)
						{
							text3 = this.lectureDate1.Value;
						}
						else
						{
							bool flag5 = text.Contains("2");
							if (flag5)
							{
								text3 = this.lectureDate2.Value;
							}
							else
							{
								text3 = this.lectureDate3.Value;
							}
						}
						text3 = (text3 ?? "").Trim();
						DateTime dateTime;
						bool flag6 = text3.Length < 1 || !DateTime.TryParse(text3, out dateTime) || dateTime == DateTime.MinValue || dateTime > DateTime.Now;
						if (!flag6)
						{
							list.Add(new user_NotetakingNotetakers_SampleNotesNotetaker.LectureNoteWithDate
							{
								LectureDate = dateTime,
								File = httpPostedFile
							});
						}
					}
				}
			}
			string text4 = "notes";
			bool flag7 = text4.Length < 1;
			if (flag7)
			{
				this.lbl_fileUploadError.Text = "Please provide a description for the file you want to upload. Nothing was done.";
				this.p_fileUploadError.Visible = true;
			}
			else
			{
				bool flag8 = list.Count < 1;
				if (flag8)
				{
					this.lbl_fileUploadError.Text = "Please choose a file to upload. Nothing was done.";
					this.p_fileUploadError.Visible = true;
				}
				else
				{
					int settingValue = new WebSettingsClientManager().GetSettingValue<int>(Setting.NOTETAKINGB_NotetakersMaxSampleNotesUploadCount);
					bool flag9 = settingValue <= 0;
					if (flag9)
					{
					}
					INotetakingWebClientManager notetakingWebClientManager = new NotetakingWebClientManager();
					foreach (user_NotetakingNotetakers_SampleNotesNotetaker.LectureNoteWithDate lectureNoteWithDate in list)
					{
						Exception ex;
						bool flag10 = notetakingWebClientManager.UploadLectureNote(lectureNoteWithDate.File.InputStream, lectureNoteWithDate.File.ContentLength, lectureNoteWithDate.File.FileName, text4, num2, intFromUrlParameter, lectureNoteWithDate.LectureDate, true, out ex);
						bool flag11 = !flag10;
						if (flag11)
						{
							this.lbl_fileUploadError.Text = "Something went wrong uploading the file; please try it again.";
							this.p_fileUploadError.Visible = true;
							return;
						}
					}
					bool flag12 = this.gv_courses.Items.Count == 0;
					if (flag12)
					{
						StringDictionary stringDictionary = new StringDictionary();
						DbParameter[] parameters = new DbParameter[]
						{
							clockWork.GetParameter("@id", DbType.Int32, num2)
						};
						DataTable dataTable = clockWork.ExecuteQuery(ClockWorkWebAPI.QueryStorage.QS_Select_ServiceProviderById, parameters);
						dataTable = encryption.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
						{
							"firstname",
							"lastname",
							"email",
							"student_no"
						});
						bool flag13 = dataTable.Rows.Count > 0;
						if (flag13)
						{
							DataRow dataRow = dataTable.Rows[0];
							stringDictionary.Add("email", dataRow["email"].ToString());
							stringDictionary.Add("firstname", dataRow["firstname"].ToString());
							stringDictionary.Add("lastname", dataRow["lastname"].ToString());
							stringDictionary.Add("student_no", dataRow["student_no"].ToString());
						}
						stringDictionary.Add("course", this.lblSampleNotesCourse.Text);
						IMailMergeCodes mailMergeCodes = new MailMergeCodes();
						stringDictionary.Add("from", mailMergeCodes.GetDefaultFromAddress(eWebModule.Notetaking));
						stringDictionary.Add("signature", mailMergeCodes.GetDefaultSignature(eWebModule.Notetaking));
						IEmailClientManager emailClientManager = new EmailClientManager();
						MailMergeContextDTO mailMergeContext = new MailMergeContextDTO
						{
							LuCourseId = intFromUrlParameter,
							ServiceProviderId = num2
						};
						emailClientManager.SendEmail(Setting.NOTETAKINGB_Email_ThankyouForUploadingSampleNotes, mailMergeContext, stringDictionary.InsertBaseUserMailMergeValues(), "NotetakingNotetakers_SampleNotesNotetaker");
					}
					this.lbl_fileUploadError.Text = "File successfully uploaded.";
					this.p_fileUploadError.Visible = true;
					base.Response.Redirect("SampleNotesNotetaker.aspx?success=1&lucid=" + base.Request.QueryString["lucid"], true);
				}
			}
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00024958 File Offset: 0x00022B58
		protected void btn_return_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("NotetakerApp.aspx");
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0002983C File Offset: 0x00027A3C
		protected void gv_courses_ItemCommand(object source, GridCommandEventArgs e)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			int num = this.LookupNotetakerPid();
			bool flag = num <= 0;
			if (flag)
			{
			}
			int urlVariableInt = ClockWorkWebCore.GetUrlVariableInt(base.Request, "lucid", true, encryption);
			object commandArgument = e.CommandArgument;
			bool flag2 = commandArgument != null;
			int num2;
			if (flag2)
			{
				string text = commandArgument.ToString().Trim();
				bool flag3 = text.Length > 0;
				if (flag3)
				{
					try
					{
						num2 = int.Parse(text);
					}
					catch
					{
						num2 = 0;
					}
				}
				else
				{
					num2 = 0;
				}
			}
			else
			{
				num2 = 0;
			}
			bool flag4 = e.CommandName.Equals("download");
			if (flag4)
			{
				INotetakingWebClientManager notetakingWebClientManager = new NotetakingWebClientManager();
				bool flag5 = notetakingWebClientManager.DownloadLectureNoteToBrowser(num2);
				bool flag6 = !flag5;
				if (flag6)
				{
					this.lbl_fileUploadError.Text = "Something went wrong downloading the file; please try it again.";
					this.p_fileUploadError.Visible = true;
				}
			}
			else
			{
				bool flag7 = e.CommandName.Equals("remove");
				if (flag7)
				{
					DbParameter[] array = new DbParameter[]
					{
						clockWork.Parameter
					};
					array[0].ParameterName = "@id";
					array[0].DbType = DbType.Int32;
					array[0].Value = num2;
					clockWork.ExecuteNonQuery(ClockWorkWebAPI.QueryStorage.QS_DELETE_Note, array);
					StringBuilder stringBuilder = new StringBuilder();
					for (int i = 0; i < base.Request.QueryString.Count; i++)
					{
						bool flag8 = i > 0;
						if (flag8)
						{
							stringBuilder.Append("&");
						}
						else
						{
							stringBuilder.Append("?");
						}
						string text2 = base.Request.QueryString.Keys[i];
						stringBuilder.Append(text2);
						stringBuilder.Append("=");
						string text3 = base.Request.QueryString[text2];
						bool flag9 = text3 != null;
						if (flag9)
						{
							stringBuilder.Append(text3);
						}
					}
					base.Response.Redirect("SampleNotesNotetaker.aspx" + stringBuilder.ToString(), true);
				}
			}
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00029A80 File Offset: 0x00027C80
		private string GetCourseDescriptionFromUrl()
		{
			return base.Request.QueryString["cd"] ?? "";
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00029AB0 File Offset: 0x00027CB0
		protected void btn_backToChooseNotetaker_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("ChooseNotetaker.aspx");
		}

		// Token: 0x040003CF RID: 975
		private string QS_Select_Notes2 = "SELECT DISTINCT TOP {0} nd.notetakerdocumentid,nd.docname,nd.numpages,nd.sizeinbytes,nd.datecreated,nd.notes,nd.lecturedate,sp.firstname,sp.lastname,sp.student_no,nd.issamplenotes,nd.lucourseid,luc.startdate,luc.enddate,luc.term,luc.duration,lucd.altlookupstring AS subject,luc.subjectid,luc.course,luc.timeofday,luc.section,lucd2.altlookupstring AS instructor,lucd.altlookupstring + ' ' + luc.course + luc.timeofday + ' ' + luc.section AS CourseDescription,nd.docname AS description \r\nFROM    notetakerdocument nd LEFT JOIN serviceproviders sp ON sp.serviceproviderid=nd.notetakerid \r\n        LEFT JOIN lucourses luc ON luc.lucourseid=nd.lucourseid \r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid \r\nWHERE nd.notetakerid=@spid \r\n        AND nd.lucourseid=@lucid2 \r\n        --AND nd.issamplenotes=1 \r\n        --AND NOT ( ( luc.enddate<@sdate ) OR (luc.startdate > @edate ) ) \r\nORDER BY nd.lecturedate";

		// Token: 0x040003D0 RID: 976
		protected ScriptManager bbb;

		// Token: 0x040003D1 RID: 977
		protected Panel p_title;

		// Token: 0x040003D2 RID: 978
		protected Label lblTitle;

		// Token: 0x040003D3 RID: 979
		protected Label lblSampleNotesNotetaker;

		// Token: 0x040003D4 RID: 980
		protected Label lblSampleNotesCourse;

		// Token: 0x040003D5 RID: 981
		protected Label lbl_intro;

		// Token: 0x040003D6 RID: 982
		protected Panel p_fileupload;

		// Token: 0x040003D7 RID: 983
		protected Panel p_fileUploadError;

		// Token: 0x040003D8 RID: 984
		protected Label lbl_fileUploadError;

		// Token: 0x040003D9 RID: 985
		protected Label lbl_selectFile;

		// Token: 0x040003DA RID: 986
		protected HtmlGenericControl uploadDiv;

		// Token: 0x040003DB RID: 987
		protected HtmlInputText lectureDate1;

		// Token: 0x040003DC RID: 988
		protected HtmlInputFile file1;

		// Token: 0x040003DD RID: 989
		protected HtmlGenericControl grpFile2;

		// Token: 0x040003DE RID: 990
		protected HtmlInputText lectureDate2;

		// Token: 0x040003DF RID: 991
		protected HtmlInputFile file2;

		// Token: 0x040003E0 RID: 992
		protected HtmlGenericControl grpFile3;

		// Token: 0x040003E1 RID: 993
		protected HtmlInputText lectureDate3;

		// Token: 0x040003E2 RID: 994
		protected HtmlInputFile file3;

		// Token: 0x040003E3 RID: 995
		protected Button btn_upload;

		// Token: 0x040003E4 RID: 996
		protected Button btn_return;

		// Token: 0x040003E5 RID: 997
		protected RadGrid gv_courses;

		// Token: 0x040003E6 RID: 998
		protected Button btn_backToChooseNotetaker;

		// Token: 0x040003E7 RID: 999
		protected Button btn_selectNotetaker;

		// Token: 0x040003E8 RID: 1000
		protected Panel p_additionalInfo;

		// Token: 0x040003E9 RID: 1001
		protected Label lbl_additionalInfo;

		// Token: 0x020001F9 RID: 505
		internal class LectureNoteWithDate
		{
			// Token: 0x17000306 RID: 774
			// (get) Token: 0x06000D99 RID: 3481 RVA: 0x0004F442 File Offset: 0x0004D642
			// (set) Token: 0x06000D9A RID: 3482 RVA: 0x0004F44A File Offset: 0x0004D64A
			public DateTime LectureDate { get; set; }

			// Token: 0x17000307 RID: 775
			// (get) Token: 0x06000D9B RID: 3483 RVA: 0x0004F453 File Offset: 0x0004D653
			// (set) Token: 0x06000D9C RID: 3484 RVA: 0x0004F45B File Offset: 0x0004D65B
			public HttpPostedFile File { get; set; }
		}
	}
}
