using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.IO;
using System.Text;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Telerik.Web.UI.CloudUpload;

namespace Telerik.Web.UI
{
	// Token: 0x020001B4 RID: 436
	public class AmazonS3Provider : ProviderBase, IAmazonS3Provider, ICloudStorageProvider, IDisposable
	{
		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x0003A475 File Offset: 0x00038675
		// (set) Token: 0x06000FDC RID: 4060 RVA: 0x0003A47D File Offset: 0x0003867D
		public string AccessKey { get; set; }

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x0003A486 File Offset: 0x00038686
		// (set) Token: 0x06000FDE RID: 4062 RVA: 0x0003A48E File Offset: 0x0003868E
		public string SecretKey { get; set; }

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x0003A497 File Offset: 0x00038697
		// (set) Token: 0x06000FE0 RID: 4064 RVA: 0x0003A49F File Offset: 0x0003869F
		public string BucketName { get; set; }

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x0003A4A8 File Offset: 0x000386A8
		// (set) Token: 0x06000FE2 RID: 4066 RVA: 0x0003A4B0 File Offset: 0x000386B0
		public string SubFolderStructure { get; set; }

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06000FE3 RID: 4067 RVA: 0x0003A4B9 File Offset: 0x000386B9
		// (set) Token: 0x06000FE4 RID: 4068 RVA: 0x0003A4C1 File Offset: 0x000386C1
		public bool EnsureContainer { get; set; }

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x0003A4CA File Offset: 0x000386CA
		// (set) Token: 0x06000FE6 RID: 4070 RVA: 0x0003A4D2 File Offset: 0x000386D2
		public TimeSpan UncommitedFilesExpirationPeriod { get; set; }

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x0003A4DB File Offset: 0x000386DB
		// (set) Token: 0x06000FE8 RID: 4072 RVA: 0x0003A4E3 File Offset: 0x000386E3
		public string UploadedPartETag
		{
			get
			{
				return this._uploadedPartETag;
			}
			set
			{
				this._uploadedPartETag = value;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x0003A4EC File Offset: 0x000386EC
		// (set) Token: 0x06000FEA RID: 4074 RVA: 0x0003A4F4 File Offset: 0x000386F4
		protected internal virtual AmazonS3Client AmazonS3Client
		{
			get
			{
				return this._s3Client;
			}
			set
			{
				this._s3Client = value;
			}
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x0003A500 File Offset: 0x00038700
		public virtual void UploadFile(string keyName, NameValueCollection metaData, Stream fileStream)
		{
			PutObjectRequest putObjectRequest = new PutObjectRequest
			{
				Key = keyName,
				BucketName = this.BucketName,
				InputStream = fileStream
			};
			foreach (object obj in metaData)
			{
				string text = (string)obj;
				string text2 = metaData[text];
				byte[] bytes = Encoding.UTF8.GetBytes(text2);
				if (bytes.Length != text2.Length)
				{
					text2 = Convert.ToBase64String(bytes);
				}
				putObjectRequest.Metadata.Add(text, text2);
			}
			try
			{
				this._s3Client.PutObject(putObjectRequest);
			}
			catch (AmazonS3Exception innerException)
			{
				string message = string.Format("Exception thrown for upload operation for file with keyName: {0}", keyName);
				throw new CloudUploadProviderException(message, innerException, keyName, this.BucketName);
			}
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x0003A5EC File Offset: 0x000387EC
		public virtual void UploadChunk(NameValueCollection config, Stream fileStream)
		{
			UploadPartRequest uploadPartRequest = new UploadPartRequest();
			string text = config["uploadId"];
			string s = config["partNumber"];
			string text2 = config["keyName"];
			uploadPartRequest.BucketName = this.BucketName;
			uploadPartRequest.UploadId = text;
			uploadPartRequest.PartNumber = int.Parse(s);
			uploadPartRequest.Key = text2;
			uploadPartRequest.InputStream = fileStream;
			try
			{
				UploadPartResponse uploadPartResponse = this._s3Client.UploadPart(uploadPartRequest);
				this._uploadedPartETag = uploadPartResponse.ETag;
			}
			catch (AmazonS3Exception innerException)
			{
				string message = string.Format("Part Upload with ID: {1} throw exception for upload operation for file with keyName: {0} located in bucket: {2}", text2, text, this.BucketName);
				throw new CloudUploadProviderException(message, innerException, text2, this.BucketName, text);
			}
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x0003A6A8 File Offset: 0x000388A8
		public virtual void DeleteFile(string keyName)
		{
			DeleteObjectRequest deleteObjectRequest = new DeleteObjectRequest();
			deleteObjectRequest.Key = keyName;
			deleteObjectRequest.BucketName = this.BucketName;
			try
			{
				this._s3Client.DeleteObject(deleteObjectRequest);
			}
			catch (AmazonS3Exception innerException)
			{
				string message = string.Format("Exception thrown for file delete operation with parameters: KeyName:{0} and BucketName:{1}", keyName, this.BucketName);
				throw new CloudUploadProviderException(message, innerException, keyName, this.BucketName);
			}
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x0003A710 File Offset: 0x00038910
		public virtual void CommitChunkUpload(IDictionary<string, object> config, NameValueCollection metaData)
		{
			CompleteMultipartUploadRequest completeMultipartUploadRequest = new CompleteMultipartUploadRequest();
			string text = config["uploadId"].ToString();
			string text2 = config["keyName"].ToString();
			completeMultipartUploadRequest.BucketName = this.BucketName;
			completeMultipartUploadRequest.UploadId = text;
			completeMultipartUploadRequest.Key = text2;
			completeMultipartUploadRequest.PartETags = (List<PartETag>)config["partETags"];
			try
			{
				this._s3Client.CompleteMultipartUpload(completeMultipartUploadRequest);
			}
			catch (AmazonS3Exception innerException)
			{
				string message = string.Format("Exception thrown for commit multipart upload operation with parameters: UploadId {0}, KeyName:{1} and BucketName:{2}", text, text2, this.BucketName);
				throw new CloudUploadProviderException(message, innerException, text2, this.BucketName, text);
			}
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0003A7BC File Offset: 0x000389BC
		public virtual void EnsureStorageContainer()
		{
			if (this.EnsureContainer)
			{
				this.CreateBucket();
			}
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0003A7CC File Offset: 0x000389CC
		public virtual string InitiateMultiPartUpload(string keyName, NameValueCollection metaData)
		{
			InitiateMultipartUploadRequest initiateMultipartUploadRequest = new InitiateMultipartUploadRequest();
			initiateMultipartUploadRequest.BucketName = this.BucketName;
			initiateMultipartUploadRequest.Key = keyName;
			foreach (object obj in metaData)
			{
				string text = (string)obj;
				initiateMultipartUploadRequest.Metadata.Add(text, metaData[text]);
			}
			InitiateMultipartUploadResponse initiateMultipartUploadResponse;
			try
			{
				initiateMultipartUploadResponse = this._s3Client.InitiateMultipartUpload(initiateMultipartUploadRequest);
			}
			catch (AmazonS3Exception innerException)
			{
				throw new CloudUploadProviderException("Exception thrown for initiate multi part upload operation", innerException);
			}
			return initiateMultipartUploadResponse.UploadId;
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0003A87C File Offset: 0x00038A7C
		public virtual void AbortChunktUpload(string keyName, string uploadId)
		{
			AbortMultipartUploadRequest abortMultipartUploadRequest = new AbortMultipartUploadRequest();
			abortMultipartUploadRequest.BucketName = this.BucketName;
			abortMultipartUploadRequest.UploadId = uploadId;
			abortMultipartUploadRequest.Key = keyName;
			try
			{
				this._s3Client.AbortMultipartUpload(abortMultipartUploadRequest);
			}
			catch (AmazonS3Exception innerException)
			{
				string message = string.Format("Exception thrown for abort multipart upload operation with parameters: UploadId {0}, KeyName:{1} and BucketName:{2}", uploadId, keyName, this.BucketName);
				throw new CloudUploadProviderException(message, innerException, keyName, this.BucketName, uploadId);
			}
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x0003A8EC File Offset: 0x00038AEC
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._s3Client.Dispose();
			}
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0003A8FC File Offset: 0x00038AFC
		public override void Initialize(string name, NameValueCollection config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("No valid configuration is provided.");
			}
			base.Initialize(name, config);
			this.AccessKey = config["accessKey"];
			if (string.IsNullOrEmpty(this.AccessKey))
			{
				throw new ProviderException("Missing AccessKey. Please specify it with the accessKey property.");
			}
			this.SecretKey = config["secretKey"];
			if (string.IsNullOrEmpty(this.SecretKey))
			{
				throw new ProviderException("Missing SecretKey. Please specify it with the secretKey property.");
			}
			this.BucketName = config["bucketName"];
			if (string.IsNullOrEmpty(this.BucketName))
			{
				throw new ProviderException("Missing Container Name. Please specify it with the containerName property.");
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
			if (!string.IsNullOrEmpty(config["uncommitedFilesExpirationPeriod"]))
			{
				this.UncommitedFilesExpirationPeriod = TimeSpan.Parse(config["uncommitedFilesExpirationPeriod"]);
				return;
			}
			this.UncommitedFilesExpirationPeriod = TimeSpan.FromHours(4.0);
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x0003AA50 File Offset: 0x00038C50
		public virtual void EnsureWebClient()
		{
			if (this._s3Client == null)
			{
				try
				{
					this._s3Client = new AmazonS3Client(this.AccessKey, this.SecretKey, RegionEndpoint.USEast1);
				}
				catch (AmazonS3Exception innerException)
				{
					throw new CloudUploadProviderException("Exception thrown for Amazon Client initialization operation", innerException);
				}
			}
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0003AAA0 File Offset: 0x00038CA0
		public virtual void CreateBucket()
		{
			PutBucketRequest putBucketRequest = new PutBucketRequest();
			putBucketRequest.BucketName = this.BucketName;
			try
			{
				this._s3Client.PutBucket(putBucketRequest);
			}
			catch (AmazonS3Exception innerException)
			{
				throw new CloudUploadProviderException("Exception thrown for bucket creation operation", innerException);
			}
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0003AAEC File Offset: 0x00038CEC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0400047F RID: 1151
		private AmazonS3Client _s3Client;

		// Token: 0x04000480 RID: 1152
		private string _uploadedPartETag;
	}
}
