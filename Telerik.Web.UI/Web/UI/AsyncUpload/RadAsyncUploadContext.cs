using System;
using Telerik.Web.UI.Upload;

namespace Telerik.Web.UI.AsyncUpload
{
	// Token: 0x02001B71 RID: 7025
	internal class RadAsyncUploadContext : RadUploadContext
	{
		// Token: 0x17005321 RID: 21281
		// (get) Token: 0x06011075 RID: 69749 RVA: 0x003C2700 File Offset: 0x003C0900
		// (set) Token: 0x06011076 RID: 69750 RVA: 0x003C2744 File Offset: 0x003C0944
		public int UploadsInProgress
		{
			get
			{
				int result;
				lock (this.uploadsLock)
				{
					result = this.uploadsInProgress;
				}
				return result;
			}
			set
			{
				lock (this.uploadsLock)
				{
					this.uploadsInProgress = value;
				}
			}
		}

		// Token: 0x06011077 RID: 69751 RVA: 0x003C2788 File Offset: 0x003C0988
		internal RadAsyncUploadContext(int requestLength, RequestStateStore stateStore) : base(requestLength, stateStore)
		{
		}

		// Token: 0x04004C2C RID: 19500
		private readonly object uploadsLock = new object();

		// Token: 0x04004C2D RID: 19501
		private int uploadsInProgress;
	}
}
