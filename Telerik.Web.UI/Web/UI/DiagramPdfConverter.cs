using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02000242 RID: 578
	public class DiagramPdfConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06001539 RID: 5433 RVA: 0x00048ED8 File Offset: 0x000470D8
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			DiagramPdf diagramPdf = obj as DiagramPdf;
			ExplicitJavaScriptConverter.AddProperty(state, "author", diagramPdf.Author, null);
			ExplicitJavaScriptConverter.AddProperty(state, "creator", diagramPdf.Creator, "Kendo UI PDF Generator");
			ExplicitJavaScriptConverter.AddProperty(state, "date", diagramPdf.Date, null);
			ExplicitJavaScriptConverter.AddProperty(state, "fileName", diagramPdf.FileName, "Export.pdf");
			ExplicitJavaScriptConverter.AddProperty(state, "forceProxy", diagramPdf.ForceProxy, false);
			ExplicitJavaScriptConverter.AddProperty(state, "keywords", diagramPdf.Keywords, null);
			ExplicitJavaScriptConverter.AddProperty(state, "landscape", diagramPdf.Landscape, false);
			ExplicitJavaScriptConverter.AddProperty(state, "margin", diagramPdf.MarginSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "paperSize", diagramPdf.PaperSize, "auto");
			ExplicitJavaScriptConverter.AddProperty(state, "proxyURL", diagramPdf.ProxyURL, null);
			ExplicitJavaScriptConverter.AddProperty(state, "proxyTarget", diagramPdf.ProxyTarget, "_self");
			ExplicitJavaScriptConverter.AddProperty(state, "subject", diagramPdf.Subject, null);
			ExplicitJavaScriptConverter.AddProperty(state, "title", diagramPdf.Title, null);
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x0600153A RID: 5434 RVA: 0x00049000 File Offset: 0x00047200
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(DiagramPdf)
				};
			}
		}
	}
}
