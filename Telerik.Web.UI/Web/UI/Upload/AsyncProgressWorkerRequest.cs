using System;
using System.Web;

namespace Telerik.Web.UI.Upload
{
	// Token: 0x02001B70 RID: 7024
	internal class AsyncProgressWorkerRequest : ProgressWorkerRequest
	{
		// Token: 0x06011073 RID: 69747 RVA: 0x003C26E7 File Offset: 0x003C08E7
		public AsyncProgressWorkerRequest(HttpWorkerRequest wr, HttpRequest request) : base(wr, request)
		{
		}

		// Token: 0x06011074 RID: 69748 RVA: 0x003C26F1 File Offset: 0x003C08F1
		protected override void UpdateProgress(byte[] buffer, int validBytes)
		{
			base.RequestStateStore.UpdateCurrentRequestBytesCount(validBytes);
		}
	}
}
