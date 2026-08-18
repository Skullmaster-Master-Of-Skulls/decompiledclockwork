using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;
using System.Web.SessionState;
using Telerik.Web.UI.AsyncUpload;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI
{
	// Token: 0x02000076 RID: 118
	[RadCompressionSettings(HttpCompression = CompressionType.None)]
	public class AsyncUploadHandler : PreventableHandler, IHttpHandler, IRequiresSessionState
	{
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x0000BF44 File Offset: 0x0000A144
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x0000BF4C File Offset: 0x0000A14C
		public IAsyncUploadConfiguration Configuration { get; set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x0000BF55 File Offset: 0x0000A155
		// (set) Token: 0x060004BC RID: 1212 RVA: 0x0000BF5D File Offset: 0x0000A15D
		private string[] AllowedFileExtensions { get; set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x0000BF66 File Offset: 0x0000A166
		// (set) Token: 0x060004BE RID: 1214 RVA: 0x0000BF6E File Offset: 0x0000A16E
		internal IRequestData RequestData { get; set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0000BF77 File Offset: 0x0000A177
		// (set) Token: 0x060004C0 RID: 1216 RVA: 0x0000BF7F File Offset: 0x0000A17F
		internal IResponseWriter ResponseWriter { get; set; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x0000BF88 File Offset: 0x0000A188
		// (set) Token: 0x060004C2 RID: 1218 RVA: 0x0000BF90 File Offset: 0x0000A190
		internal ITempFileAppender FileAppender { get; set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x0000BF99 File Offset: 0x0000A199
		// (set) Token: 0x060004C4 RID: 1220 RVA: 0x0000BFA1 File Offset: 0x0000A1A1
		internal HttpContext Context { get; set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0000BFAA File Offset: 0x0000A1AA
		internal bool ChunkUploadRequest
		{
			get
			{
				return this.RequestData.FormValues != null && this.RequestData.FormValues["metadata"] != null;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x0000BFD3 File Offset: 0x0000A1D3
		public string FullPath
		{
			get
			{
				return Path.Combine(this.TemporaryFolder, this.TemporaryFileName);
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x0000BFE6 File Offset: 0x0000A1E6
		// (set) Token: 0x060004C8 RID: 1224 RVA: 0x0000BFF3 File Offset: 0x0000A1F3
		public virtual string TemporaryFolder
		{
			get
			{
				return this.Configuration.TempTargetFolder;
			}
			set
			{
				this.Configuration.TempTargetFolder = value;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0000C001 File Offset: 0x0000A201
		// (set) Token: 0x060004CA RID: 1226 RVA: 0x0000C009 File Offset: 0x0000A209
		public virtual int MaxJsonLength
		{
			get
			{
				return this._maxJsonLength;
			}
			set
			{
				this._maxJsonLength = value;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0000C012 File Offset: 0x0000A212
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x0000C03C File Offset: 0x0000A23C
		public string TemporaryFileName
		{
			get
			{
				if (string.IsNullOrEmpty(this.temporaryFileName))
				{
					this.temporaryFileName = Path.GetRandomFileName() + ".tmp";
				}
				return this.temporaryFileName;
			}
			set
			{
				this.temporaryFileName = value;
			}
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0000C045 File Offset: 0x0000A245
		public AsyncUploadHandler()
		{
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0000C058 File Offset: 0x0000A258
		public AsyncUploadHandler(IRequestData requestData, IResponseWriter responseWriter, ITempFileAppender fileAppender)
		{
			this.RequestData = requestData;
			this.ResponseWriter = responseWriter;
			this.FileAppender = fileAppender;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0000C080 File Offset: 0x0000A280
		public void ProcessRequest(HttpContext context)
		{
			bool flag = base.CheckPreventHandler("Telerik.Web.DisableAsyncUploadHandler", context, RadAsyncUpload.HandlerRouterKey);
			if (flag)
			{
				base.CompleteRequest(context.ApplicationInstance, 404);
				return;
			}
			this.EnsureContext(context);
			this.EnsureWriter();
			if (context.Request.Files.Count == 0)
			{
				this.ResponseWriter.WriteToResponse("{ \"message\" : \"RadAsyncUpload handler is registered succesfully, however, it may not be accessed directly.\" }");
				return;
			}
			this.EnsureSetup();
			if (!this.ChunkUploadRequest)
			{
				this.RequestData.UploadedFile = new AsyncPostedFile(this.RequestData.UploadedFile, this.FullPath, this.RequestData.UploadedFile.ContentLength);
				this.ProcessUploadedFile();
				return;
			}
			this.HandleChunkUploadRequest(context.Request.Form["metadata"]);
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0000C145 File Offset: 0x0000A345
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0000C148 File Offset: 0x0000A348
		internal void EnsureContext(HttpContext current)
		{
			this.Context = current;
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0000C151 File Offset: 0x0000A351
		internal void EnsureWriter()
		{
			if (this.ResponseWriter == null)
			{
				this.ResponseWriter = new ResponseWriter(this.Context);
			}
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0000C16C File Offset: 0x0000A36C
		internal void EnsureSetup()
		{
			if (this.RequestData == null)
			{
				this.RequestData = new RequestData(this.Context);
			}
			if (this.FileAppender == null && this.ChunkUploadRequest)
			{
				this.FileAppender = new ContentAppender(this.RequestData.UploadedFile.InputStream);
			}
			if (this.Configuration == null)
			{
				this.Configuration = this.GetConfiguration(this.Context.Request["rauPostData"]);
			}
			if (this.Configuration is AsyncUploadConfiguration)
			{
				AsyncUploadConfiguration asyncUploadConfiguration = this.Configuration as AsyncUploadConfiguration;
				if (asyncUploadConfiguration != null)
				{
					this.AllowedFileExtensions = asyncUploadConfiguration.AllowedFileExtensions;
					if (asyncUploadConfiguration.UseApplicationPoolImpersonation)
					{
						HostingEnvironment.Impersonate();
					}
				}
			}
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0000C21C File Offset: 0x0000A41C
		internal void HandleChunkUploadRequest(string serializedMetaData)
		{
			ChunkMetaData chunkMetaData = (ChunkMetaData)SerializationService.Deserialize(serializedMetaData, typeof(ChunkMetaData));
			if (AsyncUploadHandler.CheckFileNameForInvalidChars(chunkMetaData.UploadID))
			{
				throw new Exception("The uploaded file name contains invalid characters!");
			}
			this.RequestData.UploadedFile.FileName = this.ChangeOriginalFileName(this.RequestData.UploadedFile.FileName);
			if (this.CheckOriginalFileNameForInvalidChars(this.RequestData.UploadedFile.GetName()))
			{
				throw new Exception("The uploaded file name contains invalid characters!");
			}
			if (chunkMetaData.IsSingleChunkUpload)
			{
				this.RequestData.UploadedFile = new AsyncPostedFile(this.RequestData.UploadedFile, this.FullPath, this.RequestData.UploadedFile.ContentLength, false);
				(this.RequestData.UploadedFile as AsyncPostedFile).NormalizeWith(this.RequestData.FormValues);
				this.ProcessUploadedFile();
				return;
			}
			if (!this.ValidateSize(chunkMetaData.TotalFileSize, this.Configuration.MaxFileSize))
			{
				return;
			}
			if (!this.IsFileSizeValid(Convert.ToInt64(chunkMetaData.TotalFileSize), this.Configuration.MaxFileSize))
			{
				this.ResponseWriter.WriteToResponse("{ \"invalidFileSize\" : true }");
				return;
			}
			bool flag = chunkMetaData.ChunkIndex == --chunkMetaData.TotalChunks;
			bool flag2 = chunkMetaData.ChunkIndex == 0;
			this.TemporaryFileName = chunkMetaData.UploadID;
			this.FileAppender.AppendTo(this.FullPath);
			if (flag)
			{
				this.RequestData.UploadedFile = new AsyncPostedFile(this.RequestData.UploadedFile, this.FullPath, this.FileAppender.AppendedContentLength, true);
				(this.RequestData.UploadedFile as AsyncPostedFile).NormalizeWith(this.RequestData.FormValues);
				this.ProcessUploadedFile();
				return;
			}
			if (flag2)
			{
				this.AddCacheDependency(this.Context, chunkMetaData.UploadID, this.Configuration.TimeToLive, this.FullPath);
			}
			this.ResponseWriter.WriteToResponse("next");
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0000C418 File Offset: 0x0000A618
		private void ProcessUploadedFile()
		{
			if (!this.ValidateSize(this.RequestData.UploadedFile.ContentLength, this.Configuration.MaxFileSize))
			{
				return;
			}
			this.RequestData.UploadedFile.FileName = this.ChangeOriginalFileName(this.RequestData.UploadedFile.FileName);
			if (!this.ValidateFileExtension(this.RequestData.UploadedFile.GetExtension()))
			{
				return;
			}
			if (AsyncUploadHandler.CheckFileNameForInvalidChars(this.TemporaryFileName))
			{
				throw new Exception("The uploaded file name contains invalid characters!");
			}
			if (this.CheckOriginalFileNameForInvalidChars(this.RequestData.UploadedFile.GetName()))
			{
				throw new Exception("The uploaded file name contains invalid characters!");
			}
			IAsyncUploadResult asyncUploadResult = this.Process(this.RequestData.UploadedFile, this.Context, this.Configuration, this.TemporaryFileName);
			MetaData metaData = new MetaData
			{
				TempFileName = this.TemporaryFileName,
				AsyncUploadTypeName = asyncUploadResult.GetType().AssemblyQualifiedName
			};
			this.ResponseWriter.WriteToResponse(this.SerializeClientObject(asyncUploadResult, metaData));
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0000C51F File Offset: 0x0000A71F
		private bool ValidateSize(long fileSize, int maxFileSize)
		{
			if (this.IsFileSizeValid(fileSize, maxFileSize))
			{
				return true;
			}
			this.ResponseWriter.WriteToResponse("{ \"invalidFileSize\" : true }");
			return false;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0000C540 File Offset: 0x0000A740
		private bool ValidateFileExtension(string fileExtension)
		{
			if (this.AllowedFileExtensions != null && this.AllowedFileExtensions.Length != 0)
			{
				foreach (string text in this.AllowedFileExtensions)
				{
					if (fileExtension.ToLower().Trim(new char[]
					{
						'.'
					}) == text.ToLower().Trim(new char[]
					{
						'.'
					}))
					{
						return true;
					}
				}
				this.ResponseWriter.WriteToResponse("{ \"invalidFileExtension\" : true }");
				return false;
			}
			return true;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0000C5CE File Offset: 0x0000A7CE
		protected internal virtual IAsyncUploadResult Process(UploadedFile file, HttpContext context, IAsyncUploadConfiguration configuration, string tempFileName)
		{
			this.SaveToTempFolder(file, configuration, context, tempFileName);
			return this.CreateDefaultUploadResult<UploadedFileInfo>(file);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0000C5E2 File Offset: 0x0000A7E2
		protected void SaveToTempFolder(UploadedFile file, IAsyncUploadConfiguration config, HttpContext context, string tempFileName)
		{
			this.AddCacheDependency(context, tempFileName, config.TimeToLive, this.FullPath);
			file.SaveAs(this.FullPath, false);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0000C608 File Offset: 0x0000A808
		protected T CreateDefaultUploadResult<T>(UploadedFile file) where T : IAsyncUploadResult, new()
		{
			T t = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
			UploadedFileInfo.CopyFileInfo(t, file);
			return t;
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0000C643 File Offset: 0x0000A843
		protected bool IsFileSizeValid(long contentLength, int maxFileSize)
		{
			return maxFileSize <= 0 || contentLength <= (long)maxFileSize;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0000C654 File Offset: 0x0000A854
		protected internal virtual void AddCacheDependency(HttpContext context, string tempFileName, TimeSpan timeToLive, string fullPath)
		{
			if (context.Cache.Get(tempFileName) == null)
			{
				context.Cache.Insert(tempFileName, fullPath, null, DateTime.Now.Add(timeToLive), TimeSpan.Zero, CacheItemPriority.NotRemovable, new CacheItemRemovedCallback(this.RemovedCallback));
			}
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0000C6A0 File Offset: 0x0000A8A0
		private void RemovedCallback(string key, object value, CacheItemRemovedReason reason)
		{
			string path = (string)value;
			if (File.Exists(path))
			{
				File.Delete(path);
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0000C6C4 File Offset: 0x0000A8C4
		private string SerializeClientObject(IAsyncUploadResult fileInfo, MetaData metaData)
		{
			string arg = SerializationService.Serialize(fileInfo, this.MaxJsonLength);
			string arg2 = SerializationService.Serialize(metaData, true, this.MaxJsonLength);
			fileInfo.FileName = Path.GetFileName(fileInfo.FileName);
			return string.Format("{{\"fileInfo\":{0}, \"metaData\":\"{1}\" }}", arg, arg2);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0000C70C File Offset: 0x0000A90C
		internal IAsyncUploadConfiguration GetConfiguration(string rawData)
		{
			string[] array = rawData.Split(new char[]
			{
				'&'
			});
			string obj = array[0];
			Type type = Type.GetType(CryptoService.GetService("").Decrypt(array[1]));
			CryptoService.GetService("").CheckWhitelistTypes(type, ConfigurationManager.AppSettings["Telerik.Upload.AllowedCustomMetaDataTypes"], "Telerik.Web.UI.AsyncUploadConfiguration");
			IAsyncUploadConfiguration asyncUploadConfiguration = (IAsyncUploadConfiguration)SerializationService.Deserialize(obj, type, true);
			HmacEnabledCryptoService hmacEnabledCryptoService = new HmacEnabledCryptoService(CryptoService.GetService(""), HmacService.GetService());
			asyncUploadConfiguration.TargetFolder = hmacEnabledCryptoService.Decrypt(asyncUploadConfiguration.TargetFolder);
			asyncUploadConfiguration.TempTargetFolder = hmacEnabledCryptoService.Decrypt(asyncUploadConfiguration.TempTargetFolder);
			return asyncUploadConfiguration;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0000C7BB File Offset: 0x0000A9BB
		internal static bool CheckFileNameForInvalidChars(string fileName)
		{
			return string.IsNullOrEmpty(fileName) || fileName.IndexOfAny(Path.GetInvalidFileNameChars()) > -1;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000C7D5 File Offset: 0x0000A9D5
		protected internal virtual bool CheckOriginalFileNameForInvalidChars(string originalFileName)
		{
			return AsyncUploadHandler.CheckFileNameForInvalidChars(originalFileName);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000C7DD File Offset: 0x0000A9DD
		protected internal virtual string ChangeOriginalFileName(string fileName)
		{
			return fileName;
		}

		// Token: 0x04000099 RID: 153
		internal const string Return_Next_Chunk = "next";

		// Token: 0x0400009A RID: 154
		internal const string Max_File_Size_Exceeded = "{ \"invalidFileSize\" : true }";

		// Token: 0x0400009B RID: 155
		internal const string Invalid_File_Extension = "{ \"invalidFileExtension\" : true }";

		// Token: 0x0400009C RID: 156
		internal const string Meta_Data_String = "metadata";

		// Token: 0x0400009D RID: 157
		internal const string Post_Data_Key = "rauPostData";

		// Token: 0x0400009E RID: 158
		internal const string Temp_Files_Extension = ".tmp";

		// Token: 0x0400009F RID: 159
		internal const string Allowed_Custom_MetaData_Types = "Telerik.Upload.AllowedCustomMetaDataTypes";

		// Token: 0x040000A0 RID: 160
		internal const string uploadMetaDataFullName = "Telerik.Web.UI.AsyncUploadConfiguration";

		// Token: 0x040000A1 RID: 161
		private int _maxJsonLength = 4194304;

		// Token: 0x040000A2 RID: 162
		private string temporaryFileName;
	}
}
