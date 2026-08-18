using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles.Adapters;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.Core.StudentFiles;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.ClientManager.ICore.StudentFiles;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.StudentFiles;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.user.student
{
	// Token: 0x0200007C RID: 124
	public class filesfeed : IHttpHandler, IRequiresSessionState
	{
		// Token: 0x0600046F RID: 1135 RVA: 0x000205CC File Offset: 0x0001E7CC
		public void ProcessRequest(HttpContext context)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			int cid = webSettingsClientManager.GetSettingValue<int>(Setting.STUDENTFILES_FileUploadControlId);
			int studentPid = WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid();
			bool flag = studentPid > 0;
			filesfeed.FilesForDownload filesForDownload;
			if (flag)
			{
				IStudentFileClientManager studentFileClientManager = new StudentFileClientManager();
				StudentFileCategoryFileDescriptionsWithColDataDTO[] array = (studentPid > 0) ? studentFileClientManager.LoadStudentFileDescriptions(studentPid) : null;
				StudentFileCategoryFileDescriptionsWithColDataDTO studentFileCategoryFileDescriptionsWithColDataDTO = (array != null) ? array.FirstOrDefault<StudentFileCategoryFileDescriptionsWithColDataDTO>() : null;
				IList<DynamicFileDescriptionWithColDataDTO> source = ((studentFileCategoryFileDescriptionsWithColDataDTO != null) ? studentFileCategoryFileDescriptionsWithColDataDTO.FileDescriptions : null) ?? new List<DynamicFileDescriptionWithColDataDTO>();
				filesForDownload = new filesfeed.FilesForDownload
				{
					Title = (((studentFileCategoryFileDescriptionsWithColDataDTO != null) ? studentFileCategoryFileDescriptionsWithColDataDTO.StudentFileCategoryTitle : null) ?? ""),
					Files = (from g in source
					select new filesfeed.FileForDownload(g, cid)).ToList<filesfeed.FileForDownload>()
				};
				bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.STUDENTFILES_ShowUploadedFileStatuses);
				bool flag2 = !settingValue;
				if (flag2)
				{
					foreach (filesfeed.FileForDownload fileForDownload in filesForDownload.Files)
					{
						fileForDownload.Status = "";
						fileForDownload.StatusBadge = "success";
					}
				}
			}
			else
			{
				filesForDownload = null;
			}
			bool flag3 = filesForDownload == null;
			if (flag3)
			{
				filesForDownload = new filesfeed.FilesForDownload
				{
					Files = new List<filesfeed.FileForDownload>()
				};
			}
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			string s = javaScriptSerializer.Serialize(filesForDownload);
			context.Response.ContentType = "text/json";
			context.Response.Write(s);
			context.Response.End();
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x0002076C File Offset: 0x0001E96C
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x020001E6 RID: 486
		public class FilesForDownload
		{
			// Token: 0x170002F8 RID: 760
			// (get) Token: 0x06000D43 RID: 3395 RVA: 0x0004E8D0 File Offset: 0x0004CAD0
			// (set) Token: 0x06000D44 RID: 3396 RVA: 0x0004E8D8 File Offset: 0x0004CAD8
			public string Title { get; set; }

			// Token: 0x170002F9 RID: 761
			// (get) Token: 0x06000D45 RID: 3397 RVA: 0x0004E8E1 File Offset: 0x0004CAE1
			// (set) Token: 0x06000D46 RID: 3398 RVA: 0x0004E8E9 File Offset: 0x0004CAE9
			public IList<filesfeed.FileForDownload> Files { get; set; }
		}

		// Token: 0x020001E7 RID: 487
		public class FileForDownload
		{
			// Token: 0x06000D48 RID: 3400 RVA: 0x0000AF9E File Offset: 0x0000919E
			public FileForDownload()
			{
			}

			// Token: 0x06000D49 RID: 3401 RVA: 0x0004E8F4 File Offset: 0x0004CAF4
			public FileForDownload(DynamicFileDescriptionWithColDataDTO item, int cid)
			{
				bool flag = item == null;
				if (!flag)
				{
					string text = (from m in new string[]
					{
						(item.ControlId == cid && item.ColumnData != null && item.ColumnData.Count > 0) ? item.ColumnData[0] : null,
						"Submitted"
					}
					where !string.IsNullOrWhiteSpace(m)
					select m).First<string>() ?? "Submitted";
					this.IdStr = item.FileId.ToString();
					this.DateUploaded = ((item.DateUploaded != null) ? item.DateUploaded.Value : DateTime.MinValue);
					this.Filename = item.Filename;
					this.FileIconClass = item.Filename.GetFileIconClassFromFilename();
					this.IsFileUpload = (item.ControlId == cid);
					this.Status = text;
					this.StatusBadge = this.GetStatusBadge(text.Equals("submitted", StringComparison.OrdinalIgnoreCase) ? "" : text);
				}
			}

			// Token: 0x06000D4A RID: 3402 RVA: 0x0004EA24 File Offset: 0x0004CC24
			private string GetStatusBadge(string statusString)
			{
				StudentFilesStatusDTO studentFilesStatusDTO = new StudentFilesStatusDTO
				{
					StatusType = (statusString.EndsWith("[closed]", StringComparison.OrdinalIgnoreCase) ? eStudentFileStatusType.Closed : eStudentFileStatusType.Open),
					Title = statusString
				};
				return (studentFilesStatusDTO != null) ? studentFilesStatusDTO.GetCssClassSuffixForStatus() : null;
			}

			// Token: 0x170002FA RID: 762
			// (get) Token: 0x06000D4B RID: 3403 RVA: 0x0004EA69 File Offset: 0x0004CC69
			// (set) Token: 0x06000D4C RID: 3404 RVA: 0x0004EA71 File Offset: 0x0004CC71
			public string IdStr { get; set; }

			// Token: 0x170002FB RID: 763
			// (get) Token: 0x06000D4D RID: 3405 RVA: 0x0004EA7A File Offset: 0x0004CC7A
			// (set) Token: 0x06000D4E RID: 3406 RVA: 0x0004EA82 File Offset: 0x0004CC82
			public DateTime DateUploaded { get; set; }

			// Token: 0x170002FC RID: 764
			// (get) Token: 0x06000D4F RID: 3407 RVA: 0x0004EA8B File Offset: 0x0004CC8B
			// (set) Token: 0x06000D50 RID: 3408 RVA: 0x0004EA93 File Offset: 0x0004CC93
			public string Filename { get; set; }

			// Token: 0x170002FD RID: 765
			// (get) Token: 0x06000D51 RID: 3409 RVA: 0x0004EA9C File Offset: 0x0004CC9C
			// (set) Token: 0x06000D52 RID: 3410 RVA: 0x0004EAA4 File Offset: 0x0004CCA4
			public string FileIconClass { get; set; }

			// Token: 0x170002FE RID: 766
			// (get) Token: 0x06000D53 RID: 3411 RVA: 0x0004EAAD File Offset: 0x0004CCAD
			// (set) Token: 0x06000D54 RID: 3412 RVA: 0x0004EAB5 File Offset: 0x0004CCB5
			public bool IsFileUpload { get; set; }

			// Token: 0x170002FF RID: 767
			// (get) Token: 0x06000D55 RID: 3413 RVA: 0x0004EABE File Offset: 0x0004CCBE
			// (set) Token: 0x06000D56 RID: 3414 RVA: 0x0004EAC6 File Offset: 0x0004CCC6
			public string Status { get; set; }

			// Token: 0x17000300 RID: 768
			// (get) Token: 0x06000D57 RID: 3415 RVA: 0x0004EACF File Offset: 0x0004CCCF
			// (set) Token: 0x06000D58 RID: 3416 RVA: 0x0004EAD7 File Offset: 0x0004CCD7
			public string StatusBadge { get; set; }
		}
	}
}
