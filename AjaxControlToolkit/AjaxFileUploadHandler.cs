using System;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace AjaxControlToolkit
{
	// Token: 0x02000022 RID: 34
	public class AjaxFileUploadHandler : IHttpHandler, IReadOnlySessionState, IRequiresSessionState
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00005AA1 File Offset: 0x00003CA1
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005AA4 File Offset: 0x00003CA4
		public void ProcessRequest(HttpContext context)
		{
			HttpRequest request = context.Request;
			if (request.QueryString["contextKey"] != "{DA8BEDC8-B952-4d5d-8CC2-59FE922E2923}")
			{
				throw new Exception("Invalid context key");
			}
			if (request.Headers["Content-Type"] != null && request.Headers["Content-Type"].StartsWith("multipart/form-data;") && request.Headers["Content-Length"] != null)
			{
				AjaxFileUploadHelper.Process(context);
				context.Response.ContentEncoding = Encoding.UTF8;
				context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
				context.Response.End();
				return;
			}
			throw new Exception("Invalid upload request.");
		}
	}
}
