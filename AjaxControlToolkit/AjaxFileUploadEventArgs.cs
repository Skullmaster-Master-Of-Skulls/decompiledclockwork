using System;
using System.IO;

namespace AjaxControlToolkit
{
	// Token: 0x02000021 RID: 33
	public class AjaxFileUploadEventArgs : EventArgs
	{
		// Token: 0x0600016B RID: 363 RVA: 0x0000593C File Offset: 0x00003B3C
		public AjaxFileUploadEventArgs(string fileId, AjaxFileUploadState state, string statusMessage, string fileName, int fileSize, string contentType)
		{
			this._fileId = fileId;
			this._state = state;
			this._statusMessage = statusMessage;
			this._fileName = fileName;
			this._fileSize = fileSize;
			this._contentType = contentType;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600016C RID: 364 RVA: 0x000059BA File Offset: 0x00003BBA
		public string FileId
		{
			get
			{
				return this._fileId;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600016D RID: 365 RVA: 0x000059C2 File Offset: 0x00003BC2
		public AjaxFileUploadState State
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600016E RID: 366 RVA: 0x000059CA File Offset: 0x00003BCA
		public string ContentType
		{
			get
			{
				return this._contentType;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600016F RID: 367 RVA: 0x000059D2 File Offset: 0x00003BD2
		public int FileSize
		{
			get
			{
				return this._fileSize;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000170 RID: 368 RVA: 0x000059DA File Offset: 0x00003BDA
		public string FileName
		{
			get
			{
				return this._fileName;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000171 RID: 369 RVA: 0x000059E2 File Offset: 0x00003BE2
		public string StatusMessage
		{
			get
			{
				return this._statusMessage;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000172 RID: 370 RVA: 0x000059EA File Offset: 0x00003BEA
		// (set) Token: 0x06000173 RID: 371 RVA: 0x000059F2 File Offset: 0x00003BF2
		public string PostedUrl
		{
			get
			{
				return this._postedUrl;
			}
			set
			{
				this._postedUrl = value;
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000059FC File Offset: 0x00003BFC
		public byte[] GetContents()
		{
			byte[] result;
			using (Stream streamContents = this.GetStreamContents())
			{
				byte[] array = new byte[streamContents.Length];
				streamContents.Read(array, 0, array.Length);
				result = array;
			}
			return result;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00005A48 File Offset: 0x00003C48
		public Stream GetStreamContents()
		{
			string path = AjaxFileUpload.BuildTempFolder(this._fileId);
			return File.OpenRead(Path.Combine(path, this._fileName));
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005A74 File Offset: 0x00003C74
		public void DeleteTemporaryData()
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(AjaxFileUpload.BuildTempFolder(this._fileId));
			if (directoryInfo.Exists)
			{
				directoryInfo.Delete(true);
			}
		}

		// Token: 0x0400005D RID: 93
		private string _fileId = string.Empty;

		// Token: 0x0400005E RID: 94
		private string _statusMessage = string.Empty;

		// Token: 0x0400005F RID: 95
		private string _fileName = string.Empty;

		// Token: 0x04000060 RID: 96
		private int _fileSize;

		// Token: 0x04000061 RID: 97
		private string _contentType = string.Empty;

		// Token: 0x04000062 RID: 98
		private string _postedUrl = string.Empty;

		// Token: 0x04000063 RID: 99
		private AjaxFileUploadState _state = AjaxFileUploadState.Unknown;
	}
}
