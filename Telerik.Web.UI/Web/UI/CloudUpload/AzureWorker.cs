using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;

namespace Telerik.Web.UI.CloudUpload
{
	// Token: 0x020001BB RID: 443
	internal class AzureWorker : BaseWorker
	{
		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x0003BE9E File Offset: 0x0003A09E
		// (set) Token: 0x0600105F RID: 4191 RVA: 0x0003BEA6 File Offset: 0x0003A0A6
		internal IAzureProvider Provider { get; set; }

		// Token: 0x06001060 RID: 4192 RVA: 0x0003BEAF File Offset: 0x0003A0AF
		internal AzureWorker() : base(null, null)
		{
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0003BEBC File Offset: 0x0003A0BC
		public AzureWorker(HttpContext context, ICloudUploadConfiguration configuration, string name, Type type) : base(context, configuration)
		{
			base.GenericProvider = (this.Provider = (AzureProvider)CloudProviderFactory.GetProvider(name, type));
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0003BEF0 File Offset: 0x0003A0F0
		public override void PerformChunkUpload()
		{
			base.ResolveFileIdentifier();
			if (base.RequestMetaData.IsLastChunk)
			{
				long fileSize = base.CalculateFileSize(2);
				if (!base.IsFileSizeValid(fileSize))
				{
					base.ResponseMetaData.Status = ResponseStatus.SizeValidationFailed;
					return;
				}
			}
			this.UploadChunk();
			if (base.RequestMetaData.IsLastChunk)
			{
				this.CommitChunkUpload();
				base.StoreFileIdentifierInCache(this.FileIdentifier, this.Provider);
			}
			base.ResponseMetaData.KeyName = this.FileIdentifier;
			base.ResponseMetaData.Status = ResponseStatus.OK;
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0003BF78 File Offset: 0x0003A178
		internal virtual void UploadChunk()
		{
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection.Add("partNumber", base.RequestMetaData.ChunkNumber.ToString());
			nameValueCollection.Add("keyName", this.FileIdentifier);
			this.Provider.UploadChunk(nameValueCollection, base.UploadedFile.InputStream);
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x0003BFD4 File Offset: 0x0003A1D4
		internal virtual void CommitChunkUpload()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("lastPartNumber", base.RequestMetaData.ChunkNumber);
			dictionary.Add("keyName", this.FileIdentifier);
			this.Provider.CommitChunkUpload(dictionary, this.GetCustomMetaData());
		}

		// Token: 0x040004A6 RID: 1190
		private const int NumberOfMb = 2;
	}
}
