using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.SessionState;
using ClockWorkLogger;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.Web;

namespace TechnoPro.ClockWorkWeb.handlers
{
	// Token: 0x02000115 RID: 277
	public class FileUploadHandler : IHttpHandler, IReadOnlySessionState, IRequiresSessionState
	{
		// Token: 0x06000818 RID: 2072 RVA: 0x0003AD58 File Offset: 0x00038F58
		public void ProcessRequest(HttpContext context)
		{
			try
			{
				HttpRuntimeSection httpRuntimeSection = (HttpRuntimeSection)WebConfigurationManager.GetSection("system.web/httpRuntime");
				int num = (httpRuntimeSection.MaxRequestLength - 100) * 1024;
				bool flag = context.Request.ContentLength > num;
				if (flag)
				{
					CWLogger.Logger.Warn("FileUploadHandler:FileUpload is too large:contentlength={0}:maxrequestlength={1}", context.Request.ContentLength, num);
					throw new Exception("File is too large.");
				}
				bool flag2 = context.Request.Files.Count < 1;
				if (!flag2)
				{
					string text = (context.Request.Form["guid"] ?? "").Trim();
					bool flag3 = text.Length < 1;
					if (flag3)
					{
						CWLogger.Logger.Warn("FileUploadHandler:FileUpload is missing guid, can't upload file", context.Request.ContentLength, num);
						throw new Exception("FileUploadHandler:Missing guid");
					}
					HttpPostedFile httpPostedFile = context.Request.Files[0];
					string fileName = httpPostedFile.FileName;
					List<string> allowedFileTypes = FileUploadHandler.AllowedFileTypes;
					string ext = Path.GetExtension(fileName).ToLower().Trim();
					bool flag4 = allowedFileTypes.FirstOrDefault((string g) => g.Equals(ext, StringComparison.OrdinalIgnoreCase)) == null;
					if (flag4)
					{
						CWLogger.Logger.Warn("FileUploadHandler:File extension not allowed:ext={0}:allowedExtensions={1}", ext, string.Join(",", allowedFileTypes.ToArray()));
						throw new Exception("FileUploadHandler:File type not allowed");
					}
					byte[] fileBytes;
					using (BinaryReader binaryReader = new BinaryReader(httpPostedFile.InputStream))
					{
						fileBytes = binaryReader.ReadBytes(httpPostedFile.ContentLength);
					}
					IFileUploadWebClientManager fileUploadWebClientManager = new FileUploadWebClientManager();
					fileUploadWebClientManager.AddFileForUpload(text, fileName, fileBytes);
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("FileUploadHandler:err={0}", ex.ToString());
				throw;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x0003AF78 File Offset: 0x00039178
		public static List<string> AllowedFileTypes
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

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x0003AFF8 File Offset: 0x000391F8
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
	}
}
