using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x0200023E RID: 574
	public class DiagramGridConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060014ED RID: 5357 RVA: 0x000481A8 File Offset: 0x000463A8
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			DiagramGrid diagramGrid = obj as DiagramGrid;
			ExplicitJavaScriptConverter.AddProperty(state, "componentSpacingX", diagramGrid.ComponentSpacingX, 50.0);
			ExplicitJavaScriptConverter.AddProperty(state, "componentSpacingY", diagramGrid.ComponentSpacingY, 50.0);
			ExplicitJavaScriptConverter.AddProperty(state, "offsetX", diagramGrid.OffsetX, 50.0);
			ExplicitJavaScriptConverter.AddProperty(state, "offsetY", diagramGrid.OffsetY, 50.0);
			ExplicitJavaScriptConverter.AddProperty(state, "width", diagramGrid.Width, 1500.0);
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x060014EE RID: 5358 RVA: 0x00048270 File Offset: 0x00046470
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(DiagramGrid)
				};
			}
		}
	}
}
