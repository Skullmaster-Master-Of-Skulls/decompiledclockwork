using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.CloudUpload
{
	// Token: 0x020001B9 RID: 441
	internal abstract class BaseWorker : IDisposable
	{
		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001029 RID: 4137 RVA: 0x0003B5BC File Offset: 0x000397BC
		// (set) Token: 0x0600102A RID: 4138 RVA: 0x0003B5C4 File Offset: 0x000397C4
		internal ICloudStorageProvider GenericProvider { get; set; }

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x0600102B RID: 4139 RVA: 0x0003B5CD File Offset: 0x000397CD
		// (set) Token: 0x0600102C RID: 4140 RVA: 0x0003B5D5 File Offset: 0x000397D5
		internal HttpContext Context { get; set; }

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x0003B5DE File Offset: 0x000397DE
		// (set) Token: 0x0600102E RID: 4142 RVA: 0x0003B5E6 File Offset: 0x000397E6
		internal virtual string FileIdentifier { get; set; }

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x0600102F RID: 4143 RVA: 0x0003B5EF File Offset: 0x000397EF
		// (set) Token: 0x06001030 RID: 4144 RVA: 0x0003B5F7 File Offset: 0x000397F7
		internal ICloudUploadConfiguration Configuration { get; set; }

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x0003B600 File Offset: 0x00039800
		// (set) Token: 0x06001032 RID: 4146 RVA: 0x0003B608 File Offset: 0x00039808
		internal IRequestMetaData RequestMetaData { get; set; }

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001033 RID: 4147 RVA: 0x0003B611 File Offset: 0x00039811
		// (set) Token: 0x06001034 RID: 4148 RVA: 0x0003B619 File Offset: 0x00039819
		internal IResponseMetaData ResponseMetaData { get; set; }

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001035 RID: 4149 RVA: 0x0003B622 File Offset: 0x00039822
		// (set) Token: 0x06001036 RID: 4150 RVA: 0x0003B62A File Offset: 0x0003982A
		internal HttpPostedFile UploadedFile { get; set; }

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x0003B633 File Offset: 0x00039833
		internal JavaScriptSerializer Serializer
		{
			get
			{
				if (this._serializer == null)
				{
					this._serializer = new JavaScriptSerializer();
				}
				return this._serializer;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001038 RID: 4152 RVA: 0x0003B64E File Offset: 0x0003984E
		internal EventHandlerList Events
		{
			get
			{
				if (this._events == null)
				{
					this._events = new EventHandlerList();
				}
				return this._events;
			}
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x0003B669 File Offset: 0x00039869
		public BaseWorker(HttpContext context, ICloudUploadConfiguration configuration)
		{
			this.Context = context;
			this.Configuration = configuration;
			this.ResponseMetaData = new ResponseMetaData();
		}

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x0600103A RID: 4154 RVA: 0x0003B68A File Offset: 0x0003988A
		// (remove) Token: 0x0600103B RID: 4155 RVA: 0x0003B69D File Offset: 0x0003989D
		protected internal event SetMetaDataEventHandler MetaDataSetup
		{
			add
			{
				this.Events.AddHandler(BaseWorker.MetaDataSetupEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(BaseWorker.MetaDataSetupEvent, value);
			}
		}

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x0600103C RID: 4156 RVA: 0x0003B6B0 File Offset: 0x000398B0
		// (remove) Token: 0x0600103D RID: 4157 RVA: 0x0003B6C3 File Offset: 0x000398C3
		protected internal event SetKeyNameEventHandler KeyNameSetup
		{
			add
			{
				this.Events.AddHandler(BaseWorker.KeyNameSetupEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(BaseWorker.KeyNameSetupEvent, value);
			}
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x0003B6D8 File Offset: 0x000398D8
		protected internal virtual void OnMetaDataSetup(SetMetaDataEventArgs e)
		{
			SetMetaDataEventHandler setMetaDataEventHandler = (SetMetaDataEventHandler)this.Events[BaseWorker.MetaDataSetupEvent];
			if (setMetaDataEventHandler != null)
			{
				setMetaDataEventHandler(this, e);
			}
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x0003B708 File Offset: 0x00039908
		protected internal virtual void OnKeyNameSetup(SetKeyNameEventArgs e)
		{
			SetKeyNameEventHandler setKeyNameEventHandler = (SetKeyNameEventHandler)this.Events[BaseWorker.KeyNameSetupEvent];
			if (setKeyNameEventHandler != null)
			{
				setKeyNameEventHandler(this, e);
			}
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x0003B736 File Offset: 0x00039936
		public void Process()
		{
			if (!this.IsFileExtensionValid())
			{
				this.ResponseMetaData.Status = ResponseStatus.ExtensionValidationFailed;
				return;
			}
			this.GenericProvider.EnsureStorageContainer();
			if (this.RequestMetaData.IsSingleUpload)
			{
				this.PerformSingleRequestUpload();
				return;
			}
			this.PerformChunkUpload();
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x0003B774 File Offset: 0x00039974
		public virtual void PerformSingleRequestUpload()
		{
			if (this.IsFileSizeValid(this.UploadedFile.InputStream.Length))
			{
				string keyName = this.GetKeyName(this.GenericProvider.SubFolderStructure);
				this.GenericProvider.UploadFile(keyName, this.GetCustomMetaData(), this.UploadedFile.InputStream);
				this.ResponseMetaData.KeyName = this.FileIdentifier;
				this.ResponseMetaData.Status = ResponseStatus.OK;
				this.ResponseMetaData.ContentType = this.GetContentType();
				this.ResponseMetaData.ContentLength = this.UploadedFile.InputStream.Length;
				this.StoreFileIdentifierInCache(this.FileIdentifier, this.GenericProvider);
				return;
			}
			this.ResponseMetaData.Status = ResponseStatus.SizeValidationFailed;
		}

		// Token: 0x06001042 RID: 4162
		public abstract void PerformChunkUpload();

		// Token: 0x06001043 RID: 4163 RVA: 0x0003B833 File Offset: 0x00039A33
		public void EnsureData()
		{
			this.EnsureRequestMetaData();
			this.EnsureUploadedFile();
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x0003B841 File Offset: 0x00039A41
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x0003B850 File Offset: 0x00039A50
		protected bool IsFileSizeValid(long fileSize)
		{
			return fileSize <= this.Configuration.MaxFileSize || this.Configuration.MaxFileSize == 0L;
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0003B874 File Offset: 0x00039A74
		protected bool IsFileExtensionValid()
		{
			bool result = true;
			string fileExtension = this.GetFileExtension();
			if (this.Configuration.AllowedFileExtensions != null && this.Configuration.AllowedFileExtensions.Length > 0)
			{
				result = false;
				foreach (string text in this.Configuration.AllowedFileExtensions)
				{
					if (text.Trim(new char[]
					{
						'.'
					}).Equals(fileExtension.Trim(new char[]
					{
						'.'
					}), StringComparison.InvariantCultureIgnoreCase))
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0003B905 File Offset: 0x00039B05
		protected internal virtual void EnsureUploadedFile()
		{
			if (this.RequestMetaData.PartEtags == null || this.RequestMetaData.PartEtags.Length == 0)
			{
				this.UploadedFile = this.Context.Request.Files[0];
			}
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x0003B940 File Offset: 0x00039B40
		protected internal virtual void EnsureRequestMetaData()
		{
			string input = this.Context.Request["rcuPostData"];
			this.RequestMetaData = this.Serializer.Deserialize<RequestMetaData>(input);
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x0003B975 File Offset: 0x00039B75
		protected internal virtual string GetFileName()
		{
			return Path.GetFileName(this.RequestMetaData.OriginalName);
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x0003B987 File Offset: 0x00039B87
		protected internal virtual string GetFileExtension()
		{
			return Path.GetExtension(this.RequestMetaData.OriginalName);
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x0003B999 File Offset: 0x00039B99
		protected void ResolveFileIdentifier()
		{
			if (string.IsNullOrEmpty(this.RequestMetaData.KeyName))
			{
				this.FileIdentifier = this.GetKeyName(this.GenericProvider.SubFolderStructure);
				return;
			}
			this.FileIdentifier = this.RequestMetaData.KeyName;
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x0003B9D8 File Offset: 0x00039BD8
		protected virtual string GetKeyName(string subFolderStructure)
		{
			string fileName = this.GetFileName();
			SetKeyNameEventArgs setKeyNameEventArgs = new SetKeyNameEventArgs(fileName, subFolderStructure);
			this.OnKeyNameSetup(setKeyNameEventArgs);
			this.FileIdentifier = setKeyNameEventArgs.KeyName;
			return setKeyNameEventArgs.KeyName;
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x0003BA10 File Offset: 0x00039C10
		protected virtual NameValueCollection GetCustomMetaData()
		{
			NameValueCollection metaData = new NameValueCollection();
			SetMetaDataEventArgs setMetaDataEventArgs = new SetMetaDataEventArgs
			{
				Context = this.Context,
				MetaData = metaData
			};
			this.OnMetaDataSetup(setMetaDataEventArgs);
			return setMetaDataEventArgs.MetaData;
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x0003BA4C File Offset: 0x00039C4C
		protected void StoreFileIdentifierInCache(string keyName, ICloudStorageProvider provider)
		{
			this.Context.Cache.Insert(keyName, provider, null, DateTime.UtcNow.Add(provider.UncommitedFilesExpirationPeriod), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, new CacheItemRemovedCallback(this.RemoveUploadedFileCallback));
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x0003BA91 File Offset: 0x00039C91
		protected void RemoveUploadedFileCallback(string key, object value, CacheItemRemovedReason reason)
		{
			if (reason == CacheItemRemovedReason.Expired)
			{
				(value as ICloudStorageProvider).DeleteFile(key);
			}
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x0003BAA4 File Offset: 0x00039CA4
		protected long CalculateFileSize(int numberOfMB)
		{
			long num = (long)numberOfMB * (long)Math.Pow(2.0, 20.0);
			return (long)(this.RequestMetaData.ChunkNumber - 1) * num + (long)this.UploadedFile.ContentLength;
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x0003BAEC File Offset: 0x00039CEC
		protected string GetContentType()
		{
			string text = this.GetFileExtension().ToLowerInvariant().TrimStart(new char[]
			{
				'.'
			});
			string result = string.Empty;
			if (this.UploadedFile.ContentType == "application/octet-stream" && text != ".a")
			{
				if (MimeTypes.Types.ContainsKey(text))
				{
					result = MimeTypes.Types[text];
				}
			}
			else
			{
				result = this.UploadedFile.ContentType;
			}
			return result;
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x0003BB69 File Offset: 0x00039D69
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Events.Dispose();
				this._events.Dispose();
			}
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x0003BB84 File Offset: 0x00039D84
		// Note: this type is marked as 'beforefieldinit'.
		static BaseWorker()
		{
			BaseWorker.MetaDataSetupEvent = new object();
			BaseWorker.KeyNameSetupEvent = new object();
		}

		// Token: 0x04000498 RID: 1176
		private JavaScriptSerializer _serializer;

		// Token: 0x04000499 RID: 1177
		private EventHandlerList _events;
	}
}
