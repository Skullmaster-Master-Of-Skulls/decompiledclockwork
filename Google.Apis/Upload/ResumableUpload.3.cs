using System;
using System.IO;
using System.Net.Http;
using Google.Apis.Services;

namespace Google.Apis.Upload
{
	// Token: 0x02000008 RID: 8
	public class ResumableUpload<TRequest, TResponse> : ResumableUpload<TRequest>
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00002AC0 File Offset: 0x00000CC0
		protected ResumableUpload(IClientService service, string path, string httpMethod, Stream contentStream, string contentType) : base(service, path, httpMethod, contentStream, contentType)
		{
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002ACF File Offset: 0x00000CCF
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002AD7 File Offset: 0x00000CD7
		public TResponse ResponseBody { get; private set; }

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000047 RID: 71 RVA: 0x00002AE0 File Offset: 0x00000CE0
		// (remove) Token: 0x06000048 RID: 72 RVA: 0x00002B18 File Offset: 0x00000D18
		public event Action<TResponse> ResponseReceived;

		// Token: 0x06000049 RID: 73 RVA: 0x00002B4D File Offset: 0x00000D4D
		protected override void ProcessResponse(HttpResponseMessage response)
		{
			base.ProcessResponse(response);
			this.ResponseBody = base.Service.DeserializeResponse<TResponse>(response).Result;
			Action<TResponse> responseReceived = this.ResponseReceived;
			if (responseReceived == null)
			{
				return;
			}
			responseReceived(this.ResponseBody);
		}
	}
}
