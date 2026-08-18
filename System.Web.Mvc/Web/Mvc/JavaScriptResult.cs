using System;

namespace System.Web.Mvc
{
	// Token: 0x0200016F RID: 367
	public class JavaScriptResult : ActionResult
	{
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x0001AB3F File Offset: 0x00018D3F
		// (set) Token: 0x0600099B RID: 2459 RVA: 0x0001AB47 File Offset: 0x00018D47
		public string Script { get; set; }

		// Token: 0x0600099C RID: 2460 RVA: 0x0001AB50 File Offset: 0x00018D50
		public override void ExecuteResult(ControllerContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			HttpResponseBase response = context.HttpContext.Response;
			response.ContentType = "application/x-javascript";
			if (this.Script != null)
			{
				response.Write(this.Script);
			}
		}
	}
}
