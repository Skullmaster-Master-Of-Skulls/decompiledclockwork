using System;
using System.Web;

namespace Telerik.Web.UI
{
	// Token: 0x02001B80 RID: 7040
	public class RadUploadProgressHandler : IHttpHandler
	{
		// Token: 0x060110F8 RID: 69880 RVA: 0x003C33DC File Offset: 0x003C15DC
		public void ProcessRequest(HttpContext context)
		{
			HttpResponse response = context.Response;
			try
			{
				bool flag;
				if (bool.TryParse(context.Request.QueryString["AsyncProgress"], out flag))
				{
					response.ContentType = "application/json";
					RadProgressContext.Current.Serialize(response.Output, true);
				}
				else
				{
					response.ContentType = "text/plain";
					RadProgressContext.Current.Serialize(response.Output);
				}
			}
			catch (Exception)
			{
				response.Write("Internal server error");
			}
		}

		// Token: 0x17005360 RID: 21344
		// (get) Token: 0x060110F9 RID: 69881 RVA: 0x003C3468 File Offset: 0x003C1668
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04004C66 RID: 19558
		public const string DefaultUrl = "~/Telerik.RadUploadProgressHandler.ashx";
	}
}
