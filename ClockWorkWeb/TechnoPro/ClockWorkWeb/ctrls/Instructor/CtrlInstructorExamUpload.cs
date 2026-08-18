using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkLogger;
using ClockWorkWebAPI;
using ClockWorkWebAPIWeb;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files.FileUpload;
using TechnoPro.Common.ClientManager.Core.AppointmentsTestBooking;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.Core.TempFiles;
using TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking;
using TechnoPro.Common.ClientManager.ICore.TempFiles;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public.Entities.Files.FileUpload;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.Web;
using TechnoPro.Common.UI.Web.Entity.Common.EventArgs;
using TechnoPro.Common.UI.Web.Entity.Common.FileUpload;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.ctrls.Instructor
{
	// Token: 0x02000145 RID: 325
	public class CtrlInstructorExamUpload : UserControl
	{
		// Token: 0x1400001E RID: 30
		// (add) Token: 0x060009E2 RID: 2530 RVA: 0x00045434 File Offset: 0x00043634
		// (remove) Token: 0x060009E3 RID: 2531 RVA: 0x0004546C File Offset: 0x0004366C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<NumberEventArgs> OnExamIdRequired;

		// Token: 0x060009E4 RID: 2532 RVA: 0x000454A4 File Offset: 0x000436A4
		private int GetExamId()
		{
			EventHandler<NumberEventArgs> onExamIdRequired = this.OnExamIdRequired;
			bool flag = onExamIdRequired == null;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				NumberEventArgs numberEventArgs = new NumberEventArgs();
				onExamIdRequired(this, numberEventArgs);
				result = numberEventArgs.Number;
			}
			return result;
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x000454E0 File Offset: 0x000436E0
		private FileForUploadSet AddNewFilesForUploadSet()
		{
			IFileUploadWebClientManager fileUploadWebClientManager = new FileUploadWebClientManager();
			FileForUploadSet fileForUploadSet = fileUploadWebClientManager.CreateNewFileForUploadInfoInSession();
			this.myGuid.Value = fileForUploadSet.Guid;
			return fileForUploadSet;
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00045514 File Offset: 0x00043714
		public string GetMyGuid()
		{
			string text = (this.myGuid.Value ?? "").Trim();
			return (text.Length > 0) ? text : Guid.NewGuid().ToString();
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00045560 File Offset: 0x00043760
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				this.myAllowedFileTypes.Value = string.Join(",", (from g in this.AllowedFileTypes
				select g.ToLower()).ToArray<string>());
				HttpRuntimeSection httpRuntimeSection = (HttpRuntimeSection)WebConfigurationManager.GetSection("system.web/httpRuntime");
				this.myMaxFileSize.Value = ((httpRuntimeSection.MaxRequestLength - 100) * 1024).ToString();
				this.AddNewFilesForUploadSet();
				string settingValue = new WebSettingsClientManager().GetSettingValue<string>(Setting.INSTRUCTOR_SubmitFileInstructions);
				bool flag2 = !string.IsNullOrEmpty(settingValue);
				if (flag2)
				{
					this.lbl_fileinstructions.Text = settingValue;
				}
				int instructorId = WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetInstructorId(this.Page);
				int altContactId = WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetAltContactId(this.Page);
				bool flag3 = instructorId > 0 || altContactId > 0;
				if (flag3)
				{
					this.grid_previousuploads.Rebind();
				}
			}
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x00045674 File Offset: 0x00043874
		public static void RemoveFile(int fileForUploadId, string guid)
		{
			IFileUploadWebClientManager fileUploadWebClientManager = new FileUploadWebClientManager();
			fileUploadWebClientManager.DeleteFileForUpload(guid, fileForUploadId);
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x00045694 File Offset: 0x00043894
		public void UploadFiles(int newExamId, int iid)
		{
			string text = this.GetMyGuid();
			ITempFileClientManager tempFileClientManager = new TempFileClientManager();
			int[] array = tempFileClientManager.CopyTempFilesToInstructorExamUploadAndDeleteTempFile(new TempFileContextDTO
			{
				Usage = eTempFileUsage.InstructorUpload,
				GroupId = text
			}, newExamId, iid, "");
			CWLogger logger = CWLogger.Logger;
			string message = "CtrlInstructorExamUpload:UploadedExams:examid={0}:iid={1}:newexamids={2}:guid={3}";
			object[] array2 = new object[4];
			array2[0] = newExamId;
			array2[1] = iid;
			int num = 2;
			object obj;
			if (array != null)
			{
				obj = string.Join(",", (from g in array
				select g.ToString()).ToArray<string>());
			}
			else
			{
				obj = "NULL";
			}
			array2[num] = obj;
			array2[3] = (text ?? "NULL");
			logger.Trace(message, array2);
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x00045748 File Offset: 0x00043948
		protected void grid_previousuploads_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
		{
			int examId = this.GetExamId();
			int instructorId = WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetInstructorId(this.Page);
			int altContactId = WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetAltContactId(this.Page);
			bool flag = instructorId > 0 || altContactId > 0;
			if (flag)
			{
				this.RefreshPreviousUploadsList(instructorId, altContactId, examId);
			}
			else
			{
				base.Response.Redirect("default.aspx", true);
			}
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x000457B0 File Offset: 0x000439B0
		private void RefreshPreviousUploadsList(int iid, int altContactId, int examId)
		{
			IEnumerable<ExamFileDTO> source = this.LoadPreviousUploads(iid, altContactId, examId);
			this.grid_previousuploads.DataSource = (from g in source
			select new CtrlInstructorExamUpload.ExamFileWrapper(g)).ToList<CtrlInstructorExamUpload.ExamFileWrapper>();
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00045800 File Offset: 0x00043A00
		public IEnumerable<ExamFileDTO> LoadPreviousUploads(int iid, int altContactId, int examId)
		{
			bool flag = examId < 1;
			IEnumerable<ExamFileDTO> result;
			if (flag)
			{
				result = new List<ExamFileDTO>();
			}
			else
			{
				IExamFileClientManager examFileClientManager = new ExamFileClientManager();
				result = examFileClientManager.LoadExamFilesByExamCheckProfAltContactPermissions(iid, altContactId, examId, false, false);
			}
			return result;
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x00045834 File Offset: 0x00043A34
		public List<string> AllowedFileTypes
		{
			get
			{
				return (from g in new WebSettingsClientManager().GetSettingValue<string>(Setting.INSTRUCTOR_allowedfiletypes).Split(new char[]
				{
					','
				})
				select g.Trim() into h
				where h.Length > 0
				select h).ToList<string>();
			}
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x000458B4 File Offset: 0x00043AB4
		public FileForUploadSet GetPendingFileInfosForUpload()
		{
			return CtrlInstructorExamUpload.GetPendingFileInfosForUpload(this.GetMyGuid());
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x000458D4 File Offset: 0x00043AD4
		public static FileForUploadSet GetPendingFileInfosForUpload(string guid)
		{
			IFileUploadWebClientManager fileUploadWebClientManager = new FileUploadWebClientManager();
			return fileUploadWebClientManager.GetFileForUploadInfoFromSession(guid);
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x000458F4 File Offset: 0x00043AF4
		public int UploadedFilesCount
		{
			get
			{
				FileForUploadSet pendingFileInfosForUpload = this.GetPendingFileInfosForUpload();
				return (pendingFileInfosForUpload == null || pendingFileInfosForUpload.FilesForUpload == null) ? 0 : pendingFileInfosForUpload.FilesForUpload.Count;
			}
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x00045928 File Offset: 0x00043B28
		public string ValidateFiles(int examid)
		{
			List<string> allowedFileTypes = this.AllowedFileTypes;
			FileForUploadSet pendingFileInfosForUpload = this.GetPendingFileInfosForUpload();
			bool flag = pendingFileInfosForUpload == null || pendingFileInfosForUpload.FilesForUpload == null;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				List<FileForUpload> list = (from g in pendingFileInfosForUpload.FilesForUpload
				where allowedFileTypes.Any((string h) => h == Path.GetExtension(g.Filename ?? "").ToLower().Trim())
				select g).ToList<FileForUpload>();
				bool flag2 = list.Count > 0;
				if (flag2)
				{
					CWLogger.Logger.Debug("Instructor:ExamUpload:Submit:ValidateFiles:Failed - invalid file types:exts={0}:allowedfiletypes={1}", string.Join(";", (from g in list
					select g.Filename ?? "?").ToArray<string>()), allowedFileTypes);
					result = new WebSettingsClientManager().GetSettingValue<string>(Setting.INSTRUCTOR_InvalidFileFormatUploadMessage);
				}
				else
				{
					HttpRuntimeSection httpRuntimeSection = (HttpRuntimeSection)WebConfigurationManager.GetSection("system.web/httpRuntime");
					int maxRequestLength = (httpRuntimeSection.MaxRequestLength - 100) * 1024;
					List<FileForUpload> list2 = (maxRequestLength > 0) ? (from g in pendingFileInfosForUpload.FilesForUpload
					where g.FileSize > (long)maxRequestLength
					select g).ToList<FileForUpload>() : new List<FileForUpload>();
					bool flag3 = list2.Count > 0;
					if (flag3)
					{
						CWLogger.Logger.Debug("Instructor:ExamUpload:Submit:ValidateFiles:Failed - files too big:filesTooBig={0}:maxRequestLength={1}", string.Join(";", (from g in list2
						select (g.Filename ?? "?") + ": " + this.ToFileSize(g.FileSize)).ToArray<string>()), maxRequestLength.ToString());
						result = "One or more files are too large - max file size is " + this.ToFileSize((long)maxRequestLength);
					}
					else
					{
						result = null;
					}
				}
			}
			return result;
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00045ABC File Offset: 0x00043CBC
		public string ToFileSize(long source)
		{
			double num = Convert.ToDouble(source);
			bool flag = num >= Math.Pow(1024.0, 3.0);
			string result;
			if (flag)
			{
				result = Math.Round(num / Math.Pow(1024.0, 3.0), 2) + " GB";
			}
			else
			{
				bool flag2 = num >= Math.Pow(1024.0, 2.0);
				if (flag2)
				{
					result = Math.Round(num / Math.Pow(1024.0, 2.0), 2) + " MB";
				}
				else
				{
					bool flag3 = num >= 1024.0;
					if (flag3)
					{
						result = Math.Round(num / 1024.0, 2) + " KB";
					}
					else
					{
						result = num + " Bytes";
					}
				}
			}
			return result;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00045BC4 File Offset: 0x00043DC4
		protected void grid_previousuploads_ItemCommand(object source, GridCommandEventArgs e)
		{
			object commandArgument = e.CommandArgument;
			int num = 0;
			bool flag = commandArgument != null;
			if (flag)
			{
				string text = commandArgument.ToString();
				bool flag2 = text.Length > 0;
				if (flag2)
				{
					int.TryParse(commandArgument.ToString(), out num);
				}
			}
			bool flag3 = num < 1;
			if (!flag3)
			{
				string commandName = e.CommandName;
				if (!(commandName == "view"))
				{
					if (commandName == "remove")
					{
						int examId = this.GetExamId();
						Course.DeleteUploadedExam(examId, num);
						this.grid_previousuploads.Rebind();
					}
				}
				else
				{
					string text2;
					byte[] bytes = Course.DownloadExam(num, null, out text2);
					FileWeb.DownloadFile(this.Page, base.Response, text2.Replace(' ', '_'), bytes, true);
				}
			}
		}

		// Token: 0x040007BA RID: 1978
		protected HiddenField myGuid;

		// Token: 0x040007BB RID: 1979
		protected HiddenField myAllowedFileTypes;

		// Token: 0x040007BC RID: 1980
		protected HiddenField myMaxFileSize;

		// Token: 0x040007BD RID: 1981
		protected Label lbl_fileinstructions;

		// Token: 0x040007BE RID: 1982
		protected RadProgressArea uploadProgress;

		// Token: 0x040007BF RID: 1983
		protected RadProgressManager RadProgressManager1;

		// Token: 0x040007C0 RID: 1984
		protected RadGrid grid_previousuploads;

		// Token: 0x02000253 RID: 595
		public class ExamFileWrapper : WrapperBase<ExamFileDTO>
		{
			// Token: 0x06000F22 RID: 3874 RVA: 0x00051175 File Offset: 0x0004F375
			public ExamFileWrapper()
			{
			}

			// Token: 0x06000F23 RID: 3875 RVA: 0x0005117F File Offset: 0x0004F37F
			public ExamFileWrapper(ExamFileDTO file) : base(file)
			{
			}

			// Token: 0x17000359 RID: 857
			// (get) Token: 0x06000F24 RID: 3876 RVA: 0x0005118C File Offset: 0x0004F38C
			public int ExamFileId
			{
				get
				{
					return (base.Item == null) ? 0 : base.Item.ExamFileId;
				}
			}

			// Token: 0x1700035A RID: 858
			// (get) Token: 0x06000F25 RID: 3877 RVA: 0x000511B4 File Offset: 0x0004F3B4
			public string Filename
			{
				get
				{
					return (base.Item == null || base.Item.File == null || base.Item.File.FileName == null) ? "" : base.Item.File.FileName;
				}
			}
		}
	}
}
