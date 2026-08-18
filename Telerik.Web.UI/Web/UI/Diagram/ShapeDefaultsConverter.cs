using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x0200044E RID: 1102
	public class ShapeDefaultsConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060027D2 RID: 10194 RVA: 0x000814C8 File Offset: 0x0007F6C8
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			ShapeDefaults shapeDefaults = obj as ShapeDefaults;
			if (shapeDefaults.ConnectorsCollection.Count != 0)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "connectors", shapeDefaults.ConnectorsCollection.ItemsList, null);
			}
			ExplicitJavaScriptConverter.AddProperty(state, "connectorDefaults", shapeDefaults.ConnectorDefaultsSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "content", shapeDefaults.ContentSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "editable", shapeDefaults.Editable, true);
			if (shapeDefaults.Editable)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "editable", shapeDefaults.EditableSettings, null);
			}
			ExplicitJavaScriptConverter.AddProperty(state, "fill", shapeDefaults.Fill, "");
			ExplicitJavaScriptConverter.AddProperty(state, "fill", shapeDefaults.FillSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "height", shapeDefaults.Height, 100.0);
			ExplicitJavaScriptConverter.AddProperty(state, "hover", shapeDefaults.HoverSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "minHeight", shapeDefaults.MinHeight, 20.0);
			ExplicitJavaScriptConverter.AddProperty(state, "minWidth", shapeDefaults.MinWidth, 20.0);
			ExplicitJavaScriptConverter.AddProperty(state, "path", shapeDefaults.Path, "");
			ExplicitJavaScriptConverter.AddProperty(state, "rotation", shapeDefaults.RotationSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "selectable", shapeDefaults.Selectable, true);
			ExplicitJavaScriptConverter.AddProperty(state, "source", shapeDefaults.Source, "");
			ExplicitJavaScriptConverter.AddProperty(state, "stroke", shapeDefaults.StrokeSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "type", shapeDefaults.Type, "rectangle");
			base.AddScript(state, "visual", shapeDefaults.Visual);
			ExplicitJavaScriptConverter.AddProperty(state, "width", shapeDefaults.Width, 100.0);
			ExplicitJavaScriptConverter.AddProperty(state, "x", shapeDefaults.X, 0.0);
			ExplicitJavaScriptConverter.AddProperty(state, "y", shapeDefaults.Y, 0.0);
		}

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x060027D3 RID: 10195 RVA: 0x00081700 File Offset: 0x0007F900
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(ShapeDefaults)
				};
			}
		}
	}
}
