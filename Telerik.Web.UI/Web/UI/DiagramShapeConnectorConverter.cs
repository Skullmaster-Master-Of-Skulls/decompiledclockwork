using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02000245 RID: 581
	public class DiagramShapeConnectorConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x0600156C RID: 5484 RVA: 0x000496F4 File Offset: 0x000478F4
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			DiagramShapeConnector diagramShapeConnector = obj as DiagramShapeConnector;
			ExplicitJavaScriptConverter.AddProperty(state, "name", diagramShapeConnector.Name, "");
			ExplicitJavaScriptConverter.AddProperty(state, "description", diagramShapeConnector.Description, "");
			base.AddScript(state, "position", diagramShapeConnector.Position);
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x0600156D RID: 5485 RVA: 0x00049748 File Offset: 0x00047948
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(DiagramShapeConnector)
				};
			}
		}
	}
}
