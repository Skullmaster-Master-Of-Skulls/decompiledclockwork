using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace Telerik.Web.UI
{
	// Token: 0x020016A8 RID: 5800
	internal class AsyncUploadedFile : UploadedFile
	{
		// Token: 0x1700449E RID: 17566
		// (get) Token: 0x0600DFF6 RID: 57334 RVA: 0x0031D6F8 File Offset: 0x0031B8F8
		// (set) Token: 0x0600DFF7 RID: 57335 RVA: 0x0031D700 File Offset: 0x0031B900
		private RadAsyncUpload AsyncUpload { get; set; }

		// Token: 0x1700449F RID: 17567
		// (get) Token: 0x0600DFF8 RID: 57336 RVA: 0x0031D709 File Offset: 0x0031B909
		// (set) Token: 0x0600DFF9 RID: 57337 RVA: 0x0031D711 File Offset: 0x0031B911
		private UploadedFileInfo FileInfo { get; set; }

		// Token: 0x170044A0 RID: 17568
		// (get) Token: 0x0600DFFA RID: 57338 RVA: 0x0031D71A File Offset: 0x0031B91A
		// (set) Token: 0x0600DFFB RID: 57339 RVA: 0x0031D722 File Offset: 0x0031B922
		internal string SerializedData { get; set; }

		// Token: 0x170044A1 RID: 17569
		// (get) Token: 0x0600DFFC RID: 57340 RVA: 0x0031D72B File Offset: 0x0031B92B
		// (set) Token: 0x0600DFFD RID: 57341 RVA: 0x0031D733 File Offset: 0x0031B933
		internal string FileType { get; set; }

		// Token: 0x0600DFFE RID: 57342 RVA: 0x0031D73C File Offset: 0x0031B93C
		public AsyncUploadedFile(RadAsyncUpload asyncUpload, UploadedFileInfo fileInfo)
		{
			this.AsyncUpload = asyncUpload;
			this.FileInfo = fileInfo;
			this.FileInfo.FileName = this.ConvertToUtf8(this.FileInfo.FileName);
			this.SerializedData = fileInfo.SerializedData;
			this.FileType = fileInfo.FileType;
			base.LastModifiedDate = fileInfo.LastModifiedDate;
		}

		// Token: 0x170044A2 RID: 17570
		// (get) Token: 0x0600DFFF RID: 57343 RVA: 0x0031D79D File Offset: 0x0031B99D
		public override long ContentLength
		{
			get
			{
				return this.FileInfo.ContentLength;
			}
		}

		// Token: 0x170044A3 RID: 17571
		// (get) Token: 0x0600E000 RID: 57344 RVA: 0x0031D7AA File Offset: 0x0031B9AA
		public override string ContentType
		{
			get
			{
				return this.FileInfo.ContentType;
			}
		}

		// Token: 0x170044A4 RID: 17572
		// (get) Token: 0x0600E001 RID: 57345 RVA: 0x0031D7B7 File Offset: 0x0031B9B7
		// (set) Token: 0x0600E002 RID: 57346 RVA: 0x0031D7C4 File Offset: 0x0031B9C4
		public override string FileName
		{
			get
			{
				return this.FileInfo.FileName;
			}
			internal set
			{
				this.FileInfo.FileName = value;
			}
		}

		// Token: 0x170044A5 RID: 17573
		// (get) Token: 0x0600E003 RID: 57347 RVA: 0x0031D7D2 File Offset: 0x0031B9D2
		public override Stream InputStream
		{
			get
			{
				return File.OpenRead(this.TempFilePath);
			}
		}

		// Token: 0x170044A6 RID: 17574
		// (get) Token: 0x0600E004 RID: 57348 RVA: 0x0031D7DF File Offset: 0x0031B9DF
		internal string TempFilePath
		{
			get
			{
				return Path.Combine(this.AsyncUpload.MappedTemporaryFolder, this.FileInfo.TempFileName);
			}
		}

		// Token: 0x0600E005 RID: 57349 RVA: 0x0031D7FC File Offset: 0x0031B9FC
		public override string GetFieldValue(string fieldName)
		{
			return HttpContext.Current.Request.Form[this.AsyncUpload.ClientID + fieldName + this.FileInfo.Index];
		}

		// Token: 0x0600E006 RID: 57350 RVA: 0x0031D833 File Offset: 0x0031BA33
		public override void SaveAs(string fileName, bool overwrite)
		{
			if (overwrite && File.Exists(fileName))
			{
				File.Delete(fileName);
			}
			File.Move(this.TempFilePath, fileName);
		}

		// Token: 0x170044A7 RID: 17575
		// (get) Token: 0x0600E007 RID: 57351 RVA: 0x0031D852 File Offset: 0x0031BA52
		protected internal override string InputFieldName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600E008 RID: 57352 RVA: 0x0031D858 File Offset: 0x0031BA58
		private string ConvertToUtf8(string fileName)
		{
			AspNetHostingPermissionLevel currentTrustLevel = SecurityHelper.GetCurrentTrustLevel();
			if (currentTrustLevel == AspNetHostingPermissionLevel.High || currentTrustLevel == AspNetHostingPermissionLevel.Unrestricted)
			{
				if (!string.IsNullOrEmpty(fileName))
				{
					GlobalizationSection globalizationSection = ConfigurationManager.GetSection("system.web/globalization") as GlobalizationSection;
					if (globalizationSection != null)
					{
						fileName = Encoding.UTF8.GetString(globalizationSection.RequestEncoding.GetBytes(fileName));
					}
				}
				return fileName;
			}
			return fileName;
		}
	}
}
