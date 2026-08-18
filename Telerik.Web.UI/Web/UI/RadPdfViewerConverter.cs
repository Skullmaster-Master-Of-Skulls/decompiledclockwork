using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200066E RID: 1646
	public class RadPdfViewerConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06003C2C RID: 15404 RVA: 0x000C3608 File Offset: 0x000C1808
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			RadPdfViewer radPdfViewer = obj as RadPdfViewer;
			ExplicitJavaScriptConverter.AddProperty(state, "theme", radPdfViewer.RuntimeSkin, "Default");
			ExplicitJavaScriptConverter.AddProperty(state, "pdfjsProcessing", radPdfViewer.PdfjsProcessingSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "width", radPdfViewer.Width, new Unit(1000));
			ExplicitJavaScriptConverter.AddProperty(state, "height", radPdfViewer.Height, new Unit(1200));
			ExplicitJavaScriptConverter.AddProperty(state, "defaultPageSize", radPdfViewer.DefaultPageSizeSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "page", radPdfViewer.ActivePage, 1);
			ExplicitJavaScriptConverter.AddProperty(state, "scale", radPdfViewer.Scale.Value, 0.0);
			ExplicitJavaScriptConverter.AddProperty(state, "scaleText", radPdfViewer.Scale.Text, "Automatic Width");
			ExplicitJavaScriptConverter.AddProperty(state, "zoomMin", radPdfViewer.ZoomMin, 0.5);
			ExplicitJavaScriptConverter.AddProperty(state, "zoomMax", radPdfViewer.ZoomMax, 4.0);
			ExplicitJavaScriptConverter.AddProperty(state, "zoomRate", radPdfViewer.ZoomRate, 0.25);
			ExplicitJavaScriptConverter.AddProperty(state, "messages", radPdfViewer.MessagesSettings, null);
			if (radPdfViewer.ToolBar)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "toolbar", radPdfViewer.ToolBarSettings, null);
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(state, "toolbar", radPdfViewer.ToolBar, true);
			}
			base.AddScript(state, "render", radPdfViewer.ClientEvents.OnRender);
			base.AddScript(state, "open", radPdfViewer.ClientEvents.OnOpen);
			base.AddScript(state, "error", radPdfViewer.ClientEvents.OnError);
		}

		// Token: 0x170013D3 RID: 5075
		// (get) Token: 0x06003C2D RID: 15405 RVA: 0x000C37FC File Offset: 0x000C19FC
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(RadPdfViewer)
				};
			}
		}
	}
}
