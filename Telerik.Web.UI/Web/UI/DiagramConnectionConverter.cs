using System;
using System.Collections.Generic;
using Telerik.Web.UI.Diagram;

namespace Telerik.Web.UI
{
	// Token: 0x0200022D RID: 557
	public class DiagramConnectionConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x0600149B RID: 5275 RVA: 0x000473F4 File Offset: 0x000455F4
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			DiagramConnection diagramConnection = obj as DiagramConnection;
			ExplicitJavaScriptConverter.AddProperty(state, "id", diagramConnection.Id, "");
			ExplicitJavaScriptConverter.AddProperty(state, "content", diagramConnection.ContentSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "editable", diagramConnection.Editable, true);
			if (diagramConnection.Editable)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "editable", diagramConnection.EditableSettings, null);
			}
			ExplicitJavaScriptConverter.AddProperty(state, "fromConnector", diagramConnection.FromConnector, "Auto");
			ExplicitJavaScriptConverter.AddProperty(state, "fromX", diagramConnection.FromX, 0.0);
			ExplicitJavaScriptConverter.AddProperty(state, "fromY", diagramConnection.FromY, 0.0);
			ExplicitJavaScriptConverter.AddProperty(state, "stroke", diagramConnection.StrokeSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "hover", diagramConnection.HoverSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "startCap", diagramConnection.StartCap.ToString(), ConnectionStartCap.None.ToString());
			ExplicitJavaScriptConverter.AddProperty(state, "startCap", diagramConnection.StartCapSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "endCap", diagramConnection.EndCap.ToString(), ConnectionEndCap.None.ToString());
			ExplicitJavaScriptConverter.AddProperty(state, "endCap", diagramConnection.EndCapSettings, null);
			if (diagramConnection.PointsCollection.Count != 0)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "points", diagramConnection.PointsCollection.ItemsList, null);
			}
			ExplicitJavaScriptConverter.AddProperty(state, "selectable", diagramConnection.Selectable, true);
			ExplicitJavaScriptConverter.AddProperty(state, "toConnector", diagramConnection.ToConnector, "Auto");
			ExplicitJavaScriptConverter.AddProperty(state, "toX", diagramConnection.ToX, 0.0);
			ExplicitJavaScriptConverter.AddProperty(state, "toY", diagramConnection.ToY, 0.0);
			ExplicitJavaScriptConverter.AddProperty(state, "type", StringHelpers.ToCamelCase(diagramConnection.Type.ToString()), StringHelpers.ToCamelCase(ConnectionType.Cascading.ToString()));
			ExplicitJavaScriptConverter.AddProperty(state, "from", diagramConnection.FromSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "to", diagramConnection.ToSettings, null);
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x0004764C File Offset: 0x0004584C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(DiagramConnection)
				};
			}
		}
	}
}
