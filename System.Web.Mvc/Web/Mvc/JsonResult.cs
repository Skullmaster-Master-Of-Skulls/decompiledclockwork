using System;
using System.Text;
using System.Web.Mvc.Properties;
using System.Web.Script.Serialization;

namespace System.Web.Mvc
{
	// Token: 0x020001D8 RID: 472
	public class JsonResult : ActionResult
	{
		// Token: 0x06000E05 RID: 3589 RVA: 0x00025308 File Offset: 0x00023508
		public JsonResult()
		{
			this.JsonRequestBehavior = JsonRequestBehavior.DenyGet;
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000E06 RID: 3590 RVA: 0x00025317 File Offset: 0x00023517
		// (set) Token: 0x06000E07 RID: 3591 RVA: 0x0002531F File Offset: 0x0002351F
		public Encoding ContentEncoding { get; set; }

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000E08 RID: 3592 RVA: 0x00025328 File Offset: 0x00023528
		// (set) Token: 0x06000E09 RID: 3593 RVA: 0x00025330 File Offset: 0x00023530
		public string ContentType { get; set; }

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x00025339 File Offset: 0x00023539
		// (set) Token: 0x06000E0B RID: 3595 RVA: 0x00025341 File Offset: 0x00023541
		public object Data { get; set; }

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000E0C RID: 3596 RVA: 0x0002534A File Offset: 0x0002354A
		// (set) Token: 0x06000E0D RID: 3597 RVA: 0x00025352 File Offset: 0x00023552
		public JsonRequestBehavior JsonRequestBehavior { get; set; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x0002535B File Offset: 0x0002355B
		// (set) Token: 0x06000E0F RID: 3599 RVA: 0x00025363 File Offset: 0x00023563
		public int? MaxJsonLength { get; set; }

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x0002536C File Offset: 0x0002356C
		// (set) Token: 0x06000E11 RID: 3601 RVA: 0x00025374 File Offset: 0x00023574
		public int? RecursionLimit { get; set; }

		// Token: 0x06000E12 RID: 3602 RVA: 0x00025380 File Offset: 0x00023580
		public override void ExecuteResult(ControllerContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (this.JsonRequestBehavior == JsonRequestBehavior.DenyGet && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
			{
				throw new InvalidOperationException(MvcResources.JsonRequest_GetNotAllowed);
			}
			HttpResponseBase response = context.HttpContext.Response;
			if (!string.IsNullOrEmpty(this.ContentType))
			{
				response.ContentType = this.ContentType;
			}
			else
			{
				response.ContentType = "application/json";
			}
			if (this.ContentEncoding != null)
			{
				response.ContentEncoding = this.ContentEncoding;
			}
			if (this.Data != null)
			{
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				if (this.MaxJsonLength != null)
				{
					javaScriptSerializer.MaxJsonLength = this.MaxJsonLength.Value;
				}
				if (this.RecursionLimit != null)
				{
					javaScriptSerializer.RecursionLimit = this.RecursionLimit.Value;
				}
				response.Write(javaScriptSerializer.Serialize(this.Data));
			}
		}
	}
}
