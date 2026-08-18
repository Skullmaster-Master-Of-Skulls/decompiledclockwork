using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Telerik.Web.UI.CloudUpload;

namespace Telerik.Web.UI
{
	// Token: 0x020001B5 RID: 437
	public class AzureProvider : ProviderBase, IAzureProvider, ICloudStorageProvider
	{
		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06000FF8 RID: 4088 RVA: 0x0003AB03 File Offset: 0x00038D03
		// (set) Token: 0x06000FF9 RID: 4089 RVA: 0x0003AB0B File Offset: 0x00038D0B
		public string AccountKey { get; set; }

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06000FFA RID: 4090 RVA: 0x0003AB14 File Offset: 0x00038D14
		// (set) Token: 0x06000FFB RID: 4091 RVA: 0x0003AB1C File Offset: 0x00038D1C
		public string AccountName { get; set; }

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06000FFC RID: 4092 RVA: 0x0003AB25 File Offset: 0x00038D25
		// (set) Token: 0x06000FFD RID: 4093 RVA: 0x0003AB2D File Offset: 0x00038D2D
		public string BlobContainer { get; set; }

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06000FFE RID: 4094 RVA: 0x0003AB36 File Offset: 0x00038D36
		// (set) Token: 0x06000FFF RID: 4095 RVA: 0x0003AB3E File Offset: 0x00038D3E
		public string SubFolderStructure { get; set; }

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001000 RID: 4096 RVA: 0x0003AB47 File Offset: 0x00038D47
		// (set) Token: 0x06001001 RID: 4097 RVA: 0x0003AB4F File Offset: 0x00038D4F
		public bool EnsureContainer { get; set; }

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001002 RID: 4098 RVA: 0x0003AB58 File Offset: 0x00038D58
		// (set) Token: 0x06001003 RID: 4099 RVA: 0x0003AB60 File Offset: 0x00038D60
		public TimeSpan UncommitedFilesExpirationPeriod { get; set; }

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001004 RID: 4100 RVA: 0x0003AB69 File Offset: 0x00038D69
		// (set) Token: 0x06001005 RID: 4101 RVA: 0x0003AB71 File Offset: 0x00038D71
		public string DefaultEndpointsProtocol { get; set; }

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001006 RID: 4102 RVA: 0x0003AB7A File Offset: 0x00038D7A
		// (set) Token: 0x06001007 RID: 4103 RVA: 0x0003AB82 File Offset: 0x00038D82
		[CLSCompliant(false)]
		protected internal virtual CloudBlobContainer StorageContainer
		{
			get
			{
				return this._container;
			}
			set
			{
				this._container = value;
			}
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x0003AB8C File Offset: 0x00038D8C
		public virtual void UploadFile(string keyName, NameValueCollection metaData, Stream fileStream)
		{
			long num = 64L * (long)Math.Pow(2.0, 20.0);
			if (fileStream.Length >= num)
			{
				this.UploadFileOnChunks(keyName, metaData, fileStream);
				return;
			}
			this.UploadFileOnSigleRequest(keyName, metaData, fileStream);
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x0003ABD4 File Offset: 0x00038DD4
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public virtual void UploadChunk(NameValueCollection config, Stream fileStream)
		{
			string text = config["keyName"];
			string s = config["partNumber"];
			try
			{
				CloudBlockBlob blockBlobReference = this._container.GetBlockBlobReference(text);
				string text2 = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format(CultureInfo.InvariantCulture, "{0:D4}", new object[]
				{
					int.Parse(s)
				})));
				blockBlobReference.PutBlock(text2, fileStream, null, null, null, null);
			}
			catch (Exception innerException)
			{
				string message = string.Format("Exception thrown for part upload operation for file with keyName: {0} located in blobContainer: {1}", text, this.BlobContainer);
				throw new CloudUploadProviderException(message, innerException, text, this.BlobContainer);
			}
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x0003AC84 File Offset: 0x00038E84
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public virtual void DeleteFile(string keyName)
		{
			CloudBlockBlob blockBlobReference = this._container.GetBlockBlobReference(keyName);
			try
			{
				blockBlobReference.Delete(0, null, null, null);
			}
			catch (Exception innerException)
			{
				string message = string.Format("Exception thrown for file delete operation with parameters: KeyName:{0} and BlobContainerName:{1}", keyName, this.BlobContainer);
				throw new CloudUploadProviderException(message, innerException, keyName, this.BlobContainer);
			}
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x0003AD18 File Offset: 0x00038F18
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public virtual void CommitChunkUpload(IDictionary<string, object> config, NameValueCollection metaData)
		{
			string text = config["keyName"].ToString();
			string s = config["lastPartNumber"].ToString();
			try
			{
				CloudBlockBlob blockBlobReference = this._container.GetBlockBlobReference(text);
				List<string> list = Enumerable.Range(1, int.Parse(s)).ToList<int>().ConvertAll<string>((int rangeElement) => Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format(CultureInfo.InvariantCulture, "{0:D4}", new object[]
				{
					rangeElement
				}))));
				blockBlobReference.PutBlockList(list, null, null, null);
				for (int i = 0; i < metaData.AllKeys.Length; i++)
				{
					string key = metaData.GetKey(i);
					blockBlobReference.Metadata.Add(key, metaData[key]);
				}
				blockBlobReference.SetMetadata(null, null, null);
			}
			catch (Exception innerException)
			{
				string message = string.Format("Exception thrown for commit chunk upload operation with parameters: KeyName:{0} and BlobContainerName:{1}", text, this.BlobContainer);
				throw new CloudUploadProviderException(message, innerException, text, this.BlobContainer);
			}
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x0003AE0C File Offset: 0x0003900C
		public virtual void EnsureStorageContainer()
		{
			if (this.EnsureContainer)
			{
				this.CreateContainer();
			}
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x0003AE58 File Offset: 0x00039058
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public virtual void UploadFileOnChunks(string keyName, NameValueCollection metaData, Stream fileStream)
		{
			long num = 2L * (long)Math.Pow(2.0, 20.0);
			int num2 = 1;
			byte[] array = new byte[num];
			try
			{
				CloudBlockBlob blockBlobReference = this._container.GetBlockBlobReference(keyName);
				try
				{
					long num3 = fileStream.Length / num;
					long num4 = fileStream.Length % num;
					while (fileStream.Read(array, 0, array.Length) > 0)
					{
						MemoryStream memoryStream = new MemoryStream(array);
						string text = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format(CultureInfo.InvariantCulture, "{0:D4}", new object[]
						{
							num2
						})));
						blockBlobReference.PutBlock(text, memoryStream, null, null, null, null);
						num2++;
						if ((long)num2 > num3)
						{
							array = new byte[num4];
						}
					}
					num2--;
					List<string> list = Enumerable.Range(1, num2).ToList<int>().ConvertAll<string>((int rangeElement) => Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format(CultureInfo.InvariantCulture, "{0:D4}", new object[]
					{
						rangeElement
					}))));
					blockBlobReference.PutBlockList(list, null, null, null);
					for (int i = 0; i < metaData.AllKeys.Length; i++)
					{
						string key = metaData.GetKey(i);
						blockBlobReference.Metadata.Add(key, metaData[key]);
					}
					blockBlobReference.SetMetadata(null, null, null);
				}
				finally
				{
					if (fileStream != null)
					{
						((IDisposable)fileStream).Dispose();
					}
				}
			}
			catch (Exception innerException)
			{
				string message = string.Format("Exception thrown for upload operation for file with keyName: {0}", keyName);
				throw new CloudUploadProviderException(message, innerException, keyName, this.BlobContainer);
			}
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x0003B008 File Offset: 0x00039208
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public virtual void UploadFileOnSigleRequest(string keyName, NameValueCollection metaData, Stream fileStream)
		{
			CloudBlockBlob blockBlobReference = this._container.GetBlockBlobReference(keyName);
			try
			{
				blockBlobReference.UploadFromStream(fileStream, null, null, null);
				for (int i = 0; i < metaData.AllKeys.Length; i++)
				{
					string key = metaData.GetKey(i);
					string text = metaData[key];
					byte[] bytes = Encoding.UTF8.GetBytes(text);
					if (bytes.Length != text.Length)
					{
						text = Convert.ToBase64String(bytes);
					}
					blockBlobReference.Metadata.Add(key, text);
				}
				blockBlobReference.SetMetadata(null, null, null);
			}
			catch (Exception innerException)
			{
				string message = string.Format("Exception thrown for upload operation for file with keyName: {0}", keyName);
				throw new CloudUploadProviderException(message, innerException, keyName, this.BlobContainer);
			}
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x0003B0B8 File Offset: 0x000392B8
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public virtual void CreateContainer()
		{
			try
			{
				this._container.CreateIfNotExists(null, null);
			}
			catch (Exception innerException)
			{
				throw new CloudUploadProviderException("Exception thrown for create blob container operation", innerException);
			}
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x0003B0F4 File Offset: 0x000392F4
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public virtual void EnsureWebClient()
		{
			if (this._blobClient == null)
			{
				try
				{
					bool flag = this.DefaultEndpointsProtocol == "https";
					CloudStorageAccount cloudStorageAccount = new CloudStorageAccount(new StorageCredentials(this.AccountName, this.AccountKey), flag);
					this._blobClient = cloudStorageAccount.CreateCloudBlobClient();
					this._container = this._blobClient.GetContainerReference(this.BlobContainer);
				}
				catch (InvalidOperationException innerException)
				{
					throw new CloudUploadProviderException("Exception thrown for Azure Client initialization operation", innerException);
				}
			}
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0003B17C File Offset: 0x0003937C
		public override void Initialize(string name, NameValueCollection config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("No valid configuration is provided.");
			}
			base.Initialize(name, config);
			this.AccountKey = config["accountKey"];
			if (string.IsNullOrEmpty(this.AccountKey))
			{
				throw new ProviderException("Missing AccountKey. Please specify it with the accountKey property.");
			}
			this.AccountName = config["accountName"];
			if (string.IsNullOrEmpty(this.AccountName))
			{
				throw new ProviderException("Missing AccountName. Please specify it with the accountName property.");
			}
			this.BlobContainer = config["blobContainer"];
			if (string.IsNullOrEmpty(this.BlobContainer))
			{
				throw new ProviderException("Missing BlobContainer. Please specify it with the blobContainer property.");
			}
			this.SubFolderStructure = config["subFolderStructure"];
			if (!string.IsNullOrEmpty(this.SubFolderStructure) && !this.SubFolderStructure.EndsWith("/"))
			{
				this.SubFolderStructure += "/";
			}
			if (!string.IsNullOrEmpty(config["ensureContainer"]))
			{
				bool flag;
				this.EnsureContainer = bool.TryParse(config["ensureContainer"], out flag);
			}
			else
			{
				this.EnsureContainer = false;
			}
			string text = config["defaultEndpointsProtocol"];
			if (text != null && text.ToLower() == "http")
			{
				this.DefaultEndpointsProtocol = text;
			}
			else
			{
				this.DefaultEndpointsProtocol = "https";
			}
			if (!string.IsNullOrEmpty(config["uncommitedFilesExpirationPeriod"]))
			{
				this.UncommitedFilesExpirationPeriod = TimeSpan.Parse(config["uncommitedFilesExpirationPeriod"]);
				return;
			}
			this.UncommitedFilesExpirationPeriod = TimeSpan.FromHours(4.0);
		}

		// Token: 0x04000487 RID: 1159
		private CloudBlobClient _blobClient;

		// Token: 0x04000488 RID: 1160
		private CloudBlobContainer _container;
	}
}
