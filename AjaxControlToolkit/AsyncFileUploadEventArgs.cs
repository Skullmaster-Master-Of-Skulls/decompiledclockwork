using System;

namespace AjaxControlToolkit
{
	// Token: 0x02000044 RID: 68
	public class AsyncFileUploadEventArgs : EventArgs
	{
		// Token: 0x06000247 RID: 583 RVA: 0x000086FE File Offset: 0x000068FE
		public AsyncFileUploadEventArgs()
		{
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00008730 File Offset: 0x00006930
		public AsyncFileUploadEventArgs(AsyncFileUploadState state, string statusMessage, string filename, string filesize)
		{
			this._statusMessage = statusMessage;
			this._filename = filename;
			this._filesize = filesize;
			this._state = state;
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00008788 File Offset: 0x00006988
		public string StatusMessage
		{
			get
			{
				return this._statusMessage;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00008790 File Offset: 0x00006990
		public string FileName
		{
			get
			{
				return this._filename;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00008798 File Offset: 0x00006998
		public string FileSize
		{
			get
			{
				return this._filesize;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600024C RID: 588 RVA: 0x000087A0 File Offset: 0x000069A0
		public AsyncFileUploadState State
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x040000BF RID: 191
		private string _statusMessage = string.Empty;

		// Token: 0x040000C0 RID: 192
		private string _filename = string.Empty;

		// Token: 0x040000C1 RID: 193
		private string _filesize = string.Empty;

		// Token: 0x040000C2 RID: 194
		private AsyncFileUploadState _state = AsyncFileUploadState.Unknown;
	}
}
