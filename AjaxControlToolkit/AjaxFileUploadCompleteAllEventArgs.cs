using System;

namespace AjaxControlToolkit
{
	// Token: 0x0200001E RID: 30
	public class AjaxFileUploadCompleteAllEventArgs : EventArgs
	{
		// Token: 0x06000163 RID: 355 RVA: 0x000058D9 File Offset: 0x00003AD9
		public AjaxFileUploadCompleteAllEventArgs(int filesInQueue, int filesUploaded, AjaxFileUploadCompleteAllReason reason)
		{
			this._filesInQueue = filesInQueue;
			this._filesUploaded = filesUploaded;
			this._reason = reason;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000164 RID: 356 RVA: 0x000058F6 File Offset: 0x00003AF6
		public int FilesUploaded
		{
			get
			{
				return this._filesUploaded;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000165 RID: 357 RVA: 0x000058FE File Offset: 0x00003AFE
		public int FilesInQueue
		{
			get
			{
				return this._filesInQueue;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00005906 File Offset: 0x00003B06
		public AjaxFileUploadCompleteAllReason Reason
		{
			get
			{
				return this._reason;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000590E File Offset: 0x00003B0E
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00005916 File Offset: 0x00003B16
		public string ServerArguments { get; set; }

		// Token: 0x04000055 RID: 85
		private readonly int _filesInQueue;

		// Token: 0x04000056 RID: 86
		private readonly int _filesUploaded;

		// Token: 0x04000057 RID: 87
		private readonly AjaxFileUploadCompleteAllReason _reason;
	}
}
