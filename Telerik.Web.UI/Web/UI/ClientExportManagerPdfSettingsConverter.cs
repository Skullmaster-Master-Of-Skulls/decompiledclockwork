using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000121 RID: 289
	internal class ClientExportManagerPdfSettingsConverter : JavaScriptConverter
	{
		// Token: 0x06000C1B RID: 3099 RVA: 0x0002CA47 File Offset: 0x0002AC47
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			return null;
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x0002CA4C File Offset: 0x0002AC4C
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			ClientExportManagerPdfSettings clientExportManagerPdfSettings = obj as ClientExportManagerPdfSettings;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("date", serializer.Serialize(clientExportManagerPdfSettings.Date));
			if (!string.IsNullOrEmpty(clientExportManagerPdfSettings.Author))
			{
				dictionary.Add("author", clientExportManagerPdfSettings.Author);
			}
			if (!string.IsNullOrEmpty(clientExportManagerPdfSettings.Creator))
			{
				dictionary.Add("creator", clientExportManagerPdfSettings.Creator);
			}
			if (string.IsNullOrEmpty(clientExportManagerPdfSettings.FileName))
			{
				clientExportManagerPdfSettings.FileName = "Default.pdf";
			}
			dictionary.Add("fileName", clientExportManagerPdfSettings.FileName);
			if (!string.IsNullOrEmpty(clientExportManagerPdfSettings.Keywords))
			{
				dictionary.Add("keywords", clientExportManagerPdfSettings.Keywords);
			}
			dictionary.Add("landscape", clientExportManagerPdfSettings.Landscape);
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
			if (!string.IsNullOrEmpty(clientExportManagerPdfSettings.MarginBottom))
			{
				dictionary2.Add("bottom", clientExportManagerPdfSettings.MarginBottom);
			}
			if (!string.IsNullOrEmpty(clientExportManagerPdfSettings.MarginLeft))
			{
				dictionary2.Add("left", clientExportManagerPdfSettings.MarginLeft);
			}
			if (!string.IsNullOrEmpty(clientExportManagerPdfSettings.MarginRight))
			{
				dictionary2.Add("right", clientExportManagerPdfSettings.MarginRight);
			}
			if (!string.IsNullOrEmpty(clientExportManagerPdfSettings.MarginTop))
			{
				dictionary2.Add("top", clientExportManagerPdfSettings.MarginTop);
			}
			if (dictionary2.Count > 0)
			{
				dictionary.Add("margin", serializer.Serialize(dictionary2));
			}
			if (!string.IsNullOrEmpty(clientExportManagerPdfSettings.PaperSize))
			{
				dictionary.Add("paperSize", clientExportManagerPdfSettings.PaperSize);
			}
			if (!string.IsNullOrEmpty(clientExportManagerPdfSettings.ProxyURL))
			{
				dictionary.Add("proxyURL", clientExportManagerPdfSettings.ProxyURL);
			}
			if (!string.IsNullOrEmpty(clientExportManagerPdfSettings.Subject))
			{
				dictionary.Add("subject", clientExportManagerPdfSettings.Subject);
			}
			if (!string.IsNullOrEmpty(clientExportManagerPdfSettings.Title))
			{
				dictionary.Add("title", clientExportManagerPdfSettings.Title);
			}
			if (!string.IsNullOrEmpty(clientExportManagerPdfSettings.PageBreakSelector))
			{
				dictionary.Add("pageBreakSelector", clientExportManagerPdfSettings.PageBreakSelector);
			}
			if (clientExportManagerPdfSettings.Fonts.Count > 0)
			{
				dictionary.Add("fonts", serializer.Serialize(clientExportManagerPdfSettings.Fonts));
			}
			return dictionary;
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x0002CD3C File Offset: 0x0002AF3C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(ClientExportManagerPdfSettings);
				yield break;
			}
		}
	}
}
