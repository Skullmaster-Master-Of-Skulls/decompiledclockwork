using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02000247 RID: 583
	public class DiagramShapeConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06001571 RID: 5489 RVA: 0x00049784 File Offset: 0x00047984
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			DiagramShape diagramShape = obj as DiagramShape;
			ExplicitJavaScriptConverter.AddProperty(state, "id", diagramShape.Id, "");
			ExplicitJavaScriptConverter.AddProperty(state, "editable", diagramShape.Editable, true);
			if (diagramShape.Editable)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "editable", diagramShape.EditableSettings, null);
			}
			ExplicitJavaScriptConverter.AddProperty(state, "path", diagramShape.Path, "");
			ExplicitJavaScriptConverter.AddProperty(state, "stroke", diagramShape.StrokeSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "type", diagramShape.Type, "rectangle");
			ExplicitJavaScriptConverter.AddProperty(state, "x", diagramShape.X, 0.0);
			ExplicitJavaScriptConverter.AddProperty(state, "y", diagramShape.Y, 0.0);
			ExplicitJavaScriptConverter.AddProperty(state, "minWidth", diagramShape.MinWidth, 20.0);
			ExplicitJavaScriptConverter.AddProperty(state, "minHeight", diagramShape.MinHeight, 20.0);
			ExplicitJavaScriptConverter.AddProperty(state, "width", diagramShape.Width, 100.0);
			ExplicitJavaScriptConverter.AddProperty(state, "height", diagramShape.Height, 100.0);
			ExplicitJavaScriptConverter.AddProperty(state, "fill", diagramShape.Fill, "");
			ExplicitJavaScriptConverter.AddProperty(state, "fill", diagramShape.FillSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "hover", diagramShape.HoverSettings, null);
			if (diagramShape.ConnectorsCollection.Count != 0)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "connectors", diagramShape.ConnectorsCollection.ItemsList, null);
			}
			ExplicitJavaScriptConverter.AddProperty(state, "rotation", diagramShape.RotationSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "content", diagramShape.ContentSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "selectable", diagramShape.Selectable, true);
			base.AddScript(state, "visual", diagramShape.Visual);
			ExplicitJavaScriptConverter.AddProperty(state, "connectorDefaults", diagramShape.ConnectorDefaultsSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "source", UrlHelpers.ToAbsolute(diagramShape.Source), "");
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001572 RID: 5490 RVA: 0x000499D8 File Offset: 0x00047BD8
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(DiagramShape)
				};
			}
		}
	}
}
