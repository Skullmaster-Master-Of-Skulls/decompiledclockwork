using System;
using System.Collections.Specialized;
using System.IO;
using Telerik.Web.UI.AsyncUpload;

namespace Telerik.Web.UI
{
	// Token: 0x02000071 RID: 113
	public class AsyncPostedFile : UploadedFile
	{
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x0000BACF File Offset: 0x00009CCF
		public override long ContentLength
		{
			get
			{
				return this._contentLength;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x0000BAD7 File Offset: 0x00009CD7
		// (set) Token: 0x0600048F RID: 1167 RVA: 0x0000BADF File Offset: 0x00009CDF
		public override string FileName
		{
			get
			{
				return this._fileName;
			}
			internal set
			{
				this._fileName = value;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x0000BAE8 File Offset: 0x00009CE8
		public override string ContentType
		{
			get
			{
				return this._contentType;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x0000BAF0 File Offset: 0x00009CF0
		// (set) Token: 0x06000492 RID: 1170 RVA: 0x0000BAF8 File Offset: 0x00009CF8
		internal bool ChunkRequest
		{
			get
			{
				return this._chunkRequest;
			}
			set
			{
				this._chunkRequest = value;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0000BB01 File Offset: 0x00009D01
		public override Stream InputStream
		{
			get
			{
				if (this.ChunkRequest)
				{
					return File.OpenRead(this.Path);
				}
				return this.PostedFile.InputStream;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x0000BB22 File Offset: 0x00009D22
		protected internal override string InputFieldName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0000BB29 File Offset: 0x00009D29
		public AsyncPostedFile(UploadedFile postedFile, string fullPath, long contentLength, bool chunkRequest) : this(postedFile, fullPath, contentLength)
		{
			this._chunkRequest = chunkRequest;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000BB3C File Offset: 0x00009D3C
		public AsyncPostedFile(UploadedFile postedFile, string fullPath, long contentLength)
		{
			this.PostedFile = postedFile;
			this.Path = fullPath;
			this.Init(contentLength);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000BB5C File Offset: 0x00009D5C
		private void Init(long cLength)
		{
			this._contentLength = cLength;
			string text = this.PostedFile.GetExtension().ToLowerInvariant().TrimStart(new char[]
			{
				'.'
			});
			if (this.PostedFile.ContentType == "application/octet-stream" && text != ".a")
			{
				if (MimeTypes.Types.ContainsKey(text))
				{
					this._contentType = MimeTypes.Types[text];
				}
			}
			else
			{
				this._contentType = this.PostedFile.ContentType;
			}
			this._fileName = this.PostedFile.FileName;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000BBFC File Offset: 0x00009DFC
		internal virtual void NormalizeWith(NameValueCollection formValues)
		{
			if (!string.IsNullOrEmpty(formValues["fileName"]))
			{
				this._fileName = formValues["fileName"];
			}
			if (!string.IsNullOrEmpty(formValues["contentType"]))
			{
				this._contentType = formValues["contentType"];
			}
			if (!string.IsNullOrEmpty(formValues["lastModifiedDate"]))
			{
				string s = formValues["lastModifiedDate"];
				DateTime lastModifiedDate;
				DateTime.TryParse(s, out lastModifiedDate);
				base.LastModifiedDate = lastModifiedDate;
				this.lastModifiedDateInJson = s;
			}
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000BC84 File Offset: 0x00009E84
		public override void SaveAs(string fullName, bool overwrite)
		{
			if (File.Exists(fullName))
			{
				if (!overwrite)
				{
					return;
				}
				File.Delete(fullName);
			}
			if (this.ChunkRequest)
			{
				using (FileStream fileStream = this.InputStream as FileStream)
				{
					using (FileStream fileStream2 = new FileStream(fullName, FileMode.OpenOrCreate, FileAccess.Write))
					{
						StreamExtensions.CopyTo(fileStream, fileStream2);
					}
					return;
				}
			}
			this.PostedFile.SaveAs(fullName);
		}

		// Token: 0x04000089 RID: 137
		internal const string FileApiFileNameKey = "fileName";

		// Token: 0x0400008A RID: 138
		internal const string FileApiContentTypeKEy = "contentType";

		// Token: 0x0400008B RID: 139
		internal const string FileApiLastModifiedDateKey = "lastModifiedDate";

		// Token: 0x0400008C RID: 140
		private long _contentLength;

		// Token: 0x0400008D RID: 141
		private string _fileName;

		// Token: 0x0400008E RID: 142
		private string _contentType;

		// Token: 0x0400008F RID: 143
		private bool _chunkRequest;

		// Token: 0x04000090 RID: 144
		private readonly UploadedFile PostedFile;

		// Token: 0x04000091 RID: 145
		private readonly string Path;

		// Token: 0x04000092 RID: 146
		internal string lastModifiedDateInJson;
	}
}
