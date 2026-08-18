using System;
using Microsoft.Ajax.Utilities;

namespace System.Web.Optimization
{
	// Token: 0x02000031 RID: 49
	public class CssMinify : IBundleTransform
	{
		// Token: 0x06000167 RID: 359 RVA: 0x00005850 File Offset: 0x00003A50
		public virtual void Process(BundleContext context, BundleResponse response)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}
			if (!context.EnableInstrumentation)
			{
				Minifier minifier = new Minifier();
				string content = minifier.MinifyStyleSheet(response.Content, new CssSettings
				{
					CommentMode = CssComment.None
				});
				if (minifier.ErrorList.Count > 0)
				{
					JsMinify.GenerateErrorResponse(response, minifier.ErrorList);
				}
				else
				{
					response.Content = content;
				}
			}
			response.ContentType = CssMinify.CssContentType;
		}

		// Token: 0x04000078 RID: 120
		internal static readonly CssMinify Instance = new CssMinify();

		// Token: 0x04000079 RID: 121
		internal static string CssContentType = "text/css";
	}
}
