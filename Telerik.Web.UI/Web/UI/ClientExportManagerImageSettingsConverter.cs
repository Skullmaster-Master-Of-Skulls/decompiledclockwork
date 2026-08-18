using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000122 RID: 290
	internal class ClientExportManagerImageSettingsConverter : JavaScriptConverter
	{
		// Token: 0x06000C1F RID: 3103 RVA: 0x0002CD61 File Offset: 0x0002AF61
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			return null;
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x0002CD64 File Offset: 0x0002AF64
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			ClientExportManagerImageSettings clientExportManagerImageSettings = obj as ClientExportManagerImageSettings;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (string.IsNullOrEmpty(clientExportManagerImageSettings.FileName))
			{
				clientExportManagerImageSettings.FileName = "Default.png";
			}
			dictionary.Add("fileName", clientExportManagerImageSettings.FileName);
			if (!string.IsNullOrEmpty(clientExportManagerImageSettings.ProxyURL))
			{
				dictionary.Add("proxyURL", clientExportManagerImageSettings.ProxyURL);
			}
			if (!string.IsNullOrEmpty(clientExportManagerImageSettings.Height))
			{
				dictionary.Add("height", clientExportManagerImageSettings.Height);
			}
			if (!string.IsNullOrEmpty(clientExportManagerImageSettings.Width))
			{
				dictionary.Add("width", clientExportManagerImageSettings.Width);
			}
			return dictionary;
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000C21 RID: 3105 RVA: 0x0002CED0 File Offset: 0x0002B0D0
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(ClientExportManagerImageSettings);
				yield break;
			}
		}
	}
}
