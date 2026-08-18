using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Optimization.Resources;
using Microsoft.Ajax.Utilities;

namespace System.Web.Optimization
{
	// Token: 0x02000037 RID: 55
	public class JsMinify : IBundleTransform
	{
		// Token: 0x06000187 RID: 391 RVA: 0x00005FE0 File Offset: 0x000041E0
		internal static void GenerateErrorResponse(BundleResponse bundle, IEnumerable<object> errors)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("/* ");
			stringBuilder.Append(OptimizationResources.MinifyError).Append("\r\n");
			foreach (object obj in errors)
			{
				stringBuilder.Append(obj.ToString()).Append("\r\n");
			}
			stringBuilder.Append(" */\r\n");
			stringBuilder.Append(bundle.Content);
			bundle.Content = stringBuilder.ToString();
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00006088 File Offset: 0x00004288
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
				string content = minifier.MinifyJavaScript(response.Content, new CodeSettings
				{
					EvalTreatment = EvalTreatment.MakeImmediateSafe,
					PreserveImportantComments = false
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
			response.ContentType = JsMinify.JsContentType;
		}

		// Token: 0x0400007F RID: 127
		internal static string JsContentType = "text/javascript";

		// Token: 0x04000080 RID: 128
		internal static readonly JsMinify Instance = new JsMinify();
	}
}
