using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000123 RID: 291
	internal class ClientExportManagerSvgSettingsConverter : JavaScriptConverter
	{
		// Token: 0x06000C23 RID: 3107 RVA: 0x0002CEF5 File Offset: 0x0002B0F5
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			return null;
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x0002CEF8 File Offset: 0x0002B0F8
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			ClientExportManagerSvgSettings clientExportManagerSvgSettings = obj as ClientExportManagerSvgSettings;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("raw", clientExportManagerSvgSettings.Raw);
			if (string.IsNullOrEmpty(clientExportManagerSvgSettings.FileName))
			{
				clientExportManagerSvgSettings.FileName = "Default.svg";
			}
			dictionary.Add("fileName", clientExportManagerSvgSettings.FileName);
			if (!string.IsNullOrEmpty(clientExportManagerSvgSettings.ProxyURL))
			{
				dictionary.Add("proxyURL", clientExportManagerSvgSettings.ProxyURL);
			}
			return dictionary;
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x0002D03C File Offset: 0x0002B23C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(ClientExportManagerSvgSettings);
				yield break;
			}
		}
	}
}
