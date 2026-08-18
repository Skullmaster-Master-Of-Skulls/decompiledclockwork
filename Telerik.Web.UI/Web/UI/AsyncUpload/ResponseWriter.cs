using System;
using System.Web;

namespace Telerik.Web.UI.AsyncUpload
{
	// Token: 0x0200018E RID: 398
	internal class ResponseWriter : IResponseWriter
	{
		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x00033F10 File Offset: 0x00032110
		// (set) Token: 0x06000D9E RID: 3486 RVA: 0x00033F18 File Offset: 0x00032118
		public HttpContext Context { get; set; }

		// Token: 0x06000D9F RID: 3487 RVA: 0x00033F21 File Offset: 0x00032121
		public ResponseWriter()
		{
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00033F29 File Offset: 0x00032129
		public ResponseWriter(HttpContext context)
		{
			this.Context = context;
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00033F38 File Offset: 0x00032138
		public void WriteToResponse(string response)
		{
			if ((this.Context.Request.Form["acceptJsonResponse"] == null || !(this.Context.Request.Form["acceptJsonResponse"].ToString() == "false")) && ((response.StartsWith("{") && response.EndsWith("}")) || (response.StartsWith("[") && response.EndsWith("]"))))
			{
				this.Context.Response.ContentType = "application/json";
			}
			else
			{
				this.Context.Response.ContentType = "text/html";
			}
			this.Context.Response.Write(response);
		}
	}
}
