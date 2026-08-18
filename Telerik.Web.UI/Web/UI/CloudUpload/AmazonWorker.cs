using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.Caching;
using Amazon.S3.Model;

namespace Telerik.Web.UI.CloudUpload
{
	// Token: 0x020001BA RID: 442
	internal class AmazonWorker : BaseWorker
	{
		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x0003BB9A File Offset: 0x00039D9A
		// (set) Token: 0x06001055 RID: 4181 RVA: 0x0003BBA2 File Offset: 0x00039DA2
		internal IAmazonS3Provider Provider { get; set; }

		// Token: 0x06001056 RID: 4182 RVA: 0x0003BBAB File Offset: 0x00039DAB
		internal AmazonWorker() : base(null, null)
		{
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x0003BBB8 File Offset: 0x00039DB8
		public AmazonWorker(HttpContext context, ICloudUploadConfiguration configuration, string name, Type type) : base(context, configuration)
		{
			base.GenericProvider = (this.Provider = (AmazonS3Provider)CloudProviderFactory.GetProvider(name, type));
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x0003BBEC File Offset: 0x00039DEC
		public override void PerformChunkUpload()
		{
			base.ResolveFileIdentifier();
			if (string.IsNullOrEmpty(base.RequestMetaData.UploadId))
			{
				base.ResponseMetaData.UploadId = (this.uploadId = this.Provider.InitiateMultiPartUpload(this.FileIdentifier, this.GetCustomMetaData()));
				this.StoreUploadIdInCache(this.FileIdentifier, base.ResponseMetaData.UploadId);
			}
			if (base.RequestMetaData.PartEtags == null || base.RequestMetaData.PartEtags.Length == 0)
			{
				long fileSize = base.CalculateFileSize(5);
				if (base.RequestMetaData.IsLastChunk && !base.IsFileSizeValid(fileSize))
				{
					base.ResponseMetaData.Status = ResponseStatus.SizeValidationFailed;
					return;
				}
				this.UploadChunk();
			}
			else
			{
				this.CommitChunkUpload();
			}
			base.ResponseMetaData.KeyName = this.FileIdentifier;
			base.ResponseMetaData.Status = ResponseStatus.OK;
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0003BCC8 File Offset: 0x00039EC8
		internal virtual void UploadChunk()
		{
			if (string.IsNullOrEmpty(this.uploadId))
			{
				this.uploadId = base.RequestMetaData.UploadId;
			}
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection.Add("uploadId", this.uploadId);
			nameValueCollection.Add("partNumber", base.RequestMetaData.ChunkNumber.ToString());
			nameValueCollection.Add("keyName", this.FileIdentifier);
			this.Provider.UploadChunk(nameValueCollection, base.UploadedFile.InputStream);
			base.ResponseMetaData.PartETag = this.Provider.UploadedPartETag;
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0003BD68 File Offset: 0x00039F68
		internal virtual void CommitChunkUpload()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("uploadId", base.RequestMetaData.UploadId);
			dictionary.Add("keyName", this.FileIdentifier);
			List<PartETag> list = new List<PartETag>();
			for (int i = 0; i < base.RequestMetaData.PartEtags.Length; i++)
			{
				list.Add(new PartETag
				{
					ETag = base.RequestMetaData.PartEtags[i],
					PartNumber = i + 1
				});
			}
			dictionary.Add("partETags", list);
			this.Provider.CommitChunkUpload(dictionary, null);
			base.StoreFileIdentifierInCache(this.FileIdentifier, this.Provider);
			this.RemoveUploadIdFromCache(base.RequestMetaData.UploadId);
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0003BE28 File Offset: 0x0003A028
		protected void StoreUploadIdInCache(string keyName, string uploadId)
		{
			base.Context.Cache.Insert(uploadId, keyName, null, DateTime.UtcNow.Add(this.Provider.UncommitedFilesExpirationPeriod), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, new CacheItemRemovedCallback(this.AbortChunkUploadCallback));
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x0003BE72 File Offset: 0x0003A072
		protected void RemoveUploadIdFromCache(string uploadId)
		{
			base.Context.Cache.Remove(uploadId);
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0003BE86 File Offset: 0x0003A086
		protected void AbortChunkUploadCallback(string uploadId, object keyName, CacheItemRemovedReason reason)
		{
			if (reason == CacheItemRemovedReason.Expired)
			{
				this.Provider.AbortChunktUpload(keyName.ToString(), uploadId);
			}
		}

		// Token: 0x040004A3 RID: 1187
		private const int NumberOfMb = 5;

		// Token: 0x040004A4 RID: 1188
		private string uploadId;
	}
}
