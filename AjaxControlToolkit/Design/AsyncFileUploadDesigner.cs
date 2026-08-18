using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.Design;

namespace AjaxControlToolkit.Design
{
	// Token: 0x02000042 RID: 66
	public class AsyncFileUploadDesigner : ControlDesigner
	{
		// Token: 0x06000245 RID: 581 RVA: 0x000086AC File Offset: 0x000068AC
		public override string GetDesignTimeHtml(DesignerRegionCollection regions)
		{
			AsyncFileUpload asyncFileUpload = (AsyncFileUpload)base.Component;
			StringBuilder stringBuilder = new StringBuilder(1024);
			StringWriter writer = new StringWriter(stringBuilder, CultureInfo.InvariantCulture);
			HtmlTextWriter writer2 = new HtmlTextWriter(writer);
			asyncFileUpload.CreateChilds();
			asyncFileUpload.RenderControl(writer2);
			return stringBuilder.ToString();
		}
	}
}
