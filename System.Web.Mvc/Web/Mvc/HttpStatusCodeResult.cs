using System;
using System.Net;

namespace System.Web.Mvc
{
	// Token: 0x020000DC RID: 220
	public class HttpStatusCodeResult : ActionResult
	{
		// Token: 0x060005B1 RID: 1457 RVA: 0x0000FA99 File Offset: 0x0000DC99
		public HttpStatusCodeResult(int statusCode) : this(statusCode, null)
		{
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0000FAA3 File Offset: 0x0000DCA3
		public HttpStatusCodeResult(HttpStatusCode statusCode) : this(statusCode, null)
		{
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0000FAAD File Offset: 0x0000DCAD
		public HttpStatusCodeResult(HttpStatusCode statusCode, string statusDescription) : this((int)statusCode, statusDescription)
		{
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0000FAB7 File Offset: 0x0000DCB7
		public HttpStatusCodeResult(int statusCode, string statusDescription)
		{
			this.StatusCode = statusCode;
			this.StatusDescription = statusDescription;
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0000FACD File Offset: 0x0000DCCD
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x0000FAD5 File Offset: 0x0000DCD5
		public int StatusCode { get; private set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x0000FADE File Offset: 0x0000DCDE
		// (set) Token: 0x060005B8 RID: 1464 RVA: 0x0000FAE6 File Offset: 0x0000DCE6
		public string StatusDescription { get; private set; }

		// Token: 0x060005B9 RID: 1465 RVA: 0x0000FAF0 File Offset: 0x0000DCF0
		public override void ExecuteResult(ControllerContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			context.HttpContext.Response.StatusCode = this.StatusCode;
			if (this.StatusDescription != null)
			{
				context.HttpContext.Response.StatusDescription = this.StatusDescription;
			}
		}
	}
}
