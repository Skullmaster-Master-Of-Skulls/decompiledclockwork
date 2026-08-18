using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x0200030C RID: 780
	internal class WebServiceSettingsConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06001A6E RID: 6766 RVA: 0x00056560 File Offset: 0x00054760
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			WebServiceSettings webServiceSettings = obj as WebServiceSettings;
			if (webServiceSettings == null)
			{
				throw new InvalidOperationException("Can serialize only WebServiceSettings objects.");
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			ExplicitJavaScriptConverter.AddProperty(dictionary, "path", WebServiceSettingsConverter.ResolveUrl(webServiceSettings.Path), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "method", webServiceSettings.Method, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "useHttpGet", webServiceSettings.UseHttpGet, false);
			return dictionary;
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06001A6F RID: 6767 RVA: 0x000565D8 File Offset: 0x000547D8
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(WebServiceSettings)
				};
			}
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x000565FC File Offset: 0x000547FC
		protected internal static string ResolveUrl(string originalUrl)
		{
			if (string.IsNullOrEmpty(originalUrl))
			{
				return originalUrl;
			}
			if (originalUrl.Contains("://"))
			{
				return originalUrl;
			}
			if (!originalUrl.StartsWith("~"))
			{
				return originalUrl;
			}
			int num = originalUrl.IndexOf('?');
			if (num != -1)
			{
				string str = originalUrl.Substring(num);
				string virtualPath = originalUrl.Substring(0, num);
				return VirtualPathUtility.ToAbsolute(virtualPath) + str;
			}
			return VirtualPathUtility.ToAbsolute(originalUrl);
		}
	}
}
