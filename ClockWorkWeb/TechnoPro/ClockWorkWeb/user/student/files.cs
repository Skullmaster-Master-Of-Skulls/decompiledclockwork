using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.Core.StudentFiles;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.ClientManager.ICore.StudentFiles;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.Web;

namespace TechnoPro.ClockWorkWeb.user.student
{
	// Token: 0x0200007B RID: 123
	public class files : Page
	{
		// Token: 0x0600046A RID: 1130 RVA: 0x00020298 File Offset: 0x0001E498
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !LicensingClientWebClientManager.CurrentInstance.IsModuleLicensed(TechnoPro.Common.Public.Entities.Settings.Group.STUDENTFILES);
			if (flag)
			{
				this.p_notLicensed.Visible = true;
			}
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			int pid = this.GetPid();
			string text = base.Request.QueryString["action"];
			bool flag2 = text != null && text == "download";
			if (flag2)
			{
				string text2 = base.Request.QueryString["id"];
				bool flag3 = !string.IsNullOrWhiteSpace(text2);
				int id;
				if (flag3)
				{
					int.TryParse(text2, out id);
				}
				else
				{
					id = 0;
				}
				bool flag4 = id > 0;
				if (flag4)
				{
					bool flag5 = pid > 0;
					if (flag5)
					{
						IStudentFileClientManager studentFileClientManager = new StudentFileClientManager();
						StudentFileCategoryFileDescriptionsWithColDataDTO[] array = (pid > 0) ? studentFileClientManager.LoadStudentFileDescriptions(pid) : null;
						StudentFileCategoryFileDescriptionsWithColDataDTO studentFileCategoryFileDescriptionsWithColDataDTO = (array != null) ? array.First<StudentFileCategoryFileDescriptionsWithColDataDTO>() : null;
						DynamicFileDescriptionWithColDataDTO dynamicFileDescriptionWithColDataDTO;
						if (studentFileCategoryFileDescriptionsWithColDataDTO == null)
						{
							dynamicFileDescriptionWithColDataDTO = null;
						}
						else
						{
							IList<DynamicFileDescriptionWithColDataDTO> fileDescriptions = studentFileCategoryFileDescriptionsWithColDataDTO.FileDescriptions;
							dynamicFileDescriptionWithColDataDTO = ((fileDescriptions != null) ? fileDescriptions.FirstOrDefault((DynamicFileDescriptionWithColDataDTO m) => m.FileId == id) : null);
						}
						DynamicFileDescriptionWithColDataDTO dynamicFileDescriptionWithColDataDTO2 = dynamicFileDescriptionWithColDataDTO;
						bool flag6 = dynamicFileDescriptionWithColDataDTO2 != null;
						if (flag6)
						{
							BinaryFileDTO binaryFileDTO = studentFileClientManager.LoadFileFromDynamicFileDescription(pid, dynamicFileDescriptionWithColDataDTO2);
							bool flag7 = binaryFileDTO != null;
							if (flag7)
							{
								IWebFileClientManager webFileClientManager = new WebFileClientManager();
								webFileClientManager.DownloadFile(binaryFileDTO.FileName, binaryFileDTO.ByteArray);
								return;
							}
						}
					}
				}
			}
			bool flag8 = !this.Page.IsPostBack;
			if (flag8)
			{
				this.lbl_downloadIntro.Text = webSettingsClientManager.GetSettingValue<string>(Setting.STUDENTFILES_StudentFilesIntro);
				this.txtInstructions.Text = webSettingsClientManager.GetSettingValue<string>(Setting.STUDENTFILES_FileUploadInstructions);
				this.hidden_message_fileUploadSuccess.Value = webSettingsClientManager.GetSettingValue<string>(Setting.STUDENTFILES_SuccessfulUploadMessage);
				this.hidden_message_fileTooBig.Value = webSettingsClientManager.GetSettingValue<string>(Setting.STUDENTFILES_FileTooLargeUploadMessage);
				this.hidden_message_fileTypeNotAllowed.Value = webSettingsClientManager.GetSettingValue<string>(Setting.STUDENTFILES_InvalidFileFormatUploadMessage).Replace("#<filetypes>#", "{extensions}");
				bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.STUDENTFILES_EnableStudentFileUploads);
				this.pSubmitFile.Visible = settingValue;
			}
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x000204C4 File Offset: 0x0001E6C4
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x000204E8 File Offset: 0x0001E6E8
		private string[] GetAllowedFileTypes()
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			string settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.STUDENTFILES_AllowedFileTypes);
			string[] result;
			if (settingValue == null)
			{
				result = null;
			}
			else
			{
				result = (from g in settingValue.Split(new char[]
				{
					','
				})
				select g.Trim() into m
				where m.Length > 0
				select m).ToArray<string>();
			}
			return result;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00020570 File Offset: 0x0001E770
		public string GetAllowedFileTypesForJavascript()
		{
			string[] value = (from g in this.GetAllowedFileTypes()
			select "\"" + (g.StartsWith(".") ? g.Substring(1) : g) + "\"").ToArray<string>();
			return "[" + string.Join(",", value) + "]";
		}

		// Token: 0x0400023F RID: 575
		protected Panel p_notLicensed;

		// Token: 0x04000240 RID: 576
		protected Panel pSubmitFile;

		// Token: 0x04000241 RID: 577
		protected Label txtInstructions;

		// Token: 0x04000242 RID: 578
		protected HiddenField hidden_showUpload;

		// Token: 0x04000243 RID: 579
		protected HiddenField hidden_allowedFileTypes;

		// Token: 0x04000244 RID: 580
		protected HiddenField hidden_message_fileTooBig;

		// Token: 0x04000245 RID: 581
		protected HiddenField hidden_message_fileTypeNotAllowed;

		// Token: 0x04000246 RID: 582
		protected HiddenField hidden_message_fileUploadSuccess;

		// Token: 0x04000247 RID: 583
		protected HtmlInputFile fileUpload;

		// Token: 0x04000248 RID: 584
		protected Label lbl_downloadIntro;
	}
}
