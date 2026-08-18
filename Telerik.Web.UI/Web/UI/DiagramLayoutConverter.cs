using System;
using System.Collections.Generic;
using Telerik.Web.UI.Diagram;

namespace Telerik.Web.UI
{
	// Token: 0x02000240 RID: 576
	public class DiagramLayoutConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06001517 RID: 5399 RVA: 0x00048870 File Offset: 0x00046A70
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			DiagramLayout diagramLayout = obj as DiagramLayout;
			ExplicitJavaScriptConverter.AddProperty(state, "endRadialAngle", diagramLayout.EndRadialAngle, 360.0);
			ExplicitJavaScriptConverter.AddProperty(state, "grid", diagramLayout.GridSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "horizontalSeparation", diagramLayout.HorizontalSeparation, 90.0);
			ExplicitJavaScriptConverter.AddProperty(state, "iterations", diagramLayout.Iterations, 300.0);
			ExplicitJavaScriptConverter.AddProperty(state, "layerSeparation", diagramLayout.LayerSeparation, 50.0);
			ExplicitJavaScriptConverter.AddProperty(state, "nodeDistance", diagramLayout.NodeDistance, 50.0);
			ExplicitJavaScriptConverter.AddProperty(state, "radialFirstLevelSeparation", diagramLayout.RadialFirstLevelSeparation, 200.0);
			ExplicitJavaScriptConverter.AddProperty(state, "radialSeparation", diagramLayout.RadialSeparation, 150.0);
			ExplicitJavaScriptConverter.AddProperty(state, "startRadialAngle", diagramLayout.StartRadialAngle, 0.0);
			ExplicitJavaScriptConverter.AddProperty(state, "subtype", StringHelpers.ToCamelCase(diagramLayout.Subtype.ToString()), StringHelpers.ToCamelCase(LayoutSubtype.Down.ToString()));
			ExplicitJavaScriptConverter.AddProperty(state, "tipOverTreeStartLevel", diagramLayout.TipOverTreeStartLevel, 0.0);
			ExplicitJavaScriptConverter.AddProperty(state, "type", StringHelpers.ToCamelCase(diagramLayout.Type.ToString()), StringHelpers.ToCamelCase(LayoutType.Tree.ToString()));
			ExplicitJavaScriptConverter.AddProperty(state, "underneathHorizontalOffset", diagramLayout.UnderneathHorizontalOffset, 15.0);
			ExplicitJavaScriptConverter.AddProperty(state, "underneathVerticalSeparation", diagramLayout.UnderneathVerticalSeparation, 15.0);
			ExplicitJavaScriptConverter.AddProperty(state, "underneathVerticalTopOffset", diagramLayout.UnderneathVerticalTopOffset, 15.0);
			ExplicitJavaScriptConverter.AddProperty(state, "verticalSeparation", diagramLayout.VerticalSeparation, 50.0);
			ExplicitJavaScriptConverter.AddProperty(state, "enabled", diagramLayout.Enabled, false);
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06001518 RID: 5400 RVA: 0x00048AE8 File Offset: 0x00046CE8
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(DiagramLayout)
				};
			}
		}
	}
}
