using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x0200044A RID: 1098
	public class RadDiagramConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06002792 RID: 10130 RVA: 0x000805AC File Offset: 0x0007E7AC
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			RadDiagram radDiagram = obj as RadDiagram;
			ExplicitJavaScriptConverter.AddProperty(state, "theme", radDiagram.RuntimeSkin, "Default");
			ExplicitJavaScriptConverter.AddProperty(state, "dataSource", radDiagram.ResolveClientDataSourceID(radDiagram.ClientDataSourceID), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "connectionsDataSource", radDiagram.ResolveClientDataSourceID(radDiagram.ConnectionsClientDataSourceID), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "connectionDefaults", radDiagram.ConnectionDefaultsSettings, null);
			if (radDiagram.ConnectionsCollection.Count != 0)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "connections", radDiagram.ConnectionsCollection.ItemsList, null);
			}
			ExplicitJavaScriptConverter.AddProperty(state, "editable", radDiagram.Editable, true);
			if (radDiagram.Editable)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "editable", radDiagram.EditableSettings, null);
			}
			ExplicitJavaScriptConverter.AddProperty(state, "layout", radDiagram.LayoutSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "pannable", radDiagram.Pannable, true);
			if (radDiagram.Pannable)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "pannable", radDiagram.PannableSettings, null);
			}
			ExplicitJavaScriptConverter.AddProperty(state, "pdf", radDiagram.PdfSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "selectable", radDiagram.Selectable, true);
			if (radDiagram.Selectable)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "selectable", radDiagram.SelectableSettings, null);
			}
			ExplicitJavaScriptConverter.AddProperty(state, "shapeDefaults", radDiagram.ShapeDefaultsSettings, null);
			if (radDiagram.ShapesCollection.Count != 0)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "shapes", radDiagram.ShapesCollection.ItemsList, null);
			}
			if (radDiagram.Template.StartsWith("javascript:", StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(state, "template", radDiagram.Template.Substring(11).TrimStart(new char[0]));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(state, "template", radDiagram.Template, "");
			}
			ExplicitJavaScriptConverter.AddProperty(state, "zoom", radDiagram.Zoom, 1.0);
			ExplicitJavaScriptConverter.AddProperty(state, "zoomMax", radDiagram.ZoomMax, 2.0);
			ExplicitJavaScriptConverter.AddProperty(state, "zoomMin", radDiagram.ZoomMin, 0.1);
			ExplicitJavaScriptConverter.AddProperty(state, "zoomRate", radDiagram.ZoomRate, 0.1);
			base.AddScript(state, "add", radDiagram.ClientEvents.OnAdd);
			base.AddScript(state, "cancel", radDiagram.ClientEvents.OnCancel);
			base.AddScript(state, "change", radDiagram.ClientEvents.OnChange);
			base.AddScript(state, "click", radDiagram.ClientEvents.OnClick);
			base.AddScript(state, "dataBound", radDiagram.ClientEvents.OnDataBound);
			base.AddScript(state, "drag", radDiagram.ClientEvents.OnDrag);
			base.AddScript(state, "dragEnd", radDiagram.ClientEvents.OnDragEnd);
			base.AddScript(state, "dragStart", radDiagram.ClientEvents.OnDragStart);
			base.AddScript(state, "edit", radDiagram.ClientEvents.OnEdit);
			base.AddScript(state, "itemBoundsChange", radDiagram.ClientEvents.OnItemBoundsChange);
			base.AddScript(state, "itemRotate", radDiagram.ClientEvents.OnItemRotate);
			base.AddScript(state, "mouseEnter", radDiagram.ClientEvents.OnMouseEnter);
			base.AddScript(state, "mouseLeave", radDiagram.ClientEvents.OnMouseLeave);
			base.AddScript(state, "pan", radDiagram.ClientEvents.OnPan);
			base.AddScript(state, "remove", radDiagram.ClientEvents.OnRemove);
			base.AddScript(state, "save", radDiagram.ClientEvents.OnSave);
			base.AddScript(state, "select", radDiagram.ClientEvents.OnSelect);
			base.AddScript(state, "toolBarClick", radDiagram.ClientEvents.OnToolBarClick);
			base.AddScript(state, "zoomEnd", radDiagram.ClientEvents.OnZoomEnd);
			base.AddScript(state, "zoomStart", radDiagram.ClientEvents.OnZoomStart);
		}

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x06002793 RID: 10131 RVA: 0x000809EC File Offset: 0x0007EBEC
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(RadDiagram)
				};
			}
		}
	}
}
