using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.IO;
using Telerik.Everlive.Sdk.Core;
using Telerik.Everlive.Sdk.Core.Model.Result;
using Telerik.Everlive.Sdk.Core.Query.Definition.FormData;
using Telerik.Everlive.Sdk.Core.Transport;
using Telerik.Web.UI.CloudUpload;

namespace Telerik.Web.UI
{
	// Token: 0x020001B8 RID: 440
	public class EverliveProvider : ProviderBase, ICloudStorageProvider
	{
		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001019 RID: 4121 RVA: 0x0003B3A2 File Offset: 0x000395A2
		// (set) Token: 0x0600101A RID: 4122 RVA: 0x0003B3AA File Offset: 0x000395AA
		public string ApiKey { get; set; }

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x0600101B RID: 4123 RVA: 0x0003B3B3 File Offset: 0x000395B3
		public string SubFolderStructure
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x0600101C RID: 4124 RVA: 0x0003B3BA File Offset: 0x000395BA
		// (set) Token: 0x0600101D RID: 4125 RVA: 0x0003B3C2 File Offset: 0x000395C2
		public TimeSpan UncommitedFilesExpirationPeriod { get; set; }

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x0600101E RID: 4126 RVA: 0x0003B3CB File Offset: 0x000395CB
		public string FileID
		{
			get
			{
				return this._fileID;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x0600101F RID: 4127 RVA: 0x0003B3D3 File Offset: 0x000395D3
		// (set) Token: 0x06001020 RID: 4128 RVA: 0x0003B3DB File Offset: 0x000395DB
		[CLSCompliant(false)]
		protected internal virtual EverliveApp EverliveAppClient
		{
			get
			{
				return this._everliveApp;
			}
			set
			{
				this._everliveApp = value;
			}
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x0003B3E4 File Offset: 0x000395E4
		public virtual void UploadFile(string keyName, NameValueCollection metaData, Stream fileStream)
		{
			try
			{
				FileField fileField = new FileField
				{
					FileName = keyName,
					ContentType = metaData["contentType"],
					InputStream = fileStream
				};
				CreateResultItem createResultItem = this._everliveApp.WorkWith().Files().Upload(fileField).ExecuteSync(-1);
				this._fileID = createResultItem.Id.ToString();
			}
			catch (EverliveException innerException)
			{
				throw new CloudUploadProviderException("Exception thrown for file upload operation", innerException);
			}
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x0003B470 File Offset: 0x00039670
		public void UploadChunk(NameValueCollection config, Stream fileStream)
		{
			throw new NotImplementedException("Everlive provider does not support chunk upload.");
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x0003B47C File Offset: 0x0003967C
		public virtual void DeleteFile(string fileID)
		{
			try
			{
				Guid guid = new Guid(fileID);
				this._everliveApp.WorkWith().Files().Delete(guid).ExecuteSync(-1);
			}
			catch (EverliveException innerException)
			{
				throw new CloudUploadProviderException("Exception thrown for file delete operation", innerException);
			}
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x0003B4D0 File Offset: 0x000396D0
		public void CommitChunkUpload(IDictionary<string, object> config, NameValueCollection metaData)
		{
			throw new NotImplementedException("Everlive provider does not support chunk upload.");
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x0003B4DC File Offset: 0x000396DC
		public void EnsureStorageContainer()
		{
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x0003B4E0 File Offset: 0x000396E0
		public override void Initialize(string name, NameValueCollection config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("No valid configuration is provided.");
			}
			base.Initialize(name, config);
			this.ApiKey = config["apiKey"];
			if (string.IsNullOrEmpty(this.ApiKey))
			{
				throw new ProviderException("Missing API Key. Please specify it with the apiKey property.");
			}
			if (!string.IsNullOrEmpty(config["uncommitedFilesExpirationPeriod"]))
			{
				this.UncommitedFilesExpirationPeriod = TimeSpan.Parse(config["uncommitedFilesExpirationPeriod"]);
				return;
			}
			this.UncommitedFilesExpirationPeriod = TimeSpan.FromHours(4.0);
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0003B56C File Offset: 0x0003976C
		public virtual void EnsureWebClient()
		{
			if (this._everliveApp == null)
			{
				try
				{
					this._everliveApp = new EverliveApp(this.ApiKey);
				}
				catch (EverliveException innerException)
				{
					throw new CloudUploadProviderException("Exception thrown for Everlive Application initialization operation", innerException);
				}
			}
		}

		// Token: 0x04000494 RID: 1172
		private EverliveApp _everliveApp;

		// Token: 0x04000495 RID: 1173
		private string _fileID;
	}
}
