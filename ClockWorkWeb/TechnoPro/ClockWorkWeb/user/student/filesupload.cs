using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.Core.StudentFiles;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.ClientManager.ICore.StudentFiles;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.user.student
{
	// Token: 0x02000080 RID: 128
	public class filesupload : HttpTaskAsyncHandler, IRequiresSessionState
	{
		// Token: 0x06000479 RID: 1145 RVA: 0x00020868 File Offset: 0x0001EA68
		public override async Task ProcessRequestAsync(HttpContext context)
		{
			int pid = WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid();
			bool flag = pid < 1;
			if (flag)
			{
				this.WriteError("Not authorized.", context);
			}
			else
			{
				bool flag2 = context.Request.Files.Count < 1;
				if (flag2)
				{
					this.WriteError("No file found.", context);
				}
				else
				{
					HttpPostedFile file = context.Request.Files[0];
					string fileName = file.FileName;
					byte[] buffer = new byte[file.ContentLength];
					file.InputStream.Read(buffer, 0, file.ContentLength);
					string comment = context.Request.Form["comment"];
					bool flag3 = string.IsNullOrWhiteSpace(comment);
					if (flag3)
					{
						this.WriteError("Missing comment.", context);
					}
					else
					{
						IStudentFileClientManager sfm = new StudentFileClientManager();
						int num = await sfm.UploadStudentFileAsync(comment, new BinaryFileDTO
						{
							FileName = fileName,
							ByteArray = buffer
						});
						int res = num;
						if (res < 1)
						{
							this.WriteError("Something went wrong - file may not have been uploaded.  Please refresh the page to see if the file has been uploaded.  Contact us if you need assistance.", context);
						}
						else
						{
							IEmailClientManager dm = new EmailClientManager();
							dm.SendEmail(new MailMergeContextDTO
							{
								PersonId = pid
							}, Setting.STUDENTFILES_Email_SuccessfulUploadNotification, TechnoPro.Common.Public.Entities.Settings.Group.STUDENTFILES, new Dictionary<string, string>
							{
								{
									"filename",
									fileName
								},
								{
									"comment",
									comment
								}
							});
							IWebSettingsClientManager wsm = new WebSettingsClientManager();
							context.Response.ContentType = "text/json";
							filesupload.UploadFileResult successItem = filesupload.UploadFileResult.SuccessResult(wsm.GetSettingValue<string>(Setting.STUDENTFILES_SuccessfulUploadMessage));
							context.Response.Write(new JavaScriptSerializer().Serialize(successItem));
							context.Response.End();
						}
					}
				}
			}
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x000208B6 File Offset: 0x0001EAB6
		private void WriteError(string errorMessage, HttpContext context)
		{
			context.Response.ContentType = "text/json";
			context.Response.Write(new JavaScriptSerializer().Serialize(new filesupload.UploadFileResult(errorMessage)));
			context.Response.End();
		}

		// Token: 0x020001EB RID: 491
		public class UploadFileResult
		{
			// Token: 0x06000D61 RID: 3425 RVA: 0x0000AF9E File Offset: 0x0000919E
			public UploadFileResult()
			{
			}

			// Token: 0x06000D62 RID: 3426 RVA: 0x0004EB71 File Offset: 0x0004CD71
			public UploadFileResult(string errorMessage)
			{
				this.ErrorMessage = errorMessage;
			}

			// Token: 0x06000D63 RID: 3427 RVA: 0x0004EB84 File Offset: 0x0004CD84
			public static filesupload.UploadFileResult SuccessResult(string successMessage)
			{
				return new filesupload.UploadFileResult
				{
					IsSuccess = true,
					SuccessMessage = successMessage
				};
			}

			// Token: 0x17000301 RID: 769
			// (get) Token: 0x06000D64 RID: 3428 RVA: 0x0004EBAB File Offset: 0x0004CDAB
			// (set) Token: 0x06000D65 RID: 3429 RVA: 0x0004EBB3 File Offset: 0x0004CDB3
			public bool IsSuccess { get; set; }

			// Token: 0x17000302 RID: 770
			// (get) Token: 0x06000D66 RID: 3430 RVA: 0x0004EBBC File Offset: 0x0004CDBC
			// (set) Token: 0x06000D67 RID: 3431 RVA: 0x0004EBC4 File Offset: 0x0004CDC4
			public string ErrorMessage { get; set; }

			// Token: 0x17000303 RID: 771
			// (get) Token: 0x06000D68 RID: 3432 RVA: 0x0004EBCD File Offset: 0x0004CDCD
			// (set) Token: 0x06000D69 RID: 3433 RVA: 0x0004EBD5 File Offset: 0x0004CDD5
			public string SuccessMessage { get; set; }
		}
	}
}
