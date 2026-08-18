using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;

namespace Telerik.Web.UI
{
	// Token: 0x020016CD RID: 5837
	public class CaptchaImageHandler : IHttpHandler
	{
		// Token: 0x0600E150 RID: 57680 RVA: 0x00321450 File Offset: 0x0031F650
		public void ProcessRequest(HttpContext context)
		{
			HttpApplication applicationInstance = context.ApplicationInstance;
			string text = applicationInstance.Request.QueryString["guid"];
			string isStoredInCache = applicationInstance.Request.QueryString["isc"];
			CaptchaImage captchaImage = null;
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					CaptchaImageHelper captchaImageHelper = new CaptchaImageHelper(text, isStoredInCache);
					captchaImage = captchaImageHelper.GetCaptchaImage();
				}
				catch
				{
					applicationInstance.Response.StatusCode = 404;
					context.ApplicationInstance.CompleteRequest();
					return;
				}
				if (captchaImage == null)
				{
					Brush gray = Brushes.Gray;
					Bitmap bitmap = new Bitmap(50, 50);
					Graphics graphics = Graphics.FromImage(bitmap);
					GraphicsUnit graphicsUnit = GraphicsUnit.Pixel;
					graphics.FillRectangle(gray, bitmap.GetBounds(ref graphicsUnit));
					bitmap.Save(applicationInstance.Context.Response.OutputStream, ImageFormat.Gif);
					bitmap.Dispose();
					applicationInstance.Response.ContentType = "image/gif";
				}
				else
				{
					Bitmap bitmap2 = captchaImage.RenderImage();
					bitmap2.Save(applicationInstance.Context.Response.OutputStream, ImageFormat.Jpeg);
					bitmap2.Dispose();
					applicationInstance.Response.ContentType = "image/jpeg";
				}
				applicationInstance.Response.StatusCode = 200;
				context.ApplicationInstance.CompleteRequest();
			}
		}

		// Token: 0x17004514 RID: 17684
		// (get) Token: 0x0600E151 RID: 57681 RVA: 0x003215A4 File Offset: 0x0031F7A4
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}
	}
}
