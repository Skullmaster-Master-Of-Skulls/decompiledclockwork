using System;
using System.Text;

namespace System.Web.Mvc
{
	// Token: 0x020001D7 RID: 471
	public class ContentResult : ActionResult
	{
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000DFD RID: 3581 RVA: 0x00025263 File Offset: 0x00023463
		// (set) Token: 0x06000DFE RID: 3582 RVA: 0x0002526B File Offset: 0x0002346B
		public string Content { get; set; }

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000DFF RID: 3583 RVA: 0x00025274 File Offset: 0x00023474
		// (set) Token: 0x06000E00 RID: 3584 RVA: 0x0002527C File Offset: 0x0002347C
		public Encoding ContentEncoding { get; set; }

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x00025285 File Offset: 0x00023485
		// (set) Token: 0x06000E02 RID: 3586 RVA: 0x0002528D File Offset: 0x0002348D
		public string ContentType { get; set; }

		// Token: 0x06000E03 RID: 3587 RVA: 0x00025298 File Offset: 0x00023498
		public override void ExecuteResult(ControllerContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			HttpResponseBase response = context.HttpContext.Response;
			if (!string.IsNullOrEmpty(this.ContentType))
			{
				response.ContentType = this.ContentType;
			}
			if (this.ContentEncoding != null)
			{
				response.ContentEncoding = this.ContentEncoding;
			}
			if (this.Content != null)
			{
				response.Write(this.Content);
			}
		}
	}
}
